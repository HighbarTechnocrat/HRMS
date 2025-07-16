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


public partial class ITAssetSummary : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    
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
            var isCEO = false;
            var isHOD = false;
            var isCOO = false;
            hflCEO.Value = "NO";
            hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();
            hflEmpCode.Value = Convert.ToString(Session["Empcode"]).Trim();
            txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            txtFromdate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");


            var getDate = DateTime.Now.ToString("dd/MM/yyyy");
            getDate = getDate.Replace('-', '/');
            txtFromdate.Text = getDate;
            GetEmployeeDetails();
            Fill_AssetCategory();
            Fill_AssetType();
            Fill_Status();
            Fill_Property();
            Fill_ddl_Employee();
            var getLoginEmpDeg = hflEmpDesignation.Value;

            //if (getLoginEmpDeg.Contains("Head"))
            //{
            //    isCEO = true;
            //    isHOD = true;
            //}
            var loginCode = Convert.ToString(hflEmpCode.Value);
            DataTable dtleaveInbox2 = new DataTable();
            dtleaveInbox2 = spm.GetCEOEmpCode();
            if (dtleaveInbox2.Rows.Count > 0)
            {
                var CeoEmpCode = Convert.ToString(dtleaveInbox2.Rows[0]["Emp_Code"]);
                if (loginCode == CeoEmpCode)
                {
                    isCEO = true;
                    hflCEO.Value = "YES";
                }
                else
                {
                    isCEO = false;
                    hflCEO.Value = "NO";
                }
            }

            dtleaveInbox2 = new DataTable();
            dtleaveInbox2 = spm.CheckEmpAsCustodian(hflEmpCode.Value);
            if (dtleaveInbox2.Rows.Count > 0)
            {
                var CeoEmpCode = Convert.ToString(dtleaveInbox2.Rows[0]["EmpCode"]);
                if (loginCode == CeoEmpCode)
                {
                    isCEO = true;
                    hflCEO.Value = "YES";
                }
                else
                {
                    isCEO = false;
                    hflCEO.Value = "NO";
                }
            }

            dtleaveInbox2 = new DataTable();
            dtleaveInbox2 = spm.CheckITHODExists(hflEmpCode.Value);
            if (dtleaveInbox2.Rows.Count > 0)
            {
                var CeoEmpCode = Convert.ToString(dtleaveInbox2.Rows[0]["HOD"]);
                if (loginCode == CeoEmpCode)
                {
                    isCEO = true;
                    hflCEO.Value = "YES";
                }
                else
                {
                    isCEO = false;
                    hflCEO.Value = "NO";
                }
            }

            //if (Convert.ToString(hflEmpCode.Value) == "00002878")// Upgupta patanaik sir as coo
            //{
            //    isCOO = true;
            //    hflCOO.Value = "YES";
            //}
            //else
            //{
            //    isCOO = false;
            //    hflCOO.Value = "NO";
            //}

            if (loginCode == "00002878")  //// Upgupta patanaik sir as coo
            {
                isCEO = true;
                hflCEO.Value = "YES";
            }
            if (Convert.ToString(hflEmpCode.Value) == "00631294")
            {
                isCEO = true;
                hflCEO.Value = "YES";
            }
        }
    }

    protected bool check_IsloginHR()
    {
        bool hr_code=false;
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_Check_HR_apprver_code";

            spars[1] = new SqlParameter("@exp_trvl", SqlDbType.VarChar);
            spars[1].Value = DBNull.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = hdnloginempcode.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");
            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                hr_code= true;
            }
            else
            {
                hr_code = false;
                
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
        return hr_code;
    }
    public void Fill_AssetTypeByAssetCategory(int AssetCategoryId)
    {
        DataTable Dt = spm.GetAssetTypeByAssetCategory(AssetCategoryId);
        ddl_AssetType.DataSource = Dt;

        ddl_AssetType.DataTextField = "AssetType";
        ddl_AssetType.DataValueField = "id";
        ddl_AssetType.DataBind();
        ListItem item = new ListItem("Select Asset Type", "0");
        ddl_AssetType.Items.Insert(0, item);
    }

    protected void ddl_AssetCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        int AssetCategoryId = 0;
        if (Convert.ToInt32(ddl_AssetCategory.SelectedValue) != 0)
        {
            AssetCategoryId = Convert.ToInt32(ddl_AssetCategory.SelectedValue);
            Fill_AssetTypeByAssetCategory(AssetCategoryId);
        }
        else
        {
            Fill_AssetType();
        }
    }

    public void GetEmployeeDetails()
    {
        try
        {
            DataTable dtEmpDetails = new DataTable();
            dtEmpDetails = spm.GetEmployeeData(Convert.ToString(hflEmpCode.Value));
            if (dtEmpDetails.Rows.Count > 0)
            {
                hflEmpDesignation.Value = Convert.ToString(dtEmpDetails.Rows[0]["DesginationName"]);
                hflEmpDepartment.Value = Convert.ToString(dtEmpDetails.Rows[0]["Department_Name"]);
                hflGrade.Value = Convert.ToString(dtEmpDetails.Rows[0]["Grade"]).Trim();
                hflEmailAddress.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

            throw;
        }
    }

   public void Fill_AssetCategory()
    {
        DataTable Dt = spm.GetAssetCategory();
        ddl_AssetCategory.DataSource = Dt;

        ddl_AssetCategory.DataTextField = "AssetCategory";
        ddl_AssetCategory.DataValueField = "id";
        ddl_AssetCategory.DataBind();
        ListItem item = new ListItem("Select Asset Category", "0");
        ddl_AssetCategory.Items.Insert(0, item);
    }

    public void Fill_AssetType()
    {
        DataTable Dt = spm.GetAssetType();
        ddl_AssetType.DataSource = Dt;

        ddl_AssetType.DataTextField = "AssetType";
        ddl_AssetType.DataValueField = "id";
        ddl_AssetType.DataBind();
        ListItem item = new ListItem("Select Asset Type", "0");
        ddl_AssetType.Items.Insert(0, item);
    }
    public void Fill_Status()
    {

        DataTable Dt = spm.GetStatus();
        ddl_Status.DataSource = Dt;

        ddl_Status.DataTextField = "Status";
        ddl_Status.DataValueField = "Statusid";
        ddl_Status.DataBind();
        ListItem item = new ListItem("Select Status", "0");
        ddl_Status.Items.Insert(0, item);

    }
    public void Fill_Property()
    {
        ListItem item = new ListItem("Select Asset Property", "0");
        ddl_AssetProperty.Items.Insert(0, item);
        item = new ListItem("Own", "Own");
        ddl_AssetProperty.Items.Insert(1, item);
        item = new ListItem("Rental", "Rental");
        ddl_AssetProperty.Items.Insert(2, item);
    }
    private void getAssetSummaryDetails()
    {
        try
        {
            #region get employee Claim details
            DataSet dsAsset = new DataSet();
            SqlParameter[] spars = new SqlParameter[7];
            string[] strdate;
            string strfromDate = "";
            string strfromDate_RPt = "";

            //@fromDate
            spars[0] = new SqlParameter("@from_date", SqlDbType.VarChar);
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                strfromDate_RPt = Convert.ToString(strdate[0]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[2]);
                spars[0].Value = strfromDate;
            }
            else
            {
                spars[0].Value = null;
            }

            //@AssetTypeId
            spars[1] = new SqlParameter("@AssetTypeId", SqlDbType.Int);
            if (Convert.ToString(ddl_AssetType.SelectedValue).Trim() != "0" && Convert.ToString(ddl_AssetType.SelectedValue).Trim() != "")
            {
                spars[1].Value = Convert.ToString(ddl_AssetType.SelectedValue);
            }
            else
            {
                spars[1].Value = null;
            }

            //@StatusId
            spars[2] = new SqlParameter("@AssetStatusId", SqlDbType.Int);
            if (Convert.ToString(ddl_Status.SelectedValue).Trim() != "0" && Convert.ToString(ddl_Status.SelectedValue).Trim() != "")
            {
                spars[2].Value = Convert.ToString(ddl_Status.SelectedValue);
            }
            else
            {
                spars[2].Value = null;
            }

            //@AssetProperty
            spars[3] = new SqlParameter("@AssetProperty", SqlDbType.VarChar);
            if (Convert.ToString(ddl_AssetProperty.SelectedValue).Trim() != "0" && Convert.ToString(ddl_AssetProperty.SelectedValue).Trim() != "")
            {
                spars[3].Value = Convert.ToString(ddl_AssetProperty.SelectedItem.Text.ToString().Trim());
            }
            else
            {
                spars[3].Value = null;
            }

            //@EmployeeName
            spars[4] = new SqlParameter("@AssignedTo", SqlDbType.VarChar);
            if (ddl_Employee.SelectedValue != "" && ddl_Employee.SelectedValue != "0")
            {
                spars[4].Value = Convert.ToString(ddl_Employee.SelectedValue);
            }
            else
            {
                spars[4].Value = null;
            }
            //@qtype
            spars[5] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[5].Value = "AssetSummaryRpt";
            //AssetCategoryId
            spars[6] = new SqlParameter("@AssetCategoryId", SqlDbType.Int);
            if (Convert.ToString(ddl_AssetCategory.SelectedValue).Trim() != "0" && Convert.ToString(ddl_AssetCategory.SelectedValue).Trim() != "")
            {
                spars[6].Value = Convert.ToString(ddl_AssetCategory.SelectedValue);
            }
            else
            {
                spars[6].Value = null;
            }
            dsAsset = spm.getDatasetList(spars, "SP_Admin_AssetInventory");

            #endregion

            ReportViewer2.LocalReport.Refresh();
            ReportViewer2.LocalReport.DataSources.Clear();
            ReportParameter[] param = new ReportParameter[1];
            param[0] = new ReportParameter("from_date", Convert.ToString(strfromDate_RPt));

            if (dsAsset.Tables[0].Rows.Count > 0)
            {
                ReportDataSource rdsAsset = new ReportDataSource("DsAssetSummary", dsAsset.Tables[0]);
                ReportDataSource rdsAsset_detail = new ReportDataSource("DsDetailSummary", dsAsset.Tables[1]);
                ReportViewer2.ProcessingMode = ProcessingMode.Local;
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/AssetSummary.rdlc");
                ReportViewer2.LocalReport.DataSources.Add(rdsAsset);
                ReportViewer2.LocalReport.DataSources.Add(rdsAsset_detail);
                ReportViewer2.LocalReport.SetParameters(param);
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
        }
    }

    public void Compare()
    {
        
    }
    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";

        if ((Convert.ToString(txtFromdate.Text).Trim() != "") && (Convert.ToString(txtToDate.Text).Trim() != ""))
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

            strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);


            DateTime startDate = Convert.ToDateTime(strfromDate);
            DateTime endDate = Convert.ToDateTime(strToDate);
            if (startDate > endDate)
            {
                lblmessage.Text = "From Date should be less than To Date ";
                txtFromdate.Text = "";

                return;
            }
            else
            {
                lblmessage.Text = "";
                // ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Start date should be greate than End date')", true);
            }
        }
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";

        if ((Convert.ToString(txtFromdate.Text).Trim() != "") && (Convert.ToString(txtToDate.Text).Trim() != ""))
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

            strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);


            DateTime startDate = Convert.ToDateTime(strfromDate);
            DateTime endDate = Convert.ToDateTime(strToDate);
            if (startDate > endDate)
            {
                lblmessage.Text = "To Date should be greater than From Date ";
                txtToDate.Text = "";
                
                return;
            }
            else
            {
                // ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Start date should be greate than End date')", true);
            }
        }
    }

    
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "Please Enter From Date";
            return;
        }
        getAssetSummaryDetails();
    }

    public void Fill_ddl_Employee()
    {
        DataTable dt = spm.GetEmployeeList();
        ddl_Employee.DataSource = dt;

        ddl_Employee.DataTextField = "empname";
        ddl_Employee.DataValueField = "Emp_Code";
        ddl_Employee.DataBind();
        ListItem item = new ListItem("Select Employee", "0");
        ddl_Employee.Items.Insert(0, item);

    }

}