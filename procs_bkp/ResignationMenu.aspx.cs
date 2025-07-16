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



public partial class ResignationMenu : System.Web.UI.Page
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


            hflEmpCode.Value = Convert.ToString(Session["Empcode"]);
            lblmsg.Visible = false;
            lnk_leaveinbox.Visible = false;
            lnk_MobPastApproved_ACC.Visible = false;
            span_App_head.Visible = false;
            //lnk_MobPastApproved_ACC.Visible = false;
            lnk_reimbursmentReport_1.Visible = false;
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/SampleForm");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {


                    #region Check Login Employee is  applicable for Reimbursment


                    if (check_ISLoginEmployee_ForReimbursment() == false)
                    {
                        lblheading.Text = "Reimbursement Module (Coming Soon...)";
                        editform1.Visible = false;
                        return;
                    }
                    #endregion

                    GetEmployeeDetails();
                    get_emp_fule_eligibility();
                    CheckApprover();
                    check_COS_ACC("HRML");
                    //check_COS_ACC("RACC");
                    //check_COS_ACC("RCFO");

                    check_ISLoginEmployee_ForLeave();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    public void getMobile_Fule_Claims_PendingList_cnt_Approver()
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Resignation_Reqst_Pending_cnt_Appr";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = hflEmpCode.Value;
            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            lnk_leaveinbox.Text = "Inbox :(0)";
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                span_App_head.Visible = true;
                lnk_MobPastApproved_ACC.Visible = true;
                lnk_leaveinbox.Visible = true;
                lnk_leaveinbox.Text = "HR Inbox :(" + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]).Trim() + ")";
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void CheckApprover()
    {
        DataTable dtApprovers = new DataTable();
        dtApprovers = spm.CheckApprovers(Convert.ToString(hflEmpCode.Value).Trim());
        if (dtApprovers.Rows.Count > 0)
        {
            getMobile_Fule_Claims_PendingList_cnt_Approver();
        }
    }

    protected void GetLeaveCount()
    {

        int LeaveCount = 0;
        LeaveCount = spm.GetLeaveInboxCount(Convert.ToString(hflEmpCode.Value).Trim());
        lnk_leaveinbox.Text = "Inbox :(" + LeaveCount.ToString() + ")";
    }

    protected void check_COS_ACC(string strtype)
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_Check_COS_ACC_apprver_code_byType";

            spars[1] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[1].Value = strtype;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = hflEmpCode.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");
            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                getMobileClaims_PendingList_cnt_COSACC(strtype);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void getMobileClaims_PendingList_cnt_COSACC(string strtype)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Resignation_claim_Reqst_Pending_cnt_COSACC";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = hflEmpCode.Value;

            spars[2] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[2].Value = strtype;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            if (dsTrDetails.Tables[1].Rows.Count > 0)
            {
                if (strtype == "HRML")
                {
                    lnk_MobPastApproved_ACC.Visible = true;
                    lnk_MobPastApproved_ACC.Text = "Archieved Resignations";
                    //lnk_reimbursmentReport_1.Text = "Audit Report - (ACC)";
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void lnk_leaverequest_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() != "")
        {
            Session["TripTypeId"] = null;
            Session["TravelType"] = null;
            spm.clear_Reimbursement_temp_tables(hflEmpCode.Value, "DeleteMobileTemp");
            Response.Redirect("~/procs/Resignation_Req.aspx");
        }

        else
        {
            return;
        }
    }
    public Boolean check_ISLoginEmployee_ForReimbursment()
    {
        Boolean bchkEMP = false;
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Check_ISapplicable_Reimbursment";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = hflEmpCode.Value;
            dsTrDetails = spm.getDatasetList(spars, "Usp_getEmployee_Details_All");

            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                bchkEMP = true;
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
        return bchkEMP;

    }

    public void GetEmployeeDetails()
    {
        try
        {
            DataTable dtEmpDetails = new DataTable();
            dtEmpDetails = spm.GetEmployeeData(Convert.ToString(hflEmpCode.Value));
            if (dtEmpDetails.Rows.Count > 0)
            {
                hflEmpDesignation.Value = Convert.ToString(dtEmpDetails.Rows[0]["DesginationName"]);
                hflEmpDepartment.Value = Convert.ToString(dtEmpDetails.Rows[0]["Department_Name"]);
                hflGrade.Value = Convert.ToString(dtEmpDetails.Rows[0]["Grade"]).Trim();
                hflEmailAddress.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
            }
            //  getApproverdata();  
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

            throw;
        }
    }

    public void GetMobileEligibility_New()
    {

        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.GetMobilelEligibilityMatrix_Emp_Grade(Convert.ToString(hflGrade.Value), Convert.ToString(hflEmpCode.Value), Convert.ToString(1));

        if (dtApproverEmailIds.Rows.Count <= 0)
        {
            lnk_leaverequest.Visible = false;
            lnk_mng_leaverequest.Visible = false;
        }
        if (dtApproverEmailIds.Rows.Count > 0)
        {
            if (Convert.ToString(dtApproverEmailIds.Rows[0]["Eligibility"]).Trim() != "")
            {
                if (Convert.ToDecimal(dtApproverEmailIds.Rows[0]["Eligibility"]) <= 0)
                {
                    lnk_leaverequest.Visible = false;
                    lnk_mng_leaverequest.Visible = false;
                }
            }
        }

    }


    public void get_emp_fule_eligibility()
    {
        try
        {

            #region date formatting

            string[] strdate;
            string strFromDate = "";
            if (Convert.ToString(hdnClaimDate.Value).Trim() != "")
            {
                strdate = Convert.ToString(hdnClaimDate.Value).Trim().Split('/');
                strFromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            #endregion


            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "getemp_fule_eligibility";

            spars[1] = new SqlParameter("@Empcode", SqlDbType.VarChar);
            spars[1].Value = hflEmpCode.Value;

            spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[2].Value = 0;

            spars[3] = new SqlParameter("@ClaimDate", SqlDbType.VarChar);
            if (Convert.ToString(hdnClaimDate.Value).Trim() != "")
                spars[3].Value = strFromDate;
            else
                spars[3].Value = DBNull.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            if (dsTrDetails.Tables[0].Rows.Count == 0)
            {
                #region Set Enable False if not application for Fuel Claim

                #endregion

            }



        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }

    }

    public void check_ISLoginEmployee_ForLeave()
    {

        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Check_ISapplicable_Fuel_mobile_PV";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = hflEmpCode.Value;


            dsTrDetails = spm.getDatasetList(spars, "Usp_getEmployee_Details_All");

            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                bool blnchk = false;
                //Fuel
                if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["view_fuel"]).Trim() == "N")
                {
                    blnchk = true;
                }

                //Mobile
                if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["view_mobile"]).Trim() == "N")
                {
                    blnchk = true;
                    lnk_leaveinbox.Visible = false;
                    lnk_MobPastApproved_ACC.Visible = false;
                    span_App_head.Visible = false;
                    //lnk_MobPastApproved_ACC.Visible = false;
                }


                //Payment Voucher
                if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["view_paymentVoucher"]).Trim() == "N")
                {
                    blnchk = true;
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }


    }
}
