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



public partial class InboxLeave_Req : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    public string filename = "", approveremailaddress, message;
    string Leavestatus = "";
    string IsApprover = "";
    string nxtapprcode;
    string nxtapprname = "";
    int apprid;
    int statusid;

    SP_Methods spm = new SP_Methods();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    DataTable dtLeaveDetails;
    DataSet dsLeaveRequst;
    string strempcode = "";
    StringBuilder strbuild = new StringBuilder();
    DataTable dsapprover = new DataTable();
    public DataTable dtEmp, dtleavebal, dtApprover, dtLeaveTypes, dtApproverEmailIds, dtMaxRequestId, dtIntermediate, dtLeaveCalculation, dtHolidays, dtleavedetails;
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


            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                hdnEmpCode.Value = Convert.ToString(Session["Empcode"]);
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    hdnLoginUserName.Value = Convert.ToString(Session["emp_loginName"]).Trim();
                    hdnhrappType.Value = "0";
                    // PopulateEmployeeLeaveData();
                    if (Request.QueryString.Count > 0)
                        hdnhrappType.Value = Convert.ToString(Request.QueryString[0]);



                    InboxLeaveReqstList();

                    getSelectedEmpLeaveDetails_View();
                    if (Convert.ToString(hdnhrappType.Value) == "1")
                        checkHR_Inbox();

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    private void getSelectedEmpLeaveDetails_View()
    {
        try
        {
            hdnIntermediateEmail.Value = "";
            DataSet dsList = new DataSet();
            dsList = spm.getLeaveRequest_MngEdit(hdnReqid.Value);

            if (dsList.Tables != null)
            {
                if (dsList.Tables[0].Rows.Count > 0)
                {
                    var emp_code = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Code"]).Trim();
                    hflEmpName.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Name"]).Trim();
                    var emp_des = Convert.ToString(dsList.Tables[0].Rows[0]["DesginationName"]).Trim();
                    var emp_dep = Convert.ToString(dsList.Tables[0].Rows[0]["Department_Name"]).Trim();
                    var emp_leavetype = Convert.ToString(dsList.Tables[0].Rows[0]["Leave_Type_Description"]).Trim();
                    hdnleaveType.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Leave_Type"]).Trim();
                    var fromdate = Convert.ToString(dsList.Tables[0].Rows[0]["Leave_FromDate"]).Trim();
                    var todate = Convert.ToString(dsList.Tables[0].Rows[0]["Leave_ToDate"]).Trim();
                    var leavedays = Convert.ToString(dsList.Tables[0].Rows[0]["LeaveDays"]).Trim();
                    var reason = Convert.ToString(dsList.Tables[0].Rows[0]["Reason"]).Trim();
                    var leavetypeid = Convert.ToString(dsList.Tables[0].Rows[0]["leaveTypeid"]).Trim();
                    hdnOldLeaveCount.Value = Convert.ToString(dsList.Tables[0].Rows[0]["LeaveDays"]).Trim();
                    hdnEmpEmail.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
                    hdnleaveconditiontypeid.Value = Convert.ToString(dsList.Tables[0].Rows[0]["LeaveConditionTypeid"]).Trim();
                    var forfrom = Convert.ToString(dsList.Tables[0].Rows[0]["For_From"]).Trim();
                    var forfrom_to = Convert.ToString(dsList.Tables[0].Rows[0]["For_From"]).Trim();
                    hflGrade.Value = Convert.ToString(dsList.Tables[0].Rows[0]["grade"]).Trim();
                    hdnfrmdate_emial.Value = Convert.ToString(dsList.Tables[0].Rows[0]["frmdate_email"]).Trim();
                    hdntodate_emial.Value = Convert.ToString(dsList.Tables[0].Rows[0]["todate_email"]).Trim();

                }


            }
            GetApproversStatus();
            GetApproversStatuss();

            int selectedIndex = gvMngLeaveRqstList.SelectedIndex;
            if (selectedIndex >= 0)
            {
                hdnReqid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[selectedIndex].Values[0]).Trim();
            }
            DataSet dsTrDetail = new DataSet();
            SqlParameter[] sparss = new SqlParameter[2];

            sparss[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            sparss[0].Value = "approve_leave";

            sparss[1] = new SqlParameter("@Req_id", SqlDbType.Decimal);
            sparss[1].Value = Convert.ToDecimal(hdnReqid.Value);

            dsTrDetail = spm.getDatasetList(sparss, "Usp_getDetails_leaves");

            var emp_codes = Convert.ToString(dsTrDetail.Tables[0].Rows[0]["Emp_Code"]).Trim();

            DataTable dsapproverNxt = new DataTable();
            dsapproverNxt = spm.GetNextApproverDetails(emp_codes, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value));
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
                dtIntermediateEmail = spm.GetNextIntermediateName(Convert.ToInt32(hdnCurrentID.Value), hdnReqid.Value, emp_codes);
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
                dtPreInt = spm.GetPreviousIntermidaterDetails(Convert.ToInt32(hdnCurrentID.Value), emp_codes);
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
        int selectedIndex = gvMngLeaveRqstList.SelectedIndex;
        if (selectedIndex >= 0)
        {
            hdnReqid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[selectedIndex].Values[0]).Trim();
        }
        DataSet dsTrDetail = new DataSet();
        SqlParameter[] sparss = new SqlParameter[2];

        sparss[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        sparss[0].Value = "approve_leave";

        sparss[1] = new SqlParameter("@Req_id", SqlDbType.Decimal);
        sparss[1].Value = Convert.ToDecimal(hdnReqid.Value);

        dsTrDetail = spm.getDatasetList(sparss, "Usp_getDetails_leaves");

        var emp_code = Convert.ToString(dsTrDetail.Tables[0].Rows[0]["Emp_Code"]).Trim();

        DataTable dtPreApp = new DataTable();
        dtPreApp = spm.GetPreviousApproverDetails(emp_code, hdnReqid.Value);
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
        else if (Convert.ToString(hdnleaveid.Value).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_SL"]).Trim() && Convert.ToDecimal(HDLeaveDays.Value) > 5)
            spars[1].Value = "HRML";
        else
            spars[1].Value = DBNull.Value;

        spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[2].Value = HDEmpCode.Value;

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
                hflApproverEmail.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();
                hdnisApprover_TDCOS.Value = "NA";
            }
        }
    }

    protected String getApproversList_HR()
    {
        String email_ids = "";
        try
        {
            int selectedIndex = gvMngLeaveRqstList.SelectedIndex;
            if (selectedIndex >= 0)
            {
                hdnReqid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[selectedIndex].Values[0]).Trim();
            }
            DataSet dsTrDetail = new DataSet();
            SqlParameter[] sparss = new SqlParameter[2];

            sparss[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            sparss[0].Value = "approve_leave";

            sparss[1] = new SqlParameter("@Req_id", SqlDbType.Decimal);
            sparss[1].Value = Convert.ToDecimal(hdnReqid.Value);

            dsTrDetail = spm.getDatasetList(sparss, "Usp_getDetails_leaves");

            var emp_code = Convert.ToString(dsTrDetail.Tables[0].Rows[0]["Emp_Code"]).Trim();

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_ApproverDetails_mail_rejection_correction";

            spars[1] = new SqlParameter("@Req_id", SqlDbType.Decimal);
            spars[1].Value = hdnReqid.Value;

            spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[2].Value = emp_code;

            spars[3] = new SqlParameter("@Apremp_code", SqlDbType.VarChar);
            spars[3].Value = Convert.ToString(Session["Empcode"]).Trim();

            dsTrDetails = spm.getDatasetList(spars, "Usp_getDetails_leaves");


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

   
    protected void GetApproversStatus()
    {
        int selectedIndex = gvMngLeaveRqstList.SelectedIndex;
        if (selectedIndex >= 0)
        {
            hdnReqid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[selectedIndex].Values[0]).Trim();
        }


        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "approve_leave";

        spars[1] = new SqlParameter("@Req_id", SqlDbType.Decimal);
        spars[1].Value = Convert.ToDecimal(hdnReqid.Value);

        dsTrDetails = spm.getDatasetList(spars, "Usp_getDetails_leaves");

        var emp_code = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Emp_Code"]).Trim();
        var leavetype = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Leave_Type"]).Trim();
        var leavedays = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["LeaveDays"]).Trim();
        var hdnleaveconditiontypeid = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["LeaveConditionTypeid"]).Trim();


        string strleavetype = Convert.ToString(leavetype).Trim();
        /*Int32 inoofDays = 0;
        if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
            inoofDays = Convert.ToInt32(txtLeaveDays.Text);*/

        Decimal inoofDays = 0;
        if (Convert.ToString(leavedays).Trim() != "")
            inoofDays = Convert.ToDecimal(leavedays);

        dsapprover = spm.GetApproverStatus(emp_code, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid), strleavetype, inoofDays);
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

    protected string GetApprove_RejectLists()
    {

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "approve_leave";

        spars[1] = new SqlParameter("@Req_id", SqlDbType.Decimal);
        spars[1].Value = Convert.ToDecimal(hdnReqid.Value);

        dsTrDetails = spm.getDatasetList(spars, "Usp_getDetails_leaves");

        var emp_code = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Emp_Code"]).Trim();
        var leavetype = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Leave_Type"]).Trim();
        var leavedays = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["LeaveDays"]).Trim();
        var hdnleaveconditiontypeid = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["LeaveConditionTypeid"]).Trim();
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        string strleavetype = Convert.ToString(leavetype).Trim();

        Decimal inoofDays = 0;
        if (Convert.ToString(leavedays).Trim() != "")
            inoofDays = Convert.ToDecimal(leavedays);

        dtAppRej = spm.GetApproverStatus(emp_code, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid), strleavetype, inoofDays);
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
 
    protected string GetApprove_RejectList(string emp_code,string sReqid,Int32 leaveconditionTypeid,string strleavetype, decimal inoofDays)
    {
        

        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
     
 
 
        dtAppRej = spm.GetApproverStatus(emp_code,hdnReqid.Value, leaveconditionTypeid, strleavetype, inoofDays);
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

    protected void btnIn_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }

        foreach (GridViewRow row in gvMngLeaveRqstList.Rows)
        {
            CheckBox CHK_clearancefromSubmitted = (CheckBox)row.FindControl("CHK_clearancefromSubmitted");
            if (CHK_clearancefromSubmitted.Checked == true)
            {
                hdnReqid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
                
                String strLeaveRstURL = "";
                strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_LR_S"]).Trim() + "?reqid=" + Convert.ToDecimal(hdnReqid.Value) + "&itype=0";

                DataSet dsTrDetails = new DataSet();
                SqlParameter[] spars = new SqlParameter[3];

                spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                spars[0].Value = "approve_leave";

                spars[1] = new SqlParameter("@Req_id", SqlDbType.Decimal);
                spars[1].Value = Convert.ToDecimal(hdnReqid.Value);

                spars[2] = new SqlParameter("@Apremp_code", SqlDbType.VarChar);
                spars[2].Value = Convert.ToString(hdnEmpCode.Value);

                dsTrDetails = spm.getDatasetList(spars, "Usp_getDetails_leaves");
                Decimal leavedays = 0;

                string StrrequesterCCList = "";
                if (dsTrDetails.Tables[1].Rows.Count > 0)
                {
                    StrrequesterCCList = dsTrDetails.Tables[1].Rows[0]["CCEmailAddress"].ToString();
                }

                if (dsTrDetails.Tables.Count>0)
                {
                    var emp_code = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Emp_Code"]).Trim();
                     HDEmpCode.Value= Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Emp_Code"]).Trim();
                    var leavetype = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Leave_Type"]).Trim();
                    var fromdate = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Leave_FromDate"]).Trim();
                    var todate = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Leave_ToDate"]).Trim();
                    var Reason = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Reason"]).Trim();
                    var Emp_Emailaddress = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
                    var Emp_Name = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Emp_Name"]).Trim();
                    var forfrom = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["For_From"]).Trim();
                    var forto = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["For_To"]).Trim();
                    var CurrentApproverEmpCode = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["aaproverEmpCode"]).Trim();
                    var CurrentApproverName = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["aaproverName"]).Trim();
                    var CurrentApprover_emailArdress = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["aaproverEmailId"]).Trim();
                    var empgrade = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["grade"]).Trim();
                    var Leave_TypeID = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Leave_TypeID"]).Trim();

                    if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["LeaveDays"]).Trim() != "")
                        leavedays = Convert.ToDecimal(dsTrDetails.Tables[0].Rows[0]["LeaveDays"]);
                        HDLeaveDays.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["LeaveDays"]);

                    hdnCurrentID.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Appr_id"]).Trim(); 
                    hdnleaveconditiontypeid.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["LeaveConditionTypeid"]).Trim();

                    DataTable dsapproverNxt = new DataTable();
                    dsapproverNxt = spm.GetNextApproverDetails(emp_code, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value));
                    hdnnextappcode.Value = "";
                    if (dsapproverNxt.Rows.Count > 0)
                    {
                        apprid = (int)dsapproverNxt.Rows[0]["APPR_ID"];
                        nxtapprcode = (string)dsapproverNxt.Rows[0]["A_EMP_CODE"];
                        nxtapprname = (string)dsapproverNxt.Rows[0]["Emp_Name"];
                        approveremailaddress = (string)dsapproverNxt.Rows[0]["Emp_Emailaddress"];
                        hdnnextappcode.Value = nxtapprcode;
                        hdnapprid.Value = apprid.ToString();
                        hflApproverEmail.Value = approveremailaddress;
                        //insert Next Approver & send mail 
                    }
                    else
                    {
                        if (Leave_TypeID == "3")
                        {
                            hdnleaveid.Value = Leave_TypeID;
                            getLWPML_HR_ApproverCode(Convert.ToString(hdnleaveid.Value).Trim());
                        }
                    }

                    String strapprovermails = "";
                    string strInsertmediaterlist = "";
                    string sapproverlist = "";

                    if (Convert.ToString(hdnnextappcode.Value).Trim() != "")
                    {

                        spm.InsertApproverRequest(hdnnextappcode.Value, Convert.ToInt32(hdnapprid.Value), Convert.ToDecimal(hdnReqid.Value));
                        spm.UpdateAppRequest(Convert.ToDecimal(hdnReqid.Value), "Approved", "", "", Convert.ToInt32(hdnCurrentID.Value));
                        sapproverlist = GetApprove_RejectList(emp_code, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value), leavetype, Convert.ToDecimal(leavedays));
                        spm.send_mailto_Next_Approver(Emp_Emailaddress, hflApproverEmail.Value, "Request for " + Convert.ToString(leavetype), leavetype, leavedays.ToString(), Reason, Convert.ToString(fromdate).Trim(), forfrom, Convert.ToString(todate).Trim(), forto, sapproverlist, Emp_Name, Convert.ToString(strInsertmediaterlist), strLeaveRstURL);
                     }
                    else
                    {
                        spm.UpdateAppRequest(Convert.ToDecimal(hdnReqid.Value), "Approved", "", "Final Approved", Convert.ToInt32(hdnCurrentID.Value));
                    }
                    sapproverlist = GetApprove_RejectList(emp_code, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value), leavetype, Convert.ToDecimal(leavedays));
                    var getPRM = spm.GetPRMForLeaveDetails("CheckIsPRMMultiple", Convert.ToString(HDEmpCode.Value));
                    if (getPRM != null)
                    {
                        if (getPRM.Rows.Count > 0)
                        {
                            foreach (DataRow item in getPRM.Rows)
                            {
                                if (StrrequesterCCList == "")
                                {
                                    StrrequesterCCList = Convert.ToString(item["Emp_Emailaddress"]);
                                }
                                else
                                {
                                    StrrequesterCCList = StrrequesterCCList + ";" + Convert.ToString(item["Emp_Emailaddress"]);
                                }
                            }
                        }
                    }

                    spm.send_mailto_Requester(Emp_Emailaddress, StrrequesterCCList, "Request for " + Convert.ToString(leavetype), leavetype, leavedays.ToString(), Reason, Convert.ToString(fromdate), forfrom, Convert.ToString(todate), forto, hdnLoginUserName.Value, "", sapproverlist, Emp_Name, strInsertmediaterlist);
                }
            }
           
        }
        Response.Redirect("InboxLeave_Req.aspx?itype=0");
    }
 

    protected void GetApproversStatuss()
    {
        int selectedIndex = gvMngLeaveRqstList.SelectedIndex;
        if (selectedIndex >= 0)
        {
            hdnReqid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[selectedIndex].Values[0]).Trim();
        }


        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "approve_leave";

        spars[1] = new SqlParameter("@Req_id", SqlDbType.Decimal);
        spars[1].Value = Convert.ToDecimal(hdnReqid.Value);

        dsTrDetails = spm.getDatasetList(spars, "Usp_getDetails_leaves");

        var emp_code = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Emp_Code"]).Trim();
        var leavetype = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Leave_Type"]).Trim();
        var leavedays = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["LeaveDays"]).Trim();
        var hdnleaveconditiontypeid = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["LeaveConditionTypeid"]).Trim();

        string strleavetype = Convert.ToString(leavetype).Trim();

        Decimal inoofDays = 0;
        if (Convert.ToString(leavedays).Trim() != "")
            inoofDays = Convert.ToDecimal(leavedays);

        dsapprover = spm.GetApproverStatus(emp_code, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid), strleavetype, inoofDays);

        DgvApprover.DataSource = null;
        DgvApprover.DataBind();

        if (dsapprover.Rows.Count > 0)
        {
            DgvApprover.DataSource = dsapprover;
            DgvApprover.DataBind();
        }
    }




    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)sender;
        foreach (GridViewRow row in gvMngLeaveRqstList.Rows)
        {
            CheckBox chkRow = (CheckBox)row.FindControl("CHK_clearancefromSubmitted");
            if (chkRow != null)
            {
                chkRow.Checked = chkAll.Checked;
            }

        }

    }

    protected void checkHR_Inbox()
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_Check_TD_COS_apprver_code";

            spars[1] = new SqlParameter("@exp_trvl", SqlDbType.VarChar);
            spars[1].Value = "HRML";

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = hdnEmpCode.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");
            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                hdnApproverType.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_type"]).Trim();
                InboxLeaveReqstList_HR();

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    private void InboxLeaveReqstList()
    {
        try
        {
            // DataSet dsList = new DataSet();
            DataTable dtleaveInbox = new DataTable();
            dtleaveInbox = spm.GetLeaveInbox(strempcode);

            if (dtleaveInbox.Rows.Count > 0)
            {
                gvMngLeaveRqstList.DataSource = dtleaveInbox;
                gvMngLeaveRqstList.DataBind();
                btnIn.Visible = true;
            }
            else
            {
                btnIn.Visible = false;
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void InboxLeaveReqstList_HR()
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getAllRequest_HR";

            spars[1] = new SqlParameter("@empCode", SqlDbType.VarChar);
            spars[1].Value = hdnEmpCode.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETLEAVEINBOX");
            //Travel Request Count
            gvMngLeaveRqstList.DataSource = null;
            gvMngLeaveRqstList.DataBind();
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                gvMngLeaveRqstList.DataSource = dsTrDetails.Tables[0];
                gvMngLeaveRqstList.DataBind();

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }


    public void PopulateEmployeeLeaveData()
    {
        try
        {

            dtLeaveDetails = spm.MyLeave_Req(lpm.Emp_Code);
            gvMngLeaveRqstList.DataSource = dtLeaveDetails;
            gvMngLeaveRqstList.DataBind();



        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }


    }


    
    protected void gvMngLeaveRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        PopulateEmployeeLeaveData();
        gvMngLeaveRqstList.PageIndex = e.NewPageIndex;
        gvMngLeaveRqstList.DataSource = dsLeaveRequst;
        gvMngLeaveRqstList.DataBind();
    }


    protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnReqid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
            hdnleaveTypeid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
            string strempwrkschdule = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
            string strLRapprvalType = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[3]).Trim();
            //   getSelectedEmpLeaveDetails_View();
            //Response.Redirect(ReturnUrl("sitepathmain") + "procs/Leave_Req_App.aspx?reqid=" + hdnReqid.Value);
            if (hdnleaveTypeid.Value == "11")
            {
                Response.Redirect("AppLeaveEncashmentDetailsView.aspx?reqid=" + hdnReqid.Value + "&itype=" + hdnhrappType.Value);
            }
            else
            {
                if (Convert.ToString(strempwrkschdule).Trim() == "6")
                {
                    if (Convert.ToString(strLRapprvalType) == "A")
                        Response.Redirect("Leave_Req_6_App.aspx?reqid=" + hdnReqid.Value + "&itype=" + hdnhrappType.Value);
                    else
                        Response.Redirect("Leave_Req_C_App.aspx?reqid=" + hdnReqid.Value + "&itype=" + hdnhrappType.Value);
                }
                else
                {
                    if (Convert.ToString(strLRapprvalType) == "A")
                        Response.Redirect("Leave_Req_App.aspx?reqid=" + hdnReqid.Value + "&itype=" + hdnhrappType.Value);
                    else
                        Response.Redirect("Leave_Req_C_App.aspx?reqid=" + hdnReqid.Value + "&itype=" + hdnhrappType.Value);
                }
            }



        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }



}
