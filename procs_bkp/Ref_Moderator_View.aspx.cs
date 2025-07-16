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

public partial class procs_Ref_Moderator_View : System.Web.UI.Page
{
	#region Creative_Default_methods
	SqlConnection source;
	public SqlDataAdapter sqladp;
	public DataTable dtmainSkillSet;
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

					if (Request.QueryString.Count > 0)
					{
						hdnInboxType.Value = Convert.ToString(Request.QueryString[0]).Trim();
					}
					getMainSkillset();
					getCandidateName();
					getReferredBy();
					getStatusName();
					GetRef_CandidatePeindingList();
					this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}

	#endregion
	#region PageMethod
	private void GetRef_CandidatePeindingList()
	{
		try
		{
			DataTable RequisitionList = new DataTable();
			int ModuleId = 0, Ref_Candidate_ID = 0, Empgender = 0,StatusID=0;
			string ReferedBy = "";
			lblmessage.Text = "";
			ModuleId = Convert.ToString(lstMainSkillset.SelectedValue).Trim() != "" ? Convert.ToInt32(lstMainSkillset.SelectedValue) : 0;
			Empgender = Convert.ToString(lstCandidategender.SelectedValue).Trim() != "" ? Convert.ToInt32(lstCandidategender.SelectedValue) : 0;
			Ref_Candidate_ID = Convert.ToString(lstCandidateName.SelectedValue).Trim() != "" ? Convert.ToInt32(lstCandidateName.SelectedValue) : 0;
			ReferedBy = Convert.ToString(lstEmployeeName.SelectedValue).Trim() != "" ? Convert.ToString(lstEmployeeName.SelectedValue) : "";
			StatusID = Convert.ToString(lstStatusName.SelectedValue).Trim() != "" ? Convert.ToInt32(lstStatusName.SelectedValue) : 0;

			if (Convert.ToString(Session["Empcode"]).Trim() != "" || Session["Empcode"] != null)
			{
				RequisitionList = spm.GetRef_ModeratorList(Session["Empcode"].ToString(), "ApporvalList", ModuleId, Empgender, Ref_Candidate_ID, ReferedBy,StatusID);
			}
			RecordCount.Text = "";
			gvMngTravelRqstList.DataSource = null;
			gvMngTravelRqstList.DataBind();
			if (RequisitionList.Rows.Count > 0)
			{
				RecordCount.Text = "Record Count : " + Convert.ToString(RequisitionList.Rows.Count);
				gvMngTravelRqstList.DataSource = RequisitionList;
				gvMngTravelRqstList.DataBind();
			}
			else
			{
				lblmessage.Text = "Record's not found.!";
			}

		}
		catch (Exception ex)
		{

		}
	}
	private void getMainSkillset()
	{
		try
		{
			dtmainSkillSet = spm.GetRef_CandidatedList("", "SelectSkillSet");
			lstMainSkillset.DataSource = dtmainSkillSet;
			lstMainSkillset.DataTextField = "ModuleDesc";
			lstMainSkillset.DataValueField = "ModuleId";
			lstMainSkillset.DataBind();
			lstMainSkillset.Items.Insert(0, new ListItem("Select SkillSet", ""));
		}
		catch (Exception)
		{

		}

	}
	private void getReferredBy()
	{
		try
		{
			dtmainSkillSet = new DataTable();
			dtmainSkillSet = spm.GetRef_CandidatedList("", "SelectApporvalReferredBy");
			lstEmployeeName.DataSource = dtmainSkillSet;
			lstEmployeeName.DataTextField = "Emp_Name";
			lstEmployeeName.DataValueField = "Emp_Code";
			lstEmployeeName.DataBind();
			lstEmployeeName.Items.Insert(0, new ListItem("Select Referred By", ""));
		}
		catch (Exception)
		{

		}
	}
	private void getStatusName()
	{
		try
		{
			dtmainSkillSet = new DataTable();
			dtmainSkillSet = spm.GetRef_CandidatedList("", "SelectStatusName");
			lstStatusName.DataSource = dtmainSkillSet;
			lstStatusName.DataTextField = "StatusName";
			lstStatusName.DataValueField = "Status_ID";
			lstStatusName.DataBind();
			lstStatusName.Items.Insert(0, new ListItem("Select Status", ""));
		}
		catch (Exception)
		{

		}
	}
	private void getCandidateName()
	{
		try
		{
			dtmainSkillSet = new DataTable();
			dtmainSkillSet = spm.GetRef_CandidatedList("", "SelectApporvalCandidateName");
			lstCandidateName.DataSource = dtmainSkillSet;
			lstCandidateName.DataTextField = "Ref_CandidateName";
			lstCandidateName.DataValueField = "Ref_Candidate_ID";
			lstCandidateName.DataBind();
			lstCandidateName.Items.Insert(0, new ListItem("Select Candidate Name", ""));
		}
		catch (Exception)
		{

		}
	}
	#endregion

	protected void lnkView_Click(object sender, ImageClickEventArgs e)
	{
		try
		{
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			hdnRecruitment_ReqID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
			Response.Redirect("Ref_Candidate_View.aspx?Ref_Candidated_ID=" + hdnRecruitment_ReqID.Value);
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}

	protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		gvMngTravelRqstList.PageIndex = e.NewPageIndex;
		GetRef_CandidatePeindingList();
	}

	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		GetRef_CandidatePeindingList();
	}

	protected void mobile_btnBack_Click(object sender, EventArgs e)
	{
		lstCandidategender.SelectedIndex = -1;
		lstMainSkillset.SelectedIndex = -1;
		lstCandidateName.SelectedIndex = -1;
		lstEmployeeName.SelectedIndex = -1;
		lblmessage.Text = "";
		lstStatusName.SelectedIndex = -1;
		GetRef_CandidatePeindingList();
	}
}