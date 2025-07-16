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
using Microsoft.Reporting.WebForms;


public partial class myaccount_ApprovedTravelReqst_Acc : System.Web.UI.Page
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

            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/travelindex.aspx");
            }
            else
            {
                Page.SmartNavigation = true;

                if (!Page.IsPostBack)
                {
                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["trvlingExpfiles"]).Trim());
                    lblmessage.Text = "";
                    hndloginempcode.Value = Convert.ToString(Session["Empcode"]);
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
                        // hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdnExpid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdnInboxType.Value = Convert.ToString(Request.QueryString[1]).Trim();
                    }

                    if (Convert.ToString(hdnExpid.Value).Trim() != "")
                    {
                        getTravelRequestData();
                        getExpensedtls_from_Main();
                        getExpenseUploadedFiles();
                        getMainExpenseTravelDetails();
                        getMainExpenseAccomodationDetails();
                        getMainExpenseLcoalTravel();
                    }
                    else
                    {
                        getTravelRequestData();
                    }


                    //GetCuurentApprID();
                    getApproverlist();
                    //getIntermidateslist();
                    // getnextAppIntermediate();




                    editform.Visible = true;
                    divbtn.Visible = false;
                    //divmsg.Visible = false;
                    if (Convert.ToString(hdnInboxType.Value).Trim() == "ACC")
                    {
                        btnReject.Visible = false;
                        btnCorrection.Visible = false;
                    }
                    if (Convert.ToString(txtAdvance.Text).Trim() != "")
                    {
                        if (Convert.ToString(txtTriptype.Text).Trim() == "Domestic" || Convert.ToString(txtTriptype.Text).Trim() == "")
                        {
                            lbl_adv.Visible = true;
                            txtAdvance.Visible = true;
                            lbl_cur.Visible = false;
                            txtreqCur.Visible = false;
                        }
                    }
                    else
                    {
                        lbl_adv.Visible = false;
                        lbl_cur.Visible = false;
                        txtAdvance.Visible = false;
                        txtreqCur.Visible = false;
                    }

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    protected void lnkviewfile_Click(object sender, EventArgs e)
    {
        try
        {
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
        dtIntermediate = spm.GetExpenseTravelIntermediateName(txtEmpCode.Text, Convert.ToInt32(hdnTravelConditionid.Value), hflGrade.Value);
        if (dtIntermediate.Rows.Count > 0)
        {
            lstIntermediate.DataSource = dtIntermediate;
            lstIntermediate.DataTextField = "Emp_Name";
            lstIntermediate.DataValueField = "A_EMP_CODE";
            // lstIntermediate.DataValueField = "APPR_ID";
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
                hflEmpName.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
                hflEmpDesignation.Value = Convert.ToString(dtEmpDetails.Rows[0]["DesginationName"]);
                hflEmpDepartment.Value = Convert.ToString(dtEmpDetails.Rows[0]["Department_Name"]);
                hflGrade.Value = Convert.ToString(dtEmpDetails.Rows[0]["Grade"]).Trim();
                hflloginApprEmail.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);

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
    public void getMainExpenseTravelDetails()
    {
        DataTable dtTrDetails = new DataTable();
        //dtTrDetails = spm.GetMainTravelDetails(Convert.ToInt32(hdnExpid.Value));
        dtTrDetails = spm.GetMain_expTravelDetails(Convert.ToInt32(hdnExpid.Value));


        dgTravelRequest.DataSource = null;
        dgTravelRequest.DataBind();

        if (dtTrDetails.Rows.Count > 0)
        {
            dgTravelRequest.DataSource = dtTrDetails;
            dgTravelRequest.DataBind();
        }
    }
    public void getMainExpenseAccomodationDetails()
    {
        DataTable dtTrDetails = new DataTable();
        // dtTrDetails = spm.GetMainAccomodationDetails(Convert.ToInt32(hdnTripid.Value));
        dtTrDetails = spm.GetMain_ExpAccomodationDetails(Convert.ToInt32(hdnExpid.Value));

        dgAccomodation.DataSource = null;
        dgAccomodation.DataBind();

        if (dtTrDetails.Rows.Count > 0)
        {

            //txtAdvance.Text = Convert.ToString(Session["Advance"]);
            //hdnAccReq.Value = Convert.ToString(dtTrDetails.Rows[0]["local_accomodation_remarks"]);
            //hdnAccCOS.Value = Convert.ToString(dtTrDetails.Rows[0]["travel_through_desk"]);
            //hdnAcctripid.Value = Convert.ToString(dtTrDetails.Rows[0]["trip_id"]);

            //if (Convert.ToString(txtreqCur.Text).Trim() != "")
            //{
            //    txtreqCur.Text = Convert.ToString(Session["Currency"]); ;
            //}
            dgAccomodation.DataSource = dtTrDetails;
            dgAccomodation.DataBind();
        }
    }
    public void getMainExpenseLcoalTravel()
    {
        DataTable dtTrDetails = new DataTable();
        //dtTrDetails = spm.GetMainLocalTrvlDetails(Convert.ToInt32(hdnTripid.Value));
        dtTrDetails = spm.GetMain_ExpLocalTrvlDetails(Convert.ToInt32(hdnExpid.Value));

        dgLocalTravel.DataSource = null;
        dgLocalTravel.DataBind();

        if (dtTrDetails.Rows.Count > 0)
        {

            dgLocalTravel.DataSource = dtTrDetails;
            dgLocalTravel.DataBind();
        }
    }
    public void getTravelRequestData()
    {
        DataTable dtTrDetails = new DataTable();
        dtTrDetails = spm.GetTravelExpenseData(Convert.ToInt32(hdnExpid.Value));

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
            txtdailyhaltingallowance.Text = Convert.ToString(dtTrDetails.Rows[0]["Daily_Halting_allowance"]);
            txtLessAdvTaken.Text = Convert.ToString(dtTrDetails.Rows[0]["LessAdvanceTaken"]);
            txtnetPaybltoComp.Text = Convert.ToString(dtTrDetails.Rows[0]["Net_Pay_Company"]);
            txtnetPaybltoEmp.Text = Convert.ToString(dtTrDetails.Rows[0]["Net_Pay_Employee"]);
            txtReasonDeviation.Text = Convert.ToString(dtTrDetails.Rows[0]["Reason_Deviation"]);
            txtTotAmtClaimed.Text = Convert.ToString(dtTrDetails.Rows[0]["TotalAmount_Claimed"]);


            hdnActualTrvlDays.Value = Convert.ToString(dtTrDetails.Rows[0]["Days"]);
            lblheading.Text = "Travel Expense Voucher - " + Convert.ToString(dtTrDetails.Rows[0]["Vouno"]);
            txtAccountAmount.Text = Convert.ToString(dtTrDetails.Rows[0]["Amt_Release_By_Acc"]);
            txtAccountRemark.Text = Convert.ToString(dtTrDetails.Rows[0]["Remark_By_Acc"]);
            Txt_ProjectName.Text = Convert.ToString(dtTrDetails.Rows[0]["Project_Name"]);
            Txt_DeptName.Text = Convert.ToString(dtTrDetails.Rows[0]["Dept_Name"]);

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
            hdncomp_code.Value = Convert.ToString(dtTrDetails.Rows[0]["comp_code"]);
            hdndept_Id.Value = Convert.ToString(dtTrDetails.Rows[0]["dept_id"]);

            if (hdnTravelConditionid.Value == "2" || hdnTravelConditionid.Value == "4")
                chk_exception.Checked = true;
            else
                chk_exception.Checked = false;
        }
    }
    protected void mobile_btnPrintPV_Click(object sender, EventArgs e)
    {
        getApproverlist();
        getPayementVoucher_forPrint();
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
            spars[1].Value = Convert.ToDecimal(hdnExpid.Value);

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
    public void AssigningSessions()
    {
        Session["TravelType"] = txtTriptype.Text;
        Session["Fromdate"] = txtFromdate.Text;
        Session["Todate"] = txtToDate.Text;
        Session["ReqEmpCode"] = txtEmpCode.Text;
        //   Session["TravelTypeID"] = hdnTraveltypeid.Value;
        Session["Grade"] = hflGrade.Value;
        hdnTripid.Value = "0";
        Session["TripID"] = hdnTripid.Value;

    }
    private void getApproverlist()
    {
        string appr_type = "APP";
        if (Convert.ToString(hdnInboxType.Value).Trim() == "ACC")
        {
            appr_type = "ACC";
        }
        var getcompSelectedText = Txt_ProjectName.Text;
        var getcomp_code = Convert.ToString(hdncomp_code.Value);
        var Dept_id = 0;
        if (getcompSelectedText.Contains("Head Office"))
        {
            Dept_id = Convert.ToInt32(hdndept_Id.Value);
        }
        DataTable dtapprover = new DataTable();
        dtapprover = spm.GetExpenseApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnExpid.Value), Convert.ToInt32(hdnTravelConditionid.Value), appr_type.ToString(), getcomp_code, Dept_id);
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
    protected void GetCuurentApprID()
    {
        int capprid;
        string Actions = "";
        DataTable dtCApprID = new DataTable();
        dtCApprID = spm.ExpenseCurrentApprID(Convert.ToInt32(hdnExpid.Value), hndloginempcode.Value);
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
        var getcompSelectedText = Txt_ProjectName.Text;
        var getcomp_code = Convert.ToString(hdncomp_code.Value);
        var Dept_id = 0;
        if (getcompSelectedText.Contains("Head Office"))
        {
            Dept_id = Convert.ToInt32(hdndept_Id.Value);
        }
        DataTable dsapproverNxt = new DataTable();
        dsapproverNxt = spm.GetExpenseNextApproverDetails(txtEmpCode.Text, Convert.ToInt32(hdnExpid.Value), Convert.ToInt32(hdnTravelConditionid.Value), getcomp_code, Dept_id);
        if (dsapproverNxt.Rows.Count > 0)
        {
            hdnNextApprId.Value = Convert.ToString(dsapproverNxt.Rows[0]["APPR_ID"]);
            hdnNextApprCode.Value = Convert.ToString(dsapproverNxt.Rows[0]["A_EMP_CODE"]);
            hdnNextApprName.Value = Convert.ToString(dsapproverNxt.Rows[0]["Emp_Name"]);
            hdnNextApprEmail.Value = Convert.ToString(dsapproverNxt.Rows[0]["Emp_Emailaddress"]);
            DataTable dtintermediateemail = new DataTable();
            dtintermediateemail = spm.ExpenseNextIntermediateName(Convert.ToInt32(hdnCurrentApprID.Value), txtEmpCode.Text);
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
        dtPreApp = spm.ExpensePreviousApproverDetails(txtEmpCode.Text, Convert.ToInt32(hdnExpid.Value));
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
    protected string getRejectionCorrectionmailList()
    {
        string email_ids = "";
        DataTable dtApproverEmailIds = new DataTable();
        //dtApproverEmailIds = spm.ExpenseApproverDetails_Rejection_cancellation(txtEmpCode.Text, Convert.ToInt32(hdnExpid.Value), "get_ApproverDetails_mail_rejection_correction");
        dtApproverEmailIds = spm.ExpensesTravelPreviousApproverDetails(txtEmpCode.Text, Convert.ToInt32(hdnExpid.Value));

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
        var getcompSelectedText = Txt_ProjectName.Text;
        var getcomp_code = Convert.ToString(hdncomp_code.Value);
        var Dept_id = 0;
        if (getcompSelectedText.Contains("Head Office"))
        {
            Dept_id = Convert.ToInt32(hdndept_Id.Value);
        }
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        string appr_type = "APP";
        if (Convert.ToString(hdnInboxType.Value).Trim() == "ACC")
        {
            appr_type = "ACC";
        }
        dtAppRej = spm.GetExpenseApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnExpid.Value), Convert.ToInt32(hdnTravelConditionid.Value), appr_type.ToString(), getcomp_code, Dept_id);
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
        dsExpDtls = spm.getExpenseDetails_FromMain_Dataset(Convert.ToDecimal(hdnExpid.Value));
        gvexpensdtls.DataSource = null;
        gvexpensdtls.DataBind();

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
            gvexpensdtls.DataSource = dtTrDetails;
            gvexpensdtls.DataBind();
        }
    }
    protected void gvexpensdtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Color clrdisable = ColorTranslator.FromHtml("#ebebe4");
            Color clrForeCTotRow = ColorTranslator.FromHtml("#febf39");
            Color clrTotRow = ColorTranslator.FromHtml("#656b67");
            string strtrpdtlsid = "";
            strtrpdtlsid = Convert.ToString(gvexpensdtls.DataKeys[e.Row.RowIndex][3]).Trim();
            if (Convert.ToString(strtrpdtlsid) == "")
            {
                e.Row.BackColor = clrTotRow;
                //e.Row.BackColor = clrdisable;
                e.Row.ForeColor = clrForeCTotRow;

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
        spars[2].Value = hdnExpid.Value;

        spars[3] = new SqlParameter("@exp_sr_no", SqlDbType.Int);
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

    private string get_approverlist_ifTD_COS()
    {
        string email_ids = "";
        DataTable dtApproverEmailIds = new DataTable();

        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "getPreviousAppr_Interm_mails";

        spars[1] = new SqlParameter("@expid", SqlDbType.Decimal);
        spars[1].Value = hdnExpid.Value;

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = txtEmpCode.Text.ToString();

        spars[3] = new SqlParameter("@ApproverCode", SqlDbType.VarChar);
        spars[3].Value = hndloginempcode.Value;

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

        //ACC Approver Code
        if (dsTrDetails.Tables[2].Rows.Count > 0)
        {
            hdnApprovalACC_Code.Value = Convert.ToString(dsTrDetails.Tables[2].Rows[0]["approver_emp_code"]).Trim();
            hdnApprovalACC_ID.Value = Convert.ToString(dsTrDetails.Tables[2].Rows[0]["app_id"]).Trim();
            hdnApprovalACC_mail.Value = Convert.ToString(dsTrDetails.Tables[2].Rows[0]["approver_emp_mail"]).Trim();
            hdnApprovalACC_Name.Value = Convert.ToString(dsTrDetails.Tables[2].Rows[0]["app_remarks"]).Trim();
            btnReject.Visible = false;
            btnCorrection.Visible = false;
        }

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

    protected void check_ACC_ApprovalStatus()
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_checkCurnt_ACC_Approver";

            spars[1] = new SqlParameter("@expid", SqlDbType.Decimal);
            spars[1].Value = hdnExpid.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = hndloginempcode.Value;

            spars[3] = new SqlParameter("@exp_trvl", SqlDbType.VarChar);
            spars[3].Value = "ACC";


            dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");
            //Account Request Count
            hdnisApproval_ACC_Status.Value = "Approver";

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                hdnisApproval_ACC_Status.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Action"]).Trim();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    private string get_approverlist_ifTD_COS_finalApproval()
    {
        string email_ids = "";
        DataTable dtApproverEmailIds = new DataTable();

        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "getAppr_Interm_mails_finalApprovers";

        spars[1] = new SqlParameter("@expid", SqlDbType.Decimal);
        spars[1].Value = hdnExpid.Value;

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = txtEmpCode.Text.ToString();

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
    #endregion

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request.QueryString.Count > 3)
        {
            if (Convert.ToString(Request.QueryString["batch"]).Trim() == "c")
            {
                Response.Redirect("~/procs/VSCB_CreateBatch.aspx");
            }
            if (Convert.ToString(Request.QueryString["batch"]).Trim() == "asb")
            {
                Response.Redirect("~/procs/VSCB_AssignbankRefApproveBatch.aspx?batchid=" + Convert.ToString(Request.QueryString["batchid"]).Trim());
            }
            if (Convert.ToString(Request.QueryString["batch"]).Trim() == "ap")
            {
                if (Convert.ToString(Request.QueryString["mngexp"]).Trim() == "2")
                    Response.Redirect("~/procs/VSCB_ApproveBatch.aspx?batchid=" + Convert.ToString(Request.QueryString["batchid"]).Trim() + "&mngexp=2");
                else
                    Response.Redirect("~/procs/VSCB_ApproveBatch.aspx?batchid=" + Convert.ToString(Request.QueryString["batchid"]).Trim());
            }
        }

        Response.Redirect("~/procs/InboxTravel_ACC.aspx?stype=" + Convert.ToString(hdnInboxType.Value));
    }
}