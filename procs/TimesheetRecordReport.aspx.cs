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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public partial class TimesheetRecordReport : System.Web.UI.Page
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

                    GetYearList();
                    PopulateEmployeeData();
                                    
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
        Response.Redirect("Leaves.aspx");
    }

    protected void lnkIndex_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/Index.aspx");
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

    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=HTML.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.Output.Write(PlaceHolder1.FindControl("gvMain"));
        Response.Flush();
        Response.End();
    }

    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            var getDate = txtFromdate.Text.ToString();

            DateTime StartDate = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime StartingDate = DateTime.ParseExact("25/04/2021", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var getDay = DateTime.DaysInMonth(StartDate.Year, StartDate.Month);

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

            var SelectEndDate = EndDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            SelectEndDate = SelectEndDate.Replace('-', '/');
            txtToDate.Text = SelectEndDate;
            DateTime SelectedDate = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
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
        GridView1.DataSource = null;
        GridView1.DataBind();
        string strSelfApprover = "";
        try
        {
            var getdate = txtFromdate.Text.Trim();
            DateTime temp = DateTime.ParseExact(getdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var getDay = DateTime.DaysInMonth(temp.Year, temp.Month);

            var Timesheet_date = temp.ToString("yyyy-MM-dd");
           // lblmessage.Text = Timesheet_date+"-"+ lpm.Emp_Code;
            DataSet dsTimesheet = new DataSet();
            dsTimesheet = spm.GetTimesheetReportEmp(Timesheet_date, lpm.Emp_Code,"Created", (getDay-1));
            if (dsTimesheet.Tables.Count > 0)
            {
                var dsTrDetails = dsTimesheet.Tables[0];

                if (dsTrDetails.Rows.Count > 0)
                {
                    
                    GridView1.DataSource = dsTrDetails;
                    GridView1.DataBind();
                    var dtTotalTime = dsTimesheet.Tables[1];
                    BindHTML(dsTrDetails, dtTotalTime);
                }
                else
                {
                    btnIn.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    [System.Web.Services.WebMethod]
    public static List<string> SearchProject(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";
                //SELECT comp_code,(comp_code+' - '+Location_name) AS ProjectName,Location_name from tbl_hmst_company_Location order by comp_code, Location_name
                strsql = " SELECT distinct comp_code,Location_name from tbl_hmst_company_Location_Timesheet " +
                           "   where Location_name like '%' + @SearchText + '%' or comp_code like '%' + @SearchText + '%' order by comp_code, Location_name asc";
                //strsql = "SELECT distinct Location_name FROM tbl_hmst_company_Location " +
                //           "   where Location_name like '%' + @SearchText + '%' order by Location_name asc";
                cmd.CommandText = strsql;
                cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));

                cmd.Connection = conn;
                conn.Open();
                List<string> employees = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        employees.Add(sdr["comp_code"].ToString()+"/"+sdr["Location_name"].ToString());
                    }
                }
                conn.Close();
                return employees;
            }
        }
    }

    [System.Web.Services.WebMethod]
    public static List<string> SearchTask(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";
                strsql = " Select Activity_Desc,Activity_Id from TBL_HRMS_ACTIVITY" +
                           "   where Activity_Desc not in ('Idle') AND Activity_Desc like '%' + @SearchText + '%' order by Activity_Desc asc";
                //strsql = "SELECT Department_Name FROM tblDepartmentMaster " +
                //           "   where Department_Name like '%' + @SearchText + '%' order by Department_Name asc";

                cmd.CommandText = strsql;
                cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));

                cmd.Connection = conn;
                conn.Open();
                System.Collections.Generic.List<string> employees = new System.Collections.Generic.List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        employees.Add(sdr["Activity_Id"].ToString()+"-"+sdr["Activity_Desc"].ToString());
                    }
                }
                conn.Close();
                return employees;
            }
        }
    }
    //Bind 
    public void BindHTML(DataTable dt, DataTable dtTime)
    {
        //Populating a DataTable from database.
        // DataTable dt = this.GetData();
        //Building an HTML string.
        StringBuilder html = new StringBuilder();
        //Table start.
        html.Append("<table border = '1' cellspacing='0' width='281% !important'; id='gvMain' style='border-collapse: collapse; border-color: black;'>");
        //Building the Header row.
        html.Append("<tr class='GridViewScrollItem'>");
        foreach (DataColumn column in dt.Columns)
        {

            html.Append("<th style='background-color: #C7D3D4;'>");
            html.Append(column.ColumnName);
            html.Append("</th>");

        }
        html.Append("</tr>");
        foreach (DataRow row in dt.Rows)
        {
            html.Append("<tr class='GridViewScrollItem'>");

            foreach (DataColumn column in dt.Columns)
            {
                var name = column.ColumnName.ToString();
                if (name == "Project Name" || name == "Activity Desc" || name== "Total Hours")
                {
                    if(name == "Project Name")
                        html.Append("<td style='width: 4%';> ");
                    else if(name == "Activity Desc")
                        html.Append("<td style='width: 7%';> ");
                    else
                        html.Append("<td style='width: 3%';> ");
                    //row[column.ColumnName] = "W";
                    //cell.BackColor = System.Drawing.Color.FromArgb(252, 92, 84);

                    html.Append(row[column.ColumnName]);
                    html.Append("</td>");
                    // html.Append("<td style='color: rgb(253, 242, 185);background-color: rgb(253, 242, 185);width:8px;'>");
                }
                else
                {
                    var getDate = column.ColumnName.ToString();
                    var splitDate = getDate.Split('-');
                    var finaldate = splitDate[2] + "-" + splitDate[1] + "-" + splitDate[0];
                    var getDt = spm.GetTimesheetRegInbox(lpm.Emp_Code, finaldate);
                    var getVal = Convert.ToString(getDt.Rows[0]["ISWORKING"]);
                    if (getVal == "WK")
                    {
                        html.Append("<td width='20px' style='color: black;background-color: #C0C0C0;width:8px;'>");
                    }
                    else if (getVal == "HO")
                    {
                        html.Append("<td width='20px' style='color: black;background-color: #FF9933;width:8px;'>");
                    }
                    else if (getVal == "LE")
                    {
                        html.Append("<td width='20px' style='color: black;background-color: yellow;width:8px;'>");
                    }
                    else
                    {
                        html.Append("<td width='20px'>");
                    }

                    html.Append(row[column.ColumnName]);
                    //html.Append(row[column.ColumnName]);
                    html.Append("</td>");
                }
                // html.Append("<td>");

            }
            html.Append("</tr>");
        }
        foreach (DataRow row in dtTime.Rows)
        {
            html.Append("<tr class='GridViewScrollItem'>");
            html.Append("<td colspan='2' style='text-align:center !important'>Total</td>");
            foreach (DataColumn column in dtTime.Columns)
            {
                
                html.Append("<td>");
                html.Append(row[column.ColumnName]);
                html.Append("</td>");

                //html.Append(row[column.ColumnName]);

            }
            html.Append("</tr>");
        }
        //Table end.
        html.Append("</table>");
        //Append the HTML string to Placeholder.
        PlaceHolder1.Controls.Add(new Literal { Text = "" });
        PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    public bool checkIsExistProject(string projectName)
    {
        bool blnchk = false;
        try
        {
            var getProjectSplit = projectName.Split('/');
            var getCode = "";
            var getLocation = "";
            if (getProjectSplit.Length == 2)
            {
                getCode = getProjectSplit[0];
                getLocation = getProjectSplit[1];
            }
            else
            {
                return false;
            }

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckProjectExist";

            spars[1] = new SqlParameter("@projectName", SqlDbType.VarChar);
            if (Convert.ToString(getLocation).Trim() != "")
                spars[1].Value = Convert.ToString(getLocation).Trim();

            spars[2] = new SqlParameter("@projectCode", SqlDbType.VarChar);
            if (Convert.ToString(getCode).Trim() != "")
                spars[2].Value = Convert.ToString(getCode).Trim();

            DataTable dtcities = spm.getDataList(spars, "SP_TimesheetCheckProjectANDTask");

            if (dtcities != null)
            {
                if (dtcities.Rows.Count > 0)
                {
                    blnchk = true;
                }
            }
            return blnchk;
        }
        catch (Exception)
        {
            return blnchk;
        }
    }

    public bool CheckIsExistTask(string task)
    {
        bool blnchk = false;
        try
        {
            var gettaskSplit = task.Split('/');
            var getCode = 0;
            var getDescription = "";
            if (gettaskSplit.Length == 2)
            {
                getCode = Convert.ToInt32(gettaskSplit[0]);
                getDescription = gettaskSplit[1];
            }
            else
            {
                return false;
            }

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckTaskExist";

            spars[1] = new SqlParameter("@taskDescripation", SqlDbType.NVarChar);
            if (Convert.ToString(getDescription).Trim() != "")
                spars[1].Value = Convert.ToString(getDescription).Trim();

            spars[2] = new SqlParameter("@taskId", SqlDbType.Int);
            if (Convert.ToString(getCode).Trim() != "")
                spars[2].Value = Convert.ToInt32(getCode);

            DataTable dtcities = spm.getDataList(spars, "SP_TimesheetCheckProjectANDTask");

            if (dtcities != null)
            {
                if (dtcities.Rows.Count > 0)
                {
                    blnchk = true;
                }
            }
            return blnchk;
        }
        catch (Exception)
        {
            return false;
        }
    }

    protected void btnIn_Click(object sender, EventArgs e)
    {
        try
        {
            btnIn.Visible = true;
            var getdate = txtFromdate.Text.Trim();
            if(getdate=="")
            {
                lblmessage.Text = "Please select Month And Year.";
                return;
            }
            getTimesheetDetailsInDate();
        }
        catch (Exception)
        {

            throw;
        }
    }
    public void GetYearList()
    {
        try
        {
            var getDtMonthdata = ToDataTable(GetMonths(Convert.ToDateTime("2021-04-25"), Convert.ToDateTime(DateTime.Now.Date)));
            if (getDtMonthdata.Rows.Count > 0)
            {
                ddlMonthYear.DataSource = null;
                ddlMonthYear.DataSource = getDtMonthdata;
                //ddlDepartment.DataBind();
                ddlMonthYear.DataTextField = "MonthYear";
                ddlMonthYear.DataValueField = "MonthYear";
                ddlMonthYear.DataBind();
                ddlMonthYear.Items.Insert(0, new ListItem("Select Month Year", "0")); //updated code

            }
            else
            {
                ddlMonthYear.DataSource = null;
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
        return GetDates(date1, date2).Select(x => x.ToString("MMM/yyyy", System.Globalization.CultureInfo.InvariantCulture)).ToList();
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
        table.Columns.Add("SrNo", typeof(int));

        object[] values = new object[props.Count];
        int srNo = 0;
        foreach (T item in data)
        {
            srNo += 1;
            table.Rows.Add(item.ToString(), srNo);
        }
        table.DefaultView.Sort = "SrNo desc";
        return table;
    }

    protected void ddlMonthYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            var getDDLGetVal = ddlMonthYear.SelectedValue.ToString();
            if (getDDLGetVal != "0")
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
               // hdnDay.Value = getDayTotal.ToString();
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
                   // ddlProject.Enabled = true;
                   
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
}


