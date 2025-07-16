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


public partial class procs_App_Latter_M_Index : System.Web.UI.Page
{

	#region Creative_Default_methods
	SqlConnection source;
	public SqlDataAdapter sqladp;
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
					if (Request.QueryString.Count > 0)
					{
						hdnInboxType.Value = Convert.ToString(Request.QueryString[0]).Trim();
					}
					if (Convert.ToString(hdnInboxType.Value).Trim() == "APP")
					{
						GetFilter_App_M_Index("Moderation_Apporval_List");
						lblheading.Text = "Appointment Latter Approval list";
					}
					if (Convert.ToString(hdnInboxType.Value).Trim() == "Pending")
					{
						GetFilter_App_M_Index("Moderation_Pending_List");
						lblheading.Text = "Inbox Appointment Latter Approval";
					}
					this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}
	protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
	{
		try
		{
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			hdnOffer_App_ID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
			//string StatusName = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
			//string Rec_ID = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
			Response.Redirect("App_Latter_Approval.aspx?Appointment_ID=" + hdnOffer_App_ID.Value + "&Type=Pending");
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}


	protected void lnkView_Click(object sender, ImageClickEventArgs e)
	{
		try
		{
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			hdnOffer_App_ID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
			//string Rec_ID = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
			Response.Redirect("App_Latter_Approval.aspx?Appointment_ID=" + hdnOffer_App_ID.Value + "&Type=View");
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}

	protected void gvMngTravelRqstList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			ImageButton imgBtn = (ImageButton)e.Row.FindControl("lnkView");
			ImageButton imgBtn1 = (ImageButton)e.Row.FindControl("lnkEdit");
			if (Convert.ToString(hdnInboxType.Value).Trim() == "APP")
			{
				imgBtn.Visible = true;
				imgBtn1.Visible = false;
			}
			else
			{
				imgBtn1.Visible = true;
				imgBtn.Visible = false;
			}
		}
	}

	protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		gvMngTravelRqstList.PageIndex = e.NewPageIndex;
		if (Convert.ToString(hdnInboxType.Value).Trim() == "APP")
		{
			GetFilter_App_M_Index("Moderation_Apporval_List");
			lblheading.Text = "Appointment Latter Approval list";
		}
		if (Convert.ToString(hdnInboxType.Value).Trim() == "Pending")
		{
			GetFilter_App_M_Index("Moderation_Pending_List");
		}
	}

	
	public void GetFilter_App_M_Index(string Stype)
	{
		DataTable dtRequisitionDetails = new DataTable();
		lblmessage.Text = "";
		try
		{
			SqlParameter[] spars = new SqlParameter[7];
			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = Stype;
			spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(Session["Empcode"]);
			//spars[2] = new SqlParameter("@ModuleId", SqlDbType.Int);
			//spars[2].Value = lstSkillset.SelectedValue;
			dtRequisitionDetails = spm.getMobileRemDataList(spars, "SP_APP_Employee_Details");
			gvMngTravelRqstList.DataSource = null;
			gvMngTravelRqstList.DataBind();
			if (dtRequisitionDetails.Rows.Count > 0)
			{
				gvMngTravelRqstList.DataSource = dtRequisitionDetails;
				gvMngTravelRqstList.DataBind();
			}
			else
			{
				lblmessage.Text = "Record not found !.";
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}

	
}