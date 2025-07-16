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



public partial class TravelDeskTravelDetails : System.Web.UI.Page
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
    //public string filename = "";
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

            FieldValidations();

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
                   // divmsg.Visible = false;

                    txtEmpCode.Text = Convert.ToString(Session["ReqEmpCode"]);
                    txtTravelType.Text = Convert.ToString(Session["TravelType"]);
                    //   hdnTripid.Value = Convert.ToString(Session["TripID"]);
                    hdnfromdate.Value = Convert.ToString(Session["Fromdate"]);
                    hdnTodate.Value = Convert.ToString(Session["Todate"]);
                    hdnTryiptypeid.Value = Convert.ToString(Session["TripTypeId"]);
                    hflGrade.Value = Convert.ToString(Session["Grade"]);
                    //hdnactualDeparturedate.Value = Convert.ToString(Session["Fromdate"]);
                    //hdnactualArrivaldate.Value = Convert.ToString(Session["Todate"]);

                    if (Convert.ToString(hdnTryiptypeid.Value).Trim() == "2")
                        GetTravelMode();
                    else
                        GetTravelMode_old();

                    GetCities();
                    //hdnTripid.Value = "1";
                    //hdntrdetailsid.Value = "1";
                    //getTravelDetailsEdit();
                    EnableFalse();

                   // lstTravelType.SelectedValue = Convert.ToString(Session["TripTypeId"]).Trim();
                    if (Request.QueryString.Count > 0)
                    {
                        hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdntrdetailsid.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        //   hdnexp_id.Value = Convert.ToString(Request.QueryString[2]).Trim();
                        hdnInboxType.Value = Convert.ToString(Request.QueryString[3]).Trim();
                        hdnFreshByTD.Value= Convert.ToString(Request.QueryString[4]).Trim();
                        getTravelDetailsEdit();
                        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetCntrls('" + txtStatus.Text + "');", true);
                        
 
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

    protected void trvldeatils_cancel_btn_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/procs/travel_Exp.aspx");
      //  Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value+"&expid="+hdnexp_id.Value);
         Response.Redirect("~/procs/AppTravelrequest.aspx?reqid="+hdnTripid.Value+"&stype="+hdnInboxType.Value);
        
    }
    protected void trvldeatils_delete_btn_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/procs/travel_Exp.aspx");
        //  Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value+"&expid="+hdnexp_id.Value);
        spm.DeleteTravelDetailsforTravelDesk(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdntrdetailsid.Value));
        Response.Redirect("~/procs/AppTravelrequest.aspx?reqid=" + hdnTripid.Value + "&stype=" + hdnInboxType.Value);

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
            if (hdnFreshByTD.Value =="N")
            {
                SetTravelDeviation();
            }
            else
            {
                SetTravelDeviationbyRequester();

            }
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetDeviation('" + hdnDeviation.Value + "');", true);
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
    protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
    {
        lblmessage.Visible = true;
        lblmessage.Text = "";

        string filename = "";
        #region Check Validations

        if (hdnFreshByTD.Value == "Y")
        {
            txtFromdate.Text = txtActualDeptDate.Text;
            txtToDate.Text = txtActualArrivalDate.Text;
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

        if (Convert.ToString(txtStatus.Text).Trim() == "")
        {
            lblmessage.Text = "Please select status for Travel Details";
            return;
        }
        
        if (Convert.ToString(txtStatus.Text).Trim() == "Not Booked")
        {
            txtfare.Text = "0";
        }

         if (Convert.ToString(txtStatus.Text).Trim() == "Booked")
        {
            if (Convert.ToString(txtFromTime.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Departure Time.";
                return;
            }

            if (Convert.ToString(txtToTime.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Arrival Time.";
                return;
            }

         }

        string[] strdate;
        string strDeptTime = "";
        string strArrvialTime = "";
        if (Convert.ToString(txtToTime.Text).Trim() != "" || Convert.ToString(txtFromTime.Text).Trim() != "")
        {
            if (Convert.ToString(txtToTime.Text).Trim() == "" || Convert.ToString(txtFromTime.Text).Trim() == "")
            {
                if (Convert.ToString(txtFromTime.Text).Trim() == "")
                    lblmessage.Text = "Please enter correct Departure Time.";
                if (Convert.ToString(txtToTime.Text).Trim() == "")
                    lblmessage.Text = "Please enter correct Arrival Time.";
                return;
            }
            else
            {
                if (Convert.ToString(txtFromTime.Text).Trim() != "")
                {
                    strdate = Convert.ToString(txtFromTime.Text).Trim().Split(':');
                    if (strdate.Length == 2)
                    {
                        if (strdate[0].Length != 2 || strdate[1].Length != 2)
                        {
                            lblmessage.Text = "Please enter correct Departure Time.";
                            return;
                        }
                        else
                        {
                            if (Convert.ToInt32(strdate[0].ToString()) > 24)
                            {
                                lblmessage.Text = "Please enter correct Departure Time.";
                                return;
                            }
                            if (Convert.ToInt32(strdate[1].ToString()) > 59)
                            {
                                lblmessage.Text = "Please enter correct Departure Time.";
                                return;
                            }
                        }
                    }
                    else
                    {
                        lblmessage.Text = "Please enter correct Departure Time.";
                        return;
                    }
                }

                if (Convert.ToString(txtToTime.Text).Trim() != "")
                {
                    strdate = Convert.ToString(txtToTime.Text).Trim().Split(':');
                    if (strdate.Length == 2)
                    {
                        if (strdate[0].Length != 2 || strdate[1].Length != 2)
                        {
                            lblmessage.Text = "Please enter correct Arrival Time.";
                            return;
                        }
                        else
                        {
                            if (Convert.ToInt32(strdate[0].ToString()) > 24)
                            {
                                lblmessage.Text = "Please enter correct Arrival Time.";
                                return;
                            }
                            if (Convert.ToInt32(strdate[1].ToString()) > 59)
                            {
                                lblmessage.Text = "Please enter correct Arrival Time.";
                                return;
                            }
                        }
                    }
                    else
                    {
                        lblmessage.Text = "Please enter correct Arrival Time.";
                        return;
                    }
                }

                strDeptTime = Convert.ToString(txtFromTime.Text).Trim().Replace(':', '.');
                strArrvialTime = Convert.ToString(txtToTime.Text).Trim().Replace(':', '.');
                if (Convert.ToDouble(strArrvialTime) > 24)
                {
                    lblmessage.Text = "Please enter correct Arrival Time.";
                    return;
                }
                if (Convert.ToDouble(strDeptTime) > 24)
                {
                    lblmessage.Text = "Please enter correct Departure Time.";
                    return;
                }
            }
        }

        if (Convert.ToString(txtFromdate.Text).Trim() == Convert.ToString(txtToDate.Text).Trim())
        {
            if (Convert.ToString(txtToTime.Text).Trim() != "" && Convert.ToString(txtFromTime.Text).Trim() != "")
            {
                if (Convert.ToString(txtToTime.Text).Trim() != Convert.ToString(txtFromTime.Text).Trim())
                {
                    if (Convert.ToDouble(strArrvialTime) < Convert.ToDouble(strDeptTime))
                    {
                        lblmessage.Text = "Arrival Time should not be less than Departure Time.";
                        return;
                    }
                }
            }
        }
        Decimal dcharges = 0;
        string ActualArrivalDate = "";
        string ActualDeptDate = "";
        if (Convert.ToString(txtStatus.Text).Trim() == "Booked")
        {
            if (Convert.ToString(txtfare.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter correct Fare.";
                return;
            }
            if (Convert.ToString(txtfare.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtfare.Text).Trim().Split('.');
                if (strdate.Length > 2)
                {
                    txtfare.Text = "0";
                    lblmessage.Text = "Please enter correct Fare.";
                    return;
                }
                Decimal dfare = 0;
                dfare = Convert.ToDecimal(txtfare.Text);
                if (dfare == 0)
                {
                    lblmessage.Text = "Please enter correct Fare.";
                    return;
                }
            }
            if (Convert.ToString(txtActualDeptDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtActualDeptDate.Text).Trim().Split('/');
                ActualDeptDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            if (Convert.ToString(txtActualArrivalDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtActualArrivalDate.Text).Trim().Split('/');
                ActualArrivalDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
        }
        else
        {
            txtfare.Text = "0";
            dcharges = 0;
        }

        #endregion
     //   return;
        if (chkMealInc.Checked == true)
        {
            hdnchkMealInc.Value = "Yes";
            
        }
        else
        {
            hdnchkMealInc.Value = "No";
            
        }

        if (uploadfile.HasFile)
        {
            filename = uploadfile.FileName;
        }
       

        string strfromDate=null;
        string strfileName = null;
        #region date formatting
        if (Convert.ToString(txtActualDeptDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtActualDeptDate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[0]) + "_" + Convert.ToString(strdate[1]);

        }
        #endregion

        if (Convert.ToString(filename).Trim() != "")
        {
            filename = uploadfile.FileName;
            strfileName = "";
            strfileName = Session["ReqEmpCode"].ToString() + "_" + strfromDate + "_" + uploadfile.FileName;
            filename = strfileName;
            uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["TravelClaimDocuments"]).Trim()), strfileName));
        }

        if (Convert.ToString(Session["chkTrvlTDCOSbtnStatus"]).Trim() != "")
        {
            Response.Redirect("~/procs/AppTravelrequest.aspx?reqid=" + hdnTripid.Value + "&stype=" + hdnInboxType.Value);
        }
        Session["chkTrvlTDCOSbtnStatus"] = "Accomdation button Event is Submitted";

        if (Convert.ToString(txtfare.Text).Trim() != "")
            dcharges = Math.Round(Convert.ToDecimal(txtfare.Text), 2);

        if (hdnFreshByTD.Value == "N")
        {
            spm.UpdateTravelDetailsforTravelDesk(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdntrdetailsid.Value), txtFromTime.Text, txtToTime.Text, dcharges, txtStatus.Text, ActualDeptDate, ActualArrivalDate, txtfortravelRequest.Text, hdnchkMealInc.Value, filename, txtOrigin.Text, txtDestination.Text);
        }
        else
        {
            spm.InsertTravelDetailsforTravelDesk(Convert.ToInt32(hdnTripid.Value), Session["TravelType"].ToString(), txtTravelMode.Text, ActualDeptDate, ActualArrivalDate, txtFromTime.Text, txtToTime.Text, txtDeviation.Text, dcharges, txtStatus.Text, txtfortravelRequest.Text, hdnchkMealInc.Value, filename, txtOrigin.Text, txtDestination.Text);
        }
        lblmessage.Text = "Status successfully updated for this Trael Details";
        Response.Redirect("~/procs/AppTravelrequest.aspx?reqid=" + hdnTripid.Value+"&stype="+hdnInboxType.Value);

    }
    protected void lnkuplodedfile_Click(object sender, EventArgs e)
    {
        try
        {
            bool hasFile = uploadfile.HasFile;
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["TravelClaimDocuments"]).Trim()), lnkuplodedfile.Text);

            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
            Response.WriteFile(strfilepath);
            Response.End();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    protected void lstStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtStatus.Text = lstStatus.SelectedItem.Text;
        PopupControlExtender5.Commit(lstStatus.SelectedItem.Text);
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetCntrls('" + lstStatus.SelectedItem.Text + "');", true);
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
         public void GetCities()
         {
             DataTable dtCities = new DataTable();
             dtCities = spm.getCitiesDetails();
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
        
        public void SetTravelDeviationbyRequester()
          {
             DataTable dtTripDev = new DataTable();
             dtTripDev = spm.getTravelDeviationByEMPcode(Session["ReqEmpCode"].ToString(), Convert.ToInt32(lstTravelMode.SelectedValue));
             if (dtTripDev.Rows.Count > 0)
             {
                 hdnDeviation.Value = "No";

             }
             else
             {
                 hdnDeviation.Value = "Yes";


             }
         }
    
         public void getTravelDetailsEdit()
         {
             DataTable dtTrDetails = new DataTable();
             dtTrDetails = spm.GetTravelDetailsEdit(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdntrdetailsid.Value));

             if (dtTrDetails.Rows.Count > 0 && hdnFreshByTD.Value == "N")
             {
                 txtTripId.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_id"]);
                 txtTravelMode.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_mode"]);
                 txtDeviation.Text = Convert.ToString(dtTrDetails.Rows[0]["deviation"]);
                 txtRequirememt.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_remarks"]);
                 txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["departure_date"]);
                 txtToDate.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_date"]);
                 txtTravelType.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_description"]);

                 hdnDesk.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_through_desk"]);
                 txtOrigin.Text = Convert.ToString(dtTrDetails.Rows[0]["departure_place"]);
                 txtDestination.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_place"]);
                 txtActualDeptDate.Text = Convert.ToString(dtTrDetails.Rows[0]["ActualarrivalDate"]);
                 txtActualArrivalDate.Text = Convert.ToString(dtTrDetails.Rows[0]["ActualdepartureDate"]);
                 hdnchkMealInc.Value = Convert.ToString(dtTrDetails.Rows[0]["mealnc"]);
                 hdnaddedbyTDFlg.Value = Convert.ToString(dtTrDetails.Rows[0]["AddedFlg"]);
                 if (hdnaddedbyTDFlg.Value == "AddedByTD")
                 {
                     trvldeatils_delete_btn.Visible=true;
                 }
                 if (Convert.ToString(hdnchkMealInc.Value).Trim() == "Yes")
                 {
                     chkMealInc.Checked = true;
                 }
                 else
                 {
                     chkMealInc.Checked = false;
                 }
                 //,departuretime,,,
                 if (Convert.ToString(dtTrDetails.Rows[0]["trip_remarks"])!="")                     
                 txtRequirememt.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_remarks"]);

                 if (Convert.ToString(dtTrDetails.Rows[0]["departuretime"]) != "")
                     txtFromTime.Text = Convert.ToString(dtTrDetails.Rows[0]["departuretime"]);

                 if (Convert.ToString(dtTrDetails.Rows[0]["arrivaltime"]) != "")
                     txtToTime.Text = Convert.ToString(dtTrDetails.Rows[0]["arrivaltime"]);

                 if (Convert.ToString(dtTrDetails.Rows[0]["Fare"]) != "")
                     txtfare.Text = Convert.ToString(dtTrDetails.Rows[0]["Fare"]);

                 if (Convert.ToString(dtTrDetails.Rows[0]["Status"]) != "")
                 {
                     txtStatus.Text = Convert.ToString(dtTrDetails.Rows[0]["Status"]);
                     lstStatus.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["Status"]);
                 }

                 if (Convert.ToString(dtTrDetails.Rows[0]["expecteddeparture"]) != "")
                     txtExpDepartDate.Text = Convert.ToString(dtTrDetails.Rows[0]["expecteddeparture"]);

                 if (Convert.ToString(dtTrDetails.Rows[0]["expectedarrival"]) != "")
                     txtExpArrivalDate.Text = Convert.ToString(dtTrDetails.Rows[0]["expectedarrival"]);

                 if (Convert.ToString(dtTrDetails.Rows[0]["ActualdepartureDate"]) != "")
                     txtActualDeptDate.Text = Convert.ToString(dtTrDetails.Rows[0]["ActualdepartureDate"]);

                 if (Convert.ToString(dtTrDetails.Rows[0]["ActualarrivalDate"]) != "")
                     txtActualArrivalDate.Text = Convert.ToString(dtTrDetails.Rows[0]["ActualarrivalDate"]);

                 if (Convert.ToString(dtTrDetails.Rows[0]["TravelRemarks"]) != "")
                     txtfortravelRequest.Text = Convert.ToString(dtTrDetails.Rows[0]["TravelRemarks"]);
                 
                 if (Convert.ToString(dtTrDetails.Rows[0]["Status"]) == "Booked")
                 {
                     //txtFromTime.Enabled = true;
                     //txtToTime.Enabled = true;
                     //txtfare.Enabled = true;
                 }
                 else
                 {
                     //txtFromTime.Enabled = false;
                     //txtToTime.Enabled = false;
                    // txtfare.Enabled = false;
                 }
                 lnkuplodedfile.Text = Convert.ToString(dtTrDetails.Rows[0]["UploadFile"]).Trim();

                 txtTravelMode.Attributes.Add("class", "grayDropdown");
                 //txtActualDeptDate.Attributes.Add("class", "grayDropdown");
                 //txtActualArrivalDate.Attributes.Add("class", "grayDropdown");
             }
             else
             {
                 EnableTrue();
                 txtTripId.Text = hdnTripid.Value;
                 txtTravelType.Text = Session["TravelType"].ToString();
                 txtFromTime.Enabled = true;
                 txtToTime.Enabled = true;
                 txtfare.Enabled = true;
                 txtTravelMode.Attributes.Add("class", "");
                 txtActualDeptDate.Attributes.Add("class", "");
                 txtActualArrivalDate.Attributes.Add("class", "");
                 txtTravelMode.Attributes.Add("class", "");
                 txtStatus.Text = "Booked";
             }
         }

         private void FieldValidations()
         {
             txtTravelType.Attributes.Add("onkeypress", "return noanyCharecters(event);");
             txtTripId.Attributes.Add("onkeypress", "return noanyCharecters(event);");
             txtTravelMode.Attributes.Add("onkeypress", "return noanyCharecters(event);");
             txtDeviation.Attributes.Add("onkeypress", "return noanyCharecters(event);");
             txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
             txtOrigin.Attributes.Add("onkeypress", "return noanyCharecters(event);");
             txtFromTime.Attributes.Add("onkeypress", "return onCharOnlyNumber_Time(event);");
             txtToDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
             txtDestination.Attributes.Add("onkeypress", "return noanyCharecters(event);");
             txtToTime.Attributes.Add("onkeypress", "return onCharOnlyNumber_Time(event);");
             txtfare.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
         }
         protected void txtActualArrivalDate_TextChanged(object sender, EventArgs e)
         {
             lblmessage.Text = "";
             if (Convert.ToString(txtActualDeptDate.Text).Trim() == "")
             {
                 lblmessage.Text = "From date  cannot be blank";
                 return;
             }


             string[] tr_strdate;
             string tr_strfromDate = "";
             string tr_strToDate = "";

             if (hdnFreshByTD.Value == "N")
             {
                 #region TraelRequestdate formatting

                 if (Convert.ToString(txtFromdate.Text).Trim() != "")
                 {
                     tr_strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                     tr_strfromDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
                 }
                 if (Convert.ToString(txtToDate.Text).Trim() != "")
                 {
                     tr_strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                     tr_strToDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
                 }

                 #endregion
             }
             else
             {
                 #region TraelRequestdate formatting

                 if (Convert.ToString(Session["Fromdate"].ToString()).Trim() != "")
                 {
                     tr_strdate = Convert.ToString(Session["Fromdate"].ToString()).Trim().Split('/');
                     tr_strfromDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
                 }
                 if (Convert.ToString(Session["Todate"].ToString()).Trim() != "")
                 {
                     tr_strdate = Convert.ToString(Session["Todate"].ToString()).Trim().Split('/');
                     tr_strToDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
                 }

                 #endregion

             }


             lblmessage.Text = "";
             string[] strdate;
             string strfromDate = "";
             string strToDate = "";
             string message = "";
           
                 #region date formatting

                 if (Convert.ToString(txtActualDeptDate.Text).Trim() != "")
                 {
                     strdate = Convert.ToString(txtActualDeptDate.Text).Trim().Split('/');
                     strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                 }
                 if (Convert.ToString(txtActualArrivalDate.Text).Trim() != "")
                 {
                     strdate = Convert.ToString(txtActualArrivalDate.Text).Trim().Split('/');
                     strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                 }

                 #endregion
            
           
             
             
             DataSet dttraveletails = new DataSet();
             if (Convert.ToString(txtActualDeptDate.Text).Trim() != "" && Convert.ToString(txtActualArrivalDate.Text).Trim() != "")
             {
                 dttraveletails = spm.Get_Travel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);

                 if (dttraveletails.Tables[1].Rows.Count > 0)
                 {
                     message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);

                 }

                 if (Convert.ToString(message).Trim() != "")
                 {
                     lblmessage.Text = Convert.ToString(message).Trim();
                     txtActualDeptDate.Text = "";
                     txtActualArrivalDate.Text = "";


                     // hdnmsg.Value = lblmessage.Text;
                     return;
                 }
             }
         }
         protected void txtActualDeptDate_TextChanged(object sender, EventArgs e)
         {
             lblmessage.Text = "";
             string[] tr_strdate;
             string tr_strfromDate = "";
             string tr_strToDate = "";

             

             if (hdnFreshByTD.Value == "N")
             {
                 #region txtActualDeptDate formatting
                 if (Convert.ToString(txtFromdate.Text).Trim() != "")
                {
                 tr_strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                 tr_strfromDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
                }
                if (Convert.ToString(txtToDate.Text).Trim() != "")
                {
                 tr_strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                 tr_strToDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
                }
                 #endregion
             }
             else
             {
                 #region TraelRequestdate formatting

                 if (Convert.ToString(Session["Fromdate"].ToString()).Trim() != "")
                 {
                     tr_strdate = Convert.ToString(Session["Fromdate"].ToString()).Trim().Split('/');
                     tr_strfromDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
                 }
                 if (Convert.ToString(Session["Todate"].ToString()).Trim() != "")
                 {
                     tr_strdate = Convert.ToString(Session["Todate"].ToString()).Trim().Split('/');
                     tr_strToDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
                 }

                 #endregion

             }
            



             lblmessage.Text = "";
             string[] strdate;
             string strfromDate = "";
             string strToDate = "";
             string message = "";
             #region date formatting

             if (Convert.ToString(txtActualDeptDate.Text).Trim() != "")
             {
                 strdate = Convert.ToString(txtActualDeptDate.Text).Trim().Split('/');
                 strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
             }
             if (Convert.ToString(txtActualArrivalDate.Text).Trim() != "")
             {
                 strdate = Convert.ToString(txtActualArrivalDate.Text).Trim().Split('/');
                 strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
             }

             #endregion
             DataSet dttraveletails = new DataSet();
             if (Convert.ToString(txtActualDeptDate.Text).Trim() != "" && Convert.ToString(txtActualArrivalDate.Text).Trim() != "")
             {
                 dttraveletails = spm.Get_Travel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);

                 if (dttraveletails.Tables[1].Rows.Count > 0)
                 {
                     message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);

                 }

                 if (Convert.ToString(message).Trim() != "")
                 {
                     lblmessage.Text = Convert.ToString(message).Trim();
                     txtActualDeptDate.Text = "";
                     txtActualArrivalDate.Text = "";


                     // hdnmsg.Value = lblmessage.Text;
                     return;
                 }
             }
         }
         
        
       
       
         private void EnableFalse()
         {
             litripid.Visible = true;
             spantripid.Visible = true;
             txtTripId.Visible = true;
             litriptype.Visible = true;
             spantriptype.Visible = true;
             txtTravelType.Visible = true;

             txtTripId.Enabled = false;
             txtTravelType.Enabled = false;
             txtFromdate.Enabled = false;
             txtToDate.Enabled = false;
            // txtOrigin.Enabled = false;
             //txtDestination.Enabled = false;
             txtDeviation.Enabled = false;
             txtTravelMode.Enabled = false;
             txtRequirememt.Enabled = false;
             txtExpArrivalDate.Enabled = false;
             txtExpDepartDate.Enabled = false;
         }
         private void EnableTrue()
         {
             litripid.Visible = true;
             spantripid.Visible = true;
             txtTripId.Visible = true;
             litriptype.Visible = true;
             spantriptype.Visible = true;
             txtTravelType.Visible = true;

             txtTripId.Enabled = false;
             txtTravelType.Enabled = false;
             txtFromdate.Enabled = false;
             txtToDate.Enabled = false;
             txtOrigin.Enabled = true;
             txtDestination.Enabled = true;
             txtDeviation.Enabled = false;
             txtTravelMode.Enabled = true;
             txtRequirememt.Enabled = false;
             txtExpArrivalDate.Enabled = false;
             txtExpDepartDate.Enabled = false;
         }
       
    #endregion



     
}
