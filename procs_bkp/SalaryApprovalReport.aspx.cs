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
using System.Text;
using System.IO;
using ClosedXML.Excel;
using System.Data.Common;
using System.Data.OleDb;
using Microsoft.Reporting.WebForms;

public partial class procs_SalaryApprovalReport : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    public DataTable dt, dtExitSurvey, dtEmpCode;
    string Emp_Code;
    bool hasKeys;
    string monthyear;
    int SalaryStatus;
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
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

            if (!Page.IsPostBack)
            {
                //LoadProcessedData(Emp_Code, monthyear,SalaryStatus);
                FillMonthYear();
                FillRMEMP();
            }

        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    public void FillMonthYear()
    {
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
        spars[0].Value = "GetMonthyear";

        DataTable dt = spm.getDropdownList(spars, "spGetEmpDetailsForSalary");
        ddl_MonthYear.DataSource = dt;

        ddl_MonthYear.DataTextField = "MonthYearDesc";
        ddl_MonthYear.DataValueField = "MonthAndYear";
        ddl_MonthYear.DataBind();
        ListItem item = new ListItem("Select Month Year", "0");
        ddl_MonthYear.Items.Insert(0, item);
    }

    public void FillRMEMP()
    {
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
        spars[0].Value = "GetRMEMP";

        DataTable dt = spm.getDropdownList(spars, "spGetEmpDetailsForSalary");
        dlRMList.DataSource = dt;

        dlRMList.DataTextField = "RMName";
        dlRMList.DataValueField = "RM_EMPCODE";
        dlRMList.DataBind();
        ListItem item = new ListItem("Select RM", "0");
        dlRMList.Items.Insert(0, item);
    }
    public void LoadProcessedData(string empCode, string monthYear, int status)
    {
        DataSet ds = new DataSet();
        ds = spm.GetSalaryApprovalReport(empCode, monthYear, status);
        //dgSalApproved.DataSource = null;
        //dgSalApproved.DataBind();
        if (ds.Tables.Count > 0)
        {
            //dgSalApproved.Visible = true;
            //dgSalApproved.DataSource = dt;
            //dgSalApproved.DataBind();
            //btnExport.Visible = true;
            rptvwrSalaryStatus.Visible = true;
            rptvwrSalaryStatus.LocalReport.Refresh();
            rptvwrSalaryStatus.LocalReport.DataSources.Clear();

            ReportDataSource rdsAsset = new ReportDataSource("SalaryStatusRM", ds.Tables[0]);
            ReportDataSource rdsAsset_detail = new ReportDataSource("SalaryStatusEMP", ds.Tables[1]);

            rptvwrSalaryStatus.ProcessingMode = ProcessingMode.Local;
            rptvwrSalaryStatus.LocalReport.ReportPath = Server.MapPath("~/procs/SalaryStatusUpdateReport.rdlc");
            rptvwrSalaryStatus.LocalReport.DataSources.Add(rdsAsset);
            rptvwrSalaryStatus.LocalReport.DataSources.Add(rdsAsset_detail);

        }
        else
        {
            //btnExport.Visible = false;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (dlRMList.SelectedIndex > 0)
        {
            Emp_Code = Convert.ToString(dlRMList.SelectedValue);
        }
        if (ddl_MonthYear.SelectedIndex > 0)
        {
            monthyear = Convert.ToString(ddl_MonthYear.SelectedValue);
        }
        if (ddl_SalAppStatus.SelectedIndex > 0)
        {
            SalaryStatus = Convert.ToInt32(ddl_SalAppStatus.SelectedValue);
        }
        LoadProcessedData(Emp_Code, monthyear, SalaryStatus);
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {

    }

    protected void dgSalApproved_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewData")
        {
            LinkButton lblempcode = (LinkButton)e.CommandSource;    // the button
            GridViewRow myRow = (GridViewRow)lblempcode.Parent.Parent;  // the row
            GridView myGrid = (GridView)sender; // the gridview
            string RMEMPCode = myGrid.DataKeys[myRow.RowIndex].Value.ToString();

            //Emp_Code = lblempcode.Text;

            Response.Redirect("SalaryApproval.aspx?id=" + e.CommandArgument + "&empCode=" + RMEMPCode);
        }
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        if (dlRMList.SelectedIndex > 0)
        {
            Emp_Code = Convert.ToString(dlRMList.SelectedValue);
        }
        if (ddl_MonthYear.SelectedIndex > 0)
        {
            monthyear = Convert.ToString(ddl_MonthYear.SelectedValue);
        }
        //if (ddl_SalAppStatus.SelectedIndex > 0)
        //{
            SalaryStatus = Convert.ToInt32(ddl_SalAppStatus.SelectedValue);
        //}
        LoadProcessedData(Emp_Code, monthyear, SalaryStatus);
    }

    protected void mobile_cancel_Click(object sender, EventArgs e)
    {
        dlRMList.SelectedIndex = 0;
        ddl_MonthYear.SelectedIndex = 0;
        ddl_SalAppStatus.SelectedIndex = 0;
        rptvwrSalaryStatus.Visible = false;
        //rptvwrSalaryStatus.LocalReport.DataSources.Clear();
        //dgSalApproved.DataSource = null;
        //dgSalApproved.Visible = false;
        //btnExport.Visible = false;
    }

    protected void mobile_clear_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "SalaryApproval_"+ DateTime.Now + ".xlsx";

        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

        //dgSalApproved.GridLines = GridLines.Both;
        //dgSalApproved.HeaderStyle.Font.Bold = true;
        //dgSalApproved.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }

    //protected void btnExport_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DataTable dt = (DataTable)ViewState["Data"]; //your datatable  
    //        dt.Columns.Remove("MonthAndYear");
    //        //t.Columns.RemoveAt(6);
    //        dt.Columns.Remove("Status");
    //        //t.Columns.RemoveAt(7);

    //        string FileName = "SalaryApproval_" + DateTime.Now + ".xlsx";
    //        using (XLWorkbook wb = new XLWorkbook())
    //        {
    //            wb.Worksheets.Add(dt, "SalaryStatusData");
    //            Response.Clear();
    //            Response.Buffer = true;
    //            Response.Charset = "";
    //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //            Response.AddHeader("content-disposition", "attachment;filename="+ FileName);
    //            using (MemoryStream MyMemoryStream = new MemoryStream())
    //            {
    //                wb.SaveAs(MyMemoryStream);
    //                MyMemoryStream.WriteTo(Response.OutputStream);
    //                Response.Flush();
    //                dt.Dispose();
    //                Response.End();
    //            }
    //        }
    //    }
    //    catch (Exception ex) { }
    //}

}