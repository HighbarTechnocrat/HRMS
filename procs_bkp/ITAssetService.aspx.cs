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


public partial class ITAssetService : System.Web.UI.Page
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
            lnk_MobACC.Visible = false;
            lnk_MobCFO.Visible = false;
            lnk_MobPastApproved_ACC.Visible = false;
            span_acc_head.Visible = false;
            span_cos_head.Visible = false;
            
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
                    getMobile_Fule_Claims_PendingList_cnt_Approver();
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
            DataTable dtleaveInbox = new DataTable();

            // For CUSTODIAN
            var loginCode = Convert.ToString(hflEmpCode.Value);
            DataTable dtCustodianCODE = new DataTable();
            dtCustodianCODE = spm.GetAllCustodianDetails();

            lnk_leaveinbox.Text = "Inbox :(0)";
            lnk_leaveinbox.Visible = true;

            if (dtCustodianCODE.Rows.Count > 0)
            {
                var CeoCustCode = Convert.ToString(dtCustodianCODE.Rows[0]["EmpCode"]);
                if (loginCode == CeoCustCode)
                {
                    dtleaveInbox = spm.GetCustodianINBOX(hflEmpCode.Value);
                    if (dtleaveInbox.Rows.Count > 0)
                    {
                        lnk_leaveinbox.Visible = true;
                        lnk_leaveinbox.Text = "Inbox :(" + Convert.ToString(dtleaveInbox.Rows.Count).Trim() + ")";
                    }
                }
                if (loginCode == "I-017")
                {
                    dtleaveInbox = spm.GetCustodianINBOX(hflEmpCode.Value);
                    if (dtleaveInbox.Rows.Count > 0)
                    {
                        lnk_leaveinbox.Visible = true;
                        lnk_leaveinbox.Text = "Inbox :(" + Convert.ToString(dtleaveInbox.Rows.Count).Trim() + ")";
                    }
                    else
                    {
                        lnk_leaveinbox.Text = "Inbox :(0)";
                    }
                }
            }

            //FOR IT HOD
            var ITHodCode ="";
            DataTable dtITHODCODE = new DataTable();
            dtITHODCODE = spm.GetITHod();
            
            if (dtITHODCODE.Rows.Count > 0)
            {
                ITHodCode = Convert.ToString(dtITHODCODE.Rows[0]["HOD"]);
            }

                if (loginCode == ITHodCode)
                {
                    dtleaveInbox = spm.GetITHodINBOX(hflEmpCode.Value);
                    if (dtleaveInbox.Rows.Count > 0)
                    {
                        lnk_leaveinbox.Visible = true;
                        lnk_leaveinbox.Text = "Inbox :(" + Convert.ToString(dtleaveInbox.Rows.Count).Trim() + ")";
                        lnk_leaverequest.Visible = false;
                        lnk_mng_leaverequest.Visible = false;
                    }
                   else
                     {
                       lnk_leaveinbox.Text = "Inbox :(0)";
                       lnk_leaverequest.Visible = false;
                       lnk_mng_leaverequest.Visible = false;
                     }
                }
            
             //Commented code for processed request
             DataTable dtleaveInbox2 = new DataTable();
             dtleaveInbox2 = spm.GetInboxProcessedReqList(hflEmpCode.Value, null, null);
             if (dtleaveInbox2.Rows.Count > 0)
             {
                 lnk_MobACC.Visible = true;
            }

            
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
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
            spars[0].Value = "get_Mobile_claim_Reqst_Pending_cnt_COSACC";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = hflEmpCode.Value;

            spars[2] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[2].Value = strtype;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");



            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                if (strtype == "RACC")
                {
                    span_acc_head.Visible = true;
                    span_cos_head.Visible = false;
                    lnk_MobACC.Visible = true;
                    lnk_reimbursmentReport_1.Visible = true;
                    lnk_MobPastApproved_ACC.Visible = true;
                    lnk_MobACC.Text = "Account-Inbox :(" + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]).Trim() + ")";
                    lnk_reimbursmentReport_1.Text = "Report - (ACC)";
                }
                if (strtype == "RCFO")
                {
                    span_acc_head.Visible = false;
                    span_cos_head.Visible = true;
                    lnk_MobCFO.Visible = true;
                    lnk_MobPastApproved_ACC.Visible = false;
                    lnk_MobCFO.Text = "COS-Inbox :(" + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]).Trim() + ")";
                    lnk_reimbursmentReport_1.Visible = true;
                    lnk_reimbursmentReport_1.Text = "Report - (COS)";
                }

            }


            if (dsTrDetails.Tables[1].Rows.Count > 0)
            {
                if (strtype == "RACC")
                {
                    //lnk_MobPastApproved_ACC.Visible = true;
                    //lnk_MobPastApproved_ACC.Text = "Account Mobile Inbox";
                    lnk_reimbursmentReport_1.Text = "Audit Report - (ACC)";
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
            Response.Redirect("~/procs/ITAssetService_RepairReplaceReq.aspx");
        }

        else
        {
            return;
        }
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
   
}
