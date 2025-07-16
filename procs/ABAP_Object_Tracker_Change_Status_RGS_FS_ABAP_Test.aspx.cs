using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class procs_ABAP_Object_Tracker_Change_Status_RGS_FS_ABAP_Test : System.Web.UI.Page
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

            Txt_RevisedActualPreparationStart.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            txt_RevisedActualPreparationFinish.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            Txt_RevisedActualSubmission.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            txt_RevisedActualApproval.Attributes.Add("onkeypress", "return noanyCharecters(event);");

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
                    //get_ABAP_Object_Submitted_Plan_FSDetails();
                    GetWeekStartDateEndDate();

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
    //private void getProjectLocation()
    //{
    //    try
    //    {
    //        var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();

    //        SqlParameter[] spars = new SqlParameter[2];
    //        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
    //        spars[0].Value = "getUploadedLocationMaster";

    //        spars[1] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
    //        spars[1].Value = getCreatedBy;

    //        DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");

    //        if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
    //        {
    //            DDLProjectLocation.DataSource = DS.Tables[0];
    //            DDLProjectLocation.DataTextField = "Location_name";
    //            DDLProjectLocation.DataValueField = "comp_code";
    //            DDLProjectLocation.DataBind();
    //            DDLProjectLocation.Items.Insert(0, new ListItem("Select Project Location", "0"));
    //        }
    //        else
    //        {
    //            DDLProjectLocation.Items.Insert(0, new ListItem("Select Project Location", "0"));
    //            lbl_error.Text = "Error";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lbl_error.Text = ex.Message.ToString();
    //        return;
    //    }
    //}

    private void getProjectLocation()
    {
        try
        {
            var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();

            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetLocationForFunctionalConsultant";

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
                    rgs.Visible = false;
                    fs.Visible = false;
                    ABAP.Visible = false;
                    hbttest.Visible = false;
                    ctmtesting.Visible = false;
                    btn_SumitTimesheet.Visible = false;

                    gvRGSDetails.Visible = false;
                    gvFSDetails.Visible = false;
                    gvHBTTestDetails.Visible = false;
                    gvABAPDevDetails.Visible = false;
                    liProjectMangaer.Visible = true;
                    HDProjectLocation.Value = DS.Tables[0].Rows[0]["comp_code"].ToString();
                    get_ABAP_Object_Submitted_Plan_FSDetails();
                }
            }
            else
            {
                DDLProjectLocation.Items.Insert(0, new ListItem("Select Project Location", "0"));
                lbl_error.Text = "Change status of RGS/FS/HBT is restricted to RGS/FS/HBT Users only.";
            }
        }
        catch (Exception ex)
        {
            lbl_error.Text = ex.Message.ToString();
            return;
        }
    }

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        lblmessagesub.Text = "";
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        HDID.Value = Convert.ToString(gvRGSDetails.DataKeys[row.RowIndex].Values[0]).Trim();
        getStatusRGS("RGS");
        get_RGS_Detail();
        HDStatus.Value = "RGS";
        DivIDPopup.Visible = true;
        liupload_1.Visible = false;
        liupload_2.Visible = false;
        liupload_3.Visible = false;

        li_ddl_stage_1.Visible = false;
        li_ddl_stage_2.Visible = false;
        li_ddl_stage_3.Visible = false;

        lbl1.Text = "Revised / Actual Preparation Start Date";
        lbl2.Text = "Revised / Actual Preparation Finish Date";
        lbl3.Text = "Revised / Actual Submission Date";
        lbl4.Text = "Revised / Actual Approval Date";

        Label1.Text = "Planned Preparation Start Date";
        Label2.Text = "Planned Preparation Finish Date";

        PlnSubmitDate.Visible = true;
        PlnApprDate.Visible = true;
        PlnEmptyDate.Visible = true;


        liUploadCTMTestCase.Visible = false;
        liUploadUATSingOff.Visible = false;
        liUploadCTMTestCase_3.Visible = false;

        //lbl5.Text = "RGS Status";
        this.ModalPopupExtenderIRSheet.Show();

    }

    protected void lnkEdit_Click1(object sender, ImageClickEventArgs e)
    {
        lblmessagesub.Text = "";
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        HDID.Value = Convert.ToString(gvFSDetails.DataKeys[row.RowIndex].Values[0]).Trim();
        HDStatus.Value = "FS";
        getStatusRGS("FS");
        get_FS_Detail();
        //lblmessagesub.Text = lblmessagesub.Text != "" ? lblmessagesub.Text : "";
        DivIDPopup.Visible = true;
        lbl1.Text = "Revised Start Date";
        lbl2.Text = "Revised Finish Date";
        lbl3.Text = "Actual Start Date";
        lbl4.Text = "Actual Finish Date";
        //lbl5.Text = "FS Status";
        Label1.Text = "Planned Start Date";
        Label2.Text = "Planned Finish Date";
        PlnSubmitDate.Visible = false;
        PlnApprDate.Visible = false;
        PlnEmptyDate.Visible = false;

        li_ddl_stage_1.Visible = false;
        li_ddl_stage_2.Visible = false;
        li_ddl_stage_3.Visible = false;
        liupload_1.Visible = false;
        liupload_2.Visible = false;
        liupload_3.Visible = false;

        liUploadCTMTestCase.Visible = false;
        liUploadUATSingOff.Visible = false;
        liUploadCTMTestCase_3.Visible = false;
        this.ModalPopupExtenderIRSheet.Show();
    }

    protected void lnkEdit_Click2(object sender, ImageClickEventArgs e)
    {
        lblmessagesub.Text = "";
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        HDID.Value = Convert.ToString(gvABAPDevDetails.DataKeys[row.RowIndex].Values[0]).Trim();
        HDStatus.Value = "ABAP";
        getStatusRGS("ABAP");
        get_ABAP_Detail();
        DivIDPopup.Visible = true;
        lbl1.Text = "Revised Start Date";
        lbl2.Text = "Revised Finish Date";
        lbl3.Text = "Actual Start Date";
        lbl4.Text = "Actual Finish Date";
        //lbl5.Text = "Development Status";
        Label1.Text = "Planned Start Date";
        Label2.Text = "Planned Finish Date";
        PlnSubmitDate.Visible = false;
        PlnApprDate.Visible = false;
        PlnEmptyDate.Visible = false;
        liupload_1.Visible = false;
        liupload_2.Visible = false;
        liupload_3.Visible = false;
        li_ddl_stage_1.Visible = false;
        li_ddl_stage_2.Visible = false;
        li_ddl_stage_3.Visible = false;

        liUploadCTMTestCase.Visible = false;
        liUploadUATSingOff.Visible = false;
        liUploadCTMTestCase_3.Visible = false;

       // this.ModalPopupExtenderIRSheet.Show();
    }

    protected void lnkEdit_Click3(object sender, ImageClickEventArgs e)
    {
        lblmessagesub.Text = "";
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        HDID.Value = Convert.ToString(gvHBTTestDetails.DataKeys[row.RowIndex].Values[0]).Trim();
        HDStatus.Value = "HBTTest";
        getStatusRGS("HBTTest");
        get_HBTTesting_Detail();

        DivIDPopup.Visible = true;
        lbl1.Text = "Revised Start Date";
        lbl2.Text = "Revised Finish Date";
        lbl3.Text = "Actual Start Date";
        lbl4.Text = "Actual Finish Date";
        //lbl5.Text = "HBT Testing Status<br />";
        Label1.Text = "Planned Start Date";
        Label2.Text = "Planned Finish Date";
        PlnSubmitDate.Visible = false;
        PlnApprDate.Visible = false;
        PlnEmptyDate.Visible = false;
        liupload_1.Visible = false;
        liupload_2.Visible = false;
        liupload_3.Visible = false;

        liUploadCTMTestCase.Visible = false;
        liUploadUATSingOff.Visible = false;
        liUploadCTMTestCase_3.Visible = false;

        this.ModalPopupExtenderIRSheet.Show();
    }

    protected void lnkEdit_Click4(object sender, ImageClickEventArgs e)
    {
        lblmessagesub.Text = "";
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        HDID.Value = Convert.ToString(gvCTMTestDetails.DataKeys[row.RowIndex].Values[0]).Trim();
        HDStatus.Value = "CTMTest";
        getStatusRGS("CTMTest");
        get_CTMTesting_Detail();
        DivIDPopup.Visible = true;
        lbl1.Text = "Revised Start Date";
        lbl2.Text = "Revised Finish Date";
        lbl3.Text = "Actual Start Date";
        lbl4.Text = "Actual Finish Date";
        //lbl5.Text = "CTM Testing Status<br />";
        Label1.Text = "Planned Start Date";
        Label2.Text = "Planned Finish Date";
        PlnSubmitDate.Visible = false;
        PlnApprDate.Visible = false;
        PlnEmptyDate.Visible = false;
        liupload_1.Visible = false;
        liupload_2.Visible = false;
        liupload_3.Visible = false;
        liUploadCTMTestCase.Visible = false;
        liUploadUATSingOff.Visible = false;
        liUploadCTMTestCase_3.Visible = false;
        this.ModalPopupExtenderIRSheet.Show();
    }

    public void get_RGS_Detail()
    {

        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetRGS_DetailBYID";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = (Session["Empcode"]).ToString().Trim();

        spars[2] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
        spars[2].Value = DDLProjectLocation.SelectedValue == "0" ? "" : DDLProjectLocation.SelectedValue;

        spars[3] = new SqlParameter("@ID", SqlDbType.Int);
        spars[3].Value = Convert.ToInt32(HDID.Value);

        dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");


        Txt_DevelopmentDescription.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Development_Desc"]);
        Txt_Module.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ModuleDesc"]);
        Txt_Interface.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Interface"]);
        Txt_FRICECategory.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FCategoryName"]);
        Txt_Order.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Priority"]);
        Txt_Priority.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PriorityName"]);
        Txt_Complexity.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ComplexityName"]);


        AllStatus.SelectedValue = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RGSStatus"]);
        if (AllStatus.SelectedValue == "8" && !string.IsNullOrEmpty(AllStatus.SelectedValue))
        {
            AllStatus.Enabled = false;
        }
        else
        {
            AllStatus.Enabled = true;
        }

        HDStatusCheck.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RGSStatus"]);
        HDStatusCheckFlag.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CheckStatusName"]);

        Txt_PlannedPreparationStart.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PlannedPrepStart"]);
        txt_PlannedPreparationFinish.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PlannedPrepFinish"]);
        Txt_PlannedSubmission.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PlannedSubmit"]);
        txt_PlannedApproval.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PlannedApprove"]);

        Txt_RevisedActualPreparationStart.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RevActualPrepStart"]);
        txt_RevisedActualPreparationFinish.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RevActualPrepFinish"]);
        Txt_RevisedActualSubmission.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RevActualSubmit"]);
        txt_RevisedActualApproval.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RevActualApprove"]);

        HDDateCheck1.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RevActualPrepStart"]);
        HDDateCheck2.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RevActualPrepFinish"]);
        HDDateCheck3.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RevActualSubmit"]);
        HDDateCheck4.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RevActualApprove"]);

        Txt_Remarks.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);
        HDRemarkcheck.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);
        Txt_RevisedActualSubmission.Enabled = true;
        LinkBtnSavePopup.Visible = true;

        if (HDStatusCheckFlag.Value == "Submitted")
        {
            Txt_RevisedActualSubmission.Enabled = false;
        }

        if (HDStatusCheckFlag.Value == "Completed" && !string.IsNullOrEmpty(txt_RevisedActualApproval.Text))
        {
            LinkBtnSavePopup.Visible = false;
        }
    }

    public void get_FS_Detail()
    {

        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetFS_DetailBYID";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = (Session["Empcode"]).ToString().Trim();

        spars[2] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
        spars[2].Value = DDLProjectLocation.SelectedValue == "0" ? "" : DDLProjectLocation.SelectedValue;

        spars[3] = new SqlParameter("@ID", SqlDbType.Int);
        spars[3].Value = Convert.ToInt32(HDID.Value);

        dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");

        Txt_DevelopmentDescription.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Development_Desc"]);
        Txt_Module.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ModuleDesc"]);
        Txt_Interface.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Interface"]);
        Txt_FRICECategory.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FCategoryName"]);
        Txt_Order.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Priority"]);
        Txt_Priority.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PriorityName"]);
        Txt_Complexity.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ComplexityName"]);
        if (AllStatus.Items.FindByValue(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FSStatus"].ToString()) != null)
        {
            AllStatus.SelectedValue = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FSStatus"]);
        }
        else
        {
            AllStatus.Items.Clear();
            AllStatus.Items.Add(new ListItem(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FSStatuss"]), Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FSStatus"])));
            AllStatus.Items.Add(new ListItem("Hold", "4"));
            AllStatus.Items.Add(new ListItem("Completed", "8"));

            AllStatus.SelectedValue = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FSStatus"]);
        }

        if (AllStatus.SelectedValue == "8" && !string.IsNullOrEmpty(AllStatus.SelectedValue))
        {
            AllStatus.Enabled = false;
            liupload_1.Visible = false;
            liupload_2.Visible = false;
            liupload_3.Visible = false;
        }
        else
        {
            AllStatus.Enabled = true;
        }
        HDStatusCheck.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FSStatus"]);
        HDStatusCheckFlag.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CheckStatusName"]);

        Txt_PlannedPreparationStart.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PlannedStart"]);
        txt_PlannedPreparationFinish.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PlannedFinish"]);

        Txt_RevisedActualPreparationStart.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FSRevisedStart"]);
        txt_RevisedActualPreparationFinish.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FSRevisedFinish"]);
        Txt_RevisedActualSubmission.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FSActualStart"]);
        txt_RevisedActualApproval.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FSActualFinish"]);

        HDDateCheck1.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FSRevisedStart"]);
        HDDateCheck2.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FSRevisedFinish"]);
        HDDateCheck3.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FSActualStart"]);
        HDDateCheck4.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FSActualFinish"]);
        Txt_Remarks.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);
        HDRemarkcheck.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);



        string stropennotopen = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Checkflag"]);
        if (stropennotopen == "open")
        {
            if (HDStatusCheckFlag.Value == "Completed" && !string.IsNullOrEmpty(HDDateCheck4.Value))
            {
                LinkBtnSavePopup.Visible = false;
            }
            else
            {
                LinkBtnSavePopup.Visible = true;
            }
        }
        else
        {
            lblmessagesub.Text = "This object cannot be updated because the previous stage has not yet been marked as completed.";
            LinkBtnSavePopup.Visible = false;
        }
    }

    public void get_ABAP_Detail()
    {

        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetABAP_DetailBYID";

        spars[1] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
        spars[1].Value = (Session["Empcode"]).ToString().Trim();

        spars[2] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
        spars[2].Value = DDLProjectLocation.SelectedValue == "0" ? "" : DDLProjectLocation.SelectedValue;

        spars[3] = new SqlParameter("@ID", SqlDbType.Int);
        spars[3].Value = Convert.ToInt32(HDID.Value);

        dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");

        if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
        {
            hdnABAPDetailsId.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPDetailsId"]);
            Txt_DevelopmentDescription.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Development_Desc"]);
            Txt_Module.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ModuleDesc"]);
            Txt_Interface.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Interface"]);
            Txt_FRICECategory.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FCategoryName"]);
            Txt_Order.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Priority"]);
            Txt_Priority.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PriorityName"]);
            Txt_Complexity.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ComplexityName"]);
            List<ListItem> itemsToRemove = new List<ListItem>();
            if (string.IsNullOrEmpty(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPFunctionalStatus"])) || Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPFunctionalStatus"]) == "21")
            {
                foreach (ListItem item in AllStatus.Items)
                {
                    if (item.Value == "2" || item.Value == "10")
                    {
                        itemsToRemove.Add(item);
                    }
                }

                foreach (ListItem item in itemsToRemove)
                {
                    AllStatus.Items.Remove(item);
                }

                Txt_RevisedActualPreparationStart.Enabled = false;
                txt_RevisedActualPreparationFinish.Enabled = false;
                Txt_RevisedActualSubmission.Enabled = false;
                txt_RevisedActualApproval.Enabled = false;
                CalendarExtender1.Enabled = false;
                CalendarExtender2.Enabled = false;
                CalendarExtender3.Enabled = false;
                CalendarExtender4.Enabled = false;

                if (!string.IsNullOrEmpty(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPFunctionalStatus"])))
                {
                    AllStatus.SelectedValue = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPFunctionalStatus"]);

                }
            }
            else if (string.IsNullOrEmpty(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPDevStatus"])) || Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPDevStatus"]) == "18")
            {
                AllStatus.Items.Clear();
                AllStatus.Items.Add(new ListItem(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPDevStatuss"]), Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPDevStatus"])));
                AllStatus.Items.Add(new ListItem("Functional Testing", "10"));

                AllStatus.SelectedValue = Convert.ToString(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPDevStatus"]));
            }
            else
            {
                foreach (ListItem item in AllStatus.Items)
                {
                    if (item.Value == "20" || item.Value == "21")
                    {
                        itemsToRemove.Add(item);
                    }
                }

                foreach (ListItem item in itemsToRemove)
                {
                    AllStatus.Items.Remove(item);

                }

                AllStatus.SelectedValue = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPDevStatus"]);
                Txt_RevisedActualPreparationStart.Enabled = true;
                txt_RevisedActualPreparationFinish.Enabled = true;
                Txt_RevisedActualSubmission.Enabled = true;
                txt_RevisedActualApproval.Enabled = true;
                CalendarExtender1.Enabled = true;
                CalendarExtender1.Enabled = true;
                CalendarExtender2.Enabled = true;
                CalendarExtender3.Enabled = true;
                CalendarExtender4.Enabled = true;
            }


            if (AllStatus.SelectedValue == "10" && !string.IsNullOrEmpty(AllStatus.SelectedValue))
            {
                AllStatus.Enabled = false;
            }
            else
            {
                AllStatus.Enabled = true;
            }
            HDStatusCheck.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPDevStatus"]);
            HDStatusCheckFlag.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CheckStatusName"]);

            Txt_PlannedPreparationStart.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PlannedStart"]);
            txt_PlannedPreparationFinish.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PlannedFinish"]);


            Txt_RevisedActualPreparationStart.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPRevisedStart"]);
            txt_RevisedActualPreparationFinish.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPRevisedFinish"]);
            Txt_RevisedActualSubmission.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPActualStart"]);
            txt_RevisedActualApproval.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPActualFinish"]);

            HDDateCheck1.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPRevisedStart"]);
            HDDateCheck2.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPRevisedFinish"]);
            HDDateCheck3.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPActualStart"]);
            HDDateCheck4.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPActualFinish"]);
            Txt_Remarks.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);
            HDRemarkcheck.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);
            string stropennotopen = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Checkflag"]);
            if (stropennotopen == "open")
            {
                if (HDStatusCheckFlag.Value == "Functional Testing" && !string.IsNullOrEmpty(HDDateCheck4.Value))
                {
                    LinkBtnSavePopup.Visible = false;
                }
                else
                {
                    LinkBtnSavePopup.Visible = true;
                }
            }
            else
            {
                lblmessagesub.Text = "This object cannot be updated because the previous stage has not yet been marked as Completed.";
                LinkBtnSavePopup.Visible = false;
            }

            //lnkDownload.CommandArgument = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FileNames"]);
            var files = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FileNames"]);
            if (!string.IsNullOrEmpty(files))
            {
                string[] separateFiles = files.Split(',');
                DataSet objdd = new DataSet();

                DataTable table = new DataTable("MyTable");
                objdd.Tables.Add(table);

                table.Columns.Add("Srno", typeof(int));
                table.Columns.Add("FileName", typeof(string));

                for (int i = 0; i < separateFiles.Length; i++)  // Index starts from 0
                {
                    DataRow row = table.NewRow();
                    row["Srno"] = i + 1;
                    row["FileName"] = separateFiles[i];
                    table.Rows.Add(row);
                }

                gvuploadedFiles.DataSource = null;
                gvuploadedFiles.DataBind();
                gvuploadedFiles.DataSource = objdd.Tables[0];
                gvuploadedFiles.DataBind();
            }
        }
    }

    public void get_HBTTesting_Detail()
    {

        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetHBTTest_DetailBYID";

        spars[1] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
        spars[1].Value = (Session["Empcode"]).ToString().Trim();

        spars[2] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
        spars[2].Value = DDLProjectLocation.SelectedValue == "0" ? "" : DDLProjectLocation.SelectedValue;

        spars[3] = new SqlParameter("@ID", SqlDbType.Int);
        spars[3].Value = Convert.ToInt32(HDID.Value);

        dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");

        Txt_DevelopmentDescription.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Development_Desc"]);
        Txt_Module.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ModuleDesc"]);
        Txt_Interface.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Interface"]);
        Txt_FRICECategory.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FCategoryName"]);
        Txt_Order.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Priority"]);
        Txt_Priority.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PriorityName"]);
        Txt_Complexity.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ComplexityName"]);
        AllStatus.SelectedValue = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"]);
        if (AllStatus.SelectedValue == "10" && !string.IsNullOrEmpty(AllStatus.SelectedValue))
        {
            AllStatus.Enabled = false;
        }
        else
        {
            AllStatus.Enabled = true;
        }
        if (AllStatus.SelectedValue == "18")
        {
            li_ddl_stage_1.Visible = true;
            li_ddl_stage_2.Visible = true;
            li_ddl_stage_3.Visible = true;
        }
        else
        {
            li_ddl_stage_1.Visible = false;
            li_ddl_stage_2.Visible = false;
            li_ddl_stage_3.Visible = false;
        }
        HDStatusCheck.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"]);
        HDStatusCheckFlag.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CheckStatusName"]);

        Txt_PlannedPreparationStart.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PlannedStart"]);
        txt_PlannedPreparationFinish.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PlannedFinish"]);

        Txt_RevisedActualPreparationStart.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["hbtRevisedStart"]);
        txt_RevisedActualPreparationFinish.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["hbtRevisedFinish"]);
        Txt_RevisedActualSubmission.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["hbtActualStart"]);
        txt_RevisedActualApproval.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["hbtActualFinish"]);

        HDDateCheck1.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["hbtRevisedStart"]);
        HDDateCheck2.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["hbtRevisedFinish"]);
        HDDateCheck3.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["hbtActualStart"]);
        HDDateCheck4.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["hbtActualFinish"]);
        Txt_Remarks.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);
        HDRemarkcheck.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);


        string stropennotopen = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Checkflag"]);
        if (stropennotopen == "open")
        {
            if (HDStatusCheckFlag.Value == "Functional Testing" && !string.IsNullOrEmpty(HDDateCheck4.Value))
            {
                LinkBtnSavePopup.Visible = false;
            }
            else
            {
                LinkBtnSavePopup.Visible = true;
            }
        }
        else
        {
            lblmessagesub.Text = "This object cannot be updated because the previous stage has not yet been marked as Functional Testing.";
            LinkBtnSavePopup.Visible = false;
        }
    }

    public void get_CTMTesting_Detail()
    {

        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetCTMTest_DetailBYID";

        spars[1] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
        spars[1].Value = (Session["Empcode"]).ToString().Trim();

        spars[2] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
        spars[2].Value = DDLProjectLocation.SelectedValue == "0" ? "" : DDLProjectLocation.SelectedValue;

        spars[3] = new SqlParameter("@ID", SqlDbType.Int);
        spars[3].Value = Convert.ToInt32(HDID.Value);

        dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");

        Txt_DevelopmentDescription.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Development_Desc"]);
        Txt_Module.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ModuleDesc"]);
        Txt_Interface.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Interface"]);
        Txt_FRICECategory.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FCategoryName"]);
        Txt_Order.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Priority"]);
        Txt_Priority.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PriorityName"]);
        Txt_Complexity.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ComplexityName"]);
        if (AllStatus.Items.FindByValue(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMStatus"].ToString()) != null)
        {
            AllStatus.SelectedValue = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMStatus"]);
        }
        else
        {
            AllStatus.Items.Clear();
            AllStatus.Items.Add(new ListItem(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMTestStatusName"]), Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMStatus"])));
        }

        if (AllStatus.SelectedValue == "17" && !string.IsNullOrEmpty(AllStatus.SelectedValue))
        {
            AllStatus.Enabled = false;
        }
        else
        {
            AllStatus.Enabled = true;
        }

        HDStatusCheck.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMStatus"]);
        HDStatusCheckFlag.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CheckStatusName"]);

        Txt_PlannedPreparationStart.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMPlannedStart"]);
        txt_PlannedPreparationFinish.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMPlannedFinish"]);

        Txt_RevisedActualPreparationStart.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMRevisedFinish"]);
        txt_RevisedActualPreparationFinish.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMRevisedFinish"]);
        Txt_RevisedActualSubmission.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMActualStart"]);
        txt_RevisedActualApproval.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMActualFinish"]);

        HDDateCheck1.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMRevisedFinish"]);
        HDDateCheck2.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMRevisedFinish"]);
        HDDateCheck3.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMActualStart"]);
        HDDateCheck4.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMActualFinish"]);
        Txt_Remarks.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);
        HDRemarkcheck.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);


        string stropennotopen = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Checkflag"]);
        if (stropennotopen == "open")
        {
            if (HDStatusCheckFlag.Value == "Test Case Approved" && !string.IsNullOrEmpty(HDDateCheck4.Value))
            {
                LinkBtnSavePopup.Visible = false;
            }
            else
            {
                LinkBtnSavePopup.Visible = true;
            }
        }
        else
        {
            lblmessagesub.Text = "This object cannot be updated because the previous stage has not yet been marked as Test Case Approved.";
            LinkBtnSavePopup.Visible = false;
        }
    }

    private void getStatusRGS(string Str)
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[1];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getAllStatus";

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");

            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                if (Str == "RGS")
                {
                    AllStatus.DataSource = DS.Tables[0];
                }
                if (Str == "FS")
                {
                    AllStatus.DataSource = DS.Tables[1];
                }
                if (Str == "ABAP")
                {
                    AllStatus.DataSource = DS.Tables[2];
                }
                if (Str == "HBTTest")
                {
                    AllStatus.DataSource = DS.Tables[3];
                }
                if (Str == "CTMTest")
                {
                    AllStatus.DataSource = DS.Tables[4];
                }

                AllStatus.DataTextField = "StatusName";
                AllStatus.DataValueField = "StatusId";
                AllStatus.DataBind();
                AllStatus.Items.Insert(0, new ListItem("Select Status", "0"));
            }
        }
        catch (Exception ex)
        {
            lbl_error.Text = ex.Message.ToString();
            return;
        }
    }

    public void get_ABAP_Object_Submitted_Plan_FSDetails()
    {
        try
        {
            DataSet dsABAPObjectPlanSubmitted = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetRGSFSABAPHBTTestingDetailsByProjectLocation";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = (Session["Empcode"]).ToString().Trim();

            if (HDProjectLocation.Value != "")
            {
                DDLProjectLocation.SelectedValue = HDProjectLocation.Value;
            }

            spars[2] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
            spars[2].Value = DDLProjectLocation.SelectedValue == "0" ? "" : DDLProjectLocation.SelectedValue;

            dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");

            rgs.Visible = false;
            fs.Visible = false;
            ABAP.Visible = false;
            hbttest.Visible = false;
            ctmtesting.Visible = false;
            btn_SumitTimesheet.Visible = false;
            if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
            {
                rgs.Visible = true;
                gvRGSDetails.Visible = true;
                gvRGSDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvRGSDetails.DataBind();
            }

            if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[1].Rows.Count > 0)
            {
                fs.Visible = true;
                gvFSDetails.Visible = true;
                gvFSDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[1];
                gvFSDetails.DataBind();
            }

            if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[2].Rows.Count > 0)
            {
                hbttest.Visible = true;
                gvHBTTestDetails.Visible = true;
                gvHBTTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[2];
                gvHBTTestDetails.DataBind();
            }

            if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[3].Rows.Count > 0)
            {
                hdnABAPDetailsId.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[3].Rows[0]["ABAPDetailsId"]);
                string result = string.Join(",", dsABAPObjectPlanSubmitted.Tables[3].AsEnumerable()
                                       .Select(row => row["ABAPDetailsId"].ToString())
                                       .Where(abapDetailsId => !string.IsNullOrEmpty(abapDetailsId)));
                hdnCommaSeperABAPDetailsId.Value = result;

                ABAP.Visible = true;
                gvABAPDevDetails.Visible = true;
                btn_SumitTimesheet.Visible = true;
                gvABAPDevDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[3];
                gvABAPDevDetails.DataBind();
            }

            if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[4].Rows.Count > 0)
            {
                ctmtesting.Visible = true;
                gvCTMTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[4];
                gvCTMTestDetails.DataBind();
            }

            if (dsABAPObjectPlanSubmitted.Tables[5].Rows.Count == 1)
            {
                liProjectMangaer.Visible = true;
            }

            txtPRM.Text = dsABAPObjectPlanSubmitted.Tables[5].Rows.Count == 1 ? dsABAPObjectPlanSubmitted.Tables[5].Rows[0]["ProgramManager"].ToString().Trim() : "";
        }
        catch(Exception e)
        {

        }
    }

    public void DDLProjectLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvRGSDetails.Visible = false;
        gvFSDetails.Visible = false;
        gvHBTTestDetails.Visible = false;
        gvABAPDevDetails.Visible = false;
        liProjectMangaer.Visible = false;
        HDProjectLocation.Value = DDLProjectLocation.SelectedValue;
        get_ABAP_Object_Submitted_Plan_FSDetails();
    }

    protected void LinkBtnSavePopup_Click(object sender, EventArgs e)
    {
        lblmessagesub.Text = "";

        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }
        var FirstDate = "";
        var SecondDate = "";
        var ThirdDate = "";
        var FourthDate = "";
        var PlannedPreparationStart = "";
        var PlannedPreparationFinish = "";
        var PlannedSubmission = "";
        var PlannedApproval = "";
        DateTime PlannedSubmissiondt = new DateTime();
        DateTime PlannedApprovaldt = new DateTime();
        var ddlStageval = "";

        if (!string.IsNullOrEmpty(ddlStage.SelectedValue) || ddlStage.SelectedValue != "")
        {
            ddlStageval = ddlStage.SelectedValue;
        }

        var splitPlannedPreparationStart = Txt_PlannedPreparationStart.Text.Split('/');
        PlannedPreparationStart = splitPlannedPreparationStart[2] + "-" + splitPlannedPreparationStart[1] + "-" + splitPlannedPreparationStart[0];
        DateTime PlannedPreparationStartdt = DateTime.Parse(PlannedPreparationStart);

        var splitPlannedPreparationFinish = txt_PlannedPreparationFinish.Text.Split('/');
        PlannedPreparationFinish = splitPlannedPreparationFinish[2] + "-" + splitPlannedPreparationFinish[1] + "-" + splitPlannedPreparationFinish[0];
        DateTime PlannedPreparationFinishdt = DateTime.Parse(PlannedPreparationFinish);

        if (!string.IsNullOrEmpty(Txt_PlannedSubmission.Text))
        {
            var splitPlannedSubmission = Txt_PlannedSubmission.Text.Split('/');
            PlannedSubmission = splitPlannedSubmission[2] + "-" + splitPlannedSubmission[1] + "-" + splitPlannedSubmission[0];
            PlannedSubmissiondt = DateTime.Parse(PlannedSubmission);
        }

        if (!string.IsNullOrEmpty(txt_PlannedApproval.Text))
        {
            var splitPlannedApproval = txt_PlannedApproval.Text.Split('/');
            PlannedApproval = splitPlannedApproval[2] + "-" + splitPlannedApproval[1] + "-" + splitPlannedApproval[0];
            PlannedApprovaldt = DateTime.Parse(PlannedApproval);
        }



        if (Txt_RevisedActualPreparationStart.Text == "" && txt_RevisedActualPreparationFinish.Text != "")
        {
            DivIDPopup.Visible = true;
            PnlIrSheet.Visible = true;
            this.ModalPopupExtenderIRSheet.Show();
            if (HDStatus.Value == "RGS")
            {
                lblmessagesub.Text = "Select the Revised Actual Preparation Start Date.";
                return;
            }
            if (HDStatus.Value == "FS" || HDStatus.Value == "ABAP" || HDStatus.Value == "HBTTest" || HDStatus.Value == "CTMTest")
            {
                lblmessagesub.Text = "Select the Revised Start Date.";
                return;
            }
        }
        if (txt_RevisedActualPreparationFinish.Text == "" && Txt_RevisedActualPreparationStart.Text != "")
        {
            DivIDPopup.Visible = true;
            PnlIrSheet.Visible = true;
            this.ModalPopupExtenderIRSheet.Show();
            if (HDStatus.Value == "RGS")
            {
                lblmessagesub.Text = "Select the Revised Actual Preparation Finish Date.";
                return;
            }
            if (HDStatus.Value == "FS" || HDStatus.Value == "ABAP" || HDStatus.Value == "HBTTest" || HDStatus.Value == "CTMTest")
            {
                lblmessagesub.Text = "Select the Revised Finish Date";
                return;
            }
        }
        if (HDStatus.Value == "FS" || HDStatus.Value == "ABAP" || HDStatus.Value == "HBTTest" || HDStatus.Value == "CTMTest")
        {
            if (Txt_RevisedActualSubmission.Text == "" && txt_RevisedActualApproval.Text != "")
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Select the Actual Start Date.";
                return;
            }

            //if (txt_RevisedActualApproval.Text == "" && Txt_RevisedActualSubmission.Text != "")
            //{
            //    DivIDPopup.Visible = true;
            //    PnlIrSheet.Visible = true;
            //    this.ModalPopupExtenderIRSheet.Show();
            //    lblmessagesub.Text = "Select the Actual Finish Date.";
            //    return;
            //}
        }
        if (HDStatus.Value == "RGS")
        {
            //if (AllStatus.SelectedValue == "3")
            //{
            //if (Txt_RevisedActualSubmission.Text == "")
            //{
            //    DivIDPopup.Visible = true;
            //    PnlIrSheet.Visible = true;
            //    this.ModalPopupExtenderIRSheet.Show();
            //    lblmessagesub.Text = "Select Revised/ Actual Submission Date.";
            //    return;
            //}


            //}
            //if (AllStatus.SelectedValue == "7")
            //{
            //if (txt_RevisedActualApproval.Text == "")
            //{
            //    DivIDPopup.Visible = true;
            //    PnlIrSheet.Visible = true;
            //    this.ModalPopupExtenderIRSheet.Show();
            //    lblmessagesub.Text = "Select Revised/ Actual Approval Date.";
            //    return;
            //}
            //}
        }
        //if (txt_RevisedActualApproval.Text == "")
        //{
        //    lblmessagesub.Text = "Select the Date";
        //    return;
        //}
        if (AllStatus.SelectedValue == "0")
        {
            lblmessagesub.Text = "Select the Status";
            return;
        }
        if (Txt_RevisedActualPreparationStart.Text != "")
        {
            var splitDate = Txt_RevisedActualPreparationStart.Text.Split('/');
            FirstDate = splitDate[2] + "-" + splitDate[1] + "-" + splitDate[0];
        }
        if (Txt_RevisedActualPreparationStart.Text != "")
        {
            var splitDate = Txt_RevisedActualPreparationStart.Text.Split('/');
            FirstDate = splitDate[2] + "-" + splitDate[1] + "-" + splitDate[0];
        }

        if (txt_RevisedActualPreparationFinish.Text != "")
        {
            var splitDate1 = txt_RevisedActualPreparationFinish.Text.Split('/');
            SecondDate = splitDate1[2] + "-" + splitDate1[1] + "-" + splitDate1[0];
        }
        if (Txt_RevisedActualSubmission.Text != "")
        {
            var splitDate2 = Txt_RevisedActualSubmission.Text.Split('/');
            ThirdDate = splitDate2[2] + "-" + splitDate2[1] + "-" + splitDate2[0];
        }
        if (txt_RevisedActualApproval.Text != "")
        {
            var splitDate3 = txt_RevisedActualApproval.Text.Split('/');
            FourthDate = splitDate3[2] + "-" + splitDate3[1] + "-" + splitDate3[0];
        }

        if (!string.IsNullOrWhiteSpace(Txt_RevisedActualPreparationStart.Text) && !string.IsNullOrWhiteSpace(txt_RevisedActualPreparationFinish.Text))
        {
            DateTime startDate = DateTime.Parse(FirstDate);
            DateTime finishDate = DateTime.Parse(SecondDate);
             if (startDate > finishDate)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Revised Start Date must be earlier than Revised Finish Date.";
                return;
            }
        }
       

        if (HDStatus.Value == "FS" || HDStatus.Value == "ABAP" || HDStatus.Value == "HBTTest" || HDStatus.Value == "CTMTest")
        {
            if (!string.IsNullOrWhiteSpace(Txt_RevisedActualSubmission.Text) && !string.IsNullOrWhiteSpace(txt_RevisedActualApproval.Text))
            {
                DateTime startDate = DateTime.Parse(ThirdDate);
                DateTime finishDate = DateTime.Parse(FourthDate);
                if (startDate > finishDate)
                {
                    DivIDPopup.Visible = true;
                    PnlIrSheet.Visible = true;
                    this.ModalPopupExtenderIRSheet.Show();
                    lblmessagesub.Text = "Actual Start Date must be earlier than or equal to Actual Finish Date";
                    return;
                }
            }
            
        }

        if (AllStatus.SelectedValue == "8" && (HDStatus.Value == "FS"))
        {
            string filename = "";
            if (!uplFSAttachment.HasFile)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Please Upload the attachement.";
                return;
            }
            if (uplFSAttachment.HasFile && AllStatus.SelectedValue == "8")
            {
                string FSAttachmentFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ABAPObjectFS"]).Trim() + "/");
                bool folderExists = Directory.Exists(FSAttachmentFilePath);
                if (!folderExists)
                {
                    Directory.CreateDirectory(FSAttachmentFilePath);
                }
                int srno = 1;
                foreach (HttpPostedFile uploadedFile in uplFSAttachment.PostedFiles)
                {
                    filename = uploadedFile.FileName;

                    string fileExtension = System.IO.Path.GetExtension(filename);
                    string timestamp = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
                    filename = "FS_Attachment_" + timestamp + fileExtension;

                    uploadedFile.SaveAs(Path.Combine(FSAttachmentFilePath, filename));

                    string uploadedFilePath = FSAttachmentFilePath + filename;

                    string fullPath = System.IO.Path.GetFullPath(uploadedFilePath);

                    SqlParameter[] sparam = new SqlParameter[5];
                    sparam[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                    sparam[0].Value = "Upload_FS_Attachment";

                    sparam[1] = new SqlParameter("@ID", SqlDbType.Int);
                    sparam[1].Value = Convert.ToInt32(HDID.Value);

                    sparam[2] = new SqlParameter("@SrNo", SqlDbType.Int);
                    sparam[2].Value = Convert.ToInt32(srno);

                    sparam[3] = new SqlParameter("@FileType", SqlDbType.VarChar);
                    sparam[3].Value = "FS_Attachment";

                    sparam[4] = new SqlParameter("@filename", SqlDbType.VarChar);
                    sparam[4].Value = filename;


                    DataSet ObjDS = spm.getDatasetList(sparam, "SP_ABAPObjectTracking");
                    if (ObjDS == null)
                    {
                        DivIDPopup.Visible = true;
                        PnlIrSheet.Visible = true;
                        this.ModalPopupExtenderIRSheet.Show();
                        lblmessagesub.Text = "Error in Attchment Upload.";
                        return;
                    }
                    srno = srno + 1;
                }
            }
            else
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "When Status is completed then only you can upload attchament";
                return;
            }

            if (Txt_RevisedActualSubmission.Text == "" || txt_RevisedActualApproval.Text == "")
            {
                if (Txt_RevisedActualSubmission.Text == "")
                {
                    DivIDPopup.Visible = true;
                    PnlIrSheet.Visible = true;
                    this.ModalPopupExtenderIRSheet.Show();
                    lblmessagesub.Text = "You cannot set the status to Completed if the Actual Start Date has not been entered.";
                    return;
                }

                if (txt_RevisedActualApproval.Text == "")
                {
                    DivIDPopup.Visible = true;
                    PnlIrSheet.Visible = true;
                    this.ModalPopupExtenderIRSheet.Show();
                    lblmessagesub.Text = "You cannot set the status to Completed if the Actual Finish Date has not been entered.";
                    return;
                }
            }
        }

        if (AllStatus.SelectedValue == "8" && HDStatus.Value == "RGS")
        {
            if (Txt_RevisedActualSubmission.Text == "" || txt_RevisedActualApproval.Text == "")
            {
                if (Txt_RevisedActualSubmission.Text == "")
                {
                    DivIDPopup.Visible = true;
                    PnlIrSheet.Visible = true;
                    this.ModalPopupExtenderIRSheet.Show();
                    lblmessagesub.Text = "You cannot set the status to Completed if the Actual Submission Date has not been entered.";
                    return;
                }

                if (txt_RevisedActualApproval.Text == "")
                {
                    DivIDPopup.Visible = true;
                    PnlIrSheet.Visible = true;
                    this.ModalPopupExtenderIRSheet.Show();
                    lblmessagesub.Text = "You cannot set the status to Completed if the Actual Approval Date has not been entered.";
                    return;
                }
            }
        }

        if (AllStatus.SelectedValue == "10" && HDStatus.Value == "ABAP")
        {
            if (Txt_RevisedActualSubmission.Text == "" || txt_RevisedActualApproval.Text == "")
            {
                if (Txt_RevisedActualSubmission.Text == "")
                {
                    DivIDPopup.Visible = true;
                    PnlIrSheet.Visible = true;
                    this.ModalPopupExtenderIRSheet.Show();
                    lblmessagesub.Text = "You cannot set the status to Functional Testing if the Actual Start Date has not been entered.";
                    return;
                }

                if (txt_RevisedActualApproval.Text == "")
                {
                    DivIDPopup.Visible = true;
                    PnlIrSheet.Visible = true;
                    this.ModalPopupExtenderIRSheet.Show();
                    lblmessagesub.Text = "You cannot set the status to Functional Testing if the Actual Finish Date has not been entered.";
                    return;
                }
            }
        }

        if (AllStatus.SelectedValue == "17" && HDStatus.Value == "HBTTest")
        {
            if (Txt_RevisedActualSubmission.Text == "" || txt_RevisedActualApproval.Text == "")
            {
                if (Txt_RevisedActualSubmission.Text == "")
                {
                    DivIDPopup.Visible = true;
                    PnlIrSheet.Visible = true;
                    this.ModalPopupExtenderIRSheet.Show();
                    lblmessagesub.Text = "You cannot set the status to Test Case Approved if the Actual Start Date has not been entered.";
                    return;
                }

                if (txt_RevisedActualApproval.Text == "")
                {
                    DivIDPopup.Visible = true;
                    PnlIrSheet.Visible = true;
                    this.ModalPopupExtenderIRSheet.Show();
                    lblmessagesub.Text = "You cannot set the status to Test Case Approved if the Actual Finish Date has not been entered.";
                    return;
                }
            }
        }

        string CTMTestCasefilename = "";
        string UATSignOfffilename = "";
        if (AllStatus.SelectedValue == "16" && HDStatus.Value == "CTMTest")
        {
            CTMTestCasefilename = "";
            UATSignOfffilename = "";
            if (!uplCTMTestCases.HasFile)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Please Upload the CTM Test Cases.";
                return;
            }
            if (!uplUATSingOff.HasFile)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Please Upload the UAT Sing Off Attachment.";
                return;
            }

            if (uplCTMTestCases.HasFile && uplUATSingOff.HasFile && AllStatus.SelectedValue == "16")
            {
                //CTM Test Case file upload section 
                string CTMTestCasesFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ABAPObjectCTMTestCase"]).Trim() + "/");
                bool folderExists = Directory.Exists(CTMTestCasesFilePath);
                if (!folderExists)
                {
                    Directory.CreateDirectory(CTMTestCasesFilePath);
                }
                int srno = 1;
                foreach (HttpPostedFile uploadedFile in uplCTMTestCases.PostedFiles)
                {
                    CTMTestCasefilename = uploadedFile.FileName;

                    string fileExtension = System.IO.Path.GetExtension(CTMTestCasefilename);
                    string timestamp = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
                    CTMTestCasefilename = "CTMTestCase_" + timestamp + fileExtension;

                    uploadedFile.SaveAs(Path.Combine(CTMTestCasesFilePath, CTMTestCasefilename));

                    string uploadedFilePath = CTMTestCasesFilePath + CTMTestCasefilename;

                    string fullPath = System.IO.Path.GetFullPath(uploadedFilePath);

                    SqlParameter[] sparam = new SqlParameter[5];
                    sparam[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                    sparam[0].Value = "Upload_CTMTestCase_Attachment";

                    sparam[1] = new SqlParameter("@ID", SqlDbType.Int);
                    sparam[1].Value = Convert.ToInt32(HDID.Value);

                    sparam[2] = new SqlParameter("@SrNo", SqlDbType.Int);
                    sparam[2].Value = Convert.ToInt32(srno);

                    sparam[3] = new SqlParameter("@FileType", SqlDbType.VarChar);
                    sparam[3].Value = "CTMTestCase";

                    sparam[4] = new SqlParameter("@filename", SqlDbType.VarChar);
                    sparam[4].Value = CTMTestCasefilename;

                    DataSet ObjDS = spm.getDatasetList(sparam, "SP_ABAPObjectTracking");
                    if (ObjDS == null)
                    {
                        DivIDPopup.Visible = true;
                        PnlIrSheet.Visible = true;
                        this.ModalPopupExtenderIRSheet.Show();
                        lblmessagesub.Text = "Error in Attchment Upload.";
                        return;
                    }
                    srno = srno + 1;
                }


                //UAT Sign Off file upload section 
                string UATSingOffFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ABAPObjectUATSingOff"]).Trim() + "/");
                bool uatfolderExists = Directory.Exists(UATSingOffFilePath);
                if (!uatfolderExists)
                {
                    Directory.CreateDirectory(UATSingOffFilePath);
                }
                int srno1 = 1;
                foreach (HttpPostedFile uploadedFile in uplUATSingOff.PostedFiles)
                {
                    UATSignOfffilename = uploadedFile.FileName;

                    string fileExtension = System.IO.Path.GetExtension(UATSignOfffilename);
                    string timestamp = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
                    UATSignOfffilename = "UATSingOff_" + timestamp + fileExtension;

                    uploadedFile.SaveAs(Path.Combine(UATSingOffFilePath, UATSignOfffilename));
                    string uploadedFilePath = UATSingOffFilePath + UATSignOfffilename;
                    string fullPath = System.IO.Path.GetFullPath(uploadedFilePath);

                    SqlParameter[] sparam = new SqlParameter[5];
                    sparam[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                    sparam[0].Value = "Upload_UATSingOff_Attachment";

                    sparam[1] = new SqlParameter("@ID", SqlDbType.Int);
                    sparam[1].Value = Convert.ToInt32(HDID.Value);

                    sparam[2] = new SqlParameter("@SrNo", SqlDbType.Int);
                    sparam[2].Value = Convert.ToInt32(srno1);

                    sparam[3] = new SqlParameter("@FileType", SqlDbType.VarChar);
                    sparam[3].Value = "UATSingOff";

                    sparam[4] = new SqlParameter("@filename", SqlDbType.VarChar);
                    sparam[4].Value = UATSignOfffilename;

                    DataSet ObjDS = spm.getDatasetList(sparam, "SP_ABAPObjectTracking");
                    if (ObjDS == null)
                    {
                        DivIDPopup.Visible = true;
                        PnlIrSheet.Visible = true;
                        this.ModalPopupExtenderIRSheet.Show();
                        lblmessagesub.Text = "Error in Attchment Upload.";
                        return;
                    }
                    srno1 = srno1 + 1;
                }
            }
            else
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "When Status is Test Case Approval then only you can upload attchament";
                return;
            }


            if (Txt_RevisedActualSubmission.Text == "" || txt_RevisedActualApproval.Text == "")
            {
                if (Txt_RevisedActualSubmission.Text == "")
                {
                    DivIDPopup.Visible = true;
                    PnlIrSheet.Visible = true;
                    this.ModalPopupExtenderIRSheet.Show();
                    lblmessagesub.Text = "You cannot set the status to Send Case Approval if the Actual Start Date has not been entered.";
                    return;
                }

                if (txt_RevisedActualApproval.Text == "")
                {
                    DivIDPopup.Visible = true;
                    PnlIrSheet.Visible = true;
                    this.ModalPopupExtenderIRSheet.Show();
                    lblmessagesub.Text = "You cannot set the status to Send Case Approval if the Actual Finish Date has not been entered.";
                    return;
                }
            }
        }


        SqlParameter[] spars = new SqlParameter[13];
        //if (HDStatusCheck.Value == AllStatus.SelectedValue && Txt_RevisedActualPreparationStart.Text.Trim() == HDDateCheck1.Value && txt_RevisedActualPreparationFinish.Text.Trim() == HDDateCheck2.Value
        //    && Txt_RevisedActualSubmission.Text.Trim() == HDDateCheck3.Value && txt_RevisedActualApproval.Text.Trim() == HDDateCheck4.Value && Txt_Remarks.Text.Trim() == HDRemarkcheck.Value)
        //{
        //    DivIDPopup.Visible = false;
        //}
        //else
        //{
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "UpdateTable_RGS_FS_ABAP_HBTTest_CTMTest";
        spars[1] = new SqlParameter("@ID", SqlDbType.Int);
        spars[1].Value = Convert.ToInt32(HDID.Value);
        spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[2].Value = Session["Empcode"].ToString();
        spars[3] = new SqlParameter("@RevisedStart", SqlDbType.VarChar);
        if (FirstDate == "")
        {
            spars[3].Value = DBNull.Value;
        }
        else
        {
            spars[3].Value = FirstDate;
        }
        spars[4] = new SqlParameter("@RevisedFinish", SqlDbType.VarChar);
        if (SecondDate == "")
        {
            spars[4].Value = DBNull.Value;
        }
        else
        {
            spars[4].Value = SecondDate;
        }
        spars[5] = new SqlParameter("@ActualStart", SqlDbType.VarChar);

        if (ThirdDate == "")
        {
            spars[5].Value = DBNull.Value;
        }
        else
        {
            spars[5].Value = ThirdDate;
        }
        spars[6] = new SqlParameter("@ActualFinish", SqlDbType.VarChar);
        if (FourthDate == "")
        {
            spars[6].Value = DBNull.Value;
        }
        else
        {
            spars[6].Value = FourthDate;
        }
        spars[7] = new SqlParameter("@AllStatusID", SqlDbType.VarChar);
        spars[7].Value = AllStatus.SelectedValue;
        spars[8] = new SqlParameter("@Statustypecheck", SqlDbType.VarChar);
        spars[8].Value = HDStatus.Value;
        spars[9] = new SqlParameter("@Remark", SqlDbType.VarChar);
        spars[9].Value = Txt_Remarks.Text.Trim();

        spars[10] = new SqlParameter("@radiobuttonval", SqlDbType.VarChar);
        spars[10].Value = ddlStageval;


        spars[11] = new SqlParameter("@CTMTestCaseFile", SqlDbType.VarChar);
        spars[11].Value = CTMTestCasefilename;

        spars[12] = new SqlParameter("@UATSingOffFile", SqlDbType.VarChar);
        spars[12].Value = UATSignOfffilename;

        DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        DivIDPopup.Visible = false;

        get_ABAP_Object_Submitted_Plan_FSDetails();
        //}

    }

    public DataSet UpdateConsultantDB(string qtype, string DetailsId, string FConsultantId)
    {
        DataSet dsData = new DataSet();
        try
        {
            List<SqlParameter> spars = new List<SqlParameter>
            {
                new SqlParameter("@qtype", SqlDbType.VarChar) { Value = qtype },
                new SqlParameter("@DetailsId", SqlDbType.VarChar) { Value = DetailsId },
                new SqlParameter("@FConsultantId", SqlDbType.VarChar) { Value = FConsultantId },
                new SqlParameter("@CreatedBy", SqlDbType.VarChar) { Value = Session["Empcode"].ToString().Trim()
        },
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

    protected void LinkBtnBackPopup_Click(object sender, EventArgs e)
    {
        DivIDPopup.Visible = false;
    }

    protected void ibdownloadbtn_Click(object sender, EventArgs e)
    {
        ImageButton button = sender as ImageButton;
        if (button != null)
        {
            string commandArgument = button.CommandArgument;
            string filePath = Server.MapPath("~/ABAPTracker/FS_Attachment/" + commandArgument);

            if (System.IO.File.Exists(filePath))
            {
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + commandArgument);
                Response.WriteFile(filePath);
                Response.End();
            }
            else
            {
                Response.Write("File not found.");
            }
        }





    }


    protected void AllStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmessagesub.Text = "";
        string selectedValue = AllStatus.SelectedValue;
        if (HDStatus.Value == "FS" && selectedValue == "8")
        {
            liupload_1.Visible = true;
            liupload_2.Visible = true;
            liupload_3.Visible = true;
        }
        else
        {
            liupload_1.Visible = false;
            liupload_2.Visible = false;
            liupload_3.Visible = false;
        }

        if (HDStatus.Value == "HBTTest" && selectedValue == "18")
        {
            li_ddl_stage_1.Visible = true;
            li_ddl_stage_2.Visible = true;
            li_ddl_stage_3.Visible = true;
            HtmlGenericControl myList = (HtmlGenericControl)FindControl("editform1");
        }
        else
        {
            li_ddl_stage_1.Visible = false;
            li_ddl_stage_2.Visible = false;
            li_ddl_stage_3.Visible = false;
        }

        if (HDStatus.Value == "CTMTest" && selectedValue == "16")
        {
            liUploadCTMTestCase.Visible = true;
            liUploadUATSingOff.Visible = true;
            liUploadCTMTestCase_3.Visible = true;
        }
        else
        {
            liUploadCTMTestCase.Visible = false;
            liUploadUATSingOff.Visible = false;
            liUploadCTMTestCase_3.Visible = false;
        }

        DivIDPopup.Visible = true;
        this.ModalPopupExtenderIRSheet.Show();

    }






    public void GetWeekStartDateEndDate()
    {
        try
        {

            var getStartDate = spm.GetWeekStartDateEndDate(0);
            if (getStartDate.Rows.Count > 0)
            {
                var startDate = Convert.ToString(getStartDate.Rows[0]["StartDate"]);
                var endDate = Convert.ToString(getStartDate.Rows[getStartDate.Rows.Count - 1]["StartDate"]);
                hdnStartDate.Value = startDate;
                hdnEndDate.Value = endDate;

            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private Boolean Check_isLogin_ABAP()
    {
        Boolean Validate_ABAP = false;
        try
        {
            SqlParameter[] sparsError = new SqlParameter[2];
            sparsError[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            sparsError[0].Value = "check_isABAP_Developer";

            sparsError[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            sparsError[1].Value = Convert.ToString(Session["Empcode"]).Trim();
            DataSet DS = spm.getDatasetList(sparsError, "SP_ABAP_Productivity_CompletionSheet");

            if (DS.Tables[0].Rows.Count > 0)
            {
                Validate_ABAP = true;
            }

        }
        catch (Exception ex)
        {
        }
        return Validate_ABAP;
    }

    protected void btn_SumitTimesheet_Click(object sender, EventArgs e)
    {
        lblTimesheetErrorMsg.Text = "";
        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[5];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetABAPersTimsheetDetails";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = (Session["Empcode"]).ToString().Trim();

        spars[2] = new SqlParameter("@TS_ABAPDetailsId", SqlDbType.VarChar);
        spars[2].Value = hdnCommaSeperABAPDetailsId.Value.Trim();

        spars[3] = new SqlParameter("@WeekStartDate", SqlDbType.VarChar);
        spars[3].Value = "2024-12-09"; //hdnStartDate.Value;

        spars[4] = new SqlParameter("@WeekEndDate", SqlDbType.VarChar);
        spars[4].Value = "2024-12-15";//hdnEndDate.Value;

        dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");

        if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
        {
            gvTimesheet.DataSource = null;
            gvTimesheet.DataBind();
            gvTimesheet.Columns.Clear();

            foreach (DataColumn column in dsABAPObjectPlanSubmitted.Tables[0].Columns)
            {
                if (column.DataType == typeof(DateTime))
                {
                    TemplateField templateField = new TemplateField();
                    templateField.HeaderText = column.ColumnName;
                    templateField.ItemTemplate = new CustomTemplate(ListItemType.Item, column.ColumnName);
                    templateField.EditItemTemplate = new CustomTemplate(ListItemType.EditItem, column.ColumnName);
                    gvTimesheet.Columns.Add(templateField);
                }
                else
                {
                    BoundField boundField = new BoundField();
                    boundField.DataField = column.ColumnName;
                    boundField.HeaderText = column.ColumnName;

                    if (column.ColumnName == "SrNo")
                    {
                        boundField.ItemStyle.Width = Unit.Percentage(5);
                        boundField.HeaderStyle.Width = Unit.Percentage(5);
                        gvTimesheet.Columns.Add(boundField);
                    }
                    else if (column.ColumnName == "ABAPDetailsId")
                    {
                        boundField.ItemStyle.Width = Unit.Percentage(2);
                        boundField.HeaderStyle.Width = Unit.Percentage(2);
                        gvTimesheet.Columns.Add(boundField);
                    }
                    else if (column.ColumnName == "Development_Desc")
                    {
                        boundField.ItemStyle.Width = Unit.Percentage(25);
                        boundField.HeaderStyle.Width = Unit.Percentage(25);
                        gvTimesheet.Columns.Add(boundField);
                    }

                    else
                    {
                        //if (column.ColumnName != "SrNo" && column.ColumnName != "ABAPDetailsId" && column.ColumnName != "Development_Desc")
                        //{
                        gvTimesheet.Columns.Add(boundField);
                        //}
                    }
                }
            }

            gvTimesheet.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvTimesheet.DataBind();

        }
        else
        {
            gvTimesheet.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvTimesheet.DataBind();
        }
        DivIDABAPTimesheetPopup.Visible = true;
        this.ModalPopupExtenderABAPTimesheet.Show();

    }

    protected void btnBack_Click1(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvTimesheet.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                foreach (DataControlField column in gvTimesheet.Columns)
                {
                    if (column is TemplateField)
                    {
                        TextBox txtBox = (TextBox)row.FindControl("MainContent_gvTimesheet_txt_09-12-2024_0");
                        if (txtBox != null)
                        {
                            string textValue = txtBox.Text;
                        }
                    }


                    string objSrNo = HttpUtility.HtmlDecode(row.Cells[0].Text);
                    string objABAPDetailsId = HttpUtility.HtmlDecode(row.Cells[1].Text);
                    string objDevDesc = HttpUtility.HtmlDecode(row.Cells[2].Text);
                    string objdayone = HttpUtility.HtmlDecode(row.Cells[3].Text);
                    string objdaytwo = HttpUtility.HtmlDecode(row.Cells[4].Text);
                    string objdaythree = HttpUtility.HtmlDecode(row.Cells[5].Text);
                    string objdayfour = HttpUtility.HtmlDecode(row.Cells[6].Text);
                    string objdayfive = HttpUtility.HtmlDecode(row.Cells[7].Text);
                    string objdaysix = HttpUtility.HtmlDecode(row.Cells[8].Text);
                    string objdayseven = HttpUtility.HtmlDecode(row.Cells[9].Text);

                    bool hasDataForDays = row.Cells.Cast<TableCell>().Skip(3).Take(7).Any(cell => !string.IsNullOrWhiteSpace(cell.Text));

                    if (hasDataForDays)
                    {
                        bool allDaysValid = true;
                        for (int i = 3; i <= 9; i++)
                        {
                            string dayValue = HttpUtility.HtmlDecode(row.Cells[i].Text.Trim());

                            if (!string.IsNullOrWhiteSpace(dayValue))
                            {
                                TimeSpan timeValue;
                                if (!TimeSpan.TryParseExact(dayValue, "hh\\:mm", CultureInfo.InvariantCulture, out timeValue))
                                {
                                    allDaysValid = false;
                                    lblTimesheetErrorMsg.Text = "Invalid time format in Day {i - 2}. Please enter time in the format HH:mm.";
                                    DivIDABAPTimesheetPopup.Visible = true;
                                    this.ModalPopupExtenderABAPTimesheet.Show();
                                    break;
                                }

                                if (timeValue.Hours >= 24 || timeValue.Minutes >= 60)
                                {
                                    allDaysValid = false;
                                    lblTimesheetErrorMsg.Text = "Invalid time in Day { i - 2  }. Please enter a valid time (less than 24 hours and less than 60 minutes).";
                                    DivIDABAPTimesheetPopup.Visible = true;
                                    this.ModalPopupExtenderABAPTimesheet.Show();
                                    break;
                                }

                                if (timeValue.Hours == 0 && timeValue.Minutes == 0)
                                {
                                    allDaysValid = false;
                                    lblTimesheetErrorMsg.Text = "Invalid time in Day {i - 2}. Please enter a valid time (not 0:00).";
                                    DivIDABAPTimesheetPopup.Visible = true;
                                    this.ModalPopupExtenderABAPTimesheet.Show();
                                    break;
                                }
                            }
                        }

                        if (allDaysValid)
                        {
                            // Further processing if necessary
                        }
                    }
                    else
                    {
                        lblTimesheetErrorMsg.Text = "Please provide data for all days.";
                    }


                }
            }


        }
        //var qtype = "INSERTTIMESHEET";
        //spm.InsertTimeSheet(qtype, 0, emp_code, Timesheet_date, projectCode, taskid, hour, status, Description);
        //lblmessage.Text = "You have add timesheet successfully ";

    }


    #endregion

}