using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Security;
using System.Web.UI;



public partial class Appraisalindex : System.Web.UI.Page
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
    string sRegsignationDt = "";
     

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url;}

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            //Empcode_Appr
            if (Convert.ToString(Session["Empcode_Appr"]).Trim() == "" || Session["Empcode_Appr"] == null)
            {
               Response.Redirect(ReturnUrl("sitepathmain") + "procs/Appraisal_login.aspx"); 
            }


           hflEmpCode.Value  = Convert.ToString(Session["Empcode_Appr"]);       
           lblmsg.Visible = false;
             
            //  lblmsg.Text =Convert.ToString(Session["Empcode"]); 

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Appraisalindex.aspx");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {

                    //#region Check Login Employee is  applicable for Appraisal
                    //if (check_ISLoginEmployee_ForReimbursment() == false)
                    //{
                    //    lblheading.Text = "Appraisal Module (Coming Soon...)";
                    //    editform1.Visible = false;
                    //    return;
                    //}
                    //#endregion
                    //hdnSingPOCopyFilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PMSUserManual"]).Trim());
                    hdnSingPOCopyFilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBUploadCutofdatafiles"]).Trim());
                    hdnSingPOCopyFileName.Value = "AppraisalUserManual.pdf";
                    lnk_mng_PerformanceRevList.Visible = false;
                    lnk_mng_RecommendationList.Visible = false;
                    lnk_TeamCalendar.Visible = false;

                    lnk_HODTEAM.Visible = false;

                    GetEmployeeDetails();
                    GetHighprofileUserList();

                    GetInboxTotalCountPer();
                    GetInboxTotalCountRecco();

                    GetPerformanceCount();
                    GetREcommenCount();
                    GetSelfAssessmentCount();

                    GetHODCount();

                    CheckIs_AppraisalProcessStatus_Access();

                    Get_AppraisalProcess_Report_Access();
	
		    getAssessmentList();

                    if(Convert.ToString(hflEmpCode.Value).Trim()=="00002726")
                    {
                        lnkUploadAppraisalData.Visible = true;
                    }
                    DisplayProfileProperties();
                    loadorder();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                    lnk_viewAppraisalForm.Visible = false;
 		    if(Convert.ToString(sRegsignationDt).Trim()!="")
                    {
                        lnk_CreateSelfAssessment.Visible = false;
                        lnk_mng_SelfAssessment.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    private void Get_AppraisalProcess_Report_Access()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "rpt_IsAppraisalProcess_Access";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = hflEmpCode.Value;



        dsList = spm.ApprgetDatasetList(spars, "SP_Rpt_Appraisal_Process");

        if (dsList != null)
        {
            if (dsList.Tables[0].Rows.Count > 0)
            {
                lnk_AppraisalProcess.Visible = true; 
            }
        }
       

    }

    public void CheckIs_AppraisalProcessStatus_Access()
    {
        DataSet getdtDetails = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "check_AppraisalProcessStatus_Access";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@AssessStatus", SqlDbType.VarChar);
            spars[2].Value = "AppraisalProcessStatus";

            getdtDetails = spm.ApprgetDatasetList(spars, "[SP_GETALL_Appraisal_DETAILS]");
            tdAppraisalProcessStatus.Visible = false;
            if (getdtDetails.Tables[0].Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Tables[0].Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    tdAppraisalProcessStatus.Visible = true; 
                }
            }
            // return false;
        }
        catch (Exception ex)
        {
            // return false;
        }
    }

    protected void GetInboxTotalCountPer()
    {
        int PerCount = 0;
        PerCount = spm.GetInboxTotalCount(Convert.ToString(Session["Empcode"]).Trim(), 2, 2 ,2);
        if (PerCount > 0)
        {
            lnk_mng_PerformanceRevList.Visible = true;
            lnk_TeamCalendar.Visible = true;
        }
        else
        {
            lnk_mng_PerformanceRevList.Visible = false;
            lnk_TeamCalendar.Visible = false;
        }
            
        

    }

    protected void GetHighprofileUserList()
    {
        int PerCount = 0;
        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "SP_UserwithoutAppraisal";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();


        dsTrDetails = spm.ApprgetDatasetList(spars, "[SP_GETALL_Appraisal_ApproverDetails]");
        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            PerCount = Convert.ToInt32(dsTrDetails.Tables[0].Rows[0]["cnt"]);
        }
        if (PerCount > 0)
        {
            lnk_CreateSelfAssessment.Visible = false;
            lnk_mng_SelfAssessment.Visible = false;
            lnk_viewAppraisalForm.Visible = false;
        }
        else
        {
            lnk_CreateSelfAssessment.Visible = true;
            lnk_mng_SelfAssessment.Visible = true;
            lnk_viewAppraisalForm.Visible = true;
        }
    }
    protected void GetHODCount()
    {
        int PerCount = 0;
        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "getHODInboxCount";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();


        dsTrDetails = spm.ApprgetDatasetList(spars, "[SP_GETALL_Appraisal_DETAILS]");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            PerCount = Convert.ToInt32(dsTrDetails.Tables[0].Rows[0]["cnt"]);
        }           
        if (PerCount > 0)
        {
            lnk_HODTEAM.Visible = true;            
        }
        else
        {
            lnk_HODTEAM.Visible = false;
         
        }



    }

    protected void GetInboxTotalCountRecco()
    {
        int PerCount = 0;
        //PerCount = spm.GetInboxTotalCount(Convert.ToString(Session["Empcode"]).Trim(), 2, 3, 4);
        PerCount = spm.GetInboxTotalCount(Convert.ToString(Session["Empcode"]).Trim(), 0, 3, 4);
        if (PerCount > 0)
        {
            lnk_mng_RecommendationList.Visible = true;
            lnk_TeamCalendar.Visible = true;
        }
        else
        {
            lnk_mng_RecommendationList.Visible = false;
            //lnk_TeamCalendar.Visible = false;
        }
    
     
    }

    protected void GetSelfAssessmentCount()
    {
        int SelfAssessmentCount = 0;
        SelfAssessmentCount = spm.GetInboxPendingCount(Convert.ToString(Session["Empcode"]).Trim(), 1);
        int ManAssessCount = 0;
        ManAssessCount = spm.GetInboxPendingCount(Convert.ToString(Session["Empcode"]).Trim(), 3);
        SelfAssessmentCount = SelfAssessmentCount + ManAssessCount;
        lnk_mng_SelfAssessment.Text = "Manage Self Assessment : (" + SelfAssessmentCount.ToString() + ")";

    }
   

    protected void GetPerformanceCount()
    {
        int PerCount = 0;
        PerCount = spm.GetInboxPendingCount(Convert.ToString(Session["Empcode"]).Trim(), 2);
        lnk_mng_PerformanceRevList.Visible = false;
        if (PerCount > 0)
        {
            lnk_mng_PerformanceRevList.Text = "Performance Review List : (" + PerCount.ToString() + ")";
            lnk_mng_PerformanceRevList.Visible = true;
        }

    }
    protected void GetREcommenCount()
    {
        int PerCount = 0;
        PerCount = spm.GetInboxPendingCount(Convert.ToString(Session["Empcode"]).Trim(), 4);
        lnk_mng_RecommendationList.Visible = false;
        if (PerCount > 0)
        {
            lnk_mng_RecommendationList.Text = "Recommedation List : (" + PerCount.ToString() + ")";
            lnk_mng_RecommendationList.Visible = true;
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
 		sRegsignationDt = Convert.ToString(dtEmpDetails.Rows[0]["Resig_Date"]).Trim();
            }
            //  getApproverdata();  
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

            throw;
        }
    }

   private void getAssessmentList()
    {
        try
        {
            DataTable dtTravelRequest = new DataTable();
            dtTravelRequest = spm.getSelfAssessList(Convert.ToString(hflEmpCode.Value));
             
            if (dtTravelRequest.Rows.Count == 0)
            {
                lnk_CreateSelfAssessment.Visible = false; 
            }
            

        }
        catch (Exception ex)
        {

        }
    }
   
}
