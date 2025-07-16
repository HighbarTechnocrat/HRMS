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
using ClosedXML.Excel;

public partial class TaskMonitoring : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    LeaveBalance bl = new LeaveBalance();
    SP_Methods spm = new SP_Methods();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
			if (!Page.IsPostBack)
			{
				Session["chkbtnStatus_Appr"] = "";
				hflEmpCode.Value = Convert.ToString(Session["Empcode"]);
				CheckIsExecuter();
				CheckIsSupervisor(); 
				BackButtonCall();// Back Button call then prev data show;
                CheckIsAlltaskDataShow();
                CheckIsProjectScheduleShow();
                CheckIsCreateProjectScheduleSetting();
                //Session value remove
                Session.Remove("ddlTaskExecuter");
			   Session.Remove("ddlTaskRefId");
			   Session.Remove("ddlTaskId");
			   Session.Remove("ddlStatus");
			}
		}
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    public void CheckIsAlltaskDataShow()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowTaskReport";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "AllTaskMonitoringReport";



            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_TASK_M_DETAILS");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    lnk_Alltask.Visible = true;
                    // return true;
                }
                else
                {
                    lnk_Alltask.Visible = false;
                    // return false;
                }
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }


    public void CheckIsProjectScheduleShow()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowTaskReportProjectSchedule";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "TaskMonitoringProjectSchedule";



            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_TASK_M_DETAILS");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                var getRights = Convert.ToString(getdtDetails.Rows[0]["Rights"]);
                if (getStatus == "SHOW")
                {
                    HFIDQString.Value = getRights;
                    lnk_ProjectDuration.Visible = true;
                    Lnk_CreateprojectSchedule.Visible = true;
                }
                else
                {
                    lnk_ProjectDuration.Visible = false;
                    Lnk_CreateprojectSchedule.Visible = false;
                }
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }

    public void CheckIsCreateProjectScheduleSetting()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowTaskReportProjectScheduleCreate"; 

             spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
             spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "TaskMonitoringProjectScheduleCreation";



            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_TASK_M_DETAILS");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    Lnk_CreateprojectSchedule.Visible = true;
                }
                else
                {
                    Lnk_CreateprojectSchedule.Visible = false;
                }
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }

    private void BackButtonCall()
	{
		try
		{
			//bank button call  Pending Task Assigned By Me
			if (Convert.ToString(Session["AssignedBy"]).Trim() != "" || Session["AssignedBy"] != null)
			{
				int i = 0;
				if (Convert.ToString(Session["dgselectedIndex"]).Trim() != "" || Session["dgselectedIndex"] != null)
				{
					i = Convert.ToInt32(Session["dgselectedIndex"]);
				}
				string EmpCode = Session["AssignedBy"].ToString();
				hdnAssignedBy.Value = EmpCode;
				getExecutorTaskDetails(EmpCode, "Executor");
				dgSupervisor.Rows[i].CssClass = "dgSupervisor";//select index backgoundcolor			
				//Session.Remove("AssignedBy");
			}
			//bank button call  Pending Task Assigned To Me
			if (Convert.ToString(Session["AssignedTo"]).Trim() != "" || Session["AssignedTo"] != null)
			{
				int i = 0;
				if (Convert.ToString(Session["dgselectedIndexTo"]).Trim() != "" || Session["dgselectedIndexTo"] != null)
				{
					i = Convert.ToInt32(Session["dgselectedIndexTo"]);
				}
				string EmpCode = Session["AssignedTo"].ToString();
				hdnAssignedMI.Value = EmpCode;
				getSupervisorTaskDetails(EmpCode, "Supervisor");
				dgExecutor.Rows[i].CssClass = "dgSupervisor";//select index backgoundcolor			
				//Session.Remove("AssignedTo");
			}
			//bank button call  Pending  Delayed Task 
			if (Convert.ToString(Session["DelayedTask"]).Trim() != "" || Session["DelayedTask"] != null)
			{
				//string EmpCode = Session["DelayedTask"].ToString();			
				getDelayedTaskDetails(); ;
			}

		}
		catch (Exception)
		{

			throw;
		}

	}

    private void CheckIsExecuter()
    {
        try
        {
            var empCode = Convert.ToString(Session["Empcode"]);
            var getOrginserStatus = spm.getTempAttendee("CheckIsExecuter", empCode, 0);
            if (getOrginserStatus != null)
            {
                if (getOrginserStatus.Rows.Count > 0)
                {
                    var getMsg = Convert.ToString(getOrginserStatus.Rows[0]["Message"]);
                    if (getMsg == "Exists")
                    {
                        IsTaskExecuter.Visible = true;
                        IsTaskExecuter1.Visible = true;
                        lnk_ExecuterReport.Visible = true;
                        trReport.Visible = true;
                        var TaskId = "";
                        var TaskRefId = "";
                        var getResult = spm.getTaskExecuterDDL(empCode, "GetTaskExecuterInbox", TaskId, TaskRefId, Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), 0, Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), Convert.ToBoolean(0), 0, Convert.ToBoolean(0), 0, Convert.ToBoolean(0), Convert.ToBoolean(0));
                        var getTaskExecuterList = getResult.Tables[0];

                        if (getTaskExecuterList.Rows.Count > 0)
                        {
                            Inbox_Task.Text = "Inbox (" + getTaskExecuterList.Rows.Count + ")";
                        }
                        else
                        {
                            Inbox_Task.Text = "Inbox";
                        }
                        getPendingTaskListExecutor();
                    }
                }
            }
        }
        catch (Exception ex)
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
                    if (getMsg == "Exists")
                    {
                        IsTaskSupervisor.Visible = true;
                        IsTaskSupervisor1.Visible = true;
                        IsTaskSupervisor2.Visible = true;
                        //
                        lnk_SupervisorReport.Visible = true;
                        lnk_AuditTrialReport.Visible = true;
                        trReport.Visible = true;
                        var getResult = spm.getTaskMonitoringDDL(empCode, 0, "getDueDateChangeRequest");
                        if (getResult != null)
                        {
                            if (getResult.Tables.Count > 0)
                            {
                                var gettable = getResult.Tables[0];
                                if (gettable.Rows.Count > 0)
                                {
                                    DueDate_Change_Inbox.Text = "Due Date Change Request Inbox (" + gettable.Rows.Count + ")";
                                }
                                else
                                {
                                    DueDate_Change_Inbox.Text = "Due Date Change Request Inbox";
                                }
                            }
                        }
                        //
                        var getResult1 = spm.getTaskMonitoringDDL(empCode, 0, "getCloseChangeRequest");
                        if (getResult1 != null)
                        {
                            if (getResult1.Tables.Count > 0)
                            {
                                var gettable = getResult1.Tables[0];
                                if (gettable.Rows.Count > 0)
                                {
                                    Clusore.Text = "Task Closure Inbox (" + gettable.Rows.Count + ")";
                                }
                                else
                                {
                                    Clusore.Text = "Task Closure Inbox";
                                }
                            }
                        }
                        getPendingTaskListSupervisor();
						getDelayedTaskPendingCount();

					}
                }
            }
        }
        catch (Exception)
        {

        }
    }

    private void getPendingTaskListSupervisor()
    {
        DataSet dsleavebal = new DataSet();
        dsleavebal = spm.getPendingTaskList(hflEmpCode.Value.ToString());

        dgSupervisor.DataSource = null;
        dgSupervisor.DataBind();

        if (dsleavebal.Tables[0].Rows.Count > 0)
        {	
				dgSupervisor.DataSource = dsleavebal.Tables[0];
				dgSupervisor.DataBind();
				dgSupervisor.Visible = true;
				dvSupervisor.Visible = true;	
		}
		
	}

    

    private void getPendingTaskListExecutor()
    {
        DataSet dsleavebal = new DataSet();
        dsleavebal = spm.getPendingTaskList(hflEmpCode.Value.ToString());

        dgExecutor.DataSource = null;
        dgExecutor.DataBind();

        if (dsleavebal.Tables[1].Rows.Count > 0)
        {
            dgExecutor.DataSource = dsleavebal.Tables[1];
            dgExecutor.DataBind();
            dgExecutor.Visible = true;
            dvExecutor.Visible = true;
        }
    }

    private void getExecutorTaskDetails(string EmpCode, string Type)
    {
        DataSet dsleavebal = new DataSet();
        dsleavebal = spm.getTaskDetails(EmpCode,hflEmpCode.Value.ToString(), Type);

            dgTaskDetails.DataSource = null;
            dgTaskDetails.DataBind();

            if (dsleavebal.Tables[0].Rows.Count > 0)
            {
                dgTaskDetails.DataSource = dsleavebal.Tables[0];
                dgTaskDetails.DataBind();
                dgTaskDetails.Visible = true;
                dvTaskDetails.Visible = true;
            }
    }
	private void getSupervisorTaskDetails(string EmpCode, string Type)
	{
		DataSet dsleavebal = new DataSet();
		dsleavebal = spm.getTaskDetails(EmpCode, hflEmpCode.Value.ToString(), Type);

		dgExecutorDetails.DataSource = null;
		dgExecutorDetails.DataBind();

		if (dsleavebal.Tables[0].Rows.Count > 0)
		{
			dgExecutorDetails.DataSource = dsleavebal.Tables[0];
			dgExecutorDetails.DataBind();
			dgExecutorDetails.Visible = true;
			DivExecutor1.Visible = true;
		}
	}

	protected void dgSupervisor_SelectedIndexChanged(object sender, EventArgs e)
    {
		Session.Remove("dgselectedIndex");
		Session.Remove("AssignedBy");
		int selectedIndex = dgSupervisor.SelectedIndex;
        string EmpCode = (dgSupervisor.DataKeys[selectedIndex]["Task_Executer"]).ToString();
		getExecutorTaskDetails(EmpCode, "Executor");
		hdnAssignedBy.Value = EmpCode;
		Session["dgselectedIndex"] = selectedIndex;
		foreach (GridViewRow row in dgSupervisor.Rows)
		{
			row.CssClass = row.CssClass.Replace("dgSupervisor", String.Empty);
		}
	}
    
    protected void dgExecutor_SelectedIndexChanged(object sender, EventArgs e)
    {
		Session.Remove("dgselectedIndexTo");
		Session.Remove("AssignedTo");
		int selectedIndex = dgExecutor.SelectedIndex;
        string EmpCode = (dgExecutor.DataKeys[selectedIndex]["Task_Supervisor"]).ToString();
		getSupervisorTaskDetails(EmpCode, "Supervisor");
		hdnAssignedMI.Value = EmpCode;
		Session["dgselectedIndexTo"] = selectedIndex;
		foreach (GridViewRow row in dgExecutor.Rows)
		{
			row.CssClass = row.CssClass.Replace("dgSupervisor", String.Empty);
		}



	}

    protected void dgSupervisor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");
			
		}

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");
			e.Row.Cells[1].Attributes.Add("onmouseover", "this.style.color='#F28820'");
			e.Row.Cells[1].Attributes.Add("onmouseout", "this.style.color='Blue'");
			e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.dgSupervisor, "Select$" + e.Row.RowIndex);
        }
    }
    protected void dgExecutor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");
			e.Row.Cells[1].Attributes.Add("onmouseover", "this.style.color='#F28820'");
			e.Row.Cells[1].Attributes.Add("onmouseout", "this.style.color='Blue'");
			e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.dgExecutor, "Select$" + e.Row.RowIndex);
        }
    }

	protected void MyTask_Edit_Click(object sender, EventArgs e)
	{
		try
		{
			//Session.Remove("AssignedBy");
			LinkButton btn = (LinkButton)sender;
			string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
			string TaskId = commandArgs[0];
			string Task_Ref_Id = commandArgs[1];			
			Session["AssignedBy"] = hdnAssignedBy.Value;
			Response.Redirect("~/procs/TaskExecuter_Edit.aspx?Task_Id=" + TaskId + "&TaskRefId=" + Task_Ref_Id + "&flag=" + "sp");
		}
		catch (Exception)
		{
			throw;
		}
	}

	protected void Task_Edit_Click(object sender, EventArgs e)
	{
		try
		{
			//Session.Remove("AssignedTo");
			LinkButton btn = (LinkButton)sender;
			string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
			string TaskId = commandArgs[0];
			string Task_Ref_Id = commandArgs[1];
			Session["AssignedTo"] = hdnAssignedMI.Value;
			Response.Redirect("~/procs/TaskExecuter_Edit.aspx?Task_Id=" + TaskId + "&TaskRefId=" + Task_Ref_Id + "&flag=" + "NA");
		}
		catch (Exception)
		{
			throw;
		}
	}

	protected void dgDelayed_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.Header)
		{
			e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");
		}

		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");
			e.Row.Cells[1].Attributes.Add("onmouseover", "this.style.color='Blue'");
			e.Row.Cells[1].Attributes.Add("onmouseout", "this.style.color='#F28820'");
			e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.dgDelayed, "Select$" + e.Row.RowIndex);
		}
	}

	protected void dgDelayed_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{			
			Session.Remove("DelayedTask");
			string TaskRef_ID = "";
			hdnDelayedList.Value = "";
			getDelayedTaskDetails();
			
		}
		catch (Exception)
		{

			throw;
		}
	}

	private void getDelayedTaskPendingCount()
	{
		DataTable dtresult = new DataTable();
		try
		{
			SqlParameter[] spars = new SqlParameter[3];
			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = "GetDelayedTaskPendingCount";
			spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(Session["Empcode"]);
			dtresult = spm.getMobileRemDataList(spars, "SP_TASK_M_DETAILS_DASHBOARD");
			if (dtresult.Rows.Count > 0)
			{
				if (Convert.ToInt32(dtresult.Rows[0]["DelayedTask"]) > 0)
				{
					dgDelayed.DataSource = dtresult;
					dgDelayed.DataBind();
					dvDelayed.Visible = true;
				}
			}
		}
		catch (Exception)
		{

			throw;
		}
	}
	private void getDelayedTaskDetails()
	{
		DataTable dtresult = new  DataTable();
		try
		{
			SqlParameter[] spars = new SqlParameter[3];
			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = "GetDelayedTaskList";
			spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(Session["Empcode"]);
			dtresult = spm.getMobileRemDataList(spars, "SP_TASK_M_DETAILS_DASHBOARD");
			GRDDelayedList.DataSource = null;
			GRDDelayedList.DataBind();
			if (dtresult.Rows.Count > 0)
			{
				GRDDelayedList.DataSource = dtresult;
				GRDDelayedList.DataBind();

				if (DvDelayedList.Visible && hdnDelayedList.Value == "")
				{
					DvDelayedList.Visible = false;
					dgDelayed.Rows[0].CssClass = "dgDelayedHide";
					Session.Remove("DelayedTask");
				}
				else
				{
					dgDelayed.Rows[0].CssClass = "dgDelayedShow";
					DvDelayedList.Visible = true;
				}
				
			}

			}
		catch (Exception)
		{

			throw;
		}
    }
	protected void DelayTask_Edit_Click(object sender, EventArgs e)
	{
		try
		{
			LinkButton btn = (LinkButton)sender;
			string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
			string TaskId = commandArgs[0];
			string Task_Ref_Id = commandArgs[1];
			Session["DelayedTask"] = "Yes";
			Response.Redirect("~/procs/TaskExecuter_Edit.aspx?Task_Id=" + TaskId + "&TaskRefId=" + Task_Ref_Id + "&flag=" + "sp");
		}
		catch (Exception)
		{
			throw;
		}
	}

	protected void btnExcelDownload_Click(object sender, EventArgs e)
	{
		
	}

	protected void GRDDelayedList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		try
		{
			hdnDelayedList.Value = "1";
			GRDDelayedList.PageIndex = e.NewPageIndex;
			getDelayedTaskDetails();
			

		}
		catch (Exception)
		{

			throw;
		}
	}

	protected void lnk_Download_Click(object sender, ImageClickEventArgs e)
	{
		DataTable dtresult = new DataTable();
		try
		{
			SqlParameter[] spars = new SqlParameter[3];
			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value ="GetDelayedTaskListdownload"; //"GetDelayedTaskList";
			spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(Session["Empcode"]);
			dtresult = spm.getMobileRemDataList(spars, "SP_TASK_M_DETAILS_DASHBOARD");
			if (dtresult.Rows.Count > 0)
			{
				var newTable = new DataTable();
				newTable.Columns.Add("Executor Name");
				newTable.Columns.Add("Task ID");
				newTable.Columns.Add("Task Description");
                newTable.Columns.Add("Project / Location");
                newTable.Columns.Add("Original Due Date");
				newTable.Columns.Add("Revised Due Date");
				newTable.Columns.Add("Due Date Change Count");
				newTable.Columns.Add("Status");
				foreach (DataRow item in dtresult.Rows)
				{
					DataRow _dr = newTable.NewRow();
					_dr["Executor Name"] = item["Emp_Name"].ToString();
					_dr["Task ID"] = item["Task_ID"].ToString();
					_dr["Task Description"] = item["Task_Description"].ToString();
                    _dr["Project / Location"] = item["Location_name"].ToString();
                    _dr["Original Due Date"] = item["OriginalDueDate"].ToString();
					_dr["Revised Due Date"] = item["Due_Date"].ToString();
					_dr["Due Date Change Count"] = item["Due_DateCount"].ToString();
					_dr["Status"] = item["StatusName"].ToString();
					newTable.Rows.Add(_dr);
				}
				var dateTime = DateTime.Now.ToString("ddMMyyyyHHmmss");
				var excelName = "Delayed TaskList_" + dateTime;

				var aCode = 65;
				//var excelName = "IR_Sheet_";
				using (XLWorkbook wb = new XLWorkbook())
				{

					var ws = wb.Worksheets.Add("Task Details");
					int rowIndex = 1; int i = 1;
					int columnIndex = 0;
					foreach (DataColumn column in newTable.Columns)
					{
						ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Value = column.ColumnName;
						ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Font.Bold = true;
						ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
						ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Fill.BackgroundColor = XLColor.Gainsboro;
						ws.Column(i).Width = 25;
						columnIndex++; i++;
					}
					rowIndex++;
					foreach (DataRow row in newTable.Rows)
					{
						int valueCount = 0;
						foreach (object rowValue in row.ItemArray)
						{
							ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Value = rowValue;
							ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
							ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Style.Alignment.WrapText = true;
							valueCount++;
						}
						rowIndex++;
					}
					Response.Clear();
					Response.Buffer = true;
					Response.Charset = "";
					Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
					Response.AddHeader("content-disposition", "attachment;filename=" + excelName + ".xlsx");
					using (MemoryStream MyMemoryStream = new MemoryStream())
					{
						wb.SaveAs(MyMemoryStream);
						MyMemoryStream.WriteTo(Response.OutputStream);
						Response.Flush();
						Response.End();
					}
				}
			}
		}
		catch (Exception)
		{

			throw;
		}

	}

    protected void lnk_ProjectDuration_Click(object sender, EventArgs e)
    {
        Response.Redirect("Task_ProjectDurationInfo.aspx?ID=" + HFIDQString.Value);
    }
}
