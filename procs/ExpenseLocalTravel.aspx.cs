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

public partial class ExpenseLocalTravel : System.Web.UI.Page
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
                if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
                }

                lblmessage.Text = "";

                if (Convert.ToString(Session["Empcode"]).Trim() == "")
                    Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

                lblmsg.Visible = false;
                if (Page.User.Identity.IsAuthenticated == false)
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/travelindex");
                }
                else
                {
                    Page.SmartNavigation = true;
                    if (!Page.IsPostBack)
                    {
                        txtFromdate_Locl.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                        txtToDate_Locl.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                       // txtLocation_Locl.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                        txtTravelMode_Locl.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                        txtCharges_Locl.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");

                        txtFromdate_Locl.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
                        txtToDate_Locl.Attributes.Add("onkeyDown", "return noanyCharecters(event);");


                        editform.Visible = true;
                        divbtn.Visible = false;
                         

                        //txtToDate_Locl.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                        //txtFromdate_Locl.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                        txtEmpCode_Locl.Text = Convert.ToString(Session["ReqEmpCode"]);
                        txtTravelType_Locl.Text = Convert.ToString(Session["TravelType"]);
                        hdnTripid.Value = Convert.ToString(Session["TripID"]);
                        hdnfromdate_Locl.Value = Convert.ToString(Session["Fromdate"]);
                        hdnTodate_Locl.Value = Convert.ToString(Session["Todate"]);
                        hdnTryiptypeid.Value = Convert.ToString(Session["Tr_type_id"]);
                        hflGrade_Locl.Value = Convert.ToString(Session["Grade"]);
                        GetTravelMode();
                       // GetCities();

                        if (Request.QueryString.Count > 0)
                        {
                            hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                            hdntrdetailsid_Locl.Value = Convert.ToString(Request.QueryString[1]).Trim();
                            hdnexp_id.Value = Convert.ToString(Request.QueryString[2]).Trim();
                        }
                        if (Convert.ToString(hdntrdetailsid_Locl.Value).Trim() == "0" || Convert.ToString(hdntrdetailsid_Locl.Value).Trim() == "")
                        {
                            localtrvl_delete_btn.Visible = false;
                        }
                        getLocaTravelDetails_edit();

                        if (Convert.ToString(hdntrdetailsid_Locl.Value).Trim() != "0" && Convert.ToString(hdnDesk_Locl.Value).Trim()=="Yes")
                        {
                            if (Convert.ToString(hdnTripid.Value).Trim() != "0" && Convert.ToString(hdnTripid.Value).Trim() != "")
                            {
                                Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                                txtTravelType_Locl.BackColor = color;
                                localtrvl_delete_btn.Visible = false;
                                txtFromdate_Locl.Enabled = false;
                                txtToDate_Locl.Enabled = false;
                               // txtRemarks_Locl.Enabled = false;
                                txtLocation_Locl.Enabled = false;
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

        protected void localtrvl_btnSave_Click(object sender, EventArgs e)
        {
            string[] strdate;
            #region Check All Fields Blank
            //  lblmessage.Text = "";
            if(Convert.ToString(txtTravelMode_Locl.Text).Trim()=="")
            {
                lblmessage.Text = "Please select Mode of Travel";
                return;
            }
            if (Convert.ToString(txtFromdate_Locl.Text).Trim() == "")
            {
                lblmessage.Text = "Please select From Date";
                return;
            }
            if (Convert.ToString(txtToDate_Locl.Text).Trim() == "")
            {
                lblmessage.Text = "Please select To Date";
                return;
            }
            if (Convert.ToString(txtLocation_Locl.Text).Trim() == "")
            {
                lblmessage.Text = "Please Enter Location For Travel";
                return;
            }
            //if (Check_Cities_name(txtLocation_Locl.Text) == false)
            //{
            //    lblmessage.Text = "Please Select Correct Location.";
            //    return;
            //}



            if (Convert.ToString(txtCharges_Locl.Text).Trim() == "0" || Convert.ToString(txtCharges_Locl.Text).Trim() == "" || Convert.ToString(txtCharges_Locl.Text).Trim() == "0.00")
            {
                if (Convert.ToString(txtRemarks_Locl.Text).Trim() == "")
                {
                    lblmessage.Text = "Please enter Remarks.";
                    return;
                }
            }
            if (Convert.ToString(txtCharges_Locl.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtCharges_Locl.Text).Trim().Split('.');
                if (strdate.Length > 2)
                {
                    txtCharges_Locl.Text = "0";
                    lblmessage.Text = "Please enter correct Charges.";
                    return;
                }
                //Decimal dfare = 0;
                //dfare = Convert.ToDecimal(txtCharges_Locl.Text);
                //if (dfare == 0)
                //{
                //    lblmessage.Text = "Please enter correct Charges.";
                //    return;
                //}
            }

            #endregion


            if (Convert.ToString(Session["chkTrvlAccLocalTrvlbtnStatus"]).Trim() != "")
            {
                Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value);
            }
            Session["chkTrvlAccLocalTrvlbtnStatus"] = "Local Expenses button Event is Submitted";

            Decimal dCharges = 0;
            if (Convert.ToString(txtCharges_Locl.Text).Trim() != "")
                dCharges = Math.Round(Convert.ToDecimal(txtCharges_Locl.Text), 2);

            string strfromDate = "";
            string strToDate = "";
            #region date formatting
            if (Convert.ToString(txtFromdate_Locl.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate_Locl.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            if (Convert.ToString(txtToDate_Locl.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtToDate_Locl.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }

            #endregion

            SqlParameter[] spars = new SqlParameter[19];

            //spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            //spars[0].Value = "Inserttemptable";
            string strsptype = "insert";
            if (Convert.ToString(localtrvl_btnSave.Text).Trim() == "Update")
            {
                strsptype = "update";
            }
            hdntrmodeid.Value = lstTravelMode_Locl.SelectedValue;
            spm.InsertExpenseLocalTrvlDetails(Convert.ToInt32(hdnTryiptypeid.Value), strfromDate, strToDate, txtLocation_Locl.Text, txtEmpCode_Locl.Text, txtRemarks_Locl.Text, strsptype, Convert.ToInt32(hdntrmodeid.Value), Convert.ToString(hdnDeviation_Locl.Value).Trim(), dCharges, Convert.ToDecimal(hdntrdetailsid_Locl.Value), Convert.ToDecimal(hdnTripid.Value));
                Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid="+hdnTripid.Value +"&expid="+hdnexp_id.Value);
               // Response.Redirect("~/procs/ExpenseWithoutTravlReqst.aspx");
            //}
            //else
            //{
            //    //spm.UpdateLocalTrvlDetails(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnlocalId.Value), strfromDate, strToDate, txtLocation_Locl.Text, hdnCOS_Locl.Value, txtRemarks_Locl.Text, "UpdateMainTable");
            //    //Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value);
            //   // Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
            //}
        }

        protected void localtrvl_cancel_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("TravelRequest_exp.aspx?tripid="+hdnTripid.Value+"&expid="+hdnexp_id.Value);
           // Response.Redirect("TravelRequest.aspx");
        }

        protected void localtrvl_delete_btn_Click(object sender, EventArgs e)
        {
            string[] strdate;
            string strfromDate = "";
            string strToDate = "";
            #region date formatting
            if (Convert.ToString(txtFromdate_Locl.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate_Locl.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            if (Convert.ToString(txtToDate_Locl.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtToDate_Locl.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }

            #endregion

            string strsptype = "delete";
           
            hdntrmodeid.Value = lstTravelMode_Locl.SelectedValue;
            spm.InsertExpenseLocalTrvlDetails(Convert.ToInt32(hdnTryiptypeid.Value), strfromDate, strToDate, txtLocation_Locl.Text, txtEmpCode_Locl.Text, txtRemarks_Locl.Text, strsptype, Convert.ToInt32(hdntrmodeid.Value), Convert.ToString(hdnDeviation_Locl.Value).Trim(), 0, Convert.ToDecimal(hdntrdetailsid_Locl.Value), Convert.ToDecimal(hdnTripid.Value));
            Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value);
        }

        protected void lstTravelMode_Locl_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTravelMode_Locl.Text = lstTravelMode_Locl.SelectedItem.Text;
            hdntrmodeid.Value = lstTravelMode_Locl.SelectedValue;
            SetTravelDeviation();
            PopupControlExtender1.Commit(lstTravelMode_Locl.SelectedItem.Text);

            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetDeviation_Locl('" + hdnDeviation_Locl.Value + "');", true);
        }

        protected void lstLocation_Locl_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtLocation_Locl.Text = lstLocation_Locl.SelectedItem.Text;
            PopupControlExtender3.Commit(lstLocation_Locl.SelectedItem.Text);

        }

        protected void lstTravelType_Locl_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTravelType_Locl.Text = lstTravelType_Locl.SelectedItem.Text;
            PopupControlExtender2.Commit(lstTravelType_Locl.SelectedItem.Text);
        }

        protected void txtFromdate_Locl_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(txtFromdate_Locl.Text).Trim() == "")
            {
                return;
            }
            DateValidations();
        }
        protected void txtToDate_Locl_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(txtFromdate_Locl.Text).Trim() == "")
            {
                lblmessage.Text = "From date  cannot be blank";
                return;
            }


            DateValidations();
        }

    #endregion

    #region Pagemethods
        public void GetTravelMode_Sanjay()
        {
            DataTable dtTripMode = new DataTable();
            dtTripMode = spm.getTravelMode();
            if (dtTripMode.Rows.Count > 0)
            {
                lstTravelMode_Locl.DataSource = dtTripMode;
                lstTravelMode_Locl.DataTextField = "trip_mode";
                lstTravelMode_Locl.DataValueField = "trip_mode_id";
                lstTravelMode_Locl.DataBind();

            }
        }
        public void GetCities()
        {
            DataTable dtCities = new DataTable();
            dtCities = spm.getCitiesDetails();
            if (dtCities.Rows.Count > 0)
            {
                lstLocation_Locl.DataSource = dtCities;
                lstLocation_Locl.DataTextField = "CITYNAME";
                lstLocation_Locl.DataValueField = "CITYID";
                lstLocation_Locl.DataBind();


            }
        }
        public void SetTravelDeviation()
        {
            DataTable dtTripDev = new DataTable();
            //dtTripDev = spm.getTravelDeviation(hflGrade_Locl.Value, Convert.ToInt32(lstTravelMode_Locl.SelectedValue));
            dtTripDev = spm.getTravelDeviation_localTrvl(hflGrade_Locl.Value, Convert.ToInt32(lstTravelMode_Locl.SelectedValue));
            if (dtTripDev.Rows.Count > 0)
            {
                hdnDeviation_Locl.Value = "No";

            }
            else
            {
                hdnDeviation_Locl.Value = "Yes";
            }
            
        }
        public void getLocaTravelDetails_edit()
        {
            DataTable dtTrDetails = new DataTable();
            dtTrDetails = spm.GetExpneseLocalTrvlDetails_edit(txtEmpCode_Locl.Text,hdntrdetailsid_Locl.Value);

            if (dtTrDetails.Rows.Count > 0)
            {   
                txtFromdate_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["From Date"]);
                txtToDate_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["To Date"]);
                txtRemarks_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["remarks"]);
                txtLocation_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["Location"]);
                hdnlocalid.Value = Convert.ToString(dtTrDetails.Rows[0]["local_travel_id"]);
                txtCharges_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["Charge"]);
                txtDeviation_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["Deviation"]);
                if (Convert.ToString(dtTrDetails.Rows[0]["trip_mode"]).Trim() != "" && Convert.ToString(dtTrDetails.Rows[0]["trip_mode"])!="0")
                txtTravelMode_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_mode"]);

                hdntrmodeid.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_mode"]);
                lstTravelMode_Locl.SelectedValue = hdntrmodeid.Value;
                hdnDeviation_Locl.Value = Convert.ToString(dtTrDetails.Rows[0]["Deviation"]);
               lstTravelType_Locl.SelectedValue= Convert.ToString(dtTrDetails.Rows[0]["Trip_type_id"]);
               hdnDesk_Locl.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_through_desk"]);
               localtrvl_btnSave.Text = "Update";
               if (Convert.ToString(dtTrDetails.Rows[0]["trvl_status"]) == "Booked")
               {
                  // txtCharges_Locl.Enabled = false;
                   //txtTravelType_Locl.Enabled = false;
                   Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                   txtTravelMode_Locl.BackColor = color;
                   txtTravelMode_Locl.Enabled = false;

               }
            }
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

        public void GetTravelMode()
        {
            DataTable dtTripMode = new DataTable();
            dtTripMode = spm.getTravelModefor_Requestor();
            if (dtTripMode.Rows.Count > 0)
            {
                lstTravelMode_Locl.DataSource = dtTripMode;
                lstTravelMode_Locl.DataTextField = "trip_mode";
                lstTravelMode_Locl.DataValueField = "trip_mode_id";
                lstTravelMode_Locl.DataBind();
            }
        }

        protected void DateValidations()
        {
            if (Convert.ToString(txtFromdate_Locl.Text).Trim() == "")
                return;

            if (Convert.ToString(txtToDate_Locl.Text).Trim() == "")
                return;


            string[] tr_strdate;
            string tr_strfromDate = "";
            string tr_strToDate = "";

            #region Exp.TraelRequestdate formatting

            if (Convert.ToString(txtFromdate_Locl.Text).Trim() != "")
            {
                tr_strdate = Convert.ToString(hdnfromdate_Locl.Value).Trim().Split('/');
                tr_strfromDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
            }
            if (Convert.ToString(txtToDate_Locl.Text).Trim() != "")
            {
                tr_strdate = Convert.ToString(hdnTodate_Locl.Value).Trim().Split('/');
                tr_strToDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
            }

            #endregion

            lblmessage.Text = "";
            string message = "";
            string[] strdate;
            string strfromDate = "";
            string strToDate = "";

            #region date formatting

            if (Convert.ToString(txtFromdate_Locl.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate_Locl.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            if (Convert.ToString(txtToDate_Locl.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtToDate_Locl.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }

            #endregion
            DataSet dttraveletails = new DataSet();
            if (Convert.ToString(txtFromdate_Locl.Text).Trim() != "" && Convert.ToString(txtToDate_Locl.Text).Trim() != "")
            {
                dttraveletails = spm.Get_ExpTravel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode_Locl.Text, hdnTripid.Value, "ExpLocalTrvls");

                if (dttraveletails.Tables[1].Rows.Count > 0)
                {
                    message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);

                }

                if (Convert.ToString(message).Trim() != "")
                {
                    lblmessage.Text = Convert.ToString(message).Trim();
                    lblmessage.Visible = true;
                    txtFromdate_Locl.Text = "";
                    txtToDate_Locl.Text = "";
                }

            }

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

                    string strtest = Convert.ToString(System.Web.HttpContext.Current.Session["Tr_type_id"]);
                    if (Convert.ToString(strtest).Trim() == "1")

                        strsql = " select top 10 CITYNAME from TBL_CITYMASTER where   " +
                                    "  CITYNAME like   @SearchText + '%' order by CITYNAME ";
                    else
                        strsql = " select top 10 CITYNAME from tbl_countriesMaster where  " +
                                    "   CITYNAME like   @SearchText + '%' order by CITYNAME ";
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




