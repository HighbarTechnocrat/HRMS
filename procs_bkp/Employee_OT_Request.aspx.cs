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
using System.Linq;
using ClosedXML.Excel;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;

public partial class Employee_OT_Request : System.Web.UI.Page
{

    #region CreativeMethods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    #endregion

    SP_Methods spm = new SP_Methods();
    DataSet dspaymentVoucher_Apprs = new DataSet();
    String CEOInList = "N";
    double YearlymobileAmount = 0;
    #region PageEvents
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            //mobile_btnPrintPV.Visible = false;

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    hdnApp_Id.Value = "0";
                    txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtToDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txt_OTAmounttobepaid.Attributes.Add("onkeypress", "return onCharOnlyNumber_EXP(event);");

                    BindAllDDL();
                    GetYearList();
                    if (Request.QueryString.Count > 0)
                    {
                        var t = Convert.ToString(Request.QueryString[0]).Trim();
                        hdnApp_Id.Value = t;
                        lnk_LinkButton2.Visible = true;
                        lnk_ed_Search.Visible = false;
                        backList.Visible = true;
                        BindData();
                    }
                    else
                    {
                        backList.Visible = false;
                        lnk_LinkButton1.Visible = true;
                    }
                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["EmployeeCVDCFilePath"]).Trim());
                    //TXT_AssginmentDate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                    editform.Visible = true;
                    divbtn.Visible = false;
                    divmsg.Visible = false;
                    hdnempcode.Value = Convert.ToString(Session["Empcode"]);
                    //BindEmpDetails(Convert.ToString(Session["Empcode"]), "All");

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
                else
                {

                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    #endregion

    #region PageMethods
    private void BindAllDDL()
    {
        try
        {
            var getDetails = spm.GetOTRequestEmployeeDetails("Get_EmployeeList", "", "", "");
            if (getDetails != null)
            {
                ddl_Employee.DataSource = null;
                ddl_Employee.DataBind();
                if (getDetails.Rows.Count > 0)
                {
                    ddl_Employee.DataSource = getDetails;
                    ddl_Employee.DataTextField = "EmpFullName";
                    ddl_Employee.DataValueField = "Emp_Code";
                    ddl_Employee.DataBind();
                    ddl_Employee.Items.Insert(0, new ListItem("Select Employee", "0"));
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void BindData()
    {
        try
        {
            var getid = hdnApp_Id.Value;
            if (getid != "")
            {
                var getDs = spm.getOTReport(getid, "Get_Emp_Details_ByOTId");
                if (getDs != null)
                {
                    if (getDs.Tables.Count > 0)
                    {
                        var getEmployeeDetails = getDs.Tables[0];
                        ddl_Employee.Items.FindByValue(Convert.ToString(getEmployeeDetails.Rows[0]["Emp_Code"])).Selected = true;
                        ddl_Employee.Enabled = false;
                        ddlMonthYear.Items.FindByValue(Convert.ToString(getEmployeeDetails.Rows[0]["SelectedMonth"])).Selected = true;
                        ddlMonthYear.Enabled = false;
                        txtFromdate.Text = Convert.ToString(getEmployeeDetails.Rows[0]["FromDate"]);
                        txtFromdate.Enabled = false;
                        txtToDate.Text = Convert.ToString(getEmployeeDetails.Rows[0]["ToDate"]);
                        txtToDate.Enabled = false;
                        txt_Location.Text = Convert.ToString(getEmployeeDetails.Rows[0]["Emp_ProjectName"]);
                        txt_Location.Enabled = false;
                        txt_Designation.Text = Convert.ToString(getEmployeeDetails.Rows[0]["Designation"]);
                        txt_Designation.Enabled = false;
                        txt_ShiftTime.Text = Convert.ToString(getEmployeeDetails.Rows[0]["In_range"]) + " - " + Convert.ToString(getEmployeeDetails.Rows[0]["Out_range"]);
                        txt_ShiftTime.Enabled = false;
                        txt_EligibleOTHours.Text = Convert.ToString(getEmployeeDetails.Rows[0]["EligibleOTHours"]);
                        txt_EligibleOTHours.Enabled = false;
                        txt_EligiblePayableOT.Text = Convert.ToString(getEmployeeDetails.Rows[0]["EligiblePayableOTAmount"]);
                        txt_EligiblePayableOT.Enabled = false;
                        txt_OTAmounttobepaid.Text = Convert.ToString(getEmployeeDetails.Rows[0]["OTAmountToBePaid"]);
                        txt_OTAmounttobepaid.Enabled = false;
                        txtOverAllRemark.Text = Convert.ToString(getEmployeeDetails.Rows[0]["OverallRemarks"]);
                        txtOverAllRemark.Enabled = false;
                        txt_RM.Text = Convert.ToString(getEmployeeDetails.Rows[0]["ReportingManagerName"]);
                        txt_RM.Enabled = false;

                        var getOTdetails = getDs.Tables[1];
                        if (getOTdetails.Rows.Count > 0)
                        {
                            gv_EmployeeDetails.DataSource = getOTdetails;
                            gv_EmployeeDetails.DataBind();
                            var showTotalWorkingHR = getOTdetails.Rows[0]["TotalWorkingHours"].ToString();
                            var getTotalOverTime = getOTdetails.Rows[0]["TotalOverTime"].ToString();
                            gv_EmployeeDetails.FooterRow.Cells[6].Text = showTotalWorkingHR;
                            gv_EmployeeDetails.FooterRow.Cells[7].Text = getTotalOverTime;
                            foreach (GridViewRow rows in gv_EmployeeDetails.Rows)
                            {
                                TextBox remark = (TextBox)rows.FindControl("txt_Remark_Enter");
                                remark.Enabled = false;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void BindGrid()
    {
        try
        {
            var ddlEmployee = string.Empty;
            var ddlSelectedMonth = string.Empty;
            if (ddl_Employee.SelectedValue == "" || ddl_Employee.SelectedValue == "0")
            {
                lblmessage.Text = "Please select employee name";
                return;
            }
            ddlEmployee = ddl_Employee.SelectedValue;
            if (ddlMonthYear.SelectedValue == "" || ddlMonthYear.SelectedValue == "0")
            {
                lblmessage.Text = "Please select month ";
                return;
            }
            ddlSelectedMonth = ddlMonthYear.SelectedValue;
            if (!CheckIsExist(ddlEmployee, ddlSelectedMonth))
            {
                lblmessage.Text = "Report already generated.";
                var getSelectedVal = ddl_Employee.SelectedValue;
                ddl_Employee.Items.FindByValue(getSelectedVal).Selected = false;
                ddl_Employee.Items.FindByValue("0").Selected = true;
                getSelectedVal = ddlMonthYear.SelectedValue;
                ddlMonthYear.Items.FindByValue(getSelectedVal).Selected = false;
                ddlMonthYear.Items.FindByValue("0").Selected = true;
                txtFromdate.Text = "";
                txtToDate.Text = "";
                return;
            }
            if (txtFromdate.Text == "")
            {
                lblmessage.Text = "Please select from date";
                return;
            }
            if (txtToDate.Text == "")
            {
                lblmessage.Text = "Please select to date";
                return;
            }

            var strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            var strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

            strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
            var strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

            var getDetails = spm.GetOTRequestEmployeeDetails("Get_Emp_Details", ddlEmployee, strfromDate, strToDate);
            if (getDetails != null)
            {
                gv_EmployeeDetails.DataSource = null;
                gv_EmployeeDetails.DataBind();
                if (getDetails.Rows.Count > 0)
                {
                    gv_EmployeeDetails.DataSource = getDetails;
                    gv_EmployeeDetails.DataBind();
                    var showTotalWorkingHR = getDetails.Rows[0]["TotalWorkingHours"].ToString();
                    var getTotalOverTime = getDetails.Rows[0]["TotalOverTime"].ToString();
                    if (getTotalOverTime != "" && getTotalOverTime != null)
                    {
                        var splitVal = getTotalOverTime.Split(':')[0].ToString();
                        var getVal = Convert.ToInt32(splitVal);
                        if (getVal >= 25)
                        {
                            var EligibleOTHR = getVal - 25;
                            var EligiblePayableOTAmount = EligibleOTHR * 100;
                            txt_EligibleOTHours.Text = EligibleOTHR.ToString();
                            txt_EligiblePayableOT.Text = EligiblePayableOTAmount.ToString();
                            txt_OTAmounttobepaid.Enabled = true;
                        }
                        else
                        {
                            txt_EligibleOTHours.Text = "0";
                            txt_EligiblePayableOT.Text = "0";
                            txt_OTAmounttobepaid.Enabled = false;
                        }
                    }
                    gv_EmployeeDetails.FooterRow.Cells[6].Text = showTotalWorkingHR;
                    gv_EmployeeDetails.FooterRow.Cells[7].Text = getTotalOverTime;
                    hdnTotalHR.Value = showTotalWorkingHR;
                    hdnTotalOverTimeHR.Value = getTotalOverTime;
                    txt_RM.Text = Convert.ToString(Session["emp_loginName"]).Trim() + " - " + DateTime.Now.ToString("dd-MM-yyyy hh:mm");


                }
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message;
            return;
        }
    }
    #endregion

    protected void lnk_ed_Clear_Click(object sender, EventArgs e)
    {
        try
        {
            BindAllDDL();

        }
        catch (Exception ex)
        {

        }
    }

    protected void lnk_ed_Download_Click(object sender, EventArgs e)
    {
        //try
        //{

        //    lblmessage.Text = "";
        //    if (rbWildSearch.Checked == true)
        //    {
        //        WildSearchDownload();
        //    }
        //    else
        //    {
        //        if (txt_From.Text.Trim() != "")
        //        {
        //            if (txt_To.Text.Trim() == "")
        //            {
        //                lblmessage.Text = "Please enter both  overall experience from and to";
        //                return;
        //            }
        //            else
        //            {
        //                var getFrom = decimal.Parse(txt_From.Text.Trim());
        //                var getTo = decimal.Parse(txt_To.Text.Trim());
        //                if (getFrom >= getTo)
        //                {
        //                    lblmessage.Text = "from overall experience value can't be greater than to overall experience";
        //                    txt_To.Text = "";
        //                    return;
        //                }
        //            }
        //        }
        //        if (txt_From.Text.Trim() == "" && txt_To.Text.Trim() != "")
        //        {
        //            lblmessage.Text = "Please enter both  SAP experience from and to ";

        //            return;
        //        }
        //        //SAP
        //        if (txt_From_SAP.Text.Trim() != "")
        //        {
        //            if (txt_To_SAP.Text.Trim() == "")
        //            {
        //                lblmessage.Text = "Please enter both  SAP experience from and to";
        //                return;
        //            }
        //            else
        //            {
        //                var getFrom = decimal.Parse(txt_From_SAP.Text.Trim());
        //                var getTo = decimal.Parse(txt_To_SAP.Text.Trim());
        //                if (getFrom >= getTo)
        //                {
        //                    lblmessage.Text = "from SAP experience value can't be greater than to SAP experience";
        //                    txt_To_SAP.Text = "";
        //                    return;
        //                }
        //            }
        //        }
        //        if (txt_From_SAP.Text.Trim() == "" && txt_To_SAP.Text.Trim() != "")
        //        {
        //            lblmessage.Text = "Please enter both  SAP experience from and to ";

        //            return;
        //        }
        //        //SAP
        //        if (txt_From_DE.Text.Trim() != "")
        //        {
        //            if (txt_To_DE.Text.Trim() == "")
        //            {
        //                lblmessage.Text = "Please enter both  domain experience from and to";
        //                return;
        //            }
        //            else
        //            {
        //                var getFrom = decimal.Parse(txt_From_DE.Text.Trim());
        //                var getTo = decimal.Parse(txt_To_DE.Text.Trim());
        //                if (getFrom >= getTo)
        //                {
        //                    lblmessage.Text = "from domain experience value can't be greater than to SAP experience";
        //                    txt_From_DE.Text = "";
        //                    return;
        //                }
        //            }
        //        }
        //        if (txt_From_DE.Text.Trim() == "" && txt_To_DE.Text.Trim() != "")
        //        {
        //            lblmessage.Text = "Please enter both  domain experience from and to ";

        //            return;
        //        }
        //        DownloadGridData();
        //    }

        //}
        //catch (Exception ex)
        //{
        //    lblmessage.Text = ex.Message;
        //}
    }

    protected void lnk_ED_View_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        var fId = Convert.ToString(gv_EmployeeDetails.DataKeys[row.RowIndex].Values[0]).Trim();
        if (fId != null)
        {
            Response.Redirect("ViewEmployeeCV.aspx?reqid=" + fId + "");
        }
    }


    protected void gv_EmployeeDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_EmployeeDetails.PageIndex = e.NewPageIndex;
        this.BindGrid();
    }

    protected void lnk_Download_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(gv_EmployeeDetails.DataKeys[row.RowIndex].Values[0]).Trim();
            if (fId != null)
            {
                //BindEmpDetails(Convert.ToInt32(fId));
                var getDS = spm.getCVReportById(Convert.ToInt32(fId), "GetDetailsByEmpId");
                DownlodWord(getDS);
                // System.Threading.Tasks.Task.Delay();
                // Thread.Sleep(5000);
                // DownlodPDF(getDS);
            }
        }
        catch (Exception)
        {

            throw;
        }

    }

    private void DownlodPDF(DataSet ds)
    {
        try
        {
            var getDS = ds;
            if (getDS != null)
            {
                StringBuilder strbuild = new StringBuilder();
                var isMarried = "";
                if (getDS.Tables.Count > 0)
                {
                    var getEmployeeDetails = getDS.Tables[0];
                    var getReportdetails = getDS.Tables[1];
                    ReportViewer ReportViewer1 = new ReportViewer();
                    // Create Report DataSource
                    ReportDataSource rdEmployeeDetails = new ReportDataSource("dsOTRequest", getEmployeeDetails);
                    ReportDataSource rdOTReportDetails = new ReportDataSource("dsOTRequestDetails", getReportdetails);

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/OTRequestReport.rdlc");
                    ReportViewer1.LocalReport.DataSources.Add(rdEmployeeDetails);
                    ReportViewer1.LocalReport.DataSources.Add(rdOTReportDetails);
                    //ReportViewer1.LocalReport.SetParameters(param);


                    #region Create payment Voucher PDF file
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;
                    string getDateTime = DateTime.Now.ToString("ddMMyyyyHHmmss");
                    DataTable DataTable1 = new DataTable();
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                    byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                    Response.Buffer = true;
                    //Response.ClearHeaders();
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename=" + "OT_Report_" + getDateTime + "." + extension);
                    try
                    {
                        Response.BinaryWrite(bytes);
                        // Response.ClearHeaders();
                        // Response.Flush();
                        //Response.Clear();
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error while generating PDF.');", true);
                        Console.WriteLine(ex.StackTrace);
                    }


                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void DownlodWord(DataSet ds)
    {
        try
        {
            var getDS = ds;
            if (getDS != null)
            {
                StringBuilder strbuild = new StringBuilder();
                var isMarried = "";
                if (getDS.Tables.Count > 0)
                {
                    var getEmployeeDetails = getDS.Tables[0];
                    var getSNAPSHOT = getDS.Tables[1];
                    var getProjectDetails = getDS.Tables[2];
                    var getDomainDetails = getDS.Tables[3];
                    var getEduactionDetails = getDS.Tables[4];
                    var getCertificationDetails = getDS.Tables[5];
                    var getEmpName = Convert.ToString(getEmployeeDetails.Rows[0]["Emp_Name"]);
                    ReportViewer ReportViewer1 = new ReportViewer();
                    // Create Report DataSource
                    ReportDataSource rdEmployeeDetails = new ReportDataSource("dsEmployeeDetails", getEmployeeDetails);
                    ReportDataSource rdSNAPSHOT = new ReportDataSource("dsSAPSNAPSHOT", getSNAPSHOT);
                    ReportDataSource rdProjectDetails = new ReportDataSource("dsProjectDetails", getProjectDetails);
                    ReportDataSource rdDomainDetails = new ReportDataSource("dsDomainDetails", getDomainDetails);
                    ReportDataSource rdEduactionDetails = new ReportDataSource("dsEducationDetails", getEduactionDetails);
                    ReportDataSource rdCertificationDetails = new ReportDataSource("dsCertificationDetails", getCertificationDetails);

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/EmployeeCVReport.rdlc");
                    ReportViewer1.LocalReport.DataSources.Add(rdEmployeeDetails);
                    ReportViewer1.LocalReport.DataSources.Add(rdSNAPSHOT);
                    ReportViewer1.LocalReport.DataSources.Add(rdProjectDetails);
                    ReportViewer1.LocalReport.DataSources.Add(rdDomainDetails);
                    ReportViewer1.LocalReport.DataSources.Add(rdEduactionDetails);
                    ReportViewer1.LocalReport.DataSources.Add(rdCertificationDetails);
                    //ReportViewer1.LocalReport.SetParameters(param);


                    #region Create payment Voucher PDF file
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;
                    DataTable DataTable1 = new DataTable();
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                    byte[] bytes = ReportViewer1.LocalReport.Render("WORD", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename=" + getEmpName + "_CV." + extension);
                    try
                    {
                        Response.BinaryWrite(bytes);
                        //Response.Flush();
                        //Response.Clear();
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error while generating PDF.');", true);
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    #endregion
    #endregion

    protected void lnk_Download_PDF_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(gv_EmployeeDetails.DataKeys[row.RowIndex].Values[0]).Trim();
            if (fId != null)
            {
                //BindEmpDetails(Convert.ToInt32(fId));
                var getDS = spm.getCVReportById(Convert.ToInt32(fId), "GetDetailsByEmpId");
                // DownlodWord(getDS);
                // System.Threading.Tasks.Task.Delay();
                // Thread.Sleep(5000);
                DownlodPDF(getDS);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }


    //private void DownloadGridData()
    //{
    //    try
    //    {
    //        var ddlDepartment = "";
    //        var ddlDesignation = "";
    //        var ddlProject = "";
    //        var ddlmodule = "";
    //        var ddlQualification = "";
    //        var ddlDegree = "";
    //        var ddlCertification = "";
    //        var ddlProjectType = "";
    //        var ddlIndustryType = "";
    //        var ddlRole = "";
    //        var ddlDomain = "";
    //        var ddlBand = "";
    //        var ddlUniversity = "";
    //        var ddlOrganisationType = "";
    //        var ddlOrganisationName = "";
    //        var ddlStream = "";
    //        var ddlRegion = "";
    //        var ddlSelect = string.Empty;
    //        int? year = null;
    //        var ddlEmployee = string.Empty;
    //        decimal? overAllFrom = null;
    //        decimal? overAllTo = null;
    //        //
    //        decimal? projectFrom = null;
    //        decimal? projectTo = null;
    //        //getddl Department
    //        decimal? domainFrom = null;
    //        decimal? domainTo = null;
    //        // Department
    //        foreach (ListItem item in ddl_Department.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                if (item.Value != "" && item.Value != "0")
    //                {
    //                    if (ddlDepartment == "")
    //                    {
    //                        ddlDepartment = item.Value;
    //                    }
    //                    else
    //                    {
    //                        ddlDepartment = ddlDepartment + "|" + item.Value;
    //                    }
    //                }
    //            }
    //        }
    //        //DDL Designations
    //        foreach (ListItem item in ddl_Designation.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                if (item.Value != "" && item.Value != "0")
    //                {
    //                    if (ddlDesignation == "")
    //                    {
    //                        ddlDesignation = item.Value;
    //                    }
    //                    else
    //                    {
    //                        ddlDesignation = ddlDesignation + "|" + item.Value;
    //                    }
    //                }
    //            }
    //        }
    //        //DDL ddlProject
    //        foreach (ListItem item in ddl_Project.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                if (item.Value != "" && item.Value != "0")
    //                {
    //                    if (ddlProject == "")
    //                    {
    //                        ddlProject = item.Value;
    //                    }
    //                    else
    //                    {
    //                        ddlProject = ddlProject + "|" + item.Value;
    //                    }
    //                }
    //            }
    //        }
    //        //DDL Module
    //        foreach (ListItem item in ddl_Module.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                if (item.Value != "" && item.Value != "0")
    //                {
    //                    if (ddlmodule == "")
    //                    {
    //                        ddlmodule = item.Value;
    //                    }
    //                    else
    //                    {
    //                        ddlmodule = ddlmodule + "|" + item.Value;
    //                    }
    //                }
    //            }
    //        }
    //        // Qualification
    //        foreach (ListItem item in lstQualification.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                if (item.Value != "" && item.Value != "0")
    //                {
    //                    if (ddlQualification == "")
    //                    {
    //                        ddlQualification = item.Value;
    //                    }
    //                    else
    //                    {
    //                        ddlQualification = ddlQualification + "|" + item.Value;
    //                    }
    //                }
    //            }
    //        }
    //        // Degree
    //        foreach (ListItem item in lstDegree.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                if (item.Value != "" && item.Value != "0")
    //                {
    //                    if (ddlDegree == "")
    //                    {
    //                        ddlDegree = item.Value;
    //                    }
    //                    else
    //                    {
    //                        ddlDegree = ddlDegree + "|" + item.Value;
    //                    }
    //                }
    //            }
    //        }
    //        //Certifiation
    //        foreach (ListItem item in lstCertification.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                if (item.Value != "" && item.Value != "0")
    //                {
    //                    if (ddlCertification == "")
    //                    {
    //                        ddlCertification = item.Value;
    //                    }
    //                    else
    //                    {
    //                        ddlCertification = ddlCertification + "|" + item.Value;
    //                    }
    //                }
    //            }
    //        }
    //        //Project Type
    //        var getProjectTypeText = "";
    //        foreach (ListItem item in lstProject.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                if (item.Value != "" && item.Value != "0")
    //                {
    //                    if (ddlProjectType == "")
    //                    {
    //                        ddlProjectType = item.Value;
    //                    }
    //                    else
    //                    {
    //                        ddlProjectType = ddlProjectType + "|" + item.Value;
    //                    }
    //                    //
    //                    if (getProjectTypeText == "")
    //                    {
    //                        getProjectTypeText = item.Text;
    //                    }
    //                    else
    //                    {
    //                        getProjectTypeText = getProjectTypeText + "|" + item.Text;
    //                    }
    //                }
    //            }
    //        }
    //        //Industry
    //        foreach (ListItem item in lstIndustry.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                if (item.Value != "" && item.Value != "0")
    //                {
    //                    if (ddlIndustryType == "")
    //                    {
    //                        ddlIndustryType = item.Value;
    //                    }
    //                    else
    //                    {
    //                        ddlIndustryType = ddlIndustryType + "|" + item.Value;
    //                    }
    //                }
    //            }
    //        }
    //        // ROle And Designation
    //        foreach (ListItem item in lstDesignation.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                if (item.Value != "" && item.Value != "0")
    //                {
    //                    if (ddlRole == "")
    //                    {
    //                        ddlRole = item.Value;
    //                    }
    //                    else
    //                    {
    //                        ddlRole = ddlRole + "|" + item.Value;
    //                    }
    //                }
    //            }
    //        }
    //        //Domain
    //        foreach (ListItem item in lstDomain.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                if (item.Value != "" && item.Value != "0")
    //                {
    //                    if (ddlDomain == "")
    //                    {
    //                        ddlDomain = item.Value;
    //                    }
    //                    else
    //                    {
    //                        ddlDomain = ddlDomain + "|" + item.Value;
    //                    }
    //                }
    //            }
    //        }
    //        //Band
    //        foreach (ListItem item in ddl_Band.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                if (item.Value != "" && item.Value != "0")
    //                {
    //                    if (ddlBand == "")
    //                    {
    //                        ddlBand = item.Value;
    //                    }
    //                    else
    //                    {
    //                        ddlBand = ddlBand + "|" + item.Value;
    //                    }
    //                }
    //            }
    //        }
    //        // University
    //        foreach (ListItem item in lstUniversity.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                if (item.Value != "" && item.Value != "0")
    //                {
    //                    if (ddlUniversity == "")
    //                    {
    //                        ddlUniversity = item.Value;
    //                    }
    //                    else
    //                    {
    //                        ddlUniversity = ddlUniversity + "|" + item.Value;
    //                    }
    //                }
    //            }
    //        }
    //        // Organisation Type
    //        foreach (ListItem item in lstOrganisationType.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                if (item.Value != "" && item.Value != "0")
    //                {
    //                    if (ddlOrganisationType == "")
    //                    {
    //                        ddlOrganisationType = item.Value;
    //                    }
    //                    else
    //                    {
    //                        ddlOrganisationType = ddlOrganisationType + "|" + item.Value;
    //                    }
    //                }
    //            }
    //        }
    //        // Organisation Name
    //        foreach (ListItem item in lstOrganisationName.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                if (item.Value != "" && item.Value != "0")
    //                {
    //                    if (ddlOrganisationName == "")
    //                    {
    //                        ddlOrganisationName = item.Value;
    //                    }
    //                    else
    //                    {
    //                        ddlOrganisationName = ddlOrganisationName + "|" + item.Value;
    //                    }
    //                }
    //            }
    //        }
    //        //ddlSelect
    //        if (ddl_Status.SelectedValue != "" && ddl_Status.SelectedValue != "0")
    //        {
    //            ddlSelect = ddl_Status.SelectedValue;
    //        }

    //        // Employee Code
    //        if (ddl_Employee.SelectedValue != "" && ddl_Employee.SelectedValue != "0")
    //        {
    //            ddlEmployee = ddl_Employee.SelectedValue;
    //        }
    //        //Year Of Passing
    //        if (txt_YearOfPassing.Text != "")
    //        {
    //            year = Convert.ToInt32(txt_YearOfPassing.Text);
    //        }
    //        //ddlSelect
    //        if (txt_From.Text.Trim() != "")
    //        {
    //            overAllFrom = decimal.Parse(txt_From.Text.Trim());
    //        }
    //        if (txt_To.Text.Trim() != "")
    //        {
    //            overAllTo = decimal.Parse(txt_To.Text.Trim());
    //        }
    //        if (txt_From_DE.Text.Trim() != "")
    //        {
    //            domainFrom = decimal.Parse(txt_From_DE.Text.Trim());
    //        }
    //        if (txt_To_DE.Text.Trim() != "")
    //        {
    //            domainTo = decimal.Parse(txt_To_DE.Text.Trim());
    //        }
    //        if (txt_From_SAP.Text.Trim() != "")
    //        {
    //            projectFrom = decimal.Parse(txt_From_SAP.Text.Trim());
    //        }

    //        if (txt_To_SAP.Text.Trim() != "")
    //        {
    //            projectTo = decimal.Parse(txt_To_SAP.Text.Trim());
    //        }

    //        // Region
    //        foreach (ListItem item in lst_Region.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                if (item.Value != "" && item.Value != "0")
    //                {
    //                    if (ddlRegion == "")
    //                    {
    //                        ddlRegion = item.Value;
    //                    }
    //                    else
    //                    {
    //                        ddlRegion = ddlRegion + "|" + item.Value;
    //                    }
    //                }
    //            }
    //        }
    //        // Stream
    //        foreach (ListItem item in lst_Stream.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                if (item.Value != "" && item.Value != "0")
    //                {
    //                    if (ddlStream == "")
    //                    {
    //                        ddlStream = item.Value;
    //                    }
    //                    else
    //                    {
    //                        ddlStream = ddlStream + "|" + item.Value;
    //                    }
    //                }
    //            }
    //        }
    //        var IsCompletedCertification = chk_Completed_Certification.Checked;
    //        var getDS = spm.getCVDetailInbox(0, "GetInboxDetails", ddlEmployee, ddlDepartment, ddlDesignation, ddlProject, ddlmodule, overAllFrom, overAllTo, ddlSelect, ddlQualification, ddlDegree, ddlCertification, ddlProjectType, ddlIndustryType, ddlRole, ddlDomain, ddlBand, ddlUniversity, ddlOrganisationType, ddlOrganisationName, year, projectFrom, projectTo, domainFrom, domainTo, ddlRegion, ddlStream, IsCompletedCertification);
    //        if (getDS != null)
    //        {
    //            if (getDS.Tables.Count > 0)
    //            {

    //                var getDetails = getDS.Tables[0];

    //                if (getDetails.Rows.Count > 0)
    //                {
    //                    var IsUpdated = false;
    //                    var status = "Compliant";
    //                    foreach (DataRow item1 in getDetails.Rows)
    //                    {
    //                        bool IsDepartment = true, IsBand = true, IsModule = true, IsDesignation = true, IsEmployee = true, IsProject = true, IsQualification = true;
    //                        bool IsUniversity = true, IsDegree = true, IsProjectType = true, IsCertification = true, IsRole = true, IsDomain = true, IsOrganisationName = true;
    //                        bool IsOrganisationType = true, IsIndustryType = true, Isyear = true, IsoverAllFrom = true, IsdomainFrom = true, IsprojectFrom = true, IsStream = true, IsRegion = true;
    //                        bool IsCertified = true;
    //                        var getEmpId = Convert.ToString(item1["Emp_Code"]);
    //                        var getDeptId = Convert.ToString(item1["DeptId"]);
    //                        var getDesigId = Convert.ToString(item1["DesigId"]);
    //                        var getgrade = Convert.ToString(item1["grade"]);
    //                        var getEmpLocation = Convert.ToString(item1["EmpLocation"]);
    //                        var getTotalSAPExperience = Convert.ToString(item1["TotalSAPExperience"]);
    //                        var getOverallWorkExperience = Convert.ToString(item1["OverallWorkExperience"]);
    //                        var getTotalDomainExperience = Convert.ToString(item1["TotalDomainExperience"]);
    //                        if (ddlDepartment != "")
    //                        {
    //                            var splitVal = ddlDepartment.Split('|');
    //                            if (splitVal.Contains(getDeptId))
    //                            {
    //                                IsDepartment = true;
    //                            }
    //                            else
    //                            {
    //                                IsDepartment = false;
    //                            }
    //                        }
    //                        if (ddlBand != "")
    //                        {
    //                            var splitVal = ddlBand.Split('|');
    //                            if (splitVal.Contains(getgrade))
    //                            {
    //                                IsBand = true;
    //                            }
    //                            else
    //                            {
    //                                IsBand = false;
    //                            }
    //                        }
    //                        if (ddlmodule != "")
    //                        {
    //                            var splitVal = ddlmodule.Split('|');
    //                            foreach (var item in splitVal)
    //                            {
    //                                var getStatus = spm.CheckIsIdsExists("CheckModuleExists", getEmpId, Convert.ToInt32(item));
    //                                if (getStatus.Rows.Count > 0)
    //                                {
    //                                    var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
    //                                    if (getMsg == "EXISTS")
    //                                    {
    //                                        IsModule = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        IsModule = false;
    //                                        break;
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    IsModule = false;
    //                                }
    //                            }
    //                        }
    //                        if (ddlDesignation != "")
    //                        {
    //                            var splitVal = ddlDesignation.Split('|');
    //                            if (splitVal.Contains(getDesigId))
    //                            {
    //                                IsDesignation = true;
    //                            }
    //                            else
    //                            {
    //                                IsDesignation = false;
    //                            }
    //                        }
    //                        if (ddlEmployee != "")
    //                        {
    //                            var splitVal = ddlEmployee;
    //                            if (splitVal.Contains(getEmpId))
    //                            {
    //                                IsEmployee = true;
    //                            }
    //                            else
    //                            {
    //                                IsEmployee = false;
    //                            }
    //                        }
    //                        if (ddlProject != "")
    //                        {
    //                            var splitVal = ddlProject.Split('|');
    //                            if (splitVal.Contains(getEmpLocation))
    //                            {
    //                                IsProject = true;
    //                            }
    //                            else
    //                            {
    //                                IsProject = false;
    //                            }
    //                        }
    //                        if (ddlQualification != "")
    //                        {
    //                            var splitVal = ddlQualification.Split('|');
    //                            foreach (var item in splitVal)
    //                            {
    //                                var getStatus = spm.CheckIsIdsExists("CheckQualificationExists", getEmpId, Convert.ToInt32(item));
    //                                if (getStatus.Rows.Count > 0)
    //                                {
    //                                    var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
    //                                    if (getMsg == "EXISTS")
    //                                    {
    //                                        IsQualification = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        IsQualification = false;
    //                                        break;
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    IsQualification = false;
    //                                }
    //                            }
    //                        }
    //                        if (ddlUniversity != "")
    //                        {
    //                            var splitVal = ddlUniversity.Split('|');
    //                            foreach (var item in splitVal)
    //                            {
    //                                var getStatus = spm.CheckIsIdsExists("CheckBoardExists", getEmpId, Convert.ToInt32(item));
    //                                if (getStatus.Rows.Count > 0)
    //                                {
    //                                    var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
    //                                    if (getMsg == "EXISTS")
    //                                    {
    //                                        IsUniversity = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        IsUniversity = false;
    //                                        break;
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    IsUniversity = false;
    //                                }
    //                            }
    //                        }
    //                        if (ddlDegree != "")
    //                        {
    //                            var splitVal = ddlDegree.Split('|');
    //                            foreach (var item in splitVal)
    //                            {
    //                                var getStatus = spm.CheckIsIdsExists("CheckDegreeExists", getEmpId, Convert.ToInt32(item));
    //                                if (getStatus.Rows.Count > 0)
    //                                {
    //                                    var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
    //                                    if (getMsg == "EXISTS")
    //                                    {
    //                                        IsDegree = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        IsDegree = false;
    //                                        break;
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    IsDegree = false;
    //                                }
    //                            }
    //                        }
    //                        if (ddlProjectType != "")
    //                        {
    //                            var splitVal = ddlProjectType.Split('|');
    //                            foreach (var item in splitVal)
    //                            {
    //                                var getStatus = spm.CheckIsIdsExists("CheckProjectTypeExists", getEmpId, Convert.ToInt32(item));
    //                                if (getStatus.Rows.Count > 0)
    //                                {
    //                                    var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
    //                                    if (getMsg == "EXISTS")
    //                                    {
    //                                        IsProjectType = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        IsProjectType = false;
    //                                        break;
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    IsProjectType = false;
    //                                }
    //                            }
    //                        }
    //                        if (ddlCertification != "")
    //                        {
    //                            var splitVal = ddlCertification.Split('|');
    //                            foreach (var item in splitVal)
    //                            {
    //                                var getStatus = spm.CheckIsIdsExists("CheckCertificationExists", getEmpId, Convert.ToInt32(item));
    //                                if (getStatus.Rows.Count > 0)
    //                                {
    //                                    var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
    //                                    if (getMsg == "EXISTS")
    //                                    {
    //                                        IsCertification = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        IsCertification = false;
    //                                        break;
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    IsCertification = false;
    //                                }
    //                            }
    //                        }
    //                        if (ddlRole != "")
    //                        {
    //                            var splitVal = ddlRole.Split('|');
    //                            foreach (var item in splitVal)
    //                            {
    //                                var getStatus = spm.CheckIsIdsExists("CheckRoleExists", getEmpId, Convert.ToInt32(item));
    //                                if (getStatus.Rows.Count > 0)
    //                                {
    //                                    var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
    //                                    if (getMsg == "EXISTS")
    //                                    {
    //                                        IsRole = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        IsRole = false;
    //                                        break;
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    IsRole = false;
    //                                }
    //                            }
    //                        }
    //                        if (ddlDomain != "")
    //                        {
    //                            var splitVal = ddlDomain.Split('|');
    //                            foreach (var item in splitVal)
    //                            {
    //                                var getStatus = spm.CheckIsIdsExists("CheckDomainExists", getEmpId, Convert.ToInt32(item));
    //                                if (getStatus.Rows.Count > 0)
    //                                {
    //                                    var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
    //                                    if (getMsg == "EXISTS")
    //                                    {
    //                                        IsDomain = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        IsDomain = false;
    //                                        break;
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    IsDomain = false;
    //                                }
    //                            }
    //                        }
    //                        if (ddlOrganisationName != "")
    //                        {
    //                            var splitVal = ddlOrganisationName.Split('|');
    //                            foreach (var item in splitVal)
    //                            {
    //                                var getStatus = spm.CheckIsIdsExists("CheckOrganisationNameExists", getEmpId, Convert.ToInt32(item));
    //                                if (getStatus.Rows.Count > 0)
    //                                {
    //                                    var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
    //                                    if (getMsg == "EXISTS")
    //                                    {
    //                                        IsOrganisationName = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        IsOrganisationName = false;
    //                                        break;
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    IsOrganisationName = false;
    //                                }
    //                            }
    //                        }
    //                        if (ddlOrganisationType != "")
    //                        {
    //                            var splitVal = ddlOrganisationType.Split('|');
    //                            foreach (var item in splitVal)
    //                            {
    //                                var getStatus = spm.CheckIsIdsExists("CheckOrganisationTypeExists", getEmpId, Convert.ToInt32(item));
    //                                if (getStatus.Rows.Count > 0)
    //                                {
    //                                    var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
    //                                    if (getMsg == "EXISTS")
    //                                    {
    //                                        IsOrganisationName = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        IsOrganisationName = false;
    //                                        break;
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    IsOrganisationName = false;
    //                                }
    //                            }
    //                        }
    //                        if (ddlIndustryType != "")
    //                        {
    //                            var splitVal = ddlIndustryType.Split('|');
    //                            foreach (var item in splitVal)
    //                            {
    //                                var getStatus = spm.CheckIsIdsExists("CheckIndustryTypeExists", getEmpId, Convert.ToInt32(item));
    //                                if (getStatus.Rows.Count > 0)
    //                                {
    //                                    var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
    //                                    if (getMsg == "EXISTS")
    //                                    {
    //                                        IsIndustryType = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        IsIndustryType = false;
    //                                        break;
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    IsIndustryType = false;
    //                                }
    //                            }
    //                        }
    //                        if (year != null)
    //                        {
    //                            var getStatus = spm.CheckIsIdsExists("CheckYearExists", getEmpId, Convert.ToInt32(year));
    //                            if (getStatus.Rows.Count > 0)
    //                            {
    //                                var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
    //                                if (getMsg == "EXISTS")
    //                                {
    //                                    Isyear = true;
    //                                }
    //                                else
    //                                {
    //                                    Isyear = false;
    //                                    break;
    //                                }
    //                            }
    //                            else
    //                            {
    //                                Isyear = false;
    //                            }
    //                        }
    //                        if (overAllFrom != null)
    //                        {
    //                            if (overAllFrom >= Convert.ToDecimal(getOverallWorkExperience) && overAllTo <= Convert.ToDecimal(getOverallWorkExperience))
    //                            {
    //                                IsoverAllFrom = true;
    //                            }
    //                            else
    //                            {
    //                                IsoverAllFrom = false;
    //                            }
    //                        }
    //                        if (domainFrom != null)
    //                        {
    //                            if (domainFrom >= Convert.ToDecimal(getTotalDomainExperience) && domainTo <= Convert.ToDecimal(getTotalDomainExperience))
    //                            {
    //                                IsdomainFrom = true;
    //                            }
    //                            else
    //                            {
    //                                IsdomainFrom = false;
    //                            }
    //                        }
    //                        if (projectFrom != null)
    //                        {
    //                            if (projectFrom >= Convert.ToDecimal(getTotalSAPExperience) && projectTo <= Convert.ToDecimal(getTotalSAPExperience))
    //                            {
    //                                IsprojectFrom = true;
    //                            }
    //                            else
    //                            {
    //                                IsprojectFrom = false;
    //                            }
    //                        }

    //                        if (ddlRegion != "")
    //                        {
    //                            var splitVal = ddlRegion.Split('|');
    //                            foreach (var item in splitVal)
    //                            {
    //                                var getStatus = spm.CheckIsIdsExists("CheckRegionExists", getEmpId, Convert.ToInt32(item));
    //                                if (getStatus.Rows.Count > 0)
    //                                {
    //                                    var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
    //                                    if (getMsg == "EXISTS")
    //                                    {
    //                                        IsRegion = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        IsRegion = false;
    //                                        break;
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    IsRegion = false;
    //                                }
    //                            }
    //                        }
    //                        if (ddlStream != "")
    //                        {
    //                            var splitVal = ddlStream.Split('|');
    //                            foreach (var item in splitVal)
    //                            {
    //                                var getStatus = spm.CheckIsIdsExists("CheckStreamExists", getEmpId, Convert.ToInt32(item));
    //                                if (getStatus.Rows.Count > 0)
    //                                {
    //                                    var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
    //                                    if (getMsg == "EXISTS")
    //                                    {
    //                                        IsStream = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        IsStream = false;
    //                                        break;
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    IsStream = false;
    //                                }
    //                            }
    //                        }
    //                        if (IsDepartment == true && IsBand == true && IsModule == true && IsDesignation == true && IsEmployee == true && IsProject == true && IsQualification == true && IsUniversity == true && IsDegree == true && IsProjectType == true && IsCertification == true && IsRole == true && IsDomain == true && IsOrganisationName == true && IsOrganisationType == true && IsIndustryType == true && Isyear == true && IsoverAllFrom == true && IsdomainFrom == true && IsprojectFrom == true && IsStream == true && IsRegion == true)
    //                        {
    //                            item1["Criteria"] = "Compliant";
    //                        }
    //                    }

    //                    if (getDetails.Rows.Count > 0)
    //                    {
    //                        var newTable = new DataTable();
    //                        newTable.Columns.Add("Employee Code");
    //                        newTable.Columns.Add("Employee Name");
    //                        newTable.Columns.Add("Department");
    //                        newTable.Columns.Add("Designation");
    //                        newTable.Columns.Add("Band");
    //                        newTable.Columns.Add("Project");
    //                        newTable.Columns.Add("Highest Qualification");
    //                        newTable.Columns.Add("Primary Module");
    //                        newTable.Columns.Add("Secondary Module");
    //                        newTable.Columns.Add("Date Of Joining");
    //                        newTable.Columns.Add("Total SAP Experience");
    //                        newTable.Columns.Add("Total Domain Experience");
    //                        newTable.Columns.Add("Over all Work Experience");
    //                        newTable.Columns.Add("Full Life Cycle Implementation");
    //                        newTable.Columns.Add("Rollout");
    //                        newTable.Columns.Add("Migration");
    //                        newTable.Columns.Add("Support");
    //                        newTable.Columns.Add("Enhancements");
    //                        newTable.Columns.Add("Others");
    //                        newTable.Columns.Add("Criteria");

    //                        foreach (DataRow item in getDetails.Rows)
    //                        {
    //                            DataRow _dr = newTable.NewRow();
    //                            _dr["Employee Code"] = item["Emp_Code"].ToString();
    //                            _dr["Employee Name"] = item["Emp_Name"].ToString();
    //                            _dr["Department"] = item["Department"].ToString();
    //                            _dr["Designation"] = item["Designation"].ToString();
    //                            _dr["Band"] = item["grade"].ToString();
    //                            _dr["Project"] = item["emp_projectName"].ToString();
    //                            _dr["Highest Qualification"] = item["HighestQualification"].ToString();
    //                            _dr["Primary Module"] = item["Module1"].ToString();//Primary Module/ Secondry
    //                            _dr["Secondary Module"] = item["SecondaryModule"].ToString();//Primary Module/ Secondry
    //                            _dr["Date Of Joining"] = item["DOJ"].ToString();
    //                            _dr["Total SAP Experience"] = item["TotalSAPExperience"].ToString();
    //                            _dr["Total Domain Experience"] = item["TotalDomainExperience"].ToString();
    //                            _dr["Over all Work Experience"] = item["OverallWorkExperience"].ToString();
    //                            _dr["Full Life Cycle Implementation"] = item["FLCI"].ToString();
    //                            _dr["Rollout"] = item["Rollout"].ToString();
    //                            _dr["Migration"] = item["Migration"].ToString();
    //                            _dr["Support"] = item["Support"].ToString();
    //                            _dr["Enhancements"] = item["Enhancements"].ToString();
    //                            _dr["Others"] = item["Others"].ToString();
    //                            _dr["Criteria"] = item["Criteria"].ToString();
    //                            newTable.Rows.Add(_dr);
    //                        }
    //                        var dateTime = DateTime.Now.ToString("ddMMyyyyHHmmss");
    //                        var excelName = "Employee CV Automation_" + dateTime;
    //                        using (XLWorkbook wb = new XLWorkbook())
    //                        {
    //                            wb.Worksheets.Add(newTable, "Employee List");
    //                            Response.Clear();
    //                            Response.Buffer = true;
    //                            Response.Charset = "";
    //                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //                            Response.AddHeader("content-disposition", "attachment;filename=" + excelName + ".xlsx");
    //                            using (MemoryStream MyMemoryStream = new MemoryStream())
    //                            {
    //                                wb.SaveAs(MyMemoryStream);
    //                                MyMemoryStream.WriteTo(Response.OutputStream);
    //                                Response.Flush();
    //                                Response.End();
    //                            }
    //                        }

    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblmessage.Text = ex.Message;
    //    }
    //}


    //protected void rbWildSearch_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (rbWildSearch.Checked == true)
    //        {
    //            txt_Wild_Search.Enabled = true;

    //            show1.Visible = false;
    //            show2.Visible = false;
    //            show3.Visible = false;
    //            show4.Visible = false;
    //            show5.Visible = false;
    //            show6.Visible = false;
    //            show7.Visible = false;
    //            showCHK1.Visible = false;
    //            show8.Visible = false;
    //            show9.Visible = false;
    //            show10.Visible = false;
    //            show11.Visible = false;
    //            show12.Visible = false;
    //            show13.Visible = false;
    //            show14.Visible = false;
    //            show15.Visible = false;
    //            show16.Visible = false;
    //            show17.Visible = false;
    //            show18.Visible = false;
    //            show19.Visible = false;
    //            show20.Visible = false;
    //            show21.Visible = false;
    //            show22.Visible = false;
    //            show23.Visible = false;
    //            show24.Visible = false;
    //            show25.Visible = false;
    //            show26.Visible = false;
    //            show27.Visible = false;
    //            show28.Visible = false;
    //            show29.Visible = false;
    //            show30.Visible = false;
    //            show31.Visible = false;
    //            show32.Visible = false;
    //            show33.Visible = false;
    //        }
    //        else if (rbNormalSearch.Checked == true)
    //        {
    //            txt_Wild_Search.Enabled = false;
    //            show1.Visible = true;
    //            show2.Visible = true;
    //            show3.Visible = true;
    //            show4.Visible = true;
    //            show5.Visible = true;
    //            show6.Visible = true;
    //            show7.Visible = true;
    //            showCHK1.Visible = true;
    //            show8.Visible = true;
    //            show9.Visible = true;
    //            show10.Visible = true;
    //            show11.Visible = true;
    //            show12.Visible = true;
    //            show13.Visible = true;
    //            show14.Visible = true;
    //            show15.Visible = true;
    //            show16.Visible = true;
    //            show17.Visible = true;
    //            show18.Visible = true;
    //            show19.Visible = true;
    //            show20.Visible = true;
    //            show21.Visible = true;
    //            show22.Visible = true;
    //            show23.Visible = true;
    //            show24.Visible = true;
    //            show25.Visible = true;
    //            show26.Visible = true;
    //            show27.Visible = true;
    //            show28.Visible = true;
    //            show29.Visible = true;
    //            show30.Visible = true;
    //            show31.Visible = true;
    //            show32.Visible = true;
    //            show33.Visible = true;
    //        }
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}

    //private void WildSearch()
    //{
    //    try
    //    {
    //        var getSearch = txt_Wild_Search.Text.Trim();
    //        if (getSearch != "")
    //        {
    //            var result = spm.getCVDetailInbox(0, "GetInboxDetailsWildSearch", "", "", "", "", "", null, null, "", "", "", "", "", "", "", "", "", "", "", "", null, null, null, null, null, "", getSearch,false);
    //            if (result != null)
    //            {
    //                gv_EmployeeDetails.DataSource = null;
    //                gv_EmployeeDetails.DataBind();
    //                if (result.Tables.Count > 0)
    //                {
    //                    var getDetails = result.Tables[0];
    //                    if (getDetails != null)
    //                    {
    //                        if (getDetails.Rows.Count > 0)
    //                        {
    //                            gv_EmployeeDetails.DataSource = getDetails;
    //                            gv_EmployeeDetails.DataBind();
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
    //private void WildSearchDownload()
    //{
    //    try
    //    {
    //        var getSearch = txt_Wild_Search.Text.Trim();
    //        if (getSearch != "")
    //        {
    //            var result = spm.getCVDetailInbox(0, "GetInboxDetailsWildSearch", "", "", "", "", "", null, null, "", "", "", "", "", "", "", "", "", "", "", "", null, null, null, null, null, "", getSearch,false);
    //            if (result != null)
    //            {
    //                if (result.Tables.Count > 0)
    //                {
    //                    var getDetails = result.Tables[0];
    //                    if (getDetails != null)
    //                    {
    //                        if (getDetails.Rows.Count > 0)
    //                        {
    //                            var newTable = new DataTable();
    //                            newTable.Columns.Add("Employee Code");
    //                            newTable.Columns.Add("Employee Name");
    //                            newTable.Columns.Add("Department");
    //                            newTable.Columns.Add("Designation");
    //                            newTable.Columns.Add("Band");
    //                            newTable.Columns.Add("Project");
    //                            newTable.Columns.Add("Highest Qualification");
    //                            newTable.Columns.Add("Primary Module");
    //                            newTable.Columns.Add("Secondary Module");
    //                            newTable.Columns.Add("Date Of Joining");
    //                            newTable.Columns.Add("Total SAP Experience");
    //                            newTable.Columns.Add("Total Domain Experience");
    //                            newTable.Columns.Add("Over all Work Experience");
    //                            newTable.Columns.Add("Full Life Cycle Implementation");
    //                            newTable.Columns.Add("Rollout");
    //                            newTable.Columns.Add("Migration");
    //                            newTable.Columns.Add("Support");
    //                            newTable.Columns.Add("Enhancements");
    //                            newTable.Columns.Add("Others");
    //                            newTable.Columns.Add("Criteria");

    //                            foreach (DataRow item in getDetails.Rows)
    //                            {
    //                                DataRow _dr = newTable.NewRow();
    //                                _dr["Employee Code"] = item["Emp_Code"].ToString();
    //                                _dr["Employee Name"] = item["Emp_Name"].ToString();
    //                                _dr["Department"] = item["Department"].ToString();
    //                                _dr["Designation"] = item["Designation"].ToString();
    //                                _dr["Band"] = item["grade"].ToString();
    //                                _dr["Project"] = item["emp_projectName"].ToString();
    //                                _dr["Highest Qualification"] = item["HighestQualification"].ToString();
    //                                _dr["Primary Module"] = item["Module1"].ToString();//Primary Module/ Secondry
    //                                _dr["Secondary Module"] = item["SecondaryModule"].ToString();//Primary Module/ Secondry
    //                                _dr["Date Of Joining"] = item["DOJ"].ToString();
    //                                _dr["Total SAP Experience"] = item["TotalSAPExperience"].ToString();
    //                                _dr["Total Domain Experience"] = item["TotalDomainExperience"].ToString();
    //                                _dr["Over all Work Experience"] = item["OverallWorkExperience"].ToString();
    //                                _dr["Full Life Cycle Implementation"] = item["FLCI"].ToString();
    //                                _dr["Rollout"] = item["Rollout"].ToString();
    //                                _dr["Migration"] = item["Migration"].ToString();
    //                                _dr["Support"] = item["Support"].ToString();
    //                                _dr["Enhancements"] = item["Enhancements"].ToString();
    //                                _dr["Others"] = item["Others"].ToString();
    //                                _dr["Criteria"] = item["Criteria"].ToString();
    //                                newTable.Rows.Add(_dr);
    //                            }
    //                            var dateTime = DateTime.Now.ToString("ddMMyyyyHHmmss");
    //                            var excelName = "Employee CV Automation_" + dateTime;
    //                            using (XLWorkbook wb = new XLWorkbook())
    //                            {
    //                                wb.Worksheets.Add(newTable, "Employee List");
    //                                Response.Clear();
    //                                Response.Buffer = true;
    //                                Response.Charset = "";
    //                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //                                Response.AddHeader("content-disposition", "attachment;filename=" + excelName + ".xlsx");
    //                                using (MemoryStream MyMemoryStream = new MemoryStream())
    //                                {
    //                                    wb.SaveAs(MyMemoryStream);
    //                                    MyMemoryStream.WriteTo(Response.OutputStream);
    //                                    Response.Flush();
    //                                    Response.End();
    //                                }
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}

    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            txtToDate.Enabled = true;
            var getDate = txtFromdate.Text.ToString();
            DateTime StartDate = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime StartingDate = DateTime.ParseExact("25/04/2021", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var getDay = DateTime.DaysInMonth(StartDate.Year, StartDate.Month);
            //hdnDay.Value = (getDay-1);
            DateTime EndDate = StartDate.AddDays(30);
            if (getDay == 30)
            {
                EndDate = StartDate.AddDays(29);
            }
            else if (getDay == 28)
            {
                EndDate = StartDate.AddDays(27);
            }
            else if (getDay == 29)
            {
                EndDate = StartDate.AddDays(28);
            }
            var getDayTotal = (getDay - 1);
            var SelectEndDate = EndDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            SelectEndDate = SelectEndDate.Replace('-', '/');
            txtToDate.Text = SelectEndDate;
            DateTime SelectedDate = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime Today = DateTime.Now;
            if (StartingDate > SelectedDate)
            {
                lblmessage.Text = "Please select date from 25 April 2021.";
                //txtFromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromdate.Text = "";
                txtToDate.Text = "";
                return;
            }
            else if (Today < SelectedDate)
            {
                lblmessage.Text = "Please select date only today or less than today.";
                txtFromdate.Text = "";
                txtToDate.Text = "";
                return;
            }
            else
            {
                lblmessage.Text = "";
            }
            //var ToDate = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            //txtToDate.Text = ToDate;
        }
        catch (Exception ex)
        {

        }

    }

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        try
        {
            var getDate = txtFromdate.Text.ToString();
            var getDate1 = txtToDate.Text.ToString();
            DateTime SelectedDate = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime Today = DateTime.ParseExact(getDate1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            // DateTime Today = DateTime.Now;
            if (Today < SelectedDate)
            {
                lblmessage.Text = "Please select to date greater than from date";
                //txtFromdate.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                txtToDate.Text = "";
                return;
            }
            var Today1 = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            Today = DateTime.ParseExact(Today1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var toDate1 = DateTime.ParseExact(getDate1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (Today <= toDate1)
            {
                lblmessage.Text = "Please select date less than today";
                txtToDate.Text = "";
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddl_Employee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var ddlEmployee = string.Empty;
            if (ddl_Employee.SelectedValue != "" && ddl_Employee.SelectedValue != "0")
            {
                ddlEmployee = ddl_Employee.SelectedValue;
            }
            var ddlSelectedMonth = ddlMonthYear.SelectedValue;
            if (!CheckIsExist(ddlEmployee, ddlSelectedMonth))
            {
                lblmessage.Text = "Report already generated.";
                var getSelectedVal = ddl_Employee.SelectedValue;
                ddl_Employee.Items.FindByValue(getSelectedVal).Selected = false;
                ddl_Employee.Items.FindByValue("0").Selected = true;
                getSelectedVal = ddlMonthYear.SelectedValue;
                ddlMonthYear.Items.FindByValue(getSelectedVal).Selected = false;
                ddlMonthYear.Items.FindByValue("0").Selected = true;
                txtFromdate.Text = "";
                txtToDate.Text = "";
                return;
            }
            var getDetails = spm.GetOTRequestEmployeeDetails("getEmployeeDetails", ddlEmployee, "", "");
            if (getDetails != null)
            {
                if (getDetails.Rows.Count > 0)
                {
                    txt_Location.Text = Convert.ToString(getDetails.Rows[0]["Emp_ProjectName"]);
                    txt_Designation.Text = Convert.ToString(getDetails.Rows[0]["Designation"]);
                    txt_ShiftTime.Text = "In Range:- " + Convert.ToString(getDetails.Rows[0]["In_range"]) + " - Out Range:- " + Convert.ToString(getDetails.Rows[0]["Out_range"]);
                }
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message;
            return;
        }
    }

    protected void lnk_ed_Search_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch (Exception ex)
        {
        }
    }

    protected void lnk_LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            var ddlEmployee = string.Empty;
            var ddlSelectedMonth = string.Empty;
            if (ddl_Employee.SelectedValue == "" || ddl_Employee.SelectedValue == "0")
            {
                lblmessage.Text = "Please select employee name";
                return;
            }
            ddlEmployee = ddl_Employee.SelectedValue;
            if (ddlMonthYear.SelectedValue == "" || ddlMonthYear.SelectedValue == "0")
            {
                lblmessage.Text = "Please select month ";
                return;
            }
            ddlSelectedMonth = ddlMonthYear.SelectedValue;
            if (!CheckIsExist(ddlEmployee, ddlSelectedMonth))
            {
                lblmessage.Text = "Report already generated.";
                var getSelectedVal = ddl_Employee.SelectedValue;
                ddl_Employee.Items.FindByValue(getSelectedVal).Selected = false;
                ddl_Employee.Items.FindByValue("0").Selected = true;
                getSelectedVal = ddlMonthYear.SelectedValue;
                ddlMonthYear.Items.FindByValue(getSelectedVal).Selected = false;
                ddlMonthYear.Items.FindByValue("0").Selected = true;
                txtFromdate.Text = "";
                txtToDate.Text = "";
                return;
            }
            if (txtFromdate.Text == "")
            {
                lblmessage.Text = "Please select from date";
                return;
            }
            if (txtToDate.Text == "")
            {
                lblmessage.Text = "Please select to date";
                return;
            }
            if (txt_OTAmounttobepaid.Text == "")
            {
                lblmessage.Text = "Please enter OT amount to be paid.";
                return;
            }
            if (txtOverAllRemark.Text == "")
            {
                lblmessage.Text = "Please enter over all remark.";
                return;
            }
            var CreatedBy = Convert.ToString(Session["Empcode"]);
            var emp_Code = Convert.ToString(ddl_Employee.SelectedValue);
            var getEligibleOTHours = txt_EligibleOTHours.Text;
            var getEligiblePayableOT = txt_EligiblePayableOT.Text;
            var getOTAmounttobepaid = txt_OTAmounttobepaid.Text;
            var getOverAllRemark = txtOverAllRemark.Text;
            var getTotalWorkingHR = hdnTotalHR.Value;
            var getTotalOverTimeHR = hdnTotalOverTimeHR.Value;
            var getSelectedMonth = ddlMonthYear.SelectedValue;
            var strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            var strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

            strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
            var strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

            var ToBePaidAmount = Convert.ToDecimal(getOTAmounttobepaid);
            var EligiblePayableOT = Convert.ToDecimal(getEligiblePayableOT);
            if(ToBePaidAmount> EligiblePayableOT)
            {
                lblmessage.Text = "Please Enter amount to be less than Eligible Payable OT";
                return;
            }
           // return;
            var getApp_Id = "";
            //creating DataTable  
            var getId = spm.Insert_OT_Request("INSERT", emp_Code, getEligibleOTHours, getEligiblePayableOT, getOTAmounttobepaid, CreatedBy, getOverAllRemark, strfromDate, strToDate, getSelectedMonth);
            if (getId != null)
            {
                getApp_Id = Convert.ToString(getId);
            }
            DataTable dt = new DataTable();
            DataRow dr;
            dt.TableName = "OT_Report_Details";
            //creating columns for DataTable  
            dt.Columns.Add(new DataColumn("App_Id", typeof(decimal)));
            dt.Columns.Add(new DataColumn("Emp_Code", typeof(string)));
            dt.Columns.Add(new DataColumn("Att_Date", typeof(string)));
            dt.Columns.Add(new DataColumn("In_Time_Out_Time", typeof(string)));
            dt.Columns.Add(new DataColumn("In_Out_Status", typeof(string)));
            dt.Columns.Add(new DataColumn("Leave_Type", typeof(string)));
            dt.Columns.Add(new DataColumn("Leave_Status", typeof(string)));
            dt.Columns.Add(new DataColumn("Reason", typeof(string)));
            dt.Columns.Add(new DataColumn("WorkingHours", typeof(string)));
            dt.Columns.Add(new DataColumn("TotalWorkingHours", typeof(string)));
            dt.Columns.Add(new DataColumn("OverTime", typeof(string)));
            dt.Columns.Add(new DataColumn("TotalOverTime", typeof(string)));
            dt.Columns.Add(new DataColumn("Remark", typeof(string)));
            dt.Columns.Add(new DataColumn("CreatedBy", typeof(string)));
            dt.Columns.Add(new DataColumn("CreatedOn", typeof(DateTime)));
            DataRow drCurrentRow = null;
            foreach (GridViewRow rows in gv_EmployeeDetails.Rows)
            {
                var date = rows.Cells[0].Text.Trim();
                if (date != "")
                {
                    var InTimeOutTime = rows.Cells[1].Text.ToString().Trim();
                    var InOutStatus = rows.Cells[2].Text.ToString().Trim();
                    var LeaveType = rows.Cells[3].Text.ToString().Trim().TrimStart().TrimEnd();
                    var LeaveStatus = rows.Cells[4].Text.ToString().Trim();

                    var LeaveReason = "";
                    var getRES = (HiddenField)rows.FindControl("hdnReason");
                    LeaveReason = getRES.Value.ToString().Trim();

                    var WorkingHours = "";
                    var getWH = (HiddenField)rows.FindControl("hdnWorkingHours");
                    WorkingHours = getWH.Value.ToString().Trim();

                    var OverTimeHours = "";
                    var getOTime = (HiddenField)rows.FindControl("hdnOverTime");
                    OverTimeHours = getOTime.Value.ToString().Trim();

                    TextBox remark = (TextBox)rows.FindControl("txt_Remark_Enter");
                    var getRemark = remark.Text.ToString().Trim();

                    drCurrentRow = dt.NewRow();
                    drCurrentRow["App_Id"] = Convert.ToDecimal(getApp_Id);
                    drCurrentRow["Emp_Code"] = emp_Code;
                    drCurrentRow["Att_Date"] = date;
                    drCurrentRow["In_Time_Out_Time"] = InTimeOutTime.Replace("&nbsp;", "").Trim();
                    drCurrentRow["In_Out_Status"] = InOutStatus.Replace("&nbsp;", "").Trim();
                    drCurrentRow["Leave_Type"] = LeaveType.Replace("&nbsp;", "").Trim();
                    drCurrentRow["Leave_Status"] = LeaveStatus.Replace("&nbsp;", "").Trim();
                    drCurrentRow["Reason"] = LeaveReason.Trim();
                    drCurrentRow["WorkingHours"] = WorkingHours;
                    drCurrentRow["TotalWorkingHours"] = getTotalWorkingHR;
                    drCurrentRow["OverTime"] = OverTimeHours;
                    drCurrentRow["TotalOverTime"] = getTotalOverTimeHR;
                    drCurrentRow["Remark"] = getRemark;
                    drCurrentRow["CreatedBy"] = CreatedBy;
                    drCurrentRow["CreatedOn"] = Convert.ToDateTime(DateTime.Now);
                    dt.Rows.Add(drCurrentRow);
                }
            }
            spm.BulkInsertToOTRequest(dt);
            //Send EmailTo Manager
            var getReportingManagerDetails = spm.GetOTRequestEmployeeDetails("getOTManagerDetails", "", "", "");
            if (getReportingManagerDetails != null)
            {
                if (getReportingManagerDetails.Rows.Count > 0)
                {
                    var getSelectedM = ddlMonthYear.SelectedValue;
                    var replaceMonth = getSelectedM.Replace("/", "-");
                    var getEmpName = ddl_Employee.SelectedItem.Text;
                    var splitEmpName = getEmpName.Split('-');
                    var FinalEmpName = splitEmpName[splitEmpName.Length - 1];
                    var reportingManagerName = Convert.ToString(Session["emp_loginName"]);
                    var getToEmailAddress = Convert.ToString(getReportingManagerDetails.Rows[0]["Emp_Emailaddress"]);
                    var getEmp_Name = Convert.ToString(getReportingManagerDetails.Rows[0]["Emp_Name"]);
                    var link = "http://localhost/hrms/procs/Employee_OT_Request.aspx?reqid=" + getApp_Id;
                    var subject = "OT calculation - "+ FinalEmpName + " for the month of "+ replaceMonth + "";
                    spm.send_mailto_OTReportManager(getEmp_Name, getToEmailAddress, subject, link,FinalEmpName, replaceMonth, reportingManagerName);
                }
            }
            Response.Redirect("~/procs/InboxOTRequest.aspx");
            //End Report Manager
            //send_mailto_OTReportManager
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnk_LinkButton2_Click(object sender, EventArgs e)
    {
        try
        {
            var getId = hdnApp_Id.Value;
            var getDs = spm.getOTReport(getId, "Get_Emp_Details_Report");
            DownlodPDF(getDs);
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlMonthYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            var getDDLGetVal = ddlMonthYear.SelectedValue.ToString();
            if (getDDLGetVal != "0")
            {
                lblmessage.Text = "";
                var getDate = txtFromdate.Text.ToString();
                getDDLGetVal = "25/" + getDDLGetVal;
                DateTime StartDate = DateTime.ParseExact(getDDLGetVal, "dd/MMM/yyyy", CultureInfo.InvariantCulture).AddMonths(-1);
                DateTime StartingDate = DateTime.ParseExact("25/04/2021", "dd/MM/yyyy", CultureInfo.InvariantCulture).AddMonths(-1);

                var getDay = DateTime.DaysInMonth(StartDate.Year, StartDate.Month);
                //hdnDay.Value = (getDay-1);
                DateTime EndDate = StartDate.AddDays(30);
                if (getDay == 30)
                {
                    EndDate = StartDate.AddDays(29);
                }
                else if (getDay == 28)
                {
                    EndDate = StartDate.AddDays(27);
                }
                else if (getDay == 29)
                {
                    EndDate = StartDate.AddDays(28);
                }
                var getDayTotal = (getDay - 1);
                // hdnDay.Value = getDayTotal.ToString();
                var SelectEndDate = EndDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                var SelectStartDate = StartDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                SelectEndDate = SelectEndDate.Replace('-', '/');
                SelectStartDate = SelectStartDate.Replace('-', '/');
                txtToDate.Text = SelectEndDate;
                txtFromdate.Text = SelectStartDate;
                DateTime SelectedDate = DateTime.ParseExact(SelectStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime Today = DateTime.Now;
                if (StartingDate > SelectedDate)
                {
                    lblmessage.Text = "Please select date from 25 Apr 2021 ";
                    //txtFromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFromdate.Text = "";
                    txtToDate.Text = "";
                }
                else if (Today < SelectedDate)
                {
                    lblmessage.Text = "Please select current month only or less than current month";
                    //txtFromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFromdate.Text = "";
                    txtToDate.Text = "";
                }
                else
                {
                    lblmessage.Text = "";
                }
            }
            else
            {
                lblmessage.Text = "Please Select Month And Year";
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void GetYearList()
    {
        try
        {
            var getDtMonthdata = ToDataTable(GetMonths(Convert.ToDateTime("2021-04-25"), Convert.ToDateTime(DateTime.Now.Date)));
            if (getDtMonthdata.Rows.Count > 0)
            {
                ddlMonthYear.DataSource = null;
                ddlMonthYear.DataSource = getDtMonthdata;
                //ddlDepartment.DataBind();
                ddlMonthYear.DataTextField = "MonthYear";
                ddlMonthYear.DataValueField = "MonthYear";
                ddlMonthYear.DataBind();
                ddlMonthYear.Items.Insert(0, new ListItem("Select Month Year", "0")); //updated code

            }
            else
            {
                ddlMonthYear.DataSource = null;
            }
        }
        catch (Exception)
        {

            throw;
        }

    }
    public static List<string> GetMonths(DateTime date1, DateTime date2)
    {
        //Note - You may change the format of date as required.  
        return GetDates(date1, date2).OrderByDescending(s => s.Month).OrderByDescending(s => s.Year).Select(x => x.ToString("MMM/yyyy", CultureInfo.InvariantCulture)).ToList();
    }
    public static IEnumerable<DateTime> GetDates(DateTime date1, DateTime date2)
    {
        while (date1 <= date2)
        {
            yield return date1;
            date1 = date1.AddMonths(1);
        }
        if (date1 > date2 && date1.Month == date2.Month)
        {
            // Include the last month  
            yield return date1;
        }
    }
    public static DataTable ToDataTable<T>(IList<T> data)
    {
        PropertyDescriptorCollection props =
        TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();

        table.Columns.Add("MonthYear");

        object[] values = new object[props.Count];
        foreach (T item in data)
        {
            table.Rows.Add(item.ToString());
        }
        return table;
    }
    public bool CheckIsExist(string emp_Code, string selectedMonth)
    {
        var result = false;
        try
        {
            var getResult = spm.GetOTRequestEmployeeDetails("CheckIsExistMonth", emp_Code, selectedMonth, "");
            if (getResult != null)
            {
                if (getResult.Rows.Count > 0)
                {
                    var getStatus = Convert.ToString(getResult.Rows[0]["MESSAGE"]);
                    if (getStatus == "Yes")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            result = false;
        }
        return result;
    }

}