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



public partial class CustEscalation: System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc="", dept="", subdept="", desg = "";
    public int did = 0;
    LeaveBalance bl = new LeaveBalance();
    SP_Methods spm = new SP_Methods();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();

     

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url;}

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
			if (Convert.ToString(Session["Empcode"]) == "R-001")
			{
				Session["Empcode"] = "00631013";
			}


			hflEmpCode.Value  = Convert.ToString(Session["Empcode"]);
              lblmsg.Visible = false;
              lnk_leaveinbox.Visible = false;
              lnk_MobACC.Visible = false;
              
              span_App_head.Visible = false;
              //lnk_MobPastApproved_ACC.Visible = false;
              lnk_reimbursmentReport_1.Visible = false;
            lnk_summary_report.Visible = false;
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/SampleForm");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    

                    #region Check Login Employee is  applicable for Reimbursment

                    
                    if (check_ISLoginEmployee_ForReimbursment() == false)
                    {
                        lblheading.Text = "Reimbursement Module (Coming Soon...)";
                        editform1.Visible = false;
                        return;
                    }
                    #endregion

                    GetEmployeeDetails();
                    //get_emp_fule_eligibility();
                    CheckApprover();
                    CheckIsSPOC();
					GetCustomerFirstApproval_Count();
					CheckIsCustomerReportShow();
					CheckIsCustomerViewShow();
					// check_ISLoginEmployee_ForLeave();
					this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                    lnk_summary_report.Visible = false;
                    if (Convert.ToString(hflEmpCode.Value) == "S-005" || Convert.ToString(hflEmpCode.Value)== "00631019")
                    {
                       // lnk_summary_report.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

	public void CheckIsCustomerReportShow()
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
			spars[2].Value = "CustFReport";



			getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
			if (getdtDetails.Rows.Count > 0)
			{
				var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
				if (getStatus == "SHOW")
				{
					SPReport.Visible = true;
					lnk_CustomerFirstReport.Visible = true;
					Report.Visible = true;

				}
				else
				{
					SPReport.Visible = false;
					lnk_CustomerFirstReport.Visible = false;
					Report.Visible = false;

				}
			}
			
		}
		catch (Exception)
		{
			
		}
	}

	public void CheckIsCustomerViewShow()
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
			spars[2].Value = "CustFView";
			getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
			if (getdtDetails.Rows.Count > 0)
			{
				var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
				if (getStatus == "SHOW")
				{
					SPReport.Visible = true;			
					lnk_CustomerFirstView.Visible = true;
				}
				else
				{
					SPReport.Visible = false;				
					lnk_CustomerFirstView.Visible = false;

				}
			}

		}
		catch (Exception)
		{

		}
	}

	public void getMobile_Fule_Claims_PendingList_cnt_Approver()
    {
        try
        {
            DataTable dtleaveInbox = new DataTable();
            dtleaveInbox = spm.GetCustEscalationSPOCInbox(hflEmpCode.Value);
            lnk_leaveinbox.Text = "Inbox :(0)";
            if (dtleaveInbox.Rows.Count > 0)
            {
                span_App_head.Visible = true;
                lnk_leaveinbox.Visible = true;
                lnk_leaveinbox.Text = "Inbox :(" + Convert.ToString(dtleaveInbox.Rows.Count).Trim() + ")";
            }

            DataTable dtleaveInbox1 = new DataTable();
            dtleaveInbox1 = spm.GetInboxCustEscalationRCH(hflEmpCode.Value,null,null);
           // lnk_leaveinbox.Text = "Inbox :(0)";
            if (dtleaveInbox1.Rows.Count > 0)
            {
                //span_App_head.Visible = true;
                //showPSR.Visible = true;
                //lnk_MobACC.Visible = true;
               // lnk_MobACC.Visible = true;
                //lnk_leaveinbox.Text = "Inbox :(" + Convert.ToString(dtleaveInbox.Rows.Count).Trim() + ")";
            }

             //var getLoginEmpDeg = hflEmpDesignation.Value;
            //if(getLoginEmpDeg.Contains("Head"))
            //{
            //    lnk_reimbursmentReport_1.Visible = true;
            //}
            //\
            IsShowHODReport();

            DataTable dtleaveInbox2 = new DataTable();
            dtleaveInbox2 = spm.GetCustEscalationCEOEmpCode();
            if(dtleaveInbox2.Rows.Count>0)
            {
                var loginCode = Convert.ToString(hflEmpCode.Value);
                var CeoEmpCode = Convert.ToString(dtleaveInbox2.Rows[0]["Emp_Code"]);
                if(loginCode== CeoEmpCode)
                {
                    //lnk_reimbursmentReport_1.Visible = true;
                    //lnk_summary_report.Visible = true;
                }
                    
            }
            //linkReport

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void CheckApprover()
    {
        
            getMobile_Fule_Claims_PendingList_cnt_Approver();
       
    }

    protected void GetLeaveCount()
    {

        int LeaveCount = 0;
        LeaveCount = spm.GetLeaveInboxCount(Convert.ToString(hflEmpCode.Value).Trim());
        lnk_leaveinbox.Text = "Inbox :(" + LeaveCount.ToString() + ")";
    }
	protected void GetCustomerFirstApproval_Count() 
	{
		DataTable dtleaveInbox = new DataTable();
		int PendingCount = 0;
		dtleaveInbox = spm.GetCustEscalation_Approval("EscalatedApproval", hflEmpCode.Value);
		if (dtleaveInbox.Rows.Count > 0)
		{
			
			foreach (DataRow row in dtleaveInbox.Rows)
			{
				int AUTO_ESCALATIONDay = 8;
				var diffDay = 0.00;
				var Id = Convert.ToInt32(row["Id"].ToString());				
				var ServicesRequestID = Convert.ToString(row["ServicesRequestID"].ToString());
				var ServiceDepartment = Convert.ToString(row["ServiceDepartment"].ToString());							
				var AssignedDate = Convert.ToDateTime(row["AssignedDates"].ToString()).Date;
				var todayDate = DateTime.Now.Date;
				string officeLocation = "HO-NaviMum";				
				var getDayesdt = spm.GetCustEscalationServiceCount(officeLocation, AssignedDate, todayDate);
				if (getDayesdt.Rows.Count > 0)
				{
					diffDay = Convert.ToInt32(getDayesdt.Rows[0]["WORKINGDAY"]);
				}
				else
				{
					diffDay = (todayDate - AssignedDate).TotalDays;
				}
				DataTable dtSPOC = spm.GetCustEscalationSPOCData(ServiceDepartment);
				if (dtSPOC.Rows.Count > 0)
				{
					AUTO_ESCALATIONDay = Convert.ToInt32(dtSPOC.Rows[0]["USER_ESCALATION"]);
				}

				if (diffDay <= AUTO_ESCALATIONDay)
				{
					PendingCount = PendingCount + 1;					
				}			
			}
			if (PendingCount > 0)
			{
				lnk_reimbursmentReport.Visible = true;
				lnk_reimbursmentReport.Text = "Closure Confirmation Inbox :(" + PendingCount + ")";
			}



		}

	}

	protected void check_COS_ACC(string strtype)
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_Check_COS_ACC_apprver_code_byType";

            spars[1] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[1].Value = strtype;			
			spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = hflEmpCode.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");
            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {   
                getMobileClaims_PendingList_cnt_COSACC(strtype);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void getMobileClaims_PendingList_cnt_COSACC(string strtype)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Mobile_claim_Reqst_Pending_cnt_COSACC";            

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = hflEmpCode.Value;

            spars[2] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[2].Value = strtype;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
               
               
            }


            if (dsTrDetails.Tables[1].Rows.Count > 0)
            {
                if (strtype == "RACC")
                {
                    //lnk_MobPastApproved_ACC.Visible = true;
                    //lnk_MobPastApproved_ACC.Text = "Account Mobile Inbox";
                    lnk_reimbursmentReport_1.Text = "Audit Report - (ACC)";
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void lnk_leaverequest_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() != "")
        {
            //Session["TripTypeId"] = null;
           // Session["TravelType"] = null;
           // spm.clear_Reimbursement_temp_tables(hflEmpCode.Value, "DeleteMobileTemp");
            Response.Redirect("~/procs/CustEscalation_Req.aspx");
        }

        else
        {
            return;
        }
    }
    public Boolean check_ISLoginEmployee_ForReimbursment()
    {
        Boolean bchkEMP = false;
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Check_ISapplicable_Reimbursment";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
			spars[1].Value =hflEmpCode.Value; 
            dsTrDetails = spm.getDatasetList(spars, "Usp_getEmployee_Details_All");

            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                bchkEMP = true;
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
        return bchkEMP;

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
    public void CheckIsSPOC()
    {
        try
        {
            var empCode = Convert.ToString(Session["Empcode"]).Trim();
            var getSPOCStatus = spm.GETCustEscalationSPOCEXISTS(empCode);
            if (getSPOCStatus.Rows.Count > 0)
            {
                var Message = Convert.ToString(getSPOCStatus.Rows[0]["Message"]);
                if (Message == "YES")
                {
                    //showPSR.Visible = true;
                    //lnk_MobACC.Visible = true;
                    span_App_head.InnerText = "Coordinator";
                }
                else
                {

                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

            throw;
        }
    }
    public void GetMobileEligibility_New()
    {

        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.GetMobilelEligibilityMatrix_Emp_Grade(Convert.ToString(hflGrade.Value), Convert.ToString(hflEmpCode.Value), Convert.ToString(1));

        if (dtApproverEmailIds.Rows.Count <= 0)
        {  
            lnk_leaverequest.Visible = false;
            lnk_mng_leaverequest.Visible = false;
        }
        if (dtApproverEmailIds.Rows.Count > 0)
        {
            if (Convert.ToString(dtApproverEmailIds.Rows[0]["Eligibility"]).Trim() != "")
            {
                if (Convert.ToDecimal(dtApproverEmailIds.Rows[0]["Eligibility"]) <= 0)
                {
                    lnk_leaverequest.Visible = false;
                    lnk_mng_leaverequest.Visible = false;
                }
            }
        }

    }


    public void get_emp_fule_eligibility()
    {
        try
        {

            #region date formatting

            string[] strdate;
            string strFromDate = "";
            if (Convert.ToString(hdnClaimDate.Value).Trim() != "")
            {
                strdate = Convert.ToString(hdnClaimDate.Value).Trim().Split('/');
                strFromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            #endregion


            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "getemp_fule_eligibility";

            spars[1] = new SqlParameter("@Empcode", SqlDbType.VarChar);
            spars[1].Value = hflEmpCode.Value;

            spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[2].Value = 0;

            spars[3] = new SqlParameter("@ClaimDate", SqlDbType.VarChar);
            if (Convert.ToString(hdnClaimDate.Value).Trim() != "")
                spars[3].Value = strFromDate;
            else
                spars[3].Value = DBNull.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            if (dsTrDetails.Tables[0].Rows.Count == 0)
            {
                #region Set Enable False if not application for Fuel Claim

                #endregion

            }



        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }

    }

    public void check_ISLoginEmployee_ForLeave()
    {

        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Check_ISapplicable_Fuel_mobile_PV";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = hflEmpCode.Value;


            dsTrDetails = spm.getDatasetList(spars, "Usp_getEmployee_Details_All");

            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                bool blnchk = false;
                //Fuel
                if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["view_fuel"]).Trim() == "N")
                {
                    blnchk = true;
                }

                //Mobile
                if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["view_mobile"]).Trim() == "N")
                {
                    blnchk = true;
                    lnk_leaveinbox.Visible = false;
                    lnk_MobACC.Visible = false;
                   
                }


                //Payment Voucher
                if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["view_paymentVoucher"]).Trim() == "N")
                {
                    blnchk = true;
                }
                if (blnchk == true)
                {
                    lnk_reimbursmentReport.Visible = true;
                    
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
       

    }

     public void IsShowHODReport()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "IsDepartmentHOD";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);
            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_INSERTCUST_ESCALATION");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    lnk_reimbursmentReport_1.Visible = true;
                    lnk_summary_report.Visible = true;
                }
                else
                {
                    lnk_reimbursmentReport_1.Visible = false;
                    lnk_summary_report.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }    

}
