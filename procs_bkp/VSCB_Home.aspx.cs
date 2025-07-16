using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class VSCB_Home: System.Web.UI.Page
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

    protected void lnk_trvlAccInbox_Click(object sender, EventArgs e)
    {
        DataSet dtKRA = new DataSet();

        #region get KRA


        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
        spars[0].Value = "getOfferLetter";


        dtKRA = spm.getDatasetList(spars, "SP_VSCB_Upload_CutoffData");

        #endregion

        try
        {
            string s = Convert.ToString(dtKRA.Tables[0].Rows[0]["offer_text"]);
            string sOfferLetterNo = "HRD/HBT/OL1248";
            string sOfferDate = "30th August, 2022";
            string sOfferband = "Band IV";
            string sOfferPossiotion = "Lead - Technical";
            if (s.Contains("@offerletterno"))
                s = s.Replace("@offerletterno", sOfferLetterNo);

            if (s.Contains("@offerdate"))
                s = s.Replace("@offerdate", sOfferDate);

            if (s.Contains("@offerBand"))
                s = s.Replace("@offerBand", sOfferband);

            if (s.Contains("@offerPossition"))
                s = s.Replace("@offerPossition", sOfferPossiotion);

            lblid.Text= Convert.ToString(s).Trim();
            dtKRA.Tables[0].Rows[0]["offer_text"] = Convert.ToString(s).Trim(); 

            string strpath = Server.MapPath("~/procs/offerletter.rdlc"); 

            LocalReport ReportViewer2 = new LocalReport();
            ReportViewer2.ReportPath = strpath;
            ReportDataSource rds = new ReportDataSource("dsMyoffer", dtKRA.Tables[0]);
        


            ReportViewer2.DataSources.Clear();
            ReportViewer2.DataSources.Add(rds);         
            ReportViewer2.Refresh();

            Warning[] warnings;
            string[] streamIds;
            string contentType;
            string encoding;
            string extension;

            //Export the RDLC Report to Byte Array.
            byte[] bytes = ReportViewer2.Render("pdf", null, out contentType, out encoding, out extension, out streamIds, out warnings);

            //Download the RDLC Report in Word, Excel, PDF and Image formats.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=myOffer" + "." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();


        }
        catch (Exception ex)
        {

        }
    }
}
