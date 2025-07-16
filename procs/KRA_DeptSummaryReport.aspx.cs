using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Linq;

public partial class procs_KRA_DeptSummaryReport : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
        }
        if (Page.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/KRA_Index.aspx");
        }

        lblmessage.Text = "";
        if (!Page.IsPostBack)
        {
            hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();
            get_DepartmentList();
            get_KRAPeriodList();
            get_RoleList();


        }
    }

    protected void btnback_mng_Click(object sender, EventArgs e)
    {
        string DeptIDs = string.Empty;
        string roleIDs = string.Empty;
        if (Convert.ToString(lstPeriod.SelectedValue).Trim() == "0")
        {
            lblmessage.Text = "Please select period";
            return;
        }

        DeptIDs = String.Join(",", lstDepartment.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
        roleIDs = String.Join(",", DDLApprovalType.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
        getDepartmentStatusReport(DeptIDs, roleIDs);
    }
    private void getDepartmentStatusReport(string DeptIDs,string roleIDs)
    {

        #region get KRA Team Status

        DataSet dtKRATeam = new DataSet();
        SqlParameter[] spars = new SqlParameter[6];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Rpt_Department_Summary_status";
        spars[1] = new SqlParameter("@Dept_IDs", SqlDbType.NVarChar);
        if (Convert.ToString(lstDepartment.SelectedValue).Trim() != "")
            spars[1].Value = DeptIDs;
        else
            spars[1].Value = DBNull.Value;
        spars[2] = new SqlParameter("@period_id", SqlDbType.Int);
        spars[2].Value = Convert.ToInt32(lstPeriod.SelectedValue);

        //spars[3] = new SqlParameter("@Role_IDs", SqlDbType.VarChar);
        //if (Convert.ToString(DDLApprovalType.SelectedValue).Trim() != "")
        //    spars[3].Value = roleIDs;
        //else
        //    spars[3].Value = DBNull.Value;

        spars[4] = new SqlParameter("@empcode", SqlDbType.NVarChar);
        spars[4].Value = hdnloginempcode.Value;
        spars[5] = new SqlParameter("@ReportName", SqlDbType.NVarChar);
        spars[5].Value = "KRADeptSummaryReport";
        dtKRATeam = spm.getDatasetList(spars, "SP_KRA_Reports");

        #endregion

        try
        {
            string strpath = Server.MapPath("~/procs/KRADepartmentSummaryRpt.rdlc");
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = strpath;

            ReportDataSource rds_summary = new ReportDataSource("dsDeptStatus_Summary", dtKRATeam.Tables[0]);
            ReportDataSource rds_KRANotSubmitted = new ReportDataSource("dsKRA_AllEmployeeSummaryReport", dtKRATeam.Tables[1]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds_summary);
            ReportViewer1.LocalReport.DataSources.Add(rds_KRANotSubmitted);
            ReportViewer1.LocalReport.Refresh();
        }
        catch (Exception ex)
        {

        }
    }

    public void get_DepartmentList()
    {
        DataSet dtDepartment = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Department_list";
        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = hdnloginempcode.Value;
        spars[2] = new SqlParameter("@ReportName", SqlDbType.NVarChar);
        spars[2].Value = "KRADeptReport";
        dtDepartment = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
        if (dtDepartment.Tables[0].Rows.Count > 0)
        {
            lstDepartment.DataSource = dtDepartment.Tables[0];
            lstDepartment.DataTextField = "Department_Name";
            lstDepartment.DataValueField = "Department_id";
            lstDepartment.DataBind();
            //lstDepartment.Items.Insert(0, new ListItem(" All Department", "0"));
        }

    }

    public void get_RoleList()
    {
        DataSet dtDepartment = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_KRARole_list";
        dtDepartment = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
        if (dtDepartment.Tables[0].Rows.Count > 0)
        {
            DDLApprovalType.DataSource = dtDepartment.Tables[0];
            DDLApprovalType.DataTextField = "Role_Name";
            DDLApprovalType.DataValueField = "Role_Id";
            DDLApprovalType.DataBind();
            DDLApprovalType.Items.Insert(0, new ListItem("Select Role", "0"));
        }

    }


    public void get_KRAPeriodList()
    {
        DataSet dtPeriod = new DataSet();

        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_KRAPeriod_list";

        dtPeriod = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
        if (dtPeriod.Tables[0].Rows.Count > 0)
        {
            lstPeriod.DataSource = dtPeriod.Tables[0];
            lstPeriod.DataTextField = "period_name";
            lstPeriod.DataValueField = "period_id";
            lstPeriod.DataBind();
            lstPeriod.Items.Insert(0, new ListItem("Select Period", "0"));
        }

    }
}