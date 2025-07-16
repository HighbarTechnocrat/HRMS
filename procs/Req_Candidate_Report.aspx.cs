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

public partial class Req_Candidate_Report : System.Web.UI.Page
{
    #region CreativeMethods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public DataSet DSDetailReport;
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
                    GetCVSource();
                    GetModules();
                   // GetDepartmentMaster();
                    GetEmployeement_Status();
                    CurrentlyonNotice();
                    GetCandidateName();
                   // GetSkillsetName();
                    //GetRecruitmentStatus();
                    //txtfromdate.Attributes.Add("onkeypress", "return onOnlyNumber(event);");
                    //txttodate.Attributes.Add("onkeypress", "return onOnlyNumber(event);");
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    public void StartOfWeek()
    {
        //DateTime dt = StartOfWeek(DateTime.Now, DayOfWeek.Monday);
        //DateTime Endaday = dt.AddDays(6);
        //txtfromdate.Text = dt.ToString("dd/MM/yyyy");
        //txttodate.Text = Endaday.ToString("dd/MM/yyyy");


    }
   
    public void GetModules()
    {
        DataTable dtPositionCriti = new DataTable();
        DataTable dtEmployee = new DataTable();
        SqlParameter[] spars = new SqlParameter[1];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "GetMODULE";
        //spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        //spars[1].Value = Convert.ToString(Session["Empcode"]);
       // dtEmployee = spm.getMobileRemDataList(spars, "SP_Get_Rec_Candidate_Detail_Report");

        dtPositionCriti = spm.getMobileRemDataList(spars, "SP_Get_Rec_Candidate_Detail_Report");
        if (dtPositionCriti.Rows.Count > 0)
        {
            lstPositionCriti.DataSource = dtPositionCriti;
            lstPositionCriti.DataTextField = "ModuleDesc";
            lstPositionCriti.DataValueField = "ModuleId";
            lstPositionCriti.DataBind();
            lstPositionCriti.Items.Insert(0, new ListItem("Select Module", "0"));
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

    public void GetEmployeement_Status()
    {

        lstPositionLoca.Items.Insert(0, new ListItem("Select Employeement Status", "0"));
        lstPositionLoca.Items.Insert(1, new ListItem("Onboard", "Onboard"));
        lstPositionLoca.Items.Insert(2, new ListItem("Open", "Open"));

    }
    public void CurrentlyonNotice()
    {
        //DataTable dtInterviewer = new DataTable();
        //dtInterviewer = spm.GetRecruitment_Curre();
        //if (dtInterviewer.Rows.Count > 0)
        //{
        //    lstRecruiter.DataSource = dtInterviewer;
        //    lstRecruiter.DataTextField = "EmpName";
        //    lstRecruiter.DataValueField = "Emp_Code";
        //    lstRecruiter.DataBind();
            lstRecruiter.Items.Insert(0, new ListItem("Select Currently On Notice", "0"));
            lstRecruiter.Items.Insert(1, new ListItem("YES", "1"));
            lstRecruiter.Items.Insert(2, new ListItem("No", "2"));
//}
    }

	public void GetCVSource()
	{
		DataSet dsReqNo = new DataSet();
		try
		{
			DataTable dtEmployee = new DataTable();
			SqlParameter[] spars = new SqlParameter[1];
			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "GetCVSource";
			//spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			//spars[1].Value = Convert.ToString(Session["Empcode"]);
			dtEmployee = spm.getMobileRemDataList(spars, "SP_Get_Rec_Candidate_Detail_Report");
			if (dtEmployee.Rows.Count > 0)
			{
				lstPositionDept.DataSource = dtEmployee;
				lstPositionDept.DataTextField = "CVSource";
				lstPositionDept.DataValueField = "CVSource_ID";
				lstPositionDept.DataBind();
				lstPositionDept.Items.Insert(0, new ListItem("Select CV Source", "0"));
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}

	public void GetCandidateName()
    {
        DataTable dtEmployee = new DataTable();
        SqlParameter[] spars = new SqlParameter[1];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "GetCandidateName";
        //spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        //spars[1].Value = Convert.ToString(Session["Empcode"]);
        dtEmployee = spm.getMobileRemDataList(spars, "SP_Get_Rec_Candidate_Detail_Report");
        if (dtEmployee.Rows.Count > 0)
        {
            DDLSkillSet.DataSource = dtEmployee;
            DDLSkillSet.DataTextField = "CandidateName";
            DDLSkillSet.DataValueField = "Candidate_ID";
            DDLSkillSet.DataBind();
            DDLSkillSet.Items.Insert(0, new ListItem("Select Name", "0"));

        }
    }
    public void GetRecruitmentStatus()
    {
        DataTable dtRecruitmentStatus = new DataTable();
        dtRecruitmentStatus = spm.GetRecruitment_RecruitmentStatus();
        if (dtRecruitmentStatus.Rows.Count > 0)
        {
            DDLStatus.DataSource = dtRecruitmentStatus;
            DDLStatus.DataTextField = "RecruitmentStatus";
            DDLStatus.DataValueField = "RecStatusID";
            DDLStatus.DataBind();
            DDLStatus.Items.Insert(0, new ListItem("Select Status", "0"));

        }
    }

    //protected void txtfromdate_TextChanged(object sender, EventArgs e)
    //{
    //    if (txttodate.Text.Trim() != "" && txtfromdate.Text.Trim() != "")
    //    {
    //        if (Date_Validation() == false)
    //        {
    //            lblmessage.Text = "From date cannot be greater than To Date";
    //            return;
    //        }
    //    }
    //}
    private Boolean Date_Validation()
    {
        lblmessage.Text = "";
        Boolean blnValid = false;

        //DateTime? ddt = null;
        //DateTime? ddt2 = null;
        //string[] strdate, strdate1;
        //string StartDate = "", EndDate = "";
        //strdate = Convert.ToString(txtfromdate.Text).Trim().Split('/');
        //if (strdate[2].Length > 3)
        //{
        //    StartDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        //    ddt = DateTime.ParseExact(StartDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
        //}
        //strdate1 = Convert.ToString(txttodate.Text).Trim().Split('/');
        //if (strdate1[2].Length > 3)
        //{
        //    EndDate = Convert.ToString(strdate1[2]) + "-" + Convert.ToString(strdate1[1]) + "-" + Convert.ToString(strdate1[0]);
        //    ddt2 = DateTime.ParseExact(EndDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
        //}
        //if (ddt <= ddt2)
        //{

        //    blnValid = true;
        //}

        return blnValid;
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            #region get Rec Dept details
            string[] fromdate, todate;
            string StartDate = "", EndDate = "", Stype = "SelectDetailReport", CVSource = "", EmploymentType = "", Module = "";
            string CurrentlyOnNotice = "", CandidateName = "", UrgencyCritiName = "";

            int DeptID = 0, PositionCritiID = 0, SkillsetID=0, RecStatusID=0;
            lblmessage.Text = "";
            ReportViewer1.Visible = false;

            string confirmValue = hdnYesNo.Value.ToString();


            StartDate = DateTime.Now.ToString();
            EndDate = DateTime.Now.ToString();
            var ddlCVSource = "";
            var ddlEmploymentStatus = "";
            var ddlModule = "";
            var ddlCurrentlyonNotice = "";//CurrentlyonNotice
            var ddlName = "";
            var ddlStatus = "";
            var isSelected = false;
            //DDL CV Source
            foreach (ListItem item in lstPositionDept.Items)
            {
                if (item.Selected)
                {
                    if(item.Value!=""&& item.Value != "0")
                    {
                        isSelected = true;
                        if (ddlCVSource == "")
                        {
                            ddlCVSource = item.Value;
                            CVSource = item.Text;
                        }                          
                        else
                        {
                            ddlCVSource = ddlCVSource + "|" + item.Value;
                            CVSource = CVSource + ", " + item.Text;
                        }
                    }                    
                }
            }
            //DDl ddlEmploymentStatus
            foreach (ListItem item in lstPositionLoca.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        isSelected = true;
                        if (ddlEmploymentStatus == "")
                        {
                            ddlEmploymentStatus = item.Value;
                            EmploymentType = item.Text;
                        }
                        else
                        {
                            ddlEmploymentStatus = ddlEmploymentStatus + "|" + item.Value;
                            EmploymentType = EmploymentType + ", " + item.Text;
                        }
                    }
                }
            }
            // ddlModule
            foreach (ListItem item in lstPositionCriti.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        isSelected = true;
                        if (ddlModule == "")
                        {
                            ddlModule = item.Value;
                            Module = item.Text;
                        }
                        else
                        {
                            ddlModule = ddlModule + "|" + item.Value;
                            Module = Module + ", "+item.Text;
                        }
                    }
                }
            }
            // Currently on Notice
            foreach (ListItem item in lstRecruiter.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        isSelected = true;
                        if (ddlCurrentlyonNotice == "")
                        {
                            ddlCurrentlyonNotice = item.Value;
                            CurrentlyOnNotice = item.Text;
                        }
                        else
                        {
                            ddlCurrentlyonNotice = ddlCurrentlyonNotice + "|" + item.Value;
                            CurrentlyOnNotice = CurrentlyOnNotice + ", " + item.Text;
                        }
                    }
                }
            }

            // ddlSkillSet
            foreach (ListItem item in DDLSkillSet.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        isSelected = true;
                        if (ddlName == "")
                        {
                            ddlName = item.Value;
                            CandidateName = item.Text;
                        }
                        else
                        {
                            ddlName = ddlName + "|" + item.Value;
                            CandidateName = CandidateName + ", " + item.Text;
                        }
                    }
                }
            }

            SqlParameter[] spars = new SqlParameter[7];
            spars[0] = new SqlParameter("@EmploymentType", SqlDbType.NVarChar);
            spars[0].Value = ddlEmploymentStatus;
            spars[1] = new SqlParameter("@CVSource", SqlDbType.NVarChar);
            spars[1].Value = ddlCVSource;
            spars[2] = new SqlParameter("@CandidateId", SqlDbType.NVarChar);
            spars[2].Value = ddlName;            
            spars[3] = new SqlParameter("@CurrentlyonNotice", SqlDbType.NVarChar);
            spars[3].Value = ddlCurrentlyonNotice;
            spars[4] = new SqlParameter("@Module", SqlDbType.NVarChar);
            spars[4].Value = ddlModule;            
            spars[5] = new SqlParameter("@FromDate", SqlDbType.Date);
            spars[5].Value = DateTime.Now;
            spars[6] = new SqlParameter("@stype", SqlDbType.NVarChar);
            spars[6].Value = "GetReportDetails";
            //DSDetailReport = spm.Get_Requisition_Detail_Report(Stype, STDate, EDDate, lstPositionLoca.SelectedValue.Trim(), DeptID, SkillsetID, RecStatusID, lstRecruiter.SelectedValue.Trim(),PositionCritiID);
            //DSDetailReport = spm.Get_Requisition_Detail_Report(Stype, Convert.ToDateTime(StartDate), Convert.ToDateTime(EndDate), lstPositionLoca.SelectedValue.Trim(), DeptID, SkillsetID, RecStatusID, lstRecruiter.SelectedValue.Trim(), PositionCritiID, Convert.ToString(Session["Empcode"]));
            DSDetailReport = spm.getServiceRequestReportCount(spars, "SP_Get_Rec_Candidate_Detail_Report");


            #endregion
            if (DSDetailReport.Tables[0].Rows.Count > 0)
            {
                ReportViewer1.Visible = true;
                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.LocalReport.DataSources.Clear();

                ReportParameter[] param = new ReportParameter[7];
              //  param[0] = new ReportParameter("pyear", "Opening as on " + Convert.ToString(txtfromdate.Text.Replace('/', '-')));
                //param[0] = new ReportParameter("FromDate", Convert.ToString(txtfromdate.Text.Replace('/', '-')));
                //param[1] = new ReportParameter("ToDate", Convert.ToString(txttodate.Text.Replace('/', '-')));
                param[0] = new ReportParameter("FromDate", "");
                param[1] = new ReportParameter("ToDate", "");
                param[2] = new ReportParameter("Location", Convert.ToString(CVSource));
                param[3] = new ReportParameter("DeptName", Convert.ToString(EmploymentType));
                param[4] = new ReportParameter("SkillSet", Convert.ToString(Module));
                param[5] = new ReportParameter("Status", Convert.ToString(CurrentlyOnNotice));
                param[6] = new ReportParameter("Recruiter", Convert.ToString(CandidateName));
               // param[7] = new ReportParameter("Urgency", Convert.ToString(UrgencyCritiName));

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
              //  ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/Req_Deptwise_Report.rdlc");
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/Req_CandidateDetail_Reports.rdlc");
                ReportDataSource rds = new ReportDataSource("CandidateDetails", DSDetailReport.Tables[0]);
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
}