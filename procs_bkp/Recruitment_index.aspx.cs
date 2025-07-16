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



public partial class Recruitment_index : System.Web.UI.Page
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
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionendR.aspx");
            }


            hflEmpCode.Value  = Convert.ToString(Session["Empcode"]);
        // Session["Empcode"] = "11536";
        //  Session["Empcode"] = "12443";

          //  Session["Empcode"] = "11536";
            //Session["Empcode"] = "11549";


            //hflEmpCode.Value = "11111";
            //Session["Empcode"] = "11111";
            //hflEmpCode.Value = "22222";
            //Session["empcode"] = "22222";
            //hflEmpCode.Value = "33333";
            //Session["empcode"] = "33333";

            //Session["emp_loginName"] = "Ms. User M1";
           // Response.Write(Session["Empcode"]);
              lblmsg.Visible = false;
            //  lblmsg.Text =Convert.ToString(Session["Empcode"]); 
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "loginR.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Recruitment_index");
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

                    delete_tmp_Functions_positions();
                    GetEmployeeDetails();
                    //GetMobileEligibility_New();
                    get_emp_fule_eligibility();
                    
                    //Check is Recruiter Login
                    check_isRecruiter_login();
                    lnk_Attendancereg.Visible = false;
                    lnk_mng_Attendancereg.Visible = false;
                    lnk_leaverequest.Visible = false;
                    lnk_mng_leaverequest.Visible = false;
                    if (Convert.ToString(get_Recuiter_isCreate_Position()).Trim()=="Y")
                    {
                        lnk_Attendancereg.Visible = true;
                        lnk_mng_Attendancereg.Visible = true;
                    }

                    if (Convert.ToString(get_Recuiter_isCandiates()).Trim() == "Y")
                    {
                        lnk_leaverequest.Visible = true;
                        lnk_mng_leaverequest.Visible = true;
                    }

                    //txtReason.Text = "This is after function";
                    /*Comment 
                     CheckApprover();
                     check_COS_ACC("RCOS");
                     check_COS_ACC("RACC");
                     check_COS_ACC("RCFO");

                    check_COS_ACC_Fuel("RCOS");
                    check_COS_ACC_Fuel("RACC");
                    */
                    DisplayProfileProperties();

                     loadorder();
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
            //try
            //{
            //    DataSet dsTrDetails = new DataSet();
            //    SqlParameter[] spars = new SqlParameter[2];

            //    spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            //    spars[0].Value = "getMobclaim_Pendinglst_Approver";
            //    //spars[0].Value = "getMobclaim_Pendinglst_Approver";

            //    spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            //    spars[1].Value = hflEmpCode.Value;

            //    dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            //    if (dsTrDetails.Tables[0].Rows.Count > 0)
            //    {
            //        lnk_leaveinbox.Text = "Inbox Mobile Reimbursement :(" + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["mobresPendinglist"]).Trim() + ")";
            //        lnk_leaveinbox.Visible = true;
            //        lnk_leaveParametersmst.Visible = true;
            //    }


            //}
            //catch (Exception ex)
            //{
            //    Response.Write(ex.Message.ToString());
            //}
            
        //}         

    }

    protected void GetLeaveCount()
    {

        int LeaveCount = 0;
        LeaveCount = spm.GetLeaveInboxCount(Convert.ToString(hflEmpCode.Value).Trim());

       // lnk_attendanceinbox.Text = "Attendance regularization requests:(" + AttLeaveCount.ToString() + ")";

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


    
    protected void loadorder()
    {
        DataTable dtp = classpkg.getcountorderdetailbyemaild(Page.User.Identity.Name.ToString().Trim());
        if (dtp.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtp.Rows[0]["orderid"].ToString()) == 0)
            {
                lihistory.Visible = false;
            }
        }
        else
        {
            lihistory.Visible = true;
        }
    }
    private void DisplayProfileProperties()
    {
        try
        {
            Boolean varfindcity = false;

            MembershipUser user = Membership.GetUser(this.Page.User.Identity.Name.ToString().Trim());
            DataSet ds_userdetails = classaddress.getalluserbyusername(this.Page.User.Identity.Name.ToString().Trim());
            if (ds_userdetails.Tables.Count > 0)
            {
                if( ds_userdetails.Tables[0].Rows.Count > 0)
                {
                   // txtemail.Text = ds_userdetails.Tables[0].Rows[0]["username"].ToString();
                   // txtemailadress.Text = ds_userdetails.Tables[0].Rows[0]["emailid"].ToString();
                    //txtfirstname.Text = ds_userdetails.Tables[0].Rows[0]["firstname"].ToString();
                    //txtlastname.Text = ds_userdetails.Tables[0].Rows[0]["lastname"].ToString();
                    //txtaddress1.Text = ds_userdetails.Tables[0].Rows[0]["address"].ToString();
                   // txtpincode.Text = ds_userdetails.Tables[0].Rows[0]["pincode"].ToString();
                    //txtphone.Text = ds_userdetails.Tables[0].Rows[0]["telno"].ToString();
                    //txtmobile.Text = ds_userdetails.Tables[0].Rows[0]["mobileno"].ToString();
                    //txttempaddress.Text = ds_userdetails.Tables[0].Rows[0]["tempaddress"].ToString();
                    //txtaltemail.Text = ds_userdetails.Tables[0].Rows[0]["alternateemail"].ToString();
                    //txtextension.Text = ds_userdetails.Tables[0].Rows[0]["extentionno"].ToString();
                    //txtoffno.Text = ds_userdetails.Tables[0].Rows[0]["officemob"].ToString();
                    //txtaltno.Text = ds_userdetails.Tables[0].Rows[0]["alternatemob"].ToString();
                    //txtoffphone.Text = ds_userdetails.Tables[0].Rows[0]["officephone"].ToString();
                    //txtfaxno.Text = ds_userdetails.Tables[0].Rows[0]["faxno"].ToString();
                  //  txtloc.Text = ds_userdetails.Tables[0].Rows[0]["location"].ToString();
                    //txtdept.Text = ds_userdetails.Tables[0].Rows[0]["department"].ToString();
                    //txtsubdept.Text = ds_userdetails.Tables[0].Rows[0]["sub_department"].ToString();
                    //txtdesg.Text = ds_userdetails.Tables[0].Rows[0]["designation"].ToString();

                    //DateTime dob1 = new DateTime();

                    //if (ds_userdetails.Tables[0].Rows[0]["DOB1"].ToString().Length > 0 && ds_userdetails.Tables[0].Rows[0]["DOB1"].ToString() != null && ds_userdetails.Tables[0].Rows[0]["DOB1"].ToString() != "")
                    //{
                    //    dob1 = Convert.ToDateTime(ds_userdetails.Tables[0].Rows[0]["DOB1"]);
                    //    if (dob1.ToString("dd/MMM/yyyy") == "01/Jan/1900")
                    //    {
                    //        txtdob1.Text = "";
                    //    }
                    //    else
                    //    {
                    //        txtdob1.Text = dob1.ToString("dd/MMM/yyyy");
                    //    }
                    //}
                    //else
                    //{
                    //    txtdob1.Text = "";
                    //}


                    DateTime dob = new DateTime();

                    if (ds_userdetails.Tables[0].Rows[0]["DOB"].ToString().Length > 0 && ds_userdetails.Tables[0].Rows[0]["DOB"].ToString() != null && ds_userdetails.Tables[0].Rows[0]["DOB"].ToString() != "")
                    {
                        dob = Convert.ToDateTime(ds_userdetails.Tables[0].Rows[0]["DOB"]);
                        if (dob.ToString("dd/MMM/yyyy") == "01/Jan/1900")
                        {
                           // txtdob.Text = "";
                        }
                        else
                        {
                          //  txtdob.Text = dob.ToString("dd/MMM/yyyy");
                        }
                    }
                    else
                    {
                        //txtdob.Text = "";
                    }

                    string gen = ds_userdetails.Tables[0].Rows[0]["gender"].ToString();
                    if (gen == "M" || gen == "m")
                    {
                        //rbtnmale.Checked = true;
                    }
                    else
                    {
                        //rbtnfemale.Checked = true;
                    }


                    DataTable user2 = classreviews.getuseridbyemail(Page.User.Identity.Name);

                    if (user2.Rows.Count > 0)
                    {
                        userid = user2.Rows[0]["indexid"].ToString();
                        if (user2.Rows[0]["profilephoto"].ToString() != "")
                        {
                            pimg = user2.Rows[0]["profilephoto"].ToString().Trim();
                            if (user2.Rows[0]["profilephoto"].ToString().Trim() == "noimage1.png" || user2.Rows[0]["profilephoto"].ToString().Trim() == "noimage3.jpg")
                            {
                              //  removeprofile.Visible = false;
                            }
                            else
                            {
                               // removeprofile.Visible = true;
                            }
                            if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profilephoto/" + user2.Rows[0]["profilephoto"].ToString())))
                            {
                              //  imgprofile.Src = ReturnUrl("sitepath") + "images/profilephoto/" + user2.Rows[0]["profilephoto"].ToString();
                            }
                            else
                            {
                               // imgprofile.Src = "http://graph.facebook.com/" + user2.Rows[0]["profilephoto"].ToString() + "/picture?type=large";
                            }
                        }
                        else
                        {
                           // imgprofile.Src = ConfigurationManager.AppSettings["adminsitepath"]+ "images/noprofile.jpg";
                           // removeprofile.Visible = false;
                        }
                        if (File.Exists(Server.MapPath("~/themes/creative1.0/images/coverphoto/" + user2.Rows[0]["coverphoto"].ToString())))
                        {
                            cimg = user2.Rows[0]["coverphoto"].ToString().Trim();
                            //imgcover.Src = ReturnUrl("sitepath") + "images/coverphoto/" + user2.Rows[0]["coverphoto"].ToString();
                        }
                        else
                        {
                            //imgcover.Visible = false;
                            //removecover.Visible = false;
                        }
                    }
                    else
                    {
                       // imgprofile.Visible = false;
                        //imgcover.Visible = false;
                    }
                   
                   
                }
            }
            
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnk_leaverequest_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() != "")
        {
            delete_tmp_job_Qualification();
            //spm.clear_Reimbursement_temp_tables(hflEmpCode.Value, "DeleteMobileTemp");
            Response.Redirect("~/procs/Recruitments.aspx");
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
            Session["postitle"] = "";
            Session["posDesp"] = "";
            Session["recruiterCode"] ="";

            delete_tmp_job_positions();
            Response.Redirect("~/procs/positioncreation.aspx");
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
        dtApproverEmailIds = spm.GetMobilelEligibilityMatrix_Emp_Grade(Convert.ToString(hflGrade.Value), Convert.ToString(hflEmpCode.Value));
        //dtApproverEmailIds = spm.GetMobilelEligibilityMatrix(Convert.ToString(hdnGrade.Value));

        if (dtApproverEmailIds.Rows.Count <= 0)
        {  
          //  lblmessage.Text = "Sorry You are not entitled for mobile claims!";
            lnk_leaverequest.Visible = false;
            lnk_mng_leaverequest.Visible = false;
        }
        if (dtApproverEmailIds.Rows.Count > 0)
        {
            if (Convert.ToString(dtApproverEmailIds.Rows[0]["Eligibility"]).Trim() != "")
            {
                if (Convert.ToDecimal(dtApproverEmailIds.Rows[0]["Eligibility"]) <= 0)
                {
                  //  lblmessage.Text = "Sorry You are not entitled for mobile claims!";
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

                //lblmessage.Text = "Sorry You are not entitled for Fuel claims!";

                #endregion

            }



        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }

    }


    protected void delete_tmp_Functions_positions()
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_delete_functins_Positions_tmp";

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = hflEmpCode.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_Recruitment_Masters");
            
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void delete_tmp_job_positions()
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_delete_jobs_Positions_tmp";

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = hflEmpCode.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_Recruitment_Masters");

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void delete_tmp_job_Qualification()
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_delete_Qualification_tmp";

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = hflEmpCode.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_Recruitment_Masters");

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void check_isRecruiter_login()
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_check_isRecruiter_login";

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = hflEmpCode.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_Recruitment_Masters");
            lnk_leavereport.Visible = false;
            if (dsTrDetails.Tables[0].Rows.Count>0)
            {
               // lnk_leavereport.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }


    private string get_Recuiter_isCreate_Position()
    {
        string strYesNo = "N";
        try
        {

            DataSet tmpds_pv = new DataSet();

            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_get_recruiter_isCreate_position";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value).Trim();

            tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Masters");


            if (tmpds_pv.Tables[0].Rows.Count > 0)
            {
                strYesNo = Convert.ToString(tmpds_pv.Tables[0].Rows[0]["is_pos_creation"]).Trim();
            }

        }
        catch (Exception ex)
        {

        }
        return strYesNo;
    }

    private string get_Recuiter_isCandiates()
    {
        string strYesNo = "N";
        try
        {

            DataSet tmpds_pv = new DataSet();

            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_get_recruiter_isCreate_Candiates";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value).Trim();

            tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Masters");


            if (tmpds_pv.Tables[0].Rows.Count > 0)
            {
                strYesNo = "Y";
            }

        }
        catch (Exception ex)
        {

        }
        return strYesNo;
    }

}
