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
using System.Collections.Generic;


public partial class ExpenseAccomodation : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();

    #region Hcc_Default_methods

    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
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

                    // fillcity(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["state"].ToString()));

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

    protected void lnkcont_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/SampleForm");
    }
    #endregion

    #region Page_Events
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            //rdoAccomodation.CssClass.Replace("required", "");
            //rdoFood.CssClass.Replace("required", "");
            //rdoFoodAccomodation.CssClass.Replace("required", "");

            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

            lblmessage.Text = "";
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/travelindex");
            }
            else
            {
                txtAdditionalFoodExp_emp.Text = "Employee";
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    txtTravelType.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtAcctype.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtToDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    //txtLocation.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtPaidBy.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtCharges.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtEligibility.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtFlatElg.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtFlatPaid.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtFlatChg.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtAdditionalFoodExp_exp.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtnoofDays.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");

                    txtFromdate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
                    txtToDate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");


                    editform.Visible = true;
                    divbtn.Visible = false;
                    
                    accmo_delete_btn.Visible = false;
                    lblmessage.Text = "";
                    GetCities();
                    txtEmpCode.Text = Convert.ToString(Session["ReqEmpCode"]);
                    txtTravelType.Text = Convert.ToString(Session["TravelType"]);
                    hdnTripid.Value = Convert.ToString(Session["TripID"]);
                    hdnTryiptypeid.Value = Convert.ToString(Session["TripTypeId"]);
                    hdnfromdate.Value = Convert.ToString(Session["Fromdate"]);
                    hdnTodate.Value = Convert.ToString(Session["Todate"]);

                    get_Actual_DayDiff();
                    if (Convert.ToString(hdnTripid.Value).Trim() == "")
                    {
                        txtLocation.Text = Convert.ToString(Session["Location"]);
                    }
                    else
                    {
                        getAccomodationDetails();
                    }

                    if (Request.QueryString.Count > 0)
                    {
                        hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdnAccdtlsid.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        hdnexp_id.Value = Convert.ToString(Request.QueryString[2]).Trim();

                        if (Convert.ToString(hdnAccdtlsid.Value).Trim() != "0")
                        {
                            getAccDetailsEdit();
                            accmo_btnSave.Text = "Update";
                            accmo_delete_btn.Visible = true;
                        }

                    }
                    txtTravelType.Enabled = false;

                    //   txtLocation.Enabled = false;

                    //txtToDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    //txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");

                    if (Convert.ToString(hdnAccdtlsid.Value).Trim() != "0"  && Convert.ToString(hdnIsThrughCOS.Value).Trim()=="Yes")
                    {
                        if (Convert.ToString(hdnTripid.Value).Trim() != "0" && Convert.ToString(hdnTripid.Value).Trim() != "")
                        {
                            Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                            txtTravelType.BackColor = color;
                            accmo_delete_btn.Visible = false;
                            txtFromdate.Enabled = false;
                            txtToDate.Enabled = false;
                          //  txtRemarks.Enabled = false;
                            txtLocation.Enabled = false;
                          //  txtAcctype.Enabled = false;
                           // txtAcctype.BackColor = color;
                        }
                    }
                    get_Actual_DayDiff();
                    DisplayProfileProperties();                   
                    setControlVisibility();
                    loadorder();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
                //{
                //    get_Actual_Flats_rates();
                //    txtCalculateAmoutns();
                //}
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    protected void accmo_btnSave_Click(object sender, EventArgs e)
    {
         
        if (Check_Cities_name(txtLocation.Text) == false)
        {
            lblmessage.Text = "Please Select Correct Location.";
            return;
        }
        add_updateDelete_Accomodations("InsertTempTable");
        //InsertTempTable

    }

    private Boolean Check_Cities_name(string strcityname)
    {
        Boolean blnchk = false;
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_getCity_dtls_exps";

        spars[1] = new SqlParameter("@exp_trvl", SqlDbType.VarChar);
        if (Convert.ToString(strcityname).Trim() != "")
            spars[1].Value = Convert.ToString(strcityname).Trim();

        spars[2] = new SqlParameter("@exp_sr_no", SqlDbType.VarChar);
        if (Convert.ToString(hdnTryiptypeid.Value).Trim() != "")
            spars[2].Value = Convert.ToString(hdnTryiptypeid.Value).Trim();

        DataTable dtcities = spm.getDataList(spars, "SP_GETALL_Expense_DETAILS");

        if (dtcities != null)
        {
            if (dtcities.Rows.Count > 0)
            {
                blnchk = true;
            }
        }
        return blnchk;
    }


    private void add_updateDelete_Accomodations(string strtype)
    {
        string[] strdate;

        #region Check All Fields Blank
        //  lblmessage.Text = "";
        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "Please select From Date";
            return;
        }
        if (Convert.ToString(txtToDate.Text).Trim() == "")
        {
            lblmessage.Text = "Please select To Date";          
            return;
        }         

        if (Convert.ToString(txtnoofDays.Text).Trim() == "" || Convert.ToString(txtnoofDays.Text).Trim() == "0" || Convert.ToString(txtnoofDays.Text).Trim() == "0.00")
        {
            lblmessage.Text = "Please enter correct Actual stay duration (in days)";            
            return;
        }

        if (Convert.ToString(txtLocation.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Location For Travel";            
            return;
        }

        if (Convert.ToString(txtRemarks.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Remarks";
            return;
        }

        #region if Booked Through Hotel

        if (Convert.ToString(txtAcctype.Text).Trim() == "Hotel")
        {
            if (Convert.ToString(txtCharges.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtCharges.Text).Trim().Split('.');
                if (strdate.Length > 2)
                {
                    lblmessage.Text = "Please enter correct Amount for Actual";
                    txtCharges.Text = "0";
                    return;
                }
                if (Convert.ToString(txtEligibility.Text).Trim() != "")
                {
                    if (Convert.ToString(txtEligibility.Text).Trim() != "0.00" || Convert.ToString(txtEligibility.Text).Trim() != "0")
                    {
                        if (Convert.ToString(txtCharges.Text).Trim() == "")
                        {
                            lblmessage.Text = "Please enter the Amount for Actual";
                            return;
                        }
                        //Decimal dfare = 0;
                        //dfare = Convert.ToDecimal(txtCharges.Text);
                        //if (dfare == 0)
                        //{
                        //    lblmessage.Text = "Please enter correct Amount for Actual";
                        //    return;
                        //}
                    }
                }
            }

        }
        #endregion

        if (Convert.ToString(txtFlatChg.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFlatChg.Text).Trim().Split('.');
            if (strdate.Length > 2)
            {
                txtFlatChg.Text = "0";
                lblmessage.Text = "Please enter correct Flat Rate Amount";
                Session["chkTrvlAccLocalTrvlbtnStatus"] = "";
                return;
            }
            if (Convert.ToString(txtFlatElg.Text).Trim() != "")
            {
                if (Convert.ToString(txtFlatElg.Text).Trim() != "0.00" || Convert.ToString(txtFlatElg.Text).Trim() != "0")
                {
                    if (Convert.ToString(txtFlatChg.Text).Trim() == "")
                    {
                        //lblmessage.Text = "Please enter the Flat Charges";
                        //return;
                    }
                    //Decimal dfare = 0;
                    //dfare = Convert.ToDecimal(txtFlatChg.Text);
                    //if (dfare == 0)
                    //{
                    //    lblmessage.Text = "Please enter correct Flat Charges";
                    //    return;
                    //}
                }
            }
        }
        //if (Convert.ToString(txtRemarks.Text).Trim() == "")
        //{
        //    lblmessage.Text = "Please Enter Remarks For Travel";
        //    return;
        //}

           
            strdate = Convert.ToString(txtnoofDays.Text).Trim().Split('.');
            if (strdate.Length > 2)
            {
                txtnoofDays.Text = "0";
                lblmessage.Text = "Please enter correct Actual stay duration (in days)";
                Session["chkTrvlAccLocalTrvlbtnStatus"] = "";
                return;
            }
            if (Convert.ToString(txtnoofDays.Text).Trim() != "")
            {
                if (Convert.ToString(txtnoofDays.Text).Trim() != "0.00" || Convert.ToString(txtnoofDays.Text).Trim() != "0")
                {
                    if (Convert.ToString(txtnoofDays.Text).Trim() == "")
                    {
                        lblmessage.Text = "Please enter Actual stay duration (in days)";
                        Session["chkTrvlAccLocalTrvlbtnStatus"] = "";
                        return;
                    }
                    Decimal dfare = 0;
                    dfare = Convert.ToDecimal(txtnoofDays.Text);
                    if (dfare == 0)
                    {
                        lblmessage.Text = "Please enter Actual stay duration (in days)";
                        Session["chkTrvlAccLocalTrvlbtnStatus"] = "";
                        return;
                    }
                }
            }

        #endregion

        if (Convert.ToString(Session["chkTrvlAccLocalTrvlbtnStatus"]).Trim() != "")
        {
            Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value);
        }
        Session["chkTrvlAccLocalTrvlbtnStatus"] = "Travel Expenses button Event is Submitted";


        if(rdoOwnArgmnet.Checked==true)
        {
            
            if(Convert.ToString(txtaddintionalExpens.Text).Trim()!="" && Convert.ToString(txtaddintionalExpens.Text).Trim()!="0")
            {
                txtAdditional_exp_deviation.Text = "Yes";
            }

        }
        string strfromDate = "";
        string strToDate = "";
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
 
        
        SqlParameter[] spars = new SqlParameter[25];

        #region Set SQL Parameters
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = strtype;
       // spars[0].Value = "InsertTempTable";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

        spars[2] = new SqlParameter("@trip_id", SqlDbType.Decimal);
        spars[2].Value = hdnTripid.Value;

        spars[3] = new SqlParameter("@Trip_type_id", SqlDbType.Decimal);
        if (Convert.ToString(hdnTryiptypeid.Value).Trim() != "")
            spars[3].Value = Convert.ToInt32(hdnTryiptypeid.Value);

        spars[4] = new SqlParameter("@from_date", SqlDbType.VarChar);
        spars[4].Value = strfromDate;

        spars[5] = new SqlParameter("@todate", SqlDbType.VarChar);
        spars[5].Value = strToDate;

        spars[6] = new SqlParameter("@location", SqlDbType.VarChar);
        spars[6].Value = Convert.ToString(txtLocation.Text).Trim();

        spars[7] = new SqlParameter("@local_accomodation_remarks", SqlDbType.VarChar);
        spars[7].Value = Convert.ToString(txtRemarks.Text).Trim();

        spars[8] = new SqlParameter("@Accomodation_type", SqlDbType.VarChar);
        if (rdoAccomodation.Checked==true)
            spars[8].Value = Convert.ToString(rdoAccomodation.Text).Trim();
        else if (rdoFood.Checked == true)
            spars[8].Value = Convert.ToString(rdoFood.Text).Trim();
        else if (rdoFoodAccomodation.Checked == true)
            spars[8].Value = Convert.ToString(rdoFoodAccomodation.Text).Trim();
        else if (rdoOwnArgmnet.Checked == true)
            spars[8].Value = Convert.ToString(rdoOwnArgmnet.Text).Trim();
        //spars[8].Value = Convert.ToString(txtAcctype.Text).Trim();

        spars[9] = new SqlParameter("@ActualPaid_by", SqlDbType.VarChar);
        if (Convert.ToString(txtPaidBy.Text).Trim() != "")
            spars[9].Value = Convert.ToString(txtPaidBy.Text).Trim();

        spars[10] = new SqlParameter("@ActualCharges", SqlDbType.Decimal);
        if (Convert.ToString(txtCharges.Text).Trim() != "")
            spars[10].Value = Math.Round(Convert.ToDecimal(txtCharges.Text), 2);        

        spars[11] = new SqlParameter("@ActualDeviation", SqlDbType.VarChar);
        if (Convert.ToString(txtDeviation.Text).Trim() != "")
            spars[11].Value = Convert.ToString(txtDeviation.Text).Trim();

        spars[12] = new SqlParameter("@ActualEligibility", SqlDbType.Decimal);
        if (Convert.ToString(txtEligibility.Text).Trim() != "")
            spars[12].Value = Convert.ToDecimal(txtEligibility.Text);

        spars[13] = new SqlParameter("@FLatPaid_by", SqlDbType.VarChar);
        if (Convert.ToString(txtFlatPaid.Text).Trim() != "")
            spars[13].Value = Convert.ToString(txtFlatPaid.Text).Trim();

        spars[14] = new SqlParameter("@FlatCharges", SqlDbType.Decimal);
        if (Convert.ToString(txtFlatChg.Text).Trim() != "")
            spars[14].Value = Math.Round(Convert.ToDecimal(txtFlatChg.Text), 2);

        spars[15] = new SqlParameter("@FLatDeviation", SqlDbType.VarChar);
        if (Convert.ToString(txtFlatDev.Text).Trim() != "")
            spars[15].Value = Convert.ToString(txtFlatDev.Text).Trim();

        spars[16] = new SqlParameter("@FlatEligibility", SqlDbType.Decimal);
        if (Convert.ToString(txtFlatElg.Text).Trim() != "")
            spars[16].Value = Convert.ToDecimal(txtFlatElg.Text);

        spars[17] = new SqlParameter("@chkupdate", SqlDbType.VarChar);
        if (Convert.ToString(accmo_btnSave.Text).Trim() == "Update")
            spars[17].Value = Convert.ToString("update");
        else
            spars[17].Value = Convert.ToString("insert");

        spars[18] = new SqlParameter("@local_accomodation_id", SqlDbType.Int);
        if (Convert.ToString(hdnAccdtlsid.Value).Trim() != "")
            spars[18].Value = hdnAccdtlsid.Value;
        else
            spars[18].Value = DBNull.Value;

        spars[19] = new SqlParameter("@is_thorugh_cos", SqlDbType.VarChar);
        //if (Convert.ToString(accmo_btnSave.Text).Trim() == "Update")
        //    spars[19].Value = Convert.ToString(Session["isthrughCOS"]).Trim();
        //else
            spars[19].Value = Convert.ToString("No");

            spars[20] = new SqlParameter("@AdditionalFoodExp_emp", SqlDbType.VarChar);
        if (Convert.ToString(txtAdditionalFoodExp_emp.Text).Trim() != "")
            spars[20].Value =Convert.ToString(txtAdditionalFoodExp_emp.Text).Trim();
        else
            spars[20].Value = DBNull.Value;

        spars[21] = new SqlParameter("@AdditionalFoodExp_exp", SqlDbType.Decimal);
        if (Convert.ToString(txtAdditionalFoodExp_exp.Text).Trim() != "")
            spars[21].Value =Math.Round(Convert.ToDecimal(txtAdditionalFoodExp_exp.Text), 2);
        else
            spars[21].Value = DBNull.Value;

        spars[22] = new SqlParameter("@AdditionalExp_exp_deviation", SqlDbType.VarChar);
        if (Convert.ToString(txtAdditional_exp_deviation.Text).Trim() != "")
            spars[22].Value = Convert.ToString(txtAdditional_exp_deviation.Text).Trim();
        else
            spars[22].Value = DBNull.Value;

        spars[23] = new SqlParameter("@additnal_flat_exps", SqlDbType.Decimal);
        if (Convert.ToString(txtaddintionalExpens.Text).Trim() != "")
            spars[23].Value = Math.Round(Convert.ToDecimal(txtaddintionalExpens.Text), 2);
        else
            spars[23].Value = DBNull.Value;

        spars[24] = new SqlParameter("@noofdays", SqlDbType.Decimal);
        if (Convert.ToString(txtnoofDays.Text).Trim() != "")
            spars[24].Value = Math.Round(Convert.ToDecimal(txtnoofDays.Text), 2);
        else
            spars[24].Value = DBNull.Value;


        #endregion

        DataTable dt = spm.InsertorUpdateData(spars, "SP_insert_Expense_accomodation_details");
        Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value+"&expid="+hdnexp_id.Value);


        //if (accmo_btnSave.Text == "Submit")
        //{
        //    spm.InsertExpenseAccomodationDetails(Convert.ToInt32(hdnTryiptypeid.Value), strfromDate, strToDate, txtLocation.Text, txtEmpCode.Text, 0,  txtRemarks.Text, "InsertTempTable",txtAcctype.Text,txtPaidBy.Text,Convert.ToDecimal(txtCharges.Text),txtDeviation.Text,Convert.ToDecimal(txtEligibility.Text),txtFlatPaid.Text,Convert.ToDecimal(txtFlatChg.Text),txtFlatDev.Text,Convert.ToDecimal(txtFlatElg.Text));
        //  //  Response.Redirect("~/procs/ExpenseWithoutTravlReqst.aspx");
        //    Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid="+ hdnTripid.Value);
        //}
        //else
        //{

        //    //spm.UpdateAccomodationDetails(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnAccdtlsid.Value), strfromDate, strToDate, txtLocation.Text, hdnCOS.Value, txtRequirement.Text, "UpdateMainTable");
        //    //Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
        //}


    }


    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "From date  cannot be blank";
            return;
        }
        get_Actual_DayDiff();
        get_Actual_Flats_rates();
        DateValidations();
    }

    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            return;
        }
        get_Actual_DayDiff();
        get_Actual_Flats_rates();
        DateValidations();
    }

    protected void lstLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtLocation.Text = lstLocation.SelectedItem.Text;
       // get_Actual_Flats_rates();
        PopupControlExtender5.Commit(lstLocation.SelectedItem.Text);
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "validateTripType();", true);
    }

    protected void lstTravelType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lstAccType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAcctype.Text = lstAccType.SelectedItem.Text;
        
        PopupControlExtender1.Commit(lstAccType.SelectedItem.Text);
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "validateTripType();", true);
    }
    protected void lstPaidBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtPaidBy.Text = lstPaidBy.SelectedItem.Text;
        PopupControlExtender3.Commit(lstPaidBy.SelectedItem.Text);
    }
    protected void lstFlatPaid_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFlatPaid.Text = lstFlatPaid.SelectedItem.Text;
        PopupControlExtender4.Commit(lstFlatPaid.SelectedItem.Text);
    }

    protected void accmo_cancel_btn_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value);
    }
    protected void accmo_delete_btn_Click(object sender, EventArgs e)
    {
        add_updateDelete_Accomodations("deleteTempTable");
    }

    protected void rdoAccomodation_CheckedChanged(object sender, EventArgs e)
    {
        txtFlatChg.Enabled = true;


        #region Set textBox Value
        txtCharges.Text = "0";
        txtAdditionalFoodExp_exp.Text = "0";
        txtaddintionalExpens.Text = "0";
        txtFlatChg.Text = "0";
        txtEligibility.Text = "0.00";
        txtFlatElg.Text = "0.00";

        #endregion

        setControlVisibility();
        get_Actual_Flats_rates();

        //if (rdoFoodAccomodation.Checked == true)
        //{
        //    txtFlatChg.Enabled = false;
        //    txtFlatElg.Text = "0";
        //}


    }

    protected void txtnoofDays_TextChanged(object sender, EventArgs e)
    {
        get_Actual_DayDiff();
    }

    protected void txtAdditionalFoodExp_exp_TextChanged(object sender, EventArgs e)
    {
        if (txtCheck_CorrectAmtount(txtAdditionalFoodExp_exp) == false)
        {
            get_Actual_Flats_rates();
            txtCalculateAmoutns();
        }

        if (rdoFoodAccomodation.Checked == true)
        {
            if (txtCheck_CorrectAmtount(txtFlatChg) == false)
                getDeviation_For_Geuest_Hose_withoutFood();
        }

    }
    protected void txtFlatChg_TextChanged(object sender, EventArgs e)
    {

        //if Hotel
        if (rdoFoodAccomodation.Checked == true)
        {

            if (txtCheck_CorrectAmtount(txtFlatChg) == false)
                getDeviation_For_Geuest_Hose_withoutFood();
            else
                return;
        }

        if (rdoFood.Checked == true)
        {
            getdeviation_Guesthouse_wihtFood();
        }

        if (rdoOwnArgmnet.Checked == true)
        {
            txtCheck_CorrectAmtount(txtFlatChg);
        }


        #region if Own Arrangement then Don't allow to enter more than eligibility
        if (rdoOwnArgmnet.Checked == true)
        {

            Double dfltaexlibilty = 0;
            Double dtxtactchrs = 0;

            if (Convert.ToString(txtFlatChg.Text).Trim() != "")
                dtxtactchrs = Convert.ToDouble(txtFlatChg.Text);

            if (Convert.ToString(txtFlatElg.Text).Trim() != "")
                dfltaexlibilty = Convert.ToDouble(txtFlatElg.Text);

            //if (dtxtactchrs > dfltaexlibilty)
            //{
            //    txtFlatChg.Text = Convert.ToString(dfltaexlibilty);
            //}

        }
        #endregion


    }

    private Boolean txtCheck_CorrectAmtount(TextBox txtAmt)
    {
        Boolean blnchk = false;
        if (Convert.ToString(txtAmt.Text).Trim() != "")
        {
            if (Convert.ToString(txtAmt.Text).Trim() != "0.00" || Convert.ToString(txtAmt.Text).Trim() != "0")
            {
                if (Convert.ToString(txtAmt.Text).Trim() == "")
                {
                    lblmessage.Text = "Please enter the Amount";
                    blnchk = true;
                }
                //Decimal dfare = 0;
                //dfare = Convert.ToDecimal(txtAmt.Text);
                //if (dfare == 0)
                //{
                //    lblmessage.Text = "Please enter correct Charges";
                //    blnchk = true;
                //}
            }
            if (Convert.ToString(txtAmt.Text).Trim() != "")
            {
                string[] strdate;
                strdate = Convert.ToString(txtAmt.Text).Trim().Split('.');
                if (strdate.Length > 2)
                {
                    txtAmt.Text = "0";
                    lblmessage.Text = "Please enter correct Amount.";
                    blnchk = true;
                }
            }
        }

        return blnchk;
    }
    protected void txtaddintionalExpens_TextChanged(object sender, EventArgs e)
    {
        if (rdoFoodAccomodation.Checked == true)
        {
            if (txtCheck_CorrectAmtount(txtFlatChg) == false)
                getDeviation_For_Geuest_Hose_withoutFood();
        }
        if (rdoOwnArgmnet.Checked == true)
        {
            txtCheck_CorrectAmtount(txtaddintionalExpens);
        }
        if (rdoFood.Checked == true)
        {
            getdeviation_Guesthouse_wihtFood();
        }
    }


    #endregion

    #region PageMethods

    public void get_Actual_Flats_rates()
    {
      
            txtEligibility.Text = "0";
            hdnactualEligbility.Value = "0";        
            txtFlatElg.Text = "0";
            hdnflatEligbility.Value = "0";
        
        

        string[] strdate;
        string strfromDate = "";
        string strToDate = "";
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

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[7];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_flat_actual_rate_Accomdation";

        spars[1] = new SqlParameter("@cityname", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtLocation.Text);//lstLocation.SelectedValue; 

        spars[2] = new SqlParameter("@exp_trvl", SqlDbType.VarChar);
        if (rdoAccomodation.Checked == true)
            spars[2].Value = "Hotel";
        else if (rdoFoodAccomodation.Checked == true)
            spars[2].Value = "Guest House";
        else if (rdoOwnArgmnet.Checked == true)
            spars[2].Value = "Own Arrangement";

        //if (rdoAccomodation.Checked == true)
        //    spars[2].Value = "Guest House";
        //else if (rdoFood.Checked == true)
        //    spars[2].Value = "Hotel";
        //else
        //    spars[2].Value = DBNull.Value;

        //spars[2].Value = Convert.ToString(txtAcctype.Text);

        spars[3] = new SqlParameter("@grade", SqlDbType.VarChar);
        spars[3].Value = Convert.ToString(Session["EMPAGrade"]).Trim();
        //spars[3].Value = Convert.ToString(Session["Grade"]).Trim();
        

        spars[4] = new SqlParameter("@exp_trvl_t", SqlDbType.VarChar);
        if (rdoAccomodation.Checked == true)
            spars[4].Value = "Hotel";
        else if (rdoFoodAccomodation.Checked == true)
            spars[4].Value = "Guest House";
        else if (rdoOwnArgmnet.Checked == true)
            spars[4].Value = "Own Arrangement";


        spars[5] = new SqlParameter("@FromDate", SqlDbType.VarChar);
        spars[5].Value = strfromDate;


        spars[6] = new SqlParameter("@ToDate", SqlDbType.VarChar);
        spars[6].Value =strToDate;

        
        dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");
        Decimal dAmount = 0;
        if (dsTrDetails.Tables[1].Rows.Count > 0)
        {
            if (Convert.ToString(dsTrDetails.Tables[1].Rows[0]["Amount"]).Trim() != "")
                dAmount = Convert.ToDecimal(dsTrDetails.Tables[1].Rows[0]["Amount"]);

            txtFlatElg.Text = Convert.ToString(dAmount).Trim();
            hdnflatEligbility.Value = Convert.ToString(dAmount).Trim();

            if(Convert.ToString(txtnoofDays.Text).Trim()!="" &&  Convert.ToString(txtnoofDays.Text).Trim()!="0" && Convert.ToString(txtnoofDays.Text).Trim()!="0.00" )
            {
                txtFlatElg.Text = Convert.ToString(Math.Round(dAmount * Convert.ToDecimal(txtnoofDays.Text), 2)).Trim();
                hdnflatEligbility.Value = Convert.ToString(Math.Round(dAmount * Convert.ToDecimal(txtnoofDays.Text), 2)).Trim();
            }
             
        }
        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            dAmount = 0;
            if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Amount"]).Trim() != "")
                dAmount = Convert.ToDecimal(dsTrDetails.Tables[0].Rows[0]["Amount"]);

            txtEligibility.Text = Convert.ToString(dAmount).Trim();
            hdnactualEligbility.Value = Convert.ToString(dAmount).Trim();

            //txtEligibility.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Amount"]).Trim();
            //hdnactualEligbility.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Amount"]).Trim();

            if (Convert.ToString(txtnoofDays.Text).Trim() != "" && Convert.ToString(txtnoofDays.Text).Trim() != "0" && Convert.ToString(txtnoofDays.Text).Trim() != "0.00")
            {
                txtEligibility.Text = Convert.ToString(Math.Round(dAmount * Convert.ToDecimal(txtnoofDays.Text),2)).Trim();
                hdnactualEligbility.Value = Convert.ToString(Math.Round(dAmount * Convert.ToDecimal(txtnoofDays.Text), 2)).Trim();
            }

        }

        if(Convert.ToString(txtEligibility.Text).Trim()=="")
        {
            txtEligibility.Text = "0";
            hdnactualEligbility.Value = "0";
        }
        if (Convert.ToString(txtFlatElg.Text).Trim() == "")
        {
            txtFlatElg.Text = "0";
            hdnflatEligbility.Value = "0";
        }
        

         
    }

    public void getAccomodationDetails()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetExpensesAccomodationDetails(txtEmpCode.Text);

        if (dtTrDetails.Rows.Count > 0)
        {
            txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["From Date"]);
            txtToDate.Text = Convert.ToString(dtTrDetails.Rows[0]["To Date"]);
            txtRemarks.Text = Convert.ToString(dtTrDetails.Rows[0]["local_accomodation_remarks"]);
            txtLocation.Text = Convert.ToString(dtTrDetails.Rows[0]["Location"]);
            lstLocation.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["locationid"]);
            //hdnCOS.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_through_desk"]);
            
            //if (Convert.ToString(hdnCOS.Value).Trim() != "Yes")
            //{
            //    chkCos.Checked = false;
            //}
            //else
            //{
            //    chkCos.Checked = true;
            //}

        }
    }
    public void GetCities()
    {
        DataTable dtCities = new DataTable();
        dtCities = spm.getCitiesDetails();
        if (dtCities.Rows.Count > 0)
        {
            lstLocation.DataSource = dtCities;
            lstLocation.DataTextField = "CITYNAME";
            lstLocation.DataValueField = "CITYID";
            lstLocation.DataBind();

        }
    }
    public void getAccDetailsEdit()
    {
        DataTable dtTrDetails = new DataTable();

        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_gettmp_ExpAccomodation_dtlsEdit";

        spars[1] = new SqlParameter("@trip_id", SqlDbType.Decimal);
        spars[1].Value = hdnTripid.Value;

        spars[2] = new SqlParameter("@trip_dtls_id", SqlDbType.Decimal);
        spars[2].Value = hdnAccdtlsid.Value;

        spars[3] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[3].Value = Convert.ToString(txtEmpCode.Text).Trim();
        dtTrDetails = spm.getDataList(spars, "SP_GETALL_Expense_DETAILS");

       // dtTrDetails = spm.GetAccomodationDetailsEdit(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnAccdtlsid.Value));
        if (dtTrDetails.Rows.Count > 0)
        { 
            txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["From Date"]);
            txtToDate.Text = Convert.ToString(dtTrDetails.Rows[0]["To Date"]);
            txtRemarks.Text = Convert.ToString(dtTrDetails.Rows[0]["local_accomodation_remarks"]);
            txtLocation.Text = Convert.ToString(dtTrDetails.Rows[0]["Location"]);
            hdnCOS.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_through_desk"]);

            txtPaidBy.Text = Convert.ToString(dtTrDetails.Rows[0]["ActualPaid_by"]);
            lstPaidBy.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["ActualPaid_by"]);
            txtCharges.Text = Convert.ToString(dtTrDetails.Rows[0]["ActualCharges"]);
            hdntripcharges.Value = Convert.ToString(dtTrDetails.Rows[0]["ActualCharges"]);
            txtDeviation.Text = Convert.ToString(dtTrDetails.Rows[0]["ActualDeviation"]);
            txtEligibility.Text = Convert.ToString(dtTrDetails.Rows[0]["ActualEligibility"]);

            txtFlatPaid.Text = Convert.ToString(dtTrDetails.Rows[0]["FLatPaid_by"]);
            lstFlatPaid.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["FLatPaid_by"]);

            txtFlatChg.Text = Convert.ToString(dtTrDetails.Rows[0]["FlatCharges"]);
            txtFlatDev.Text = Convert.ToString(dtTrDetails.Rows[0]["FLatDeviation"]);
            txtFlatElg.Text = Convert.ToString(dtTrDetails.Rows[0]["FlatEligibility"]);

            txtAcctype.Text = Convert.ToString(dtTrDetails.Rows[0]["Accomodation_type"]);
            lstAccType.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["Accomodation_type"]);
            lstLocation.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["locationid"]);
            hdnIsThrughCOS.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_through_desk"]);

            txtAdditionalFoodExp_emp.Text = Convert.ToString(dtTrDetails.Rows[0]["addtn_food_exp_paidby"]);
            txtAdditionalFoodExp_exp.Text = Convert.ToString(dtTrDetails.Rows[0]["addtn_food_exp_charges"]);
            hdnAccomodationStatus.Value = Convert.ToString(dtTrDetails.Rows[0]["bookedstatus"]);
            txtaddintionalExpens.Text = Convert.ToString(dtTrDetails.Rows[0]["additnal_flat_exps"]);

            txtAdditional_exp_deviation.Text = Convert.ToString(dtTrDetails.Rows[0]["add_exp_deviation"]);
            txtnoofDays.Text = Convert.ToString(dtTrDetails.Rows[0]["no_of_days"]);
            
            ////rdoAccomodation= false;
            //rdoFood.Enabled = false;
            //rdoFoodAccomodation.Enabled = false;
            //rdoOwnArgmnet.Enabled = false;

            #region set Radio Buttons 
            if (Convert.ToString(dtTrDetails.Rows[0]["Accomodation_type"]).Trim() == "Hotel")
            {
                rdoAccomodation.Checked = true;
                rdoFood.Checked = false;
                rdoFoodAccomodation.Checked = false;
                rdoOwnArgmnet.Checked = false;    
            
            }
            if (Convert.ToString(dtTrDetails.Rows[0]["Accomodation_type"]).Trim() == "Guest House (with Food)")
            {
                rdoFood.Checked = true;
                rdoAccomodation.Checked = false;
                rdoFoodAccomodation.Checked = false;
                rdoOwnArgmnet.Checked = false;
            }
            if (Convert.ToString(dtTrDetails.Rows[0]["Accomodation_type"]).Trim() == "Guest House (without Food)")
            {
                rdoFoodAccomodation.Checked = true;
                rdoFood.Checked = false;
                rdoAccomodation.Checked = false;                
                rdoOwnArgmnet.Checked = false;
            }
            if (Convert.ToString(dtTrDetails.Rows[0]["Accomodation_type"]).Trim() == "Own Arrangement")
            {
                rdoOwnArgmnet.Checked = true;
                rdoFoodAccomodation.Checked = false;
                rdoFood.Checked = false;
                rdoAccomodation.Checked = false;                
            }

            #endregion
            if (Convert.ToString(hdnTripid.Value).Trim() != "" && Convert.ToString(hdnTripid.Value).Trim() != "0")
            {
                //bookedstatus
                if (Convert.ToString(dtTrDetails.Rows[0]["bookedstatus"]) == "Booked")
                {
                    txtPaidBy.Text = "Company";
                    lstPaidBy.SelectedValue = "Company";
                   // txtAcctype.Text = "Hotel";
                   // lstAccType.SelectedValue = "Hotel";
                    txtPaidBy.Enabled = false;
                    txtCharges.Enabled = false;

                    Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                    txtTravelType.BackColor = color;
                      txtAcctype.Enabled = false;
                     txtAcctype.BackColor = color;
                     get_Actual_Flats_rates();
                     txtCalculateAmoutns();
                    
                }
                else
                {
                    txtPaidBy.Text = "Employee";
                    lstPaidBy.SelectedValue = "Employee";
                    //txtAcctype.Text = "Guest House";
                    //lstAccType.SelectedValue = "Guest House";
                   // txtAcctype.Text = "Hotel";
                   // lstAccType.SelectedValue = "Hotel";
                    txtPaidBy.Enabled = true;
                    txtCharges.Enabled = true;
                    get_Actual_Flats_rates();
                    txtCalculateAmoutns();

                }

                 
            }

            if (Convert.ToString(txtEligibility.Text).Trim() == "")
            {
                txtEligibility.Text = "0";
                hdnactualEligbility.Value = "0";
            }
            if (Convert.ToString(txtFlatElg.Text).Trim() == "")
            {
                txtFlatElg.Text = "0";
                hdnflatEligbility.Value = "0";
            }
             
        }
    }

    public void get_Actual_DayDiff()
    {

         String [] strdate ;
         strdate= Convert.ToString(txtnoofDays.Text).Trim().Split('.');
        if (strdate.Length > 2)
        {
            txtnoofDays.Text = "0";
            lblmessage.Text = "Please enter correct Actual stay duration (in days)";
            return;
        }
        if (Convert.ToString(txtnoofDays.Text).Trim() != "")
        {
            if (Convert.ToString(txtnoofDays.Text).Trim() != "0.00" || Convert.ToString(txtnoofDays.Text).Trim() != "0")
            {
                if (Convert.ToString(txtnoofDays.Text).Trim() == "")
                {
                    lblmessage.Text = "Please enter Actual stay duration (in days)";
                    return;
                }
                Decimal dfare = 0;
                dfare = Convert.ToDecimal(txtnoofDays.Text);
                if (dfare == 0)
                {
                    lblmessage.Text = "Please enter Actual stay duration (in days)";
                    return;
                }
            }
        }

       
        string strfromDate = "";
        string strToDate = "";
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

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_day_diff";

        spars[1] = new SqlParameter("@FromDate", SqlDbType.VarChar);
        spars[1].Value = strfromDate;

        spars[2] = new SqlParameter("@ToDate", SqlDbType.VarChar);
        spars[2].Value = strToDate;

        dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            hdnDaysDiff.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["DayDiff"]);
            if (Convert.ToString(txtnoofDays.Text).Trim() != "")
            {
                Decimal dnodays = Convert.ToDecimal(txtnoofDays.Text);
                Decimal dActnodays = Convert.ToDecimal(hdnDaysDiff.Value);
                if (dnodays > dActnodays)
                {
                    txtnoofDays.Text = Convert.ToString(dActnodays);
                }
            }

        }

        get_Actual_Flats_rates();
    }

    private void setControlVisibility()
    {
        if (Convert.ToString(hdnAccdtlsid.Value).Trim()=="0")
        {
            hdnIsThrughCOS.Value = "No";
            hdnAccomodationStatus.Value = "Not Booked";
        }
        txtAdditionalFoodExp_emp.Text = "Employee";
        
        liAccomdation_charges_1.Visible = false;
        liAccomdation_charges_2.Visible = false;
        liAccomdation_charges_paidby.Visible = false;
        liAccomdation_charges_charges.Visible = false;

        //Additional Expenses
        liAddintional_exps_1.Visible = false;
        liAddintional_exps_2.Visible = false;
        liAddintional_exps_flatpaid_1.Visible = false;
        liAddintional_exps_flatpaid_2.Visible = false;
        liAddintional_exps_Charges.Visible = false;
        liAddintional_exps_eligibility.Visible = false;
        liAddintional_exps_deviation_1.Visible = false;
        liAddintional_exps_deviation_2.Visible = false;


        //Accommodation Charges
        liAccomdation_charges_1.Visible = false;
        liAccomdation_charges_2.Visible = false;
        liAccomdation_charges_paidby.Visible = false;
        liAccomdation_charges_charges.Visible = false;

        //Additional Food Expenses
        liAccomdation_Food_exp_1.Visible = false;
        liAccomdation_Food_exp_2.Visible = false;
        liAccomdation_Food_paidby.Visible = false;
        liAccomdation_Food_Charges.Visible = false;
        liAccomdation_Food_Deviation.Visible = false;
        liAccomdation_Food_Eligibility.Visible = false;
     
        //Additional Expenses
        liAddintional_exps_Charges.Visible = false;
        liAddintional_exps_eligibility.Visible = false;

       #region if Book through COS Yes & Booked
        if (Convert.ToString(hdnIsThrughCOS.Value) == "Yes" &&  Convert.ToString(hdnAccomodationStatus.Value).Trim() == "Booked"  )
        {
            spnArrgment.Visible = false;            
            rdoAccomodation.Visible = false;
            rdoFoodAccomodation.Visible = false;
            rdoFood.Visible = false;
            rdoOwnArgmnet.Visible = false;

            //spnArrgment_1.Visible = true;
            //lblAccmmodationType.Visible = true;
            //spnArrgment_2.Visible = true;
            txtAcctype.Visible = true;
            spnArrgment_accmodation.Visible = true;
            spnArrgment_2.Visible = true;

            if (rdoAccomodation.Checked == true)
            {
                //Accommodation Charges
                liAccomdation_charges_1.Visible = true;
                liAccomdation_charges_2.Visible = true;
                liAccomdation_charges_paidby.Visible = true;
                liAccomdation_charges_charges.Visible = true;
                txtCharges.Enabled = false;

                //Additional Food Expenses
                liAccomdation_Food_exp_1.Visible = false;
                liAccomdation_Food_exp_2.Visible = false;
                liAccomdation_Food_paidby.Visible = true;
                liAccomdation_Food_Charges.Visible = true;
                liAccomdation_Food_Deviation.Visible = true;
                liAccomdation_Food_Eligibility.Visible = true;

                
            }
           // if (Convert.ToString(txtAcctype.Text).Trim() == "Guest House (without Food)")
            if (rdoFoodAccomodation.Checked == true)
            {
               //Additional Expenses
                liAddintional_exps_flatpaid_1.Visible = true;
                liAddintional_exps_flatpaid_2.Visible = true;
                
                liAddintional_exps_1.Visible=true;
                liAddintional_exps_2.Visible = true;
                liAddintional_exps_Charges.Visible = true;
                liAddintional_exps_eligibility.Visible = true;
               liAddintional_exps_deviation_1.Visible = true;
               liAddintional_exps_deviation_2.Visible = true;
               
                getDeviation_For_Geuest_Hose_withoutFood();
            }

           // if (Convert.ToString(txtAcctype.Text).Trim() == "Guest House (with Food)")
            if (rdoFood.Checked == true)
            {
                liAddintional_exps_flatpaid_1.Visible = true;
                liAddintional_exps_flatpaid_2.Visible = true;

                liAddintional_exps_1.Visible = true;
                liAddintional_exps_2.Visible = true;
                liAddintional_exps_deviation_1.Visible = true;
                liAddintional_exps_deviation_2.Visible = true;                
                txtAdditional_exp_deviation.Text = "No";
            }
        }
        #endregion

       #region Booked through COS Yes & Not Booked
        if ((Convert.ToString(hdnIsThrughCOS.Value) == "Yes" && Convert.ToString(hdnAccomodationStatus.Value).Trim() == "Not Booked") || Convert.ToString(hdnIsThrughCOS.Value) == "No")
        {
            //if (Convert.ToString(txtAcctype.Text).Trim() == "Hotel")
            spnArrgment_3.Visible = true;
            if (rdoAccomodation.Checked == true)
            {
                //Accommodation Charges
                liAccomdation_charges_1.Visible = true;
                liAccomdation_charges_2.Visible = true;               
                liAccomdation_charges_paidby.Visible = true;
                liAccomdation_charges_charges.Visible = true;
                liAccomdation_Food_Deviation.Visible = true;
                liAccomdation_Food_Eligibility.Visible = true;

                
                txtPaidBy.Text = "Employee";
                lstPaidBy.SelectedValue = "Employee";
                Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                txtPaidBy.BackColor = color;
                txtPaidBy.Enabled = false;
                txtPaidBy.BackColor = color;

            }
            //if (Convert.ToString(txtAcctype.Text).Trim() == "Guest House (without Food)")
            if (rdoFoodAccomodation.Checked == true)
            {
                //Additional Expenses    
                //liAddintional_exps_flatpaid_1.Visible = true;
                //liAddintional_exps_flatpaid_2.Visible = true;

                liAddintional_exps_1.Visible = true;
                liAddintional_exps_2.Visible = true;
                liAddintional_exps_Charges.Visible = true;
                liAddintional_exps_eligibility.Visible = true;
                liAddintional_exps_deviation_1.Visible = true;
                liAddintional_exps_deviation_2.Visible = true;
            }
            //if (Convert.ToString(txtAcctype.Text).Trim() == "Guest House (with Food)")
            if (rdoFood.Checked == true)
            {
                //Additional Expenses  
                liAddintional_exps_flatpaid_1.Visible = true;
                liAddintional_exps_flatpaid_2.Visible = true;
                //liAddintional_exps_Charges.Visible = true;
                //liAddintional_exps_eligibility.Visible = true;

               // liAddintional_exps_1.Visible = true;
                //liAddintional_exps_2.Visible = true;
                //liAddintional_exps_deviation_1.Visible = true;
                // liAddintional_exps_deviation_2.Visible = true;
                txtAdditional_exp_deviation.Text = "No";
            }

            //if (Convert.ToString(txtAcctype.Text).Trim() == "Own Arrangement")
            if (rdoOwnArgmnet.Checked == true)
            {
                //Additional Expenses    
                //liAddintional_exps_flatpaid_1.Visible = true;
                //liAddintional_exps_flatpaid_2.Visible = true;

                liAddintional_exps_1.Visible = true;
                liAddintional_exps_2.Visible = true;
                liAddintional_exps_Charges.Visible = true;                
                liAddintional_exps_eligibility.Visible = true;               

                txtAdditional_exp_deviation.Text = "No";
            }

        }

       #endregion

       #region if Expenses Accommodation after Travel i.e  Expenses without Travel
        if (Convert.ToString(hdnTripid.Value).Trim() == "0" || Convert.ToString(hdnIsThrughCOS.Value).Trim() =="")
                {
                    spnArrgment_3.Visible = true;
                    if (rdoAccomodation.Checked == true)
                    {
                        //Accommodation Charges
                        liAccomdation_charges_1.Visible = true;
                        liAccomdation_charges_2.Visible = true;
                        liAccomdation_charges_paidby.Visible = true;
                        liAccomdation_charges_charges.Visible = true;
                        liAccomdation_Food_Deviation.Visible = true;
                        liAccomdation_Food_Eligibility.Visible = true;


                        txtPaidBy.Text = "Employee";
                        lstPaidBy.SelectedValue = "Employee";
                        Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                        txtPaidBy.BackColor = color;
                        txtPaidBy.Enabled = false;
                        txtPaidBy.BackColor = color;

                    }
                    //if (Convert.ToString(txtAcctype.Text).Trim() == "Guest House (without Food)")
                    if (rdoFoodAccomodation.Checked == true)
                    {
                        //Additional Expenses    
                        //liAddintional_exps_flatpaid_1.Visible = true;
                        liAddintional_exps_flatpaid_2.Visible = false;

                        liAddintional_exps_1.Visible = true;
                        liAddintional_exps_2.Visible = true;
                        liAddintional_exps_Charges.Visible = true;
                        liAddintional_exps_eligibility.Visible = true;
                        liAddintional_exps_deviation_1.Visible = true;
                        liAddintional_exps_deviation_2.Visible = true;
                    }
                    //if (Convert.ToString(txtAcctype.Text).Trim() == "Guest House (with Food)")
                    if (rdoFood.Checked == true)
                    {
                        //Additional Expenses  
                        liAddintional_exps_flatpaid_1.Visible = true;
                        liAddintional_exps_flatpaid_2.Visible = true;
                        //liAddintional_exps_Charges.Visible = true;
                        //liAddintional_exps_eligibility.Visible = true;

                        // liAddintional_exps_1.Visible = true;
                        //liAddintional_exps_2.Visible = true;
                        //liAddintional_exps_deviation_1.Visible = true;
                        // liAddintional_exps_deviation_2.Visible = true;
                        txtAdditional_exp_deviation.Text = "No";
                    }

                    //if (Convert.ToString(txtAcctype.Text).Trim() == "Own Arrangement")
                    if (rdoOwnArgmnet.Checked == true)
                    {
                        //Additional Expenses    
                        //liAddintional_exps_flatpaid_1.Visible = true;
                        //liAddintional_exps_flatpaid_2.Visible = true;

                        liAddintional_exps_1.Visible = true;
                        liAddintional_exps_2.Visible = true;
                        liAddintional_exps_Charges.Visible = true;
                        liAddintional_exps_eligibility.Visible = true;
                        txtAdditional_exp_deviation.Text = "No";
                    }

                }
        #endregion
    }

    private void getdeviation_Guesthouse_wihtFood()
    {
        if (rdoFood.Checked == true)
        {

            txtAdditional_exp_deviation.Text = "No";
            if (txtCheck_CorrectAmtount(txtaddintionalExpens) == false)
            {
                if (Convert.ToString(txtaddintionalExpens.Text).Trim() != "")
                {
                    if (Convert.ToDecimal(txtaddintionalExpens.Text) > 0)
                        txtAdditional_exp_deviation.Text = "Yes";
                }
            }
            else
            {
                return;
            }
            if (txtCheck_CorrectAmtount(txtFlatChg) == false)
            {
                if (Convert.ToString(txtFlatChg.Text).Trim() != "")
                {
                    if (Convert.ToDecimal(txtFlatChg.Text) > 0)
                        txtAdditional_exp_deviation.Text = "Yes";
                }
            }
            else
            {
                return;
            }

        }
    }

    protected void DateValidations()
    {
        if (Convert.ToString(txtFromdate.Text).Trim() == "")
            return;

        if (Convert.ToString(txtToDate.Text).Trim() == "")
            return;


        string[] tr_strdate;
        string tr_strfromDate = "";
        string tr_strToDate = "";

        #region Exp.TraelRequestdate formatting

        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            tr_strdate = Convert.ToString(hdnfromdate.Value).Trim().Split('/');
            tr_strfromDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
        }
        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            tr_strdate = Convert.ToString(hdnTodate.Value).Trim().Split('/');
            tr_strToDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
        }

        #endregion

        lblmessage.Text = "";
        string message = "";
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";

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
        DataSet dttraveletails = new DataSet();
        if (Convert.ToString(txtFromdate.Text).Trim() != "" && Convert.ToString(txtToDate.Text).Trim() != "")
        {
            dttraveletails = spm.Get_ExpTravel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value, "ExpAccomodatins");

            if (dttraveletails.Tables[1].Rows.Count > 0)
            {
                message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);

            }
            hdnactualdays.Value = Convert.ToString(dttraveletails.Tables[0].Rows[0]["actualAccDays"]);

            if (Convert.ToString(message).Trim() != "")
            {


                lblmessage.Text = Convert.ToString(message).Trim();
                lblmessage.Visible = true;
                txtFromdate.Text = "";
                txtToDate.Text = "";
            }

            if (Convert.ToString(hdnactualdays.Value).Trim() != "")
                txtCalculateAmoutns();

        }

    }

    private void txtCalculateAmoutns()
    {
        Double dactexlibilty = 0;
        Double dfltaexlibilty = 0;
        Double dtxtactchrs = 0;
        Double dtxtflatchrs = 0;

        if (txtCheck_CorrectAmtount(txtCharges) == true)
        {
            return;
        }
        if (txtCheck_CorrectAmtount(txtAdditionalFoodExp_exp) == true)
        {
            return;
        }
        if (txtCheck_CorrectAmtount(txtFlatChg) == true)
        {
            return;
        }


        txtFlatDev.Text = "No";

        if (Convert.ToString(hdnactualdays.Value).Trim() != "")
        {
            if (Convert.ToString(hdnactualEligbility.Value).Trim() != "")
                dactexlibilty = Convert.ToDouble(hdnactualEligbility.Value);

            txtEligibility.Text = Convert.ToString(dactexlibilty);

            if (Convert.ToString(hdnflatEligbility.Value).Trim() != "")
                dfltaexlibilty = Convert.ToDouble(hdnflatEligbility.Value);

            txtFlatElg.Text = Convert.ToString(dfltaexlibilty);

        }

        if (Convert.ToString(txtCharges.Text).Trim() != "")
            dtxtactchrs = Convert.ToDouble(txtCharges.Text);

        if (Convert.ToString(txtAdditionalFoodExp_exp.Text).Trim() != "")
            dtxtactchrs += Convert.ToDouble(txtAdditionalFoodExp_exp.Text);



        if (Convert.ToString(txtEligibility.Text).Trim() != "")
            dactexlibilty = Convert.ToDouble(txtEligibility.Text);

        if (Convert.ToString(txtFlatElg.Text).Trim() != "")
            dfltaexlibilty = Convert.ToDouble(txtFlatElg.Text);

        if (Convert.ToString(txtFlatChg.Text).Trim() != "")
            dtxtflatchrs = Convert.ToDouble(txtFlatChg.Text);




        if ((dtxtactchrs - dactexlibilty) > 0)
            txtDeviation.Text = "Yes";
        else
            txtDeviation.Text = "No";
    }

    private void getDeviation_For_Geuest_Hose_withoutFood()
    {
        Double dactexlibilty = 0;
        Double dtxtactchrs = 0;
        Double dtxtaddexp = 0;

        if (txtCheck_CorrectAmtount(txtFlatElg) == false)
        {
            if (Convert.ToString(txtFlatElg.Text).Trim() != "")
                dactexlibilty = Convert.ToDouble(txtFlatElg.Text);
        }
        else
        {
            return;
        }

        if (txtCheck_CorrectAmtount(txtaddintionalExpens) == false)
        {
            if (Convert.ToString(txtaddintionalExpens.Text).Trim() != "")
                dtxtaddexp = Convert.ToDouble(txtaddintionalExpens.Text);
        }
        else
        {
            return;
        }

        if (txtCheck_CorrectAmtount(txtFlatChg) == false)
        {
            if (Convert.ToString(txtFlatChg.Text).Trim() != "")
                dtxtactchrs = Convert.ToDouble(txtFlatChg.Text);
        }
        else
        {
            return;
        }
        if (((dtxtactchrs + dtxtaddexp) - dactexlibilty) > 0)
            //  if (Convert.ToString(txtFlatChg.Text).Trim() != "" && Convert.ToString(txtFlatChg.Text).Trim()!="0")
            txtAdditional_exp_deviation.Text = "Yes";
        else
            txtAdditional_exp_deviation.Text = "No";
    }

    #endregion

       

    #region Search Location
    //[System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [System.Web.Services.WebMethod]
    public static List<string> SearchCities(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";

                string strtest = Convert.ToString(System.Web.HttpContext.Current.Session["TripTypeId"]);
                if (Convert.ToString(strtest).Trim() == "1")

                    strsql = " select top 10 CITYNAME from TBL_CITYMASTER where " +
                                "  CITYNAME like   @SearchText + '%' order by CITYNAME ";
                else
                    strsql = " select top 10 CITYNAME from tbl_countriesMaster where  " +
                                "  CITYNAME like   @SearchText + '%' order by CITYNAME ";
                cmd.CommandText = strsql;
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["CITYNAME"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
    #endregion


     
     
   

    
}
