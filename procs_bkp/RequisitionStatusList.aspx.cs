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
using System.Collections.Generic;

public partial class procs_RequisitionStatusList : System.Web.UI.Page
{
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public int did = 0;
    SP_Methods spm = new SP_Methods();
    DataSet dsRecruterInox;
    // public DataTable dtRecCandidate, dtcandidateDetails, dtmainSkillSet;


    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
            // lblmessage.Text = "";
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
            // lpm.Emp_Code = Session["Empcode"].ToString();
            // lblmessage.Visible = true;
            //   
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    GetSkillsetName();
                    GetCompany_Location();
                    GetDepartmentMaster();
                    GetRecruiter();
                    GetlstSchedular();
                    GetlstEmploymentType();
                    GetlstCandidateInfo();
                    GetlstRecruitmentStatus();
                   // GetlstRequisitionComment();
                    GetlstPositionBand();
                    getMngRequisitionStatusList();
                    txtfromdate.Attributes.Add("onkeypress", "return onOnlyNumber(event);");
                    txttodate.Attributes.Add("onkeypress", "return onOnlyNumber(event);");
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    #endregion

    #region page Methods

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
        DataSet dsReqNo = new DataSet();
        try
        {
            DataTable dtEmployee = new DataTable();
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "SP_RequisitionViewStatusList";
            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(Session["Empcode"]);
            dtEmployee = spm.getMobileRemDataList(spars, "SP_GET_REQ_REQUISTIONNO");

            if (dtEmployee.Rows.Count > 0)
            {
                LstDepartment.DataSource = dtEmployee;
                LstDepartment.DataTextField = "Department_Name";
                LstDepartment.DataValueField = "Department_id";
                LstDepartment.DataBind();
                LstDepartment.Items.Insert(0, new ListItem("Select Department", "0"));
            }
            if (dtEmployee.Rows.Count == 1)
            {
                //LstDepartment.Enabled = false;
                LstDepartment.SelectedValue = dtEmployee.Rows[0]["Department_id"].ToString();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void GetRecruiter()
    {
        DataTable dtInterviewer = new DataTable();
        dtInterviewer = spm.GetRecruitment_Recruiter();
        if (dtInterviewer.Rows.Count > 0)
        {
            LstRecuiter.DataSource = dtInterviewer;
            LstRecuiter.DataTextField = "EmpName";
            LstRecuiter.DataValueField = "Emp_Code";
            LstRecuiter.DataBind();
            LstRecuiter.Items.Insert(0, new ListItem("Select Recruiter", "0"));
        }
    }

    public void GetlstSchedular()
    {
        SqlParameter[] spars = new SqlParameter[3];
        DataSet DS = new DataSet();
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_Req_InterviewScheduler";
        DS = spm.getDatasetList(spars, "SP_GETREQUISTIONLIST_DETAILS");

        if (DS.Tables[0].Rows.Count > 0)
        {
            LstScheduler.DataSource = DS.Tables[0];
            LstScheduler.DataTextField = "Emp_Name";
            LstScheduler.DataValueField = "InterviewerSchedularEmpCode";
            LstScheduler.DataBind();
            LstScheduler.Items.Insert(0, new ListItem("Select Scheduler", "0"));
        }
    }

    public void GetlstEmploymentType()
    {
        SqlParameter[] spars = new SqlParameter[3];
        DataSet DS = new DataSet();
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_Req_Candi_Employment_Type";
        DS = spm.getDatasetList(spars, "SP_GETREQUISTIONLIST_DETAILS");

        if (DS.Tables[0].Rows.Count > 0)
        {
            LstEmploymentType.DataSource = DS.Tables[0];
            LstEmploymentType.DataTextField = "Particulars";
            LstEmploymentType.DataValueField = "PID";
            LstEmploymentType.DataBind();
            LstEmploymentType.Items.Insert(0, new ListItem("Select Employment Type", "0"));
        }
    }

    public void GetlstCandidateInfo()
    {
        SqlParameter[] spars = new SqlParameter[3];
        DataSet DS = new DataSet();
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_Req_AllCandidateLst";
        DS = spm.getDatasetList(spars, "SP_GETREQUISTIONLIST_DETAILS");

        if (DS.Tables[0].Rows.Count > 0)
        {
            LstCandidate.DataSource = DS.Tables[0];
            LstCandidate.DataTextField = "CandidateName";
            LstCandidate.DataValueField = "Candidate_ID";
            LstCandidate.DataBind();
            LstCandidate.Items.Insert(0, new ListItem("Select Candidate", "0"));
        }
    }

    public void GetlstRecruitmentStatus()
    {
        SqlParameter[] spars = new SqlParameter[3];
        DataSet DS = new DataSet();
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_Req_RecruitmentStatusList";
        DS = spm.getDatasetList(spars, "SP_GETREQUISTIONLIST_DETAILS");

        if (DS.Tables[0].Rows.Count > 0)
        {
            LstRecruitmentStatus.DataSource = DS.Tables[0];
            LstRecruitmentStatus.DataTextField = "RecruitmentStatus";
            LstRecruitmentStatus.DataValueField = "RecruitmentStatus";
            LstRecruitmentStatus.DataBind();
            LstRecruitmentStatus.Items.Insert(0, new ListItem("Select Recruitment Status", "0"));
        }
    }

    //public void GetlstRequisitionComment()
    //{
    //    SqlParameter[] spars = new SqlParameter[3];
    //    DataSet DS = new DataSet();
    //    spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
    //    spars[0].Value = "sp_Req_RequisitionComment";
    //    DS = spm.getDatasetList(spars, "SP_GETREQUISTIONLIST_DETAILS");

    //    if (DS.Tables[0].Rows.Count > 0)
    //    {
    //        DDLRequisitionComment.DataSource = DS.Tables[0];
    //        DDLRequisitionComment.DataTextField = "Comments";
    //        DDLRequisitionComment.DataValueField = "Comments";
    //        DDLRequisitionComment.DataBind();
    //        DDLRequisitionComment.Items.Insert(0, new ListItem("Select Comment", "0"));
    //    }
    //}




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
    private void getMngRequisitionStatusList()
    {
        try
        {
            dsRecruterInox = spm.getRecruterInoxList(Convert.ToString(Session["Empcode"]).Trim(), "RequisitionStatusView");
            gvRecruterInoxList.DataSource = dsRecruterInox.Tables[0];
            gvRecruterInoxList.DataBind();
            if (dsRecruterInox.Tables[0].Rows.Count > 0)
            {
                lbltotalRecords.Text = "Total Records :- " + dsRecruterInox.Tables[0].Rows.Count;

                lstRequisitionNo.DataSource = dsRecruterInox.Tables[0];
                lstRequisitionNo.DataTextField = "RequisitionNumber";
                lstRequisitionNo.DataValueField = "Recruitment_ReqID";
                lstRequisitionNo.DataBind();
                lstRequisitionNo.Items.Insert(0, new ListItem("Select Requisition No", "0"));
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        HFRecruitment_ReqID.Value = Convert.ToString(gvRecruterInoxList.DataKeys[row.RowIndex].Values[0]).Trim();
        Response.Redirect("~/procs/RequisitionStatusView.aspx?Recruitment_ReqID=" + HFRecruitment_ReqID.Value);
    }

    #endregion

    protected void gvRecruterInoxList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRecruterInoxList.PageIndex = e.NewPageIndex;

        if (Txt_RequisitionComment.Text.Trim() =="" && LstCandidate.SelectedValue == "0" && LstScheduler.SelectedValue == "0" && LstRecuiter.SelectedValue == "0" && LstEmploymentType.SelectedValue == "0" && LstRecruitmentStatus.SelectedValue == "0" && txttodate.Text.Trim() == "" && txtfromdate.Text.Trim() == "" && Lstband.SelectedValue == "0" && LstDepartment.SelectedValue == "0" && LstLocation.SelectedValue == "0" && lstRequisitionNo.SelectedValue == "0" && lstSkillSet.SelectedValue == "0")
        {
            getMngRequisitionStatusList();
        }
        else
        {
            Searchmethod();
        }
    }

    private void Searchmethod()
    {
        try
        {
            int SkillSetID = 0, Recruitment_ReqID = 0, DeptID = 0;
            SkillSetID = Convert.ToString(lstSkillSet.SelectedValue).Trim() != "" ? Convert.ToInt32(lstSkillSet.SelectedValue) : 0;
            Recruitment_ReqID = Convert.ToString(lstRequisitionNo.SelectedValue).Trim() != "" ? Convert.ToInt32(lstRequisitionNo.SelectedValue) : 0;
            DeptID = Convert.ToString(LstDepartment.SelectedValue).Trim() != "" ? Convert.ToInt32(LstDepartment.SelectedValue) : 0;
            string[] fromdate, todate;
            string StartDate = "", EndDate = "";
            DateTime? STDate = null;
            DateTime? EDDate = null;

            if (txtfromdate.Text.Trim() == "" && txttodate.Text.Trim() != "")
            {

                lblmessagesearch.Text = "Select from Date"; 
            }
            else
            {
                if (txtfromdate.Text != "")
                {
                    fromdate = Convert.ToString(txtfromdate.Text).Trim().Split('/');
                    StartDate = Convert.ToString(fromdate[2]) + "-" + Convert.ToString(fromdate[1]) + "-" + Convert.ToString(fromdate[0]);
                    STDate = DateTime.ParseExact(StartDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    if (txttodate.Text.Trim() != "")
                    {
                        todate = Convert.ToString(txttodate.Text).Trim().Split('/');
                        EndDate = Convert.ToString(todate[2]) + "-" + Convert.ToString(todate[1]) + "-" + Convert.ToString(todate[0]);
                    }
                    else
                    {
                        EndDate = DateTime.Today.ToString("yyyy") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("dd");
                    }
                    EDDate = DateTime.ParseExact(EndDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                }
            }


            SqlParameter[] spars = new SqlParameter[16];
            DataSet DS = new DataSet();
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "RequisitionStatusViewSearch";
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
            spars[6] = new SqlParameter("@Department_id", SqlDbType.Int);
            spars[6].Value = DeptID;

            spars[7] = new SqlParameter("@FromDate", SqlDbType.DateTime);
            spars[7].Value = STDate;
            spars[8] = new SqlParameter("@ToDate", SqlDbType.DateTime);
            spars[8].Value = EDDate;
            spars[9] = new SqlParameter("@RecruitmentStatus", SqlDbType.VarChar);
            spars[9].Value = LstRecruitmentStatus.SelectedValue;
            spars[10] = new SqlParameter("@EmploymentType", SqlDbType.VarChar);
            spars[10].Value = LstEmploymentType.SelectedValue;
            spars[11] = new SqlParameter("@RecruiterCode", SqlDbType.VarChar);
            spars[11].Value = LstRecuiter.SelectedValue;
            spars[12] = new SqlParameter("@SchedulerCode", SqlDbType.VarChar);
            spars[12].Value = LstScheduler.SelectedValue;
            spars[13] = new SqlParameter("@Candidate", SqlDbType.VarChar);
            spars[13].Value = LstCandidate.SelectedValue;
            spars[14] = new SqlParameter("@Comment", SqlDbType.VarChar);
            spars[14].Value = Txt_RequisitionComment.Text.Trim();


            DS = spm.getDatasetList(spars, "SP_Get_Rec_RequiterChangeIndex_Search");

            if (DS.Tables[0].Rows.Count > 0)
            {
                lbltotalRecords.Text = "Total Records :- " + DS.Tables[0].Rows.Count;
                gvRecruterInoxList.DataSource = DS.Tables[0];
                gvRecruterInoxList.DataBind();
                lblmessagesearch.Text = "";
            }
            else
            {
                lblmessagesearch.Text = "Requisition Record's not found";
                gvRecruterInoxList.DataSource = null;
                gvRecruterInoxList.DataBind();
                lbltotalRecords.Text = "";

            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        Searchmethod();
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        lstSkillSet.SelectedValue = "0";
        LstLocation.SelectedValue = "0";
        LstDepartment.SelectedValue = "0";
        Lstband.SelectedValue = "0";
        txtfromdate.Text = "";
        txttodate.Text = "";
        LstRecruitmentStatus.SelectedValue = "0";
        LstEmploymentType.SelectedValue = "0";
        LstRecuiter.SelectedValue = "0";
        LstScheduler.SelectedValue = "0";
        LstCandidate.SelectedValue = "0";
        lblmessagesearch.Text = "";
        Txt_RequisitionComment.Text = "";
        getMngRequisitionStatusList();
    }
    public static DateTime FirstDayOfWeek(DateTime dt)
    {
        var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
        var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
        if (diff < 0)
            diff += 7;
        return dt.AddDays(-diff).Date;
    }
    public static DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
    {
        int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-1 * diff).Date;
    }

    public void StartOfWeek()
    {
        DateTime dt = StartOfWeek(DateTime.Now, DayOfWeek.Monday);
        DateTime Endaday = dt.AddDays(6);
        txtfromdate.Text = dt.ToString("dd/MM/yyyy");
        txttodate.Text = Endaday.ToString("dd/MM/yyyy");
    }

    private Boolean Date_Validation()
    {
        lblmessagesearch.Text = "";
        Boolean blnValid = false;

        DateTime? ddt = null;
        DateTime? ddt2 = null;
        string[] strdate, strdate1;
        string StartDate = "", EndDate = "";
        strdate = Convert.ToString(txtfromdate.Text).Trim().Split('/');
        if (strdate[2].Length > 3)
        {
            StartDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            ddt = DateTime.ParseExact(StartDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
        }
        strdate1 = Convert.ToString(txttodate.Text).Trim().Split('/');
        if (strdate1[2].Length > 3)
        {
            EndDate = Convert.ToString(strdate1[2]) + "-" + Convert.ToString(strdate1[1]) + "-" + Convert.ToString(strdate1[0]);
            ddt2 = DateTime.ParseExact(EndDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
        }
        if (ddt <= ddt2)
        {

            blnValid = true;
        }

        return blnValid;
    }

    protected void txtfromdate_TextChanged1(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";

        if ((Convert.ToString(txtfromdate.Text).Trim() != "") && (Convert.ToString(txttodate.Text).Trim() != ""))
        {
            strdate = Convert.ToString(txtfromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

            strdate = Convert.ToString(txttodate.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);


            DateTime startDate = Convert.ToDateTime(strfromDate);
            DateTime endDate = Convert.ToDateTime(strToDate);
            if (startDate > endDate)
            {
                lblmessagesearch.Text = "From Date should be less than To Date ";
                txtfromdate.Text = "";
                return;
            }
            else
            {
                lblmessagesearch.Text = "";
                // ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Start date should be greate than End date')", true);
            }
        }
    }

    protected void txttodate_TextChanged(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";

        if ((Convert.ToString(txtfromdate.Text).Trim() != "") && (Convert.ToString(txttodate.Text).Trim() != ""))
        {
            strdate = Convert.ToString(txtfromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

            strdate = Convert.ToString(txttodate.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);


            DateTime startDate = Convert.ToDateTime(strfromDate);
            DateTime endDate = Convert.ToDateTime(strToDate);
            if (startDate > endDate)
            {
                lblmessagesearch.Text = "To Date should be greater than From Date ";
                txttodate.Text = "";

                return;
            }
            else
            {
                // ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Start date should be greate than End date')", true);
            }
        }
    }
}