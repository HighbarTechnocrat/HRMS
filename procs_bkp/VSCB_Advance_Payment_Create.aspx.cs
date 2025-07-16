using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class procs_VSCB_Advance_Payment_Create : System.Web.UI.Page
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
					txtAmountPaidWithTax.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VendorPaymentDocpath"]).Trim());
					FilePathInvoice.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBInvoicefiles"]).Trim());
					FilePathPOWOSignCopy.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBSignPOWOfiles"]).Trim());
					GetPoWoNumber();
					GetPaymentType();
					if (Request.QueryString.Count > 0)
					{
						//lstPOWONumber.Enabled = false;
						//lstPOWONumber.SelectedValue = Request.QueryString[1];
						int POID = Convert.ToInt32(Request.QueryString[1]);
						//PoWODetailsUsingView(POID, Convert.ToInt32(Request.QueryString[0]));
						//hdnInvoiceID.Value = Convert.ToString(Request.QueryString[0]).Trim();
						//Create_Advance_Payment_POWO();
						Check_CostCenterApprovalMatrix();
					}

				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}

	protected void lstPOWONumber_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{

			ClearPOWODetails();

			if (lstPOWONumber.SelectedIndex > 0)
			{

				int POID = Convert.ToInt32(lstPOWONumber.SelectedValue);
                GetPaymentType();
				PoWODetails(POID);
				Create_Advance_Payment_POWO();
				Check_CostCenterApprovalMatrix();
			}

		}
		catch (Exception)
		{

			throw;
		}

	}

	protected void localtrvl_btnSave_Click(object sender, EventArgs e)
	{
		string confirmValue = hdnYesNo.Value.ToString();
		if (confirmValue != "Yes")
		{
			return;
		}

		int Payment_ID = 0, MstoneID = 0, InvoiceID = 0, POWOID = 0, AdvancePayTypeID = 0;
		decimal BalAmount = 0;
		DateTime PaymentDate;
		string EmpCode = "", strtoDate = "", multiplefilename = "", multiplefilenameadd = "", strfileName = "";
		string[] strdate;
		HttpPostedFile uploadfileName = null;
		try
		{
			#region Check For Blank Fields

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
			if (Convert.ToString(txtAmountPaidWithoutTax.Text).Trim() == "" || Convert.ToString(txtAmountPaidWithoutTax.Text).Trim() == "0")
			{
				lblmessage.Text = "Please enter Advance Payment Amount (Without GST)";
				return;
			}
			//if (Convert.ToString(txtAmountPaidWithTax.Text).Trim() != "" && Convert.ToString(txtPoWOPaidBalAmt.Text).Trim() != "")
			//{
			//	if (Convert.ToDecimal(txtAmountPaidWithTax.Text) > Convert.ToDecimal(txtPoWOPaidBalAmt.Text))
			//	{
			//		lblmessage.Text = "Amount to be paid cannot exceed balance PO/WO Amount! ";
			//		return;
			//	}
			//}
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
            if (Convert.ToString(uploadfile.FileName).Trim() == "")
            {
                lblmessage.Text = "Please Upload Advance Payment Supporting Files";
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
			POWOID = Convert.ToString(lstPOWONumber.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPOWONumber.SelectedValue) : 0;
			AdvancePayTypeID = Convert.ToString(lstPaymentType.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPaymentType.SelectedValue) : 0;
			multiplefilenameadd = multiplefilenameadd.TrimEnd(',');

            if(chkIsAdvAmtWithGST.Checked==false)
            {
                txtAmountPaidWithTax.Text = txtAmountPaidWithoutTax.Text;
            }
			DTInsertPayment = spm.Insert_Advance_Pay_Request("ADVANCEPAYINSERTREQUEST", EmpCode, POWOID, InvoiceID, AdvancePayTypeID, 0, txtPaymentRequestNo.Text.Trim(), Convert.ToDecimal(txtAmountPaidWithTax.Text), PaymentDate, 1, 1, 0, BalAmount, "", multiplefilenameadd, txtRemark.Text.Trim(),Convert.ToDouble(txtAmountPaidWithoutTax.Text),Convert.ToString(txtCGST_Amt.Text).Trim(), Convert.ToString(txtSGST_Amt.Text).Trim(), Convert.ToString(txtIGST_Amt.Text).Trim());
			if (DTInsertPayment.Rows.Count > 0)
			{
				Payment_ID = Convert.ToInt32(DTInsertPayment.Rows[0]["Payment_ID"]);
				if (Convert.ToString(Payment_ID).Trim() == "0")
					return;

				string strPatmentURL = "", strapproverlist = "";
				string PowoNo = "";
				if (lstPOWONumber.SelectedItem.Text != "Select PO/ WO Number" || lstPOWONumber.SelectedIndex != 0)
				{
					PowoNo = lstPOWONumber.SelectedItem.Text;
				}
				else
				{
					PowoNo = txtInvoiceNo.Text;
				}

				spm.Insert_Advance_Pay_Approver_Request("INSERTADVANCE", Approvers_code, apprid, Payment_ID, "Pending", "", "");

				strPatmentURL = (Convert.ToString(ConfigurationManager.AppSettings["Link_AD_PayReqAPP"]).Trim() + "?Payment_ID=" + Payment_ID + "&POID=" + lstPOWONumber.SelectedValue + "&Type=Pending");
				strapproverlist = GetPaymnetApprove_RejectList(hdnEmpCpde.Value, Payment_ID, hdnDept_Name.Value, hdnCostCentre.Value);
				var actionByName = Session["emp_loginName"].ToString();
				// GetMilestoneDetails();
				DataSet dsMilestone = new DataSet();
				//GetInvoiceTDSAmt();
				string CostCenter = txtTallyCode_display.Text; //hdnCostCentre.Value;

                string sAdvSecType = "Advance Payment";
                if(AdvancePayTypeID==2)
                    sAdvSecType = "Security Deposit Payment";


                string strsubject = "OneHR: Request for - "+ sAdvSecType + " Approval “" + txtPaymentRequestNo.Text + "”  For “" + PowoNo + "”";
				string TBody = actionByName + " has created a "+ sAdvSecType + " request as per the details below. Request your approval please.";


				DataSet dsFormatAmount = spm.getFormated_Amount("get_formated_currncy_Amount", Convert.ToString(txtCurrency.Text), Convert.ToDecimal(txtAmountPaidWithTax.Text), 0, 0, 0, 0);
				string sformatPaymentReqAmt = Convert.ToString(dsFormatAmount.Tables[0].Rows[0]["Amount_A"]);
				spm.send_mailto_VSCB__Advance_Pay_Approver(nxtapprname, "", approveremailaddress, strsubject, TBody, "", strPatmentURL, strapproverlist, lstPOWONumber.SelectedItem.Text, txtPoWoTitle.Text, txtPoWoDate.Text, txtPoWoType.Text, txtVendorName.Text, txtPoWoAmtWithTaxes.Text, txtPoWOPaidAmt.Text, txtPoWOPaidBalAmt.Text, txtProjectName.Text, txtInvoiceNo.Text, txtInvoiceAmount.Text, txtInvoiceBalAmt.Text, txtPaymentRequestNo.Text, txtRequestDate.Text, sformatPaymentReqAmt, lstPaymentType.SelectedItem.Text, txtRemark.Text.Trim(), CostCenter, hdnDirectTax_Type.Value, hdnDirectTax_Percentage.Value, hdnDirectTax_Amount.Value, hdnPayable_Amt_Invoice.Value, MstoneID.ToString(), hdnMilestoneName.Value, hdnMilestAmount.Value, hdnMilestCGST_Amt.Value, hdnMilestSGST_Amt.Value, hdnMilestIGST_Amt.Value, hdnMilestAmtWithTax.Value, hdnMilestPyamentStatus.Value, dsMilestone);
			}
				Response.Redirect("~/procs/VSCB_InboxMy_Adv_PayRequest.aspx", false);
		}
		catch (Exception ex)
		{

			throw;
		}

	}

	protected void lnkViewhist_Click(object sender, ImageClickEventArgs e)
	{

	}

	protected void lnkView_Click(object sender, EventArgs e)
	{

	}

	protected void GrdInvoiceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
	{

	}
	#endregion
	
    #region PageMethods
	public void GetPoWoNumber()
	{
		DTPoWoNumber = spm.dtPOWoCreate("POWO_Advance_Pay_Request", Convert.ToString(hdnEmpCpde.Value));
		lstPOWONumber.DataSource = DTPoWoNumber;
		lstPOWONumber.DataTextField = "PONumber";
		lstPOWONumber.DataValueField = "POID";
		lstPOWONumber.DataBind();
		lstPOWONumber.Items.Insert(0, new ListItem("Select PO/ WO Number", "0"));
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
	private void ClearPOWODetails()
	{
        lblmessage.Text = "";

        txtPoWoDate.Text = "";
		txtPoWoTitle.Text = "";
		txtPoWoType.Text = "";
		txtVendorName.Text = "";
		txtGSTINNO.Text = "";
		txtPoWoStatus.Text = "";
		txtPoWoAmtWithTaxes.Text = "";
		txtPoWOPaidAmt.Text = "";
		txtPoPaidAmt_WithOutDT.Text = "";

		txtPoWOPaidBalAmt.Text = "";
		txtProjectName.Text = "";
		txtPODirectTaxAmt.Text = "";
		txtDepartment.Text = "";
		DgvMilestones.DataSource = null;
		DgvMilestones.DataBind();
		GrdInvoiceDetails.DataSource = null;
		GrdInvoiceDetails.DataBind();
		GrdInvoiceHistDetails.DataSource = null;
		GrdInvoiceHistDetails.DataBind();
		spInvoice.Visible = false;
		spMilestones.Visible = false;

        txtAmountPaidWithoutTax.Text = "";
        chkIsAdvAmtWithGST.Checked = false;
        txtAmountPaidWithTax.Text = "";
        txtCGST_Amt.Text = "";
        txtCGST_Amt.Text = "";
        txtIGST_Amt.Text = "";

        liAdvAmtWithGSt_1.Visible = false;
        liAdvAmtWithGSt_2.Visible = false;
        liAdvAmtWithGSt_3.Visible = false;

        liGSTAmt_1.Visible = false;
        liGSTAmt_2.Visible = false;
        liGSTAmt_3.Visible = false;



    }
	private void PoWODetails(int POID)
	{
		try
		{
			DataSet DTPoWoNumber = new DataSet();
			DTPoWoNumber = spm.dtPOWoNumberDetailsPRequestlist("POWO_Advance_PaymentRequestList", Convert.ToString(hdnEmpCpde.Value), POID, 0);
			if (DTPoWoNumber.Tables[0].Rows.Count > 0)
			{
				txtPoWoDate.Text = DTPoWoNumber.Tables[0].Rows[0]["PODate"].ToString();
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
				txtTallyCode_display.Text = DTPoWoNumber.Tables[0].Rows[0]["Tallycode"].ToString();
				//hdnTallyCode.Value = DTPoWoNumber.Tables[0].Rows[0]["Project_Name"].ToString();
				hdnDept_Name.Value = DTPoWoNumber.Tables[0].Rows[0]["Department"].ToString();

				txtCurrency.Text = Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["CurName"]);
				txtPOWOSettelmentAmt.Text = Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["PO_SettelmentAmt"]).Trim();

                txtHPOType.Text = Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["HPOTypeName"]).Trim();
                divScurity_Diposit.Visible = false;
                if (Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["PO_Security_Deposit_Amt"]).Trim() != "")
                {
                    txtSecurity_DepositAmt.Text = Convert.ToString(DTPoWoNumber.Tables[0].Rows[0]["PO_Security_Deposit_Amt"]).Trim();
                    if (Convert.ToDecimal(DTPoWoNumber.Tables[0].Rows[0]["PO_Security_Deposit_Amt"]) > 0)
                    {
                        divScurity_Diposit.Visible = true;
                    }
                }

                lnkfile_PO.Text = "";
				spnPOWOSignCopy.Visible = false;
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
				spMilestones.Visible = true;
			}
			

			 
			SPPayHist.Visible = false;
            #region Invoice History grid not required
           /* if (DTPoWoNumber.Tables[2].Rows.Count > 0)
			{
				SPPayHist.Visible = true;
				GrdInvoiceHistDetails.DataSource = DTPoWoNumber.Tables[2];
				GrdInvoiceHistDetails.DataBind();
			}*/

            #endregion

            #region check GST Application for Selected PO

            hdnCGST_Per.Value = "0";
            hdnSGST_Per.Value = "0";
            hdnIGST_Per.Value = "0";
            chkIsAdvAmtWithGST.Visible = false;
            if (DTPoWoNumber.Tables[3].Rows.Count > 0)
            {
                if (Convert.ToString(DTPoWoNumber.Tables[3].Rows[0]["CGST_Per"]).Trim() != "")
                {
                    hdnCGST_Per.Value = Convert.ToString(DTPoWoNumber.Tables[3].Rows[0]["CGST_Per"]).Trim();
                    chkIsAdvAmtWithGST.Visible = true;

                }
                if (Convert.ToString(DTPoWoNumber.Tables[3].Rows[0]["SGST_Per"]).Trim() != "")
                {
                    hdnSGST_Per.Value = Convert.ToString(DTPoWoNumber.Tables[3].Rows[0]["SGST_Per"]).Trim();
                    chkIsAdvAmtWithGST.Visible = true;


                }
                if (Convert.ToString(DTPoWoNumber.Tables[3].Rows[0]["IGST_Per"]).Trim() != "")
                {
                    hdnIGST_Per.Value = Convert.ToString(DTPoWoNumber.Tables[3].Rows[0]["IGST_Per"]).Trim();
                    chkIsAdvAmtWithGST.Visible = true;

                }
            }
            #endregion

            #region get Already Process invoice Amount
            hdnAleadyProcess_totalInvoiceAmt.Value = "0";

            if (DTPoWoNumber.Tables[4].Rows.Count > 0)
            {
                if (Convert.ToString(DTPoWoNumber.Tables[4].Rows[0]["poBalAmt"]).Trim() != "")
                {
                    hdnAleadyProcess_totalInvoiceAmt.Value = Convert.ToString(DTPoWoNumber.Tables[4].Rows[0]["poBalAmt"]).Trim();

                }

            }
            #endregion

            getApproverlist(hdnEmpCpde.Value, 0, hdnDept_Name.Value, hdnCostCentre.Value, Convert.ToString(txtPoWoType.Text).Trim());
		}
		catch (Exception)
		{

			throw;
		}
	}
	public void Check_CostCenterApprovalMatrix()
	{
		DataSet dtPOWODetails = new DataSet();
		SqlParameter[] spars = new SqlParameter[3];

		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "get_Selected_TallyCode_Payment_Approval_Matrix_P";

		spars[1] = new SqlParameter("@Project_Dept_Name", SqlDbType.VarChar);
		spars[1].Value = Convert.ToString(txtTallyCode_display.Text).Trim();

		spars[2] = new SqlParameter("@POType", SqlDbType.VarChar);
		if (Convert.ToString(txtPoWoType.Text).Trim() != "")
			spars[2].Value = Convert.ToString(txtPoWoType.Text);
		else
			spars[2].Value = DBNull.Value;


		dtPOWODetails = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

		localtrvl_btnSave.Visible = true;
		lblmessage.Text = "";
		if (dtPOWODetails.Tables[0].Rows.Count <= 0)
		{
			localtrvl_btnSave.Visible = false;
			lblmessage.Text = "Approval Matrix not set. Please contact to Admin";
		}

	}
	private void Create_Advance_Payment_POWO()
	{
		try
		{
			DataTable DTCreateInvoice = new DataTable();
			int InvoiceID = 0, MstoneID = 0;
			InvoiceID = Convert.ToString(hdnInvoiceID.Value).Trim() != "" ? Convert.ToInt32(hdnInvoiceID.Value) : 0;
			//MstoneID = Convert.ToString(hdnMstoneID.Value).Trim() != "" ? Convert.ToInt32(hdnMstoneID.Value) : 0;
			MstoneID= Convert.ToString(lstPOWONumber.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPOWONumber.SelectedValue) : 0;
			DTCreateInvoice = spm.dtShowInvoiceDetailsForPayment("POWO_Advance_Payment_Create", Convert.ToString(hdnEmpCpde.Value), InvoiceID, MstoneID);
			if (DTCreateInvoice.Rows.Count > 0)
			{
				DivCreateInvoice.Visible = true;
				//txtInvoiceNo.Text = DTCreateInvoice.Rows[0]["InvoiceNo"].ToString();
				//txtInvoiceAmount.Text = DTCreateInvoice.Rows[0]["AmtWithTax"].ToString();//AmtWithTax Payable_Amt_With_Tax
				//txtInvoiceBalAmt.Text = DTCreateInvoice.Rows[0]["BalanceAmt"].ToString();
				txtPaymentRequestNo.Text = DTCreateInvoice.Rows[0]["PaymentRequestNo"].ToString();
				hdnTobePaidAmt.Value = DTCreateInvoice.Rows[0]["TobePaidAmtWithtax"].ToString();
				//hdnInvoicePayableAmt.Value = DTCreateInvoice.Rows[0]["Payable_Amt_With_Tax"].ToString();
				txtRequestDate.Text = DateTime.Now.ToString("dd-MM-yyyy");

				//txtPaymentRequestAmount.Text = DTCreateInvoice.Rows[0]["paymentRequestForAmt"].ToString(); //DirectTax_Amount
				//txtInvoiceTDSAmt.Text = DTCreateInvoice.Rows[0]["DirectTax_Amount"].ToString();
				//txtInvoicePaidAmount.Text = DTCreateInvoice.Rows[0]["InvoicePaidAmt"].ToString(); //InvoicePaidAmt;
				//lnkfile_Invoice.Text = DTCreateInvoice.Rows[0]["Invoice_File"].ToString();

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
				txtPaymentRequestAmount.Text = "";
				txtInvoicePaidAmount.Text = "";

			}

		}
		catch (Exception)
		{

			throw;
		}
	}
	private void getApproverlist(string EmpCode, int Payment_ID, string DeptnName, string TallyCode, string spotype)
	{
		DataTable dtapprover = new DataTable();
		dtapprover = spm.Get_Advance_Pay_ApproverEmailID("Get_Advance_Pay_ApprovalList", EmpCode,  DeptnName, TallyCode, Payment_ID, spotype);
		DgvApprover.DataSource = null;
		DgvApprover.DataBind();
		if (dtapprover.Rows.Count > 0)
		{
			DgvApprover.DataSource = dtapprover;
			DgvApprover.DataBind();
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

    public void get_AlreadyProcess_TotalInvoiceAmount()
    {
        DataSet dtPOWODetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Selected_TallyCode_Payment_Approval_Matrix_P";

        spars[1] = new SqlParameter("@POID", SqlDbType.BigInt);
        spars[1].Value = Convert.ToString(txtTallyCode_display.Text).Trim(); 

        dtPOWODetails = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        
        if (dtPOWODetails.Tables[0].Rows.Count <= 0)
        {
            
        }

    }

    public DataSet  get_Secuirty_DepositAmount_forCheck()
    {
       // double dSecurityBalanceAmt = 0;

        DataSet dtPOWODetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Security_DepoitAmt";

        spars[1] = new SqlParameter("@POID", SqlDbType.BigInt);
        spars[1].Value = Convert.ToString(lstPOWONumber.SelectedValue).Trim();

        spars[2] = new SqlParameter("@costcenter", SqlDbType.VarChar);
        spars[2].Value = Convert.ToString(ConfigurationManager.AppSettings["vscb_SecurityDeposit"]).Trim();

        dtPOWODetails = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");


        //if (dtPOWODetails.Tables[1].Rows.Count >= 0)
        //{
        //    dSecurityBalanceAmt = Convert.ToDouble(dtPOWODetails.Tables[1].Rows[0]["TobePaidAmtWithtax"]);
        //}
        //return dSecurityBalanceAmt;

        return dtPOWODetails;

    }
    #endregion




    protected void chkIsAdvAmtWithGST_CheckedChanged(object sender, EventArgs e)
    {
        if (chkIsAdvAmtWithGST.Checked)
        {
            liGSTAmt_1.Visible = true;
            liGSTAmt_2.Visible = true;
            liGSTAmt_3.Visible = true;

            liAdvAmtWithGSt_1.Visible = true;
            liAdvAmtWithGSt_2.Visible = true;
            liAdvAmtWithGSt_3.Visible = true;
            CalculateGSTAmount();
        }
        else
        {
            liGSTAmt_1.Visible = false;
            liGSTAmt_2.Visible = false;
            liGSTAmt_3.Visible = false;

            liAdvAmtWithGSt_1.Visible = false;
            liAdvAmtWithGSt_2.Visible = false;
            liAdvAmtWithGSt_3.Visible = false;
        }
    }

    private void CalculateGSTAmount()
    {
        lblmessage.Text = "";
        double dAdvAmount = 0;
        double dCGSTAmt = 0;
        double dSGSTAmt = 0;
        double dIGSTAmt = 0;

        double dGstPer = 0;
        if(Convert.ToString(txtAmountPaidWithoutTax.Text).Trim()=="")
        {
            return;
        }

        dAdvAmount = Convert.ToDouble(txtAmountPaidWithoutTax.Text);

        //get Secuerity Deposit Payment request created?
        if (Convert.ToString(lstPaymentType.SelectedValue).Trim() == "2")
        {
            DataSet dsSecuiryDeposit = new DataSet();
            dsSecuiryDeposit = get_Secuirty_DepositAmount_forCheck();

            double dSecuirtyDepositAmt = 0;
            double dSecuirtyDepositAmt_Submitted = 0; 
            double dSecuirtyDepositAmt_Balance = 0;

            if (dsSecuiryDeposit.Tables[0].Rows.Count > 0)
            {
                dSecuirtyDepositAmt = Convert.ToDouble(dsSecuiryDeposit.Tables[0].Rows[0]["AmtWithTax"]);
            }

            if (dsSecuiryDeposit.Tables[1].Rows.Count>0)
            {
                dSecuirtyDepositAmt_Submitted = Convert.ToDouble(dsSecuiryDeposit.Tables[1].Rows[0]["TobePaidAmtWithtax"]);
            }
            dSecuirtyDepositAmt_Balance = dSecuirtyDepositAmt - dSecuirtyDepositAmt_Submitted;

            if (dAdvAmount > dSecuirtyDepositAmt_Balance)
            {
                txtAmountPaidWithoutTax.Text = "";
                lblmessage.Text = "Security Deposit Amount cannot be exceed than PO/WO Security Deposit Amount.";
                return;
            }

        }

        //Check this Balance amount for Only if It's Advance Payment Request
        if (Convert.ToString(lstPaymentType.SelectedValue).Trim() != "2")
        {
            if (Convert.ToString(hdnAleadyProcess_totalInvoiceAmt.Value).Trim() != "")
            {
                double dTotalProcessInvoiceAmt = 0;

                dTotalProcessInvoiceAmt = Convert.ToDouble(hdnAleadyProcess_totalInvoiceAmt.Value);

                if (dAdvAmount > dTotalProcessInvoiceAmt)
                {
                    txtAmountPaidWithoutTax.Text = "";
                    lblmessage.Text = "Advance Payment Amount cannot be exceed balance PO/WO Amount (" + Convert.ToString(hdnAleadyProcess_totalInvoiceAmt.Value).Trim() + ")   ";
                    return;
                }

            }
        }

        if (chkIsAdvAmtWithGST.Checked)
        {
            if (Convert.ToString(hdnCGST_Per.Value).Trim() != "")
            {
                dGstPer = Convert.ToDouble(hdnCGST_Per.Value);
                dCGSTAmt = Convert.ToDouble(dAdvAmount * dGstPer / 100);
                if (dCGSTAmt > 0)
                    txtCGST_Amt.Text = Convert.ToString(dCGSTAmt).Trim();
            }
            if (Convert.ToString(hdnSGST_Per.Value).Trim() != "")
            {
                dGstPer = Convert.ToDouble(hdnSGST_Per.Value);
                dSGSTAmt = Convert.ToDouble(dAdvAmount * dGstPer / 100);
                if (dSGSTAmt > 0)
                    txtSGST_Amt.Text = Convert.ToString(dSGSTAmt).Trim();
            }
            if (Convert.ToString(hdnIGST_Per.Value).Trim() != "")
            {
                dGstPer = Convert.ToDouble(hdnIGST_Per.Value);
                dIGSTAmt = Convert.ToDouble(dAdvAmount * dGstPer / 100);
                if (dIGSTAmt > 0)
                    txtIGST_Amt.Text = Convert.ToString(dIGSTAmt).Trim();
            }
        }
        txtAmountPaidWithTax.Text = Convert.ToString(dAdvAmount + dCGSTAmt + dSGSTAmt + dIGSTAmt);
    }

    protected void txtAmountPaidWithoutTax_TextChanged(object sender, EventArgs e)
    { 
       CalculateGSTAmount(); 
    }

    protected void lstPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(lstPaymentType.SelectedValue).Trim() != "" && Convert.ToString(lstPaymentType.SelectedValue).Trim() != "0")
        {
            if(Convert.ToString(lstPaymentType.SelectedValue).Trim() == "2")
            {
                txtIGST_Amt.Text = "";
                txtSGST_Amt.Text = "";
                txtCGST_Amt.Text = "";
                chkIsAdvAmtWithGST.Visible = false;
                txtAmountPaidWithoutTax.Text = "";
                txtAmountPaidWithTax.Text ="";
                liGSTAmt_1.Visible = false;
                liGSTAmt_2.Visible = false;
                liGSTAmt_3.Visible = false;

                liAdvAmtWithGSt_1.Visible = false;
                liAdvAmtWithGSt_2.Visible = false;
                liAdvAmtWithGSt_3.Visible = false;
            }
            else
            {
                txtIGST_Amt.Text = "";
                txtSGST_Amt.Text = "";
                txtCGST_Amt.Text = "";
                chkIsAdvAmtWithGST.Visible = true;
                chkIsAdvAmtWithGST.Checked = false;
                txtAmountPaidWithoutTax.Text = "";
                txtAmountPaidWithTax.Text = "";
            }

        }


    }
}