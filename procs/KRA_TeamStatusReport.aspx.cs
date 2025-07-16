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

public partial class KRA_TeamStatusReport : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
        }
        if (Page.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/KRA_Index.aspx");
        }

        if (!Page.IsPostBack)
        {
            hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();
            getTeamStatusReport();


        } 
    }

    protected void getTeamStatusReport()
    {


        #region get KRA Team Status

        DataSet dtKRATeam = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Rpt_my_team_status";

        spars[1] = new SqlParameter("@Approver_emp_code", SqlDbType.VarChar);
        spars[1].Value = hdnloginempcode.Value;

        spars[2] = new SqlParameter("@period_id", SqlDbType.Int);
        spars[2].Value = DBNull.Value;

        dtKRATeam =spm.getDatasetList(spars, "SP_KRA_Reports");

        #endregion

        try
        {
            string strpath = Server.MapPath("~/procs/KRAMyTeamstatus_rpt.rdlc");
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = strpath;

            ReportDataSource rds_summary = new ReportDataSource("dsMyTeam_Status", dtKRATeam.Tables[0]);
            ReportDataSource rds_KRANotSubmitted = new ReportDataSource("dsKRA_NotSubmitted", dtKRATeam.Tables[1]);
            ReportDataSource rds_KRASubmitted = new ReportDataSource("dsKRA_Submitted", dtKRATeam.Tables[2]);


            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds_summary);
            ReportViewer1.LocalReport.DataSources.Add(rds_KRANotSubmitted);
            ReportViewer1.LocalReport.DataSources.Add(rds_KRASubmitted);
            ReportViewer1.LocalReport.Refresh();
        }
        catch (Exception ex)
        {

        }
    }





}