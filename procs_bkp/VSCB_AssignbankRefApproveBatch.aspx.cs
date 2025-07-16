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

public partial class VSCB_AssignbankRefApproveBatch : System.Web.UI.Page
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
                    editform.Visible = true;
                    PopulateEmployeeData();
                    GetBatchNos_List();

                    if (Request.QueryString.Count > 0)
                    {
                        hdnBatchId.Value = Convert.ToString(Request.QueryString["batchid"]).Trim();
                        lstBactchRefNo.SelectedValue = hdnBatchId.Value;
                        get_ApprovedBatchRequest_Details();
                    }

                    //    get_BatchRequest_Approver();

                    //    if(Request.QueryString.Count==2)
                    //    {
                    //        hdnUrlType.Value = Convert.ToString(Request.QueryString["mngexp"]).Trim();
                    //    }
                    //    if(Convert.ToString(hdnUrlType.Value).Trim()=="2")
                    //    {
                    //        trvl_btnSave.Visible = false;
                    //        btnCancel.Visible = false;
                    //        spnremarks.Visible = false;
                    //        txtRemarks.Visible = false;
                    //    }
                    //}


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

    public void GetBatchNos_List()
    {

        DataSet dtBatchNosList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_ApprovedBatchNos_list"; // "get_Approved_POWOList_CreateInvoice";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = txtEmpCode.Text;


        dtBatchNosList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");


        if (dtBatchNosList.Tables[0].Rows.Count > 0)
        {
            lstBactchRefNo.DataSource = dtBatchNosList;
            lstBactchRefNo.DataTextField = "Batch_No";
            lstBactchRefNo.DataValueField = "Batch_ID";
            lstBactchRefNo.DataBind();
        }
        lstBactchRefNo.Items.Insert(0, new ListItem("Select Batch No", "0"));

    }


    private void get_ApprovedBatchRequest_Details()
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
            spars[0].Value = "get_BatchReq_Details_Payment";

            spars[1] = new SqlParameter("@POWOID", SqlDbType.BigInt);
            spars[1].Value = Convert.ToDouble(hdnBatchId.Value);

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(txtEmpCode.Text).Trim();

            dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");


            if (dsList.Tables[0].Rows.Count > 0)
            {
                txtbatchCreateDate.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_Date"]).Trim();
                txtbatchCreatedBy.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_Created_By"]).Trim();
                txtbatchNo.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_No"]).Trim();
                txtbatchNoOfRequest.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_No_Requests"]).Trim();
                txtbatchTotalPayment.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_Total_Payament"]).Trim();
                txtBank_name.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Name"]).Trim();
                txtBankRef_Link.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_URL"]).Trim();
                txtBankRefDate.Text = Convert.ToString(dsList.Tables[0].Rows[0]["BankRefDate"]).Trim();
                 
            }

            if (dsList.Tables[1].Rows.Count > 0)
            {
                gvMngPaymentList_Batch.DataSource = dsList.Tables[1];
                gvMngPaymentList_Batch.DataBind();
                spnVoucherDetails.Visible = true;
            }

            //get Checker or approver code Emp details
            if (dsList.Tables[2].Rows.Count > 0)
            {
                if (Convert.ToString(dsList.Tables[2].Rows[0]["Mekar_Empcode"]).Trim() == Convert.ToString(txtEmpCode.Text).Trim())
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
                DgBatchPaymentApprover.DataSource = dsList.Tables[3];
                DgBatchPaymentApprover.DataBind();
                spnBatchPaymentApprover.Visible = true;
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
                txtEmpName.Text = (string)dtEmp.Rows[0]["Emp_Name"];
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

    public string ReplaceInvalidChars(string filename)
    {
        Regex illegalInFileName = new Regex(@"[#%&{}\!/<'>@+*?$|=]");
        string myString = illegalInFileName.Replace(filename, "_");
        //return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        return myString;
    }

    #endregion


    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {


        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        if (Convert.ToString(lstBactchRefNo.SelectedValue).Trim() == "0")
        {
            lblmessage.Text = "Please Select Batch Ref. No";
            return;
        }
        if (Convert.ToString(txtBankRefNo.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Bank Ref. No";
            return;
        }
        if (Convert.ToString(txtBankRefDate.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Bank Ref. Date";
            return;
        }

        if (Convert.ToString(txtBankRef_Link.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Bank Ref. Link";
            return;
        }


        #region Check Duplicate Batch Ref.No
        DataSet dsList_C = new DataSet();
        SqlParameter[] sparC = new SqlParameter[2];
        sparC[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        sparC[0].Value = "Validate_BankRefNo";

        sparC[1] = new SqlParameter("@costcenter", SqlDbType.VarChar);
        sparC[1].Value = Convert.ToString(txtBankRefNo.Text);

        dsList_C = spm.getDatasetList(sparC,"SP_VSCB_GETALL_DETAILS");
        if(dsList_C !=null)
        {
            if(dsList_C.Tables[0].Rows.Count>0)
            {
                lblmessage.Text = " Bank Ref. No already exist. ";
                return;
            }
        }
        #endregion

        //if (!InvoiceUploadfile.HasFile)
        //{
        //    lblmessage.Text = "Please upload Batch Transaction file.";
        //    return;
        //}

        string[] strdate;
        string strInvoiceDate = "";
        if (Convert.ToString(txtBankRefDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtBankRefDate.Text).Trim().Split('-');
            strInvoiceDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        double dMaxReqId = 0;
        StringBuilder sbBatchDetails = new StringBuilder();
        StringBuilder sbBatchRequest = new StringBuilder();
        StringBuilder sbBatchApprs = new StringBuilder();
        StringBuilder strbuildBodyMsg = new StringBuilder();

        string strcheckerEmail = "";
        string filename = "";
        dMaxReqId = Convert.ToDouble(lstBactchRefNo.SelectedValue);


        #region insert or upload multiple files
        //if (InvoiceUploadfile.HasFile)
        //{
        //    filename = InvoiceUploadfile.FileName;
        //    string MilestoneFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBTallyBatchfiles"]).Trim() + "/");
        //    bool folderExists = Directory.Exists(MilestoneFilePath);
        //    if (!folderExists)
        //    {
        //        Directory.CreateDirectory(MilestoneFilePath);
        //    }
        //    String InputFile = System.IO.Path.GetExtension(InvoiceUploadfile.FileName);
        //    filename = txtEmpCode.Text + "_" + Convert.ToString(dMaxReqId).Trim() + "_Tally_Batch" + InputFile;
        //    InvoiceUploadfile.SaveAs(Path.Combine(MilestoneFilePath, filename));
        //}

     
        #endregion

        #region Update Batch details

        DataSet dsBatchReq = new DataSet();
        SqlParameter[] spars = new SqlParameter[6];
        spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
        spars[0].Value = "Update_Batch_details";

        spars[1] = new SqlParameter("@PaymentID", SqlDbType.BigInt);
        spars[1].Value = dMaxReqId;

        spars[2] = new SqlParameter("@Bank_Ref_no", SqlDbType.VarChar);
        if (Convert.ToString(txtBankRefNo.Text).Trim() != "")
            spars[2].Value = Convert.ToString(txtBankRefNo.Text).Trim();
        else
            spars[2].Value = DBNull.Value;

        spars[3] = new SqlParameter("@Bank_Ref_Date", SqlDbType.VarChar);
        if (Convert.ToString(txtBankRefDate.Text).Trim() != "")
            spars[3].Value = Convert.ToString(strInvoiceDate).Trim();
        else
            spars[3].Value = DBNull.Value;

        spars[4] = new SqlParameter("@Bank_Ref_Link", SqlDbType.VarChar);
        if (Convert.ToString(txtBankRef_Link.Text).Trim() != "")
            spars[4].Value = Convert.ToString(txtBankRef_Link.Text).Trim();
        else
            spars[4].Value = DBNull.Value;

        spars[5] = new SqlParameter("@Bank_Ref_File", SqlDbType.VarChar);
        if (Convert.ToString(filename).Trim() != "")
            spars[5].Value = Convert.ToString(filename).Trim();
        else
            spars[5].Value = DBNull.Value;

        dsBatchReq = spm.getDatasetList(spars, "SP_VSCB_INSERT_POWOTAlly");
         

        #endregion

        #region get Batch Details 
        DataSet dsList = new DataSet();
        SqlParameter[] spar = new SqlParameter[2];
        spar[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spar[0].Value = "get_myBatch_Details";

        spar[1] = new SqlParameter("@POWOID", SqlDbType.BigInt);
        spar[1].Value = dMaxReqId;

        dsList = spm.getDatasetList(spar, "SP_VSCB_GETALL_DETAILS");
        //gvMngPaymentList_Batch.DataSource = null;
        //gvMngPaymentList_Batch.DataBind();
        //DgvApprover.DataSource = null;
        //DgvApprover.DataBind();

        #endregion


        #region send Email to  Batch Approval
        string strInvoiceURL = "";
       // string strsubject = "OneHR: Request for Payment Batch approval - Batch no." +  Convert.ToString(dsList.Tables[0].Rows[0]["Batch_No"]).Trim();
        string strsubject = "OneHR: Request for online approval for payment on BOB Digi-next Net-banking Facility Bank-Refernce No." +  Convert.ToString(txtBankRefNo.Text).Trim();
        strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["BatchapproverLink_VSCB"]).Trim() + "?batchid=" + dMaxReqId).Trim();


        strbuildBodyMsg.Append("<table cellpadding='5' cellspacing='0' style='font-size: 9pt;font-family:Arial'>");
        strbuildBodyMsg.Append("<tr><td  colspan='2'>Dear Sir, </td></tr>");
        strbuildBodyMsg.Append("<tr><td style='height:15px'></td></tr>");
        strbuildBodyMsg.Append("<tr><td colspan='2'>We have generated Online Payment instruction for Vendor Payment through BOB Digi-Next net banking facility.</td></tr>");

        //strbuildBodyMsg.Append("<tr><td style='height:15px'></td></tr>");
        strbuildBodyMsg.Append("<tr><td colspan='2'>Approval for this payment is attached herewith.</ td></tr>");
        strbuildBodyMsg.Append("<tr><td style='height:15px'></td></tr></table>");

        sbBatchRequest.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial;width:80%;'>");
        sbBatchRequest.Append("<tr><th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:left'>Reference Number</th>");
        //sbBatchRequest.Append("<th style='background-color: #B4C6E7;border: 1px solid #ccc'>File Name</th>");
        sbBatchRequest.Append("<th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:left'>Upload By</th>");
        sbBatchRequest.Append("<th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:right'>Number of Transaction</th>");
        sbBatchRequest.Append("<th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:right'>Amount</th></tr>");

        sbBatchRequest.Append("<tr><td style='width:20%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(txtBankRefNo.Text).Trim() + "</td>");
        //sbBatchRequest.Append("<td style='width:20%;border: 1px solid #ccc'></td>");
        sbBatchRequest.Append("<td style='width:30%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Name"]).Trim() + "</td>");
        sbBatchRequest.Append("<td style='width:10%;border: 1px solid #ccc;text-align:right'>" + Convert.ToString(dsList.Tables[0].Rows[0]["Batch_No_Requests"]).Trim() + "</td>");
        sbBatchRequest.Append("<td style='width:20%;border: 1px solid #ccc;text-align:right'>" + Convert.ToString(dsList.Tables[0].Rows[0]["Batch_Total_Payament"]).Trim() + "</td></tr>");
        sbBatchRequest.Append("</table>");

        #region Create Batch Request table
        if (dsList.Tables[1].Rows.Count > 0)
        {
            int sno = 1;
            sbBatchDetails.Append("<br/><p>Below are details for Vendor Payment :-</p><table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial;width:100%;'>");
            sbBatchDetails.Append("<tr><th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:left'>Sr.No</th>");
            sbBatchDetails.Append("<th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:left'>Payment Type</th>");
            sbBatchDetails.Append("<th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:left'>Vendor Name</th>");
            sbBatchDetails.Append("<th style='background-color: #B4C6E7;border: 1px solid #ccctext-align:right'>Amt.Rs.</th>");
            sbBatchDetails.Append("<th th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:left'>Details</th>");
            sbBatchDetails.Append("<th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:left'>Approved By</th>");
            sbBatchDetails.Append("<th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:left'>Bank A/C No.</th>");
            sbBatchDetails.Append("<th style='background-color: #B4C6E7;border: 1px solid #ccc;text-align:left'>Beneficiary IFSC Code</ th></tr>");
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
        sbBatchApprs.Append("<tr><th style='background-color: #C7D3D4;border: 1px solid #ccc;text-align:left'>Approver Name</th>");
        sbBatchApprs.Append("<th style='background-color: #C7D3D4;border: 1px solid #ccc;text-align:left'>Status</th>");
        sbBatchApprs.Append("<th style='background-color: #C7D3D4;border: 1px solid #ccc;text-align:left'>Approved On</th>");
        sbBatchApprs.Append("<th style='background-color: #C7D3D4;border: 1px solid #ccc;text-align:left'>Approver Remarks</th></tr>");
        for (Int32 irow = 0; irow < dsList.Tables[2].Rows.Count; irow++)
        {
            if (Convert.ToString(strcheckerEmail).Trim() == "")
            {
                strcheckerEmail = Convert.ToString(dsList.Tables[2].Rows[irow]["Emp_Emailaddress"]).Trim();
            }
            sbBatchApprs.Append("<tr><td style='width:40%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[2].Rows[irow]["ApproverName"]).Trim() + " </td>");
            sbBatchApprs.Append("<td style='width:10%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[2].Rows[irow]["Status"]).Trim() + "</td>");
            sbBatchApprs.Append("<td style='width:15%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[2].Rows[irow]["approved_on"]).Trim() + "</td>");
            sbBatchApprs.Append("<td style='width:35%;border: 1px solid #ccc;text-align:left'>" + Convert.ToString(dsList.Tables[2].Rows[irow]["Remarks"]).Trim() + "</td></tr>");
        }
        sbBatchApprs.Append("</table>");
        #endregion

        sbBatchApprs.Append("<br/><p><a href=" + strInvoiceURL + ">Please click here to view Batch details & take appropriate action.</a></p>");
        string sccemail = Convert.ToString(ConfigurationManager.AppSettings["VSCB_Batch_EmailCC"]).Trim();
        spm.sendMail_VSCB(strcheckerEmail, strsubject, Convert.ToString(strbuildBodyMsg).Trim() + Convert.ToString(sbBatchRequest).Trim() + Convert.ToString(sbBatchDetails).Trim() + Convert.ToString(sbBatchApprs).Trim(), "", sccemail);

        #endregion




        Response.Redirect("VSCB_PendingBatchReqForAtchBankRef.aspx");

    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    protected void lnkLeaveDetails_Click1(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnPOWOID.Value = Convert.ToString(gvMngPaymentList_Batch.DataKeys[row.RowIndex].Values[2]).Trim();
        hdnInvoiceId.Value = Convert.ToString(gvMngPaymentList_Batch.DataKeys[row.RowIndex].Values[0]).Trim();
        string ipaymentTypeid = Convert.ToString(gvMngPaymentList_Batch.DataKeys[row.RowIndex].Values[3]).Trim();

        if (Convert.ToString(ipaymentTypeid).Trim() == "1")
            Response.Redirect("VSCB_ApprovePaymentRequest_View.aspx?Payment_ID=" + hdnInvoiceId.Value + "&POID=" + hdnPOWOID.Value + "&type=asb&batchid=" + hdnBatchId.Value);

        if (Convert.ToString(ipaymentTypeid).Trim() == "2")
            Response.Redirect("Mobile_Req_Arch.aspx?rem_id=" + hdnInvoiceId.Value + "&mngexp=0&inbtype=RACC&batch=asb&batchid=" + hdnBatchId.Value);

        if (Convert.ToString(ipaymentTypeid).Trim() == "3")
            Response.Redirect("Fuel_Req_Arch.aspx?rem_id=" + hdnInvoiceId.Value + "&mngexp=0&inbtype=RACC&batch=asb&batchid=" + hdnBatchId.Value);

        if (Convert.ToString(ipaymentTypeid).Trim() == "4")
            Response.Redirect("Payment_Req_Arch.aspx?rem_id=" + hdnInvoiceId.Value + "&mngexp=0&inbtype=RACC&batch=asb&batchid=" + hdnBatchId.Value);

        if (Convert.ToString(ipaymentTypeid).Trim() == "5")
            Response.Redirect("ApprovedTravelReqst_Acc.aspx?expid=" + hdnInvoiceId.Value + "&stype=ACC&batch=asb&batchid=" + hdnBatchId.Value);

        if (Convert.ToString(ipaymentTypeid).Trim() == "6" || Convert.ToString(ipaymentTypeid).Trim() == "7")
            Response.Redirect("VSCB_My_Adv_PayRequestView.aspx?Payment_ID=" + hdnInvoiceId.Value + "&POID=" + hdnPOWOID.Value + "&batch=v&batchid=" + hdnBatchId.Value);

    }

    protected void btnback_mng_Click(object sender, EventArgs e)
    {
        Response.Redirect("VSCB_PendingBatchReqForAtchBankRef.aspx");
    }

    protected void lstBactchRefNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnBatchId.Value = "0";
        if (Convert.ToString(lstBactchRefNo.SelectedValue).Trim() != "0")
            hdnBatchId.Value = Convert.ToString(lstBactchRefNo.SelectedValue).Trim();

        get_ApprovedBatchRequest_Details();
    }

    protected void lnkfile_Invoice_Click(object sender, EventArgs e)
    {

    }



    protected void gvMngPaymentList_Batch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMngPaymentList_Batch.PageIndex = e.NewPageIndex;
        get_ApprovedBatchRequest_Details();
    }
}