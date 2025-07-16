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
using System.Linq;

public partial class Resignation_Req_Arch : System.Web.UI.Page
{

    #region CreativeMethods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    #endregion

    SP_Methods spm = new SP_Methods();
    DataSet dspaymentVoucher_Apprs = new DataSet();
    String CEOInList = "N";
    double YearlymobileAmount = 0;
    #region PageEvents
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {

                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    divbtn.Visible = false;
                    divmsg.Visible = false;
                    hdnempcode.Value = Convert.ToString(Session["Empcode"]);
                    txtFromdate_sub.Enabled = false;
                    Txt_CFO_Recm_Amt.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    GetEmployeeDetails();


                    if (Request.QueryString.Count > 0)
                    {

                        hdnRemid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        // hdnClaimid.Value = Convert.ToString(Request.QueryString[1]).Trim(); 
                        hdnInboxType.Value = Convert.ToString(Request.QueryString[2]).Trim();
                    }

                    if (Convert.ToString(hdnRemid.Value).Trim() != "")
                    {

                        if (Request.QueryString.Count > 1)
                        {
                            //checkApprovalStatus_Submit();
                            getMobRemlsDetails_usingRemid();
                            //   InsertMobileRem_DatatoTempTables_trvl();
                            //getMobileClaimDetails();
                            getMobileClaimUploadedFiles();
                        }
                        //GetCuurentApprID();
                        //getApproverlist();
                        //getApproverdata();
                        //getnextAppIntermediate();

                    }

                    if (Convert.ToString(hdnInboxType.Value).Trim() == "RACC")
                    {
                        Txt_CFO_Recm_Amt.Visible = true;
                        Spn_CFO_Recm_Amt.Visible = true;
                        Txt_CFO_Recm_Amt.Enabled = false;
                    }

                    if (Convert.ToString(hdnInboxType.Value).Trim() == "RCOS" || Convert.ToString(hdnInboxType.Value).Trim() == "RCFO")
                    {
                        txtRecommendation_COS.Visible = true;
                        txtComment.Visible = false;
                        COS_Rem.Visible = false;
                        Txt_COSRecommended.Visible = false;
                        Txt_COSRecommended.Enabled = false;
                    }

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
                else
                {

                }

                Txt_CFO_Recm_Amt.Visible = false;
                Spn_CFO_Recm_Amt.Visible = false;

            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    #endregion

    #region PageMethods

    private void getPayementVoucher_forPrint()
    {
        try
        {


            #region get payment Voucher details
            DataSet dspaymentVoucher = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "mobile_claim";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDecimal(hdnRemid.Value);

            dspaymentVoucher = spm.getDatasetList(spars, "rpt_dataProcedure");

            #endregion

            if (dspaymentVoucher.Tables[0].Rows.Count > 0)
            {
                ReportViewer ReportViewer1 = new ReportViewer();


                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.LocalReport.DataSources.Clear();

                ReportParameter[] param = new ReportParameter[19];
                param[0] = new ReportParameter("pdocno", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Vouno"]));
                param[1] = new ReportParameter("ppvdate", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Rem_Sub_Date"]));
                param[2] = new ReportParameter("pempName", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Emp_Name"]));
                param[3] = new ReportParameter("pempCode", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Empcode"]));
                param[4] = new ReportParameter("pRsinwords", Convert.ToString(dspaymentVoucher.Tables[2].Rows[0]["AmountinWords"]));
                param[7] = new ReportParameter("pBankName", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["tperiod"]));

                #region Cost Cente & Bank Details
                if (dspaymentVoucher.Tables[3].Rows.Count > 0)
                {
                    param[5] = new ReportParameter("pCostCenterCode", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["Cost_Center"]));
                    param[6] = new ReportParameter("pCostCenterNM", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["Cost_Center_desc"]));

                    //param[7] = new ReportParameter("pBankName", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["BANK_name"]));
                    param[8] = new ReportParameter("pBankAccCode", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["BANK_acc"]));
                    param[9] = new ReportParameter("pBankIFSCcode", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["BANK_ifsc"]));
                    param[10] = new ReportParameter("pBankBranch", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["BANK_Branch"]));

                    param[11] = new ReportParameter("pRbnkName", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["R_BANK_name"]));
                    param[12] = new ReportParameter("pRBnkAccCode", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["R_BANK_acc"]));
                    param[13] = new ReportParameter("pRbnkIFSCcode", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["R_BANK_ifsc"]));
                    param[14] = new ReportParameter("pRbnkBranch", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["R_BANK_Branch"]));
                }
                else
                {
                    param[5] = new ReportParameter("pCostCenterCode", Convert.ToString(""));
                    param[6] = new ReportParameter("pCostCenterNM", Convert.ToString(""));

                    //param[7] = new ReportParameter("pBankName", Convert.ToString(""));
                    param[8] = new ReportParameter("pBankAccCode", Convert.ToString(""));
                    param[9] = new ReportParameter("pBankIFSCcode", Convert.ToString(""));
                    param[10] = new ReportParameter("pBankBranch", Convert.ToString(""));

                    param[11] = new ReportParameter("pRbnkName", Convert.ToString(""));
                    param[12] = new ReportParameter("pRBnkAccCode", Convert.ToString(""));
                    param[13] = new ReportParameter("pRbnkIFSCcode", Convert.ToString(""));
                    param[14] = new ReportParameter("pRbnkBranch", Convert.ToString(""));
                }
                #endregion

                param[15] = new ReportParameter("pContact", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["mobile"]));
                param[16] = new ReportParameter("PAlt_Contact", "");
                param[17] = new ReportParameter("pProjectName", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Project_Name"]));
                param[18] = new ReportParameter("pDept_Name", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Dept_Name"]));

                // Create Report DataSource
                ReportDataSource rds = new ReportDataSource("dspaymentVoucher", dspaymentVoucher.Tables[0]);
                ReportDataSource rdsApprs = new ReportDataSource("dspaymentVoucher_Apprs", dspaymentVoucher_Apprs.Tables[0]);

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/PaymentVoucher.rdlc");
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

    private void checkApprovalStatus_Submit()
    {
        try
        {

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "check_ResignationReimbstReqAppr_Status";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDecimal(hdnRemid.Value);

            spars[2] = new SqlParameter("@apprempcode", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(hdnempcode.Value);

            DataTable dtTrDetails = spm.getMobileRemDataList(spars, "[SP_GETALLreembursement_DETAILS]");
            if (dtTrDetails.Rows.Count == 0)
            {
                Response.Redirect("~/procs/InboxResignation.aspx?app=" + hdnInboxType.Value);
            }
            if (dtTrDetails.Rows.Count > 0)
            {
                if (Convert.ToString(dtTrDetails.Rows[0]["pvappstatus"]) != "Pending")
                {
                    Response.Redirect("~/procs/InboxResignation.aspx?app=" + hdnInboxType.Value);
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
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

                hflempName.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
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

    public void GetEmployeeDetails_loginemployee()
    {
        try
        {
            DataTable dtEmpDetails = new DataTable();
            dtEmpDetails = spm.GetEmployeeData(hdnempcode.Value);
            if (dtEmpDetails.Rows.Count > 0)
            {

                hdnloginemployee_name.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);

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
    public void AssigningSessions()
    {

        Session["Fromdate"] = txtFromdate_sub.Text;
        Session["ReqEmpCode"] = txtEmpCode.Text;
        Session["Grade"] = hflGrade.Value;

        Session["TrDays"] = hdnTrdays.Value;


    }
    public void getMobileClaimDetails()
    {
        DataTable dtMobileDetails = new DataTable();
        dtMobileDetails = spm.GetResignationClaimDetails_ForApprover(txtEmpCode.Text, hdnRemid.Value);

        ////dgMobileClaim.DataSource = null;
        ////dgMobileClaim.DataBind();

        if (dtMobileDetails.Rows.Count > 0)
        {

            ////dgMobileClaim.DataSource = dtMobileDetails;
            ////dgMobileClaim.DataBind();

            //txtFromdate.Text = Convert.ToString(dtMobileDetails.Rows[0]["Rem_Month"]).Trim();
            //txtAmount.Text = Convert.ToString(dtMobileDetails.Rows[0]["Amount"]).Trim();
            ////lnkuplodedfile.Text = Convert.ToString(dtMobileDetails.Rows[0]["UploadFile"]).Trim();
            hdnYearlymobileAmount.Value = Convert.ToString(dtMobileDetails.Rows[0]["Eligible_Amt"]).Trim();
            //txtReason.Text = Convert.ToString(dtMobileDetails.Rows[0]["Reason"]).Trim();

        }

        #region Calulate Total Claim Amount
        //txtAmount.Text = "0";
        //txtAmount.Enabled = false;
        //Decimal dtotclaimAmt = 0;

        //for (Int32 irow = 0; irow < dgMobileClaim.Rows.Count; irow++)
        //{
        //    if (Convert.ToString(dgMobileClaim.Rows[irow].Cells[1].Text).Trim() != "")
        //    {
        //        dtotclaimAmt += Convert.ToDecimal(dgMobileClaim.Rows[irow].Cells[1].Text);
        //        YearlymobileAmount = Convert.ToDouble(dgMobileClaim.Rows[0].Cells[2].Text);
        //    }
        //}
        //hdnYearlymobileAmount.Value = Convert.ToString(YearlymobileAmount);
        //txtAmount.Text = Convert.ToString(dtotclaimAmt);
        #endregion
    }
    private void InsertMobileRem_DatatoTempTables_trvl()
    {
        try
        {

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_MobileRem_insert_mainData_toTempTabls";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDecimal(hdnRemid.Value);

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            DataTable dtMaxTripID = spm.InsertorUpdateData(spars, "[SP_GETALLreembursement_DETAILS]");

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }


    }
    public void getApproverdata()
    {

        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.GeTResignationApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToDecimal(hdnRemid.Value));
        //IsEnabledFalse (true);
        dspaymentVoucher_Apprs.Tables.Add(dtApproverEmailIds);
        lstApprover.Items.Clear();
        if (dtApproverEmailIds.Rows.Count > 0)
        {
            hdnApprEmailaddress.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["Emp_Emailaddress"]);
            hdnApprId.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["APPR_ID"]);

            DgvApprover.DataSource = null;
            DgvApprover.DataBind();

            if (dtApproverEmailIds.Rows.Count > 0)
            {
                DgvApprover.DataSource = dtApproverEmailIds;
                DgvApprover.DataBind();
            }

            hflapprcode.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["approver_emp_code"]);
        }
        else
        {
            lblmessage.Text = "Since you are not assigned under any approver you can not apply for any type of Travel request, please contact HR";

        }
    }
    public void getMobRemlsDetails_usingRemid()
    {
        try
        {
            DataTable dtTrDetails = new DataTable();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_getMainResignationRequest_forApproval";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Int);
            spars[1].Value = hdnRemid.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            dtTrDetails = spm.getMobileRemDataList(spars, "[SP_GETALLreembursement_DETAILS]");
            if (dtTrDetails.Rows.Count > 0)
            {
                txtEmpCode.Text = Convert.ToString(dtTrDetails.Rows[0]["Emp_Code"]);
                txtEmpName.Text = Convert.ToString(dtTrDetails.Rows[0]["Emp_Name"]);
                txtFromdate_sub.Text = Convert.ToString(dtTrDetails.Rows[0]["created_on"]);
                //txtAmount.Text = Convert.ToString(dtTrDetails.Rows[0]["TotalAmount_Claimed"]);
                Txt_COSRecommended.Text = Convert.ToString(dtTrDetails.Rows[0]["cos_recommendation"]);
                //Txt_HOD_Recm_Amt.Text = Convert.ToString(dtTrDetails.Rows[0]["HOD_recm_Amt"]);
                Txt_CFO_Recm_Amt.Text = Convert.ToString(dtTrDetails.Rows[0]["CFO_recm_Amt"]);
                hdnReqEmailaddress.Value = Convert.ToString(dtTrDetails.Rows[0]["Emp_Emailaddress"]);
                hdnTravelConditionid.Value = Convert.ToString(dtTrDetails.Rows[0]["Rem_Conditionid"]);
                hflGrade.Value = Convert.ToString(dtTrDetails.Rows[0]["grade"]);
                //Txt_ProjectName.Text = Convert.ToString(dtTrDetails.Rows[0]["Project_Name"]);
                //Txt_DeptName.Text = Convert.ToString(dtTrDetails.Rows[0]["Dept_Name"]);
                txtFromdate_N.Text = Convert.ToString(dtTrDetails.Rows[0]["From_date"]);
                txtTodate_N.Text = Convert.ToString(dtTrDetails.Rows[0]["To_date"]);
                //Txt_BillNo.Text = Convert.ToString(dtTrDetails.Rows[0]["Billno"]);
                txtBilltype.Text = Convert.ToString(dtTrDetails.Rows[0]["BillType"]).Trim();
                txtReason.Text = Convert.ToString(dtTrDetails.Rows[0]["Reason"]).Trim();
                //lblheading.Text = "Mobile bill Voucher - " + Convert.ToString(dtTrDetails.Rows[0]["Vouno"]);
                hdnvouno.Value = Convert.ToString(dtTrDetails.Rows[0]["Vouno"]);
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    }
    public void getApproverdata_old()
    {

        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.GeTfuelApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), 0,"",0);
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

    protected void GetCuurentApprID()
    {

        string Actions = "";
        DataTable dtCApprID = new DataTable();
        dtCApprID = spm.GetCurrentApprIDResignation(Convert.ToInt32(hdnRemid.Value), hdnempcode.Value);
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

    private void getApproverlist()
    {
        DataTable dtapprover = new DataTable();
        dtapprover = spm.GetMobileApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
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

    public void getnextAppIntermediate()
    {
        //Check if Cureent login is Final Approver
        if (Convert.ToString(hdnTravelConditionid.Value) == "1")
        {

        }
        DataTable dsapproverNxt = new DataTable();
        dsapproverNxt = spm.GetMobilelNextApproverDetails(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
        if (dsapproverNxt.Rows.Count > 0)
        {
            hdnNextApprId.Value = Convert.ToString(dsapproverNxt.Rows[0]["APPR_ID"]);
            hdnNextApprCode.Value = Convert.ToString(dsapproverNxt.Rows[0]["A_EMP_CODE"]);
            hdnNextApprName.Value = Convert.ToString(dsapproverNxt.Rows[0]["Emp_Name"]);
            hdnNextApprEmail.Value = Convert.ToString(dsapproverNxt.Rows[0]["Emp_Emailaddress"]);

            //DataTable dtintermediateemail = new DataTable();
            //dtintermediateemail = spm.TravelNextIntermediateName(Convert.ToInt32(hdnCurrentApprID.Value), txtEmpCode.Text);
            //if (dtintermediateemail.Rows.Count > 0)
            //{
            //    hdnIntermediateEmail.Value = (string)dtintermediateemail.Rows[0]["emp_emailaddress"];
            //}
        }
        else
        {
            hdnstaus.Value = "Final Approver";

            //For  Previous approver   
            getPreviousApprovesEmailList();

            //hdnIntermediateEmail.Value = "";
            //DataTable dtPreInt = new DataTable();
            //dtPreInt = spm.FuelPreviousIntermidaterDetails(txtEmpCode.Text, Convert.ToInt32(hdnCurrentApprID.Value));
            //if (dtPreInt.Rows.Count > 0)
            //{

            //    for (int i = 0; i < dtPreInt.Rows.Count; i++)
            //    {
            //        if (Convert.ToString(hdnIntermediateEmail.Value).Trim() == "")
            //        {
            //            hdnIntermediateEmail.Value = Convert.ToString(dtPreInt.Rows[i]["Emp_Emailaddress"]).Trim();
            //        }
            //        else
            //        {
            //            hdnIntermediateEmail.Value += ";" + Convert.ToString(dtPreInt.Rows[i]["Emp_Emailaddress"]).Trim();
            //        }
            //    }
            //}
        }

    }

    private void get_HOD_ACC_CFO_details_ForNextApprover(string strstype)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[5];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            if (Convert.ToString(strstype) == "HOD" || Convert.ToString(strstype) == "RCFO" || Convert.ToString(strstype) == "RACC")
                //if (Convert.ToString(strstype) == "HOD" || Convert.ToString(strstype) == "RCFO" || Convert.ToString(strstype) == "RCOS" || Convert.ToString(strstype) == "RACC")
                spars[0].Value = "get_next_Approver_dtls_MobClaim";
            else
                spars[0].Value = "get_ACC_HOD_isApproved_claim";

            spars[1] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[1].Value = strstype;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            spars[3] = new SqlParameter("@rem_id", SqlDbType.VarChar);
            spars[3].Value = hdnRemid.Value;

            spars[4] = new SqlParameter("@conditiontypeid", SqlDbType.Int);
            spars[4].Value = hdnTravelConditionid.Value;


            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                hdnstaus.Value = "";
                hdnApprovalACCHOD_mail.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();
                hdnApprovalACCHOD_Code.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_code"]).Trim();
                hdnApprovalACCHOD_ID.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_id"]).Trim();
                hdnApprovalACCHOD_Name.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_remarks"]).Trim();

            }
            if (dsTrDetails.Tables[1].Rows.Count > 0)
            {
                if (dsTrDetails.Tables[0].Rows.Count == 0)
                {
                    hdnApprovalACCHOD_mail.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["approver_emp_mail"]).Trim();
                    hdnApprovalACCHOD_Code.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["approver_emp_code"]).Trim();
                    hdnApprovalACCHOD_ID.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["app_id"]).Trim();
                    hdnApprovalACCHOD_Name.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["app_remarks"]).Trim();
                    hdnstaus.Value = "";
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    private void checkisCOSor_ACC_ClaimApproved()
    {


        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            // spars[0].Value = "check_COS_ACC_HOD_isApproved_claim";
            spars[0].Value = "check_COS_ACC_HOD_isApproved_claim_Mobile";


            spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[1].Value = hdnRemid.Value;
            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                for (Int32 irow = 0; irow < dsTrDetails.Tables[0].Rows.Count; irow++)
                {
                    if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Action"]).Trim() == "Pending" && (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_type"]).Trim() == "RCOS" || Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_type"]).Trim() == "RCFO"))
                    {
                        hdnCurrentApprID.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Appr_id"]).Trim();
                        get_HOD_ACC_CFO_details_ForNextApprover("RACC");

                    }

                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    protected string GetApprove_RejectList()
    {
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        //dtAppRej = spm.GetMobileApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
        dtAppRej = spm.GeTfuelApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToDecimal(hdnRemid.Value),"",0);
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
    private void getPreviousApprovesEmailList()
    {
        DataTable dtPreApp = new DataTable();
        dtPreApp = spm.MobilePreviousApproverDetails(Convert.ToInt32(hdnRemid.Value));
        if (dtPreApp.Rows.Count > 0)
        {

            for (int i = 0; i < dtPreApp.Rows.Count; i++)
            {
                if (Convert.ToString(hflEmailAddress.Value).Trim() == "")
                {
                    hflEmailAddress.Value = Convert.ToString(dtPreApp.Rows[i]["Emp_Emailaddress"]).Trim();
                }
                else
                {
                    hflEmailAddress.Value += ";" + Convert.ToString(dtPreApp.Rows[i]["Emp_Emailaddress"]).Trim();
                }
            }
        }
    }
    protected string getRejectionCorrectionmailList()
    {
        string email_ids = "";
        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.MobileApproverDetails_Rejection_cancellation(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), "get_MobileApproverDetails_mail_rejection_correction");
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

    public void getMobileClaimUploadedFiles()
    {

        DataTable dtTrDetails = new DataTable();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Resignation_Claim_UploadedFilesList";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

        spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
        spars[2].Value = Convert.ToDecimal(hdnRemid.Value);

        dtTrDetails = spm.getDataList(spars, "SP_GETALLreembursement_DETAILS");

        gvuploadedFiles.DataSource = null;
        gvuploadedFiles.DataBind();
        if (dtTrDetails.Rows.Count > 0)
        {
            gvuploadedFiles.DataSource = dtTrDetails;
            gvuploadedFiles.DataBind();
        }
    }
    #endregion

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/InboxResignation_Arch.aspx?app=" + hdnInboxType.Value);
    }

    protected void lnkviewfile_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            Int32 ifileid = Convert.ToInt32(gvuploadedFiles.DataKeys[row.RowIndex].Values[0]);
            LinkButton lnkviewfile = (LinkButton)row.FindControl("lnkviewfile");

            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Resignationpath"]).Trim()), lnkviewfile.Text);
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

}
