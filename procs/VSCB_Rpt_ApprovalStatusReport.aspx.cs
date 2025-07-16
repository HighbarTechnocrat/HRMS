using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_VSCB_Rpt_ApprovalStatusReport : System.Web.UI.Page
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
                    get_ApprovalStatusReport_DropdownList();
                    Get_Pending_StatusReport_Access();
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
        Reports();

    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {

        get_ApprovalStatusReport_DropdownList();
        txtFromdate.Text = "";
        txtToDate.Text = "";
        Reports();
        
    }

    #endregion

    #region Page Methods

    private void get_ApprovalStatusReport_DropdownList()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        if (hdnEmpCode.Value == "99999999")
        {
            spars[0].Value = "get_ApprovalStatusreport_Dropdown_List_Account";
        }
        else
        {
            spars[0].Value = "get_ApprovalStatusreport_Dropdown_List";
        }
        

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCode.Value;

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
            lstVendorName.DataSource = dsList.Tables[1];
            lstVendorName.DataTextField = "Name";
            lstVendorName.DataValueField = "VendorID";
            lstVendorName.DataBind();
        }
        lstVendorName.Items.Insert(0, new ListItem("Select Vendor Name", "0"));

        if (dsList.Tables[2].Rows.Count > 0)
        {
            lstCostCenter.DataSource = dsList.Tables[2];
            lstCostCenter.DataTextField = "CostCentre";
            lstCostCenter.DataValueField = "Dept_ID";
            lstCostCenter.DataBind();
        }
        lstCostCenter.Items.Insert(0, new ListItem("Select Cost Center", "0"));

    }


    private void Reports()
    {
        DataSet dsApprovalStatusReport = new DataSet();

        string[] strInvdate;
        string strfromDate = "";
        string strToDate = "";


        #region get Approval Status Report


        SqlParameter[] spars = new SqlParameter[7];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_ApprovalStatusreport";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCode.Value;

        spars[2] = new SqlParameter("@POWOID", SqlDbType.BigInt);
        if (Convert.ToString(lstPOWONo.SelectedValue).Trim() != "0")
            spars[2].Value = Convert.ToDouble(lstPOWONo.SelectedValue);
        else
            spars[2].Value = DBNull.Value;


        spars[3] = new SqlParameter("@VendorId", SqlDbType.Int);
        if (Convert.ToString(lstVendorName.SelectedValue).Trim() != "0")
            spars[3].Value = Convert.ToInt32(lstVendorName.SelectedValue);
        else
            spars[3].Value = DBNull.Value;

        spars[4] = new SqlParameter("@Costcenter", SqlDbType.NVarChar);
        if (Convert.ToString(lstCostCenter.SelectedValue).Trim() != "0")
            spars[4].Value = Convert.ToInt32(lstCostCenter.SelectedValue);
        else
            spars[4].Value = DBNull.Value;

        spars[5] = new SqlParameter("@FromDate", SqlDbType.VarChar);
        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            strInvdate = Convert.ToString(txtFromdate.Text).Trim().Split('-');
            strfromDate = Convert.ToString(strInvdate[2]) + "-" + Convert.ToString(strInvdate[1]) + "-" + Convert.ToString(strInvdate[0]);
            spars[5].Value = strfromDate;
        }
        else
        {
            spars[5].Value = DBNull.Value;
        }
        spars[6] = new SqlParameter("@ToDate", SqlDbType.VarChar);
        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            strInvdate = Convert.ToString(txtToDate.Text).Trim().Split('-');
            strToDate = Convert.ToString(strInvdate[2]) + "-" + Convert.ToString(strInvdate[1]) + "-" + Convert.ToString(strInvdate[0]);

            spars[6].Value = strToDate;
        }
        else
        {
            spars[6].Value = DBNull.Value;
        }


        dsApprovalStatusReport = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");

        if (dsApprovalStatusReport.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsApprovalStatusReport.Tables[0].Rows.Count; i++)
            {
                string str = dsApprovalStatusReport.Tables[0].Rows[i]["Approver5"].ToString();
                if (str.Trim() == "")
                {
                    dsApprovalStatusReport.Tables[0].Rows[i]["Approver5"] = "Accounts Department";
                }

                if (dsApprovalStatusReport.Tables[0].Rows[i]["InvoiceNo"].ToString().Trim() == "")
                {
                    dsApprovalStatusReport.Tables[0].Rows[i]["Approver5"] = "";
                }

            }
        }
        dsApprovalStatusReport.AcceptChanges();
        #endregion

        try
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/VSCB_rpt_ApprovalStatusreport.rdlc");

            ReportDataSource rds = new ReportDataSource("DSApprovalStatusReport", dsApprovalStatusReport.Tables[0]);

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.Refresh();
        }
        catch (Exception ex)
        {

        }
    }

    private void Get_Pending_StatusReport_Access()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[4];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "CheckIsShow_Reports"; //"Get_Pending_Status_Report_Access"; 

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCode.Value;

        spars[3] = new SqlParameter("@ReportName", SqlDbType.VarChar);
        spars[3].Value = "VSCB_SendPendingInoviceMail";

        dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        //if (dsList.Tables[0].Rows.Count > 0)
        //{
        //    mobile_cancel.Visible = true;
        //}

        if (dsList.Tables[0].Rows.Count > 0)
        {
            var getStatus = Convert.ToString(dsList.Tables[0].Rows[0]["IsAccess"]);
            if (getStatus == "SHOW")
            {
                mobile_cancel.Visible = true;
            }
        }

    }

    #endregion
}