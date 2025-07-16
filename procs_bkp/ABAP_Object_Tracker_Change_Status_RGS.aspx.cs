using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ABAP_Object_Tracker_Change_Status_RGS : System.Web.UI.Page
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
                    PnlIrSheet.Visible = false;
                    getProjectLocation();
                    //get_ABAP_Object_Submitted_Plan_FSDetails();
                    //     GetWeekStartDateEndDate();

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

                    gvRGSDetails.Visible = false;
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
        HDStatus.Value = "RGS";
        getStatusRGS("RGS");
        get_RGS_Detail();

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
        if (AllStatus.Items.FindByValue(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RGSStatus"].ToString()) != null)
        {
            AllStatus.SelectedValue = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RGSStatus"]);
            if (AllStatus.SelectedValue == "8" && !string.IsNullOrEmpty(AllStatus.SelectedValue))
            {
                AllStatus.Enabled = false;
            }
            else
            {
                AllStatus.Enabled = true;
            }
            if (AllStatus.SelectedValue == "5")
            {
                ListItem itemToRemove = AllStatus.Items.FindByValue("2");
                if (itemToRemove != null)
                {
                    AllStatus.Items.Remove(itemToRemove);
                }
            }

        }
        else
        {
            AllStatus.Items.Add(new ListItem(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RGSStatuss"]), Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RGSStatus"])));
            AllStatus.SelectedValue = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RGSStatus"]);

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
        if (!string.IsNullOrEmpty(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RevActualSubmit"].ToString()))
        {
            Txt_RevisedActualPreparationStart.Enabled = false;
        }
        else
        {
            Txt_RevisedActualPreparationStart.Enabled = true;
        }
        if (!string.IsNullOrEmpty(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RevActualApprove"].ToString()))
        {
            txt_RevisedActualPreparationFinish.Enabled = false;
        }
        else
        {
            txt_RevisedActualPreparationFinish.Enabled = true;
        }



        HDDateCheck1.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RevActualPrepStart"]);
        HDDateCheck2.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RevActualPrepFinish"]);
        HDDateCheck3.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RevActualSubmit"]);
        HDDateCheck4.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RevActualApprove"]);

        Txt_Remarks.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);
        HDRemarkcheck.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);
        Txt_RevisedActualSubmission.Enabled = true;
        LinkBtnSavePopup.Visible = true;

        //if (HDStatusCheckFlag.Value == "Submitted")
        //{
        //    Txt_RevisedActualSubmission.Enabled = false;
        //}
        rgsupload_1.Visible = false;
        rgsupload_2.Visible = false;
        rgsupload_3.Visible = false;

        if ((HDStatusCheckFlag.Value == "Submitted" || HDStatusCheckFlag.Value == "Approved") && !string.IsNullOrEmpty(Txt_RevisedActualSubmission.Text))
        {
            LinkBtnSavePopup.Visible = false;
            AllStatus.Enabled = false;
        }

        if (AllStatus.SelectedValue == "2") //Not Started
        {
            Txt_RevisedActualSubmission.Enabled = false;
            txt_RevisedActualApproval.Enabled = false;
            Txt_RevisedActualPreparationStart.Enabled = true;
            txt_RevisedActualPreparationFinish.Enabled = true;
            submitmendatory.Visible = false;
            approvemendatory.Visible = false;
        }

        if (AllStatus.SelectedValue == "8") //Send for Approval
        {
            txt_RevisedActualApproval.Enabled = true;
            submitmendatory.Visible = true;
            approvemendatory.Visible = true;

            if (Convert.ToString(HDDateCheck1.Value).Trim() == "")
            {
                Txt_RevisedActualPreparationStart.Text = "";
            }

            if (Convert.ToString(HDDateCheck2.Value).Trim() == "")
            {
                txt_RevisedActualPreparationFinish.Text = "";
            }
            if (Convert.ToString(HDDateCheck3.Value).Trim() == "")
            {
                Txt_RevisedActualSubmission.Text = "";
            }
            if (Convert.ToString(HDDateCheck4.Value).Trim() == "")
            {
                txt_RevisedActualApproval.Text = "";
            }

            Txt_RevisedActualPreparationStart.Enabled = false;
            txt_RevisedActualPreparationFinish.Enabled = false;

            if (Txt_RevisedActualSubmission.Text == "")
            {
                Txt_RevisedActualSubmission.Enabled = true;
            }
            else
            {
                Txt_RevisedActualSubmission.Enabled = false;
            }
            if (txt_RevisedActualApproval.Text == "")
            {
                txt_RevisedActualApproval.Enabled = true;
            }
            else
            {
                txt_RevisedActualApproval.Enabled = false;
            }
        }


        if (AllStatus.SelectedValue == "5") //Started
        {
            if (Convert.ToString(HDDateCheck1.Value).Trim() == "")
            {
                Txt_RevisedActualPreparationStart.Text = "";
            }

            if (Convert.ToString(HDDateCheck2.Value).Trim() == "")
            {
                txt_RevisedActualPreparationFinish.Text = "";
            }
            if (Convert.ToString(HDDateCheck3.Value).Trim() == "")
            {
                Txt_RevisedActualSubmission.Text = "";
            }
            if (Convert.ToString(HDDateCheck4.Value).Trim() == "")
            {
                txt_RevisedActualApproval.Text = "";
            }

            if (Txt_RevisedActualPreparationStart.Text != "")
            {
                txt_RevisedActualPreparationFinish.Enabled = true;
            }
            else
            {
                txt_RevisedActualPreparationFinish.Enabled = false;
            }

            if (Txt_RevisedActualSubmission.Text == "")
            {
                Txt_RevisedActualSubmission.Enabled = true;
            }
            else
            {
                Txt_RevisedActualSubmission.Enabled = false;
            }

            Txt_RevisedActualPreparationStart.Enabled = false;
            txt_RevisedActualApproval.Enabled = false;
            submitmendatory.Visible = true;
            approvemendatory.Visible = false;
        }

        if (AllStatus.SelectedValue == "4")
        {
            Txt_RevisedActualPreparationStart.Enabled = false;
            txt_RevisedActualPreparationFinish.Enabled = false;
            Txt_RevisedActualSubmission.Enabled = false;
            txt_RevisedActualApproval.Enabled = false;

            submitmendatory.Visible = false;
            approvemendatory.Visible = false;
        }


        #region Get RGS Attachment
        var RGSFilesNames = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RGSFilesNames"]);
        if (!string.IsNullOrEmpty(RGSFilesNames))
        {
            rgs.Visible = true;
            DataSet objDS = new DataSet();
            SqlParameter[] objSpars = new SqlParameter[3];

            objSpars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            objSpars[0].Value = "GetRGSAttachmentfromDB";

            objSpars[1] = new SqlParameter("@ID", SqlDbType.VarChar);
            objSpars[1].Value = Convert.ToString(HDID.Value).Trim();

            objDS = spm.getDatasetList(objSpars, "SP_ABAPObjectTracking");
            if (objDS != null)
            {
                gvuploadedFiles.DataSource = null;
                gvuploadedFiles.DataBind();
                gvuploadedFiles.DataSource = objDS.Tables[0];
                gvuploadedFiles.DataBind();
            }
            else
            {
                gvuploadedFiles.DataSource = null;
                gvuploadedFiles.DataBind();
            }
        }
        else
        {
            gvuploadedFiles.DataSource = null;
            gvuploadedFiles.DataBind();
        }
        #endregion




        PnlIrSheet.Visible = true;
        DivIDPopup.Visible = true;
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

                AllStatus.DataTextField = "RGS_DisplayName";
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
            spars[0].Value = "GetRGSFSABAPHBTTestingDetailsByProjectLocation_RGS";
            //spars[0].Value = "GetRGSFSABAPHBTTestingDetailsByProjectLocation";

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
            rgs.Visible = true;
            gvRGSDetails.Visible = true;

            if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
            {
                gvRGSDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvRGSDetails.DataBind();
                hdnRGSConsultantName.Value = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RGSEmpName"].ToString().Trim();
                hdnRGSConsultantMail.Value = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RGSEmpEmail"].ToString().Trim();
            }
            else
            {
                rgs.Visible = false;
                gvRGSDetails.DataSource = null;
                gvRGSDetails.DataBind();
                gvRGSDetails.Visible = false;
                lblmessage.Text = "No record found.";
            }
            
            txtPRM.Text = "";
            if (dsABAPObjectPlanSubmitted.Tables[1].Rows.Count > 0)
            {
                liProjectMangaer.Visible = true;
                txtPRM.Text = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["projectManager"].ToString().Trim();
                hdnProjectManagerName.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["projectManager"].ToString().Trim();
                hdnProjectManagerMail.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["projectManagerMail"].ToString().Trim();

                hdnProgramManagerName.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["programnager"].ToString().Trim();
                hdnProgramManagerMail.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["programnagerMail"].ToString().Trim();
            }
        }
        catch (Exception e)
        {

        }
    }

    public void DDLProjectLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvRGSDetails.Visible = false;
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
            lblmessagesub.Text = "Select the Revised Actual Preparation Start Date.";
            return;

        }
        if (txt_RevisedActualPreparationFinish.Text == "" && Txt_RevisedActualPreparationStart.Text != "")
        {
            DivIDPopup.Visible = true;
            PnlIrSheet.Visible = true;
            this.ModalPopupExtenderIRSheet.Show();
            lblmessagesub.Text = "Select the Revised Actual Preparation Finish Date.";
        }

        if (Txt_RevisedActualSubmission.Text == "" && txt_RevisedActualApproval.Text != "")
        {
            DivIDPopup.Visible = true;
            PnlIrSheet.Visible = true;
            this.ModalPopupExtenderIRSheet.Show();
            lblmessagesub.Text = "Select the Actual Start Date.";
            return;
        }

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
            if (startDate <= PlannedPreparationStartdt)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Revised/Actual Preparation Start Date must be greater than or equal to Planned Preparation Start Date";
                return;
            }
            else if (finishDate < PlannedPreparationFinishdt)
            {
                //DivIDPopup.Visible = true;
                //PnlIrSheet.Visible = true;
                //this.ModalPopupExtenderIRSheet.Show();
                //lblmessagesub.Text = "Revised/ Actual Preparation Finish Date must be greater than or equal to Planned Preparation Finish Date.";
                //return;
            }
            else if (startDate > finishDate)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Revised Start Date must be earlier than Revised Finish Date.";
                return;
            }
            else if (startDate.Date < DateTime.Now.Date)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Revised/Actual Preparation Start Date must not be previous date.";
                return;

            }
            else if (finishDate.Date < DateTime.Now.Date)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Revised/Actual Preparation END Date must not be previous date.";
                return;
            }
        }

        if (!string.IsNullOrWhiteSpace(Txt_RevisedActualSubmission.Text))
        {
            DateTime startDate = DateTime.Parse(ThirdDate);
            if (startDate < PlannedSubmissiondt)
            {
                //DivIDPopup.Visible = true;
                //PnlIrSheet.Visible = true;
                //this.ModalPopupExtenderIRSheet.Show();
                //lblmessagesub.Text = "Revised/ Actual Submission Date must be greater than or equal to Planned Submission Date.";
                //return;
            }
            if (startDate.Date > DateTime.Now.Date)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Revised/ Actual Submission Date must not be future date.";
                return;
            }
        }

        if (!string.IsNullOrWhiteSpace(txt_RevisedActualApproval.Text))
        {
            DateTime finishDate = DateTime.Parse(FourthDate);
            if (finishDate < PlannedApprovaldt)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Revised/ Actual Approval Date must be greater than or equal to Planned Approval Date.";
                return;
            }
            if (finishDate.Date > DateTime.Now.Date)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Revised/ Actual Approval Date must not be future date.";
                return;
            }
        }

        if (!string.IsNullOrWhiteSpace(Txt_RevisedActualPreparationStart.Text))
        {
            DateTime startDate = DateTime.Parse(FirstDate);
            if (startDate.Date < DateTime.Now.Date)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Revised/Actual Preparation Start Date must not be previous date.";
                return;
            }
        }

        if (txt_RevisedActualPreparationFinish.Text != "")
        {
            DateTime endDate = DateTime.Parse(SecondDate);
            if (endDate.Date < DateTime.Parse(FirstDate))
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Revised/Actual Preparation Finish Date must greater than Revised/Actual Preparation Start Date.";
                return;
            }
        }

            if (!string.IsNullOrWhiteSpace(Txt_RevisedActualSubmission.Text))
        {
            //DateTime startDate = DateTime.Parse(ThirdDate);
            //if (startDate < PlannedSubmissiondt)
            //{
            //    DivIDPopup.Visible = true;
            //    PnlIrSheet.Visible = true;
            //    this.ModalPopupExtenderIRSheet.Show();
            //    lblmessagesub.Text = "Revised/ Actual Submission Date must be greater than or equal to Planned Submission Date.";
            //    return;
            //}
        }

        if (!string.IsNullOrWhiteSpace(txt_RevisedActualApproval.Text))
        {
            DateTime finishDate = DateTime.Parse(FourthDate);
            if (finishDate < PlannedApprovaldt)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Revised/ Actual Approval Date must be greater than or equal to Planned Approval Date.";
                return;
            }
        }

        if (!string.IsNullOrWhiteSpace(Txt_RevisedActualSubmission.Text) && !string.IsNullOrWhiteSpace(txt_RevisedActualApproval.Text))
        {
            DateTime startDate = DateTime.Parse(ThirdDate);
            DateTime finishDate = DateTime.Parse(FourthDate);
            if (startDate < PlannedPreparationStartdt)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show(); lblmessagesub.Text = "Actual Start Date must be greater than or equal to Planned Start Date.";
                return;
            }
            else if (finishDate < PlannedPreparationStartdt)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show(); lblmessagesub.Text = "Actual Finish Date must be greater than or equal to Planned Finish Date.";
                return;
            }
            else if (startDate > finishDate)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Actual Start Date must be earlier than or equal to Actual Finish Date";
                return;
            }

        }

        if (!string.IsNullOrWhiteSpace(Txt_RevisedActualSubmission.Text))
        {
            DateTime startDate = DateTime.Parse(ThirdDate);
            if (startDate < PlannedPreparationStartdt)
            {
                //DivIDPopup.Visible = true;
                //PnlIrSheet.Visible = true;
                //this.ModalPopupExtenderIRSheet.Show(); lblmessagesub.Text = "Actual Start Date must be greater than or equal to Planned Start Date.";
                //return;
            }
        }





        if (AllStatus.SelectedValue == "5")
        {
            if (Txt_RevisedActualSubmission.Text == "")
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "The status cannot be set to Started unless the Actual Submission Date has been entered.";
                return;
            }
        }

        if (AllStatus.SelectedValue == "8")
        {
            if (Txt_RevisedActualSubmission.Text == "" || txt_RevisedActualApproval.Text == "")
            {
                if (Txt_RevisedActualSubmission.Text == "")
                {
                    DivIDPopup.Visible = true;
                    PnlIrSheet.Visible = true;
                    this.ModalPopupExtenderIRSheet.Show();
                    lblmessagesub.Text = "You cannot set the status to Send for Approval if the Actual Submission Date has not been entered.";
                    return;
                }

                //if (txt_RevisedActualApproval.Text == "")
                //{
                //    DivIDPopup.Visible = true;
                //    PnlIrSheet.Visible = true;
                //    this.ModalPopupExtenderIRSheet.Show();
                //    lblmessagesub.Text = "You cannot set the status to Completed if the Actual Approval Date has not been entered.";
                //    return;
                //}
            }

            if (!furgsupload.HasFile)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Please Upload the attachement.";
                return;
            }
        }


       


        SqlParameter[] spars = new SqlParameter[12];
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

        spars[11] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
        spars[11].Value = Convert.ToString(DDLProjectLocation.SelectedValue);


        DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (DS != null)
        {
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["ResponseStatusVal"].ToString() == "8")
                {
                    string filename = "";
                    if (!furgsupload.HasFile)
                    {
                        DivIDPopup.Visible = true;
                        PnlIrSheet.Visible = true;
                        this.ModalPopupExtenderIRSheet.Show();
                        lblmessagesub.Text = "Please Upload the attachement.";
                        return;
                    }
                    if (furgsupload.HasFile && AllStatus.SelectedValue == "8")
                    {
                        string FSAttachmentFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ABAPObjectRGS"]).Trim() + "/");
                        bool folderExists = Directory.Exists(FSAttachmentFilePath);
                        if (!folderExists)
                        {
                            Directory.CreateDirectory(FSAttachmentFilePath);
                        }
                        int srno = 1;
                        foreach (HttpPostedFile uploadedFile in furgsupload.PostedFiles)
                        {
                            filename = uploadedFile.FileName;

                            string fileExtension = System.IO.Path.GetExtension(filename);
                            string timestamp = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
                            filename = "RGS_Attachment_" + timestamp + fileExtension;

                            uploadedFile.SaveAs(Path.Combine(FSAttachmentFilePath, filename));

                            string uploadedFilePath = FSAttachmentFilePath + filename;

                            string fullPath = System.IO.Path.GetFullPath(uploadedFilePath);

                            SqlParameter[] sparam = new SqlParameter[5];
                            sparam[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                            sparam[0].Value = "Upload_RGS_Attachment";

                            sparam[1] = new SqlParameter("@ID", SqlDbType.Int);
                            sparam[1].Value = Convert.ToInt32(HDID.Value);

                            sparam[2] = new SqlParameter("@SrNo", SqlDbType.Int);
                            sparam[2].Value = Convert.ToInt32(srno);

                            sparam[3] = new SqlParameter("@FileType", SqlDbType.VarChar);
                            sparam[3].Value = "RGS_Attachment";

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


                    #region Send Email 
                    string strSubject = "";
                    strSubject = DDLProjectLocation.SelectedItem.Text.ToString() + " ABAP Object Development Plan Submitted for Approval";

                    string sApproverEmail_CC = "";
                    sApproverEmail_CC = hdnProgramManagerMail.Value.ToString().Trim();
                    string strInvoiceURL = "";
                    strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["RGSCaseAppr_ABAPObjectPlan"]).Trim()).Trim();
                    StringBuilder strbuild = new StringBuilder();

                    strbuild.Append("<p style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'></p>");
                    strbuild.Append("<p>" + "Dear " + hdnProjectManagerName.Value.ToString().Trim() + "</p>");
                    strbuild.Append("<p> This is to inform you that <b>" + hdnRGSConsultantName.Value.ToString().Trim() + "</b> has send RGS for approval for the object <b>" + Txt_DevelopmentDescription.Text.ToString().Trim() + "</b>. Please click on below link for action.</p>");
                    strbuild.Append("<table cellpadding='5' cellspacing='0' style='font-size:11pt;font-family:Arial;font-style:Regular;width:100%;'>");
                    strbuild.Append("<tr><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Object Name</th> <th style='background-color: #C7D3D4;border: 1px solid #ccc'>Module</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Interface</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Plan Start Date</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Plan End Date</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Revised Start Date</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Revised End date</th>" +
                                         "</tr>");
                    strbuild.Append("<tr><td style='width:38%;border: 1px solid #ccc'>" + Convert.ToString(Txt_DevelopmentDescription.Text).Trim() + " </td>");
                    strbuild.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(Txt_Module.Text).Trim() + "</td>");
                    strbuild.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(Txt_Interface.Text).Trim() + "</td>");
                    strbuild.Append("<td style='width:8%;border: 1px solid #ccc'>" + Convert.ToString(Txt_PlannedPreparationStart.Text).Trim() + "</td>");
                    strbuild.Append("<td style='width:8%;border: 1px solid #ccc'>" + Convert.ToString(txt_PlannedPreparationFinish.Text).Trim() + "</td>");
                    strbuild.Append("<td style='width:8%;border: 1px solid #ccc'>" + Convert.ToString(Txt_RevisedActualPreparationStart.Text).Trim() + "</td>");
                    strbuild.Append("<td style='width:8%;border: 1px solid #ccc'>" + Convert.ToString(txt_RevisedActualPreparationFinish.Text).Trim() + "</td></tr>");
                    strbuild.Append("</table>");
                    strbuild.Append("<br/><p><a href=" + strInvoiceURL + ">Please click here to view ABAP Object Plan.</a></p>");
                    strbuild.Append("<br><br>This is an auto generated email, please do not reply!");
                    spm.sendMail(hdnProjectManagerMail.Value, strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);
                    #endregion

                }
            }
        }

        DivIDPopup.Visible = false;
        get_ABAP_Object_Submitted_Plan_FSDetails();

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
        PnlIrSheet.Visible = false;
    }

    protected void ibdownloadbtn_Click(object sender, EventArgs e)
    {
        ImageButton button = sender as ImageButton;
        if (button != null)
        {
            string commandArgument = button.CommandArgument;
            string filePath = Server.MapPath("~/ABAPTracker/RGSAttachment/" + commandArgument);

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
        rgsupload_1.Visible = false;
        rgsupload_2.Visible = false;
        rgsupload_3.Visible = false;

        if (AllStatus.SelectedValue == "2") // Not Started
        {
            if (Convert.ToString(HDDateCheck1.Value).Trim() == "")
            {
                Txt_RevisedActualPreparationStart.Text = "";
            }

            if (Convert.ToString(HDDateCheck2.Value).Trim() == "")
            {
                txt_RevisedActualPreparationFinish.Text = "";
            }
            if (Convert.ToString(HDDateCheck3.Value).Trim() == "")
            {
                Txt_RevisedActualSubmission.Text = "";
            }
            if (Convert.ToString(HDDateCheck4.Value).Trim() == "")
            {
                txt_RevisedActualApproval.Text = "";
            }
            Txt_RevisedActualSubmission.Enabled = false;
            txt_RevisedActualApproval.Enabled = false;
            Txt_RevisedActualPreparationStart.Enabled = true;
            txt_RevisedActualPreparationFinish.Enabled = true;
            submitmendatory.Visible = false;
            approvemendatory.Visible = false;
        }

        //if (selectedValue == "8") //Submitted
        //{
        //    if (Convert.ToString(HDDateCheck1.Value).Trim() == "")
        //    {
        //        Txt_RevisedActualPreparationStart.Text = "";
        //    }

        //    if (Convert.ToString(HDDateCheck2.Value).Trim() == "")
        //    {
        //        txt_RevisedActualPreparationFinish.Text = "";
        //    }
        //    if (Convert.ToString(HDDateCheck3.Value).Trim() == "")
        //    {
        //        Txt_RevisedActualSubmission.Text = "";
        //    }
        //    if (Convert.ToString(HDDateCheck4.Value).Trim() == "")
        //    {
        //        txt_RevisedActualApproval.Text = "";
        //    }

        //    Txt_RevisedActualPreparationStart.Enabled = false;
        //    txt_RevisedActualPreparationFinish.Enabled = false;

        //    if (Txt_RevisedActualSubmission.Text == "")
        //    {
        //        Txt_RevisedActualSubmission.Enabled = true;
        //    }
        //    else
        //    {
        //        Txt_RevisedActualSubmission.Enabled = false;
        //    }

        //    txt_RevisedActualApproval.Enabled = true;
        //    submitmendatory.Visible = true;
        //    approvemendatory.Visible = true;
        //}

        if (selectedValue == "8") //Send for Approval
        {
            if (Convert.ToString(HDDateCheck1.Value).Trim() == "")
            {
                Txt_RevisedActualPreparationStart.Text = "";
            }

            if (Convert.ToString(HDDateCheck2.Value).Trim() == "")
            {
                txt_RevisedActualPreparationFinish.Text = "";
            }
            if (Convert.ToString(HDDateCheck3.Value).Trim() == "")
            {
                Txt_RevisedActualSubmission.Text = "";
            }
            if (Convert.ToString(HDDateCheck4.Value).Trim() == "")
            {
                txt_RevisedActualApproval.Text = "";
            }

            Txt_RevisedActualPreparationStart.Enabled = false;
            txt_RevisedActualPreparationFinish.Enabled = false;

            if (Txt_RevisedActualSubmission.Text == "")
            {
                Txt_RevisedActualSubmission.Enabled = true;
            }
            else
            {
                Txt_RevisedActualSubmission.Enabled = false;
            }

            rgsupload_1.Visible = true;
            rgsupload_2.Visible = true;
            rgsupload_3.Visible = true;

            txt_RevisedActualApproval.Enabled = false;
            submitmendatory.Visible = true;
            approvemendatory.Visible = false;
        }


        if (selectedValue == "5") //Started
        {
            if (Convert.ToString(HDDateCheck1.Value).Trim() == "")
            {
                Txt_RevisedActualPreparationStart.Text = "";
            }

            if (Convert.ToString(HDDateCheck2.Value).Trim() == "")
            {
                txt_RevisedActualPreparationFinish.Text = "";
            }
            if (Convert.ToString(HDDateCheck3.Value).Trim() == "")
            {
                Txt_RevisedActualSubmission.Text = "";
            }
            if (Convert.ToString(HDDateCheck4.Value).Trim() == "")
            {
                txt_RevisedActualApproval.Text = "";
            }


            if (Txt_RevisedActualPreparationStart.Text != "")
            {
                txt_RevisedActualPreparationFinish.Enabled = true;
            }
            else
            {
                txt_RevisedActualPreparationFinish.Enabled = false;

            }
            Txt_RevisedActualSubmission.Enabled = true;


            Txt_RevisedActualPreparationStart.Enabled = false;
            txt_RevisedActualApproval.Enabled = false;
            submitmendatory.Visible = true;
            approvemendatory.Visible = false;
        }

        if (selectedValue == "4")
        {
            Txt_RevisedActualPreparationStart.Enabled = false;
            txt_RevisedActualPreparationFinish.Enabled = false;
            Txt_RevisedActualSubmission.Enabled = false;
            txt_RevisedActualApproval.Enabled = false;

            submitmendatory.Visible = false;
            approvemendatory.Visible = false;

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
    
    #endregion

}