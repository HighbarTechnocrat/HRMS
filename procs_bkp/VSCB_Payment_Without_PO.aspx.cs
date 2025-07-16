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

public partial class VSCB_Payment_Without_PO : System.Web.UI.Page
{
	#region CreativeMethods
	SqlConnection source;
	public SqlDataAdapter sqladp;

	public string loc = "", approveremailaddress = "", Approvers_code = "";
	public int did = 0, apprid;
	public DataTable dtEmp, DTPoWoNumber, DTInsertPayment, dtApproverEmailIds;
	public string filename = "";
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
			hdnEmpCpde.Value = Session["Empcode"].ToString();

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
					FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VendorPaymentDocpath"]).Trim());
					// trvldeatils_delete_btn.Visible = false;
					if (Request.QueryString.Count > 0)
					{
						string InvoiceID = Convert.ToString(Request.QueryString[0]).Trim();
						hdnType.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        
						if (hdnType.Value == "Create")
						{
                            //InvoiceDetails(Convert.ToInt32(InvoiceID));
                            //ACreate.Visible = true;

                            // harshad Code Start
                            if (Request.QueryString.Count == 3)
                            {
                                hdnInvoiceID.Value = Convert.ToString(Request.QueryString[0]).Trim();
                                InvoiceDetailsLinkOnPageLoad(Convert.ToInt32(InvoiceID));
                                ACreate.Visible = true;
                                CreateInvoiceDetailsForPayment();
                                
                            }
                            else
                            {
                                InvoiceDetails(Convert.ToInt32(InvoiceID));
                                ACreate.Visible = true;
                            }
                            // harshad Code END
                        }
                        else
						{
							lblheading.Text = "Payment Request View";
							hdnPayment_ID.Value = InvoiceID;
							PaymentDetailsWithOutPO();
							btnRecBack.Visible = true;
							localtrvl_btnSave.Text = "Update Payment Request";
							localtrvl_btnSave.ToolTip = "Update Payment Request";
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
	private void PaymentDetailsWithOutPO()
	{
		try
		{
			DataSet DTPoWoNumber = new DataSet();
			DataTable DTstatus = new DataTable();
			int Payment_ID = 0, POID = 0;
			Payment_ID = Convert.ToString(hdnPayment_ID.Value).Trim() != "" ? Convert.ToInt32(hdnPayment_ID.Value) : 0;
			//POID = Convert.ToString(hdnPOID.Value).Trim() != "" ? Convert.ToInt32(hdnPOID.Value) : 0;
			DTPoWoNumber = spm.GetVSCB_MyPaymentDetails("MyPaymentRequestDeatilsWithOutPO", Convert.ToString(hdnEmpCpde.Value), POID, Payment_ID);

			accmo_delete_btn.Visible = false;
			if (DTPoWoNumber.Tables[0].Rows.Count > 0)
			{
				GrdInvoiceDetails.DataSource = DTPoWoNumber.Tables[0];
				GrdInvoiceDetails.DataBind();
				lstInvoiceNo.Enabled = false;
				lstInvoiceNo.Text = DTPoWoNumber.Tables[0].Rows[0]["InvoiceNo"].ToString();
				txtVendorName.Text = DTPoWoNumber.Tables[0].Rows[0]["Name"].ToString();
				txtDepartment.Text = DTPoWoNumber.Tables[0].Rows[0]["Department"].ToString();
				txtCostCentor.Text = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
				hdnCostCentre.Value = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
				hdnDept_Name.Value = DTPoWoNumber.Tables[0].Rows[0]["Department"].ToString();
                txtTallyCode_display.Text = DTPoWoNumber.Tables[0].Rows[0]["Tallycode"].ToString();
            }

            if (DTPoWoNumber.Tables[1].Rows.Count > 0)
            {
                DivCreateInvoice.Visible = true;
                hdnInvoiceID.Value = DTPoWoNumber.Tables[1].Rows[0]["InvoiceID"].ToString();
                txtInvoiceNo.Text = DTPoWoNumber.Tables[1].Rows[0]["InvoiceNo"].ToString();
                txtInvoiceAmount.Text = DTPoWoNumber.Tables[1].Rows[0]["AmtWithTax"].ToString();  //AmtWithTax Payable_Amt_With_Tax
                txtInvoiceBalAmt.Text = DTPoWoNumber.Tables[1].Rows[0]["BalanceAmt"].ToString();
                //txtGSTINNO.Text = DTCreateInvoice.Rows[0]["PaymentStatusID"].ToString();
                txtPaymentRequestNo.Text = DTPoWoNumber.Tables[1].Rows[0]["PaymentReqNo"].ToString();
                txtRequestDate.Text = DTPoWoNumber.Tables[1].Rows[0]["PaymentReqDate"].ToString();
                txtAmountPaidWithTax.Text = DTPoWoNumber.Tables[1].Rows[0]["TobePaidAmtWithtax"].ToString();
                hdnInvoicePayableAmt.Value = DTPoWoNumber.Tables[1].Rows[0]["Payable_Amt_With_Tax"].ToString();

                txtInvoiceTDSAmt.Text = DTPoWoNumber.Tables[1].Rows[0]["DirectTax_Amount"].ToString();
                txtInvoicePaidAmount.Text = DTPoWoNumber.Tables[1].Rows[0]["AccountPaidAmt"].ToString();
                txtPaymentRequestAmount.Text = DTPoWoNumber.Tables[1].Rows[0]["PaymentReqstForAmt"].ToString();

                txtInvoiceType.Text = DTPoWoNumber.Tables[0].Rows[0]["InvoiceTypeDisplay"].ToString();
                hdnInvoiceType.Value = DTPoWoNumber.Tables[0].Rows[0]["POType"].ToString();
                hdnInvoiceTypeId.Value = DTPoWoNumber.Tables[0].Rows[0]["PoTypeId"].ToString();


                //hdnpaymentAmt.Value=
                hflStatusID.Value = DTPoWoNumber.Tables[1].Rows[0]["Status_id"].ToString();
                lblheading.Text = "Payment Request View - " + txtPaymentRequestNo.Text + ", Payment Status - " + DTPoWoNumber.Tables[1].Rows[0]["PyamentStatus"].ToString();
                this.GrdInvoiceDetails.Columns[0].Visible = false;
                if (Convert.ToInt32(DTPoWoNumber.Tables[1].Rows[0]["Status_id"]) == 5)
                {
                    localtrvl_btnSave.Visible = true;

                }
                else
                {

                    localtrvl_btnSave.Visible = false;
                    txtAmountPaidWithTax.Enabled = false;
                }
                if (Convert.ToInt32(DTPoWoNumber.Tables[1].Rows[0]["Status_id"]) == 2)
                {
                    Account1.Visible = true;
                    Account2.Visible = true;
                    Account3.Visible = true;
                    txtAccountPaidAmt.Text = DTPoWoNumber.Tables[1].Rows[0]["Amt_paid_Account"].ToString();
                    txtAccountAmtBal.Text = DTPoWoNumber.Tables[1].Rows[0]["AccountBalAmt"].ToString();
                    txtAccountPaidAmt.Enabled = false;
                    txtAccountAmtBal.Enabled = false;
                }
                else
                {
                    Account1.Visible = false;
                    Account2.Visible = false;
                    Account3.Visible = false;
                }
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
            if (DTPoWoNumber.Tables[5].Rows.Count > 0)
            {
                GrdInvoiceApr.DataSource = DTPoWoNumber.Tables[5];
                GrdInvoiceApr.DataBind();
            }
            else
            {
                Span2.Visible = false;
            }

            Payment_ID = Convert.ToString(hdnPayment_ID.Value).Trim() != "" ? Convert.ToInt32(hdnPayment_ID.Value) : 0;
			DTstatus = spm.Get_Payment_AccountApprovalEmp("GetPaymentApprovalStatus", "", Payment_ID);
			if (DTstatus.Rows.Count > 0)
			{
				if (Convert.ToString(DTstatus.Rows[0]["Action"]).Trim() == "Pending" || Convert.ToString(DTstatus.Rows[0]["Action"]).Trim() == "Correction")
				{
					this.GrdFileUpload.Columns[1].Visible = true;
				}
			}
			PaymentBalanceAmt();
			getApproverlist(hdnEmpCpde.Value, Payment_ID, hdnDept_Name.Value, hdnCostCentre.Value);
		}
		catch (Exception)
		{

			throw;
		}
	}
	
	private void PaymentBalanceAmt()
	{
		try
		{
			DataTable DTCreateInvoice = new DataTable();
			decimal InvoiceBalAmt = 0; int InvoiceID = 0;
			InvoiceID = Convert.ToString(hdnInvoiceID.Value).Trim() != "" ? Convert.ToInt32(hdnInvoiceID.Value) : 0;
			DTCreateInvoice = spm.dtShowInvoiceDetailsForPayment("POWO_Invoice_PaymentAmt", Convert.ToString(hdnEmpCpde.Value), InvoiceID, 0);
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
		int Payment_ID = 0, MstoneID = 0, InvoiceID = 0;
		decimal BalAmount = 0;
		DateTime PaymentDate;
		string EmpCode = "", strtoDate = "", multiplefilename = "", multiplefilenameadd = "", strfileName = "",QType="";
		string[] strdate;
		HttpPostedFile uploadfileName = null;
		try
		{
			#region Check For Blank Fields
			string confirmValue = hdnYesNo.Value.ToString();
			if (confirmValue != "Yes")
			{
				return;
			}
			if (Convert.ToString(txtInvoiceNo.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter Invoice No";
				return;
			}
			if (Convert.ToString(txtPaymentRequestNo.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter Payment Request No";
				return;
			}
            if (Convert.ToString(txtInvoiceType.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Invoice Type";
                return;
            }

            if (Convert.ToString(txtAmountPaidWithTax.Text).Trim() == "" || Convert.ToString(txtAmountPaidWithTax.Text).Trim() == "0")
			{
				lblmessage.Text = "Please enter Amount Paid (With Tax)";
				return;
			}
			if (Convert.ToString(txtAmountPaidWithTax.Text).Trim() != "" && Convert.ToString(txtInvoiceBalAmt.Text).Trim() != "")
			{
				if (Convert.ToDecimal(txtAmountPaidWithTax.Text) > Convert.ToDecimal(txtInvoiceBalAmt.Text))//txtInvoiceAmount
				{
					lblmessage.Text = "Amount to be paid cannot exceed balance Invoice Amount! ";
					return;
				}
			}
			if (Convert.ToString(txtAmountPaidWithTax.Text).Trim() != "" && hdnTobePaidAmt.Value != "")
			{
				decimal BalAmt = 0;
				BalAmt = Convert.ToDecimal(hdnInvoicePayableAmt.Value) - Convert.ToDecimal(hdnTobePaidAmt.Value);
				if (Convert.ToDecimal(txtAmountPaidWithTax.Text) > BalAmt)//txtInvoiceAmount
				{
					lblmessage.Text = "Amount to be paid cannot exceed balance Invoice Amount! ";
					return;
				}
			}
			#endregion

			if (Convert.ToString(uploadfile.FileName).Trim() != "")
			{
				HttpFileCollection fileCollection = Request.Files;
				int j = 1;
				for (int i = 0; i < fileCollection.Count; i++)
				{
					uploadfileName = fileCollection[i];
					string fileName = Path.GetFileName(uploadfileName.FileName);
					if (uploadfileName.ContentLength > 0)
					{
						multiplefilename = fileName;
						strfileName = "";
						string Dates = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
						string strremoveSpace = "Pay_" + j + "_" + hdnEmpCpde.Value + "_" + Dates + Path.GetExtension(fileName);
						//string strremoveSpace = i + "_" + multiplefilename + "_" + Dates + Path.GetExtension(fileName);
						strfileName = Regex.Replace(strremoveSpace, @"[^0-9a-zA-Z\._]", "_");
						uploadfileName.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VendorPaymentDocpath"]).Trim()), strfileName));
						multiplefilenameadd += strfileName + ","; j++;
					}
				}
			}
			strdate = Convert.ToString(txtRequestDate.Text).Trim().Split('-');
			strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
			PaymentDate = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
			EmpCode = hdnEmpCpde.Value;
			dtApproverEmailIds = spm.Get_Payment_Request_ApproverEmailID(EmpCode, hdnDept_Name.Value, hdnCostCentre.Value,0,Convert.ToString(hdnInvoiceType.Value));
			if (dtApproverEmailIds.Rows.Count > 0)
			{
				approveremailaddress = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];
				apprid = (int)dtApproverEmailIds.Rows[0]["APPR_ID"];
				nxtapprname = (string)dtApproverEmailIds.Rows[0]["Emp_Name"];
				Approvers_code = (string)dtApproverEmailIds.Rows[0]["A_EMP_CODE"];
				hdnapprcode.Value = Approvers_code;
			}
			InvoiceID = Convert.ToString(hdnInvoiceID.Value).Trim() != "" ? Convert.ToInt32(hdnInvoiceID.Value) : 0;
			Payment_ID = Convert.ToString(hdnPayment_ID.Value).Trim() != "" ? Convert.ToInt32(hdnPayment_ID.Value) : 0;
			//BalAmount=Convert.ToDecimal(txtAmountPaidWithTax) - Convert.ToDecimal(txtAmountPaidWithTax);			
			multiplefilenameadd = multiplefilenameadd.TrimEnd(',');
			if (Payment_ID > 0)
			{
				QType = "PAYMNETUPDATEREQUEST";
			}
			else
			{
				QType = "PAYMNETINSERTREQUEST_WITHOUTPO";
			}

            DataSet dtPOWODetails = spm.getFormated_Amount("get_formated_Invoice_Amount",Convert.ToDecimal(txtAmountPaidWithTax.Text), 0, 0, 0, 0); 
            if (dtPOWODetails.Tables[0].Rows.Count > 0)
            {
                txtAmountPaidWithTax.Text = Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["invoiceAmt"]).Trim(); 
            }

            DTInsertPayment = spm.dtInsert_Payment_Request(QType, EmpCode, InvoiceID, MstoneID, Payment_ID, txtPaymentRequestNo.Text.Trim(), Convert.ToDecimal(txtAmountPaidWithTax.Text), PaymentDate, 1, 1, 0, BalAmount, "", multiplefilenameadd);
			if (DTInsertPayment.Rows.Count > 0)
			{
				Payment_ID = Convert.ToInt32(DTInsertPayment.Rows[0]["Payment_ID"]);
				if (Convert.ToString(Payment_ID).Trim() == "0")
					return;
				string strPatmentURL = "", strapproverlist = "";
				string PowoNo = "";
				PowoNo = txtInvoiceNo.Text;
				spm.Insert_Payment_Approver_Request(Approvers_code, apprid, Payment_ID);
				#region Send Email to 1st Approver
				strPatmentURL = (Convert.ToString(ConfigurationManager.AppSettings["Link_PayRequestAPPwithOut"]).Trim() + "?Payment_ID=" + Payment_ID + "&Type=APP");
				strapproverlist = GetPaymnetApprove_RejectList(hdnEmpCpde.Value, Payment_ID, hdnDept_Name.Value, hdnCostCentre.Value);
				var actionByName = Session["emp_loginName"].ToString();
				GetInvoiceTDSAmt();
				string CostCenter = hdnCostCentre.Value;
				string strsubject = "Request for - Payment Approval “" + txtPaymentRequestNo.Text + "”  For “" + PowoNo + "”";
				string TBody = actionByName + " has created a payment request as per the details below. Request your approval please.";
				StringBuilder strbuild = new StringBuilder();
				strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;width:100%;table-layout:fixed;'><tr><td>   ");
				strbuild.Append("</td></tr>");
				strbuild.Append("<tr><td style='height:20px;'></td></tr>");
				strbuild.Append("<tr><td>Dear " + nxtapprname + ",</td></tr>");
				strbuild.Append("<tr><td style='height:10px;'></td></tr>");
				strbuild.Append("<tr><td> " + TBody + "</td></tr>");
				strbuild.Append("<tr><td  style='height:10px;'></td></tr>");
				strbuild.Append("<tr><td>");
				strbuild.Append("<table style='width:80% !important;color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;table-layout:fixed;'>");
				strbuild.Append("<tr><td width='30%'>Payment Request No  :</td><td width='70%'>" + txtPaymentRequestNo.Text + "</td></tr>");
				strbuild.Append("<tr><td width='30%'>Request Date  :</td><td width='70%'>" + txtRequestDate.Text + "</td></tr>");
				strbuild.Append("<tr><td width='30%'>Amount to be paid (with Tax) :</td><td width='70%'>" + txtAmountPaidWithTax.Text + "</td></tr>");
				strbuild.Append("<tr><td style='height:20px;'></td></tr>");
				strbuild.Append("<tr><td width='30%'>Invoice No  :</td><td width='70%'>" + PowoNo + "</td></tr>");
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
				spm.sendMail_VSCB(approveremailaddress, strsubject, Convert.ToString(strbuild).Trim(), "", "");
				#endregion
			}
            if (Request.QueryString.Count == 3)
            {
                Response.Redirect("~/procs/VSCB_PaymentRequestAll.aspx", false);
            }
            else
            {
                Response.Redirect("~/procs/VSCB_InboxMyPaymentRequest.aspx", false);
            }
                
		}
		catch (Exception ex)
		{

			throw;
		}
	}

	protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
	{

	}

	//protected void lstInvoiceNo_SelectedIndexChanged(object sender, EventArgs e)
	//{
	//	try
	//	{	
	//		if (lstInvoiceNo.SelectedIndex > 0)
	//		{
	//			int InvoiceID = Convert.ToInt32(lstInvoiceNo.SelectedValue);
				
	//		}
	//	}
	//	catch (Exception)
	//	{
	//		throw;
	//	}
	//}

	private void InvoiceDetails(int InvoiceID)
	{
		try
		{
			DataSet DTPoWoNumber = new DataSet();
			DTPoWoNumber = spm.Vendor_InvoiceDetails("InvoiceRequestDeatils", Convert.ToString(hdnEmpCpde.Value), InvoiceID);
			if (DTPoWoNumber.Tables[0].Rows.Count > 0)
			{
				txtVendorName.Text = DTPoWoNumber.Tables[0].Rows[0]["Name"].ToString();
				txtDepartment.Text = DTPoWoNumber.Tables[0].Rows[0]["Department"].ToString();			
				txtCostCentor.Text = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
				lstInvoiceNo.Text = DTPoWoNumber.Tables[0].Rows[0]["InvoiceNo"].ToString();
				//hdnCostCentre.Value = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
				hdnCostCentre.Value = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
				hdnDept_Name.Value = DTPoWoNumber.Tables[0].Rows[0]["Department"].ToString();
                txtInvoiceType.Text = DTPoWoNumber.Tables[0].Rows[0]["InvoiceTypeDisplay"].ToString();
                hdnInvoiceType.Value = DTPoWoNumber.Tables[0].Rows[0]["POType"].ToString();
                hdnInvoiceTypeId.Value = DTPoWoNumber.Tables[0].Rows[0]["PoTypeId"].ToString();
                txtTallyCode_display.Text = DTPoWoNumber.Tables[0].Rows[0]["Tallycode"].ToString();

                GrdInvoiceDetails.DataSource = DTPoWoNumber.Tables[0];
				GrdInvoiceDetails.DataBind();
			}
			if (DTPoWoNumber.Tables[1].Rows.Count > 0)
			{
				SPPayHist.Visible = true;
				GrdInvoiceHistDetails.DataSource = DTPoWoNumber.Tables[1];
				GrdInvoiceHistDetails.DataBind();
			}
            if (DTPoWoNumber.Tables[2].Rows.Count > 0)              //Section added by ajinkya   
            {
                GrdInvoiceApr.DataSource = DTPoWoNumber.Tables[2];
                GrdInvoiceApr.DataBind();
            }
            else
            {
                Span2.Visible = false;
            }

            getApproverlist(hdnEmpCpde.Value, 0, hdnDept_Name.Value, hdnCostCentre.Value);
		}
		catch (Exception)
		{

			throw;
		}
	}

    private void InvoiceDetailsLinkOnPageLoad(int InvoiceID)
    {
        try
        {
            DataSet DTPoWoNumber = new DataSet();
            DTPoWoNumber = spm.Vendor_InvoiceDetails("InvoiceRequestDeatilsPayment", Convert.ToString(hdnEmpCpde.Value), InvoiceID);
            if (DTPoWoNumber.Tables[0].Rows.Count > 0)
            {
                txtVendorName.Text = DTPoWoNumber.Tables[0].Rows[0]["Name"].ToString();
                txtDepartment.Text = DTPoWoNumber.Tables[0].Rows[0]["Department"].ToString();
                txtCostCentor.Text = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
                lstInvoiceNo.Text = DTPoWoNumber.Tables[0].Rows[0]["InvoiceNo"].ToString();
                //hdnCostCentre.Value = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
                hdnCostCentre.Value = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
                hdnDept_Name.Value = DTPoWoNumber.Tables[0].Rows[0]["Department"].ToString();
                txtInvoiceType.Text = DTPoWoNumber.Tables[0].Rows[0]["InvoiceTypeDisplay"].ToString();
                hdnInvoiceType.Value = DTPoWoNumber.Tables[0].Rows[0]["POType"].ToString();
                hdnInvoiceTypeId.Value = DTPoWoNumber.Tables[0].Rows[0]["PoTypeId"].ToString();
                txtTallyCode_display.Text= DTPoWoNumber.Tables[0].Rows[0]["Tallycode"].ToString();


                GrdInvoiceDetails.DataSource = DTPoWoNumber.Tables[0];
                GrdInvoiceDetails.DataBind();
                if (GrdInvoiceDetails.Columns.Count > 1)
                {
                    GrdInvoiceDetails.Columns[0].Visible = false;
                }
            }
            if (DTPoWoNumber.Tables[1].Rows.Count > 0)
            {
                SPPayHist.Visible = true;
                GrdInvoiceHistDetails.DataSource = DTPoWoNumber.Tables[1];
                GrdInvoiceHistDetails.DataBind();
            }
            if (DTPoWoNumber.Tables[2].Rows.Count > 0)
            {
                GrdInvoiceApr.DataSource = DTPoWoNumber.Tables[2];
                GrdInvoiceApr.DataBind();
            }

            getApproverlist(hdnEmpCpde.Value, 0, hdnDept_Name.Value, hdnCostCentre.Value);
            Check_CostCenterApprovalMatrix(Convert.ToString(txtTallyCode_display.Text).Trim());
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void Check_CostCenterApprovalMatrix(string stallycode)
    {
        DataSet dtPOWODetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Selected_TallyCode_Payment_Approval_Matrix";

        spars[1] = new SqlParameter("@Project_Dept_Name", SqlDbType.VarChar);
        spars[1].Value = stallycode;

        spars[2] = new SqlParameter("@POTypeID", SqlDbType.VarChar);
        if (Convert.ToString(hdnInvoiceTypeId.Value).Trim() != "")
            spars[2].Value = Convert.ToString(hdnInvoiceTypeId.Value);
        else
            spars[2].Value = DBNull.Value;


        dtPOWODetails = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        if (dtPOWODetails.Tables[0].Rows.Count <= 0)
        {
            localtrvl_btnSave.Visible = false;
            lblmessage.Text = "Approval Matrix not set. Please contact to Admin";
        }

    }

    protected string GetPaymnetApprove_RejectList(string EmpCode, int Payment_ID, string DeptName, string TallyCode)
	{
		StringBuilder strbuild_Approvers = new StringBuilder();
		strbuild_Approvers.Length = 0;
		strbuild_Approvers.Capacity = 0;
		DataTable dtAppRej = new DataTable();
		dtAppRej = spm.Get_Payment_Request_ApproverList(EmpCode, Payment_ID, DeptName, TallyCode,Convert.ToString(hdnInvoiceType.Value));
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
			CreateInvoiceDetailsForPayment();
		}
		catch (Exception)
		{

			throw;
		}
	}
	private void CreateInvoiceDetailsForPayment()
	{
		try
		{
			DataTable DTCreateInvoice = new DataTable();
			int InvoiceID = 0, MstoneID = 0;
			InvoiceID = Convert.ToString(hdnInvoiceID.Value).Trim() != "" ? Convert.ToInt32(hdnInvoiceID.Value) : 0;
			DTCreateInvoice = spm.dtShowInvoiceDetailsForPayment("Create_Payment_WithOutPO", Convert.ToString(hdnEmpCpde.Value), InvoiceID, MstoneID);
			if (DTCreateInvoice.Rows.Count > 0)
			{
				DivCreateInvoice.Visible = true;
				txtInvoiceNo.Text = DTCreateInvoice.Rows[0]["InvoiceNo"].ToString();
				txtInvoiceAmount.Text = DTCreateInvoice.Rows[0]["AmtWithTax"].ToString();//AmtWithTax Payable_Amt_With_Tax
				txtInvoiceBalAmt.Text = DTCreateInvoice.Rows[0]["BalanceAmt"].ToString();
				//txtVendorName.Text = DTCreateInvoice.Rows[0]["Status_id"].ToString();
				//txtGSTINNO.Text = DTCreateInvoice.Rows[0]["PaymentStatusID"].ToString();
				txtPaymentRequestNo.Text = DTCreateInvoice.Rows[0]["PaymentRequestNo"].ToString();
				hdnTobePaidAmt.Value = DTCreateInvoice.Rows[0]["TobePaidAmtWithtax"].ToString();
				hdnInvoicePayableAmt.Value = DTCreateInvoice.Rows[0]["Payable_Amt_With_Tax"].ToString();
				txtRequestDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                txtInvoiceTDSAmt.Text= DTCreateInvoice.Rows[0]["DirectTax_Amount"].ToString();
                txtInvoicePaidAmount.Text = DTCreateInvoice.Rows[0]["AccountPaidAmt"].ToString();
                txtPaymentRequestAmount.Text = DTCreateInvoice.Rows[0]["PaymentReqstForAmt"].ToString();

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
			dtCostCenter = spm.GetInvoiceTDS_Amt("GetInvoiceTDSAmt", hdnEmpCpde.Value, InvoiceID);
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
			DTPoWoNumber = spm.GetVSCB_MyPaymentDetails("POWOPaymentRequestHistory", Convert.ToString(hdnEmpCpde.Value), POID, Payment_ID);
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
			}
		}
		catch (Exception)
		{

			throw;
		}
	}
	protected void lnkdelete_Click(object sender, ImageClickEventArgs e)
	{
		try
		{
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			string FileName = ""; int Payment_ID = 0;
			FileName = Convert.ToString(GrdFileUpload.DataKeys[row.RowIndex].Values[1]).Trim();
			Payment_ID = Convert.ToInt32(GrdFileUpload.DataKeys[row.RowIndex].Values[0]);
			if (Convert.ToString(FileName).Trim() != "")
			{
				string file = "";
				file = (Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VendorPaymentDocpath"]).Trim()), FileName.ToString().Trim()));

				if ((System.IO.File.Exists(file)))
				{
					System.IO.File.Delete(file);
				}
			}
			DataTable dtFile = spm.Get_VSCB_Payment_DeleteFile("SelectPaymentRequest_DeleteFile", Convert.ToString(hdnEmpCpde.Value), Payment_ID, FileName);
			if (dtFile.Rows.Count > 0)
			{
				GrdFileUpload.DataSource = dtFile;
				GrdFileUpload.DataBind();
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}
	protected void GrdInvoiceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		try
		{
			DataTable DTCreateInvoice = new DataTable();
			Decimal PaymentAmt = 0, InvoiceAmt = 0;
			string abcd = "";
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				int InvoiceID = Convert.ToInt32(GrdInvoiceDetails.DataKeys[e.Row.RowIndex].Values[0]);
				int MstoneID = 0;
				InvoiceAmt = Convert.ToDecimal(e.Row.Cells[13].Text);
				//abcd = e.Row.Cells[14].Text.Trim();				
				DTCreateInvoice = spm.dtShowInvoiceDetailsForPayment("POWO_Invoice_PaymentAmt", Convert.ToString(hdnEmpCpde.Value), InvoiceID, MstoneID);
				if (DTCreateInvoice.Rows.Count > 0)
				{
					//PaymentAmt = Convert.ToDecimal(DTCreateInvoice.Rows[0]["TobePaidAmtWithtax"]);
					PaymentAmt = Convert.ToString(DTCreateInvoice.Rows[0]["TobePaidAmtWithtax"]).Trim() != "" ? Convert.ToDecimal(DTCreateInvoice.Rows[0]["TobePaidAmtWithtax"]) : 0;
					if (PaymentAmt >= InvoiceAmt)
					{
						LinkButton btn = (LinkButton)e.Row.FindControl("lnkView");
						btn.Visible = false;
						//e.Row.Cells[0].CssClass = "hiddencol1";

					}
				}
			}
		}
		catch (Exception)
		{
			throw;
		}
	}

	protected void accmo_delete_btn_Click(object sender, EventArgs e)
	{
		localtrvl_btnSave.Visible = true;
		PaymentDetailsWithOutPO();
	}

}