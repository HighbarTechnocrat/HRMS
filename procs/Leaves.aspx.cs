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



public partial class Leaves : System.Web.UI.Page
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

            
            Session["chkbtnStatus_Appr"] = "";
            hflEmpCode.Value  = Convert.ToString(Session["Empcode"]);

            //Session["Empcode"] = "11536";
            //Session["Empcode"] = "12443";

            //Session["Empcode"] = "11536";
            //Session["Empcode"] = "11549";

            //Session["emp_loginName"] = "Ms. User M1";
            //Response.Write(Session["Empcode"]);

            //hflEmpCode.Value = "44444";
            //Session["empcode"] = "44444";

              lblmsg.Visible = false;


               
            //  lblmsg.Text =Convert.ToString(Session["Empcode"]); 
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    span_App_head.Visible = false;
                    span_Hr_head.Visible = false;
                    hdnisViewAttendance.Value = "Y";

                    #region Check Employee Working Sechdule
                    DataSet dsempwrkschdule = spm.get_emp_workschdule(hflEmpCode.Value,"get_emp_wrkschdule");

                    if (dsempwrkschdule.Tables[0].Rows.Count > 0)
                    {   
                        hdnempwrkSchdule.Value= dsempwrkschdule.Tables[0].Rows[0]["emp_wrkschdule"].ToString();
                        hdnisViewAttendance.Value = dsempwrkschdule.Tables[0].Rows[0]["view_attendance"].ToString();
                    }


                    #endregion

                    #region Check Login Employee is applicable for Leave

                    if (check_ISLoginEmployee_ForLeave() == false)
                    {
                        lblheading.Text = "Leave Module (Coming Soon...)";
                        editform1.Visible = false;
                        return;
                    }
                    #endregion

                    //txtReason.Text = "This is after function";
                    CheckApprover();
                    checkHR_Inbox();
                    getemployeeLeaveBalance();
                    //hheadyear.InnerText = " Leave Card - " + Convert.ToString(System.DateTime.Now.Year);
                   // hheadyear.InnerText = " The below reflected leave balance is subject to changes, post closure of 2024 leave reconciliation";
		     hheadyear.InnerText = "The reflected leave balance is subject to change upon the closure of the leave reconciliation.";

                    if (Convert.ToString(hdnisViewAttendance.Value).Trim() == "N")
                    {
                        //lnk_Attendancereg.Visible = false;
                        //lnk_mng_Attendancereg.Visible = false;
                        //lnk_HRAttendanceInbox.Visible = false;
                        //lnk_attendanceinbox.Visible = false;
                    }
                    
                    //DisplayProfileProperties();

                    // loadorder();
                     this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                  //   lnk_TeamCalendar.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    private void getemployeeLeaveBalance()
    {
        DataTable dtleavebal = new DataTable();
        dtleavebal = spm.GetLeaveBalance(hflEmpCode.Value.ToString());

        dgLeaveBalance.DataSource = null;
        dgLeaveBalance.DataBind();

        if (dtleavebal.Rows.Count > 0)
        {
            dgLeaveBalance.DataSource = dtleavebal;
            dgLeaveBalance.DataBind();

            decimal dleavebal = 0;
            foreach (GridViewRow row in dgLeaveBalance.Rows)
            {
                if (Convert.ToString(dgLeaveBalance.Rows[row.RowIndex].Cells[7].Text).Trim() != "")
                {
                    dleavebal = dleavebal + Convert.ToDecimal(dgLeaveBalance.Rows[row.RowIndex].Cells[7].Text);
                }
            }
            if (dleavebal == 0)
                dgLeaveBalance.Columns[7].Visible = false;
        }
    }

    protected void CheckApprover()
    {
        DataTable dtApprovers = new DataTable();
        dtApprovers = spm.CheckApprovers(Convert.ToString(hflEmpCode.Value).Trim ());

        //lnk_HRAttendanceInbox.Visible = false;
        //lnk_TeamCalendar.Visible = false;
        if (dtApprovers.Rows.Count > 0)
        {
            GetLeaveCount();
            lnk_leaveinbox.Visible = true;
            span_App_head.Visible = true;
            //lnk_attendanceinbox.Visible = true;
            //lnk_TeamCalendar.Visible = true;
            lnk_leavereport.Visible = true;
        }
        else
        {
            lnk_leaveinbox.Visible = false;
            span_App_head.Visible = false;
            //lnk_attendanceinbox.Visible = false;
            //lnk_TeamCalendar.Visible = false;
            lnk_leavereport.Visible = false;
        }

    }
    protected void GetLeaveCount()
    {
       
        int LeaveCount = 0;
        LeaveCount = spm.GetLeaveInboxCount(Convert.ToString(hflEmpCode.Value).Trim());

        int AttLeaveCount = 0;
        AttLeaveCount = spm.getAttendance_InboxList_Count(Convert.ToString(hflEmpCode.Value).Trim());

        lnk_leaveinbox.Text = "Inbox:(" + LeaveCount.ToString() + ")";

        //lnk_attendanceinbox.Text = "Attendance regularization requests:(" + AttLeaveCount.ToString() + ")";
       
    }
    //protected void loadorder()
    //{
    //    DataTable dtp = classpkg.getcountorderdetailbyemaild(Page.User.Identity.Name.ToString().Trim());
    //    if (dtp.Rows.Count > 0)
    //    {
    //        if (Convert.ToInt32(dtp.Rows[0]["orderid"].ToString()) == 0)
    //        {
    //            lihistory.Visible = false;
    //        }
    //    }
    //    else
    //    {
    //        lihistory.Visible = true;
    //    }
    //}
    //private void DisplayProfileProperties()
    //{
    //    try
    //    {
    //        Boolean varfindcity = false;

    //        MembershipUser user = Membership.GetUser(this.Page.User.Identity.Name.ToString().Trim());
    //        DataSet ds_userdetails = classaddress.getalluserbyusername(this.Page.User.Identity.Name.ToString().Trim());
    //        if (ds_userdetails.Tables.Count > 0)
    //        {
    //            if( ds_userdetails.Tables[0].Rows.Count > 0)
    //            {
    //               // txtemail.Text = ds_userdetails.Tables[0].Rows[0]["username"].ToString();
    //               // txtemailadress.Text = ds_userdetails.Tables[0].Rows[0]["emailid"].ToString();
    //                //txtfirstname.Text = ds_userdetails.Tables[0].Rows[0]["firstname"].ToString();
    //                //txtlastname.Text = ds_userdetails.Tables[0].Rows[0]["lastname"].ToString();
    //                //txtaddress1.Text = ds_userdetails.Tables[0].Rows[0]["address"].ToString();
    //               // txtpincode.Text = ds_userdetails.Tables[0].Rows[0]["pincode"].ToString();
    //                //txtphone.Text = ds_userdetails.Tables[0].Rows[0]["telno"].ToString();
    //                //txtmobile.Text = ds_userdetails.Tables[0].Rows[0]["mobileno"].ToString();
    //                //txttempaddress.Text = ds_userdetails.Tables[0].Rows[0]["tempaddress"].ToString();
    //                //txtaltemail.Text = ds_userdetails.Tables[0].Rows[0]["alternateemail"].ToString();
    //                //txtextension.Text = ds_userdetails.Tables[0].Rows[0]["extentionno"].ToString();
    //                //txtoffno.Text = ds_userdetails.Tables[0].Rows[0]["officemob"].ToString();
    //                //txtaltno.Text = ds_userdetails.Tables[0].Rows[0]["alternatemob"].ToString();
    //                //txtoffphone.Text = ds_userdetails.Tables[0].Rows[0]["officephone"].ToString();
    //                //txtfaxno.Text = ds_userdetails.Tables[0].Rows[0]["faxno"].ToString();
    //              //  txtloc.Text = ds_userdetails.Tables[0].Rows[0]["location"].ToString();
    //                //txtdept.Text = ds_userdetails.Tables[0].Rows[0]["department"].ToString();
    //                //txtsubdept.Text = ds_userdetails.Tables[0].Rows[0]["sub_department"].ToString();
    //                //txtdesg.Text = ds_userdetails.Tables[0].Rows[0]["designation"].ToString();

    //                //DateTime dob1 = new DateTime();

    //                //if (ds_userdetails.Tables[0].Rows[0]["DOB1"].ToString().Length > 0 && ds_userdetails.Tables[0].Rows[0]["DOB1"].ToString() != null && ds_userdetails.Tables[0].Rows[0]["DOB1"].ToString() != "")
    //                //{
    //                //    dob1 = Convert.ToDateTime(ds_userdetails.Tables[0].Rows[0]["DOB1"]);
    //                //    if (dob1.ToString("dd/MMM/yyyy") == "01/Jan/1900")
    //                //    {
    //                //        txtdob1.Text = "";
    //                //    }
    //                //    else
    //                //    {
    //                //        txtdob1.Text = dob1.ToString("dd/MMM/yyyy");
    //                //    }
    //                //}
    //                //else
    //                //{
    //                //    txtdob1.Text = "";
    //                //}


    //                DateTime dob = new DateTime();

    //                if (ds_userdetails.Tables[0].Rows[0]["DOB"].ToString().Length > 0 && ds_userdetails.Tables[0].Rows[0]["DOB"].ToString() != null && ds_userdetails.Tables[0].Rows[0]["DOB"].ToString() != "")
    //                {
    //                    dob = Convert.ToDateTime(ds_userdetails.Tables[0].Rows[0]["DOB"]);
    //                    if (dob.ToString("dd/MMM/yyyy") == "01/Jan/1900")
    //                    {
    //                       // txtdob.Text = "";
    //                    }
    //                    else
    //                    {
    //                      //  txtdob.Text = dob.ToString("dd/MMM/yyyy");
    //                    }
    //                }
    //                else
    //                {
    //                    //txtdob.Text = "";
    //                }

    //                string gen = ds_userdetails.Tables[0].Rows[0]["gender"].ToString();
    //                if (gen == "M" || gen == "m")
    //                {
    //                    //rbtnmale.Checked = true;
    //                }
    //                else
    //                {
    //                    //rbtnfemale.Checked = true;
    //                }


    //                DataTable user2 = classreviews.getuseridbyemail(Page.User.Identity.Name);

    //                if (user2.Rows.Count > 0)
    //                {
    //                    userid = user2.Rows[0]["indexid"].ToString();
    //                    if (user2.Rows[0]["profilephoto"].ToString() != "")
    //                    {
    //                        pimg = user2.Rows[0]["profilephoto"].ToString().Trim();
    //                        if (user2.Rows[0]["profilephoto"].ToString().Trim() == "noimage1.png" || user2.Rows[0]["profilephoto"].ToString().Trim() == "noimage3.jpg")
    //                        {
    //                          //  removeprofile.Visible = false;
    //                        }
    //                        else
    //                        {
    //                           // removeprofile.Visible = true;
    //                        }
    //                        if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profilephoto/" + user2.Rows[0]["profilephoto"].ToString())))
    //                        {
    //                          //  imgprofile.Src = ReturnUrl("sitepath") + "images/profilephoto/" + user2.Rows[0]["profilephoto"].ToString();
    //                        }
    //                        else
    //                        {
    //                           // imgprofile.Src = "http://graph.facebook.com/" + user2.Rows[0]["profilephoto"].ToString() + "/picture?type=large";
    //                        }
    //                    }
    //                    else
    //                    {
    //                       // imgprofile.Src = ConfigurationManager.AppSettings["adminsitepath"]+ "images/noprofile.jpg";
    //                       // removeprofile.Visible = false;
    //                    }
    //                    if (File.Exists(Server.MapPath("~/themes/creative1.0/images/coverphoto/" + user2.Rows[0]["coverphoto"].ToString())))
    //                    {
    //                        cimg = user2.Rows[0]["coverphoto"].ToString().Trim();
    //                        //imgcover.Src = ReturnUrl("sitepath") + "images/coverphoto/" + user2.Rows[0]["coverphoto"].ToString();
    //                    }
    //                    else
    //                    {
    //                        //imgcover.Visible = false;
    //                        //removecover.Visible = false;
    //                    }
    //                }
    //                else
    //                {
    //                   // imgprofile.Visible = false;
    //                    //imgcover.Visible = false;
    //                }
                   
                   
    //            }
    //        }
            
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}

    protected void checkHR_Inbox()
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_Check_TD_COS_apprver_code";

            spars[1] = new SqlParameter("@exp_trvl", SqlDbType.VarChar);
            spars[1].Value = "HRML";

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = hflEmpCode.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");
            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                get_Pending_LeaveReqstList_cnt();
                lnk_HRLeaveInbox.Visible = true;
                span_Hr_head.Visible = true;
                get_Pending_AttendanceReqstList_cnt();
                //lnk_HRAttendanceInbox.Visible = true;

            
                 lnk_LeaveRequest_FrmHR.Visible = true;
                 //lnk_TeamCalendar.Visible = true;
                 lnk_leavereport_Hr.Visible = true;
            
            }
            else
            {
                lnk_HRLeaveInbox.Visible = false;
                span_Hr_head.Visible = false;
                lnk_LeaveRequest_FrmHR.Visible = false;
                //lnk_TeamCalendar.Visible = false;
                lnk_leavereport_Hr.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void get_Pending_LeaveReqstList_cnt()
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_inboxlst_cnt_HR";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = hflEmpCode.Value;

            dsTrDetails = spm.getDatasetList(spars, "Usp_getEmployee_Details_All");

            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                lnk_HRLeaveInbox.Text = "HR Inbox:(" + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["leave_reqst_pending"]).Trim() + ")";
            }
             
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void get_Pending_AttendanceReqstList_cnt()
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_emp_Regularise_atten_HR_inboxlst_cnt";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = hflEmpCode.Value;
            dsTrDetails = spm.getDatasetList(spars, "Usp_getEmployee_Details_All");

            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                //lnk_HRAttendanceInbox.Text = "HR Inbox Attendance requests:(" + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["attendcnt"]).Trim() + ")";
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public Boolean check_ISLoginEmployee_ForLeave()
    {
        Boolean bchkEMP = false;
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Check_ISapplicable_Leave";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
			spars[1].Value =hflEmpCode.Value; 
            //spars[1].Value ="00008727";
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

    protected void lnk_leaverequest_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(hdnempwrkSchdule.Value).Trim()=="6")
            Response.Redirect("~/procs/Leave_Req_6.aspx");
        else
            Response.Redirect("~/procs/Leave_Req.aspx");
         
    }
    protected void lnk_mng_leaverequest_Click(object sender, EventArgs e)
    {

        if (Convert.ToString(hdnempwrkSchdule.Value).Trim() == "6")
            Response.Redirect("~/procs/MyLeave_Req_6.aspx");
        else
            Response.Redirect("~/procs/MyLeave_Req.aspx");
    }
    
}
