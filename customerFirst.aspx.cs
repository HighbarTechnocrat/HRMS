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



public partial class customerFirst : System.Web.UI.Page
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

     

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url;}

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }


            hflEmpCode.Value  = Convert.ToString(Session["Empcode"]);
              lblmsg.Visible = false;
            lnk_leaverequest.Visible = false;
            lnk_mng_leaverequest.Visible = false;
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/SampleForm");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    GetEmployeeDetails();
                    //get_emp_fule_eligibility();
                    CheckApprover();
                   // CheckIsSPOC();
                   // check_COS_ACC("RCOS");
                   // check_COS_ACC("RACC");
                   // check_COS_ACC("RCFO");

                   // check_ISLoginEmployee_ForLeave();
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
            // For PM
            DataTable dtleaveInbox1 = new DataTable();
            dtleaveInbox1 = spm.GetPMORHOD_INBOX(hflEmpCode.Value);
            lnk_leaverequest.Text = "Inbox :(0)";
            if (dtleaveInbox1.Rows.Count > 0)
            {
                lnk_leaverequest.Visible = true;
                lnk_leaverequest.Text = "Inbox :(" + Convert.ToString(dtleaveInbox1.Rows.Count).Trim() + ")";
            }

            DataTable dtIHODInboxList1 = new DataTable();
            dtIHODInboxList1 = spm.GetPMORHOD_AllSurveyList(hflEmpCode.Value);
            if (dtIHODInboxList1.Rows.Count > 0)
            {
                lnk_mng_leaverequest.Visible = true;
            }
            //// For HOD
            //DataTable dtleaveInbox = new DataTable();
            //dtleaveInbox = spm.GetHOD_INBOX(hflEmpCode.Value);
            ////lnk_leaverequest.Text = "Inbox :(0)";
            //if (dtleaveInbox.Rows.Count > 0)
            //{
            //    lnk_leaverequest.Visible = true;
            //    lnk_leaverequest.Text = "Inbox :(" + Convert.ToString(dtleaveInbox.Rows.Count).Trim() + ")";
            //}

            //DataTable dtIHODInboxList = new DataTable();
            //dtIHODInboxList = spm.GetHOD_AllSurveyList(hflEmpCode.Value);
            //if (dtIHODInboxList.Rows.Count > 0)
            //{
            //    lnk_mng_leaverequest.Visible = true;
            //}
            //For CEO
            DataTable dtCEOEMPCODE = new DataTable();
            dtCEOEMPCODE = spm.GetCEOEmpCode();
            if(dtCEOEMPCODE.Rows.Count>0)
            {
                var loginCode = Convert.ToString(hflEmpCode.Value);
                var CeoEmpCode = Convert.ToString(dtCEOEMPCODE.Rows[0]["Emp_Code"]);
                if(loginCode== CeoEmpCode)
                {
                    var getCEOInbox = spm.GetCEO_INBOX();
                    if(getCEOInbox.Rows.Count>0)
                    {
                        lnk_leaverequest.Visible = true;
                        lnk_leaverequest.Text = "Inbox :(" + Convert.ToString(getCEOInbox.Rows.Count).Trim() + ")";
                    }
                    var getCEOInboxList = spm.GetCEO_AllSurveyList();
                    if (getCEOInboxList.Rows.Count > 0)
                    {
                        lnk_mng_leaverequest.Visible = true;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void CheckApprover()
    {
        //DataTable dtApprovers = new DataTable();
        //dtApprovers = spm.CheckApprovers(Convert.ToString(hflEmpCode.Value).Trim());
        //if (dtApprovers.Rows.Count > 0)
        //{
            getMobile_Fule_Claims_PendingList_cnt_Approver();
       // }
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
