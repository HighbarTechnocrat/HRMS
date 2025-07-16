using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TravelRequest_exp : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    DataSet dspaymentVoucher_Apprs = new DataSet();
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

            lblmessage.Text = "";
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");


            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/travelindex.aspx");
            }
            else
            {
                Page.SmartNavigation = true;



                mobile_btnPrintPV.Visible = false;
                CultureInfo hindi = new CultureInfo("hi-IN");
                if (!Page.IsPostBack)
                {
                    hdndept_id.Value = "0";
                    hdncomp_name.Value = "0";
                    hdnIsDraft.Value = "0";
                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["trvlingExpfiles"]).Trim());
                    txtTriptype.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtFromdate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
                    txtToDate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
                    txtToDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtAdvance.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtLessAdvTaken.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtdailyhaltingallowance.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtTotAmtClaimed.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    //txtTotAmtClaimed.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtnetPaybltoComp.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtnetPaybltoEmp.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");

                    txtTravelType.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtTripId.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtDeviation.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtFromdate_Trvl.Attributes.Add("onkeypress", "return noanyCharecters(event);");

                    txtFromTime.Attributes.Add("onkeypress", "return onCharOnlyNumber_Time(event);");
                    txtToDate_Trvl.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtToTime.Attributes.Add("onkeypress", "return onCharOnlyNumber_Time(event);");
                    txtfare.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtFromdate_Trvl.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
                    txtToDate_Trvl.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
                    txtFoodAllowance.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtFoodEligibilty.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
                    txtTravelMode.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtTravelMode.Attributes.Add("onkeyDown", "return noanyCharecters(event);");

                    txtFromdate_Accm.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtToDate_Accm.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    //txtLocation.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtPaidBy_Accm.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtCharges.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtEligibility_Accm.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtFlatElg_Accm.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtFlatPaid_Accm.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtFlatChg_Accm.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtAdditionalFoodExp_exp.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtnoofDays.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtFromdate_Accm.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
                    txtToDate_Accm.Attributes.Add("onkeyDown", "return noanyCharecters(event);");

                    txtaddintionalExpens_Accm.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");

                    txtFromdate_Locl.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtToDate_Locl.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtTravelMode_Locl.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtCharges_Locl.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtFromdate_Locl.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
                    txtToDate_Locl.Attributes.Add("onkeyDown", "return noanyCharecters(event);");

                    txtFromdate_Oth.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtFromdate_Oth.Attributes.Add("onkeyDown", "return noanyCharecters(event);");

                    txtExpdtls_Oth.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtpaidby.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtDeviation.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtAmt_Oth.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtnoofDays_Oth.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    GetPalceJoin();
                    lblmessage.Text = "";
                    txtEmpCode.Text = Convert.ToString(Session["Empcode"]);
                    btnMod.Visible = false;
                    btnCancel.Visible = false;


                    this.lstApprover.SelectedIndex = 0;
                    hdnTravelConditionid.Value = "2";
                    hdnTraveltypeid.Value = "1";

                    if (Convert.ToString(lstTripType.SelectedValue).Trim() != "")
                        hdnTraveltypeid.Value = lstTripType.SelectedValue;

                    hdnTrdays.Value = "2";
                    hdnEligible.Value = "Eligible";

                    check_isEmployee_NewJoinee();

                    GetCompany_Location();
                    GetDepartMentList();
                    GetEmployeeDetails();
                    GetTripDetails();
                    hdnexp_id.Value = "0";
                    //txtAdvance.Visible = false;
                    //spnadv.Visible = false;
                    var isDraft = false;
                    if (Request.QueryString.Count > 0)
                    {
                        hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdnexp_id.Value = Convert.ToString(Request.QueryString[1]).Trim();

                    }
                    if (hdnexp_id.Value == "0")
                    {
                        isDraft = GetStatusDraft();
                    }
                    if (Convert.ToString(Session["TripTypeId"]) != null && Convert.ToString(Session["TripTypeId"]).Trim() != "")
                    {
                        lstTripType.SelectedValue = Convert.ToString(Session["TripTypeId"]);
                        hdnActualTrvlDays.Value = Convert.ToString(Session["acttrvldays"]);
                    }


                    if (Convert.ToString(hdnTripid.Value).Trim() != "0")
                    {

                        setVisibleCntrls();
                        getTravelsDetails_usingTripid();

                        if (Request.QueryString.Count > 2)
                        {

                            if (Convert.ToString(hdnexp_id.Value).Trim() == "0")
                                InsertExp_DatatoTempTables_trvl();
                            //setVisibleCntrls();
                        }
                        else if (isDraft == true)
                        {
                            if (Convert.ToString(hdnexp_id.Value).Trim() == "0")
                                InsertExp_DatatoTempTables_trvl();
                            //setVisibleCntrls();
                        }
                    }


                    //   getApproverdata();
                    editform.Visible = true;
                    divbtn.Visible = false;

                    trvl_btnSave.Visible = true;
                    btnMod.Visible = false;
                    if (Convert.ToString(hdnexp_id.Value).Trim() != "0")
                    {
                        if (Request.QueryString.Count > 2)
                        {
                            InsertExp_DatatoTempTables();
                            getExpenseUploadedFiles();

                        }
                        else if (isDraft == true)
                        {
                            InsertExp_DatatoTempTables();
                            getExpenseUploadedFiles();
                        }
                        trvl_btnSave.Visible = true;
                        btnCancel.Visible = true;
                        getExpTravelDetails();
                        getApproverlist();
                    }

                    //  getApproverlist();
                    getTrStatus();

                    GetExpenseTravelDetails();
                    GetExpenseAccomodationDetails();
                    getExpenseLcoalTravel();
                    getExpensedtls_from_temp();
                    getExpenseUploadedFiles();

                    if (dgTravelRequest.Rows.Count > 0)
                    {
                        for (Int32 irow = 0; irow < dgTravelRequest.Rows.Count; irow++)
                        {
                            if (Convert.ToString(dgTravelRequest.Rows[irow].Cells[7].Text).Trim() == "Yes")
                            {
                                hdnEligible.Value = "Deviation";
                                break;
                            }
                        }
                    }
                    if (dgAccomodation.Rows.Count > 0)
                    {
                        for (Int32 irow = 0; irow < dgAccomodation.Rows.Count; irow++)
                        {
                            if (Convert.ToString(dgAccomodation.Rows[irow].Cells[6].Text).Trim() == "Yes")
                            {
                                if (Convert.ToString(hdnEligible.Value).Trim() == "Deviation")
                                    break;

                                hdnEligible.Value = "Deviation";
                                break;
                            }
                        }
                    }

                    if (dgLocalTravel.Rows.Count > 0)
                    {
                        for (Int32 irow = 0; irow < dgLocalTravel.Rows.Count; irow++)
                        {
                            if (Convert.ToString(dgLocalTravel.Rows[irow].Cells[6].Text).Trim() == "Yes")
                            {
                                if (Convert.ToString(hdnEligible.Value).Trim() == "Deviation")
                                    break;

                                hdnEligible.Value = "Deviation";
                                break;
                            }
                        }
                    }

                    GetExpenseContitionId();

                    checkExpApprovalStatus();

                    if (Convert.ToString(hdnmainexpStatus.Value).Trim() == "1" && Convert.ToString(hdn_apprStatus.Value).Trim() == "Approved")
                    {
                        trvl_accmo_btn.Visible = false;
                        trvl_localbtn.Visible = false;
                        btnTra_Details.Visible = false;
                        lnkbtn_expdtls.Visible = false;

                        dgTravelRequest.Enabled = false;
                        dgAccomodation.Enabled = false;
                        dgLocalTravel.Enabled = false;
                        btnCancel.Visible = false;
                        btnMod.Visible = false;

                        txtReasonDeviation.Enabled = false;
                        ploadexpfile.Enabled = false;

                        txtAdvance.Enabled = false;

                        if (Convert.ToString(txtAdvance.Text).Trim() != "")
                        {
                            decimal parsed4 = decimal.Parse(Convert.ToString(txtAdvance.Text), CultureInfo.InvariantCulture);
                            txtAdvance.Text = string.Format(hindi, "{0:#,#0.00}", parsed4);
                        }

                        txtreqCur.Enabled = false;
                        txtReason.Enabled = false;
                        txtFromdate.Enabled = false;
                        txtToDate.Enabled = false;
                        txtTriptype.Enabled = false;
                        Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                        txtTriptype.BackColor = color;
                        trvl_btnSave.Visible = false;
                        gvexpensdtls.Enabled = false;
                        btn_Draft.Visible = false;

                    }
                    if (Convert.ToString(hdnmainexpStatus.Value).Trim() == "2")
                    {
                        trvl_accmo_btn.Visible = false;
                        trvl_localbtn.Visible = false;
                        btnTra_Details.Visible = false;
                        lnkbtn_expdtls.Visible = false;

                        txtReasonDeviation.Enabled = false;
                        ploadexpfile.Enabled = false;

                        dgTravelRequest.Enabled = false;
                        dgAccomodation.Enabled = false;
                        dgLocalTravel.Enabled = false;
                        btnCancel.Enabled = true;
                        btnMod.Visible = false;
                        txtAdvance.Enabled = false;

                        if (txtAdvance.Text != "")
                        {
                            decimal parsed4 = decimal.Parse(Convert.ToString(txtAdvance.Text), CultureInfo.InvariantCulture);
                            txtAdvance.Text = string.Format(hindi, "{0:#,#0.00}", parsed4);
                        }


                        txtreqCur.Enabled = false;
                        txtReason.Enabled = false;
                        txtFromdate.Enabled = false;
                        txtToDate.Enabled = false;
                        txtTriptype.Enabled = false;
                        Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                        txtTriptype.BackColor = color;
                        trvl_btnSave.Visible = false;
                        gvexpensdtls.Enabled = false;
                        trvl_btnSave.Visible = false;
                        btnCancel.Visible = false;

                        gvexpensdtls.Visible = false;

                        getExpensedtls_from_Main();
                        SpnAccountAmount.Visible = true;
                        txtAccountAmount.Visible = true;
                    }
                    if (Convert.ToString(hdnmainexpStatus.Value).Trim() == "3" || Convert.ToString(hdnmainexpStatus.Value).Trim() == "4")
                    {
                        trvl_accmo_btn.Visible = false;
                        trvl_localbtn.Visible = false;
                        btnTra_Details.Visible = false;
                        lnkbtn_expdtls.Visible = false;

                        txtReasonDeviation.Enabled = false;
                        ploadexpfile.Enabled = false;

                        dgTravelRequest.Enabled = false;
                        dgAccomodation.Enabled = false;
                        dgLocalTravel.Enabled = false;
                        btnCancel.Visible = false;
                        btnMod.Visible = false;
                        gvexpensdtls.Enabled = false;
                        trvl_btnSave.Visible = false;

                    }


                    //if (Convert.ToString(hdnmainexpStatus.Value).Trim() == "1" && Convert.ToString(hdnApprovalStatusExp.Value).Trim()=="Pending")
                    //{
                    //    trvl_btnSave.Visible = true;
                    //    btnCancel.Visible = true;

                    //}
                    if (chkIsNewJoinExp_req.Checked)
                        getApproverlist_Newjoinee();
                    else
                        getApproverlist_New();

                    CalculateClaimAmt();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";

                    if (hdnIsDraft.Value == "1" || hdnexp_id.Value == "0")
                    {
                        btn_Draft.Visible = true;
                        if (chkIsNewJoinExp_req.Checked)
                        {
                            getApproverlist_Newjoinee();
                        }
                        else
                        {
                            getApproverlist_Draft();
                        }
                    }
                    else
                    {
                        btn_Draft.Visible = false;
                    }
                }
                else
                {
                    CalculateClaimAmt();
                }
            }

            travelDetailsRowCreated();
            //editform.Visible = false;
            //divBtnHide.Visible = false;
            //lblmessage.Text = "Due to some critical maintenance activity, claims will not be allowed from 06-10-22 till 08-10-22!";


        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    public void GetCompany_Location()
    {
        DataTable lstPosition = new DataTable();
        lstPosition = spm.getMobileVoucherProject_List();
        if (lstPosition.Rows.Count > 0)
        {
            ddl_ProjectName.DataSource = lstPosition;
            ddl_ProjectName.DataTextField = "Location_name";
            ddl_ProjectName.DataValueField = "comp_code";
            ddl_ProjectName.DataBind();
            ddl_ProjectName.Items.Insert(0, new ListItem("Select  Project", "0"));
        }
    }
    public void GetDepartMentList()
    {
        DataTable lstPosition = new DataTable();
        lstPosition = spm.getPaymentVoucherDepartment_List();
        if (lstPosition.Rows.Count > 0)
        {
            ddl_DeptName.DataSource = lstPosition;
            ddl_DeptName.DataTextField = "Department_Name";
            ddl_DeptName.DataValueField = "Department_id";
            ddl_DeptName.DataBind();
            ddl_DeptName.Items.Insert(0, new ListItem("Select  Department", "0"));
        }
    }

    protected void lstTripType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (gvexpensdtls.Rows.Count > 0)
        {
            lblmessage.Text = "Please delete added details first inorder to change the Travel Type!";
            lstTripType.SelectedValue = hdnTraveltypeid.Value;
            return;
        }
        txtTriptype.Text = lstTripType.SelectedItem.Text;

        //PopupControlExtender2.Commit(lstTripType.SelectedItem.Text);
        //lblmessage.Text = "";
        hdnTraveltypeid.Value = "";
        hdnTraveltypeid.Value = lstTripType.SelectedValue;
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "validateTripType(" + hdnTraveltypeid.Value + ");", true);
    }
    protected void trvl_btnSave_Click(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";
        if (FromDateValidation_exp() == true)
        {
            return;
        }
        #region Check For Blank Fields
        lblmessage.Text = "";

        if (dgTravelRequest.Rows.Count == 0)
        {
            lblmessage.Text = "Please enter Travel Details.";
            return;
        }

        if (Convert.ToString(txtTotAmtClaimed.Text).Trim() == "" && Convert.ToString(txtTotAmtClaimed.Text).Trim() == "0")
        {
            lblmessage.Text = "Please calculate the Expense by clicking the Calculate button";
            return;
        }
        if (Convert.ToString(lstTripType.SelectedValue).Trim() == "" || Convert.ToString(lstTripType.SelectedValue).Trim() == "0")
        {
            lblmessage.Text = "Please select Travel Type";
            return;
        }

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
        if (Convert.ToString(ddl_ProjectName.SelectedItem.Text).Trim() == "" || Convert.ToString(ddl_ProjectName.SelectedItem.Text).Trim() == "0")
        {
            lblmessage.Text = "Please select Project Name";
            return;
        }
        else
        {
            //if (Convert.ToString(Txt_ProjectName.Text).Trim() == "Head Office")
            if (Convert.ToString(ddl_ProjectName.SelectedItem).Contains("Head Office"))
            {
                if (Convert.ToString(ddl_DeptName.SelectedItem.Text).Trim() == "" || Convert.ToString(ddl_DeptName.SelectedItem.Text).Trim() == "0" || Convert.ToString(ddl_DeptName.SelectedItem.Text).Trim() == "Select  Department")
                {
                    lblmessage.Text = "Please select Department Name";
                    return;
                }
            }
        }
        #endregion


        Boolean blnchk = false;
        #region check Expenses is Submitted

        foreach (GridViewRow irow in dgTravelRequest.Rows)
        {
            Label lblSubmitStatus = (Label)irow.FindControl("lblSubmitStatus");
            if (lblSubmitStatus.Text == "No")
            {


                irow.BackColor = Color.Yellow;
                blnchk = true;
            }
        }
        foreach (GridViewRow irow in dgAccomodation.Rows)
        {
            Label lblSubmitStatus = (Label)irow.FindControl("lblSubmitStatus");
            if (lblSubmitStatus.Text == "No")
            {
                irow.BackColor = Color.Yellow;
                blnchk = true;
            }
        }
        foreach (GridViewRow irow in dgLocalTravel.Rows)
        {
            Label lblSubmitStatus = (Label)irow.FindControl("lblSubmitStatus");
            if (lblSubmitStatus.Text == "No")
            {
                irow.BackColor = Color.Yellow;
                blnchk = true;
            }
        }
        if (blnchk == true)
        {


            lblmessage.Text = "Please Submit details for highlighted records";
            return;
        }
        #endregion

        #region Not required

        /*
         if (Convert.ToString(lstTripType.SelectedValue).Trim() == "1" && Convert.ToString(hdnTripid.Value).Trim()!="0")
         {
             if (gvexpensdtls.Rows.Count > 0)
             {

                 Decimal dpaidemp = 0;
                 Decimal dpaidcomp = 0;
                 Decimal dtotamt = 0;
                 Decimal dEligibiltyamt = 0;
                 Decimal ddiffyamt = 0;


                 for (Int32 irow = 0; irow < gvexpensdtls.Rows.Count; irow++)
                 {
                     dpaidemp = 0;
                     dpaidcomp = 0;
                     dtotamt = 0;
                     dEligibiltyamt = 0;
                     ddiffyamt = 0;
                     blnchk = false;

                     #region code not required
                     //if (Convert.ToString(gvexpensdtls.Rows[irow].Cells[4].Text).Trim() != "" && Convert.ToString(gvexpensdtls.Rows[irow].Cells[4].Text).Trim() != "&nbsp;")
                     //{
                     //    dpaidcomp = Convert.ToDecimal(gvexpensdtls.Rows[irow].Cells[4].Text);
                     //}
                     //if (Convert.ToString(gvexpensdtls.Rows[irow].Cells[5].Text).Trim() != "" && Convert.ToString(gvexpensdtls.Rows[irow].Cells[5].Text).Trim() != "&nbsp;")
                     //{
                     //    dpaidemp = Convert.ToDecimal(gvexpensdtls.Rows[irow].Cells[5].Text);
                     //}

                     //if (dpaidemp == dpaidcomp)
                     //{
                     //    if (dpaidemp == 0 && dpaidcomp == 0)
                     //    {
                     //        //Convert.ToString(gvexpensdtls.Rows[irow].Cells[5].Text).Trim() 
                     //        lblmessage.Text = "Please enter " + Convert.ToString(gvexpensdtls.Rows[irow].Cells[3].Text).Trim() + " expenses.";
                     //        return;
                     //    }
                     //}
                     #endregion
                     if (Convert.ToString(gvexpensdtls.Rows[irow].Cells[8].Text).Trim() != "" && Convert.ToString(gvexpensdtls.Rows[irow].Cells[8].Text).Trim() != "&nbsp;")
                     {
                         ddiffyamt = Convert.ToDecimal(gvexpensdtls.Rows[irow].Cells[8].Text);
                     }


                     if (ddiffyamt == 0)
                     {
                         #region if not Accommodation
                         if (Convert.ToString(gvexpensdtls.Rows[irow].Cells[3].Text).Trim() != "Accommodation")
                         {
                             lblmessage.Text = "Please enter " + Convert.ToString(gvexpensdtls.Rows[irow].Cells[3].Text).Trim() + " expenses.";
                             return;
                         }
                         #endregion

                         #region if Only Accommodation
                         if (Convert.ToString(gvexpensdtls.Rows[irow].Cells[3].Text).Trim() == "Accommodation")
                         {
                             if (Convert.ToString(gvexpensdtls.Rows[irow].Cells[8].Text).Trim() == "" || Convert.ToString(gvexpensdtls.Rows[irow].Cells[8].Text).Trim() == "&nbsp;")
                             {
                                 if (Convert.ToString(gvexpensdtls.Rows[irow].Cells[8].Text).Trim() != "0" )
                                 {
                                     lblmessage.Text = "Please enter " + Convert.ToString(gvexpensdtls.Rows[irow].Cells[3].Text).Trim() + " expenses.";
                                     return;
                                 }
                             }
                         }
                         #endregion
                     }

                 }
             }
         }
          */
        #endregion


        if (ploadexpfile.HasFiles)
        {
            //lblmessage.Text = "Please Enter reason For Travel";
            //return;
        }
        else
        {
            if (gvuploadedFiles.Rows.Count <= 0)
            {
                lblmessage.Text = "Please Upload Expenses Files.";
                return;
            }
        }


        //if (Convert.ToString(Session["chkbtnStatus"]).Trim() != "")
        //{
        //    Response.Redirect("~/procs/travelindex.aspx");
        //}
        //Session["chkbtnStatus"] = "Travel Expenses Request button Event is Submitted";

        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "Confirm();", true);
        //ClientScript.RegisterStartupScript(this.GetType(),"alert","Confirm();",true);

        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            //lblmessage.Text = "Not Saved";
            return;
        }

        String[] stremp;
        stremp = Convert.ToString(ddl_ProjectName.SelectedItem.Text).Split('/');

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
        int status = 1;

        decimal Daily_Halting_allowance = 0, TotalAmount_Claimed = 0, LessAdvanceTaken = 0, Net_Pay_Company = 0, Net_Pay_Employee = 0;
        string Reason_Deviation = "", Upload_File = "";

        if (Convert.ToString(txtdailyhaltingallowance.Text).Trim() != "")
            Daily_Halting_allowance = Convert.ToDecimal(txtdailyhaltingallowance.Text);

        if (Convert.ToString(txtTotAmtClaimed.Text).Trim() != "")
            TotalAmount_Claimed = Convert.ToDecimal(txtTotAmtClaimed.Text);

        if (Convert.ToString(txtLessAdvTaken.Text).Trim() != "")
            LessAdvanceTaken = Convert.ToDecimal(txtLessAdvTaken.Text);

        if (Convert.ToString(txtnetPaybltoComp.Text).Trim() != "")
            Net_Pay_Company = Convert.ToDecimal(txtnetPaybltoComp.Text);

        if (Convert.ToString(txtnetPaybltoEmp.Text).Trim() != "")
            Net_Pay_Employee = Convert.ToDecimal(txtnetPaybltoEmp.Text);

        if (Convert.ToString(txtReasonDeviation.Text).Trim() != "")
            Reason_Deviation = Convert.ToString(txtReasonDeviation.Text).Trim();


        if (Convert.ToString(ploadexpfile.FileName).Trim() != "")
            Upload_File = Convert.ToString(ploadexpfile.FileName).Trim();

        SqlParameter[] spars = new SqlParameter[23];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "Insert";

        spars[1] = new SqlParameter("@trip_type", SqlDbType.Int);
        if (Convert.ToString(lstTripType.SelectedValue).Trim() != "")
            spars[1].Value = Convert.ToInt32(lstTripType.SelectedValue);
        //spars[1].Value = Convert.ToInt32(hdnTraveltypeid.Value);

        spars[2] = new SqlParameter("@trip_frm_date", SqlDbType.VarChar);
        spars[2].Value = strfromDate;

        spars[3] = new SqlParameter("@trip_to_date", SqlDbType.VarChar);
        spars[3].Value = strToDate;

        spars[4] = new SqlParameter("@exp_id", SqlDbType.Decimal);
        if (Convert.ToString(hdnexp_id.Value).Trim() != "0")
            spars[4].Value = hdnexp_id.Value;
        else
            spars[4].Value = DBNull.Value;

        spars[5] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[5].Value = Convert.ToString(txtEmpCode.Text).Trim();

        spars[6] = new SqlParameter("@trip_status", SqlDbType.Int);
        spars[6].Value = status;

        spars[7] = new SqlParameter("@req_adv_amt", SqlDbType.Decimal);
        if (Convert.ToString(txtAdvance.Text).Trim() != "")
            spars[7].Value = Convert.ToDecimal(txtAdvance.Text);
        else
            spars[7].Value = DBNull.Value;

        spars[8] = new SqlParameter("@currency_type", SqlDbType.VarChar);
        spars[8].Value = reqcurrency;

        spars[9] = new SqlParameter("@Daily_Halting_allowance", SqlDbType.Decimal);
        spars[9].Value = Daily_Halting_allowance;

        spars[10] = new SqlParameter("@TotalAmount_Claimed", SqlDbType.Decimal);
        spars[10].Value = TotalAmount_Claimed;

        spars[11] = new SqlParameter("@LessAdvanceTaken", SqlDbType.Decimal);
        spars[11].Value = LessAdvanceTaken;

        spars[12] = new SqlParameter("@Net_Pay_Company", SqlDbType.Decimal);
        spars[12].Value = Net_Pay_Company;

        spars[13] = new SqlParameter("@Net_Pay_Employee", SqlDbType.Decimal);
        spars[13].Value = Net_Pay_Employee;

        spars[14] = new SqlParameter("@Reason_Deviation", SqlDbType.VarChar);
        spars[14].Value = Reason_Deviation;

        spars[15] = new SqlParameter("@Upload_File", SqlDbType.VarChar);
        spars[15].Value = Upload_File;

        spars[16] = new SqlParameter("@trip_id", SqlDbType.Decimal);
        spars[16].Value = hdnTripid.Value;

        spars[17] = new SqlParameter("@trp_reason", SqlDbType.VarChar);
        spars[17].Value = Convert.ToString(txtReason.Text).Trim();

        spars[18] = new SqlParameter("@conditionid", SqlDbType.Decimal);
        spars[18].Value = hdnTravelConditionid.Value;

        spars[19] = new SqlParameter("@ProjectName", SqlDbType.VarChar);
        spars[19].Value = Convert.ToString(stremp[1].Trim());

        spars[20] = new SqlParameter("@DeptName", SqlDbType.VarChar);
        spars[20].Value = Convert.ToString(ddl_DeptName.SelectedValue);

        spars[21] = new SqlParameter("@IsNewjoinee", SqlDbType.Bit);
        if (chkIsNewJoinExp_req.Checked)
            spars[21].Value = 1;
        else
            spars[21].Value = 0;
        spars[22] = new SqlParameter("@IsDraft", SqlDbType.Bit);
        spars[22].Value = 0;

        DataTable dtMaxTripID = spm.InsertorUpdateData(spars, "SP_SAVE_Exp_TRAVELREQUEST_Main");



        //DataTable dtMaxTripID = spm.InsertExpenseTravelRequest_main(Convert.ToInt32(hdnTraveltypeid.Value), strfromDate, strToDate,Convert.ToString(txtReason.Text).Trim(), Convert.ToString(txtEmpCode.Text).Trim(), status, Convert.ToInt32(txtAdvance.Text), reqcurrency,Daily_Halting_allowance, TotalAmount_Claimed, LessAdvanceTaken, Net_Pay_Company, Net_Pay_Employee, Reason_Deviation, Upload_File);
        Session["TripTypeId"] = null;
        Session["TravelType"] = null;
        decimal maxtripid = 0; //= Convert.ToInt32(dtMaxTripID.Rows[0]["maxtripid"]);
        if (dtMaxTripID.Rows.Count > 0)
        {
            maxtripid = Convert.ToInt32(dtMaxTripID.Rows[0]["maxtripid"]);
            hdnvouno.Value = Convert.ToString(dtMaxTripID.Rows[0]["vouno"]);
        }
        if (maxtripid == 0)
        {
            return;
        }
        spm.InsertExpenseApproverDetails(hflapprcode.Value, Convert.ToInt32(hdnApprId.Value), maxtripid);
        //UploadFiles(maxtripid);

        #region insert or upload multiple files
        if (ploadexpfile.HasFile)
        {
            string filename = ploadexpfile.FileName;
            //uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["FuelClaimsDocumentpath"]).Trim()), uploadfile.FileName));
            if (Convert.ToString(filename).Trim() != "")
            {
                #region date formatting
                if (Convert.ToString(txtFromdate.Text).Trim() != "")
                {
                    strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                    strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
                }
                #endregion


                string FuelclaimPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["trvlingExpfiles"]).Trim() + "/");
                bool folderExists = Directory.Exists(FuelclaimPath);
                if (!folderExists)
                {
                    Directory.CreateDirectory(FuelclaimPath);
                }


                ////string[] Files = Directory.GetFiles(FuelclaimPath);
                ////foreach (string file in Files)
                ////{
                ////    File.Delete(file);
                ////}
                Int32 t_cnt = spm.getTravelUploaded_Files(maxtripid);

                Boolean blnfile = true;
                HttpFileCollection fileCollection = Request.Files;
                for (int i = 0; i < fileCollection.Count; i++)
                {
                    string strfileName = "";

                    HttpPostedFile uploadfileName = fileCollection[i];
                    string fileName = ReplaceInvalidChars(Path.GetFileName(uploadfileName.FileName));
                    if (uploadfileName.ContentLength > 0)
                    {
                        // strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + Convert.ToString(i + 1).Trim() + "_" + uploadfileName.FileName;
                        String InputFile = System.IO.Path.GetExtension(uploadfileName.FileName);
                        //strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + Convert.ToString(t_cnt + 1).Trim() + "_FuelClaim" + (t_cnt + 1).ToString() + InputFile;
                        strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + fileName;
                        filename = strfileName;
                        //uploadfileName.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["FuelClaimsDocumentpath"]).Trim()), strfileName));
                        uploadfileName.SaveAs(Path.Combine(FuelclaimPath, strfileName));


                        //spm.InsertFuelUploaded_Files(maxRemid, blnfile,Convert.ToString( Convert.ToString(maxRemid) + "/" +  strfileName).Trim(),"FuelClaim",i+1);
                        spm.InsertFuelUploaded_Files(maxtripid, blnfile, Convert.ToString(strfileName).Trim(), "expenses", t_cnt + 1);
                        //spm.getFuelUploaded_Files(maxtripid);
                        //blnfile = true;
                        t_cnt = t_cnt + 1;
                    }
                }

            }


        }
        getExpenseUploadedFiles();
        #endregion
        lblmessage.Visible = true;
        lblmessage.Text = "Travel Expense Submitted Successfully";

        //getFomatated dates for Email
        getFromdateTodate_FroEmail();

        String strTrvlrestURL = "";
        strTrvlrestURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_TE"]).Trim() + "?expid=" + maxtripid + "&stype=APP";

        #region SaveTravel Expenses Code

        spm.send_mailto_RM_ExpensesApprover(txtEmpName.Text, hflEmailAddress.Value, hdnApprEmailaddress.Value, "Request for Travel Expenses - " + hdnvouno.Value.ToString(), txtTriptype.Text, hdnActualTrvlDays.Value, txtReason.Text, Convert.ToString(hdnfrmdate_emial.Value).Trim(), Convert.ToString(hdntodate_emial.Value).Trim(), "", GetApprove_RejectList(maxtripid), GetIntermediatesList_Formail(), strTrvlrestURL);


        string strapprovermails = "";
        strapprovermails = get_approverlist_ifTD_COS(maxtripid);
        //if (Convert.ToString(strapprovermails).Trim()!="")
        //spm.Expense_send_mailto_Intermediate(hflEmailAddress.Value, strapprovermails, "Request for Travel Expenses", txtTriptype.Text, hdnActualTrvlDays.Value, txtReason.Text, Convert.ToString(hdnfrmdate_emial.Value).Trim(), Convert.ToString(hdntodate_emial.Value).Trim(), GetApprove_RejectList(maxtripid), txtEmpName.Text, GetIntermediatesList());


        Response.Redirect("~/procs/travelindex.aspx");
        ClearControls();
        #endregion
    }

    public string ReplaceInvalidChars(string filename)
    {
        Regex illegalInFileName = new Regex(@"[#%&{}\!/<'>@+*?$|=]");
        string myString = illegalInFileName.Replace(filename, "_");
        //return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        return myString;
    }

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        if (FromDateValidation_exp() == true)
        {
            return;
        }
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


                // hdnmsg.Value = lblmessage.Text;
                return;
            }
        }
    }

    protected void btnTra_Details_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        if (Convert.ToString(lstTripType.SelectedValue).Trim() == "" || Convert.ToString(lstTripType.SelectedValue).Trim() == "0")
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
        txtRemark.Text = "";
        txtFoodAllowance.Text = "";
        txtfare.Text = "";
        txtFoodEligibilty.Text = "";
        txtToTime.Text = "";
        // txtDestination.Text = "";
        DDLDestination.SelectedValue = "0";
        txtToDate_Trvl.Text = "";
        txtFromTime.Text = "";
        DDLOrigin.SelectedValue = "0";
        // txtOrigin.Text = "";
        txtFromdate_Trvl.Text = "";
        txtDeviation.Text = "";
        txtTravelMode.Text = "";
        txtEmpCode_Trvl.Text = "";
        txtTravelType.Text = "";
        txtTripId.Text = "";

        AssigningSessions();

        txtEmpCode_Trvl.Text = Convert.ToString(Session["ReqEmpCode"]);
        txtTravelType.Text = Convert.ToString(Session["TravelType"]);
        //   hdnTripid.Value = Convert.ToString(Session["TripID"]);
        hdnfromdate_Trvl.Value = Convert.ToString(Session["Fromdate"]);
        hdnTodate_Trvl.Value = Convert.ToString(Session["Todate"]);
        hdnTryiptypeid.Value = Convert.ToString(Session["TripTypeId"]);
        hflGrade.Value = Convert.ToString(Session["Grade"]);

        if (Convert.ToString(hdnTryiptypeid.Value).Trim() == "2")
            GetTravelMode();
        else
            GetTravelMode_old();

        GetCities();
        trvldeatils_delete_btn.Visible = false;
        // lstTravelType.SelectedValue = Convert.ToString(Session["TripTypeId"]).Trim();
        trvldeatils_btnSave.Text = "Submit";

        if (Convert.ToString(hdntrdetailsid.Value).Trim() != "0" && Convert.ToString(hdnDesk.Value).Trim() == "Yes")
        {
            if (Convert.ToString(hdnTripid.Value).Trim() != "0" && Convert.ToString(hdnTripid.Value).Trim() != "")
            {
                Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                txtTravelType.BackColor = color;
                trvldeatils_delete_btn.Visible = false;
                txtFromdate_Trvl.Enabled = false;
                txtToDate_Trvl.Enabled = false;
                // txtDestination.Enabled = false;
                // txtOrigin.Enabled = false;
                DDLDestination.Enabled = false;
                DDLOrigin.Enabled = false;
                //txtRemark.Enabled = false;
                txtTravelMode.Enabled = false;
                txtTravelMode.BackColor = color;
            }
        }
        //Response.Redirect("~/procs/ExpenseTravelDetails.aspx?tripid=" + hdnTripid.Value + "&trdetailsid=0&expid=" + hdnexp_id.Value);
    }


    protected void trvl_accmo_btn_Click(object sender, EventArgs e)
    {

        lblmessage.Text = "";

        if (Convert.ToString(lstTripType.SelectedValue).Trim() == "" || Convert.ToString(lstTripType.SelectedValue).Trim() == "0")
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

        txtTripId_Accm.Text = "";
        txtTravelType_Accm.Text = "";
        txtEmpCode_Accm.Text = "";
        txtAcctype.Text = "Hotel";
        rdoAccomodation.Checked = true;
        rdoOwnArgmnet.Checked = false;
        rdoFood.Checked = false;
        rdoFoodAccomodation.Checked = false;
        // txtLocation_Accm.Text="";
        DDL_Location_Accm.SelectedValue = "0";
        DDL_Location_Accm.SelectedValue = "0";
        txtFromdate_Accm.Text = "";
        txtToDate_Accm.Text = "";
        txtnoofDays.Text = "";
        txtPaidBy_Accm.Text = "";
        txtCharges.Text = "";
        txtDeviation_Accm.Text = "";
        txtEligibility_Accm.Text = "";
        txtAdditionalFoodExp_emp.Text = "";
        txtAdditionalFoodExp_exp.Text = "";
        txtFlatDev_Accm.Text = "";
        txtFlatChg_Accm.Text = "";
        txtFlatElg_Accm.Text = "";
        txtAdditional_exp_deviation_Accm.Text = "";
        txtaddintionalExpens_Accm.Text = "";
        txtFlatPaid_Accm.Text = "";
        txtRemarks_Accm.Text = "";
        lstTravelMode.SelectedValue = null;

        if (DivAccm.Visible)
        {
            DivAccm.Visible = false;
            trvl_accmo_btn.Text = "+";
        }
        else
        {
            DivAccm.Visible = true;
            trvl_accmo_btn.Text = "-";
            hdnAccdtlsid.Value = "";
            accmo_btnSave.Text = "Submit";
        }
        AssigningSessions();
        accmo_delete_btn.Visible = false;
        lblmessage.Text = "";
        GetCities();
        txtEmpCode_Accm.Text = Convert.ToString(Session["ReqEmpCode"]);
        txtTravelType_Accm.Text = Convert.ToString(Session["TravelType"]);
        //hdnTripid.Value = Convert.ToString(Session["TripID"]);
        hdnTryiptypeid.Value = Convert.ToString(Session["TripTypeId"]);
        hdnfromdate_Accm.Value = Convert.ToString(Session["Fromdate"]);
        hdnTodate_Accm.Value = Convert.ToString(Session["Todate"]);
        GetPaidBy_Accm();
        if (Convert.ToString(hdnAccdtlsid.Value).Trim() != "0" && Convert.ToString(hdnIsThrughCOS.Value).Trim() == "Yes")
        {
            if (Convert.ToString(hdnTripid.Value).Trim() != "0" && Convert.ToString(hdnTripid.Value).Trim() != "")
            {
                Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                txtTravelType_Accm.BackColor = color;
                accmo_delete_btn.Visible = false;
                txtFromdate_Accm.Enabled = false;
                txtToDate_Accm.Enabled = false;
                // txtLocation_Accm.Enabled = false;
                DDL_Location_Accm.Enabled = false;
            }
        }
        get_Actual_DayDiff();
        setControlVisibility();
        //Response.Redirect("~/procs/ExpenseAccomodation.aspx?tripid="+hdnTripid.Value+"&accid=0&expid="+hdnexp_id.Value);
    }
    protected void trvl_localbtn_Click(object sender, EventArgs e)
    {

        if (Convert.ToString(lstTripType.SelectedValue).Trim() == "" || Convert.ToString(lstTripType.SelectedValue).Trim() == "0")
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

        txtTripId_Locl.Text = "";
        txtTravelType_Locl.Text = "";
        txtEmpCode_Locl.Text = "";
        txtFromdate_Locl.Text = "";
        txtToDate_Locl.Text = "";
        //txtLocation_Locl.Text="";
        DDL_Location_Locl.SelectedValue = "0";
        txtTravelMode_Locl.Text = "";
        txtDeviation_Locl.Text = "";
        txtCharges_Locl.Text = "";
        txtRemarks_Locl.Text = "";

        if (Div_Locl.Visible)
        {
            Div_Locl.Visible = false;
            trvl_localbtn.Text = "+";
        }
        else
        {
            Div_Locl.Visible = true;
            trvl_localbtn.Text = "-";
            hdntrdetailsid_Locl.Value = "0";
            localtrvl_btnSave.Text = "Submit";
        }
        AssigningSessions();
        txtEmpCode_Locl.Text = Convert.ToString(Session["ReqEmpCode"]);
        txtTravelType_Locl.Text = Convert.ToString(Session["TravelType"]);
        hdnTripid.Value = Convert.ToString(Session["TripID"]);
        hdnfromdate_Locl.Value = Convert.ToString(Session["Fromdate"]);
        hdnTodate_Locl.Value = Convert.ToString(Session["Todate"]);
        hdnTryiptypeid.Value = Convert.ToString(Session["Tr_type_id"]);
        hflGrade_Locl.Value = Convert.ToString(Session["Grade"]);
        GetTravelMode_locl();
        // GetCities();
        txtFromdate_Locl.Focus();
        localtrvl_delete_btn.Visible = false;

        if (Convert.ToString(hdntrdetailsid_Locl.Value).Trim() != "0" && Convert.ToString(hdnDesk_Locl.Value).Trim() == "Yes")
        {
            if (Convert.ToString(hdnTripid.Value).Trim() != "0" && Convert.ToString(hdnTripid.Value).Trim() != "")
            {
                Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                txtTravelType_Locl.BackColor = color;
                localtrvl_delete_btn.Visible = false;
                txtFromdate_Locl.Enabled = false;
                txtToDate_Locl.Enabled = false;
                // txtLocation_Locl.Enabled = false;
                DDL_Location_Locl.Enabled = false;
            }
        }

        //Response.Redirect("~/procs/ExpenseLocalTravel.aspx?tripid=" + hdnTripid.Value + "&Localid=0&expid=" + hdnexp_id.Value);
    }

    protected void lnkLocalTravleEdit_Click(object sender, EventArgs e)
    {
        txtTripId_Locl.Text = "";
        txtTravelType_Locl.Text = "";
        txtEmpCode_Locl.Text = "";
        txtFromdate_Locl.Text = "";
        txtToDate_Locl.Text = "";
        //  txtLocation_Locl.Text = "";
        DDL_Location_Locl.SelectedValue = "0";
        txtTravelMode_Locl.Text = "";
        txtDeviation_Locl.Text = "";
        txtCharges_Locl.Text = "";
        txtRemarks_Locl.Text = "";

        AssigningSessions();
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnLocalId.Value = Convert.ToString(dgLocalTravel.DataKeys[row.RowIndex].Values[1]).Trim();
        if (Div_Locl.Visible == false)
        {
            Div_Locl.Visible = true;
            trvl_localbtn.Text = "-";
            localtrvl_btnSave.Text = "Update";
        }
        txtEmpCode_Locl.Text = Convert.ToString(Session["ReqEmpCode"]);
        txtTravelType_Locl.Text = Convert.ToString(Session["TravelType"]);
        //hdnTripid.Value = Convert.ToString(Session["TripID"]);
        hdnfromdate_Locl.Value = Convert.ToString(Session["Fromdate"]);
        hdnTodate_Locl.Value = Convert.ToString(Session["Todate"]);
        hdnTryiptypeid.Value = Convert.ToString(Session["Tr_type_id"]);
        hflGrade_Locl.Value = Convert.ToString(Session["Grade"]);
        GetTravelMode_locl();
        // GetCities();

        //hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();
        hdntrdetailsid_Locl.Value = hdnLocalId.Value;
        //hdnexp_id.Value = Convert.ToString(Request.QueryString[2]).Trim();
        localtrvl_delete_btn.Visible = false;
        getLocaTravelDetails_edit_Locl();

        if (Convert.ToString(hdntrdetailsid_Locl.Value).Trim() != "0" && Convert.ToString(hdnDesk_Locl.Value).Trim() == "Yes")
        {
            if (Convert.ToString(hdnTripid.Value).Trim() != "0" && Convert.ToString(hdnTripid.Value).Trim() != "")
            {
                Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                txtTravelType_Locl.BackColor = color;
                localtrvl_delete_btn.Visible = false;
                txtFromdate_Locl.Enabled = false;
                txtToDate_Locl.Enabled = false;
                // txtLocation_Locl.Enabled = false;
                DDL_Location_Locl.Enabled = false;
            }
        }

        //Response.Redirect("~/procs/ExpenseLocalTravel.aspx?tripid=" + hdnTripid.Value + "&Localid=" + hdnLocalId.Value +"&expid="+hdnexp_id.Value);

    }

    protected void lnkAccomodationdit_Click(object sender, EventArgs e)
    {

        ////if (Convert.ToString(hdnTripid.Value).Trim() != "")
        ////{
        AssigningSessions();
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnTripid.Value = Convert.ToString(dgAccomodation.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnAccId.Value = Convert.ToString(dgAccomodation.DataKeys[row.RowIndex].Values[1]).Trim();
        CheckBox chkisthourCOS = (CheckBox)dgAccomodation.Rows[row.RowIndex].FindControl("ChkCOS");
        if (chkisthourCOS.Checked == true)
            Session["isthrughCOS"] = "Yes";
        else
            Session["isthrughCOS"] = "No";

        accmo_delete_btn.Visible = false;
        lblmessage.Text = "";
        GetCities();
        txtEmpCode_Accm.Text = Convert.ToString(Session["ReqEmpCode"]);
        txtTravelType_Accm.Text = Convert.ToString(Session["TravelType"]);
        //hdnTripid.Value = Convert.ToString(Session["TripID"]);
        hdnTryiptypeid.Value = Convert.ToString(Session["TripTypeId"]);
        hdnfromdate_Accm.Value = Convert.ToString(Session["Fromdate"]);
        hdnTodate_Accm.Value = Convert.ToString(Session["Todate"]);
        if (DivAccm.Visible == false)
        {
            DivAccm.Visible = true;
            trvl_accmo_btn.Text = "-";
            accmo_btnSave.Text = "Update";
        }
        get_Actual_DayDiff();
        if (Convert.ToString(hdnTripid.Value).Trim() == "")
        {
            //txtLocation.Text = Convert.ToString(Session["Location"]);
        }
        else
        {
            getAccomodationDetails_Accm();
        }

        hdnAccdtlsid.Value = hdnAccId.Value;

        if (Convert.ToString(hdnAccdtlsid.Value).Trim() != "0")
        {
            getAccDetailsEdit();
            accmo_btnSave.Text = "Update";
            //accmo_delete_btn.Visible = true;
        }
        txtTravelType_Accm.Enabled = false;

        if (Convert.ToString(hdnAccdtlsid.Value).Trim() != "0" && Convert.ToString(hdnIsThrughCOS.Value).Trim() == "Yes")
        {
            if (Convert.ToString(hdnTripid.Value).Trim() != "0" && Convert.ToString(hdnTripid.Value).Trim() != "")
            {
                Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                txtTravelType_Accm.BackColor = color;
                accmo_delete_btn.Visible = false;
                txtFromdate_Accm.Enabled = false;
                txtToDate_Accm.Enabled = false;
                //  txtRemarks.Enabled = false;
                //  txtLocation_Accm.Enabled = false;
                DDL_Location_Accm.Enabled = false;

            }
        }
        get_Actual_DayDiff();
        setControlVisibility();

        //Response.Redirect("~/procs/ExpenseAccomodation.aspx?tripid=" + hdnTripid.Value + "&accid=" + hdnAccId.Value +"&expid="+hdnexp_id.Value);
        ////}
        ////else
        ////{

        ////}

    }
    protected void lnkTravelDetailsEdit_Click(object sender, EventArgs e)
    {

        if (Convert.ToString(hdnTripid.Value).Trim() != "")
        {
            AssigningSessions();
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnTripid.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[0]).Trim();
            hdnTravelDtlsId.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[1]).Trim();

            txtEmpCode_Trvl.Text = Convert.ToString(Session["ReqEmpCode"]);
            txtTravelType.Text = Convert.ToString(Session["TravelType"]);
            //   hdnTripid.Value = Convert.ToString(Session["TripID"]);
            hdnfromdate_Trvl.Value = Convert.ToString(Session["Fromdate"]);
            hdnTodate_Trvl.Value = Convert.ToString(Session["Todate"]);
            hdnTryiptypeid.Value = Convert.ToString(Session["TripTypeId"]);
            hflGrade.Value = Convert.ToString(Session["Grade"]);

            if (DivTrvl.Visible == false)
            {
                DivTrvl.Visible = true;
                btnTra_Details.Text = "-";
                trvldeatils_btnSave.Text = "Update";
            }
            if (Convert.ToString(hdnTryiptypeid.Value).Trim() == "2")
                GetTravelMode();
            else
                GetTravelMode_old();

            GetCities();
            trvldeatils_delete_btn.Visible = false;
            // lstTravelType.SelectedValue = Convert.ToString(Session["TripTypeId"]).Trim();

            //hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();
            hdntrdetailsid.Value = hdnTravelDtlsId.Value;
            //hdnexp_id.Value = Convert.ToString(Request.QueryString[2]).Trim();


            GetExpensesTravelDetails_Edit();
            if (Convert.ToString(hdntrdetailsid.Value).Trim() != "0")
            {
                trvldeatils_btnSave.Text = "Update";
            }


            if (Convert.ToString(hdntrdetailsid.Value).Trim() != "0" && Convert.ToString(hdnDesk.Value).Trim() == "Yes")
            {
                if (Convert.ToString(hdnTripid.Value).Trim() != "0" && Convert.ToString(hdnTripid.Value).Trim() != "")
                {
                    Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                    txtTravelType.BackColor = color;
                    trvldeatils_delete_btn.Visible = false;
                    txtFromdate_Trvl.Enabled = false;
                    txtToDate_Trvl.Enabled = false;
                    // txtDestination.Enabled = false;
                    // txtOrigin.Enabled = false;
                    DDLDestination.Enabled = false;
                    DDLOrigin.Enabled = false;

                    //txtRemark.Enabled = false;
                    txtTravelMode.Enabled = false;
                    txtTravelMode.BackColor = color;
                }
            }
            Session["trvlS"] = "0";
            //Response.Redirect("~/procs/ExpenseTravelDetails.aspx?tripid=" + hdnTripid.Value + "&trdetailsid=" + hdnTravelDtlsId.Value+"&expid="+ hdnexp_id.Value);
        }
        else
        {
            //   Session["TripID"] = hdnTripid.Value;

            //Response.Redirect("~/procs/ExpenseTravelDetails.aspx");
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
            lstTravelMode.Items.Insert(0, new ListItem("Select Travel Mode", "0"));
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
            lstTravelMode.Items.Insert(0, new ListItem("Select Travel Mode", "0"));
        }
    }

    public void GetExpensesTravelDetails_Edit()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetExpensesTravelDetails(txtEmpCode.Text, Convert.ToDecimal(hdntrdetailsid.Value));
        txtFoodEligibilty.Text = "0.00";
        txtFoodAllowance.Text = "";
        if (dtTrDetails.Rows.Count > 0)
        {
            hdnTryiptypeid.Value = Convert.ToString(dtTrDetails.Rows[0]["trip_type_id"]);
            txtTravelType.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_description"]);
            txtTravelMode.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_mode"]);
            lstTravelMode.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["trip_mode_id"]);
            txtFromTime.Text = Convert.ToString(dtTrDetails.Rows[0]["departure_time"]);
            txtToTime.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_time"]);

            txtDeviation.Text = Convert.ToString(dtTrDetails.Rows[0]["deviation"]);
            hdnDeviation.Value = Convert.ToString(dtTrDetails.Rows[0]["deviation"]);
            txtFromdate_Trvl.Text = Convert.ToString(dtTrDetails.Rows[0]["departure_date"]);
            txtToDate_Trvl.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_date"]);
            //  txtToDate.Text = Convert.ToString(dtTrDetails.Rows[0]["Actual_Fare"]);

            txtfare.Text = Convert.ToString(dtTrDetails.Rows[0]["Actual_Fare"]);
            txtRemark.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_requirements"]);

            // txtTravelType.Text = Convert.ToString(Session["TravelType"]);
            hdnDesk.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_through_desk"]);
            DDLOrigin.SelectedItem.Text = Convert.ToString(dtTrDetails.Rows[0]["departure_place"]);
            DDLDestination.SelectedItem.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_place"]);

            //txtOrigin.Text = Convert.ToString(dtTrDetails.Rows[0]["departure_place"]);
            //txtDestination.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_place"]);

            hdnTrvlBookdStatus.Value = Convert.ToString(dtTrDetails.Rows[0]["trvl_status"]);


            // txtFoodEligibilty.Text = Convert.ToString(dtTrDetails.Rows[0]["Eligibility"]);
            get_Trvl_FoodEligibilty();
            if (Convert.ToString(dtTrDetails.Rows[0]["efoodallowance"]).Trim() != "")
                txtFoodAllowance.Text = Convert.ToString(dtTrDetails.Rows[0]["efoodallowance"]);

            //hdncomp_code.Value = Convert.ToString(dtTrDetails.Rows[0]["comp_code"]);
            //hdndept_id.Value = Convert.ToString(dtTrDetails.Rows[0]["dept_id"]);

            if (Convert.ToString(dtTrDetails.Rows[0]["trvl_status"]) == "Booked")
            {
                chkCOS_Trvl.Checked = true;
                ////txtfare.Enabled = false;
                ////txtFromTime.Enabled = false;
                ////txtToTime.Enabled = false;
                //trvldeatils_btnSave.Visible = false;
            }
            else
            {
                chkCOS_Trvl.Checked = false;
            }
        }
    }

    public void GetCities()
    {
        DataTable dtCities = new DataTable();
        dtCities = spm.getCitiesDetails();
        if (dtCities.Rows.Count > 0)
        {
            //lstOrigin.DataSource = dtCities;
            //lstOrigin.DataTextField = "CITYNAME"; DDLOrigin
            //lstOrigin.DataValueField = "CITYID";
            //lstOrigin.DataBind();

            //lstDestination.DataSource = dtCities;
            //lstDestination.DataTextField = "CITYNAME";
            //lstDestination.DataValueField = "CITYID";
            //lstDestination.DataBind();

            //lstLocation_Accm.DataSource = dtCities;
            //lstLocation_Accm.DataTextField = "CITYNAME";
            //lstLocation_Accm.DataValueField = "CITYID";
            //lstLocation_Accm.DataBind();
        }
    }

    public void GetPalceJoin()
    {
        DataTable dtCities = new DataTable();
        dtCities = spm.getCitiesDetails();
        if (dtCities.Rows.Count > 0)
        {
            DDLOrigin.DataSource = dtCities;
            DDLOrigin.DataTextField = "CITYNAME";
            DDLOrigin.DataValueField = "CITYID";
            DDLOrigin.DataBind();
            DDLOrigin.Items.Insert(0, new ListItem("Select  Place", "0"));

            DDLDestination.DataSource = dtCities;
            DDLDestination.DataTextField = "CITYNAME";
            DDLDestination.DataValueField = "CITYID";
            DDLDestination.DataBind();
            DDLDestination.Items.Insert(0, new ListItem("Select  Place", "0"));

            DDL_Location_Accm.DataSource = dtCities;
            DDL_Location_Accm.DataTextField = "CITYNAME";
            DDL_Location_Accm.DataValueField = "CITYID";
            DDL_Location_Accm.DataBind();
            DDL_Location_Accm.Items.Insert(0, new ListItem("Select  Location", "0"));

            DDL_Location_Locl.DataSource = dtCities;
            DDL_Location_Locl.DataTextField = "CITYNAME";
            DDL_Location_Locl.DataValueField = "CITYID";
            DDL_Location_Locl.DataBind();
            DDL_Location_Locl.Items.Insert(0, new ListItem("Select  Location", "0"));

        }
    }
    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        if (FromDateValidation_exp() == true)
        {
            return;
        }

    }

    protected void btnMod_Click(object sender, EventArgs e)
    {

        string[] strdate;
        string strfromDate = "";
        string strToDate = "";
        #region Check For Blank Fields
        lblmessage.Text = "";
        if (Convert.ToString(lstTripType.SelectedValue).Trim() == "" || Convert.ToString(lstTripType.SelectedValue).Trim() == "0")
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

        if (Convert.ToString(Session["chkbtnStatus"]).Trim() != "")
        {
            Response.Redirect("~/procs/ManageTravel_expense.aspx");
        }
        Session["chkbtnStatus"] = "Travel Expenses Request button Event is Submitted";

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

        StringBuilder sbAccomodation = new StringBuilder();
        StringBuilder sbTravelRequest = new StringBuilder();
        StringBuilder sblocaltravel = new StringBuilder();
        StringBuilder sbexpdtls = new StringBuilder();

        sbTravelRequest.Append("Insert into tbl_Expensetravel_request_dtls( exp_id,trip_id,trip_mode_id,departure_date,departure_place,arrival_date,arrival_place,travel_through_desk,deviation)  values ");
        for (int i = 0; i < dgTravelRequest.Rows.Count; i++)
        {
            #region date formatting

            if (Convert.ToString(dgTravelRequest.Rows[i].Cells[1]).Trim() != "")
            {
                strdate = Convert.ToString(dgTravelRequest.Rows[i].Cells[1]).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            if (Convert.ToString(dgTravelRequest.Rows[i].Cells[4]).Trim() != "")
            {
                strdate = Convert.ToString(dgTravelRequest.Rows[i].Cells[4]).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            #endregion

            //Convert.ToString(dgAccomodation.DataKeys[row.RowIndex].Values[1]).Trim();
            CheckBox chktrvl = (CheckBox)dgTravelRequest.Rows[i].FindControl("ChkCOS");
            sbTravelRequest.Append("(1 " + hdnTripid.Value + "," + hdnTraveltypeid.Value + ",'" + Convert.ToString(dgTravelRequest.Rows[i].Cells[0]).Trim() + "','" + strfromDate + "','" + Convert.ToString(dgTravelRequest.Rows[i].Cells[2]).Trim() + "','" + strToDate + "','" + Convert.ToString(dgTravelRequest.Rows[i].Cells[5]).Trim() + "',");
            if (chktrvl.Checked == true)
                sbTravelRequest.Append("'Yes',");
            else
                sbTravelRequest.Append("'No',");

            sbTravelRequest.Append("'" + Convert.ToString(dgTravelRequest.Rows[i].Cells[6]).Trim() + "',");

            sbTravelRequest.Append("); ");



        }

        lblmessage.Text = Convert.ToString(sbTravelRequest).Trim();
    }


    protected void gvexpensdtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton lnkAddExpense = e.Row.FindControl("lnkAddExpense") as ImageButton;
            Color clrdisable = ColorTranslator.FromHtml("#ebebe4");

            Color clrTotRow = ColorTranslator.FromHtml("#656b67");
            Color clrForeCTotRow = ColorTranslator.FromHtml("#febf39");


            lnkAddExpense.Enabled = true;
            string strtrpdtlsid = "";
            strtrpdtlsid = Convert.ToString(gvexpensdtls.DataKeys[e.Row.RowIndex][3]).Trim();

            if (Convert.ToString(strtrpdtlsid).Trim() == "7")
            {
                //lnkAddExpense.Enabled = false;
                lnkAddExpense.Visible = false;
                e.Row.BackColor = clrdisable;
            }
            if (Convert.ToString(strtrpdtlsid).Trim() == "8")
            {
                e.Row.BackColor = clrdisable;
                lnkAddExpense.Visible = false;
            }
            if (Convert.ToString(strtrpdtlsid).Trim() == "9")
            {
                e.Row.BackColor = clrdisable;
                lnkAddExpense.Visible = false;
            }
            strtrpdtlsid = "";
            strtrpdtlsid = Convert.ToString(gvexpensdtls.DataKeys[e.Row.RowIndex][3]).Trim();
            if (Convert.ToString(strtrpdtlsid) == "")
            {
                e.Row.BackColor = clrTotRow;
                //e.Row.BackColor = clrdisable;
                e.Row.ForeColor = clrForeCTotRow;
                lnkAddExpense.Visible = false;
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //if (Convert.ToString(Session["chkbtnStatus"]).Trim() != "")
        //{
        //    Response.Redirect("~/procs/ManageTravel_expense.aspx");
        //}
        //Session["chkbtnStatus"] = "Travel Expenses Request button Event is Submitted";

        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }

        hdnEligible.Value = "Cancellation";

        getFromdateTodate_FroEmail();
        string strapprovermails = "";
        //strapprovermails = GetApprove_RejectList(Convert.ToDecimal(hdnexp_id.Value));
        strapprovermails = getCancellationmailList();
        spm.RrejectExpensesTravelrequest(Convert.ToInt32(hdnexp_id.Value), txtEmpCode.Text, 0, "", "Canceltravelrequest");
        spm.ExpenseTravel_send_mailto_Cancel_Intermediate(hflEmailAddress.Value, strapprovermails, " Cancellation of Travel Expenses - " + hdnvouno.Value.ToString(), txtTriptype.Text, hdnActualTrvlDays.Value, txtReason.Text, Convert.ToString(hdnfrmdate_emial.Value).Trim(), Convert.ToString(hdntodate_emial.Value).Trim(), Convert.ToString(GetApprove_RejectList(Convert.ToInt32(hdnTripid.Value))).Trim(), txtEmpName.Text, GetIntermediatesList());
        lblmessage.Text = "Travel Cancelation Done and Notification has been send to your Reporting Manager";
        Response.Redirect("~/procs/ManageTravel_expense.aspx");
    }

    protected void dgTravelRequest_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        //{
        //    if (Convert.ToString(hdnTripid.Value).Trim() != "0" && Convert.ToString(hdnTripid.Value).Trim() != "")
        //    {
        //        e.Row.Cells[5].Visible = true;
        //        e.Row.Cells[6].Visible = true;
        //    }
        //    else
        //    {
        //        e.Row.Cells[5].Visible = false;
        //        e.Row.Cells[6].Visible = false;
        //    }

        //    if (Convert.ToString(hdnmainexpStatus.Value).Trim() == "2" || Convert.ToString(hdnmainexpStatus.Value).Trim() == "3" || Convert.ToString(hdnmainexpStatus.Value).Trim() == "4" || Convert.ToString(hdn_apprStatus.Value).Trim() == "Approved")
        //    {
        //        e.Row.Cells[8].Visible = false;
        //    }
        //}

    }
    protected void dgAccomodation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        //{
        //    if (Convert.ToString(hdnTripid.Value).Trim() != "0" && Convert.ToString(hdnTripid.Value).Trim() != "")
        //    {
        //        e.Row.Cells[3].Visible = true;
        //        e.Row.Cells[4].Visible = true;
        //    }
        //    else
        //    {
        //        e.Row.Cells[3].Visible = false;
        //        e.Row.Cells[4].Visible = false;
        //    }

        //    if (Convert.ToString(hdnmainexpStatus.Value).Trim() == "2" || Convert.ToString(hdnmainexpStatus.Value).Trim() == "3" || Convert.ToString(hdnmainexpStatus.Value).Trim() == "4" || Convert.ToString(hdn_apprStatus.Value).Trim() == "Approved")
        //    {
        //        e.Row.Cells[6].Visible = false;
        //    }

        //}
    }
    protected void dgLocalTravel_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        //{
        //    if (Convert.ToString(hdnTripid.Value).Trim() != "0" && Convert.ToString(hdnTripid.Value).Trim() != "")
        //    {
        //        e.Row.Cells[3].Visible = true;
        //        e.Row.Cells[4].Visible = true;
        //    }
        //    else
        //    {
        //        e.Row.Cells[3].Visible = false;
        //        e.Row.Cells[4].Visible = false;
        //    }

        //    if (Convert.ToString(hdnmainexpStatus.Value).Trim() == "2" || Convert.ToString(hdnmainexpStatus.Value).Trim() == "3" || Convert.ToString(hdnmainexpStatus.Value).Trim() == "4" || Convert.ToString(hdn_apprStatus.Value).Trim() == "Approved")
        //    {
        //        e.Row.Cells[6].Visible = false;
        //    }
        //}

    }


    protected void gvexpensdtls_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            if (Convert.ToString(hdnmainexpStatus.Value).Trim() == "2" || Convert.ToString(hdnmainexpStatus.Value).Trim() == "3" || Convert.ToString(hdnmainexpStatus.Value).Trim() == "4" || Convert.ToString(hdn_apprStatus.Value).Trim() == "Approved")
            {
                e.Row.Cells[8].Visible = false;
            }
        }
    }

    protected void lnkviewfile_Click(object sender, EventArgs e)
    {
        try
        {
            hdnfileid.Value = "";
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            Int32 ifileid = Convert.ToInt32(gvuploadedFiles.DataKeys[row.RowIndex].Values[0]);
            LinkButton lnkviewfile = (LinkButton)row.FindControl("lnkviewfile");

            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["trvlingExpfiles"]).Trim()), lnkviewfile.Text);
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
    protected void lnkDeleteexpFile_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            Int32 ifileid = Convert.ToInt32(gvuploadedFiles.DataKeys[row.RowIndex].Values[0]);
            LinkButton lnkviewfile = (LinkButton)row.FindControl("lnkviewfile");
            hdnfileid.Value = "";
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["trvlingExpfiles"]).Trim()), lnkviewfile.Text);

            if (System.IO.File.Exists(strfilepath))
            {
                System.IO.File.Delete(strfilepath);
                hdnfileid.Value = Convert.ToString(ifileid);
                getExpenseUploadedFiles();
                hdnfileid.Value = "";
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }


    }

    protected void lnkbtn_expdtls_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        if (Convert.ToString(lstTripType.SelectedValue).Trim() == "" || Convert.ToString(lstTripType.SelectedValue).Trim() == "0")
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

        txtTripId_Oth.Text = "";
        txtTravelType_Oth.Text = "";
        txtEmpCode_Oth.Text = "";
        txtExpdtls_Oth.Text = "";
        txtFromdate_Oth.Text = "";
        txtAmt_Oth.Text = "0";
        txtnoofDays_Oth.Text = "";
        txtRemarks_Oth.Text = "";
        txtpaidby.Text = "";

        if (Div_Oth.Visible)
        {
            Div_Oth.Visible = false;
            lnkbtn_expdtls.Text = "+";
        }
        else
        {
            Div_Oth.Visible = true;
            lnkbtn_expdtls.Text = "-";
            hdntrdetailsid_Oth.Value = "0";
            Oth_btnSave.Text = "Submit";
        }
        //AdditionalExpenses
        AssigningSessions();
        lblmessage.Text = "";

        GetTripDetails_Oth();
        GetTravelMode_Oth();
        //GetCities();

        txtEmpCode_Oth.Text = Convert.ToString(Session["ReqEmpCode"]);
        txtTravelType_Oth.Text = Convert.ToString(Session["TravelType"]);
        //   hdnTripid.Value = Convert.ToString(Session["TripID"]);
        hdnfromdate_Oth.Value = Convert.ToString(Session["Fromdate"]);
        hdnTodate_Oth.Value = Convert.ToString(Session["Todate"]);
        //txtFromdate.Text = hdnTodate_Oth.Value;


        if (Convert.ToString(hdnTripid.Value).Trim() == "" || Convert.ToString(hdnTripid.Value).Trim() == "0")
        {
            hdnTryiptypeid.Value = Convert.ToString(Session["TripTypeId"]);
            hflGrade_Oth.Value = Convert.ToString(Session["Grade"]);
        }
        ////else
        ////{
        ////    getTravelDetails_Oth();
        ////}

        ////if (Request.QueryString.Count > 0)
        ////{
        ////    hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();
        ////    hdntrdetailsid_Oth.Value = Convert.ToString(Request.QueryString[1]).Trim();
        ////    hdnexpSrno_Oth.Value = Convert.ToString(Request.QueryString[2]).Trim();
        ////    hdnexp_id.Value = Convert.ToString(Request.QueryString[1]).Trim();

        ////    if (Convert.ToString(hdnexpSrno_Oth.Value).Trim() != "0")
        ////    {
        ////        get_Actual_DayDiff_Oth();
        ////        getTravelDetailsEdit_Oth();
        ////        Oth_btnSave.Text = "Update";
        ////    }
        ////}
        Oth_btnDelete.Visible = false;

        //Response.Redirect("~/procs/AdditionalExpenses.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value + "&expsrno=0");
    }

    protected void lnkAddExpense_Click(object sender, EventArgs e)
    {
        txtTripId_Oth.Text = "";
        txtTravelType_Oth.Text = "";
        txtEmpCode_Oth.Text = "";
        txtExpdtls_Oth.Text = "";
        txtFromdate_Oth.Text = "";
        txtAmt_Oth.Text = "0";
        txtnoofDays_Oth.Text = "";
        txtRemarks_Oth.Text = "";
        txtpaidby.Text = "";

        AssigningSessions();
        Int32 iexpsrno = 0;
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        // hdnTripid.Value = Convert.ToString(gvexpensdtls.DataKeys[row.RowIndex].Values[0]).Trim();            
        //iexpsrno = Convert.ToInt32(gvexpensdtls.DataKeys[row.RowIndex].Values[3]);
        iexpsrno = Convert.ToInt32(gvexpensdtls.DataKeys[row.RowIndex].Values[2]);

        if (Div_Oth.Visible == false)
        {
            Div_Oth.Visible = true;
            lnkbtn_expdtls.Text = "-";
            Oth_btnSave.Text = "Update";
        }
        //AdditionalExpenses
        lblmessage.Text = "";

        GetTripDetails_Oth();
        GetTravelMode_Oth();
        //GetCities();

        txtEmpCode_Oth.Text = Convert.ToString(Session["ReqEmpCode"]);
        txtTravelType_Oth.Text = Convert.ToString(Session["TravelType"]);
        //   hdnTripid.Value = Convert.ToString(Session["TripID"]);
        hdnfromdate_Oth.Value = Convert.ToString(Session["Fromdate"]);
        hdnTodate_Oth.Value = Convert.ToString(Session["Todate"]);
        //txtFromdate.Text = hdnTodate_Oth.Value;


        if (Convert.ToString(hdnTripid.Value).Trim() == "" || Convert.ToString(hdnTripid.Value).Trim() == "0")
        {
            hdnTryiptypeid.Value = Convert.ToString(Session["TripTypeId"]);
            hflGrade_Oth.Value = Convert.ToString(Session["Grade"]);
        }
        else
        {
            getTravelDetails_Oth();
        }

        //hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();
        //hdntrdetailsid_Oth.Value = Convert.ToString(Request.QueryString[1]).Trim();
        hdnexpSrno_Oth.Value = Convert.ToString(iexpsrno).Trim();
        //hdnexp_id.Value = Convert.ToString(Request.QueryString[1]).Trim();

        if (Convert.ToString(hdnexpSrno_Oth.Value).Trim() != "0")
        {
            get_Actual_DayDiff_Oth();
            getTravelDetailsEdit_Oth();
            Oth_btnSave.Text = "Update";
        }

        //Response.Redirect("~/procs/AdditionalExpenses.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value + "&expsrno=" + iexpsrno);
    }

    #endregion

    #region PageMethods

    private void checkExpApprovalStatus()
    {
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_chk_exp_approvalStatus";

        spars[1] = new SqlParameter("@expid", SqlDbType.Decimal);
        if (Convert.ToString(hdnexp_id.Value).Trim() != "")
            spars[1].Value = Convert.ToString(hdnexp_id.Value).Trim();

        DataTable dtcities = spm.getDataList(spars, "SP_GETALL_Expense_DETAILS");
        if (dtcities.Rows.Count > 0)
        {
            hdnApprovalStatusExp.Value = "Pending";
        }
    }

    protected string GetApprove_RejectList(Decimal maxtripid)
    {
        var getcompSelectedText = ddl_ProjectName.SelectedItem.Text;
        var getcomp_code = Convert.ToString(ddl_ProjectName.SelectedValue);
        var Dept_id = 0;
        if (Convert.ToString(hdncomp_code.Value) != "")
        {
            getcomp_code = Convert.ToString(hdncomp_code.Value);
        }
        if (getcompSelectedText.Contains("Head Office"))
        {
            Dept_id = Convert.ToInt32(ddl_DeptName.SelectedValue);
            if (Convert.ToString(hdndept_id.Value) != "0")
            {
                Dept_id = Convert.ToInt32(hdndept_id.Value);
            }
        }

        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        dtAppRej = spm.GetExpensesTravelApproverStatus(txtEmpCode.Text, Convert.ToInt32(maxtripid), Convert.ToInt32(hdnTravelConditionid.Value), getcomp_code, Dept_id);
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

    protected string GetIntermediatesList_Formail()
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

    public void GetExpenseContitionId()
    {
        if (Convert.ToString(hdnTraveltypeid.Value).Trim() == "")
            return;
        if (Convert.ToString(hdnEligible.Value).Trim() == "")
            return;


        int TrConditionId = 0;
        TrConditionId = spm.getExpenseConditionTypeId(Convert.ToInt32(hdnTraveltypeid.Value), hdnEligible.Value);
        hdnTravelConditionid.Value = Convert.ToString(TrConditionId);

        getApproverdata();

         getIntermidateslist();

    }

    public void getIntermidateslist()
    {
        DataTable dtIntermediate = new DataTable();
        dtIntermediate = spm.GetExpenseTravelIntermediateName(txtEmpCode.Text, Convert.ToInt32(hdnTravelConditionid.Value), hflGrade.Value);
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
        var getcomp_code = "";
        var Dept_id = 0;
        getcomp_code = Convert.ToString(hdncomp_code.Value);
        var getcompSelectedText = hdncomp_name.Value;
        if (getcompSelectedText.Contains("Head Office"))
        {
            Dept_id = Convert.ToInt32(hdndept_id.Value);
        }
        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.GetExpensesApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), getcomp_code, Dept_id);
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
            // lblmessage.Text = "Since you are not assigned under any approver you can not apply for any type of Travel request, please contact HR";
            // setEnablesCntrls(false);

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
                hflEMPAGrade.Value = Convert.ToString(dtEmpDetails.Rows[0]["emp_A_Grade"]).Trim();

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
            lstTripType.Items.Insert(0, new ListItem("Select Travel Type", "0"));

        }
    }

    public void getExpTravelDetails()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetExpTravel_Main(txtEmpCode.Text, Convert.ToDecimal(hdnexp_id.Value));

        //dgTravelRequest.DataSource = null;
        //dgTravelRequest.DataBind();

        if (dtTrDetails.Rows.Count > 0)
        {
            //hdnTripid.Value = Convert.ToString(dtTrDetails.Rows[0]["trip_id"]);
            //txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_description"]);
            //txtToDate.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_date"]);
            txtTriptype.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_description"]);
            lstTripType.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["trip_type"]);
            hdnTraveltypeid.Value = lstTripType.SelectedValue;
            txtReason.Text = Convert.ToString(dtTrDetails.Rows[0]["trp_reason"]);
            txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_frm_date"]);
            txtToDate.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_to_date"]);

            //ddl_ProjectName.SelectedItem.Text = Convert.ToString(dtTrDetails.Rows[0]["Project_Name"]);
            //ddl_DeptName.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["Dept_Name"]);

            txtdailyhaltingallowance.Text = Convert.ToString(dtTrDetails.Rows[0]["Daily_Halting_allowance"]);

            txtTotAmtClaimed.Text = Convert.ToString(dtTrDetails.Rows[0]["TotalAmount_Claimed"]);

            txtLessAdvTaken.Text = Convert.ToString(dtTrDetails.Rows[0]["LessAdvanceTaken"]);

            txtnetPaybltoComp.Text = Convert.ToString(dtTrDetails.Rows[0]["Net_Pay_Company"]);
            txtnetPaybltoEmp.Text = Convert.ToString(dtTrDetails.Rows[0]["Net_Pay_Employee"]);
            txtAccountAmount.Text = Convert.ToString(dtTrDetails.Rows[0]["Amt_Release_By_Acc"]);
            hdnIsDraft.Value = Convert.ToString(dtTrDetails.Rows[0]["IsDraft"]);


            CultureInfo hindi = new CultureInfo("hi-IN");

            if (Convert.ToString(txtTotAmtClaimed.Text).Trim() != "")
            {
                decimal parsed = decimal.Parse(Convert.ToString(txtTotAmtClaimed.Text), CultureInfo.InvariantCulture);
                txtTotAmtClaimed.Text = string.Format(hindi, "{0:#,#0.00}", parsed);
            }
            if (Convert.ToString(txtLessAdvTaken.Text).Trim() != "")
            {
                decimal parsed1 = decimal.Parse(Convert.ToString(txtLessAdvTaken.Text), CultureInfo.InvariantCulture);
                txtLessAdvTaken.Text = string.Format(hindi, "{0:#,#0.00}", parsed1);
            }
            if (Convert.ToString(txtnetPaybltoEmp.Text).Trim() != "")
            {
                decimal parsed2 = decimal.Parse(Convert.ToString(txtnetPaybltoEmp.Text), CultureInfo.InvariantCulture);
                txtnetPaybltoEmp.Text = string.Format(hindi, "{0:#,#0.00}", parsed2);
            }
            if (Convert.ToString(txtnetPaybltoComp.Text).Trim() != "")
            {
                decimal parsed3 = decimal.Parse(Convert.ToString(txtnetPaybltoComp.Text), CultureInfo.InvariantCulture);
                txtnetPaybltoComp.Text = string.Format(hindi, "{0:#,#0.00}", parsed3);
            }

            if (Convert.ToString(txtAccountAmount.Text).Trim() != "")
            {
                decimal parsed4 = decimal.Parse(Convert.ToString(txtAccountAmount.Text), CultureInfo.InvariantCulture);
                txtAccountAmount.Text = string.Format(hindi, "{0:#,#0.00}", parsed4);
            }

            txtReasonDeviation.Text = Convert.ToString(dtTrDetails.Rows[0]["Reason_Deviation"]);
            hdnmainexpStatus.Value = Convert.ToString(dtTrDetails.Rows[0]["trip_status"]);
            hdn_apprStatus.Value = Convert.ToString(dtTrDetails.Rows[0]["appr_status"]);
            lblheading.Text = "Travel Expense Voucher - " + Convert.ToString(dtTrDetails.Rows[0]["Vouno"]);
            hdnvouno.Value = Convert.ToString(dtTrDetails.Rows[0]["Vouno"]);
            txtAdvance.Text = Convert.ToString(dtTrDetails.Rows[0]["req_adv_amt"]);
            txtLessAdvTaken.Text = txtAdvance.Text;
            Session["AdvanceTaken"] = txtAdvance.Text;
            hdnActualTrvlDays.Value = Convert.ToString(dtTrDetails.Rows[0]["actualdays"]);
            hdnEligible.Value = Convert.ToString(dtTrDetails.Rows[0]["req_adv_amt"]);

            hdncomp_code.Value = Convert.ToString(dtTrDetails.Rows[0]["comp_code"]);
            hdndept_id.Value = Convert.ToString(dtTrDetails.Rows[0]["dept_id"]);
            hdncomp_name.Value = Convert.ToString(dtTrDetails.Rows[0]["Project_Name"]);

            if (Convert.ToString(txtTriptype.Text).Trim() == "Domestic" || Convert.ToString(txtTriptype.Text).Trim() == "")
            {
                txtreqCur.Visible = false;
                lbl_cur.Visible = false;
            }
            else
            {
                if (hdnTripid.Value != "0")
                {
                    //txtreqCur.Visible = true;
                    //lbl_cur.Visible = true;
                }
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
                // FromDateValidation();
            }
            var getPr = ddl_ProjectName.SelectedItem.Value;
            ddl_ProjectName.Items.FindByValue(getPr).Selected = false;
            var getnewPro = Convert.ToString(dtTrDetails.Rows[0]["comp_code"]);
            ddl_ProjectName.Items.FindByValue(getnewPro).Selected = true;
            if (Convert.ToString(ddl_ProjectName.SelectedItem).Contains("Head Office"))
            {
                ddl_DeptName.Enabled = true;
                var getDep = ddl_DeptName.SelectedItem.Value;
                ddl_DeptName.Items.FindByValue(getDep).Selected = false;
                var getnewDept = Convert.ToString(dtTrDetails.Rows[0]["dept_id"]);
                ddl_DeptName.Items.FindByValue(getnewDept).Selected = true;
            }
            else
            {
                ddl_DeptName.Enabled = false;
            }



            mobile_btnPrintPV.Visible = true;
            //dgTravelRequest.DataSource = dtTrDetails;
            //dgTravelRequest.DataBind();
            Boolean blnIsNewjoinee = false;
            if (Convert.ToString(dtTrDetails.Rows[0]["IsNewjoinee"]).Trim() != "")
            {
                blnIsNewjoinee = Convert.ToBoolean(dtTrDetails.Rows[0]["IsNewjoinee"]);
            }

            if (blnIsNewjoinee == true)
            {
                chkIsNewJoinExp_req.Checked = true;
                chkIsNewJoinExp_req.Enabled = false;
            }

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

            // txtAdvance.Text = Convert.ToString(Session["Advance"]);
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
        Session["TripidTR"] = hdnTripid.Value;
        Session["TravelType"] = txtTriptype.Text;
        Session["Fromdate"] = txtFromdate.Text;
        Session["Todate"] = txtToDate.Text;
        Session["ReqEmpCode"] = txtEmpCode.Text;
        Session["reasntrvl"] = txtReason.Text;
        Session["Grade"] = hflGrade.Value;
        Session["EMPAGrade"] = hflEMPAGrade.Value;
        Session["Reason"] = txtReason.Text;
        Session["TripTypeId"] = Convert.ToString(lstTripType.SelectedValue);
        Session["Location"] = hdnDestnation.Value;
        Session["TripID"] = hdnAcctripid.Value;
        Session["TripID"] = hdnLcalTripid.Value;
        Session["DestLocation"] = hdnDeptPlace.Value;
        hdnTraveltypeid.Value = lstTripType.SelectedValue;
        Session["Tr_type_id"] = hdnTraveltypeid.Value;
        Session["acttrvldays"] = Convert.ToString(hdnActualTrvlDays.Value);
        if (Convert.ToString(txtAdvance.Text).Trim() != "")
        {
            Session["AdvanceTaken"] = txtAdvance.Text;
        }
        else
        {
            // decimal advance = 0;
            // txtAdvance.Text = Convert.ToString(advance);
            Session["AdvanceTaken"] = txtAdvance.Text;
        }


    }

    public DataTable getApprover_DH_HOD(string scompcode, string sdeptid, string sempcode)
    {

        DataTable dtTrDetails = new DataTable();
        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_HOD_DH_Approver_Trvl_Expenses";

        spars[1] = new SqlParameter("@comp_code", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(scompcode);

        spars[2] = new SqlParameter("@dept_id", SqlDbType.Int);
        if (Convert.ToString(sdeptid).Trim() != "")
            spars[2].Value = Convert.ToInt32(sdeptid);
        else
            spars[2].Value = 0;


        spars[3] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[3].Value = Convert.ToString(sempcode);

        dtTrDetails = spm.getDataList(spars, "SP_GETALL_Expense_DETAILS");
        return dtTrDetails;


    }

    private void getApproverlist()
    {
        try
        {
            var getcompSelectedText = ddl_ProjectName.SelectedItem.Text;
            var getcomp_code = Convert.ToString(ddl_ProjectName.SelectedValue);
            var Dept_id = 0;
            if (getcompSelectedText.Contains("Head Office"))
            {
                Dept_id = Convert.ToInt32(ddl_DeptName.SelectedValue);
            }
            DataTable dtapprover = new DataTable();
            //if(hdnIsDraft.Value=="1")
            //{
            //    getcomp_code =Convert.ToString(hdncomp_code.Value);
            //}
            dtapprover = spm.GetTravelApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnTravelConditionid.Value), getcomp_code, Dept_id);

            if (Convert.ToString(Session["Empcode"]).Trim() != "00002726")
            {
                if (dtapprover.Rows.Count == 1)
                {
                    dtapprover = getApprover_DH_HOD(getcomp_code, Convert.ToString(Dept_id), txtEmpCode.Text);
                }
            }

            lstApprover.Items.Clear();
            if (dtapprover.Rows.Count > 0)
            {
                lstApprover.DataSource = dtapprover;
                lstApprover.DataTextField = "names";
                lstApprover.DataValueField = "names";
                lstApprover.DataBind();

                hdnApprEmailaddress.Value = Convert.ToString(dtapprover.Rows[0]["Emp_Emailaddress"]);
                hdnApprId.Value = Convert.ToString(dtapprover.Rows[0]["APPR_ID"]);
                hflapprcode.Value = Convert.ToString(dtapprover.Rows[0]["A_EMP_CODE"]);
            }
            else
            {
                lblmessage.Text = "Since you are not assigned under any approver you can not apply for any type of Travel request, please contact HR";


            }
        }
        catch (Exception ex)
        {
        }
    }
    private void setVisibleCntrls()
    {
        // btnTra_Details.Visible = false;
        //  trvl_accmo_btn.Visible = false;
        // trvl_localbtn.Visible = false;
        txtFromdate.Enabled = false;
        txtToDate.Enabled = false;
        txtReason.Enabled = false;
        txtAdvance.Enabled = false;
        txtAdvance.Visible = true;
        spnadv.Visible = true;
        txtTriptype.Enabled = false;
        Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
        txtTriptype.BackColor = color;
        //spntrvldtls.Visible = false;
        //btnTra_Details.Visible = false;
        //spnadvrequired.Visible = false;
        //txtAdvance.Visible = false;
        //spnaccomodation.Visible = false;
        //trvl_accmo_btn.Visible = false;
        //spnlocalTrvl.Visible = false;
        //trvl_localbtn.Visible = false;
        //dgLocalTravel.Visible = false;
        //dgAccomodation.Visible = false;
        //litrvldetail.Visible = false;
        //litrvlgrid.Visible = false;
        //dgTravelRequest.Visible = false;
        //litrvlAdvnce.Visible = false;
        //litrvlcurrency.Visible = false;
        //litrvlaccomodation.Visible = false;
        //litrvlgridAccomodation.Visible = false;

        //lsttrvlapprover.Visible = false;
        //lsttrvlIntermidates.Visible = false;

    }

    private void CalculateClaimAmt()
    {
        txtTotAmtClaimed.Text = "0";
        Double dtotclaimAmt = 0;
        Double dtotpaidbyComAmt = 0;
        Double TotalAmount_Claimed = 0, LessAdvanceTaken = 0, Net_Pay_Company = 0, Net_Pay_Employee = 0;

        //txtLessAdvTaken.Text = txtAdvance.Text;
        CultureInfo hindi = new CultureInfo("hi-IN");
        if (Convert.ToString(txtAdvance.Text) != "")
        {
            decimal parsed = decimal.Parse(Convert.ToString(txtAdvance.Text), CultureInfo.InvariantCulture);
            txtLessAdvTaken.Text = string.Format(hindi, "{0:#,#0.00}", parsed);
        }


        #region Calulate Total Claim Amount
        for (Int32 irow = 0; irow < gvexpensdtls.Rows.Count - 1; irow++)
        {
            if (Convert.ToString(gvexpensdtls.Rows[irow].Cells[7].Text).Trim() != "" && Convert.ToString(gvexpensdtls.Rows[irow].Cells[7].Text).Trim() != "&nbsp;")
                dtotclaimAmt += Convert.ToDouble(gvexpensdtls.Rows[irow].Cells[7].Text);

            if (Convert.ToString(gvexpensdtls.Rows[irow].Cells[5].Text).Trim() != "" && Convert.ToString(gvexpensdtls.Rows[irow].Cells[5].Text).Trim() != "&nbsp;")
                dtotpaidbyComAmt += Convert.ToDouble(gvexpensdtls.Rows[irow].Cells[5].Text);
        }
        //  txtTotAmtClaimed.Text = Convert.ToString(Math.Round(dtotclaimAmt,2));

        //txtTotAmtClaimed.Text = Convert.ToString(dtotclaimAmt.ToString("00.00", CultureInfo.InvariantCulture));

        txtTotAmtClaimed.Text = string.Format(hindi, "{0:#,#0.00}", dtotclaimAmt);


        if (Convert.ToString(txtTotAmtClaimed.Text).Trim() != "")
            TotalAmount_Claimed = Convert.ToDouble(txtTotAmtClaimed.Text);

        if (Convert.ToString(txtLessAdvTaken.Text).Trim() != "")
            LessAdvanceTaken = Convert.ToDouble(txtAdvance.Text);

        if (Convert.ToString(txtnetPaybltoComp.Text).Trim() != "")
            Net_Pay_Company = Convert.ToDouble(txtnetPaybltoComp.Text);

        if (Convert.ToString(txtnetPaybltoEmp.Text).Trim() != "")
            Net_Pay_Employee = Convert.ToDouble(txtnetPaybltoEmp.Text);
        #endregion


        if (LessAdvanceTaken >= 0)
        {
            Net_Pay_Company = LessAdvanceTaken - (TotalAmount_Claimed - dtotpaidbyComAmt);
            if (Net_Pay_Company > 0)
            {
                //txtnetPaybltoComp.Text = Convert.ToString(Net_Pay_Company);
                txtnetPaybltoComp.Text = string.Format(hindi, "{0:#,#0.00}", Net_Pay_Company);

                Net_Pay_Employee = 0;
                //txtnetPaybltoEmp.Text = Convert.ToString(Net_Pay_Employee);
                txtnetPaybltoEmp.Text = string.Format(hindi, "{0:#,#0.00}", Net_Pay_Employee);
            }
            else
            {
                //txtnetPaybltoEmp.Text = Convert.ToString(Math.Round(Net_Pay_Company*-1,2));
                //txtnetPaybltoEmp.Text = Convert.ToString((Net_Pay_Company * -1).ToString("00.00", CultureInfo.InvariantCulture));
                txtnetPaybltoEmp.Text = string.Format(hindi, "{0:#,#0.00}", Net_Pay_Company * -1);

                Net_Pay_Company = 0;
                //txtnetPaybltoComp.Text = Convert.ToString(Math.Round(Net_Pay_Company,2));
                //txtnetPaybltoComp.Text = Convert.ToString((Net_Pay_Company).ToString("00.00", CultureInfo.InvariantCulture));

                txtnetPaybltoComp.Text = string.Format(hindi, "{0:#,#0.00}", Net_Pay_Company);
            }
        }

    }

    public void getTravelsDetails_usingTripid()
    {
        try
        {
            DataTable dtTrDetails = new DataTable();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_getempTravelRequest_forexp";

            spars[1] = new SqlParameter("@trip_id", SqlDbType.Int);
            spars[1].Value = hdnTripid.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            dtTrDetails = spm.getDataList(spars, "SP_GETALL_Expense_DETAILS");

            if (dtTrDetails.Rows.Count > 0)
            {
                txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_frm_date"]);
                txtToDate.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_to_date"]);
                txtReason.Text = Convert.ToString(dtTrDetails.Rows[0]["trp_reason"]).Trim();
                lstTripType.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["trip_type"]).Trim();
                txtTriptype.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_description"]).Trim();
                txtAdvance.Text = Convert.ToString(dtTrDetails.Rows[0]["req_adv_amt"]).Trim();
                hdnActualTrvlDays.Value = Convert.ToString(dtTrDetails.Rows[0]["actualdays"]);
                Session["AdvanceTaken"] = txtAdvance.Text;
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    }
    public void GetExpenseTravelDetails()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetExpenseTravelDetails(txtEmpCode.Text);

        dgTravelRequest.DataSource = null;
        dgTravelRequest.DataBind();

        if (Convert.ToString(Session["TravelType"]).Trim() != null && Convert.ToString(Session["TravelType"]).Trim() != "")
        {
            txtTriptype.Text = Convert.ToString(Session["TravelType"]);
            lstTripType.SelectedValue = Convert.ToString(Session["Tr_type_id"]);
            txtReason.Text = Convert.ToString(Session["Reason"]);
            txtFromdate.Text = Convert.ToString(Session["Fromdate"]);
            txtToDate.Text = Convert.ToString(Session["Todate"]);
            hdnTraveltypeid.Value = Convert.ToString(Session["Tr_type_id"]);
            hdnTripid.Value = Convert.ToString(Session["TripidTR"]);
        }

        if (dtTrDetails.Rows.Count > 0)
        {
            ////hdnTripid.Value = Convert.ToString(dtTrDetails.Rows[0]["trip_id"]);
            ////txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["departure_date"]);
            ////txtToDate.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_date"]);


            //if (Convert.ToString(txtTriptype.Text).Trim() == "Domestic" || Convert.ToString(txtTriptype.Text).Trim() == "")
            //{
            //    txtreqCur.Visible = false;
            //    lbl_cur.Visible = false;
            //}
            //else
            //{
            //    txtreqCur.Visible = true;
            //    lbl_cur.Visible = true;
            //}

            //if (Convert.ToString(hdnDeviation.Value).Trim() == "Yes")
            //{
            //    hdnEligible.Value = "Deviation";
            //}
            //else
            //{
            //    hdnEligible.Value = "Eligible";

            //}
            //if (Convert.ToString(txtFromdate.Text).Trim() != "")
            //{
            //  //  FromDateValidation();
            //}

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

            //txtAdvance.Text = Convert.ToString(Session["Advance"]);
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

    private void setEnablesCntrls(Boolean blncheck)
    {
        trvl_btnSave.Enabled = blncheck;
        txtFromdate.Enabled = blncheck;
        txtToDate.Enabled = blncheck;
        txtTriptype.Enabled = blncheck;
        txtReason.Enabled = blncheck;
        txtAdvance.Enabled = blncheck;
        txtReasonDeviation.Enabled = blncheck;
        ploadexpfile.Enabled = blncheck;

    }

    private Boolean FromDateValidation_exp()
    {
        Boolean blnValid = false;
        lblmessage.Text = "";
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";
        string message = "";
        hdnTrdays.Value = "0";
        string sLocation = "";

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
            //txtreqCur.Visible = true;
            //lbl_cur.Visible = true;
        }


        if (Convert.ToString(ddl_ProjectName.SelectedValue).Trim() == "0")
        {
            blnValid = true;

        }

        sLocation = Convert.ToString(ddl_ProjectName.SelectedValue).Trim();
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
            //dttraveletails = spm.Get_TravelValidationResult(strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);
            dttraveletails = spm.Get_TravelExpenseValidationResult_New(strfromDate, strToDate, txtEmpCode.Text, hdnexp_id.Value, sLocation);
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

            if (dttraveletails.Tables[0].Rows.Count > 0)
            {
                hdnTrdays.Value = Convert.ToString(dttraveletails.Tables[0].Rows[0]["TotalTravelDays"]);
                hdnActualTrvlDays.Value = Convert.ToString(dttraveletails.Tables[0].Rows[0]["actualTravelDays"]);
                //GetExpenseContitionId();
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

                //dttraveletails = spm.Get_Travel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);
                dttraveletails = spm.Get_ExpTravel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode_Trvl.Text, hdnTripid.Value, "ExpTrvlDtls");
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

                //dttraveletails = spm.Get_Travel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);
                dttraveletails = spm.Get_ExpTravel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode_Accm.Text, hdnTripid.Value, "ExpAccomodatins");
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

                //dttraveletails = spm.Get_Travel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);
                dttraveletails = spm.Get_ExpTravel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode_Locl.Text, hdnTripid.Value, "ExpLocalTrvls");
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

            #region Check date validation for Other Expenses

            for (Int32 irow = 0; irow < gvexpensdtls.Rows.Count; irow++)
            {
                if (Convert.ToString(gvexpensdtls.Rows[irow].Cells[3].Text).Trim() == "Other")
                {
                    strdate_F = Convert.ToString(gvexpensdtls.Rows[irow].Cells[0].Text).Trim();


                    #region date formatting

                    if (Convert.ToString(strdate_F).Trim() != "")
                    {
                        strdate = Convert.ToString(strdate_F).Trim().Split('/');
                        strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                    }
                    if (Convert.ToString(strdate_F).Trim() != "")
                    {
                        strdate = Convert.ToString(strdate_F).Trim().Split('/');
                        strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                    }

                    #endregion

                    //dttraveletails = spm.Get_Travel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value);
                    //dttraveletails = spm.Get_ExpTravel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode_Locl.Text, hdnTripid.Value, "ExpLocalTrvls");
                    dttraveletails = spm.Get_ExpTravel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode_Oth.Text, hdnTripid.Value, "ExpLocalTrvls");
                    if (dttraveletails.Tables[1].Rows.Count > 0)
                    {
                        message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);
                    }

                    if (Convert.ToString(message).Trim() != "")
                    {
                        lblmessage.Text = Convert.ToString(message).Trim();
                        blnValid = true;
                        gvexpensdtls.Rows[irow].BackColor = Color.Yellow;
                    }
                    else
                    {
                        gvexpensdtls.Rows[irow].BackColor = Color.White;
                    }
                }
            }
            #endregion
            #endregion
        }

        return blnValid;
    }

    private void FromDateValidation()
    {
        if (Convert.ToString(txtFromdate.Text).Trim() == "")
            return;

        if (Convert.ToString(txtToDate.Text).Trim() == "")
            return;

        hdnTraveltypeid.Value = lstTripType.SelectedValue;
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
            dttraveletails = spm.Get_TravelExpenseValidationResult_New(strfromDate, strToDate, txtEmpCode.Text, hdnTripid.Value, "");

            if (dttraveletails.Tables[1].Rows.Count > 0)
            {
                message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);
            }

            if (Convert.ToString(message).Trim() != "")
            {
                lblmessage.Text = Convert.ToString(message).Trim();
                txtFromdate.Text = "";
                txtToDate.Text = "";
                return;
            }

            if (dttraveletails.Tables[0].Rows.Count > 0)
            {
                hdnTrdays.Value = Convert.ToString(dttraveletails.Tables[0].Rows[0]["TotalTravelDays"]);
                hdnActualTrvlDays.Value = Convert.ToString(dttraveletails.Tables[0].Rows[0]["actualTravelDays"]);
                GetExpenseContitionId();
            }



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

    private string get_approverlist_ifTD_COS(decimal maxtripid)
    {
        string email_ids = "";
        DataTable dtApproverEmailIds = new DataTable();

        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "getPreviousAppr_Interm_mails";


        spars[1] = new SqlParameter("@expid", SqlDbType.Decimal);
        spars[1].Value = maxtripid;

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = txtEmpCode.Text.ToString();

        spars[3] = new SqlParameter("@ApproverCode", SqlDbType.VarChar);
        spars[3].Value = hflapprcode.Value;


        dtApproverEmailIds = spm.getDataList(spars, "SP_GETALL_Expense_DETAILS");

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

    protected string getCancellationmailList()
    {
        string email_ids = "";
        DataTable dtApproverEmailIds = new DataTable();

        //dtApproverEmailIds = spm.ExpensePreviousApproverDetails_Reqst(txtEmpCode.Text, Convert.ToInt32(hdnTripid.Value));
        dtApproverEmailIds = spm.ExpensePreviousApproverDetails(txtEmpCode.Text, Convert.ToInt32(hdnexp_id.Value));
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

    private void getApproverlist_New()
    {
        var getcompSelectedText = ddl_ProjectName.SelectedItem.Text;
        var getcomp_code = Convert.ToString(ddl_ProjectName.SelectedValue);
        var Dept_id = 0;
        if (Convert.ToString(hdncomp_code.Value) != "")
        {
            getcomp_code = Convert.ToString(hdncomp_code.Value);
        }
        if (getcompSelectedText.Contains("Head Office"))
        {
            Dept_id = Convert.ToInt32(ddl_DeptName.SelectedValue);
            if (Convert.ToString(hdndept_id.Value) != "0")
            {
                Dept_id = Convert.ToInt32(hdndept_id.Value);
            }
        }
        string appr_type = "APP";
        DataTable dtapprover = new DataTable();
        dtapprover = spm.GetExpenseApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnexp_id.Value), Convert.ToInt32(hdnTravelConditionid.Value), appr_type.ToString(), getcomp_code, Dept_id);
        lstApprover.Items.Clear();
        dspaymentVoucher_Apprs.Tables.Add(dtapprover);
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


    private void travelDetailsRowCreated()
    {
        #region Trvl Dtsl
        if (Convert.ToString(hdnTripid.Value).Trim() != "0" && Convert.ToString(hdnTripid.Value).Trim() != "")
        {
            dgTravelRequest.Columns[5].Visible = true;
            dgTravelRequest.Columns[6].Visible = true;

            //e.Row.Cells[5].Visible = true;
            //e.Row.Cells[6].Visible = true;
        }
        else
        {
            dgTravelRequest.Columns[5].Visible = false;
            dgTravelRequest.Columns[6].Visible = false;
            // e.Row.Cells[5].Visible = false;
            //e.Row.Cells[6].Visible = false;
        }

        if (Convert.ToString(hdnmainexpStatus.Value).Trim() == "2" || Convert.ToString(hdnmainexpStatus.Value).Trim() == "3" || Convert.ToString(hdnmainexpStatus.Value).Trim() == "4" || Convert.ToString(hdn_apprStatus.Value).Trim() == "Approved")
        {
            //e.Row.Cells[8].Visible = false;
            dgTravelRequest.Columns[8].Visible = false;
        }
        #endregion

        #region  Accomodation
        if (Convert.ToString(hdnTripid.Value).Trim() != "0" && Convert.ToString(hdnTripid.Value).Trim() != "")
        {
            dgAccomodation.Columns[4].Visible = true;
            dgAccomodation.Columns[5].Visible = true;

            // e.Row.Cells[3].Visible = true;
            //e.Row.Cells[4].Visible = true;
        }
        else
        {
            dgAccomodation.Columns[4].Visible = false;
            dgAccomodation.Columns[5].Visible = false;
            //e.Row.Cells[3].Visible = false;
            //e.Row.Cells[4].Visible = false;
        }

        if (Convert.ToString(hdnmainexpStatus.Value).Trim() == "2" || Convert.ToString(hdnmainexpStatus.Value).Trim() == "3" || Convert.ToString(hdnmainexpStatus.Value).Trim() == "4" || Convert.ToString(hdn_apprStatus.Value).Trim() == "Approved")
        {
            dgAccomodation.Columns[7].Visible = false;
            //e.Row.Cells[6].Visible = false;
        }

        #endregion

        #region Local Trvl
        if (Convert.ToString(hdnTripid.Value).Trim() != "0" && Convert.ToString(hdnTripid.Value).Trim() != "")
        {
            dgLocalTravel.Columns[4].Visible = true;
            dgLocalTravel.Columns[5].Visible = true;
            //e.Row.Cells[3].Visible = true;
            //e.Row.Cells[4].Visible = true;
        }
        else
        {
            dgLocalTravel.Columns[4].Visible = false;
            dgLocalTravel.Columns[5].Visible = false;
            //e.Row.Cells[3].Visible = false;
            //e.Row.Cells[4].Visible = false;
        }

        if (Convert.ToString(hdnmainexpStatus.Value).Trim() == "2" || Convert.ToString(hdnmainexpStatus.Value).Trim() == "3" || Convert.ToString(hdnmainexpStatus.Value).Trim() == "4" || Convert.ToString(hdn_apprStatus.Value).Trim() == "Approved")
        {
            dgLocalTravel.Columns[7].Visible = false;
            //e.Row.Cells[6].Visible = false;
        }
        #endregion
    }


    private void UploadFiles(decimal expid)
    {
        // Check File Prasent or not  
        if (ploadexpfile.HasFiles)
        {
            StringBuilder sbinsertmain = new StringBuilder();
            StringBuilder sbinsertValues = new StringBuilder();
            sbinsertmain.Append("Insert into tbl_Expense_Travel_UploadedFiles(exp_trip_id,filename,is_exp_trip,fileid) Values  ");
            string serverfolder = string.Empty;
            string serverpath = string.Empty;
            serverfolder = Server.MapPath("");
            String strfileName = "";
            string[] strdate;
            string strfromDate = "";
            #region date formatting
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
            }


            #endregion
            Int32 ifilecnt = 1;


            foreach (HttpPostedFile postfiles in ploadexpfile.PostedFiles)
            {
                strfileName = "";
                strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + postfiles.FileName.Replace("'", "''");
                postfiles.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["trvlingExpfiles"]).Trim()), strfileName));

                if (Convert.ToString(sbinsertValues).Trim() == "")
                    sbinsertValues.Append(" ( " + expid + ",'" + strfileName + "','expenses'," + ifilecnt + " ) ");
                else
                    sbinsertValues.Append(" , ( " + expid + ",'" + strfileName + "','expenses'," + ifilecnt + " ) ");
                ifilecnt += 1;
            }
            if (Convert.ToString(sbinsertValues).Trim() != "")
            {
                Int32 irecupdate = 0;
                irecupdate = spm.add_Regularise_Attendance_emp(Convert.ToString(sbinsertmain).Trim() + Convert.ToString(sbinsertValues).Trim());

            }

        }
    }
    public void getExpenseUploadedFiles()
    {

        DataTable dtTrDetails = new DataTable();
        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Sp_getExp_trip_uploadedFiles";

        spars[1] = new SqlParameter("@exp_trvl", SqlDbType.VarChar);
        spars[1].Value = "expenses";

        spars[2] = new SqlParameter("@expid", SqlDbType.Decimal);
        spars[2].Value = hdnexp_id.Value;

        spars[3] = new SqlParameter("@exp_sr_no", SqlDbType.Int);
        if (Convert.ToString(hdnfileid.Value).Trim() != "")
            spars[3].Value = hdnfileid.Value;
        else
            spars[3].Value = DBNull.Value;


        dtTrDetails = spm.getDataList(spars, "SP_GETALL_Expense_DETAILS");

        gvuploadedFiles.DataSource = null;
        gvuploadedFiles.DataBind();
        if (dtTrDetails.Rows.Count > 0)
        {
            gvuploadedFiles.DataSource = dtTrDetails;
            gvuploadedFiles.DataBind();
        }
    }

    private void getExpensedtls_from_temp()
    {
        DataTable dtTrDetails = new DataTable();
        //dtTrDetails = spm.getExpenseDetails_Fromtemp(txtEmpCode.Text,Convert.ToDecimal(hdnTripid.Value));
        DataSet dsExpDtls = new DataSet();
        dsExpDtls = spm.getExpenseDetails_Fromtemp_Dataset(txtEmpCode.Text, Convert.ToDecimal(hdnTripid.Value));


        gvexpensdtls.DataSource = null;
        gvexpensdtls.DataBind();

        if (dsExpDtls.Tables[0].Rows.Count > 0)
        {
            // dsExpDtls.Tables[0].Rows.Add();
            Decimal dpaidbyComp = 0;
            Decimal dpaidbyEmp = 0;
            Decimal dTotClaimAmt = 0;
            Int32 irow = 0;
            for (irow = 0; irow < dsExpDtls.Tables[0].Rows.Count; irow++)
            {
                if (Convert.ToString(dsExpDtls.Tables[0].Rows[irow]["paid_by_comp"]).Trim() != "")
                {
                    dpaidbyComp += Convert.ToDecimal(dsExpDtls.Tables[0].Rows[irow]["paid_by_comp"]);
                }

                if (Convert.ToString(dsExpDtls.Tables[0].Rows[irow]["paid_emp"]).Trim() != "")
                {
                    dpaidbyEmp += Convert.ToDecimal(dsExpDtls.Tables[0].Rows[irow]["paid_emp"]);
                }

                if (Convert.ToString(dsExpDtls.Tables[0].Rows[irow]["totamt"]).Trim() != "")
                {
                    dTotClaimAmt += Convert.ToDecimal(dsExpDtls.Tables[0].Rows[irow]["totamt"]);
                }

            }
            dsExpDtls.Tables[0].Rows.Add();
            dsExpDtls.Tables[0].Rows[irow]["paid_by_comp"] = Convert.ToString(dpaidbyComp);
            dsExpDtls.Tables[0].Rows[irow]["paid_emp"] = Convert.ToString(dpaidbyEmp);
            dsExpDtls.Tables[0].Rows[irow]["totamt"] = Convert.ToString(dTotClaimAmt);

            dtTrDetails = dsExpDtls.Tables[0];
            txtAdvance.Text = Convert.ToString(Session["AdvanceTaken"]);
            txtLessAdvTaken.Text = txtAdvance.Text;
            txtLessAdvTaken.Enabled = false;
            gvexpensdtls.DataSource = dtTrDetails;
            gvexpensdtls.DataBind();
        }
    }
    private void getMainExpensedtls()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.getMainExpenseDetails(Convert.ToDecimal(hdnexp_id.Value));

        gvexpensdtls.DataSource = null;
        gvexpensdtls.DataBind();

        if (dtTrDetails.Rows.Count > 0)
        {
            gvexpensdtls.DataSource = dtTrDetails;
            gvexpensdtls.DataBind();
        }
    }
    private void InsertExp_DatatoTempTables()
    {
        try
        {

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_Exp_insert_mainData_toTempTabls";

            spars[1] = new SqlParameter("@expid", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDecimal(hdnexp_id.Value);

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            DataTable dtMaxTripID = spm.InsertorUpdateData(spars, "SP_GETALL_Expense_DETAILS");

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }


    }
    private void InsertExp_DatatoTempTables_trvl()
    {
        try
        {

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_Exp_insert_mainData_toTempTabls_trvl";

            spars[1] = new SqlParameter("@trip_id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDecimal(hdnTripid.Value);

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            DataTable dtMaxTripID = spm.InsertorUpdateData(spars, "SP_GETALL_Expense_DETAILS");

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }


    }
    private void SessiontoControl()
    {
        txtTriptype.Text = Convert.ToString(Session["TravelType"]);
        txtFromdate.Text = Convert.ToString(Session["Fromdate"]);
        txtToDate.Text = Convert.ToString(Session["Todate"]);
        txtEmpCode.Text = Convert.ToString(Session["ReqEmpCode"]);
        txtReason.Text = Convert.ToString(Session["reasntrvl"]);
        hflGrade.Value = Convert.ToString(Session["Grade"]);
        hflEMPAGrade.Value = Convert.ToString(Session["EMPAGrade"]);
        hdnDestnation.Value = Convert.ToString(Session["Location"]);
        lstTripType.SelectedValue = Convert.ToString(Session["TripTypeId"]);

        hdnAcctripid.Value = Convert.ToString(Session["TripID"]);
        hdnLcalTripid.Value = Convert.ToString(Session["TripID"]);
        hdnDeptPlace.Value = Convert.ToString(Session["DestLocation"]);
        hdnTraveltypeid.Value = Convert.ToString(Session["Tr_type_id"]);
        hdnTraveltypeid.Value = lstTripType.SelectedValue;
        hdnTripid.Value = Convert.ToString(Session["TripidTR"]);

    }

    #endregion

    #region ModifyTravelMethods
    public void getMainTravelDetails()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetMain_expTravelDetails(Convert.ToInt32(hdnexp_id.Value));

        dgTravelRequest.DataSource = null;
        dgTravelRequest.DataBind();

        if (dtTrDetails.Rows.Count > 0)
        {
            hdnTravelDtlsId.Value = Convert.ToString(dtTrDetails.Rows[0]["Exptrip_dtls_id"]);

            for (int i = 0; i < dtTrDetails.Rows.Count; i++)
            {

                hdnTravelDtlsId.Value = Convert.ToString(dtTrDetails.Rows[i]["Exptrip_dtls_id"]).Trim();

            }

            dgTravelRequest.DataSource = dtTrDetails;
            dgTravelRequest.DataBind();
        }
    }

    public void getMainAccomodationDetails()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetMain_ExpAccomodationDetails(Convert.ToInt32(hdnexp_id.Value));

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
        dtTrDetails = spm.GetMain_ExpLocalTrvlDetails(Convert.ToInt32(hdnexp_id.Value));

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
            //hdncomp_code.Value = Convert.ToString(dtTrDetails.Rows[0]["comp_code"]);
            //hdndept_id.Value = Convert.ToString(dtTrDetails.Rows[0]["dept_id"]);

        }
    }
    #endregion


    protected void lnkCalculate_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(txtTotAmtClaimed.Text).Trim() != "")
        {
            decimal Daily_Halting_allowance = 0, TotalAmount_Claimed = 0, LessAdvanceTaken = 0, Net_Pay_Company = 0, Net_Pay_Employee = 0;
            if (Convert.ToString(txtTotAmtClaimed.Text).Trim() != "")
                TotalAmount_Claimed = Convert.ToDecimal(txtTotAmtClaimed.Text);

            if (Convert.ToString(txtLessAdvTaken.Text).Trim() != "")
                LessAdvanceTaken = Convert.ToDecimal(txtLessAdvTaken.Text);

            if (Convert.ToString(txtnetPaybltoComp.Text).Trim() != "")
                Net_Pay_Company = Convert.ToDecimal(txtnetPaybltoComp.Text);

            if (Convert.ToString(txtnetPaybltoEmp.Text).Trim() != "")
                Net_Pay_Employee = Convert.ToDecimal(txtnetPaybltoEmp.Text);
            if (LessAdvanceTaken > 0)
            {
                Net_Pay_Company = LessAdvanceTaken - TotalAmount_Claimed;
                if (Net_Pay_Company > 0)
                {
                    txtnetPaybltoComp.Text = Convert.ToString(Net_Pay_Company);
                    Net_Pay_Employee = 0;
                    txtnetPaybltoEmp.Text = Convert.ToString(Net_Pay_Employee);
                }
                else
                {
                    Net_Pay_Company = 0;
                    txtnetPaybltoComp.Text = Convert.ToString(Net_Pay_Company);
                }
            }
            else
            {
                Net_Pay_Employee = LessAdvanceTaken - TotalAmount_Claimed;
                if (Net_Pay_Employee < 0)
                {
                    txtnetPaybltoEmp.Text = Convert.ToString(Net_Pay_Employee);
                    Net_Pay_Company = 0;
                    txtnetPaybltoComp.Text = Convert.ToString(Net_Pay_Company);
                }
                else
                {
                    Net_Pay_Employee = 0;
                    txtnetPaybltoEmp.Text = Convert.ToString(Net_Pay_Employee);
                }
            }
        }
        else
        {
            return;
        }
    }

    protected void gvuploadedFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            //LinkButton lnkDeleteexpFile
            if (Convert.ToString(hdnmainexpStatus.Value).Trim() == "1" && Convert.ToString(hdn_apprStatus.Value).Trim() == "Approved")
            {
                e.Row.Cells[1].Visible = false;
            }
            if (Convert.ToString(hdnmainexpStatus.Value).Trim() == "2")
            {
                e.Row.Cells[1].Visible = false;
            }
            if (Convert.ToString(hdnmainexpStatus.Value).Trim() == "3" || Convert.ToString(hdnmainexpStatus.Value).Trim() == "4")
            {
                e.Row.Cells[1].Visible = false;
            }
        }
    }
    #region Travel Details
    ////protected void txtFromdate_Trvl_TextChanged(object sender, EventArgs e)
    ////{
    ////    if (Convert.ToString(txtToDate_Trvl.Text).Trim() == "")
    ////    {
    ////        return;
    ////    }


    ////    DateValidations();
    ////}
    ////protected void txtToDate_Trvl_TextChanged(object sender, EventArgs e)
    ////{
    ////    if (Convert.ToString(txtFromdate_Trvl.Text).Trim() == "")
    ////    {
    ////        lblmessage.Text = "From date  cannot be blank";
    ////        return;
    ////    }


    ////    DateValidations();
    ////}

    protected void DateValidations()
    {
        if (Convert.ToString(txtFromdate_Trvl.Text).Trim() == "")
            return;

        if (Convert.ToString(txtToDate_Trvl.Text).Trim() == "")
            return;


        string[] tr_strdate;
        string tr_strfromDate = "";
        string tr_strToDate = "";

        #region Exp.TraelRequestdate formatting

        if (Convert.ToString(txtFromdate_Trvl.Text).Trim() != "")
        {
            tr_strdate = Convert.ToString(hdnfromdate_Trvl.Value).Trim().Split('/');
            tr_strfromDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
        }
        if (Convert.ToString(txtToDate_Trvl.Text).Trim() != "")
        {
            tr_strdate = Convert.ToString(hdnTodate_Trvl.Value).Trim().Split('/');
            tr_strToDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
        }

        #endregion

        lblmessage.Text = "";
        string message = "";
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

        #endregion
        DataSet dttraveletails = new DataSet();
        if (Convert.ToString(txtFromdate_Trvl.Text).Trim() != "" && Convert.ToString(txtToDate_Trvl.Text).Trim() != "")
        {
            dttraveletails = spm.Get_ExpTravel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode_Trvl.Text, hdnTripid.Value, "ExpTrvlDtls");

            if (dttraveletails.Tables[1].Rows.Count > 0)
            {
                message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);
            }

            if (Convert.ToString(message).Trim() != "")
            {
                lblmessage.Text = Convert.ToString(message).Trim();
                lblmessage.Visible = true;
                txtFromdate_Trvl.Text = "";
                txtToDate_Trvl.Text = "";
            }

        }
        get_Trvl_FoodEligibilty();

    }

    private void get_Trvl_FoodEligibilty()
    {
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_trvl_foodEligibility";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode_Trvl.Text).Trim();

        spars[2] = new SqlParameter("@trip_mode_id", SqlDbType.VarChar);
        spars[2].Value = Convert.ToString(lstTravelMode.SelectedValue).Trim();

        DataSet dsfoodEligibity = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");
        txtFoodEligibilty.Text = "0.00";
        if (dsfoodEligibity != null)
        {
            if (dsfoodEligibity.Tables[0].Rows.Count > 0)
            {
                txtFoodEligibilty.Text = Convert.ToString(dsfoodEligibity.Tables[0].Rows[0]["Eligibility"]).Trim();
            }
        }

    }

    //protected void lstOrigin_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtOrigin.Text = lstOrigin.SelectedItem.Text;
    //    PopupControlExtender1.Commit(lstOrigin.SelectedItem.Text);
    //}
    //protected void lstDestination_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtDestination.Text = lstDestination.SelectedItem.Text;
    //    PopupControlExtender4.Commit(lstDestination.SelectedItem.Text);
    //}
    protected void lstTravelMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTravelMode.Text = lstTravelMode.SelectedItem.Text;
        //PopupControlExtender3.Commit(lstTravelMode.SelectedItem.Text);

        SetTravelDeviation();
        txtDeviation.Text = hdnDeviation.Value;
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetDeviation('" + hdnDeviation.Value + "');", true);
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
    protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";

        #region Check Validations

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

        if (DDLOrigin.SelectedItem.Text == "0" || DDLOrigin.SelectedItem.Text == "")
        {
            lblmessage.Text = "Please Select Correct Departure Place.";
            return;
        }
        if (DDLDestination.SelectedItem.Text == "0" || DDLDestination.SelectedItem.Text == "")
        {
            lblmessage.Text = "Please Select Correct Arrival Place.";
            return;
        }
        if (DDLDestination.SelectedItem.Text == DDLOrigin.SelectedItem.Text)
        {
            lblmessage.Text = "Departure Place & Arrival Place should not be same.";
            return;
        }

        if (Convert.ToString(txtFromTime.Text) == "")
        {
            lblmessage.Text = "Please Enter Departure Time.";
            return;
        }
        if (Convert.ToString(txtToTime.Text) == "")
        {
            lblmessage.Text = "Please Enter Arrival Time.";
            return;
        }

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

        if (Convert.ToString(txtFromdate_Trvl.Text).Trim() == Convert.ToString(txtToDate_Trvl.Text).Trim())
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

        if (Convert.ToString(txtFoodAllowance.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFoodAllowance.Text).Trim().Split('.');
            /*if (Convert.ToString(strdate[1]).Trim()!="")
            {
                if (Convert.ToString(strdate[0]).Trim() == "")
                {
                    txtFoodAllowance.Text = "0";
                    lblmessage.Text = "Please enter Food Allowance.";
                    return;
                }
            }*/
            if (strdate.Length > 2)
            {
                txtFoodAllowance.Text = "0";
                lblmessage.Text = "Please enter Food Allowance.";
                return;
            }
            Decimal dfoodAllowance = 0;
            dfoodAllowance = Convert.ToDecimal(txtFoodAllowance.Text);
            //if (dfoodAllowance == 0)
            //{
            //    lblmessage.Text = "Please enter Food Allowance.";
            //    return;
            //}
            Decimal dfoodEligibilty = 0;
            if (Convert.ToString(txtFoodEligibilty.Text).Trim() != "")
                dfoodEligibilty = Convert.ToDecimal(txtFoodEligibilty.Text);

            //if (dfoodAllowance > dfoodEligibilty)
            //{

            //    lblmessage.Text = "Food Allowance more than Eligiblity not allowed.";
            //    return;
            //}
        }

        #endregion

        strfromDate = "";
        strToDate = "";


        if (Convert.ToString(Session["chkTrvlAccLocalTrvlbtnStatus"]).Trim() != "")
        {
            Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value);
        }
        Session["chkTrvlAccLocalTrvlbtnStatus"] = "Travel Expenses button Event is Submitted";


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

        if (DDLOrigin.SelectedItem.Text == "" || DDLOrigin.SelectedItem.Text == "0")
        {
            lblmessage.Text = "Place of Origin cannot be blank";
            return;
        }

        if (DDLDestination.SelectedItem.Text == "" || DDLDestination.SelectedItem.Text == "0")
        {
            lblmessage.Text = "Place of Destination cannot be blank";
            return;
        }
        #endregion

        #region InsertTravelDetails

        if (Convert.ToString(hdnTripid.Value).Trim() == "")
        {
            hdnTripid.Value = "0";
        }
        if (Convert.ToString(hdntrdetailsid.Value).Trim() == "")
        {
            hdntrdetailsid.Value = "0";
        }

        Decimal dactualfare = 0;
        string stype = "insert";
        if (trvldeatils_btnSave.Text == "Update")
            stype = "update";

        if (Convert.ToString(txtfare.Text).Trim() != "")
            dactualfare = Math.Round(Convert.ToDecimal(txtfare.Text), 2);

        decimal dfoodallowance = 0;
        if (Convert.ToString(txtFoodAllowance.Text).Trim() != "")
        {
            dfoodallowance = Math.Round(Convert.ToDecimal(txtFoodAllowance.Text), 2);

        }
        Session["Destination"] = DDLDestination.SelectedItem.Text;
        if (hdntrdetailsid.Value == "")
            hdntrdetailsid.Value = "0";
        string trvl_status = "";
        if (chkCOS_Trvl.Checked)
            trvl_status = "Booked";
        spm.InsertExpenseTravelDetails(Convert.ToInt32(hdnTryiptypeid.Value), Convert.ToInt32(lstTravelMode.SelectedValue), strfromDate, DDLOrigin.SelectedItem.Text, txtFromTime.Text, strToDate, DDLDestination.SelectedItem.Text, txtDeviation.Text, Convert.ToString(txtToTime.Text), txtEmpCode_Trvl.Text, Convert.ToDecimal(hdnTripid.Value), hdnCOS.Value, dactualfare, txtRemark.Text, stype, Convert.ToDecimal(hdntrdetailsid.Value), dfoodallowance, trvl_status);
        DivTrvl.Visible = false;
        btnTra_Details.Text = "+";
        GetExpenseTravelDetails();
        getExpensedtls_from_temp();
        CalculateClaimAmt();

        //Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value);
        // Response.Redirect("~/procs/ExpenseWithoutTravlReqst.aspx");
        #endregion
        //}
        //else
        //{
        //    #region UpdateTravelDetails
        //    //spm.UpdateTravelDetails(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdntrdetailsid.Value), Convert.ToInt32(lstTravelMode.SelectedValue), strfromDate, txtOrigin.Text, strToDate, txtDestination.Text, txtDeviation.Text, hdnCOS.Value, txtRequirememt.Text, "UpdateMainTable");
        //    //Response.Redirect("~/procs/TravelRequest.aspx?tripid=" + hdnTripid.Value);
        //    #endregion
        //}

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

    protected void trvldeatils_delete_btn_Click(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";

        #region Check Validations
        //if (Check_Cities_name(txtOrigin.Text) == false)
        //{
        //    lblmessage.Text = "Please Select Correct Departure Place.";
        //    return;
        //}
        //if (Check_Cities_name(txtDestination.Text) == false)
        //{
        //    lblmessage.Text = "Please Select Correct Arrival Place.";
        //    return;
        //}
        //if (Convert.ToString(txtOrigin.Text).Trim() == Convert.ToString(txtDestination.Text).Trim())
        //{
        //    lblmessage.Text = "Departure Place & Arrival Place should not be same.";
        //    return;
        //}

        if (DDLOrigin.SelectedItem.Text == "" || DDLOrigin.SelectedItem.Text == "0")
        {
            lblmessage.Text = "Please Select Correct Departure Place.";
            return;
        }
        if (DDLDestination.SelectedItem.Text == "" || DDLDestination.SelectedItem.Text == "0")
        {
            lblmessage.Text = "Please Select Correct Arrival Place.";
            return;
        }
        if (DDLOrigin.SelectedItem.Text == DDLDestination.SelectedItem.Text)
        {
            lblmessage.Text = "Departure Place & Arrival Place should not be same.";
            return;
        }

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

        if (Convert.ToString(txtFromdate_Trvl.Text).Trim() == Convert.ToString(txtToDate_Trvl.Text).Trim())
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

        if (Convert.ToString(txtfare.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtfare.Text).Trim().Split('.');
            if (strdate.Length > 2)
            {
                txtfare.Text = "0";
            }
        }
        #endregion

        strfromDate = "";
        strToDate = "";

        if (Convert.ToString(Session["chkTrvlAccLocalTrvlbtnStatus"]).Trim() != "")
        {
            Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value);
        }

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

        if (DDLOrigin.SelectedItem.Text == "" || DDLOrigin.SelectedItem.Text == "0")
        {
            lblmessage.Text = "Place of Origin cannot be blank";
            return;
        }

        if (DDLDestination.SelectedItem.Text == "" || DDLDestination.SelectedItem.Text == "0")
        {
            lblmessage.Text = "Place of Destination cannot be blank";
            return;
        }
        #endregion

        #region InsertTravelDetails
        Decimal dactualfare = 0;
        string stype = "delete";


        if (Convert.ToString(txtfare.Text).Trim() != "")
            dactualfare = Math.Round(Convert.ToDecimal(txtfare.Text), 2);

        decimal dfoodallowance = 0;
        if (Convert.ToString(txtFoodAllowance.Text).Trim() != "")
        {
            dfoodallowance = Math.Round(Convert.ToDecimal(txtFoodAllowance.Text), 2);

        }

        Session["Destination"] = DDLDestination.SelectedItem.Text;
        spm.InsertExpenseTravelDetails(Convert.ToInt32(hdnTryiptypeid.Value), Convert.ToInt32(lstTravelMode.SelectedValue), strfromDate, DDLOrigin.SelectedItem.Text, txtFromTime.Text, strToDate, DDLDestination.SelectedItem.Text, txtDeviation.Text, Convert.ToString(txtToTime.Text), txtEmpCode.Text, Convert.ToDecimal(hdnTripid.Value), hdnCOS.Value, dactualfare, txtRemark.Text, stype, Convert.ToDecimal(hdntrdetailsid.Value), dfoodallowance, "");
        Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value);
        // Response.Redirect("~/procs/ExpenseWithoutTravlReqst.aspx");
        #endregion
        //}
    }
    protected void trvldeatils_cancel_btn_Click(object sender, EventArgs e)
    {
        DivTrvl.Visible = false;
        btnTra_Details.Text = "+";
        //Response.Redirect("~/procs/travel_Exp.aspx");
        //Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value);
    }
    #endregion Travel Details
    #region Accommodation
    //protected void lstLocation_Accm_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtLocation_Accm.Text = lstLocation_Accm.SelectedItem.Text;
    //    // get_Actual_Flats_rates();
    //    PopupControlExtender5.Commit(lstLocation_Accm.SelectedItem.Text);
    //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "validateTripType();", true);
    //}
    ////protected void txtFromdate_Accm_TextChanged(object sender, EventArgs e)
    ////{
    ////    if (Convert.ToString(txtFromdate_Accm.Text).Trim() == "")
    ////    {
    ////        return;
    ////    }
    ////    get_Actual_DayDiff();
    ////    get_Actual_Flats_rates();
    ////    DateValidations_Accm();
    ////}
    ////protected void txtToDate_Accm_TextChanged(object sender, EventArgs e)
    ////{
    ////    if (Convert.ToString(txtFromdate_Accm.Text).Trim() == "")
    ////    {
    ////        lblmessage.Text = "From date  cannot be blank";
    ////        return;
    ////    }
    ////    get_Actual_DayDiff();
    ////    get_Actual_Flats_rates();
    ////    DateValidations_Accm();
    ////}
    ////protected void txtnoofDays_TextChanged(object sender, EventArgs e)
    ////{
    ////    get_Actual_DayDiff();
    ////}
    protected void lstPaidBy_Accm_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtPaidBy_Accm.Text = lstPaidBy_Accm.SelectedItem.Text;
        //PopupControlExtender6.Commit(lstPaidBy_Accm.SelectedItem.Text);
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
            if (txtCheck_CorrectAmtount(txtFlatChg_Accm) == false)
                getDeviation_For_Geuest_Hose_withoutFood();
        }

    }
    protected void txtaddintionalExpens_Accm_TextChanged(object sender, EventArgs e)
    {
        if (rdoFoodAccomodation.Checked == true)
        {
            if (txtCheck_CorrectAmtount(txtFlatChg_Accm) == false)
                getDeviation_For_Geuest_Hose_withoutFood();
        }
        if (rdoOwnArgmnet.Checked == true)
        {
            txtCheck_CorrectAmtount(txtaddintionalExpens_Accm);
        }
        if (rdoFood.Checked == true)
        {
            getdeviation_Guesthouse_wihtFood();
        }

    }
    protected void lstFlatPaid_Accm_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFlatPaid_Accm.Text = lstFlatPaid_Accm.SelectedItem.Text;
        //  PopupControlExtender4.Commit(lstFlatPaid_Accm.SelectedItem.Text);

    }
    protected void txtFlatChg_Accm_TextChanged(object sender, EventArgs e)
    {
        if (rdoFoodAccomodation.Checked == true)
        {

            if (txtCheck_CorrectAmtount(txtFlatChg_Accm) == false)
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
            txtCheck_CorrectAmtount(txtFlatChg_Accm);
        }


        #region if Own Arrangement then Don't allow to enter more than eligibility
        if (rdoOwnArgmnet.Checked == true)
        {

            Double dfltaexlibilty = 0;
            Double dtxtactchrs = 0;

            if (Convert.ToString(txtFlatChg_Accm.Text).Trim() != "")
                dtxtactchrs = Convert.ToDouble(txtFlatChg_Accm.Text);

            if (Convert.ToString(txtFlatElg_Accm.Text).Trim() != "")
                dfltaexlibilty = Convert.ToDouble(txtFlatElg_Accm.Text);

            //if (dtxtactchrs > dfltaexlibilty)
            //{
            //    txtFlatChg.Text = Convert.ToString(dfltaexlibilty);
            //}

        }
        #endregion
    }

    protected void accmo_btnSave_Click(object sender, EventArgs e)
    {

        //if (Check_Cities_name(txtLocation_Accm.Text) == false)
        //{
        //    lblmessage.Text = "Please Select Correct Location.";
        //    return;
        //}
        if (DDL_Location_Accm.SelectedItem.Text == "" || DDL_Location_Accm.SelectedItem.Text == "0")
        {
            lblmessage.Text = "Please Select Correct Location.";
            return;
        }

        add_updateDelete_Accomodations("InsertTempTable");
        //InsertTempTable

    }
    protected void accmo_cancel_btn_Click(object sender, EventArgs e)
    {
        DivAccm.Visible = false;
        trvl_accmo_btn.Text = "+";
        //Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value);
    }
    protected void accmo_delete_btn_Click(object sender, EventArgs e)
    {
        add_updateDelete_Accomodations("deleteTempTable");
    }
    private void add_updateDelete_Accomodations(string strtype)
    {
        string[] strdate;

        #region Check All Fields Blank
        //  lblmessage.Text = "";
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

        if (Convert.ToString(txtnoofDays.Text).Trim() == "" || Convert.ToString(txtnoofDays.Text).Trim() == "0" || Convert.ToString(txtnoofDays.Text).Trim() == "0.00")
        {
            lblmessage.Text = "Please enter correct Actual stay duration (in days)";
            return;
        }

        //if (Convert.ToString(txtLocation_Accm.Text).Trim() == "")  
        //{
        //    lblmessage.Text = "Please enter Location For Travel";
        //    return;
        //}

        if (DDL_Location_Accm.SelectedItem.Text == "0" || DDL_Location_Accm.SelectedItem.Text == "")
        {
            lblmessage.Text = "Please enter Location For Travel";
            return;
        }

        if (Convert.ToString(txtRemarks_Accm.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Remarks";
            return;
        }

        #region if Booked Through Hotel

        if (Convert.ToString(txtAcctype.Text).Trim() == "Hotel")
        {
            if (Convert.ToString(txtPaidBy_Accm.Text).Trim() == "")
            {
                lblmessage.Text = "Please select paid by";
                return;
            }
            if (Convert.ToString(txtCharges.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtCharges.Text).Trim().Split('.');
                if (strdate.Length > 2)
                {
                    lblmessage.Text = "Please enter correct Amount for Actual";
                    txtCharges.Text = "0";
                    return;
                }
                if (Convert.ToString(txtEligibility_Accm.Text).Trim() != "")
                {
                    if (Convert.ToString(txtEligibility_Accm.Text).Trim() != "0.00" || Convert.ToString(txtEligibility_Accm.Text).Trim() != "0")
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
        else
        {
            txtPaidBy_Accm.Text = "Employee";
        }
        #endregion

        if (Convert.ToString(txtFlatChg_Accm.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFlatChg_Accm.Text).Trim().Split('.');
            if (strdate.Length > 2)
            {
                txtFlatChg_Accm.Text = "0";
                lblmessage.Text = "Please enter correct Flat Rate Amount";
                Session["chkTrvlAccLocalTrvlbtnStatus"] = "";
                return;
            }
            if (Convert.ToString(txtFlatElg_Accm.Text).Trim() != "")
            {
                if (Convert.ToString(txtFlatElg_Accm.Text).Trim() != "0.00" || Convert.ToString(txtFlatElg_Accm.Text).Trim() != "0")
                {
                    if (Convert.ToString(txtFlatChg_Accm.Text).Trim() == "")
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


        if (rdoOwnArgmnet.Checked == true)
        {

            if (Convert.ToString(txtaddintionalExpens_Accm.Text).Trim() != "" && Convert.ToString(txtaddintionalExpens_Accm.Text).Trim() != "0")
            {
                txtAdditional_exp_deviation_Accm.Text = "Yes";
            }

        }
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


        SqlParameter[] spars = new SqlParameter[25];

        #region Set SQL Parameters
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = strtype;
        // spars[0].Value = "InsertTempTable";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode_Accm.Text).Trim();

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
        spars[6].Value = DDL_Location_Accm.SelectedItem.Text;

        spars[7] = new SqlParameter("@local_accomodation_remarks", SqlDbType.VarChar);
        spars[7].Value = Convert.ToString(txtRemarks_Accm.Text).Trim();

        spars[8] = new SqlParameter("@Accomodation_type", SqlDbType.VarChar);
        if (rdoAccomodation.Checked == true)
            spars[8].Value = Convert.ToString(rdoAccomodation.Text).Trim();
        else if (rdoFood.Checked == true)
            spars[8].Value = Convert.ToString(rdoFood.Text).Trim();
        else if (rdoFoodAccomodation.Checked == true)
            spars[8].Value = Convert.ToString(rdoFoodAccomodation.Text).Trim();
        else if (rdoOwnArgmnet.Checked == true)
            spars[8].Value = Convert.ToString(rdoOwnArgmnet.Text).Trim();
        //spars[8].Value = Convert.ToString(txtAcctype.Text).Trim();

        spars[9] = new SqlParameter("@ActualPaid_by", SqlDbType.VarChar);
        if (Convert.ToString(txtPaidBy_Accm.Text).Trim() != "")
            spars[9].Value = Convert.ToString(txtPaidBy_Accm.Text).Trim();

        spars[10] = new SqlParameter("@ActualCharges", SqlDbType.Decimal);
        if (Convert.ToString(txtCharges.Text).Trim() != "")
            spars[10].Value = Math.Round(Convert.ToDecimal(txtCharges.Text), 2);

        spars[11] = new SqlParameter("@ActualDeviation", SqlDbType.VarChar);
        if (Convert.ToString(txtDeviation_Accm.Text).Trim() != "")
            spars[11].Value = Convert.ToString(txtDeviation_Accm.Text).Trim();

        spars[12] = new SqlParameter("@ActualEligibility", SqlDbType.Decimal);
        if (Convert.ToString(txtEligibility_Accm.Text).Trim() != "")
            spars[12].Value = Convert.ToDecimal(txtEligibility_Accm.Text);

        spars[13] = new SqlParameter("@FLatPaid_by", SqlDbType.VarChar);
        if (Convert.ToString(txtFlatPaid_Accm.Text).Trim() != "")
            spars[13].Value = Convert.ToString(txtFlatPaid_Accm.Text).Trim();

        spars[14] = new SqlParameter("@FlatCharges", SqlDbType.Decimal);
        if (Convert.ToString(txtFlatChg_Accm.Text).Trim() != "")
            spars[14].Value = Math.Round(Convert.ToDecimal(txtFlatChg_Accm.Text), 2);

        spars[15] = new SqlParameter("@FLatDeviation", SqlDbType.VarChar);
        if (Convert.ToString(txtFlatDev_Accm.Text).Trim() != "")
            spars[15].Value = Convert.ToString(txtFlatDev_Accm.Text).Trim();

        spars[16] = new SqlParameter("@FlatEligibility", SqlDbType.Decimal);
        if (Convert.ToString(txtFlatElg_Accm.Text).Trim() != "")
            spars[16].Value = Convert.ToDecimal(txtFlatElg_Accm.Text);

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
            spars[20].Value = Convert.ToString(txtAdditionalFoodExp_emp.Text).Trim();
        else
            spars[20].Value = DBNull.Value;

        spars[21] = new SqlParameter("@AdditionalFoodExp_exp", SqlDbType.Decimal);
        if (Convert.ToString(txtAdditionalFoodExp_exp.Text).Trim() != "")
            spars[21].Value = Math.Round(Convert.ToDecimal(txtAdditionalFoodExp_exp.Text), 2);
        else
            spars[21].Value = DBNull.Value;

        spars[22] = new SqlParameter("@AdditionalExp_exp_deviation", SqlDbType.VarChar);
        if (Convert.ToString(txtAdditional_exp_deviation_Accm.Text).Trim() != "")
            spars[22].Value = Convert.ToString(txtAdditional_exp_deviation_Accm.Text).Trim();
        else
            spars[22].Value = DBNull.Value;

        spars[23] = new SqlParameter("@additnal_flat_exps", SqlDbType.Decimal);
        if (Convert.ToString(txtaddintionalExpens_Accm.Text).Trim() != "")
            spars[23].Value = Math.Round(Convert.ToDecimal(txtaddintionalExpens_Accm.Text), 2);
        else
            spars[23].Value = DBNull.Value;

        spars[24] = new SqlParameter("@noofdays", SqlDbType.Decimal);
        if (Convert.ToString(txtnoofDays.Text).Trim() != "")
            spars[24].Value = Math.Round(Convert.ToDecimal(txtnoofDays.Text), 2);
        else
            spars[24].Value = DBNull.Value;


        #endregion

        DataTable dt = spm.InsertorUpdateData(spars, "SP_insert_Expense_accomodation_details");
        //Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value);


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
        DivAccm.Visible = false;
        trvl_accmo_btn.Text = "+";
        GetExpenseAccomodationDetails();
        getExpensedtls_from_temp();
        CalculateClaimAmt();

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

    public void get_Actual_DayDiff()
    {

        String[] strdate;
        strdate = Convert.ToString(txtnoofDays.Text).Trim().Split('.');
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
    public void get_Actual_Flats_rates()
    {

        txtEligibility_Accm.Text = "0";
        hdnactualEligbility.Value = "0";
        txtFlatElg_Accm.Text = "0";
        hdnflatEligbility.Value = "0";



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

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[7];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_flat_actual_rate_Accomdation";

        spars[1] = new SqlParameter("@cityname", SqlDbType.VarChar);
        spars[1].Value = DDL_Location_Accm.SelectedItem.Text;//lstLocation.SelectedValue; 

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
        spars[6].Value = strToDate;


        dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");
        Decimal dAmount = 0;
        if (dsTrDetails.Tables[1].Rows.Count > 0)
        {
            if (Convert.ToString(dsTrDetails.Tables[1].Rows[0]["Amount"]).Trim() != "")
                dAmount = Convert.ToDecimal(dsTrDetails.Tables[1].Rows[0]["Amount"]);

            txtFlatElg_Accm.Text = Convert.ToString(dAmount).Trim();
            hdnflatEligbility.Value = Convert.ToString(dAmount).Trim();

            if (Convert.ToString(txtnoofDays.Text).Trim() != "" && Convert.ToString(txtnoofDays.Text).Trim() != "0" && Convert.ToString(txtnoofDays.Text).Trim() != "0.00")
            {
                txtFlatElg_Accm.Text = Convert.ToString(Math.Round(dAmount * Convert.ToDecimal(txtnoofDays.Text), 2)).Trim();
                hdnflatEligbility.Value = Convert.ToString(Math.Round(dAmount * Convert.ToDecimal(txtnoofDays.Text), 2)).Trim();
            }

        }
        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            dAmount = 0;
            if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Amount"]).Trim() != "")
                dAmount = Convert.ToDecimal(dsTrDetails.Tables[0].Rows[0]["Amount"]);

            txtEligibility_Accm.Text = Convert.ToString(dAmount).Trim();
            hdnactualEligbility.Value = Convert.ToString(dAmount).Trim();

            //txtEligibility.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Amount"]).Trim();
            //hdnactualEligbility.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Amount"]).Trim();

            if (Convert.ToString(txtnoofDays.Text).Trim() != "" && Convert.ToString(txtnoofDays.Text).Trim() != "0" && Convert.ToString(txtnoofDays.Text).Trim() != "0.00")
            {
                txtEligibility_Accm.Text = Convert.ToString(Math.Round(dAmount * Convert.ToDecimal(txtnoofDays.Text), 2)).Trim();
                hdnactualEligbility.Value = Convert.ToString(Math.Round(dAmount * Convert.ToDecimal(txtnoofDays.Text), 2)).Trim();
            }

        }

        if (Convert.ToString(txtEligibility_Accm.Text).Trim() == "")
        {
            txtEligibility_Accm.Text = "0";
            hdnactualEligbility.Value = "0";
        }
        if (Convert.ToString(txtFlatElg_Accm.Text).Trim() == "")
        {
            txtFlatElg_Accm.Text = "0";
            hdnflatEligbility.Value = "0";
        }



    }


    private void getDeviation_For_Geuest_Hose_withoutFood()
    {
        Double dactexlibilty = 0;
        Double dtxtactchrs = 0;
        Double dtxtaddexp = 0;

        if (txtCheck_CorrectAmtount(txtFlatElg_Accm) == false)
        {
            if (Convert.ToString(txtFlatElg_Accm.Text).Trim() != "")
                dactexlibilty = Convert.ToDouble(txtFlatElg_Accm.Text);
        }
        else
        {
            return;
        }

        if (txtCheck_CorrectAmtount(txtaddintionalExpens_Accm) == false)
        {
            if (Convert.ToString(txtaddintionalExpens_Accm.Text).Trim() != "")
                dtxtaddexp = Convert.ToDouble(txtaddintionalExpens_Accm.Text);
        }
        else
        {
            return;
        }

        if (txtCheck_CorrectAmtount(txtFlatChg_Accm) == false)
        {
            if (Convert.ToString(txtFlatChg_Accm.Text).Trim() != "")
                dtxtactchrs = Convert.ToDouble(txtFlatChg_Accm.Text);
        }
        else
        {
            return;
        }
        if (((dtxtactchrs + dtxtaddexp) - dactexlibilty) > 0)
            //  if (Convert.ToString(txtFlatChg.Text).Trim() != "" && Convert.ToString(txtFlatChg.Text).Trim()!="0")
            txtAdditional_exp_deviation_Accm.Text = "Yes";
        else
            txtAdditional_exp_deviation_Accm.Text = "No";
    }
    private void getdeviation_Guesthouse_wihtFood()
    {
        if (rdoFood.Checked == true)
        {

            txtAdditional_exp_deviation_Accm.Text = "No";
            if (txtCheck_CorrectAmtount(txtaddintionalExpens_Accm) == false)
            {
                if (Convert.ToString(txtaddintionalExpens_Accm.Text).Trim() != "")
                {
                    if (Convert.ToDecimal(txtaddintionalExpens_Accm.Text) > 0)
                        txtAdditional_exp_deviation_Accm.Text = "Yes";
                }
            }
            else
            {
                return;
            }
            if (txtCheck_CorrectAmtount(txtFlatChg_Accm) == false)
            {
                if (Convert.ToString(txtFlatChg_Accm.Text).Trim() != "")
                {
                    if (Convert.ToDecimal(txtFlatChg_Accm.Text) > 0)
                        txtAdditional_exp_deviation_Accm.Text = "Yes";
                }
            }
            else
            {
                return;
            }

        }
    }
    protected void DateValidations_Accm()
    {
        if (Convert.ToString(txtFromdate_Accm.Text).Trim() == "")
            return;

        if (Convert.ToString(txtToDate_Accm.Text).Trim() == "")
            return;


        string[] tr_strdate;
        string tr_strfromDate = "";
        string tr_strToDate = "";

        #region Exp.TraelRequestdate formatting

        if (Convert.ToString(txtFromdate_Accm.Text).Trim() != "")
        {
            tr_strdate = Convert.ToString(hdnfromdate_Accm.Value).Trim().Split('/');
            tr_strfromDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
        }
        if (Convert.ToString(txtToDate_Accm.Text).Trim() != "")
        {
            tr_strdate = Convert.ToString(hdnTodate_Accm.Value).Trim().Split('/');
            tr_strToDate = Convert.ToString(tr_strdate[2]) + "-" + Convert.ToString(tr_strdate[1]) + "-" + Convert.ToString(tr_strdate[0]);
        }

        #endregion

        lblmessage.Text = "";
        string message = "";
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
        DataSet dttraveletails = new DataSet();
        if (Convert.ToString(txtFromdate_Accm.Text).Trim() != "" && Convert.ToString(txtToDate_Accm.Text).Trim() != "")
        {
            dttraveletails = spm.Get_ExpTravel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode_Accm.Text, hdnTripid.Value, "ExpAccomodatins");

            if (dttraveletails.Tables[1].Rows.Count > 0)
            {
                message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);

            }
            hdnactualdays.Value = Convert.ToString(dttraveletails.Tables[0].Rows[0]["actualAccDays"]);

            if (Convert.ToString(message).Trim() != "")
            {


                lblmessage.Text = Convert.ToString(message).Trim();
                lblmessage.Visible = true;
                txtFromdate_Accm.Text = "";
                txtToDate_Accm.Text = "";
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
        if (txtCheck_CorrectAmtount(txtFlatChg_Accm) == true)
        {
            return;
        }


        txtFlatDev_Accm.Text = "No";

        if (Convert.ToString(hdnactualdays.Value).Trim() != "")
        {
            if (Convert.ToString(hdnactualEligbility.Value).Trim() != "")
                dactexlibilty = Convert.ToDouble(hdnactualEligbility.Value);

            txtEligibility_Accm.Text = Convert.ToString(dactexlibilty);

            if (Convert.ToString(hdnflatEligbility.Value).Trim() != "")
                dfltaexlibilty = Convert.ToDouble(hdnflatEligbility.Value);

            txtFlatElg_Accm.Text = Convert.ToString(dfltaexlibilty);

        }

        if (Convert.ToString(txtCharges.Text).Trim() != "")
            dtxtactchrs = Convert.ToDouble(txtCharges.Text);

        if (Convert.ToString(txtAdditionalFoodExp_exp.Text).Trim() != "")
            dtxtactchrs += Convert.ToDouble(txtAdditionalFoodExp_exp.Text);



        if (Convert.ToString(txtEligibility_Accm.Text).Trim() != "")
            dactexlibilty = Convert.ToDouble(txtEligibility_Accm.Text);

        if (Convert.ToString(txtFlatElg_Accm.Text).Trim() != "")
            dfltaexlibilty = Convert.ToDouble(txtFlatElg_Accm.Text);

        if (Convert.ToString(txtFlatChg_Accm.Text).Trim() != "")
            dtxtflatchrs = Convert.ToDouble(txtFlatChg_Accm.Text);




        if ((dtxtactchrs - dactexlibilty) > 0)
            txtDeviation_Accm.Text = "Yes";
        else
            txtDeviation_Accm.Text = "No";
    }

    private void setControlVisibility()
    {
        if (Convert.ToString(hdnAccdtlsid.Value).Trim() == "0")
        {
            hdnIsThrughCOS.Value = "No";
            hdnAccomodationStatus.Value = "Not Booked";
        }
        txtAdditionalFoodExp_emp.Text = "Employee";

        liAccomdation_charges_1.Visible = false;
        liAccomdation_charges_Blank1.Visible = false;
        liAccomdation_charges_Blank2.Visible = false;

        //liAccomdation_charges_2.Visible = false;
        liAccomdation_charges_paidby.Visible = false;
        liAccomdation_charges_paidbyBlank1.Visible = false;
        liAccomdation_charges_paidbyBlank2.Visible = false;
        liAccomdation_charges_charges.Visible = false;
        liAccomdation_charges_chargesBlank1.Visible = false;

        //Additional Expenses
        liAddintional_exps_1.Visible = false;
        liAddintional_exps_Blank1.Visible = false;
        liAddintional_exps_Blank2.Visible = false;
        //liAddintional_exps_2.Visible = false;
        liAddintional_exps_flatpaid_1.Visible = false;
        liAddintional_exps_flatpaid_Blank1.Visible = false;
        liAddintional_exps_flatpaid_Blank2.Visible = false;
        //liAddintional_exps_flatpaid_2.Visible = false;
        //liAddintional_exps_Charges.Visible = false;
        //liAddintional_exps_eligibility.Visible = false;
        spnAdditionaExp_eligibility.Visible = false;
        txtFlatElg_Accm.Visible = false;
        txtFlatChg_Accm.Visible = false;
        spnAdditionalCharges.Visible = false;
        liAddintional_exps_deviation_1.Visible = false;
        txtAdditional_exp_deviation_Accm.Visible = false;
        //liAddintional_exps_deviation_2.Visible = false;


        //Accommodation Charges
        liAccomdation_charges_1.Visible = false;
        liAccomdation_charges_Blank1.Visible = false;
        liAccomdation_charges_Blank2.Visible = false;

        //liAccomdation_charges_2.Visible = false;
        liAccomdation_charges_paidby.Visible = false;
        liAccomdation_charges_paidbyBlank1.Visible = false;
        liAccomdation_charges_paidbyBlank2.Visible = false;

        liAccomdation_charges_charges.Visible = false;
        liAccomdation_charges_chargesBlank1.Visible = false;
        //Additional Food Expenses
        liAccomdation_Food_exp_1.Visible = false;
        liAccomdation_Food_exp_Blank1.Visible = false;
        liAccomdation_Food_exp_Blank2.Visible = false;
        //liAccomdation_Food_exp_2.Visible = false;
        liAccomdation_Food_paidby.Visible = false;
        liAccomdation_Food_Charges.Visible = false;
        liAccomdation_Food_Charges_Blank.Visible = false;
        liAccomdation_Food_Deviation.Visible = false;
        liAccomdation_Food_Eligibility.Visible = false;
        liAccomdation_Food_Eligibility_Blank1.Visible = false;
        liAccomdation_Food_Eligibility_Blank2.Visible = false;
        //Additional Expenses
        //liAddintional_exps_Charges.Visible = false;
        //liAddintional_exps_eligibility.Visible = false;
        spnAdditionaExp_eligibility.Visible = false;
        txtFlatElg_Accm.Visible = false;
        txtFlatChg_Accm.Visible = false;
        spnAdditionalCharges.Visible = false;
        #region if Book through COS Yes & Booked
        if (Convert.ToString(hdnIsThrughCOS.Value) == "Yes" && Convert.ToString(hdnAccomodationStatus.Value).Trim() == "Booked")
        {
            spnArrgment.Visible = false;
            rdoAccomodation.Visible = false;
            rdoFoodAccomodation.Visible = false;
            rdoFood.Visible = false;
            rdoOwnArgmnet.Visible = false;

            txtAcctype.Visible = true;
            spnArrgment_accmodation.Visible = true;
            //spnArrgment_2.Visible = true;

            if (rdoAccomodation.Checked == true)
            {
                //Accommodation Charges
                liAccomdation_charges_1.Visible = true;
                liAccomdation_charges_Blank1.Visible = true;
                liAccomdation_charges_Blank2.Visible = true;
                //liAccomdation_charges_2.Visible = true;
                liAccomdation_charges_paidby.Visible = true;
                liAccomdation_charges_paidbyBlank1.Visible = true;
                liAccomdation_charges_paidbyBlank2.Visible = true;

                liAccomdation_charges_charges.Visible = true;
                liAccomdation_charges_chargesBlank1.Visible = true;
                ////txtCharges.Enabled = false;

                //Additional Food Expenses
                liAccomdation_Food_exp_1.Visible = false;
                liAccomdation_Food_exp_Blank1.Visible = false;
                liAccomdation_Food_exp_Blank2.Visible = false;
                //liAccomdation_Food_exp_2.Visible = false;
                liAccomdation_Food_paidby.Visible = true;
                liAccomdation_Food_Charges.Visible = true;
                liAccomdation_Food_Charges_Blank.Visible = true;
                liAccomdation_Food_Deviation.Visible = true;
                liAccomdation_Food_Eligibility.Visible = true;
                liAccomdation_Food_Eligibility_Blank1.Visible = true;
                liAccomdation_Food_Eligibility_Blank2.Visible = true;
            }
            if (rdoFoodAccomodation.Checked == true)
            {
                //Additional Expenses
                liAddintional_exps_flatpaid_1.Visible = true;
                liAddintional_exps_flatpaid_Blank1.Visible = true;
                liAddintional_exps_flatpaid_Blank2.Visible = true;

                //liAddintional_exps_flatpaid_2.Visible = true;

                liAddintional_exps_1.Visible = true;
                liAddintional_exps_Blank1.Visible = true;
                liAddintional_exps_Blank2.Visible = true;
                //liAddintional_exps_2.Visible = true;
                //liAddintional_exps_Charges.Visible = true;
                //liAddintional_exps_eligibility.Visible = true;
                //spnAdditionaExp_eligibility.Visible = true;
                //txtFlatElg_Accm.Visible = true;
                txtFlatChg_Accm.Visible = true;
                spnAdditionalCharges.Visible = true;
                //liAddintional_exps_deviation_1.Visible = true;
                //txtAdditional_exp_deviation_Accm.Visible = true;
                //liAddintional_exps_deviation_2.Visible = true;

                getDeviation_For_Geuest_Hose_withoutFood();
            }
            if (rdoFood.Checked == true)
            {
                liAddintional_exps_flatpaid_1.Visible = true;
                liAddintional_exps_flatpaid_Blank1.Visible = true;
                liAddintional_exps_flatpaid_Blank2.Visible = true;

                //liAddintional_exps_flatpaid_2.Visible = true;

                liAddintional_exps_1.Visible = true;
                liAddintional_exps_Blank1.Visible = true;
                liAddintional_exps_Blank2.Visible = true;
                //liAddintional_exps_2.Visible = true;
                //liAddintional_exps_deviation_1.Visible = true;
                //txtAdditional_exp_deviation_Accm.Visible = true;
                //liAddintional_exps_deviation_2.Visible = true;
                txtAdditional_exp_deviation_Accm.Text = "No";
            }
        }
        #endregion

        #region Booked through COS Yes & Not Booked
        if ((Convert.ToString(hdnIsThrughCOS.Value) == "Yes" && Convert.ToString(hdnAccomodationStatus.Value).Trim() == "Not Booked") || Convert.ToString(hdnIsThrughCOS.Value) == "No")
        {
            //spnArrgment_3.Visible = true;
            if (rdoAccomodation.Checked == true)
            {
                //Accommodation Charges
                liAccomdation_charges_1.Visible = true;
                liAccomdation_charges_Blank1.Visible = true;
                liAccomdation_charges_Blank2.Visible = true;

                //liAccomdation_charges_2.Visible = true;
                liAccomdation_charges_paidby.Visible = true;
                liAccomdation_charges_paidbyBlank1.Visible = true;
                liAccomdation_charges_paidbyBlank2.Visible = true;

                liAccomdation_charges_charges.Visible = true;
                liAccomdation_charges_chargesBlank1.Visible = true;
                liAccomdation_Food_Deviation.Visible = true;
                liAccomdation_Food_Eligibility.Visible = true;
                liAccomdation_Food_Eligibility_Blank1.Visible = true;
                liAccomdation_Food_Eligibility_Blank2.Visible = true;

                ////txtPaidBy_Accm.Text = "Employee";
                ////lstPaidBy_Accm.SelectedValue = "Employee";
                Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                ////txtPaidBy_Accm.BackColor = color;
                ////txtPaidBy_Accm.Enabled = false;
                ////txtPaidBy_Accm.BackColor = color;
            }
            if (rdoFoodAccomodation.Checked == true)
            {
                //Additional Expenses    
                liAddintional_exps_1.Visible = true;
                liAddintional_exps_Blank1.Visible = true;
                liAddintional_exps_Blank2.Visible = true;
                //liAddintional_exps_2.Visible = true;
                //liAddintional_exps_Charges.Visible = true;
                //liAddintional_exps_eligibility.Visible = true;
                //spnAdditionaExp_eligibility.Visible = true;
                //txtFlatElg_Accm.Visible = true;
                txtFlatChg_Accm.Visible = true;
                spnAdditionalCharges.Visible = true;
                //liAddintional_exps_deviation_1.Visible = true;
                //txtAdditional_exp_deviation_Accm.Visible = true;
                //liAddintional_exps_deviation_2.Visible = true;
            }
            if (rdoFood.Checked == true)
            {
                //Additional Expenses  
                liAddintional_exps_flatpaid_1.Visible = true;
                liAddintional_exps_flatpaid_Blank1.Visible = true;
                liAddintional_exps_flatpaid_Blank2.Visible = true;

                //liAddintional_exps_flatpaid_2.Visible = true;
                txtAdditional_exp_deviation_Accm.Text = "No";
            }

            if (rdoOwnArgmnet.Checked == true)
            {
                //Additional Expenses    
                liAddintional_exps_1.Visible = true;
                liAddintional_exps_Blank1.Visible = true;
                liAddintional_exps_Blank2.Visible = true;
                //liAddintional_exps_2.Visible = true;
                //liAddintional_exps_Charges.Visible = true;
                //liAddintional_exps_eligibility.Visible = true;
                //spnAdditionaExp_eligibility.Visible = true;
                //txtFlatElg_Accm.Visible = true;
                txtFlatChg_Accm.Visible = true;
                spnAdditionalCharges.Visible = true;
                txtAdditional_exp_deviation_Accm.Text = "No";
            }

        }

        #endregion

        #region if Expenses Accommodation after Travel i.e  Expenses without Travel
        if (Convert.ToString(hdnTripid.Value).Trim() == "0" || Convert.ToString(hdnIsThrughCOS.Value).Trim() == "")
        {
            //spnArrgment_3.Visible = true;
            if (rdoAccomodation.Checked == true)
            {
                //Accommodation Charges
                liAccomdation_charges_1.Visible = true;
                liAccomdation_charges_Blank1.Visible = true;
                liAccomdation_charges_Blank2.Visible = true;

                //liAccomdation_charges_2.Visible = true;
                liAccomdation_charges_paidby.Visible = true;
                liAccomdation_charges_paidbyBlank1.Visible = true;
                liAccomdation_charges_paidbyBlank2.Visible = true;

                liAccomdation_charges_charges.Visible = true;
                liAccomdation_charges_chargesBlank1.Visible = true;
                liAccomdation_Food_Deviation.Visible = true;
                liAccomdation_Food_Eligibility.Visible = true;
                liAccomdation_Food_Eligibility_Blank1.Visible = true;
                liAccomdation_Food_Eligibility_Blank2.Visible = true;

                ////txtPaidBy_Accm.Text = "Employee";
                ////lstPaidBy_Accm.SelectedValue = "Employee";
                Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                ////txtPaidBy_Accm.BackColor = color;
                ////txtPaidBy_Accm.Enabled = false;
                ////txtPaidBy_Accm.BackColor = color;

            }
            if (rdoFoodAccomodation.Checked == true)
            {
                //Additional Expenses    
                //liAddintional_exps_flatpaid_2.Visible = false;

                liAddintional_exps_1.Visible = true;
                liAddintional_exps_Blank1.Visible = true;
                liAddintional_exps_Blank2.Visible = true;
                //liAddintional_exps_2.Visible = true;
                //liAddintional_exps_Charges.Visible = true;
                //liAddintional_exps_eligibility.Visible = true;
                //spnAdditionaExp_eligibility.Visible = true;
                //txtFlatElg_Accm.Visible = true;
                txtFlatChg_Accm.Visible = true;
                spnAdditionalCharges.Visible = true;

                //liAddintional_exps_deviation_1.Visible = true;
                //txtAdditional_exp_deviation_Accm.Visible = true;
                //liAddintional_exps_deviation_2.Visible = true;
            }
            if (rdoFood.Checked == true)
            {
                //Additional Expenses  
                liAddintional_exps_flatpaid_1.Visible = true;
                liAddintional_exps_flatpaid_Blank1.Visible = true;
                liAddintional_exps_flatpaid_Blank2.Visible = true;

                //liAddintional_exps_flatpaid_2.Visible = true;
                txtAdditional_exp_deviation_Accm.Text = "No";
            }

            if (rdoOwnArgmnet.Checked == true)
            {
                //Additional Expenses    
                liAddintional_exps_1.Visible = true;
                liAddintional_exps_Blank1.Visible = true;
                liAddintional_exps_Blank2.Visible = true;
                //liAddintional_exps_2.Visible = true;
                //liAddintional_exps_Charges.Visible = true;
                //liAddintional_exps_eligibility.Visible = true;
                //spnAdditionaExp_eligibility.Visible = true;
                //txtFlatElg_Accm.Visible = true;
                txtFlatChg_Accm.Visible = true;
                spnAdditionalCharges.Visible = true;

                txtAdditional_exp_deviation_Accm.Text = "No";
            }

        }
        #endregion
    }

    protected void lstAccType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAcctype.Text = lstAccType.SelectedItem.Text;

        // PopupControlExtender1.Commit(lstAccType.SelectedItem.Text);
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "validateTripType();", true);
    }

    protected void rdoAccomodation_CheckedChanged(object sender, EventArgs e)
    {
        txtFlatChg_Accm.Enabled = true;

        if (rdoAccomodation.Checked == true)
            txtAcctype.Text = "Hotel";
        if (rdoOwnArgmnet.Checked == true)
            txtAcctype.Text = "Own Arrangement";
        if (rdoFood.Checked == true)
            txtAcctype.Text = "Guest House (Food)";
        if (rdoFoodAccomodation.Checked == true)
            txtAcctype.Text = "Guest House (without Food)";

        #region Set textBox Value
        txtCharges.Text = "0";
        txtAdditionalFoodExp_exp.Text = "0";
        txtaddintionalExpens_Accm.Text = "0";
        txtFlatChg_Accm.Text = "0";
        txtEligibility_Accm.Text = "0.00";
        txtFlatElg_Accm.Text = "0.00";

        #endregion

        setControlVisibility();
        get_Actual_Flats_rates();

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
        spars[3].Value = Convert.ToString(txtEmpCode_Accm.Text).Trim();
        dtTrDetails = spm.getDataList(spars, "SP_GETALL_Expense_DETAILS");

        // dtTrDetails = spm.GetAccomodationDetailsEdit(Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnAccdtlsid.Value));
        if (dtTrDetails.Rows.Count > 0)
        {
            txtFromdate_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["From Date"]);
            txtToDate_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["To Date"]);
            txtRemarks_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["local_accomodation_remarks"]);
            // txtLocation_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["Location"]);
            DDL_Location_Accm.SelectedItem.Text = Convert.ToString(dtTrDetails.Rows[0]["Location"]);
            hdnCOS.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_through_desk"]);

            txtPaidBy_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["ActualPaid_by"]);
            lstPaidBy_Accm.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["ActualPaid_by"]);
            txtCharges.Text = Convert.ToString(dtTrDetails.Rows[0]["ActualCharges"]);
            hdntripcharges_Accm.Value = Convert.ToString(dtTrDetails.Rows[0]["ActualCharges"]);
            txtDeviation_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["ActualDeviation"]);
            txtEligibility_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["ActualEligibility"]);

            txtFlatPaid_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["FLatPaid_by"]);
            lstFlatPaid_Accm.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["FLatPaid_by"]);

            txtFlatChg_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["FlatCharges"]);
            txtFlatDev_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["FLatDeviation"]);
            txtFlatElg_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["FlatEligibility"]);

            txtAcctype.Text = Convert.ToString(dtTrDetails.Rows[0]["Accomodation_type"]);
            lstAccType.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["Accomodation_type"]);
            // lstLocation_Accm.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["locationid"]);
            // DDL_Location_Accm.SelectedItem.Text = Convert.ToString(dtTrDetails.Rows[0]["locationid"]);
            hdnIsThrughCOS.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_through_desk"]);

            txtAdditionalFoodExp_emp.Text = Convert.ToString(dtTrDetails.Rows[0]["addtn_food_exp_paidby"]);
            txtAdditionalFoodExp_exp.Text = Convert.ToString(dtTrDetails.Rows[0]["addtn_food_exp_charges"]);
            hdnAccomodationStatus.Value = Convert.ToString(dtTrDetails.Rows[0]["bookedstatus"]);
            txtaddintionalExpens_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["additnal_flat_exps"]);

            txtAdditional_exp_deviation_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["add_exp_deviation"]);
            txtnoofDays.Text = Convert.ToString(dtTrDetails.Rows[0]["no_of_days"]);

            #region set Radio Buttons
            if (Convert.ToString(dtTrDetails.Rows[0]["Accomodation_type"]).Trim() == "Hotel")
            {
                rdoAccomodation.Checked = true;
                rdoFood.Checked = false;
                rdoFoodAccomodation.Checked = false;
                rdoOwnArgmnet.Checked = false;

            }
            if (Convert.ToString(dtTrDetails.Rows[0]["Accomodation_type"]).Trim() == "Guest House (Food)")
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

                ////if (Convert.ToString(dtTrDetails.Rows[0]["bookedstatus"]) == "Booked")
                ////{
                ////    txtPaidBy_Accm.Text = "Company";
                ////    lstPaidBy_Accm.SelectedValue = "Company";
                ////    //txtPaidBy_Accm.Enabled = false;
                ////    txtCharges.Enabled = false;

                ////    Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                ////    txtTravelType.BackColor = color;
                ////    txtAcctype.Enabled = false;
                ////    txtAcctype.BackColor = color;
                ////    get_Actual_Flats_rates();
                ////    txtCalculateAmoutns();
                ////}
                ////else
                ////{
                ////    txtPaidBy_Accm.Text = "Employee";
                ////    lstPaidBy_Accm.SelectedValue = "Employee";
                ////    txtPaidBy_Accm.Enabled = true;
                ////    txtCharges.Enabled = true;
                ////    get_Actual_Flats_rates();
                ////    txtCalculateAmoutns();
                ////}

            }
            if (Convert.ToString(dtTrDetails.Rows[0]["ActualPaid_by"]) == "Company")
            {
                txtPaidBy_Accm.Text = "Company";
                lstPaidBy_Accm.SelectedValue = "Company";
                //txtPaidBy_Accm.Enabled = false;
                ////txtCharges.Enabled = false;

                Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
                txtTravelType.BackColor = color;
                txtAcctype.Enabled = false;
                txtAcctype.BackColor = color;
                get_Actual_Flats_rates();
                txtCalculateAmoutns();
            }
            else
            {
                txtPaidBy_Accm.Text = "Employee";
                lstPaidBy_Accm.SelectedValue = "Employee";
                txtPaidBy_Accm.Enabled = true;
                txtCharges.Enabled = true;
                get_Actual_Flats_rates();
                txtCalculateAmoutns();
            }
            if (Convert.ToString(txtEligibility_Accm.Text).Trim() == "")
            {
                txtEligibility_Accm.Text = "0";
                hdnactualEligbility.Value = "0";
            }
            if (Convert.ToString(txtFlatElg_Accm.Text).Trim() == "")
            {
                txtFlatElg_Accm.Text = "0";
                hdnflatEligbility.Value = "0";
            }

        }
    }
    public void getAccomodationDetails_Accm()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetExpensesAccomodationDetails(txtEmpCode_Accm.Text);

        if (dtTrDetails.Rows.Count > 0)
        {
            txtFromdate_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["From Date"]);
            txtToDate_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["To Date"]);
            txtRemarks_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["local_accomodation_remarks"]);
            // txtLocation_Accm.Text = Convert.ToString(dtTrDetails.Rows[0]["Location"]);
            // lstLocation_Accm.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["locationid"]);
            DDL_Location_Accm.SelectedItem.Text = Convert.ToString(dtTrDetails.Rows[0]["Location"]);
            // lstLocation_Accm.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["locationid"]);

        }
    }
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
    #endregion Accommodation

    #region Local Travel
    protected void lstTravelMode_Locl_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTravelMode_Locl.Text = lstTravelMode_Locl.SelectedItem.Text;
        hdntrmodeid.Value = lstTravelMode_Locl.SelectedValue;
        SetTravelDeviation_Locl();
        txtDeviation_Locl.Text = hdnDeviation_Locl.Value;
        hdnDeviation_Locl.Value = hdnDeviation_Locl.Value;
        //PopupControlExtender11.Commit(lstTravelMode_Locl.SelectedItem.Text);

        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetDeviation_Locl('" + hdnDeviation_Locl.Value + "');", true);
    }
    //protected void lstLocation_Locl_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtLocation_Locl.Text = lstLocation_Locl.SelectedItem.Text;
    //    PopupControlExtender10.Commit(lstLocation_Locl.SelectedItem.Text);

    //}
    ////protected void txtFromdate_Locl_TextChanged(object sender, EventArgs e)
    ////{
    ////    if (Convert.ToString(txtFromdate_Locl.Text).Trim() == "")
    ////    {
    ////        return;
    ////    }
    ////    DateValidations_Locl();
    ////}
    ////protected void txtToDate_Locl_TextChanged(object sender, EventArgs e)
    ////{
    ////    if (Convert.ToString(txtFromdate_Locl.Text).Trim() == "")
    ////    {
    ////        lblmessage.Text = "From date  cannot be blank";
    ////        return;
    ////    }


    ////    DateValidations_Locl();
    ////}
    protected void lstTravelType_Locl_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTravelType_Locl.Text = lstTravelType_Locl.SelectedItem.Text;
        PopupControlExtender9.Commit(lstTravelType_Locl.SelectedItem.Text);
    }

    protected void DateValidations_Locl()
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

    protected void localtrvl_btnSave_Click(object sender, EventArgs e)
    {
        string[] strdate;
        #region Check All Fields Blank
        //  lblmessage.Text = "";
        if (Convert.ToString(txtTravelMode_Locl.Text).Trim() == "")
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
        if (DDL_Location_Locl.SelectedItem.Text == "0" || DDL_Location_Locl.SelectedItem.Text == "")
        {
            lblmessage.Text = "Please Enter Location For Travel";
            return;
        }

        if (Convert.ToString(txtRemarks_Locl.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Remarks.";
            return;
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

        string strsptype = "insert";
        if (Convert.ToString(localtrvl_btnSave.Text).Trim() == "Update")
        {
            strsptype = "update";
        }
        if (hdnTripid.Value == "")
            hdnTripid.Value = "0";
        string Locl_Status = "";
        if (Chk_COS_Locl.Checked)
            Locl_Status = "Booked";
        hdntrmodeid.Value = lstTravelMode_Locl.SelectedValue;
        spm.InsertExpenseLocalTrvlDetails(Convert.ToInt32(hdnTryiptypeid.Value), strfromDate, strToDate, DDL_Location_Locl.SelectedItem.Text, txtEmpCode_Locl.Text, txtRemarks_Locl.Text, strsptype, Convert.ToInt32(hdntrmodeid.Value), Convert.ToString(hdnDeviation_Locl.Value).Trim(), dCharges, Convert.ToDecimal(hdntrdetailsid_Locl.Value), Convert.ToDecimal(hdnTripid.Value), Locl_Status);
        Div_Locl.Visible = false;
        trvl_localbtn.Text = "+";
        getExpenseLcoalTravel();
        getExpensedtls_from_temp();
        CalculateClaimAmt();
        //Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value);
    }

    protected void localtrvl_cancel_btn_Click(object sender, EventArgs e)
    {
        Div_Locl.Visible = false;
        trvl_localbtn.Text = "+";
        //Response.Redirect("TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value);
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
        spm.InsertExpenseLocalTrvlDetails(Convert.ToInt32(hdnTryiptypeid.Value), strfromDate, strToDate, DDL_Location_Locl.SelectedItem.Text, txtEmpCode_Locl.Text, txtRemarks_Locl.Text, strsptype, Convert.ToInt32(hdntrmodeid.Value), Convert.ToString(hdnDeviation_Locl.Value).Trim(), 0, Convert.ToDecimal(hdntrdetailsid_Locl.Value), Convert.ToDecimal(hdnTripid.Value), "");
        //Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value);
    }

    public void GetTravelMode_locl()
    {
        DataTable dtTripMode = new DataTable();
        dtTripMode = spm.getTravelModefor_Requestor();
        if (dtTripMode.Rows.Count > 0)
        {
            lstTravelMode_Locl.DataSource = dtTripMode;
            lstTravelMode_Locl.DataTextField = "trip_mode";
            lstTravelMode_Locl.DataValueField = "trip_mode_id";
            lstTravelMode_Locl.DataBind();
            lstTravelMode_Locl.Items.Insert(0, new ListItem("Select Mode", "0"));
        }
    }

    public void GetPaidBy_Accm()
    {
        lstPaidBy_Accm.Items.Clear();
        lstPaidBy_Accm.Items.Insert(0, new ListItem("Select Paid By", "0"));
        lstPaidBy_Accm.Items.Insert(1, new ListItem("Company", "Company"));
        lstPaidBy_Accm.Items.Insert(2, new ListItem("Employee", "Employee"));
    }

    public void getLocaTravelDetails_edit_Locl()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetExpneseLocalTrvlDetails_edit(txtEmpCode_Locl.Text, hdntrdetailsid_Locl.Value);

        if (dtTrDetails.Rows.Count > 0)
        {
            txtFromdate_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["From Date"]);
            txtToDate_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["To Date"]);
            txtRemarks_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["remarks"]);
            // txtLocation_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["Location"]);
            DDL_Location_Locl.SelectedItem.Text = Convert.ToString(dtTrDetails.Rows[0]["Location"]);
            hdnLocalId.Value = Convert.ToString(dtTrDetails.Rows[0]["local_travel_id"]);
            txtCharges_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["Charge"]);
            txtDeviation_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["Deviation"]);
            if (Convert.ToString(dtTrDetails.Rows[0]["trip_mode"]).Trim() != "" && Convert.ToString(dtTrDetails.Rows[0]["trip_mode"]) != "0")
                txtTravelMode_Locl.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_mode"]);

            hdntrmodeid.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_mode"]);
            lstTravelMode_Locl.SelectedValue = hdntrmodeid.Value;
            hdnDeviation_Locl.Value = Convert.ToString(dtTrDetails.Rows[0]["Deviation"]);
            lstTravelType_Locl.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["Trip_type_id"]);
            hdnDesk_Locl.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_through_desk"]);
            localtrvl_btnSave.Text = "Update";
            ////if (Convert.ToString(dtTrDetails.Rows[0]["trvl_status"]) == "Booked")
            ////{
            ////    // txtCharges_Locl.Enabled = false;
            ////    //txtTravelType_Locl.Enabled = false;
            ////    Color color = System.Drawing.ColorTranslator.FromHtml("#ebebe4");
            ////    txtTravelMode_Locl.BackColor = color;
            ////    txtTravelMode_Locl.Enabled = false;

            ////}
            if (Convert.ToString(dtTrDetails.Rows[0]["status"]) == "Booked")
            {
                Chk_COS_Locl.Checked = true;
            }
            else
            {
                Chk_COS_Locl.Checked = false;
            }
        }
    }
    public void SetTravelDeviation_Locl()
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
    #endregion Local Travel

    protected void lstTravelType_Oth_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTravelType_Oth.Text = lstTravelType_Oth.SelectedItem.Text;
        PopupControlExtender12.Commit(lstTravelType_Oth.SelectedItem.Text);
    }
    protected void lstExpdtls_Oth_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtExpdtls_Oth.Text = lstExpdtls_Oth.SelectedItem.Text;
        //PopupControlExtender13.Commit(lstExpdtls_Oth.SelectedItem.Text);
        hdnselectionStatus_Oth.Value = "Yes";
        SetTravelDeviation_oth();
        hdntxtAmt_Oth.Value = "0";


        GetIncidentalCharges(Convert.ToString(txtExpdtls_Oth.Text));
        txtAmt_Oth.Text = hdntxtAmt_Oth.Value;

        ////if (Convert.ToString(txtExpdtls_Oth.Text) == "Daily Halting Allowance" || Convert.ToString(txtExpdtls_Oth.Text) == "Incidental charges during travel")
        ////{
        ////    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetCntrsl('Yes','" + hdntxtAmt_Oth.Value + "');", true);
        ////}
        ////else
        ////{
        ////    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetCntrsl('No','" + hdntxtAmt_Oth.Value + "');", true);
        ////}

    }
    public void GetIncidentalCharges(string detail)
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
    public void SetTravelDeviation_oth()
    {
        hdnDeviation_Oth.Value = "No";
    }
    protected void txtnoofDays_Oth_TextChanged(object sender, EventArgs e)
    {
        get_Actual_DayDiff_Oth();
        //lblnodays.Style.Add("display", "inline");
        //txtnoofDays_Oth.Style.Add("display", "inline");

    }

    public void get_Actual_DayDiff_Oth()
    {
        string[] strdate;
        //strdate = Convert.ToString(txtnoofDays_Oth.Text).Trim().Split('.');
        //if (strdate.Length > 2)
        //{
        //    lblmessage.Text = "Please enter correct Actual stay duration (in days)";
        //    txtnoofDays_Oth.Text = "0";
        //    return;
        //}
        //if (Convert.ToString(txtnoofDays_Oth.Text).Trim() != "")
        //{
        //    if (Convert.ToString(txtnoofDays_Oth.Text).Trim() != "0.00" || Convert.ToString(txtnoofDays_Oth.Text).Trim() != "0")
        //    {
        //        if (Convert.ToString(txtnoofDays_Oth.Text).Trim() == "")
        //        {
        //            lblmessage.Text = "Please enter Actual stay duration (in days)";
        //            return;
        //        }
        //        Decimal dfare = 0;
        //        dfare = Convert.ToDecimal(txtnoofDays_Oth.Text);
        //        if (dfare == 0)
        //        {
        //            lblmessage.Text = "Please enter Actual stay duration (in days)";
        //            return;
        //        }
        //    }
        //}

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

    protected void Oth_btnSave_Click(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";

        if (txtFromdate_Oth.Text == "")
        {
            lblmessage.Text = "Please Enter date!";
            return;
        }
        if (txtRemarks_Oth.Text == "")
        {
            lblmessage.Text = "Please Enter Remarks!";
            return;
        }
        if (txtExpdtls_Oth.Text == "")
        {
            lblmessage.Text = "Please Select Details!";
            return;
        }
        //if (Convert.ToString(txtExpdtls_Oth.Text) == "Daily Halting Allowance" || Convert.ToString(txtExpdtls_Oth.Text) == "Incidental charges during travel")
        //{
        //    strdate = Convert.ToString(txtnoofDays_Oth.Text).Trim().Split('.');
        //    if (strdate.Length > 2)
        //    {
        //        lblmessage.Text = "Please enter correct Actual stay duration (in days)";
        //        txtnoofDays_Oth.Text = "0";
        //        return;
        //    }

        //    if (Convert.ToString(txtnoofDays_Oth.Text).Trim() != "")
        //    {
        //        if (Convert.ToString(txtnoofDays_Oth.Text).Trim() != "0.00" || Convert.ToString(txtnoofDays_Oth.Text).Trim() != "0")
        //        {
        //            if (Convert.ToString(txtnoofDays_Oth.Text).Trim() == "")
        //            {
        //                lblmessage.Text = "Please enter Actual stay duration (in days)";
        //                txtnoofDays_Oth.Text = "";
        //                return;
        //            }
        //            Decimal dfare = 0;
        //            dfare = Convert.ToDecimal(txtnoofDays_Oth.Text);
        //            if (dfare == 0)
        //            {
        //                lblmessage.Text = "Please enter Actual stay duration (in days)";
        //                txtnoofDays_Oth.Text = "";
        //                return;
        //            }
        //        }
        //    }
        //}


        if (Convert.ToString(txtAmt_Oth.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter the Amount";
            Session["chkTrvlAccLocalTrvlbtnStatus"] = "";
            //if (Convert.ToString(txtExpdtls_Oth.Text) == "Daily Halting Allowance" || Convert.ToString(txtExpdtls_Oth.Text) == "Incidental charges during travel")
            //{
            //    lblnodays.Style.Add("display", "inline");
            //    txtnoofDays_Oth.Style.Add("display", "inline");
            //}
            //else
            //{
            //    lblnodays.Style.Add("display", "none");
            //    txtnoofDays_Oth.Style.Add("display", "none");
            //}
            return;
        }
        //if (Convert.ToString(txtExpdtls_Oth.Text) == "Daily Halting Allowance" || Convert.ToString(txtExpdtls_Oth.Text) == "Incidental charges during travel")
        //{
        //    if (Convert.ToString(txtnoofDays_Oth.Text).Trim() == "" || Convert.ToString(txtnoofDays_Oth.Text).Trim() == "0" || Convert.ToString(txtnoofDays_Oth.Text).Trim() == "0.00")
        //    {
        //        lblmessage.Text = "Please enter correct Actual stay duration (in days)";
        //        Session["chkTrvlAccLocalTrvlbtnStatus"] = "";
        //        if (Convert.ToString(txtExpdtls_Oth.Text) == "Daily Halting Allowance" || Convert.ToString(txtExpdtls_Oth.Text) == "Incidental charges during travel")
        //        {
        //            lblnodays.Style.Add("display", "inline");
        //            txtnoofDays_Oth.Style.Add("display", "inline");
        //        }
        //        else
        //        {
        //            lblnodays.Style.Add("display", "none");
        //            txtnoofDays_Oth.Style.Add("display", "none");
        //        }
        //        return;
        //    }
        //}

        if (Convert.ToString(txtAmt_Oth.Text).Trim() != "")
        {

            strdate = Convert.ToString(txtAmt_Oth.Text).Trim().Split('.');
            if (strdate.Length > 2)
            {
                lblmessage.Text = "Please enter correct Amount.";
                txtAmt_Oth.Text = "0";
                Session["chkTrvlAccLocalTrvlbtnStatus"] = "";
                //if (Convert.ToString(txtExpdtls_Oth.Text) == "Daily Halting Allowance" || Convert.ToString(txtExpdtls_Oth.Text) == "Incidental charges during travel")
                //{
                //    lblnodays.Style.Add("display", "inline");
                //    txtnoofDays_Oth.Style.Add("display", "inline");
                //}
                //else
                //{
                //    lblnodays.Style.Add("display", "none");
                //    txtnoofDays_Oth.Style.Add("display", "none");
                //}
                return;
            }
            Decimal dfare = 0;
            dfare = Convert.ToDecimal(txtAmt_Oth.Text);
            if (dfare == 0)
            {
                lblmessage.Text = "Please enter correct Amount.";
                Session["chkTrvlAccLocalTrvlbtnStatus"] = "";
                //if (Convert.ToString(txtExpdtls_Oth.Text) == "Daily Halting Allowance" || Convert.ToString(txtExpdtls_Oth.Text) == "Incidental charges during travel")
                //{
                //    lblnodays.Style.Add("display", "inline");
                //    txtnoofDays_Oth.Style.Add("display", "inline");
                //}
                //else
                //{
                //    lblnodays.Style.Add("display", "none");
                //    txtnoofDays_Oth.Style.Add("display", "none");
                //}
                return;
            }
        }

        if (Convert.ToString(txtnoofDays_Oth.Text).Trim() != "" && Convert.ToString(txtnoofDays_Oth.Text).Trim() != "0" && Convert.ToString(txtnoofDays_Oth.Text).Trim() != "0.00")
        {
            Decimal dnodays = Convert.ToDecimal(txtnoofDays_Oth.Text);
            Decimal dActnodays = Convert.ToDecimal(hdnDaysDiff_Oth.Value);
            if (dnodays > dActnodays)
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
        if (Convert.ToString(txtFromdate_Oth.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate_Oth.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        #endregion

        decimal paidbycomp = 0;
        decimal paidbyemp = 0;
        txtpaidby.Text = "Employee";
        txtnoofDays_Oth.Text = "0";
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
        if (Convert.ToString(txtAmt_Oth.Text).Trim() != "")
            paidbyemp = Math.Round(Convert.ToDecimal(txtAmt_Oth.Text), 2);
        Decimal dnoofDays = 0;

        if (Convert.ToString(txtnoofDays_Oth.Text).Trim() != "")
            dnoofDays = Convert.ToDecimal(txtnoofDays_Oth.Text);
        Boolean blnCheckAddnExp = false;

        #region Check Selected Additional Expeses already Submitted
        ////DataSet dsTrDetails = new DataSet();
        ////SqlParameter[] spars = new SqlParameter[4];

        ////spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        ////spars[0].Value = "Check_addintional_exp_isSubmitted";

        ////spars[1] = new SqlParameter("@exp_sr_no", SqlDbType.Int);
        ////if (Convert.ToString(lstExpdtls_Oth.SelectedValue).Trim() != "")
        ////    spars[1].Value = Convert.ToInt32(lstExpdtls_Oth.SelectedValue);
        ////else
        ////    spars[1].Value = DBNull.Value;

        ////spars[2] = new SqlParameter("@exp_sr_a", SqlDbType.Int);
        ////if (Convert.ToString(hdnexpSrno_Oth.Value).Trim() != "")
        ////    spars[2].Value = Convert.ToInt32(hdnexpSrno_Oth.Value);
        ////else
        ////    spars[2].Value = "0";

        ////spars[3] = new SqlParameter("@empcode", SqlDbType.VarChar);
        ////spars[3].Value = txtEmpCode_Oth.Text;

        ////dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");

        ////if (dsTrDetails.Tables[0].Rows.Count > 0)
        ////{
        ////    blnCheckAddnExp = true;
        ////}
        #endregion

        if (Oth_btnSave.Text == "Submit")
        {
            //lstTravelType_Oth
            #region InsertTravelDetails
            Session["Destination"] = DDLDestination.SelectedItem.Text;
            //spm.InsertTravelDetails(Convert.ToInt32(hdnTryiptypeid.Value), Convert.ToInt32(lstExpdtls_Oth.SelectedValue), strfromDate, txtOrigin.Text, strToDate, txtDestination.Text, txtDeviation.Text, txtEmpCode_Oth.Text, 0, hdnCOS.Value, txtpaidby.Text, "InsertTempTable");
            //spm.InsertExpensesDetails(Convert.ToInt32(hdnTryiptypeid.Value), Convert.ToInt32(lstExpdtls_Oth.SelectedValue), strfromDate, txtOrigin.Text, strToDate, txtDestination.Text, txtDeviation.Text, txtEmpCode_Oth.Text, 0, hdnCOS.Value, txtpaidby.Text, "InsertTempTable_addionalExp");

            if (Convert.ToString(hdnTripid.Value).Trim() == "")
            {
                lblmessage.Text = "Please Travel type.";
                return;
            }
            if (Convert.ToString(paidbyemp).Trim() == "")
            {
                lblmessage.Text = "Please Check Paid by Employee.";
                return;
            }
            if (Convert.ToString(paidbycomp).Trim() == "")
            {
                lblmessage.Text = "Please Check Paid by Company.";
                return;
            }
            if (Convert.ToString(dnoofDays).Trim() == "")
            {
                lblmessage.Text = "Please Date.";
                return;
            }
            if (blnCheckAddnExp == true)
            {
                lblmessage.Text = "Selected Expense details already Submitted.";
                Session["chkTrvlAccLocalTrvlbtnStatus"] = "";
                //if (Convert.ToString(txtExpdtls_Oth.Text) == "Daily Halting Allowance" || Convert.ToString(txtExpdtls_Oth.Text) == "Incidental charges during travel")
                //{
                //    lblnodays.Style.Add("display", "inline");
                //    txtnoofDays_Oth.Style.Add("display", "inline");
                //}
                //else
                //{
                //    lblnodays.Style.Add("display", "none");
                //    txtnoofDays_Oth.Style.Add("display", "none");
                //}
                return;
            }
            else
            {
                spm.InsertExpensesDetails(txtEmpCode_Oth.Text, strfromDate, strfromDate, Convert.ToDecimal(hdnTripid.Value), "", Convert.ToString(lstExpdtls_Oth.SelectedValue).Trim(), paidbycomp, paidbyemp, 0, 0, 0, Convert.ToString(txtTravelType_Oth.Text).Trim(), "", "InsertTempTable_addionalExp", Convert.ToString(txtRemarks_Oth.Text).Trim(), dnoofDays);

                //Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value, false);
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
                //if (Convert.ToString(txtExpdtls_Oth.Text) == "Daily Halting Allowance" || Convert.ToString(txtExpdtls_Oth.Text) == "Incidental charges during travel")
                //{
                //    lblnodays.Style.Add("display", "inline");
                //    txtnoofDays_Oth.Style.Add("display", "inline");
                //}
                //else
                //{
                //    lblnodays.Style.Add("display", "none");
                //    txtnoofDays_Oth.Style.Add("display", "none");
                //}
                return;
            }
            else
            {
                spm.InsertExpensesDetails(txtEmpCode_Oth.Text, strfromDate, strfromDate, Convert.ToDecimal(hdnTripid.Value), "", Convert.ToString(lstExpdtls_Oth.SelectedValue).Trim(), paidbycomp, paidbyemp, 0, 0, 0, Convert.ToString(txtTravelType_Oth.Text).Trim(), Convert.ToString(hdnexpSrno_Oth.Value).Trim(), "InsertTempTable_addionalExp", Convert.ToString(txtRemarks_Oth.Text).Trim(), dnoofDays);

                //Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value, false);
            }
            #endregion
        }
        Div_Oth.Visible = false;
        lnkbtn_expdtls.Text = "+";
        getExpensedtls_from_temp();
        CalculateClaimAmt();

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
        if (Convert.ToString(txtAmt_Oth.Text).Trim() != "")
            paidbyemp = Convert.ToDecimal(txtAmt_Oth.Text);

        if (Convert.ToString(Session["chkTrvlAccLocalTrvlbtnStatus"]).Trim() != "")
        {
            Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value, false);
        }
        Session["chkTrvlAccLocalTrvlbtnStatus"] = "Additional Expenses button Event is Submitted";

        //lstTravelType_Oth
        #region InsertTravelDetails
        spm.InsertExpensesDetails(txtEmpCode_Oth.Text, strfromDate, strfromDate, Convert.ToDecimal(hdnTripid.Value), "", Convert.ToString(lstExpdtls_Oth.SelectedValue).Trim(), paidbycomp, paidbyemp, 0, 0, 0, Convert.ToString(txtTravelType_Oth.Text).Trim(), Convert.ToString(hdnexpSrno_Oth.Value).Trim(), "DeleteTempTable_addionalExp", Convert.ToString(txtRemarks_Oth.Text).Trim(), 0);

        Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value, false);
        #endregion

    }
    protected void Oth_btnCancel_Click(object sender, EventArgs e)
    {
        Div_Oth.Visible = false;
        lnkbtn_expdtls.Text = "+";
        //Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value, false);
    }
    public void GetTripDetails_Oth()
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
    public void GetTravelMode_Oth()
    {
        DataTable dtTripMode = new DataTable();
        dtTripMode = spm.getExpenseTravelMode();
        if (dtTripMode.Rows.Count > 0)
        {
            lstExpdtls_Oth.DataSource = dtTripMode;
            lstExpdtls_Oth.DataTextField = "exp_name";
            lstExpdtls_Oth.DataValueField = "expid";
            lstExpdtls_Oth.DataBind();
            lstExpdtls_Oth.Items.Insert(0, new ListItem("Select Expense", "0"));
        }
    }
    public void getTravelDetails_Oth()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetTravelDetails(txtEmpCode_Oth.Text);

        if (dtTrDetails.Rows.Count > 0)
        {
            txtExpdtls_Oth.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_mode"]);
            txtDeviation.Text = Convert.ToString(dtTrDetails.Rows[0]["deviation"]);
            txtpaidby.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_requirements"]);
            //txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["departure_date"]);
            //txttrvlLocation.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_date"]);
            txtTravelType_Oth.Text = Convert.ToString(Session["TravelType"]);
            hdnDesk_Oth.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_through_desk"]);
            // txtOrigin.Text = Convert.ToString(dtTrDetails.Rows[0]["departure_place"]);
            // txtDestination.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_place"]);
            DDLOrigin.SelectedItem.Text = Convert.ToString(dtTrDetails.Rows[0]["departure_place"]);
            DDLDestination.SelectedItem.Text = Convert.ToString(dtTrDetails.Rows[0]["arrival_place"]);

        }
    }
    public void getTravelDetailsEdit_Oth()
    {

        DataTable dtTrDetails = new DataTable();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_getExpens_dtlsEdit_Temp";

        spars[1] = new SqlParameter("@exp_sr_no", SqlDbType.Int);
        spars[1].Value = hdnexpSrno_Oth.Value;

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = txtEmpCode_Oth.Text;
        dtTrDetails = spm.getDataList(spars, "SP_GETALL_Expense_DETAILS");

        if (dtTrDetails.Rows.Count > 0)
        {
            //txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_frm_date"]);
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
            txtFromdate_Oth.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_frm_date"]).Trim();
            txtExpdtls_Oth.Text = Convert.ToString(dtTrDetails.Rows[0]["exp_name"]).Trim();
            lstExpdtls_Oth.SelectedValue = Convert.ToString(dtTrDetails.Rows[0]["trip_details"]).Trim();
            //txttrvlLocation.Text = Convert.ToString(dtTrDetails.Rows[0]["trip_destination"]).Trim();
            txtnoofDays_Oth.Text = Convert.ToString(dtTrDetails.Rows[0]["no_of_days"]).Trim();

            lblnodays.Style.Add("display", "none");
            txtnoofDays_Oth.Style.Add("display", "none");

            //if (Convert.ToString(txtExpdtls_Oth.Text) == "Daily Halting Allowance" || Convert.ToString(txtExpdtls_Oth.Text) == "Incidental charges during travel")
            //{
            //    lblnodays.Style.Add("display", "inline");
            //    txtnoofDays_Oth.Style.Add("display", "inline");
            //}
        }
    }
    protected void lstpaidby_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtpaidby.Text = lstpaidby.SelectedItem.Text;
        PopupControlExtender14.Commit(lstpaidby.SelectedItem.Text);

    }
    protected void btn_del_Trvl_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnTripid.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnTravelDtlsId.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[1]).Trim();
        Decimal dactualfare = 0;
        string stype = "delete";


        if (Convert.ToString(txtfare.Text).Trim() != "")
            dactualfare = Math.Round(Convert.ToDecimal(txtfare.Text), 2);

        decimal dfoodallowance = 0;
        if (Convert.ToString(txtFoodAllowance.Text).Trim() != "")
        {
            dfoodallowance = Math.Round(Convert.ToDecimal(txtFoodAllowance.Text), 2);

        }

        spm.InsertExpenseTravelDetails(Convert.ToInt32(0), Convert.ToInt32(0), "", DDLOrigin.SelectedItem.Text, txtFromTime.Text, "", DDLDestination.SelectedItem.Text, txtDeviation.Text, Convert.ToString(txtToTime.Text), txtEmpCode.Text, Convert.ToDecimal(0), hdnCOS.Value, dactualfare, txtRemark.Text, stype, Convert.ToDecimal(hdnTravelDtlsId.Value), dfoodallowance, "");
        DivTrvl.Visible = false;
        btnTra_Details.Text = "+";
        GetExpenseTravelDetails();
        getExpensedtls_from_temp();
        CalculateClaimAmt();
    }
    protected void btn_del_Accm_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnTripid.Value = Convert.ToString(dgAccomodation.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnAccId.Value = Convert.ToString(dgAccomodation.DataKeys[row.RowIndex].Values[1]).Trim();

        SqlParameter[] spars = new SqlParameter[3];

        #region Set SQL Parameters
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "deleteTempTable";
        // spars[0].Value = "InsertTempTable";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

        spars[2] = new SqlParameter("@local_accomodation_id", SqlDbType.Int);
        if (Convert.ToString(hdnAccId.Value).Trim() != "")
            spars[2].Value = hdnAccId.Value;
        else
            spars[2].Value = DBNull.Value;

        #endregion

        DataTable dt = spm.InsertorUpdateData(spars, "SP_insert_Expense_accomodation_details");
        DivAccm.Visible = false;
        trvl_accmo_btn.Text = "+";
        GetExpenseAccomodationDetails();
        getExpensedtls_from_temp();
        CalculateClaimAmt();
    }
    protected void btn_del_Locl_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnLocalId.Value = Convert.ToString(dgLocalTravel.DataKeys[row.RowIndex].Values[1]).Trim();

        string strsptype = "delete";

        spm.InsertExpenseLocalTrvlDetails(Convert.ToInt32(0), "", "", DDL_Location_Locl.SelectedItem.Text, txtEmpCode.Text, txtRemarks_Locl.Text, strsptype, Convert.ToInt32(0), Convert.ToString(hdnDeviation_Locl.Value).Trim(), 0, Convert.ToDecimal(hdnLocalId.Value), Convert.ToDecimal(0), "");
        Div_Locl.Visible = false;
        trvl_localbtn.Text = "+";
        getExpenseLcoalTravel();
        getExpensedtls_from_temp();
        CalculateClaimAmt();

    }

    private void getPayementVoucher_forPrint()
    {
        try
        {
            #region get payment Voucher details
            DataSet dspaymentVoucher = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "TravelExp";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDecimal(hdnexp_id.Value);

            dspaymentVoucher = spm.getDatasetList(spars, "rpt_dataProcedure");

            #endregion

            if (dspaymentVoucher.Tables[0].Rows.Count > 0)
            {
                ReportViewer ReportViewer1 = new ReportViewer();


                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.LocalReport.DataSources.Clear();

                ReportParameter[] param = new ReportParameter[20];
                param[0] = new ReportParameter("pdocno", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Vouno"]));
                param[1] = new ReportParameter("ppvdate", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Rem_Sub_Date"]));
                param[2] = new ReportParameter("pempName", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Emp_Name"]));
                param[3] = new ReportParameter("pempCode", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["emp_code"]));

                param[5] = new ReportParameter("pTrip_Duration", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Trip_Duration"]));
                param[6] = new ReportParameter("padvance", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["advance"]));
                param[11] = new ReportParameter("pNet_Pay_Company", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Net_Pay_Company"]));
                param[12] = new ReportParameter("pNet_Pay_Employee", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Net_Pay_Employee"]));
                param[13] = new ReportParameter("trip_description", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["trip_description"]));
                param[14] = new ReportParameter("pReason", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Reason"]));

                param[4] = new ReportParameter("pRsinwords", Convert.ToString(dspaymentVoucher.Tables[2].Rows[0]["AmountinWords"]));


                #region Cost Cente & Bank Details
                if (dspaymentVoucher.Tables[3].Rows.Count > 0)
                {
                    param[7] = new ReportParameter("pBankName", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["BANK_name"]));
                    param[8] = new ReportParameter("pBankAccCode", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["BANK_acc"]));
                    param[9] = new ReportParameter("pBankIFSCcode", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["BANK_ifsc"]));
                    param[10] = new ReportParameter("pBankBranch", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["BANK_Branch"]));
                }
                else
                {
                    param[7] = new ReportParameter("pBankName", Convert.ToString(""));
                    param[8] = new ReportParameter("pBankAccCode", Convert.ToString(""));
                    param[9] = new ReportParameter("pBankIFSCcode", Convert.ToString(""));
                    param[10] = new ReportParameter("pBankBranch", Convert.ToString(""));
                }
                #endregion

                param[15] = new ReportParameter("pContact", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["mobile"]));
                param[16] = new ReportParameter("PAlt_Contact", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["grade"]));
                param[17] = new ReportParameter("pProjectName", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Project_Name"]));
                param[18] = new ReportParameter("pDept_Name", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Dept_Name"]));
                param[19] = new ReportParameter("pTotalClaimedAmt", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["TotalClaimedAmt"]));

                // Create Report DataSource
                ReportDataSource rds = new ReportDataSource("dsTravelExpenses", dspaymentVoucher.Tables[0]);
                ReportDataSource rdsApprs = new ReportDataSource("dsTravel_Apprs", dspaymentVoucher_Apprs.Tables[0]);

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/PaymentVoucher_Travel.rdlc");
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.DataSources.Add(rdsApprs);
                ReportViewer1.LocalReport.SetParameters(param);


                #region Create payment Voucher PDF file
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                DataTable DataTable1 = new DataTable();
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=paymentVoucherDetails." + extension);
                try
                {
                    Response.BinaryWrite(bytes);
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error while generating PDF.');", true);
                    Console.WriteLine(ex.StackTrace);
                }

                #endregion


            }

        }
        catch (Exception ex)
        {
        }
    }
    protected void mobile_btnPrintPV_Click(object sender, EventArgs e)
    {
        getApproverlist_New();
        getPayementVoucher_forPrint();
    }
    protected void txtFromdate_Oth_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txtFromdate_Oth.Text).Trim() == "")
        {
            return;
        }
        DateValidations_Oth();
    }

    protected void DateValidations_Oth()
    {
        if (Convert.ToString(txtFromdate_Oth.Text).Trim() == "")
            return;


        string[] tr_strdate;
        string tr_strfromDate = "";
        string tr_strToDate = "";

        #region Exp.TraelRequestdate formatting

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

        lblmessage.Text = "";
        string message = "";
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";

        #region date formatting

        if (Convert.ToString(txtFromdate_Oth.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate_Oth.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        #endregion
        DataSet dttraveletails = new DataSet();
        if (Convert.ToString(txtFromdate_Oth.Text).Trim() != "")
        {
            dttraveletails = spm.Get_ExpTravel_Details_ValidationResult(tr_strfromDate, tr_strToDate, strfromDate, strToDate, txtEmpCode_Oth.Text, hdnTripid.Value, "ExpLocalTrvls");

            if (dttraveletails.Tables[1].Rows.Count > 0)
            {
                message = Convert.ToString(dttraveletails.Tables[1].Rows[0]["Message"]);

            }

            if (Convert.ToString(message).Trim() != "")
            {
                lblmessage.Text = Convert.ToString(message).Trim();
                lblmessage.Visible = true;
                txtFromdate_Oth.Text = "";
            }

        }

    }

    //protected void Txt_ProjectName_TextChanged(object sender, EventArgs e)
    //{

    //}

    [System.Web.Services.WebMethod]
    public static List<string> SearchProject(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";
                strsql = "SELECT distinct Location_name FROM tbl_hmst_company_Location " +
                           "   where Location_name like '%' + @SearchText + '%' order by Location_name asc";

                cmd.CommandText = strsql;
                cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));

                cmd.Connection = conn;
                conn.Open();
                List<string> employees = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        employees.Add(sdr["Location_name"].ToString());
                    }
                }
                conn.Close();
                return employees;
            }
        }
    }

    [System.Web.Services.WebMethod]
    public static List<string> SearchDepartment(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";
                strsql = "SELECT Department_Name FROM tblDepartmentMaster " +
                           "   where Department_Name like '%' + @SearchText + '%' order by Department_Name asc";

                cmd.CommandText = strsql;
                cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));

                cmd.Connection = conn;
                conn.Open();
                List<string> employees = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        employees.Add(sdr["Department_Name"].ToString());
                    }
                }
                conn.Close();
                return employees;
            }
        }
    }
    public bool checkIsExistProject(string projectName)
    {
        bool blnchk = false;
        try
        {
            // var getProjectSplit = projectName.Split('/');
            // var getCode = "";
            var getLocation = projectName;
            if (getLocation == "")
            {
                return false;
            }

            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckProjectExistOnClaim";

            spars[1] = new SqlParameter("@projectName", SqlDbType.VarChar);
            if (Convert.ToString(getLocation).Trim() != "")
                spars[1].Value = Convert.ToString(getLocation).Trim();

            DataTable dtcities = spm.getDataList(spars, "SP_TimesheetCheckProjectANDTask");

            if (dtcities != null)
            {
                if (dtcities.Rows.Count > 0)
                {
                    blnchk = true;
                }
            }
            return blnchk;
        }
        catch (Exception)
        {
            return blnchk;
        }
    }

    protected void ddl_ProjectName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(ddl_ProjectName.SelectedValue).Trim() != "0")
            {

                if (Convert.ToString(txtFromdate.Text).Trim() == "")
                {
                    lblmessage.Text = "Please select Travel From Date";
                    return;
                }

                if (Convert.ToString(txtToDate.Text).Trim() == "")
                {
                    lblmessage.Text = "Please select Travel To Date";
                    return;
                }


                ddl_DeptName.SelectedValue = "0";
                String[] stremp;
                stremp = Convert.ToString(ddl_ProjectName.SelectedItem.Text).Split('/');
                //Check Is Project Existix Or Not
                if (!checkIsExistProject(stremp[1]))
                {
                    ddl_ProjectName.SelectedValue = "0";
                    lblmessage.Text = "Please select project name in list only";
                    return;
                }


                btn_Draft.Visible = true;
                trvl_btnSave.Visible = true;

                if (!Convert.ToString(stremp[1]).Contains("Head Office"))
                {
                    ddl_DeptName.Enabled = false;
                }

                if (FromDateValidation_exp() == true)
                {
                    btn_Draft.Visible = false;
                    trvl_btnSave.Visible = false;
                    return;
                }

                //if (Txt_ProjectName.Text.Trim() == "Head Office")
                if (Convert.ToString(stremp[1]).Contains("Head Office"))
                {
                    ddl_DeptName.Enabled = true;
                }
                else
                {
                    ddl_DeptName.Enabled = false;
                    ddl_DeptName.SelectedValue = "0";
                    if (chkIsNewJoinExp_req.Checked)
                    {
                        getApproverlist_Newjoinee();
                    }
                    else
                    {
                        getApproverlist();
                    }
                }
            }


        }
        catch (Exception)
        {

            throw;
        }
    }


    protected void chkIsNewJoinExp_req_CheckedChanged(object sender, EventArgs e)
    {
        if (chkIsNewJoinExp_req.Checked)
        {
            getApproverlist_Newjoinee();
        }
        else
        {
            getApproverlist_New();
            getApproverdata();
        }
    }


    #region New Joinee Expenses methods
    private void check_isEmployee_NewJoinee()
    {
        try
        {
            DataSet lds = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_trvl_forNewJoinee";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

            lds = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");
            chkIsNewJoinExp_req.Visible = false;
            if (lds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(lds.Tables[0].Rows[0]["checknewjoinee"]).Trim() == "yes")
                {
                    chkIsNewJoinExp_req.Visible = true;
                }
            }

        }
        catch (Exception ex)
        {

        }
    }

    private void getApproverlist_Newjoinee()
    {
        var getcompSelectedText = ddl_ProjectName.SelectedItem.Text;
        var getcomp_code = Convert.ToString(ddl_ProjectName.SelectedValue);
        var Dept_id = 0;
        if (getcompSelectedText.Contains("Head Office"))
        {
            Dept_id = Convert.ToInt32(ddl_DeptName.SelectedValue);
        }
        string appr_type = "newjoinee";
        DataTable dtapprover = new DataTable();
        dtapprover = spm.GetExpenseApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnexp_id.Value), Convert.ToInt32(hdnTravelConditionid.Value), appr_type.ToString(), getcomp_code, Dept_id);
        lstApprover.Items.Clear();
        dspaymentVoucher_Apprs.Tables.Add(dtapprover);
        if (dtapprover.Rows.Count > 0)
        {
            hdnApprEmailaddress.Value = Convert.ToString(dtapprover.Rows[0]["Emp_Emailaddress"]);
            hdnApprId.Value = Convert.ToString(dtapprover.Rows[0]["APPR_ID"]);
            hflapprcode.Value = Convert.ToString(dtapprover.Rows[0]["A_EMP_CODE"]);

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
    #endregion  

    protected void ddl_DeptName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_ProjectName.SelectedIndex > 0)
            {
                if (chkIsNewJoinExp_req.Checked)
                {
                    getApproverlist_Newjoinee();
                }
                else
                {
                    getApproverlist();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void getExpensedtls_from_Main()
    {
        ////DataTable dtTrDetails = new DataTable();
        ////dtTrDetails = spm.getExpenseDetails_FromMain(Convert.ToDecimal(hdnExpid.Value));

        ////gvexpensdtls.DataSource = null;
        ////gvexpensdtls.DataBind();

        ////if (dtTrDetails.Rows.Count > 0)
        ////{
        ////    gvexpensdtls.DataSource = dtTrDetails;
        ////    gvexpensdtls.DataBind();
        ////}
        DataTable dtTrDetails = new DataTable();
        //dtTrDetails = spm.getExpenseDetails_FromMain(Convert.ToDecimal(hdnExpid.Value));


        DataSet dsExpDtls = new DataSet();
        dsExpDtls = spm.getExpenseDetails_FromMain_Dataset(Convert.ToDecimal(hdnexp_id.Value));
        grdExpAccApproved.DataSource = null;
        grdExpAccApproved.DataBind();

        if (dsExpDtls.Tables[0].Rows.Count > 0)
        {
            // dsExpDtls.Tables[0].Rows.Add();
            Decimal dpaidbyComp = 0;
            Decimal dpaidbyEmp = 0;
            Decimal dTotClaimAmt = 0;
            Decimal dpaidbyCompAcc = 0;
            Decimal dpaidbyEmpAcc = 0;
            Int32 irow = 0;
            for (irow = 0; irow < dsExpDtls.Tables[0].Rows.Count; irow++)
            {
                if (Convert.ToString(dsExpDtls.Tables[0].Rows[irow]["paid_by_comp"]).Trim() != "")
                {
                    dpaidbyComp += Convert.ToDecimal(dsExpDtls.Tables[0].Rows[irow]["paid_by_comp"]);
                }

                if (Convert.ToString(dsExpDtls.Tables[0].Rows[irow]["paid_emp"]).Trim() != "")
                {
                    dpaidbyEmp += Convert.ToDecimal(dsExpDtls.Tables[0].Rows[irow]["paid_emp"]);
                }

                if (Convert.ToString(dsExpDtls.Tables[0].Rows[irow]["totamt"]).Trim() != "")
                {
                    dTotClaimAmt += Convert.ToDecimal(dsExpDtls.Tables[0].Rows[irow]["totamt"]);
                }

                if (Convert.ToString(dsExpDtls.Tables[0].Rows[irow]["amtRelAccPayCom"]).Trim() != "")
                {
                    dpaidbyCompAcc += Convert.ToDecimal(dsExpDtls.Tables[0].Rows[irow]["amtRelAccPayCom"]);
                }

                if (Convert.ToString(dsExpDtls.Tables[0].Rows[irow]["amtRelAccPayEmp"]).Trim() != "")
                {
                    dpaidbyEmpAcc += Convert.ToDecimal(dsExpDtls.Tables[0].Rows[irow]["amtRelAccPayEmp"]);
                }
            }

            CultureInfo hindi = new CultureInfo("hi-IN");
            string paidByCom = "", paidByEmp = "", totalAmt = "", paidByComAcc = "", paidByEmpAcc = "";

            paidByCom = string.Format(hindi, "{0:#,#0.00}", dpaidbyComp);

            paidByEmp = string.Format(hindi, "{0:#,#0.00}", dpaidbyEmp);

            totalAmt = string.Format(hindi, "{0:#,#0.00}", dTotClaimAmt);

            paidByComAcc = string.Format(hindi, "{0:#,#0.00}", dpaidbyCompAcc);

            paidByEmpAcc = string.Format(hindi, "{0:#,#0.00}", dpaidbyEmpAcc);

            dsExpDtls.Tables[0].Rows.Add();
            dsExpDtls.Tables[0].Rows[irow]["paid_by_comp"] = Convert.ToString(paidByCom);
            dsExpDtls.Tables[0].Rows[irow]["paid_emp"] = Convert.ToString(paidByEmp);
            dsExpDtls.Tables[0].Rows[irow]["totamt"] = Convert.ToString(totalAmt);
            dsExpDtls.Tables[0].Rows[irow]["amtRelAccPayCom"] = Convert.ToString(paidByComAcc);
            dsExpDtls.Tables[0].Rows[irow]["amtRelAccPayEmp"] = Convert.ToString(paidByEmpAcc);

            dtTrDetails = dsExpDtls.Tables[0];
            grdExpAccApproved.DataSource = dtTrDetails;
            grdExpAccApproved.DataBind();
        }
    }

    protected void grdExpAccApproved_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Color clrdisable = ColorTranslator.FromHtml("#ebebe4");
            Color clrForeCTotRow = ColorTranslator.FromHtml("#febf39");
            Color clrTotRow = ColorTranslator.FromHtml("#656b67");
            string strtrpdtlsid = "";
            strtrpdtlsid = Convert.ToString(grdExpAccApproved.DataKeys[e.Row.RowIndex][3]).Trim();
            if (Convert.ToString(strtrpdtlsid) == "")
            {
                e.Row.BackColor = clrTotRow;
                //e.Row.BackColor = clrdisable;
                e.Row.ForeColor = clrForeCTotRow;

            }
        }
    }

    protected void btn_Draft_Click(object sender, EventArgs e)
    {
        try
        {
            String[] stremp;
            stremp = Convert.ToString(ddl_ProjectName.SelectedItem.Text).Split('/');
            string[] strdate;
            string strfromDate = "";
            string strToDate = "";
            if (FromDateValidation_exp() == true)
            {
                return;
            }
            #region Check For Blank Fields
            lblmessage.Text = "";

            if (gvexpensdtls.Rows.Count == 0)
            {
                lblmessage.Text = "Please enter any one details.";
                return;
            }

            if (Convert.ToString(txtTotAmtClaimed.Text).Trim() == "" && Convert.ToString(txtTotAmtClaimed.Text).Trim() == "0")
            {
                lblmessage.Text = "Please calculate the Expense by clicking the Calculate button";
                return;
            }
            if (Convert.ToString(lstTripType.SelectedValue).Trim() == "" || Convert.ToString(lstTripType.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Travel Type";
                return;
            }

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
            if (Convert.ToString(ddl_ProjectName.SelectedItem.Text).Trim() == "" || Convert.ToString(ddl_ProjectName.SelectedItem.Text).Trim() == "0")
            {
                lblmessage.Text = "Please select Project Name";
                return;
            }
            else
            {
                //if (Convert.ToString(Txt_ProjectName.Text).Trim() == "Head Office")
                if (Convert.ToString(ddl_ProjectName.SelectedItem).Contains("Head Office"))
                {
                    if (Convert.ToString(ddl_DeptName.SelectedItem.Text).Trim() == "" || Convert.ToString(ddl_DeptName.SelectedItem.Text).Trim() == "0")
                    {
                        lblmessage.Text = "Please select Department Name";
                        return;
                    }
                }
            }
            #endregion


            Boolean blnchk = false;
            #region check Expenses is Submitted

            foreach (GridViewRow irow in dgTravelRequest.Rows)
            {
                Label lblSubmitStatus = (Label)irow.FindControl("lblSubmitStatus");
                if (lblSubmitStatus.Text == "No")
                {


                    irow.BackColor = Color.Yellow;
                    blnchk = true;
                }
            }
            foreach (GridViewRow irow in dgAccomodation.Rows)
            {
                Label lblSubmitStatus = (Label)irow.FindControl("lblSubmitStatus");
                if (lblSubmitStatus.Text == "No")
                {
                    irow.BackColor = Color.Yellow;
                    blnchk = true;
                }
            }
            foreach (GridViewRow irow in dgLocalTravel.Rows)
            {
                Label lblSubmitStatus = (Label)irow.FindControl("lblSubmitStatus");
                if (lblSubmitStatus.Text == "No")
                {
                    irow.BackColor = Color.Yellow;
                    blnchk = true;
                }
            }
            if (blnchk == true)
            {


                lblmessage.Text = "Please Submit details for highlighted records";
                return;
            }
            #endregion

            #region Not required

            /*
             if (Convert.ToString(lstTripType.SelectedValue).Trim() == "1" && Convert.ToString(hdnTripid.Value).Trim()!="0")
             {
                 if (gvexpensdtls.Rows.Count > 0)
                 {

                     Decimal dpaidemp = 0;
                     Decimal dpaidcomp = 0;
                     Decimal dtotamt = 0;
                     Decimal dEligibiltyamt = 0;
                     Decimal ddiffyamt = 0;


                     for (Int32 irow = 0; irow < gvexpensdtls.Rows.Count; irow++)
                     {
                         dpaidemp = 0;
                         dpaidcomp = 0;
                         dtotamt = 0;
                         dEligibiltyamt = 0;
                         ddiffyamt = 0;
                         blnchk = false;

                         #region code not required
                         //if (Convert.ToString(gvexpensdtls.Rows[irow].Cells[4].Text).Trim() != "" && Convert.ToString(gvexpensdtls.Rows[irow].Cells[4].Text).Trim() != "&nbsp;")
                         //{
                         //    dpaidcomp = Convert.ToDecimal(gvexpensdtls.Rows[irow].Cells[4].Text);
                         //}
                         //if (Convert.ToString(gvexpensdtls.Rows[irow].Cells[5].Text).Trim() != "" && Convert.ToString(gvexpensdtls.Rows[irow].Cells[5].Text).Trim() != "&nbsp;")
                         //{
                         //    dpaidemp = Convert.ToDecimal(gvexpensdtls.Rows[irow].Cells[5].Text);
                         //}

                         //if (dpaidemp == dpaidcomp)
                         //{
                         //    if (dpaidemp == 0 && dpaidcomp == 0)
                         //    {
                         //        //Convert.ToString(gvexpensdtls.Rows[irow].Cells[5].Text).Trim() 
                         //        lblmessage.Text = "Please enter " + Convert.ToString(gvexpensdtls.Rows[irow].Cells[3].Text).Trim() + " expenses.";
                         //        return;
                         //    }
                         //}
                         #endregion
                         if (Convert.ToString(gvexpensdtls.Rows[irow].Cells[8].Text).Trim() != "" && Convert.ToString(gvexpensdtls.Rows[irow].Cells[8].Text).Trim() != "&nbsp;")
                         {
                             ddiffyamt = Convert.ToDecimal(gvexpensdtls.Rows[irow].Cells[8].Text);
                         }


                         if (ddiffyamt == 0)
                         {
                             #region if not Accommodation
                             if (Convert.ToString(gvexpensdtls.Rows[irow].Cells[3].Text).Trim() != "Accommodation")
                             {
                                 lblmessage.Text = "Please enter " + Convert.ToString(gvexpensdtls.Rows[irow].Cells[3].Text).Trim() + " expenses.";
                                 return;
                             }
                             #endregion

                             #region if Only Accommodation
                             if (Convert.ToString(gvexpensdtls.Rows[irow].Cells[3].Text).Trim() == "Accommodation")
                             {
                                 if (Convert.ToString(gvexpensdtls.Rows[irow].Cells[8].Text).Trim() == "" || Convert.ToString(gvexpensdtls.Rows[irow].Cells[8].Text).Trim() == "&nbsp;")
                                 {
                                     if (Convert.ToString(gvexpensdtls.Rows[irow].Cells[8].Text).Trim() != "0" )
                                     {
                                         lblmessage.Text = "Please enter " + Convert.ToString(gvexpensdtls.Rows[irow].Cells[3].Text).Trim() + " expenses.";
                                         return;
                                     }
                                 }
                             }
                             #endregion
                         }

                     }
                 }
             }
              */
            #endregion


            if (ploadexpfile.HasFiles)
            {
                //lblmessage.Text = "Please Enter reason For Travel";
                //return;
            }
            else
            {
                //if (gvuploadedFiles.Rows.Count <= 0)
                //{
                //    lblmessage.Text = "Please Upload Expenses Files.";
                //    return;
                //}
            }


            //if (Convert.ToString(Session["chkbtnStatus"]).Trim() != "")
            //{
            //    Response.Redirect("~/procs/travelindex.aspx");
            //}
            //Session["chkbtnStatus"] = "Travel Expenses Request button Event is Submitted";

            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "Confirm();", true);
            //ClientScript.RegisterStartupScript(this.GetType(),"alert","Confirm();",true);

            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                //lblmessage.Text = "Not Saved";
                return;
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

            decimal Daily_Halting_allowance = 0, TotalAmount_Claimed = 0, LessAdvanceTaken = 0, Net_Pay_Company = 0, Net_Pay_Employee = 0;
            string Reason_Deviation = "", Upload_File = "";

            if (Convert.ToString(txtdailyhaltingallowance.Text).Trim() != "")
                Daily_Halting_allowance = Convert.ToDecimal(txtdailyhaltingallowance.Text);

            if (Convert.ToString(txtTotAmtClaimed.Text).Trim() != "")
                TotalAmount_Claimed = Convert.ToDecimal(txtTotAmtClaimed.Text);

            if (Convert.ToString(txtLessAdvTaken.Text).Trim() != "")
                LessAdvanceTaken = Convert.ToDecimal(txtLessAdvTaken.Text);

            if (Convert.ToString(txtnetPaybltoComp.Text).Trim() != "")
                Net_Pay_Company = Convert.ToDecimal(txtnetPaybltoComp.Text);

            if (Convert.ToString(txtnetPaybltoEmp.Text).Trim() != "")
                Net_Pay_Employee = Convert.ToDecimal(txtnetPaybltoEmp.Text);

            if (Convert.ToString(txtReasonDeviation.Text).Trim() != "")
                Reason_Deviation = Convert.ToString(txtReasonDeviation.Text).Trim();


            if (Convert.ToString(ploadexpfile.FileName).Trim() != "")
                Upload_File = Convert.ToString(ploadexpfile.FileName).Trim();

            SqlParameter[] spars = new SqlParameter[23];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "Insert";

            spars[1] = new SqlParameter("@trip_type", SqlDbType.Int);
            if (Convert.ToString(lstTripType.SelectedValue).Trim() != "")
                spars[1].Value = Convert.ToInt32(lstTripType.SelectedValue);
            //spars[1].Value = Convert.ToInt32(hdnTraveltypeid.Value);

            spars[2] = new SqlParameter("@trip_frm_date", SqlDbType.VarChar);
            spars[2].Value = strfromDate;

            spars[3] = new SqlParameter("@trip_to_date", SqlDbType.VarChar);
            spars[3].Value = strToDate;

            spars[4] = new SqlParameter("@exp_id", SqlDbType.Decimal);
            if (Convert.ToString(hdnexp_id.Value).Trim() != "0")
                spars[4].Value = hdnexp_id.Value;
            else
                spars[4].Value = DBNull.Value;

            spars[5] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[5].Value = Convert.ToString(txtEmpCode.Text).Trim();

            spars[6] = new SqlParameter("@trip_status", SqlDbType.Int);
            spars[6].Value = status;

            spars[7] = new SqlParameter("@req_adv_amt", SqlDbType.Decimal);
            if (Convert.ToString(txtAdvance.Text).Trim() != "")
                spars[7].Value = Convert.ToDecimal(txtAdvance.Text);
            else
                spars[7].Value = DBNull.Value;

            spars[8] = new SqlParameter("@currency_type", SqlDbType.VarChar);
            spars[8].Value = reqcurrency;

            spars[9] = new SqlParameter("@Daily_Halting_allowance", SqlDbType.Decimal);
            spars[9].Value = Daily_Halting_allowance;

            spars[10] = new SqlParameter("@TotalAmount_Claimed", SqlDbType.Decimal);
            spars[10].Value = TotalAmount_Claimed;

            spars[11] = new SqlParameter("@LessAdvanceTaken", SqlDbType.Decimal);
            spars[11].Value = LessAdvanceTaken;

            spars[12] = new SqlParameter("@Net_Pay_Company", SqlDbType.Decimal);
            spars[12].Value = Net_Pay_Company;

            spars[13] = new SqlParameter("@Net_Pay_Employee", SqlDbType.Decimal);
            spars[13].Value = Net_Pay_Employee;

            spars[14] = new SqlParameter("@Reason_Deviation", SqlDbType.VarChar);
            spars[14].Value = Reason_Deviation;

            spars[15] = new SqlParameter("@Upload_File", SqlDbType.VarChar);
            spars[15].Value = Upload_File;

            spars[16] = new SqlParameter("@trip_id", SqlDbType.Decimal);
            spars[16].Value = hdnTripid.Value;

            spars[17] = new SqlParameter("@trp_reason", SqlDbType.VarChar);
            spars[17].Value = Convert.ToString(txtReason.Text).Trim();

            spars[18] = new SqlParameter("@conditionid", SqlDbType.Decimal);
            spars[18].Value = hdnTravelConditionid.Value;

            spars[19] = new SqlParameter("@ProjectName", SqlDbType.VarChar);
            spars[19].Value = Convert.ToString(stremp[1].Trim());

            spars[20] = new SqlParameter("@DeptName", SqlDbType.VarChar);
            spars[20].Value = Convert.ToString(ddl_DeptName.SelectedValue);

            spars[21] = new SqlParameter("@IsNewjoinee", SqlDbType.Bit);
            if (chkIsNewJoinExp_req.Checked)
                spars[21].Value = 1;
            else
                spars[21].Value = 0;

            spars[22] = new SqlParameter("@IsDraft", SqlDbType.Bit);
            spars[22].Value = 1;

            DataTable dtMaxTripID = spm.InsertorUpdateData(spars, "SP_SAVE_Exp_TRAVELREQUEST_Main");



            //DataTable dtMaxTripID = spm.InsertExpenseTravelRequest_main(Convert.ToInt32(hdnTraveltypeid.Value), strfromDate, strToDate,Convert.ToString(txtReason.Text).Trim(), Convert.ToString(txtEmpCode.Text).Trim(), status, Convert.ToInt32(txtAdvance.Text), reqcurrency,Daily_Halting_allowance, TotalAmount_Claimed, LessAdvanceTaken, Net_Pay_Company, Net_Pay_Employee, Reason_Deviation, Upload_File);
            Session["TripTypeId"] = null;
            Session["TravelType"] = null;
            decimal maxtripid = 0; //= Convert.ToInt32(dtMaxTripID.Rows[0]["maxtripid"]);
            if (dtMaxTripID.Rows.Count > 0)
            {
                maxtripid = Convert.ToInt32(dtMaxTripID.Rows[0]["maxtripid"]);
                hdnvouno.Value = Convert.ToString(dtMaxTripID.Rows[0]["vouno"]);
            }
            if (maxtripid == 0)
            {
                return;
            }
            // spm.InsertExpenseApproverDetails(hflapprcode.Value, Convert.ToInt32(hdnApprId.Value), maxtripid);
            //UploadFiles(maxtripid);

            #region insert or upload multiple files
            if (ploadexpfile.HasFile)
            {
                string filename = ploadexpfile.FileName;
                //uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["FuelClaimsDocumentpath"]).Trim()), uploadfile.FileName));
                if (Convert.ToString(filename).Trim() != "")
                {
                    #region date formatting
                    if (Convert.ToString(txtFromdate.Text).Trim() != "")
                    {
                        strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                        strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
                    }
                    #endregion


                    string FuelclaimPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["trvlingExpfiles"]).Trim() + "/");
                    bool folderExists = Directory.Exists(FuelclaimPath);
                    if (!folderExists)
                    {
                        Directory.CreateDirectory(FuelclaimPath);
                    }


                    ////string[] Files = Directory.GetFiles(FuelclaimPath);
                    ////foreach (string file in Files)
                    ////{
                    ////    File.Delete(file);
                    ////}
                    Int32 t_cnt = spm.getTravelUploaded_Files(maxtripid);

                    Boolean blnfile = true;
                    HttpFileCollection fileCollection = Request.Files;
                    for (int i = 0; i < fileCollection.Count; i++)
                    {
                        string strfileName = "";

                        HttpPostedFile uploadfileName = fileCollection[i];
                        string fileName = ReplaceInvalidChars(Path.GetFileName(uploadfileName.FileName));
                        if (uploadfileName.ContentLength > 0)
                        {
                            // strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + Convert.ToString(i + 1).Trim() + "_" + uploadfileName.FileName;
                            String InputFile = System.IO.Path.GetExtension(uploadfileName.FileName);
                            //strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + Convert.ToString(t_cnt + 1).Trim() + "_FuelClaim" + (t_cnt + 1).ToString() + InputFile;
                            strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + fileName;
                            filename = strfileName;
                            //uploadfileName.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["FuelClaimsDocumentpath"]).Trim()), strfileName));
                            uploadfileName.SaveAs(Path.Combine(FuelclaimPath, strfileName));


                            //spm.InsertFuelUploaded_Files(maxRemid, blnfile,Convert.ToString( Convert.ToString(maxRemid) + "/" +  strfileName).Trim(),"FuelClaim",i+1);
                            spm.InsertFuelUploaded_Files(maxtripid, blnfile, Convert.ToString(strfileName).Trim(), "expenses", t_cnt + 1);
                            //spm.getFuelUploaded_Files(maxtripid);
                            //blnfile = true;
                            t_cnt = t_cnt + 1;
                        }
                    }

                }


            }
            getExpenseUploadedFiles();
            #endregion
            lblmessage.Visible = true;
            lblmessage.Text = "Travel Expense Save As Draft Successfully";

            //getFomatated dates for Email
            getFromdateTodate_FroEmail();

            String strTrvlrestURL = "";
            strTrvlrestURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_TE"]).Trim() + "?expid=" + maxtripid + "&stype=APP";

            #region SaveTravel Expenses Code

            //spm.send_mailto_RM_ExpensesApprover(txtEmpName.Text, hflEmailAddress.Value, hdnApprEmailaddress.Value, "Request for Travel Expenses - " + hdnvouno.Value.ToString(), txtTriptype.Text, hdnActualTrvlDays.Value, txtReason.Text, Convert.ToString(hdnfrmdate_emial.Value).Trim(), Convert.ToString(hdntodate_emial.Value).Trim(), "", GetApprove_RejectList(maxtripid), GetIntermediatesList_Formail(), strTrvlrestURL);


            string strapprovermails = "";
            //  strapprovermails = get_approverlist_ifTD_COS(maxtripid);
            //if (Convert.ToString(strapprovermails).Trim()!="")
            //spm.Expense_send_mailto_Intermediate(hflEmailAddress.Value, strapprovermails, "Request for Travel Expenses", txtTriptype.Text, hdnActualTrvlDays.Value, txtReason.Text, Convert.ToString(hdnfrmdate_emial.Value).Trim(), Convert.ToString(hdntodate_emial.Value).Trim(), GetApprove_RejectList(maxtripid), txtEmpName.Text, GetIntermediatesList());


            Response.Redirect("~/procs/travelindex.aspx");
            ClearControls();
            #endregion
        }
        catch (Exception ex)
        {

        }
    }

    public bool GetStatusDraft()
    {
        var IsDraft = false;
        try
        {
            var getDetails = spm.Get_TravelIsDraftStatus("CheckIsDraft", Convert.ToString(Session["Empcode"]).Trim(), 0);
            if (getDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getDetails.Rows[0]["IsDraftStatus"]);
                if (getStatus == "Yes")
                {
                    IsDraft = true;
                    hdnTripid.Value = Convert.ToString(getDetails.Rows[0]["trip_id"]).Trim();
                    hdnexp_id.Value = Convert.ToString(getDetails.Rows[0]["exp_id"]).Trim();
                    hdnIsDraft.Value = "1";
                }
            }
        }
        catch (Exception ex)
        {
            IsDraft = false;
        }
        return IsDraft;
    }

    private void getApproverlist_Draft()
    {
        try
        {
            lblmessage.Text = "";
            var getcompSelectedText = ddl_ProjectName.SelectedItem.Text;
            var getcomp_code = Convert.ToString(ddl_ProjectName.SelectedValue);
            var Dept_id = 0;
            if (getcompSelectedText.Contains("Head Office"))
            {
                Dept_id = Convert.ToInt32(ddl_DeptName.SelectedValue);
            }
            DataTable dtapprover = new DataTable();
            if (hdnIsDraft.Value == "1")
            {
                getcomp_code = Convert.ToString(hdncomp_code.Value);
            }
            dtapprover = spm.GetTravelApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnTripid.Value), Convert.ToInt32(hdnTravelConditionid.Value), getcomp_code, Dept_id);
            lstApprover.Items.Clear();
            if (dtapprover.Rows.Count > 0)
            {
                lstApprover.DataSource = dtapprover;
                lstApprover.DataTextField = "names";
                lstApprover.DataValueField = "names";
                lstApprover.DataBind();

                hdnApprEmailaddress.Value = Convert.ToString(dtapprover.Rows[0]["Emp_Emailaddress"]);
                hdnApprId.Value = Convert.ToString(dtapprover.Rows[0]["APPR_ID"]);
                hflapprcode.Value = Convert.ToString(dtapprover.Rows[0]["A_EMP_CODE"]);
            }
            else
            {
                lblmessage.Text = "Since you are not assigned under any approver you can not apply for any type of Travel request, please contact HR";


            }
        }
        catch (Exception ex)
        {
        }
    }
}