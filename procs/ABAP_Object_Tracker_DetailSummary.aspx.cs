using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ABAP_Object_Tracker_DetailSummary : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();

    string selectedProject = "";


    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            getProjectLocation(sender, e);
            getPriorityMaster(sender, e);
        }
    }
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
                if (DS.Tables[0].Rows.Count == 1)
                {
                    HDProjectLocation.Value = DS.Tables[0].Rows[0]["comp_code"].ToString();
                    DDLProjectLocation_SelectedIndexChanged(sender, e);
                    selectedProject = HDProjectLocation.Value.Trim();
                    // getConsultantsByLocation();
                }
                else
                {
                    HDProjectLocation.Value = Convert.ToString(DDLProjectLocation.SelectedValue).Trim();
                    selectedProject = HDProjectLocation.Value.Trim();

                }
            }

        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return;
        }
    }

    private void getPriorityMaster(object sender, EventArgs e)
    {
        try
        {
            var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();

            SqlParameter[] spars = new SqlParameter[1];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getPriorityMaster";

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                DDLPriorities.DataSource = DS.Tables[0];
                DDLPriorities.DataTextField = "PriorityName";
                DDLPriorities.DataValueField = "PriorityId";
                DDLPriorities.DataBind();
                DDLPriorities.Items.Insert(0, new ListItem("Select Priority", "0"));

            }

        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return;
        }
    }


    public void DDLProjectLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedProject = Convert.ToString(DDLProjectLocation.SelectedValue).Trim();
    }
    protected void btnGenerateReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (DDLProjectLocation.SelectedValue == "0")
            {
                lblmessage.Text = "Please select the project/ location";
                return;
            }

            if (DDLPriorities.SelectedValue == "0")
            {
                lblmessage.Text = "Please select the Priority";
                return;
            }

            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "ABAPTrackerDetailSummaryReport";

            spars[1] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(!string.IsNullOrEmpty(selectedProject) ? selectedProject : DDLProjectLocation.SelectedValue).Trim();

            spars[2] = new SqlParameter("@PriorityType", SqlDbType.VarChar);
            spars[2].Value = DDLPriorities.SelectedValue;

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");

            string strpath = Server.MapPath("~/procs/ABAP_Object_Tracker_DetailSummary.rdlc");
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = strpath;

            ReportDataSource rgs_Details = new ReportDataSource("ds_DetailSummary", DS.Tables[0]);

            ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rgs_Details);


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
            ReportViewer1.Visible = false;
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return;
        }
    }
}
