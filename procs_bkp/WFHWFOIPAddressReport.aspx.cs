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
using System.Web;
using System.ComponentModel;
using System.Linq;

public partial class WFHWFOIPAddressReport : System.Web.UI.Page
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
    public DataTable dtEmp, dtleavebal, dtApprover, dtLeaveTypes, dtApproverEmailIds, dtMaxRequestId, dtIntermediate, dtLeaveCalculation, dtHolidays,dtleavedetails;
    public int Leaveid;
    public int leavetype, openbal, avail, rembal,weekendcount,publicholiday,apprid;
    double totaldays;
    public string filename = "", approveremailaddress,message;
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    #region Page Events
    private void Page_Load(object sender, System.EventArgs e)
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
            btnIn.Visible = true;
           // btnback_mng.Visible = false;
            //   
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
               
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    hdnReqid.Value = "";
                   // txtDescription.Attributes.Add("maxlength", "500");
                    //get_Shiftdetails();
                    // get_CheckIn();
                    if (Request.QueryString.Count > 0)
                    {
                        hdnReqid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                    }
                    //txtFromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                   // BindProjectDDL(lpm.Emp_Code);
                    //GetYearList();
                   // PopulateEmployeeData();
                   if(!CheckIsReportShow())
                    {
                        Response.Redirect("~/procs/Attendance.aspx");
                    }
                }
                
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/Attendance.aspx");
    }

    protected void lnkIndex_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/Attendance.aspx");
    }
    protected void txtFromfor_TextChanged(object sender, EventArgs e)
    {
        //LeavedaysCalcualation();
    }
    protected void txtToFor_TextChanged(object sender, EventArgs e)
    {
        //LeavedaysCalcualation();
    }

    #endregion

    #region page Methods


    public void PopulateEmployeeData()
    {
        try
        {
            txtEmpCode.Text = lpm.Emp_Code;
            //btnSave.Enabled = false; 
            dtEmp = spm.GetEmployeeData(lpm.Emp_Code);

            if (dtEmp.Rows.Count > 0)
            {
                lpm.Emp_Status = (string)dtEmp.Rows[0]["Emp_status"];

                //lblmessage.Text = "You are not allowed to apply for any type of leave sicne your employee status is in resignation";
                // IsEnabledFalse(false);                

                lpm.Emp_Name = (string)dtEmp.Rows[0]["Emp_Name"];
                lpm.Designation_name = (string)dtEmp.Rows[0]["DesginationName"];
                lpm.department_name = (string)dtEmp.Rows[0]["Department_Name"];
                lpm.Grade = Convert.ToString(dtEmp.Rows[0]["Grade"]).Trim();
                lpm.EmailAddress = (string)dtEmp.Rows[0]["Emp_EmailAddress"];
                hflGrade.Value = lpm.Grade;
                hflEmailAddress.Value = lpm.EmailAddress;
                hflEmpName.Value = lpm.Emp_Name;
                hflEmpDepartment.Value = lpm.department_name;
                hflEmpDesignation.Value = lpm.Designation_name;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }
    #endregion 

    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            txtToDate.Enabled = true;
            var getDate = txtFromdate.Text.ToString();

            DateTime StartDate = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime StartingDate = DateTime.ParseExact("01/06/2022", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var getDay = DateTime.DaysInMonth(StartDate.Year, StartDate.Month);
            //hdnDay.Value = (getDay-1);
            DateTime EndDate = StartDate.AddDays(30);
            if (getDay == 30)
            {
                EndDate = StartDate.AddDays(29);
            }
            else if (getDay == 28)
            {
                EndDate = StartDate.AddDays(27);
            }
            else if (getDay == 29)
            {
                EndDate = StartDate.AddDays(28);
            }
            var getDayTotal = (getDay - 1);
            hdnDay.Value = getDayTotal.ToString();
            var SelectEndDate = EndDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            SelectEndDate = SelectEndDate.Replace('-', '/');
           // txtToDate.Text = SelectEndDate;
            DateTime SelectedDate = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime Today = DateTime.Now;
            if (StartingDate > SelectedDate)
            {
                lblmessage.Text = "Please select date from 01 Jun 2022 ";
                //txtFromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromdate.Text = "";
                txtToDate.Text = "";
            }
            else if (Today < SelectedDate)
            {
                lblmessage.Text = "Please select date only today or less than today";
                //txtFromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromdate.Text = "";
                txtToDate.Text = "";
            }
            else
            {               
                lblmessage.Text = "";
            }
        }
        catch (Exception ex)
        {

        }
    
    }

    private void getTimesheetDetailsInDate()
    {
        try
        {
            #region get Timesheet CLientwise Report
            DataTable dtTimesheetClientwise = new DataTable();
            DataTable dtTimesheetEmployeewise = new DataTable();
            SqlParameter[] spars = new SqlParameter[4];           
            string[] strdate;
            string strfromDate = "";
            string strToDate = "";
            string secoundReportHide = "N";
            string EmpName = "";
            //@fromDate
            spars[0] = new SqlParameter("@FromDate", SqlDbType.VarChar);
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
               
                //strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                //strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
               
                spars[0].Value = strfromDate;
            }
            else
            {
                spars[0].Value = null;
            }
            
            
            //@serviceRequestId
            spars[1] = new SqlParameter("@ToDate", SqlDbType.VarChar);
            if (Convert.ToString(txtToDate.Text).Trim() != "")
            {
                //strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                //strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            else
            {
				var ToDate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
				strdate = Convert.ToString(ToDate).Trim().Split('/');
				strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

			}
			spars[1].Value = strToDate;
			spars[2] = new SqlParameter("@qtype", SqlDbType.VarChar);

            spars[2].Value = "GETIPADDRESSDETAILS";

            spars[3] = new SqlParameter("@PM_Emp_Code", SqlDbType.VarChar);

            spars[3].Value = Convert.ToString(lpm.Emp_Code);

            var getLoginId = lpm.Emp_Code;          
            dtTimesheetClientwise = spm.getTeamReport(spars, "SP_WFOWFHCEOReport");
            #endregion

            ReportParameter[] param = new ReportParameter[4];
            
           param[0] = new ReportParameter("StartDate", Convert.ToString(strfromDate));
           param[1] = new ReportParameter("EndDate", Convert.ToString(strToDate));
           param[2] = new ReportParameter("secoundReportHide", Convert.ToString(secoundReportHide));
           param[3] = new ReportParameter("EmpName", Convert.ToString(EmpName));          


            ReportViewer2.LocalReport.Refresh();
            ReportViewer2.LocalReport.DataSources.Clear();

            if (dtTimesheetClientwise.Rows.Count > 0)
            {
                // Create Report DataSource :- for payment Voucher
                ReportDataSource serviceRequestDS = new ReportDataSource("dsGetIPAddress", dtTimesheetClientwise);
                ReportViewer2.ProcessingMode = ProcessingMode.Local;
                //ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/PaymentVoucher_reimbursment.rdlc");
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/WFHWFOIPAddressReport.rdlc");
                ReportViewer2.LocalReport.DataSources.Add(serviceRequestDS);
                //ReportViewer2.ShowToolBar = true;
            }
            else
            {
                // Create Report DataSource :- for payment Voucher
                ReportDataSource serviceRequestDS = new ReportDataSource("dsGetIPAddress", dtTimesheetClientwise);
                ReportViewer2.ProcessingMode = ProcessingMode.Local;
                //ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/PaymentVoucher_reimbursment.rdlc");
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/WFHWFOIPAddressReport.rdlc");
                ReportViewer2.LocalReport.DataSources.Add(serviceRequestDS);
                //ReportViewer2.ShowToolBar = true;
            }
            ReportViewer2.LocalReport.SetParameters(param);

        }
        catch (Exception ex)
        {
        }

    }
    
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
  
    protected void btnIn_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        try
        {
            btnIn.Visible = true;
            var getdate = txtFromdate.Text.Trim();
            if(getdate=="")
            {
                lblmessage.Text = "Please Select From Date";
                return;
            }

            var getTodate = txtToDate.Text.Trim();
            //if (getTodate == "")
            //{
            //    lblmessage.Text = "Please select to date";
            //    return;
            //}

            getTimesheetDetailsInDate();
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var getDate = txtFromdate.Text.ToString();
            var getDate1 = txtToDate.Text.ToString();
            DateTime SelectedDate = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime Today = DateTime.ParseExact(getDate1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            // DateTime Today = DateTime.Now;
            if (Today < SelectedDate)
            {
                lblmessage.Text = "Please select to date greater than from date";
                //txtFromdate.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                txtToDate.Text = "";
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }

    public bool CheckIsReportShow()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowCV";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(lpm.Emp_Code);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "IPAddressReport";



            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    return  true;
                }
                else
                {
                    return false;
                }
            }
             return false;
        }
        catch (Exception)
        {
             return false;
        }
    }
}


