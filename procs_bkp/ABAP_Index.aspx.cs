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

public partial class procs_ABAP_Index : System.Web.UI.Page
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
                    CheckReport();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

   

    public void CheckApprover()
    {

        SqlParameter[] sparsd = new SqlParameter[2];
        sparsd[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        sparsd[0].Value = "GetApproverPageButton";
        sparsd[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        sparsd[1].Value = Convert.ToString(Session["Empcode"].ToString());
        DataSet DSApprover = spm.getDatasetList(sparsd, "SP_ABAP_Productivity_CompletionSheet");

        if (DSApprover.Tables[0].Rows.Count > 0)
        {
            lnk_ResigForm.Visible = true;
            lnk_MyResig.Visible = true;
        }

        if (DSApprover.Tables[1].Rows.Count > 0)
        {
            trScreener.Visible = true;
            trApprover.Visible = true;

            string Countt = Convert.ToString(DSApprover.Tables[2].Rows[0]["Countt"]).ToString();
            HDCount.Value = Countt;
            lnk_InboxResignations.Text = "Inbox ABAP Completion(" + Convert.ToString(Countt) + ")";
            

        }
    }

    public void CheckReport()
    {

        SqlParameter[] sparsd = new SqlParameter[2];
        sparsd[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        sparsd[0].Value = "Get_ABAP_ReportDisplay";
        sparsd[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        sparsd[1].Value = Convert.ToString(Session["Empcode"].ToString());
        DataSet DSRpt = spm.getDatasetList(sparsd, "SP_ABAP_Productivity_CompletionSheet");

        if (DSRpt.Tables[0].Rows.Count > 0)
        {
            HDRptCout.Value = "0";
            TRReport.Visible = true;
            tr1Report.Visible = true;
        }
    }



    protected void lnk_InboxResignations_Click(object sender, EventArgs e)
    {
        if (HDCount.Value != "0")
        {
            Response.Redirect("~/procs/ABAP_Prd_TimeSheetAppList.aspx");
        }
    }

    protected void Lnk_ReportABAP_Click(object sender, EventArgs e)
    {
        if (HDRptCout.Value == "0")
        {
            Response.Redirect("~/procs/ABAP_ObjectCompletionReport.aspx");
        }
    }
}