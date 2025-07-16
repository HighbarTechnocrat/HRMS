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

public partial class procs_VSCB_Advance_Pay_Approval : System.Web.UI.Page
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
	#region PageEvents
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
						hdnInboxType.Value = Convert.ToString(Request.QueryString[2]).Trim();
						get_DirectTaxSectionsList();
						if (Convert.ToString(hdnInboxType.Value).Trim() == "Pending")
						{
							CheckPayment_ApprovalStatus_Submit();
							GetPayment_CurrentApprID();
							btnRecBack.Visible = true;
							btnRecBack1.Visible = false;
						}
						else
						{
							Set_Control();
							btnRecBack1.Visible = true;
							btnRecBack.Visible = false;
						}
						
						PaymentDetails();
						
						CheckAccountApprovalEmp();

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

	#endregion

	#region PageMethod

	public void get_DirectTaxSectionsList()
	{

		DataSet dsProjectsVendors = new DataSet();
		SqlParameter[] spars = new SqlParameter[1];
		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "get_TDS_Sections";
		dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
		if (dsProjectsVendors.Tables[0].Rows.Count > 0)
		{
			lstDirectTaxSections_ACC.DataSource = dsProjectsVendors.Tables[0];
			lstDirectTaxSections_ACC.DataTextField = "TDS_Description";
			lstDirectTaxSections_ACC.DataValueField = "TDS_Descript_ID";
			lstDirectTaxSections_ACC.DataBind();
			lstDirectTaxSections_ACC.Items.Insert(0, new ListItem("Select Direct Taxt Sections", "0"));
		}
	}
	private void Set_Control()
	{
		txtApprovalRemark.Visible = false;
		localtrvl_btnSave.Visible = false;
		trvldeatils_btnSave.Visible = false;
		accmo_delete_btn.Visible = false;
		lstDirectTaxSections_ACC.Enabled = false;
		ST.Visible = false;
	}
	private void CheckPayment_ApprovalStatus_Submit()
	{
		try
		{
			SqlParameter[] spars = new SqlParameter[3];
			spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
			spars[0].Value = "Check_Advance_PayAppr_Status";
			spars[1] = new SqlParameter("@Payment_ID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdnPayment_ID.Value);
			spars[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[2].Value = Convert.ToString(hdnEmpCode.Value).Trim();
			dtPaymentStatus = spm.getMobileRemDataList(spars, "sp_VSCB_CreatePOWO_Users");
			if (dtPaymentStatus.Rows.Count == 0)
			{
				Response.Redirect("~/procs/VSCB_Inbox_ADV_Payment.aspx?Type=Pending");
			}
			if (dtPaymentStatus.Rows.Count > 0)
			{
				if (Convert.ToString(dtPaymentStatus.Rows[0]["pvappstatus"]) != "Pending")
				{
					Response.Redirect("~/procs/VSCB_Inbox_ADV_Payment.aspx?Type=Pending");
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
		dtCApprID = spm.GetCurrent_Payment_ApprID("Check_Current_Adv_PayAppr_Status", Convert.ToInt32(hdnPayment_ID.Value), hdnEmpCode.Value);
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
			dtextraApp = spm.Get_Payment_AccountApprovalEmp("Get_Account_ADV_Pay_Details", Convert.ToString(Session["Empcode"]).Trim(), Convert.ToInt32(hdnPayment_ID.Value));
			if (dtextraApp.Rows.Count > 0)
			{
				hdnExtraAPP.Value = (string)dtextraApp.Rows[0]["app_remarks"].ToString().Trim();
				//hdnExtraAPPID.Value = (string)dtextraApp.Rows[0]["Appr_id"].ToString().Trim();
				if (hdnExtraAPP.Value.ToString().Trim() == "Account")
				{
					localtrvl_btnSave.Text = "Verify";
					localtrvl_btnSave.ToolTip = "Verify";
					
					txtAccountAmtBal.Text = "0.00";
					Account1.Visible = true;
					 
					Account3.Visible = true;
					Account4.Visible = true;
					Account5.Visible = true;
					Account6.Visible = true;
				}
			}
			else
			{
				Account1.Visible = false;
				Account2.Visible = false;
				Account3.Visible = false;
				Account4.Visible = false;
				Account5.Visible = false;
				Account6.Visible = false;
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
			throw;
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
			DTPoWoNumber = spm.GetVSCB_MyPaymentDetails("POWOMy_ADV_Pay_RequestDeatils", Convert.ToString(hdnEmpCode.Value), POID, Payment_ID);
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

				txtPoWOAmtWIthoutTax.Text = DTPoWoNumber.Tables[0].Rows[0]["POWO_T_BaseAmt"].ToString();
				txtPoWOPaidBalAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["PO_Bal_Amt"].ToString();
				txtPODirectTaxAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["DirectTaxCollection_Amt"].ToString();
				txtProjectName.Text = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
				txtDepartment.Text = DTPoWoNumber.Tables[0].Rows[0]["Department"].ToString();
				hdnCostCentre.Value = DTPoWoNumber.Tables[0].Rows[0]["CostCentre"].ToString();
				txtTallyCode_display.Text = DTPoWoNumber.Tables[0].Rows[0]["Tallycode"].ToString();
				//hdnTallyCode.Value = DTPoWoNumber.Tables[0].Rows[0]["Project_Name"].ToString();
				hdnDept_Name.Value = DTPoWoNumber.Tables[0].Rows[0]["Department"].ToString();
				txtCurrency.Text = Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["CurName"]);
				txtPOWOSettelmentAmt.Text = Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["PO_SettelmentAmt"]).Trim();

                txtHPOType.Text = Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["HPOTypeName"]).Trim();               
                if (Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["PO_Security_Deposit_Amt"]).Trim() != "")
                {
                    txtSecurity_DepositAmt.Text = Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["PO_Security_Deposit_Amt"]).Trim();
                    if (Convert.ToDecimal(DTPoWoNumber.Tables[0].Rows[0]["PO_Security_Deposit_Amt"]) > 0)
                    {
                        divScurity_Diposit.Visible = true;
                    }
                }

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
			//if (DTPoWoNumber.Tables[6].Rows.Count > 0)
			//{
			//	GrdInvoiceDetails.DataSource = DTPoWoNumber.Tables[6];
			//	GrdInvoiceDetails.DataBind();
			//}
			if (DTPoWoNumber.Tables[2].Rows.Count > 0)
			{
				
				txtPaymentRequestNo.Text = DTPoWoNumber.Tables[2].Rows[0]["PaymentReqNo"].ToString();
				txtRequestDate.Text = DTPoWoNumber.Tables[2].Rows[0]["PaymentReqDate"].ToString();
				txtAmountPaidWithTax.Text = DTPoWoNumber.Tables[2].Rows[0]["TobePaidAmtWithtax"].ToString();
				hdnEmpCodePrve.Value = DTPoWoNumber.Tables[2].Rows[0]["Emp_Code"].ToString();
				txtPaymentType.Text= DTPoWoNumber.Tables[2].Rows[0]["TypeName"].ToString();
				txtRemark.Text = DTPoWoNumber.Tables[2].Rows[0]["CreatedRemarks"].ToString();
                hdnPaymentTypeId.Value = Convert.ToString(DTPoWoNumber.Tables[2].Rows[0]["AdvancePayTypeID"]).Trim();
                //txtPaymentRequestAmount.Text = DTPoWoNumber.Tables[2].Rows[0]["paymentRequestForAmt"].ToString(); //DirectTax_Amount
                txtAccountPaidAmt.Text = txtAmountPaidWithTax.Text;
                //txtInvoiceTDSAmt.Text = DTPoWoNumber.Tables[2].Rows[0]["DirectTax_Amount"].ToString();

                txtAmountPaidWithoutTax.Text = DTPoWoNumber.Tables[2].Rows[0]["TobePaidAmtWithOuttax"].ToString();

                liGSTAmt_1.Visible = false;
                liGSTAmt_2.Visible = false;
                liGSTAmt_3.Visible = false;

                liAdvAmtWithGSt_1.Visible = false;
                liAdvAmtWithGSt_2.Visible = false;
                liAdvAmtWithGSt_3.Visible = false;

                if (Convert.ToString(DTPoWoNumber.Tables[2].Rows[0]["CGST_Amt"]).Trim() != "")
                {
                    txtCGST_Amt.Text = Convert.ToString(DTPoWoNumber.Tables[2].Rows[0]["CGST_Amt"]).Trim();
                }

                if (Convert.ToString(DTPoWoNumber.Tables[2].Rows[0]["SGST_Amt"]).Trim() != "")
                {
                    txtSGST_Amt.Text = Convert.ToString(DTPoWoNumber.Tables[2].Rows[0]["SGST_Amt"]).Trim();
                }

                if (Convert.ToString(DTPoWoNumber.Tables[2].Rows[0]["IGST_Amt"]).Trim() != "")
                {
                    txtIGST_Amt.Text = Convert.ToString(DTPoWoNumber.Tables[2].Rows[0]["IGST_Amt"]).Trim();
                }

                if (Convert.ToString(DTPoWoNumber.Tables[2].Rows[0]["CGST_Amt"]).Trim() != "" || Convert.ToString(DTPoWoNumber.Tables[2].Rows[0]["SGST_Amt"]).Trim() != "" || Convert.ToString(DTPoWoNumber.Tables[2].Rows[0]["IGST_Amt"]).Trim() != "")
                {
                    liGSTAmt_1.Visible = true;
                    liGSTAmt_2.Visible = true;
                    liGSTAmt_3.Visible = true;

                    liAdvAmtWithGSt_1.Visible = true;
                    liAdvAmtWithGSt_2.Visible = true;
                    liAdvAmtWithGSt_3.Visible = true;

                }

              


                if (DTPoWoNumber.Tables[2].Rows[0]["Status_id"].ToString() == "1" && Convert.ToString(txtPaymentType.Text)== "Security Deposit")
				{
					lstDirectTaxSections_ACC.SelectedValue = "1";
					lstDirectTaxSections_ACC.Enabled = false;
					txt_Tax_Percentage.Text = "0.00";
					txt_Direct_Tax_Amt.Text = "0.00";
				}
				else
				{
					lstDirectTaxSections_ACC.SelectedValue = DTPoWoNumber.Tables[2].Rows[0]["TDS_Descript_ID"].ToString(); //InvoicePaidAmt;
					txt_Tax_Percentage.Text = DTPoWoNumber.Tables[2].Rows[0]["DirectTax_Percentage"].ToString();
					txt_Direct_Tax_Amt.Text = DTPoWoNumber.Tables[2].Rows[0]["DirectTax_Amount"].ToString();
				}

				if (DTPoWoNumber.Tables[2].Rows[0]["IsLDC_Applicable"].ToString() == "1")
				{
					chkLDC_Applicable_ACC.Checked = true;
					chkLDC_Applicable_ACC.Enabled = false;
					lstDirectTaxSections_ACC.Enabled = false;
				}
				if (DTPoWoNumber.Tables[2].Rows[0]["Status_id"].ToString() == "2")
				{
					txtAccountPaidAmt.Text = DTPoWoNumber.Tables[2].Rows[0]["Amt_paid_Account"].ToString();
					lstDirectTaxSections_ACC.Enabled = false;
				}
                
                    lblheading.Text = "Approval Advance Payment Request - " + txtPaymentRequestNo.Text;// + DTPoWoNumber.Tables[3].Rows[0]["PyamentStatus"].ToString();

                if (Convert.ToString(hdnPaymentTypeId.Value).Trim() == "2")
                    lblheading.Text = "Approval Security Deposit Payment Request - " + txtPaymentRequestNo.Text;// + DTPoWoNumber.Tables[3].Rows[0]["PyamentStatus"].ToString()

                //getInvoiceUploadedFiles();

            }
			if (DTPoWoNumber.Tables[3].Rows.Count > 0)
			{
				GrdFileUpload.DataSource = DTPoWoNumber.Tables[3];
				GrdFileUpload.DataBind();
				spnSupportingFiles.Visible = true;
				liPaymentSupportingFiles_1.Visible = true;
				liPaymentSupportingFiles_2.Visible = true;
				liPaymentSupportingFiles_2.Visible = true;
				liPaymentSupportingFiles_4.Visible = true;

			}

            #region Payment Histroy not required to show
            /*if (DTPoWoNumber.Tables[4].Rows.Count > 0)
			{
				SPPayHist.Visible = true;
				GrdInvoiceHistDetails.DataSource = DTPoWoNumber.Tables[4];
				GrdInvoiceHistDetails.DataBind();
			}*/
            #endregion
            getApproverlist(hdnEmpCode.Value, Payment_ID, hdnDept_Name.Value, hdnCostCentre.Value);
			DataTable dsapproverNxt = new DataTable();
			dsapproverNxt = spm.GetNext_Payment_Request_ApproverDetails("Get_ADV_NextApprover_mail", hdnEmpCode.Value, Convert.ToInt32(Payment_ID), hdnDept_Name.Value, hdnCostCentre.Value, Convert.ToString(txtPoWoType.Text));
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
	private void getApproverlist(string EmpCode, int Payment_ID, string DeptnName, string TallyCode)
	{
		DataTable dtapprover = new DataTable();
		dtapprover = spm.Get_Advance_Pay_ApproverEmailID("Get_Advance_Pay_ApprovalList", EmpCode, DeptnName, TallyCode, Payment_ID, Convert.ToString(txtPoWoType.Text).Trim());
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
			hdnNextappName.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Emp_Name"]).Trim(); //"Account Department";
			hdnApproverType.Value = "NA";
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
	protected string GetPaymnetApprove_RejectList(string EmpCode, int Payment_ID, string DeptName, string TallyCode)
	{
		StringBuilder strbuild_Approvers = new StringBuilder();
		strbuild_Approvers.Length = 0;
		strbuild_Approvers.Capacity = 0;
		DataTable dtAppRej = new DataTable();
		dtAppRej = spm.Get_Advance_Pay_ApproverEmailID("Get_Advance_Pay_ApprovalList", EmpCode, DeptName, TallyCode, Payment_ID, Convert.ToString(txtPoWoType.Text).Trim());
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

	public string GetPayment_Approval_CCMail_Deatils(int PaymentID, string Assgineto)
	{

		try
		{
			String sbapp = "";
			DataTable dsTrDetails = new DataTable();
			dsTrDetails = spm.Get_Payment_Request_Approver_CCmail("Get_ADV_Intermidates_Pay_list", Assgineto, PaymentID, "", "");
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
	#endregion

	protected void localtrvl_btnSave_Click(object sender, EventArgs e)
	{
		try
		{
			lblmessage.Text = ""; string EmpCode = "", strtoDate = "";
			int Payment_ID = 0, Payment_Status_ID = 0, InvoiceID = 0, MstoneID = 0;
			DateTime PaymentDate; string[] strdate;
			Decimal dDirectTaxPercentage_Amt = 0;
			Decimal dDirectTaxPercentage = 0;
			Int32 iDirectTaxTypeID = 0;
			int IsLDC_Applicable = 0;
			Decimal dPayableAmt_WithTax = 0;
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}
			if (hdnExtraAPP.Value.ToString().Trim() == "Account")
			{
				if (Convert.ToString(lstDirectTaxSections_ACC.SelectedValue).Trim() == "0")
				{
					lblmessage.Text = "Please select Direct Tax Section.";
					return;
				}

				if (Convert.ToString(lstDirectTaxSections_ACC.SelectedValue).Trim() != "1" && Convert.ToString(lstDirectTaxSections_ACC.SelectedValue).Trim() != "0")
				{
					if (Convert.ToString(txt_Tax_Percentage.Text).Trim() == "")
					{
						lblmessage.Text = "Please enter Direct Tax Percentage.";
						return;
					}
					if (Convert.ToString(txt_Tax_Percentage.Text).Trim() != "")
					{
						if (Convert.ToDouble(txt_Tax_Percentage.Text) > 100)
						{
							txt_Tax_Percentage.Text = "";
							lblmessage.Text = "Please enter correct Direct Tax Percentage.";
							return;
						}

						strdate = Convert.ToString(txt_Tax_Percentage.Text).Trim().Split('.');
						if (strdate.Length > 2)
						{
							txt_Tax_Percentage.Text = "";
							lblmessage.Text = "Please enter correct Direct Tax Percentage.";
							return;
						}
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
				strdate = Convert.ToString(txtRequestDate.Text).Trim().Split('-');
				strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
				PaymentDate = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

				if (Convert.ToString(txtAmountPaidWithTax.Text).Trim() != "" && Convert.ToString(txtAccountPaidAmt.Text).Trim() != "")
				{ 
					Payment_Status_ID = 1;
					dDirectTaxPercentage = Convert.ToString(txt_Tax_Percentage.Text).Trim() != "" ? Convert.ToDecimal(txt_Tax_Percentage.Text):0;
					dDirectTaxPercentage_Amt = Math.Round(dDirectTaxPercentage * Convert.ToDecimal(txtAmountPaidWithoutTax.Text) / 100, 2);					 
					dPayableAmt_WithTax = Math.Round((Convert.ToDecimal(txtAmountPaidWithTax.Text)) - dDirectTaxPercentage_Amt, 2);
				 
					iDirectTaxTypeID = Convert.ToString(lstDirectTaxSections_ACC.SelectedValue).Trim() != "0" ? Convert.ToInt32(lstDirectTaxSections_ACC.SelectedValue) : 0;

					if (chkLDC_Applicable_ACC.Checked)
					{
						IsLDC_Applicable = 1;
					}
				}

                if (Convert.ToString(hdnPaymentTypeId.Value).Trim() == "2")
                {
                    txtAccountAmtBal.Text = txtAmountPaidWithTax.Text;
                }
                    DTInsertPayment = spm.dtUpdate_Payment_Request("ADVANCE_PAY_ACCOUNTREQUEST", EmpCode, InvoiceID, MstoneID, Payment_ID, "", Convert.ToDecimal(txtAmountPaidWithTax.Text), PaymentDate, 2, Payment_Status_ID, Convert.ToDecimal(txtAccountPaidAmt.Text), Convert.ToDecimal(txtAccountAmtBal.Text), txtPaymentRequestNo.Text.Trim(), txtApprovalRemark.Text, iDirectTaxTypeID, dDirectTaxPercentage, dDirectTaxPercentage_Amt, IsLDC_Applicable);
			}
			String strPatmentURL = "";int POID = 0;
			POID = Convert.ToString(hdnPOID.Value).Trim() != "" ? Convert.ToInt32(hdnPOID.Value) : 0;
			strPatmentURL = (Convert.ToString(ConfigurationManager.AppSettings["Link_AD_PayReqAPP"]).Trim() + "?Payment_ID=" + Payment_ID +"&POID=" + POID + "&Type=Pending");
			if (Convert.ToString((hdnstaus.Value).Trim()) != "")
			{
				if (Convert.ToString(hdnApproverType.Value).Trim() != "Approver" && Convert.ToString(hdnCurrentID.Value).Trim() != Convert.ToString(hdnapprid.Value).Trim())
				{
					if (Convert.ToString(hdnApproverType.Value).Trim() != "Approver")
					{
						spm.Insert_Advance_Pay_Approver_Request("INSERTADVANCE", hdnnextappcode.Value, Convert.ToInt32(hdnapprid.Value), Convert.ToInt32(hdnPayment_ID.Value), "Pending", "", Convert.ToString(("").Trim()));
					}
					spm.Insert_Advance_Pay_Approver_Request("UPDATEADVANCE", "", Convert.ToInt32(hdnCurrentID.Value), Convert.ToInt32(hdnPayment_ID.Value), "Approved", txtApprovalRemark.Text, Convert.ToString(("").Trim()));
				}
				else
				{				
					spm.Insert_Advance_Pay_Approver_Request("UPDATEADVANCE","", Convert.ToInt32(hdnCurrentID.Value), Convert.ToInt32(hdnPayment_ID.Value), "Approved", txtApprovalRemark.Text, Convert.ToString((hdnstaus.Value).Trim()));
				}
			}
			else
			{
				spm.Insert_Advance_Pay_Approver_Request("UPDATEADVANCE", "", Convert.ToInt32(hdnCurrentID.Value), Convert.ToInt32(hdnPayment_ID.Value), "Approved", txtApprovalRemark.Text, Convert.ToString(("").Trim()));
				spm.Insert_Advance_Pay_Approver_Request("INSERTADVANCE", hdnnextappcode.Value, Convert.ToInt32(hdnapprid.Value), Convert.ToInt32(hdnPayment_ID.Value), "Pending", "", Convert.ToString(("").Trim()));

			}
			string strapproverlist = "", ccMailIDs = "", PowoNo = "";
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
			string strsubject = "OneHR: Request for - Advance Payment Approval  - “" + txtPaymentRequestNo.Text + "”  For “" + PowoNo + "”";

            if(Convert.ToString(hdnPaymentTypeId.Value).Trim()=="2")
                strsubject = "OneHR: Request for - Security Deposit Payment Approval  - “" + txtPaymentRequestNo.Text + "”  For “" + PowoNo + "”";

            strapproverlist = GetPaymnetApprove_RejectList(hdnEmpCode.Value, Payment_ID, hdnDept_Name.Value, hdnCostCentre.Value);
			ccMailIDs = GetPayment_Approval_CCMail_Deatils(Payment_ID, hdnEmpCode.Value);
			//GetInvoiceTDSAmt();
			DataSet dsMilestone = new DataSet();
				//GetMilestoneDetails();
			string CostCenter = Convert.ToString(txtTallyCode_display.Text).Trim(); //hdnCostCentre.Value;
			if (hdnExtraAPP.Value.ToString().Trim() != "Account")
			{
				var actionByName = Session["emp_loginName"].ToString();
				Tbody = "<b>" + hdnEmpCodePrveName.Value + "</b> has created a Advance payment request as per the details below. Request your approval please.";
                if (Convert.ToString(hdnPaymentTypeId.Value).Trim() == "2")
                    Tbody = "<b>" + hdnEmpCodePrveName.Value + "</b> has created a Security Deposit payment request as per the details below. Request your approval please.";

                spm.send_mailto_VSCB_ADV_Pay_Next_Approver(hdnNextappName.Value, hdnLoginEmpEmail.Value, hdnApproverEmail.Value, strsubject, Tbody, ccMailIDs, strPatmentURL, strapproverlist, txtPOWONumber.Text, txtPoWoTitle.Text, txtPoWoDate.Text, txtPoWoType.Text, txtVendorName.Text, txtPoWoAmtWithTaxes.Text, txtPoWOPaidAmt.Text, txtPoWOPaidBalAmt.Text, txtProjectName.Text, txtInvoiceNo.Text, txtInvoiceAmount.Text, txtInvoiceBalAmt.Text, txtPaymentRequestNo.Text, txtRequestDate.Text, txtAmountPaidWithTax.Text, CostCenter, hdnDirectTax_Type.Value, hdnDirectTax_Percentage.Value, hdnDirectTax_Amount.Value, hdnPayable_Amt_Invoice.Value, MstoneID.ToString(), hdnMilestoneName.Value, hdnMilestAmount.Value, hdnMilestCGST_Amt.Value, hdnMilestSGST_Amt.Value, hdnMilestIGST_Amt.Value, hdnMilestAmtWithTax.Value, hdnMilestPyamentStatus.Value, dsMilestone,txtPaymentType.Text,txtRemark.Text);
			}
			else
			{
				DataSet dsFormatAmount = spm.getFormated_Amount("get_formated_currncy_Amount", Convert.ToString(txtCurrency.Text), Convert.ToDecimal(txtAccountPaidAmt.Text), Convert.ToDecimal(txtAccountAmtBal.Text), Convert.ToDecimal(txtPoWOPaidAmt.Text), Convert.ToDecimal(txtPoWOPaidBalAmt.Text), 0);
				string sformatPaymentpaidAmt = Convert.ToString(dsFormatAmount.Tables[0].Rows[0]["Amount_A"]);
				string sformatPaymentBalAmt = Convert.ToString(dsFormatAmount.Tables[0].Rows[0]["Amount_B"]);
				string sformattxtPoWOPaidAmt = Convert.ToString(dsFormatAmount.Tables[0].Rows[0]["Amount_C"]);
				string sformattxtPoWOPaidBalAmt = Convert.ToString(dsFormatAmount.Tables[0].Rows[0]["Amount_D"]);

				strPatmentURL = "";
				Tbody = "This is to inform you that " + hdnNextappName.Value + " has Approved below payment request.";
				spm.send_mailto_VSCB_ADV_Pay_Account_Approver(hdnEmpCodePrveName.Value, hdnLoginEmpEmail.Value, hdnEmpCodePrveEmailID.Value, strsubject, Tbody, ccMailIDs, strPatmentURL, strapproverlist, txtPOWONumber.Text, txtPoWoTitle.Text, txtPoWoDate.Text, txtPoWoType.Text, txtVendorName.Text, txtPoWoAmtWithTaxes.Text, sformattxtPoWOPaidAmt, sformattxtPoWOPaidBalAmt, txtProjectName.Text, txtInvoiceNo.Text, txtInvoiceAmount.Text, txtInvoiceBalAmt.Text, txtPaymentRequestNo.Text, txtRequestDate.Text, txtAmountPaidWithTax.Text, sformatPaymentpaidAmt, sformatPaymentBalAmt, CostCenter, hdnDirectTax_Type.Value, hdnDirectTax_Percentage.Value, hdnDirectTax_Amount.Value, hdnPayable_Amt_Invoice.Value, MstoneID.ToString(), hdnMilestoneName.Value, hdnMilestAmount.Value, hdnMilestCGST_Amt.Value, hdnMilestSGST_Amt.Value, hdnMilestIGST_Amt.Value, hdnMilestAmtWithTax.Value, hdnMilestPyamentStatus.Value, dsMilestone, txtPaymentType.Text, txtRemark.Text, lstDirectTaxSections_ACC.SelectedValue, lstDirectTaxSections_ACC.SelectedItem.Text,txt_Tax_Percentage.Text,txt_Direct_Tax_Amt.Text);
			}

			Response.Redirect("~/procs/VSCB_Inbox_ADV_Payment.aspx?Type=Pending", false);
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
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
			if (txtPOWONumber.Text != "")
			{
				PowoNo = txtPOWONumber.Text;
			}
			else
			{
				PowoNo = txtInvoiceNo.Text;
			}
			int MstoneID = 0;
			string strapproverlist = "", strPatmentURL = "", ccMailIDs = "";
			MstoneID = Convert.ToString(hdnMstoneID.Value).Trim() != "" ? Convert.ToInt32(hdnMstoneID.Value) : 0;
			spm.Insert_Advance_Pay_Approver_Request("ADV_Payment_Reject", hdnEmpCode.Value, Convert.ToInt32(hdnCurrentID.Value), Convert.ToInt32(hdnPayment_ID.Value), "Reject", txtApprovalRemark.Text, Convert.ToString(("").Trim()));
			GetEmployeeCode(hdnEmpCodePrve.Value);
			strapproverlist = GetPaymnetApprove_RejectList(hdnEmpCode.Value, Convert.ToInt32(hdnPayment_ID.Value), hdnDept_Name.Value, hdnCostCentre.Value);
			ccMailIDs = GetPayment_Approval_CCMail_Deatils(Convert.ToInt32(hdnPayment_ID.Value), hdnEmpCode.Value);
			string strsubject = "OneHR: Advance Payment request - “" + txtPaymentRequestNo.Text + "” Rejected against “" + PowoNo + "”";
			var actionByName = Session["emp_loginName"].ToString();

			//GetInvoiceTDSAmt();
			//GetMilestoneDetails()
			DataSet dsMilestone = new DataSet();
			string CostCenter = Convert.ToString(txtTallyCode_display.Text); // hdnCostCentre.Value;
			var Tbody = "This is to inform you that " + hdnLoginUserName.Value + " has<b> Rejected</b> Payment request  as per the details below.";
			spm.send_mail_VSCB_Advance_Pay_Rejection_Correction(hdnEmpCodePrveName.Value, hdnEmpCodePrveEmailID.Value, hdnLoginEmpEmail.Value, Tbody, strsubject, ccMailIDs, strPatmentURL, strapproverlist, txtPOWONumber.Text, txtPoWoTitle.Text, txtPoWoDate.Text, txtPoWoType.Text, txtVendorName.Text, txtPoWoAmtWithTaxes.Text, txtPoWOPaidAmt.Text, txtPoWOPaidBalAmt.Text, txtProjectName.Text, txtInvoiceNo.Text, txtInvoiceAmount.Text, txtInvoiceBalAmt.Text, txtPaymentRequestNo.Text, txtRequestDate.Text, txtAmountPaidWithTax.Text, CostCenter, hdnDirectTax_Type.Value, hdnDirectTax_Percentage.Value, hdnDirectTax_Amount.Value, hdnPayable_Amt_Invoice.Value, MstoneID.ToString(), hdnMilestoneName.Value, hdnMilestAmount.Value, hdnMilestCGST_Amt.Value, hdnMilestSGST_Amt.Value, hdnMilestIGST_Amt.Value, hdnMilestAmtWithTax.Value, hdnMilestPyamentStatus.Value, dsMilestone,txtPaymentType.Text.Trim(),txtRemark.Text.Trim());
			Response.Redirect("~/procs/VSCB_Inbox_ADV_Payment.aspx?Type=Pending");
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
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
			spm.Insert_Advance_Pay_Approver_Request("ADV_Payment_Correction", hdnEmpCode.Value, Convert.ToInt32(hdnCurrentID.Value), Convert.ToInt32(hdnPayment_ID.Value), "Correction", txtApprovalRemark.Text, Convert.ToString(("").Trim()));
			GetEmployeeCode(hdnEmpCodePrve.Value);
			strapproverlist = GetPaymnetApprove_RejectList(hdnEmpCode.Value, Convert.ToInt32(hdnPayment_ID.Value), hdnDept_Name.Value, hdnCostCentre.Value);
			ccMailIDs = GetPayment_Approval_CCMail_Deatils(Convert.ToInt32(hdnPayment_ID.Value), hdnEmpCode.Value);
			string strsubject = "OneHR: Payment request your Sent Back Correction “" + txtPaymentRequestNo.Text + "” For “" + PowoNo + "”";
			var actionByName = Session["emp_loginName"].ToString();

			//GetInvoiceTDSAmt();
			//GetMilestoneDetails();
			DataSet dsMilestone = new DataSet();
			string CostCenter = Convert.ToString(txtTallyCode_display.Text).Trim(); // hdnCostCentre.Value;
			var Tbody = "<b> " + hdnLoginUserName.Value + "</b> has Sent back the payment request for <b> Correction.</b> Please correct the same as instructed and resend for approval.";
			spm.send_mail_VSCB_Advance_Pay_Rejection_Correction(hdnEmpCodePrveName.Value, hdnEmpCodePrveEmailID.Value, hdnLoginEmpEmail.Value, Tbody, strsubject, ccMailIDs, strPatmentURL, strapproverlist, txtPOWONumber.Text, txtPoWoTitle.Text, txtPoWoDate.Text, txtPoWoType.Text, txtVendorName.Text, txtPoWoAmtWithTaxes.Text, txtPoWOPaidAmt.Text, txtPoWOPaidBalAmt.Text, txtProjectName.Text, txtInvoiceNo.Text, txtInvoiceAmount.Text, txtInvoiceBalAmt.Text, txtPaymentRequestNo.Text, txtRequestDate.Text, txtAmountPaidWithTax.Text, CostCenter, hdnDirectTax_Type.Value, hdnDirectTax_Percentage.Value, hdnDirectTax_Amount.Value, hdnPayable_Amt_Invoice.Value, MstoneID.ToString(), hdnMilestoneName.Value, hdnMilestAmount.Value, hdnMilestCGST_Amt.Value, hdnMilestSGST_Amt.Value, hdnMilestIGST_Amt.Value, hdnMilestAmtWithTax.Value, hdnMilestPyamentStatus.Value, dsMilestone,txtPaymentType.Text.Trim(),txtRemark.Text.Trim());
			Response.Redirect("~/procs/VSCB_Inbox_ADV_Payment.aspx?Type=Pending");
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

	protected void lnkdelete_Click(object sender, ImageClickEventArgs e)
	{

	}

	protected void lnkViewhist_Click(object sender, ImageClickEventArgs e)
	{

	}

	protected void lstDirectTaxSections_ACC_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (Convert.ToString(lstDirectTaxSections_ACC.SelectedValue).Trim() == "1")
		{
			txt_Tax_Percentage.Text = "0.00";
			txt_Tax_Percentage.Enabled = false;
			chkLDC_Applicable_ACC.Checked = false;
			chkLDC_Applicable_ACC.Enabled = false;
		}
		else
		{
			chkLDC_Applicable_ACC.Enabled = true;
		}
		get_DirectTax_Percentage_ACC(Convert.ToString(lstDirectTaxSections_ACC.SelectedValue), txt_Tax_Percentage.Text);
		Calculate_DirectTax_Amount_ACC();
	}
	public void get_DirectTax_Percentage_ACC(string isrno, string DirectTaxPercentage_ACC)
	{

		DataSet dsTDSPer = new DataSet();
		SqlParameter[] spars = new SqlParameter[2];

		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "get_TDS_Percentage";

		spars[1] = new SqlParameter("@Srno", SqlDbType.Int);
		spars[1].Value = Convert.ToInt32(isrno);


		dsTDSPer = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
		txt_Tax_Percentage.Text = "";
		if (dsTDSPer.Tables[0].Rows.Count > 0)
		{
			txt_Tax_Percentage.Text = Convert.ToString(dsTDSPer.Tables[0].Rows[0]["TDS_Percentage"]).Trim();
		}
	}

	private void Calculate_DirectTax_Amount_ACC()
	{

		Decimal dDirectTaxPercentage_Amt = 0;
		Decimal dDirectTaxPercentage = 0;
		
		if (Convert.ToString(lstDirectTaxSections_ACC.SelectedValue).Trim() != "0" && Convert.ToString(lstDirectTaxSections_ACC.SelectedValue).Trim() != "1")
			{
				if (Convert.ToString(txt_Tax_Percentage.Text).Trim() != "")
					dDirectTaxPercentage = Convert.ToDecimal(txt_Tax_Percentage.Text);

				if (Convert.ToString(txtAmountPaidWithoutTax.Text).Trim() != "")
				{
					dDirectTaxPercentage_Amt += Math.Round(dDirectTaxPercentage * Convert.ToDecimal(txtAmountPaidWithoutTax.Text) / 100, 2);
				}
			}

		txt_Direct_Tax_Amt.Text = Convert.ToString(dDirectTaxPercentage_Amt).Trim();
		txtAccountPaidAmt.Text = Convert.ToString((Convert.ToDecimal(txtAmountPaidWithTax.Text) - dDirectTaxPercentage_Amt));
		
		#region format Currency

		DataSet dtPOWODetails = new DataSet();
		decimal dinvoiceAmt = 0, dCGSTAmt = 0, dSGSTAmt = 0, dIGSTAmt = 0, dInvoiceAmtWithtax = 0;

		if (Convert.ToString(txtAccountPaidAmt.Text).Trim() != "")
			dinvoiceAmt = Convert.ToDecimal(txtAccountPaidAmt.Text);

		if (Convert.ToString(txt_Direct_Tax_Amt.Text).Trim() != "")
			dCGSTAmt = Convert.ToDecimal(txt_Direct_Tax_Amt.Text);

		dtPOWODetails = spm.getFormated_Amount("get_formated_Invoice_Amount", dinvoiceAmt, dCGSTAmt, dSGSTAmt, dIGSTAmt, dInvoiceAmtWithtax);


		if (dtPOWODetails.Tables[0].Rows.Count > 0)
		{
			txtAccountPaidAmt.Text = Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["invoiceAmt"]).Trim();
			txt_Direct_Tax_Amt.Text = Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["cgstAmt"]).Trim();
			//txtDirecttaxAmt_ACC_Display.Text = txtDirecttaxAmt.Text;
		}


		#endregion

	}

	protected void chkLDC_Applicable_ACC_CheckedChanged(object sender, EventArgs e)
	{
		if (chkLDC_Applicable_ACC.Checked)
		{
			txt_Tax_Percentage.Enabled = true;
			txt_Tax_Percentage.ReadOnly = false;
		}
		else
		{
			txt_Tax_Percentage.Enabled = false;
			txt_Tax_Percentage.ReadOnly = true;
		}
		get_DirectTax_Percentage_ACC(Convert.ToString(lstDirectTaxSections_ACC.SelectedValue), txt_Tax_Percentage.Text);
		Calculate_DirectTax_Amount_ACC();
	}

	protected void txt_Tax_Percentage_TextChanged(object sender, EventArgs e)
	{
		if(Convert.ToString(txt_Tax_Percentage.Text).Trim() != "")
		{
			decimal dDirectTaxperce = Convert.ToDecimal(txt_Tax_Percentage.Text);
			if (dDirectTaxperce > 100)
			{
				lblmessage.Text = "Please enter correct Direct Tax Percentage.";
				txt_Tax_Percentage.Text = "";
			}
		}
	Calculate_DirectTax_Amount_ACC();
	}

    protected void trvldeatils_cancel_btn_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(hdnInboxType.Value).Trim() == "Pending")
        {
            Response.Redirect("VSCB_Inbox_ADV_Payment.aspx?Type=Pending");
        }
        else if (Convert.ToString(hdnInboxType.Value).Trim() == "View")
        {
            Response.Redirect("VSCB_Inbox_ADV_Payment.aspx?Type=View");
        }
        else
        {
            Response.Redirect("Vscb_Index.aspx");
        }
    }
}