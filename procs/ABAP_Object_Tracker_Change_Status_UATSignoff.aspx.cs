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

public partial class ABAP_Object_Tracker_Change_Status_UATSignoff : System.Web.UI.Page
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
                    get_UATSignOffDetails();

                }

            }

        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    #endregion

    #region uatsingoffdata
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
        HDID.Value = Convert.ToString(gvUATSingOffDetails.DataKeys[row.RowIndex].Values[0]).Trim();
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
        HDStatusCheck.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["UAT_Status"]);
        Txt_PlannedDate.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ABAPPlannedStart"]);
        Txt_Final_Script_Sub_Date.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Final_Script_Sub_Date"]);
        txt_RevisedDate.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Revised_Date"]);
        Txt_ActualDate.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Actual_Date"]);
        HDDateCheck1.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Final_Script_Sub_Date"]);
        HDDateCheck2.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Revised_Date"]);
        HDDateCheck3.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Actual_Date"]);
        Txt_Remarks.Text = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);
        HDRemarkcheck.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"]);

        //string stropennotopen = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Checkflag"]);
        //if (stropennotopen == "open")
        //{
        //    if (HDStatusCheckFlag.Value == "Test Case Approved" && !string.IsNullOrEmpty(HDDateCheck4.Value))
        //    {
        //        LinkBtnSavePopup.Visible = false;
        //    }
        //    else
        //    {
        //        LinkBtnSavePopup.Visible = true;
        //    }
        //}
        //else
        //{
        //    lblmessagesub.Text = "This object cannot be updated because the previous stage has not yet been marked as Passed.";
        //    LinkBtnSavePopup.Visible = false;
        //}
    }



    public void get_UATSignOffDetails()
    {

        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "Get_UATSignOffDetailsByProjectLocation";

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

        if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
        {
            rgs.Visible = true;
            gvUATSingOffDetails.Visible = true;

            gvUATSingOffDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvUATSingOffDetails.DataBind();
        }
        else
        {
            lblmessage.Text = "No Records Found.";
        }

        txtPRM.Text = dsABAPObjectPlanSubmitted.Tables[1].Rows.Count == 1 ? dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ProjectMangaer"].ToString().Trim() : "";
    }

    public void DDLProjectLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvUATSingOffDetails.Visible = false;
        liProjectMangaer.Visible = true;
        HDProjectLocation.Value = DDLProjectLocation.SelectedValue;
        get_UATSignOffDetails();
    }

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
        var FinalSubmitDate = "";

        DateTime PlannedDatedt = new DateTime();
        DateTime RevisedDatedt = new DateTime();
        DateTime ActualDatedt = new DateTime();
        DateTime FinalSubmitdt = new DateTime();

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

        if (!string.IsNullOrEmpty(Txt_Final_Script_Sub_Date.Text))
        {
            var splitPlannedApproval = Txt_Final_Script_Sub_Date.Text.Split('/');
            FinalSubmitDate = splitPlannedApproval[2] + "-" + splitPlannedApproval[1] + "-" + splitPlannedApproval[0];
            FinalSubmitdt = DateTime.Parse(FinalSubmitDate);
        }
    

        SqlParameter[] spars = new SqlParameter[7];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "UpdateTable_UATSingOff";

        spars[1] = new SqlParameter("@ID", SqlDbType.Int);
        spars[1].Value = Convert.ToInt32(HDID.Value);

        spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[2].Value = Session["Empcode"].ToString();

        spars[3] = new SqlParameter("@RevisedStart", SqlDbType.VarChar);
        spars[3].Value = RevisedDate;

        spars[4] = new SqlParameter("@ActualStart", SqlDbType.VarChar);
        if (ActualDate == "")
        {
            spars[4].Value = DBNull.Value;
        }
        else
        {
            spars[4].Value = ActualDate;
        }

        spars[5] = new SqlParameter("@FinalSubmitDate", SqlDbType.VarChar);
        if (FinalSubmitDate == "")
        {
            spars[5].Value = DBNull.Value;
        }
        else
        {
            spars[5].Value = FinalSubmitDate;
        }

        spars[6] = new SqlParameter("@Remark", SqlDbType.VarChar);
        spars[6].Value = Txt_Remarks.Text.Trim();
        
        DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        DivIDPopup.Visible = false;
        get_UATSignOffDetails();


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

    #endregion

}