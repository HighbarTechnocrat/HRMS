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



public partial class ODApplication_App : System.Web.UI.Page
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
            //   
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                var getPageViewStatus = "";
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    txtRemark.Attributes.Add("maxlength", "500");
                    hdnAppr_id.Value = "0";
                    lstLeaveType.Enabled = false;
                    hdnReqid.Value = "";
                    txtLeaveCancelReason.Enabled = false;
                    if (Request.QueryString.Count > 0)
                    {
                        hdnReqid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        getPageViewStatus = Convert.ToString(Request.QueryString[1]).Trim();
                        txtLeaveCancelReason.Enabled = true;
                    }
                    btnMod.Visible = false;
                    btnCancel.Visible = false;
                    btnback_mng.Visible = false;
                    btnSave.Visible = false; 
                    txtLeaveCancelReason.Visible = false;
                    hdnlstfromfor.Value = "Full Day";
                    hdnlsttofor.Value = "Full Day";
                    editform.Visible = true;
                    divbtn.Visible = false;
                    lblmessage.Text = "";
                    txtFromfor.Text = "Full Day";
                    txtToFor.Text = "Full Day";
                    this.lstApprover.SelectedIndex = 0;
                    hdnleaveconditiontypeid.Value = "13";
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                    txtLeaveType.BackColor = Color.FromArgb(235, 235, 228);
                    PopulateEmployeeData();
                      
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                    txtFromfor.Enabled = false;
                    txtToFor.Enabled = false;
                    Fullday.Enabled = false;
                    FirstHalf.Enabled = false;
                    SecondHalf.Enabled = false;
                    Radio1.Enabled = false;
                    Radio2.Enabled = false;
                    if (Convert.ToString(hdnReqid.Value).Trim() != "")
                    {
                        getLeaveRequest_details_forEdit();
                        var getEmp_Code = Convert.ToString(txtEmpCode.Text);
                        getApproverlist(getEmp_Code, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value));
                        setenablefalseConttols();
                    }
                    if (getPageViewStatus == "app")
                    {
                        btnMod.Visible = true;
                        btnCancel.Visible = true;
                        btnSave.Visible = false;
                    }
                    else if(getPageViewStatus=="arr")
                    {
                        HideRemark.Visible = false;
                        btnSave.Visible = false;
                    }
                    else if (getPageViewStatus == "mod")
                    {
                        btnSave.Visible = true;
                    }
                    btnback_mng.Visible = true;
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
        }


    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Leaves.aspx");
    }
    protected void lstLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {

        txtLeaveType.Text = Convert.ToString(lstLeaveType.SelectedItem.Text).Trim(); //Add Convert String on 17092018 If Selected Leave Type is LWP and From date was clear.
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
            //if (Convert.ToString(txtLeaveDays.Text).Trim() == "")
            //{
            //    lblmessage.Text = "Leave Days not calculated.Please try again";
            //    return;
            //}
            if (Convert.ToString(txtReason.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Reason for Leave.";
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
                strfromDate_tt = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            if (Convert.ToString(txtToDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            #endregion
            DateTime ddt = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

            #region MethodsCall
            string strleavetype = Convert.ToString(txtLeaveType.Text).Trim();

            Decimal inoofDays = 0;
            if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
                inoofDays = Convert.ToDecimal(txtLeaveDays.Text);

            string strfromfor = "";
            string strtofor = "";
            strfromfor = txtFromfor.Text;
            strtofor = txtToFor.Text;
            if (Convert.ToString(txtFromdate.Text).Trim() == Convert.ToString(txtToDate.Text).Trim())
            {
                strtofor = strfromfor;
            }
            if (strfromfor == "")
            {
                lblmessage.Text = "Please select for from.";
                return;
            }
            if (strtofor == "")
            {
                lblmessage.Text = "Please select for to.";
                return;
            }
            double App_Id = 0;
            string qtype = "INSERTWFHApproval";
            string emp_code = Convert.ToString(Session["Empcode"].ToString());
            string createdBy = Convert.ToString(Session["Empcode"].ToString());
            int TypeId = Convert.ToInt32(lstLeaveType.SelectedValue);
            string From_Date = strfromDate_tt;
            string To_date = strtoDate;
            string From_Duration = strfromfor;
            string To_Duration = strtofor;
            string Reason = Convert.ToString(txtReason.Text).Trim();
            string A_Emp_Code = Convert.ToString(hdnA_Emp_Code.Value);
            //string City = Convert.ToString(txt_Citys.Text).Trim();
            //string Client = Convert.ToString(txt_Client.Text).Trim();
            var Appr_id = Convert.ToInt32(hdnAppr_id.Value);
            var d = spm.InsertUpdateWFOWFH(App_Id, Appr_id, qtype, emp_code, createdBy, TypeId, From_Date, To_date, From_Duration, To_Duration, Reason, A_Emp_Code,"","");

            Response.Redirect("~/procs/Attendance.aspx");
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
                //hheadyear.InnerText = " The below reflected leave balance is subject to changes, post closure of 2021 leave reconciliation";
                hflGrade.Value = lpm.Grade;
                hflEmailAddress.Value = lpm.EmailAddress;
                hflEmpName.Value = lpm.Emp_Name;
                hflEmpDepartment.Value = lpm.department_name;
                hflEmpDesignation.Value = lpm.Designation_name;
                //Call Leave Balance default.
                getemployeeLeaveBalance();
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
        dtLeaveTypes = spm.GetLeaveTypeOD(lpm.Emp_Code, "GetWFOWFHTypeOD");
        lstLeaveType.DataSource = dtLeaveTypes;
        lstLeaveType.DataTextField = "TypeName";
        lstLeaveType.DataValueField = "Id";
        lstLeaveType.DataBind();
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
            lblmessage.Text = "Since you are not assigned under any approver you can not apply  for any leave, please contact HR";
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

        //if (Convert.ToString(lnkfile_SL.Text).Trim() != "")
        //{
        //    if (Convert.ToString(hdnPLwithSL_succession.Value).Trim() != "")
        //    {
        //        strPLwithSL = "SL";
        //    }
        //}

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

        if (Convert.ToString(GetLocationforLeaveDaysCalculation()) == "")
        {
            lblmessage.Text = "Cannot apply leave for this dates since, Project change / allocation date is falling between From & To Date of the Leave!";
            txtLeaveDays.Text = "";
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
                if (Convert.ToString(lstLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_SL"]).Trim())
                {
                    lblmessage.Text = Convert.ToString(message).Trim();
                    hdnmsg.Value = lblmessage.Text;
                    //uploadfile.Enabled = true;
                    //uploadfile.Attributes["onchange"] = "UploadFile(this)";
                }
            }
            else
            {
                lblmessage.Text = Convert.ToString(message).Trim();
                txtLeaveDays.Text = "";
                hdnleavedays.Value = "0";
                hdnmsg.Value = lblmessage.Text;
                if (Convert.ToString(message).Trim().Contains("Please Extend your Privilege Leave"))
                {
                    hdnPLwithSL_succession.Value = "true";
                    //uploadfile.Enabled = true;
                    //uploadfile.Attributes["onchange"] = "UploadFile(this)";
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
            lblmessage.Text = "You can not apply more than 1 TO at same time";
            txtLeaveDays.Text = "";
            return;
        }
        #endregion

        //#region if Employee Sick levae More than 5 days ----Upload file
        //if (Convert.ToString(lstLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_SL"]).Trim())
        //{
        //    if (totaldays > 5)
        //    {
        //        uploadfile.Enabled = true;
        //    }
        //    else
        //    {
        //        DateTime ddt = DateTime.ParseExact(strToDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
        //        if (ddt >= DateTime.Today)
        //        {
        //            if (Convert.ToString(uploadfile.FileName).Trim() == "")
        //            {
        //                lblmessage.Text = "Future dated Sick Leave must be accompanied by a Medical Certificate.";
        //                uploadfile.Enabled = true;
        //                return;
        //            }
        //        }
        //        else
        //        {
        //            uploadfile.Enabled = false;
        //        }
        //    }
        //}
        //else
        //{
        //    uploadfile.Enabled = false;
        //}
        //#endregion

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
                txtLeaveDays.Text = "";
                return;
            }
        }
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


            //if (Convert.ToString(uploadfile.FileName).Trim() != "")
            //{
            //    #region Insert file on Temp Table

            //    String strfileName = "";
            //    string[] strdate;
            //    string strfromDate = "";


            //    #region date formatting
            //    if (Convert.ToString(txtFromdate.Text).Trim() != "")
            //    {
            //        strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            //        strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
            //    }


            //    #endregion

            //    #region fileUpload

            //    if (uploadfile.HasFile)
            //    {
            //        filename = uploadfile.FileName;
            //        strfileName = "";
            //        strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + uploadfile.FileName;
            //        filename = strfileName;
            //        uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["SLcertificatesTemp"]).Trim()), strfileName));
            //        lnkfile_SL.Text = strfileName;
            //        lnkfile_SL.Visible = true;
            //    }


            //    #endregion

            //    DataSet dsTrDetails = new DataSet();
            //    SqlParameter[] spars = new SqlParameter[2];

            //    spars[0] = new SqlParameter("@empCode", SqlDbType.VarChar);
            //    spars[0].Value = Convert.ToString(txtEmpCode.Text).Trim();

            //    spars[1] = new SqlParameter("@slfilename", SqlDbType.VarChar);
            //    spars[1].Value = Convert.ToString(filename).Trim();

            //    dsTrDetails = spm.getDatasetList(spars, "sp_tmpInsert_SLFiles");

            //    #endregion

            //}

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
            if (Convert.ToString(txtToDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
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
            if (Convert.ToString(txtToDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }

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
    protected void btnMod_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            lblmessage.Text = "";
            if (Convert.ToString(txtRemark.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter remark.";
                return;
            }
            #region get First Approver id
            var nextAppEmail = "";
            var nextAppAppr_Id = 0;
            var nextAppEmp_Code = "";
            var empCode = txtEmpCode.Text;
            var req_Id = Convert.ToDouble(hdnReqid.Value);
            var getNextApprovar = spm.GetNextApproverDetailsWFH(empCode, req_Id, 13, "get_NextApproverDetails_mail");
            if (getNextApprovar != null)
            {
                if (getNextApprovar.Rows.Count > 0)
                {
                    nextAppEmail = Convert.ToString(getNextApprovar.Rows[0]["Emp_Emailaddress"].ToString());
                    nextAppAppr_Id = Convert.ToInt32(getNextApprovar.Rows[0]["APPR_ID"].ToString());
                    nextAppEmp_Code = Convert.ToString(getNextApprovar.Rows[0]["A_EMP_CODE"].ToString());
                }
            }
            var Appr_id = Convert.ToInt32(hdnAppr_id.Value);
            var Created_By = Convert.ToString(Session["Empcode"].ToString());
            var Reamrk = Convert.ToString(txtRemark.Text).Trim();
            #endregion
            if (nextAppEmail == "")
            {
                var strInsertmediaterlist = "";

                //Final Approval
                spm.INSERTUPDATEAPP(req_Id, Appr_id, "UpdateFinalApprovar", empCode, Created_By, Reamrk, Created_By);
                //spm.send_mailto_RequesterWFH(hflEmailAddress.Value, Emp_EmailAddress.Value, "Request for " + Convert.ToString(txtLeaveType.Text), txtLeaveType.Text, lpm.LeaveDays.ToString(), txtReason.Text, Convert.ToString(txtFromdate.Text).Trim(), txtFromfor.Text, Convert.ToString(txtToDate.Text).Trim(), txtToFor.Text, hflEmpName.Value, txtRemark.Text, GetApprove_RejectList(Convert.ToString(req_Id)), hflEmpName.Value, Convert.ToString(strInsertmediaterlist));
                //spm.send_mailto_RequesterWFH(Emp_EmailAddress.Value, "", "Request for " + Convert.ToString(txtLeaveType.Text), txtLeaveType.Text, lpm.LeaveDays.ToString(), txtReason.Text, Convert.ToString(txtFromdate.Text).Trim(), txtFromfor.Text, Convert.ToString(txtToDate.Text).Trim(), txtToFor.Text, hflEmpName.Value, txtRemark.Text, GetApprove_RejectList(Convert.ToString(req_Id)), hflEmpName.Value, Convert.ToString(strInsertmediaterlist));
                  spm.send_mailto_RequesterWFH(Emp_EmailAddress.Value, "", "Request for " + Convert.ToString(txtLeaveType.Text), txtLeaveType.Text, lpm.LeaveDays.ToString(), txtReason.Text, Convert.ToString(txtFromdate.Text).Trim(), txtFromfor.Text, Convert.ToString(txtToDate.Text).Trim(), txtToFor.Text, hflEmpName.Value, txtRemark.Text, GetApprove_RejectList(Convert.ToString(req_Id)), Emp_Name.Value, Convert.ToString(strInsertmediaterlist));
            }
            else
            {
                //Update Current Approvar  http://localhost/hrms/Default.aspx
                spm.INSERTUPDATEAPP(req_Id, Appr_id, "UpdateApplicationApprovar", empCode, Created_By, Reamrk, Created_By);
                spm.INSERTUPDATEAPP(req_Id, nextAppAppr_Id, "InsertNextApplicationApprovar", empCode, Created_By, Reamrk, nextAppEmp_Code);
                var strInsertmediaterlist = "";
                var strLeaveRstURL = "http://localhost/hrms/procs/ODApplication_App.aspx?reqid=" + req_Id + "&type=app";
                spm.send_mailto_Next_ApproverWFO(Emp_EmailAddress.Value, nextAppEmail, "Request for " + Convert.ToString(txtLeaveType.Text), txtLeaveType.Text, lpm.LeaveDays.ToString(), txtReason.Text, Convert.ToString(txtFromdate.Text).Trim(), txtFromfor.Text, Convert.ToString(txtToDate.Text).Trim(), txtToFor.Text, GetApprove_RejectList(hdnReqid.Value), Emp_Name.Value, Convert.ToString(strInsertmediaterlist), strLeaveRstURL);
                //spm.send_mailto_RM_ODApprover(Convert.ToString(hflEmpName.Value), Convert.ToString(hflEmailAddress.Value), APP_EmailAddress, "Request for " + Convert.ToString(txtLeaveType.Text), txtLeaveType.Text, lpm.LeaveDays.ToString(), Reason, From_Date, From_Duration, To_date, To_Duration, "", strLeaveRstURL, strApproverlist, strInsertmediaterlist);
               // spm.send_mailto_RequesterWFH(hflEmailAddress.Value, Emp_EmailAddress.Value, "Request for " + Convert.ToString(txtLeaveType.Text), txtLeaveType.Text, lpm.LeaveDays.ToString(), txtReason.Text, Convert.ToString(txtFromdate.Text).Trim(), txtFromfor.Text, Convert.ToString(txtToDate.Text).Trim(), txtToFor.Text, hflEmpName.Value, txtRemark.Text, GetApprove_RejectList(Convert.ToString(req_Id)), hflEmpName.Value, Convert.ToString(strInsertmediaterlist));
                //spm.send_mailto_RequesterWFH(Emp_EmailAddress.Value, "", "Request for " + Convert.ToString(txtLeaveType.Text), txtLeaveType.Text, lpm.LeaveDays.ToString(), txtReason.Text, Convert.ToString(txtFromdate.Text).Trim(), txtFromfor.Text, Convert.ToString(txtToDate.Text).Trim(), txtToFor.Text, hflEmpName.Value, txtRemark.Text, GetApprove_RejectList(Convert.ToString(req_Id)), hflEmpName.Value, Convert.ToString(strInsertmediaterlist));
                spm.send_mailto_RequesterWFH(Emp_EmailAddress.Value, "", "Request for " + Convert.ToString(txtLeaveType.Text), txtLeaveType.Text, lpm.LeaveDays.ToString(), txtReason.Text, Convert.ToString(txtFromdate.Text).Trim(), txtFromfor.Text, Convert.ToString(txtToDate.Text).Trim(), txtToFor.Text, hflEmpName.Value, txtRemark.Text, GetApprove_RejectList(Convert.ToString(req_Id)), Emp_Name.Value, Convert.ToString(strInsertmediaterlist));
                
            }
            Response.Redirect("~/procs/InboxODApplication.aspx");
        }
        catch (Exception ex)
        { }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            lblmessage.Text = "";
            if (Convert.ToString(txtRemark.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter remark.";
                return;
            }
            #region get First Approver id
            var nextAppEmail = "";
            var nextAppAppr_Id = 0;
            var nextAppEmp_Code = "";
            var empCode = txtEmpCode.Text;
            var req_Id = Convert.ToDouble(hdnReqid.Value);
            var getNextApprovar = spm.GetNextApproverDetailsWFH(empCode, req_Id, 13, "get_NextApproverDetails_mail");
            if (getNextApprovar != null)
            {
                if (getNextApprovar.Rows.Count > 0)
                {
                    nextAppEmail = Convert.ToString(getNextApprovar.Rows[0]["Emp_Emailaddress"].ToString());
                    nextAppAppr_Id = Convert.ToInt32(getNextApprovar.Rows[0]["APPR_ID"].ToString());
                    nextAppEmp_Code = Convert.ToString(getNextApprovar.Rows[0]["A_EMP_CODE"].ToString());
                }
            }
            var Appr_id = Convert.ToInt32(hdnAppr_id.Value);
            var Created_By = Convert.ToString(Session["Empcode"].ToString());
            var Reamrk = Convert.ToString(txtRemark.Text).Trim();
            #endregion
            var strInsertmediaterlist = "";
            spm.INSERTUPDATEAPP(req_Id, Appr_id, "UpdateRejectApplication", empCode, Created_By, Reamrk, Created_By);
            //spm.send_mail_Rejection_Correction(hflEmailAddress.Value, hdnLoginUserName.Value, "Rejection of " + txtLeaveType.Text + " Request", txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, Convert.ToString(hdnfrmdate_emial.Value).Trim(), lpm.Leave_From_for, Convert.ToString(hdntodate_emial.Value).Trim(), lpm.Leave_To_For, strapprovermails, Convert.ToString(GetApprove_RejectList()).Trim(), hflEmpName.Value, Convert.ToString(strInsertmediaterlist).Trim());
            spm.send_mail_Rejection_CorrectionWFH(Emp_EmailAddress.Value, hflEmpName.Value, "Rejection of " + txtLeaveType.Text + " Request", txtLeaveType.Text, lpm.LeaveDays.ToString(),txtRemark.Text, Convert.ToString(txtFromdate.Text).Trim(), txtFromfor.Text, Convert.ToString(txtToDate.Text).Trim(), txtToFor.Text,"", GetApprove_RejectList(Convert.ToString(req_Id)), Emp_Name.Value, Convert.ToString(strInsertmediaterlist));
            //spm.send_mailto_RequesterWFH(hflEmailAddress.Value, Emp_EmailAddress.Value, "Request for " + Convert.ToString(txtLeaveType.Text), txtLeaveType.Text, lpm.LeaveDays.ToString(), txtReason.Text, Convert.ToString(txtFromdate.Text).Trim(), txtFromfor.Text, Convert.ToString(txtToDate.Text).Trim(), txtToFor.Text, hflEmpName.Value, txtRemark.Text, GetApprove_RejectList(Convert.ToString(req_Id)), hflEmpName.Value, Convert.ToString(strInsertmediaterlist));

            Response.Redirect("~/procs/InboxODApplication.aspx");
        }
        catch (Exception ex)
        { }
    }
    #endregion

    #region Leave Request modification Methods
    private void getLeaveRequest_details_forEdit()
    {
        try
        {

            #region Get Previous/Next Approvers
            DataTable dtlvstatus = new DataTable();
            #endregion
            #region set Employee Leave Request details
            DataSet dsList = new DataSet();
            dsList = spm.getODApplicationDetails(hdnReqid.Value, "WFHApprovalDetailsById");
            if (dsList.Tables != null)
            {

                if (dsList.Tables[0].Rows.Count > 0)
                {
                    txtEmpCode.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Code"]).Trim();
                    // hflEmpName.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Name"]).Trim();
                    Emp_Name.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Name"]).Trim();
                    Emp_EmailAddress.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_EmailAddress"]).Trim();
                    hflEmpDesignation.Value = Convert.ToString(dsList.Tables[0].Rows[0]["DesginationName"]).Trim();
                    hflEmpDepartment.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Department_Name"]).Trim();
                    txtLeaveType.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Leave_Type_Description"]).Trim();
                    txtFromdate.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Leave_FromDate"]).Trim();
                    txtToDate.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Leave_ToDate"]).Trim();
                    txtReason.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Reason"]).Trim();
                    txt_Client.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Client"]).Trim();
                    txt_Citys.Text = Convert.ToString(dsList.Tables[0].Rows[0]["City"]).Trim();
                    lstLeaveType.SelectedValue = Convert.ToString(dsList.Tables[0].Rows[0]["Type_Id"]).Trim();

                    txtEmp_Name.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Name"]).Trim();
                    txtEmp_Desigantion.Text = Convert.ToString(dsList.Tables[0].Rows[0]["DesginationName"]).Trim();
                    txtEmp_Department.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Department_Name"]).Trim(); 
                    txt_Project.Text = Convert.ToString(dsList.Tables[0].Rows[0]["PROJ_NAME_LONG"]).Trim();

                    txtFromfor.Text = Convert.ToString(dsList.Tables[0].Rows[0]["From_Duration"]).Trim();
                    hdnlstfromfor.Value = Convert.ToString(dsList.Tables[0].Rows[0]["From_Duration"]).Trim();
                    if (txtFromfor.Text.ToString().Trim().Contains("Full"))
                        Fullday.Checked = true;
                    else if (txtFromfor.Text.ToString().Trim().Contains("First"))
                        FirstHalf.Checked = true;
                    else
                        SecondHalf.Checked = true;


                    hdnlsttofor.Value = Convert.ToString(dsList.Tables[0].Rows[0]["To_Duration"]).Trim();
                    txtToFor.Text = Convert.ToString(dsList.Tables[0].Rows[0]["To_Duration"]).Trim();
                    if (txtToFor.Text.ToString().Trim().Contains("Full"))
                        Radio1.Checked = true;
                    else
                        Radio2.Checked = true;

                }
            }
            #endregion
        }
        catch (Exception ex)
        { }
    }
    #endregion
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

        dtapprover = spm.GetApproverStatusWFO(strempcodes, reqid, leavecondtiontypeid, strleavetype, inoofDays);

        DgvApprover.DataSource = null;
        DgvApprover.DataBind();

        if (dtapprover.Rows.Count > 0)
        {
            hdnA_Emp_Code.Value = Convert.ToString(dtapprover.Rows[0]["A_EMP_CODE"].ToString());
            hdnAppr_id.Value = Convert.ToString(dtapprover.Rows[0]["APPR_ID"].ToString());
            DgvApprover.DataSource = dtapprover;
            DgvApprover.DataBind();
        }
    }

    private void setenablefalseConttols()
    {
        btnCancel.Visible = false;
        btnMod.Visible = false;
        lstLeaveType.Enabled = false;
        txtFromdate.Enabled = false;
        txtToDate.Enabled = false;
        txtReason.Enabled = false;
        txt_Citys.Enabled = false;
        txt_Client.Enabled = false;
        txtToFor.Enabled = false;
        txtFromfor.Enabled = false;
        //uploadfile.Enabled = false;
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

        dtAppRej = spm.GetApproverStatusWFO(txtEmpCode.Text, strReqid, Convert.ToInt32(hdnleaveconditiontypeid.Value), strleavetype, inoofDays);
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
        hdnmsg.Value = "0";

    }

    protected void btnback_mng_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Count > 0)
            {
                hdnReqid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                var getPageViewStatus = Convert.ToString(Request.QueryString[1]).Trim();
                if (getPageViewStatus == "app")
                {
                    Response.Redirect("~/procs/InboxODApplication.aspx");
                }
                else
                {
                    Response.Redirect("~/procs/MyODProcessRequest.aspx");
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnCancelAPP_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            lblmessage.Text = "";
            if (Convert.ToString(txtRemark.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter remark.";
                return;
            }
            #region get First Approver id
            var nextAppEmail = "";
            var nextAppAppr_Id = 0;
            var nextAppEmp_Code = "";
            var empCode = txtEmpCode.Text;
            var req_Id = Convert.ToDouble(hdnReqid.Value);
            var getNextApprovar = spm.GetNextApproverDetailsWFH(empCode, req_Id, 13, "get_NextApproverDetails_mail");
            if (getNextApprovar != null)
            {
                if (getNextApprovar.Rows.Count > 0)
                {
                    nextAppEmail = Convert.ToString(getNextApprovar.Rows[0]["Emp_Emailaddress"].ToString());
                    nextAppAppr_Id = Convert.ToInt32(getNextApprovar.Rows[0]["APPR_ID"].ToString());
                    nextAppEmp_Code = Convert.ToString(getNextApprovar.Rows[0]["A_EMP_CODE"].ToString());
                }
            }
            var Appr_id = Convert.ToInt32(hdnAppr_id.Value);
            var Created_By = Convert.ToString(Session["Empcode"].ToString());
            var Reamrk = Convert.ToString(txtRemark.Text).Trim();
            #endregion
           
            spm.INSERTUPDATEAPP(req_Id, Appr_id, "CancelledApplication", empCode, Created_By, Reamrk, nextAppEmp_Code);

            Response.Redirect("~/procs/Attendance.aspx");
        }
        catch (Exception ex)
        { }
    }
}
