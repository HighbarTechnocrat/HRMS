using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ABAP_Object_Tracker_Changed_RGSFSABAPHBTCTM_Consultant : System.Web.UI.Page
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
                        spnrgs.Visible = false;
                        spnfs.Visible = false;
                        spnhbttest.Visible = false;
                        //ctmtest.Visible = false;
                        hdnABAPODUploadId.Value = Convert.ToString(Request.QueryString["ABAPODId"]).Trim();
                        get_ABAP_Object_Submitted_Plan_FSDetails();
                        DDLABAPDevDesc_SelectedIndexChanged(sender, e);
                        getModuleByProjectLocation();
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

    public void get_ABAP_Object_Submitted_Plan_FSDetails()
    {
        try
        {
            //spnrgs.Visible = true;
            //spnfs.Visible = true;
            //spnhbttest.Visible = true;

            var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();

            SqlParameter[] spars = new SqlParameter[4];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getABAPDevelopmentDescListBeasedOnABAPODUploadId";

            spars[1] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
            spars[1].Value = getCreatedBy;

            spars[2] = new SqlParameter("@ABAPODUploadId", SqlDbType.VarChar);
            spars[2].Value = hdnABAPODUploadId.Value;

            spars[3] = new SqlParameter("@Module", SqlDbType.VarChar);
            spars[3].Value = DDLModule.SelectedValue == "" ? "0" :DDLModule.SelectedValue;

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                hdnProjLocation.Value = DS.Tables[0].Rows[0]["ProjectLocation"].ToString();
                DDLABAPDevDesc.DataSource = DS.Tables[0];
                DDLABAPDevDesc.DataTextField = "Development_Desc";
                DDLABAPDevDesc.DataValueField = "ABAPODId";
                DDLABAPDevDesc.DataBind();
                DDLABAPDevDesc.Items.Insert(0, new ListItem("Select ABAP Development Desc", "0"));

                DDLProjectLocation.SelectedValue = DS.Tables[0].Rows[0]["ProjectLocation"].ToString().Trim();
                txtPRM.Text = DS.Tables[1].Rows[0]["ProgramManager"].ToString().Trim();


                //gvRGSDetails.DataSource = DS.Tables[2];
                //gvRGSDetails.DataBind();

                //gvFSDetails.DataSource = DS.Tables[2];
                //gvFSDetails.DataBind();

                //gvHBTTestDetails.DataSource = DS.Tables[2];
                //gvHBTTestDetails.DataBind();
            }
            else
            {
                DDLABAPDevDesc.Items.Insert(0, new ListItem("Select ABAP Development Desc", "0"));
            }

        }
        catch (Exception ex)
        {
            return;
        }

    }

    public void DDLABAPDevDesc_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        spnrgs.Visible = true;
        spnfs.Visible = true;
        spnhbttest.Visible = true;
        //ctmtest.Visible = true;
        var selectedCategoryId = DDLABAPDevDesc.SelectedValue;

        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[7];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetChangedConsultant_RGS_FS_ABAP_HBT_CTM_Details";

        spars[1] = new SqlParameter("@ABAPODUploadId", SqlDbType.VarChar);
        spars[1].Value = Convert.ToInt32(hdnABAPODUploadId.Value);

        spars[2] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
        spars[2].Value = (Session["Empcode"]).ToString().Trim();

        spars[3] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
        spars[3].Value = DDLProjectLocation.SelectedValue == "0" ? "" : DDLProjectLocation.SelectedValue;

        spars[4] = new SqlParameter("@ABAPDevDesc", SqlDbType.VarChar);
        spars[4].Value = DDLABAPDevDesc.SelectedValue == "0" ? "" : DDLABAPDevDesc.SelectedValue;

        spars[5] = new SqlParameter("@StageValue", SqlDbType.VarChar);
        spars[5].Value = DDLStage.SelectedValue == "0" ? "" : DDLStage.SelectedValue;

        spars[6] = new SqlParameter("@Module", SqlDbType.VarChar);
        spars[6].Value = DDLModule.SelectedValue;

        dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
        {
            if (DDLStage.SelectedValue == "")
            {
                spnrgs.Visible = false;
                spnfs.Visible = false;
                spnabap.Visible = false;
                spnhbttest.Visible = false;
                spnctmtest.Visible = false;

                gvRGSDetails.Visible = false;
                gvFSDetails.Visible = false;
                gvABAPDevDetails.Visible = false;
                gvHBTTestDetails.Visible = false;
                gvCTMTestDetails.Visible = false;
            }

            if (DDLStage.SelectedValue == "RGS")
            {
                spnrgs.Visible = true;
                spnfs.Visible = false;
                spnabap.Visible = false;
                spnhbttest.Visible = false;
                spnctmtest.Visible = false;

                gvRGSDetails.Visible = true;
                gvFSDetails.Visible = false;
                gvABAPDevDetails.Visible = false;
                gvHBTTestDetails.Visible = false;
                gvCTMTestDetails.Visible = false;

                BindDataToGridView(dsABAPObjectPlanSubmitted, "RGS", gvRGSDetails);
            }

            if (DDLStage.SelectedValue == "FS")
            {
                spnrgs.Visible = false;
                spnfs.Visible = true;
                spnabap.Visible = false;
                spnhbttest.Visible = false;
                spnctmtest.Visible = false;

                gvRGSDetails.Visible = false;
                gvFSDetails.Visible = true;
                gvABAPDevDetails.Visible = false;
                gvHBTTestDetails.Visible = false;
                gvCTMTestDetails.Visible = false;

                BindDataToGridView(dsABAPObjectPlanSubmitted, "FS", gvFSDetails);
            }

            if (DDLStage.SelectedValue == "HBT Testing")
            {
                spnrgs.Visible = false;
                spnfs.Visible = false;
                spnabap.Visible = false;
                spnhbttest.Visible = true;
                spnctmtest.Visible = false;

                gvRGSDetails.Visible = false;
                gvFSDetails.Visible = false;
                gvABAPDevDetails.Visible = false;
                gvHBTTestDetails.Visible = true;
                gvCTMTestDetails.Visible = false;

                BindDataToGridView(dsABAPObjectPlanSubmitted, "HBT", gvHBTTestDetails);
            }

            if (DDLStage.SelectedValue == "ABAP")
            {
                spnrgs.Visible = false;
                spnfs.Visible = false;
                spnabap.Visible = true;
                spnhbttest.Visible = false;
                spnctmtest.Visible = false;

                gvRGSDetails.Visible = false;
                gvFSDetails.Visible = false;
                gvABAPDevDetails.Visible = true;
                gvHBTTestDetails.Visible = false;
                gvCTMTestDetails.Visible = false;

                BindDataToGridView(dsABAPObjectPlanSubmitted, "ABAP Dev", gvABAPDevDetails);
            }

            if (DDLStage.SelectedValue == "CTM Testing")
            {
                spnrgs.Visible = false;
                spnfs.Visible = false;
                spnabap.Visible = false;
                spnhbttest.Visible = false;
                spnctmtest.Visible = true;

                gvRGSDetails.Visible = false;
                gvFSDetails.Visible = false;
                gvABAPDevDetails.Visible = false;
                gvHBTTestDetails.Visible = false;
                gvCTMTestDetails.Visible = true;

                BindDataToGridView(dsABAPObjectPlanSubmitted, "CTM", gvCTMTestDetails);
            }
        }
        else
        {
            gvRGSDetails.DataSource = null;
            gvRGSDetails.DataBind();

            gvFSDetails.DataSource = null;
            gvFSDetails.DataBind();

            gvHBTTestDetails.DataSource = null;
            gvHBTTestDetails.DataBind();

            gvCTMTestDetails.DataSource = null;
            gvCTMTestDetails.DataBind();

            spnrgs.Visible = false;
            spnfs.Visible = false;
            spnhbttest.Visible = false;
            spnctmtest.Visible = false;
            lblmessage.Text = "No record found.";
        }

        DDLProjectLocation.SelectedValue = hdnProjLocation.Value;

        if (DDLStage.SelectedValue == "")
        {
            txtPRM.Text = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ProjectManager"].ToString().Trim();
        }
        else
        {
            txtPRM.Text = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ProjectManager"].ToString().Trim();
        }

    }

    private void BindDataToGridView(DataSet dataSet, string stage, GridView gridView)
    {
        if (dataSet.Tables != null)
        {
            DataTable filteredTable = null;
            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                var filteredRows = dataSet.Tables[0].AsEnumerable().Where(row => row.Field<string>("Stage") == stage);
                if (filteredRows.Any())
                {
                    filteredTable = filteredRows.CopyToDataTable();
                }
                else
                {
                    filteredTable = dataSet.Tables[0].Clone();

                }
            }

            if (gridView.ID == "gvRGSDetails")
            {
                if (filteredTable.Rows.Count > 0)
                {
                    gridView.Visible = true;
                    gridView.DataSource = filteredTable;
                    gridView.DataBind();
                }
                else
                {
                    gridView.Visible = false;
                    gridView.DataSource = null;
                    gridView.DataBind();
                    lblmessage.Text = "No record found.";

                }
            }
            else if (gridView.ID == "gvFSDetails")
            {
                if (filteredTable.Rows.Count > 0)
                {
                    gridView.Visible = true;
                    gridView.DataSource = filteredTable;
                    gridView.DataBind();
                }
                else
                {
                    gridView.Visible = false;
                    gridView.DataSource = null;
                    gridView.DataBind();
                    lblmessage.Text = "No record found.";

                }
            }
            else if (gridView.ID == "gvHBTTestDetails")
            {
                if (filteredTable.Rows.Count > 0)
                {
                    gridView.Visible = true;
                    gridView.DataSource = filteredTable;
                    gridView.DataBind();
                }
                else
                {
                    gridView.Visible = false;
                    gridView.DataSource = null;
                    gridView.DataBind();
                    lblmessage.Text = "No record found.";

                }
            }
            else if (gridView.ID == "gvABAPDevDetails")
            {
                if (filteredTable.Rows.Count > 0)
                {
                    gridView.Visible = true;
                    gridView.DataSource = filteredTable;
                    gridView.DataBind();
                }
                else
                {
                    gridView.Visible = false;
                    gridView.DataSource = null;
                    gridView.DataBind();
                    lblmessage.Text = "No record found.";

                }
            }
            else if (gridView.ID == "gvCTMTestDetails")
            {
                if (filteredTable.Rows.Count > 0)
                {
                    gridView.Visible = true;
                    gridView.DataSource = filteredTable;
                    gridView.DataBind();
                }
                else
                {
                    gridView.Visible = false;
                    gridView.DataSource = null;
                    gridView.DataBind();
                    lblmessage.Text = "No record found.";

                }
            }

        }
    }

    protected void btnABAPPlanSubmit_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvFSDetails.Rows)
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

        foreach (GridViewRow row in gvFSDetails.Rows)
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


        foreach (GridViewRow row in gvFSDetails.Rows)
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

        foreach (GridViewRow row in gvFSDetails.Rows)
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


    public void DDLStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        DDLABAPDevDesc.Items.Clear();
        DDLABAPDevDesc.Items.Insert(0, new ListItem("Select ABAP Development Desc", "0"));

        get_ABAP_Object_Submitted_Plan_FSDetails();
        DDLABAPDevDesc_SelectedIndexChanged(sender, e);
        
    }

    private void getModuleByProjectLocation()
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getModuleByProjectLocation";

            spars[1] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
            spars[1].Value = hdnProjLocation.Value;

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
            DDLModule.DataSource = DS.Tables[0];
            DDLModule.DataTextField = "ModuleDesc";
            DDLModule.DataValueField = "ModuleId";
            DDLModule.DataBind();
            DDLModule.Items.Insert(0, new ListItem("Select Module", "0"));
        }
        catch (Exception ex)
        {

        }
    }

    public void DDLModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLStage.SelectedValue != "0")
        {
            lblmessage.Text = "Please select the Stage";
            return;
        }
            get_ABAP_Object_Submitted_Plan_FSDetails();
        DDLABAPDevDesc_SelectedIndexChanged(sender, e);

    }

    #endregion
}