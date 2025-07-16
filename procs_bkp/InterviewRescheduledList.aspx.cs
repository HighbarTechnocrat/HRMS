using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Xml;
using System.Text;
using System.IO;
using System.Data.Common;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;

public partial class procs_InterviewRescheduledList : System.Web.UI.Page
{
    #region Creative_Default_methods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";

    public int did = 0;

    SP_Methods spm = new SP_Methods();
    string strempcode = "";
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void lnkcont_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/Reembursementindex");
    }

    #endregion

    #region PageEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                // Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                   // GetRecruiterNumber();
                    GetSkillsetName();
                    GetCompany_Location();
                    GetDepartmentMaster();
                    GetlstPositionBand();
                    editform.Visible = true;
                    GetReq_InterviewerrescheduledList();

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    #endregion

    #region PageMethod

    public void GetSkillsetName()
    {
        DataTable dtSkillset = new DataTable();
        dtSkillset = spm.GetRecruitment_SkillsetName();
        if (dtSkillset.Rows.Count > 0)
        {
            lstSkillSet.DataSource = dtSkillset;
            lstSkillSet.DataTextField = "ModuleDesc";
            lstSkillSet.DataValueField = "ModuleId";
            lstSkillSet.DataBind();
            lstSkillSet.Items.Insert(0, new ListItem("Select Skillset", "0"));

        }
    }
    public void GetCompany_Location()
    {
        DataTable lstPosition = new DataTable();
        lstPosition = spm.GetRecruitment_Req_company_Location();
        if (lstPosition.Rows.Count > 0)
        {
            LstLocation.DataSource = lstPosition;
            LstLocation.DataTextField = "Location_name";
            LstLocation.DataValueField = "comp_code";
            LstLocation.DataBind();
            LstLocation.Items.Insert(0, new ListItem("Select Location", "0"));

        }
    }
    
    public void GetDepartmentMaster()
    {
        DataTable dtPositionDept = new DataTable();
        dtPositionDept = spm.GetRecruitment_Req_DepartmentMaster();
        if (dtPositionDept.Rows.Count > 0)
        {
            LstDepartment.DataSource = dtPositionDept;
            LstDepartment.DataTextField = "Department_Name";
            LstDepartment.DataValueField = "Department_id";
            LstDepartment.DataBind();
            LstDepartment.Items.Insert(0, new ListItem("Select Department", "0"));

        }
    }
    public void GetlstPositionBand()
    {
        DataTable dtPositionBand = new DataTable();
        dtPositionBand = spm.GetRecruitment_Req_HRMS_BAND_MASTER();
        if (dtPositionBand.Rows.Count > 0)
        {
            Lstband.DataSource = dtPositionBand;
            Lstband.DataTextField = "BAND";
            Lstband.DataValueField = "BAND";
            Lstband.DataBind();
            Lstband.Items.Insert(0, new ListItem("Select BAND", "0"));
        }
    }

    private void GetReq_InterviewerrescheduledList()
    {
        try
        {

            DataSet InterviewRescheduledlist = new DataSet();
            SqlParameter[] spars = new SqlParameter[9];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "InterviewerRescheduledList";

            spars[1] = new SqlParameter("@strInterviewerempcode", SqlDbType.VarChar);
            spars[1].Value = Session["Empcode"].ToString();
            
            InterviewRescheduledlist = spm.getDatasetList(spars, "SP_Rec_Interviewer_ReScheduled");

            if (InterviewRescheduledlist.Tables[0].Rows.Count > 0)
            {
                lstRequisitionNo.DataSource = InterviewRescheduledlist.Tables[0];
                lstRequisitionNo.DataTextField = "RequisitionNumber";
                lstRequisitionNo.DataValueField = "Recruitment_ReqID";
                lstRequisitionNo.DataBind();
                lstRequisitionNo.Items.Insert(0, new ListItem("Select Requisition No", "0"));
            }
            if (InterviewRescheduledlist.Tables[0].Rows.Count > 0)
            {
                GVInterviewRescheduledList.DataSource = InterviewRescheduledlist.Tables[0];
                GVInterviewRescheduledList.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    #endregion

    private void SearchMethod()
    {
        try
        {
            int SkillSetID = 0, Recruitment_ReqID = 0, DeptID = 0;
            SkillSetID = Convert.ToString(lstSkillSet.SelectedValue).Trim() != "" ? Convert.ToInt32(lstSkillSet.SelectedValue) : 0;
            Recruitment_ReqID = Convert.ToString(lstRequisitionNo.SelectedValue).Trim() != "" ? Convert.ToInt32(lstRequisitionNo.SelectedValue) : 0;
            DeptID = Convert.ToString(LstDepartment.SelectedValue).Trim() != "" ? Convert.ToInt32(LstDepartment.SelectedValue) : 0;

            SqlParameter[] spars = new SqlParameter[8];
            DataSet DS = new DataSet();
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "InterviewerRescheduledListSearch";
            spars[1] = new SqlParameter("@emp_Code", SqlDbType.VarChar);
            spars[1].Value = Session["Empcode"].ToString();
            spars[2] = new SqlParameter("@SkillSetID", SqlDbType.Int);
            spars[2].Value = SkillSetID;
            spars[3] = new SqlParameter("@LocationID", SqlDbType.VarChar);
            spars[3].Value = LstLocation.SelectedValue;
            spars[4] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
            spars[4].Value = Recruitment_ReqID;
            spars[5] = new SqlParameter("@BandID", SqlDbType.VarChar);
            spars[5].Value = Lstband.SelectedValue;
            spars[6] = new SqlParameter("@DeptID", SqlDbType.Int);
            spars[6].Value = DeptID;
            DS = spm.getDatasetList(spars, "SP_Get_Rec_RequiterChangeIndex_Search");

            if (DS.Tables[0].Rows.Count > 0)
            {
                GVInterviewRescheduledList.DataSource = DS.Tables[0];
                GVInterviewRescheduledList.DataBind();
                lblmessagesearch.Text = "";
            }
            else
            {
                lblmessagesearch.Text = "Requisition Record's not found";
                GVInterviewRescheduledList.DataSource = null;
                GVInterviewRescheduledList.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        SearchMethod();

    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        //lstRequisitionNo.SelectedValue = "0";
        lstSkillSet.SelectedValue = "0";
        LstLocation.SelectedValue = "0";
        Lstband.SelectedValue = "0";
        LstDepartment.SelectedValue = "0";
        lblmessagesearch.Text = "";
        GetReq_InterviewerrescheduledList();
    }

    protected void lnkEditFeedBack_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;

        HFRecruitment_ReqID.Value = Convert.ToString(GVInterviewRescheduledList.DataKeys[row.RowIndex].Values[0]).Trim();
        HFCandidateID.Value = Convert.ToString(GVInterviewRescheduledList.DataKeys[row.RowIndex].Values[1]).Trim();
        HFCanRID.Value = Convert.ToString(GVInterviewRescheduledList.DataKeys[row.RowIndex].Values[2]).Trim();
        Response.Redirect("~/procs/InterviewerRescheduledFeedback.aspx?ReqID=" + HFRecruitment_ReqID.Value + "&CanID=" + HFCandidateID.Value + "&CanSRID=" + HFCanRID.Value);
    }

    protected void GVInterviewRescheduledList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVInterviewRescheduledList.PageIndex = e.NewPageIndex;
        
        if (Lstband.SelectedValue == "0" && LstDepartment.SelectedValue == "0" && LstLocation.SelectedValue == "0" && lstRequisitionNo.SelectedValue == "0" && lstSkillSet.SelectedValue == "0")
        {
            GetReq_InterviewerrescheduledList();
        }
        else
        {
            SearchMethod();
        }
    }
}