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

public partial class procs_My_Attendance_List : System.Web.UI.Page
{
	public SqlDataAdapter sqladp;
	public static string dob1;
	public string userid;
	public static string pimg = "";
	public static string cimg = "";
	public string loc = "", dept = "", subdept = "", desg = "", Wsch = "";
	public int did = 0;
	LeaveBalance bl = new LeaveBalance();
	SP_Methods spm = new SP_Methods();
	Leave_Request_Parameters lpm = new Leave_Request_Parameters();
	public DataTable dtEmp, dtleavebal, dtApprover,  dtleavedetails;
	
	public string  message;
	protected void lnkhome_Click(object sender, EventArgs e)
	{
		Response.Redirect(ReturnUrl("sitepathmain") + "default");
	}
	public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
			}

			lblmessage.Text = "";

			if (Convert.ToString(Session["Empcode"]).Trim() == "")
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

			lpm.Emp_Code = Session["Empcode"].ToString();
			lblmessage.Visible = true;
			if (Page.User.Identity.IsAuthenticated == false)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
			}
			else
			{
				Page.SmartNavigation = true;
				if (!Page.IsPostBack)
				{
					if (Request.QueryString.Count > 0)
					{
						hdnReqid.Value = Convert.ToString(Request.QueryString[0]).Trim();
					}
				
					editform.Visible = true;
					divbtn.Visible = false;
					lblmessage.Text = "";
					this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
					getAttTimeSheet();
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}
	#region page Methods
	private void getAttTimeSheet()
	{
		DgvApprover.DataSource = null;
		DgvApprover.DataBind();
		SqlParameter[] spars = new SqlParameter[2];
		spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
		spars[0].Value = "Emp_My_Att_List";
		spars[1] = new SqlParameter("@Emp_code", SqlDbType.VarChar);
		if (lpm.Emp_Code.ToString() == "")
			spars[1].Value = DBNull.Value;
		else
			spars[1].Value = lpm.Emp_Code.ToString();

		DataTable dt = spm.getDropdownList(spars, "SP_Attendance");
		if (dt.Rows.Count > 0)
		{
			DgvApprover.DataSource = dt;
			DgvApprover.DataBind();
		}
	}
	#endregion

}