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

public partial class travelindex : System.Web.UI.Page
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
    public DataTable dtEmp, dtleavebal, dtApprover, dtLeaveTypes, dtApproverEmailIds, dtMaxRequestId, dtIntermediate, dtLeaveCalculation, dtHolidays;
    public int Leaveid;
    public int leavetype, openbal, avail, rembal, leaveconditionid;
    public string filename = "", approveremailaddress;
    DateTime holidaydate = new DateTime();

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url;}
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            lnk_trvlrequest.Visible = false;
            lnk_mng_trvlrequest.Visible = false;
            lnk_mng_trvlexpenseswpay.Visible = false;
            lnk_mng_trvlexpenses.Visible = false;
            lnk_trvlParametersmst.Visible = false;
            //lnk_trvlreport.Visible = false;
            //lnk_trvlattendanceinbox.Visible = false;
            lnk_trvePara.Visible = false;
            lnk_expPara.Visible = false;
            lnk_trvlinbox.Visible = false;
            lnk_TeamCalendar.Visible = false;

            lnk_trvl_TDInbox.Visible = false;
            lnk_trvl_COSInbox.Visible = false;
            lnk_trvl_AccInbox.Visible = false;
            lnk_expens_AccInbox.Visible = false;
            lnk_reimbursmentReport_3.Visible = false;
            lnk_trvlAccInbox.Visible = false;
            span_App_head.Visible = false;
            span_TD_head.Visible = false;
            span_cos_head.Visible = false;
            span_acc_head.Visible = false;

            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            Session["chkbtnStatus"] = "";

            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

            txtEmpCode.Text = Convert.ToString(Session["Empcode"]).Trim();
            lblmsg.Visible = false; ;
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/travelindex");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {

                    //txtReason.Text = "This is after function";
                    hdnempcode.Value = Convert.ToString(Session["Empcode"]);
                    if (hdnempcode.Value == "99999999")
                    {
                        lnk_TravelVoucher.Visible = true;
                    }
                    else
                    {
                        lnk_TravelVoucher.Visible = false;
                    }


                    #region Check Login Employee is  applicable for Reimbursment


                    if (check_ISLoginEmployee_ForReimbursment() == false)
                    {
                        lblheading.Text = "Travel Module (Coming Soon...)";
                        editform1.Visible = false;
                        return;
                    }
                    #endregion
                    //hdnempcode.Value = "11111";
                    //Session["Empcode"] = "11111";

                    //hdnempcode.Value = "2222";
                    //Session["empcode"] = "2222";

                    //hdnempcode.Value = "33333";
                    //Session["empcode"] = "33333";

                    CheckApprover();
                    //Check if TD / COS or ACC
                    checkTD_COS_ACC("TD");
                    checkTD_COS_ACC("COS");
                    checkTD_COS_ACC("ACC");

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
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
            spars[1].Value = hdnempcode.Value;
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

    protected void lnk_trvlrequest_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() != "")
        {
            Session["TripTypeId"] = null;
            Session["TravelType"] = null;

            Session["Fromdate"] = null;
            Session["Todate"] = null;

            spm.clear_temp_travel_tables(hdnempcode.Value);
            Response.Redirect("~/procs/TravelRequest.aspx");
        }

        else
        {
            return;
        }
     
    }

    public void deleteTempExpeseData()
    {

        DataTable dtTrDetails = new DataTable();        
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_Exp_delete_toTempTabls_trvl";
         
        spars[1] = new SqlParameter("@exp_sr_no", SqlDbType.Int);
        spars[1].Value = DBNull.Value;

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = txtEmpCode.Text;
        dtTrDetails = spm.getDataList(spars, "SP_GETALL_Expense_DETAILS");

        if (dtTrDetails.Rows.Count > 0)
        {   

        }
    }
    protected void lnk_mng_trvlexpenseswpay_Click(object sender, EventArgs e)
    {
        deleteTempExpeseData();
        Session["TripTypeId"] = null;
        Session["TravelType"] = null;

        Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=0&expid=0");        
    }

    protected void CheckApprover()
    {
        lnk_trvlrequest.Visible = true;
        lnk_mng_trvlrequest.Visible = true;
        lnk_mng_trvlexpenseswpay.Visible = true;
        lnk_mng_trvlexpenses.Visible = true;
        

        DataTable dtApprovers = new DataTable();
        dtApprovers = spm.CheckApprovers(Convert.ToString(hdnempcode.Value).Trim());
        if (dtApprovers.Rows.Count > 0)
        {
            getTravel_Expenses_PendingList_cnt_Approver();
            span_App_head.Visible = true;
            lnk_trvlinbox.Visible = true;
            lnk_trvlParametersmst.Visible = true;
            lnk_TeamCalendar.Visible = true;
        }
         

    }

    public void getTravel_Expenses_PendingList_cnt_TDCOSACC(string strtype)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_get_Expenses_travel_Reqst_Pending_cnt_TDCOSACC";

            spars[1] = new SqlParameter("@exp_sr_no", SqlDbType.Int);
            spars[1].Value = DBNull.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = hdnempcode.Value;

            spars[3] = new SqlParameter("@exp_trvl", SqlDbType.VarChar);
            spars[3].Value = strtype;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");

            
            //lnk_trvl_COSInbox.Visible = false;
            //lnk_trvl_AccInbox.Visible = false;

            //Travel Request Count

           
                if (dsTrDetails.Tables[0].Rows.Count > 0)
                {
                    if (strtype == "TD")
                    {
                        lnk_trvl_TDInbox.Text = "Travel Desk Inbox requests:(" + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]).Trim() + ")";
                    }
                    if (strtype == "COS")
                    {
                        lnk_trvl_COSInbox.Text = "COS Inbox Travel requests:(" + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]).Trim() + ")";
                    }
                    if (strtype == "ACC")
                    {
                        lnk_trvl_AccInbox.Text = "ACC Inbox Travel requests:(" + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]).Trim() + ")";

                        lnk_expens_AccInbox.Text = "Inbox Travel Expenses:(" + Convert.ToString(dsTrDetails.Tables[1].Rows[0]["expenses_reqst_pending"]).Trim() + ")";
                    }
                }
            
            ////Expenses Request Count
            //if (dsTrDetails.Tables[1].Rows.Count > 0)
            //{
            //    if (Convert.ToString(dsTrDetails.Tables[1].Rows[0]["expenses_reqst_pending"]).Trim()!="0")
            //    lnk_trvlParametersmst.Text = "Inbox Travel Expenses:(" + Convert.ToString(dsTrDetails.Tables[1].Rows[0]["expenses_reqst_pending"]).Trim() + ")";
            //}
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void getTravel_Expenses_PendingList_cnt_Approver()
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_get_Expenses_travel_Reqst_Pending_cnt";

            spars[1] = new SqlParameter("@exp_sr_no", SqlDbType.Int);
            spars[1].Value = DBNull.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = hdnempcode.Value;
            dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");


            //lnk_trvl_COSInbox.Visible = false;
            //lnk_trvl_AccInbox.Visible = false;

            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                lnk_trvlinbox.Text = " Inbox Travel  requests:(" + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]).Trim() + ")";
            }
            //Expenses Request Count
            if (dsTrDetails.Tables[1].Rows.Count > 0)
            {
               // lnk_trvlinbox.Text = "Inbox  Travel Expenses:(" + Convert.ToString(dsTrDetails.Tables[1].Rows[0]["expenses_reqst_pending"]).Trim() + ")";
            }

            //Expenses Request Count
            if (dsTrDetails.Tables[1].Rows.Count > 0)
            {
                //if (Convert.ToString(dsTrDetails.Tables[1].Rows[0]["expenses_reqst_pending"]).Trim() != "0")
                    lnk_trvlParametersmst.Text = "Inbox Travel Expenses:(" + Convert.ToString(dsTrDetails.Tables[1].Rows[0]["expenses_reqst_pending"]).Trim() + ")";
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void checkTD_COS_ACC(string strtype)
    {
      try
      {
          
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_Check_TD_COS_apprver_code_byType";

            spars[1] = new SqlParameter("@exp_trvl", SqlDbType.VarChar);
            spars[1].Value = strtype;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = hdnempcode.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");
            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                if (strtype == "TD")
                {
                    lnk_trvl_TDInbox.Visible = true;
                    span_TD_head.Visible = true;
                }

                if (strtype == "COS")
                {
                    lnk_trvl_COSInbox.Visible = true;
                    span_cos_head.Visible = true;
                }

                if (strtype == "ACC")
                {
                    lnk_trvl_AccInbox.Visible = true;
                    lnk_expens_AccInbox.Visible = true;
                    lnk_reimbursmentReport_3.Visible = true;
                    lnk_trvlAccInbox.Visible = true;
                    span_acc_head.Visible = true;
                }

                getTravel_Expenses_PendingList_cnt_TDCOSACC(strtype);
                
            }
      }
      catch(Exception ex)
      {
          Response.Write(ex.Message.ToString());
      }

    }


    protected void lnk_mng_trvlrequest_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() != "")
        {
            Session["TripTypeId"] = null;
            Session["TravelType"] = null;

            Session["Fromdate"] = null;
            Session["Todate"] = null;

            spm.clear_temp_travel_tables(hdnempcode.Value);
            Response.Redirect("~/procs/ManageTravelRequest.aspx");
        }

        else
        {
            return;
        }

       
    }

    protected void lnk_TravelVoucher_Click(object sender, EventArgs e)
    {
        DataSet DSFuelDetail = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetDetailsTravel";
        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = DBNull.Value;
        DSFuelDetail = spm.getDatasetList(spars, "GET_AlldetailExpense");

        if (DSFuelDetail.Tables[0].Rows.Count > 0)
        {
            lblmsg.Visible = false;
            for (int i = 0; i < DSFuelDetail.Tables[1].Rows.Count; i++)
            {
                SqlParameter[] sparss = new SqlParameter[3];
                sparss[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                sparss[0].Value = "GetDetailsTravelUpdate";
                sparss[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
                sparss[1].Value = DBNull.Value;
                sparss[2] = new SqlParameter("@Id", SqlDbType.Int);
                sparss[2].Value = DSFuelDetail.Tables[1].Rows[i]["Rem_id"].ToString();
                var ss = spm.getDatasetList(sparss, "GET_AlldetailExpense");
            }

            //Build the CSV file data as a Comma separated string.
            string csv = string.Empty;
            foreach (DataColumn column in DSFuelDetail.Tables[0].Columns)
            {
                //Add the Header row for CSV file.
                csv += column.ColumnName + ',';
            }

            //Add new line.
            csv += "\r\n";
            foreach (DataRow row in DSFuelDetail.Tables[0].Rows)
            {
                foreach (DataColumn column in DSFuelDetail.Tables[0].Columns)
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
            Response.AddHeader("content-disposition", "attachment;filename=TravelVoucher.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();

            //using (XLWorkbook wb = new XLWorkbook())
            //{
            //    wb.Worksheets.Add(DSFuelDetail.Tables[0], "TravelVoucher");
            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.Charset = "";
            //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //    Response.AddHeader("content-disposition", "attachment;filename=TravelVoucher.xlsx");
            //    using (MemoryStream MyMemoryStream = new MemoryStream())
            //    {
            //        wb.SaveAs(MyMemoryStream);
            //        MyMemoryStream.WriteTo(Response.OutputStream);
            //        Response.Flush();
            //        Response.End();
            //    }
            //}

        }
        else
        {
            lblmsg.Text = "No Records Found for Downloading...";
            lblmsg.Visible = true;
            CheckApprover();
            //Check if TD / COS or ACC
            checkTD_COS_ACC("TD");
            checkTD_COS_ACC("COS");
            checkTD_COS_ACC("ACC");
        }
    }
}
