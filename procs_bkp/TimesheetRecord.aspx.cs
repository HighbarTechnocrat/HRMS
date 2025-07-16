using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TimesheetRecord : System.Web.UI.Page
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
    bool blnisLastworkingdatePassed = false;

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
            btnIn.Visible = false;
            btnback_mng.Visible = false;
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
                    txtDescription.Attributes.Add("maxlength", "500");
                    //get_Shiftdetails();
                    // get_CheckIn();
                    if (Request.QueryString.Count > 0)
                    {
                        hdnReqid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                    }
                    //txtFromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    btnback_mng.Visible = false;
                    txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtHours.Attributes.Add("onkeypress", "return onCharOnlyNumber_Time(event);");
                    hdnlstfromfor.Value = "Full Day";
                    hdnlsttofor.Value = "Full Day";

                    editform.Visible = true;
                    divbtn.Visible = false;
                    lblmessage.Text = "";
                    this.lstApprover.SelectedIndex = 0;

                    hdnleaveconditiontypeid.Value = "12";
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";

                    PopulateEmployeeData();

                    BindDDLProject();
                    BindDDLTask();
                    BindTimesheetList();
                    if (Convert.ToString(hdnReqid.Value).Trim() != "")
                    {
                        hdnId.Value = Convert.ToString(hdnReqid.Value).Trim();
                        // btnIn.Visible = false;
                        btnBack.Visible = false;
                        btnIn.Visible = true;
                        btnback_mng.Visible = true;

                        //getLeaveRequest_details_forEdit();
                        //setenablefalseConttols();
                        if (hdnId.Value != "0")
                        {
                            //Response.Redirect("TimesheetRecord.aspx?reqid=" + hdnReqid.Value);
                            GetTimesheetDetails(Convert.ToDouble(hdnId.Value));
                        }

                    }
                    //getAttTimeSheet();
                    //SetAttTimeSheetToday();
                    btnback_mng.Visible = false;
                    //getTimesheetDetailsInDate();
                }
                getTimesheetDetailsInDate();

                //var isSave = true;
                //for (int i = 0; i < DgvApprover.Rows.Count; i++)
                //{
                //    var getProject = DgvApprover.Rows[i].Cells[1].Text;
                //    var getTask = DgvApprover.Rows[i].Cells[2].Text;
                //    if (getProject == "Bench (HO)" && getTask == "Idle")
                //    {
                //        isSave = false;
                //    }
                //}
                //if (isSave == false)
                //{
                //    lblmessage.Text = "Please select appropriate project and task";
                //    return;
                //}

                if (blnisLastworkingdatePassed == true)
                {
                    lblmessage.Text = "Timesheet submission not allowed. Last working date has passed.";
                    txtFromdate.Enabled = false;
                    btnBack.Visible = false;
                    btnIn.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    public void get_Shiftdetails()
    {
        try
        {
            DataSet dtTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];
            TimeSpan Instart = new TimeSpan();
            TimeSpan CurTime = new TimeSpan();

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "Get_details";

            spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            spars[1].Value = lpm.Emp_Code.ToString();

            //dtTrDetails = spm.getMobileRemDataList(spars, "[SP_GETALLreembursement_DETAILS]");
            dtTrDetails = spm.getDatasetList(spars, "[SP_Admin_Shift_Change]");
            if (dtTrDetails.Tables[0].Rows.Count > 0)
            {
                // In_range.Text = "Shift In Time Range: " + Convert.ToString(dtTrDetails.Tables[0].Rows[0]["In_range"]);
                // Out_range.Text = "Shift Out Time Range: " + Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Out_range"]);
                Instart = TimeSpan.Parse(Convert.ToString(dtTrDetails.Tables[0].Rows[0]["In_Start"]));
                CurTime = TimeSpan.Parse(DateTime.Now.TimeOfDay.ToString());
                ////if (CurTime > Instart)
                ////{
                ////    btnIn.Enabled = true;
                ////    btnBack.Enabled = true;
                ////}
                ////else
                ////{
                ////    btnIn.Enabled = false;
                ////    btnBack.Enabled = false;
                ////}
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    }

    public void get_CheckIn()
    {
        try
        {
            hdnisTimeInShow.Value = "0";
            hdnisTimeoutShow.Value = "0";
            DataSet dtTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];
            TimeSpan Instart = new TimeSpan();
            TimeSpan CurTime = new TimeSpan();

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "Get_CheckIn";

            spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            spars[1].Value = lpm.Emp_Code.ToString();

            //dtTrDetails = spm.getMobileRemDataList(spars, "[SP_GETALLreembursement_DETAILS]");
            dtTrDetails = spm.getDatasetList(spars, "[SP_Attendance]");
            if (dtTrDetails.Tables[0].Rows.Count > 0)
            {
                //Txt_CheckIn.Visible = true;
                //Txt_InTime.Visible = false;
                //Txt_CheckIn.Text = "Checked In at: " + Convert.ToString(dtTrDetails.Tables[0].Rows[0]["att_time"]);
                //btnIn.Enabled = false;
                //btnIn.Visible = false;
                btnBack.Enabled = true;
                hdnisTimeInShow.Value = "1";
                if (dtTrDetails.Tables[0].Rows.Count > 1)
                {
                    //Txt_CheckOut.Visible = true;
                    //Txt_OutTime.Visible = false;
                    //Txt_CheckOut.Text = "Checked Out at: " + Convert.ToString(dtTrDetails.Tables[0].Rows[1]["att_time"]);
                    btnBack.Enabled = false;
                    btnBack.Visible = false;
                    hdnisTimeoutShow.Value = "1";
                }
                //Instart = TimeSpan.Parse(Convert.ToString(dtTrDetails.Tables[0].Rows[0]["In_Start"]));
                //CurTime = TimeSpan.Parse(DateTime.Now.TimeOfDay.ToString());
                ////if (CurTime > Instart)
                ////{
                ////    btnIn.Enabled = true;
                ////    btnBack.Enabled = true;
                ////}
                ////else
                ////{
                ////    btnIn.Enabled = false;
                ////    btnBack.Enabled = false;
                ////}
            }
            else
            {
                // btnIn.Enabled = true;
                btnBack.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

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

    protected void btnSave_Click1(object sender, EventArgs e)
    {
        try
        {

            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }


            if (Validate_ABAP_CompletationSheet() == true)
            {
                lblmessage.Visible = true;
                lblmessage.Text = "Please Submit your object completion sheet for pending week(s).";
                return;
            }


            #region Check All Parameters Selected
            lblmessage.Text = "";
            if (Convert.ToString(txtFromdate.Text).Trim() == "")
            {
                lblmessage.Text = "Please select From Date";
                return;
            }
            #endregion

            lblmessage.Text = "";
            string[] strdate;
            string strfromDate = "";
            string strtoDate = "";
            string strfromDate_tt = "";

            #region date formatting
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
                strfromDate_tt = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[2]);
            }

            #endregion
            DateTime ddt = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            BindLeaveRequestProperties();

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient();
            StringBuilder strbuild = new StringBuilder();
            String strfileName = "";
            hdnPLwithSL_succession.Value = "";

            #region MaxRequestId

            //  dtMaxRequestId = spm.GetMaxRequestId();
            //reqid = (int)dtMaxRequestId.Rows[0]["Request_id"];
            //reqid = reqid + 1;

            #endregion

            #region MethodsCall
            string strleavetype = "";
            Decimal inoofDays = 0;

            dtApproverEmailIds = spm.GetApproverEmailID(lpm.Emp_Code, hflGrade.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value), strleavetype, inoofDays);
            if (dtApproverEmailIds.Rows.Count > 0)
            {


                approveremailaddress = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];
                apprid = (int)dtApproverEmailIds.Rows[0]["APPR_ID"];

                string strfromfor = "";
                string strtofor = "";
                strfromfor = lpm.Leave_From_for;
                strtofor = lpm.Leave_To_For;


                dtMaxRequestId = spm.InsertLeaveRequest(lpm.Emp_Code, lpm.Leave_Type_id, Convert.ToInt32(hdnleaveconditiontypeid.Value), lpm.Leave_FromDate, strfromfor, lpm.Leave_ToDate, strtofor, lpm.LeaveDays, lpm.Reason, filename);
                Int32 ireqid = 0;
                if (dtMaxRequestId.Rows.Count > 0)
                    ireqid = Convert.ToInt32(dtMaxRequestId.Rows[0]["maxreqid"]);

                if (Convert.ToString(ireqid).Trim() == "0")
                    return;


                string is_self = "N";
                is_self = Is_Self_Approver(Convert.ToString(lpm.Emp_Code));

                if (is_self == "N")
                {
                    spm.InsertApproverRequest(lpm.Approvers_code, apprid, Convert.ToDecimal(dtMaxRequestId.Rows[0]["maxreqid"]));
                }
                String strfromdate_M, strtodate_M;
                strfromdate_M = Convert.ToString(dtMaxRequestId.Rows[0]["fromdate"]).Trim();
                strtodate_M = Convert.ToString(dtMaxRequestId.Rows[0]["todate"]).Trim();

                String strLeaveRstURL = "";
                if (Wsch.ToString().Trim() == "6")
                    strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_LR_S"]).Trim() + "?reqid=" + Convert.ToDecimal(dtMaxRequestId.Rows[0]["maxreqid"]) + "&itype=0";
                else
                    strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_LR"]).Trim() + "?reqid=" + Convert.ToDecimal(dtMaxRequestId.Rows[0]["maxreqid"]) + "&itype=0";

                string strInsertmediaterlist = "";

                string strApproverlist = "";

                string strHREmailForCC = "";
                strHREmailForCC = Get_HREmail_ForCC(Convert.ToString(lpm.Emp_Code));

                if (is_self == "Y")
                {
                    //spm.send_mailto_Requester(hflEmailAddress.Value, "", "Request for " + Convert.ToString(txtLeaveType.Text), txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, strfromdate_M, lpm.Leave_From_for, strtodate_M, lpm.Leave_To_For, lpm.Emp_Name, "", strApproverlist, hflEmpName.Value, "");
                }
                else
                {
                    //spm.send_mailto_RM_Approver(lpm.Emp_Name, lpm.EmailAddress, approveremailaddress, "Request for " + Convert.ToString(txtLeaveType.Text), txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, strfromdate_M, lpm.Leave_From_for, strtodate_M, lpm.Leave_To_For, strHREmailForCC, strLeaveRstURL, strApproverlist, strInsertmediaterlist);
                }

                ClearControls();
                lblmessage.Text = "Leave Request Submitted and Email has been sent to your Reporting Manager for Approval";
                getemployeeLeaveBalance();

                Response.Redirect("~/procs/MyLeave_Req.aspx");
            }
            else
            {
                lblmessage.Text = "Leave Request Failes to submit";
            }


            #endregion

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);

            //throw;
        }
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
                //hheadyear.InnerText = " Leave Card - " + Convert.ToString(dtEmp.Rows[0]["years"]);
                //hheadyear.InnerText = " The below reflected leave balance is subject to changes, post closure of 2020 leave reconciliation on 18th Jan 2021";
                hflGrade.Value = lpm.Grade;
                hflEmailAddress.Value = lpm.EmailAddress;
                hflEmpName.Value = lpm.Emp_Name;
                hflEmpDepartment.Value = lpm.department_name;
                hflEmpDesignation.Value = lpm.Designation_name;

                blnisLastworkingdatePassed = false;

                if (Convert.ToString(dtEmp.Rows[0]["IsLastWorkingDatePast"]).Trim() == "yes")
                    blnisLastworkingdatePassed = true;

                //Call Leave Balance default.
                getemployeeLeaveBalance();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }

    private void getemployeeLeaveBalance()
    {
        //dtleavebal = spm.GetLeaveBalance(lpm.Emp_Code);

        //dgLeaveBalance.DataSource = null;
        //dgLeaveBalance.DataBind();

        //if (dtleavebal.Rows.Count > 0)
        //{
        //    dgLeaveBalance.DataSource = dtleavebal;
        //    dgLeaveBalance.DataBind();
        //}
    }
    private void getAttTimeSheet()
    {
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetEmpAtt_List";

        spars[1] = new SqlParameter("@Emp_code", SqlDbType.VarChar);
        if (lpm.Emp_Code.ToString() == "")
            spars[1].Value = DBNull.Value;
        else
            spars[1].Value = lpm.Emp_Code.ToString();

        DataTable dt = spm.getDropdownList(spars, "SP_Attendance");
        if (dt.Rows.Count > 0)
        {
            DgvApprover.DataSource = dt;
            DgvApprover.DataBind();
        }
    }
    private void SetAttTimeSheetToday()
    {

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GETWORKSCHEDULE";

        spars[1] = new SqlParameter("@Emp_code", SqlDbType.VarChar);
        if (lpm.Emp_Code.ToString() == "")
            spars[1].Value = DBNull.Value;
        else
            spars[1].Value = lpm.Emp_Code.ToString();

        DataTable dt = spm.getDropdownList(spars, "SP_Attendance_CountLeave");
        if (dt.Rows.Count > 0)
        {
            var getToday = DateTime.Now.DayOfWeek.ToString();
            //var getToday = "Sunday";
            var getDays = Convert.ToString(dt.Rows[0]["OFF_DAYS_NAME"]);
            var splitData = getDays.Split(',');
            foreach (string item in splitData)
            {
                if (getToday == item)
                {
                    // btnIn.Visible = false;
                    btnBack.Visible = false;
                    hdnisTimeInShow.Value = "1";
                    hdnisTimeoutShow.Value = "1";
                }
            }
        }
    }
    protected void IsEnabledFalse(Boolean blnSetControl)
    {
        txtFromdate.Enabled = blnSetControl;

        // btnIn.Enabled = blnSetControl;
    }
    public void BindLeaveRequestProperties()
    {

        string[] strdate;
        string strfromDate = "";
        string strToDate = "";
        #region date formatting
        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('-');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        #endregion


        lpm.Emp_Code = txtEmpCode.Text;
        lpm.Leave_FromDate = strfromDate;
        lpm.Leave_ToDate = strToDate;
        lpm.Grade = hflGrade.Value.ToString();
        lpm.Approvers_code = hflapprcode.Value;
        lpm.appr_id = Convert.ToInt32(lstApprover.SelectedIndex);
        lpm.EmailAddress = hflEmailAddress.Value;
        lpm.Emp_Name = hflEmpName.Value;
    }
    protected void LeavedaysCalcualation(string strPLwithSL)
    {
        hdnleavedays.Value = "";


        if (Convert.ToString(strPLwithSL).Trim() == "")
        {
            //uploadfile.Enabled = false;
        }
        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "From date  cannot be blank";
            return;
        }

        if (Convert.ToString(hdnlstfromfor.Value).Trim() == "First Half")
        {
            hdnToDate.Value = txtFromdate.Text;
        }

        string[] strdate;
        string strfromDate = "";
        string strToDate = "";

        #region date formatting
        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        #endregion

        lpm.Emp_Code = txtEmpCode.Text;
        int lvid = 0;

        string strfromfor, strtofor;
        strfromfor = hdnlstfromfor.Value;
        strtofor = hdnlsttofor.Value;

        DataSet dtleavedetails_t = new DataSet();

        if (Convert.ToString(GetLocationforLeaveDaysCalculation()) == "")
        {
            lblmessage.Text = "Cannot apply leave for this dates since, Project change / allocation date is falling between From & To Date of the Leave!";
            return;
        }

        if (Wsch.ToString().Trim() == "6")
        {
            if (Convert.ToString(hdnReqid.Value).Trim() != "")
                dtleavedetails_t = spm.GetLeaveDetails_Site(strfromDate, strToDate, lvid, lpm.Emp_Code, hdnReqid.Value, strfromfor, strtofor, Convert.ToString(strPLwithSL).Trim());
            else
                dtleavedetails_t = spm.GetLeaveDetails_Site(strfromDate, strToDate, lvid, lpm.Emp_Code, "", strfromfor, strtofor, Convert.ToString(strPLwithSL).Trim());
        }
        else
        {
            if (Convert.ToString(hdnReqid.Value).Trim() != "")
                dtleavedetails_t = spm.GetLeaveDetails(strfromDate, strToDate, lvid, lpm.Emp_Code, hdnReqid.Value, strfromfor, strtofor, Convert.ToString(strPLwithSL).Trim());
            else
                dtleavedetails_t = spm.GetLeaveDetails(strfromDate, strToDate, lvid, lpm.Emp_Code, "", strfromfor, strtofor, Convert.ToString(strPLwithSL).Trim());

        }
        message = "";
        hdnmsg.Value = "";
        if (dtleavedetails_t.Tables[0].Rows.Count > 0)
        {
            totaldays = Convert.ToDouble(dtleavedetails_t.Tables[0].Rows[0]["TotalDays"]);
            if (Convert.ToString(dtleavedetails_t.Tables[0].Rows[0]["Message"]).Trim() != "")
                message = (string)dtleavedetails_t.Tables[0].Rows[0]["Message"];

            weekendcount = (int)dtleavedetails_t.Tables[0].Rows[0]["TotalWeekends"];
            publicholiday = (int)dtleavedetails_t.Tables[0].Rows[0]["NoofPublicHoliday"];


        }

        if (Convert.ToString(message).Trim() != "")
        {
            if (Convert.ToString(message).Trim().Contains("Future"))
            {
                lblmessage.Text = Convert.ToString(message).Trim();
                hdnmsg.Value = lblmessage.Text;

            }
            else
            {
                lblmessage.Text = Convert.ToString(message).Trim();
                hdnleavedays.Value = "0";
                hdnmsg.Value = lblmessage.Text;
                if (Convert.ToString(message).Trim().Contains("Please Extend your Privilege Leave"))
                {
                    hdnPLwithSL_succession.Value = "true";
                }
                return;
            }

        }

        if (Convert.ToString(totaldays).Trim() == "0")
        {
            return;
        }

        #region  get Leave days
        double days = 0;
        if (totaldays != 0)
        {
            days = totaldays;
            // btnIn.Enabled = true;
            if (days >= 1)
            {
                //if from & to date are same
                //check for from for = second half & tofor = first half
                //then 1
            }
        }

        #endregion

        #region Check Time of more than One days
        if (lvid == 5 && days > 1)
        {
            lblmessage.Text = "You can not apply more than 1 TO at same time";
            return;
        }
        #endregion


        #region Check PL more than 4 days
        if (lvid == 1 && days > 4)
        {
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "checkPL";

            spars[1] = new SqlParameter("@Emp_code", SqlDbType.VarChar);
            if (lpm.Emp_Code.ToString() == "")
                spars[1].Value = DBNull.Value;
            else
                spars[1].Value = lpm.Emp_Code.ToString();

            DataTable dt = spm.getDropdownList(spars, "SP_Admin_checkPL_4days");
            if (Convert.ToInt32(dt.Rows[0]["cnt"]) > 1)
            {
                lblmessage.Text = "Cannot apply for Privilege Leave > 4days, Since you have already availed More than 4 days Privilege Leave twice in this Year!";
                return;
            }
        }
        #endregion

        #region calculate  LeaveConditionid
        hdnleaveconditiontypeid.Value = "0";
        #endregion

    }

    public void ClearControls()
    {
        txtFromdate.Text = "";
        lblmessage.Text = "";
        hdnFromFor.Value = "";
        hdnToDate.Value = "";
        hdnlsttofor.Value = "";
        hdnlstfromfor.Value = "";
        hdnOldLeaveCount.Value = "";
        hdnleavedays.Value = "";
        hdnLeaveStatus.Value = "";
        hflLeavestatus.Value = "";
        hdnlstfromfor.Value = "Full Day";
        hdnlsttofor.Value = "Full Day";
    }

    protected string Check_LeaveReqest_IsCancellation_process()
    {
        string strleaveProcess = "";
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_LeaveRequest_IsCancellation_process";

            spars[1] = new SqlParameter("@Req_id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDecimal(hdnReqid.Value);

            spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[2].Value = DBNull.Value;

            spars[3] = new SqlParameter("@ext_appr", SqlDbType.VarChar);
            spars[3].Value = DBNull.Value;

            dsTrDetails = spm.getDatasetList(spars, "Usp_getDetails_leaves");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                strleaveProcess = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Action"]).Trim();
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

        return strleaveProcess;

    }

    protected string Check_Leave_15_days_earlier()
    {
        string strleaveProcess = "";
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Check_Leave_15_days_earlier";

            spars[1] = new SqlParameter("@Req_id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDecimal(hdnReqid.Value);

            spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[2].Value = DBNull.Value;

            spars[3] = new SqlParameter("@ext_appr", SqlDbType.VarChar);
            spars[3].Value = DBNull.Value;

            dsTrDetails = spm.getDatasetList(spars, "Usp_getDetails_leaves");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                strleaveProcess = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Leave_ToDate"]).Trim();
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

        return strleaveProcess;

    }

    protected string GetLocationforLeaveDaysCalculation()
    {
        string strleaveProcess = "";
        try
        {
            string[] strdate;
            string strToDate = "";
            string strFromDate = "";
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strFromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "GetLocationforLeaveDaysCalculation";

            spars[1] = new SqlParameter("@fromdate", SqlDbType.VarChar);
            spars[1].Value = strFromDate;

            spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[2].Value = lpm.Emp_Code;

            spars[3] = new SqlParameter("@todate", SqlDbType.VarChar);
            spars[3].Value = strToDate;

            dsTrDetails = spm.getDatasetList(spars, "Usp_getDetails_leaves");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                loc = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["comp_code"]).Trim();
                Wsch = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["wrk_schedule"]).Trim();
                strleaveProcess = loc + Wsch;
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

        return strleaveProcess;

    }

    protected string n_days_earlier_leave_notallowed(int ndays)
    {
        string strleaveProcess = "";
        try
        {
            string[] strdate;
            string strToDate = "";

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "15_days_earlier_Leave";

            spars[1] = new SqlParameter("@Leavedays", SqlDbType.Int);
            spars[1].Value = ndays;

            spars[2] = new SqlParameter("@todate", SqlDbType.VarChar);
            spars[2].Value = strToDate;

            dsTrDetails = spm.getDatasetList(spars, "Usp_getDetails_leaves");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                strleaveProcess = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["todate"]).Trim();
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

        return strleaveProcess;

    }

    protected string Is_Self_Approver(string strEmpCode)
    {
        string strSelfApprover = "";
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Is_Self_Approver";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = strEmpCode;

            dsTrDetails = spm.getDatasetList(spars, "Usp_getDetails_leaves");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                strSelfApprover = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["self_approver"]).Trim();
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

        return strSelfApprover;

    }
    #endregion

    #region Leave Request modification Events

    #endregion

    #region Leave Request modification Methods
    private void getLeaveRequest_details_forEdit()
    {
        try
        {

            #region Get Previous/Next Approvers
            DataTable dtlvstatus = new DataTable();

            //idspnreasnforCancellation.Visible = true;
            //txtLeaveCancelReason.Visible = true;
            dtlvstatus = spm.GetLeaveStatus(hdnReqid.Value);
            hflLeavestatus.Value = "";
            if (dtlvstatus.Rows.Count > 0)
            {
                hflLeavestatus.Value = "Pending";
            }
            //    hflstatusid.Value = (string)dtlvstatus.Rows[0]["Status_id"];
            //    approveremailaddress = (string)dtlvstatus.Rows[0]["EmailAddress"];

            //    for (int i = 0; i < dtlvstatus.Rows.Count; i++)
            //    {
            //        if (Convert.ToString(dtlvstatus.Rows[i]["Status"]).Trim() == "Approved")
            //        {
            //            if (Convert.ToString(approveremailaddress).Trim() == "")
            //            {
            //                approveremailaddress = Convert.ToString(dtlvstatus.Rows[i]["EmailAddress"]).Trim();
            //                hflLeavestatus.Value = Convert.ToString(dtlvstatus.Rows[i]["Status"]).Trim();
            //                hflstatusid.Value = Convert.ToString(dtlvstatus.Rows[i]["Status_id"]).Trim();
            //            }
            //            else
            //            {
            //                approveremailaddress  += ";" + Convert.ToString(dtlvstatus.Rows[i]["EmailAddress"]).Trim();
            //                hflLeavestatus.Value = Convert.ToString(dtlvstatus.Rows[i]["Status"]).Trim();
            //                hflstatusid.Value = Convert.ToString(dtlvstatus.Rows[i]["Status_id"]).Trim();
            //            }

            //        }
            //        else
            //        {
            //            approveremailaddress = Convert.ToString(dtlvstatus.Rows[i]["EmailAddress"]).Trim();
            //            hflLeavestatus.Value = Convert.ToString(dtlvstatus.Rows[i]["Status"]).Trim();
            //            hflstatusid.Value = Convert.ToString(dtlvstatus.Rows[i]["Status_id"]).Trim();
            //        }
            //    }

            //}
            #endregion
            #region set Employee Leave Request details
            DataSet dsList = new DataSet();
            dsList = spm.getLeaveRequest_MngEdit(hdnReqid.Value);
            if (dsList.Tables != null)
            {

                if (dsList.Tables[0].Rows.Count > 0)
                {
                    txtEmpCode.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Code"]).Trim();
                    hflEmpName.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Name"]).Trim();
                    hflEmpDesignation.Value = Convert.ToString(dsList.Tables[0].Rows[0]["DesginationName"]).Trim();
                    hflEmpDepartment.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Department_Name"]).Trim();
                    txtFromdate.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Leave_FromDate"]).Trim();
                    hdnOldLeaveCount.Value = Convert.ToString(dsList.Tables[0].Rows[0]["LeaveDays"]).Trim();
                    hdnleaveconditiontypeid.Value = Convert.ToString(dsList.Tables[0].Rows[0]["LeaveConditionTypeid"]).Trim();
                    hdnlstfromfor.Value = Convert.ToString(dsList.Tables[0].Rows[0]["For_From"]).Trim();

                    hdnfrmdate_emial.Value = Convert.ToString(dsList.Tables[0].Rows[0]["frmdate_email"]).Trim();
                    hdntodate_emial.Value = Convert.ToString(dsList.Tables[0].Rows[0]["todate_email"]).Trim();

                    hdnAppr_status.Value = Convert.ToString(dsList.Tables[0].Rows[0]["lAppr_status"]).Trim();

                    hdnLeaveStatus.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Status_id"]).Trim();

                    if (hdnAppr_status.Value == "Approved" && Convert.ToString(hdnLeaveStatus.Value).Trim() == "1")
                    {
                        txtFromdate.Enabled = false;
                    }

                }
            }
            #endregion
        }
        catch (Exception ex)
        { }
    }

    private void setenablefalseConttols()
    {
        if (Convert.ToString(hdnLeaveStatus.Value).Trim() != "1" && Convert.ToString(hdnLeaveStatus.Value).Trim() != "5")
        {
            txtFromdate.Enabled = false;
        }
        if (hdnAppr_status.Value == "Approved" && Convert.ToString(hdnLeaveStatus.Value).Trim() == "1")
        {
            txtFromdate.Enabled = false;
        }
    }

    protected string Get_HREmail_ForCC(string strEmpCode)
    {
        String sbapp = "";

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_HREmail_ID";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = strEmpCode;


        dsTrDetails = spm.getDatasetList(spars, "Usp_getDetails_leaves");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsTrDetails.Tables[0].Rows.Count; i++)
            {
                if (Convert.ToString(sbapp).Trim() == "")
                    sbapp = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();
                else
                    sbapp = ";" + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();

            }
        }

        return Convert.ToString(sbapp);
    }

    private void getFromdateTodate_FroEmail()
    {
        try
        {

            string[] strdate;
            string strfromDate = "";
            string strToDate = "";

            #region date formatting
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }

            #endregion


            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_FromdateTodate_for_Mail";

            spars[1] = new SqlParameter("@fromdate", SqlDbType.VarChar);
            spars[1].Value = strfromDate;

            spars[2] = new SqlParameter("@todate", SqlDbType.VarChar);
            spars[2].Value = strToDate;

            dsTrDetails = spm.getDatasetList(spars, "Usp_getDetails_leaves");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                hdnfrmdate_emial.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["fromdate"]).Trim();
                hdntodate_emial.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["todate"]).Trim();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    #endregion

    protected void btnIn_Click(object sender, EventArgs e)
    {
        try
        {

            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }

            #region Check All Parameters Selected
            lblmessage.Text = "";
            if (Convert.ToString(txtFromdate.Text).Trim() == "")
            {
                lblmessage.Text = "Please select From Date";
                return;
            }
            #endregion

            lblmessage.Text = "";
            string[] strdate;
            string strfromDate = "";
            string strtoDate = "";
            string strfromDate_tt = "";

            #region date formatting
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('-');
                strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
                strfromDate_tt = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }

            #endregion
            BindLeaveRequestProperties();

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient();
            StringBuilder strbuild = new StringBuilder();
            hdnPLwithSL_succession.Value = "";

            #region MethodsCall
            string att_status = "";

            SqlParameter[] spars1 = new SqlParameter[7];

            spars1[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars1[1] = new SqlParameter("@Id", SqlDbType.Int);
            spars1[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            spars1[3] = new SqlParameter("@att_date", SqlDbType.VarChar);
            spars1[4] = new SqlParameter("@att_type", SqlDbType.VarChar);
            spars1[5] = new SqlParameter("@att_time", SqlDbType.NVarChar);
            spars1[6] = new SqlParameter("@att_status", SqlDbType.NVarChar);


            spars1[0].Value = "Insert";
            spars1[1].Value = DBNull.Value;
            spars1[2].Value = lpm.Emp_Code.ToString();
            if (strfromDate_tt.ToString() == "")
                spars1[3].Value = DBNull.Value;
            else
                spars1[3].Value = strfromDate_tt.ToString();

            spars1[4].Value = "In";
            spars1[5].Value = hdnInTime.Value.ToString();
            spars1[6].Value = att_status;


            spm.Insert_Data(spars1, "SP_Attendance");
            ClearControls();
            lblmessage.Text = "You have successfully checked-In";

            Response.Redirect("~/procs/Attendance.aspx");

            #endregion

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);

            //throw;
        }

    }

    private Boolean Check_isLogin_ABAP()
    {
        Boolean Validate_ABAP = false;
        try
        {
            SqlParameter[] sparsError = new SqlParameter[2];
            sparsError[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            sparsError[0].Value = "check_isABAP_Developer";

            sparsError[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            sparsError[1].Value = Convert.ToString(Session["Empcode"]).Trim();
            DataSet DS = spm.getDatasetList(sparsError, "SP_ABAP_Productivity_CompletionSheet");

            if (DS.Tables[0].Rows.Count > 0)
            {
                Validate_ABAP = true;
            }

        }
        catch (Exception ex)
        {
        }
        return Validate_ABAP;
    }

    private Boolean Validate_ABAP_CompletationSheet()
    {
        Boolean Validate_ABAP = false;
        try
        {
            string[] strdate;
            string strtoDate = "";
            string strfromDate_tt = "";

            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strfromDate_tt = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }

            SqlParameter[] sparsError = new SqlParameter[3];
            sparsError[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            sparsError[0].Value = "DateValidationTimeSheet";
            sparsError[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            sparsError[1].Value = Convert.ToString(Session["Empcode"]).Trim();
            sparsError[2] = new SqlParameter("@TimeSheetDate", SqlDbType.VarChar);
            sparsError[2].Value = strfromDate_tt;
            DataSet DS = spm.getDatasetList(sparsError, "SP_ABAP_Productivity_CompletionSheet");

            if (DS.Tables[0].Rows.Count == 0)
            {
                Validate_ABAP = true;
            }

        }
        catch (Exception ex)
        {
        }
        return Validate_ABAP;
    }

    protected void btnBack_Click1(object sender, EventArgs e)
    {
        try
        {
            btnBack.Visible = true;
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }



            if (txtFromdate.Text.Trim() == "")
            {
                lblmessage.Text = "Please select date";
                return;
            }
            if (txtHours.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter hours";
                return;
            }
            if (ddlProject.SelectedValue.Trim() == "0")
            {
                lblmessage.Text = "Please select project";
                return;
            }
            if (ddlTask.Text.Trim() == "0")
            {
                lblmessage.Text = "Please select task";
                return;
            }
            if (ddlProject.Text.Trim() == "Bench (HO)" && ddlTask.Text.Trim() == "8")
            {
                lblmessage.Text = "Please select appropriate project and task.";
                return;
            }
            if (Convert.ToString(txtHours.Text).Trim() != "")
            {
                var strdate = Convert.ToString(txtHours.Text).Trim().Split(':');
                if (strdate.Length == 2)
                {
                    if (strdate[0].Length != 2 || strdate[1].Length != 2)
                    {
                        lblmessage.Text = "Please enter correct  hours.";
                        return;
                    }
                    else
                    {
                        if (Convert.ToInt32(strdate[0].ToString()) >= 24)
                        {
                            lblmessage.Text = "Please enter less than 24 hours.";
                            return;
                        }
                        if (Convert.ToInt32(strdate[1].ToString()) > 59)
                        {
                            lblmessage.Text = "Please enter correct hours.";
                            return;
                        }
                        if (Convert.ToInt32(strdate[0].ToString()) <= 00 && Convert.ToInt32(strdate[1].ToString()) <= 00)
                        {
                            lblmessage.Text = "Please enter correct hours.";
                            return;
                        }
                    }
                }
                else
                {
                    lblmessage.Text = "Please enter correct hours.";
                    return;
                }
            }

            //if(!checkIsExistProject(txtProject.Text.Trim()))
            //{
            //    lblmessage.Text = "Please select project name in list.";
            //    txtProject.Text = "";
            //    return;
            //}
            //if (!CheckIsExistTask(txtTask.Text.Trim()))
            //{
            //    lblmessage.Text = "Please select task name in list.";
            //    txtTask.Text = "";
            //    return;
            //}

            if (Check_isLogin_ABAP() == true)
            {
                if (Validate_ABAP_CompletationSheet() == true)
                {

                    lblmessage.Visible = true;
                    lblmessage.Text = "Please Submit your object completion sheet for pending week(s). ";

                    return;
                }
            }


            var emp_code = lpm.Emp_Code;
            var projectCode = ddlProject.SelectedValue.ToString();
            //var projectCode = txtProject.Text.Trim().Split('/')[0];
            //var task = ddlTask.SelectedItem.Text.ToString();
            var taskid = Convert.ToInt32(ddlTask.SelectedValue.ToString());
            //var taskid = Convert.ToInt32(txtTask.Text.Trim().Split('/')[0]);
            var hour = txtHours.Text.ToString();
            var Description = txtDescription.Text.ToString();
            DateTime temp = DateTime.ParseExact(txtFromdate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var Timesheet_date = temp.ToString("yyyy-MM-dd");
            var status = "Created";// Created / Pending / Approved / Correction

            //Check Total Hr Is not in 24 Hr 
            if (!CheckIsNot24Hr(Timesheet_date, hour, 0))
            {
                lblmessage.Text = "Please enter less than 24 hours.";
                return;
            }
            //

            var qtype = "INSERTTIMESHEET";
            spm.InsertTimeSheet(qtype, 0, emp_code, Timesheet_date, projectCode, taskid, hour, status, Description);
            lblmessage.Text = "You have add timesheet successfully ";

            Response.Redirect("~/procs/TimesheetRecord.aspx");


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);

            //throw;
        }

    }

    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        try
        {

            lblmessage.Text = "";
            var getDate = txtFromdate.Text.ToString();
            var startDateText = hdnStartDate.Value;
            var endDateText = hdnEndDate.Value;
            DateTime StartDate = DateTime.ParseExact(startDateText, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            DateTime EndDate = DateTime.ParseExact(endDateText, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            DateTime SelectedDate = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime Today = DateTime.Now;


            if (Today < SelectedDate)
            {
                lblmessage.Text = "Please select date only today or less than today";
                //txtFromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromdate.Text = "";
            }
            else if (StartDate > SelectedDate || EndDate < SelectedDate)
            {
                lblmessage.Text = "Please select date range between " + startDateText + "  to " + endDateText;
                txtFromdate.Text = "";
                //txtFromdate.Text= DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                lblmessage.Text = "";
            }

            if (Check_isLogin_ABAP() == true)
            {
                if (Validate_ABAP_CompletationSheet() == true)
                {

                    lblmessage.Visible = true;
                    lblmessage.Text = "Please Submit your object completion sheet for pending week(s). ";
                    txtFromdate.Text = "";
                    return;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void BindDDLProject()
    {
        try
        {
            ddlProject.DataSource = null;
            ddlProject.DataBind();
            var qtype = "Get_ProjectDDL";
            var dtProject = spm.GetProjectTaskList(qtype);
            if (dtProject.Rows.Count > 0)
            {
                ddlProject.DataSource = dtProject;
                ddlProject.DataTextField = "ProjectName";
                ddlProject.DataValueField = "comp_code";
                ddlProject.DataBind();
                ddlProject.Items.Insert(0, new ListItem("Please select project", "0"));
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public void BindDDLTask()
    {
        try
        {
            ddlTask.DataSource = null;
            ddlTask.DataBind();
            var qtype = "Get_TaskDDL";
            var dtProject = spm.GetProjectTaskList(qtype);
            if (dtProject.Rows.Count > 0)
            {
                ddlTask.DataSource = dtProject;
                ddlTask.DataTextField = "Activity_Desc";
                ddlTask.DataValueField = "Activity_Id";
                ddlTask.DataBind();
                ddlTask.Items.Insert(0, new ListItem("Please select task", "0"));
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void BindTimesheetList()
    {
        try
        {
            var emp_code = lpm.Emp_Code;
            DgvApprover.DataSource = null;
            DgvApprover.DataBind();
            var qtype = "GETTIMESHEETLIST";
            var dtTimesheet = spm.GetTimesheetList(qtype, emp_code);
            if (dtTimesheet.Rows.Count > 0)
            {
                DgvApprover.DataSource = dtTimesheet;
                DgvApprover.DataBind();
            }
            var getStartDate = spm.GetWeekStartDateEndDate(0);
            if (getStartDate.Rows.Count > 0)
            {
                var startDate = Convert.ToString(getStartDate.Rows[0]["StartDate"]);
                var endDate = Convert.ToString(getStartDate.Rows[getStartDate.Rows.Count - 1]["StartDate"]);
                hdnStartDate.Value = startDate;
                hdnEndDate.Value = endDate;

            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void DgvApprover_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        var emp_code = lpm.Emp_Code;
        //DgvApprover.DataSource = null;
        //DgvApprover.DataBind();
        var qtype = "GETTIMESHEETLIST";
        var dtTimesheet = spm.GetTimesheetList(qtype, emp_code);
        if (dtTimesheet.Rows.Count > 0)
        {
            DgvApprover.PageIndex = e.NewPageIndex;
            DgvApprover.DataSource = dtTimesheet;
            DgvApprover.DataBind();
        }
        //PopulateEmployeeLeaveData();
        //DgvApprover.PageIndex = e.NewPageIndex;
        //DgvApprover.DataSource = dsLeaveRequst;
        //DgvApprover.DataBind();
    }

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnId.Value = Convert.ToString(DgvApprover.DataKeys[row.RowIndex].Values[0]).Trim();
            if (hdnId.Value != "0")
            {
                Response.Redirect("TimesheetRecord.aspx?reqid=" + hdnId.Value);
                // GetTimesheetDetails(Convert.ToInt32(hdnId.Value));
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            btnIn.Visible = true;
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            if (txtFromdate.Text.Trim() == "")
            {
                lblmessage.Text = "Please select date";
                return;
            }

            if (ddlProject.SelectedValue.Trim() == "0")
            {
                lblmessage.Text = "Please select project name";
                return;
            }
            if (ddlTask.Text.Trim() == "")
            {
                lblmessage.Text = "Please select task name";
                return;
            }
            if (txtHours.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter hours";
                return;
            }
            if (ddlProject.Text.Trim() == "Bench (HO)" && ddlTask.Text.Trim() == "8")
            {
                lblmessage.Text = "Please select appropriate project and task.";
                return;
            }
            if (Convert.ToString(txtHours.Text).Trim() != "")
            {
                var strdate = Convert.ToString(txtHours.Text).Trim().Split(':');
                if (strdate.Length == 2)
                {
                    if (strdate[0].Length != 2 || strdate[1].Length != 2)
                    {
                        lblmessage.Text = "Please enter correct  hours.";
                        return;
                    }
                    else
                    {
                        if (Convert.ToInt32(strdate[0].ToString()) >= 24)
                        {
                            lblmessage.Text = "Please enter less than 24 hours.";
                            return;
                        }
                        if (Convert.ToInt32(strdate[1].ToString()) > 59)
                        {
                            lblmessage.Text = "Please enter correct hours.";
                            return;
                        }
                        if (Convert.ToInt32(strdate[0].ToString()) <= 00 && Convert.ToInt32(strdate[1].ToString()) <= 00)
                        {
                            lblmessage.Text = "Please enter correct hours.";
                            return;
                        }
                    }
                }
                else
                {
                    lblmessage.Text = "Please enter correct hours.";
                    return;
                }
            }

            //if (!checkIsExistProject(txtProject.Text.Trim()))
            //{
            //    lblmessage.Text = "Please select project name in list.";
            //    txtProject.Text = "";
            //    return;
            //}
            //if (!CheckIsExistTask(txtTask.Text.Trim()))
            //{
            //    lblmessage.Text = "Please select task name in list.";
            //    txtTask.Text = "";
            //    return;
            //}
            var emp_code = lpm.Emp_Code;
            var projectCode = ddlProject.SelectedValue.ToString();
            //var projectCode = txtProject.Text.Trim().Split('/')[0];
            //var task = ddlTask.SelectedItem.Text.ToString();
            var taskid = Convert.ToInt32(ddlTask.SelectedItem.Value.ToString());
            //var taskid = Convert.ToInt32(txtTask.Text.Trim().Split('/')[0]);
            //if (txtFromdate.Text.Trim() == "")
            //{
            //    lblmessage.Text = "Please select date";
            //    return;
            //}

            //if (ddlProject.SelectedValue == "0")
            //{
            //    lblmessage.Text = "Please select project name";
            //    return;
            //}
            //if (ddlTask.SelectedValue == "0")
            //{
            //    lblmessage.Text = "Please select task name";
            //    return;
            //}
            //if (txtHours.Text.Trim() == "")
            //{
            //    lblmessage.Text = "Please enter hours";
            //    return;
            //}

            //var emp_code = lpm.Emp_Code;
            //var projectCode = ddlProject.SelectedValue.ToString();
            //var task = ddlTask.SelectedItem.Text.ToString();
            //var taskid = Convert.ToInt32(ddlTask.SelectedItem.Value.ToString());
            var hour = txtHours.Text.ToString();
            var Description = txtDescription.Text.ToString();
            var id = Convert.ToDouble(hdnId.Value);
            DateTime temp = DateTime.ParseExact(txtFromdate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var Timesheet_date = temp.ToString("yyyy-MM-dd");
            var status = "Created";// Created / Pending / Approved / Correction
            //Check Total Hr Is not in 24 Hr 
            if (!CheckIsNot24Hr(Timesheet_date, hour, id))
            {
                lblmessage.Text = "Please enter less than 24 hours.";
                return;
            }
            //
            var qtype = "UpdateTIMESHEET";
            spm.InsertTimeSheet(qtype, id, emp_code, Timesheet_date, projectCode, taskid, hour, status, Description);
            lblmessage.Text = "You have add timesheet successfully ";

            Response.Redirect("~/procs/TimesheetRecord.aspx");


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);

            //throw;
        }
    }

    public void GetTimesheetDetails(double id)
    {
        try
        {

            var dtTimesheet = spm.GetTimesheetDetailById(id);
            if (dtTimesheet.Rows.Count > 0)
            {
                btnBack.Visible = false;
                btnIn.Visible = true;
                txtFromdate.Text = Convert.ToString(dtTimesheet.Rows[0]["TimesheetDate"]);
                txtFromdate.Enabled = false;
                txtHours.Text = Convert.ToString(dtTimesheet.Rows[0]["Hours"]);

                //txtProject.Text = Convert.ToString(dtTimesheet.Rows[0]["ProjectName"]);
                //txtTask.Text = Convert.ToString(dtTimesheet.Rows[0]["Task"]);
                txtDescription.Text = Convert.ToString(dtTimesheet.Rows[0]["Description"]);
                ddlProject.Items.FindByValue(Convert.ToString(dtTimesheet.Rows[0]["ProjectNameDDL"])).Selected = true;
                ddlTask.Items.FindByValue(Convert.ToString(dtTimesheet.Rows[0]["TaskIdDDL"])).Selected = true;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void getTimesheetDetailsInDate()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        string strSelfApprover = "";
        try
        {
            var getdate = hdnStartDate.Value;
            DateTime temp = DateTime.ParseExact(getdate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var Timesheet_date = temp.ToString("yyyy-MM-dd");
            // lblmessage.Text = Timesheet_date+"-"+ lpm.Emp_Code;
            DataSet dsTimesheet = new DataSet();
            dsTimesheet = spm.GetTimesheetDetailForDate(Timesheet_date, lpm.Emp_Code, "Created");
            if (dsTimesheet.Tables.Count > 0)
            {

                var dsTrDetails = dsTimesheet.Tables[0];
                var dsShifDetails = dsTimesheet.Tables[2];
                var getFullDay = Convert.ToString(dsShifDetails.Rows[0]["FullDay_Hrs"]);
                var getHalfDay = Convert.ToString(dsShifDetails.Rows[0]["HalfDay_Hrs"]);
                hdnFullDay.Value = getFullDay;
                hdnHalfDay.Value = getHalfDay;
                // trFullShift.InnerText = getFullDay;
                // trHalfShift.InnerText = getHalfDay;
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
                        employees.Add(sdr["comp_code"].ToString() + "/" + sdr["Location_name"].ToString());
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
                           "   where Activity_Desc like '%' + @SearchText + '%' order by Activity_Desc asc";
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
                        employees.Add(sdr["Activity_Id"].ToString() + "/" + sdr["Activity_Desc"].ToString());
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
        html.Append("<table border = '1' cellspacing='0' width='213% !important'; id='gvMain' style='border-collapse: collapse; border-color: black;'>");
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
                if (name == "Project Name" || name == "Activity Desc")
                {
                    //row[column.ColumnName] = "W";
                    //cell.BackColor = System.Drawing.Color.FromArgb(252, 92, 84);
                    html.Append("<td width='20px'>");
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
        //Full Day Hours as per Shift
        foreach (DataRow row in dtTime.Rows)
        {
            html.Append("<tr class='GridViewScrollItem' style='color: black;background-color: #def7eb;'>");
            html.Append("<td colspan='2' style='text-align:center !important'>Full Day Hours as per Shift</td>");
            foreach (DataColumn column in dtTime.Columns)
            {
                html.Append("<td>");
                var getDate = column.ColumnName.ToString();
                var splitDate = getDate.Split('-');
                var finaldate = splitDate[2] + "-" + splitDate[1] + "-" + splitDate[0];
                var getDt = spm.GetTimesheetRegInbox(lpm.Emp_Code, finaldate);
                var getVal = Convert.ToString(getDt.Rows[0]["ISWORKING"]);
                if (getVal == "WK")
                {
                    html.Append("00:00");
                }
                else if (getVal == "HO")
                {
                    html.Append("00:00");
                }
                else if (getVal == "LE")
                {
                    html.Append("00:00");
                }
                else
                {
                    var getHr = Convert.ToString(getDt.Rows[0]["FullDay_Hrs"]);
                    html.Append(getHr);
                }
                html.Append("</td>");
            }
            html.Append("</tr>");
        }
        //Half Day Hours as per Shift
        foreach (DataRow row in dtTime.Rows)
        {
            html.Append("<tr class='GridViewScrollItem' style='color: black;background-color: #def7eb;'>");//def7eb
            html.Append("<td colspan='2' style='text-align:center !important'>Half Day Hours as per Shift</td>");
            foreach (DataColumn column in dtTime.Columns)
            {
                html.Append("<td>");
                var getDate = column.ColumnName.ToString();
                var splitDate = getDate.Split('-');
                var finaldate = splitDate[2] + "-" + splitDate[1] + "-" + splitDate[0];
                var getDt = spm.GetTimesheetRegInbox(lpm.Emp_Code, finaldate);
                var getVal = Convert.ToString(getDt.Rows[0]["ISWORKING"]);
                if (getVal == "WK")
                {
                    html.Append("00:00");
                }
                else if (getVal == "HO")
                {
                    html.Append("00:00");
                }
                else if (getVal == "LE")
                {
                    var getFro_FromLeave = Convert.ToString(getDt.Rows[0]["For_FromLeave"]); //For_FromLeave
                    if (getFro_FromLeave == "Full Day")
                    {
                        html.Append("00:00");
                    }
                    else
                    {
                        var getHr = Convert.ToString(getDt.Rows[0]["HalfDay_Hrs"]);
                        html.Append(getHr);
                    }
                }
                else
                {
                    var getHr = Convert.ToString(getDt.Rows[0]["HalfDay_Hrs"]);
                    html.Append(getHr);
                }
                html.Append("</td>");
            }
            html.Append("</tr>");
        }
        //Balance Hours as per Shift
        foreach (DataRow row in dtTime.Rows)
        {
            html.Append("<tr class='GridViewScrollItem' style='color: black;background-color: #f7d240;'>");
            html.Append("<td colspan='2' style='text-align:center !important'>Balance Hours as per Shift</td>");
            foreach (DataColumn column in dtTime.Columns)
            {
                html.Append("<td>");
                var getDate = column.ColumnName.ToString();
                var splitDate = getDate.Split('-');
                var finaldate = splitDate[2] + "-" + splitDate[1] + "-" + splitDate[0];
                var startDateText = hdnStartDate.Value;
                DateTime StartDate = DateTime.ParseExact(startDateText, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime CurrentDate = DateTime.ParseExact(finaldate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                if (StartDate <= CurrentDate)
                {
                    var getDt = spm.GetTimesheetRegInbox(lpm.Emp_Code, finaldate);
                    var getVal = Convert.ToString(getDt.Rows[0]["ISWORKING"]);
                    if (getVal == "WK")
                    {
                        html.Append("00:00");
                    }
                    else if (getVal == "HO")
                    {
                        html.Append("00:00");
                    }
                    else if (getVal == "LE")
                    {
                        var getFor_FromLeave = Convert.ToString(getDt.Rows[0]["For_FromLeave"]);
                        var getFor_ToLeave = Convert.ToString(getDt.Rows[0]["For_ToLeave"]);
                        if ((getFor_FromLeave == "First Half" || getFor_FromLeave == "Second Half") || (getFor_ToLeave == "First Half" || getFor_ToLeave == "Second Half"))
                        {
                            var getHalfDay = Convert.ToString(getDt.Rows[0]["HalfDay_Hrs"]);
                            //var getFullDay =Convert.ToString(hdnFullDay.Value);
                            TimeSpan time = TimeSpan.Parse(getHalfDay);
                            var getHr = Convert.ToString(row[column.ColumnName]);
                            if (getHr != "" && getHr != null)
                            {
                                var getEnterHr = TimeSpan.Parse(getHr);
                                TimeSpan duration = new TimeSpan(getEnterHr.Ticks - time.Ticks);
                                if (duration.ToString().Contains("-"))
                                {
                                    var gethr = duration.ToString().Replace("-", "");
                                    var getHrSplit = gethr.Split(':');
                                    var final = getHrSplit[0] + ":" + getHrSplit[1];
                                    html.Append(final);
                                }
                                else
                                {
                                    html.Append("00:00");
                                }
                            }
                            else
                            {
                                html.Append(getHalfDay);
                            }
                        }
                        else
                        {
                            html.Append("00:00");
                        }

                    }
                    else
                    {
                        var getFullDay = Convert.ToString(getDt.Rows[0]["FullDay_Hrs"]);
                        //var getFullDay =Convert.ToString(hdnFullDay.Value);
                        TimeSpan time = TimeSpan.Parse(getFullDay);
                        var getHr = Convert.ToString(row[column.ColumnName]);
                        if (getHr != "" && getHr != null)
                        {
                            var getEnterHr = TimeSpan.Parse(getHr);
                            TimeSpan duration = new TimeSpan(getEnterHr.Ticks - time.Ticks);
                            if (duration.ToString().Contains("-"))
                            {
                                var gethr = duration.ToString().Replace("-", "");
                                var getHrSplit = gethr.Split(':');
                                var final = getHrSplit[0] + ":" + getHrSplit[1];
                                html.Append(final);
                            }
                            else
                            {
                                html.Append("00:00");
                            }
                        }
                        else
                        {
                            html.Append(getFullDay);
                        }
                    }

                }
                else
                {
                    html.Append("00:00");
                }
                html.Append("</td>");
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

    public bool CheckIsNot24Hr(string timesheetDate, string Hours, double id)
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[5];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetTotalHRDayTimesheet";

            spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            spars[1].Value = lpm.Emp_Code;

            spars[2] = new SqlParameter("@Timesheet_date", SqlDbType.VarChar);
            spars[2].Value = timesheetDate;

            spars[3] = new SqlParameter("@Hours", SqlDbType.VarChar);
            spars[3].Value = Hours;

            spars[4] = new SqlParameter("@Id", SqlDbType.BigInt);
            spars[4].Value = id;

            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_Timesheet");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["Message"]);
                var gethr = Convert.ToString(getdtDetails.Rows[0]["HR"]);
                if (getStatus == "INSERT")
                {
                    return true;
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


