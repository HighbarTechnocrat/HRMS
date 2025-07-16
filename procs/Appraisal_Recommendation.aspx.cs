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



public partial class Appraisal_Recommendation : System.Web.UI.Page
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/appraisalindex.aspx");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {                    
                    lblmessage.Text = "";
                    editform.Visible = true;
                    divbtn.Visible = false;
                   
                    
                    txtEmpCode.Text = Convert.ToString(Session["ReqEmpCode"]);
                    hdnGrade.Value = Convert.ToString(Session["Grade"]);
                    getActiveAppYear();

                    SpanCreatedby.Visible = false;
                    SpanCreatedon.Visible = false;
                    txtcreatedby.Visible = false;
                    txtcreatedon.Visible = false;
                    if (Request.QueryString.Count > 0)
                    {
                        hdnAssessid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        //hdndtlid.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        
                        mngAssess.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        hdnAssessType.Value = Convert.ToString(Request.QueryString[2]).Trim();
                        hdnflg.Value = Convert.ToString(Request.QueryString[3]).Trim();
                        mngAssessOld.Value = Convert.ToString(Request.QueryString[4]).Trim();

                        if (Convert.ToString(mngAssess.Value) == "N1")
                        {
                            getAttributeCompetency();
                            liB.Visible = true;
                            liB1.Visible = true;
                            liB2.Visible = true;
                            liB3.Visible = true;
                            span5.Visible = true;
                            SpanCompeAttribute.Visible = true;
                            txtattribute.Visible = true;
                            gvattributeLegend.Visible = true;
                        }
                        else
                        {
                                liB.Visible = false;
                                liB1.Visible = false;
                                liB2.Visible = false;
                                liB3.Visible = false;
                            span5.Visible = false;
                            SpanCompeAttribute.Visible = false;
                            txtattribute.Visible = false;
                            gvattributeLegend.Visible = false;
                        }

                        if ((Convert.ToString(hdnAssessType.Value) == "reco") || (Convert.ToString(hdnAssessType.Value) == "RevWerEd") || (Convert.ToString(hdnAssessType.Value) == "RevWerHd")) //Recommendation
                        {
                            GetPerformance();
                            GetPromoType();
                            GetPromotion();
                            
                            getRecommendationdtl();
                            
                        }
                       if(Convert.ToString(hdnflg.Value) == "V")
                       {
                           btnSubmit.Visible = false;
                           txtOverAllComment.Enabled = false;
                           txtPromotion.Enabled = false;
                           txtPromotype.Enabled = false;
                           txtPerformance.Enabled = false;
                           SpanCreatedby.Visible = true;
                           SpanCreatedon.Visible = true;
                           txtcreatedby.Visible = true;
                           txtcreatedon.Visible = true;
                       }
                    }
                    
                   
                    DisplayProfileProperties();
                    loadorder();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
                else
                {
                   
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
        if (Convert.ToString(hdnAssessType.Value) == "reco") //Performance Review Reviewer
        {
            
                Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?xvfsd454f=" + hdnAssessid.Value + "&ytyb56ghf=" + mngAssess.Value + "&bvazxsde=" + hdnAssessType.Value);
            
        }
        if((Convert.ToString(hdnAssessType.Value) == "RevWerEd") ||(Convert.ToString(hdnAssessType.Value) == "RevWerHd"))
        {

            Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?xvfsd454f=" + hdnAssessid.Value + "&ytyb56ghf=" + mngAssess.Value + "&bvazxsde=" + hdnAssessType.Value);
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
        if ((Convert.ToString(lstPerformance.SelectedValue) == "") && (Convert.ToString(txtPerformance.Text) == ""))
        {
            lblmessage.Text = "Please enter the Overall Performance Rating";
            return;
        }
        if ((Convert.ToString(mngAssess.Value) == "N1") && (Convert.ToString(txtattribute.Text) == ""))
        {
            lblmessage.Text = "Please  enter the Overall Personal Attributes Assessment";
            return;
        }
       
        if ((Convert.ToString(lstPromotion.SelectedValue) == "") && (Convert.ToString(txtPromotion.Text) == ""))
        {
            lblmessage.Text = "Please enter the Promotion Recommendation";
            return;
        }
        if ((Convert.ToString(lstPromotype.SelectedValue) == "") && (Convert.ToString(txtPromotype.Text) == ""))
        {
            lblmessage.Text = "Please select the Promotion Type";
            return;
        }
        if (Convert.ToString(txtOverAllComment.Text) == "")
        {
            lblmessage.Text = "Please enter Overall Comments";
            return;
        }
        
     

        string encry = null;
        encry = spm.Encrypt(txtOverAllComment.Text.Trim());

        if (Convert.ToString(mngAssess.Value) != "") //Performance Review Reviewer
        {
            string val = lstattribute.SelectedValue;
            if (val == "")
            {
                val = "0";
            }

            if (Convert.ToString(hdnflg.Value).Trim() == "N")
            {
                hdnsptype.Value = "sp_InsertAssessPerf_Prom_rating";
                spm.InsertRecommendation(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(encry), Convert.ToDecimal(lstPerformance.SelectedValue), Convert.ToDecimal(lstPromotion.SelectedValue), Convert.ToDecimal(lstPromotype.SelectedValue), mngAssess.Value, Convert.ToString(Session["Empcode"]), hdnsptype.Value, Convert.ToDecimal(val));
                Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?xvftsd454f=" + hdnAssessid.Value + "&ytyb56ghf=" + mngAssess.Value + "&nmgnfdmgm=" + hdnAssessType.Value);
            }
            if (Convert.ToString(hdnflg.Value).Trim() == "E")
            {
                hdnsptype.Value = "sp_UpdateAssessPerf_Prom_rating";
                spm.InsertRecommendation(Convert.ToDecimal(hdnAssessid.Value), Convert.ToString(encry), Convert.ToDecimal(lstPerformance.SelectedValue), Convert.ToDecimal(lstPromotion.SelectedValue), Convert.ToDecimal(lstPromotype.SelectedValue), mngAssess.Value, Convert.ToString(Session["Empcode"]), hdnsptype.Value, Convert.ToDecimal(val));
                Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?xvftsd454f=" + hdnAssessid.Value + "&ytyb56ghf=" + mngAssess.Value + "&nmgnfdmgm=" + hdnAssessType.Value);
               
            }
        }    
    }



    protected void lstPerformance_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtPerformance.Text = lstPerformance.SelectedItem.Text;
        PopupControlExtender2.Commit(lstPerformance.SelectedItem.Text);
    }
    protected void lstPromotion_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtPromotion.Text = lstPromotion.SelectedItem.Text;
        PopupControlExtender3.Commit(lstPromotion.SelectedItem.Text);
    }
    protected void lstPromotype_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtPromotype.Text = lstPromotype.SelectedItem.Text;
        PopupControlExtender1.Commit(lstPromotype.SelectedItem.Text);
    }
    protected void lstattribute_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtattribute.Text = lstattribute.SelectedItem.Text;
        PopupControlExtender4.Commit(lstattribute.SelectedItem.Text);
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

     private void getRecommendationdtl()
     {

         DataSet dsTrDetails = new DataSet();
         SqlParameter[] spars = new SqlParameter[3];

         spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
         spars[0].Value = "sp_getAssessPerf_Prom_ratingForAppr";

         spars[1] = new SqlParameter("@Assess_id", SqlDbType.VarChar);
         spars[1].Value = Convert.ToString(hdnAssessid.Value);

         spars[2] = new SqlParameter("@assessType", SqlDbType.VarChar);
         if (Convert.ToString(hdnflg.Value) != "V")
         {
             spars[2].Value = Convert.ToString(mngAssess.Value);
         }
         else
         {
             spars[2].Value = Convert.ToString(mngAssessOld.Value);
         }

         //spars[3] = new SqlParameter("@empcode", SqlDbType.VarChar);
         //spars[3].Value = Convert.ToString(Session["Empcode"]);

         dsTrDetails = spm.ApprgetDatasetList(spars, "SP_GETALL_Appraisal_DETAILS");

         if (dsTrDetails.Tables[0].Rows.Count > 0)
         {
             txtOverAllComment.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Reviewer_Overall_comments"]).Trim());

             lstPerformance.SelectedValue = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Perf_Rate_id"]);

             txtPerformance.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["PerfrateDesc"]).Trim());

             txtPromotion.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["PromorateDesc"]).Trim());
             lstPromotion.SelectedValue = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Prom_Rate_id"]);


             txtPromotype.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["PromoTypeDesc"]).Trim());
             lstPromotype.SelectedValue = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Prom_Type_id"]);

             txtcreatedby.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Emp_Name"]);
             txtcreatedon.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["RecommDate"]);

             if (Convert.ToString(mngAssess.Value) == "N1")
              {
                //txtattribute.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Comp_Rating"]).Trim());
                txtattribute.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Comp_Rating_Id"]);
                lstattribute.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Comp_Rating_Id"]);
                lstattribute.SelectedValue = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Comp_Rating_Id"]);
              }
         }
     }


     public void GetPerformance()
     {
         DataTable dtPerformance = new DataTable();
         dtPerformance = spm.getRecommPreformRating("getPerformanceRating", Convert.ToDecimal(hdnAssessid.Value));
         if (dtPerformance.Rows.Count > 0)
         {
             foreach (DataRow dr in dtPerformance.Rows)
             {
                 
                 if (dr["Rating"].ToString() != "")
                 {
                     dr["Rating"] = spm.Decrypt(dr["Rating"].ToString());
                     dr["cnt"] = dr["Rating"].ToString().Length;
                 }
                 if (dr["descript"].ToString() != "")
                 {
                     //dr["descript"] =   dr["Rating"] +". "+ spm.Decrypt(dr["descript"].ToString());
                     dr["descript"] = spm.Decrypt(dr["descript"].ToString());
                 }
                 
             }
             dtPerformance.AcceptChanges();
             DataView view = dtPerformance.DefaultView;
             view.Sort = "cnt desc,Rating ";
             dtPerformance = view.ToTable();

             gvPerformanceLegend.DataSource = dtPerformance;
             gvPerformanceLegend.DataBind();
             
             lstPerformance.DataSource = dtPerformance;
             lstPerformance.DataTextField = "Rating";
             lstPerformance.DataValueField = "id";
             lstPerformance.DataBind();

             //lstApprover.Items.Clear();
             //if (dtPerformance.Rows.Count > 0)
             //{

             //    lstApprover.DataSource = dtPerformance;
             //    lstApprover.DataTextField = "descript";
             //    lstApprover.DataValueField = "Rating";
             //    lstApprover.DataBind();

             //}
         }
     }
     public void GetPromotion()
     {
         DataTable dtPromotion = new DataTable();
         dtPromotion = spm.getRecommPreformRating("getPromotionRating", Convert.ToDecimal(hdnAssessid.Value));
         if (dtPromotion.Rows.Count > 0)
         {
             foreach (DataRow dr in dtPromotion.Rows)
             {
                 if (dr["descript"].ToString() != "")
                 {
                      //dr["descript"] = spm.Decrypt(dr["descript"].ToString());
                     dr["descript"] = spm.Decrypt(dr["Rating"].ToString()) + "-" + spm.Decrypt(dr["descript"].ToString());
                 }
             }
             dtPromotion.AcceptChanges();
             DataView view = dtPromotion.DefaultView;
             //view.Sort = "descript asc";
             view.Sort = "descript desc";
             dtPromotion = view.ToTable();

             lstPromotion.DataSource = dtPromotion;
             lstPromotion.DataTextField = "descript";
             lstPromotion.DataValueField = "id";
             lstPromotion.DataBind();
         }
     }
     public void GetPromoType()
     {
         DataTable dtPromoType = new DataTable();
         //dtPromoType = spm.getPromoType("getPromoType", Convert.ToString(Session["ReqEmpCode"]));
         dtPromoType = spm.getPromoType("getPromoType", Convert.ToDecimal(hdnAssessid.Value));
         if (dtPromoType.Rows.Count > 0)
         {
             foreach (DataRow dr in dtPromoType.Rows)
             {
                 if (dr["descript"].ToString() != "")
                 {
                     dr["descript"] = spm.Decrypt(dr["descript"].ToString());
                 }
             }
             dtPromoType.AcceptChanges();

             DataView view = dtPromoType.DefaultView;
             view.Sort = "descript asc";
             dtPromoType = view.ToTable();

             lstPromotype.DataSource = dtPromoType;
             lstPromotype.DataTextField = "descript";
             lstPromotype.DataValueField = "id";
             lstPromotype.DataBind();
         }
     }

     public void getAttributeCompetency()
     {
         DataTable dtAttributeCompetency = new DataTable();

         dtAttributeCompetency = spm.getPromoType("getCompetencyAttributeForReco", Convert.ToDecimal(hdnAssessid.Value));
         if (dtAttributeCompetency.Rows.Count > 0)
         {
             foreach (DataRow dr in dtAttributeCompetency.Rows)
             {
                 if (dr["descript"].ToString() != "")
                 {
                     dr["descript"] = spm.Decrypt(dr["descript"].ToString());
                     //dr["desc"] = dr["descript"] + ". " + dr["desc"].ToString();
                     dr["desc"] = dr["desc"].ToString();
                 }
             }
             dtAttributeCompetency.AcceptChanges();

             DataView view = dtAttributeCompetency.DefaultView;
             view.Sort = "descript desc";
             dtAttributeCompetency = view.ToTable();

             lstattribute.DataSource = dtAttributeCompetency;
             lstattribute.DataTextField = "descript";
             lstattribute.DataValueField = "id";
             lstattribute.DataBind();

             gvattributeLegend.DataSource = dtAttributeCompetency;
             gvattributeLegend.DataBind();
         }
     }

    
     #endregion


    
}
