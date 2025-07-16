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
using System.Linq;

public partial class Task_D_DateChange : System.Web.UI.Page
{

    #region CreativeMethods
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
    #endregion

    SP_Methods spm = new SP_Methods();
    DataSet dspaymentVoucher_Apprs = new DataSet();
    String CEOInList = "N";
    double YearlymobileAmount = 0;
    #region PageEvents
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            //mobile_btnPrintPV.Visible = false;

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/TaskMonitoring");
            }
            else
            {

                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString.Count > 0)
                    {
                       // hdnRemid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["TaskMonitoringPath"]).Trim());
						//TXT_AssginmentDate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");					.
					    txt_Current_remark.Attributes.Add("maxlength", "500");
						editform.Visible = true;
                        divbtn.Visible = false;
                        divmsg.Visible = false;
                        hdnempcode.Value = Convert.ToString(Session["Empcode"]);                     
                        txt_Created_By.Text = Convert.ToString(Session["emp_loginName"]);
                        hdnTaskID.Value = Convert.ToString(Request.QueryString[0]);
                        CheckDueRequestSisApproveNUll();
                        hdnAttendeeID.Value = "0";
                        CheckLoginExecuterSupervisor(hdnempcode.Value);
                        BindData("All");
                        this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                        //hdnFamilyDetailID.Value = "0";
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



    #endregion

    private void CheckDueRequestSisApproveNUll()
    {

        SqlParameter[] spars = new SqlParameter[3];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "DueRequestSisApproveNUll";
        spars[1] = new SqlParameter("@Task_Id", SqlDbType.VarChar);
        spars[1].Value = hdnTaskID.Value;
        DataSet DS = spm.getDatasetList(spars, "SP_TASK_M_DETAILS");
        if (DS.Tables[0].Rows.Count <= 0)
        {
            Response.Redirect("~/procs/TaskMonitoring.aspx");
        }
    }

    private void CheckLoginExecuterSupervisor(string empCodelogin)
    {
        SqlParameter[] spars = new SqlParameter[3];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetloginCheckExecuterSupervisor";
        spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        spars[1].Value = empCodelogin;
        spars[2] = new SqlParameter("@Task_Id", SqlDbType.VarChar);
        spars[2].Value = hdnTaskID.Value;
        DataSet DS = spm.getDatasetList(spars, "SP_TASK_M_DETAILS");

        if (DS.Tables[0].Rows.Count <= 0)
        {
            if (DS.Tables[1].Rows.Count > 0)
            {
                lnk_Task_Create.Visible = false;
                lnk_Task_Update.Visible = false;
            }
            else
            {
                Response.Redirect("~/procs/TaskMonitoring.aspx");
            }
        }
    }

    #region PageMethods
    private void BindData(string qtype)
    {
        try
        {
            var empCode = Convert.ToString(Session["Empcode"]);
            var getVal = Convert.ToDouble(hdnTaskID.Value);
            var getdsTables = spm.getTaskMonitoringDDL("", Convert.ToDouble(getVal), "getTaskDetailsByDueDate");
            if (getdsTables.Tables.Count > 0)
            {
                var getds = getdsTables.Tables[0];
                if (getds.Rows.Count < 0)
                {
                    return;
                }
                var getTaskRefDetails= getdsTables.Tables[0];
                var getAttendeeDetails= getdsTables.Tables[1];
                var getTaskDetails= getdsTables.Tables[2];
                var getDeuDateDetails= getdsTables.Tables[3];
                var getTaskHistory= getdsTables.Tables[4];
                var getFileDetailsList= getdsTables.Tables[5];
                var getIntimationList = getdsTables.Tables[7];

                if (getAttendeeDetails.Rows.Count>0)
                {
                    txt_Organizer.Text = Convert.ToString(getAttendeeDetails.Rows[0]["Organizer"]);
                    txt_Attendees.Text = Convert.ToString(getAttendeeDetails.Rows[0]["Attendees"]);
                }
                if (getIntimationList.Rows.Count > 0)
                {
                    Txt_TaskIntimation.Text = Convert.ToString(getIntimationList.Rows[0]["Attendees"]);
                }

                if (getTaskRefDetails.Rows.Count > 0)
                {
                    txt_Ref_Id.Text = Convert.ToString(getTaskRefDetails.Rows[0]["Task_Reference_ID"]);
                    txt_Ref_Date.Text = Convert.ToString(getTaskRefDetails.Rows[0]["Task_Reference_Date"]);
                    txt_Metting_Title.Text = Convert.ToString(getTaskRefDetails.Rows[0]["Meeting_Discussion_Title"]);
                    txt_Metting_Dis.Text = Convert.ToString(getTaskRefDetails.Rows[0]["Meeting_Discussion_Date"]);
                    txt_MeetingType.Text = Convert.ToString(getTaskRefDetails.Rows[0]["MeetingTitle"]);
                }
                if (getTaskDetails.Rows.Count > 0)
                {
                    txt_Task_Id.Text = Convert.ToString(getTaskDetails.Rows[0]["Task_ID"]);
                    txt_Created_Date.Text = Convert.ToString(getTaskDetails.Rows[0]["Task_Creation_Date"]);
                    txt_Created_By.Text = Convert.ToString(getTaskDetails.Rows[0]["CreatedBy"]);//
                    chk_for_Info.Checked = Convert.ToBoolean(getTaskDetails.Rows[0]["For_Information_Only"]);
                   // txt_Supervisor.Text = Convert.ToString(getTaskDetails.Rows[0]["Executor"]);
                    txt_Executor.Text = Convert.ToString(getTaskDetails.Rows[0]["Executor"]);
                    txt_Supervisor.Text = Convert.ToString(getTaskDetails.Rows[0]["Supervisor"]);
                    txtprojectLocation.Text = Convert.ToString(getTaskDetails.Rows[0]["Location_name"]);

                    if (getDeuDateDetails.Rows.Count>0)
                    {
                        txt_Due_Date.Text = Convert.ToString(getDeuDateDetails.Rows[0]["Old_Due_Date"]);
                        txt_NewDueDate.Text = Convert.ToString(getDeuDateDetails.Rows[0]["New_Due_Date"]);
                        txt_Executer_Remarks.Text = Convert.ToString(getDeuDateDetails.Rows[0]["Remark"]);
                    }
                   // txt_Executer_Remarks.Text = Convert.ToString(getTaskDetails.Rows[0]["Action_Remarks"]);
                    txt_TaskDescripation.Text = Convert.ToString(getTaskDetails.Rows[0]["Task_Description"]);
                    txt_TaskRemark.Text = Convert.ToString(getTaskDetails.Rows[0]["Task_Remarks"]);
                    txt_Current_Status.Text = Convert.ToString(getTaskDetails.Rows[0]["StatusName"]);
                    //
                    hdnExCode.Value= Convert.ToString(getTaskDetails.Rows[0]["Task_Executer"]);
                    hdnSupcode.Value= Convert.ToString(getTaskDetails.Rows[0]["Task_Supervisor"]);
                    hdnTaskRefID.Value= Convert.ToString(getTaskDetails.Rows[0]["Task_Ref_id"]);
                }
                gv_TaskHistory.DataSource = null;
                gv_TaskHistory.DataBind();
                if (getTaskHistory.Rows.Count > 0)
                {
                    gv_TaskHistory.DataSource = getTaskHistory;
                    gv_TaskHistory.DataBind();

                }
                gv_Documents.DataSource = null;
                gv_Documents.DataBind();
                if (getFileDetailsList.Rows.Count > 0)
                {
                    IsShowUploadFile.Visible = true;
                    gv_Documents.DataSource = getFileDetailsList;
                    gv_Documents.DataBind();

                }
            }

        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void lnk_Task_Create_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            if (Convert.ToString(txt_Current_remark.Text).Trim() == "")
            {
                Label3.Text = "Enter task remark.";
                return;
            }
            var gettaskId = Convert.ToDouble(hdnTaskID.Value);
            var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();
            var getCurrent_remark = Convert.ToString(txt_Current_remark.Text).Trim();
            bool IsApprove = true;
            var getExecuter = Convert.ToString(hdnExCode.Value);
            var TaskRefId = Convert.ToString(hdnTaskRefID.Value);
            var getTaskEmailDetails = spm.getTaskMonitoringReport("getTaskDetailsDueDateApproved", getExecuter, Convert.ToDouble(TaskRefId), gettaskId);
            var status = spm.UpdateDueDateChangeStatus(gettaskId, "UpdateDueChangeRequest", getCreatedBy, getCurrent_remark, IsApprove,"");
            if (status == true)
            {
               if (getTaskEmailDetails != null)
                {
                    if (getTaskEmailDetails.Tables.Count > 0)
                    {
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
                        // getCurrent_remark = Convert.ToString(getTaskRefDetails.Rows[0]["Remark"]);
                        if (getTaskDetails1.Rows.Count > 0)
                        {
                            var link = "https://ess.highbartech.com/hrms/login.aspx?ReturnUrl=procs/InboxExecuter.aspx";
                            var ExecuterEmail = Convert.ToString(getTaskDetails1.Rows[0]["ExecuterEmail"]);
                            var ExecuterName = Convert.ToString(getTaskDetails1.Rows[0]["Executer"]);
                            var SupervisorName = Convert.ToString(getTaskDetails1.Rows[0]["Supervisor"]);
                            var SupervisorEmail = Convert.ToString(getTaskDetails1.Rows[0]["SupervisorEmail"]);
                           // var ParentTaskSupervisor = Convert.ToString(getTaskDetails1.Rows[0]["ParentTaskSupervisor"]);//AS CC

                            var AllTaskSupervisor = Convert.ToString(getAllTaskSupervisor.Rows[0]["CCEmailAddressSupervisor"]);//AS CC
                            var OnlymainTaskSupervisorEmail = Convert.ToString(getMainTaskSupervisor.Rows[0]["MainSupervisorEmail"]);//AS To
                            var SupervisorNameMain = Convert.ToString(getMainTaskSupervisor.Rows[0]["Emp_Name"]);//AS To Name

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
                            

                            spm.Task_Monitoring_Due_Date_Change(1, "Approved", getTaskDetails1, ExecuterEmail, Meeting_Discussion_Title, Meeting_Discussion_Date, MeetingType, Task_Reference_ID, link, AllTaskSupervisor, SupervisorNameMain, getCurrent_remark);
                        }
                    }
                }

                Response.Redirect("~/procs/Task_DueDateChange_Inbox.aspx");
            }
            else
            {
                Label3.Text = "Status not updated, Please try again.";
                return;
            }

        }
        catch (Exception ex)
        {
            Label3.Text = "Something went wrong , Please try again.";
            return;
        }
    }
    protected void lnk_Final_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/procs/TaskMonitoring.aspx");
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnk_View_History_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            s1.Visible = false;
            s2.Visible = false;
            s3.Visible = false;
            s4.Visible = false;
            s5.Visible = false;
            s6.Visible = false;
            s7.Visible = false;
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(gv_TaskHistory.DataKeys[row.RowIndex].Values[0]).Trim();
            if (fId != "0")
            {
                var getds = spm.getTaskMonitoringDDL("", Convert.ToDouble(fId), "getLogById");
                if (getds.Tables.Count > 0)
                {

                    var getTable = getds.Tables[0];
                    if(getTable.Rows.Count>0)
                    {
                        IsShowHistoryDetails.Visible = true;
                        s1.Visible = true;
                        s2.Visible = true;
                        s3.Visible = true;
                        s4.Visible = true;
                        s5.Visible = true;
                        s6.Visible = true;
                        s7.Visible = true;
                        TextBox1.Text = Convert.ToString(getTable.Rows[0]["ActionDate"]);
                        TextBox2.Text = Convert.ToString(getTable.Rows[0]["DueDate"]);
                        TextBox3.Text = Convert.ToString(getTable.Rows[0]["StatusName"]);
                        TextBox4.Text = Convert.ToString(getTable.Rows[0]["SupervisorName"]);
                        TextBox5.Text = Convert.ToString(getTable.Rows[0]["ActionBy"]);
                        TextBox6.Text = Convert.ToString(getTable.Rows[0]["ExecutorName"]);
                        TextBox7.Text = Convert.ToString(getTable.Rows[0]["Task_Remark"]);
                    }
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    #endregion

    protected void lnk_Task_Update_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            if (Convert.ToString(txt_Current_remark.Text).Trim() == "")
            {
                Label3.Text = "Enter task remark.";
                return;
            }
            var gettaskId = Convert.ToDouble(hdnTaskID.Value);
            var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();
            var getCurrent_remark = Convert.ToString(txt_Current_remark.Text).Trim();
            bool IsApprove = false;
            var getExecuter = Convert.ToString(hdnExCode.Value);
            var TaskRefId = Convert.ToString(hdnTaskRefID.Value);
            var getTaskEmailDetails = spm.getTaskMonitoringReport("getTaskDetailsDueDateRejected", getExecuter, Convert.ToDouble(TaskRefId), gettaskId);
            var status = spm.UpdateDueDateChangeStatus(gettaskId, "UpdateDueChangeRequest", getCreatedBy, getCurrent_remark, IsApprove,"");
            if(status==true)
            {               
                if (getTaskEmailDetails != null)
                {
                    if (getTaskEmailDetails.Tables.Count > 0)
                    {
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
                         
                        if (getTaskDetails1.Rows.Count > 0)
                        {
                            var link = "https://ess.highbartech.com/hrms/login.aspx?ReturnUrl=procs/InboxExecuter.aspx";
                            var ExecuterEmail = Convert.ToString(getTaskDetails1.Rows[0]["ExecuterEmail"]);
                            var ExecuterName = Convert.ToString(getTaskDetails1.Rows[0]["Executer"]);
                            var SupervisorName = Convert.ToString(getTaskDetails1.Rows[0]["Supervisor"]);
                            var SupervisorEmail = Convert.ToString(getTaskDetails1.Rows[0]["SupervisorEmail"]);
                             SupervisorEmail = "";

                            var AllTaskSupervisor = Convert.ToString(getAllTaskSupervisor.Rows[0]["CCEmailAddressSupervisor"]);//AS CC

                            var SupervisorNameMain = Convert.ToString(getMainTaskSupervisor.Rows[0]["Emp_Name"]);//AS To Name

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
                            // appendIntimation = Convert.ToString(getAllTaskIntimation.Rows[0]["CCEmailAddress"]);
                            spm.Task_Monitoring_Due_Date_Change(1, "Rejected", getTaskDetails1, ExecuterEmail, Meeting_Discussion_Title, Meeting_Discussion_Date, MeetingType, Task_Reference_ID, link, AllTaskSupervisor, SupervisorNameMain, getCurrent_remark);
                        }
                    }
                }

                Response.Redirect("~/procs/Task_DueDateChange_Inbox.aspx");
            }
            else
            {
                Label3.Text = "Status not updated, Please try again.";
                return;
            }

        }
        catch (Exception ex)
        {
            Label3.Text = "Something went wrong , Please try again.";
            return;
        }
    }
}
