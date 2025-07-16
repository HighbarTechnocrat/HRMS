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



public partial class Appraisal_DevelopmentPlan : System.Web.UI.Page
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
            lblmsg.Visible = false; ;
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                Page.SmartNavigation = true;
                
                if (!Page.IsPostBack)
                {                    
                    lblmessage.Text = "";
                    editform.Visible = true;
                    divbtn.Visible = false;

                   // txtDevArea.Attributes.Add("onkeypress", "return noanyCharecters(event);");           

                    btnDelete.Visible = false;
                    
                    txtEmpCode.Text = Convert.ToString(Session["ReqEmpCode"]);
                    hdnGrade.Value = Convert.ToString(Session["Grade"]);
                    getActiveAppYear();
                   
                   // GetMobileEligibility();

                    if (Request.QueryString.Count > 0)
                    {
                        hdnAssessid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdndtlid.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        mngAssess.Value = Convert.ToString(Request.QueryString[2]).Trim();
                        

                        if (Convert.ToString(mngAssess.Value) == "PerR") //Performance Review Reviewer
                        {

                        }
                        if (Convert.ToString(mngAssess.Value) == "ReV")//Self Assessment Reviewee View
                        {
                            txtTimeLines.Enabled = false;
                            txtMethod.Enabled = false;
                            txtDevArea.Enabled = false;
                            btnDelete.Visible = false;
                            btnSubmit.Visible = false;
                        }
                    }
                    if (Convert.ToString(hdndtlid.Value).Trim() != "0")
                    {
                        GetMethod();
                        GetTimelines();
                        getTrainingCode();
                        btnDelete.Visible = true;
                        getDevelopmentPlan();
                       
                    }
                    else
                    {
                        GetMethod();
                        GetTimelines();
                        getTrainingCode();
                    }
                    DisplayProfileProperties();
                    loadorder();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
                else
                {
                   
                }

                if (txtMethod.Text.Contains("Training") == true)
                {
                    //SpanStrength.Visible = false;
                  //  SpanStrength.Attributes.Add("display", "none");
                    SpanStrength.Style.Add("display", "none");
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
            Response.Write(ex.Message.ToString());
          
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(mngAssess.Value) == "PerR") //Performance Review Reviewer
        {
            Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?xvfsd454f=" + hdnAssessid.Value + "&ytyb56ghf=PerR");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
      
       if ((Convert.ToString(lstMethod.SelectedValue) == "") && (Convert.ToString(txtMethod.Text) == ""))
       {
           lblmessage.Text = "Please select the Method";
                return;
       }

        
       if (txtMethod.Text.Contains("Training") == true)
       {
           txtTraining.Enabled = true;
           txtTrainingOther.Enabled = false;
           txtMethodDesc.Enabled = false;
           if ((Convert.ToString(lstTraining.SelectedValue) == "") || (Convert.ToString(txtTraining.Text) == ""))
           {
               lblmessage.Text = "Please select Training Program";
               txtTrainingOther.Text = Convert.ToString(txtTraining.Text);
               return;
           }
          
       }
       else
       {
           //lstTraining.SelectedValue = "0";
           lstTraining.SelectedIndex = Convert.ToInt16("0");
           txtTraining.Text = "";
           txtTraining.Enabled = false;
           txtTrainingOther.Enabled = false;
           if (Convert.ToString(txtMethodDesc.Text) == "")
           {
               lblmessage.Text = "Please enter Description of Method";
               return;
           }

       }
        if ((Convert.ToString(lstTimeLines.SelectedValue) == "") && (Convert.ToString(txtTimeLines.Text) == ""))
        {
            lblmessage.Text = "Please select the Timelines";
            return;
        }
        if (Convert.ToString(txtDevArea.Text) == "")
        {
            lblmessage.Text = "Please enter the Development Area of the Reviewee";
            return;
        }
    
        if (txtTraining.Text.Contains("Other") == true)
        {
            if(Convert.ToString(txtTrainingOther.Text) == "")
            {
                txtTrainingOther.Enabled = true;
                lblmessage.Text = "Please specify the Training Program";
                return;
            }
        }
        else
        {

            txtTrainingOther.Text = Convert.ToString(txtTraining.Text.Trim());
        }
       
        string encytxtMethodDesc = null;
        if (Convert.ToString(txtMethodDesc.Text) != "")
        {
            encytxtMethodDesc = spm.Encrypt(Convert.ToString(txtMethodDesc.Text.Trim()));
        }


        if (Convert.ToString(mngAssess.Value) == "PerR") //Performance Review Reviewer
        {
            Decimal dlsttraining = 0;
            if (Convert.ToString(lstTraining.SelectedValue).Trim()!="")
                dlsttraining = Convert.ToDecimal(lstTraining.SelectedValue);

            if (Convert.ToString(hdndtlid.Value).Trim() == "0")
            {
                
                hdnsptype.Value = "sp_InsertDevelopmentPlanToTrans";
                //Comment selectedspm.InsertDevelopmentPlan(Convert.ToDecimal(hdnAssessid.Value), spm.Encrypt(txtDevArea.Text), Convert.ToDecimal(lstMethod.SelectedValue), Convert.ToDecimal(lstTimeLines.SelectedValue), Convert.ToDecimal(lstTraining.SelectedValue),Convert.ToString(txtTrainingOther.Text),encytxtMethodDesc, hdnsptype.Value);
                spm.InsertDevelopmentPlan(Convert.ToDecimal(hdnAssessid.Value), spm.Encrypt(txtDevArea.Text), Convert.ToDecimal(lstMethod.SelectedValue), Convert.ToDecimal(lstTimeLines.SelectedValue), Convert.ToDecimal(dlsttraining), Convert.ToString(txtTrainingOther.Text), encytxtMethodDesc, hdnsptype.Value);
                //Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?assessid=" + hdnAssessid.Value + "&mngAssess=2");
                Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?xvftsd454f=" + hdnAssessid.Value + "&ytyb56ghf=PerR");
            }
            else
            {
                hdnsptype.Value = "sp_UpdateDevelopmentPlanToTrans";
                spm.UpdateDevelopmentPlan(Convert.ToDecimal(hdnAssessid.Value), Convert.ToDecimal(hdndtlid.Value), spm.Encrypt(txtDevArea.Text), Convert.ToDecimal(lstMethod.SelectedValue), Convert.ToDecimal(lstTimeLines.SelectedValue), Convert.ToDecimal(dlsttraining), Convert.ToString(txtTrainingOther.Text), encytxtMethodDesc, hdnsptype.Value);
                //Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?assessid=" + hdnAssessid.Value + "&mngAssess=2");
                Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?xytyfsd454f=" + hdnAssessid.Value + "&ytyb56ghf=PerR");
            }
        }    
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        hdnsptype.Value = "sp_DeleteDevelopmentPlanToTrans";
        spm.DeleteDevelopmentPlan(Convert.ToDecimal(hdnAssessid.Value), Convert.ToDecimal(hdndtlid.Value), hdnsptype.Value);
        //Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?assessid=" + hdnAssessid.Value + "&mngAssess=2");
        Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?gt6fsd454f=" + hdnAssessid.Value + "&ytyb56ghf=PerR");
    }


    protected void lstMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        string msg = null;
        txtMethod.Text = lstMethod.SelectedItem.Text;
        PopupControlExtender2.Commit(lstMethod.SelectedItem.Text);
        if (txtMethod.Text.Contains("Training") == true)
        {
            msg = "Training";
        }
        else
        {
            msg = "Nothing";
        }
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetMethod('" + msg + "');", true);
    }
    protected void lstTimeLines_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTimeLines.Text = lstTimeLines.SelectedItem.Text;
        PopupControlExtender1.Commit(lstTimeLines.SelectedItem.Text);
    }
    protected void lstTraining_SelectedIndexChanged(object sender, EventArgs e)
    {
        string msg = null;
        txtTraining.Text = lstTraining.SelectedItem.Text;
       
        PopupControlExtender3.Commit(lstTraining.SelectedItem.Text);
        if (txtTraining.Text.Contains("Other") == true)
        {
            msg = "Other";
           
        }
        else
        {
            msg = "Nothing";
            
        }
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetCntrls('" + msg + "');", true);
    }
    #endregion

    #region PageMethods
    
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
            hdnAppYearStartDate.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["App_From_Date"]).Trim();
            hdnAppYearEndDate.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["App_To_Date"]).Trim();
            hdnAppAppType.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["App_Type"]).Trim();           
            hdnAppYearTypeid.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["App_Year_id"]).Trim();
        }

    }    
    
     private void getDevelopmentPlan()
     {

         DataSet dsTrDetails = new DataSet();
         SqlParameter[] spars = new SqlParameter[3];

         spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
         spars[0].Value = "sp_SelectDevelopmentPlanToTrans";

         spars[1] = new SqlParameter("@Assess_id", SqlDbType.VarChar);
         spars[1].Value = Convert.ToString(hdnAssessid.Value);

         spars[2] = new SqlParameter("@Assess_Dev_Plan_id", SqlDbType.VarChar);
         spars[2].Value = Convert.ToString(hdndtlid.Value);

         dsTrDetails = spm.ApprgetDatasetList(spars, "SP_GETALL_Appraisal_DETAILS");

         if (dsTrDetails.Tables[0].Rows.Count > 0)
         {
             txtDevArea.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Dev_area"]).Trim());
             lstMethod.SelectedValue = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Method_id"]);
             
             txtMethod.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Method_desc"]).Trim();
             txtTimeLines.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Timelines_desc"]).Trim();
             lstTimeLines.SelectedValue = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Timelines_id"]);

             txtTraining.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["TrainingCodeDesc"]).Trim();
             lstTraining.SelectedValue = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["TrainingCodeID"]);
             txtTrainingOther.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["OtherTraining"]).Trim();
             if (txtTraining.Text.Contains("Other") == true)
             {
                 txtTrainingOther.Enabled = true;
             }
             else
             {
                 txtTrainingOther.Enabled = false;
             }
             if (txtMethod.Text.Contains("Training") == true)
             {
                 txtTraining.Enabled = true;
                 txtMethodDesc.Enabled = false;
             }
             else
             {
                 txtTraining.Enabled = false;
                 txtMethodDesc.Enabled = true;
             }
             txtMethodDesc.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["MethodDesc"]).Trim());
             
         }
         else
         {
             txtTrainingOther.Enabled = false;

         }
     }


     public void GetMethod()
     {
         DataTable KRAMethod = new DataTable();
         KRAMethod = spm.getDevPlanMethod();
         if (KRAMethod.Rows.Count > 0)
         {
             lstMethod.DataSource = KRAMethod;
             lstMethod.DataTextField = "Method_Desc";
             lstMethod.DataValueField = "Method_id";
             lstMethod.DataBind();

         }
     }
     public void GetTimelines()
     {
         DataTable KRATimeLines = new DataTable();
         KRATimeLines = spm.getDevPlanTimelines();
         if (KRATimeLines.Rows.Count > 0)
         {
             lstTimeLines.DataSource = KRATimeLines;
             lstTimeLines.DataTextField = "Timelines_Desc";
             lstTimeLines.DataValueField = "Timelines_id";
             lstTimeLines.DataBind();
         }
     }

     public void getTrainingCode()
     {
         DataTable KRATrainingCode = new DataTable();
         KRATrainingCode = spm.getDevPlanTrainingCode();
         if (KRATrainingCode.Rows.Count > 0)
         {
             lstTraining.DataSource = KRATrainingCode;
             lstTraining.DataTextField = "descript";
             lstTraining.DataValueField = "id";
             lstTraining.DataBind();
             //lstTraining.Items.Insert(0, "");
             lstTraining.Items.Insert(0, new ListItem("", "0")); 
         }
     }

    
     #endregion
       
}
