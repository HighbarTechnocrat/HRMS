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


public partial class Payment_Req_App : System.Web.UI.Page
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
	public DataTable dtTripMode;

	String CEOInList = "N"; 
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
                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PaymentVoucherDocumentpath"]).Trim());
                    editform.Visible = true;
                    divbtn.Visible = false;
                    divmsg.Visible = false;
                    hdnempcode.Value = Convert.ToString(Session["Empcode"]);
                    txtFromdate.Enabled = false;
                    Txt_CFO_Recm_Amt.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    Txt_HOD_Recm_Amt.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
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
                            checkApprovalStatus();
                            getPaymentRemlsDetails_usingRemid();
							//   InsertMobileRem_DatatoTempTables_trvl();
							GetTravelMode();//Add list dispay for Acc Dept
							getPaymentClaimDetails();
                            getPaymentClaimUploadedFiles();


                        }
                        GetCuurentApprID();
                        //getApproverlist();
                        //getApproverdata();
                        getnextAppIntermediate();
                        //getPayementVoucher_forPrint();

                    }
                    mobile_btnPrintPV.Visible = false;
                   // chk_exception.Enabled = true;
                    if (Convert.ToString(hdnInboxType.Value).Trim() == "RACC")
                    {
						//mobile_btnSave.Text = "Approve";
						// mobile_btnSave.ToolTip = "Approve";
						dgMobileClaim.Enabled = true;
						//SExpenses.Visible = true;
						mobile_btnSave.Visible = false;
                        mobile_btnSave_COSACC.Visible = true;
                        mobile_btnSave_COSACC.Text = "Approve";
                        mobile_btnSave_COSACC.ToolTip = "Approve";
                        Txt_HOD_Recm_Amt.Visible = false;
                        Spn_HOD_Recm_Amt.Visible = false;
                        Txt_HOD_Recm_Amt.Enabled = false;
                        Txt_CFO_Recm_Amt.Visible = false;
                        Spn_CFO_Recm_Amt.Visible = false;
                        Txt_CFO_Recm_Amt.Enabled = false;
                        mobile_btnReject.Visible = false;
                        mobile_btnPrintPV.Visible = true;
                        mobile_btnCorrection.Visible = true;
                        chk_exception.Enabled = false;
                        GETALLNearByPaymentDetails();
                    }

                    if (Convert.ToString(hdnInboxType.Value).Trim() == "RCOS")
                    {
                        txtRecommendation_COS.Visible = true;
                        txtComment.Visible = false;
                        mobile_btnReject.Visible = false;
                        COS_Rem.Visible = false;
                        Txt_COSRecommended.Visible = false;
                        mobile_btnSave.Text = "Recommendation";
                        mobile_btnSave.ToolTip = "Recommendation";
                        chk_exception.Enabled = false;
                        mobile_btnCorrection.Visible = true;
                    }

                    if (Convert.ToString(hdnInboxType.Value).Trim() == "APP")
                    {
                        Txt_HOD_Recm_Amt.Visible = false;
                        Spn_HOD_Recm_Amt.Visible = false;
                        Txt_HOD_Recm_Amt.Enabled = false;
                        mobile_btnCorrection.Visible = false;
                    }

                    if (Convert.ToString(hdnInboxType.Value).Trim() == "RCFO")
                    {
                        Txt_HOD_Recm_Amt.Visible = false;
                        Spn_HOD_Recm_Amt.Visible = false;
                        Txt_HOD_Recm_Amt.Enabled = false;
                        Txt_CFO_Recm_Amt.Visible = false;
                        Spn_CFO_Recm_Amt.Visible = false;
                        Txt_CFO_Recm_Amt.Enabled = false;
                        mobile_btnCorrection.Visible = true;
                        chk_exception.Enabled = false;
                    }
                    if (hdnTravelConditionid.Value.ToString() == "1")
                    {
                        if (CEOInList == "Y")
                            chk_exception.Enabled = false;
                    }
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
                else
                {
                    if (Convert.ToString(Txt_HOD_Recm_Amt.Text).Trim() != "")
                    {
                        string[] strdate;
                        strdate = Convert.ToString(Txt_HOD_Recm_Amt.Text).Trim().Split('.');
                        if (strdate.Length > 2)
                        {
                            txtAmount.Text = "0";
                            lblmessage.Text = "Please enter correct amount.";
                            return;
                        }

                        Decimal dfare = 0;
                        dfare = Convert.ToDecimal(Txt_HOD_Recm_Amt.Text);
                        if (dfare == 0)
                        {
                            lblmessage.Text = "Please enter correct amount.";
                            return;
                        }
                    }
                    if (Convert.ToString(Txt_CFO_Recm_Amt.Text).Trim() != "")
                    {
                        string[] strdate;
                        strdate = Convert.ToString(Txt_CFO_Recm_Amt.Text).Trim().Split('.');
                        if (strdate.Length > 2)
                        {
                            txtAmount.Text = "0";
                            lblmessage.Text = "Please enter correct amount.";
                            return;
                        }

                        Decimal dfare = 0;
                        dfare = Convert.ToDecimal(Txt_CFO_Recm_Amt.Text);
                        if (dfare == 0)
                        {
                            lblmessage.Text = "Please enter correct amount.";
                            return;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    protected void btnTra_Details_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter submission date";
            return;
        }
        AssigningSessions();
        Response.Redirect("~/procs/PaymentClaim.aspx");
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        string strclaim_month = "";
        string[] strdate;
        string strfromDate = "";
        decimal recm_amount = 0;

        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }

        lblmessage.Visible = true;
        if (Convert.ToString(hdnInboxType.Value).Trim() == "RACC")
        {
			foreach (GridViewRow gvrow in dgMobileClaim.Rows)
			{
				DropDownList ddlExpenses = (DropDownList)gvrow.FindControl("ddlExpenses");
				if (ddlExpenses.SelectedValue == "0")
				{
					lblmessage.Text = "Please select the Expenses!.";
					return;
				}
			}
				if (Convert.ToString(txtComment.Text).Trim()=="")
            {
                lblmessage.Text = "Please enter Remarks.";
                return;
            }
        }
        if (Convert.ToString(hdnInboxType.Value).Trim() == "RCOS")
        {
            if (Convert.ToString(txtRecommendation_COS.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Recommendation.";
                return;
            }
            txtComment.Text = txtRecommendation_COS.Text.ToString();
        }
        if (Convert.ToString(hdnInboxType.Value).Trim() == "APP")
        {
            //if (Convert.ToString(Txt_HOD_Recm_Amt.Text).Trim() == "")
            //{
            //    lblmessage.Text = "Please enter Recommended Amount.";
            //    return;
            //}
            //if (Convert.ToString(Txt_HOD_Recm_Amt.Text).Trim() != "")
            //{
            //    strdate = Convert.ToString(Txt_HOD_Recm_Amt.Text).Trim().Split('.');
            //    if (strdate.Length > 2)
            //    {
            //        txtAmount.Text = "0";
            //        lblmessage.Text = "Please enter correct Amount.";
            //        return;
            //    }

            //    Decimal dfare = 0;
            //    dfare = Convert.ToDecimal(Txt_HOD_Recm_Amt.Text);
            //    if (dfare == 0)
            //    {
            //        lblmessage.Text = "Please enter correct Amount.";
            //        return;
            //    }
            //}
            //recm_amount = Convert.ToDecimal(Txt_HOD_Recm_Amt.Text.ToString());
        }

        if (Convert.ToString(hdnInboxType.Value).Trim() == "RCFO")
        {
            //if (Convert.ToString(Txt_CFO_Recm_Amt.Text).Trim() == "")
            //{
            //    lblmessage.Text = "Please enter Recommended Amount.";
            //    return;
            //}
            //if (Convert.ToString(Txt_CFO_Recm_Amt.Text).Trim() != "")
            //{
            //    strdate = Convert.ToString(Txt_CFO_Recm_Amt.Text).Trim().Split('.');
            //    if (strdate.Length > 2)
            //    {
            //        txtAmount.Text = "0";
            //        lblmessage.Text = "Please enter correct Amount.";
            //        return;
            //    }

            //    Decimal dfare = 0;
            //    dfare = Convert.ToDecimal(Txt_CFO_Recm_Amt.Text);
            //    if (dfare == 0)
            //    {
            //        lblmessage.Text = "Please enter correct Amount.";
            //        return;
            //    }
            //}
            //recm_amount = Convert.ToDecimal(Txt_CFO_Recm_Amt.Text.ToString());
        }
        string strapprovermails = "";

        //FeulConditionTType Value

        strapprovermails = getRejectionCorrectionmailList();

        ////spm.UpdateMobileAppRequest(Convert.ToInt32(hdnRemid.Value), "Approved", txtComment.Text, Convert.ToString((hdnstaus.Value).Trim()), Convert.ToInt32(hdnCurrentApprID.Value));

        //  getApproverlist();
        // Commented by R1 on 16-10-2018
        ////if (Convert.ToString(hdnTravelConditionid.Value).Trim() == "1")
        ////    checkisCOSor_ACC_ClaimApproved();
        // Commented by R1 on 16-10-2018
        string newlinkinboxtype = "";
        if (Convert.ToString(hdnTravelConditionid.Value).Trim() == "2" || Convert.ToString(hdnTravelConditionid.Value).Trim() == "1")
        {           
             //if (Convert.ToString(hdnTravelConditionid.Value).Trim() == "2")
            get_HOD_ACC_CFO_details_ForNextApprover("HOD");
            newlinkinboxtype = "APP";
             //if (Convert.ToString(hdnTravelConditionid.Value).Trim() == "3")
             //    get_HOD_ACC_CFO_details_ForNextApprover("RCFO");

             if (Convert.ToString(hdnApprovalACCHOD_Code.Value).Trim() == "")
             {
                 if (Convert.ToString(hdnTravelConditionid.Value).Trim() == "1" || Convert.ToString(hdnTravelConditionid.Value).Trim() == "2")
                 {
                     get_HOD_ACC_CFO_details_ForNextApprover("RACC");
                     newlinkinboxtype = "RACC";
                  
                 }
                    
             }

            /* RCFO not required on 13.11.2018 if (Convert.ToString(hdnApprovalACCHOD_Code.Value).Trim() == "")
             {
                 if (Convert.ToString(hdnTravelConditionid.Value).Trim() == "2")
                 { 
                     if (Convert.ToString(hdnInboxType.Value).Trim() == "RCFO")
                     {
                         get_HOD_ACC_CFO_details_ForNextApprover("RACC");
                         newlinkinboxtype = "RACC";
                     }
                     else
                     {
                         get_HOD_ACC_CFO_details_ForNextApprover("RCFO");
                         newlinkinboxtype = "RCFO";
                     }
                 }
             }*/
        }
        
        //string ss = GetApprove_RejectList();
        String strmobeRemURL = "";
        //strmobeRemURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_PVRem"]).Trim() + "?rem_id=" + hdnRemid.Value + "&mngexp=0&inbtype=" + hdnInboxType.Value;
        strmobeRemURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_PVRem"]).Trim() + "?rem_id=" + hdnRemid.Value + "&mngexp=0&inbtype=" + newlinkinboxtype;

        GetEmployeeDetails_loginemployee();
        if (Convert.ToString(hdnInboxType.Value).Trim() == "RACC")
        {
            hdnloginemployee_name.Value = "Account";
            hdnstaus.Value = "Final Approver";
        }
        if (Convert.ToString(hdnInboxType.Value).Trim() == "RCOS")
        {
            hdnloginemployee_name.Value = "COS";
        }

        if (Convert.ToString(hdnInboxType.Value).Trim() == "RCFO")
        {
            hdnloginemployee_name.Value = "CFO";
        }


        if (dgMobileClaim.Rows.Count > 0)
        {
            DateTime tdate;
            strfromDate = Convert.ToString(dgMobileClaim.Rows[0].Cells[0].Text).Trim();

            if (Convert.ToString(strfromDate).Trim() != "")
            {
                strdate = Convert.ToString(strfromDate).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                tdate = Convert.ToDateTime(strfromDate);
                strclaim_month = tdate.ToString("MMM-yy");
                //strclaim_month = Convert.ToString(strdate[0]) + "-" + Convert.ToString(strdate[1]);
            }
        }

        

        if (Convert.ToString((hdnstaus.Value).Trim()) == "")
        {
           // spm.InsertMobileApproverDetails(hdnNextApprCode.Value, Convert.ToInt32(hdnNextApprId.Value), Convert.ToInt32(hdnRemid.Value), "");
            spm.UpdatePaymentAppRequest(Convert.ToInt32(hdnRemid.Value), "Approved", Convert.ToString(txtComment.Text).Trim(), Convert.ToString((hdnstaus.Value).Trim()), Convert.ToInt32(hdnCurrentApprID.Value), recm_amount, Convert.ToInt32(hdnTravelConditionid.Value));
            spm.InsertPaymentVoucherApproverDetails(hdnApprovalACCHOD_Code.Value, Convert.ToInt32(hdnApprovalACCHOD_ID.Value), Convert.ToInt32(hdnRemid.Value), "");
            spm.Payment_send_mailto_Next_Approver(hdnReqEmailaddress.Value, hdnApprovalACCHOD_mail.Value, "Request for Payment Voucher bill Reimbursement ", "", txtAmount.Text, GetApprove_RejectList(), txtEmpName.Text, strmobeRemURL, strclaim_month);

            spm.Payment_send_mailto_Next_Approver_Intermediate(hdnReqEmailaddress.Value, strapprovermails, "Request for Payment Voucher Reimbursement ", "", txtAmount.Text, GetApprove_RejectList(), txtEmpName.Text, hdnloginemployee_name.Value, strclaim_month);
            //   spm.Travel_send_mailto_Intermediate(hdnReqEmailaddress.Value, hdnIntermediateEmail.Value, "Request for Travel", txtTriptype.Text, hdnTrdays.Value, txtReason.Text, txtFromdate.Text, txtToDate.Text, GetApprove_RejectList(), txtEmpName.Text);
            lblmessage.Text = "Mobile Reimbursement Resquest has been approved and and send for next level approvals";
            //  Response.Redirect("~/procs/InboxTravelRequest.aspx");
        }
        else
        {
			if (Convert.ToString(hdnInboxType.Value).Trim() == "RACC")
			{
				ExpensesChangeByAccount(); //Account Dept Changes Only  Expenses
			}
			spm.UpdatePaymentAppRequest(Convert.ToInt32(hdnRemid.Value), "Approved", Convert.ToString(txtComment.Text).Trim(), Convert.ToString((hdnstaus.Value).Trim()), Convert.ToInt32(hdnCurrentApprID.Value), 0, Convert.ToInt32(hdnTravelConditionid.Value));
            spm.Payment_send_mailto_Next_Approver_Intermediate(hdnReqEmailaddress.Value, strapprovermails, "Request for Payment Voucher Reimbursement ", "", txtAmount.Text, GetApprove_RejectList(), txtEmpName.Text, hdnloginemployee_name.Value, strclaim_month);
            lblmessage.Text = "Fuel Reimbursement Resquest has been approved and notofication has been send to the Requester and Previous Intermediate,Approver Levels";

        }
        Response.Redirect("~/procs/InboxPayments.aspx?app=" + hdnInboxType.Value);

    }

	private void ExpensesChangeByAccount()
	{
		try
		{
			DataTable dsExpressList = new DataTable();
			foreach (GridViewRow gvrow in dgMobileClaim.Rows)
			{
				DropDownList ddlExpenses = (DropDownList)gvrow.FindControl("ddlExpenses");
				string Claims_id = Convert.ToString(dgMobileClaim.DataKeys[gvrow.RowIndex].Values[0]).Trim();
				HiddenField HFpv_id = (HiddenField)gvrow.FindControl("HFClaims_id");
				SqlParameter[] spars = new SqlParameter[6];
				spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
				spars[0].Value = "UpdateByAccount";
				spars[1] = new SqlParameter("@Rem_id", SqlDbType.Int);
				spars[1].Value = Convert.ToInt32(hdnRemid.Value);
				spars[2] = new SqlParameter("@Exp_Type", SqlDbType.Int);
				spars[2].Value = Convert.ToInt32(ddlExpenses.SelectedValue);
				spars[3] = new SqlParameter("@Claims_id_o", SqlDbType.Int);
				spars[3].Value = Convert.ToInt32(Claims_id);
				spars[4] = new SqlParameter("@empcode", SqlDbType.VarChar);
				spars[4].Value = Convert.ToString(Session["Empcode"]); 
				dsExpressList = spm.getDropdownList(spars, "SP_Insert_Payment_VoucherDetails");
			}
		}
		catch (Exception)
		{

			throw;
		}
	}

	protected void mobile_btnReject_Click(object sender, EventArgs e)
    {

        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }


        string strclaim_month = "";
        string[] strdate;
        string strfromDate = "";

         if(Convert.ToString(txtComment.Text).Trim()=="")
         {
             lblmessage.Text = "Please enter Rejection Remarks.";
             return;
         }
         if (dgMobileClaim.Rows.Count > 0)
         {
             DateTime tdate;
             strfromDate = Convert.ToString(dgMobileClaim.Rows[0].Cells[0].Text).Trim();

             if (Convert.ToString(strfromDate).Trim() != "")
             {
                 strdate = Convert.ToString(strfromDate).Trim().Split('/');
                 strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                 tdate = Convert.ToDateTime(strfromDate);
                 strclaim_month = tdate.ToString("MMM-yy");
                 //strclaim_month = Convert.ToString(strdate[0]) + "-" + Convert.ToString(strdate[1]);
             }
         }

        string strapprovermails = "";
        strapprovermails = getRejectionCorrectionmailList();
        GetEmployeeDetails_loginemployee();
        if (Convert.ToString(hdnInboxType.Value).Trim() == "RACC")
        {
            hdnloginemployee_name.Value= "Account";
        }
        if (Convert.ToString(hdnInboxType.Value).Trim() == "RCOS")
        {
            hdnloginemployee_name.Value= "COS";
        }
        if (Convert.ToString(hdnInboxType.Value).Trim() == "RCFO")
        {
            hdnloginemployee_name.Value = "CFO";
        }

        spm.RrejectPaymentrequest(Convert.ToInt32(hdnRemid.Value), hdnempcode.Value, Convert.ToInt32(hdnCurrentApprID.Value), Convert.ToString(txtComment.Text).Trim(), "rejectFuelReimbursementrequest");
        spm.Payment_send_mail_Rejection_Correction(hdnReqEmailaddress.Value, hdnloginemployee_name.Value, "Rejection of Payment Voucher Reimbursement Request", Convert.ToString(txtComment.Text).Trim(), txtAmount.Text, strapprovermails, Convert.ToString(GetApprove_RejectList()).Trim(), "", txtEmpName.Text, strclaim_month);
        Response.Redirect("~/procs/InboxPayments.aspx?app=" + hdnInboxType.Value);
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
            spars[0].Value = "payment_voucher";

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


                #region Cost Cente & Bank Details
                if (dspaymentVoucher.Tables[3].Rows.Count > 0)
                {
                    param[5] = new ReportParameter("pCostCenterCode", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["Cost_Center"]));
                    param[6] = new ReportParameter("pCostCenterNM", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["Cost_Center_desc"]));

                    param[7] = new ReportParameter("pBankName", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["BANK_name"]));
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

                    param[7] = new ReportParameter("pBankName", Convert.ToString(""));
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
                param[16] = new ReportParameter("PAlt_Contact", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["alternate_contact_no"]));
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

        Session["Fromdate"] = txtFromdate.Text;
        Session["ReqEmpCode"] = txtEmpCode.Text;
        Session["Grade"] = hflGrade.Value;
  
        Session["TrDays"] = hdnTrdays.Value;


    }
    public void getPaymentClaimDetails()
    {
        DataTable dtMobileDetails = new DataTable();
        dtMobileDetails = spm.GetPaymentVoucherClaimDetails_ForApprover(txtEmpCode.Text, hdnRemid.Value);
        dgMobileClaim.DataSource = null;
        dgMobileClaim.DataBind();
        if (dtMobileDetails.Rows.Count > 0)
        {

            dgMobileClaim.DataSource = dtMobileDetails;
            dgMobileClaim.DataBind();
        }

        #region Calulate Total Claim Amount
        txtAmount.Text = "0";
        txtAmount.Enabled = false;
        Decimal dtotclaimAmt = 0;
        for (Int32 irow = 0; irow < dgMobileClaim.Rows.Count; irow++)
        {
            if (Convert.ToString(dgMobileClaim.Rows[irow].Cells[3].Text).Trim() != "")
                dtotclaimAmt += Convert.ToDecimal(dgMobileClaim.Rows[irow].Cells[3].Text);
        }
        txtAmount.Text = Convert.ToString(dtotclaimAmt);
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

    private void checkApprovalStatus()
    {
        try
        {

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "check_PVReqAppr_Status";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDecimal(hdnRemid.Value);

            spars[2] = new SqlParameter("@apprempcode", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(hdnempcode.Value);
            
                DataTable  dtTrDetails = spm.getMobileRemDataList(spars, "[SP_GETALLreembursement_DETAILS]");
			if (dtTrDetails.Rows.Count == 0)
			{
				mobile_btnCorrection.Visible = false;
				mobile_btnSave.Visible = false;
				mobile_btnReject.Visible = false;
				mobile_btnPrintPV.Visible = false;
			}
			if (dtTrDetails.Rows.Count > 0)
        {
            if (Convert.ToString(dtTrDetails.Rows[0]["pvappstatus"]) != "Pending")
            {
                Response.Redirect("~/procs/InboxPayments.aspx?app=" + hdnInboxType.Value);
            }
        }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }


    }

    public void getApproverdata(string comp_code)
    {
        var deptId = 0;
        if(comp_code=="")
        {
            comp_code = Comp_Code.Value;
        }
        if (Convert.ToString(Txt_ProjectName.Text).Contains("Head Office"))
        {
            deptId = Convert.ToInt32(hdnDept_Id.Value);
        }
        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.GeTPaymentVoucherApproverEmailID_Comp(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToDecimal(hdnRemid.Value), comp_code, deptId);
        //IsEnabledFalse (true);
        //dspaymentVoucher_Apprs.Tables[0] = (Datatable) dtApproverEmailIds;
        dspaymentVoucher_Apprs.Tables.Add(dtApproverEmailIds);
        //dspaymentVoucher_Apprs= spm.GeTPaymentVoucherApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToDecimal(hdnRemid.Value));

        lstApprover.Items.Clear();

        if (dtApproverEmailIds.Rows.Count > 1)
        {
            foreach (DataRow item in dtApproverEmailIds.Rows)
            {
                var getEmpCode = Convert.ToString(item["approver_emp_code"]);
                var getApprId = Convert.ToInt32(item["APPR_ID"]);
                if (getEmpCode == Convert.ToString(Session["Empcode"]).Trim() && (getApprId == 11 || getApprId == 13) && hdnTravelConditionid.Value == "2")
                {
                    chk_exception.Enabled = false;
                }
            }
        }


        if (dtApproverEmailIds.Rows.Count > 0)
        {
            foreach (DataRow row in dtApproverEmailIds.Rows)
            {
                if (row[1].ToString() == "00002726")
                {
                    CEOInList = "Y"; 
                }
            }
            hdnApprEmailaddress.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["Emp_Emailaddress"]);
            hdnApprId.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["APPR_ID"]);

            //lstApprover.DataSource = dtApproverEmailIds;
            //lstApprover.DataTextField = "names";
            //lstApprover.DataValueField = "APPR_ID";
            //lstApprover.DataBind();

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
    public void getPaymentRemlsDetails_usingRemid()
    {
        try
        {
            DataTable dtTrDetails = new DataTable();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_getMainPaymentVoucherRequest_forApproval";

             spars[1] = new SqlParameter("@rem_id", SqlDbType.Int);
            spars[1].Value = hdnRemid.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            dtTrDetails = spm.getMobileRemDataList(spars, "[SP_GETALLreembursement_DETAILS]");
            if (dtTrDetails.Rows.Count > 0)
            {
                txtEmpCode.Text = Convert.ToString(dtTrDetails.Rows[0]["Emp_Code"]);
                txtEmpName.Text = Convert.ToString(dtTrDetails.Rows[0]["Emp_Name"]);
                txtFromdate.Text = Convert.ToString(dtTrDetails.Rows[0]["created_on"]);
                txtAmount.Text = Convert.ToString(dtTrDetails.Rows[0]["TotalAmount_Claimed"]);
                Txt_COSRecommended.Text = Convert.ToString(dtTrDetails.Rows[0]["cos_recommendation"]);
                Txt_HOD_Recm_Amt.Text = Convert.ToString(dtTrDetails.Rows[0]["HOD_recm_Amt"]);
                Txt_CFO_Recm_Amt.Text = Convert.ToString(dtTrDetails.Rows[0]["CFO_recm_Amt"]);
                hdnReqEmailaddress.Value = Convert.ToString(dtTrDetails.Rows[0]["Emp_Emailaddress"]);
                hdnTravelConditionid.Value = Convert.ToString(dtTrDetails.Rows[0]["Rem_Conditionid"]);
                Txt_Contact.Text = Convert.ToString(dtTrDetails.Rows[0]["mobile"]);
                Txt_Alt_Contact.Text = Convert.ToString(dtTrDetails.Rows[0]["alternate_contact_no"]);
                Txt_ProjectName.Text = Convert.ToString(dtTrDetails.Rows[0]["Project_Name"]);
                Txt_DeptName.Text = Convert.ToString(dtTrDetails.Rows[0]["Dept_Name"]);
                hflGrade.Value = Convert.ToString(dtTrDetails.Rows[0]["grade"]);
                hdnDept_Id.Value = Convert.ToString(dtTrDetails.Rows[0]["DeptId"]);

                lblheading.Text = "Payment Voucher - " + Convert.ToString(dtTrDetails.Rows[0]["Vouno"]);
                if (hdnTravelConditionid.Value == "2")
                    chk_exception.Checked = true;
                else
                    chk_exception.Checked = false;
                Comp_Code.Value = Convert.ToString(dtTrDetails.Rows[0]["Comp_Code"]);
                getApproverdata(Convert.ToString(dtTrDetails.Rows[0]["Comp_Code"]));
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
        dtApproverEmailIds = spm.GeTfuelApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value),0,"",0);
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
        dtCApprID = spm.GetCurrentApprIDPayment(Convert.ToInt32(hdnRemid.Value), hdnempcode.Value);
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
        if(Convert.ToString(hdnTravelConditionid.Value)=="1")
        {
            
        }
        DataTable dsapproverNxt = new DataTable();
        dsapproverNxt = spm.GetPaymentlNextApproverDetails(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
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
            //if (Convert.ToString(strstype) == "HOD")
                spars[0].Value = "get_next_Approver_dtls_PayClaim_COMP";
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
            spars[0].Value = "check_COS_ACC_HOD_isApproved_claim_Payment";
            

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[1].Value = hdnRemid.Value;
            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");
           
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                for (Int32 irow = 0; irow < dsTrDetails.Tables[0].Rows.Count; irow++)
                {
                    if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Action"]).Trim() == "Pending" && Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_type"]).Trim() == "RCOS")
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
        var deptId = 0;
        if (Convert.ToString(Txt_ProjectName.Text).Contains("Head Office"))
        {
            deptId = Convert.ToInt32(hdnDept_Id.Value);
        }
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        //dtAppRej = spm.GetMobileApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
        dtAppRej = spm.GeTPaymentVoucherApproverEmailID_Comp(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToDecimal(hdnRemid.Value), Convert.ToString(Comp_Code.Value), deptId);
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
        dtApproverEmailIds = spm.PaymentApproverDetails_Rejection_cancellation(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), "get_PaymentApproverDetails_mail_rejection_correction");
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

    protected void mobile_btnCorrection_Click(object sender, EventArgs e)
    {

        /*  string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
          if (confirmValue != "Yes")
          {
              return;
          }

          if (Convert.ToString(txtComment.Text).Trim() == "")
          {
              lblmessage.Text = "Please enter Send For Correction Remarks";
              return;
          }

          //getFromdateTodate_FroEmail();
          //string strcliamMonth = Convert.ToString(hdnfrmdate_emial.Value).Trim();
          // GetEmployeeDetails_loginemployee();
          hdnloginemployee_name.Value = Convert.ToString(hflempName.Value); // txtEmpName.Text;
          if (Convert.ToString(hdnInboxType.Value).Trim() == "RACC")
          {
              hdnloginemployee_name.Value = "Account";
          }
          if (Convert.ToString(hdnInboxType.Value).Trim() == "RCOS")
          {
              hdnloginemployee_name.Value = "COS";
          }
          if (Convert.ToString(hdnInboxType.Value).Trim() == "RCFO")
          {
              hdnloginemployee_name.Value = "CFO";
          }


          string strapprovermails = "";
          strapprovermails = getRejectionCorrectionmailList();
          spm.RrejectFuelrequest(Convert.ToInt32(hdnRemid.Value), hdnempcode.Value, Convert.ToInt32(hdnCurrentApprID.Value), txtComment.Text, "CorrectionFuelReimbursementrequest");
          spm.Fuel_send_mail_Rejection_Correction(hdnReqEmailaddress.Value, hdnloginemployee_name.Value, "Send For Mobile Correction", txtQuantity.Text, txtAmount.Text, strapprovermails, Convert.ToString(GetApprove_RejectList()).Trim(), txtComment.Text, txtEmpName.Text, strcliamMonth);
          Response.Redirect("~/procs/InboxFuel.aspx?inbtype=" + hdnInboxType.Value);
          //--
          */


        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }


        string strclaim_month = "";
        string[] strdate;
        string strfromDate = "";

        if (Convert.ToString(hdnInboxType.Value).Trim() == "RACC")
        {
            if (Convert.ToString(txtComment.Text).Trim() == "" || Convert.ToString(txtComment.Text).Trim() == " ")
            {

                lblmessage.Text = "Please Enter Correction Remark..";
                return;
            }
        }
        if (Convert.ToString(hdnInboxType.Value).Trim() == "RCOS" || Convert.ToString(hdnInboxType.Value).Trim() == "RCFO")
        {
            if (Convert.ToString(txtRecommendation_COS.Text).Trim() == "" || Convert.ToString(txtRecommendation_COS.Text).Trim() == " ")
            {
                lblmessage.Text = "Please Enter Remarks!";
                return;
            }
        }

        if (txtRecommendation_COS.Text != "")
        {
            txtComment.Text = txtRecommendation_COS.Text.ToString();
        }
        if (dgMobileClaim.Rows.Count > 0)
        {
            strfromDate = Convert.ToString(dgMobileClaim.Rows[0].Cells[0].Text).Trim();

            if (Convert.ToString(strfromDate).Trim() != "")
            {
                strdate = Convert.ToString(strfromDate).Trim().Split('/');
                strclaim_month = Convert.ToString(strdate[0]) + "-" + Convert.ToString(strdate[1]);
            }
        }

        string strapprovermails = "";
        strapprovermails = getRejectionCorrectionmailList();
        GetEmployeeDetails_loginemployee();
        if (Convert.ToString(hdnInboxType.Value).Trim() == "RACC")
        {
            hdnloginemployee_name.Value = "Account";
        }
        if (Convert.ToString(hdnInboxType.Value).Trim() == "RCOS")
        {
            hdnloginemployee_name.Value = "COS";
        }
        if (Convert.ToString(hdnInboxType.Value).Trim() == "RCFO")
        {
            //hdnloginemployee_name.Value = "CFO";
            hdnloginemployee_name.Value = "COS";
        }

        spm.RrejectPaymentrequest(Convert.ToInt32(hdnRemid.Value), hdnempcode.Value, Convert.ToInt32(hdnCurrentApprID.Value), Convert.ToString(txtComment.Text).Trim(), "CorrectionPaymentvoucherrequest");
        //spm.RrejectMobilerequest(Convert.ToInt32(hdnRemid.Value), hdnempcode.Value, Convert.ToInt32(hdnCurrentApprID.Value), Convert.ToString(txtComment.Text).Trim(), "CorrectionMobileReimbursementrequest");
        spm.Payment_send_mail_Rejection_Correction(hdnReqEmailaddress.Value, hdnloginemployee_name.Value, "Correction: Payment Voucher Reimbursement Request - " + strclaim_month, Convert.ToString(txtComment.Text).Trim(), txtAmount.Text, strapprovermails, Convert.ToString(GetApprove_RejectList()).Trim(), txtComment.Text.Trim(), txtEmpName.Text, strclaim_month);
        //spm.Fuel_send_mail_Rejection_Correction(hdnReqEmailaddress.Value, hdnloginemployee_name.Value, "Correction: Mobile bill Claim - " + strclaim_month, Convert.ToString(txtComment.Text).Trim(), txtAmount.Text, strapprovermails, Convert.ToString(GetApprove_RejectList()).Trim(), txtComment.Text.Trim(), txtEmpName.Text, strclaim_month);
        Response.Redirect("~/procs/InboxPayments.aspx?app=" + hdnInboxType.Value);

    }

    public void getPaymentClaimUploadedFiles()
    {

        DataTable dtTrDetails = new DataTable();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_PaymentClaim_UploadedFilesList";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

        spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
        spars[2].Value =Convert.ToDecimal(hdnRemid.Value);

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
        Response.Redirect("~/procs/InboxPayments.aspx?app=" + hdnInboxType.Value);
    }

    protected void lnkviewfile_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            Decimal ifileid = Convert.ToDecimal(gvuploadedFiles.DataKeys[row.RowIndex].Values[0]);
            LinkButton lnkviewfile = (LinkButton)row.FindControl("lnkviewfile");

            //String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ClaimsDocumentpath"]).Trim()), lnkviewfile.Text);
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PaymentVoucherDocumentpath"]).Trim()), lnkviewfile.Text);
            ////String strfilepath = "http://192.168.21.193/hrms/payemntVoucher/";
            ////String newstrfilename = lnkviewfile.Text;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
            Response.WriteFile(strfilepath);
            Response.End();
            ////string fileName = newstrfilename;
            ////string user = Environment.GetFolderPath(Environment.SpecialFolder.);
            ////string download = Path.Combine(user, "Downloads");
            ////string descFilePathAndName = Path.Combine(download, fileName);
            ////WebClient webClient = new WebClient();
            ////webClient.DownloadFile(strfilepath + newstrfilename, descFilePathAndName);

            ////try
            ////{
            ////    WebRequest myre = WebRequest.Create(strfilepath + newstrfilename);
            ////}
            ////catch
            ////{
            ////    return;
            ////}
            ////try
            ////{
            ////    byte[] fileData;
            ////    using (WebClient client = new WebClient())
            ////    {
            ////        fileData = client.DownloadData(strfilepath + newstrfilename);
            ////    }
            ////    using (FileStream fs =
            ////          new FileStream(descFilePathAndName, FileMode.OpenOrCreate))
            ////    {
            ////        fs.Write(fileData, 0, fileData.Length);
            ////    }
            ////    return;
            ////}
            ////catch (Exception ex)
            ////{
            ////    throw new Exception("download field", ex.InnerException);
            ////}
            ////return;
            //////string sContentType = "";
            //////string fileExt = strfilepath.Substring(strfilepath.LastIndexOf(".")).ToLower();
            //////switch (fileExt)
            //////{
            //////    case ".doc": case ".dot":
            //////        sContentType = "application/msword";
            //////        break;
            //////    case ".xls": case ".xlt": case ".xla":
            //////        sContentType = "application/vnd.ms-excel";
            //////        break;
            //////    case ".xlam":
            //////        sContentType = "application/vnd.ms-excel.addin.macroEnabled.12";
            //////        break;
            //////    case ".xlsb":
            //////        sContentType = "application/vnd.ms-excel.sheet.binary.macroEnabled.12";
            //////        break;
            //////    case ".xlsm":
            //////        sContentType = "application/vnd.ms-excel.sheet.macroEnabled.12";
            //////        break;
            //////    case ".xltm":
            //////        sContentType = "application/vnd.ms-excel.template.macroEnabled.12";
            //////        break;
            //////    case ".ppt": case ".pot": case ".pps": case ".ppa":
            //////        sContentType = "application/vnd.ms-powerpoint";
            //////        break;
            //////    case ".ppam":
            //////        sContentType = "application/vnd.ms-powerpoint.addin.macroEnabled.12";
            //////        break;
            //////    case ".pptm":
            //////        sContentType = "application/vnd.ms-powerpoint.presentation.macroEnabled.12";
            //////        break;
            //////    case ".ppsm":
            //////        sContentType = "application/vnd.ms-powerpoint.slideshow.macroEnabled.12";
            //////        break;
            //////    case ".potm":
            //////        sContentType = "application/vnd.ms-powerpoint.template.macroEnabled.12";
            //////        break;
            //////    case ".docm":
            //////        sContentType = "application/vnd.ms-word.document.macroEnabled.12";
            //////        break;
            //////    case ".dotm":
            //////        sContentType = "application/vnd.ms-word.template.macroEnabled.12";
            //////        break;
            //////    case ".pptx":
            //////        sContentType = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
            //////        break;
            //////    case ".ppsx":
            //////        sContentType = "application/vnd.openxmlformats-officedocument.presentationml.slideshow";
            //////        break;
            //////    case ".potx":
            //////        sContentType = "application/vnd.openxmlformats-officedocument.presentationml.template";
            //////        break;
            //////    case ".xlsx":
            //////        sContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //////        break;
            //////    case ".xltx":
            //////        sContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.template";
            //////        break;
            //////    case ".docx":
            //////        sContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            //////        break;
            //////    case ".dotx":
            //////        sContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.template";
            //////        break;                    
            //////    default:
            //////        sContentType = "Application/octet-stream";
            //////        break;
            //////}
            //////Response.Clear();
            //////Response.ContentType = sContentType;
            //////Response.WriteFile(@strfilepath);
            //////Response.End();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void mobile_btnPrintPV_Click(object sender, EventArgs e)
    {
        getApproverdata("");
        getPayementVoucher_forPrint();
    }
    protected void chk_exception_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_exception.Checked)
            hdnTravelConditionid.Value ="2";
        else
            hdnTravelConditionid.Value = "1";
        getApproverdata("");
    }
	public void GetTravelMode()
	{
		//DataTable dtTripMode = new DataTable();		
		dtTripMode = spm.getPaymentVoucher_List(txtEmpCode.Text.ToString());
		if (dtTripMode.Rows.Count > 0)
		{
			//lstTravelMode.DataSource = dtTripMode;
			//lstTravelMode.DataTextField = "pv";
			//lstTravelMode.DataValueField = "pv_id";
			//lstTravelMode.DataBind();
		}
	}
	protected void dgMobileClaim_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		try
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				var ddlExpenses = (DropDownList)e.Row.FindControl("ddlExpenses");
				HiddenField hfExpensesId = dgMobileClaim.FindControl("Claims_id") as HiddenField;				
				ddlExpenses.DataSource = dtTripMode;
				ddlExpenses.DataTextField = "pv";
				ddlExpenses.DataValueField = "pv_id";
				ddlExpenses.DataBind();
				
			}
			
		}
		catch (Exception)
		{

			throw;
		}
	}

	protected void dgMobileClaim_DataBound(object sender, EventArgs e)
	{
		try
		{
		foreach (GridViewRow gvRow in dgMobileClaim.Rows)
		{
			DropDownList ddlExpenses = gvRow.FindControl("ddlExpenses") as DropDownList;
			HiddenField hfExpensesId = gvRow.FindControl("HFClaims_id") as HiddenField;
			if (ddlExpenses != null && hfExpensesId != null)
			{
				ddlExpenses.SelectedValue = hfExpensesId.Value;
			}
		}
		}
		catch (Exception)
		{

			throw;
		}
	}

    protected void gvNearByPaymentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvNearByPaymentDetails_DataBound(object sender, EventArgs e)
    {

    }

    public void GETALLNearByPaymentDetails()
    {
        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.GETALLNearByPaymentDetails(txtEmpCode.Text, Convert.ToDouble(hdnRemid.Value));


        gvNearByPaymentDetails.DataSource = null;
        gvNearByPaymentDetails.DataBind();

        if (dtApproverEmailIds.Rows.Count > 0)
        {
            IsShowGridList.Visible = true;
            gvNearByPaymentDetails.DataSource = dtApproverEmailIds;
            gvNearByPaymentDetails.DataBind();
        }
    }
}
