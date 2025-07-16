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



public partial class Leave_Req_hbt : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc="", dept="", subdept="", desg = "";
    public int did = 0;
    LeaveBalance bl = new LeaveBalance();
    SP_Methods spm = new SP_Methods();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    public DataTable dtEmp, dtleavebal, dtApprover, dtLeaveTypes, dtApproverEmailIds, dtMaxRequestId, dtIntermediate, dtLeaveCalculation, dtHolidays;
    public int Leaveid;
    public int leavetype, openbal, avail, rembal, leaveconditionid;
    public string filename = "", approveremailaddress;
    DateTime holidaydate = new DateTime();

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url;}
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/SampleForm");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    divbtn.Visible = false;
                    divmsg.Visible = false;
                    txtEmpCode.Text = "This is before function";
                    PopulateEmployeeData();
                    //txtReason.Text = "This is after function";
                     this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    public void PopulateEmployeeData()
    {
        try
        {
            string dgheader1 = "APPR_ID";
            string dgheader2 = "A_EMP_CODE";
          //  lpm.Emp_Code = "00630134";
            txtEmpCode.Text = lpm.Emp_Code;

            //    txtEmpCode.Enabled = false; 

            //  BindControls();
            dtEmp = spm.GetEmployeeData(lpm.Emp_Code);
            //txtReason.Text = "This is after function";
            if (dtEmp.Rows.Count > 0)
            {
                txtReason.Text = "This is after function";
                //myTextBox.Text = (string) dt.Rows[0]["name"];
                lpm.Emp_Status = (string)dtEmp.Rows[0]["Emp_status"];
                if (lpm.Emp_Status == "Resgined")
                {
                    txtReason.Text = "This is Resigned function";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Information", "alert('You are not allowed to apply for any type of leave sicne your employee status is in resignation')", true);
                    //Response.Write("You are not allowed to apply for any type of leave sicne your employee status is in resignation");
                }
                else
                {
                    txtReason.Text = "This is value block function";
                    lpm.Emp_Name = (string)dtEmp.Rows[0]["Emp_Name"];
                    lpm.Designation_name = (string)dtEmp.Rows[0]["DesginationName"];
                    lpm.department_name = (string)dtEmp.Rows[0]["Department_Name"];
                    lpm.Grade = (string)dtEmp.Rows[0]["Grade"];
                    lpm.EmailAddress = (string)dtEmp.Rows[0]["Emp_EmailAddress"];

                    //txtEmpName.Text = lpm.Emp_Name;
                    //txtDepartment.Text = lpm.department_name;
                    //txtDesig.Text = lpm.Designation_name ;
                    hflGrade.Value = lpm.Grade;
                    hflEmailAddress.Value = lpm.EmailAddress;
                    hflEmpName.Value = lpm.Emp_Name;
                    hflEmpDepartment.Value = lpm.department_name;
                    hflEmpDesignation.Value = lpm.Designation_name;

                  
                    dtleavebal = spm.GetLeaveBalance(lpm.Emp_Code);
                    dgLeaveBalance.DataSource = dtleavebal;
                    dgLeaveBalance.DataBind();
                    txtReason.Text = dtleavebal.Rows.Count.ToString() + " This is gridview function";

                    dtApprover = spm.GetApproverName(lpm.Emp_Code);

                    if (dtApprover.Rows.Count > 0)
                    {
                      
                        lstApprover.DataSource = dtApprover;
                        lstApprover.DataTextField = "Emp_Name";
                        lstApprover.DataValueField = "APPR_ID";
                        lstApprover.DataBind();

                        lpm.Approvers_code = (string)dtApprover.Rows[0]["A_EMP_CODE"];
                        hflapprcode.Value = lpm.Approvers_code;
                        txtReason.Text = dtApprover.Rows.Count.ToString() + " This is dtApprover function";
                    }

                    dtIntermediate = spm.GetIntermediateName(lpm.Emp_Code,0,"");
                    if (dtIntermediate.Rows.Count > 0)
                    {
                        lstIntermediate.DataSource = dtIntermediate;
                        lstIntermediate.DataTextField = "Emp_Name";
                        lstIntermediate.DataValueField = "APPR_ID";
                        lstIntermediate.DataBind();
                    }

                    //dtLeaveTypes = spm.GetLeaveType();
                    //ddlLeaveType.DataSource = dtLeaveTypes;
                    //ddlLeaveType.DataTextField = "Leave_Type_Description";
                    //ddlLeaveType.DataValueField = "Leavetype_id";
                    //ddlLeaveType.DataBind();
                   

                 


                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();
            txtReason.Text = "This is Catch function";
            throw;
        }
        //BindLeaveRequestProperties();
        //lpm.Emp_Code = txtEmpCode.Text;

    
    }

    public void BindLeaveRequestProperties()
    {
        lpm.Emp_Code = txtEmpCode.Text;
        lpm.LeaveDays = Convert.ToInt32(txtLeaveDays.Text);
        lpm.Leave_Type_id = Convert.ToInt32(ddlLeaveType.SelectedValue);
     //   lpm.Leave_FromDate = Convert.ToDateTime(txtFromdate.Text);
        lpm.Leave_From_for = ddlFromFor.Text;
     //   lpm.Leave_ToDate = Convert.ToDateTime(txtToDate.Text);
        lpm.Leave_To_For = ddlToFor.Text;
        lpm.Reason = txtReason.Text;
        lpm.Grade = hflGrade.Value.ToString();
        lpm.Approvers_code = hflapprcode.Value;
        lpm.appr_id = Convert.ToInt32(lstApprover.SelectedValue);
        lpm.EmailAddress = hflEmailAddress.Value;
        lpm.Emp_Name = hflEmpName.Value;

    }  
  
    protected void txtLeaveDays_TextChanged(object sender, EventArgs e)
    {
        int lvdays = Convert.ToInt32(txtLeaveDays.Text);
        if (Convert.ToString(ddlLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_SL"]).Trim() && (lvdays >= 5))
        {
            uploadprofile.Enabled = false;
        }
        else
        {
            uploadprofile.Enabled = true;
        }
            
    }
    protected void txtsadate_TextChanged(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            DateTime FromDate = Convert.ToDateTime(txtsadate.Text );
            if (FromDate < DateTime.Now)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Information", "alert('Not allowed to apply leave for back dates')", true);
                txtsadate.Text = "";
            }
        }
    }
    protected void txtedate_TextChanged(object sender, EventArgs e)
    {
        lpm.Emp_Code = txtEmpCode.Text;
        ; int lvid;
        double Days = 0;

        lvid = Convert.ToInt32(ddlLeaveType.SelectedValue);



        DateTime FromDate = Convert.ToDateTime(txtsadate.Text);
        DateTime ToDate = Convert.ToDateTime(txtedate.Text);

        // Function count no of sat and sun between the Leave Dates
        int noofweekends = spm.GetWeekends(FromDate, ToDate);

        // Getting the leave balance based on leave type
        dtLeaveCalculation = spm.LeaveCalculation(lpm.Emp_Code, lvid);
        lpm.leave_balance = (double)dtLeaveCalculation.Rows[0]["Balance"];


        // Gets the date of Holiday based on the selected date
        //dtHolidays = spm.GetHolidayDate(FromDate, ToDate);

        //if (dtHolidays.Rows.Count > 0)
        //{
        //    holidaydate = (DateTime)dtHolidays.Rows[0]["Holiday_Date"];
        //}


        // Logic to cross check the from date and to date Criteria
        if (ToDate < FromDate)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", "alert('To Date date should be greate than From Date date')", true);
            txtedate.Text = " ";
            txtsadate.Text = " ";
        }
        //else if ((FromDate == holidaydate || ToDate == holidaydate))
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", "alert('Holidays cannot be applied on public holiday')", true);
        //    txtedate.Text = " ";
        //}

        else
        {
            TimeSpan objTimeSpan = ToDate - FromDate;
            Days = Convert.ToDouble(objTimeSpan.TotalDays);
            Days = Days + 1;


        }
        if ((noofweekends > 0) && (lvid == 1))
        {
            //Days = Days - noofweekends;
            txtLeaveDays.Text = Days.ToString();

        }

        else if ((noofweekends > 0) && (lvid == 2))
        {
            Days = Days - noofweekends;
            txtLeaveDays.Text = Days.ToString();

        }

        else
        {
            txtLeaveDays.Text = Days.ToString();
        }



        if (Days > lpm.leave_balance)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", "alert('Due to inadequate balance, you can not apply for Leave.')", true);
            txtedate.Text = " ";
            txtLeaveDays.Text = " ";

        }
            
       
    }
    protected void ddlFromFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(ddlLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_SL"]).Trim() && (ddlFromFor.Text == "First Half"))
        {
            txtedate.Enabled = false;
            ddlToFor.Enabled = false;
            txtedate.Text = txtedate.Text;
            txtLeaveDays.Text = " " + 0.5;

        }
        else if (Convert.ToString(ddlLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_SL"]).Trim() && (ddlFromFor.Text == "Second Half") || (ddlFromFor.Text == "Full Day"))
        {
            txtedate.Enabled = true;

        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click1(object sender, EventArgs e)
    {
        try
        {
            BindLeaveRequestProperties();

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient();
            StringBuilder strbuild = new StringBuilder();


            #region fileUpload
            if (Convert.ToString(ddlLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_SL"]).Trim() && lpm.LeaveDays > 5)
            {
                if (uploadprofile.HasFile)
                {
                    filename = Path.Combine(Server.MapPath(""), uploadprofile.FileName);
                    uploadprofile.SaveAs(filename);

                }
            }
            else
            {
                filename = "Not Applicable";
            }
            #endregion

            #region LeaveConditionid
            if (Convert.ToString(ddlLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_PL"]).Trim() && (lpm.LeaveDays <= 15))
            {
                leaveconditionid = 2;
            }
            else if (Convert.ToString(ddlLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_PL"]).Trim() && (lpm.LeaveDays > 15))
            {
                leaveconditionid = 1;
            }
            #endregion

            #region MaxRequestId

            dtMaxRequestId = spm.GetMaxRequestId();
            //reqid = (int)dtMaxRequestId.Rows[0]["Request_id"];
            //reqid = reqid + 1;

            #endregion

            #region MethodsCall

       //     spm.InsertLeaveRequest(lpm.Emp_Code, lpm.Leave_Type_id, leaveconditionid, lpm.Leave_FromDate, lpm.Leave_From_for, lpm.Leave_ToDate, lpm.Leave_To_For, lpm.LeaveDays, lpm.Reason, filename);
           // spm.InsertApproverRequest(lpm.Approvers_code, lpm.appr_id);

            dtApproverEmailIds = spm.GetApproverEmailID(lpm.Emp_Code, lpm.Grade, leaveconditionid,"",0);
            approveremailaddress = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];

       //     spm.send_mailto_RM_Approver(lpm.Emp_Name, lpm.EmailAddress, approveremailaddress, "Leave Request", "Peivillege Leave", lpm.LeaveDays.ToString(), lpm.Reason, lpm.Leave_FromDate.ToShortDateString(), lpm.Leave_From_for, lpm.Leave_ToDate.ToShortDateString(), lpm.Leave_To_For);


            //    (string ReqMailID, string toMailIDs, string strsubject, string tType, string tDays, string tRemarks, string tFrom, string tFrom_For, string tTo, string tTo_For, string AppMailID, string Rej_Remarks)
            //   spm.sendMail(approveremailaddress, "Leave Request", Convert.ToString(strbuild).Trim(), filename);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Information", "alert('Leave Request Submiteed and Email has been send to your Reporting Manager for Approver')", true);

            lblmessage.Text = "Leave Request Submiteed and Email has been send to your Reporting Manager for Approver"; 

            #endregion

        }
        catch (Exception)
        {

            throw;
        }
    
      
    }
}
