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

public partial class ABAP_Object_Tracker_Change_Status_CTMTesting : System.Web.UI.Page
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
                    ctmtesting.Visible = false;
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
        if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
        {
            Txt_DevelopmentDescription.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Development_Desc"]);
            Txt_Module.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ModuleDesc"]);
            Txt_Interface.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Interface"]);
            Txt_FRICECategory.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FCategoryName"]);
            Txt_Order.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Priority"]);
            Txt_Priority.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PriorityName"]);
            Txt_Complexity.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ComplexityName"]);
            if (AllStatus.Items.FindByValue(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMStatus"].ToString()) != null)
            {
                if (Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMStatus"]) == "18")
                {
                    spnreason.Visible = true;
                    Span3.Visible = true;
                    DDLReason.Visible = true;
                }
                else
                {
                    spnreason.Visible = false;
                    Span3.Visible = false;
                    DDLReason.Visible = false;
                }
                if (dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMStatus"].ToString() == "5")
                {
                    ListItem itemToRemove = AllStatus.Items.FindByValue("2");
                    if (itemToRemove != null)
                    {
                        AllStatus.Items.Remove(itemToRemove);
                    }
                }
                if (dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMStatus"].ToString() == "4")
                {
                    ListItem itemToRemove = AllStatus.Items.FindByValue("2");
                    if (itemToRemove != null)
                    {
                        AllStatus.Items.Remove(itemToRemove);
                    }
                }
                AllStatus.SelectedValue = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMStatus"]);
            }
            else if (string.IsNullOrEmpty(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMStatus"])) || Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMStatus"]) == "12")
            {
                AllStatus.Items.Clear();
                AllStatus.Items.Add(new ListItem(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMTestStatusName"]), Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMStatus"])));
                AllStatus.Items.Add(new ListItem("Hold", "4"));
                AllStatus.Items.Add(new ListItem("Started", "5"));
                AllStatus.Items.Add(new ListItem("Send Test Case for Approval", "16"));
                AllStatus.Items.Add(new ListItem("Failed", "18"));

                AllStatus.SelectedValue = Convert.ToString(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"]));
            }
            else
            {
                AllStatus.Items.Clear();
                AllStatus.Items.Add(new ListItem(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMTestStatusName"]), Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMStatus"])));

            }

            if ((AllStatus.SelectedValue == "16" /*|| AllStatus.SelectedValue == "17"*/) && !string.IsNullOrEmpty(AllStatus.SelectedValue))
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

            txt_clientperson.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMName"]);
            txt_clientpersonemail.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMEmail"]);

            hdnRevisedStartDt.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMRevisedFinish"]);
            hdnRevisedFinishDt.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMRevisedFinish"]);
            HDDateCheck3.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMActualStart"]);
            HDDateCheck4.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMActualFinish"]);
            Txt_Remarks.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);
            HDRemarkcheck.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);


            string stropennotopen = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Checkflag"]);
            if (stropennotopen == "open")
            {
                if (HDStatusCheckFlag.Value == "Test Case Approval" && !string.IsNullOrEmpty(HDDateCheck4.Value))
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

        if (dsABAPObjectPlanSubmitted.Tables[1].Rows.Count > 0)
        {
            hdnCTMConsultantEmail.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["CTMConsultantEmail"].ToString().Trim();
            hdnCTMConsultantName.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["CTMConsultantName"].ToString().Trim();
        }
        if (dsABAPObjectPlanSubmitted.Tables[2].Rows.Count > 0)
        {
            hdnHBTConsultantEmail.Value = dsABAPObjectPlanSubmitted.Tables[2].Rows[0]["HBTConsultantEmail"].ToString().Trim();
            hdnHBTConsultantName.Value = dsABAPObjectPlanSubmitted.Tables[2].Rows[0]["HBTConsultantName"].ToString().Trim();
        }
        submitmendatory.Visible = false;
        approvemendatory.Visible = false;

        if (AllStatus.SelectedValue == "2") //not started
        {
            Txt_RevisedActualPreparationStart.Enabled = true;
            txt_RevisedActualPreparationFinish.Enabled = true;
            Txt_RevisedActualSubmission.Enabled = false;
            txt_RevisedActualApproval.Enabled = false;
            if (Convert.ToString(hdnRevisedStartDt.Value).Trim() != "")
            {
                Txt_RevisedActualPreparationStart.Enabled = false;
            }
            if (Convert.ToString(hdnRevisedFinishDt.Value).Trim() != "")
            {
                txt_RevisedActualPreparationFinish.Enabled = false;
            }

        }

        if (AllStatus.SelectedValue == "5") // Started
        {
            submitmendatory.Visible = true;
            Txt_RevisedActualSubmission.Enabled = true;
            txt_RevisedActualApproval.Enabled = false;
            Txt_RevisedActualPreparationStart.Enabled = false;

            txt_RevisedActualPreparationFinish.Enabled = true;

            if (Convert.ToString(hdnRevisedStartDt.Value).Trim() == "")
            {
                Txt_RevisedActualPreparationStart.Text = "";
            }
            if (Convert.ToString(hdnRevisedStartDt.Value).Trim() != "")
            {
                Txt_RevisedActualPreparationStart.Enabled = false;
            }


            if (Convert.ToString(hdnRevisedFinishDt.Value).Trim() == "")
            {
                txt_RevisedActualPreparationFinish.Text = "";
            }

            if (Convert.ToString(hdnRevisedFinishDt.Value).Trim() != "")
            {
                txt_RevisedActualPreparationFinish.Enabled = true;
            }
        }

        if (AllStatus.SelectedValue == "16")  // Send For Approval
        {
            Txt_RevisedActualSubmission.Enabled = false;
            txt_RevisedActualApproval.Enabled = false;
            approvemendatory.Visible = true;
            submitmendatory.Visible = true;
        }
        spnpassedhbt.Visible = false;

        #region Get HBT Test Cases failed Attachment
        var HBTFailedfiles = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTPassedFilesNames"]);
        if (!string.IsNullOrEmpty(HBTFailedfiles))
        {
            spnpassedhbt.Visible = true;
            DataSet objDS = new DataSet();
            SqlParameter[] objSpars = new SqlParameter[3];

            objSpars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            objSpars[0].Value = "GetHBTPassedAttachmentsfromDB";

            objSpars[1] = new SqlParameter("@ID", SqlDbType.VarChar);
            objSpars[1].Value = Convert.ToString(HDID.Value).Trim();

            objDS = spm.getDatasetList(objSpars, "SP_ABAPObjectTracking");
            if (objDS != null)
            {
                gvHBTTestCaseFailedFiles.DataSource = null;
                gvHBTTestCaseFailedFiles.DataBind();
                gvHBTTestCaseFailedFiles.DataSource = objDS.Tables[0];
                gvHBTTestCaseFailedFiles.DataBind();
            }
            else
            {
                gvHBTTestCaseFailedFiles.DataSource = null;
                gvHBTTestCaseFailedFiles.DataBind();
            }
        }
        #endregion


        DivIDPopup.Visible = true;
        PnlIrSheet.Visible = true;
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
            spars[0].Value = "GetRGSFSABAPHBTTestingDetailsByProjectLocation_CTMTest";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = (Session["Empcode"]).ToString().Trim();

            if (HDProjectLocation.Value != "")
            {
                DDLProjectLocation.SelectedValue = HDProjectLocation.Value;
            }

            spars[2] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
            spars[2].Value = DDLProjectLocation.SelectedValue == "0" ? "" : DDLProjectLocation.SelectedValue;

            dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");


            ctmtesting.Visible = false;
            if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
            {
                ctmtesting.Visible = true;
                gvCTMTestDetails.Visible = true;
                gvCTMTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvCTMTestDetails.DataBind();
            }
            else
            {
                lblmessage.Text = "No Record Found.";
                ctmtesting.Visible = false;
                gvCTMTestDetails.DataSource = null;
                gvCTMTestDetails.DataBind();
                gvCTMTestDetails.Visible = false;
            }

            txtPRM.Text = "";
            if (dsABAPObjectPlanSubmitted.Tables[1].Rows.Count > 0)
            {
                liProjectMangaer.Visible = true;
                txtPRM.Text = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["projectManager"].ToString().Trim();
                hdnprojectManager.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["projectManager"].ToString().Trim();
                hdnprojectManagerEmail.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["projectManagerEmail"].ToString().Trim();
                hdnprogramnager.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["programnager"].ToString().Trim();
                hdnprogramnagerEmail.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["programnagerEmail"].ToString().Trim();
            }
        }
        catch (Exception e)
        {

        }
    }

    public void DDLProjectLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
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
            else if (startDate.Date < DateTime.Now.Date)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Revised Start Date must not be less than today's date.";
                return;
            }
            else if (finishDate.Date < DateTime.Now.Date)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Revised END Date must must not be less than today's date.";
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
                else if (startDate.Date > DateTime.Now.Date)
                {
                    DivIDPopup.Visible = true;
                    PnlIrSheet.Visible = true;
                    this.ModalPopupExtenderIRSheet.Show();
                    lblmessagesub.Text = "Actual Start Date Date must not be future date.";
                    return;
                }
                else if (finishDate.Date > DateTime.Now.Date)
                {
                    DivIDPopup.Visible = true;
                    PnlIrSheet.Visible = true;
                    this.ModalPopupExtenderIRSheet.Show();
                    lblmessagesub.Text = "Actual Start Finish Date must not be future date.";
                    return;
                }
            }


            if (!string.IsNullOrWhiteSpace(Txt_RevisedActualSubmission.Text))
            {
                DateTime startDate = DateTime.Parse(ThirdDate);
                if (startDate.Date > DateTime.Now.Date)
                {
                    DivIDPopup.Visible = true;
                    PnlIrSheet.Visible = true;
                    this.ModalPopupExtenderIRSheet.Show();
                    lblmessagesub.Text = "Actual Start Date Date must not be future date.";
                    return;
                }
            }

        }

        if (AllStatus.SelectedValue == "18")
        {
            if (DDLReason.SelectedValue == "0" || DDLReason.SelectedValue == "")
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Please select the reason.";
                return;
            }

        }

        string CTMTestCasefilename = "";
        string UATSignOfffilename = "";
        if (AllStatus.SelectedValue == "16")
        {
            if (txt_clientperson.Text == "")
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Please enter the client person.";
                return;
            }
            if (txt_clientpersonemail.Text == "")
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Please enter the client person email.";
                return;
            }

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

                SqlParameter[] sprm = new SqlParameter[3];
                sprm[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                sprm[0].Value = "Delete_CTMTestCase_Attachment";

                sprm[1] = new SqlParameter("@ID", SqlDbType.Int);
                sprm[1].Value = Convert.ToInt32(HDID.Value);

                sprm[2] = new SqlParameter("@FileType", SqlDbType.VarChar);
                sprm[2].Value = "CTMTestCas";
                DataSet ObjDS1 = spm.getDatasetList(sprm, "SP_ABAPObjectTracking");


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
                lblmessagesub.Text = "When Status is Send Test Case for Approval then only you can upload attchament";
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




        SqlParameter[] spars = new SqlParameter[16];

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

        spars[13] = new SqlParameter("@clientperson", SqlDbType.VarChar);
        spars[13].Value = Convert.ToString(txt_clientperson.Text).Trim();

        spars[14] = new SqlParameter("@clientpersonemail", SqlDbType.VarChar);
        spars[14].Value = Convert.ToString(txt_clientpersonemail.Text).Trim();

        spars[15] = new SqlParameter("@ReasonId", SqlDbType.VarChar);
        spars[15].Value = DDLReason.SelectedValue == "0" ? "" : DDLReason.SelectedValue.Trim();

        DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (DS != null)
        {
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["ResponseStatusVal"].ToString() == "16")
                {
                    #region Send Email 
                    string strSubject = "";
                    strSubject = DDLProjectLocation.SelectedItem.Text.ToString().Trim() + " ABAP Object - " + Txt_DevelopmentDescription.Text.ToString().Trim();
                    string Email_TO = hdnprojectManagerEmail.Value.ToString().Trim();
                    string Email_CC = hdnprogramnagerEmail.Value.ToString().Trim();

                    string strInvoiceURL = "";
                    strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["CTMTestCaseAppr_ABAPObjectPlan"]).Trim());
                    StringBuilder strbuild = new StringBuilder();

                    strbuild.Append("<p style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'></p>");
                    strbuild.Append("<p>" + "Dear " + hdnprojectManager.Value.ToString().Trim() + "</p>");
                    strbuild.Append("<p> This is to inform you that <b>" + hdnCTMConsultantName.Value.ToString().Trim() + "</b> has send Test Cases for approval for the object <b>" + Txt_DevelopmentDescription.Text.ToString().Trim() + "</b>. Please click on below link for action.</p>");
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
                    strbuild.Append("<p><a href=" + strInvoiceURL + ">Please click here to view ABAP Object Plan.</a></p>");
                    strbuild.Append("<br><br>This is an auto generated email, please do not reply!");

                    spm.sendMail(Email_TO, strSubject, Convert.ToString(strbuild).Trim(), "", Email_CC);

                    #endregion
                }

                if (DS.Tables[0].Rows[0]["ResponseStatusVal"].ToString() == "18")
                {
                    #region Send Email to ABAP User 
                    string strSubject = "";
                    strSubject = DDLProjectLocation.SelectedItem.Text.ToString().Trim() + " ABAP Object - " + Txt_DevelopmentDescription.Text.ToString().Trim();
                    string Email_TO = hdnHBTConsultantEmail.Value.ToString().Trim();
                    string Email_CC = hdnprojectManagerEmail.Value.ToString().Trim();

                    string strInvoiceURL = "";
                    strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["HBTTesting_ABAPObjectPlan"]).Trim());
                    StringBuilder strbuild = new StringBuilder();

                    strbuild.Append("<p style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'></p>");
                    strbuild.Append("<p>" + "Dear " + hdnHBTConsultantName.Value.ToString().Trim() + "</p>");
                    strbuild.Append("<p> This is to inform you that <b>" + hdnCTMConsultantName.Value.ToString().Trim() + "</b> has send Test Cases for approval for the object <b>" + Txt_DevelopmentDescription.Text.ToString().Trim() + "</b>. Please click on below link for action.</p>");
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
                    strbuild.Append("<p><a href=" + strInvoiceURL + ">Please click here to view ABAP Object Plan.</a></p>");
                    strbuild.Append("<br><br>This is an auto generated email, please do not reply!");

                   //spm.sendMail(Email_TO, strSubject, Convert.ToString(strbuild).Trim(), "", Email_CC);

                    #endregion
                }


            }
            PnlIrSheet.Visible = false;
            DivIDPopup.Visible = false;
            get_ABAP_Object_Submitted_Plan_FSDetails();

        }
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
        PnlIrSheet.Visible = false;
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
    protected void lnkhbttestcaseDownload_Click(object sender, EventArgs e)
    {
        ImageButton button = sender as ImageButton;
        if (button != null)
        {
            string commandArgument = button.CommandArgument;
            string filePath = Server.MapPath("~/ABAPTracker/PassedHBTTestCase/" + commandArgument);

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
        submitmendatory.Visible = false;
        approvemendatory.Visible = false;
        spnclientperson.Visible = false;
        spnclientpersonemail.Visible = false;

        if (selectedValue == "2") //not started
        {
            if (Convert.ToString(hdnRevisedStartDt.Value).Trim() == "")
            {
                Txt_RevisedActualPreparationStart.Text = "";
            }
            if (Convert.ToString(hdnRevisedFinishDt.Value).Trim() == "")
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

            Txt_RevisedActualPreparationStart.Enabled = true;
            txt_RevisedActualPreparationFinish.Enabled = true;
            Txt_RevisedActualSubmission.Enabled = false;
            txt_RevisedActualApproval.Enabled = false;
            spnreason.Visible = false;
            Span3.Visible = false;
            DDLReason.Visible = false;
        }

        if (selectedValue == "5") // Started
        {
            submitmendatory.Visible = true;
            Txt_RevisedActualSubmission.Enabled = true;
            txt_RevisedActualApproval.Enabled = false;
            Txt_RevisedActualPreparationStart.Enabled = false;

            txt_RevisedActualPreparationFinish.Enabled = false;

            if (Convert.ToString(hdnRevisedStartDt.Value).Trim() == "")
            {
                Txt_RevisedActualPreparationStart.Text = "";
                //Txt_RevisedActualPreparationStart.Enabled = true;  -- commnent on 20-1-2025
            }
            if (Convert.ToString(hdnRevisedFinishDt.Value).Trim() == "")
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

            if (Convert.ToString(hdnRevisedStartDt.Value).Trim() != "")
            {
                Txt_RevisedActualPreparationStart.Enabled = false;
            }

            if (Convert.ToString(hdnRevisedFinishDt.Value).Trim() != "")
            {
                txt_RevisedActualPreparationFinish.Enabled = true;
            }

            spnreason.Visible = false;
            Span3.Visible = false;
            DDLReason.Visible = false;
        }
        
        if (selectedValue == "16") // send for approval
        {
            if (Convert.ToString(hdnRevisedStartDt.Value).Trim() == "")
            {
                Txt_RevisedActualPreparationStart.Text = "";
            }
            if (Convert.ToString(hdnRevisedFinishDt.Value).Trim() == "")
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

            spnclientperson.Visible = true;
            spnclientpersonemail.Visible = true;

            Txt_RevisedActualPreparationStart.Enabled = false;
            txt_RevisedActualPreparationFinish.Enabled = false;

            Txt_RevisedActualSubmission.Enabled = true;
            if (Convert.ToString(Txt_RevisedActualSubmission.Text).Trim() != "")
            {
                Txt_RevisedActualSubmission.Enabled = false;

            }
            if (Convert.ToString(Txt_RevisedActualSubmission.Text).Trim() == "")
            {
                Txt_RevisedActualSubmission.Enabled = true;
                submitmendatory.Visible = true;
            }




            txt_RevisedActualApproval.Enabled = true;

            approvemendatory.Visible = true;
            liUploadCTMTestCase.Visible = true;
            liUploadUATSingOff.Visible = true;
            liUploadCTMTestCase_3.Visible = true;

            spnreason.Visible = false;
            Span3.Visible = false;
            DDLReason.Visible = false;
        }
        else
        {
            liUploadCTMTestCase.Visible = false;
            liUploadUATSingOff.Visible = false;
            liUploadCTMTestCase_3.Visible = false;
        }


        if (selectedValue == "18")  // Test Case Correction
        {
            if (Convert.ToString(hdnRevisedStartDt.Value).Trim() == "")
            {
                Txt_RevisedActualPreparationStart.Text = "";
            }
            if (Convert.ToString(hdnRevisedFinishDt.Value).Trim() == "")
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
            txt_RevisedActualApproval.Enabled = true;
            approvemendatory.Visible = true;

            if (Convert.ToString(Txt_RevisedActualSubmission.Text).Trim() != "")
            {
                Txt_RevisedActualSubmission.Enabled = false;
            }
            if (Convert.ToString(Txt_RevisedActualSubmission.Text).Trim() == "")
            {
                Txt_RevisedActualSubmission.Enabled = true;
                submitmendatory.Visible = true;
            }

            spnreason.Visible = false;
            Span3.Visible = false;
            DDLReason.Visible = false;

            GetReason();
        }

        if (selectedValue == "4")
        {
            if (Convert.ToString(hdnRevisedStartDt.Value).Trim() == "")
            {
                Txt_RevisedActualPreparationStart.Text = "";
            }
            if (Convert.ToString(hdnRevisedFinishDt.Value).Trim() == "")
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

            LinkBtnSavePopup.Visible = true;
            Txt_RevisedActualPreparationStart.Enabled = false;
            txt_RevisedActualPreparationFinish.Enabled = false;
            Txt_RevisedActualSubmission.Enabled = false;
            txt_RevisedActualApproval.Enabled = false;
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

    private void GetReason()
    {
        var stageval = Convert.ToString(ddlStage.SelectedValue).Trim();
        var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetReasonForCTM";

        DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
        {
            DDLReason.DataSource = DS.Tables[0];
            DDLReason.DataTextField = "ReasonName";
            DDLReason.DataValueField = "Reason_Id";
            DDLReason.DataBind();
            DDLReason.Items.Insert(0, new ListItem("Select the reason", "0"));

            spnreason.Visible = true;
            Span3.Visible = true;
            DDLReason.Visible = true;
        }
        else
        {
            DDLReason.Items.Insert(0, new ListItem("Select the reason", "0"));
            lbl_error.Text = "Change status of ABAP Object is restricted to ABAP Users only.";
            spnreason.Visible = false;
            Span3.Visible = false;
            DDLReason.Visible = false;
        }


        DivIDPopup.Visible = true;
        PnlIrSheet.Visible = true;
        this.ModalPopupExtenderIRSheet.Show();
    }



    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        gvCTMTestDetails.Columns[0].Visible = false;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=CTMTestDetailPlanExport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                // Hide buttons or controls not to export
                gvCTMTestDetails.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        gvCTMTestDetails.Columns[0].Visible = true;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms the GridView was rendered
    }
    #endregion

}