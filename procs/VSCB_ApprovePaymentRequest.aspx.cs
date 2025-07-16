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
using System.Collections.Generic;
public partial class procs_VSCB_ApprovePaymentRequest : System.Web.UI.Page
{
	#region CreativeMethods
	SqlConnection source;
	public SqlDataAdapter sqladp;

	public string loc = "", approveremailaddress = "", Approvers_code = "";
	public int did = 0, apprid;
	public DataTable dtEmp, DTPoWoNumber, DTInsertPayment, dtApproverEmailIds, dtPaymentStatus, dtextraApp;
	public string filename = "";
	public string nxtapprcode = "", nxtapprname = "";
	SP_Methods spm = new SP_Methods();

	protected void lnkhome_Click(object sender, EventArgs e)
	{
		Response.Redirect(ReturnUrl("sitepathmain") + "default");
	}

	public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
	#endregion
	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
			}
			hdnEmpCode.Value = Session["Empcode"].ToString();

			if (Page.User.Identity.IsAuthenticated == false)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/vscb_index");
			}
			else
			{
				Page.SmartNavigation = true;
				if (!Page.IsPostBack)
				{
					FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VendorPaymentDocpath"]).Trim());
                    FilePathInvoice.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBInvoicefiles"]).Trim());
                    FilePathPOWOSignCopy.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBSignPOWOfiles"]).Trim());


                    txtAccountPaidAmt.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					hdnLoginUserName.Value = Convert.ToString(Session["emp_loginName"]).Trim();
					hdnLoginEmpEmail.Value = Convert.ToString(Session["LoginEmpmail"]).Trim();
					if (Request.QueryString.Count > 0)
					{
						hdnPayment_ID.Value = Convert.ToString(Request.QueryString[0]).Trim();
						hdnPOID.Value = Convert.ToString(Request.QueryString[1]).Trim();
						CheckPayment_ApprovalStatus_Submit();
						GetPayment_CurrentApprID();
						CheckAccountApprovalEmp();
						PaymentDetails();
						btnRecBack.Visible = true;
					}
                    hdnApprovedPO_FileName.Value = Convert.ToString(Regex.Replace(Convert.ToString(txtPOWONumber.Text), @"[^0-9a-zA-Z\._]", "_")).Trim() + ".pdf";
                    hdnApprovedPO_FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBApprovedPOWOfiles"]).Trim());

                    if (Convert.ToString(txtIsSecurity_DepositInvoice.Text).Trim() == "true")
                    {
                        Check_Advance_Payment_POWO();
                        spInvoice.Visible = false;
                        txtAccountAmtBal.Text = "";
                        txtAccountPaidAmt.Text = "";
                    }
                }
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}

    public void Check_Advance_Payment_POWO()
    {
        DataSet dtAdvBal = new DataSet();
        SqlParameter[] spars12 = new SqlParameter[3];
        spars12[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);      
        spars12[0].Value = "get_Advance_Security_Amount_Status";

        spars12[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        spars12[1].Value = DBNull.Value;

        spars12[2] = new SqlParameter("@POID", SqlDbType.VarChar);
        if (Convert.ToString(hdnPOID.Value).Trim() != "")
            spars12[2].Value = Convert.ToDouble(hdnPOID.Value);
        else
            spars12[2].Value = DBNull.Value;

        dtAdvBal = spm.getDatasetList(spars12, "sp_VSCB_CreatePOWO_Users");

        txt_Pay_Adv_Amt.Text = "";
        txt_Pay_Adv_Settlement.Text = "";
        txt_Pay_Adv_Bal_Amt.Text = "";  
        if (dtAdvBal.Tables[0].Rows.Count > 0)
        {
            Adv1.Visible = false;
            Adv2.Visible = false;
            Adv3.Visible = false;

            if (!String.IsNullOrEmpty(dtAdvBal.Tables[0].Rows[0]["AdvPayment_Amt"].ToString()))
            {

                txt_Pay_Adv_Amt.Text = Convert.ToString(dtAdvBal.Tables[0].Rows[0]["AdvPayment_Amt"]).Trim();
                txt_Pay_Adv_Settlement.Text = Convert.ToString(dtAdvBal.Tables[0].Rows[0]["AdvPayment_Settlement_Amt"]).Trim();
                txt_Pay_Adv_Bal_Amt.Text = Convert.ToString(dtAdvBal.Tables[0].Rows[0]["AdvPayment_Bal_Amt"]).Trim();
                Adv1.Visible = true;
                Adv2.Visible = true;
                Adv3.Visible = true;

            }
            else
            {
                Adv1.Visible = false;
                Adv2.Visible = false;
                Adv3.Visible = false;
            }
        } 
    }
    protected void lnkfile_Invoice_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBInvoicefiles"]).Trim()), lnkfile_Invoice.Text);
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

    private void PaymentDetails()
	{
		try
		{
			DataSet DTPoWoNumber = new DataSet();
			int Payment_ID = 0, POID = 0;
			Payment_ID = Convert.ToString(hdnPayment_ID.Value).Trim() != "" ? Convert.ToInt32(hdnPayment_ID.Value) : 0;
			POID = Convert.ToString(hdnPOID.Value).Trim() != "" ? Convert.ToInt32(hdnPOID.Value) : 0;
			DTPoWoNumber = spm.GetVSCB_MyPaymentDetails("POWOMyPaymentRequestDeatils", Convert.ToString(hdnEmpCode.Value), POID, Payment_ID);
			if (DTPoWoNumber.Tables[0].Rows.Count > 0)
			{
				txtPoWoDate.Text = DTPoWoNumber.Tables[0].Rows[0]["PODate"].ToString();				
				txtPOWONumber.Text = DTPoWoNumber.Tables[0].Rows[0]["PONumber"].ToString();
				txtPoWoTitle.Text = DTPoWoNumber.Tables[0].Rows[0]["POTitle"].ToString();
				txtPoWoTitle.ToolTip = DTPoWoNumber.Tables[0].Rows[0]["POTitle"].ToString();
				txtPoWoType.Text = DTPoWoNumber.Tables[0].Rows[0]["POType"].ToString();
				txtVendorName.Text = DTPoWoNumber.Tables[0].Rows[0]["Name"].ToString();
				txtGSTINNO.Text = DTPoWoNumber.Tables[0].Rows[0]["GSTIN_NO"].ToString();
				txtPoWoStatus.Text = DTPoWoNumber.Tables[0].Rows[0]["PyamentStatus"].ToString();
				txtPoWoAmtWithTaxes.Text = DTPoWoNumber.Tables[0].Rows[0]["POWOAmt"].ToString();
				txtPoWOPaidAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["PO_Paid_Amt"].ToString();
                txtPoPaidAmt_WithOutDT.Text= DTPoWoNumber.Tables[0].Rows[0]["POPiadAmount_withoutDT"].ToString();
                
                txtPoWOAmtWIthoutTax.Text = DTPoWoNumber.Tables[0].Rows[0]["POWO_T_BaseAmt"].ToString();
				txtPoWOPaidBalAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["PO_Bal_Amt"].ToString();
				txtPODirectTaxAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["DirectTaxCollection_Amt"].ToString();
				txtProjectName.Text = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
				txtDepartment.Text = DTPoWoNumber.Tables[0].Rows[0]["Department"].ToString();
				hdnCostCentre.Value = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
                txtTallyCode_display.Text= DTPoWoNumber.Tables[0].Rows[0]["Tallycode"].ToString();
                //hdnTallyCode.Value = DTPoWoNumber.Tables[0].Rows[0]["Project_Name"].ToString();
                hdnDept_Name.Value = DTPoWoNumber.Tables[0].Rows[0]["Department"].ToString();

                 

                txtCurrency.Text = Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["CurName"]);
                txtPOWOSettelmentAmt.Text = Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["PO_SettelmentAmt"]).Trim();

                if (Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["powo_signCopy"]).Trim() != "")
                {
                    lnkfile_PO.Text = Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["powo_signCopy"]).Trim();
                    spnPOWOSignCopy.Visible = true;
                    lnkfile_PO.Visible = true;
                }
            }
			if (DTPoWoNumber.Tables[1].Rows.Count > 0)
			{
				DgvMilestones.DataSource = DTPoWoNumber.Tables[1];
				DgvMilestones.DataBind();
			}
			if (DTPoWoNumber.Tables[6].Rows.Count > 0)
			{
				GrdInvoiceDetails.DataSource = DTPoWoNumber.Tables[6];
				GrdInvoiceDetails.DataBind();
			}
            if (DTPoWoNumber.Tables[7].Rows.Count > 0)
            {
                GridViewAccountpaidbyAmount.DataSource = DTPoWoNumber.Tables[7];
                GridViewAccountpaidbyAmount.DataBind();
                txtAccountPaidAmt.Enabled = false;
            }
            else
            {
                txtAccountPaidAmt.Enabled = true;
            }

            if (DTPoWoNumber.Tables[3].Rows.Count > 0)
			{
				//DivCreateInvoice.Visible = true;
				hdnMstoneID.Value = DTPoWoNumber.Tables[3].Rows[0]["MstoneID"].ToString();
				hdnInvoiceID.Value= DTPoWoNumber.Tables[3].Rows[0]["InvoiceID"].ToString();
				txtInvoiceNo.Text = DTPoWoNumber.Tables[3].Rows[0]["InvoiceNo"].ToString();
				txtInvoiceAmount.Text = DTPoWoNumber.Tables[3].Rows[0]["AmtWithTax"].ToString(); // //AmtWithTax Payable_Amt_With_Tax
				txtInvoiceBalAmt.Text = DTPoWoNumber.Tables[3].Rows[0]["BalanceAmt"].ToString();
				//txtVendorName.Text = DTCreateInvoice.Rows[0]["Status_id"].ToString();
				//txtGSTINNO.Text = DTCreateInvoice.Rows[0]["PaymentStatusID"].ToString();
				txtPaymentRequestNo.Text = DTPoWoNumber.Tables[3].Rows[0]["PaymentReqNo"].ToString();
				txtRequestDate.Text = DTPoWoNumber.Tables[3].Rows[0]["PaymentReqDate"].ToString();

				txtAmountPaidWithTax.Text = DTPoWoNumber.Tables[3].Rows[0]["TobePaidAmtWithtax"].ToString();
				hdnEmpCodePrve.Value = DTPoWoNumber.Tables[3].Rows[0]["Emp_Code"].ToString();
                txtAccountAmtBal.Text = DTPoWoNumber.Tables[3].Rows[0]["TobePaidAmtWithtax"].ToString();
                txtAccountPaidAmt.Text= DTPoWoNumber.Tables[3].Rows[0]["TobePaidAmtWithtax"].ToString();

                txtPaymentRequestAmount.Text = DTPoWoNumber.Tables[3].Rows[0]["paymentRequestForAmt"].ToString(); //DirectTax_Amount
                txtInvoiceTDSAmt.Text = DTPoWoNumber.Tables[3].Rows[0]["DirectTax_Amount"].ToString();
                txtInvoicePaidAmount.Text = DTPoWoNumber.Tables[3].Rows[0]["AccountPaidAmt"].ToString(); //InvoicePaidAmt;
                lnkfile_Invoice.Text = DTPoWoNumber.Tables[3].Rows[0]["Invoice_File"].ToString();


                txtIsSecurity_DepositInvoice.Text = Convert.ToString(DTPoWoNumber.Tables[3].Rows[0]["IsSecurity_Deposit_Invoice"]).Trim().ToLower();

                lblheading.Text = "Approval Payment Request - " + txtPaymentRequestNo.Text + "";// + DTPoWoNumber.Tables[3].Rows[0]["PyamentStatus"].ToString();
                getInvoiceUploadedFiles();

            }
			if (DTPoWoNumber.Tables[4].Rows.Count > 0)
			{
				GrdFileUpload.DataSource = DTPoWoNumber.Tables[4];
				GrdFileUpload.DataBind();
                spnSupportingFiles.Visible = true;
                liPaymentSupportingFiles_1.Visible = true;
                liPaymentSupportingFiles_2.Visible = true;
                liPaymentSupportingFiles_2.Visible = true;
                liPaymentSupportingFiles_4.Visible = true;

            }
			if (DTPoWoNumber.Tables[5].Rows.Count > 0)
			{
				SPPayHist.Visible = true;
				GrdInvoiceHistDetails.DataSource = DTPoWoNumber.Tables[5];
				GrdInvoiceHistDetails.DataBind();
			}
		if (DTPoWoNumber.Tables[8].Rows.Count > 0)
            {
                GrdInvoiceApr.DataSource = DTPoWoNumber.Tables[8];
                GrdInvoiceApr.DataBind();
            }
			getApproverlist(hdnEmpCode.Value, Payment_ID, hdnDept_Name.Value, hdnCostCentre.Value);
			DataTable dsapproverNxt = new DataTable();
			dsapproverNxt = spm.GetNext_Payment_Request_ApproverDetails("Get_NextApproverDetails_mail", hdnEmpCode.Value, Convert.ToInt32(Payment_ID), hdnDept_Name.Value, hdnCostCentre.Value,Convert.ToString(txtPoWoType.Text));
			if (dsapproverNxt.Rows.Count > 0)
			{
				apprid = (int)dsapproverNxt.Rows[0]["APPR_ID"];
				nxtapprcode = (string)dsapproverNxt.Rows[0]["APPR_Emp_Code"];
				nxtapprname = (string)dsapproverNxt.Rows[0]["Emp_Name"];
				approveremailaddress = (string)dsapproverNxt.Rows[0]["Emp_Emailaddress"];
				hdnnextappcode.Value = nxtapprcode;
				hdnapprid.Value = apprid.ToString();
				hdnApproverEmail.Value = approveremailaddress;
				hdnNextappName.Value = nxtapprname;
			}
			else
			{
				hdnstaus.Value = "Final Approver";
				Get_Account_ApproverCode();
			}
		}
		catch (Exception)
		{

			throw;
		}
	}

    protected void txtMilestoneInvoiceAmt_TextChanged(object sender, EventArgs e)
    {

        double dMilesStoneAmt_forInvoice = 0;
        double dAmtPaidWithTax_forInvoice = 0;
        decimal Result = 0;
        string Strclear = "";
        string strmsg = "";
        for (int i = 0; i <= GridViewAccountpaidbyAmount.Rows.Count - 1; i++)
        {   

            decimal MSInvoiceAmt = 0;
            GridViewRow ro = GridViewAccountpaidbyAmount.Rows[i];
            TextBox txtMSInvoiceAmt  = (TextBox)ro.FindControl("txtMilestoneInvoiceAmt");
            Label lblamountcheck     = (Label)ro.FindControl("lblamountcheck"); 
            Label lblAmounttobepaidd = (Label)ro.FindControl("lblAmounttobepaidd");

                 if (Convert.ToString(txtMSInvoiceAmt.Text).Trim() == "")
                   {
                      MSInvoiceAmt = Convert.ToDecimal(MSInvoiceAmt);
                   }
                 else
                  {
                     MSInvoiceAmt = Convert.ToDecimal(txtMSInvoiceAmt.Text);
                     Strclear = "msg";
                  }
                 if (Convert.ToDecimal(MSInvoiceAmt) <= Convert.ToDecimal(lblAmounttobepaidd.Text))
                  {
                    dMilesStoneAmt_forInvoice += Convert.ToDouble(MSInvoiceAmt);
                    dAmtPaidWithTax_forInvoice += Convert.ToDouble(lblAmounttobepaidd.Text);
                    txtAccountPaidAmt.Text = Convert.ToDouble(dMilesStoneAmt_forInvoice).ToString();
                    txtAccountPaidAmt.Text = Math.Round(Convert.ToDecimal(txtAccountPaidAmt.Text), 2).ToString();

                    Result = Convert.ToDecimal(dAmtPaidWithTax_forInvoice) - Convert.ToDecimal(txtAccountPaidAmt.Text);
                    txtAccountAmtBal.Text = Result.ToString();
                    lblmessage.Text = "";
                   strmsg = "msgg";
                 }
                 else
                  {
                   strmsg = "";
                   txtMSInvoiceAmt.Text = "";
                    
                  }
            }

            if (Strclear == "")
            {
              txtAccountPaidAmt.Text = "";
            }
            if (strmsg == "")
            {
            lblmessage.Text = "Amount to be paid cannot exceed Amount To be Requested! ";
            return;
        }
    }

    public void getInvoiceUploadedFiles()
    {

        DataSet dsFiles = spm.getVSCB_UploadedFiles("get_VSCB_UploadedFiels", "Invoice", Convert.ToDouble(hdnInvoiceID.Value), "", "");
        gvuploadedFiles.DataSource = null;
        gvuploadedFiles.DataBind();
        if (dsFiles.Tables[0].Rows.Count > 0)
        {
            gvuploadedFiles.DataSource = dsFiles;
            gvuploadedFiles.DataBind();
            spnSupportinFiles.Visible = true;
            divInvoiceSupportingFiles.Visible = true;
        }
    }

    private void CheckPayment_ApprovalStatus_Submit()
	{
		try
		{
			SqlParameter[] spars = new SqlParameter[3];
			spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
			spars[0].Value = "Check_PaymentAppr_Status";
			spars[1] = new SqlParameter("@Payment_ID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdnPayment_ID.Value);
			spars[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[2].Value = Convert.ToString(hdnEmpCode.Value).Trim();
			dtPaymentStatus = spm.getMobileRemDataList(spars, "sp_VSCB_CreatePOWO_Users");
			if (dtPaymentStatus.Rows.Count == 0)
			{
				Response.Redirect("~/procs/VSCB_InboxPaymentRequest.aspx");
			}
			if (dtPaymentStatus.Rows.Count > 0)
			{
				if (Convert.ToString(dtPaymentStatus.Rows[0]["pvappstatus"]) != "Pending")
				{
					Response.Redirect("~/procs/VSCB_InboxPaymentRequest.aspx");
				}
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}
	protected void GetPayment_CurrentApprID()
	{
		int capprid;
		string Actions = "";
		DataTable dtCApprID = new DataTable();
		dtCApprID = spm.GetCurrent_Payment_ApprID("check_Current_PaymentAppr_Status", Convert.ToInt32(hdnPayment_ID.Value), hdnEmpCode.Value);
		capprid = (int)dtCApprID.Rows[0]["APPR_ID"];
		Actions = (string)dtCApprID.Rows[0]["Action"];
		hdnCurrentID.Value = capprid.ToString();

		if (Convert.ToString(hdnCurrentID.Value).Trim() == "")
		{
			lblmessage.Text = "Acton on this Request not yet taken by other approvals";
			return;
		}
		else if (Convert.ToString(Actions).Trim() != "Pending")
		{
			lblmessage.Text = "You already actioned for this request";
			return;
		}
	}
	private void getApproverlist(string EmpCode, int Payment_ID, string DeptnName, string TallyCode)
	{
		DataTable dtapprover = new DataTable();
		dtapprover = spm.Get_Payment_Request_ApproverList(EmpCode, Payment_ID, DeptnName, TallyCode,Convert.ToString(txtPoWoType.Text).Trim());
		DgvApprover.DataSource = null;
		DgvApprover.DataBind();
		if (dtapprover.Rows.Count > 0)
		{
			DgvApprover.DataSource = dtapprover;
			DgvApprover.DataBind();
		}
	}
	public void Get_Account_ApproverCode()
	{
		DataSet dsTrDetails = new DataSet();
		SqlParameter[] spars = new SqlParameter[3];
		spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
		spars[0].Value = "Get_Account_mail_Details";
		spars[2] = new SqlParameter("@Emp_code", SqlDbType.VarChar);
		spars[2].Value = hdnEmpCode.Value;
		dsTrDetails = spm.getDatasetList(spars, "SP_VSCB_GETPAYMENT_APPMETRIX");
		hdnApproverType.Value = "Approver";
		if (dsTrDetails.Tables[0].Rows.Count > 0)
		{
			hdnnextappcode.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_code"]).Trim();
			hdnapprid.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_id"]).Trim();
			approveremailaddress = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();
			hdnAccountApp_Email.Value = approveremailaddress;
			hdnApproverEmail.Value = approveremailaddress;
			hdnNextappName.Value =  Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Emp_Name"]).Trim(); //"Account Department";
            hdnApproverType.Value = "NA";
		}
	}
	public void CheckAccountApprovalEmp()
	{
		try
		{
			hdnExtraAPP.Value = "";
			dtextraApp = spm.Get_Payment_AccountApprovalEmp("Get_Account_Payment_Details", Convert.ToString(Session["Empcode"]).Trim(), Convert.ToInt32(hdnPayment_ID.Value));
			if (dtextraApp.Rows.Count > 0)
			{
				hdnExtraAPP.Value = (string)dtextraApp.Rows[0]["app_remarks"].ToString().Trim();
				//hdnExtraAPPID.Value = (string)dtextraApp.Rows[0]["Appr_id"].ToString().Trim();
				if (hdnExtraAPP.Value.ToString().Trim() == "Account")
				{
					localtrvl_btnSave.Text = "Verify";
					localtrvl_btnSave.ToolTip = "Verify";
					Account1.Visible = true;
					Account2.Visible = true;
					Account3.Visible = true;
                    SpanAccounttobepaid.Visible = true;
                    Span2.Visible = true;
                }
            }
			else
			{
                SpanAccounttobepaid.Visible = false;
                Span2.Visible = false;
                Account1.Visible = false;
				Account2.Visible = false;
				Account3.Visible = false;
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
			throw;
		}
	}
	protected void localtrvl_btnSave_Click(object sender, EventArgs e)
	{
		try
		{
			lblmessage.Text = ""; string EmpCode = "", strtoDate = "" ;
			int Payment_ID = 0, Payment_Status_ID=0, InvoiceID=0, MstoneID=0;
			DateTime PaymentDate; string[] strdate;
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}
			if (hdnExtraAPP.Value.ToString().Trim() == "Account")
			{
                string strmsgg = "";
                for (int i = 0; i <= GridViewAccountpaidbyAmount.Rows.Count - 1; i++)
                {
                    GridViewRow ro = GridViewAccountpaidbyAmount.Rows[i];
                    TextBox txtMSInvoiceAmt = (TextBox)ro.FindControl("txtMilestoneInvoiceAmt");
                    if(txtMSInvoiceAmt.Text.Trim() !="" || txtMSInvoiceAmt.Text.Trim() == "0")
                    {
                        strmsgg = "msg";
                    }
                }
                if (Convert.ToString(txtIsSecurity_DepositInvoice.Text).Trim() == "false")
                {
                    if (strmsgg == "")
                    {
                        lblmessage.Text = "Please enter Amount paid by Accounts";
                        return;
                    }
                }

                if (Convert.ToString(txtAccountPaidAmt.Text).Trim() == "" || Convert.ToString(txtAccountPaidAmt.Text).Trim() == "0")
                {
                    lblmessage.Text = "Please enter Amount paid by Accounts";
                    return;
                }
            }
			if (Convert.ToString(Session["Empcode"]).Trim() != "" || Session["Empcode"] != null)
			{
				EmpCode = Convert.ToString(Session["Empcode"]).ToString();
			}
			
		   Payment_ID = Convert.ToString(hdnPayment_ID.Value).Trim() != "" ? Convert.ToInt32(hdnPayment_ID.Value) : 0;
		   InvoiceID = Convert.ToString(hdnInvoiceID.Value).Trim() != "" ? Convert.ToInt32(hdnInvoiceID.Value) : 0;
		   MstoneID = Convert.ToString(hdnMstoneID.Value).Trim() != "" ? Convert.ToInt32(hdnMstoneID.Value) : 0;

			if (hdnExtraAPP.Value.ToString().Trim() == "Account")
			{
				decimal BalAmt = 0;
				strdate = Convert.ToString(txtRequestDate.Text).Trim().Split('-');
				strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
				PaymentDate = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

				if (Convert.ToString(txtAmountPaidWithTax.Text).Trim()!="" && Convert.ToString(txtAccountPaidAmt.Text).Trim() != "")
				{
					BalAmt = Convert.ToDecimal(txtAmountPaidWithTax.Text) - Convert.ToDecimal(txtAccountPaidAmt.Text);
					if (BalAmt > 0)
					{
						Payment_Status_ID = 3;
					}
					else
					{
						Payment_Status_ID = 1;
					}
				}
				DTInsertPayment = spm.dtInsert_Payment_Request("PAYMNETACCOUNTREQUEST", EmpCode, InvoiceID, MstoneID, Payment_ID, "", Convert.ToDecimal(txtAmountPaidWithTax.Text), PaymentDate, 2, Payment_Status_ID, Convert.ToDecimal(txtAccountPaidAmt.Text), Convert.ToDecimal(txtAccountAmtBal.Text), txtPaymentRequestNo.Text.Trim(), txtApprovalRemark.Text);

                for (int i = 0; i <= GridViewAccountpaidbyAmount.Rows.Count - 1; i++)
                {
                    GridViewRow ro = GridViewAccountpaidbyAmount.Rows[i];
                    TextBox txtMSInvoiceAmt = (TextBox)ro.FindControl("txtMilestoneInvoiceAmt");
                    string HDmilestoneid = "";
                    //if (Convert.ToString(txtMSInvoiceAmt.Text).Trim() != "")
                    //{
                    HDmilestoneid = Convert.ToString(GridViewAccountpaidbyAmount.DataKeys[ro.RowIndex].Values[0]).Trim();
                    SqlParameter[] spars = new SqlParameter[6];
                    spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
                    spars[0].Value = "PaymentRequestMileStoneAmountUpdate";
                    spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
                    spars[1].Value = EmpCode;
                    spars[2] = new SqlParameter("@InvoiceID", SqlDbType.VarChar);
                    spars[2].Value = InvoiceID;
                    spars[3] = new SqlParameter("@MstoneID", SqlDbType.VarChar);
                    spars[3].Value = HDmilestoneid;
                    spars[4] = new SqlParameter("@TobePaidAmtWithtax", SqlDbType.VarChar);
                    if (Convert.ToString(txtMSInvoiceAmt.Text).Trim() != "")
                    {
                        spars[4].Value = txtMSInvoiceAmt.Text.Trim();
                    }
                    else
                    {
                        spars[4].Value = 0;
                    }

                        
                        spars[5] = new SqlParameter("@Payment_ID", SqlDbType.VarChar);
                        spars[5].Value = Payment_ID;
                        DataSet dsApprovalStatusReport = spm.getDatasetList(spars, "sp_VSCB_CreatePOWO_Users");
                   // }
                }

            }
			String strPatmentURL = "";
			strPatmentURL = (Convert.ToString(ConfigurationManager.AppSettings["Link_PaymentRequestAPP"]).Trim() + "?Payment_ID=" + Payment_ID);
			if (Convert.ToString((hdnstaus.Value).Trim()) != "")
			{				
				if (Convert.ToString(hdnApproverType.Value).Trim() != "Approver" && Convert.ToString(hdnCurrentID.Value).Trim() != Convert.ToString(hdnapprid.Value).Trim())
				{
					if (Convert.ToString(hdnApproverType.Value).Trim() != "Approver")
					{						
						spm.Insert_Payment_Approver_Request(hdnnextappcode.Value, Convert.ToInt32(hdnapprid.Value), Convert.ToInt32(hdnPayment_ID.Value));
					}
					spm.Update_Payment_Request_Apporver(Convert.ToInt32(hdnPayment_ID.Value), "Approved", txtApprovalRemark.Text, Convert.ToString(("").Trim()), Convert.ToInt32(hdnCurrentID.Value));
				}
				else
				{
					spm.Update_Payment_Request_Apporver(Convert.ToInt32(hdnPayment_ID.Value), "Approved", txtApprovalRemark.Text, Convert.ToString((hdnstaus.Value).Trim()), Convert.ToInt32(hdnCurrentID.Value));
				}
			}
			else
			{
				spm.Update_Payment_Request_Apporver(Convert.ToInt32(hdnPayment_ID.Value), "Approved", txtApprovalRemark.Text, Convert.ToString(""), Convert.ToInt32(hdnCurrentID.Value));
				spm.Insert_Payment_Approver_Request(hdnnextappcode.Value, Convert.ToInt32(hdnapprid.Value), Convert.ToInt32(hdnPayment_ID.Value));
			}
			string strapproverlist = "", ccMailIDs = "",PowoNo= "";
			var Tbody = "";
			GetEmployeeCode(hdnEmpCodePrve.Value);
			if (txtPOWONumber.Text != "")
			{
				PowoNo = txtPOWONumber.Text;
			}
			else
			{
				PowoNo = txtInvoiceNo.Text;
			}
			string strsubject = "OneHR: Request for - Payment Approval  - “" + txtPaymentRequestNo.Text + "”  For “" + PowoNo + "”";
			strapproverlist = GetPaymnetApprove_RejectList(hdnEmpCode.Value, Payment_ID, hdnDept_Name.Value, hdnCostCentre.Value);
			ccMailIDs = GetPayment_Approval_CCMail_Deatils(Payment_ID, hdnEmpCode.Value);
			//GetProject_CostCenter();
			GetInvoiceTDSAmt();
            //GetMilestoneDetails();
            DataSet dsMilestone = GetMilestoneDetails();

            string CostCenter = Convert.ToString(txtTallyCode_display.Text).Trim(); //hdnCostCentre.Value;
			if (hdnExtraAPP.Value.ToString().Trim() != "Account")
			{
				var actionByName = Session["emp_loginName"].ToString();
				Tbody = "<b>"+ hdnEmpCodePrveName.Value + "</b> has created a payment request as per the details below. Request your approval please.";
				spm.send_mailto_VSCB_Payment_Request_Next_Approver(hdnNextappName.Value, hdnLoginEmpEmail.Value, hdnApproverEmail.Value, strsubject, Tbody, ccMailIDs, strPatmentURL, strapproverlist, txtPOWONumber.Text, txtPoWoTitle.Text, txtPoWoDate.Text, txtPoWoType.Text, txtVendorName.Text, txtPoWoAmtWithTaxes.Text, txtPoWOPaidAmt.Text, txtPoWOPaidBalAmt.Text, txtProjectName.Text, txtInvoiceNo.Text, txtInvoiceAmount.Text, txtInvoiceBalAmt.Text, txtPaymentRequestNo.Text, txtRequestDate.Text, txtAmountPaidWithTax.Text,CostCenter, hdnDirectTax_Type.Value, hdnDirectTax_Percentage.Value, hdnDirectTax_Amount.Value, hdnPayable_Amt_Invoice.Value, MstoneID.ToString(), hdnMilestoneName.Value, hdnMilestAmount.Value, hdnMilestCGST_Amt.Value, hdnMilestSGST_Amt.Value, hdnMilestIGST_Amt.Value, hdnMilestAmtWithTax.Value, hdnMilestPyamentStatus.Value,dsMilestone);
			}
			else
			{
                DataSet dsFormatAmount = spm.getFormated_Amount("get_formated_currncy_Amount", Convert.ToString(txtCurrency.Text), Convert.ToDecimal(txtAccountPaidAmt.Text), Convert.ToDecimal(txtAccountAmtBal.Text), Convert.ToDecimal(txtPoWOPaidAmt.Text), Convert.ToDecimal(txtPoWOPaidBalAmt.Text), 0);
                string sformatPaymentpaidAmt = Convert.ToString(dsFormatAmount.Tables[0].Rows[0]["Amount_A"]);
                string sformatPaymentBalAmt = Convert.ToString(dsFormatAmount.Tables[0].Rows[0]["Amount_B"]);
                string sformattxtPoWOPaidAmt = Convert.ToString(dsFormatAmount.Tables[0].Rows[0]["Amount_C"]);
                string sformattxtPoWOPaidBalAmt = Convert.ToString(dsFormatAmount.Tables[0].Rows[0]["Amount_D"]);

                strPatmentURL = "";
				Tbody = "This is to inform you that "+ hdnNextappName.Value + " has Approved below payment request.";
				spm.send_mailto_VSCB_Payment_Request_Account_Approver(hdnEmpCodePrveName.Value, hdnLoginEmpEmail.Value, hdnEmpCodePrveEmailID.Value, strsubject, Tbody, ccMailIDs, strPatmentURL, strapproverlist, txtPOWONumber.Text, txtPoWoTitle.Text, txtPoWoDate.Text, txtPoWoType.Text, txtVendorName.Text, txtPoWoAmtWithTaxes.Text, sformattxtPoWOPaidAmt, sformattxtPoWOPaidBalAmt, txtProjectName.Text, txtInvoiceNo.Text, txtInvoiceAmount.Text, txtInvoiceBalAmt.Text, txtPaymentRequestNo.Text, txtRequestDate.Text, txtAmountPaidWithTax.Text, sformatPaymentpaidAmt, sformatPaymentBalAmt, CostCenter, hdnDirectTax_Type.Value, hdnDirectTax_Percentage.Value, hdnDirectTax_Amount.Value, hdnPayable_Amt_Invoice.Value, MstoneID.ToString(), hdnMilestoneName.Value, hdnMilestAmount.Value, hdnMilestCGST_Amt.Value, hdnMilestSGST_Amt.Value, hdnMilestIGST_Amt.Value, hdnMilestAmtWithTax.Value, hdnMilestPyamentStatus.Value,dsMilestone);
			}

			Response.Redirect("~/procs/VSCB_InboxPaymentRequest.aspx",false);
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}
	public string GetPayment_Approval_CCMail_Deatils(int PaymentID, string Assgineto)
	{

		try
		{
			String sbapp = "";
			DataTable dsTrDetails = new DataTable();
			dsTrDetails = spm.Get_Payment_Request_Approver_CCmail("Get_Intermidates_Payment_list", Assgineto, PaymentID,"","");
			if (dsTrDetails.Rows.Count > 0)
			{
				for (int i = 0; i < dsTrDetails.Rows.Count; i++)
				{
					if (Convert.ToString(sbapp).Trim() == "")
						sbapp = Convert.ToString(dsTrDetails.Rows[0]["Emp_Emailaddress"]).Trim();
					else
						sbapp = sbapp + ";" + Convert.ToString(dsTrDetails.Rows[i]["Emp_Emailaddress"]).Trim();

				}
			}
			return Convert.ToString(sbapp);
		}
		catch (Exception)
		{

			throw;
		}


	}
	public void GetEmployeeCode(string strtype)
	{

		DataSet dsTrDetails = new DataSet();
		SqlParameter[] spars = new SqlParameter[3];
		spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
		spars[0].Value = "GetEmployeeDetails";
		spars[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
		spars[2].Value = strtype;
		dsTrDetails = spm.getDatasetList(spars, "sp_VSCB_CreatePOWO_Users");
		if (dsTrDetails.Tables[0].Rows.Count > 0)
		{
			hdnEmpCodePrveName.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Emp_Name"]).Trim();
			hdnEmpCodePrveEmailID.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();	
		}
	}
	private void GetProject_CostCenter()
	{
		try
		{
			DataTable dtCostCenter = new DataTable();
			dtCostCenter = spm.Get_Project_CostCenter("GetDeptCostCenter", hdnEmpCode.Value, txtProjectName.Text.Trim());
			if (dtCostCenter.Rows.Count > 0)
			{
				hdnCostCentre.Value = dtCostCenter.Rows[0]["CostCentre"].ToString();
			}
		}
		catch (Exception)
		{

			throw;
		}
	}

	private DataSet GetMilestoneDetails()
	{
        DataSet dsMilestone = new DataSet();
		try
		{
            DataSet dtPOWODetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
            spars[0].Value = "InvoiceMilestoneList_Payment";

            spars[1] = new SqlParameter("@Payment_ID", SqlDbType.VarChar);
            spars[1].Value = hdnPayment_ID.Value;


            dsMilestone = spm.getDatasetList(spars, "sp_VSCB_CreatePOWO_Users");

        }
        catch (Exception)
		{

			throw;
		}
        return dsMilestone;
	}
	private void GetInvoiceTDSAmt()
	{
		try
		{
			DataTable dtCostCenter = new DataTable();
			int InvoiceID = 0;
			InvoiceID = Convert.ToString(hdnInvoiceID.Value).Trim() != "" ? Convert.ToInt32(hdnInvoiceID.Value) : 0;
			dtCostCenter = spm.GetInvoiceTDS_Amt("GetInvoiceTDSAmt", hdnEmpCode.Value, InvoiceID);
			if (dtCostCenter.Rows.Count > 0)
			{
				hdnDirectTax_Type.Value = dtCostCenter.Rows[0]["DirectTax_Type"].ToString();
				hdnDirectTax_Percentage.Value = dtCostCenter.Rows[0]["DirectTax_Percentage"].ToString();
				hdnDirectTax_Amount.Value = dtCostCenter.Rows[0]["DirectTax_Amount"].ToString();
				hdnPayable_Amt_Invoice.Value = dtCostCenter.Rows[0]["Payable_Amt_With_Tax"].ToString();
			}
		}
		catch (Exception)
		{

			throw;
		}
	}
	protected string GetPaymnetApprove_RejectList(string EmpCode, int Payment_ID, string DeptName, string TallyCode)
	{
		StringBuilder strbuild_Approvers = new StringBuilder();
		strbuild_Approvers.Length = 0;
		strbuild_Approvers.Capacity = 0;
		DataTable dtAppRej = new DataTable();
		dtAppRej = spm.Get_Payment_Request_ApproverList(EmpCode, Payment_ID, DeptName, TallyCode,Convert.ToString(txtPoWoType.Text).Trim());
		if (dtAppRej.Rows.Count > 0)
		{
			strbuild_Approvers.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;width:100%;border:1px solid'>");
			strbuild_Approvers.Append("<tr style='background-color:#C7D3D4'><td style='border:1px solid'>Approver Name</td><td style='border:1px solid'>Status</td><td style='border:1px solid'>Approved On</td><td style='border:1px solid'>Approver Remarks</td></tr>");
			for (Int32 irow = 0; irow < dtAppRej.Rows.Count; irow++)
			{
				strbuild_Approvers.Append("<tr><td style='border:1px solid'>" + Convert.ToString(dtAppRej.Rows[irow]["tName"]).Trim() + " </td>");
				strbuild_Approvers.Append("<td style='border:1px solid'>" + Convert.ToString(dtAppRej.Rows[irow]["Status"]).Trim() + "</td>");
				strbuild_Approvers.Append("<td style='border:1px solid'>" + Convert.ToString(dtAppRej.Rows[irow]["tdate"]).Trim() + "</td>");
				strbuild_Approvers.Append("<td style='border:1px solid'>" + Convert.ToString(dtAppRej.Rows[irow]["Comment"]).Trim() + "</td></tr>");
			}
			strbuild_Approvers.Append("</table>");
		}
		return Convert.ToString(strbuild_Approvers);
	}

	protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
	{
		try
		{
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			string strapprovermails = "";
			if (confirmValue != "Yes")
			{
				return;
			}
			if (Convert.ToString((txtApprovalRemark.Text).Trim()) == "")
			{
				lblmessage.Text = "Please mention the comment before rejecting the Remark";
				return;
			}
			string PowoNo = "";
			if (txtPOWONumber.Text != "")
			{
				PowoNo = txtPOWONumber.Text;
			}
			else
			{
				PowoNo = txtInvoiceNo.Text;
			}
			int MstoneID = 0;
			string strapproverlist = "",strPatmentURL = "", ccMailIDs="";
			MstoneID = Convert.ToString(hdnMstoneID.Value).Trim() != "" ? Convert.ToInt32(hdnMstoneID.Value) : 0;
			spm.VSCB_Reject_Payment_Request("Payment_Reject",Convert.ToInt32(hdnPayment_ID.Value), hdnEmpCode.Value, Convert.ToInt32(hdnCurrentID.Value), 4,txtApprovalRemark.Text);
			GetEmployeeCode(hdnEmpCodePrve.Value);
			strapproverlist = GetPaymnetApprove_RejectList(hdnEmpCode.Value, Convert.ToInt32(hdnPayment_ID.Value), hdnDept_Name.Value, hdnCostCentre.Value);
			ccMailIDs = GetPayment_Approval_CCMail_Deatils(Convert.ToInt32(hdnPayment_ID.Value), hdnEmpCode.Value);
			string strsubject = "OneHR: Payment request - “" + txtPaymentRequestNo.Text + "” Rejected against “" + PowoNo + "”";
			var actionByName = Session["emp_loginName"].ToString();
			//GetProject_CostCenter();
			GetInvoiceTDSAmt();
            //GetMilestoneDetails();
             
            DataSet dsMilestone = GetMilestoneDetails();

            string CostCenter = Convert.ToString(txtTallyCode_display.Text); // hdnCostCentre.Value;
			var Tbody = "This is to inform you that " + hdnLoginUserName.Value + " has<b> Rejected</b> Payment request  as per the details below.";
			spm.send_mail_VSCB_Payment_Rejection_Correction(hdnEmpCodePrveName.Value,  hdnEmpCodePrveEmailID.Value, hdnLoginEmpEmail.Value, Tbody, strsubject, ccMailIDs, strPatmentURL, strapproverlist, txtPOWONumber.Text, txtPoWoTitle.Text, txtPoWoDate.Text, txtPoWoType.Text, txtVendorName.Text, txtPoWoAmtWithTaxes.Text, txtPoWOPaidAmt.Text, txtPoWOPaidBalAmt.Text, txtProjectName.Text, txtInvoiceNo.Text, txtInvoiceAmount.Text, txtInvoiceBalAmt.Text, txtPaymentRequestNo.Text, txtRequestDate.Text, txtAmountPaidWithTax.Text,CostCenter, hdnDirectTax_Type.Value, hdnDirectTax_Percentage.Value, hdnDirectTax_Amount.Value,hdnPayable_Amt_Invoice.Value, MstoneID.ToString(), hdnMilestoneName.Value, hdnMilestAmount.Value, hdnMilestCGST_Amt.Value, hdnMilestSGST_Amt.Value, hdnMilestIGST_Amt.Value, hdnMilestAmtWithTax.Value, hdnMilestPyamentStatus.Value, dsMilestone);
			Response.Redirect("~/procs/VSCB_InboxPaymentRequest.aspx");
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}

	protected void lnkdelete_Click(object sender, ImageClickEventArgs e)
	{

	}

	protected void accmo_delete_btn_Click(object sender, EventArgs e)
	{
		try
		{
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			string strapprovermails = "";
			if (confirmValue != "Yes")
			{
				return;
			}
			if (Convert.ToString((txtApprovalRemark.Text).Trim()) == "")
			{
				lblmessage.Text = "Please mention the comment before Send for Correction the Remark";
				return;
			}
			string PowoNo = "";
			if (txtPOWONumber.Text != "")
			{
				PowoNo = txtPOWONumber.Text;
			}
			else
			{
				PowoNo = txtInvoiceNo.Text;
			}
			int MstoneID = 0;
			string strapproverlist = "", strPatmentURL = "", ccMailIDs;
			 MstoneID = Convert.ToString(hdnMstoneID.Value).Trim() != "" ? Convert.ToInt32(hdnMstoneID.Value) : 0;
			spm.VSCB_Reject_Payment_Request("Payment_Correction", Convert.ToInt32(hdnPayment_ID.Value), hdnEmpCode.Value, Convert.ToInt32(hdnCurrentID.Value), 5, txtApprovalRemark.Text);
			GetEmployeeCode(hdnEmpCodePrve.Value);
			strapproverlist = GetPaymnetApprove_RejectList(hdnEmpCode.Value, Convert.ToInt32(hdnPayment_ID.Value), hdnDept_Name.Value, hdnCostCentre.Value);
			ccMailIDs = GetPayment_Approval_CCMail_Deatils(Convert.ToInt32(hdnPayment_ID.Value), hdnEmpCode.Value);
			string strsubject = "OneHR: Payment request your Sent Back Correction “" + txtPaymentRequestNo.Text + "” For “"+ PowoNo +"”";
			var actionByName = Session["emp_loginName"].ToString();
			//GetProject_CostCenter();
			GetInvoiceTDSAmt();
            //GetMilestoneDetails();

            DataSet dsMilestone = GetMilestoneDetails();

            string CostCenter = Convert.ToString(txtTallyCode_display.Text).Trim(); // hdnCostCentre.Value;
			var Tbody = "<b> " + hdnLoginUserName.Value + "</b> has Sent back the payment request for <b> Correction.</b> Please correct the same as instructed and resend for approval.";
			spm.send_mail_VSCB_Payment_Rejection_Correction(hdnEmpCodePrveName.Value, hdnEmpCodePrveEmailID.Value, hdnLoginEmpEmail.Value,  Tbody, strsubject, ccMailIDs, strPatmentURL, strapproverlist, txtPOWONumber.Text, txtPoWoTitle.Text, txtPoWoDate.Text, txtPoWoType.Text, txtVendorName.Text, txtPoWoAmtWithTaxes.Text, txtPoWOPaidAmt.Text, txtPoWOPaidBalAmt.Text, txtProjectName.Text, txtInvoiceNo.Text, txtInvoiceAmount.Text, txtInvoiceBalAmt.Text, txtPaymentRequestNo.Text, txtRequestDate.Text, txtAmountPaidWithTax.Text,CostCenter, hdnDirectTax_Type.Value, hdnDirectTax_Percentage.Value, hdnDirectTax_Amount.Value, hdnPayable_Amt_Invoice.Value, MstoneID.ToString(), hdnMilestoneName.Value, hdnMilestAmount.Value, hdnMilestCGST_Amt.Value, hdnMilestSGST_Amt.Value, hdnMilestIGST_Amt.Value, hdnMilestAmtWithTax.Value, hdnMilestPyamentStatus.Value, dsMilestone);
			Response.Redirect("~/procs/VSCB_InboxPaymentRequest.aspx");
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}

	protected void txtAccountPaidAmt_TextChanged(object sender, EventArgs e)
	{
		decimal Result = 0;
		if (txtAccountPaidAmt.Text != "")
		{

			if (Convert.ToDecimal(txtAccountPaidAmt.Text) <= Convert.ToDecimal(txtAmountPaidWithTax.Text))
			{
				txtAccountPaidAmt.Text = Math.Round(Convert.ToDecimal(txtAccountPaidAmt.Text), 2).ToString();
				Result = Convert.ToDecimal(txtAmountPaidWithTax.Text) - Convert.ToDecimal(txtAccountPaidAmt.Text);
				txtAccountAmtBal.Text = Result.ToString();
			}
			else
			{
				txtAccountAmtBal.Text = "";
				txtAccountPaidAmt.Text = "";
			}
		}
		else
		{
			txtAccountAmtBal.Text = "";
		}
	}

    protected void lnkfile_PO_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBSignPOWOfiles"]).Trim()), lnkfile_PO.Text);
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



    protected void btnTra_Details_Click(object sender, EventArgs e)
    {
        DataSet dsPOWOContent = new DataSet();

        #region get POWO Content 


        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_POWO_Content_FromTally";

        spars[1] = new SqlParameter("@POWO_Number", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtPOWONumber.Text).Trim();  //"PO/042021/00001"; 

        spars[2] = new SqlParameter("@POWOID", SqlDbType.Decimal);
        spars[2].Value = Convert.ToDouble(hdnPOID.Value);  //"PO/042021/00001";


        dsPOWOContent = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");

        #endregion

        try
        {



            string strpath = Server.MapPath("~/procs/VSCB_Rpt_POWO_Content_New.rdlc");
            string PowoNumber = Convert.ToString(txtPOWONumber.Text).Trim().Replace("/", "-");

            LocalReport ReportViewer2 = new LocalReport();
            ReportViewer2.ReportPath = strpath;// Server.MapPath(strpath);
            ReportDataSource rds = new ReportDataSource("dspowoContent", dsPOWOContent.Tables[0]);
            ReportDataSource rds_2 = new ReportDataSource("dsowoDetails", dsPOWOContent.Tables[1]);
            ReportDataSource rds_3 = new ReportDataSource("dsMilestone", dsPOWOContent.Tables[2]);
            ReportDataSource rds_4 = new ReportDataSource("dsPOWOAmountinWords", dsPOWOContent.Tables[3]);

            ReportDataSource rds_5 = new ReportDataSource("dsPOPreparedBy", dsPOWOContent.Tables[4]);
            ReportDataSource rds_6 = new ReportDataSource("dsPOCheckedBy", dsPOWOContent.Tables[5]);
            ReportDataSource rds_7 = new ReportDataSource("dsPOApprovedBY_HOD", dsPOWOContent.Tables[6]);
            ReportDataSource rds_8 = new ReportDataSource("dsPOApprovedBY_COO", dsPOWOContent.Tables[7]);
            ReportDataSource rds_9 = new ReportDataSource("dsPOApprovedBY_CEO", dsPOWOContent.Tables[8]);

            //if (Convert.ToString(dsPOWOContent.Tables[1].Rows[0]["Approver_Count"]).Trim() == "2")
            //{
            // ReportDataSource rds_6 = new ReportDataSource("dsPOCheckedBy", dsPOWOContent.Tables[5]);
            //  ReportDataSource rds_7 = new ReportDataSource("dsPOApprovedBY_HOD_COO", dsPOWOContent.Tables[6]);

            //  ReportViewer2.DataSources.Add(rds_7);
            //}


            ReportViewer2.DataSources.Clear();
            ReportViewer2.DataSources.Add(rds);
            ReportViewer2.DataSources.Add(rds_2);
            ReportViewer2.DataSources.Add(rds_3);
            ReportViewer2.DataSources.Add(rds_4);

            ReportViewer2.DataSources.Add(rds_5);
            ReportViewer2.DataSources.Add(rds_6);
            ReportViewer2.DataSources.Add(rds_7);
            ReportViewer2.DataSources.Add(rds_8);
            ReportViewer2.DataSources.Add(rds_9);
            ReportViewer2.Refresh();

            Warning[] warnings;
            string[] streamIds;
            string contentType;
            string encoding;
            string extension;

            //Export the RDLC Report to Byte Array.
            byte[] bytes = ReportViewer2.Render("pdf", null, out contentType, out encoding, out extension, out streamIds, out warnings);

            //Download the RDLC Report in Word, Excel, PDF and Image formats.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=POWO_" + PowoNumber + "." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();
        }
    }
}