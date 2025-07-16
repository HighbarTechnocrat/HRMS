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

using System.Linq;

public partial class CustEscalationView_Index : System.Web.UI.Page
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

					
					loadDropDownDepartment();
					loadImpactProject();
					loadSeverity();
					loadCustSatisfaction();
					loadIncidentOwner();
					loadStatusName();
					loadIncidentNo();
					GetFilterCustomerFirst();
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}
	public void GetFilterCustomerFirst()
	{
		DataTable dtRequisitionDetails = new DataTable();
		string ProjectID = string.Empty, SeverityID = string.Empty, CustSatisfactionID = string.Empty, ImpactProjectID = string.Empty,
		StatusID = string.Empty, IncidentOwner = string.Empty, IncidentID=String.Empty;		
		lblmessage.Text = "";

		ProjectID = String.Join(",", ddlProjectName.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
		SeverityID = String.Join(",", ddlSeverity.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
		CustSatisfactionID = String.Join(",", ddlCustSatisfaction.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
		ImpactProjectID = String.Join(",", ddlImpactProject.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
		StatusID = String.Join(",", ddlStatus.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
		IncidentOwner = String.Join(",", ddlIncidentOwner.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
		IncidentID = String.Join(",", lstIncidentNo.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());

		try
		{
			SqlParameter[] spars = new SqlParameter[10];
			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = "GETCUSTESCALATIONSEARCH";
			spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(Session["Empcode"]);
			spars[2] = new SqlParameter("@ProjectID", SqlDbType.VarChar);
			spars[2].Value = ProjectID;
			spars[3] = new SqlParameter("@SeverityID", SqlDbType.VarChar);
			spars[3].Value = SeverityID;
			spars[4] = new SqlParameter("@CustSatisfactionID", SqlDbType.VarChar);
			spars[4].Value = CustSatisfactionID;
			spars[5] = new SqlParameter("@ImpactProjectID", SqlDbType.VarChar);
			spars[5].Value = ImpactProjectID;
			spars[6] = new SqlParameter("@StatusID", SqlDbType.VarChar);
			spars[6].Value = StatusID;
			spars[7] = new SqlParameter("@IncidentOwner", SqlDbType.VarChar);
			spars[7].Value = IncidentOwner;
			spars[8] = new SqlParameter("@IncidentID", SqlDbType.VarChar);
			spars[8].Value = IncidentID;
			dtRequisitionDetails = spm.getMobileRemDataList(spars, "SP_CUSTOMER_ESCALATION_REPORT");
			gvMngTravelRqstList.DataSource = null;
			gvMngTravelRqstList.DataBind();
			if (dtRequisitionDetails.Rows.Count > 0)
			{
				gvMngTravelRqstList.DataSource = dtRequisitionDetails;
				gvMngTravelRqstList.DataBind();

			}
			else
			{
				lblmessage.Text = "Record not available";
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}
	//Use Create Sevice Request
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

	public void loadIncidentNo()
	{
		DataTable dtleaveInbox = new DataTable();
		try
		{
			SqlParameter[] spars = new SqlParameter[2];
			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = "GETCUSTESCALATIONINCIDENT";
			//spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			//spars[1].Value = Convert.ToString(Session["Empcode"]);
			dtleaveInbox = spm.getMobileRemDataList(spars, "SP_CUSTOMER_ESCALATION_REPORT");
			if (dtleaveInbox.Rows.Count > 0)
			{
				lstIncidentNo.DataSource = dtleaveInbox;
				lstIncidentNo.DataTextField = "ServicesRequestID";
				lstIncidentNo.DataValueField = "Id";
				lstIncidentNo.DataBind();
				//ddlStatus.Items.Insert(0, new ListItem("Select Status", "0")); //updated code
			}

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
		
	}







	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		try
		{
			GetFilterCustomerFirst();
		}
		catch (Exception ex)
		{
		}
	}
	protected void lnkFuelDetails_Click(object sender, EventArgs e)
	{
		ImageButton btn = (ImageButton)sender;
		GridViewRow row = (GridViewRow)btn.NamingContainer;
		string ID = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
		Response.Redirect("~/procs/CustEscalationView.aspx?id=" + ID + "&type=View");
	}
	protected void mobile_btnBack_Click(object sender, EventArgs e)
	{
		ddlProjectName.SelectedIndex = -1;
		ddlSeverity.SelectedIndex = -1;
		ddlCustSatisfaction.SelectedIndex = -1;
		ddlStatus.SelectedIndex = -1;
		ddlIncidentOwner.SelectedIndex = -1;
		ddlImpactProject.SelectedIndex = -1;
		lstIncidentNo.SelectedIndex = -1;
		lblmessage.Text = "";
		GetFilterCustomerFirst();
	}
	protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		gvMngTravelRqstList.PageIndex = e.NewPageIndex;
		this.GetFilterCustomerFirst();
	}
}