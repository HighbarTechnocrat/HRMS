using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ABAP_Object_Tracker_Change_Status_ABAP_SourceCode : System.Web.UI.Page
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
                    //PnlABAPTimeSheet.Visible = false;
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
                    DDLProjectLocation.Visible = true;
                    spnlabel.Visible = true;
                    ABAP.Visible = false;
                    //btn_SumitTimesheet.Visible = false;

                    gvABAPDevDetails.Visible = false;
                    liProjectMangaer.Visible = true;
                    HDProjectLocation.Value = DS.Tables[0].Rows[0]["comp_code"].ToString();
                    get_ABAP_Object_Submitted_Plan_FSDetails();
                }
            }
            else
            {
                DDLProjectLocation.Visible = false;
                spnlabel.Visible = false;
                DDLProjectLocation.Items.Insert(0, new ListItem("Select Project Location", "0"));
                lbl_error.Text = "Change status of ABAP Object is restricted to ABAP Users only.";
            }
        }
        catch (Exception ex)
        {
            lbl_error.Text = ex.Message.ToString();
            return;
        }
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

        this.ModalPopupExtenderIRSheet.Show();
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
                    if (item.Value == "2" || item.Value == "10" || item.Value == "5")
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
                if (Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPDevStatus"]) == "18")
                {
                    lireason1.Visible = true;
                    lireason2.Visible = true;
                    lireason3.Visible = true;
                    txt_Reason.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ReasonName"]);
                    hdnReason.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ReasonName"]);
                }
                else
                {
                    lireason1.Visible = false;
                    lireason2.Visible = false;
                    lireason3.Visible = false;
                    txt_Reason.Text = "";
                }

                AllStatus.Items.Clear();
                AllStatus.Items.Add(new ListItem(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPDevStatuss"]), Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPDevStatus"])));
                AllStatus.Items.Add(new ListItem("Hold", "4"));
                AllStatus.Items.Add(new ListItem("Submit For Functional Testing", "10"));

                AllStatus.SelectedValue = Convert.ToString(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPDevStatus"]));
            }
            else if (string.IsNullOrEmpty(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPDevStatus"])) || Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPDevStatus"]) == "8")
            {
                AllStatus.Items.Clear();
                AllStatus.Items.Add(new ListItem(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPDevStatuss"]), Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPDevStatus"])));
                AllStatus.Items.Add(new ListItem("Hold", "4"));
                AllStatus.Items.Add(new ListItem("Submit For Functional Testing", "10"));

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

                if (dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPDevStatus"].ToString() == "5")
                {
                    ListItem itemToRemove = AllStatus.Items.FindByValue("2");
                    if (itemToRemove != null)
                    {
                        AllStatus.Items.Remove(itemToRemove);
                    }
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

            if (Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPDevStatus"]) == "2")
            {
                Txt_RevisedActualSubmission.Enabled = false;
                txt_RevisedActualApproval.Enabled = false;
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

            hdnABAPRevisedStartDt.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPRevisedStart"]);
            hdnABAPRevisedFinishDt.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPRevisedFinish"]);
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

            spnfailedhbt.Visible = false;
            spnfsdocs.Visible = false;

            #region Get Functional Specification Attachment
            var files = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FileNames"]);
            if (!string.IsNullOrEmpty(files))
            {
                spnfsdocs.Visible = true;
                DataSet objDS = new DataSet();
                SqlParameter[] objSpars = new SqlParameter[3];

                objSpars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                objSpars[0].Value = "GetFSAttachmentsfromDB";

                objSpars[1] = new SqlParameter("@ID", SqlDbType.VarChar);
                objSpars[1].Value = Convert.ToString(hdnABAPDetailsId.Value).Trim();

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
            #endregion

            #region Get HBT Test Cases failed Attachment
            var HBTFailedfiles = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTFailedFilesNames"]);
            if (!string.IsNullOrEmpty(HBTFailedfiles))
            {
                spnfailedhbt.Visible = true;
                DataSet objDS = new DataSet();
                SqlParameter[] objSpars = new SqlParameter[3];

                objSpars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                objSpars[0].Value = "GetHBTFailedABAPAttachmentsfromDB";

                objSpars[1] = new SqlParameter("@ID", SqlDbType.VarChar);
                objSpars[1].Value = Convert.ToString(hdnABAPDetailsId.Value).Trim();

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
        }

        if (dsABAPObjectPlanSubmitted.Tables[1].Rows.Count > 0)
        {
            hdnABAPperEmail.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ABAPperEmail"].ToString().Trim();
            hdnABAPperName.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ABAPperName"].ToString().Trim();
        }
        if (dsABAPObjectPlanSubmitted.Tables[2].Rows.Count > 0)
        {
            hdnFSConsultantEmail.Value = dsABAPObjectPlanSubmitted.Tables[2].Rows[0]["FSConsultantEmail"].ToString().Trim();
            hdnFSConsultantName.Value = dsABAPObjectPlanSubmitted.Tables[2].Rows[0]["FSConsultantName"].ToString().Trim();
        }
        if (dsABAPObjectPlanSubmitted.Tables[3].Rows.Count > 0)
        {
            hdnHBTConsultantEmail.Value = dsABAPObjectPlanSubmitted.Tables[3].Rows[0]["HBTConsultantEmail"].ToString().Trim();
            hdnHBTConsultantName.Value = dsABAPObjectPlanSubmitted.Tables[3].Rows[0]["HBTConsultantName"].ToString().Trim();
        }
        hdnctmstatus.Value = "";
        if (Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMStatus"]) == "17")
        {
            if (string.IsNullOrEmpty(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["SourceCodeFile"])))
            {
                Txt_Remarks.Enabled = false;
                li_sc_upload1.Visible = true;
                li_sc_upload2.Visible = true;
                li_sc_upload3.Visible = true;
                LinkBtnSavePopup.Visible = true;
                hdnctmstatus.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMStatus"]);
            }
            else
            {
                Txt_Remarks.Enabled = false;
                li_sc_upload1.Visible = false;
                li_sc_upload2.Visible = false;
                li_sc_upload3.Visible = false;
                LinkBtnSavePopup.Visible = false;
                gvSourceCode.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvSourceCode.DataBind();
                spnsourcecode.Visible = true;
            }
        }
        else
        {
            Txt_Remarks.Enabled = false;
            li_sc_upload1.Visible = false;
            li_sc_upload2.Visible = false;
            li_sc_upload3.Visible = false;
            LinkBtnSavePopup.Visible = false;
            gvSourceCode.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvSourceCode.DataBind();
            spnsourcecode.Visible = true;
        }

        if (AllStatus.SelectedValue == "2") //Not Started
        {
            Txt_RevisedActualPreparationStart.Enabled = true;
            txt_RevisedActualPreparationFinish.Enabled = true;
            Txt_RevisedActualSubmission.Enabled = false;
            txt_RevisedActualApproval.Enabled = false;

            submitmendatory.Visible = false;
            approvemendatory.Visible = false;
        }

        if (AllStatus.SelectedValue == "8") //Submitted
        {
            LinkBtnSavePopup.Visible = true;

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

            if (txt_RevisedActualApproval.Text != "")
            {
                txt_RevisedActualApproval.Enabled = true;
            }
            else
            {
                txt_RevisedActualApproval.Enabled = false;
            }
            submitmendatory.Visible = true;
            approvemendatory.Visible = true;
        }

        if (AllStatus.SelectedValue == "5") //Started
        {
            submitmendatory.Visible = true;
            Txt_RevisedActualSubmission.Enabled = true;
            txt_RevisedActualApproval.Enabled = false;
            Txt_RevisedActualPreparationStart.Enabled = false;

            txt_RevisedActualPreparationFinish.Enabled = true;

            if (Convert.ToString(hdnABAPRevisedStartDt.Value).Trim() == "")
            {
                Txt_RevisedActualPreparationStart.Text = "";
            }

            if (Convert.ToString(hdnABAPRevisedFinishDt.Value).Trim() == "")
            {
                txt_RevisedActualPreparationFinish.Text = "";
            }

            if (Convert.ToString(hdnABAPRevisedFinishDt.Value).Trim() != "")
            {
                txt_RevisedActualPreparationFinish.Enabled = true;
            }

        }
        if (AllStatus.SelectedValue == "4")
        {
            LinkBtnSavePopup.Visible = true;
            Txt_RevisedActualPreparationStart.Enabled = false;
            txt_RevisedActualPreparationFinish.Enabled = false;
            Txt_RevisedActualSubmission.Enabled = false;
            txt_RevisedActualApproval.Enabled = false;
        }
        if (AllStatus.SelectedValue == "10")
        {
            Txt_RevisedActualPreparationStart.Enabled = false;
            txt_RevisedActualPreparationFinish.Enabled = false;
            Txt_RevisedActualSubmission.Enabled = false;
            txt_RevisedActualApproval.Enabled = false;
        }

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
            spars[0].Value = "GetDetailsByProjectLocation_ABAPDev_SourceCode";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = (Session["Empcode"]).ToString().Trim();

            if (HDProjectLocation.Value != "")
            {
                DDLProjectLocation.SelectedValue = HDProjectLocation.Value;
            }

            spars[2] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
            spars[2].Value = DDLProjectLocation.SelectedValue == "0" ? "" : DDLProjectLocation.SelectedValue;

            dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");

            ABAP.Visible = false;
            gvABAPDevDetails.Visible = true;
            //btn_SumitTimesheet.Visible = true;
            if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
            {
                btnRedirect.Visible = true;
                hdnABAPDetailsId.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPDetailsId"]);
                string result = string.Join(",", dsABAPObjectPlanSubmitted.Tables[0].AsEnumerable()
                                       .Select(row => row["ABAPDetailsId"].ToString())
                                       .Where(abapDetailsId => !string.IsNullOrEmpty(abapDetailsId)));
                hdnCommaSeperABAPDetailsId.Value = result;

                ABAP.Visible = true;
                gvABAPDevDetails.Visible = true;
                //btn_SumitTimesheet.Visible = true;
                gvABAPDevDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvABAPDevDetails.DataBind();
            }
            else
            {
                btnRedirect.Visible = false;
                gvABAPDevDetails.Visible = false;
                gvABAPDevDetails.DataSource = null;
                gvABAPDevDetails.DataBind();
                lblmessage.Text = "No Record Found.";
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
            lblmessagesub.Text = "Select the Revised Start Date.";
            return;
        }
        if (txt_RevisedActualPreparationFinish.Text == "" && Txt_RevisedActualPreparationStart.Text != "")
        {
            DivIDPopup.Visible = true;
            PnlIrSheet.Visible = true;
            this.ModalPopupExtenderIRSheet.Show();
            lblmessagesub.Text = "Select the Revised Finish Date";
            return;
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
                if (HDStatus.Value == "RGS")
                {
                    lblmessagesub.Text = "Revised/Actual Preparation Start Date must be greater than or equal to Planned Preparation Start Date";
                    return;
                }
                else if (HDStatus.Value == "FS" || HDStatus.Value == "ABAP" || HDStatus.Value == "HBTTest" || HDStatus.Value == "CTMTest")
                {
                    lblmessagesub.Text = "Revised Start Date must be greater than or equal to Planned Start Date.";
                    return;
                }
            }
            else if (finishDate < PlannedPreparationFinishdt)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                if (HDStatus.Value == "RGS")
                {
                    lblmessagesub.Text = "Revised/ Actual Preparation Finish Date must be greater than or equal to Planned Preparation Finish Date.";
                    return;
                }
                if (HDStatus.Value == "FS" || HDStatus.Value == "ABAP" || HDStatus.Value == "HBTTest" || HDStatus.Value == "CTMTest")
                {
                    lblmessagesub.Text = "Revised Finish Date must be greater than or equal to Planned Finish Date.";
                    return;
                }

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
                if (Txt_RevisedActualPreparationStart.Enabled)
                {
                    DivIDPopup.Visible = true;
                    PnlIrSheet.Visible = true;
                    this.ModalPopupExtenderIRSheet.Show();
                    lblmessagesub.Text = "Revised Start Date must not be less than today's date.";
                    return;
                }
            }
            else if (finishDate.Date < DateTime.Now.Date)
            {
                if (txt_RevisedActualPreparationFinish.Enabled)
                {
                    DivIDPopup.Visible = true;
                    PnlIrSheet.Visible = true;
                    this.ModalPopupExtenderIRSheet.Show();
                    lblmessagesub.Text = "Revised END Date must must not be less than today's date.";
                    return;
                }
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
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Actual Start Date must be greater than or equal to Planned Start Date.";
                return;
            }
            else if (finishDate < PlannedPreparationStartdt)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Actual Finish Date must be greater than or equal to Planned Finish Date.";
                return;
            }
            else if (startDate > finishDate)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Actual Start Date must be less than or equal to Actual Finish Date";
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
            if (startDate < PlannedPreparationStartdt)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Actual Start Date must be greater than or equal to Planned Start Date.";
                return;
            }
            if (startDate.Date > DateTime.Now.Date)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Actual Start Date Date must not be future date.";
                return;
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

        if(Convert.ToString(hdnctmstatus.Value) == "17")
        {
        //    DivIDPopup.Visible = true;
        //    PnlIrSheet.Visible = true;
        //    this.ModalPopupExtenderIRSheet.Show();
        //    lblmessagesub.Text = "Uploading the source code is mandatory.";
        //    return;
        //}
        //else
        //{
            string filename = "";
            if (fuSourceCode.HasFile)
            {
                string ABAP_SourceCodeFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ABAPObjectABAPSoruceCode"]).Trim() + "/");
                bool folderExists = Directory.Exists(ABAP_SourceCodeFilePath);
                if (!folderExists)
                {
                    Directory.CreateDirectory(ABAP_SourceCodeFilePath);
                }
                int srno = 1;
                foreach (HttpPostedFile uploadedFile in fuSourceCode.PostedFiles)
                {
                    filename = uploadedFile.FileName;

                    string fileExtension = System.IO.Path.GetExtension(filename);
                    string timestamp = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
                    filename = "ABAP_SourceCode" + timestamp + fileExtension;

                    uploadedFile.SaveAs(Path.Combine(ABAP_SourceCodeFilePath, filename));

                    string uploadedFilePath = ABAP_SourceCodeFilePath + filename;

                    string fullPath = System.IO.Path.GetFullPath(uploadedFilePath);

                    SqlParameter[] sparam = new SqlParameter[3];
                    sparam[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                    sparam[0].Value = "Upload_ABAP_SourceCode";

                    sparam[1] = new SqlParameter("@ID", SqlDbType.Int);
                    sparam[1].Value = Convert.ToInt32(HDID.Value);

                    sparam[2] = new SqlParameter("@filename", SqlDbType.VarChar);
                    sparam[2].Value = filename;

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

        DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (DS != null)
        {
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["ResponseStatusVal"].ToString() == "10")
                {
                    #region Send Email 
                    string strSubject = "";
                    strSubject = DDLProjectLocation.SelectedItem.Text.ToString().Trim() + " ABAP Object - " + Txt_DevelopmentDescription.Text;
                    string Email_TO = hdnHBTConsultantEmail.Value.ToString().Trim();
                    string Email_CC = hdnprojectManagerEmail.Value.ToString().Trim() + ";" + hdnFSConsultantEmail.Value.ToString().Trim();

                    string strInvoiceURL = "";
                    strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["HBTTesting_ABAPObjectPlan"]).Trim());
                    StringBuilder strbuild = new StringBuilder();

                    strbuild.Append("<p style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'></p>");
                    strbuild.Append("<p>" + "Dear " + hdnHBTConsultantName.Value.ToString().Trim() + "</p>");
                    strbuild.Append("<p> This is to inform you that <b>" + hdnABAPperName.Value.ToString().Trim() + "</b> has has complete the object <b> " + Txt_DevelopmentDescription.Text + "</b> development. Please take the action on this Object.</p>");
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
                PnlIrSheet.Visible = false;
                DivIDPopup.Visible = false;
                get_ABAP_Object_Submitted_Plan_FSDetails();
            }
        }
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
            string filePath = Server.MapPath("~/ABAPTracker/FailedHBTTestCase/" + commandArgument);

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
    protected void ibdownloadSourceCode_Click(object sender, EventArgs e)
    {
        ImageButton button = sender as ImageButton;
        if (button != null)
        {
            string commandArgument = button.CommandArgument;
            string filePath = Server.MapPath("~/ABAPTracker/ABAP_SourceCode/" + commandArgument);

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
        submitmendatory.Visible = false;
        approvemendatory.Visible = false;
        lblmessagesub.Text = "";
        string selectedValue = AllStatus.SelectedValue;
        if (selectedValue == "2") //not started
        {
            if (Convert.ToString(hdnABAPRevisedStartDt.Value).Trim() == "")
            {
                Txt_RevisedActualPreparationStart.Text = "";
            }

            if (Convert.ToString(hdnABAPRevisedFinishDt.Value).Trim() == "")
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
            lireason1.Visible = false;
            lireason2.Visible = false;
            lireason3.Visible = false;
            txt_Reason.Text = "";
        }

        if (selectedValue == "5") // Started
        {
            submitmendatory.Visible = true;
            Txt_RevisedActualSubmission.Enabled = true;
            txt_RevisedActualApproval.Enabled = false;
            Txt_RevisedActualPreparationStart.Enabled = false;

            txt_RevisedActualPreparationFinish.Enabled = true;

            if (Convert.ToString(hdnABAPRevisedStartDt.Value).Trim() == "")
            {
                Txt_RevisedActualPreparationStart.Text = "";
            }

            if (Convert.ToString(hdnABAPRevisedFinishDt.Value).Trim() == "")
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


            if (Convert.ToString(hdnABAPRevisedFinishDt.Value).Trim() != "")
            {
                txt_RevisedActualPreparationFinish.Enabled = true;
            }
            lireason1.Visible = false;
            lireason2.Visible = false;
            lireason3.Visible = false;
            txt_Reason.Text = "";
        }

        if (selectedValue == "10") // Submit For Functional Testing
        {
            submitmendatory.Visible = true;
            approvemendatory.Visible = true;
            txt_RevisedActualApproval.Enabled = false;
            Txt_RevisedActualPreparationStart.Enabled = false;
            txt_RevisedActualPreparationFinish.Enabled = false;
            if (Convert.ToString(txt_RevisedActualApproval.Text).Trim() == "")
            {
                Txt_RevisedActualSubmission.Enabled = true;
                txt_RevisedActualApproval.Enabled = true;

            }

            if (Txt_RevisedActualSubmission.Text != "")    // new added 20-1-2025
            {
                Txt_RevisedActualSubmission.Enabled = false;
            }

            if (Convert.ToString(hdnABAPRevisedStartDt.Value).Trim() == "")
            {
                Txt_RevisedActualPreparationStart.Text = "";
            }

            if (Convert.ToString(hdnABAPRevisedFinishDt.Value).Trim() == "")
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

            txt_RevisedActualPreparationFinish.Enabled = false;
            lireason1.Visible = false;
            lireason2.Visible = false;
            lireason3.Visible = false;
            txt_Reason.Text = "";
        }

        if (selectedValue == "18")
        {
            if (Convert.ToString(hdnABAPRevisedStartDt.Value).Trim() == "")
            {
                Txt_RevisedActualPreparationStart.Text = "";
            }

            if (Convert.ToString(hdnABAPRevisedFinishDt.Value).Trim() == "")
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

            lireason1.Visible = true;
            lireason2.Visible = true;
            lireason3.Visible = true;
            txt_Reason.Text = hdnReason.Value;
        }
        if (selectedValue == "4")
        {
            if (Convert.ToString(hdnABAPRevisedStartDt.Value).Trim() == "")
            {
                Txt_RevisedActualPreparationStart.Text = "";
            }

            if (Convert.ToString(hdnABAPRevisedFinishDt.Value).Trim() == "")
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
    protected void btnRedirect_Click(object sender, EventArgs e)
    {
        Session["CommaSeperABAPDetailsId"] = hdnCommaSeperABAPDetailsId.Value.ToString();
        Session["projectCode"] = HDProjectLocation.Value.ToString();
        Response.Redirect("~/procs/ABAP_Object_Tracker_Change_Status_ABAP_Timesheet_New.aspx");
    }
    #endregion

}