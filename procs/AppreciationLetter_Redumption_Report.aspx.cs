
using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AppreciationLetter_Redumption_Report : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    bool isMGR = false;
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
        }
        lblmessage.Text = "";
        if (!Page.IsPostBack)
        {
            if (check_admin_report() == false)
            {
                Response.Redirect("Appreciation_Letter_index.aspx");
            }

            hflCEO.Value = "NO";
            hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();
            hflEmpCode.Value = Convert.ToString(Session["Empcode"]).Trim();
            
  
            var getDepartment = hflEmpDepartment.Value;
            var thankyoucard = HiddenFieldid.Value;
            loadDropDownemployeename(getDepartment);
            
            
        }

    }

    public Boolean check_admin_report()
    {
        Boolean isvalid = false;

        DataSet dsLocations = new DataSet();

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "check_admin_report";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();

        dsLocations = spm.getDatasetList(spars, "Appreciation_Letter");

        if (dsLocations != null)
        {
            if (dsLocations.Tables[0].Rows.Count > 0)
            {
                isvalid = true;
            }
        }
        return isvalid;

    }

    private void Reports()
    {
        DataSet dsApprovalStatusReport = new DataSet();

        SqlParameter[] spars = new SqlParameter[2];
         
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "Redumption_Point";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        if (Convert.ToString(ddlempname.SelectedValue).Trim() != "0")
            spars[1].Value = Convert.ToString(ddlempname.SelectedValue).Trim();
        else
            spars[1].Value = DBNull.Value;

        dsApprovalStatusReport = spm.getDatasetList(spars, "Appreciation_Letter");

        ReportViewer1.LocalReport.Refresh();
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportParameter[] param = new ReportParameter[2];
        

        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/update_photo_report.rdlc");
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/Redumption_Report.rdlc");

        ReportDataSource rdsFuel = new ReportDataSource("DataSet1", dsApprovalStatusReport.Tables[0]);
        ReportViewer1.LocalReport.DataSources.Add(rdsFuel); 
        ReportViewer1.LocalReport.Refresh();

    }
     
    protected void btnSave_Click(object sender, EventArgs e)
    { 
        Reports(); 
    }
     
    public void loadDropDownemployeename(string selectVal)
    {
        DataTable dtleaveInbox = new DataTable();
        //dtleaveInbox = spm.GetSerivesRequestDepartment();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "employeename_redumptionreport";

        dtleaveInbox = spm.getDropdownList(spars, "Appreciation_Letter");
        if (dtleaveInbox.Rows.Count > 0)
        {
            ddlempname.DataSource = dtleaveInbox;
            //ddlDepartment.DataBind();
            ddlempname.DataTextField = "Emp_Name";
            ddlempname.DataValueField = "Emp_Code";
            ddlempname.DataBind();
            ddlempname.Items.Insert(0, new ListItem("Select Employee Name", "0")); //updated code

            
        }
    }
     
}