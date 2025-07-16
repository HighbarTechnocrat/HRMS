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
using System.Linq;
using ClosedXML.Excel;

public partial class Appointment_Letter_Issued_Report : System.Web.UI.Page
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
                    GetEmployee();
                    GetSkillsetName();
                    GetEmpStatus();
                    GetAppointmentStatus();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    public void GetEmployee()
    {
        DataTable dtInterviewer = new DataTable();
        dtInterviewer = spm.GetEmployee();
        if (dtInterviewer.Rows.Count > 0)
        {
            lstEmployee.DataSource = dtInterviewer;
            lstEmployee.DataTextField = "EmpName";
            lstEmployee.DataValueField = "Emp_Code";
            lstEmployee.DataBind();
            lstEmployee.Items.Insert(0, new ListItem("Select Employee", "0"));
        }
    }

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

    public void GetEmpStatus()
    {
        DataTable dtInterviewer = new DataTable();
        dtInterviewer = spm.GetEmpStatus();
        if (dtInterviewer.Rows.Count > 0)
        {
            lstEmpStatus.DataSource = dtInterviewer;
            lstEmpStatus.DataTextField = "emp_Status";
            lstEmpStatus.DataValueField = "empStatusId";
            lstEmpStatus.DataBind();
            lstEmpStatus.Items.Insert(0, new ListItem("Select Employee Status", "0"));
        }
    }
   
    public void GetAppointmentStatus()
    {
        DataTable dtInterviewer = new DataTable();
        dtInterviewer = spm.GetAppointmentStatus();
        if (dtInterviewer.Rows.Count > 0)
        {
            lstAppoStatus.DataSource = dtInterviewer;
            lstAppoStatus.DataTextField = "Appointmentletterissuedstatus";
            lstAppoStatus.DataValueField = "AppoStatusId";
            lstAppoStatus.DataBind();
            lstAppoStatus.Items.Insert(0, new ListItem("Select Appointment Letter Issue Status", "0"));
        }
    }

	
   
    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            #region get Rec Dept details
            string[] fromdate, todate;
            string StartDate = "", EndDate = "";
            string Employee = "", Skillset = "", EmpStatus = "", AppointmentStatus = "";
            int SkillsetID = 0, EmpStatusID = 0, AppoStatusID = 0;
            lblmessage.Text = "";
            //ReportViewer1.Visible = false;

            string confirmValue = hdnYesNo.Value.ToString();
            
            SkillsetID = Convert.ToString(lstSkillSet.SelectedValue).Trim() != "" ? Convert.ToInt32(lstSkillSet.SelectedValue) : 0;
            EmpStatusID = Convert.ToString(lstEmpStatus.SelectedValue).Trim() != "" ? Convert.ToInt32(lstEmpStatus.SelectedValue) : 0;
            AppoStatusID = Convert.ToString(lstAppoStatus.SelectedValue).Trim() != "" ? Convert.ToInt32(lstAppoStatus.SelectedValue) : 0;

            StartDate = DateTime.Now.ToString();
            EndDate = DateTime.Now.ToString();
           
            var ddlEmployee = "";
            var ddlSkillSet = "";
            var ddlEmpStatus = "";
            var ddlAppoStatus = "";
            var isSelected = false;

            // ddlEmployee
            foreach (ListItem item in lstEmployee.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        isSelected = true;
                        if (ddlEmployee == "")
                        {
                            ddlEmployee = item.Value;
                            Employee = item.Text;
                        }
                        else
                        {
                            ddlEmployee = ddlEmployee + "|" + item.Value;
                            Employee = Employee + "," + item.Text;
                        }
                    }
                }
            }

            // ddlSkillSet
            foreach (ListItem item in lstSkillSet.Items)
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
            foreach (ListItem item in lstEmpStatus.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        isSelected = true;
                        if (ddlEmpStatus == "")
                        {
                            ddlEmpStatus = item.Value;
                            EmpStatus = item.Text;
                        }
                        else
                        {
                            ddlEmpStatus = ddlEmpStatus + "|" + item.Value;
                            EmpStatus = EmpStatus + "|" + item.Text;
                        }
                    }
                }
            }

            // ddlAppoStatus
            foreach (ListItem item in lstAppoStatus.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        isSelected = true;
                        if (ddlAppoStatus == "")
                        {
                            ddlAppoStatus = item.Value;
                            AppointmentStatus = item.Text;
                        }
                        else
                        {
                            ddlAppoStatus = ddlAppoStatus + "|" + item.Value;
                            AppointmentStatus = AppointmentStatus + "|" + item.Text;
                        }
                    }
                }
                
            }
            
            BindReport(ddlEmployee, ddlSkillSet, EmpStatus, AppointmentStatus, Convert.ToString(Session["Empcode"]));

            #endregion
        }
        catch (Exception ex)
        {
        }
    }
	
	public void BindReport(string Employee,string Skillset,string EmpStatus, string AppointmentStatus,string Empcode)
    {
        try
        {
			DataTable dtRequisitionDetails = new DataTable();
			SqlParameter[] spars = new SqlParameter[7];
			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "AppointmetLetterIssuedRptDetails";
			spars[1] = new SqlParameter("@EmpIDs", SqlDbType.VarChar);
			spars[1].Value = Employee;
			spars[2] = new SqlParameter("@SkillSetIDs", SqlDbType.VarChar);
			spars[2].Value = Skillset;
			spars[3] = new SqlParameter("@EmpStatusIDs", SqlDbType.VarChar);
			spars[3].Value = EmpStatus;
			spars[4] = new SqlParameter("@AppoStatusIDs", SqlDbType.VarChar);
			spars[4].Value = AppointmentStatus;
			spars[5] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[5].Value = Convert.ToString(Session["Empcode"]);
			dtRequisitionDetails = spm.getMobileRemDataList(spars, "SP_AppointmentLetter_Issued_Report");
			if (dtRequisitionDetails.Rows.Count > 0)
			{
				ReportViewer1.Visible = true;
				string Result = string.Empty;
				string Result2 = string.Empty;
				string Result3 = string.Empty;
				string Result4 = string.Empty;
				ReportViewer1.LocalReport.Refresh();
				ReportViewer1.LocalReport.DataSources.Clear();
				if (Employee != "")
				{
					if (!Employee.Contains('|'))
					{
						Result3 = "Employee Name : " + lstEmployee.SelectedItem.Text;
					}
				}
				if (EmpStatus != "")
				{
					if (!EmpStatus.Contains('|'))
					{
						Result4 = "Module Name :" + lstSkillSet.SelectedItem.Text; 
					}
				}
				if (EmpStatus != "")
				{
					if (!EmpStatus.Contains('|'))
					{
						Result = "Employee Status :" + lstEmpStatus.SelectedItem.Text; 
					}
				}
				if (AppointmentStatus != "")
				{
					if (!AppointmentStatus.Contains('|'))
					{
						Result2 = " Appointment Lette  Status : " + lstAppoStatus.SelectedItem.Text;
					}
				}
				ReportParameter[] param = new ReportParameter[3];
				param[0] = new ReportParameter("EmployeeName", Result3);
				param[1] = new ReportParameter("ModuleName", Result4);
				param[2] = new ReportParameter("Status", Convert.ToString(Result + Result));

				ReportViewer1.ProcessingMode = ProcessingMode.Local;
				ReportDataSource rds1 = new ReportDataSource();
				ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Procs/Appointment_Letter_Issued_New_Report.rdlc");
				ReportDataSource rds = new ReportDataSource("AppointmetLett", dtRequisitionDetails);
				ReportViewer1.LocalReport.DataSources.Clear();
				ReportViewer1.LocalReport.SetParameters(param);
				ReportViewer1.LocalReport.DataSources.Add(rds);
				ReportViewer1.LocalReport.Refresh();
			}
			else
			{
				lblmessage.Text = "Record not found..";
				ReportViewer1.Visible = false;
			}

		}
        catch (Exception ex)
        {
            
        }
    }
}