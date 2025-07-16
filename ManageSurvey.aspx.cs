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
using System.Windows.Forms;


public partial class ManageSurvey : System.Web.UI.Page
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
    SqlConnection scon = null;
    SqlCommand sCommand = null;
    SqlDataAdapter sadp = null;
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
                    
                    txtEmpCode.Text = Convert.ToString(Session["ReqEmpCode"]);
                    hdnGrade.Value = Convert.ToString(Session["Grade"]);
                   
                    getSurveyListTrans(Convert.ToString(Session["Empcode"]));
                    

                 
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
            Response.Redirect("~/default.aspx");
        
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //if ((Convert.ToString(hdnAssessid.Value).Trim() != "0") && (Convert.ToString(hdnAssessid.Value).Trim() != ""))
       // {
        decimal TotAnswer = 0;
        decimal TotQuestion = 0;
        foreach (GridViewRow row in gvCompetency.Rows)
        {
            TotQuestion = TotQuestion + 1;
            if ((Convert.ToString((row.FindControl("ddlRowReviewee") as DropDownList).SelectedItem.Value) != "0") && (Convert.ToString((row.FindControl("ddlRowReviewee") as DropDownList).SelectedItem.Value) != ""))
            {
                TotAnswer = TotAnswer + 1;                
            }
        }
        if (TotAnswer < TotQuestion)
        {
            lblmessage.Visible = true;
            lblmessage.Text = "<br>Please Attend all Options.";
            return;
        }
                hdnsptype.Value = "DeleteSurveyQuestionTrans";
                DeleteSurveyTrans(Convert.ToString(Session["Empcode"]), hdnsptype.Value);
                //return;
                foreach (GridViewRow row in gvCompetency.Rows)
                {
                    //Get the HobbyId from the DataKey property.
                    hdnsptype.Value = "InsertSurveyQuestionTrans";

                    decimal Comp_SurveyId = Convert.ToDecimal(gvCompetency.DataKeys[row.RowIndex].Values[0]);
                    decimal Comp_QuestionId = Convert.ToDecimal(gvCompetency.DataKeys[row.RowIndex].Values[1]);
                    
                    //decimal rating = Convert.ToDecimal(gvCompetency.DataKeys[row.RowIndex].Values[3]);
                    //Get the checked value of the CheckBox.
                   // bool isSelected = (row.FindControl("chkRowReviewee") as CheckBox).Checked;
                    decimal RevieweeRating = 0;
                    if ((Convert.ToString((row.FindControl("ddlRowReviewee") as DropDownList).SelectedItem.Value) != "0") && (Convert.ToString((row.FindControl("ddlRowReviewee") as DropDownList).SelectedItem.Value) != ""))
                    {
                         RevieweeRating = Convert.ToDecimal((row.FindControl("ddlRowReviewee") as DropDownList).SelectedItem.Value);
                    }

                    InsertSurveyTrans(Convert.ToString(Session["Empcode"]).Trim(), Comp_SurveyId, Comp_QuestionId, RevieweeRating, hdnsptype.Value);
                   // }

                }
                Response.Redirect("~/default.aspx");
            
            
        //}
    }
   
    protected void gvCompetency_RowDataBound(object sender, GridViewRowEventArgs e)
    {      
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlRowReviewee = (e.Row.FindControl("ddlRowReviewee") as DropDownList);
            
            string lsComp_Survey_id = gvCompetency.DataKeys[e.Row.RowIndex].Values[0].ToString();
            string lsComp_Question_id = gvCompetency.DataKeys[e.Row.RowIndex].Values[1].ToString();
            
            DataTable dtPromoType = new DataTable();
            dtPromoType = getSurveyQuestionAnswerMst("getSurveyQuestionAnswerList", Convert.ToDecimal(lsComp_Survey_id), Convert.ToDecimal(lsComp_Question_id));
            if (dtPromoType.Rows.Count > 0)
            {
                
                foreach (DataRow dr in dtPromoType.Rows)
                {   
                    dr["descript"] = dr["descript"].ToString();                   
                }

                dtPromoType.AcceptChanges();
                ddlRowReviewee.DataSource = dtPromoType;
                ddlRowReviewee.DataTextField = "descript";
                ddlRowReviewee.DataValueField = "id";
                ddlRowReviewee.DataBind();
                
                //Add blank item at index 0.
                ddlRowReviewee.Items.Insert(0, "");
                string selectedReviewee = DataBinder.Eval(e.Row.DataItem, "Reviewee_RatingID").ToString();
                if (selectedReviewee == "") selectedReviewee = "0";
                if (selectedReviewee == "0")
                {
                    selectedReviewee = "";
                    ddlRowReviewee.Items.FindByValue(selectedReviewee).Selected = true;
                    //ddlRowReviewee.Enabled = true;

                    int PerCountN = GetResetCountN();
                    if (PerCountN > 0)
                    {   
                        ddlRowReviewee.Enabled = true;
                        Label1.Text = "Please choose the components of your compensation structure that are flexible. Once you exercise the option, the same will be applicable from 1st July, 2019. <br><br>The options once exercised can only be changed in the open window period declared by HR.<br>For details, please refer to the communication issued on compensation structure before choosing the options.<br><br>";
                        btnSubmit.Visible = true;
                    }
                    else
                    {
                        ddlRowReviewee.Enabled = false;
                        Label1.Text = "We are sorry, you cannot exercise your options now, as the last date is over.<br><br>";
                        btnSubmit.Visible = false;
                    } 
                    //ddlRowReviewee.Enabled = false;
                    //Need to check Reset Option 
                    //******************
                    //******************
                    //Label1.Text = "Please choose the components of your compensation structure that are flexible. Once you exercise the option, the same will be applicable from 1st July, 2019. <br><br>The options once exercised can only be changed in the open window period declared by HR.<br>For details, please refer to the communication issued on compensation structure before choosing the options.<br><br>";               
                    //Label1.Text = "The window period to exercise the options is closed on 20th July, 2019.For those employees who have not provided the said options, the default options will be applied.<br><br>";
                    

                }
                else
                {
                    ddlRowReviewee.Items.FindByValue(selectedReviewee).Selected = true;
                    //ddlRowReviewee.Enabled = false;
                    //Need to check Reset Option 
                    //******************
                    int PerCount = GetResetCount();
                    if (PerCount > 0)
                    {
                        ddlRowReviewee.Enabled = true;
                        Label1.Text = "Please choose the components of your compensation structure that are flexible. Once you exercise the option, the same will be applicable from 1st July, 2019. <br><br>The options once exercised can only be changed in the open window period declared by HR.<br>For details, please refer to the communication issued on compensation structure before choosing the options.<br><br>";
                        btnSubmit.Visible = true;
                    }
                    else
                    {
                        ddlRowReviewee.Enabled = false;
                        Label1.Text = "You have exercised your options. You will now be able to change your options, in the open window period to be declared by HR.";
                        btnSubmit.Visible = false;
                    } 
                    //******************
                    
                }
            }           
        }
    }
    protected int GetResetCount()
    {
        int PerCount = 0;
        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "SurveyDataActiveForReset";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();


        dsTrDetails = spm.getDatasetList(spars, "[SP_GETALL_Survey_DETAILS]");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            PerCount = Convert.ToInt32(dsTrDetails.Tables[0].Rows[0]["ResetSuyveycount"]);
        }
        return PerCount;

        
    }

    protected int GetResetCountN()
    {
        int PerCount = 0;
        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "ActiveSurveyData";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();


        dsTrDetails = spm.getDatasetList(spars, "[SP_GETALL_Survey_DETAILS]");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            PerCount = Convert.ToInt32(dsTrDetails.Tables[0].Rows[0]["ResetSuyveycount"]);
        }
        return PerCount;


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
     public void InsertSurveyTrans(string empno, decimal Surveyid, decimal Questionid, decimal Answerid, string qtype)
     {
         DataTable dtTravelDetails = new DataTable();
         try
         {
             scon = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconnection"].ToString());
             if (scon.State == ConnectionState.Closed || scon.State == ConnectionState.Broken)
                 scon.Open();
             sCommand = new SqlCommand();
             sCommand.Connection = scon;
             sCommand.CommandText = "[SP_GETALL_Survey_DETAILS]";
             sCommand.CommandType = CommandType.StoredProcedure;
             sCommand.Parameters.AddWithValue("@empcode", empno);
             sCommand.Parameters.AddWithValue("@Surveyid", Surveyid);
             sCommand.Parameters.AddWithValue("@Questionid", Questionid);
             sCommand.Parameters.AddWithValue("@Answerid", Answerid);           
             sCommand.Parameters.AddWithValue("@stype", qtype);
             sCommand.ExecuteNonQuery();
             scon.Close();
         }
         catch (Exception)
         {
             throw;
         }
         finally
         {

         }

     }

    public void getSurveyList(decimal Surveyid)
     {
         DataTable dtTrDetails = new DataTable();
         SqlParameter[] spars = new SqlParameter[2];
         spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
         spars[0].Value = "getSurveyList";
         //spars[1] = new SqlParameter("@Surveyid", SqlDbType.Decimal);
         //spars[1].Value = Convert.ToDecimal(1);
         spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
         spars[1].Value = Convert.ToString(Session["Empcode"]);

         dtTrDetails = apprgetDataList(spars, "SP_GETALL_Survey_DETAILS");
         gvCompetency.DataSource = null;
         gvCompetency.DataBind();
         if (dtTrDetails.Rows.Count > 0)
         {
             gvCompetency.DataSource = dtTrDetails;
             gvCompetency.DataBind();
         }
     }

     public void getSurveyListTrans(string SurveyEmp)
     {
         DataTable dtTrDetails = new DataTable();
         SqlParameter[] spars = new SqlParameter[2];
         spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
         spars[0].Value = "getSurveyListAsperTrans";
         spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
         spars[1].Value = Convert.ToString(SurveyEmp);

         dtTrDetails = apprgetDataList(spars, "SP_GETALL_Survey_DETAILS");
         gvCompetency.DataSource = null;
         gvCompetency.DataBind();
         if (dtTrDetails.Rows.Count > 0)
         {
             gvCompetency.DataSource = dtTrDetails;
             gvCompetency.DataBind();
            // btnSubmit.Visible = false;             
         }
         else
         {
            
             getSurveyList(1);
             //btnSubmit.Visible = false;  
         }
     }

     public DataTable apprgetDataList(SqlParameter[] parameters, string strspname)
     {
         DataTable lds = new DataTable();
        
         scon = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconnection"].ToString());
         try
         {
             if (scon.State == ConnectionState.Closed || scon.State == ConnectionState.Broken)
                 scon.Open();
             sCommand = new SqlCommand();
             sCommand.Connection = scon;
             sCommand.CommandText = strspname;
             sCommand.CommandType = CommandType.StoredProcedure;

             foreach (SqlParameter p in parameters)
             {
                 if (p != null)
                 {
                     sCommand.Parameters.Add(p);
                 }
             }
             sadp = new SqlDataAdapter();
             sadp.SelectCommand = sCommand;
             sadp.Fill(lds);
         }
         catch (Exception ex)
         {
             return null;
         }
         finally
         {
             scon.Close();
         }
         return lds;
     }

     public DataTable getSurveyQuestionAnswerMst(string stype, decimal id1, decimal id2)
     {
         DataTable dttrip = new DataTable();
         scon = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconnection"].ToString());
         if (scon.State == ConnectionState.Closed || scon.State == ConnectionState.Broken)
             scon.Open();
         sCommand = new SqlCommand();
         sCommand.Connection = scon;
         sCommand.CommandText = "SP_GETALL_Survey_DETAILS";
         sCommand.CommandType = CommandType.StoredProcedure;
         sCommand.Parameters.AddWithValue("@stype", stype);
         sCommand.Parameters.AddWithValue("@Surveyid", id1);
         sCommand.Parameters.AddWithValue("@Questionid", id2);
         sCommand.Parameters.AddWithValue("@empcode", Convert.ToString(Session["Empcode"]));
         sadp = new SqlDataAdapter();
         sadp.SelectCommand = sCommand;
         sadp.Fill(dttrip);
         scon.Close();
         return dttrip;
     }


     public void DeleteSurveyTrans(string empno,string qtype)
     {
         DataTable dtTravelDetails = new DataTable();
         try
         {
             scon = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconnection"].ToString());
             if (scon.State == ConnectionState.Closed || scon.State == ConnectionState.Broken)
                 scon.Open();
             sCommand = new SqlCommand();
             sCommand.Connection = scon;
             sCommand.CommandText = "[SP_GETALL_Survey_DETAILS]";
             sCommand.CommandType = CommandType.StoredProcedure;
             sCommand.Parameters.AddWithValue("@empcode", empno);
             sCommand.Parameters.AddWithValue("@stype", qtype);
             
             sCommand.ExecuteNonQuery();
             scon.Close();
         }
         catch (Exception)
         {
             throw;
         }
         finally
         {

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
