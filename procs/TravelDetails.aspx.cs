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

public partial class TravelDetails : System.Web.UI.Page
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

    #region Page_Events
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }


            //if (Convert.ToString(hdnDeviation.Value).Trim() != "")
            //    txtDeviation.Text = Convert.ToString(hdnDeviation.Value).Trim();

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
                    
                    txtTravelMode.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtToDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                  //  txtDestination.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                   // txtOrigin.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtDeviation.Attributes.Add("onkeypress", "return noanyCharecters(event);");

                    txtFromdate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
                    txtToDate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");


                    txtDeviation.Enabled = false;
                    trvldeatils_delete_btn.Visible = false;
                    editform.Visible = true;
                    divbtn.Visible = false;

                    divmsg.Visible = false;
                    // lstDeviation.Enabled = false;

                    lblmessage.Text = "";

                    GetTripDetails();
                    
                   

                    if (Convert.ToString(Session["TripTypeId"]) != null && Convert.ToString(Session["TripTypeId"]).Trim() != "")
                    {
                        txtEmpCode.Text = Convert.ToString(Session["ReqEmpCode"]);
                        txtTravelType.Text = Convert.ToString(Session["TravelType"]);
                        hdnTripid.Value = Convert.ToString(Session["TripID"]);
                        hdnfromdate.Value = Convert.ToString(Session["Fromdate"]);
                        hdnTodate.Value = Convert.ToString(Session["Todate"]);
                        hdnTryiptypeid.Value = Convert.ToString(Session["TripTypeId"]);
                        hflGrade.Value = Convert.ToString(Session["Grade"]);
                    }
                    if (Convert.ToString(hdnTryiptypeid.Value).Trim()=="2")
                        GetTravelMode();
                    else
                        GetTravelMode_old();
                      
                    if (Request.QueryString.Count > 2)
                    {  
                        hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();

                        hdntrdetailsid.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        trvldeatils_btnSave.Text = "Update";
                        trvldeatils_delete_btn.Visible = true;
                        if (Convert.ToString(hdntrdetailsid.Value).Trim() == "0")
                        {
                            getTravelDetails();
                        }
                        else
                        {
                            getTravelDetailsEdit();
                        }
                        
                    }
              
                    if (Convert.ToString(hdntrdetailsid.Value).Trim() == "")
                    {
                        trvldeatils_delete_btn.Enabled = false;
                    }
                    else
                    {
                        trvldeatils_delete_btn.Enabled = true;
                    }
                 //   GetCities();
                    txtTravelType.Enabled = false;
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

    protected void trvldeatils_cancel_btn_Click(object sender, EventArgs e)
    {
       // Server.Transfer("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
        if (Convert.ToString(hdnTripid.Value).Trim() != "")
            Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
        else
            Response.Redirect("~/procs/TravelRequest.aspx");

    }
    protected void lstTravelType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTravelType.Text = lstTravelType.SelectedItem.Text;
        PopupControlExtender2.Commit(lstTravelType.SelectedItem.Text);
    }
    protected void lstTravelMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTravelMode.Text = lstTravelMode.SelectedItem.Text;
        PopupControlExtender1.Commit(lstTravelMode.SelectedItem.Text);

        SetTravelDeviation();

        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetDeviation('" + hdnDeviation.Value + "');", true);

        // txtDeviation.Text = Convert.ToString(hdnDeviation.Value);
    }

    protected void lstOrigin_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtOrigin.Text = lstOrigin.SelectedItem.Text;
        PopupControlExtender3.Commit(lstOrigin.SelectedItem.Text);
    }
    protected void lstDestination_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDestination.Text = lstDestination.SelectedItem.Text;
        PopupControlExtender4.Commit(lstDestination.SelectedItem.Text);
    }

    //protected void lstDeviation_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtDeviation.Text = lstDeviation.SelectedItem.Text;
    //    PopupControlExtender5.Commit(lstDeviation.SelectedItem.Text);


    //}

    protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
    {
        #region Check For Blank Fields
        lblmessages.Text = "";
        if (Convert.ToString(txtTravelType.Text).Trim() == "")
        {
            lblmessages.Text = "Please select Travel Type";
            return;
        }
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
        if (Convert.ToString(txtOrigin.Text).Trim() == "")
        {
            lblmessages.Text = "Please Select Departure Place For Travel";
            return;
        }
        if (Convert.ToString(txtDestination.Text).Trim() == "")
        {
            lblmessages.Text = "Please Select Arrival Place For Travel";
            return;
        }

        if (Check_Cities_name(txtOrigin.Text) == false)
        {
            lblmessages.Text = "Please Select Correct Departure Place For Travel";
            return;
        }
        if (Check_Cities_name(txtDestination.Text) == false)
        {
            lblmessages.Text = "Please Select Correct Arrival Place For Travel";
            return;
        }
        if (Convert.ToString(txtOrigin.Text).Trim() == Convert.ToString(txtDestination.Text).Trim())
        {
            lblmessages.Text = "Departure Place & Arrival Place  should not be same.";
            return;
        }
      

        #endregion
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
        if (Convert.ToString(txtTravelMode.Text).Trim() == "")
        {
            lblmessage.Text = " Travel Mode  cannot be blank";
            return;
        }

        if (Convert.ToString(txtOrigin.Text).Trim() == "")
        {
            lblmessage.Text = "Place of Origin cannot be blank";
            return;
        }

        if (Convert.ToString(txtDestination.Text).Trim() == "")
        {
            lblmessage.Text = "Place of Destination cannot be blank";
            return;
        }

        if (chkCOS.Checked == true)
        {
            hdnCOS.Value = "Yes";
            txtDeviation.Enabled = false;
        }
        else
        {
            hdnCOS.Value = "No";
            txtDeviation.Enabled = false;
        }

        #endregion


        //if (Convert.ToString(Session["chkTrvlAccLocalTrvlbtnStatus"]).Trim() != "")
        //{
        //    if (Convert.ToString(hdnTripid.Value).Trim() != "0" && Convert.ToString(hdnTripid.Value).Trim() != "")
        //        Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
        //    else
        //        Response.Redirect("~/procs/TravelRequest.aspx");
        //}
        //Session["chkTrvlAccLocalTrvlbtnStatus"] = "Travel Details button Event is Submitted";

        if (trvldeatils_btnSave.Text == "Submit")
        {
            #region InsertTravelDetails
            Session["Destination"] = Convert.ToString(txtDestination.Text).Trim();
            spm.InsertTravelDetails(Convert.ToInt32(hdnTryiptypeid.Value), Convert.ToInt32(lstTravelMode.SelectedValue), strfromDate, Convert.ToString(txtOrigin.Text).Trim(), strToDate, Convert.ToString(txtDestination.Text).Trim(), txtDeviation.Text, txtEmpCode.Text, 0, hdnCOS.Value, txtRequirememt.Text, "InsertTempTable", txtExpArrivalDate.Text, txtExpDepartDate.Text);
            if (Convert.ToString(hdnTripid.Value).Trim() != "0" && Convert.ToString(hdnTripid.Value).Trim() != "")
                Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
            else
                Response.Redirect("~/procs/TravelRequest.aspx");
            #endregion
        }
        else
        {
            #region UpdateTravelDetails
            txtDeviation.Text = hdnDeviation.Value;
            spm.UpdateTravelDetails(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdntrdetailsid.Value), txtTravelMode.Text, strfromDate, Convert.ToString(txtOrigin.Text).Trim(), strToDate, Convert.ToString(txtDestination.Text).Trim(), txtDeviation.Text, hdnCOS.Value, txtRequirememt.Text, "UpdateMainTable", Convert.ToString(txtEmpCode.Text), txtExpArrivalDate.Text, txtExpDepartDate.Text);
            if (Convert.ToString(hdnTripid.Value).Trim() != "0" && Convert.ToString(hdnTripid.Value).Trim() != "")
                Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
            else
                Response.Redirect("~/procs/TravelRequest.aspx");
            #endregion
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
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        txtDeviation.Text = hdnDeviation.Value;
        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "From date  cannot be blank";
            return;
        }

        
        string[] tr_strdate;
        string tr_strfromDate = "";
        string tr_strToDate = "";

        #region TraelRequestdate formatting

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
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";
        string message = "";
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
          
            dttraveletails = spm.Get_Travel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);

            if (dttraveletails.Tables[1].Rows.Count > 0)
            {
                message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);

            }

            if (Convert.ToString(message).Trim() != "")
            {
                lblmessages.Text = Convert.ToString(message).Trim();
                txtFromdate.Text = "";
                txtToDate.Text = "";
            }
            else
            {
                lblmessages.Text = "";
            }
        }
    }

    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        txtDeviation.Text = hdnDeviation.Value;
        string[] tr_strdate;
        string tr_strfromDate = "";
        string tr_strToDate = "";

        #region TraelRequestdate formatting

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
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";
        string message = "";
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
            dttraveletails = spm.Get_Travel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);

            if (dttraveletails.Tables[1].Rows.Count > 0)
            {
                message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);

            }

            if (Convert.ToString(message).Trim() != "")
            {
                lblmessages.Text = Convert.ToString(message).Trim();
                txtFromdate.Text = "";
                txtToDate.Text = "";
            }
            else
            {
                lblmessages.Text = "";
            }
        }
    }
    protected void trvldeatils_delete_btn_Click(object sender, EventArgs e)
    {
        #region Check For Blank Fields
        lblmessage.Text = "";
        if (Convert.ToString(txtTravelType.Text).Trim() == "")
        {
            lblmessage.Text = "Please select Travel Type";
            return;
        }
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
        if (Convert.ToString(txtOrigin.Text).Trim() == "")
        {
            lblmessage.Text = "Please Select Departure Place For Travel";
            return;
        }
        if (Convert.ToString(txtDestination.Text).Trim() == "")
        {
            lblmessage.Text = "Please Select Arrival Place For Travel";
            return;
        }
        #endregion


        if (Convert.ToString(Session["chkTrvlAccLocalTrvlbtnStatus"]).Trim() != "")
        {
            if (Convert.ToString(hdnTripid.Value).Trim() != "")
                Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
            else
                Response.Redirect("~/procs/TravelRequest.aspx");
        }
        Session["chkTrvlAccLocalTrvlbtnStatus"] = "Travel Details button Event is Submitted";

        #region Delete TravelDetails
        spm.DeleteTravelDetails(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdntrdetailsid.Value), Convert.ToString(txtEmpCode.Text));
        lblmessage.Text = "Travails Details Deleted Successfully";
        if (Convert.ToString(hdnTripid.Value).Trim() != "")
            Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
        else
            Response.Redirect("~/procs/TravelRequest.aspx");
        #endregion


    }
    #endregion

    #region Pagemethods
    public void GetTravelMode_old()
    {
        DataTable dtTripMode = new DataTable();
        dtTripMode = spm.getTravelMode();
        if (dtTripMode.Rows.Count > 0)
        {
            lstTravelMode.DataSource = dtTripMode;
            lstTravelMode.DataTextField = "trip_mode";
            lstTravelMode.DataValueField = "trip_mode_id";
            lstTravelMode.DataBind();

        }
    }

    public void GetTravelMode()
    {
        DataTable dtTripMode = new DataTable();
        //hdnTryiptypeid.Value
        //dtTripMode = spm.getTravelMode();
        dtTripMode = spm.getTravelMode_tripwise(Convert.ToInt32(hdnTryiptypeid.Value));
        if (dtTripMode.Rows.Count > 0)
        {
            lstTravelMode.DataSource = dtTripMode;
            lstTravelMode.DataTextField = "trip_mode";
            lstTravelMode.DataValueField = "trip_mode_id";
            lstTravelMode.DataBind();

        }
    }

    public void GetTravelMode_Typewise()
    {
        DataTable dtTripMode = new DataTable();
        dtTripMode = spm.getTravelMode();
        if (dtTripMode.Rows.Count > 0)
        {
            lstTravelMode.DataSource = dtTripMode;
            lstTravelMode.DataTextField = "trip_mode";
            lstTravelMode.DataValueField = "trip_mode_id";
            lstTravelMode.DataBind();

        }
    }
    public void GetCities()
    {
        DataTable dtCities = new DataTable();
        dtCities = spm.getCitiesDetails_exps(hdnTryiptypeid.Value);
        if (dtCities.Rows.Count > 0)
        {
            lstOrigin.DataSource = dtCities;
            lstOrigin.DataTextField = "CITYNAME";
            lstOrigin.DataValueField = "CITYID";
            lstOrigin.DataBind();


            lstDestination.DataSource = dtCities;
            lstDestination.DataTextField = "CITYNAME";
            lstDestination.DataValueField = "CITYID";
            lstDestination.DataBind();

        }
    }
    public void GetTripDetails()
    {
        DataTable dtTripDetails = new DataTable();
        dtTripDetails = spm.getTripDetails();
        if (dtTripDetails.Rows.Count > 0)
        {
            lstTravelType.DataSource = dtTripDetails;
            lstTravelType.DataTextField = "trip_description";
            lstTravelType.DataValueField = "trip_type";
            lstTravelType.DataBind();

        }
    }
    public void SetTravelDeviation()
    {
        DataTable dtTripDev = new DataTable();
        dtTripDev = spm.getTravelDeviation(hflGrade.Value, Convert.ToInt32(lstTravelMode.SelectedValue));
        if (dtTripDev.Rows.Count > 0)
        {
            hdnDeviation.Value = "No";
        }
        else
        {
            hdnDeviation.Value = "Yes";
        }
    }
    public void getTravelDetails()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetTravelDetails(txtEmpCode.Text);

        if (dtTrDetails.Rows.Count > 0)
        {
            txtTravelMode.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_mode"]);
            txtDeviation.Text = Convert.ToString(dtTrDetails.Rows[0]["deviation"]);
            txtRequirememt.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_requirements"]);
            txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["departure_date"]);
            txtToDate.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_date"]);
            txtTravelType.Text = Convert.ToString(Session["TravelType"]);
            hdnDesk.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_through_desk"]);
            txtOrigin.Text = Convert.ToString(dtTrDetails.Rows[0]["departure_place"]);
            txtDestination.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_place"]);
            txtExpArrivalDate.Text = Convert.ToString(dtTrDetails.Rows[0]["expectedarrival"]); 
            txtExpDepartDate.Text = Convert.ToString(dtTrDetails.Rows[0]["expecteddeparture"]);

            if (Convert.ToString(hdnDesk.Value).Trim() == "Yes")
            {
                chkCOS.Checked = true;
            }
            else
            {
                chkCOS.Checked = false;
            }

        }
    }

    public void getTravelDetailsEdit()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetTempTravelDetailsEdit(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdntrdetailsid.Value), Convert.ToString(txtEmpCode.Text));

        if (dtTrDetails.Rows.Count > 0)
        {
            txtTravelMode.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_mode"]);
            txtDeviation.Text = Convert.ToString(dtTrDetails.Rows[0]["deviation"]);
            txtRequirememt.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_requirements"]);
            txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["departure_date"]);
            txtToDate.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_date"]);
            txtTravelType.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_description"]);

            hdnDesk.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_through_desk"]);
            txtOrigin.Text = Convert.ToString(dtTrDetails.Rows[0]["departure_place"]);
            txtDestination.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_place"]);
            hdnDeviation.Value = Convert.ToString(dtTrDetails.Rows[0]["deviation"]);
            txtExpArrivalDate.Text = Convert.ToString(dtTrDetails.Rows[0]["expectedarrival"]);
            txtExpDepartDate.Text = Convert.ToString(dtTrDetails.Rows[0]["expecteddeparture"]);


            if (Convert.ToString(hdnDesk.Value).Trim() == "Yes")
            {
                chkCOS.Checked = true;
            }
            else
            {
                chkCOS.Checked = false;
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

                string strtest = Convert.ToString(System.Web.HttpContext.Current.Session["TripTypeId"]);
                if (Convert.ToString(strtest).Trim() == "1")
                    strsql = " select top 10 CITYNAME from TBL_CITYMASTER where  " +
                                "  CITYNAME like   @SearchText + '%' order by CITYNAME ";
                 else
                    strsql = " select top 10 cname as CITYNAME from TBL_hcountryMASTER where " +
                                 " CNAME like   @SearchText + '%' order by CNAME ";
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
