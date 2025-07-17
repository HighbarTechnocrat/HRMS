using Microsoft.Reporting.WebForms;
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

public partial class VSCB_ApproveInvoice_New : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
     
    #region Creative_Default_methods

    
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0; 
    public DataTable dtEmp;
    public DataSet dsDirecttaxSectionList = new DataSet();
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

                lblMilestoneCostCenter_Err.Text = "";
                if (!Page.IsPostBack)
                {
                  
                    FilePath.Value = Convert.ToString(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBInvoicefiles"]).Trim() + "/"));
                    hdnSingPOCopyFilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBSignPOWOfiles"]).Trim());

                   // PopulateEmployeeData();

                      GetPODetails_List(); 
                      GetPOTypes();
                    //if (Convert.ToString(Session["Empcode"]).Trim() == "99999999")
                    //{
                    //    get_ProjectDept_Vendor_List();
                    //    get_DirectTaxTypeList();
                    //    get_DirectTaxSectionsList();
                    //}

                    if (Request.QueryString.Count>0)
                    { 
                        hdnInvoiceId.Value = Convert.ToString(Request.QueryString["invid"]).Trim();
                        check_POWO_IsForLoginEployeee();
                        
                        get_InvoiceDetails_MilestonesList_Approval();
                        get_Approver_List_Invoice();
                        get_Check_Approver_IsInvoice_Approved();

                        hdnApprovedPO_FileName.Value = Convert.ToString(Regex.Replace(Convert.ToString(lstTripType.SelectedItem.Text), @"[^0-9a-zA-Z\._]", "_")).Trim() + ".pdf";
                        hdnApprovedPO_FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBApprovedPOWOfiles"]).Trim());
                         
                         
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

        if (Convert.ToString(hdnIsFinalApprover.Value).Trim().ToLower() == "yes")
        {
            if (chkIsInvoiceWithoutPO.Checked == true)
            {
                if (Convert.ToString(lstExpenses.SelectedValue).Trim() == "0" || Convert.ToString(lstExpenses.SelectedValue).Trim() == "")
                {
                    lblmessage.Text = "Please Select Exppenses Type.";
                    return;
                }

                if (Convert.ToString(lstExpense_details.SelectedValue).Trim() == "0" || Convert.ToString(lstExpense_details.SelectedValue).Trim() == "")
                {
                    lblmessage.Text = "Please Select Exppenses Details.";
                    return;
                }

                if (Convert.ToString(lstItem_details.SelectedValue).Trim() == "0" || Convert.ToString(lstItem_details.SelectedValue).Trim() == "")
                {
                    lblmessage.Text = "Please Select Product Details.";
                    return;
                }
            }


            if (chkIsInvoiceWithoutPO.Checked == false)
            {
                if (Validate_CostcenterAmt(TextBox1, "Y") == false)
                {
                    return;
                }
            }

        }

        string[] strdate;
        string strInvoiceDate = "";
        Decimal dDirectTaxPercentage_Amt = 0;
        Decimal dDirectTaxPercentage = 0;
        Int32 iDirectTaxTypeID = 1;
        Int32 iDirectTax_DescriptionID = 1;
        Decimal dPayableAmt_WithTax = 0;
        Boolean blnIsTDSApplicable = false;
        dPayableAmt_WithTax = Convert.ToDecimal(txtAmtWithTax_Invoice.Text);

        if (Convert.ToString(hdnIsFinalApprover.Value).Trim().ToLower() == "yes")
        {
            //Validate or calculate TDS
            if (chkIsInvoiceWithoutPO.Checked)
            {
                if (Convert.ToString(lstDirectTaxSections.SelectedValue).Trim() == "0")
                {
                    lblmessage.Text = "Please select Direct Tax Section.";
                    return;
                }

                if (Convert.ToString(lstDirectTax.SelectedValue).Trim() != "1" && Convert.ToString(lstDirectTax.SelectedValue).Trim() != "0")
                {
                    if (Convert.ToString(txtDirectTaxPercentage.Text).Trim() == "")
                    {
                        lblmessage.Text = "Please enter Direct Tax Percentage.";
                        return;
                    }
                    if (Convert.ToString(txtDirectTaxPercentage.Text).Trim() != "")
                    {
                        if (Convert.ToDouble(txtDirectTaxPercentage.Text) > 100)
                        {
                            txtDirectTaxPercentage.Text = "";
                            lblmessage.Text = "Please enter correct Direct Tax Percentage.";
                            return;
                        }

                        strdate = Convert.ToString(txtDirectTaxPercentage.Text).Trim().Split('.');
                        if (strdate.Length > 2)
                        {
                            txtDirectTaxPercentage.Text = "";
                            lblmessage.Text = "Please enter correct Direct Tax Percentage.";
                            return;
                        }
                    }


                    dDirectTaxPercentage = Convert.ToDecimal(txtDirectTaxPercentage.Text);

                    Decimal rCGST = 0;
                    Decimal rSGST = 0;
                    Decimal rIGST = 0;

                    #region GST Calculation Not Required
                    /* if (Convert.ToString(txtCGST_Per.Text).Trim() != "")
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
                     }*/
                    #endregion

                    if (Convert.ToString(txtCGST_Amt.Text).Trim() != "")
                    {
                        rCGST = Math.Round(Convert.ToDecimal(txtCGST_Amt.Text), 2);
                    }
                    if (Convert.ToString(txtSGST_Amt.Text).Trim() != "")
                    {
                        rSGST = Math.Round(Convert.ToDecimal(txtSGST_Amt.Text), 2);
                    }
                    if (Convert.ToString(txtIGST_Amt.Text).Trim() != "")
                    {
                        rIGST = Math.Round(Convert.ToDecimal(txtIGST_Amt.Text), 2);
                    }

                    dDirectTaxPercentage_Amt = Math.Round(dDirectTaxPercentage * Convert.ToDecimal(txtAmtWithOutTax.Text) / 100, 2);

                    txtDirecttaxAmt.Text = Convert.ToString(dDirectTaxPercentage_Amt).Trim();
                    dPayableAmt_WithTax = Math.Round((rCGST + rSGST + rIGST + Convert.ToDecimal(txtAmtWithOutTax.Text)) - dDirectTaxPercentage_Amt, 2);
                    txtAmtWithTax_Invoice.Text = Convert.ToString(dPayableAmt_WithTax);
                }
            }
            else
            {
                foreach (GridViewRow row in gvInvoiceMilestone_Acc.Rows)
                {
                    DropDownList lstDirectTaxSections_ACC = (DropDownList)row.FindControl("lstDirectTaxSections_ACC");
                    if (Convert.ToString(lstDirectTaxSections_ACC.SelectedValue).Trim() == "0")
                    {
                        lblmessage.Text = "Please select Direct Tax Section.";
                        return;
                    } 

                }
                if (Convert.ToString(txtDirecttaxAmt.Text).Trim() != "")
                    dDirectTaxPercentage_Amt = Convert.ToDecimal(txtDirecttaxAmt.Text);

				if (Convert.ToString(txtAdv_Bal_Amt.Text).Trim() != "")
				{
					if (Convert.ToDecimal(txtAdv_Bal_Amt.Text) < 0)
					{
						lblmessage.Text = "Overall Invoice Payable Amt Exceeding Invoice Amount (Without GST).Please correct and try again!"; 
						return;
					}
				}

                if(Convert.ToString(txtIsSecurity_DepositInvoice.Text).Trim()=="true")
                {
                    double dSecurity_Deposit_BalAmt = 0;
                    double dSecurity_Deposit_SettlemtAmt = 0;
                    if (Convert.ToString(txt_Pay_Adv_Bal_Amt.Text).Trim()!="")
                    {
                        dSecurity_Deposit_BalAmt = Convert.ToDouble(txt_Pay_Adv_Bal_Amt.Text);
                    }                    

                    if(Convert.ToString(txtSecurityDeposit.Text).Trim()!="")
                    {
                        dSecurity_Deposit_SettlemtAmt = Convert.ToDouble(txtSecurityDeposit.Text);
                    }

                    if(dSecurity_Deposit_SettlemtAmt > dSecurity_Deposit_BalAmt)
                    {
                        lblmessage.Text = "Overall Invoice Settlement Amt Exceeding than Security Deposit Balance Amt.Please correct and try again!";
                        return;
                    }

                    if (Convert.ToString(txtinvoice_description.Text).Trim() == "")
                    {
                        lblmessage.Text = "Please Enter Invoice Description.";
                        return;
                    }


                }




            }

            #region format Currency

            /*DataSet dtPOWODetails = new DataSet();
            decimal dinvoiceAmt = 0, dCGSTAmt = 0, dSGSTAmt = 0, dIGSTAmt = 0, dInvoiceAmtWithtax = 0;

            if (Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() != "")
                dinvoiceAmt = Convert.ToDecimal(txtAmtWithTax_Invoice.Text);

            if (Convert.ToString(txtDirecttaxAmt.Text).Trim() != "")
                dCGSTAmt = Convert.ToDecimal(txtDirecttaxAmt.Text);

            dtPOWODetails = spm.getFormated_Amount("get_formated_Invoice_Amount", dinvoiceAmt, dCGSTAmt, dSGSTAmt, dIGSTAmt, dInvoiceAmtWithtax);


            if (dtPOWODetails.Tables[0].Rows.Count > 0)
            {
                txtAmtWithTax_Invoice.Text = Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["invoiceAmt"]).Trim();
                txtDirecttaxAmt.Text = Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["cgstAmt"]).Trim();
                txtDirecttaxAmt_ACC_Display.Text = Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["cgstAmt"]).Trim();
            }*/
            #endregion
        }

        #region date formatting
        if (Convert.ToString(txtInvoiceDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtInvoiceDate.Text).Trim().Split('-');
            strInvoiceDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        #endregion
         

        if (chkLDC_Applicable.Checked == true)
            blnIsTDSApplicable = true;

        if (Convert.ToString(hdn_next_Appr_Emp_Name.Value).Trim() != "")
        {
            //update Next approver Invoice Status
            spm.InsertInvoice_ApproverDetails("Insertinvoice_Approver", Convert.ToDouble(hdnInvoiceId.Value), hdn_next_Appr_Empcode.Value, Convert.ToInt32(hdn_next_Appr_ID.Value), "Pending", Convert.ToString(txtRemarks.Text).Trim(),Convert.ToString(hdn_curnt_Appr_Empcode.Value), Convert.ToString(hdn_curnt_Appr_ID.Value),"");



            #region Send Email to next Approver

            /* string sApproverEmail_CC = "";

             DataSet dsMilestone = new DataSet();
             dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), txtEmpCode.Text, hdnProject_Dept_Name.Value);

             for (Int32 irow = 0; irow < dsMilestone.Tables[0].Rows.Count; irow++)
             {
                 if (Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() != "" && Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() != "Pending" && Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() !=Convert.ToString(hdn_curnt_Appr_EmpEmail_ID.Value).Trim())
                 {
                     if (Convert.ToString(sApproverEmail_CC).Trim() == "")
                         sApproverEmail_CC = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                     else
                         sApproverEmail_CC = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() + ";" + sApproverEmail_CC;
                 }
             }

             StringBuilder strbuild_Approvers = new StringBuilder();
             strbuild_Approvers.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%;border:solid'>");
             strbuild_Approvers.Append("<tr style='background-color:#C7D3D4'><td>Approver Name</td><td>Status</td><td>Approved On</td><td>Approver Remarks</td></tr>");
             for (Int32 irow = 0; irow < dsMilestone.Tables[0].Rows.Count; irow++)
             {
                 strbuild_Approvers.Append("<tr style='border:solid'><td style='border:solid'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["ApproverName"]).Trim() + " </td>");
                 strbuild_Approvers.Append("<td style='border:solid'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() + "</td>");
                 strbuild_Approvers.Append("<td style='border:solid'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["approved_on"]).Trim() + "</td>");
                 strbuild_Approvers.Append("<td style='border:solid'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Remarks"]).Trim() + "</td></tr>");
             }
             strbuild_Approvers.Append("</table>");

             string strSubject = "Request for - Invoice Approval - " + Convert.ToString(txtInvoiceNo.Text).Trim();
             string strInvoiceURL = "";            
             strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["approverLink_VSCB"]).Trim() + "?invid=" + hdnInvoiceId.Value).Trim();
             StringBuilder strbuild = new StringBuilder();

             strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'>");
             strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
             strbuild.Append("<tr><td style='height:20px'></td></tr>");
             strbuild.Append("<tr><td colspan='2'> " + hdn_curnt_Appr_Emp_Name.Value + " has approved an invoice with the following details.</td></tr>");
             strbuild.Append("<tr><td style='height:20px'></td></tr>");

             if (chkIsInvoiceWithoutPO.Checked == false)
             {
                 strbuild.Append("<tr><td>POWO Title :-</td><td>" + Convert.ToString(txtPOTitle.Text).Trim() + "</td></tr>");
                 strbuild.Append("<tr><td>POWO No :-</td><td>" + Convert.ToString(lstTripType.SelectedItem.Text).Trim() + "</td></tr>");
                 strbuild.Append("<tr><td>POWO Amount :-</td><td>" + Convert.ToString(txtPOWOAmt.Text).Trim() + "</td></tr>");
                 strbuild.Append("<tr><td>Project/Department :-</td><td>" + Convert.ToString(txtProject.Text).Trim() + "</td></tr>");
                 strbuild.Append("<tr><td>POWO Date :-</td><td>" + Convert.ToString(txtFromdate.Text).Trim() + "</td></tr>");
                 strbuild.Append("<tr><td>Vendor Name :-</td><td>" + Convert.ToString(txtVendor.Text).Trim() + "</td></tr>");
             }
             if (chkIsInvoiceWithoutPO.Checked == false)
             {
                 strbuild.Append("<tr><td>Project/Department :-</td><td>" + Convert.ToString(lstProjectDept.SelectedItem.Text).Trim() + "</td></tr>");
                 strbuild.Append("<tr><td>Vendor Name :-</td><td>" + Convert.ToString(lstVendors.SelectedItem.Text).Trim() + "</td></tr>");
             }
             strbuild.Append("<tr><td style='height:30px'></td></tr>");
             strbuild.Append("<tr><td>Invoice No :-</td><td>" + Convert.ToString(txtInvoiceNo.Text).Trim() + "</td></tr>");
             strbuild.Append("<tr><td>Invoice Date :-</td><td>" + Convert.ToString(txtInvoiceDate.Text).Trim() + "</td></tr>");
             strbuild.Append("<tr><td>Invoice Amount :-</td><td>" + Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() + "</td></tr>");

             strbuild.Append("<tr><td style='height:40px'></td></tr>");
             strbuild.Append("<tr><td style='height:20px'><a href='" + strInvoiceURL + "'> Please Click here for your action </a></td></tr>");

             strbuild.Append("</table>  <br /><br />");

             strbuild.Append(strbuild_Approvers);

             spm.sendMail(hdn_next_Appr_EmpEmail_ID.Value, strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);

         */
            #endregion

        }
        else
        {
            //Update Direct Tax Amount


            string sExpenseId = "";
            string sExpesesDtlsId = "";
            string sItemDtlsId = "";

            if (chkIsInvoiceWithoutPO.Checked == true)
            {

                sExpenseId = lstExpenses.SelectedValue;
                sExpesesDtlsId = lstExpense_details.SelectedValue;
                sItemDtlsId = lstItem_details.SelectedValue;
                if (Convert.ToString(lstDirectTax.SelectedValue).Trim() != "0")
                {
                    iDirectTaxTypeID = Convert.ToInt32(lstDirectTax.SelectedValue);
                }

                if (Convert.ToString(lstDirectTaxSections.SelectedValue).Trim() != "0")
                {
                    iDirectTax_DescriptionID = Convert.ToInt32(lstDirectTaxSections.SelectedValue);
                }
            }
            else
            {
                iDirectTax_DescriptionID = 0;
                iDirectTaxTypeID = 0;
            }

            if (ChkAdvSettlement.Checked || ChkSecuritySettlement.Checked)
            {
                if (Convert.ToString(txtAdv_Bal_Amt.Text).Trim() != "")
                    dPayableAmt_WithTax = Convert.ToDecimal(txtAdv_Bal_Amt.Text);
            }

            //Update final Approval Invoice Status
            spm.InsertInvoice_ApproverDetails_Final("UpdateInvoice_FinalApproval", Convert.ToDouble(hdnInvoiceId.Value), hdn_curnt_Appr_Empcode.Value, Convert.ToInt32(hdn_curnt_Appr_ID.Value), "Approved", Convert.ToString(txtRemarks.Text).Trim(), "", "", iDirectTaxTypeID, dDirectTaxPercentage, dDirectTaxPercentage_Amt, dPayableAmt_WithTax, iDirectTax_DescriptionID, blnIsTDSApplicable, sExpenseId, sExpesesDtlsId, Convert.ToString(txtinvoice_description.Text).Trim(), sItemDtlsId);

            if (ChkSecuritySettlement.Checked)
            {
                if (Convert.ToString(txtSecurityDeposit.Text).Trim() != "")
                {
                    //update the Security Amount change by  account
                    UpdateOn_Final_Security_DepositAmount();
                }
            }

             if (chkIsInvoiceWithoutPO.Checked == false)
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
                            spm.InsertInvoice_CostCeterAmt("Insert_distribute_InvocieAmt_toCostCnter_ByAcc", Convert.ToDouble(hdnInvoiceId.Value), Convert.ToInt32(lstMilestoneAmtCostCenter.SelectedValue), Convert.ToDecimal(txtCostCenterAmt.Text), isrno, Convert.ToDecimal(txtCostCenterAmt.Text));
                            isrno += 1;
                        }
                    }

                }

                #region Advance and Security Diposit Calculation

                Decimal Adv_Settlement_Amt = 0, Invoice_AmtWithout_GST_ACC = 0, Adv_Bal_Amt = 0, SecurityDeposit = 0;
                bool blnIsAdvSecDepositCheck = false;
                if (Convert.ToString(txtAdv_Settlement_Amt.Text).Trim() != "")
                {
                    Adv_Bal_Amt = Convert.ToDecimal(txtAdv_Settlement_Amt.Text);
                }
                if (Convert.ToString(txtSecurityDeposit.Text).Trim() != "")
                {
                    SecurityDeposit = Convert.ToDecimal(txtSecurityDeposit.Text);
                }


                foreach (GridViewRow row in gvInvoiceMilestone_Acc.Rows)
                {
                    DropDownList lstDirectTaxSections_ACC = (DropDownList)row.FindControl("lstDirectTaxSections_ACC");
                    TextBox txtDirectTaxPercentage_ACC = (TextBox)row.FindControl("txtDirectTaxPercentage_ACC");
                    TextBox txtInvoiceAmtWithoutGST_ACC = (TextBox)row.FindControl("txtInvoiceAmtWithoutGST_ACC");
                    string dMilestoneId = Convert.ToString(gvInvoiceMilestone_Acc.DataKeys[row.RowIndex].Values[2]);

                    Invoice_AmtWithout_GST_ACC = Convert.ToDecimal(txtInvoiceAmtWithoutGST_ACC.Text);


                    #region check Addvance settlment is erquired
                    if (ChkAdvSettlement.Checked)
                    {
                        blnIsAdvSecDepositCheck = true;
                        if (Adv_Bal_Amt > Invoice_AmtWithout_GST_ACC)
                        {
                            Adv_Bal_Amt = Math.Abs(Convert.ToDecimal(Adv_Bal_Amt) - Invoice_AmtWithout_GST_ACC);
                            Adv_Settlement_Amt = Invoice_AmtWithout_GST_ACC;
                            Invoice_AmtWithout_GST_ACC = 0;
                        }
                        else
                        {
                            Invoice_AmtWithout_GST_ACC = Math.Abs(Invoice_AmtWithout_GST_ACC - Convert.ToDecimal(Adv_Bal_Amt));
                            Adv_Settlement_Amt = Adv_Bal_Amt;
                            Adv_Bal_Amt = 0;
                        }


                        if (Convert.ToString(hdnGSTAmt.Value).Trim() == "")
                            hdnGSTAmt.Value = "0";

                        Adv_Settlement_Amt = Adv_Settlement_Amt + Convert.ToDecimal(hdnGSTAmt.Value);
                        UpdateOn_Final_TDSPercetage_TDS_Section_ID(Convert.ToInt32(lstDirectTaxSections_ACC.SelectedValue), txtDirectTaxPercentage_ACC.Text, dMilestoneId, Invoice_AmtWithout_GST_ACC, Adv_Settlement_Amt, 0);

                    }
                    #endregion

                    #region check Security Deposit reqiuired
                    if (ChkSecuritySettlement.Checked)
                    {
                        blnIsAdvSecDepositCheck = true;
                        if (SecurityDeposit > 0 && Invoice_AmtWithout_GST_ACC > 0)
                        {
                            Adv_Settlement_Amt = 0;
                            if (SecurityDeposit > Invoice_AmtWithout_GST_ACC)
                            {
                                SecurityDeposit = Math.Abs(Convert.ToDecimal(SecurityDeposit) - Invoice_AmtWithout_GST_ACC);
                                Adv_Settlement_Amt = Invoice_AmtWithout_GST_ACC;
                                Invoice_AmtWithout_GST_ACC = 0;
                            }
                            else
                            {
                                Invoice_AmtWithout_GST_ACC = Math.Abs(Invoice_AmtWithout_GST_ACC - Convert.ToDecimal(SecurityDeposit));
                                Adv_Settlement_Amt = SecurityDeposit;
                                SecurityDeposit = 0;
                            }

                            if (Convert.ToString(hdnGSTAmt.Value).Trim() == "")
                                hdnGSTAmt.Value = "0";

                            Adv_Settlement_Amt = Adv_Settlement_Amt + Convert.ToDecimal(hdnGSTAmt.Value);
                            UpdateOn_Final_TDSPercetage_TDS_Section_ID(Convert.ToInt32(lstDirectTaxSections_ACC.SelectedValue), txtDirectTaxPercentage_ACC.Text, dMilestoneId, Invoice_AmtWithout_GST_ACC, Adv_Settlement_Amt, Adv_Settlement_Amt);

                        }
                    }
                    #endregion

                    //it will execute if no any advance or security deposit payment
                    if (blnIsAdvSecDepositCheck == false)
                    {
                        UpdateOn_Final_TDSPercetage_TDS_Section_ID(Convert.ToInt32(lstDirectTaxSections_ACC.SelectedValue), txtDirectTaxPercentage_ACC.Text, dMilestoneId, Invoice_AmtWithout_GST_ACC, 0, 0);

                    }
                }


                if (ChkAdvSettlement.Checked == true || ChkSecuritySettlement.Checked==true)
                {
                    if (Convert.ToString(txtAdv_Bal_Amt.Text).Trim() != "")
                    {
                        if (Convert.ToDecimal(txtAdv_Bal_Amt.Text) <= 0)
                        {
                            //set Payment Approved Status
                            DataSet dsPOStatus = new DataSet();
                            SqlParameter[] spars = new SqlParameter[2];

                            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                            spars[0].Value = "UpdatePOWO_Milestone_Amt_if_NoPayment"; 

                            spars[1] = new SqlParameter("@InvoiceID", SqlDbType.BigInt);
                            spars[1].Value = Convert.ToDouble(hdnInvoiceId.Value);

                            //spars[2] = new SqlParameter("@invoices_description", SqlDbType.VarChar);
                            //spars[2].Value = Convert.ToString(txtInvocieRemarks.Text).Trim();

                            //string qtype = "UpdateMail";


                            //DataSet dsgoal = new DataSet();
                            //SqlParameter[] spars = new SqlParameter[2];

                            //spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                            //spars[0].Value = qtype;

                            //spars[1] = new SqlParameter("@invoices_description", SqlDbType.VarChar);
                            //spars[1].Value = Convert.ToString(txtInvocieRemarks.Text).Trim();

                            //spm.Insert_Data(spars, "SP_VSCB_CreateInvoice");

                            //dsPOStatus = spm.getDatasetList(spars, "SP_VSCB_CreateInvoice");
                        }
                    }
                }

                
                #endregion

            }

        }


        #region Send Email to next Approver and  if Its Final Approver then send to Invocie Creator

        string strSubject = "";
        if(Convert.ToString(hdnIsFinalApprover.Value).Trim()=="yes")
        {
          strSubject=  "OneHR: Approved Invoice Request- " + Convert.ToString(txtInvoiceNo.Text).Trim();
        }
        else
        {
            if (chkIsInvoiceWithoutPO.Checked)
                strSubject = "OneHR: Request for - Invoice Approval - " + Convert.ToString(txtInvoiceNo.Text).Trim();
            else
                strSubject = "OneHR: Request for - Invoice Approval - " + Convert.ToString(txtInvoiceNo.Text).Trim() + " against " + Convert.ToString(lstTripType.SelectedItem.Text).Trim();

        }
        

        string sApproverEmail_CC = "";

        DataSet dsMilestoneList = new DataSet();
        dsMilestoneList = spm.get_Invoice_Milestone_List(Convert.ToDouble(hdnInvoiceId.Value));
        int ichkWithoutPo = 0;
        if (chkIsInvoiceWithoutPO.Checked)
            ichkWithoutPo = 1;
        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), txtEmpCode.Text, hdnProject_Dept_Name.Value,Convert.ToString(lstPOType.SelectedValue), ichkWithoutPo);

        for (Int32 irow = 0; irow < dsMilestone.Tables[0].Rows.Count; irow++)
        {
            if (Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() != "" && Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() != "Pending" && Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() != Convert.ToString(hdn_curnt_Appr_EmpEmail_ID.Value).Trim())
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

       
        string strInvoiceURL = "";
        strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["approverLink_VSCB"]).Trim() + "?invid=" + hdnInvoiceId.Value).Trim();
        StringBuilder strbuild = new StringBuilder();

        strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:80%'>");
        strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>");

        if (Convert.ToString(hdnIsFinalApprover.Value).Trim() == "yes")
            strbuild.Append("<tr><td colspan='2'> " + hdn_curnt_Appr_Emp_Name.Value + " has approved an invoice with the following details. You can now create Payment Request against this invoice.</td></tr>");
        else
            strbuild.Append("<tr><td colspan='2'> " + hdn_curnt_Appr_Emp_Name.Value + " has approved an invoice with the following details.</td></tr>");

        strbuild.Append("<tr><td style='height:20px'></td></tr>");

         

        strbuild.Append("<tr><td>Invoice No :-</td><td>" + Convert.ToString(txtInvoiceNo.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>Invoice Date :-</td><td>" + Convert.ToString(txtInvoiceDate.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>Invoice Amount (With Tax) :-</td><td>" + Convert.ToString(hdnInvocieAmtWithTax.Value).Trim() + "</td></tr>");
		
		if (chkIsInvoiceWithoutPO.Checked == true)
        {
           // strbuild.Append("<tr><td>Department :-</td><td>" + Convert.ToString(lstProjectDept.SelectedItem.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Vendor Name :-</td><td>" + Convert.ToString(lstVendors.SelectedItem.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(lstProjectDept.SelectedItem.Text).Trim() + "</td></tr>");
        }


        if (Convert.ToString(hdnIsFinalApprover.Value).Trim().ToLower() == "yes")
        {
            if (chkIsInvoiceWithoutPO.Checked == true)
            {
                if (Convert.ToString(lstDirectTax.SelectedValue).Trim() != "1")
                {
                    strbuild.Append("<tr><td>Direct Tax :-</td><td>" + Convert.ToString(lstDirectTax.SelectedItem.Text).Trim() + "</td></tr>");
                    strbuild.Append("<tr><td>Direct Tax(%) :-</td><td>" + Convert.ToString(txtDirectTaxPercentage.Text).Trim() + "</td></tr>");                    
                }
            }
            strbuild.Append("<tr><td>Direct Tax Amount :-</td><td>" + Convert.ToString(txtDirecttaxAmt.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Payable Amount (With Tax) :-</td><td>" + Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() + "</td></tr>");
        }
		if (ChkAdvSettlement.Checked == true)
		{
			strbuild.Append("<tr><td>Advance Settlement Amount :-</td><td>" + Convert.ToString(txtAdv_Settlement_Amt.Text).Trim() + "</td></tr>");
		}
		if (ChkSecuritySettlement.Checked == true)
		{
			strbuild.Append("<tr><td>Security Settlement Amount :-</td><td>" + Convert.ToString(txtSecurityDeposit.Text).Trim() + "</td></tr>");
		}
		if (Convert.ToString(txtAdv_Bal_Amt.Text).Trim() != "" && Convert.ToString(hdnIsFinalApprover.Value).Trim().ToLower() == "yes")
		{
			strbuild.Append("<tr><td>Invoice Payable Amt (Exclude Advance Amt):-</td><td>" + Convert.ToString(txtAdv_Bal_Amt.Text).Trim() + "</td></tr>");
		}
		strbuild.Append("<tr><td style='height:20px'></td></tr>");

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
            strbuild.Append("<tr><td>PO/ WO No :-</td><td>" + Convert.ToString(lstTripType.SelectedItem.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>PO/ WO Type :-</td><td>" + Convert.ToString(lstPOType.SelectedItem.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>PO/ WO Date :-</td><td>" + Convert.ToString(txtFromdate.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Vendor Name :-</td><td>" + Convert.ToString(txtVendor.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(txtCostCenter.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>PO/ WO Amount (With GST) :-</td><td>" + Convert.ToString(txtPOWOAmt.Text).Trim() + "</td></tr>");
			// strbuild.Append("<tr><td>Department :-</td><td>" + Convert.ToString(txtProject.Text).Trim() + "</td></tr>");
			if (ChkAdvSettlement.Checked == true)
			{
				strbuild.Append("<tr><td>Payment Advance Amount :-</td><td>" + Convert.ToString(txt_Pay_Adv_Amt.Text).Trim() + "</td></tr>");
				strbuild.Append("<tr><td>Payment Advance Settlement :-</td><td>" + Convert.ToString(txt_Pay_Adv_Settlement.Text).Trim() + "</td></tr>");
				strbuild.Append("<tr><td>Payment Advance Bal Amount :-</td><td>" + Convert.ToString(txt_Pay_Adv_Bal_Amt.Text).Trim() + "</td></tr>");

			}


		}
                

        if (Convert.ToString(hdnIsFinalApprover.Value).Trim() != "yes")
        {
            strbuild.Append("<tr><td style='height:40px'></td></tr>");
            strbuild.Append("<tr><td style='height:20px'><a href='" + strInvoiceURL + "'> Please Click here for your action </a></td></tr>");
        }

        strbuild.Append("</table>  <br /><br />");

        strbuild.Append(strbuild_Approvers);

        if (Convert.ToString(hdnIsFinalApprover.Value).Trim().ToLower() == "yes")
        {
            spm.sendMail_VSCB(hdnInvoiceCreator_Email.Value, strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);
        }
        else
        {
            spm.sendMail_VSCB(hdn_next_Appr_EmpEmail_ID.Value, strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);
        }


        #endregion

        Response.Redirect("VSCB_Inboxinvoice.aspx");

        /* else
         {
             lblmessage.Text = "Invoice creation failed. Please contact to admin.";
             return;

       }
          */

        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        if(Convert.ToString(txtRemarks.Text).Trim()=="")
        {
            lblmessage.Text = "Please mention the comment before rejecting the Invoice";
            return;
        }

        //if (Convert.ToString(txtinvoice_description.Text).Trim() == "")
        //{
        //    lblmessage.Text = "Please Enter Invoice Description.";
        //    return;
        //}

        spm.InsertInvoice_ApproverDetails("UpdateInvoice_Reject", Convert.ToDouble(hdnInvoiceId.Value), hdn_curnt_Appr_Empcode.Value, Convert.ToInt32(hdn_curnt_Appr_ID.Value), "Reject", Convert.ToString(txtRemarks.Text).Trim(), "", "", Convert.ToString(txtinvoice_description.Text).Trim());


        #region Send Email to next Approver

        DataSet dsMilestoneList = new DataSet();
        dsMilestoneList = spm.get_Invoice_Milestone_List(Convert.ToDouble(hdnInvoiceId.Value));

        string sApproverEmail_CC = "";

        int ichkWithoutPo = 0;
        if (chkIsInvoiceWithoutPO.Checked)
            ichkWithoutPo = 1;

        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), txtEmpCode.Text, hdnProject_Dept_Name.Value,Convert.ToString(lstPOType.SelectedValue), ichkWithoutPo);

        for (Int32 irow = 0; irow < dsMilestone.Tables[0].Rows.Count; irow++)
        {
            if (Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() != ""  && Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() != Convert.ToString(hdn_curnt_Appr_EmpEmail_ID.Value).Trim())
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

        string strSubject = "";
        if (chkIsInvoiceWithoutPO.Checked)
            strSubject = "OneHR: Invoice - " + Convert.ToString(txtInvoiceNo.Text).Trim() + "Rejected";
        else
            strSubject = "OneHR: Invoice - " + Convert.ToString(txtInvoiceNo.Text).Trim() + " Rejected against " + Convert.ToString(lstTripType.SelectedItem.Text).Trim();


        string strInvoiceURL = "";
        strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["approverLink_VSCB"]).Trim() + "?invid=" + hdnInvoiceId.Value).Trim();
        StringBuilder strbuild = new StringBuilder();

        strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:80%'>");
        strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>"); 
        strbuild.Append("<tr><td colspan='2'> This is to inform you that "+ hdn_curnt_Appr_Emp_Name.Value + " has rejected an invoice with the following details.</td></tr>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>");

        if (chkIsInvoiceWithoutPO.Checked == true)
        {
            //strbuild.Append("<tr><td>Department :-</td><td>" + Convert.ToString(lstProjectDept.SelectedItem.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(lstProjectDept.SelectedItem.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Vendor Name :-</td><td>" + Convert.ToString(lstVendors.SelectedItem.Text).Trim() + "</td></tr>");
        }
       
        strbuild.Append("<tr><td>Invoice No :-</td><td>" + Convert.ToString(txtInvoiceNo.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>Invoice Date :-</td><td>" + Convert.ToString(txtInvoiceDate.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>Invoice Amount (With GST):-</td><td>" + Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() + "</td></tr>");
		if (ChkAdvSettlement.Checked == true)
		{
			strbuild.Append("<tr><td>Advance Settlement Amount :-</td><td>" + Convert.ToString(txtAdv_Settlement_Amt.Text).Trim() + "</td></tr>");
			//strbuild.Append("<tr><td>Invoice Payable Amt (Exclude Advance Amt) :-</td><td>" + Convert.ToString(txtAdv_Bal_Amt.Text).Trim() + "</td></tr>");
		}
		if (ChkSecuritySettlement.Checked == true)
		{
			strbuild.Append("<tr><td>Security Settlement Amount :-</td><td>" + Convert.ToString(txtSecurityDeposit.Text).Trim() + "</td></tr>");
		}
		if (Convert.ToString(txtAdv_Bal_Amt.Text).Trim() != "")
		{
			strbuild.Append("<tr><td>Invoice Payable Amt (Exclude Advance Amt):-</td><td>" + Convert.ToString(txtAdv_Bal_Amt.Text).Trim() + "</td></tr>");
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

            strbuild.Append("<tr><td>PO/ WO No :-</td><td>" + Convert.ToString(lstTripType.SelectedItem.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>PO/ WO Type :-</td><td>" + Convert.ToString(lstPOType.SelectedItem.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>PO/ WO Date :-</td><td>" + Convert.ToString(txtFromdate.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Vendor Name :-</td><td>" + Convert.ToString(txtVendor.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(txtCostCenter.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>PO/ WO Amount (With GST):-</td><td>" + Convert.ToString(txtPOWOAmt.Text).Trim() + "</td></tr>");
			if (ChkAdvSettlement.Checked == true)
			{
				strbuild.Append("<tr><td>Payment Advance Amount :-</td><td>" + Convert.ToString(txt_Pay_Adv_Amt.Text).Trim() + "</td></tr>");
				strbuild.Append("<tr><td>Payment Advance Settlement :-</td><td>" + Convert.ToString(txt_Pay_Adv_Settlement.Text).Trim() + "</td></tr>");
				strbuild.Append("<tr><td>Payment Advance Bal Amount :-</td><td>" + Convert.ToString(txt_Pay_Adv_Bal_Amt.Text).Trim() + "</td></tr>");

			}
			// strbuild.Append("<tr><td>Department :-</td><td>" + Convert.ToString(txtProject.Text).Trim() + "</td></tr>");



		}
       
        strbuild.Append("</table>  <br /><br />");

        strbuild.Append(strbuild_Approvers);

        spm.sendMail_VSCB(hdnInvoiceCreator_Email.Value, strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);


        #endregion

        Response.Redirect("VSCB_Inboxinvoice.aspx");

    }

    

    protected void txtAmtWithOutTax_TextChanged(object sender, EventArgs e)
    {
        Decimal dMilestoneBalAmt = 0;
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
        if (Convert.ToString(txtMilestoneBalanceAmt_Invoice.Text).Trim() != "")
        {
            dMilestoneBalAmt = Math.Round(Convert.ToDecimal(txtMilestoneBalanceAmt_Invoice.Text), 2);
        }

        #endregion

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

        txtAmtWithTax_Invoice.Text = Convert.ToString(Math.Round(rCGST + rSGST + rIGST + Convert.ToDecimal(txtAmtWithOutTax.Text), 2)).Trim();

        if (Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() != "")
        {
            if (Convert.ToDecimal(txtAmtWithTax_Invoice.Text) > dMilestoneBalAmt)
            {
                txtAmtWithTax_Invoice.Text = "";
                lblmessage.Text = "Amount (With Tax ) is grater than Milestone balance Amount not allowed. Please contact to admin.";
                return;
            }
        }
    }

    protected void btnCorrection_Click(object sender, EventArgs e)
    {

        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        if(Convert.ToString(txtRemarks.Text).Trim()=="")
        {
            lblmessage.Text = "Please mention the comment before Send for Correction the Invoice";
            return;
        }

        //if (Convert.ToString(txtinvoice_description.Text).Trim() == "")
        //{
        //    lblmessage.Text = "Please Enter Invoice Description.";
        //    return;
        //}

        spm.InsertInvoice_ApproverDetails("UpdateInvoice_Correction", Convert.ToDouble(hdnInvoiceId.Value), hdn_curnt_Appr_Empcode.Value, Convert.ToInt32(hdn_curnt_Appr_ID.Value), "Correction", Convert.ToString(txtRemarks.Text).Trim(), "", "", Convert.ToString(txtinvoice_description.Text).Trim());


        #region Send Email to next Approver

        DataSet dsMilestoneList = new DataSet();
        dsMilestoneList = spm.get_Invoice_Milestone_List(Convert.ToDouble(hdnInvoiceId.Value));

       
        string sApproverEmail_CC = "";
        int ichkWithoutPo = 0;
        if (chkIsInvoiceWithoutPO.Checked)
            ichkWithoutPo = 1;
        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), txtEmpCode.Text, hdnProject_Dept_Name.Value,Convert.ToString(lstPOType.SelectedValue), ichkWithoutPo);

        for (Int32 irow = 0; irow < dsMilestone.Tables[0].Rows.Count; irow++)
        {
            if (Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() != "" && Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() != Convert.ToString(hdn_curnt_Appr_EmpEmail_ID.Value).Trim())
            {
                if (Convert.ToString(sApproverEmail_CC).Trim() == "")
                    sApproverEmail_CC = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                else
                    sApproverEmail_CC =  Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() + ";" + sApproverEmail_CC;
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

        string strSubject = "";

        if (chkIsInvoiceWithoutPO.Checked)
            strSubject = "OneHR: Invoice - " + Convert.ToString(txtInvoiceNo.Text).Trim() + " sent back for correction";
        else
            strSubject = "OneHR: Invoice - " + Convert.ToString(txtInvoiceNo.Text).Trim() + " sent back for correction against " + Convert.ToString(lstTripType.SelectedItem.Text).Trim();

        //  = "Invoice - " + Convert.ToString(txtInvoiceNo.Text).Trim() + " send back for correction";

        string strInvoiceURL = "";
        strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["CorrectionLink_VSCB"]).Trim() + "?invid=" + hdnInvoiceId.Value).Trim() + "&mngexp=1";
        StringBuilder strbuild = new StringBuilder();

        strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:80%'>");
        strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>");
        strbuild.Append("<tr><td colspan='2'> " + hdn_curnt_Appr_Emp_Name.Value + " has Sent back the invoice for correction. Please correct the same as instructed and resend for approval.</td></tr>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>");


        if (chkIsInvoiceWithoutPO.Checked == true)
        {
            //strbuild.Append("<tr><td>Department :-</td><td>" + Convert.ToString(lstProjectDept.SelectedItem.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(lstProjectDept.SelectedItem.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Vendor Name :-</td><td>" + Convert.ToString(lstVendors.SelectedItem.Text).Trim() + "</td></tr>");
        }
                
        strbuild.Append("<tr><td>Invoice No :-</td><td>" + Convert.ToString(txtInvoiceNo.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>Invoice Date :-</td><td>" + Convert.ToString(txtInvoiceDate.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>Invoice Amount (With GST) :-</td><td>" + Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() + "</td></tr>");
		if (ChkAdvSettlement.Checked == true)
		{
			strbuild.Append("<tr><td>Advance Settlement Amount :-</td><td>" + Convert.ToString(txtAdv_Settlement_Amt.Text).Trim() + "</td></tr>");
			//strbuild.Append("<tr><td>Invoice Payable Amt (Exclude Advance Amt) :-</td><td>" + Convert.ToString(txtAdv_Bal_Amt.Text).Trim() + "</td></tr>");
		}
		if (ChkSecuritySettlement.Checked == true)
		{
			strbuild.Append("<tr><td>Security Settlement Amount :-</td><td>" + Convert.ToString(txtSecurityDeposit.Text).Trim() + "</td></tr>");
		}
		if (Convert.ToString(txtAdv_Bal_Amt.Text).Trim() != "")
		{
			strbuild.Append("<tr><td>Invoice Payable Amt (Exclude Advance Amt):-</td><td>" + Convert.ToString(txtAdv_Bal_Amt.Text).Trim() + "</td></tr>");
		}
		strbuild.Append("<tr><td style='height:20px'></td></tr>");

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
            strbuild.Append("<tr><td>PO/ WO No :-</td><td>" + Convert.ToString(lstTripType.SelectedItem.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>PO/ WO Type :-</td><td>" + Convert.ToString(lstPOType.SelectedItem.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>PO/ WO Date :-</td><td>" + Convert.ToString(txtFromdate.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Vendor Name :-</td><td>" + Convert.ToString(txtVendor.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(txtCostCenter.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>PO/ WO Amount (With GST) :-</td><td>" + Convert.ToString(txtPOWOAmt.Text).Trim() + "</td></tr>");
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

        spm.sendMail_VSCB(hdnInvoiceCreator_Email.Value, strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);


        #endregion

        Response.Redirect("VSCB_Inboxinvoice.aspx");


    }

    

    protected void gvuploadedFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void lstDirectTax_SelectedIndexChanged(object sender, EventArgs e)
    {

        txtDirectTaxPercentage.Text = "";
        txtDirecttaxAmt.Text = "";
        txtTDSTCS_Description.Text = "";
        
        spnLDCApplicable.Visible = false;
        chkLDC_Applicable.Visible = false;

        if (Convert.ToString(lstDirectTaxSections.SelectedValue).Trim() != "0")
        {
            spnLDCApplicable.Visible = true;
            chkLDC_Applicable.Visible = true;
        }
        get_DirectTaxTypeList();
        get_DirectTax_Percentage();
        Calculate_DirectTax_Amount();
    }

    protected void txtDirectTaxPercentage_TextChanged(object sender, EventArgs e)
    {
         
         
        Calculate_DirectTax_Amount();
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
            lstExpense_details.DataTextField = "exp_detail_Name";
            lstExpense_details.DataValueField = "exp_dtl_id";
            lstExpense_details.DataBind();
            lstExpense_details.Items.Insert(0, new ListItem("Select Expese Details", "0"));
        }
    }

    private void Calculate_DirectTax_Amount()
    {
       

        if (Convert.ToString(lstDirectTax.SelectedValue).Trim() != "0" && Convert.ToString(lstDirectTax.SelectedValue).Trim() != "1")
        {
            //if (Convert.ToString(txtDirectTaxPercentage.Text).Trim() == "")
            //{
            //    lblmessage.Text = "Please enter Direct Tax Percentage.";
            //    return;
            //}
            if (Convert.ToString(txtDirectTaxPercentage.Text).Trim() != "")
            {
                if (Convert.ToDouble(txtDirectTaxPercentage.Text) > 100)
                {
                    txtDirectTaxPercentage.Text = "";
                    lblmessage.Text = "Please enter correct Direct Tax Percentage.";
                    return;
                }
                string[] strdate;
                strdate = Convert.ToString(txtDirectTaxPercentage.Text).Trim().Split('.');
                if (strdate.Length > 2)
                {
                    txtDirectTaxPercentage.Text = "";
                    lblmessage.Text = "Please enter correct Direct Tax Percentage.";
                    return;
                }
            }
        }

        Decimal dDirectTaxPercentage_Amt = 0;
        Decimal dDirectTaxPercentage = 0;
        if (Convert.ToString(txtDirectTaxPercentage.Text).Trim() != "")
            dDirectTaxPercentage = Convert.ToDecimal(txtDirectTaxPercentage.Text);

        Decimal rCGST = 0;
        Decimal rSGST = 0;
        Decimal rIGST = 0;

        #region GST Calculation Not required
        //if (Convert.ToString(txtCGST_Per.Text).Trim() != "")
        //{
        //    rCGST = Math.Round(Convert.ToDecimal(txtCGST_Per.Text) * Convert.ToDecimal(txtAmtWithOutTax.Text) / 100, 2);
        //}
        //if (Convert.ToString(txtSGST_Per.Text).Trim() != "")
        //{
        //    rSGST = Math.Round(Convert.ToDecimal(txtSGST_Per.Text) * Convert.ToDecimal(txtAmtWithOutTax.Text) / 100, 2);
        //}
        //if (Convert.ToString(txtIGST_Per.Text).Trim() != "")
        //{
        //    rIGST = Math.Round(Convert.ToDecimal(txtIGST_Per.Text) * Convert.ToDecimal(txtAmtWithOutTax.Text) / 100, 2);
        //}
        #endregion  

        if (Convert.ToString(txtCGST_Amt.Text).Trim() != "")
        {
            rCGST = Math.Round(Convert.ToDecimal(txtCGST_Amt.Text), 2);
        }
        if (Convert.ToString(txtSGST_Amt.Text).Trim() != "")
        {
            rSGST = Math.Round(Convert.ToDecimal(txtSGST_Amt.Text), 2);
        }
        if (Convert.ToString(txtIGST_Amt.Text).Trim() != "")
        {
            rIGST = Math.Round(Convert.ToDecimal(txtIGST_Amt.Text), 2);
        }
        dDirectTaxPercentage_Amt = Math.Round(dDirectTaxPercentage * Convert.ToDecimal(txtAmtWithOutTax.Text) / 100, 2);

        txtDirecttaxAmt.Text = Convert.ToString(dDirectTaxPercentage_Amt).Trim();
        txtAmtWithTax_Invoice.Text = Convert.ToString((rCGST + rSGST + rIGST + Convert.ToDecimal(txtAmtWithOutTax.Text) - dDirectTaxPercentage_Amt));


        #region format Currency

        DataSet dtPOWODetails = new DataSet(); 
        decimal dinvoiceAmt = 0, dCGSTAmt = 0, dSGSTAmt = 0, dIGSTAmt = 0, dInvoiceAmtWithtax = 0;

        if (Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() != "")
            dinvoiceAmt = Convert.ToDecimal(txtAmtWithTax_Invoice.Text);

        if (Convert.ToString(txtDirecttaxAmt.Text).Trim() != "")
            dCGSTAmt = Convert.ToDecimal(txtDirecttaxAmt.Text); 

        dtPOWODetails = spm.getFormated_Amount("get_formated_Invoice_Amount", dinvoiceAmt, dCGSTAmt, dSGSTAmt, dIGSTAmt, dInvoiceAmtWithtax);


        if (dtPOWODetails.Tables[0].Rows.Count > 0)
        {
            txtAmtWithTax_Invoice.Text = Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["invoiceAmt"]).Trim();
            txtDirecttaxAmt.Text = Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["cgstAmt"]).Trim(); 
        }


        #endregion

    }

    private void Calculate_DirectTax_Amount_ACC()
    {

 

        Decimal dDirectTaxPercentage_Amt = 0; 
        Decimal dDirectTaxPercentage = 0;
		Decimal SecurityDeposit = 0,Adv_Settlement_Amt = 0, InvoiceAmtWithoutGST_ACC=0;
		Decimal rCGST = 0;
        Decimal rSGST = 0;
        Decimal rIGST = 0;
		Decimal Adv_Settlment_Amt = 0;
        decimal dGSTAmt = 0;


		if (Convert.ToString(txtCGST_Amt.Text).Trim() != "")
        {
            rCGST = Math.Round(Convert.ToDecimal(txtCGST_Amt.Text), 2);
        }
        if (Convert.ToString(txtSGST_Amt.Text).Trim() != "")
        {
            rSGST = Math.Round(Convert.ToDecimal(txtSGST_Amt.Text), 2);
        }
        if (Convert.ToString(txtIGST_Amt.Text).Trim() != "")
        {
            rIGST = Math.Round(Convert.ToDecimal(txtIGST_Amt.Text), 2);
        }
		int j = 0;

		//if (Convert.ToString(hdnAdvance_IsTDC.Value) != "")
        if(ChkAdvSettlement.Checked==true)
		{
			if (Convert.ToString(txtAdv_Settlement_Amt.Text).Trim() != "")
			{
                Adv_Settlment_Amt = Convert.ToDecimal(txtAdv_Settlement_Amt.Text);

                if (Convert.ToString(hdnGSTAmt.Value).Trim() != "")
                    dGSTAmt = Convert.ToDecimal(hdnGSTAmt.Value); 

            }
		}
		foreach (GridViewRow row in gvInvoiceMilestone_Acc.Rows)
        {
            DropDownList lstDirectTaxSections_ACC = (DropDownList)row.FindControl("lstDirectTaxSections_ACC");
            TextBox txtDirectTaxPercentage_ACC = (TextBox)row.FindControl("txtDirectTaxPercentage_ACC");
            TextBox txtInvoiceAmtWithoutGST_ACC = (TextBox)row.FindControl("txtInvoiceAmtWithoutGST_ACC");
            if (Convert.ToString(lstDirectTaxSections_ACC.SelectedValue).Trim() != "0" && Convert.ToString(lstDirectTaxSections_ACC.SelectedValue).Trim() != "1")
            {
                if (Convert.ToString(txtDirectTaxPercentage_ACC.Text).Trim() != "")
                    dDirectTaxPercentage = Convert.ToDecimal(txtDirectTaxPercentage_ACC.Text);
				
                if (Convert.ToString(txtInvoiceAmtWithoutGST_ACC.Text).Trim() != "")
                {
					InvoiceAmtWithoutGST_ACC = Convert.ToDecimal(txtInvoiceAmtWithoutGST_ACC.Text);

					if (Adv_Settlment_Amt > InvoiceAmtWithoutGST_ACC)
					{

                        Adv_Settlment_Amt = Math.Abs(Convert.ToDecimal(Adv_Settlment_Amt) - InvoiceAmtWithoutGST_ACC);
						InvoiceAmtWithoutGST_ACC = 0;
					}
					else
					{
						InvoiceAmtWithoutGST_ACC = Math.Abs(InvoiceAmtWithoutGST_ACC - Convert.ToDecimal(Adv_Settlment_Amt));
                        Adv_Settlment_Amt = 0;
					}

					dDirectTaxPercentage_Amt += Math.Round(dDirectTaxPercentage * Convert.ToDecimal(InvoiceAmtWithoutGST_ACC) / 100, 2);
                }
            }
 
        }

       

        txtDirecttaxAmt.Text = Convert.ToString(dDirectTaxPercentage_Amt).Trim();
        txtDirecttaxAmt_ACC_Display.Text = Convert.ToString(dDirectTaxPercentage_Amt).Trim();

         
         txtAmtWithTax_Invoice.Text = Convert.ToString((rCGST + rSGST + rIGST + Convert.ToDecimal(txtAmtWithOutTax.Text) - dDirectTaxPercentage_Amt) - dGSTAmt);
        
		int J = 0;
		if (ChkAdvSettlement.Checked == true)
		{
			Adv_Settlement_Amt = Convert.ToDecimal(txtAdv_Settlement_Amt.Text);
			J = 1;
		}
		if (ChkSecuritySettlement.Checked == true)
		{
			SecurityDeposit = Convert.ToDecimal(txtSecurityDeposit.Text);
			J = 1;
		}
		if (J == 1)
		{
			DataSet dtAdvDetails = new DataSet();
			decimal Adv_Bal_Amt1 = 0;
            
            Adv_Bal_Amt1 = Convert.ToDecimal(Convert.ToDecimal(txtAmtWithTax_Invoice.Text) - Convert.ToDecimal(Adv_Settlement_Amt) - Convert.ToDecimal(SecurityDeposit));

            //if (Adv_Bal_Amt1 < 0)
            //    Adv_Bal_Amt1 = 0;

            dtAdvDetails = spm.getFormated_Amount("get_formated_Invoice_Amount", Adv_Bal_Amt1, 0, 0, 0, 0);
			if (dtAdvDetails.Tables[0].Rows.Count > 0)
			{
				txtAdv_Bal_Amt.Text = Convert.ToString(dtAdvDetails.Tables[0].Rows[0]["invoiceAmt"]).Trim();
			}
		}
		else
		{
			txtAdv_Bal_Amt.Text = "";

		}
		#region format Currency

		DataSet dtPOWODetails = new DataSet();
        decimal dinvoiceAmt = 0, dCGSTAmt = 0, dSGSTAmt = 0, dIGSTAmt = 0, dInvoiceAmtWithtax = 0;

        if (Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() != "")
            dinvoiceAmt = Convert.ToDecimal(txtAmtWithTax_Invoice.Text);

        if (Convert.ToString(txtDirecttaxAmt.Text).Trim() != "")
            dCGSTAmt = Convert.ToDecimal(txtDirecttaxAmt.Text);

        dtPOWODetails = spm.getFormated_Amount("get_formated_Invoice_Amount", dinvoiceAmt, dCGSTAmt, dSGSTAmt, dIGSTAmt, dInvoiceAmtWithtax);


        if (dtPOWODetails.Tables[0].Rows.Count > 0)
        {
            txtAmtWithTax_Invoice.Text = Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["invoiceAmt"]).Trim();
            txtDirecttaxAmt.Text = Convert.ToString(dtPOWODetails.Tables[0].Rows[0]["cgstAmt"]).Trim();
            txtDirecttaxAmt_ACC_Display.Text = txtDirecttaxAmt.Text;
        }


        #endregion

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
        txtProject.Text = "";
        lstPOType.SelectedValue = "0";

    }

    public void clear_Milestone_Cntrls()
    { 
        hdnSrno.Value = ""; 

    }
 

    public void GetPODetails_List()
    {
        DataTable dtPOWODetails = new DataTable();
        dtPOWODetails = spm.get_ALLPOWOList(txtEmpCode.Text);
        if (dtPOWODetails.Rows.Count > 0)
        {
            lstTripType.DataSource = dtPOWODetails;
            lstTripType.DataTextField = "PONumber";
            lstTripType.DataValueField = "POID";
            lstTripType.DataBind();
            lstTripType.Items.Insert(0, new ListItem("Select PO/WO Number", "0"));

        }
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
     

    public void get_PWODetails_MilestonesList()
    { 

        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.getMilestone_list_CreateInvoice(txtEmpCode.Text, Convert.ToDouble(lstTripType.SelectedValue));

        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
             
            hdnPOWOID.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POID"]).Trim();
            txtFromdate.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PODate"]);
            txtProject.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Project_Name"]).Trim();
            txtPOStatus.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PyamentStatus"]).Trim();            
            hdnPOTypeId.Value= Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTypeID"]).Trim();
            txtPOTitle.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTitle"]).Trim();
            txtPOTitle.ToolTip = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTitle"]).Trim();
            txtVendor.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Name"]).Trim();
            txtGSTIN_No.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["GSTIN_NO"]).Trim();
            txtPOWOAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POWO_T_BaseAmt"]).Trim();
           
            lstPOType.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTypeID"]).Trim();
            txtCostCenter.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CostCentre"]).Trim();
             
            hdnCompCode.Value =Convert.ToString(dsMilestone.Tables[0].Rows[0]["Comp_Code"]).Trim();  
            hdnVendorId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["VendorID"]).Trim();   

           
            lstPOType.Enabled = false;
                
            dgTravelRequest.DataSource = null;
            dgTravelRequest.DataBind();
             

            if (dsMilestone.Tables[1].Rows.Count > 0)
            {
                dgTravelRequest.DataSource = dsMilestone.Tables[1];
                dgTravelRequest.DataBind();

            }
        }
    }

    public void get_InvoiceDetails_MilestonesList_Approval()
    {

        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.getInvoicedetails_edit_View(txtEmpCode.Text, Convert.ToDouble(hdnInvoiceId.Value));
        txtIsSecurity_DepositInvoice.Text = "0";
        Boolean bIsSecurityDepositInvoice = false;
        
        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
            chkIsInvoiceWithoutPO.Checked = false;

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["IsSecurity_Deposit_Invoice"]) != "")
            {
                bIsSecurityDepositInvoice = Convert.ToBoolean(dsMilestone.Tables[0].Rows[0]["IsSecurity_Deposit_Invoice"]);
            }

            if (Convert.ToBoolean(dsMilestone.Tables[0].Rows[0]["IsInvoiceWithPO"]) == false)
            {
                chkIsInvoiceWithoutPO.Checked = true;
                chkIsInvoiceWithoutPO.Checked = true;
                lblIsInvoiceWithPO.Visible = true;
                lstProjectDept.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Prj_Dept_id"]).Trim();
                lstVendors.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["VendorId"]).Trim();
                lstCostCenter.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Prj_Dept_id"]).Trim();
                txtInvocieRemarks.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Invoice_Remarks"]).Trim();
                lstDisplayPOTypes.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTypeID"]).Trim();
                lstDisplayPOTypes.Enabled = false;
                
            }

            CheckIs_Invoice_Wthout_POWO();

            chkIsInvoiceWithoutPO.Enabled = false; 
            if (chkIsInvoiceWithoutPO.Checked == true)
            {
                lstProjectDept.Enabled = false;
                lstVendors.Enabled = false;
                liGST_Per_1.Visible = true;
                liGST_Per_2.Visible = true;
                liGST_Per_3.Visible = true;

                liGst_per_blank_1.Visible = false;
                liGst_per_blank_2.Visible = false;
                liGst_per_blank_1.Visible = false;
                invoice_description.Visible=false;

            }
            else
            {
                liGST_Per_1.Visible = false;
                liGST_Per_2.Visible = false;
                liGST_Per_3.Visible = false;

                liGst_per_blank_1.Visible = true;
                liGst_per_blank_2.Visible = true;
                liGst_per_blank_3.Visible = true;

                txtCostCenter.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Costcenter"]).Trim();
                txtCurrency.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CurName"]);
                txtPOWOSettelmentAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_SettelmentAmt"]).Trim();

                if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["powo_signCopy"]).Trim() != "")
                {
                    lnkfile_PO.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["powo_signCopy"]).Trim();
                    hdnSingPOCopyFileName.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["powo_signCopy"]).Trim();
                    spnPOWOSignCopy.Visible = true;
                    lnkfile_PO.Visible = true;
                }
                txtTriptype.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PONumber"]).Trim();

            }


            hdnProject_Dept_Id.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Prj_Dept_id"]).Trim();
            
            hdnPOWOID.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POID"]).Trim();
            hdnMilestoneID.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MstoneID"]).Trim();
            txtFromdate.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PODate"]);
            txtProject.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Project_Name"]).Trim();
            hdnProject_Dept_Name.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Project_Name"]).Trim();
            txtPOStatus.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POWOStatus"]).Trim();
            txtBalanceAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Bal_Amt"]).Trim();
            txtPaidAmr.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Paid_Amt"]).Trim();
            txtPoPaidAmt_WithOutDT.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POPiadAmount_withoutDT"]).Trim();
            hdnPOTypeId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTypeID"]).Trim();
            txtPOTitle.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTitle"]).Trim();
            txtPOTitle.ToolTip = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTitle"]).Trim();
            txtVendor.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["vendorname"]).Trim();
            txtGSTIN_No.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["GSTIN_NO"]).Trim();
            txtPOWOAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POWOAmt"]).Trim();
            txtBasePOWOWAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POWO_T_BaseAmt"]).Trim();

            lstPOType.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTypeID"]).Trim();
            lstTripType.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POID"]).Trim();
            txtDiretTaxAmtPOWO.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["DirectTaxCollection_Amt"]).Trim();
            

            hdnCompCode.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Comp_Code"]).Trim();
            hdnVendorId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["VendorID"]).Trim();


            txtMilestoneName_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MilestoneName"]).Trim();
            txtMilestoneAmt_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MilestoneAmt"]).Trim();
            txtMilestoneBalanceAmt_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["BalanceMilestoneAmt"]).Trim();             
            txtInvoiceNo.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["InvoiceNo"]).Trim();
            txtInvoiceDate.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["InvoiceDate"]).Trim();
            txtAmtWithOutTax.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["AmtWithoutTax"]).Trim();
            txtAmtWithTax_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["AmtWithTax"]).Trim();
            hdnInvocieAmtWithTax.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["AmtWithTax"]).Trim();
            txtCGST_Per.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Per"]).Trim();
            txtSGST_Per.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Per"]).Trim();
            txtIGST_Per.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Per"]).Trim();

            txtCGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Amt"]).Trim();
            txtSGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Amt"]).Trim();
            txtIGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Amt"]).Trim();
            txtinvoice_description.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["invoices_description"]).Trim();

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["IsSecurity_Deposit_Invoice"]).Trim() != "")
                txtIsSecurity_DepositInvoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["IsSecurity_Deposit_Invoice"]).ToLower();


            lnkfile_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Invoice_File"]).Trim();
            lnkfile_Invoice.Visible = true;

            hdnInvoiceCreator_Name.Value= Convert.ToString(dsMilestone.Tables[0].Rows[0]["invoiceCreatorName"]).Trim();
            hdnInvoiceCreator_Email.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["invoiceCreatorEmail"]).Trim();
            txtRemarks.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CurntApproverRemarks"]).Trim();
            txtinvoice_description.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["invoices_description"]).Trim();

            hdnInvoiceApprovalStatus.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["status_id"]).Trim();
            //if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["InvoiceApprovalStatus"]).Trim() == "Approved")
            //{
            //    trvl_btnSave.Visible = false;
            //}
            lstTripType.Enabled = false;
            lstPOType.Enabled = false;

			
			if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["status_id"]).Trim() == "2")
            { 
                txtDirecttaxAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["DirectTax_Amount"]).Trim();
                txtDirectTaxPercentage.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["DirectTax_Percentage"]).Trim();
                lstDirectTaxSections.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["TDS_Descript_ID"]).Trim();
                lstDirectTax.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["DirectTax_TypeID"]).Trim();
                txtTDSTCS_Description.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["TDS_Description"]).Trim();

                txtDirecttaxAmt_ACC_Display.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["DirectTax_Amount"]).Trim(); 
            }

            if (chkIsInvoiceWithoutPO.Checked == false)
            {
                Check_Advance_Payment_POWO();
            }

            if (dsMilestone.Tables[8].Rows.Count > 0)
			{
				 
				decimal AdvAmt = 0, SecurityAmt = 0, Advance_TDS_Amt=0, dGSTAmt=0;
				int I = 0;
				DataRow[] result = dsMilestone.Tables[8].Select("AdvancePayTypeID = 1");
				foreach (DataRow row in result)
				{
					//string stuname =Convert.ToString(row["AdvancePayTypeID"].ToString());
					txtAdv_Settlement_Amt.Text = Convert.ToString(row["ADSettlement_Amt"].ToString());
                    dGSTAmt = Convert.ToDecimal(row["AD_Settlment_GSTAmt"]);
                    Advance_TDS_Amt = Convert.ToDecimal(row["DirectTax_Amount"].ToString());
					AdvAmt = Convert.ToDecimal(row["ADSettlement_Amt"].ToString());
					ChkAdvSettlement.Checked = true;
					ChkAdvSettlement.Visible = true;
					DAdvSet.Visible = true;
					SAADV_Amt.Visible = true;
					DAdvSet1.Visible = true;
					//ADVtax.Visible = true;
					I = ++I;
				}
				DataRow[] result2 = dsMilestone.Tables[8].Select("AdvancePayTypeID = 2");
				foreach (DataRow row in result2)
				{
					//string stuname = Convert.ToString(row["AdvancePayTypeID"].ToString());
					txtSecurityDeposit.Text = Convert.ToString(row["ADSettlement_Amt"].ToString());
                    dGSTAmt = Convert.ToDecimal(row["AD_Settlment_GSTAmt"]);
                    Advance_TDS_Amt += Convert.ToDecimal(row["DirectTax_Amount"].ToString());
					SecurityAmt = Convert.ToDecimal(row["ADSettlement_Amt"].ToString());
					ChkSecuritySettlement.Checked = true;
					ChkSecuritySettlement.Visible = true;
					SASecurity.Visible = true;
					SAADV_Amt.Visible = true;
					DAdvSet2.Visible = true;
					I = ++I;
                    if (Convert.ToString(txtEmpCode.Text).Trim() == "99999999")
                    {
                        txtSecurityDeposit.Enabled = true;
                        //invoice_description.Enabled = true;
                    }

                }
				txt_Advance_TDS_Amt.Text = Convert.ToString(Advance_TDS_Amt);
				if (I == 2)
				{
					//DAdvSet4.Visible = false;
					DAdvSet5.Visible = false;
					//ADVtax.Visible = false;
				}
				DataSet dtAdvDetails = new DataSet();
				decimal Adv_Bal_Amt = 0;
				Adv_Bal_Amt = Convert.ToDecimal(Convert.ToDecimal(txtAmtWithTax_Invoice.Text) - Convert.ToDecimal(AdvAmt) - Convert.ToDecimal(SecurityAmt))- dGSTAmt;
                hdnGSTAmt.Value = Convert.ToString(dGSTAmt);
                dtAdvDetails = spm.getFormated_Amount("get_formated_Invoice_Amount", Adv_Bal_Amt, 0, 0, 0, 0);
				if (dtAdvDetails.Tables[0].Rows.Count > 0)
				{
					txtAdv_Bal_Amt.Text = Convert.ToString(dtAdvDetails.Tables[0].Rows[0]["invoiceAmt"]).Trim();
				}
			}

			if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["CurntApproverStatus"]).Trim() != "Pending")
            {
                trvl_btnSave.Visible = false;
                btnCancel.Visible = false;
                btnCorrection.Visible = false;
                txtRemarks.Enabled = false;
            }


            dgTravelRequest.DataSource = null;
            dgTravelRequest.DataBind();

            //if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["Emp_Code"]).Trim() != "99999999")
            //{
            //    invoice_description.Enabled = true;
            //}

                if (dsMilestone.Tables[1].Rows.Count > 0)
            {
                dgTravelRequest.DataSource = dsMilestone.Tables[1];
                dgTravelRequest.DataBind();
                spMilestones.Visible = true;
            }
            

            if (dsMilestone.Tables[4].Rows.Count > 0)
            {
                gvInvoiceMilestone.Visible = false;
                gvInvoiceMilestone_Acc.Visible = false; 

                if (Convert.ToString(txtEmpCode.Text).Trim() == "99999999")
                {
                    gvInvoiceMilestone_Acc.Visible = true;
                    gvInvoiceMilestone_Acc.DataSource = dsMilestone.Tables[4];
                    gvInvoiceMilestone_Acc.DataBind();
                }
                else
                {
                    gvInvoiceMilestone.Visible = true;
                    gvInvoiceMilestone.DataSource = dsMilestone.Tables[4];
                    gvInvoiceMilestone.DataBind();
                }

                //if (Convert.ToString(invoice_description.Text).Trim() == "99999999")
                //{
                //    invoice_description.Enabled = true;
                //}
            }



            if (Convert.ToBoolean(dsMilestone.Tables[0].Rows[0]["IsInvoiceWithPO"]) == true)
            {
                if (dsMilestone.Tables[2].Rows.Count > 0)
                {
                    spInvocies.Visible = true;
		    //Comment by Sanjay due to taking time
                    //gvMngPaymentList_Batch.DataSource = dsMilestone.Tables[2];
                    //gvMngPaymentList_Batch.DataBind();
                }
            }
            idliCostCenterList_ACC.Visible = false; 
            if (Convert.ToString(txtEmpCode.Text).Trim() == "99999999")
            {

                
                if (chkIsInvoiceWithoutPO.Checked == false)
                {
                    idliCostCenterList_ACC.Visible = true;
                    if (dsMilestone.Tables[5].Rows.Count > 0)
                    {
                        dgCostcenterList.DataSource = dsMilestone.Tables[5];
                        dgCostcenterList.DataBind();

                        //hdnMilestoneRowCnt.Value = Convert.ToString(dsMilestone.Tables[7].Rows.Count);
                        Int32 irowcnt = 1;                      
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

                        //int irowcnt = 1;
                        //foreach (GridViewRow row in dgCostcenterList.Rows)
                        //{
                        //    if (irowcnt <= Convert.ToInt32(hdnMilestoneRowCnt.Value))
                        //    {
                        //        row.Visible = true;
                        //        Label lblCostCenterAmt = (Label)row.FindControl("lblCostCenterAmt");
                        //        DropDownList lstMilestoneAmtCostCenter = (DropDownList)row.FindControl("lstMilestoneAmtCostCenter");
                        //        lblCostCenterAmt.Text = Convert.ToString(dsMilestone.Tables[7].Rows[irowcnt - 1]["CostCenter_Amt"]).Trim();
                        //        lstMilestoneAmtCostCenter.SelectedValue = Convert.ToString(dsMilestone.Tables[7].Rows[irowcnt - 1]["Dept_ID"]).Trim();
                        //    }
                        //    else
                        //    {
                        //        row.Visible = false;
                        //    }
                        //    irowcnt += 1;
                        //}

                    }
                }


                if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["status_id"]).Trim() == "2")
                {
                    trvldeatils_btnSave.Visible = false;
                    dgConstcneterList_Approved.Visible = true;
                    dgConstcneterList_Approved.DataSource = dsMilestone.Tables[6];
                    dgConstcneterList_Approved.DataBind(); 
                  
                    dgCostcenterList.Visible = false;
                }
            }
               getInvoiceUploadedFiles();
        }
    }

    public void get_Approver_List_Invoice()
    {
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();

        idDirectTaxApplicable.Visible = false;
        idPercentage.Visible = false;
        idDirectTaxAmount.Visible = false;

        idDirectTaxApplicable_TDS_1.Visible = false;
        idDirectTaxApplicable_TDS_2.Visible = false;
        idDirectTaxApplicable_TDS_3.Visible = false;

        lstExpenses.Visible = false;
        lstExpense_details.Visible = false;
        lstItem_details.Visible = false;
        idspnSelectExpenses.Visible = false;
        idspnexpstar.Visible = false;
        idspnitemstar.Visible = false;
        idspnSelectExpenses_details.Visible = false;
        idspnSelectitem_details.Visible = false;


        hdnIsFinalApprover.Value = "no";

        //string sPotypeid = "0";
        //if (chkIsInvoiceWithoutPO.Checked == true)
        //{
        //    sPotypeid = Convert.ToString(lstDisplayPOTypes.SelectedValue);

        //}
        //else
        //{
        //    sPotypeid = Convert.ToString(lstPOType.SelectedValue);
        //}
        int ichkWithoutPo = 0;
        if (chkIsInvoiceWithoutPO.Checked)
            ichkWithoutPo = 1;
        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), txtEmpCode.Text,hdnProject_Dept_Name.Value,Convert.ToString(lstPOType.SelectedValue), ichkWithoutPo);

        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
            DgvApprover.DataSource = dsMilestone.Tables[0];
            DgvApprover.DataBind();

            hdn_next_Appr_ID.Value = "";
            hdn_next_Appr_Empcode.Value = "";
            hdn_next_Appr_EmpEmail_ID.Value = "";
            hdn_next_Appr_Emp_Name.Value = "";
            for(int irow=0;irow<dsMilestone.Tables[0].Rows.Count;irow++)
            {
                if (Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() == "")
                {
                    hdn_next_Appr_ID.Value = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["APPR_ID"]).Trim();
                    hdn_next_Appr_Empcode.Value = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["APPR_Emp_Code"]).Trim();
                    hdn_next_Appr_EmpEmail_ID.Value = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                    hdn_next_Appr_Emp_Name.Value = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["ApproverName"]).Trim();
                    break;
                }
                if (Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() == "Pending")
                {
                    hdn_curnt_Appr_ID.Value = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["APPR_ID"]).Trim();
                    hdn_curnt_Appr_Empcode.Value = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["APPR_Emp_Code"]).Trim();
                    hdn_curnt_Appr_EmpEmail_ID.Value = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                    hdn_curnt_Appr_Emp_Name.Value = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["ApproverName"]).Trim(); 
                }
            }

            if(Convert.ToString(hdn_curnt_Appr_ID.Value).Trim()!="")
            {
				ADVtax.Visible = false;
				ADVtax2.Visible = false;
				//DAdvSet5.Visible = false;
				if (Convert.ToInt32(hdn_curnt_Appr_ID.Value)>99)
                {
                    trvl_btnSave.Text = "Verify";
                    trvl_btnSave.ToolTip = "Verify";
                    hdnIsFinalApprover.Value = "yes";
                    

                    if (chkIsInvoiceWithoutPO.Checked)
                    {
                        idDirectTaxApplicable_TDS_1.Visible = true;
                        idDirectTaxApplicable_TDS_2.Visible = true;
                        idDirectTaxApplicable_TDS_3.Visible = true;

                        idDirectTaxApplicable.Visible = true;
                        idPercentage.Visible = true;
                        idDirectTaxAmount.Visible = true;

                    }
                    else
                    {
                        idDirectTaxAmt_ACC_1.Visible = true;
                        idDirectTaxAmt_ACC_2.Visible = true;
                        idDirectTaxAmt_ACC_3.Visible = true;
						if (Convert.ToString(txtAdv_Settlement_Amt.Text).Trim() != "")
						{
							ADVtax.Visible = true;
						}
						if (Convert.ToString(txtSecurityDeposit.Text).Trim() != "")
						{
							ADVtax.Visible = true;
						}
						ADVtax2.Visible = true;
						//DAdvSet5.Visible = true;

					}

                    lstExpenses.Visible = true;
                    lstExpense_details.Visible = true;
                    lstItem_details.Visible = true;
                    idspnSelectExpenses.Visible = true;
                    idspnexpstar.Visible = true;
                    idspnitemstar.Visible = true;
                    idspnSelectExpenses_details.Visible = true;
                    idspnSelectitem_details.Visible = true;
                   
                    if (dgTravelRequest.Rows.Count > 0)
                    {
                        btnTra_Details.Visible = true;
                    }

                    //get_DirectTaxTypeList();
                    //get_DirectTaxSectionsList();
                }
            }

            if(chkIsInvoiceWithoutPO.Checked==true)
            {
                btnTra_Details.Visible = false;
            }
        }
    }

    protected string GetInvoiceApprove_RejectList(string invoiceid,string EmpCode)
    {
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        int ichkWithoutPo = 0;
        if (chkIsInvoiceWithoutPO.Checked)
            ichkWithoutPo = 1;
        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), EmpCode,hdnProject_Dept_Name.Value,Convert.ToString(lstPOType.SelectedValue), ichkWithoutPo);
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

    public string ReplaceInvalidChars(string filename)
    {
        Regex illegalInFileName = new Regex(@"[#%&{}\!/<'>@+*?$|=]");
        string myString = illegalInFileName.Replace(filename, "_");
        //return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        return myString;
    }

    public void getInvoiceUploadedFiles()
    {

        DataSet dsFiles = spm.getVSCB_UploadedFiles("get_VSCB_UploadedFiels", "Invoice", Convert.ToDouble(hdnInvoiceId.Value), "","");

        gvuploadedFiles.DataSource = null;
        gvuploadedFiles.DataBind();
        if (dsFiles.Tables[0].Rows.Count > 0)
        {
            gvuploadedFiles.DataSource = dsFiles;
            gvuploadedFiles.DataBind();
            spnSupportinFiles.Visible = true;
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
            dgTravelRequest.DataSource = null;
            dgTravelRequest.DataBind();
            dgTravelRequest.Visible = false;

            idWithoutInv_ProjectDept.Visible = true;
            idWithoutInv_Vendor.Visible = true;
            idWithoutInv_Blank.Visible = true;

            idWithoutInv_bank_1.Visible = true;
            idWithoutInv_bank_2.Visible = true;
            //idWithoutInv_bank_3.Visible = true;

            idWithoutInv_InvoiceType_1.Visible = true;
            idWithoutInv_InvoiceType_2.Visible = true;
            idWithoutInv_InvoiceType_3.Visible = true;

            idWithoutInv_InvoiceRemarks_1.Visible = true;
            idWithoutInv_InvoiceRemarks_2.Visible = true;
            idWithoutInv_InvoiceRemarks_3.Visible = true;

            txtInvoiceNo.Text = "";
            txtInvoiceDate.Text = "";
            txtAmtWithOutTax.Text = "";
            txtAmtWithTax_Invoice.Text = "";

            txtCGST_Per.Text = "";
            txtSGST_Per.Text = "";
            txtIGST_Per.Text = "";


        }
        else
        {
            editform.Visible = true;
            idMilestone.Visible = true;
            idMilestoneAmt.Visible = true;
            idBalanceAmt.Visible = true;

            dgTravelRequest.DataSource = null;
            dgTravelRequest.DataBind();
            dgTravelRequest.Visible = true;


            idWithoutInv_ProjectDept.Visible = false;
            idWithoutInv_Vendor.Visible = false;
            idWithoutInv_Blank.Visible = false;

            idWithoutInv_bank_1.Visible = false;
            idWithoutInv_bank_2.Visible = false;
            idWithoutInv_bank_3.Visible = false;

            idWithoutInv_InvoiceType_1.Visible = false;
            idWithoutInv_InvoiceType_2.Visible = false;
            idWithoutInv_InvoiceType_3.Visible = false;

            idWithoutInv_InvoiceRemarks_1.Visible = false;
            idWithoutInv_InvoiceRemarks_2.Visible = false;
            idWithoutInv_InvoiceRemarks_3.Visible = false;

        }

        txtCGST_Per.ReadOnly = true;
        txtSGST_Per.ReadOnly = true;
        txtIGST_Per.ReadOnly = true;

        txtCGST_Per.Enabled = false;
        txtSGST_Per.Enabled = false;
        txtIGST_Per.Enabled = false;

        
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

        if (dsProjectsVendors.Tables[2].Rows.Count > 0)
        {
            lstCostCenter.DataSource = dsProjectsVendors.Tables[2];
            lstCostCenter.DataTextField = "CostCentre";
            lstCostCenter.DataValueField = "Dept_ID";
            lstCostCenter.DataBind();
            lstCostCenter.Items.Insert(0, new ListItem("Select Cost Center", "0"));
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

        if (dsProjectsVendors.Tables[5].Rows.Count > 0)
        {
            lstItem_details.DataSource = dsProjectsVendors.Tables[5];
            lstItem_details.DataTextField = "Item_detail";
            lstItem_details.DataValueField = "Item_detail";
            lstItem_details.DataBind();
            lstItem_details.Items.Insert(0, new ListItem("Select Item", "0"));
        }
    }

    public void get_DirectTaxTypeList()
    {

        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_DirectTaxType_List";

        dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        if (dsProjectsVendors.Tables[0].Rows.Count > 0)
        {
            lstDirectTax.DataSource = dsProjectsVendors.Tables[0];
            lstDirectTax.DataTextField = "DirectTax_Type";
            lstDirectTax.DataValueField = "DirectTax_TypeID";
            lstDirectTax.DataBind();
            lstDirectTax.Items.Insert(0, new ListItem("Select Direct Taxt Type", "0"));
        }
}


    public void get_DirectTaxSectionsList()
    {

        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_TDS_Sections";

        dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        dsDirecttaxSectionList = dsProjectsVendors;
        if (dsProjectsVendors.Tables[0].Rows.Count > 0)
        {
            lstDirectTaxSections.DataSource = dsProjectsVendors.Tables[0];
            lstDirectTaxSections.DataTextField = "TDS_Description";
            lstDirectTaxSections.DataValueField = "TDS_Descript_ID";
            lstDirectTaxSections.DataBind();
            lstDirectTaxSections.Items.Insert(0, new ListItem("Select Direct Taxt Sections", "0"));
        }
    }

    public void get_DirectTax_Percentage()
    {

        DataSet dsTDSPer = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_TDS_Percentage";

        spars[1] = new SqlParameter("@Srno", SqlDbType.Int);
        spars[1].Value =Convert.ToInt32(lstDirectTaxSections.SelectedValue);


        dsTDSPer = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        if (dsTDSPer.Tables[0].Rows.Count > 0)
        {
            txtTDSTCS_Description.Text = Convert.ToString(dsTDSPer.Tables[0].Rows[0]["TDS_Description"]).Trim();
            txtTDSTCS_Description.ToolTip = Convert.ToString(dsTDSPer.Tables[0].Rows[0]["TDS_Description"]).Trim();

            lstDirectTax.SelectedValue = Convert.ToString(dsTDSPer.Tables[0].Rows[0]["DirectTax_TypeID"]).Trim();
            txtDirectTaxPercentage.Text = Convert.ToString(dsTDSPer.Tables[0].Rows[0]["TDS_Percentage"]).Trim();
        }
    }

    public void get_DirectTax_Percentage_ACC(string isrno,TextBox txtDirectTaxPercentage_ACC)
    {

        DataSet dsTDSPer = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_TDS_Percentage";

        spars[1] = new SqlParameter("@Srno", SqlDbType.Int);
        spars[1].Value = Convert.ToInt32(isrno);


        dsTDSPer = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        txtDirectTaxPercentage_ACC.Text = "";
        if (dsTDSPer.Tables[0].Rows.Count > 0)
        { 
            txtDirectTaxPercentage_ACC.Text = Convert.ToString(dsTDSPer.Tables[0].Rows[0]["TDS_Percentage"]).Trim();
        }
    }
    public void check_POWO_IsForLoginEployeee()
    {
        DataSet dsEmployee = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Check_Invoice_forLoginEmployee";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode.Text);

        spars[2] = new SqlParameter("@InvoiceId", SqlDbType.BigInt);
        spars[2].Value = Convert.ToDouble(hdnInvoiceId.Value);

        dsEmployee = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        if (dsEmployee.Tables[0].Rows.Count == 0)
        {
            Response.Redirect("~/procs/vscb_index.aspx");
        }
    }
    public void get_Check_Approver_IsInvoice_Approved()
    {

        DataSet dsapproved = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Checked_Approver_Approved_Invocie";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value =Convert.ToString( txtEmpCode.Text).Trim();

        spars[2] = new SqlParameter("@InvoiceId", SqlDbType.BigInt);
        spars[2].Value = Convert.ToDouble(hdnInvoiceId.Value);

        dsapproved = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        if (dsapproved.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToString(dsapproved.Tables[0].Rows[0]["Action"]).Trim() == "Approved")
            {
                trvl_btnSave.Visible = false;
                btnCancel.Visible = false;
                btnCorrection.Visible = false;
                idDirectTaxApplicable.Visible = false;
                idPercentage.Visible = false;
                idDirectTaxAmount.Visible = false;

                idDirectTaxApplicable_TDS_1.Visible = false;
                idDirectTaxApplicable_TDS_2.Visible = false;
                idDirectTaxApplicable_TDS_3.Visible = false;

                if (Convert.ToString(dsapproved.Tables[0].Rows[0]["Appr_ID"]).Trim() == "102")
                {
                    //idDirectTaxApplicable.Visible = true;
                    //idPercentage.Visible = true;
                    //idDirectTaxAmount.Visible = true;
                    //idDirectTaxApplicable_TDS_1.Visible = true;
                    //idDirectTaxApplicable_TDS_2.Visible = true;
                    //idDirectTaxApplicable_TDS_3.Visible = true;

                    //lstDirectTax.Enabled = false;
                    //txtDirectTaxPercentage.Enabled = false;
                    //lstDirectTaxSections.Enabled = false;


                    if (chkIsInvoiceWithoutPO.Checked)
                    {
                        lstDirectTax.Enabled = false;
                        txtDirectTaxPercentage.Enabled = false;
                        lstDirectTaxSections.Enabled = false;

                        idDirectTaxApplicable_TDS_1.Visible = true;
                        idDirectTaxApplicable_TDS_2.Visible = true;
                        idDirectTaxApplicable_TDS_3.Visible = true;

                        idDirectTaxApplicable.Visible = true;
                        idPercentage.Visible = true;
                        idDirectTaxAmount.Visible = true;

                    }
                    else
                    {
                        idDirectTaxAmt_ACC_1.Visible = true;
                        idDirectTaxAmt_ACC_2.Visible = true;
                        idDirectTaxAmt_ACC_3.Visible = true;

						if (Convert.ToString(txtAdv_Settlement_Amt.Text).Trim() != "")
						{
							ADVtax.Visible = true;
						}
						if (Convert.ToString(txtSecurityDeposit.Text).Trim() != "")
						{
							ADVtax.Visible = true;
						}
						ADVtax2.Visible = true;
					}
                }
            }
        }
        
    }

    public void UpdateOn_Final_TDSPercetage_TDS_Section_ID(int itds_DescripId,string dTds_Per,string dmilestoneID, decimal iamtwithoutTax,decimal Adv_Settlement_Amt, decimal SecDeposit_Settlement_Amt)
    {

        Decimal dDirectTaxPercentage_Amt = 0;
        Decimal dDirectTaxPercentage = 0;

        if (Convert.ToString(dTds_Per).Trim()!="")
            dDirectTaxPercentage = Convert.ToDecimal(dTds_Per);

        if (Convert.ToString(iamtwithoutTax).Trim() != "")
            dDirectTaxPercentage_Amt = Convert.ToDecimal(iamtwithoutTax);


        if (SecDeposit_Settlement_Amt > 0)
            dDirectTaxPercentage_Amt += SecDeposit_Settlement_Amt;

            dDirectTaxPercentage_Amt = Math.Round(dDirectTaxPercentage_Amt * dDirectTaxPercentage / 100, 2);

        
        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[7];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "UpdateInvoice_Milestone_TDS_Amt";

        spars[1] = new SqlParameter("@DirectTax_DescriptionID", SqlDbType.Int);
        spars[1].Value = itds_DescripId;

        spars[2] = new SqlParameter("@DirectTax_Percentage", SqlDbType.Decimal);
        spars[2].Value = dDirectTaxPercentage;

        spars[3] = new SqlParameter("@MstoneID", SqlDbType.BigInt);
        spars[3].Value = Convert.ToDouble(dmilestoneID);

        spars[4] = new SqlParameter("@DirectTax_Amount", SqlDbType.Decimal);
        spars[4].Value = dDirectTaxPercentage_Amt;

        spars[5] = new SqlParameter("@InvoiceID", SqlDbType.BigInt);
        spars[5].Value = Convert.ToDouble(hdnInvoiceId.Value);

		spars[6] = new SqlParameter("@ADSettlement_Amt", SqlDbType.Decimal);
		spars[6].Value = Convert.ToDouble(Adv_Settlement_Amt);

		dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_CreateInvoice");
 
    }

    public void UpdateOn_Final_Security_DepositAmount()
    {

        

        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "UpdateInvoice_Secutity_SettlemtAmt";

        spars[1] = new SqlParameter("@InvoiceID", SqlDbType.BigInt);
        spars[1].Value = Convert.ToDouble(hdnInvoiceId.Value);
          
        spars[2] = new SqlParameter("@ADSettlement_Amt", SqlDbType.Decimal);
        spars[2].Value = Convert.ToDouble(txtSecurityDeposit.Text);

        dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_CreateInvoice");

    }

    public void get_Milestone_TDS_Details(DropDownList lstDirectTaxSections_ACC,TextBox txtDirectTaxPercentage_ACC,CheckBox chkLDC_Applicable_ACC, string sMstoneID)
    { 

        DataSet dsMilestoneTDS = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_milestone_TDS_details"; 

        spars[1] = new SqlParameter("@MstoneID", SqlDbType.BigInt);
        spars[1].Value = Convert.ToDouble(sMstoneID);
         
        spars[2] = new SqlParameter("@InvoiceId", SqlDbType.BigInt);
        spars[2].Value = Convert.ToDouble(hdnInvoiceId.Value);

        dsMilestoneTDS = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        if(dsMilestoneTDS.Tables[0].Rows.Count>0)
        {
            lstDirectTaxSections_ACC.SelectedValue = Convert.ToString(dsMilestoneTDS.Tables[0].Rows[0]["TDS_Descript_ID"]);
            txtDirectTaxPercentage_ACC.Text = Convert.ToString(dsMilestoneTDS.Tables[0].Rows[0]["DirectTax_Percentage"]);
            if (Convert.ToString(dsMilestoneTDS.Tables[0].Rows[0]["IsLDC"]) == "true")
                chkLDC_Applicable_ACC.Checked = true;
            else
                chkLDC_Applicable_ACC.Checked = false;
        }

    }

    #endregion

    //Comment 7

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


    protected void btnTra_Details_Click(object sender, EventArgs e)
    {
        DataSet dsPOWOContent = new DataSet();

        #region get POWO Content 


        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_POWO_Content_FromTally";

        spars[1] = new SqlParameter("@POWO_Number", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(lstTripType.SelectedItem.Text).Trim();  //"PO/042021/00001"; 

        spars[2] = new SqlParameter("@POWOID", SqlDbType.Decimal);
        spars[2].Value = Convert.ToDouble(hdnPOWOID.Value);  //"PO/042021/00001";


        dsPOWOContent = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");

        #endregion

        try
        {



            string strpath = Server.MapPath("~/procs/VSCB_Rpt_POWO_Content_New.rdlc");
            string PowoNumber = Convert.ToString(lstTripType.SelectedItem.Text).Trim().Replace("/", "-");
            LocalReport ReportViewer2 = new LocalReport();
            ReportViewer2.ReportPath = strpath;// Server.MapPath(strpath);
            ReportDataSource rds = new ReportDataSource("dspowoContent", dsPOWOContent.Tables[0]);
            ReportDataSource rds_2 = new ReportDataSource("dsowoDetails", dsPOWOContent.Tables[1]);
            ReportDataSource rds_3 = new ReportDataSource("dsMilestone", dsPOWOContent.Tables[2]);
            ReportDataSource rds_4 = new ReportDataSource("dsPOWOAmountinWords", dsPOWOContent.Tables[3]);

            ReportDataSource rds_5 = new ReportDataSource("dsPOPreparedBy", dsPOWOContent.Tables[4]);
            ReportDataSource rds_6 = new ReportDataSource("dsPOCheckedBy", dsPOWOContent.Tables[5]);
            ReportDataSource rds_7 = new ReportDataSource("dsPOApprovedBY_HOD", dsPOWOContent.Tables[6]);
            ReportDataSource rds_8 = new ReportDataSource("dsPOApprovedBY_COO", dsPOWOContent.Tables[7]);
            ReportDataSource rds_9 = new ReportDataSource("dsPOApprovedBY_CEO", dsPOWOContent.Tables[8]);

            //if (Convert.ToString(dsPOWOContent.Tables[1].Rows[0]["Approver_Count"]).Trim() == "2")
            //{
            // ReportDataSource rds_6 = new ReportDataSource("dsPOCheckedBy", dsPOWOContent.Tables[5]);
            //  ReportDataSource rds_7 = new ReportDataSource("dsPOApprovedBY_HOD_COO", dsPOWOContent.Tables[6]);

            //  ReportViewer2.DataSources.Add(rds_7);
            //}


            ReportViewer2.DataSources.Clear();
            ReportViewer2.DataSources.Add(rds);
            ReportViewer2.DataSources.Add(rds_2);
            ReportViewer2.DataSources.Add(rds_3);
            ReportViewer2.DataSources.Add(rds_4);

            ReportViewer2.DataSources.Add(rds_5);
            ReportViewer2.DataSources.Add(rds_6);
            ReportViewer2.DataSources.Add(rds_7);
            ReportViewer2.DataSources.Add(rds_8);
            ReportViewer2.DataSources.Add(rds_9);
            ReportViewer2.Refresh();

            Warning[] warnings;
            string[] streamIds;
            string contentType;
            string encoding;
            string extension;

            //Export the RDLC Report to Byte Array.
            byte[] bytes = ReportViewer2.Render("pdf", null, out contentType, out encoding, out extension, out streamIds, out warnings);

            //Download the RDLC Report in Word, Excel, PDF and Image formats.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=POWO_"+ PowoNumber + "." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();
        }
    }



    protected void chkLDC_Applicable_CheckedChanged(object sender, EventArgs e)
    {
        if(chkLDC_Applicable.Checked==false)
        {
            get_DirectTax_Percentage();
            Calculate_DirectTax_Amount();

            txtDirectTaxPercentage.Enabled = false;
            txtDirectTaxPercentage.ReadOnly = true;

        }
        else
        {
            txtDirectTaxPercentage.Enabled = true;
            txtDirectTaxPercentage.ReadOnly = false;
        }
        
    }

    protected void lstExpenses_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region get Expeses List
        GetExpeses_Details();
        if (Convert.ToString(lstExpenses.SelectedValue).Trim() == "0")
            lstExpense_details.Enabled = false;
        else
            lstExpense_details.Enabled = true;
        #endregion
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

    protected void txtCostCenterAmt_TextChanged(object sender, EventArgs e)
    {
        TextBox txtCostCenterAmt = (TextBox)sender;
        GridViewRow row = (GridViewRow)txtCostCenterAmt.NamingContainer;

        DropDownList lstMilestoneAmtCostCenter = (DropDownList)row.FindControl("lstMilestoneAmtCostCenter");
 
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




    protected void btnback_mng_Click(object sender, EventArgs e)
    {
        //PostBackUrl = "~/procs/VSCB_Inboxinvoice.aspx"

        if(Request.QueryString.Count==2)
        {
            Response.Redirect("VSCB_Myapprovedinvoice.aspx");
        }

        Response.Redirect("VSCB_Inboxinvoice.aspx");

    }

    protected void gvInvoiceMilestone_Acc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // CheckBox chkLDC_Applicable_ACC = e.Row.FindControl("chkLDC_Applicable_ACC") as CheckBox;
            //chkLDC_Applicable_ACC.Visible = false;
          //  gvInvoiceMilestone_Acc.Columns[1].Visible = false;
            DropDownList lstDirectTaxSections_ACC = e.Row.FindControl("lstDirectTaxSections_ACC") as DropDownList;
            if (dsDirecttaxSectionList.Tables[0].Rows.Count > 0)
            {
                lstDirectTaxSections_ACC.DataSource = dsDirecttaxSectionList.Tables[0];
                lstDirectTaxSections_ACC.DataTextField = "TDS_Description";
                lstDirectTaxSections_ACC.DataValueField = "TDS_Descript_ID";
                lstDirectTaxSections_ACC.DataBind();
            }
            lstDirectTaxSections_ACC.Items.Insert(0, new ListItem("Select Direct Taxt Sections", "0"));

            if(Convert.ToString(hdnInvoiceApprovalStatus.Value).Trim() =="2")
            {
                TextBox txtDirectTaxPercentage_ACC = e.Row.FindControl("txtDirectTaxPercentage_ACC") as TextBox;
                CheckBox chkLDC_Applicable_ACC = e.Row.FindControl("chkLDC_Applicable_ACC") as CheckBox;
                string sMstoneID =Convert.ToString(gvInvoiceMilestone_Acc.DataKeys[e.Row.RowIndex].Values[2]);
                get_Milestone_TDS_Details(lstDirectTaxSections_ACC, txtDirectTaxPercentage_ACC, chkLDC_Applicable_ACC, sMstoneID);                 
                lstDirectTaxSections_ACC.Enabled = false;

                
            }
        }
       
    }

    protected void lstDirectTaxSections_ACC_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList lstDirectTaxSections_ACC = (DropDownList)sender;
        GridViewRow row = (GridViewRow)lstDirectTaxSections_ACC.NamingContainer;

        TextBox txtDirectTaxPercentage_ACC = (TextBox)row.FindControl("txtDirectTaxPercentage_ACC");
        TextBox txtInvoiceAmtWithoutGST_ACC = (TextBox)row.FindControl("txtInvoiceAmtWithoutGST_ACC");
        CheckBox chkLDC_Applicable_ACC = (CheckBox)row.FindControl("chkLDC_Applicable_ACC");

        chkLDC_Applicable_ACC.Enabled = false;

        if (Convert.ToString(lstDirectTaxSections_ACC.SelectedValue).Trim() == "1")
        {
            txtDirectTaxPercentage_ACC.Text = "0.00";
            txtDirectTaxPercentage_ACC.Enabled = false;
            chkLDC_Applicable_ACC.Checked = false;
            chkLDC_Applicable_ACC.Enabled = false;
        }
         else
        {
            chkLDC_Applicable_ACC.Enabled = true;
        }
        get_DirectTax_Percentage_ACC(Convert.ToString(lstDirectTaxSections_ACC.SelectedValue), txtDirectTaxPercentage_ACC);
        // Calculate_DirectTax_Amount_ACC(lstDirectTaxSections_ACC, txtDirectTaxPercentage_ACC, txtInvoiceAmtWithoutGST_ACC);
        Calculate_DirectTax_Amount_ACC();
    }

    protected void chkLDC_Applicable_ACC_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkLDC_Applicable_ACC = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chkLDC_Applicable_ACC.NamingContainer;

        TextBox txtDirectTaxPercentage_ACC = (TextBox)row.FindControl("txtDirectTaxPercentage_ACC");
        DropDownList lstDirectTaxSections_ACC = (DropDownList)row.FindControl("lstDirectTaxSections_ACC");
        //CheckBox chkLDC_Applicable_ACC = (CheckBox)row.FindControl("chkLDC_Applicable_ACC");

        if (chkLDC_Applicable_ACC.Checked)
        {
            txtDirectTaxPercentage_ACC.Enabled = true;
            txtDirectTaxPercentage_ACC.ReadOnly = false;
        }
        else
        {
            txtDirectTaxPercentage_ACC.Enabled = false;
            txtDirectTaxPercentage_ACC.ReadOnly = true;
        }

        get_DirectTax_Percentage_ACC(Convert.ToString(lstDirectTaxSections_ACC.SelectedValue), txtDirectTaxPercentage_ACC);
        Calculate_DirectTax_Amount_ACC();

    }

    protected void txtDirectTaxPercentage_ACC_TextChanged(object sender, EventArgs e)
    {
        TextBox txtDirectTaxPercentage_ACC = (TextBox)sender;
        GridViewRow row = (GridViewRow)txtDirectTaxPercentage_ACC.NamingContainer;

        DropDownList lstDirectTaxSections_ACC = (DropDownList)row.FindControl("lstDirectTaxSections_ACC");
        TextBox txtInvoiceAmtWithoutGST_ACC = (TextBox)row.FindControl("txtInvoiceAmtWithoutGST_ACC");

        if(Convert.ToString(txtDirectTaxPercentage_ACC.Text).Trim()!="")
        {
            decimal dDirectTaxperce = Convert.ToDecimal(txtDirectTaxPercentage_ACC.Text);

            if(dDirectTaxperce>100)
            {
                lblmessage.Text = "Please enter correct Direct Tax Percentage.";
                txtDirectTaxPercentage_ACC.Text = "";
            }
        }
        // Calculate_DirectTax_Amount_ACC(lstDirectTaxSections_ACC, txtDirectTaxPercentage_ACC, txtInvoiceAmtWithoutGST_ACC);
        Calculate_DirectTax_Amount_ACC();
    }
	public void Check_Advance_Payment_POWO_old()
	{
		DataSet dtAdvBal = new DataSet();
		SqlParameter[] spars12 = new SqlParameter[3];
		spars12[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
        spars12[0].Value = "Get_ADV_Pay_POWO_Invoice"; 
        // spars12[0].Value = "get_Advance_Security_Amount_Status";

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
		//hdnSecurityDeposit.Value = "";
		ChkAdvSettlement.Checked = false;
		ChkSecuritySettlement.Checked = false;
		ChkSecuritySettlement.Visible = false;
		int IsTDS = 0;
		hdnAdvance_IsTDC.Value = "";
		
		if (dtAdvBal.Tables[0].Rows.Count > 0)
		{
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
		if (dtAdvBal.Tables[1].Rows.Count > 0)
		{
			if (Convert.ToDecimal(dtAdvBal.Tables[1].Rows[0]["Adv_Pay_Bal"].ToString()) > 0)
			{
				ChkAdvSettlement.Visible = true;
			}
			IsTDS = Convert.ToInt32(dtAdvBal.Tables[1].Rows[0]["IsTDS"]);
			if (IsTDS == 1)
			{
				hdnAdvance_IsTDC.Value = "1";
			}
		}
		else
		{
			ChkAdvSettlement.Visible = false;
		}
		if (dtAdvBal.Tables[2].Rows.Count > 0)
		{
			if (Convert.ToString(dtAdvBal.Tables[2].Rows[0]["AdvancePayTypeID"].ToString()) != "")
			{
				ChkSecuritySettlement.Visible = true;
			}
			IsTDS = Convert.ToInt32(dtAdvBal.Tables[2].Rows[0]["IsTDS"]);
			if (IsTDS == 1)
			{
				hdnAdvance_IsTDC.Value = "1";
			}
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
    }

    //protected void trvl_btnSave_Click(object sender, EventArgs e)
    //{

    //    string qtype = "UpdateMail";


    //    DataSet dsgoal = new DataSet();
    //    SqlParameter[] spars = new SqlParameter[2];

    //    spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
    //    spars[0].Value = qtype;

    //    spars[1] = new SqlParameter("@invoices_description", SqlDbType.VarChar);
    //    spars[1].Value = Convert.ToString(txtInvocieRemarks.Text).Trim();

    //    spm.Insert_Data(spars, "SP_VSCB_CreateInvoice");
    //}

        protected void txtSecurityDeposit_TextChanged(object sender, EventArgs e)
    {
        if(Convert.ToString(txtSecurityDeposit.Text).Trim()!="")
        {
            decimal dInvoiceAmt = Convert.ToDecimal(txtAmtWithTax_Invoice.Text);
            decimal dSecurityDepositAmt = Convert.ToDecimal(txtSecurityDeposit.Text);
            decimal dInvoicePaybleAmt = 0;
            dInvoicePaybleAmt = dInvoiceAmt - dSecurityDepositAmt;
            txtAdv_Bal_Amt.Text = Convert.ToString(dInvoicePaybleAmt);
        }
    }
}   