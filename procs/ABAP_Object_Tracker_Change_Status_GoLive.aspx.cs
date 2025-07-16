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

public partial class ABAP_Object_Tracker_Change_Status_GoLive : System.Web.UI.Page
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

            Txt_PlannedDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            txt_RevisedDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            Txt_ActualDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            Txt_Final_Script_Sub_Date.Attributes.Add("onkeypress", "return noanyCharecters(event);");

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
                if (DS.Tables[0].Rows.Count == 1)
                {
                    golive.Visible = false;
                    gvGoliveDetails.Visible = false;
                    liProjectMangaer.Visible = true;
                    HDProjectLocation.Value = DS.Tables[0].Rows[0]["comp_code"].ToString();
                    get_ABAP_Object_Submitted_Plan_FSDetails();
                }
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


    protected void lnkEdit_Click1(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        HDID.Value = Convert.ToString(gvGoliveDetails.DataKeys[row.RowIndex].Values[0]).Trim();
        HDStatus.Value = "GOLive";
        lbl5.Text = "GO Live Status";
        getStatus("GOLive");

        Label1.Text = "Go-Live Planned Date";
        label2.Text = "Go-Live Revised Date";
        Label3.Text = "Go-Live Actual Date";
        UATDisplay1.Visible = false;
        UATDisplay2.Visible = false;
        UATDisplay3.Visible = false;
        get_GOLive_Detail();
        lblmessagesub.Text = "";
        DivIDPopup.Visible = true;

        divchkShowFields.Visible = false;
        chkShowFields.Checked = false;
        divReusableClientName.Visible = false;
        divReusableRemark.Visible = false;
        divReusableAdditonalEffrot.Visible = false;

        divchkShowFields.Visible = false;

        if (AllStatus.SelectedValue == "24")
        {
            AllStatus.Enabled = false;
            LinkBtnSavePopup.Visible = false;
        }
        else
        {
            AllStatus.Enabled = true;
            LinkBtnSavePopup.Visible = true;
        }

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
        Txt_PlannedDate.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPPlannedStart"]);
        Txt_Final_Script_Sub_Date.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Final_Script_Sub_Date"]);
        txt_RevisedDate.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Revised_Date"]);
        Txt_ActualDate.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Actual_Date"]);

        HDDateCheck1.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Final_Script_Sub_Date"]);
        hdnRevisedDate.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Revised_Date"]);
        HdnActualDate.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Actual_Date"]);

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

        Txt_PlannedDate.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PlannedDate"]);
        txt_RevisedDate.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RevisedDate"]);
        Txt_ActualDate.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ActualDate"]);

        HDDateCheck1.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PlannedDate"]);
        hdnRevisedDate.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["RevisedDate"]);
        HdnActualDate.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ActualDate"]);

        Txt_Remarks.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);
        HDRemarkcheck.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);

        if (HDStatusCheckFlag.Value == "Completed")
        {
            LinkBtnSavePopup.Visible = false;
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

        if (AllStatus.SelectedValue == "24")
        {
            LinkBtnSavePopup.Visible = false;
            txt_RevisedDate.Enabled = false;
            Txt_ActualDate.Enabled = true;
        }
        if (AllStatus.SelectedValue == "2")
        {
            txt_RevisedDate.Enabled = true;
            Txt_ActualDate.Enabled = false;
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
        spars[0].Value = "Get_GOLiveDetailsByProjectLocation";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = (Session["Empcode"]).ToString().Trim();

        if (HDProjectLocation.Value != "")
        {
            DDLProjectLocation.SelectedValue = HDProjectLocation.Value;
        }

        spars[2] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
        spars[2].Value = DDLProjectLocation.SelectedValue == "0" ? "" : DDLProjectLocation.SelectedValue;

        dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        golive.Visible = false;

        if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
        {
            liProjectMangaer.Visible = false;
            golive.Visible = true;
            gvGoliveDetails.Visible = true;
            gvGoliveDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvGoliveDetails.DataBind();


            if (dsABAPObjectPlanSubmitted.Tables[0].Rows.Count == 0 && dsABAPObjectPlanSubmitted.Tables[1].Rows.Count == 0)
            {
                lblmessage.Text = "No Records Found.";
            }
            txtPRM.Text = dsABAPObjectPlanSubmitted.Tables[1].Rows.Count == 1 ? dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ProjectManager"].ToString().Trim() : "";
            hdnprojectManager.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ProjectManager"].ToString().Trim();
            hdnprojectManagerEmail.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["projectManagerEmail"].ToString().Trim();
            hdnprogramnager.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["programnager"].ToString().Trim();
            hdnprogramnagerEmail.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["programnagerEmail"].ToString().Trim();
            hdnDeliveryHeadMail.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["DeliveryHeadEMail"].ToString().Trim();

        }
        else
        {

            lblmessage.Text = "No Records Found.";

            liProjectMangaer.Visible = false;
        }
    }

    public void DDLProjectLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvGoliveDetails.Visible = false;
        liProjectMangaer.Visible = true;
        HDProjectLocation.Value = DDLProjectLocation.SelectedValue;
        get_ABAP_Object_Submitted_Plan_FSDetails();
    }

    #endregion

    protected void LinkBtnSavePopup_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }
        lblmessagesub.Text = "";
        var PlannedDate = "";
        var RevisedDate = "";
        var ActualDate = "";

        DateTime PlannedDatedt = new DateTime();
        DateTime RevisedDatedt = new DateTime();
        DateTime ActualDatedt = new DateTime();

        if (!string.IsNullOrEmpty(Txt_PlannedDate.Text))
        {
            var splitPlannedSubmission = Txt_PlannedDate.Text.Split('/');
            PlannedDate = splitPlannedSubmission[2] + "-" + splitPlannedSubmission[1] + "-" + splitPlannedSubmission[0];
            PlannedDatedt = DateTime.Parse(PlannedDate);
        }

        if (!string.IsNullOrEmpty(txt_RevisedDate.Text))
        {
            var splitPlannedSubmission = txt_RevisedDate.Text.Split('/');
            RevisedDate = splitPlannedSubmission[2] + "-" + splitPlannedSubmission[1] + "-" + splitPlannedSubmission[0];
            RevisedDatedt = DateTime.Parse(RevisedDate);
        }

        if (!string.IsNullOrEmpty(Txt_ActualDate.Text))
        {
            var splitPlannedApproval = Txt_ActualDate.Text.Split('/');
            ActualDate = splitPlannedApproval[2] + "-" + splitPlannedApproval[1] + "-" + splitPlannedApproval[0];
            ActualDatedt = DateTime.Parse(ActualDate);
        }

        if (AllStatus.SelectedValue.ToString() == "8")
        {
            if (string.IsNullOrWhiteSpace(Txt_ActualDate.Text))
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "You cannot set the status to Completed if the Actual Start Date has not been entered.";
                return;
            }

        }

        if (!string.IsNullOrWhiteSpace(Txt_ActualDate.Text))
        {
            if (ActualDatedt > DateTime.Now.Date)
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Actual Date must not be future Date.";
                return;
            }
           
        }


        if (chkShowFields.Checked)
        {
            if (string.IsNullOrEmpty(txtReusableClientName.Text))
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Please enter the Reusable Client Name.";
                return;
            }

            if (string.IsNullOrEmpty(txtReusableRemark.Text))
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Please enter the Reusable Reusable Remark."; return;
            }

            if (string.IsNullOrEmpty(txtReusableAdditonalEffrot.Text))
            {
                DivIDPopup.Visible = true;
                PnlIrSheet.Visible = true;
                this.ModalPopupExtenderIRSheet.Show();
                lblmessagesub.Text = "Please enter the Reusable Additional Efforts.";
                return;
            }
        }

        bool isChecked = false;
        if (chkShowFields.Checked)
        {
            isChecked = true;
        }
        else
        {
            isChecked = false;
        }

        SqlParameter[] spars = new SqlParameter[9];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "UpdateTable_GoLIve";

        spars[1] = new SqlParameter("@ID", SqlDbType.Int);
        spars[1].Value = Convert.ToInt32(HDID.Value);

        spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[2].Value = Session["Empcode"].ToString();

        spars[3] = new SqlParameter("@RevisedStart", SqlDbType.VarChar);
        if (RevisedDate == "")
        {
            spars[3].Value = DBNull.Value;
        }
        else
        {
            spars[3].Value = RevisedDate;
        }

        spars[4] = new SqlParameter("@ActualStart", SqlDbType.VarChar);
        if (ActualDate == "")
        {
            spars[4].Value = DBNull.Value;
        }
        else
        {
            spars[4].Value = ActualDate;
        }
        spars[5] = new SqlParameter("@AllStatusID", SqlDbType.VarChar);
        spars[5].Value = AllStatus.SelectedValue;

        spars[6] = new SqlParameter("@isReuseChecked", SqlDbType.VarChar);
        spars[6].Value = isChecked;

        spars[7] = new SqlParameter("@ReuseClientName", SqlDbType.VarChar);
        spars[7].Value = txtReusableClientName.Text.Trim();

        spars[7] = new SqlParameter("@ReuseRemark", SqlDbType.VarChar);
        spars[7].Value = txtReusableRemark.Text.Trim();

        spars[8] = new SqlParameter("@ReuseAdditonalEffrot", SqlDbType.VarChar);
        spars[8].Value = txtReusableAdditonalEffrot.Text.Trim();


        DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (DS != null)
        {
            if (DS.Tables[0].Rows[0]["ResponseStatusVal"].ToString() == "8")
            {
                #region Send Email to CTM User 
                string strSubject = "";
                strSubject = DDLProjectLocation.SelectedItem.Text.ToString().Trim() + " ABAP Object " + Txt_DevelopmentDescription.Text.ToString().Trim();
                string Email_TO = hdnFSConsultantEmail.Value.ToString().Trim() + "; " + hdnABAPperEmail.Value.ToString().Trim() + "; " + hdnHBTConsultantEmail.Value.ToString().Trim() + "; " + hdnCTMConsultantEmail.Value.ToString().Trim();
                string Email_CC = hdnprogramnagerEmail.Value.ToString().Trim() + "; " + hdnDeliveryHeadMail.Value.ToString().Trim();

                StringBuilder strbuild = new StringBuilder();
                strbuild.Append("<p style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'></p>");
                strbuild.Append("<p>" + "Dear " + hdnCTMConsultantName.Value.ToString().Trim() + "</p>");
                strbuild.Append("<p> This is to inform you that Object <b>" + Txt_DevelopmentDescription.Text.ToString().Trim() + "<b> has been Go-Live.</p>");
                strbuild.Append("<table cellpadding='5' cellspacing='0' style='font-size:11pt;font-family:Arial;font-style:Regular;width:100%;'>");
                strbuild.Append("<tr><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Object Name</th> <th style='background-color: #C7D3D4;border: 1px solid #ccc'>Module</th>" +
                                     "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Interface</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Plan Date</th>" +
                                     "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Revised Date</th>" +
                                     "</tr>");
                strbuild.Append("<tr><td style='width:38%;border: 1px solid #ccc'>" + Convert.ToString(Txt_DevelopmentDescription.Text).Trim() + " </td>");
                strbuild.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(Txt_Module.Text).Trim() + "</td>");
                strbuild.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(Txt_Interface.Text).Trim() + "</td>");
                strbuild.Append("<td style='width:8%;border: 1px solid #ccc'>" + Convert.ToString(Txt_PlannedDate.Text).Trim() + "</td>");
                strbuild.Append("<td style='width:8%;border: 1px solid #ccc'>" + Convert.ToString(txt_RevisedDate.Text).Trim() + "</td>");
                strbuild.Append("</table>");
                strbuild.Append("<br><br>This is an auto generated email, please do not reply!");

                spm.sendMail(Email_TO, strSubject, Convert.ToString(strbuild).Trim(), "", Email_CC);

                #endregion
            }

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

    protected void AllStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (AllStatus.SelectedValue == "24")
        {
            if(Convert.ToString(hdnRevisedDate.Value).Trim() == "")
            { 
                txt_RevisedDate.Text = "";
            }

            txt_RevisedDate.Enabled = false;
            Txt_ActualDate.Enabled = true;
        }
        if (AllStatus.SelectedValue == "2")
        {
            Txt_ActualDate.Text = "";
            txt_RevisedDate.Enabled = true;
            Txt_ActualDate.Enabled = false;
        }

        //if (AllStatus.SelectedValue == "8")
        //{
        //    divchkShowFields.Visible = true;
        //}
        //else
        //{
        //    divchkShowFields.Visible = false;
        //    chkShowFields.Checked = false;
        //    divReusableClientName.Visible = false;
        //    divReusableRemark.Visible = false;
        //    divReusableAdditonalEffrot.Visible = false;
        //}
        DivIDPopup.Visible = true;
        this.ModalPopupExtenderIRSheet.Show();

    }
    protected void chkShowFields_CheckedChanged(object sender, EventArgs e)
    {
        if (chkShowFields.Checked)
        {
            divReusableClientName.Visible = true;
            divReusableRemark.Visible = true;
            divReusableAdditonalEffrot.Visible = true;
            DivIDPopup.Visible = true;
            this.ModalPopupExtenderIRSheet.Show();
        }
        else
        {
            divReusableClientName.Visible = false;
            divReusableRemark.Visible = false;
            divReusableAdditonalEffrot.Visible = false;
            DivIDPopup.Visible = true;
            this.ModalPopupExtenderIRSheet.Show();
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        gvGoliveDetails.Columns[0].Visible = false;

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=GoLiveDetailPlanExport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                // Hide buttons or controls not to export
                gvGoliveDetails.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        gvGoliveDetails.Columns[0].Visible = true;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms the GridView was rendered
    }

}