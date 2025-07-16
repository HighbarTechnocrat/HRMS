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

public partial class procs_Rec_Offer_Index : System.Web.UI.Page
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
					GetSkillsetName();					
					GetDepartmentMaster();
					GetCompany_Location();
					GetlstPositionBand();

					if (Request.QueryString.Count > 0)
					{						
						hdnInboxType.Value = Convert.ToString(Request.QueryString[0]).Trim();
					}
					if (Convert.ToString(hdnInboxType.Value).Trim() == "APP")
					{
						GetRecruiterNumber("Select_Req_Offer_Approval");
						GetFilterOfferAPP("Select_Rec_Offer_APP_Search");
						lblheading.Text = "Offer Approval list";
					}
					if (Convert.ToString(hdnInboxType.Value).Trim() == "Pending")
					{
						GetRecruiterNumber("OfferApprovalPending");
						GetFilterOfferAPP("Select_Offer_Pending_Search");
						lblheading.Text = "Inbox Offer Approval Request";
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
			string StatusName = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
			string Rec_ID = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();


			if (StatusName == "Cancelled")
			{
				//Response.Redirect("Req_Requisition_Approval_C.aspx?Req_Requi_ID=" + hdnRecruitment_ReqID.Value);
			}
			else
			{
				Response.Redirect("Req_Offer_Approval.aspx?Offer_App_ID=" + hdnOffer_App_ID.Value +"&Rec_ID="+ Rec_ID);
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}
	#endregion
	#region PageMethod

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
	
	public void GetlstPositionBand()
	{
		DataTable dtPositionBand = new DataTable();
		dtPositionBand = spm.GetRecruitment_Req_HRMS_BAND_MASTER();
		if (dtPositionBand.Rows.Count > 0)
		{
			lstPositionBand.DataSource = dtPositionBand;
			lstPositionBand.DataTextField = "BAND";
			lstPositionBand.DataValueField = "BAND";
			lstPositionBand.DataBind();
			lstPositionBand.Items.Insert(0, new ListItem("Select Band", "0"));
		}
	}

	public void GetDepartmentMaster()
	{
		DataTable dtPositionDept = new DataTable();
		dtPositionDept = spm.GetRecruitment_Req_DepartmentMaster();
		if (dtPositionDept.Rows.Count > 0)
		{
			lstPositionDept.DataSource = dtPositionDept;
			lstPositionDept.DataTextField = "Department_Name";
			lstPositionDept.DataValueField = "Department_id";
			lstPositionDept.DataBind();
			lstPositionDept.Items.Insert(0, new ListItem("Select Department", "0"));

		}
	}
	public void GetCompany_Location()
	{
		DataTable lstPosition = new DataTable();
		lstPosition = spm.GetRecruitment_Req_company_Location();
		if (lstPosition.Rows.Count > 0)
		{
			lstPositionLoca.DataSource = lstPosition;
			lstPositionLoca.DataTextField = "Location_name";
			lstPositionLoca.DataValueField = "comp_code";
			lstPositionLoca.DataBind();
			lstPositionLoca.Items.Insert(0, new ListItem("Select Position Location", "0"));

		}
	}

	public void GetRecruiterNumber(string Stype)
	{
		DataTable RequisitionList = new DataTable();
		
		lblmessage.Text = "";
		try
		{

			SqlParameter[] spars = new SqlParameter[2];
			spars[0] = new SqlParameter("@mode", SqlDbType.VarChar);
			spars[0].Value = Stype;
			spars[1] = new SqlParameter("@empCode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(Session["Empcode"]);
			RequisitionList = spm.getMobileRemDataList(spars, "sp_Offer_Approval_Details");			
			if (RequisitionList.Rows.Count > 0)
			{
				DataView view = new DataView(RequisitionList);
				RequisitionList = view.ToTable(true, "RequisitionNumber", "Recruitment_ReqID");

				lstRequisitionNo.DataSource = RequisitionList;
				lstRequisitionNo.DataTextField = "RequisitionNumber";
				lstRequisitionNo.DataValueField = "Recruitment_ReqID";
				lstRequisitionNo.DataBind();				
			}
			lstRequisitionNo.Items.Insert(0, new ListItem("Select Requisition No", "0"));
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
		
	}
	private void GetReq_Offer_Approval_List(string Stype)
	{
		try
		{
			// DataSet dsList = new DataSet();
			DataTable OfferList = new DataTable();
			if (Convert.ToString(Session["Empcode"]).Trim() != "" || Session["Empcode"] != null)
			{
				OfferList = spm.GetReq_Req_Offer_Approval_List(Session["Empcode"].ToString(), Stype);
			}
			gvMngTravelRqstList.DataSource = null;
			gvMngTravelRqstList.DataBind();
			if (OfferList.Rows.Count > 0)
			{
				gvMngTravelRqstList.DataSource = OfferList;
				gvMngTravelRqstList.DataBind();

			}

		}
		catch (Exception ex)
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
			hdnOffer_App_ID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
			string Rec_ID = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
			Response.Redirect("Req_Offer_View.aspx?Offer_App_ID=" + hdnOffer_App_ID.Value + "&Rec_ID=" + Rec_ID);
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
			if (Convert.ToString(hdnInboxType.Value).Trim() == "APP")
			{
				ImageButton imgBtn = (ImageButton)e.Row.FindControl("lnkView");
				imgBtn.Visible = true;
				ImageButton imgBtn1 = (ImageButton)e.Row.FindControl("lnkEdit");
				imgBtn1.Visible = false;
            }
			else
			{
				ImageButton imgBtn = (ImageButton)e.Row.FindControl("lnkEdit");
				imgBtn.Visible = true;
               
            }
		}
	}

	protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		gvMngTravelRqstList.PageIndex = e.NewPageIndex;
		if (Convert.ToString(hdnInboxType.Value).Trim() == "APP")
		{
			GetFilterOfferAPP("Select_Rec_Offer_APP_Search");

			lblheading.Text = "Offer Approval list";
		}
		if (Convert.ToString(hdnInboxType.Value).Trim() == "Pending")
		{
			GetFilterOfferAPP("Select_Offer_Pending_Search");

		}
	}

	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		if (Convert.ToString(hdnInboxType.Value).Trim() == "APP")
		{
			GetFilterOfferAPP("Select_Rec_Offer_APP_Search");
		}
		if (Convert.ToString(hdnInboxType.Value).Trim() == "Pending")
		{
			GetFilterOfferAPP("Select_Offer_Pending_Search");
		}
	}
	public void GetFilterOfferAPP(string Stype)
	{
		DataTable dtRequisitionDetails = new DataTable();
		lblmessage.Text = "";
		try
		{

			SqlParameter[] spars = new SqlParameter[7];
			spars[0] = new SqlParameter("@mode", SqlDbType.VarChar);
			spars[0].Value = Stype;
			spars[1] = new SqlParameter("@empCode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(Session["Empcode"]);
			spars[2] = new SqlParameter("@ModuleId", SqlDbType.Int);
			spars[2].Value = lstSkillset.SelectedValue;
			spars[3] = new SqlParameter("@loc_code", SqlDbType.VarChar);
			spars[3].Value = lstPositionLoca.SelectedValue;
			spars[4] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
			spars[4].Value = lstRequisitionNo.SelectedValue;
			spars[5] = new SqlParameter("@BAND", SqlDbType.VarChar);
			spars[5].Value = lstPositionBand.SelectedValue;
			spars[6] = new SqlParameter("@Department_id", SqlDbType.Int);
			spars[6].Value = lstPositionDept.SelectedValue;
			dtRequisitionDetails = spm.getMobileRemDataList(spars, "sp_Offer_Approval_Details");
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

	protected void mobile_btnBack_Click(object sender, EventArgs e)
	{
		lstPositionBand.SelectedIndex = -1;
		lstPositionDept.SelectedIndex = -1;
		lstRequisitionNo.SelectedIndex = -1;
		lstPositionLoca.SelectedIndex = -1;
		lstSkillset.SelectedIndex = -1;
		lblmessage.Text = "";
		if (Convert.ToString(hdnInboxType.Value).Trim() == "APP")
		{
			GetRecruiterNumber("Select_Req_Offer_Approval");
			GetFilterOfferAPP("Select_Rec_Offer_APP_Search");

			lblheading.Text = "Offer Approval list";

		}
		if (Convert.ToString(hdnInboxType.Value).Trim() == "Pending")
		{
			GetRecruiterNumber("OfferApprovalPending");
			GetFilterOfferAPP("Select_Offer_Pending_Search");

		}
	}
}