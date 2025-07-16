using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Xml;
using System.Text;
using System.IO;
using System.Data.Common;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;

public partial class procs_VSCB_PaymentRequestView : System.Web.UI.Page
{
    #region CreativeMethods
    SqlConnection source;
    public SqlDataAdapter sqladp;

    public string loc = "", approveremailaddress = "", Approvers_code = "";
    public int did = 0, apprid;
    public DataTable dtEmp, DTPoWoNumber, DTInsertPayment, dtApproverEmailIds, dtPaymentStatus, dtextraApp;
    public string filename = "";
    public string nxtapprcode = "", nxtapprname = "";
    SP_Methods spm = new SP_Methods();

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    #endregion

    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
            hdnEmpCode.Value = Session["Empcode"].ToString();

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/vscb_index.aspx");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VendorPaymentDocpath"]).Trim());
                    FilePathInvoice.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBInvoicefiles"]).Trim());
                    hdnSingPOCopyFilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBSignPOWOfiles"]).Trim());

                    txtAccountPaidAmt.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    hdnLoginUserName.Value = Convert.ToString(Session["emp_loginName"]).Trim();
                    hdnLoginEmpEmail.Value = Convert.ToString(Session["LoginEmpmail"]).Trim();
                    if (Request.QueryString.Count > 0)
                    {
                        hdnPayment_ID.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdnPOID.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        CheckAccountApprovalEmp();
                        PaymentDetails();
                        btnRecBack.Visible = true;
                        if (Request.QueryString.Count > 2)
                        {
                            if (Convert.ToString(Request.QueryString["IsAmtApprover"]).Trim() == "1")
                            {
                                localtrvl_btnSave.Visible = true;
                                localtrvl_btnSave.Text = "Save";
                                txtAccountPaidAmt.Enabled = true;
                                btnRecBack.HRef = "VSCB_ApprovedPaymentRequestViewForChange.aspx";
                                trvldeatils_cancel_btn.PostBackUrl = "VSCB_ApprovedPaymentRequestViewForChange.aspx";
                            }
                            else
                            {
                                localtrvl_btnSave.Visible = false;
                                txtAccountPaidAmt.Enabled = false;
                                btnRecBack.HRef = "VSCB_InboxPaymentRequestView.aspx";
                                trvldeatils_cancel_btn.PostBackUrl = "VSCB_InboxPaymentRequestView.aspx";
                            }
                        }
                    }

                    hdnApprovedPO_FileName.Value = Convert.ToString(Regex.Replace(Convert.ToString(txtPOWONumber.Text), @"[^0-9a-zA-Z\._]", "_")).Trim() + ".pdf";
                    hdnApprovedPO_FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBApprovedPOWOfiles"]).Trim());
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    protected void localtrvl_btnSave_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }
        if (Convert.ToString(Request.QueryString["IsAmtApprover"]).Trim() == "1")
        {
            if (txtAccountPaidAmt.Text == "" || Convert.ToDecimal(txtAccountPaidAmt.Text)<=0)
            {
                lblmessage.Text = "Amount Paid By Account cannot be zero or blank.";
                return;
            }

            decimal AmtToBePaid = 0, AmtPaidByAcc = 0, BalanceAmt = 0, TotalPaidAmt = 0;
            int isPartialAmt = 0;
            AmtToBePaid = Convert.ToDecimal(txtAmountPaidWithTax.Text);
            AmtPaidByAcc = Convert.ToDecimal(txtAccountPaidAmt.Text);
            BalanceAmt = Convert.ToDecimal(txtAccountAmtBal.Text);

            TotalPaidAmt = AmtPaidByAcc + BalanceAmt;

            

            if (TotalPaidAmt > AmtToBePaid)
            {
                lblmessage.Text = "Amount Paid By Account cannot exceeds to The Actual Amount To Be Paid.";
                return;
            }
            BalanceAmt = TotalPaidAmt - AmtPaidByAcc;

            spm.UpdateAmountPaidByAccount(Convert.ToInt32(hdnPayment_ID.Value), AmtPaidByAcc, BalanceAmt, hdnEmpCode.Value);

            Response.Redirect("~/procs/VSCB_ApprovedPaymentRequestViewForChange.aspx", false);
        }
        else
        {
            lblmessage.Text = "You have not access to change.";
        }
    }

    protected string GetPaymnetApprove_RejectList(string EmpCode, int Payment_ID, string DeptName, string TallyCode)
    {
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        dtAppRej = spm.Get_Payment_Request_ApproverList(EmpCode, Payment_ID, DeptName, TallyCode, Convert.ToString(txtPoWoType.Text).Trim());
        if (dtAppRej.Rows.Count > 0)
        {
            sbapp.Append("<table>");
            for (int i = 0; i < dtAppRej.Rows.Count; i++)
            {
                sbapp.Append("<tr>");
                sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
                sbapp.Append("</tr>");
            }
            sbapp.Append("</table>");
        }
        return Convert.ToString(sbapp);
    }

    protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
    {

    }

    protected void lnkdelete_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void accmo_delete_btn_Click(object sender, EventArgs e)
    {
        PaymentDetails();
        accmo_delete_btn.Visible = false;
    }

    protected void txtAccountPaidAmt_TextChanged(object sender, EventArgs e)
    {
        decimal Result = 0;
        if (txtAccountPaidAmt.Text != "")
        {

            if (Convert.ToDecimal(txtAccountPaidAmt.Text) <= Convert.ToDecimal(txtAmountPaidWithTax.Text))
            {
                Result = Convert.ToDecimal(txtAmountPaidWithTax.Text) - Convert.ToDecimal(txtAccountPaidAmt.Text);
                txtAccountAmtBal.Text = Result.ToString();
            }
            else
            {
                //txtAccountAmtBal.Text = Result.ToString();
                //txtAccountPaidAmt.Text = Result.ToString();
                lblmessage.Text = "Amount Paid By Account cannot exceeds to The Actual Amount To Be Paid";
                return;
            }
        }
        else
        {
            //txtAccountAmtBal.Text = txtAmountPaidWithTax.Text;
        }
    }
    #endregion

    protected void lnkfile_Invoice_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBInvoicefiles"]).Trim()), lnkfile_Invoice.Text);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
            Response.WriteFile(strfilepath);
            Response.End();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }


    #region PageMethods
    private void PaymentDetails()
    {
        try
        {
            DataSet DTPoWoNumber = new DataSet();
            int Payment_ID = 0, POID = 0;
            Payment_ID = Convert.ToString(hdnPayment_ID.Value).Trim() != "" ? Convert.ToInt32(hdnPayment_ID.Value) : 0;
            POID = Convert.ToString(hdnPOID.Value).Trim() != "" ? Convert.ToInt32(hdnPOID.Value) : 0;
            DTPoWoNumber = spm.GetVSCB_MyPaymentDetails("POWOMyPaymentRequestDeatils", Convert.ToString(hdnEmpCode.Value), POID, Payment_ID);
            if (DTPoWoNumber.Tables[0].Rows.Count > 0)
            {
                txtPoWoDate.Text = DTPoWoNumber.Tables[0].Rows[0]["PODate"].ToString();
                txtPOWONumber.Text = DTPoWoNumber.Tables[0].Rows[0]["PONumber"].ToString();
                txtPoWoTitle.Text = DTPoWoNumber.Tables[0].Rows[0]["POTitle"].ToString();
                txtPoWoTitle.ToolTip = DTPoWoNumber.Tables[0].Rows[0]["POTitle"].ToString();
                txtPoWoType.Text = DTPoWoNumber.Tables[0].Rows[0]["POType"].ToString();
                txtVendorName.Text = DTPoWoNumber.Tables[0].Rows[0]["Name"].ToString();
                txtGSTINNO.Text = DTPoWoNumber.Tables[0].Rows[0]["GSTIN_NO"].ToString();
                txtPoWoStatus.Text = DTPoWoNumber.Tables[0].Rows[0]["PyamentStatus"].ToString();
                txtPoWoAmtWithTaxes.Text = DTPoWoNumber.Tables[0].Rows[0]["POWOAmt"].ToString();
                txtPoWOAmtWIthoutTax.Text = DTPoWoNumber.Tables[0].Rows[0]["POWO_T_BaseAmt"].ToString();
                txtPoWOPaidAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["PO_Paid_Amt"].ToString();
                txtPoPaidAmt_WithOutDT.Text = DTPoWoNumber.Tables[0].Rows[0]["POPiadAmount_withoutDT"].ToString();
                txtPoWOPaidBalAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["PO_Bal_Amt"].ToString();
                txtPODirectTaxAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["DirectTaxCollection_Amt"].ToString();
                txtProjectName.Text = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
                txtDepartment.Text = DTPoWoNumber.Tables[0].Rows[0]["Department"].ToString();
                hdnCostCentre.Value = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
                //hdnTallyCode.Value = DTPoWoNumber.Tables[0].Rows[0]["Project_Name"].ToString();
                hdnDept_Name.Value = DTPoWoNumber.Tables[0].Rows[0]["Department"].ToString();

                txtCurrency.Text = Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["CurName"]);
                txtPOWOSettelmentAmt.Text = Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["PO_SettelmentAmt"]).Trim();


                if (Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["powo_signCopy"]).Trim() != "")
                {
                    lnkfile_PO.Text = Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["powo_signCopy"]).Trim();
                    spnPOWOSignCopy.Visible = true;
                    lnkfile_PO.Visible = true;
                }
            }
            if (DTPoWoNumber.Tables[1].Rows.Count > 0)
            {
                DgvMilestones.DataSource = DTPoWoNumber.Tables[1];
                DgvMilestones.DataBind();
            }
            if (DTPoWoNumber.Tables[6].Rows.Count > 0)
            {
                GrdInvoiceDetails.DataSource = DTPoWoNumber.Tables[6];
                GrdInvoiceDetails.DataBind();
            }
            if (DTPoWoNumber.Tables[7].Rows.Count > 0)
            {
                GridViewAccountpaidbyAmount.DataSource = DTPoWoNumber.Tables[7];
                GridViewAccountpaidbyAmount.DataBind();
            }

            if (DTPoWoNumber.Tables[3].Rows.Count > 0)
            {
                //DivCreateInvoice.Visible = true;
                txtInvoiceNo.Text = DTPoWoNumber.Tables[3].Rows[0]["InvoiceNo"].ToString();
                hdnInvoiceID.Value = DTPoWoNumber.Tables[3].Rows[0]["InvoiceID"].ToString();
                txtInvoiceAmount.Text = DTPoWoNumber.Tables[3].Rows[0]["AmtWithTax"].ToString(); // //AmtWithTax Payable_Amt_With_Tax
                txtInvoiceBalAmt.Text = DTPoWoNumber.Tables[3].Rows[0]["BalanceAmt"].ToString();
                //txtVendorName.Text = DTCreateInvoice.Rows[0]["Status_id"].ToString();
                //txtGSTINNO.Text = DTCreateInvoice.Rows[0]["PaymentStatusID"].ToString();
                txtPaymentRequestNo.Text = DTPoWoNumber.Tables[3].Rows[0]["PaymentReqNo"].ToString();
                txtRequestDate.Text = DTPoWoNumber.Tables[3].Rows[0]["PaymentReqDate"].ToString();
                txtAmountPaidWithTax.Text = DTPoWoNumber.Tables[3].Rows[0]["TobePaidAmtWithtax"].ToString();
                hdnEmpCodePrve.Value = DTPoWoNumber.Tables[3].Rows[0]["Emp_Code"].ToString();
                txtAmountPaidWithTax.Text = DTPoWoNumber.Tables[3].Rows[0]["TobePaidAmtWithtax"].ToString();
                lnkfile_Invoice.Text = DTPoWoNumber.Tables[3].Rows[0]["Invoice_File"].ToString();

                txtPaymentRequestAmount.Text = DTPoWoNumber.Tables[3].Rows[0]["paymentRequestForAmt"].ToString(); //DirectTax_Amount
                txtInvoiceTDSAmt.Text = DTPoWoNumber.Tables[3].Rows[0]["DirectTax_Amount"].ToString();
                txtInvoicePaidAmount.Text = DTPoWoNumber.Tables[3].Rows[0]["AccountPaidAmt"].ToString(); //InvoicePaidAmt;

                lblheading.Text = "Payment Request View - " + txtPaymentRequestNo.Text + ", Payment Status - " + DTPoWoNumber.Tables[3].Rows[0]["PyamentStatus"].ToString();

                if (Convert.ToInt32(DTPoWoNumber.Tables[3].Rows[0]["Status_id"]) == 2 || Convert.ToInt32(DTPoWoNumber.Tables[3].Rows[0]["Status_id"]) == 3)
                {
                    Account1.Visible = true;
                    Account2.Visible = true;
                    Account3.Visible = true;
                    txtAccountPaidAmt.Text = DTPoWoNumber.Tables[3].Rows[0]["Amt_paid_Account"].ToString();
                    txtAccountAmtBal.Text = DTPoWoNumber.Tables[3].Rows[0]["AccountBalAmt"].ToString();
                    txtAccountPaidAmt.Enabled = false;
                    txtAccountAmtBal.Enabled = false;

                    SpanAccounttobepaid.Visible = true;
                    Span2.Visible = true;

                }
                getInvoiceUploadedFiles();
            }
            if (DTPoWoNumber.Tables[4].Rows.Count > 0)
            {
                liPaySuppFiles1.Visible = true;
                liPaySuppFiles2.Visible = true;
                liPaySuppFiles3.Visible = true;
                liGrdFileUpload.Visible = true;

                spnPaymentSupportingFile.Visible = true;
                GrdFileUpload.DataSource = DTPoWoNumber.Tables[4];
                GrdFileUpload.DataBind();
            }
            if (DTPoWoNumber.Tables[5].Rows.Count > 0)
            {
                SPPayHist.Visible = true;
                GrdInvoiceHistDetails.DataSource = DTPoWoNumber.Tables[5];
                GrdInvoiceHistDetails.DataBind();
            }

            getApproverlist(hdnEmpCode.Value, Payment_ID, hdnDept_Name.Value, hdnCostCentre.Value);

        }
        catch (Exception)
        {

            throw;
        }
    }

    public void getInvoiceUploadedFiles()
    {

        DataSet dsFiles = spm.getVSCB_UploadedFiles("get_VSCB_UploadedFiels", "Invoice", Convert.ToDouble(hdnInvoiceID.Value), "", "");
        gvuploadedFiles.DataSource = null;
        gvuploadedFiles.DataBind();
        if (dsFiles.Tables[0].Rows.Count > 0)
        {
            gvuploadedFiles.DataSource = dsFiles;
            gvuploadedFiles.DataBind();
            spnSupportinFiles.Visible = true;
            spnSupportinFiles1.Visible = true;
            spnSupportinFiles2.Visible = true;

            liInvoiceUploadFile.Visible = true;
            liInvoiceUploadFile2.Visible = true;
            liInvoiceUploadFile3.Visible = true;
        }
    }


    private void getApproverlist(string EmpCode, int Payment_ID, string DeptnName, string TallyCode)
    {
        DataTable dtapprover = new DataTable();
        dtapprover = spm.Get_Payment_Request_ApproverList(EmpCode, Payment_ID, DeptnName, TallyCode, Convert.ToString(txtPoWoType.Text).Trim());
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();
        if (dtapprover.Rows.Count > 0)
        {
            string status = "";
            for (int i = 0; i < dtapprover.Rows.Count; i++)
            {
                status += Convert.ToString(dtapprover.Rows[i]["Status"]);
            }
            if (status!="")
            {
                DgvApprover.DataSource = dtapprover;
                DgvApprover.DataBind();

                liPayApproval.Visible = true;
                liPayApproval2.Visible = true;
                liPayApproval3.Visible = true;
                liDgvApprover.Visible = true;
            }
            else
            {
                DgvApprover.Visible = false;
                liPayApproval.Visible = false;
            }

        }

    }
    public void Get_Account_ApproverCode()
    {
        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];
        spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
        spars[0].Value = "Get_Account_mail_Details";
        spars[2] = new SqlParameter("@Emp_code", SqlDbType.VarChar);
        spars[2].Value = hdnEmpCode.Value;
        dsTrDetails = spm.getDatasetList(spars, "SP_VSCB_GETPAYMENT_APPMETRIX");
        hdnApproverType.Value = "Approver";
        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            hdnnextappcode.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_code"]).Trim();
            hdnapprid.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_id"]).Trim();
            approveremailaddress = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();
            hdnAccountApp_Email.Value = approveremailaddress;
            hdnApproverEmail.Value = approveremailaddress;
            hdnNextappName.Value = "Account Team";
            hdnApproverType.Value = "NA";
        }
    }
    public void CheckAccountApprovalEmp()
    {
        try
        {
            hdnExtraAPP.Value = "";
            dtextraApp = spm.Get_Payment_AccountApprovalEmp("Get_Account_Payment_Details", Convert.ToString(Session["Empcode"]).Trim(), Convert.ToInt32(hdnPayment_ID.Value));
            if (dtextraApp.Rows.Count > 0)
            {
                hdnExtraAPP.Value = (string)dtextraApp.Rows[0]["app_remarks"].ToString().Trim();
                //hdnExtraAPPID.Value = (string)dtextraApp.Rows[0]["Appr_id"].ToString().Trim();
                if (hdnExtraAPP.Value.ToString().Trim() == "Account")
                {
                    localtrvl_btnSave.Text = "Verify";
                    localtrvl_btnSave.ToolTip = "Verify";
                    Account1.Visible = true;
                    Account2.Visible = true;
                    Account3.Visible = true;
                    SpanAccounttobepaid.Visible = true;
                    Span2.Visible = true;
                }
            }
            else
            {
                Account1.Visible = false;
                Account2.Visible = false;
                Account3.Visible = false;
                SpanAccounttobepaid.Visible = false;
                Span2.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            throw;
        }
    }
    public void GetEmployeeCode(string strtype)
    {

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];
        spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
        spars[0].Value = "GetEmployeeDetails";
        spars[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        spars[2].Value = strtype;
        dsTrDetails = spm.getDatasetList(spars, "sp_VSCB_CreatePOWO_Users");
        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            hdnEmpCodePrveName.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Emp_Name"]).Trim();
            hdnEmpCodePrveEmailID.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
        }
    }
    #endregion

    protected void lnkViewhist_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int Payment_ID = 0;
            Payment_ID = Convert.ToInt32(GrdInvoiceHistDetails.DataKeys[row.RowIndex].Values[0]);
            PaymentHistoryDetails(Payment_ID);
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void PaymentHistoryDetails(int Payment_ID)
    {
        try
        {
            DataSet DTPoWoNumber = new DataSet();
            int POID = 0;
            //SPCreate.InnerText = "View Payment Request";
            DivCreateInvoice.Visible = true;
            accmo_delete_btn.Visible = true;
            localtrvl_btnSave.Visible = false;
            trvldeatils_btnSave.Visible = false;
            txtAmountPaidWithTax.Enabled = false;
            //Payment_ID = Convert.ToString(hdnPayment_ID.Value).Trim() != "" ? Convert.ToInt32(hdnPayment_ID.Value) : 0;
            POID = Convert.ToString(hdnPOID.Value).Trim() != "" ? Convert.ToInt32(hdnPOID.Value) : 0;
            DTPoWoNumber = spm.GetVSCB_MyPaymentDetails("POWOPaymentRequestHistory", Convert.ToString(hdnEmpCode.Value), POID, Payment_ID);
            if (DTPoWoNumber.Tables[0].Rows.Count > 0)
            {
                //DivCreateInvoice.Visible = true;
                txtInvoiceNo.Text = DTPoWoNumber.Tables[0].Rows[0]["InvoiceNo"].ToString();
                txtInvoiceAmount.Text = DTPoWoNumber.Tables[0].Rows[0]["AmtWithTax"].ToString();
                txtInvoiceBalAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["BalanceAmt"].ToString();
                txtPaymentRequestNo.Text = DTPoWoNumber.Tables[0].Rows[0]["PaymentReqNo"].ToString();
                txtRequestDate.Text = DTPoWoNumber.Tables[0].Rows[0]["PaymentReqDate"].ToString();
                txtAmountPaidWithTax.Text = DTPoWoNumber.Tables[0].Rows[0]["TobePaidAmtWithtax"].ToString();
                //hflStatusID.Value = DTPoWoNumber.Tables[0].Rows[0]["Status_id"].ToString();
                if (Convert.ToInt32(DTPoWoNumber.Tables[0].Rows[0]["Status_id"]) == 2)
                {
                    Account1.Visible = true;
                    Account2.Visible = true;
                    Account3.Visible = true;
                    txtAccountPaidAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["Amt_paid_Account"].ToString();
                    txtAccountAmtBal.Text = DTPoWoNumber.Tables[0].Rows[0]["AccountBalAmt"].ToString();
                    txtAccountPaidAmt.Enabled = false;
                    txtAccountAmtBal.Enabled = false;
                    SpanAccounttobepaid.Visible = true;
                }
                else
                {
                    Account1.Visible = false;
                    Account2.Visible = false;
                    Account3.Visible = false;
                    SpanAccounttobepaid.Visible = false;
                }
            }
            GrdFileUpload.DataSource = null;
            GrdFileUpload.DataBind();
            if (DTPoWoNumber.Tables[1].Rows.Count > 0)
            {
                GrdFileUpload.DataSource = DTPoWoNumber.Tables[1];
                GrdFileUpload.DataBind();
            }

            if (DTPoWoNumber.Tables[2].Rows.Count > 0)
            {
                GridViewAccountpaidbyAmount.DataSource = DTPoWoNumber.Tables[2];
                GridViewAccountpaidbyAmount.DataBind();
            }


        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void GrdInvoiceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string lsDataKeyValue = GrdInvoiceDetails.DataKeys[e.Row.RowIndex].Values[1].ToString();
                if (Convert.ToString(lsDataKeyValue).Trim() == "0")
                {
                    e.Row.Visible = false;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void lnkfile_PO_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBSignPOWOfiles"]).Trim()), lnkfile_PO.Text);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
            Response.WriteFile(strfilepath);
            Response.End();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }


    protected void btnTra_Details_Click(object sender, EventArgs e)
    {
        DataSet dsPOWOContent = new DataSet();

        #region get POWO Content 


        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_POWO_Content_FromTally";

        spars[1] = new SqlParameter("@POWO_Number", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtPOWONumber.Text).Trim();  //"PO/042021/00001"; 

        spars[2] = new SqlParameter("@POWOID", SqlDbType.Decimal);
        spars[2].Value = Convert.ToDouble(hdnPOID.Value);  //"PO/042021/00001";


        dsPOWOContent = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");

        #endregion

        try
        {



            string strpath = Server.MapPath("~/procs/VSCB_Rpt_POWO_Content_New.rdlc");
            string PowoNumber = Convert.ToString(txtPOWONumber.Text).Trim().Replace("/", "-");

            LocalReport ReportViewer2 = new LocalReport();
            ReportViewer2.ReportPath = strpath;// Server.MapPath(strpath);
            ReportDataSource rds = new ReportDataSource("dspowoContent", dsPOWOContent.Tables[0]);
            ReportDataSource rds_2 = new ReportDataSource("dsowoDetails", dsPOWOContent.Tables[1]);
            ReportDataSource rds_3 = new ReportDataSource("dsMilestone", dsPOWOContent.Tables[2]);
            ReportDataSource rds_4 = new ReportDataSource("dsPOWOAmountinWords", dsPOWOContent.Tables[3]);

            ReportDataSource rds_5 = new ReportDataSource("dsPOPreparedBy", dsPOWOContent.Tables[4]);
            ReportDataSource rds_6 = new ReportDataSource("dsPOCheckedBy", dsPOWOContent.Tables[5]);
            ReportDataSource rds_7 = new ReportDataSource("dsPOApprovedBY_HOD", dsPOWOContent.Tables[6]);
            ReportDataSource rds_8 = new ReportDataSource("dsPOApprovedBY_COO", dsPOWOContent.Tables[7]);
            ReportDataSource rds_9 = new ReportDataSource("dsPOApprovedBY_CEO", dsPOWOContent.Tables[8]);

            //if (Convert.ToString(dsPOWOContent.Tables[1].Rows[0]["Approver_Count"]).Trim() == "2")
            //{
            // ReportDataSource rds_6 = new ReportDataSource("dsPOCheckedBy", dsPOWOContent.Tables[5]);
            //  ReportDataSource rds_7 = new ReportDataSource("dsPOApprovedBY_HOD_COO", dsPOWOContent.Tables[6]);

            //  ReportViewer2.DataSources.Add(rds_7);
            //}


            ReportViewer2.DataSources.Clear();
            ReportViewer2.DataSources.Add(rds);
            ReportViewer2.DataSources.Add(rds_2);
            ReportViewer2.DataSources.Add(rds_3);
            ReportViewer2.DataSources.Add(rds_4);

            ReportViewer2.DataSources.Add(rds_5);
            ReportViewer2.DataSources.Add(rds_6);
            ReportViewer2.DataSources.Add(rds_7);
            ReportViewer2.DataSources.Add(rds_8);
            ReportViewer2.DataSources.Add(rds_9);
            ReportViewer2.Refresh();

            Warning[] warnings;
            string[] streamIds;
            string contentType;
            string encoding;
            string extension;

            //Export the RDLC Report to Byte Array.
            byte[] bytes = ReportViewer2.Render("pdf", null, out contentType, out encoding, out extension, out streamIds, out warnings);

            //Download the RDLC Report in Word, Excel, PDF and Image formats.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=POWO_" + PowoNumber + "." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();
        }
    }
}