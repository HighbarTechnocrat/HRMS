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

public partial class TravelRequest : System.Web.UI.Page
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




                Session["chkTrvlAccLocalTrvlbtnStatus"] = "";

                if (Convert.ToString(Session["Empcode"]).Trim() == "")
                    Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");


                if (Page.User.Identity.IsAuthenticated == false)
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/travelindex");
                }
                else
                {
                    Page.SmartNavigation = true;

                    if (!Page.IsPostBack)
                    {
                        txtTriptype.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                        txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                        txtToDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                        txtAdvance.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                       // txtreqCur.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");

                        txtFromdate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
                        txtToDate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");

                        txtTravelMode.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                        txtFromdate_Trvl.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                        txtToDate_Trvl.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                        //  txtDestination.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                        // txtOrigin.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                        txtDeviation.Attributes.Add("onkeypress", "return noanyCharecters(event);");

                        txtFromdate_Trvl.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
                        txtToDate_Trvl.Attributes.Add("onkeyDown", "return noanyCharecters(event);");

                        txtDeviation.Enabled = false;
                        trvldeatils_delete_btn.Visible = false;

                        lblmessage.Text = "";
                        txtEmpCode.Text = Convert.ToString(Session["Empcode"]);
                        btnMod.Visible = false;
                        btnCancel.Visible = false;
                        btnback_mng.Visible = false;
                        //hdnTripid.Value = "";
                        this.lstApprover.SelectedIndex = 0;
                        hdnTravelConditionid.Value = "1";
                        hdnTraveltypeid.Value = "1";
                        //hdnTrdays.Value = "4";
                        hdnTrdays.Value = "2";
                        hdnEligible.Value = "Eligible";
                        //txtreqCur.Enabled = false;

                        if (Convert.ToString(Session["TripTypeId"]) != null && Convert.ToString(Session["TripTypeId"]).Trim() != "")
                        {
                            setSessionsVariables();

                        }

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

                        GetEmployeeDetails();
                        GetTripDetails();
                        if (Request.QueryString.Count > 0)
                        {
                            if (Convert.ToString(Request.QueryString[0]).Trim() != "0" && Convert.ToString(Request.QueryString[0]).Trim() != "")
                                hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        }
                         
                           
                       

                        if (Request.QueryString.Count > 2)
                        {
                            
                            InsertMaintotemp();
                        }
                        //else
                        //{
                        //    spm.clear_temp_travel_tables(txtEmpCode.Text);
                        //}

                        if (Convert.ToString(hdnTripid.Value).Trim() == "" && Convert.ToString(hdnTripid.Value).Trim() != "0")
                        {
                            getTravelDetails();
                            getAccomodationDetails();
                            getLcoalTravel();
                         
                        }


                        if (Convert.ToString(hdnTripid.Value).Trim() != "0" && Convert.ToString(hdnTripid.Value).Trim()!="")
                        {
                            trvl_btnSave.Visible = false;
                            //  btnBack.Visible = false;
                            btnMod.Visible = true;
                            btnCancel.Visible = true;
                            btnback_mng.Visible = true;
                            // hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();+
                           
                            //getMainTravelDetails();
                            //getMainAccomodationDetails();
                            //getMainLcoalTravel();
                            getTravelRequestData();
                            getApproverlist();
                            getTrStatus();
                            getTravelDetails();
                            getAccomodationDetails();
                            getLcoalTravel();

                            sprtfunctions.Visible = true;
                            lstApprover_suprt.Visible = true;
                            get_TDCOSACC_Approval_Status();

                            if (hdnLeavestatusValue.Value == "Pending" && Convert.ToString(hdnLeavestatusId.Value).Trim() == "1")
                            {
                                trvl_accmo_btn.Enabled = true;
                                trvl_localbtn.Enabled = true;
                                btnTra_Details.Enabled = true;
                                dgTravelRequest.Enabled = true;
                                dgAccomodation.Enabled = true;
                                dgLocalTravel.Enabled = true;                                
                                btnCancel.Visible = true;
                                btnMod.Visible = true;
                            }
                            if (Convert.ToString(hdnLeavestatusId.Value).Trim() == "3" || Convert.ToString(hdnLeavestatusId.Value).Trim() == "4")
                            {
                                //trvl_accmo_btn.Enabled = false;
                                //trvl_localbtn.Enabled = false;
                                //btnTra_Details.Enabled = false;
                                //dgTravelRequest.Enabled = false;
                                //dgAccomodation.Enabled = false;
                                //dgLocalTravel.Enabled = false;
                                trvl_accmo_btn.Visible = false;
                                trvl_localbtn.Visible = false;
                                btnTra_Details.Visible = false;
                                dgTravelRequest.Enabled = false;
                                dgAccomodation.Enabled = false;
                                dgLocalTravel.Enabled = false;
                                txtAdvance.Enabled = false;
                                txtreqCur.Enabled = false;
                                txtReason.Enabled = false;
                                txtFromdate.Enabled = false;
                                txtToDate.Enabled = false;
                                txtTriptype.Enabled = false;
                                Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                                txtTriptype.BackColor = color;
                                btnCancel.Visible = false;
                                btnMod.Visible = false;
                            }
                            else if (Convert.ToString(hdnLeavestatusId.Value).Trim() == "2")
                            {
                                trvl_accmo_btn.Visible = false;
                                trvl_localbtn.Visible = false;
                                btnTra_Details.Visible = false;
                                dgTravelRequest.Enabled = false;
                                dgAccomodation.Enabled = false;
                                dgLocalTravel.Enabled = false;
                                txtAdvance.Enabled = false;
                                txtreqCur.Enabled = false;
                                txtReason.Enabled = false;
                                txtFromdate.Enabled = false;
                                txtToDate.Enabled = false;
                                txtTriptype.Enabled = false;
                                Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                                txtTriptype.BackColor = color;
                                btnCancel.Visible = true;
                                btnMod.Visible = false;
//                                lnksubTrvlReqst_exp.Visible = true;
                                CheckTravelRequest_Submitted_Expese();

                            }
                            if (Convert.ToString(hdnLeavestatusValue.Value).Trim() == "Approved" && Convert.ToString(hdnLeavestatusId.Value).Trim() == "1")
                            {
                                //trvl_accmo_btn.Enabled = false;
                                //trvl_localbtn.Enabled = false;
                                //btnTra_Details.Enabled = false;
                                //dgTravelRequest.Enabled = false;
                                //dgAccomodation.Enabled = false;
                                //dgLocalTravel.Enabled = false;  
                                trvl_accmo_btn.Visible = false;
                                trvl_localbtn.Visible = false;
                                btnTra_Details.Visible = false;
                                dgTravelRequest.Enabled = false;
                                dgAccomodation.Enabled = false;
                                dgLocalTravel.Enabled = false;
                                txtAdvance.Enabled = false;
                                txtreqCur.Enabled = false;
                                txtReason.Enabled = false;
                                txtFromdate.Enabled = false;
                                txtToDate.Enabled = false;
                                txtTriptype.Enabled = false;
                                Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                                txtTriptype.BackColor = color;   
                          
                                btnMod.Visible = false;
                                btnCancel.Visible = false;
                            }  
                        }

                        if (hdnTDCOSStatus.Value == "Pending" && Convert.ToString(hdnLeavestatusId.Value).Trim() == "2")
                        {
                            trvl_accmo_btn.Visible = true;
                            trvl_localbtn.Visible = true;
                            btnTra_Details.Visible = true;

                            trvl_accmo_btn.Enabled = true;
                            trvl_localbtn.Enabled = true;
                            btnTra_Details.Enabled = true;
                            dgTravelRequest.Enabled = true;
                            dgAccomodation.Enabled = true;
                            dgLocalTravel.Enabled = true;
                            btnCancel.Visible = true;
                            btnMod.Visible = true;

                            txtAdvance.Enabled = true;
                            txtreqCur.Enabled = true;
                            txtReason.Enabled = true;
                            txtFromdate.Enabled = true;
                            txtToDate.Enabled = true;
                            txtTriptype.Enabled = false;
                            Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                            txtTriptype.BackColor = color;
                           

                        }

                        if (dgTravelRequest.Rows.Count>0)
                        {
                            for(Int32 irow=0;irow<dgTravelRequest.Rows.Count;irow++)
                            {
                                if (Convert.ToString(dgTravelRequest.Rows[irow].Cells[6].Text).Trim() == "Yes")
                                {
                                    hdnEligible.Value = "Deviation";                                    
                                }
                            }
                        }
                        if (Convert.ToString(hdnTripid.Value).Trim() =="")
                        GetTravelContitionId();

                        
                        //getApproverdata();
                        //getIntermidateslist();
                        
                        editform.Visible = true;
                        divbtn.Visible = false;

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
            
            lblmessage.Text = "";
            hdnTraveltypeid.Value = "";
            hdnTraveltypeid.Value = lstTripType.SelectedValue;
            PopupControlExtender2.Commit(lstTripType.SelectedItem.Text);
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "validateTripType(" + hdnTraveltypeid.Value + ");", true);
        }
        protected void trvl_btnSave_Click(object sender, EventArgs e)
        {
            string[] strdate;
            string strfromDate = "";
            string strToDate = "";

            if (FromDateValidation()==true)
            {
                return;
            }

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
                lblmessage.Text = "Please enter reason For Travel";
                return;
            }

            if (dgTravelRequest.Rows.Count ==0 )
            {
                lblmessage.Text = "Please enter the Travel details.";
                return;
            }

            if(Convert.ToString(hdnTraveltypeid.Value).Trim()=="2")
            {
                if (Convert.ToString(txtAdvance.Text).Trim() != "")
                {
                    if (Convert.ToString(txtreqCur.Text).Trim() == "")
                    {
                        lblmessage.Text = "Please enter Currency Required For Travel";
                        return;
                    }
                }
            }
            if (Convert.ToString(txtAdvance.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtAdvance.Text).Trim().Split('.');
                if (strdate.Length > 2)
                {
                    txtAdvance.Text = "0";
                    lblmessage.Text = "Please enter correct amount.";
                    return;
                }

            }


             

            #endregion

            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            #region date formatting
            strfromDate = "";
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


            //if (Convert.ToString(Session["chkbtnStatus"]).Trim() != "")
            //{
            //    Response.Redirect("~/procs/travelindex.aspx");
            //}
            //Session["chkbtnStatus"] = "Travel Request button Event is Submitted";

            

            //getFomatated dates for Email
            getFromdateTodate_FroEmail();
             
            #region SaveTravelRequest Code
            Decimal dadvReq = 0;
            if (Convert.ToString(txtAdvance.Text).Trim() != "")
                dadvReq = Math.Round(Convert.ToDecimal(txtAdvance.Text), 2);

            hdnTraveltypeid.Value = lstTripType.SelectedValue;
            DataTable dtMaxTripID = new DataTable();
            int status = 1;
            dtMaxTripID = spm.InsertTravelRequest(Convert.ToInt32(hdnTraveltypeid.Value), strfromDate, strToDate, txtReason.Text, txtEmpCode.Text, status, dadvReq, reqcurrency, Convert.ToInt32(hdnTravelConditionid.Value));
            int maxtripid = Convert.ToInt32(dtMaxTripID.Rows[0]["maxtripid"]);
            if (Convert.ToString(maxtripid).Trim() == "" || Convert.ToString(maxtripid).Trim() == "0")
                return;
            spm.InsertTravelDetails(0, 0, null, null, null, null, null, txtEmpCode.Text, maxtripid, null, null, "InsertMainTable","","");
            if (dgAccomodation.Rows.Count > 0)
            {
                spm.InsertAccomodationDetails(0, null, null, null, txtEmpCode.Text, maxtripid, null, null, "InsertMainTable");
            }
            if (dgLocalTravel.Rows.Count > 0)
            {
                spm.InsertLocalTrvlDetails(0, null, null, null, txtEmpCode.Text, maxtripid, null, null, "InsertMainTable");
            }
            spm.InsertTravelApproverDetails(hflapprcode.Value, Convert.ToInt32(hdnApprId.Value), maxtripid,"");
           // getTravelDetails();
            getAccomodationDetails();
            getLcoalTravel();
            getTravelDetails();

            String strTrvlrestURL = "";
            strTrvlrestURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_TR"]).Trim() + "?reqid=" + maxtripid;

         


            //spm.send_mailto_RM_TravelApprover(txtEmpName.Text, hflEmailAddress.Value, hdnApprEmailaddress.Value, "Request for Travel", txtTriptype.Text, hdnTrdays.Value, txtReason.Text, txtFromdate.Text, txtToDate.Text, "");
            spm.send_mailto_RM_TravelApprover(txtEmpName.Text, hflEmailAddress.Value, hdnApprEmailaddress.Value, "Request for Travel", txtTriptype.Text, hdnActualTrvlDays.Value, txtReason.Text, Convert.ToString(hdnfrmdate_emial.Value), Convert.ToString(hdntodate_emial.Value), "", GetApprove_RejectList(maxtripid), GetIntermediatesList(), strTrvlrestURL);
            string strapprovermails = "";
            strapprovermails = get_approverlist_ifTD_COS(maxtripid);
            if (Convert.ToString(strapprovermails).Trim()!="")
            {                
                spm.Travel_send_mailto_Intermediate(hflEmailAddress.Value, strapprovermails, "Request for Travel", txtTriptype.Text, hdnActualTrvlDays.Value, txtReason.Text, Convert.ToString(hdnfrmdate_emial.Value).Trim(), Convert.ToString(hdntodate_emial.Value).Trim(), GetApprove_RejectList(maxtripid), txtEmpName.Text, GetIntermediatesList());
            }
            lblmessage.Visible = true;
            lblmessage.Text = "Travel Reuqest Submitted Successfully";
            Response.Redirect("~/procs/travelindex.aspx");
            //getTravelDetails();
            //getAccomodationDetails();
            //getLcoalTravel();
            ClearControls();
            #endregion
        }
        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            FromDateValidation_old();
            //FromDateValidation();
            return;
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


                    //hdnmsg.Value = lblmessage.Text;
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
            if (DivTrvl.Visible)
            {
                DivTrvl.Visible = false;
                btnTra_Details.Text = "+";
            }
            else
            {
                DivTrvl.Visible = true;
                btnTra_Details.Text = "-";
                hdntrdetailsid.Value = "";
                trvldeatils_btnSave.Text = "Submit";
            }
            AssigningSessions();
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

            if (hdnTripid.Value == "")
                hdnTripid.Value = "0";

            if (Convert.ToString(hdnTryiptypeid.Value).Trim() == "2")
                GetTravelMode();
            else
                GetTravelMode_old();

                txtRequirememt.Text = "";
                txtExpArrivalDate.Text = "";
                txtDestination.Text = "";
                txtToDate_Trvl.Text = "";
                txtExpDepartDate.Text = "";
                txtOrigin.Text = "";
                txtFromdate_Trvl.Text = "";
                txtDeviation.Text = "";
                txtTravelMode.Text = "";


            //if (Convert.ToString(hdntrdetailsid.Value).Trim() == "")
            //{
                trvldeatils_delete_btn.Visible = false;
            //}
            //else
            //{
            //    trvldeatils_delete_btn.Enabled = true;
            //}
            //   GetCities();
            txtTravelType.Enabled = false;


            //Response.Redirect("~/procs/TravelDetails.aspx");
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
            
            if (Convert.ToString(txtreqCur.Text).Trim() != "")
            {
                Session["Currency"] = txtreqCur.Text;
            }
            Session["Location"] = hdnDestnation.Value;
            if (Convert.ToString(txtreqCur.Text).Trim() != "")
            {
                Session["RequireCurrency"] = txtreqCur.Text;
            }

            if (DivAccm.Visible)
            {
                DivAccm.Visible = false;
                trvl_accmo_btn.Text = "+";
            }
            else
            {
                DivAccm.Visible = true;
                trvl_accmo_btn.Text = "-";
                //hdntrdetailsid.Value = "";
                hdnAccdtlsid.Value = "";
                accmo_btnSave.Text = "Submit";
            }

            AssigningSessions();

             txtTravelType.Text = Convert.ToString(Session["TravelType"]);
             hdnTripid.Value = Convert.ToString(Session["TripID"]);
             hdnTryiptypeid.Value = Convert.ToString(Session["TripTypeId"]);
             hdnfromdate.Value = Convert.ToString(Session["Fromdate"]);
             hdnTodate.Value = Convert.ToString(Session["Todate"]);
             txtLocation.Text = Convert.ToString(Session["Location"]).Trim();
             accmo_delete_btn.Visible = false;

             if (hdnTripid.Value == "")
                 hdnTripid.Value = "0";

            txtTripId.Text="";
            txtTravelType_Accm.Text="";
            txtFromdate_Accm.Text="";
            txtToDate_Accm.Text="";
            txtLocation.Text="";
            txtRequirement.Text = "";
            //Response.Redirect("~/procs/Accomodation.aspx");
        }
        protected void trvl_localbtn_Click(object sender, EventArgs e)
        {
            
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

            if (Div_Locl.Visible)
            {
                Div_Locl.Visible = false;
                trvl_localbtn.Text = "+";
            }
            else
            {
                Div_Locl.Visible = true;
                trvl_localbtn.Text = "-";
                //hdntrdetailsid.Value = "";
                hdnLocalId.Value = "";
                localtrvl_btnSave.Text = "Submit";
            }

            AssigningSessions();

            txtEmpCode.Text = Convert.ToString(Session["ReqEmpCode"]);
            txtTravelType_Locl.Text = Convert.ToString(Session["TravelType"]);
            hdnTripid.Value = Convert.ToString(Session["TripID"]);
            hdnTryiptypeid.Value = Convert.ToString(Session["TripTypeId"]);
            hdnfromdate.Value = Convert.ToString(Session["Fromdate"]);
            hdnTodate.Value = Convert.ToString(Session["Todate"]);
            txtLocation_Locl.Text = Convert.ToString(Session["DestLocation"]);
            localtrvl_delete_btn.Visible = false;
            ////if (Request.QueryString.Count > 2)
            ////{
            ////    hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();
            ////    hdnlocalId.Value = Convert.ToString(Request.QueryString[1]).Trim();

            ////    localtrvl_btnSave.Text = "Update";
            ////    localtrvl_delete_btn.Visible = true;
            ////    if (Convert.ToString(hdnlocalId.Value).Trim() == "0")
            ////    {
            ////        getLocaTravelDetails();
            ////    }
            ////    else
            ////    {
            ////        getLocalDetailsEdit();
            ////    }
            ////}

            txtTripId_Locl.Text = "";
            txtTravelType_Locl.Text = "";
            txtFromdate_Locl.Text = "";
            txtToDate_Locl.Text = "";
            txtLocation_Locl.Text = "";
            txtRequirement_Locl.Text = "";

            //Response.Redirect("~/procs/LocalTravel.aspx");
        }
        protected void lnkLocalTravleEdit_Click(object sender, EventArgs e)
        {
            lblmessage.Text = "";
            AssigningSessions();
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnTripid.Value = Convert.ToString(dgLocalTravel.DataKeys[row.RowIndex].Values[0]).Trim();
            hdnLocalId.Value = Convert.ToString(dgLocalTravel.DataKeys[row.RowIndex].Values[1]).Trim();

            txtEmpCode.Text = Convert.ToString(Session["ReqEmpCode"]);
            txtTravelType_Locl.Text = Convert.ToString(Session["TravelType"]);
            hdnTripid.Value = Convert.ToString(Session["TripID"]);
            hdnTryiptypeid.Value = Convert.ToString(Session["TripTypeId"]);
            hdnfromdate.Value = Convert.ToString(Session["Fromdate"]);
            hdnTodate.Value = Convert.ToString(Session["Todate"]);
            txtLocation_Locl.Text = Convert.ToString(Session["DestLocation"]);
            localtrvl_delete_btn.Visible = false;
            //if (Request.QueryString.Count > 2)
            //{
                //hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                //hdnlocalId.Value = Convert.ToString(Request.QueryString[1]).Trim();

                localtrvl_btnSave.Text = "Update";
                //localtrvl_delete_btn.Visible = true;
                if (Convert.ToString(hdnLocalId.Value).Trim() == "0")
                {
                    getLocaTravelDetails();
                }
                else
                {
                    getLocalDetailsEdit();
                }
            //}

            if (Convert.ToString(hdnAccdtlsid.Value).Trim() == "")
            {
                localtrvl_delete_btn.Enabled = false;
            }
            else
            {
                localtrvl_delete_btn.Enabled = true;
            }
            txtTravelType_Locl.Enabled = false;
            if (Div_Locl.Visible == false)
            {
                Div_Locl.Visible = true;
                localtrvl_btnSave.Text = "Update";
                trvl_localbtn.Text = "-";
            }
            //Response.Redirect("~/procs/LocalTravel.aspx?tripid=" + hdnTripid.Value + "&Localid=" + hdnLocalId.Value+ "&updatedid=0");
        }
        protected void lnkAccomodationdit_Click(object sender, EventArgs e)
        {
            lblmessage.Text = "";
                AssigningSessions();
                ImageButton btn = (ImageButton)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                hdnTripid.Value = Convert.ToString(dgAccomodation.DataKeys[row.RowIndex].Values[0]).Trim();
                hdnAccId.Value = Convert.ToString(dgAccomodation.DataKeys[row.RowIndex].Values[1]).Trim();

                txtEmpCode.Text = Convert.ToString(Session["ReqEmpCode"]);
                txtTravelType.Text = Convert.ToString(Session["TravelType"]);
                hdnTripid.Value = Convert.ToString(Session["TripID"]);
                hdnTryiptypeid.Value = Convert.ToString(Session["TripTypeId"]);
                hdnfromdate.Value = Convert.ToString(Session["Fromdate"]);
                hdnTodate.Value = Convert.ToString(Session["Todate"]);
                txtLocation.Text = Convert.ToString(Session["Location"]).Trim();
                accmo_delete_btn.Visible = false;
                //if (Request.QueryString.Count > 2)
                //{
                    //hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                hdnAccdtlsid.Value = Convert.ToString(hdnAccId.Value).Trim();
                    accmo_btnSave.Text = "Update";
                    //accmo_delete_btn.Visible = true;
                    if (Convert.ToString(hdnAccdtlsid.Value).Trim() == "0")
                    {
                        getAccomodationDetails_Edit();
                    }
                    else
                    {
                        getAccDetailsEdit();
                    }

                //}

                if (Convert.ToString(hdnAccdtlsid.Value).Trim() == "")
                {
                    accmo_delete_btn.Enabled = false;
                }
                else
                {
                    accmo_delete_btn.Enabled = true;
                }
                txtTravelType.Enabled = false;
                if (DivAccm.Visible == false)
                {
                    DivAccm.Visible = true;
                    accmo_btnSave.Text = "Update";
                    trvl_accmo_btn.Text = "-";
                }
        }
        protected void lnkTravelDetailsEdit_Click(object sender, EventArgs e)
        {
            lblmessage.Text = "";
                AssigningSessions();
                ImageButton btn = (ImageButton)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                hdnTripid.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[0]).Trim();
                hdnTravelDtlsId.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[1]).Trim();


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
            
            if (hdnTripid.Value == "")
                    hdnTripid.Value = "0";

                if (Convert.ToString(hdnTryiptypeid.Value).Trim() == "2")
                    GetTravelMode();
                else
                    GetTravelMode_old();

                //if (Request.QueryString.Count > 2)
                //{
                    //hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();

                    hdntrdetailsid.Value = Convert.ToString(hdnTravelDtlsId.Value).Trim();
                    trvldeatils_btnSave.Text = "Update";
                    trvldeatils_delete_btn.Visible = true;
                    if (Convert.ToString(hdntrdetailsid.Value).Trim() == "0")
                    {
                        getTravelDetails_Trvl();
                    }
                    else
                    {
                        getTravelDetailsEdit_Trvl();
                    }

                //}

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
                if (DivTrvl.Visible == false)
                {
                    DivTrvl.Visible = true;
                    btnTra_Details.Text = "-";
                }
                //Response.Redirect("~/procs/TravelDetails.aspx?tripid=" + hdnTripid.Value + "&trdetailsid=" + hdnTravelDtlsId.Value+"&updatedid=0");
        }
        protected void txtFromdate_TextChanged(object sender, EventArgs e)
        {
            //FromDateValidation();
            FromDateValidation_old();
        }
        protected void lnksubTrvlReqst_exp_Click(object sender, EventArgs e)
        {
            Session["TripTypeId"] = null;
            Session["TravelType"] = null;
            Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=0&mngexp=1");
        }
    #endregion

    #region PageMethods
        public void GetTravelContitionId()
              {
                  if (Convert.ToString(hdnTraveltypeid.Value).Trim() == "")
                      return;
                  if (Convert.ToString(hdnEligible.Value).Trim() == "")
                      return;
                  if (Convert.ToString(hdnTrdays.Value).Trim() == "")
                      return;

                    int TrConditionId = 0;
                    TrConditionId = spm.getTravelConditionTypeId(Convert.ToInt32(hdnTraveltypeid.Value), hdnEligible.Value, Convert.ToDouble(hdnTrdays.Value));
                    hdnTravelConditionid.Value = Convert.ToString(TrConditionId);
                  
                     getApproverdata();                     
                   
                    getIntermidateslist();
              }
        public void getIntermidateslist()
        {
            if (Convert.ToString(hdnTravelConditionid.Value) == "")
                return;
            if (Convert.ToString(hflGrade.Value) == "")
                return;


            DataTable dtIntermediate = new DataTable();
            dtIntermediate = spm.GetTravelIntermediateName(txtEmpCode.Text, Convert.ToInt32(hdnTravelConditionid.Value), hflGrade.Value);
            if (dtIntermediate.Rows.Count > 0)
            {
                lstIntermediate.DataSource = dtIntermediate;
                lstIntermediate.DataTextField = "Emp_Name";
                lstIntermediate.DataValueField = "A_EMP_CODE";
                
                //lstIntermediate.DataValueField = "APPR_ID";
                lstIntermediate.DataBind();
            }
        }
        public void getApproverdata()
        {
            if (Convert.ToString(hdnTravelConditionid.Value) == "")
                return;
            if (Convert.ToString(hflGrade.Value) == "")
                return;

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
                setEnablesCntrls(false);

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
        public void getTravelDetails()
        {
            DataTable dtTrDetails = new DataTable();
            dtTrDetails = spm.GetTravelDetails(txtEmpCode.Text);

            dgTravelRequest.DataSource = null;
            dgTravelRequest.DataBind();

            if (dtTrDetails.Rows.Count > 0)
            {
                //hdnTripid.Value = Convert.ToString(dtTrDetails.Rows[0]["trip_id"]);
               // txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["departure_date"]);
                // txtToDate.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_date"]);
                #region Comment 
                /*
                txtTriptype.Text = Convert.ToString(Session["TravelType"]);
                hdnTraveltypeid.Value = Convert.ToString(Session["TripTypeId"]);
                txtReason.Text = Convert.ToString(Session["Reason"]);
                txtFromdate.Text = Convert.ToString(Session["Fromdate"]);
                txtToDate.Text = Convert.ToString(Session["Todate"]);
                */
                #endregion

                //lstTripType.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["trip_mode_id"]);
                if (Convert.ToString(hdnTripid.Value).Trim() != "")
                {
                    hdnTraveltypeid.Value = Convert.ToString(dtTrDetails.Rows[0]["trip_type_id"]);
                }
                else
                {
                    hdnTraveltypeid.Value = lstTripType.SelectedValue;
                }
               
                hdnDesk.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_through_desk"]);
               
                hdnDestnation.Value = Convert.ToString(dtTrDetails.Rows[0]["arrival_place"]);
                hdnDeptPlace.Value = Convert.ToString(dtTrDetails.Rows[0]["departure_place"]);
                hdnTravelmode.Value = Convert.ToString(dtTrDetails.Rows[0]["trip_mode_id"]);
                hdnDeviation.Value = Convert.ToString(dtTrDetails.Rows[0]["Deviation"]);
                hdnTrDetRequirements.Value = Convert.ToString(dtTrDetails.Rows[0]["trip_requirements"]);
                
                


                //if (Convert.ToString(hdnTrdays.Value).Trim() != "")
                //{
                //    hdnTrdays.Value = Convert.ToString(Session["TrDays"]);
                //}

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

                //if (Convert.ToString(txtFromdate.Text).Trim() != "")
                //{
                //    FromDateValidation();
                //}

                dgTravelRequest.DataSource = dtTrDetails;
                dgTravelRequest.DataBind();
            }
        }
        public void getAccomodationDetails()
        {
            DataTable dtTrDetails = new DataTable();
            dtTrDetails = spm.GetAccomodationDetails(txtEmpCode.Text);

            dgAccomodation.DataSource = null;
            dgAccomodation.DataBind();

            if (dtTrDetails.Rows.Count > 0)
            {

                //txtAdvance.Text = Convert.ToString(Session["Advance"]);
                hdnAccReq.Value = Convert.ToString(dtTrDetails.Rows[0]["local_accomodation_remarks"]);
                hdnAccCOS.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_through_desk"]);
                hdnAcctripid.Value = Convert.ToString(dtTrDetails.Rows[0]["trip_id"]);
                txtreqCur.Text = Convert.ToString(Session["RequireCurrency"]);

                if (Convert.ToString(txtreqCur.Text).Trim() != "")
                {
                    txtreqCur.Text = Convert.ToString(Session["Currency"]); ;
                }
                dgAccomodation.DataSource = dtTrDetails;
                dgAccomodation.DataBind();
            }
        }
        public void getLcoalTravel()
        {
            DataTable dtTrDetails = new DataTable();
            dtTrDetails = spm.GetLocalTrvlDetails(txtEmpCode.Text);

            dgLocalTravel.DataSource = null;
            dgLocalTravel.DataBind();

            if (dtTrDetails.Rows.Count > 0)
            {

                hdnLcalTripid.Value = Convert.ToString(dtTrDetails.Rows[0]["trip_id"]);

                hdnlocaltrReq.Value = Convert.ToString(dtTrDetails.Rows[0]["remarks"]);
                hdnlocalTrCOS.Value = Convert.ToString(dtTrDetails.Rows[0]["is_thorugh_cos"]);

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
            Session["Reason"] = txtReason.Text;
            Session["TripTypeId"] = Convert.ToString(lstTripType.SelectedValue);
            Session["acttrvldays"] = Convert.ToString(hdnActualTrvlDays.Value);
            Session["TravelType"] = txtTriptype.Text;
            Session["Fromdate"] = txtFromdate.Text;
            Session["Todate"] = txtToDate.Text;
            Session["ReqEmpCode"] = txtEmpCode.Text;
            //Session["TravelTypeID"] = hdnTraveltypeid.Value;
            Session["Grade"] = hflGrade.Value;             
            Session["TripID"] = hdnTripid.Value;
            Session["TrDays"] = hdnTrdays.Value;
            Session["Advance"] =Convert.ToString(txtAdvance.Text).Trim();

            if (Convert.ToString(hdnTRApprverstatus.Value).Trim() == "Approved" && Convert.ToString(hdnLeavestatusId.Value).Trim() == "1")
            {
                Session["TRApprStatus"] = Convert.ToString(hdnTRApprverstatus.Value).Trim();
            }
            else
            {
                Session["TRApprStatus"] = "Peding";
            }
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
            getIntermidateslist();
        }
        private void FromDateValidation_old()
        {
            lblmessage.Text = "";
            string[] strdate;
            string strfromDate = "";
            string strToDate = "";
            string message = "";
            hdnTrdays.Value = "0";

           if (Convert.ToString(txtFromdate.Text).Trim() =="")
           {
               return;
           }
           if (Convert.ToString(txtToDate.Text).Trim() == "")
           {
               return;
           }
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
            hdnTraveltypeid.Value = lstTripType.SelectedValue;
            DataSet dttraveletails = new DataSet();
            if (Convert.ToString(txtFromdate.Text).Trim() != "" && Convert.ToString(txtToDate.Text).Trim() != "")
            {
                dttraveletails = spm.Get_TravelValidationResult(strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);
                
                if (dttraveletails.Tables[0].Rows.Count > 0)
                {
                    hdnTrdays.Value = Convert.ToString(dttraveletails.Tables[0].Rows[0]["TotalTravelDays"]);
                    hdnActualTrvlDays.Value = Convert.ToString(dttraveletails.Tables[0].Rows[0]["actualTravelDays"]);
                    GetTravelContitionId();
                   // getApproverdata();
                }

                if (dttraveletails.Tables[1].Rows.Count > 0)
                {
                    //message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);

                }

                if (Convert.ToString(message).Trim() != "")
                {
                    //lblmessage.Text = Convert.ToString(message).Trim();
                    //txtFromdate.Text = "";
                    //txtToDate.Text = "";

                }
            }
        }

        private Boolean FromDateValidation()
        {
            Boolean blnValid = false;
            lblmessage.Text = "";
            string[] strdate;
            string strfromDate = "";
            string strToDate = "";
            string message = "";
            hdnTrdays.Value = "0";

            if (Convert.ToString(txtFromdate.Text).Trim() == "")
            {
                blnValid = true;
            }
            if (Convert.ToString(txtToDate.Text).Trim() == "")
            {
                blnValid = true;
            }
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
            hdnTraveltypeid.Value = lstTripType.SelectedValue;
            DataSet dttraveletails = new DataSet();
            if (Convert.ToString(txtFromdate.Text).Trim() != "" && Convert.ToString(txtToDate.Text).Trim() != "")
            {
                dttraveletails = spm.Get_TravelValidationResult(strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);

                if (dttraveletails.Tables[0].Rows.Count > 0)
                {
                    hdnTrdays.Value = Convert.ToString(dttraveletails.Tables[0].Rows[0]["TotalTravelDays"]);
                    hdnActualTrvlDays.Value = Convert.ToString(dttraveletails.Tables[0].Rows[0]["actualTravelDays"]);
                    GetTravelContitionId();
                    // getApproverdata();
                }

                if (dttraveletails.Tables[1].Rows.Count > 0)
                {
                    message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);
                   
                }

                if (Convert.ToString(message).Trim() != "")
                {
                    lblmessage.Text = Convert.ToString(message).Trim();
                   // txtFromdate.Text = "";
                    //txtToDate.Text = "";
                    blnValid = true;
                }

                string[] tr_strdate;
                string tr_strfromDate = "";
                string tr_strToDate = "";

                String strdate_F;
                String strdate_T;

               

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


                #region Check Date Validations for all data grid view
                #region Check date validation for Travel Details

                for (Int32 irow = 0; irow < dgTravelRequest.Rows.Count; irow++)
                {
                    strdate_F = Convert.ToString(dgTravelRequest.Rows[irow].Cells[1].Text).Trim();
                    strdate_T = Convert.ToString(dgTravelRequest.Rows[irow].Cells[3].Text).Trim();

                    #region date formatting

                    if (Convert.ToString(strdate_F).Trim() != "")
                    {
                        strdate = Convert.ToString(strdate_F).Trim().Split('/');
                        strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                    }
                    if (Convert.ToString(strdate_T).Trim() != "")
                    {
                        strdate = Convert.ToString(strdate_T).Trim().Split('/');
                        strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                    }

                    #endregion

                    dttraveletails = spm.Get_Travel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);

                    if (dttraveletails.Tables[1].Rows.Count > 0)
                    {
                        message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);
                    }

                    if (Convert.ToString(message).Trim() != "")
                    {
                        lblmessage.Text = Convert.ToString(message).Trim();
                        blnValid = true;
                        dgTravelRequest.Rows[irow].BackColor = Color.Yellow;
                    }
                    else
                    {
                        dgTravelRequest.Rows[irow].BackColor = Color.White;
                    }
                     
                }
                #endregion

                #region Check date validation for Accomodation

                for (Int32 irow = 0; irow < dgAccomodation.Rows.Count; irow++)
                {
                    strdate_F = Convert.ToString(dgAccomodation.Rows[irow].Cells[0].Text).Trim();
                    strdate_T = Convert.ToString(dgAccomodation.Rows[irow].Cells[1].Text).Trim();

                    #region date formatting

                    if (Convert.ToString(strdate_F).Trim() != "")
                    {
                        strdate = Convert.ToString(strdate_F).Trim().Split('/');
                        strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                    }
                    if (Convert.ToString(strdate_T).Trim() != "")
                    {
                        strdate = Convert.ToString(strdate_T).Trim().Split('/');
                        strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                    }

                    #endregion

                    dttraveletails = spm.Get_Travel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);

                    if (dttraveletails.Tables[1].Rows.Count > 0)
                    {
                        message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);
                    }

                    if (Convert.ToString(message).Trim() != "")
                    {
                        lblmessage.Text = Convert.ToString(message).Trim();
                        blnValid = true;
                        dgAccomodation.Rows[irow].BackColor = Color.Yellow;
                    }
                    else
                    {
                        dgAccomodation.Rows[irow].BackColor = Color.White;
                    }

                }
                #endregion

                #region Check date validation for Local Travel

                for (Int32 irow = 0; irow < dgLocalTravel.Rows.Count; irow++)
                {
                    strdate_F = Convert.ToString(dgLocalTravel.Rows[irow].Cells[0].Text).Trim();
                    strdate_T = Convert.ToString(dgLocalTravel.Rows[irow].Cells[1].Text).Trim();

                    #region date formatting

                    if (Convert.ToString(strdate_F).Trim() != "")
                    {
                        strdate = Convert.ToString(strdate_F).Trim().Split('/');
                        strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                    }
                    if (Convert.ToString(strdate_T).Trim() != "")
                    {
                        strdate = Convert.ToString(strdate_T).Trim().Split('/');
                        strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                    }

                    #endregion

                    dttraveletails = spm.Get_Travel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);

                    if (dttraveletails.Tables[1].Rows.Count > 0)
                    {
                        message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);
                    }

                    if (Convert.ToString(message).Trim() != "")
                    {
                        lblmessage.Text = Convert.ToString(message).Trim();
                        blnValid = true;
                        dgLocalTravel.Rows[irow].BackColor = Color.Yellow;
                    }
                    else
                    {
                        dgLocalTravel.Rows[irow].BackColor = Color.White;
                    }

                }
                #endregion

                #endregion
            }

            return blnValid;
        }

        private void InsertMaintotemp()
             {
                 spm.InsertmaintoTemp(txtEmpCode.Text, Convert.ToInt32(hdnTripid.Value));
               }
        public void CheckTravelRequest_Submitted_Expese()
          {

              DataTable dtTrDetails = new DataTable();
              SqlParameter[] spars = new SqlParameter[2];

              spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
              spars[0].Value = "Sp_getTripid_fromExpenseRequestmain";

              spars[1] = new SqlParameter("@trip_id", SqlDbType.Decimal);
              spars[1].Value = hdnTripid.Value;

              dtTrDetails = spm.getDataList(spars, "SP_GETALL_Expense_DETAILS");

              if (dtTrDetails.Rows.Count > 0)
              {
                  lnksubTrvlReqst_exp.Visible = false;
                  btnCancel.Visible = false;
              }
          }
        private void setEnablesCntrls(Boolean blncheck)
        {
            trvl_btnSave.Enabled = blncheck;
            txtFromdate.Enabled = blncheck;
            txtToDate.Enabled = blncheck;
            txtTriptype.Enabled = blncheck;
            txtReason.Enabled = blncheck;
            txtAdvance.Enabled = blncheck;

        }

        protected string GetApprove_RejectList(Int32 maxtripid)
        {
            StringBuilder sbapp = new StringBuilder();
            sbapp.Length = 0;
            sbapp.Capacity = 0;
            DataTable dtAppRej = new DataTable();
            dtAppRej = spm.GetTravelApproverStatus(txtEmpCode.Text, Convert.ToInt32(maxtripid), Convert.ToInt32(hdnTravelConditionid.Value));
            if (dtAppRej.Rows.Count > 0)
            {
                sbapp.Append("<table>");
                for (int i = 0; i < dtAppRej.Rows.Count; i++)
                {
                    sbapp.Append("<tr>");
                    sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
                    sbapp.Append("</tr>");
                }
                sbapp.Append("</table>");
            }

            return Convert.ToString(sbapp);
        }

        protected string GetIntermediatesList()
        {
            StringBuilder sbapp = new StringBuilder();
            sbapp.Length = 0;
            sbapp.Capacity = 0;

            //dsapprover = spm.GetApproverStatus(txtempocde_lap.Text, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value));

            if (lstIntermediate.Items.Count > 0)
            {
                sbapp.Append("<table>");
                for (int i = 0; i < lstIntermediate.Items.Count; i++)
                {
                    sbapp.Append("<tr>");
                    //sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
                    sbapp.Append("<td>" + Convert.ToString(lstIntermediate.Items[i].Text).Trim() + "</td>");
                    sbapp.Append("</tr>");
                }
                sbapp.Append("</table>");
            }

            return Convert.ToString(sbapp);
        }
        private string get_approverlist_ifTD_COS(Int32 maxtripid)
        {
            string email_ids = "";
            DataTable dtApproverEmailIds = new DataTable();

            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "getPreviousAppr_Interm_mails";

            spars[1] = new SqlParameter("@trip_id", SqlDbType.Decimal);
            spars[1].Value = maxtripid;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text.ToString();

            spars[3] = new SqlParameter("@ApproverCode", SqlDbType.VarChar);
            spars[3].Value = hflapprcode.Value;

            dtApproverEmailIds = spm.getDataList(spars, "SP_GETALLTRAVEL_DETAILS");

            if (dtApproverEmailIds.Rows.Count > 0)
            {
                for (int irow = 0; irow < dtApproverEmailIds.Rows.Count; irow++)
                {
                    if (Convert.ToString(email_ids).Trim() == "")
                        email_ids = Convert.ToString(dtApproverEmailIds.Rows[irow]["Emp_Emailaddress"]).Trim();
                    else
                        email_ids = email_ids + ";" + Convert.ToString(dtApproverEmailIds.Rows[irow]["Emp_Emailaddress"]).Trim();
                }
            }

            return email_ids;

        }

    private void setSessionsVariables()
      {

          lstTripType.SelectedValue = Convert.ToString(Session["TripTypeId"]).Trim();
           txtReason.Text=Convert.ToString(Session["Reason"] ).Trim();
           txtTriptype.Text = Convert.ToString(Session["TravelType"]).Trim();
           txtFromdate.Text = Convert.ToString(Session["Fromdate"]).Trim();
           txtToDate.Text = Convert.ToString(Session["Todate"]).Trim();
           //txtEmpCode.Text = Convert.ToString(Session["ReqEmpCode"]).Trim();
           txtEmpCode.Text = Convert.ToString(Session["Empcode"]);
           hflGrade.Value = Convert.ToString(Session["Grade"]).Trim();
           hdnTripid.Value = Convert.ToString(Session["TripID"]).Trim();
           hdnTrdays.Value = Convert.ToString(Session["TrDays"]).Trim();
           txtAdvance.Text = Convert.ToString(Session["Advance"]);
           hdnActualTrvlDays.Value = Convert.ToString(Session["acttrvldays"]);
      }
    public void getApprover_mails_CODEID()
    {
        if (Convert.ToString(hdnTravelConditionid.Value) == "")
            return;
        if (Convert.ToString(hflGrade.Value) == "")
            return;

        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.GeTraveltApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value));
        
        lstApprover.Items.Clear();
        if (dtApproverEmailIds.Rows.Count > 0)
        {
            hdnApprEmailaddress.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["Emp_Emailaddress"]);
            hdnApprId.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["APPR_ID"]); 
            hflapprcode.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["A_EMP_CODE"]);
        }
        else
        {
            lblmessage.Text = "Since you are not assigned under any approver you can not apply for any type of Travel request, please contact HR";
            setEnablesCntrls(false);

        }
    }
    private void getFromdateTodate_FroEmail()
    {
        try
        {

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
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_FromdateTodate_for_Mail";

            spars[1] = new SqlParameter("@fromdate", SqlDbType.VarChar);
            spars[1].Value = strfromDate;

            spars[2] = new SqlParameter("@todate", SqlDbType.VarChar);
            spars[2].Value = strToDate;

            dsTrDetails = spm.getDatasetList(spars, "Usp_getDetails_leaves");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                hdnfrmdate_emial.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["fromdate"]).Trim();
                hdntodate_emial.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["todate"]).Trim();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    private string get_TDCOSACC_Approval_Status()
    {
        string email_ids = "";
        DataTable dtApproverEmailIds = new DataTable();

        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_getTDCOSACC_Approval_Status";

        spars[1] = new SqlParameter("@trip_id", SqlDbType.Decimal);
        spars[1].Value = hdnTripid.Value;

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = txtEmpCode.Text.ToString();

        dtApproverEmailIds = spm.getDataList(spars, "SP_GETALLTRAVEL_DETAILS");

        if (dtApproverEmailIds.Rows.Count > 0)
        {
            lstApprover_suprt.DataSource = dtApproverEmailIds;
            lstApprover_suprt.DataTextField = "names";
            lstApprover_suprt.DataValueField = "names";
            lstApprover_suprt.DataBind();
        }

        return email_ids;

    }
    #endregion

    #region ModifyTravelRequestEvents
    protected void btnMod_Click(object sender, EventArgs e)
    {
        
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";

        if (FromDateValidation() == true)
        {
            return;
        }

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
        if (Convert.ToString(hdnTraveltypeid.Value).Trim() == "2")
        {
            if (Convert.ToString(txtreqCur.Text).Trim() == "")
            {
                lblmessage.Text = "Please Enter Currency Required For Travel";
                return;
            }
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
            dttraveletails = spm.Get_TravelValidationResult_ForModAdd_temp(strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);
            if (dttraveletails.Tables[1].Rows.Count > 0)
            {
                message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);
            }

            if (Convert.ToString(message).Trim() != "")
            {
                lblmessage.Text = Convert.ToString(message).Trim();
                //txtFromdate.Text = "";
                //txtToDate.Text = "";
                // hdnmsg.Value = lblmessage.Text;
                return;
            }

            
            dttraveletails = spm.Get_TravelValidationResult(strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);

            if (dttraveletails.Tables[1].Rows.Count > 0)
            {
                message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);

            }

            if (Convert.ToString(message).Trim() != "")
            {
                lblmessage.Text = Convert.ToString(message).Trim();
                //txtFromdate.Text = "";
                //txtToDate.Text = "";


                // hdnmsg.Value = lblmessage.Text;
                return;
            }

        }

        #endregion

        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }

        //if (Convert.ToString(Session["chkbtnStatus"]).Trim() != "")
        //{
        //    Response.Redirect("~/procs/ManageTravelRequest.aspx");
        //}
        //Session["chkbtnStatus"] = "Travel Request button Event is Submitted";

        //getFomatated dates for Email
        getFromdateTodate_FroEmail();

        string reqcurrency = "";
        if (Convert.ToString(txtreqCur.Text).Trim() != "")
        {
            reqcurrency = txtreqCur.Text;
        }
        else
        {
            reqcurrency = "NA";
        }
        int status = 1;
        getApprover_mails_CODEID();
        spm.UpdateTravelRequest(Convert.ToInt32(hdnTripid.Value), strfromDate, strToDate, txtReason.Text, status, Convert.ToDouble(txtAdvance.Text), txtreqCur.Text,Convert.ToInt32(hdnTravelConditionid.Value));
        if (dgTravelRequest.Rows.Count > 0)
        {
            spm.InsertTravelDetails(0, 0, null, null, null, null, null, txtEmpCode.Text, Convert.ToInt32(hdnTripid.Value), null, null, "InsertMainTable","","");
        }
    
        if (dgAccomodation.Rows.Count > 0)
        {
            spm.InsertAccomodationDetails(0, null, null, null, txtEmpCode.Text, Convert.ToInt32(hdnTripid.Value), null, null, "InsertMainTable");
        }
        if (dgLocalTravel.Rows.Count > 0)
        {
            spm.InsertLocalTrvlDetails(0, null, null, null, txtEmpCode.Text, Convert.ToInt32(hdnTripid.Value), null, null, "InsertMainTable");
        }

        String strTrvlrestURL = "";
        strTrvlrestURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_TR"]).Trim() + "?reqid=" + Convert.ToDecimal(hdnTripid.Value);


        spm.InsertTravelApproverDetails(hflapprcode.Value, Convert.ToInt32(hdnApprId.Value), Convert.ToInt32(hdnTripid.Value),Convert.ToString(hdnIsApprover.Value));
        if (lstIntermediate.Items.Count > 0)
        {
            string strapprovermails = "";
            strapprovermails = get_approverlist_ifTD_COS(Convert.ToInt32(hdnTripid.Value));
            if (Convert.ToString(strapprovermails).Trim() != "")
            {
                spm.Travel_send_mailto_Intermediate(hflEmailAddress.Value, strapprovermails, "Request for Travel", txtTriptype.Text, hdnActualTrvlDays.Value, txtReason.Text, Convert.ToString(hdnfrmdate_emial.Value).Trim(), Convert.ToString(hdntodate_emial.Value).Trim(), GetApprove_RejectList(Convert.ToInt32(hdnTripid.Value)), txtEmpName.Text, GetIntermediatesList());
            }
        }

        lblmessage.Visible = true;
        spm.send_mailto_RM_TravelApprover(txtEmpName.Text, hflEmailAddress.Value, hdnApprEmailaddress.Value, "Request for Travel", txtTriptype.Text, hdnActualTrvlDays.Value, txtReason.Text, txtFromdate.Text, txtToDate.Text, "", GetApprove_RejectList(Convert.ToInt32(hdnTripid.Value)), GetIntermediatesList(), strTrvlrestURL);
        Response.Redirect("~/procs/ManageTravelRequest.aspx");
        lblmessage.Text = "Leave Reuqest Modified Successfully";

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        if (FromDateValidation() == true)
        {
            return;
        }
       

        //if (Convert.ToString(Session["chkbtnStatus"]).Trim() != "")
        //{
        //    Response.Redirect("~/procs/ManageTravelRequest.aspx");
        //}
        //Session["chkbtnStatus"] = "Travel Request button Event is Submitted";


        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            //lblmessage.Text = "Not Saved";
            return;
        }

        btnMod.Visible = false;
        btnCancel.Visible = false;
        hdnEligible.Value = "Cancellation";
        //GetTravelContitionId();
        //getApproverdata();
        //getFomatated dates for Email
        getFromdateTodate_FroEmail();
        string strapprovermails = "";
        //strapprovermails = Convert.ToString(GetApprove_RejectList(Convert.ToInt32(hdnTripid.Value))).Trim();
        strapprovermails = getCancellationmailList();
        
        spm.RrejectTravelrequest(Convert.ToInt32(hdnTripid.Value), txtEmpCode.Text, 0, "", "Canceltravelrequest");
        spm.Travel_send_mailto_Cancel_Intermediate(hflEmailAddress.Value, strapprovermails, " Cancellation of Travel", txtTriptype.Text, hdnActualTrvlDays.Value, txtReason.Text, Convert.ToString(hdnfrmdate_emial.Value).Trim(), Convert.ToString(hdntodate_emial.Value).Trim(), Convert.ToString(GetApprove_RejectList(Convert.ToInt32(hdnTripid.Value))).Trim(), txtEmpName.Text, GetIntermediatesList());
        lblmessage.Text = "Travel Cancelation Done and Notification has been send to your Reporting Manager";
        Response.Redirect("~/procs/ManageTravelRequest.aspx");
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
                    if (Convert.ToString(txtFromdate.Text).Trim()=="")
                    txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["FromDate"]);

                    if (Convert.ToString(txtToDate.Text).Trim() == "")
                    txtToDate.Text = Convert.ToString(dtTrDetails.Rows[0]["ToDate"]);

                    txtReason.Text = Convert.ToString(dtTrDetails.Rows[0]["trp_reason"]);
                    txtAdvance.Text = Convert.ToString(dtTrDetails.Rows[0]["req_adv_amt"]);
                    txtreqCur.Text = Convert.ToString(dtTrDetails.Rows[0]["currency_type"]);
                    Session["RequireCurrency"] = Convert.ToString(dtTrDetails.Rows[0]["currency_type"]);
                    Session["Currency"] = Convert.ToString(dtTrDetails.Rows[0]["currency_type"]); 
                    hdnTravelConditionid.Value = Convert.ToString(dtTrDetails.Rows[0]["tr_Conditionid"]);
                    hdnLeavestatusId.Value = Convert.ToString(dtTrDetails.Rows[0]["trip_status"]);
                    hdnIsApprover.Value = Convert.ToString(dtTrDetails.Rows[0]["IsApprover_TD_COS"]);
                    hdnTRApprverstatus.Value = Convert.ToString(dtTrDetails.Rows[0]["TRapproverStatus"]);
                    hdnTravelConditionid.Value = Convert.ToString(dtTrDetails.Rows[0]["tr_Conditionid"]);
                    hdnTraveltypeid.Value = Convert.ToString(dtTrDetails.Rows[0]["trip_typeid"]);
                    hdnLeavestatusValue.Value = Convert.ToString(dtTrDetails.Rows[0]["approverStatus"]);
                    Session["TripTypeId"] = hdnTraveltypeid.Value;
                    lstTripType.SelectedValue = hdnTraveltypeid.Value;
                    hdnActualTrvlDays.Value = Convert.ToString(dtTrDetails.Rows[0]["actualTravelDays"]);
                    hdnTrdays.Value = Convert.ToString(dtTrDetails.Rows[0]["TotalTravelDays"]);
                    hdnTDCOSStatus.Value = Convert.ToString(dtTrDetails.Rows[0]["TDCOSStatus"]);

                    if (Convert.ToString(dtTrDetails.Rows[0]["trip_status"]) == "2" && Convert.ToString(dtTrDetails.Rows[0]["TDCOSStatus"]) == "Approved")
                    {
                        if (Convert.ToInt32(dtTrDetails.Rows[0]["chkvalid"]) < 0)
                        {
                            lnksubTrvlReqst_exp.Visible = true;
                        }
                    }
                    lnksubTrvlReqst_exp.Visible = true;

                    if (Convert.ToString(dtTrDetails.Rows[0]["trip_status"]) != "1")
                    {                         
                        btnTra_Details.Visible = false;
                        trvl_accmo_btn.Visible = false;
                        trvl_localbtn.Visible = false;
                        txtTriptype.Enabled = false;
                        txtFromdate.Enabled = false;
                        txtToDate.Enabled = false;
                        txtReason.Enabled = false;
                        txtAdvance.Enabled = false;
                        txtreqCur.Enabled = false;
                    }

                }
            }
          
            protected string getCancellationmailList()
            {
                string email_ids = "";
                DataTable dtApproverEmailIds = new DataTable();
                dtApproverEmailIds = spm.TravelPreviousApproverDetails(txtEmpCode.Text, Convert.ToInt32(hdnTripid.Value));
                lstApprover.Items.Clear();
                if (dtApproverEmailIds.Rows.Count > 0)
                {
                    for (int irow = 0; irow < dtApproverEmailIds.Rows.Count; irow++)
                    {
                        if (Convert.ToString(email_ids).Trim() == "")
                            email_ids = Convert.ToString(dtApproverEmailIds.Rows[irow]["Emp_Emailaddress"]).Trim();
                        else
                            email_ids = email_ids + ";" + Convert.ToString(dtApproverEmailIds.Rows[irow]["Emp_Emailaddress"]).Trim();
                    }
                }

                return email_ids;

            }
        

    #endregion

    #region Datagrid RowCreated and DataRowBound Events
    protected void dgTravelRequest_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //Convert.ToString(hdnLeavestatusId.Value).Trim() == "2"
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            if (Convert.ToString(hdnLeavestatusId.Value).Trim() == "2" || Convert.ToString(hdnLeavestatusId.Value).Trim() == "3" || Convert.ToString(hdnLeavestatusId.Value).Trim() == "4" || Convert.ToString(hdnTRApprverstatus.Value).Trim() == "Approved")
            {
                e.Row.Cells[8].Visible = false;
            }
            if (hdnLeavestatusValue.Value == "Approved" && Convert.ToString(hdnLeavestatusId.Value).Trim() == "1")
            {
                e.Row.Cells[8].Visible = false;
            }
            if (Convert.ToString(hdnLeavestatusId.Value).Trim() == "2" && Convert.ToString(hdnTDCOSStatus.Value).Trim() == "Pending")
            {
                e.Row.Cells[8].Visible = true;
             }
            //if (Convert.ToString(hdnLeavestatusId.Value).Trim() != "2" )
            //{
            //    e.Row.Cells[7].Visible = false;
            //}
        }

        
    }
    protected void dgAccomodation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //Convert.ToString(hdnLeavestatusId.Value).Trim() == "2"
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            if (Convert.ToString(hdnLeavestatusId.Value).Trim() == "2" || Convert.ToString(hdnLeavestatusId.Value).Trim() == "3" || Convert.ToString(hdnLeavestatusId.Value).Trim() == "4" || Convert.ToString(hdnTRApprverstatus.Value).Trim() == "Approved")
            {
                e.Row.Cells[5].Visible = false;
            }
            if (hdnLeavestatusValue.Value == "Approved" && Convert.ToString(hdnLeavestatusId.Value).Trim() == "1")
            {
                e.Row.Cells[5].Visible = false;
            }
            if (Convert.ToString(hdnLeavestatusId.Value).Trim() == "2" && Convert.ToString(hdnTDCOSStatus.Value).Trim() == "Pending")
            {
                e.Row.Cells[5].Visible = true;
            }

            //if (Convert.ToString(hdnLeavestatusId.Value).Trim() != "2")
            //{
            //    e.Row.Cells[4].Visible = false;
            //}
        }
    }
    protected void dgLocalTravel_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //Convert.ToString(hdnLeavestatusId.Value).Trim() == "2"
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            if (Convert.ToString(hdnLeavestatusId.Value).Trim() == "2" || Convert.ToString(hdnLeavestatusId.Value).Trim() == "3" || Convert.ToString(hdnLeavestatusId.Value).Trim() == "4" || Convert.ToString(hdnTRApprverstatus.Value).Trim() == "Approved")
            {
                e.Row.Cells[5].Visible = false;
            }
            if (hdnLeavestatusValue.Value == "Approved" && Convert.ToString(hdnLeavestatusId.Value).Trim() == "1")
            {
                e.Row.Cells[5].Visible = false;
            }

            if (Convert.ToString(hdnLeavestatusId.Value).Trim() == "2" && Convert.ToString(hdnTDCOSStatus.Value).Trim() == "Pending")
            {
                e.Row.Cells[5].Visible = true;
            }
             
        }

    }

    protected void dgTravelRequest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton lnkview = new ImageButton();
            lnkview = (ImageButton)e.Row.FindControl("lnkTravelDetailsEdit");
            if (Convert.ToString(e.Row.Cells[7].Text).Trim() == "Booked")
            {   
                lnkview.Visible = false;
            }

            if (Convert.ToString(hdnLeavestatusId.Value).Trim() == "2" || Convert.ToString(hdnTDCOSStatus.Value).Trim() == "Pending")
            {
                lnkview.Visible = true;
            }
        }
    }
    protected void dgAccomodation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton lnkview = new ImageButton();
            lnkview = (ImageButton)e.Row.FindControl("lnkAccomodationdit");
            if (Convert.ToString(e.Row.Cells[4].Text).Trim() == "Booked")
            {             
                lnkview.Visible = false;
            }

            if (Convert.ToString(hdnLeavestatusId.Value).Trim() == "2" || Convert.ToString(hdnTDCOSStatus.Value).Trim() == "Pending")
            {
                lnkview.Visible = true;
            }

        }
    }
    protected void dgLocalTravel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton lnkview = new ImageButton();
            lnkview = (ImageButton)e.Row.FindControl("lnkLocalTravleEdit");

            if (Convert.ToString(e.Row.Cells[4].Text).Trim() == "Booked")
            {               
                lnkview.Visible = false;
            }

            if (Convert.ToString(hdnLeavestatusId.Value).Trim() == "2" || Convert.ToString(hdnTDCOSStatus.Value).Trim() == "Pending")
            {
                lnkview.Visible = true;
            }

        }
    }
    #endregion

    protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
    {
        #region Check For Blank Fields
        lblmessage.Text = "";
        if (Convert.ToString(txtTravelType.Text).Trim() == "")
        {
            lblmessage.Text = "Please select Travel Type";
            return;
        }
        if (Convert.ToString(txtFromdate_Trvl.Text).Trim() == "")
        {
            lblmessage.Text = "Please select From Date";
            return;
        }
        if (Convert.ToString(txtToDate_Trvl.Text).Trim() == "")
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

        if (Check_Cities_name(txtOrigin.Text) == false)
        {
            lblmessage.Text = "Please Select Correct Departure Place For Travel";
            return;
        }
        if (Check_Cities_name(txtDestination.Text) == false)
        {
            lblmessage.Text = "Please Select Correct Arrival Place For Travel";
            return;
        }
        if (Convert.ToString(txtOrigin.Text).Trim() == Convert.ToString(txtDestination.Text).Trim())
        {
            lblmessage.Text = "Departure Place & Arrival Place  should not be same.";
            return;
        }


        #endregion
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";
        #region date formatting
        if (Convert.ToString(txtFromdate_Trvl.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate_Trvl.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(txtToDate_Trvl.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtToDate_Trvl.Text).Trim().Split('/');
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
            getTravelDetails();
            //if (Convert.ToString(hdnTripid.Value).Trim() != "0" && Convert.ToString(hdnTripid.Value).Trim() != "")
            //    Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
            //else
            //    Response.Redirect("~/procs/TravelRequest.aspx");
            #endregion
        }
        else
        {
            #region UpdateTravelDetails
            txtDeviation.Text = hdnDeviation.Value;
            spm.UpdateTravelDetails(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdntrdetailsid.Value), txtTravelMode.Text, strfromDate, Convert.ToString(txtOrigin.Text).Trim(), strToDate, Convert.ToString(txtDestination.Text).Trim(), txtDeviation.Text, hdnCOS.Value, txtRequirememt.Text, "UpdateMainTable", Convert.ToString(txtEmpCode.Text), txtExpArrivalDate.Text, txtExpDepartDate.Text);
            getTravelDetails();
            //if (Convert.ToString(hdnTripid.Value).Trim() != "0" && Convert.ToString(hdnTripid.Value).Trim() != "")
            //    Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
            //else
            //    Response.Redirect("~/procs/TravelRequest.aspx");
            #endregion
        }

        txtRequirememt.Text = "";
        txtExpArrivalDate.Text = "";
        txtDestination.Text = "";
        txtToDate_Trvl.Text = "";
        txtExpDepartDate.Text = "";
        txtOrigin.Text = "";
        txtFromdate_Trvl.Text = "";
        txtDeviation.Text = "";
        txtTravelMode.Text = "";
        hdntrdetailsid.Value = "";
        DivTrvl.Visible = false;
        btnTra_Details.Text = "+";
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

    protected void trvldeatils_cancel_btn_Click(object sender, EventArgs e)
    {
        txtRequirememt.Text = "";
        txtExpArrivalDate.Text = "";
        txtDestination.Text = "";
        txtToDate_Trvl.Text = "";
        txtExpDepartDate.Text = "";
        txtOrigin.Text = "";
        txtFromdate_Trvl.Text = "";
        txtDeviation.Text = "";
        txtTravelMode.Text = "";
        hdntrdetailsid.Value = "";
        DivTrvl.Visible = false;
        btnTra_Details.Text = "+";
        // Server.Transfer("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
        //if (Convert.ToString(hdnTripid.Value).Trim() != "")
        //    Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
        //else
        //    Response.Redirect("~/procs/TravelRequest.aspx");

    }

    protected void lstDestination_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDestination.Text = lstDestination.SelectedItem.Text;
        PopupControlExtender4.Commit(lstDestination.SelectedItem.Text);
    }
    protected void txtToDate_Trvl_TextChanged(object sender, EventArgs e)
    {
        txtDeviation.Text = hdnDeviation.Value;
        if (Convert.ToString(txtFromdate_Trvl.Text).Trim() == "")
        {
            lblmessage.Text = "From date  cannot be blank";
            return;
        }


        string[] tr_strdate;
        string tr_strfromDate = "";
        string tr_strToDate = "";

        #region TraelRequestdate formatting

        if (Convert.ToString(txtFromdate_Trvl.Text).Trim() != "")
        {
            tr_strdate = Convert.ToString(hdnfromdate.Value).Trim().Split('/');
            tr_strfromDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
        }
        if (Convert.ToString(txtToDate_Trvl.Text).Trim() != "")
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

        if (Convert.ToString(txtFromdate_Trvl.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate_Trvl.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(txtToDate_Trvl.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtToDate_Trvl.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        #endregion
        DataSet dttraveletails = new DataSet();
        if (Convert.ToString(txtFromdate_Trvl.Text).Trim() != "" && Convert.ToString(txtToDate_Trvl.Text).Trim() != "")
        {

            dttraveletails = spm.Get_Travel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);

            if (dttraveletails.Tables[1].Rows.Count > 0)
            {
                message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);

            }

            if (Convert.ToString(message).Trim() != "")
            {
                lblmessage.Text = Convert.ToString(message).Trim();
                txtFromdate_Trvl.Text = "";
                txtToDate_Trvl.Text = "";
            }
            else
            {
                lblmessage.Text = "";
            }
        }
    }

    protected void lstOrigin_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtOrigin.Text = lstOrigin.SelectedItem.Text;
        PopupControlExtender3.Commit(lstOrigin.SelectedItem.Text);
    }
    protected void txtFromdate_Trvl_TextChanged(object sender, EventArgs e)
    {
        txtDeviation.Text = hdnDeviation.Value;
        string[] tr_strdate;
        string tr_strfromDate = "";
        string tr_strToDate = "";

        #region TraelRequestdate formatting

        if (Convert.ToString(txtFromdate_Trvl.Text).Trim() != "")
        {
            tr_strdate = Convert.ToString(hdnfromdate.Value).Trim().Split('/');
            tr_strfromDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
        }
        if (Convert.ToString(txtToDate_Trvl.Text).Trim() != "")
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

        if (Convert.ToString(txtFromdate_Trvl.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate_Trvl.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(txtToDate_Trvl.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtToDate_Trvl.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        #endregion
        DataSet dttraveletails = new DataSet();
        if (Convert.ToString(txtFromdate_Trvl.Text).Trim() != "" && Convert.ToString(txtToDate_Trvl.Text).Trim() != "")
        {
            dttraveletails = spm.Get_Travel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);

            if (dttraveletails.Tables[1].Rows.Count > 0)
            {
                message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);

            }

            if (Convert.ToString(message).Trim() != "")
            {
                lblmessage.Text = Convert.ToString(message).Trim();
                txtFromdate_Trvl.Text = "";
                txtToDate_Trvl.Text = "";
            }
            else
            {
                lblmessage.Text = "";
            }
        }
    }

    protected void lstTravelMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTravelMode.Text = lstTravelMode.SelectedItem.Text;
        PopupControlExtender1.Commit(lstTravelMode.SelectedItem.Text);

        SetTravelDeviation();

        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetDeviation('" + hdnDeviation.Value + "');", true);

        // txtDeviation.Text = Convert.ToString(hdnDeviation.Value);
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

    public void getTravelDetails_Trvl()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetTravelDetails(txtEmpCode.Text);

        if (dtTrDetails.Rows.Count > 0)
        {
            txtTravelMode.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_mode"]);
            txtDeviation.Text = Convert.ToString(dtTrDetails.Rows[0]["deviation"]);
            txtRequirememt.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_requirements"]);
            txtFromdate_Trvl.Text = Convert.ToString(dtTrDetails.Rows[0]["departure_date"]);
            txtToDate_Trvl.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_date"]);
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

    public void getTravelDetailsEdit_Trvl()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetTempTravelDetailsEdit(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdntrdetailsid.Value), Convert.ToString(txtEmpCode.Text));

        if (dtTrDetails.Rows.Count > 0)
        {
            txtTravelMode.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_mode"]);
            txtDeviation.Text = Convert.ToString(dtTrDetails.Rows[0]["deviation"]);
            txtRequirememt.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_requirements"]);
            txtFromdate_Trvl.Text = Convert.ToString(dtTrDetails.Rows[0]["departure_date"]);
            txtToDate_Trvl.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_date"]);
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
    protected void btn_del_Trvl_Click(object sender, ImageClickEventArgs e)
    {
        #region Delete TravelDetails
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnTripid.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnTravelDtlsId.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[1]).Trim();
        spm.DeleteTravelDetails(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnTravelDtlsId.Value), Convert.ToString(txtEmpCode.Text));
        //lblmessage.Text = "Travel Detail Deleted Successfully";
        getTravelDetails();
        //if (Convert.ToString(hdnTripid.Value).Trim() != "")
        //    Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
        //else
        //    Response.Redirect("~/procs/TravelRequest.aspx");
        #endregion
    }

    #region Accomodation
    protected void txtFromdate_Accm_TextChanged(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        string[] tr_strdate;
        string tr_strfromDate = "";
        string tr_strToDate = "";

        #region TraelRequestdate formatting

        if (Convert.ToString(txtFromdate_Accm.Text).Trim() != "")
        {
            tr_strdate = Convert.ToString(hdnfromdate.Value).Trim().Split('/');
            tr_strfromDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
        }
        if (Convert.ToString(txtToDate_Accm.Text).Trim() != "")
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

        if (Convert.ToString(txtFromdate_Accm.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate_Accm.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(txtToDate_Accm.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtToDate_Accm.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        #endregion
        DataSet dttraveletails = new DataSet();
        if (Convert.ToString(txtFromdate_Accm.Text).Trim() != "" && Convert.ToString(txtToDate_Accm.Text).Trim() != "")
        {
            dttraveletails = spm.Get_Travel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);

            if (dttraveletails.Tables[1].Rows.Count > 0)
            {
                message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);

            }

            if (Convert.ToString(message).Trim() != "")
            {
                lblmessage.Text = Convert.ToString(message).Trim();
                txtFromdate_Accm.Text = "";
                txtToDate_Accm.Text = "";


                // hdnmsg.Value = lblmessage.Text;
                return;
            }
        }
    }

    protected void txtToDate_Accm_TextChanged(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        if (Convert.ToString(txtFromdate_Accm.Text).Trim() == "")
        {
            lblmessage.Text = "From date  cannot be blank";
            return;
        }


        string[] tr_strdate;
        string tr_strfromDate = "";
        string tr_strToDate = "";

        #region TraelRequestdate formatting

        if (Convert.ToString(txtFromdate_Accm.Text).Trim() != "")
        {
            tr_strdate = Convert.ToString(hdnfromdate.Value).Trim().Split('/');
            tr_strfromDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
        }
        if (Convert.ToString(txtToDate_Accm.Text).Trim() != "")
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

        if (Convert.ToString(txtFromdate_Accm.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate_Accm.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(txtToDate_Accm.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtToDate_Accm.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        #endregion
        DataSet dttraveletails = new DataSet();
        if (Convert.ToString(txtFromdate_Accm.Text).Trim() != "" && Convert.ToString(txtToDate_Accm.Text).Trim() != "")
        {
            dttraveletails = spm.Get_Travel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);

            if (dttraveletails.Tables[1].Rows.Count > 0)
            {
                message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);

            }

            if (Convert.ToString(message).Trim() != "")
            {
                lblmessage.Text = Convert.ToString(message).Trim();
                txtFromdate_Accm.Text = "";
                txtToDate_Accm.Text = "";


                // hdnmsg.Value = lblmessage.Text;
                return;
            }
        }
    }

    protected void accmo_delete_btn_Click(object sender, EventArgs e)
    {
        #region Check For Blank Fields
        lblmessage.Text = "";
        if (Convert.ToString(txtTravelType.Text).Trim() == "")
        {
            lblmessage.Text = "Please select Travel Type";
            return;
        }
        if (Convert.ToString(txtFromdate_Accm.Text).Trim() == "")
        {
            lblmessage.Text = "Please select From Date";
            return;
        }
        if (Convert.ToString(txtToDate_Accm.Text).Trim() == "")
        {
            lblmessage.Text = "Please select To Date";
            return;
        }
        if (Convert.ToString(txtLocation.Text).Trim() == "")
        {
            lblmessage.Text = "Please Select Departure Place For Travel";
            return;
        }

        #endregion

        //if (Convert.ToString(Session["chkTrvlAccLocalTrvlbtnStatus"]).Trim() != "")
        //{
        //    if (Convert.ToString(hdnTripid.Value).Trim() != "" && Convert.ToString(hdnTripid.Value).Trim() != "0")
        //        Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
        //    else
        //        Response.Redirect("~/procs/TravelRequest.aspx");
        //}
        //Session["chkTrvlAccLocalTrvlbtnStatus"] = "Accomodations button Event is Submitted";

        #region Delete TravelDetails
        spm.DeleteAccomodationlDetails(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnAccdtlsid.Value), Convert.ToString(txtEmpCode.Text));
        lblmessage.Text = "Accommodation Details Deleted Successfully";
        if (Convert.ToString(hdnTripid.Value).Trim() != "" && Convert.ToString(hdnTripid.Value).Trim() != "0")
            Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
        else
            Response.Redirect("~/procs/TravelRequest.aspx");
        #endregion
    }
    protected void accmo_cancel_btn_Click(object sender, EventArgs e)
    {
        txtTripId.Text = "";
        txtTravelType_Accm.Text = "";
        txtFromdate_Accm.Text = "";
        txtToDate_Accm.Text = "";
        txtLocation.Text = "";
        txtRequirement.Text = "";
        trvl_accmo_btn.Text = "+";
        accmo_btnSave.Text = "Submit";
        DivAccm.Visible = false;
        // Server.Transfer("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
        //if (Convert.ToString(hdnTripid.Value).Trim() != "" && Convert.ToString(hdnTripid.Value).Trim() != "0")
        //    Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
        //else
        //    Response.Redirect("~/procs/TravelRequest.aspx");
    }

    protected void accmo_btnSave_Click(object sender, EventArgs e)
    {
        #region Check All Fields Blank
        //  lblmessage.Text = "";
        lblmessage.Visible = true;
        if (Convert.ToString(txtFromdate_Accm.Text).Trim() == "")
        {
            lblmessage.Text = "Please select From Date";
            return;
        }
        if (Convert.ToString(txtToDate_Accm.Text).Trim() == "")
        {
            lblmessage.Text = "Please select To Date";
            return;
        }
        if (Convert.ToString(txtLocation.Text).Trim() == "")
        {
            lblmessage.Text = "Please Enter Location For Travel";
            return;
        }

        if (Check_Cities_name(txtLocation.Text) == false)
        {
            lblmessage.Text = "Please Select Correct Location.";
            return;
        }

        #endregion

        //if (Convert.ToString(Session["chkTrvlAccLocalTrvlbtnStatus"]).Trim() != "")
        //{
        //    if (Convert.ToString(hdnTripid.Value).Trim() != "")
        //        Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
        //    else
        //        Response.Redirect("~/procs/TravelRequest.aspx");

        //}
        //Session["chkTrvlAccLocalTrvlbtnStatus"] = "Accomodations button Event is Submitted";


        string[] strdate;
        string strfromDate = "";
        string strToDate = "";
        #region date formatting
        if (Convert.ToString(txtFromdate_Accm.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate_Accm.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(txtToDate_Accm.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtToDate_Accm.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        #endregion

        if (chkCos_Accm.Checked == true)
        {
            hdnCOS_Accm.Value = "Yes";
        }
        else
        {
            hdnCOS_Accm.Value = "No";
        }

        if (accmo_btnSave.Text == "Submit")
        {
            spm.InsertAccomodationDetails(Convert.ToInt32(hdnTryiptypeid.Value), strfromDate, strToDate, Convert.ToString(txtLocation.Text).Trim(), txtEmpCode.Text, 0, hdnCOS_Accm.Value, txtRequirement.Text, "InsertTempTable");

            //if (Convert.ToString(hdnTripid.Value).Trim() != "")
            //    Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
            //else
            //    Response.Redirect("~/procs/TravelRequest.aspx");
            getAccomodationDetails();
        }
        else
        {

            spm.UpdateAccomodationDetails(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnAccdtlsid.Value), strfromDate, strToDate, Convert.ToString(txtLocation.Text).Trim(), hdnCOS_Accm.Value, txtRequirement.Text, "UpdateMainTable", Convert.ToString(txtEmpCode.Text));
            //if (Convert.ToString(hdnTripid.Value).Trim() != "")
            //    Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
            //else
            //    Response.Redirect("~/procs/TravelRequest.aspx");
            getAccomodationDetails();
        }

        txtTripId.Text = "";
        txtTravelType_Accm.Text = "";
        txtFromdate_Accm.Text = "";
        txtToDate_Accm.Text = "";
        txtLocation.Text = "";
        txtRequirement.Text = "";
        trvl_accmo_btn.Text = "+";
        accmo_btnSave.Text = "Submit";
        DivAccm.Visible = false;
    }

    protected void lstLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtLocation.Text = lstLocation.SelectedItem.Text;
        PopupControlExtender5.Commit(lstLocation.SelectedItem.Text);
    }

    public void getAccomodationDetails_Edit()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetAccomodationDetails(txtEmpCode.Text);

        if (dtTrDetails.Rows.Count > 0)
        {
            txtFromdate_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["From Date"]);
            txtToDate_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["To Date"]);
            txtRequirement.Text = Convert.ToString(dtTrDetails.Rows[0]["local_accomodation_remarks"]);
            txtLocation.Text = Convert.ToString(dtTrDetails.Rows[0]["Location"]);
            hdnCOS_Accm.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_through_desk"]);

            if (Convert.ToString(hdnCOS_Accm.Value).Trim() != "Yes")
            {
                chkCos_Accm.Checked = false;
            }
            else
            {
                chkCos_Accm.Checked = true;
            }

        }
    }

    public void getAccDetailsEdit()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetAccomodationDetailsEdit(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnAccdtlsid.Value));
        if (dtTrDetails.Rows.Count > 0)
        {
            txtFromdate_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["From Date"]);
            txtToDate_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["To Date"]);
            txtRequirement.Text = Convert.ToString(dtTrDetails.Rows[0]["local_accomodation_remarks"]);
            txtLocation.Text = Convert.ToString(dtTrDetails.Rows[0]["Location"]);
            hdnCOS_Accm.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_through_desk"]);

            if (Convert.ToString(hdnCOS_Accm.Value).Trim() != "Yes")
            {
                chkCos_Accm.Checked = false;
            }
            else
            {
                chkCos_Accm.Checked = true;
            }

        }
    }
    protected void btn_del_Accm_Click(object sender, ImageClickEventArgs e)
    {
        spm.DeleteAccomodationlDetails(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnAccdtlsid.Value), Convert.ToString(txtEmpCode.Text));
        lblmessage.Text = "Accommodation Details Deleted Successfully";
        getAccomodationDetails();
    }
    #endregion Accomodation


    #region Local Travel
    protected void txtFromdate_Locl_TextChanged(object sender, EventArgs e)
    {
        string[] tr_strdate;
        string tr_strfromDate = "";
        string tr_strToDate = "";
        lblmessage.Text = "";
        #region TraelRequestdate formatting

        if (Convert.ToString(txtFromdate_Locl.Text).Trim() != "")
        {
            tr_strdate = Convert.ToString(hdnfromdate.Value).Trim().Split('/');
            tr_strfromDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
        }
        if (Convert.ToString(txtToDate_Locl.Text).Trim() != "")
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
            dttraveletails = spm.Get_Travel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);

            if (dttraveletails.Tables[1].Rows.Count > 0)
            {
                message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);

            }

            if (Convert.ToString(message).Trim() != "")
            {
                lblmessage.Text = Convert.ToString(message).Trim();
                txtFromdate_Locl.Text = "";
                txtToDate_Locl.Text = "";
                return;
            }
        }
    }
    protected void txtToDate_Locl_TextChanged(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        if (Convert.ToString(txtFromdate_Locl.Text).Trim() == "")
        {
            lblmessage.Text = "From date  cannot be blank";
            return;
        }


        string[] tr_strdate;
        string tr_strfromDate = "";
        string tr_strToDate = "";

        #region TraelRequestdate formatting

        if (Convert.ToString(txtFromdate_Locl.Text).Trim() != "")
        {
            tr_strdate = Convert.ToString(hdnfromdate.Value).Trim().Split('/');
            tr_strfromDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
        }
        if (Convert.ToString(txtToDate_Locl.Text).Trim() != "")
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
            dttraveletails = spm.Get_Travel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);

            if (dttraveletails.Tables[1].Rows.Count > 0)
            {
                message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);
            }

            if (Convert.ToString(message).Trim() != "")
            {
                lblmessage.Text = Convert.ToString(message).Trim();
                txtFromdate_Locl.Text = "";
                txtToDate_Locl.Text = "";
                return;
            }
        }
    }
    protected void lstLocation_Locl_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtLocation_Locl.Text = lstLocation_Locl.SelectedItem.Text;
        PopupControlExtender6.Commit(lstLocation_Locl.SelectedItem.Text);
    }


    protected void localtrvl_btnSave_Click(object sender, EventArgs e)
    {

        #region Check All Fields Blank
        lblmessage.Text = "";

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

        //if (Check_Cities_name(txtLocation.Text) == false)
        //{
        //    lblmessages.Text = "Please Select Correct Location.";
        //    return;
        //}

        #endregion

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
        if (localtevl_chkCOS.Checked == true)
        {
            hdnCOS_Locl.Value = "Yes";
        }
        else
        {
            hdnCOS_Locl.Value = "No";
        }
        if (Convert.ToString(localtrvl_btnSave.Text).Trim() == "Submit")
        {
            spm.InsertLocalTrvlDetails(Convert.ToInt32(hdnTryiptypeid.Value), strfromDate, strToDate, Convert.ToString(txtLocation_Locl.Text).Trim(), txtEmpCode.Text, 0, hdnCOS_Locl.Value, txtRequirement_Locl.Text, "Inserttemptable");
            getLcoalTravel();
            ////if (Convert.ToString(hdnTripid.Value).Trim() != "" && Convert.ToString(hdnTripid.Value).Trim() != "0")
            ////    Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
            ////else
            ////    Response.Redirect("~/procs/TravelRequest.aspx");
        }
        else
        {
            spm.UpdateLocalTrvlDetails(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnLocalId.Value), strfromDate, strToDate, Convert.ToString(txtLocation_Locl.Text).Trim(), hdnCOS_Locl.Value, txtRequirement_Locl.Text, "UpdateMainTable", Convert.ToString(txtEmpCode.Text));
            getLcoalTravel();
            ////if (Convert.ToString(hdnTripid.Value).Trim() != "" && Convert.ToString(hdnTripid.Value).Trim() != "0")
            ////    Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
            ////else
            ////    Response.Redirect("~/procs/TravelRequest.aspx");
        }

        txtTripId_Locl.Text = "";
        txtTravelType_Locl.Text = "";
        txtFromdate_Locl.Text = "";
        txtToDate_Locl.Text = "";
        txtLocation_Locl.Text = "";
        txtRequirement_Locl.Text = "";

        trvl_localbtn.Text = "+";
        localtrvl_btnSave.Text = "Submit";
        Div_Locl.Visible = false;

    }

    protected void localtrvl_cancel_btn_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        txtTripId_Locl.Text = "";
        txtTravelType_Locl.Text = "";
        txtFromdate_Locl.Text = "";
        txtToDate_Locl.Text = "";
        txtLocation_Locl.Text = "";
        txtRequirement_Locl.Text = "";

        trvl_localbtn.Text = "+";
        localtrvl_btnSave.Text = "Submit";
        Div_Locl.Visible = false;
        //if (Convert.ToString(hdnTripid.Value).Trim() != "" && Convert.ToString(hdnTripid.Value).Trim() != "0")
        //    Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
        //else
        //    Response.Redirect("~/procs/TravelRequest.aspx");
    }
    protected void localtrvl_delete_btn_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        ////if (Convert.ToString(Session["chkTrvlAccLocalTrvlbtnStatus"]).Trim() != "")
        ////{
        ////    if (Convert.ToString(hdnTripid.Value).Trim() != "" && Convert.ToString(hdnTripid.Value).Trim() != "0")
        ////        Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
        ////    else
        ////        Response.Redirect("~/procs/TravelRequest.aspx");
        ////}

        #region Delete TravelDetails
        spm.DeleteLocalDetails(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnLocalId.Value), Convert.ToString(txtEmpCode.Text));
        lblmessage.Text = "Local Details Deleted Successfully";
        getLcoalTravel();
        ////if (Convert.ToString(hdnTripid.Value).Trim() != "" && Convert.ToString(hdnTripid.Value).Trim() != "0")
        ////    Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
        ////else
        ////    Response.Redirect("~/procs/TravelRequest.aspx");
        #endregion
    }
    public void getLocaTravelDetails()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetLocalTrvlDetails(txtEmpCode.Text);

        if (dtTrDetails.Rows.Count > 0)
        {
            txtFromdate_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["From Date"]);
            txtToDate_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["To Date"]);
            txtRequirement_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["remarks"]);
            txtLocation_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["Location"]);
            hdnCOS_Locl.Value = Convert.ToString(dtTrDetails.Rows[0]["is_thorugh_cos"]);

            if (Convert.ToString(hdnCOS_Locl.Value).Trim() != "Yes")
            {
                localtevl_chkCOS.Checked = false;
            }
            else
            {
                localtevl_chkCOS.Checked = true;
            }

        }
    }
    public void getLocalDetailsEdit()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetLocalDetailsEdit(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnLocalId.Value));
        if (dtTrDetails.Rows.Count > 0)
        {
            txtFromdate_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["From Date"]);
            txtToDate_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["To Date"]);
            txtRequirement_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["remarks"]);
            txtLocation_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["Location"]);
            hdnCOS_Locl.Value = Convert.ToString(dtTrDetails.Rows[0]["is_thorugh_cos"]);

            if (Convert.ToString(hdnCOS_Locl.Value).Trim() != "Yes")
            {
                localtevl_chkCOS.Checked = false;
            }
            else
            {
                localtevl_chkCOS.Checked = true;
            }

        }
    }
    #endregion Local Travel
    protected void btn_del_Locl_Click(object sender, ImageClickEventArgs e)
    {
        lblmessage.Text = "";
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnTripid.Value = Convert.ToString(dgLocalTravel.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnLocalId.Value = Convert.ToString(dgLocalTravel.DataKeys[row.RowIndex].Values[1]).Trim();
        spm.DeleteLocalDetails(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnLocalId.Value), Convert.ToString(txtEmpCode.Text));
        lblmessage.Text = "Local Travel Detail Deleted Successfully";
        getLcoalTravel();

    }
}

