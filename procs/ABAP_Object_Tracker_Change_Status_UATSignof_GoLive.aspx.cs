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

public partial class procs_ABAP_Object_Tracker_Change_Status_UATSignof_GoLive : System.Web.UI.Page
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

            Txt_PlannedPreparationStart.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            txt_RevisedDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            Txt_ActualDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            Txt_Final_Script_Sub_Date.Attributes.Add("onkeypress", "return noanyCharecters(event);");

            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
            txtEmpCode.Text = Convert.ToString(Session["Empcode"]);

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/vscb_index.aspx");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    getProjectLocation();
                    get_ABAP_Object_Submitted_Plan_FSDetails();

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
            spars[0].Value = "GetLocationForFunctionalUATSignOffGoLive";

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
            }
            else
            {
                DDLProjectLocation.Items.Insert(0, new ListItem("Select Project Location", "0"));
                lbl_error.Text = "Change status of UAT Sign Off is restricted to UAT Sign Off Users only.";
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
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        HDID.Value = Convert.ToString(gvRGSDetails.DataKeys[row.RowIndex].Values[0]).Trim();
        getStatus("UAT");
        lblmessagesub.Text = "";
        UATDisplay1.Visible = true;
        UATDisplay2.Visible = true;
        UATDisplay3.Visible = true;
        Label1.Text = "Planned Date";
        label2.Text = "Revised Date";
        Label3.Text = "Actual Date";
        get_UAT_Detail();
        HDStatus.Value = "UAT";
        DivIDPopup.Visible = true;
        lbl5.Text = "UAT Status";
        this.ModalPopupExtenderIRSheet.Show();

    }

    protected void lnkEdit_Click1(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        HDID.Value = Convert.ToString(gvFSDetails.DataKeys[row.RowIndex].Values[0]).Trim();
        HDStatus.Value = "GOLive";
        lbl5.Text = "GO Live Status";
        getStatus("GOLive");

        Label1.Text = "Go-Live Planned Date";
        label2.Text = "Go-Live Revised Date";
        Label3.Text = "Go-Live Actual Date";
        UATDisplay1.Visible = false;
        UATDisplay2.Visible = false;
        UATDisplay3.Visible = false;
        AllStatus.Enabled = false;
        get_GOLive_Detail();
        lblmessagesub.Text = "";
        DivIDPopup.Visible = true;
        this.ModalPopupExtenderIRSheet.Show();
    }

    public void get_UAT_Detail()
    {

        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetUAT_DetailBYID";

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
        AllStatus.SelectedValue = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["UAT_Status"]);
       
        HDStatusCheck.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["UAT_Status"]);
        // HDStatusCheckFlag.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CheckStatusName"]);

        Txt_PlannedPreparationStart.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPPlannedStart"]);
        Txt_Final_Script_Sub_Date.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Final_Script_Sub_Date"]);
        txt_RevisedDate.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Revised_Date"]);
        Txt_ActualDate.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Actual_Date"]);

        HDDateCheck1.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Final_Script_Sub_Date"]);
        HDDateCheck2.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Revised_Date"]);
        HDDateCheck3.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Actual_Date"]);

        Txt_Remarks.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);
        HDRemarkcheck.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);
    }

    public void get_GOLive_Detail()
    {

        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetGOLive_DetailBYID";

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
        AllStatus.SelectedValue = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PRD_Status"]);
       
        HDStatusCheck.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PRD_Status"]);
        //HDStatusCheckFlag.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CheckStatusName"]);

        Txt_PlannedPreparationStart.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PlannedDate"]);
        txt_RevisedDate.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RevisedDate"]);
        Txt_ActualDate.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ActualDate"]);

        HDDateCheck1.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PlannedDate"]);
        HDDateCheck2.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RevisedDate"]);
        HDDateCheck3.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ActualDate"]);

        Txt_Remarks.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);
        HDRemarkcheck.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);

        if (HDStatusCheckFlag.Value == "Completed")
        {
            LinkBtnSavePopup.Visible = false;
        }

    }

    private void getStatus(string Str)
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[1];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getAllStatus";

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");

            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                if (Str == "UAT")
                {
                    AllStatus.DataSource = DS.Tables[5];
                }
                if (Str == "GOLive")
                {
                    AllStatus.DataSource = DS.Tables[6];
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

        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "Get_UATGOLiveDetailsByProjectLocation";

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
        txtPRM.Text = dsABAPObjectPlanSubmitted.Tables[2].Rows.Count == 1 ? dsABAPObjectPlanSubmitted.Tables[2].Rows[0]["ProgramManager"].ToString().Trim() : "";

    }

    public void DDLProjectLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvRGSDetails.Visible = false;
        gvFSDetails.Visible = false;
        liProjectMangaer.Visible = true;
        HDProjectLocation.Value = DDLProjectLocation.SelectedValue;
        get_ABAP_Object_Submitted_Plan_FSDetails();
    }

    #endregion

    protected void LinkBtnSavePopup_Click(object sender, EventArgs e)
    {
        lblmessagesub.Text = "";
        var FirstDate = "";
        var SecondDate = "";
        var ThirdDate = "";
        var FourthDate = "";
        var plannedDate = "";

        var FithDateCTMTraining = "";
        var SixthDateCTMTraining = "";

        var splitPlannedDate = Txt_PlannedPreparationStart.Text.Split('/');
        plannedDate = splitPlannedDate[2] + "-" + splitPlannedDate[1] + "-" + splitPlannedDate[0];
        DateTime PlannedDatedt = DateTime.Parse(plannedDate);




        //if (HDStatus.Value == "FS" || HDStatus.Value == "ABAP" || HDStatus.Value == "HBTTest" || HDStatus.Value == "CTM")
        //{


        //}


        //DivIDPopup.Visible = true;
        //PnlIrSheet.Visible = true;
        //this.ModalPopupExtenderIRSheet.Show();
        //lblmessagesub.Text = "Select Revised / Actual Submission Date";
        //return;
        


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

        //if (Txt_RevisedActualPreparationStart.Text != "")
        //{
        //    var splitDate = Txt_RevisedActualPreparationStart.Text.Split('/');
        //    FirstDate = splitDate[2] + "-" + splitDate[1] + "-" + splitDate[0];
        //}
        //if (Txt_RevisedActualPreparationStart.Text != "")
        //{
        //    var splitDate = Txt_RevisedActualPreparationStart.Text.Split('/');
        //    FirstDate = splitDate[2] + "-" + splitDate[1] + "-" + splitDate[0];
        //}

        



        //if (!string.IsNullOrWhiteSpace(Txt_RevisedActualPreparationStart.Text) && !string.IsNullOrWhiteSpace(txt_RevisedActualPreparationFinish.Text))
        //{
        //    DateTime startDate = DateTime.Parse(FirstDate);
        //    DateTime finishDate = DateTime.Parse(SecondDate);
        //    if (startDate < PlannedDatedt || finishDate < PlannedDatedt)
        //    {
        //        DivIDPopup.Visible = true;
        //        PnlIrSheet.Visible = true;
        //        this.ModalPopupExtenderIRSheet.Show();
        //        lblmessagesub.Text = "Revised Start and Finish Date must be greater than Revised Planned Date";
        //        return;
        //    }

        //    else if (startDate > finishDate)
        //    {
        //        DivIDPopup.Visible = true;
        //        PnlIrSheet.Visible = true;
        //        this.ModalPopupExtenderIRSheet.Show();
        //        lblmessagesub.Text = "Revised Start Date must be earlier than Revised Finish Date";
        //        return;
        //    }
        //}

        SqlParameter[] spars = new SqlParameter[13];
        bool flagcheck = true;
       
        if (flagcheck == true)
        {

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "UpdateTable_RGS_FS_ABAP_HBTTest";
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
            spars[10] = new SqlParameter("@StartTrainingDate", SqlDbType.VarChar);

            if (FithDateCTMTraining == "")
            {
                spars[10].Value = DBNull.Value;
            }
            else
            {
                spars[10].Value = FithDateCTMTraining;
            }

            spars[11] = new SqlParameter("@FinishTrainingDate", SqlDbType.VarChar);

            if (SixthDateCTMTraining == "")
            {
                spars[11].Value = DBNull.Value;
            }
            else
            {
                spars[11].Value = SixthDateCTMTraining;
            }

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
            DivIDPopup.Visible = false;
            get_ABAP_Object_Submitted_Plan_FSDetails();

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
    }
}