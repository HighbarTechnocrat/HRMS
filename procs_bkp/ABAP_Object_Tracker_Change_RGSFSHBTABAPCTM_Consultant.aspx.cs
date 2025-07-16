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

public partial class ABAP_Object_Tracker_Change_RGSFSHBTABAPCTM_Consultant : System.Web.UI.Page
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
                    spnrgs.Visible = false;
                    spnfs.Visible = false;
                    spnabap.Visible = false;
                    spnhbttest.Visible = false;
                    spnctmtest.Visible = false;
                    trvl_btnSave.Visible = false;
                    litxtPRM.Visible = false;

                    getProjectLocation(sender, e);
                    getFunctionalConsultant();
                    DDLABAPDevDesc.Items.Insert(0, new ListItem("Select ABAP Development Desc", "0"));
                    //get_ABAP_Object_Submitted_Plan_FSDetails();
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
            spars[0].Value = "getUploadedLocationMaster";

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

    public void get_ABAP_Object_Submitted_Plan_FSDetails()
    {
        lblmessage.Text = "";
        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetRGSFSHBTDetailsByProjectLocation";

        spars[1] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
        spars[1].Value = (Session["Empcode"]).ToString().Trim();

        spars[2] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
        spars[2].Value = DDLProjectLocation.SelectedValue == "0" ? "" : DDLProjectLocation.SelectedValue;

        spars[3] = new SqlParameter("@ABAPDevDesc", SqlDbType.VarChar);
        spars[3].Value = DDLABAPDevDesc.SelectedValue == "0" ? "" : DDLABAPDevDesc.SelectedValue;

        dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
        {
            DataTable originalTable = dsABAPObjectPlanSubmitted.Tables[0];

            // Filter rows for RGSDetailsId
            DataTable RGSfilteredTable = null;
            var RGSrows = originalTable.AsEnumerable().Where(row => !row.IsNull("RGSDetailsId"));
            if (RGSrows.Any())
            {
                RGSfilteredTable = RGSrows.CopyToDataTable();
            }
            else
            {
                RGSfilteredTable = originalTable.Clone();
            }

            // Filter rows for FSDetailsId
            DataTable FSfilteredTable = null;
            var FSrows = originalTable.AsEnumerable().Where(row => !row.IsNull("FSDetailsId"));
            if (FSrows.Any())
            {
                FSfilteredTable = FSrows.CopyToDataTable();
            }
            else
            {
                FSfilteredTable = originalTable.Clone();
            }

            // Filter rows for HBTDetailsId
            DataTable HBTfilteredTable = null;
            var HBTrows = originalTable.AsEnumerable().Where(row => !row.IsNull("HBTDetailsId"));
            if (HBTrows.Any())
            {
                HBTfilteredTable = HBTrows.CopyToDataTable();
            }
            else
            {
                HBTfilteredTable = originalTable.Clone();
            }

            //Bind RGS GridView
            if (RGSfilteredTable.Rows.Count > 0)
            {
                gvRGSDetails.DataSource = RGSfilteredTable;
                gvRGSDetails.DataBind();
                gvRGSDetails.Visible = true;
                spnrgs.Visible = true;
            }
            else
            {
                gvRGSDetails.DataSource = null;
                gvRGSDetails.DataBind();
                gvRGSDetails.Visible = false;
                spnrgs.Visible = false;
            }

            //Bind FS GridView
            if (FSfilteredTable.Rows.Count > 0)
            {
                gvFSDetails.DataSource = FSfilteredTable;
                gvFSDetails.DataBind();
                gvFSDetails.Visible = true;
                spnfs.Visible = true;
            }
            else
            {
                gvFSDetails.DataSource = FSfilteredTable;
                gvFSDetails.DataBind();
                gvFSDetails.Visible = false;
                spnfs.Visible = false;
            }

            //Bind HBT GridView
            if (HBTfilteredTable.Rows.Count > 0)
            {
                gvHBTTestDetails.DataSource = HBTfilteredTable;
                gvHBTTestDetails.DataBind();
                gvHBTTestDetails.Visible = true;
                spnhbttest.Visible = true;
            }
            else
            {
                gvHBTTestDetails.DataSource = HBTfilteredTable;
                gvHBTTestDetails.DataBind();
                gvHBTTestDetails.Visible = false;
                spnhbttest.Visible = false;
            }

            trvl_btnSave.Visible = true;
            litxtPRM.Visible = true;
        }
        else
        {
            gvRGSDetails.DataSource = null;
            gvRGSDetails.DataBind();

            gvFSDetails.DataSource = null;
            gvFSDetails.DataBind();

            gvHBTTestDetails.DataSource = null;
            gvHBTTestDetails.DataBind();

            lblmessage.Text = "No records found.";
            spnrgs.Visible = false;
            spnfs.Visible = false;
            spnhbttest.Visible = false;
            trvl_btnSave.Visible = false;
            litxtPRM.Visible = false;

        }
        txtPRM.Text = dsABAPObjectPlanSubmitted.Tables[1].Rows.Count > 0 ? dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ProjectManager"].ToString().Trim() : "";
        hdnprojectManager.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows.Count > 0 ? dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ProjectManager"].ToString().Trim() : "";
        hdnprojectManagerEmail.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows.Count > 0 ? dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["projectManagerEmail"].ToString().Trim() : "";
        hdnprogramnager.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows.Count > 0 ? dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["programnager"].ToString().Trim() : "";
        hdnprogramnagerEmail.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows.Count > 0 ? dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["programnagerEmail"].ToString().Trim() : "";

    }

    private void getFunctionalConsultant()
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getConsultantMaster";

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");

            DDLFSFunctionalConsultant.DataSource = DS.Tables[0];
            DDLFSFunctionalConsultant.DataTextField = "Emp_Name";
            DDLFSFunctionalConsultant.DataValueField = "Emp_Code";
            DDLFSFunctionalConsultant.DataBind();
            DDLFSFunctionalConsultant.Items.Insert(0, new ListItem("Select Functional Consultant", "0"));


        }
        catch (Exception ex)
        {

        }
    }

    private void getABAPConsultantMaster()
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getABAPConsultantMaster";

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
            DDLFSFunctionalConsultant.DataSource = DS.Tables[0];
            DDLFSFunctionalConsultant.DataTextField = "Emp_Name";
            DDLFSFunctionalConsultant.DataValueField = "Emp_Code";
            DDLFSFunctionalConsultant.DataBind();
            DDLFSFunctionalConsultant.Items.Insert(0, new ListItem("Select ABAP Consultant", "0"));
        }
        catch (Exception ex)
        {

        }
    }

    public void DDLProjectLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            SqlParameter[] spars = new SqlParameter[5];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetPlanDetailBasedStageforChangeConsultant";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();

            if (HDProjectLocation.Value != "")
            {
                DDLProjectLocation.SelectedValue = HDProjectLocation.Value;
            }

            spars[2] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
            spars[2].Value = DDLProjectLocation.SelectedValue == "0" ? "" : DDLProjectLocation.SelectedValue;

            spars[3] = new SqlParameter("@StageValue", SqlDbType.VarChar);
            spars[3].Value = DDLStage.SelectedValue;

            spars[4] = new SqlParameter("@ABAPDevDesc", SqlDbType.VarChar);
            spars[4].Value = DDLABAPDevDesc.SelectedValue;

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
            if (DDLStage.SelectedValue != "")
            {
                if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0 && DDLABAPDevDesc.SelectedValue == "0")
                {
                    DDLABAPDevDesc.DataSource = DS.Tables[0];
                    DDLABAPDevDesc.DataTextField = "Development_Desc";
                    DDLABAPDevDesc.DataValueField = "ABAPODId";
                    DDLABAPDevDesc.DataBind();
                    DDLABAPDevDesc.Items.Insert(0, new ListItem("Select ABAP Development Desc", "0"));
                }

                if (DS != null && DS.Tables.Count > 0 && DS.Tables[1].Rows.Count > 0)
                {
                    DataTable originalTable = DS.Tables[1];
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

                        DataTable RGSfilteredTable = originalTable.AsEnumerable().Where(row => !row.IsNull("RGSDetailsId")).CopyToDataTable();

                        gvRGSDetails.DataSource = RGSfilteredTable;
                        gvRGSDetails.DataBind();
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

                        DataTable FSfilteredTable = originalTable.AsEnumerable().Where(row => !row.IsNull("FSDetailsId")).CopyToDataTable();

                        gvFSDetails.DataSource = FSfilteredTable;
                        gvFSDetails.DataBind();
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

                        DataTable ABAPfilteredTable = originalTable.AsEnumerable().Where(row => !row.IsNull("ABAPDetailsId")).CopyToDataTable();
                        gvABAPDevDetails.DataSource = ABAPfilteredTable;
                        gvABAPDevDetails.DataBind();
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

                        DataTable HBTfilteredTable = originalTable.AsEnumerable().Where(row => !row.IsNull("HBTDetailsId")).CopyToDataTable();
                        gvHBTTestDetails.DataSource = HBTfilteredTable;
                        gvHBTTestDetails.DataBind();
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

                        DataTable CTMfilteredTable = originalTable.AsEnumerable().Where(row => !row.IsNull("CTMDetailsId")).CopyToDataTable();
                        gvCTMTestDetails.DataSource = CTMfilteredTable;
                        gvCTMTestDetails.DataBind();
                    }

                    trvl_btnSave.Visible = true;
                    litxtPRM.Visible = true;
                    txtPRM.Text = DS.Tables[2].Rows.Count > 0 ? DS.Tables[2].Rows[0]["ProjectManager"].ToString().Trim() : "";

                    hdnprojectManager.Value = DS.Tables[2].Rows.Count > 0 ? DS.Tables[2].Rows[0]["ProjectManager"].ToString().Trim() : "";
                    hdnprojectManagerEmail.Value = DS.Tables[2].Rows.Count > 0 ? DS.Tables[2].Rows[0]["projectManagerEmail"].ToString().Trim() : "";
                    hdnprogramnager.Value = DS.Tables[2].Rows.Count > 0 ? DS.Tables[2].Rows[0]["programnager"].ToString().Trim() : "";
                    hdnprogramnagerEmail.Value = DS.Tables[2].Rows.Count > 0 ? DS.Tables[2].Rows[0]["programnagerEmail"].ToString().Trim() : "";
                }
                else
                {
                    DDLABAPDevDesc.Items.Clear();

                    gvRGSDetails.Visible = false;
                    gvFSDetails.Visible = false;
                    gvABAPDevDetails.Visible = false;
                    gvHBTTestDetails.Visible = false;
                    gvCTMTestDetails.Visible = false;

                    lblmessage.Text = "No records found.";
                    spnrgs.Visible = false;
                    spnfs.Visible = false;
                    spnabap.Visible = false;
                    spnhbttest.Visible = false;
                    spnctmtest.Visible = false;

                    trvl_btnSave.Visible = false;
                    DDLABAPDevDesc.Items.Insert(0, new ListItem("Select ABAP Development Desc", "0"));
                    litxtPRM.Visible = false;
                }
            }
            else
            {
                DDLABAPDevDesc.Items.Clear();

                gvRGSDetails.Visible = false;
                gvFSDetails.Visible = false;
                gvABAPDevDetails.Visible = false;
                gvHBTTestDetails.Visible = false;
                gvCTMTestDetails.Visible = false;


                lblmessage.Text = "No records found.";
                spnrgs.Visible = false;
                spnfs.Visible = false;
                spnabap.Visible = false;
                spnhbttest.Visible = false;
                spnctmtest.Visible = false;

                trvl_btnSave.Visible = false;
                DDLABAPDevDesc.Items.Insert(0, new ListItem("Select ABAP Development Desc", "0"));
                litxtPRM.Visible = false;
            }




        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return;
        }
    }

    public void DDLStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLProjectLocation.SelectedValue != "0")
        {
            DDLABAPDevDesc.Items.Clear();
            DDLABAPDevDesc.Items.Insert(0, new ListItem("Select ABAP Development Desc", "0"));
            if (DDLStage.SelectedValue == "ABAP")
            {
                getABAPConsultantMaster();
            }
            else
            {
                getFunctionalConsultant();
            }

            DDLProjectLocation_SelectedIndexChanged(sender, e);
        }
    }

    public void DDLABAPDevDesc_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDLProjectLocation_SelectedIndexChanged(sender, e);
    }

    protected void updatefs_btnSave_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }
        bool isChecked = false;
        string chkboxval = "";
        lbl_FSConsultant_Error.Text = "";
        if (Convert.ToString(DDLProjectLocation.SelectedValue).Trim() == "0" || Convert.ToString(DDLProjectLocation.SelectedValue).Trim() == "")
        {
            lbl_FSConsultant_Error.Text = "Please select Project Location.";
            return;
        }

        if (Convert.ToString(DDLFSFunctionalConsultant.SelectedValue).Trim() == "0" || Convert.ToString(DDLFSFunctionalConsultant.SelectedValue).Trim() == "")
        {
            lbl_FSConsultant_Error.Text = "Please select Functional Consultant.";
            return;
        }

        if (string.IsNullOrEmpty(txtRemarks.Text))
        {
            lblRemark.Text = "Please enter the remark.";
            return;
        }

        if (Convert.ToString(DDLStage.SelectedValue).Trim() == "RGS")
        {
            foreach (GridViewRow row in gvRGSDetails.Rows)
            {
                string RGSDetailsId = gvRGSDetails.DataKeys[row.RowIndex].Value.ToString();
                var FunctionalConsultant = Convert.ToString(DDLFSFunctionalConsultant.SelectedValue).ToString();

                var objdata = UpdateConsultantDB("ChangeRGS_FS_ABAP_HBT_CTM_Consultant", RGSDetailsId, FunctionalConsultant, "RGS", txtRemarks.Text);
                if (objdata != null || objdata.Tables.Count > 0 || objdata.Tables[0].Rows.Count > 0)
                {
                    if (objdata.Tables[0].Rows[0]["ResponseMsg"].ToString() == "Records udpate sucessfully.")
                    {
                        hdnNewRGSConsultantEmailId.Value = objdata.Tables[0].Rows[0]["NewRGSConsultantEmailId"].ToString();
                        hdnNewRGSConsultantName.Value = objdata.Tables[0].Rows[0]["NewRGSConsultantName"].ToString();

                        hdnOldRGSConsultantEmailId.Value = objdata.Tables[0].Rows[0]["OldRGSConsultantEmailId"].ToString();
                        hdnOldRGSConsultantName.Value = objdata.Tables[0].Rows[0]["OldFSConsultantName"].ToString();

                        #region Send Email 
                        string strSubject = "";
                        strSubject = DDLProjectLocation.SelectedItem.Text.ToString().Trim() + " ABAP Object " + HttpUtility.HtmlDecode(row.Cells[1].Text.ToString().Trim());
                        string Email_TO = hdnNewRGSConsultantEmailId.Value.ToString().Trim();
                        string Email_CC = hdnprogramnagerEmail.Value.ToString().Trim() + ";" + hdnOldRGSConsultantEmailId.Value.ToString().Trim();

                        StringBuilder strbuild = new StringBuilder();
                        strbuild.Append("<p style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'></p>");
                        strbuild.Append("<p>" + "Dear " + hdnNewRGSConsultantName.Value.ToString().Trim() + "</p>");
                        strbuild.Append("<p> This is to inform you that you have assigned to new object <b>" + HttpUtility.HtmlDecode(row.Cells[1].Text.ToString().Trim()) + "</b>. Please co-ordinatinate with <b>" + hdnOldRGSConsultantName.Value.ToString().Trim() + "</b>.</p>");

                        strbuild.Append("<br><br>This is an auto generated email, please do not reply!");

                        //spm.sendMail(Email_TO, strSubject, Convert.ToString(strbuild).Trim(), "", Email_CC);

                        #endregion
                    }
                }
            }
        }

        if (Convert.ToString(DDLStage.SelectedValue).Trim() == "FS")
        {
            foreach (GridViewRow row in gvFSDetails.Rows)
            {
                string FSDetailsId = gvFSDetails.DataKeys[row.RowIndex].Value.ToString();
                var FunctionalConsultant = Convert.ToString(DDLFSFunctionalConsultant.SelectedValue).ToString();
                var objdata = UpdateConsultantDB("ChangeRGS_FS_ABAP_HBT_CTM_Consultant", FSDetailsId, FunctionalConsultant, "FS", txtRemarks.Text);
                if (objdata != null || objdata.Tables.Count > 0 || objdata.Tables[0].Rows.Count > 0)
                {
                    if (objdata.Tables[0].Rows[0]["ResponseMsg"].ToString() == "Records udpate sucessfully.")
                    {
                        hdnNewFSConsultantEmailId.Value = objdata.Tables[0].Rows[0]["NewFSConsultantEmailId"].ToString();
                        hdnNewFSConsultantName.Value = objdata.Tables[0].Rows[0]["NewFSConsultantName"].ToString();

                        hdnOldFSConsultantEmailId.Value = objdata.Tables[0].Rows[0]["OldFSConsultantEmailId"].ToString();
                        hdnOldFSConsultantName.Value = objdata.Tables[0].Rows[0]["OldFSConsultantName"].ToString();

                        #region Send Email 
                        string strSubject = "";
                        strSubject = DDLProjectLocation.SelectedItem.Text.ToString().Trim() + " ABAP Object " + HttpUtility.HtmlDecode(row.Cells[1].Text.ToString().Trim());
                        string Email_TO = hdnNewFSConsultantEmailId.Value.ToString().Trim();
                        string Email_CC = hdnprogramnagerEmail.Value.ToString().Trim() + ";" + hdnOldFSConsultantEmailId.Value.ToString().Trim();

                        StringBuilder strbuild = new StringBuilder();
                        strbuild.Append("<p style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'></p>");
                        strbuild.Append("<p>" + "Dear " + hdnNewFSConsultantName.Value.ToString().Trim() + "</p>");
                        strbuild.Append("<p> This is to inform you that you have assigned to new object <b>" + HttpUtility.HtmlDecode(row.Cells[1].Text.ToString().Trim()) + "</b>. Please co-ordinatinate with <b>" + hdnOldFSConsultantName.Value.ToString().Trim() + "</b>.</p>");
                        strbuild.Append("<br><br>This is an auto generated email, please do not reply!");
                        //spm.sendMail(Email_TO, strSubject, Convert.ToString(strbuild).Trim(), "", Email_CC);

                        #endregion
                    }
                }
            }
        }

        if (Convert.ToString(DDLStage.SelectedValue).Trim() == "ABAP")
        {
            foreach (GridViewRow row in gvABAPDevDetails.Rows)
            {
                string ABAPDetailsId = gvABAPDevDetails.DataKeys[row.RowIndex].Value.ToString();
                var FunctionalConsultant = Convert.ToString(DDLFSFunctionalConsultant.SelectedValue).ToString();
                var objdata = UpdateConsultantDB("ChangeRGS_FS_ABAP_HBT_CTM_Consultant", ABAPDetailsId, FunctionalConsultant, "ABAP", txtRemarks.Text);
                if (objdata != null || objdata.Tables.Count > 0 || objdata.Tables[0].Rows.Count > 0)
                {
                    if (objdata.Tables[0].Rows[0]["ResponseMsg"].ToString() == "Records udpate sucessfully.")
                    {
                        hdnNewFSConsultantEmailId.Value = objdata.Tables[0].Rows[0]["NewABAPConsultantEmailId"].ToString();
                        hdnNewFSConsultantName.Value = objdata.Tables[0].Rows[0]["NewABAPConsultantName"].ToString();

                        hdnOldFSConsultantEmailId.Value = objdata.Tables[0].Rows[0]["OldABAPConsultantEmailId"].ToString();
                        hdnOldFSConsultantName.Value = objdata.Tables[0].Rows[0]["OldABAPConsultantName"].ToString();

                        #region Send Email 
                        string strSubject = "";
                        strSubject = DDLProjectLocation.SelectedItem.Text.ToString().Trim() + " ABAP Object " + HttpUtility.HtmlDecode(row.Cells[1].Text.ToString().Trim());
                        string Email_TO = hdnNewFSConsultantEmailId.Value.ToString().Trim();
                        string Email_CC = hdnprogramnagerEmail.Value.ToString().Trim() + ";" + hdnOldFSConsultantEmailId.Value.ToString().Trim();

                        StringBuilder strbuild = new StringBuilder();
                        strbuild.Append("<p style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'></p>");
                        strbuild.Append("<p>" + "Dear " + hdnNewFSConsultantName.Value.ToString().Trim() + "</p>");
                        strbuild.Append("<p> This is to inform you that you have assigned to new object <b>" + HttpUtility.HtmlDecode(row.Cells[1].Text.ToString().Trim()) + "</b>. Please co-ordinatinate with <b>" + hdnOldFSConsultantName.Value.ToString().Trim() + "</b>.</p>");
                        strbuild.Append("<br><br>This is an auto generated email, please do not reply!");
                        //spm.sendMail(Email_TO, strSubject, Convert.ToString(strbuild).Trim(), "", Email_CC);

                        #endregion
                    }
                }

            }
        }

        if (Convert.ToString(DDLStage.SelectedValue).Trim() == "HBT Testing")
        {
            foreach (GridViewRow row in gvHBTTestDetails.Rows)
            {
                string HBTTestDetailsId = gvHBTTestDetails.DataKeys[row.RowIndex].Value.ToString();
                var FunctionalConsultant = Convert.ToString(DDLFSFunctionalConsultant.SelectedValue).ToString();
                var objdata = UpdateConsultantDB("ChangeRGS_FS_ABAP_HBT_CTM_Consultant", HBTTestDetailsId, FunctionalConsultant, "HBT", txtRemarks.Text);
                if (objdata != null || objdata.Tables.Count > 0 || objdata.Tables[0].Rows.Count > 0)
                {
                    if (objdata.Tables[0].Rows[0]["ResponseMsg"].ToString() == "Records udpate sucessfully.")
                    {
                        hdnNewHBTConsultantEmailId.Value = objdata.Tables[0].Rows[0]["NewHBTConsultantEmailId"].ToString();
                        hdnNewHBTConsultantName.Value = objdata.Tables[0].Rows[0]["NewHBTConsultantName"].ToString();

                        hdnOldHBTConsultantEmailId.Value = objdata.Tables[0].Rows[0]["OldHBTConsultantEmailId"].ToString();
                        hdnOldHBTConsultantName.Value = objdata.Tables[0].Rows[0]["OldHBTConsultantName"].ToString();

                        #region Send Email 
                        string strSubject = "";
                        strSubject = DDLProjectLocation.SelectedItem.Text.ToString().Trim() + " ABAP Object " + HttpUtility.HtmlDecode(row.Cells[1].Text.ToString().Trim());
                        string Email_TO = hdnNewHBTConsultantEmailId.Value.ToString().Trim();
                        string Email_CC = hdnprogramnagerEmail.Value.ToString().Trim() + ";" + hdnOldHBTConsultantEmailId.Value.ToString().Trim();

                        StringBuilder strbuild = new StringBuilder();
                        strbuild.Append("<p style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'></p>");
                        strbuild.Append("<p>" + "Dear " + hdnNewHBTConsultantName.Value.ToString().Trim() + "</p>");
                        strbuild.Append("<p> This is to inform you that you have assigned to new object <b>" + HttpUtility.HtmlDecode(row.Cells[1].Text.ToString().Trim()) + "</b>. Please co-ordinatinate with <b>" + hdnOldHBTConsultantName.Value.ToString().Trim() + "</b>.</p>");

                        strbuild.Append("<br><br>This is an auto generated email, please do not reply!");

                        //spm.sendMail(Email_TO, strSubject, Convert.ToString(strbuild).Trim(), "", Email_CC);

                        #endregion
                    }
                }
            }
        }

        if (Convert.ToString(DDLStage.SelectedValue).Trim() == "CTM Testing")
        {
            foreach (GridViewRow row in gvCTMTestDetails.Rows)
            {
                string CTMDetailsId = gvCTMTestDetails.DataKeys[row.RowIndex].Value.ToString();
                var FunctionalConsultant = Convert.ToString(DDLFSFunctionalConsultant.SelectedValue).ToString();
                var objdata = UpdateConsultantDB("ChangeRGS_FS_ABAP_HBT_CTM_Consultant", CTMDetailsId, FunctionalConsultant, "CTM", txtRemarks.Text);
                if (objdata != null || objdata.Tables.Count > 0 || objdata.Tables[0].Rows.Count > 0)
                {
                    if (objdata.Tables[0].Rows[0]["ResponseMsg"].ToString() == "Records udpate sucessfully.")
                    {
                        hdnNewCTMConsultantEmailId.Value = objdata.Tables[0].Rows[0]["NewCTMConsultantEmailId"].ToString();
                        hdnNewCTMConsultantName.Value = objdata.Tables[0].Rows[0]["NewCTMConsultantName"].ToString();

                        hdnOldCTMConsultantEmailId.Value = objdata.Tables[0].Rows[0]["OldCTMConsultantEmailId"].ToString();
                        hdnOldCTMConsultantName.Value = objdata.Tables[0].Rows[0]["OldCTMConsultantName"].ToString();

                        #region Send Email 
                        string strSubject = "";
                        strSubject = DDLProjectLocation.SelectedItem.Text.ToString().Trim() + " ABAP Object " + HttpUtility.HtmlDecode(row.Cells[1].Text.ToString().Trim());
                        string Email_TO = hdnNewCTMConsultantEmailId.Value.ToString().Trim();
                        string Email_CC = hdnprogramnagerEmail.Value.ToString().Trim() + ";" + hdnOldCTMConsultantEmailId.Value.ToString().Trim();

                        StringBuilder strbuild = new StringBuilder();
                        strbuild.Append("<p style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'></p>");
                        strbuild.Append("<p>" + "Dear " + hdnNewCTMConsultantName.Value.ToString().Trim() + "</p>");
                        strbuild.Append("<p> This is to inform you that you have assigned to new object <b>" + HttpUtility.HtmlDecode(row.Cells[1].Text.ToString().Trim()) + "</b>. Please co-ordinatinate with <b>" + hdnOldCTMConsultantName.Value.ToString().Trim() + "</b>.</p>");
                        strbuild.Append("<br><br>This is an auto generated email, please do not reply!");
                        //spm.sendMail(Email_TO, strSubject, Convert.ToString(strbuild).Trim(), "", Email_CC);

                        #endregion
                    }
                }


            }
        }

        Response.Redirect("~/procs/ABAP_Object_Tracker_Index.aspx");

    }

    public DataSet UpdateConsultantDB(string qtype, string DetailsId, string FConsultantId, string objStage, string Remark)
    {
        DataSet dsData = new DataSet();
        try
        {
            List<SqlParameter> spars = new List<SqlParameter>
            {
                new SqlParameter("@qtype", SqlDbType.VarChar) { Value = qtype },
                new SqlParameter("@DetailsId", SqlDbType.VarChar) { Value = DetailsId },
                new SqlParameter("@FConsultantId", SqlDbType.VarChar) { Value = FConsultantId },
                new SqlParameter("@CreatedBy", SqlDbType.VarChar) { Value = Session["Empcode"].ToString().Trim() },
                new SqlParameter("@objStage", SqlDbType.VarChar) { Value = objStage },
                new SqlParameter("@Remark", SqlDbType.VarChar) { Value = Remark },
            };

            dsData = spm.getDatasetList(spars.ToArray(), "SP_ABAPObjectTracking");
            return dsData;
        }
        catch (Exception e)
        {
            lbl_FSConsultant_Error.Text = e.Message.ToString();
            lbl_FSConsultant_Error.Visible = true;
        }

        return dsData;
    }

    #endregion
}