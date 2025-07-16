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

public partial class ABAP_Object_Tracker_Change_Status_ABAP_Accept_NotAccept : System.Web.UI.Page
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
                    getReason();
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
                spnlabel.Visible = true;
                DDLProjectLocation.DataSource = DS.Tables[0];
                DDLProjectLocation.DataTextField = "Location_name";
                DDLProjectLocation.DataValueField = "comp_code";
                DDLProjectLocation.DataBind();
                DDLProjectLocation.Items.Insert(0, new ListItem("Select Project Location", "0"));

                if (DS.Tables[0].Rows.Count == 1)
                {

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

    private void getReason()
    {
        try
        {
            var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();

            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetReasonForFS";

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                DDLReason.Visible = true;
                spnreason.Visible = true;
                Span4.Visible = true;
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
                Span4.Visible = false;
                DDLReason.Items.Insert(0, new ListItem("Select the reason", "0"));
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

        DDLReason.Visible = false;
        spnreason.Visible = false;
        Span4.Visible = false;


        this.ModalPopupExtenderIRSheet.Show();
    }

    public void get_ABAP_Detail()
    {
        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetABAP_DetailBYID";

        spars[1] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();

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
                    if (item.Value == "2" || item.Value == "10" || item.Value == "5" || item.Value == "4")
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

                //if (!string.IsNullOrEmpty(Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPFunctionalStatus"])))   Commented on 21-05-2025 for showing default value of status.
                //{
                //    AllStatus.SelectedValue = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPFunctionalStatus"]);

                //}
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

            Txt_PlannedPreparationStart.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FSPlannedStart"]);
            txt_PlannedPreparationFinish.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FSPlannedFinish"]);


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
            spnfsdocs.Visible = false;

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
            //if (!string.IsNullOrEmpty(files))
            //{
            //    string[] separateFiles = files.Split(',');
            //    DataSet objdd = new DataSet();

            //    DataTable table = new DataTable("MyTable");
            //    objdd.Tables.Add(table);

            //    table.Columns.Add("Srno", typeof(string));
            //    table.Columns.Add("FileName", typeof(string));

            //    for (int i = 0; i < separateFiles.Length; i++)
            //    {
            //        DataRow row = table.NewRow();
            //        row["Srno"] = "V" + (i + 1).ToString();
            //        row["FileName"] = separateFiles[i];
            //        table.Rows.Add(row);
            //    }

            //    gvuploadedFiles.DataSource = null;
            //    gvuploadedFiles.DataBind();
            //    gvuploadedFiles.DataSource = objdd.Tables[0];
            //    gvuploadedFiles.DataBind();
            //}
            //else
            //{
            //    gvuploadedFiles.DataSource = null;
            //    gvuploadedFiles.DataBind();
            //}
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


        liStart.Visible = false;
        lifinish.Visible = false;
        liSubmitssion.Visible = false;
        liApproval.Visible = false;
        liEmpty1.Visible = false;
        liEmpty2.Visible = false;

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
            spars[0].Value = "GetDetailsByProjectLocation_ABAPDevAcceptNotAccept";

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
                lblmessage.Text = "No Record Found.";
                gvABAPDevDetails.DataSource = null;
                gvABAPDevDetails.DataBind();
                gvABAPDevDetails.Visible = false;
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

        if (AllStatus.SelectedValue == "0")
        {
            DivIDPopup.Visible = true;
            PnlIrSheet.Visible = true;
            this.ModalPopupExtenderIRSheet.Show();
            lblmessagesub.Text = "Please select the status.";
            return;
        }

        if (DDLReason.SelectedValue == "0" && AllStatus.SelectedValue == "21")
        {
            DivIDPopup.Visible = true;
            PnlIrSheet.Visible = true;
            this.ModalPopupExtenderIRSheet.Show();
            lblmessagesub.Text = "Please select the reason.";
            return;
        }

        SqlParameter[] spars = new SqlParameter[7];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "UpdateTable_RGS_FS_ABAP_HBTTest_CTMTest";

        spars[1] = new SqlParameter("@ID", SqlDbType.Int);
        spars[1].Value = Convert.ToInt32(HDID.Value);

        spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[2].Value = Session["Empcode"].ToString();

        spars[3] = new SqlParameter("@AllStatusID", SqlDbType.VarChar);
        spars[3].Value = AllStatus.SelectedValue;

        spars[4] = new SqlParameter("@Statustypecheck", SqlDbType.VarChar);
        spars[4].Value = HDStatus.Value;

        spars[5] = new SqlParameter("@Remark", SqlDbType.VarChar);
        spars[5].Value = Txt_Remarks.Text.Trim();

        spars[6] = new SqlParameter("@ReasonId", SqlDbType.VarChar);
        spars[6].Value = DDLReason.SelectedValue == "0" ? "" : DDLReason.SelectedValue.Trim();

        DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (DS != null)
        {
            if (DS.Tables.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["ResponseStatusVal"].ToString() == "20")
                {
                    #region Send Email 
                    string strSubject = "";
                    strSubject = DDLProjectLocation.SelectedItem.Text.ToString().Trim() + " ABAP Object - " + Txt_DevelopmentDescription.Text;
                    string Email_TO = hdnFSConsultantEmail.Value.ToString().Trim();
                    string Email_CC = hdnprojectManagerEmail.Value.ToString().Trim();

                    StringBuilder strbuild = new StringBuilder();
                    strbuild.Append("<p style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'></p>");
                    strbuild.Append("<p> Dear " + hdnFSConsultantName.Value.ToString().Trim() + "</p>");
                    strbuild.Append("<p> This is to inform you that <b>" + hdnABAPperName.Value.ToString().Trim() + "</b> has accepted the Functional Specification (FS) for the object <b>" + Txt_DevelopmentDescription.Text + "</b>.</p>");
                    strbuild.Append("<table cellpadding='5' cellspacing='0' style='font-size:11pt;font-family:Arial;font-style:Regular;width:100%;'>");
                    strbuild.Append("<tr><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Object Name</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Module</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Interface</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Plan Start Date</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Plan End Date</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Revised Start Date</th>" +
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


                    spm.sendMail(Email_TO, strSubject, Convert.ToString(strbuild).Trim(), "", Email_CC);

                    #endregion
                }
                if (DS.Tables[0].Rows[0]["ResponseStatusVal"].ToString() == "21")
                {
                    #region Send Email 
                    string strSubject = "";
                    strSubject = DDLProjectLocation.SelectedItem.Text.ToString().Trim() + " ABAP Object - " + Txt_DevelopmentDescription.Text;
                    string Email_TO = hdnFSConsultantEmail.Value.ToString().Trim();
                    string Email_CC = hdnprojectManagerEmail.Value.ToString().Trim();

                    string strInvoiceURL = "";
                    strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["ABAPNotAccept_ABAPObjectPlan"]).Trim());
                    StringBuilder strbuild = new StringBuilder();
                    strbuild.Append("<p style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'></p>");
                    strbuild.Append("<p> Dear " + hdnFSConsultantName.Value.ToString().Trim() + "</p>");
                    strbuild.Append("<p> This is to inform you that <b>" + hdnABAPperName.Value.ToString().Trim() + "</b> has not accepted the Functional Specification (FS) for the object <b>" + Txt_DevelopmentDescription.Text + "</b>.</p>");
                    strbuild.Append("<table cellpadding='5' cellspacing='0' style='font-size:11pt;font-family:Arial;font-style:Regular;width:100%;'>");
                    strbuild.Append("<tr><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Object Name</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Module</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Interface</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Plan Start Date</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Plan End Date</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Revised Start Date</th>" +
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
            }
            PnlIrSheet.Visible = false;
            DivIDPopup.Visible = false;
            get_ABAP_Object_Submitted_Plan_FSDetails();

            //clear all fields
            ClearAllfeildsAfterSumit();

        }
        //}

    }

    public void ClearAllfeildsAfterSumit()
    {
        hdnABAPDetailsId.Value = "";
        Txt_DevelopmentDescription.Text = "";
        Txt_Module.Text = "";
        Txt_Interface.Text = "";
        Txt_FRICECategory.Text = "";
        Txt_Order.Text = "";
        Txt_Priority.Text = "";
        Txt_Complexity.Text = "";
        HDStatusCheck.Value = "";
        HDStatusCheckFlag.Value = "";
        Txt_PlannedPreparationStart.Text = "";
        txt_PlannedPreparationFinish.Text = "";
        Txt_RevisedActualPreparationStart.Text = "";
        txt_RevisedActualPreparationFinish.Text = "";
        Txt_RevisedActualSubmission.Text = "";
        txt_RevisedActualApproval.Text = "";
        HDDateCheck1.Value = "";
        HDDateCheck2.Value = "";
        HDDateCheck3.Value = "";
        HDDateCheck4.Value = "";
        Txt_Remarks.Text = "";
        HDRemarkcheck.Value = "";
        spnfsdocs.Visible = false;
        hdnABAPperEmail.Value = "";
        hdnABAPperName.Value = "";
        hdnFSConsultantEmail.Value = "";
        hdnFSConsultantName.Value = "";
        DDLReason.SelectedValue = "0";
        gvuploadedFiles.DataSource = null;
        gvuploadedFiles.DataBind();

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
        ClearAllfeildsAfterSumit();
    }

    //protected void LinkBtnABAPBackPopup_Click(object sender, EventArgs e)
    //{

    //    DivIDABAPTimesheetPopup.Visible = false;
    //    PnlABAPTimeSheet.Visible = false;
    //}

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
        if (selectedValue == "21")
        {
            Span4.Visible = true;
            spnreason.Visible = true;
            DDLReason.Visible = true;
        }
        else
        {
            Span4.Visible = false;
            spnreason.Visible = false;
            DDLReason.Visible = false;
        }

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
        Session["projectCode"] = HDProjectLocation.Value.ToString();
        Response.Redirect("~/procs/ABAP_Object_Tracker_Change_Status_ABAP_Timesheet.aspx");
    }

    //protected void btn_SumitTimesheet_Click(object sender, EventArgs e)
    //{
    //    PnlABAPTimeSheet.Visible = true;
    //    DivIDABAPTimesheetPopup.Visible = true;

    //    lblTimesheetErrorMsg.Text = "";
    //    var firstDate = DateTime.ParseExact(hdnStartDate.Value, "dd-MM-yyyy", null).ToString("yyyy-MM-dd");
    //    var secondDate = DateTime.ParseExact(hdnEndDate.Value, "dd-MM-yyyy", null).ToString("yyyy-MM-dd");


    //    DataSet dsABAPObjectPlanSubmitted = new DataSet();
    //    SqlParameter[] spars = new SqlParameter[5];

    //    spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
    //    spars[0].Value = "GetABAPersTimsheetDetails";

    //    spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
    //    spars[1].Value = (Session["Empcode"]).ToString().Trim();

    //    spars[2] = new SqlParameter("@TS_ABAPDetailsId", SqlDbType.VarChar);
    //    spars[2].Value = hdnCommaSeperABAPDetailsId.Value.Trim();

    //    spars[3] = new SqlParameter("@WeekStartDate", SqlDbType.VarChar);
    //    spars[3].Value = firstDate;

    //    spars[4] = new SqlParameter("@WeekEndDate", SqlDbType.VarChar);
    //    spars[4].Value = secondDate;

    //    dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");

    //    if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
    //    {
    //        gvTimesheet.DataSource = null;
    //        gvTimesheet.DataBind();
    //        gvTimesheet.Columns.Clear();
    //        for (int i = 0; i < dsABAPObjectPlanSubmitted.Tables[0].Columns.Count; i++)
    //        {
    //            DataColumn column = dsABAPObjectPlanSubmitted.Tables[0].Columns[i];

    //            if (column.DataType == typeof(DateTime))
    //            {
    //                TemplateField templateField = new TemplateField();
    //                templateField.HeaderText = column.ColumnName;
    //                templateField.ItemTemplate = new CustomTemplate(ListItemType.Item, column.ColumnName);
    //                templateField.EditItemTemplate = new CustomTemplate(ListItemType.EditItem, column.ColumnName);
    //                gvTimesheet.Columns.Add(templateField);
    //                if (i == dsABAPObjectPlanSubmitted.Tables[0].Columns.Count - 2 || i == dsABAPObjectPlanSubmitted.Tables[0].Columns.Count - 1)
    //                {
    //                    templateField.ItemStyle.BackColor = System.Drawing.Color.Gray;
    //                    templateField.HeaderStyle.BackColor = System.Drawing.Color.Gray;
    //                }
    //            }
    //            else
    //            {
    //                BoundField boundField = new BoundField();
    //                boundField.DataField = column.ColumnName;
    //                boundField.HeaderText = column.ColumnName;

    //                if (column.ColumnName == "SrNo")
    //                {
    //                    boundField.ItemStyle.Width = Unit.Percentage(5);
    //                    boundField.HeaderStyle.Width = Unit.Percentage(5);
    //                    gvTimesheet.Columns.Add(boundField);
    //                }
    //                else if (column.ColumnName == "ABAPDetailsId")
    //                {
    //                    boundField.ItemStyle.Width = Unit.Percentage(2);
    //                    boundField.HeaderStyle.Width = Unit.Percentage(2);
    //                    gvTimesheet.Columns.Add(boundField);
    //                }
    //                else if (column.ColumnName == "Development_Desc")
    //                {
    //                    boundField.ItemStyle.Width = Unit.Percentage(25);
    //                    boundField.HeaderStyle.Width = Unit.Percentage(25);
    //                    gvTimesheet.Columns.Add(boundField);
    //                }
    //                else
    //                {
    //                    gvTimesheet.Columns.Add(boundField);
    //                }
    //            }

    //        }



    //        //foreach (DataColumn column in dsABAPObjectPlanSubmitted.Tables[0].Columns)
    //        //{
    //        //    if (column.DataType == typeof(DateTime))
    //        //    {
    //        //        TemplateField templateField = new TemplateField();
    //        //        templateField.HeaderText = column.ColumnName;
    //        //        templateField.ItemTemplate = new CustomTemplate(ListItemType.Item, column.ColumnName);
    //        //        templateField.EditItemTemplate = new CustomTemplate(ListItemType.EditItem, column.ColumnName);
    //        //        gvTimesheet.Columns.Add(templateField);
    //        //    }
    //        //    else
    //        //    {
    //        //        BoundField boundField = new BoundField();
    //        //        boundField.DataField = column.ColumnName;
    //        //        boundField.HeaderText = column.ColumnName;

    //        //        if (column.ColumnName == "SrNo")
    //        //        {
    //        //            boundField.ItemStyle.Width = Unit.Percentage(5);
    //        //            boundField.HeaderStyle.Width = Unit.Percentage(5);
    //        //            gvTimesheet.Columns.Add(boundField);
    //        //        }
    //        //        else if (column.ColumnName == "ABAPDetailsId")
    //        //        {
    //        //            boundField.ItemStyle.Width = Unit.Percentage(2);
    //        //            boundField.HeaderStyle.Width = Unit.Percentage(2);
    //        //            gvTimesheet.Columns.Add(boundField);
    //        //        }
    //        //        else if (column.ColumnName == "Development_Desc")
    //        //        {
    //        //            boundField.ItemStyle.Width = Unit.Percentage(25);
    //        //            boundField.HeaderStyle.Width = Unit.Percentage(25);
    //        //            gvTimesheet.Columns.Add(boundField);
    //        //        }

    //        //        else
    //        //        {
    //        //            //if (column.ColumnName != "SrNo" && column.ColumnName != "ABAPDetailsId" && column.ColumnName != "Development_Desc")
    //        //            //{
    //        //            gvTimesheet.Columns.Add(boundField);
    //        //            //}
    //        //        }
    //        //    }
    //        //}

    //        gvTimesheet.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
    //        gvTimesheet.DataBind();

    //    }
    //    else
    //    {
    //        gvTimesheet.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
    //        gvTimesheet.DataBind();
    //    }


    //    this.ModalPopupExtenderABAPTimesheet.Show();

    //}

    //protected void btnSubmit_Click1(object sender, EventArgs e)
    //{
    //    foreach (GridViewRow row in gvTimesheet.Rows)
    //    {
    //        if (row.RowType == DataControlRowType.DataRow)
    //        {
    //            foreach (DataControlField column in gvTimesheet.Columns)
    //            {
    //                if (column is TemplateField)
    //                {
    //                    TextBox txtBox = (TextBox)row.FindControl("ID");

    //                    if (txtBox != null)
    //                    {
    //                        string textValue = txtBox.Text;
    //                    }
    //                }


    //                string objSrNo = HttpUtility.HtmlDecode(row.Cells[0].Text);
    //                string objABAPDetailsId = HttpUtility.HtmlDecode(row.Cells[1].Text);
    //                string objDevDesc = HttpUtility.HtmlDecode(row.Cells[2].Text);
    //                string objdayone = HttpUtility.HtmlDecode(row.Cells[3].Text);
    //                string objdaytwo = HttpUtility.HtmlDecode(row.Cells[4].Text);
    //                string objdaythree = HttpUtility.HtmlDecode(row.Cells[5].Text);
    //                string objdayfour = HttpUtility.HtmlDecode(row.Cells[6].Text);
    //                string objdayfive = HttpUtility.HtmlDecode(row.Cells[7].Text);
    //                string objdaysix = HttpUtility.HtmlDecode(row.Cells[8].Text);
    //                string objdayseven = HttpUtility.HtmlDecode(row.Cells[9].Text);

    //                bool hasDataForDays = row.Cells.Cast<TableCell>().Skip(3).Take(7).Any(cell => !string.IsNullOrWhiteSpace(cell.Text));
    //                if (hasDataForDays)
    //                {
    //                    bool allDaysValid = true;
    //                    for (int i = 3; i <= 9; i++)
    //                    {
    //                        string dayValue = HttpUtility.HtmlDecode(row.Cells[i].Text.Trim());
    //                        if (!string.IsNullOrWhiteSpace(dayValue))
    //                        {
    //                            TimeSpan timeValue;
    //                            if (!TimeSpan.TryParseExact(dayValue, "hh\\:mm", CultureInfo.InvariantCulture, out timeValue))
    //                            {
    //                                allDaysValid = false;
    //                                lblTimesheetErrorMsg.Text = "Invalid time format in Day {i - 2}. Please enter time in the format HH:mm.";
    //                                DivIDABAPTimesheetPopup.Visible = true;
    //                                this.ModalPopupExtenderABAPTimesheet.Show();
    //                                break;
    //                            }

    //                            if (timeValue.Hours >= 24 || timeValue.Minutes >= 60)
    //                            {
    //                                allDaysValid = false;
    //                                lblTimesheetErrorMsg.Text = "Invalid time in Day { i - 2  }. Please enter a valid time (less than 24 hours and less than 60 minutes).";
    //                                DivIDABAPTimesheetPopup.Visible = true;
    //                                this.ModalPopupExtenderABAPTimesheet.Show();
    //                                break;
    //                            }

    //                            if (timeValue.Hours == 0 && timeValue.Minutes == 0)
    //                            {
    //                                allDaysValid = false;
    //                                lblTimesheetErrorMsg.Text = "Invalid time in Day {i - 2}. Please enter a valid time (not 0:00).";
    //                                DivIDABAPTimesheetPopup.Visible = true;
    //                                this.ModalPopupExtenderABAPTimesheet.Show();
    //                                break;
    //                            }
    //                        }
    //                    }

    //                    if (allDaysValid)
    //                    {
    //                        var emp_code = Session["Empcode"].ToString();   //abaper empcode
    //                        var Timesheet_date = DateTime.Now.ToString("yyyy-MM-dd");  //abaper timesheet date
    //                        string projectCode = HDProjectLocation.Value.ToString();   //abaper project location
    //                        int taskid = 0;  //abaper development desc
    //                        string hour = "";  //abaper timesheet hour
    //                        var status = "Created";  //abaper timesheet status
    //                        var Description = "";

    //                        var qtype = "INSERTTIMESHEET";
    //                        spm.InsertTimeSheet(qtype, 0, emp_code, Timesheet_date, projectCode, taskid, hour, status, Description);
    //                        lblmessage.Text = "You have add timesheet successfully ";
    //                    }
    //                }
    //                else
    //                {
    //                    lblTimesheetErrorMsg.Text = "Please provide data for all days.";
    //                }


    //            }
    //        }


    //    }




    //}

    #endregion

}