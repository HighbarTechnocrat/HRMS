using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class procs_VSCB_Index : System.Web.UI.Page
{
	SqlConnection source;
	public SqlDataAdapter sqladp;
	public static string dob1;
	public string userid;
	public static string pimg = "";
	public static string cimg = "";
	public string loc = "", dept = "", subdept = "", desg = "";
	public int did = 0;
	LeaveBalance bl = new LeaveBalance();
	SP_Methods spm = new SP_Methods();
	Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    
    //Comment 2
    protected void lnkhome_Click(object sender, EventArgs e)
	{
		Response.Redirect(ReturnUrl("sitepathmain") + "default");
	}

	public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

	private void Page_Load(object sender, System.EventArgs e)

    {
		try
		{
			if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
			}

			hflEmpCode.Value = Convert.ToString(Session["Empcode"]);
			lblmsg.Visible = false;

			if (Page.User.Identity.IsAuthenticated == false)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/SampleForm");
			}
			else
			{
				Page.SmartNavigation = true;
				if (!Page.IsPostBack)
				{
                    //GetCreatePOWO();
                    //GetCreatePOWOApproval();
                    //GetCreatePOWO_Account_Approval();

                    ////               CheckIsVSCBAmountPaidByAccountChange();
                    ////               CheckAprovedBatchDetails();
                    ////               CheckIsReportShow_VSCB_CreateInvoicePaymentMaker();
                    ////               CheckIsReportShow_VSCB_VendorCreate();
                    ////               CheckIsReportShow_VSCB_DeletePOWO();

                    ////               GetPaymentRequestPendingCount();
                    ////               getMngPOWOReqstCount();
                    ////               getMngInvoiceReqstCount();
                    ////getMng_PendingBatchReqstCount();
                    ////GetPartialPaymentPendingCount();
                    ////               getMng_PendingBatchReqstCount_BankRef();
                    ////               check_LoginEmployee_InvoiceCreateInboxpaymentReq();

                    ////               //get Login user details for check 
                    ////               check_LoginEmployee_Role_forVendorBilling_process(); 
                    ////               CheckIsReportShow_VSCB_InvoiceApprovalMatrix();
                    ////               CheckIsReportShow_VSCB_UploadedPOWO();
                    ////               CheckIsReportShow_VSCB_Reports();
                    ////               CheckIsReportShow_UploadCutoffData();

                    ////               Get_POWO_Approval_Status_Report();
                    ///

                    //Below is new function

                    Get_ADV_Pay_PendingCount();
                    Get_ADV_Pay_Approval();

                    check_LoginEmployee_Role_forVSCB_process();
                    CheckIs_VSCB_Reports_Accecc();

                    //
                    Get_POWO_Approval_Status_Report();
                    CheckIsReportShow_VSCB_VendorCreate();



                    if (spm.get_SHOW_HIDE_From_ReportDetails(Convert.ToString(hflEmpCode.Value).Trim(), "VSCB_CreatePOWOMatrix")=="SHOW")
                    {
                        Lnk_POWO_Other_Approval.Visible = true;
                    }

                    tdAdvance_Create.Visible = true;
                    CheckIsReportShow_VSCB_CreateProduct();
			
		            check_IsPOList_Security_InvoiceLink_Show();
                    ReverseButtonRequestSend_Approval();
                    ReverseButton_Approval();

                }
            }
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}

    public void ReverseButtonRequestSend_Approval()
    {
        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "InvoiceApprovalReport";

        spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(Session["Empcode"]).ToString();

        dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_CreateInvoice");

            if (dsProjectsVendors.Tables[0].Rows.Count > 0)
            {
                 Lnk_ApprovedInvoiceReverse.Visible = true;
            }
    }

    public void ReverseButton_Approval()
    {
        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "InvoiceRequestApprovalInbox_Reversal";

        spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(Session["Empcode"]).ToString();

        dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_CreateInvoice");

        if (dsProjectsVendors.Tables[0].Rows.Count > 0)
        {
            Lnk_InboxInvoiceReverse.Visible = true;
            Lnk_InboxInvoiceReverse.Text = "Inbox Reversel Invoices (" + dsProjectsVendors.Tables[0].Rows.Count + ")";
        }
    }

    public void check_IsPOList_Security_InvoiceLink_Show()
    {

        DataSet dtPOWODetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
        spars[0].Value = "POWO_List_forSecurityDeposit_Invoice";  

        spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(hflEmpCode.Value).Trim();


        dtPOWODetails = spm.getDatasetList(spars, "sp_VSCB_CreatePOWO_Users");


        if (dtPOWODetails.Tables[0].Rows.Count > 0)
        {
            tr_CreateInvoice_Security_Deposit.Visible = true;
            lnk_CreateInvoice_SecurityDeposit.Visible = true;
        }
        

    }

    public void Get_ADV_Pay_Approval()
    {
        try
        {
            DataTable dtEmpDetails = new DataTable();
            dtEmpDetails = spm.dtPOWoCreate("Get_ADV_Payment_Approval", Convert.ToString(hflEmpCode.Value));
            if (dtEmpDetails.Rows.Count > 0)
            {
                span_App_head.Visible = true;
                TrADPayment.Visible = true;
                hr_App_head.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

            throw;
        }
    }
    protected void Get_ADV_Pay_PendingCount()
    {
        try
        {
            int PaymentRequestCount = 0;
            PaymentRequestCount = spm.GetPaymentRequest_Pending_InboxList_Count("Get_ADV_Pay_Pending_cnt", Convert.ToString(hflEmpCode.Value).Trim());
            lnk_Inbox_ADV_Payment_Request.Text = "Inbox Advance Payment Request :(" + PaymentRequestCount.ToString() + ")";
        }
        catch (Exception)
        {

            throw;
        }
    }


    private void check_LoginEmployee_Role_forVSCB_process()
    {
        try
        {
            DataSet dsList = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Check_IndexAccess_forLoginEmployee";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value).Trim();

            dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS_For_Index");

            if (dsList != null)
            {
                if (dsList.Tables.Count > 0)
                {
                    //set visibility for Create PO/WO and Payment Req.with POWO
                    if (dsList.Tables[0].Rows.Count > 0)
                    {
                        idTRPOWO_Create.Visible = true;
                        //  idTRPaymentReqWithPOWO.Visible = true; comment code from  harshad

                    }

                    //set visibility for Inbox PO/WO and PO/WO Approved List
                    if (dsList.Tables[1].Rows.Count > 0)
                    {
                        idTRApproverHead.Visible = true;
                        idTRApproverHead_Line.Visible = true;
                        idTRPOWOApprover_inbox.Visible = true;
                    }

                    //set visibility for Invoice Inbox and Approved Invocie List
                    if (dsList.Tables[2].Rows.Count > 0)
                    {
                        idTRApproverHead.Visible = true;
                        idTRApproverHead_Line.Visible = true;
                        idTRInvoiceApprover_inbox.Visible = true;
                    }

                    //set visibility for Payment Req Inbox and Approved Payment Req List
                    if (dsList.Tables[3].Rows.Count > 0)
                    {
                        idTRApproverHead.Visible = true;
                        idTRApproverHead_Line.Visible = true;
                        idTRPaymentReqApprover_inbox.Visible = true;
                    }

                    //set visibility for Partial Payment Req Inbox 
                    if (dsList.Tables[4].Rows.Count > 0)
                    {
                        if (Convert.ToString(dsList.Tables[4].Rows[0]["chkAccount"]).Trim() == "yes")
                        {
                            idTRAccountHead.Visible = true;
                            idTRAccountHead_Line.Visible = true;
                            idTRPartialPay_Inbox.Visible = true;
                            //idTRAcc_Invoice_Inbox.Visible = true;
                        }
                    }

                    //set visibility for  Create Batch
                    if (dsList.Tables[5].Rows.Count > 0)
                    {
                        idTRApproverHead.Visible = true;
                        idTRApproverHead_Line.Visible = true;
                        idTRBatch_Create.Visible = true;
                        idTRAtachBatch_BankRef.Visible = true;
                        Tr_Payment_Report.Visible = true;
                    }

                    //set visibility for  Batch Req.Approver
                    if (dsList.Tables[6].Rows.Count > 0)
                    {
                        idTRApproverHead.Visible = true;
                        idTRApproverHead_Line.Visible = true;
                        idTRBatch_Req_Approver.Visible = true;
                    }

                    //CheckAprovedBatchDetails
                    if (dsList.Tables[7].Rows.Count > 0)
                    {
                        trViewApprovedBatch.Visible = true;
                    }

                    //check_LoginEmployee_InvoiceCreateInboxpaymentReq
                    if (dsList.Tables[8].Rows.Count > 0)
                    {
                        // IdTRCreInvoicePRequest.Visible = true;
                        Lnk_PaymentRequestAll.Visible = true;
                        lnk_reimbursmentReport_1.Visible = true;
                        if (dsList.Tables[8].Rows.Count > 0)
                        {
                            int strCount = dsList.Tables[9].Rows.Count;
                            Lnk_PaymentRequestAll.Text = "Create Payment Requests (" + strCount + ")";
                        }

                    }
                    //CheckIsReportShow_VSCB_CreateInvoicePaymentMaker
                    if (dsList.Tables[10].Rows.Count > 0)
                    {
                        Lnk_Invoice_Payment_Maker.Visible = true;
                        Lnk_POWO_Other_Approval.Visible = true;

                    }

                    //GetPaymentRequestPendingCount--Get_PaymentRequest_Pending_Approver_cnt
                    int PaymentRequestCount = 0;
                    if (dsList.Tables[11].Rows.Count > 0)
                    {
                        PaymentRequestCount = (int)dsList.Tables[11].Rows[0]["Paymentcnt"];
                    }
                    lnk_Inbox_Payment_Request.Text = "Inbox Payment Request :(" + PaymentRequestCount.ToString() + ")";

                    //getMngPOWOReqstCount
                    if (dsList.Tables[12].Rows.Count > 0)
                    {
                        lnk_POWOInbox.Text = "Inbox PO/ WO (" + dsList.Tables[12].Rows[0]["NoofPOWOs"] + ")";
                    }

                    //getMngInvoiceReqstCount
                    if (dsList.Tables[13].Rows.Count > 0)
                    {
                        lnk_summary_report.Text = "Inbox Invoices (" + dsList.Tables[13].Rows[0]["NoofInvoices"] + ")";
                    }

                    //getMng_PendingBatchReqstCount
                    if (dsList.Tables[14].Rows.Count > 0)
                    {
                        lnk_Index_Acc_Batch_Requests.Text = "Inbox Batch Requests (" + dsList.Tables[14].Rows.Count + ")";
                    }

                    //GetPartialPaymentPendingCount
                    int ParPaymentRequestCount = 0;
                    if (dsList.Tables[15].Rows.Count > 0)
                    {
                        ParPaymentRequestCount = (int)dsList.Tables[15].Rows[0]["Paymentcnt"];
                    }
                    lnk_CustomerFirstReport.Text = "Inbox Partial Payment Requests:(" + ParPaymentRequestCount.ToString() + ")";

                    //getMng_PendingBatchReqstCount_BankRef

                    if (dsList.Tables[16].Rows.Count > 0)
                    {
                        lnkAttachBatch_BankPaymentRef.Text = "Attach Bank Ref to Batch for Approval (" + dsList.Tables[16].Rows.Count + ")";
                    }

                    //Get_POWO_Approval_Status_Report
                    if (dsList.Tables[17].Rows.Count > 0)
                    {
                        idTR_Reports_1.Visible = true;
                        Tr_POWO_Report.Visible = true;
                        lnk_POWO_Approval_Status_Report.Visible = true;
                    }

                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void CheckIs_VSCB_Reports_Accecc()
    {
        DataSet getdtDetails = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowVSCBReportsAccess";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            getdtDetails = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS_For_Index");

            //CheckIsVSCBAmountPaidByAccountChange - VSCB_ACCPIADAMTCHANGE
            if (getdtDetails.Tables[0].Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Tables[0].Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    TrACCPIADAMTCHANGE.Visible = true;
                    idTR_Reports_1.Visible = true;
                }
            }

            //CheckIsReportShow_VSCB_VendorCreate - VSCB_CreateVendor
            if (getdtDetails.Tables[1].Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Tables[1].Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    Lnk_Createvendor.Visible = true;
                }
            }

            //CheckIsReportShow_VSCB_DeletePOWO - DeletePOWOList
            if (getdtDetails.Tables[2].Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Tables[2].Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    lnk_ApprovedPOWOList.Visible = true;
                }
            }

            //CheckIsShow_InvoiceAppMatrix - VSCB_InvoiceAppMatrix
            if (getdtDetails.Tables[3].Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Tables[3].Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    idTRInvoiceApprMatrix.Visible = true;
                }
            }

            //CheckIsReportShow_VSCB_UploadedPOWO - VSCB_UplaodedPOWO
            idTRACCPOWO.Visible = false;
            if (getdtDetails.Tables[4].Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Tables[4].Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    idTRACCPOWO.Visible = true;
                }
            }

            //CheckIsReportShow_VSCB_Reports - VSCBAuditTrailReport

            idTR_Reports_1.Visible = false;
            idTR_Reports_2.Visible = false;
            idTR_Reports_3.Visible = false;
            idTR_Reports_4.Visible = false;
            IdTR_Reports_5.Visible = false;
            if (getdtDetails.Tables[5].Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Tables[5].Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    idTR_Reports_1.Visible = true;
                    idTR_Reports_2.Visible = true;
                    idTR_Reports_3.Visible = true;
                    idTR_Reports_4.Visible = true;
                    IdTR_Reports_5.Visible = true;
                }
                //CheckIsReportApprovalStatus_VSCB_Reports();

                //getdtDetails = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
                if (getdtDetails.Tables[6].Rows.Count > 0)
                {
                    idTR_Reports_1.Visible = true;
                    idTR_Reports_4.Visible = true;
                    IdTR_Reports_5.Visible = true;
                }

                if (getdtDetails.Tables[7].Rows.Count > 0)
                {
                    idTR_Reports_1.Visible = true;
                    idTR_Reports_4.Visible = true;
                    IdTR_Reports_5.Visible = true;
                    lnk_PaymentApprovalStatusreport.Visible = true;
                }
                if (hflEmpCode.Value == "99999999")
                {
                    idTR_Reports_1.Visible = true;
                    idTR_Reports_4.Visible = true;
                    IdTR_Reports_5.Visible = true;
                    lnk_PaymentApprovalStatusreport.Visible = true;
                }
            }

            //CheckIsReportShow_UploadCutoffData
            idTRUploadCutoffdata.Visible = false;

            if (getdtDetails.Tables[8].Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Tables[8].Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    idTRUploadCutoffdata.Visible = true;
                }
            }

            //Show Invoice/Payment Request create and PO/ WO Approval Matrix
            if (getdtDetails.Tables[9].Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Tables[9].Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    Lnk_Invoice_Payment_Maker.Visible = true;
                    //Lnk_POWO_Other_Approval.Visible = true;
                }
            }

            //CheckIsVSCBAmountPaidByAccountChange - VSCB_ACCPIADAMTCHANGE
            if (getdtDetails.Tables[10].Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Tables[10].Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    TDid_ChangeInvoiceAmt.Visible = true; 
                }
            }

        }
        catch (Exception)
        {
            // return false;
        }
    }

    public void Check_For_InvoiceTDSAmountChange()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShow_Reports";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "VSCB_AllowedTOChange_InvoiceTDS_Amt";

            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_VSCB_GETALL_DETAILS");

            TDid_ChangeInvoiceAmt.Visible = false;
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    TDid_ChangeInvoiceAmt.Visible = true;
                } 
            } 
        }
        catch (Exception)
        {
            // return false;
        }
    }


    public void Get_POWO_Approval_Status_Report()
    {
        try
        {
            DataTable dtEmpDetails = new DataTable();
            dtEmpDetails = spm.dtPOWoCreate("Select_POWO_Approval_Access", Convert.ToString(hflEmpCode.Value));
            if (dtEmpDetails.Rows.Count > 0)
            {
               
                idTR_Reports_1.Visible = true;
                Tr_POWO_Report.Visible = true;
                lnk_POWO_Approval_Status_Report.Visible = true;
            }

            if (Convert.ToString(hflEmpCode.Value).Trim()=="99999999")
            {
                idTR_Reports_1.Visible = true;
                Tr_POWO_Report.Visible = true;
                lnk_POWO_Approval_Status_Report.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

            throw;
        }
    }

    public void CheckIsVSCBAmountPaidByAccountChange()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShow_Reports";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "VSCB_ACCPIADAMTCHANGE";

            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_VSCB_GETALL_DETAILS");
            TrACCPIADAMTCHANGE.Visible = false;
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    TrACCPIADAMTCHANGE.Visible = true;
                    idTR_Reports_1.Visible = true;
                }
            }
            // return false;
        }
        catch (Exception ex)
        {
            // return false;
        }
    }

    public void CheckIsReportShow_VSCB_InvoiceApprovalMatrix()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShow_InvoiceAppMatrix";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value =Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "VSCB_InvoiceAppMatrix";
             


            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_VSCB_GETALL_DETAILS");
            idTRInvoiceApprMatrix.Visible = false;
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    idTRInvoiceApprMatrix.Visible = true;
                }

            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }

    public void CheckIsReportShow_VSCB_DeletePOWO()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShow_DeletePOWOList";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "DeletePOWOList";
            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_VSCB_GETALL_DETAILS");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    lnk_ApprovedPOWOList.Visible = true;
                }

            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }

    public void CheckIsReportShow_VSCB_VendorCreate()
    {
        DataSet getdtDetails = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShow_CreateVendor";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "VSCB_CreateVendor";
            getdtDetails = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            if (getdtDetails.Tables[0].Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Tables[0].Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    Lnk_Createvendor.Visible = true;
                }
            }
            if (getdtDetails.Tables[1].Rows.Count > 0)
            {
                Lnk_Createvendor.Visible = true;
            }
        }
        catch (Exception)
        {

        }
    }


    public void CheckIsReportShow_VSCB_CreateInvoicePaymentMaker()
    {
        //var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_DH_HOD_for_CreateInvoice_Payment_Creator";//"get_InvoicePaymentmakerCreate_dropdown_List";
           // spars[0].Value = "CheckIsShow_CreateVendor";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "VSCB_CreateInvoicePaymentMaker";
            // getdtDetails = spm.getTeamReportAllDDL(spars, "SP_VSCB_GETALL_DETAILS");
            DataSet getdtDetails = new DataSet();
            getdtDetails= spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
            if (getdtDetails.Tables[0].Rows.Count > 0)
            {
                Lnk_Invoice_Payment_Maker.Visible = true;
                Lnk_POWO_Other_Approval.Visible = true;

                //var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                //if (getStatus == "SHOW")
                //{
                //    Lnk_Invoice_Payment_Maker.Visible = true;
                //}
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }


    public void CheckIsReportShow_VSCB_UploadedPOWO()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShow_InvoiceAppMatrix";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "VSCB_UplaodedPOWO"; 

            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_VSCB_GETALL_DETAILS");
            idTRACCPOWO.Visible = false;
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    idTRACCPOWO.Visible = true;
                }

            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }

    public void CheckIsReportShow_VSCB_Reports()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShow_Reports";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "VSCBAuditTrailReport";

            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_VSCB_GETALL_DETAILS");
            idTR_Reports_1.Visible = false;
            idTR_Reports_2.Visible = false;
            idTR_Reports_3.Visible = false;
            idTR_Reports_4.Visible = false;
            IdTR_Reports_5.Visible = false;
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    idTR_Reports_1.Visible = true;
                    idTR_Reports_2.Visible = true;
                    idTR_Reports_3.Visible = true;
                    idTR_Reports_4.Visible = true;
                    IdTR_Reports_5.Visible = true;
                }
                CheckIsReportApprovalStatus_VSCB_Reports();
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }

    public void CheckIsReportApprovalStatus_VSCB_Reports()
    {
        DataSet getdtDetails = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_POWOApprovalStatusReport";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            getdtDetails = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
            if (getdtDetails.Tables[0].Rows.Count > 0)
            {
                idTR_Reports_1.Visible = true;
                idTR_Reports_4.Visible = true;
                IdTR_Reports_5.Visible = true;
            }

            if (getdtDetails.Tables[1].Rows.Count > 0)
            {
                idTR_Reports_1.Visible = true;
                idTR_Reports_4.Visible = true;
                IdTR_Reports_5.Visible = true;
                lnk_PaymentApprovalStatusreport.Visible = true;
            }
            if (hflEmpCode.Value == "99999999")
            {
                idTR_Reports_1.Visible = true;
                idTR_Reports_4.Visible = true;
                IdTR_Reports_5.Visible = true;
                lnk_PaymentApprovalStatusreport.Visible = true;
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }

    public void CheckIsReportShow_UploadCutoffData()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShow_cutoffdata";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "VSCB_UploadCutoffdata";

            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_VSCB_GETALL_DETAILS");
            idTRUploadCutoffdata.Visible = false;
           
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    idTRUploadCutoffdata.Visible = true;  
                }

            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }
    private void getMngInvoiceReqstCount()
	{
		try
		{
			DataTable dtTravelRequest = new DataTable();
			dtTravelRequest = spm.getPendingInvoiceCount(hflEmpCode.Value);
			if (dtTravelRequest.Rows.Count > 0)
			{
				lnk_summary_report.Text = "Inbox Invoices (" + dtTravelRequest.Rows[0]["NoofInvoices"] + ")";
			}
		}
		catch (Exception ex)
		{

		}
	}

    private void getMngPOWOReqstCount()
    {
        try
        {
            DataTable dtTravelRequest = new DataTable();
            dtTravelRequest = spm.getPendingPOWOCount(hflEmpCode.Value);
            if (dtTravelRequest.Rows.Count > 0)
            {
                lnk_POWOInbox.Text = "Inbox PO/ WO (" + dtTravelRequest.Rows[0]["NoofPOWOs"] + ")";
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void GetCreatePOWO()
	{
		try
		{
			DataTable dtEmpDetails = new DataTable();
			dtEmpDetails = spm.dtPOWoCreate("CreatePOWOUser", Convert.ToString(hflEmpCode.Value));
			if (dtEmpDetails.Rows.Count > 0)
			{
				lnk_leaverequest.Visible = true;
				lnk_mng_leaverequest.Visible = true;
				lnk_leaveinbox.Visible = true;
				lnk_reimbursmentReport.Visible = true;
				lnk_MobACC.Visible = true;
				lnk_reimbursmentReport_1.Visible = true;

			}

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());

			throw;
		}
	}

	public void GetCreatePOWOApproval()
	{
		try
		{
			DataTable dtEmpDetails = new DataTable();
			dtEmpDetails = spm.dtPOWoCreate("POWOUserApproval", Convert.ToString(hflEmpCode.Value));
			if (dtEmpDetails.Rows.Count > 0)
			{
				span_App_head.Visible = true;
				lnk_summary_report.Visible = true;
				lnk_Inbox_Payment_Request.Visible = true;
				hr_App_head.Visible = true;
			}

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());

			throw;
		}
	}
	public void GetCreatePOWO_Account_Approval()
	{
		try
		{
			DataTable dtEmpDetails = new DataTable();
			dtEmpDetails = spm.dtPOWoCreate("POWOUser_Account_Approval", Convert.ToString(hflEmpCode.Value));
			if (dtEmpDetails.Rows.Count > 0)
			{
				span_Account_App.Visible = true;
				hr_Acc_head.Visible = true;
				lnk_CustomerFirstReport.Visible = true;
				lnk_CustomerFirstView.Visible = true;

				lnk_Index_Acc_Invoices.Visible = true;
				lnk_Index_Acc_Payment_Requests.Visible = true;
				lnk_Index_Acc_Batch_Approval.Visible = true;
				lnk_Index_Acc_Batch_Requests.Visible = true;
			}

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());

			throw;
		}
	}

	protected void GetPaymentRequestPendingCount()
	{
		try
		{
			int PaymentRequestCount = 0;
			PaymentRequestCount = spm.GetPaymentRequest_Pending_InboxList_Count("Get_PaymentRequest_Pending_Approver_cnt", Convert.ToString(hflEmpCode.Value).Trim());
			lnk_Inbox_Payment_Request.Text = "Inbox Payment Request :(" + PaymentRequestCount.ToString() + ")";
		}
		catch (Exception)
		{

			throw;
		}
	}
	protected void GetPartialPaymentPendingCount()
	{
		try
		{
			int PaymentRequestCount = 0;
			PaymentRequestCount = spm.GetPaymentRequest_Pending_InboxList_Count("Get_PartialPayment_Pending_Approver_cnt", Convert.ToString(hflEmpCode.Value).Trim());
			lnk_CustomerFirstReport.Text = "Inbox Partial Payment Requests:(" + PaymentRequestCount.ToString() + ")";
		}
		catch (Exception)
		{

			throw;
		}

	}

	private void getMng_PendingBatchReqstCount()
	{
		try
		{
			DataTable dtTravelRequest = new DataTable();
			dtTravelRequest = spm.getPendingBatchReqList(hflEmpCode.Value);
			if (dtTravelRequest.Rows.Count > 0)
			{
				lnk_Index_Acc_Batch_Requests.Text = "Inbox Batch Requests (" + dtTravelRequest.Rows.Count + ")";
			}
		}
		catch (Exception ex)
		{

		}
	}

    private void getMng_PendingBatchReqstCount_BankRef()
    {
        try
        {
            DataTable dtTravelRequest = new DataTable();
            dtTravelRequest = spm.getPendingBatchReqList_BankRef(hflEmpCode.Value);
            if (dtTravelRequest.Rows.Count > 0)
            {
                lnkAttachBatch_BankPaymentRef.Text = "Attach Bank Ref to Batch for Approval (" + dtTravelRequest.Rows.Count + ")";
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void check_LoginEmployee_InvoiceCreateInboxpaymentReq()
    {
        try
        {
            DataSet dsList = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Check_InvoicePR_forLoginEmployee";
            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value).Trim();
            dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
            if (dsList != null)
            {
                if (dsList.Tables.Count > 0)
                {
                    if (dsList.Tables[0].Rows.Count > 0)
                    {
                       // IdTRCreInvoicePRequest.Visible = true;
                        Lnk_PaymentRequestAll.Visible = true;
                        lnk_reimbursmentReport_1.Visible = true;
                        if (dsList.Tables[0].Rows.Count > 0)
                        {
                            int strCount = dsList.Tables[1].Rows.Count;
                            Lnk_PaymentRequestAll.Text = "Create Payment Requests (" + strCount + ")";
                        }
                            
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void check_LoginEmployee_Role_forVendorBilling_process()
    {
        try
        {
            DataSet dsList = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Check_Index_forLoginEmployee";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value).Trim();

            dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            if(dsList !=null)
            {
                if(dsList.Tables.Count>0)
                {
                    //set visibility for Create PO/WO and Payment Req.with POWO
                    if(dsList.Tables[0].Rows.Count>0)
                    {
                        idTRPOWO_Create.Visible = true;
                      //  idTRPaymentReqWithPOWO.Visible = true; comment code from  harshad
                         
                    }

                    //set visibility for Inbox PO/WO and PO/WO Approved List
                    if (dsList.Tables[1].Rows.Count > 0)
                    {
                        idTRApproverHead.Visible = true;
                        idTRApproverHead_Line.Visible = true;
                        idTRPOWOApprover_inbox.Visible = true;
                    }

                    //set visibility for Invoice Inbox and Approved Invocie List
                    if (dsList.Tables[2].Rows.Count > 0)
                    {
                        idTRApproverHead.Visible = true;
                        idTRApproverHead_Line.Visible = true;
                        idTRInvoiceApprover_inbox.Visible = true;
                    }

                    //set visibility for Payment Req Inbox and Approved Payment Req List
                    if (dsList.Tables[3].Rows.Count > 0)
                    {
                        idTRApproverHead.Visible = true;
                        idTRApproverHead_Line.Visible = true;
                        idTRPaymentReqApprover_inbox.Visible = true;
                    }

                    //set visibility for Partial Payment Req Inbox 
                    if (dsList.Tables[4].Rows.Count > 0)
                    {
                        if (Convert.ToString(dsList.Tables[4].Rows[0]["chkAccount"]).Trim() == "yes")
                        {
                            idTRAccountHead.Visible = true;
                            idTRAccountHead_Line.Visible = true;
                            idTRPartialPay_Inbox.Visible = true;
                            //idTRAcc_Invoice_Inbox.Visible = true;
                        }
                    }

                    //set visibility for  Create Batch
                    if (dsList.Tables[5].Rows.Count > 0)
                    {
                        idTRApproverHead.Visible = true;
                        idTRApproverHead_Line.Visible = true;
                        idTRBatch_Create.Visible = true;                     
                        idTRAtachBatch_BankRef.Visible = true;
                        Tr_Payment_Report.Visible = true;
                    }

                    //set visibility for  Batch Req.Approver
                    if (dsList.Tables[6].Rows.Count > 0)
                    {
                        idTRApproverHead.Visible = true;
                        idTRApproverHead_Line.Visible = true;
                        idTRBatch_Req_Approver.Visible = true;
                    }

                     
                }
            }
        }
        catch(Exception ex)
        {

        }
    }

    protected void lnk_Index_Acc_Batch_Approval_Click(object sender, EventArgs e)
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "delete_BatchRequest_Temp";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(hflEmpCode.Value).Trim();

        dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        Response.Redirect("VSCB_CreateBatch.aspx");
    }

    private void CheckAprovedBatchDetails()
    {
        try
        {
            DataSet dsList = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "getCheckAprovedBatchDetails";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value).Trim();

            dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            if (dsList != null)
            {
                if (dsList.Tables[0].Rows.Count > 0)
                {
                    trViewApprovedBatch.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void CheckIsReportShow_VSCB_CreateProduct()
    {
        DataSet getdtDetails = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShow_Createproduct";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "VSCB_CreateProduct";
            getdtDetails = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            if (getdtDetails.Tables[0].Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Tables[0].Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    TrProduct.Visible = true;
                    lnkBtnCreateproduct.Visible = true;
                }
            }

        }
        catch (Exception)
        {

        }
    }







}