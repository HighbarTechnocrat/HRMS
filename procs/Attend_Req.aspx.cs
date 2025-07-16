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

public partial class Attend_Req : System.Web.UI.Page
{
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
                    txtLeaveCancelReason.Enabled = false;
                    if (Request.QueryString.Count > 0)
                    {
                        hdnReqid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        txtLeaveCancelReason.Enabled = true;
                    }

                    btnMod.Visible = false;
                    btnCancel.Visible = false;
                    btnback_mng.Visible = false;
                    uploadfile.Enabled = false;
                    idspnreasnforCancellation.Visible = false;
                    txtLeaveCancelReason.Visible = false;

                    txtLeaveType.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtFromfor.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtToFor.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtToDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    hdnlstfromfor.Value = "Full Day";
                    hdnlsttofor.Value = "Full Day";

                    editform.Visible = true;
                    divbtn.Visible = false;
                    lblmessage.Text = "";
                    txtFromfor.Text = "Full Day";
                    txtToFor.Text = "Full Day";
                    this.lstApprover.SelectedIndex = 0;

                    hdnleaveconditiontypeid.Value = "2";
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                    txtLeaveType.BackColor = Color.FromArgb(235, 235, 228);

                    PopulateEmployeeData();
                    txtFromfor.Enabled = false;
                    txtToFor.Enabled = false;


                    Fullday.Enabled = false;
                    FirstHalf.Enabled = false;
                    SecondHalf.Enabled = false;
                    Radio1.Enabled = false;
                    Radio2.Enabled = false;
                

                    if (Convert.ToString(hdnReqid.Value).Trim() != "")
                    {
                        btnSave.Visible = false;
                        btnBack.Visible = false;

                        btnMod.Visible = true;
                        btnCancel.Visible = true;
                        btnback_mng.Visible = true;

                        getLeaveRequest_details_forEdit();
                        getApproverlist(lpm.Emp_Code, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value));

                        if (Convert.ToString(txtFromdate.Text).Trim() == Convert.ToString(txtToDate.Text).Trim())
                        {
                            Fullday.Enabled = true;
                            FirstHalf.Enabled = true;
                            SecondHalf.Enabled = true;
                            Radio1.Enabled = false;
                            Radio2.Enabled = false;
                            txtToFor.Text = "";
                            hdnlsttofor.Value = "";
                        }
                        else
                        {
                            Fullday.Enabled = true;
                            FirstHalf.Enabled = true;
                            SecondHalf.Enabled = true;
                            Radio1.Enabled = true;
                            Radio2.Enabled = true;
                        }
                       
                        
                        setenablefalseConttols();

                        if(Convert.ToString(Check_LeaveReqest_IsCancellation_process())!="")
                        {
                            btnCancel.Visible = false;
                        }


                    }
                    else
                    {
                        getApproverlist(lpm.Emp_Code, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value));

                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        
        if (Convert.ToString(txtLeaveType.Text).Trim() == "")
        {
            lblmessage.Text = "Please select the leave type";
            txtFromdate.Text = "";
            return;
        }

        if (FirstHalf.Checked)
            txtFromfor.Text = "First Half";
        if (Fullday.Checked)
            txtFromfor.Text = "Full Day";
        if (SecondHalf.Checked)
            txtFromfor.Text = "Second Half";

        if (Radio2.Checked)
            txtToFor.Text = "First Half";
        if (Radio1.Checked)
            txtToFor.Text = "Full Day";

        hdnlsttofor.Value = Convert.ToString(txtToFor.Text);
        hdnlstfromfor.Value = Convert.ToString(txtFromfor.Text);

        if (Convert.ToString(txtFromfor.Text).Trim() == "First Half")
        {
            txtToDate.Text = txtFromdate.Text;
            txtToDate.Enabled = false;
        }

        if (Convert.ToString(lstLeaveType.SelectedValue).Trim() == "5")
        {
            txtToDate.Text = txtFromdate.Text;
            txtToDate.Enabled = false;
            Radio1.Enabled = false;
            Radio2.Enabled = false;
            txtToFor.BackColor = Color.FromArgb(235, 235, 228);
        }
        else
        {
            txtToDate.Enabled = true;
            txtToFor.BackColor = Color.FromArgb(255, 255, 255);
        }

        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                if (Convert.ToString(txtFromdate.Text).Trim() == Convert.ToString(txtToDate.Text).Trim())
                {
                    Fullday.Enabled = true;
                    FirstHalf.Enabled = true;
                    SecondHalf.Enabled = true;
                    Radio1.Enabled = false;
                    Radio2.Enabled = false;
                    txtToFor.Text = "";
                    hdnlsttofor.Value = "";
                }
                else
                {
                    Fullday.Enabled = true;
                    FirstHalf.Enabled = true;
                    SecondHalf.Enabled = true;
                    Radio1.Enabled = true;
                    Radio2.Enabled = true;
                    txtToFor.Text = "Full Day";
                    hdnlsttofor.Value = "Full Day";
                }
            }
        }


        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            LeavedaysCalcualation("");

        }
        
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            lblmessage.Text = "";

            if (FirstHalf.Checked)
                txtFromfor.Text = "First Half";
            if (Fullday.Checked)
                txtFromfor.Text = "Full Day";
            if (SecondHalf.Checked)
                txtFromfor.Text = "Second Half";

            if (Radio2.Checked)
                txtToFor.Text = "First Half";
            if (Radio1.Checked)
                txtToFor.Text = "Full Day";

            hdnlsttofor.Value = Convert.ToString(txtToFor.Text);
            hdnlstfromfor.Value = Convert.ToString(txtFromfor.Text);

            if (Convert.ToString(txtToDate.Text).Trim() != "")
            {
                if (Convert.ToString(txtFromdate.Text).Trim() != "")
                {
                    if (Convert.ToString(txtFromdate.Text).Trim() == Convert.ToString(txtToDate.Text).Trim())
                    {
                        Fullday.Enabled = true;
                        FirstHalf.Enabled = true;
                        SecondHalf.Enabled = true;
                        Radio1.Enabled = false;
                        Radio2.Enabled = false;
                        txtToFor.Text = "";
                        hdnlsttofor.Value = "";
                    }
                    else
                    {
                        Fullday.Enabled = true;
                        FirstHalf.Enabled = true;
                        SecondHalf.Enabled = true;
                        Radio1.Enabled = true;
                        Radio2.Enabled = true;
                        txtToFor.Text = "Full Day";
                        hdnlsttofor.Value = "Full Day";
                    } 
                }
            }
            LeavedaysCalcualation("");

        }


    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Attendance.aspx");
    }

    protected void lstLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {

        txtLeaveType.Text =  Convert.ToString(lstLeaveType.SelectedItem.Text).Trim(); //Add Convert String on 17092018 If Selected Leave Type is LWP and From date was clear.
        txtToFor.Text = "Full Day";
        txtFromfor.Text = "Full Day";
        Fullday.Checked = true;
        FirstHalf.Checked = false;
        SecondHalf.Checked = false;
        Radio1.Checked = true;
        Radio2.Checked = false;
        Fullday.Enabled = false;
        FirstHalf.Enabled = false;
        SecondHalf.Enabled = false;
        Radio1.Enabled = false;
        Radio2.Enabled = false;
        lblmessage.Text = "";
        hdnFromFor.Value = "";
        hdnlstfromfor.Value = "";
        hdnlsttofor.Value = "";
        txtFromdate.Text = "";
        txtLeaveDays.Text = "";
        txtToDate.Text = "";
        //PopupControlExtender3.Commit(lstLeaveType.SelectedItem.Text);
        htnleavetypeid.Value = "";
        htnleavetypeid.Value = lstLeaveType.SelectedValue;


        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "validateLeaveType(" + htnleavetypeid.Value + ");", true);

    }
    
    protected void txtLeaveType_TextChanged(object sender, EventArgs e)
    {

        txtFromdate.Text = "";
        txtToDate.Text = "";		
		//hdnlstfromfor.Value = "Full Day";
        //hdnlsttofor.Value = "Full Day";
		
		
        //txtLeaveDays.Text = "";
        //lblmessage.Text = "";

        //if (Convert.ToString(lstLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_TO"]).Trim())
        //{
        //    txtToDate.Enabled = false;
        //    txtToFor.Enabled = false;
        //}
        //else
        //{
        //    txtToDate.Enabled = true;
        //    txtToFor.Enabled = true;
        //}
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
            if (Convert.ToString(lstLeaveType.SelectedValue).Trim() == "")
            {
                lblmessage.Text = "Please select Leave Type";
                return;
            }
            if (Convert.ToString(txtFromdate.Text).Trim() == "")
            {
                lblmessage.Text = "Please select From Date";
                return;
            }
            if (Convert.ToString(txtToDate.Text).Trim() == "")
            {
                lblmessage.Text = "Please select To Date";
                return;
            }
            if (Convert.ToString(txtLeaveDays.Text).Trim() == "")
            {
                lblmessage.Text = "Leave Days not calculated.Please try again";
                return;
            }

            if (Convert.ToString(txtReason.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Reason for Leave.";
                return;
            }

            #endregion

            lblmessage.Text = "";

            BindLeaveRequestProperties();

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient();
            StringBuilder strbuild = new StringBuilder();
            String strfileName = "";
            string[] strdate;
            string strfromDate = "";
            string strfromDate_tt = "";
            hdnPLwithSL_succession.Value = "";

            #region date formatting
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
                strfromDate_tt = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[2]);
            }


            #endregion

            #region fileUpload

            
            #endregion

            #region MaxRequestId

            //  dtMaxRequestId = spm.GetMaxRequestId();
            //reqid = (int)dtMaxRequestId.Rows[0]["Request_id"];
            //reqid = reqid + 1;

            #endregion

            #region MethodsCall
            string strleavetype = Convert.ToString(txtLeaveType.Text).Trim();
            /*Int32 inoofDays = 0;
            if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
                inoofDays = Convert.ToInt32(txtLeaveDays.Text);*/
            Decimal inoofDays = 0;
            if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
                inoofDays = Convert.ToDecimal(txtLeaveDays.Text);

            dtApproverEmailIds = spm.GetApproverEmailID(lpm.Emp_Code, hflGrade.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value), strleavetype, inoofDays);

            //DataTable dtIntermediatelist = new DataTable();
            //dtIntermediatelist = spm.GetIntermediateName(lpm.Emp_Code, Convert.ToInt32(hdnleaveconditiontypeid.Value), hflGrade.Value);

            if (dtApproverEmailIds.Rows.Count > 0)
            {


                approveremailaddress = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];
                apprid = (int)dtApproverEmailIds.Rows[0]["APPR_ID"];

                string strfromfor = "";
                string strtofor = "";
                strfromfor = lpm.Leave_From_for;
                strtofor = lpm.Leave_To_For;

                //if (Convert.ToString(strfromfor).Trim() == "First Half")
                //    strtofor = strfromfor;

                if (Convert.ToString(txtFromdate.Text).Trim() == Convert.ToString(txtToDate.Text).Trim())
                {
                    strtofor = strfromfor;
                }

                dtMaxRequestId = spm.InsertLeaveRequest(lpm.Emp_Code, lpm.Leave_Type_id, Convert.ToInt32(hdnleaveconditiontypeid.Value), lpm.Leave_FromDate, strfromfor, lpm.Leave_ToDate, strtofor, lpm.LeaveDays, lpm.Reason, filename);
                Int32 ireqid = 0;
                if (dtMaxRequestId.Rows.Count > 0)
                    ireqid = Convert.ToInt32(dtMaxRequestId.Rows[0]["maxreqid"]);

                if (Convert.ToString(ireqid).Trim() == "0")
                    return;

                spm.InsertApproverRequest(lpm.Approvers_code, apprid, Convert.ToDecimal(dtMaxRequestId.Rows[0]["maxreqid"]));
                String strfromdate_M, strtodate_M;
                strfromdate_M = Convert.ToString(dtMaxRequestId.Rows[0]["fromdate"]).Trim();
                strtodate_M = Convert.ToString(dtMaxRequestId.Rows[0]["todate"]).Trim();

                //if(lstLeaveType.SelectedValue=="3" )
                //    spm.send_mailto_RM_Approver(lpm.Emp_Name, lpm.EmailAddress, approveremailaddress, "Request for Leave", txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, lpm.Leave_FromDate, lpm.Leave_From_for, lpm.Leave_ToDate, lpm.Leave_To_For, Convert.ToString(ConfigurationManager.AppSettings["MLMailId_HR"]).Trim());
                //else if (lstLeaveType.SelectedValue=="4")
                //    spm.send_mailto_RM_Approver(lpm.Emp_Name, lpm.EmailAddress, approveremailaddress, "Request for Leave", txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, lpm.Leave_FromDate, lpm.Leave_From_for, lpm.Leave_ToDate, lpm.Leave_To_For, Convert.ToString(ConfigurationManager.AppSettings["LWPMailId_HR"]).Trim());
                //else

                //spm.send_mailto_RM_Approver(lpm.Emp_Name, lpm.EmailAddress, approveremailaddress, "Request for Leave", txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, lpm.Leave_FromDate, lpm.Leave_From_for, lpm.Leave_ToDate, lpm.Leave_To_For, "");

                String strLeaveRstURL = "";
                strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_LR"]).Trim() + "?reqid=" + Convert.ToDecimal(dtMaxRequestId.Rows[0]["maxreqid"]) + "&itype=0";

                string strInsertmediaterlist = "";
                //strInsertmediaterlist = GetIntermediatesList();

                string strApproverlist = "";
                strApproverlist = GetApprove_RejectList(Convert.ToString(ireqid));

                string strHREmailForCC = "";
                strHREmailForCC = Get_HREmail_ForCC(Convert.ToString(lpm.Emp_Code));

                //spm.send_mailto_RM_Approver(lpm.Emp_Name, lpm.EmailAddress, approveremailaddress, "Request for Leave", txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, Convert.ToString(txtFromdate.Text).Trim(), lpm.Leave_From_for, Convert.ToString(txtToDate.Text).Trim(), lpm.Leave_To_For, "", strLeaveRstURL);               
                spm.send_mailto_RM_Approver(lpm.Emp_Name, lpm.EmailAddress, approveremailaddress, "Request for " + Convert.ToString(txtLeaveType.Text), txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, strfromdate_M, lpm.Leave_From_for, strtodate_M, lpm.Leave_To_For, strHREmailForCC, strLeaveRstURL, strApproverlist, strInsertmediaterlist);


                ClearControls();
                lblmessage.Text = "Leave Request Submitted and Email has been sent to your Reporting Manager for Approval";

                Response.Redirect("~/procs/MyAttend_Req.aspx");
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
    protected void lnkfile_SL_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["SLcertificatesTemp"]).Trim()), lnkfile_SL.Text);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
            Response.WriteFile(strfilepath);
            Response.End();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
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
                hflGrade.Value = lpm.Grade;
                hflEmailAddress.Value = lpm.EmailAddress;
                hflEmpName.Value = lpm.Emp_Name;
                hflEmpDepartment.Value = lpm.department_name;
                hflEmpDesignation.Value = lpm.Designation_name;
                //Call Leave Balance default.
                // IsEnabledFalse(true);                     
                getLeavetypeList();
                //getIntermidateslist();
                getApproverdata();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }
    private void getLeavetypeList()
    {
        dtLeaveTypes = spm.GetLeaveType_OD_WFH(lpm.Emp_Code);
        lstLeaveType.DataSource = dtLeaveTypes;
        lstLeaveType.DataTextField = "Leave_Type_Description";
        lstLeaveType.DataValueField = "Leavetype_id";
        lstLeaveType.DataBind();
    }

    protected void getApproverdata()
    {
        string strleavetype = Convert.ToString(txtLeaveType.Text).Trim();
        /*Int32 inoofDays = 0;
        if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
            inoofDays = Convert.ToInt32(txtLeaveDays.Text);
        */

        Decimal inoofDays = 0;
        if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
            inoofDays = Convert.ToDecimal(txtLeaveDays.Text);
        

        dtApproverEmailIds = spm.GetApproverEmailID(lpm.Emp_Code, hflGrade.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value), strleavetype, inoofDays);
        //IsEnabledFalse (true);
        
        lstApprover.Items.Clear();
        if (dtApproverEmailIds.Rows.Count > 0)
        {
            approveremailaddress = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];
            apprid = (int)dtApproverEmailIds.Rows[0]["APPR_ID"];

            //lstApprover.DataSource = null;
            //lstApprover.DataBind();

            lstApprover.DataSource = dtApproverEmailIds;
            lstApprover.DataTextField = "Emp_Name";
            lstApprover.DataValueField = "APPR_ID";
            lstApprover.DataBind();

            lpm.Approvers_code = (string)dtApproverEmailIds.Rows[0]["A_EMP_CODE"];
            hflapprcode.Value = lpm.Approvers_code;

        }
        else
        {
            lblmessage.Text = "No Reporting Manager Assigned to You, Please contact HR!";
            IsEnabledFalse(false);
        }


    }
    protected string getCancellationmailList()
    {
        string email_ids = "";
        dtApproverEmailIds = spm.GetApproverDetails_Rejection_cancellation(lpm.Emp_Code, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value), "get_PreviousApproverDetails_mail");
        lstApprover.Items.Clear();
        if (dtApproverEmailIds.Rows.Count > 0)
        {
            for (int irow = 0; irow < dtApproverEmailIds.Rows.Count; irow++)
            {
                if (Convert.ToString(email_ids).Trim() == "")
                    email_ids = Convert.ToString(dtApproverEmailIds.Rows[irow]["Emp_Emailaddress"]).Trim();
                else
                    email_ids = email_ids + ";" + Convert.ToString(dtApproverEmailIds.Rows[irow]["Emp_Emailaddress"]).Trim();
            }
        }

        return email_ids;

    }
    //protected void getIntermidateslist()
    //{
    //    dtIntermediate = new DataTable();
    //    dtIntermediate = spm.GetIntermediateName(lpm.Emp_Code, Convert.ToInt32(hdnleaveconditiontypeid.Value), hflGrade.Value);
    //    if (dtIntermediate.Rows.Count > 0)
    //    {
    //        lstIntermediate.DataSource = dtIntermediate;
    //        lstIntermediate.DataTextField = "Emp_Name";
    //        lstIntermediate.DataValueField = "A_EMP_CODE";
    //        //lstIntermediate.DataValueField = "APPR_ID";
    //        lstIntermediate.DataBind();
    //    }

    //}
    protected void IsEnabledFalse(Boolean blnSetControl)
    {
        txtLeaveType.Enabled = blnSetControl;
        lstLeaveType.Enabled = blnSetControl;
        txtFromdate.Enabled = blnSetControl;

        Fullday.Enabled = blnSetControl;
        FirstHalf.Enabled = blnSetControl;
        SecondHalf.Enabled = blnSetControl;
        txtFromfor.Enabled = blnSetControl;
        txtToDate.Enabled = blnSetControl;
        txtToFor.Enabled = blnSetControl;

        txtReason.Enabled = blnSetControl;
        btnSave.Enabled = blnSetControl;
    }
    public void BindLeaveRequestProperties()
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
        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        #endregion


        lpm.Emp_Code = txtEmpCode.Text;
        lpm.LeaveDays = Convert.ToDouble(txtLeaveDays.Text);
        lpm.Leave_Type_id = Convert.ToInt32(lstLeaveType.SelectedValue);
        //lpm.Leave_FromDate = Convert.ToDateTime(txtFromdate.Text);
        lpm.Leave_FromDate = strfromDate;
        lpm.Leave_From_for = txtFromfor.Text;
        // lpm.Leave_ToDate = Convert.ToDateTime(txtToDate.Text);
        lpm.Leave_ToDate = strToDate;
        lpm.Leave_To_For = txtToFor.Text;
        lpm.Reason = txtReason.Text;
        lpm.Grade = hflGrade.Value.ToString();
        lpm.Approvers_code = hflapprcode.Value;
        lpm.appr_id = Convert.ToInt32(lstApprover.SelectedIndex);
        lpm.EmailAddress = hflEmailAddress.Value;
        lpm.Emp_Name = hflEmpName.Value;
        //Convert.ToInt32(lstApprover.SelectedValue);
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

        if (Convert.ToString(txtToDate.Text).Trim() == "")
        {
            lblmessage.Text = "To date cannot be blank";
            return;
        }

        if (Convert.ToString(lnkfile_SL.Text).Trim() != "")
        {
            if (Convert.ToString(hdnPLwithSL_succession.Value).Trim() != "")
            {
                strPLwithSL = "SL";
            }
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
        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        #endregion

        lpm.Emp_Code = txtEmpCode.Text;
        int lvid = 0;

        if (Convert.ToString(lstLeaveType.SelectedValue).Trim() != "")
            lvid = Convert.ToInt32(lstLeaveType.SelectedValue);

        if (lvid == 0)
            return;

        string strfromfor, strtofor;
        strfromfor = hdnlstfromfor.Value;
        strtofor = hdnlsttofor.Value;
        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                if (Convert.ToString(txtFromdate.Text).Trim() == Convert.ToString(txtToDate.Text).Trim())
                {
                    hdnlsttofor.Value = hdnlstfromfor.Value;
                }
            }
        }
        if (Convert.ToString(txtFromdate.Text).Trim() == Convert.ToString(txtToDate.Text).Trim())
        {
            strtofor = strfromfor;
        }

        DataSet dtleavedetails_t = new DataSet();

        if (Convert.ToString(hdnReqid.Value).Trim() != "")
            dtleavedetails_t = spm.GetLeaveDetails(strfromDate, strToDate, lvid, lpm.Emp_Code, hdnReqid.Value, strfromfor, strtofor, Convert.ToString(strPLwithSL).Trim());
        else
            dtleavedetails_t = spm.GetLeaveDetails(strfromDate, strToDate, lvid, lpm.Emp_Code, "", strfromfor, strtofor, Convert.ToString(strPLwithSL).Trim());


        message = "";
        hdnmsg.Value = "";
        if (dtleavedetails_t.Tables[0].Rows.Count > 0)
        {
            //totaldays = (int)dtleavedetails_t.Tables[0].Rows[0]["TotalDays"];
            totaldays = Convert.ToDouble(dtleavedetails_t.Tables[0].Rows[0]["TotalDays"]);
            if (Convert.ToString(dtleavedetails_t.Tables[0].Rows[0]["Message"]).Trim() != "")
                message = (string)dtleavedetails_t.Tables[0].Rows[0]["Message"];

            weekendcount = (int)dtleavedetails_t.Tables[0].Rows[0]["TotalWeekends"];
            publicholiday = (int)dtleavedetails_t.Tables[0].Rows[0]["NoofPublicHoliday"];


        }

        if (Convert.ToString(message).Trim() != "")
        {
            lblmessage.Text = Convert.ToString(message).Trim();
            txtLeaveDays.Text = "";
            hdnleavedays.Value = "0";
            hdnmsg.Value = lblmessage.Text;
            if (Convert.ToString(message).Trim().Contains("Please Extend your Privilege Leave"))
            {
                hdnPLwithSL_succession.Value = "true";
                uploadfile.Enabled = true;
                uploadfile.Attributes["onchange"] = "UploadFile(this)";
            }
            return;
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
            btnSave.Enabled = true;
            if (days >= 1)
            {
                //if from & to date are same
                //check for from for = second half & tofor = first half
                //then 1
                if (Convert.ToString(txtFromdate.Text).Trim() == Convert.ToString(txtToDate.Text).Trim())
                {
                    if (Convert.ToString(hdnlstfromfor.Value).Trim() == "Second Half")
                    {
                        days = days - 0.5;
                    }
                    else if (Convert.ToString(hdnlstfromfor.Value).Trim() == "First Half")
                    {
                        days = days - 0.5;
                    }
                    else if (Convert.ToString(hdnlstfromfor.Value).Trim() == "Full Day")
                    {
                        days = totaldays;
                    }
                }
                else
                {
                    if (Convert.ToString(hdnlstfromfor.Value).Trim() == "Second Half" && Convert.ToString(hdnlsttofor.Value).Trim() == "Full Day")
                    {
                        days = days - 0.5;
                    }
                    else if (Convert.ToString(hdnlstfromfor.Value).Trim() == "First Half")
                    {
                        days = days - 0.5;
                    }
                    else if (Convert.ToString(hdnlstfromfor.Value).Trim() == "Second Half" && Convert.ToString(hdnlsttofor.Value).Trim() == "First Half")
                    {
                        days = days - 1.0;
                    }
                    else if (Convert.ToString(hdnlstfromfor.Value).Trim() == "Full Day" && Convert.ToString(hdnlsttofor.Value).Trim() == "First Half")
                    {
                        days = days - 0.5;
                    }
                }
            }

            txtLeaveDays.Text = days.ToString();
            hdnleavedays.Value = txtLeaveDays.Text;
        }

        #endregion

        #region Check Time of more than One days
        if (lvid == 5 && days > 1)
        {
            lblmessage.Text = "You can not apply more then 1 TO at same time";
            txtLeaveDays.Text = "";
            return;
        }
        #endregion

        #region if Employee Sick levae More than 5 days ----Upload file

        #endregion

        #region calculate  LeaveConditionid
        if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
        {
            hdnleaveconditiontypeid.Value = Convert.ToString(spm.getLeaveConditionTypeId(Convert.ToInt32(lstLeaveType.SelectedValue), Convert.ToDouble(txtLeaveDays.Text)));
            getApproverdata();
            getApproverlist(lpm.Emp_Code, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value));
            //getIntermidateslist();
        }
        #endregion

    }
    protected void lnkfile_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["leavecertificates"]).Trim()), lnkfile.Text);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
            Response.WriteFile(strfilepath);
            Response.End();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    //protected string GetIntermediatesList()
    //{
    //    StringBuilder sbapp = new StringBuilder();
    //    sbapp.Length = 0;
    //    sbapp.Capacity = 0;

    //    //dsapprover = spm.GetApproverStatus(txtempocde_lap.Text, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value));

    //    if (lstIntermediate.Items.Count > 0)
    //    {
    //        sbapp.Append("<table>");
    //        for (int i = 0; i < lstIntermediate.Items.Count; i++)
    //        {
    //            sbapp.Append("<tr>");
    //            //sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
    //            sbapp.Append("<td>" + Convert.ToString(lstIntermediate.Items[i].Text).Trim() + "</td>");
    //            sbapp.Append("</tr>");
    //        }
    //        sbapp.Append("</table>");
    //    }

    //    return Convert.ToString(sbapp);
    //}
    protected void check_ISHR()
    {
        try
        {

            //DataSet dsTrDetails = new DataSet();
            //SqlParameter[] spars = new SqlParameter[4];

            //spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            //spars[0].Value = "get_HRMailds_for_MLLWP";

            //spars[1] = new SqlParameter("@Req_id", SqlDbType.Decimal);
            //spars[1].Value =DBNull.Value;

            //spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            //spars[2].Value = DBNull.Value;

            //spars[3] = new SqlParameter("@ext_appr", SqlDbType.VarChar);

            //if (Convert.ToString(hdnleaveid.Value).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_ML"]).Trim())
            //    spars[3].Value = "HR" + Convert.ToString(hdnleaveType.Value);
            //else if (Convert.ToString(hdnleaveid.Value).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_LWP"]).Trim())
            //    spars[3].Value = "HR" + Convert.ToString(hdnleaveType.Value);
            //else
            //    spars[3].Value = DBNull.Value;


            // dsTrDetails = spm.getDatasetList(spars, "Usp_getDetails_leaves");

            //  if (dsTrDetails.Tables[0].Rows.Count > 0)
            //  {
            //      hdnHRMailId_MLLWP.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_type"]).Trim();
            //  }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    protected void Upload(object sender, EventArgs e)
    {
        if (Convert.ToString(txtFromdate.Text).Trim() != "" && Convert.ToString(txtToDate.Text).Trim() != "")
        {
            if (Convert.ToString(hdnPLwithSL_succession.Value).Trim() != "")
            {
                LeavedaysCalcualation("SL");
            }


            if (Convert.ToString(uploadfile.FileName).Trim() != "")
            {
                #region Insert file on Temp Table

                String strfileName = "";
                string[] strdate;
                string strfromDate = "";


                #region date formatting
                if (Convert.ToString(txtFromdate.Text).Trim() != "")
                {
                    strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                    strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
                }


                #endregion

                #region fileUpload

                if (uploadfile.HasFile)
                {
                    filename = uploadfile.FileName;
                    strfileName = "";
                    strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + uploadfile.FileName;
                    filename = strfileName;
                    uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["SLcertificatesTemp"]).Trim()), strfileName));
                    lnkfile_SL.Text = strfileName;
                    lnkfile_SL.Visible = true;
                }


                #endregion

                DataSet dsTrDetails = new DataSet();
                SqlParameter[] spars = new SqlParameter[2];

                spars[0] = new SqlParameter("@empCode", SqlDbType.VarChar);
                spars[0].Value = Convert.ToString(txtEmpCode.Text).Trim();

                spars[1] = new SqlParameter("@slfilename", SqlDbType.VarChar);
                spars[1].Value = Convert.ToString(filename).Trim();

                dsTrDetails = spm.getDatasetList(spars, "sp_tmpInsert_SLFiles");

                #endregion

            }

        }
    }
    public void ClearControls()
    {
        txtLeaveType.Text = "";
        txtFromdate.Text = "";
        txtToDate.Text = "";
        txtFromfor.Text = "";
        txtToFor.Text = "";
        txtReason.Text = "";
        txtLeaveDays.Text = "";
        lblmessage.Text = "";
        hdnFromFor.Value = "";
        hdnToDate.Value = "";
        hdnlsttofor.Value = "";
        hdnlstfromfor.Value = "";
        hdnOldLeaveCount.Value = "";
        hdnleavedays.Value = "";
        hdnLeaveStatus.Value = "";
        //hflapprcode.Value = "";
        //hflEmailAddress.Value = "";
        //hflEmpDepartment.Value = "";
        //hflEmpDesignation.Value = "";
        //hflEmpName.Value = "";
        //hflGrade.Value = "";
        hflLeavestatus.Value = "";
        //hflstatusid.Value = "";
        //hdnReqid.Value = "";
        hdnlstfromfor.Value = "Full Day";
        hdnlsttofor.Value = "Full Day";
        //hdnlstfromfor.Value = "";
        //hdnlsttofor.Value = "";
        txtFromfor.Text = hdnlstfromfor.Value;
        txtToFor.Text = hdnlsttofor.Value;
        lstLeaveType.SelectedValue = null;

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
    #endregion

    #region Leave Request modification Events
    protected void btnMod_Click(object sender, EventArgs e)
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
            if (Convert.ToString(lstLeaveType.SelectedValue).Trim() == "")
            {
                lblmessage.Text = "Please select Leave Type";
                return;
            }
            if (Convert.ToString(txtFromdate.Text).Trim() == "")
            {
                lblmessage.Text = "Please select From Date";
                return;
            }
            if (Convert.ToString(txtToDate.Text).Trim() == "")
            {
                lblmessage.Text = "Please select To Date";
                return;
            }
            if (Convert.ToString(txtLeaveDays.Text).Trim() == "")
            {
                lblmessage.Text = "Leave Days not calculated.Please try again";
                return;
            }

            if (Convert.ToString(txtReason.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Reason for Leave.";
                return;
            }

            #endregion

            lblmessage.Text = "";

            BindLeaveRequestProperties();

            string[] strdate;
            string strfromDate = "";
            string strToDate = "";
            String strfileName = "";
            String strdtfileName = "";


            #region date formatting
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                strdtfileName = Convert.ToString(strdate[2]) + "_" + Convert.ToString(strdate[1]) + "_" + Convert.ToString(strdate[0]);
            }
            if (Convert.ToString(txtToDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }

            #endregion

            #region fileUpload

            #endregion

            #region get First Approver id
            string strleavetype = Convert.ToString(txtLeaveType.Text).Trim();
           /* Int32 inoofDays = 0;
            if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
                inoofDays = Convert.ToInt32(txtLeaveDays.Text);*/
             Decimal inoofDays = 0;
            if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
                inoofDays = Convert.ToDecimal(txtLeaveDays.Text);

            dtApproverEmailIds = spm.GetApproverEmailID(lpm.Emp_Code, hflGrade.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value), strleavetype, inoofDays);

            if (dtApproverEmailIds.Rows.Count > 0)
            {
                approveremailaddress = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];
                apprid = (int)dtApproverEmailIds.Rows[0]["APPR_ID"];

            }
            #endregion

            string strfromfor = "";
            string strtofor = "";
            strfromfor = lpm.Leave_From_for;
            strtofor = lpm.Leave_To_For;

            //if (Convert.ToString(strfromfor).Trim() == "First Half")
            //    strtofor = strfromfor;


            //if (Convert.ToString(strfromfor).Trim() == "First Half" || Convert.ToString(strfromfor).Trim() == "First Half")
            //{

            if (Convert.ToString(txtFromdate.Text).Trim() == Convert.ToString(txtToDate.Text).Trim())
            {
                strtofor = strfromfor;
            }
            // }

            spm.UpdateLeaveRequest(lpm.Emp_Code, hdnReqid.Value, strfromDate, strfromfor, strToDate, strtofor, Convert.ToDouble(txtLeaveDays.Text), lpm.Reason, Convert.ToDouble(hdnOldLeaveCount.Value), Convert.ToInt32(lstLeaveType.SelectedValue), Convert.ToInt32(hdnleaveconditiontypeid.Value), filename);

            spm.InsertApproverRequest(lpm.Approvers_code, apprid, Convert.ToDecimal(hdnReqid.Value));
            getFromdateTodate_FroEmail();
            //if (lstLeaveType.SelectedValue == "3")
            //    spm.send_mailto_RM_Approver(lpm.Emp_Name, lpm.EmailAddress, approveremailaddress, "Request for Leave", txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, lpm.Leave_FromDate, lpm.Leave_From_for, lpm.Leave_ToDate, lpm.Leave_To_For, Convert.ToString(ConfigurationManager.AppSettings["MLMailId_HR"]).Trim());
            //else if (lstLeaveType.SelectedValue == "4")
            //    spm.send_mailto_RM_Approver(lpm.Emp_Name, lpm.EmailAddress, approveremailaddress, "Request for Leave", txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, lpm.Leave_FromDate, lpm.Leave_From_for, lpm.Leave_ToDate, lpm.Leave_To_For, Convert.ToString(ConfigurationManager.AppSettings["LWPMailId_HR"]).Trim());
            //else
            String strLeaveRstURL = "";
            //strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_LR"]).Trim() + "?reqid=" + Convert.ToDecimal(dtMaxRequestId.Rows[0]["maxreqid"]) + "&itype=0";
            strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_LR"]).Trim() + "?reqid=" + Convert.ToDecimal(hdnReqid.Value) + "&itype=0";


            //spm.send_mailto_RM_Approver(lpm.Emp_Name, lpm.EmailAddress, approveremailaddress, "Request for Leave", txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, lpm.Leave_FromDate, lpm.Leave_From_for, lpm.Leave_ToDate, lpm.Leave_To_For, "", strLeaveRstURL);
            string strInsertmediaterlist = "";
            //strInsertmediaterlist = GetIntermediatesList();

            string strApproverlist = "";
            strApproverlist = GetApprove_RejectList(Convert.ToString(hdnReqid.Value));

            string strHREmailForCC = "";
            strHREmailForCC = Get_HREmail_ForCC(Convert.ToString(lpm.Emp_Code));

            spm.send_mailto_RM_Approver(lpm.Emp_Name, lpm.EmailAddress, approveremailaddress, "Request for " + Convert.ToString(txtLeaveType.Text), txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, Convert.ToString(hdnfrmdate_emial.Value), lpm.Leave_From_for, Convert.ToString(hdntodate_emial.Value), lpm.Leave_To_For, strHREmailForCC, strLeaveRstURL, strApproverlist, strInsertmediaterlist);

            lblmessage.Text = "Leave Request Modified and send for approval";

            Response.Redirect("~/procs/MyAttend_Req.aspx");


        }
        catch (Exception ex)
        { }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }

        idspnreasnforCancellation.Visible = true;
        txtLeaveCancelReason.Visible = true;

        #region Check All Parameters Selected
        lblmessage.Text = "";
        if (Convert.ToString(lstLeaveType.SelectedValue).Trim() == "")
        {
            lblmessage.Text = "Please select Leave Type";
            return;
        }
        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "Please select From Date";
            return;
        }
        if (Convert.ToString(txtToDate.Text).Trim() == "")
        {
            lblmessage.Text = "Please select To Date";
            return;
        }
        if (Convert.ToString(txtLeaveDays.Text).Trim() == "")
        {
            lblmessage.Text = "Leave Days not calculated.Please try again";
            return;
        }
        if (Convert.ToString(txtLeaveCancelReason.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Reason for Leave Cancellation.";
            return;
        }
        #endregion

        BindLeaveRequestProperties();
        string strapprovermails = "";
        strapprovermails = getCancellationmailList();

        string strInsertmediaterlist = "";
        //strInsertmediaterlist = GetIntermediatesList();

        string strApproverlist = "";
        strApproverlist = GetApprove_RejectList(Convert.ToString(hdnReqid.Value));

        //return;
        string strHREmailForCC = "";
        strHREmailForCC = Get_HREmail_ForCC(Convert.ToString(lpm.Emp_Code));

        if (Convert.ToString(hdnLeaveStatus.Value).Trim() == "2")
        {
            string approverEMPCode = "";
            
            #region get First Approver id
            string strleavetype = Convert.ToString(txtLeaveType.Text).Trim();            
            Decimal inoofDays = 0;
            if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
                inoofDays = Convert.ToDecimal(txtLeaveDays.Text);

            dtApproverEmailIds = spm.GetApproverEmailID(lpm.Emp_Code, hflGrade.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value), strleavetype, inoofDays);

            if (dtApproverEmailIds.Rows.Count > 0)
            {
                approveremailaddress = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];
                apprid = (int)dtApproverEmailIds.Rows[0]["APPR_ID"];
                approverEMPCode = (string)dtApproverEmailIds.Rows[0]["A_EMP_CODE"];

            }
            #endregion

            String strLeaveRstURL = "";
            strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_LRC"]).Trim() + "?reqid=" + Convert.ToDecimal(hdnReqid.Value) + "&itype=0";

            spm.Cancel_Approved_LeaveRequest(hdnReqid.Value, lpm.Emp_Code, lpm.LeaveDays, Convert.ToInt32(lstLeaveType.SelectedValue), Convert.ToString(txtLeaveCancelReason.Text).Trim(), approverEMPCode,apprid);
            getFromdateTodate_FroEmail();
            strApproverlist = GetApprove_RejectList_LeaveCancellation(Convert.ToString(hdnReqid.Value));

            spm.send_mailto_CancelLR_Approver(lpm.Emp_Name, lpm.EmailAddress, approveremailaddress, "Cancellation of " + Convert.ToString(txtLeaveType.Text), txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, Convert.ToString(hdnfrmdate_emial.Value), lpm.Leave_From_for, Convert.ToString(hdntodate_emial.Value), lpm.Leave_To_For, strHREmailForCC, strLeaveRstURL, strApproverlist, strInsertmediaterlist);
        }
        else
        {
            spm.CancelLeaveRequest(hdnReqid.Value, lpm.Emp_Code, lpm.LeaveDays, Convert.ToInt32(lstLeaveType.SelectedValue), Convert.ToString(txtLeaveCancelReason.Text).Trim());
            getFromdateTodate_FroEmail();

            //spm.send_mailto_Cancel_Intermediate(hflEmailAddress.Value, strapprovermails, " Cancellation of Leave", txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, lpm.Leave_FromDate, lpm.Leave_From_for, lpm.Leave_ToDate, lpm.Leave_To_For, Convert.ToString(GetApprove_RejectList()).Trim(),lpm.Emp_Name);



            if (Convert.ToString(strapprovermails).Trim() != "")
                strapprovermails = ";" + strHREmailForCC;
            else
                strapprovermails = strHREmailForCC;



            spm.send_mailto_Cancel_Intermediate(hflEmailAddress.Value, strapprovermails, " Cancellation of " + Convert.ToString(txtLeaveType.Text), txtLeaveType.Text, Convert.ToString(txtLeaveDays.Text), Convert.ToString(txtLeaveCancelReason.Text).Trim(), Convert.ToString(hdnfrmdate_emial.Value).Trim(), lpm.Leave_From_for, Convert.ToString(hdntodate_emial.Value).Trim(), lpm.Leave_To_For, strApproverlist, lpm.Emp_Name, strInsertmediaterlist);
        }
        lblmessage.Text = "Leave Cancelation Done and Notification has been send to your Reporting Manager";

        Response.Redirect("~/procs/MyAttend_Req.aspx");
    }


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
                    txtLeaveType.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Leave_Type_Description"]).Trim();
                    //  hdnleaveType.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Leave_Type"]).Trim();
                    txtFromdate.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Leave_FromDate"]).Trim();
                    txtToDate.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Leave_ToDate"]).Trim();
                    txtLeaveDays.Text = Convert.ToString(dsList.Tables[0].Rows[0]["LeaveDays"]).Trim();
                    txtReason.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Reason"]).Trim();
                    //hdnleaveid.Value = Convert.ToString(dsList.Tables[0].Rows[0]["leaveTypeid"]).Trim();
                    hdnOldLeaveCount.Value = Convert.ToString(dsList.Tables[0].Rows[0]["LeaveDays"]).Trim();
                    lstLeaveType.SelectedValue = Convert.ToString(dsList.Tables[0].Rows[0]["leaveTypeid"]).Trim();
                    hdnleaveconditiontypeid.Value = Convert.ToString(dsList.Tables[0].Rows[0]["LeaveConditionTypeid"]).Trim();
                    txtFromfor.Text = Convert.ToString(dsList.Tables[0].Rows[0]["For_From"]).Trim();
                    hdnlstfromfor.Value = Convert.ToString(dsList.Tables[0].Rows[0]["For_From"]).Trim();
                    if (txtFromfor.Text.ToString().Trim().Contains("Full"))
                        Fullday.Checked = true;
                    else if (txtFromfor.Text.ToString().Trim().Contains("First"))
                        FirstHalf.Checked = true;
                    else
                        SecondHalf.Checked = true;

                    hdnfrmdate_emial.Value = Convert.ToString(dsList.Tables[0].Rows[0]["frmdate_email"]).Trim();
                    hdntodate_emial.Value = Convert.ToString(dsList.Tables[0].Rows[0]["todate_email"]).Trim();

                    hdnAppr_status.Value = Convert.ToString(dsList.Tables[0].Rows[0]["lAppr_status"]).Trim();

                    if (Convert.ToString(txtFromdate.Text).Trim() == Convert.ToString(txtToDate.Text).Trim())
                    {
                        hdnlsttofor.Value = "";
                        txtToFor.Text = "";
                    }
                    else
                    {
                        hdnlsttofor.Value = Convert.ToString(dsList.Tables[0].Rows[0]["For_To"]).Trim();
                        txtToFor.Text = Convert.ToString(dsList.Tables[0].Rows[0]["For_To"]).Trim();
                        if (txtToFor.Text.ToString().Trim().Contains("Full"))
                            Radio1.Checked = true;
                        else 
                            Radio2.Checked = true;

                    }
                    hdnLeaveStatus.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Status_id"]).Trim();

                    txtLeaveCancelReason.Text = "";
                    if (Convert.ToString(dsList.Tables[0].Rows[0]["reason_req_cancel"]).Trim() != "")
                    {
                        txtLeaveCancelReason.Text = Convert.ToString(dsList.Tables[0].Rows[0]["reason_req_cancel"]).Trim();
                    }
                    //hflstatusid.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Status_id"]).Trim();
                    //if (Convert.ToString(dsList.Tables[0].Rows[0]["Status_id"]).Trim() == "5")
                    //  hflLeavestatus.Value = "Correction";
                    if (hdnAppr_status.Value == "Approved" && Convert.ToString(hdnLeaveStatus.Value).Trim() == "1")
                    {
                        btnCancel.Visible = false;
                        btnMod.Visible = false;
                        lstLeaveType.Enabled = false;
                        txtFromdate.Enabled = false;
                        txtToDate.Enabled = false;
                        txtReason.Enabled = false;
                        uploadfile.Enabled = false;
                        txtLeaveType.Enabled = false;

                        Fullday.Enabled = false;
                        FirstHalf.Enabled = false;
                        SecondHalf.Enabled = false;
                        Radio1.Enabled = false;
                        Radio2.Enabled = false;
                        txtFromfor.Enabled = false;
                        txtToFor.Enabled = false;
                        txtLeaveCancelReason.Enabled = false;
                    }


                    //if (hflLeavestatus.Value == "" && Convert.ToString(hdnLeaveStatus.Value).Trim() == "1")
                    //{
                    //    btnCancel.Enabled = false;
                    //    btnMod.Enabled = false;
                    //}
                    if (Convert.ToString(hdnLeaveStatus.Value).Trim() == "3" || Convert.ToString(hdnLeaveStatus.Value).Trim() == "4")
                    {
                        btnMod.Visible = false;
                        btnCancel.Visible = false;

                        //btnCancel.Enabled = false;
                        //btnMod.Enabled = false;
                    }
                    else if (Convert.ToString(hdnLeaveStatus.Value).Trim() == "2")
                    {
                        btnCancel.Enabled = true;
                        //btnMod.Enabled = false;
                        btnMod.Visible = false;
                        txtLeaveCancelReason.Enabled = true;
                    }

                    if (Convert.ToString(dsList.Tables[0].Rows[0]["UploadFile"]).Trim() != "")
                    {
                        lnkfile.Text = Convert.ToString(dsList.Tables[0].Rows[0]["UploadFile"]).Trim();
                        lnkfile.Visible = true;
                        uploadfile.Enabled = true;

                    }
                    else
                    {
                        uploadfile.Enabled = false;
                        lnkfile.Text = "";
                        lnkfile.Visible = false;
                    }



                    if (Convert.ToString(dsList.Tables[0].Rows[0]["leaveTypeid"]).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_TO"]).Trim())
                    {
                        txtToDate.Enabled = false;
                        txtToFor.BackColor = Color.FromArgb(235, 235, 228);
                    }
                    if (Convert.ToString(hdnLeaveStatus.Value).Trim() == "3")
                    {

                        idspnreasnforCancellation.Visible = true;
                        txtLeaveCancelReason.Visible = true;
                    }

                    //  if (Convert.ToString(dsList.Tables[0].Rows[0]["ismodify"]).Trim() == "yes")
                    //{
                    //    btnMod.Visible = false;
                    //    btnCancel.Visible = false;
                    //}
                }
            }
            #endregion
        }
        catch (Exception ex)
        { }
    }

    private void getApproverlist(string strempcodes, string reqid, int leavecondtiontypeid)
    {
        DataTable dtapprover = new DataTable();
        string strleavetype = Convert.ToString(txtLeaveType.Text).Trim();

       /* Int32 inoofDays = 0;
        if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
            inoofDays = Convert.ToInt32(txtLeaveDays.Text);*/

         Decimal inoofDays = 0;
        if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
            inoofDays = Convert.ToDecimal(txtLeaveDays.Text);


        dtapprover = spm.GetApproverStatus(strempcodes, reqid, leavecondtiontypeid, strleavetype, inoofDays);

        //lstApprover.Items.Clear();
        //if (dtapprover.Rows.Count > 0)
        //{
        //    lstApprover.DataSource = dtapprover;
        //    lstApprover.DataTextField = "names";
        //    lstApprover.DataValueField = "names";
        //    lstApprover.DataBind();

        //}
        //else
        //{
        //    lblmessage.Text = "There is no request for approver.";
        //}
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();

        if (dtapprover.Rows.Count > 0)
        {
            DgvApprover.DataSource = dtapprover;
            DgvApprover.DataBind();
        }
    }

    private void setenablefalseConttols()
    {
        if (Convert.ToString(hdnLeaveStatus.Value).Trim() == "2")
        {
            btnMod.Enabled = false;
            btnCancel.Enabled = true;
        }
        if (Convert.ToString(hdnLeaveStatus.Value).Trim() != "1" && Convert.ToString(hdnLeaveStatus.Value).Trim() != "5")
        {
            lstLeaveType.Enabled = false;
            txtFromdate.Enabled = false;
            txtToDate.Enabled = false;
            txtReason.Enabled = false;
            uploadfile.Enabled = false;
            txtLeaveType.Enabled = false;

            Fullday.Enabled = false;
            FirstHalf.Enabled = false;
            SecondHalf.Enabled = false;
            Radio1.Enabled = false;
            Radio2.Enabled = false;
            txtFromfor.Enabled = false;
            txtToFor.Enabled = false;
            txtLeaveCancelReason.Enabled = false;
            if (Convert.ToString(hdnLeaveStatus.Value).Trim() == "2")
                txtLeaveCancelReason.Enabled = true;
        }
        if (Convert.ToString(hdnLeaveStatus.Value).Trim() == "4" || Convert.ToString(hdnLeaveStatus.Value).Trim() == "3")
        {
            btnMod.Visible = false;
            btnCancel.Visible = false;
            txtLeaveCancelReason.Enabled = false;
        }
        if (hdnAppr_status.Value == "Approved" && Convert.ToString(hdnLeaveStatus.Value).Trim() == "1")
        {
            btnCancel.Visible = false;
            btnMod.Visible = false;
            lstLeaveType.Enabled = false;
            txtFromdate.Enabled = false;
            txtToDate.Enabled = false;
            txtReason.Enabled = false;
            uploadfile.Enabled = false;
            txtLeaveType.Enabled = false;

            Fullday.Enabled = false;
            FirstHalf.Enabled = false;
            SecondHalf.Enabled = false;
            Radio1.Enabled = false;
            Radio2.Enabled = false;
            txtFromfor.Enabled = false;
            txtToFor.Enabled = false;
            txtLeaveCancelReason.Enabled = false;
        }
        if (Request.QueryString.Count > 0)
        {
            lstLeaveType.Enabled = false;
            txtLeaveType.Enabled = false;

        }
    }

    protected string GetApprove_RejectList(string strReqid)
    {
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        string strleavetype = Convert.ToString(txtLeaveType.Text).Trim();
        /*Int32 inoofDays = 0;
        if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
            inoofDays = Convert.ToInt32(txtLeaveDays.Text);*/

        Decimal inoofDays = 0;
        if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
            inoofDays = Convert.ToDecimal(txtLeaveDays.Text);

        dtAppRej = spm.GetApproverStatus(txtEmpCode.Text, strReqid, Convert.ToInt32(hdnleaveconditiontypeid.Value), strleavetype, inoofDays);
        if (dtAppRej.Rows.Count > 0)
        {
            sbapp.Append("<table>");
            for (int i = 0; i < dtAppRej.Rows.Count; i++)
            {
                sbapp.Append("<tr>");
                sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
                sbapp.Append("</tr>");
            }
            sbapp.Append("</table>");
        }

        return Convert.ToString(sbapp);
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

    
    protected string GetApprove_RejectList_LeaveCancellation(string strReqid)
    {
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        string strleavetype = Convert.ToString(txtLeaveType.Text).Trim();
        /*Int32 inoofDays = 0;
        if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
            inoofDays = Convert.ToInt32(txtLeaveDays.Text);*/

        Decimal inoofDays = 0;
        if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
            inoofDays = Convert.ToDecimal(txtLeaveDays.Text);

        dtAppRej = spm.GetApproverStatus_LeaveCancellation(txtEmpCode.Text, strReqid, Convert.ToInt32(hdnleaveconditiontypeid.Value), strleavetype, inoofDays);
        if (dtAppRej.Rows.Count > 0)
        {
            sbapp.Append("<table>");
            for (int i = 0; i < dtAppRej.Rows.Count; i++)
            {
                sbapp.Append("<tr>");
                sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
                sbapp.Append("</tr>");
            }
            sbapp.Append("</table>");
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
            if (Convert.ToString(txtToDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
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


    #region Not in Use code
 
    #endregion



    protected void Fullday_CheckedChanged(object sender, EventArgs e)
    {
        if (FirstHalf.Checked)
            txtFromfor.Text = "First Half";
        if (Fullday.Checked)
            txtFromfor.Text = "Full Day";
        if (SecondHalf.Checked)
            txtFromfor.Text = "Second Half";

        if (Radio2.Checked)
            txtToFor.Text = "First Half";
        if (Radio1.Checked)
            txtToFor.Text = "Full Day";


        hdnlstfromfor.Value = Convert.ToString(txtFromfor.Text);
        hdnlsttofor.Value = Convert.ToString(txtToFor.Text);
        hdnFromFor.Value = txtFromfor.Text;

        if (Convert.ToString(txtFromfor.Text).Trim() == "First Half")
        {
            txtToDate.Text = txtFromdate.Text;
            txtToDate.Enabled = false;
            Radio1.Enabled = false;
            Radio2.Enabled = false;
        }
        else
        {
            txtToDate.Enabled = true;
            Radio1.Enabled = true;
            Radio2.Enabled = true;
        }
        if (Convert.ToString(txtFromdate.Text).Trim() == Convert.ToString(txtToDate.Text).Trim())
        {
            //txtToDate.Enabled = false;
            Radio1.Enabled = false;
            Radio2.Enabled = false;
            txtToFor.Text = "";
        }


        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            LeavedaysCalcualation("");
        }
        hdnmsg.Value = "0";

    }
}


