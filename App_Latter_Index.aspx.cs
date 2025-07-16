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

public partial class procs_App_Latter_Index : System.Web.UI.Page
{
	SqlConnection source;
	public SqlDataAdapter sqladp;
	
	public string userid;
	public string loc = "", dept = "", subdept = "", desg = "";
	public int did = 0;
	SP_Methods spm = new SP_Methods();
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
			Session["chkbtnStatus_Appr"] = "";
			hflEmpCode.Value = Convert.ToString(Session["Empcode"]);
			lblmsg.Visible = false;

			//  lblmsg.Text =Convert.ToString(Session["Empcode"]); 
			if (Page.User.Identity.IsAuthenticated == false)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
			}
			else
			{
				Page.SmartNavigation = true;
				if (!Page.IsPostBack)
				{
					CheckIsAdminAccess();
					Inbox_Mode_PendingRecord();
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}
	protected void CheckIsAdminAccess()
	{
		try
		{
			DataTable dtEmployee = new DataTable();
			dtEmployee = spm.Get_APP_Employee_Details("Moderation_Employee_Index", "SP_APP_Employee_Details", hflEmpCode.Value);
			if (dtEmployee.Rows.Count > 0)
			{
				lnk_Board.Visible = true;
				lnk_Degree.Visible = true;
				isShowAdmin.Visible = true;
			}

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}
	protected void Inbox_Mode_PendingRecord()
	{
		try
		{
			DataTable dtEmployee = new DataTable();
			dtEmployee = spm.Get_APP_Employee_Details("Moderation_Pending_Count", "SP_APP_Employee_Details", hflEmpCode.Value);
			if (dtEmployee.Rows.Count > 0)
			{
				lnk_Board.Text = "Inbox(" + dtEmployee.Rows.Count + ")";
			}
			
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}
}