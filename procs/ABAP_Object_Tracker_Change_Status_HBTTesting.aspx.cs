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

public partial class ABAP_Object_Tracker_Change_Status_HBTTesting : System.Web.UI.Page
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
                DDLProjectLocation.Visible = true;
                spnLabel.Visible = true;
                DDLProjectLocation.DataSource = DS.Tables[0];
                DDLProjectLocation.DataTextField = "Location_name";
                DDLProjectLocation.DataValueField = "comp_code";
                DDLProjectLocation.DataBind();
                DDLProjectLocation.Items.Insert(0, new ListItem("Select Project Location", "0"));

                if (DS.Tables[0].Rows.Count == 1)
                {
                    hbttest.Visible = false;
                    gvHBTTestDetails.Visible = false;

                    liProjectMangaer.Visible = true;
                    HDProjectLocation.Value = DS.Tables[0].Rows[0]["comp_code"].ToString();
                    get_ABAP_Object_Submitted_Plan_FSDetails();
                }

            }
            else
            {
                DDLProjectLocation.Visible = false;
                spnLabel.Visible = false;
                DDLProjectLocation.Items.Insert(0, new ListItem("Select Project Location", "0"));
                lbl_error.Text = "Change status of ABAP object HBT Stage is restricted to HBT users only.";
            }
        }
        catch (Exception ex)
        {
            lbl_error.Text = ex.Message.ToString();
            return;
        }
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

        this.ModalPopupExtenderIRSheet.Show();
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
        if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
        {
            Txt_DevelopmentDescription.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Development_Desc"]);
            Txt_Module.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ModuleDesc"]);
            Txt_Interface.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Interface"]);
            Txt_FRICECategory.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FCategoryName"]);
            Txt_Order.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Priority"]);
            Txt_Priority.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PriorityName"]);
            Txt_Complexity.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ComplexityName"]);
            if (string.IsNullOrEmpty(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"])) || Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"]) == "10")
            {
                if (Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"]) == "18" && Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["StageType"]) == "CTM")
                {
                    spnreason.Visible = true;
                    txt_Reason.Visible = true;
                    txt_Reason.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ReasonName"]);
                    DDLReason.Items.Insert(0, new ListItem("Select the reason", "0"));
                }
                else {
                    lblreason.Visible = false;
                    txt_Reason.Visible = false;

                    spnreason.Visible = false;
                    DDLReason.Visible = false;
                    txt_Reason.Text = "";
                }
                AllStatus.Items.Clear();
                AllStatus.Items.Add(new ListItem(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatuss"]), Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"])));
                AllStatus.Items.Add(new ListItem("Hold", "4"));
                AllStatus.Items.Add(new ListItem("Started", "5"));
                AllStatus.Items.Add(new ListItem("Passed", "17"));
                AllStatus.Items.Add(new ListItem("Failed", "18"));

                AllStatus.SelectedValue = Convert.ToString(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"]));
            }
            else if (string.IsNullOrEmpty(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"])) || Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"]) == "18")
            {
                if (Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"]) == "18" && Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["StageType"]) == "CTM")
                {
                    spnreason.Visible = true;
                    txt_Reason.Visible = true;
                    txt_Reason.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ReasonName"]);

                    DDLReason.Items.Insert(0, new ListItem("Select the reason", "0"));

                    //spnreason.Visible = false;
                    //DDLReason.Visible = false;
                    //Span1.Visible = false;
                }
                //else if (Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"]) == "18" && (Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["StageType"]) == "HBTFS" || Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["StageType"]) == "HBTABAP"))
                //{
                //    spnreason.Visible = false;
                //    Span3.Visible = false;
                //    txt_Reason.Visible = false;
                //}
                else
                {
                    lblreason.Visible = false;
                    txt_Reason.Visible = false;

                    spnreason.Visible = false;
                    DDLReason.Visible = false;
                    txt_Reason.Text = "";
                }

               
                AllStatus.Items.Clear();
                AllStatus.Items.Add(new ListItem(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatuss"]), Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"])));
                AllStatus.Items.Add(new ListItem("CTM Testing", "12"));

                AllStatus.SelectedValue = Convert.ToString(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"]));
            }
            else if (string.IsNullOrEmpty(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"])) || Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"]) == "12")
            {
                if (Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"]) == "18" && Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["StageType"]) == "CTM")
                {
                    spnreason.Visible = true;
                    Span3.Visible = true;
                    txt_Reason.Visible = true;
                    txt_Reason.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ReasonName"]);

                    DDLReason.Items.Insert(0, new ListItem("Select the reason", "0"));

                    //spnreason.Visible = false;
                    //DDLReason.Visible = false;
                    //Span1.Visible = false;
                }
                //else if (Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"]) == "18" && (Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["StageType"]) == "HBTFS" || Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["StageType"]) == "HBTABAP"))
                //{
                //    spnreason.Visible = false;
                //    Span3.Visible = false;
                //    txt_Reason.Visible = false;
                //}
                else
                {
                    spnreason.Visible = false;
                    Span3.Visible = false;
                    DDLReason.Visible = false;
                    txt_Reason.Text = "";
                }

                AllStatus.Items.Clear();
                AllStatus.Items.Add(new ListItem(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatuss"]), Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"])));
                AllStatus.SelectedValue = Convert.ToString(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"]));
            }
            else
            {
                if (Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"]) == "18" && Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["StageType"]) == "CTM")
                {
                    spnreason.Visible = true;
                    txt_Reason.Visible = true;
                    txt_Reason.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ReasonName"]);

                    DDLReason.Items.Insert(0, new ListItem("Select the reason", "0"));

                    lblreason.Visible = false;
                    txt_Reason.Visible = false;

                    //spnreason.Visible = false;
                    //DDLReason.Visible = false;
                    //Span1.Visible = false;
                }
                //else if (Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"]) == "18" && (Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["StageType"]) == "HBTFS" || Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["StageType"]) == "HBTABAP"))
                //{
                //    spnreason.Visible = false;
                //    Span3.Visible = false;
                //    txt_Reason.Visible = false;
                //}
                else
                {
                    spnreason.Visible = false;
                    DDLReason.Visible = false;
                    txt_Reason.Text = "";


                    lblreason.Visible = false;
                    txt_Reason.Visible = false;
                }
                if (dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"].ToString() == "5")
                {
                    ListItem itemToRemove = AllStatus.Items.FindByValue("2");
                    if (itemToRemove != null)
                    {
                        AllStatus.Items.Remove(itemToRemove);
                    }
                }
                if (dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"].ToString() == "4")
                {
                    ListItem itemToRemove = AllStatus.Items.FindByValue("2");
                    if (itemToRemove != null)
                    {
                        AllStatus.Items.Remove(itemToRemove);
                    }
                }
                AllStatus.SelectedValue = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"]);
            }

            if ((AllStatus.SelectedValue == "17" || AllStatus.SelectedValue == "12") && !string.IsNullOrEmpty(AllStatus.SelectedValue))
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

            if (!string.IsNullOrEmpty(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Correction_Case"])))
            {
                ddlStage.SelectedValue = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Correction_Case"]);
            }
            else
            {
                spnreason.Visible = false;
                Span1.Visible = false;
                DDLReason.Visible = false;
            }

            HDStatusCheck.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTTestStatus"]);
            HDStatusCheckFlag.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CheckStatusName"]);

            Txt_PlannedPreparationStart.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PlannedStart"]);
            txt_PlannedPreparationFinish.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PlannedFinish"]);

            Txt_RevisedActualPreparationStart.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["hbtRevisedStart"]);
            txt_RevisedActualPreparationFinish.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["hbtRevisedFinish"]);
            Txt_RevisedActualSubmission.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["hbtActualStart"]);
            txt_RevisedActualApproval.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["hbtActualFinish"]);

            hdnRevisedStartDt.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["hbtRevisedStart"]);
            hdnRevisedFinishDt.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["hbtRevisedFinish"]);
            HDDateCheck3.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["hbtActualStart"]);
            HDDateCheck4.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["hbtActualFinish"]);
            Txt_Remarks.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);
            HDRemarkcheck.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);




            string stropennotopen = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Checkflag"]);
            if (stropennotopen == "open")
            {
                //if (HDStatusCheckFlag.Value == "Functional Testing" && !string.IsNullOrEmpty(HDDateCheck4.Value))
                //{
                //    LinkBtnSavePopup.Visible = false;
                //}
                //else
                //{
                //    LinkBtnSavePopup.Visible = true;
                //}
                if ((HDStatusCheckFlag.Value == "Test Case Approved" || HDStatusCheckFlag.Value == "CTM Testing") && !string.IsNullOrEmpty(HDDateCheck4.Value))
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
        if (dsABAPObjectPlanSubmitted.Tables[4].Rows.Count > 0)
        {
            hdnCTMConsultantEmail.Value = dsABAPObjectPlanSubmitted.Tables[4].Rows[0]["CTMConsultantEmail"].ToString().Trim();
            hdnCTMConsultantName.Value = dsABAPObjectPlanSubmitted.Tables[4].Rows[0]["CTMConsultantName"].ToString().Trim();
        }

        #region Get HBT Test Cases failed Attachment
        var HBTFailedfiles = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["HBTFailedFilesNames"]);
        if (!string.IsNullOrEmpty(HBTFailedfiles))
        {
            lihbttestcase_1.Visible = false;
            lihbttestcase_2.Visible = false;
            lihbttestcase_3.Visible = false;
            DataSet objDS = new DataSet();
            SqlParameter[] objSpars = new SqlParameter[3];

            objSpars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            objSpars[0].Value = "GetHBTFailedABAPAttachmentsfromDB";

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


        submitmendatory.Visible = false;
        if (AllStatus.SelectedValue == "2") //not started
        {
            Txt_RevisedActualPreparationStart.Enabled = true;
            txt_RevisedActualPreparationFinish.Enabled = true;
            Txt_RevisedActualSubmission.Enabled = false;
            txt_RevisedActualApproval.Enabled = false;
            li_ddl_stage_1.Visible = false;
            li_ddl_stage_2.Visible = false;
            li_ddl_stage_3.Visible = false;
            lihbttestcase_1.Visible = false;
            lihbttestcase_2.Visible = false;
            lihbttestcase_3.Visible = false;

            submitmendatory.Visible = false;
            approvemendatory.Visible = false;
        }

        if (AllStatus.SelectedValue == "5") // Started
        {
            submitmendatory.Visible = true;
            Txt_RevisedActualSubmission.Enabled = true;
            txt_RevisedActualApproval.Enabled = false;
            Txt_RevisedActualPreparationStart.Enabled = false;

            //txt_RevisedActualPreparationFinish.Enabled = true;

            if (Convert.ToString(hdnRevisedStartDt.Value).Trim() == "")
            {
                Txt_RevisedActualPreparationStart.Text = "";
            }

            if (Convert.ToString(hdnRevisedFinishDt.Value).Trim() == "")
            {
                txt_RevisedActualPreparationFinish.Text = "";
            }

            if (Convert.ToString(hdnRevisedFinishDt.Value).Trim() != "")
            {
                txt_RevisedActualPreparationFinish.Enabled = true;
            }
            else
            {
                txt_RevisedActualPreparationFinish.Enabled = false;
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
        if (AllStatus.SelectedValue == "17" || AllStatus.SelectedValue == "12")  // Passed
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

            lihbttestcase_1.Visible = false;
            lihbttestcase_2.Visible = false;
            lihbttestcase_3.Visible = false;
            submitmendatory.Visible = true;
            approvemendatory.Visible = true;
            Txt_RevisedActualSubmission.Enabled = true;
            if (Convert.ToString(Txt_RevisedActualSubmission.Text).Trim() != "")
            {
                Txt_RevisedActualSubmission.Enabled = false;
            }
            txt_RevisedActualApproval.Enabled = false;
            Txt_RevisedActualPreparationStart.Enabled = false;
            txt_RevisedActualPreparationFinish.Enabled = false;
            li_ddl_stage_1.Visible = false;
            li_ddl_stage_2.Visible = false;
            li_ddl_stage_3.Visible = false;

            gvHBTTestCaseFailedFiles.Visible = false;
        }

        if (AllStatus.SelectedValue == "18")  // Test Case Correction/Failed
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

            lihbttestcase_1.Visible = true;
            lihbttestcase_2.Visible = true;
            lihbttestcase_3.Visible = true;
            spnstagemandatory.Visible = true;
            submitmendatory.Visible = true;
            approvemendatory.Visible = true;
            Txt_RevisedActualSubmission.Enabled = true;

            if (Convert.ToString(Txt_RevisedActualSubmission.Text).Trim() != "")
            {
                Txt_RevisedActualSubmission.Enabled = false;
            }
            txt_RevisedActualApproval.Enabled = true;
            Txt_RevisedActualPreparationStart.Enabled = false;
            txt_RevisedActualPreparationFinish.Enabled = false;


            li_ddl_stage_1.Visible = true;
            li_ddl_stage_2.Visible = true;
            li_ddl_stage_3.Visible = true;
            HtmlGenericControl myList = (HtmlGenericControl)FindControl("editform1");
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
            spars[0].Value = "GetRGSFSABAPHBTTestingDetailsByProjectLocation_HBTTest";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = (Session["Empcode"]).ToString().Trim();

            if (HDProjectLocation.Value != "")
            {
                DDLProjectLocation.SelectedValue = HDProjectLocation.Value;
            }

            spars[2] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
            spars[2].Value = DDLProjectLocation.SelectedValue == "0" ? "" : DDLProjectLocation.SelectedValue;

            dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");


            hbttest.Visible = false;

            if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
            {
                hbttest.Visible = true;
                gvHBTTestDetails.Visible = true;
                gvHBTTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvHBTTestDetails.DataBind();
            }
            else
            {
                lblmessage.Text = "No Record Found.";
                gvHBTTestDetails.Visible = false;
                gvHBTTestDetails.DataSource = null;
                gvHBTTestDetails.DataBind();

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

        gvHBTTestDetails.Visible = false;
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

        if (AllStatus.SelectedValue == "5")
        {
            if (Txt_RevisedActualSubmission.Text == "")
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "You cannot set the status to Failed if the Actual Start Date has not been entered.";
                return;
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


            if (Txt_RevisedActualSubmission.Text == "" || txt_RevisedActualApproval.Text == "")
            {
                if (Txt_RevisedActualSubmission.Text == "")
                {
                    DivIDPopup.Visible = true;
                    PnlIrSheet.Visible = true;
                    this.ModalPopupExtenderIRSheet.Show();
                    lblmessagesub.Text = "You cannot set the status to Failed if the Actual Start Date has not been entered.";
                    return;
                }

                if (txt_RevisedActualApproval.Text == "")
                {
                    DivIDPopup.Visible = true;
                    PnlIrSheet.Visible = true;
                    this.ModalPopupExtenderIRSheet.Show();
                    lblmessagesub.Text = "You cannot set the status to Failed if the Actual Finish Date has not been entered.";
                    return;
                }
            }

            if (ddlStage.SelectedValue == "")
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Please select the stage.";
                return;
            }

            if (!uplHBTTestCase.HasFile)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Please Upload the HBT Test Cases.";
                return;
            }
        }

        if ((AllStatus.SelectedValue == "17" || AllStatus.SelectedValue == "12"))
        {
            if (Txt_RevisedActualSubmission.Text == "" || txt_RevisedActualApproval.Text == "")
            {
                if (Txt_RevisedActualSubmission.Text == "")
                {
                    DivIDPopup.Visible = true;
                    PnlIrSheet.Visible = true;
                    this.ModalPopupExtenderIRSheet.Show();
                    lblmessagesub.Text = "You cannot set the status to Passed if the Actual Start Date has not been entered.";
                    return;
                }

                if (txt_RevisedActualApproval.Text == "")
                {
                    DivIDPopup.Visible = true;
                    PnlIrSheet.Visible = true;
                    this.ModalPopupExtenderIRSheet.Show();
                    lblmessagesub.Text = "You cannot set the status to Passed if the Actual Finish Date has not been entered.";
                    return;
                }
            }

            if (!uplHBTTestCase.HasFile)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Please Upload the HBT Test Cases.";
                return;
            }
        }

        string CTMTestCasefilename = "";
        string UATSignOfffilename = "";
        string filename = "";



        SqlParameter[] spars = new SqlParameter[14];
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

        spars[13] = new SqlParameter("@ReasonId", SqlDbType.VarChar);
        spars[13].Value = DDLReason.SelectedValue == "0" ? "" : DDLReason.SelectedValue.Trim();

        DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (DS != null)
        {
            if (DS.Tables[0].Rows[0]["ResponseStatusVal"].ToString() == "17" || DS.Tables[0].Rows[0]["ResponseStatusVal"].ToString() == "12")
            {
                if (uplHBTTestCase.HasFile)
                {
                    string FSAttachmentFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ABAPObjectPassedHBTTestCase"]).Trim() + "/");
                    bool folderExists = Directory.Exists(FSAttachmentFilePath);
                    if (!folderExists)
                    {
                        Directory.CreateDirectory(FSAttachmentFilePath);
                    }

                    foreach (HttpPostedFile uploadedFile in uplHBTTestCase.PostedFiles)
                    {
                        filename = uploadedFile.FileName;
                        string fileExtension = System.IO.Path.GetExtension(filename);
                        string timestamp = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
                        filename = "HBT_PassedTestCase" + timestamp + fileExtension;
                        uploadedFile.SaveAs(Path.Combine(FSAttachmentFilePath, filename));
                        string uploadedFilePath = FSAttachmentFilePath + filename;
                        string fullPath = System.IO.Path.GetFullPath(uploadedFilePath);

                        SqlParameter[] sparam = new SqlParameter[4];
                        sparam[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                        sparam[0].Value = "Upload_HBTTestCases";

                        sparam[1] = new SqlParameter("@ID", SqlDbType.Int);
                        sparam[1].Value = Convert.ToInt32(HDID.Value);

                        sparam[2] = new SqlParameter("@FileType", SqlDbType.VarChar);
                        sparam[2].Value = "PassedHBTTestCase";

                        sparam[3] = new SqlParameter("@filename", SqlDbType.VarChar);
                        sparam[3].Value = filename;


                        DataSet ObjDS = spm.getDatasetList(sparam, "SP_ABAPObjectTracking");
                        if (ObjDS == null)
                        {
                            DivIDPopup.Visible = true;
                            PnlIrSheet.Visible = true;
                            this.ModalPopupExtenderIRSheet.Show();
                            lblmessagesub.Text = "Error in Attchment Upload.";
                            return;
                        }
                    }
                }

                #region Send Email to CTM User 
                string strSubject = "";
                strSubject = DDLProjectLocation.SelectedItem.Text.ToString().Trim() + " ABAP Object " + Txt_DevelopmentDescription.Text.ToString().Trim();
                string Email_TO = hdnCTMConsultantEmail.Value.ToString().Trim();
                string Email_CC = hdnprojectManagerEmail.Value.ToString().Trim();

                string strInvoiceURL = "";
                strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["CTMTesting_ABAPObjectPlan"]).Trim());
                StringBuilder strbuild = new StringBuilder();

                strbuild.Append("<p style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'></p>");
                strbuild.Append("<p>" + "Dear " + hdnCTMConsultantName.Value.ToString().Trim() + "</p>");
                strbuild.Append("<p> This is to inform you that the object <b>" + Txt_DevelopmentDescription.Text.ToString().Trim() + "</b> for Testing please click on below lnk for action.</p>");
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

            if (DS.Tables[0].Rows[0]["ResponseStatusVal"].ToString() == "17" || DS.Tables[0].Rows[0]["ResponseStatusVal"].ToString() == "12")
            {
                #region Send Email to ABAPe User 
                string strSubject = "";
                strSubject = DDLProjectLocation.SelectedItem.Text.ToString().Trim() + " ABAP Object " + Txt_DevelopmentDescription.Text.ToString().Trim();
                string Email_TO = hdnCTMConsultantEmail.Value.ToString().Trim();
                string Email_CC = hdnprojectManagerEmail.Value.ToString().Trim();

                StringBuilder strbuild = new StringBuilder();

                strbuild.Append("<p style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'></p>");
                strbuild.Append("<p>" + "Dear " + hdnABAPperName.Value.ToString().Trim() + "</p>");
                strbuild.Append("<p> This is to inform you that <b>" + hdnHBTConsultantName.Value.ToString().Trim() + "</b> has approved the object <b>" + Txt_DevelopmentDescription.Text.ToString().Trim() + "</b>.</p>");
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
                strbuild.Append("<br><br>This is an auto generated email, please do not reply!");

                //spm.sendMail(Email_TO, strSubject, Convert.ToString(strbuild).Trim(), "", Email_CC);

                #endregion
            }

            if (DS.Tables[0].Rows[0]["ResponseStatusVal"].ToString() == "18" && DS.Tables[0].Rows[0]["ResponseCorrectionCase"].ToString() == "ABAP")
            {
                if (uplHBTTestCase.HasFile)
                {
                    string FSAttachmentFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ABAPObjectFailedHBTTestCase"]).Trim() + "/");
                    bool folderExists = Directory.Exists(FSAttachmentFilePath);
                    if (!folderExists)
                    {
                        Directory.CreateDirectory(FSAttachmentFilePath);
                    }

                    foreach (HttpPostedFile uploadedFile in uplHBTTestCase.PostedFiles)
                    {
                        filename = uploadedFile.FileName;
                        string fileExtension = System.IO.Path.GetExtension(filename);
                        string timestamp = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
                        filename = "HBT_FailedTestCaseToABAP" + timestamp + fileExtension;
                        uploadedFile.SaveAs(Path.Combine(FSAttachmentFilePath, filename));
                        string uploadedFilePath = FSAttachmentFilePath + filename;
                        string fullPath = System.IO.Path.GetFullPath(uploadedFilePath);

                        SqlParameter[] sparam = new SqlParameter[4];
                        sparam[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                        sparam[0].Value = "Upload_HBTTestCases";

                        sparam[1] = new SqlParameter("@ID", SqlDbType.Int);
                        sparam[1].Value = Convert.ToInt32(HDID.Value);

                        sparam[2] = new SqlParameter("@FileType", SqlDbType.VarChar);
                        sparam[2].Value = "FailedHBTTestCaseToABAP";

                        sparam[3] = new SqlParameter("@filename", SqlDbType.VarChar);
                        sparam[3].Value = filename;


                        DataSet ObjDS = spm.getDatasetList(sparam, "SP_ABAPObjectTracking");
                        if (ObjDS == null)
                        {
                            DivIDPopup.Visible = true;
                            PnlIrSheet.Visible = true;
                            this.ModalPopupExtenderIRSheet.Show();
                            lblmessagesub.Text = "Error in Attchment Upload.";
                            return;
                        }
                    }
                }

                #region Send Email to ABAP User 
                string strSubject = "";
                strSubject = DDLProjectLocation.SelectedItem.Text.ToString().Trim() + " ABAP Object -" + Txt_DevelopmentDescription.Text.ToString().Trim();
                string Email_TO = hdnABAPperEmail.Value.ToString().Trim();
                string Email_CC = hdnprojectManagerEmail.Value.ToString().Trim();

                string strInvoiceURL = "";
                strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["ABAPStatus_ABAPObjectPlan"]).Trim());
                StringBuilder strbuild = new StringBuilder();

                strbuild.Append("<p style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'></p>");
                strbuild.Append("<p>" + "Dear " + hdnABAPperName.Value.ToString().Trim() + "</p>");
                strbuild.Append("<p> This is to inform you that <b>" + hdnHBTConsultantName.Value.ToString().Trim() + "</b> has send for correction the object <b>" + Txt_DevelopmentDescription.Text.ToString().Trim() + "</b>. Please review the object & resubmit.</p>");
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

            if (DS.Tables[0].Rows[0]["ResponseStatusVal"].ToString() == "18" && DS.Tables[0].Rows[0]["ResponseCorrectionCase"].ToString() == "FS")
            {
                if (uplHBTTestCase.HasFile)
                {
                    string FSAttachmentFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ABAPObjectFailedHBTTestCase"]).Trim() + "/");
                    bool folderExists = Directory.Exists(FSAttachmentFilePath);
                    if (!folderExists)
                    {
                        Directory.CreateDirectory(FSAttachmentFilePath);
                    }

                    foreach (HttpPostedFile uploadedFile in uplHBTTestCase.PostedFiles)
                    {
                        filename = uploadedFile.FileName;
                        string fileExtension = System.IO.Path.GetExtension(filename);
                        string timestamp = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
                        filename = "HBT_FailedTestCaseToFS" + timestamp + fileExtension;
                        uploadedFile.SaveAs(Path.Combine(FSAttachmentFilePath, filename));
                        string uploadedFilePath = FSAttachmentFilePath + filename;
                        string fullPath = System.IO.Path.GetFullPath(uploadedFilePath);

                        SqlParameter[] sparam = new SqlParameter[4];
                        sparam[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                        sparam[0].Value = "Upload_HBTTestCases";

                        sparam[1] = new SqlParameter("@ID", SqlDbType.Int);
                        sparam[1].Value = Convert.ToInt32(HDID.Value);

                        sparam[2] = new SqlParameter("@FileType", SqlDbType.VarChar);
                        sparam[2].Value = "FailedHBTTestCaseToFS";

                        sparam[3] = new SqlParameter("@filename", SqlDbType.VarChar);
                        sparam[3].Value = filename;


                        DataSet ObjDS = spm.getDatasetList(sparam, "SP_ABAPObjectTracking");
                        if (ObjDS == null)
                        {
                            DivIDPopup.Visible = true;
                            PnlIrSheet.Visible = true;
                            this.ModalPopupExtenderIRSheet.Show();
                            lblmessagesub.Text = "Error in Attchment Upload.";
                            return;
                        }
                    }
                }

                #region Send Email to FS User with cc as ABAP User
                string strSubject = "";
                strSubject = DDLProjectLocation.SelectedItem.Text.ToString().Trim() + " ABAP Object -" + Txt_DevelopmentDescription.Text.ToString().Trim();
                string Email_TO = hdnFSConsultantEmail.Value.ToString().Trim();
                string Email_CC = hdnprojectManagerEmail.Value.ToString().Trim() + "; " + hdnABAPperEmail.Value.ToString().Trim();

                string strInvoiceURL = "";
                strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["ABAPNotAccept_ABAPObjectPlan"]).Trim());
                StringBuilder strbuild = new StringBuilder();

                strbuild.Append("<p style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'></p>");
                strbuild.Append("<p>" + "Dear " + hdnFSConsultantName.Value.ToString().Trim() + "</p>");
                strbuild.Append("<p> This is to inform you that <b>" + hdnHBTConsultantName.Value.ToString().Trim() + "</b> has send for correction the object <b>" + Txt_DevelopmentDescription.Text.ToString().Trim() + "</b>. Please review the object & resubmit.</p>");
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

    protected void AllStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmessagesub.Text = "";
        string selectedValue = AllStatus.SelectedValue;
        submitmendatory.Visible = false;
        approvemendatory.Visible = false;
        spnstagemandatory.Visible = false;
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
            li_ddl_stage_1.Visible = false;
            li_ddl_stage_2.Visible = false;
            li_ddl_stage_3.Visible = false;
            lihbttestcase_1.Visible = false;
            lihbttestcase_2.Visible = false;
            lihbttestcase_3.Visible = false;
        }

        if (selectedValue == "5") // Started
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

            if (Convert.ToString(hdnRevisedFinishDt.Value).Trim() != "")
            {
                txt_RevisedActualPreparationFinish.Enabled = true;
            }
            else
            {
                txt_RevisedActualPreparationFinish.Enabled = false;
            }
            li_ddl_stage_1.Visible = false;
            li_ddl_stage_2.Visible = false;
            li_ddl_stage_3.Visible = false;
            lihbttestcase_1.Visible = false;
            lihbttestcase_2.Visible = false;
            lihbttestcase_3.Visible = false;
        }

        if (selectedValue == "17" || selectedValue == "12")  // Passed
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

            lihbttestcase_1.Visible = true;
            lihbttestcase_2.Visible = true;
            lihbttestcase_3.Visible = true;
            submitmendatory.Visible = true;
            approvemendatory.Visible = true;
            Txt_RevisedActualSubmission.Enabled = true;
            if (Convert.ToString(Txt_RevisedActualSubmission.Text).Trim() != "")
            {
                Txt_RevisedActualSubmission.Enabled = false;
            }
            txt_RevisedActualApproval.Enabled = true;
            Txt_RevisedActualPreparationStart.Enabled = false;
            txt_RevisedActualPreparationFinish.Enabled = false;
            li_ddl_stage_1.Visible = false;
            li_ddl_stage_2.Visible = false;
            li_ddl_stage_3.Visible = false;

            gvHBTTestCaseFailedFiles.Visible = false;
        }

        if (selectedValue == "18")  // Test Case Correction/Failed
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
             
            lihbttestcase_1.Visible = true;
            lihbttestcase_2.Visible = true;
            lihbttestcase_3.Visible = true;
            spnstagemandatory.Visible = true;
            submitmendatory.Visible = true;
            approvemendatory.Visible = true;
            Txt_RevisedActualSubmission.Enabled = true;

            if (Convert.ToString(Txt_RevisedActualSubmission.Text).Trim() != "")
            {
                Txt_RevisedActualSubmission.Enabled = false;
            }
            txt_RevisedActualApproval.Enabled = true;
            Txt_RevisedActualPreparationStart.Enabled = false;
            txt_RevisedActualPreparationFinish.Enabled = false;


            li_ddl_stage_1.Visible = true;
            li_ddl_stage_2.Visible = true;
            li_ddl_stage_3.Visible = true;
            HtmlGenericControl myList = (HtmlGenericControl)FindControl("editform1");
            ddlStage_SelectedIndexChanged(sender, e);
        }
        //else
        //{
        //    li_ddl_stage_1.Visible = false;
        //    lihbttestcase.Visible = false;
        //    li_ddl_stage_3.Visible = false;
        //}

        if (selectedValue == "4")
        {
            LinkBtnSavePopup.Visible = true;
            Txt_RevisedActualPreparationStart.Enabled = false;
            txt_RevisedActualPreparationFinish.Enabled = false;
            Txt_RevisedActualSubmission.Enabled = false;
            txt_RevisedActualApproval.Enabled = false;
        }


        DivIDPopup.Visible = true;
        PnlIrSheet.Visible = true;
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
    protected void ddlStage_SelectedIndexChanged(object sender, EventArgs e)
    {
        var stageval = Convert.ToString(ddlStage.SelectedValue).Trim();
        var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        if (stageval == "FS")
        {
            spars[0].Value = "GetReasonForHBTFS";
        }
        else if (stageval == "ABAP")
        {
            spars[0].Value = "GetReasonForHBTABAP";
        }
        else
        {
            spars[0].Value = DBNull.Value;
        }
        DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
        {
            DDLReason.Visible = true;
            spnreason.Visible = true;
            Span1.Visible = true;
            DDLReason.DataSource = DS.Tables[0];
            DDLReason.DataTextField = "ReasonName";
            DDLReason.DataValueField = "Reason_Id";
            DDLReason.DataBind();
            DDLReason.Items.Insert(0, new ListItem("Select the reason", "0"));
        }
        else
        {
            DDLReason.Visible = false;
            spnreason.Visible = false;
            Span1.Visible = false;
            DDLReason.Items.Insert(0, new ListItem("Select the reason", "0"));
        }


        DivIDPopup.Visible = true;
        PnlIrSheet.Visible = true;
        this.ModalPopupExtenderIRSheet.Show();
    }


    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        gvHBTTestDetails.Columns[0].Visible = false;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=HBTTestDetailPlanExport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                // Hide buttons or controls not to export
                gvHBTTestDetails.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        gvHBTTestDetails.Columns[0].Visible = true;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms the GridView was rendered
    }
    #endregion

}