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


public partial class procs_MyRetentionInbox : System.Web.UI.Page
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
					GetEmployeeName();
					GetDepartmentMaster();
					GetRetentionType();
					if (Request.QueryString.Count > 0)
					{
						hdnInboxType.Value = Convert.ToString(Request.QueryString[0]).Trim();
					}
						GetRetentionEmployee();
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
			Response.Redirect("EmployeeRetentionDetails.aspx?RetentionID=" + hdnRetentionID.Value);
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}
	#endregion
	#region PageMethod

	public void GetEmployeeName()
	{
		DataTable dtSkillset = new DataTable();
		dtSkillset = spm.Get_Dept_Retention_Detail("MyRetention_EMP", Convert.ToString(Session["Empcode"]).Trim());
		if (dtSkillset.Rows.Count > 0)
		{
			lstEmployeeName.DataSource = dtSkillset;
			lstEmployeeName.DataTextField = "Emp_Name";
			lstEmployeeName.DataValueField = "Emp_Code";
			lstEmployeeName.DataBind();
		}
		lstEmployeeName.Items.Insert(0, new ListItem("Select Employee Name", "0"));
	}
	public void GetDepartmentMaster()
	{
		DataTable dtPositionDept = new DataTable();
		dtPositionDept = spm.Get_Dept_Retention_Detail("MyRetention_Dept", Convert.ToString(Session["Empcode"]).Trim());
		if (dtPositionDept.Rows.Count > 0)
		{
			lstPositionDept.DataSource = dtPositionDept;
			lstPositionDept.DataTextField = "Department_Name";
			lstPositionDept.DataValueField = "Department_id";
			lstPositionDept.DataBind();
		}
		lstPositionDept.Items.Insert(0, new ListItem("Select Department", "0"));
	}
	public void GetRetentionType()
	{
		DataTable dtPositionDept = new DataTable();
		dtPositionDept = spm.Get_Dept_Retention_Detail("MyRetention_Type", Convert.ToString(Session["Empcode"]).Trim());
		if (dtPositionDept.Rows.Count > 0)
		{
			lstRetentionType.DataSource = dtPositionDept;
			lstRetentionType.DataTextField = "RetentionTypeName";
			lstRetentionType.DataValueField = "RetentionTypeID";
			lstRetentionType.DataBind();
		}
		lstRetentionType.Items.Insert(0, new ListItem("Select Retention Type", "0"));
	}
	#endregion

	protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		gvMngTravelRqstList.PageIndex = e.NewPageIndex;
		GetRetentionEmployee();
	}

	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		GetRetentionEmployee();
	}
	public void GetRetentionEmployee()
	{
		DataTable dtRequisitionDetails = new DataTable();
		lblmessage.Text = "";
		try
		{
			int Dept_id = 0, RetentionTypeID=0;
			string EmpCode = "";
			if (lstEmployeeName.SelectedIndex > 0)
			{
				EmpCode = lstEmployeeName.SelectedValue;
			}
			if (lstPositionDept.SelectedIndex > 0)
			{
				Dept_id = Convert.ToInt32(lstPositionDept.SelectedValue);
			}
			if (lstRetentionType.SelectedIndex > 0)
			{
				RetentionTypeID = Convert.ToInt32(lstRetentionType.SelectedValue);
			}
			SqlParameter[] spars = new SqlParameter[5];
			spars[0] = new SqlParameter("@Qtype", SqlDbType.VarChar);
			spars[0].Value = "MyRetention_EmployeeDetails";
			spars[1] = new SqlParameter("@EmpCode", SqlDbType.NVarChar);
			spars[1].Value = EmpCode;
			spars[2] = new SqlParameter("@Dept_id", SqlDbType.Int);
			spars[2].Value = Dept_id;
			spars[3] = new SqlParameter("@RetentionTypeID", SqlDbType.Int);
			spars[3].Value = RetentionTypeID;
			spars[4] = new SqlParameter("@AppEmpCode", SqlDbType.NVarChar);
			spars[4].Value =  Convert.ToString(Session["Empcode"]);
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

	protected void mobile_btnBack_Click(object sender, EventArgs e)
	{
		lstEmployeeName.SelectedIndex = -1;
		lstPositionDept.SelectedIndex = -1;
		lstRetentionType.SelectedIndex = -1;
		lblmessage.Text = "";
		GetRetentionEmployee();
	}
}
