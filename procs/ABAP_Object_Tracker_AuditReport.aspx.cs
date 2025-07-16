using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ABAP_Object_Tracker_AuditReport : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    string selectedProject = "";
    #region Creative_Default_methods

    public DataSet dsDirecttaxSectionList = new DataSet();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
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

            lblmessage.Text = "";
            txtEmpCode.Text = Convert.ToString(Session["Empcode"]);
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/ABAP_Object_Tracker_Index.aspx");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    DDLFunctionalConsultant.Items.Insert(0, new ListItem("Select Consultant", "0"));
                    getProjectLocation(sender, e);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    #endregion

    #region ABAP Object Submitted Plan 
    private void getProjectLocation(object sender, EventArgs e)
    {
        try
        {
            var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();

            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetAllLocationsReportAccess";

            spars[1] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
            spars[1].Value = getCreatedBy;

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                DDLProjectLocation.DataSource = DS.Tables[0];
                DDLProjectLocation.DataTextField = "Location_name";
                DDLProjectLocation.DataValueField = "comp_code";
                DDLProjectLocation.DataBind();
                DDLProjectLocation.Items.Insert(0, new ListItem("Select Project Location", "0"));
                if (DS.Tables[0].Rows.Count == 1)
                {
                    HDProjectLocation.Value = DS.Tables[0].Rows[0]["comp_code"].ToString();
                    DDLProjectLocation_SelectedIndexChanged(sender, e);
                    selectedProject = HDProjectLocation.Value.Trim();
                }
            }
            else
            {
                DDLProjectLocation.Items.Insert(0, new ListItem("Select Project Location", "0"));
                lblmessage.Text = "Changes to the RGS/FS/HBT/ABAP Consultant have been restricted to project managers only.";
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return;
        }
    }
    private void getConsultantsByLocation()
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getConsultantsByLocation";

            spars[1] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(selectedProject).Trim();

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
            DDLFunctionalConsultant.DataSource = DS.Tables[0];
            DDLFunctionalConsultant.DataTextField = "Emp_Name";
            DDLFunctionalConsultant.DataValueField = "Emp_Code";
            DDLFunctionalConsultant.DataBind();
            DDLFunctionalConsultant.Items.Insert(0, new ListItem("Select Consultant", "0"));
        }
        catch (Exception ex)
        {

        }
    }
    public void DDLProjectLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedProject = Convert.ToString(DDLProjectLocation.SelectedValue).Trim();
        getConsultantsByLocation();
    }
    protected void btnGenerateReport_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        try
        {
            if(DDLProjectLocation.SelectedValue =="0")
            {
                lblmessage.Text = "Please select the Project/ Location";
                return;
            }

            SqlParameter[] spars = new SqlParameter[4];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getABAPObjectAuditReportByLocation";

            spars[1] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(!string.IsNullOrEmpty(selectedProject) ? selectedProject : DDLProjectLocation.SelectedValue).Trim();

            spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            if (DDLFunctionalConsultant.SelectedValue == "0")
            {
                spars[2].Value = DBNull.Value;
            }
            else
            {
                spars[2].Value = DDLFunctionalConsultant.SelectedValue;
            }

            spars[3] = new SqlParameter("@StageValue", SqlDbType.VarChar);
            if (ddlStage.SelectedValue == "0")
            {
                spars[3].Value = DBNull.Value;
            }
            else
            {
                spars[3].Value = ddlStage.SelectedValue;
            }

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");

            string strpath = Server.MapPath("~/procs/ABAP_Object_Tracker_AuditReport.rdlc");
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = strpath;

            ReportDataSource rgs_Details = new ReportDataSource("ds_RGSAuditDetails", DS.Tables[0]);
            ReportDataSource fs_Details = new ReportDataSource("ds_FSAuditDetails", DS.Tables[1]);
            ReportDataSource abapdev_Details = new ReportDataSource("ds_ABAPDevAuditDetails", DS.Tables[2]);
            ReportDataSource hbttest_Details = new ReportDataSource("ds_HBTAuditDetails", DS.Tables[3]);
            ReportDataSource ctmtest_Details = new ReportDataSource("ds_CTMAuditDetails", DS.Tables[4]);


            ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rgs_Details);
            ReportViewer1.LocalReport.DataSources.Add(fs_Details);
            ReportViewer1.LocalReport.DataSources.Add(abapdev_Details);
            ReportViewer1.LocalReport.DataSources.Add(hbttest_Details);
            ReportViewer1.LocalReport.DataSources.Add(ctmtest_Details);

            ReportViewer1.LocalReport.Refresh();
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return;
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            DDLProjectLocation.SelectedIndex = 0;
            DDLFunctionalConsultant.Items.Clear();
            DDLFunctionalConsultant.Items.Insert(0, new ListItem("Select Consultant", "0"));

            ddlStage.SelectedIndex = 0;
            ReportViewer1.Visible = false;
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return;
        }
    }
    #endregion
}