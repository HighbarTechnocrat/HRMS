using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ABAP_Object_Tracker_Changed_CTM_Consultant : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();

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


            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
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
                    getProjectLocation();

                    if (Request.QueryString.Count > 0)
                    {
                        DDLProjectLocation.Enabled = false;
                        hdnABAPODUploadId.Value = Convert.ToString(Request.QueryString["ABAPODId"]).Trim();
                        GetApproved_Plan_ABAPDetails();
                        DDLABAPDevDesc_SelectedIndexChanged(sender, e);

                    }

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
    private void getProjectLocation()
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetDropDownprojectLocation";

            DataSet DS = spm.getDatasetList(spars, "SP_TASK_M_DETAILS");
            DDLProjectLocation.DataSource = DS.Tables[0];
            DDLProjectLocation.DataTextField = "Location_name";
            DDLProjectLocation.DataValueField = "comp_code";
            DDLProjectLocation.DataBind();
            DDLProjectLocation.Items.Insert(0, new ListItem("Select Project Location", "0"));
        }
        catch (Exception ex)
        {

        }
    }

    public void GetApproved_Plan_ABAPDetails()
    {
        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[5];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetChangedConsultantCTMDetails";

        spars[1] = new SqlParameter("@ABAPODUploadId", SqlDbType.VarChar);
        spars[1].Value = Convert.ToInt32(hdnABAPODUploadId.Value);

        spars[2] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
        spars[2].Value = (Session["Empcode"]).ToString().Trim();

        spars[3] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
        spars[3].Value = DDLProjectLocation.SelectedValue == "0" ? "" : DDLProjectLocation.SelectedValue;

        spars[4] = new SqlParameter("@ABAPDevDesc", SqlDbType.VarChar);
        spars[4].Value = DDLABAPDevDesc.SelectedValue == "0" ? "" : DDLABAPDevDesc.SelectedValue;

        dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
        {
            hdnProjLocation.Value = dsABAPObjectPlanSubmitted.Tables[3].Rows[0]["ProjectLocation"].ToString();
            DDLABAPDevDesc.DataSource = dsABAPObjectPlanSubmitted.Tables[3];
            DDLABAPDevDesc.DataTextField = "Development_Desc";
            DDLABAPDevDesc.DataValueField = "ABAPODId";
            DDLABAPDevDesc.DataBind();
            DDLABAPDevDesc.Items.Insert(0, new ListItem("Select ABAP Development Desc", "0"));

            gvCTMTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvCTMTestDetails.DataBind();
        }
        else
        {
            gvCTMTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvCTMTestDetails.DataBind();
        }

        DDLProjectLocation.SelectedValue = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ProjectLocation"].ToString().Trim();
        txtPRM.Text = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ProjectManager"].ToString().Trim();

    }
    
    public void DDLProjectLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetApproved_Plan_ABAPDetails();
    }

    protected void btnABAPPlanSubmit_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvCTMTestDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string secondColumnValue = row.Cells[1].Text.Trim();
                if (string.IsNullOrEmpty(secondColumnValue))
                {
                    lblmessage.Text = "Consultant value cannot be empty.";
                    lblmessage.Visible = true;
                }
            }

        }

        foreach (GridViewRow row in gvCTMTestDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string secondColumnValue = row.Cells[1].Text.Trim();
                if (string.IsNullOrEmpty(secondColumnValue))
                {
                    lblmessage.Text = "Consultant value cannot be empty.";
                    lblmessage.Visible = true;
                }
            }

        }


        foreach (GridViewRow row in gvCTMTestDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string secondColumnValue = row.Cells[1].Text.Trim();
                if (string.IsNullOrEmpty(secondColumnValue))
                {
                    lblmessage.Text = "Error: Second column value cannot be empty.";
                    lblmessage.Visible = true;
                }
            }

        }

        foreach (GridViewRow row in gvCTMTestDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string secondColumnValue = row.Cells[1].Text.Trim();
                if (string.IsNullOrEmpty(secondColumnValue))
                {
                    lblmessage.Text = "Error: Second column value cannot be empty.";
                    lblmessage.Visible = true;
                }
            }

        }
    }

    public void DDLABAPDevDesc_SelectedIndexChanged(object sender, EventArgs e)
    {

        lblmessage.Text = "";
        var selectedCategoryId = DDLABAPDevDesc.SelectedValue;

        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[5];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetChangedConsultantCTMDetails";

        spars[1] = new SqlParameter("@ABAPODUploadId", SqlDbType.VarChar);
        spars[1].Value = Convert.ToInt32(hdnABAPODUploadId.Value);

        spars[2] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
        spars[2].Value = (Session["Empcode"]).ToString().Trim();

        spars[3] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
        spars[3].Value = DDLProjectLocation.SelectedValue == "0" ? "" : DDLProjectLocation.SelectedValue;

        spars[4] = new SqlParameter("@ABAPDevDesc", SqlDbType.VarChar);
        spars[4].Value = DDLABAPDevDesc.SelectedValue == "0" ? "" : DDLABAPDevDesc.SelectedValue;

        dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
        {
            gvCTMTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvCTMTestDetails.DataBind();
            ctmtest.Visible = true;
        }
        else
        {
            ctmtest.Visible = false;
            gvCTMTestDetails.DataSource = null;
            gvCTMTestDetails.DataBind();
            lblmessage.Text = "No record found.";
        }


        //DDLProjectLocation.SelectedValue = hdnProjLocation.Value;
        txtPRM.Text = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ProjectManager"].ToString().Trim();
    }

    #endregion
}