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

public partial class procs_VSCB_CreatePartialPaymentRequest : System.Web.UI.Page
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
						CheckPayment_ApprovalStatus_Submit();
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
			DTPoWoNumber = spm.GetVSCB_MyPaymentDetails("POWOPartialPaymentRequest", Convert.ToString(hdnEmpCode.Value), POID, Payment_ID);
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
                txtPoPaidAmt_WithOutDT.Text = DTPoWoNumber.Tables[0].Rows[0]["POPiadAmount_withoutDT"].ToString();
                txtPoWOPaidBalAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["PO_Bal_Amt"].ToString();
				txtPoWOAmtWIthoutTax.Text = DTPoWoNumber.Tables[0].Rows[0]["POWO_T_BaseAmt"].ToString();
				txtPODirectTaxAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["DirectTaxCollection_Amt"].ToString();
				txtProjectName.Text = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
                txtTallyCode_display.Text = DTPoWoNumber.Tables[0].Rows[0]["TallyCode"].ToString();
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
				lblheading.Text = "Partial Payment Request View - " + DTPoWoNumber.Tables[3].Rows[0]["PaymentReqNo"].ToString();
			}
			
			GrdFileUpload.DataSource = null;
			GrdFileUpload.DataBind();
			if (DTPoWoNumber.Tables[4].Rows.Count > 0)
			{
				GrdFileUpload.DataSource = DTPoWoNumber.Tables[4];
				GrdFileUpload.DataBind();
                spnPaymentSupportingFiles.Visible = true;
			}
			if (DTPoWoNumber.Tables[5].Rows.Count > 0)
			{
				SPPayHist.Visible = true;
				GrdInvoiceHistDetails.DataSource = DTPoWoNumber.Tables[5];
				GrdInvoiceHistDetails.DataBind();
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

    protected void txtMilestoneInvoiceAmt_TextChanged(object sender, EventArgs e)
    {
        double dMilesStoneAmt_forInvoice = 0;
        double dAmtPaidWithTax_forInvoice = 0;
        decimal Result = 0;
        string strmsgblnk  = "";
        string strmsgblnk1 = "";
        for (int i = 0; i <= GridViewAccountpaidbyAmount.Rows.Count - 1; i++)
        {
            GridViewRow ro = GridViewAccountpaidbyAmount.Rows[i];
            TextBox txtMSInvoiceAmt = (TextBox)ro.FindControl("txtMilestoneInvoiceAmt");
            Label lblAmounttobepaidd = (Label)ro.FindControl("lblAmounttobepaidd");
            if (Convert.ToString(txtMSInvoiceAmt.Text).Trim() != "" || Convert.ToString(txtMSInvoiceAmt.Text).Trim() == "0")
            {
                strmsgblnk1 = "msg";
                if (Convert.ToDecimal(txtMSInvoiceAmt.Text) <= Convert.ToDecimal(lblAmounttobepaidd.Text))
                {
                    dMilesStoneAmt_forInvoice += Convert.ToDouble(txtMSInvoiceAmt.Text);
                    dAmtPaidWithTax_forInvoice += Convert.ToDouble(lblAmounttobepaidd.Text);
                    txtAccountPaidAmt.Text = Convert.ToDouble(dMilesStoneAmt_forInvoice).ToString();
                    txtAccountPaidAmt.Text = Math.Round(Convert.ToDecimal(txtAccountPaidAmt.Text), 2).ToString();

                    lblmessage.Text = "";
                }
                else
                {
                    txtMSInvoiceAmt.Text = "";
                    txtAccountPaidAmt.Text = "";
                    strmsgblnk = "msg";
                }
            }
            else
            {
                if (strmsgblnk1 == "")
                {
                    txtAccountPaidAmt.Text = "";
                }
            }
        }
        
        if (strmsgblnk != "")
        {
            lblmessage.Text = "Amount paid by account exceed Balance Amount! ";
            return;
        }
        if (Convert.ToString(txtAccountPaidAmt.Text) != "")
        {
            txtAccountAmtBal.Text = Convert.ToString(Convert.ToDecimal(txtAmountPaidWithTax.Text) - Convert.ToDecimal(txtAccountPaidAmt.Text));
        }
        else
        {
            txtAccountAmtBal.Text = Convert.ToString(Convert.ToDecimal(txtAmountPaidWithTax.Text));

        }

    }

    public void CheckAccountApprovalEmp()
	{
		try
		{
			hdnExtraAPP.Value = "";
			dtextraApp = spm.Get_Payment_AccountApprovalEmp("Get_Account_mail_Details", Convert.ToString(Session["Empcode"]).Trim(), Convert.ToInt32(hdnPayment_ID.Value));
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
			int Payment_ID = 0, Payment_Status_ID = 0, InvoiceID=0, MstoneID=0;
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
			MstoneID = Convert.ToString(hdnMstoneID.Value).Trim() != "" ? Convert.ToInt32(hdnMstoneID.Value) : 0;

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
				DTInsertPayment = spm.dtInsert_Payment_Request("PAYMNETPARTIALREQUESTINSERT", EmpCode, InvoiceID, MstoneID, Payment_ID, txtPaymentRequestNo.Text, Convert.ToDecimal(txtAmountPaidWithTax.Text), PaymentDate, 2, Payment_Status_ID, Convert.ToDecimal(txtAccountPaidAmt.Text), Convert.ToDecimal(PayBalAmt), txtOldPaymentRequestNo.Text.Trim(), txtApprovalRemark.Text);

                for (int i = 0; i <= GridViewAccountpaidbyAmount.Rows.Count - 1; i++)
                {
                    GridViewRow ro = GridViewAccountpaidbyAmount.Rows[i];
                    TextBox txtMSInvoiceAmt = (TextBox)ro.FindControl("txtMilestoneInvoiceAmt");
                    Label lblAmounttobepaidd = (Label)ro.FindControl("lblAmounttobepaidd");
                    Label lblAmounttoberequested = (Label)ro.FindControl("lblAmounttoberequested");
                    string HDmilestoneid = "";
                  //  if (Convert.ToString(txtMSInvoiceAmt.Text).Trim() != "")
                   // {
                        HDmilestoneid = Convert.ToString(GridViewAccountpaidbyAmount.DataKeys[ro.RowIndex].Values[0]).Trim();

                       if (txtMSInvoiceAmt.Text.Trim() == "")
                       {
                          txtMSInvoiceAmt.Text = "0";
                       }
                    


                        SqlParameter[] spars = new SqlParameter[8];
                        spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
                        spars[0].Value = "PartialPaymentRequestMSAmountinsert";
                        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
                        spars[1].Value = EmpCode;
                        spars[2] = new SqlParameter("@InvoiceID", SqlDbType.VarChar);
                        spars[2].Value = InvoiceID;
                        spars[3] = new SqlParameter("@MstoneID", SqlDbType.VarChar);
                        spars[3].Value = HDmilestoneid;
                        spars[4] = new SqlParameter("@Amt_paid_Account", SqlDbType.VarChar);
                        spars[4].Value = Convert.ToDecimal(txtMSInvoiceAmt.Text.Trim());
                        spars[5] = new SqlParameter("@BalanceAmt", SqlDbType.VarChar);
                        spars[5].Value = Convert.ToDecimal(lblAmounttobepaidd.Text) - Convert.ToDecimal(txtMSInvoiceAmt.Text);
                        spars[6] = new SqlParameter("@Payment_ID", SqlDbType.VarChar);
                        spars[6].Value = DTInsertPayment.Rows[0]["Payment_ID"].ToString();
                        spars[7] = new SqlParameter("@TobePaidAmtWithtax", SqlDbType.VarChar);
                        spars[7].Value = Convert.ToDecimal(lblAmounttoberequested.Text.Trim());

                        DataSet dsApprovalStatusReport = spm.getDatasetList(spars, "sp_VSCB_CreatePOWO_Users");
                  //  }
                }

            }
			String strPatmentURL = "", strapproverlist = "";
		    var Tbody = "";
			GetOldPaymentRequestNo(txtOldPaymentRequestNo.Text);
			string strsubject = " Partial Payment request of “" + txtPaymentRequestNo.Text + "”  For “" + txtPOWONumber.Text + " ”";
			if (hdnExtraAPP.Value.ToString().Trim() == "Account")		
			{
				strPatmentURL = ""; 
				GetInvoiceTDSAmt();
				string CostCenter = hdnCostCentre.Value;
				Tbody = "This is to inform you that Account has below Partial Payment Paid.";
				//spm.send_mailto_VSCB_Payment_Request_Partial_Payment(hdnEmpCodePrveName.Value, hdnLoginEmpEmail.Value, hdnEmpCodePrveEmailID.Value, strsubject, Tbody, "", strPatmentURL, strapproverlist, txtPOWONumber.Text, txtPoWoTitle.Text, txtPoWoDate.Text, txtPoWoType.Text, txtVendorName.Text, txtPoWoAmtWithTaxes.Text, txtPoWOPaidAmt.Text, txtPoWOPaidBalAmt.Text, txtProjectName.Text, txtInvoiceNo.Text, txtInvoiceAmount.Text, txtInvoiceBalAmt.Text, txtPaymentRequestNo.Text, txtRequestDate.Text, txtAmountPaidWithTax.Text, txtAccountPaidAmt.Text, PayBalAmt.ToString(), txtOldPaymentRequestNo.Text,CostCenter, hdnDirectTax_Type.Value, hdnDirectTax_Percentage.Value, hdnDirectTax_Amount.Value, hdnPayable_Amt_Invoice.Value);
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
	protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
	{
		
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
	protected void lnkdelete_Click(object sender, ImageClickEventArgs e)
	{

	}

	protected void accmo_delete_btn_Click(object sender, EventArgs e)
	{
		PaymentDetails();
		DivCreateInvoice.Visible = false;
		OldPayNo.Style.Add("display", "inline-block;");
		txtAmountPaidWithTax.Text = "";
		txtAccountPaidAmt.Text = "";
		txtAccountAmtBal.Text = "";
		txtOldPaymentRequestNo.Text = "";
		txtRequestDate.Text = "";
		txtPaymentRequestNo.Text = "";
		txtApprovalRemark.Text = "";
		Account3.Visible = false;
		localtrvl_btnSave.Visible = true;
		txtAccountPaidAmt.Enabled = true;
		txtApprovalRemark.Enabled = true;
		txtTotalPaymentAmt.Text = "";
		txtPartialPaymentBalAmt.Text = "";
		PayBal1.Visible = true;
		PayBal2.Visible = true;
		PayBal3.Visible = true;
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
			DivCreateInvoice.Visible = true;
			txtOldPaymentRequestNo.Text = "";
			PayBal1.Visible = false;
			PayBal2.Visible = false;
			PayBal3.Visible = false;
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
				hflStatusID.Value = DTPoWoNumber.Tables[0].Rows[0]["Status_id"].ToString();
				//HDNPartialPaymnetBal.Value = DTPoWoNumber.Tables[0].Rows[0]["AccountBalAmt"].ToString();
				txtAccountPaidAmt.Enabled = false;
				txtAccountAmtBal.Enabled = false;
				txtApprovalRemark.Enabled = false;
				//if (Convert.ToInt32(DTPoWoNumber.Tables[0].Rows[0]["Status_id"]) == 2)
				//{
				//	Account1.Visible = true;
				//	Account2.Visible = true;
				//    Account3.Visible = true;	
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
                spnPaymentSupportingFiles.Visible = true;
			}
		}
		catch (Exception)
		{

			throw;
		}
	}



	protected void lnkView_Click(object sender, EventArgs e)
	{
		try
		{	
		int Payment_ID = 0;
		LinkButton btn = (LinkButton)sender;
		GridViewRow row = (GridViewRow)btn.NamingContainer;
		hdnInvoiceID.Value = Convert.ToString(GrdPartialPayment.DataKeys[row.RowIndex].Values[1]).Trim();
		Payment_ID = Convert.ToInt32(GrdPartialPayment.DataKeys[row.RowIndex].Values[0]);
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
			PayBal1.Visible = true;
			PayBal2.Visible = true;
			PayBal3.Visible = true;
			localtrvl_btnSave.Visible = true;
			InvoiceID = Convert.ToString(hdnInvoiceID.Value).Trim() != "" ? Convert.ToInt32(hdnInvoiceID.Value) : 0;
			//MstoneID = Convert.ToString(hdnMstoneID.Value).Trim() != "" ? Convert.ToInt32(hdnMstoneID.Value) : 0;
			DTCreateInvoice = spm.dtShowInvoiceDetailsForPayment("POWOPartialPaymentCreate", Convert.ToString(hdnEmpCode.Value), InvoiceID, Payment_ID);
			if (DTCreateInvoice.Rows.Count > 0)
			{
				DivCreateInvoice.Visible = true;
				txtInvoiceNo.Text = DTCreateInvoice.Rows[0]["InvoiceNo"].ToString();
				hdnInvoiceID.Value = DTCreateInvoice.Rows[0]["InvoiceID"].ToString();
				hdnMstoneID.Value = DTCreateInvoice.Rows[0]["MstoneID"].ToString();
				txtInvoiceAmount.Text = DTCreateInvoice.Rows[0]["AmtWithTax"].ToString();
				txtInvoiceBalAmt.Text = DTCreateInvoice.Rows[0]["BalanceAmt"].ToString();
				txtPaymentRequestNo.Text = DTCreateInvoice.Rows[0]["PaymentReqNo"].ToString();
				txtOldPaymentRequestNo.Text = DTCreateInvoice.Rows[0]["OldPaymentReqNO"].ToString();
				txtAmountPaidWithTax.Text = DTCreateInvoice.Rows[0]["RemainingBalAmt"].ToString();
			    txtAccountAmtBal.Text = DTCreateInvoice.Rows[0]["RemainingBalAmt"].ToString();
				HDNPartialPaymnetBal.Value = DTCreateInvoice.Rows[0]["RemainingBalAmt"].ToString();
				txtTotalPaymentAmt.Text = DTCreateInvoice.Rows[0]["TobePaidAmtWithtax"].ToString();
				txtPartialPaymentBalAmt.Text = DTCreateInvoice.Rows[0]["RemainingBalAmt"].ToString();
                txtPaymentRequestAmount.Text = DTCreateInvoice.Rows[0]["paymentRequestForAmt"].ToString(); //DirectTax_Amount
                txtInvoiceTDSAmt.Text = DTCreateInvoice.Rows[0]["DirectTax_Amount"].ToString();
                txtInvoicePaidAmount.Text = DTCreateInvoice.Rows[0]["AccountPaidAmt"].ToString(); //InvoicePaidAmt;
                 txtRequestDate.Text = DateTime.Now.ToString("dd-MM-yyyy");

                    SqlParameter[] spars = new SqlParameter[3];
                    spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
                    spars[0].Value = "POWOPartialPaymentCreate";
                    spars[1] = new SqlParameter("@BalanceAmt", SqlDbType.VarChar);
                if (txtAccountAmtBal.Text == "")
                {
                    spars[1].Value = 0;
                }
                else
                {
                    spars[1].Value = txtAccountAmtBal.Text;
                }
                    
                    spars[2] = new SqlParameter("@PaymentReqNos", SqlDbType.VarChar);
                    spars[2].Value = txtOldPaymentRequestNo.Text;
                    

                DataSet dsApprovalStatusReport = spm.getDatasetList(spars, "sp_VSCB_CreatePOWO_Users");
                if (dsApprovalStatusReport.Tables[2].Rows.Count > 0)
                {
                    GridViewAccountpaidbyAmount.DataSource = dsApprovalStatusReport.Tables[2];
                    GridViewAccountpaidbyAmount.DataBind();
                }
                else
                {
                    GridViewAccountpaidbyAmount.DataSource = dsApprovalStatusReport.Tables[1];
                    GridViewAccountpaidbyAmount.DataBind();
                } 
                    foreach (GridViewRow gvr in GridViewAccountpaidbyAmount.Rows)
                    {
                        TextBox txtMilestoneInvoiceAmt = (TextBox)gvr.FindControl("txtMilestoneInvoiceAmt");
                        Label lblAmounttobepaidd = (Label)gvr.FindControl("lblAmounttobepaidd");
                        if (lblAmounttobepaidd.Text == "0.00" || lblAmounttobepaidd.Text == "0")
                        {
                            txtMilestoneInvoiceAmt.Enabled = false;
                        }
                    }
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