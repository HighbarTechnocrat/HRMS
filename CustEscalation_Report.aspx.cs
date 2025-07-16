using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
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
using System.Data;
using Microsoft.Reporting.WebForms;


public partial class procs_CustEscalation_Report : System.Web.UI.Page
{
	#region CreativeMethods
	SqlConnection source;
	public SqlDataAdapter sqladp;
	public static string dob1;
	public string userid;
	public DataSet dtDeptReport;
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

					//GetHODDept();
					loadDropDownDepartment();					
					loadImpactProject();
					loadSeverity();					
					loadCustSatisfaction();
					loadIncidentOwner();
					loadStatusName();
					txtfromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					txttodate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}//Use Create Sevice Request
	public void loadDropDownDepartment()
	{
		DataTable dtleaveInbox = new DataTable();
		dtleaveInbox = spm.GetCustEscalationDepartment("DDLDEPARTMENT");
		ddlProjectName.DataSource = dtleaveInbox;
		ddlProjectName.DataTextField = "DepartmentName";
		ddlProjectName.DataValueField = "DepartmentId";
		ddlProjectName.DataBind();
		//ddlProjectName.Items.Insert(0, new ListItem("Select Project", "0")); //updated code
	}
	
	
	public void loadSeverity()
	{
		DataTable dtleaveInbox = new DataTable();
		dtleaveInbox = spm.GetCustEscalationDepartment("DDLSEVERITY");
		ddlSeverity.DataSource = dtleaveInbox;
		ddlSeverity.DataTextField = "Severity_Name";
		ddlSeverity.DataValueField = "Severity_ID";
		ddlSeverity.DataBind();
		//ddlSeverity.Items.Insert(0, new ListItem("Select Severity", "0")); //updated code
	}
	public void loadImpactProject()
	{
		DataTable dtleaveInbox = new DataTable();
		dtleaveInbox = spm.GetCustEscalationDepartment("DDLIMPACTPROJECT");
		ddlImpactProject.DataSource = dtleaveInbox;
		ddlImpactProject.DataTextField = "Cust_Impact_Name";
		ddlImpactProject.DataValueField = "Cust_Impact_ID";
		ddlImpactProject.DataBind();
		//ddlImpactProject.Items.Insert(0, new ListItem("Select Impact Project", "0")); //updated code
	}

	public void loadCustSatisfaction()
	{
		DataTable dtleaveInbox = new DataTable();
		dtleaveInbox = spm.GetCustEscalationDepartment("DDLCUSTSATISFACTION");
		ddlCustSatisfaction.DataSource = dtleaveInbox;
		ddlCustSatisfaction.DataTextField = "Cust_Satisfaction_Name";
		ddlCustSatisfaction.DataValueField = "Cust_Satisfaction_ID";
		ddlCustSatisfaction.DataBind();
		//ddlCustSatisfaction.Items.Insert(0, new ListItem("Select Cust Satisfaction Index", "0")); //updated code
	}
	public void loadIncidentOwner()
	{
		DataTable dtleaveInbox = new DataTable();
		dtleaveInbox = spm.GetCustEscalationDepartment("DDLINCIDENTOWNER");
		ddlIncidentOwner.DataSource = dtleaveInbox;
		ddlIncidentOwner.DataTextField = "Emp_Name";
		ddlIncidentOwner.DataValueField = "Emp_Code";
		ddlIncidentOwner.DataBind();
		//ddlIncidentOwner.Items.Insert(0, new ListItem("Select Incident Owner", "0")); //updated code
	}

	public void loadStatusName()
	{
		DataTable dtleaveInbox = new DataTable();
		dtleaveInbox = spm.GetCustEscalationDepartment("DDLSTATUSLISTReport");
		ddlStatus.DataSource = dtleaveInbox;
		ddlStatus.DataTextField = "StatusTitle";
		ddlStatus.DataValueField = "Id";
		ddlStatus.DataBind();
		//ddlStatus.Items.Insert(0, new ListItem("Select Status", "0")); //updated code
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
				//lstPositionDept.DataSource = dtEmployee;
				//lstPositionDept.DataTextField = "Department_Name";
				//lstPositionDept.DataValueField = "Department_id";
				//lstPositionDept.DataBind();
				//lstPositionDept.Items.Insert(0, new ListItem("Select Department", "0"));
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

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
	

	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		try
		{
			#region get Rec Dept details
			string[] fromdate, todate;
			string  ReportName = string.Empty;
			string StartDate = "", EndDate = "", Stype = "GETCUSTESCALATIONREPORT", PositionCritiName = "", DeptName = "", Location = "", Recruiter = "";
		
			string ProjectID = string.Empty, SeverityID = string.Empty, CustSatisfactionID = string.Empty, ImpactProjectID = string.Empty,
				StatusID = string.Empty, IncidentOwner = string.Empty;
			lblmessage.Text = ""; string strToDate_RPt = string.Empty;
			ReportViewer1.Visible = false;
			//if (Convert.ToString(ddlProjectName.SelectedValue).Trim() == "0" || Convert.ToString(ddlProjectName.SelectedItem.Text).Trim() == "Select Project")
			//{
			//	lblmessage.Text = "Please Select Report By ";
			//	return;
			//}

			string confirmValue = hdnYesNo.Value.ToString();
			
			ProjectID = String.Join(",", ddlProjectName.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
			SeverityID = String.Join(",", ddlSeverity.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
			CustSatisfactionID = String.Join(",", ddlCustSatisfaction.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
			ImpactProjectID = String.Join(",", ddlImpactProject.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
			StatusID = String.Join(",", ddlStatus.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
			IncidentOwner = String.Join(",", ddlIncidentOwner.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());			
			dtDeptReport = spm.GetCustEscalation_DetailsReport(Stype, ProjectID, SeverityID, CustSatisfactionID, ImpactProjectID, StatusID, IncidentOwner, Convert.ToString(Session["Empcode"]));

			#endregion
			if (dtDeptReport.Tables[0].Rows.Count > 0)
			{
				string ProjectName = string.Empty, Severity = string.Empty;
				ReportViewer1.Visible = true;
				ReportViewer1.LocalReport.Refresh();
				ReportViewer1.LocalReport.DataSources.Clear();

				if (ProjectID != "")
				{
					if (!ProjectID.ToLower().Contains(','))
					{
						ProjectName ="Project Name : "+ ddlProjectName.SelectedItem.Text;
					}
				}
				if (SeverityID != "")
				{
					if (!SeverityID.ToLower().Contains(','))
					{
						ProjectName = ProjectName + " Severity : " + ddlSeverity.SelectedItem.Text;
					}
				}
				if (CustSatisfactionID != "")
				{
					if (!CustSatisfactionID.ToLower().Contains(','))
					{
						ProjectName = ProjectName +" Customer Satisfaction Index : " + ddlCustSatisfaction.SelectedItem.Text;
					}
				}
				if (ImpactProjectID != "")
				{
					if (!ImpactProjectID.ToLower().Contains(','))
					{
						Severity = "Impact on Project : " + ddlImpactProject.SelectedItem.Text;
					}
				}
				if (IncidentOwner != "")
				{
					if (!IncidentOwner.ToLower().Contains(','))
					{
						Severity = Severity + " Incident Owner : " + ddlIncidentOwner.SelectedItem.Text;
					}
				}
				if (StatusID != "")
				{
					if (!StatusID.ToLower().Contains(','))
					{
						Severity = Severity + " Status : " + ddlStatus.SelectedItem.Text;
					}
				}

				ReportParameter[] param = new ReportParameter[2];
				param[0] = new ReportParameter("ProjectName", ProjectName);
				param[1] = new ReportParameter("Severity", Severity);

				ReportViewer1.ProcessingMode = ProcessingMode.Local;
				ReportDataSource rds1 = new ReportDataSource();			
				ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/CustEscalation_Summary_Report.rdlc");
				ReportDataSource rds = new ReportDataSource("CustomerFirst", dtDeptReport.Tables[0]);				
				ReportViewer1.LocalReport.DataSources.Clear();
				ReportViewer1.LocalReport.SetParameters(param);
				ReportViewer1.LocalReport.DataSources.Add(rds);
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
		ddlProjectName.SelectedIndex = -1;
		ddlSeverity.SelectedIndex = -1;
		ddlCustSatisfaction.SelectedIndex = -1;
		ddlStatus.SelectedIndex = -1;
		ddlIncidentOwner.SelectedIndex = -1;
		ddlImpactProject.SelectedIndex = -1;
		txtfromdate.Text = "";
		txttodate.Text = "";
		ReportViewer1.Visible = false;
		lblmessage.Text = "";
	}
}