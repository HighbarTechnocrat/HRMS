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
using System.Data.SqlClient;
using System.Collections.Generic;
public partial class myaccount_AdditionalExpenses : System.Web.UI.Page
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

            //if (Convert.ToString(hdnIncidentalCharges.Value).Trim() != "")
            //    txtAmt_Oth.Text = Convert.ToString(hdnIncidentalCharges.Value).Trim();

            lblmsg.Visible = false;
            lblmessage.Text = "";
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/AdditionalExpenses");
            }
            else
            {
                txtpaidby.Text = "Employee";
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtExpdtls_Oth.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtpaidby.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtDeviation.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtAmt_Oth.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtnoofDays_Oth.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");

                    txtFromdate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
                   


                    editform.Visible = true;
                    divbtn.Visible = false;

                    //divmsg.Visible = false;
                    // lstDeviation.Enabled = false;

                    lblmessage.Text = "";

                    GetTripDetails();
                    GetTravelMode();
                    GetCities();
                    
                    txtEmpCode_Oth.Text = Convert.ToString(Session["ReqEmpCode"]);
                    txtTravelType_Oth.Text = Convert.ToString(Session["TravelType"]);
                    //   hdnTripid.Value = Convert.ToString(Session["TripID"]);
                    hdnfromdate_Oth.Value = Convert.ToString(Session["Fromdate"]);
                    hdnTodate_Oth.Value = Convert.ToString(Session["Todate"]);
                    txtFromdate.Text = hdnTodate_Oth.Value;


                    if (Convert.ToString(hdnTripid.Value).Trim() == "" || Convert.ToString(hdnTripid.Value).Trim() == "0")
                    {
                        hdnTryiptypeid.Value = Convert.ToString(Session["TripTypeId"]);
                        hflGrade_Oth.Value = Convert.ToString(Session["Grade"]);
                    }
                    else
                    {
                        getTravelDetails();
                    }
                   
                    if (Request.QueryString.Count > 0)
                    {
                        hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdntrdetailsid_Oth.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        hdnexpSrno_Oth.Value = Convert.ToString(Request.QueryString[2]).Trim();
                        hdnexp_id.Value = Convert.ToString(Request.QueryString[1]).Trim();

                        if (Convert.ToString(hdnexpSrno_Oth.Value).Trim() != "0")
                        {
                            get_Actual_DayDiff();
                            getTravelDetailsEdit();
                            Oth_btnSave.Text = "Update";
                        }
                    }

                    if (Convert.ToString(hdntrdetailsid_Oth.Value).Trim() == "0" || Convert.ToString(hdntrdetailsid_Oth.Value).Trim() == "")
                    {
                        Oth_btnDelete.Visible = false;
                    }

                    txtTravelType_Oth.Enabled = false;
                    if (Oth_btnSave.Text != "Update")
                    {
                       // ClientScript.RegisterStartupScript(Page.GetType(), "OnLoad", "SetCntrsl('No');", true);
                       // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "SetCntrsl('No')", true);
                        //ClientScript.RegisterStartupScript(Page.GetType(), "OnLoad", "SetDeviation('" + hdnDeviation.Value + "');", true);
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

        if (Convert.ToString(txtExpdtls_Oth.Text) == "Daily Halting Allowance" || Convert.ToString(txtExpdtls_Oth.Text) == "Incidental charges during travel")
        {
            lblnodays.Style.Add("display", "inline");
            txtnoofDays_Oth.Style.Add("display", "inline");
           
        }

    }

    protected void Oth_btnCancel_Click(object sender, EventArgs e)
    {
        //Server.Transfer("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value);
        //Server.Transfer("~/procs/TravelRequest_exp.aspx");
        //if (Convert.ToString(hdnpagereqestfrm.Value).Trim() == "0")
        //    Response.Redirect("~/procs/ExpenseWithoutTravlReqst.aspx?pgefrm=0", false);
        //else
        //    Response.Redirect("~/procs/TravelRequest_exp.aspx", false);
        Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value, false);


    }

    protected void lstTravelType_Oth_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTravelType_Oth.Text = lstTravelType_Oth.SelectedItem.Text;
        PopupControlExtender2.Commit(lstTravelType_Oth.SelectedItem.Text);
    }
    protected void lstExpdtls_Oth_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtExpdtls_Oth.Text = lstExpdtls_Oth.SelectedItem.Text;
        PopupControlExtender1.Commit(lstExpdtls_Oth.SelectedItem.Text);
        hdnselectionStatus_Oth.Value = "Yes";
        SetTravelDeviation();
        hdntxtAmt_Oth.Value = "0";
        
        
        GetIncidentalCharges(Convert.ToString(txtExpdtls_Oth.Text));
        
       
        if (Convert.ToString(txtExpdtls_Oth.Text) == "Daily Halting Allowance" || Convert.ToString(txtExpdtls_Oth.Text) == "Incidental charges during travel")
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetCntrsl('Yes','" + hdntxtAmt_Oth.Value + "');", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetCntrsl('No','" + hdntxtAmt_Oth.Value + "');", true);
        }



        



        
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

    protected void Oth_btnSave_Click(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";


        if (Convert.ToString(txtExpdtls_Oth.Text) == "Daily Halting Allowance" || Convert.ToString(txtExpdtls_Oth.Text) == "Incidental charges during travel")
        {
            strdate = Convert.ToString(txtnoofDays_Oth.Text).Trim().Split('.');
            if (strdate.Length > 2)
            {
                lblmessage.Text = "Please enter correct Actual stay duration (in days)";
                txtnoofDays_Oth.Text = "0";
                return;
            }

            if (Convert.ToString(txtnoofDays_Oth.Text).Trim() != "")
            {
                if (Convert.ToString(txtnoofDays_Oth.Text).Trim() != "0.00" || Convert.ToString(txtnoofDays_Oth.Text).Trim() != "0")
                {
                    if (Convert.ToString(txtnoofDays_Oth.Text).Trim() == "")
                    {
                        lblmessage.Text = "Please enter Actual stay duration (in days)";
                        txtnoofDays_Oth.Text = "";
                        return;
                    }
                    Decimal dfare = 0;
                    dfare = Convert.ToDecimal(txtnoofDays_Oth.Text);
                    if (dfare == 0)
                    {
                        lblmessage.Text = "Please enter Actual stay duration (in days)";
                        txtnoofDays_Oth.Text = "";
                        return;
                    }
                }
            }
        }


        if (Convert.ToString(txtAmt_Oth.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter the Amount";
            Session["chkTrvlAccLocalTrvlbtnStatus"] = "";
            if (Convert.ToString(txtExpdtls_Oth.Text) == "Daily Halting Allowance" || Convert.ToString(txtExpdtls_Oth.Text) == "Incidental charges during travel")
            {
                lblnodays.Style.Add("display", "inline");
                txtnoofDays_Oth.Style.Add("display", "inline");
            }
            else
            {
                lblnodays.Style.Add("display", "none");
                txtnoofDays_Oth.Style.Add("display", "none");
            }
            return;
        }
        if (Convert.ToString(txtExpdtls_Oth.Text) == "Daily Halting Allowance" || Convert.ToString(txtExpdtls_Oth.Text) == "Incidental charges during travel")
        {
            if(Convert.ToString(txtnoofDays_Oth.Text).Trim()=="" || Convert.ToString(txtnoofDays_Oth.Text).Trim()=="0" || Convert.ToString(txtnoofDays_Oth.Text).Trim()=="0.00")
            {
                lblmessage.Text = "Please enter correct Actual stay duration (in days)";
                Session["chkTrvlAccLocalTrvlbtnStatus"] = "";
                if (Convert.ToString(txtExpdtls_Oth.Text) == "Daily Halting Allowance" || Convert.ToString(txtExpdtls_Oth.Text) == "Incidental charges during travel")
                {
                    lblnodays.Style.Add("display", "inline");
                    txtnoofDays_Oth.Style.Add("display", "inline");
                }
                else
                {
                    lblnodays.Style.Add("display", "none");
                    txtnoofDays_Oth.Style.Add("display", "none");
                }
                return;
            }
        }

        if (Convert.ToString(txtAmt_Oth.Text).Trim() != "")
        {

            strdate = Convert.ToString(txtAmt_Oth.Text).Trim().Split('.');
            if (strdate.Length > 2)
            {
                lblmessage.Text = "Please enter correct Amount.";
                txtAmt_Oth.Text = "0";
                Session["chkTrvlAccLocalTrvlbtnStatus"] = "";
                if (Convert.ToString(txtExpdtls_Oth.Text) == "Daily Halting Allowance" || Convert.ToString(txtExpdtls_Oth.Text) == "Incidental charges during travel")
                {
                    lblnodays.Style.Add("display", "inline");
                    txtnoofDays_Oth.Style.Add("display", "inline");
                }
                else
                {
                    lblnodays.Style.Add("display", "none");
                    txtnoofDays_Oth.Style.Add("display", "none");
                }
                return;
            }
            Decimal dfare = 0;
            dfare = Convert.ToDecimal(txtAmt_Oth.Text);
            if (dfare == 0)
            {
                lblmessage.Text = "Please enter correct Amount.";
                Session["chkTrvlAccLocalTrvlbtnStatus"] = "";
                if (Convert.ToString(txtExpdtls_Oth.Text) == "Daily Halting Allowance" || Convert.ToString(txtExpdtls_Oth.Text) == "Incidental charges during travel")
                {
                    lblnodays.Style.Add("display", "inline");
                    txtnoofDays_Oth.Style.Add("display", "inline");
                }
                else
                {
                    lblnodays.Style.Add("display", "none");
                    txtnoofDays_Oth.Style.Add("display", "none");
                }
                return;
            }
        }

        if (Convert.ToString(txtnoofDays_Oth.Text).Trim() != "" && Convert.ToString(txtnoofDays_Oth.Text).Trim() != "0" && Convert.ToString(txtnoofDays_Oth.Text).Trim() != "0.00")
        {
            Decimal dnodays = Convert.ToDecimal(txtnoofDays_Oth.Text);
            Decimal dActnodays = Convert.ToDecimal(hdnDaysDiff_Oth.Value);
            if(dnodays>dActnodays)
            {
                txtnoofDays_Oth.Text = Convert.ToString(dActnodays);
            }
        }


        if (Convert.ToString(Session["chkTrvlAccLocalTrvlbtnStatus"]).Trim() != "")
        {
            Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value, false);
        }
        Session["chkTrvlAccLocalTrvlbtnStatus"] = "Additional Expenses button Event is Submitted";

      

        #region date formatting
        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(txttrvlLocation.Text).Trim() != "")
        {
            strdate = Convert.ToString(txttrvlLocation.Text).Trim().Split('/');
            //strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }



        #endregion

        decimal paidbycomp = 0;
        decimal paidbyemp = 0;
        txtpaidby.Text = "Employee";

        if (Convert.ToString(txtpaidby.Text).Trim() == "Employee")
        {
            if (Convert.ToString(txtAmt_Oth.Text).Trim() != "")
                paidbyemp = Math.Round(Convert.ToDecimal(txtAmt_Oth.Text), 2);
        }
        else
        {
            if (Convert.ToString(txtAmt_Oth.Text).Trim() != "")
                paidbycomp = Math.Round(Convert.ToDecimal(txtAmt_Oth.Text), 2);
        }

        Decimal dnoofDays = 0;

        if (Convert.ToString(txtnoofDays_Oth.Text).Trim()!="")
        dnoofDays = Convert.ToDecimal(txtnoofDays_Oth.Text);
        Boolean blnCheckAddnExp = false;

        #region Check Selected Additional Expeses already Submitted
             DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Check_addintional_exp_isSubmitted";

            spars[1] = new SqlParameter("@exp_sr_no", SqlDbType.Int);
            if (Convert.ToString(lstExpdtls_Oth.SelectedValue).Trim() != "")
                spars[1].Value = Convert.ToInt32(lstExpdtls_Oth.SelectedValue);
            else
                spars[1].Value = DBNull.Value;

            spars[2] = new SqlParameter("@exp_sr_a", SqlDbType.Int);
            if (Convert.ToString(hdnexpSrno_Oth.Value).Trim() != "")
                spars[2].Value = Convert.ToInt32(hdnexpSrno_Oth.Value);
            else
                spars[2].Value = "0";
        
              

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                blnCheckAddnExp = true;
            }
        #endregion

        if (Oth_btnSave.Text == "Submit")
        {
            //lstTravelType_Oth
            #region InsertTravelDetails
            Session["Destination"] = txtDestination.Text;
            //spm.InsertTravelDetails(Convert.ToInt32(hdnTryiptypeid.Value), Convert.ToInt32(lstExpdtls_Oth.SelectedValue), strfromDate, txtOrigin.Text, strToDate, txtDestination.Text, txtDeviation.Text, txtEmpCode_Oth.Text, 0, hdnCOS.Value, txtpaidby.Text, "InsertTempTable");
            //spm.InsertExpensesDetails(Convert.ToInt32(hdnTryiptypeid.Value), Convert.ToInt32(lstExpdtls_Oth.SelectedValue), strfromDate, txtOrigin.Text, strToDate, txtDestination.Text, txtDeviation.Text, txtEmpCode_Oth.Text, 0, hdnCOS.Value, txtpaidby.Text, "InsertTempTable_addionalExp");

            if (blnCheckAddnExp == true)
            {
                lblmessage.Text = "Selected Expense details already Submitted.";
                Session["chkTrvlAccLocalTrvlbtnStatus"] = "";
                if (Convert.ToString(txtExpdtls_Oth.Text) == "Daily Halting Allowance" || Convert.ToString(txtExpdtls_Oth.Text) == "Incidental charges during travel")
                {
                    lblnodays.Style.Add("display", "inline");
                    txtnoofDays_Oth.Style.Add("display", "inline");
                }
                else
                {
                    lblnodays.Style.Add("display", "none");
                    txtnoofDays_Oth.Style.Add("display", "none");
                }
                return;
            }
            else
            {
                spm.InsertExpensesDetails(txtEmpCode_Oth.Text, strfromDate, strfromDate, Convert.ToDecimal(hdnTripid.Value), Convert.ToString(txttrvlLocation.Text).Trim(), Convert.ToString(lstExpdtls_Oth.SelectedValue).Trim(), paidbycomp, paidbyemp, 0, 0, 0, Convert.ToString(txtTravelType_Oth.Text).Trim(), "", "InsertTempTable_addionalExp", Convert.ToString(txtRemarks_Oth.Text).Trim(), dnoofDays);

                Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value, false);
            }
            #endregion
        }
        else
        {
            #region UpdateTravelDetails
            //spm.UpdateTravelDetails(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdntrdetailsid.Value), Convert.ToInt32(lstExpdtls_Oth.SelectedValue), strfromDate, txtOrigin.Text, strToDate, txtDestination.Text, txtDeviation.Text, hdnCOS.Value, txtpaidby.Text, "UpdateMainTable");
            //Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);

            if (blnCheckAddnExp == true)
            {
                lblmessage.Text = "Selected Expense details already Submitted.";
                Session["chkTrvlAccLocalTrvlbtnStatus"] = "";
                if (Convert.ToString(txtExpdtls_Oth.Text) == "Daily Halting Allowance" || Convert.ToString(txtExpdtls_Oth.Text) == "Incidental charges during travel")
                {
                    lblnodays.Style.Add("display", "inline");
                    txtnoofDays_Oth.Style.Add("display", "inline");
                }
                else
                {
                    lblnodays.Style.Add("display", "none");
                    txtnoofDays_Oth.Style.Add("display", "none");
                }
                return;
            }
            else
            {
                spm.InsertExpensesDetails(txtEmpCode_Oth.Text, strfromDate, strfromDate, Convert.ToDecimal(hdnTripid.Value), Convert.ToString(txttrvlLocation.Text).Trim(), Convert.ToString(lstExpdtls_Oth.SelectedValue).Trim(), paidbycomp, paidbyemp, 0, 0, 0, Convert.ToString(txtTravelType_Oth.Text).Trim(), Convert.ToString(hdnexpSrno_Oth.Value).Trim(), "InsertTempTable_addionalExp", Convert.ToString(txtRemarks_Oth.Text).Trim(), dnoofDays);

                Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value, false);
            }
            #endregion
        }




    }
    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "From date  cannot be blank";
            return;
        }


        DateValidations();
    }
    #endregion

    #region Pagemethods
    public void GetTravelMode()
    {
        DataTable dtTripMode = new DataTable();
        dtTripMode = spm.getExpenseTravelMode();
        if (dtTripMode.Rows.Count > 0)
        {
            lstExpdtls_Oth.DataSource = dtTripMode;
            lstExpdtls_Oth.DataTextField = "exp_name";
            lstExpdtls_Oth.DataValueField = "expid";
            lstExpdtls_Oth.DataBind();

        }
    }
    public void GetIncidentalCharges( string detail)
    {
        string EMPGrade = Convert.ToString(Session["EMPAGrade"]).Trim();
        if (detail == "Incidental charges during travel")
        {
            DataTable dtTripCharges = new DataTable();
            dtTripCharges = spm.getExpenseTravelIncidentalCharges(EMPGrade);
            if (dtTripCharges.Rows.Count > 0)
            {
                txtAmt_Oth.Text = Convert.ToString(dtTripCharges.Rows[0]["Amount"]).Trim();
                hdntxtAmt_Oth.Value = Convert.ToString(dtTripCharges.Rows[0]["Amount"]).Trim();
            }
        }
        else
        {
            txtAmt_Oth.Text = "0";
            hdntxtAmt_Oth.Value = "0";
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


            lstLocation.DataSource = dtCities;
            lstLocation.DataTextField = "CITYNAME";
            lstLocation.DataValueField = "CITYID";
            lstLocation.DataBind();

        }
    }
    public void GetTripDetails()
    {
        DataTable dtTripDetails = new DataTable();
        dtTripDetails = spm.getTripDetails();
        if (dtTripDetails.Rows.Count > 0)
        {
            lstTravelType_Oth.DataSource = dtTripDetails;
            lstTravelType_Oth.DataTextField = "trip_description";
            lstTravelType_Oth.DataValueField = "trip_type";
            lstTravelType_Oth.DataBind();

        }
    }
    public void SetTravelDeviation()
    {
        //DataTable dtTripDev = new DataTable();
        //dtTripDev = spm.getTravelDeviation(hflGrade.Value, Convert.ToInt32(lstExpdtls_Oth.SelectedValue));
        //if (dtTripDev.Rows.Count > 0)
        //{
        hdnDeviation_Oth.Value = "No";

        //}
        //else
        //{
        //    hdnDeviation.Value = "Yes";


        //}
    }
    public void getTravelDetails()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetTravelDetails(txtEmpCode_Oth.Text);

        if (dtTrDetails.Rows.Count > 0)
        {
            txtExpdtls_Oth.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_mode"]);
            txtDeviation.Text = Convert.ToString(dtTrDetails.Rows[0]["deviation"]);
            txtpaidby.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_requirements"]);
            txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["departure_date"]);
            txttrvlLocation.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_date"]);
            txtTravelType_Oth.Text = Convert.ToString(Session["TravelType"]);
            hdnDesk_Oth.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_through_desk"]);
            txtOrigin.Text = Convert.ToString(dtTrDetails.Rows[0]["departure_place"]);
            txtDestination.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_place"]);

             

        }
    }
    public void getTravelDetailsEdit()
    {
        
        DataTable dtTrDetails = new DataTable();
       // dtTrDetails = spm.GetTravelDetailsEdit(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdntrdetailsid.Value));
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
       // if (Convert.ToString(hdnexp_id.Value).Trim() == "0")
            spars[0].Value = "sp_getExpens_dtlsEdit_Temp";
        //else
        //    spars[0].Value = "sp_getExpens_dtlsEdit";

        spars[1] = new SqlParameter("@exp_sr_no", SqlDbType.Int);
        spars[1].Value = hdnexpSrno_Oth.Value;

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = txtEmpCode_Oth.Text;
        dtTrDetails = spm.getDataList(spars, "SP_GETALL_Expense_DETAILS");

        if (dtTrDetails.Rows.Count > 0)
        {
            txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_frm_date"]);
            txtRemarks_Oth.Text = Convert.ToString(dtTrDetails.Rows[0]["exp_remarks"]).Trim();
            if (Convert.ToString(dtTrDetails.Rows[0]["paid_by_comp"]).Trim() == "0.00")
            {
                txtpaidby.Text = "Employee";
                lstpaidby.SelectedValue = "Employee";
                txtAmt_Oth.Text = Convert.ToString(dtTrDetails.Rows[0]["paid_emp"]).Trim();
            }
            else
            {
                txtAmt_Oth.Text = Convert.ToString(dtTrDetails.Rows[0]["paid_by_comp"]).Trim();
                txtpaidby.Text = "Company";
                lstpaidby.SelectedValue = "Company";
            }

            //
            txtExpdtls_Oth.Text = Convert.ToString(dtTrDetails.Rows[0]["exp_name"]).Trim();
            lstExpdtls_Oth.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["trip_details"]).Trim();
            txttrvlLocation.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_destination"]).Trim();
            txtnoofDays_Oth.Text = Convert.ToString(dtTrDetails.Rows[0]["no_of_days"]).Trim();

            //lblnodays.Visible = false;
            //txtnoofDays_Oth.Visible = false;
            
            lblnodays.Style.Add("display", "none");
            txtnoofDays_Oth.Style.Add("display", "none");
            //txtnoofDays_Oth.Visible = false;

            //if(Convert.ToString(txtnoofDays_Oth.Text).Trim()!="" && Convert.ToString(txtnoofDays_Oth.Text).Trim()!="0" && Convert.ToString(txtnoofDays_Oth.Text).Trim()!="0.00" )
            //{
            //    lblnodays.Visible = true;
            //    txtnoofDays_Oth.Visible = true;                
            //}

            if (Convert.ToString(txtExpdtls_Oth.Text) == "Daily Halting Allowance" || Convert.ToString(txtExpdtls_Oth.Text) == "Incidental charges during travel")
            {
                lblnodays.Style.Add("display", "inline");
                txtnoofDays_Oth.Style.Add("display", "inline");
            }
             

            /*txtExpdtls_Oth.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_mode"]);
            txtDeviation.Text = Convert.ToString(dtTrDetails.Rows[0]["deviation"]);
            txtpaidby.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_remarks"]);
            
            txttrvlLocation.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_date"]);
            txtTravelType_Oth.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_description"]);            
            txtOrigin.Text = Convert.ToString(dtTrDetails.Rows[0]["departure_place"]);
            txtDestination.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_place"]);

            */

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
    #endregion

    protected void DateValidations()
    {
        if (Convert.ToString(txtFromdate.Text).Trim() == "")
            return;

       


        string[] tr_strdate;
        string tr_strfromDate = "";
        string tr_strToDate = "";

        #region Exp.TraelRequestdate formatting

        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            tr_strdate = Convert.ToString(hdnfromdate_Oth.Value).Trim().Split('/');
            tr_strfromDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);

            tr_strdate = Convert.ToString(hdnTodate_Oth.Value).Trim().Split('/');
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
        

        #endregion
        DataSet dttraveletails = new DataSet();
        if (Convert.ToString(txtFromdate.Text).Trim() != "" )
        {
            dttraveletails = spm.Get_ExpTravel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strfromDate, txtEmpCode_Oth.Text, hdnTripid.Value, "AdditionalExp");

            if (dttraveletails.Tables[1].Rows.Count > 0)
            {
                message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);

            }

            if (Convert.ToString(message).Trim() != "")
            {
                lblmessage.Text = Convert.ToString(message).Trim();
                lblmessage.Visible = true;
                txtFromdate.Text = "";
                return;
            }

        }

    }
    protected void lstpaidby_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtpaidby.Text = lstpaidby.SelectedItem.Text;
        PopupControlExtender5.Commit(lstpaidby.SelectedItem.Text);

    }
    protected void Oth_btnDelete_Click(object sender, EventArgs e)
    {

        string[] strdate;
        string strfromDate = "";
      
        #region date formatting
        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        #endregion

        decimal paidbycomp = 0;
        decimal paidbyemp = 0;
        if (Convert.ToString(txtpaidby.Text).Trim() == "Employee")
        {
            if (Convert.ToString(txtAmt_Oth.Text).Trim() != "")
                paidbyemp = Convert.ToDecimal(txtAmt_Oth.Text);
        }
        else
        {
            if (Convert.ToString(txtAmt_Oth.Text).Trim() != "")
                paidbycomp = Convert.ToDecimal(txtAmt_Oth.Text);
        }

        if (Convert.ToString(Session["chkTrvlAccLocalTrvlbtnStatus"]).Trim() != "")
        {
            Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value, false);
        }
        Session["chkTrvlAccLocalTrvlbtnStatus"] = "Additional Expenses button Event is Submitted";

        //lstTravelType_Oth
        #region InsertTravelDetails           
        spm.InsertExpensesDetails(txtEmpCode_Oth.Text, strfromDate, strfromDate, Convert.ToDecimal(hdnTripid.Value), Convert.ToString(txttrvlLocation.Text).Trim(), Convert.ToString(lstExpdtls_Oth.SelectedValue).Trim(), paidbycomp, paidbyemp, 0, 0, 0, Convert.ToString(txtTravelType_Oth.Text).Trim(), Convert.ToString(hdnexpSrno_Oth.Value).Trim(), "DeleteTempTable_addionalExp", Convert.ToString(txtRemarks_Oth.Text).Trim(), 0);

        Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value, false);
        #endregion
         
    }

    protected void lstLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        txttrvlLocation.Text = lstLocation.SelectedItem.Text;
        PopupControlExtender6.Commit(lstLocation.SelectedItem.Text);
    }

    public void get_Actual_DayDiff()
    {
        string[] strdate;        
        strdate = Convert.ToString(txtnoofDays_Oth.Text).Trim().Split('.');
        if (strdate.Length > 2)
        {
            lblmessage.Text = "Please enter correct Actual stay duration (in days)";
            txtnoofDays_Oth.Text = "0";
            return;
        }
        if (Convert.ToString(txtnoofDays_Oth.Text).Trim() != "")
        {
            if (Convert.ToString(txtnoofDays_Oth.Text).Trim() != "0.00" || Convert.ToString(txtnoofDays_Oth.Text).Trim() != "0")
            {
                if (Convert.ToString(txtnoofDays_Oth.Text).Trim() == "")
                {
                    lblmessage.Text = "Please enter Actual stay duration (in days)";
                    return;
                }
                Decimal dfare = 0;
                dfare = Convert.ToDecimal(txtnoofDays_Oth.Text);
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
        if (Convert.ToString(hdnfromdate_Oth.Value).Trim() != "")
        {
            strdate = Convert.ToString(hdnfromdate_Oth.Value).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(hdnTodate_Oth.Value).Trim() != "")
        {
            strdate = Convert.ToString(hdnTodate_Oth.Value).Trim().Split('/');
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
            hdnDaysDiff_Oth.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["DayDiff"]);
            if (Convert.ToString(txtnoofDays_Oth.Text).Trim() != "")
            {
                Decimal dnodays = Convert.ToDecimal(txtnoofDays_Oth.Text);
                Decimal dActnodays = Convert.ToDecimal(hdnDaysDiff_Oth.Value);
                if (dnodays > dActnodays)
                {
                    txtnoofDays_Oth.Text = Convert.ToString(dActnodays);
                }
            }
            

        }
        


    }



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

                    strsql = " select top 10 CITYNAME from TBL_CITYMASTER where CLASS not in ('C') and " +
                                "  CITYNAME like   @SearchText + '%' order by CITYNAME ";
                else
                    strsql = " select top 10 CITYNAME from TBL_CITYMASTER where  CLASS in ('C') " +
                                "  and CITYNAME like   @SearchText + '%' order by CITYNAME ";
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
    protected void txtnoofDays_Oth_TextChanged(object sender, EventArgs e)
    {
        get_Actual_DayDiff();
    }
}