using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;

public partial class procs_VSCB_AssignBankRefApprovedbatchView : System.Web.UI.Page
{
    #region Creative_Default_methods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    public DataTable dtEmp;
    OleDbConnection oConnstr;

    SP_Methods spm = new SP_Methods();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    string strempcode = "";
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void lnkcont_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/SampleForm");
    }

    #endregion

    #region Page_Events

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }


            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");


            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/vscb_index");
            }
            else
            {

                lblmessage.Text = "";
                Page.SmartNavigation = true;
                txtEmpCode.Text = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    PopulateEmployeeData();
                    if (Request.QueryString.Count > 0)
                    {
                        hdnPOWOID.Value = Convert.ToString(Request.QueryString["batchid"]).Trim();
                        get_Batch_details();
                        idBankRef_1.Visible = true;
                        idBankRef_2.Visible = true;
                        idBankRef_3.Visible = true;
                    }
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }



    #endregion

    #region Page Methods

    public void PopulateEmployeeData()
    {
        try
        {
            dtEmp = spm.GetEmployeeData(txtEmpCode.Text);
            if (dtEmp.Rows.Count > 0)
            {
                lpm.Emp_Code = txtEmpCode.Text;
                lpm.Emp_Status = (string)dtEmp.Rows[0]["Emp_status"];
                lpm.Emp_Name = (string)dtEmp.Rows[0]["Emp_Name"];
                txtEmpName.Text = (string)dtEmp.Rows[0]["Emp_Name"];
                txtbatchCreatedBy.Text = (string)dtEmp.Rows[0]["Emp_Name"];
                lpm.Designation_name = (string)dtEmp.Rows[0]["DesginationName"];
                lpm.department_name = (string)dtEmp.Rows[0]["Department_Name"];
                lpm.Grade = Convert.ToString(dtEmp.Rows[0]["Grade"]).Trim();
                lpm.EmailAddress = (string)dtEmp.Rows[0]["Emp_EmailAddress"];

                hflGrade.Value = lpm.Grade;
                hflEmailAddress.Value = lpm.EmailAddress;
                hflEmpName.Value = lpm.Emp_Name;
                hflEmpDepartment.Value = lpm.department_name;
                hflEmpDesignation.Value = lpm.Designation_name;

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }

    public void get_Batch_details()
    {
        try
        {
            DataSet dsList = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_myBatch_Details";

            spars[1] = new SqlParameter("@POWOID", SqlDbType.BigInt);
            spars[1].Value = Convert.ToDouble(hdnPOWOID.Value);

            dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
            gvMngPaymentList_Batch.DataSource = null;
            gvMngPaymentList_Batch.DataBind();
            if (dsList.Tables[0].Rows.Count > 0)
            {
                txtbatchCreateDate.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_Date"]).Trim();
                txtbatchCreatedBy.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Name"]).Trim();
                txtbatchNo.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_No"]).Trim();
                txtbatchNoOfRequest.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_No_Requests"]).Trim();
                txtbatchTotalPayment.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Batch_Total_Payament"]).Trim();
                txtBankname.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Name"]);
                txtBankRefNo.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Ref_no"]).Trim();
                txtBankRefDate.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Bank_Ref_Date"]).Trim();
                txtbatchNo.Visible = true;
                spnBatchNo.Visible = true;
                if (dsList.Tables[1].Rows.Count > 0)
                {
                    gvMngPaymentList_Batch.DataSource = dsList.Tables[1];
                    gvMngPaymentList_Batch.DataBind();

                    gvMngPaymentList_Batch.Columns[0].Visible = false;
                    gvMngPaymentList_Batch.Visible = true;

                    foreach (GridViewRow gvrow in gvMngPaymentList_Batch.Rows)
                    {
                        TextBox TxtPayRefNo = (TextBox)gvrow.FindControl("TxtPaymentRefNo");
                        if (TxtPayRefNo.Text != "")
                        {
                            TxtPayRefNo.Enabled = false;
                        }
                    }
                }
                if (dsList.Tables[3].Rows.Count > 0)
                {
                    HDPaymentRelDate.Value = Convert.ToString(dsList.Tables[3].Rows[0]["Paymentreldate"]).Trim();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    #endregion

    protected void gvMngPaymentList_Batch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // if(Request.QueryString.Count>0)
        // e.Row.Cells[10].Visible = false;
    }
    protected void gvMngPaymentList_Batch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMngPaymentList_Batch.PageIndex = e.NewPageIndex;

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString();
            if (confirmValue != "Yes")
            {
                return;
            }

            #region Validation

            lblmessage.Text = ""; 
            foreach (GridViewRow gvrow in gvMngPaymentList_Batch.Rows)
            {
                TextBox TxtPayRefNo = (TextBox)gvrow.FindControl("TxtPaymentRefNo");
                Label HFEmail = (Label)gvrow.FindControl("HFEmail");
                Label HDVendorName = (Label)gvrow.FindControl("HDVendorName");
                string strEmailAddress = HFEmail.Text;
                string strVendorName = HDVendorName.Text;
                if (TxtPayRefNo.Text.Trim() == "")
                {
                    //lblmessage.Text = "Please Enter the Payment Ref. No.";
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ASD", "StopLoader();", true);
                    //return;
                }
            }


            #endregion
             
            foreach (GridViewRow gvrow in gvMngPaymentList_Batch.Rows)
            {
                string strPOID = Convert.ToString(gvMngPaymentList_Batch.DataKeys[gvrow.RowIndex].Values[0]).Trim();
                string strPayment_ID = Convert.ToString(gvMngPaymentList_Batch.DataKeys[gvrow.RowIndex].Values[2]).Trim();
                string strInvoice_ID = Convert.ToString(gvMngPaymentList_Batch.DataKeys[gvrow.RowIndex].Values[3]).Trim();
                string strPaymentType_Id = Convert.ToString(gvMngPaymentList_Batch.DataKeys[gvrow.RowIndex].Values[4]).Trim();
                TextBox TxtPayRefNo = (TextBox)gvrow.FindControl("TxtPaymentRefNo");

                Label HFEmail           = (Label)gvrow.FindControl("HFEmail");
                Label HDVendorName      = (Label)gvrow.FindControl("HDVendorName");
                Label LblvendorAcNo     = (Label)gvrow.FindControl("LblvendorAcNo");
                Label LblVendorIFSCCode = (Label)gvrow.FindControl("LblVendorIFSCCode");
                Label lblVendorCode     = (Label)gvrow.FindControl("lblVendorCode");

                Label LblAmounttobepaid = (Label)gvrow.FindControl("LblAmounttobepaid");
                Label lblPaymentCreatorEmailaddress = (Label)gvrow.FindControl("lblPaymentCreatorEmailaddress");
                Label lblAmountINWord = (Label)gvrow.FindControl("lblAmountINWord");


                if (Convert.ToString(HFEmail.Text).Trim() != "" && Convert.ToString(TxtPayRefNo.Text).Trim()!="") 
                {

                    if (TxtPayRefNo.Enabled == true)
                    {
                        DataSet dsList = new DataSet();
                        SqlParameter[] spars = new SqlParameter[7];
                        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                        spars[0].Value = "update_batch_PaymentRef_nos";

                        spars[1] = new SqlParameter("@Batch_ID", SqlDbType.BigInt);
                        spars[1].Value = Convert.ToDouble(hdnPOWOID.Value);

                        spars[2] = new SqlParameter("@PaymentId", SqlDbType.BigInt);
                        spars[2].Value = Convert.ToDouble(strPayment_ID);

                        spars[3] = new SqlParameter("@costcenter", SqlDbType.VarChar);
                        spars[3].Value = Convert.ToString(TxtPayRefNo.Text).Trim();

                        spars[4] = new SqlParameter("@Status_Id", SqlDbType.Int);
                        spars[4].Value = Convert.ToInt32(strPaymentType_Id);

                        spars[5] = new SqlParameter("@InvoiceId", SqlDbType.BigInt);
                        if (Convert.ToString(strInvoice_ID).Trim() != "")
                            spars[5].Value = Convert.ToDouble(strInvoice_ID);
                        else
                            spars[5].Value = DBNull.Value;

                        spars[6] = new SqlParameter("@Batch_No", SqlDbType.VarChar);
                        spars[6].Value = Convert.ToString(HFEmail.Text).Trim();

                        dsList = spm.getDatasetList(spars, "SP_VSCB_CreateBatchRequest");

                        string svendoremail = Convert.ToString(HFEmail.Text).Trim();

                         #region send Email to  Batch Approval
                            /*
                            string strsubject = "Payment Advice";
                            StringBuilder sbBatchDetails = new StringBuilder();
                            StringBuilder sbBatchVendorInvoiceDetail = new StringBuilder();
                            StringBuilder strbuildBodyMsg = new StringBuilder();
                            StringBuilder strbuildFooterMsg = new StringBuilder();

                            sbBatchDetails.Append("<table cellpadding='0' cellspacing='0' width='80%' style='font-size: 10pt;font-family:Trebuchet MS'>");
                            sbBatchDetails.Append("<tr><td style='height:30px;text-align:center' colspan='3'><span style='font-size: 14pt;font-family:Trebuchet MS'> Payment Advice </span></td></tr>");
                            if (strPaymentType_Id == "1")
                            {
                                sbBatchDetails.Append("<tr><td style='height:20px;width:140px'>Vendor Code :</td>");
                            }
                            else
                            {
                                sbBatchDetails.Append("<tr><td style='height:20px;width:140px'>Employee Code :</td>");
                            }
                            sbBatchDetails.Append("<td style='height:20px;width:300px'> " + lblVendorCode.Text.Trim() + "</td>");
                            sbBatchDetails.Append("<td style='height:20px;width:140px'>Amount :</td>");
                            sbBatchDetails.Append("<td style='height:20px;width:300px'>" + LblAmounttobepaid.Text.Trim() + "</td></tr>");
                            if (strPaymentType_Id == "1")
                            {
                                sbBatchDetails.Append("<tr><td style='height:20px;width:140px'>Vendor Name :</td>");
                            }
                            else
                            {
                                sbBatchDetails.Append("<tr><td style='height:20px;width:140px'>Employee Name :</td>");
                            }
                            sbBatchDetails.Append("<td style='height:20px;width:300px'> " + HDVendorName.Text.Trim() + "</td>");
                            sbBatchDetails.Append("<td style='height:20px;width:140px'>Transaction Ref. No :</td>");
                            sbBatchDetails.Append("<td style='height:20px;width:300px'> " + txtBankRefNo.Text.Trim() + "</td></tr>");
                            if (strPaymentType_Id == "1")
                            {
                                sbBatchDetails.Append("<tr><td style='height:20px;width:140px'>Vendor A/c No :</td>");
                            }
                            else
                            {
                                sbBatchDetails.Append("<tr><td style='height:20px;width:140px'>Employee A/c No :</td>");
                            }
                            sbBatchDetails.Append("<td style='height:20px;width:300px'>  " + LblvendorAcNo.Text.Trim() + "</td>");
                            sbBatchDetails.Append("<td style='height:20px;width:180px'></td>");
                            sbBatchDetails.Append("<td style='height:20px;width:300px'> </td></tr>");
                            if (strPaymentType_Id == "1")
                            {
                                sbBatchDetails.Append("<tr><td style='height:20px;width:140px'>Vendor IFSC Code :</td>");
                            }
                            else
                            {
                                sbBatchDetails.Append("<tr><td style='height:20px;width:140px'>Employee IFSC Code :</td>");
                            }
                            sbBatchDetails.Append("<td style='height:20px;width:300px'> " + LblVendorIFSCCode.Text.Trim() + "</td>");
                            sbBatchDetails.Append("<td style='height:20px;width:180px'></td>");
                            sbBatchDetails.Append("<td style='height:20px;width:300px'> </td></tr>");

                            sbBatchDetails.Append("<tr><td style='height:20px;width:140px'>UTR Number :</td>");
                            sbBatchDetails.Append("<td style='height:20px;width:300px'> " + TxtPayRefNo.Text.Trim() + "</td>");
                            sbBatchDetails.Append("<td style='height:20px;width:140px'></td>");
                            sbBatchDetails.Append("<td style='height:20px;width:300px'> </td></tr>");
                            sbBatchDetails.Append("<tr><td style='height:20px;width:140px'>Amount in Words :</td>");
                            sbBatchDetails.Append("<td style='height:20px;width:300px' colspan='3'> " + lblAmountINWord.Text.Trim() + " </td></tr>");
                            sbBatchDetails.Append("</table>");
                            strbuildBodyMsg.Append("<table cellpadding='5' cellspacing='0' style='font-size: 10pt;font-family:Trebuchet MS;margin-top:30px'>");
                            strbuildBodyMsg.Append("<tr><td><div style='margin-bottom:30px'>Dear Sir/Madam,<br/><br/>");
                            strbuildBodyMsg.Append("We have initiated your payment through NEFT/RTGS/Transfer on " + HDPaymentRelDate.Value + " for an amount of INR <b> " + LblAmounttobepaid.Text.Trim() + " (" + lblAmountINWord.Text.Trim() + ") </b>, Transaction details of this payment is as below. In case of further clarifications related to this transaction, kindly contact the concerned officials at HIGHBAR TECHNOCRAT LIMITED.");
                            strbuildBodyMsg.Append("</div></td></tr>");
                            strbuildBodyMsg.Append("</table>");
                            if (strPaymentType_Id == "1")
                            {
                                sbBatchVendorInvoiceDetail.Append("<table width='100%' style='color:#000000;font-size:10pt;font-family:Trebuchet MS !important;font-style:Regular;width:100%;border-collapse: collapse !important;border: 1px solid black;'>");
                                sbBatchVendorInvoiceDetail.Append("<tr style='text-align:left !important;height:50px;border: 1px solid black;'><th style='border: 1px solid black;text-align:left'>Invoice No</th><th style='border: 1px solid black;text-align:left'>Invoice Date (Supplier)</th><th style='border: 1px solid black;text-align:right'>Invoice Amount (Without GST)</th><th style='border: 1px solid black;text-align:right'>GST Amount</th><th style='border: 1px solid black;text-align:right'>Invoice Amount (With GST)</th><th style='border: 1px solid black;text-align:right'>TDS/TCS Deduction Amount</th> <th style='border: 1px solid black;text-align:right'>Amount Paid</th></tr>");
                                for (int irow = 0; irow < dsList.Tables[0].Rows.Count; irow++)
                                {
                                    sbBatchVendorInvoiceDetail.Append("<tr style='text-align:left !important;border: 1px solid black;'>");
                                    sbBatchVendorInvoiceDetail.Append("<td width:'15%' style='border: 1px solid black;text-align:left'>" + Convert.ToString(dsList.Tables[0].Rows[irow]["InvoiceNo"]).Trim() + "</td>");
                                    sbBatchVendorInvoiceDetail.Append("<td width:'15%' style='border: 1px solid black;text-align:left'>" + Convert.ToString(dsList.Tables[0].Rows[irow]["InvoiceDate"]).Trim() + "</td>");
                                    sbBatchVendorInvoiceDetail.Append("<td width:'25%' style='border: 1px solid black;text-align:right'>" + Convert.ToString(dsList.Tables[0].Rows[irow]["InvoiceAmountWithoutGST"]).Trim() + "</td>");
                                    sbBatchVendorInvoiceDetail.Append("<td width:'15%' style='border: 1px solid black;text-align:right'>" + Convert.ToString(dsList.Tables[0].Rows[irow]["GSTAmount"]).Trim() + "</td>");
                                    sbBatchVendorInvoiceDetail.Append("<td width:'15%' style='border: 1px solid black;text-align:right'>" + Convert.ToString(dsList.Tables[0].Rows[irow]["InvoiceAmountWithGST"]).Trim() + "</td>");
                                    sbBatchVendorInvoiceDetail.Append("<td width:'15%' style='border: 1px solid black;text-align:right'>" + Convert.ToString(dsList.Tables[0].Rows[irow]["TDSTCSDeductionAmount"]).Trim() + "</td>");
                                    sbBatchVendorInvoiceDetail.Append("<td width:'15%' style='border: 1px solid black;text-align:right'>" + Convert.ToString(dsList.Tables[0].Rows[irow]["AmountPaid"]).Trim() + "</td>");
                                    sbBatchVendorInvoiceDetail.Append("</tr>");
                                }
                                sbBatchVendorInvoiceDetail.Append("</table>");
                            }
                            else
                            {
                                sbBatchVendorInvoiceDetail.Append("<table width='100%' style='color:#000000;font-size:10pt;font-family:Trebuchet MS !important;font-style:Regular;width:100%;border-collapse: collapse !important;border: 1px solid black;'>");
                                sbBatchVendorInvoiceDetail.Append("<tr style='text-align:left !important;height:50px;border: 1px solid black;'><th style='border: 1px solid black;text-align:left'>Voucher Type</th><th style='border: 1px solid black;text-align:left'>Voucher No</th><th style='border: 1px solid black;text-align:left'>Voucher Date</th><th style='border: 1px solid black;text-align:left'>Cost Center</th><th style='border: 1px solid black;text-align:right'>Voucher Amount</th></tr>");
                                for (int irow = 0; irow < dsList.Tables[0].Rows.Count; irow++)
                                {
                                    sbBatchVendorInvoiceDetail.Append("<tr style='text-align:left !important;border: 1px solid black;'>");
                                    sbBatchVendorInvoiceDetail.Append("<td width:'15%' style='border: 1px solid black;text-align:left'>" + Convert.ToString(dsList.Tables[0].Rows[irow]["VoucherType"]).Trim() + "</td>");
                                    sbBatchVendorInvoiceDetail.Append("<td width:'15%' style='border: 1px solid black;text-align:left'>" + Convert.ToString(dsList.Tables[0].Rows[irow]["VoucherNo"]).Trim() + "</td>");
                                    sbBatchVendorInvoiceDetail.Append("<td width:'25%' style='border: 1px solid black;text-align:left'>" + Convert.ToString(dsList.Tables[0].Rows[irow]["VoucherDate"]).Trim() + "</td>");
                                    sbBatchVendorInvoiceDetail.Append("<td width:'15%' style='border: 1px solid black;text-align:left'>" + Convert.ToString(dsList.Tables[0].Rows[irow]["CostCenter"]).Trim() + "</td>");
                                    sbBatchVendorInvoiceDetail.Append("<td width:'15%' style='border: 1px solid black;text-align:right'>" + Convert.ToString(dsList.Tables[0].Rows[irow]["VoucherAmount"]).Trim() + "</td>");
                                    sbBatchVendorInvoiceDetail.Append("</tr>");
                                }
                                sbBatchVendorInvoiceDetail.Append("</table>");
                            }
                            strbuildFooterMsg.Append("<table cellspacing='0' style='font-size: 10pt;font-family:Trebuchet MS;margin-top:30px'>");
                            strbuildFooterMsg.Append("<tr><td><div>Regards,<br/>");
                            strbuildFooterMsg.Append("<span style='color:purple;'> HIGHBAR TECHNOCRAT LTD </span><br/>");
                            strbuildFooterMsg.Append("D-Wing, 14th Floor, Empire Tower, Reliable Cloud City,<br/>");
                            strbuildFooterMsg.Append("Off. Thane-Belapur Road, Airoli, Navi Mumbai – 400 708.<br/>");
                            string Myurl = "https://www.highbartech.com/";
                            strbuildFooterMsg.Append("Website : <a href=" + Myurl + ">www.highbartech.com</a><br/>");
                            strbuildFooterMsg.Append("</div></td></tr>");
                            strbuildFooterMsg.Append("</table>");



                            if (dsList.Tables[1].Rows.Count > 0)
                            {
                                if (Convert.ToString(dsList.Tables[1].Rows[0]["cc_email"]).Trim() != "")
                                {
                                    lblPaymentCreatorEmailaddress.Text = lblPaymentCreatorEmailaddress.Text.Trim() + ";" + Convert.ToString(dsList.Tables[1].Rows[0]["cc_email"]).Trim();
                                }

                            }

                            string strmailcontain = Convert.ToString(sbBatchDetails).Trim() + Convert.ToString(strbuildBodyMsg).Trim() + Convert.ToString(sbBatchVendorInvoiceDetail).Trim() + Convert.ToString(strbuildFooterMsg).Trim();
                            spm.sendMail_VSCB(HFEmail.Text.Trim(), strsubject, strmailcontain, "", lblPaymentCreatorEmailaddress.Text.Trim());

                            sVendorEmailid = sVendorEmailid + ";" + Convert.ToString(HFEmail.Text).Trim();
                            */
                            #endregion
                         
                    }
                     
                }
            }


            #region get Vendor Emails Id and send Payment Advice to vendor

            DataSet dsList_VEmails = new DataSet();
            SqlParameter[] spar_E = new SqlParameter[2];
            spar_E[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spar_E[0].Value = "get_payment_Batch_VendorEmails";

            spar_E[1] = new SqlParameter("@BatchID", SqlDbType.BigInt);
            spar_E[1].Value = Convert.ToDouble(hdnPOWOID.Value);

            dsList_VEmails = spm.getDatasetList(spar_E, "SP_VSCB_GETALL_DETAILS");
            if(dsList_VEmails !=null)
            {
                string sccmails = "";
                string sVendorEmail = "";
                string sVendorName = "";
                if (dsList_VEmails.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsList_VEmails.Tables[0].Rows.Count; i++)
                    {
                        string strPaymentType_Id = Convert.ToString(dsList_VEmails.Tables[0].Rows[i]["paymentType_id"]).Trim();
                        sVendorEmail= Convert.ToString(dsList_VEmails.Tables[0].Rows[i]["VendorEmailAddress"]).Trim();
                        if (Convert.ToString(dsList_VEmails.Tables[0].Rows[i]["VendorName"]).Trim() != "")
                            sVendorName = Convert.ToString(dsList_VEmails.Tables[0].Rows[i]["VendorName"]).Trim();

                        #region send Email to  Batch Approval

                        string strsubject = "Payment Advice";
                        
                        StringBuilder sbBatchDetails = new StringBuilder();
                        StringBuilder sbBatchVendorInvoiceDetail = new StringBuilder();
                        StringBuilder strbuildBodyMsg = new StringBuilder();
                        StringBuilder strbuildFooterMsg = new StringBuilder();

                        sbBatchDetails.Append("<table cellpadding='0' cellspacing='0' width='80%' style='font-size: 10pt;font-family:Trebuchet MS'>");
                        sbBatchDetails.Append("<tr><td style='height:30px;text-align:center' colspan='3'><span style='font-size: 14pt;font-family:Trebuchet MS'> Payment Advice </span></td></tr>");
                        if (strPaymentType_Id == "1" || strPaymentType_Id=="6" || strPaymentType_Id=="7")
                        {
                            sbBatchDetails.Append("<tr><td style='height:20px;width:140px'>Vendor Code :</td>");
                        }
                        else
                        {
                            sbBatchDetails.Append("<tr><td style='height:20px;width:140px'>Employee Code :</td>");
                        }
                        sbBatchDetails.Append("<td style='height:20px;width:300px'> " + Convert.ToString(dsList_VEmails.Tables[0].Rows[i]["VendorCode"]).Trim() + "</td>");
                        sbBatchDetails.Append("<td style='height:20px;width:140px'>Amount :</td>");
                        sbBatchDetails.Append("<td style='height:20px;width:300px'>" + Convert.ToString(dsList_VEmails.Tables[0].Rows[i]["Amounttobepaid"]).Trim() + "</td></tr>");
                        if (strPaymentType_Id == "1" || strPaymentType_Id == "6" || strPaymentType_Id == "7")
                        {
                            sbBatchDetails.Append("<tr><td style='height:20px;width:140px'>Vendor Name :</td>");
                        }
                        else
                        {
                            sbBatchDetails.Append("<tr><td style='height:20px;width:140px'>Employee Name :</td>");
                        }
                        sbBatchDetails.Append("<td style='height:20px;width:300px'> " + Convert.ToString(dsList_VEmails.Tables[0].Rows[i]["VendorName"]).Trim()  + "</td>");
                        sbBatchDetails.Append("<td style='height:20px;width:140px'>Transaction Ref. No :</td>");
                        sbBatchDetails.Append("<td style='height:20px;width:300px'> " + Convert.ToString(dsList_VEmails.Tables[0].Rows[i]["BankRef_no"]).Trim() + "</td></tr>");
                        if (strPaymentType_Id == "1" || strPaymentType_Id == "6" || strPaymentType_Id == "7")
                        {
                            sbBatchDetails.Append("<tr><td style='height:20px;width:140px'>Vendor A/c No :</td>");
                        }
                        else
                        {
                            sbBatchDetails.Append("<tr><td style='height:20px;width:140px'>Employee A/c No :</td>");
                        }
                        sbBatchDetails.Append("<td style='height:20px;width:300px'>  " + Convert.ToString(dsList_VEmails.Tables[0].Rows[i]["Acc_no"]).Trim() + "</td>");
                        sbBatchDetails.Append("<td style='height:20px;width:180px'></td>");
                        sbBatchDetails.Append("<td style='height:20px;width:300px'> </td></tr>");
                        if (strPaymentType_Id == "1" || strPaymentType_Id == "6" || strPaymentType_Id == "7")
                        {
                            sbBatchDetails.Append("<tr><td style='height:20px;width:140px'>Vendor IFSC Code :</td>");
                        }
                        else
                        {
                            sbBatchDetails.Append("<tr><td style='height:20px;width:140px'>Employee IFSC Code :</td>");
                        }
                        sbBatchDetails.Append("<td style='height:20px;width:300px'> " + Convert.ToString(dsList_VEmails.Tables[0].Rows[i]["IFSC_Code"]).Trim() + "</td>");
                        sbBatchDetails.Append("<td style='height:20px;width:180px'></td>");
                        sbBatchDetails.Append("<td style='height:20px;width:300px'> </td></tr>");

                        sbBatchDetails.Append("<tr><td style='height:20px;width:140px'>UTR Number :</td>");
                        sbBatchDetails.Append("<td style='height:20px;width:300px'> " + Convert.ToString(dsList_VEmails.Tables[0].Rows[i]["PaymentRefNo"]).Trim() + "</td>");
                        sbBatchDetails.Append("<td style='height:20px;width:140px'></td>");
                        sbBatchDetails.Append("<td style='height:20px;width:300px'> </td></tr>");
                        sbBatchDetails.Append("<tr><td style='height:20px;width:140px'>Amount in Words :</td>");
                        sbBatchDetails.Append("<td style='height:20px;width:300px' colspan='3'> " + Convert.ToString(dsList_VEmails.Tables[0].Rows[i]["AmountINWord"]).Trim() + " </td></tr>");
                        sbBatchDetails.Append("</table>");
                        strbuildBodyMsg.Append("<table cellpadding='5' cellspacing='0' style='font-size: 10pt;font-family:Trebuchet MS;margin-top:30px'>");
                        strbuildBodyMsg.Append("<tr><td><div style='margin-bottom:30px'>Dear Sir/Madam,<br/><br/>");
                        strbuildBodyMsg.Append("We have initiated your payment through NEFT/RTGS/Transfer on " + Convert.ToString(dsList_VEmails.Tables[0].Rows[i]["paymentReleaseDate"]).Trim() + " for an amount of INR <b> " + Convert.ToString(dsList_VEmails.Tables[0].Rows[i]["Amounttobepaid"]).Trim() + " (" + Convert.ToString(dsList_VEmails.Tables[0].Rows[i]["AmountINWord"]).Trim() + ") </b>, Transaction details of this payment is as below. In case of further clarifications related to this transaction, kindly contact the concerned officials at HIGHBAR TECHNOCRAT LIMITED.");
                        strbuildBodyMsg.Append("</div></td></tr>");
                        strbuildBodyMsg.Append("</table>");

                        DataSet dsInvoiceList = new DataSet();
                        SqlParameter[] spar_I = new SqlParameter[3];
                        spar_I[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                        if (Convert.ToString(dsList_VEmails.Tables[0].Rows[i]["paymentType_id"]).Trim() == "1")
                            spar_I[0].Value = "get_Invoice_dtls_toSend_PaymentAdvice_toVendor";
                        else
                            spar_I[0].Value = "get_claims_details_to_send_paymentAdvice_toEmployee";

                        spar_I[1] = new SqlParameter("@costcenter", SqlDbType.VarChar);
                        spar_I[1].Value = sVendorEmail;

                        spar_I[2] = new SqlParameter("@BatchID", SqlDbType.BigInt);
                        spar_I[2].Value = Convert.ToDouble(hdnPOWOID.Value);

                        dsInvoiceList = spm.getDatasetList(spar_I, "SP_VSCB_GETALL_DETAILS");

                        if (strPaymentType_Id == "1")
                        {

                            sbBatchVendorInvoiceDetail.Append("<table width='100%' style='padding:1px 1px 1px 1px;color:#000000;font-size:10pt;font-family:Trebuchet MS !important;font-style:Regular;width:100%;border-collapse: collapse !important;border: 1px solid black;'>");
                            sbBatchVendorInvoiceDetail.Append("<tr style='text-align:left !important;height:50px;border: 1px solid black;'><th style='border: 1px solid black;text-align:left'>Cost Center</th><th style='border: 1px solid black;text-align:left'> Invoice No</th><th style='border: 1px solid black;text-align:left'> Invoice Date (Supplier)</th><th style='border: 1px solid black;text-align:right'>Invoice Amount (Without GST) </th><th style='border: 1px solid black;text-align:right'>GST Amount</th><th style='border: 1px solid black;text-align:right'>Invoice Amount (With GST)</th><th style='border: 1px solid black;text-align:right'>TDS/TCS Deduction Amount</th> <th style='border: 1px solid black;text-align:right'>Amount Paid</th></tr>");
                           if(dsInvoiceList !=null)
                            {
                                if(dsInvoiceList.Tables[0].Rows.Count>0)
                                {
                                    for (int irow = 0; irow < dsInvoiceList.Tables[0].Rows.Count; irow++)
                                    {
                                        sbBatchVendorInvoiceDetail.Append("<tr style='text-align:left !important;border: 1px solid black;'>");
                                        sbBatchVendorInvoiceDetail.Append("<td width:'15%' style='border: 1px solid black;text-align:left'> " + Convert.ToString(dsInvoiceList.Tables[0].Rows[irow]["CostCentre"]).Trim() + " </td>");
                                        sbBatchVendorInvoiceDetail.Append("<td width:'13%' style='border: 1px solid black;text-align:left'> " + Convert.ToString(dsInvoiceList.Tables[0].Rows[irow]["InvoiceNo"]).Trim() + " </td>");
                                        sbBatchVendorInvoiceDetail.Append("<td width:'15%' style='border: 1px solid black;text-align:left'> " + Convert.ToString(dsInvoiceList.Tables[0].Rows[irow]["InvoiceDate"]).Trim() + " </td>");
                                        sbBatchVendorInvoiceDetail.Append("<td width:'20%' style='border: 1px solid black;text-align:right'> " + Convert.ToString(dsInvoiceList.Tables[0].Rows[irow]["InvoiceAmountWithoutGST"]).Trim() + " </td>");
                                        sbBatchVendorInvoiceDetail.Append("<td width:'13%' style='border: 1px solid black;text-align:right'> " + Convert.ToString(dsInvoiceList.Tables[0].Rows[irow]["GSTAmount"]).Trim() + " </td>");
                                        sbBatchVendorInvoiceDetail.Append("<td width:'13%' style='border: 1px solid black;text-align:right'> " + Convert.ToString(dsInvoiceList.Tables[0].Rows[irow]["InvoiceAmountWithGST"]).Trim() + " </td>");
                                        sbBatchVendorInvoiceDetail.Append("<td width:'13%' style='border: 1px solid black;text-align:right'> " + Convert.ToString(dsInvoiceList.Tables[0].Rows[irow]["TDSTCSDeductionAmount"]).Trim() + " </td>");
                                        sbBatchVendorInvoiceDetail.Append("<td width:'13%' style='border: 1px solid black;text-align:right'> " + Convert.ToString(dsInvoiceList.Tables[0].Rows[irow]["AmountPaid"]).Trim() + "</td>");
                                        sbBatchVendorInvoiceDetail.Append("</tr>");
                                    }
                                }
                            }
                            sbBatchVendorInvoiceDetail.Append("</table>");
                        }
                        else if  ( strPaymentType_Id == "6" || strPaymentType_Id == "7")
                        {
                            //for Advance Payment and Security Deposit
                        }
                        else
                        { 
                            //Employee reimbursment payments
                            sbBatchVendorInvoiceDetail.Append("<table width='100%' style='color:#000000;font-size:10pt;font-family:Trebuchet MS !important;font-style:Regular;width:100%;border-collapse: collapse !important;border: 1px solid black;'>");
                            sbBatchVendorInvoiceDetail.Append("<tr style='text-align:left !important;height:50px;border: 1px solid black;'><th style='border: 1px solid black;text-align:left'>Voucher Type</th><th style='border: 1px solid black;text-align:left'>Voucher No</th><th style='border: 1px solid black;text-align:left'>Voucher Date</th><th style='border: 1px solid black;text-align:left'>Cost Center</th><th style='border: 1px solid black;text-align:right'>Voucher Amount</th></tr>");
                            if (dsInvoiceList != null)
                            {
                                if (dsInvoiceList.Tables[0].Rows.Count > 0)
                                {
                                    for (int irow = 0; irow < dsInvoiceList.Tables[0].Rows.Count; irow++)
                                    {
                                        sbBatchVendorInvoiceDetail.Append("<tr style='text-align:left !important;border: 1px solid black;'>");
                                        sbBatchVendorInvoiceDetail.Append("<td width:'15%' style='border: 1px solid black;text-align:left'>" + Convert.ToString(dsInvoiceList.Tables[0].Rows[irow]["VoucherType"]).Trim() + "</td>");
                                        sbBatchVendorInvoiceDetail.Append("<td width:'15%' style='border: 1px solid black;text-align:left'>" + Convert.ToString(dsInvoiceList.Tables[0].Rows[irow]["VoucherNo"]).Trim() + "</td>");
                                        sbBatchVendorInvoiceDetail.Append("<td width:'25%' style='border: 1px solid black;text-align:left'>" + Convert.ToString(dsInvoiceList.Tables[0].Rows[irow]["VoucherDate"]).Trim() + "</td>");
                                        sbBatchVendorInvoiceDetail.Append("<td width:'15%' style='border: 1px solid black;text-align:left'>" + Convert.ToString(dsInvoiceList.Tables[0].Rows[irow]["CostCenter"]).Trim() + "</td>");
                                        sbBatchVendorInvoiceDetail.Append("<td width:'15%' style='border: 1px solid black;text-align:right'>" + Convert.ToString(dsInvoiceList.Tables[0].Rows[irow]["VoucherAmount"]).Trim() + "</td>");
                                        sbBatchVendorInvoiceDetail.Append("</tr>");
                                    }
                                }
                            }
                            sbBatchVendorInvoiceDetail.Append("</table>");
                        }
                        strbuildFooterMsg.Append("<table cellspacing='0' style='font-size: 10pt;font-family:Trebuchet MS;margin-top:30px'>");
                        strbuildFooterMsg.Append("<tr><td><div>Regards,<br/>");
                        strbuildFooterMsg.Append("<span style='color:purple;'> HIGHBAR TECHNOCRAT LTD </span><br/>");
                        strbuildFooterMsg.Append("D-Wing, 14th Floor, Empire Tower, Reliable Cloud City,<br/>");
                        strbuildFooterMsg.Append("Off. Thane-Belapur Road, Airoli, Navi Mumbai – 400 708.<br/>");
                        string Myurl = "https://www.highbartech.com/";
                        strbuildFooterMsg.Append("Website : <a href=" + Myurl + ">www.highbartech.com</a><br/>");
                        strbuildFooterMsg.Append("</div></td></tr>");
                        strbuildFooterMsg.Append("</table>");


                        sccmails = "";
                        if (dsInvoiceList.Tables[1].Rows.Count > 0)
                        {
                            for (int irow = 0; irow < dsInvoiceList.Tables[1].Rows.Count; irow++)
                            {
                                if (Convert.ToString(dsInvoiceList.Tables[1].Rows[irow]["cc_email"]).Trim() != "")
                                {
                                    if (Convert.ToString(dsInvoiceList.Tables[1].Rows[irow]["cc_email"]).Trim() != "")
                                    {
                                        if (Convert.ToString(sccmails).Trim() == "")
                                            sccmails = Convert.ToString(dsInvoiceList.Tables[1].Rows[irow]["cc_email"]).Trim();
                                        else
                                            sccmails = sccmails + ";" + Convert.ToString(dsInvoiceList.Tables[1].Rows[irow]["cc_email"]).Trim();

                                    }
                                }
                            }

                        }

                        string strmailcontain = Convert.ToString(sbBatchDetails).Trim() + Convert.ToString(strbuildBodyMsg).Trim() + Convert.ToString(sbBatchVendorInvoiceDetail).Trim() + Convert.ToString(strbuildFooterMsg).Trim();

                        if (Convert.ToString(sVendorName).Trim() != "")
                            strsubject = strsubject + " :- " + sVendorName;

			 
                        spm.sendMail_VSCB(sVendorEmail, strsubject, strmailcontain, "",  sccmails.Trim(),"");

                        #endregion

                        #region Update the send mail status

                            DataSet dsBatchList_SendMail = new DataSet();
                            SqlParameter[] spar_M = new SqlParameter[3];
                            spar_M[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                            spar_M[0].Value = "update_batch_PaymentRef_nos_sendmail";

                            spar_M[1] = new SqlParameter("@costcenter", SqlDbType.VarChar);
                            spar_M[1].Value = sVendorEmail;

                            spar_M[2] = new SqlParameter("@BatchID", SqlDbType.BigInt);
                            spar_M[2].Value = Convert.ToDouble(hdnPOWOID.Value);

                            dsBatchList_SendMail = spm.getDatasetList(spar_M, "SP_VSCB_GETALL_DETAILS");

                        #endregion

                    }
                }
            }

            #endregion

            Response.Redirect("VSCB_AssignBankRefApprovedbatch.aspx");
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            //throw;
        }
    }

    protected void trvl_btnSave_Click(object sender, EventArgs e)
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_myBatch_DetailsImportExcel";

        spars[1] = new SqlParameter("@POWOID", SqlDbType.BigInt);
        spars[1].Value = Convert.ToDouble(hdnPOWOID.Value);

        dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        if(dsList.Tables[0].Rows.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dsList.Tables[0], "AssignPaymentRef");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=AssignPaymentRef.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }


    protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
    {
        ReadXCLFileOpenXl();
    }

    public void ReadExcelfile()
    {
        if (Uploadfile.HasFile)
        {
            var supportedTypes = new[] { ".xls", ".xlsx" };
            string extension = System.IO.Path.GetExtension(Uploadfile.FileName);
            if (!supportedTypes.Contains(extension))
            {
                lblmessage.Text = "File Extension Is InValid - Only Upload Excel file";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ASD", "StopLoader();", true);
                return;
            }

            string filename = Uploadfile.FileName;
            string POWOFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBUploadAssignPaymentRef"]).Trim() + "/");
            bool folderExists = Directory.Exists(POWOFilePath);
            if (!folderExists)
            {
                Directory.CreateDirectory(POWOFilePath);
            }
            String InputFile = System.IO.Path.GetExtension(Uploadfile.FileName);

            string str = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
            filename = "AssignPaymentRef_" + str + InputFile;
            Uploadfile.SaveAs(Path.Combine(POWOFilePath, filename));
            string powoUplaodedFile = POWOFilePath + filename;
            string read = System.IO.Path.GetFullPath(powoUplaodedFile);
            if (Path.GetExtension(read) == ".xls")
            {
                oConnstr = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + read + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");
            }
            else if (Path.GetExtension(read) == ".xlsx")
            {
                oConnstr = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + read + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
            }

            OleDbCommand excommand = new OleDbCommand();
            OleDbDataAdapter oadp = new OleDbDataAdapter();
            DataSet dsPOWO = new DataSet();
            excommand.Connection = oConnstr;
            excommand.CommandType = CommandType.Text;
            excommand.CommandText = "SELECT* FROM [AssignPaymentRef$]";
            oadp = new OleDbDataAdapter(excommand);
            oadp.Fill(dsPOWO, "VSCB");


            if (dsPOWO.Tables.Count > 0)
            {
                if (dsPOWO.Tables[0].Columns.Count == 6)
                {
                    foreach (DataColumn col in dsPOWO.Tables[0].Columns)
                    {
                        if (col.ColumnName == "Vendor Account No")
                        {
                            col.ColumnName = "VendorAccountNo";
                        }
                        if (col.ColumnName == "Invoice No")
                        {
                            col.ColumnName = "InvoiceNo";
                        }
                    }
                    dsPOWO.AcceptChanges();
                    foreach (GridViewRow row in gvMngPaymentList_Batch.Rows)
                    {
                        TextBox TxtPayRefNo = (TextBox)row.FindControl("TxtPaymentRefNo");
                        Label lblInvoiceno = (Label)row.FindControl("lblInvoiceno");
                        Label LblAccountno = (Label)row.FindControl("LblAccountno");
                        string strPaymentType_Id = Convert.ToString(gvMngPaymentList_Batch.DataKeys[row.RowIndex].Values[4]).Trim();

                        if (TxtPayRefNo.Enabled == false)
                        {

                        }
                        else
                        {
                            if (strPaymentType_Id.ToString().Trim() == "1")
                            {
                                DataRow[] dr = dsPOWO.Tables[0].Select("VendorAccountNo= '" + LblAccountno.Text.ToString() + "' AND InvoiceNo = '" + lblInvoiceno.Text + "' ");
                                if (dr.Length == 1)
                                {
                                    TxtPayRefNo.Text = dr[0].ItemArray[5].ToString();
                                }
                            }
                            else
                            {
                                DataRow[] dr = dsPOWO.Tables[0].Select("InvoiceNo = '" + lblInvoiceno.Text + "' ");
                                if (dr.Length == 1)
                                {
                                    TxtPayRefNo.Text = dr[0].ItemArray[5].ToString();
                                }
                            }

                        }
                    }
                }
                else
                {
                    lblmessage.Text = "Column Not matching as per download Tamplate";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ASD", "StopLoader();", true);
                    return;
                }
            }
        }
    }

    public DataTable ReadXCLFileOpenXl()
    {
        DataTable dtCsv = new DataTable();
        //Create a new DataTable.
        DataTable dt = new DataTable();
        string Fulltext;
        if (Uploadfile.HasFile)
        {
            //string FileSaveWithPath = Server.MapPath("\\Files\\Import" + System.DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".csv");
            //Uploadfile.SaveAs(FileSaveWithPath);

            string filename = Uploadfile.FileName;
            string POWOFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBUploadAssignPaymentRef"]).Trim() + "/");
            bool folderExists = Directory.Exists(POWOFilePath);
            if (!folderExists)
            {
                Directory.CreateDirectory(POWOFilePath);
            }
            String InputFile = System.IO.Path.GetExtension(Uploadfile.FileName);

            string str = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
            filename = "AssignPaymentRef_" + str + InputFile;
            Uploadfile.SaveAs(Path.Combine(POWOFilePath, filename));
            string powoUplaodedFile = POWOFilePath + filename;
            string read = System.IO.Path.GetFullPath(powoUplaodedFile);

            //Open the Excel file using ClosedXML.
            using (XLWorkbook workBook = new XLWorkbook(read))
            {
                //Read the first Sheet from Excel file.
                IXLWorksheet workSheet = workBook.Worksheet(1);
                //Loop through the Worksheet rows.
                bool firstRow = true;
                foreach (IXLRow row in workSheet.Rows())
                {
                    //Use the first row to add columns to DataTable.
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Columns.Add(cell.Value.ToString());
                        }
                        firstRow = false;
                    }
                    else
                    {
                        //Add rows to DataTable.
                        dt.Rows.Add();
                        int i = 0;
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                if (dt.Columns.Count == 6)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.ColumnName == "Vendor Account No")
                        {
                            col.ColumnName = "VendorAccountNo";
                        }
                        if (col.ColumnName == "Invoice No")
                        {
                            col.ColumnName = "InvoiceNo";
                        }
                    }
                    dt.AcceptChanges();
                    foreach (GridViewRow row in gvMngPaymentList_Batch.Rows)
                    {
                        TextBox TxtPayRefNo = (TextBox)row.FindControl("TxtPaymentRefNo");
                        Label lblInvoiceno = (Label)row.FindControl("lblInvoiceno");
                        Label LblAccountno = (Label)row.FindControl("LblAccountno");
                        string strPaymentType_Id = Convert.ToString(gvMngPaymentList_Batch.DataKeys[row.RowIndex].Values[4]).Trim();

                        if (TxtPayRefNo.Enabled == false)
                        {

                        }
                        else
                        {
                            if (strPaymentType_Id.ToString().Trim() == "1")
                            {
                                DataRow[] dr = dt.Select("VendorAccountNo= '" + LblAccountno.Text.ToString() + "' AND InvoiceNo = '" + lblInvoiceno.Text + "' ");
                                if (dr.Length == 1)
                                {
                                    TxtPayRefNo.Text = dr[0].ItemArray[5].ToString();
                                }
                            }
                            else
                            {
                                DataRow[] dr = dt.Select("InvoiceNo = '" + lblInvoiceno.Text + "' ");
                                if (dr.Length == 1)
                                {
                                    TxtPayRefNo.Text = dr[0].ItemArray[5].ToString();
                                }
                            }

                        }
                    }
                }
            }
        }
        return dt;
    }

}