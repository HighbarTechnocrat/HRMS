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

public partial class TaskMonitoringSupervisorReport : System.Web.UI.Page
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
    public DataTable dtEmp, dtleavebal, dtApprover, dtLeaveTypes, dtApproverEmailIds, dtMaxRequestId, dtIntermediate, dtLeaveCalculation, dtHolidays, dtleavedetails;
    public int Leaveid;
    public int leavetype, openbal, avail, rembal, weekendcount, publicholiday, apprid;
    double totaldays;
    public string filename = "", approveremailaddress, message;
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
                    txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtToDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtDueDateFrom.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtDueDateTo.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    // txtDescription.Attributes.Add("maxlength", "500");
                    //get_Shiftdetails();
                    // get_CheckIn();
                    if (Request.QueryString.Count > 0)
                    {
                        hdnReqid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                    }
                    //txtFromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    // PopulateEmployeeData();
                    BindAllDLL("All");
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
    public void BindAllDLL(string qtype)
    {
        try
        {
            var loginEmp_Code = Convert.ToString(Session["Empcode"]);
            var getDs = spm.getTaskMonitoringReport("getAuditReportDLL", loginEmp_Code, 0, 0);
            if (getDs != null)
            {
                if (getDs.Tables.Count > 0)
                {
                    var getRefIds = getDs.Tables[0];
                    var getTaskIds = getDs.Tables[1];
                    var getExecuter = getDs.Tables[2];
                    var getStatus = getDs.Tables[3];

                    if (qtype == "All")
                    {
                        ddlTaskRefId.DataSource = null;
                        ddlTaskRefId.DataBind();

                        ddlTaskId.DataSource = null;
                        ddlTaskId.DataBind();

                        ddlTaskExecuter.DataSource = null;
                        ddlTaskExecuter.DataBind();

                        ddlStatus.DataSource = null;
                        ddlStatus.DataBind();

                        if (getRefIds != null)
                        {
                            if (getRefIds.Rows.Count > 0)
                            {
                                ddlTaskRefId.DataSource = getRefIds;
                                ddlTaskRefId.DataTextField = "Task_Reference_ID";
                                ddlTaskRefId.DataValueField = "ID";
                                ddlTaskRefId.DataBind();
                                ddlTaskRefId.Items.Insert(0, new ListItem("Select Task Reference id", "0"));
                            }
                        }
                        if (getExecuter != null)
                        {
                            if (getExecuter.Rows.Count > 0)
                            {
                                ddlTaskExecuter.DataSource = getExecuter;
                                ddlTaskExecuter.DataTextField = "Emp_Name";
                                ddlTaskExecuter.DataValueField = "Emp_Code";
                                ddlTaskExecuter.DataBind();
                                ddlTaskExecuter.Items.Insert(0, new ListItem("Select Executor", "0"));
                            }
                        }
                        if (getTaskIds != null)
                        {
                            if (getTaskIds.Rows.Count > 0)
                            {
                                ddlTaskId.DataSource = getTaskIds;
                                ddlTaskId.DataTextField = "Task_ID";
                                ddlTaskId.DataValueField = "ID";
                                ddlTaskId.DataBind();
                                ddlTaskId.Items.Insert(0, new ListItem("Select Task id", "0"));
                            }
                        }
                        if (getStatus != null)
                        {
                            if (getStatus.Rows.Count > 0)
                            {
                                ddlStatus.DataSource = getStatus;
                                ddlStatus.DataTextField = "StatusName";
                                ddlStatus.DataValueField = "Id";
                                ddlStatus.DataBind();
                                ddlStatus.Items.Insert(0, new ListItem("Select Status", "0"));
                            }
                        }
                    }
                    else if (qtype == "Executer")
                    {
                        ddlTaskExecuter.DataSource = null;
                        ddlTaskExecuter.DataBind();
                        if (getExecuter != null)
                        {
                            if (getExecuter.Rows.Count > 0)
                            {
                                ddlTaskExecuter.DataSource = getExecuter;
                                ddlTaskExecuter.DataTextField = "Emp_Name";
                                ddlTaskExecuter.DataValueField = "Emp_Code";
                                ddlTaskExecuter.DataBind();
                                ddlTaskExecuter.Items.Insert(0, new ListItem("Select Executer", "0"));
                            }
                        }
                    }
                }
            }
        }
        catch (Exception)
        {

        }
    }

    #endregion
    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            if (txtFromdate.Text.ToString().Trim() == "")
            {
                lblmessage.Text = "Please select task reference from date";
                return;
            }
            var getDate = txtFromdate.Text.ToString();
            var getTodayDate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime StartDate = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime StartingDate = DateTime.ParseExact(getTodayDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (StartingDate < StartDate)
            {
                lblmessage.Text = "Please select date only today or less than today";
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
            SqlParameter[] spars = new SqlParameter[11];
            string[] strdate;
            string strfromDate = null;
            string strToDate = null;
            string strfromDate1 = null;
            string strToDate1 = null;
            string secoundReportHide = "N";
            string EmpName = "";
            string IsTaskDateShow = "", DueDateTo="", ToDate="";
            string IsTaskRefDateShow = "";
            //@fromDate
            spars[0] = new SqlParameter("@TaskRFDate", SqlDbType.VarChar);
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                //IsTaskRefDateShow = "Task Reference From Date : " + Convert.ToString(txtFromdate.Text).Trim() + "  -  Task Reference To Date : " + Convert.ToString(txtToDate.Text).Trim() + "";
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

                spars[0].Value = strfromDate;
            }
            else
            {
                spars[0].Value = "";
            }
            spars[1] = new SqlParameter("@TaskRTDate", SqlDbType.VarChar);
			
				if (Convert.ToString(txtToDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
				ToDate = Convert.ToString(txtToDate.Text).Trim();

			}
            else
            {
				strToDate = DateTime.Today.ToString("yyyy") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("dd");
				ToDate = DateTime.Today.ToString("dd/MM/yyyy");
			}
			spars[1].Value = strToDate;
			if (Convert.ToString(txtFromdate.Text).Trim() != "")
			{
				IsTaskRefDateShow = "Task Reference From Date : " + Convert.ToString(txtFromdate.Text).Trim() + "  -  Task Reference To Date : " + Convert.ToString(ToDate).Trim() + "";
			}
			spars[2] = new SqlParameter("@TaskDFDate", SqlDbType.VarChar);
            if (Convert.ToString(txtDueDateFrom.Text).Trim() != "")
            {
               // IsTaskDateShow = "From Task Due Date : " + Convert.ToString(txtDueDateFrom.Text).Trim() + "  -   To Task Due Date : " + Convert.ToString(txtDueDateTo.Text).Trim() + "";
                strdate = Convert.ToString(txtDueDateFrom.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

                spars[2].Value = strfromDate;
            }
            else
            {
                spars[2].Value = "";
            }
            spars[3] = new SqlParameter("@TaskDTDate", SqlDbType.VarChar);
            if (Convert.ToString(txtDueDateTo.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtDueDateTo.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
				DueDateTo = Convert.ToString(txtDueDateTo.Text).Trim();
			}
            else
            {
				strToDate = DateTime.Today.ToString("yyyy") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("dd");
				DueDateTo = DateTime.Today.ToString("dd/MM/yyyy");
			}
			spars[3].Value = strToDate;
			if (Convert.ToString(txtDueDateFrom.Text).Trim() != "")
			{
				IsTaskDateShow = "From Task Due Date : " + Convert.ToString(txtDueDateFrom.Text).Trim() + "  -   To Task Due Date : " + Convert.ToString(DueDateTo).Trim() + "";
			}
				spars[4] = new SqlParameter("@Task_Executer", SqlDbType.VarChar);
            if (Convert.ToString(ddlTaskExecuter.SelectedValue).Trim() != "0")
            {
                string stremp;
                string selected_comp_code = "";
                stremp = Convert.ToString(ddlTaskExecuter.SelectedValue).Trim();
                if (stremp != "")
                {
                    selected_comp_code = stremp;
                }

                spars[4].Value = Convert.ToString(selected_comp_code.ToString());
            }
            else
            {
                spars[4].Value = null;
            }
            spars[5] = new SqlParameter("@Task_Ref_Id", SqlDbType.Decimal);
            if (Convert.ToString(ddlTaskRefId.SelectedValue).Trim() != "0")
            {
                string stremp;
                string selected_comp_code = "";
                stremp = Convert.ToString(ddlTaskRefId.SelectedValue).Trim();
                if (stremp != "")
                {
                    selected_comp_code = stremp;
                }

                spars[5].Value = Convert.ToDouble(selected_comp_code.ToString());
            }
            else
            {
                spars[5].Value = null;
            }
            spars[6] = new SqlParameter("@Task_Id", SqlDbType.Decimal);
            if (Convert.ToString(ddlTaskId.SelectedValue).Trim() != "0")
            {
                string stremp;
                string selected_comp_code = "";
                stremp = Convert.ToString(ddlTaskId.SelectedValue).Trim();
                if (stremp != "")
                {
                    selected_comp_code = stremp;
                }

                if (selected_comp_code == "2")
                {
                    spars[6].Value = Convert.ToInt32(selected_comp_code.ToString());
                }
                else
                {
                    spars[6].Value = null;
                }
            }
            else
            {
                spars[6].Value = null;
            }
            spars[7] = new SqlParameter("@StatusId", SqlDbType.Int);
            if (Convert.ToString(ddlStatus.SelectedValue).Trim() != "0")
            {
                string stremp;
                string selected_comp_code = "";
                stremp = Convert.ToString(ddlStatus.SelectedValue).Trim();
                if (stremp != "")
                {
                    selected_comp_code = stremp;
                }

                spars[7].Value = Convert.ToInt32(selected_comp_code.ToString());
            }
            else
            {
                spars[7].Value = null;
            }
            spars[8] = new SqlParameter("@IsOverdueTasks", SqlDbType.Bit);
            spars[8].Value = Convert.ToBoolean(chkOverdueTasksOnly.Checked);

            spars[9] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[9].Value = Session["Empcode"].ToString();

            spars[10] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[10].Value = "SupervisorReport";
            var getLoginId = lpm.Emp_Code;

            dtTimesheetClientwise = spm.getTeamReport(spars, "SP_TASK_M_Report");

            #endregion

            ReportParameter[] param = new ReportParameter[4];

            param[0] = new ReportParameter("StartDate", Convert.ToString(IsTaskDateShow));
            param[1] = new ReportParameter("EndDate", Convert.ToString(IsTaskRefDateShow));
            param[2] = new ReportParameter("secoundReportHide", Convert.ToString(secoundReportHide));
            param[3] = new ReportParameter("EmpName", Convert.ToString(EmpName));

            ReportViewer2.LocalReport.Refresh();
            ReportViewer2.LocalReport.DataSources.Clear();

            if (dtTimesheetClientwise.Rows.Count > 0)
            {
                // Create Report DataSource :- for payment Voucher
                ReportDataSource serviceRequestDS = new ReportDataSource("dsTaskMonitoring", dtTimesheetClientwise);
                ReportViewer2.ProcessingMode = ProcessingMode.Local;
                //ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/PaymentVoucher_reimbursment.rdlc");
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/TaskMonitoringSupervisorReport.rdlc");
                ReportViewer2.LocalReport.DataSources.Add(serviceRequestDS);
                //ReportViewer2.ShowToolBar = true;
            }
            else
            {
                // Create Report DataSource :- for payment Voucher
                ReportDataSource serviceRequestDS = new ReportDataSource("dsTaskMonitoring", dtTimesheetClientwise);
                ReportViewer2.ProcessingMode = ProcessingMode.Local;
                //ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/PaymentVoucher_reimbursment.rdlc");
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/TaskMonitoringSupervisorReport.rdlc");
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
        try
        {
            btnIn.Visible = true;

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

    protected void txtDueDateFrom_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            if (txtDueDateFrom.Text.ToString().Trim() == "")
            {
                lblmessage.Text = "Please select from task due date";
                return;
            }
            var getDate = txtDueDateFrom.Text.ToString();
            var getTodayDate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime StartDate = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime StartingDate = DateTime.ParseExact(getTodayDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (StartingDate < StartDate)
            {
                lblmessage.Text = "Please select date only today or less than today";
                txtDueDateTo.Text = "";
                txtDueDateFrom.Text = "";
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

    protected void txtDueDateTo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            if (txtDueDateFrom.Text.ToString().Trim() == "")
            {
                lblmessage.Text = "Please select from task due date";
                return;
            }
            if (txtDueDateTo.Text.ToString().Trim() == "")
            {
                lblmessage.Text = "Please select to task due date";
                return;
            }
            var getFromDate = txtDueDateFrom.Text.ToString();
            var getToDate = txtDueDateTo.Text.Trim();

            DateTime fromDate = DateTime.ParseExact(getFromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime toDate = DateTime.ParseExact(getToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (toDate < fromDate)
            {
                lblmessage.Text = "Please select to date less than from date";
                txtDueDateTo.Text = "";
                txtDueDateFrom.Text = "";
            }
            else
            {

                lblmessage.Text = "";
            }
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
            lblmessage.Text = "";
            if (txtFromdate.Text.ToString().Trim() == "")
            {
                lblmessage.Text = "Please select task reference from date";
                return;
            }
            if (txtToDate.Text.ToString().Trim() == "")
            {
                lblmessage.Text = "Please select task reference to date";
                return;
            }
            var getFromDate = txtFromdate.Text.ToString();
            var getToDate = txtToDate.Text.Trim();

            DateTime fromDate = DateTime.ParseExact(getFromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime toDate = DateTime.ParseExact(getToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (toDate < fromDate)
            {
                lblmessage.Text = "Please select to date less than from date";
                txtFromdate.Text = "";
                txtToDate.Text = "";
            }
            else
            {

                lblmessage.Text = "";
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void ddlTaskRefId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var getVal = Convert.ToString(ddlTaskRefId.SelectedValue);
            if (getVal != "0")
            {
                txtFromdate.Enabled = false;
                txtToDate.Enabled = false;

                var getExecuter = ddlTaskExecuter.SelectedValue;
                if (getExecuter != null && getExecuter != "" && getExecuter != "0")
                {
                    BindTaskId(Convert.ToDouble(getVal), "getSelectTaskRefExecuter", Convert.ToString(getExecuter));
                }
                else
                {
                    ddlTaskExecuter.DataSource = null;
                    ddlTaskExecuter.DataBind();
                    ddlTaskExecuter.Items.Clear();
                    ddlTaskExecuter.Items.Insert(0, new ListItem("Select Executer", "0"));
                    ddlTaskExecuter.Enabled = false;
                    BindTaskId(Convert.ToDouble(getVal), "getTDSelectTaskRef", Convert.ToString(Session["Empcode"].ToString()));
                }

            }
            else
            {
                ddlTaskExecuter.Enabled = true;
                BindAllDLL("All");
                txtFromdate.Enabled = true;
                txtToDate.Enabled = true;
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlTaskExecuter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var getEmpCode = ddlTaskExecuter.SelectedValue.ToString();
            if (getEmpCode != "0")
            {
                BindTaskRef(getEmpCode);
            }
        }
        catch (Exception)
        {

        }
    }

    protected void ddlTaskId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var getVal = Convert.ToString(ddlTaskId.SelectedValue);
            if (getVal != "0")
            {
                txtDueDateFrom.Enabled = false;
                txtDueDateTo.Enabled = false;

                var getExecuter = ddlTaskExecuter.SelectedValue;
                if (getExecuter != null && getExecuter != "" && getExecuter != "0")
                {
                }
                else
                {
                    ddlTaskExecuter.DataSource = null;
                    ddlTaskExecuter.DataBind();
                    ddlTaskExecuter.Items.Clear();
                    ddlTaskExecuter.Items.Insert(0, new ListItem("Select Executer", "0"));
                    ddlTaskExecuter.Enabled = false;
                }

                var getTaskRefId = ddlTaskRefId.SelectedValue;
                if (getTaskRefId != null && getTaskRefId != "" && getTaskRefId != "0")
                {
                }
                else
                {
                    ddlTaskRefId.DataSource = null;
                    ddlTaskRefId.DataBind();
                    ddlTaskRefId.Items.Clear();
                    ddlTaskRefId.Items.Insert(0, new ListItem("Select Task Reference id", "0"));
                    ddlTaskRefId.Enabled = false;
                }
            }
            else
            {
                ddlTaskExecuter.Enabled = true;
                ddlTaskRefId.Enabled = true;
                BindAllDLL("All");
                txtDueDateFrom.Enabled = true;
                txtDueDateTo.Enabled = true;
            }
        }
        catch (Exception)
        {

        }
    }

    private void CheckIsSupervisor()
    {
        try
        {
            var empCode = Convert.ToString(Session["Empcode"]);
            var getOrginserStatus = spm.getTempAttendee("CheckIsSupervisor", empCode, 0);
            if (getOrginserStatus != null)
            {
                if (getOrginserStatus.Rows.Count > 0)
                {
                    var getMsg = Convert.ToString(getOrginserStatus.Rows[0]["Message"]);
                    if (getMsg != "Exists")
                    {
                        Response.Redirect("~/procs/TaskMonitoring.aspx");
                    }

                }
            }
        }
        catch (Exception)
        {

        }
    }
    private void BindTaskRef(string Executer)
    {
        try
        {
            var getDs = spm.getTaskMonitoringReport("getTRTDSelectExecuter", Executer, 0, 0);
            if (getDs != null)
            {
                if (getDs.Tables.Count > 0)
                {
                    ddlTaskRefId.DataSource = null;
                    ddlTaskRefId.DataBind();

                    ddlTaskId.DataSource = null;
                    ddlTaskId.DataBind();

                    var getRefIds = getDs.Tables[0];
                    var getTaskIds = getDs.Tables[1];

                    if (getRefIds != null)
                    {
                        if (getRefIds.Rows.Count > 0)
                        {
                            ddlTaskRefId.DataSource = getRefIds;
                            ddlTaskRefId.DataTextField = "Task_Reference_ID";
                            ddlTaskRefId.DataValueField = "ID";
                            ddlTaskRefId.DataBind();
                            ddlTaskRefId.Items.Insert(0, new ListItem("Select Task Reference id", "0"));
                        }
                    }
                    if (getTaskIds != null)
                    {
                        if (getTaskIds.Rows.Count > 0)
                        {
                            ddlTaskId.DataSource = getTaskIds;
                            ddlTaskId.DataTextField = "Task_ID";
                            ddlTaskId.DataValueField = "ID";
                            ddlTaskId.DataBind();
                            ddlTaskId.Items.Insert(0, new ListItem("Select Task id", "0"));
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {

        }
    }

    private void BindTaskId(double taskRefId, string qtype, string empCode)
    {
        try
        {
            var getDs = spm.getTaskMonitoringReport(qtype, empCode, taskRefId, 0);
            if (getDs != null)
            {
                if (getDs.Tables.Count > 0)
                {

                    ddlTaskId.DataSource = null;
                    ddlTaskId.DataBind();

                    var getTaskIds = getDs.Tables[0];

                    if (getTaskIds != null)
                    {
                        if (getTaskIds.Rows.Count > 0)
                        {
                            ddlTaskId.DataSource = getTaskIds;
                            ddlTaskId.DataTextField = "Task_ID";
                            ddlTaskId.DataValueField = "ID";
                            ddlTaskId.DataBind();
                            ddlTaskId.Items.Insert(0, new ListItem("Select Task id", "0"));
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {

        }
    }


    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            BindAllDLL("All");
            txtFromdate.Text = "";
            txtToDate.Text = "";
            txtDueDateFrom.Text = "";
            txtDueDateTo.Text = "";
            chkOverdueTasksOnly.Checked = false;
            ReportViewer2.LocalReport.Refresh();
            ReportViewer2.LocalReport.DataSources.Clear();
        }
        catch (Exception)
        {

            throw;
        }
    }
}


