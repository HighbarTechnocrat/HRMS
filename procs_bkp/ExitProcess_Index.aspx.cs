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
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
public partial class ExitProcess_Index : System.Web.UI.Page
{
	public string userid;
	SP_Methods spm = new SP_Methods();
	public DataTable dtEmp, dtRectruter;
	public string filename = "", approveremailaddress;

	protected void lnkhome_Click(object sender, EventArgs e)
	{
		Response.Redirect(ReturnUrl("sitepathmain") + "default");
	}
	public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
			}
			hflEmpCode.Value = Convert.ToString(Session["Empcode"]);
			//  lblmsg.Text =Convert.ToString(Session["Empcode"]); 
			if (Page.User.Identity.IsAuthenticated == false)
			{
				//Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
			}
			else
			{
				Page.SmartNavigation = true;
				if (!Page.IsPostBack)
				{
					CheckApprover();
                    CheckClearanceApprover();
                    InboxResignationscount();
                    LoadTeamExitCount();
                    LoadClearanceCount();
                    checkEligible();
					Check_Retention_Access_Employee();
					Check_Retention_Access_Moderation();
					GetRetention_Approval_PendingCount();
					this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}

	public void MakeAllLinksVisible()
    {
		lnk_ResigForm.Visible = true;
		lnk_MyResig.Visible = true;
		lnk_ExitSurveyForm.Visible = true;
		lnk_MyExitSurveyForm.Visible = true;
		lnk_ClearanceForm.Visible = true;
		lnk_MyClearanceForm.Visible = true;

		lnk_InboxResignations.Visible = true;
		lnk_TeamExitInterviewForm.Visible = true;
		lnk_ProcessedResignations.Visible = true;
	}

	public void CheckApprover()
	{
		DataTable dtApprovers = new DataTable();
		dtApprovers = spm.ExitProcCheckApprovers(Convert.ToString(hflEmpCode.Value).Trim());
		
		if (dtApprovers.Rows.Count > 0)
		{
			span_App.Visible = true;
			lnk_InboxResignations.Visible = true;
			lnk_SubmitExitInteriewForm.Visible = true;
			lnk_TeamExitInterviewForm.Visible = true;
			lnk_ProcessedResignations.Visible = true;
			lnk_SummaryReport.Visible = false;
			lnk_Detail_Report.Visible = false;
			span_clr.Visible = true;
			span_rpt.Visible = false;
			lnk_InboxClearanceForm.Visible = true;
			lnk_ApprovedClearanceForm.Visible = true;
		}
		else
		{
			span_App.Visible = false;
			lnk_InboxResignations.Visible = false;
			lnk_SubmitExitInteriewForm.Visible = false;
			lnk_TeamExitInterviewForm.Visible = false;
			lnk_ProcessedResignations.Visible = false;
			lnk_SummaryReport.Visible = false;
			lnk_Detail_Report.Visible = false;
			span_clr.Visible = false;
			span_rpt.Visible = false;
			lnk_InboxClearanceForm.Visible = false;
			lnk_ApprovedClearanceForm.Visible = false;

		}
	}


    //get_ClearanceApprover
    public void CheckClearanceApprover()
    {
        DataTable dtApprovers = new DataTable();
        dtApprovers = spm.ExitProcClearanceApprover(Convert.ToString(hflEmpCode.Value).Trim());

        if (dtApprovers.Rows.Count > 0)
        {
            span_App.Visible = false;
            lnk_InboxResignations.Visible = false;
            lnk_SubmitExitInteriewForm.Visible = false;
            lnk_TeamExitInterviewForm.Visible = false;
            lnk_ProcessedResignations.Visible = false;
            lnk_SummaryReport.Visible = false;
            lnk_Detail_Report.Visible = false;
            span_clr.Visible = true;
            span_rpt.Visible = false;
            lnk_InboxClearanceForm.Visible = true;
            lnk_ApprovedClearanceForm.Visible = true;
        }
        else
        {
            //span_App.Visible = false;
            //lnk_InboxResignations.Visible = false;
            //lnk_SubmitExitInteriewForm.Visible = false;
            //lnk_TeamExitInterviewForm.Visible = false;
            //lnk_ProcessedResignations.Visible = false;
            //lnk_SummaryReport.Visible = false;
            //lnk_Detail_Report.Visible = false;
            //span_clr.Visible = false;
            //span_rpt.Visible = false;
            //lnk_InboxClearanceForm.Visible = false;
            //lnk_ApprovedClearanceForm.Visible = false;

        }
    }
    public void InboxResignationscount()
    {
		try
		{
			DataTable dtResigDetails = new DataTable();
			dtResigDetails = spm.InboxResignations(hflEmpCode.Value);
			if (dtResigDetails.Rows.Count > 0)
			{
				lnk_InboxResignations.Visible = true;
				lnk_InboxResignations.Text = "Inbox Resignation(" + Convert.ToString(dtResigDetails.Rows.Count).Trim() + ")";
			}
            else
            {
				lnk_InboxResignations.Text = "Inbox Resignation(0)";
				//lnk_InboxResignations.Visible = true;
			}
		}
		catch (Exception ex)
		{

		}
	}

    public void LoadTeamExitCount()
    {
        DataTable dtExitIntCnt = spm.GetTeamExitInterviewListFormDetails(Convert.ToString(Session["Empcode"]).Trim());
        if (dtExitIntCnt.Rows.Count > 0)
        {
            lnk_SubmitExitInteriewForm.Text = "Submit Exit Interview Form(" + dtExitIntCnt.Rows.Count + ")";
        }
    }
    public void LoadClearanceCount()
    {
        DataTable dtClearanceInbxCnt = spm.GetClearanceInbox(Convert.ToString(Session["Empcode"]).Trim());
        if (dtClearanceInbxCnt.Rows.Count > 0)
        {
            lnk_InboxClearanceForm.Text = "Inbox Clearance Form(" + dtClearanceInbxCnt.Rows.Count + ")";
        }
    }
    public void checkEligible()
    {
        DataTable dtExitEligible = spm.GetUserEligibleData(Convert.ToString(Session["Empcode"]).Trim());

        if (dtExitEligible.Rows.Count > 0)
        {
            if (Convert.ToString(dtExitEligible.Rows[0]["ExitSurveyEligible"]) == "Yes")
            {
                lnk_ExitSurveyForm.Visible = true;
                lnk_MyExitSurveyForm.Visible = true;
            }
            else
            {
                lnk_ExitSurveyForm.Visible = false;
                lnk_MyExitSurveyForm.Visible = false;
            }
            if (Convert.ToString(dtExitEligible.Rows[0]["ExitClearEligible"]) == "Yes")
            {
                lnk_ClearanceForm.Visible = true;
                lnk_MyClearanceForm.Visible = true;
            }
            else
            {
                lnk_ClearanceForm.Visible = false;
                lnk_MyClearanceForm.Visible = false;
            }
            if (Convert.ToString(dtExitEligible.Rows[0]["ExitMyExitIntEligible"]) == "Yes")
            {
                lnk_MyExitInterviewForm.Visible = true;
            }
            else
            {
                lnk_MyExitInterviewForm.Visible = false;
            }
        }
    }

	public void Check_Retention_Access_Employee()
	{
		try
		{
				DataTable dtClearanceInbxCnt = spm.Get_Dept_Retention_Detail("Get_Dept_HOD", Convert.ToString(Session["Empcode"]).Trim());
				if (dtClearanceInbxCnt.Rows.Count > 0)
				{
				    SPRT.Visible = true;
				    lnk_Retention_Create.Visible = true;
				    lnk_Retention_MyList.Visible = true;
			    }
		}
		catch (Exception)
		{

			throw;
		}
	}
	public void Check_Retention_Access_Moderation()
	{
		try
		{
			DataTable dtClearanceInbxCnt = spm.Get_Dept_Retention_Detail("Check_Moderation_Employee", Convert.ToString(Session["Empcode"]).Trim());
			if (dtClearanceInbxCnt.Rows.Count > 0)
			{
				SPMD.Visible = true;
				lnk_Moderation_Inbox.Visible = true;
				lnk_Moderation_APP.Visible = true;
			}
		}
		catch (Exception)
		{

			throw;
		}
	}
	protected void GetRetention_Approval_PendingCount()
	{
		DataTable dt = new DataTable();
		dt = spm.Get_Dept_Retention_Detail("Retention_Pending_Mode", Convert.ToString(Session["Empcode"]).Trim());
		if (dt.Rows.Count > 0)
		{
			lnk_Moderation_Inbox.Text = "Inbox(" + dt.Rows.Count + ")";
		}

	}
}