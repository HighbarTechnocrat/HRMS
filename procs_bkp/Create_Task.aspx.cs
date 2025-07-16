using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using System.Linq;
using ClosedXML.Excel;
using System.Collections.Generic;

public partial class Create_Task : System.Web.UI.Page
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    // txt_SPOCComment.Attributes.Add("maxlength", "500");
                    txt_Metting_Title.Attributes.Add("maxlength", "250");
                    txt_TaskDescripation.Attributes.Add("maxlength", "500");
                    txt_TaskRemark.Attributes.Add("maxlength", "500");
                    txt_Metting_Dis.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txt_Due_Date.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["TaskMonitoringPath"]).Trim());
                    //TXT_AssginmentDate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                    editform.Visible = true;
                    divbtn.Visible = false;
                    divmsg.Visible = false;
                    hdnempcode.Value = Convert.ToString(Session["Empcode"]);
                    txt_Ref_Date.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    txt_Created_Date.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    txt_Created_By.Text = Convert.ToString(Session["emp_loginName"]);
                    hdnTaskRefID.Value = "0";
                    hdnAttendeeID.Value = "0";
                    hdnOldTaskRefID.Value = "0";
                    getProjectLocation();
                    DeleteTempAttendee();
                    BindData("All");
                    this.ddl_Attendees.Attributes.Add("disabled", "");
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                    //hdnFamilyDetailID.Value = "0";
                    uplTaskTemplate.Attributes.Clear();
                    lbl_Upload_Eror.Text = "";
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

    #region PageMethods
    private void BindData(string qtype)
    {
        try
        {
            var empCode = Convert.ToString(Session["Empcode"]);
            var getVal = Convert.ToDouble(hdnTaskRefID.Value);
            var getResult = spm.getTaskMonitoringDDL(empCode, getVal, "GetAllDDL_TEMP_Create");
            if (getResult != null)
            {
                if (getResult.Tables.Count > 0)
                {
                    if (qtype == "All")
                    {
                        var getMeetingType = getResult.Tables[0];
                        BindMettingType(getMeetingType);
                        var getTask_M_Reference = getResult.Tables[1];
                        BindMettingRefId(getTask_M_Reference);
                        var getAttendeeDDL = getResult.Tables[2];
                        var getSupervisorDDL = getResult.Tables[6];
                        BindAttendee(getAttendeeDDL);
                        BindTaskIntimation(getAttendeeDDL);
                        BindOrganizer(getAttendeeDDL);
                        //DataView view = new DataView(getSupervisorDDL);
                        //DataTable distinctValues = view.ToTable(true, "Emp_Code", "Emp_Name");
                        BindSupervisor(getSupervisorDDL);
                        BindExecutor(getSupervisorDDL);
                        var getTaskMR_Details = getResult.Tables[3];
                        if (getTaskMR_Details.Rows.Count > 0)
                        {
                            hdnTaskRefID.Value = Convert.ToString(getTaskMR_Details.Rows[0]["ID"]);
                            txt_Ref_Date.Text = Convert.ToString(getTaskMR_Details.Rows[0]["Task_Reference_Date"]);
                            txt_Metting_Title.Text = Convert.ToString(getTaskMR_Details.Rows[0]["Meeting_Discussion_Title"]);
                            txt_Metting_Dis.Text = Convert.ToString(getTaskMR_Details.Rows[0]["Meeting_Discussion_Date"]);
                            var Meeting_Type_Id = Convert.ToString(getTaskMR_Details.Rows[0]["Meeting_Type_Id"]);
                            var mettingId = ddl_Meeting_Type.SelectedValue.ToString();
                            ddl_Meeting_Type.Items.FindByValue(mettingId).Selected = false;
                            ddl_Meeting_Type.Items.FindByValue(Meeting_Type_Id).Selected = true;

                            var Old_Task_Ref_Id = Convert.ToString(getTaskMR_Details.Rows[0]["Old_Task_Ref_Id"]);
                            if (Old_Task_Ref_Id != "")
                            {
                                var OldTaskRefId = ddl_Meeting_Id.SelectedValue.ToString();
                                ddl_Meeting_Id.Items.FindByValue(OldTaskRefId).Selected = false;
                                ddl_Meeting_Id.Items.FindByValue(Meeting_Type_Id).Selected = true;

                                dg_OldTaskDetails.DataSource = null;
                                dg_OldTaskDetails.DataBind();
                                var getid = Convert.ToDouble(ddl_Meeting_Id.SelectedValue);
                                if (getid != 0)
                                {
                                    var getResult_Old = spm.getTempAttendee("GetRefTaskIds", "", getid);
                                    if (getResult_Old.Rows.Count > 0)
                                    {
                                        dg_OldTaskDetails.DataSource = getResult_Old;
                                        dg_OldTaskDetails.DataBind();
                                    }
                                    else
                                    {

                                    }
                                }
                                ddl_Meeting_Id.Enabled = false;
                            }

                        }
                        else
                        {
                            //hdnTaskRefID.Value = "0";
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
                        BindTaskIntimation(getAttendeeDDL);
                        var getSupervisorDDL = getResult.Tables[6];
                        // BindAttendee(getAttendeeDDL);
                        BindSupervisor(getSupervisorDDL);
                        BindExecutor(getSupervisorDDL);
                        var getOrganizerDDL = getResult.Tables[7];
                        if (getOrganizerDDL.Rows.Count > 0)
                        {
                            BindOrganizer(getOrganizerDDL);
                        }
                    }

                }
            }

        }
        catch (Exception ex)
        {

            throw;
        }
    }
    private void BindMettingType(DataTable dataTable)
    {
        try
        {
            var tempMeetingId = ddl_Meeting_Type.SelectedValue.ToString();
            if (tempMeetingId == "" || tempMeetingId == "0")
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
            var tempMeetingId = ddl_Meeting_Id.SelectedValue.ToString();
            if (tempMeetingId == "" || tempMeetingId == "0")
            {
                ddl_Meeting_Id.DataSource = null;
                ddl_Meeting_Id.DataBind();
                if (dataTable.Rows.Count > 0)
                {
                    ddl_Meeting_Id.DataSource = dataTable;
                    ddl_Meeting_Id.DataTextField = "Task_Reference_ID";
                    ddl_Meeting_Id.DataValueField = "ID";
                    ddl_Meeting_Id.DataBind();
                    ddl_Meeting_Id.Items.Insert(0, new ListItem("Select Meeting Reference", "0"));
                }
                else
                {
                    ddl_Meeting_Id.Items.Insert(0, new ListItem("Select Meeting Reference", "0"));
                }
            }
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
            ddl_Attendees.DataSource = null;
            ddl_Attendees.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                ddl_Attendees.DataSource = dataTable;
                ddl_Attendees.DataTextField = "Emp_Name";
                ddl_Attendees.DataValueField = "Emp_Code";
                ddl_Attendees.DataBind();
                ddl_Attendees.Items.Insert(0, new ListItem("Select Attendee", "0"));
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }

    private void BindTaskIntimation(DataTable dataTable)
    {
        try
        {
            DDl_TaskIntimation.DataSource = null;
            DDl_TaskIntimation.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                DDl_TaskIntimation.DataSource = dataTable;
                DDl_TaskIntimation.DataTextField = "Emp_Name";
                DDl_TaskIntimation.DataValueField = "Emp_Code";
                DDl_TaskIntimation.DataBind();
                DDl_TaskIntimation.Items.Insert(0, new ListItem("Select Task Intimation", "0"));
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }

    private void getProjectLocation()
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetDropDownprojectLocation";

            DataSet DS = spm.getDatasetList(spars, "SP_TASK_M_DETAILS");
            DDLProjectLocation.DataSource = DS.Tables[0];
            DDLProjectLocation.DataTextField = "Location_name";
            DDLProjectLocation.DataValueField = "comp_code";
            DDLProjectLocation.DataBind();
            DDLProjectLocation.Items.Insert(0, new ListItem("Select Project Location", "0"));
        }
        catch (Exception ex)
        {

        }
    }

    private void BindOrganizer(DataTable dataTable)
    {
        try
        {
            ddl_Organizer.DataSource = null;
            ddl_Organizer.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                ddl_Organizer.DataSource = dataTable;
                ddl_Organizer.DataTextField = "Emp_Name";
                ddl_Organizer.DataValueField = "Emp_Code";
                ddl_Organizer.DataBind();
                ddl_Organizer.Items.Insert(0, new ListItem("Select Organizer", "0"));
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }
    private void BindGridAttendee(DataTable dataTable)
    {
        //try
        //{
        dg_ATT_Details.DataSource = null;
        dg_ATT_Details.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            dg_ATT_Details.DataSource = dataTable;
            dg_ATT_Details.DataBind();

            var rows2 = from row in dataTable.AsEnumerable()
                        where row.Field<Boolean>("IsOrganizer") == true
                        select row;
            int count2 = rows2.Count<DataRow>();
            if (count2 > 0)
            {
                btn_ATT_Save.Visible = true;
                this.ddl_Attendees.Attributes.Remove("disabled");
                btn_Org_Save.Visible = false;
                ddl_Organizer.Enabled = false;
            }
            else
            {
                btn_ATT_Save.Visible = false;
                this.ddl_Attendees.Attributes.Add("disabled", "");
                btn_Org_Save.Visible = true;
                ddl_Organizer.Enabled = true;
            }
            foreach (DataRow item in dataTable.Rows)
            {
                var getName = item["Attendee_Name"].ToString();

                foreach (ListItem itm in ddl_Attendees.Items)
                {
                    if (itm.Text == getName)
                    {
                        itm.Attributes.Add("disabled", "disabled");
                    }
                }
                foreach (ListItem itm in ddl_Organizer.Items)
                {
                    if (itm.Text == getName)
                    {
                        itm.Attributes.Add("disabled", "disabled");
                    }
                }
            }
        }
        else
        {
            btn_ATT_Save.Visible = false;
            this.ddl_Attendees.Attributes.Add("disabled", "");
            btn_Org_Save.Visible = true;
            ddl_Organizer.Enabled = true;
        }

        //}
        //catch (Exception ex)
        //{
        //	lblmessage.Text = ex.Message.ToString();
        //}
    }

    private void BindGridIntimation(DataTable dataTable)
    {

        GridTaskIntimation.DataSource = null;
        GridTaskIntimation.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            GridTaskIntimation.DataSource = dataTable;
            GridTaskIntimation.DataBind();
        }
        else
        {
        }
    }

    private void DeleteTempAttendee()
    {
        //try
        //{
        var getEmpCode = Convert.ToString(Session["Empcode"]);
        spm.InsertUpdateTempAttendee(0, 0, "DELETETempAttendee", getEmpCode, getEmpCode, false);
        //}
        //catch (Exception ex)
        //{

        //}
    }
    private void ClearAttendee()
    {
        chk_Ref_Select.Checked = false;
        //btn_ATT_Save.Visible = true;
        btn_ATT_Update.Visible = false;
        btn_FD_Cancel.Visible = false;
        //btn_Org_Save.Visible = true;
        btn_Org_Update.Visible = false;
        BindData("BindDDLAttendee");
    }

    private void ClearIntimation()
    {
        BindData("BindDDLAttendee");
    }

    private void ClearTask()
    {
        //try
        //{
        BindData("All");
        getProjectLocation();
        lnk_Task_Update.Visible = false;
        lnk_Task_Cancel.Visible = false;
        lnk_Task_Create.Visible = true;
        chk_for_Info.Checked = false;
        txt_TaskDescripation.Text = "";
        txt_TaskRemark.Text = "";
        txt_Due_Date.Text = "";
        txt_Reminder_Day.Text = "1";
        txt_Reminder_Repe_Day.Text = "1";
        chk_Reminder_Due_Date.Checked = true;
        chk_Escalation_Due_Date.Checked = true;
        chk_Escalation_Repeate.Checked = false;

        ddl_Task_Supervisor.Enabled = true;
        ddl_Task_Executor.Enabled = true;
        txt_Due_Date.Enabled = true;
        // txt_TaskDescripation.Enabled = true;
        //txt_TaskRemark.Enabled = true;
        uploadfile.Enabled = true;
        chk_Reminder_Due_Date.Enabled = true;
        chk_Reminder_Due_Date.Checked = true;

        txt_Reminder_Day.Text = "1";
        txt_Reminder_Day.Enabled = true;

        chk_Escalation_Due_Date.Enabled = true;
        chk_Escalation_Due_Date.Checked = true;

        txt_Reminder_Repe_Day.Text = "1";
        txt_Reminder_Repe_Day.Enabled = true;
        //}
        //catch (Exception)
        //{

        //	throw;
        //}
    }
    private void BindGridTask(DataTable dataTable)
    {
        try
        {
            dv_TaskList.DataSource = null;
            dv_TaskList.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                IsShowTaskList.Visible = true;
                IsShowTaskList1.Visible = true;
                IsShowTaskList12.Visible = true;
                IsShowTaskList2.Visible = true;
                IsShowTaskList3.Visible = true;
                IsShowTaskList4.Visible = true;
                ddl_Meeting_Id.Enabled = true;
                dv_TaskList.DataSource = dataTable;
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
    private void BindSupervisor(DataTable dataTable)
    {
        try
        {
            ddl_Task_Supervisor.DataSource = null;
            ddl_Task_Supervisor.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                ddl_Task_Supervisor.DataSource = dataTable;
                ddl_Task_Supervisor.DataTextField = "Emp_Name";
                ddl_Task_Supervisor.DataValueField = "Emp_Code";
                ddl_Task_Supervisor.DataBind();
                ddl_Task_Supervisor.Items.Insert(0, new ListItem("Select Supervisor", "0"));
            }
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
            ddl_Task_Executor.DataSource = null;
            ddl_Task_Executor.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                ddl_Task_Executor.DataSource = dataTable;
                ddl_Task_Executor.DataTextField = "Emp_Name";
                ddl_Task_Executor.DataValueField = "Emp_Code";
                ddl_Task_Executor.DataBind();
                ddl_Task_Executor.Items.Insert(0, new ListItem("Select Executor", "0"));
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }
    #endregion
    //Add New refarance
    protected void btn_Ref_Add_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            if (Convert.ToString(txt_Metting_Title.Text).Trim() == "")
            {
                Label2.Text = "Enter meeting title.";
                return;
            }
            if (Convert.ToString(txt_Metting_Dis.Text).Trim() == "")
            {
                Label2.Text = "Enter meeting date.";
                return;
            }
            if (Convert.ToString(ddl_Meeting_Type.SelectedValue).Trim() == "0" || Convert.ToString(ddl_Meeting_Type.SelectedValue).Trim() == "")
            {
                Label2.Text = "Select meeting type.";
                return;
            }
            var IsSave = false;
            //Click on Add reference
            foreach (GridViewRow row in dg_OldTaskDetails.Rows)

            {
                LinkButton btn = (LinkButton)sender;
                int iIsDraft = Convert.ToInt32(btn.CommandArgument);
                //Finding Dropdown control  
                CheckBox ddl1 = row.FindControl("chk_Ref_Select") as CheckBox;
                if (ddl1.Checked == true)
                {
                    IsSave = true;
                    var empCode = Convert.ToString(Session["Empcode"]);
                    //Add refanace 
                    var getOldTaskRef = Convert.ToDouble(0);
                    var getMettingOldref = ddl_Meeting_Id.SelectedValue.ToString();

                    if (getMettingOldref != "")
                    {
                        getOldTaskRef = Convert.ToDouble(getMettingOldref);
                        hdnOldTaskRefID.Value = getOldTaskRef.ToString();
                    }
                    else
                    {
                        hdnOldTaskRefID.Value = getOldTaskRef.ToString();
                    }
                    var getRefAdd = Convert.ToDouble(hdnTaskRefID.Value);
                    if (getRefAdd == 0)

                    {
                        var getMettingDiscussionTitle = Convert.ToString(txt_Metting_Title.Text).ToString();
                        var getMettingDiscussionDate = "";
                        var getTempMeetingDate = Convert.ToString(txt_Metting_Dis.Text).ToString();
                        var splitmeetingDate = getTempMeetingDate.Split('/');
                        getMettingDiscussionDate = splitmeetingDate[2] + "-" + splitmeetingDate[1] + "-" + splitmeetingDate[0];
                        var getMettingDiscussionType = Convert.ToString(ddl_Meeting_Type.SelectedValue).ToString();


                        var getStatus = spm.InsertUpdateTaskDetails(0, 0, "AddTaskReference_Temp", empCode, empCode, getMettingDiscussionTitle, getMettingDiscussionDate, Convert.ToInt32(getMettingDiscussionType), false, getOldTaskRef, "", "", "", "", "", "", false, 0, false, 0, false, 0, "", iIsDraft);
                        if (Convert.ToDouble(getStatus) != 0)
                        {
                            hdnTaskRefID.Value = getStatus.ToString();
                            getRefAdd = Convert.ToDouble(getStatus);
                        }
                    }

                    Label lblTask_id = row.FindControl("lblTask_id") as Label;
                    var getlblTask_id = Convert.ToInt32(lblTask_id.Text.Trim());

                    var getStatusTask = spm.InsertUpdateTaskDetails(getlblTask_id, getRefAdd, "AddTaskRef_Temp", empCode, empCode, "", "", 0, false, getOldTaskRef, "", "", "", "", "", "", false, 0, false, 0, false, 0, "", iIsDraft);

                }
            }
            if (IsSave == true)
            {
                ClearTask();
            }
            else
            {
                Label2.Text = "Select any one task.";
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btn_FD_Cancel_Click(object sender, EventArgs e)
    {
        try
        {
            // Click on cancel reference
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddl_Meeting_Id_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            showOldRef.Visible = true;
            showOldRef.Visible = false;
            dg_OldTaskDetails.DataSource = null;
            dg_OldTaskDetails.DataBind();
            CheckOrganizer_Attendee();
            var getid = Convert.ToDouble(ddl_Meeting_Id.SelectedValue);
            if (getid != 0)
            {
                var getResult = spm.getTempAttendee("GetRefTaskIds", "", getid);
                if (getResult.Rows.Count > 0)
                {
                    dg_OldTaskDetails.DataSource = getResult;
                    dg_OldTaskDetails.DataBind();
                }
                else
                {
                    showOldRef.Visible = false;
                }

            }
            else
            {

            }
        }
        catch (Exception ex)
        {

        }
    }
    //
    protected void ddl_Meeting_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_Task_Supervisor.SelectedIndex > 0)
            {
                //CheckOrganizer_Attendee();
            }
            var getVal = Convert.ToString(ddl_Meeting_Type.SelectedValue);
            if (getVal == "4")
            {
                SelectOrganizer_Attendee();
            }
        }
        catch (Exception)
        {

            throw;
        }

    }

    private void CheckOrganizer_Attendee()
    {
        try
        {
            var getAtt = false;
            var getOrg = false;
            Label3.Text = "";
            if (dg_ATT_Details.Rows.Count > 0)
            {
                foreach (GridViewRow row in dg_ATT_Details.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chk_Ref_Select") as CheckBox);
                        if (chkRow.Checked)
                        {
                            getOrg = true;
                        }
                    }
                }
                if (dg_ATT_Details.Rows.Count >= 2)
                {
                    getAtt = true;
                }
                else if (ddl_Meeting_Type.SelectedItem.Value == "4")
                {
                    getAtt = true;
                }
            }
            if (getOrg == false)
            {
                ddl_Task_Supervisor.SelectedIndex = -1;
                ddl_Task_Executor.SelectedIndex = -1;
                Label3.Text = "Please add Organizer.";
                return;
            }
            if (getAtt == false)
            {
                ddl_Task_Supervisor.SelectedIndex = -1;
                ddl_Task_Executor.SelectedIndex = -1;
                Label3.Text = "Please add attendees.";
                return;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void txt_Metting_Dis_TextChanged(object sender, EventArgs e)
    {
        Label2.Text = "";
        try
        {
            var getDate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tempF = DateTime.ParseExact(txt_Metting_Dis.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tempT = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (tempF > tempT)
            {
                Label2.Text = "Meeting/Dicussion Date cannot be future date";
                txt_Metting_Dis.Text = "";
                return;
            }
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
        //try
        //{
        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }
        Label1.Text = "";
        var empCode = Convert.ToString(Session["Empcode"]);
        var getVal = Convert.ToInt32(hdnTaskRefID.Value);
        var IsSelect = false;
        var count = 0;
        var getOrganizer = chk_Ref_Select.Checked;
        foreach (ListItem item in ddl_Attendees.Items)
        {
            if (item.Selected)
            {
                if (item.Value != "" && item.Value != "0")
                {
                    IsSelect = true;
                    count++;
                }
            }
        }

        if (IsSelect == false)
        {
            Label1.Text = "Please select attendee";
            return;
        }
        if (count >= 2 && getOrganizer == true)
        {
            Label1.Text = "Please select only one organizer";
            return;
        }

        // var getAttendee = Convert.ToString(ddl_Attendees.SelectedValue).Trim();
        var getAttendee = "";

        //if (getOrganizer == true)
        //{
        //	var getOrginserStatus = spm.getTempAttendee("CheckIsOrginser", empCode, getVal);
        //	if (getOrginserStatus != null)
        //	{
        //		if (getOrginserStatus.Rows.Count > 0)
        //		{
        //			var getMsg = Convert.ToString(getOrginserStatus.Rows[0]["Message"]);
        //			if (getMsg == "Exists")
        //			{
        //				Label1.Text = "Please select one organizer ";
        //				return;
        //			}
        //		}
        //	}
        //}
        var qtype = "";
        if (getVal == 0)
        {
            qtype = "AddTempAttendee";
        }
        else
        {
            qtype = "AddTempAttendee";
        }
        var getStatus = false;
        foreach (ListItem item in ddl_Attendees.Items)
        {
            if (item.Selected)
            {
                if (item.Value != "" && item.Value != "0")
                {
                    getAttendee = item.Value;
                    getStatus = spm.InsertUpdateTempAttendee(0, getVal, qtype, getAttendee, empCode, getOrganizer);
                }
            }
        }
        //  var getAttendeeList = new DataTable();
        //var 
        if (getStatus == false)
        {
            Label1.Text = "Something went wrong";
            return;
        }
        else
        {
            ClearAttendee();
            if (getVal == 0)
            {
                var getAttendeeList = spm.getTempAttendee("GetTempAttendee", empCode, 0);
                BindGridAttendee(getAttendeeList);
            }
            else
            {
                var getAttendeeList = spm.getTempAttendee("GetTempAttendee", empCode, getVal);
                BindGridAttendee(getAttendeeList);
            }
        }

        //}
        //catch (Exception ex)
        //{

        //}
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
            var IsSelect = false;
            var count = 0;
            var getOrganizer = chk_Ref_Select.Checked;
            foreach (ListItem item in ddl_Attendees.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        IsSelect = true;
                        count++;
                    }
                }
            }

            if (IsSelect == false)
            {
                Label1.Text = "Please select attendee";
                return;
            }
            if (count >= 2 && getOrganizer == true)
            {
                Label1.Text = "Please select only one organizer";
                return;
            }
            //if (Convert.ToString(ddl_Attendees.SelectedValue).Trim() == "0")
            //{
            //    Label1.Text = "Please select relation";
            //    return;
            //}
            //var getAttendee = Convert.ToString(ddl_Attendees.SelectedValue).Trim();
            var getAttendee = "";

            //if (getOrganizer == true)
            //{
            //	var getOrginserStatus = spm.getTempAttendee("CheckIsOrginserUpdate", empCode, getId);
            //	if (getOrginserStatus != null)
            //	{
            //		if (getOrginserStatus.Rows.Count > 0)
            //		{
            //			var getMsg = Convert.ToString(getOrginserStatus.Rows[0]["Message"]);
            //			if (getMsg == "Exists")
            //			{
            //				Label1.Text = "Please select only one organizer ";
            //				return;
            //			}
            //		}
            //	}
            //}
            var qtype = "";
            if (getVal == 0)
            {
                qtype = "UpdateTempAttendee";
            }
            else
            {
                qtype = "UpdateTempAttendee";
            }
            var getStatus = false;
            foreach (ListItem item in ddl_Attendees.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        getAttendee = item.Value;
                        getStatus = spm.InsertUpdateTempAttendee(getId, getVal, qtype, getAttendee, empCode, getOrganizer);
                    }
                }
            }

            // var getStatus = spm.InsertUpdateTempAttendee(getId, getVal, qtype, getAttendee, empCode, getOrganizer);
            if (getStatus == false)
            {
                Label1.Text = "Something went wrong";
                return;
            }
            else
            {
                ClearAttendee();
                if (getVal == 0)
                {
                    var getAttendeeList = spm.getTempAttendee("GetTempAttendee", empCode, 0);
                    BindGridAttendee(getAttendeeList);
                }
                else
                {
                    var getAttendeeList = spm.getTempAttendee("GetTempAttendee", empCode, getVal);
                    BindGridAttendee(getAttendeeList);
                }
            }

        }
        catch (Exception ex)
        {

        }
    }
    //Click On cancel Attendee 

    protected void lnk_DD_TaskIntimationSave_Click(object sender, EventArgs e)
    {
        //try
        //{
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }
        Label4.Text = "";
        var empCode = Convert.ToString(Session["Empcode"]);
        var getVal = Convert.ToInt32(hdnTaskRefID.Value);
        var IsSelect = false;
        var count = 0;
        var getOrganizer = chk_Ref_Select.Checked;
        foreach (ListItem item in DDl_TaskIntimation.Items)
        {
            if (item.Selected)
            {
                if (item.Value != "" && item.Value != "0")
                {
                    IsSelect = true;
                    count++;
                }
            }
        }

        if (IsSelect == false)
        {
            Label4.Text = "Please select Task Intimation";
            return;
        }
        var getAttendee = "";
        var qtype = "";
        if (getVal == 0)
        {
            qtype = "AddTempIntimation";
        }
        else
        {
            qtype = "AddTempIntimation";
        }
        var getStatus = false;
        foreach (ListItem item in DDl_TaskIntimation.Items)
        {
            if (item.Selected)
            {
                if (item.Value != "" && item.Value != "0")
                {
                    getAttendee = item.Value;

                    SqlParameter[] sparsDuplicate = new SqlParameter[6];
                    sparsDuplicate[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                    sparsDuplicate[0].Value = "DuplicateTempIntimation";
                    sparsDuplicate[1] = new SqlParameter("@Task_Ref_Id", SqlDbType.VarChar);
                    sparsDuplicate[1].Value = Convert.ToString(getVal);
                    sparsDuplicate[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                    sparsDuplicate[2].Value = Convert.ToString(getAttendee);
                    sparsDuplicate[3] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
                    sparsDuplicate[3].Value = Convert.ToString(empCode);
                    DataSet DSS = spm.getDatasetList(sparsDuplicate, "SP_TASK_M_DETAILS");

                    if (DSS.Tables[0].Rows.Count > 0)
                    {
                        ClearIntimation();
                        Label4.Text = "Already exists Task Intimation";
                        return;
                    }
                    else
                    {
                        SqlParameter[] spars = new SqlParameter[6];
                        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                        spars[0].Value = Convert.ToString(qtype);
                        spars[1] = new SqlParameter("@Id", SqlDbType.VarChar);
                        spars[1].Value = "0";
                        spars[2] = new SqlParameter("@Task_Ref_Id", SqlDbType.VarChar);
                        spars[2].Value = Convert.ToString(getVal);
                        spars[3] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                        spars[3].Value = Convert.ToString(getAttendee);
                        spars[4] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
                        spars[4].Value = Convert.ToString(empCode);
                        DataSet DS = spm.getDatasetList(spars, "SP_TASK_M_DETAILS");
                    }

                }
            }
        }

        ClearIntimation();
        if (getVal == 0)
        {
            var getIntimationList = spm.getTempIntimation("GetTempIntimation", empCode, 0);
            BindGridIntimation(getIntimationList);
        }
        else
        {
            var getIntimationList = spm.getTempIntimation("GetTempIntimation", empCode, getVal);
            BindGridIntimation(getIntimationList);
        }
    }

    //Click on Add TaskIntimation

    protected void lnk_DD_TaskIntimationUpdate_Click(object sender, EventArgs e)
    {

    }

    //Click on Update TaskIntimation

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
            // var getSelectedValues = ddl_Attendees.SelectedValue.ToString();
            // ddl_Attendees.Items.FindByValue(getSelectedValues).Selected = false;
            foreach (ListItem itm in ddl_Attendees.Items)
            {
                if (itm.Selected)
                {
                    itm.Selected = false;
                }
            }
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
                    getds = spm.getTempAttendee("GetTempAttendeeById", "", Convert.ToDouble(fId));
                }
                hdnAttendeeID.Value = fId;
                if (getds != null)
                {
                    if (getds.Rows.Count > 0)
                    {

                        if (getds.Rows.Count > 0)
                        {


                            var getEmpId = getds.Rows[0]["Emp_Code"].ToString();
                            chk_Ref_Select.Checked = Convert.ToBoolean(getds.Rows[0]["IsOrganizer"].ToString());
                            if (Convert.ToBoolean(getds.Rows[0]["IsOrganizer"].ToString()) != true)
                            {
                                btn_ATT_Save.Visible = false;
                                foreach (ListItem itm in ddl_Attendees.Items)
                                {
                                    if (itm.Value == getEmpId)
                                    {
                                        itm.Selected = true;
                                    }
                                }
                                btn_ATT_Update.Visible = true;
                                btn_FD_Cancel.Visible = true;
                            }
                            else
                            {
                                ddl_Organizer.SelectedValue = getEmpId;
                                btn_Org_Save.Visible = false;
                                btn_Org_Update.Visible = true;
                            }



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
                    result = spm.DeleteAttendeeDetails(Convert.ToDouble(fId), "DeleteTempAttendeeById");
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

    protected void lnk_AtendTemp_Delete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(GridTaskIntimation.DataKeys[row.RowIndex].Values[0]).Trim();
            if (fId != null)
            {
                var empCode = Convert.ToString(Session["Empcode"]);
                var id = int.Parse(fId);
                var result = false;
                var getVal = Convert.ToInt32(hdnTaskRefID.Value);
                if (getVal == 0)
                {
                    result = spm.DeleteTaskIntimationDetails(Convert.ToDouble(fId), "DeleteTempIntimationById");
                }
                else
                {
                    result = spm.DeleteTaskIntimationDetails(Convert.ToDouble(fId), "DeleteTempIntimationById");
                }
                if (result == true)
                {

                }
                else
                {
                    Label1.Text = "Something went wrong , Please try again.";
                    return;
                }

                ClearIntimation();
                if (getVal == 0)
                {
                    var getIntimationList = spm.getTempIntimation("GetTempIntimation", empCode, 0);
                    BindGridIntimation(getIntimationList);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void txt_Due_Date_TextChanged(object sender, EventArgs e)
    {
        Label3.Text = "";
        try
        {
            var getDate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tempF = DateTime.ParseExact(txt_Due_Date.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tempT = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (tempF < tempT)
            {
                Label3.Text = "Task due date cannot be less than today date";
                txt_Due_Date.Text = "";
                return;
            }
        }
        catch (Exception ex)
        {

        }
        return;
    }
    //Click on Create Task
    protected void lnk_Task_Create_Click(object sender, EventArgs e)
    {
        //try
        //{
        // LinkButton btn = (LinkButton)sender;

        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }
        LinkButton btn = (LinkButton)sender;

        if (btn.Text == "Create")
        {
            if (Convert.ToString(txt_Metting_Title.Text).Trim() == "")
            {
                Label2.Text = "Please enter Meeting/Discussion Title.";
                return;
            }
            if (Convert.ToString(txt_Metting_Dis.Text).Trim() == "")
            {
                Label2.Text = "Please enter Meeting/Discussion Date.";
                return;
            }
            if (Convert.ToString(ddl_Meeting_Type.SelectedValue).Trim() == "0" || Convert.ToString(ddl_Meeting_Type.SelectedValue).Trim() == "")
            {
                Label2.Text = "Please enter Meeting/Discussion Type.";
                return;
            }
            var getAtt = false;
            var getOrg = false;
            if (dg_ATT_Details.Rows.Count > 0)
            {
                foreach (GridViewRow row in dg_ATT_Details.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chk_Ref_Select") as CheckBox);
                        if (chkRow.Checked)
                        {
                            getOrg = true;
                        }
                    }
                }
                if (dg_ATT_Details.Rows.Count >= 2)
                {
                    getAtt = true;
                }
                else if (ddl_Meeting_Type.SelectedItem.Value == "4")
                {
                    getAtt = true;
                }

            }
            if (getOrg == false)
            {
                Label1.Text = "Please add Organizer.";
                return;
            }
            if (getAtt == false)
            {
                Label1.Text = "Please add attendees.";
                return;
            }
            if (chk_for_Info.Checked == false)
            {
                if (Convert.ToString(ddl_Task_Supervisor.SelectedValue).Trim() == "0" || Convert.ToString(ddl_Task_Supervisor.SelectedValue).Trim() == "")
                {
                    Label3.Text = "Please add at least one Organizer.";
                    return;
                }
                if (Convert.ToString(ddl_Task_Executor.SelectedValue).Trim() == "0" || Convert.ToString(ddl_Task_Executor.SelectedValue).Trim() == "")
                {
                    Label3.Text = "Please select task executor.";
                    return;
                }
            }
            if (Convert.ToString(DDLProjectLocation.SelectedValue).Trim() == "0" || Convert.ToString(DDLProjectLocation.SelectedValue).Trim() == "")
            {
                Label3.Text = "Please select Project Location.";
                return;
            }
            if (Convert.ToString(txt_TaskDescripation.Text).Trim() == "")
            {
                Label3.Text = "Please enter task descripation.";
                return;
            }
            if (Convert.ToString(txt_TaskRemark.Text).Trim() == "")
            {
                Label3.Text = "Please enter task remark.";
                return;
            }
            if (chk_for_Info.Checked == false)
            {
                if (Convert.ToString(txt_Due_Date.Text).Trim() == "")
                {
                    Label3.Text = "Please select task due date.";
                    return;
                }
                if (Convert.ToBoolean(chk_Reminder_Due_Date.Checked) == true)
                {
                    if (Convert.ToString(txt_Reminder_Day.Text).Trim() == "")
                    {
                        Label3.Text = "Please enter reminder before day.";
                        return;
                    }
                }
                if (Convert.ToBoolean(chk_Escalation_Due_Date.Checked) == true)
                {
                    if (Convert.ToString(txt_Reminder_Repe_Day.Text).Trim() == "")
                    {
                        Label3.Text = "Please enter escalation mail frequency.";
                        return;
                    }
                }
            }
            //getting Values
            var filename = "";
            var getRefId = Convert.ToDouble(hdnTaskRefID.Value);
            var getOldTaskRef = Convert.ToDouble(hdnOldTaskRefID.Value);
            var getMettingDiscussionTitle = Convert.ToString(txt_Metting_Title.Text).ToString();
            var getMettingDiscussionDate = "";
            var getTempMeetingDate = Convert.ToString(txt_Metting_Dis.Text).ToString();
            var splitmeetingDate = getTempMeetingDate.Split('/');
            getMettingDiscussionDate = splitmeetingDate[2] + "-" + splitmeetingDate[1] + "-" + splitmeetingDate[0];
            var getMettingDiscussionType = Convert.ToString(ddl_Meeting_Type.SelectedValue).ToString();
            //var getMettingOldref = ddl_Meeting_Id.SelectedValue.ToString();
            //if (getMettingOldref != "")
            //{
            //    getOldTaskRef = Convert.ToDouble(getMettingOldref);
            //}
            var getTaskSupervisor = Convert.ToString(ddl_Task_Supervisor.SelectedValue).ToString();
            var getTaskExecutor = Convert.ToString(ddl_Task_Executor.SelectedValue).ToString();
            var getFor_Info = Convert.ToBoolean(chk_for_Info.Checked);
            var getTaskDescripation = Convert.ToString(txt_TaskDescripation.Text).ToString();
            var getTaskRemark = Convert.ToString(txt_TaskRemark.Text).ToString();
            var getDueDate = Convert.ToString(txt_Due_Date.Text).ToString();
            var FinalDueDate = "";
            var getReminder_Due_Date = false;
            var getReminderDay = 0;
            var getEscalationDay = 0;
            var getEscalation_Due_Date = false;
            var getEscalation_Repeate = false;
            if (chk_for_Info.Checked == false)
            {
                var splitDueDate = getDueDate.Split('/');
                FinalDueDate = splitDueDate[2] + "-" + splitDueDate[1] + "-" + splitDueDate[0];
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
                    strfileName = "TASK_" + getTaskExecutor + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
                    filename = strfileName;
                    uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["TaskMonitoringPath"]).Trim()), strfileName));
                }
                getReminder_Due_Date = Convert.ToBoolean(chk_Reminder_Due_Date.Checked);

                if (getReminder_Due_Date == true)
                {
                    getReminderDay = Convert.ToInt32(txt_Reminder_Day.Text);
                }
                getEscalation_Due_Date = Convert.ToBoolean(chk_Escalation_Due_Date.Checked);
                if (getEscalation_Due_Date == true)
                {
                    getEscalationDay = Convert.ToInt32(txt_Reminder_Repe_Day.Text);
                }
                getEscalation_Repeate = Convert.ToBoolean(chk_Escalation_Repeate.Checked);
            }
            var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();
            var ProjectLocation = Convert.ToString(DDLProjectLocation.SelectedValue).ToString();
            //LinkButton btn = (LinkButton)sender;
            //  int storeValue = Convert.ToInt32(btn.CommandArgument);
            var getStatus = spm.InsertUpdateTaskDetails(0, getRefId, "INSERTTASKDETAILS_TEMP", getCreatedBy, getCreatedBy, getMettingDiscussionTitle, getMettingDiscussionDate, Convert.ToInt32(getMettingDiscussionType), getFor_Info, getOldTaskRef, getTaskSupervisor, getTaskExecutor, getTaskDescripation, getTaskRemark, FinalDueDate, filename, getReminder_Due_Date, getReminderDay, getEscalation_Due_Date, getEscalationDay, getEscalation_Repeate, 0, ProjectLocation, 0);
            if (Convert.ToDouble(getStatus) != 0)
            {
                hdnTaskRefID.Value = getStatus.ToString();
                ClearTask();
            }
            else
            {
                Label3.Text = "Something went wrong. please try again";
                return;
            }
        }
        else if (btn.Text == "Update")
        {
            if (chk_for_Info.Checked == false)
            {
                if (Convert.ToString(ddl_Task_Supervisor.SelectedValue).Trim() == "0" || Convert.ToString(ddl_Task_Supervisor.SelectedValue).Trim() == "")
                {
                    Label3.Text = "Please select task supervisor.";
                    return;
                }
                if (Convert.ToString(ddl_Task_Executor.SelectedValue).Trim() == "0" || Convert.ToString(ddl_Task_Executor.SelectedValue).Trim() == "")
                {
                    Label3.Text = "Please select task executor.";
                    return;
                }
            }
            if (Convert.ToString(txt_TaskDescripation.Text).Trim() == "")
            {
                Label3.Text = "Please enter task descripation.";
                return;
            }
            if (Convert.ToString(txt_TaskRemark.Text).Trim() == "")
            {
                Label3.Text = "Please enter task remark.";
                return;
            }
            if (chk_for_Info.Checked == false)
            {
                if (Convert.ToString(txt_Due_Date.Text).Trim() == "")
                {
                    Label3.Text = "Please select task due date.";
                    return;
                }
                if (Convert.ToBoolean(chk_Reminder_Due_Date.Checked) == true)
                {
                    if (Convert.ToString(txt_Reminder_Day.Text).Trim() == "")
                    {
                        Label3.Text = "Please enter reminder before day.";
                        return;
                    }
                }
                if (Convert.ToBoolean(chk_Escalation_Due_Date.Checked) == true)
                {
                    if (Convert.ToString(txt_Reminder_Repe_Day.Text).Trim() == "")
                    {
                        Label3.Text = "Please enter escalation mail frequency.";
                        return;
                    }
                }
            }
            //getting Values
            var filename = "";
            var getRefId = Convert.ToDouble(hdnTaskRefID.Value);
            var getOldTaskRef = Convert.ToDouble(hdnOldTaskRefID.Value);
            var getMettingDiscussionTitle = Convert.ToString(txt_Metting_Title.Text).ToString();
            var getMettingDiscussionDate = "";
            var getTempMeetingDate = Convert.ToString(txt_Metting_Dis.Text).ToString();
            var splitmeetingDate = getTempMeetingDate.Split('/');
            getMettingDiscussionDate = splitmeetingDate[2] + "-" + splitmeetingDate[1] + "-" + splitmeetingDate[0];
            var getMettingDiscussionType = Convert.ToString(ddl_Meeting_Type.SelectedValue).ToString();
            //var getMettingOldref = ddl_Meeting_Id.SelectedValue.ToString();
            //if (getMettingOldref != "")
            //{
            //    getOldTaskRef = Convert.ToDouble(getMettingOldref);
            //}
            var getTaskSupervisor = Convert.ToString(ddl_Task_Supervisor.SelectedValue).ToString();
            var getTaskExecutor = Convert.ToString(ddl_Task_Executor.SelectedValue).ToString();
            var getFor_Info = Convert.ToBoolean(chk_for_Info.Checked);
            var getTaskDescripation = Convert.ToString(txt_TaskDescripation.Text).ToString();
            var getTaskRemark = Convert.ToString(txt_TaskRemark.Text).ToString();
            var getDueDate = Convert.ToString(txt_Due_Date.Text).ToString();
            var FinalDueDate = "";
            var getReminder_Due_Date = false;
            var getReminderDay = 0;
            var getEscalation_Repeate = false;
            var getEscalationDay = 0;
            var getEscalation_Due_Date = false;

            if (chk_for_Info.Checked == false)
            {
                var splitDueDate = getDueDate.Split('/');
                FinalDueDate = splitDueDate[2] + "-" + splitDueDate[1] + "-" + splitDueDate[0];
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
                    strfileName = "TASK_" + getTaskExecutor + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
                    filename = strfileName;
                    uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["TaskMonitoringPath"]).Trim()), strfileName));
                }
                getReminder_Due_Date = Convert.ToBoolean(chk_Reminder_Due_Date.Checked);
                if (getReminder_Due_Date == true)
                {
                    getReminderDay = Convert.ToInt32(txt_Reminder_Day.Text);
                }
                getEscalation_Due_Date = Convert.ToBoolean(chk_Escalation_Due_Date.Checked);
                if (getEscalation_Due_Date == true)
                {
                    getEscalationDay = Convert.ToInt32(txt_Reminder_Repe_Day.Text);
                }
                getEscalation_Repeate = Convert.ToBoolean(chk_Escalation_Repeate.Checked);
            }
            var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();
            var getprojectLocation = Convert.ToString(DDLProjectLocation.SelectedValue).Trim();
            var id = Convert.ToInt32(hdnTaskID.Value);
            int iIsDraft = Convert.ToInt32(btn.CommandArgument);
            var getStatus = spm.InsertUpdateTaskDetails(id, getRefId, "UPDATETASKDETAILS_TEMP", getCreatedBy, getCreatedBy, getMettingDiscussionTitle, getMettingDiscussionDate, Convert.ToInt32(getMettingDiscussionType), getFor_Info, getOldTaskRef, getTaskSupervisor, getTaskExecutor, getTaskDescripation, getTaskRemark, FinalDueDate, filename, getReminder_Due_Date, getReminderDay, getEscalation_Due_Date, getEscalationDay, getEscalation_Repeate, 0, getprojectLocation, iIsDraft);
            if (Convert.ToDouble(getStatus) != 0)
            {
                hdnTaskRefID.Value = getStatus.ToString();
                ClearTask();
                gv_Documents.DataSource = null;
                gv_Documents.DataBind();
            }
            else
            {
                Label3.Text = "Something went wrong. please try again";
                return;
            }
        }
        else if (btn.Text == "Cancel")
        {
            ClearTask();
            gv_Documents.DataSource = null;
            gv_Documents.DataBind();
        }
        //}
        //catch (Exception ex)
        //{
        //    Label1.Text = "Something went wrong. please try again";
        //    return;
        //}
    }
    //Click on Edit Task on grid
    protected void lnk_TLI_Edit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            var getSelectedValues = ddl_Task_Executor.SelectedValue.ToString();
            var getSelectedValues1 = ddl_Task_Supervisor.SelectedValue.ToString();
            var getSelectedValues2 = ddl_Task_Supervisor.SelectedValue.ToString();

            ddl_Task_Executor.Items.FindByValue(getSelectedValues).Selected = false;
            ddl_Task_Supervisor.Items.FindByValue(getSelectedValues1).Selected = false;
            DDLProjectLocation.Items.FindByValue(getSelectedValues2).Selected = false;

            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(dv_TaskList.DataKeys[row.RowIndex].Values[0]).Trim();
            if (fId != "0")
            {
                var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();
                var TaskRefID = Convert.ToInt32(hdnTaskRefID.Value);
                var getdsDetails = spm.getTaskDetailsById(getCreatedBy, Convert.ToDouble(TaskRefID), Convert.ToDouble(fId), "getTaskDetailsById_Temp");
                if (getdsDetails.Tables.Count > 0)
                {
                    hdnTaskID.Value = fId;
                    var getds = getdsDetails.Tables[0];
                    var getForOnlyInfo = Convert.ToBoolean(getds.Rows[0]["For_Information_Only"]);
                    if (getForOnlyInfo == false)
                    {
                        ddl_Task_Supervisor.Enabled = true;
                        ddl_Task_Executor.Enabled = true;
                        txt_Due_Date.Enabled = true;
                        uploadfile.Enabled = true;
                        chk_Reminder_Due_Date.Enabled = true;
                        txt_Reminder_Day.Enabled = true;

                        chk_Escalation_Due_Date.Enabled = true;
                        chk_Escalation_Repeate.Enabled = true;
                        txt_Reminder_Repe_Day.Enabled = true;

                        var getSupervisor = Convert.ToString(getds.Rows[0]["Task_Supervisor"]);
                        var getExecutor = Convert.ToString(getds.Rows[0]["Task_Executer"]);
                        var getCompCode = Convert.ToString(getds.Rows[0]["comp_code"]);
                        ddl_Task_Executor.Items.FindByValue(getExecutor).Selected = true;
                        ddl_Task_Supervisor.Items.FindByValue(getSupervisor).Selected = true;
                        DDLProjectLocation.Items.FindByValue(getCompCode).Selected = true;

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
                        txt_Due_Date.Text = Convert.ToString(getds.Rows[0]["DUEDATE"]);
                        var getDocumentTable = getdsDetails.Tables[2];
                        gv_Documents.DataSource = null;
                        gv_Documents.DataBind();
                        // var getDocumentTable = getdsTables.Tables[2];
                        if (getDocumentTable.Rows.Count > 0)
                        {
                            gv_Documents.DataSource = getDocumentTable;
                            gv_Documents.DataBind();

                        }
                    }
                    else
                    {
                        //ClearTask();
                        ddl_Task_Supervisor.Enabled = false;
                        ddl_Task_Executor.Enabled = false;
                        txt_Due_Date.Enabled = false;
                        //  txt_TaskDescripation.Enabled = false;
                        // txt_TaskRemark.Enabled = false;
                        uploadfile.Enabled = false;

                        chk_Reminder_Due_Date.Enabled = false;
                        chk_Reminder_Due_Date.Checked = false;

                        txt_Reminder_Day.Text = "";
                        txt_Reminder_Day.Enabled = false;

                        chk_Escalation_Due_Date.Enabled = false;
                        chk_Escalation_Due_Date.Checked = false;

                        chk_Escalation_Repeate.Enabled = false;

                        txt_Reminder_Repe_Day.Text = "";
                        txt_Reminder_Repe_Day.Enabled = false;
                    }
                    txt_TaskDescripation.Text = Convert.ToString(getds.Rows[0]["Task_Description"]);
                    txt_TaskRemark.Text = Convert.ToString(getds.Rows[0]["Task_Remarks"]);

                    lnk_Task_Update.Visible = true;
                    lnk_Task_Cancel.Visible = true;
                    lnk_Task_Create.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnk_Final_Submit_Click(object sender, EventArgs e)
    {
        //try
        //{
        if (Convert.ToString(txt_Metting_Title.Text).Trim() == "")
        {
            Label2.Text = "Please enter Meeting/Discussion Title.";
            return;
        }
        if (Convert.ToString(txt_Metting_Dis.Text).Trim() == "")
        {
            Label2.Text = "Please enter Meeting/Discussion Date.";
            return;
        }
        if (Convert.ToString(ddl_Meeting_Type.SelectedValue).Trim() == "0" || Convert.ToString(ddl_Meeting_Type.SelectedValue).Trim() == "")
        {
            Label2.Text = "Please enter Meeting/Discussion Type.";
            return;
        }
        var IsOrg = false;
        var getAtt = false;
        foreach (GridViewRow row in dg_ATT_Details.Rows)
        {
            //Finding Dropdown control  
            CheckBox ddl1 = row.FindControl("chk_Ref_Select") as CheckBox;
            if (ddl1.Checked == true)
            {
                IsOrg = true;
            }
            if (dg_ATT_Details.Rows.Count >= 2)
            {
                getAtt = true;
            }
            else if (ddl_Meeting_Type.SelectedItem.Value == "4")
            {
                getAtt = true;
            }
        }
        if (IsOrg == false)
        {
            Label1.Text = "Please add at least one Organizer.";
            return;
        }
        if (getAtt == false)
        {
            Label1.Text = "Please add attendees.";
            return;
        }

        var getTempMettingOldref = ddl_Meeting_Id.SelectedValue.ToString();
        if (getTempMettingOldref != "0")
        {
            var IsTaskRef = false;
            //Click on Add reference
            foreach (GridViewRow row in dg_OldTaskDetails.Rows)
            {
                //Finding Dropdown control  
                CheckBox ddl1 = row.FindControl("chk_Ref_Select") as CheckBox;
                if (ddl1.Checked == true)
                {
                    IsTaskRef = true;
                }
            }
            if (IsTaskRef == false)
            {
                Label2.Text = "Please select at least one task reference.";
                return;
            }
        }
        var getTaskCount = dv_TaskList.Rows.Count;
        if (getTaskCount < 1)
        {
            Label2.Text = "Please add at least one task ";
            return;
        }
        var getRefId = Convert.ToDouble(hdnTaskRefID.Value);
        var getOldTaskRef = Convert.ToDouble(0);
        var getMettingDiscussionTitle = Convert.ToString(txt_Metting_Title.Text).ToString();
        var getMettingDiscussionDate = "";
        var getTempMeetingDate = Convert.ToString(txt_Metting_Dis.Text).ToString();
        var splitmeetingDate = getTempMeetingDate.Split('/');
        getMettingDiscussionDate = splitmeetingDate[2] + "-" + splitmeetingDate[1] + "-" + splitmeetingDate[0];
        var getMettingDiscussionType = Convert.ToString(ddl_Meeting_Type.SelectedValue).ToString();
        var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();

        LinkButton btn = (LinkButton)sender;
        int storeValue = Convert.ToInt32(btn.CommandArgument);

        var getStatus = spm.InsertUpdateTaskDetails(0, 0, "UpdateTaskRef", getCreatedBy, getCreatedBy, getMettingDiscussionTitle, getMettingDiscussionDate, Convert.ToInt32(getMettingDiscussionType), false, getOldTaskRef, "", "", "", "", "", "", false, 0, false, 0, false, 0, "", storeValue);
        hdnTaskRefID.Value = getStatus.ToString();
        var getList = spm.getTaskMonitoringDDL(getCreatedBy, 0, "GetTempTaskList");
        if (getList != null)
        {
            if (getList.Tables.Count > 0)
            {
                var getTable = getList.Tables[0];
                if (getTable != null)
                {
                    if (getTable.Rows.Count > 0)
                    {
                        foreach (DataRow item in getTable.Rows)
                        {
                            var getTaskRefId = Convert.ToDouble(getStatus);
                            getRefId = getTaskRefId;
                            var getTaskSupervisor = Convert.ToString(item["Task_Supervisor"]).ToString();
                            var getTaskExecutor = Convert.ToString(item["Task_Executer"]).ToString();
                            var getFor_Info = Convert.ToBoolean(item["For_Information_Only"]);
                            var getTaskDescripation = Convert.ToString(item["Task_Description"]).ToString();
                            var getTaskRemark = Convert.ToString(item["Task_Remarks"]).ToString();
                            var getDueDate = "";
                            var getReminder_Due_Date = false;
                            var getReminderDay = 0;
                            var getEscalation_Due_Date = false;
                            var getEscalationDay = 0;
                            var getEscalation_Repeate = false;
                            if (getFor_Info == false)
                            {
                                getDueDate = Convert.ToString(item["DueDate"]).ToString();
                                getReminder_Due_Date = Convert.ToBoolean(item["Remainder_Before_Due_Date"]);
                                getReminderDay = Convert.ToInt32(item["Reminder_Before_Days"]);
                                getEscalation_Due_Date = Convert.ToBoolean(item["Escalation_Mail_After_Due_Date"]);
                                getEscalationDay = Convert.ToInt32(item["Escalation_Mail_Frequency_Days"]);
                                getEscalation_Repeate = Convert.ToBoolean(item["Repeated_Escalation_Mail"]);
                            }
                            var id = Convert.ToInt32(item["Id"]);
                            var tempTaskRefId = Convert.ToInt32(item["Task_Ref_id"]);
                            var getProjectLocationCode = Convert.ToString(item["comp_code"]).ToString();
                            // 
                            var getStatus1 = spm.InsertUpdateTaskDetails(id, getTaskRefId, "AddNewTaskDetails", getCreatedBy, getCreatedBy, getMettingDiscussionTitle, getMettingDiscussionDate, Convert.ToInt32(getMettingDiscussionType), getFor_Info, getOldTaskRef, getTaskSupervisor, getTaskExecutor, getTaskDescripation, getTaskRemark, getDueDate, "", getReminder_Due_Date, getReminderDay, getEscalation_Due_Date, getEscalationDay, getEscalation_Repeate, tempTaskRefId, getProjectLocationCode, storeValue);

                        }
                    }
                }
            }
        }

        if (storeValue == 0 && getStatus != null)
        {
            // Sending Email
            var getTaskRef = Convert.ToDouble(hdnTaskRefID.Value);
            var getDs = spm.getTaskMonitoringReport("getTaskEmaildetails", "", getTaskRef, 0);
            if (getDs != null)
            {
                if (getDs.Tables.Count > 0)
                {
                    var getTaskRefDetails = getDs.Tables[0];

                    if (getTaskRefDetails.Rows.Count > 0)
                    {
                        var getTaskDetails = getDs.Tables[1];
                        var getAllAttendee = getDs.Tables[2];
                        var getAllExecuter = getDs.Tables[3];
                        var getAllTaskIntimation = getDs.Tables[4];
                        var appendAttendee = "";
                        var appendIntimation = "";
                        foreach (DataRow item in getAllAttendee.Rows)
                        {
                            if (appendAttendee == "")
                            {
                                appendAttendee = Convert.ToString(item["Emp_Emailaddress"]);
                            }
                            else
                            {
                                appendAttendee = appendAttendee + ";" + Convert.ToString(item["Emp_Emailaddress"]);
                            }
                        }

                        appendIntimation = Convert.ToString(getAllTaskIntimation.Rows[0]["CCEmailAddress"]);

                        var Task_Reference_ID = Convert.ToString(getTaskRefDetails.Rows[0]["Task_Reference_ID"]);
                        var Meeting_Discussion_Title = Convert.ToString(getTaskRefDetails.Rows[0]["Meeting_Discussion_Title"]);
                        var Meeting_Discussion_Date = Convert.ToString(getTaskRefDetails.Rows[0]["Meeting_Discussion_Date"]);
                        var MeetingType = Convert.ToString(getTaskRefDetails.Rows[0]["MeetingType"]);
                        spm.Task_Monitoring_All_Attendee(getTaskDetails, appendAttendee, Meeting_Discussion_Title, Meeting_Discussion_Date, MeetingType, Task_Reference_ID, appendIntimation);
                        var link = "http://localhost/hrms/login.aspx?ReturnUrl=procs/InboxExecuter.aspx";
                        if (ddl_Meeting_Type.SelectedItem.Value != "4")
                        {
                            foreach (DataRow item in getAllExecuter.Rows)
                            {
                                var getExecuter = Convert.ToString(item["Task_Executer"]);
                                var getTaskEmailDetails = spm.getTaskMonitoringReport("getTaskDetailsByExecuter", getExecuter, getTaskRef, 0);
                                if (getTaskEmailDetails != null)
                                {
                                    if (getTaskEmailDetails.Tables.Count > 0)
                                    {
                                        var getTaskDetails1 = getTaskEmailDetails.Tables[0];
                                        if (getTaskDetails1.Rows.Count > 0)
                                        {
                                            var ExecuterEmail = Convert.ToString(getTaskDetails1.Rows[0]["ExecuterEmail"]);
                                            var SupervisorEmail = Convert.ToString(getTaskDetails1.Rows[0]["SupervisorEmail"]);
                                            SupervisorEmail = "";
                                            spm.Task_Monitoring_Executer(getTaskDetails1, ExecuterEmail, Meeting_Discussion_Title, Meeting_Discussion_Date, MeetingType, Task_Reference_ID, link, SupervisorEmail);
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }

        }
        //Add Old Ref 
        var getMettingOldref = ddl_Meeting_Id.SelectedValue.ToString();
        if (getMettingOldref != "0")
        {
            //Click on Add reference
            foreach (GridViewRow row in dg_OldTaskDetails.Rows)
            {
                //Finding Dropdown control  
                CheckBox ddl1 = row.FindControl("chk_Ref_Select") as CheckBox;
                if (ddl1.Checked == true)
                {
                    var empCode = Convert.ToString(Session["Empcode"]);
                    //Add refanace 
                    getOldTaskRef = Convert.ToDouble(0);
                    getMettingOldref = ddl_Meeting_Id.SelectedValue.ToString();

                    if (getMettingOldref != "")
                    {
                        getOldTaskRef = Convert.ToDouble(getMettingOldref);
                        hdnOldTaskRefID.Value = getOldTaskRef.ToString();
                    }
                    else
                    {
                        hdnOldTaskRefID.Value = getOldTaskRef.ToString();
                    }
                    Label lblTask_id = row.FindControl("lblTask_id") as Label;
                    var getlblTask_id = Convert.ToInt32(lblTask_id.Text.Trim());

                    var getStatusTask = spm.InsertUpdateTaskDetails(getlblTask_id, getRefId, "AddTaskRef", empCode, empCode, "", "", 0, false, getOldTaskRef, "", "", "", "", "", "", false, 0, false, 0, false, 0, "", storeValue);

                }
            }


        }
        DeleteTempAttendee();
        //End Email
        Response.Redirect("~/procs/TaskMonitoring.aspx");
        //}
        //catch (Exception ex)
        //{

        //}
    }

    protected void chk_for_Info_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            var getVal = chk_for_Info.Checked;
            if (getVal == true)
            {
                //ClearTask();
                ddl_Task_Supervisor.Enabled = false;
                ddl_Task_Executor.Enabled = false;
                txt_Due_Date.Enabled = false;
                //  txt_TaskDescripation.Enabled = false;
                // txt_TaskRemark.Enabled = false;
                uploadfile.Enabled = false;

                chk_Reminder_Due_Date.Enabled = false;
                chk_Reminder_Due_Date.Checked = false;

                txt_Reminder_Day.Text = "";
                txt_Reminder_Day.Enabled = false;

                chk_Escalation_Due_Date.Enabled = false;
                chk_Escalation_Due_Date.Checked = false;

                chk_Escalation_Repeate.Enabled = false;

                txt_Reminder_Repe_Day.Text = "";
                txt_Reminder_Repe_Day.Enabled = false;

            }
            else
            {
                // ClearTask();
                ddl_Task_Supervisor.Enabled = true;
                ddl_Task_Executor.Enabled = true;
                txt_Due_Date.Enabled = true;
                // txt_TaskDescripation.Enabled = true;
                //txt_TaskRemark.Enabled = true;
                uploadfile.Enabled = true;
                chk_Reminder_Due_Date.Enabled = true;
                chk_Reminder_Due_Date.Checked = true;

                txt_Reminder_Day.Text = "1";
                txt_Reminder_Day.Enabled = true;

                chk_Escalation_Due_Date.Enabled = true;
                chk_Escalation_Due_Date.Checked = true;

                chk_Escalation_Repeate.Enabled = true;

                txt_Reminder_Repe_Day.Text = "1";
                txt_Reminder_Repe_Day.Enabled = true;
            }
        }
        catch (Exception)
        {

        }
    }

    protected void lnk_File_Delete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(gv_Documents.DataKeys[row.RowIndex].Values[0]).Trim();
            var FilePath = Convert.ToString(gv_Documents.DataKeys[row.RowIndex].Values[1]).Trim();
            if (fId != null)
            {
                var id = int.Parse(fId);
                var result = spm.DeleteAttendeeDetails(id, "DeleteDocumentDetails_Temp");
                if (result == true)
                {

                    string path = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["TaskMonitoringPath"]).Trim()), FilePath);
                    FileInfo file = new FileInfo(path);
                    if (file.Exists)//check file exsit or not  
                    {
                        file.Delete();
                    }
                    //ClearDocumentDetails();
                    var getDocumentTable = spm.getTempAttendee("GetDocumentDetails_Temp", "", Convert.ToDouble(hdnTaskID.Value));
                    gv_Documents.DataSource = null;
                    gv_Documents.DataBind();
                    // var getDocumentTable = getdsTables.Tables[2];
                    if (getDocumentTable.Rows.Count > 0)
                    {
                        gv_Documents.DataSource = getDocumentTable;
                        gv_Documents.DataBind();

                    }
                }
                else
                {
                    Label2.Text = "Something went wrong , Please try again.";
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Label2.Text = ex.Message.ToString();
            return;
        }
    }
    //Save Orgnaz..
    protected void btn_Org_Save_Click(object sender, EventArgs e)
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
            var IsSelect = false;
            var count = 0;
            var getOrganizer = true; //chk_Ref_Select.Checked;

            if (ddl_Organizer.SelectedValue == "" || ddl_Organizer.SelectedValue == "0")
            {
                Label1.Text = "Please select Organizer";
                return;
            }

            var getAttendee = "";
            if (getOrganizer == true)
            {
                var getOrginserStatus = spm.getTempAttendee("CheckIsOrginser", empCode, getVal);
                if (getOrginserStatus != null)
                {
                    if (getOrginserStatus.Rows.Count > 0)
                    {
                        var getMsg = Convert.ToString(getOrginserStatus.Rows[0]["Message"]);
                        if (getMsg == "Exists")
                        {
                            Label1.Text = "Please select one organizer ";
                            return;
                        }
                    }
                }
            }
            var qtype = "";
            if (getVal == 0)
            {
                qtype = "AddTempAttendee";
            }
            else
            {
                qtype = "AddTempAttendee";
            }
            var getStatus = false;

            if (ddl_Organizer.SelectedIndex > 0)
            {
                getAttendee = ddl_Organizer.SelectedValue;
                getStatus = spm.InsertUpdateTempAttendee(0, getVal, qtype, getAttendee, empCode, getOrganizer);
            }
            if (getStatus == false)
            {
                Label1.Text = "Something went wrong";
                return;
            }
            else
            {
                ClearAttendee();
                if (getVal == 0)
                {
                    var getAttendeeList = spm.getTempAttendee("GetTempAttendee", empCode, 0);
                    BindGridAttendee(getAttendeeList);
                }
                else
                {
                    var getAttendeeList = spm.getTempAttendee("GetTempAttendee", empCode, getVal);
                    BindGridAttendee(getAttendeeList);
                }
                SelectOrganizer_Attendee();
            }


        }
        catch (Exception ex)
        {

        }

    }
    //Update Orgnaz...
    protected void btn_Org_Update_Click(object sender, EventArgs e)
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
            var IsSelect = false;
            var count = 0;
            var getOrganizer = true;

            if (ddl_Organizer.SelectedValue == "" || ddl_Organizer.SelectedValue == "0")
            {
                Label1.Text = "Please select Organizer";
                return;
            }
            if (count >= 2 && getOrganizer == true)
            {
                Label1.Text = "Please select only one organizer";
                return;
            }

            var getAttendee = "";
            if (getOrganizer == true)
            {
                var getOrginserStatus = spm.getTempAttendee("CheckIsOrginserUpdate", empCode, getId);
                if (getOrginserStatus != null)
                {
                    if (getOrginserStatus.Rows.Count > 0)
                    {
                        var getMsg = Convert.ToString(getOrginserStatus.Rows[0]["Message"]);
                        if (getMsg == "Exists")
                        {
                            Label1.Text = "Please select only one organizer ";
                            return;
                        }
                    }
                }
            }
            var qtype = "";
            if (getVal == 0)
            {
                qtype = "UpdateTempAttendee";
            }
            else
            {
                qtype = "UpdateTempAttendee";
            }
            var getStatus = false;
            if (ddl_Organizer.SelectedIndex > 0)
            {
                getAttendee = ddl_Organizer.SelectedValue;
                getStatus = spm.InsertUpdateTempAttendee(getId, getVal, qtype, getAttendee, empCode, getOrganizer);
            }

            if (getStatus == false)
            {
                Label1.Text = "Something went wrong";
                return;
            }
            else
            {
                ClearAttendee();
                if (getVal == 0)
                {
                    var getAttendeeList = spm.getTempAttendee("GetTempAttendee", empCode, 0);
                    BindGridAttendee(getAttendeeList);
                }
                else
                {
                    var getAttendeeList = spm.getTempAttendee("GetTempAttendee", empCode, getVal);
                    BindGridAttendee(getAttendeeList);
                }
                SelectOrganizer_Attendee();
            }

        }
        catch (Exception ex)
        {

        }
    }
    private void SelectOrganizer_Attendee()
    {
        try
        {
            var getAtt = false;
            var getOrg = false;
            Label3.Text = "";
            var getVal = Convert.ToString(ddl_Meeting_Type.SelectedValue);
            if (getVal == "4")
            {
                if (dg_ATT_Details.Rows.Count > 0)
                {
                    foreach (GridViewRow row in dg_ATT_Details.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            TextBox text = (row.Cells[0].FindControl("txtAttendee_Name") as TextBox);
                            if (text.Text != "")
                            {
                                var getSelectedValue = Convert.ToString(text.Text);
                                var ddlTaskExecutor = ddl_Task_Executor.SelectedValue.ToString();
                                ddl_Task_Executor.Items.FindByValue(ddlTaskExecutor).Selected = false;

                                ddl_Task_Executor.Items.FindByText(getSelectedValue).Selected = true;
                                ddl_Task_Executor.Enabled = false;

                                var ddlTaskSupervisor = ddl_Task_Supervisor.SelectedValue.ToString();
                                ddl_Task_Supervisor.Items.FindByValue(ddlTaskSupervisor).Selected = false;

                                ddl_Task_Supervisor.Items.FindByText(getSelectedValue).Selected = true;
                                ddl_Task_Supervisor.Enabled = false;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void tsktemplate_btnSave_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        int objparam = Convert.ToInt32(btn.CommandArgument);

        DataTable dtTaskTemp = new DataTable();
        string Fulltext;
        if (uplTaskTemplate.HasFile)
        {
            string filename = uplTaskTemplate.FileName;
            string POWOFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["TaskMonitoringUploadTemplatePath"]).Trim() + "/");
            bool folderExists = Directory.Exists(POWOFilePath);
            if (!folderExists)
            {
                Directory.CreateDirectory(POWOFilePath);
            }
            String InputFile = System.IO.Path.GetExtension(uplTaskTemplate.FileName);

            string str = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
            filename = "TaskUpload" + str + InputFile;
            uplTaskTemplate.SaveAs(Path.Combine(POWOFilePath, filename));
            string powoUplaodedFile = POWOFilePath + filename;
            string read = System.IO.Path.GetFullPath(powoUplaodedFile);
            bool hasEmptyValues = false;
            int columnIndexToCheck = 0;

            //Open the Excel file using ClosedXML.
            using (XLWorkbook workBook = new XLWorkbook(read))
            {
                //Read the first Sheet from Excel file.
                IXLWorksheet workSheet = workBook.Worksheet(1);



                //Loop through the Worksheet rows.
                bool firstRow = true;
                Dictionary<string, int> rowSet = new Dictionary<string, int>();

                if (workSheet.RowsUsed().Count() > 1)
                {
                    int rowIndex = 0;
                    foreach (IXLRow row in workSheet.RowsUsed())
                    {
                        rowIndex++;
                        lbl_Upload_Eror.Text = "";
                        if (firstRow)
                        {
                            foreach (IXLCell cell in row.CellsUsed())
                            {
                                dtTaskTemp.Columns.Add(cell.Address.ToString());
                            }
                            firstRow = false;
                        }
                        else
                        {
                            string rowString = string.Join(",", row.Cells(1, 8).Select(cell => cell.Value.ToString()));

                            if (rowSet.ContainsKey(rowString))
                            {
                                lbl_Upload_Eror.Text = "Duplicate row found in the Excel sheet at record: " + (rowIndex + 1) + ". Original record: " + (rowSet[rowString]) + ".";
                                return;
                            }
                            rowSet.Add(rowString, rowIndex);

                            //Add rows to DataTable.
                            dtTaskTemp.Rows.Add();
                            int i = 0;
                            foreach (IXLCell cell in row.CellsUsed())
                            {
                                if (cell.Address.ToString().Contains("B") || cell.Address.ToString().Contains("F"))
                                {
                                    string dateString = cell.GetValue<string>();
                                    DateTime dateValue;

                                    if (!DateTime.TryParseExact(dateString, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                                    {
                                        if (cell.Address.ToString().Contains("B"))
                                        {
                                            lbl_Upload_Eror.Text = "Invalid Meeting/Dicussion date format at record: " + rowIndex + ". Ensure the date is in dd.MM.yyyy format.";
                                        }
                                        else if (cell.Address.ToString().Contains("F"))
                                        {
                                            lbl_Upload_Eror.Text = "Invalid Task due date format at record: " + rowIndex + ". Ensure the date is in dd.MM.yyyy format.";
                                        }
                                        return;
                                    }

                                    if (cell.Address.ToString().Contains("B") && dateValue > DateTime.Today)
                                    {
                                        lbl_Upload_Eror.Text = "Meeting/Dicussion Date cannot be future date at record: " + rowIndex + ".";
                                        return;
                                    }

                                    if (cell.Address.ToString().Contains("F") && dateValue < DateTime.Today)
                                    {
                                        lbl_Upload_Eror.Text = "Task due date cannot be less than today date  at record: " + rowIndex + ".";
                                        return;
                                    }
                                }

                                if (string.IsNullOrEmpty(cell.Value.ToString()))
                                {
                                    lbl_Upload_Eror.Text = "One or more columns contain empty values at record: " + rowIndex + ".";
                                    return;
                                }
                                else
                                {
                                    dtTaskTemp.Rows[dtTaskTemp.Rows.Count - 1][i] = cell.Value.ToString();
                                    i++;
                                }

                                if (row.Cell(8).Value.ToString().Length > 200 )
                                {
                                    lbl_Upload_Eror.Text = "Task description contains more than 200 characters at record: " + rowIndex + ".";
                                    return;
                                }

                                if (row.Cell(9).Value.ToString().Length > 200)
                                {
                                    lbl_Upload_Eror.Text = "Task remark contains more than 200 characters at record: " + rowIndex + ".";
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    lbl_Upload_Eror.Text = "The entire sheet is empty; no rows are available.";
                    return;
                }
            }

            if (dtTaskTemp.Rows.Count > 0)
            {
                int taskRefId = 0;
                int rowIndex = 1;

                string is_Valid = ValidateAll("CheckValidation", dtTaskTemp);
                string[] splitdata = is_Valid.Split(',');
                if (splitdata[0] == "NoRecord")
                {
                    foreach (DataRow row in dtTaskTemp.Rows)
                    {
                        rowIndex++;
                        taskRefId = SaveTaskAsDraft(objparam, row);
                    }
                }

                lbl_Upload_Eror.Text = "";
                if (taskRefId == 0 && splitdata[0] == "IsSubmitted")
                {
                    lbl_Upload_Eror.Text = "The task at record: " + splitdata[1] + " has already been submitted. Adding a sub-task is not allowed.";
                    return;
                }
                else if (taskRefId == 0 || splitdata[0] == "Duplicate")
                {
                    lbl_Upload_Eror.Text = "Duplicate row found in the Excel sheet at record: " + splitdata[1] + ".";
                    return;
                }
                else
                {
                    Response.Redirect("~/procs/Update_Task.aspx?id=" + taskRefId);
                }
            }
        }
        else
        {
            lbl_Upload_Eror.Text = "Please upload the template.";
        }
    }

    protected int SaveTaskAsDraft(int inputParam, DataRow dr)
    {
        #region Save Task Details Data into Main Table as Draft

        var filename = "";
        var getRefId = Convert.ToDouble(hdnTaskRefID.Value);
        var getOldTaskRef = Convert.ToDouble(hdnOldTaskRefID.Value);
        var getMettingDiscussionTitle = dr["A1"].ToString();
        var getMettingDiscussionDate = DateTime.ParseExact(dr["B1"].ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

        var GetData = GetIdsBasedOnColumns("GetIdsBasedOnColumns", dr["C1"].ToString().Trim(), dr["D1"].ToString().Trim(), dr["E1"].ToString().Trim(), dr["G1"].ToString().Trim());
        var getTaskExecutor = "";
        var getMeetingDiscussionType = "";
        var getTaskSupervisor = "";
        var ProjectLocation = "";
        if (GetData != null)
        {
            if (GetData.Tables.Count > 0)
            {
                getMeetingDiscussionType = GetData.Tables[0] != null && GetData.Tables[0].Rows.Count > 0 && GetData.Tables[0].Columns.Contains("Id") ? GetData.Tables[0].Rows[0]["Id"].ToString() : "";
                getTaskSupervisor = GetData.Tables[1] != null && GetData.Tables[1].Rows.Count > 0 && GetData.Tables[1].Columns.Contains("Emp_Code") ? GetData.Tables[1].Rows[0]["Emp_Code"].ToString() : "";
                getTaskExecutor = GetData.Tables[2] != null && GetData.Tables[2].Rows.Count > 0 && GetData.Tables[2].Columns.Contains("Emp_Code") ? GetData.Tables[2].Rows[0]["Emp_Code"].ToString() : "";
                ProjectLocation = GetData.Tables[3] != null && GetData.Tables[3].Rows.Count > 0 && GetData.Tables[3].Columns.Contains("comp_code") ? GetData.Tables[3].Rows[0]["comp_code"].ToString() : "";
            }
        }

        var getFor_Info = false;
        var getTaskDescripation = dr["H1"].ToString();
        var getTaskRemark = dr["I1"].ToString();
        var getDueDate = DateTime.ParseExact(dr["F1"].ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
        var FinalDueDate = "";
        var getReminder_Due_Date = false;
        var getReminderDay = 0;
        var getEscalationDay = 0;
        var getEscalation_Due_Date = false;
        var getEscalation_Repeate = false;
        if (chk_for_Info.Checked == false)
        {
            FinalDueDate = getDueDate;
            if (uploadfile.HasFile)
            {
                filename = uploadfile.FileName;
            }
            if (Convert.ToString(filename).Trim() != "")
            {
                DateTime loadedDate = DateTime.Now;
                var strfromDate = loadedDate.ToString("ddMMyyyyHHmmss");
                filename = uploadfile.FileName;
                var strfileName = "";
                strfileName = "TASK_" + getTaskExecutor + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
                filename = strfileName;
                uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["TaskMonitoringPath"]).Trim()), strfileName));
            }

            getReminder_Due_Date = Convert.ToBoolean(chk_Reminder_Due_Date.Checked);

            if (getReminder_Due_Date == true)
            {
                getReminderDay = Convert.ToInt32(txt_Reminder_Day.Text);
            }
            getEscalation_Due_Date = Convert.ToBoolean(chk_Escalation_Due_Date.Checked);
            if (getEscalation_Due_Date == true)
            {
                getEscalationDay = Convert.ToInt32(txt_Reminder_Repe_Day.Text);
            }
            getEscalation_Repeate = Convert.ToBoolean(chk_Escalation_Repeate.Checked);
        }

        var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();

        //Insert Uploaded data into Reference Table as Draft New block Added in SP by Ajinkya
        var getStatus = spm.InsertUpdateTaskDetails(0, 0, "InsertUploadedTaskRef", getCreatedBy, getCreatedBy, getMettingDiscussionTitle, getMettingDiscussionDate, Convert.ToInt32(getMeetingDiscussionType), false, getOldTaskRef, getTaskSupervisor, getTaskExecutor, "", "", getDueDate, "", false, 0, false, 0, false, 0, ProjectLocation, inputParam);
        if (getStatus.ToString() == "0")
        {
            return 0;
        }

        hdnTaskRefID.Value = getStatus.ToString();

        var getTaskRefId = Convert.ToDouble(getStatus);
        getRefId = getTaskRefId;

        var id = Convert.ToInt32(0);
        var tempTaskRefId = Convert.ToInt32(hdnTaskRefID.Value);
        var getStatus2 = spm.InsertUpdateTaskDetails(id, getTaskRefId, "AddNewTaskDetails", getCreatedBy, getCreatedBy, getMettingDiscussionTitle, getMettingDiscussionDate, Convert.ToInt32(getMeetingDiscussionType), getFor_Info, getOldTaskRef, getTaskSupervisor, getTaskExecutor, getTaskDescripation, getTaskRemark, getDueDate, "", getReminder_Due_Date, getReminderDay, getEscalation_Due_Date, getEscalationDay, getEscalation_Repeate, tempTaskRefId, ProjectLocation, inputParam);
        return Convert.ToInt32(getTaskRefId);
        #endregion Save Task Details Data into Main Table as Draft 
    }

    #region Ajinkya 
    //Get id of fields added by excel sheet while creating Task 
    public DataSet GetIdsBasedOnColumns(string qtype, string MeetingDiscussionType, string TaskSupervisor, string TaskExecutor, string ProjectLocation)
    {
        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[5];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = qtype;

        spars[1] = new SqlParameter("@UpldMeetingType", SqlDbType.VarChar);
        spars[1].Value = MeetingDiscussionType;

        spars[2] = new SqlParameter("@UpldTaskSupervisor", SqlDbType.VarChar);
        spars[2].Value = TaskSupervisor;

        spars[3] = new SqlParameter("@UpldTaskExecutor", SqlDbType.VarChar);
        spars[3].Value = TaskExecutor;

        spars[4] = new SqlParameter("@UpldProjectLocation", SqlDbType.VarChar);
        spars[4].Value = ProjectLocation;

        dsProjectsVendors = spm.getDatasetList(spars, "SP_TASK_M_DETAILS");
        return dsProjectsVendors;

    }


    public string ValidateAll(string qtype, DataTable objDT)
    {
        DataTable resultTable = new DataTable();
        string allValid = "NoRecord,0";

        foreach (DataRow dr in objDT.Rows)
        {
            var GetData = GetIdsBasedOnColumns("GetIdsBasedOnColumns", dr["C1"].ToString().Trim(), dr["D1"].ToString().Trim(), dr["E1"].ToString().Trim(), dr["G1"].ToString().Trim());
            var getTaskExecutor = "";
            var getMeetingDiscussionType = "";
            var getTaskSupervisor = "";
            var ProjectLocation = "";
            if (GetData != null)
            {
                if (GetData.Tables.Count > 0)
                {
                    getMeetingDiscussionType = GetData.Tables[0] != null && GetData.Tables[0].Rows.Count > 0 && GetData.Tables[0].Columns.Contains("Id") ? GetData.Tables[0].Rows[0]["Id"].ToString() : "";
                    getTaskSupervisor = GetData.Tables[1] != null && GetData.Tables[1].Rows.Count > 0 && GetData.Tables[1].Columns.Contains("Emp_Code") ? GetData.Tables[1].Rows[0]["Emp_Code"].ToString() : "";
                    getTaskExecutor = GetData.Tables[2] != null && GetData.Tables[2].Rows.Count > 0 && GetData.Tables[2].Columns.Contains("Emp_Code") ? GetData.Tables[2].Rows[0]["Emp_Code"].ToString() : "";
                    ProjectLocation = GetData.Tables[3] != null && GetData.Tables[3].Rows.Count > 0 && GetData.Tables[3].Columns.Contains("comp_code") ? GetData.Tables[3].Rows[0]["comp_code"].ToString() : "";
                }
            }

            SqlParameter[] spars = new SqlParameter[9];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = qtype;

            spars[1] = new SqlParameter("@mettingTitle", SqlDbType.VarChar);
            spars[1].Value = dr["A1"].ToString();

            spars[2] = new SqlParameter("@mettingDate", SqlDbType.VarChar);
            spars[2].Value = DateTime.ParseExact(dr["B1"].ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

            spars[3] = new SqlParameter("@mettingTypeId", SqlDbType.VarChar);
            spars[3].Value = getMeetingDiscussionType;

            spars[4] = new SqlParameter("@TaskSupervisor", SqlDbType.VarChar);
            spars[4].Value = getTaskSupervisor;

            spars[5] = new SqlParameter("@TaskExecutor", SqlDbType.VarChar);
            spars[5].Value = getTaskExecutor;

            spars[6] = new SqlParameter("@DueDate", SqlDbType.VarChar);
            spars[6].Value = DateTime.ParseExact(dr["F1"].ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

            spars[7] = new SqlParameter("@comp_code", SqlDbType.VarChar);
            spars[7].Value = ProjectLocation;

            spars[8] = new SqlParameter("@TaskDescripation", SqlDbType.VarChar);
            spars[8].Value = dr["H1"].ToString();

            DataTable dsProjectsVendors = spm.getData_FromCode(spars, "SP_TASK_M_DETAILS");
            if (dsProjectsVendors.Rows.Count == 0 || dsProjectsVendors.Rows[0][0].ToString() != "1")
            {
                if (dsProjectsVendors.Rows[0][0].ToString() == "0")
                {
                    allValid = "IsSubmitted," + (objDT.Rows.IndexOf(dr) + 2);
                }
                else if (dsProjectsVendors.Rows[0][0].ToString() == "2")
                {
                    allValid = "Duplicate," + (objDT.Rows.IndexOf(dr) + 2);
                }
                break;
            }

            resultTable = dsProjectsVendors;
        }
        return allValid;
    }
    #endregion


    protected void DownloadExcel_Click(object sender, EventArgs e)
    {
        string filePath = Server.MapPath("~/TaskMonitoring_UploadTemplate/TaskUpload_Template.xlsx"); // Replace with actual path
        if (!System.IO.File.Exists(filePath))
        {
            return;
        }

        byte[] fileContent = System.IO.File.ReadAllBytes(filePath);

        //Fetch Supervisor and Executor Master Data
        var empCode = Convert.ToString(Session["Empcode"]);
        var getVal = Convert.ToDouble(hdnTaskRefID.Value);
        var getResult = spm.getTaskMonitoringDDL(empCode, getVal, "GetAllDDL_TEMP_Create");
        var getMaster = getResult.Tables[6];
        DataRow[] rowsToRemove = getMaster.Select("emp_code = '99999999'");

        foreach (DataRow row in rowsToRemove)
        {
            getMaster.Rows.Remove(row);
        }


        //Fetch Location Master Data
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetDropDownprojectLocation";
        DataSet DS = spm.getDatasetList(spars, "SP_TASK_M_DETAILS");
        var getLocationMaster = DS.Tables[0];
        int mainSheetColumnIndex = 0;
        using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            using (XLWorkbook workBook = new XLWorkbook(stream))
            {
                #region Code to Add Supervisor Master Data to an Excel Sheet.
                var masterWorksheet = workBook.Worksheets.Add("Supervisor Master Data");
                for (int i = 0; i < getMaster.Columns.Count; i++)
                {
                    masterWorksheet.Cell(1, i + 1).Value = getMaster.Columns[i].ColumnName;
                }

                for (int i = 0; i < getMaster.Rows.Count; i++)
                {
                    for (int j = 0; j < getMaster.Columns.Count; j++)
                    {
                        masterWorksheet.Cell(i + 2, j + 1).Value = getMaster.Rows[i][j];
                    }
                }
                // Auto-adjust column widths
                masterWorksheet.Columns().AdjustToContents();

                var mainWorksheet = workBook.Worksheets.FirstOrDefault();

                // Apply data validation to the desired column in the main worksheet
                mainSheetColumnIndex = 4; // Assuming you want to bind the dropdown to the fourth column (D)
                 var dropdownRange = mainWorksheet.Range("D2:D100");
                var dataValidation = dropdownRange.SetDataValidation();
                dataValidation.List(masterWorksheet.Range("B2:B" + (getMaster.Rows.Count + 1).ToString()));
                dataValidation.IgnoreBlanks = true;
                dataValidation.InCellDropdown = true;
                #endregion

                #region Code to Add Executor Master Data to an Excel Sheet.
                var executormasterWorksheet = workBook.Worksheets.Add("Executor Master Data");
                for (int i = 0; i < getMaster.Columns.Count; i++)
                {
                    executormasterWorksheet.Cell(1, i + 1).Value = getMaster.Columns[i].ColumnName;
                }

                for (int i = 0; i < getMaster.Rows.Count; i++)
                {
                    for (int j = 0; j < getMaster.Columns.Count; j++)
                    {
                        executormasterWorksheet.Cell(i + 2, j + 1).Value = getMaster.Rows[i][j];
                    }
                }

                // Auto-adjust column widths
                executormasterWorksheet.Columns().AdjustToContents();

                // Apply data validation to the desired column in the main worksheet
                mainSheetColumnIndex = 5; // Assuming you want to bind the dropdown to the fourth column (D)
                 var dropdownRange1 = mainWorksheet.Range("E2:E100");
                var dataValidation1 = dropdownRange1.SetDataValidation();
                dataValidation1.List(masterWorksheet.Range("B2:B" + (getMaster.Rows.Count + 1).ToString()));
                dataValidation1.IgnoreBlanks = true;
                dataValidation1.InCellDropdown = true; 
                #endregion

                #region Code to Add Location Master Data to an Excel Sheet.
                var locationmasterWorksheet = workBook.Worksheets.Add("Location Master Data");
                for (int i = 0; i < getLocationMaster.Columns.Count; i++)
                {
                    locationmasterWorksheet.Cell(1, i + 1).Value = getLocationMaster.Columns[i].ColumnName;
                }

                for (int i = 0; i < getLocationMaster.Rows.Count; i++)
                {
                    for (int j = 0; j < getLocationMaster.Columns.Count; j++)
                    {
                        locationmasterWorksheet.Cell(i + 2, j + 1).Value = getLocationMaster.Rows[i][j];
                    }
                }
                locationmasterWorksheet.Columns().AdjustToContents();
                
                // Apply data validation to the desired column in the main worksheet
                mainSheetColumnIndex = 7; // Assuming you want to bind the dropdown to the fourth column (D)
                var dropdownRange2 = mainWorksheet.Range("G2:G100"); // + mainWorksheet.LastRowUsed().RowNumber().ToString());
                var dataValidation2 = dropdownRange2.SetDataValidation();
                dataValidation2.List(locationmasterWorksheet.Range("C2:C" + (getLocationMaster.Rows.Count + 1).ToString()));
                dataValidation2.IgnoreBlanks = true;
                dataValidation2.InCellDropdown = true;
                #endregion

                // Save the workbook to a memory stream
                using (var memoryStream = new MemoryStream())
                {
                    workBook.SaveAs(memoryStream);
                    byte[] byteArray = memoryStream.ToArray();

                    // Send the file to the client
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment; filename=TaskUpload_Template.xlsx");
                    Response.BinaryWrite(byteArray);
                    Response.End();
                }
            }
        }
    }
}
