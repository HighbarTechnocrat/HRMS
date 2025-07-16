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

public partial class procs_VSCB_PaymentRequest : System.Web.UI.Page
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

                    // trvldeatils_delete_btn.Visible = false;
                    GetPoWoNumber();
					if (Request.QueryString.Count > 0)
					{
                        lstPOWONumber.Enabled = false;
                        lstPOWONumber.SelectedValue = Request.QueryString[1];
                        int POID = Convert.ToInt32(Request.QueryString[1]);
                        PoWODetailsUsingView(POID, Convert.ToInt32(Request.QueryString[0]));
                        hdnInvoiceID.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        //hdnMstoneID.Value = Convert.ToString(Request.QueryString[2]).Trim();
                        CreateInvoiceDetailsForPayment();
                        getInvoiceUploadedFiles();
                        Check_CostCenterApprovalMatrix();

                        if (Convert.ToString(txtIsSecurity_DepositInvoice.Text).Trim() == "true")
                        {
                            spInvoice.Visible = false;
                            txtAmountPaidWithTax.Enabled = true;
                        }
                        Get_PaymnetHistoryOnInvoiceDepend();
                    }

                }
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}

    public void Get_PaymnetHistoryOnInvoiceDepend()
    {
        DataSet dspaymentHistory = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
        spars[0].Value = "get_PaymnetHistoryOnInvoiceDepend";

        spars[1] = new SqlParameter("@InvoiceID", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(hdnInvoiceID.Value).Trim();

        dspaymentHistory = spm.getDatasetList(spars, "sp_VSCB_CreatePOWO_Users");

        if (dspaymentHistory.Tables[0].Rows.Count > 0)
        {
            GVPaymnetHistoryOnInvoice.DataSource = dspaymentHistory.Tables[0];
            GVPaymnetHistoryOnInvoice.DataBind();
            PaymnetHistoryOnInvoice.Visible = true;
        }
        else
        {
            PaymnetHistoryOnInvoice.Visible = false;
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

        if (dtPOWODetails.Tables[0].Rows.Count <= 0)
        {
            localtrvl_btnSave.Visible = false;
            lblmessage.Text = "Approval Matrix not set. Please contact to Admin";
        }

    }

    protected void localtrvl_btnSave_Click(object sender, EventArgs e)
	{
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        int Payment_ID = 0, MstoneID = 0, InvoiceID = 0;
		decimal BalAmount = 0;
		DateTime PaymentDate;
		string EmpCode = "", strtoDate = "", multiplefilename = "", multiplefilenameadd = "", strfileName = "";
		string[] strdate;
		HttpPostedFile uploadfileName = null;
		try
		{
            #region Check For Blank Fields

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
			if (Convert.ToString(txtAmountPaidWithTax.Text).Trim() != ""  && hdnTobePaidAmt.Value!="")
			{
				decimal BalAmt = 0;
				BalAmt = Convert.ToDecimal(hdnInvoicePayableAmt.Value) - Convert.ToDecimal(hdnTobePaidAmt.Value);
				if (Convert.ToDecimal(txtAmountPaidWithTax.Text) > BalAmt)//txtInvoiceAmount
				{
					lblmessage.Text = "Amount to be paid cannot exceed balance Invoice Amount! ";
					return;
				}
			}

            #region Check the Amount submitted or Not
                    SqlParameter[] spars_P = new SqlParameter[2];
                    spars_P[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                    spars_P[0].Value = "get_submitted_PaymentRestedAmt";

                    spars_P[1] = new SqlParameter("@InvoiceID", SqlDbType.VarChar);
                    spars_P[1].Value = hdnInvoiceID.Value;
                
                DataSet dsSubmittedPaytmentReqAmt = spm.getDatasetList(spars_P, "SP_VSCB_GETALL_DETAILS");
                if (dsSubmittedPaytmentReqAmt != null)
                {
                    if (dsSubmittedPaytmentReqAmt.Tables[0].Rows.Count > 0)
                    {
                          decimal PaymntSubmiitedAmount = 0;
                          decimal dTotalPayableInvAmt = 0;

                     dTotalPayableInvAmt = Convert.ToDecimal(dsSubmittedPaytmentReqAmt.Tables[1].Rows[0]["Payable_Amt_With_Tax"]) ;
                    PaymntSubmiitedAmount = Convert.ToDecimal(dsSubmittedPaytmentReqAmt.Tables[0].Rows[0]["paymentReqtedAmt"]) + Convert.ToDecimal(txtAmountPaidWithTax.Text);
                        if (Convert.ToDecimal(PaymntSubmiitedAmount) > dTotalPayableInvAmt)//txtInvoiceAmount
                        {
                            lblmessage.Text = "Amount to be paid cannot exceed balance Invoice Amount! ";
                            return;
                        }
                    }
                }

            #endregion
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
			dtApproverEmailIds = spm.Get_Payment_Request_ApproverEmailID(EmpCode,hdnDept_Name.Value,hdnCostCentre.Value,0,Convert.ToString(txtPoWoType.Text).Trim());
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
			//BalAmount=Convert.ToDecimal(txtAmountPaidWithTax) - Convert.ToDecimal(txtAmountPaidWithTax);			
			multiplefilenameadd = multiplefilenameadd.TrimEnd(',');
			DTInsertPayment = spm.dtInsert_Payment_Request("PAYMNETINSERTREQUEST", EmpCode, InvoiceID, MstoneID, 0, txtPaymentRequestNo.Text.Trim(), Convert.ToDecimal(txtAmountPaidWithTax.Text), PaymentDate, 1, 1, 0, BalAmount, "", multiplefilenameadd);

            for (int i = 0; i <= GVEditpaymentReqamtpaid.Rows.Count - 1; i++)
            {
                GridViewRow ro = GVEditpaymentReqamtpaid.Rows[i];
                TextBox txtMSInvoiceAmt = (TextBox)ro.FindControl("txtMilestoneInvoiceAmt");
                string HDmilestoneid = "";
                if (Convert.ToString(txtMSInvoiceAmt.Text).Trim() != "")
                {
                    HDmilestoneid = Convert.ToString(GVEditpaymentReqamtpaid.DataKeys[ro.RowIndex].Values[2]).Trim();

                    SqlParameter[] spars = new SqlParameter[6];
                    spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
                    spars[0].Value = "PaymentRequestMileStoneAmount";
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
				Payment_ID = Convert.ToInt32(DTInsertPayment.Rows[0]["Payment_ID"]);
				if (Convert.ToString(Payment_ID).Trim() == "0")
					return;
				string strPatmentURL = "", strapproverlist="";
				string PowoNo = "";
				if (lstPOWONumber.SelectedItem.Text != "Select PO/ WO Number" || lstPOWONumber.SelectedIndex != 0)
				{
					PowoNo = lstPOWONumber.SelectedItem.Text;
				}
				else
				{
					PowoNo = txtInvoiceNo.Text;
				}
				spm.Insert_Payment_Approver_Request(Approvers_code, apprid, Payment_ID);
				strPatmentURL = (Convert.ToString(ConfigurationManager.AppSettings["Link_PaymentRequestAPP"]).Trim() + "?Payment_ID=" + Payment_ID + "&POID="+ lstPOWONumber.SelectedValue);
				strapproverlist = GetPaymnetApprove_RejectList(hdnEmpCpde.Value, Payment_ID, hdnDept_Name.Value, hdnCostCentre.Value);
				var actionByName = Session["emp_loginName"].ToString();
               // GetMilestoneDetails();
                 DataSet dsMilestone = GetMilestoneDetails();

                GetInvoiceTDSAmt();
                string CostCenter = txtTallyCode_display.Text; //hdnCostCentre.Value;
				string strsubject = "OneHR: Request for - Payment Approval “" + txtPaymentRequestNo.Text + "”  For “"+ PowoNo + "”";
				string TBody = actionByName + " has created a payment request as per the details below. Request your approval please.";


                DataSet dsFormatAmount = spm.getFormated_Amount("get_formated_currncy_Amount", Convert.ToString(txtCurrency.Text), Convert.ToDecimal(txtAmountPaidWithTax.Text), 0, 0, 0, 0);
                string sformatPaymentReqAmt = Convert.ToString(dsFormatAmount.Tables[0].Rows[0]["Amount_A"]);
                spm.send_mailto_VSCB_Payment_Request_Approver(nxtapprname, "", approveremailaddress, strsubject, TBody, "", strPatmentURL, strapproverlist,lstPOWONumber.SelectedItem.Text,txtPoWoTitle.Text,txtPoWoDate.Text,txtPoWoType.Text,txtVendorName.Text,txtPoWoAmtWithTaxes.Text,txtPoWOPaidAmt.Text,txtPoWOPaidBalAmt.Text,txtProjectName.Text,txtInvoiceNo.Text,txtInvoiceAmount.Text,txtInvoiceBalAmt.Text,txtPaymentRequestNo.Text, txtRequestDate.Text, sformatPaymentReqAmt, CostCenter, hdnDirectTax_Type.Value, hdnDirectTax_Percentage.Value, hdnDirectTax_Amount.Value,hdnPayable_Amt_Invoice.Value, MstoneID.ToString(), hdnMilestoneName.Value,hdnMilestAmount.Value,hdnMilestCGST_Amt.Value,hdnMilestSGST_Amt.Value,hdnMilestIGST_Amt.Value,hdnMilestAmtWithTax.Value,hdnMilestPyamentStatus.Value,dsMilestone);
			}

            if (Request.QueryString.Count > 0)
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

	protected void lstPOWONumber_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{

			ClearPOWODetails();
			
			if (lstPOWONumber.SelectedIndex > 0)
			{

				int POID = Convert.ToInt32(lstPOWONumber.SelectedValue);
				PoWODetails(POID);
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

			LinkButton btn = (LinkButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			hdnInvoiceID.Value = Convert.ToString(GrdInvoiceDetails.DataKeys[row.RowIndex].Values[1]).Trim();
			hdnMstoneID.Value = Convert.ToString(GrdInvoiceDetails.DataKeys[row.RowIndex].Values[0]).Trim();
			CreateInvoiceDetailsForPayment();
            getInvoiceUploadedFiles();

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


    #endregion
    #region PageMethods

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
	public void GetPoWoNumber()
	{
		DTPoWoNumber = spm.dtPOWoCreate("POWOPaymentRequest", Convert.ToString(hdnEmpCpde.Value));
		lstPOWONumber.DataSource = DTPoWoNumber;
		lstPOWONumber.DataTextField = "PONumber";
		lstPOWONumber.DataValueField = "POID";
		lstPOWONumber.DataBind();
		lstPOWONumber.Items.Insert(0, new ListItem("Select PO/ WO Number", "0"));
	}
	private void ClearPOWODetails()
	{
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

	}

	private void PoWODetails(int POID)
	{
		try
		{
			DataSet DTPoWoNumber = new DataSet();
			DTPoWoNumber = spm.dtPOWoNumberDetails("POWOPaymentRequestDeatils", Convert.ToString(hdnEmpCpde.Value), POID);
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
			if (DTPoWoNumber.Tables[2].Rows.Count > 0)
			{
				spInvoice.Visible = true;
				GrdInvoiceDetails.DataSource = DTPoWoNumber.Tables[2];
				GrdInvoiceDetails.DataBind();
			}
            SPPayHist.Visible = false;
            if (DTPoWoNumber.Tables[3].Rows.Count > 0)
			{
				SPPayHist.Visible = true;
				GrdInvoiceHistDetails.DataSource = DTPoWoNumber.Tables[3];
				GrdInvoiceHistDetails.DataBind();
			}

			getApproverlist(hdnEmpCpde.Value, 0, hdnDept_Name.Value, hdnCostCentre.Value,Convert.ToString(txtPoWoType.Text).Trim());
		}
		catch (Exception)
		{

			throw;
		}
	}

    private void PoWODetailsUsingView(int POID,int INVID)
    {
        try
        {
            DataSet DTPoWoNumber = new DataSet();
            DTPoWoNumber = spm.dtPOWoNumberDetailsPRequestlist("POWOPaymentRequestListInvoice", Convert.ToString(hdnEmpCpde.Value), POID,INVID);
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
            if (DTPoWoNumber.Tables[2].Rows.Count > 0)
            {
                spInvoice.Visible = true;
                GrdInvoiceDetails.DataSource = DTPoWoNumber.Tables[2];
                GrdInvoiceDetails.DataBind();
                if (GrdInvoiceDetails.Columns.Count > 1)
                {
                    GrdInvoiceDetails.Columns[0].Visible = false;
                }

            }

            if (DTPoWoNumber.Tables[4].Rows.Count > 0)
            {
                gvInvoiceMilestone.DataSource = DTPoWoNumber.Tables[4];
                gvInvoiceMilestone.DataBind();

                GVEditpaymentReqamtpaid.DataSource = DTPoWoNumber.Tables[5];
                GVEditpaymentReqamtpaid.DataBind();

                for (int i = 0; i <= GVEditpaymentReqamtpaid.Rows.Count - 1; i++)
                {
                           GridViewRow ro = GVEditpaymentReqamtpaid.Rows[i];
                           TextBox txtMSInvoiceAmt = (TextBox)ro.FindControl("txtMilestoneInvoiceAmt");
                           Label lblamountcheck = (Label)ro.FindControl("lblamountcheck");
                           string HDmilestoneid = "";
                            HDmilestoneid = Convert.ToString(GVEditpaymentReqamtpaid.DataKeys[ro.RowIndex].Values[2]).Trim();
                            SqlParameter[] spars = new SqlParameter[3];
                            spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
                            spars[0].Value = "PRMileStoneAmountcheck";
                            spars[1] = new SqlParameter("@InvoiceID", SqlDbType.VarChar);
                            spars[1].Value = INVID;
                            spars[2] = new SqlParameter("@MstoneID", SqlDbType.VarChar);
                            spars[2].Value = HDmilestoneid;
                            DataSet DS = spm.getDatasetList(spars, "sp_VSCB_CreatePOWO_Users");
                           
                            if (DS.Tables[0].Rows.Count > 0)
                            {
                                decimal BalAmt = 0;
                                string stramt = DS.Tables[0].Rows[0]["SumAmounttobepaid"].ToString();
                                if (stramt != "")
                                {
                                  BalAmt = Convert.ToDecimal(stramt);
                                  if (Convert.ToDecimal(lblamountcheck.Text) == BalAmt)
                                  {
                                    txtMSInvoiceAmt.Enabled = false;
                                  }
                                }
                            }
                     }

            }
            SPPayHist.Visible = false;
            if (DTPoWoNumber.Tables[3].Rows.Count > 0)
            {
                SPPayHist.Visible = true;
                GrdInvoiceHistDetails.DataSource = DTPoWoNumber.Tables[3];
                GrdInvoiceHistDetails.DataBind();
            }
            if (DTPoWoNumber.Tables[6].Rows.Count > 0)
            {
                GrdInvoiceApr.DataSource = DTPoWoNumber.Tables[6];
                GrdInvoiceApr.DataBind();
            }
            else
            {
                Span3.Visible = false;
            }

            getApproverlist(hdnEmpCpde.Value, 0, hdnDept_Name.Value, hdnCostCentre.Value, Convert.ToString(txtPoWoType.Text).Trim());
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
            Label lblamountcheck    = (Label)ro.FindControl("lblamountcheck");
            string HDmilestoneid = "";
            string HDInvoiceIdd = "";
            if (Convert.ToString(txtMSInvoiceAmt.Text).Trim() != "" && Convert.ToString(lblamountcheck.Text).Trim()!="")
            {
                StrClear = "msg";


                if (Convert.ToDouble(lblamountcheck.Text) >= Convert.ToDouble(txtMSInvoiceAmt.Text))
                {
                    HDmilestoneid = Convert.ToString(GVEditpaymentReqamtpaid.DataKeys[ro.RowIndex].Values[2]).Trim();
                    HDInvoiceIdd = hdnInvoiceID.Value;
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
                        //if (BalAmt == Convert.ToDecimal(lblamountcheck.Text))
                        //{
                        //    txtMSInvoiceAmt.Enabled = false;
                        //}
                        //else
                        //{
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
                       // }
                    }
                }
                else
                {
                    txtMSInvoiceAmt.Text = "";
                    strmsg = "Insidemsg";
                }
            }
        }
        if(StrClear =="")
        {
            txtAmountPaidWithTax.Text = "";
        }
        if (strmsg != "")
        {
            lblmessage.Text = "Amount to be Requested cannot exceed milesstone payble invoice amount! ";
            return;
        }
    }

    private void getApproverlist(string EmpCode, int Payment_ID, string DeptnName,string TallyCode,string spotype)
	{
		DataTable dtapprover = new DataTable();     
		dtapprover = spm.Get_Payment_Request_ApproverList(EmpCode, Payment_ID, DeptnName, TallyCode, spotype);
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
		dtAppRej = spm.Get_Payment_Request_ApproverList(EmpCode, Payment_ID, DeptName, TallyCode,Convert.ToString(txtPoWoType.Text).Trim());
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

    private void CreateInvoiceDetailsForPayment()
	{
		try
		{
			DataTable DTCreateInvoice = new DataTable();
			int InvoiceID = 0, MstoneID = 0;
			InvoiceID = Convert.ToString(hdnInvoiceID.Value).Trim() != "" ? Convert.ToInt32(hdnInvoiceID.Value) : 0;
			MstoneID = Convert.ToString(hdnMstoneID.Value).Trim() != "" ? Convert.ToInt32(hdnMstoneID.Value) : 0;
			DTCreateInvoice = spm.dtShowInvoiceDetailsForPayment("POWOPayment_Invoice_Deatils", Convert.ToString(hdnEmpCpde.Value), InvoiceID, MstoneID);
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

                txtPaymentRequestAmount.Text = DTCreateInvoice.Rows[0]["paymentRequestForAmt"].ToString(); //DirectTax_Amount
                txtInvoiceTDSAmt.Text = DTCreateInvoice.Rows[0]["DirectTax_Amount"].ToString();
                txtInvoicePaidAmount.Text = DTCreateInvoice.Rows[0]["InvoicePaidAmt"].ToString(); //InvoicePaidAmt;
                lnkfile_Invoice.Text = DTCreateInvoice.Rows[0]["Invoice_File"].ToString();

                txtIsSecurity_DepositInvoice.Text =Convert.ToString( DTCreateInvoice.Rows[0]["IsSecurity_Deposit_Invoice"]).Trim().ToLower();
                 

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

	private void GetProject_CostCenter()
	{
		try
		{
		DataTable dtCostCenter = new DataTable();
		dtCostCenter = spm.Get_Project_CostCenter("GetDeptCostCenter", hdnEmpCpde.Value, txtProjectName.Text.Trim());
		if(dtCostCenter.Rows.Count>0)
		{
			hdnCostCentre.Value = dtCostCenter.Rows[0]["CostCentre"].ToString();
		}
		}
		catch (Exception)
		{

			throw;
		}
	}
	private void GetMilestoneDetails_old()
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
	#endregion



	protected void lnkView_Click1(object sender, EventArgs e)
	{

	}

	protected void accmo_delete_btn_Click(object sender, EventArgs e)
	{
		//ClearPOWODetails();
		SPCreate.InnerText = "Create Payment Request";
		localtrvl_btnSave.Visible = true;
		txtAmountPaidWithTax.Enabled = true;
		txtAmountPaidWithTax.Text = "";
		Account1.Visible = false;
		Account2.Visible = false;
		Account3.Visible = false;
		GrdFileUpload.DataSource = null;
		GrdFileUpload.DataBind();
		CreateInvoiceDetailsForPayment();
		accmo_delete_btn.Visible = false;
		

	}
	protected void GrdInvoiceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		try
		{
			DataTable DTCreateInvoice = new DataTable();
			Decimal PaymentAmt = 0,InvoiceAmt=0;
			string abcd = "";
 			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				int InvoiceID = Convert.ToInt32(GrdInvoiceDetails.DataKeys[e.Row.RowIndex].Values[1]);
				int MstoneID = Convert.ToInt32(GrdInvoiceDetails.DataKeys[e.Row.RowIndex].Values[0]);
				InvoiceAmt =Convert.ToDecimal(e.Row.Cells[13].Text);
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