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

public partial class VSCB_ApproveBatch : System.Web.UI.Page
{

    #region Creative_Default_methods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    public DataTable dtEmp;

    SP_Methods spm = new SP_Methods();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();

    DataSet dsBatchList = new DataSet();
    string strempcode = "";
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void lnkcont_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/SampleForm");
    }

    #endregion

    #region Page_Events

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }


            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");


            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/vscb_index");
            }
            else
            {

                lblmessage.Text = "";
                Page.SmartNavigation = true;
                txtEmpCode.Text = Session["Empcode"].ToString();
                hdnVendorBankDetails.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBVendorFiles"]).Trim());
                if (!Page.IsPostBack)
                {
                    txtRemarks.Attributes.Add("maxlength", txtRemarks.MaxLength.ToString());

                    editform.Visible = true;
                    PopulateEmployeeData();
                     
                    if (Request.QueryString.Count > 0)
                    {
                        hdnBatchId.Value = Convert.ToString(Request.QueryString["batchid"]).Trim(); 
                        get_BatchRequest_Approver();

                        if(Request.QueryString.Count==2)
                        {
                            hdnUrlType.Value = Convert.ToString(Request.QueryString["mngexp"]).Trim();
                        }
                        if(Convert.ToString(hdnUrlType.Value).Trim()=="2")
                        {
                            trvl_btnSave.Visible = false;
                            btnCancel.Visible = false;
                            spnremarks.Visible = false;
                            txtRemarks.Visible = false;
                        }
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
 

    #endregion

    #region Page Methods
    

    private void get_BatchRequest_Approver()
    {
        try
        {
            gvMngPaymentList_Batch.DataSource = null;
            gvMngPaymentList_Batch.DataBind();

            DgvApprover.DataSource = null;
            DgvApprover.DataBind();

            DataSet dsList = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_BatchReq_Details_Appr";

            spars[1] = new SqlParameter("@POWOID", SqlDbType.BigInt);
            spars[1].Value = Convert.ToDouble(hdnBatchId.Value);

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(txtEmpCode.Text).Trim();

            dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            if (dsList.Tables[4].Rows.Count == 0)
            {
                Response.Redirect("~/procs/vscb_index.aspx");
            }
            
            if (dsList.Tables[0].Rows.Count > 0)
            {
                txtbatchCreateDate.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_Date"]).Trim();
                txtbatchCreatedBy.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_Created_By"]).Trim();
                txtbatchNo.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_No"]).Trim();
                txtbatchNoOfRequest.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_No_Requests"]).Trim();
                txtbatchTotalPayment.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_Total_Payament"]).Trim();

                txtBank_name.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Name"]).Trim();
                txtBankRefNo.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Ref_no"]).Trim();
                txtBankRef_Link.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Ref_Link"]).Trim();
                lnkBank.InnerText = "Please click here for Bank Login";
                lnkBank.HRef = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Ref_Link"]).Trim();
                txtBankRefDate.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Ref_Date"]).Trim();
                HDDateDifferencehours.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Hours_Difference"]).Trim();

                if (Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Ref_File"]).Trim() != "")
                {
                    lnkfile_Invoice.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Ref_File"]).Trim();
                    lnkfile_Invoice.Visible = true;
                    spnInvoicefile.Visible = true;
                }

            }

            if (dsList.Tables[1].Rows.Count > 0)
            {
                gvMngPaymentList_Batch.DataSource = dsList.Tables[1];
                gvMngPaymentList_Batch.DataBind(); 
            }
            //get Checker or approver code Emp details
            if (dsList.Tables[2].Rows.Count > 0)
            {
                if (Convert.ToBoolean(dsList.Tables[2].Rows[0]["IsVerifed"]) == false)
                {
                    trvl_btnSave.Text = "Verify Batch";
                    trvl_btnSave.ToolTip = "Verify Batch";
                }

                if (Convert.ToString(dsList.Tables[2].Rows[0]["Mekar_Empcode"]).Trim()==Convert.ToString(txtEmpCode.Text).Trim())
                {
                    hdn_Checker_Appr_Id.Value = Convert.ToString(dsList.Tables[2].Rows[0]["maker_appr_id"]).Trim();
                    hdn_Checker_Appr_EmpCode.Value = Convert.ToString(dsList.Tables[2].Rows[0]["Mekar_Empcode"]).Trim();
                    hdn_Checker_Appr_EmailId.Value = Convert.ToString(dsList.Tables[2].Rows[0]["Mekar_Emp_Emailaddress"]).Trim();
                    hdn_Checker_Appr_EmpName.Value = Convert.ToString(dsList.Tables[2].Rows[0]["Mekar_Emp_Name"]).Trim();
                    hdnApprovertype.Value = "Checker";
                }

                if (Convert.ToString(dsList.Tables[2].Rows[0]["ApporvalEmpCode1"]).Trim() == Convert.ToString(txtEmpCode.Text).Trim())
                {
                    hdn_Approver1_Appr_Id.Value = Convert.ToString(dsList.Tables[2].Rows[0]["Appr_id1"]).Trim();
                    hdn_Approver1_Appr_EmpCode.Value = Convert.ToString(dsList.Tables[2].Rows[0]["ApporvalEmpCode1"]).Trim();
                    hdn_Approver1_Appr_EmailId.Value = Convert.ToString(dsList.Tables[2].Rows[0]["ApprEmp_Emailaddress1"]).Trim();
                    hdn_Approver1_Appr_EmpName.Value = Convert.ToString(dsList.Tables[2].Rows[0]["ApprEmp_Name1"]).Trim();
                    hdnApprovertype.Value = "Approver1";
                }
                if (Convert.ToString(dsList.Tables[2].Rows[0]["ApporvalEmpCode2"]).Trim() == Convert.ToString(txtEmpCode.Text).Trim())
                {
                    hdn_Approver2_Appr_Id.Value = Convert.ToString(dsList.Tables[2].Rows[0]["Appr_id2"]).Trim();
                    hdn_Approver2_Appr_EmpCode.Value = Convert.ToString(dsList.Tables[2].Rows[0]["ApporvalEmpCode2"]).Trim();
                    hdn_Approver2_Appr_EmailId.Value = Convert.ToString(dsList.Tables[2].Rows[0]["ApprEmp_Emailaddress2"]).Trim();
                    hdn_Approver2_Appr_EmpName.Value = Convert.ToString(dsList.Tables[2].Rows[0]["ApprEmp_Name2"]).Trim();
                    hdnApprovertype.Value = "Approver2";
                }
            }


            if (dsList.Tables[3].Rows.Count > 0)
            {
                DgvApprover.DataSource = dsList.Tables[3];
                DgvApprover.DataBind();
            }

            if (dsList.Tables[4].Rows.Count > 0)
            {
                if (Convert.ToString(dsList.Tables[4].Rows[0]["action"]).Trim() != "Pending")
                {
                    trvl_btnSave.Visible = false;
                    btnCancel.Visible = false;
                }
                else
                {
                     if (Request.QueryString.Count == 1)
                    {
 			//if(Convert.ToInt32(HDDateDifferencehours.Value) >= 130)
                        if(Convert.ToInt32(HDDateDifferencehours.Value) >= 94)
                        { 
                            if (Convert.ToString(txtbatchNo.Text).Trim() != "2024/12/13/639")
                            {
                                RejectCode("Auto Reject", "Auto ");
                                trvl_btnSave.Visible = false;
                                btnCancel.Visible = false;
                                string message = "Link often have a limited lifespan for security reasons so you can not take the action for this Batch. This Batch will be auto rejected.";
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append("<script type = 'text/javascript'>");
                                sb.Append("window.onload=function(){");
                                sb.Append("alert('");
                                sb.Append(message);
                                sb.Append("')};");
                                sb.Append("</script>");
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                            }
                        }
                    } 
                }
            }

            
        }
        catch (Exception ex)
        {

        }
    }
     
    public void PopulateEmployeeData()
    {
        try
        {
            dtEmp = spm.GetEmployeeData(txtEmpCode.Text);
            if (dtEmp.Rows.Count > 0)
            {
                lpm.Emp_Code = txtEmpCode.Text;
                lpm.Emp_Status = (string)dtEmp.Rows[0]["Emp_status"];
                lpm.Emp_Name = (string)dtEmp.Rows[0]["Emp_Name"];
                txtEmpName.Text= (string)dtEmp.Rows[0]["Emp_Name"]; 
                lpm.Designation_name = (string)dtEmp.Rows[0]["DesginationName"];
                lpm.department_name = (string)dtEmp.Rows[0]["Department_Name"];
                lpm.Grade = Convert.ToString(dtEmp.Rows[0]["Grade"]).Trim();
                lpm.EmailAddress = (string)dtEmp.Rows[0]["Emp_EmailAddress"];

                hflGrade.Value = lpm.Grade;
                hflEmailAddress.Value = lpm.EmailAddress;
                hflEmpName.Value = lpm.Emp_Name;
                hflEmpDepartment.Value = lpm.department_name;
                hflEmpDesignation.Value = lpm.Designation_name;

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }

 

    #endregion 

    //Comment 8
    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }
        #region Checker or Approved Batch Reques
        Boolean isFinalApprover = false;
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[4];
        string sApproverNameonMail = "";
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        if (Convert.ToString(hdnApprovertype.Value).Trim() == "Checker")
        {
            spars[0].Value = "Insert_verify_batch";
            sApproverNameonMail = Convert.ToString(hdn_Checker_Appr_EmpName.Value).Trim();
        }
        else if (Convert.ToString(hdnApprovertype.Value).Trim() == "Approver1")
        {
            spars[0].Value = "Insert_Approver1_batch";
            sApproverNameonMail = Convert.ToString(hdn_Approver1_Appr_EmpName.Value).Trim();
        }
        else
        {
            spars[0].Value = "Insert_Approver2_batch";
            sApproverNameonMail = Convert.ToString(hdn_Approver2_Appr_EmpName.Value).Trim();
        }

        spars[1] = new SqlParameter("@Batch_ID", SqlDbType.BigInt);
        spars[1].Value = Convert.ToDouble(hdnBatchId.Value);

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = Convert.ToString(txtEmpCode.Text).Trim();

        spars[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
        spars[3].Value = Convert.ToString(txtRemarks.Text).Trim();

        dsList = spm.getDatasetList(spars, "SP_VSCB_CreateBatchRequest_Details");

        #endregion



        #region Update Payment, Invoice , Milestone, POWO payment Status
        if (dsList.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToString(dsList.Tables[0].Rows[0]["approvertype"]).Trim() == "final")
            {
                DataSet dspayments = spm.get_UpdatePayment_Invoice_BatchReqApproved(Convert.ToDouble(hdnBatchId.Value), "get_Pending_BatchReqList");

                if (dspayments != null)
                {
                    if (dspayments.Tables[1].Rows.Count > 0)
                    {
                        for (Int32 irow = 0; irow < dspayments.Tables[1].Rows.Count; irow++)
                        {
                            SqlParameter[] sparsM = new SqlParameter[3];
                            sparsM[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                            sparsM[0].Value = "get_Invoice_Milestone_list";

                            sparsM[1] = new SqlParameter("@InvoiceId", SqlDbType.BigInt);
                            sparsM[1].Value = Convert.ToDouble(dspayments.Tables[1].Rows[irow]["InvoiceID"]);

                            sparsM[2] = new SqlParameter("@Batch_ID", SqlDbType.BigInt);
                            sparsM[2].Value = Convert.ToDouble(hdnBatchId.Value);

                            DataSet dsMList = spm.getDatasetList(sparsM, "SP_VSCB_PaymentStatus");

                            if (dsMList != null)
                            {
                                if (dsMList.Tables[0].Rows.Count > 0)
                                {
                                    for (Int32 jrow = 0; jrow < dsMList.Tables[0].Rows.Count; jrow++)
                                    {
                                        Decimal dMilestonePaidAmt = Convert.ToDecimal(dsMList.Tables[0].Rows[jrow]["Milesstone_Paid_Amt"]);
                                        if (dMilestonePaidAmt > 0)
                                        {
                                            #region update Milestone and PO/ WO Amount
                                            SqlParameter[] sparsMP = new SqlParameter[5];
                                            sparsMP[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                                            sparsMP[0].Value = "update_InvoiceMilestone_POWO_detils";

                                            sparsMP[1] = new SqlParameter("@MilestonePaidAmt", SqlDbType.Decimal);
                                            sparsMP[1].Value = dMilestonePaidAmt;

                                            sparsMP[2] = new SqlParameter("@InvoiceId", SqlDbType.Decimal);
                                            sparsMP[2].Value = Convert.ToDouble(dspayments.Tables[1].Rows[irow]["InvoiceID"]);

                                            sparsMP[3] = new SqlParameter("@MstoneId", SqlDbType.BigInt);
                                            sparsMP[3].Value = Convert.ToDouble(dsMList.Tables[0].Rows[jrow]["MstoneID"]);

                                            sparsMP[4] = new SqlParameter("@Batch_ID", SqlDbType.BigInt);
                                            sparsMP[4].Value = Convert.ToDouble(hdnBatchId.Value);

                                            DataSet dsMPList = spm.getDatasetList(sparsMP, "SP_VSCB_PaymentStatus");
                                            #endregion
                                        }
                                    }
                                }
                            }

                        }
                    }

                    #region Code Not Required
                  /*  for (Int32 irow = 0; irow < dspayments.Tables[4].Rows.Count; irow++)
                    {
                        SqlParameter[] spram = new SqlParameter[5];
                        spram[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                        spram[0].Value = "update_InvoiceMilestone_POWO_detils_PaymentWise";

                        spram[1] = new SqlParameter("@InvoiceId", SqlDbType.BigInt);
                        spram[1].Value = Convert.ToDouble(dspayments.Tables[4].Rows[irow]["InvoiceID"]);

                        spram[2]  = new SqlParameter("@PaymentID", SqlDbType.BigInt);
                        spram[2].Value = Convert.ToDouble(dspayments.Tables[4].Rows[irow]["Payment_ID"]);

                        spram[3] = new SqlParameter("@MstoneId", SqlDbType.BigInt);
                        spram[3].Value = Convert.ToDouble(dspayments.Tables[4].Rows[irow]["MstoneID"]);

                        spram[4] = new SqlParameter("@Batch_ID", SqlDbType.BigInt);
                        spram[4].Value = Convert.ToDouble(hdnBatchId.Value);

                        DataSet dsMListtt = spm.getDatasetList(spram, "SP_VSCB_PaymentStatus");
                    }*/
                    #endregion
                }

                CreateSecurity_Deposit_Entry();
            }
        }

        #endregion


        StringBuilder sbBatchDetails = new StringBuilder();
        StringBuilder sbBatchRequest = new StringBuilder();
        StringBuilder sbBatchApprs = new StringBuilder();
        StringBuilder strbuildBodyMsg = new StringBuilder();

        string approveremail_to = "";
        string approveremail_cc = "";

        string batchCreatorEmail = ""; 


        #region get Batch Details 
        dsList = new DataSet();
        SqlParameter[] spars1 = new SqlParameter[2];
        spars1[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars1[0].Value = "get_myBatch_Details";

        spars1[1] = new SqlParameter("@POWOID", SqlDbType.BigInt);
        spars1[1].Value = Convert.ToDouble(hdnBatchId.Value);

        dsList = spm.getDatasetList(spars1, "SP_VSCB_GETALL_DETAILS");

        batchCreatorEmail = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();

        for (Int32 irow = 0; irow < dsList.Tables[2].Rows.Count; irow++)
        {

            if(Convert.ToString(approveremail_to).Trim()=="")
            {
                if(Convert.ToString(dsList.Tables[2].Rows[irow]["Status"]).Trim()=="Pending")
                approveremail_to = Convert.ToString(dsList.Tables[2].Rows[irow]["Emp_Emailaddress"]).Trim();
            }
            if (Convert.ToString(dsList.Tables[2].Rows[irow]["Status"]).Trim() == "Approved" && Convert.ToString(dsList.Tables[2].Rows[irow]["approver_empcode"]).Trim() != Convert.ToString(txtEmpCode.Text).Trim())
            {
                if (Convert.ToString(approveremail_cc).Trim() == "")
                    approveremail_cc = Convert.ToString(dsList.Tables[2].Rows[irow]["Emp_Emailaddress"]).Trim();
                else
                    approveremail_cc = Convert.ToString(dsList.Tables[2].Rows[irow]["Emp_Emailaddress"]).Trim() + ";" + approveremail_cc;
            }
        }
            #endregion

        string strInvoiceURL = "";
        string strsubject = ""; if (Convert.ToString(approveremail_to).Trim() != "")
            strsubject = "OneHR: Request for online approval for payment on BOB Digi-next Net-banking Facility Bank-Refernce No." + Convert.ToString(txtBankRefNo.Text).Trim();
        else
            strsubject = "OneHR: online payment Approved - Bank-Refernce No." + Convert.ToString(txtBankRefNo.Text).Trim();

        strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["BatchapproverLink_VSCB"]).Trim() + "?batchid=" + hdnBatchId.Value).Trim();
       strbuildBodyMsg.Append("<table cellpadding='5' cellspacing='0' style='font-size: 9pt;font-family:Arial'>");
        strbuildBodyMsg.Append("<tr><td  colspan='2'>Dear Sir, </td></tr>");
        strbuildBodyMsg.Append("<tr><td style='height:15px'></td></tr>");
        if (Convert.ToString(approveremail_to).Trim() != "")
        {
            strbuildBodyMsg.Append("<tr><td colspan='2'> We have generated Online Payment instruction for Vendor Payment through BOB Digi-Next net banking facility.</td></tr>");
            strbuildBodyMsg.Append("<tr><td colspan='2'>Approval for this payment is attached herewith."+ sApproverNameonMail + " has done approval on portal.</ td></tr>");
          
        }
        else
        {
           strbuildBodyMsg.Append("<tr><td colspan='2'> This is to inform you that the online payment with Bank-Refernce No." + Convert.ToString(txtBankRefNo.Text).Trim() + " is approved.Following are the details for your reference</td></tr>");
        }

        strbuildBodyMsg.Append("<tr><td style='height:15px'></td></tr>");
        sbBatchRequest.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial;width:80%;'>");
        sbBatchRequest.Append("<tr><th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:left'>Reference Number</th>");
        sbBatchRequest.Append("<th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:left'>Upload By</th>");
        sbBatchRequest.Append("<th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:right'>Number of Transaction</th>");
        sbBatchRequest.Append("<th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:right'>Amount</th></tr>"); sbBatchRequest.Append("<tr><td style='width:20%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(txtBankRefNo.Text).Trim() + "</td>");
        sbBatchRequest.Append("<td style='width:30%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Name"]).Trim() + "</td>");
        sbBatchRequest.Append("<td style='width:15%;border: 1px solid #ccc;text-align:right'>" + Convert.ToString(dsList.Tables[0].Rows[0]["Batch_No_Requests"]).Trim() + "</td>");
        sbBatchRequest.Append("<td style='width:20%;border: 1px solid #ccc;text-align:right'>" + Convert.ToString(dsList.Tables[0].Rows[0]["Batch_Total_Payament"]).Trim() + "</td></tr>");
        sbBatchRequest.Append("</table>");

        #region Create Batch Request table
        if (dsList.Tables[1].Rows.Count > 0)
        {
            int sno = 1;
            sbBatchDetails.Append(" <br/><p>Below are details for Vendor Payment :-</p> <table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial;width:100%;'>");
            sbBatchDetails.Append("<tr><th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:left'>Sr.No</th>");
            sbBatchDetails.Append("<th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:left'>Payment Type</th>");
            sbBatchDetails.Append("<th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:left'>Vendor Name</th>");
            sbBatchDetails.Append("<th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:right'>Amt.Rs.</th>");
            sbBatchDetails.Append("<th th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:left'>Details</th>");
            sbBatchDetails.Append("<th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:left'>Approved By</th>");
            sbBatchDetails.Append("<th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:left'>Bank A/C No.</th>");
            sbBatchDetails.Append("<th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:left'>Beneficiary IFSC Code</th></tr>");
            for (Int32 irow = 0; irow < dsList.Tables[1].Rows.Count; irow++)
            {
                sbBatchDetails.Append("<tr>");
                sbBatchDetails.Append("<td style='width:10%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(sno).Trim() + "</td>");
                sbBatchDetails.Append("<td style='width:15%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[1].Rows[irow]["paymentType"]).Trim() + "</td>");
                sbBatchDetails.Append("<td style='width:20%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[1].Rows[irow]["VendorName"]).Trim() + "</td>");
                sbBatchDetails.Append("<td style='width:10%;border: 1px solid #ccc;text-align:right'>" + Convert.ToString(dsList.Tables[1].Rows[irow]["Amt_paid_Account"]).Trim() + "</td>");
                sbBatchDetails.Append("<td style='width:20%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[1].Rows[irow]["MilestoneParticular"]).Trim() + "</td>");
                sbBatchDetails.Append("<td style='width:20%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[1].Rows[irow]["PaymentApproverName"]).Trim() + "</td>");
                sbBatchDetails.Append("<td style='width:10%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[1].Rows[irow]["Acc_no"]).Trim() + "</td>");
                sbBatchDetails.Append("<td style='width:10%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[1].Rows[irow]["IFSC_Code"]).Trim() + "</td>");

                sbBatchDetails.Append("</tr>");
                sno += 1;
            }
            sbBatchDetails.Append("</table>");
        }


        #endregion

        #region create table for Approver
        sbBatchApprs.Append("<br/><br/><table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial;width:100%;'>");
        sbBatchApprs.Append("<tr><th style='background-color: #B8DBFD;border: 1px solid #ccc;text-align:left'>Approver Name</th>");
        sbBatchApprs.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc;text-align:left'>Status</th>");
        sbBatchApprs.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc;text-align:left'>Approved On</th>");
        sbBatchApprs.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc;text-align:left'>Approver Remarks</th></tr>");
        for (Int32 irow = 0; irow < dsList.Tables[2].Rows.Count; irow++)
        {
            
            sbBatchApprs.Append("<tr><td style='width:40%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[2].Rows[irow]["ApproverName"]).Trim() + " </td>");
            sbBatchApprs.Append("<td style='width:10%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[2].Rows[irow]["Status"]).Trim() + "</td>");
            sbBatchApprs.Append("<td style='width:15%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[2].Rows[irow]["approved_on"]).Trim() + "</td>");
            sbBatchApprs.Append("<td style='width:35%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[2].Rows[irow]["Remarks"]).Trim() + "</td></tr>");
        }
        sbBatchApprs.Append("</table>");
        #endregion


        if (Convert.ToString(approveremail_to).Trim() != "")
            sbBatchApprs.Append("<br/><p><a href=" + strInvoiceURL + ">Please click here to view Batch details & take appropriate action.</a></p>");


        if (Convert.ToString(approveremail_to).Trim() == "")
            approveremail_to = batchCreatorEmail;


        if (Convert.ToString(approveremail_cc).Trim() != "")
            approveremail_cc = approveremail_cc + ";" + Convert.ToString(ConfigurationManager.AppSettings["VSCB_Batch_EmailCC"]).Trim();
        else
            approveremail_cc = Convert.ToString(ConfigurationManager.AppSettings["VSCB_Batch_EmailCC"]).Trim();

        spm.sendMail_VSCB(approveremail_to, strsubject, Convert.ToString(strbuildBodyMsg).Trim() + Convert.ToString(sbBatchRequest).Trim() + Convert.ToString(sbBatchDetails).Trim() + Convert.ToString(sbBatchApprs).Trim(), "", approveremail_cc);


        Response.Redirect("VSCB_InboxBatchReq.aspx");

    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {

    }

    private void CreateSecurity_Deposit_Entry()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "get_Security_Deposit_entry";

        spars[1] = new SqlParameter("@Batch_ID", SqlDbType.BigInt);
        spars[1].Value = Convert.ToDouble(hdnBatchId.Value);

        dsList = spm.getDatasetList(spars, "SP_VSCB_CreateBatchRequest");

        if (dsList != null)
        {
            if (dsList.Tables[0].Rows.Count > 0)
            {
                for (Int32 irow = 0; irow < dsList.Tables[0].Rows.Count; irow++)
                {

                    #region Move File from Approved PO to 
                    string POFileName = Convert.ToString(Regex.Replace(Convert.ToString(dsList.Tables[0].Rows[irow]["Policyno"]), @"[^0-9a-zA-Z\._]", "_")).Trim() + ".pdf"; ;
                    string destinationFilePath = @"D:\HRMS\hrmsadmin\files\Insurance\" + POFileName;
                    string sourceFilePath = @"D:\HRMS\hrms\VendorBilling\ApprovedPO\" + POFileName;

                    // Copy the file.
                    File.Copy(sourceFilePath, destinationFilePath, true);

                    #endregion
                    SqlParameter[] spars1 = new SqlParameter[21];
                    spars1[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                    spars1[0].Value = "Insert";

                    spars1[1] = new SqlParameter("@Policyno", SqlDbType.NVarChar);
                    spars1[1].Value = Convert.ToString(dsList.Tables[0].Rows[irow]["Policyno"]);

                    spars1[2] = new SqlParameter("@Policytype", SqlDbType.Int);
                    spars1[2].Value = Convert.ToInt32(dsList.Tables[0].Rows[irow]["Policytype"]);

                    spars1[3] = new SqlParameter("@Policystatus", SqlDbType.Int);
                    spars1[3].Value = Convert.ToInt32(dsList.Tables[0].Rows[irow]["Policystatus"]);

                    spars1[4] = new SqlParameter("@Purpose", SqlDbType.VarChar);
                    spars1[4].Value = Convert.ToString(dsList.Tables[0].Rows[irow]["Purpose"]);

                    spars1[5] = new SqlParameter("@SumInsured", SqlDbType.Decimal);
                    spars1[5].Value = Convert.ToDecimal(dsList.Tables[0].Rows[irow]["SumInsured"]);

                    spars1[6] = new SqlParameter("@ValidFrom", SqlDbType.VarChar);
                    spars1[6].Value = Convert.ToString(dsList.Tables[0].Rows[irow]["ValidFrom"]);

                    spars1[7] = new SqlParameter("@ValidTo", SqlDbType.VarChar);
                    spars1[7].Value = Convert.ToString(dsList.Tables[0].Rows[irow]["ValidTo"]);

                    spars1[8] = new SqlParameter("@ResponsiblePerson", SqlDbType.VarChar);
                    spars1[8].Value = Convert.ToString(dsList.Tables[0].Rows[irow]["ResponsiblePerson"]);

                    spars1[9] = new SqlParameter("@InsuranceContactName", SqlDbType.VarChar);
                    spars1[9].Value = Convert.ToString(dsList.Tables[0].Rows[irow]["InsuranceContactName"]);

                    spars1[10] = new SqlParameter("@InsuranceContactMobile", SqlDbType.VarChar);
                    spars1[10].Value = Convert.ToString(dsList.Tables[0].Rows[irow]["InsuranceContactMobile"]);

                    spars1[11] = new SqlParameter("@InsuranceContactemail", SqlDbType.VarChar);
                    spars1[11].Value = Convert.ToString(dsList.Tables[0].Rows[irow]["InsuranceContactemail"]);

                    spars1[12] = new SqlParameter("@Createdby", SqlDbType.VarChar);
                    spars1[12].Value = Convert.ToString(dsList.Tables[0].Rows[irow]["Createdby"]);

                    spars1[13] = new SqlParameter("@Policy_file", SqlDbType.VarChar);
                    spars1[13].Value = Convert.ToString(POFileName);

                    spars1[14] = new SqlParameter("@Policy_Receipt", SqlDbType.VarChar);
                    spars1[14].Value = Convert.ToString(POFileName);

                    spars1[15] = new SqlParameter("@CustomerName", SqlDbType.VarChar);
                    spars1[15].Value = Convert.ToString(dsList.Tables[0].Rows[irow]["CustomerName"]);

                    spars1[16] = new SqlParameter("@ClaimDate", SqlDbType.VarChar);
                    spars1[16].Value = Convert.ToString(dsList.Tables[0].Rows[irow]["ClaimDate"]);

                    spars1[17] = new SqlParameter("@BankName", SqlDbType.VarChar);
                    spars1[17].Value = Convert.ToString(dsList.Tables[0].Rows[irow]["BankName"]);

                    spars1[18] = new SqlParameter("@InstrumentType", SqlDbType.Int);
                    spars1[18].Value = Convert.ToInt32(dsList.Tables[0].Rows[irow]["InstrumentType"]);

                    spars1[19] = new SqlParameter("@Project", SqlDbType.VarChar);
                    spars1[19].Value = Convert.ToString(dsList.Tables[0].Rows[irow]["Project"]);

                    spars1[20] = new SqlParameter("@project_name", SqlDbType.VarChar);
                    spars1[20].Value = Convert.ToString(dsList.Tables[0].Rows[irow]["project_name"]);

                    //spm.Insert_Data(spars1, "SP_Admin_BG");
                    DataSet lds = spm.getDatasetList(spars1, "SP_Admin_BG");

                }
            }
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        if (Convert.ToString(txtRemarks.Text).Trim() == "")
        {
            lblmessage.Text = "Please mention the comment before rejecting the Payment batch";
            return;
        }

        RejectCode(txtRemarks.Text.Trim(), "");


        Response.Redirect("VSCB_InboxBatchReq.aspx");
    }

    public void RejectCode(string strremrk,string strAutorejectSubjtmail)
    {
        try
        {
            DataSet dsList = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "Reject_batch";

            spars[1] = new SqlParameter("@Batch_ID", SqlDbType.BigInt);
            spars[1].Value = Convert.ToDouble(hdnBatchId.Value);

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(txtEmpCode.Text).Trim();

            spars[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
            spars[3].Value = Convert.ToString(strremrk).Trim();


            dsList = spm.getDatasetList(spars, "SP_VSCB_CreateBatchRequest_Details");



            StringBuilder sbBatchDetails = new StringBuilder();
            StringBuilder sbBatchRequest = new StringBuilder();
            StringBuilder sbBatchApprs = new StringBuilder();
            StringBuilder strbuildBodyMsg = new StringBuilder();

            string approveremail_to = "";
            string approveremail_cc = "";

            string batchCreatorEmail = "";
            string batchRejectApproverName = "";

            #region get Batch Details 
            dsList = new DataSet();
            SqlParameter[] spars1 = new SqlParameter[2];
            spars1[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars1[0].Value = "get_myBatch_Details";

            spars1[1] = new SqlParameter("@POWOID", SqlDbType.BigInt);
            spars1[1].Value = Convert.ToDouble(hdnBatchId.Value);

            dsList = spm.getDatasetList(spars1, "SP_VSCB_GETALL_DETAILS");

            batchCreatorEmail = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();

            for (Int32 irow = 0; irow < dsList.Tables[2].Rows.Count; irow++)
            {

                if (Convert.ToString(approveremail_to).Trim() == "")
                {
                    if (Convert.ToString(dsList.Tables[2].Rows[irow]["Status"]).Trim() == "Reject")
                        batchRejectApproverName = Convert.ToString(dsList.Tables[2].Rows[irow]["ApproverName"]).Trim();
                }
                if (Convert.ToString(dsList.Tables[2].Rows[irow]["Status"]).Trim() == "Approved" && Convert.ToString(dsList.Tables[2].Rows[irow]["approver_empcode"]).Trim() != Convert.ToString(txtEmpCode.Text).Trim())
                {
                    if (Convert.ToString(approveremail_cc).Trim() == "")
                        approveremail_cc = Convert.ToString(dsList.Tables[2].Rows[irow]["Emp_Emailaddress"]).Trim();
                    else
                        approveremail_cc = Convert.ToString(dsList.Tables[2].Rows[irow]["Emp_Emailaddress"]).Trim() + ";" + approveremail_cc;
                }
            }
            #endregion

            string strsubject = "";
            strsubject = strAutorejectSubjtmail + "Reject online payment -  " + Convert.ToString(dsList.Tables[0].Rows[0]["Batch_No"]).Trim();

            // strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["BatchapproverLink_VSCB"]).Trim() + "?batchid=" + hdnBatchId.Value).Trim();


            strbuildBodyMsg.Append("<table cellpadding='5' cellspacing='0' style='font-size: 9pt;font-family:Arial'>");
            strbuildBodyMsg.Append("<tr><td>Dear Sir </td></tr>");
            strbuildBodyMsg.Append("<tr><td style='height:15px'></td></tr>");
            if (strAutorejectSubjtmail == "")
            {
                strbuildBodyMsg.Append("<tr><td colspan='2'> This is to inform you that  " + batchRejectApproverName + " has reject an Online Payment instruction for Vendor Payment through OneHr with the following details.</td></tr>");
            }
            else
            {
                strbuildBodyMsg.Append("<tr><td colspan='2'> This is to inform you that the online payment instruction was  auto rejected due to the link often have a limited lifespan for security reason for vendor payment through OneHR with the following details.</td></tr>");
             }

            strbuildBodyMsg.Append("<tr><td style='height:15px'></td></tr>");

            sbBatchRequest.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial;width:100%;'>");
            sbBatchRequest.Append("<tr><th style='background-color: #B8DBFD;border: 1px solid #ccc;text-align:left'>Reference Number</th>");
            //sbBatchRequest.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>File Name</th>");
            sbBatchRequest.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc;text-align:left'>Upload By</th>");
            sbBatchRequest.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc;text-align:right'>Number of Transaction</th>");
            sbBatchRequest.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc;text-align:right'>Amount</th></tr>");

            //sbBatchRequest.Append("<tr><td style='width:20%;border: 1px solid #ccc'>" + Convert.ToString(dsList.Tables[0].Rows[0]["Batch_No"]).Trim() + "</td>");
            sbBatchRequest.Append("<tr><td style='width:20%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(txtBankRefNo.Text).Trim() + "</td>");
            //sbBatchRequest.Append("<td style='width:20%;border: 1px solid #ccc'></td>");
            sbBatchRequest.Append("<td style='width:30%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Name"]).Trim() + "</td>");
            sbBatchRequest.Append("<td style='width:15%;border: 1px solid #ccc;text-align:right'>" + Convert.ToString(dsList.Tables[0].Rows[0]["Batch_No_Requests"]).Trim() + "</td>");
            sbBatchRequest.Append("<td style='width:20%;border: 1px solid #ccc;text-align:right'>" + Convert.ToString(dsList.Tables[0].Rows[0]["Batch_Total_Payament"]).Trim() + "</td></tr>");
            sbBatchRequest.Append("</table>");

            #region Create Batch Request table
            if (dsList.Tables[1].Rows.Count > 0)
            {
                int sno = 1;
                sbBatchDetails.Append("<br/><br/> <table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial;width:100%;'>");
                sbBatchDetails.Append("<tr><th style='background-color: #B8DBFD;border: 1px solid #ccc;text-align:left'>Sr.No</th>");
                sbBatchDetails.Append("<th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:left'>Payment Type</th>");
                sbBatchDetails.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc;text-align:left'>Vendor Name</th>");
                sbBatchDetails.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc;text-align:right'>Amt.Rs.</th>");
                sbBatchDetails.Append("<th th style='background-color: #B8DBFD;border: 1px solid #ccc;text-align:left'>Details</th>");
                sbBatchDetails.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc;text-align:left'>Approved By</th>");
                sbBatchDetails.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc;text-align:left'>Bank A/C No.</th>");
                sbBatchDetails.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc;text-align:left'>IFSC Code</th></tr>");
                for (Int32 irow = 0; irow < dsList.Tables[1].Rows.Count; irow++)
                {
                    sbBatchDetails.Append("<tr>");
                    sbBatchDetails.Append("<td style='width:8%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(sno).Trim() + "</td>");
                    sbBatchDetails.Append("<td style='width:10%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[1].Rows[irow]["paymentType"]).Trim() + "</td>");
                    sbBatchDetails.Append("<td style='width:20%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[1].Rows[irow]["VendorName"]).Trim() + "</td>");
                    sbBatchDetails.Append("<td style='width:10%;border: 1px solid #ccc;text-align:right'>" + Convert.ToString(dsList.Tables[1].Rows[irow]["Amt_paid_Account"]).Trim() + "</td>");
                    sbBatchDetails.Append("<td style='width:20%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[1].Rows[irow]["MilestoneParticular"]).Trim() + "</td>");
                    sbBatchDetails.Append("<td style='width:20%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[1].Rows[irow]["PaymentApproverName"]).Trim() + "</td>");
                    sbBatchDetails.Append("<td style='width:10%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[1].Rows[irow]["Acc_no"]).Trim() + "</td>");
                    sbBatchDetails.Append("<td style='width:10%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[1].Rows[irow]["IFSC_Code"]).Trim() + "</td>");

                    sbBatchDetails.Append("</tr>");
                    sno += 1;
                }
                sbBatchDetails.Append("</table>");
            }
            #endregion

            #region create table for Approver
            sbBatchApprs.Append("<br/><br/><table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial;width:100%;'>");
            sbBatchApprs.Append("<tr><th style='background-color: #B8DBFD;border: 1px solid #ccc'>Approver Name</th>");
            sbBatchApprs.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Status</th>");
            sbBatchApprs.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Approved On</th>");
            sbBatchApprs.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Approver Remarks</th></tr>");
            for (Int32 irow = 0; irow < dsList.Tables[2].Rows.Count; irow++)
            {

                sbBatchApprs.Append("<tr><td style='width:40%;border: 1px solid #ccc'>" + Convert.ToString(dsList.Tables[2].Rows[irow]["ApproverName"]).Trim() + " </td>");
                sbBatchApprs.Append("<td style='width:10%;border: 1px solid #ccc'>" + Convert.ToString(dsList.Tables[2].Rows[irow]["Status"]).Trim() + "</td>");
                sbBatchApprs.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(dsList.Tables[2].Rows[irow]["approved_on"]).Trim() + "</td>");
                sbBatchApprs.Append("<td style='width:35%;border: 1px solid #ccc'>" + Convert.ToString(dsList.Tables[2].Rows[irow]["Remarks"]).Trim() + "</td></tr>");
            }
            sbBatchApprs.Append("</table>");
            #endregion


            approveremail_to = batchCreatorEmail;
            if (Convert.ToString(approveremail_cc).Trim() != "")
                approveremail_cc = approveremail_cc + ";" + Convert.ToString(ConfigurationManager.AppSettings["VSCB_Batch_EmailCC"]).Trim();
            else
                approveremail_cc =  Convert.ToString(ConfigurationManager.AppSettings["VSCB_Batch_EmailCC"]).Trim();

            spm.sendMail_VSCB(approveremail_to, strsubject, Convert.ToString(strbuildBodyMsg).Trim() + Convert.ToString(sbBatchRequest).Trim() + Convert.ToString(sbBatchDetails).Trim() + Convert.ToString(sbBatchApprs).Trim(), "", approveremail_cc);


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    protected void lnkLeaveDetails_Click1(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnPOWOID.Value = Convert.ToString(gvMngPaymentList_Batch.DataKeys[row.RowIndex].Values[2]).Trim();
        hdnInvoiceId.Value = Convert.ToString(gvMngPaymentList_Batch.DataKeys[row.RowIndex].Values[0]).Trim();
        string spaymentTypeid = Convert.ToString(gvMngPaymentList_Batch.DataKeys[row.RowIndex].Values[3]).Trim();

        if (Convert.ToString(hdnUrlType.Value).Trim() == "2")
        {
            if (Convert.ToString(spaymentTypeid).Trim() == "2")
                Response.Redirect("Mobile_Req_Arch.aspx?rem_id=" + hdnInvoiceId.Value + "&mngexp=2&inbtype=RACC&batch=ap&batchid=" + hdnBatchId.Value);

            if (Convert.ToString(spaymentTypeid).Trim() == "3")
                Response.Redirect("Fuel_Req_Arch.aspx?rem_id=" + hdnInvoiceId.Value + "&mngexp=2&inbtype=RACC&batch=ap&batchid=" + hdnBatchId.Value);

            if (Convert.ToString(spaymentTypeid).Trim() == "4")
                Response.Redirect("Payment_Req_Arch.aspx?rem_id=" + hdnInvoiceId.Value + "&mngexp=2&inbtype=RACC&batch=ap&batchid=" + hdnBatchId.Value);

            if (Convert.ToString(spaymentTypeid).Trim() == "5")
                Response.Redirect("ApprovedTravelReqst_Acc.aspx?expid=" + hdnInvoiceId.Value + "&mngexp=2&stype=ACC&batch=ap&batchid=" + hdnBatchId.Value);
        }
        else
        {
            if (Convert.ToString(spaymentTypeid).Trim() == "2")
                Response.Redirect("Mobile_Req_Arch.aspx?rem_id=" + hdnInvoiceId.Value + "&mngexp=0&inbtype=RACC&batch=ap&batchid=" + hdnBatchId.Value);

            if (Convert.ToString(spaymentTypeid).Trim() == "3")
                Response.Redirect("Fuel_Req_Arch.aspx?rem_id=" + hdnInvoiceId.Value + "&mngexp=0&inbtype=RACC&batch=ap&batchid=" + hdnBatchId.Value);

            if (Convert.ToString(spaymentTypeid).Trim() == "4")
                Response.Redirect("Payment_Req_Arch.aspx?rem_id=" + hdnInvoiceId.Value + "&mngexp=0&inbtype=RACC&batch=ap&batchid=" + hdnBatchId.Value);

            if (Convert.ToString(spaymentTypeid).Trim() == "5")
                Response.Redirect("ApprovedTravelReqst_Acc.aspx?expid=" + hdnInvoiceId.Value + "&mngexp=0&stype=ACC&batch=ap&batchid=" + hdnBatchId.Value);

        }

        if (Convert.ToString(hdnUrlType.Value).Trim() == "2")
        {
            if (Convert.ToString(spaymentTypeid).Trim() == "6" || Convert.ToString(spaymentTypeid).Trim() == "7")
                Response.Redirect("VSCB_My_Adv_PayRequestView.aspx?Payment_ID=" + hdnInvoiceId.Value + "&POID=" + hdnPOWOID.Value + "&batch=vap&batchid=" + hdnBatchId.Value);
            else
                Response.Redirect("VSCB_ApprovePaymentRequest_View.aspx?Payment_ID=" + hdnInvoiceId.Value + "&POID=" + hdnPOWOID.Value + "&type=appr&batchid=" + hdnBatchId.Value);
        }
        else
        {
            if (Convert.ToString(spaymentTypeid).Trim() == "6"  || Convert.ToString(spaymentTypeid).Trim() == "7")
                Response.Redirect("VSCB_My_Adv_PayRequestView.aspx?Payment_ID=" + hdnInvoiceId.Value + "&POID=" + hdnPOWOID.Value + "&batch=va&batchid=" + hdnBatchId.Value);
            else
                Response.Redirect("VSCB_ApprovePaymentRequest_View.aspx?Payment_ID=" + hdnInvoiceId.Value + "&POID=" + hdnPOWOID.Value + "&type=ab&batchid=" + hdnBatchId.Value);
        }
    }

    protected void btnback_mng_Click(object sender, EventArgs e)
    {

        if(Convert.ToString(hdnUrlType.Value).Trim()=="2")
        {
            Response.Redirect("VSCB_Myapprovedbatch.aspx");
        }
        else
        {
            Response.Redirect("VSCB_InboxBatchReq.aspx");
        } 
    }

    protected void lnkfile_Invoice_Click(object sender, EventArgs e)
    {

        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBTallyBatchfiles"]).Trim()), lnkfile_Invoice.Text);
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

    protected void gvMngPaymentList_Batch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMngPaymentList_Batch.PageIndex = e.NewPageIndex;
        get_BatchRequest_Approver();
    }
}