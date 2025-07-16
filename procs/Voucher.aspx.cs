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

public partial class Voucher : System.Web.UI.Page
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


            hflEmpCode.Value  = Convert.ToString(Session["Empcode"]);
              lblmsg.Visible = false;
              lnk_approverPaymentInbox.Visible = false;
              lnk_PayACC.Visible = false;
              lnk_PayCFO.Visible = false;
              lnk_Approved_ACC.Visible = false;
              lnk_reimbursmentReport_3.Visible = false;
            if (hflEmpCode.Value == "99999999")
            {
                Lnk_DownLoadReport.Visible = true;
                LINK_TallyCodeDept.Visible = true;
                LINK_TallyCodeLocation.Visible = true;
            }
            else
            {
                Lnk_DownLoadReport.Visible = false;
                LINK_TallyCodeDept.Visible = false;
                LINK_TallyCodeLocation.Visible = false;
            }

            //  lblmsg.Text =Convert.ToString(Session["Empcode"]); 
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/SampleForm");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {

                    span_App_head.Visible = false;
                    span_acc_head.Visible = false;
                    #region Check Login Employee is  applicable for Reimbursment

                    
                    if (check_ISLoginEmployee_ForReimbursment() == false)
                    {
                        lblheading.Text = "Other Module (Coming Soon...)";
                        editform1.Visible = false;
                        return;
                    }
                    #endregion

                    GetEmployeeDetails();
                    Get_Recruitment_Eligibility();
                    get_emp_fule_eligibility();
                    CheckApprover();
                    check_COS_ACC("RCOS");
                    check_COS_ACC("RACC");
                    check_COS_ACC("RCFO");

                    check_COS_ACC_Fuel("RCOS");
                    check_COS_ACC_Fuel("RACC");

                    check_ISLoginEmployee_ForLeave();
                    CheckEmpExpMapping();
                   check_ISLoginEmployee_ForApprovedClaim();
                     this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    public void getMobile_Fule_Claims_PendingList_cnt_Approver()
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Mobile_claim_Reqst_Pending_cnt_Appr";
            
            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = hflEmpCode.Value;
            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            //Mobile Claim Request Count
            lnk_approverPaymentInbox.Text = "Inbox Payment Voucher :(0)";
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
            }
            if (dsTrDetails.Tables[1].Rows.Count > 0)
            {
            }
            if (dsTrDetails.Tables[2].Rows.Count > 0)
            {
                lnk_approverPaymentInbox.Visible = true;
                span_App_head.Visible = true;
                //if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]).Trim() != "0")              
                lnk_approverPaymentInbox.Text = "Inbox :(" + Convert.ToString(dsTrDetails.Tables[2].Rows[0]["trvl_reqst_pending"]).Trim() + ")";
            }
            else
            {
                lnk_approverPaymentInbox.Visible = false;
                span_App_head.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void CheckApprover()
    {
        DataTable dtApprovers = new DataTable();
        dtApprovers = spm.CheckApprovers(Convert.ToString(hflEmpCode.Value).Trim());
        if (dtApprovers.Rows.Count > 0)
        {
            getMobile_Fule_Claims_PendingList_cnt_Approver();
        }
    }

    protected void GetLeaveCount()
    {

        int LeaveCount = 0;
        LeaveCount = spm.GetLeaveInboxCount(Convert.ToString(hflEmpCode.Value).Trim());
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
                getPaymentClaims_PendingList_cnt_COSACC(strtype);

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void check_COS_ACC_Fuel(string strtype)
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
                getFuelClaims_PendingList_cnt_COSACC(strtype);

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void getFuelClaims_PendingList_cnt_COSACC(string strtype)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Fuel_claim_Reqst_Pending_cnt_COSACC";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = hflEmpCode.Value;

            spars[2] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[2].Value = strtype;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {

                if (strtype == "RCOS")
                {
                }
                if (strtype == "RACC")
                {
                }                

            }
            if (dsTrDetails.Tables[1].Rows.Count > 0)
            {
                if (strtype == "RACC")
                {
                }
            }

            if (dsTrDetails.Tables[1].Rows.Count > 0)
            {
                if (strtype == "RCOS")
                {
                }
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
                
                if (strtype == "RCOS")
                {
                }
                if (strtype == "RACC")
                {
                    lnk_reimbursmentReport_3.Visible = true;
                }
                if (strtype == "RCFO")
                {
                }
               
            }

            
            if (dsTrDetails.Tables[1].Rows.Count > 0)
            {
                if (strtype == "RACC")
                {
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void getPaymentClaims_PendingList_cnt_COSACC(string strtype)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Payment_claim_Reqst_Pending_cnt_COSACC";            

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = hflEmpCode.Value;

            spars[2] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[2].Value = strtype;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                if (strtype == "RACC")
                {
                    lnk_PayACC.Visible = true;
                    span_acc_head.Visible = true;
                    lnk_Approved_ACC.Visible = true;
                    lnk_PayACC.Text = "Inbox Account :(" + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]).Trim() + ")";
                }
                if (strtype == "RCFO")
                {
                    lnk_PayCFO.Visible = false;
                }
               
            }

            
            if (dsTrDetails.Tables[1].Rows.Count > 0)
            {
                if (strtype == "RACC")
                {
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
            Session["TripTypeId"] = null;
            Session["TravelType"] = null;
            spm.clear_Reimbursement_temp_tables(hflEmpCode.Value, "DeleteMobileTemp");
            Response.Redirect("~/procs/Mobile_Req.aspx");
        }

        else
        {
            return;
        }
    }
    protected void lnk_pvrequest_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() != "")
        {
            Session["TripTypeId"] = null;
            Session["TravelType"] = null;
            spm.clear_Reimbursement_temp_tables(hflEmpCode.Value, "DeletePaymentTemp");
            Response.Redirect("~/procs/Payment_Req.aspx");
        }

        else
        {
            return;
        }
    }
    protected void lnk_Attendancereg_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() != "")
        {
            Session["TripTypeId"] = null;
            Session["TravelType"] = null;
            Session["OutsTrvl"] = null;
            Session["sfuelBalaceQty"] = null;
            Session["sfuelEligibilty"] = null;
            Session["sclamdt"] = "";
            Session["tollchrgs"] = null;
            Session["airportparking"] = null;
            Session["parkwashclaimed"] = null;

            spm.clear_Reimbursement_temp_tables(hflEmpCode.Value, "DeleteFuelTemp");
            Response.Redirect("~/procs/Fuel_Req.aspx");
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
            //spars[1].Value ="00000001";
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

    public void GetMobileEligibility_New()
    {

        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.GetMobilelEligibilityMatrix_Emp_Grade(Convert.ToString(hflGrade.Value), Convert.ToString(hflEmpCode.Value), Convert.ToString(1));

        if (dtApproverEmailIds.Rows.Count <= 0)
        {  
        }
        if (dtApproverEmailIds.Rows.Count > 0)
        {
            if (Convert.ToString(dtApproverEmailIds.Rows[0]["Eligibility"]).Trim() != "")
            {
                if (Convert.ToDecimal(dtApproverEmailIds.Rows[0]["Eligibility"]) <= 0)
                {
                }
            }
        }
    }

    public void check_ISLoginEmployee_ForApprovedClaim()
    {

        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_getPaymnetVoucherApproved";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = hflEmpCode.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                if (hflEmpCode.Value != "99999999")
                {
                    Lnk_ApproveredPayment.Visible = true;
                }
                
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
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

    public void Get_Recruitment_Eligibility()
    {

        DataTable dtRecr_login = spm.getuserinfodetails_for_Recruitment(Convert.ToString(hflEmpCode.Value).Trim());
        if (dtRecr_login.Rows.Count > 0)
        {
            if (Convert.ToString(dtRecr_login.Rows[0]["errmsg"]).Trim() != "")
            {
            }
            else
            {
            }
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
                }

                //Payment Voucher
                if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["view_paymentVoucher"]).Trim() == "N")
                {
                    blnchk = true;
                    lnk_leaveParametersmst.Visible = false;
                    lnk_leaveParametersmst1.Visible = false;
                    lnk_approverPaymentInbox.Visible = false;
                    lnk_PayACC.Visible = false;
                    lnk_PayCFO.Visible = false;
                    lnk_Approved_ACC.Visible = false;
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

    protected void Lnk_DownLoadReport_Click(object sender, EventArgs e)
    {
        DataSet DSPaymentDetail = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetDetailsPayment";
        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = DBNull.Value;
        DSPaymentDetail = spm.getDatasetList(spars, "GET_AlldetailExpense");

        if (DSPaymentDetail.Tables[0].Rows.Count > 0)
        {
            lblmsg.Visible = false;
            for (int i = 0; i < DSPaymentDetail.Tables[1].Rows.Count; i++)
            {
                SqlParameter[] sparss = new SqlParameter[3];
                sparss[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                sparss[0].Value = "GetDetailsPaymentUpdate";
                sparss[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
                sparss[1].Value = DBNull.Value;
                sparss[2] = new SqlParameter("@Id", SqlDbType.Int);
                sparss[2].Value = DSPaymentDetail.Tables[1].Rows[i]["Rem_id"].ToString();
                var ss = spm.getDatasetList(sparss, "GET_AlldetailExpense");
            }
            foreach (DataRow item in DSPaymentDetail.Tables[0].Rows)
            {
                item["Cr Ledger"] = Convert.ToString(item["Cr Ledger"]);
            }



            //Build the CSV file data as a Comma separated string.
            string csv = string.Empty;
            foreach (DataColumn column in DSPaymentDetail.Tables[0].Columns)
            {
                //Add the Header row for CSV file.
                csv += column.ColumnName + ',';
            }

            //Add new line.
            csv += "\r\n";
            foreach (DataRow row in DSPaymentDetail.Tables[0].Rows)
            {
                foreach (DataColumn column in DSPaymentDetail.Tables[0].Columns)
                {
                    //Add the Data rows.
                    csv += row[column.ColumnName].ToString().Replace(", ", ";") + ',';
                }

                //Add new line.
                csv += "\r\n";
            }

            //Download the CSV file.
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=paymentVoucher.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();
        



        //using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        wb.Worksheets.Add(DSPaymentDetail.Tables[0], "paymentVoucher");
        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.Charset = "";
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.AddHeader("content-disposition", "attachment;filename=paymentVoucher.xlsx");
        //        using (MemoryStream MyMemoryStream = new MemoryStream())
        //        {
        //            wb.SaveAs(MyMemoryStream);
        //            MyMemoryStream.WriteTo(Response.OutputStream);
        //            Response.Flush();
        //            Response.End();
        //        }
        //    }
        }
        else
        {
            lblmsg.Text = "No Records Found for Downloading...";
            lblmsg.Visible = true;
            Get_Recruitment_Eligibility();
            get_emp_fule_eligibility();
            CheckApprover();
            check_COS_ACC("RCOS");
            check_COS_ACC("RACC");
            check_COS_ACC("RCFO");
            check_COS_ACC_Fuel("RCOS");
            check_COS_ACC_Fuel("RACC");
            check_ISLoginEmployee_ForLeave();
        }
    }
    public void CheckEmpExpMapping()
    {
        try
        {
            var ds = spm.getEmployeeExpComp("CheckIsPRM", Convert.ToString(Session["Empcode"]).Trim());
            if(ds!=null)
            {
                if(ds.Rows.Count>0)
                {
                    var getMessage = Convert.ToString(ds.Rows[0]["MESSAGE"]);
                    if(getMessage=="Yes")
                    {
                       // span_App_head.Visible = true;
                        lnk_ExpencessMapping.Visible = true;
                    }
                    else
                    {
                       // span_App_head.Visible = false;
                        lnk_ExpencessMapping.Visible = false;
                    }
                }
            }
        }
        catch (Exception)
        {

        }
        
    }
}
