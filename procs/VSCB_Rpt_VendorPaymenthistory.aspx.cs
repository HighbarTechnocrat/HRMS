using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSCB_Rpt_VendorPaymenthistory : System.Web.UI.Page
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

    SP_Methods spm = new SP_Methods();
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
    public DataTable dtPOWONo;
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
                

                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    hdnEmpCode.Value = Session["Empcode"].ToString();
                    get_VendorPaymentHistory_DropdownList();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }


    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        DataSet dsVendorPaymentHistory = new DataSet();

        #region get Vendor Payment History

        string[] strdate;
        string strInvFromDt = "";
        string strInvToDt = "";
        string strPaymntReqFromDt = "";
        string strPaymntReqtoDt = "";
        if (Convert.ToString(txtInvoiceFrmDt.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtInvoiceFrmDt.Text).Trim().Split('-');
            strInvFromDt = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        if (Convert.ToString(txtInvoiceToDt.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtInvoiceToDt.Text).Trim().Split('-');
            strInvToDt = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        if (Convert.ToString(txtPaymntReqFrmDt.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtPaymntReqFrmDt.Text).Trim().Split('-');
            strPaymntReqFromDt = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        if (Convert.ToString(txtPaymntReqToDt.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtPaymntReqToDt.Text).Trim().Split('-');
            strPaymntReqtoDt = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        SqlParameter[] spars = new SqlParameter[12];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Rpt_Vendor_wise_payment_history";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = DBNull.Value;

        spars[2] = new SqlParameter("@POWOID", SqlDbType.BigInt);
        if (Convert.ToString(lstPOWONo.SelectedValue).Trim() != "0")
            spars[2].Value = Convert.ToDouble(lstPOWONo.SelectedValue);
        else
            spars[2].Value = DBNull.Value;

        spars[3] = new SqlParameter("@InvoiceFrmDt", SqlDbType.VarChar);
        if (Convert.ToString(strInvFromDt).Trim() != "")
            spars[3].Value = strInvFromDt;
        else
            spars[3].Value = DBNull.Value;

        spars[4] = new SqlParameter("@InvoiceToDt", SqlDbType.VarChar);
        if (Convert.ToString(strInvToDt).Trim() != "")
            spars[4].Value = strInvToDt;
        else
            spars[4].Value = DBNull.Value;

        spars[5] = new SqlParameter("@paymentReqFrmDt", SqlDbType.VarChar);
        if (Convert.ToString(strPaymntReqFromDt).Trim() != "")
            spars[5].Value = strPaymntReqFromDt;
        else
            spars[5].Value = DBNull.Value;

        spars[6] = new SqlParameter("@paymentReqToDt", SqlDbType.VarChar);
        if (Convert.ToString(strPaymntReqtoDt).Trim() != "")
            spars[6].Value = strPaymntReqtoDt;
        else
            spars[6].Value = DBNull.Value;

        spars[7] = new SqlParameter("@VendorId", SqlDbType.Int);
        if (Convert.ToString(lstVendorName.SelectedValue).Trim() != "0")
            spars[7].Value = Convert.ToInt32(lstVendorName.SelectedValue);
        else
            spars[7].Value = DBNull.Value;

        spars[8] = new SqlParameter("@InvoiceID", SqlDbType.BigInt);
        if (Convert.ToString(lstInvoiceNo.SelectedValue).Trim() != "0")
            spars[8].Value = Convert.ToDouble(lstInvoiceNo.SelectedValue);
        else
            spars[8].Value = DBNull.Value;


        spars[9] = new SqlParameter("@Payment_ID", SqlDbType.BigInt);
        if (Convert.ToString(lstpaymentReqNo.SelectedValue).Trim() != "0")
            spars[9].Value = Convert.ToDouble(lstpaymentReqNo.SelectedValue);
        else
            spars[9].Value = DBNull.Value;


        spars[10] = new SqlParameter("@PrjDetpId", SqlDbType.Int);
        if (Convert.ToString(lstDepartment.SelectedValue).Trim() != "0")
            spars[10].Value = Convert.ToInt32(lstDepartment.SelectedValue);
        else
            spars[10].Value = DBNull.Value;

        spars[11] = new SqlParameter("@Costcenter", SqlDbType.VarChar);
        if (Convert.ToString(lstCostCenter.SelectedValue).Trim() != "0")
            spars[11].Value = Convert.ToString(lstCostCenter.SelectedValue);
        else
            spars[11].Value = DBNull.Value;


        dsVendorPaymentHistory = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");

        #endregion

        try
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/rpt_vendorPaymentHistroy.rdlc");

            ReportDataSource rds = new ReportDataSource("dsVendorPaymentHistroy", dsVendorPaymentHistory.Tables[0]);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.Refresh();
        }
        catch (Exception ex)
        {

        }

    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {

        get_VendorPaymentHistory_DropdownList();
    }

    #endregion

    #region Page Methods

    private void get_VendorPaymentHistory_DropdownList()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_VendorPaymenthistory_Dropdown_List";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

        dsList = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");

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
            lstCostCenter.DataSource = dsList.Tables[1];
            lstCostCenter.DataTextField = "CostCentre";
            lstCostCenter.DataValueField = "CostCentre";
            lstCostCenter.DataBind();
        }
        lstCostCenter.Items.Insert(0, new ListItem("Select Cost Center", "0"));


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
            lstDepartment.DataSource = dsList.Tables[4];
            lstDepartment.DataTextField = "Project_Dept_Name";
            lstDepartment.DataValueField = "Dept_ID";
            lstDepartment.DataBind();
        }
        lstDepartment.Items.Insert(0, new ListItem("Select Department Name", "0"));



        if (dsList.Tables[5].Rows.Count > 0)
        {
            lstpaymentReqNo.DataSource = dsList.Tables[5];
            lstpaymentReqNo.DataTextField = "PaymentReqNo";
            lstpaymentReqNo.DataValueField = "Payment_ID";
            lstpaymentReqNo.DataBind();
        }
        lstpaymentReqNo.Items.Insert(0, new ListItem("Select Payment Request No", "0"));



    }

    #endregion





}