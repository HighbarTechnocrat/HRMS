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

public partial class procs_Insurance_Reminder_Report : System.Web.UI.Page
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
					GetInsuranceType();
					GetStatusName();
					GetResponsibleEmployee();				
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}

	public void GetInsuranceType()
	{
		DataTable dtPositionCriti = new DataTable();
		SqlParameter[] spars = new SqlParameter[9];
		spars[0] = new SqlParameter("@Stype", SqlDbType.NVarChar);
		spars[0].Value = "POLICYTYPE";
		dtPositionCriti = spm.getMobileRemDataList(spars, "SP_Get_Insurance_Reminder_Report");
		if (dtPositionCriti.Rows.Count > 0)
		{
			lstinsuranceType.DataSource = dtPositionCriti;
			lstinsuranceType.DataTextField = "PolicyType";
			lstinsuranceType.DataValueField = "PolicyTypeId";
			lstinsuranceType.DataBind();
			lstinsuranceType.Items.Insert(0, new ListItem("Select Type", "0"));
		}
	}
	public void GetStatusName()
	{
		DataTable dtPositionDept = new DataTable();
		SqlParameter[] spars = new SqlParameter[9];
		spars[0] = new SqlParameter("@Stype", SqlDbType.NVarChar);
		spars[0].Value = "POLICYSTATUS";
		dtPositionDept = spm.getMobileRemDataList(spars, "SP_Get_Insurance_Reminder_Report");
		if (dtPositionDept.Rows.Count > 0)
		{
			lstStatus.DataSource = dtPositionDept;
			lstStatus.DataTextField = "PolicyStatus";
			lstStatus.DataValueField = "PolicyStatusId";
			lstStatus.DataBind();
			lstStatus.Items.Insert(0, new ListItem("Select Status", "0"));
		}
	}

	public void GetResponsibleEmployee()
	{
		DataTable dtPositionEmp = new DataTable();
		SqlParameter[] spars = new SqlParameter[9];	
		spars[0] = new SqlParameter("@Stype", SqlDbType.NVarChar);
		spars[0].Value = "RESPONSEBLEEMP";
		dtPositionEmp = spm.getMobileRemDataList(spars, "SP_Get_Insurance_Reminder_Report");
		if (dtPositionEmp.Rows.Count > 0)
		{
			lstResponsibleEmp.DataSource = dtPositionEmp;
			lstResponsibleEmp.DataTextField = "Emp_Name";
			lstResponsibleEmp.DataValueField = "ResponsiblePerson";
			lstResponsibleEmp.DataBind();
			lstResponsibleEmp.Items.Insert(0, new ListItem("Select Employee", "0"));

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
			int DeptID = 0, PositionCritiID = 0;
			var ddlInsuranceType = "";
			var ddlStatus = "";
			var ddlResponsibleEmp = "";
			var InsuranceType = "";
			var EmpName = "";
			var ddlStatusName = "";
			var InsuranceType1 = "";
			var EmpName1 = "";
			var ddlStatusName1 = "";
			var isSelected = false;
			//DDL InsuranceType
			foreach (ListItem item in lstinsuranceType.Items)
			{
				if (item.Selected)
				{
					if (item.Value != "" && item.Value != "0")
					{
						isSelected = true;
						if (ddlInsuranceType == "")
						{
							ddlInsuranceType = item.Value;
							InsuranceType = item.Text;
						}
						else
						{
							ddlInsuranceType = ddlInsuranceType + "|" + item.Value;
							InsuranceType = InsuranceType + "," + item.Text;
						}
					}
				}
			}
			//DDl Status Name
			foreach (ListItem item in lstStatus.Items)
			{
				if (item.Selected)
				{
					if (item.Value != "" && item.Value != "0")
					{
						isSelected = true;
						if (ddlStatus == "")
						{
							ddlStatus = item.Value;
							ddlStatusName = item.Text;
						}
						else
						{
							ddlStatus = ddlStatus + "|" + item.Value;
							ddlStatusName = ddlStatusName + "," + item.Text;
						}
					}
				}
			}
			// ddl Responsible Emp
			foreach (ListItem item in lstResponsibleEmp.Items)
			{
				if (item.Selected)
				{
					if (item.Value != "" && item.Value != "0")
					{
						isSelected = true;
						if (ddlResponsibleEmp == "")
						{
							ddlResponsibleEmp = item.Value;
							EmpName = item.Text;
						}
						else
						{
							ddlResponsibleEmp = ddlResponsibleEmp + "|" + item.Value;
							EmpName = EmpName + "," + item.Text;
						}
					}
				 }
			}
			if (InsuranceType!="" && !InsuranceType.Contains(","))
			{
				InsuranceType1 = "Type : " + InsuranceType;
			}
			if (ddlStatusName!="" && !ddlStatusName.Contains(","))
			{
				ddlStatusName1 = "Status : " + Convert.ToString(ddlStatusName);
			}
			if (EmpName != "" && !EmpName.Contains(","))
			{
				EmpName1 = "Responsible Employee : " + Convert.ToString(EmpName);
			}

			SqlParameter[] spars = new SqlParameter[5];
			spars[0] = new SqlParameter("@ddlInsuranceTypeID", SqlDbType.NVarChar);
			spars[0].Value = ddlInsuranceType;
			spars[1] = new SqlParameter("@ddlStatusID", SqlDbType.NVarChar);
			spars[1].Value = ddlStatus;
			spars[2] = new SqlParameter("@ResponsibleEmp", SqlDbType.NVarChar);
			spars[2].Value = ddlResponsibleEmp;
			spars[3] = new SqlParameter("@Stype", SqlDbType.NVarChar);
			spars[3].Value = "SELECTINSURANCEREROT";
			spars[4] = new SqlParameter("@EmpCode", SqlDbType.NVarChar);
			spars[4].Value = Convert.ToString(Session["Empcode"]);
			
			DSDetailReport = spm.getServiceRequestReportCount(spars, "SP_Get_Insurance_Reminder_Report");
			#endregion
			if (DSDetailReport.Tables[0].Rows.Count > 0)
			{
				ReportViewer1.Visible = true;
				ReportViewer1.LocalReport.Refresh();
				ReportViewer1.LocalReport.DataSources.Clear();
				ReportParameter[] param = new ReportParameter[3];
				param[0] = new ReportParameter("InsuranceType", InsuranceType1);
				param[1] = new ReportParameter("Status", ddlStatusName1);
				param[2] = new ReportParameter("EmpName", Convert.ToString(EmpName1));
				
				ReportViewer1.ProcessingMode = ProcessingMode.Local;
				ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/Insurance_Reminder_Reports.rdlc");
				ReportDataSource rds = new ReportDataSource("insurancereminder", DSDetailReport.Tables[0]);
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
		lstinsuranceType.SelectedIndex = -1;
		lstStatus.SelectedIndex = -1;
		lstResponsibleEmp.SelectedIndex = -1;		
		ReportViewer1.Visible = false;
	}
}