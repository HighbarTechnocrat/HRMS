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
using System.Linq;
using ClosedXML.Excel;
using System.Data.SqlClient;
using System.Collections.Specialized;

public partial class TaskExecuter_Edit : System.Web.UI.Page
{

    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    SP_Methods spm = new SP_Methods();
    DataSet dspaymentVoucher_Apprs = new DataSet();
    String CEOInList = "N";
    double YearlymobileAmount = 0;



    private void Page_Load(object sender, System.EventArgs e)
    {

        var Task_Id = "";
        var TaskRefId = "";
        var TaskSubId = "";
        DataTable dataTable = new DataTable();
        try
        {
            var empCode = Convert.ToString(Session["Empcode"]);
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            //mobile_btnPrintPV.Visible = false;

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {

                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    txt_TaskDescripation.Attributes.Add("maxlength", "500");
                    txt_TaskRemark.Attributes.Add("maxlength", "500");
                    txt_ActionRemarks.Attributes.Add("maxlength", "500");

                    txt_R_Due_Date.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txt_R_NewDue_Date.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    hdnTaskStatus_Id.Value = "0";

                    var flagType = Convert.ToString(Request.QueryString[2]);
                    if (flagType == "sp")
                    {
                        if (Request.QueryString["Task_Id"] != null && Request.QueryString["Task_Id"] != string.Empty)
                        {

                            Task_Id = Request.QueryString["Task_Id"];

                        }
                        if (Request.QueryString["TaskRefId"] != null && Request.QueryString["TaskRefId"] != string.Empty)
                        {

                            TaskRefId = Request.QueryString["TaskRefId"];

                        }
                        dg_ATT_Details.Visible = false;
                        hdnTaskID.Value = Task_Id;
                        hdnTaskRefID.Value = TaskRefId;
                        hdnAttendeeID.Value = "0";
                        hdnTask_Executer.Value = "0";
                        hdnTask_Supervisor.Value = "0";
                        dv_SubTaskHistoryList.Visible = false;
                        editform.Visible = true;
                        ActionDate.Visible = false;
                        Status.Visible = false;
                        ActionBy.Visible = false;
                        Remarks.Visible = false;
                        file.Visible = false;
                        ddl_Action_Type.Visible = false;
                        //   UploadFileHide.Visible = false;
                        lnk_Task_Create.Visible = false;
                        actionHide.Visible = false;
                        BindTaskReferenceDetails(Task_Id);

                        BindAttendeeListBox(TaskRefId);
                        BindIntimationListBox(TaskRefId);

                        BindTaskDetails(Task_Id);

                        BindData("All");

                        // BindActionTask("GetActionStatusList");

                        BindTaskList();

                        BindUploadedFile();

                        BindSubTaskList();

                        if (!string.IsNullOrEmpty((string)Session["AssignedBy"]))
                        {
                            BanktoIndex.Visible = true;
                        }
                        else if (!string.IsNullOrEmpty((string)Session["DelayedTask"]))
                        {
                            BanktoIndex.Visible = true;
                        }
                        else
                        {
                            // HideParam();
                            backToArr.Visible = true;
                        }
                    }
                    else if (flagType == "ep")
                    {
                        backToSPOC.Visible = true;
                        if (Request.QueryString["Task_Id"] != null && Request.QueryString["Task_Id"] != string.Empty)
                        {

                            Task_Id = Request.QueryString["Task_Id"];

                        }
                        if (Request.QueryString["TaskRefId"] != null && Request.QueryString["TaskRefId"] != string.Empty)
                        {

                            TaskRefId = Request.QueryString["TaskRefId"];

                        }
                        dg_ATT_Details.Visible = false;
                        hdnTaskID.Value = Task_Id;
                        hdnTaskRefID.Value = TaskRefId;
                        hdnAttendeeID.Value = "0";
                        hdnTask_Executer.Value = "0";
                        hdnTask_Supervisor.Value = "0";
                        dv_SubTaskHistoryList.Visible = false;
                        editform.Visible = true;
                        ActionDate.Visible = false;
                        Status.Visible = false;
                        ActionBy.Visible = false;
                        Remarks.Visible = false;
                        file.Visible = false;
                        ddl_Action_Type.Visible = false;
                        //   UploadFileHide.Visible = false;
                        lnk_Task_Create.Visible = false;
                        actionHide.Visible = false;
                        BindTaskReferenceDetails(Task_Id);

                        BindAttendeeListBox(TaskRefId);
                        BindIntimationListBox(TaskRefId);

                        BindTaskDetails(Task_Id);

                        BindData("All");

                        // BindActionTask("GetActionStatusList");

                        BindTaskList();

                        BindUploadedFile();

                        BindSubTaskList();
                        // HideParam();
                        // backToArr.Visible = true;
                    }
                    else
                    {

                        FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["TaskMonitoringPath"]).Trim());

                        if (Request.QueryString["Task_Id"] != null && Request.QueryString["Task_Id"] != string.Empty)
                        {

                            Task_Id = Request.QueryString["Task_Id"];

                        }
                        hdnTaskID.Value = Task_Id;

                        if (Request.QueryString["TaskRefId"] != null && Request.QueryString["TaskRefId"] != string.Empty)
                        {
                            TaskRefId = Request.QueryString["TaskRefId"];

                        }

                        hdnTaskRefID.Value = TaskRefId;
                        empCode = Convert.ToString(Session["Empcode"]);
                        hdnAttendeeID.Value = "0";
                        hdnTask_Executer.Value = "0";
                        hdnTask_Supervisor.Value = "0";
                        var getVal = Convert.ToDouble(TaskRefId);
                        dv_SubTaskHistoryList.Visible = false;
                        editform.Visible = true;
                        ActionDate.Visible = false;
                        Status.Visible = false;
                        ActionBy.Visible = false;
                        Remarks.Visible = false;
                        file.Visible = false;
                        BindTaskReferenceDetails(Task_Id);
                        BindAttendeeListBox(TaskRefId);
                        BindIntimationListBox(TaskRefId);
                        BindTaskDetails(Task_Id);
                        BindData("All");
                        if (HDMeeting_Type_Id.Value == "8")
                        {
                            UploadFileProject.Visible = true;
                            BindActionTask("GetActionStatusListProjectSchedule");
                            IDUploadFileSpanProjectSchedule.Visible = true;

                        }
                        else
                        {
                            uploadfile.Visible = true;
                            BindActionTask("GetActionStatusList");
                        }


                        BindTaskList();
                        BindUploadedFile();
                        // BindUploadedFileExecute();
                        BindSubTaskList();
                        if (!string.IsNullOrEmpty((string)Session["AssignedTo"]))
                        {
                            BanktoIndex.Visible = true;
                        }
                        else
                        {
                            backToEmployee.Visible = true;
                        }

                    }

                    // check sub task or not
                    if (Request.QueryString["Task_Id"] != null && Request.QueryString["Task_Id"] != string.Empty)
                    {

                        Task_Id = Request.QueryString["Task_Id"];

                    }
                    var GetSubTask = spm.getTaskExecuterDDL(empCode, "GetSubTaskList", Task_Id, "", "", "", "", 0, "", "", "", "", "", "", "", "", "", Convert.ToBoolean(0), 0, Convert.ToBoolean(0), 0, Convert.ToBoolean(0), Convert.ToBoolean(0));
                    var getSubTaskList = GetSubTask.Tables[0];

                    if (getSubTaskList.Rows.Count == 0)
                    {
                        AssignSubTask.Visible = false;
                        SubTaskHistory.Visible = false;
                        SubTskHistoruDetails.Visible = false;
                        subtask.Visible = false;
                        SubTaskDtl.Visible = false;
                        Assigntskhr.Visible = false;

                    }
                    else
                    {

                    }


                }


                else
                {

                }
            }
        }
        catch (Exception ex)
        {

            ErrorLog.WriteError(ex.ToString());
        }
    }

    private void BindData(string qtype)
    {
        try
        {

            var empCode = Convert.ToString(Session["Empcode"]);

            var getVal = Convert.ToDouble(hdnTaskRefID.Value);

            var getResult = spm.getTaskMonitoringDDL(empCode, getVal, "GetAllDDL");

            if (getResult != null)
            {
                if (getResult.Tables.Count > 0)
                {

                    if (qtype == "All")
                    {
                        var getMeetingType = getResult.Tables[0];
                        //BindMettingType(getMeetingType);
                        //var getTask_M_Reference = getResult.Tables[1];
                        // BindMettingRefId(getTask_M_Reference);
                        var getAttendeeDDL = getResult.Tables[2];
                        BindAttendee(getAttendeeDDL);
                        BindSupervisor(getAttendeeDDL);
                        BindExecutor(getAttendeeDDL);

                        var getTaskMR_Details = getResult.Tables[3];
                        if (getTaskMR_Details.Rows.Count > 0)
                        {
                            hdnTaskRefID.Value = Convert.ToString(getTaskMR_Details.Rows[0]["ID"]);
                            txt_Ref_Date.Text = Convert.ToString(getTaskMR_Details.Rows[0]["Task_Reference_Date"]);
                            txt_TaskRefId.Text = Convert.ToString(getTaskMR_Details.Rows[0]["Task_Reference_Id"]);
                            var Meeting_Type_Id = Convert.ToString(getTaskMR_Details.Rows[0]["Meeting_Type_Id"]);
                            HDMeeting_Type_Id.Value = Meeting_Type_Id;
                            var mettingId = ddl_Meeting_Type.SelectedValue.ToString();
                            ddl_Meeting_Type.Items.FindByValue(mettingId).Selected = false;
                            ddl_Meeting_Type.Items.FindByValue(Meeting_Type_Id).Selected = true;

                            //var Old_Task_Ref_Id = Convert.ToString(getTaskMR_Details.Rows[0]["Old_Task_Ref_Id"]);
                            //if (Old_Task_Ref_Id != "")
                            //{
                            //    var OldTaskRefId = ddl_Meeting_Id.SelectedValue.ToString();
                            //    ddl_Meeting_Id.Items.FindByValue(OldTaskRefId).Selected = false;
                            //    ddl_Meeting_Type.Items.FindByValue(Meeting_Type_Id).Selected = true;
                            //}

                        }
                        else
                        {
                            hdnTaskRefID.Value = "0";
                        }
                        var getTaskDetails = getResult.Tables[4];
                        if (getTaskDetails.Rows.Count > 0)
                        {
                            BindGridTask(getTaskDetails);
                        }
                        var getGridAttendee = getResult.Tables[5];
                        if (getGridAttendee.Rows.Count > 0)
                        {
                            BindGridAttendee(getGridAttendee);
                        }
                    }
                    else if (qtype == "BindDDLAttendee")
                    {
                        var getAttendeeDDL = getResult.Tables[2];
                        BindAttendee(getAttendeeDDL);
                    }

                }
            }

        }
        catch (Exception ex)
        {

        }
    }

    private void BindTaskReferenceDetails(string TaskId)
    {

        try
        {
            var empCode = Convert.ToString(Session["Empcode"]);
            var GetExecuterEditDetails = spm.getTaskExecuterDDL(empCode, "TaskExecuterRefDetails", TaskId, "", "", "", "", 0, "", "", "", "", "", "", "", "", "", Convert.ToBoolean(0), 0, Convert.ToBoolean(0), 0, Convert.ToBoolean(0), Convert.ToBoolean(0));
            var getTaskExecuterList = GetExecuterEditDetails.Tables[0];

            ddl_Meeting_Type.Items.Add(getTaskExecuterList.Rows[0]["Name"].ToString());
            // ddl_Meeting_Type.Items.FindByText(Convert.ToString(getTaskExecuterList.Rows[0]["Name"].ToString())).Selected = true;
            // ddl_Meeting_Type.DataSource = getTaskExecuterList;
            //ddl_Meeting_Type.SelectedValue = Convert.ToString(getTaskExecuterList.Rows[0]["Name"].ToString());
            txt_Ref_Date.Text = Convert.ToString(getTaskExecuterList.Rows[0]["Task_Creation_Date"].ToString());
            txt_Metting_Title.Text = Convert.ToString(getTaskExecuterList.Rows[0]["Meeting_Discussion_Title"].ToString());
            txt_Metting_Dis.Text = Convert.ToString(getTaskExecuterList.Rows[0]["Meeting_Discussion_Date"].ToString());
        }
        catch (Exception ex)
        {

        }

    }

    private void BindAttendeeListBox(string TaskRefId)
    {
        try
        {

            var empCode = Convert.ToString(Session["Empcode"]);
            var GetAttendees = spm.getTaskExecuterDDL(empCode, "TaskExecuterAttendeesList", "", TaskRefId, "", "", "", 0, "", "", "", "", "", "", "", "", "", Convert.ToBoolean(0), 0, Convert.ToBoolean(0), 0, Convert.ToBoolean(0), Convert.ToBoolean(0));
            var getTaskAttendeesList = GetAttendees.Tables[0];
            txt_Attendees.Text = Convert.ToString(getTaskAttendeesList.Rows[0]["Organizer"].ToString());
            txt_Attendeess_Member.Text = Convert.ToString(getTaskAttendeesList.Rows[0]["AttendeesList"].ToString());
            //AttendeesMember.Items.Add(getTaskAttendeesList.Rows[0]["AttendeesList"].ToString());

        }
        catch (Exception ex)
        {

        }

    }

    private void BindIntimationListBox(string TaskRefId)
    {
        try
        {
            var empCode = Convert.ToString(Session["Empcode"]);
            var GetAttendees = spm.getTaskExecuterDDL(empCode, "TaskExecuterIntimationList", "", TaskRefId, "", "", "", 0, "", "", "", "", "", "", "", "", "", Convert.ToBoolean(0), 0, Convert.ToBoolean(0), 0, Convert.ToBoolean(0), Convert.ToBoolean(0));
            var getTaskAttendeesList = GetAttendees.Tables[0];
            Txt_TaskIntimation.Text = Convert.ToString(getTaskAttendeesList.Rows[0]["AttendeesList"].ToString());
        }
        catch (Exception ex)
        {

        }

    }

    private void BindTaskDetails(string Task_Id)
    {

        var empCode = Convert.ToString(Session["Empcode"]);
        var GetExecuterEditDetails = spm.getTaskExecuterDDL(empCode, "TaskExecuterRefDetails", Task_Id, "", "", "", "", 0, "", "", "", "", "", "", "", "", "", Convert.ToBoolean(0), 0, Convert.ToBoolean(0), 0, Convert.ToBoolean(0), Convert.ToBoolean(0));
        var getTaskExecuterList = GetExecuterEditDetails.Tables[0];

        hdnTask_Executer.Value = Convert.ToString(getTaskExecuterList.Rows[0]["Task_Executer"].ToString());
        hdnTask_Supervisor.Value = Convert.ToString(getTaskExecuterList.Rows[0]["Task_Supervisor"].ToString());
        hdnTaskStatus_Id.Value = Convert.ToString(getTaskExecuterList.Rows[0]["StatusId"].ToString());
        var SubSupervisor = Convert.ToString(getTaskExecuterList.Rows[0]["Emp_Code"].ToString());
        Session["SubSupervisor"] = SubSupervisor;
        txt_Supervisor.Text = Convert.ToString(getTaskExecuterList.Rows[0]["ActionBy"].ToString());
        txt_TaskId.Text = Convert.ToString(getTaskExecuterList.Rows[0]["Task_Id"].ToString());
        txt_Created_Date.Text = Convert.ToString(getTaskExecuterList.Rows[0]["Task_Creation_Date"].ToString());
        txt_Created_By.Text = Convert.ToString(getTaskExecuterList.Rows[0]["Created_by"].ToString());
        txt_Task_Supervisor.Text = Convert.ToString(getTaskExecuterList.Rows[0]["Supervisor"].ToString());
        Session["Supervisor_Id"] = Convert.ToString(getTaskExecuterList.Rows[0]["Task_Supervisor"].ToString());
        txt_Task_Executor.Text = Convert.ToString(getTaskExecuterList.Rows[0]["Executor"].ToString());

        TxtProjectLocation.Text = Convert.ToString(getTaskExecuterList.Rows[0]["Location_name"].ToString());

        Session["Executer_Id"] = Convert.ToString(getTaskExecuterList.Rows[0]["Task_Executer"].ToString());
        txt_TaskDescripation.Text = Convert.ToString(getTaskExecuterList.Rows[0]["Task_Description"].ToString());
        txt_TaskRemark.Text = Convert.ToString(getTaskExecuterList.Rows[0]["Task_Remarks"].ToString());

        txt_Due_Date.Text = Convert.ToString(getTaskExecuterList.Rows[0]["Due_Date"].ToString());
        txt_Reminder_Day.Text = Convert.ToString(getTaskExecuterList.Rows[0]["Reminder_Before_Days"].ToString());
        txt_Reminder_Repe_Day.Text = Convert.ToString(getTaskExecuterList.Rows[0]["Escalation_Mail_Frequency_Days"].ToString());
        var CheckReminderDueDate = Convert.ToString(getTaskExecuterList.Rows[0]["Remainder_Before_Due_Date"].ToString());
        var RepeatedEscalationCheck = Convert.ToString(getTaskExecuterList.Rows[0]["Repeated_Escalation_Mail"].ToString());
        var EscalationCheck = Convert.ToString(getTaskExecuterList.Rows[0]["Escalation_Mail_After_Due_Date"].ToString());

        if (CheckReminderDueDate == "True")
        {
            chk_Reminder_Due_Date.Checked = true;
        }
        if (EscalationCheck == "True")
        {
            chk_Escalation_Due_Date.Checked = true;
        }
        if (RepeatedEscalationCheck == "True")
        {
            chk_Escalation_Repeate.Checked = true;
        }

    }

    //private void HideParam()
    //{
    //    lblAssignTask.Visible = false;
    //    ActionRemarls.Visible = false;
    //    SubTskHistoruDetails.Visible = true;
    //    SubTaskHistory.Visible = true;
    //    AssignSubTask.Visible = true;
    //    UploadedFile.Visible = true;
    //    txt_ActionRemarks.Visible = false;
    //    SuperVisor.Visible = false;
    //    Executer.Visible = false;
    //    DueDate.Visible = false;
    //    ReminderDueDate.Visible = false;
    //    ReminderDays.Visible = false;
    //    EsclationDueDate.Visible = false;
    //    RepeatedEsclation.Visible = false;
    //    EsclationFreq.Visible = false;
    //    NewDueDate.Visible = false;

    //    lbl_TaskHistoryDetailsById.Visible = false;
    //    lbl_ActionDate_Hdtls.Visible = false;
    //    txt_ActionDate_Hdtls.Visible = false;
    //    lbl_DueDate_Hdtls.Visible = false;
    //    txt_DueDate_Hdtls.Visible = false;
    //    lbl_Action_Hdtls.Visible = false;
    //    txt_Action_Hdtls.Visible = false;
    //    lbl_Supervisor_Hdtls.Visible = false;
    //    lbl_Executer_Hdtls.Visible = false;
    //    txt_Executer_Hdtls.Visible = false;
    //    lbl_ActionBy_Hdtls.Visible = false;
    //    txt_ActionBy_Hdtls.Visible = false;
    //    lbl_Remarks_Hdtls.Visible = false;
    //    txt_Remarks_Hdtls.Visible = false;
    //}

    protected void ActionTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AssgineTask1.Visible = false;
            AssgineTask2.Visible = false;
            AssgineTask3.Visible = false;
            AssgineTask4.Visible = false;
            AssgineTask5.Visible = false;
            AssgineTask6.Visible = false;
            AssgineTask7.Visible = false;
            AssgineTask8.Visible = false;
            IsShowDueDate.Visible = false;
            NewDueDate1.Visible = false;
            IsShowActionRemark.Visible = false;
            // IsShowUploadFile.Visible = false;
            IsShowButton.Visible = false;
            txt_ActionRemarks.Text = "";
            txt_R_NewDue_Date.Text = "";
            if (ddl_Action_Type.SelectedValue == "Select Action")
            {
                AssgineTask1.Visible = false;
                AssgineTask2.Visible = false;
                AssgineTask3.Visible = false;
                AssgineTask4.Visible = false;
                AssgineTask5.Visible = false;
                AssgineTask6.Visible = false;
                AssgineTask7.Visible = false;
                AssgineTask8.Visible = false;
                IsShowDueDate.Visible = false;
                IsShowActionRemark.Visible = false;
                //  IsShowUploadFile.Visible = false;
                IsShowButton.Visible = false;
            }
            else if (ddl_Action_Type.SelectedValue == "1")
            {
                IsShowButton.Visible = true;
                lblAssignTask.Visible = true;
                lblAssignTask.InnerText = "Assgine Task Details";
                AssgineTask1.Visible = true;
                AssgineTask2.Visible = true;
                AssgineTask3.Visible = true;
                AssgineTask4.Visible = true;
                AssgineTask5.Visible = true;
                AssgineTask6.Visible = true;
                AssgineTask7.Visible = true;
                AssgineTask8.Visible = true;
                IsShowActionRemark.Visible = true;
                IsShowUploadFile.Visible = true;
            }
            else if (ddl_Action_Type.SelectedValue == "8")
            {
                IsShowButton.Visible = true;
                lblAssignTask.Visible = true;
                lblAssignTask.InnerText = "Close request";
                IsShowActionRemark.Visible = true;
                IsShowUploadFile.Visible = true;

            }
            else if (ddl_Action_Type.SelectedValue == "5")
            {
                lblAssignTask.Visible = true;
                lblAssignTask.InnerText = "Change Due date request";
                IsShowButton.Visible = true;
                IsShowActionRemark.Visible = true;
                IsShowDueDate.Visible = true;
                NewDueDate1.Visible = true;
                IsShowUploadFile.Visible = true;
            }
            else
            {

            }
        }
        catch (Exception)
        {

        }
    }

    private void BindMettingType(DataTable dataTable)
    {
        try
        {
            ddl_Meeting_Type.DataSource = null;
            ddl_Meeting_Type.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                ddl_Meeting_Type.DataSource = dataTable;
                ddl_Meeting_Type.DataTextField = "MeetingType";
                ddl_Meeting_Type.DataValueField = "Id";
                ddl_Meeting_Type.DataBind();
                ddl_Meeting_Type.Items.Insert(0, new ListItem("Select Meeting Type", "0"));
            }


        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }


    private void BindMettingRefId(DataTable dataTable)
    {
        try
        {

        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }
    private void BindAttendee(DataTable dataTable)
    {
        try
        {
            //ddl_Attendees.DataSource = null;
            //ddl_Attendees.DataBind();
            //if (dataTable.Rows.Count > 0)
            //{
            //    ddl_Attendees.DataSource = dataTable;
            //    ddl_Attendees.DataTextField = "Emp_Name";
            //    ddl_Attendees.DataValueField = "Emp_Code";
            //    ddl_Attendees.DataBind();
            //    ddl_Attendees.Items.Insert(0, new ListItem("Select Attendee", "0"));
            //}
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }

    private void BindTaskList()
    {

        var TaskRefId = Convert.ToString(hdnTaskRefID.Value);
        var TaskId = Convert.ToString(hdnTaskID.Value);

        var empCode = Convert.ToString(Session["Empcode"]);
        var GetSubTask = spm.getTaskExecuterDDL(empCode, "GetTaskList", TaskId, TaskRefId, "", "", "", 0, "", "", "", "", "", "", "", "", "", Convert.ToBoolean(0), 0, Convert.ToBoolean(0), 0, Convert.ToBoolean(0), Convert.ToBoolean(0));
        var getSubTaskList = GetSubTask.Tables[0];

        try
        {
            dv_TaskList.DataSource = null;
            dv_TaskList.DataBind();
            if (getSubTaskList.Rows.Count > 0)
            {
                dv_TaskList.DataSource = getSubTaskList;
                dv_TaskList.DataBind();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {

            lblmessage.Text = ex.Message.ToString();
        }
    }

    private void BindSubTaskList()
    {

        var TaskId = Convert.ToString(hdnTaskID.Value);
        var empCode = Convert.ToString(Session["Empcode"]);
        var GetSubTask = spm.getTaskExecuterDDL(empCode, "GetSubTaskList", TaskId, "", "", "", "", 0, "", "", "", "", "", "", "", "", "", Convert.ToBoolean(0), 0, Convert.ToBoolean(0), 0, Convert.ToBoolean(0), Convert.ToBoolean(0));
        var getSubTaskList = GetSubTask.Tables[0];
        try
        {
            gv_SubTaskList.DataSource = null;
            gv_SubTaskList.DataBind();
            if (getSubTaskList.Rows.Count > 0)
            {
                AssignSubTask.Visible = true;
                gv_SubTaskList.DataSource = getSubTaskList;
                gv_SubTaskList.DataBind();

                var getSubTaskStatus = GetSubTask.Tables[1];
                if (getSubTaskStatus.Rows.Count > 0)
                {
                    var getMessage = getSubTaskStatus.Rows[0]["Message"].ToString();
                    if (getMessage == "No Exists")
                    {
                        ddl_Action_Type.Attributes.Add("disabled", "disabled");
                    }
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }



    private void BindSubTaskHistoryList()
    {
        dv_SubTaskHistoryList.Visible = true;

        var TaskId = Convert.ToString(hdnTaskID.Value);
        var empCode = Convert.ToString(Session["Empcode"]);
        var GetSubTaskHistory = spm.getTaskExecuterDDL(empCode, "GetSubTaskHistoryList", TaskId, "", "", "", "", 0, "", "", "", "", "", "", "", "", "", Convert.ToBoolean(0), 0, Convert.ToBoolean(0), 0, Convert.ToBoolean(0), Convert.ToBoolean(0));
        var getSubTaskHistory = GetSubTaskHistory.Tables[0];


        try
        {
            dv_SubTaskHistoryList.DataSource = null;
            dv_SubTaskHistoryList.DataBind();
            if (getSubTaskHistory.Rows.Count > 0)
            {
                dv_SubTaskHistoryList.DataSource = getSubTaskHistory;
                dv_SubTaskHistoryList.DataBind();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }

    private void BindUploadedFile()
    {
        try
        {
            var TaskId = Convert.ToString(hdnTaskID.Value);
            var empCode = Convert.ToString(Session["Empcode"]);
            var GetDocument = spm.getTaskExecuterDDL(empCode, "GetFileDetailsById", TaskId, "", "", "", "", 0, "", "", "", "", "", "", "", "", "", Convert.ToBoolean(0), 0, Convert.ToBoolean(0), 0, Convert.ToBoolean(0), Convert.ToBoolean(0));
            var getDocumentTable = GetDocument.Tables[0];

            gv_Documents.DataSource = null;
            gv_Documents.DataBind();
            // var getDocumentTable = getdsTables.Tables[2];
            if (getDocumentTable.Rows.Count > 0)
            {
                UploadedFile.Visible = true;
                gv_Documents.DataSource = getDocumentTable;
                gv_Documents.DataBind();

            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }

    }

    //private void BindUploadedFileExecute()
    //{
    //    try
    //    {

    //        SqlParameter[] spars = new SqlParameter[3];
    //        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
    //        spars[0].Value = "GetFileDetailsExecuteById";
    //        spars[1] = new SqlParameter("@TaskId", SqlDbType.VarChar);
    //        spars[1].Value = Convert.ToString(hdnTaskID.Value);
    //        spars[2] = new SqlParameter("@TaskRefId", SqlDbType.VarChar);
    //        spars[2].Value = Convert.ToString(hdnTaskRefID.Value);


    //        DataSet DS = spm.getDatasetList(spars, "SP_TASK_M_EXECUTER");
    //        if (DS.Tables[0].Rows.Count > 0)
    //        {
    //           // GV_UploadExecute.Visible = false;
    //            Span1.Visible = false;
    //            GV_UploadExecute.DataSource = DS.Tables[0];
    //            GV_UploadExecute.DataBind();
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblmessage.Text = ex.Message.ToString();
    //    }

    //}


    private void BindGridAttendee(DataTable dataTable)
    {
        try
        {
            dg_ATT_Details.DataSource = null;
            dg_ATT_Details.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                dg_ATT_Details.DataSource = dataTable;
                dg_ATT_Details.DataBind();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }
    private void DeleteTempAttendee()
    {
        try
        {
            var getEmpCode = Convert.ToString(Session["Empcode"]);
            spm.InsertUpdateTempAttendee(0, 0, "DELETETempAttendee", getEmpCode, getEmpCode, false);
        }
        catch (Exception ex)
        {

        }
    }
    private void ClearAttendee()
    {
        //chk_Ref_Select.Checked = false;
        //btn_ATT_Save.Visible = true;
        //btn_ATT_Update.Visible = false;


    }
    private void ClearTask()
    {
        try
        {

            chk_for_Info.Checked = false;
            txt_TaskDescripation.Text = "";
            txt_TaskRemark.Text = "";
            txt_Due_Date.Text = "";
            txt_Reminder_Day.Text = "";
            txt_Reminder_Repe_Day.Text = "";
            chk_Reminder_Due_Date.Checked = false;
            chk_Escalation_Due_Date.Checked = false;
            chk_Escalation_Repeate.Checked = false;
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void BindGridTask(DataTable dataTable)
    {
        try
        {
            //dv_TaskList.DataSource = null;
            //dv_TaskList.DataBind();
            //if (dataTable.Rows.Count > 0)
            //{
            //    dv_TaskList.DataSource = dataTable;
            //    dv_TaskList.DataBind();
            //}
            //else
            //{

            //}
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }
    private void BindSupervisor(DataTable dataTable)
    {
        try
        {
            //ddl_Supervisor.DataSource = null;
            //ddl_Supervisor.DataBind();
            //if (dataTable.Rows.Count > 0)
            //{
            //    ddl_Supervisor.DataSource = dataTable;
            //    ddl_Supervisor.DataTextField = "Emp_Name";
            //    ddl_Supervisor.DataValueField = "Emp_Code";
            //    ddl_Supervisor.DataBind();
            //    ddl_Supervisor.Items.Insert(0, new ListItem("Select Supervisor", "0"));
            //}
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }
    private void BindExecutor(DataTable dataTable)
    {
        try
        {
            ddl_Executer.DataSource = null;
            ddl_Executer.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                ddl_Executer.DataSource = dataTable;
                ddl_Executer.DataTextField = "Emp_Name";
                ddl_Executer.DataValueField = "Emp_Code";
                ddl_Executer.DataBind();
                ddl_Executer.Items.Insert(0, new ListItem("Select Executor", "0"));
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }

    //Add New refarance

    private void BindActionTask(string qtype)
    {

        try
        {

            DataSet GetAction = spm.getTaskExecuterDDL("", qtype, "", "", "", "", "", 0, "", "", "", "",
                "", "", "", "", "", Convert.ToBoolean(0), 0, Convert.ToBoolean(0), 0, Convert.ToBoolean(0), Convert.ToBoolean(0));
            DataTable getActionList = GetAction.Tables[0];
            //  txt_Attendees.Text = Convert.ToString(getTaskAttendeesList.Rows[0]["Organizer"].ToString());

            ddl_Action_Type.DataSource = null;
            ddl_Action_Type.DataBind();

            if (getActionList.Rows.Count > 0)
            {
                ddl_Action_Type.DataSource = getActionList;

                for (var i = 0; i < getActionList.Rows.Count; i++)
                {

                    ddl_Action_Type.DataTextField = "StatusName";
                    ddl_Action_Type.DataValueField = "Id";
                }

                ddl_Action_Type.DataBind();
                ddl_Action_Type.Items.Insert(0, "Select Action");
                var getval = Convert.ToString(hdnTaskStatus_Id.Value);
                foreach (ListItem itm in ddl_Action_Type.Items)
                {
                    if (getval == "8" || getval == "5")
                    {
                        ddl_Action_Type.Attributes.Add("disabled", "disabled");
                        lnk_Task_Create.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }

    protected void lnk_TaskList_Click(object sender, EventArgs e)
    {
        try
        {
            IsShowTaskHistory.Visible = true;
            IsShowTaskHistory1.Visible = true;
            IsShowTaskHistory2.Visible = true;
            IsShowTaskHistory3.Visible = true;
            IsShowTaskHistory4.Visible = true;
            IsShowTaskHistory5.Visible = true;
            IsShowTaskHistory6.Visible = true;
            IsShowTaskHistory7.Visible = true;
            IsShowTaskHistory8.Visible = true;
            LinkButton btn = (LinkButton)sender;

            string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
            string Task_ID = commandArgs[0];


            DataSet GetTaskListById = spm.getTaskExecuterDDL("", "GetTaskListById", Task_ID, "", "", "", "", 0, "", "", "", "",
                "", "", "", "", "", Convert.ToBoolean(0), 0, Convert.ToBoolean(0), 0, Convert.ToBoolean(0), Convert.ToBoolean(0));
            DataTable getTaskHistoryById = GetTaskListById.Tables[0];

            txt_ActionDate_Hdtls.Text = Convert.ToString(getTaskHistoryById.Rows[0]["Action_Date"].ToString());
            txt_DueDate_Hdtls.Text = Convert.ToString(getTaskHistoryById.Rows[0]["Due_Date"].ToString());
            txt_Action_Hdtls.Text = Convert.ToString(getTaskHistoryById.Rows[0]["StatusName"].ToString());
            txt_Supervisor_Hdtls.Text = Convert.ToString(getTaskHistoryById.Rows[0]["Supervisor"].ToString());

            txt_ActionBy_Hdtls.Text = Convert.ToString(getTaskHistoryById.Rows[0]["ActionBy"].ToString());
            txt_Executer_Hdtls.Text = Convert.ToString(getTaskHistoryById.Rows[0]["Executer"].ToString());
            txt_Remarks_Hdtls.Text = Convert.ToString(getTaskHistoryById.Rows[0]["Task_Remark"].ToString());
        }
        catch (Exception ex)
        {

        }
    }

    protected void TaskExecuter_Edit_Click(object sender, EventArgs e)
    {
        try
        {
            //gv_SubTaskList.Visible = true;
            dv_SubTaskHistoryList.Visible = true;
            SubTaskHistory.Visible = true;
            // var TaskId = Convert.ToString(hdnTaskID.Value);
            LinkButton btn = (LinkButton)sender;

            string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
            string TaskExecuter_EditVal = commandArgs[0];
            string Task_Ref_Id = commandArgs[1];
            var empCode = Convert.ToString(Session["Empcode"]);
            var GetSubTaskHistory = spm.getTaskExecuterDDL(empCode, "GetSubTaskHistoryList", TaskExecuter_EditVal, "", "", "", "", 0, "", "", "", "", "", "", "", "", "", Convert.ToBoolean(0), 0, Convert.ToBoolean(0), 0, Convert.ToBoolean(0), Convert.ToBoolean(0));
            var getSubTaskHistory = GetSubTaskHistory.Tables[0];
            try
            {
                dv_SubTaskHistoryList.DataSource = null;
                dv_SubTaskHistoryList.DataBind();
                if (getSubTaskHistory.Rows.Count > 0)
                {
                    dv_SubTaskHistoryList.DataSource = getSubTaskHistory;
                    dv_SubTaskHistoryList.DataBind();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message.ToString();
            }
        }
        catch (Exception ex)
        {

        }

    }
    protected void txt_R_Due_Date_TextChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        try
        {
            var getOldDueDate = "";
            var getOldDue_Date = Convert.ToString(txt_Due_Date.Text).ToString();
            var splitOldDueDate = getOldDue_Date.Replace('-', '/');
            var getDate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tempF = DateTime.ParseExact(txt_R_Due_Date.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tempT = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tempOldDate = DateTime.ParseExact(splitOldDueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (tempF < tempT)
            {
                Label1.Text = "Due date cannot be less than today date";
                txt_R_Due_Date.Text = "";
                return;
            }
            else if (tempOldDate <= tempF)
            {
                Label1.Text = "Due date cannot be greater than old due date " + splitOldDueDate;
                txt_R_Due_Date.Text = "";
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void txt_Metting_Dis_TextChanged(object sender, EventArgs e)
    {
        //Change Date
    }
    protected void txt_R_NewDue_DateTextChanged(object sender, EventArgs e)
    {
        Label1.Text = "";
        try
        {
            var getOldDueDate = "";
            var getOldDue_Date = Convert.ToString(txt_Due_Date.Text).ToString();
            var splitOldDueDate = getOldDue_Date.Replace('-', '/');
            var getDate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tempF = DateTime.ParseExact(txt_R_NewDue_Date.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tempT = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tempOldDate = DateTime.ParseExact(splitOldDueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (tempF < tempT)
            {
                Label1.Text = "Due date cannot be less than today date";
                txt_R_NewDue_Date.Text = "";
                return;
            }
            //else if (tempOldDate <= tempF)
            //{
            //    Label1.Text = "Due date cannot be greater than old due date " + splitOldDueDate;
            //    txt_R_NewDue_Date.Text = "";
            //    return;
            //}
        }
        catch (Exception ex)
        {

        }
    }



    protected void ddl_Attendees_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //Click on Add Attedee  
    protected void btn_ATT_Save_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            Label1.Text = "";
            var empCode = Convert.ToString(Session["Empcode"]);
            var getVal = Convert.ToInt32(hdnTaskRefID.Value);
            //if (Convert.ToString(ddl_Attendees.SelectedValue).Trim() == "0")
            //{
            //    Label1.Text = "Please select relation";
            //    return;
            //}
            var getAttendee = "";// Convert.ToString(ddl_Attendees.SelectedValue).Trim();
            var getOrganizer = false;// chk_Ref_Select.Checked;
            var qtype = "";
            if (getVal == 0)
            {
                qtype = "AddTempAttendee";
            }
            else
            {
                qtype = "AddAttendee";
            }
            var getStatus = spm.InsertUpdateTempAttendee(0, getVal, qtype, getAttendee, empCode, getOrganizer);
            if (getStatus == false)
            {
                Label1.Text = "Something went wrong";
                return;
            }
            else
            {
                if (getVal == 0)
                {
                    var getAttendeeList = spm.getTempAttendee("GetTempAttendee", empCode, 0);
                    BindGridAttendee(getAttendeeList);
                }
                else
                {
                    var getAttendeeList = spm.getTempAttendee("GetListAttendee", empCode, getVal);
                    BindGridAttendee(getAttendeeList);
                }
            }
            ClearAttendee();
        }
        catch (Exception ex)
        {

        }
    }
    //Click Attendee Update
    protected void btn_ATT_Update_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            Label1.Text = "";
            var empCode = Convert.ToString(Session["Empcode"]);
            var getVal = Convert.ToInt32(hdnTaskRefID.Value);
            var getId = Convert.ToInt32(hdnAttendeeID.Value);
            //if (Convert.ToString(ddl_Attendees.SelectedValue).Trim() == "0")
            //{
            //    Label1.Text = "Please select relation";
            //    return;
            //}
            var getAttendee = "";// Convert.ToString(ddl_Attendees.SelectedValue).Trim();
            var getOrganizer = false;// chk_Ref_Select.Checked;
            var qtype = "";
            if (getVal == 0)
            {
                qtype = "AddTempAttendee";
            }
            else
            {
                qtype = "AddAttendee";
            }
            var getStatus = spm.InsertUpdateTempAttendee(0, getVal, qtype, getAttendee, empCode, getOrganizer);
            if (getStatus == false)
            {
                Label1.Text = "Something went wrong";
                return;
            }
            else
            {
                if (getVal == 0)
                {
                    var getAttendeeList = spm.getTempAttendee("GetTempAttendee", empCode, 0);
                    BindAttendee(getAttendeeList);
                }
                else
                {
                    var getAttendeeList = spm.getTempAttendee("GetListAttendee", empCode, getVal);
                    BindAttendee(getAttendeeList);
                }
            }
            ClearAttendee();
        }
        catch (Exception ex)
        {

        }
    }
    //Click On cancel Attendee 
    protected void btn_ATT_Cancel_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {

        }
    }
    //Click on Edit attendee grid
    protected void lnk_Atend_Edit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblmessage.Text = "";

            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(dg_ATT_Details.DataKeys[row.RowIndex].Values[0]).Trim();

            if (fId != "0")
            {
                var getds = new DataTable();
                var getVal = Convert.ToInt32(hdnTaskRefID.Value);


                if (getVal == 0)
                {
                    getds = spm.getTempAttendee("GetTempAttendeeById", "", Convert.ToDouble(fId));
                }
                else
                {
                    getds = spm.getTempAttendee("GetAttendeeById", "", Convert.ToDouble(fId));
                }
                hdnAttendeeID.Value = fId;
                if (getds != null)
                {
                    if (getds.Rows.Count > 0)
                    {

                        if (getds.Rows.Count > 0)
                        {
                            //btn_ATT_Save.Visible = false;

                            var getEmpId = getds.Rows[0]["Emp_Code"].ToString();
                            //ddl_Attendees.Items.FindByValue(getEmpId).Selected = true;
                            //chk_Ref_Select.Checked=Convert.ToBoolean(getds.Rows[0]["IsOrganizer"].ToString());
                            //btn_ATT_Update.Visible = true;

                        }
                        else
                        {

                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
            return;
        }
    }
    //Click on Delete Attendee Grid
    protected void lnk_Atend_Delete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(dg_ATT_Details.DataKeys[row.RowIndex].Values[0]).Trim();
            if (fId != null)
            {
                var empCode = Convert.ToString(Session["Empcode"]);
                var id = int.Parse(fId);
                var result = false;
                var getVal = Convert.ToInt32(hdnTaskRefID.Value);
                if (getVal == 0)
                {
                    result = spm.DeleteAttendeeDetails(Convert.ToDouble(fId), "DeleteTempAttendeeById");
                }
                else
                {
                    result = spm.DeleteAttendeeDetails(Convert.ToDouble(fId), "DeleteAttendeeById");
                }
                if (result == true)
                {

                }
                else
                {
                    Label1.Text = "Something went wrong , Please try again.";
                    return;
                }

                ClearAttendee();
                if (getVal == 0)
                {
                    var getAttendeeList = spm.getTempAttendee("GetTempAttendee", empCode, 0);
                    BindGridAttendee(getAttendeeList);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void txt_Due_Date_TextChanged(object sender, EventArgs e)
    {

    }

    protected void lnk_Task_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            var EmpCode = Convert.ToString(Session["Empcode"]);
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            string ActionType = ddl_Action_Type.SelectedValue;

            if (HDMeeting_Type_Id.Value == "8")
            {
                HttpFileCollection fileCollection1 = Request.Files;
                for (int i = 0; i < fileCollection1.Count; i++)
                {
                    string strfileName = "";
                    HttpPostedFile uploadfileName = fileCollection1[i];
                    if (uploadfileName.ContentLength == 0)
                    {
                        Label3.Text = "Please select Upload File";
                        return;
                    }
                }
            }

            if (ActionType == "5")
            {
                //Due Date Change Request

                if (Convert.ToString(txt_TaskRemark.Text).Trim() == "")
                {
                    Label1.Text = "Enter task descripation.";
                    return;
                }

                if (Convert.ToString(txt_ActionRemarks.Text).Trim() == "")
                {
                    Label1.Text = "Enter Action Remarks.";
                    return;
                }
                if (Convert.ToString(txt_R_NewDue_Date.Text).Trim() == "")
                {
                    Label1.Text = "Enter New Due Date.";
                    return;
                }
                var filename = "";
                var TaskRefId = hdnTaskRefID.Value;
                var TaskId = hdnTaskID.Value;

                var getOldDueDate = "";
                var getOldDue_Date = Convert.ToString(txt_Due_Date.Text).ToString();
                var splitOldDueDate = getOldDue_Date.Split('-');
                getOldDueDate = splitOldDueDate[2] + "-" + splitOldDueDate[1] + "-" + splitOldDueDate[0];

                var getNewDueDate = "";
                var getNewDue_Date = Convert.ToString(txt_R_NewDue_Date.Text).ToString();
                var splitNewDueDate = getNewDue_Date.Split('/');
                getNewDueDate = splitNewDueDate[2] + "-" + splitNewDueDate[1] + "-" + splitNewDueDate[0];

                var Created_By = Convert.ToString(Session["Empcode"]);
                int ActionStatus = Convert.ToInt32(ddl_Action_Type.SelectedValue);
                var TaskRemarks = txt_ActionRemarks.Text;

                var getDueDate = "";
                var getDue_Date = Convert.ToString(txt_Due_Date.Text).ToString();
                var splitDueDate = getDue_Date.Split('-');
                getDueDate = splitDueDate[2] + "-" + splitDueDate[1] + "-" + splitDueDate[0];

                var Executer = hdnTask_Executer.Value;
                var Supervisor = hdnTask_Supervisor.Value;

                DateTime OldDueDate = Convert.ToDateTime(getOldDueDate);
                DateTime NewDueDate = Convert.ToDateTime(getNewDueDate);

                //if (OldDueDate <= NewDueDate)
                //{
                //    Label1.Text = "Due date cannot be greater than main due date.";
                //    return;
                //}
                var qtype = "";
                if (ddl_Meeting_Type.SelectedItem.Value == "MyTasks" && Executer == Supervisor)
                {
                    qtype = "InsertChangeDueDateRequestSelf";
                }
                else
                {
                    qtype = "InsertChangeDueDateRequest";
                }

                Int32 t_cnt = 1;
                filename = "";
                HttpFileCollection fileCollection = Request.Files;
                for (int i = 0; i < fileCollection.Count; i++)
                {
                    string strfileName = "";
                    HttpPostedFile uploadfileName = fileCollection[i];
                    DateTime loadedDate = DateTime.Now;
                    var strfromDate = loadedDate.ToString("ddMMyyyyHHmmss");

                    if (uploadfileName.ContentLength > 0)
                    {
                        string InputFile = System.IO.Path.GetExtension(uploadfileName.FileName);
                        strfileName = "TASK_" + Executer + "_" + t_cnt + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
                        filename = strfileName;
                        uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["TaskMonitoringPath"]).Trim()), strfileName));

                        //spm.InsertUploadMultipleFile(EmpCode, "InsertMultipleFileRequest", hdnTaskID.Value, hdnTaskRefID.Value, 
                        //    Created_By, strfileName);
                        SqlParameter[] spars = new SqlParameter[6];
                        spars[0] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                        spars[0].Value = Convert.ToString(EmpCode);
                        spars[1] = new SqlParameter("@qtype", SqlDbType.VarChar);
                        spars[1].Value = "InsertMultipleFileRequest";
                        spars[2] = new SqlParameter("@TaskId", SqlDbType.VarChar);
                        spars[2].Value = Convert.ToString(hdnTaskID.Value);
                        spars[3] = new SqlParameter("@TaskRefId", SqlDbType.VarChar);
                        spars[3].Value = Convert.ToString(hdnTaskRefID.Value);
                        spars[4] = new SqlParameter("@Created_By", SqlDbType.VarChar);
                        spars[4].Value = Convert.ToString(Created_By);
                        spars[5] = new SqlParameter("@FilePath", SqlDbType.VarChar);
                        spars[5].Value = Convert.ToString(strfileName);
                        DataSet DS = spm.getDatasetList(spars, "SP_TASK_M_EXECUTER");
                        t_cnt = t_cnt + 1;
                    }
                }
                spm.InsertChangeActionRequest(EmpCode, qtype, TaskId, TaskRefId, getOldDueDate, getNewDueDate, Created_By, ActionStatus, TaskRemarks, getDueDate, Executer, Supervisor,
                "", "", "", "", "", Convert.ToBoolean(0), 0, Convert.ToBoolean(0), 0, Convert.ToBoolean(0), Convert.ToBoolean(0), Convert.ToString(EmpCode), "");

                var link = "http://localhost/hrms/login.aspx?ReturnUrl=procs/Task_DueDateChange_Inbox.aspx";
                var getExecuter = Convert.ToString(Executer);
                // var TaskRefId = Convert.ToString(hdnTaskRefID.Value);
                if (ddl_Meeting_Type.SelectedItem.Value == "MyTasks" && Executer == Supervisor)
                {

                }
                else
                {
                    var getTaskEmailDetails = spm.getTaskMonitoringReport("getTaskDetailsDueDate", getExecuter, Convert.ToDouble(TaskRefId), Convert.ToDouble(TaskId));
                    if (getTaskEmailDetails != null)
                    {
                        if (getTaskEmailDetails.Tables.Count > 0)
                        {
                            ///procs/Task_D_DateChange.aspx?id=54
                            link = "http://localhost/hrms/login.aspx?ReturnUrl=procs/Task_D_DateChange.aspx?id=" + TaskId + "";

                            var getTaskDetails1 = getTaskEmailDetails.Tables[0];
                            var getTaskRefDetails = getTaskEmailDetails.Tables[1];
                            var getAllTaskIntimation = getTaskEmailDetails.Tables[2];
                            var getAllTaskSupervisor = getTaskEmailDetails.Tables[3];
                            var getMainTaskSupervisor = getTaskEmailDetails.Tables[4];
                            var appendIntimation = "";

                            var Task_Reference_ID = Convert.ToString(getTaskRefDetails.Rows[0]["Task_Reference_ID"]);
                            var Meeting_Discussion_Title = Convert.ToString(getTaskRefDetails.Rows[0]["Meeting_Discussion_Title"]);
                            var Meeting_Discussion_Date = Convert.ToString(getTaskRefDetails.Rows[0]["Meeting_Discussion_Date"]);
                            var MeetingType = Convert.ToString(getTaskRefDetails.Rows[0]["MeetingType"]);
                            //var Meetingremarks = Convert.ToString(getTaskRefDetails.Rows[0]["Remark"]);
                            if (getTaskDetails1.Rows.Count > 0)
                            {
                                var ExecuterEmail = Convert.ToString(getTaskDetails1.Rows[0]["ExecuterEmail"]);
                                var ExecuterName = Convert.ToString(getTaskDetails1.Rows[0]["Executer"]);
                                var SupervisorEmail = Convert.ToString(getTaskDetails1.Rows[0]["SupervisorEmail"]);
                                // var ParentTaskSupervisor = Convert.ToString(getTaskDetails1.Rows[0]["ParentTaskSupervisor"]);//AS CC

                                var AllTaskSupervisor = Convert.ToString(getAllTaskSupervisor.Rows[0]["CCEmailAddressSupervisor"]);//AS CC
                                var OnlymainTaskSupervisorEmail = Convert.ToString(getMainTaskSupervisor.Rows[0]["MainSupervisorEmail"]);//AS To

                                appendIntimation = Convert.ToString(getAllTaskIntimation.Rows[0]["CCEmailAddress"]);

                                if (appendIntimation != "")
                                {
                                    if (AllTaskSupervisor == "")
                                    {
                                        AllTaskSupervisor = appendIntimation;
                                    }
                                    else
                                    {
                                        AllTaskSupervisor = AllTaskSupervisor + ";" + appendIntimation;
                                    }
                                }

                                spm.Task_Monitoring_Due_Date_Change(0, "", getTaskDetails1, OnlymainTaskSupervisorEmail, Meeting_Discussion_Title, Meeting_Discussion_Date, MeetingType, Task_Reference_ID, link, AllTaskSupervisor, ExecuterName, TaskRemarks);
                            }
                        }
                    }
                }
                Response.Redirect("~/procs/InboxExecuter.aspx");
                // Response.Redirect("~/procs/TaskMonitoring.aspx");

            }
            else if (ActionType == "8")
            {
                // Close Task Request
                var Created_By = Convert.ToString(Session["Empcode"]);
                int ActionStatus = Convert.ToInt32(ddl_Action_Type.SelectedValue);
                var Executer = Convert.ToString(Session["Executer_Id"]);
                var Supervisor = Convert.ToString(Session["Supervisor_Id"]);
                var ActionRemarks = Convert.ToString(txt_ActionRemarks.Text);
                var TaskId = Convert.ToString(hdnTaskID.Value);

                var GetTaskCloseStatus = spm.getTaskExecuterDDL(Created_By, "CheckISCloseTask", TaskId, "", "", "", "", 0, "", "", "", "", "", "", "", "", "", Convert.ToBoolean(0), 0, Convert.ToBoolean(0), 0, Convert.ToBoolean(0), Convert.ToBoolean(0));
                if (GetTaskCloseStatus != null)
                {
                    if (GetTaskCloseStatus.Tables.Count > 0)
                    {
                        var getTable = GetTaskCloseStatus.Tables[0];
                        if (getTable.Rows.Count > 0)
                        {
                            var getMessage = Convert.ToString(getTable.Rows[0]["MESSAGE"]);
                            if (getMessage == "Exists")
                            {
                                Label1.Text = "You can't be close first main task.";
                                return;
                            }
                        }
                    }
                }
                var TaskSubId = Convert.ToString(hdnTaskID.Value);
                var getOldDueDate = "";
                var getOldDue_Date = Convert.ToString(txt_Due_Date.Text).ToString();
                var splitOldDueDate = getOldDue_Date.Split('-');
                getOldDueDate = splitOldDueDate[2] + "-" + splitOldDueDate[1] + "-" + splitOldDueDate[0];


                string Task_Id = Convert.ToString(hdnTaskID.Value);
                var qtype = "";
                string ProjectSchedulefilename = "";

                if (ddl_Meeting_Type.SelectedItem.Value == "MyTasks" && Executer == Supervisor)
                {
                    qtype = "UpdateTaskStatusSelf";
                }
                else
                {
                    if (HDMeeting_Type_Id.Value == "8")
                    {
                        qtype = "UpdateTaskStatusprojectSchedule";
                    }
                    else
                    {
                        qtype = "UpdateTaskStatus";
                    }
                }

                Int32 t_cnt = 1;
                string filename = "";
                HttpFileCollection fileCollection = Request.Files;
                for (int i = 0; i < fileCollection.Count; i++)
                {
                    string strfileName = "";
                    HttpPostedFile uploadfileName = fileCollection[i];
                    DateTime loadedDate = DateTime.Now;
                    var strfromDate = loadedDate.ToString("ddMMyyyyHHmmss");

                    if (uploadfileName.ContentLength > 0)
                    {
                        string InputFile = System.IO.Path.GetExtension(uploadfileName.FileName);

                        if (HDMeeting_Type_Id.Value == "8")
                        {
                            strfileName = "TASK_" + Executer + "_" + t_cnt + "_" + strfromDate + Path.GetExtension(UploadFileProject.FileName);
                            filename = strfileName;
                            UploadFileProject.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["TaskMonitoringPath"]).Trim()), strfileName));
                            ProjectSchedulefilename = strfileName;
                        }
                        else
                        {
                            strfileName = "TASK_" + Executer + "_" + t_cnt + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
                            filename = strfileName;
                            uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["TaskMonitoringPath"]).Trim()), strfileName));
                            ProjectSchedulefilename = strfileName;
                        }



                        SqlParameter[] spars = new SqlParameter[6];
                        spars[0] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                        spars[0].Value = Convert.ToString(EmpCode);
                        spars[1] = new SqlParameter("@qtype", SqlDbType.VarChar);
                        spars[1].Value = "InsertMultipleFileRequest";
                        spars[2] = new SqlParameter("@TaskId", SqlDbType.VarChar);
                        spars[2].Value = Convert.ToString(TaskSubId);
                        spars[3] = new SqlParameter("@TaskRefId", SqlDbType.VarChar);
                        spars[3].Value = Convert.ToString(hdnTaskRefID.Value);
                        spars[4] = new SqlParameter("@Created_By", SqlDbType.VarChar);
                        spars[4].Value = Convert.ToString(Created_By);
                        spars[5] = new SqlParameter("@FilePath", SqlDbType.VarChar);
                        spars[5].Value = Convert.ToString(strfileName);
                        DataSet DS = spm.getDatasetList(spars, "SP_TASK_M_EXECUTER");


                        t_cnt = t_cnt + 1;
                    }
                }
                spm.InsertChangeActionRequest(EmpCode, qtype, TaskSubId, TaskId, getOldDueDate, "", Created_By, ActionStatus, ActionRemarks, "", Executer, Supervisor,
                   "", "", "", "", "", Convert.ToBoolean(0), 0, Convert.ToBoolean(0), 0, Convert.ToBoolean(0), Convert.ToBoolean(0), Convert.ToString(EmpCode), "");
                var link = "http://localhost/hrms/login.aspx?ReturnUrl=procs/Task_Closure_Inbox.aspx";
                var getExecuter = Convert.ToString(Executer);
                var TaskRefId = Convert.ToString(hdnTaskRefID.Value);
                if (ddl_Meeting_Type.SelectedItem.Value == "MyTasks" && Executer == Supervisor)
                {

                }
                else
                {
                    var getTaskEmailDetails = spm.getTaskMonitoringReport("getTaskDetailsByExecuterClose", getExecuter, Convert.ToDouble(TaskRefId), Convert.ToDouble(Task_Id));
                    if (getTaskEmailDetails != null)
                    {
                        if (getTaskEmailDetails.Tables.Count > 0)
                        {
                            var getTaskDetails1 = getTaskEmailDetails.Tables[0];
                            var getTaskRefDetails = getTaskEmailDetails.Tables[1];
                            var getAllTaskIntimation = getTaskEmailDetails.Tables[2];
                            var getAllTaskattendees = getTaskEmailDetails.Tables[3];
                            var appendIntimation = "";
                            var Task_Reference_ID = Convert.ToString(getTaskRefDetails.Rows[0]["Task_Reference_ID"]);
                            var Meeting_Discussion_Title = Convert.ToString(getTaskRefDetails.Rows[0]["Meeting_Discussion_Title"]);
                            var Meeting_Discussion_Date = Convert.ToString(getTaskRefDetails.Rows[0]["Meeting_Discussion_Date"]);
                            var MeetingType = Convert.ToString(getTaskRefDetails.Rows[0]["MeetingType"]);
                             
                            if (getTaskDetails1.Rows.Count > 0)
                            {
                                link = "http://localhost/hrms/login.aspx?ReturnUrl=procs/Task_Closure.aspx?id=" + Task_Id + "";
                                var ExecuterEmail = Convert.ToString(getTaskDetails1.Rows[0]["ExecuterEmail"]);
                                var ExecuterName = Convert.ToString(getTaskDetails1.Rows[0]["Executer"]);
                                var SupervisorEmail = Convert.ToString(getTaskDetails1.Rows[0]["SupervisorEmail"]);
                                appendIntimation = Convert.ToString(getAllTaskIntimation.Rows[0]["CCEmailAddress"]);

                                if (HDMeeting_Type_Id.Value == "8")
                                {
                                    string strFilepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["TaskMonitoringPath"]).Trim()) + ProjectSchedulefilename;

                                    appendIntimation = Convert.ToString(getAllTaskattendees.Rows[0]["CCEmailAddress"]);
                                    if (getAllTaskIntimation.Rows.Count > 0)
                                    {
                                        appendIntimation = appendIntimation + ";" + Convert.ToString(getAllTaskIntimation.Rows[0]["CCEmailAddress"]);
                                    }
                                    spm.Task_Monitoring_Close_Request_ProjectSchedule(getTaskDetails1, SupervisorEmail, Meeting_Discussion_Title, Meeting_Discussion_Date, MeetingType, Task_Reference_ID, "", appendIntimation, ExecuterName, strFilepath, ActionRemarks);
                                }
                                else
                                {
                                    spm.Task_Monitoring_Close(getTaskDetails1, SupervisorEmail, Meeting_Discussion_Title, Meeting_Discussion_Date, MeetingType, Task_Reference_ID, link, appendIntimation, ExecuterName, ActionRemarks);
                                }
                            }
                        }
                    }
                }

                Response.Redirect("~/procs/InboxExecuter.aspx");
                // Response.Redirect("~/procs/TaskMonitoring.aspx");
            }
            else if (ActionType == "1")
            {
                //Assgined new Task

                if (Convert.ToString(txt_ActionRemarks.Text).Trim() == "")
                {
                    Label1.Text = "Enter Action Remarks.";
                    return;
                }
                if (Convert.ToString(txt_Supervisor.Text).Trim() == "")
                {
                    Label1.Text = "Select Supervisor.";
                    return;
                }
                if (Convert.ToString(ddl_Executer.SelectedValue).Trim() == "0" || Convert.ToString(ddl_Executer.SelectedValue).Trim() == "")
                {
                    Label1.Text = "Select Executer.";
                    return;
                }
                if (Convert.ToString(txt_R_Due_Date.Text).Trim() == "")
                {
                    Label1.Text = "Enter Due Date.";
                    return;
                }
                if (Convert.ToBoolean(Chk_R_Before_Due_Date.Checked) == true)
                {
                    if (Convert.ToString(txt_R_Reminder_Days.Text).Trim() == "")
                    {
                        Label1.Text = "Enter reminder before day.";
                        return;
                    }
                }
                if (Convert.ToBoolean(Chk_R_Escalation_Due_Date.Checked) == true)
                {
                    if (Convert.ToString(txt_R_Escalation_Mail_Freq.Text).Trim() == "")
                    {
                        Label1.Text = "Enter escalation mail frequency.";
                        return;
                    }
                }

                var TaskDescription = Convert.ToString(txt_TaskDescripation.Text);

                var filename = "";
                int ActionStatus = Convert.ToInt32(ddl_Action_Type.SelectedValue);
                var TaskRefId = hdnTaskRefID.Value;

                var TaskId = hdnTaskID.Value;


                var Supervisor = Convert.ToString(Session["SubSupervisor"]);

                var Executer = Convert.ToString(ddl_Executer.SelectedValue);

                var ActionRemarks = Convert.ToString(txt_ActionRemarks.Text);
                var SubDueDate = Convert.ToString(txt_R_Due_Date.Text);


                var getOldDueDate = "";
                var getOldDue_Date = Convert.ToString(txt_Due_Date.Text).ToString();
                var splitOldDueDate = getOldDue_Date.Split('-');
                getOldDueDate = splitOldDueDate[2] + "-" + splitOldDueDate[1] + "-" + splitOldDueDate[0];


                var getSubDueDate = "";
                var getSubDue_Date = Convert.ToString(txt_R_Due_Date.Text).ToString();
                var splitSubDueDate = getSubDue_Date.Split('/');
                getSubDueDate = splitSubDueDate[2] + "-" + splitSubDueDate[1] + "-" + splitSubDueDate[0];

                DateTime OldDueDate = Convert.ToDateTime(getOldDueDate);
                DateTime NewDueDate = Convert.ToDateTime(getSubDueDate);

                if (OldDueDate <= NewDueDate)
                {
                    Label1.Text = "Due date cannot be greater than main due date.";
                    return;
                }


                bool CheckForInfo = Convert.ToBoolean(chk_for_Info.Checked);

                bool EscalationDueDateChk = Convert.ToBoolean(Chk_R_Escalation_Due_Date.Checked);

                bool ReminderBeforeDueDate = Convert.ToBoolean(Chk_R_Before_Due_Date.Checked);
                int EsclationMailFreqDays = Convert.ToInt32(txt_R_Escalation_Mail_Freq.Text);

                int ReminderMailDays = Convert.ToInt32(txt_R_Reminder_Days.Text);

                bool RepeatedEscalation = Convert.ToBoolean(Chk_R_RepeatedEscalation.Checked);



                var TaskRemarks = Convert.ToString(txt_TaskRemark.Text);

                if (uploadfile.HasFile)
                {
                    filename = uploadfile.FileName;
                }
                if (Convert.ToString(filename).Trim() != "")
                {
                    //var getdate = Convert.ToDateTime(Txt_DateCreate.Text);
                    DateTime loadedDate = DateTime.Now;
                    var strfromDate = loadedDate.ToString("ddMMyyyyHHmmss");
                    filename = uploadfile.FileName;
                    var strfileName = "";
                    strfileName = "TASK_" + Executer + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
                    filename = strfileName;
                    uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["TaskMonitoringPath"]).Trim()), strfileName));
                }

                spm.InsertChangeActionRequest(EmpCode, "InsertSubTaskRequest", TaskId, TaskRefId, getOldDueDate, Convert.ToString(""), EmpCode, ActionStatus, ActionRemarks, Convert.ToString(""), Convert.ToString(""), Convert.ToString(""),
                      TaskDescription, TaskRemarks, Executer, Supervisor, getSubDueDate, Convert.ToBoolean(ReminderBeforeDueDate), ReminderMailDays, Convert.ToBoolean(EscalationDueDateChk), EsclationMailFreqDays, Convert.ToBoolean(RepeatedEscalation), Convert.ToBoolean(CheckForInfo),
                      Convert.ToString(EmpCode), filename);
                var link = "http://localhost/hrms/login.aspx?ReturnUrl=procs/InboxExecuter.aspx";
                var getExecuter = Convert.ToString(Executer);
                if (ddl_Meeting_Type.SelectedItem.Value == "MyTasks" && Executer == Supervisor)
                {

                }
                else
                {
                    var getTaskEmailDetails = spm.getTaskMonitoringReport("getTaskDetailsByExecuterReAssgine", getExecuter, Convert.ToDouble(TaskRefId), Convert.ToDouble(TaskId));
                    if (getTaskEmailDetails != null)
                    {
                        if (getTaskEmailDetails.Tables.Count > 0)
                        {
                            var getTaskDetails1 = getTaskEmailDetails.Tables[0];
                            var getTaskRefDetails = getTaskEmailDetails.Tables[1];
                            var getAllTaskIntimation = getTaskEmailDetails.Tables[2];
                            var appendIntimation = "";
                            var Task_Reference_ID = Convert.ToString(getTaskRefDetails.Rows[0]["Task_Reference_ID"]);
                            var Meeting_Discussion_Title = Convert.ToString(getTaskRefDetails.Rows[0]["Meeting_Discussion_Title"]);
                            var Meeting_Discussion_Date = Convert.ToString(getTaskRefDetails.Rows[0]["Meeting_Discussion_Date"]);
                            var MeetingType = Convert.ToString(getTaskRefDetails.Rows[0]["MeetingType"]);
                           // var Meetingremarks = Convert.ToString(getTaskRefDetails.Rows[0]["Remark"]);
                            if (getTaskDetails1.Rows.Count > 0)
                            {
                                var id = Convert.ToString(getTaskDetails1.Rows[0]["ID"]);
                                link = "http://localhost/hrms/login.aspx?ReturnUrl=procs/TaskExecuter_Edit.aspx?Task_Id=" + id + "&TaskRefId=" + TaskRefId + "&flag=NA";
                                var ExecuterEmail = Convert.ToString(getTaskDetails1.Rows[0]["ExecuterEmail"]);
                                var SupervisorEmail = Convert.ToString(getTaskDetails1.Rows[0]["SupervisorEmail"]);
                                SupervisorEmail = "";
                                appendIntimation = Convert.ToString(getAllTaskIntimation.Rows[0]["CCEmailAddress"]);

                                spm.Task_Monitoring_Executer_Assgine(getTaskDetails1, ExecuterEmail, Meeting_Discussion_Title, Meeting_Discussion_Date, MeetingType, Task_Reference_ID, link, appendIntimation, ActionRemarks);
                                //spm.Task_Monitoring_Executer_Assgine(getTaskDetails1, ExecuterEmail, Meeting_Discussion_Title, Meeting_Discussion_Date, MeetingType, Task_Reference_ID, link, SupervisorEmail);
                            }
                        }
                    }
                }
                //Response.Redirect(Request.RawUrl);
                Response.Redirect("~/procs/InboxExecuter.aspx");
                // Response.Redirect("~/procs/TaskMonitoring.aspx");
            }
            else if (ActionType == "Choose Action")
            {
                Label3.Text = "Please select Action Type.";
                return;
            }
        }

        catch (Exception ex)
        {

            Label1.Text = "Something went wrong. please try again";
            return;
        }
    }
    //Click on Edit Task on grid
    protected void lnk_TLI_Edit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //var getSelectedValues = ddl_Task_Executor.SelectedValue.ToString();
            //var getSelectedValues1 = ddl_Task_Supervisor.SelectedValue.ToString();

            //ddl_Task_Executor.Items.FindByValue(getSelectedValues).Selected = false;
            //ddl_Task_Supervisor.Items.FindByValue(getSelectedValues1).Selected = false;
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = "0";//Convert.ToString(dv_TaskList.DataKeys[row.RowIndex].Values[0]).Trim();
            if (fId != "0")
            {
                var getds = spm.getTempAttendee("getTaskDetailsById", "", Convert.ToDouble(fId));
                if (getds.Rows.Count > 0)
                {
                    hdnTaskID.Value = fId;
                    var getSupervisor = Convert.ToString(getds.Rows[0]["Task_Supervisor"]);
                    var getExecutor = Convert.ToString(getds.Rows[0]["Task_Executer"]);
                    //ddl_Task_Executor.Items.FindByValue(getExecutor).Selected = true;
                    //ddl_Task_Supervisor.Items.FindByValue(getSupervisor).Selected = true;
                    chk_for_Info.Checked = Convert.ToBoolean(getds.Rows[0]["For_Information_Only"]);
                    chk_Reminder_Due_Date.Checked = Convert.ToBoolean(getds.Rows[0]["Remainder_Before_Due_Date"]);
                    chk_Escalation_Due_Date.Checked = Convert.ToBoolean(getds.Rows[0]["Escalation_Mail_After_Due_Date"]);
                    chk_Escalation_Repeate.Checked = Convert.ToBoolean(getds.Rows[0]["Repeated_Escalation_Mail"]);

                    if (Convert.ToString(getds.Rows[0]["Reminder_Before_Days"]) != "")
                    {
                        txt_Reminder_Day.Text = Convert.ToString(getds.Rows[0]["Reminder_Before_Days"]);
                    }
                    if (Convert.ToString(getds.Rows[0]["Escalation_Mail_Frequency_Days"]) != "")
                    {
                        txt_Reminder_Repe_Day.Text = Convert.ToString(getds.Rows[0]["Escalation_Mail_Frequency_Days"]);
                    }

                    txt_TaskDescripation.Text = Convert.ToString(getds.Rows[0]["Task_Description"]);
                    txt_TaskRemark.Text = Convert.ToString(getds.Rows[0]["Task_Remarks"]);



                    lnk_Task_Create.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void TaskExecuterSubTask_Edit_Click(object sender, EventArgs e)
    {
        try
        {
            SubTskHistoruDetails.Visible = true;
            LinkButton btn = (LinkButton)sender;
            gv_Document.Visible = true;

            string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
            string Task_ID = commandArgs[0];
            string ActionLogId = commandArgs[1];

            DataSet GetSubActionListById = spm.getTaskExecuterDDL("", "GetSubTaskHistoryListById", Task_ID, ActionLogId, "", "", "", 0, "", "", "", "",
                "", "", "", "", "", Convert.ToBoolean(0), 0, Convert.ToBoolean(0), 0, Convert.ToBoolean(0), Convert.ToBoolean(0));
            DataTable getSubHistoryActionById = GetSubActionListById.Tables[0];

            txt_ActionDate_SubHistory.Text = Convert.ToString(getSubHistoryActionById.Rows[0]["Action_Date"].ToString());
            txt_Status_SubHistory.Text = Convert.ToString(getSubHistoryActionById.Rows[0]["StatusName"].ToString());
            txt_ActionBy_SubHistory.Text = Convert.ToString(getSubHistoryActionById.Rows[0]["ActionBy"].ToString());
            txt_Remarks_SubHistory.Text = Convert.ToString(getSubHistoryActionById.Rows[0]["Task_Remark"].ToString());
            ActionDate.Visible = true;
            Status.Visible = true;
            ActionBy.Visible = true;
            Remarks.Visible = true;
            file.Visible = true;
            try
            {
                var TaskId = Convert.ToString(hdnTaskID.Value);
                var empCode = Convert.ToString(Session["Empcode"]);
                var GetDocument = spm.getTaskExecuterDDL(empCode, "GetFileDetailsById", TaskId, "", "", "", "", 0, "", "", "", "", "", "", "", "", "", Convert.ToBoolean(0), 0, Convert.ToBoolean(0), 0, Convert.ToBoolean(0), Convert.ToBoolean(0));
                var getDocumentTable = GetDocument.Tables[0];

                gv_Document.DataSource = null;
                gv_Document.DataBind();
                // var getDocumentTable = getdsTables.Tables[2];
                if (getDocumentTable.Rows.Count > 0)
                {
                    gv_Document.DataSource = getDocumentTable;
                    gv_Document.DataBind();

                }
            }
            catch (Exception ex)
            {
                lblmessage.Text = ex.Message.ToString();
            }

        }
        catch (Exception ex)
        {

        }

    }


}
