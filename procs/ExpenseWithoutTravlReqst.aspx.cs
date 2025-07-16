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

public partial class myaccount_ExpenseWithoutTravlReqst : System.Web.UI.Page
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

            lblmsg.Visible = false; ;
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/travelindex.aspx");
            }
            else
            {
                Page.SmartNavigation = true;

                if (!Page.IsPostBack)
                {
                    lblmessage.Text = "";
                    txtEmpCode.Text = Convert.ToString(Session["Empcode"]);
                    btnMod.Visible = false;
                    btnCancel.Visible = false;
                    btnback_mng.Visible = false;
                    //hdnTripid.Value = "";
                    this.lstApprover.SelectedIndex = 0;
                    hdnTravelConditionid.Value = "2";
                    hdnTraveltypeid.Value = "1";
                    hdnTrdays.Value = "2";
                    hdnEligible.Value = "Eligible";
                    //txtreqCur.Enabled = false;
                    GetEmployeeDetails();
                    GetTripDetails();

                 if (Request.QueryString.Count > 0)
                    {
                        hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                    }
                    //if (Convert.ToString(hdnTripid.Value).Trim() != "")
                    //{
                    //    hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                    //}
                    if (Convert.ToString(hdnTripid.Value).Trim() == "")
                    {
                        GetExpenseTravelDetails();

                         GetExpenseAccomodationDetails();
                         getExpenseLcoalTravel(); 
                    }
                    else if (Convert.ToString(hdnTripid.Value).Trim() != "")
                    {
                        trvl_btnSave.Visible = false;
                        //  btnBack.Visible = false;
                        btnMod.Visible = true;
                        btnCancel.Visible = true;
                        btnback_mng.Visible = true;
                       // hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        getMainTravelDetails();
                        getMainAccomodationDetails();
                        getMainLcoalTravel();
                        getTravelRequestData();
                        getApproverlist();
                        getTrStatus();
                        //if (hdnLeavestatusValue.Value == "" && Convert.ToString(hdnLeavestatusId.Value).Trim() == "1")
                        //{
                        //    trvl_accmo_btn.Enabled = false;
                        //    trvl_localbtn.Enabled = false;
                        //    btnTra_Details.Enabled = false;
                        //    dgTravelRequest.Enabled = false;
                        //    dgAccomodation.Enabled = false;
                        //    dgLocalTravel.Enabled = false;
                        //    btnCancel.Enabled = false;
                        //    btnMod.Enabled = false;
                        //}
                        //if (Convert.ToString(hdnLeavestatusId.Value).Trim() == "3" || Convert.ToString(hdnLeavestatusId.Value).Trim() == "4")
                        //{
                        //    trvl_accmo_btn.Enabled = false;
                        //    trvl_localbtn.Enabled = false;
                        //    btnTra_Details.Enabled = false;
                        //    dgTravelRequest.Enabled = false;
                        //    dgAccomodation.Enabled = false;
                        //    dgLocalTravel.Enabled = false;
                        //    btnCancel.Enabled = false;
                        //    btnMod.Enabled = false;
                        //}
                        //else if (Convert.ToString(hdnLeavestatusId.Value).Trim() == "2")
                        //{
                        //    trvl_accmo_btn.Enabled = false;
                        //    trvl_localbtn.Enabled = false;
                        //    btnTra_Details.Enabled = false;
                        //    dgTravelRequest.Enabled = false;
                        //    dgAccomodation.Enabled = false;
                        //    dgLocalTravel.Enabled = false;
                        //    btnCancel.Enabled = true;
                        //    btnMod.Enabled = false;
                        //}
                    }
                    else
                    {
                        spm.clear_temp_travel_tables(txtEmpCode.Text);
                    }
                    //else
                    //{
                    //    setVisibleCntrls();
                    //}
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

    protected void lstTripType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTriptype.Text = lstTripType.SelectedItem.Text;
        PopupControlExtender2.Commit(lstTripType.SelectedItem.Text);
        lblmessage.Text = "";
        hdnTraveltypeid.Value = "";
        hdnTraveltypeid.Value = lstTripType.SelectedValue;
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "validateTripType(" + hdnTraveltypeid.Value + ");", true);
    }
    protected void trvl_btnSave_Click(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";
        #region Check For Blank Fields
        lblmessage.Text = "";
        if (Convert.ToString(txtTriptype.Text).Trim() == "")
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
        if (Convert.ToString(txtReason.Text).Trim() == "")
        {
            lblmessage.Text = "Please Enter reason For Travel";
            return;
        }

        #endregion
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

        string reqcurrency = "";
        if (Convert.ToString(txtreqCur.Text).Trim() != "")
        {
            reqcurrency = txtreqCur.Text;
        }
        else
        {
            reqcurrency = "NA";
        }

        #region SaveTravel Expenses Code

        DataTable dtMaxexpID = new DataTable();
        int status = 1;

        string filename = "NULL";
        dtMaxexpID = spm.InsertExpenseTravelRequest_main(Convert.ToInt32(hdnTraveltypeid.Value), strfromDate, strToDate, txtReason.Text, txtEmpCode.Text, status, Convert.ToInt32(txtAdvance.Text), reqcurrency, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToDecimal(txtdailyhaltingallowance.Text), Convert.ToDecimal(txtTotAmtClaimed.Text), Convert.ToDecimal(txtLessAdvTaken.Text), Convert.ToDecimal(txtnetPaybltoComp.Text), Convert.ToDecimal(txtnetPaybltoEmp.Text), txtReasonDeviation.Text, filename);
        lblmessage.Visible = true;
        lblmessage.Text = "Travel Expense Submitted Successfully";        
        
                  
        //int maxexpid = Convert.ToInt32(dtMaxexpID.Rows[0]["maxtripid"]);
        //spm.InsertTravelDetails(0, 0, null, null, null, null, null, txtEmpCode.Text, maxexpid, null, null, "InsertMainTable");
        //if (dgAccomodation.Rows.Count > 0)
        //{
        //    spm.InsertAccomodationDetails(0, null, null, null, txtEmpCode.Text, maxexpid, null, null, "InsertMainTable");
        //}
        //if (dgLocalTravel.Rows.Count > 0)
        //{
        //    spm.InsertLocalTrvlDetails(0, null, null, null, txtEmpCode.Text, maxexpid, null, null, "InsertMainTable");
        //}
        //spm.InsertTravelApproverDetails(hflapprcode.Value, Convert.ToInt32(hdnApprId.Value), maxexpid);
        //getTravelDetails();
        //getAccomodationDetails();
        //getLcoalTravel();
        //spm.send_mailto_RM_TravelApprover(txtEmpName.Text, hflEmailAddress.Value, hdnApprEmailaddress.Value, "Request for Travel", txtTriptype.Text, hdnTrdays.Value, txtReason.Text, txtFromdate.Text, txtToDate.Text, "");
        
     
        //getTravelDetails();
        //getAccomodationDetails();
        //getLcoalTravel();
        ClearControls();
        #endregion
    }


    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "From date cannot be blank";
            return;
        }

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
            dttraveletails = spm.Get_TravelExpenseValidationResult(strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);

            if (dttraveletails.Tables[1].Rows.Count > 0)
            {
                message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);

            }

            if (Convert.ToString(message).Trim() != "")
            {
                lblmessage.Text = Convert.ToString(message).Trim();
                txtFromdate.Text = "";
                txtToDate.Text = "";


                // hdnmsg.Value = lblmessage.Text;
                return;
            }
        }
    }

    protected void btnTra_Details_Click(object sender, EventArgs e)
    {
      

        lblmessage.Text = "";
        if (Convert.ToString(lstTripType.SelectedValue).Trim() == "")
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
        if (Convert.ToString(txtReason.Text).Trim() == "")
        {
            lblmessage.Text = "Please Enter reason For Travel";
            return;
        }

        AssigningSessions();
        Response.Redirect("~/procs/ExpenseTravelDetails.aspx");
    }


    protected void trvl_accmo_btn_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";

       

        if (Convert.ToString(txtTriptype.Text).Trim() == "")
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
        if (Convert.ToString(txtReason.Text).Trim() == "")
        {
            lblmessage.Text = "Please Enter reason For Travel";
            return;
        }
        Session["Advance"] = txtAdvance.Text;
        if (Convert.ToString(txtreqCur.Text).Trim() != "")
        {
            Session["Currency"] = txtreqCur.Text;
        }
    
        AssigningSessions();
  
        Response.Redirect("~/procs/ExpenseAccomodation.aspx");
    }
    protected void trvl_localbtn_Click(object sender, EventArgs e)
    {
         AssigningSessions();
        if (Convert.ToString(txtTriptype.Text).Trim() == "")
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
        if (Convert.ToString(txtReason.Text).Trim() == "")
        {
            lblmessage.Text = "Please Enter reason For Travel";
            return;
        }
   

        Response.Redirect("~/procs/ExpenseLocalTravel.aspx");
    }

    protected void lnkLocalTravleEdit_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(hdnTripid.Value).Trim() != "")
        {
            AssigningSessions();
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnTripid.Value = Convert.ToString(dgLocalTravel.DataKeys[row.RowIndex].Values[0]).Trim();
            hdnLocalId.Value = Convert.ToString(dgLocalTravel.DataKeys[row.RowIndex].Values[1]).Trim();
            Response.Redirect("~/procs/ExpenseLocalTravel.aspx?tripid=" + hdnTripid.Value + "&Localid=" + hdnLocalId.Value);
        }
        else
        {
            AssigningSessions();
            Session["TripID"] = hdnTripid.Value;
            Response.Redirect("~/procs/ExpenseLocalTravel.aspx");
        }




    }
    protected void lnkAccomodationdit_Click(object sender, EventArgs e)
    {

        if (Convert.ToString(hdnTripid.Value).Trim() != "")
        {
            AssigningSessions();
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnTripid.Value = Convert.ToString(dgAccomodation.DataKeys[row.RowIndex].Values[0]).Trim();
            hdnAccId.Value = Convert.ToString(dgAccomodation.DataKeys[row.RowIndex].Values[1]).Trim();
            Response.Redirect("~/procs/Accomodation.aspx?tripid=" + hdnTripid.Value + "&accid=" + hdnAccId.Value);
        }
        else
        {
            Session["TripID"] = hdnTripid.Value;
            Response.Redirect("~/procs/Accomodation.aspx");
        }

    }
    protected void lnkTravelDetailsEdit_Click(object sender, EventArgs e)
    {

        if (Convert.ToString(hdnTripid.Value).Trim() != "")
        {
            AssigningSessions();
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnTripid.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[0]).Trim();
            hdnTravelDtlsId.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[1]).Trim();

            Response.Redirect("~/procs/TravelDetails.aspx?tripid=" + hdnTripid.Value + "&trdetailsid=" + hdnTravelDtlsId.Value);


        }
        else
        {
            Session["TripID"] = hdnTripid.Value;

            Response.Redirect("~/procs/TravelDetails.aspx");
        }






    }
    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        FromDateValidation();
    }

    private void FromDateValidation()
    {
        lblmessage.Text = "";
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";
        string message = "";
        hdnTrdays.Value = "0";
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
            dttraveletails = spm.Get_TravelExpenseValidationResult(strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);

            if (dttraveletails.Tables[0].Rows.Count > 0)
            {
                hdnTrdays.Value = Convert.ToString(dttraveletails.Tables[0].Rows[0]["TotalTravelDays"]);
                GetTravelContitionId();
            }

            if (dttraveletails.Tables[1].Rows.Count > 0)
            {
                message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);

            }

            if (Convert.ToString(message).Trim() != "")
            {
                lblmessage.Text = Convert.ToString(message).Trim();
                txtFromdate.Text = "";
                txtToDate.Text = "";

            }
        }
    }
    #endregion

    #region PageMethods

    public void GetTravelContitionId()
    {
        int TrConditionId = 0;
        TrConditionId = spm.getTravelConditionTypeId(Convert.ToInt32(hdnTraveltypeid.Value), hdnEligible.Value, Convert.ToDouble(hdnTrdays.Value));
        hdnTravelConditionid.Value = Convert.ToString(TrConditionId);

    }

    public void getIntermidateslist()
    {
        DataTable dtIntermediate = new DataTable();
        dtIntermediate = spm.GetTravelIntermediateName(txtEmpCode.Text, Convert.ToInt32(hdnTravelConditionid.Value), hflGrade.Value);
        if (dtIntermediate.Rows.Count > 0)
        {
            lstIntermediate.DataSource = dtIntermediate;
            lstIntermediate.DataTextField = "Emp_Name";
            lstIntermediate.DataValueField = "APPR_ID";
            lstIntermediate.DataBind();
        }

    }
    public void getApproverdata()
    {
        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.GeTraveltApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value));
        //IsEnabledFalse (true);
        lstApprover.Items.Clear();
        if (dtApproverEmailIds.Rows.Count > 0)
        {
            hdnApprEmailaddress.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["Emp_Emailaddress"]);
            hdnApprId.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["APPR_ID"]);

            //lstApprover.DataSource = null;
            //lstApprover.DataBind();

            lstApprover.DataSource = dtApproverEmailIds;
            lstApprover.DataTextField = "Emp_Name";
            lstApprover.DataValueField = "APPR_ID";
            lstApprover.DataBind();

            hflapprcode.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["A_EMP_CODE"]);


        }
        else
        {
            lblmessage.Text = "Since you are not assigned under any approver you can not apply for any type of Travel request, please contact HR";

        }


    }
    public void GetEmployeeDetails()
    {
        try
        {
            DataTable dtEmpDetails = new DataTable();
            dtEmpDetails = spm.GetEmployeeData(txtEmpCode.Text);

            if (dtEmpDetails.Rows.Count > 0)
            {
                txtEmpName.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
                hflEmpDesignation.Value = Convert.ToString(dtEmpDetails.Rows[0]["DesginationName"]);
                hflEmpDepartment.Value = Convert.ToString(dtEmpDetails.Rows[0]["Department_Name"]);
                hflGrade.Value = Convert.ToString(dtEmpDetails.Rows[0]["Grade"]).Trim();
                hflEmailAddress.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);

            }

            //  getApproverdata();  
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }
    public void GetTripDetails()
    {
        DataTable dtTripDetails = new DataTable();
        dtTripDetails = spm.getTripDetails();
        if (dtTripDetails.Rows.Count > 0)
        {
            lstTripType.DataSource = dtTripDetails;
            lstTripType.DataTextField = "trip_description";
            lstTripType.DataValueField = "trip_type";
            lstTripType.DataBind();

        }
    }

    public void GetExpenseTravelDetails()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetExpenseTravelDetails(txtEmpCode.Text);

        dgTravelRequest.DataSource = null;
        dgTravelRequest.DataBind();

        if (dtTrDetails.Rows.Count > 0)
        {
            //hdnTripid.Value = Convert.ToString(dtTrDetails.Rows[0]["trip_id"]);
            //txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["departure_date"]);
            //txtToDate.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_date"]);
            txtTriptype.Text = Convert.ToString(Session["TravelType"]);
            lstTripType.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["trip_mode_id"]);

            hdnTraveltypeid.Value = lstTripType.SelectedValue;
          //  hdnDesk.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_through_desk"]);
            txtReason.Text = Convert.ToString(Session["Reason"]);
            hdnDestnation.Value = Convert.ToString(dtTrDetails.Rows[0]["arrival_place"]);
            hdnDeptPlace.Value = Convert.ToString(dtTrDetails.Rows[0]["departure_place"]);
            hdnTravelmode.Value = Convert.ToString(dtTrDetails.Rows[0]["trip_mode_id"]);
            hdnDeviation.Value = Convert.ToString(dtTrDetails.Rows[0]["Deviation"]);
            hdnTrDetRequirements.Value = Convert.ToString(dtTrDetails.Rows[0]["trip_requirements"]);
            txtFromdate.Text = Convert.ToString(Session["Fromdate"]);
            txtToDate.Text = Convert.ToString(Session["Todate"]);
            hdnTraveltypeid.Value = Convert.ToString(Session["Tr_type_id"]);

            if (Convert.ToString(txtTriptype.Text).Trim() == "Domestic" || Convert.ToString(txtTriptype.Text).Trim() == "")
            {
                txtreqCur.Visible = false;
                lbl_cur.Visible = false;
            }
            else
            {
                txtreqCur.Visible = true;
                lbl_cur.Visible = true;
            }

            if (Convert.ToString(hdnDeviation.Value).Trim() == "Yes")
            {
                hdnEligible.Value = "Deviation";
            }
            else
            {
                hdnEligible.Value = "Eligible";

            }
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                FromDateValidation();
            }

            dgTravelRequest.DataSource = dtTrDetails;
            dgTravelRequest.DataBind();
        }
    }

    public void GetExpenseAccomodationDetails()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetExpenseAccomodationDetails(txtEmpCode.Text);

        dgAccomodation.DataSource = null;
        dgAccomodation.DataBind();

        if (dtTrDetails.Rows.Count > 0)
        {

            txtAdvance.Text = Convert.ToString(Session["Advance"]);
            hdnAccReq.Value = Convert.ToString(dtTrDetails.Rows[0]["local_accomodation_remarks"]);
            hdnAccCOS.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_through_desk"]);
            hdnAcctripid.Value = Convert.ToString(dtTrDetails.Rows[0]["trip_id"]);

            if (Convert.ToString(txtreqCur.Text).Trim() != "")
            {
                txtreqCur.Text = Convert.ToString(Session["Currency"]); ;
            }
            dgAccomodation.DataSource = dtTrDetails;
            dgAccomodation.DataBind();
        }
    }

    public void getExpenseLcoalTravel()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetExpenseLocalTrvlDetails(txtEmpCode.Text);

        dgLocalTravel.DataSource = null;
        dgLocalTravel.DataBind();

        if (dtTrDetails.Rows.Count > 0)
        {

            //hdnLcalTripid.Value = Convert.ToString(dtTrDetails.Rows[0]["trip_id"]); 

            hdnlocaltrReq.Value = Convert.ToString(dtTrDetails.Rows[0]["remarks"]);
            //hdnlocalTrCOS.Value = Convert.ToString(dtTrDetails.Rows[0]["is_thorugh_cos"]);

            dgLocalTravel.DataSource = dtTrDetails;
            dgLocalTravel.DataBind();
        }
    }



    public void ClearControls()
    {
        txtTriptype.Text = "";
        txtFromdate.Text = "";
        txtToDate.Text = "";
        txtAdvance.Text = "";
        txtReason.Text = "";
        txtreqCur.Text = "";

    }

    public void AssigningSessions()
    {
        Session["TravelType"] = txtTriptype.Text;
        Session["Fromdate"] = txtFromdate.Text;
        Session["Todate"] = txtToDate.Text;
        Session["ReqEmpCode"] = txtEmpCode.Text;
        Session["reasntrvl"] = txtReason.Text;
        Session["Grade"] = hflGrade.Value;
        Session["Reason"] = txtReason.Text;
        Session["TripTypeId"] = Convert.ToString(lstTripType.SelectedValue);
        Session["Location"] = hdnDestnation.Value;
        Session["TripID"] = hdnAcctripid.Value;
        Session["TripID"] = hdnLcalTripid.Value;
        Session["DestLocation"] = hdnDeptPlace.Value;
        hdnTraveltypeid.Value = lstTripType.SelectedValue;
        Session["Tr_type_id"] = hdnTraveltypeid.Value;
  
    }
    public void AssingSessionto_controls()
    {
        txtTriptype.Text = Convert.ToString(Session["TravelType"]).Trim();
        txtFromdate.Text = Convert.ToString(Session["Fromdate"]).Trim();
        txtToDate.Text = Convert.ToString(Session["Todate"]).Trim();
        txtEmpCode.Text = Convert.ToString(Session["ReqEmpCode"]).Trim();
        txtReason.Text = Convert.ToString(Session["reasntrvl"]).Trim();

    }
    private void getApproverlist()
    {
        DataTable dtapprover = new DataTable();
        dtapprover = spm.GetTravelApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
        lstApprover.Items.Clear();
        if (dtapprover.Rows.Count > 0)
        {
            lstApprover.DataSource = dtapprover;
            lstApprover.DataTextField = "names";
            lstApprover.DataValueField = "names";
            lstApprover.DataBind();

        }
        else
        {
            lblmessage.Text = "There is no request for approver.";
        }
    }

    private void setVisibleCntrls()
    {
        spntrvldtls.Visible = true;
        btnTra_Details.Visible = true;
        spnadvrequired.Visible = true;
        txtAdvance.Visible = true;
        spnaccomodation.Visible = true;
        trvl_accmo_btn.Visible = true;
        spnlocalTrvl.Visible = true;
        trvl_localbtn.Visible = true;
        dgLocalTravel.Visible = true;
        dgAccomodation.Visible = true;
        litrvldetail.Visible = true;
        litrvlgrid.Visible = true;
        dgTravelRequest.Visible = true;
        litrvlAdvnce.Visible = true;
        litrvlcurrency.Visible = true;
        litrvlaccomodation.Visible = true;
        litrvlgridAccomodation.Visible = true;

        lsttrvlapprover.Visible = false;
        lsttrvlIntermidates.Visible = false;

    }
    #endregion

    #region ModifyTravelRequestEvents
    protected void btnMod_Click(object sender, EventArgs e)
    {

        string[] strdate;
        string strfromDate = "";
        string strToDate = "";
        #region Check For Blank Fields
        lblmessage.Text = "";
        if (Convert.ToString(txtTriptype.Text).Trim() == "")
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
        if (Convert.ToString(txtReason.Text).Trim() == "")
        {
            lblmessage.Text = "Please Enter reason For Travel";
            return;
        }

        #endregion
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

        if (Convert.ToString(txtFromdate.Text).Trim() != "" && Convert.ToString(txtToDate.Text).Trim() != "")
        {
            string message = "";
            DataSet dttraveletails = new DataSet();
            dttraveletails = spm.Get_TravelValidationResult(strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);

            if (dttraveletails.Tables[1].Rows.Count > 0)
            {
                message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);

            }

            if (Convert.ToString(message).Trim() != "")
            {
                lblmessage.Text = Convert.ToString(message).Trim();
                txtFromdate.Text = "";
                txtToDate.Text = "";


                // hdnmsg.Value = lblmessage.Text;
                return;
            }

        }

        #endregion

       
    }
    #endregion

    #region ModifyTravelMethods
    public void getMainTravelDetails()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetMainTravelDetails(Convert.ToInt32(hdnTripid.Value));

        dgTravelRequest.DataSource = null;
        dgTravelRequest.DataBind();

        if (dtTrDetails.Rows.Count > 0)
        {
            hdnTravelDtlsId.Value = Convert.ToString(dtTrDetails.Rows[0]["trip_dtls_id"]);

            for (int i = 0; i < dtTrDetails.Rows.Count; i++)
            {

                hdnTravelDtlsId.Value = Convert.ToString(dtTrDetails.Rows[i]["trip_dtls_id"]).Trim();

            }

            dgTravelRequest.DataSource = dtTrDetails;
            dgTravelRequest.DataBind();
        }
    }

    public void getMainAccomodationDetails()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetMainAccomodationDetails(Convert.ToInt32(hdnTripid.Value));

        dgAccomodation.DataSource = null;
        dgAccomodation.DataBind();

        if (dtTrDetails.Rows.Count > 0)
        {
            hdnAccId.Value = Convert.ToString(dtTrDetails.Rows[0]["local_accomodation_id"]);
            dgAccomodation.DataSource = dtTrDetails;
            dgAccomodation.DataBind();
        }
    }

    public void getMainLcoalTravel()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetMainLocalTrvlDetails(Convert.ToInt32(hdnTripid.Value));

        dgLocalTravel.DataSource = null;
        dgLocalTravel.DataBind();

        if (dtTrDetails.Rows.Count > 0)
        {

            dgLocalTravel.DataSource = dtTrDetails;
            dgLocalTravel.DataBind();
        }
    }

    public void getTrStatus()
    {
        DataTable dttrstatus = new DataTable();

        dttrstatus = spm.GetTravelStatus(Convert.ToInt32(hdnTripid.Value));

        if (dttrstatus.Rows.Count > 0)
        {
            hdnTravelstatus.Value = Convert.ToString(dttrstatus.Rows[0]["Status"]);
            //hflstatusid.Value = (string)dtlvstatus.Rows[0]["Status_id"];
            //approveremailaddress = (string)dtlvstatus.Rows[0]["EmailAddress"];

        }

        //for (int i = 0; i < dtlvstatus.Rows.Count; i++)
        //{
        //    if (Convert.ToString(dtlvstatus.Rows[i]["Status"]).Trim() == "Approved")
        //    {
        //        if (Convert.ToString(hflApproverEmail.Value).Trim() == "")
        //        {
        //            hflApproverEmail.Value = Convert.ToString(dtlvstatus.Rows[i]["EmailAddress"]).Trim();
        //            hflLeavestatus.Value = Convert.ToString(dtlvstatus.Rows[i]["Status"]).Trim();
        //            hflstatusid.Value = Convert.ToString(dtlvstatus.Rows[i]["Status_id"]).Trim();
        //        }
        //        else
        //        {
        //            hflApproverEmail.Value += ";" + Convert.ToString(dtlvstatus.Rows[i]["EmailAddress"]).Trim();
        //            hflLeavestatus.Value = Convert.ToString(dtlvstatus.Rows[i]["Status"]).Trim();
        //            hflstatusid.Value = Convert.ToString(dtlvstatus.Rows[i]["Status_id"]).Trim();
        //        }

        //    }
        //    else
        //    {
        //        hflApproverEmail.Value = Convert.ToString(dtlvstatus.Rows[i]["EmailAddress"]).Trim();
        //        hflLeavestatus.Value = Convert.ToString(dtlvstatus.Rows[i]["Status"]).Trim();
        //        hflstatusid.Value = Convert.ToString(dtlvstatus.Rows[i]["Status_id"]).Trim();
        //    }
        //}
    }
    public void getTravelRequestData()
    {



        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetTravelReqData(Convert.ToInt32(hdnTripid.Value));

        if (dtTrDetails.Rows.Count > 0)
        {
            txtTriptype.Text = Convert.ToString(dtTrDetails.Rows[0]["Type"]);
            txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["FromDate"]);
            txtToDate.Text = Convert.ToString(dtTrDetails.Rows[0]["ToDate"]);
            txtReason.Text = Convert.ToString(dtTrDetails.Rows[0]["trp_reason"]);
            txtAdvance.Text = Convert.ToString(dtTrDetails.Rows[0]["req_adv_amt"]);
            txtreqCur.Text = Convert.ToString(dtTrDetails.Rows[0]["currency_type"]);
            hdnTravelConditionid.Value = Convert.ToString(dtTrDetails.Rows[0]["tr_Conditionid"]);

        }
    }
    #endregion

    protected void lnkbtn_expdtls_Click(object sender, EventArgs e)
    {
        //AdditionalExpenses
        AssigningSessions();
        Response.Redirect("~/procs/AdditionalExpenses.aspx?pgereq=0");
    }
}