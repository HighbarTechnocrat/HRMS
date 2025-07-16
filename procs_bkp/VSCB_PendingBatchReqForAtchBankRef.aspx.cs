using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSCB_PendingBatchReqForAtchBankRef : System.Web.UI.Page
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
                    //get_CreatBatch_PaymentReq_DropdownList();
                    getMngInvoiceReqstList();

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

        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer; 
        hdnMilestoneId.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim(); 
        
        Response.Redirect("VSCB_AssignbankRefApproveBatch.aspx?batchid=" + hdnMilestoneId.Value);
    }

    #endregion

    #region Page Methods
    private void getMngInvoiceReqstList()
    {
        try
        {
 
           DataSet dsList = new DataSet();
           SqlParameter[] spars = new SqlParameter[2];
             

           spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
           spars[0].Value = "get_Batch_List_PendingBankRef";

           spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
           spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

         
           dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

           gvMngTravelRqstList.DataSource = null;
           gvMngTravelRqstList.DataBind(); 
           if (dsList.Tables[0].Rows.Count > 0)
           {
               gvMngTravelRqstList.DataSource = dsList.Tables[0];
               gvMngTravelRqstList.DataBind(); 
           } 


            /*
            DataSet dsList = new DataSet();
            SqlParameter[] spars = new SqlParameter[10];

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
            spars[0].Value = "get_Batch_List_PendingBankRef";

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

            spars[4] = new SqlParameter("@VendorId", SqlDbType.Int);
            if (Convert.ToString(lstVendorName.SelectedValue).Trim() != "0")
                spars[4].Value = Convert.ToInt32(lstVendorName.SelectedValue);
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
                
            dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            gvMngTravelRqstList.DataSource = null;
            gvMngTravelRqstList.DataBind(); 
            if (dsList.Tables[0].Rows.Count > 0)
            {
                gvMngTravelRqstList.DataSource = dsList.Tables[0];
                gvMngTravelRqstList.DataBind(); 
            }*/


        }
        catch (Exception ex)
        {

        }
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
        lstPOWONo.Items.Insert(0, new ListItem("Select PO/WO No", "0"));


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
            lstVendorName.DataValueField = "VendorID";
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


  

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {

    }

  

    

    protected void btnCorrection_Click(object sender, EventArgs e)
    {
        getMngInvoiceReqstList();
    }

    protected void lnkTravelDetailsEdit_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
       
         

    }

    protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMngTravelRqstList.PageIndex = e.NewPageIndex;
        this.getMngInvoiceReqstList();
    }
}