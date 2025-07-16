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
using ClosedXML.Excel;

public partial class IndexEmployeeCV : System.Web.UI.Page
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
            Session["chkbtnStatus_Appr"] = "";
            hflEmpCode.Value = Convert.ToString(Session["Empcode"]);
            lblmsg.Visible = false;

            //  lblmsg.Text =Convert.ToString(Session["Empcode"]); 
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    
                    CheckIsAdminAccess();
                    CheckIsReportShow();
                    CheckIsHRReportShow();
                    CheckIsHRUpdateCVReportShow();
					EmployeeCVReviewInbox(Convert.ToString(Session["Empcode"]));
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
    public void CheckIsReportShow()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowCV";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "CVViewReport";



            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    lnk_Inbox_CV.Visible = true;
                }
                else
                {
                    lnk_Inbox_CV.Visible = false;
                }
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }

    public void CheckIsHRReportShow()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowCV";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "CVHRAdmin";



            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    lnk_hrView.Visible = true;
                }
                else
                {
                    lnk_hrView.Visible = false;
                }
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }

    public void CheckIsHRUpdateCVReportShow()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowCV";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "HRUpdateCV";



            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    Lnk_HRUpdateCV.Visible = true;
                }
                else
                {
                    Lnk_HRUpdateCV.Visible = false;
                }
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }

    public void CheckIsAdminAccess()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowCV";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "CVAdmin";



            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    lnk_Board.Visible = true;
                    lnk_Degree.Visible = true;
                    LinkButton3.Visible = true;
                    LinkButton4.Visible = true;
                    LinkButton5.Visible = true;
                    LinkButton6.Visible = true;
                    LinkButton7.Visible = true;
                    LinkButton8.Visible = true;
                    LinkButton9.Visible = true;
                    LinkButton10.Visible = true;
                    LinkButton11.Visible = true;
                    LinkButton12.Visible = true;
                    // return true;
                    isShowAdmin.Visible = true;
                    BtnLink_DownLoadCVDump.Visible = true;


                }
                else
                {
                    lnk_Board.Visible = false;
                    lnk_Degree.Visible = false;
                    LinkButton3.Visible = false;
                    LinkButton4.Visible = false;
                    LinkButton5.Visible = false;
                    LinkButton6.Visible = false;
                    LinkButton7.Visible = false;
                    LinkButton8.Visible = false;
                    LinkButton9.Visible = false;
                    LinkButton10.Visible = false;
                    LinkButton11.Visible = false;
                    LinkButton12.Visible = false;
                    isShowAdmin.Visible = false;
                    BtnLink_DownLoadCVDump.Visible = false;


                }
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }

    protected void BtnLink_DownLoadCVDump_Click(object sender, EventArgs e)
    {

        try
        {
	    
            DataSet DSPaymentDetail = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CVAutomationdownload";
            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = DBNull.Value;
            DSPaymentDetail = spm.getDatasetList(spars, "GET_CVAutomationdownload");

            //string[] ColumnsToBeDeleted = {"Emp_id", "emp_status"};

            //foreach (string ColName in ColumnsToBeDeleted)
            //{
            //    if (DSPaymentDetail.Tables[0].Columns.Contains(ColName))
            //    {
            //        DSPaymentDetail.Tables[0].Columns.Remove(ColName);
            //    }
            //}

            //  DSPaymentDetail.Tables[0].AcceptChanges();

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(DSPaymentDetail.Tables[0], "Employee_MainSheet");
                wb.Worksheets.Add(DSPaymentDetail.Tables[1], "Employee_FamilyDetails");
                wb.Worksheets.Add(DSPaymentDetail.Tables[2], "Employee_EducationDetails");
                wb.Worksheets.Add(DSPaymentDetail.Tables[3], "Employee_CertificationDetails");
                wb.Worksheets.Add(DSPaymentDetail.Tables[4], "Employee_ProjectDetails");
                wb.Worksheets.Add(DSPaymentDetail.Tables[5], "Employee_DomainDetails");


                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=DownloadCVdump.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
lblmsg.Visible = true;
        }
    }
	
	 private void EmployeeCVReviewInbox(string emp_code)
    {
        try
        {
            DataTable dtTravelRequest = new DataTable();
            dtTravelRequest = spm.getEmployeeCVReviewInbox("getEmployeeCVReviewInbox", Convert.ToString(emp_code));
            if (dtTravelRequest.Rows.Count > 0)
            {
                lnk_EmpCVReview.Visible = true;
                lnk_EmpCVReview.Text = "Employee CV Review ("+ dtTravelRequest.Rows.Count + ")";
            }
            else
            {
                lnk_EmpCVReview.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
}
