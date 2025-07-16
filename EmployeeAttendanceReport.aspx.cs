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

public partial class EmployeeAttendanceReport : System.Web.UI.Page
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
                   //if(!CheckIsReportShow())
                   // {
                   //     Response.Redirect("~/procs/Attendance.aspx");
                   // }
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

    //public void BindProjectDDL(string pm_Emp_Code)
    //{
    //    try
    //    {
    //        ddlProject.DataSource = null;
    //        ddlProject.Items.Clear();
    //        var getEmpdataTable = getAllDLLData("GetProjectList", pm_Emp_Code,null,null);
    //        if(getEmpdataTable!=null)
    //        {
    //            if (getEmpdataTable.Rows.Count > 0)
    //            {

    //                ddlProject.DataSource = getEmpdataTable;
    //                //ddlDepartment.DataBind();
    //                ddlProject.DataTextField = "ProjectFullName";
    //                ddlProject.DataValueField = "comp_code";
    //                ddlProject.DataBind();
    //                ddlProject.Items.Insert(0, new ListItem("Select Project", "0")); //updated code
    //            }
    //            else
    //            {
    //                //ddlProject.DataSource = null;
    //                ddlProject.Items.Insert(0, new ListItem("Select Project", "0")); //updated code
    //            }
    //        }
    //        else
    //        {
    //            //ddlProject.DataSource = null;
    //            ddlProject.Items.Insert(0, new ListItem("Select Project", "0")); //updated code
    //        }   
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}
    //public void BindReporteeOffEmpDDL(string pm_Emp_Code)
    //{
    //    try
    //    {
    //        var getEmpdataTable = getAllDLLData("GetEmployeeAnReportee", pm_Emp_Code, null, null);
    //        if (getEmpdataTable != null)
    //        {
    //            if (getEmpdataTable.Rows.Count > 0)
    //            {
    //                Reporteesoff.Enabled = true;
    //                Reporteesoff.Items.Clear();
                    
                   
    //              //  Reporteesoff.d = true;
    //                Reporteesoff.DataSource = getEmpdataTable;
    //                //ddlDepartment.DataBind();
    //                Reporteesoff.DataTextField = "Emp_Name";
    //                Reporteesoff.DataValueField = "Emp_Code";
    //                Reporteesoff.DataBind();
    //                Reporteesoff.Items.Insert(0, new ListItem("Select Reportee Name", "0")); //updated code
    //            }
    //            else
    //            {
    //                Reporteesoff.DataSource = null;
    //            }
    //        }
    //        else
    //        {
    //            Reporteesoff.DataSource = null;
    //        }
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}

    //public void BindEmployeeDDL(string pm_Emp_Code,string a_Emp_Code,string rType)
    //{
    //    try
    //    {


    //        var getEmpdataTable = getAllDLLData("GetEmployee", pm_Emp_Code, rType, a_Emp_Code);
    //        if (getEmpdataTable != null)
    //        {
    //            if (getEmpdataTable.Rows.Count > 0)
    //            {
    //                ddlEmployee.Enabled = true;
    //                ddlEmployee.Items.Clear();
    //                ddlEmployee.DataSource = getEmpdataTable;
    //                //ddlDepartment.DataBind();
    //                ddlEmployee.DataTextField = "Emp_Name";
    //                ddlEmployee.DataValueField = "Emp_Code";
    //                ddlEmployee.DataBind();
    //                ddlEmployee.Items.Insert(0, new ListItem("Select Employee Name", "0")); //updated code
    //            }
    //            else
    //            {
    //                ddlEmployee.DataSource = null;
    //            }
    //        }
    //        else
    //        {
    //            ddlEmployee.DataSource = null;
    //        }
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}
    public DataTable getAllDLLData(string qtype,string pm_emp_Code,string RType,string a_Emp_Code)
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[5];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = qtype;

            spars[1] = new SqlParameter("@PM_Emp_Code", SqlDbType.VarChar);
            spars[1].Value = pm_emp_Code;

            spars[2] = new SqlParameter("@A_Emp_Code", SqlDbType.VarChar);
            spars[2].Value = a_Emp_Code;

            spars[3] = new SqlParameter("@Reportees", SqlDbType.VarChar);
            spars[3].Value = RType;

            spars[4] = new SqlParameter("@FromDate", SqlDbType.VarChar);
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
               var strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
               var strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

                strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
              var  strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

                spars[4].Value = strfromDate;
            }
            else
            {
                spars[4].Value = null;
            }
            var getLoginId = lpm.Emp_Code;

            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_WFOWFHCEOReport");


            return getdtDetails;
        }
        catch (Exception)
        {
            getdtDetails = new DataTable();
            return getdtDetails;
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
            DateTime StartingDate = DateTime.ParseExact("25/04/2021", "dd/MM/yyyy", CultureInfo.InvariantCulture);

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
            txtToDate.Text = SelectEndDate;
            DateTime SelectedDate = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime Today = DateTime.Now;
            if (StartingDate > SelectedDate)
            {
                lblmessage.Text = "Please select date from 25 April 2021";
                //txtFromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromdate.Text = "";
                txtToDate.Text = "";
                return;
            }
            else if (Today < SelectedDate)
            {
                lblmessage.Text = "Please select date only today or less than today";
                //txtFromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromdate.Text = "";
                txtToDate.Text = "";
                return;
            }
            else
            {
                //ddlProject.Enabled = true;
                //Reportees.Enabled = true;
                //ddlDirectALLR.Enabled = true;
                lblmessage.Text = "";
            }
            var ToDate = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            txtToDate.Text = ToDate;
        }
        catch (Exception ex)
        {

        }
    
    }

    private void getTimesheetDetailsInDate()
    {
        try
        {
            #region 
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
               
                strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
               
                spars[0].Value = strfromDate;
            }
            else
            {
                spars[0].Value = null;
            }
            
            spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            spars[1].Value = lpm.Emp_Code;
            //@serviceRequestId
            spars[2] = new SqlParameter("@ToDate", SqlDbType.VarChar);
            if (Convert.ToString(txtToDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

                strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

                spars[2].Value = strToDate;
            }
            else
            {
                spars[2].Value = null;
            }

            spars[3] = new SqlParameter("@qtype", SqlDbType.VarChar);

            spars[3].Value = "getEmployeeAttReport";

            
            var getLoginId = lpm.Emp_Code;          
            dtTimesheetClientwise = spm.getTeamReport(spars, "SP_AttendanceEmployeeReport");
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
                ReportDataSource serviceRequestDS = new ReportDataSource("dsAttedanceDetailsReport", dtTimesheetClientwise);
                ReportViewer2.ProcessingMode = ProcessingMode.Local;
                //ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/PaymentVoucher_reimbursment.rdlc");
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/EmployeeAttendanceReport.rdlc");
                ReportViewer2.LocalReport.DataSources.Add(serviceRequestDS);
                //ReportViewer2.ShowToolBar = true;
            }
            else
            {
                // Create Report DataSource :- for payment Voucher
                ReportDataSource serviceRequestDS = new ReportDataSource("dsAttedanceDetailsReport", dtTimesheetClientwise);
                ReportViewer2.ProcessingMode = ProcessingMode.Local;
                //ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/PaymentVoucher_reimbursment.rdlc");
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/EmployeeAttendanceReport.rdlc");
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
                lblmessage.Text = "Please select from date";
                return;
            }
            var getTodate = txtToDate.Text.Trim();
            if (getdate == "")
            {
                lblmessage.Text = "Please select to date";
                return;
            }
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

    //protected void Reportees_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ddlEmployee.Items.Clear();
    //        ddlEmployee.Items.Insert(0, new ListItem("Select Employee", "0")); //updated code
    //        ddlEmployee.Enabled = false;

    //        ddlDirectALLR.Items.Clear();
    //        ddlDirectALLR.Items.Insert(0, new ListItem("All Reportees", "ALLR")); //updated code
    //        ddlDirectALLR.Items.Insert(1, new ListItem("Direct Reportees", "DR")); //updated code
    //        ddlDirectALLR.Items.Insert(2, new ListItem("An Employee", "ANEMP")); //updated code
    //        ddlDirectALLR.Items.FindByValue("ALLR").Selected = true;

    //        var getVal = Reportees.SelectedValue.ToString();
    //        if(getVal== "ROFF")
    //        {
    //            //BindReporteeOffEmpDDL(lpm.Emp_Code);
    //        }
    //        else
    //        {
    //            Reporteesoff.Items.Clear();
    //            Reporteesoff.Items.Insert(0, new ListItem("Employee for Reportees Of", "0")); //updated code
    //            Reporteesoff.Enabled = false;

    //            ddlEmployee.Items.Clear();
    //            ddlEmployee.Items.Insert(0, new ListItem("Select Employee", "0")); //updated code
    //            ddlEmployee.Enabled = false;

    //            ddlDirectALLR.Items.Clear();
    //            ddlDirectALLR.Items.Insert(0, new ListItem("All Reportees", "ALLR")); //updated code
    //            ddlDirectALLR.Items.Insert(1, new ListItem("Direct Reportees", "DR")); //updated code
    //            ddlDirectALLR.Items.Insert(2, new ListItem("An Employee", "ANEMP")); //updated code
    //            ddlDirectALLR.Items.FindByValue("ALLR").Selected = true;
    //        }
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}

    //protected void ddlDirectALLR_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        var getVal = ddlDirectALLR.SelectedValue.ToString();
    //        if (getVal == "ANEMP")
    //        {
    //            var getRVal = Reportees.SelectedValue.ToString();
    //            if(getRVal== "ROFF")
    //            {
    //                var getRepoteeVal = Reporteesoff.SelectedValue.ToString();
    //                if(getRepoteeVal=="0")
    //                {
    //                    lblmessage.Text = "Please Select Repotee off";
    //                }
    //                else
    //                {
    //                    BindEmployeeDDL(lpm.Emp_Code, getRepoteeVal, getRVal);
    //                }                    
    //            }
    //            else if(getRVal == "MR")
    //            {
    //                BindEmployeeDDL(lpm.Emp_Code,null, getRVal);
    //            }
    //           else
    //            {
    //                BindEmployeeDDL(lpm.Emp_Code, null, null);
    //            }
    //        }
    //        else
    //        {
    //           // ddlDirectALLR.Enabled = false;
    //            ddlEmployee.Items.Clear();
    //            ddlEmployee.Items.Insert(0, new ListItem("Select Employee", "0")); //updated code
    //            ddlEmployee.Enabled = false;

    //        }
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}

    //protected void Reporteesoff_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        var getVal = ddlDirectALLR.SelectedValue.ToString();
    //        if (getVal == "ANEMP")
    //        {
    //            var getRVal = Reportees.SelectedValue.ToString();
    //            if (getRVal == "ROFF")
    //            {
    //                var getRepoteeVal = Reporteesoff.SelectedValue.ToString();
    //                if (getRepoteeVal == "0")
    //                {
    //                    lblmessage.Text = "Please Select Repotee off";
    //                }
    //                else
    //                {
    //                    BindEmployeeDDL(lpm.Emp_Code, getRepoteeVal, getRVal);
    //                }
    //            }
    //            else if (getRVal == "MR")
    //            {
    //                BindEmployeeDDL(lpm.Emp_Code, null, getRVal);
    //            }
    //            else
    //            {
    //                BindEmployeeDDL(lpm.Emp_Code, null, null);
    //            }
    //        }            
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}
    public void GetYearList()
    {
        try
        {
            var getDtMonthdata = ToDataTable(GetMonths(Convert.ToDateTime("2021-04-25"), Convert.ToDateTime(DateTime.Now.Date)));
            if (getDtMonthdata.Rows.Count > 0)
            {
                //ddlMonthYear.DataSource = null;
                //ddlMonthYear.DataSource = getDtMonthdata;
                ////ddlDepartment.DataBind();
                //ddlMonthYear.DataTextField = "MonthYear";
                //ddlMonthYear.DataValueField = "MonthYear";
                //ddlMonthYear.DataBind();
                //ddlMonthYear.Items.Insert(0, new ListItem("Select Month Year", "0")); //updated code

            }
            else
            {
               // ddlMonthYear.DataSource = null;
            }
        }
        catch (Exception)
        {

            throw;
        }
       
    }
    public static List<string> GetMonths(DateTime date1, DateTime date2)
    {
        //Note - You may change the format of date as required.  
        return GetDates(date1, date2).Select(x => x.ToString("MMM/yyyy", CultureInfo.InvariantCulture)).ToList();
    }


    public static IEnumerable<DateTime> GetDates(DateTime date1, DateTime date2)
    {
        while (date1 <= date2)
        {
            yield return date1;
            date1 = date1.AddMonths(1);
        }
        if (date1 > date2 && date1.Month == date2.Month)
        {
            // Include the last month  
            yield return date1;
        }
    }
    public static DataTable ToDataTable<T>(IList<T> data)
    {
        PropertyDescriptorCollection props =
        TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();

        table.Columns.Add("MonthYear");

        object[] values = new object[props.Count];
        foreach (T item in data)
        {            
            table.Rows.Add(item.ToString());
        }
        return table;
    }

    protected void ddlMonthYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
           // var getDDLGetVal = ddlMonthYear.SelectedValue.ToString();
            var getDDLGetVal = "";
            if(getDDLGetVal!="0")
            {
                lblmessage.Text = "";
                var getDate = txtFromdate.Text.ToString();
                getDDLGetVal = "25/" + getDDLGetVal;
                DateTime StartDate = DateTime.ParseExact(getDDLGetVal, "dd/MMM/yyyy", CultureInfo.InvariantCulture).AddMonths(-1);
                DateTime StartingDate = DateTime.ParseExact("25/04/2021", "dd/MM/yyyy", CultureInfo.InvariantCulture).AddMonths(-1);

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
                var SelectStartDate = StartDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                SelectEndDate = SelectEndDate.Replace('-', '/');
                SelectStartDate = SelectStartDate.Replace('-', '/');
                txtToDate.Text = SelectEndDate;
                txtFromdate.Text = SelectStartDate;
                DateTime SelectedDate = DateTime.ParseExact(SelectStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime Today = DateTime.Now;
                if (StartingDate > SelectedDate)
                {
                    lblmessage.Text = "Please select date from 25 Apr 2021 ";
                    //txtFromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFromdate.Text = "";
                    txtToDate.Text = "";
                }
                else if (Today < SelectedDate)
                {
                    lblmessage.Text = "Please select current month only or less than current month";
                    //txtFromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFromdate.Text = "";
                    txtToDate.Text = "";
                }
                else
                {
                  //// ddlProject.Enabled = true;
                  //  Reportees.Enabled = true;
                  //  ddlDirectALLR.Enabled = true;
                    lblmessage.Text = "";
                }
            }
            else
            {
                lblmessage.Text = "Please Select Month And Year";
                return;
            }        
        }
        catch (Exception ex)
        {

        }

    }

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        lblmessage.Text = "";
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
            var Today1 = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            Today = DateTime.ParseExact(Today1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var toDate1 = DateTime.ParseExact(getDate1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (Today <= toDate1)
            {
                lblmessage.Text = "Please select date less than today";
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
            spars[2].Value = "WFOWFHREPORT";



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


