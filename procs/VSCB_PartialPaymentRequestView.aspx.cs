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

public partial class procs_VSCB_PartialPaymentRequestView : System.Web.UI.Page
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
                    FilePathPOWOSignCopy.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBSignPOWOfiles"]).Trim());
                    txtAccountPaidAmt.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					hdnLoginUserName.Value = Convert.ToString(Session["emp_loginName"]).Trim();
					hdnLoginEmpEmail.Value = Convert.ToString(Session["LoginEmpmail"]).Trim();
					if (Request.QueryString.Count > 0)
					{
						hdnPayment_ID.Value = Convert.ToString(Request.QueryString[0]).Trim();
						hdnPOID.Value = Convert.ToString(Request.QueryString[1]).Trim();
						//CheckPayment_ApprovalStatus_Submit();					
						CheckAccountApprovalEmp();
						PaymentDetails();
						btnRecBack.Visible = true;
					}
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}



	private void PaymentDetails()
	{
		try
		{
			accmo_delete_btn.Visible = false;
			DataSet DTPoWoNumber = new DataSet();
			int Payment_ID = 0, POID = 0;
			Payment_ID = Convert.ToString(hdnPayment_ID.Value).Trim() != "" ? Convert.ToInt32(hdnPayment_ID.Value) : 0;
			POID = Convert.ToString(hdnPOID.Value).Trim() != "" ? Convert.ToInt32(hdnPOID.Value) : 0;
			DTPoWoNumber = spm.GetVSCB_MyPaymentDetails("POWOMyPartialPaymentRequest", Convert.ToString(hdnEmpCode.Value), POID, Payment_ID);
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
				txtPoWOAmtWIthoutTax.Text = DTPoWoNumber.Tables[0].Rows[0]["POWO_T_BaseAmt"].ToString();
				txtPoWOPaidAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["PO_Paid_Amt"].ToString();
                txtPoPaidAmt_WithOutDT.Text = DTPoWoNumber.Tables[0].Rows[0]["POPiadAmount_withoutDT"].ToString();
                txtPoWOPaidBalAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["PO_Bal_Amt"].ToString();
				txtPODirectTaxAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["DirectTaxCollection_Amt"].ToString();
				txtProjectName.Text = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
				txtDepartment.Text = DTPoWoNumber.Tables[0].Rows[0]["Department"].ToString();
				hdnCostCentre.Value = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
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
			if (DTPoWoNumber.Tables[2].Rows.Count > 0)
			{
				GrdInvoiceDetails.DataSource = DTPoWoNumber.Tables[2];
				GrdInvoiceDetails.DataBind();
			}
			if (DTPoWoNumber.Tables[3].Rows.Count > 0)
			{
				GrdPartialPayment.DataSource = DTPoWoNumber.Tables[3];
				GrdPartialPayment.DataBind();
			}
			if (DTPoWoNumber.Tables[4].Rows.Count > 0)
			{
				//DivCreateInvoice.Visible = true;
				hdnInvoiceID.Value = DTPoWoNumber.Tables[4].Rows[0]["InvoiceID"].ToString();
				hdnMstoneID.Value = DTPoWoNumber.Tables[4].Rows[0]["MstoneID"].ToString();
				txtInvoiceNo.Text = DTPoWoNumber.Tables[4].Rows[0]["InvoiceNo"].ToString();
				txtInvoiceAmount.Text = DTPoWoNumber.Tables[3].Rows[0]["AmtWithTax"].ToString(); // //AmtWithTax Payable_Amt_With_Tax
				txtInvoiceBalAmt.Text = DTPoWoNumber.Tables[4].Rows[0]["BalanceAmt"].ToString();
				txtPaymentRequestNo.Text = DTPoWoNumber.Tables[4].Rows[0]["PaymentReqNo"].ToString();
				txtRequestDate.Text = DTPoWoNumber.Tables[4].Rows[0]["PaymentReqDate"].ToString();
				txtOldPaymentRequestNo.Text = DTPoWoNumber.Tables[4].Rows[0]["OldPaymentReqNO"].ToString();
				txtAmountPaidWithTax.Text = DTPoWoNumber.Tables[4].Rows[0]["TobePaidAmtWithtax"].ToString();
				txtAccountAmtBal.Text = DTPoWoNumber.Tables[4].Rows[0]["AccountBalAmt"].ToString();
				txtAccountPaidAmt.Text = DTPoWoNumber.Tables[4].Rows[0]["Amt_paid_Account"].ToString();
				txtApprovalRemark.Text = DTPoWoNumber.Tables[4].Rows[0]["Remarks"].ToString();
				hdnEmpCodePrve.Value = DTPoWoNumber.Tables[4].Rows[0]["Emp_Code"].ToString();

                txtPaymentRequestAmount.Text = DTPoWoNumber.Tables[4].Rows[0]["paymentRequestForAmt"].ToString(); //DirectTax_Amount
                txtInvoiceTDSAmt.Text = DTPoWoNumber.Tables[4].Rows[0]["DirectTax_Amount"].ToString();
                txtInvoicePaidAmount.Text = DTPoWoNumber.Tables[4].Rows[0]["AccountPaidAmt"].ToString(); //InvoicePaidAmt;

                lblheading.Text = " Partial Payment Request View - " + txtPaymentRequestNo.Text + ", Payment Status - " + DTPoWoNumber.Tables[4].Rows[0]["PyamentStatus"].ToString();

			}
			GrdFileUpload.DataSource = null;
			GrdFileUpload.DataBind();
			if (DTPoWoNumber.Tables[5].Rows.Count > 0)
			{
				GrdFileUpload.DataSource = DTPoWoNumber.Tables[5];
				GrdFileUpload.DataBind();

                spnPaymentSupportingFiles.Visible = true;
			}
			if (DTPoWoNumber.Tables[6].Rows.Count > 0)
			{
				SPPayHist.Visible = true;
				GrdInvoiceHistDetails.DataSource = DTPoWoNumber.Tables[6];
				GrdInvoiceHistDetails.DataBind();
			}

            if (DTPoWoNumber.Tables[7].Rows.Count > 0)
            {
                GridViewAccountpaidbyAmount.DataSource = DTPoWoNumber.Tables[7];
                GridViewAccountpaidbyAmount.DataBind();
            }


        }
		catch (Exception)
		{

			throw;
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
					localtrvl_btnSave.ToolTip = "Create & verify Payment Request";
					Account1.Visible = true;
					Account2.Visible = true;
					Account3.Visible = false;
				}
			}
			else
			{
				Account1.Visible = true;
				Account2.Visible = true;
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

				if (Convert.ToString(txtAmountPaidWithTax.Text).Trim() != "" && Convert.ToString(txtAccountPaidAmt.Text).Trim() != "")
				{
					BalAmt = Convert.ToDecimal(txtAmountPaidWithTax.Text) - Convert.ToDecimal(txtAccountPaidAmt.Text);
					if (BalAmt > 0)
					{
						Payment_Status_ID = 3;
					}
					else
					{
						Payment_Status_ID = 2;
					}
				}

				DTInsertPayment = spm.dtInsert_Payment_Request("PAYMNETPARTIALREQUESTINSERT", EmpCode, InvoiceID, MstoneID, Payment_ID, txtPaymentRequestNo.Text.Trim(), Convert.ToDecimal(txtAmountPaidWithTax.Text), PaymentDate, 2, Payment_Status_ID, Convert.ToDecimal(txtAccountPaidAmt.Text), Convert.ToDecimal(txtAccountAmtBal.Text), txtOldPaymentRequestNo.Text.Trim(), "");
			}
			String strPatmentURL = "", strapproverlist = "";
			var Tbody = "";
			GetEmployeeCode(hdnEmpCodePrve.Value);
			string strsubject = "Vendor Sub - Con Billing - Payment request for “" + txtPaymentRequestNo.Text + "”  of " + hdnEmpCodePrveName.Value;
			if (hdnExtraAPP.Value.ToString().Trim() == "Account")
			{
				strPatmentURL = "";
				Tbody = "This is to inform you that Account has below Partial Payment Paid.";
				//spm.send_mailto_VSCB_Payment_Request_Partial_Payment(hdnEmpCodePrveName.Value, hdnLoginEmpEmail.Value, hdnEmpCodePrveEmailID.Value, strsubject, Tbody, "", strPatmentURL, strapproverlist, txtPOWONumber.Text, txtPoWoTitle.Text, txtPoWoDate.Text, txtPoWoType.Text, txtVendorName.Text, txtPoWoAmtWithTaxes.Text, txtPoWOPaidAmt.Text, txtPoWOPaidBalAmt.Text, txtProjectName.Text, txtInvoiceNo.Text, txtInvoiceAmount.Text, txtInvoiceBalAmt.Text, txtPaymentRequestNo.Text, txtRequestDate.Text, txtAmountPaidWithTax.Text, txtAccountPaidAmt.Text, txtAccountAmtBal.Text);
			}
			Response.Redirect("~/procs/VSCB_InboxPartialPaymentRequest.aspx", false);
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

	protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
	{

	}

	protected void lnkdelete_Click(object sender, ImageClickEventArgs e)
	{

	}

	protected void accmo_delete_btn_Click(object sender, EventArgs e)
	{
		
		//OldPayNo.Style.Add("display", "inline-block;");
		txtAmountPaidWithTax.Text = "";
		txtAccountPaidAmt.Text = "";
		txtAccountAmtBal.Text = "";
		txtOldPaymentRequestNo.Text = "";
		txtRequestDate.Text = "";
		txtPaymentRequestNo.Text = "";
		txtApprovalRemark.Text = "";
		Account3.Visible = false;
		localtrvl_btnSave.Visible = false;
		txtAccountPaidAmt.Enabled = false;
		txtApprovalRemark.Enabled = false;
		PaymentDetails();
	}

	protected void txtAccountPaidAmt_TextChanged(object sender, EventArgs e)
	{
		decimal Result = 0;
		if (txtAccountPaidAmt.Text != "")
		{
			if (Convert.ToDecimal(txtAccountPaidAmt.Text) <= Convert.ToDecimal(txtAmountPaidWithTax.Text))
			{
				Result = Convert.ToDecimal(txtAmountPaidWithTax.Text) - Convert.ToDecimal(txtAccountPaidAmt.Text);
				txtAccountAmtBal.Text = Result.ToString();
			}
			else
			{
				txtAccountAmtBal.Text = Result.ToString();
				txtAccountPaidAmt.Text = Result.ToString();
			}
		}
		else
		{
			txtAccountAmtBal.Text = "";
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
			accmo_delete_btn.Visible = true;
			localtrvl_btnSave.Visible = false;
			trvldeatils_btnSave.Visible = false;
			//OldPayNo.Style.Add("display", "none");
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
				txtOldPaymentRequestNo.Text = DTPoWoNumber.Tables[0].Rows[0]["Ref_PaymentReqNo"].ToString();
				txtAccountPaidAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["Amt_paid_Account"].ToString();
				txtAccountAmtBal.Text = DTPoWoNumber.Tables[0].Rows[0]["AccountBalAmt"].ToString();
				txtApprovalRemark.Text = DTPoWoNumber.Tables[0].Rows[0]["Remarks"].ToString();
				txtAccountPaidAmt.Enabled = false;
				txtAccountAmtBal.Enabled = false;
				txtApprovalRemark.Enabled = false;
				//hflStatusID.Value = DTPoWoNumber.Tables[0].Rows[0]["Status_id"].ToString();
				//if (Convert.ToInt32(DTPoWoNumber.Tables[0].Rows[0]["Status_id"]) == 2)
				//{
				//	Account1.Visible = true;
				//	Account2.Visible = true;
				//	Account3.Visible = true;
				//	txtAccountPaidAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["Amt_paid_Account"].ToString();
				//	txtAccountAmtBal.Text = DTPoWoNumber.Tables[0].Rows[0]["AccountBalAmt"].ToString();
				//	txtAccountPaidAmt.Enabled = false;
				//	txtAccountAmtBal.Enabled = false;
				//}
				//else
				//{
				//	Account1.Visible = false;
				//	Account2.Visible = false;
				//	Account3.Visible = false;
				//}
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
}