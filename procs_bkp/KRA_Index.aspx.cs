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

public partial class KRA_Index : System.Web.UI.Page
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

			if (Page.User.Identity.IsAuthenticated == false)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/KRA_Index");
			}
			else
			{
				Page.SmartNavigation = true;
				if (!Page.IsPostBack)
				{
                    getMngKRAReqstCount();

                    checkIsLoginEmployeeAsReviewer();

                    get_LoginEmployeeIs_Submit_KRA();

                    checkIsRole_Assigned();

                    CheckIsReportShow_ResetKRA();
					CheckIsReport_DepartmentStatus();
					CheckIsReport_TemplateStatus();
                    CheckIsReport_DepartmentSummaryStatus();


                }
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}

    protected void lnk_leaverequest_Click(object sender, EventArgs e)
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "delete_GoalMeasurements_Temp_EmpKRATemplate";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(hflEmpCode.Value).Trim();

        dsList = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
        Response.Redirect("KRA_Create.aspx");
    }

    private void getMngKRAReqstCount()
    {
        try
        {
            DataTable dtTravelRequest = new DataTable();
            dtTravelRequest = spm.getPendingKRACount(hflEmpCode.Value);
            //Check Is Approver
            if (dtTravelRequest.Rows.Count > 0)
            {
                lnk_summary_report.Text = "Inbox KRA (" + dtTravelRequest.Rows[0]["KRA_Pending_Cnt"] + ")";
                //span_App_head.Visible = true;
                //hr_App_head.Visible = true;
                //lnk_summary_report.Visible = true;
                //lnk_Approved_Invoice.Visible = true;
            }
        }
        catch (Exception ex)
        {

        }
    }
 


    private void checkIsLoginEmployeeAsReviewer()
    {
        try
        {
            DataTable dtTravelRequest = new DataTable();
            dtTravelRequest = spm.getLoginEmployeeIsReviewer(hflEmpCode.Value);
            span_App_head.Visible = false;
            hr_App_head.Visible = false;
            lnk_summary_report.Visible = false;

            spnRpt.Visible = false;
            hr_Rpt_head.Visible = false;
            lnk_Reports.Visible = false;
            if (dtTravelRequest.Rows.Count > 0)
            {
                span_App_head.Visible = true;
                hr_App_head.Visible = true;
                lnk_summary_report.Visible = true;

                lnk_Reports.Visible = true;
                spnRpt.Visible = true;
                hr_Rpt_head.Visible = true;

                lnk_Index_Acc_Invoices.Visible = true;

                //lnk_Inbox_Payment_View.Visible = true;
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void checkIsRole_Assigned()
    {
        try
        {
            DataSet dsKRA = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "check_IsRole_assigned_employee";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = hflEmpCode.Value;

            dsKRA = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
 
            if (dsKRA.Tables[0].Rows.Count > 0)
            {
                lnk_leaveinbox.Visible = true;
                lnk_MobACC.Visible = true;
               // lnk_Index_My_Batch_Requests.Visible = true;
            }

            /*if (dsKRA.Tables[1].Rows.Count > 0)
            {
                lnk_leaverequest.Visible = true;
            }*/

         }
        catch (Exception ex)
        {

        }
    }


    private void get_LoginEmployeeIs_Submit_KRA()
    {
        try
        {

            DataSet dsKRA = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_LoginEmployeeIs_Submit_KRA";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = hflEmpCode.Value;

            dsKRA = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

         
            if (dsKRA.Tables[0].Rows.Count > 0)
            {
                tdCreateKRA.Visible = false;
            }

            if (spnRpt.Visible != true)
            {
                spnRpt.Visible = false;
                hr_Rpt_head.Visible = false;
                lnk_Reports.Visible = false;
            }

             //Check Is Department HOD
            if (dsKRA.Tables[1].Rows.Count > 0)
            {
                lnk_Index_Acc_Invoices.Visible = true;
                lnk_Reports.Visible = true;
                spnRpt.Visible = true;
                hr_Rpt_head.Visible = true;
				lnk_Index_My_Batch_Requests.Visible = true;
				lnk_Inbox_Payment_View.Visible = true;
			}


        }
        catch (Exception ex)
        {

        }
    }



    protected void lnk_leaveinbox_Click(object sender, EventArgs e)
    {

        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "delete_GoalMeasurements_Temp_Template";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(hflEmpCode.Value).Trim();

        dsList = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
        Response.Redirect("KRA_Template_Create.aspx");

    }

    public void CheckIsReportShow_ResetKRA()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShow_ResetKRA";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "ResetKRA";

            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_KRA_GETALL_DETAILS");
            //lnk_Index_Acc_Batch_Approval.Visible = false;
            lnk_Deemed_Approval.Visible = false;
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
			
					lnk_Index_Acc_Batch_Approval.Visible = true;
                    lnk_Deemed_Approval.Visible = true;
                }
                
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }
	

	public void CheckIsReport_TemplateStatus()
	{
		var getdtDetails = new DataTable();
		try
		{
			SqlParameter[] spars = new SqlParameter[3];
			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "CheckIsShow_ResetKRA";
			spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(hflEmpCode.Value);
			spars[2] = new SqlParameter("@ReportName", SqlDbType.NVarChar);
			spars[2].Value = "KRATemplateStatus";
			getdtDetails = spm.getTeamReportAllDDL(spars, "SP_KRA_GETALL_DETAILS");
			//lnk_Index_Acc_Batch_Approval.Visible = false;
			if (getdtDetails.Rows.Count > 0)
			{
				var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
				if (getStatus == "SHOW")
				{
					lnk_Index_My_Batch_Requests.Visible = true;
				}
			}
		}
		catch (Exception)
		{
		}
	}
	public void CheckIsReport_DepartmentStatus()
	{
		var getdtDetails = new DataTable();
		try
		{
			SqlParameter[] spars = new SqlParameter[3];
			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "CheckIsShow_ResetKRA";
			spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(hflEmpCode.Value);
			spars[2] = new SqlParameter("@ReportName", SqlDbType.NVarChar);
			spars[2].Value = "KRADeptReport";
			getdtDetails = spm.getTeamReportAllDDL(spars, "SP_KRA_GETALL_DETAILS");
			if (getdtDetails.Rows.Count > 0)
			{
				var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
				if (getStatus == "SHOW")
				{
					lnk_Inbox_Payment_View.Visible = true;
				}
			}
			
		}
		catch (Exception)
		{
			
		}
	}

    public void CheckIsReport_DepartmentSummaryStatus()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShow_ResetKRA";
            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);
            spars[2] = new SqlParameter("@ReportName", SqlDbType.NVarChar);
            spars[2].Value = "KRADeptSummaryReport";
            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_KRA_GETALL_DETAILS");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    Link_KRASummaryReports.Visible = true;
                }
            }

        }
        catch (Exception)
        {

        }
    }
}