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
using System.Web;
using System.ComponentModel;
using System.Linq;

public partial class procs_BenchListReport : System.Web.UI.Page
{
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    SP_Methods spm = new SP_Methods();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    #region Page Events
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

            lpm.Emp_Code = Session["Empcode"].ToString();

            lblmessage.Visible = true;
           // btnIn.Visible = true;
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    getBenchListAsOnDate();
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

    #endregion

    private void getBenchListAsOnDate()
    {
        try
        {
            #region get BenchList Report
            DataTable dtBenchList = new DataTable();
            SqlParameter[] spars = new SqlParameter[1];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "BenchListSystemAppli";
            dtBenchList = spm.getTeamReport(spars, "SP_BenchListReport");

            #endregion

            //ReportParameter[] param = new ReportParameter[4];

            //param[0] = new ReportParameter("StartDate", Convert.ToString(strfromDate));
            //param[1] = new ReportParameter("EndDate", Convert.ToString(strToDate));
            //param[2] = new ReportParameter("secoundReportHide", Convert.ToString(secoundReportHide));
            //param[3] = new ReportParameter("EmpName", Convert.ToString(EmpName));

            ReportViewer2.LocalReport.Refresh();
            ReportViewer2.LocalReport.DataSources.Clear();

            if (dtBenchList.Rows.Count > 0)
            {
                ReportDataSource ITAssetSummary = new ReportDataSource("BenchList", dtBenchList);
                ReportViewer2.ProcessingMode = ProcessingMode.Local;

                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/BenchListReport.rdlc");
                ReportViewer2.LocalReport.DataSources.Add(ITAssetSummary);
                //ReportViewer2.ShowToolBar = true;
            }

            // ReportViewer2.LocalReport.SetParameters(param);

        }
        catch (Exception ex)
        {
        }
    }

    //protected void btnIn_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        getBenchListAsOnDate();
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}

    

}