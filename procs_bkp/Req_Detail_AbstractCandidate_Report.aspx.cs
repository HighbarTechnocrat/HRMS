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

public partial class procs_Req_Detail_AbstractCandidate_Report : System.Web.UI.Page
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
                    GetHODDept();
                    GetPositionCriticality();
                    GetCompany_Location();
                    GetRecruiter();
                    GetSkillsetName();
                    GetRecruitmentStatus();
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

    public void GetPositionCriticality()
    {
        DataTable dtPositionCriti = new DataTable();
        dtPositionCriti = spm.GetRecruitment_Req_PositionCriticality();
        if (dtPositionCriti.Rows.Count > 0)
        {
            lstPositionCriti.DataSource = dtPositionCriti;
            lstPositionCriti.DataTextField = "PositionCriticality";
            lstPositionCriti.DataValueField = "PositionCriticality_ID";
            lstPositionCriti.DataBind();
            lstPositionCriti.Items.Insert(0, new ListItem("Select Criticality", "0"));
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

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            #region get Rec Dept details
            string[] fromdate, todate;
            string StartDate = "", EndDate = "", Stype = "SelectDetailReport", Location = "", DeptName = "", Skillset = "";
            string RecStatus = "", Recruiter = "", UrgencyCritiName = "";

            int DeptID = 0, PositionCritiID = 0, SkillsetID = 0, RecStatusID = 0;
            lblmessage.Text = "";
            ReportViewer1.Visible = false;
            string confirmValue = hdnYesNo.Value.ToString();
            DeptID = Convert.ToString(lstPositionDept.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionDept.SelectedValue) : 0;
            PositionCritiID = Convert.ToString(lstPositionCriti.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionCriti.SelectedValue) : 0;
            SkillsetID = Convert.ToString(DDLSkillSet.SelectedValue).Trim() != "" ? Convert.ToInt32(DDLSkillSet.SelectedValue) : 0;
            RecStatusID = Convert.ToString(DDLStatus.SelectedValue).Trim() != "" ? Convert.ToInt32(DDLStatus.SelectedValue) : 0;

            StartDate = DateTime.Now.ToString();
            EndDate = DateTime.Now.ToString();
            var ddlDepartment = "";
            var ddlPositionLocation = "";
            var ddlPositionCriticality = "";
            var ddlRecruiter = "";
            var ddlSkillSet = "";
            var ddlStatus = "";
            var isSelected = false;
            //DDL Department
            foreach (ListItem item in lstPositionDept.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        isSelected = true;
                        if (ddlDepartment == "")
                        {
                            ddlDepartment = item.Value;
                            DeptName = item.Text;
                        }
                        else
                        {
                            ddlDepartment = ddlDepartment + "|" + item.Value;
                            DeptName = DeptName + "," + item.Text;
                        }
                    }
                }
            }
            //DDl Location
            foreach (ListItem item in lstPositionLoca.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        isSelected = true;
                        if (ddlPositionLocation == "")
                        {
                            ddlPositionLocation = item.Value;
                            Location = item.Text;
                        }
                        else
                        {
                            ddlPositionLocation = ddlPositionLocation + "|" + item.Value;
                            Location = Location + "," + item.Text;
                        }
                    }
                }
            }
            // ddlPositionCriticality
            foreach (ListItem item in lstPositionCriti.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        isSelected = true;
                        if (ddlPositionCriticality == "")
                        {
                            ddlPositionCriticality = item.Value;
                            UrgencyCritiName = item.Text;
                        }
                        else
                        {
                            ddlPositionCriticality = ddlPositionCriticality + "|" + item.Value;
                            UrgencyCritiName = UrgencyCritiName + "," + item.Text;
                        }
                    }
                }
            }
            // ddlRecruiter
            foreach (ListItem item in lstRecruiter.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        isSelected = true;
                        if (ddlRecruiter == "")
                        {
                            ddlRecruiter = item.Value;
                            Recruiter = item.Text;
                        }
                        else
                        {
                            ddlRecruiter = ddlRecruiter + "|" + item.Value;
                            Recruiter = Recruiter + "," + item.Text;
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
                        if (ddlSkillSet == "")
                        {
                            ddlSkillSet = item.Value;
                            Skillset = item.Text;
                        }
                        else
                        {
                            ddlSkillSet = ddlSkillSet + "|" + item.Value;
                            Skillset = Skillset + "," + item.Text;
                        }
                    }
                }
            }
            // ddlStatus
            foreach (ListItem item in DDLStatus.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        isSelected = true;
                        if (ddlStatus == "")
                        {
                            ddlStatus = item.Value;
                            RecStatus = item.Text;
                        }
                        else
                        {
                            ddlStatus = ddlStatus + "|" + item.Value;
                            RecStatus = RecStatus + "," + item.Text;
                        }
                    }
                }
            }

            SqlParameter[] spars = new SqlParameter[9];
            spars[0] = new SqlParameter("@compcodes", SqlDbType.NVarChar);
            spars[0].Value = ddlPositionLocation;
            spars[1] = new SqlParameter("@DepartmentIds", SqlDbType.NVarChar);
            spars[1].Value = ddlDepartment;
            spars[2] = new SqlParameter("@SkillSetIDs", SqlDbType.NVarChar);
            spars[2].Value = ddlSkillSet;
            spars[3] = new SqlParameter("@RecStatusIDs", SqlDbType.NVarChar);
            spars[3].Value = ddlStatus;
            spars[4] = new SqlParameter("@RecruiterCodes", SqlDbType.NVarChar);
            spars[4].Value = ddlRecruiter;
            spars[5] = new SqlParameter("@PositionCriticalityIDs", SqlDbType.NVarChar);
            spars[5].Value = ddlPositionCriticality;
            spars[6] = new SqlParameter("@EmpCode", SqlDbType.NVarChar);
            spars[6].Value = Convert.ToString(Session["Empcode"]);
            spars[7] = new SqlParameter("@FromDate", SqlDbType.Date);
            spars[7].Value = DateTime.Now;
            spars[8] = new SqlParameter("@stype", SqlDbType.NVarChar);
            spars[8].Value = "";
            DSDetailReport = spm.getServiceRequestReportCount(spars, "SP_Get_Rec_Detail_AbstractCandidate_Report");
            #endregion
            if (DSDetailReport.Tables[0].Rows.Count > 0)
            {
                ReportViewer1.Visible = true;
                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportParameter[] param = new ReportParameter[8];
                param[0] = new ReportParameter("FromDate", "");
                param[1] = new ReportParameter("ToDate", "");
                param[2] = new ReportParameter("Location", Convert.ToString(Location));
                param[3] = new ReportParameter("DeptName", Convert.ToString(DeptName));
                param[4] = new ReportParameter("SkillSet", Convert.ToString(Skillset));
                param[5] = new ReportParameter("Status", Convert.ToString(RecStatus));
                param[6] = new ReportParameter("Recruiter", Convert.ToString(Recruiter));
                param[7] = new ReportParameter("Urgency", Convert.ToString(UrgencyCritiName));

                ReportViewer1.ProcessingMode = ProcessingMode.Local;

                if (RBLWithInterview.Checked == true) 
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/Req_Detail_AbstractCandidate_Report.rdlc");
                }
                else
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/Req_Detail_AbstractCandidate_ReportWithout.rdlc");
                }

                // ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/Req_Detail_AbstractCandidate_Report.rdlc");
                ReportDataSource rds = new ReportDataSource("RecruiterDetails", DSDetailReport.Tables[0]);
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

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        lstPositionDept.SelectedIndex = -1;
        lstPositionCriti.SelectedIndex = -1;
        lstPositionLoca.SelectedIndex = -1;
        DDLStatus.SelectedIndex = -1;
        DDLSkillSet.SelectedIndex = -1;
        lstRecruiter.SelectedIndex = -1;
        ReportViewer1.Visible = false;
        RBLWithInterview.Checked = true;
        RBLWithOutInterview.Checked = false;
    }

    protected void RBLWithInterview_CheckedChanged(object sender, EventArgs e)
    {
        RBLWithOutInterview.Checked = false;
    }

    protected void RBLWithOutInterview_CheckedChanged(object sender, EventArgs e)
    {
        RBLWithInterview.Checked = false;
    }
}