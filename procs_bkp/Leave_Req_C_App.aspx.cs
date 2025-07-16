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



public partial class Leave_Req_C_App : System.Web.UI.Page
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
    public DataTable dtEmp, dtleavebal, dtApprover, dtLeaveTypes, dtApproverEmailIds, dtMaxRequestId, dtIntermediate, dtLeaveCalculation, dtHolidays, dtleavedetails;
    DataTable dsapprover = new DataTable();
    public int Leaveid;
    public int leavetype, openbal, avail, rembal, leaveconditionid, totaldays, weekendcount, publicholiday;
    public string filename = "", approveremailaddress, message;
    string holidaydate;


    //Code for Request Details Voew

    String strloginid = "";
    String strempcode = "";
    string Leavestatus = "";
    string IsApprover = "";
    string nxtapprcode;
    string nxtapprname = "";
    int apprid;
    int statusid;

    StringBuilder strbuild = new StringBuilder();

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

             
            lblmessage.Text = "";
       
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

            //Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves.aspx");

            //Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/SampleForm");

             //lpm.Emp_Code =  Convert.ToString(Session["Empcode"]).Trim();
            // txtToDate.Text = hdlDate.Value;
            lblmessage.Visible = true;
            //   
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                Page.SmartNavigation = true;
                hdnReqid.Value = "";
                
                if (Request.QueryString.Count > 0)
                {
                    //s  hdnReqid.Value = "1";
                    hdnReqid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                    hdnhrappType.Value = Convert.ToString(Request.QueryString[1]).Trim();                  

                    
                }
                

               
                if (!Page.IsPostBack)
                {
                    hdnempCode_Lofin.Value = Convert.ToString(Session["Empcode"]).Trim();
                    strempcode  = Convert.ToString(Session["Empcode"]).Trim();
                    hdnLoginUserName.Value = Convert.ToString(Session["emp_loginName"]).Trim();
                    hdnLoginEmpEmail.Value = Convert.ToString(Session["LoginEmpmail"]).Trim();
                    txtLWP_To_PL.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    editform.Visible = true;
                    divbtn.Visible = false;
                    txtFromfor.Enabled = false;
                    txtToFor.Enabled = false;
                    txtToDate.Enabled = false;
                    txtFromdate.Enabled = false;
                    txtReason.Enabled = false; 
                    //   divmsg.Visible = false;
                    lblmessage.Text = "";
                    txtFromfor.Text = "Full Day";
                    txtToFor.Text = "Full Day";

                    idspnLWPTrnsfer_PL.Visible = false;
                    txtLWP_To_PL.Visible = false;


                    checkApprovalStatus_Submit();

                    GetCuurentApprID();
                    getSelectedEmpLeaveDetails_View();
                   
                    check_ISHR();

                    PopulateEmployeeData();

                    lstApprover.SelectedIndex = 0;


                    idspnLWPTrnsfer_PL.Visible = false;
                    txtLWP_To_PL.Visible = false;

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                    txtLeaveType.BackColor = Color.FromArgb(235, 235, 228);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());

            
        }

    }

    protected void GetCuurentApprID()
    {
        int capprid;
        string Actions="";
        DataTable dtCApprID = new DataTable();
        dtCApprID = spm.GetCurrentApprID_LRCancellation(hdnReqid.Value, strempcode);
        capprid = (int)dtCApprID.Rows[0]["APPR_ID"];
        Actions = (string)dtCApprID.Rows[0]["Action"];
        hdnCurrentID.Value = capprid.ToString();

        if (Convert.ToString(hdnCurrentID.Value).Trim() == "")
         {
             lblmessage.Text = "Acton on this REquest not yet taken by other approvals";
             return;
         }
        else if (Convert.ToString(Actions).Trim() != "Pending")
         {
            lblmessage.Text= "You already actioned for this request";
            return;
         }
    }

    protected string GetApprove_RejectList()
    {
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        //dsapprover = spm.GetApproverStatus(txtempocde_lap.Text, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value));
        string strleavetype = Convert.ToString(txtLeaveType.Text).Trim();

       /* Int32 inoofDays = 0;
        if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
            inoofDays = Convert.ToInt32(txtLeaveDays.Text);*/

         Decimal inoofDays = 0;
        if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
            inoofDays = Convert.ToDecimal(txtLeaveDays.Text);

        dtAppRej = spm.GetApproverStatus_Cancellation(txtempocde_lap.Text, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value), strleavetype, inoofDays);
        if (dtAppRej.Rows.Count > 0)
        {
            sbapp.Append("<table>");  
            for (int i = 0;i < dtAppRej.Rows.Count;i++)
            {
                sbapp.Append("<tr>");
                sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
                sbapp.Append("</tr>");
            }
            sbapp.Append("</table>");
        }

        return Convert.ToString (sbapp);
    }

    private void getSelectedEmpLeaveDetails_View()
    {  
        try
        {
            hdnIntermediateEmail.Value = "";
            DataSet dsList = new DataSet();
            dsList = spm.getLeaveRequest_MngEdit_Cancellation(hdnReqid.Value);

            if (dsList.Tables != null)
            {
                if (dsList.Tables[0].Rows.Count > 0)
                {
                    txtempocde_lap.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Code"]).Trim();
                    hflEmpName.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Name"]).Trim();
                    hflEmpDesignation.Value = Convert.ToString(dsList.Tables[0].Rows[0]["DesginationName"]).Trim();
                    hflEmpDepartment.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Department_Name"]).Trim();
                    txtLeaveType.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Leave_Type_Description"]).Trim();
                    hdnleaveType.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Leave_Type"]).Trim();
                    txtFromdate.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Leave_FromDate"]).Trim();
                    txtToDate.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Leave_ToDate"]).Trim();
                    txtLeaveDays.Text = Convert.ToString(dsList.Tables[0].Rows[0]["LeaveDays"]).Trim();
                    txtReason.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Reason"]).Trim();
                    hdnleaveid.Value = Convert.ToString(dsList.Tables[0].Rows[0]["leaveTypeid"]).Trim();
                    hdnOldLeaveCount.Value = Convert.ToString(dsList.Tables[0].Rows[0]["LeaveDays"]).Trim();
                    hdnEmpEmail.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
                    hdnleaveconditiontypeid.Value = Convert.ToString(dsList.Tables[0].Rows[0]["LeaveConditionTypeid"]).Trim();
                    txtFromfor.Text = Convert.ToString(dsList.Tables[0].Rows[0]["For_From"]).Trim();
                    lstFromfor.SelectedValue = Convert.ToString(dsList.Tables[0].Rows[0]["For_From"]).Trim();
                    hflGrade.Value = Convert.ToString(dsList.Tables[0].Rows[0]["grade"]).Trim();
                    hdnfrmdate_emial.Value = Convert.ToString(dsList.Tables[0].Rows[0]["frmdate_email"]).Trim();
                    hdntodate_emial.Value = Convert.ToString(dsList.Tables[0].Rows[0]["todate_email"]).Trim();
                     
                    if (Convert.ToString(dsList.Tables[0].Rows[0]["UploadFile"]).Trim() != "")
                    {
                        lnkfile.Text = Convert.ToString(dsList.Tables[0].Rows[0]["UploadFile"]).Trim();                        
                        lnkfile.Visible = true;
                    }
                    else
                    {
                        lnkfile.Text = "";
                        lnkfile.Visible = false;
                    }

                    if (Convert.ToString(txtFromdate.Text).Trim() == Convert.ToString(txtToDate.Text).Trim())
                    {
                        txtToFor.Text = "";
                        lstTofor.SelectedValue = "";
                    }
                    else
                    {
                        txtToFor.Text = Convert.ToString(dsList.Tables[0].Rows[0]["For_To"]).Trim();
                        lstTofor.SelectedValue = Convert.ToString(dsList.Tables[0].Rows[0]["For_To"]).Trim();
                    }

                    if (Convert.ToString(hdnleaveid.Value).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_LWP"]).Trim())
                    {
                        if (Convert.ToString(hdnhrappType.Value).Trim() == "1")
                        {
                            idspnLWPTrnsfer_PL.Visible = true;
                            txtLWP_To_PL.Visible = true;
                        }
                    }
                }


            }

            GetApproversStatus();
            getIntermidateslist();
            DataTable dsapproverNxt = new DataTable();
            dsapproverNxt = spm.GetNextApproverDetails_LRCancellation(txtempocde_lap.Text, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value));
            if (dsapproverNxt.Rows.Count > 0)
            {
                   apprid = (int)dsapproverNxt.Rows[0]["APPR_ID"];                 
                    nxtapprcode = (string)dsapproverNxt.Rows[0]["A_EMP_CODE"];
                    nxtapprname = (string)dsapproverNxt.Rows[0]["Emp_Name"];
                    approveremailaddress = (string)dsapproverNxt.Rows[0]["Emp_Emailaddress"];
                    hdnnextappcode.Value = nxtapprcode;
                    hdnapprid.Value = apprid.ToString();
                    hflApproverEmail.Value = approveremailaddress;

                    DataTable dtIntermediateEmail = new DataTable();
                    dtIntermediateEmail = spm.GetNextIntermediateName(Convert.ToInt32(hdnCurrentID.Value), hdnReqid.Value, txtempocde_lap.Text);
                    if (dtIntermediateEmail.Rows.Count > 0)
                    {
                        hdnIntermediateEmail.Value = (string)dtIntermediateEmail.Rows[0]["Emp_Emailaddress"];
                    }
            }
            else
            {
                hdnstaus.Value = "Final Approver";
                          //For  Previous approver   
                getPreviousApprovesEmailList();

                getLWPML_HR_ApproverCode("");
                hdnIntermediateEmail.Value = "";
                DataTable dtPreInt = new DataTable();
                dtPreInt = spm.GetPreviousIntermidaterDetails(Convert.ToInt32(hdnCurrentID.Value), txtempocde_lap.Text);
                if (dtPreInt.Rows.Count > 0)
                {

                    for (int i = 0; i < dtPreInt.Rows.Count; i++)
                    {
                        if (Convert.ToString(hdnIntermediateEmail.Value).Trim() == "")
                        {
                            hdnIntermediateEmail.Value = Convert.ToString(dtPreInt.Rows[i]["Emp_Emailaddress"]).Trim();
                        }
                        else
                        {
                            hdnIntermediateEmail.Value += ";" + Convert.ToString(dtPreInt.Rows[i]["Emp_Emailaddress"]).Trim();
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

    private void getPreviousApprovesEmailList()
    {
        DataTable dtPreApp = new DataTable();
        dtPreApp = spm.GetPreviousApproverDetails_LRCancellation(txtempocde_lap.Text, hdnReqid.Value);
        if (dtPreApp.Rows.Count > 0)
        {

            for (int i = 0; i < dtPreApp.Rows.Count; i++)
            {
                if (Convert.ToString(hflApproverEmail.Value).Trim() == "")
                {
                    hdnPreviousApprMails.Value = Convert.ToString(dtPreApp.Rows[i]["Emp_Emailaddress"]).Trim();
                }
                else
                {
                    hdnPreviousApprMails.Value += ";" + Convert.ToString(dtPreApp.Rows[i]["Emp_Emailaddress"]).Trim();
                }
            }
        }
    }

    protected void getIntermidateslist()
    {
        dtIntermediate = new DataTable();
        dtIntermediate = spm.GetIntermediateName(txtempocde_lap.Text, Convert.ToInt32(hdnleaveconditiontypeid.Value), hflGrade.Value);
        if (dtIntermediate.Rows.Count > 0)
        {
            lstIntermediate.DataSource = dtIntermediate;
            lstIntermediate.DataTextField = "Emp_Name";
            lstIntermediate.DataValueField = "A_EMP_CODE";
            
            //lstIntermediate.DataValueField = "APPR_ID";
            lstIntermediate.DataBind();
        }

    }

    protected void GetApproversStatus()
    {
        string strleavetype = Convert.ToString(txtLeaveType.Text).Trim();
        /*Int32 inoofDays = 0;
        if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
            inoofDays = Convert.ToInt32(txtLeaveDays.Text);*/

        Decimal inoofDays = 0;
        if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
            inoofDays = Convert.ToDecimal(txtLeaveDays.Text);

        dsapprover = spm.GetApproverStatus_Cancellation(txtempocde_lap.Text, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value), strleavetype, inoofDays);
        //if (dsapprover.Rows.Count > 0)
        //{
        //    lstApprover.DataSource = dsapprover;
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

        if (dsapprover.Rows.Count > 0)
        {
            DgvApprover.DataSource = dsapprover;
            DgvApprover.DataBind();
        }
    }
    
    public void PopulateEmployeeData()
    {
        try
        {
           
            ////txtEmpCode.Text = lpm.Emp_Code;

            //    txtEmpCode.Enabled = false; 

            //  BindControls();
            dtEmp = spm.GetEmployeeData(txtempocde_lap.Text);
            //txtReason.Text = "This is after function";
            if (dtEmp.Rows.Count > 0)
            {

                //myTextBox.Text = (string) dt.Rows[0]["name"];
                lpm.Emp_Status = (string)dtEmp.Rows[0]["Emp_status"];
                if (lpm.Emp_Status == "Resgined")
                {
                    lblmessage.Text = "You are not allowed to apply for any type of leave sicne your employee status is in resignation";
                    //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Information", "alert('You are not allowed to apply for any type of leave sicne your employee status is in resignation')", true);
                    //Response.Write("You are not allowed to apply for any type of leave sicne your employee status is in resignation");
                }
                else
                {

                    lpm.Emp_Name = (string)dtEmp.Rows[0]["Emp_Name"];
                    lpm.Designation_name = (string)dtEmp.Rows[0]["DesginationName"];
                    lpm.department_name = (string)dtEmp.Rows[0]["Department_Name"];
                    lpm.Grade = (string)dtEmp.Rows[0]["Grade"];
                    lpm.EmailAddress = (string)dtEmp.Rows[0]["Emp_EmailAddress"];

                    txtEmpName.Text = lpm.Emp_Name;
                    txtDepartment.Text = lpm.department_name;
                    txtDesig.Text = lpm.Designation_name;
                    //txtempocde_lap.Text = Convert.ToString(Session["empcode"]).Trim();

                   // txtempocde_lap.Text = lpm.Emp_Code;
                   
                    hflGrade.Value = lpm.Grade;
                    hflEmailAddress.Value = lpm.EmailAddress;
                    hflEmpName.Value = lpm.Emp_Name;
                    hflEmpDepartment.Value = lpm.department_name;
                    hflEmpDesignation.Value = lpm.Designation_name;


                    dtleavebal = spm.GetLeaveBalance(txtempocde_lap.Text);
                    dgLeaveBalance.DataSource = dtleavebal;
                    dgLeaveBalance.DataBind();

                    //dtApprover = spm.GetApproverName(txtEmpCode.Text );

                    //if (dtApprover.Rows.Count > 0)
                    //{

                    //    //lstApprover.DataSource = dtApprover;
                    //    //lstApprover.DataTextField = "Emp_Name";
                    //    //lstApprover.DataValueField = "APPR_ID";
                    //    //lstApprover.DataBind();

                    //    //lpm.Approvers_code = (string)dtApprover.Rows[0]["A_EMP_CODE"];
                    //    //hflapprcode.Value = lpm.Approvers_code;

                    //    ListBox3.DataSource = dtApprover;
                    //    ListBox3.DataTextField = "Emp_Name";
                    //    ListBox3.DataValueField = "APPR_ID";
                    //    ListBox3.DataBind();



                    //}

                    //dtIntermediate = spm.GetIntermediateName(txtEmpCode.Text );
                    //if (dtIntermediate.Rows.Count > 0)
                    //{
                    //    lstIntermediate.DataSource = dtIntermediate;
                    //    lstIntermediate.DataTextField = "Emp_Name";
                    //    lstIntermediate.DataValueField = "APPR_ID";
                    //    lstIntermediate.DataBind();
                    //}



                    //dtLeaveTypes = spm.GetLeaveType();
                    //lstLeaveType.DataSource = dtLeaveTypes;
                    //lstLeaveType.DataTextField = "Leave_Type_Description";
                    //lstLeaveType.DataValueField = "Leavetype_id";
                    //lstLeaveType.DataBind();




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

            throw;
        }
        //BindLeaveRequestProperties();
        //lpm.Emp_Code = txtEmpCode.Text;


    }

    public void BindLeaveRequestProperties()
    {

        //string[] strdate;
        //string strfromDate = "";
        //string strToDate = "";
        //#region date formatting
        //if (Convert.ToString(txtFromdate.Text).Trim() != "")
        //{
        //    strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
        //    strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        //}
        //if (Convert.ToString(txtToDate.Text).Trim() != "")
        //{
        //    strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
        //    strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        //}

        //#endregion


        lpm.Emp_Code = txtEmpCode.Text;
        lpm.LeaveDays = Convert.ToDouble(txtLeaveDays.Text);
      //  lpm.Leave_Type_id = Convert.ToInt32(txtLeaveType.Text);
        //lpm.Leave_FromDate = Convert.ToDateTime(txtFromdate.Text);
        lpm.Leave_FromDate = txtFromdate.Text ;
        lpm.Leave_From_for = txtFromfor.Text;
        // lpm.Leave_ToDate = Convert.ToDateTime(txtToDate.Text);
        lpm.Leave_ToDate = txtToDate.Text ;
        lpm.Leave_To_For = txtToFor.Text;
        lpm.Reason = txtReason.Text;
        lpm.Grade = hflGrade.Value.ToString();
        lpm.Approvers_code = hflapprcode.Value;
      //  lpm.appr_id = Convert.ToInt32(lstApprover.SelectedValue);
        lpm.EmailAddress = hflEmailAddress.Value;
        lpm.Emp_Name = hflEmpName.Value;
        //Convert.ToInt32(lstApprover.SelectedValue);
    }


    protected void txtLeaveDays_TextChanged(object sender, EventArgs e)
    {


    }
    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txtLeaveType.Text).Trim() == "")
        {
            lblmessage.Text = "Please select the leave type";
            txtFromdate.Text = "";
            return;

        }
        else
        {
            lblmessage.Text = "";

        }



    }

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        
    }
    protected void ddlFromFor_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/InboxLeave_Req.aspx?itype=" + hdnhrappType.Value);
    }
    

    protected void lstLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtLeaveType.Text = lstLeaveType.SelectedItem.Text;
        PopupControlExtender3.Commit(lstLeaveType.SelectedItem.Text);

        txtFromdate.Text = "";
        txtToDate.Text = "";
        txtLeaveDays.Text = "";
        lblmessage.Text = "";

        if (Convert.ToString(lstLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_TO"]).Trim())
        {
            txtToDate.Enabled = false;
            txtToFor.Enabled = false;
        }
        else
        {
            txtToDate.Enabled = true;
            txtToFor.Enabled = true;
        }
    }

    protected void lstToFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtToFor.Text = lstTofor.SelectedValue;
        PopupControlExtender2.Commit(lstTofor.SelectedValue);
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
    }
    protected void lstFromfor_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFromfor.Text = lstFromfor.SelectedValue;
        PopupControlExtender1.Commit(lstFromfor.SelectedValue);

        if (Convert.ToString(lstLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_TO"]).Trim() && (txtFromfor.Text == "First Half"))
        {
            lstTofor.Enabled = false;
            hdlDate.Value = txtFromdate.Text;
            txtToDate.Text = txtFromdate.Text;
            txtToDate.Enabled = false;

            txtLeaveDays.Text = " " + 0.5;

        }
        else if (Convert.ToString(lstLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_SL"]).Trim() && (txtFromfor.Text == "Second Half") || (txtFromfor.Text == "Full Day"))
        {
            txtToDate.Enabled = true;

        }
        PopupControlExtender1.Commit(txtToDate.Text);

    }
    protected void txtLeaveType_TextChanged(object sender, EventArgs e)
    {

        //txtFromdate.Text = "";
        //txtToDate.Text = "";
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
    protected string getRejectionCorrectionmailList()
    {
        string email_ids = "";
        dtApproverEmailIds = spm.GetApproverDetails_Rejection_cancellation(txtempocde_lap.Text, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value), "get_ApproverDetails_mail_rejection_correction");
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

    #region Approve Reject Buttons
    protected void btnReject_Click(object sender, EventArgs e)
    {
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }

            if (Convert.ToString((txtComment.Text).Trim()) == "")
            {
                lblmessage.Text = "Please mention the comment before rejecting the leave request";                     
                return;
            }
            BindLeaveRequestProperties();
            string strapprovermails = "";
            //strapprovermails = getRejectionCorrectionmailList();
            strapprovermails = getApproversList_HR();
            string strInsertmediaterlist = "";
            strInsertmediaterlist = GetIntermediatesList();

            //spm.Rrejectleaverequest(hdnReqid.Value, txtempocde_lap.Text, lpm.LeaveDays, Convert.ToInt32(hdnCurrentID.Value), Convert.ToInt32(hdnleaveid.Value),txtComment.Text);
            spm.Reject_Approved_LeaveRequest(hdnReqid.Value, "", 0, 0, Convert.ToString(txtComment.Text).Trim(), hdnempCode_Lofin.Value, Convert.ToInt32(hdnCurrentID.Value));
            spm.send_mail_Rejection_Correction(hflEmailAddress.Value, hdnLoginUserName.Value, "Approved Leave Rejection of " + txtLeaveType.Text + " Request", txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, Convert.ToString(hdnfrmdate_emial.Value).Trim(), lpm.Leave_From_for, Convert.ToString(hdntodate_emial.Value).Trim(), lpm.Leave_To_For, strapprovermails, Convert.ToString(GetApprove_RejectList()).Trim(), hflEmpName.Value, Convert.ToString(strInsertmediaterlist).Trim());

            
            Response.Redirect("~/procs/InboxLeave_Req.aspx?itype=" + hdnhrappType.Value);

        #region not Use
        /*if (Convert.ToString((hflApproverEmail.Value).Trim()) != "")
            {
                if (Convert.ToString((hdnstaus.Value).Trim()) != "")
                {

                    spm.send_Reject_mailto_Pre_Next_Approver(hflEmailAddress.Value, hflApproverEmail.Value, "Rejection of Leave Request of", txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, lpm.Leave_FromDate, lpm.Leave_From_for, lpm.Leave_ToDate, lpm.Leave_To_For, hdnLoginUserName.Value, txtComment.Text, GetApprove_RejectList());
                    spm.send_mailto_Reject_Next_Approver(hflEmailAddress.Value, hdnIntermediateEmail.Value, "Rejection of Leave Request of", txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, lpm.Leave_FromDate, lpm.Leave_From_for, lpm.Leave_ToDate, lpm.Leave_To_For, hdnLoginUserName.Value, txtComment.Text, GetApprove_RejectList());
                    spm.send_mailto_Rej_Requester(hflEmailAddress.Value, hflApproverEmail.Value, "Rejection of Leave Request of", txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, lpm.Leave_FromDate, lpm.Leave_From_for, lpm.Leave_ToDate, lpm.Leave_To_For, hdnLoginUserName.Value, txtComment.Text, GetApprove_RejectList());
                    lblmessage.Text = "Leave Resquest has been Rejected and notofication has been send to the Previous requester and Intermediate/Approver Levels";
                    IsEnabled(false);  
                }
                else
                {
                    spm.send_mailto_Reject_Next_Approver(hflEmailAddress.Value, hflApproverEmail.Value, "Rejection of Leave Request of", txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, lpm.Leave_FromDate, lpm.Leave_From_for, lpm.Leave_ToDate, lpm.Leave_To_For, hdnLoginUserName.Value, txtComment.Text, GetApprove_RejectList());
                    spm.send_mailto_Intermediate_reject(hflEmailAddress.Value, hdnIntermediateEmail.Value, "Rejection of Leave Request of", txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, lpm.Leave_FromDate, lpm.Leave_From_for, lpm.Leave_ToDate, lpm.Leave_To_For);
                    lblmessage.Text = "Leave Resquest has been Rejected ";
                    IsEnabled(false);
                }
   
            }
            else
            {
                spm.send_mailto_Rej_Requester(hflEmailAddress.Value, hflApproverEmail.Value, "Rejection of Leave Request of", txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, lpm.Leave_FromDate, lpm.Leave_From_for, lpm.Leave_ToDate, lpm.Leave_To_For, hdnLoginUserName.Value, txtComment.Text, GetApprove_RejectList());
                lblmessage.Text = "Leave Resquest has been Rejecyed and notofication has been send to the requester";
                IsEnabled(false);
            } 
             */
        #endregion
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
     

           BindLeaveRequestProperties();

           string strapprovermails = "";


           string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
           if (confirmValue != "Yes")
           {
               return;
           }

        if(Convert.ToString(txtLWP_To_PL.Text).Trim()!="")
        {
            //Adjust LWP aginst to PL
            if(Convert.ToDecimal(txtLWP_To_PL.Text)>Convert.ToDecimal(txtLeaveDays.Text))
            {
                txtLWP_To_PL.Text = "";
                lblmessage.Text = "PL count should not be greater than applied LWP days.";
                return;
            }
            //Check Input Validation
            String [] strtmpdate;
            strtmpdate = Convert.ToString(txtLWP_To_PL.Text).Trim().Split('.');
            if (strtmpdate.Length > 2)
            {
                lblmessage.Text = "Please enter correct number to transfer LWP to PL.";
                return;
            }
            AddLWP_againstto_PL();
           
        }

        String strLeaveRstURL = "";
        strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_LRC"]).Trim() + "?reqid=" + Convert.ToDecimal(hdnReqid.Value) + "&itype=0";

      // return;
           if (Convert.ToString((hdnstaus.Value).Trim()) != "")
           {
               //if LR is LWP OR ML the add HR for Approval
             //  if (Convert.ToString(hdnisApprover_TDCOS.Value).Trim() == "Approver")
               getLWPML_HR_ApproverCode(Convert.ToString(hdnleaveid.Value).Trim());
               if (Convert.ToString(hdnisApprover_TDCOS.Value).Trim() != "Approver")
               {
                   if ((Convert.ToString(hdnleaveid.Value).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_LWP"]).Trim()) || (Convert.ToString(hdnleaveid.Value).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_ML"]).Trim()) || (Convert.ToString(hdnleaveid.Value).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_SL"]).Trim()))
                   {
                       if (Convert.ToString(hdnisApprover_TDCOS.Value).Trim() != "Approver")
                       {
                           strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_LRC"]).Trim() + "?reqid=" + Convert.ToDecimal(hdnReqid.Value) + "&itype=1";
                           getLWPML_HR_ApproverCode(Convert.ToString(hdnleaveid.Value).Trim());
                           spm.InsertApproverRequest_Cancellation(hdnnextappcode.Value, Convert.ToInt32(hdnapprid.Value), Convert.ToDecimal(hdnReqid.Value));
                       }
                       spm.UpdateAppRequest_Cancellation(Convert.ToDecimal(hdnReqid.Value), "Approved", txtComment.Text, Convert.ToString(("").Trim()), Convert.ToInt32(hdnCurrentID.Value));
                   }
                   else
                   {
                       spm.UpdateAppRequest_Cancellation(Convert.ToDecimal(hdnReqid.Value), "Approved", txtComment.Text, Convert.ToString((hdnstaus.Value).Trim()), Convert.ToInt32(hdnCurrentID.Value));
                   }

               }
               else
               {
                   spm.UpdateAppRequest_Cancellation(Convert.ToDecimal(hdnReqid.Value), "Approved", txtComment.Text, Convert.ToString((hdnstaus.Value).Trim()), Convert.ToInt32(hdnCurrentID.Value));
               }
           }
           else
           {
               spm.UpdateAppRequest_Cancellation(Convert.ToDecimal(hdnReqid.Value), "Approved", txtComment.Text, Convert.ToString((hdnstaus.Value).Trim()), Convert.ToInt32(hdnCurrentID.Value));
           }

           //if (Convert.ToString(hdnisApprover_TDCOS.Value).Trim() != "Approver")
               strapprovermails = getApproversList_HR();
           //else
               //strapprovermails = getRejectionCorrectionmailList();

            GetApproversStatus();
            string strInsertmediaterlist = "";
            strInsertmediaterlist = GetIntermediatesList();

        

            if (Convert.ToString((hdnstaus.Value).Trim()) == "")
            {
                spm.InsertApproverRequest_Cancellation(hdnnextappcode.Value, Convert.ToInt32(hdnapprid.Value), Convert.ToDecimal(hdnReqid.Value));             
                spm.send_mailto_Next_Approver_CancellationLR(hflEmailAddress.Value, hflApproverEmail.Value, "Cancellation Request for " + Convert.ToString(txtLeaveType.Text), txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, Convert.ToString(hdnfrmdate_emial.Value).Trim(), lpm.Leave_From_for, Convert.ToString(hdntodate_emial.Value).Trim(), lpm.Leave_To_For, GetApprove_RejectList(), hflEmpName.Value, Convert.ToString(strInsertmediaterlist), strLeaveRstURL);
                strapprovermails = getApproversList_HR();                
                spm.send_mailto_Next_Approver_Intermediate_CancellationLR(hflEmailAddress.Value, strapprovermails, "Cancellation Request for " + Convert.ToString(txtLeaveType.Text), txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, Convert.ToString(hdnfrmdate_emial.Value).Trim(), lpm.Leave_From_for, Convert.ToString(hdntodate_emial.Value).Trim(), lpm.Leave_To_For, hdnLoginUserName.Value, txtComment.Text, GetApprove_RejectList(), hflEmpName.Value, Convert.ToString(strInsertmediaterlist), "");

                lblmessage.Text = "Leave Resquest has been approved and and send for next level approvals";
                Response.Redirect("~/procs/InboxLeave_Req.aspx?itype=" + hdnhrappType.Value);

                IsEnabled(false);
            }
            else
            {
                if (Convert.ToString((hdnApproverid_LWPPLEmail.Value).Trim()) != "")
                {
                    if (Convert.ToString((hdnstaus.Value).Trim()) != "")
                    {
                        if (Convert.ToString(txtLWP_To_PL.Text).Trim() != "")
                        {
                            if (Convert.ToString(hdnNewReqid.Value).Trim() != "")
                            {
                                txtLeaveDays.Text = Convert.ToString(Convert.ToDecimal(txtLeaveDays.Text) - (Convert.ToDecimal(txtLWP_To_PL.Text))).Trim();
                                txtFromdate.Text = Convert.ToString(hdnFrmdate_LWP.Value);
                                getFromdateTodate_FroEmail(txtFromdate.Text, txtToDate.Text);
                            }
                        }
                        else
                        {

                            if (Convert.ToString(hflApproverEmail.Value).Trim() == "")
                            {
                                strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_LRC"]).Trim() + "?reqid=" + Convert.ToDecimal(hdnReqid.Value) + "&itype=1";
                                spm.send_mailto_Next_Approver_CancellationLR (hflEmailAddress.Value, hdnApproverid_LWPPLEmail.Value, "Cancellation Request for " + Convert.ToString(txtLeaveType.Text), txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, Convert.ToString(hdnfrmdate_emial.Value).Trim(), lpm.Leave_From_for, Convert.ToString(hdntodate_emial.Value).Trim(), lpm.Leave_To_For, GetApprove_RejectList(), hflEmpName.Value, Convert.ToString(strInsertmediaterlist), strLeaveRstURL);
                            }
                        }

                        spm.send_mailto_Next_Approver_Intermediate_CancellationLR(hflEmailAddress.Value, strapprovermails, "Cancellation Request for " + Convert.ToString(txtLeaveType.Text), txtLeaveType.Text, Convert.ToString(txtLeaveDays.Text), lpm.Reason, Convert.ToString(hdnfrmdate_emial.Value).Trim(), lpm.Leave_From_for, Convert.ToString(hdntodate_emial.Value).Trim(), lpm.Leave_To_For, hdnLoginUserName.Value, txtComment.Text, GetApprove_RejectList(), hflEmpName.Value, Convert.ToString(strInsertmediaterlist), Convert.ToString(strbuild));
                        lblmessage.Text = "Leave Resquest has been approved and notofication has been send to the Requester and Previous Intermediate,Approver Levels";
                        Response.Redirect("~/procs/InboxLeave_Req.aspx?itype=" + hdnhrappType.Value);
                        IsEnabled(false);

                    }
                    else
                    {
                        //IsEnabled(false);
                    }
                }
                else
                {                     
                    strapprovermails = getApproversList_HR();

                    spm.send_mailto_Requester_CancellationLR(hflEmailAddress.Value, strapprovermails, "Cancellation Request for " + Convert.ToString(txtLeaveType.Text), txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, Convert.ToString(hdnfrmdate_emial.Value), lpm.Leave_From_for, Convert.ToString(hdntodate_emial.Value), lpm.Leave_To_For, hdnLoginUserName.Value, txtComment.Text, GetApprove_RejectList(), hflEmpName.Value, strInsertmediaterlist);
                    lblmessage.Text = "Leave Resquest has been approved and notofication has been send to the requester";
                    Response.Redirect("~/procs/InboxLeave_Req.aspx?itype=" + hdnhrappType.Value);

                    IsEnabled(false);

                }

            }
    }

  

    #endregion

    protected void lnkfile_Click(object sender, EventArgs e)
    {
        if(Convert.ToString(lnkfile.Text).Trim()!="")
        {
            //Convert.ToString(ConfigurationManager.AppSettings["leavecertificates"]).Trim()
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["leavecertificates"]).Trim()), lnkfile.Text);
          //  string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
            Response.WriteFile(strfilepath);
            Response.End();

        }
    }

    protected void IsEnabled(Boolean bln)
    {
        btnReject.Enabled = bln;
        btnApprove.Enabled = bln;
        btnCorrection.Enabled = bln;
        txtComment.Enabled = bln;
    }
    protected void check_ISHR()
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
           // spars[0].Value = "CheckHR_SelectedLeaveRqst_isPending";
            spars[0].Value = "CheckHR_SelectedLeaveRqst_isPending_Cancellation";

            spars[1] = new SqlParameter("@Req_id", SqlDbType.Decimal);
            spars[1].Value = hdnReqid.Value;

            spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[2].Value = strempcode;

            spars[3] = new SqlParameter("@ext_appr", SqlDbType.VarChar);
            //if (Convert.ToString(hdnleaveType.Value))
            if (Convert.ToString(hdnleaveid.Value).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_ML"]).Trim())
                spars[3].Value = "HR" + Convert.ToString(hdnleaveType.Value);
            else if (Convert.ToString(hdnleaveid.Value).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_LWP"]).Trim())
                spars[3].Value = "HR" + Convert.ToString(hdnleaveType.Value);
            else if (Convert.ToString(hdnleaveid.Value).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_SL"]).Trim() && Convert.ToDecimal(txtLeaveDays.Text)>5)
                spars[3].Value = "HRML";
            else
                spars[3].Value = Convert.ToString(hdnleaveType.Value);
            
            
            dsTrDetails = spm.getDatasetList(spars, "Usp_getDetails_leaves");
            //Travel Request Count
            hdnisApprover_TDCOS.Value = "Approver";
            if (Convert.ToString(hdnhrappType.Value).Trim() != "1")
            {
                if (dsTrDetails.Tables[0].Rows.Count > 0)
                {
                    hdnisApprover_TDCOS.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_type"]).Trim();
                    //  btnReject.Visible = false;
                    hdnApproverTDCOS_status.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Action"]).Trim();
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected String getApproversList_FinalApprved()
    {
        String email_ids = "";
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_FinalApprover_mail_rejection_correction";

            spars[1] = new SqlParameter("@Req_id", SqlDbType.Decimal);
            spars[1].Value = hdnReqid.Value;

            spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[2].Value = txtempocde_lap.Text;

            spars[3] = new SqlParameter("@Apremp_code", SqlDbType.VarChar);
            spars[3].Value = Convert.ToString(Session["Empcode"]).Trim();

            dsTrDetails = spm.getDatasetList(spars, "Usp_getDetails_leaves");
          
            
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                  
            }
            //Travel Request Count

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                for (int irow = 0; irow < dsTrDetails.Tables[0].Rows.Count; irow++)
                {
                    if (Convert.ToString(email_ids).Trim() == "")
                        email_ids = Convert.ToString(dsTrDetails.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                    else
                        email_ids = email_ids + ";" + Convert.ToString(dsTrDetails.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                }
            }

          
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
        return email_ids;

    }

    protected String getApproversList_HR()
    {
        String email_ids = "";
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_ApproverDetails_mail_rejection_Cancellation";

            spars[1] = new SqlParameter("@Req_id", SqlDbType.Decimal);
            spars[1].Value = hdnReqid.Value;

            spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[2].Value = txtempocde_lap.Text;

            spars[3] = new SqlParameter("@Apremp_code", SqlDbType.VarChar);
            spars[3].Value = Convert.ToString(Session["Empcode"]).Trim();

            dsTrDetails = spm.getDatasetList(spars, "Usp_getDetails_leaves");
          
            
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                  
            }
            //Travel Request Count

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                for (int irow = 0; irow < dsTrDetails.Tables[0].Rows.Count; irow++)
                {
                    if (Convert.ToString(email_ids).Trim() == "")
                        email_ids = Convert.ToString(dsTrDetails.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                    else
                        email_ids = email_ids + ";" + Convert.ToString(dsTrDetails.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                }
            }

          
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
        return email_ids;

    }

    public void getLWPML_HR_ApproverCode(string strtype)
    {

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "LWPML_HREmpCode";

        spars[1] = new SqlParameter("@apprvr_type", SqlDbType.VarChar);
        if (Convert.ToString(hdnleaveid.Value).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_LWP"]).Trim())
            spars[1].Value = "HRLWP";
        else if (Convert.ToString(hdnleaveid.Value).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_ML"]).Trim())
            spars[1].Value = "HRML";
        else if (Convert.ToString(hdnleaveid.Value).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_SL"]).Trim() && Convert.ToDecimal(txtLeaveDays.Text) > 5)
            spars[1].Value = "HRML";
        else            
            spars[1].Value = DBNull.Value;


        spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[2].Value = txtempocde_lap.Text;

        dsTrDetails = spm.getDatasetList(spars, "Usp_getDetails_leaves");

        //Travel Desk Approver Code
        hdnisApprover_TDCOS.Value = "Approver";
        if (Convert.ToString(hdnhrappType.Value).Trim() != "1")
        {
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                hdnnextappcode.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_code"]).Trim();
                hdnapprid.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_id"]).Trim();
                hdnApproverid_LWPPLEmail.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();
                ///.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_code"]).Trim();
                hdnisApprover_TDCOS.Value = "NA";
            }
        }
    }

    protected string GetIntermediatesList()
    {
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        
        //dsapprover = spm.GetApproverStatus(txtempocde_lap.Text, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value));
        
        if (lstIntermediate.Items.Count > 0)
        {
            sbapp.Append("<table>");
            for (int i = 0; i < lstIntermediate.Items.Count; i++)
            {
                sbapp.Append("<tr>");
                //sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
                sbapp.Append("<td>" + Convert.ToString(lstIntermediate.Items[i].Text).Trim() + "</td>");
                sbapp.Append("</tr>");
            }
            sbapp.Append("</table>");
        }

        return Convert.ToString(sbapp);
    }

    private void AddLWP_againstto_PL()
    {
        try
        {
           

            #region date formatting

            string[] strdate;
            string strfromDate = "";
            string strtoDate = "";
            string strtoDate_PL = "";
            string strfromDate_LWP = "";

            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) +"-" + Convert.ToString(strdate[0]);
            }
            if (Convert.ToString(txtToDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                
            }
            get_Fromdate_toDate_For_LWP_PL();
            if (Convert.ToString(hdnTodate_PL.Value).Trim() != "")
            {
                strdate = Convert.ToString(hdnTodate_PL.Value).Trim().Split('/');
                strtoDate_PL = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            if (Convert.ToString(hdnFrmdate_LWP.Value).Trim() != "")
            {
                strdate = Convert.ToString(hdnFrmdate_LWP.Value).Trim().Split('/');
                strfromDate_LWP = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            
            #endregion


            string s_LWPFrmFor, s_LWPToFor, s_newFrmFor, s_newtoFor;
            #region set New From For & Actual To For
            Decimal hr_trnsfrPL = Convert.ToDecimal(txtLWP_To_PL.Text);
            if (Convert.ToString(txtFromfor.Text).Trim() == "Second Half")
            {
                s_newFrmFor = "Second Half";
                if ((hr_trnsfrPL % 1) > 0)
                {
                    s_newtoFor = "Full Day";
                }
                else
                {
                    if (Convert.ToString(hdnTodate_PL.Value).Trim() != "")
                    {
                        strdate = Convert.ToString(Convert.ToDateTime(hdnTodate_PL.Value, new CultureInfo("en-GB")).AddDays(1).ToString("dd/MM/yyyy")).Trim().Split('/');
                        strtoDate_PL = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                        hdnTodate_PL.Value = strtoDate_PL;
                    }
                    s_newtoFor = "First Half";
                }
            }
            else
            {
                s_newFrmFor = "Full Day";
                if ((hr_trnsfrPL % 1) > 0)
                    s_newtoFor = "First Half";
                else
                    s_newtoFor = "Full Day";
            }

            if (s_newtoFor == "First Half")
            {
                s_LWPFrmFor = "Second Half";
                s_LWPToFor = Convert.ToString(txtToFor.Text).Trim();
                if (Convert.ToString(hdnTodate_PL.Value).Trim() != "")
                {
                    strfromDate_LWP = strtoDate_PL;
                }
            }
            else
            {
                s_LWPFrmFor = "Full Day";
                s_LWPToFor = Convert.ToString(txtToFor.Text).Trim();
                if (Convert.ToString(hdnTodate_PL.Value).Trim() != "")
                {
                    strdate = Convert.ToString(Convert.ToDateTime(hdnTodate_PL.Value, new CultureInfo("en-GB")).AddDays(1).ToString("dd/MM/yyyy")).Trim().Split('/');
                    strfromDate_LWP = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                }
            }


            #endregion

           
            DataSet dsTrDetails = new DataSet();
            #region Insert Leave Details
            SqlParameter[] spars = new SqlParameter[19];
            spars[0] = new SqlParameter("@mode", SqlDbType.VarChar);
            spars[0].Value = "INSERT";

            spars[1] = new SqlParameter("@EMP_CODE", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(txtempocde_lap.Text).Trim();

            spars[2] = new SqlParameter("@Appl_Type", SqlDbType.VarChar);
            spars[2].Value = "1";

            spars[3] = new SqlParameter("@Leave_Type", SqlDbType.VarChar);
            spars[3].Value = Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_PL"]).Trim();

            spars[4] = new SqlParameter("@LeaveConditionTypeid", SqlDbType.Int);
            //if (Convert.ToString(hdnleaveconditiontypeid.Value).Trim() != "")
                spars[4].Value = 2;
            //    spars[4].Value = Convert.ToInt32(hdnleaveconditiontypeid.Value);
            //else

            spars[5] = new SqlParameter("@Leave_FromDate", SqlDbType.VarChar);
            spars[5].Value = strfromDate;

            spars[6] = new SqlParameter("@Leave_ToDate", SqlDbType.VarChar);
            spars[6].Value = Convert.ToString(strtoDate_PL);

            spars[7] = new SqlParameter("@Leave_For", SqlDbType.VarChar);
            spars[7].Value = Convert.ToString(txtFromfor.Text).Trim();

            spars[8] = new SqlParameter("@Leave_To", SqlDbType.VarChar);
            spars[8].Value = Convert.ToString(txtToFor.Text).Trim();

            spars[9] = new SqlParameter("@LeaveDays", SqlDbType.Decimal);
            spars[9].Value = Convert.ToDecimal(txtLWP_To_PL.Text);

            spars[10] = new SqlParameter("@Reason", SqlDbType.VarChar);
            //spars[10].Value = "Transfer LWP to PL";
            spars[10].Value = "Transferred " + Convert.ToString(txtLWP_To_PL.Text) + " days of LWP to PL";

            spars[11] = new SqlParameter("@Status_id", SqlDbType.Int);
            spars[11].Value = 2;

            spars[12] = new SqlParameter("@TrsnferLWPTOPL", SqlDbType.VarChar);
            spars[12].Value = "TransferLWPPL";

            spars[13] = new SqlParameter("@FromDate_LWP", SqlDbType.VarChar);
            spars[13].Value = strfromDate_LWP;

            spars[14] = new SqlParameter("@Req_idold", SqlDbType.Decimal);
            spars[14].Value = hdnReqid.Value;

            spars[15] = new SqlParameter("@LeaveDays_LWP", SqlDbType.Float);
            spars[15].Value =Convert.ToDecimal(txtLeaveDays.Text) - Convert.ToDecimal(txtLWP_To_PL.Text);

            spars[16] = new SqlParameter("@LeaveType_LWP", SqlDbType.VarChar);
            spars[16].Value = Convert.ToString(hdnleaveid.Value).Trim();

            spars[17] = new SqlParameter("@Leave_For_LWP", SqlDbType.VarChar);
            spars[17].Value = Convert.ToString(s_LWPFrmFor).Trim();

            spars[18] = new SqlParameter("@Leave_Tofor_LWP", SqlDbType.VarChar);
            spars[18].Value = Convert.ToString(s_LWPToFor).Trim();


            dsTrDetails = spm.getDatasetList(spars, "sp_SaveLeave");
            #endregion

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                hdnNewReqid.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["maxreqid"]).Trim();

                //dtApproverEmailIds = spm.GetApproverEmailID(txtempocde_lap.Text, hflGrade.Value, 2);
                //if (dtApproverEmailIds.Rows.Count > 0)
                //{
                //    hdnApproverid_LWPPL.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["APPR_ID"]);
                //    hdnApproverCode_LWPPL.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["A_EMP_CODE"]);
                //}
                getLWPML_HR_ApproverCode_transferPL(Convert.ToString(hdnleaveid.Value).Trim());
                #region insert Approver details
                SqlParameter[] spars_A = new SqlParameter[6];
                spars_A[0] = new SqlParameter("@QUERYMODE", SqlDbType.VarChar);
                spars_A[0].Value = "INSERT";

                spars_A[1] = new SqlParameter("@REQ_ID", SqlDbType.Decimal);
                spars_A[1].Value = Convert.ToDecimal(hdnNewReqid.Value);

                spars_A[2] = new SqlParameter("@APPR_EMP_CODE", SqlDbType.VarChar);               
                spars_A[2].Value = hdnApproverCode_LWPPL.Value;

                spars_A[3] = new SqlParameter("@Action", SqlDbType.VarChar);
                spars_A[3].Value = "Approved";

                spars_A[4] = new SqlParameter("@SYSTEM_APP", SqlDbType.VarChar);
                spars_A[4].Value = "Y";

                spars_A[5] = new SqlParameter("@APPR_ID", SqlDbType.Int);
               spars_A[5].Value = Convert.ToInt32(hdnApproverid_LWPPL.Value);

                dsTrDetails = spm.getDatasetList(spars_A, "SP_SAVE_APPR_STATUS");

                #endregion


                //spm.send_mailto_Requester(hflEmailAddress.Value, hdnApproverid_LWPPLEmail.Value, "Request for Leave", "Privilege Leave", Convert.ToString(txtLWP_To_PL.Text).Trim(), lpm.Reason, txtFromdate.Text, lpm.Leave_From_for, hdnTodate_PL.Value, lpm.Leave_To_For, hdnLoginUserName.Value, txtComment.Text, "", hflEmpName.Value);
               // spm.send_mailto_Requester(hdnApproverid_LWPPLEmail.Value, hdnApproverid_LWPPLEmail.Value, "Request for Leave", "Privilege Leave", Convert.ToString(txtLWP_To_PL.Text).Trim(), lpm.Reason, txtFromdate.Text, lpm.Leave_From_for, hdnTodate_PL.Value, lpm.Leave_To_For, "", "Transfer LWP to PL", "", hflEmpName.Value);
                //Convert.ToDecimal(txtLeaveDays.Text) - Convert.ToDecimal(txtLWP_To_PL.Text)
                #region Create PL Leave Mail Msg
                getFromdateTodate_FroEmail(txtFromdate.Text, hdnTodate_PL.Value);
                strbuild.Append("<tr><td>Leave Type :</td><td colspan=2> Privilege Leave </td></tr>");
                strbuild.Append("<tr><td>From :</td><td> " + Convert.ToString(hdnfrmdate_emial.Value).Trim() + "</td><td> " + lpm.Leave_From_for + "</td></tr>");
                strbuild.Append("<tr><td>To :</td><td> " + Convert.ToString(hdntodate_emial.Value).Trim()  + "</td><td> " + lpm.Leave_To_For + "</td></tr>");
                strbuild.Append("<tr><td>Leave Days :</td><td> " + Convert.ToString(txtLWP_To_PL.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Remarks :</td><td colspan=2>Transfer LWP to PL</td></tr>");
                #endregion
            }
        }
        catch(Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    private void get_Fromdate_toDate_For_LWP_PL()
    {
        try
        {

            string[] strdate;
            string strfromDate = "";            

            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }

            DataSet dsdates = new DataSet();            
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_FromdateTodate_for_LWPPL";

            spars[1] = new SqlParameter("@Leavedays", SqlDbType.Int);
            spars[1].Value = Convert.ToInt32(Math.Round(Convert.ToDecimal(txtLWP_To_PL.Text), 0, MidpointRounding.AwayFromZero));
            //spars[1].Value = Convert.ToInt32(Math.Round(Convert.ToDecimal(txtLWP_To_PL.Text),2));

            spars[2] = new SqlParameter("@fromdate", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(strfromDate).Trim();

            dsdates = spm.getDatasetList(spars, "Usp_getDetails_leaves");

            if (dsdates.Tables[0].Rows.Count > 0)
            {
                hdnFrmdate_LWP.Value = Convert.ToString(dsdates.Tables[0].Rows[0]["fromdateLWP"]).Trim();
                hdnTodate_PL.Value = Convert.ToString(dsdates.Tables[0].Rows[0]["todatePL"]).Trim();
            }
        }
        catch(Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    public void getLWPML_HR_ApproverCode_transferPL(string strtype)
    {

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "LWPML_HREmpCode";

        spars[1] = new SqlParameter("@apprvr_type", SqlDbType.VarChar);
        if (Convert.ToString(hdnleaveid.Value).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_LWP"]).Trim())
            spars[1].Value = "HRLWP";
        else if (strtype == "3")
            spars[1].Value = "HRML";
        else
            spars[1].Value = DBNull.Value;
        dsTrDetails = spm.getDatasetList(spars, "Usp_getDetails_leaves");

        //Travel Desk Approver Code
        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            hdnApproverCode_LWPPL.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_code"]).Trim();
            hdnApproverid_LWPPL.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_id"]).Trim();
            hdnApproverid_LWPPLEmail.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();
        }

    }

    private void getFromdateTodate_FroEmail(string strfromdate_t,string strtodate_t)
    {
        try
        {

            string[] strdate;
            string strfromDate = "";
            string strToDate = "";

            #region date formatting
            if (Convert.ToString(strfromdate_t).Trim() != "")
            {
                strdate = Convert.ToString(strfromdate_t).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            if (Convert.ToString(strtodate_t).Trim() != "")
            {
                strdate = Convert.ToString(strtodate_t).Trim().Split('/');
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


    private void checkApprovalStatus_Submit()
    {
        try
        {

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "check_LeaveReqAppr_CancellationStatus";

            spars[1] = new SqlParameter("@req_id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDecimal(hdnReqid.Value);

            spars[2] = new SqlParameter("@emp_code_approver", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(hdnempCode_Lofin.Value).Trim();
            
            //spars[2].Value = Convert.ToString(strempcode).Trim();
            ///spars[2].Value = Convert.ToString(Session["Empcode"]).Trim();

            DataTable dtTrDetails = spm.getMobileRemDataList(spars, "Usp_getEmployee_Details_All");

            if (dtTrDetails.Rows.Count == 0)
            {
                //Response.Redirect("~/procs/InboxLeave_Req.aspx?app=" + hdnhrappType.Value);
                Response.Redirect("~/procs/InboxLeave_Req.aspx?app=0");
            }

            if (dtTrDetails.Rows.Count > 0)
            {
                if (Convert.ToString(dtTrDetails.Rows[0]["pvappstatus"]) != "Pending")
                {
                    //Response.Redirect("~/procs/InboxLeave_Req.aspx?app=" + hdnhrappType.Value);
                    Response.Redirect("~/procs/InboxLeave_Req.aspx?app=0");
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }


    }
}
 