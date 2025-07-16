using System;
using System.Data;
using System.Web.UI;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;

public partial class procs_MIS_OnboardingSeparationNew : System.Web.UI.Page
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
            txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            txtFromdate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
            txtToDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            txtToDate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
            GetEmployeeDetails();

        }

    }

    protected bool check_IsloginHR()
    {
        bool hr_code = false;
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
                hr_code = true;
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
            //  getApproverdata();  
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

            throw;
        }
    }
    private void getemployee_ReimbursmentDetails()
    {
        try
        {
            #region get employee Claim details
            DataSet dsempreimburstment = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];
            string[] strdate;
            string strfromDate = "";
            string strToDate = "";
            string strfromDate_RPt = "";
            string strToDate_RPt = "";
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
            //@toDate
            spars[1] = new SqlParameter("@to_date", SqlDbType.VarChar);
            if (Convert.ToString(txtToDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                strToDate_RPt = Convert.ToString(strdate[0]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[2]);
                spars[1].Value = strToDate;
            }
            else
            {
                strToDate = DateTime.Today.ToString("yyyy") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("dd");
                strToDate_RPt = DateTime.Today.ToString("dd") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("yyyy");
                spars[1].Value = strToDate;
            }

            spars[2] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[2].Value = "OnBoardingSeperationReportNew";

            dsempreimburstment = spm.getDatasetList(spars, "rpt_MISReport");

            #endregion

            ReportViewer2.LocalReport.Refresh();
            ReportViewer2.LocalReport.DataSources.Clear();
            ReportParameter[] param = new ReportParameter[2];
            param[0] = new ReportParameter("FromDate", Convert.ToString(strfromDate_RPt));
            param[1] = new ReportParameter("ToDate", Convert.ToString(strToDate_RPt));

            if (dsempreimburstment.Tables[0].Rows.Count > 0)
            {
                ReportDataSource rdsFuel = new ReportDataSource("MIS_OnboardingSummaryNew", dsempreimburstment.Tables[0]);
                ReportDataSource rdsMIS_OpeningEmployeelist = new ReportDataSource("MIS_OnboardingOpeningEmployeelist", dsempreimburstment.Tables[1]);
                ReportDataSource MIS_ClosingEmployeelist = new ReportDataSource("MIS_OnboardingClosingEmployeelist", dsempreimburstment.Tables[2]);
                ReportDataSource rdsMIS_SeparatedEmployeelist = new ReportDataSource("MIS_OnboardingSeparatedEmployeelist", dsempreimburstment.Tables[3]);
                ReportDataSource MIS_OnboardedEmployeelist = new ReportDataSource("MIS_OnboardingOnboardedEmployeelist", dsempreimburstment.Tables[4]);
                ReportDataSource MIS_ResignedEmployeelist = new ReportDataSource("MIS_OnboardingResignedEmployeelist", dsempreimburstment.Tables[5]);
                ReportDataSource MIS_ALWDPassedEmployeelist1 = new ReportDataSource("MIS_OnboardingALWDPassedEmployeelist1", dsempreimburstment.Tables[6]);

                ReportViewer2.ProcessingMode = ProcessingMode.Local;
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/MIS_OnboardingSeparationNew.rdlc");
                ReportViewer2.LocalReport.DataSources.Add(rdsFuel);
                ReportViewer2.LocalReport.DataSources.Add(rdsMIS_OpeningEmployeelist);
                ReportViewer2.LocalReport.DataSources.Add(MIS_ClosingEmployeelist);
                ReportViewer2.LocalReport.DataSources.Add(rdsMIS_SeparatedEmployeelist);
                ReportViewer2.LocalReport.DataSources.Add(MIS_OnboardedEmployeelist);
                ReportViewer2.LocalReport.DataSources.Add(MIS_ResignedEmployeelist);
                ReportViewer2.LocalReport.DataSources.Add(MIS_ALWDPassedEmployeelist1);
                ReportViewer2.LocalReport.SetParameters(param);
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
            lblmessage.Text = "From date  cannot be blank";
            return;
        }


        getemployee_ReimbursmentDetails();

    }



}