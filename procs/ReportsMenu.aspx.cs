using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;



public partial class ReportsMenu : System.Web.UI.Page
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


            hflEmpCode.Value = Convert.ToString(Session["Empcode"]);
            lblmsg.Visible = false;
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/ReportsMenu");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {

                    trOnboardingSeparation_MRMReport.Visible = false;
                    GetEmployeeDetails();

                    DataTable dtApprovers = new DataTable();
                    dtApprovers = spm.CheckHOD(Convert.ToString(hflEmpCode.Value).Trim());
                    if (dtApprovers.Rows.Count > 0)
                    {
                        //REPO_STRU.Visible = true;
                        lnk_leaverequest.Visible = true;
                        lnk_mng_leaverequest.Visible = true;
                    }
                    else
                    {
                        lnk_leaverequest.Visible = false;
                        lnk_mng_leaverequest.Visible = false;
                    }
			
                    if ( Convert.ToString(hflEmpCode.Value) == "00631609" || Convert.ToString(hflEmpCode.Value) == "00631294" || Convert.ToString(hflEmpCode.Value) == "00630814" || Convert.ToString(hflEmpCode.Value) == "00631019" || Convert.ToString(hflEmpCode.Value) == "00630967" || Convert.ToString(hflEmpCode.Value) == "00631230" || Convert.ToString(hflEmpCode.Value) == "00631257" || Convert.ToString(hflEmpCode.Value) == "00630008" || Convert.ToString(hflEmpCode.Value) == "00631710" || Convert.ToString(hflEmpCode.Value) == "00631357" || Convert.ToString(hflEmpCode.Value) == "99999999")
                    {
                        lnk_leaverequest.Visible = true;
                        lnk_mng_leaverequest.Visible = true;
                    }
                    CheckIsBenchListReportShow();
                    CheckIsLOPReportShow();
                    CheckIsITReportShow();
                    CheckIsPerDiemReportShow();
                    CheckIsInsuranceReportShow();
                    CheckIsEmployeeReportShow();
                    ChkIsAppointmetLetterIssuedReportShow();
                    CheckIsELCReportShow();
                    CheckIsSalaryStatusUpdateReportShow();
                    //Update 10-11-2021
                    if (Convert.ToString(hflEmpCode.Value) == "00631087" || Convert.ToString(hflEmpCode.Value) == "00631257" || Convert.ToString(hflEmpCode.Value) == "00631230" || Convert.ToString(hflEmpCode.Value) == "00631300")
                    {
                        lnk_leaverequest.Visible = true;
                        lnk_mng_leaverequest.Visible = true;
                        lnk_leaveinbox.Visible = true;
                    }
                    if (Convert.ToString(hflEmpCode.Value) == "00630008" || Convert.ToString(hflEmpCode.Value) == "00630674" || Convert.ToString(hflEmpCode.Value) == "00631551")
                    {
                        lnk_leaveinbox.Visible = true;
                    }
                    //Report
                    if (CheckIsContractExpiryReportShow())
                    {
                        lnk_ContractExpiryReport.Visible = true;
                    }
                    else
                    {
                        lnk_ContractExpiryReport.Visible = false;
                    }
					CheckIsSurveyReportShow();
                    CheckIsIntrviewReportShow();
					CheckIsFuelReportShow();
                    CheckIsCTCBreackpReportShow();

                    CheckIsOnboardingSeparation_MRMReportShow();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    public void CheckIsBenchListReportShow()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowLOPReport";
            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);
            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "BenchListReport";
            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    Lnk_BenchList.Visible = true;
                }
                else
                {
                    Lnk_BenchList.Visible = false;
                }
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }
    public void CheckIsEmployeeReportShow()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsEmployeeReport";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "EmplyeeDetailsReport";

            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    EmpReport.Visible = true;
                    // return true;
                }
                else
                {
                    EmpReport.Visible = false;
                    // return false;
                }
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
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
            //  getApproverdata();  
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

            throw;
        }
    }
    public void CheckIsLOPReportShow()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowLOPReport";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "LOPReport";



            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    lnk_LOP.Visible = true;
                    // return true;
                }
                else
                {
                    lnk_LOP.Visible = false;
                    // return false;
                }
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }
    public void CheckIsITReportShow()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowLOPReport";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "ITAssetsReport";

            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    lnk_ITAssetSummaryRpt.Visible = true;
                    // return true;
                }
                else
                {
                    lnk_ITAssetSummaryRpt.Visible = false;
                    // return false;
                }
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }
    public void CheckIsPerDiemReportShow()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            //old
            //spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            //spars[0].Value = "CheckIsShowLOPReport";

            //spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            //spars[1].Value = Convert.ToString(hflEmpCode.Value);

            //spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            //spars[2].Value = "PerDiemReport";

            //New Manisha-2-12-2021

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "PerDiemReportAccess";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);


            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    lnk_Per_Diem.Visible = true;
                    // return true;
                }
                else
                {
                    lnk_Per_Diem.Visible = false;
                    // return false;
                }
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }

    public void CheckIsInsuranceReportShow()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowLOPReport";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "InsuRReport";

            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    lnk_Insurance_Report.Visible = true;
                    // return true;
                }
                else
                {
                    lnk_Insurance_Report.Visible = false;
                    // return false;
                }
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }
    public void ChkIsAppointmetLetterIssuedReportShow()

    {

        var getdtDetails = new DataTable();

        try

        {

            SqlParameter[] spars = new SqlParameter[3];



            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);

            spars[0].Value = "ChkIsAppointmetLetterIssuedReport";



            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);

            spars[1].Value = Convert.ToString(hflEmpCode.Value);



            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);

            spars[2].Value = "AppointmentLetterIssuedReport";





            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");

            if (getdtDetails.Rows.Count > 0)

            {

                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);

                if (getStatus == "SHOW")

                {

                    lnk_AppointLtrIssuedRpt.Visible = true;

                    // return true;

                }

                else

                {

                    lnk_AppointLtrIssuedRpt.Visible = false;

                    // return false;

                }

            }

            // return false;

        }

        catch (Exception)

        {

            // return false;

        }

    }
    public void CheckIsELCReportShow()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowLOPReport";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "ELCReport";



            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    lnk_ELC.Visible = true;
                    // return true;
                }
                else
                {
                    lnk_ELC.Visible = false;
                    // return false;
                }
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }
    public void CheckIsSalaryStatusUpdateReportShow()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowLOPReport";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "SALSTUPRPT";



            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    lnkSalaryUpdateReport.Visible = true;
                    // return true;
                }
                else
                {
                    lnkSalaryUpdateReport.Visible = false;
                    // return false;
                }
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }
    public bool CheckIsContractExpiryReportShow()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowLOPReport";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "ContractREPORT";
            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {                  
                     return true;
                }
                else
                {
                     return false;
                }
            }
        }
        catch (Exception ex)
        {
            
        }
        return false;
    }
	public void CheckIsSurveyReportShow()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowLOPReport";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "EXITSURVEYREPORT";



            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    lnk_ExitSurvey.Visible = true;
                    // return true;
                }
                else
                {
                    lnk_ExitSurvey.Visible = false;
                    // return false;
                }
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }
    public void CheckIsIntrviewReportShow()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowLOPReport";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "EXITINTERVIEWREPORT";



             getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    lnk_ExitIntrview.Visible = true;
                    // return true;
                }
                else
                {
                    lnk_ExitIntrview.Visible = false;
                    // return false;
                }
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }
	public void CheckIsFuelReportShow()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowLOPReport";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "FUELSUMMARYREPORT";



            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    lnk_FuelSummary.Visible = true;
                    // return true;
                }
                else
                {
                    lnk_FuelSummary.Visible = false;
                    // return false;
                }
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }

    public void CheckIsCTCBreackpReportShow()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowCTCBreackupReport";
            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);
            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "CTCBreackupReport";

            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    lnkCTCBreackupReport.Visible = true;
                }
                else
                {
                    lnkCTCBreackupReport.Visible = false;
                }
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }

    public void CheckIsOnboardingSeparation_MRMReportShow()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowOnbaordingSeparationMRMReport";
            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);
            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "OnboardingMRMReport";

            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    trOnboardingSeparation_MRMReport.Visible = true;
                }   
                 
            }
            // return false;
        }
        catch (Exception)
        {
            // return false;
        }
    }
}
