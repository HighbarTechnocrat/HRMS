using System;
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

public partial class procs_VSCB_My_Adv_PayRequestView : System.Web.UI.Page
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

	#region PageEvents
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
					FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VendorPaymentDocpath"]).Trim());
					FilePathInvoice.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBInvoicefiles"]).Trim());
					GetPoWoNumber();
					GetPaymentType();
					if (Request.QueryString.Count > 0)
					{
                        

                            hdnPayment_ID.Value = Convert.ToString(Request.QueryString[0]).Trim();
                            hdnPOID.Value = Convert.ToString(Request.QueryString[1]).Trim();
                            PaymentDetails();
                            SetEnableDalseConttols();
                            btnRecBack.Visible = true;

                        if (Request.QueryString.Count >= 3)
                        {
                            if (Request.QueryString["batch"] == "c" || Request.QueryString["batch"] == "v" || Request.QueryString["batch"] == "va" || Request.QueryString["batch"] == "vap")
                            {
                                div_buttons.Visible = false;
                                trvldeatils_delete_btn.Visible = false;
                                Cancel1.Visible = false;
                                txtPONumber.Visible = true;
                                lstPOWONumber.Visible = false;
                                txtAmountPaidWithoutTax.Enabled = false;
                                if (Request.QueryString["batch"] == "v" || Request.QueryString["batch"] == "va" || Request.QueryString["batch"] == "vap")
                                {
                                    hdnBatchId.Value = Request.QueryString["batchid"];
                                }
                            }
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
	protected void localtrvl_btnSave_Click(object sender, EventArgs e)
	{
		int Payment_ID = 0, MstoneID = 0, InvoiceID = 0, POID = 0, AdvancePayTypeID=0; 
		decimal BalAmount = 0;
		DateTime PaymentDate;
		string EmpCode = "", strtoDate = "", multiplefilename = "", multiplefilenameadd = "", strfileName = "";
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
			if (Convert.ToString(lstPOWONumber.SelectedValue).Trim() == "" || Convert.ToString(lstPOWONumber.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please Select Po/Wo Number";
				return;
			}
			if (Convert.ToString(txtPaymentRequestNo.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter Payment Request No";
				return;
			}
			if (Convert.ToString(txtAmountPaidWithTax.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter advance payment amount";
				return;
			}
			if (Convert.ToString(txtAmountPaidWithTax.Text).Trim() != "" && Convert.ToString(txtPoWOPaidBalAmt.Text).Trim() != "")
			{
				if (Convert.ToDecimal(txtAmountPaidWithTax.Text) > Convert.ToDecimal(txtPoWOPaidBalAmt.Text))//txtInvoiceAmount
				{
					lblmessage.Text = "Amount to be paid cannot exceed balance PO/WO Amount! ";
					return;
				}
			}
			if (Convert.ToString(txtAmountPaidWithTax.Text).Trim() != "" && hdnTobePaidAmt.Value != "")
			{
				decimal BalAmt = 0;
				BalAmt = Convert.ToDecimal(hdnInvoicePayableAmt.Value) - Convert.ToDecimal(hdnTobePaidAmt.Value);
				if (Convert.ToDecimal(txtAmountPaidWithTax.Text) > BalAmt)
				{
					lblmessage.Text = "Amount to be paid cannot exceed PO/WO Amount! ";
					return;
				}
			}
			if (Convert.ToString(lstPaymentType.SelectedValue).Trim() == "" || Convert.ToString(lstPaymentType.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please Select Payment Type.";
				return;
			}
			if (Convert.ToString(txtRemark.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter Remark";
				return;
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
						string strremoveSpace = "APay_" + j + "_" + hdnEmpCpde.Value + "_" + Dates + Path.GetExtension(fileName);
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
			//int DeptID = Convert.ToString(lstPositionDept.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionDept.SelectedValue) : 0;
			dtApproverEmailIds = spm.Get_Advance_Pay_ApproverEmailID("Get_Advance_Pay_ApprovalList", EmpCode, hdnDept_Name.Value, hdnCostCentre.Value, 0, Convert.ToString(txtPoWoType.Text).Trim());
			if (dtApproverEmailIds.Rows.Count > 0)
			{
				approveremailaddress = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];
				apprid = (int)dtApproverEmailIds.Rows[0]["APPR_ID"];
				nxtapprname = (string)dtApproverEmailIds.Rows[0]["Emp_Name"];
				Approvers_code = (string)dtApproverEmailIds.Rows[0]["A_EMP_CODE"];
				hdnapprcode.Value = Approvers_code;
			}

			InvoiceID = Convert.ToString(hdnInvoiceID.Value).Trim() != "" ? Convert.ToInt32(hdnInvoiceID.Value) : 0;
			MstoneID = Convert.ToString(hdnMstoneID.Value).Trim() != "" ? Convert.ToInt32(hdnMstoneID.Value) : 0;
			Payment_ID = Convert.ToString(hdnPayment_ID.Value).Trim() != "" ? Convert.ToInt32(hdnPayment_ID.Value) : 0;
			AdvancePayTypeID = Convert.ToString(lstPaymentType.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPaymentType.SelectedValue) : 0;
			//BalAmount=Convert.ToDecimal(txtAmountPaidWithTax) - Convert.ToDecimal(txtAmountPaidWithTax);			
			multiplefilenameadd = multiplefilenameadd.TrimEnd(',');
			POID = Convert.ToString(hdnPOID.Value).Trim() != "" ? Convert.ToInt32(hdnPOID.Value) : 0;
			DTInsertPayment = spm.Insert_Advance_Pay_Request("ADV_PAYUPDATEREQUEST", EmpCode, POID, InvoiceID, AdvancePayTypeID, Payment_ID, txtPaymentRequestNo.Text.Trim(), Convert.ToDecimal(txtAmountPaidWithTax.Text), PaymentDate, 1, 1, 0, BalAmount, "", multiplefilenameadd, txtRemark.Text.Trim(),0,"","","");
			if (DTInsertPayment.Rows.Count > 0)
			{
				//Payment_ID = Convert.ToInt32(DTInsertPayment.Rows[0]["Payment_ID"]);
				string strPatmentURL = "", strapproverlist = "";
				spm.Insert_Advance_Pay_Approver_Request("INSERTADVANCE", Approvers_code, apprid, Payment_ID, "Pending", "", "");
				strPatmentURL = (Convert.ToString(ConfigurationManager.AppSettings["Link_AD_PayReqAPP"]).Trim() + "?Payment_ID=" + Payment_ID + "&POID=" + POID + "&Type=Pending");
				strapproverlist = GetPaymnetApprove_RejectList(hdnEmpCpde.Value, Payment_ID, hdnDept_Name.Value, hdnCostCentre.Value);
				string PowoNo = "";
				if (lstPOWONumber.SelectedItem.Text != "Select PO/ WO Number" || lstPOWONumber.SelectedIndex != 0)
				{
					PowoNo = lstPOWONumber.SelectedItem.Text;
				}
				else
				{
					//PowoNo = txtInvoiceNo.Text;
				}
				var actionByName = Session["emp_loginName"].ToString();
				
				//GetInvoiceTDSAmt();
				//GetMilestoneDetails();
				DataSet dsMilestone = new DataSet();
				string CostCenter = Convert.ToString(txtTallyCode_display.Text).Trim(); // txtProjectName.Text;
				DataSet dsFormatAmount = spm.getFormated_Amount("get_formated_currncy_Amount", Convert.ToString(txtCurrency.Text), Convert.ToDecimal(txtAmountPaidWithTax.Text), 0, 0, 0, 0);
				string sformatPaymentReqAmt = Convert.ToString(dsFormatAmount.Tables[0].Rows[0]["Amount_A"]);
				string strsubject = "Request for - Advance Payment Approval  “" + txtPaymentRequestNo.Text + "”  For " + PowoNo;
				string TBody = actionByName + " has created a payment request as per the details below. Request your approval please.";
				spm.send_mailto_VSCB__Advance_Pay_Approver(nxtapprname, "", approveremailaddress, strsubject, TBody, "", strPatmentURL, strapproverlist, lstPOWONumber.SelectedItem.Text, txtPoWoTitle.Text, txtPoWoDate.Text, txtPoWoType.Text, txtVendorName.Text, txtPoWoAmtWithTaxes.Text, txtPoWOPaidAmt.Text, txtPoWOPaidBalAmt.Text, txtProjectName.Text, "", "", "", txtPaymentRequestNo.Text, txtRequestDate.Text, sformatPaymentReqAmt, lstPaymentType.SelectedItem.Text, txtRemark.Text.Trim(), CostCenter, hdnDirectTax_Type.Value, hdnDirectTax_Percentage.Value, hdnDirectTax_Amount.Value, hdnPayable_Amt_Invoice.Value, MstoneID.ToString(), hdnMilestoneName.Value, hdnMilestAmount.Value, hdnMilestCGST_Amt.Value, hdnMilestSGST_Amt.Value, hdnMilestIGST_Amt.Value, hdnMilestAmtWithTax.Value, hdnMilestPyamentStatus.Value, dsMilestone);
			}
			Response.Redirect("~/procs/VSCB_InboxMy_Adv_PayRequest.aspx", false);
		}
		catch (Exception ex)
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
			dtCostCenter = spm.GetInvoiceTDS_Amt("GetInvoiceTDSAmt", hdnEmpCpde.Value, InvoiceID);
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
	private void GetProject_CostCenter()
	{
		try
		{
			DataTable dtCostCenter = new DataTable();
			dtCostCenter = spm.Get_Project_CostCenter("GetDeptCostCenter", hdnEmpCpde.Value, txtProjectName.Text.Trim());
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

	private void GetMilestoneDetails_Old()
	{
		try
		{
			int MstoneID = 0;
			DataTable dtMilestone = new DataTable();
			MstoneID = Convert.ToString(hdnMstoneID.Value).Trim() != "" ? Convert.ToInt32(hdnMstoneID.Value) : 0;
			dtMilestone = spm.dtShowInvoiceDetailsForPayment("GetPayment_MilestoneDetails", hdnEmpCpde.Value, 0, MstoneID);
			if (dtMilestone.Rows.Count > 0)
			{
				hdnMilestoneName.Value = dtMilestone.Rows[0]["MilestoneName"].ToString();
				//hdnCostCentre.Value = dtMilestone.Rows[0]["Quantity"].ToString();
				//hdnCostCentre.Value = dtMilestone.Rows[0]["Rate"].ToString();
				hdnMilestAmount.Value = dtMilestone.Rows[0]["Amount"].ToString();
				hdnMilestCGST_Amt.Value = dtMilestone.Rows[0]["CGST_Amt"].ToString();
				hdnMilestSGST_Amt.Value = dtMilestone.Rows[0]["SGST_Amt"].ToString();
				hdnMilestIGST_Amt.Value = dtMilestone.Rows[0]["IGST_Amt"].ToString();
				//hdnCostCentre.Value = dtMilestone.Rows[0]["CGST_Per"].ToString();
				//hdnCostCentre.Value = dtMilestone.Rows[0]["SGST_Per"].ToString();
				//hdnCostCentre.Value = dtMilestone.Rows[0]["IGST_Per"].ToString();
				hdnMilestAmtWithTax.Value = dtMilestone.Rows[0]["AmtWithTax"].ToString();
				//hdnCostCentre.Value = dtMilestone.Rows[0]["Milesstone_Balance_Amt"].ToString();
				//hdnCostCentre.Value = dtMilestone.Rows[0]["Collect_TDS_Amt"].ToString();
				hdnMilestPyamentStatus.Value = dtMilestone.Rows[0]["PyamentStatus"].ToString();
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
			spars[0].Value = "InvoiceMilestoneList_CreatePayment";

			spars[1] = new SqlParameter("@InvoiceID", SqlDbType.VarChar);
			spars[1].Value = hdnInvoiceID.Value;


			dsMilestone = spm.getDatasetList(spars, "sp_VSCB_CreatePOWO_Users");

		}
		catch (Exception)
		{

			throw;
		}
		return dsMilestone;
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
	protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
	{

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
			DataTable dtFile = spm.Get_VSCB_Payment_DeleteFile("Select_ADV_Payment_DeleteFile", Convert.ToString(hdnEmpCpde.Value), Payment_ID, FileName);
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
	protected void accmo_delete_btn_Click(object sender, EventArgs e)
	{
		//localtrvl_btnSave.Visible = true;
		PaymentDetails();
		SetEnableDalseConttols();

	}
	#endregion

	#region PageMethods
	public void GetPoWoNumber()
	{
		DTPoWoNumber = spm.dtPOWoCreate("POWO_ADV_My_Pay_Request", Convert.ToString(hdnEmpCpde.Value));
		lstPOWONumber.DataSource = DTPoWoNumber;
		lstPOWONumber.DataTextField = "PONumber";
		lstPOWONumber.DataValueField = "POID";
		lstPOWONumber.DataBind();
		lstPOWONumber.Items.Insert(0, new ListItem("Select PO/ WO Number", "0"));
	}
	private void SetEnableDalseConttols()
	{
		try
		{
			if (hflStatusID.Value == "5")
			{
				uploadfile.Visible = true;
				spnPaymentSuppotingFiles.Visible = true;
				txtRequestDate.Text = DateTime.Now.ToString("dd-MM-yyyy");

			}
			else if (hflStatusID.Value == "2")
			{
				Cancel1.Visible = true;
				trvldeatils_delete_btn.Visible = true;
				txtAmountPaidWithTax.Enabled = false;
				txtRemark.Enabled = false;
				lstPaymentType.Enabled = false;
				uploadfile.Visible = false;
			}
			else
			{
				txtAmountPaidWithTax.Enabled = false;
				txtRemark.Enabled = false;
				lstPaymentType.Enabled = false;
				uploadfile.Visible = false;
			}

            //if (Convert.ToString(hdnPyamentStatus.Value).Trim() != "2")
            //{
            //    trvldeatils_delete_btn.Visible = false;
            //    Cancel1.Visible = false;
            //    txtAmountPaidWithTax.Enabled = false;
            //    txtAmountPaidWithoutTax.Enabled = false;
            //}
            if (Convert.ToString(hdnPyamentStatus.Value).Trim() == "2")
            {
                trvldeatils_delete_btn.Visible = false;
                Cancel1.Visible = false;
                txtAmountPaidWithTax.Enabled = false;
                txtAmountPaidWithoutTax.Enabled = false;
            }
        }
		catch (Exception)
		{

			throw;
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
			DataTable DTstatus = new DataTable();
			int Payment_ID = 0, POID = 0;
			Payment_ID = Convert.ToString(hdnPayment_ID.Value).Trim() != "" ? Convert.ToInt32(hdnPayment_ID.Value) : 0;
			POID = Convert.ToString(hdnPOID.Value).Trim() != "" ? Convert.ToInt32(hdnPOID.Value) : 0;
			DTPoWoNumber = spm.GetVSCB_MyPaymentDetails("POWOMy_ADV_Pay_RequestDeatils", Convert.ToString(hdnEmpCpde.Value), POID, Payment_ID);
			if (DTPoWoNumber.Tables[0].Rows.Count > 0)
			{
				txtPoWoDate.Text = DTPoWoNumber.Tables[0].Rows[0]["PODate"].ToString();
				lstPOWONumber.SelectedValue = DTPoWoNumber.Tables[0].Rows[0]["POID"].ToString();
                txtPONumber.Text = DTPoWoNumber.Tables[0].Rows[0]["PONumber"].ToString();
                 

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
				txtTallyCode_display.Text = DTPoWoNumber.Tables[0].Rows[0]["Tallycode"].ToString();
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
				//DivCreateInvoice.Visible = true;
				hdnMstoneID.Value = DTPoWoNumber.Tables[2].Rows[0]["MstoneID"].ToString();
				hdnInvoiceID.Value = DTPoWoNumber.Tables[2].Rows[0]["InvoiceID"].ToString();
				txtPaymentRequestNo.Text = DTPoWoNumber.Tables[2].Rows[0]["PaymentReqNo"].ToString();
				txtRequestDate.Text = DTPoWoNumber.Tables[2].Rows[0]["PaymentReqDate"].ToString();
				txtAmountPaidWithTax.Text = DTPoWoNumber.Tables[2].Rows[0]["TobePaidAmtWithtax"].ToString();
				lstPaymentType.SelectedValue = DTPoWoNumber.Tables[2].Rows[0]["AdvancePayTypeID"].ToString();
				txtRemark.Text = DTPoWoNumber.Tables[2].Rows[0]["CreatedRemarks"].ToString();

                txtAmountPaidWithoutTax.Text = DTPoWoNumber.Tables[2].Rows[0]["TobePaidAmtWithOuttax"].ToString();
                hdnPyamentStatus.Value = Convert.ToString(DTPoWoNumber.Tables[2].Rows[0]["PaymentStatusID"]).Trim();
                liGSTAmt_1.Visible = false;
                liGSTAmt_2.Visible = false;
                liGSTAmt_3.Visible = false;

                liAdvAmtWithGSt_1.Visible = false;
                liAdvAmtWithGSt_2.Visible = false;
                liAdvAmtWithGSt_3.Visible = false;

                if (Convert.ToString(DTPoWoNumber.Tables[2].Rows[0]["CGST_Amt"]).Trim()!="")
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

                if (Convert.ToString(DTPoWoNumber.Tables[2].Rows[0]["CGST_Amt"]).Trim() != "" || Convert.ToString(DTPoWoNumber.Tables[2].Rows[0]["SGST_Amt"]).Trim()!="" || Convert.ToString(DTPoWoNumber.Tables[2].Rows[0]["IGST_Amt"]).Trim() !="")
                {
                    liGSTAmt_1.Visible = true;
                    liGSTAmt_2.Visible = true;
                    liGSTAmt_3.Visible = true;

                    liAdvAmtWithGSt_1.Visible = true;
                    liAdvAmtWithGSt_2.Visible = true;
                    liAdvAmtWithGSt_3.Visible = true;

                }

                if (Convert.ToInt32(DTPoWoNumber.Tables[2].Rows[0]["Status_id"]) == 1)
                {
                    txtAmountPaidWithoutTax.Enabled = false;
                }

                    //hdnInvoicePayableAmt.Value = DTPoWoNumber.Tables[3].Rows[0]["Payable_Amt_With_Tax"].ToString();
                    //lnkfile_Invoice.Text = DTPoWoNumber.Tables[3].Rows[0]["Invoice_File"].ToString();

                    hflStatusID.Value = DTPoWoNumber.Tables[2].Rows[0]["Status_id"].ToString();
               
				lblheading.Text = "Advance Payment Request View - " + txtPaymentRequestNo.Text + ", Payment Status - " + DTPoWoNumber.Tables[2].Rows[0]["PyamentStatus"].ToString();

                if (Convert.ToString(DTPoWoNumber.Tables[2].Rows[0]["AdvancePayTypeID"]).Trim() == "2")
                    lblheading.Text = "Security Deposit Payment Request View - " + txtPaymentRequestNo.Text + ", Payment Status - " + DTPoWoNumber.Tables[2].Rows[0]["PyamentStatus"].ToString();

                if (Convert.ToInt32(DTPoWoNumber.Tables[2].Rows[0]["Status_id"]) == 5)
				{
					localtrvl_btnSave.Visible = true;
					spnPaymentSuppotingFiles.Visible = true;
				}
				if (Convert.ToInt32(DTPoWoNumber.Tables[2].Rows[0]["Status_id"]) == 2)
				{
					Account1.Visible = true;
					Account2.Visible = true;
					Account3.Visible = true;
					txtAccountPaidAmt.Text = DTPoWoNumber.Tables[2].Rows[0]["Amt_paid_Account"].ToString();
					txtAccountAmtBal.Text = DTPoWoNumber.Tables[2].Rows[0]["AccountBalAmt"].ToString();
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
			if (DTPoWoNumber.Tables[3].Rows.Count > 0)
			{
				spnPaymentSuppotingFiles1.Visible = true;
				GrdFileUpload.DataSource = DTPoWoNumber.Tables[3];
				GrdFileUpload.DataBind();
			}
			if (DTPoWoNumber.Tables[4].Rows.Count > 0)
			{
				SPPayHist.Visible = true;
				GrdInvoiceHistDetails.DataSource = DTPoWoNumber.Tables[4];
				GrdInvoiceHistDetails.DataBind();
			}
			Payment_ID = Convert.ToString(hdnPayment_ID.Value).Trim() != "" ? Convert.ToInt32(hdnPayment_ID.Value) : 0;
			DTstatus = spm.Get_Payment_AccountApprovalEmp("Get_ADV_Pay_ApprovalStatus", "", Payment_ID);
			if (DTstatus.Rows.Count > 0)
			{
				if (Convert.ToString(DTstatus.Rows[0]["Action"]).Trim() == "Pending" || Convert.ToString(DTstatus.Rows[0]["Action"]).Trim() == "Correction")
				{
					this.GrdFileUpload.Columns[1].Visible = true;
				}
			}
			//PaymentBalanceAmt();
			getApproverlist(hdnEmpCpde.Value, Payment_ID, hdnDept_Name.Value, hdnCostCentre.Value, Convert.ToString(txtPoWoType.Text).Trim());
		 
		}
		catch (Exception ex)
		{

			throw;
		}
	} 

    public void GetPaymentType()
	{
		DTPoWoNumber = spm.dtPOWoCreate("POWO_Advance_PaymentType", Convert.ToString(hdnEmpCpde.Value));
		lstPaymentType.DataSource = DTPoWoNumber;
		lstPaymentType.DataTextField = "TypeName";
		lstPaymentType.DataValueField = "AdvancePayTypeID";
		lstPaymentType.DataBind();
		lstPaymentType.Items.Insert(0, new ListItem("Select Payment Type", "0"));
	}

	private void getApproverlist(string EmpCode, int Payment_ID, string DeptnName, string TallyCode, string spotype)
	{
		DataTable dtapprover = new DataTable();
		dtapprover = spm.Get_Advance_Pay_ApproverEmailID("Get_Advance_Pay_ApprovalList", EmpCode, DeptnName, TallyCode, Payment_ID, spotype);
		DgvApprover.DataSource = null;
		DgvApprover.DataBind();
		if (dtapprover.Rows.Count > 0)
		{
			DgvApprover.DataSource = dtapprover;
			DgvApprover.DataBind();
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
			//Payment_ID = Convert.ToString(hdnPayment_ID.Value).Trim() != "" ? Convert.ToInt32(hdnPayment_ID.Value) : 0;
			POID = Convert.ToString(hdnPOID.Value).Trim() != "" ? Convert.ToInt32(hdnPOID.Value) : 0;
			DTPoWoNumber = spm.GetVSCB_MyPaymentDetails("POWOPaymentRequestHistory", Convert.ToString(hdnEmpCpde.Value), POID, Payment_ID);
			if (DTPoWoNumber.Tables[0].Rows.Count > 0)
			{
				//DivCreateInvoice.Visible = true;
				//txtInvoiceBalAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["BalanceAmt"].ToString();
				txtPaymentRequestNo.Text = DTPoWoNumber.Tables[0].Rows[0]["PaymentReqNo"].ToString();
				txtRequestDate.Text = DTPoWoNumber.Tables[0].Rows[0]["PaymentReqDate"].ToString();
				txtAmountPaidWithTax.Text = DTPoWoNumber.Tables[0].Rows[0]["TobePaidAmtWithtax"].ToString();
				hflStatusID.Value = DTPoWoNumber.Tables[0].Rows[0]["Status_id"].ToString();
				if (Convert.ToInt32(DTPoWoNumber.Tables[0].Rows[0]["Status_id"]) == 2)
				{
					Account1.Visible = true;
					Account2.Visible = true;
					Account3.Visible = true;
					txtAccountPaidAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["Amt_paid_Account"].ToString();
					txtAccountAmtBal.Text = DTPoWoNumber.Tables[0].Rows[0]["AccountBalAmt"].ToString();
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
			GrdFileUpload.DataSource = null;
			GrdFileUpload.DataBind();
			if (DTPoWoNumber.Tables[1].Rows.Count > 0)
			{
				GrdFileUpload.DataSource = DTPoWoNumber.Tables[1];
				GrdFileUpload.DataBind();
				this.GrdFileUpload.Columns[1].Visible = false;
			}
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
			DTCreateInvoice = spm.dtShowInvoiceDetailsForPayment("POWO_ADV_PaymentAmt", Convert.ToString(hdnEmpCpde.Value), InvoiceID, 0);
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
	#endregion


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

	protected void trvldeatils_delete_btn_Click(object sender, EventArgs e)
	{
		int Payment_ID = 0, MstoneID = 0, InvoiceID = 0, POID = 0, AdvancePayTypeID = 0;
		decimal BalAmount = 0;
		DateTime PaymentDate;
		string EmpCode = "", strtoDate = "", multiplefilename = "", multiplefilenameadd = "", strfileName = "";
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
			
			if (Convert.ToString(txtRemark1.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter Cancel Remark";
				return;
			}
			#endregion

		    EmpCode = hdnEmpCpde.Value;
			//int DeptID = Convert.ToString(lstPositionDept.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionDept.SelectedValue) : 0;

			strdate = Convert.ToString(DateTime.Now.ToString("dd-MM-yyyy")).Trim().Split('-');
			strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
			PaymentDate = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

			InvoiceID = Convert.ToString(hdnInvoiceID.Value).Trim() != "" ? Convert.ToInt32(hdnInvoiceID.Value) : 0;
			MstoneID = Convert.ToString(hdnMstoneID.Value).Trim() != "" ? Convert.ToInt32(hdnMstoneID.Value) : 0;
			Payment_ID = Convert.ToString(hdnPayment_ID.Value).Trim() != "" ? Convert.ToInt32(hdnPayment_ID.Value) : 0;
			
			POID = Convert.ToString(hdnPOID.Value).Trim() != "" ? Convert.ToInt32(hdnPOID.Value) : 0;
			DTInsertPayment = spm.Insert_Advance_Pay_Request("ADV_PAY_CANCEL_REQUEST", EmpCode, POID, InvoiceID, AdvancePayTypeID, Payment_ID, txtPaymentRequestNo.Text.Trim(), Convert.ToDecimal(txtAmountPaidWithTax.Text), PaymentDate, 3, 1, 0, BalAmount, "", multiplefilenameadd, txtRemark1.Text.Trim(),0,"","","");
			if (DTInsertPayment.Rows.Count > 0)
			{
				//Payment_ID = Convert.ToInt32(DTInsertPayment.Rows[0]["Payment_ID"]);
				string strPatmentURL = "", strapproverlist = "",sbapp = "" ;
			    string PowoNo = "";
				if (lstPOWONumber.SelectedItem.Text != "Select PO/ WO Number" || lstPOWONumber.SelectedIndex != 0)
				{
					PowoNo = lstPOWONumber.SelectedItem.Text;
				}
				dtApproverEmailIds = spm.Get_Advance_Pay_ApproverEmailID("Get_Advance_Pay_ApprovalList", EmpCode, hdnDept_Name.Value, hdnCostCentre.Value, 0, Convert.ToString(txtPoWoType.Text).Trim());
				if (dtApproverEmailIds.Rows.Count > 0)
				{
					for (int i = 0; i < dtApproverEmailIds.Rows.Count; i++)
					{
						if (Convert.ToString(sbapp).Trim() == "")
							sbapp = Convert.ToString(dtApproverEmailIds.Rows[0]["Emp_Emailaddress"]).Trim();
						else
							sbapp = sbapp + ";" + Convert.ToString(dtApproverEmailIds.Rows[i]["Emp_Emailaddress"]).Trim();

					}
				}
				var actionByName = Session["emp_loginName"].ToString();
				DataSet dsMilestone = new DataSet();
				string CostCenter = Convert.ToString(txtTallyCode_display.Text).Trim(); // txtProjectName.Text;
				DataSet dsFormatAmount = spm.getFormated_Amount("get_formated_currncy_Amount", Convert.ToString(txtCurrency.Text), Convert.ToDecimal(txtAmountPaidWithTax.Text), 0, 0, 0, 0);
				string sformatPaymentReqAmt = Convert.ToString(dsFormatAmount.Tables[0].Rows[0]["Amount_A"]);
				string strsubject = "Request for - Advance Payment Cancellation  “" + txtPaymentRequestNo.Text + "”  For " + PowoNo;
				string TBody = actionByName + " has created a payment request cancellation as per the details below.";
				spm.send_mailto_VSCB__Advance_Pay_Cancel(nxtapprname, "", sbapp, strsubject, TBody, "", strPatmentURL, strapproverlist, lstPOWONumber.SelectedItem.Text, txtPoWoTitle.Text, txtPoWoDate.Text, txtPoWoType.Text, txtVendorName.Text, txtPoWoAmtWithTaxes.Text, txtPoWOPaidAmt.Text, txtPoWOPaidBalAmt.Text, txtProjectName.Text, "", "", "", txtPaymentRequestNo.Text, txtRequestDate.Text, sformatPaymentReqAmt, lstPaymentType.SelectedItem.Text, txtRemark.Text.Trim(), CostCenter, hdnDirectTax_Type.Value, hdnDirectTax_Percentage.Value, hdnDirectTax_Amount.Value, hdnPayable_Amt_Invoice.Value, MstoneID.ToString(), hdnMilestoneName.Value, hdnMilestAmount.Value, hdnMilestCGST_Amt.Value, hdnMilestSGST_Amt.Value, hdnMilestIGST_Amt.Value, hdnMilestAmtWithTax.Value, hdnMilestPyamentStatus.Value, dsMilestone,txtRemark1.Text);
			}
			Response.Redirect("~/procs/VSCB_InboxMy_Adv_PayRequest.aspx", false);
		}
		catch (Exception ex)
		{
			
			throw;
		}
	}

    protected void trvldeatils_cancel_btn_Click(object sender, EventArgs e)
    {
        if (Request.QueryString.Count >= 3)
        {
            if (Request.QueryString["batch"] == "c")            
                Response.Redirect("VSCB_CreateBatch.aspx");

            if (Request.QueryString["batch"] == "v")
                Response.Redirect("VSCB_AssignbankRefApproveBatch.aspx?batchid="+Convert.ToString(hdnBatchId.Value).Trim());

            if (Request.QueryString["batch"] == "va")
                Response.Redirect("VSCB_ApproveBatch.aspx?batchid=" + Convert.ToString(hdnBatchId.Value).Trim());

            if (Request.QueryString["batch"] == "vap")
                Response.Redirect("VSCB_ApproveBatch.aspx?batchid=" + Convert.ToString(hdnBatchId.Value).Trim() + "&mngexp=2");

        }
        Response.Redirect("VSCB_InboxMy_Adv_PayRequest.aspx"); 
    }
}