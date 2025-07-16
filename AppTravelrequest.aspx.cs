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

public partial class AppTravelrequest : System.Web.UI.Page
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
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

            lblmessage.Text = "";
            Session["chkTrvlTDCOSbtnStatus"] = "";
            //lblmsg.Visible = false; ;
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
                    hndloginempcode.Value = Convert.ToString(Session["Empcode"]);
                    txtAdv_Approved_Amt.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    //btnMod.Visible = false;
                    //btnCancel.Visible = false;
                    //btnback_mng.Visible = false;
                    //hdnTripid.Value = "";
                    this.lstApprover.SelectedIndex = 0;

                    //txtreqCur.Enabled = false;
                    GetEmployeeDetails();
                    GetTripDetails();
                    
                    if (Request.QueryString.Count > 0)
                    {
                        trvl_btnSave.Visible = false;
                        //  btnBack.Visible = false;

                        //btnMod.Visible = true;
                        //btnCancel.Visible = true;
                        //btnback_mng.Visible = true;
                        hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        if (Request.QueryString.Count >1)
                        {
                            hdnInboxType.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        }
                        checkTO_COS_ACC();
                        hdnisBookthrugh_COS.Value = "No";
                        hdnisBookthrugh_TD.Value = "No";
                          
                        getMainTravelDetails();
                        getMainAccomodationDetails();
                        getMainLcoalTravel();
                        getTravelRequestData();
                        GetCuurentApprID();
                        getApproverlist();
                        getIntermidateslist();
                        getnextAppIntermediate();
                       // AssigningSessions();
                        if (Convert.ToString(hdnInboxType.Value).Trim() == "TD")
                        {
                           // btnApprove.Text = "Confirmation";
                            btnApprove_TDCOS.Visible = true;
                            btnApprove.Visible= false;

                            btnApprove.Visible = false;
                            spnadvreq.Visible = false;
                            txtAdvance.Visible = false;
                            spnaccomd.Visible = false;
                            trvl_accmo_btn.Visible = false;
                            dgAccomodation.Visible = false;

                            spnlcolTrvl.Visible = false;
                            trvl_localbtn.Visible = false;
                            dgLocalTravel.Visible = false;
                            btnTra_Details.Visible = true;
                        }
                        if (Convert.ToString(hdnInboxType.Value).Trim() == "COS")
                        {
                            // btnApprove.Text = "Confirmation";
                            btnApprove_TDCOS.Visible = true;
                            btnApprove.Visible = false;
                            spnadvreq.Visible = true;
                            txtAdvance.Visible = true;
                            spntrvldtls.Visible = false;
                            btnTra_Details.Visible = false;
                            dgTravelRequest.Visible = false;
                        }
                        if (Convert.ToString(hdnInboxType.Value).Trim() == "ACC")
                        {
                            // btnApprove.Text = "Confirmation";
                            btnApprove_TDCOS.Visible = true;
                            btnApprove.Visible = false;
                            spnadvreq.Visible = true;
                            txtAdvance.Visible = true;
                            spnaccomd.Visible = true;
                            trvl_accmo_btn.Visible = true;
                            dgAccomodation.Visible = true;
                            btnCorrection.Visible = false;
                            trvl_accmo_btn.Visible = false;

                        }
                    }

                    if (Convert.ToString(hdnInboxType.Value).Trim() != "APP")
                    {
                        spnscrrctnRmkrs.Visible = false;
                        txtRemarks.Visible = false;
                        idspnTemCalendar.Visible = false;
                        sprtfunctions.Visible = true;
                        lstApprover_suprt.Visible = true;
                        get_TDCOSACC_Approval_Status();
                    }
                    if (Convert.ToString(hdnInboxType.Value).Trim() == "ACC")
                    {
                        spnadvAmtbyAcc.Visible = true;
                        txtAdv_Approved_Amt.Visible = true;
                    }
                    editform.Visible = true;
                    divbtn.Visible = false;
                    //divmsg.Visible = false;

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
    
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        //if (Convert.ToString(hdnbtnStatus.Value).Trim() != "")
        //{
        //    return;
        //}
        //hdnbtnStatus.Value = "Approved button Event is Submitted";

        #region  if Account
        if (Convert.ToString(hdnInboxType.Value).Trim() == "ACC")
        {
            if (Convert.ToString(txtAdvance.Text).Trim() != "")
            {
                if (Convert.ToDecimal(txtAdvance.Text) > 0)
                {
                    if (Convert.ToString(txtAdv_Approved_Amt.Text).Trim() == "")
                    {
                        lblmessage.Text = "Please enter Amount.";
                        return;
                    }

                    if (Convert.ToString(txtAdv_Approved_Amt.Text).Trim() != "")
                    {
                        string[] strdate;
                        strdate = Convert.ToString(txtAdv_Approved_Amt.Text).Trim().Split('.');
                        if (strdate.Length > 2)
                        {
                            txtAdv_Approved_Amt.Text = "0";
                            lblmessage.Text = "Please enter correct Amount.";
                            return;
                        }
                    }
                }
            }
        }
        #endregion
        #region Check Status of Booked or not for Travel Details , Accomodation,Local Travel

        if (Convert.ToString(hdnInboxType.Value).Trim() != "APP")
        {
            if (Convert.ToString(txtComments.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter comments.";
                return;
            }
        }
        if (Convert.ToString(hdnInboxType.Value).Trim() == "TD")
        {            
            if (Convert.ToString(hdnisBookthrugh_TD.Value).Trim() == "Yes")
            {
               
                for (Int32 irow=0;irow<dgTravelRequest.Rows.Count;irow++)
                {
                    Label lblstatus = (Label)dgTravelRequest.Rows[irow].FindControl("lbltrvlbookstatus");
                    //CheckBox ChkTD = (CheckBox)dgTravelRequest.Rows[irow].FindControl("ChkCOS");
                    Label lblchkcos = (Label)dgTravelRequest.Rows[irow].FindControl("lblchkcos");

                    if (lblchkcos.Text == "Yes")
                     {                     
                        if (Convert.ToString(lblstatus.Text).Trim() == "")
                        {
                            lblmessage.Text = "Travel Status not set for this Travels";
                            return;
                        }
                    }
                    
                }
            }
        }
        if (Convert.ToString(hdnInboxType.Value).Trim() == "COS")
        {
            if (Convert.ToString(hdnisBookthrugh_COS.Value).Trim() == "Yes")
            {
                for (Int32 irow = 0; irow < dgAccomodation.Rows.Count; irow++)
                {
                    Label lblstatus = (Label)dgAccomodation.Rows[irow].FindControl("lbltrvlAccbookstatus");
                    //CheckBox ChkCOS = (CheckBox)dgLocalTravel.Rows[irow].FindControl("ChkCOS");
                    Label lblchkcos = (Label)dgAccomodation.Rows[irow].FindControl("lblchkcos");

                    if (lblchkcos.Text == "Yes")
                     {
                         if (Convert.ToString(lblstatus.Text).Trim() == "")
                         {
                             lblmessage.Text = "Accommodation Status not set for this Travels";
                             return;
                         }
                     }
                }
            }
        }
        if (Convert.ToString(hdnInboxType.Value).Trim() == "COS")
        {
            if (Convert.ToString(hdnisBookthrugh_COS.Value).Trim() == "Yes")
            {
                for (Int32 irow = 0; irow < dgLocalTravel.Rows.Count; irow++)
                {
                    Label lblstatus = (Label)dgLocalTravel.Rows[irow].FindControl("lblLocaltrvlbookstatus");
                    //CheckBox ChkCOS = (CheckBox)dgLocalTravel.Rows[irow].FindControl("ChkCOS");
                    Label lblchkcos = (Label)dgLocalTravel.Rows[irow].FindControl("lblchkcos");

                    if (lblchkcos.Text == "Yes")
                    {
                         if (Convert.ToString(lblstatus.Text).Trim() == "")
                         {
                             lblmessage.Text = "Local Travel Status not set for this Travels";
                             return;
                         }
                     }
                }
            }
        }
        #endregion

       // btnApprove.Attributes.Add("onclick", "this.disabled=true;");
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "setDisableApprovedButton();", true);
        //if (Convert.ToString(Session["chkbtnStatus"]).Trim() != "")
        //{
        //    Response.Redirect("~/procs/InboxTravelRequest.aspx?stype=" + hdnInboxType.Value);            
        //}
        //Session["chkbtnStatus"] = "Approved button Event is Submitted";


        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            //lblmessage.Text = "Not Saved";
            return;
        }
        

        string strapprovermails = "";
        strapprovermails = getRejectionCorrectionmailList();

        //getFomatated dates for Email

        getFromdateTodate_FroEmail();
        String strTrvlrestURL = "";
        String strapprovername = "";
        strTrvlrestURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_TR"]).Trim() + "?reqid=" + hdnTripid.Value;

        Decimal dadvamt = 0;
        if (Convert.ToString(txtAdvance.Text).Trim() != "")
        {
            dadvamt = Convert.ToDecimal(txtAdvance.Text);
        }
        decimal strAccApproved = 0;
        //if Final Approver               
        if (Convert.ToString((hdnstaus.Value).Trim()) == "Final Approver")
        {
            //get TD/COS/ACC Emp COde & APP ID
            getTravle_Desk_COS_ApproverCode();

            if (Convert.ToString(hdnApproverTDCOS_status.Value).Trim() == "")
            {
                #region if International Travel Request
                if (Convert.ToString(ConfigurationManager.AppSettings["TrvlStatusid_I"]).Trim() == Convert.ToString(hdnTraveltypeid.Value).Trim())
                {
                    if (Convert.ToString(hdnisBookthrugh_COS.Value).Trim() == "No")
                    {
                        Decimal dadvamt_I = 0;
                        if (Convert.ToString(txtAdvance.Text).Trim() != "")
                        {
                            dadvamt_I = Convert.ToDecimal(txtAdvance.Text);
                        }

                        if (dadvamt_I != 0)
                        {
                            hdnisBookthrugh_COS.Value = "Yes";
                        }
                    }
                }
                #endregion

                if (Convert.ToString(hdnisBookthrugh_TD.Value).Trim() == "Yes")
                    spm.InsertTravelApproverDetails(hdnApprovalTD_Code.Value, Convert.ToInt32(hdnApprovalTD_ID.Value), Convert.ToInt32(hdnTripid.Value), "");

                if (Convert.ToString(hdnisBookthrugh_COS.Value).Trim() == "Yes")
                    spm.InsertTravelApproverDetails(hdnApprovalCOS_Code.Value, Convert.ToInt32(hdnApprovalCOS_ID.Value), Convert.ToInt32(hdnTripid.Value), "");


                //if Adavance Amount is required for ACC
                if (dadvamt != 0)
                {
                    spm.InsertTravelApproverDetails(hdnApprovalACC_Code.Value, Convert.ToInt32(hdnApprovalACC_ID.Value), Convert.ToInt32(hdnTripid.Value), "");
                }
            }

            if (Convert.ToString(hdnInboxType.Value).Trim() == "ACC")
            {
                strAccApproved = Convert.ToDecimal(txtAdv_Approved_Amt.Text);
            }
            spm.UpdateTravelAppRequest(Convert.ToInt32(hdnTripid.Value), "Approved", txtComments.Text, Convert.ToString(("").Trim()), Convert.ToInt32(hdnCurrentApprID.Value), strAccApproved);

        }
        else
        {
            spm.UpdateTravelAppRequest(Convert.ToInt32(hdnTripid.Value), "Approved", txtComments.Text, Convert.ToString((hdnstaus.Value).Trim()), Convert.ToInt32(hdnCurrentApprID.Value), strAccApproved);
        }
        getApproverlist();
        //strapprovermails = get_approverlist_ifTD_COS();
        String strmailsTDCOS = "";
        #region set TD,COS & ACC email id for sending mail
            if (Convert.ToString(hdnisBookthrugh_TD.Value).Trim() == "Yes")
            {
                strmailsTDCOS = Convert.ToString(hdnApprovalTD_mail.Value).Trim();
            }
            if (Convert.ToString(hdnisBookthrugh_COS.Value).Trim() == "Yes")
            {
                strmailsTDCOS = strmailsTDCOS + ";" + Convert.ToString(hdnApprovalCOS_mail.Value).Trim();
            }
            if (dadvamt != 0)
            {
                strmailsTDCOS = strmailsTDCOS + ";" + Convert.ToString(hdnApprovalACC_mail.Value).Trim();
            }
        #endregion

        if (Convert.ToString((hdnstaus.Value).Trim()) == "")
        {
            
            spm.InsertTravelApproverDetails(hdnNextApprCode.Value, Convert.ToInt32(hdnNextApprId.Value), Convert.ToInt32(hdnTripid.Value),"");           
            getApproverlist();
            //strapprovermails = get_approverlist_ifTD_COS();   
            strapprovermails = get_approverlist_Rejection_Correction();
            spm.Travel_send_mailto_Next_Approver(hdnReqEmailaddress.Value, hdnNextApprEmail.Value, "Request for Travel", txtTriptype.Text, hdnTrdays.Value, txtReason.Text, Convert.ToString(hdnfrmdate_emial.Value).Trim(), Convert.ToString(hdntodate_emial.Value).Trim(), GetApprove_RejectList(), txtEmpName.Text, GetIntermediatesList_formail(), strTrvlrestURL);
            spm.Travel_send_mailto_Intermediate(hdnReqEmailaddress.Value, strapprovermails, "Request for Travel", txtTriptype.Text, hdnTrdays.Value, txtReason.Text, Convert.ToString(hdnfrmdate_emial.Value).Trim(), Convert.ToString(hdntodate_emial.Value).Trim(), GetApprove_RejectList(), txtEmpName.Text, GetIntermediatesList_formail());

            lblmessage.Text = "Travel Resquest has been approved and and send for next level approvals";          
        }
        else
        {
            //get TD/COS/ACC Emp COde & APP ID
            if (hdnInboxType.Value == "APP")
            {
                               
                //Added 
                if (Convert.ToString(strmailsTDCOS).Trim() != "")
                {
                    //strapprovermails = get_approverlist_ifTD_COS_finalApproval();
                    strapprovermails = get_approverlist_Rejection_Correction();
                  
                    spm.Travel_send_mailto_Next_Approver(hdnReqEmailaddress.Value, strmailsTDCOS, "Request for Travel", txtTriptype.Text, hdnTrdays.Value, txtReason.Text, Convert.ToString(hdnfrmdate_emial.Value).Trim(), Convert.ToString(hdntodate_emial.Value).Trim(), GetApprove_RejectList(), txtEmpName.Text, GetIntermediatesList_formail(), strTrvlrestURL);
                    spm.Travel_send_mailto_Intermediate(hdnReqEmailaddress.Value, strapprovermails, "Request for Travel", txtTriptype.Text, hdnTrdays.Value, txtReason.Text, Convert.ToString(hdnfrmdate_emial.Value).Trim(), Convert.ToString(hdntodate_emial.Value).Trim(), GetApprove_RejectList(), txtEmpName.Text, GetIntermediatesList_formail());
                    lblmessage.Text = "Travel Resquest has been approved and send for next level approvals";
                }
                
            }
            
            if (hdnInboxType.Value != "ACC")
            {
                //if Travel Request with TD,COS or Without TD or COS after all approval Add This Reqst for ACC
                #region not required
                /* Decimal dadvamt = 0;
                if (Convert.ToString(txtAdvance.Text).Trim() != "")
                {
                    dadvamt = Convert.ToDecimal(txtAdvance.Text);
                } 

                if (dadvamt != 0)
                {
                    getTravle_Desk_COS_ApproverCode();
                    if (get_Travel_Approval_Details_forACC() == true)
                    {                            
                        spm.InsertTravelApproverDetails(hdnApprovalACC_Code.Value, Convert.ToInt32(hdnApprovalACC_ID.Value), Convert.ToInt32(hdnTripid.Value), "");
                        spm.Travel_send_mailto_Next_Approver(hdnReqEmailaddress.Value, hdnApprovalACC_mail.Value, "Request for Travel", txtTriptype.Text, hdnTrdays.Value, txtReason.Text, Convert.ToString(hdnfrmdate_emial.Value).Trim(), Convert.ToString(hdntodate_emial.Value).Trim(), GetApprove_RejectList(), txtEmpName.Text, GetIntermediatesList_formail(), strTrvlrestURL);
                        lblmessage.Text = "Travel Resquest has been approved and send for next level approvals";                          
                    }

                    if (hdnInboxType.Value != "APP")
                    {
                        getApproverlist();
                        strapprovermails = get_approverlist_ifTD_COS_finalApproval();
                        strapprovermails = strapprovermails + ";" + hdnApprovalTD_mail.Value + ";" + hdnApprovalCOS_mail.Value;                            
                            //spm.Travel_send_mailto_Intermediate(hdnReqEmailaddress.Value, strapprovermails, "Request for Travel", txtTriptype.Text, hdnTrdays.Value, txtReason.Text, Convert.ToString(hdnfrmdate_emial.Value).Trim(), Convert.ToString(hdntodate_emial.Value).Trim(), GetApprove_RejectList(), txtEmpName.Text, GetIntermediatesList_formail());
                    }
                }
                */
                #endregion
            }
        }

        // To Check Final approver is aproved

        DataTable dtappstatus = new DataTable();
        dtappstatus = spm.GetAllTravelStatus(Convert.ToInt32(hdnTripid.Value));
        string strTrvlDtls_Status= "";
        strTrvlDtls_Status = get_TravelDetails_List();

        string strAccomdtnDtls_Status = "";
        strAccomdtnDtls_Status = get_AccomodationTravelDetails_List();

        string strLocalTrvlDtls_Status = "";
        strLocalTrvlDtls_Status = get_LocalTravelDetails_List();

        if (dtappstatus.Rows.Count > 0)
        {
            string action = Convert.ToString(dtappstatus.Rows[0]["Action"]);
            Boolean flag = false;
            for (int i = 0; i < dtappstatus.Rows.Count; i++)
            {
                if (Convert.ToString(dtappstatus.Rows[i]["Action"]).Trim() == "Approved")
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                    break;
                }
            }
            if (flag == true)
            {
                getApproverlist();
                strapprovermails = get_approverlist_ifTD_COS_finalApproval();
                // mail to requester + all previous approvers if approved by all
                strapprovermails = strapprovermails + ";" + strmailsTDCOS;
                spm.UpdateTravelAppRequest(Convert.ToInt32(hdnTripid.Value), "Approved", txtComments.Text, Convert.ToString((hdnstaus.Value).Trim()), Convert.ToInt32(hdnCurrentApprID.Value), strAccApproved);
                strapprovername = "";

                if (Convert.ToString(hdnInboxType.Value).Trim() == "APP")
                {
                    strapprovername = Convert.ToString(hflEmpName.Value);
                }
                else
                {
                    if (hdnInboxType.Value == "TD")
                        strapprovername = Convert.ToString(hdnApprovalTD_Name.Value);
                    else if (hdnInboxType.Value == "COS")
                        strapprovername = Convert.ToString(hdnApprovalCOS_Name.Value);
                    else
                        strapprovername = Convert.ToString(hdnApprovalACC_Name.Value);                           
                }

                string strAccountAmt = "";
                string strcurrncy = "INR";

                if (Convert.ToString(hdnInboxType.Value).Trim() == "ACC")
                {
                    if (Convert.ToString(ConfigurationManager.AppSettings["TrvlStatusid_I"]).Trim() == Convert.ToString(hdnTraveltypeid.Value).Trim())
                    {
                        strcurrncy = Convert.ToString(txtreqCur.Text).Trim();
                    }
                    if (Convert.ToString(txtAdvance.Text).Trim() != "")
                    {
                        strAccountAmt =  strcurrncy + "   " + Convert.ToString(txtAdv_Approved_Amt.Text).Trim() + "  Paid. ";
                    }
                }
                if (Convert.ToString(hdnInboxType.Value).Trim() != "APP")
                {

                         //spm.Travel_send_mailto_Next_Approver_Intermediate(hdnReqEmailaddress.Value, strapprovermails, "Request for Travel", txtTriptype.Text, hdnTrdays.Value, txtReason.Text, Convert.ToString(hdnfrmdate_emial.Value).Trim(), Convert.ToString(hdntodate_emial.Value).Trim(), GetApprove_RejectList(), txtEmpName.Text, strapprovername, GetIntermediatesList_formail(),"","","","");
                    spm.Travel_send_mailto_Next_Approver_Intermediate(hdnReqEmailaddress.Value, strapprovermails, "Request for Travel", txtTriptype.Text, hdnTrdays.Value, txtReason.Text, Convert.ToString(hdnfrmdate_emial.Value).Trim(), Convert.ToString(hdntodate_emial.Value).Trim(), GetApprove_RejectList(), txtEmpName.Text, strapprovername, GetIntermediatesList_formail(), strTrvlDtls_Status, strAccomdtnDtls_Status, strLocalTrvlDtls_Status, strAccountAmt);
                }
                lblmessage.Text = "Travel Resquest has been approved and notofication has been send to the Requester and Previous Intermediate,Approver Levels";
            }
            else
            {
                if (hdnInboxType.Value == "TD" || hdnInboxType.Value == "COS")
                {
                   // Decimal dadvamt = 0;
                    if (Convert.ToString(txtAdvance.Text).Trim() != "")
                    {
                        dadvamt = Convert.ToDecimal(txtAdvance.Text);
                    }

                    if (dadvamt == 0)
                    {
                        getApproverlist();
                        strapprovermails = get_approverlist_ifTD_COS_finalApproval();
                        if (hdnInboxType.Value == "TD")
                            strapprovermails = strapprovermails + ";" + hdnApprovalCOS_mail.Value;
                        else
                            strapprovermails = strapprovermails + ";" + hdnApprovalTD_mail.Value;

                        if (Convert.ToString(hdnInboxType.Value).Trim() != "APP")
                        {

                            //spm.Travel_send_mailto_Intermediate(hdnReqEmailaddress.Value, strapprovermails, "Request for Travel", txtTriptype.Text, hdnTrdays.Value, txtReason.Text, Convert.ToString(hdnfrmdate_emial.Value).Trim(), Convert.ToString(hdntodate_emial.Value).Trim(), GetApprove_RejectList(), txtEmpName.Text, GetIntermediatesList_formail());
                            spm.Travel_send_mailto_Intermediate(hdnReqEmailaddress.Value, strapprovermails, "Request for Travel", txtTriptype.Text, hdnTrdays.Value, txtReason.Text, Convert.ToString(hdnfrmdate_emial.Value).Trim(), Convert.ToString(hdntodate_emial.Value).Trim(), GetApprove_RejectList(), txtEmpName.Text, GetIntermediatesList_formail());
                        }

                    }
                }
            }
            Response.Redirect("~/procs/InboxTravelRequest.aspx?stype="+hdnInboxType.Value);
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        if (Convert.ToString((txtRemarks.Text).Trim()) == "")
        {
            //lblmessage.Text = "Please mention the Rejection remarks for Reject travel request.";
            lblmessage.Text = "Please mention the Rejection remarks before rejecting the request";
            return;
        }

        //if (Convert.ToString(Session["chkbtnStatus"]).Trim() != "")
        //{
        //    Response.Redirect("~/procs/InboxTravelRequest.aspx?stype=" + hdnInboxType.Value);
        //}
        //Session["chkbtnStatus"] = "Reject button Event is Submitted";

        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            //lblmessage.Text = "Not Saved";
            return;
        }

        //getFomatated dates for Email
        getFromdateTodate_FroEmail();

        string strapprovermails = "";
      //  strapprovermails = getCancellationmailList();

       // strapprovermails = get_approverlist_ifTD_COS();
        strapprovermails = get_approverlist_Rejection_Correction();

        if (Convert.ToString(hdnInboxType.Value).Trim() == "TD" || Convert.ToString(hdnInboxType.Value).Trim() == "COS")
            getTravle_Desk_COS_ApproverCode();

        if (Convert.ToString(hdnInboxType.Value).Trim() == "TD")
            strapprovermails = strapprovermails + ";" + hdnApprovalCOS_mail.Value;
        else if (Convert.ToString(hdnInboxType.Value).Trim() == "COS")
            strapprovermails = strapprovermails + ";" + hdnApprovalTD_mail.Value;
        
        spm.RrejectTravelrequest(Convert.ToInt32(hdnTripid.Value), hndloginempcode.Value, Convert.ToInt32(hdnCurrentApprID.Value), Convert.ToString(txtRemarks.Text).Trim(), "rejecttravelrequest");

       // spm.Travel_send_mail_Rejection_Correction(hdnReqEmailaddress.Value, hflEmpName.Value, "Rejection of Travel Request", txtTriptype.Text, hdnTrdays.Value, txtReason.Text, txtFromdate.Text, txtToDate.Text, strapprovermails, Convert.ToString(GetApprove_RejectList()).Trim(), txtEmpName.Text);
        spm.Travel_send_mail_Rejection_Correction(hdnReqEmailaddress.Value, hflEmpName.Value, "Rejection of Travel Request", txtTriptype.Text, hdnTrdays.Value, txtReason.Text, Convert.ToString(hdnfrmdate_emial.Value).Trim(), Convert.ToString(hdntodate_emial.Value).Trim(), strapprovermails, Convert.ToString(GetApprove_RejectList()).Trim(), txtEmpName.Text, GetIntermediatesList_formail());
        Response.Redirect("~/procs/InboxTravelRequest.aspx?stype=" + hdnInboxType.Value);
    }
    protected void btnCorrection_Click(object sender, EventArgs e)
    {
        try
        {

            #region Check Status of Booked or not for Travel Details , Accomodation,Local Travel

            if (Convert.ToString(hdnInboxType.Value).Trim() == "TD")
            {
                if (Convert.ToString(hdnisBookthrugh_TD.Value).Trim() == "Yes")
                {

                    for (Int32 irow = 0; irow < dgTravelRequest.Rows.Count; irow++)
                    {
                        Label lblstatus = (Label)dgTravelRequest.Rows[irow].FindControl("lbltrvlbookstatus");
                        //CheckBox ChkTD = (CheckBox)dgTravelRequest.Rows[irow].FindControl("ChkCOS");
                        Label lblchkcos = (Label)dgTravelRequest.Rows[irow].FindControl("lblchkcos");

                        if (lblchkcos.Text == "Yes")
                        {
                            if (Convert.ToString(lblstatus.Text).Trim() == "")
                            {
                                lblmessage.Text = "Travel Status not set for this Travels";
                                return;
                            }
                        }

                    }
                }
            }
            if (Convert.ToString(hdnInboxType.Value).Trim() == "COS")
            {
                if (Convert.ToString(hdnisBookthrugh_COS.Value).Trim() == "Yes")
                {
                    for (Int32 irow = 0; irow < dgAccomodation.Rows.Count; irow++)
                    {
                        Label lblstatus = (Label)dgAccomodation.Rows[irow].FindControl("lbltrvlAccbookstatus");
                        //CheckBox ChkCOS = (CheckBox)dgLocalTravel.Rows[irow].FindControl("ChkCOS");
                        Label lblchkcos = (Label)dgAccomodation.Rows[irow].FindControl("lblchkcos");

                        if (lblchkcos.Text == "Yes")
                        {
                            if (Convert.ToString(lblstatus.Text).Trim() == "")
                            {
                                lblmessage.Text = "Accommodation Status not set for this Travels";
                                return;
                            }
                        }
                    }
                }
            }
            if (Convert.ToString(hdnInboxType.Value).Trim() == "COS")
            {
                if (Convert.ToString(hdnisBookthrugh_COS.Value).Trim() == "Yes")
                {
                    for (Int32 irow = 0; irow < dgLocalTravel.Rows.Count; irow++)
                    {
                        Label lblstatus = (Label)dgLocalTravel.Rows[irow].FindControl("lblLocaltrvlbookstatus");
                        //CheckBox ChkCOS = (CheckBox)dgLocalTravel.Rows[irow].FindControl("ChkCOS");
                        Label lblchkcos = (Label)dgLocalTravel.Rows[irow].FindControl("lblchkcos");

                        if (lblchkcos.Text == "Yes")
                        {
                            if (Convert.ToString(lblstatus.Text).Trim() == "")
                            {
                                lblmessage.Text = "Local Travel Status not set for this Travels";
                                return;
                            }
                        }
                    }
                }
            }
            #endregion

            if (Convert.ToString((txtRemarks.Text).Trim()) == "")
            {
                lblmessage.Text = "Please mention the correction remarks before sending the travel request for correction";
                return;
            }

            //if (Convert.ToString(Session["chkbtnStatus"]).Trim() != "")
            //{
            //    Response.Redirect("~/procs/InboxTravelRequest.aspx?stype=" + hdnInboxType.Value);
            //}
            //Session["chkbtnStatus"] = "Reject button Event is Submitted";

            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                //lblmessage.Text = "Not Saved";
                return;
            }

            //getFomatated dates for Email
            getFromdateTodate_FroEmail();

            string strapprovermails = "";
           // strapprovermails = getRejectionCorrectionmailList();
          //  strapprovermails = getCancellationmailList();

            //strapprovermails = get_approverlist_ifTD_COS();
            strapprovermails = get_approverlist_Rejection_Correction();
            if (Convert.ToString(hdnInboxType.Value).Trim() == "TD" || Convert.ToString(hdnInboxType.Value).Trim() == "COS")
            getTravle_Desk_COS_ApproverCode();

            if (Convert.ToString(hdnInboxType.Value).Trim() == "TD")
                strapprovermails = strapprovermails + ";" + hdnApprovalCOS_mail.Value;
            else if (Convert.ToString(hdnInboxType.Value).Trim() == "COS")
                strapprovermails = strapprovermails + ";" + hdnApprovalTD_mail.Value;
            
           // spm.UpdateForCorrection(Convert.ToInt32(hdnTripid.Value));
            spm.RrejectTravelrequest(Convert.ToInt32(hdnTripid.Value), hndloginempcode.Value, Convert.ToInt32(hdnCurrentApprID.Value), Convert.ToString(txtRemarks.Text).Trim(), "correcttravelrequest");

            //spm.Travel_send_mail_Rejection_Correction(hdnReqEmailaddress.Value, hflEmpName.Value, "Correction of Travel Request", txtTriptype.Text, hdnTrdays.Value, txtReason.Text, txtFromdate.Text, txtToDate.Text, strapprovermails, Convert.ToString(GetApprove_RejectList()).Trim(), txtEmpName.Text);
            spm.Travel_send_mail_Rejection_Correction(hdnReqEmailaddress.Value, hflEmpName.Value, "Correction of Travel Request", txtTriptype.Text, hdnTrdays.Value, txtReason.Text, Convert.ToString(hdnfrmdate_emial.Value).Trim(), Convert.ToString(hdntodate_emial.Value).Trim(), strapprovermails, Convert.ToString(GetApprove_RejectList()).Trim(), txtEmpName.Text, GetIntermediatesList_formail());
            lblmessage.Text = "Travel Resquest has been sent for correction and notofication has been send to the Requester and Previous Intermediate,Approver Levels";
            Response.Redirect("~/procs/InboxTravelRequest.aspx?stype=" + hdnInboxType.Value);
        }
        catch (Exception)
        {

            throw;
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
    protected void btnTra_Details_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";   
        AssigningSessions();
       // Response.Redirect("~/procs/TravelDetails.aspx");
        Response.Redirect("~/procs/TravelDeskTravelDetails.aspx?tripid=" + hdnTripid.Value + "&trdetailsid=" + hdndgTravelRequestMaxID.Value + "&updatedid=0&stype=" + hdnInboxType.Value+ "&freshTripbyTD=Y");
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
            lstIntermediate.DataValueField = "A_EMP_CODE";
            //lstIntermediate.DataValueField = "APPR_ID";
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
            dtEmpDetails = spm.GetEmployeeData(hndloginempcode.Value);

            if (dtEmpDetails.Rows.Count > 0)
            {
                hflEmpName.Value= Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
                hflEmpDesignation.Value = Convert.ToString(dtEmpDetails.Rows[0]["DesginationName"]);
                hflEmpDepartment.Value = Convert.ToString(dtEmpDetails.Rows[0]["Department_Name"]);
               hflGrade.Value = Convert.ToString(dtEmpDetails.Rows[0]["Grade"]).Trim();
                hflloginApprEmail.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
                Session["empgrade"] = hflGrade.Value;
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
    public void getMainTravelDetails()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetMainTravelDetails(Convert.ToInt32(hdnTripid.Value));

        dgTravelRequest.DataSource = null;
        dgTravelRequest.DataBind();
        hdnisBookthrugh_TD.Value = "No";
        if (dtTrDetails.Rows.Count > 0)
        {

            for (Int32 irow = 0; irow < dtTrDetails.Rows.Count; irow++)
            {
                if(Convert.ToString(dtTrDetails.Rows[irow]["travel_through_desk"]).Trim()=="Yes")
                {
                    hdnisBookthrugh_TD.Value = "Yes";
                    hdndgTravelRequestMaxID.Value = Convert.ToString(irow + 2);
                    break;
                }
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

            for (Int32 irow = 0; irow < dtTrDetails.Rows.Count; irow++)
            {
                if (Convert.ToString(dtTrDetails.Rows[irow]["travel_through_desk"]).Trim() == "Yes")
                {
                    hdnisBookthrugh_COS.Value = "Yes";
                    break;
                }
            }

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
            for (Int32 irow = 0; irow < dtTrDetails.Rows.Count; irow++)
            {
                if (Convert.ToString(dtTrDetails.Rows[irow]["Through_COS"]).Trim() == "Yes")
                {
                    hdnisBookthrugh_COS.Value = "Yes";
                    break;
                }
            }

            dgLocalTravel.DataSource = dtTrDetails;
            dgLocalTravel.DataBind();
        }
    }
    public void getTravelRequestData()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetTravelReqData(Convert.ToInt32(hdnTripid.Value));

        if (dtTrDetails.Rows.Count > 0)
        {
            txtEmpCode.Text = Convert.ToString(dtTrDetails.Rows[0]["emp_code"]);
            txtEmpName.Text = Convert.ToString(dtTrDetails.Rows[0]["Emp_Name"]);
            hdnReqEmailaddress.Value = Convert.ToString(dtTrDetails.Rows[0]["Emp_Emailaddress"]);
            hdnTrdays.Value = Convert.ToString(dtTrDetails.Rows[0]["Days"]);
            txtTriptype.Text = Convert.ToString(dtTrDetails.Rows[0]["Type"]);
            txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["FromDate"]);
            txtToDate.Text = Convert.ToString(dtTrDetails.Rows[0]["ToDate"]);
            txtReason.Text = Convert.ToString(dtTrDetails.Rows[0]["trp_reason"]);
            txtAdvance.Text = Convert.ToString(dtTrDetails.Rows[0]["req_adv_amt"]);
            txtreqCur.Text = Convert.ToString(dtTrDetails.Rows[0]["currency_type"]);
            hdnTravelConditionid.Value = Convert.ToString(dtTrDetails.Rows[0]["tr_Conditionid"]);
            hdnTraveltypeid.Value = Convert.ToString(dtTrDetails.Rows[0]["trip_typeid"]);
            txtAccountDetails.Text = Convert.ToString(dtTrDetails.Rows[0]["bankdetails"]);
            lbl_cur.Visible = false;
            txtreqCur.Visible = false;
            txtAccountDetails.Visible = false;
            Span1.Visible = false;
            if (Convert.ToString(hdnInboxType.Value).Trim() == "APP" || Convert.ToString(hdnInboxType.Value).Trim() == "ACC" || Convert.ToString(hdnInboxType.Value).Trim() == "COS")
            {
                if (Convert.ToString(ConfigurationManager.AppSettings["TrvlStatusid_I"]).Trim() == Convert.ToString(hdnTraveltypeid.Value).Trim())
                {
                    lbl_cur.Visible = true;
                    txtreqCur.Visible = true;
                   
                }
            }
            if (Convert.ToString(hdnInboxType.Value).Trim() == "ACC")
            {
                txtAccountDetails.Visible = true;
                Span1.Visible = true;
            }
            else
            {
                txtAccountDetails.Visible = false;
                Span1.Visible = false;
            }

            
        }
    }
    public void AssigningSessions()
    {
        Session["TravelType"] = txtTriptype.Text;
        Session["Fromdate"] = txtFromdate.Text;
        Session["Todate"] = txtToDate.Text;
        Session["ReqEmpCode"] = txtEmpCode.Text;
        //   Session["TravelTypeID"] = hdnTraveltypeid.Value;
        Session["Grade"] = hflGrade.Value;
        //hdnTripid.Value = "0";
        Session["TripID"] = hdnTripid.Value;

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
    protected void GetCuurentApprID()
    {
        int capprid;
        string Actions = "";
        DataTable dtCApprID = new DataTable();
        if (Convert.ToString(hdnInboxType.Value).Trim() == "APP")
            dtCApprID = spm.CurrentApprID(Convert.ToInt32(hdnTripid.Value), hndloginempcode.Value);
        else
            dtCApprID = spm.CurrentApprID_ForTDCOSACC(Convert.ToInt32(hdnTripid.Value), hndloginempcode.Value, Convert.ToString(hdnInboxType.Value).Trim());

        hdnCurrentApprID.Value = Convert.ToString(dtCApprID.Rows[0]["APPR_ID"]);
        Actions = Convert.ToString(dtCApprID.Rows[0]["Action"]);

        if (Convert.ToString(hdnCurrentApprID.Value).Trim() == "")
        {
            lblmessage.Text = "Acton on this REquest not yet taken by other approvals";
            return;
        }
        else if (Convert.ToString(Actions).Trim() != "Pending")
        {
            lblmessage.Text = "You already actioned for this request";
            return;
        }
    }
    public void getnextAppIntermediate()
    {
        DataTable dsapproverNxt = new DataTable();
        dsapproverNxt = spm.GetTravelNextApproverDetails(txtEmpCode.Text, Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
        if (dsapproverNxt.Rows.Count > 0)
        {
            hdnNextApprId.Value = Convert.ToString(dsapproverNxt.Rows[0]["APPR_ID"]);
            hdnNextApprCode.Value = Convert.ToString(dsapproverNxt.Rows[0]["A_EMP_CODE"]);
            hdnNextApprName.Value = Convert.ToString(dsapproverNxt.Rows[0]["Emp_Name"]);
            hdnNextApprEmail.Value = Convert.ToString(dsapproverNxt.Rows[0]["Emp_Emailaddress"]);

            DataTable dtintermediateemail = new DataTable();
            dtintermediateemail = spm.TravelNextIntermediateName(Convert.ToInt32(hdnCurrentApprID.Value), txtEmpCode.Text);
            if (dtintermediateemail.Rows.Count > 0)
            {
                hdnIntermediateEmail.Value = (string)dtintermediateemail.Rows[0]["emp_emailaddress"];
            }
        }
        else
        {
            hdnstaus.Value = "Final Approver";

            //For  Previous approver   
            getPreviousApprovesEmailList();

            hdnIntermediateEmail.Value = "";
            DataTable dtPreInt = new DataTable();
            dtPreInt = spm.TravelPreviousIntermidaterDetails(txtEmpCode.Text, Convert.ToInt32(hdnCurrentApprID.Value));
            if (dtPreInt.Rows.Count > 0)
            {

                for (int i = 0; i < dtPreInt.Rows.Count; i++)
                {
                    if (Convert.ToString(hdnIntermediateEmail.Value).Trim() == "")
                    {
                        hdnIntermediateEmail.Value = Convert.ToString(dtPreInt.Rows[i]["Emp_Emailaddress"]).Trim();
                    }
                    else
                    {
                        hdnIntermediateEmail.Value += ";" + Convert.ToString(dtPreInt.Rows[i]["Emp_Emailaddress"]).Trim();
                    }
                }
            }
        }

    }
    private void getPreviousApprovesEmailList()
    {
        DataTable dtPreApp = new DataTable();
        dtPreApp = spm.TravelPreviousApproverDetails(txtEmpCode.Text, Convert.ToInt32(hdnTripid.Value));
        if (dtPreApp.Rows.Count > 0)
        {

            for (int i = 0; i < dtPreApp.Rows.Count; i++)
            {
                if (Convert.ToString(hflloginApprEmail.Value).Trim() == "")
                {
                    hflloginApprEmail.Value = Convert.ToString(dtPreApp.Rows[i]["Emp_Emailaddress"]).Trim();
                }
                else
                {
                    hflloginApprEmail.Value += ";" + Convert.ToString(dtPreApp.Rows[i]["Emp_Emailaddress"]).Trim();
                }
            }
        }
    }
    private string get_approverlist_ifTD_COS()
    {
        string email_ids = "";
        DataTable dtApproverEmailIds = new DataTable();
        
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "getPreviousAppr_Interm_mails";

        spars[1] = new SqlParameter("@trip_id", SqlDbType.Decimal);
        spars[1].Value = hdnTripid.Value;

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = txtEmpCode.Text.ToString();

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


    private string get_approverlist_Rejection_Correction()
    {
        string email_ids = "";
        DataTable dtApproverEmailIds = new DataTable();

        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "getPreviousAppr_Interm_mails_CorrectionRejection";

        spars[1] = new SqlParameter("@trip_id", SqlDbType.Decimal);
        spars[1].Value = hdnTripid.Value;

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = txtEmpCode.Text.ToString();

        spars[3] = new SqlParameter("@Appr_empcode", SqlDbType.VarChar);
        spars[3].Value = hndloginempcode.Value;

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

    protected string getRejectionCorrectionmailList()
    {
        string email_ids = "";
        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.TravelApproverDetails_Rejection_cancellation(txtEmpCode.Text, Convert.ToInt32(hdnTripid.Value), "get_ApproverDetails_mail_rejection_correction");
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
    protected string GetApprove_RejectList()
    {
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        dtAppRej = spm.GetTravelApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
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

    private string get_TravelDetails_List()
    {

        DataTable dttrvldtls = new DataTable();

        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_getMaintrvl_dtls_ForMail";

        spars[1] = new SqlParameter("@trip_id", SqlDbType.Decimal);
        spars[1].Value = hdnTripid.Value;

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = txtEmpCode.Text.ToString();

        dttrvldtls = spm.getDataList(spars, "SP_GETALLTRAVEL_DETAILS");


        StringBuilder strbuild = new StringBuilder();
        strbuild.Length = 0;
        strbuild.Clear();

        if (dttrvldtls.Rows.Count > 0)
        {
            strbuild.Append("<table border='1'>");
            strbuild.Append("<tr style='background-color:#C5BE97'><td>Mode</td><td> Dep. Date </td><td>Dep. Place</td><td>Arr. Date </td><td>Arr. Place </td><td>Through Travel Desk</td><td>Deviation</td><td>Status</td></tr>");
            for (int irow = 0; irow < dttrvldtls.Rows.Count; irow++)
            {
                strbuild.Append("<tr>");
                strbuild.Append("<td>" + Convert.ToString(dttrvldtls.Rows[irow]["trip_mode"]).Trim() + "</td>");
                strbuild.Append("<td>" + Convert.ToString(dttrvldtls.Rows[irow]["departure_date"]).Trim() + "</td>");
                strbuild.Append("<td>" + Convert.ToString(dttrvldtls.Rows[irow]["departure_place"]).Trim() + "</td>");
                strbuild.Append("<td>" + Convert.ToString(dttrvldtls.Rows[irow]["arrival_date"]).Trim() + "</td>");
                strbuild.Append("<td>" + Convert.ToString(dttrvldtls.Rows[irow]["arrival_place"]).Trim() + "</td>");
                strbuild.Append("<td>" + Convert.ToString(dttrvldtls.Rows[irow]["travel_through_desk"]).Trim() + "</td>");
                strbuild.Append("<td>" + Convert.ToString(dttrvldtls.Rows[irow]["deviation"]).Trim() + "</td>");
                strbuild.Append("<td>" + Convert.ToString(dttrvldtls.Rows[irow]["Status"]).Trim() + "</td>");
                strbuild.Append("</tr>");
            }
            strbuild.Append("</table>");
        }

        return Convert.ToString(strbuild);

    }

    private string get_AccomodationTravelDetails_List()
    {
       
        DataTable dttrvldtls = new DataTable();

        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_getMainAccomodation_dtls_Formail";

        spars[1] = new SqlParameter("@trip_id", SqlDbType.Decimal);
        spars[1].Value = hdnTripid.Value;

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = txtEmpCode.Text.ToString();

        dttrvldtls = spm.getDataList(spars, "SP_GETALLTRAVEL_DETAILS");


        StringBuilder strbuild = new StringBuilder();
        strbuild.Length = 0;
        strbuild.Clear();

        if (dttrvldtls.Rows.Count > 0)
        {
            strbuild.Append("<table border='1'>");
            strbuild.Append("<tr style='background-color:#C5BE97'><td>From Date</td><td> To Date </td><td>Location</td><td>Through COS </td><td> Status </td></tr>");
            for (int irow = 0; irow < dttrvldtls.Rows.Count; irow++)
            {
                strbuild.Append("<tr>");
                strbuild.Append("<td>" + Convert.ToString(dttrvldtls.Rows[irow]["FromDate"]).Trim() + "</td>");
                strbuild.Append("<td>" + Convert.ToString(dttrvldtls.Rows[irow]["ToDate"]).Trim() + "</td>");
                strbuild.Append("<td>" + Convert.ToString(dttrvldtls.Rows[irow]["Location"]).Trim() + "</td>");
                strbuild.Append("<td>" + Convert.ToString(dttrvldtls.Rows[irow]["travel_through_desk"]).Trim() + "</td>");
                strbuild.Append("<td>" + Convert.ToString(dttrvldtls.Rows[irow]["Status"]).Trim() + "</td>");                
                strbuild.Append("</tr>");
            }
            strbuild.Append("</table>");
        }

        return Convert.ToString(strbuild);

    }

    private string get_LocalTravelDetails_List()
    {

        DataTable dttrvldtls = new DataTable();

        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_getMainlocaltrvl_dtls_Formail";

        spars[1] = new SqlParameter("@trip_id", SqlDbType.Decimal);
        spars[1].Value = hdnTripid.Value;

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = txtEmpCode.Text.ToString();

        dttrvldtls = spm.getDataList(spars, "SP_GETALLTRAVEL_DETAILS");


        StringBuilder strbuild = new StringBuilder();
        strbuild.Length = 0;
        strbuild.Clear();

        if (dttrvldtls.Rows.Count > 0)
        {
            strbuild.Append("<table border='1'>");
            strbuild.Append("<tr style='background-color:#C5BE97'><td>From Date</td><td> To Date </td><td>Location</td><td>Through COS </td><td> Status </td></tr>");
            for (int irow = 0; irow < dttrvldtls.Rows.Count; irow++)
            {
                strbuild.Append("<tr>");
                strbuild.Append("<td>" + Convert.ToString(dttrvldtls.Rows[irow]["FromDate"]).Trim() + "</td>");
                strbuild.Append("<td>" + Convert.ToString(dttrvldtls.Rows[irow]["ToDate"]).Trim() + "</td>");
                strbuild.Append("<td>" + Convert.ToString(dttrvldtls.Rows[irow]["Location"]).Trim() + "</td>");
                strbuild.Append("<td>" + Convert.ToString(dttrvldtls.Rows[irow]["travel_through_desk"]).Trim() + "</td>");
                strbuild.Append("<td>" + Convert.ToString(dttrvldtls.Rows[irow]["Status"]).Trim() + "</td>");
                strbuild.Append("</tr>");
            }
            strbuild.Append("</table>");
        }

        return Convert.ToString(strbuild);

    }


    private string get_approverlist_ifTD_COS_finalApproval()
    {
        string email_ids = "";
        DataTable dtApproverEmailIds = new DataTable();

        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "getAppr_Interm_mails_finalApprovers";

        spars[1] = new SqlParameter("@trip_id", SqlDbType.Decimal);
        spars[1].Value = hdnTripid.Value;

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = txtEmpCode.Text.ToString();

        spars[3] = new SqlParameter("@Appr_empcode", SqlDbType.VarChar);
        spars[3].Value = hndloginempcode.Value;

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
    public void getTravle_Desk_COS_ApproverCode()
    {

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Sp_get_TD_COS_apprver_code";

        spars[1] = new SqlParameter("@exp_sr_no", SqlDbType.Int);
        spars[1].Value = DBNull.Value;

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = hndloginempcode.Value;

        dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");

        //Travel Desk Approver Code
        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            
            hdnApprovalTD_Code.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_code"]).Trim();
            hdnApprovalTD_ID.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_id"]).Trim();
            hdnApprovalTD_mail.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();
            hdnApprovalTD_Name.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_remarks"]).Trim();
            
            btnReject.Visible = false;
        }

        //COS Approver Code
        if (dsTrDetails.Tables[1].Rows.Count > 0)
        {
            hdnApprovalCOS_Code.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["approver_emp_code"]).Trim();
            hdnApprovalCOS_ID.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["app_id"]).Trim();
            hdnApprovalCOS_mail.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["approver_emp_mail"]).Trim();
            hdnApprovalCOS_Name.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["app_remarks"]).Trim();
            btnReject.Visible = false;
        }

        //ACC Approver Code
        if (dsTrDetails.Tables[2].Rows.Count > 0)
        {
            hdnApprovalACC_Code.Value = Convert.ToString(dsTrDetails.Tables[2].Rows[0]["approver_emp_code"]).Trim();
            hdnApprovalACC_ID.Value = Convert.ToString(dsTrDetails.Tables[2].Rows[0]["app_id"]).Trim();
            hdnApprovalACC_mail.Value = Convert.ToString(dsTrDetails.Tables[2].Rows[0]["approver_emp_mail"]).Trim();
            hdnApprovalACC_Name.Value = Convert.ToString(dsTrDetails.Tables[2].Rows[0]["app_remarks"]).Trim();
            btnReject.Visible = false;
        }
        
    }

    protected void checkTO_COS_ACC()
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_checkCurnt_TDCOSACC_Approver";

            spars[1] = new SqlParameter("@trip_id", SqlDbType.Decimal);
            spars[1].Value = hdnTripid.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = hndloginempcode.Value;

            spars[3] = new SqlParameter("@trvl_expType", SqlDbType.VarChar);
            spars[3].Value = hdnInboxType.Value;
                       

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLTRAVEL_DETAILS");
            //Travel Request Count
            hdnisApprover_TDCOS.Value = "Approver";
            hdnApproverTDCOS_status.Value = "";
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                hdnisApprover_TDCOS.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_type"]).Trim();
                btnReject.Visible = false;
                btnCorrection.Visible = false;
                hdnApproverTDCOS_status.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Action"]).Trim();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected Boolean get_Travel_Approval_Details_forACC()
    {
        Boolean blnisFnlAcc = false;
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Trvl_Approve_status_for_AddACC";

            spars[1] = new SqlParameter("@trip_id", SqlDbType.Decimal);
            spars[1].Value = hdnTripid.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = hndloginempcode.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");
             
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            { 
                for(Int32 irow=0;irow<dsTrDetails.Tables[0].Rows.Count;irow++)
                {
                    if (Convert.ToString(dsTrDetails.Tables[0].Rows[irow]["Action"]).Trim() == "Approved")
                    {
                        //hdnCheckTRStatus_forACC.Value = "Yes";
                        blnisFnlAcc = true;
                        if (dsTrDetails.Tables[1].Rows.Count > 0)
                        {
                            if (Convert.ToString(dsTrDetails.Tables[1].Rows[0]["app_id"]).Trim() == Convert.ToString(dsTrDetails.Tables[0].Rows[irow]["appr_id"]).Trim())
                            {
                                blnisFnlAcc = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        //hdnCheckTRStatus_forACC.Value = "No";
                        blnisFnlAcc = false;
                        break;
                    }
                }
            }
             
            return blnisFnlAcc;
             
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
        return blnisFnlAcc;

    }
 

    #endregion

    protected void dgTravelRequest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkTravelDetailsEdit = e.Row.FindControl("lnktravlsDtlsEidt") as LinkButton;
            CheckBox ChkCOS = e.Row.FindControl("ChkCOS") as CheckBox;
            ChkCOS.Enabled = false;
            lnkTravelDetailsEdit.Enabled = false;
            lnkTravelDetailsEdit.Visible = false;
            if (ChkCOS.Checked == true)
            {
                if (Convert.ToString(hdnInboxType.Value).Trim() == "TD")
                {
                    if (Convert.ToString(hdnisBookthrugh_TD.Value).Trim() == "Yes")
                    {
                        lnkTravelDetailsEdit.Enabled = true;
                        lnkTravelDetailsEdit.Visible = true;
                    }
                }
            }
        }
    }
    protected void dgAccomodation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkAccomodationdit = e.Row.FindControl("lnkAccomodationdit") as LinkButton;
            CheckBox ChkCOS = e.Row.FindControl("ChkCOS") as CheckBox;
            ChkCOS.Enabled = false;
            lnkAccomodationdit.Enabled = false;
            lnkAccomodationdit.Visible = false;
            if (ChkCOS.Checked == true)
            {
                if (Convert.ToString(hdnInboxType.Value).Trim() == "COS")
                {
                    if (Convert.ToString(hdnisBookthrugh_COS.Value).Trim() == "Yes")
                    {
                        lnkAccomodationdit.Enabled = true;
                        lnkAccomodationdit.Visible = true;
                    }

                }
            }
        }
    }
    protected void dgLocalTravel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkLocalTravleEdit = e.Row.FindControl("lnkLocalTravleEdit") as LinkButton;
            CheckBox ChkCOS = e.Row.FindControl("ChkCOS") as CheckBox;
            ChkCOS.Enabled = false;
            lnkLocalTravleEdit.Enabled = false;
            lnkLocalTravleEdit.Visible = false;
            if (ChkCOS.Checked == true)
            {
                if (Convert.ToString(hdnInboxType.Value).Trim() == "COS")
                {
                    if (Convert.ToString(hdnisBookthrugh_COS.Value).Trim() == "Yes")
                    {
                        lnkLocalTravleEdit.Enabled = true;
                        lnkLocalTravleEdit.Visible = true;
                    }
                }
            }
        }
    }
    protected void lnktravlsDtlsEidt_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnTripid.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnTravelDtlsId.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[1]).Trim();
        Response.Redirect("~/procs/TravelDeskTravelDetails.aspx?tripid=" + hdnTripid.Value + "&trdetailsid=" + hdnTravelDtlsId.Value + "&updatedid=0&stype="+hdnInboxType.Value+"&freshTripbyTD=N");
        
        
    }
    protected void lnkAccomodationdit_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnTripid.Value = Convert.ToString(dgAccomodation.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnTravelDtlsId.Value = Convert.ToString(dgAccomodation.DataKeys[row.RowIndex].Values[1]).Trim();

        txtFromdate_Accm.Attributes.Add("onkeypress", "return noanyCharecters(event);");
        txtToDate_Accm.Attributes.Add("onkeypress", "return noanyCharecters(event);");
        txtLocation_Accm.Attributes.Add("onkeypress", "return noanyCharecters(event);");
        txtStatus_Accm.Attributes.Add("onkeypress", "return noanyCharecters(event);");
        txtfare_Accm.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
        txtAcctype_Accm.Attributes.Add("onkeypress", "return noanyCharecters(event);");

        AssigningSessions();
        lblmessage.Text = "";
        GetCities();
        //txtEmpCode.Text = Convert.ToString(Session["ReqEmpCode"]);
        txtTravelType_Accm.Text = Convert.ToString(Session["TravelType"]);
        hdnTryiptypeid.Value = Convert.ToString(Session["TripTypeId"]);
        hdnfromdate_Accm.Value = Convert.ToString(Session["Fromdate"]);
        hdnTodate_Accm.Value = Convert.ToString(Session["Todate"]);
        txtLocation_Accm.Text = Convert.ToString(Session["Location"]);

        //hdnTripid.Value = "1";
        //hdnAccdtlsid.Value = "1";
        //getAccDetailsEdit();
        EnabledFalse_Accm();

        //if (Request.QueryString.Count > 2)
        //{
        hdnTripid.Value = hdnTripid.Value;
        hdnAccdtlsid.Value = hdnTravelDtlsId.Value;
            //hdnInboxType.Value = Convert.ToString(Request.QueryString[3]).Trim();
            getAccDetailsEdit();

        //}


            accmo_delete_btn.Visible = false;

        ClientScript.RegisterStartupScript(Page.GetType(), "OnLoad", "SetCntrls('" + txtStatus_Accm.Text + "');", true);
        ClientScript.RegisterStartupScript(Page.GetType(), "OnLoad", "validateTripType_Accm('" + txtAcctype_Accm.Text + "');", true);
        DivAccm.Visible = true;
        //Response.Redirect("~/procs/TravelDeskAccomodation.aspx?tripid=" + hdnTripid.Value + "&trdetailsid=" + hdnTravelDtlsId.Value + "&updatedid=0&stype="+hdnInboxType.Value);
    }
    protected void lnkLocalTravleEdit_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnTripid.Value = Convert.ToString(dgLocalTravel.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnTravelDtlsId.Value = Convert.ToString(dgLocalTravel.DataKeys[row.RowIndex].Values[1]).Trim();
        Response.Redirect("~/procs/TravelDeskLocalTravel.aspx?tripid=" + hdnTripid.Value + "&trdetailsid=" + hdnTravelDtlsId.Value + "&updatedid=0&stype="+hdnInboxType.Value);
    }
    protected string GetIntermediatesList_formail()
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/InboxTravelRequest.aspx?stype=" + hdnInboxType.Value);
    }
    protected void dgTravelRequest_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            if (Convert.ToString(hdnInboxType.Value).Trim() == "APP" || Convert.ToString(hdnInboxType.Value).Trim() == "ACC")
            {
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[7].Visible = false;
            }
            else
            {
                e.Row.Cells[8].Visible = true;
                e.Row.Cells[7].Visible = true;
                 
            }
        }
       
    }
    protected void dgAccomodation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            if (Convert.ToString(hdnInboxType.Value).Trim() == "APP" || Convert.ToString(hdnInboxType.Value).Trim() == "ACC")
            {
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[4].Visible = false;
            }
            else
            {
                e.Row.Cells[5].Visible = true;
                e.Row.Cells[4].Visible = true;

            }
        }
    }
    protected void dgLocalTravel_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            if (Convert.ToString(hdnInboxType.Value).Trim() == "APP" || Convert.ToString(hdnInboxType.Value).Trim() == "ACC")
            {
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[4].Visible = false;
            }
            else
            {
                e.Row.Cells[5].Visible = true;
                e.Row.Cells[4].Visible = true;
            }
        }

    }

    protected void lstStatus_Accm_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtStatus_Accm.Text = lstStatus_Accm.SelectedItem.Text;
        PopupControlExtender5.Commit(lstStatus_Accm.SelectedItem.Text);
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetCntrls('" + lstStatus_Accm.SelectedItem.Text + "');", true);
    }
    protected void lstAccType_Accm_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAcctype_Accm.Text = lstAccType_Accm.SelectedItem.Text;

        PopupControlExtender3.Commit(lstAccType_Accm.SelectedItem.Text);
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "validateTripType_Accm('" + lstAccType_Accm.SelectedItem.Text + "');", true);
    }
    public void GetCities()
    {
        DataTable dtCities = new DataTable();
        dtCities = spm.getCitiesDetails();
        if (dtCities.Rows.Count > 0)
        {
            //lstLocation.DataSource = dtCities;
            //lstLocation.DataTextField = "CITYNAME";
            //lstLocation.DataValueField = "CITYID";
            //lstLocation.DataBind();

        }
    }
    public void EnabledFalse_Accm()
    {
        txtTripId_Accm.Enabled = false;
        txtTravelType_Accm.Enabled = false;
        txtFromdate_Accm.Enabled = false;
        txtToDate_Accm.Enabled = false;
        txtRequirement_Accm.Enabled = false;
        txtLocation_Accm.Enabled = false;

    }
    public void getAccDetailsEdit()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetAccomodationDetailsEdit_TDCOS(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnAccdtlsid.Value));
        if (dtTrDetails.Rows.Count > 0)
        {
            txtTripId_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_id"]);
            txtTravelType_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_description"]);
            txtFromdate_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["From Date"]);
            txtToDate_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["To Date"]);
            txtRequirement_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["local_accomodation_remarks"]);
            txtLocation_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["Location"]);

            if (Convert.ToString(dtTrDetails.Rows[0]["Acc_Charges"]).Trim() != "")
                txtfare_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["Acc_Charges"]);

            if (Convert.ToString(dtTrDetails.Rows[0]["Status"]).Trim() != "")
            {
                txtStatus_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["Status"]);
                lstStatus_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["Status"]);
            }
            if (Convert.ToString(dtTrDetails.Rows[0]["accomodation_type"]).Trim() != "")
            {
                txtAcctype_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["accomodation_type"]);
            }
            else
            {
                txtAcctype_Accm.Text = "";
            }


        }
    }
    protected void accmo_btnSave_Click(object sender, EventArgs e)
    {
        #region Check All Fields Blank


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


        //  lblmessage.Text = "";

        //if (Convert.ToString(txtAcctype.Text).Trim() == "")
        //{
        //    lblmessages.Text = "Please select Accommodation Type.";
        //    return;
        //}
        if (Convert.ToString(txtStatus_Accm.Text).Trim() == "Booked")
        {
            //if (Convert.ToString(txtAcctype.Text).Trim() == "Hotel")
            //{
            //    if (Convert.ToString(txtfare.Text).Trim() == "")
            //    {
            //        lblmessages.Text = "Please enter the Charges";
            //        return;
            //    }
            //}

            if (Convert.ToString(txtfare_Accm.Text).Trim() != "")
            {
                string[] strdate;
                strdate = Convert.ToString(txtfare_Accm.Text).Trim().Split('.');
                if (strdate.Length > 2)
                {
                    txtfare_Accm.Text = "0";
                    lblmessage.Text = "Please enter correct Fare.";
                    return;
                }
                #region Commented by R1 29-03-2018
                //Decimal dfare = 0;
                //dfare=Convert.ToDecimal(txtfare.Text);
                //if (dfare == 0)
                //{
                //    lblmessage.Text = "Please enter correct Fare.";
                //    return;
                //}
                #endregion Commented by R1 29-03-2018
            }
        }

        if (Convert.ToString(Session["chkTrvlTDCOSbtnStatus"]).Trim() != "")
        {
            Response.Redirect("~/procs/AppTravelrequest.aspx?reqid=" + hdnTripid.Value + "&stype=" + hdnInboxType.Value);
        }
        Session["chkTrvlTDCOSbtnStatus"] = "Accomdation button Event is Submitted";



        if (Convert.ToString(txtStatus_Accm.Text).Trim() == "Not Booked")
        {
            txtfare_Accm.Text = "0";
        }
        Decimal dcharges = 0;
        if (Convert.ToString(txtfare_Accm.Text).Trim() != "")
            dcharges = Math.Round(Convert.ToDecimal(txtfare_Accm.Text), 2);


        spm.UpdateAccomodationDetailsForCOS(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnAccdtlsid.Value), dcharges, txtStatus_Accm.Text, Convert.ToString(txtAcctype_Accm.Text).Trim());
        //Response.Redirect("~/procs/AppTravelrequest.aspx?reqid=" + hdnTripid.Value + "&stype=" + hdnInboxType.Value);

        DivAccm.Visible = false;
        #endregion

    }
    protected void accmo_cancel_btn_Click(object sender, EventArgs e)
    {
        DivAccm.Visible = false;
        //Response.Redirect("~/procs/AppTravelrequest.aspx?reqid=" + hdnTripid.Value + "&stype=" + hdnInboxType.Value);
    }

    protected void accmo_delete_btn_Click(object sender, EventArgs e)
    {
        #region Check For Blank Fields
        lblmessage.Text = "";
        if (Convert.ToString(txtTravelType_Accm.Text).Trim() == "")
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
        if (Convert.ToString(txtLocation_Accm.Text).Trim() == "")
        {
            lblmessage.Text = "Please Select Departure Place For Travel";
            return;
        }

        #endregion
        #region Delete TravelDetails

        if (Convert.ToString(Session["chkTrvlTDCOSbtnStatus"]).Trim() != "")
        {
            Response.Redirect("~/procs/AppTravelrequest.aspx?reqid=" + hdnTripid.Value + "&stype=" + hdnInboxType.Value);
        }
        Session["chkTrvlTDCOSbtnStatus"] = "Accomdation button Event is Submitted";

        spm.DeleteAccomodationlDetails(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnAccdtlsid.Value), txtEmpCode.Text);
        lblmessage.Text = "Accommodation Details Deleted Successfully";
        //Response.Redirect("~/procs/AppTravelrequest.aspx?reqid=" + hdnTripid.Value + "&stype=" + hdnInboxType.Value);
        #endregion
    }
}