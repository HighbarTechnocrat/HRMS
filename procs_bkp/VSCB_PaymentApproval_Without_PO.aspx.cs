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

public partial class procs_VSCB_PaymentApproval_Without_PO : System.Web.UI.Page
{
	#region CreativeMethods
	SqlConnection source;
	public SqlDataAdapter sqladp;

	public string loc = "", approveremailaddress = "", Approvers_code = "";
	public int did = 0, apprid;
	public DataTable dtEmp, DTPoWoNumber, DTInsertPayment, dtApproverEmailIds, dtPaymentStatus, dtextraApp;
	public string filename = "", nxtapprcode = "";
	public string nxtapprname = "";
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
					txtAmountPaidWithTax.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					hdnLoginUserName.Value = Convert.ToString(Session["emp_loginName"]).Trim();
					hdnLoginEmpEmail.Value = Convert.ToString(Session["LoginEmpmail"]).Trim();
					FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VendorPaymentDocpath"]).Trim());
                    FilePathInvoice.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBInvoicefiles"]).Trim());
                    // trvldeatils_delete_btn.Visible = false;
                    //GetInvoiceNumber();
                    if (Request.QueryString.Count > 0)
					{
						hdnPayment_ID.Value = Convert.ToString(Request.QueryString[0]).Trim();
						hdnAPPType.Value = Convert.ToString(Request.QueryString[1]).Trim();
						lblheading.Text = "Payment Request View";
						if (hdnAPPType.Value == "APP")
						{
                            hdnPageType.Value = "APP";
                            CheckPayment_ApprovalStatus_Submit();
							GetPayment_CurrentApprID();
							CheckAccountApprovalEmp();
							btnRecBack.Visible = true;
						}
						PaymentDetailsWithOutPO();
						if (hdnAPPType.Value == "View")
						{
                            hdnPageType.Value = "View";
                            localtrvl_btnSave.Visible = false;
							trvldeatils_btnSave.Visible = false;
							accmo_delete_btn.Visible = false;
							btnViewBack.Visible = true;
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
				}
			}
			else
			{
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
	private void PaymentDetailsWithOutPO()
	{
		try
		{
			DataSet DTPoWoNumber = new DataSet();
			DataTable DTstatus = new DataTable();
			int Payment_ID = 0, POID = 0;
			Payment_ID = Convert.ToString(hdnPayment_ID.Value).Trim() != "" ? Convert.ToInt32(hdnPayment_ID.Value) : 0;
			//POID = Convert.ToString(hdnPOID.Value).Trim() != "" ? Convert.ToInt32(hdnPOID.Value) : 0;
			DTPoWoNumber = spm.GetVSCB_MyPaymentDetails("MyPaymentRequestDeatilsWithOutPO", Convert.ToString(hdnEmpCode.Value), POID, Payment_ID);	
			if (DTPoWoNumber.Tables[0].Rows.Count > 0)
			{
				GrdInvoiceDetails.DataSource = DTPoWoNumber.Tables[0];
				GrdInvoiceDetails.DataBind();
				lstInvoiceNo.Enabled = false;
				lstInvoiceNo.Text = DTPoWoNumber.Tables[0].Rows[0]["InvoiceNo"].ToString();
				txtVendorName.Text = DTPoWoNumber.Tables[0].Rows[0]["Name"].ToString();
				txtDepartment.Text = DTPoWoNumber.Tables[0].Rows[0]["Department"].ToString();
				txtCostCentor.Text = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
                txtTallyCode_display.Text = DTPoWoNumber.Tables[0].Rows[0]["Tallycode"].ToString();
                hdnCostCentre.Value = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
                hdnDept_Name.Value = DTPoWoNumber.Tables[0].Rows[0]["Department"].ToString();
                lnkfile_Invoice.Text = DTPoWoNumber.Tables[0].Rows[0]["Invoice_File"].ToString();

                txtInvoiceType.Text = DTPoWoNumber.Tables[0].Rows[0]["InvoiceTypeDisplay"].ToString();
                hdnInvoiceType.Value = DTPoWoNumber.Tables[0].Rows[0]["POType"].ToString();
                hdnInvoiceTypeId.Value = DTPoWoNumber.Tables[0].Rows[0]["PoTypeId"].ToString();

            }

			if (DTPoWoNumber.Tables[1].Rows.Count > 0)
			{
				DivCreateInvoice.Visible = true;
				txtAmountPaidWithTax.Enabled = false;
				hdnInvoiceID.Value = DTPoWoNumber.Tables[1].Rows[0]["InvoiceID"].ToString();
				txtInvoiceNo.Text = DTPoWoNumber.Tables[1].Rows[0]["InvoiceNo"].ToString();
				txtInvoiceAmount.Text = DTPoWoNumber.Tables[1].Rows[0]["AmtWithTax"].ToString();  //AmtWithTax Payable_Amt_With_Tax
				txtInvoiceBalAmt.Text = DTPoWoNumber.Tables[1].Rows[0]["BalanceAmt"].ToString();
				//txtGSTINNO.Text = DTCreateInvoice.Rows[0]["PaymentStatusID"].ToString();
				txtPaymentRequestNo.Text = DTPoWoNumber.Tables[1].Rows[0]["PaymentReqNo"].ToString();
				txtRequestDate.Text = DTPoWoNumber.Tables[1].Rows[0]["PaymentReqDate"].ToString();
				txtAmountPaidWithTax.Text = DTPoWoNumber.Tables[1].Rows[0]["TobePaidAmtWithtax"].ToString();
				hdnInvoicePayableAmt.Value = DTPoWoNumber.Tables[1].Rows[0]["Payable_Amt_With_Tax"].ToString();
				hdnEmpCodePrve.Value = DTPoWoNumber.Tables[1].Rows[0]["Emp_Code"].ToString();

                txtInvoiceTDSAmt.Text = DTPoWoNumber.Tables[1].Rows[0]["DirectTax_Amount"].ToString();
                txtInvoicePaidAmount.Text = DTPoWoNumber.Tables[1].Rows[0]["AccountPaidAmt"].ToString();
                txtPaymentRequestAmount.Text = DTPoWoNumber.Tables[1].Rows[0]["PaymentReqstForAmt"].ToString();


                //hdnpaymentAmt.Value=
                hflStatusID.Value = DTPoWoNumber.Tables[1].Rows[0]["Status_id"].ToString();
				if (Convert.ToInt32(DTPoWoNumber.Tables[1].Rows[0]["Status_id"]) == 2)
				{
					Account1.Visible = true;
					Account2.Visible = true;
					Account3.Visible = true;
					txtAccountPaidAmt.Text = DTPoWoNumber.Tables[1].Rows[0]["Amt_paid_Account"].ToString();
					txtAccountAmtBal.Text = DTPoWoNumber.Tables[1].Rows[0]["AccountBalAmt"].ToString();
					txtApprovalRemark.Text = DTPoWoNumber.Tables[1].Rows[0]["Remarks"].ToString();
					txtAccountPaidAmt.Enabled = false;
					txtAccountAmtBal.Enabled = false;
					txtApprovalRemark.Enabled = false;
				}
				lblheading.Text = "Payment Request Approval - " + txtPaymentRequestNo.Text + "";//, Payment Status - " + DTPoWoNumber.Tables[1].Rows[0]["PyamentStatus"].ToString();			
			}
			if (hdnAPPType.Value == "View")
			{
				lblheading.Text = "Payment Request View - " + txtPaymentRequestNo.Text +  " , Payment Status - " + DTPoWoNumber.Tables[1].Rows[0]["PyamentStatus"].ToString();			
			}
			if (DTPoWoNumber.Tables[2].Rows.Count > 0)
			{
				GrdFileUpload.DataSource = DTPoWoNumber.Tables[2];
				GrdFileUpload.DataBind();
                spnsupportingFiles.Visible = true;
                liPaymentSupportingFiles_1.Visible = true;
                liPaymentSupportingFiles_2.Visible = true;
                liPaymentSupportingFiles_3.Visible = true;

            }
			if (DTPoWoNumber.Tables[3].Rows.Count > 0)
			{
				SPPayHist.Visible = true;
				GrdInvoiceHistDetails.DataSource = DTPoWoNumber.Tables[3];
				GrdInvoiceHistDetails.DataBind();
			}

            if (DTPoWoNumber.Tables[4].Rows.Count > 0)
            {
                gvuploadedFiles.DataSource = DTPoWoNumber.Tables[4]; 
                gvuploadedFiles.DataBind();
                spnSupportinFiles.Visible = true;
                liInvoiceSupportingFiles_1.Visible = true;
            }
           

            getApproverlist(hdnEmpCode.Value, Payment_ID, hdnDept_Name.Value, hdnCostCentre.Value);
			DataTable dsapproverNxt = new DataTable();
			dsapproverNxt = spm.GetNext_Payment_Request_ApproverDetails("Get_NextApproverDetails_mail", hdnEmpCode.Value, Convert.ToInt32(Payment_ID), hdnDept_Name.Value, hdnCostCentre.Value,Convert.ToString(hdnInvoiceType.Value).Trim());
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
			hdnNextappName.Value = "Account Department";
			hdnApproverType.Value = "NA";
		}
	}
	//public void GetInvoiceNumber()
	//{
	//	DTPoWoNumber = spm.dtPOWoCreate("InvoicePaymentRequest", Convert.ToString(hdnEmpCode.Value));
	//	lstInvoiceNo.DataSource = DTPoWoNumber;
	//	lstInvoiceNo.DataTextField = "InvoiceNo";
	//	lstInvoiceNo.DataValueField = "InvoiceID";
	//	lstInvoiceNo.DataBind();
	//	lstInvoiceNo.Items.Insert(0, new ListItem("Select Invoice NO", "0"));
	//}
	private void PaymentBalanceAmt()
	{
		try
		{
			DataTable DTCreateInvoice = new DataTable();
			decimal InvoiceBalAmt = 0; int InvoiceID = 0;
			InvoiceID = Convert.ToString(hdnInvoiceID.Value).Trim() != "" ? Convert.ToInt32(hdnInvoiceID.Value) : 0;
			DTCreateInvoice = spm.dtShowInvoiceDetailsForPayment("POWO_Invoice_PaymentAmt", Convert.ToString(hdnEmpCode.Value), InvoiceID, 0);
			if (DTCreateInvoice.Rows.Count > 0)
			{
				InvoiceBalAmt = Convert.ToString(DTCreateInvoice.Rows[0]["TobePaidAmtWithtax"]).Trim() != "" ? Convert.ToDecimal(DTCreateInvoice.Rows[0]["TobePaidAmtWithtax"]) : 0;
				if (InvoiceBalAmt > 0)
				{
					InvoiceBalAmt = InvoiceBalAmt - Convert.ToDecimal(txtAmountPaidWithTax.Text);
					hdnTobePaidAmt.Value = InvoiceBalAmt.ToString();
				}
			}
		}
		catch (Exception)
		{

			throw;
		}
	}
	protected void localtrvl_btnSave_Click(object sender, EventArgs e)
	{
		try
		{
			lblmessage.Text = ""; string EmpCode = "", strtoDate = "";
			int Payment_ID = 0, Payment_Status_ID = 0, InvoiceID = 0, MstoneID = 0;
			DateTime PaymentDate; string[] strdate;
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}
			if (hdnExtraAPP.Value.ToString().Trim() == "Account")
			{
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
			MstoneID =  0;
			if (hdnExtraAPP.Value.ToString().Trim() == "Account")
			{
				decimal BalAmt = 0;
				strdate = Convert.ToString(txtRequestDate.Text).Trim().Split('-');
				strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
				PaymentDate = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
				if (Convert.ToString(txtAmountPaidWithTax.Text).Trim() != "" && Convert.ToString(txtAccountPaidAmt.Text).Trim() != "")
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
			}
			String strPatmentURL = "";
			strPatmentURL = (Convert.ToString(ConfigurationManager.AppSettings["Link_PayRequestAPPwithOut"]).Trim() + "?Payment_ID=" + Payment_ID + "&Type=APP");
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
			string strapproverlist = "", ccMailIDs = "", PowoNo = "";
			var Tbody = "";
			GetEmployeeCode(hdnEmpCodePrve.Value);
			PowoNo = txtInvoiceNo.Text;			
			string strsubject = "Request for - Payment Approval  - “" + txtPaymentRequestNo.Text + "”  For “" + PowoNo + "”";
			strapproverlist = GetPaymnetApprove_RejectList(hdnEmpCode.Value, Payment_ID, hdnDept_Name.Value, hdnCostCentre.Value);
			ccMailIDs = GetPayment_Approval_CCMail_Deatils(Payment_ID, hdnEmpCode.Value);		
			GetInvoiceTDSAmt();
			string CostCenter = txtCostCentor.Text;
			if (hdnExtraAPP.Value.ToString().Trim() != "Account")
			{
				var actionByName = Session["emp_loginName"].ToString();
				Tbody = "<b>" + hdnEmpCodePrveName.Value + "</b> has created a payment request as per the details below. Request your approval please.";
			   SendMailNextApproval(hdnNextappName.Value, hdnApproverEmail.Value, strsubject, Tbody, ccMailIDs, strapproverlist, strPatmentURL);
			}
			else
			{
				strPatmentURL = "";
				Tbody = "This is to inform you that Account has Approved below payment request.";
				SendMailCreatePayment(hdnEmpCodePrveName.Value, hdnEmpCodePrveEmailID.Value, strsubject, Tbody, ccMailIDs, strapproverlist, strPatmentURL);
			}
			Response.Redirect("~/procs/VSCB_InboxPaymentRequest.aspx", false);
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}
	private void SendMailNextApproval(string ApprovalName, string ApprovalEmail, string strsubject, string TBody, string ccMailIDs, string strapproverlist, string strPatmentURL)
	{
		StringBuilder strbuild = new StringBuilder();
		strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;width:100%;table-layout:fixed;'><tr><td>   ");
		strbuild.Append("</td></tr>");
		strbuild.Append("<tr><td style='height:20px;'></td></tr>");
		strbuild.Append("<tr><td>Dear " + ApprovalName + ",</td></tr>");
		strbuild.Append("<tr><td style='height:10px;'></td></tr>");
		strbuild.Append("<tr><td> " + TBody + "</td></tr>");
		strbuild.Append("<tr><td  style='height:10px;'></td></tr>");
		strbuild.Append("<tr><td>");
		strbuild.Append("<table style='width:80% !important;color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;table-layout:fixed;'>");
		strbuild.Append("<tr><td width='30%'>Payment Request No  :</td><td width='70%'>" + txtPaymentRequestNo.Text + "</td></tr>");
		strbuild.Append("<tr><td width='30%'>Request Date  :</td><td width='70%'>" + txtRequestDate.Text + "</td></tr>");
		strbuild.Append("<tr><td width='30%'>Amount to be paid (with Tax) :</td><td width='70%'>" + txtAmountPaidWithTax.Text + "</td></tr>");
		strbuild.Append("<tr><td style='height:20px;'></td></tr>");
		strbuild.Append("<tr><td width='30%'>Invoice No  :</td><td width='70%'>" + txtInvoiceNo.Text + "</td></tr>");
		strbuild.Append("<tr><td width='30%'>Invoice Amount  :</td><td width='70%'>" + txtInvoiceAmount.Text+ "</td></tr>");
		if (hdnDirectTax_Type.Value != "")
		{
			strbuild.Append("<tr><td width='30%'>Direct Tax  :</td><td width='70%'>" + hdnDirectTax_Type.Value + "</td></tr>");
			strbuild.Append("<tr><td width='30%'>DirectTax(%)  :</td><td width='70%'>" + hdnDirectTax_Percentage.Value + "</td></tr>");
			strbuild.Append("<tr><td width='30%'>Direct Tax Amount  :</td><td width='70%'>" + hdnDirectTax_Amount.Value + "</td></tr>");
			strbuild.Append("<tr><td width='30%'>Payable Amount (with Tax)  :</td><td width='70%'>" + hdnPayable_Amt_Invoice.Value + "</td></tr>");
		}
		strbuild.Append("<tr><td width='30%'>Balance Amount  :</td><td width='70%'>" + txtInvoiceBalAmt.Text + "</td></tr>");
		//strbuild.Append("<tr><td width='30%'>Department   :</td><td width='70%'>" + txtDepartment.Text + "</td></tr>");
		strbuild.Append("<tr><td width='30%'>Cost Center   :</td><td width='70%'>" + txtTallyCode_display.Text + "</td></tr>");
		strbuild.Append("<tr><td style='height:10px;'></td></tr>");
		strbuild.Append("</table>");
		strbuild.Append(strapproverlist);
		strbuild.Append("</td></tr>");
		strbuild.Append("<tr><td style='height:20px'></td></tr>");
		if (strPatmentURL != "")
		{
			strbuild.Append("<tr><td style='height:30px'><a href='" + strPatmentURL + "' target='_blank'>Please click here to take action on payment request</a></td></tr>");
		}
		strbuild.Append("</table>");
		spm.sendMail_VSCB(ApprovalEmail, strsubject, Convert.ToString(strbuild).Trim(), "", ccMailIDs);

	}
	private void SendMailCreatePayment(string ApprovalName, string ApprovalEmail, string strsubject, string TBody, string ccMailIDs, string strapproverlist, string strPatmentURL)
	{

        DataSet dsFormatAmount = spm.getFormated_Amount("get_formated_currncy_Amount", Convert.ToString("EN-IN"), Convert.ToDecimal(txtAccountPaidAmt.Text), Convert.ToDecimal(txtAccountAmtBal.Text),0, 0, 0);
        string sformatPaymentpaidAmt = Convert.ToString(dsFormatAmount.Tables[0].Rows[0]["Amount_A"]);
        string sformatPaymentBalAmt = Convert.ToString(dsFormatAmount.Tables[0].Rows[0]["Amount_B"]); 

        StringBuilder strbuild = new StringBuilder();
		strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;width:100%;table-layout:fixed;'><tr><td>   ");
		strbuild.Append("</td></tr>");
		strbuild.Append("<tr><td style='height:20px;'></td></tr>");
		strbuild.Append("<tr><td>Dear " + ApprovalName + ",</td></tr>");
		strbuild.Append("<tr><td style='height:10px;'></td></tr>");
		strbuild.Append("<tr><td> " + TBody + "</td></tr>");
		strbuild.Append("<tr><td  style='height:10px;'></td></tr>");
		strbuild.Append("<tr><td>");
		strbuild.Append("<table style='width:80% !important;color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;table-layout:fixed;'>");
		strbuild.Append("<tr><td width='30%'>Payment Request No  :</td><td width='70%'>" + txtPaymentRequestNo.Text + "</td></tr>");
		strbuild.Append("<tr><td width='30%'>Request Date  :</td><td width='70%'>" + txtRequestDate.Text + "</td></tr>");
		strbuild.Append("<tr><td width='30%'>Amount to be paid (with Tax) :</td><td width='70%'>" + txtAmountPaidWithTax.Text + "</td></tr>");
        //strbuild.Append("<tr><td width='30%'>Amount paid by Accounts :</td><td width='70%'>" + txtAccountPaidAmt.Text + "</td></tr>");
        //strbuild.Append("<tr><td width='30%'>Balance Amount :</td><td width='70%'>" + txtAccountAmtBal.Text + "</td></tr>");

        strbuild.Append("<tr><td width='30%'>Amount paid by Accounts :</td><td width='70%'>" + sformatPaymentpaidAmt + "</td></tr>");
        strbuild.Append("<tr><td width='30%'>Balance Amount :</td><td width='70%'>" + sformatPaymentBalAmt + "</td></tr>");

        strbuild.Append("<tr><td style='height:20px;'></td></tr>");

		strbuild.Append("<tr><td width='30%'>Invoice No  :</td><td width='70%'>" + txtInvoiceNo.Text + "</td></tr>");
		strbuild.Append("<tr><td width='30%'>Invoice Amount  :</td><td width='70%'>" + txtInvoiceAmount.Text + "</td></tr>");
		if (hdnDirectTax_Type.Value != "")
		{
			strbuild.Append("<tr><td width='30%'>Direct Tax  :</td><td width='70%'>" + hdnDirectTax_Type.Value + "</td></tr>");
			strbuild.Append("<tr><td width='30%'>DirectTax(%)  :</td><td width='70%'>" + hdnDirectTax_Percentage.Value + "</td></tr>");
			strbuild.Append("<tr><td width='30%'>Direct Tax Amount  :</td><td width='70%'>" + hdnDirectTax_Amount.Value + "</td></tr>");
			strbuild.Append("<tr><td width='30%'>Payable Amount (with Tax)  :</td><td width='70%'>" + hdnPayable_Amt_Invoice.Value + "</td></tr>");
		}
		strbuild.Append("<tr><td width='30%'>Balance Amount  :</td><td width='70%'>" + txtInvoiceBalAmt.Text + "</td></tr>");
		//strbuild.Append("<tr><td width='30%'>Department   :</td><td width='70%'>" + txtDepartment.Text + "</td></tr>");
		strbuild.Append("<tr><td width='30%'>Cost Center   :</td><td width='70%'>" + txtTallyCode_display.Text + "</td></tr>");
		strbuild.Append("<tr><td style='height:10px;'></td></tr>");
		strbuild.Append("</table>");
		strbuild.Append(strapproverlist);
		strbuild.Append("</td></tr>");
		strbuild.Append("<tr><td style='height:20px'></td></tr>");
		if (strPatmentURL != "")
		{
			strbuild.Append("<tr><td style='height:30px'><a href='" + strPatmentURL + "' target='_blank'>Please click here to take action on payment request</a></td></tr>");
		}
		strbuild.Append("</table>");
		spm.sendMail_VSCB(ApprovalEmail, strsubject, Convert.ToString(strbuild).Trim(), "", ccMailIDs);

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
			if (txtInvoiceNo.Text != "")
			{
				PowoNo = txtInvoiceNo.Text;
			}
			
			string strapproverlist = "", strPatmentURL = "", ccMailIDs = "";
			spm.VSCB_Reject_Payment_Request("Payment_Reject", Convert.ToInt32(hdnPayment_ID.Value), hdnEmpCode.Value, Convert.ToInt32(hdnCurrentID.Value), 4, txtApprovalRemark.Text);
			GetEmployeeCode(hdnEmpCodePrve.Value);
			strapproverlist = GetPaymnetApprove_RejectList(hdnEmpCode.Value, Convert.ToInt32(hdnPayment_ID.Value), hdnDept_Name.Value, hdnCostCentre.Value);
			ccMailIDs = GetPayment_Approval_CCMail_Deatils(Convert.ToInt32(hdnPayment_ID.Value), hdnEmpCode.Value);
			string strsubject = "Payment request - “" + txtPaymentRequestNo.Text + "” Rejected against “" + PowoNo + "”";
			var actionByName = Session["emp_loginName"].ToString();
			GetInvoiceTDSAmt();
			var Tbody = "This is to inform you that " + hdnLoginUserName.Value + " has<b> Rejected</b> Payment request  as per the details below.";
			SendMailNextApproval(hdnEmpCodePrveName.Value, hdnEmpCodePrveEmailID.Value, strsubject, Tbody, ccMailIDs, strapproverlist, strPatmentURL);
			Response.Redirect("~/procs/VSCB_InboxPaymentRequest.aspx");

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
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
	public string GetPayment_Approval_CCMail_Deatils(int PaymentID, string Assgineto)
	{
		try
		{
			String sbapp = "";
			DataTable dsTrDetails = new DataTable();
			dsTrDetails = spm.Get_Payment_Request_Approver_CCmail("Get_Intermidates_Payment_list", Assgineto, PaymentID, "", "");
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
	
	
	private void InvoiceDetails(int InvoiceID)
	{
		try
		{
			DataSet DTPoWoNumber = new DataSet();
			DTPoWoNumber = spm.Vendor_InvoiceDetails("InvoiceRequestDeatils", Convert.ToString(hdnEmpCode.Value), InvoiceID);
			if (DTPoWoNumber.Tables[0].Rows.Count > 0)
			{
				txtVendorName.Text = DTPoWoNumber.Tables[0].Rows[0]["Name"].ToString();
				txtDepartment.Text = DTPoWoNumber.Tables[0].Rows[0]["Department"].ToString();
				txtCostCentor.Text = DTPoWoNumber.Tables[0].Rows[0]["Project_Dept_Name"].ToString();
				//txtProjectName.Text = DTPoWoNumber.Tables[0].Rows[0]["Location_name"].ToString();
				//hdnCostCentre.Value = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
				hdnTallyCode.Value = DTPoWoNumber.Tables[0].Rows[0]["Project_Dept_Name"].ToString();
				//hdnDept_Name.Value = DTPoWoNumber.Tables[0].Rows[0]["Dept_Name"].ToString();

				GrdInvoiceDetails.DataSource = DTPoWoNumber.Tables[0];
				GrdInvoiceDetails.DataBind();
			}
			if (DTPoWoNumber.Tables[1].Rows.Count > 0)
			{
				SPPayHist.Visible = true;
				GrdInvoiceHistDetails.DataSource = DTPoWoNumber.Tables[1];
				GrdInvoiceHistDetails.DataBind();
			}
			getApproverlist(hdnEmpCode.Value, 0, hdnDept_Name.Value, hdnTallyCode.Value);
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
		dtAppRej = spm.Get_Payment_Request_ApproverList(EmpCode, Payment_ID, DeptName, TallyCode,Convert.ToString(hdnInvoiceType.Value).Trim());
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

	protected void lnkView_Click(object sender, EventArgs e)
	{
		try
		{
			LinkButton btn = (LinkButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			hdnInvoiceID.Value = Convert.ToString(GrdInvoiceDetails.DataKeys[row.RowIndex].Values[0]).Trim();		
		}
		catch (Exception)
		{

			throw;
		}
	}
	
	private void getApproverlist(string EmpCode, int Payment_ID, string DeptnName, string TallyCode)
	{
		DataTable dtapprover = new DataTable();
		dtapprover = spm.Get_Payment_Request_ApproverList(EmpCode, Payment_ID, DeptnName, TallyCode,Convert.ToString(hdnInvoiceType.Value).Trim());
		DgvApprover.DataSource = null;
		DgvApprover.DataBind();
		if (dtapprover.Rows.Count > 0)
		{
			DgvApprover.DataSource = dtapprover;
			DgvApprover.DataBind();
		}
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
				hdnInvoiceTaxAmount.Value = dtCostCenter.Rows[0]["AmtWithTax"].ToString();
				hdnInvoiceBalAmt.Value = dtCostCenter.Rows[0]["BalanceAmt"].ToString();
			}
		}
		catch (Exception)
		{

			throw;
		}
	}
	protected void lnkViewhist_Click(object sender, ImageClickEventArgs e)
	{
		try
		{
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			int Payment_ID = 0;
			Payment_ID = Convert.ToInt32(GrdInvoiceHistDetails.DataKeys[row.RowIndex].Values[0]);
			PaymentHistoryDetails(Payment_ID);
		}
		catch (Exception)
		{
			throw;
		}
	}
	private void PaymentHistoryDetails(int Payment_ID)
	{
		try
		{
			DataSet DTPoWoNumber = new DataSet();
			int POID = 0;
			SPCreate.InnerText = "Payment Request View :";
			DivCreateInvoice.Visible = true;
			accmo_delete_btn.Visible = true;
			localtrvl_btnSave.Visible = false;
			trvldeatils_btnSave.Visible = false;
			txtAmountPaidWithTax.Enabled = false;
			//Payment_ID = Convert.ToString(hdnPayment_ID.Value).Trim() != "" ? Convert.ToInt32(hdnPayment_ID.Value) : 0;
			POID = Convert.ToString(hdnPOID.Value).Trim() != "" ? Convert.ToInt32(hdnPOID.Value) : 0;
			DTPoWoNumber = spm.GetVSCB_MyPaymentDetails("POWOPaymentRequestHistory", Convert.ToString(hdnEmpCode.Value), POID, Payment_ID);
			if (DTPoWoNumber.Tables[0].Rows.Count > 0)
			{
				//DivCreateInvoice.Visible = true;
				txtInvoiceNo.Text = DTPoWoNumber.Tables[0].Rows[0]["InvoiceNo"].ToString();
				txtInvoiceAmount.Text = DTPoWoNumber.Tables[0].Rows[0]["AmtWithTax"].ToString();
				txtInvoiceBalAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["BalanceAmt"].ToString();
				txtPaymentRequestNo.Text = DTPoWoNumber.Tables[0].Rows[0]["PaymentReqNo"].ToString();
				txtRequestDate.Text = DTPoWoNumber.Tables[0].Rows[0]["PaymentReqDate"].ToString();
				txtAmountPaidWithTax.Text = DTPoWoNumber.Tables[0].Rows[0]["TobePaidAmtWithtax"].ToString();
				txtAccountPaidAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["Amt_paid_Account"].ToString();
				txtAccountAmtBal.Text = DTPoWoNumber.Tables[0].Rows[0]["AccountBalAmt"].ToString();
				//hflStatusID.Value = DTPoWoNumber.Tables[0].Rows[0]["Status_id"].ToString();
				if (Convert.ToInt32(DTPoWoNumber.Tables[0].Rows[0]["Status_id"]) == 2)
				{
					Account1.Visible = true;
					Account2.Visible = true;
					Account3.Visible = true;
					//txtAccountPaidAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["Amt_paid_Account"].ToString();
					//txtAccountAmtBal.Text = DTPoWoNumber.Tables[0].Rows[0]["AccountBalAmt"].ToString();
					//txtAccountPaidAmt.Enabled = false;
					//txtAccountAmtBal.Enabled = false;
				}
				else
				{
					Account1.Visible = false;
					Account2.Visible = false;
					Account3.Visible = false;
				}
			}
			GrdFileUpload.DataSource = null;
			GrdFileUpload.DataBind();
			if (DTPoWoNumber.Tables[1].Rows.Count > 0)
			{
				GrdFileUpload.DataSource = DTPoWoNumber.Tables[1];
				GrdFileUpload.DataBind();
                spnsupportingFiles.Visible = true;
			}
		}
		catch (Exception)
		{

			throw;
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
			if (txtInvoiceNo.Text != "")
			{
				PowoNo = txtInvoiceNo.Text;
			}
			string strapproverlist = "", strPatmentURL = "", ccMailIDs="";
			spm.VSCB_Reject_Payment_Request("Payment_Correction", Convert.ToInt32(hdnPayment_ID.Value), hdnEmpCode.Value, Convert.ToInt32(hdnCurrentID.Value), 5, txtApprovalRemark.Text);
			GetEmployeeCode(hdnEmpCodePrve.Value);
			strapproverlist = GetPaymnetApprove_RejectList(hdnEmpCode.Value, Convert.ToInt32(hdnPayment_ID.Value), hdnDept_Name.Value, hdnCostCentre.Value);
			ccMailIDs = GetPayment_Approval_CCMail_Deatils(Convert.ToInt32(hdnPayment_ID.Value), hdnEmpCode.Value);
			string strsubject = "Payment request your Sent Back Correction “" + txtPaymentRequestNo.Text + "” For “" + PowoNo + "”";
			var actionByName = Session["emp_loginName"].ToString();
			GetInvoiceTDSAmt();
			var Tbody = "<b> " + hdnLoginUserName.Value + "</b> has Sent back the payment request for <b> Correction.</b> Please correct the same as instructed and resend for approval.";
			SendMailNextApproval(hdnEmpCodePrveName.Value, hdnEmpCodePrveEmailID.Value, strsubject, Tbody, ccMailIDs, strapproverlist, strPatmentURL);
			Response.Redirect("~/procs/VSCB_InboxPaymentRequest.aspx");
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
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


    protected void trvldeatils_cancel_btn_Click(object sender, EventArgs e)
    {
        //PostBackUrl = "~/procs/VSCB_InboxPaymentRequest.aspx"

        if (Convert.ToString(hdnPageType.Value).Trim() == "View")
            Response.Redirect("VSCB_InboxPaymentRequestView.aspx");
        else
            Response.Redirect("VSCB_InboxPaymentRequest.aspx");

    }
}