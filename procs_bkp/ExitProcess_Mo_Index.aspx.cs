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

public partial class procs_ExitProcess_Mo_Index : System.Web.UI.Page
{
	#region Creative_Default_methods
	SqlConnection source;
	public SqlDataAdapter sqladp;
	public static string dob1;
	public string userid;
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
						if (hdnInboxType.Value == "Pending")
						{
							GetEmployeeName("Retention_M_Pending_Emp");
							GetDepartmentMaster("Retention_M_Pending_Dept");
							GetLocation_Name("Retention_M_Pending_Location");
							GetRetentionEmployee("Retention_Mode_Pending_List");
						}
						else 
						{
							GetEmployeeName("Retention_M_APP_Emp");
							GetDepartmentMaster("Retention_M_APP_Dept");
							GetLocation_Name("Retention_M_APP_Location");
							GetRetentionEmployee("Retention_Mode_APP_List");
						}

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
			hdnRetentionID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
			//string Rec_ID = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
			Response.Redirect("ExitProcess_Mo_Approval.aspx?RetentionID=" + hdnRetentionID.Value+"&Type=APP");
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}
	protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		gvMngTravelRqstList.PageIndex = e.NewPageIndex;
		if (hdnInboxType.Value == "Pending")
		{
			GetRetentionEmployee("Retention_Mode_Pending_List");
		}
		else
		{
			GetRetentionEmployee("Retention_Mode_APP_List");
		}
	}
	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		if (hdnInboxType.Value == "Pending")
		{
			GetRetentionEmployee("Retention_Mode_Pending_List");
		}
		else
		{
			GetRetentionEmployee("Retention_Mode_APP_List");
		}
	}
	protected void mobile_btnBack_Click(object sender, EventArgs e)
	{
		lstEmployeeName.SelectedIndex = -1;
		lstPositionDept.SelectedIndex = -1;
		lstLocation.SelectedIndex = -1;
		lblmessage.Text = "";
		if (hdnInboxType.Value == "Pending")
		{
			GetEmployeeName("Retention_M_Pending_Emp");
			GetDepartmentMaster("Retention_M_Pending_Dept");
			GetLocation_Name("Retention_M_Pending_Location");
			GetRetentionEmployee("Retention_Mode_Pending_List");
		}
		else
		{
			GetEmployeeName("Retention_M_APP_Emp");
			GetDepartmentMaster("Retention_M_APP_Dept");
			GetLocation_Name("Retention_M_APP_Location");
			GetRetentionEmployee("Retention_Mode_APP_List");
		}
	}
	protected void lnkView_Click(object sender, ImageClickEventArgs e)
	{
		try
		{
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			hdnRetentionID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
			//string Rec_ID = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
			Response.Redirect("ExitProcess_Mo_Approval.aspx?RetentionID=" + hdnRetentionID.Value + "&Type=View");
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
			if (Convert.ToString(hdnInboxType.Value).Trim() == "View")
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
	#endregion
	#region PageMethod

	public void GetEmployeeName(string Qtype)
	{
		DataTable dtSkillset = new DataTable();
		dtSkillset = spm.Get_Dept_Retention_Detail(Qtype, Convert.ToString(Session["Empcode"]).Trim());
		if (dtSkillset.Rows.Count > 0)
		{
			lstEmployeeName.DataSource = dtSkillset;
			lstEmployeeName.DataTextField = "Emp_Name";
			lstEmployeeName.DataValueField = "Emp_Code";
			lstEmployeeName.DataBind();
		}
		lstEmployeeName.Items.Insert(0, new ListItem("Select Employee Name", "0"));
	}
	public void GetDepartmentMaster(string Qtype)
	{
		DataTable dtPositionDept = new DataTable();
		dtPositionDept = spm.Get_Dept_Retention_Detail(Qtype, Convert.ToString(Session["Empcode"]).Trim());
		if (dtPositionDept.Rows.Count > 0)
		{
			lstPositionDept.DataSource = dtPositionDept;
			lstPositionDept.DataTextField = "Department_Name";
			lstPositionDept.DataValueField = "Department_id";
			lstPositionDept.DataBind();
		}
		lstPositionDept.Items.Insert(0, new ListItem("Select Department", "0"));
	}
	public void GetLocation_Name(string Qtype)
	{
		DataTable dtPositionDept = new DataTable();
		dtPositionDept = spm.Get_Dept_Retention_Detail(Qtype, Convert.ToString(Session["Empcode"]).Trim());
		if (dtPositionDept.Rows.Count > 0)
		{
			lstLocation.DataSource = dtPositionDept;
			lstLocation.DataTextField = "Location_name";
			lstLocation.DataValueField = "comp_code";
			lstLocation.DataBind();		
		}
		lstLocation.Items.Insert(0, new ListItem("Select Project Name", "0"));
	}
	public void GetRetentionEmployee(string Qtype)
	{
		DataTable dtRequisitionDetails = new DataTable();
		lblmessage.Text = "";
		try
		{
			int Dept_id = 0;
			string EmpCode = "", comp_code = "";
			if (lstEmployeeName.SelectedIndex > 0)
			{
				EmpCode = lstEmployeeName.SelectedValue;
			}
			if (lstPositionDept.SelectedIndex > 0)
			{
				Dept_id = Convert.ToInt32(lstPositionDept.SelectedValue);
			}
			if (lstLocation.SelectedIndex > 0)
			{
				comp_code = Convert.ToString(lstLocation.SelectedValue);
			}
			SqlParameter[] spars = new SqlParameter[5];
			spars[0] = new SqlParameter("@Qtype", SqlDbType.VarChar);
			spars[0].Value = Qtype;
			spars[1] = new SqlParameter("@EmpCode", SqlDbType.NVarChar);
			spars[1].Value = EmpCode;
			spars[2] = new SqlParameter("@Dept_id", SqlDbType.Int);
			spars[2].Value = Dept_id;
			spars[3] = new SqlParameter("@Comp_code", SqlDbType.NVarChar);
			spars[3].Value = comp_code;
			spars[4] = new SqlParameter("@AppEmpCode", SqlDbType.NVarChar);
			spars[4].Value = Convert.ToString(Session["Empcode"]);
			dtRequisitionDetails = spm.getMobileRemDataList(spars, "SP_Employee_Retention_Details");
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
	#endregion
	
}