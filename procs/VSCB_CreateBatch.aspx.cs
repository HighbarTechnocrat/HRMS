using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSCB_CreateBatch : System.Web.UI.Page
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
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    PopulateEmployeeData();

                    hdnVendorBankDetails.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBVendorFiles"]).Trim());
                    if (Request.QueryString.Count>0)
                    {
                        hdnPOWOID.Value = Convert.ToString(Request.QueryString["batchid"]).Trim();
                        ulSearch.Visible = false;
                        lblheading.Text = "View Payment Batch";
                        liApprover2.Visible = false;
                        liApprover1.Visible = false;
                        lichecker.Visible = false;
                        btnCancel.Visible = false;
                        get_CreatBatch_PaymentReq_DropdownList();
                        get_Batch_details();
                        idBankRef_1.Visible = true;
                        idBankRef_2.Visible = true;
                        idBankRef_3.Visible = true;

                        if (txtBankRefNo.Text.Trim() == "")
                        {
                            btnCancel.Visible = true;
                            btnCancel.Text = "Cancel Batch";
                            if (hdStatusIDcheck.Value == "4")
                            {
                                btnCancel.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        get_CreatBatch_PaymentReq_DropdownList();                        
                        getMngInvoiceReqstList();
                        get_Added_PaymentReqList_CreateBatch("","","","","","");
                    }
                    

                    get_Batch_Maker_details();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    {

        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnInvoiceId.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
        hdnMilestoneId.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
        hdnPOWOID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();

        //Response.Redirect("VSCB_ApprovedInvoice_View.aspx?invid=" + hdnInvoiceId.Value + "&batchid=" + hdnBatchId.Value + "&mngexp=2");
        
        if (Convert.ToString(hdnMilestoneId.Value).Trim() == "1") 
            Response.Redirect("VSCB_ApprovePaymentRequest_View.aspx?Payment_ID=" + hdnInvoiceId.Value + "&POID=" + hdnPOWOID.Value + "&type=c");
        

        if (Convert.ToString(hdnMilestoneId.Value).Trim() == "2")
            Response.Redirect("Mobile_Req_Arch.aspx?rem_id=" + hdnInvoiceId.Value + "&mngexp=0&inbtype=RACC&batch=c");

        if (Convert.ToString(hdnMilestoneId.Value).Trim() == "3")
            Response.Redirect("Fuel_Req_Arch.aspx?rem_id=" + hdnInvoiceId.Value + "&mngexp=0&inbtype=RACC&batch=c");

        if (Convert.ToString(hdnMilestoneId.Value).Trim() == "4")
            Response.Redirect("Payment_Req_Arch.aspx?rem_id=" + hdnInvoiceId.Value + "&mngexp=0&inbtype=RACC&batch=c");

        if(Convert.ToString(hdnMilestoneId.Value).Trim() == "5")
            Response.Redirect("ApprovedTravelReqst_Acc.aspx?expid=" + hdnInvoiceId.Value + "&mngexp=0&stype=ACC&batch=c");


        if (Convert.ToString(hdnMilestoneId.Value).Trim() == "6" || Convert.ToString(hdnMilestoneId.Value).Trim() == "7")
            Response.Redirect("VSCB_My_Adv_PayRequestView.aspx?Payment_ID=" + hdnInvoiceId.Value + "&POID=" + hdnPOWOID.Value + "&batch=c");

    }

    #endregion

    #region Page Methods
    private void getMngInvoiceReqstList()
    {
        try
        {
            DataSet dsList = new DataSet();
            SqlParameter[] spars = new SqlParameter[12];

            string[] strdate;
            string strPODate = "";
            if (Convert.ToString(lstPOWODate.SelectedValue).Trim() != "0")
            {
                strdate = Convert.ToString(lstPOWODate.SelectedValue).Trim().Split('-');
                strPODate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }

            string strPaymentDateDate = "";
            if (Convert.ToString(lstpaymentRequestDate.SelectedValue).Trim() != "0")
            {
                strdate = Convert.ToString(lstpaymentRequestDate.SelectedValue).Trim().Split('-');
                strPaymentDateDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }


            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Paymentreq_Createbatch_List";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

           spars[2] = new SqlParameter("@POID", SqlDbType.BigInt);
            if (Convert.ToString(lstPOWONo.SelectedValue).Trim() != "0")
                spars[2].Value = Convert.ToDouble(lstPOWONo.SelectedValue);
            else
                spars[2].Value = DBNull.Value;

            spars[3] = new SqlParameter("@PODate", SqlDbType.VarChar);
            if (Convert.ToString(strPODate).Trim() != "")
                spars[3].Value = Convert.ToString(strPODate).Trim();
            else
                spars[3].Value = DBNull.Value;

            spars[4] = new SqlParameter("@VendorName", SqlDbType.VarChar);
            if (Convert.ToString(lstVendorName.SelectedValue).Trim() != "0")
                spars[4].Value = Convert.ToString(lstVendorName.SelectedItem.Text).Trim();
            else
                spars[4].Value = DBNull.Value;

            spars[5] = new SqlParameter("@InvoiceID", SqlDbType.BigInt);
            if (Convert.ToString(lstInvoiceNo.SelectedValue).Trim() != "0")
                spars[5].Value = Convert.ToDouble(lstInvoiceNo.SelectedValue);
            else
                spars[5].Value = DBNull.Value;
             

            spars[6] = new SqlParameter("@PaymentReqNo", SqlDbType.BigInt);
            if (Convert.ToString(lstPaymentRequestNo.SelectedValue).Trim() != "0")
                spars[6].Value = Convert.ToDouble(lstPaymentRequestNo.SelectedValue);
            else
                spars[6].Value = DBNull.Value;

            spars[7] = new SqlParameter("@PaymentReqDate", SqlDbType.VarChar);
            if (Convert.ToString(strPaymentDateDate).Trim() != "")
                spars[7].Value = Convert.ToString(strPaymentDateDate).Trim();
            else
                spars[7].Value = DBNull.Value;

            spars[8] = new SqlParameter("@PaymentReqAmt", SqlDbType.Decimal);
            if (Convert.ToString(txtPaymentRequestamt.Text).Trim() != "")
                spars[8].Value = Convert.ToDecimal(txtPaymentRequestamt.Text);
            else
                spars[8].Value = DBNull.Value;

            spars[9] = new SqlParameter("@StatuId", SqlDbType.Int);
            if (Convert.ToString(lstStatus.SelectedValue).Trim() != "0")
                spars[9].Value = Convert.ToInt32(lstStatus.SelectedValue);
            else
                spars[9].Value = DBNull.Value;

            spars[10] = new SqlParameter("@Prj_Dept_id", SqlDbType.Int);
            if (Convert.ToString(lstDepartment.SelectedValue).Trim() != "0")
                spars[10].Value = Convert.ToInt32(lstDepartment.SelectedValue);
            else
                spars[10].Value = DBNull.Value;



            spars[11] = new SqlParameter("@POTypeID", SqlDbType.Int);
            if (Convert.ToString(lstPaymentRequestType.SelectedValue).Trim() != "0")
                spars[11].Value = Convert.ToInt32(lstPaymentRequestType.SelectedValue);
            else
                spars[11].Value = DBNull.Value;
             




            dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            gvMngTravelRqstList.DataSource = null;
            gvMngTravelRqstList.DataBind();
            trvl_btnSave.Visible = false;
            if (dsList.Tables[0].Rows.Count > 0)
            {
                gvMngTravelRqstList.DataSource = dsList.Tables[0];
                gvMngTravelRqstList.DataBind();
                trvl_btnSave.Visible = true;
            }
 

        }
        catch (Exception ex)
        {

        }
    }

    private void get_Added_PaymentReqList_CreateBatch(string spaymentIds, string spaymentIds_Mobile, string spaymentIds_Fuel, string spaymentIds_Voucher,string strPaymentId_Travel, string strPaymentId_Advance_SD)
    {
        try
        {
            DataSet dsList = new DataSet();
            SqlParameter[] spars = new SqlParameter[8];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            if (Convert.ToString(spaymentIds).Trim() != "" || Convert.ToString(spaymentIds_Mobile).Trim() != "" || Convert.ToString(spaymentIds_Fuel).Trim() != "" || Convert.ToString(spaymentIds_Voucher).Trim() != "" || Convert.ToString(strPaymentId_Travel).Trim() != ""  || Convert.ToString(strPaymentId_Advance_SD).Trim() != "")
                spars[0].Value = "get_Paymentreq_Added_Createbatch";
            else
                spars[0].Value = "get_Payments_List_FromTemptbl";

            spars[1] = new SqlParameter("@spaymentId", SqlDbType.VarChar);
            if (Convert.ToString(spaymentIds).Trim() != "")
                spars[1].Value = spaymentIds;
            else
                spars[1].Value = DBNull.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            spars[3] = new SqlParameter("@spaymentId_Mobile", SqlDbType.VarChar);
            if (Convert.ToString(spaymentIds_Mobile).Trim() != "")
                spars[3].Value = spaymentIds_Mobile;
            else
                spars[3].Value = DBNull.Value;

            spars[4] = new SqlParameter("@spaymentId_Fuel", SqlDbType.VarChar);
            if (Convert.ToString(spaymentIds_Fuel).Trim() != "")
                spars[4].Value = spaymentIds_Fuel;
            else
                spars[4].Value = DBNull.Value;

            spars[5] = new SqlParameter("@spaymentId_Voucher", SqlDbType.VarChar);
            if (Convert.ToString(spaymentIds_Voucher).Trim() != "")
                spars[5].Value = spaymentIds_Voucher;
            else
                spars[5].Value = DBNull.Value;

            spars[6] = new SqlParameter("@spaymentId_Travel", SqlDbType.VarChar);
            if (Convert.ToString(strPaymentId_Travel).Trim() != "")
                spars[6].Value = strPaymentId_Travel;
            else
                spars[6].Value = DBNull.Value;

            spars[7] = new SqlParameter("@spaymentId_Advance", SqlDbType.VarChar);
            if (Convert.ToString(strPaymentId_Advance_SD).Trim() != "")
                spars[7].Value = strPaymentId_Advance_SD;
            else
                spars[7].Value = DBNull.Value;

             
            dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            gvMngPaymentList_Batch.DataSource = null;
            gvMngPaymentList_Batch.DataBind();
            txtbatchTotalPayment.Text = "";
            txtbatchNoOfRequest.Text = "";
            if (dsList.Tables[0].Rows.Count > 0)
            {
                gvMngPaymentList_Batch.DataSource = dsList.Tables[0];
                gvMngPaymentList_Batch.DataBind();

                txtbatchTotalPayment.Text =Convert.ToString(dsList.Tables[0].Rows[0]["TotalPaymentAmt"]).Trim();
                txtbatchNoOfRequest.Text = Convert.ToString(dsList.Tables[0].Rows.Count);
            }

            get_CreatBatch_PaymentReq_DropdownList();
            getMngInvoiceReqstList();

        }
        catch (Exception ex)
        {

        }
    }


    private string get_vendorBankName(string strVedorName)
    {
        string vendorBankName = "";
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_VendorBank_Name";

        spars[1] = new SqlParameter("@costcenter", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(strVedorName).Trim();

        dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        if(dsList !=null)
        {
            if (dsList.Tables[0].Rows.Count > 0)
            {
               vendorBankName = Convert.ToString(dsList.Tables[0].Rows[0]["Name"]).Trim();
            }
        }
       return vendorBankName;
    }


    private void get_CreatBatch_PaymentReq_DropdownList()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Createbatch_Dropdown_List";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

        dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        if (dsList.Tables[0].Rows.Count > 0)
        {
            lstPOWONo.DataSource = dsList.Tables[0];
            lstPOWONo.DataTextField = "PONumber";
            lstPOWONo.DataValueField = "POID";
            lstPOWONo.DataBind();
        }
        lstPOWONo.Items.Insert(0, new ListItem("Select PO/WO Number", "0"));


        if (dsList.Tables[1].Rows.Count > 0)
        {
            lstPOWODate.DataSource = dsList.Tables[1];
            lstPOWODate.DataTextField = "PODate";
            lstPOWODate.DataValueField = "PODate";
            lstPOWODate.DataBind();
        }
        lstPOWODate.Items.Insert(0, new ListItem("Select PO/WO Date", "0"));


        if (dsList.Tables[2].Rows.Count > 0)
        {
            lstVendorName.DataSource = dsList.Tables[2];
            lstVendorName.DataTextField = "Name";
            lstVendorName.DataValueField = "Name";
            lstVendorName.DataBind();
        }
        lstVendorName.Items.Insert(0, new ListItem("Select Vendor Name", "0"));

        if (dsList.Tables[3].Rows.Count > 0)
        {
            lstInvoiceNo.DataSource = dsList.Tables[3];
            lstInvoiceNo.DataTextField = "InvoiceNo";
            lstInvoiceNo.DataValueField = "InvoiceID";
            lstInvoiceNo.DataBind();
        }
        lstInvoiceNo.Items.Insert(0, new ListItem("Select Invoice No", "0"));

        if (dsList.Tables[4].Rows.Count > 0)
        {
            lstPaymentRequestNo.DataSource = dsList.Tables[4];
            lstPaymentRequestNo.DataTextField = "PaymentReqNo";
            lstPaymentRequestNo.DataValueField = "Payment_ID";
            lstPaymentRequestNo.DataBind();
        }
        lstPaymentRequestNo.Items.Insert(0, new ListItem("Select Payment Request No", "0"));

        if (dsList.Tables[5].Rows.Count > 0)
        {
            lstpaymentRequestDate.DataSource = dsList.Tables[5];
            lstpaymentRequestDate.DataTextField = "PaymentReqDate";
            lstpaymentRequestDate.DataValueField = "PaymentReqDate";
            lstpaymentRequestDate.DataBind();
        }
        lstpaymentRequestDate.Items.Insert(0, new ListItem("Select Payment Request Date", "0"));

        if (dsList.Tables[6].Rows.Count > 0)
        {
            lstStatus.DataSource = dsList.Tables[6];
            lstStatus.DataTextField = "PyamentStatus";
            lstStatus.DataValueField = "PaymentStatusID";
            lstStatus.DataBind();
        }
        lstStatus.Items.Insert(0, new ListItem("Select Payment Request Status", "0"));


        txtbatchCreateDate.Text = Convert.ToString(dsList.Tables[7].Rows[0]["batchdate"]).Trim();


        if (dsList.Tables[8].Rows.Count > 0)
        {
            lstChecker.DataSource = dsList.Tables[8];
            lstChecker.DataTextField = "Emp_Name";
            lstChecker.DataValueField = "Mekar_Empcode";
            lstChecker.DataBind();
        }
        lstChecker.Items.Insert(0, new ListItem("Select Checker", "0"));

        if (dsList.Tables[9].Rows.Count > 0)
        {
            lstApprover1.DataSource = dsList.Tables[9];
            lstApprover1.DataTextField = "Emp_Name";
            lstApprover1.DataValueField = "Approver_Empcode";
            lstApprover1.DataBind();
        }
        lstApprover1.Items.Insert(0, new ListItem("Select Approver 1", "0"));

        if (dsList.Tables[10].Rows.Count > 0)
        {
            lstApprover2.DataSource = dsList.Tables[10];
            lstApprover2.DataTextField = "Emp_Name";
            lstApprover2.DataValueField = "Approver_Empcode";
            lstApprover2.DataBind();
        }
        lstApprover2.Items.Insert(0, new ListItem("Select Approver 2", "0"));

        if (dsList.Tables[11].Rows.Count > 0)
        {
            lstDepartment.DataSource = dsList.Tables[11];
            lstDepartment.DataTextField = "Department";
            lstDepartment.DataValueField = "Dept_ID";
            lstDepartment.DataBind();
        }
        lstDepartment.Items.Insert(0, new ListItem("Select Project/Department", "0"));

        if (dsList.Tables[12].Rows.Count > 0)
        {
            lstBanksList.DataSource = dsList.Tables[12];
            lstBanksList.DataTextField = "Bank_Name";
            lstBanksList.DataValueField = "Bank_ID";
            lstBanksList.DataBind();
        }
        lstBanksList.Items.Insert(0, new ListItem("Select Bank Name", "0"));

        if (dsList.Tables[13].Rows.Count > 0)
        {
            lstPaymentRequestType.DataSource = dsList.Tables[13];
            lstPaymentRequestType.DataTextField = "paymentType";
            lstPaymentRequestType.DataValueField = "paymentType_id";
            lstPaymentRequestType.DataBind();
        }
        lstPaymentRequestType.Items.Insert(0, new ListItem("Select Payment Type ", "0"));

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
                txtbatchCreatedBy.Text= (string)dtEmp.Rows[0]["Emp_Name"];
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

    public void get_Batch_details()
    {
        try
        {
            DataSet dsList = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_myBatch_Details";

            spars[1] = new SqlParameter("@POWOID", SqlDbType.BigInt);
            spars[1].Value = Convert.ToDouble(hdnPOWOID.Value);
            
            dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
            gvMngPaymentList_Batch.DataSource = null;
            gvMngPaymentList_Batch.DataBind();
            DgvApprover.DataSource = null;
            DgvApprover.DataBind();
            if (dsList.Tables[0].Rows.Count > 0)
            {
                hdStatusIDcheck.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Status_Id"]).Trim();
                txtbatchCreateDate.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_Date"]).Trim();
                txtbatchCreatedBy.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Name"]).Trim();
                txtbatchNo.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_No"]).Trim();
                txtbatchNoOfRequest.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_No_Requests"]).Trim();
                txtbatchTotalPayment.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_Total_Payament"]).Trim();
                lstBanksList.SelectedValue = Convert.ToString(dsList.Tables[0].Rows[0]["HBT_BankID"]);
                txtBankRefNo.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Ref_no"]).Trim();
                txtBankRefDate.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Ref_Date"]).Trim();
                txtBankRef_Link.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Ref_Link"]).Trim();
                lnkBank.InnerText = "Please click here for Bank Login";
                lnkBank.HRef = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Ref_Link"]).Trim();
                lstBanksList.Enabled = false;
                txtbatchNo.Visible = true;
                spnBatchNo.Visible = true;
                if (dsList.Tables[1].Rows.Count > 0)
                { 
                    gvMngPaymentList_Batch.DataSource = dsList.Tables[1];
                    gvMngPaymentList_Batch.DataBind();
                     
                    gvMngPaymentList_Batch.Columns[0].Visible = false;
                    gvMngPaymentList_Batch.Visible = true;
                }

                if (dsList.Tables[2].Rows.Count > 0)
                {
                    DgvApprover.DataSource = dsList.Tables[2];
                    DgvApprover.DataBind();
                }

            }
         }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    public void get_Batch_Maker_details()
    {
        try
        {
            DataSet dsList = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Check_loginEmp_IsBatchMaker";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value =Convert.ToString(txtEmpCode.Text).Trim();

            dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
             
            if (dsList.Tables[0].Rows.Count == 0)
            {
                gvMngTravelRqstList.DataSource = null;
                gvMngTravelRqstList.DataBind();
                gvMngPaymentList_Batch.DataSource = null;
                gvMngPaymentList_Batch.DataBind();
                Ul1.Visible = false;
                trvl_btnSave.Visible = false;
                btnCancel.Visible = false;
                btnback_mng.Visible = false;


                btnCorrection.Visible = false;

                lstPOWONo.Items.Clear();
                lstPOWONo.Items.Insert(0, new ListItem("Select PO/WO No", "0"));

                lstVendorName.Items.Clear();
                lstVendorName.Items.Insert(0, new ListItem("Select Vendor Name", "0"));

                lstPaymentRequestNo.Items.Clear();
                lstPaymentRequestNo.Items.Insert(0, new ListItem("Select Payment Request No", "0"));

                lstpaymentRequestDate.Items.Clear();
                lstpaymentRequestDate.Items.Insert(0, new ListItem("Select Payment Request Date", "0"));

                lstStatus.Items.Clear();
                lstStatus.Items.Insert(0, new ListItem("Select Payment Request Status", "0"));

                lstChecker.Items.Clear();
                lstChecker.Items.Insert(0, new ListItem("Select Checker", "0"));

                lstApprover1.Items.Clear();
                lstApprover1.Items.Insert(0, new ListItem("Select Approver 1", "0"));

                lstApprover2.Items.Clear();
                lstApprover2.Items.Insert(0, new ListItem("Select Approver 2", "0"));



            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    #endregion 


    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        if (gvMngTravelRqstList.Rows.Count > 0)
        {
            int iTotalPaymentReqAdd = 0;
            string strPaymentId = "0";

            string strPaymentId_Mobile = "0";
            string strPaymentId_Fuel = "0";
            string strPaymentId_Voucher = "0";
            string strPaymentId_Travel = "0";
            string strPaymentId_Advance_SD = "0";

            string sVendorName = "";
            string sVendorName_Cur = "";
            foreach (GridViewRow row in gvMngTravelRqstList.Rows)
            {
                CheckBox chkAddPaymentRequest = (CheckBox)row.FindControl("chkAddPaymentRequest");
                
                if (chkAddPaymentRequest.Checked)
                {
                     
                    if (Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim() == "1")
                    {
                        string svbank = get_vendorBankName(Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[4]).Trim());
                        if (svbank != "")
                        {
                            if (Convert.ToString(sVendorName).Trim() == "")
                                sVendorName = svbank;
                            else
                                sVendorName = sVendorName + "<br />" + svbank;

                            chkAddPaymentRequest.Checked = false;
                        }
                    }

                    if (Convert.ToString(hdncurId.Value).Trim() == "")
                    {
                        hdncurId.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[5]).Trim();
                    }
                    else
                    {
                       // string sss = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[5]).Trim();

                        if (Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[5]).Trim() != Convert.ToString(hdncurId.Value))
                        {
                            if (Convert.ToString(sVendorName_Cur).Trim() == "")
                                sVendorName_Cur = Convert.ToString(gvMngTravelRqstList.Rows[row.RowIndex].Cells[3].Text).Trim();
                            else
                                sVendorName_Cur = sVendorName_Cur + "<br />" + Convert.ToString(gvMngTravelRqstList.Rows[row.RowIndex].Cells[3].Text).Trim();
                            chkAddPaymentRequest.Checked = false;
                        }
                    }


                }
            }

            if(Convert.ToString(sVendorName).Trim()!="")
            {
                lblmessage.Text = "below Vendor Banks details not found.so you can't create Payment Batch for this Vendor <br/>" + sVendorName;
                 
            }

            if (Convert.ToString(sVendorName_Cur).Trim() != "")
            {
                lblmessage.Text = "below Vendor having different currency .so you can't create Payment Batch for this Vendor <br/>" + sVendorName_Cur;

            }


            foreach (GridViewRow row in gvMngTravelRqstList.Rows)
            {
                CheckBox chkAddPaymentRequest = (CheckBox)row.FindControl("chkAddPaymentRequest");
                if(chkAddPaymentRequest.Checked)
                {
                    iTotalPaymentReqAdd += 1;
                    if (Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim() == "1")
                    {
                        strPaymentId = strPaymentId + "," + Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
                    }

                    if (Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim() == "2")
                    {
                        strPaymentId_Mobile = strPaymentId_Mobile + "," + Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
                    }

                    if (Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim() == "3")
                    {
                        strPaymentId_Fuel = strPaymentId_Fuel + "," + Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
                    }

                    if (Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim() == "4")
                    {
                        strPaymentId_Voucher = strPaymentId_Voucher + "," + Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
                    }
                    if (Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim() == "5")
                    {
                        strPaymentId_Travel = strPaymentId_Travel + "," + Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
                    }

                    if (Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim() == "6" || Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim() == "7")
                    {
                        strPaymentId_Advance_SD = strPaymentId_Advance_SD + "," + Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
                    }

                }
            }

            //if (iTotalPaymentReqAdd > 0)
            //{
                txtbatchNoOfRequest.Text = Convert.ToString(iTotalPaymentReqAdd).Trim();
                get_Added_PaymentReqList_CreateBatch(strPaymentId, strPaymentId_Mobile, strPaymentId_Fuel, strPaymentId_Voucher, strPaymentId_Travel, strPaymentId_Advance_SD);
             
            //}
        }
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {

    }

    protected void chkAddPaymentRequest_CheckedChanged(object sender, EventArgs e)
    {
       /* CheckBox btn = (CheckBox)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        CheckBox chkAddPaymentRequest = (CheckBox)row.FindControl("chkAddPaymentRequest");
        if(chkAddPaymentRequest.Checked)
        {
            hdnInvoiceId.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
            hdnMilestoneId.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
            hdnPOWOID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
        }*/
        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        Boolean isDraft = true;

        if (hdnPOWOID.Value != "")
        {
            DataSet DS = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CanceledBatch_Request";
            spars[1] = new SqlParameter("@Batch_ID", SqlDbType.BigInt);
            spars[1].Value = Convert.ToDouble(hdnPOWOID.Value);
            DS = spm.getDatasetList(spars, "SP_VSCB_CreateBatchRequest");
        }
        else
        {
            #region Batch Request Validation
            if (gvMngPaymentList_Batch.Rows.Count <= 0)
            {
                lblmessage.Text = "Please Add Approved Payment Request into batch";
                return;
            }
            if (Convert.ToString(txtbatchTotalPayment.Text).Trim() == "")
            {
                lblmessage.Text = "Please check Batch Total Payment";
                return;
            }
            if (Convert.ToString(txtbatchNoOfRequest.Text).Trim() == "")
            {
                lblmessage.Text = "Please check Batch no. of Requests";
                return;
            }

            if (Convert.ToString(lstChecker.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Checker Name";
                return;
            }
            if (Convert.ToString(lstBanksList.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Bank Name";
                return;
            }

            if (Convert.ToString(lstApprover1.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Approver1 Name";
                return;
            }
            if (Convert.ToString(lstApprover2.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Approver2 Name";
                return;
            }
            #endregion

            string[] strdate;
            string strBatchDate = "";

            string sapproverEmpCode2 = "";
            if (Convert.ToString(lstApprover2.SelectedValue).Trim() != "0")
            {
                sapproverEmpCode2 = Convert.ToString(lstApprover2.SelectedValue).Trim();
            }

            #region date formatting
            if (Convert.ToString(txtbatchCreateDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtbatchCreateDate.Text).Trim().Split('-');
                strBatchDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            #endregion

            double dMaxReqId = 0;
            string sInvoiceType = "CreateBatch_Request";
            //if (Convert.ToString(hdnInvoiceId.Value) == "0")
            //    sInvoiceType = "Insertinvoice";
            //else
            //    sInvoiceType = "Updateinvoice";

            DataSet dtMaxreqID = new DataSet();
            dtMaxreqID = spm.Create_PaymentBatchRequest(txtEmpCode.Text, sInvoiceType, strBatchDate, "btchno1", Convert.ToInt32(txtbatchNoOfRequest.Text), Convert.ToDecimal(txtbatchTotalPayment.Text), Convert.ToString(lstChecker.SelectedValue), Convert.ToString(lstApprover1.SelectedValue).Trim(), sapproverEmpCode2, false, 1, Convert.ToInt32(lstBanksList.SelectedValue), isDraft);

            dMaxReqId = Convert.ToDouble(dtMaxreqID.Tables[0].Rows[0]["MaxReqID"]);
            // spm.InsertBatchRequest_ApproverDetails("Insert_BatchReq_Approver", dMaxReqId, Convert.ToString(lstChecker.SelectedValue), 1, "Pending", "", "", "");

            double dInvoiceId = 0;
            double dpaymentId = 0;
            double dPOID = 0;
            Int32 isrno = 1;
            Int32 ipaymentTypeid = 1;

            foreach (GridViewRow row in gvMngPaymentList_Batch.Rows)
            {
                dInvoiceId = 0;
                dpaymentId = 0;
                dPOID = 0;

                if (Convert.ToString(gvMngPaymentList_Batch.DataKeys[row.RowIndex].Values[0]).Trim() != "")
                    dPOID = Convert.ToDouble(gvMngPaymentList_Batch.DataKeys[row.RowIndex].Values[0]);

                if (Convert.ToString(gvMngPaymentList_Batch.DataKeys[row.RowIndex].Values[2]).Trim() != "")
                    dpaymentId = Convert.ToDouble(gvMngPaymentList_Batch.DataKeys[row.RowIndex].Values[2]);

                if (Convert.ToString(gvMngPaymentList_Batch.DataKeys[row.RowIndex].Values[3]).Trim() != "")
                    dInvoiceId = Convert.ToDouble(gvMngPaymentList_Batch.DataKeys[row.RowIndex].Values[3]);

                if (Convert.ToString(gvMngPaymentList_Batch.DataKeys[row.RowIndex].Values[4]).Trim() != "")
                    ipaymentTypeid = Convert.ToInt32(gvMngPaymentList_Batch.DataKeys[row.RowIndex].Values[4]);

                spm.InsertBatchRequest_Details("Insert_BatchReq_Details", dMaxReqId, dInvoiceId, dpaymentId, isrno, dPOID, ipaymentTypeid);

            }

            StringBuilder sbBatchDetails = new StringBuilder();
            StringBuilder sbBatchRequest = new StringBuilder();
            StringBuilder sbBatchApprs = new StringBuilder();
            StringBuilder strbuildBodyMsg = new StringBuilder();

            string strcheckerEmail = "";


            #region get Batch Details 
            DataSet dsList = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_myBatch_Details";

            spars[1] = new SqlParameter("@POWOID", SqlDbType.BigInt);
            spars[1].Value = dMaxReqId;

            dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
            //gvMngPaymentList_Batch.DataSource = null;
            //gvMngPaymentList_Batch.DataBind();
            //DgvApprover.DataSource = null;
            //DgvApprover.DataBind();

            #endregion

            if (isDraft == false)
            {
                #region send Email to  Batch Approval
                string strInvoiceURL = "";
                string strsubject = "OneHR: Request for Payment Batch approval - Batch no." + Convert.ToString(dsList.Tables[0].Rows[0]["Batch_No"]).Trim();
                strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["BatchapproverLink_VSCB"]).Trim() + "?batchid=" + dMaxReqId).Trim();


                strbuildBodyMsg.Append("<table cellpadding='5' cellspacing='0' style='font-size: 9pt;font-family:Arial'>");
                strbuildBodyMsg.Append("<tr><td>Dear Sir </td></tr>");
                strbuildBodyMsg.Append("<tr><td style='height:15px'></td></tr>");
                strbuildBodyMsg.Append("<tr><td colspan='2'> We have generated Online Payment instructions for Vendor Payment through OneHr.</td></tr>");

                strbuildBodyMsg.Append("<tr><td style='height:15px'></td></tr>");
                strbuildBodyMsg.Append("<tr><td colspan='2'>Following are the details for your reference. </td></tr>");
                strbuildBodyMsg.Append("<tr><td style='height:15px'></td></tr>");

                sbBatchRequest.Append("<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial;width:100%;'>");
                sbBatchRequest.Append("<tr><th style='background-color: #B8DBFD;border: 1px solid #ccc'>Reference Number</th>");
                //sbBatchRequest.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>File Name</th>");
                sbBatchRequest.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Upload By</th>");
                sbBatchRequest.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Number of Transaction</th>");
                sbBatchRequest.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Amount</th></tr>");

                sbBatchRequest.Append("<tr><td style='width:20%;border: 1px solid #ccc'>" + Convert.ToString(dsList.Tables[0].Rows[0]["Batch_No"]).Trim() + "</td>");
                //sbBatchRequest.Append("<td style='width:20%;border: 1px solid #ccc'></td>");
                sbBatchRequest.Append("<td style='width:30%;border: 1px solid #ccc'>" + Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Name"]).Trim() + "</td>");
                sbBatchRequest.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(dsList.Tables[0].Rows[0]["Batch_No_Requests"]).Trim() + "</td>");
                sbBatchRequest.Append("<td style='width:20%;border: 1px solid #ccc'>" + Convert.ToString(dsList.Tables[0].Rows[0]["Batch_Total_Payament"]).Trim() + "</td></tr>");
                sbBatchRequest.Append("</table>");

                #region Create Batch Request table
                if (dsList.Tables[1].Rows.Count > 0)
                {
                    int sno = 1;
                    sbBatchDetails.Append("<br/><br/> <table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial;width:100%;'>");
                    sbBatchDetails.Append("<tr><th style='background-color: #B8DBFD;border: 1px solid #ccc'>Sr.No</th>");
                    sbBatchDetails.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Vendor Payment</th>");
                    sbBatchDetails.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Amt.Rs.</th>");
                    sbBatchDetails.Append("<th th style='background-color: #B8DBFD;border: 1px solid #ccc'>Details</th>");
                    sbBatchDetails.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Approved By</th>");
                    sbBatchDetails.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>Bank A/C No.</th>");
                    sbBatchDetails.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>IFSC Code</th></tr>");
                    for (Int32 irow = 0; irow < dsList.Tables[1].Rows.Count; irow++)
                    {
                        sbBatchDetails.Append("<tr>");
                        sbBatchDetails.Append("<td style='width:10%;border: 1px solid #ccc'>" + Convert.ToString(sno).Trim() + "</td>");
                        sbBatchDetails.Append("<td style='width:20%;border: 1px solid #ccc'>" + Convert.ToString(dsList.Tables[1].Rows[irow]["VendorName"]).Trim() + "</td>");
                        sbBatchDetails.Append("<td style='width:10%;border: 1px solid #ccc'>" + Convert.ToString(dsList.Tables[1].Rows[irow]["Amt_paid_Account"]).Trim() + "</td>");
                        sbBatchDetails.Append("<td style='width:20%;border: 1px solid #ccc'>" + Convert.ToString(dsList.Tables[1].Rows[irow]["MilestoneParticular"]).Trim() + "</td>");
                        sbBatchDetails.Append("<td style='width:20%;border: 1px solid #ccc'>" + Convert.ToString(dsList.Tables[1].Rows[irow]["PaymentApproverName"]).Trim() + "</td>");
                        sbBatchDetails.Append("<td style='width:10%;border: 1px solid #ccc'>" + Convert.ToString(dsList.Tables[1].Rows[irow]["Acc_no"]).Trim() + "</td>");
                        sbBatchDetails.Append("<td style='width:10%;border: 1px solid #ccc'>" + Convert.ToString(dsList.Tables[1].Rows[irow]["IFSC_Code"]).Trim() + "</td>");

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
                    if (Convert.ToString(strcheckerEmail).Trim() == "")
                    {
                        strcheckerEmail = Convert.ToString(dsList.Tables[2].Rows[irow]["Emp_Emailaddress"]).Trim();
                    }
                    sbBatchApprs.Append("<tr><td style='width:40%;border: 1px solid #ccc'>" + Convert.ToString(dsList.Tables[2].Rows[irow]["ApproverName"]).Trim() + " </td>");
                    sbBatchApprs.Append("<td style='width:10%;border: 1px solid #ccc'>" + Convert.ToString(dsList.Tables[2].Rows[irow]["Status"]).Trim() + "</td>");
                    sbBatchApprs.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(dsList.Tables[2].Rows[irow]["approved_on"]).Trim() + "</td>");
                    sbBatchApprs.Append("<td style='width:35%;border: 1px solid #ccc'>" + Convert.ToString(dsList.Tables[2].Rows[irow]["Remarks"]).Trim() + "</td></tr>");
                }
                sbBatchApprs.Append("</table>");
                #endregion

                sbBatchApprs.Append("<br/><p><a href=" + strInvoiceURL + ">Please click here to view Batch details & take appropriate action.</a></p>");

                spm.sendMail_VSCB(strcheckerEmail, strsubject, Convert.ToString(strbuildBodyMsg).Trim() + Convert.ToString(sbBatchRequest).Trim() + Convert.ToString(sbBatchDetails).Trim() + Convert.ToString(sbBatchApprs).Trim(), "", "");

                #endregion

            }
        }

        Response.Redirect("VSCB_MyBatch.aspx");
        
    }

    protected void btnCorrection_Click(object sender, EventArgs e)
    {
        getMngInvoiceReqstList();
    }

    protected void lnkTravelDetailsEdit_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
       
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Deate_Batch_items_Temptable";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

        spars[2] = new SqlParameter("@MstoneID", SqlDbType.BigInt);
        spars[2].Value = Convert.ToDouble(gvMngPaymentList_Batch.DataKeys[row.RowIndex].Values[2]);

        dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        gvMngPaymentList_Batch.DataSource = null;
        gvMngPaymentList_Batch.DataBind();
        txtbatchTotalPayment.Text = "";
        txtbatchNoOfRequest.Text = "";
        if (dsList.Tables[0].Rows.Count > 0)
        {
            gvMngPaymentList_Batch.DataSource = dsList.Tables[0];
            gvMngPaymentList_Batch.DataBind();

            txtbatchTotalPayment.Text = Convert.ToString(dsList.Tables[0].Rows[0]["TotalPaymentAmt"]).Trim();
            txtbatchNoOfRequest.Text = Convert.ToString(dsList.Tables[0].Rows.Count);
        }

        getMngInvoiceReqstList();

    }

    protected void gvMngPaymentList_Batch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       // if(Request.QueryString.Count>0)
       // e.Row.Cells[10].Visible = false;
    }

    protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMngTravelRqstList.PageIndex = e.NewPageIndex;
        getMngInvoiceReqstList();
    }

    protected void gvMngPaymentList_Batch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMngPaymentList_Batch.PageIndex = e.NewPageIndex;
        get_Added_PaymentReqList_CreateBatch("","","","","","");
    }
}