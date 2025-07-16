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

public partial class procs_VSCB_MyPaymentRequestView : System.Web.UI.Page
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
					if (Request.QueryString.Count > 0)
					{
						hdnPayment_ID.Value = Convert.ToString(Request.QueryString[0]).Trim();
						hdnPOID.Value = Convert.ToString(Request.QueryString[1]).Trim();
						PaymentDetails();
						SetEnableDalseConttols();
						btnRecBack.Visible = true;
                        GetPaymentRequestcanceldEnabledisablebutton();

                    }
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}

    private void GetPaymentRequestcanceldEnabledisablebutton()
    {
        SqlParameter[] spars = new SqlParameter[4];
        spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
        spars[0].Value = "GetPaymentRequestcanceldflagCheck";
        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCpde.Value;
        spars[2] = new SqlParameter("@POID", SqlDbType.Int);
        spars[2].Value = hdnPOID.Value;
        spars[3] = new SqlParameter("@Payment_ID", SqlDbType.Int);
        spars[3].Value = hdnPayment_ID.Value;
        DataSet dsApprovalStatusReport = spm.getDatasetList(spars, "sp_VSCB_CreatePOWO_Users");

        if (dsApprovalStatusReport.Tables[0].Rows.Count > 0)
        {
            trvldeatils_btnSave.Visible = false;
        }
        if (dsApprovalStatusReport.Tables[1].Rows.Count > 0)
        {
            trvldeatils_btnSave.Visible = false;
        }
    }

	protected void localtrvl_btnSave_Click(object sender, EventArgs e)
	{
		int Payment_ID = 0, MstoneID = 0, InvoiceID = 0;
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
			if (Convert.ToString(txtAmountPaidWithTax.Text).Trim() == "" || Convert.ToString(txtAmountPaidWithTax.Text).Trim() == "0")
			{
				lblmessage.Text = "Please enter Amount to be Requested.";
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
				if (Convert.ToDecimal(txtAmountPaidWithTax.Text) > BalAmt)
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
			//int DeptID = Convert.ToString(lstPositionDept.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionDept.SelectedValue) : 0;
			dtApproverEmailIds = spm.Get_Payment_Request_ApproverEmailID(EmpCode, hdnDept_Name.Value, hdnCostCentre.Value, 0,Convert.ToString(txtPoWoType.Text).Trim());
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
			//BalAmount=Convert.ToDecimal(txtAmountPaidWithTax) - Convert.ToDecimal(txtAmountPaidWithTax);			
			multiplefilenameadd = multiplefilenameadd.TrimEnd(',');
			DTInsertPayment = spm.dtInsert_Payment_Request("PAYMNETUPDATEREQUEST", EmpCode, InvoiceID, MstoneID, Payment_ID, txtPaymentRequestNo.Text.Trim(), Convert.ToDecimal(txtAmountPaidWithTax.Text), PaymentDate, 1, 1, 0, BalAmount, "", multiplefilenameadd);

            for (int i = 0; i <= GVEditpaymentReqamtpaid.Rows.Count - 1; i++)
            {
                GridViewRow ro = GVEditpaymentReqamtpaid.Rows[i];
                TextBox txtMSInvoiceAmt = (TextBox)ro.FindControl("txtMilestoneInvoiceAmt");
                string HDmilestoneid = "";
                if (Convert.ToString(txtMSInvoiceAmt.Text).Trim() != "")
                {
                    HDmilestoneid = Convert.ToString(GVEditpaymentReqamtpaid.DataKeys[ro.RowIndex].Values[0]).Trim();

                    SqlParameter[] spars = new SqlParameter[6];
                    spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
                    spars[0].Value = "PaymentRequestMSAmountCorrectionUpdate";
                    spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
                    spars[1].Value = EmpCode;
                    spars[2] = new SqlParameter("@InvoiceID", SqlDbType.VarChar);
                    spars[2].Value = InvoiceID;
                    spars[3] = new SqlParameter("@MstoneID", SqlDbType.VarChar);
                    spars[3].Value = HDmilestoneid;
                    spars[4] = new SqlParameter("@TobePaidAmtWithtax", SqlDbType.VarChar);
                    spars[4].Value = txtMSInvoiceAmt.Text.Trim();
                    spars[5] = new SqlParameter("@Payment_ID", SqlDbType.Int);
                    spars[5].Value = DTInsertPayment.Rows[0]["Payment_ID"].ToString();
                    DataSet dsApprovalStatusReport = spm.getDatasetList(spars, "sp_VSCB_CreatePOWO_Users");

                }
            }


            if (DTInsertPayment.Rows.Count > 0)
			{
				//Payment_ID = Convert.ToInt32(DTInsertPayment.Rows[0]["Payment_ID"]);
				string strPatmentURL = "", strapproverlist = "";
				spm.Insert_Payment_Approver_Request(Approvers_code, apprid, Payment_ID);
				strPatmentURL = (Convert.ToString(ConfigurationManager.AppSettings["Link_PaymentRequestAPP"]).Trim() + "?Payment_ID=" + Payment_ID);
				strapproverlist = GetPaymnetApprove_RejectList(hdnEmpCpde.Value, Payment_ID, hdnDept_Name.Value, hdnCostCentre.Value);
				string PowoNo = "";
				if (lstPOWONumber.SelectedItem.Text != "Select PO/ WO Number" || lstPOWONumber.SelectedIndex != 0)
				{
					PowoNo = lstPOWONumber.SelectedItem.Text;
				}
				else
				{
					PowoNo = txtInvoiceNo.Text;
				}
				var actionByName = Session["emp_loginName"].ToString();
				//GetProject_CostCenter();
				GetInvoiceTDSAmt();
                //GetMilestoneDetails();
                DataSet dsMilestone = GetMilestoneDetails();
                string CostCenter = Convert.ToString(txtTallyCode_display.Text).Trim(); // txtProjectName.Text;
				string strsubject = "Request for - Payment Approval  “" + txtPaymentRequestNo.Text + "”  For " + PowoNo;
				string TBody = actionByName + " has created a payment request as per the details below. Request your approval please.";
				spm.send_mailto_VSCB_Payment_Request_Approver(nxtapprname, "", approveremailaddress, strsubject, TBody, "", strPatmentURL, strapproverlist, lstPOWONumber.SelectedItem.Text, txtPoWoTitle.Text, txtPoWoDate.Text, txtPoWoType.Text, txtVendorName.Text, txtPoWoAmtWithTaxes.Text, txtPoWOPaidAmt.Text, txtPoWOPaidBalAmt.Text, txtProjectName.Text, txtInvoiceNo.Text, txtInvoiceAmount.Text, txtInvoiceBalAmt.Text, txtPaymentRequestNo.Text, txtRequestDate.Text, txtAmountPaidWithTax.Text, CostCenter, hdnDirectTax_Type.Value, hdnDirectTax_Percentage.Value, hdnDirectTax_Amount.Value, hdnPayable_Amt_Invoice.Value, MstoneID.ToString(), hdnMilestoneName.Value, hdnMilestAmount.Value, hdnMilestCGST_Amt.Value, hdnMilestSGST_Amt.Value, hdnMilestIGST_Amt.Value, hdnMilestAmtWithTax.Value, hdnMilestPyamentStatus.Value, dsMilestone);
			}
			Response.Redirect("~/procs/VSCB_InboxMyPaymentRequest.aspx", false);
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
		dtAppRej = spm.Get_Payment_Request_ApproverList(EmpCode, Payment_ID, DeptName, TallyCode,Convert.ToString(txtPoWoType.Text));
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
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        SqlParameter[] spars = new SqlParameter[3];
        spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
        spars[0].Value = "PaymentRequestMileStoneCanceledUpdate";
        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCpde.Value;
        spars[2] = new SqlParameter("@Payment_ID", SqlDbType.Int);
        spars[2].Value = hdnPayment_ID.Value;
        DataSet dsApprovalStatusReport = spm.getDatasetList(spars, "sp_VSCB_CreatePOWO_Users");
        Response.Redirect("VSCB_InboxMyPaymentRequest.aspx");
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
		DTPoWoNumber = spm.dtPOWoCreate("POWOPaymentRequest", Convert.ToString(hdnEmpCpde.Value));
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
			if (hflStatusID.Value != "5")
			{
				txtAmountPaidWithTax.Enabled = false;
				uploadfile.Visible = false;
                foreach (GridViewRow gvr in GVEditpaymentReqamtpaid.Rows)
                {
                    TextBox txtMilestoneInvoiceAmt = (TextBox)gvr.FindControl("txtMilestoneInvoiceAmt");
                    txtMilestoneInvoiceAmt.Enabled = false;
                }

            }
			else
			{

				uploadfile.Visible = true;
				txtRequestDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
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
        string StrClear = "";
        string strmsg = "";
        for (int i = 0; i <= GVEditpaymentReqamtpaid.Rows.Count - 1; i++)
        {
            GridViewRow ro = GVEditpaymentReqamtpaid.Rows[i];
            TextBox txtMSInvoiceAmt = (TextBox)ro.FindControl("txtMilestoneInvoiceAmt");
            Label lblamountcheck = (Label)ro.FindControl("lblamountcheck");
            string HDmilestoneid = "";
            string HDInvoiceIdd = "";
            
            if (Convert.ToString(txtMSInvoiceAmt.Text).Trim() != "")
            {
                StrClear = "msg";

                if (Convert.ToDouble(lblamountcheck.Text) >= Convert.ToDouble(txtMSInvoiceAmt.Text))
                {
                    HDmilestoneid = Convert.ToString(GVEditpaymentReqamtpaid.DataKeys[ro.RowIndex].Values[0]).Trim();
                    HDInvoiceIdd = Convert.ToString(GVEditpaymentReqamtpaid.DataKeys[ro.RowIndex].Values[1]).Trim();
                    SqlParameter[] spars = new SqlParameter[3];
                    spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
                    spars[0].Value = "PRMileStoneAmountcheck";
                    spars[1] = new SqlParameter("@InvoiceID", SqlDbType.VarChar);
                    spars[1].Value = HDInvoiceIdd;
                    spars[2] = new SqlParameter("@MstoneID", SqlDbType.VarChar);
                    spars[2].Value = HDmilestoneid;
                    DataSet DS = spm.getDatasetList(spars, "sp_VSCB_CreatePOWO_Users");
                    localtrvl_btnSave.Enabled = true;
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        decimal BalAmt = 0;
                        string stramt = DS.Tables[0].Rows[0]["SumAmounttobepaid"].ToString();
                        if (stramt == "")
                        {
                            stramt = "0";
                        }

                        BalAmt = Convert.ToDecimal(lblamountcheck.Text) - Convert.ToDecimal(stramt);
                        if (Convert.ToDecimal(txtMSInvoiceAmt.Text) <= Convert.ToDecimal(BalAmt))
                        {
                            dMilesStoneAmt_forInvoice += Convert.ToDouble(txtMSInvoiceAmt.Text);
                            txtAmountPaidWithTax.Text = Convert.ToDouble(dMilesStoneAmt_forInvoice).ToString();
                            lblmessage.Text = "";
                        }
                        else
                        {
                            txtMSInvoiceAmt.Text = "";
                            localtrvl_btnSave.Enabled = false;
                            lblmessage.Text = "Amount to be Requested cannot exceed milesstone payble invoice amount! ";
                            return;
                        }
                    }
                }
                else
                {
                    txtMSInvoiceAmt.Text = "";
                    strmsg = "Insidemsg";
                }
            }
        }
        if (StrClear == "")
        {
            txtAmountPaidWithTax.Text = "";
        }
        if (strmsg != "")
        {
            lblmessage.Text = "Amount to be Requested cannot exceed milesstone payble invoice amount! ";
            return;
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
			DTPoWoNumber = spm.GetVSCB_MyPaymentDetails("POWOMyPaymentRequestDeatils", Convert.ToString(hdnEmpCpde.Value), POID, Payment_ID);
			if (DTPoWoNumber.Tables[0].Rows.Count > 0)
			{
				txtPoWoDate.Text = DTPoWoNumber.Tables[0].Rows[0]["PODate"].ToString();
				lstPOWONumber.SelectedValue = DTPoWoNumber.Tables[0].Rows[0]["POID"].ToString();
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
			if (DTPoWoNumber.Tables[6].Rows.Count > 0)
			{
				GrdInvoiceDetails.DataSource = DTPoWoNumber.Tables[6];
				GrdInvoiceDetails.DataBind();

            }
            if (DTPoWoNumber.Tables[7].Rows.Count > 0)
            {
                GVEditpaymentReqamtpaid.DataSource = DTPoWoNumber.Tables[7];
                GVEditpaymentReqamtpaid.DataBind();
                GVEditpaymentReqamtpaid.Visible = true;
            }

            if (DTPoWoNumber.Tables[3].Rows.Count > 0)
			{
				//DivCreateInvoice.Visible = true;
				hdnMstoneID.Value = DTPoWoNumber.Tables[3].Rows[0]["MstoneID"].ToString();
				hdnInvoiceID.Value = DTPoWoNumber.Tables[3].Rows[0]["InvoiceID"].ToString();
				txtInvoiceNo.Text = DTPoWoNumber.Tables[3].Rows[0]["InvoiceNo"].ToString();
				txtInvoiceAmount.Text = DTPoWoNumber.Tables[3].Rows[0]["AmtWithTax"].ToString();  //AmtWithTax Payable_Amt_With_Tax
				txtInvoiceBalAmt.Text = DTPoWoNumber.Tables[3].Rows[0]["BalanceAmt"].ToString();
				//txtVendorName.Text = DTCreateInvoice.Rows[0]["Status_id"].ToString();
				//txtGSTINNO.Text = DTCreateInvoice.Rows[0]["PaymentStatusID"].ToString();
				txtPaymentRequestNo.Text = DTPoWoNumber.Tables[3].Rows[0]["PaymentReqNo"].ToString();
				txtRequestDate.Text = DTPoWoNumber.Tables[3].Rows[0]["PaymentReqDate"].ToString();
				txtAmountPaidWithTax.Text = DTPoWoNumber.Tables[3].Rows[0]["TobePaidAmtWithtax"].ToString();
				hdnInvoicePayableAmt.Value = DTPoWoNumber.Tables[3].Rows[0]["Payable_Amt_With_Tax"].ToString();
                lnkfile_Invoice.Text = DTPoWoNumber.Tables[3].Rows[0]["Invoice_File"].ToString();


                txtInvoicePaidAmount.Text= DTPoWoNumber.Tables[3].Rows[0]["AccountPaidAmt"].ToString();
                txtInvoiceTDSAmt.Text = DTPoWoNumber.Tables[3].Rows[0]["DirectTax_Amount"].ToString();
                txtPaymentRequestAmount.Text = DTPoWoNumber.Tables[3].Rows[0]["paymentRequestForAmt"].ToString();

                //hdnpaymentAmt.Value=
                hflStatusID.Value = DTPoWoNumber.Tables[3].Rows[0]["Status_id"].ToString();
				lblheading.Text = "Payment Request View - " + txtPaymentRequestNo.Text + ", Payment Status - " + DTPoWoNumber.Tables[3].Rows[0]["PyamentStatus"].ToString();
				if (Convert.ToInt32(DTPoWoNumber.Tables[3].Rows[0]["Status_id"]) == 5)
				{
					localtrvl_btnSave.Visible = true;
                    spnPaymentSuppotingFiles.Visible = true;
				}
				if (Convert.ToInt32(DTPoWoNumber.Tables[3].Rows[0]["Status_id"]) == 2)
				{
					Account1.Visible = true;
					Account2.Visible = true;
					Account3.Visible = true;
					txtAccountPaidAmt.Text = DTPoWoNumber.Tables[3].Rows[0]["Amt_paid_Account"].ToString();
					txtAccountAmtBal.Text = DTPoWoNumber.Tables[3].Rows[0]["AccountBalAmt"].ToString();
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
			if (DTPoWoNumber.Tables[4].Rows.Count > 0)
			{
                spnPaymentSuppotingFiles.Visible = true;
				GrdFileUpload.DataSource = DTPoWoNumber.Tables[4];
				GrdFileUpload.DataBind();
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
            else
            {
                Span5.Visible = false;
            }

            Payment_ID = Convert.ToString(hdnPayment_ID.Value).Trim() != "" ? Convert.ToInt32(hdnPayment_ID.Value) : 0;
			DTstatus = spm.Get_Payment_AccountApprovalEmp("GetPaymentApprovalStatus","", Payment_ID);
			if (DTstatus.Rows.Count > 0)
			{
				if (Convert.ToString(DTstatus.Rows[0]["Action"]).Trim() == "Pending" || Convert.ToString(DTstatus.Rows[0]["Action"]).Trim() == "Correction")
				{
					this.GrdFileUpload.Columns[1].Visible = true;
				}
			}
			PaymentBalanceAmt();
			getApproverlist(hdnEmpCpde.Value, Payment_ID, hdnDept_Name.Value, hdnCostCentre.Value);
            getInvoiceUploadedFiles();

        }
		catch (Exception ex)
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
        }
    }

    private void getApproverlist(string EmpCode, int Payment_ID, string DeptnName, string TallyCode)
	{
		DataTable dtapprover = new DataTable();
		dtapprover = spm.Get_Payment_Request_ApproverList(EmpCode, Payment_ID, DeptnName, TallyCode,Convert.ToString(txtPoWoType.Text));
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
				txtInvoiceNo.Text = DTPoWoNumber.Tables[0].Rows[0]["InvoiceNo"].ToString();
				txtInvoiceAmount.Text = DTPoWoNumber.Tables[0].Rows[0]["AmtWithTax"].ToString();
				txtInvoiceBalAmt.Text = DTPoWoNumber.Tables[0].Rows[0]["BalanceAmt"].ToString();
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

            if (DTPoWoNumber.Tables[2].Rows.Count > 0)
            {
                GVEditpaymentReqamtpaid.DataSource = DTPoWoNumber.Tables[2];
                GVEditpaymentReqamtpaid.DataBind();
                GVEditpaymentReqamtpaid.Visible = true;
                SetEnableDalseConttols();
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
}