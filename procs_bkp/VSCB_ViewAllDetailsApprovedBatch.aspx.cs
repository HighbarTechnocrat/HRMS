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
public partial class procs_VSCB_ViewAllDetailsApprovedBatch : System.Web.UI.Page
{
	#region Creative_Default_methods
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

	protected void lnkcont_Click(object sender, EventArgs e)
	{
		Response.Redirect(ReturnUrl("sitepathmain") + "procs/vscb_index.aspx");
	}

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
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/VSCB_Index.aspx");
			}
			else
			{
				Page.SmartNavigation = true;
				if (!Page.IsPostBack)
				{
					FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VendorPaymentDocpath"]).Trim());
					FilePathInvoice.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBInvoicefiles"]).Trim());
					hdnSingPOCopyFilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBSignPOWOfiles"]).Trim());
					
					hdnLoginUserName.Value = Convert.ToString(Session["emp_loginName"]).Trim();
					hdnLoginEmpEmail.Value = Convert.ToString(Session["LoginEmpmail"]).Trim();
					if (Request.QueryString.Count > 0)
					{
						hdnPayment_ID.Value = Convert.ToString(Request.QueryString[0]).Trim();
						hdnPOID.Value = Convert.ToString(Request.QueryString[1]).Trim();
						hdnBatchId.Value = Convert.ToString(Request.QueryString[2]).Trim();
						hdnInvoiceID.Value= Convert.ToString(Request.QueryString[3]).Trim();
						InvoiceDetails();
						get_Approver_List_Invoice();
						get_BatchRequest_Approver();
					}

					hdnApprovedPO_FileName.Value = Convert.ToString(Regex.Replace(Convert.ToString(txtPOWONumber.Text), @"[^0-9a-zA-Z\._]", "_")).Trim() + ".pdf";
					hdnApprovedPO_FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBApprovedPOWOfiles"]).Trim());
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}

	#region PageMethods
	private void InvoiceDetails()
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
				txtPoWOAmtWIthoutTax.Text = DTPoWoNumber.Tables[0].Rows[0]["POWO_T_BaseAmt"].ToString();
				txtPoWOPaidAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["PO_Paid_Amt"].ToString();
				txtPoPaidAmt_WithOutDT.Text = DTPoWoNumber.Tables[0].Rows[0]["POPiadAmount_withoutDT"].ToString();
				txtPoWOPaidBalAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["PO_Bal_Amt"].ToString();
				txtPODirectTaxAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["DirectTaxCollection_Amt"].ToString();
				txtProjectName.Text = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
				txtDepartment.Text = DTPoWoNumber.Tables[0].Rows[0]["Department"].ToString();
				hdnCostCentre.Value = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
				hdnTallyCode.Value = DTPoWoNumber.Tables[0].Rows[0]["Project_Name"].ToString();
				hdnDept_Name.Value = DTPoWoNumber.Tables[0].Rows[0]["Department"].ToString();

				txtCurrency.Text = Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["CurName"]);
				txtPOWOSettelmentAmt.Text = Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["PO_SettelmentAmt"]).Trim();
				hdnPOTypeID.Value= Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["POTypeID"]);

				if (Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["powo_signCopy"]).Trim() != "")
				{
					lnkfile_PO.Text = Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["powo_signCopy"]).Trim();
					spnPOWOSignCopy.Visible = true;
					lnkfile_PO.Visible = true;
					btnCorrection.Visible = false;
				}
                else
                {
					btnCorrection.Visible = true;
					lnkfile_PO.Visible = false;
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
			if (DTPoWoNumber.Tables[3].Rows.Count > 0)
			{
				//DivCreateInvoice.Visible = true;
				txtInvoiceNo.Text = DTPoWoNumber.Tables[3].Rows[0]["InvoiceNo"].ToString();
				hdnInvoiceID.Value = DTPoWoNumber.Tables[3].Rows[0]["InvoiceID"].ToString();
				txtInvoiceAmount.Text = DTPoWoNumber.Tables[3].Rows[0]["AmtWithTax"].ToString(); // //AmtWithTax Payable_Amt_With_Tax
				txtInvoiceBalAmt.Text = DTPoWoNumber.Tables[3].Rows[0]["BalanceAmt"].ToString();
				//txtVendorName.Text = DTCreateInvoice.Rows[0]["Status_id"].ToString();
				//txtGSTINNO.Text = DTCreateInvoice.Rows[0]["PaymentStatusID"].ToString();
				txtPaymentRequestNo.Text = DTPoWoNumber.Tables[3].Rows[0]["PaymentReqNo"].ToString();
				txtRequestDate.Text = DTPoWoNumber.Tables[3].Rows[0]["PaymentReqDate"].ToString();
				txtAmountPaidWithTax.Text = DTPoWoNumber.Tables[3].Rows[0]["TobePaidAmtWithtax"].ToString();
				hdnEmpCodePrve.Value = DTPoWoNumber.Tables[3].Rows[0]["Emp_Code"].ToString();
				txtAmountPaidWithTax.Text = DTPoWoNumber.Tables[3].Rows[0]["TobePaidAmtWithtax"].ToString();
				lnkfile_Invoice.Text = DTPoWoNumber.Tables[3].Rows[0]["Invoice_File"].ToString();

				txtPaymentRequestAmount.Text = DTPoWoNumber.Tables[3].Rows[0]["paymentRequestForAmt"].ToString(); //DirectTax_Amount
				txtInvoiceTDSAmt.Text = DTPoWoNumber.Tables[3].Rows[0]["DirectTax_Amount"].ToString();
				txtInvoicePaidAmount.Text = DTPoWoNumber.Tables[3].Rows[0]["AccountPaidAmt"].ToString(); //InvoicePaidAmt;

				//lblheading.Text = "Payment Request View - " + txtPaymentRequestNo.Text + ", Payment Status - " + DTPoWoNumber.Tables[3].Rows[0]["PyamentStatus"].ToString();
				
				txtAccountPaidAmt.Text = DTPoWoNumber.Tables[3].Rows[0]["Amt_paid_Account"].ToString();
				txtAccountAmtBal.Text = DTPoWoNumber.Tables[3].Rows[0]["AccountBalAmt"].ToString();
				txtAccountPaidAmt.Enabled = false;
				txtAccountAmtBal.Enabled = false;
				getInvoiceUploadedFiles();
			}
			if (DTPoWoNumber.Tables[4].Rows.Count > 0)
			{
				spnPaymentSupportingFile.Visible = true;
				GrdFileUpload.DataSource = DTPoWoNumber.Tables[4];
				GrdFileUpload.DataBind();
			}

			getApproverlist(hdnEmpCode.Value, Payment_ID, hdnDept_Name.Value, hdnCostCentre.Value);

		}
		catch (Exception)
		{

			throw;
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
			spnSupportinFiles1.Visible = true;
			spnSupportinFiles2.Visible = true;

			liInvoiceUploadFile.Visible = true;
			liInvoiceUploadFile2.Visible = true;
			liInvoiceUploadFile3.Visible = true;
		}
	}
	public void get_Approver_List_Invoice()
	{
		DgvApprover.DataSource = null;
		DgvApprover.DataBind();

		DataSet dsMilestone = new DataSet();
		dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceID.Value), hdnEmpCode.Value, Convert.ToString(txtDepartment.Text).Trim(), Convert.ToString(hdnPOTypeID.Value).Trim());

		if (dsMilestone.Tables[0].Rows.Count > 0)
		{
			DgvApprover.DataSource = dsMilestone.Tables[0];
			DgvApprover.DataBind();
		}
	}
	private void getApproverlist(string EmpCode, int Payment_ID, string DeptnName, string TallyCode)
	{
		DataTable dtapprover = new DataTable();
		dtapprover = spm.Get_Payment_Request_ApproverList(EmpCode, Payment_ID, DeptnName, TallyCode, Convert.ToString(txtPoWoType.Text).Trim());
		DgvApproverPayments.DataSource = null;
		DgvApproverPayments.DataBind();
		if (dtapprover.Rows.Count > 0)
		{
			DgvApproverPayments.DataSource = dtapprover;
			DgvApproverPayments.DataBind();
		}
	}
	#endregion

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
	protected void GrdInvoiceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		try
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				string lsDataKeyValue = GrdInvoiceDetails.DataKeys[e.Row.RowIndex].Values[1].ToString();
				if (Convert.ToString(lsDataKeyValue).Trim() == "0")
				{
					e.Row.Visible = false;
				}
			}
		}
		catch (Exception)
		{
			throw;
		}
	}

	private void get_BatchRequest_Approver()
	{
		try
		{
			DataSet dsList = new DataSet();
			SqlParameter[] spars = new SqlParameter[3];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "get_BatchReq_Details_Appr";

			spars[1] = new SqlParameter("@POWOID", SqlDbType.BigInt);
			spars[1].Value = Convert.ToDouble(hdnBatchId.Value);

			spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
			spars[2].Value = Convert.ToString(hdnEmpCode.Value).Trim();

			dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");


			if (dsList.Tables[0].Rows.Count > 0)
			{
				txtbatchCreateDate.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_Date"]).Trim();
				txtbatchCreatedBy.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_Created_By"]).Trim();
				txtbatchNo.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_No"]).Trim();
				txtbatchNoOfRequest.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_No_Requests"]).Trim();
				txtbatchTotalPayment.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_Total_Payament"]).Trim();

				txtBank_name.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Name"]).Trim();
				txtBankRefNo.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Ref_no"]).Trim();
				txtBankRef_Link.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Ref_Link"]).Trim();
				//lnkBank.InnerText = "Please click here for Bank Login";
				//lnkBank.HRef = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Ref_Link"]).Trim();
				txtBankRefDate.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Ref_Date"]).Trim();
				if (Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Ref_File"]).Trim() != "")
				{
					lnkfile_Invoice.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Ref_File"]).Trim();
					lnkfile_Invoice.Visible = true;
					spnInvoicefile.Visible = true;
				}

			}
			if (dsList.Tables[3].Rows.Count > 0)
			{
				DgvApproverBatch.DataSource = dsList.Tables[3];
				DgvApproverBatch.DataBind();
			}

		}
		catch (Exception ex)
		{

		}
	}
}