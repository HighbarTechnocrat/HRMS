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

public partial class InboxCustEscalationApp : System.Web.UI.Page
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
	public DataTable dtAssign = new DataTable();
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

	#region Page_Events

	private void Page_Load(object sender, System.EventArgs e)
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
				Page.SmartNavigation = true;
				hdnEmpCode.Value = Convert.ToString(Session["Empcode"]);
				if (!Page.IsPostBack)
				{
					editform.Visible = true;
					if (Request.QueryString.Count > 0)
					hdninboxtype.Value = Convert.ToString(Request.QueryString[0]);				
					InboxMobileRemReqstList();					
					this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}

	protected void lnkFuelDetails_Click(object sender, EventArgs e)
	{
		ImageButton btn = (ImageButton)sender;
		GridViewRow row = (GridViewRow)btn.NamingContainer;
		hdnRemid.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();		
		Response.Redirect("~/procs/CustEscalation_Req_Reject.aspx?id=" + hdnRemid.Value + "&type=Rej");
	}

	#endregion

	#region PageMethods
	private void InboxMobileRemReqstList()
	{
		try
		{			
			DataTable dtleaveInbox = new DataTable();
			dtleaveInbox = spm.GetCustEscalation_Approval("EscalatedApproval", hdnEmpCode.Value);

			if (dtleaveInbox.Rows.Count > 0)
			{
				dtAssign = new DataTable();
				Createtable();
				DataRow dr;
				foreach (DataRow row in dtleaveInbox.Rows)
				{
					int AUTO_ESCALATIONDay = 8;
					var diffDay = 0.00;
					var Id = Convert.ToInt32(row["Id"].ToString());
					//var EmpCode = Convert.ToString(row["EmpCode"].ToString());
					var ServicesRequestID = Convert.ToString(row["ServicesRequestID"].ToString());
					var ServiceDepartment = Convert.ToString(row["ServiceDepartment"].ToString());
					var AssignedDate = Convert.ToDateTime(row["AssignedDates"].ToString()).Date;
					var todayDate = DateTime.Now.Date;
					string officeLocation = "HO-NaviMum";
					var getDayesdt = spm.GetCustEscalationServiceCount(officeLocation, AssignedDate, todayDate);
					if (getDayesdt.Rows.Count > 0)
					{
						diffDay = Convert.ToInt32(getDayesdt.Rows[0]["WORKINGDAY"]);
					}
					else
					{
						diffDay = (todayDate - AssignedDate).TotalDays;
					}
					DataTable dtSPOC = spm.GetCustEscalationSPOCData(ServiceDepartment);
					if (dtSPOC.Rows.Count > 0)
					{
						AUTO_ESCALATIONDay = Convert.ToInt32(dtSPOC.Rows[0]["USER_ESCALATION"]);
					}

					if (diffDay <= AUTO_ESCALATIONDay)
					{
						dr = dtAssign.NewRow();
						dr["Id"] = Convert.ToString(row["Id"].ToString());
						dr["ServicesRequestID"] = Convert.ToString(row["ServicesRequestID"].ToString());
						dr["ServiceRequestDate"] = Convert.ToString(row["ServiceRequestDate"].ToString());
						dr["EmployeeName"] = Convert.ToString(row["EmployeeName"].ToString());
						dr["AssignedTo"] = Convert.ToString(row["AssignedTo"].ToString());
						dr["AssignmentDate"] = Convert.ToString(row["AssignmentDate"].ToString());
						dr["EscalationRaisedBy"] = Convert.ToString(row["EscalationRaisedBy"].ToString());
						dr["Location_name"] = Convert.ToString(row["Location_name"].ToString());
						dr["IncidentOwner"] = Convert.ToString(row["IncidentOwner"].ToString());
						dr["DeliveryDate"] = Convert.ToString(row["DeliveryDate"].ToString());
						dr["Status"] = Convert.ToString(row["Status"].ToString());
						dtAssign.Rows.Add(dr);						
					}	
				}
				if (dtAssign.Rows.Count > 0)
				{
					gvMngTravelRqstList.DataSource = dtAssign;
					gvMngTravelRqstList.DataBind();
				}

			}

		}
		catch (Exception ex)
		{

		}
	}
	public void Createtable()
	{
		try
		{
		dtAssign.Columns.Add("Id", typeof(string));
		dtAssign.Columns.Add("ServicesRequestID", typeof(string));
		dtAssign.Columns.Add("ServiceRequestDate", typeof(string));
		dtAssign.Columns.Add("EmployeeName", typeof(string));
		dtAssign.Columns.Add("AssignedTo", typeof(string));
		dtAssign.Columns.Add("AssignmentDate", typeof(string));
		dtAssign.Columns.Add("Status", typeof(string));
		dtAssign.Columns.Add("EscalationRaisedBy", typeof(string));
		dtAssign.Columns.Add("Location_name", typeof(string));
		dtAssign.Columns.Add("IncidentOwner", typeof(string));
		dtAssign.Columns.Add("DeliveryDate", typeof(string));
		}
		catch (Exception)
		{

			throw;
		}
	}



	#endregion



	protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		gvMngTravelRqstList.PageIndex = e.NewPageIndex;
		this.InboxMobileRemReqstList();
	}
}