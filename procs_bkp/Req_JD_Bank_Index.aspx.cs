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
public partial class Req_JD_Bank_Index : System.Web.UI.Page
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
	#region PageEvent
	protected void Page_Load(object sender, EventArgs e)
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
				// Page.SmartNavigation = true;
				strempcode = Session["Empcode"].ToString();
				if (!Page.IsPostBack)
				{
					editform.Visible = true;
					GetSkillsetName();
					GetPositionName();
					GetJDBankDetail();

					this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}
	protected void gvMngTravelRqstList_RowCommand(object sender, GridViewCommandEventArgs e)
	{

	}
	protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
	{
		try
		{
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			hdnQuest_ID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
			Response.Redirect("Req_JD_Bank_create.aspx?Quest_ID=" + hdnQuest_ID.Value);
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}
	#endregion
	#region PageMethod
	private void GetJDBankDetail()
	{
		int Quest_ID = 0;
		DataTable JDList = new DataTable();
		string Stype = "getAssignJDBank_Index";
		lblmessage.Text = "";
		JDList = spm.GetAssign_JDBankFilter(Stype, Convert.ToInt32(lstSkillset.SelectedValue), Convert.ToInt32(lstPositionName.SelectedValue));
		gvMngTravelRqstList.DataSource = null;
		gvMngTravelRqstList.DataBind();
		if (JDList.Rows.Count > 0)
		{
			gvMngTravelRqstList.DataSource = JDList;
			gvMngTravelRqstList.DataBind();

		}
		else
		{
			lblmessage.Text = "Record not available";
		}
	}
	public void GetSkillsetName()
	{
		DataTable dtSkillset = new DataTable();
		dtSkillset = spm.GetRecruitment_SkillsetName();
		if (dtSkillset.Rows.Count > 0)
		{
			lstSkillset.DataSource = dtSkillset;
			lstSkillset.DataTextField = "ModuleDesc";
			lstSkillset.DataValueField = "ModuleId";
			lstSkillset.DataBind();
			lstSkillset.Items.Insert(0, new ListItem("Select Skillset", "0"));

		}
	}
	public void GetPositionName()
	{
		DataTable dtPositionName = new DataTable();
		dtPositionName = spm.GetRecruitment_PositionTitle();
		if (dtPositionName.Rows.Count > 0)
		{
			lstPositionName.DataSource = dtPositionName;
			lstPositionName.DataTextField = "PositionTitle";
			lstPositionName.DataValueField = "PositionTitle_ID";
			lstPositionName.DataBind();
			lstPositionName.Items.Insert(0, new ListItem("Select Position", "0"));
		}
	}
	#endregion
	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		GetJDBankDetail();
	}
	

	protected void mobile_cancel_Click(object sender, EventArgs e)
	{
		lstPositionName.SelectedIndex = -1;
		lstSkillset.SelectedIndex = -1;
		GetJDBankDetail();
	}




	protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		gvMngTravelRqstList.PageIndex = e.NewPageIndex;
		GetJDBankDetail();
	}
}

