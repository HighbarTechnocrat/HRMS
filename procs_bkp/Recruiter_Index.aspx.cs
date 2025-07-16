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

public partial class procs_Recruiter_Index : System.Web.UI.Page
{
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public int did = 0;
    SP_Methods spm = new SP_Methods();
    DataSet dsRecruterInox, dsInterviewerInox;


    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
           
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
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
                   
                    InboxRecruiterPendingRecord();
                    InboxInterviewerPendingRecord();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    #endregion

    #region page Methods
    protected void InboxRecruiterPendingRecord()
    {
      try
        {
           
            dsRecruterInox = spm.getRecruterInoxList(Convert.ToString(Session["Empcode"]).Trim(), "InRec");
            lblheading.Text = "Recruiter - OneHR";
            if (dsRecruterInox.Tables[0].Rows.Count > 0)
            {
                lnk_mng_recInbox.Text = "Inbox (" + Convert.ToString(dsRecruterInox.Tables[1].Rows[0]["Count"]).Trim() + ")";
            }
            else
            {
                lnk_mng_recInbox.Visible = false;
            }
            if (Convert.ToString(dsRecruterInox.Tables[2].Rows[0]["Count"]).Trim() != "0")
            {
                lnk_mng_recInterviewerShortlisted.Text = "Schedule Interview (" + Convert.ToString(dsRecruterInox.Tables[2].Rows[0]["Count"]).Trim() + ")";
                lnk_mng_recInterviewerShortlisted.Visible = true;
            }
            if (Convert.ToString(dsRecruterInox.Tables[3].Rows[0]["Count"]).Trim() != "0")
            {
                Lnk_mng_recRescheduleInterview.Text = "Reschedule Interview (" + Convert.ToString(dsRecruterInox.Tables[3].Rows[0]["Count"]).Trim() + ")";
                Lnk_mng_recRescheduleInterview.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void InboxInterviewerPendingRecord()
    {
        try
        {
            dsInterviewerInox = spm.getInterviewerInoxList(Convert.ToString(Session["Empcode"]).Trim(), "InPInter");
            if (dsInterviewerInox.Tables[2].Rows.Count > 0)
            {
                lblheading.Text = "Interviewer - OneHR";
                lnk_RecCreateEditCandidate.Visible = false;
                lnk_mng_recInbox.Visible = false;
                lnk_mng_ViewRecRequest.Visible = false;
                lnk_mng_recInterviewerShortlisted.Visible = false;
                Lnk_mng_recRescheduleInterview.Visible = false;

                lnk_mng_InterviewerShortlisting.Visible = true;
                lnk_mng_ViewRecRequestInterviewer.Visible = true;
                if (dsInterviewerInox.Tables[0].Rows.Count > 0)
                {
                    string countinbox = dsInterviewerInox.Tables[0].Rows[0]["count"].ToString();
                    if (countinbox == "0")
                    {
                        lnk_mng_InterviewerShortlisting.Visible = false;
                    }
                    else
                    {
                        lnk_mng_InterviewerShortlisting.Text = "Inbox Shortlisting (" + Convert.ToString(dsInterviewerInox.Tables[0].Rows[0]["Count"]).Trim() + ")";
                    }
                   
                        string countinbox1 = dsInterviewerInox.Tables[3].Rows[0]["count"].ToString();
                        if (countinbox1 == "0")
                        {
                           lnk_mng_InterviewrInbox.Visible = true;
                        }
                        else
                        {
                        lnk_mng_InterviewrInbox.Visible = true;
                        lnk_mng_InterviewrInbox.Text = "Inbox Interview (" + Convert.ToString(dsInterviewerInox.Tables[3].Rows[0]["Count"]).Trim() + ")";
                        }
                }
                else
                {
                }
            }
            else
            {
                lblheading.Text = "Recruiter - OneHR";
                lnk_RecCreateEditCandidate.Visible = true;
                lnk_mng_recInbox.Visible = true;
                lnk_mng_ViewRecRequest.Visible = true;
                lnk_mng_recInterviewerShortlisted.Visible = false;
                lnk_mng_InterviewrInbox.Visible = false;
                lnk_mng_InterviewerShortlisting.Visible = false;
                lnk_mng_ViewRecRequestInterviewer.Visible = false;
                Lnk_mng_recRescheduleInterview.Visible = false;
                InboxRecruiterPendingRecord();
            }
            
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    #endregion



}