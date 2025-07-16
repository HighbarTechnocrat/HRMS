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

public partial class procs_VSCB_CreatePartialPay_Without_PO : System.Web.UI.Page
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
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
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
					// trvldeatils_delete_btn.Visible = false;
					GetInvoiceNumber();
					if (Request.QueryString.Count > 0)
					{
						hdnPayment_ID.Value = Convert.ToString(Request.QueryString[0]).Trim();
						hdnAPPType.Value = Convert.ToString(Request.QueryString[1]).Trim();
						lblheading.Text = "Payment Request View";
						if (hdnAPPType.Value == "APP")
						{
							CheckPayment_ApprovalStatus_Submit();							
							CheckAccountApprovalEmp();
							PaymentDetailsWithOutPO();
							btnRecBack.Visible = true;
						}
						
						if (hdnAPPType.Value == "View")
						{
							localtrvl_btnSave.Visible = false;
							trvldeatils_btnSave.Visible = false;
							accmo_delete_btn.Visible = false;
							PaymentDetailsWithOutPOView();
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
			spars[0].Value = "Check_PartailPayment_Status";
			spars[1] = new SqlParameter("@Payment_ID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdnPayment_ID.Value);
			spars[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[2].Value = Convert.ToString(hdnEmpCode.Value).Trim();
			dtPaymentStatus = spm.getMobileRemDataList(spars, "sp_VSCB_CreatePOWO_Users");
			if (dtPaymentStatus.Rows.Count == 0)
			{
				Response.Redirect("~/procs/VSCB_InboxPartialPaymentRequest.aspx");
			}
			if (dtPaymentStatus.Rows.Count > 0)
			{
				if (Convert.ToString(dtPaymentStatus.Rows[0]["pvappstatus"]) != "True")
				{
					Response.Redirect("~/procs/VSCB_InboxPartialPaymentRequest.aspx");
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
					localtrvl_btnSave.Text = "Create & Verify Payment Request";
					localtrvl_btnSave.ToolTip = "Create & Verify Payment Request";
					Account1.Visible = true;
					Account2.Visible = true;
					Account3.Visible = false;
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

				DTPoWoNumber = spm.GetVSCB_MyPaymentDetails("PartialPaymentRequestDeatilsWithOutPO", Convert.ToString(hdnEmpCode.Value), POID, Payment_ID);
			if (DTPoWoNumber.Tables[0].Rows.Count > 0)
			{
				GrdInvoiceDetails.DataSource = DTPoWoNumber.Tables[0];
				GrdInvoiceDetails.DataBind();
				lstInvoiceNo.Enabled = false;
				lstInvoiceNo.SelectedValue = DTPoWoNumber.Tables[0].Rows[0]["InvoiceID"].ToString();
				txtVendorName.Text = DTPoWoNumber.Tables[0].Rows[0]["Name"].ToString();
				txtDepartment.Text = DTPoWoNumber.Tables[0].Rows[0]["Department"].ToString();
				txtCostCentor.Text = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
                txtTallyCode_display.Text = DTPoWoNumber.Tables[0].Rows[0]["Tallycode"].ToString();
                hdnTallyCode.Value = DTPoWoNumber.Tables[0].Rows[0]["Project_Dept_Name"].ToString();
			}
			
			if (DTPoWoNumber.Tables[1].Rows.Count > 0)
			{
				GrdPartialPayment.DataSource = DTPoWoNumber.Tables[1];
				GrdPartialPayment.DataBind();	
				lblheading.Text = "Partial Payment Request - " + DTPoWoNumber.Tables[1].Rows[0]["PaymentReqNo"].ToString() + "";//, Payment Status - " + DTPoWoNumber.Tables[1].Rows[0]["PyamentStatus"].ToString();			
			}
			if (hdnAPPType.Value == "View")
			{
				lblheading.Text = "Partial Payment Request View - " + txtPaymentRequestNo.Text + " , Payment Status - " + DTPoWoNumber.Tables[1].Rows[0]["PyamentStatus"].ToString();
			}
			if (DTPoWoNumber.Tables[2].Rows.Count > 0)
			{
				GrdFileUpload.DataSource = DTPoWoNumber.Tables[2];
				GrdFileUpload.DataBind();
			}
			if (DTPoWoNumber.Tables[3].Rows.Count > 0)
			{
				SPPayHist.Visible = true;
				GrdInvoiceHistDetails.DataSource = DTPoWoNumber.Tables[3];
				GrdInvoiceHistDetails.DataBind();
			}
			//getApproverlist(hdnEmpCode.Value, Payment_ID, hdnDept_Name.Value, hdnTallyCode.Value);
			
		}
		catch (Exception)
		{

			throw;
		}
	}
	private void PaymentDetailsWithOutPOView()
	{
		try
		{
			DataSet DTPoWoNumber = new DataSet();
			DataTable DTstatus = new DataTable();
			int Payment_ID = 0, POID = 0;
			Payment_ID = Convert.ToString(hdnPayment_ID.Value).Trim() != "" ? Convert.ToInt32(hdnPayment_ID.Value) : 0;
			//POID = Convert.ToString(hdnPOID.Value).Trim() != "" ? Convert.ToInt32(hdnPOID.Value) : 0;

			DTPoWoNumber = spm.GetVSCB_MyPaymentDetails("MyPartialPaymentRequestDeatilsWithOutPO", Convert.ToString(hdnEmpCode.Value), POID, Payment_ID);
			if (DTPoWoNumber.Tables[0].Rows.Count > 0)
			{
				GrdInvoiceDetails.DataSource = DTPoWoNumber.Tables[0];
				GrdInvoiceDetails.DataBind();
				lstInvoiceNo.Enabled = false;
				lstInvoiceNo.SelectedValue = DTPoWoNumber.Tables[0].Rows[0]["InvoiceID"].ToString();
				txtVendorName.Text = DTPoWoNumber.Tables[0].Rows[0]["Name"].ToString();
				txtDepartment.Text = DTPoWoNumber.Tables[0].Rows[0]["Department"].ToString();
				txtCostCentor.Text = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
				hdnTallyCode.Value = DTPoWoNumber.Tables[0].Rows[0]["Project_Dept_Name"].ToString();
			}

			if (DTPoWoNumber.Tables[1].Rows.Count > 0)
			{
				GrdPartialPayment.DataSource = DTPoWoNumber.Tables[1];
				GrdPartialPayment.DataBind();
				//this.GrdPartialPayment.Columns[0].Visible = false;
				lblheading.Text = "Partial Payment Request - " + DTPoWoNumber.Tables[1].Rows[0]["PaymentReqNo"].ToString() + "";//, Payment Status - " + DTPoWoNumber.Tables[1].Rows[0]["PyamentStatus"].ToString();			
			}

			if (DTPoWoNumber.Tables[2].Rows.Count > 0)
			{
				DivCreateInvoice.Visible = true;
				txtInvoiceNo.Text = DTPoWoNumber.Tables[2].Rows[0]["InvoiceNo"].ToString();
				//hdnInvoiceID.Value = DTCreateInvoice.Rows[0]["InvoiceID"].ToString();
				
				txtInvoiceAmount.Text = DTPoWoNumber.Tables[2].Rows[0]["AmtWithTax"].ToString();
				txtInvoiceBalAmt.Text = DTPoWoNumber.Tables[2].Rows[0]["BalanceAmt"].ToString();
				txtPaymentRequestNo.Text = DTPoWoNumber.Tables[2].Rows[0]["PaymentReqNo"].ToString();
				txtOldPaymentRequestNo.Text = DTPoWoNumber.Tables[2].Rows[0]["Ref_PaymentReqNo"].ToString();
				txtAmountPaidWithTax.Text = DTPoWoNumber.Tables[2].Rows[0]["TobePaidAmtWithtax"].ToString();
				txtAccountAmtBal.Text = DTPoWoNumber.Tables[2].Rows[0]["AccountBalAmt"].ToString();
				HDNPartialPaymnetBal.Value = DTPoWoNumber.Tables[2].Rows[0]["AccountBalAmt"].ToString();
				txtRequestDate.Text = DTPoWoNumber.Tables[2].Rows[0]["PaymentReqDate"].ToString();
				txtAccountPaidAmt.Text = DTPoWoNumber.Tables[2].Rows[0]["Amt_paid_Account"].ToString();
				txtApprovalRemark.Text = DTPoWoNumber.Tables[2].Rows[0]["Remarks"].ToString();
				txtApprovalRemark.Enabled = false;
				txtAmountPaidWithTax.Enabled = false;
				txtAccountPaidAmt.Enabled = false;
			}
			if (hdnAPPType.Value == "View")
			{
				this.GrdPartialPayment.Columns[0].Visible = false;
				lblheading.Text = "Partial Payment Request View - " + txtPaymentRequestNo.Text + " , Payment Status - " + DTPoWoNumber.Tables[1].Rows[0]["PyamentStatus"].ToString();
			}
			if (DTPoWoNumber.Tables[3].Rows.Count > 0)
			{
				GrdFileUpload.DataSource = DTPoWoNumber.Tables[3];
				GrdFileUpload.DataBind();
			}
			if (DTPoWoNumber.Tables[4].Rows.Count > 0)
			{
				SPPayHist.Visible = true;
				GrdInvoiceHistDetails.DataSource = DTPoWoNumber.Tables[4];
				GrdInvoiceHistDetails.DataBind();
			}
			//getApproverlist(hdnEmpCode.Value, Payment_ID, hdnDept_Name.Value, hdnTallyCode.Value);

		}
		catch (Exception)
		{

			throw;
		}
	}
	public void GetInvoiceNumber()
	{
		DTPoWoNumber = spm.dtPOWoCreate("InvoicePaymentRequest", Convert.ToString(hdnEmpCode.Value));
		lstInvoiceNo.DataSource = DTPoWoNumber;
		lstInvoiceNo.DataTextField = "InvoiceNo";
		lstInvoiceNo.DataValueField = "InvoiceID";
		lstInvoiceNo.DataBind();
		lstInvoiceNo.Items.Insert(0, new ListItem("Select Invoice NO", "0"));
	}
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
			DateTime PaymentDate; string[] strdate; decimal PayBalAmt = 0;
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}
			if (hdnExtraAPP.Value.ToString().Trim() == "Account")
			{
				if (Convert.ToString(txtAmountPaidWithTax.Text).Trim() == "" || Convert.ToString(txtAmountPaidWithTax.Text).Trim() == "0")
				{
					lblmessage.Text = "Please enter Amount Paid With Tax";
					return;
				}

				if (Convert.ToString(txtAccountPaidAmt.Text).Trim() == "" || Convert.ToString(txtAccountPaidAmt.Text).Trim() == "0")
				{
					lblmessage.Text = "Please enter Amount paid by Accounts";
					return;
				}
				if (Convert.ToDecimal(txtAccountPaidAmt.Text) > Convert.ToDecimal(HDNPartialPaymnetBal.Value))
				{
					lblmessage.Text = "Amount to be paid cannot exceed balance Amount! ";
					return;
				}

			}
			if (Convert.ToString(Session["Empcode"]).Trim() != "" || Session["Empcode"] != null)
			{
				EmpCode = Convert.ToString(Session["Empcode"]).ToString();
			}
			Payment_ID = Convert.ToString(hdnPayment_ID.Value).Trim() != "" ? Convert.ToInt32(hdnPayment_ID.Value) : 0;
			InvoiceID = Convert.ToString(hdnInvoiceID.Value).Trim() != "" ? Convert.ToInt32(hdnInvoiceID.Value) : 0;
			MstoneID = 0;
			if (hdnExtraAPP.Value.ToString().Trim() == "Account")
			{
				decimal BalAmt = 0;
				strdate = Convert.ToString(txtRequestDate.Text).Trim().Split('-');
				strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
				PaymentDate = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

				if (Convert.ToString(txtAmountPaidWithTax.Text).Trim() != "" && Convert.ToString(txtAccountPaidAmt.Text).Trim() != "")
				{
					BalAmt = Convert.ToDecimal(HDNPartialPaymnetBal.Value) - Convert.ToDecimal(txtAccountPaidAmt.Text);
					if (BalAmt > 0)
					{
						Payment_Status_ID = 3;
					}
					else
					{
						Payment_Status_ID = 2;
					}
				}
				PayBalAmt = Convert.ToDecimal(HDNPartialPaymnetBal.Value) - Convert.ToDecimal(txtAccountPaidAmt.Text);
				DTInsertPayment = spm.dtInsert_Payment_Request("PARTIALPAYMNETINSERT_WITHOUTPO", EmpCode, InvoiceID, MstoneID, Payment_ID, txtPaymentRequestNo.Text, Convert.ToDecimal(txtAmountPaidWithTax.Text), PaymentDate, 2, Payment_Status_ID, Convert.ToDecimal(txtAccountPaidAmt.Text), Convert.ToDecimal(PayBalAmt), txtOldPaymentRequestNo.Text.Trim(), txtApprovalRemark.Text);
			}
			String strPatmentURL = "", strapproverlist = "";
			var Tbody = "";
			GetOldPaymentRequestNo(txtOldPaymentRequestNo.Text);
			string strsubject = " Partial Payment request of “" + txtPaymentRequestNo.Text + "”  For “" + txtInvoiceNo.Text + " ”";
			if (hdnExtraAPP.Value.ToString().Trim() == "Account")
			{
				strPatmentURL = "";
				GetInvoiceTDSAmt();				
				Tbody = "This is to inform you that Account has below Partial Payment Paid.";
				//SendMailCreatePayment(hdnEmpCodePrveName.Value, hdnEmpCodePrveEmailID.Value, strsubject, Tbody,"","","");
			}
			Response.Redirect("~/procs/VSCB_InboxPartialPaymentRequest.aspx", false);
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}
	private void SendMailCreatePayment(string ApprovalName, string ApprovalEmail, string strsubject, string TBody, string ccMailIDs, string strapproverlist, string strPatmentURL)
	{
		StringBuilder strbuild = new StringBuilder();
		strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;width:100%;table-layout:fixed;'><tr><td>");
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
		strbuild.Append("<tr><td width='30%'>Old Payment Request No  :</td><td width='70%'>" + txtOldPaymentRequestNo.Text + "</td></tr>");
		strbuild.Append("<tr><td width='30%'>Amount to be paid (with Tax) :</td><td width='70%'>" + txtAmountPaidWithTax.Text + "</td></tr>");
		strbuild.Append("<tr><td width='30%'>Amount paid by Accounts :</td><td width='70%'>" + txtAccountPaidAmt.Text + "</td></tr>");
		strbuild.Append("<tr><td width='30%'>Balance Amount :</td><td width='70%'>" + txtAccountAmtBal.Text + "</td></tr>");
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
		strbuild.Append("<tr><td width='30%'>Department   :</td><td width='70%'>" + txtDepartment.Text + "</td></tr>");
		strbuild.Append("<tr><td width='30%'>Cost Center   :</td><td width='70%'>" + txtCostCentor.Text + "</td></tr>");
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
		spm.sendMail(ApprovalEmail, strsubject, Convert.ToString(strbuild).Trim(), "", ccMailIDs);

	}

	protected void txtAccountPaidAmt_TextChanged(object sender, EventArgs e)
	{
		decimal Result = 0;
		lblmessage.Text = "";
		if (txtAccountPaidAmt.Text != "")
		{
			if (Convert.ToDecimal(txtAccountPaidAmt.Text) > Convert.ToDecimal(HDNPartialPaymnetBal.Value))
			{
				lblmessage.Text = "Amount to be paid cannot exceed balance Amount! ";
				txtAccountPaidAmt.Text = "";
				txtAccountAmtBal.Text = HDNPartialPaymnetBal.Value;
				return;

			}
			else
			{
				txtAccountPaidAmt.Text = Math.Round(Convert.ToDecimal(txtAccountPaidAmt.Text), 2).ToString();
				Result = Convert.ToDecimal(HDNPartialPaymnetBal.Value) - Convert.ToDecimal(txtAccountPaidAmt.Text);
				txtAccountAmtBal.Text = Result.ToString();
			}
		}
		else
		{
			txtAccountAmtBal.Text = HDNPartialPaymnetBal.Value;
		}
	}
	protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
	{
		
	}
	public void GetOldPaymentRequestNo(string strtype)
	{

		DataSet dsTrDetails = new DataSet();
		SqlParameter[] spars = new SqlParameter[3];
		spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
		spars[0].Value = "GetEmployeePaymentRequest";
		spars[2] = new SqlParameter("@PaymentReqNos", SqlDbType.VarChar);
		spars[2].Value = strtype;
		dsTrDetails = spm.getDatasetList(spars, "sp_VSCB_CreatePOWO_Users");
		if (dsTrDetails.Tables[0].Rows.Count > 0)
		{
			hdnEmpCodePrveName.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Emp_Name"]).Trim();
			hdnEmpCodePrveEmailID.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
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

	protected void lstInvoiceNo_SelectedIndexChanged(object sender, EventArgs e)
	{
	
	}
	
	protected void lnkView_Click(object sender, EventArgs e)
	{
		try
		{
			int Payment_ID = 0;
			LinkButton btn = (LinkButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			 Payment_ID = Convert.ToInt32(GrdPartialPayment.DataKeys[row.RowIndex].Values[0]);
			hdnInvoiceID.Value = Convert.ToString(GrdPartialPayment.DataKeys[row.RowIndex].Values[1]).Trim();
			CreateInvoiceDetailsForPayment(Payment_ID);
		}
		catch (Exception)
		{

			throw;
		}
	}

	private void CreateInvoiceDetailsForPayment(int Payment_ID)
	{
		try
		{
			DataTable DTCreateInvoice = new DataTable();
			int InvoiceID = 0, MstoneID = 0;
			InvoiceID = Convert.ToString(hdnInvoiceID.Value).Trim() != "" ? Convert.ToInt32(hdnInvoiceID.Value) : 0;
			//MstoneID = Convert.ToString(hdnMstoneID.Value).Trim() != "" ? Convert.ToInt32(hdnMstoneID.Value) : 0;
			DTCreateInvoice = spm.dtShowInvoiceDetailsForPayment("Create_PartialPayment_WithOutPO", Convert.ToString(hdnEmpCode.Value), InvoiceID, Payment_ID);
			if (DTCreateInvoice.Rows.Count > 0)
			{
				DivCreateInvoice.Visible = true;
				txtApprovalRemark.Enabled = true;
				txtAccountPaidAmt.Enabled = true;
				localtrvl_btnSave.Visible = true;
				accmo_delete_btn.Visible = false;
				PayBal1.Visible = true;
				PayBal2.Visible = true;
				PayBal3.Visible = true;
				txtApprovalRemark.Text = "";
				txtAccountPaidAmt.Text = "";
				txtOldPaymentRequestNo.Text = "";
				txtInvoiceNo.Text = DTCreateInvoice.Rows[0]["InvoiceNo"].ToString();
				hdnInvoiceID.Value = DTCreateInvoice.Rows[0]["InvoiceID"].ToString();
				//hdnMstoneID.Value = DTCreateInvoice.Rows[0]["MstoneID"].ToString();
				txtInvoiceAmount.Text = DTCreateInvoice.Rows[0]["AmtWithTax"].ToString();
				txtInvoiceBalAmt.Text = DTCreateInvoice.Rows[0]["BalanceAmt"].ToString();
				txtPaymentRequestNo.Text = DTCreateInvoice.Rows[0]["PaymentReqNo"].ToString();
				txtOldPaymentRequestNo.Text = DTCreateInvoice.Rows[0]["OldPaymentReqNO"].ToString();
				txtAmountPaidWithTax.Text = DTCreateInvoice.Rows[0]["RemainingBalAmt"].ToString();
				txtAccountAmtBal.Text = DTCreateInvoice.Rows[0]["RemainingBalAmt"].ToString();
				HDNPartialPaymnetBal.Value = DTCreateInvoice.Rows[0]["RemainingBalAmt"].ToString();
				txtTotalPaymentAmt.Text = DTCreateInvoice.Rows[0]["TobePaidAmtWithtax"].ToString();
				txtPartialPaymentBalAmt.Text = DTCreateInvoice.Rows[0]["RemainingBalAmt"].ToString();
				txtRequestDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
			}
			else
			{
				txtInvoiceNo.Text = "";
				txtInvoiceAmount.Text = "";
				txtInvoiceBalAmt.Text = "";
				txtPaymentRequestNo.Text = "";
				txtRequestDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
				txtAccountPaidAmt.Text = "";
				txtAccountAmtBal.Text = "";
			}
		}
		catch (Exception)
		{
			throw;
		}
	}
	private void getApproverlist(string EmpCode, int Payment_ID, string DeptnName, string TallyCode)
	{
		DataTable dtapprover = new DataTable();
		dtapprover = spm.Get_Payment_Request_ApproverList(EmpCode, Payment_ID, DeptnName, TallyCode,"");
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
			SPCreate.InnerText = "Partial Payment Request View :";
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
				txtOldPaymentRequestNo.Text = DTPoWoNumber.Tables[0].Rows[0]["Ref_PaymentReqNo"].ToString();
				txtApprovalRemark.Text = DTPoWoNumber.Tables[0].Rows[0]["Remarks"].ToString();
				//hflStatusID.Value = DTPoWoNumber.Tables[0].Rows[0]["Status_id"].ToString();
				txtApprovalRemark.Enabled = false;
				txtAccountPaidAmt.Enabled = false;
				if (Convert.ToInt32(DTPoWoNumber.Tables[0].Rows[0]["Status_id"]) == 2)
				{
					Account1.Visible = true;
					Account2.Visible = true;
					Account3.Visible = false;
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
			}
		}
		catch (Exception)
		{

			throw;
		}
	}
	protected void lnkdelete_Click(object sender, ImageClickEventArgs e)
	{
		//accmo_delete_btn.Visible = false;
		//txtApprovalRemark.Enabled = true;
		//txtAccountPaidAmt.Enabled = true;
		//txtApprovalRemark.Text = "";
		//txtAccountPaidAmt.Text = "";
		//txtOldPaymentRequestNo.Text = "";
		//PaymentDetailsWithOutPOView();
		//if (hdnAPPType.Value != "View")
		//{
		//	localtrvl_btnSave.Visible = true;
		//}

	}


	protected void accmo_delete_btn_Click(object sender, EventArgs e)
	{
		accmo_delete_btn.Visible = false;
		txtApprovalRemark.Enabled = true;
		txtAccountPaidAmt.Enabled = true;
		txtApprovalRemark.Text = "";
		txtAccountPaidAmt.Text = "";

		txtOldPaymentRequestNo.Text = "";
		PaymentDetailsWithOutPOView();
		if (hdnAPPType.Value != "View")
		{
			localtrvl_btnSave.Visible = true;
			DivCreateInvoice.Visible = false;
			PayBal1.Visible = false;
			PayBal2.Visible = false;
			PayBal3.Visible = false;
			txtTotalPaymentAmt.Text = "";
			txtPartialPaymentBalAmt.Text = "";
		}
	}



	protected void lnkView_Click1(object sender, EventArgs e)
	{

	}

	
}