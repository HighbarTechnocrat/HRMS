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

public partial class TravelDeskLocalTravel : System.Web.UI.Page
{   

    SP_Methods spm = new SP_Methods();

    #region Creative_Default_methods
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

    #region Not Use Events
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
    }
    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void localtrvl_delete_btn_Click(object sender, EventArgs e)
    {

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
                        editform.Visible = true;
                        divbtn.Visible = false;
                        divmsg.Visible = false;
                        GetCities();

                        txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                        txtToDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                        txtLocation.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                        txtTravelMode.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                        txtDeviation.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                        txtStatus.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                        
                      //  hdnTripid.Value = "1";
                      //  hdnlocalId.Value = "1";
                        //getLocalDetailsEdit();
                        //GetTravelMode();
                        EnabledFalse();


                        if (Request.QueryString.Count > 2)
                        {
                            hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                            hdnlocalId.Value = Convert.ToString(Request.QueryString[1]).Trim();
                            hdnInboxType.Value = Convert.ToString(Request.QueryString[3]).Trim();
                            hflGrade.Value = Convert.ToString(Session["empgrade"]);
                            GetTravelMode();
                            getLocalDetailsEdit();
                        }
                        ClientScript.RegisterStartupScript(Page.GetType(), "OnLoad", "SetCntrls('" + txtStatus.Text + "');", true);
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
            #region Check All Fields Blank
            //  lblmessage.Text = "";

            if (Convert.ToString(txtFromdate.Text).Trim() == "")
            {
                lblmessages.Text = "Please select From Date";
                return;
            }
            if (Convert.ToString(txtToDate.Text).Trim() == "")
            {
                lblmessages.Text = "Please select To Date";
                return;
            }

            if (Convert.ToString(txtStatus.Text).Trim() == "")
            {
                lblmessages.Text = "Please select Local travel status";
                return;
            }

            if (Convert.ToString(txtStatus.Text).Trim() == "Booked")
            {
                if (Convert.ToString(txtTravelMode.Text).Trim() == "")
                {
                    lblmessages.Text = "Please select Travel Mode";
                    return;
                }
            }

            if (Convert.ToString(Session["chkTrvlTDCOSbtnStatus"]).Trim() != "")
            {
                Response.Redirect("~/procs/AppTravelrequest.aspx?reqid=" + hdnTripid.Value + "&stype=" + hdnInboxType.Value);
            }
            Session["chkTrvlTDCOSbtnStatus"] = "Accomdation button Event is Submitted";

            Int32 ilocalid = 0;
            if (Convert.ToString(lstTravelMode.SelectedValue).Trim() != "")
                ilocalid = Convert.ToInt32(lstTravelMode.SelectedValue);
            spm.UpdateLocalTrvlDetailsForCOS(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnlocalId.Value), ilocalid, txtDeviation.Text, txtStatus.Text);
            Response.Redirect("~/procs/AppTravelrequest.aspx?reqid=" + hdnTripid.Value+"&stype="+hdnInboxType.Value);
            #endregion

        }
        protected void localtrvl_cancel_btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/procs/AppTravelrequest.aspx?reqid=" + hdnTripid.Value+"&stype="+hdnInboxType.Value);
        }
        protected void lstLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtLocation.Text = lstLocation.SelectedItem.Text;
            PopupControlExtender1.Commit(lstLocation.SelectedItem.Text);
        }
        protected void lstStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtStatus.Text = lstStatus.SelectedItem.Text;
            PopupControlExtender5.Commit(lstStatus.SelectedItem.Text);
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetCntrls('" + lstStatus.SelectedItem.Text + "');", true);

        }
        protected void lstTravelMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTravelMode.Text = lstTravelMode.SelectedItem.Text;
            PopupControlExtender3.Commit(lstTravelMode.SelectedItem.Text);
            SetTravelDeviation();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetDeviation('" + hdnDeviation.Value + "');", true);
        }
    #endregion

    #region Page_Methods

        public void SetTravelDeviation()
        {
            DataTable dtTripDev = new DataTable();
            dtTripDev = spm.getTravelDeviation_LocalTrvl(hdnTripid.Value, Convert.ToInt32(lstTravelMode.SelectedValue));
            if (dtTripDev.Rows.Count > 0)
            {
                hdnDeviation.Value = "No";
            }
            else
            {
                hdnDeviation.Value = "Yes";
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
        public void getLocalDetailsEdit()
        {
            DataTable dtTrDetails = new DataTable();
            dtTrDetails = spm.GetLocalDetailsEditforCOS(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnlocalId.Value));
            if (dtTrDetails.Rows.Count > 0)
            {
                txtTripId.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_id"]);
                txtTravelType.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_description"]);
                txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["From Date"]);
                txtToDate.Text = Convert.ToString(dtTrDetails.Rows[0]["To Date"]);
                txtRequirement.Text = Convert.ToString(dtTrDetails.Rows[0]["remarks"]);
                txtLocation.Text = Convert.ToString(dtTrDetails.Rows[0]["Location"]);
                if (Convert.ToString(dtTrDetails.Rows[0]["travel_mode_id"]) != "")
                {
                    lstTravelMode.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["travel_mode_id"]);
                    txtTravelMode.Text = Convert.ToString(dtTrDetails.Rows[0]["tripmode"]);
                    
                }
                //if (Convert.ToString(dtTrDetails.Rows[0]["travel_mode_id"]) != "")
                //{
                //    txtTravelMode.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_mode"]);
                //    lstTravelMode.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["travel_mode_id"]);
                //}

                if (Convert.ToString(dtTrDetails.Rows[0]["Status"]) != "")
                {
                    txtStatus.Text = Convert.ToString(dtTrDetails.Rows[0]["Status"]);
                    lstStatus.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["Status"]);
                }

                if (Convert.ToString(dtTrDetails.Rows[0]["Deviation"]) != "")
                {
                    txtDeviation.Text = Convert.ToString(dtTrDetails.Rows[0]["Deviation"]);                
                 }
              
            }
        }
        public void GetTravelMode()
        {
            DataTable dtTripMode = new DataTable();
            dtTripMode = spm.getTravelModeforCOS();
            if (dtTripMode.Rows.Count > 0)
            {
                lstTravelMode.DataSource = dtTripMode;
                lstTravelMode.DataTextField = "trip_mode";
                lstTravelMode.DataValueField = "trip_mode_id";
                lstTravelMode.DataBind();
            }
        }
        public void EnabledFalse()
        {
            txtTripId.Enabled = false;
            txtTravelType.Enabled = false;
            txtFromdate.Enabled = false;
            txtToDate.Enabled = false;
            txtRequirement.Enabled = false;
            txtLocation.Enabled = false;

        }
    #endregion





      
}




