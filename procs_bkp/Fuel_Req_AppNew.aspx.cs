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

public partial class procs_Fuel_Req_AppNew : System.Web.UI.Page
{
    #region CreativeMethods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    #endregion

    SP_Methods spm = new SP_Methods();
    DataSet dspaymentVoucher_Apprs = new DataSet();
    DataTable Summ = new DataTable();
    #region PageEvents
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["FuelClaimsDocumentpath"]).Trim());
                    editform.Visible = true;
                    divbtn.Visible = false;
                    divmsg.Visible = false;
                    lblmessage.Visible = true;
                    hdnempcode.Value = Convert.ToString(Session["Empcode"]);
                    txtFromdate.Text = Convert.ToString(Session["Fromdate"]);
                    GetEmployeeDetails();
                    txtCosApprovedAmt.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");

                    if (Request.QueryString.Count > 0)
                    {
                        hdnRemid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        // hdnClaimid.Value = Convert.ToString(Request.QueryString[1]).Trim(); 
                        hdnInboxType.Value = Convert.ToString(Request.QueryString[2]).Trim();
                    }
                    if (Convert.ToString(hdnRemid.Value).Trim() != "")
                    {

                        if (Request.QueryString.Count > 1)
                        {
                            
                            checkApprovalStatus_Submit();
                            getMobRemlsDetails_usingRemid();
                            InsertMobileRem_DatatoTempTables_trvl();
                            getFueClaimDetails();
                            get_employee_FuelUploaded_Files();

                        }
                        GetCuurentApprID();
                        getApproverdata();
                        getnextAppIntermediate();

                    }

                    mobile_btnPrintPV.Visible = false;
                    if (Convert.ToString(hdnInboxType.Value).Trim() == "RACC")
                    {
                        //fuel_btnSave.Text = "Disbursed";
                        fuel_btnSave.Visible = false;
                        fuel_btnSave_ACC.Text = "Approve";
                        fuel_btnSave_ACC.ToolTip = "Approve";
                        fuel_btnSave_ACC.Visible = true;
                        fuel_btnReject.Visible = false;
                        mobile_btnPrintPV.Visible = true;
                        fuel_btnCorrection.Visible = true;
                        txtCosApprovedAmt.Visible = true;
                        lblcostappamt.Visible = true;
                        txtCosApprovedAmt.Enabled = false;


                    }

                    if (Convert.ToString(hdnInboxType.Value).Trim() == "APP")
                    {
                        fuel_btnCorrection.Visible = true;
                        txtCosApprovedAmt.Visible = false;
                        lblcostappamt.Visible = false;
                    }
                    if (Convert.ToString(hdnInboxType.Value).Trim() == "RCOS" || Convert.ToString(hdnInboxType.Value).Trim() == "COS")
                    {

                        fuel_btnCorrection.Visible = true;
                        txtCosApprovedAmt.Visible = true;
                        txtCosApprovedAmt.Enabled = true;
                        lblcostappamt.Visible = true;
                    }

                    if (Convert.ToString(hdnInboxType.Value).Trim() == "RCOS")
                    {
                        get_COSApprovedAmount();
                    }
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    protected void btnfuel_Details_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter submission date";
            return;
        }
        AssigningSessions();
        //Response.Redirect("~/procs/FuelClaim.aspx");
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {

    }

    protected void fuel_btnSave_Click(object sender, EventArgs e)
    {

        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }

        if (Convert.ToString(hdnInboxType.Value).Trim() == "RACC")
        {
            if (Convert.ToString(txtComment.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Comments/Remarks.";
                return;
            }
        }
        string strapprovermails = "";

        //FeulConditionTType Value
        if (Convert.ToString(hdnInboxType.Value).Trim() == "RCOS")
        {
            for (Int32 irow = 0; irow < dgFuelClaim.Rows.Count; irow++)
            {
                if (Convert.ToString(dgFuelClaim.Rows[irow].Cells[2].Text).Trim() == "" || Convert.ToString(dgFuelClaim.Rows[irow].Cells[2].Text).Trim() == "&nbsp;")
                {
                    lblmessage.Text = "Please enter COS Approved Amount.";
                    if (Request.QueryString.Count > 1)
                    {
                        getFueClaimDetails();
                    }
                    return;
                }
            }
            if (Convert.ToString(txtCosApprovedAmt.Text) != "")
            {
                if (Convert.ToDecimal(txtAmount.Text) < Convert.ToDecimal(txtCosApprovedAmt.Text))
                {
                    txtCosApprovedAmt.Text = txtAmount.Text;
                    lblmessage.Text = "Amount cannot be more than Claimed Amount";
                    return;
                }
            }
        }

        strapprovermails = getRejectionCorrectionmailList();

         String strnextappr = "";

        if (Convert.ToString(hdnTravelConditionid.Value).Trim() == "4")
        {
            checkisCOSor_ACC_ClaimApproved();
            strnextappr = "RACC";
        }

        if (Convert.ToString(hdnTravelConditionid.Value).Trim() == "5")// || Convert.ToString(hdnTravelConditionid.Value).Trim() == "4")
        {
            //////get_HOD_ACC_CFO_details_ForNextApprover("RCOS");
            //////strnextappr = "RCOS";
            get_HOD_ACC_CFO_details_ForNextApprover("HOD");
            strnextappr = "APP";

            if (Convert.ToString(hdnApprovalACCHOD_Code.Value).Trim() == "")
            {
                if (Convert.ToString(hdnTravelConditionid.Value).Trim() == "5")// || Convert.ToString(hdnTravelConditionid.Value).Trim() == "4")
                {
                    get_HOD_ACC_CFO_details_ForNextApprover("RACC");
                    strnextappr = "RACC";
                }
            }
        }
        if (Convert.ToString(hdnInboxType.Value).Trim() == "RACC")
        {
            hdnApprovalACCHOD_Name.Value = "Account";
        }
        if (Convert.ToString(hdnInboxType.Value).Trim() == "RCOS")
        {
            hdnApprovalACCHOD_Name.Value = "COS";
            fuel_btnCorrection.Visible = true;
        }
        if (Convert.ToString(hdnInboxType.Value).Trim() == "RCFO")
        {
            hdnApprovalACCHOD_Name.Value = "CFO";
        }

        if (Convert.ToString(hdnInboxType.Value).Trim() == "APP")
        {
            hdnApprovalACCHOD_Name.Value = hflempName.Value;
            fuel_btnCorrection.Visible = true;
        }


        String strmobeRemURL = "";
        strmobeRemURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_FuelRem"]).Trim() + "?rem_id=" + hdnRemid.Value + "&mngexp=0&inbtype=" + strnextappr;

        getFromdateTodate_FroEmail();
        string strcliamMonth = Convert.ToString(hdnfrmdate_emial.Value).Trim();

        if (Convert.ToString((hdnstaus.Value).Trim()) == "")
        {
            spm.UpdateFuelAppRequest(Convert.ToInt32(hdnRemid.Value), "Approved", Convert.ToString(txtComment.Text).Trim(), Convert.ToString((hdnstaus.Value).Trim()), Convert.ToInt32(hdnCurrentApprID.Value), Convert.ToDecimal(txtCosApprovedAmt.Text), Convert.ToString(hdnInboxType.Value));
            spm.InsertFuelApproverDetails(hdnApprovalACCHOD_Code.Value, Convert.ToInt32(hdnApprovalACCHOD_ID.Value), Convert.ToInt32(hdnRemid.Value), "");
            spm.Fuel_send_mailto_Next_ApproverNew(hdnReqEmailaddress.Value, hdnApprovalACCHOD_mail.Value, "Request for Fuel bill Reimbursement", txtQuantity.Text, txtAmount.Text, GetApprove_RejectList(), txtEmpName.Text, strmobeRemURL, strcliamMonth);
            spm.Fuel_send_mailto_Next_Approver_IntermediateNew(hdnReqEmailaddress.Value, strapprovermails, "Request for Fuel bill Reimbursement", txtQuantity.Text, txtAmount.Text, GetApprove_RejectList(), txtEmpName.Text, hdnApprovalACCHOD_Name.Value, strcliamMonth);
            lblmessage.Text = "Fuel Reimbursement Resquest has been approved and and send for next level approvals";
            //  Response.Redirect("~/procs/InboxTravelRequest.aspx");
        }
        else
        {
            spm.UpdateFuelAppRequest(Convert.ToInt32(hdnRemid.Value), "Approved", Convert.ToString(txtComment.Text).Trim(), Convert.ToString((hdnstaus.Value).Trim()), Convert.ToInt32(hdnCurrentApprID.Value), Convert.ToDecimal(txtCosApprovedAmt.Text), Convert.ToString(hdnInboxType.Value));
            spm.Fuel_send_mailto_Next_Approver_IntermediateNew(hdnReqEmailaddress.Value, strapprovermails, "Request for Fuel bill Reimbursement", txtQuantity.Text, txtAmount.Text, GetApprove_RejectList(), txtEmpName.Text, "Account", strcliamMonth);
            lblmessage.Text = "Fuel Reimbursement Resquest has been approved and notofication has been send to the Requester and Previous Intermediate,Approver Levels";
        }

        Response.Redirect("~/procs/InboxFuel.aspx?inbtype=" + hdnInboxType.Value);


    }

    protected void fuel_btnReject_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }

        if (Convert.ToString(txtComment.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Rejection Remarks";
            return;
        }

        getFromdateTodate_FroEmail();
        string strcliamMonth = Convert.ToString(hdnfrmdate_emial.Value).Trim();
        // GetEmployeeDetails_loginemployee();
        hdnloginemployee_name.Value = Convert.ToString(hflempName.Value); // txtEmpName.Text;
        if (Convert.ToString(hdnInboxType.Value).Trim() == "RACC")
        {
            hdnloginemployee_name.Value = "Account";
        }
        if (Convert.ToString(hdnInboxType.Value).Trim() == "RCOS")
        {
            hdnloginemployee_name.Value = "COS";
        }
        if (Convert.ToString(hdnInboxType.Value).Trim() == "RCFO")
        {
            hdnloginemployee_name.Value = "CFO";
        }


        string strapprovermails = "";
        strapprovermails = getRejectionCorrectionmailList();
        spm.RrejectFuelrequest(Convert.ToInt32(hdnRemid.Value), hdnempcode.Value, Convert.ToInt32(hdnCurrentApprID.Value), txtComment.Text, "rejectFuelReimbursementrequest");
        spm.Fuel_send_mail_Rejection_CorrectionNew(hdnReqEmailaddress.Value, hdnloginemployee_name.Value, "Rejection of Fuel bill Reimbursement Request", txtQuantity.Text, txtAmount.Text, strapprovermails, Convert.ToString(GetApprove_RejectList()).Trim(), txtComment.Text, txtEmpName.Text, strcliamMonth);
        Response.Redirect("~/procs/InboxFuel.aspx?inbtype=" + hdnInboxType.Value);
    }

    protected void lnkuplodedfile_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["FuelClaimsDocumentpath"]).Trim()), lnkuplodedfile.Text);

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

    #endregion

    #region PageMethods
    private void getPayementVoucher_forPrint()
    {
        try
        {
           

            #region get payment Voucher details
            DataSet dspaymentVoucher = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "fuel_claim";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDecimal(hdnRemid.Value);

            dspaymentVoucher = spm.getDatasetList(spars, "rpt_dataProcedure");
            HDTotaltillAmount.Value = dspaymentVoucher.Tables[6].Rows[0]["Amountt"].ToString();

            Fill_Summary_Grid();

            #endregion

            if (dspaymentVoucher.Tables[0].Rows.Count > 0)
            {
                ReportViewer ReportViewer1 = new ReportViewer();


                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.LocalReport.DataSources.Clear();

                ReportParameter[] param = new ReportParameter[17];
                param[0] = new ReportParameter("pdocno", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Vouno"]));
                param[1] = new ReportParameter("ppvdate", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Rem_Sub_Date"]));
                param[2] = new ReportParameter("pempName", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Emp_Name"]));
                param[3] = new ReportParameter("pempCode", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Empcode"]));
                param[4] = new ReportParameter("pRsinwords", Convert.ToString(dspaymentVoucher.Tables[2].Rows[0]["AmountinWords"]));

                param[5] = new ReportParameter("pFuelClaimheads", Convert.ToString("Fuel Claim Details"));

                if (dspaymentVoucher.Tables[4].Rows.Count > 0)
                    param[6] = new ReportParameter("pFuelOutClaimheads", Convert.ToString("Outsation Details"));
                else
                    param[6] = new ReportParameter("pFuelOutClaimheads", "");


                #region Cost Cente & Bank Details
                if (dspaymentVoucher.Tables[5].Rows.Count > 0)
                {
                    param[7] = new ReportParameter("pCostCenterCode", Convert.ToString(dspaymentVoucher.Tables[5].Rows[0]["Cost_Center"]));
                    param[8] = new ReportParameter("pCostCenterNM", Convert.ToString(dspaymentVoucher.Tables[5].Rows[0]["Cost_Center_desc"]));

                    param[9] = new ReportParameter("pBankName", Convert.ToString(dspaymentVoucher.Tables[5].Rows[0]["BANK_name"]));
                    param[10] = new ReportParameter("pBankAccCode", Convert.ToString(dspaymentVoucher.Tables[5].Rows[0]["BANK_acc"]));
                    param[11] = new ReportParameter("pBankIFSCcode", Convert.ToString(dspaymentVoucher.Tables[5].Rows[0]["BANK_ifsc"]));
                    param[12] = new ReportParameter("pBankBranch", Convert.ToString(dspaymentVoucher.Tables[5].Rows[0]["BANK_Branch"]));

                    param[13] = new ReportParameter("pRbnkName", Convert.ToString(dspaymentVoucher.Tables[5].Rows[0]["R_BANK_name"]));
                    param[14] = new ReportParameter("pRBnkAccCode", Convert.ToString(dspaymentVoucher.Tables[5].Rows[0]["R_BANK_acc"]));
                    param[15] = new ReportParameter("pRbnkIFSCcode", Convert.ToString(dspaymentVoucher.Tables[5].Rows[0]["R_BANK_ifsc"]));
                    param[16] = new ReportParameter("pRbnkBranch", Convert.ToString(dspaymentVoucher.Tables[5].Rows[0]["R_BANK_Branch"]));
                }
                else
                {
                    param[7] = new ReportParameter("pCostCenterCode", Convert.ToString(""));
                    param[8] = new ReportParameter("pCostCenterNM", Convert.ToString(""));

                    param[9] = new ReportParameter("pBankName", Convert.ToString(""));
                    param[10] = new ReportParameter("pBankAccCode", Convert.ToString(""));
                    param[11] = new ReportParameter("pBankIFSCcode", Convert.ToString(""));
                    param[12] = new ReportParameter("pBankBranch", Convert.ToString(""));

                    param[13] = new ReportParameter("pRbnkName", Convert.ToString(""));
                    param[14] = new ReportParameter("pRBnkAccCode", Convert.ToString(""));
                    param[15] = new ReportParameter("pRbnkIFSCcode", Convert.ToString(""));
                    param[16] = new ReportParameter("pRbnkBranch", Convert.ToString(""));
                }
                #endregion


                // Create Report DataSource
                ReportDataSource rds = new ReportDataSource("dspaymentVoucher", dspaymentVoucher.Tables[0]);
                ReportDataSource rdsApprs = new ReportDataSource("dspaymentVoucher_Apprs", dspaymentVoucher_Apprs.Tables[0]);
                ReportDataSource rdsFuelClaims = new ReportDataSource("dsfuelClaimdtls", dspaymentVoucher.Tables[3]);
                ReportDataSource rdsFuelClaimsOutstn = new ReportDataSource("dsfuelclaimOutstaiondtls", dspaymentVoucher.Tables[4]);
                ReportDataSource rdsFuelSumm = new ReportDataSource("ds_Fuel_Summ", Summ);


                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/PaymentVoucher_FuelNew.rdlc");

                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.DataSources.Add(rdsApprs);

                ReportViewer1.LocalReport.DataSources.Add(rdsFuelClaimsOutstn);
                ReportViewer1.LocalReport.DataSources.Add(rdsFuelClaims);
                ReportViewer1.LocalReport.DataSources.Add(rdsFuelSumm);

                ReportViewer1.LocalReport.SetParameters(param);
                ReportViewer1.LocalReport.Refresh();

                #region Create payment Voucher PDF file
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                DataTable DataTable1 = new DataTable();
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=paymentVoucherDetails." + extension);
                try
                {
                    Response.BinaryWrite(bytes);
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error while generating PDF.');", true);
                    Console.WriteLine(ex.StackTrace);
                }

                #endregion


            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            Response.End();
        }
    }

    private void checkApprovalStatus_Submit()
    {
        try
        {

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "check_FuelReqAppr_Status";
            spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDecimal(hdnRemid.Value);
            spars[2] = new SqlParameter("@apprempcode", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(hdnempcode.Value);

            DataTable dtTrDetails = spm.getMobileRemDataList(spars, "[SP_GETALLreembursement_DETAILS]");

            if (dtTrDetails.Rows.Count == 0)
            {
                Response.Redirect("~/procs/InboxFuel.aspx?app=" + hdnInboxType.Value);
            }

            if (dtTrDetails.Rows.Count > 0)
            {
                if (Convert.ToString(dtTrDetails.Rows[0]["pvappstatus"]) != "Pending")
                {
                    Response.Redirect("~/procs/InboxFuel.aspx?app=" + hdnInboxType.Value);
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }


    }
    private void checkisCOSor_ACC_ClaimApproved()
    {


        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "check_COS_ACC_HOD_isApproved_claim";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[1].Value = hdnRemid.Value;
            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                for (Int32 irow = 0; irow < dsTrDetails.Tables[0].Rows.Count; irow++)
                {
                    if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Action"]).Trim() == "Approved" && Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_type"]).Trim() == Convert.ToString(hdnInboxType.Value).Trim())
                    {
                        hdnCurrentApprID.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Appr_id"]).Trim();
                        get_HOD_ACC_CFO_details_ForNextApprover("RACC");

                    }
                    else if (Convert.ToString(hdnTravelConditionid.Value).Trim() == "4" && Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Action"]).Trim() == "Pending" && Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_type"]).Trim() == Convert.ToString(hdnInboxType.Value).Trim())
                    {
                        hdnCurrentApprID.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Appr_id"]).Trim();
                        get_HOD_ACC_CFO_details_ForNextApprover("RACC");

                    }

                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }


    }

    private void get_HOD_ACC_CFO_details_ForNextApprover(string strstype)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[5];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            if (Convert.ToString(strstype) == "HOD" || Convert.ToString(strstype) == "RACC")
                spars[0].Value = "get_next_Approver_dtls_FuleClaim";
            else if (Convert.ToString(strstype) == "RCOS")
                spars[0].Value = "get_next_Approver_dtls_FuleClaim";
            else
                spars[0].Value = "get_ACC_HOD_isApproved_claim";

            spars[1] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[1].Value = strstype;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            spars[3] = new SqlParameter("@rem_id", SqlDbType.VarChar);
            spars[3].Value = hdnRemid.Value;

            spars[4] = new SqlParameter("@conditiontypeid", SqlDbType.Int);
            spars[4].Value = hdnTravelConditionid.Value;


            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                hdnstaus.Value = "";
                hdnApprovalACCHOD_mail.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();
                hdnApprovalACCHOD_Code.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_code"]).Trim();
                hdnApprovalACCHOD_ID.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_id"]).Trim();
                hdnApprovalACCHOD_Name.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_remarks"]).Trim();

            }
            if (dsTrDetails.Tables[1].Rows.Count > 0)
            {


                if (dsTrDetails.Tables[0].Rows.Count == 0)
                {
                    hdnApprovalACCHOD_mail.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["approver_emp_mail"]).Trim();
                    hdnApprovalACCHOD_Code.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["approver_emp_code"]).Trim();
                    hdnApprovalACCHOD_ID.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["app_id"]).Trim();
                    hdnApprovalACCHOD_Name.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["app_remarks"]).Trim();
                    hdnstaus.Value = "";
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    public void getApproverdata()
    {

        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.GeTfuelApproverEmailID_OrgFuel(Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(hflGrade.Value).Trim(), Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToDecimal(hdnRemid.Value));
        dspaymentVoucher_Apprs.Tables.Add(dtApproverEmailIds);
        //IsEnabledFalse (true);
        lstApprover.Items.Clear();
        if (dtApproverEmailIds.Rows.Count > 0)
        {
            hdnApprEmailaddress.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["Emp_Emailaddress"]);
            hdnApprId.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["APPR_ID"]);

            //lstApprover.DataSource = dtApproverEmailIds;
            //lstApprover.DataTextField = "names";
            //lstApprover.DataValueField = "APPR_ID";
            //lstApprover.DataBind();
            DgvApprover.DataSource = null;
            DgvApprover.DataBind();

            if (dtApproverEmailIds.Rows.Count > 0)
            {
                DgvApprover.DataSource = dtApproverEmailIds;
                DgvApprover.DataBind();
            }
            hflapprcode.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["approver_emp_code"]);
        }
        else
        {
            lblmessage.Text = "Since you are not assigned under any approver you can not apply for any type of Travel request, please contact HR";

        }
    }

    public void GetEmployeeDetails()
    {
        try
        {
            DataTable dtEmpDetails = new DataTable();
            dtEmpDetails = spm.GetEmployeeData(hdnempcode.Value);
            if (dtEmpDetails.Rows.Count > 0)
            {
                // txtEmpName.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
                hflempName.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
                hflEmpDesignation.Value = Convert.ToString(dtEmpDetails.Rows[0]["DesginationName"]);
                hflEmpDepartment.Value = Convert.ToString(dtEmpDetails.Rows[0]["Department_Name"]);
                hflGrade.Value = Convert.ToString(dtEmpDetails.Rows[0]["Grade"]).Trim();
                hflEmailAddress.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
            }
            //  getApproverdata();  
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();
            throw;
        }
    }
    public void AssigningSessions()
    {

        Session["Fromdate"] = txtFromdate.Text;
        Session["ReqEmpCode"] = txtEmpCode.Text;
        //Session["Grade"] = hflGrade.Value;

        //Session["TrDays"] = hdnTrdays.Value;


    }

    public void getFueClaimDetails()
    {
        DataSet dtMobileDetails = new DataSet();
        dtMobileDetails = spm.GetFuelClaimDetailsById_approverNew(Convert.ToInt32(hdnRemid.Value), Convert.ToString(hdnInboxType.Value).Trim(), Convert.ToString(hdnempcode.Value).Trim());

        dgFuelClaim.DataSource = null;
        dgFuelClaim.DataBind();

        if (dtMobileDetails.Tables[0].Rows.Count > 0)
        {
            hdnClaimsID.Value = Convert.ToString(dtMobileDetails.Tables[0].Rows[0]["FuelClaims_id"]);
            dgFuelClaim.DataSource = dtMobileDetails.Tables[0];
            dgFuelClaim.DataBind();
            FuelBills_Heading.Visible = true;

            decimal totalAmount = 0;
            foreach (DataRow row in dtMobileDetails.Tables[0].Rows)
            {
                totalAmount += Convert.ToDecimal(row["Amount"]);
            }
            // txt_TotalClaimAmount.Text = Convert.ToString(totalAmount);
           

            //Txt_BalanceAmountCurrentMonth.Text = dtMobileDetails.Tables[1].Rows[0]["BalnceAmountCurrent"].ToString();
            //Txt_BalanceAmountPreviousMonth.Text = dtMobileDetails.Tables[1].Rows[0]["BalnceAmountPrevious"].ToString();
            decimal Amount;
            Amount = Convert.ToDecimal(Txt_BalanceAmountCurrentMonth.Text) + Convert.ToDecimal(Txt_BalanceAmountPreviousMonth.Text);
            Txt_BlanceAmount.Text = Convert.ToString(Amount);
        }
        else
        {
            FuelBills_Heading.Visible = false;
        }

        
    }

    private void InsertMobileRem_DatatoTempTables_trvl()
    {
        try
        {

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_FuelRem_insert_mainData_toTempTablsNew";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDecimal(hdnRemid.Value);

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            DataTable dtMaxTripID = spm.InsertorUpdateData(spars, "[SP_GETALLreembursement_DETAILS]");

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }


    }

    public void getMobRemlsDetails_usingRemid()
    {
        try
        {
            DataTable dtTrDetails = new DataTable();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_getMainTravelRequest_forFuelNew";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Int);
            spars[1].Value = hdnRemid.Value;

            dtTrDetails = spm.getMobileRemDataList(spars, "[SP_GETALLreembursement_DETAILS]");
            if (dtTrDetails.Rows.Count > 0)
            {
                txtEmpCode.Text = Convert.ToString(dtTrDetails.Rows[0]["Empcode"]);
                txtEmpName.Text = Convert.ToString(dtTrDetails.Rows[0]["Emp_Name"]);
                hdnReqEmailaddress.Value = Convert.ToString(dtTrDetails.Rows[0]["Emp_EmailAddress"]);
                txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["created_on"]);
                Txt_ClaimMonth.Text = Convert.ToString(dtTrDetails.Rows[0]["mnth"]);
                txtFuelType.Text = Convert.ToString(dtTrDetails.Rows[0]["fuleType"]);
                HDEligibility.Value = Convert.ToString(dtTrDetails.Rows[0]["Eligibility"]);

                Txt_BalanceAmountCurrentMonth.Text = Convert.ToString(dtTrDetails.Rows[0]["BalnceAmountCurrent"]).Trim();
                Txt_BalanceAmountPreviousMonth.Text = Convert.ToString(dtTrDetails.Rows[0]["BalnceAmountPrevious"]).Trim();

                txtAmount.Text = Convert.ToString(dtTrDetails.Rows[0]["TotalAmount_Claimed"]);
                txt_TotalClaimAmount.Text = Convert.ToString(dtTrDetails.Rows[0]["TotalAmount_Claimed"]);


                lnkuplodedfile.Text = Convert.ToString(dtTrDetails.Rows[0]["UploadFile"]).Trim();
                hdnTravelConditionid.Value = Convert.ToString(dtTrDetails.Rows[0]["Fuel_Conditionid"]);
                hflGrade.Value = Convert.ToString(dtTrDetails.Rows[0]["grade"]).Trim();
                txtTollCharges.Text = Convert.ToString(dtTrDetails.Rows[0]["toll_Charges"]).Trim();
                txt_airport_parking.Text = Convert.ToString(dtTrDetails.Rows[0]["airport_parking"]).Trim();
                txt_parkwash_claimed.Text = Convert.ToString(dtTrDetails.Rows[0]["parkwash_claimed"]).Trim();
                txt_parkwash_elg.Text = Convert.ToString(dtTrDetails.Rows[0]["parkwash_elg"]).Trim();
                txtCosApprovedAmt.Text = Convert.ToString(dtTrDetails.Rows[0]["Cos_Amount"]).Trim();
                Txt_WashingAllowance.Text = Convert.ToString(dtTrDetails.Rows[0]["parkwash_claimed"]).Trim();
                //Decimal dBalance = 0;
                //dBalance = Convert.ToDecimal(txtBalElig.Text); //- dActualTotalFuelConsump;
                lblheading.Text = "Fuel Bill - " + Convert.ToString(dtTrDetails.Rows[0]["Vouno"]);

                //if (Convert.ToString(txtEligible.Text).Trim() != "")
                //    txtTotalClaimTillDt.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txtEligible.Text) - (dBalance), 2)).Trim();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    }

    protected void GetCuurentApprID()
    {

        string Actions = "";
        DataTable dtCApprID = new DataTable();
        dtCApprID = spm.GetCurrentApprIDFuel(Convert.ToInt32(hdnRemid.Value), hdnempcode.Value);
        hdnCurrentApprID.Value = Convert.ToString(dtCApprID.Rows[0]["APPR_ID"]);
        Actions = Convert.ToString(dtCApprID.Rows[0]["Action"]);

        if (Convert.ToString(hdnCurrentApprID.Value).Trim() == "")
        {
            lblmessage.Text = "Acton on this REquest not yet taken by other approvals";
            return;
        }
        else if (Convert.ToString(Actions).Trim() != "Pending")
        {
            lblmessage.Text = "You already actioned for this request";
            return;
        }
    }

    private void getApproverlist()
    {
        DataTable dtapprover = new DataTable();
        dtapprover = spm.GetFuelApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
        lstApprover.Items.Clear();
        if (dtapprover.Rows.Count > 0)
        {
            lstApprover.DataSource = dtapprover;
            lstApprover.DataTextField = "names";
            lstApprover.DataValueField = "names";
            lstApprover.DataBind();

        }
        else
        {
            lblmessage.Text = "There is no request for approver.";
        }
    }

    public void getnextAppIntermediate()
    {
        DataTable dsapproverNxt = new DataTable();
        dsapproverNxt = spm.GetFuelNextApproverDetails(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
        if (dsapproverNxt.Rows.Count > 0)
        {
            hdnNextApprId.Value = Convert.ToString(dsapproverNxt.Rows[0]["APPR_ID"]);
            hdnNextApprCode.Value = Convert.ToString(dsapproverNxt.Rows[0]["A_EMP_CODE"]);
            hdnNextApprName.Value = Convert.ToString(dsapproverNxt.Rows[0]["Emp_Name"]);
            hdnNextApprEmail.Value = Convert.ToString(dsapproverNxt.Rows[0]["Emp_Emailaddress"]);
        }
        else
        {
            hdnstaus.Value = "Final Approver";

            //For  Previous approver   
            getPreviousApprovesEmailList();
        }

    }
    
    protected string GetApprove_RejectList()
    {
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        //dtAppRej = spm.GetFuelApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnTravelConditionid.Value));

        dtAppRej = spm.GeTfuelApproverEmailID_OrgFuel(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToDecimal(hdnRemid.Value));
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
    private void getPreviousApprovesEmailList()
    {
        DataTable dtPreApp = new DataTable();
        dtPreApp = spm.FuelPreviousApproverDetails(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value));
        if (dtPreApp.Rows.Count > 0)
        {

            for (int i = 0; i < dtPreApp.Rows.Count; i++)
            {
                if (Convert.ToString(hflEmailAddress.Value).Trim() == "")
                {
                    hflEmailAddress.Value = Convert.ToString(dtPreApp.Rows[i]["Emp_Emailaddress"]).Trim();
                }
                else
                {
                    hflEmailAddress.Value += ";" + Convert.ToString(dtPreApp.Rows[i]["Emp_Emailaddress"]).Trim();
                }
            }
        }
    }
    protected string getRejectionCorrectionmailList()
    {
        string email_ids = "";
        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.FuelApproverDetails_Rejection_cancellation(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), "get_ApproverDetails_mail_rejection_correction");
        lstApprover.Items.Clear();
        if (dtApproverEmailIds.Rows.Count > 0)
        {
            for (int irow = 0; irow < dtApproverEmailIds.Rows.Count; irow++)
            {
                if (Convert.ToString(email_ids).Trim() == "")
                    email_ids = Convert.ToString(dtApproverEmailIds.Rows[irow]["Emp_Emailaddress"]).Trim();
                else
                    email_ids = email_ids + ";" + Convert.ToString(dtApproverEmailIds.Rows[irow]["Emp_Emailaddress"]).Trim();
            }
        }

        return email_ids;

    }

    public void get_employee_FuelUploaded_Files()
    {
        try
        {


            DataSet dsfuelFiles = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_fuelclaimFilesNew";

            spars[1] = new SqlParameter("@filetype", SqlDbType.VarChar);
            spars[1].Value = "FuelClaim";

            spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[2].Value = hdnRemid.Value;
            dsfuelFiles = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            gvfuel_claimsFiles.DataSource = null;
            gvfuel_claimsFiles.DataBind();
            if (dsfuelFiles.Tables[0].Rows.Count > 0)
            {
                gvfuel_claimsFiles.DataSource = dsfuelFiles.Tables[0];
                gvfuel_claimsFiles.DataBind();
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }

    }

    private void get_COSApprovedAmount()
    {
        try
        {
            txtCosApprovedAmt.Text = "";
            Decimal dCOsApprAmnt = 0;
            for (Int32 i = 0; i < dgFuelClaim.Rows.Count; i++)
            {
                if (Convert.ToString(dgFuelClaim.Rows[i].Cells[2].Text).Trim() != "")
                {
                    dCOsApprAmnt = dCOsApprAmnt + Convert.ToDecimal(dgFuelClaim.Rows[i].Cells[2].Text);
                }
            }
           

            if (Convert.ToString(txt_airport_parking.Text).Trim() != "")
            {
                dCOsApprAmnt = dCOsApprAmnt + Convert.ToDecimal(txt_airport_parking.Text);
            }
            if (Convert.ToString(txtTollCharges.Text).Trim() != "")
            {
                dCOsApprAmnt = dCOsApprAmnt + Convert.ToDecimal(txtTollCharges.Text);
            }

            if (Convert.ToString(txt_parkwash_claimed.Text).Trim() != "")
            {
                dCOsApprAmnt = dCOsApprAmnt + Convert.ToDecimal(txt_parkwash_claimed.Text);
            }

            decimal parsed = decimal.Parse(Convert.ToString(dCOsApprAmnt).Trim(), CultureInfo.InvariantCulture);
            CultureInfo hindi = new CultureInfo("hi-IN");
            txtCosApprovedAmt.Text = string.Format(hindi, "{0:#,#0.00}", parsed);
            txt_TotalClaimAmount.Text = txtCosApprovedAmt.Text;
        }
        catch (Exception ex)
        { }
    }
    #endregion

    private void getFromdateTodate_FroEmail()
    {
        try
        {

            string[] strdate;
            string strfromDate = "";


            if (dgFuelClaim.Rows.Count > 0)
            {
                strfromDate = Convert.ToString(dgFuelClaim.Rows[0].Cells[0].Text).Trim();

                if (Convert.ToString(strfromDate).Trim() != "")
                {
                    strdate = Convert.ToString(strfromDate).Trim().Split('/');
                    strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                }
            }
            else
            {
                if (Convert.ToString(Txt_ClaimMonth.Text).Trim() != "")
                {
                    DateTime dt = DateTime.ParseExact(Txt_ClaimMonth.Text, "MMM/yyyy", CultureInfo.InvariantCulture);
                    strfromDate = dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
            }
            #region date formatting



            #endregion


            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_fuelclaimMonth";

            spars[1] = new SqlParameter("@formatdate", SqlDbType.VarChar);
            spars[1].Value = strfromDate;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                hdnfrmdate_emial.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["fuelclaimMonth"]).Trim();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    protected void fuel_btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/InboxFuel.aspx?inbtype=" + hdnInboxType.Value);
    }

    protected void lnkViewFiles_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        //hdnRemid.Value = Convert.ToString(dgFuelClaim.DataKeys[row.RowIndex].Values[0]).Trim();
        string strfilename = Convert.ToString(hdnRemid.Value) + "/" + Convert.ToString(gvfuel_claimsFiles.Rows[row.RowIndex].Cells[0].Text).Trim();

        String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["FuelClaimsDocumentpath"]).Trim()), strfilename);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
        Response.WriteFile(strfilepath);
        Response.End();

    }
    protected void mobile_btnPrintPV_Click(object sender, EventArgs e)
    {
        getApproverdata();
        getPayementVoucher_forPrint();
    }
    protected void fuel_btnCorrection_Click(object sender, EventArgs e)
    {

        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }

        if (Convert.ToString(txtComment.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Correction Remarks";
            return;
        }

        getFromdateTodate_FroEmail();
        string strcliamMonth = Convert.ToString(hdnfrmdate_emial.Value).Trim();
        // GetEmployeeDetails_loginemployee();
        hdnloginemployee_name.Value = Convert.ToString(hflempName.Value); // txtEmpName.Text;
        if (Convert.ToString(hdnInboxType.Value).Trim() == "RACC")
        {
            hdnloginemployee_name.Value = "Account";
        }
        if (Convert.ToString(hdnInboxType.Value).Trim() == "RCOS")
        {
            hdnloginemployee_name.Value = "COS";
        }
        if (Convert.ToString(hdnInboxType.Value).Trim() == "RCFO")
        {
            hdnloginemployee_name.Value = "CFO";
        }


        string strapprovermails = "";
        strapprovermails = getRejectionCorrectionmailList();
        spm.RrejectFuelrequest(Convert.ToInt32(hdnRemid.Value), hdnempcode.Value, Convert.ToInt32(hdnCurrentApprID.Value), txtComment.Text, "CorrectionFuelReimbursementrequest");
        spm.Fuel_send_mail_Rejection_Correction(hdnReqEmailaddress.Value, hdnloginemployee_name.Value, "Request For Fuel Reimbursement Correction - " + strcliamMonth, txtQuantity.Text, txtAmount.Text, strapprovermails, Convert.ToString(GetApprove_RejectList()).Trim(), txtComment.Text, txtEmpName.Text, strcliamMonth);
        Response.Redirect("~/procs/InboxFuel.aspx?inbtype=" + hdnInboxType.Value);

    }

    private void Fill_Summary_Grid()
    {
        Decimal dTotalClaimAmt_out = 0;
        Decimal douttrvltot = 0;
        Decimal dtot_Outquantity = 0;

        Decimal dtotclaimAmt = 0, dtotquantity = 0;

        for (Int32 irow = 0; irow < dgFuelClaim.Rows.Count; irow++)
        {
            if (Convert.ToString(dgFuelClaim.Rows[irow].Cells[1].Text).Trim() != "")
                dtotquantity += Convert.ToDecimal(dgFuelClaim.Rows[irow].Cells[1].Text);

            if (Convert.ToString(dgFuelClaim.Rows[irow].Cells[2].Text).Trim() != "")
                dtotclaimAmt += Convert.ToDecimal(dgFuelClaim.Rows[irow].Cells[2].Text);


        }

        DataTable dt = new DataTable();
        Summ = new DataTable();
        if (dt.Columns.Count == 0)
        {
            dt.Columns.Add("COL1", typeof(string));
            dt.Columns.Add("COL2", typeof(string));
            dt.Columns.Add("COL3", typeof(string));
            dt.Columns.Add("COL4", typeof(string));
        }

        string amtdtotclaimAmt, amtYearEligibility, amtBalanceEligibility, amtdTotalClaimAmt_out = "";
        CultureInfo hindi = new CultureInfo("hi-IN");

        decimal parsed = decimal.Parse(Convert.ToString(dtotclaimAmt), CultureInfo.InvariantCulture);
        amtdtotclaimAmt = string.Format(hindi, "{0:#,#0.00}", parsed);

        decimal parsed2 = decimal.Parse(Convert.ToString(dTotalClaimAmt_out), CultureInfo.InvariantCulture);
        amtdTotalClaimAmt_out = string.Format(hindi, "{0:#,#0.00}", parsed2);

        decimal parsed3 = decimal.Parse(Convert.ToString(HDEligibility.Value), CultureInfo.InvariantCulture);
        amtYearEligibility = string.Format(hindi, "{0:#,#0.00}", parsed3);

        DataRow NewRow = dt.NewRow();
        NewRow[0] = "Parking / Washing Allowance: ";
        NewRow[1] = Convert.ToString(Convert.ToDecimal(txt_parkwash_elg.Text));
        NewRow[2] = "Yearly Eligibility: ";
        NewRow[3] = HDEligibility.Value;
        dt.Rows.Add(NewRow);

        NewRow = dt.NewRow();
        NewRow[0] = "Fuel Charges: ";
        NewRow[1] = Convert.ToString(amtdtotclaimAmt);
        NewRow[2] = "";
        NewRow[3] = "";
        dt.Rows.Add(NewRow);

        NewRow = dt.NewRow();
        NewRow[0] = "Toll Pass: ";
        NewRow[1] = txtTollCharges.Text;
        NewRow[2] = "";
        NewRow[3] = "";
        dt.Rows.Add(NewRow);

        NewRow = dt.NewRow();
        NewRow[0] = "Airport Parking: ";
        NewRow[1] = txt_airport_parking.Text;
        NewRow[2] = "Amount Claimed till date: ";
        NewRow[3] = HDTotaltillAmount.Value;
        dt.Rows.Add(NewRow);

        if (HDEligibility.Value == "")
        {
            HDEligibility.Value = "0.00";
        }
        if (HDTotaltillAmount.Value == "")
        {
            HDTotaltillAmount.Value = "0.00";
        }

        decimal dcramteligilibity = Convert.ToDecimal(HDEligibility.Value) - Convert.ToDecimal(HDTotaltillAmount.Value);
        decimal parsed4 = decimal.Parse(Convert.ToString(dcramteligilibity), CultureInfo.InvariantCulture);
        amtBalanceEligibility = string.Format(hindi, "{0:#,#0.00}", parsed4);

        decimal parsed5 = decimal.Parse(Convert.ToString(txtAmount.Text), CultureInfo.InvariantCulture);
        string amtTotalClaimAmount = string.Format(hindi, "{0:#,#0.00}", parsed5);

        NewRow = dt.NewRow();
        NewRow[0] = "Amount Claimed for the Month: ";
        NewRow[1] = amtTotalClaimAmount;
        NewRow[2] = "Balance Eligibility: ";
        NewRow[3] = amtBalanceEligibility;
        dt.Rows.Add(NewRow);
        Summ = dt.Copy();
      
    }

}