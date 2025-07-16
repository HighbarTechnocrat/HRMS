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

public partial class Check_In_Out : System.Web.UI.Page
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
                    hdnTypeId.Value= "0";
                    get_Shiftdetails();
                    get_CheckIn();
                    if (Request.QueryString.Count > 0)
                    {
                        hdnReqid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                    }
                    txtFromdate.Text = DateTime.Now.ToString("dd-MM-yyyy");

                    btnback_mng.Visible = false;
                    txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    hdnlstfromfor.Value = "Full Day";
                    hdnlsttofor.Value = "Full Day";

                    editform.Visible = true;
                    divbtn.Visible = false;
                    lblmessage.Text = "";
                    this.lstApprover.SelectedIndex = 0;

                    hdnleaveconditiontypeid.Value = "12";
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";

                    PopulateEmployeeData();

                    if (Convert.ToString(hdnReqid.Value).Trim() != "")
                    {
                        btnIn.Visible = false;
                        btnBack.Visible = false;
                        btnback_mng.Visible = true;

                        getLeaveRequest_details_forEdit();
                        setenablefalseConttols();


                    }
                    getAttTimeSheet();
                    SetAttTimeSheetToday();
                    SetAttTimeSheetTodayHoliDay();
                    GetDeploymentTypeStatus();
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
                In_range.Text = "Shift In Time Range: " + Convert.ToString(dtTrDetails.Tables[0].Rows[0]["In_range"]);
                Out_range.Text = "Shift Out Time Range: " + Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Out_range"]);
                Instart = TimeSpan.Parse(Convert.ToString(dtTrDetails.Tables[0].Rows[0]["In_Start"]));
                CurTime = TimeSpan.Parse(DateTime.Now.TimeOfDay.ToString());
                var getFullHr = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["FullDay_Hrs"]).Split(':')[0];
                var getFullHr1 = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["FullDay_Hrs"]).Split(':')[1];
                var getHalfHr = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["HalfDay_Hrs"]).Split(':')[0];
                var getHalfHr1 = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["HalfDay_Hrs"]).Split(':')[1];
                var getHalfTime = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Half_Time"]).Split(':')[0]+":"+ Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Half_Time"]).Split(':')[1];
                hdnHalfTime.Value = getHalfTime;
                if (getHalfHr1=="00")
                {
                    getHalfHr1 = "";
                }
                else
                {
                    getHalfHr1 = ":"+ getHalfHr1;
                }

                if (getFullHr1 == "00")
                {
                    getFullHr1 = "";
                }
                else
                {
                    getFullHr1 = ":" + getFullHr1;
                }
                //Label1.Text = "* Shift Full Day  " + getFullHr + ""+ getFullHr1 + " Hours & Half Day " + getHalfHr + ""+ getHalfHr1 + " Hours";
                Label1.Text = "* Shift Full Day  " + getFullHr + ""+ getFullHr1 + " Hours & Half Day " + getHalfHr + ""+ getHalfHr1 + " Hours & Shift Half day Time:-"+ getHalfTime;

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
            //Txt_CheckIn.Visible = false;
            Txt_InTime.Visible = false;
            Txt_OutTime.Visible = false;
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
                Txt_CheckIn.Visible = true;
                Txt_InTime.Visible = false;
                Txt_CheckIn.Text = "Checked In at: " + Convert.ToString(dtTrDetails.Tables[0].Rows[0]["att_time"]);
                btnIn.Enabled = false;
                btnIn.Visible = false;
                btnBack.Enabled = true;
                hdnisTimeInShow.Value = "1";
                if (dtTrDetails.Tables[0].Rows.Count > 1)
                {
                    Txt_CheckOut.Visible = true;
                    Txt_OutTime.Visible = false;
                    Txt_CheckOut.Text = "Checked Out at: " + Convert.ToString(dtTrDetails.Tables[0].Rows[1]["att_time"]);
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
                btnIn.Enabled = true;
                btnBack.Visible = false;
                liout.Visible = false;
                hdnisTimeoutShow.Value = "1";
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
				hheadyear.InnerText = " Leave Card - " + Convert.ToString(dtEmp.Rows[0]["years"]);
                //hheadyear.InnerText = " The below reflected leave balance is subject to changes, post closure of 2020 leave reconciliation on 18th Jan 2021";
                hflGrade.Value = lpm.Grade;
                hflEmailAddress.Value = lpm.EmailAddress;
                hflEmpName.Value = lpm.Emp_Name;
                hflEmpDepartment.Value = lpm.department_name;
                hflEmpDesignation.Value = lpm.Designation_name;
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
        dtleavebal = spm.GetLeaveBalance(lpm.Emp_Code);

        dgLeaveBalance.DataSource = null;
        dgLeaveBalance.DataBind();

        if (dtleavebal.Rows.Count > 0)
        {
            dgLeaveBalance.DataSource = dtleavebal;
            dgLeaveBalance.DataBind();
        }
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
        if (dt.Rows.Count>0)
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
                if(getToday==item)
                {
                    btnIn.Visible = false;
                    btnBack.Visible = false;
                    hdnisTimeInShow.Value = "1";
                    hdnisTimeoutShow.Value = "1";
                }
            }
        }
    }
    private void SetAttTimeSheetTodayHoliDay()
    {

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GETHolidayWORKSCHEDULE";

        spars[1] = new SqlParameter("@Emp_code", SqlDbType.VarChar);
        if (lpm.Emp_Code.ToString() == "")
            spars[1].Value = DBNull.Value;
        else
            spars[1].Value = lpm.Emp_Code.ToString();

        DataTable dt = spm.getDropdownList(spars, "SP_Attendance_CountLeave");
        if (dt.Rows.Count > 0)
        {
            var getDays = Convert.ToString(dt.Rows[0]["ISWORKING"]);
            if(getDays== "HO")
            {
                btnIn.Visible = false;
                btnBack.Visible = false;
                hdnisTimeInShow.Value = "1";
                hdnisTimeoutShow.Value = "1";
            }
            
        }
    }
    protected void IsEnabledFalse(Boolean blnSetControl)
    {
        txtFromdate.Enabled = blnSetControl;

        btnIn.Enabled = blnSetControl;
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
            btnIn.Enabled = true;
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
            //var getTime=
            #region Check All Parameters Selected
            lblmessage.Text = "";
            if (Convert.ToString(txtFromdate.Text).Trim() == "")
            {
                lblmessage.Text = "Please select From Date";
                return;
            }
            //if (Convert.ToString(hdnInTime.Value).Trim() == "")
            //{
            //    lblmessage.Text = "Please wait for timer start.";
            //    return;
            //}
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

            //MailMessage mail = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient();
            //StringBuilder strbuild = new StringBuilder();
            hdnPLwithSL_succession.Value = "";

            #region MethodsCall
            string att_status = "";

            SqlParameter[] spars1 = new SqlParameter[9];

            spars1[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars1[1] = new SqlParameter("@Id", SqlDbType.Int);
            spars1[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            spars1[3] = new SqlParameter("@att_date", SqlDbType.VarChar);
            spars1[4] = new SqlParameter("@att_type", SqlDbType.VarChar);
            spars1[5] = new SqlParameter("@att_time", SqlDbType.NVarChar);
            spars1[6] = new SqlParameter("@att_status", SqlDbType.NVarChar);
            spars1[7] = new SqlParameter("@IpAddress", SqlDbType.NVarChar);
            spars1[8] = new SqlParameter("@Type_Id", SqlDbType.Int);


            spars1[0].Value = "Insert";
            spars1[1].Value = DBNull.Value;
            spars1[2].Value = lpm.Emp_Code.ToString();
            if (strfromDate_tt.ToString() == "")
                spars1[3].Value = DBNull.Value;
            else
                spars1[3].Value = strfromDate_tt.ToString();

            spars1[4].Value = "In";
            // spars1[5].Value = hdnInTime.Value.ToString();
            spars1[5].Value = null;
            spars1[6].Value = att_status;
            spars1[7].Value = SesstionID();
            spars1[8].Value =Convert.ToInt32(hdnTypeId.Value);


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

    protected void btnBack_Click1(object sender, EventArgs e)
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
            //if (Convert.ToString(hdnOutTime.Value).Trim() == "")
            //{
            //    lblmessage.Text = "Please wait for timer start.";
            //    return;
            //}
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

            //MailMessage mail = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient();
            //StringBuilder strbuild = new StringBuilder();
            hdnPLwithSL_succession.Value = "";

            #region MethodsCall
            string att_status = "";

            SqlParameter[] spars1 = new SqlParameter[9];

            spars1[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars1[1] = new SqlParameter("@Id", SqlDbType.Int);
            spars1[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            spars1[3] = new SqlParameter("@att_date", SqlDbType.VarChar);
            spars1[4] = new SqlParameter("@att_type", SqlDbType.VarChar);
            spars1[5] = new SqlParameter("@att_time", SqlDbType.NVarChar);
            spars1[6] = new SqlParameter("@att_status", SqlDbType.NVarChar);
            spars1[7] = new SqlParameter("@IpAddress", SqlDbType.NVarChar);
            spars1[8] = new SqlParameter("@Type_Id", SqlDbType.Int);

            spars1[0].Value = "Update";
            spars1[1].Value = DBNull.Value;
            spars1[2].Value = lpm.Emp_Code.ToString();
            if (strfromDate_tt.ToString() == "")
                spars1[3].Value = DBNull.Value;
            else
                spars1[3].Value = strfromDate_tt.ToString();

            spars1[4].Value = "Out";
            //spars1[5].Value = hdnOutTime.Value.ToString();
            spars1[5].Value = null;
            spars1[6].Value = att_status;
            spars1[7].Value = SesstionID();
            spars1[8].Value = Convert.ToInt32(hdnTypeId.Value);


            spm.Insert_Data(spars1, "SP_Attendance");
            ClearControls();
            lblmessage.Text = "You have successfully checked-out";

            Response.Redirect("~/procs/Attendance.aspx");

            #endregion

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);

            //throw;
        }

    }
    public void GetDeploymentTypeStatus()
    {
        try
        {
            var finaldate = DateTime.Now.ToString("yyyy-MM-dd");
            var getDt = spm.GetDeploymentTypeStatus("GetDeploymentStatus", lpm.Emp_Code, finaldate);

            if (getDt != null)
            {
                if (getDt.Rows.Count > 0)
                {
                    var TypeId = Convert.ToInt32(getDt.Rows[0]["TypeId"]);
                    hdnTypeId.Value = Convert.ToString(TypeId);
                    if (TypeId == 1)
                    {
                        var TypeName = Convert.ToString(getDt.Rows[0]["TypeName"]);
                        var IPAddress = Convert.ToString(getDt.Rows[0]["IPAddress"]);
                        var getIPAddress = GetIPAddress();
                        if (IPAddress != getIPAddress)
                        {
                            hdnTypeId.Value = "2";
                           var getToDayODStatus= spm.GetDeploymentTypeStatus("CheckToDayODApplication", lpm.Emp_Code, finaldate);
                            if(getToDayODStatus!=null)
                            {
                                if(getToDayODStatus.Rows.Count>0)
                                {
                                    var getMessage = getToDayODStatus.Rows[0]["Message"].ToString();
                                    if(getMessage=="YES")
                                    {
                                        var getHalfDayTime =Convert.ToDateTime(hdnHalfTime.Value.ToString());
                                        var ConvertHalfDayTime = getHalfDayTime.ToString("HH:mm:ss");
                                        var CurrentDateTime = DateTime.Now.ToString("HH:mm:ss");
                                        var getStatus = getToDayODStatus.Rows[0]["Status"].ToString();
                                        var convertCurrentTime = Convert.ToDateTime(CurrentDateTime).TimeOfDay;
                                        var ConvertHalfDayTime1 = Convert.ToDateTime(ConvertHalfDayTime).TimeOfDay;
                                        if(getStatus == "First Half")
                                        {
                                            if(convertCurrentTime> ConvertHalfDayTime1)
                                            {
                                                btnBack.Visible = false;
                                                hdnisTimeoutShow.Value = "1";
                                                lblmessage.Text = " You have to be in Head Office Network in order to Check In / Check Out!";
                                                lblmessage.Visible = true;
                                            }
                                        }
                                        else if (getStatus == "Second Half")
                                        {
                                            if (convertCurrentTime < ConvertHalfDayTime1)
                                            {
                                                btnIn.Visible = false;
                                                hdnisTimeInShow.Value = "1";
                                                lblmessage.Text = " You have to be in Head Office Network in order to Check In / Check Out!";
                                                lblmessage.Visible = true;
                                            }
                                        }                                        
                                        else 
                                        {

                                        }
                                    }
                                    else
                                    {
                                        btnIn.Visible = false;
                                        btnBack.Visible = false;
                                        hdnisTimeInShow.Value = "1";
                                        hdnisTimeoutShow.Value = "1";

                                        //lblmessage.Text = "You are not entitled for Attendance." + TypeName;
                                        lblmessage.Text = "You have to be in Head Office Network in order to Check In / Check Out!";
                                        lblmessage.Visible = true;
                                    }
                                }
                                else
                                {
                                    btnIn.Visible = false;
                                    btnBack.Visible = false;
                                    hdnisTimeInShow.Value = "1";
                                    hdnisTimeoutShow.Value = "1";

                                    //lblmessage.Text = "You are not entitled for Attendance." + TypeName;
                                    lblmessage.Text = "You have to be in Head Office Network in order to Check In / Check Out!";
                                    lblmessage.Visible = true;
                                }
                            }
                            else
                            {
                                btnIn.Visible = false;
                                btnBack.Visible = false;
                                hdnisTimeInShow.Value = "1";
                                hdnisTimeoutShow.Value = "1";

                                //lblmessage.Text = "You are not entitled for Attendance." + TypeName;
                                lblmessage.Text = "You have to be in Head Office Network in order to Check In / Check Out!";
                                lblmessage.Visible = true;
                            }
                           
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected string GetIPAddress()
    {
       string ip = "";
        try
        {
            string _ipList = "";
            //string _ipList = HttpContext.Current.Request.Headers["CF-CONNECTING-IP"].ToString();

            _ipList = HttpContext.Current.Request.ServerVariables["HTTP_X_CLUSTER_CLIENT_IP"];
            if (!string.IsNullOrWhiteSpace(_ipList))
            {
                ip += _ipList.Split(',')[0].Trim();
            }
            else
            {
                _ipList = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrWhiteSpace(_ipList))
                {
                    ip += _ipList.Split(',')[0].Trim();
                }
                else
                {
                    ip += HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString().Trim();
                }
            }
        }
        catch (Exception ex)
        {
            ip = "110.173.184.58";
        }
        return ip;
    }
    public string SesstionID()
    {
        string returnString = "";
        try
        {
            var SesstionId = HttpContext.Current.Session.SessionID;
            //var resultString = Regex.Replace(SesstionId, "[^0-9]+", string.Empty);
            returnString = SesstionId;
        }
        catch (Exception)
        {
        }
        return returnString;
    }

}


