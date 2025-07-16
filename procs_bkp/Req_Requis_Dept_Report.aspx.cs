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

public partial class Req_Requis_Dept_Report : System.Web.UI.Page
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

					GetHODDept();
					GetPositionCriticality();
                    //GetDepartmentMaster();
                    GetCompany_Location();
                    GetRecruiter();
                    txtfromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txttodate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    public void GetPositionCriticality()
    {
        DataTable dtPositionCriti = new DataTable();
        dtPositionCriti = spm.GetRecruitment_Req_PositionCriticality();
        if (dtPositionCriti.Rows.Count > 0)
        {
            lstPositionCriti.DataSource = dtPositionCriti;
            lstPositionCriti.DataTextField = "PositionCriticality";
            lstPositionCriti.DataValueField = "PositionCriticality_ID";
            lstPositionCriti.DataBind();
            lstPositionCriti.Items.Insert(0, new ListItem("Select Criticality", "0"));
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
			//lstPositionDept.Enabled = false;
			//updated code
			//DataRow[] dr = dtPositionDept.Select("Department_Name = '" + hflEmpDepartment.Value.ToString().Trim() + "'");
			//if (dr.Length > 0)
			//{
			//	string avalue = dr[0]["Department_id"].ToString();
			//	lstPositionDept.SelectedValue = avalue;
			//}
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
    public void GetRecruiter()
    {
        DataTable dtInterviewer = new DataTable();
        dtInterviewer = spm.GetRecruitment_Recruiter();
        if (dtInterviewer.Rows.Count > 0)
        {
            lstRecruiter.DataSource = dtInterviewer;
            lstRecruiter.DataTextField = "EmpName";
            lstRecruiter.DataValueField = "Emp_Code";
            lstRecruiter.DataBind();
            lstRecruiter.Items.Insert(0, new ListItem("Select Recruiter", "0"));
        }
    }

	public void GetHODDept()
	{
		DataSet dsReqNo = new DataSet();
		try
		{
			DataTable dtEmployee = new DataTable();
			SqlParameter[] spars = new SqlParameter[2];
			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "sp_Report_HOD";
			spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(Session["Empcode"]);
			dtEmployee = spm.getMobileRemDataList(spars, "SP_GET_REQ_REQUISTIONNO");
			if (dtEmployee.Rows.Count > 0)
			{
				lstPositionDept.DataSource = dtEmployee;
				lstPositionDept.DataTextField = "Department_Name";
				lstPositionDept.DataValueField = "Department_id";
				lstPositionDept.DataBind();
				lstPositionDept.Items.Insert(0, new ListItem("Select Department", "0"));
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}
	protected void txtfromdate_TextChanged(object sender, EventArgs e)
	{
		string[] strdate;
		string strfromDate = "";
		string strToDate = "";

		if ((Convert.ToString(txtfromdate.Text).Trim() != "") && (Convert.ToString(txttodate.Text).Trim() != ""))
		{
			strdate = Convert.ToString(txtfromdate.Text).Trim().Split('/');
			strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

			strdate = Convert.ToString(txttodate.Text).Trim().Split('/');
			strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);


			DateTime startDate = Convert.ToDateTime(strfromDate);
			DateTime endDate = Convert.ToDateTime(strToDate);
			if (startDate > endDate)
			{
				lblmessage.Text = "From Date should be less than To Date ";
				txtfromdate.Text = "";
				return;
			}
			else
			{
				lblmessage.Text = "";
				// ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Start date should be greate than End date')", true);
			}
		}
	}
	protected void txtToDate_TextChanged(object sender, EventArgs e)
	{
		string[] strdate;
		string strfromDate = "";
		string strToDate = "";

		if ((Convert.ToString(txtfromdate.Text).Trim() != "") && (Convert.ToString(txttodate.Text).Trim() != ""))
		{
			strdate = Convert.ToString(txtfromdate.Text).Trim().Split('/');
			strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

			strdate = Convert.ToString(txttodate.Text).Trim().Split('/');
			strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);


			DateTime startDate = Convert.ToDateTime(strfromDate);
			DateTime endDate = Convert.ToDateTime(strToDate);
			if (startDate > endDate)
			{
				lblmessage.Text = "To Date should be greater than From Date ";
				txttodate.Text = "";

				return;
			}
			else
			{
				// ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Start date should be greate than End date')", true);
			}
		}
	}
	private Boolean Date_Validation()
    {
        lblmessage.Text = "";
        Boolean blnValid = false;
        
        DateTime? ddt = null;
        DateTime? ddt2 = null;
        string[] strdate, strdate1;
        string StartDate = "", EndDate = "";
        strdate = Convert.ToString(txtfromdate.Text).Trim().Split('/');
        if (strdate[2].Length > 3)
        {
            StartDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
             ddt = DateTime.ParseExact(StartDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
        }
        strdate1 = Convert.ToString(txttodate.Text).Trim().Split('/');
        if (strdate1[2].Length > 3)
        {
            EndDate = Convert.ToString(strdate1[2]) + "-" + Convert.ToString(strdate1[1]) + "-" + Convert.ToString(strdate1[0]);
            ddt2 = DateTime.ParseExact(EndDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
        }
        if (ddt <= ddt2)
        {

            blnValid = true;
        }

        return blnValid;
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
		try {
			#region get Rec Dept details
			string[] fromdate, todate;
			string ListName=string.Empty, ReportName= string.Empty;
			string StartDate = "", EndDate = "", Stype = "SelectDeptReport", PositionCritiName = "", DeptName = "", Location = "",Recruiter ="";
        int DeptID = 0, PositionCritiID = 0;
        lblmessage.Text = ""; string strToDate_RPt = string.Empty;
		ReportViewer1.Visible = false;

		if (Convert.ToString(lstreportName.SelectedValue).Trim() == "0" || Convert.ToString(lstreportName.SelectedItem.Text).Trim() == "Select Report Name")
		{
				lblmessage.Text = "Please Select Report By ";
				return;
		}
			if (Convert.ToString(txtfromdate.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter From Date ";
            return;
        }

			if (Convert.ToString(lstreportName.SelectedValue).Trim() != "0")
			{
				Stype = Convert.ToString(lstreportName.SelectedValue).Trim();
			}
			
		string confirmValue = hdnYesNo.Value.ToString();       
        fromdate = Convert.ToString(txtfromdate.Text).Trim().Split('/');
        StartDate = Convert.ToString(fromdate[2]) + "-" + Convert.ToString(fromdate[1]) + "-" + Convert.ToString(fromdate[0]);
	    DateTime STDate = DateTime.ParseExact(StartDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
			if (txttodate.Text.Trim() !="")
			{
				todate = Convert.ToString(txttodate.Text).Trim().Split('/');
				EndDate = Convert.ToString(todate[2]) + "-" + Convert.ToString(todate[1]) + "-" + Convert.ToString(todate[0]);				
			}
			else
			{
				EndDate = DateTime.Today.ToString("yyyy") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("dd");
			}						
			DateTime EDDate = DateTime.ParseExact(EndDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
			//strToDate_RPt = DateTime.Today.ToString("dd") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("yyyy");

		DeptID = Convert.ToString(lstPositionDept.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionDept.SelectedValue) : 0;
        PositionCritiID = Convert.ToString(lstPositionCriti.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionCriti.SelectedValue) : 0;

        dtDeptReport = spm.Get_Rec_Recruit_Dept_Report(Stype, DeptID, lstPositionLoca.SelectedValue.Trim(), PositionCritiID, lstRecruiter.SelectedValue.Trim(), STDate, EDDate, Convert.ToString(Session["Empcode"]));

			#endregion
			if (dtDeptReport.Tables[0].Rows.Count > 0)
			{
				ReportViewer1.Visible = true;
				ReportViewer1.LocalReport.Refresh();
				ReportViewer1.LocalReport.DataSources.Clear();

				if (PositionCritiID == 1)
				{
					PositionCritiName = "Only Regular Positions :";
				}
				else if (PositionCritiID == 2)
				{
					PositionCritiName = "Only Critical Positions :";
				}
				else
				{
					PositionCritiName = "All Category Positions :";
				}
				if (DeptID != 0)
				{
					DeptName = "Department : "+lstPositionDept.SelectedItem.Text.Trim();
				}
				if (lstPositionLoca.SelectedValue !="0")
				{
					Location = " Location : " + lstPositionLoca.SelectedItem.Text.Trim();
				}
				if (lstRecruiter.SelectedValue != "0")
				{
					Recruiter = " Recruiter : " + lstRecruiter.SelectedItem.Text.Trim();
				}
				if (Convert.ToString(lstreportName.SelectedValue).Trim() != "0")
				{
					ListName = Convert.ToString(lstreportName.SelectedItem.Text).Trim();
					ReportName = Convert.ToString(lstreportName.SelectedItem.Text).Trim()+" Wise Summary Report";
				}
				if (txttodate.Text.Trim() != "")
				{
					strToDate_RPt = " To Date : " + Convert.ToString(txttodate.Text.Replace('/', '-'));
				}
				else
				{
					strToDate_RPt = " To Date : " + DateTime.Today.ToString("dd") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("yyyy");
				}
					ReportParameter[] param = new ReportParameter[9];
				param[0] = new ReportParameter("pyear", "Opening as on " + Convert.ToString(txtfromdate.Text.Replace('/','-')));
				param[1] = new ReportParameter("FromDate", "From Date : " + Convert.ToString(txtfromdate.Text.Replace('/', '-'))+ " " + strToDate_RPt + " " + Recruiter);				
				param[2] = new ReportParameter("ToDate", Convert.ToString(""));									
				param[3] = new ReportParameter("CriticalPositions", Convert.ToString(PositionCritiName));
				param[4] = new ReportParameter("DeptName", Convert.ToString(DeptName + " " + Location));
				param[5] = new ReportParameter("Location", Convert.ToString(Location));
				param[6] = new ReportParameter("Recruiter", Convert.ToString(Recruiter));
				param[7] = new ReportParameter("ListName", Convert.ToString(ListName));
				param[8] = new ReportParameter("ReportName", Convert.ToString(ReportName));


				// Create Report DataSource

				ReportViewer1.ProcessingMode = ProcessingMode.Local;
				ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/Req_Deptwise_Report.rdlc");
				ReportDataSource rds = new ReportDataSource("Rec_DeptWise", dtDeptReport.Tables[0]);
				ReportDataSource rds1 = new ReportDataSource("Recruit_Dept_Critical", dtDeptReport.Tables[1]);
				ReportViewer1.LocalReport.DataSources.Clear();
				ReportViewer1.LocalReport.DataSources.Add(rds);
				ReportViewer1.LocalReport.DataSources.Add(rds1);
				ReportViewer1.LocalReport.SetParameters(param);
				ReportViewer1.LocalReport.Refresh();


			}
			else
			{
				ReportViewer1.Visible = false;
				lblmessage.Text = "Record not available";
			}
	}
        catch (Exception ex)
        {
        }
	}

    
    



	

	protected void mobile_btnBack_Click(object sender, EventArgs e)
	{
		lstreportName.SelectedIndex = 1;
		lstPositionDept.SelectedIndex = -1;
		lstPositionLoca.SelectedIndex = -1;
		lstPositionCriti.SelectedIndex = -1;
		lstRecruiter.SelectedIndex = -1;
		txtfromdate.Text = "";
		txttodate.Text = "";
		ReportViewer1.Visible = false;
		lblmessage.Text = "";
	}
}