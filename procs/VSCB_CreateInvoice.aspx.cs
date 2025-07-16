using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSCB_CreateInvoice : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();

    decimal tMilestoneAmt_Invoice = 0, MilestoneBalanceAmt_Invoice = 0, MilestonePaidAmt = 0, MilestoneDirectTaxAmt = 0, dMilestoneAmt_ForInvoice = 0;

    #region Creative_Default_methods


    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    public DataTable dtEmp;
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
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

            lblmessage.Text = "";
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
            txtEmpCode.Text = Convert.ToString(Session["Empcode"]);

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/vscb_index.aspx");
            }
            else
            {
                Page.SmartNavigation = true;

                FilePath.Value = Convert.ToString(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBInvoicefiles"]).Trim() + "/"));

                hdnSingPOCopyFilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBSignPOWOfiles"]).Trim());

                lblheading.Text = "Create Invoice";
                if (!Page.IsPostBack)
                {
                    hdnMilestoneRowCnt.Value = "0";
                    if (Request.QueryString.Count > 0)
                    {
                        hdnInvoiceId.Value = Convert.ToString(Request.QueryString["invid"]).Trim();
                    }
                    else
                    {
                        hdnInvoiceId.Value = "0";
                    }

                    txtAmtWithOutTax.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");
                    txtCGST_Per.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");
                    txtSGST_Per.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");
                    txtIGST_Per.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");
                    txtAdv_Settlement_Amt.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");
                    txtSecurityDeposit.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");

                    PopulateEmployeeData();

                    GetPODetails_List();
                    GetPOTypes();
                    GetCostCenterList_ForChange();
                    get_Approver_List();
                    CheckIs_Invoice_Wthout_POWO();
                    get_ProjectDept_Vendor_List();

                    if (Request.QueryString.Count > 0)
                    {
                        //GetPODetails_List_Invoice_View();
                        //Check_Advance_Payment_POWO();//Advance Payment POWO

                        btnCancel.Visible = true;
                        get_MilestoneList_from_Invoice_Milestone_toTemp();
                        get_InvoiceDetails_MilestonesList_Update();

                        if (Convert.ToString(hdnInvoiceApprStatusId.Value).Trim() != "2")
                            get_Approver_List();

                        txtInvoiceNo.Enabled = false;
                        //
                        //txtinvoice_description.Enabled = false;
                        trvl_btnSave.Text = "Update Invoice";

                        livoucher_1.Visible = true;
                        livoucher_2.Visible = true;
                        livoucher_3.Visible = true;
                        //get_Approved_Reject_Approver_List();

                        if (Convert.ToString(hdnInvoiceApprStatusId.Value).Trim() == "3" || Convert.ToString(hdnInvoiceApprStatusId.Value).Trim() == "4" || Convert.ToString(hdnInvoiceApprStatusId.Value).Trim() == "2")
                        {
                            trvl_btnSave.Visible = false;
                        }


                    }

                    if (chkIsInvoiceWithoutPO.Checked == false)
                       
                    {
                        txtAmtWithOutTax.Enabled = false;
                        txtAmtWithOutTax.ReadOnly = true;
                    }
                    

                }

            }

        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    protected void lstTripType_SelectedIndexChanged(object sender, EventArgs e)
    {


        clear_POWO_Cntrls();
        clear_Milestone_Cntrls();
        get_PWODetails_MilestonesList();
        Check_CostCenterApprovalMatrix(Convert.ToString(lstforChangeCostCenter.SelectedValue).Trim());
        Check_Advance_Payment_POWO();//Advance Payment POWO
    }



    protected void trvldeatils_delete_btn_Click(object sender, EventArgs e)
    {

    }

    protected void trvldeatils_cancel_btn_Click(object sender, EventArgs e)
    {
    }




    protected void trvl_btnSave_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        if (checkVenddorEmailIdExists() == false)
        {

            lblmessage.Text = "Vednor Email-address not updated. So you can not create the Invoice for the selected cost center.";
            return;
        }
        DataTable DT = new DataTable();
        SqlParameter[] spars4 = new SqlParameter[2];
        spars4[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars4[0].Value = "CheckprocessInvoice";

        spars4[1] = new SqlParameter("@InvoiceID", SqlDbType.Int);
        spars4[1].Value = Convert.ToInt32(hdnInvoiceId.Value);


        DT = spm.getDropdownList(spars4, "SP_VSCB_CreateInvoice");

        if (DT.Rows.Count > 0)
        {
            lblmessage.Text = "The invoice is currently under approval processing.";
            return;
        }


      Create_Invoice_WithPOWO();
        //if (chkIsInvoiceWithoutPO.Checked == false)
        //{
        //    Create_Invoice_WithPOWO();
        //}
        //else
        //{
        //    Create_Invoice_Without_POWO();
        //}

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        if (chkIsInvoiceWithoutPO.Checked == false)
        {
            if (Convert.ToString(txtinvoice_description.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Invoice Description.";
                return;
            }
        }

        #region Check is any Payment request is pending or any partial payment is done under this invoice
        DataSet dsPaymentReqs = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Invoice_PaymentRequest_list_ForCancelledInvoice";

        spars[1] = new SqlParameter("@InvoiceId", SqlDbType.BigInt);
        spars[1].Value = Convert.ToDouble(hdnInvoiceId.Value);

        dsPaymentReqs = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        if (dsPaymentReqs.Tables[0].Rows.Count > 0)
        {
            lblmessage.Text = "Some Payment Request are found under this Invoice.Please cancelled the payment request first.";
            return;
        }

        #endregion


        string sPotypeid = "0";
        if (chkIsInvoiceWithoutPO.Checked == true)
        {
            sPotypeid = Convert.ToString(lstDisplayPOTypes.SelectedValue);
        }
        else
        {

            sPotypeid = Convert.ToString(lstPOType.SelectedValue);
        }

        //cancelled Invoice
        spm.Cancel_Invoicedetails(Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToDouble(hdnInvoiceId.Value), Convert.ToString(txtInvocieRemarks.Text), Convert.ToString(txtinvoice_description.Text));

        // string approverList = GetInvoiceApprove_RejectList(hdnInvoiceId.Value, txtEmpCode.Text);

        DataSet dsMilestoneList = new DataSet();
        dsMilestoneList = spm.get_Invoice_Milestone_List(Convert.ToDouble(hdnInvoiceId.Value));


        #region Send Email to 1st Approver
        string sApproverEmail_CC = "";
        DataSet dsMilestone = new DataSet();
        int ichkWithoutPo = 0;
        if (chkIsInvoiceWithoutPO.Checked)
            ichkWithoutPo = 1;
        dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), txtEmpCode.Text, hdnProject_Dept_Name.Value, Convert.ToString(sPotypeid), ichkWithoutPo);

        for (Int32 irow = 0; irow < dsMilestone.Tables[0].Rows.Count; irow++)
        {
            if (Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() != "")
            {
                if (Convert.ToString(sApproverEmail_CC).Trim() == "")
                    sApproverEmail_CC = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                else
                    sApproverEmail_CC = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() + ";" + sApproverEmail_CC;
            }
        }

        StringBuilder strbuild_Approvers = new StringBuilder();
        strbuild_Approvers.Append("<table cellpadding='5' cellspacing='0' style='font-size:11pt;font-family:Arial;font-style:Regular;width:100%;'>");
        strbuild_Approvers.Append("<tr><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Approver Name</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Status</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Approved On</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Approver Remarks</th></tr>");
        for (Int32 irow = 0; irow < dsMilestone.Tables[0].Rows.Count; irow++)
        {
            strbuild_Approvers.Append("<tr><td style='width:40%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["ApproverName"]).Trim() + " </td>");
            strbuild_Approvers.Append("<td style='width:10%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() + "</td>");
            strbuild_Approvers.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["approved_on"]).Trim() + "</td>");
            strbuild_Approvers.Append("<td style='width:35%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Remarks"]).Trim() + "</td></tr>");
        }
        strbuild_Approvers.Append("</table>");

        string strSubject = "OneHR: Invoice no. " + Convert.ToString(txtInvoiceNo.Text).Trim() + " is Cancelled";
        string strInvoiceURL = "";
        strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["approverLink_VSCB"]).Trim() + "?invid=" + hdnInvoiceId.Value).Trim();
        StringBuilder strbuild = new StringBuilder();

        strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'>");
        strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>");
        strbuild.Append("<tr><td colspan='2'> " + hflEmpName.Value + " has Cancelled an invoice with the following details.</td></tr>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>");


        if (chkIsInvoiceWithoutPO.Checked == true)
        {
            //strbuild.Append("<tr><td>Department :-</td><td>" + Convert.ToString(lstProjectDept.SelectedItem.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(lstProjectDept.SelectedItem.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Vendor Name :-</td><td>" + Convert.ToString(lstVendors.SelectedItem.Text).Trim() + "</td></tr>");
        }

        strbuild.Append("<tr><td style='height:20px'></td></tr>");
        strbuild.Append("<tr><td>Invoice No :-</td><td>" + Convert.ToString(txtInvoiceNo.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>Invoice Date :-</td><td>" + Convert.ToString(txtInvoiceDate.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>Invoice Amount (With GST):-</td><td>" + Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() + "</td></tr>");
        if (ChkAdvSettlement.Checked == true)
        {
            strbuild.Append("<tr><td>Advance Settlement Amount :-</td><td>" + Convert.ToString(txtAdv_Settlement_Amt.Text).Trim() + "</td></tr>");
        }
        if (ChkSecuritySettlement.Checked == true)
        {
            strbuild.Append("<tr><td>Security Settlement Amount :-</td><td>" + Convert.ToString(txtAdv_Settlement_Amt.Text).Trim() + "</td></tr>");
            //strbuild.Append("<tr><td>Invoice Payable Amount :-</td><td>" + Convert.ToString(txtAdv_Bal_Amt.Text).Trim() + "</td></tr>");
        }
        if (Convert.ToString(txtAdv_Bal_Amt.Text).Trim() != "")
        {
            strbuild.Append("<tr><td>Invoice Payable Amount :-</td><td>" + Convert.ToString(txtAdv_Bal_Amt.Text).Trim() + "</td></tr>");
        }

        if (chkIsInvoiceWithoutPO.Checked == false)
        {

            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td colspan=3>");
            strbuild.Append("<table cellpadding='5' cellspacing='0' style='font-size:11pt;font-family:Arial;font-style:Regular;width:100%;'>");
            strbuild.Append("<tr><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Milestone Particular</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Milestone Amount</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Milestone Balance Amount </th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Invoice Amount (wihtout GST) </th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Invoice Amount (wiht GST) </th></tr>");
            for (Int32 irow = 0; irow < dsMilestoneList.Tables[0].Rows.Count; irow++)
            {
                strbuild.Append("<tr><td style='width:40%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestoneList.Tables[0].Rows[irow]["MilestoneName"]).Trim() + " </td>");
                strbuild.Append("<td style='width:15%;border: 1px solid #ccc;text-align:right'>" + Convert.ToString(dsMilestoneList.Tables[0].Rows[irow]["AmtWithTax"]).Trim() + "</td>");
                strbuild.Append("<td style='width:15%;border: 1px solid #ccc;text-align:right'>" + Convert.ToString(dsMilestoneList.Tables[0].Rows[irow]["Milesstone_Balance_Amt"]).Trim() + "</td>");
                strbuild.Append("<td style='width:15%;border: 1px solid #ccc;text-align:right'>" + Convert.ToString(dsMilestoneList.Tables[0].Rows[irow]["Milesstone_Amt_ForInvoice"]).Trim() + "</td>");
                strbuild.Append("<td style='width:15%;border: 1px solid #ccc;text-align:right'>" + Convert.ToString(dsMilestoneList.Tables[0].Rows[irow]["MilestoneAmt_WithTax_ForInvoice"]).Trim() + "</td>");
            }

            strbuild.Append("</table>");
            strbuild.Append("</td></tr>");


            //strbuild.Append("<tr><td style='height:20px'></td></tr>");
            //strbuild.Append("<tr><td>Milestone Particular :-</td><td>" + Convert.ToString(txtMilestoneName_Invoice.Text).Trim() + "</td></tr>");
            //strbuild.Append("<tr><td>Milestone Amount :-</td><td>" + Convert.ToString(txtMilestoneAmt_Invoice.Text).Trim() + "</td></tr>");
            //strbuild.Append("<tr><td>Milestone Balance Amount :-</td><td>" + Convert.ToString(txtMilestoneBalanceAmt_Invoice.Text).Trim() + "</td></tr>");

            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td>PO/WO No :-</td><td>" + Convert.ToString(lstTripType.SelectedItem.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>PO/WO Type :-</td><td>" + Convert.ToString(txtPOtype.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>PO/WO Date :-</td><td>" + Convert.ToString(txtFromdate.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Vendor Name :-</td><td>" + Convert.ToString(txtVendor.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(txtCostCenter.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>PO/WO Amount (With GST):-</td><td>" + Convert.ToString(txtPOWOAmt.Text).Trim() + "</td></tr>");
            //strbuild.Append("<tr><td>Department :-</td><td>" + Convert.ToString(txtProject.Text).Trim() + "</td></tr>");
            if (ChkAdvSettlement.Checked == true)
            {
                strbuild.Append("<tr><td>Payment Advance Amount :-</td><td>" + Convert.ToString(txt_Pay_Adv_Amt.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Payment Advance Settlement :-</td><td>" + Convert.ToString(txt_Pay_Adv_Settlement.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Payment Advance Bal Amount :-</td><td>" + Convert.ToString(txt_Pay_Adv_Bal_Amt.Text).Trim() + "</td></tr>");

            }


        }



        strbuild.Append("<tr><td style='height:20px'></td></tr>");
        // strbuild.Append("<tr><td style='height:20px'><a href='" + strInvoiceURL + "'> Please Click here for your action </a></td></tr>");

        strbuild.Append("</table>  <br /><br />");

        strbuild.Append(strbuild_Approvers);

        spm.sendMail_VSCB(hflEmailAddress.Value, strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);


        #endregion



        Response.Redirect("VSCB_MyInvoice.aspx");
    }

    protected void lnkTravelDetailsEdit_Click(object sender, EventArgs e)
    {

        //ImageButton btn = (ImageButton)sender;
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnSrno.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnpamentStatusid.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[1]).Trim();
        hdnMilestoneID.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[2]).Trim();

        hdnTotalInvoiceAmt.Value = "0";
        txtInvoiceNo.Text = "";
        txtInvoiceDate.Text = "";
        txtAmtWithOutTax.Text = "";
        txtAmtWithTax_Invoice.Text = "";

        txtMilestoneDirectTaxAmt.Text = "";
        txtMilestonePaidAmt.Text = "";


        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.getMilestone_Details_CreateInvoice(Convert.ToDouble(hdnMilestoneID.Value), Convert.ToDouble(lstTripType.SelectedValue));
        hdnCGSTPer_O.Value = "";
        hdnSGSTPer_O.Value = "";
        hdnIGSTPer_O.Value = "";
        txtCGST_Per.Text = "";
        txtSGST_Per.Text = "";
        txtIGST_Per.Text = "";
        txtCGST_Amt.Text = "";
        txtSGST_Amt.Text = "";
        txtIGST_Amt.Text = "";

        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
            txtMilestoneName_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MilestoneName"]).Trim();
            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["GSTIN_No"]).Trim() != "")
            {
                if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Per"]).Trim() != "")
                {
                    if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Per"]).Trim() != "0.00")
                    {
                        txtCGST_Per.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dsMilestone.Tables[0].Rows[0]["CGST_Per"]), 2)).Trim();
                        hdnCGSTPer_O.Value = Convert.ToString(Math.Round(Convert.ToDecimal(dsMilestone.Tables[0].Rows[0]["CGST_Per"]), 2)).Trim();
                    }
                }

                if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Per"]).Trim() != "")
                {
                    if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Per"]).Trim() != "0.00")
                    {
                        txtSGST_Per.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dsMilestone.Tables[0].Rows[0]["SGST_Per"]), 2)).Trim();
                        hdnSGSTPer_O.Value = Convert.ToString(Math.Round(Convert.ToDecimal(dsMilestone.Tables[0].Rows[0]["SGST_Per"]), 2)).Trim();
                    }
                }

                if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Per"]).Trim() != "")
                {
                    if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Per"]).Trim() != "0.00")
                    {
                        txtIGST_Per.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dsMilestone.Tables[0].Rows[0]["IGST_Per"]), 2)).Trim();
                        hdnIGSTPer_O.Value = Convert.ToString(Math.Round(Convert.ToDecimal(dsMilestone.Tables[0].Rows[0]["IGST_Per"]), 2)).Trim();
                    }
                }
            }

            txtMilestoneAmt_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Milestoneamt"]).Trim();
            txtMilestoneBalanceAmt_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Milesstone_Balance_Amt"]).Trim();

            txtMilestonePaidAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MilestonePaidAmt"]).Trim();
            txtMilestoneDirectTaxAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Collect_TDS_Amt"]).Trim();

            //txtInvoiceDate.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Invoicedate"]).Trim();






            //txtCGST_Per.Enabled = true;
            //txtSGST_Per.Enabled = true;
            //txtIGST_Per.Enabled = true;

            //if (Convert.ToString(txtGSTIN_No.Text) == "")
            //{
            //    txtCGST_Per.Enabled = false;
            //    txtSGST_Per.Enabled = false;
            //    txtIGST_Per.Enabled = false;
            //}

            //
            txtCGST_Per.Enabled = false;
            txtSGST_Per.Enabled = false;
            txtIGST_Per.Enabled = false;

        }
    }

    protected void txtAmtWithOutTax_TextChanged(object sender, EventArgs e)
    {

        if (Convert.ToString(txtAmtWithOutTax.Text).Trim() == "" || Convert.ToString(txtAmtWithOutTax.Text).Trim() == "0")
        {
            txtCGST_Per.Text = "";
            txtSGST_Per.Text = "";
            txtIGST_Per.Text = "";
            txtCGST_Amt.Text = "";
            txtSGST_Amt.Text = "";
            txtIGST_Amt.Text = "";
        }

        if (chkIsInvoiceWithoutPO.Checked == true)
        {
            if (Convert.ToString(txtAmtWithOutTax.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Amount (Without GST).";
                return;
            }

            if (Convert.ToDouble(txtAmtWithOutTax.Text) > 10000)
            {
                lblmessage.Text = "Invoice Amount more than 10000 not allowed.";
                txtAmtWithOutTax.Text = "";
                return;
            }
        }
        check_get_GSTPercenatgeAmt();
    }


    protected void gvuploadedFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void lnkDeleteexpFile_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            Int32 ifileid = Convert.ToInt32(gvuploadedFiles.DataKeys[row.RowIndex].Values[0]);
            LinkButton lnkviewfile = (LinkButton)row.FindControl("lnkviewfile");
            hdnfileid.Value = "";
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBInvoicefiles"]).Trim()), lnkviewfile.Text);

            if (System.IO.File.Exists(strfilepath))
            {
                System.IO.File.Delete(strfilepath);
                hdnSrno.Value = Convert.ToString(ifileid);

                spm.InsertInvoiceUploaded_Files(Convert.ToDouble(hdnInvoiceId.Value), "deletefile_srno", "", "Invoice", ifileid);
                getInvoiceUploadedFiles();
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    protected void chkIsInvoiceWithoutPO_CheckedChanged(object sender, EventArgs e)
    {
        get_ProjectDept_Vendor_List();
        CheckIs_Invoice_Wthout_POWO();
    }

    protected void lstVendors_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnVendorId.Value = lstVendors.SelectedValue;
        //txtCGST_Per.ReadOnly = true;
        //txtSGST_Per.ReadOnly = true;
        //txtIGST_Per.ReadOnly = true;

        //txtCGST_Per.Enabled = false;
        //txtSGST_Per.Enabled = false;
        //txtIGST_Per.Enabled = false;

        //DataSet dsProjectsVendors = new DataSet();
        //SqlParameter[] spars = new SqlParameter[2];

        //spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        //spars[0].Value = "get_Vendor_Details";

        //spars[1] = new SqlParameter("@Srno", SqlDbType.Int);
        //spars[1].Value = Convert.ToInt32(lstVendors.SelectedValue);


        //dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        //if (dsProjectsVendors.Tables[0].Rows.Count > 0)
        //{
        //    if (Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["GSTIN_NO"]).Trim() != "")
        //    {
        //        txtCGST_Per.ReadOnly = false;
        //        txtSGST_Per.ReadOnly = false;
        //        txtIGST_Per.ReadOnly = false;

        //        txtCGST_Per.Enabled = true;
        //        txtSGST_Per.Enabled = true;
        //        txtIGST_Per.Enabled = true;
        //    }
        //}
    }

    protected void lstProjectDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnProject_Dept_Name.Value = lstProjectDept.SelectedItem.Text;
        hdnProject_Dept_Id.Value = lstProjectDept.SelectedValue;
        hdnPOTypeId.Value = lstDisplayPOTypes.SelectedValue;
        get_Approver_List();
        Check_CostCenterApprovalMatrix(Convert.ToString(lstProjectDept.SelectedItem.Text).Trim());


    }

    protected void lnkTravelDetailsEdit_Click1(object sender, EventArgs e)
    {
        // ImageButton btn = (ImageButton)sender;
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnInvoiceId.Value = Convert.ToString(gvMngPaymentList_Batch.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnMilestoneID.Value = Convert.ToString(gvMngPaymentList_Batch.DataKeys[row.RowIndex].Values[1]).Trim();
        hdnpamentStatusid.Value = Convert.ToString(gvMngPaymentList_Batch.DataKeys[row.RowIndex].Values[2]).Trim();

        if (hdnpamentStatusid.Value == "2" || hdnpamentStatusid.Value == "3")
        {
            trvl_btnSave.Visible = false;
            btnCancel.Visible = false;
            txtInvoiceNo.Enabled = false;
            // txtinvoice_description.Enabled = false;
            txtInvoiceDate.Enabled = false;
            txtAmtWithOutTax.Enabled = false;
        }

        txtCGST_Per.Text = "";
        txtSGST_Per.Text = "";
        txtIGST_Per.Text = "";
        txtInvoiceNo.Text = "";
        txtInvoiceDate.Text = "";
        txtAmtWithOutTax.Text = "";
        txtAmtWithTax_Invoice.Text = "";
        // txtinvoice_description.Text = "";
        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.getInvoice_Details_Edit_View(Convert.ToDouble(hdnInvoiceId.Value), Convert.ToDouble(hdnMilestoneID.Value), Convert.ToDouble(lstTripType.SelectedValue));

        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
            trvl_btnSave.Text = "Update Invoice";
            txtMilestoneName_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MilestoneName"]).Trim();
            txtInvoiceNo.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["InvoiceNo"]).Trim();
            txtInvoiceDate.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["InvoiceDate"]).Trim();
            txtAmtWithOutTax.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["AmtWithoutTax"]).Trim();
            txtCGST_Per.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Per"]).Trim();
            txtSGST_Per.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Per"]).Trim();
            txtIGST_Per.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Per"]).Trim();
            txtAmtWithTax_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["AmtWithTax"]).Trim();
            txtMilestoneAmt_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MilestoneAmt"]).Trim();
            txtMilestoneBalanceAmt_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Milesstone_Balance_Amt"]).Trim();
            //txtMilestoneBalanceAmt_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["invoices_description"]).Trim();

            txtCGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Amt"]).Trim();
            txtSGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Amt"]).Trim();
            txtIGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Amt"]).Trim();
            if (Convert.ToString(txtCGST_Per.Text).Trim() != "" && Convert.ToString(txtCGST_Per.Text).Trim() != "0.00")
            {
                txtIGST_Per.Enabled = false;
            }
            else if (Convert.ToString(txtIGST_Per.Text).Trim() != "" && Convert.ToString(txtIGST_Per.Text).Trim() != "0.00")
            {
                txtCGST_Per.Enabled = false;
                txtSGST_Per.Enabled = false;
            }

            lnkfile_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Invoice_File"]).Trim();
            lnkfile_Invoice.Visible = true;

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["Status_id"]).Trim() == "2")
            {
                trvl_btnSave.Visible = false;
            }
            gvuploadedFiles.DataSource = null;
            gvuploadedFiles.DataBind();
            if (dsMilestone.Tables[1].Rows.Count > 0)
            {
                gvuploadedFiles.DataSource = dsMilestone.Tables[1];
                gvuploadedFiles.DataBind();
            }
            get_Approver_List();
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

    #endregion

    #region PageMethods
    private void check_get_GSTPercenatgeAmt()
    {

        Decimal dMilestoneBalAmt = 0;

        if (chkIsInvoiceWithoutPO.Checked == false)
        {
            if (Convert.ToString(txtCGST_Per.Text).Trim() != "")
            {
                txtIGST_Amt.Text = "";
                txtIGST_Per.Enabled = false;
            }

            #region Validation  
            if (Convert.ToString(txtAmtWithOutTax.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Amount (Without Tax).";
                return;
            }
            if (Convert.ToString(txtAmtWithOutTax.Text).Trim() != "")
            {
                string[] strdate;
                strdate = Convert.ToString(txtAmtWithOutTax.Text).Trim().Split('.');
                if (strdate.Length > 2)
                {
                    txtAmtWithOutTax.Text = "";
                    lblmessage.Text = "Please enter correct Amount (Without Tax).";
                    return;
                }

                Decimal dfare = 0;
                dfare = Convert.ToDecimal(txtAmtWithOutTax.Text);
                if (dfare == 0)
                {
                    txtAmtWithOutTax.Text = "0";
                    lblmessage.Text = "Please enter correct Amount (Without Tax).";
                    return;
                }
            }

            if (Convert.ToString(txtMilestoneBalanceAmt_Invoice.Text).Trim() == "")
            {
                lblmessage.Text = "Milestone Balance Amount not availabe. Please contact to admin.";
                return;

            }
            if (Convert.ToString(txtMilestoneAmt_Invoice.Text).Trim() != "")
            {
                dMilestoneBalAmt = Math.Round(Convert.ToDecimal(txtMilestoneAmt_Invoice.Text), 2);
            }

            #endregion

            //get Total Invoice Amount againt Selected Milestone
            double dInvoiceId = 0;
            if (Convert.ToString(hdnInvoiceId.Value).Trim() != "")
                dInvoiceId = Convert.ToDouble(hdnInvoiceId.Value);

            DataSet ldsMilestoneAmt = spm.get_Milestone_Inovice_Amount(Convert.ToDouble(hdnMilestoneID.Value), dInvoiceId);
            hdnTotalInvoiceAmt.Value = "0";
            if (ldsMilestoneAmt.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(ldsMilestoneAmt.Tables[0].Rows[0]["TotalInvoiceAmt"]).Trim() != "")
                {
                    hdnTotalInvoiceAmt.Value = Convert.ToString(ldsMilestoneAmt.Tables[0].Rows[0]["TotalInvoiceAmt"]);
                }
            }

        }

        if (chkIsInvoiceWithoutPO.Checked == true)
        {
            if (Convert.ToString(txtAmtWithOutTax.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Amount (Without Tax).";
                return;
            }
            if (Convert.ToString(txtAmtWithOutTax.Text).Trim() != "")
            {
                string[] strdate;
                strdate = Convert.ToString(txtAmtWithOutTax.Text).Trim().Split('.');
                if (strdate.Length > 2)
                {
                    txtAmtWithOutTax.Text = "";
                    lblmessage.Text = "Please enter correct Amount (Without Tax).";
                    return;
                }

                Decimal dfare = 0;
                dfare = Convert.ToDecimal(txtAmtWithOutTax.Text);
                if (dfare == 0)
                {
                    txtAmtWithOutTax.Text = "0";
                    lblmessage.Text = "Please enter correct Amount (Without Tax).";
                    return;
                }
            }
        }

        Decimal rCGST = 0;
        Decimal rSGST = 0;
        Decimal rIGST = 0;

        if (Convert.ToString(txtCGST_Per.Text).Trim() != "")
        {
            rCGST = Math.Round(Convert.ToDecimal(txtCGST_Per.Text) * Convert.ToDecimal(txtAmtWithOutTax.Text) / 100, 2);

        }

        if (Convert.ToString(txtSGST_Per.Text).Trim() != "")
        {
            rSGST = Math.Round(Convert.ToDecimal(txtSGST_Per.Text) * Convert.ToDecimal(txtAmtWithOutTax.Text) / 100, 2);

        }
        if (Convert.ToString(txtIGST_Per.Text).Trim() != "")
        {
            rIGST = Math.Round(Convert.ToDecimal(txtIGST_Per.Text) * Convert.ToDecimal(txtAmtWithOutTax.Text) / 100, 2);

        }

        if (Convert.ToString(txtAmtWithOutTax.Text).Trim() != "")
            txtAmtWithTax_Invoice.Text = Convert.ToString(Math.Round(rCGST + rSGST + rIGST + Convert.ToDecimal(txtAmtWithOutTax.Text), 2)).Trim();
        else
            txtAmtWithTax_Invoice.Text = "";


        txtCGST_Amt.Text = Convert.ToString(rCGST);
        txtSGST_Amt.Text = Convert.ToString(rSGST);
        txtIGST_Amt.Text = Convert.ToString(rIGST);

        decimal dTotalInvoiceAmt = Convert.ToDecimal(hdnTotalInvoiceAmt.Value) + Convert.ToDecimal(txtAmtWithTax_Invoice.Text);

        if (chkIsInvoiceWithoutPO.Checked == false)
        {
            if (Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() != "")
            {
                if (dTotalInvoiceAmt > dMilestoneBalAmt)
                {
                    txtAmtWithTax_Invoice.Text = "";
                    lblmessage.Text = "Overall Invoice amount Exceeding Milestone Amount. Please correct and try again!";
                    return;
                }
            }
        }

        #region format Currency

        DataSet dtPOWODetails = new DataSet();
        // SqlParameter[] spars = new SqlParameter[6];

        // spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        // spars[0].Value = "get_formated_Invoice_Amount"; 

        // spars[1] = new SqlParameter("@InvoiceAmt", SqlDbType.Decimal);
        // if (Convert.ToString(txtAmtWithOutTax.Text).Trim() != "")
        //     spars[1].Value = Convert.ToDecimal(txtAmtWithOutTax.Text);
        // else
        //     spars[1].Value = 0;

        //spars[2] = new SqlParameter("@CGSTAmt", SqlDbType.Decimal);
        // if (Convert.ToString(txtCGST_Amt.Text).Trim() != "")
        //     spars[2].Value = Convert.ToDecimal(txtCGST_Amt.Text);
        // else
        //     spars[2].Value = 0;

        // spars[3] = new SqlParameter("@SGSTAmt", SqlDbType.Decimal);
        // if (Convert.ToString(txtSGST_Amt.Text).Trim() != "")
        //     spars[3].Value = Convert.ToDecimal(txtCGST_Amt.Text);
        // else
        //     spars[3].Value = 0;

        // spars[4] = new SqlParameter("@IGSTAmt", SqlDbType.Decimal);
        // if (Convert.ToString(txtIGST_Amt.Text).Trim() != "")
        //     spars[4].Value = Convert.ToDecimal(txtCGST_Amt.Text);
        // else
        //     spars[4].Value = 0;

        // spars[5] = new SqlParameter("@InvoiceAmtwithGST", SqlDbType.Decimal);
        // if (Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() != "")
        //     spars[5].Value = Convert.ToDecimal(txtAmtWithTax_Invoice.Text);
        // else
        //     spars[5].Value = 0;

        decimal dinvoiceAmt = 0, dCGSTAmt = 0, dSGSTAmt = 0, dIGSTAmt = 0, dInvoiceAmtWithtax = 0;

        if (Convert.ToString(txtAmtWithOutTax.Text).Trim() != "")
            dinvoiceAmt = Convert.ToDecimal(txtAmtWithOutTax.Text);

        if (Convert.ToString(txtCGST_Amt.Text).Trim() != "")
            dCGSTAmt = Convert.ToDecimal(txtCGST_Amt.Text);

        if (Convert.ToString(txtSGST_Amt.Text).Trim() != "")
            dSGSTAmt = Convert.ToDecimal(txtSGST_Amt.Text);

        if (Convert.ToString(txtIGST_Amt.Text).Trim() != "")
            dIGSTAmt = Convert.ToDecimal(txtIGST_Amt.Text);

        if (Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() != "")
            dInvoiceAmtWithtax = Convert.ToDecimal(txtAmtWithTax_Invoice.Text);


        dtPOWODetails = spm.getFormated_Amount("get_formated_Invoice_Amount", dinvoiceAmt, dCGSTAmt, dSGSTAmt, dIGSTAmt, dInvoiceAmtWithtax);


        if (dtPOWODetails.Tables[0].Rows.Count > 0)
        {
            txtAmtWithOutTax.Text = Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["invoiceAmt"]).Trim();
            txtCGST_Amt.Text = Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["cgstAmt"]).Trim();
            txtSGST_Amt.Text = Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["sgstAmt"]).Trim();
            txtIGST_Amt.Text = Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["igstAmt"]).Trim();
            txtAmtWithTax_Invoice.Text = Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["invoiceAmtwithGst"]).Trim();
        }


        #endregion
    }

    private void Create_Invoice_WithPOWO()
    {
        Boolean blnIswithPO = true;
        Boolean blnIsAdvSettlement = true;
        Boolean blnIsSecuritySettlement = true;
        Decimal Adv_Settlement_Amt = 0;
        Decimal Security_Settlement_Amt = 0;
        string strInvoiceDate_FileName = "";
        if (chkIsInvoiceWithoutPO.Checked == true)
        {
            blnIswithPO = false;
        }
        else
        {
            txtAmtWithOutTax.Enabled = false;
            txtAmtWithOutTax.ReadOnly = true;
        }

        string[] strdate;
        string strInvoiceDate = "";
        string strSupplierInvoiceDate = "";

        #region date formatting
        if (Convert.ToString(txtInvoiceDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtInvoiceDate.Text).Trim().Split('-');
            strInvoiceDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            strInvoiceDate_FileName = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
        }
        if (Convert.ToString(txtSupplierInvoiceDt.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtSupplierInvoiceDt.Text).Trim().Split('-');
            strSupplierInvoiceDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        #endregion

        Decimal dMilestoneBalAmt = 0;
        if (blnIswithPO == true)
        {
            #region validations for Invoice With PO
            if (Convert.ToString(lstTripType.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please Select PO/WO Number.";
                return;
            }


            if (Convert.ToString(lstPOType.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please Select PO/WO Type.";
                return;
            }

            if (Convert.ToString(txtAmtWithOutTax.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Amount (Without Tax).";
                return;
            }

            if (Convert.ToString(txtInvoiceNo.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Invoice No.";
                return;
            }
            if (Convert.ToString(txtInvoiceDate.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Invoice Date.";
                return;
            }
            if (Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Amount (Without Tax).";
                return;
            }

            if (Convert.ToString(txtAmtWithOutTax.Text).Trim() != "")
            {
                string[] strdate1;
                strdate1 = Convert.ToString(txtAmtWithOutTax.Text).Trim().Split('.');
                if (strdate1.Length > 2)
                {
                    txtAmtWithOutTax.Text = "";
                    lblmessage.Text = "Please enter correct Amount (Without Tax).";
                    return;
                }

                if (Convert.ToString(txtinvoice_description.Text).Trim() == "")
                {
                    lblmessage.Text = "Please Enter Invoice Description.";
                    return;
                }

                Decimal dfare = 0;
                dfare = Convert.ToDecimal(txtAmtWithOutTax.Text);
                if (dfare == 0)
                {
                    txtAmtWithOutTax.Text = "";
                    lblmessage.Text = "Please enter correct Amount (Without Tax).";
                    return;
                }
            }
            #region Advance Settlement Amt
            if (ChkAdvSettlement.Checked == true)
            {
                if (Convert.ToString(txtAdv_Settlement_Amt.Text).Trim() == "")
                {
                    lblmessage.Text = "Please enter  Advance Settlement Amt.";
                    return;
                }
                if (Convert.ToString(txtAdv_Settlement_Amt.Text).Trim() != "")
                {
                    string[] strdate1;
                    strdate1 = Convert.ToString(txtAdv_Settlement_Amt.Text).Trim().Split('.');
                    if (strdate1.Length > 2)
                    {
                        txtAdv_Settlement_Amt.Text = "";
                        lblmessage.Text = "Please enter correct Advance Settlement Amt.";
                        return;
                    }
                    Decimal dfare = 0;
                    dfare = Convert.ToDecimal(txtAdv_Settlement_Amt.Text);
                    if (dfare == 0)
                    {
                        txtAdv_Settlement_Amt.Text = "";
                        lblmessage.Text = "Please enter correct Advance Settlement Amt.";
                        return;
                    }

                    //  Set_Advance_Settlement_Amt(); 

                }
            }



            if (ChkSecuritySettlement.Checked == true)
            {
                if (Convert.ToString(txtSecurityDeposit.Text).Trim() == "")
                {
                    lblmessage.Text = "Please enter  Security Deposit Amt.";
                    return;
                }
                if (Convert.ToString(txtSecurityDeposit.Text).Trim() != "")
                {
                    string[] strdate1;
                    strdate1 = Convert.ToString(txtSecurityDeposit.Text).Trim().Split('.');
                    if (strdate1.Length > 2)
                    {
                        txtSecurityDeposit.Text = "";
                        lblmessage.Text = "Please enter correct Security Deposit Amt.";
                        return;
                    }
                    Decimal dfare = 0;
                    dfare = Convert.ToDecimal(txtSecurityDeposit.Text);
                    if (dfare == 0)
                    {
                        txtSecurityDeposit.Text = "";
                        lblmessage.Text = "Please enter correct Security Deposit Amt.";
                        return;
                    }
                }
            }
            #endregion

            if (ChkAdvSettlement.Checked == true)
            {
                DataTable dtBal = new DataTable();
                decimal AdvBalAmt = 0, MilestoneSettlementAmt = 0, BasePOWOWAmt = 0, InvoiceBalAmt = 0, InvoiceAmt = 0, InvoiceAvailAmt = 0;

                SqlParameter[] spars4 = new SqlParameter[3];
                spars4[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
                spars4[0].Value = "Get_ADV_Pay_Invoice_Amt";
                spars4[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                spars4[1].Value = txtEmpCode.Text;
                spars4[2] = new SqlParameter("@POID", SqlDbType.BigInt);
                spars4[2].Value = Convert.ToDouble(lstTripType.SelectedValue);
                dtBal = spm.getDropdownList(spars4, "sp_VSCB_CreatePOWO_Users");
                if (dtBal.Rows.Count > 0)
                {
                    if (Convert.ToString(dtBal.Rows[0]["Adv_Pay_Bal"].ToString()) != "")
                    {
                        AdvBalAmt = Convert.ToDecimal(dtBal.Rows[0]["Adv_Pay_Bal"].ToString());
                    }
                    if (Convert.ToString(dtBal.Rows[0]["InvoiceBalAmt"].ToString()) != "")
                    {
                        InvoiceBalAmt = Convert.ToDecimal(dtBal.Rows[0]["InvoiceBalAmt"].ToString());
                    }


                    foreach (GridViewRow crow in dgTravelRequest.Rows)
                    {
                        TextBox txtMilestoneSettlementAmt = (TextBox)crow.FindControl("txtMilestoneSettlementAmt");
                        if (Convert.ToString(txtMilestoneSettlementAmt.Text).Trim() != "")
                        {
                            if (Convert.ToString(txtMilestoneSettlementAmt.Text).Trim() != "0" && Convert.ToString(txtMilestoneSettlementAmt.Text).Trim() != "0.00")
                            {
                                MilestoneSettlementAmt = Convert.ToDecimal(txtMilestoneSettlementAmt.Text);
                            }
                        }
                    }
                    InvoiceAmt = Convert.ToDecimal(Convert.ToDecimal(txtAmtWithOutTax.Text) + MilestoneSettlementAmt);
                    InvoiceAmt = InvoiceAmt + InvoiceBalAmt;
                    BasePOWOWAmt = Convert.ToDecimal(txtPOWOAmt.Text);
                    InvoiceAvailAmt = BasePOWOWAmt - InvoiceAmt;

                    if (Convert.ToString(txtAdv_Settlement_Amt.Text).Trim() != "" && AdvBalAmt != 0)
                    {
                        if (Convert.ToDecimal(txtAdv_Settlement_Amt.Text) > AdvBalAmt)
                        {
                            lblmessage.Text = "Overall Advance settlement amount is exceeding than Advance amount.";
                            return;
                        }
                    }
                    if (Convert.ToString(txtAdv_Settlement_Amt.Text).Trim() != "")
                    {
                        AdvBalAmt = Math.Abs(AdvBalAmt - Convert.ToDecimal(txtAdv_Settlement_Amt.Text));
                    }
                    if (InvoiceAvailAmt < AdvBalAmt)
                    {
                        lblmessage.Text = "Please Settlement advance amount.";
                        return;
                    }
                }
            }
            #endregion


            #region Add costcenter button and grid not required
            /*
            if (Validate_CostcenterAmt(TextBox1, "Y") ==false)
            {
                return;
            }
            */
            #endregion



        }

        if (blnIswithPO == false)
        {
            hdnMilestoneID.Value = "0";
            #region validations for Invoice Without PO
            if (Convert.ToString(lstProjectDept.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please Select Department Name.";
                return;
            }

            if (Convert.ToString(lstVendors.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please Select Vendor Name.";
                return;
            }
            if (Convert.ToString(lstDisplayPOTypes.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please Select Invoice Type.";
                return;
            }

            if (Convert.ToString(txtInvocieRemarks.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Invoice Description.";
                return;
            }



            if (Convert.ToString(txtAmtWithOutTax.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Amount (Without Tax).";
                return;
            }
            if (Convert.ToString(txtInvoiceNo.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Invoice No.";
                return;
            }
            if (Convert.ToString(txtInvoiceDate.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Invoice Date.";
                return;
            }
            if (Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Amount (Without Tax).";
                return;
            }

            if (Convert.ToString(txtAmtWithOutTax.Text).Trim() != "")
            {
                string[] strdate1;
                strdate1 = Convert.ToString(txtAmtWithOutTax.Text).Trim().Split('.');
                if (strdate1.Length > 2)
                {
                    txtAmtWithOutTax.Text = "";
                    lblmessage.Text = "Please enter correct Amount (Without Tax).";
                    return;
                }

                Decimal dfare = 0;
                dfare = Convert.ToDecimal(txtAmtWithOutTax.Text);
                if (dfare == 0)
                {
                    txtAmtWithOutTax.Text = "";
                    lblmessage.Text = "Please enter correct Amount (Without Tax).";
                    return;
                }
            }


            #endregion
        }

        if (Convert.ToString(hdnInvoiceId.Value) == "0")
        {
            if (!InvoiceUploadfile.HasFile)
            {
                lblmessage.Text = "Please upload Invoice file.";
                return;
            }
        }

        if (Convert.ToString(hdnProject_Dept_Id.Value).Trim() == "" || Convert.ToString(hdnProject_Dept_Id.Value).Trim() == "0")
        {
            lblmessage.Text = "Something went wrong.Cost center is changed.Please correct and try again!. ";
            return;
        }

        #region Check Invoice no is used
        if (Convert.ToString(hdnInvoiceId.Value).Trim() == "0" || Convert.ToString(hdnInvoiceId.Value).Trim() == "")
        {
            DataSet dsmyInvoice = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_CheckInvoice_No_IsUsed";

            spars[1] = new SqlParameter("@powonumber", SqlDbType.VarChar);
            if (chkIsInvoiceWithoutPO.Checked)
                spars[1].Value = Convert.ToString(lstVendors.SelectedItem.Text).Trim();
            else
                spars[1].Value = Convert.ToString(txtVendor.Text).Trim();

            spars[2] = new SqlParameter("@InvoiceNo", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(txtInvoiceNo.Text).Trim();


            spars[3] = new SqlParameter("@InvoiceDate", SqlDbType.VarChar);
            spars[3].Value = Convert.ToString(strInvoiceDate).Trim();


            dsmyInvoice = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            if (dsmyInvoice.Tables[0].Rows.Count > 0)
            {
                //lblmessage.Text = "Invoice No already in used.";
                lblmessage.Text = "Same Invoice No already Exists !";
                return;
            }
            if (dsmyInvoice.Tables[1].Rows.Count > 0)
            {
                if (Convert.ToString(dsmyInvoice.Tables[1].Rows[0]["msg"]).Trim() != "")
                {
                    lblmessage.Text = "future date invoice not allowed"; //dsmyInvoice.Tables[1].Rows[0]["msg"]
                    return;
                }
            }

        }
        #endregion

        #region Delete Invoice Approval Status

        if (Convert.ToString(hdnInvoiceId.Value).Trim() != "0" && Convert.ToString(hdnInvoiceId.Value).Trim() != "")
        {
            DataSet dsDeleteInvoiceAppr = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_CheckInvoice_DeleteApproval";

            spars[1] = new SqlParameter("@InvoiceId", SqlDbType.VarChar);
            spars[1].Value = Convert.ToDouble(hdnInvoiceId.Value);

            dsDeleteInvoiceAppr = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        }
        #endregion

        Decimal rCGST = 0;
        Decimal rSGST = 0;
        Decimal rIGST = 0;

        Decimal rCGST_Per = 0;
        Decimal rSGST_Per = 0;
        Decimal rIGST_Per = 0;

        if (blnIswithPO == false)
        {
            if (Convert.ToString(txtCGST_Per.Text).Trim() != "")
            {
                rCGST_Per = Convert.ToDecimal(txtCGST_Per.Text);
            }

            if (Convert.ToString(txtSGST_Per.Text).Trim() != "")
            {
                rSGST_Per = Convert.ToDecimal(txtSGST_Per.Text);
            }

            if (Convert.ToString(txtIGST_Per.Text).Trim() != "")
            {
                rIGST_Per = Convert.ToDecimal(txtIGST_Per.Text);
            }
        }


        if (Convert.ToString(txtCGST_Amt.Text).Trim() != "")
            rCGST = Math.Round(Convert.ToDecimal(txtCGST_Amt.Text), 2);

        if (Convert.ToString(txtSGST_Amt.Text).Trim() != "")
            rSGST = Math.Round(Convert.ToDecimal(txtSGST_Amt.Text), 2);

        if (Convert.ToString(txtIGST_Amt.Text).Trim() != "")
            rIGST = Math.Round(Convert.ToDecimal(txtIGST_Amt.Text), 2);

        string sInvoiceType = "Insertinvoice";
        if (Convert.ToString(hdnInvoiceId.Value) == "0")
            sInvoiceType = "Insertinvoice";
        else
            sInvoiceType = "Updateinvoice";

        string sPotypeid = "0";
        double dPOID = 0;
        if (blnIswithPO == true)
        {
            sPotypeid = Convert.ToString(lstPOType.SelectedValue);
            dPOID = Convert.ToDouble(lstTripType.SelectedValue);
        }
        else
        {
            sPotypeid = Convert.ToString(lstDisplayPOTypes.SelectedValue);
            dPOID = 0;
        }
        if (ChkAdvSettlement.Checked == true)
        {
            blnIsAdvSettlement = true;
            Adv_Settlement_Amt = Convert.ToDecimal(txtAdv_Settlement_Amt.Text);
        }
        else
        {
            blnIsAdvSettlement = false;
        }
        if (ChkSecuritySettlement.Checked == true)
        {
            blnIsSecuritySettlement = true;
            Security_Settlement_Amt = Convert.ToDecimal(txtSecurityDeposit.Text);
        }
        else
        {
            blnIsSecuritySettlement = false;
        }

        int ichkWithoutPo = 0;
        if (chkIsInvoiceWithoutPO.Checked)
            ichkWithoutPo = 1;
        DataSet dtApproverEmailIds = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), txtEmpCode.Text, hdnProject_Dept_Name.Value, sPotypeid, ichkWithoutPo); //spm.VSCB_GetApproverEmailID("get_Approvers_matrix", Convert.ToString(txtProject.Text).Trim(), 0);

        if (dtApproverEmailIds.Tables[0].Rows.Count > 0)
        {
            string sApproverEmpCode = "";
            int iAppr_id = 0;
            string sApproverEmail = "";
            string sApprover_name = "";
         //   string sIsInvoiceWithPO = "";

            sApproverEmpCode = Convert.ToString(dtApproverEmailIds.Tables[0].Rows[0]["APPR_Emp_Code"]).Trim();
            sApprover_name = Convert.ToString(dtApproverEmailIds.Tables[0].Rows[0]["ApproverName"]).Trim();
            sApproverEmail = Convert.ToString(dtApproverEmailIds.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
            iAppr_id = Convert.ToInt32(dtApproverEmailIds.Tables[0].Rows[0]["APPR_ID"]);
           // sIsInvoiceWithPO = Convert.ToString(dtApproverEmailIds.Tables[0].Rows[0]["IsInvoiceWithPO"]).Trim();

            double dMaxReqId = 0;

            double dProject_Dept_id = 0;
            Int32 iVendorId = 0;
            if (Convert.ToString(hdnProject_Dept_Id.Value).Trim() != "")
            {
                dProject_Dept_id = Convert.ToDouble(hdnProject_Dept_Id.Value);
            }
            if (Convert.ToString(hdnVendorId.Value).Trim() != "")
            {
                iVendorId = Convert.ToInt16(hdnVendorId.Value);
            }
            DataSet dtMaxreqID = new DataSet();
            dtMaxreqID = spm.InsertUpdate_Invoice(txtEmpCode.Text, sInvoiceType, 0, Convert.ToString(txtInvoiceNo.Text).Trim(), strInvoiceDate, Convert.ToDecimal(txtAmtWithOutTax.Text), rCGST, rSGST, rIGST, rCGST_Per, rSGST_Per, rIGST_Per, Convert.ToDecimal(txtAmtWithTax_Invoice.Text), dMilestoneBalAmt, 1, 1, blnIswithPO, dProject_Dept_id, iVendorId, Convert.ToString(hdnInvoiceId.Value), Convert.ToString(txtVoucherNo.Text), strSupplierInvoiceDate, sPotypeid, Convert.ToString(txtInvocieRemarks.Text), dPOID, false, Convert.ToString(txtinvoice_description.Text).Trim());

            dMaxReqId = Convert.ToDouble(dtMaxreqID.Tables[0].Rows[0]["MaxReqID"]);


            #region Add costcenter button and grid not required

            /*if (blnIswithPO==true)
            {
                int isrno = 1;
                foreach (GridViewRow crow in dgCostcenterList.Rows)
                {
                    DropDownList lstMilestoneAmtCostCenter = (DropDownList)crow.FindControl("lstMilestoneAmtCostCenter");
                    TextBox txtCostCenterAmt = (TextBox)crow.FindControl("txtCostCenterAmt");
                    if (Convert.ToString(lstMilestoneAmtCostCenter.SelectedValue).Trim() != "0")
                    {
                        if (Convert.ToString(txtCostCenterAmt.Text).Trim() != "" && Convert.ToString(txtCostCenterAmt.Text).Trim() != "0" && Convert.ToString(txtCostCenterAmt.Text).Trim() != "0.00")
                        {
                            spm.InsertInvoice_CostCeterAmt("Insert_distribute_InvocieAmt_toCostCnter", dMaxReqId, Convert.ToInt32(lstMilestoneAmtCostCenter.SelectedValue), Convert.ToDecimal(txtCostCenterAmt.Text), isrno);
                            isrno += 1;
                        }
                    }

                }
                
            }*/

            #endregion

            if (Convert.ToString(hdnGST_AMT.Value).Trim() == "")
                hdnGST_AMT.Value = "0";

            if (ChkAdvSettlement.Checked == true)
            {
                if (Convert.ToString(txtAdv_Settlement_Amt.Text).Trim() != "")
                {
                    Adv_Settlement_Amt = Convert.ToDecimal(txtAdv_Settlement_Amt.Text);
                }

                spm.Insert_Invoice_Adv_Settlement_Amt("InsertInvoice_Advance_Pay", dMaxReqId, dPOID, Adv_Settlement_Amt, 1, Convert.ToDecimal(hdnGST_AMT.Value));
            }
            if (ChkSecuritySettlement.Checked == true)
            {
                if (Convert.ToString(txtSecurityDeposit.Text).Trim() != "")
                {
                    Security_Settlement_Amt = Convert.ToDecimal(txtSecurityDeposit.Text);
                }

                spm.Insert_Invoice_Adv_Settlement_Amt("InsertInvoice_Advance_Pay", dMaxReqId, dPOID, Security_Settlement_Amt, 2, Convert.ToDecimal(hdnGST_AMT.Value));
            }
            spm.InsertInvoice_ApproverDetails("Insertinvoice_Approver", dMaxReqId, sApproverEmpCode, iAppr_id, "Pending", "", "", "", "");


            #region insert or upload multiple files
            if (InvoiceUploadfile.HasFile)
            {
                string filename = InvoiceUploadfile.FileName;
                string MilestoneFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBInvoicefiles"]).Trim() + "/");
                bool folderExists = Directory.Exists(MilestoneFilePath);
                if (!folderExists)
                {
                    Directory.CreateDirectory(MilestoneFilePath);
                }
                String InputFile = System.IO.Path.GetExtension(InvoiceUploadfile.FileName);


                //filename = txtEmpCode.Text + "_" + Convert.ToString(dMaxReqId).Trim() + "_Invoice_PO" + InputFile;
                //string  sinvoice_no = Regex.Replace(Convert.ToString(txtInvoiceNo.Text) +"_" + Convert.ToString(dMaxReqId).Trim(), @"[^0-9a-zA-Z\._]", "_");   
                string sinvoice_no = Regex.Replace(Convert.ToString(txtInvoiceNo.Text), @"[^0-9a-zA-Z\._]", "_");
                filename = sinvoice_no + "_" + strInvoiceDate_FileName + InputFile;
                InvoiceUploadfile.SaveAs(Path.Combine(MilestoneFilePath, filename));

                spm.InsertInvoiceUploaded_Files(dMaxReqId, "INSERT_InvociePOFile", Convert.ToString(filename).Trim(), "INSERT_InvociePOFile", 0);


            }

            if (ploadexpfile.HasFile)
            {
                string filename = ploadexpfile.FileName;

                if (Convert.ToString(filename).Trim() != "")
                {
                    string InvoiceFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBInvoicefiles"]).Trim() + "/");
                    bool folderExists = Directory.Exists(InvoiceFilePath);
                    if (!folderExists)
                    {
                        Directory.CreateDirectory(InvoiceFilePath);
                    }
                    Int32 t_cnt = 1;

                    if (ploadexpfile.HasFiles)
                    {
                        foreach (HttpPostedFile uploadedFile in ploadexpfile.PostedFiles)
                        {
                            string strfileName = "";
                            string fileName = ReplaceInvalidChars(Path.GetFileName(uploadedFile.FileName));
                            if (uploadedFile.ContentLength > 0)
                            {

                                String InputFile = System.IO.Path.GetExtension(uploadedFile.FileName);
                                //strfileName = txtEmpCode.Text + "_" + Convert.ToString(dMaxReqId).Trim() + "_Invoice" + (t_cnt).ToString() + InputFile;
                                //string sinvoice_no = Regex.Replace(Convert.ToString(txtInvoiceNo.Text) + "_" + Convert.ToString(dMaxReqId).Trim(), @"[^0-9a-zA-Z\._]", "_");
                                string sinvoice_no = Regex.Replace(Convert.ToString(txtInvoiceNo.Text), @"[^0-9a-zA-Z\._]", "_");
                                filename = "Supporting_" + (t_cnt).ToString() + "_" + sinvoice_no + "_" + strInvoiceDate_FileName + InputFile;
                                //filename = strfileName;
                                uploadedFile.SaveAs(Path.Combine(InvoiceFilePath, filename));


                                spm.InsertInvoiceUploaded_Files(dMaxReqId, "INSERT", Convert.ToString(filename).Trim(), "Invoice", t_cnt);

                                t_cnt = t_cnt + 1;
                            }
                        }
                    }

                }


            }
            #endregion


            #region Send Email to 1st Approver

            
            if (chkIsInvoiceWithoutPO.Checked)
                ichkWithoutPo = 1;

            DataSet dsMilestone = new DataSet();
            dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(dMaxReqId), txtEmpCode.Text, hdnProject_Dept_Name.Value, Convert.ToString(sPotypeid), ichkWithoutPo);

            DataSet dsMilestoneList = new DataSet();
            dsMilestoneList = spm.get_Invoice_Milestone_List(Convert.ToDouble(dMaxReqId));


            StringBuilder strbuild_Approvers = new StringBuilder();
            strbuild_Approvers.Append("<table cellpadding='5' cellspacing='0' style='font-size:11pt;font-family:Arial;font-style:Regular;width:100%;'>");
            strbuild_Approvers.Append("<tr><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Approver Name</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Status</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Approved On</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Approver Remarks</th></tr>");
            for (Int32 irow = 0; irow < dsMilestone.Tables[0].Rows.Count; irow++)
            {
                strbuild_Approvers.Append("<tr><td style='width:40%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["ApproverName"]).Trim() + " </td>");
                strbuild_Approvers.Append("<td style='width:10%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() + "</td>");
                strbuild_Approvers.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["approved_on"]).Trim() + "</td>");
                strbuild_Approvers.Append("<td style='width:35%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Remarks"]).Trim() + "</td></tr>");
            }

            strbuild_Approvers.Append("</table>");

            string strSubject = "";
            if (chkIsInvoiceWithoutPO.Checked)
                strSubject = "OneHR: Request for - Invoice Approval - " + Convert.ToString(txtInvoiceNo.Text).Trim();
            else
                strSubject = "OneHR: Request for - Invoice Approval - " + Convert.ToString(txtInvoiceNo.Text).Trim() + " against " + Convert.ToString(lstTripType.SelectedItem.Text).Trim();

            string strInvoiceURL = "";
            strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["approverLink_VSCB"]).Trim() + "?invid=" + dMaxReqId).Trim();
            StringBuilder strbuild = new StringBuilder();

            strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:80%'>");
            strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td colspan='2'> " + hflEmpName.Value + " has created an invoice with the following details and requested your approval.</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");

            strbuild.Append("<tr><td>Invoice No :-</td><td>" + Convert.ToString(txtInvoiceNo.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Invoice Date :-</td><td>" + Convert.ToString(txtInvoiceDate.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Invoice Amount (With GST) :-</td><td>" + Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() + "</td></tr>");

            if (ChkAdvSettlement.Checked == true)
            {
                strbuild.Append("<tr><td>Advance Settlement Amount :-</td><td>" + Convert.ToString(txtAdv_Settlement_Amt.Text).Trim() + "</td></tr>");
            }
            if (ChkSecuritySettlement.Checked == true)
            {
                strbuild.Append("<tr><td>Security Settlement Amount :-</td><td>" + Convert.ToString(txtSecurityDeposit.Text).Trim() + "</td></tr>");
            }
            if (Convert.ToString(txtAdv_Bal_Amt.Text).Trim() != "")
            {
                //strbuild.Append("<tr><td>Invoice Payable Amount :-</td><td>" + Convert.ToString(txtAdv_Bal_Amt.Text).Trim() + "</td></tr>");
            }
            if (chkIsInvoiceWithoutPO.Checked == true)
            {
                // strbuild.Append("<tr><td>Department :-</td><td>" + Convert.ToString(lstProjectDept.SelectedItem.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Vendor Name :-</td><td>" + Convert.ToString(lstVendors.SelectedItem.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(lstProjectDept.SelectedItem.Text).Trim() + "</td></tr>");
            }

            if (chkIsInvoiceWithoutPO.Checked == false)
            {
                strbuild.Append("<tr><td style='height:20px'></td></tr>");
                strbuild.Append("<tr><td colspan=3>");
                strbuild.Append("<table cellpadding='5' cellspacing='0' style='font-size:11pt;font-family:Arial;font-style:Regular;width:100%;'>");
                strbuild.Append("<tr><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Milestone Particular</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Milestone Amount</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Milestone Balance Amount </th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Invoice Amount (wihtout GST) </th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Invoice Amount (wiht GST) </th></tr>");
                for (Int32 irow = 0; irow < dsMilestoneList.Tables[0].Rows.Count; irow++)
                {
                    strbuild.Append("<tr><td style='width:40%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestoneList.Tables[0].Rows[irow]["MilestoneName"]).Trim() + " </td>");
                    strbuild.Append("<td style='width:15%;border: 1px solid #ccc;text-align:right'>" + Convert.ToString(dsMilestoneList.Tables[0].Rows[irow]["AmtWithTax"]).Trim() + "</td>");
                    strbuild.Append("<td style='width:15%;border: 1px solid #ccc;text-align:right'>" + Convert.ToString(dsMilestoneList.Tables[0].Rows[irow]["Milesstone_Balance_Amt"]).Trim() + "</td>");
                    strbuild.Append("<td style='width:15%;border: 1px solid #ccc;text-align:right'>" + Convert.ToString(dsMilestoneList.Tables[0].Rows[irow]["Milesstone_Amt_ForInvoice"]).Trim() + "</td>");
                    strbuild.Append("<td style='width:15%;border: 1px solid #ccc;text-align:right'>" + Convert.ToString(dsMilestoneList.Tables[0].Rows[irow]["MilestoneAmt_WithTax_ForInvoice"]).Trim() + "</td>");
                }

                strbuild.Append("</table>");
                strbuild.Append("</td></tr>");

                //strbuild.Append("<tr><td>Milestone Particular :-</td><td>" + Convert.ToString(txtMilestoneName_Invoice.Text).Trim() + "</td></tr>");
                //strbuild.Append("<tr><td>Milestone Amount :-</td><td>" + Convert.ToString(txtMilestoneAmt_Invoice.Text).Trim() + "</td></tr>");
                //strbuild.Append("<tr><td>Milestone Balance Amount :-</td><td>" + Convert.ToString(txtMilestoneBalanceAmt_Invoice.Text).Trim() + "</td></tr>");

                strbuild.Append("<tr><td style='height:20px'></td></tr>");
                strbuild.Append("<tr><td>PO/WO No :-</td><td>" + Convert.ToString(lstTripType.SelectedItem.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>PO/WO Type :-</td><td>" + Convert.ToString(txtPOtype.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>PO/WO Date :-</td><td>" + Convert.ToString(txtFromdate.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Vendor Name :-</td><td>" + Convert.ToString(txtVendor.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(txtCostCenter.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>PO/WO Amount (With GST):-</td><td>" + Convert.ToString(txtPOWOAmt.Text).Trim() + "</td></tr>");
                if (ChkAdvSettlement.Checked == true)
                {
                    strbuild.Append("<tr><td>Payment Advance Amount :-</td><td>" + Convert.ToString(txt_Pay_Adv_Amt.Text).Trim() + "</td></tr>");
                    strbuild.Append("<tr><td>Payment Advance Settlement :-</td><td>" + Convert.ToString(txt_Pay_Adv_Settlement.Text).Trim() + "</td></tr>");
                    strbuild.Append("<tr><td>Payment Advance Bal Amount :-</td><td>" + Convert.ToString(txt_Pay_Adv_Bal_Amt.Text).Trim() + "</td></tr>");

                }

                //strbuild.Append("<tr><td>Department :-</td><td>" + Convert.ToString(txtProject.Text).Trim() + "</td></tr>");



            }


            strbuild.Append("<tr><td style='height:40px'></td></tr>");
            strbuild.Append("<tr><td style='height:20px'><a href='" + strInvoiceURL + "'> Please Click here for your action </a></td></tr>");

            strbuild.Append("</table>  <br /><br />");

            strbuild.Append(strbuild_Approvers);

            spm.sendMail_VSCB(sApproverEmail, strSubject, Convert.ToString(strbuild).Trim(), "", "");


            #endregion


            Response.Redirect("VSCB_MyInvoice.aspx");
        }
        else
        {
            lblmessage.Text = "Invoice creation failed. Please contact to admin.";
            return;
        }
    }


    private void Create_Invoice_WithPOWO_old()
    {
        Boolean blnIswithPO = true;
        if (chkIsInvoiceWithoutPO.Checked == true)
        {
            blnIswithPO = false;
        }

        string[] strdate;
        string strInvoiceDate = "";
        string strSupplierInvoiceDate = "";

        #region date formatting
        if (Convert.ToString(txtInvoiceDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtInvoiceDate.Text).Trim().Split('-');
            strInvoiceDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(txtSupplierInvoiceDt.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtSupplierInvoiceDt.Text).Trim().Split('-');
            strSupplierInvoiceDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        #endregion

        Decimal dMilestoneBalAmt = 0;
        if (blnIswithPO == true)
        {
            #region validations for Invoice With PO
            if (Convert.ToString(lstTripType.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please Select PO/WO Number.";
                return;
            }

            if (Convert.ToString(hdnInvoiceId.Value).Trim() == "0")
            {
                if (Convert.ToString(txtMilestoneBalanceAmt_Invoice.Text).Trim() == "" || Convert.ToString(txtMilestoneBalanceAmt_Invoice.Text).Trim() == "0" || Convert.ToString(txtMilestoneBalanceAmt_Invoice.Text).Trim() == "0.00")
                {
                    lblmessage.Text = "Milestone Balance Amount is not available.";
                    return;
                }
            }


            if (Convert.ToString(lstPOType.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please Select PO/WO Type.";
                return;
            }

            if (Convert.ToString(txtAmtWithOutTax.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Amount (Without Tax).";
                return;
            }
            //if (Convert.ToString(txtVoucherNo.Text).Trim() == "")
            //{
            //    lblmessage.Text = "Please enter Voucher No.";
            //    return;
            //}
            //if (Convert.ToString(txtSupplierInvoiceDt.Text).Trim() == "")
            //{
            //    lblmessage.Text = "Please enter Supplier Invoice Date.";
            //    return;
            //}
            if (Convert.ToString(txtInvoiceNo.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Invoice No.";
                return;
            }
            if (Convert.ToString(txtInvoiceDate.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Invoice Date.";
                return;
            }
            if (Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Amount (Without Tax).";
                return;
            }

            if (Convert.ToString(txtAmtWithOutTax.Text).Trim() != "")
            {
                string[] strdate1;
                strdate1 = Convert.ToString(txtAmtWithOutTax.Text).Trim().Split('.');
                if (strdate1.Length > 2)
                {
                    txtAmtWithOutTax.Text = "";
                    lblmessage.Text = "Please enter correct Amount (Without Tax).";
                    return;
                }

                Decimal dfare = 0;
                dfare = Convert.ToDecimal(txtAmtWithOutTax.Text);
                if (dfare == 0)
                {
                    txtAmtWithOutTax.Text = "";
                    lblmessage.Text = "Please enter correct Amount (Without Tax).";
                    return;
                }
            }


            /*
            if (Convert.ToString(txtMilestoneBalanceAmt_Invoice.Text).Trim() != "")
            {            
                dMilestoneBalAmt = Math.Round(Convert.ToDecimal(txtMilestoneBalanceAmt_Invoice.Text), 2);
            }

            if (Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() != "")
            {
                if (Convert.ToDecimal(txtAmtWithTax_Invoice.Text) > dMilestoneBalAmt)
                {
                    txtAmtWithTax_Invoice.Text = "";
                    lblmessage.Text = "Amount (With Tax ) is grater than Milestone balance Amount not allowed. Please contact to admin.";
                    return;
                }
            }*/


            #region if GST % change on Invoice
            if (Convert.ToString(txtCGST_Per.Text).Trim() != Convert.ToString(hdnCGSTPer_O.Value).Trim())
            {
                lblmessage.Text = "Can't allowed to change CGST(%).Please shortcloed this milestone.";
                return;
            }
            if (Convert.ToString(txtSGST_Per.Text).Trim() != Convert.ToString(hdnSGSTPer_O.Value).Trim())
            {
                lblmessage.Text = "Can't allowed to change SGST(%).Please shortcloed this milestone.";
                return;
            }
            if (Convert.ToString(txtIGST_Per.Text).Trim() != Convert.ToString(hdnIGSTPer_O.Value).Trim())
            {
                lblmessage.Text = "Can't allowed to change IGST(%).Please shortcloed this milestone.";
                return;
            }
            #endregion

            #endregion
        }

        if (blnIswithPO == false)
        {
            hdnMilestoneID.Value = "0";
            #region validations for Invoice Without PO
            if (Convert.ToString(lstProjectDept.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please Select Department Name.";
                return;
            }

            if (Convert.ToString(lstVendors.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please Select Vendor Name.";
                return;
            }
            if (Convert.ToString(lstDisplayPOTypes.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please Select Invoice Type.";
                return;
            }

            if (Convert.ToString(txtInvocieRemarks.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Invoice Description.";
                return;
            }


            //if (Convert.ToString(txtVoucherNo.Text).Trim() == "")
            //{
            //    lblmessage.Text = "Please enter Voucher No.";
            //    return;
            //}
            //if (Convert.ToString(txtSupplierInvoiceDt.Text).Trim() == "")
            //{
            //    lblmessage.Text = "Please enter Supplier Invoice Date.";
            //    return;
            //}
            if (Convert.ToString(txtAmtWithOutTax.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Amount (Without Tax).";
                return;
            }
            if (Convert.ToString(txtInvoiceNo.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Invoice No.";
                return;
            }
            if (Convert.ToString(txtInvoiceDate.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Invoice Date.";
                return;
            }
            if (Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Amount (Without Tax).";
                return;
            }

            if (Convert.ToString(txtAmtWithOutTax.Text).Trim() != "")
            {
                string[] strdate1;
                strdate1 = Convert.ToString(txtAmtWithOutTax.Text).Trim().Split('.');
                if (strdate1.Length > 2)
                {
                    txtAmtWithOutTax.Text = "";
                    lblmessage.Text = "Please enter correct Amount (Without Tax).";
                    return;
                }

                Decimal dfare = 0;
                dfare = Convert.ToDecimal(txtAmtWithOutTax.Text);
                if (dfare == 0)
                {
                    txtAmtWithOutTax.Text = "";
                    lblmessage.Text = "Please enter correct Amount (Without Tax).";
                    return;
                }
            }


            #endregion
        }
        if (Convert.ToString(hdnInvoiceId.Value) == "0")
        {
            if (!InvoiceUploadfile.HasFile)
            {
                lblmessage.Text = "Please upload Invoice file.";
                return;
            }
        }

        #region Check Invoice no is used
        if (Convert.ToString(hdnInvoiceId.Value).Trim() == "0" || Convert.ToString(hdnInvoiceId.Value).Trim() == "")
        {
            DataSet dsmyInvoice = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_CheckInvoice_No_IsUsed";

            spars[1] = new SqlParameter("@powonumber", SqlDbType.VarChar);
            if (chkIsInvoiceWithoutPO.Checked)
                spars[1].Value = Convert.ToString(lstVendors.SelectedItem.Text).Trim();
            else
                spars[1].Value = Convert.ToString(txtVendor.Text).Trim();

            spars[2] = new SqlParameter("@InvoiceNo", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(txtInvoiceNo.Text).Trim();


            spars[3] = new SqlParameter("@InvoiceDate", SqlDbType.VarChar);
            spars[3].Value = Convert.ToString(strInvoiceDate).Trim();


            dsmyInvoice = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            if (dsmyInvoice.Tables[0].Rows.Count > 0)
            {
                //lblmessage.Text = "Invoice No already in used.";
                lblmessage.Text = "Same Invoice No already Exists !";
                return;
            }
            if (dsmyInvoice.Tables[1].Rows.Count > 0)
            {
                if (Convert.ToString(dsmyInvoice.Tables[1].Rows[0]["msg"]).Trim() != "")
                {
                    lblmessage.Text = "future date invoice not allowed"; //dsmyInvoice.Tables[1].Rows[0]["msg"]
                    return;
                }
            }

        }
        #endregion




        Decimal rCGST = 0;
        Decimal rSGST = 0;
        Decimal rIGST = 0;

        Decimal rCGST_Per = 0;
        Decimal rSGST_Per = 0;
        Decimal rIGST_Per = 0;

        if (Convert.ToString(txtCGST_Per.Text).Trim() != "")
        {
            rCGST = Math.Round(Convert.ToDecimal(txtCGST_Per.Text) * Convert.ToDecimal(txtAmtWithOutTax.Text) / 100, 2);
            rCGST_Per = Convert.ToDecimal(txtCGST_Per.Text);
        }

        if (Convert.ToString(txtSGST_Per.Text).Trim() != "")
        {
            rSGST = Math.Round(Convert.ToDecimal(txtSGST_Per.Text) * Convert.ToDecimal(txtAmtWithOutTax.Text) / 100, 2);
            rSGST_Per = Convert.ToDecimal(txtSGST_Per.Text);
        }

        if (Convert.ToString(txtIGST_Per.Text).Trim() != "")
        {
            rIGST = Math.Round(Convert.ToDecimal(txtIGST_Per.Text) * Convert.ToDecimal(txtAmtWithOutTax.Text) / 100, 2);
            rIGST_Per = Convert.ToDecimal(txtIGST_Per.Text);
        }

        txtAmtWithTax_Invoice.Text = Convert.ToString(Math.Round(rCGST + rSGST + rIGST + Convert.ToDecimal(txtAmtWithOutTax.Text), 2)).Trim();

        string sInvoiceType = "Insertinvoice";
        if (Convert.ToString(hdnInvoiceId.Value) == "0")
            sInvoiceType = "Insertinvoice";
        else
            sInvoiceType = "Updateinvoice";

        string sPotypeid = "0";
        if (blnIswithPO == true)
        {
            sPotypeid = Convert.ToString(lstPOType.SelectedValue);
        }
        else
        {
            sPotypeid = Convert.ToString(lstDisplayPOTypes.SelectedValue);
        }

        int ichkWithoutPo = 0;
        if (chkIsInvoiceWithoutPO.Checked)
            ichkWithoutPo = 1;

        DataSet dtApproverEmailIds = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), txtEmpCode.Text, hdnProject_Dept_Name.Value, sPotypeid, ichkWithoutPo); //spm.VSCB_GetApproverEmailID("get_Approvers_matrix", Convert.ToString(txtProject.Text).Trim(), 0);

        if (dtApproverEmailIds.Tables[0].Rows.Count > 0)
        {
            string sApproverEmpCode = "";
            int iAppr_id = 0;
            string sApproverEmail = "";
            string sApprover_name = "";
            string sIsInvoiceWithPO = "";

            sApproverEmpCode = Convert.ToString(dtApproverEmailIds.Tables[0].Rows[0]["APPR_Emp_Code"]).Trim();
            sApprover_name = Convert.ToString(dtApproverEmailIds.Tables[0].Rows[0]["ApproverName"]).Trim();
            sApproverEmail = Convert.ToString(dtApproverEmailIds.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
            iAppr_id = Convert.ToInt32(dtApproverEmailIds.Tables[0].Rows[0]["APPR_ID"]);
            sIsInvoiceWithPO = Convert.ToString(dtApproverEmailIds.Tables[0].Rows[0]["IsInvoiceWithPO"]).Trim();


            double dMaxReqId = 0;

            double dProject_Dept_id = 0;
            Int32 iVendorId = 0;
            if (Convert.ToString(hdnProject_Dept_Id.Value).Trim() != "")
            {
                dProject_Dept_id = Convert.ToDouble(hdnProject_Dept_Id.Value);
            }
            if (Convert.ToString(hdnVendorId.Value).Trim() != "")
            {
                iVendorId = Convert.ToInt16(hdnVendorId.Value);
            }
            DataSet dtMaxreqID = new DataSet();
            dtMaxreqID = spm.InsertUpdate_Invoice(txtEmpCode.Text, sInvoiceType, Convert.ToDouble(hdnMilestoneID.Value), Convert.ToString(txtInvoiceNo.Text).Trim(), strInvoiceDate, Convert.ToDecimal(txtAmtWithOutTax.Text), rCGST, rSGST, rIGST, rCGST_Per, rSGST_Per, rIGST_Per, Convert.ToDecimal(txtAmtWithTax_Invoice.Text), dMilestoneBalAmt, 1, 1, blnIswithPO, dProject_Dept_id, iVendorId, Convert.ToString(hdnInvoiceId.Value), Convert.ToString(txtVoucherNo.Text), strSupplierInvoiceDate, sPotypeid, Convert.ToString(txtInvocieRemarks.Text), 0, false, Convert.ToString(txtinvoice_description.Text));

            dMaxReqId = Convert.ToDouble(dtMaxreqID.Tables[0].Rows[0]["MaxReqID"]);


            spm.InsertInvoice_ApproverDetails("Insertinvoice_Approver", dMaxReqId, sApproverEmpCode, iAppr_id, "Pending", "", "", "", "");


            #region insert or upload multiple files
            if (InvoiceUploadfile.HasFile)
            {
                string filename = InvoiceUploadfile.FileName;
                string MilestoneFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBInvoicefiles"]).Trim() + "/");
                bool folderExists = Directory.Exists(MilestoneFilePath);
                if (!folderExists)
                {
                    Directory.CreateDirectory(MilestoneFilePath);
                }
                String InputFile = System.IO.Path.GetExtension(InvoiceUploadfile.FileName);
                filename = txtEmpCode.Text + "_" + Convert.ToString(dMaxReqId).Trim() + "_Invoice_PO" + InputFile;
                InvoiceUploadfile.SaveAs(Path.Combine(MilestoneFilePath, filename));
                spm.InsertInvoiceUploaded_Files(dMaxReqId, "INSERT_InvociePOFile", Convert.ToString(filename).Trim(), "INSERT_InvociePOFile", 0);
            }

            if (ploadexpfile.HasFile)
            {
                string filename = ploadexpfile.FileName;

                if (Convert.ToString(filename).Trim() != "")
                {

                    string InvoiceFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBInvoicefiles"]).Trim() + "/");
                    bool folderExists = Directory.Exists(InvoiceFilePath);
                    if (!folderExists)
                    {
                        Directory.CreateDirectory(InvoiceFilePath);
                    }


                    Int32 t_cnt = 1;


                    if (ploadexpfile.HasFiles)
                    {
                        foreach (HttpPostedFile uploadedFile in ploadexpfile.PostedFiles)
                        {
                            string strfileName = "";
                            string fileName = ReplaceInvalidChars(Path.GetFileName(uploadedFile.FileName));
                            if (uploadedFile.ContentLength > 0)
                            {

                                String InputFile = System.IO.Path.GetExtension(uploadedFile.FileName);
                                strfileName = txtEmpCode.Text + "_" + Convert.ToString(dMaxReqId).Trim() + "_Invoice" + (t_cnt).ToString() + InputFile;
                                filename = strfileName;
                                uploadedFile.SaveAs(Path.Combine(InvoiceFilePath, strfileName));


                                spm.InsertInvoiceUploaded_Files(dMaxReqId, "INSERT", Convert.ToString(strfileName).Trim(), "Invoice", t_cnt);

                                t_cnt = t_cnt + 1;
                            }



                        }
                    }

                    #region not requied
                    /*HttpFileCollection fileCollection = Request.Files;
                    for (int i = 0; i < fileCollection.Count; i++)
                    {
                        string strfileName = "";

                        HttpPostedFile uploadfileName = fileCollection[i];
                        if (Convert.ToString(ploadexpfile.FileName).Trim() != Convert.ToString(uploadfileName.FileName).Trim())
                        {
                            string fileName = ReplaceInvalidChars(Path.GetFileName(uploadfileName.FileName));

                            if (uploadfileName.ContentLength > 0)
                            {

                                String InputFile = System.IO.Path.GetExtension(uploadfileName.FileName);
                                strfileName = txtEmpCode.Text + "_" + Convert.ToString(dMaxReqId).Trim() + "_Invoice" + (t_cnt + 1).ToString() + InputFile;
                                filename = strfileName;
                                uploadfileName.SaveAs(Path.Combine(InvoiceFilePath, strfileName));


                                spm.InsertInvoiceUploaded_Files(dMaxReqId, "INSERT", Convert.ToString(strfileName).Trim(), "Invoice", i + 1);

                                t_cnt = t_cnt + 1;
                            }
                        }
                        
                    }*/
                    #endregion
                }


            }
            #endregion


            #region Send Email to 1st Approver
             
            if (chkIsInvoiceWithoutPO.Checked)
                ichkWithoutPo = 1;

            DataSet dsMilestone = new DataSet();
            dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(dMaxReqId), txtEmpCode.Text, hdnProject_Dept_Name.Value, Convert.ToString(sPotypeid), ichkWithoutPo);

            StringBuilder strbuild_Approvers = new StringBuilder();
            strbuild_Approvers.Append("<table cellpadding='5' cellspacing='0' style='font-size:11pt;font-family:Arial;font-style:Regular;width:100%;'>");
            strbuild_Approvers.Append("<tr><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Approver Name</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Status</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Approved On</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Approver Remarks</th></tr>");
            for (Int32 irow = 0; irow < dsMilestone.Tables[0].Rows.Count; irow++)
            {
                strbuild_Approvers.Append("<tr><td style='width:40%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["ApproverName"]).Trim() + " </td>");
                strbuild_Approvers.Append("<td style='width:10%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() + "</td>");
                strbuild_Approvers.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["approved_on"]).Trim() + "</td>");
                strbuild_Approvers.Append("<td style='width:35%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Remarks"]).Trim() + "</td></tr>");
            }

            strbuild_Approvers.Append("</table>");

            string strSubject = "";
            if (chkIsInvoiceWithoutPO.Checked)
                strSubject = "OneHR: Request for - Invoice Approval - " + Convert.ToString(txtInvoiceNo.Text).Trim();
            else
                strSubject = "OneHR: Request for - Invoice Approval - " + Convert.ToString(txtInvoiceNo.Text).Trim() + " against " + Convert.ToString(lstTripType.SelectedItem.Text).Trim();

            string strInvoiceURL = "";
            strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["approverLink_VSCB"]).Trim() + "?invid=" + dMaxReqId).Trim();
            StringBuilder strbuild = new StringBuilder();

            strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'>");
            strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td colspan='2'> " + hflEmpName.Value + " has created an invoice with the following details and requested your approval.</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");

            strbuild.Append("<tr><td>Invoice No :-</td><td>" + Convert.ToString(txtInvoiceNo.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Invoice Date :-</td><td>" + Convert.ToString(txtInvoiceDate.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Invoice Amount :-</td><td>" + Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() + "</td></tr>");

            if (chkIsInvoiceWithoutPO.Checked == true)
            {
                //strbuild.Append("<tr><td>Department :-</td><td>" + Convert.ToString(lstProjectDept.SelectedItem.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Vendor Name :-</td><td>" + Convert.ToString(lstVendors.SelectedItem.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(lstProjectDept.SelectedItem.Text).Trim() + "</td></tr>");
            }

            if (chkIsInvoiceWithoutPO.Checked == false)
            {
                strbuild.Append("<tr><td style='height:20px'></td></tr>");
                strbuild.Append("<tr><td>Milestone Particular :-</td><td>" + Convert.ToString(txtMilestoneName_Invoice.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Milestone Amount :-</td><td>" + Convert.ToString(txtMilestoneAmt_Invoice.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Milestone Balance Amount :-</td><td>" + Convert.ToString(txtMilestoneBalanceAmt_Invoice.Text).Trim() + "</td></tr>");

                strbuild.Append("<tr><td style='height:20px'></td></tr>");
                strbuild.Append("<tr><td>PO/WO No :-</td><td>" + Convert.ToString(lstTripType.SelectedItem.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>PO/WO Tyoe :-</td><td>" + Convert.ToString(txtPOTitle.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>PO/WO Date :-</td><td>" + Convert.ToString(txtFromdate.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Vendor Name :-</td><td>" + Convert.ToString(txtVendor.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(txtCostCenter.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>PO/WO Amount (With GST) :-</td><td>" + Convert.ToString(txtPOWOAmt.Text).Trim() + "</td></tr>");

                //strbuild.Append("<tr><td>Department :-</td><td>" + Convert.ToString(txtProject.Text).Trim() + "</td></tr>");



            }


            strbuild.Append("<tr><td style='height:40px'></td></tr>");
            strbuild.Append("<tr><td style='height:20px'><a href='" + strInvoiceURL + "'> Please Click here for your action </a></td></tr>");

            strbuild.Append("</table>  <br /><br />");

            strbuild.Append(strbuild_Approvers);

            spm.sendMail_VSCB(sApproverEmail, strSubject, Convert.ToString(strbuild).Trim(), "", "");


            #endregion



            Response.Redirect("VSCB_MyInvoice.aspx");
        }
        else
        {
            lblmessage.Text = "Invoice creation failed. Please contact to admin.";
            return;
        }
    }

    private void Create_Invoice_Without_POWO()
    {
        Decimal dMilestoneBalAmt = 0;
        #region validations for Invoice Without PO
        if (Convert.ToString(lstProjectDept.SelectedValue).Trim() == "0")
        {
            lblmessage.Text = "Please Select Project/Department Name.";
            return;
        }

        if (Convert.ToString(lstVendors.SelectedValue).Trim() == "0")
        {
            lblmessage.Text = "Please Select Vendor Name.";
            return;
        }

        if (Convert.ToString(txtAmtWithOutTax.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Amount (Without Tax).";
            return;
        }
        if (Convert.ToString(txtInvoiceNo.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Invoice No.";
            return;
        }
        if (Convert.ToString(txtInvoiceDate.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Invoice Date.";
            return;
        }
        if (Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Amount (Without Tax).";
            return;
        }

        if (Convert.ToString(txtAmtWithOutTax.Text).Trim() != "")
        {
            string[] strdate1;
            strdate1 = Convert.ToString(txtAmtWithOutTax.Text).Trim().Split('.');
            if (strdate1.Length > 2)
            {
                txtAmtWithOutTax.Text = "";
                lblmessage.Text = "Please enter correct Amount (Without Tax).";
                return;
            }

            Decimal dfare = 0;
            dfare = Convert.ToDecimal(txtAmtWithOutTax.Text);
            if (dfare == 0)
            {
                txtAmtWithOutTax.Text = "";
                lblmessage.Text = "Please enter correct Amount (Without Tax).";
                return;
            }
        }




        #endregion



        string[] strdate;
        string strInvoiceDate = "";
        string strSupplierInvoiceDate = "";

        #region date formatting
        if (Convert.ToString(txtInvoiceDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtInvoiceDate.Text).Trim().Split('-');
            strInvoiceDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        #endregion

        Decimal rCGST = 0;
        Decimal rSGST = 0;
        Decimal rIGST = 0;

        Decimal rCGST_Per = 0;
        Decimal rSGST_Per = 0;
        Decimal rIGST_Per = 0;

        if (Convert.ToString(txtCGST_Per.Text).Trim() != "")
        {
            rCGST = Math.Round(Convert.ToDecimal(txtCGST_Per.Text) * Convert.ToDecimal(txtAmtWithOutTax.Text) / 100, 2);
            rCGST_Per = Convert.ToDecimal(txtCGST_Per.Text);
        }

        if (Convert.ToString(txtSGST_Per.Text).Trim() != "")
        {
            rSGST = Math.Round(Convert.ToDecimal(txtSGST_Per.Text) * Convert.ToDecimal(txtAmtWithOutTax.Text) / 100, 2);
            rSGST_Per = Convert.ToDecimal(txtSGST_Per.Text);
        }
        if (Convert.ToString(txtIGST_Per.Text).Trim() != "")
        {
            rIGST = Math.Round(Convert.ToDecimal(txtIGST_Per.Text) * Convert.ToDecimal(txtAmtWithOutTax.Text) / 100, 2);
            rIGST_Per = Convert.ToDecimal(txtIGST_Per.Text);
        }

        Boolean blnIswithPO = false;

        double dProject_Dept_id = 0;
        Int32 iVendorId = 0;
        if (Convert.ToString(lstProjectDept.SelectedValue).Trim() != "")
        {
            dProject_Dept_id = Convert.ToDouble(lstProjectDept.SelectedValue);
        }
        if (Convert.ToString(lstVendors.SelectedValue).Trim() != "")
        {
            iVendorId = Convert.ToInt32(lstVendors.SelectedValue);
        }


        txtAmtWithTax_Invoice.Text = Convert.ToString(Math.Round(rCGST + rSGST + rIGST + Convert.ToDecimal(txtAmtWithOutTax.Text), 2)).Trim();


        int ichkWithoutPo = 0;
        if (chkIsInvoiceWithoutPO.Checked)
            ichkWithoutPo = 1;
        DataSet dtApproverEmailIds = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), txtEmpCode.Text, hdnProject_Dept_Name.Value, Convert.ToString(lstPOType.SelectedValue), ichkWithoutPo); //spm.VSCB_GetApproverEmailID("get_Approvers_matrix", Convert.ToString(txtProject.Text).Trim(), 0);

        if (dtApproverEmailIds.Tables[0].Rows.Count > 0)
        {
            string sApproverEmpCode = "";
            int iAppr_id = 0;
            string sApproverEmail = "";
            string sApprover_name = "";
            string sIsInvoiceWithPO = "";

            sApproverEmpCode = Convert.ToString(dtApproverEmailIds.Tables[0].Rows[0]["APPR_Emp_Code"]).Trim();
            sApprover_name = Convert.ToString(dtApproverEmailIds.Tables[0].Rows[0]["ApproverName"]).Trim();
            sApproverEmail = Convert.ToString(dtApproverEmailIds.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
            iAppr_id = Convert.ToInt32(dtApproverEmailIds.Tables[0].Rows[0]["APPR_ID"]);
            sIsInvoiceWithPO = Convert.ToString(dtApproverEmailIds.Tables[0].Rows[0]["IsInvoiceWithPO"]).Trim();

            double dMaxReqId = 0;

            DataSet dtMaxreqID = new DataSet();
            dtMaxreqID = spm.InsertUpdate_Invoice(txtEmpCode.Text, "Insertinvoice", 0, Convert.ToString(txtInvoiceNo.Text).Trim(), strInvoiceDate, Convert.ToDecimal(txtAmtWithOutTax.Text), rCGST, rSGST, rIGST, rCGST_Per, rSGST_Per, rIGST_Per, Convert.ToDecimal(txtAmtWithTax_Invoice.Text), dMilestoneBalAmt, 1, 1, blnIswithPO, dProject_Dept_id, iVendorId, "", "", "", "", Convert.ToString(txtInvocieRemarks.Text), 0, false, "");

            dMaxReqId = Convert.ToDouble(dtMaxreqID.Tables[0].Rows[0]["MaxReqID"]);


            spm.InsertInvoice_ApproverDetails("Insertinvoice_Approver", dMaxReqId, sApproverEmpCode, iAppr_id, "Pending", "", "", "", "");


            #region insert or upload multiple files
            if (ploadexpfile.HasFile)
            {
                string filename = ploadexpfile.FileName;

                if (Convert.ToString(filename).Trim() != "")
                {

                    string InvoiceFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBInvoicefiles"]).Trim() + "/");
                    bool folderExists = Directory.Exists(InvoiceFilePath);
                    if (!folderExists)
                    {
                        Directory.CreateDirectory(InvoiceFilePath);
                    }


                    Int32 t_cnt = 0;


                    HttpFileCollection fileCollection = Request.Files;
                    for (int i = 0; i < fileCollection.Count; i++)
                    {
                        string strfileName = "";

                        HttpPostedFile uploadfileName = fileCollection[i];
                        string fileName = ReplaceInvalidChars(Path.GetFileName(uploadfileName.FileName));
                        if (uploadfileName.ContentLength > 0)
                        {
                            String InputFile = System.IO.Path.GetExtension(uploadfileName.FileName);
                            strfileName = txtEmpCode.Text + "_" + Convert.ToString(dMaxReqId).Trim() + "_Invoice" + (t_cnt + 1).ToString() + InputFile;
                            filename = strfileName;
                            uploadfileName.SaveAs(Path.Combine(InvoiceFilePath, strfileName));


                            spm.InsertInvoiceUploaded_Files(dMaxReqId, "INSERT", Convert.ToString(strfileName).Trim(), "Invoice", i + 1);

                            t_cnt = t_cnt + 1;
                        }
                    }

                }


            }
            #endregion

            #region Send Email to 1st Approver
            // spm.send_mailto_First_Approver_Invoice(hflEmpName.Value, hflEmailAddress.Value, sApproverEmail, "Request for Approve Invoice", "", "", "");
            #endregion



            Response.Redirect("VSCB_MyInvoice.aspx");
        }
        else
        {
            lblmessage.Text = "Invoice creation failed. Please contact to admin.";
            return;
        }
    }

    protected void CheckIs_Invoice_Wthout_POWO()
    {
        if (chkIsInvoiceWithoutPO.Checked)
        {
            editform.Visible = false;
            idMilestone.Visible = false;
            idMilestoneAmt.Visible = false;
            idBalanceAmt.Visible = false;
            idDirectTaxAmt.Visible = false;
            idMilestonePaidAmt.Visible = false;
            idMilestoneBlnk_1.Visible = false;

            dgTravelRequest.DataSource = null;
            dgTravelRequest.DataBind();
            dgTravelRequest.Visible = false;

            idWithoutInv_ProjectDept.Visible = true;
            idWithoutInv_Vendor.Visible = true;
            idWithoutInv_Blank.Visible = true;

            //idWithoutInv_Expenses_1.Visible = true;
            //idWithoutInv_Expenses_2.Visible = true;
            //idWithoutInv_Expenses_3.Visible = true;

            idWithoutInv_bank_1.Visible = true;
            idWithoutInv_bank_2.Visible = true;
            idWithoutInv_bank_3.Visible = true;



            idWithoutInv_InvoiceRemarks_1.Visible = true;
            idWithoutInv_InvoiceRemarks_2.Visible = true;
            idWithoutInv_InvoiceRemarks_3.Visible = true;

            spMilestones.Visible = false;



            txtInvoiceNo.Text = "";
            txtInvoiceDate.Text = "";
            txtAmtWithOutTax.Text = "";
            txtAmtWithTax_Invoice.Text = "";

            txtCGST_Per.Text = "";
            txtSGST_Per.Text = "";
            txtIGST_Per.Text = "";

            txtPoPaidAmt_WithOutDT.Text = "";

            gvMngPaymentList_Batch.DataSource = null;
            gvMngPaymentList_Batch.DataBind();
            spInvocies.Visible = false;
            hdnTotalInvoiceAmt.Value = "0";

            GetPODetails_List();
            txtCGST_Per.Enabled = true;
            txtSGST_Per.Enabled = true;
            txtIGST_Per.Enabled = true;

            liCGST_Per.Visible = true;
            liSGST_Per.Visible = true;
            liIGST_Per.Visible = true;

            //Add costcenter button and grid not required
            //idliCostCenterList.Visible = false;
            //trvldeatils_btnSave.Visible = false;
            //lblMilestoneCostCenter_Err.Visible = false;

            txtAmtWithOutTax.Enabled = true;
            txtAmtWithOutTax.ReadOnly = false;
            liInvoiceDescription_PO.Visible = false;

        }
        else
        {
            txtAmtWithOutTax.Enabled = false;
            txtAmtWithOutTax.ReadOnly = true;

            editform.Visible = true;
            idMilestone.Visible = true;
            idMilestoneAmt.Visible = true;
            idBalanceAmt.Visible = true;
            idDirectTaxAmt.Visible = true;
            idMilestoneBlnk_1.Visible = true;

            idMilestonePaidAmt.Visible = true;

            dgTravelRequest.DataSource = null;
            dgTravelRequest.DataBind();
            dgTravelRequest.Visible = true;


            idWithoutInv_ProjectDept.Visible = false;
            idWithoutInv_Vendor.Visible = false;
            idWithoutInv_Blank.Visible = false;
            //idWithoutInv_Expenses_1.Visible = false;
            //idWithoutInv_Expenses_2.Visible = false;
            //idWithoutInv_Expenses_3.Visible = false;

            idWithoutInv_bank_1.Visible = false;
            idWithoutInv_bank_2.Visible = false;
            idWithoutInv_bank_3.Visible = false;


            idWithoutInv_InvoiceRemarks_1.Visible = false;
            idWithoutInv_InvoiceRemarks_2.Visible = false;
            idWithoutInv_InvoiceRemarks_3.Visible = false;

            liCGST_Per.Visible = false;
            liSGST_Per.Visible = false;
            liIGST_Per.Visible = false;

            //Add costcenter button and grid not required
            idliCostCenterList.Visible = true;
            lblMilestoneCostCenter_Err.Visible = true;

            hdnTotalInvoiceAmt.Value = "0";
            liInvoiceDescription_PO.Visible = true;
            clear_POWO_Cntrls();


        }

        //txtCGST_Per.ReadOnly = true;
        //txtSGST_Per.ReadOnly = true;
        //txtIGST_Per.ReadOnly = true;

        //txtCGST_Per.Enabled = false;
        //txtSGST_Per.Enabled = false;
        //txtIGST_Per.Enabled = false;


    }

    protected string GetInvoiceApprove_RejectList(string invoiceid, string EmpCode)
    {
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataSet dsMilestone = new DataSet();
        int ichkWithoutPo = 0;
        if (chkIsInvoiceWithoutPO.Checked)
            ichkWithoutPo = 1;
        dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), EmpCode, hdnProject_Dept_Name.Value, Convert.ToString(lstPOType.SelectedValue), ichkWithoutPo);
        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
            sbapp.Append("<table>");
            for (int i = 0; i < dsMilestone.Tables[0].Rows.Count; i++)
            {
                sbapp.Append("<tr>");
                sbapp.Append("<td>" + dsMilestone.Tables[0].Rows[i]["names"] + "</td>");
                sbapp.Append("</tr>");
            }
            sbapp.Append("</table>");
        }
        return Convert.ToString(sbapp);
    }

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
    public void clear_POWO_Cntrls()
    {
        txtFromdate.Text = "";
        txtPOTitle.Text = "";
        txtPOStatus.Text = "";

        txtVendor.Text = "";
        txtGSTIN_No.Text = "";
        txtPOWOAmt.Text = "";
        txtBasePOWOWAmt.Text = "";
        txtProject.Text = "";
        txtDiretTaxAmt.Text = "";
        lstPOType.SelectedValue = "0";
        txtPaidAmt.Text = "";
        txtCostCenter.Text = "";
        txtBalanceAmt.Text = "";
        lnkfile_PO.Text = "";

        lnkfile_PO.Visible = false;
        //Add costcenter button and grid not required
        //trvldeatils_btnSave.Visible = false;
        dgCostcenterList.DataSource = null;
        dgCostcenterList.DataBind();

        txtCurrency.Text = "";
        txtPOWOSettelmentAmt.Text = "";
        txtinvoice_description.Text = "";
    }
    public void clear_Milestone_Cntrls()
    {
        hdnSrno.Value = "";
        txtMilestoneName_Invoice.Text = "";
        txtMilestoneAmt_Invoice.Text = "";
        txtMilestoneBalanceAmt_Invoice.Text = "";
        txtInvoiceNo.Text = "";
        txtAmtWithOutTax.Text = "";
        txtCGST_Per.Text = "";
        txtSGST_Per.Text = "";
        txtIGST_Per.Text = "";
        txtAmtWithTax_Invoice.Text = "";
        hdnProject_Dept_Name.Value = "";
        get_Approver_List();

    }


    public Boolean checkVenddorEmailIdExists()
    {
        Boolean blncheck = false;
        DataSet dsVendorEdit = new DataSet();
        SqlParameter[] sparss = new SqlParameter[2];

        sparss[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        sparss[0].Value = "getVendordetails";

        sparss[1] = new SqlParameter("@VendorName", SqlDbType.VarChar);
        if (chkIsInvoiceWithoutPO.Checked == false)
            sparss[1].Value = Convert.ToString(txtVendor.Text).Trim();
        else
            sparss[1].Value = Convert.ToString(lstVendors.SelectedItem.Text).Trim();

        dsVendorEdit = spm.getDatasetList(sparss, "SP_VSCB_GETALL_DETAILS");
        if (dsVendorEdit.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToString(dsVendorEdit.Tables[0].Rows[0]["VendorEmailAddress"]).Trim() != "")
            {
                blncheck = true;
            }
        }
        return blncheck;

    }

    public void GetExpeses_Details()
    {
        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Expenses_Details_List";

        spars[1] = new SqlParameter("@VendorId", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(lstExpenses.SelectedValue);


        dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        lstExpense_details.DataSource = null;
        lstExpense_details.DataBind();

        if (dsProjectsVendors.Tables[0].Rows.Count > 0)
        {
            lstExpense_details.DataSource = dsProjectsVendors.Tables[0];
            lstExpense_details.DataTextField = "Project_Name";
            lstExpense_details.DataValueField = "Dept_ID";
            lstExpense_details.DataBind();
            lstExpense_details.Items.Insert(0, new ListItem("Select Expese Details", "0"));
        }
    }

    public void GetPODetails_List()
    {

        DataSet dtPOWODetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Approved_POWOListInvoice"; // "get_Approved_POWOList_CreateInvoice";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = txtEmpCode.Text;


        dtPOWODetails = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");


        if (dtPOWODetails.Tables[0].Rows.Count > 0)
        {
            lstTripType.DataSource = dtPOWODetails;
            lstTripType.DataTextField = "PONumber";
            lstTripType.DataValueField = "POID";
            lstTripType.DataBind();
        }
        lstTripType.Items.Insert(0, new ListItem("Select PO/WO Number", "0"));

    }

    public Int32 GetCostCenter_Id(string stallycode)
    {
        Int32 id = 0;
        DataSet dtPOWODetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Selected_TallyCode_id";

        spars[1] = new SqlParameter("@Project_Dept_Name", SqlDbType.VarChar);
        spars[1].Value = stallycode;


        dtPOWODetails = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");


        if (dtPOWODetails.Tables[0].Rows.Count > 0)
        {
            id = Convert.ToInt32(dtPOWODetails.Tables[0].Rows[0]["Dept_ID"]);
        }
        return id;

    }

    public void Check_CostCenterApprovalMatrix(string stallycode)
    {
        DataSet dtPOWODetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Selected_TallyCode_Approval_Matrix";

        spars[1] = new SqlParameter("@Project_Dept_Name", SqlDbType.VarChar);
        spars[1].Value = stallycode;

        spars[2] = new SqlParameter("@POTypeID", SqlDbType.VarChar);
        if (Convert.ToString(hdnPOTypeId.Value).Trim() != "")
            spars[2].Value = Convert.ToString(hdnPOTypeId.Value);
        else
            spars[2].Value = DBNull.Value;


        dtPOWODetails = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        trvl_btnSave.Visible = true;
        if (dtPOWODetails.Tables[0].Rows.Count <= 0)
        {
            trvl_btnSave.Visible = false;
            lblmessage.Text = "Approval Matrix not set. Please contact to Admin";
        }


    }

    public void GetCostCenterList_ForChange()
    {

        DataSet dtPOWODetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_CostCenter_list_forChangeinInvoice"; // "get_Approved_POWOList_CreateInvoice";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = txtEmpCode.Text;


        dtPOWODetails = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");


        if (dtPOWODetails.Tables[0].Rows.Count > 0)
        {
            lstforChangeCostCenter.DataSource = dtPOWODetails;
            lstforChangeCostCenter.DataTextField = "Tallycode";
            lstforChangeCostCenter.DataValueField = "Tallycode";
            lstforChangeCostCenter.DataBind();

        }
        lstforChangeCostCenter.Items.Insert(0, new ListItem("Select Cost Center", "0"));

    }
    public void GetPODetails_List_Invoice_View()
    {

        DataSet dtPOWODetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Approved_POWOList";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = txtEmpCode.Text;


        dtPOWODetails = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        lstTripType.DataSource = null;
        lstTripType.DataBind();
        if (dtPOWODetails.Tables[0].Rows.Count > 0)
        {
            lstTripType.DataSource = dtPOWODetails;
            lstTripType.DataTextField = "PONumber";
            lstTripType.DataValueField = "POID";
            lstTripType.DataBind();
        }
        lstTripType.Items.Insert(0, new ListItem("Select PO/WO Number", "0"));

    }

    public void GetPOTypes()
    {
        DataTable dtPOWODetails = new DataTable();
        dtPOWODetails = spm.getPOTypes();
        if (dtPOWODetails.Rows.Count > 0)
        {
            lstPOType.DataSource = dtPOWODetails;
            lstPOType.DataTextField = "POType";
            lstPOType.DataValueField = "POTypeID";
            lstPOType.DataBind();
            lstPOType.Items.Insert(0, new ListItem("Select PO/WO Type", "0"));

        }
    }

    public void get_ProjectDept_Vendor_List()
    {

        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Project_Dept_Vendor_List";

        dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        if (dsProjectsVendors.Tables[0].Rows.Count > 0)
        {
            lstProjectDept.DataSource = dsProjectsVendors.Tables[0];
            lstProjectDept.DataTextField = "Project_Name";
            lstProjectDept.DataValueField = "Dept_ID";
            lstProjectDept.DataBind();
            lstProjectDept.Items.Insert(0, new ListItem("Select Project Name", "0"));
        }
        if (dsProjectsVendors.Tables[1].Rows.Count > 0)
        {
            lstVendors.DataSource = dsProjectsVendors.Tables[1];
            lstVendors.DataTextField = "Name";
            lstVendors.DataValueField = "VendorID";
            lstVendors.DataBind();
            lstVendors.Items.Insert(0, new ListItem("Select Vendor Name", "0"));
        }

        if (dsProjectsVendors.Tables[3].Rows.Count > 0)
        {
            lstExpenses.DataSource = dsProjectsVendors.Tables[3];
            lstExpenses.DataTextField = "exp_name";
            lstExpenses.DataValueField = "exp_id";
            lstExpenses.DataBind();
            lstExpenses.Items.Insert(0, new ListItem("Select Expense", "0"));
        }

        if (dsProjectsVendors.Tables[4].Rows.Count > 0)
        {
            lstDisplayPOTypes.DataSource = dsProjectsVendors.Tables[4];
            lstDisplayPOTypes.DataTextField = "Display_Name";
            lstDisplayPOTypes.DataValueField = "POTypeID";
            lstDisplayPOTypes.DataBind();
            lstDisplayPOTypes.Items.Insert(0, new ListItem("Select Invoice Type", "0"));
        }


    }


    public void get_MilestoneList_from_Invoice_Milestone_toTemp()
    {

        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Invoice_MilestoneList_toTemp_edit";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

        spars[2] = new SqlParameter("@InvoiceId", SqlDbType.VarChar);
        if (Convert.ToString(hdnInvoiceId.Value).Trim() != "")
            spars[2].Value = Convert.ToDouble(hdnInvoiceId.Value);
        else
            spars[2].Value = DBNull.Value;
        dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");



    }
    public void get_PWODetails_MilestonesList()
    {

        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.getMilestone_list_CreateInvoice(txtEmpCode.Text, Convert.ToDouble(lstTripType.SelectedValue));

        dgTravelRequest.DataSource = null;
        dgTravelRequest.DataBind();

        gvMngPaymentList_Batch.DataSource = null;
        gvMngPaymentList_Batch.DataBind();


        spInvocies.Visible = false;
        spMilestones.Visible = false;

        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
            Boolean isShortClosed = false;
            spnPOWODShortClosedAmt.Visible = false;
            txtPOWOShortClosedAmt.Visible = false;

            hdnPOWOID.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POID"]).Trim();
            txtFromdate.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PODate"]);
            txtProject.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Project_Name"]).Trim();
            hdnProject_Dept_Name.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Project_Name"]).Trim();
            lstforChangeCostCenter.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Project_Name"]).Trim();
            txtPOStatus.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PyamentStatus"]).Trim();
            hdnPOTypeId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTypeID"]).Trim();
            txtPOTitle.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTitle"]).Trim();
            txtPOTitle.ToolTip = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTitle"]).Trim();
            txtVendor.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Name"]).Trim();
            txtGSTIN_No.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["GSTIN_NO"]).Trim();
            txtBasePOWOWAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POWO_T_BaseAmt"]).Trim();
            txtPOWOAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POWOAmt"]).Trim();
            txtPoPaidAmt_WithOutDT.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POPiadAmount_withoutDT"]).Trim();
            lstPOType.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTypeID"]).Trim();
            txtCurrency.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CurName"]).Trim();
            txtPOWOSettelmentAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_settlementAmt"]).Trim();

            txtPOtype.Text = Convert.ToString(lstPOType.SelectedItem.Text).Trim();
            txtPaidAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Paid_Amt"]).Trim();
            txtBalanceAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Bal_Amt"]).Trim();
            txtDiretTaxAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["DirectTaxCollection_Amt"]).Trim();
            spnPOWOSignCopy.Visible = false;
            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["powo_signCopy"]).Trim() != "")
            {
                lnkfile_PO.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["powo_signCopy"]).Trim();
                hdnSingPOCopyFileName.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["powo_signCopy"]).Trim();
                lnkfile_PO.Visible = true;
                spnPOWOSignCopy.Visible = true;
            }


            txtCostCenter.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CostCentre"]).Trim();

            hdnCompCode.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Project_Name"]).Trim();
            hdnVendorId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["VendorID"]).Trim();
            hdnProject_Dept_Id.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Prj_Dept_id"]).Trim();
            lnkfile_PO.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["powo_signCopy"]).Trim();

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["IsShortClose"]).Trim() != "")
                isShortClosed = Convert.ToBoolean(dsMilestone.Tables[0].Rows[0]["IsShortClose"]);


            if (isShortClosed == true)
            {
                spnPOWODShortClosedAmt.Visible = true;
                txtPOWOShortClosedAmt.Visible = true;
                txtPOWOShortClosedAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["ShortClose_Amt"]).Trim();
            }
            lstPOType.Enabled = false;
            gvShortCloseMilestone.Visible = false;
            if (dsMilestone.Tables[1].Rows.Count > 0)
            {
                dgTravelRequest.DataSource = dsMilestone.Tables[1];
                dgTravelRequest.DataBind();
                spMilestones.Visible = true;
                gvShortCloseMilestone.Visible = false;
                dgTravelRequest.Visible = true;

                if (isShortClosed == true)
                {
                    gvShortCloseMilestone.DataSource = dsMilestone.Tables[1];
                    gvShortCloseMilestone.DataBind();
                    gvShortCloseMilestone.Visible = true;
                    dgTravelRequest.Visible = false;
                }


            }

            if (dsMilestone.Tables[2].Rows.Count > 0)
            {
                spInvocies.Visible = true;
                gvMngPaymentList_Batch.DataSource = dsMilestone.Tables[2];
                gvMngPaymentList_Batch.DataBind();

            }

            #region Add costcenter button and grid not required
            /*
            dgCostcenterList.DataSource = null;
            dgCostcenterList.DataBind();
            if (dsMilestone.Tables[3].Rows.Count > 0)
            { 
                    dgCostcenterList.DataSource = dsMilestone.Tables[3];
                    dgCostcenterList.DataBind();
                Int32 irowcnt = 1;
                // 
                //trvldeatils_btnSave.Visible = true;
                foreach (GridViewRow row in dgCostcenterList.Rows)
                {
                    if (irowcnt < 6)
                    {
                        row.Visible = true;
                        hdnMilestoneRowCnt.Value = Convert.ToString(irowcnt);
                    }
                    else
                    {
                        row.Visible = false;
                    }
                    
                    irowcnt += 1;
                }
            }
            */
            #endregion

        }

        get_Approver_List();
    }

    public void get_InvoiceDetails_MilestonesList_Update()
    {

        //get_ProjectDept_Vendor_List();
        DataSet dsMilestone = new DataSet();

        dsMilestone = spm.getInvoicedetails_edit_View(txtEmpCode.Text, Convert.ToDouble(hdnInvoiceId.Value));
        spMilestones.Visible = false;
        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
            chkIsInvoiceWithoutPO.Checked = false;
            if (Convert.ToBoolean(dsMilestone.Tables[0].Rows[0]["IsInvoiceWithPO"]) == false)
            {
                chkIsInvoiceWithoutPO.Checked = true;
                lstProjectDept.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Prj_Dept_id"]).Trim();
                lstVendors.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["VendorId"]).Trim();
                lstDisplayPOTypes.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTypeID"]).Trim();
                lstDisplayPOTypes.Enabled = false;
                txtInvocieRemarks.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Invoice_Remarks"]).Trim();
                //txtinvoice_description.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["invoice_description"]).Trim();
            }
            CheckIs_Invoice_Wthout_POWO();
            chkIsInvoiceWithoutPO.Enabled = false;
            if (chkIsInvoiceWithoutPO.Checked == true)
            {
                lstProjectDept.Enabled = false;
                lstVendors.Enabled = false;
            }
            else
            {
                txtCostCenter.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Costcenter"]).Trim();
                lstforChangeCostCenter.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Costcenter"]).Trim();

                if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["powo_signCopy"]).Trim() != "")
                {
                    lnkfile_PO.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["powo_signCopy"]).Trim();
                    lnkfile_PO.Visible = true;
                    spnPOWOSignCopy.Visible = true;
                    txtAmtWithOutTax.Enabled = true;
                }

                txtCurrency.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CurName"]);
                txtPOWOSettelmentAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_SettelmentAmt"]).Trim();

                #region bind Cost center grid  and Add costcenter button and grid not required

                /*
                dgCostcenterList.DataSource = null;
                dgCostcenterList.DataBind();
                if (dsMilestone.Tables[5].Rows.Count > 0)
                {
                    dgCostcenterList.DataSource = dsMilestone.Tables[5];
                    dgCostcenterList.DataBind();
                    Int32 irowcnt = 0;
                    
                    //Add costcenter button and grid not required
                    //trvldeatils_btnSave.Visible = true;
                    //idliCostCenterList.Visible = true;

                    hdnMilestoneRowCnt.Value = Convert.ToString(dsMilestone.Tables[6].Rows.Count); 
                
                    foreach (GridViewRow row in dgCostcenterList.Rows)
                    {
                        DropDownList lstMilestoneAmtCostCenter = (DropDownList)row.FindControl("lstMilestoneAmtCostCenter");
                        TextBox txtCostCenterAmt = (TextBox)row.FindControl("txtCostCenterAmt");

                        if( irowcnt < Convert.ToInt32(hdnMilestoneRowCnt.Value))
                        {
                            lstMilestoneAmtCostCenter.SelectedValue = Convert.ToString(dsMilestone.Tables[6].Rows[irowcnt]["Dept_ID"]).Trim();
                            txtCostCenterAmt.Text = Convert.ToString(dsMilestone.Tables[6].Rows[irowcnt]["CostCenter_Amt"]).Trim();
                            row.Visible = true;                           
                        }
                        else
                        {
                            row.Visible = false;
                        }
                        irowcnt += 1;

                    }
                }
                */
                #endregion

            }
            hdnProject_Dept_Id.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Prj_Dept_id"]).Trim();
            hdnVendorId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["VendorId"]).Trim();
            hdnPOWOID.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POID"]).Trim();
            hdnMilestoneID.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MstoneID"]).Trim();
            txtFromdate.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PODate"]);
            txtProject.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Project_Name"]).Trim();
            hdnProject_Dept_Name.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Project_Name"]).Trim();
            txtPOStatus.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POWOStatus"]).Trim();
            hdnPOTypeId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTypeID"]).Trim();
            txtPOTitle.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTitle"]).Trim();
            txtPOTitle.ToolTip = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTitle"]).Trim();
            txtVendor.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["vendorname"]).Trim();
            txtGSTIN_No.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["GSTIN_NO"]).Trim();
            txtBasePOWOWAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POWO_T_BaseAmt"]).Trim();
            txtPOWOAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POWOAmt"]).Trim();
            lstPOType.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTypeID"]).Trim();
            txtPOtype.Text = Convert.ToString(lstPOType.SelectedItem.Text).Trim();
            lstTripType.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POID"]).Trim();
            txtPaidAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Paid_Amt"]).Trim();
            txtPoPaidAmt_WithOutDT.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POPiadAmount_withoutDT"]).Trim();
            txtBalanceAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Bal_Amt"]).Trim();
            txtDiretTaxAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["DirectTaxCollection_Amt"]).Trim();
            txtVoucherNo.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["VoucherNo"]).Trim();
            txtSupplierInvoiceDt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Supplier_Invoice_Date"]).Trim();

            hdnCompCode.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Comp_Code"]).Trim();
            hdnVendorId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["VendorID"]).Trim();
            hdnInvoiceApprStatusId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Status_id"]).Trim();

            //txtMilestoneName_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MilestoneName"]).Trim();
            //txtMilestoneAmt_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MilestoneAmt"]).Trim();
            //txtMilestoneBalanceAmt_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["BalanceMilestoneAmt"]).Trim();
            //txtMilestoneDirectTaxAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Collect_TDS_Amt"]).Trim();
            //txtMilestonePaidAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MilestonePaidAmt"]).Trim();


            txtInvoiceNo.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["InvoiceNo"]).Trim();
            txtInvoiceDate.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["InvoiceDate"]).Trim();

            txtAmtWithOutTax.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["AmtWithoutTax"]).Trim();
            txtAmtWithTax_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["AmtWithTax"]).Trim();

            txtCGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Amt"]).Trim();
            txtSGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Amt"]).Trim();
            txtIGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Amt"]).Trim();
            txtinvoice_description.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["invoices_description"]).Trim();
            if (chkIsInvoiceWithoutPO.Checked == true)
            {

                txtCGST_Per.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Per"]).Trim();
                txtSGST_Per.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Per"]).Trim();
                txtIGST_Per.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Per"]).Trim();
            }

            //if (!String.IsNullOrEmpty(dsMilestone.Tables[0].Rows[0]["IsInvoiceWithPO"].ToString()))

            //if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Per"]).Trim() != "" && Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Per"]).Trim() != "0.00")
            //{
            //    txtIGST_Per.Enabled = false;
            //}
            //else if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Amt"]).Trim() != "" && Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Amt"]).Trim() != "0.00")
            //{
            //    txtCGST_Per.Enabled = false;
            //    txtSGST_Per.Enabled = false;
            //}

            hdnCGSTPer_O.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Per"]).Trim();
            hdnSGSTPer_O.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Per"]).Trim();
            hdnIGSTPer_O.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Per"]).Trim();

            lnkfile_Invoice.Visible = true;
            lnkfile_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Invoice_File"]).Trim();

            lblheading.Text = "Invoice -" + txtInvoiceNo.Text + "  Status -" + Convert.ToString(dsMilestone.Tables[0].Rows[0]["InvoiceApprovalStatus"]).Trim();


            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["Status_id"]).Trim() == "3" || Convert.ToString(dsMilestone.Tables[0].Rows[0]["Status_id"]).Trim() == "4")
            {
                btnCancel.Visible = false;
                trvl_btnSave.Visible = false;
                txtCGST_Per.Enabled = false;
                txtSGST_Per.Enabled = false;
                txtSGST_Per.Enabled = false;
            }




            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["Status_id"]).Trim() == "5")
            {
                trvl_btnSave.Text = "Update Invoice";
                trvl_btnSave.Visible = true;
            }
            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["Status_id"]).Trim() == "4" || Convert.ToString(dsMilestone.Tables[0].Rows[0]["Status_id"]).Trim() == "3")
            {

                trvl_btnSave.Visible = false;
                txtAmtWithOutTax.Enabled = false;
                txtInvoiceDate.Enabled = false;
                txtInvoiceNo.Enabled = false;
                txtSupplierInvoiceDt.Enabled = false;
                txtVoucherNo.Enabled = false;
                ChkAdvSettlement.Enabled = false;
                txtAdv_Settlement_Amt.Enabled = false;
                ChkSecuritySettlement.Enabled = false;
                txtSecurityDeposit.Enabled = false;
            }
            lstTripType.Enabled = false;
            lstPOType.Enabled = false;




            dgTravelRequest.DataSource = null;
            dgTravelRequest.DataBind();

            if (dsMilestone.Tables[1].Rows.Count > 0)
            {
                dgTravelRequest.DataSource = dsMilestone.Tables[1];
                dgTravelRequest.DataBind();
                spMilestones.Visible = true;

                foreach (GridViewRow row in dgTravelRequest.Rows)
                {
                    //row.Cells[0].Visible = false;
                    LinkButton lnkTravelDetailsEdit = (LinkButton)row.FindControl("lnkTravelDetailsEdit");
                    lnkTravelDetailsEdit.Visible = false;
                }
            }
            getInvoiceUploadedFiles();

            if (chkIsInvoiceWithoutPO.Checked == false)
            {
                Check_Advance_Payment_POWO();
            }

            if (dsMilestone.Tables[8].Rows.Count > 0)
            {

                decimal AdvAmt = 0, SecurityAmt = 0, dGSTAmt = 0;
                DataRow[] result = dsMilestone.Tables[8].Select("AdvancePayTypeID = 1");
                foreach (DataRow row in result)
                {
                    //string stuname =Convert.ToString(row["AdvancePayTypeID"].ToString());
                    txtAdv_Settlement_Amt.Text = Convert.ToString(row["ADSettlement_Amt"]);
                    if (Convert.ToString(row["ADSettlement_Amt"]) != "")
                        dGSTAmt = Convert.ToDecimal(row["AD_Settlment_GSTAmt"]);
                    AdvAmt = Convert.ToDecimal(row["ADSettlement_Amt"]);
                    ChkAdvSettlement.Checked = true;
                    ChkAdvSettlement.Visible = true;
                    DAdvSet.Visible = true;
                    SAADV_Amt.Visible = true;
                }
                DataRow[] result2 = dsMilestone.Tables[8].Select("AdvancePayTypeID = 2");
                foreach (DataRow row in result2)
                {
                    //string stuname = Convert.ToString(row["AdvancePayTypeID"].ToString());
                    txtSecurityDeposit.Text = Convert.ToString(row["ADSettlement_Amt"]);
                    dGSTAmt = Convert.ToDecimal(row["AD_Settlment_GSTAmt"]);
                    SecurityAmt = Convert.ToDecimal(row["ADSettlement_Amt"]);
                    ChkSecuritySettlement.Checked = true;
                    ChkSecuritySettlement.Visible = true;
                    SASecurity.Visible = true;
                    SAADV_Amt.Visible = true;
                }
                DataSet dtAdvDetails = new DataSet();
                decimal Adv_Bal_Amt = 0;
                Adv_Bal_Amt = Convert.ToDecimal(Convert.ToDecimal(txtAmtWithTax_Invoice.Text) - Convert.ToDecimal(AdvAmt) - Convert.ToDecimal(SecurityAmt)) - dGSTAmt;
                dtAdvDetails = spm.getFormated_Amount("get_formated_Invoice_Amount", Adv_Bal_Amt, 0, 0, 0, 0);
                if (dtAdvDetails.Tables[0].Rows.Count > 0)
                {
                    txtAdv_Bal_Amt.Text = Convert.ToString(dtAdvDetails.Tables[0].Rows[0]["invoiceAmt"]).Trim();
                }
            }
            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["Status_id"]).Trim() == "2") //account approved or final approved invoice can not allowed to cancel
            {
                trvl_btnSave.Visible = false;
                btnCancel.Visible = false;
                txtAmtWithOutTax.Enabled = false;
                txtInvoiceDate.Enabled = false;
                txtInvoiceNo.Enabled = false;
                txtCGST_Per.Enabled = false;
                txtSGST_Per.Enabled = false;
                txtIGST_Amt.Enabled = false;
                txtSupplierInvoiceDt.Enabled = false;
                txtVoucherNo.Enabled = false;
                ChkAdvSettlement.Enabled = false;
                txtAdv_Settlement_Amt.Enabled = false;

                if (gvuploadedFiles.Rows.Count > 0)
                {
                    gvuploadedFiles.Columns[1].Visible = false;
                }

            }
            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["Status_id"]).Trim() == "1")
            {
                trvl_btnSave.Visible = false;
                btnCancel.Visible = false;
                ChkAdvSettlement.Enabled = false;
                txtAdv_Settlement_Amt.Enabled = false;
                ChkSecuritySettlement.Enabled = false;
                txtSecurityDeposit.Enabled = false;
            }

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["ApproverStatus"]).Trim() == "Approved")
            {
                trvl_btnSave.Visible = false;
                txtAmtWithOutTax.Enabled = false;
                txtInvoiceDate.Enabled = false;
                txtInvoiceNo.Enabled = false;
                txtCGST_Per.Enabled = false;
                txtSGST_Per.Enabled = false;
                txtIGST_Amt.Enabled = false;
                ChkAdvSettlement.Enabled = false;
                txtAdv_Settlement_Amt.Enabled = false;
                ChkSecuritySettlement.Enabled = false;
                txtSecurityDeposit.Enabled = false;
                if (gvuploadedFiles.Rows.Count > 0)
                {
                    gvuploadedFiles.Columns[1].Visible = false;
                }
            }

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["InvoiceApprovalStatus"]).Trim() == "Correction")
            {
                trvl_btnSave.Visible = true;
                txtInvoiceDate.Enabled = true;
                ChkAdvSettlement.Enabled = true;
                txtAdv_Settlement_Amt.Enabled = true;
                ChkSecuritySettlement.Enabled = true;
                txtSecurityDeposit.Enabled = true;
            }


            if (Convert.ToString(hdnInvoiceApprStatusId.Value).Trim() == "2")
            {
                DgvApprover.DataSource = dsMilestone.Tables[3];
                DgvApprover.DataBind();
            }

	   if (chkIsInvoiceWithoutPO.Checked == true)
            {
                if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["Status_id"]).Trim() == "5")
                {
                    txtAmtWithOutTax.Enabled = true;
                    txtCGST_Per.Enabled = true;
                    txtSGST_Per.Enabled = true;
                    txtIGST_Per.Enabled = true;
                }
            }



            #region  Add costcenter button and grid not required
            /*
			if (chkIsInvoiceWithoutPO.Checked == false)
			{
				if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["Status_id"]).Trim() == "2")
				{
					//Add costcenter button and grid not required
					//lstforChangeCostCenter.Enabled = false;
					//trvldeatils_btnSave.Enabled = false;

					foreach (GridViewRow row in dgCostcenterList.Rows)
					{
						DropDownList lstMilestoneAmtCostCenter = (DropDownList)row.FindControl("lstMilestoneAmtCostCenter");
						TextBox txtCostCenterAmt = (TextBox)row.FindControl("txtCostCenterAmt");
						lstMilestoneAmtCostCenter.Enabled = false;
						txtCostCenterAmt.Enabled = false;
					}
					foreach (GridViewRow row in dgTravelRequest.Rows)
					{ 
						TextBox txtMilestoneInvoiceAmt = (TextBox)row.FindControl("txtMilestoneInvoiceAmt");
						TextBox txtMilestoneSettlementAmt = (TextBox)row.FindControl("txtMilestoneSettlementAmt");

						txtMilestoneInvoiceAmt.Enabled = false;
						txtMilestoneSettlementAmt.Enabled = false;
					}
				}

			}*/
            #endregion


        }




    }

    public void get_Approver_List()
    {
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();

        int ichkWithoutPo = 0;
        if (chkIsInvoiceWithoutPO.Checked)
            ichkWithoutPo = 1;

        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), txtEmpCode.Text, hdnProject_Dept_Name.Value, Convert.ToString(lstPOType.SelectedValue), ichkWithoutPo);
        if (Convert.ToString(hdnInvoiceId.Value).Trim() == "0")
            btnCancel.Visible = false;

        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
            DgvApprover.DataSource = dsMilestone.Tables[0];
            DgvApprover.DataBind();
           // ApproverName.Visible = true;
            //trvl_btnSave.Visible = true;
            // btnCancel.Visible = true;
            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["APPR_Emp_Code"]).Trim() == "99999999")
            {
                //trvl_btnSave.Visible = false;
                //btnCancel.Visible = false;
            }

            //if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["APPR_ID"]).Trim() == "19")
            //{
            //   // ApproverName.Visible = false;
            //    //trvl_btnSave.Visible = false;
            //    //btnCancel.Visible = false;
            //}
        }
    }


    public void get_Approved_Reject_Approver_List()
    {
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();

        int ichkWithoutPo = 0;
        if (chkIsInvoiceWithoutPO.Checked)
            ichkWithoutPo = 1;

        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), txtEmpCode.Text, hdnProject_Dept_Name.Value, Convert.ToString(lstPOType.SelectedValue),ichkWithoutPo);

        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
            DgvApprover.DataSource = dsMilestone.Tables[0];
            DgvApprover.DataBind();


        }
    }

    public string ReplaceInvalidChars(string filename)
    {
        Regex illegalInFileName = new Regex(@"[#%&{}\!/<'>@+*?$|=]");
        string myString = illegalInFileName.Replace(filename, "_");
        //return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        return myString;
    }

    public void getInvoiceUploadedFiles()
    {

        DataSet dsFiles = spm.getVSCB_UploadedFiles("get_VSCB_UploadedFiels", "Invoice", Convert.ToDouble(hdnInvoiceId.Value), Convert.ToString(hdnSrno.Value).Trim(), "");

        gvuploadedFiles.DataSource = null;
        gvuploadedFiles.DataBind();
        if (dsFiles.Tables[0].Rows.Count > 0)
        {
            gvuploadedFiles.DataSource = dsFiles;
            gvuploadedFiles.DataBind();
        }
    }
    #endregion





    protected void txtCGST_Per_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txtCGST_Per.Text).Trim() == "" || Convert.ToString(txtCGST_Per.Text).Trim() == "0")
        {
            txtCGST_Per.Text = "";
            txtSGST_Per.Text = "";
            txtIGST_Per.Text = "";
            txtIGST_Per.Enabled = true;
        }
        else
        {
            txtSGST_Per.Text = Convert.ToString(txtCGST_Per.Text).Trim();
            txtIGST_Per.Enabled = false;
        }
        check_get_GSTPercenatgeAmt();


    }

    protected void txtSGST_Per_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txtSGST_Per.Text).Trim() == "" || Convert.ToString(txtSGST_Per.Text).Trim() == "0")
        {
            txtCGST_Per.Text = "";
            txtSGST_Per.Text = "";
            txtIGST_Per.Text = "";
            txtIGST_Per.Enabled = true;
        }
        else
        {
            txtCGST_Per.Text = Convert.ToString(txtSGST_Per.Text).Trim();
            txtIGST_Per.Enabled = false;
        }

        check_get_GSTPercenatgeAmt();
    }

    protected void txtIGST_Per_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txtIGST_Per.Text).Trim() != "")
        {
            txtCGST_Per.Text = "";
            txtSGST_Per.Text = "";
            txtCGST_Per.Enabled = false;
            txtSGST_Per.Enabled = false;
        }
        else
        {
            txtCGST_Per.Text = "";
            txtSGST_Per.Text = "";
            txtCGST_Per.Enabled = true;
            txtSGST_Per.Enabled = true;
        }

        check_get_GSTPercenatgeAmt();
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

    protected void lstExpenses_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region get Expeses List
        GetExpeses_Details();
        #endregion
    }

    protected void lstDisplayPOTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnVendorId.Value = lstVendors.SelectedValue;
        hdnPOTypeId.Value = lstDisplayPOTypes.SelectedValue;

        #region get Approver List as Per Invoice Type
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();

        int ichkWithoutPo = 0;
        if (chkIsInvoiceWithoutPO.Checked)
            ichkWithoutPo = 1;

        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), txtEmpCode.Text, hdnProject_Dept_Name.Value, Convert.ToString(lstDisplayPOTypes.SelectedValue), ichkWithoutPo);

        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
            DgvApprover.DataSource = dsMilestone.Tables[0];
            DgvApprover.DataBind();
            trvl_btnSave.Visible = true;
            // btnCancel.Visible = true;
            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["APPR_Emp_Code"]).Trim() == "99999999")
            {
                trvl_btnSave.Visible = false;
                btnCancel.Visible = false;
            }
        }
        #endregion

        Check_CostCenterApprovalMatrix(Convert.ToString(lstProjectDept.SelectedItem.Text).Trim());

    }



    protected void txtMilestoneInvoiceAmt_TextChanged(object sender, EventArgs e)
    {
        //ImageButton btn = (ImageButton)sender;
        TextBox txtMilestoneAmt = (TextBox)sender;
        GridViewRow row = (GridViewRow)txtMilestoneAmt.NamingContainer;
        hdnSrno.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnpamentStatusid.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[1]).Trim();
        hdnMilestoneID.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[2]).Trim();

        TextBox txtMilestoneInvoiceAmt = (TextBox)row.FindControl("txtMilestoneInvoiceAmt"); ;
        TextBox txtMilestoneSettelmentAmt = (TextBox)row.FindControl("txtMilestoneSettlementAmt");
        TextBox txtMilestoneDescription = (TextBox)row.FindControl("txtMilestoneDescription");
        double dMilesStoneAmt_forInvoice = 0;
        double dMilesStoneSettelmentAmt = 0;
        txtAdv_Settlement_Amt.Text = "";
        txtAdv_Bal_Amt.Text = "";
        if (Convert.ToString(txtMilestoneInvoiceAmt.Text).Trim() != "")
        {
            dMilesStoneAmt_forInvoice = Convert.ToDouble(txtMilestoneInvoiceAmt.Text);
        }
        if (Convert.ToString(txtMilestoneSettelmentAmt.Text).Trim() != "")
        {
            dMilesStoneSettelmentAmt = Convert.ToDouble(txtMilestoneSettelmentAmt.Text);
        }
        DataSet dsMilestone = new DataSet();

        string sInvoicetype = Convert.ToString(ConfigurationManager.AppSettings["vscb_SecurityDeposit"]).Trim();

        if (Convert.ToString(txtMilestoneDescription.Text).Trim().ToLower() == Convert.ToString(sInvoicetype).Trim().ToLower())
        {

            dsMilestone = spm.getMilestone_Details_CreateInvoice(Convert.ToDouble(hdnInvoiceId.Value), Convert.ToDouble(hdnMilestoneID.Value), Convert.ToDouble(lstTripType.SelectedValue), txtEmpCode.Text, dMilesStoneAmt_forInvoice, dMilesStoneSettelmentAmt, "insertSecurityDeposit_forInvoice_toTemptbl");

        }
        else
        {
            dsMilestone = spm.getMilestone_Details_CreateInvoice(Convert.ToDouble(hdnInvoiceId.Value), Convert.ToDouble(hdnMilestoneID.Value), Convert.ToDouble(lstTripType.SelectedValue), txtEmpCode.Text, dMilesStoneAmt_forInvoice, dMilesStoneSettelmentAmt, "insertMilesStone_forInvoice_toTemptbl");

        }

        if (dsMilestone != null)
        {
            if (dsMilestone.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["msg"]).Trim() == "insert")
                {


                    txtAmtWithTax_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MilestoneAmt_WithTax_ForInvoice"]).Trim();
                    txtAmtWithOutTax.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Milesstone_Amt_ForInvoice"]).Trim();
                    txtCGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Amt"]).Trim();
                    txtSGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Amt"]).Trim();
                    txtIGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Amt"]).Trim();
                    //txtInvoiceDate.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Invoicedate"]).Trim();
                    txtMilestoneInvoiceAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Milesstone_Amt_ForInvoice_display"]).Trim();
                    txtMilestoneSettelmentAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Milestone_settlementAmt_display"]).Trim();

                    if (Convert.ToString(txtMilestoneDescription.Text).Trim().ToLower() == Convert.ToString(sInvoicetype).Trim().ToLower())
                    {
                        ChkSecuritySettlement.Checked = true;
                        SASecurity.Visible = true;
                        SAADV_Amt.Visible = true;
                        txtSecurityDeposit.Enabled = false;
                        ChkSecuritySettlement.Enabled = false;

                        txtSecurityDeposit.Text = txtMilestoneInvoiceAmt.Text;
                        txtAdv_Bal_Amt.Text = Convert.ToString(Convert.ToDecimal(txtAmtWithTax_Invoice.Text) - Convert.ToDecimal(txtMilestoneInvoiceAmt.Text)).ToString();

                    }
                }
                else
                {
                    txtAmtWithTax_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MilestoneAmt_WithTax_ForInvoice"]).Trim();
                    txtAmtWithOutTax.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Milesstone_Amt_ForInvoice"]).Trim();
                    txtCGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Amt"]).Trim();
                    txtSGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Amt"]).Trim();
                    txtIGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Amt"]).Trim();

                    lblmessage.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["msg"]).Trim();
                    txtMilestoneInvoiceAmt.Text = "";
                    txtMilestoneSettelmentAmt.Text = "";

                }
            }
        }
    }

    protected void dgTravelRequest_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (Convert.ToString(hdnInvoiceId.Value).Trim() != "0")
            //{
            //    TextBox txtMilestoneInvoiceAmt = e.Row.FindControl("txtMilestoneInvoiceAmt") as TextBox;
            //    TextBox txtMilestoneSettlementAmt = e.Row.FindControl("txtMilestoneSettlementAmt") as TextBox;
            //    if (Convert.ToString(txtMilestoneInvoiceAmt.Text).Trim() == "0.00")
            //    {
            //        txtMilestoneInvoiceAmt.Visible = false;
            //        txtMilestoneSettlementAmt.Visible = false;
            //    } 
            //}

        }
    }

    protected void dgCostcenterList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList lstMilestoneAmtCostCenter = (e.Row.FindControl("lstMilestoneAmtCostCenter") as DropDownList);
            DataSet dsCostcenters = GetCostcenter_list();
            lstMilestoneAmtCostCenter.DataSource = dsCostcenters.Tables[0];
            lstMilestoneAmtCostCenter.DataTextField = "CostCentre";
            lstMilestoneAmtCostCenter.DataValueField = "Dept_ID";
            lstMilestoneAmtCostCenter.DataBind();

            //Add Default Item in the DropDownList
            lstMilestoneAmtCostCenter.Items.Insert(0, new ListItem("select Cost Center", "0"));

            //Select the Country of Customer in DropDownList
            //string country = (e.Row.FindControl("lblCountry") as Label).Text;
            //lstMilestoneAmtCostCenter.Items.FindByValue(country).Selected = true;
        }
    }

    public DataSet GetCostcenter_list()
    {
        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_CostCenter_list_forInvoice";

        dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        return dsProjectsVendors;
    }



    protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Int32 irowcnt = 0;
            Int32 irw = 1;
            if (Convert.ToString(hdnMilestoneRowCnt.Value).Trim() != "")
                irowcnt = Convert.ToInt32(hdnMilestoneRowCnt.Value);

            lblMilestoneCostCenter_Err.Text = "";

            if (irowcnt > 0)
            {
                irowcnt += 1;
                foreach (GridViewRow row in dgCostcenterList.Rows)
                {
                    if (irowcnt == irw)
                    {
                        row.Visible = true;
                        hdnMilestoneRowCnt.Value = Convert.ToString(irowcnt);
                        break;
                    }
                    else
                    {
                        DropDownList lstMilestoneAmtCostCenter = (DropDownList)row.FindControl("lstMilestoneAmtCostCenter");
                        if (Convert.ToString(lstMilestoneAmtCostCenter.SelectedValue).Trim() == "0")
                        {
                            lblMilestoneCostCenter_Err.Text = "Please Select all CostCenter from below list";
                            return;
                        }

                        irw += 1;
                    }
                }
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void txtCostCenterAmt_TextChanged(object sender, EventArgs e)
    {
        TextBox txtCostCenterAmt = (TextBox)sender;
        GridViewRow row = (GridViewRow)txtCostCenterAmt.NamingContainer;

        DropDownList lstMilestoneAmtCostCenter = (DropDownList)row.FindControl("lstMilestoneAmtCostCenter");

        if (Convert.ToString(hdnInvoiceApprStatusId.Value).Trim() == "2")
        {

            txtCostCenterAmt.Enabled = false;
        }

        lblMilestoneCostCenter_Err.Text = "";
        if (Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() == "")
        {
            txtCostCenterAmt.Text = "";
            lblMilestoneCostCenter_Err.Text = "Please enter the Invoice details.";
            return;
        }
        if (Convert.ToString(lstMilestoneAmtCostCenter.SelectedValue).Trim() == "0")
        {
            txtCostCenterAmt.Text = "";
            lblMilestoneCostCenter_Err.Text = "Please Select Cost Center";
            return;
        }
        bool blnval = Validate_CostcenterAmt(txtCostCenterAmt, "N");
    }

    protected bool Validate_CostcenterAmt(TextBox txtCostcenterAmt, string sflag)
    {
        bool blnValidate = true;
        Int32 iduplicateCnt = 0;
        foreach (GridViewRow crow in dgCostcenterList.Rows)
        {
            DropDownList lstMilestoneAmtCostCenter_1 = (DropDownList)crow.FindControl("lstMilestoneAmtCostCenter");
            iduplicateCnt = 0;
            if (Convert.ToString(lstMilestoneAmtCostCenter_1.SelectedValue).Trim() != "0")
            {
                foreach (GridViewRow jrow in dgCostcenterList.Rows)
                {
                    DropDownList lstMilestoneAmtCostCenter_2 = (DropDownList)jrow.FindControl("lstMilestoneAmtCostCenter");
                    if (Convert.ToString(lstMilestoneAmtCostCenter_2.SelectedValue).Trim() != "0")
                    {
                        if (Convert.ToString(lstMilestoneAmtCostCenter_1.SelectedValue).Trim() == Convert.ToString(lstMilestoneAmtCostCenter_2.SelectedValue).Trim())
                        {
                            iduplicateCnt += 1;
                        }
                    }
                }

                if (iduplicateCnt > 1)
                {
                    lblMilestoneCostCenter_Err.Text = "Cost Center already selected";
                    blnValidate = false;
                }

            }
        }

        decimal dCostcenterAmt = 0;
        foreach (GridViewRow crow in dgCostcenterList.Rows)
        {
            TextBox txtCostCenterAmt_2 = (TextBox)crow.FindControl("txtCostCenterAmt");

            if (Convert.ToString(txtCostCenterAmt_2.Text).Trim() != "")
            {
                dCostcenterAmt += Convert.ToDecimal(txtCostCenterAmt_2.Text);
            }
        }

        if (Convert.ToString(txtAmtWithOutTax.Text).Trim() != "")
        {
            //if (dCostcenterAmt > Convert.ToDecimal(txtAmtWithTax_Invoice.Text))
            if (dCostcenterAmt > Convert.ToDecimal(txtAmtWithOutTax.Text))
            {
                lblMilestoneCostCenter_Err.Text = "Overall Cost Center amount Exceeding Invoice Amount (Without GST).Please correct and try again!";
                txtCostcenterAmt.Text = "";
                blnValidate = false;
            }
            if (Convert.ToString(sflag).Trim() == "Y")
            {
                if (dCostcenterAmt < Convert.ToDecimal(txtAmtWithOutTax.Text))
                {
                    lblMilestoneCostCenter_Err.Text = "Overall Cost Center amount is less than Invoice Amount (Without GST).Please correct and try again!";
                    blnValidate = false;
                }
            }


        }

        return blnValidate;
    }

    protected void lstforChangeCostCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if(Convert.ToString(lstforChangeCostCenter.SelectedValue).Trim()!="0")
        //{

        //}
        hdnProject_Dept_Name.Value = Convert.ToString(lstforChangeCostCenter.SelectedValue).Trim();
        txtCostCenter.Text = Convert.ToString(lstforChangeCostCenter.SelectedItem.Text).Trim();
        txtProject.Text = Convert.ToString(lstforChangeCostCenter.SelectedValue).Trim();
        hdnProject_Dept_Id.Value = Convert.ToString(GetCostCenter_Id(lstforChangeCostCenter.SelectedItem.Text)).Trim();
        get_Approver_List();
        Check_CostCenterApprovalMatrix(Convert.ToString(lstforChangeCostCenter.SelectedValue).Trim());
        get_PWODetails_POTYpeAgainstCostCenterCheck(Convert.ToString(lstforChangeCostCenter.SelectedValue).Trim());
    }

    public void get_PWODetails_POTYpeAgainstCostCenterCheck(string tallyCode)
    {

        DataSet dtPOWODetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_POTYpeAgainstCostCenterCheck";

        spars[1] = new SqlParameter("@Project_Dept_Name", SqlDbType.VarChar);
        spars[1].Value = tallyCode;

        spars[2] = new SqlParameter("@POTypeID", SqlDbType.VarChar);
        if (Convert.ToString(hdnPOTypeId.Value).Trim() != "")
            spars[2].Value = Convert.ToString(hdnPOTypeId.Value);
        else
            spars[2].Value = DBNull.Value;

        //spars[3] = new SqlParameter("@IsInvoiceWithPO", SqlDbType.VarChar);
        //spars[3].Value = tblApproversStatus;

        dtPOWODetails = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        if (dtPOWODetails.Tables[0].Rows.Count <= 0)
        {
            lblmessage.Visible = true;
            lblmessage.Text = "You can not create the Invoice for the selected costcenter. please contact to Admin.";
            trvl_btnSave.Visible = false;
        }
    }

    public void Check_Advance_Payment_POWO()
    {
        DataSet dtAdvBal = new DataSet();
        SqlParameter[] spars12 = new SqlParameter[3];
        spars12[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
        //spars12[0].Value = "Get_ADV_Pay_POWO_Invoice"; 
        spars12[0].Value = "get_Advance_Security_Amount_Status";
        spars12[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        spars12[1].Value = txtEmpCode.Text;
        spars12[2] = new SqlParameter("@POID", SqlDbType.VarChar);
        if (Convert.ToString(lstTripType.SelectedValue).Trim() != "")
            spars12[2].Value = Convert.ToDouble(lstTripType.SelectedValue);
        else
            spars12[2].Value = DBNull.Value;
        dtAdvBal = spm.getDatasetList(spars12, "sp_VSCB_CreatePOWO_Users");

        txt_Pay_Adv_Amt.Text = "";
        txt_Pay_Adv_Settlement.Text = "";
        txt_Pay_Adv_Bal_Amt.Text = "";
        hdnSecurityDeposit.Value = "";

        ChkAdvSettlement.Checked = false;
        ChkSecuritySettlement.Checked = false;
        ChkSecuritySettlement.Visible = false;
        if (dtAdvBal.Tables[0].Rows.Count > 0)
        {
            Adv1.Visible = false;
            Adv2.Visible = false;
            Adv3.Visible = false;

            if (!String.IsNullOrEmpty(dtAdvBal.Tables[0].Rows[0]["AdvPayment_Amt"].ToString()))
            {

                txt_Pay_Adv_Amt.Text = Convert.ToString(dtAdvBal.Tables[0].Rows[0]["AdvPayment_Amt"]).Trim();
                txt_Pay_Adv_Settlement.Text = Convert.ToString(dtAdvBal.Tables[0].Rows[0]["AdvPayment_Settlement_Amt"]).Trim();
                txt_Pay_Adv_Bal_Amt.Text = Convert.ToString(dtAdvBal.Tables[0].Rows[0]["AdvPayment_Bal_Amt"]).Trim();
                Adv1.Visible = true;
                Adv2.Visible = true;
                Adv3.Visible = true;

            }
            else
            {
                Adv1.Visible = false;
                Adv2.Visible = false;
                Adv3.Visible = false;
            }
        }
        if (Convert.ToString(txt_Pay_Adv_Bal_Amt.Text).Trim() != "")
        {
            if (dtAdvBal.Tables[4].Rows.Count > 0)
            {
                ChkAdvSettlement.Visible = false;

                if (!String.IsNullOrEmpty(dtAdvBal.Tables[4].Rows[0]["TobePaidAmtWithtax"].ToString()))
                {
                    if (Convert.ToDecimal(txt_Pay_Adv_Bal_Amt.Text) > 0)
                        ChkAdvSettlement.Visible = true;
                    else
                        ChkAdvSettlement.Visible = false;
                }
            }
        }
        //if (dtAdvBal.Tables[1].Rows.Count > 0)
        //{
        //	if (Convert.ToDecimal(dtAdvBal.Tables[1].Rows[0]["Adv_Pay_Bal"].ToString()) > 0)
        //	{
        //		ChkAdvSettlement.Visible = true;
        //	}
        //}
        //else
        //{
        //	ChkAdvSettlement.Visible = false;
        //}
        if (dtAdvBal.Tables[1].Rows.Count > 0)
        {
            if (Convert.ToString(dtAdvBal.Tables[1].Rows[0]["AdvancePayTypeID"].ToString()) == "2")
            {
                if (dtAdvBal.Tables[5].Rows.Count > 0)
                {
                    ChkAdvSettlement.Visible = false;

                    if (!String.IsNullOrEmpty(dtAdvBal.Tables[5].Rows[0]["TobePaidAmtWithtax"].ToString()))
                    {
                        if (Convert.ToDecimal(txt_Pay_Adv_Bal_Amt.Text) > 0)
                        {
                            ChkSecuritySettlement.Visible = true;
                            hdnSecurityDeposit.Value = "Yes";
                        }
                        else
                        {
                            ChkSecuritySettlement.Visible = false;
                        }

                    }
                }


            }
        }
    }



    protected void ChkAdvSettlement_CheckedChanged(object sender, EventArgs e)
    {

        Advance_Checkboc_Checked();
    }

    private void Advance_Checkboc_Checked()
    {
        txtAdv_Settlement_Amt.Text = "";
        txtAdv_Bal_Amt.Text = "";
        txtSecurityDeposit.Text = "";
        SASecurity.Visible = false;
        if (ChkAdvSettlement.Checked && ChkSecuritySettlement.Checked)
        {
            SAADV_Amt.Visible = true;
            SASecurity.Visible = true;
            DAdvSet.Visible = true;
        }
        else if (ChkAdvSettlement.Checked)
        {
            DAdvSet.Visible = true;
            SAADV_Amt.Visible = true;
        }
        else if (ChkSecuritySettlement.Checked)
        {
            SAADV_Amt.Visible = true;
            DAdvSet.Visible = false;
            SASecurity.Visible = true;
        }
        else
        {
            DAdvSet.Visible = false;
            SAADV_Amt.Visible = false;
        }
    }
    protected void txtAdv_Settlement_Amt_TextChanged(object sender, EventArgs e)
    {
        Set_Advance_Settlement_Amt();
    }

    private void Set_Advance_Settlement_Amt()
    {
        try
        {
            hdnGST_AMT.Value = "";

            txtAdv_Bal_Amt.Text = "";
            decimal AdvBalAmt = 0, Invoice_Settlement_Amt = 0, SecuryDeposit = 0, Adv_Bal_Amt = 0;
            decimal dCGSTAmt = 0, dSGSTAmt = 0, dIGSTAmt = 0, GSTAmt = 0;
            if (Convert.ToString(txtAdv_Settlement_Amt.Text).Trim() != "" && Convert.ToString(txtAdv_Settlement_Amt.Text).Trim() != "0" && Convert.ToString(txtAdv_Settlement_Amt.Text).Trim() != "0.00")
            {
                if (Convert.ToDecimal(txtAdv_Settlement_Amt.Text) > Convert.ToDecimal(txtAmtWithTax_Invoice.Text))
                {
                    lblmessage.Text = "Overall Advance amount is exceeding than Invoice Amount.";
                    txtAdv_Settlement_Amt.Text = "";
                    return;
                }
                Invoice_Settlement_Amt = Convert.ToDecimal(txtAdv_Settlement_Amt.Text);
            }
            if (Convert.ToString(txtSecurityDeposit.Text).Trim() != "" && Convert.ToString(txtSecurityDeposit.Text).Trim() != "0")
            {
                if (Convert.ToDecimal(txtSecurityDeposit.Text) > Convert.ToDecimal(txtAmtWithTax_Invoice.Text))
                {
                    lblmessage.Text = "Overall Security Deposit is exceeding than Invoice Amount.";
                    txtSecurityDeposit.Text = "";
                    return;
                }
                SecuryDeposit = Convert.ToDecimal(txtSecurityDeposit.Text);
            }

            DataSet dtAdvBal = new DataSet();
            SqlParameter[] spars4 = new SqlParameter[3];
            spars4[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
            //spars4[0].Value = "Get_ADV_Pay_Invoice_Amt"; 
            spars4[0].Value = "get_Advance_Security_Amount_Status";

            spars4[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars4[1].Value = txtEmpCode.Text;
            spars4[2] = new SqlParameter("@POID", SqlDbType.BigInt);
            spars4[2].Value = Convert.ToDouble(lstTripType.SelectedValue);

            dtAdvBal = spm.getDatasetList(spars4, "sp_VSCB_CreatePOWO_Users");
            if (dtAdvBal.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(dtAdvBal.Tables[0].Rows[0]["AdvPayment_Bal_Amt"].ToString()) != "")
                {
                    AdvBalAmt = Convert.ToDecimal(dtAdvBal.Tables[0].Rows[0]["AdvPayment_Bal_Amt"].ToString());
                    if (Invoice_Settlement_Amt > AdvBalAmt)
                    {
                        lblmessage.Text = "Overall Advance settlement amount is exceeding than Advance amount.";
                        txtAdv_Settlement_Amt.Text = "";
                        return;
                    }
                }


                //Check GST is Applicable for this Advance Amt
                if (dtAdvBal.Tables[2].Rows.Count > 0)
                {
                    if (Convert.ToString(dtAdvBal.Tables[2].Rows[0]["CGST_Amt"]) != "")
                    {
                        if (Convert.ToDouble(dtAdvBal.Tables[2].Rows[0]["CGST_Amt"]) > 0)
                        {
                            dCGSTAmt = (Convert.ToDecimal(Invoice_Settlement_Amt) * Convert.ToDecimal(dtAdvBal.Tables[3].Rows[0]["CGST_Per"])) / 100;
                        }
                        if (Convert.ToDouble(dtAdvBal.Tables[2].Rows[0]["SGST_Amt"]) > 0)
                        {
                            dSGSTAmt = (Convert.ToDecimal(Invoice_Settlement_Amt) * Convert.ToDecimal(dtAdvBal.Tables[3].Rows[0]["SGST_Per"])) / 100;
                        }
                        if (Convert.ToDouble(dtAdvBal.Tables[2].Rows[0]["IGST_Amt"]) > 0)
                        {
                            dIGSTAmt = (Convert.ToDecimal(Invoice_Settlement_Amt) * Convert.ToDecimal(dtAdvBal.Tables[3].Rows[0]["IGST_Per"])) / 100;
                        }
                        GSTAmt = dCGSTAmt + dSGSTAmt + dIGSTAmt;
                    }
                }
            }

            Adv_Bal_Amt = (Convert.ToDecimal(txtAmtWithTax_Invoice.Text) - Convert.ToDecimal(Invoice_Settlement_Amt) - Convert.ToDecimal(SecuryDeposit)) - GSTAmt;

            hdnGST_AMT.Value = Convert.ToString(GSTAmt);

            DataSet dtPOWODetails = new DataSet();
            dtPOWODetails = spm.getFormated_Amount("get_formated_Invoice_Amount", Adv_Bal_Amt, Invoice_Settlement_Amt, SecuryDeposit, 0, 0);
            if (dtPOWODetails.Tables[0].Rows.Count > 0)
            {
                txtAdv_Bal_Amt.Text = Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["invoiceAmt"]).Trim();
                if (Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["cgstAmt"]).Trim() != "0.00")
                {
                    txtAdv_Settlement_Amt.Text = Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["cgstAmt"]).Trim();
                }
                if (Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["sgstAmt"]).Trim() != "0.00")
                {
                    txtSecurityDeposit.Text = Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["sgstAmt"]).Trim();
                }
                //txtIGST_Amt.Text = Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["igstAmt"]).Trim();
                //txtAmtWithTax_Invoice.Text = Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["invoiceAmtwithGst"]).Trim();
            }
        }
        catch (Exception ex)
        {
        }

    }
    protected void ChkSecuritySettlement_CheckedChanged(object sender, EventArgs e)
    {
        Advance_Checkboc_Checked();
    }

    protected void txtSecuryDeposit_TextChanged(object sender, EventArgs e)
    {
        Set_Advance_Settlement_Amt();
    }
}