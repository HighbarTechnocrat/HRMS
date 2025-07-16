using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class SelfAssessmentCreate : System.Web.UI.Page
{


    #region Creative_Default_methods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;

    SP_Methods spm = new SP_Methods();
    string strempcode = "";
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void lnkcont_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/SampleForm");
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

                    // fillcountry();
                    country country = classaddress.GetCountryDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["country"].ToString()));
                    // ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByText(country.countryName.Trim()));

                    // fillstate(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["country"].ToString()));
                    states state = classaddress.GetStateDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["state"].ToString()));
                    // ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByText(state.statename.Trim()));

                    //  fillcity(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["state"].ToString()));

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
    #endregion

    #region Page_Events

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {

            //Empcode_Appr
            if (Convert.ToString(Session["Empcode_Appr"]).Trim() == "" || Session["Empcode_Appr"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "procs/Appraisal_login.aspx");
            }

            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
            

            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

			//lblmsg.Visible = true;			
			//lblmsg.Text = "The timeline for filling self assessment has expired and the link is closed";
			//return;

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/appraisalindex");
            }
            else
            {
                Session["chkbtnStatus"] = "";

                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    //   PopulateEmployeeLeaveData();
                    getAssessmentList();
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
    protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnAppYearTypeid.Value = Convert.ToString(gvMngRqstList.DataKeys[row.RowIndex].Values["App_Year_id"]).Trim();
        hdnempcode.Value = Convert.ToString(Session["Empcode"]);
        hdnAssessmentPeriodFrom.Value = Convert.ToString(gvMngRqstList.DataKeys[row.RowIndex].Values["AssessmentPeriodFrom"]).Trim();
        hdnAssessmentPeriodTo.Value = Convert.ToString(gvMngRqstList.DataKeys[row.RowIndex].Values["AssessmentPeriodTo"]).Trim();
        hdnPosMappid.Value = Convert.ToString(gvMngRqstList.DataKeys[row.RowIndex].Values["EmpPosMappId"]).Trim();
        hdnValidFrom.Value = Convert.ToString(gvMngRqstList.DataKeys[row.RowIndex].Values["ValidFrom"]).Trim();
        hdnValidTo.Value = Convert.ToString(gvMngRqstList.DataKeys[row.RowIndex].Values["ValidTo"]).Trim();
        hdn_ps.Value = Convert.ToString(gvMngRqstList.DataKeys[row.RowIndex].Values["p_s"]).Trim();
        hdn_pyear.Value = Convert.ToString(gvMngRqstList.DataKeys[row.RowIndex].Values["pyear"]).Trim();
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";
        string strValidFrom = "";
        string strValidTo = "";
        if (Convert.ToString(hdnAssessmentPeriodFrom.Value).Trim() != "")
        {
            strdate = Convert.ToString(hdnAssessmentPeriodFrom.Value).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(hdnAssessmentPeriodTo.Value).Trim() != "")
        {
            strdate = Convert.ToString(hdnAssessmentPeriodTo.Value).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(hdnValidFrom.Value).Trim() != "")
        {
            strdate = Convert.ToString(hdnValidFrom.Value).Trim().Split('/');
            strValidFrom = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(hdnValidTo.Value).Trim() != "")
        {
            strdate = Convert.ToString(hdnValidTo.Value).Trim().Split('/');
            strValidTo = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        


        getassessmentIdCreated(Convert.ToString(strfromDate), Convert.ToString(strToDate), Convert.ToString(Session["Empcode"]), Convert.ToDecimal(hdnAppYearTypeid.Value), Convert.ToDecimal(hdnPosMappid.Value));


        //if (Convert.ToString(btn.Text) == "Draft")
        if ((Convert.ToString(hdnAssessid.Value).Trim() != "0"))
        {
            getassessmentIdCreated(Convert.ToString(strfromDate), Convert.ToString(strToDate), Convert.ToString(Session["Empcode"]), Convert.ToDecimal(hdnAppYearTypeid.Value), Convert.ToDecimal(hdnPosMappid.Value));
            Response.Redirect("Appraisal_SelfAssessment.aspx?8787hghj767=" + hdnAssessid.Value + "&mndfbmdbf=Rwee");           
        }
        else
        {                      

            if ((Convert.ToString(hdnAssessmentPeriodFrom.Value).Trim() != "") && (Convert.ToString(hdnAssessmentPeriodTo.Value).Trim() != ""))
            {
                hdnsptype.Value = "sp_Insert_Assess_Trans_Main";
                spm.InsertAssessMainDetails(Convert.ToString(strfromDate), Convert.ToString(strToDate), Convert.ToString(hdnempcode.Value), Convert.ToInt32(hdnAppYearTypeid.Value), null, hdnsptype.Value, Convert.ToString(strValidFrom), Convert.ToString(strValidTo), Convert.ToDecimal(hdnPosMappid.Value), Convert.ToString(hdn_ps.Value), Convert.ToString(hdn_pyear.Value));
                getassessmentIdCreated(Convert.ToString(strfromDate), Convert.ToString(strToDate), Convert.ToString(Session["Empcode"]), Convert.ToDecimal(hdnAppYearTypeid.Value), Convert.ToDecimal(hdnPosMappid.Value));

                //Create KRA
                #region get Approved KRA and Create for Self Assessment

                DataSet dsKraGoals = new DataSet();
                SqlParameter[] spars = new SqlParameter[3];

                spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                spars[0].Value = "Create_KRA_ForSelfAssment";

                spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
                spars[1].Value = Convert.ToString(hdnempcode.Value);

                spars[2] = new SqlParameter("@pyear", SqlDbType.VarChar);
                spars[2].Value = Convert.ToString(hdn_pyear.Value);

                dsKraGoals = spm.ApprgetDatasetList(spars, "[SP_GETALL_Appraisal_DETAILS]");

                decimal accessid = 0;

                if (dsKraGoals != null)
                {
                    if (dsKraGoals.Tables[0].Rows.Count > 0)
                        accessid = Convert.ToDecimal(dsKraGoals.Tables[0].Rows[0]["Assess_id"]);

                    if (accessid != 0)
                    {
                        Int32 iGoal = 0;
                        for (int i = 0; i < dsKraGoals.Tables[1].Rows.Count; i++)
                        {
                            iGoal = iGoal + 1;
                            string sgoalTitle = HTMLToText(Convert.ToString(dsKraGoals.Tables[1].Rows[i]["Goal_Title"]));

                            spm.InsertAssessKRADetails(accessid, "", spm.Encrypt(Convert.ToString(iGoal.ToString() + ". " + sgoalTitle).Trim()), "", Convert.ToDecimal(dsKraGoals.Tables[1].Rows[i]["Weightage"]), "", 0, Convert.ToInt32(hdnAppYearTypeid.Value), "", "", "", "sp_Insert_Assess_KRA_Detail", Convert.ToString(Session["Empcode"]).Trim());
                        }
                    }

                }
                #endregion
            }


            Response.Redirect("Appraisal_SelfAssessment.aspx?assessid=" + hdnAssessid.Value + "&mndfbmdbf=Rwee");
        }
    }

    public string HTMLToText(string HTMLCode)
    {
        // Remove new lines since they are not visible in HTML
        HTMLCode = HTMLCode.Replace("\n", " ");

        // Remove tab spaces
        HTMLCode = HTMLCode.Replace("\t", " ");

        // Remove multiple white spaces from HTML
        HTMLCode = Regex.Replace(HTMLCode, "\\s+", " ");

        // Remove HEAD tag
        HTMLCode = Regex.Replace(HTMLCode, "<head.*?</head>", ""
                            , RegexOptions.IgnoreCase | RegexOptions.Singleline);

        // Remove any JavaScript
        HTMLCode = Regex.Replace(HTMLCode, "<script.*?</script>", ""
          , RegexOptions.IgnoreCase | RegexOptions.Singleline);

        // Replace special characters like &, <, >, " etc.
        StringBuilder sbHTML = new StringBuilder(HTMLCode);
        // Note: There are many more special characters, these are just
        // most common. You can add new characters in this arrays if needed
        string[] OldWords = {"&nbsp;", "&amp;", "&quot;", "&lt;",
   "&gt;", "&reg;", "&copy;", "&bull;", "&trade;","&#39;"};
        string[] NewWords = { " ", "&", "\"", "<", ">", "Â®", "Â©", "â€¢", "â„¢", "\'" };
        for (int i = 0; i < OldWords.Length; i++)
        {
            sbHTML.Replace(OldWords[i], NewWords[i]);
        }

        // Check if there are line breaks (<br>) or paragraph (<p>)
        sbHTML.Replace("<br>", "\n<br>");
        sbHTML.Replace("<br ", "\n<br ");
        sbHTML.Replace("<p ", "\n<p ");

        // Finally, remove all HTML tags and return plain text
        return System.Text.RegularExpressions.Regex.Replace(
          sbHTML.ToString(), "<[^>]*>", "");
    }

    protected void gvMngRqstList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow) // checking if row is datarow or not
        {
            //Label lblHasAtt = e.Row.FindControl("lblAtt") as Label;
            LinkButton lbtnViewAtt = e.Row.FindControl("lnkLeaveDetails") as LinkButton;
            string lsAppYearTypeid = gvMngRqstList.DataKeys[e.Row.RowIndex].Values[0].ToString();
            string lsfrdt = gvMngRqstList.DataKeys[e.Row.RowIndex].Values[2].ToString();
            string lstodt = gvMngRqstList.DataKeys[e.Row.RowIndex].Values[3].ToString();
            string Mappid = gvMngRqstList.DataKeys[e.Row.RowIndex].Values[4].ToString();

            string[] strdate;
            string strfromDate = "";
            string strToDate = "";
            if (Convert.ToString(lsfrdt).Trim() != "")
            {
                strdate = Convert.ToString(lsfrdt).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            if (Convert.ToString(lstodt).Trim() != "")
            {
                strdate = Convert.ToString(lstodt).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            getassessmentIdCreated(Convert.ToString(strfromDate), Convert.ToString(strToDate), Convert.ToString(Session["Empcode"]), Convert.ToDecimal(lsAppYearTypeid), Convert.ToDecimal(Mappid));
            if ((Convert.ToString(hdnAssessid.Value).Trim() == "0"))
            {
                lbtnViewAtt.Visible = true;
            }
            else
            {
                lbtnViewAtt.Visible = true;
                if ((Convert.ToString(hdnstatus.Value).Trim() == "3"))
                {
                    lbtnViewAtt.Text = "Submitted";
                    lbtnViewAtt.Attributes.Add("onclick", "return false;");
                }
                else
                {
                    lbtnViewAtt.Text = "Draft";
                }
                
                
            }
        }
    }

    #endregion

    #region Page Methods
    
    private void getAssessmentList()
    {
        try
        {            
            DataTable dtTravelRequest = new DataTable ();
            dtTravelRequest = spm.getSelfAssessList(strempcode);

            gvMngRqstList.DataSource = null;
            gvMngRqstList.DataBind();

            if (dtTravelRequest.Rows.Count > 0)
                {
					lblmsg.Visible = false;
                    gvMngRqstList.DataSource = dtTravelRequest;
                    gvMngRqstList.DataBind();
                }
				else
				{
					lblmsg.Visible = true;
				}
            
        }
        catch (Exception ex)
        {

        }
    }
    public void getassessmentIdCreated(string fromDt, string ToDt, string empcode, decimal App_YearId, decimal id)
    {
        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[6];

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
        spars[5] = new SqlParameter("@id1", SqlDbType.Decimal);
        spars[5].Value = id;

        dsTrDetails = spm.ApprgetDatasetList(spars, "[SP_GETALL_Appraisal_DETAILS]");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            hdnAssessid.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["assess_id"]).Trim();
            hdnstatus.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["krastatus"]).Trim();
        }
        else
        {

            hdnAssessid.Value = "0";
            hdnstatus.Value = "";
        }

    }
    
    #endregion 



    
}
