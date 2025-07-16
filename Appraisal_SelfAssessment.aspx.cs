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
using Microsoft.Reporting.WebForms;
using System.Security.Cryptography;


public partial class Appraisal_SelfAssessment : System.Web.UI.Page
{

    #region CreativeMethods

    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
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
                if (ds_userdetails.Tables[0].Rows.Count > 0)
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

                    //  fillcountry();
                    country country = classaddress.GetCountryDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["country"].ToString()));
                    // ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByText(country.countryName.Trim()));

                    //  fillstate(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["country"].ToString()));
                    states state = classaddress.GetStateDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["state"].ToString()));
                    // ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByText(state.statename.Trim()));

                    //       fillcity(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["state"].ToString()));

                    city city = classaddress.GetCityDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["city"].ToString()));

                    //  ddlcity1.SelectedValue = ds_userdetails.Tables[0].Rows[0]["city"].ToString().Trim();
                    // txtcity.Text = ddlcity1.SelectedItem.Text.Trim();
                }
            }

        }
        catch (Exception ex)
        {

        }
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    #endregion

    SP_Methods spm = new SP_Methods();
    DataSet dspaymentVoucher_Apprs = new DataSet();

    #region PageEvents
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            lblmessage.Text = "";
            lblmsg.Visible = false; 
            
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Appraisalindex");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    divbtn.Visible = false;
                    divPRD.Visible = false;
                    divIDP.Visible = false;

                    txtDissHeldOn.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    
                    txtEmpCode.Text = Convert.ToString(Session["Empcode"]);                   
                    //GetEmployeeDetails();
                   
                  
                    //txtEmpCode.Text = Convert.ToString(Session["ReqEmpCode"]);
                    hdnGrade.Value = Convert.ToString(Session["Grade"]);
                    getActiveAppYear();
                    //getAddedAssessmentDates(Convert.ToString(Session["Empcode"]));
                    txtAppYearid.Enabled = false;
                    txtPeriod.Enabled = false;
                    txtEmpCode.Enabled = false;
                    txtEmpName.Enabled = false;
                    txtFromdate.Enabled = false;
                    txtToDate.Enabled = false;
                    Span5.Visible = false;
                    lifrom.Visible = false;
                    liTo.Visible = false;
                    

                    if (Request.QueryString.Count == 2) //Manage br Reviewee
                    {
                        hdnAssessid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        mngAssess.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        usercheck(Convert.ToString(Session["Empcode"]), Convert.ToDecimal(hdnAssessid.Value));
                        usercheckValidDate(Convert.ToDecimal(hdnAssessid.Value));
                        gettranApproverStatus();
                        GetEmployeeDetails(Convert.ToDecimal(hdnAssessid.Value));
                        if (Convert.ToString(mngAssess.Value) == "1")
                        { 
                            SpanPRD.Visible = false;
                            divPRD.Visible = false;
                            SpanIDP.Visible = false;
                            divIDP.Visible = false;
                            SpanRevieweeDis.Visible = false;
                            Span11.Visible = false;
                            txtRevieweeDis.Visible = false;
                            SpanDissHeldOn.Visible = false;
                            txtDissHeldOn.Visible = false;
                        }
                        if (Convert.ToString(mngAssess.Value) == "RePV")
                        {
                           // GetEmployeeDetails(Convert.ToDecimal(hdnAssessid.Value));
                            lblheading.Text = "Performance Review View";
                            EnableControlsPerformanceReviewReviewee();
                            getAssessReviewDetails(Convert.ToDecimal(hdnAssessid.Value));
                            divPRD.Visible = true;
                            divIDP.Visible = true;
                            
                            if (Convert.ToString(txtRevieweeDis.Text) !="")
                            {
                                liRecoDissN1.Visible = true;
                                txtRecoDissN1.Visible = true;
                                
                                RecoDissN1.Visible = true;
                                RecoDissN2.Visible = true;
                                
                                txtRecoDissN2.Visible = true;
                                //Span9.Visible = true;
                                RecoDissAD1.Visible = true;
                                txtRecoDissAD1.Visible = true;
                                //Span10.Visible = true;
                            }
                            txtRevieweeDis.Enabled = false;
                            chkRevieweeAgree.Enabled = false;
                            chkRevieweeDisAgree.Enabled = false;
                            chkRevieweeDisAgree.BackColor = Color.White;
                            chkRevieweeAgree.BackColor = Color.White;

                            btnSave.Visible = false;

                        }
                        if (Convert.ToString(mngAssess.Value) == "ReV")
                        {
                            lblheading.Text = "Self Assessment : View ";
                            EnableControlsReviewView_SelfAssessmentSubmit();
                        }
                        if (Convert.ToString(mngAssess.Value) == "Rwee")
                        {
                            lblheading.Text = "Self Assessment";
                            EnableControlsReviewView_SelfAssessmentPending();
                        }
                        if (Convert.ToString(mngAssess.Value) == "ReP")
                        {
                           // GetEmployeeDetails(Convert.ToDecimal(hdnAssessid.Value));
                            liStrengthadd.Visible = false;
                            liRecoDissN1.Visible = false;
                            liRecoDissN2.Visible = false;
                            //Span9.Visible = false;
                            liRecoDissAD1.Visible = false;
                            //Span10.Visible = false;
                            ligvRecomm.Visible = false;
                            lblheading.Text = "Performance Review : " + txtEmpName.Text;
                            EnableControlsPerformanceReviewReviewee();
                            getAssessReviewDetails(Convert.ToDecimal(hdnAssessid.Value));
                            divPRD.Visible = true;
                            divIDP.Visible = true;
                        }
                         if (Convert.ToString(mngAssess.Value) == "PerR")
                        {
                             //GetEmployeeDetails(Convert.ToDecimal(hdnAssessid.Value));
                             lblheading.Text = "Performance Review : "+ txtEmpName.Text;
                             EnableControlsPerformanceReview();
                             btnTra_Details.Enabled = true;
                             btnTra_Details.Visible = true;
                             getAssessReviewDetails(Convert.ToDecimal(hdnAssessid.Value));
                        }
                       
                        if (Convert.ToString(mngAssess.Value) == "2")
                        {
                            SpanPRD.Visible = true;
                            //divPRD.Visible = true;
                            SpanIDP.Visible = true;
                            divIDP.Visible = true;
                            SpanRevieweeDis.Visible = true;
                            Span11.Visible = true;
                            txtRevieweeDis.Visible = true;
                            SpanDissHeldOn.Visible = true;
                            txtDissHeldOn.Visible = true;
                            gvDevPlan.Focus();
                            txtRevieweeDis.Enabled = false;                           
                        }
                        getselfassessmentReleaseon(Convert.ToDecimal(hdnAssessid.Value));
                        
                        getAssessIDDetails(Convert.ToDecimal(hdnAssessid.Value));
                        getApproverdata();
                        
                        getTargetEvaluationDeails(Convert.ToDecimal(hdnAssessid.Value));
                        getCompetancyEvaluationDeails(Convert.ToDecimal(hdnAssessid.Value));
                        //GetKRATemplates();
                        getKRAPointsSum();
                        gvDevPlanDetails(Convert.ToDecimal(hdnAssessid.Value));
                        getMethodDetails();
                        gvStrengthDetails(Convert.ToDecimal(hdnAssessid.Value));
                    }
                    if (Request.QueryString.Count == 3) //Recommendation
                    {
                        
                        hdnAssessid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        mngAssess.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        hdnAssessType.Value = Convert.ToString(Request.QueryString[2]).Trim();
                        if ((Convert.ToString(hdnAssessType.Value) != "RevWerEd") && (Convert.ToString(hdnAssessType.Value) != "RevWerHd"))
                        {
                            usercheck(Convert.ToString(Session["Empcode"]), Convert.ToDecimal(hdnAssessid.Value));
                            btnSave.Visible = true;
                        }
                        else
                        {
                            btnSave.Visible = false;
                        }
                        if ((Convert.ToString(hdnAssessType.Value) != "RevWerEd") && (Convert.ToString(hdnAssessType.Value) != "RevWerHd"))
                        {
                            usercheckValidDate(Convert.ToDecimal(hdnAssessid.Value));
                        }
                        gettranApproverStatus();
                        
                        GetEmployeeDetails(Convert.ToDecimal(hdnAssessid.Value));
                        lblheading.Text = "Recommendation : " + txtEmpName.Text;
                        EnableControlsPerformanceReviewReviewee();
                        liStrengthadd.Visible = true;
                        SpanStrengthdesc.Visible = true;
                        ligvStrength.Visible = true;
                        gvStrength.Visible = true;

                        lichkRevieweeAgree.Visible = true;
                        liSpan2.Visible = true;
                        lichkRevieweeDisAgree.Visible = true;
                        liSpan6.Visible = true;
                        //txtRecoDissN1.Enabled = true;
                        //txtRecoDissN2.Enabled = true;
                        //txtRecoDissAD1.Enabled = true;
                        dgTargetEvaluation.Columns[4].Visible = true;
                        dgTargetEvaluation.Columns[5].Visible = true;
                        dgCompetancyEvaluation.Columns[3].Visible = false;
                        dgCompetancyEvaluation.Columns[4].Visible = true;
                        getAssessReviewDetails(Convert.ToDecimal(hdnAssessid.Value));
                        txtRevieweeDis.Enabled = false;
                        btnlnkRecommendation.Visible = true;
                        Span1.Visible = true;
                        getAssessIDDetails(Convert.ToDecimal(hdnAssessid.Value));
                        getApproverdata();
                        

                        getTargetEvaluationDeails(Convert.ToDecimal(hdnAssessid.Value));
                        getCompetancyEvaluationDeails(Convert.ToDecimal(hdnAssessid.Value));
                        //GetKRATemplates();
                        getKRAPointsSum();
                        gvDevPlanDetails(Convert.ToDecimal(hdnAssessid.Value));
                        getMethodDetails();
                        if ((Convert.ToString(hdnAssessType.Value) == "RevWerEd") || (Convert.ToString(hdnAssessType.Value) == "RevWerHd"))
                        {
                            string managestatus = null;
                            if (Convert.ToString(mngAssess.Value) == "N1V")
                            {
                                managestatus = "2";
                            }
                            if (Convert.ToString(mngAssess.Value) == "N2V")
                            {
                                managestatus = "3";
                            }
                            if ((Convert.ToString(mngAssess.Value) == "AD1V")||(Convert.ToString(mngAssess.Value) == "AD1VP")) 
                            {
                                managestatus = "4";
                            }

                            gvRecommDetails_view(Convert.ToDecimal(hdnAssessid.Value), Convert.ToDecimal(managestatus.ToString()));
                            
                        }
                        else
                        {
                            gvRecommDetails(Convert.ToDecimal(hdnAssessid.Value));
                        }
                        gvStrengthDetails(Convert.ToDecimal(hdnAssessid.Value));
                        chkRevieweeAgree.Enabled = false;
                        chkRevieweeDisAgree.Enabled = false;
                        chkRevieweeDisAgree.BackColor = Color.White;
                        chkRevieweeAgree.BackColor = Color.White;
                        divPRD.Visible = true;
                        divIDP.Visible = true;
                        getrecoCount();
                        if (Convert.ToString(txtRevieweeDis.Text) != "")
                        { 
                            if ((Convert.ToString(mngAssess.Value) == "N1")||(Convert.ToString(mngAssess.Value) == "N1V"))
                            {

                                liRecoDissN1.Visible = true;
                                txtRecoDissN1.Visible = true;
                                RecoDissN1.Visible = true;
                                txtRecoDissN2.Visible = false;
                                RecoDissN2.Visible = false;
                              //  Span9.Visible = false;
                                txtRecoDissAD1.Visible = false;
                                RecoDissAD1.Visible = false;
                              //  Span10.Visible = false;
                                txtRecoDissN1.Enabled = true;
                               
                            }
                            if ((Convert.ToString(mngAssess.Value) == "N2") || (Convert.ToString(mngAssess.Value) == "N2V"))
                            {
                                liRecoDissN1.Visible = true;
                                liRecoDissN2.Visible = true;
                                txtRecoDissN1.Visible = true;
                                RecoDissN1.Visible = true;
                                txtRecoDissN2.Visible = true;
                                RecoDissN2.Visible = true;
                              //  Span9.Visible = true;
                                txtRecoDissAD1.Visible = false;
                                RecoDissAD1.Visible = false;
                              //  Span10.Visible = false;
                                txtRecoDissN2.Enabled = true;
                            }
                            if ((Convert.ToString(mngAssess.Value) == "AD1") || (Convert.ToString(mngAssess.Value) == "AD1V") || (Convert.ToString(mngAssess.Value) == "AD1VP"))
                            {
                                liRecoDissN1.Visible = true;
                                liRecoDissN2.Visible = true;
                                liRecoDissAD1.Visible = true;
                                txtRecoDissN1.Visible = true;
                                RecoDissN1.Visible = true;
                                RecoDissN2.Visible = true;
                                txtRecoDissN2.Visible = true;
                             //   Span9.Visible = true;
                                RecoDissAD1.Visible = true;
                                txtRecoDissAD1.Visible = true;
                                txtRecoDissAD1.Enabled = true;
                            //    Span10.Visible = true;
                            }
                            if ((Convert.ToString(hdnAssessType.Value) == "RevWerEd") || (Convert.ToString(hdnAssessType.Value) == "RevWerHd"))
                            {
                                txtRecoDissN1.Enabled = false;
                                txtRecoDissN2.Enabled = false;
                                txtRecoDissAD1.Enabled = false;
                            }

                        }
                        else
                        {
                            liRecoDissN1.Visible = false;
                            txtRecoDissN1.Visible = false;
                            RecoDissN1.Visible = false;
                            liRecoDissN2.Visible = false;
                            RecoDissN2.Visible = false;
                            txtRecoDissN2.Visible = false;
                          //  Span9.Visible = false;
                            liRecoDissAD1.Visible = false;
                            RecoDissAD1.Visible = false;
                            txtRecoDissAD1.Visible = false;
                            txtRecoDissAD1.Enabled = false;
                           // Span10.Visible = false;
                        }
                    }
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
    protected void btnTra_Details_Click(object sender, EventArgs e)
    {
       
      
        //getAddedAssessmentDates(Convert.ToString(Session["Empcode"]));       
        getKRAPointsSum();

        Double ptAlloted = 0;
        Double ptAchieved = 0;
        Double ptRevAlloted = 0;
        Double ptRevRevieweeAchieved = 0;


        ptAlloted = Convert.ToDouble(hdnTotalPtAlloted.Value);
        ptAchieved = Convert.ToDouble(hdnTotalPtAchieved.Value);
        ptRevAlloted = Convert.ToDouble(hdnRevPointsAlloted.Value);
        ptRevRevieweeAchieved = Convert.ToDouble(hdnRevRevieweeAchieved.Value);


        if (Convert.ToString(mngAssess.Value) == "Rwee")//Self Assessment Reviewee
        {
            if (ptAlloted < 100)
            {
                if (ptAchieved < 100)
                {
                    AssigningSessions();
                    Response.Redirect("~/procs/Appraisal_TragetEvaluation.aspx?d232334=" + hdnAssessid.Value + "&54355fg=0&dhfgh=" + mngAssess.Value);
                }
                else
                {
                    lblmessage.Text = "Target Evaluation : Sum of Points achieved is already 100,Kindly edit and reduce Points achieved";
                    return;
                }

            }
            else
            {
                lblmessage.Text = "Target Evaluation : Sum of Points Allotted is already 100 ,Kindly edit and reduce Points Allotted";
                return;
            }
        }
        if (Convert.ToString(mngAssess.Value) == "PerR")//Performance Review Reviewer
        {

            if (ptRevAlloted < 100)
            {
                if (ptRevRevieweeAchieved < 100)
                {
                    AssigningSessions();
                    Response.Redirect("~/procs/Appraisal_TragetEvaluation.aspx?d232334=" + hdnAssessid.Value + "&54355fg=0&dhfgh=" + mngAssess.Value);
                }
                else
                {
                    lblmessage.Text = "Target Evaluation : Sum of Points achieved is already 100,Kindly edit and reduce Points achieved";
                    return;
                }

            }
            else
            {
                lblmessage.Text = "Target Evaluation : Sum of Points Allotted is already 100 ,Kindly edit and reduce Points Allotted";
                return;
            }
        }
     
    }    
    //protected void mobile_btnAddKRA_Click(object sender, EventArgs e)
    //{
    //    if (Convert.ToString(lstKRATemplate.SelectedValue) != "" && Convert.ToString(hdnYesNo.Value) == "Yes")
    //    {
    //         hdnsptype.Value = "sp_Delete_Assess_KRA_AllByAssessID";
    //         spm.DeleteAssessKRADetails(Convert.ToDecimal(hdnAssessid.Value), Convert.ToDecimal("0"), hdnsptype.Value); 
    //         InsertKRATemplateDetailsToTrans(Convert.ToDecimal(hdnAssessid.Value), Convert.ToDecimal(lstKRATemplate.SelectedValue));
    //         getTargetEvaluationDeails(Convert.ToDecimal(hdnAssessid.Value));
    //         hdnsptype.Value = "";
    //    }
    //    else
    //    {
    //        return;
    //    }
       
    //}
    protected void lnkEdit_Click(object sender, EventArgs e)
    {       
            AssigningSessions();
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            hdnAssessid.Value = Convert.ToString(dgTargetEvaluation.DataKeys[row.RowIndex].Values[0]).Trim();
            hdnAssessKRAdtlid.Value = Convert.ToString(dgTargetEvaluation.DataKeys[row.RowIndex].Values[1]).Trim();

            Response.Redirect("~/procs/Appraisal_TragetEvaluation.aspx?6476rtrtfg=" + hdnAssessid.Value + "&6476v4uuyu=" + hdnAssessKRAdtlid.Value+"&dhfgh=" + mngAssess.Value);
       
       
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }
        if (Convert.ToString(mngAssess.Value) == "Rwee")//Self Assessment Reviewee
        {
            #region Self Assessment Reviewee Submit
                    getKRAPointsSum();
                    Double ptAlloted = 0;
                    Double ptAchieved = 0;

                    ptAlloted = Convert.ToDouble(hdnTotalPtAlloted.Value);
                    ptAchieved = Convert.ToDouble(hdnTotalPtAchieved.Value);

                    if (ptAlloted < 100)
                    {
                        lblmessage.Text = "The total of the Points Allotted should equate to 100. Please reallocate the Points.";
                        return;
                    }
                    //if (ptAchieved < 40)
                    //{
                    //    lblmessage.Text = "Target Evaluation : Sum of Points achieved is less ,Kindly Add Points achieved";
                    //    return;
                    //}
                    int compecount= dgCompetancyEvaluation.Rows.Count;

                    if (compecount < 1)
                    {
                        lblmessage.Text = "Kindly Indicate the Ratings for the Attributes";
                        return;
                    }

                    getCompetancyCount();
                    Double CometancyCount = 0;
                    Double RevieweeCometancyCount = 0;

                    CometancyCount = Convert.ToDouble(hdnCompetancyCount.Value);
                    RevieweeCometancyCount = Convert.ToDouble(hdnRevieweeCompetancyCount.Value);

                    if (RevieweeCometancyCount == 0)
                    {
                        lblmessage.Text = "Kindly Indicate the Ratings for the Attributes";
                        return;
                    }
                    if (CometancyCount > RevieweeCometancyCount)
                    {
                        lblmessage.Text = "Kindly Indicate the Ratings for all the Attributes";
                        return;
                    }

                    //if (Convert.ToString(txtOverallComment.Text).Trim() == "")
                    //{
                    //    lblmessage.Text = "Please enter Overall Comments";
                    //    return;
                    //}
                    string[] strdate;
                    string strfromDate = "";
                       
                    string strToDate = "";
                    if (Convert.ToString(txtFromdate.Text).Trim() != "")
                    {
                        strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                        strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                    }
                    if (Convert.ToString(txtToDate.Text).Trim() != "")
                    {
                        strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                        strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                    }
                    if ((Convert.ToString(txtFromdate.Text).Trim() != "") && (Convert.ToString(txtToDate.Text).Trim() != ""))
                    {
                        hdnsptype.Value = "sp_Update_Assess_Trans_Main";
                        spm.UpdateAssessMainDetails(Convert.ToDecimal(hdnAssessid.Value), spm.Encrypt(Convert.ToString(txtOverallComment.Text)), Convert.ToDecimal(hdnAssessTypeId.Value), Convert.ToDecimal(hdnReviewerTypeId.Value), Convert.ToString(hdnAssessStatus.Value), Convert.ToString(hdnNextReviewer.Value), Convert.ToDecimal(hdnNextAssessTypeId.Value), Convert.ToDecimal(hdnNextReviewerTypeId.Value), Convert.ToString(hdnNextAssessstatus.Value), hdnsptype.Value);
                        String strAppraisalURL = "";
                        strAppraisalURL = Convert.ToString(ConfigurationManager.AppSettings["PerformanceReviewerInbox"]).Trim();
                        strAppraisalURL = strAppraisalURL + "?retgh345frtdgh=" + Convert.ToString(hdnAssessid.Value) + "&ytytr7676hghgbv=PerR";

                        spm.send_mail_appraisal(Convert.ToString(hdnEmpNameCurrentSession.Value), Convert.ToString(hdnToEmailID.Value), " has submitted his Performance Appraisal – Self Assessment for your review. ", "Please provide your appraisal assessment.", Convert.ToString(txtFromdate.Text.ToString()), Convert.ToString(txtToDate.Text.ToString()), Convert.ToString(hdnCCEmailID.Value), "", strAppraisalURL, Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(txtEmpName.Text).Trim(), Convert.ToString(txtposition.Text).Trim(), Convert.ToString(txtdept.Text).Trim(),Convert.ToString(txtdesig.Text).Trim(),Convert.ToString(hdnlocationname.Value), "");
                        lblmessage.Visible = true;
                        lblmessage.Text = "Appraisal Self Assessment Submitted Successfully";
                        Response.Redirect("~/procs/ManageSelfAssessment.aspx");
                    }
                    else
                    {
                        return;
                    }
           
            #endregion
        }
       
        if (Convert.ToString(mngAssess.Value) == "PerR") //Performance Review Reviewer
        {
            #region Performance Review Reviewer Submit

            getKRAPointsSum();
            Double ptReAlloted = 0;
            Double ptRevPtAlloted = 0;
            Double ptRevPtRevieweeAchieved = 0;
            ptReAlloted = Convert.ToDouble(hdnTargetTotalPtAchieved.Value);
            ptRevPtAlloted = Convert.ToDouble(hdnRevPointsAlloted.Value);
            ptRevPtRevieweeAchieved = Convert.ToDouble(hdnRevRevieweeAchieved.Value);

            if (ptRevPtAlloted < 100)
            {
                lblmessage.Text = "The total of the Points Allotted should equate to 100. Please reallocate the Points.";
                return;
            }
            
            //if (ptReAlloted == 0)
            //{
            //    lblmessage.Text = "Section 1.A: Target Evaluation Sheet - Targets Achieved (Reviewer): Sum of Points Achieved is Zero";
            //    return;
            //}

            if (ptReAlloted > 100)
            {
                lblmessage.Text = "The Points Achieved cannot be greater than the Points Allotted. Please Rectify.";
                return;
            }

            getReviewerKRADetailCount();
            Double revieweeKRADetailCount = 0;
            Double ReviewerKRADetailCount = 0;
            Double ReviewerKRADetailDescCount = 0;

            revieweeKRADetailCount = Convert.ToDouble(hdnrevieweeKRADetailCount.Value);
            ReviewerKRADetailCount= Convert.ToDouble(hdnReviewerKRADetailCount.Value );
            ReviewerKRADetailDescCount = Convert.ToDouble(hdnReviewerKRADetailDescCount.Value );

            if (ReviewerKRADetailCount != ReviewerKRADetailDescCount)
            {
                lblmessage.Text = "Please enter the Points for the Acheived Target.";
                return;
            }
            
            if (revieweeKRADetailCount > ReviewerKRADetailCount)
            {
                lblmessage.Text = "Please enter the description for the Acheived Target.";
                return;
            }

            getCompetancyCount();
            Double RevieweeCometancyCount = 0;
            Double ReviewerCometancyCount = 0;

            RevieweeCometancyCount = Convert.ToDouble(hdnRevieweeCompetancyCount.Value);
            ReviewerCometancyCount = Convert.ToDouble(hdnReviewerCompetancyCount.Value);
            if (ReviewerCometancyCount == 0)
            {
                lblmessage.Text = "Kindly Indicate the Ratings for the Attributes";
                return;
            }
            if (RevieweeCometancyCount > ReviewerCometancyCount)
            {
                lblmessage.Text = "Kindly Indicate the Ratings for all the Attributes";
                return;
            }

            if (Convert.ToString(txtPRD1.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter the description for 'Tasks accomplished successfully'";
                //txtPRD1.Focus();
                return;
            }
            if (Convert.ToString(txtPRD2.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter the description for 'Tasks  that could have been handled in a better way'";
                //txtPRD2.Focus();
                return;
            }
            if (Convert.ToString(txtPRD3.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter the description for 'Efforts taken to demonstrate the brand behaviour'";
                //txtPRD3.Focus();
                return;
            }
            if (Convert.ToString(txtPRD4.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter the description for 'Any concern that is hampering the performance of the Reviewee'";
                //txtPRD4.Focus();
                return;
            }
            //if ( (Convert.ToString(txtIDP1.Text).Trim() == "") && (Convert.ToString(txtIDP2.Text).Trim() == "") && (Convert.ToString(txtIDP3.Text).Trim() == ""))
            //{
            //    lblmessage.Text = "Please enter Strengths";
            //    //txtIDP1.Focus();
            //    return;
            //}
            
            int strenCnt = gvStrength.Rows.Count;
            if (strenCnt < 1)
            {
                lblmessage.Text = "Please enter the strength of the Reviewee";
                return;
            }

            if (Convert.ToString(txtDissHeldOn.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter the discussion date.";
                //txtDissHeldOn.Focus();
                return;
            }            

            hdnsptype.Value = "sp_Update_Assess_Trans_MainPerformanceReviewReviewer";
            spm.UpdateAssessMainDetails(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(""), Convert.ToDecimal(hdnAssessTypeId.Value), Convert.ToDecimal(hdnReviewerTypeId.Value), Convert.ToString(hdnAssessStatus.Value), Convert.ToString(hdnNextReviewer.Value), Convert.ToDecimal(hdnNextAssessTypeId.Value), Convert.ToDecimal(hdnNextReviewerTypeId.Value), Convert.ToString(hdnNextAssessstatus.Value), hdnsptype.Value);

            String strAppraisalURL = "";
            strAppraisalURL = Convert.ToString(ConfigurationManager.AppSettings["RevieweeInbox"]).Trim();
            strAppraisalURL = strAppraisalURL + "?1232ghghg="+ Convert.ToString(hdnAssessid.Value).Trim() +"&dfjk78hjdf=ReP";
            //spm.send_mail_appraisal(txtEmpName.Text, Convert.ToString(hdnToEmailID.Value), "Performance Review","", Convert.ToString(txtFromdate.Text.ToString()), Convert.ToString(txtToDate.Text.ToString()), Convert.ToString(hdnCCEmailID.Value), "", strAppraisalURL);
            spm.send_mail_appraisal(Convert.ToString(hdnEmpNameCurrentSession.Value), Convert.ToString(hdnToEmailID.Value), ", has reviewed  your self assessment", "Please provide your appraisal assessment.", Convert.ToString(txtFromdate.Text.ToString()), Convert.ToString(txtToDate.Text.ToString()), Convert.ToString(hdnCCEmailID.Value), "", strAppraisalURL, Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(txtEmpName.Text).Trim(), Convert.ToString(txtposition.Text).Trim(), Convert.ToString(txtdept.Text).Trim(), Convert.ToString(txtdesig.Text).Trim(), Convert.ToString(hdnlocationname.Value), "");
                       
            lblmessage.Visible = true;
            lblmessage.Text = "Performance Review Submitted Successfully";            
            Response.Redirect("~/procs/PerformanceReviewList.aspx");
            #endregion
        }

        if (Convert.ToString(mngAssess.Value) == "ReP") //Performance Review Reviewee
        {
            #region Performance Review Reviewee Submit

            if (chkRevieweeDisAgree.Checked)
            {
                if (Convert.ToString(txtRevieweeDis.Text).Trim() == "")
                {
                    lblmessage.Text = "Please enter your comments regarding the disagreement";                   
                    return;
                }
            }
            string encry = null;
            if (txtRevieweeDis.Text.Trim() != "")
            {                
                encry = spm.Encrypt(txtRevieweeDis.Text.Trim());
            }
            hdnsptype.Value = "sp_Update_Assess_Trans_MainPerformanceReviewReviewee";
            spm.UpdateAssessMainDetails(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(encry), Convert.ToDecimal(hdnAssessTypeId.Value), Convert.ToDecimal(hdnReviewerTypeId.Value), Convert.ToString(hdnAssessStatus.Value), Convert.ToString(hdnNextReviewer.Value), Convert.ToDecimal(hdnNextAssessTypeId.Value), Convert.ToDecimal(hdnNextReviewerTypeId.Value), Convert.ToString(hdnNextAssessstatus.Value), hdnsptype.Value);
            
            String strAppraisalURL = "";
            strAppraisalURL = Convert.ToString(ConfigurationManager.AppSettings["RecommendationInbox"]).Trim();
            strAppraisalURL = strAppraisalURL + "?1232ghghg=" + Convert.ToString(hdnAssessid.Value).Trim() + "&dfjk78hjdf=N1&mfngndfhfgnj=reco";

            spm.send_mail_appraisal(Convert.ToString(hdnEmpNameCurrentSession.Value), Convert.ToString(hdnToEmailID.Value), " submitted the Performance Appraisal Form for your recommendation.", "Please provide your appraisal assessment.", Convert.ToString(txtFromdate.Text.ToString()), Convert.ToString(txtToDate.Text.ToString()), Convert.ToString(hdnCCEmailID.Value), "", strAppraisalURL, Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(txtEmpName.Text).Trim(), Convert.ToString(txtposition.Text).Trim(), Convert.ToString(txtdept.Text).Trim(), Convert.ToString(txtdesig.Text).Trim(), Convert.ToString(hdnlocationname.Value), "");
           
            lblmessage.Visible = true;
            lblmessage.Text = "Performance Review Submitted Successfully";
            Response.Redirect("~/procs/ManageSelfAssessment.aspx");
            #endregion
        }
        if (Convert.ToString(hdnAssessType.Value) == "reco") //Recommendation Reviewer
        {
            #region Recommendation Reviewer Submit
           
            if (Convert.ToString(mngAssess.Value) == "N1")
            {
                if(Convert.ToString(txtRevieweeDis.Text)!="")
                {
                    if (Convert.ToString(txtRecoDissN1.Text) == "")
                    {
                        lblmessage.Text = "Please enter your comments regarding the disagreement";
                        return;
                    }
                }

            }
            if (Convert.ToString(mngAssess.Value) == "N2")
            {
                if (Convert.ToString(txtRevieweeDis.Text) != "")
                {
                    //if (Convert.ToString(txtRecoDissN2.Text) == "")
                    //{
                    //    lblmessage.Text = "Please enter your comments regarding the disagreement";
                    //    return;
                    //}
                }

            }
            if (Convert.ToString(mngAssess.Value) == "AD1")
            {
                if (Convert.ToString(txtRevieweeDis.Text) != "")
                {
                    //if (Convert.ToString(txtRecoDissAD1.Text) == "")
                    //{
                    //    lblmessage.Text = "Please enter your comments regarding the disagreement";
                    //    return;
                    //}
                }

            }
            
            
            double recoCount = Convert.ToDouble(hdngetrecoCount.Value);
            string statusreco = null;
            string nextstatusreco = null;
            if (recoCount == 0)
            {
                lblmessage.Text = "Please enter your Recommendations";
                return;
            }
            if (Convert.ToString(mngAssess.Value) == "N1")
            { statusreco = "7";
            nextstatusreco = "N2";
               
            } //7=N1Submitted
            if (Convert.ToString(mngAssess.Value) == "N2")
            { statusreco = "8";
              nextstatusreco = "AD1";
            
            
            } //8=N2Submitted
            if (Convert.ToString(mngAssess.Value) == "AD1")
            { statusreco = "9";

            
            
            }// 9=AD1Submitted
            if (statusreco != "")
            {
                hdnsptype.Value = "sp_Update_Assess_Trans_MainRecomm";
                spm.UpdateAssessMainDetails(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(statusreco), Convert.ToDecimal(hdnAssessTypeId.Value), Convert.ToDecimal(hdnReviewerTypeId.Value), Convert.ToString(hdnAssessStatus.Value), Convert.ToString(hdnNextReviewer.Value), Convert.ToDecimal(hdnNextAssessTypeId.Value), Convert.ToDecimal(hdnNextReviewerTypeId.Value), Convert.ToString(hdnNextAssessstatus.Value), hdnsptype.Value);

                if (Convert.ToString(hdnCCEmailID.Value) != "")
                { 
                String strAppraisalURL = "";
                strAppraisalURL = Convert.ToString(ConfigurationManager.AppSettings["RecommendationInbox"]).Trim();
                //strAppraisalURL = strAppraisalURL + "?1232ghghg=" + Convert.ToString(hdnAssessid.Value).Trim() + "&dfjk78hjdf=" + Convert.ToString(mngAssess.Value).Trim() + "&mfngndfhfgnj=reco";
                strAppraisalURL = strAppraisalURL + "?1232ghghg=" + Convert.ToString(hdnAssessid.Value).Trim() + "&dfjk78hjdf=" + nextstatusreco.ToString() + "&mfngndfhfgnj=reco";
                //spm.send_mail_appraisal(Convert.ToString(hdnEmpNameCurrentSession.Value), Convert.ToString(hdnToEmailID.Value), " has submitted Performance Appraisal for Recommendation ", "Kindly Review & finish Recommendations for the same.", Convert.ToString(txtFromdate.Text.ToString()), Convert.ToString(txtToDate.Text.ToString()), Convert.ToString(hdnCCEmailID.Value), "", strAppraisalURL);
                string strappoverDtls = "";
                strappoverDtls = get_Approver_names_forEmail();
                spm.send_mail_appraisal(txtEmpName.Text, Convert.ToString(hdnToEmailID.Value), " submitted the Performance Appraisal Form for your recommendation. ", "Please provide your appraisal assessment.", Convert.ToString(txtFromdate.Text.ToString()), Convert.ToString(txtToDate.Text.ToString()), Convert.ToString(hdnCCEmailID.Value), "", strAppraisalURL, Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(txtEmpName.Text).Trim(), Convert.ToString(txtposition.Text).Trim(), Convert.ToString(txtdept.Text).Trim(), Convert.ToString(txtdesig.Text).Trim(), Convert.ToString(hdnlocationname.Value), strappoverDtls);
                }
                lblmessage.Visible = true;
                lblmessage.Text = "Recommendation Submitted Successfully";
                Response.Redirect("~/procs/ManageRecommendationList.aspx");
            }
            #endregion
        }
        
        
        
      

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(mngAssess.Value) == "PerR") //Performance Review Reviewer
        {
            Response.Redirect("~/procs/PerformanceReviewList.aspx");
        }
        if ((Convert.ToString(mngAssess.Value) == "Rwee") || (Convert.ToString(mngAssess.Value) == "ReV") || (Convert.ToString(mngAssess.Value) == "RePV")|| (Convert.ToString(mngAssess.Value) == "ReP") )// Reviewee
        {
            Response.Redirect("~/procs/ManageSelfAssessment.aspx");
        }
        if ((Convert.ToString(mngAssess.Value) == "N1") || (Convert.ToString(mngAssess.Value) == "N2") || (Convert.ToString(mngAssess.Value) == "AD1"))// Recommendation
        {
            Response.Redirect("~/procs/ManageRecommendationList.aspx");
        }
        if (Convert.ToString(Request.QueryString[2]) == "RevWerEd")
        {
            Response.Redirect("~/procs/Appraisal_TeamCalendar.aspx");
        }
        if (Convert.ToString(Request.QueryString[2]) == "RevWerHd")
        {
            Response.Redirect("~/procs/Appraisal_HODCalendar.aspx");
            
        }
    }
    protected void btnTra_competancy_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter From date";
            return;
        }
        if (Convert.ToString(txtToDate.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter To date";
            return;
        }
      AssigningSessions();
      if (Convert.ToString(mngAssess.Value) == "PerR")
      {
          Response.Redirect("~/procs/Appraisal_Competancy.aspx?76gh76tyg=" + hdnAssessid.Value + "&ghgjhtruy=PerR");
      }
      else
      {
          Response.Redirect("~/procs/Appraisal_Competancy.aspx?76gh76tyg=" + hdnAssessid.Value + "&ghgjhtruy=Rwee");
      }
    }
    protected void btnTra_DevelopmentPlan_Click(object sender, EventArgs e)
    {
        AssigningSessions();
        Response.Redirect("~/procs/Appraisal_DevelopmentPlan.aspx?7677hgdre=" + hdnAssessid.Value + "&gfghfhyh=0&dhfghjgh=" + mngAssess.Value);

    }
    protected void mobile_cancel_Click(object sender, EventArgs e)
    {

    }
   
    //protected void lstKRATemplate_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtKRATemplate.Text = lstKRATemplate.SelectedItem.Text;
    //    PopupControlExtender2.Commit(lstKRATemplate.SelectedItem.Text);
    //    hdnKRAtempid.Value = "";
    //    hdnKRAtempid.Value = lstKRATemplate.SelectedValue;
        
    //}
    protected void lnkEditDevPlan_Click(object sender, EventArgs e)
    {

        AssigningSessions();
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;

        hdnAssessid.Value = Convert.ToString(gvDevPlan.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnDevplandtlid.Value = Convert.ToString(gvDevPlan.DataKeys[row.RowIndex].Values[1]).Trim();

        Response.Redirect("~/procs/Appraisal_DevelopmentPlan.aspx?ghyqpl=" + hdnAssessid.Value + "&vgf76hgh87j=" + hdnDevplandtlid.Value + "&dhfgfjfhjgh=" + mngAssess.Value);
    }
    protected void txtOverallComment_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txtOverallComment.Text.Trim()) != "")
        {
            hdnsptype.Value = "sp_Update_Reviewee_CommentAssess_Trans_Main";
            spm.UpdateRevieweeCommentAssessMainDetails(Convert.ToDecimal(hdnAssessid.Value), spm.Encrypt(Convert.ToString(txtOverallComment.Text.Trim())), hdnsptype.Value);
        }
           
    }
    protected void btnlnkPRD_Click(object sender, EventArgs e)
    {
        if (!divPRD.Visible)
        {
           // divPRD.Visible = true; //15.04.2019
           // txtPRD1.Focus();
        }
        else
        {
           // divPRD.Visible = false; 
        }
    }
    protected void btnlnkIDP_Click(object sender, EventArgs e)
    {
        if (!divIDP.Visible)
        {
            divIDP.Visible = true;
            txtIDP1.Focus();
        }
        else
        {
            divIDP.Visible = false;
        }
    }
    protected void lnkEmpDtl_Click(object sender, EventArgs e)
    {
        if (!Span5.Visible)
        {
            Span5.Visible = true;           
        }
        else
        {
            Span5.Visible = false;
        }
    }
    protected void txtPRD1_TextChanged(object sender, EventArgs e)
    {
        if (txtPRD1.Text.Trim() != "")
        {
            string encry = null;
            encry = spm.Encrypt(txtPRD1.Text.Trim());
            hdnsptype.Value = "sp_Update_Tra_Assess_Review_Detail";
            spm.UpdateTra_Assess_Review_Detail(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(encry), hdnsptype.Value, "PRD_1");
            //txtPRD1.Focus();
        }

    }
    protected void txtPRD2_TextChanged(object sender, EventArgs e)
    {
        if (txtPRD2.Text.Trim() != "")
        {
            string encry = null;
            encry = spm.Encrypt(txtPRD2.Text.Trim());
            hdnsptype.Value = "sp_Update_Tra_Assess_Review_Detail";
            spm.UpdateTra_Assess_Review_Detail(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(encry), hdnsptype.Value, "PRD_2");
            //txtPRD2.Focus();
        }

    }
    protected void txtPRD3_TextChanged(object sender, EventArgs e)
    {
        if (txtPRD3.Text.Trim() != "")
        {
            string encry = null;
            encry = spm.Encrypt(txtPRD3.Text.Trim());
            hdnsptype.Value = "sp_Update_Tra_Assess_Review_Detail";
            spm.UpdateTra_Assess_Review_Detail(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(encry), hdnsptype.Value, "PRD_3");
            //txtPRD3.Focus();
        }

    }
    protected void txtPRD4_TextChanged(object sender, EventArgs e)
    {
        if (txtPRD4.Text.Trim() != "")
        {
            string encry = null;
            encry = spm.Encrypt(txtPRD4.Text.Trim());
            hdnsptype.Value = "sp_Update_Tra_Assess_Review_Detail";
            spm.UpdateTra_Assess_Review_Detail(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(encry), hdnsptype.Value, "PRD_4");
           // txtPRD4.Focus();
        }

    }
    protected void txtIDP1_TextChanged(object sender, EventArgs e)
    {
        if (txtIDP1.Text.Trim() != "")
        {
            string encry = null;
            encry = spm.Encrypt(txtIDP1.Text.Trim());
            hdnsptype.Value = "sp_Update_Tra_Assess_Review_Detail";
            spm.UpdateTra_Assess_Review_Detail(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(encry), hdnsptype.Value, "IDP_1");
            gvStrengthDetails(Convert.ToDecimal(hdnAssessid.Value));
            txtIDP1.Text = "";
            gvStrength.Focus();
        }
        else
        {
            lblmessage.Text = "Enter strength of reviewee";
            return;
        }

    }
    //protected void txtIDP2_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtIDP2.Text.Trim() != "")
    //    {
    //        string encry = null;
    //        encry = spm.Encrypt(txtIDP2.Text.Trim());
    //        hdnsptype.Value = "sp_Update_Tra_Assess_Review_Detail";
    //        spm.UpdateTra_Assess_Review_Detail(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(encry), hdnsptype.Value, "IDP_2");
    //        // txtPRD4.Focus();
    //    }

    //}
    //protected void txtIDP3_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtIDP3.Text.Trim() != "")
    //    {
    //        string encry = null;
    //        encry = spm.Encrypt(txtIDP3.Text.Trim());
    //        hdnsptype.Value = "sp_Update_Tra_Assess_Review_Detail";
    //        spm.UpdateTra_Assess_Review_Detail(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(encry), hdnsptype.Value, "IDP_3");
    //        // txtPRD4.Focus();
    //    }

    //}
    protected void txtRevieweeDis_TextChanged(object sender, EventArgs e)
    {
        if (txtRevieweeDis.Text.Trim() != "")
        {
            string encry = null;
            encry = spm.Encrypt(txtRevieweeDis.Text.Trim());
            hdnsptype.Value = "sp_Update_Tra_Assess_Review_Detail";
            spm.UpdateTra_Assess_Review_Detail(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(encry), hdnsptype.Value, "IDP_3");
            // txtPRD4.Focus();
        }
    }
    protected void txtRecoDissN1_TextChanged(object sender, EventArgs e)
    {
        if (txtRecoDissN1.Text.Trim() != "")
        {
            string encry = null;
            encry = spm.Encrypt(txtRecoDissN1.Text.Trim());
            hdnsptype.Value = "sp_Update_Tra_Assess_Review_Detail";
            spm.UpdateTra_Assess_Review_Detail(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(encry), hdnsptype.Value, "Disagree_CommentsN1");
        }
    }
    protected void txtRecoDissN2_TextChanged(object sender, EventArgs e)
    {
        if (txtRecoDissN2.Text.Trim() != "")
        {
            string encry = null;
            encry = spm.Encrypt(txtRecoDissN2.Text.Trim());
            hdnsptype.Value = "sp_Update_Tra_Assess_Review_Detail";
            spm.UpdateTra_Assess_Review_Detail(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(encry), hdnsptype.Value, "Disagree_CommentsN2");
        }
    }
    protected void txtRecoDissAD1_TextChanged(object sender, EventArgs e)
    {
        if (txtRecoDissAD1.Text.Trim() != "")
        {
            string encry = null;
            encry = spm.Encrypt(txtRecoDissAD1.Text.Trim());
            hdnsptype.Value = "sp_Update_Tra_Assess_Review_Detail";
            spm.UpdateTra_Assess_Review_Detail(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(encry), hdnsptype.Value, "Disagree_CommentsAD1");
        }
    }
    protected void txtDissHeldOn_TextChanged(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        string[] strdate;
        string strfromDate = "";
        //string strToDate = DateTime.Now.ToString("dd/MM/yyyy");
        string strToDate = hdnselfassessmentReleaseon.Value;
        string message = "";
        if (Convert.ToString(txtDissHeldOn.Text).Trim() == "")
        {
            return;
        }
        
        #region date formatting

        if (Convert.ToString(txtDissHeldOn.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtDissHeldOn.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(strToDate).Trim() != "")
        {
            strdate = Convert.ToString(strToDate).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        #endregion
        DataSet dtAppetails = new DataSet();
        if (Convert.ToString(txtDissHeldOn.Text).Trim() != "" && Convert.ToString(strToDate).Trim() != "")
        {

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "dateValidation";

            spars[1] = new SqlParameter("@from_date", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(strToDate);
            
            spars[2] = new SqlParameter("@todate", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(strfromDate);

            dtAppetails = spm.ApprgetDatasetList(spars, "[SP_GETALL_Appraisal_DETAILS]");            

            if (dtAppetails.Tables[0].Rows.Count > 0)
            {
                message = Convert.ToString(dtAppetails.Tables[0].Rows[0]["Message"]);
                if (message != "")
                {
                       lblmessage.Text = message.ToString();
                }
                else
                 { 
                        hdnsptype.Value = "sp_Update_Tra_Assess_Review_Detail";
                        spm.UpdateTra_Assess_Review_Detail(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(strfromDate), hdnsptype.Value, "Discussion_Date");
                       // txtDissHeldOn.Focus();
                }
            }           
        }
    }

    //protected void btnlnkRecommendation_Click(object sender, EventArgs e)
    //{
    //   // AssigningSessions();
    //   // Response.Redirect("~/procs/Appraisal_Recommendation.aspx?7g6gh76tyg=" + hdnAssessid.Value + "&ghgjhtruy=" + mngAssess.Value + "&ujkgdcvz=" + hdnAssessType.Value + "&jgfjgj=N&jhjdfh=a");
       
        
    //}
    protected void lnkRecomm_Click(object sender, EventArgs e)
    {
        AssigningSessions();
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;

        hdnAssessid.Value = Convert.ToString(gvRecomm.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnReviewerTypeidRECO.Value = Convert.ToString(gvRecomm.DataKeys[row.RowIndex].Values[1]).Trim();
        hdnApprEmpCode.Value = Convert.ToString(gvRecomm.DataKeys[row.RowIndex].Values[2]).Trim();

        if (Convert.ToString(hdnAssessType.Value) == "reco")
        {
            if (Convert.ToString(Session["Empcode"]).Trim() != Convert.ToString(hdnApprEmpCode.Value))
            {
                Response.Redirect("~/procs/Appraisal_Recommendation.aspx?7g6gh76tyg=" + hdnAssessid.Value + "&ghgjhtruy=" + mngAssess.Value + "&ujkgdcvz=" + hdnAssessType.Value + "&jgfjgj=V&jhjdfh=" +hdnReviewerTypeidRECO.Value);
            }
            else
            {
                if (btn.Text.Trim() == "Recommend")
               {
                   Response.Redirect("~/procs/Appraisal_Recommendation.aspx?7g6gh76tyg=" + hdnAssessid.Value + "&ghgjhtruy=" + mngAssess.Value + "&ujkgdcvz=" + hdnAssessType.Value + "&jgfjgj=N&jhjdfh=a");
               }
               else
               {
                   Response.Redirect("~/procs/Appraisal_Recommendation.aspx?7g6gh76tyg=" + hdnAssessid.Value + "&ghgjhtruy=" + mngAssess.Value + "&ujkgdcvz=" + hdnAssessType.Value + "&jgfjgj=E&jhjdfh=a");
               }
                
            }

        }
        if ((Convert.ToString(hdnAssessType.Value) == "RevWerEd") || (Convert.ToString(hdnAssessType.Value) == "RevWerHd"))
        {
            Response.Redirect("~/procs/Appraisal_Recommendation.aspx?7g6gh76tyg=" + hdnAssessid.Value + "&ghgjhtruy=" + mngAssess.Value + "&ujkgdcvz=" + hdnAssessType.Value + "&jgfjgj=V&jhjdfh=" + hdnReviewerTypeidRECO.Value);
        }

    }

    protected void gvRecomm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
         if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkRecomm = (LinkButton)e.Row.FindControl("lnkRecomm");
            string lsDataKeyValue = gvRecomm.DataKeys[e.Row.RowIndex].Values[0].ToString();
            string lsDataKeyValue1 = gvRecomm.DataKeys[e.Row.RowIndex].Values[1].ToString();
            string lsDataKeyValue2 = gvRecomm.DataKeys[e.Row.RowIndex].Values[2].ToString();
            string lsDataKeyValue3 = gvRecomm.DataKeys[e.Row.RowIndex].Values[3].ToString();
            string lsDataKeyValue4 = gvRecomm.DataKeys[e.Row.RowIndex].Values[4].ToString();
            //if (Convert.ToString(hdnReviewerTypeId.Value).Trim() == "")
            //    hdnReviewerTypeId.Value = "0";

            if ((Convert.ToString(Request.QueryString[2]).Trim() == "RevWerEd") || (Convert.ToString(Request.QueryString[2]).Trim() == "RevWerHd"))
             {
                 if ((lsDataKeyValue3.ToString() == null) || (lsDataKeyValue3.ToString() == ""))
                 {
                     lnkRecomm.Text = "";
                 }
                 else
                 {
                     lnkRecomm.Text = "View";
                 }   
                 
                 
             }
             else
             {
                 if (lsDataKeyValue2 == Convert.ToString(Session["Empcode"]))
                 {
                     if ((lsDataKeyValue3.ToString() == null) || (lsDataKeyValue3.ToString() == ""))
                     {
                         lnkRecomm.Text = "Recommend";
                     }
                     else
                     {
                         lnkRecomm.Text = "Edit";
                     }
                 }
                 else
                 {
                     if (Convert.ToInt16(hdnReviewerTypeId.Value) > Convert.ToInt16(lsDataKeyValue4.ToString()))
                     {
                         lnkRecomm.Text = "View";
                     }
                     else
                     {
                         lnkRecomm.Text = "";
                     }
                 }
             }
        }
           
    }
    protected void dgTargetEvaluation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType.Equals(DataControlRowType.DataRow))
        {
           foreach (TableCell cell in e.Row.Cells)
           {
              for (int i = 0; i < dgTargetEvaluation.Columns.Count; i++)
              {
                  if (e.Row.Cells[1].Text == "0")
                    {
                    // e.Row.Cells[1].Text = e.Row.Cells[1].Text;
                        e.Row.Cells[1].Text = "";
                        e.Row.Cells[3].Text = "";
                    }
                  
             }
           }
        }

      
        
        //if (e.Row.RowType.Equals(DataControlRowType.DataRow))
        //{
        //    foreach (GridViewRow row in dgTargetEvaluation.Rows)
        //    {
        //        for (int j = 0; j < dgTargetEvaluation.Rows.Count; j++)
        //        {
        //            for (int i = 0; i < dgTargetEvaluation.Columns.Count; i++)
        //            {
        //                String cellText = null;
        //                String header = dgTargetEvaluation.Columns[i].HeaderText;
        //                if ((header == "Description") || (header == "Reviewee Description") || (header == "Reviewer Description"))
        //                {
        //                    row.Cells[i].Text = spm.Decrypt(row.Cells[i].Text);

        //                    //cellText = "";
        //                }
        //                else
        //                {
        //                    cellText = row.Cells[i].Text;
        //                }
        //            }
        //        }
        //    }
        // }
        
        //int theCellNumberWhatINeed = -1;
        //for (int cellNumber = 0; cellNumber < dgTargetEvaluation.Rows[index].Cells.Count; cellNumber++)
        //{
        //    foreach (Control ctrl in dgTargetEvaluation.Rows[index].Cells[cellNumber].Controls)
        //    {
        //        if (ctrl.ID == "aCheckBox") // or compare by clientid... etc
        //        {
        //            theCellNumberWhatINeed = cellNumber;
        //            break;
        //        }
        //    }
        //}
        //if (theCellNumberWhatINeed > -1)
        //{
        //    // ...
        //}
        //if (e.Row.RowType.Equals(DataControlRowType.DataRow))
        //{
        //    foreach (TableCell cell in e.Row.Cells)
        //    {
        //        string lsDataKeyValue = gvRecomm.DataKeys[e.Row.RowIndex].Values[1].ToString();
                
        //        string x, y, z = null;

        //        if (e.Row.Cells[0].Text.Length > 1)
        //        {
        //            x = spm.Decrypt(e.Row.Cells[0].Text);
        //            if (x.Length > 20) x = x.Substring(0, 20);
        //            e.Row.Cells[0].Text = x;
        //        }
        //        if (e.Row.Cells[2].Text.Length > 1)
        //        {
        //            y = spm.Decrypt(e.Row.Cells[2].Text);
        //            if (y.Length > 20) y = y.Substring(0, 20);
        //            e.Row.Cells[2].Text = y;
        //        }
        //        if (e.Row.Cells[4].Text != "&nbsp;")
        //        {
        //            z = spm.Decrypt(e.Row.Cells[4].Text);
        //            if (z.Length > 20) z = z.Substring(0, 20);
        //            e.Row.Cells[4].Text = z;
        //        }
        //    }
        //}
    }
    protected void lnkEditStrength_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;

        hdnAssessid.Value = Convert.ToString(gvStrength.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnStrengthid.Value = Convert.ToString(gvStrength.DataKeys[row.RowIndex].Values[1]).Trim();
        hdnsptype.Value = "sp_DeleteStrength";
        spm.DeleteStrength(Convert.ToDecimal(hdnAssessid.Value), Convert.ToDecimal(hdnStrengthid.Value), hdnsptype.Value);
        gvStrengthDetails(Convert.ToDecimal(hdnAssessid.Value));
        txtIDP1.Text = "";
        gvStrength.Focus();
    }
    protected void chkRevieweeAgree_CheckedChanged(object sender, EventArgs e)
    {
        if(chkRevieweeAgree.Checked)
        {
            chkRevieweeDisAgree.Checked = false;
            txtRevieweeDis.Visible = false;
            SpanRevieweeDis.Visible = false;
            Span11.Visible = false;
            chkRevieweeDisAgree.Focus();
        }
        if (!chkRevieweeAgree.Checked)
        {
            chkRevieweeDisAgree.Checked = true;
            txtRevieweeDis.Visible = true;
            SpanRevieweeDis.Visible = true;
            Span11.Visible = true;
            txtRevieweeDis.Focus();
        }
    }
    protected void chkRevieweeDisAgree_CheckedChanged(object sender, EventArgs e)
    {
        if (chkRevieweeDisAgree.Checked)
        {
           
            txtRevieweeDis.Visible = true;
            SpanRevieweeDis.Visible = true;
            Span11.Visible = true;
            chkRevieweeAgree.Checked = false;
            txtRevieweeDis.Focus();
        }
        if (!chkRevieweeDisAgree.Checked)
        {
            chkRevieweeAgree.Checked = true;
            txtRevieweeDis.Visible = false;
            SpanRevieweeDis.Visible = false;
            Span11.Visible = false;
            chkRevieweeAgree.Focus();
        }
    }
    protected void dgTargetEvaluation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "<div  style=\"font-weight:normal; \">Reviewee</div>";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
            if ((Convert.ToString(mngAssess.Value) != "ReV") && (Convert.ToString(mngAssess.Value) != "Rwee"))
            {
                HeaderCell = new TableCell();
                HeaderCell.Text = "<div  style=\"font-weight:normal; \">Reviewer </div>";
                HeaderCell.ColumnSpan = 3;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
            }

            //HeaderCell = new TableCell();
            //HeaderCell.ColumnSpan = 1;
            //HeaderGridRow.Cells.Add(HeaderCell);

            dgTargetEvaluation.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }
    #endregion

    #region PageMethods



    private void usercheck(string empno,decimal id)
    {
        string strusercheck=null;
        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "SP_UserCheck";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(empno);

        spars[2] = new SqlParameter("@assess_id", SqlDbType.Decimal);
        spars[2].Value = Convert.ToDecimal(id);

        dsTrDetails = spm.ApprgetDatasetList(spars, "SP_GETALL_Appraisal_ApproverDetails");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            strusercheck = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["UserCheck"]).Trim();         
        }
        else 
        {
            lblmessage.Visible = true;
            lblmessage.Text = "You are not authorised to requested details, please contact HR";
            Response.Redirect("~/procs/Appraisalindex.aspx");
        }
        if (Convert.ToString(strusercheck) != null)
        {
            if (Convert.ToString(strusercheck) == Convert.ToString(mngAssess.Value))
            { 
            
            }
            else
            {
                lblmessage.Visible = true;
                lblmessage.Text = "You are not authorised to requested details, please contact HR";
                Response.Redirect("~/procs/Appraisalindex.aspx");
            }
        }
    }
    private void usercheckValidDate(decimal id)
    {
        string strusercheck = null;
        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "SP_UserCheckForValidDate";      

        spars[1] = new SqlParameter("@assess_id", SqlDbType.Decimal);
        spars[1].Value = Convert.ToDecimal(id);

        dsTrDetails = spm.ApprgetDatasetList(spars, "SP_GETALL_Appraisal_ApproverDetails");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            
        }
        else
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Please contact HR - Validity period is expired";
            Response.Redirect("~/procs/Appraisalindex.aspx");
        }
       
    }   
    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {        
        DateValidation();
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {               
        DateValidation();
    }
    private void DateValidation()
    {
        lblmessage.Text = "";
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";
        string message = "";

        string[] tr_strdate;
        string tr_strfromDate = "";
        string tr_strToDate = "";

        string[] assess_strdate;
        string assess_strfromDate = "";
        string assess_strToDate = "";

        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            return;
        }
        if (Convert.ToString(txtToDate.Text).Trim() == "")
        {
            return;
        }
        #region AppRequestdate formatting

        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            tr_strdate = Convert.ToString(hdnAppYearStartDate.Value).Trim().Split('/');
            tr_strfromDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
        }
        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            tr_strdate = Convert.ToString(hdnAppYearEndDate.Value).Trim().Split('/');
            tr_strToDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
        }
        #endregion

        #region date formatting

        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        #endregion
        #region AppRequestdate formatting

        if (Convert.ToString(hdnAssessStartDt.Value).Trim() != "")
        {
            assess_strdate = Convert.ToString(hdnAssessStartDt.Value).Trim().Split('/');
            assess_strfromDate = Convert.ToString(assess_strdate[2]) + "-" + Convert.ToString(assess_strdate[1]) + "-" + Convert.ToString(assess_strdate[0]);
        }
        if (Convert.ToString(hdnAssessEndDt.Value).Trim() != "")
        {
            assess_strdate = Convert.ToString(hdnAssessEndDt.Value).Trim().Split('/');
            assess_strToDate = Convert.ToString(assess_strdate[2]) + "-" + Convert.ToString(assess_strdate[1]) + "-" + Convert.ToString(assess_strdate[0]);
        }
        #endregion
        DataSet dtAppetails = new DataSet();
        if (Convert.ToString(txtFromdate.Text).Trim() != "" && Convert.ToString(txtToDate.Text).Trim() != "")
        {
            dtAppetails = spm.Get_DateRange_ValidationResult(strfromDate, strToDate, tr_strfromDate, tr_strToDate, assess_strfromDate, assess_strToDate);

            if (dtAppetails.Tables[0].Rows.Count > 0)
            {
                message = Convert.ToString(dtAppetails.Tables[0].Rows[0]["Message"]);
            }

            if (Convert.ToString(message).Trim() != "")
            {
                lblmessage.Text = Convert.ToString(message).Trim();
                txtFromdate.Text = "";
                txtToDate.Text = "";
                return;
            }
            else
            {
                lblmessage.Text = "";
            }
        }
    }
    public void getApproverdata()
    {
        if ((Convert.ToString(mngAssess.Value) == "Rwee") || (Convert.ToString(mngAssess.Value) == "ReV") || (Convert.ToString(mngAssess.Value) == "RePV") || (Convert.ToString(mngAssess.Value) == "ReP"))// Reviewee
        {
            hdnsptype.Value = "get_ApproverDetailsReviewee";
        }
        else 
        {
            hdnsptype.Value = "get_ApproverDetails";
        }
        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.GeAppraApproverEmailID(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(hdnsptype.Value));
        //IsEnabledFalse (true);
        lstApprover.Items.Clear();
        if (dtApproverEmailIds.Rows.Count > 0)
        {
            
            lstApprover.DataSource = dtApproverEmailIds;
            lstApprover.DataTextField = "Emp_Name";
            lstApprover.DataValueField = "id";
            lstApprover.DataBind();
            lblmessage.Text = "";
        }
        else
        {
            lblmessage.Text = "You are not assigned under any approver, please contact HR";
            //setEnablesCntrls(false);
        }
    }
    public void gettranApproverStatus()
    {

        hdnsptype.Value = "get_TransApproverStatusDetails";
        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.GetTransAppraApproverStatus(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(hdnsptype.Value));
        //IsEnabledFalse (true);
        //lstApprover.Items.Clear();
        if (dtApproverEmailIds.Rows.Count > 0)
        {
            hdnAssessTypeId.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["Assess_type_id"]);
            hdnReviewerTypeId.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["reviewer_type_id"]);
            hdnEmpNameCurrentSession.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["EmpNameCurrentSession"]);
            hdnAssessStatus.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["Assess_status"]);
            hdnNextReviewer.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["nextreviewer"]);
            hdnNextAssessTypeId.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["nextAssesstypeid"]);
            hdnNextReviewerTypeId.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["nextreviewertypeid"]);
            hdnNextAssessstatus.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["nextAssessstatus"]);
            hdnToEmailID.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["ToEmilID"]);
            hdnCCEmailID.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["CCEmailID"]);
            //hdnApprEmailaddress.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["Emp_Emailaddress"]);
            //hdnApprId.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["APPR_ID"]);
            //hflapprcode.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["App_Emp_Code"]);
        }
        else
        {
            lblmessage.Text = "You are not assigned under any approver, please contact HR";
            //setEnablesCntrls(false);
        }
    }

    private void getActiveAppYear()
    {

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_get_ActiveAppYear";

        spars[1] = new SqlParameter("@location", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString("1100");

        dsTrDetails = spm.ApprgetDatasetList(spars, "[SP_GETALL_Appraisal_DETAILS]");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            txtAppYearid.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["AppraisalYear"]).Trim();
            txtPeriod.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["AppraisalPeriod"]).Trim();
            hdnAppYearStartDate.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["App_From_Date"]).Trim();
            hdnAppYearEndDate.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["App_To_Date"]).Trim();
            hdnAppAppType.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["App_Type"]).Trim();           
            hdnAppYearTypeid.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["App_Year_id"]).Trim();
        }
    }   
    private void getAddedAssessmentDates(string empcode)
    {

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_assess_FromToDate";
        
        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(empcode);

        dsTrDetails = spm.ApprgetDatasetList(spars, "[SP_GETALL_Appraisal_DETAILS]");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            hdnAssessStartDt.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Assess_FromDate"]).Trim();
            hdnAssessEndDt.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Assess_ToDate"]).Trim();
             
        }
    }
    public void AssigningSessions()
    {

        Session["Fromdate"] = txtFromdate.Text;
        Session["Todate"] = txtToDate.Text;
        Session["ReqEmpCode"] = txtEmpCode.Text;
        Session["Grade"] = hflGrade.Value;       
        //Session["AssessmentId"] = hdnAssessid.Value;

        //Response.Write(Convert.ToString(Session["Fromdate"]));
        //Response.End();

    }      
    public void GetEmployeeDetails()
    {
        try
        {
            DataTable dtEmpDetails = new DataTable();
            //dtEmpDetails = spm.GetApprEmployeeData(txtEmpCode.Text);
            dtEmpDetails = spm.GetApprEmployeeData(txtEmpCode.Text);
            if (dtEmpDetails.Rows.Count > 0)
            {
                
                txtEmpName.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
                hflEmpDesignation.Value = Convert.ToString(dtEmpDetails.Rows[0]["DesginationName"]);
                hflEmpDepartment.Value = Convert.ToString(dtEmpDetails.Rows[0]["Department_Name"]);
                hflGrade.Value = Convert.ToString(dtEmpDetails.Rows[0]["Grade"]).Trim();
                hflEmailAddress.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
                txtdept.Text = Convert.ToString(dtEmpDetails.Rows[0]["Department_Name"]);
                txtdesig.Text = Convert.ToString(dtEmpDetails.Rows[0]["DesginationName"]);
                txtgrade.Text = Convert.ToString(dtEmpDetails.Rows[0]["Grade"]);
            }
            //  getApproverdata();  
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            
            throw;
        }
    }
    public void GetEmployeeDetails(decimal assessId)
    {
        try
        {
            DataTable dtEmpDetails = new DataTable();
            dtEmpDetails = spm.GetApprEmployeeDataAssessIDWise(assessId);
            if (dtEmpDetails.Rows.Count > 0)
            {
                txtEmpCode.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Code"]);
                txtEmpName.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
                hflEmpDesignation.Value = Convert.ToString(dtEmpDetails.Rows[0]["DesginationName"]);
                hflEmpDepartment.Value = Convert.ToString(dtEmpDetails.Rows[0]["Department_Name"]);
                hflGrade.Value = Convert.ToString(dtEmpDetails.Rows[0]["Grade"]).Trim();
                hflEmailAddress.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
                txtdept.Text = Convert.ToString(dtEmpDetails.Rows[0]["Department_Name"]);
                txtdesig.Text = Convert.ToString(dtEmpDetails.Rows[0]["DesginationName"]);
                txtgrade.Text = Convert.ToString(dtEmpDetails.Rows[0]["Grade"]);
                txtposition.Text = Convert.ToString(dtEmpDetails.Rows[0]["position_desc"]);
                hdnPositionID.Value = Convert.ToString(dtEmpDetails.Rows[0]["Position_id"]);
                hdnlocationname.Value = Convert.ToString(dtEmpDetails.Rows[0]["Location_name"]);
            }
            //  getApproverdata();  
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

            throw;
        }
    }
    public void getAssessIDDetails(decimal assessid)
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.getAssessIDDetails(Convert.ToDecimal(assessid));

        if (dtTrDetails.Rows.Count > 0)
        {
            txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["Assess_FromDate"]);
            txtToDate.Text = Convert.ToString(dtTrDetails.Rows[0]["Assess_ToDate"]);
            txtOverallComment.Text = spm.Decrypt(Convert.ToString(dtTrDetails.Rows[0]["Reviewee_Comment"]));
            hdnPosMappid.Value = Convert.ToString(dtTrDetails.Rows[0]["EmpPosMappId"]); 
        }
    }
    public void getAssessReviewDetails(decimal assessid)
    {
        
        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_Select_Tra_Assess_Review_Detail";

        spars[1] = new SqlParameter("@Assess_id", SqlDbType.Decimal);
        spars[1].Value = Convert.ToDecimal(assessid);

        dsTrDetails = spm.ApprgetDatasetList(spars, "[SP_GETALL_Appraisal_DETAILS]");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            txtPRD1.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["PRD_1"]).Trim());
            txtPRD2.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["PRD_2"]).Trim());
            txtPRD3.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["PRD_3"]).Trim());
            txtPRD4.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["PRD_4"]).Trim());
            //txtIDP1.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["IDP_1"]).Trim());
            //txtIDP2.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["IDP_2"]).Trim());
            //txtIDP3.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["IDP_3"]).Trim());
            txtDissHeldOn.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Discussion_Date"]);
            hdnRevieweeReleasedDate.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["RevieweeReleasedDate"]);
            txtRevieweeDis.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Disagree_Comments"]).Trim());
            txtRecoDissN1.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["N1DissagreeComment"]).Trim());
            txtRecoDissN2.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["N2DissagreeComment"]).Trim());
            txtRecoDissAD1.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["AD1DissagreeComment"]).Trim());
            if (Convert.ToString(txtRevieweeDis.Text)!="")
            {
                chkRevieweeDisAgree.Checked = true;
                txtRevieweeDis.Visible = true;
                SpanRevieweeDis.Visible = true;
                Span11.Visible = true;
               
            }
            else
            {
                chkRevieweeAgree.Checked = true;
                txtRevieweeDis.Visible = false;
                SpanRevieweeDis.Visible = false;
                Span11.Visible = false;
               
            }
           
        }
    }
    public void getMethodDetails()
    {

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "getMethodCodeDesc";


        dsTrDetails = spm.ApprgetDatasetList(spars, "[SP_GETALL_Appraisal_DETAILS]");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            TextBox1.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["MethodDesc"]).Trim();
        }
    }  
    public void getTargetEvaluationDeails( decimal assessid)
    {
        DataTable dtTargetEvaluationDeails = new DataTable();
        decimal KRA_Points_Alloted = 0;
        decimal KRA_Target_Achieved_Points = 0;
        decimal Target_Achieved_Points_Reviewer = 0;
        decimal RevKRA_Points_Alloted = 0;

        if (Convert.ToString(mngAssess.Value) == "Rwee")
        {
            dtTargetEvaluationDeails = spm.GetTargetEvaluationDetails(assessid, "sp_Select_Assess_KRA_Detail");
        }
        else
        {
             dtTargetEvaluationDeails = spm.GetTargetEvaluationDetails(assessid, "sp_Select_Assess_KRA_DetailSubmitted");
        }
        dgTargetEvaluation.DataSource = null;
        dgTargetEvaluation.DataBind();
        foreach (DataRow dr in dtTargetEvaluationDeails.Rows)
        {
            KRA_Points_Alloted = KRA_Points_Alloted + Convert.ToDecimal(dr["KRA_Points_Alloted"]);
            KRA_Target_Achieved_Points = KRA_Target_Achieved_Points + Convert.ToDecimal(dr["KRA_Target_Achieved_Points"]);
            RevKRA_Points_Alloted = RevKRA_Points_Alloted + Convert.ToDecimal(dr["RevKRA_Points_Alloted"]);
            if (Convert.ToString(dr["Target_Achieved_Points_Reviewer"]).Length > 0 )
            {
              Target_Achieved_Points_Reviewer = Target_Achieved_Points_Reviewer + Convert.ToDecimal(dr["Target_Achieved_Points_Reviewer"]);
            }
            
            if (dr["KRA_Description"].ToString() != "")
            {
               if ( spm.Decrypt(dr["KRA_Description"].ToString()).Length > 16 )
               {
                    dr["KRA_Description"] =  spm.Decrypt(dr["KRA_Description"].ToString()).Substring(0,15);
               }
               else
               {
                   dr["KRA_Description"] = spm.Decrypt(dr["KRA_Description"].ToString());
               }
                
               
            }
            if (dr["KRA_AchievedDesc"].ToString() != "")
            {
                if (spm.Decrypt(dr["KRA_AchievedDesc"].ToString()).Length > 16)
                {
                    dr["KRA_AchievedDesc"] = spm.Decrypt(dr["KRA_AchievedDesc"].ToString()).Substring(0, 15);
                }
                else
                {
                    dr["KRA_AchievedDesc"] = spm.Decrypt(dr["KRA_AchievedDesc"].ToString());
                }                
               
            }

           

            if (dr["KRA_AchievedReviewerDesc"].ToString() != "")
            {
                if (spm.Decrypt(dr["KRA_AchievedReviewerDesc"].ToString()).Length > 16)
                {
                    dr["KRA_AchievedReviewerDesc"] = spm.Decrypt(dr["KRA_AchievedReviewerDesc"].ToString()).Substring(0, 15);
                }
                else
                {
                    dr["KRA_AchievedReviewerDesc"] = spm.Decrypt(dr["KRA_AchievedReviewerDesc"].ToString());
                }  
               
            }
        }

        dtTargetEvaluationDeails.AcceptChanges();
        if (dtTargetEvaluationDeails.Rows.Count > 0)
        {  
            dgTargetEvaluation.DataSource = dtTargetEvaluationDeails;
            dgTargetEvaluation.DataBind();
            dgTargetEvaluation.FooterRow.Cells[0].Text = "Total ";
            dgTargetEvaluation.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            dgTargetEvaluation.FooterRow.Cells[1].Text = KRA_Points_Alloted.ToString();
            dgTargetEvaluation.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            dgTargetEvaluation.FooterRow.Cells[3].Text = KRA_Target_Achieved_Points.ToString();
            dgTargetEvaluation.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            dgTargetEvaluation.FooterRow.Cells[4].Text = RevKRA_Points_Alloted.ToString();
            dgTargetEvaluation.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            dgTargetEvaluation.FooterRow.Cells[6].Text = Target_Achieved_Points_Reviewer.ToString();
        }
    }
    public void gvDevPlanDetails(decimal assessid)
    {
        DataTable dtDevPlanDetails = new DataTable();
        dtDevPlanDetails = spm.GetDevPlanDetails(assessid, "sp_SelectDevelopmentPlan");
        gvDevPlan.DataSource = null;
        gvDevPlan.DataBind();
        foreach (DataRow dr in dtDevPlanDetails.Rows)
        {
            if (dr["Dev_area"].ToString() != "")
            {
               // if (spm.Decrypt(dr["Dev_area"].ToString()).Length > 16)
               // {
               //     dr["Dev_area"] = spm.Decrypt(dr["Dev_area"].ToString()).Substring(0, 15); 
               // }
               //else
               //{
                   dr["Dev_area"] = spm.Decrypt(dr["Dev_area"].ToString());
               //} 
            }
        
        }

        dtDevPlanDetails.AcceptChanges();
        if (dtDevPlanDetails.Rows.Count > 0)
        {
            gvDevPlan.DataSource = dtDevPlanDetails;
            gvDevPlan.DataBind();
        }
    }

    public void gvStrengthDetails(decimal assessid)
    {
        DataTable dtStrengthDetails = new DataTable();
        dtStrengthDetails = spm.GetDevPlanDetails(assessid, "sp_SelectStrength");
        gvStrength.DataSource = null;
        gvStrength.DataBind();
        foreach (DataRow dr in dtStrengthDetails.Rows)
        {
            if (dr["Strength"].ToString() != "")
            {
                dr["Strength"] = spm.Decrypt(dr["Strength"].ToString());
            }
        }
        if (dtStrengthDetails.Rows.Count > 0)
        {
            gvStrength.DataSource = dtStrengthDetails;
            gvStrength.DataBind();
        }

    }
    public void gvRecommDetails(decimal assessid)
    {
        DataTable dtRecommDetails = new DataTable();
        dtRecommDetails = spm.GetDevPlanDetails(assessid, "sp_getAssessPerf_Prom_rating");
        gvRecomm.DataSource = null;
        gvRecomm.DataBind();

        if (dtRecommDetails.Rows.Count > 0)
        {
            foreach (DataRow dr in dtRecommDetails.Rows)
            {
                if (dr["PerfrateDesc"].ToString() != "")
                {
                    dr["PerfrateDesc"] = spm.Decrypt(dr["PerfrateDesc"].ToString());
                }
                if (dr["PromorateDesc"].ToString() != "")
                {
                    dr["PromorateDesc"] = spm.Decrypt(dr["PromorateDesc"].ToString());
                }
                if (dr["PromoTypeDesc"].ToString() != "")
                {
                    dr["PromoTypeDesc"] = spm.Decrypt(dr["PromoTypeDesc"].ToString());
                }

                if (dr["Comp_Rating"].ToString() != "")
                {
                    dr["Comp_Rating"] = spm.Decrypt(dr["Comp_Rating"].ToString());
                }
                if ((dr["Comp_Rating"].ToString() == "") && (Convert.ToString(mngAssess.Value) != "N1"))
                {
                    dr["Comp_Rating"] = "NA";
                }
                //if (Convert.ToString(hdnAssessType.Value) == "RevWerEd")
                //{ 

                //}
                //else
                //{

                //}
            }

            dtRecommDetails.AcceptChanges();
            gvRecomm.DataSource = dtRecommDetails;
            gvRecomm.DataBind();
        }

    }

    public void gvRecommDetails_view(decimal assessid, decimal reviewertype)
    {
        DataTable dtRecommDetails = new DataTable();
        dtRecommDetails = spm.GetDevPlanDetails_View(assessid, "sp_getAssessPerf_Prom_rating_viewonly", reviewertype);
        gvRecomm.DataSource = null;
        gvRecomm.DataBind();

        if (dtRecommDetails.Rows.Count > 0)
        {
            foreach (DataRow dr in dtRecommDetails.Rows)
            {
                if (dr["PerfrateDesc"].ToString() != "")
                {
                    dr["PerfrateDesc"] = spm.Decrypt(dr["PerfrateDesc"].ToString());
                }
                if (dr["PromorateDesc"].ToString() != "")
                {
                    dr["PromorateDesc"] = spm.Decrypt(dr["PromorateDesc"].ToString());
                }
                if (dr["PromoTypeDesc"].ToString() != "")
                {
                    dr["PromoTypeDesc"] = spm.Decrypt(dr["PromoTypeDesc"].ToString());
                }

                if (dr["Comp_Rating"].ToString() != "")
                {
                    dr["Comp_Rating"] = spm.Decrypt(dr["Comp_Rating"].ToString());
                }
                if ((dr["Comp_Rating"].ToString() == "") && (Convert.ToString(mngAssess.Value) != "N1"))
                {
                    dr["Comp_Rating"] = "";
                }
                //if (Convert.ToString(hdnAssessType.Value) == "RevWerEd")
                //{ 

                //}
                //else
                //{

                //}
            }

            dtRecommDetails.AcceptChanges();
            gvRecomm.DataSource = dtRecommDetails;
            gvRecomm.DataBind();
        }

    }    

    public void getCompetancyEvaluationDeails(decimal assessid)
    {
         decimal Reviewee_Rating = 0;
         decimal Reviewee_Rating_sum = 0;
         int Reviewee_count = 0;
         decimal Reviewer_Rating = 0;
         decimal Reviewer_Rating_sum = 0;
         int Reviewer_count = 0;
        DataTable dtCompetancyEvaluationDeails = new DataTable();
        dtCompetancyEvaluationDeails = spm.GetTargetEvaluationDetails(assessid, "sp_Select_Assess_Competancy_Detail");
        dgCompetancyEvaluation.DataSource = null;
        dgCompetancyEvaluation.DataBind();
        foreach (DataRow dr in dtCompetancyEvaluationDeails.Rows)
        {
            if (dr["Reviewee_Rating"].ToString() != "")
            {
                dr["Reviewee_Rating"] = spm.Decrypt(dr["Reviewee_Rating"].ToString());
                Reviewee_Rating = Reviewee_Rating + Convert.ToDecimal(dr["Reviewee_Rating"]);
                Reviewee_count = Reviewee_count + 1;
            }
            if (dr["Reviewer_Rating"].ToString() != "")
            {
                dr["Reviewer_Rating"] = spm.Decrypt(dr["Reviewer_Rating"].ToString());
                Reviewer_Rating = Reviewer_Rating + Convert.ToDecimal(dr["Reviewer_Rating"]);
                Reviewer_count = Reviewer_count + 1;
            }

        }

        dtCompetancyEvaluationDeails.AcceptChanges();

        if (dtCompetancyEvaluationDeails.Rows.Count > 0)
        {
            Reviewee_Rating_sum = Reviewee_Rating;

            string encry = null;
            encry = spm.Encrypt(Convert.ToString(Reviewee_Rating_sum));
            hdnsptype.Value = "sp_Update_Tra_Assess_Review_Detail";
            spm.UpdateTra_Assess_Review_Detail(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(encry), hdnsptype.Value, "CompetencyRevieweeSum");
            
            Reviewer_Rating_sum = Reviewer_Rating;
            encry = null;
            encry = spm.Encrypt(Convert.ToString(Reviewer_Rating_sum));
            hdnsptype.Value = "sp_Update_Tra_Assess_Review_Detail";
            spm.UpdateTra_Assess_Review_Detail(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(encry), hdnsptype.Value, "CompetencyReviewerSum");
            
            Reviewee_Rating = Reviewee_Rating / Reviewee_count;
            Reviewee_Rating = decimal.Round(Reviewee_Rating, 2, MidpointRounding.AwayFromZero);
            encry = null;
            encry = spm.Encrypt(Convert.ToString(Reviewee_Rating));
            hdnsptype.Value = "sp_Update_Tra_Assess_Review_Detail";
            spm.UpdateTra_Assess_Review_Detail(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(encry), hdnsptype.Value, "CompetencyRevieweeAvg");
            
            if (Convert.ToString(Reviewer_Rating) != "0")
            {
                Reviewer_Rating = Reviewer_Rating / Reviewer_count;
               
            }
            Reviewer_Rating = decimal.Round(Reviewer_Rating, 2, MidpointRounding.AwayFromZero);
            encry = null;
            encry = spm.Encrypt(Convert.ToString(Reviewer_Rating));
            hdnsptype.Value = "sp_Update_Tra_Assess_Review_Detail";
            spm.UpdateTra_Assess_Review_Detail(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(encry), hdnsptype.Value, "CompetencyReviewerAvg");
          
            dgCompetancyEvaluation.DataSource = dtCompetancyEvaluationDeails;
            dgCompetancyEvaluation.DataBind();
            //Calculate Sum and display in Footer Row


            dgCompetancyEvaluation.FooterRow.Cells[0].Text = "Total <br><hr style='border-top: 1px solid;color:gray'> Average ";
            dgCompetancyEvaluation.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            dgCompetancyEvaluation.FooterRow.Cells[2].Text = Reviewee_Rating_sum.ToString() + "<br><hr style='border-top: 1px solid;color:gray'>" + Reviewee_Rating.ToString();
            dgCompetancyEvaluation.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            if (Convert.ToString(Reviewer_Rating) != "0")
            {
                dgCompetancyEvaluation.FooterRow.Cells[4].Text = Reviewer_Rating_sum.ToString() + "<br><hr style='border-top: 1px solid;color:gray'>" + Reviewer_Rating.ToString();
            }

           
            //dgCompetancyEvaluation.DataBind();
        }

    }
    public void AssessmentCountValidation (string empcode,decimal App_YearId)
    {
        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_getCountAssessment";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(empcode);

        spars[2] = new SqlParameter("@App_Year_id", SqlDbType.Decimal);
        spars[2].Value = App_YearId;

        dsTrDetails = spm.ApprgetDatasetList(spars, "[SP_GETALL_Appraisal_DETAILS]");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            if (dsTrDetails.Tables[0].Rows.Count >= 2)
            {
                lblmessage.Text = "You have already entered 2 Assessment for active Assessment year";
                txtFromdate.Enabled = false;
                txtToDate.Enabled = false;
                btnSave.Visible = false;
                btnTra_competancy.Visible = false;
                btnTra_Details.Visible = false;
                txtOverallComment.Enabled = false;
                return;
            }
            if (dsTrDetails.Tables[0].Rows.Count == 1)
            {
                hdnAssessStartDt.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Assess_FromDate"]).Trim();
                hdnAssessEndDt.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Assess_ToDate"]).Trim();
               // hdnAssessid.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["assess_id"]).Trim();   
            }
        }

    }
    public void getassessmentIdCreated (string fromDt, string ToDt,string empcode,decimal App_YearId)
    {
        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[5];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "getassessmentIdCreated";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(empcode);

        spars[2] = new SqlParameter("@App_Year_id", SqlDbType.Decimal);
        spars[2].Value = App_YearId;

        spars[3] = new SqlParameter("@from_date", SqlDbType.VarChar);
        spars[3].Value = fromDt;

        spars[4] = new SqlParameter("@todate", SqlDbType.VarChar);
        spars[4].Value = ToDt;

        dsTrDetails = spm.ApprgetDatasetList(spars, "[SP_GETALL_Appraisal_DETAILS]");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {          
                hdnAssessid.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["assess_id"]).Trim();   
           
        }

    }       
    public void InsertKRATemplateDetailsToTrans(decimal assessid, decimal KRABuckID )
    {
        DataTable dtKRABuck = new DataTable();
        dtKRABuck = spm.GetKRADetailsFromTemplate(KRABuckID, Convert.ToString(Session["Empcode"]));
        if (dtKRABuck.Rows.Count > 0)
        {
            for (int i = 0; i < dtKRABuck.Rows.Count; i++)
            {
                spm.InsertKRADetails(assessid, KRABuckID, Convert.ToDecimal(dtKRABuck.Rows[i]["KRA_det_id"]));
            }
        }
    }         
    public void getrecoCount()
    {
         DataSet dsTrDetails = new DataSet();
         SqlParameter[] spars = new SqlParameter[4];

         spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
         spars[0].Value = "sp_getcountAssessPerf_Prom_ratingForAppr";

         spars[1] = new SqlParameter("@Assess_id", SqlDbType.VarChar);
         spars[1].Value = Convert.ToString(hdnAssessid.Value);

         spars[2] = new SqlParameter("@assessType", SqlDbType.VarChar);
         spars[2].Value = Convert.ToString(mngAssess.Value);

         spars[3] = new SqlParameter("@empcode", SqlDbType.VarChar);
         spars[3].Value = Convert.ToString(Session["Empcode"]);

         dsTrDetails = spm.ApprgetDatasetList(spars, "SP_GETALL_Appraisal_DETAILS");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                hdngetrecoCount.Value = "1";
               // btnlnkRecommendation.Visible = false;
            }
            else
            {
                hdngetrecoCount.Value = "0";
                //btnlnkRecommendation.Visible = true;
            }
        }
    //public void GetKRATemplates()
    //{
    //    DataTable KRATemplates = new DataTable();
    //    //KRATemplates = spm.getKRATemplate(Convert.ToString(Session["Empcode"]));
    //    KRATemplates = spm.getKRATemplate(Convert.ToString(hdnPositionID.Value));
    //    if (KRATemplates.Rows.Count > 0)
    //    {
    //        lstKRATemplate.DataSource = KRATemplates;
    //        lstKRATemplate.DataTextField = "KRA_Buk_Desc";
    //        lstKRATemplate.DataValueField = "KRA_Buk_id";
    //        lstKRATemplate.DataBind();

    //    }
    //}
    public void getKRAPointsSum()
    {
       DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "getKRAPointsSum";

        spars[1] = new SqlParameter("@Assess_id", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(hdnAssessid.Value);

        dsTrDetails = spm.ApprgetDatasetList(spars, "SP_GETALL_Appraisal_DETAILS");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            hdnTotalPtAlloted.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Points_Alloted"]).Trim();
            hdnTotalPtAchieved.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Achieved_Points"]).Trim();
            hdnTargetTotalPtAchieved.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["TargetAchieved_Points"]).Trim();
            hdnRevPointsAlloted.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["RevPointsAlloted"]).Trim();
            hdnRevRevieweeAchieved.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["RevRevieweeAchieved"]).Trim();  
        }
    }    
    public void getCompetancyCount()
    {
       DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_getReviewerCompetancyCount";

        spars[1] = new SqlParameter("@Assess_id", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(hdnAssessid.Value);

        dsTrDetails = spm.ApprgetDatasetList(spars, "SP_GETALL_Appraisal_DETAILS");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            hdnCompetancyCount.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["CompetancyCount"]).Trim();
            hdnReviewerCompetancyCount.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["reviewerCompetancyCount"]).Trim();
            hdnRevieweeCompetancyCount.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["RevieweeCompetancyCount"]).Trim();            
        }
    }
    public void getReviewerKRADetailCount()
    {
       DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_getReviewerKRADetailCount";

        spars[1] = new SqlParameter("@Assess_id", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(hdnAssessid.Value);

        dsTrDetails = spm.ApprgetDatasetList(spars, "SP_GETALL_Appraisal_DETAILS");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            hdnrevieweeKRADetailCount.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["revieweeKRADetailCount"]).Trim();
            hdnReviewerKRADetailCount.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["ReviewerKRADetailCount"]).Trim();
            hdnReviewerKRADetailDescCount.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["ReviewerKRADetailDescCount"]).Trim();            
        }
    }

    public void EnableControlsReviewView_SelfAssessmentPending()//Rwee
    {
        btnTra_competancy.Enabled = true;
        btnTra_Details.Enabled = true;
        //mobile_btnAddKRA.Enabled = true;
       // mobile_btnAddKRA.Visible = true;
       // SpanKRATEMP.Visible = true;
        //mobile_cancel.Visible = true;
       // txtKRATemplate.Enabled = true;
       // txtKRATemplate.Visible = true;
        //SpanKRATEMP.Visible = false;
        btnSave.Enabled = true;
        btnSave.Visible = true;
        // mobile_cancel.Enabled = true;

        txtOverallComment.Enabled = true;
       // mobile_btnAddKRA.Enabled = true;

        SpanPRD.Visible = false;
        divPRD.Visible = false;
        SpanIDP.Visible = false;
        divIDP.Visible = false;
        SpanRevieweeDis.Visible = false;
        Span11.Visible = false;
        txtRevieweeDis.Visible = false;
        SpanDissHeldOn.Visible = false;
        txtDissHeldOn.Visible = false;
        dgCompetancyEvaluation.Columns[1].Visible = false;
        dgTargetEvaluation.Columns[4].Visible = false;
        dgTargetEvaluation.Columns[5].Visible = false;
        dgTargetEvaluation.Columns[6].Visible = false;
        dgCompetancyEvaluation.Columns[3].Visible = false;
        dgCompetancyEvaluation.Columns[4].Visible = false;
        btnlnkIDP.Visible = false;
        btnlnkPRD.Visible = false;
        btnlnkRecommendation.Visible = false;

        liRecommendations.Visible = false;
        Span1.Visible = false;

        liRecoDissN1.Visible = false;
        txtRecoDissN1.Visible = false;
        RecoDissN1.Visible = false;

        liRecoDissN2.Visible = false;
        RecoDissN2.Visible = false;
        txtRecoDissN2.Visible = false;
        //Span9.Visible = false;

        liRecoDissAD1.Visible = false;
        RecoDissAD1.Visible = false;
        txtRecoDissAD1.Visible = false;
        //Span10.Visible = false;
        chkRevieweeAgree.Visible = false;
        chkRevieweeDisAgree.Visible = false;
      
        btnlnkrediss.Visible = false;
        Span3.Visible = false;
        Span4.Visible = false;
        ligvRecomm.Visible = false;
        liSpanRevieweeDis.Visible = false;
        liSpanDissHeldOn.Visible = false;
        lichkRevieweeDisAgree.Visible = false;
        liSpan6.Visible = false;
        lichkRevieweeAgree.Visible = false;
        liSpan2.Visible = false;
        liRevieweecomments.Visible = false;
        ligvDevPlan.Visible = false;
        ligvStrength.Visible = false;
        liPRD.Visible = false;
        liPRD1.Visible = false;
        liPRD2.Visible = false;
        liPRD3.Visible = false;
        liPRD4.Visible = false;
        liPRD5.Visible = false;
        liDevPlan.Visible = false;
        liMethodDesc.Visible = false;
        liStrengthadd.Visible = false;
    } 
    public void EnableControlsReviewView_SelfAssessmentSubmit() //ReV
    {
        btnTra_competancy.Enabled = false;
        btnTra_Details.Enabled = false;
        //mobile_btnAddKRA.Enabled = false;
        //mobile_btnAddKRA.Visible = false;
        //mobile_cancel.Visible = false;

        //txtKRATemplate.Enabled = false;
       // SpanKRATEMP.Visible = false;
        //txtKRATemplate.Visible = false;

        btnSave.Enabled = false;
        btnSave.Visible = false;
        mobile_cancel.Enabled = false;        

        txtOverallComment.Enabled = false;
        //mobile_btnAddKRA.Enabled = false;
        
        SpanPRD.Visible = false;
        divPRD.Visible = false;
        SpanIDP.Visible = false;
        divIDP.Visible = false;
        SpanRevieweeDis.Visible = false;
        Span11.Visible = false;
        txtRevieweeDis.Visible = false;
        SpanDissHeldOn.Visible = false;
        txtDissHeldOn.Visible = false;
        btnlnkIDP.Visible = false;
        btnlnkPRD.Visible = false;
        btnTra_competancy.Visible = false;
        btnTra_Details.Visible = false;
        btnlnkRecommendation.Visible = false;
        Span1.Visible = false;
        dgCompetancyEvaluation.Columns[1].Visible = false;
        dgTargetEvaluation.Columns[4].Visible = false;
        dgTargetEvaluation.Columns[5].Visible = false;
        dgTargetEvaluation.Columns[6].Visible = false;
        dgCompetancyEvaluation.Columns[3].Visible = false;
        dgCompetancyEvaluation.Columns[4].Visible = false;

        liRecoDissN1.Visible = false;
        txtRecoDissN1.Visible = false;
        RecoDissN1.Visible = false;

        liRecoDissN2.Visible = false;
        RecoDissN2.Visible = false;
        txtRecoDissN2.Visible = false;
       // Span9.Visible = false;

        liRecoDissAD1.Visible = false;
        RecoDissAD1.Visible = false;
        txtRecoDissAD1.Visible = false;
       // Span10.Visible = false;
        chkRevieweeAgree.Visible = false;
        chkRevieweeDisAgree.Visible = false;
        btnlnkrediss.Visible = false;
        Span3.Visible = false;
        Span4.Visible = false;
        ligvRecomm.Visible = false;
        liSpanRevieweeDis.Visible = false;
        liSpanDissHeldOn.Visible = false;
        lichkRevieweeDisAgree.Visible = false;
        liSpan6.Visible = false;
        lichkRevieweeAgree.Visible = false;
        liSpan2.Visible = false;
        liRevieweecomments.Visible = false;
        ligvDevPlan.Visible = false;
        ligvStrength.Visible = false;
        liPRD.Visible = false;
        liPRD1.Visible = false;
        liPRD2.Visible = false;
        liPRD3.Visible = false;
        liPRD4.Visible = false;
        liPRD5.Visible = false;
        liDevPlan.Visible = false;
        liMethodDesc.Visible = false;
        liStrengthadd.Visible = false;

    }
    public void EnableControlsPerformanceReview() //PerR
    {
        
        btnTra_Details.Enabled = false;
        //txtKRATemplate.Visible = false;
        //SpanKRATEMP.Visible = false;
        //mobile_btnAddKRA.Enabled = false;
        //mobile_btnAddKRA.Visible = false;       
        //txtKRATemplate.Enabled = false;
        txtOverallComment.Enabled = false;
        //mobile_btnAddKRA.Enabled = false;
        txtRevieweeDis.Enabled = false;        
        btnTra_Details.Visible = false;
        SpanRevieweeDis.Visible = false;
        Span11.Visible = false;
        txtRevieweeDis.Visible = false;
        btnlnkRecommendation.Visible = false;
        Span1.Visible = false;
        divPRD.Visible = true;
        divIDP.Visible = true;
        txtRecoDissN1.Visible = false;
        RecoDissN1.Visible = false;
        RecoDissN2.Visible = false;
        txtRecoDissN2.Visible = false;
        //Span9.Visible = false;
        RecoDissAD1.Visible = false;
        txtRecoDissAD1.Visible = false;
        //Span10.Visible = false;
        chkRevieweeAgree.Visible = false;
        chkRevieweeDisAgree.Visible = false;
        btnlnkrediss.Visible = false;
        Span3.Visible = false;
        Span4.Visible = false;
        dgCompetancyEvaluation.Columns[1].Visible = false;
        dgCompetancyEvaluation.Columns[3].Visible = false;
        
        liRevieweecomments.Visible = false;
        lichkRevieweeAgree.Visible = false;
        liSpan2.Visible = false;
        lichkRevieweeDisAgree.Visible = false;
        liSpan6.Visible = false;
        liSpanRevieweeDis.Visible = false;
        liRecoDissN1.Visible = false;
        liRecoDissN2.Visible = false;
        liRecoDissAD1.Visible = false;
        liRecommendations.Visible = false;
        ligvRecomm.Visible = false;

    }
    public void EnableControlsPerformanceReviewReviewee()
    {
        //SpanKRATEMP.Visible = false;
        //txtKRATemplate.Visible = false;
        //mobile_btnAddKRA.Visible = false;
        btnTra_Details.Visible = false;
        btnTra_competancy.Visible = false;

        txtOverallComment.Enabled = false;
        txtPRD1.Enabled = false;
        txtPRD2.Enabled = false;
        txtPRD3.Enabled = false;
        txtPRD4.Enabled = false;
        txtIDP1.Enabled = false;
        //txtIDP2.Enabled = false;
        //txtIDP3.Enabled = false;

        btnTra_DevelopmentPlan.Visible = false;
        gvDevPlan.Columns[4].Visible = false;
        txtDissHeldOn.Enabled = false;

        SpanRevieweeDis.Visible = true;
        Span11.Visible = true;
        txtRevieweeDis.Visible = true;
        txtRevieweeDis.Enabled = true;
        btnlnkRecommendation.Visible = false;
        Span1.Visible = false;
        dgTargetEvaluation.Columns[4].Visible = true;
        dgTargetEvaluation.Columns[5].Visible = true;
        dgCompetancyEvaluation.Columns[3].Visible = false;
        dgCompetancyEvaluation.Columns[4].Visible = true;
       
        gvStrength.Columns[2].Visible = false;
        txtRecoDissN1.Enabled = false;
        txtRecoDissN2.Enabled = false;
        txtRecoDissAD1.Enabled = false;
        dgCompetancyEvaluation.Columns[1].Visible = false;


        chkRevieweeAgree.Enabled = true;
        chkRevieweeDisAgree.Enabled = true;
        txtRecoDissN1.Visible = false;
        txtRecoDissN2.Visible = false;
        txtRecoDissAD1.Visible = false;
        RecoDissN1.Visible = false;
        RecoDissN2.Visible = false;
        RecoDissAD1.Visible = false;
        lnkStrengthAdd.Visible = false;
        //Span9.Visible = false;
        //Span10.Visible = false;
        txtIDP1.Visible = false;
        SpanStrengthdesc.Visible = false;
        SpanStrength.Visible = false;

        liRevieweecomments.Visible = true;
        lichkRevieweeAgree.Visible = true;
        liSpan2.Visible = true;
        lichkRevieweeDisAgree.Visible = true;
        liSpan6.Visible = true;
        liSpanRevieweeDis.Visible = true;
        liSpanDissHeldOn.Visible = true;

        liRecoDissN1.Visible = false;
        liRecoDissN2.Visible = false;
        liRecoDissAD1.Visible = false;
        RecoDissN1.Visible = false;
        RecoDissN2.Visible = false;
        RecoDissAD1.Visible = false;
        //Span9.Visible = false;
        //Span10.Visible = false;

        txtRecoDissAD1.Visible = false;
        txtRecoDissN1.Visible = false;
        txtRecoDissN2.Visible = false;
        txtRecoDissAD1.Enabled = false;
        txtRecoDissN1.Enabled = false;
        txtRecoDissN2.Enabled = false;
    }
    public void getselfassessmentReleaseon(decimal assessId)
    {
        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "getselfassessmentReleaseon";

        spars[1] = new SqlParameter("@assess_id", SqlDbType.VarChar);
        spars[1].Value = Convert.ToDecimal(assessId);

        dsTrDetails = spm.ApprgetDatasetList(spars, "[SP_GETALL_Appraisal_DETAILS]");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            hdnselfassessmentReleaseon.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Released_on"]).Trim();
            
             
        }
    }

    protected void mobile_btnPrintPV_Click(object sender, EventArgs e)
    {
        //getApproverdata();
        getPayementVoucher_forPrint();

    }

    private void getPayementVoucher_forPrint()
    {
        try
        {


            #region get payment Voucher details
            DataSet dspaymentVoucher = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];
            decimal Assessid = 0;
            string A_from = "";
            string A_to = "";
            DataTable DT11 = new DataTable();
            DataTable DT12 = new DataTable();
            decimal Sumreviewee = 0;
            decimal Sumreviewer = 0;
            decimal Avgreviewee = 0;
            decimal Avgreviewer = 0;
            Assessid = Convert.ToDecimal(hdnAssessid.Value);
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_getPersonalDetails";

            spars[1] = new SqlParameter("@Assess_id", SqlDbType.Decimal);
            if (Assessid != 0)
                spars[1].Value = Assessid;
            else
                spars[1].Value = 0;


            dspaymentVoucher = spm.ApprgetDatasetList(spars, "SP_GETALL_Appraisal_DETAILS_Booklet_Self");

            if (dspaymentVoucher.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dspaymentVoucher.Tables[0].Rows.Count; i++)
                {
                    A_from = dspaymentVoucher.Tables[0].Rows[i]["FromDate"].ToString();
                    A_to = dspaymentVoucher.Tables[0].Rows[i]["ToDate"].ToString();
                }
            }

            if (dspaymentVoucher.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < dspaymentVoucher.Tables[1].Rows.Count; i++)
                {

                    dspaymentVoucher.Tables[1].Rows[i]["AgreedTargetDesc"] = spm.Decrypt(dspaymentVoucher.Tables[1].Rows[i]["AgreedTargetDesc"].ToString());
                    dspaymentVoucher.Tables[1].Rows[i]["RevieweeDesc"] = spm.Decrypt(dspaymentVoucher.Tables[1].Rows[i]["RevieweeDesc"].ToString());
                    dspaymentVoucher.Tables[1].Rows[i]["ReviewerDesc"] = spm.Decrypt(dspaymentVoucher.Tables[1].Rows[i]["ReviewerDesc"].ToString());
                }
                dspaymentVoucher.Tables[1].AcceptChanges();
            }


            if (dspaymentVoucher.Tables[2].Rows.Count > 0)
            {
                for (int i = 0; i < dspaymentVoucher.Tables[2].Rows.Count; i++)
                {
                    dspaymentVoucher.Tables[2].Rows[i]["attsr"] = dspaymentVoucher.Tables[2].Rows[i]["attsr"].ToString();
                    dspaymentVoucher.Tables[2].Rows[i]["AttributesDesc"] = dspaymentVoucher.Tables[2].Rows[i]["AttributesDesc"].ToString();
                    dspaymentVoucher.Tables[2].Rows[i]["Comp_Detail_Desc"] = dspaymentVoucher.Tables[2].Rows[i]["Comp_Detail_Desc"].ToString();
                    dspaymentVoucher.Tables[2].Rows[i]["RevieweeRate"] = spm.Decrypt(dspaymentVoucher.Tables[2].Rows[i]["RevieweeRate"].ToString());
                    dspaymentVoucher.Tables[2].Rows[i]["ReviewerRate"] = spm.Decrypt(dspaymentVoucher.Tables[2].Rows[i]["ReviewerRate"].ToString());

                }
                dspaymentVoucher.Tables[2].AcceptChanges();
                dspaymentVoucher.Tables[2].Columns.Add("TotalReviewee");
                dspaymentVoucher.Tables[2].Columns.Add("TotalReviewer");
                dspaymentVoucher.Tables[2].Columns.Add("AvgReviewee");
                dspaymentVoucher.Tables[2].Columns.Add("AvgReviewer");
                dspaymentVoucher.AcceptChanges();

                for (int i = 0; i < dspaymentVoucher.Tables[2].Rows.Count; i++)
                {
                    Avgreviewee = Avgreviewee + 1;
                    Avgreviewer = Avgreviewer + 1;
                    Sumreviewee = Sumreviewee + Convert.ToDecimal(0 + dspaymentVoucher.Tables[2].Rows[i]["RevieweeRate"].ToString());
                    Sumreviewer = Sumreviewer + Convert.ToDecimal(0 + dspaymentVoucher.Tables[2].Rows[i]["ReviewerRate"].ToString());
                }

                dspaymentVoucher.Tables[2].Rows[0]["TotalReviewee"] = Sumreviewee.ToString();
                dspaymentVoucher.Tables[2].Rows[0]["TotalReviewer"] = Sumreviewer.ToString();
                dspaymentVoucher.Tables[2].Rows[0]["AvgReviewee"] = decimal.Round(Sumreviewee / Avgreviewee, 2, MidpointRounding.AwayFromZero).ToString();
                dspaymentVoucher.Tables[2].Rows[0]["AvgReviewer"] = decimal.Round(Sumreviewer / Avgreviewer, 2, MidpointRounding.AwayFromZero).ToString();
                dspaymentVoucher.Tables[2].AcceptChanges();

            }


            if (dspaymentVoucher.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < dspaymentVoucher.Tables[3].Rows.Count; i++)
                {
                    dspaymentVoucher.Tables[3].Rows[i]["PRD1"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["PRD1"].ToString());
                    dspaymentVoucher.Tables[3].Rows[i]["PRD2"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["PRD2"].ToString());
                    dspaymentVoucher.Tables[3].Rows[i]["PRD3"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["PRD3"].ToString());
                    dspaymentVoucher.Tables[3].Rows[i]["PRD4"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["PRD4"].ToString());

                    dspaymentVoucher.Tables[3].Rows[i]["IDP"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["IDP"].ToString());
                    dspaymentVoucher.Tables[3].Rows[i]["IDP1"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["IDP1"].ToString());
                    dspaymentVoucher.Tables[3].Rows[i]["IDP2"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["IDP2"].ToString());
                    dspaymentVoucher.Tables[3].Rows[i]["IDP3"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["IDP3"].ToString());
                }
                dspaymentVoucher.Tables[3].AcceptChanges();
            }

            if (dspaymentVoucher.Tables[4].Rows.Count > 0)
            {
                for (int i = 0; i < dspaymentVoucher.Tables[4].Rows.Count; i++)
                {
                    dspaymentVoucher.Tables[4].Rows[i]["Assess_Dev_Plan_id"] = dspaymentVoucher.Tables[4].Rows[i]["Assess_Dev_Plan_id"].ToString();
                    dspaymentVoucher.Tables[4].Rows[i]["Dev_area"] = spm.Decrypt(dspaymentVoucher.Tables[4].Rows[i]["Dev_area"].ToString());
                    dspaymentVoucher.Tables[4].Rows[i]["Method_desc"] = dspaymentVoucher.Tables[4].Rows[i]["Method_desc"].ToString();
                    dspaymentVoucher.Tables[4].Rows[i]["Timelines_desc"] = dspaymentVoucher.Tables[4].Rows[i]["Timelines_desc"].ToString();
                    dspaymentVoucher.Tables[4].Rows[i]["TrainingCodeDesc"] = dspaymentVoucher.Tables[4].Rows[i]["TrainingCodeDesc"].ToString();
                }
                dspaymentVoucher.Tables[4].AcceptChanges();
            }

            if (dspaymentVoucher.Tables[5].Rows.Count > 0)
            {
                for (int i = 0; i < dspaymentVoucher.Tables[5].Rows.Count; i++)
                {
                    dspaymentVoucher.Tables[5].Rows[i]["Strengthid"] = dspaymentVoucher.Tables[5].Rows[i]["Strengthid"].ToString();
                    dspaymentVoucher.Tables[5].Rows[i]["Strength"] = spm.Decrypt(dspaymentVoucher.Tables[5].Rows[i]["Strength"].ToString());
                }
                dspaymentVoucher.Tables[5].AcceptChanges();
            }

            if (dspaymentVoucher.Tables[6].Rows.Count > 0)
            {
                for (int i = 0; i < dspaymentVoucher.Tables[6].Rows.Count; i++)
                {
                    dspaymentVoucher.Tables[6].Rows[i]["pr1"] = "0";
                    dspaymentVoucher.Tables[6].Rows[i]["pr2"] = "0";
                    dspaymentVoucher.Tables[6].Rows[i]["pr3"] = "0";
                }
                dspaymentVoucher.Tables[6].AcceptChanges();
            }

            if (dspaymentVoucher.Tables[9].Rows.Count > 0)
            {

                dspaymentVoucher.Tables[9].Rows[0]["oc1"] = dspaymentVoucher.Tables[9].Rows[0]["oc1"].ToString() == "0" ? "0" : spm.Decrypt(dspaymentVoucher.Tables[9].Rows[0]["oc1"].ToString());
                dspaymentVoucher.Tables[9].Rows[0]["oc2"] = dspaymentVoucher.Tables[9].Rows[0]["oc2"].ToString() == "0" ? "0" : spm.Decrypt(dspaymentVoucher.Tables[9].Rows[0]["oc2"].ToString());
                dspaymentVoucher.Tables[9].Rows[0]["oc3"] = dspaymentVoucher.Tables[9].Rows[0]["oc3"].ToString() == "0" ? "0" : spm.Decrypt(dspaymentVoucher.Tables[9].Rows[0]["oc3"].ToString());
                dspaymentVoucher.Tables[9].AcceptChanges();
            }


            if (dspaymentVoucher.Tables[7].Rows.Count > 0)
            {
                dspaymentVoucher.Tables[7].Rows[0]["Promo_Type_Desc"] = dspaymentVoucher.Tables[7].Rows[0]["Promo_Type_Desc"].ToString() == "0" ? "0" : spm.Decrypt(dspaymentVoucher.Tables[7].Rows[0]["Promo_Type_Desc"].ToString());
                dspaymentVoucher.Tables[7].Rows[0]["pro1"] = "0";
                dspaymentVoucher.Tables[7].Rows[0]["pro2"] = "0";
                dspaymentVoucher.Tables[7].Rows[0]["pro3"] = "0";


                dspaymentVoucher.Tables[7].AcceptChanges();
            }

            if (dspaymentVoucher.Tables[8].Rows.Count > 0)
            {
                dspaymentVoucher.Tables[8].Rows[0]["Promo_Type_Desc"] = dspaymentVoucher.Tables[8].Rows[0]["Promo_Type_Desc"].ToString() == "0" ? "0" : spm.Decrypt(dspaymentVoucher.Tables[8].Rows[0]["Promo_Type_Desc"].ToString());
                dspaymentVoucher.Tables[8].Rows[0]["pp1"] = "0";
                dspaymentVoucher.Tables[8].Rows[0]["pp2"] = "0";
                dspaymentVoucher.Tables[8].Rows[0]["pp3"] = "0";
                dspaymentVoucher.Tables[8].AcceptChanges();
            }



            if (dspaymentVoucher.Tables[11].Rows.Count > 0)
            {
                for (int i = 0; i < dspaymentVoucher.Tables[11].Rows.Count; i++)
                {
                    dspaymentVoucher.Tables[11].Rows[i]["rating"] = spm.Decrypt(dspaymentVoucher.Tables[11].Rows[i]["rating"].ToString());
                    dspaymentVoucher.Tables[11].Rows[i]["ratingDesc"] = dspaymentVoucher.Tables[11].Rows[i]["rating"].ToString() + "-" + spm.Decrypt(dspaymentVoucher.Tables[11].Rows[i]["ratingDesc"].ToString());
                    //dspaymentVoucher.Tables[11].Rows[i]["ratingDesc"] = spm.Decrypt(dspaymentVoucher.Tables[11].Rows[i]["ratingDesc"].ToString());
                    dspaymentVoucher.Tables[11].AcceptChanges();


                }

                DataView dv = dspaymentVoucher.Tables[11].DefaultView;
                dv.Sort = "rating desc";
                DT11 = dv.ToTable();

                // dspaymentVoucher.Tables[11] = dv.ToTable();
                //  dspaymentVoucher.Tables[11].DefaultView.Sort = "rating desc";
                // dspaymentVoucher.Tables[11].AcceptChanges();
            }

            if (dspaymentVoucher.Tables[12].Rows.Count > 0)
            {
                for (int i = 0; i < dspaymentVoucher.Tables[12].Rows.Count; i++)
                {
                    dspaymentVoucher.Tables[12].Rows[i]["rating"] = spm.Decrypt(dspaymentVoucher.Tables[12].Rows[i]["rating"].ToString());
                    dspaymentVoucher.Tables[12].Rows[i]["cnt"] = dspaymentVoucher.Tables[12].Rows[i]["rating"].ToString().Length;
                    //dspaymentVoucher.Tables[12].Rows[i]["ratingDesc"] = dspaymentVoucher.Tables[12].Rows[i]["rating"].ToString() + "-" + spm.Decrypt(dspaymentVoucher.Tables[12].Rows[i]["ratingDesc"].ToString());
                    dspaymentVoucher.Tables[12].Rows[i]["ratingDesc"] = spm.Decrypt(dspaymentVoucher.Tables[12].Rows[i]["ratingDesc"].ToString());

                    dspaymentVoucher.Tables[12].AcceptChanges();

                }
                DataView dv = dspaymentVoucher.Tables[12].DefaultView;


                dv.Sort = "cnt desc,rating";
                DT12 = dv.ToTable();
            }


            if (dspaymentVoucher.Tables[14].Rows.Count > 0)
            {
                for (int i = 0; i < dspaymentVoucher.Tables[14].Rows.Count; i++)
                {
                    dspaymentVoucher.Tables[14].Rows[i]["id"] = spm.Decrypt(dspaymentVoucher.Tables[14].Rows[i]["descript"].ToString());
                    //dspaymentVoucher.Tables[14].Rows[i]["desc"] = dspaymentVoucher.Tables[14].Rows[i]["id"] + "-" + dspaymentVoucher.Tables[14].Rows[i]["desc"];
                    dspaymentVoucher.Tables[14].Rows[i]["desc"] = dspaymentVoucher.Tables[14].Rows[i]["desc"];
                    dspaymentVoucher.Tables[14].AcceptChanges();
                }

            }

            if (dspaymentVoucher.Tables[15].Rows.Count > 0)
            {
                for (int i = 0; i < dspaymentVoucher.Tables[15].Rows.Count; i++)
                {
                    dspaymentVoucher.Tables[15].Rows[i]["pr1"] = "0";
                    dspaymentVoucher.Tables[15].Rows[i]["pr2"] = "0";
                    dspaymentVoucher.Tables[15].Rows[i]["pr3"] = "0";
                }
                dspaymentVoucher.Tables[15].AcceptChanges();
            }

            #endregion

            if (dspaymentVoucher.Tables[0].Rows.Count > 0)
            {
                ReportViewer ReportViewer1 = new ReportViewer();


                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.LocalReport.DataSources.Clear();

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/Booklet.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();

                ReportDataSource datasource = new ReportDataSource("dsPersonalData", dspaymentVoucher.Tables[0]);
                ReportDataSource datasource1 = new ReportDataSource("dsSec1A", dspaymentVoucher.Tables[1]);
                ReportDataSource datasourceB = new ReportDataSource("dsSec1B", dspaymentVoucher.Tables[2]);
                ReportDataSource datasource2 = new ReportDataSource("dsSec1C", dspaymentVoucher.Tables[3]);
                ReportDataSource datasource3 = new ReportDataSource("dsSec1CV_dev", dspaymentVoucher.Tables[4]);
                ReportDataSource datasource4 = new ReportDataSource("dsSec1CV_str", dspaymentVoucher.Tables[5]);
                ReportDataSource datasource5 = new ReportDataSource("dsSec2A", dspaymentVoucher.Tables[6]);
                ReportDataSource datasource6 = new ReportDataSource("dsSec2B_GP", dspaymentVoucher.Tables[7]);
                ReportDataSource datasource7 = new ReportDataSource("dsSec2B_PP", dspaymentVoucher.Tables[8]);
                ReportDataSource datasource8 = new ReportDataSource("dsSec2C", dspaymentVoucher.Tables[9]);
                ReportDataSource datasource9 = new ReportDataSource("dsReviewers", dspaymentVoucher.Tables[10]);

                ReportDataSource datasource10 = new ReportDataSource("dspromRateDesc", DT11);
                ReportDataSource datasource11 = new ReportDataSource("dsperformRateDesc", DT12);
                ReportDataSource datasource12 = new ReportDataSource("dsmethodDesc", dspaymentVoucher.Tables[13]);
                ReportDataSource datasource13 = new ReportDataSource("dsCompRating", dspaymentVoucher.Tables[14]);
                ReportDataSource datasource14 = new ReportDataSource("dsSec2A1", dspaymentVoucher.Tables[15]);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.DataSources.Add(datasource1);
                ReportViewer1.LocalReport.DataSources.Add(datasourceB);
                ReportViewer1.LocalReport.DataSources.Add(datasource2);
                ReportViewer1.LocalReport.DataSources.Add(datasource3);
                ReportViewer1.LocalReport.DataSources.Add(datasource4);
                ReportViewer1.LocalReport.DataSources.Add(datasource5);
                ReportViewer1.LocalReport.DataSources.Add(datasource6);
                ReportViewer1.LocalReport.DataSources.Add(datasource7);
                ReportViewer1.LocalReport.DataSources.Add(datasource8);
                ReportViewer1.LocalReport.DataSources.Add(datasource9);
                ReportViewer1.LocalReport.DataSources.Add(datasource10);
                ReportViewer1.LocalReport.DataSources.Add(datasource11);
                ReportViewer1.LocalReport.DataSources.Add(datasource12);
                ReportViewer1.LocalReport.DataSources.Add(datasource13);
                ReportViewer1.LocalReport.DataSources.Add(datasource14);
                ReportViewer1.LocalReport.Refresh();
                //ReportViewer1.LocalReport.SetParameters(param);


                #region Create PDF file
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                DataTable DataTable1 = new DataTable();
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=MyAppraisalForm." + extension);
                try
                {
                    Response.BinaryWrite(bytes);
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error while generating PDF.');", true);
                    Console.WriteLine(ex.StackTrace);
                }

                #endregion


            }

        }
        catch (Exception ex)
        {
        }
    }


    protected string get_Approver_names_forEmail()
    {
        
            StringBuilder sbapp = new StringBuilder();
            sbapp.Length = 0;
            sbapp.Capacity = 0;
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_ApproverStatusDetails_mail";

            spars[1] = new SqlParameter("@assess_id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDecimal(hdnAssessid.Value);

            dsTrDetails = spm.ApprgetDatasetList(spars, "SP_GETALL_Appraisal_ApproverDetails");
 

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < dsTrDetails.Tables[0].Rows.Count; i++)
                {
                    sbapp.Append("<tr style='height: 30px'>");
                    sbapp.Append("<td width='30%'>" + Convert.ToString(dsTrDetails.Tables[0].Rows[i]["rtype"]).Trim() + "</td>");
                    sbapp.Append("<td width='70%'>&nbsp;" + Convert.ToString(dsTrDetails.Tables[0].Rows[i]["Emp_Name"]).Trim() + "</td>");
                    sbapp.Append("</tr>");
                }
                
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

        
        return Convert.ToString(sbapp);

    }

    #endregion










   
}
