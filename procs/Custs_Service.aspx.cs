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



public partial class Custs_Service : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc="", dept="", subdept="", desg = "";
    public int did = 0;
    public bool isCSHEad = false;
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


              hflEmpCode.Value  = Convert.ToString(Session["Empcode"]);
              lblmsg.Visible = false;
              lnk_leaveinbox.Visible = false;
              lnk_MobACC.Visible = false;
            
              span_App_head.Visible = false;
              span_CustomerServiceHead.Visible = false;
            //lnk_MobPastApproved_ACC.Visible = false;
            lnk_reimbursmentReport_1.Visible = false;
            lnk_summary_report.Visible = false;
            lnk_CustomerSericePendingInbox_CS.Visible = false;
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Service");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                 
                    GetEmployeeDetails();
                    //get_emp_fule_eligibility();
                    //  CheckApprover();
                    //  CheckIsSPOC();
                    //   CheckEscalatedService();
                    // check_COS_ACC("RCOS");
                    // check_COS_ACC("RACC");
                    // check_COS_ACC("RCFO");

                    // check_ISLoginEmployee_ForLeave();
                    lnk_CustomerSericePendingInbox_CS.Visible = false;
                    getCustomerService_HODISShow();
                    getCustomerService_PendingList_cnt_Approver();
                    if (isCSHEad == true)
                        getCustomerService_PendingList_CS_Approver();


                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }


    private void getCustomerRequestPendingPendingCount()
    {
        DataTable dtresult = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetRequestCustPendingCount";
            dtresult = spm.getMobileRemDataList(spars, "SP_INSERTSERVICE_REQUEST");
            if (dtresult.Rows.Count > 0)
            {
                dgDelayed.DataSource = dtresult;
                dgDelayed.DataBind();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void getCustomerRequestresolvedCount()
    {
        DataTable dtresult = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetRequestCustReSolveCount";
            dtresult = spm.getMobileRemDataList(spars, "SP_INSERTSERVICE_REQUEST");
            if (dtresult.Rows.Count > 0)
            {
                DGRsolvedRequest.DataSource = dtresult;
                DGRsolvedRequest.DataBind();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void dgDelayed_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");
            e.Row.Cells[1].Attributes.Add("onmouseover", "this.style.color='Blue'");
            e.Row.Cells[1].Attributes.Add("onmouseout", "this.style.color='#F28820'");
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.dgDelayed, "Select$" + e.Row.RowIndex);
        }
    }

    protected void dgDelayed_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            InboxTravelReqstList();

        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void DGRsolvedRequest_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            InboxTravelReqstListResolve();

        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void InboxTravelReqstList()
    {
        try
        {
            DataTable dsTrDetails = new DataTable();
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CustomerService_PendingServiceRequestDisplay";
            dsTrDetails = spm.getMobileRemDataList(spars, "SP_INSERTSERVICE_REQUEST");
            if (dsTrDetails.Rows.Count > 0)
            {
                gvMngTravelRqstList.DataSource = dsTrDetails;
                gvMngTravelRqstList.DataBind();
                DivPendingRequest.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void InboxTravelReqstListResolve()
    {
        try
        {
            DataTable dsTrDetails = new DataTable();
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CustomerService_ResolveServiceRequestDisplay";
            dsTrDetails = spm.getMobileRemDataList(spars, "SP_INSERTSERVICE_REQUEST");
            if (dsTrDetails.Rows.Count > 0)
            {
                gvMngTravelRqstListResolve.DataSource = dsTrDetails;
                gvMngTravelRqstListResolve.DataBind();
                DivResolveRequest.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMngTravelRqstList.PageIndex = e.NewPageIndex;
        this.InboxTravelReqstList();

    }

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnRemid.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
        Response.Redirect("~/procs/CustsService_Req_View.aspx?id=" + hdnRemid.Value + "&type=app");
    }

    protected void gvMngTravelRqstListResolve_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMngTravelRqstListResolve.PageIndex = e.NewPageIndex;
        this.InboxTravelReqstListResolve();

    }

    

    protected void lnkEditResolve_Click1(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnRemid.Value = Convert.ToString(gvMngTravelRqstListResolve.DataKeys[row.RowIndex].Values[0]).Trim();
        Response.Redirect("~/procs/CustsService_Req_View.aspx?id=" + hdnRemid.Value + "&type=app");
    }
    protected void DGRsolvedRequest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");
            e.Row.Cells[1].Attributes.Add("onmouseover", "this.style.color='Blue'");
            e.Row.Cells[1].Attributes.Add("onmouseout", "this.style.color='#F28820'");
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.DGRsolvedRequest, "Select$" + e.Row.RowIndex);
        }
    }

    public void getMobile_Fule_Claims_PendingList_cnt_Approver()
    {
        try
        {
            DataTable dtleaveInbox = new DataTable();
            dtleaveInbox = spm.GetSerivesRequestSPOCInbox(hflEmpCode.Value);
            lnk_leaveinbox.Text = "Inbox :(0)";
            if (dtleaveInbox.Rows.Count > 0)
            {
                span_App_head.Visible = true;
                lnk_leaveinbox.Visible = true;
                lnk_leaveinbox.Text = "Inbox :(" + Convert.ToString(dtleaveInbox.Rows.Count).Trim() + ")";
            }

            DataTable dtleaveInbox1 = new DataTable();
            dtleaveInbox1 = spm.GetInboxServiceRequestRCH(hflEmpCode.Value,null,null);
           // lnk_leaveinbox.Text = "Inbox :(0)";
            if (dtleaveInbox1.Rows.Count > 0)
            {
                span_App_head.Visible = true;
                showPSR.Visible = true;
                lnk_MobACC.Visible = true;
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
            dtleaveInbox2 = spm.GetCEOEmpCode();
            if(dtleaveInbox2.Rows.Count>0)
            {
                var loginCode = Convert.ToString(hflEmpCode.Value);
                var CeoEmpCode = Convert.ToString(dtleaveInbox2.Rows[0]["Emp_Code"]);
                if(loginCode== CeoEmpCode)
                {
                    lnk_reimbursmentReport_1.Visible = true;
                    lnk_summary_report.Visible = true;
                }
                    
            }
            //linkReport

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
            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_INSERTSERVICE_REQUEST");
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

    protected void CheckApprover()
    {
        //DataTable dtApprovers = new DataTable();
        //dtApprovers = spm.CheckApprovers(Convert.ToString(hflEmpCode.Value).Trim());
        //if (dtApprovers.Rows.Count > 0)
        //{
            getMobile_Fule_Claims_PendingList_cnt_Approver();
       // }
    }

    protected void GetLeaveCount()
    {

        int LeaveCount = 0;
        LeaveCount = spm.GetLeaveInboxCount(Convert.ToString(hflEmpCode.Value).Trim());
        lnk_leaveinbox.Text = "Inbox :(" + LeaveCount.ToString() + ")";
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
                if (strtype == "RACC")
                {
                
                    lnk_MobACC.Visible = true;
                    lnk_reimbursmentReport_1.Visible = true;
                   
                    lnk_MobACC.Text = "Account-Inbox :(" + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]).Trim() + ")";
                    lnk_reimbursmentReport_1.Text = "Report - (ACC)";
                }
                if (strtype == "RCFO")
                {
                  
                   
					lnk_reimbursmentReport_1.Visible = true;
                    lnk_reimbursmentReport_1.Text = "Report - (COS)";
                }
               
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
            Response.Redirect("~/procs/Service_Req.aspx");
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
            var getSPOCStatus = spm.GETSPOCEXISTS(empCode);
            if (getSPOCStatus.Rows.Count > 0)
            {
                var Message = Convert.ToString(getSPOCStatus.Rows[0]["Message"]);
                if (Message == "YES")
                {
                    showPSR.Visible = true;
                    lnk_MobACC.Visible = true;
                    span_App_head.InnerText = "SPOC";
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
                   
                    span_App_head.Visible = false;
                    //lnk_MobPastApproved_ACC.Visible = false;
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

    public void CheckEscalatedService()
    {
        DataTable dt = new DataTable();
        dt = spm.GetEscalatedSerivesRequest(Convert.ToString(hflEmpCode.Value),0);
        if (dt.Rows.Count>0)
        {
            lnkbtnEscalated.Visible = true;
        }
        else
        {
            lnkbtnEscalated.Visible = false;
        }
    }

    public void getCustomerService_PendingList_cnt_Approver()
    {


        try
        {

            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GET_CustomerSERVICELIST_forAction";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = hflEmpCode.Value;

            DataSet dslist = spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");
            lnk_CustomerSericeInbox.Text = "CUSTOMERFIRST Service Inbox :(0)";
            if (isCSHEad==true)
                lnk_CustomerSericeInbox.Text = "CUSTOMERFIRST Service Approval Inbox :(0)";

            if (dslist != null)
            {
                if (dslist.Tables[0].Rows.Count > 0)
                {
                    span_CustomerServiceHead.Visible = true;
                    lnk_CustomerSericeInbox.Visible = true;
                    if (isCSHEad == true)
                        lnk_CustomerSericeInbox.Text = "CUSTOMERFIRST Service Approval Inbox :(" + Convert.ToString(dslist.Tables[0].Rows.Count).Trim() + ")";
                    else
                        lnk_CustomerSericeInbox.Text = "CUSTOMERFIRST Service  Inbox :(" + Convert.ToString(dslist.Tables[0].Rows.Count).Trim() + ")";
                }

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }


    }

    public void getCustomerService_PendingList_CS_Approver()
    {


        try
        {

            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GET_CustomerSERVICELIST_forAction_CS";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = hflEmpCode.Value;

            DataSet dslist = spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");

            lnk_CustomerSericePendingInbox_CS.Text = "CUSTOMERFIRST Service Inbox Pending :(0)";
            if (dslist != null)
            {
                if (dslist.Tables[0].Rows.Count > 0)
                {
                    span_CustomerServiceHead.Visible = true;
                    lnk_CustomerSericeInbox.Visible = true;
                    lnk_CustomerSericePendingInbox_CS.Text = "CUSTOMERFIRST Service Inbox Pending:(" + Convert.ToString(dslist.Tables[0].Rows.Count).Trim() + ")";
                }

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }


    }

    public void getCustomerService_HODISShow()
    {
        try
        {

            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CustomerService_HODButton";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = hflEmpCode.Value;

            DataSet dslist = spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");
            if (dslist != null)
            {
                if (dslist.Tables[0].Rows.Count > 0)
                {
                    getCustomerRequestPendingPendingCount();
                    getCustomerRequestresolvedCount();
                    isCSHEad = true;
                    lnk_CustomerSericeInbox_Processed.Visible = false;
                    lnk_CustomerSericePendingInbox_CS.Visible = true;
                }

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }


    }

}
