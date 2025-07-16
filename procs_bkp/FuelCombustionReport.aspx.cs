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

public partial class procs_FuelCombustionReport : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    bool isMGR = false;
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
        }
        lblmessage.Text = "";
        if (!Page.IsPostBack)
        {
            hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();
            hflEmpCode.Value = Convert.ToString(Session["Empcode"]).Trim();
            var getLoginEmpDeg = hflEmpDesignation.Value;
            var getDepartment = hflEmpDepartment.Value;
           // loadDropDownDepartment(getDepartment);
        }

    }

   
    public class CustomReportCredentials : IReportServerCredentials
    {
        private string _UserName;
        private string _PassWord;
        private string _DomainName;

        public CustomReportCredentials(string UserName, string PassWord, string DomainName)
        {
            _UserName = UserName;
            _PassWord = PassWord;
            _DomainName = DomainName;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public ICredentials NetworkCredentials
        {
            get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string user,
         out string password, out string authority)
        {
            authCookie = null;
            user = password = authority = null;
            return false;
        }
    }

    public static void write2log(string strmsg)
    {

        System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Configuration.ConfigurationSettings.AppSettings["LogTestPath"] +
             "Log_" + DateTime.Now.Day.ToString() + ".txt", true);
        sw.WriteLine(strmsg);
        sw.Flush();
        sw.Close();
    }

    //private void getemployee_ReimbursmentDetails()
    //{
    //    try
    //    {
    //        #region get employee Claim details
    //        DataSet dsempreimburstment = new DataSet();
    //        SqlParameter[] spars = new SqlParameter[4];
    //        string[] strdate;
    //        string strfromDate = "";
    //        string strToDate = "";
    //        string strfromDate_RPt = "";
    //        string strToDate_RPt = "";
    //        //@fromDate
    //        DataTable dtApprovers = new DataTable();
    //        dtApprovers = spm.CheckHOD(Convert.ToString(hflEmpCode.Value).Trim());
    //        if (dtApprovers.Rows.Count > 0)
    //        {

    //            isMGR = false;
    //        }
    //        else
    //        {
    //            isMGR = true;
    //        }
    //        if (Convert.ToString(hflEmpCode.Value) == "S-005" || Convert.ToString(hflEmpCode.Value) == "00630814" || Convert.ToString(hflEmpCode.Value) == "00630967" || Convert.ToString(hflEmpCode.Value) == "00631019" || Convert.ToString(hflEmpCode.Value) == "00630596" || Convert.ToString(hflEmpCode.Value) == "00630008")
    //        {
    //            isMGR = false;
    //        }
    //        spars[0] = new SqlParameter("@from_date", SqlDbType.VarChar);
    //        if (Convert.ToString(txtFromdate.Text).Trim() != "")
    //        {
    //            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
    //            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
    //            strfromDate_RPt = Convert.ToString(strdate[0]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[2]);
    //            spars[0].Value = strfromDate;
    //        }
    //        else
    //        {
    //            spars[0].Value = null;
    //        }
    //        //@toDate
    //        spars[1] = new SqlParameter("@to_date", SqlDbType.VarChar);
    //        if (Convert.ToString(txtToDate.Text).Trim() != "")
    //        {
    //            strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
    //            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
    //            strToDate_RPt = Convert.ToString(strdate[0]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[2]);
    //            spars[1].Value = strToDate;
    //        }
    //        else
    //        {
    //            strToDate = DateTime.Today.ToString("yyyy") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("dd");
    //            strToDate_RPt = DateTime.Today.ToString("dd") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("yyyy");
    //            spars[1].Value = strToDate;
    //        }
    //        //@ServiceDepartment
    //        if (isMGR == false)
    //        {
    //            spars[2] = new SqlParameter("@rem_id", SqlDbType.Int);
    //            if (Convert.ToString(ddlDepartment.SelectedValue).Trim() != "0" && Convert.ToString(ddlDepartment.SelectedValue).Trim() != "")
    //            {
    //                spars[2].Value = Convert.ToString(ddlDepartment.SelectedValue);
    //            }
    //            else
    //            {
    //                spars[2].Value = null;
    //            }
    //            //@status
    //            spars[3] = new SqlParameter("@stype", SqlDbType.VarChar);
    //            spars[3].Value = "onBoarding_SeparationReport";
    //        }
    //        else
    //        {
    //            spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
    //            if (Convert.ToString(hflEmpCode.Value).Trim() != "")
    //            {
    //                spars[2].Value = Convert.ToString(hflEmpCode.Value);
    //            }
    //            else
    //            {
    //                spars[2].Value = null;
    //            }
    //            //@status
    //            spars[3] = new SqlParameter("@stype", SqlDbType.VarChar);
    //            spars[3].Value = "onBoarding_SeparationReport_Managers";
    //        }
    //        dsempreimburstment = spm.getDatasetList(spars, "rpt_dataProcedure");

    //        #endregion

    //        ReportViewer2.LocalReport.Refresh();
    //        ReportViewer2.LocalReport.DataSources.Clear();
    //        ReportParameter[] param = new ReportParameter[2];
    //        param[0] = new ReportParameter("FromDate", Convert.ToString(strfromDate_RPt));
    //        param[1] = new ReportParameter("ToDate", Convert.ToString(strToDate_RPt));

    //        if (dsempreimburstment.Tables[0].Rows.Count > 0)
    //        {
    //            ReportDataSource rdsFuel = new ReportDataSource("ds_MIS_onboarding", dsempreimburstment.Tables[0]);
    //            ReportDataSource rdsFuel_detail = new ReportDataSource("dsEmp_Rpt_Str_Detail", dsempreimburstment.Tables[1]);
    //            ReportDataSource rdsFuel_detail_Sep = new ReportDataSource("dsEmp_Rpt_Str_Detail_Sep", dsempreimburstment.Tables[2]);
    //            ReportDataSource rdsFuel_detail_onboard = new ReportDataSource("dsEmp_Rpt_Str_Detail_Onboard", dsempreimburstment.Tables[3]);
    //            ReportViewer2.ProcessingMode = ProcessingMode.Local;
    //            ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/MIS_OnboardingSeparation.rdlc");
    //            ReportViewer2.LocalReport.DataSources.Add(rdsFuel);
    //            ReportViewer2.LocalReport.DataSources.Add(rdsFuel_detail);
    //            ReportViewer2.LocalReport.DataSources.Add(rdsFuel_detail_Sep);
    //            ReportViewer2.LocalReport.DataSources.Add(rdsFuel_detail_onboard);
    //            ReportViewer2.LocalReport.SetParameters(param);
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}



    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            BindReport(ddlYear.SelectedValue, hflEmpCode.Value);
        }
        catch (Exception ex)
        {
        }
    }

    public void BindReport(string ddlYear, string Empcode)
    {
        try
        {
            string SSRSReportPath1 = ConfigurationManager.AppSettings["AppointmentLetterIssuedReportPath"];

            ReportParameter[] parameters = new ReportParameter[6];
            parameters[0] = new ReportParameter("stype", "AppointmetLetterIssuedRptDetails");
            parameters[1] = new ReportParameter("Year", ddlYear);
            parameters[5] = new ReportParameter("Emp_Code", Empcode);



            //ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("Administrator", "Hbt@2075", "HBT05");//comment for local
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;//comment for local

            ReportViewer1.ServerReport.ReportServerUrl = new Uri("http://hbt05/ReportServer");
           // ReportViewer1.ServerReport.ReportServerUrl = new Uri("http://manisha-t-hbt/ReportServer");//uncomment for local
            ReportViewer1.ServerReport.ReportPath = SSRSReportPath1;
            ReportViewer1.ServerReport.ReportPath = "/EmployeeSRRS_Report/EmpSSRS_Report";
            ReportViewer1.ServerReport.SetParameters(parameters);
            ReportViewer1.ServerReport.Refresh();

            //ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            //IReportServerCredentials irsc = new CustomReportCredentials("Administrator", "9BmCpxCUsMkmn@", "onehrdb");
            //ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            //ReportViewer1.ServerReport.ReportServerUrl = new Uri("https://onehrdb/ReportServer");
            //ReportViewer1.ServerReport.ReportPath = SSRSReportPath1;

            ReportViewer1.ServerReport.SetParameters(parameters);
            ReportViewer1.ServerReport.Refresh();

        }
        catch (Exception ex)
        {
            write2log(ex.Message);


            write2log(ex.StackTrace);
        }
    }

}