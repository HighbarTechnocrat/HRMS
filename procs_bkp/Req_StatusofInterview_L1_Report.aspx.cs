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

public partial class procs_Req_StatusofInterview_L1_Report : System.Web.UI.Page
{
    #region CreativeMethods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
   // public DataSet DSStatusofInterviewL1; 
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    #endregion
    SP_Methods spm = new SP_Methods();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }


            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    GetHODDept();
                    GetlstInterveiwer();
                    GetCompany_Location();
                    GetRecruiter();
                    GetSkillsetName();
                    GetCandiateName();
                    InterviewStatus();
                    InterviewFeedBack();
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

    public void GetDepartmentMaster1()
    {
        DataTable dtPositionDept = new DataTable();
        dtPositionDept = spm.GetRecruitment_Req_DepartmentMaster();
        if (dtPositionDept.Rows.Count > 0)
        {
            lstPositionDept.DataSource = dtPositionDept;
            lstPositionDept.DataTextField = "Department_Name";
            lstPositionDept.DataValueField = "Department_id";
            lstPositionDept.DataBind();
            lstPositionDept.Items.Insert(0, new ListItem("Select Department", "0"));
        }
    }
    public void GetDepartmentMaster()
    {
        DataTable dtPositionDept = new DataTable();
        dtPositionDept = spm.GetRecruitment_Req_DepartmentMaster();
        if (dtPositionDept.Rows.Count > 0)
        {
            lstPositionDept.DataSource = dtPositionDept;
            lstPositionDept.DataTextField = "Department_Name";
            lstPositionDept.DataValueField = "Department_id";
            lstPositionDept.DataBind();
            lstPositionDept.Items.Insert(0, new ListItem("Select Department", "0"));
            //lstPositionDept.Enabled = false;
            //////updated code
            //DataRow[] dr = dtPositionDept.Select("Department_Name = '" + hflEmpDepartment.Value.ToString().Trim() + "'");
            //if (dr.Length > 0)
            //{
            //    string avalue = dr[0]["Department_id"].ToString();
            //    lstPositionDept.SelectedValue = avalue;
            //}
        }
    }
    public void GetHODDept()
    {
        DataSet dsReqNo = new DataSet();
        try
        {
            DataTable dtEmployee = new DataTable();
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_Report_HOD";
            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(Session["Empcode"]);
            dtEmployee = spm.getMobileRemDataList(spars, "SP_GET_REQ_REQUISTIONNO");
            if (dtEmployee.Rows.Count > 0)
            {
                lstPositionDept.DataSource = dtEmployee;
                lstPositionDept.DataTextField = "Department_Name";
                lstPositionDept.DataValueField = "Department_id";
                lstPositionDept.DataBind();
                lstPositionDept.Items.Insert(0, new ListItem("Select Department", "0"));
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    public void GetCompany_Location()
    {
        DataTable lstPosition = new DataTable();
        lstPosition = spm.GetRecruitment_Req_company_Location();
        if (lstPosition.Rows.Count > 0)
        {
            lstPositionLoca.DataSource = lstPosition;
            lstPositionLoca.DataTextField = "Location_name";
            lstPositionLoca.DataValueField = "comp_code";
            lstPositionLoca.DataBind();
            lstPositionLoca.Items.Insert(0, new ListItem("Select Position Location", "0"));

        }
    }
    public void GetSkillsetName()
    {
        DataTable dtSkillset = new DataTable();
        dtSkillset = spm.GetRecruitment_SkillsetName();
        if (dtSkillset.Rows.Count > 0)
        {
            DDLSkillSet.DataSource = dtSkillset;
            DDLSkillSet.DataTextField = "ModuleDesc";
            DDLSkillSet.DataValueField = "ModuleId";
            DDLSkillSet.DataBind();
            DDLSkillSet.Items.Insert(0, new ListItem("Select Skillset", "0"));

        }
    }
    public void GetRecruiter()
    {
        DataTable dtInterviewer = new DataTable();
        dtInterviewer = spm.GetRecruitment_Recruiter();
        if (dtInterviewer.Rows.Count > 0)
        {
            lstRecruiter.DataSource = dtInterviewer;
            lstRecruiter.DataTextField = "EmpName";
            lstRecruiter.DataValueField = "Emp_Code";
            lstRecruiter.DataBind();
            lstRecruiter.Items.Insert(0, new ListItem("Select Recruiter", "0"));
        }
    }
    public void GetlstInterveiwer()
    {
        SqlParameter[] spars = new SqlParameter[3];
        DataSet DS = new DataSet();
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_Req_InterviewerL1";
        DS = spm.getDatasetList(spars, "SP_GETREQUISTIONLIST_DETAILS");

        if (DS.Tables[0].Rows.Count > 0)
        {
            lstInterveiwer.DataSource = DS.Tables[0];
            lstInterveiwer.DataTextField = "Emp_Name";
            lstInterveiwer.DataValueField = "InterEmpCode";
            lstInterveiwer.DataBind();
            lstInterveiwer.Items.Insert(0, new ListItem("Select Interveiwer", "0"));
        }
    }
    public void GetCandiateName()
    {
        SqlParameter[] spars = new SqlParameter[3];
        DataSet DS = new DataSet();
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_Req_CandidateL1";
        DS = spm.getDatasetList(spars, "SP_GETREQUISTIONLIST_DETAILS");
        if (DS.Tables[0].Rows.Count > 0)
        {
            DDLCandiate.DataSource = DS.Tables[0];
            DDLCandiate.DataTextField = "CandidateName";
            DDLCandiate.DataValueField = "Candidate_ID";
            DDLCandiate.DataBind();
            DDLCandiate.Items.Insert(0, new ListItem("Select Candidate", "0"));
        }
    }
    public void InterviewStatus()
    {
        SqlParameter[] spars = new SqlParameter[3];
        DataSet DS = new DataSet();
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_Req_InterviewStatusL1";
        DS = spm.getDatasetList(spars, "SP_GETREQUISTIONLIST_DETAILS");
        if (DS.Tables[0].Rows.Count > 0)
        {
            DDlInterviewstatus.DataSource = DS.Tables[0];
            DDlInterviewstatus.DataTextField = "InterviewStatus";
            DDlInterviewstatus.DataValueField = "InterviewStatus_ID";
            DDlInterviewstatus.DataBind();
            DDlInterviewstatus.Items.Insert(0, new ListItem("Select Interview Status", "0"));
        }
    }
    public void InterviewFeedBack()
    {
        SqlParameter[] spars = new SqlParameter[3];
        DataSet DS = new DataSet();
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_Req_InterviewFeedbackL1";
        DS = spm.getDatasetList(spars, "SP_GETREQUISTIONLIST_DETAILS");
        if (DS.Tables[0].Rows.Count > 0)
        {
            ddlinterviewfeedback.DataSource = DS.Tables[0];
            ddlinterviewfeedback.DataTextField = "InterviewFeedback";
            ddlinterviewfeedback.DataValueField = "InterviewFeedback_ID";
            ddlinterviewfeedback.DataBind();
            ddlinterviewfeedback.Items.Insert(0, new ListItem("Select Interview FeedBack", "0"));
        }
    }
    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            #region get Rec Dept details
           
            lblmessage.Text = "";
            ReportViewer1.Visible = false;
            string confirmValue = hdnYesNo.Value.ToString();
            string[] fromdate, todate;
            string StartDate = "", EndDate = "";
            DateTime? STDate = null;
            DateTime? EDDate = null;

            if (txtfromdate.Text.Trim() == "")
            {
                ReportViewer1.Visible = false;
                lblmessage.Text = "Please Select From Date";
            }
            else
            {
                lblmessage.Text = "";
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

                SqlParameter[] spars = new SqlParameter[13];
                DataSet DS = new DataSet();
                spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                spars[0].Value = "sp_Req_InterviewFeedbackL1";
                spars[1] = new SqlParameter("@Department_id", SqlDbType.Int);
                spars[1].Value = Convert.ToInt32(lstPositionDept.SelectedValue);
                spars[2] = new SqlParameter("@comp_code", SqlDbType.VarChar);
                spars[2].Value = lstPositionLoca.SelectedValue;
                spars[3] = new SqlParameter("@SkillSetID", SqlDbType.Int);
                spars[3].Value = Convert.ToInt32(DDLSkillSet.SelectedValue);
                spars[4] = new SqlParameter("@Recruiter_Code", SqlDbType.VarChar);
                spars[4].Value = lstRecruiter.SelectedValue;
                spars[5] = new SqlParameter("@interviewer_Code", SqlDbType.VarChar);
                spars[5].Value = lstInterveiwer.SelectedValue;
                spars[6] = new SqlParameter("@CandidateID", SqlDbType.Int);
                spars[6].Value = DDLCandiate.SelectedValue;
                spars[7] = new SqlParameter("@InterviewStatusID", SqlDbType.Int);
                spars[7].Value = Convert.ToInt32(DDlInterviewstatus.SelectedValue);
                spars[8] = new SqlParameter("@InterviewFeedBackID", SqlDbType.Int);
                spars[8].Value = Convert.ToInt32(ddlinterviewfeedback.SelectedValue);
                spars[9] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                spars[9].Value = Session["Empcode"].ToString();
                spars[10] = new SqlParameter("@FromDate", SqlDbType.DateTime);
                spars[10].Value = STDate;
                spars[11] = new SqlParameter("@ToDate", SqlDbType.DateTime);
                spars[11].Value = EDDate;

                DS = spm.getDatasetList(spars, "SP_Get_Rec_StatusofInterview_L1_Report");

                #endregion
                if (DS.Tables[0].Rows.Count > 0)
                {
                    ReportViewer1.Visible = true;
                    ReportViewer1.LocalReport.Refresh();
                    ReportViewer1.LocalReport.DataSources.Clear();

                    ReportParameter[] param = new ReportParameter[10];

                    if (txtfromdate.Text.Trim() == "" && txttodate.Text.Trim() == "")
                    {
                        param[0] = new ReportParameter("FromDate", "");
                        param[1] = new ReportParameter("ToDate", "");
                    }
                    else
                    {
                        param[0] = new ReportParameter("FromDate", Convert.ToString(txtfromdate.Text.Replace('/', '-')));
                        if (txttodate.Text.Trim() != "")
                        {
                            param[1] = new ReportParameter("ToDate", Convert.ToString(txttodate.Text.Replace('/', '-')));
                        }
                        else
                        {
                            string strToDate_RPt = DateTime.Today.ToString("dd") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("yyyy");
                            param[1] = new ReportParameter("ToDate", strToDate_RPt);
                        }
                    }


                    if (lstPositionLoca.SelectedValue == "0")
                    {
                        param[2] = new ReportParameter("Location", "");
                    }
                    else
                    {
                        param[2] = new ReportParameter("Location", lstPositionLoca.SelectedItem.Text);
                    }
                    if (lstPositionDept.SelectedValue == "0")
                    {
                        param[3] = new ReportParameter("DeptName", "");
                    }
                    else
                    {
                        param[3] = new ReportParameter("DeptName", lstPositionDept.SelectedItem.Text);
                    }
                    if (DDLSkillSet.SelectedValue == "0")
                    {
                        param[4] = new ReportParameter("SkillSet", "");
                    }
                    else
                    {
                        param[4] = new ReportParameter("SkillSet", DDLSkillSet.SelectedItem.Text);
                    }
                    if (DDLCandiate.SelectedValue == "0")
                    {
                        param[5] = new ReportParameter("Candidate", "");
                    }
                    else
                    {
                        param[5] = new ReportParameter("Candidate", DDLCandiate.SelectedItem.Text);
                    }
                    if (lstRecruiter.SelectedValue == "0")
                    {
                        param[6] = new ReportParameter("Recruiter", "");
                    }
                    else
                    {
                        param[6] = new ReportParameter("Recruiter", lstRecruiter.SelectedItem.Text);
                    }
                    if (lstInterveiwer.SelectedValue == "0")
                    {
                        param[7] = new ReportParameter("Interviewer", "");
                    }
                    else
                    {
                        param[7] = new ReportParameter("Interviewer", lstInterveiwer.SelectedItem.Text);
                    }
                    if (DDlInterviewstatus.SelectedValue == "0")
                    {
                        param[8] = new ReportParameter("InterviewStatus", "");
                    }
                    else
                    {
                        param[8] = new ReportParameter("InterviewStatus", DDlInterviewstatus.SelectedItem.Text);
                    }
                    if (ddlinterviewfeedback.SelectedValue == "0")
                    {
                        param[9] = new ReportParameter("InterviewFeedBack", "");
                    }
                    else
                    {
                        param[9] = new ReportParameter("InterviewFeedBack", ddlinterviewfeedback.SelectedItem.Text);
                    }

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/Req_StatusofInterview_L1_Report.rdlc");
                    ReportDataSource rds = new ReportDataSource("StatusofInterviewL1", DS.Tables[0]);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    ReportViewer1.LocalReport.SetParameters(param);
                    ReportViewer1.LocalReport.Refresh();


                }
                else
                {
                    ReportViewer1.Visible = false;
                    lblmessage.Text = "Record not available";
                }
            }
        }
        catch (Exception ex)
        {
        }
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

    protected void txtfromdate_TextChanged(object sender, EventArgs e)
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
                lblmessage.Text = "From Date should be less than To Date ";
                txtfromdate.Text = "";
                return;
            }
            else
            {
                lblmessage.Text = "";
                // ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Start date should be greate than End date')", true);
            }
        }
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
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
                lblmessage.Text = "To Date should be greater than From Date ";
                txttodate.Text = "";

                return;
            }
            else
            {
                // ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Start date should be greate than End date')", true);
            }
        }
    }
    private Boolean Date_Validation()
    {
        lblmessage.Text = "";
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
}