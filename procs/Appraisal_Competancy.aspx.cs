using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;


public partial class Appraisal_Competancy : System.Web.UI.Page
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Appraisalindex");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    
                    lblmessage.Text = "";
                    editform.Visible = true;
                    divbtn.Visible = false;


                    //txtDescription.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                  //  txtpointsAlloted.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                   // txtTargetAchDescription.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
                   // txtTargetAchPointAlloted.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");


                    
                    
                    txtEmpCode.Text = Convert.ToString(Session["ReqEmpCode"]);
                    hdnGrade.Value = Convert.ToString(Session["Grade"]);
                    getActiveAppYear();

                    //getCompetencyListAsperGrade(Convert.ToString(hdnGrade.Value));
                  
                   // GetMobileEligibility();

                    if (Request.QueryString.Count > 0)
                    {
                        hdnAssessid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdnAssTy.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        getAttributeCompetency(Convert.ToDecimal(hdnAssessid.Value));
                        getCompetencyListAsperTransaction(Convert.ToDecimal(hdnAssessid.Value));
                        
                        if (Convert.ToString(hdnAssTy.Value) == "PerR") //Performance Review Reviewer
                        {
                            
                        }
                        if (Convert.ToString(hdnAssTy.Value) == "Rwee")//Self Assessment Reviewee
                        {
                            
                        }
                       
                    }
                    //if ((Convert.ToString(hdnAssessid.Value).Trim() != "") && (Convert.ToString(hdnAssessid.Value).Trim() != "0"))
                    //{
                    //    btnDelete.Visible = true;
                    // }
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
        if (Convert.ToString(hdnAssTy.Value) == "PerR") //Performance Review Reviewer
        {
            Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?fdsf23423=" + hdnAssessid.Value + "&dgfds=PerR");
        }
        if (Convert.ToString(hdnAssTy.Value) == "Rwee")//Self Assessment Reviewee
        {
            Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?fdsf23423=" + hdnAssessid.Value + "&dgfds=Rwee");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if ((Convert.ToString(hdnAssessid.Value).Trim() != "0") && (Convert.ToString(hdnAssessid.Value).Trim() != ""))
        {
        #region gvCompetencycheck
       
        //    decimal checkhead = 0;
        //    decimal notcheckhead = 0;
        //    int checkheadcount = 0;
        //    bool checkedChk = false;
        //    bool notcheckedChk = false;
        //    foreach (GridViewRow row in gvCompetency.Rows)
        //    {
        //        decimal Comp_head_id = Convert.ToDecimal(gvCompetency.DataKeys[row.RowIndex].Values[1]);

        //        CheckBox isSelected = null;

        //        if (Convert.ToString(hdnAssTy.Value) == "Rwee")
        //        {
        //             isSelected = row.FindControl("chkRowReviewee") as CheckBox;
        //        }
        //        if (Convert.ToString(hdnAssTy.Value) == "PerR")
        //        {
        //             isSelected = row.FindControl("chkRowReviewer") as CheckBox;
        //        }

        //        if (isSelected.Checked)
        //        {
        //            checkedChk = true;
        //            if (checkhead == 0)
        //            {
        //                checkhead = Comp_head_id;
        //                checkheadcount = checkheadcount + 1;
        //                if (notcheckhead == checkhead)
        //                {
        //                    notcheckhead = 0;
        //                    notcheckedChk = false;
        //                }
        //            }
        //            else
        //            {
        //                if (checkhead == Comp_head_id)
        //                {
        //                    if (notcheckhead == checkhead)
        //                    {
        //                        notcheckhead = 0;
        //                        notcheckedChk = false;
        //                    }
        //                    lblmessage.Text = "Please select only one rating from each Competency Group ";
        //                    return;
        //                }
        //                else
        //                {
        //                    checkhead = Comp_head_id;
        //                    checkheadcount = checkheadcount + 1;
        //                    if (notcheckhead == checkhead)
        //                    {
        //                        notcheckhead = 0;
        //                        notcheckedChk = false;
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (checkhead != Comp_head_id)
        //            {
        //                if (notcheckhead == 0)
        //                {
        //                    notcheckhead = Comp_head_id;
        //                    notcheckedChk = true;
        //                }
        //                else
        //                {
        //                    if (notcheckhead == Comp_head_id)
        //                    {
        //                        notcheckedChk = true;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    if (checkedChk == false)
        //    {
        //        lblmessage.Text = "Please select Atleast one rating from each Competency Group";
        //        return;
        //    }
        //    if (notcheckedChk == true)
        //    {
        //        lblmessage.Text = "Please select Atleast one rating from each Competency Group";
        //        return;
        //    }
            #endregion
            if (Convert.ToString(hdnAssTy.Value) == "Rwee")
            {
                hdnsptype.Value = "DeleteCompetencyAssessTranReviewee";
                spm.DeleteCompetencyAssessTranReviewee(Convert.ToDecimal(hdnAssessid.Value), hdnsptype.Value);
                //return;
                foreach (GridViewRow row in gvCompetency.Rows)
                {
                    //Get the HobbyId from the DataKey property.
                    hdnsptype.Value = "InsertCompetencyAssessTranReviewee";

                    decimal Comp_Buk_id = Convert.ToDecimal(gvCompetency.DataKeys[row.RowIndex].Values[0]);
                    decimal Comp_head_id = Convert.ToDecimal(gvCompetency.DataKeys[row.RowIndex].Values[1]);
                    decimal comp_det_id = Convert.ToDecimal(gvCompetency.DataKeys[row.RowIndex].Values[2]);
                    //decimal rating = Convert.ToDecimal(gvCompetency.DataKeys[row.RowIndex].Values[3]);
                    //Get the checked value of the CheckBox.
                   // bool isSelected = (row.FindControl("chkRowReviewee") as CheckBox).Checked;
                    decimal RevieweeRating = 0;
                    if ((Convert.ToString((row.FindControl("ddlRowReviewee") as DropDownList).SelectedItem.Value) != "0") && (Convert.ToString((row.FindControl("ddlRowReviewee") as DropDownList).SelectedItem.Value) != ""))
                    {
                         RevieweeRating = Convert.ToDecimal((row.FindControl("ddlRowReviewee") as DropDownList).SelectedItem.Value);
                    }
                        //spm.InsertCompetencyAssessTranReviewee(Convert.ToDecimal(hdnAssessid.Value), Comp_Buk_id, Comp_head_id, comp_det_id, rating, isSelected, hdnsptype.Value);
                        spm.InsertCompetencyAssessTranReviewee(Convert.ToDecimal(hdnAssessid.Value), Comp_Buk_id, Comp_head_id, comp_det_id, Convert.ToDecimal("0"), RevieweeRating, hdnsptype.Value);
                   // }

                }
                Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?assessid=" + hdnAssessid.Value + "&sd67hhh=Rwee");
            }
            if (Convert.ToString(hdnAssTy.Value) == "PerR")
            {
                foreach (GridViewRow row in gvCompetency.Rows)
                {
                    //Get the HobbyId from the DataKey property.
                    hdnsptype.Value = "UpdateCompetencyAssessTranReviewee";

                    decimal Comp_Buk_id = Convert.ToDecimal(gvCompetency.DataKeys[row.RowIndex].Values[0]);
                    decimal Comp_head_id = Convert.ToDecimal(gvCompetency.DataKeys[row.RowIndex].Values[1]);
                    decimal comp_det_id = Convert.ToDecimal(gvCompetency.DataKeys[row.RowIndex].Values[2]);
                    //decimal rating = Convert.ToDecimal(gvCompetency.DataKeys[row.RowIndex].Values[3]);
                    //Get the checked value of the CheckBox.
                    //bool isSelected = (row.FindControl("chkRowReviewer") as CheckBox).Checked;
                    //if (Convert.ToString((row.FindControl("ddlRowReviewer") as DropDownList).SelectedIndex) != "")
                    decimal ReviewerRating = 0;
                    if ((Convert.ToString((row.FindControl("ddlRowReviewer") as DropDownList).SelectedItem.Value) != "0") && (Convert.ToString((row.FindControl("ddlRowReviewer") as DropDownList).SelectedItem.Value) != ""))
                    {
                        ReviewerRating = Convert.ToDecimal((row.FindControl("ddlRowReviewer") as DropDownList).SelectedItem.Value);
                       
                    }
                    spm.InsertCompetencyAssessTranReviewee(Convert.ToDecimal(hdnAssessid.Value), Comp_Buk_id, Comp_head_id, comp_det_id, Convert.ToDecimal("0"), ReviewerRating, hdnsptype.Value);
                }
                Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?assessid=" + hdnAssessid.Value + "&6gbg7jh=PerR");
            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        hdnsptype.Value = "deleteTempTable";
       // spm.InsertMobileClaimDetails(0, txtFromdate.Text, Convert.ToDecimal(txtAmount.Text), txtEmpCode.Text, Convert.ToDecimal(txtElgAmnt.Text), hdnsptype.Value, txtRemark.Text, "", hdnclaimid.Value,"");
        Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?assessid=" + hdnAssessid.Value + "&mngAssess=1");
    }
    protected void gvCompetency_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (Convert.ToString(hdnAssTy.Value) == "Rwee")
            {
                e.Row.Cells[3].Visible = false;
            }         

        }
        
        
        if (e.Row.RowType == DataControlRowType.DataRow )
        {
            

            if (Convert.ToString(hdnAssTy.Value) == "Rwee")
            {
                e.Row.Cells[3].Visible = false;
            }
            else
            {
                e.Row.Cells[3].Visible = true;
                e.Row.Cells[2].Enabled = false;
            }    
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            DropDownList ddlRowReviewee = (e.Row.FindControl("ddlRowReviewee") as DropDownList);
            DropDownList ddlRowReviewer = (e.Row.FindControl("ddlRowReviewer") as DropDownList);

            string lsComp_Buk_id = gvCompetency.DataKeys[e.Row.RowIndex].Values[0].ToString();
            string lsComp_head_id = gvCompetency.DataKeys[e.Row.RowIndex].Values[1].ToString();
            
            DataTable dtPromoType = new DataTable();
            dtPromoType = spm.getCompetencyRatingMst("getCompetencyRatingMst", Convert.ToDecimal(lsComp_Buk_id), Convert.ToDecimal(lsComp_head_id));
            if (dtPromoType.Rows.Count > 0)
            {
                
                foreach (DataRow dr in dtPromoType.Rows)
                {   
                    dr["descript"] = spm.Decrypt(dr["descript"].ToString());                   
                }

                dtPromoType.AcceptChanges();
                ddlRowReviewee.DataSource = dtPromoType;
                ddlRowReviewee.DataTextField = "descript";
                ddlRowReviewee.DataValueField = "id";
                ddlRowReviewee.DataBind();



                //Add blank item at index 0.
                ddlRowReviewee.Items.Insert(0, "");
                string selectedReviewee = DataBinder.Eval(e.Row.DataItem, "Reviewee_RatingID").ToString();
                if (selectedReviewee == "0")  selectedReviewee="";
                ddlRowReviewee.Items.FindByValue(selectedReviewee).Selected = true;            

                
            }

            if (dtPromoType.Rows.Count > 0)
            {
                //foreach (DataRow dr in dtPromoType.Rows)
                //{
                //    dr["descript"] = spm.Decrypt(dr["descript"].ToString());
                //}

                //dtPromoType.AcceptChanges();                
                ddlRowReviewer.DataSource = dtPromoType;
                ddlRowReviewer.DataTextField = "descript";
                ddlRowReviewer.DataValueField = "id";
                ddlRowReviewer.DataBind();
                ddlRowReviewer.Items.Insert(0, "");
                string selectedReviewer = DataBinder.Eval(e.Row.DataItem, "Reviewer_RatingID").ToString();
                if (selectedReviewer == "0") selectedReviewer = "";
                ddlRowReviewer.Items.FindByValue(selectedReviewer).Selected = true;
            }
        }
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
       
        for (int i = gvCompetency.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = gvCompetency.Rows[i];
            GridViewRow previousRow = gvCompetency.Rows[i - 1];
            for (int j = 0; j < row.Cells.Count-2; j++)
            {
                if (row.Cells[j].Text == previousRow.Cells[j].Text)
                {
                    if (previousRow.Cells[j].RowSpan == 0)
                    {
                        if (row.Cells[j].RowSpan == 0)
                        {
                            ////previousRow.Cells[j].RowSpan += 2;
                            //previousRow.Cells[j + 2].RowSpan += 2;
                            //previousRow.Cells[j + 3].RowSpan += 2;
                        }
                        else
                        {
                            ////previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            //previousRow.Cells[j + 2].RowSpan = row.Cells[j + 2].RowSpan + 1;
                            //previousRow.Cells[j + 3].RowSpan = row.Cells[j + 3].RowSpan + 1; 
                            
                        }
                       //// row.Cells[j].Visible = false;
                       // row.Cells[j + 2].Visible = false;
                       // row.Cells[j + 3].Visible = false;
                    }
                    
                }
                
            }
        }
    }
    #endregion

    #region PageMethods
     
     private void getActiveAppYear()
    {

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_get_ActiveAppYear";

        spars[1] = new SqlParameter("@location", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString("1100");

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = Convert.ToString(txtEmpCode.Text).Trim();

        dsTrDetails = spm.ApprgetDatasetList(spars, "[SP_GETALL_Appraisal_DETAILS]");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {          
            hdnAppYearStartDate.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["App_From_Date"]).Trim();
            hdnAppYearEndDate.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["App_To_Date"]).Trim();
            hdnAppAppType.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["App_Type"]).Trim();           
            hdnAppYearTypeid.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["App_Year_id"]).Trim();
        }

    }         
     //public void getCompetencyListAsperGrade(string grade)
     public void getCompetencyListAsperGrade(decimal assessid)
     {

         DataTable dtTrDetails = new DataTable();
         SqlParameter[] spars = new SqlParameter[2];
         spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
         spars[0].Value = "getCompetencyListAsperGrade";

         //spars[1] = new SqlParameter("@grade", SqlDbType.VarChar);
         //spars[1].Value = Convert.ToString(hdnGrade.Value);
         spars[1] = new SqlParameter("@Assess_id", SqlDbType.Decimal);
         spars[1].Value = Convert.ToDecimal(assessid);

         dtTrDetails = spm.apprgetDataList(spars, "SP_GETALL_Appraisal_DETAILS");

         gvCompetency.DataSource = null;
         gvCompetency.DataBind();
         if (dtTrDetails.Rows.Count > 0)
         {
             //foreach (DataRow dr in dtTrDetails.Rows)
             //{
             //    if (dr["Comp_Detail_Desc"].ToString() != "")
             //    {
             //        dr["Comp_Detail_Desc"] = GetPlainText(dr["Comp_Detail_Desc"].ToString());

             //    }
             //}
             //dtTrDetails.AcceptChanges();
             
             gvCompetency.DataSource = dtTrDetails;
             gvCompetency.DataBind();
         }
     }
     public void getCompetencyListAsperTransaction(decimal assessid)
     {

         DataTable dtTrDetails = new DataTable();
         SqlParameter[] spars = new SqlParameter[2];
         spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
         spars[0].Value = "getCompetencyListAsperTransaction";

         spars[1] = new SqlParameter("@Assess_id", SqlDbType.Decimal);
         spars[1].Value = Convert.ToDecimal(assessid);
         dtTrDetails = spm.apprgetDataList(spars, "SP_GETALL_Appraisal_DETAILS");

         gvCompetency.DataSource = null;
         gvCompetency.DataBind();


         if (dtTrDetails.Rows.Count > 0)
         {
             //foreach (DataRow dr in dtTrDetails.Rows)
             //{
             //    if (dr["Comp_Detail_Desc"].ToString() != "")
             //    {
             //        dr["Comp_Detail_Desc"] = GetPlainText(dr["Comp_Detail_Desc"].ToString());
                    
             //    }
             //}
             //dtTrDetails.AcceptChanges();
             
             gvCompetency.DataSource = dtTrDetails;
             gvCompetency.DataBind();
            
         }
         else
         {
             //getCompetencyListAsperGrade(Convert.ToString(hdnGrade.Value));
             getCompetencyListAsperGrade(Convert.ToDecimal(assessid));
         }
     }
     public void getAttributeCompetency(decimal assessid)
     {
         DataTable dtAttributeCompetency = new DataTable();

         dtAttributeCompetency = spm.getPromoType("getCompetencyAttributeForLegend", Convert.ToDecimal(assessid));
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

             gvattributeLegend.DataSource = dtAttributeCompetency;
             gvattributeLegend.DataBind();
         }
     }
     public string GetPlainText(object richText)
     {
         using (RichTextBox rtBox = new RichTextBox())
         {
           //  rtBox.Rtf = Convert.ToString(richText);
             rtBox.Text = Convert.ToString(richText);
             return rtBox.Text;
         }
     }
    #endregion
     
    
}
