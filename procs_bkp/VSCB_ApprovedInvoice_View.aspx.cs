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

public partial class VSCB_ApprovedInvoice_View : System.Web.UI.Page
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

                if (!Page.IsPostBack)
                {
                    txtAmtWithOutTax.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");
                    txtDirectTaxPercentage.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");

                    txtRemarks.Attributes.Add("maxlength", txtRemarks.MaxLength.ToString());

                    PopulateEmployeeData();

                    GetPODetails_List(); 
                    GetPOTypes();
                    get_ProjectDept_Vendor_List();
                    if (Request.QueryString.Count>0)
                    {
                        hdnInvoiceId.Value = Convert.ToString(Request.QueryString["invid"]).Trim();
                        if(Request.QueryString.Count ==3)
                        hdnbatchId.Value = Convert.ToString(Request.QueryString["batchid"]).Trim();

                        get_InvoiceDetails_MilestonesList_Approval();
                        get_Batched_Approved_PaymentHistory();
                        get_Approver_List_Invoice();
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

    protected void dgTravelRequest_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
             
            if (Convert.ToString(hdnInvoiceId.Value).Trim() != "")
            {
                e.Row.Cells[10].Visible = false;
            }
             
        }
    }



    protected void trvl_btnSave_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }


        string[] strdate;
        string strInvoiceDate = "";
        Decimal dDirectTaxPercentage_Amt = 0;
        Decimal dDirectTaxPercentage = 0;
        Int32 iDirectTaxTypeID = 1;
        Decimal dPayableAmt_WithTax = 0;
        dPayableAmt_WithTax = Convert.ToDecimal(txtAmtWithTax_Invoice.Text);

        if (Convert.ToString(hdnIsFinalApprover.Value).Trim().ToLower() == "yes")
        {
            if(Convert.ToString(lstDirectTax.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Direct Tax Applicable.";
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

                dDirectTaxPercentage_Amt = Math.Round(dDirectTaxPercentage * Convert.ToDecimal(txtAmtWithOutTax.Text) / 100, 2);

                txtDirecttaxAmt.Text = Convert.ToString(dDirectTaxPercentage_Amt).Trim();
                dPayableAmt_WithTax = Math.Round(rCGST + rSGST + rIGST + dDirectTaxPercentage_Amt + Convert.ToDecimal(txtAmtWithOutTax.Text), 2);
                txtAmtWithTax_Invoice.Text = Convert.ToString(dPayableAmt_WithTax);
            }
        }

        #region date formatting
        if (Convert.ToString(txtInvoiceDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtInvoiceDate.Text).Trim().Split('-');
            strInvoiceDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        #endregion

        
        if (Convert.ToString(hdn_next_Appr_Emp_Name.Value).Trim() != "")
        {
            //update Next approver Invoice Status
            spm.InsertInvoice_ApproverDetails("Insertinvoice_Approver", Convert.ToDouble(hdnInvoiceId.Value), hdn_next_Appr_Empcode.Value, Convert.ToInt32(hdn_next_Appr_ID.Value), "Pending",Convert.ToString(txtRemarks.Text).Trim(),Convert.ToString(hdn_curnt_Appr_Empcode.Value), Convert.ToString(hdn_curnt_Appr_ID.Value));

 

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
             
            if(Convert.ToString(lstDirectTax.SelectedValue).Trim() !="0")
            {
                iDirectTaxTypeID = Convert.ToInt32(lstDirectTax.SelectedValue);
            }
            //Update final Approval Invoice Status
            spm.InsertInvoice_ApproverDetails_Final("UpdateInvoice_FinalApproval", Convert.ToDouble(hdnInvoiceId.Value), hdn_curnt_Appr_Empcode.Value, Convert.ToInt32(hdn_curnt_Appr_ID.Value), "Approved", Convert.ToString(txtRemarks.Text).Trim(),"","",iDirectTaxTypeID, dDirectTaxPercentage, dDirectTaxPercentage_Amt, dPayableAmt_WithTax);
             
        }

         
        #region Send Email to next Approver and  if Its Final Approver then send to Invocie Creator

        string strSubject = "";
        if(Convert.ToString(hdnIsFinalApprover.Value).Trim()=="yes")
        {
          strSubject=  "Approved Invoice Request- " + Convert.ToString(txtInvoiceNo.Text).Trim();
        }
        else
        {
            strSubject=  "Request for - Invoice Approval - " + Convert.ToString(txtInvoiceNo.Text).Trim();
        }
        

        string sApproverEmail_CC = "";

        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), txtEmpCode.Text, hdnProject_Dept_Name.Value);

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
        strbuild_Approvers.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%;border:solid'>");
        strbuild_Approvers.Append("<tr style='background-color:#C7D3D4'><td style='border:solid'>Approver Name</td><td style='border:solid'>Status</td><td style='border:solid'>Approved On</td><td style='border:solid'>Approver Remarks</td></tr>");
        for (Int32 irow = 0; irow < dsMilestone.Tables[0].Rows.Count; irow++)
        {
            strbuild_Approvers.Append("<tr style='border:solid'><td style='border:solid'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["ApproverName"]).Trim() + " </td>");
            strbuild_Approvers.Append("<td style='border:solid'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() + "</td>");
            strbuild_Approvers.Append("<td style='border:solid'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["approved_on"]).Trim() + "</td>");
            strbuild_Approvers.Append("<td style='border:solid'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Remarks"]).Trim() + "</td></tr>");
        }

        strbuild_Approvers.Append("</table>");

       
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
        if (chkIsInvoiceWithoutPO.Checked == true)
        {
            strbuild.Append("<tr><td>Project/Department :-</td><td>" + Convert.ToString(lstProjectDept.SelectedItem.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Vendor Name :-</td><td>" + Convert.ToString(lstVendors.SelectedItem.Text).Trim() + "</td></tr>");
        }
        strbuild.Append("<tr><td style='height:30px'></td></tr>");
        strbuild.Append("<tr><td>Invoice No :-</td><td>" + Convert.ToString(txtInvoiceNo.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>Invoice Date :-</td><td>" + Convert.ToString(txtInvoiceDate.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>Invoice Amount :-</td><td>" + Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() + "</td></tr>");


        if (Convert.ToString(hdnIsFinalApprover.Value).Trim().ToLower() == "yes")
        {
            if (Convert.ToString(lstDirectTax.SelectedValue).Trim() != "1")
            {
                strbuild.Append("<tr><td>Direct Tax :-</td><td>" + Convert.ToString(lstDirectTax.SelectedItem.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Direct Tax(%) :-</td><td>" + Convert.ToString(txtDirectTaxPercentage.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Direct Tax Amount :-</td><td>" + Convert.ToString(txtDirecttaxAmt.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Payable Amount (with Tax) :-</td><td>" + Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() + "</td></tr>");
            }
        }

        //strbuild.Append("<tr><td style='height:40px'></td></tr>");
        //strbuild.Append("<tr><td style='height:20px'><a href='" + strInvoiceURL + "'> Please Click here for your action </a></td></tr>");

        strbuild.Append("</table>  <br /><br />");

        strbuild.Append(strbuild_Approvers);

        if (Convert.ToString(hdnIsFinalApprover.Value).Trim().ToLower() == "yes")
        {
            spm.sendMail(hdnInvoiceCreator_Email.Value, strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);
        }
        else
        {
            spm.sendMail(hdn_next_Appr_EmpEmail_ID.Value, strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);
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
            lblmessage.Text = "Please enter rejection remarks.";
            return;
        }


        spm.InsertInvoice_ApproverDetails("UpdateInvoice_Reject", Convert.ToDouble(hdnInvoiceId.Value), hdn_curnt_Appr_Empcode.Value, Convert.ToInt32(hdn_curnt_Appr_ID.Value), "Reject", Convert.ToString(txtRemarks.Text).Trim(), "", "");

         
        #region Send Email to next Approver

        string sApproverEmail_CC = "";

        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), txtEmpCode.Text, hdnProject_Dept_Name.Value);

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
        strbuild_Approvers.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%;border:solid'>");
        strbuild_Approvers.Append("<tr style='background-color:#C7D3D4'><td style='border:solid'>Approver Name</td><td style='border:solid'>Status</td><td style='border:solid'>Approved On</td><td style='border:solid'>Approver Remarks</td></tr>");
        for (Int32 irow = 0; irow < dsMilestone.Tables[0].Rows.Count; irow++)
        {
            strbuild_Approvers.Append("<tr style='border:solid'><td style='border:solid'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["ApproverName"]).Trim() + " </td>");
            strbuild_Approvers.Append("<td style='border:solid'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() + "</td>");
            strbuild_Approvers.Append("<td style='border:solid'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["approved_on"]).Trim() + "</td>");
            strbuild_Approvers.Append("<td style='border:solid'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Remarks"]).Trim() + "</td></tr>");
        }
        strbuild_Approvers.Append("</table>");

        string strSubject = "Invoice - " + Convert.ToString(txtInvoiceNo.Text).Trim()  + " Rejected";
        string strInvoiceURL = "";
        strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["approverLink_VSCB"]).Trim() + "?invid=" + hdnInvoiceId.Value).Trim();
        StringBuilder strbuild = new StringBuilder();

        strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'>");
        strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>");
        strbuild.Append("<tr><td colspan='2'> " + hdn_curnt_Appr_Emp_Name.Value + " has reject an invoice with the following details.</td></tr>");
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
        if (chkIsInvoiceWithoutPO.Checked == true)
        {
            strbuild.Append("<tr><td>Project/Department :-</td><td>" + Convert.ToString(lstProjectDept.SelectedItem.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td>Vendor Name :-</td><td>" + Convert.ToString(lstVendors.SelectedItem.Text).Trim() + "</td></tr>");
        }
            strbuild.Append("<tr><td style='height:30px'></td></tr>");
        strbuild.Append("<tr><td>Invoice No :-</td><td>" + Convert.ToString(txtInvoiceNo.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>Invoice Date :-</td><td>" + Convert.ToString(txtInvoiceDate.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>Invoice Amount :-</td><td>" + Convert.ToString(txtAmtWithTax_Invoice.Text).Trim() + "</td></tr>");

         
        strbuild.Append("</table>  <br /><br />");

        strbuild.Append(strbuild_Approvers);

        spm.sendMail(hdnInvoiceCreator_Email.Value, strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);


        #endregion

        Response.Redirect("VSCB_Inboxinvoice.aspx");

    }

    protected void lnkTravelDetailsEdit_Click(object sender, EventArgs e)
    {

        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnSrno.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnpamentStatusid.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[1]).Trim();
        hdnMilestoneID.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[2]).Trim();


        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.getMilestone_Details_CreateInvoice(Convert.ToDouble(hdnMilestoneID.Value), Convert.ToDouble(lstTripType.SelectedValue));

        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
            txtMilestoneName_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MilestoneName"]).Trim();
            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["GSTIN_No"]).Trim() != "")
            {
                if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Per"]).Trim() != "0.00")
                    txtCGST_Per.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Per"]).Trim();

                if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Per"]).Trim() != "0.00")
                    txtSGST_Per.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Per"]).Trim();

                if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Per"]).Trim() != "0.00")
                    txtIGST_Per.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Per"]).Trim();
            }

            txtMilestoneAmt_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["AmtWithTax"]).Trim();
            txtMilestoneBalanceAmt_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["AmtWithTax"]).Trim();
            txtInvoiceDate.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Invoicedate"]).Trim();
        }
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


        spm.InsertInvoice_ApproverDetails("UpdateInvoice_Correction", Convert.ToDouble(hdnInvoiceId.Value), hdn_curnt_Appr_Empcode.Value, Convert.ToInt32(hdn_curnt_Appr_ID.Value), "Correction", Convert.ToString(txtRemarks.Text).Trim(), "", "");


        #region Send Email to next Approver

        string sApproverEmail_CC = "";

        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), txtEmpCode.Text, hdnProject_Dept_Name.Value);

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
        strbuild_Approvers.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%;border:solid'>");
        strbuild_Approvers.Append("<tr style='background-color:#C7D3D4'><td style='border:solid'>Approver Name</td><td style='border:solid'>Status</td><td style='border:solid'>Approved On</td><td style='border:solid'>Approver Remarks</td></tr>");
        for (Int32 irow = 0; irow < dsMilestone.Tables[0].Rows.Count; irow++)
        {
            strbuild_Approvers.Append("<tr style='border:solid'><td style='border:solid'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["ApproverName"]).Trim() + " </td>");
            strbuild_Approvers.Append("<td style='border:solid'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() + "</td>");
            strbuild_Approvers.Append("<td style='border:solid'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["approved_on"]).Trim() + "</td>");
            strbuild_Approvers.Append("<td style='border:solid'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Remarks"]).Trim() + "</td></tr>");
        }
        strbuild_Approvers.Append("</table>");

        string strSubject = "Invoice - " + Convert.ToString(txtInvoiceNo.Text).Trim() + " send back for correction";
        string strInvoiceURL = "";
        strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["CorrectionLink_VSCB"]).Trim() + "?invid=" + hdnInvoiceId.Value).Trim() + "&mngexp=1";
        StringBuilder strbuild = new StringBuilder();

        strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'>");
        strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>");
        strbuild.Append("<tr><td colspan='2'> " + hdn_curnt_Appr_Emp_Name.Value + " has Sent back the invoice for correction. Please correct the same as instructed and resend for approval.</td></tr>");
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

        if (chkIsInvoiceWithoutPO.Checked == true)
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

        spm.sendMail(hdnInvoiceCreator_Email.Value, strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);


        #endregion

        Response.Redirect("VSCB_Inboxinvoice.aspx");


    }

    protected void lnkDeleteexpFile_Click(object sender, EventArgs e)
    {

    }

    protected void gvuploadedFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void lstDirectTax_SelectedIndexChanged(object sender, EventArgs e)
    {

        txtDirectTaxPercentage.Text = "";
        txtDirecttaxAmt.Text = "";

        if (Convert.ToString(lstDirectTax.SelectedValue).Trim() == "1")
        {
            txtDirectTaxPercentage.Enabled = false;
        }
        else
        {
            txtDirectTaxPercentage.Enabled = true;
        }
        Calculate_DirectTax_Amount();
    }

    protected void txtDirectTaxPercentage_TextChanged(object sender, EventArgs e)
    {

        Calculate_DirectTax_Amount();
    }

    protected void btnback_mng_Click(object sender, EventArgs e)
    {
        if (Request.QueryString.Count == 3)
            Response.Redirect("VSCB_ApproveBatch.aspx?batchid="+ hdnbatchId.Value);
        else
            Response.Redirect("VSCB_CreateBatch.aspx");
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
    private void Calculate_DirectTax_Amount()
    {

        if (Convert.ToString(lstDirectTax.SelectedValue).Trim() != "0" && Convert.ToString(lstDirectTax.SelectedValue).Trim() != "1")
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

        dDirectTaxPercentage_Amt = Math.Round(dDirectTaxPercentage * Convert.ToDecimal(txtAmtWithOutTax.Text) / 100, 2);

        txtDirecttaxAmt.Text = Convert.ToString(dDirectTaxPercentage_Amt).Trim();
        txtAmtWithTax_Invoice.Text = Convert.ToString(rCGST + rSGST + rIGST + dDirectTaxPercentage_Amt + Convert.ToDecimal(txtAmtWithOutTax.Text));

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

    public void getMilestonesList()
    {
        dgTravelRequest.DataSource = null;
        dgTravelRequest.DataBind();


        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.getMilestoneFrom_Temptable(txtEmpCode.Text,Convert.ToDouble(lstTripType.SelectedValue));

        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
            dgTravelRequest.DataSource = dsMilestone.Tables[0];
            dgTravelRequest.DataBind();

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
            txtPOWOAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POWOAmt"]).Trim();
            lstPOType.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTypeID"]).Trim();
            txtPaidAmr.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Paid_Amt"]).Trim();
            txtBalanceAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Bal_Amt"]).Trim();

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

        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
            chkIsInvoiceWithoutPO.Checked = false;
            if (Convert.ToBoolean(dsMilestone.Tables[0].Rows[0]["IsInvoiceWithPO"]) == false)
            {
                chkIsInvoiceWithoutPO.Checked = true;
                chkIsInvoiceWithoutPO.Checked = true;
                lblIsInvoiceWithPO.Visible = true;
                lstProjectDept.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Prj_Dept_id"]).Trim();
                lstVendors.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["VendorId"]).Trim();
                lstCostCenter.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Prj_Dept_id"]).Trim();

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
                txtCostCenter.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CostCenter"]).Trim();
            }



            hdnProject_Dept_Id.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Prj_Dept_id"]).Trim();
            
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
            txtPOWOAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POWOAmt"]).Trim();
            lstPOType.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTypeID"]).Trim();
            lstTripType.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POID"]).Trim();
            lnkfile_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Invoice_File"]).Trim();
            txtPaidAmr.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Paid_Amt"]).Trim();
            txtBalanceAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Bal_Amt"]).Trim();
            txtDiretTaxAmtPOWO.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["DirectTaxCollection_Amt"]).Trim();
            lnkfile_Invoice.Visible = true;

            hdnCompCode.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Comp_Code"]).Trim();
            hdnVendorId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["VendorID"]).Trim();


            txtMilestoneName_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MilestoneName"]).Trim();
            txtMilestoneAmt_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MilestoneAmt"]).Trim();
            txtMilestoneBalanceAmt_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["BalanceMilestoneAmt"]).Trim();             
            txtInvoiceNo.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["InvoiceNo"]).Trim();
            txtInvoiceDate.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["InvoiceDate"]).Trim();
            txtAmtWithOutTax.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["AmtWithoutTax"]).Trim();
            txtAmtWithTax_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["AmtWithTax"]).Trim();
            txtCGST_Per.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Per"]).Trim();
            txtSGST_Per.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Per"]).Trim();
            txtIGST_Per.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Per"]).Trim(); 

            hdnInvoiceCreator_Name.Value= Convert.ToString(dsMilestone.Tables[0].Rows[0]["invoiceCreatorName"]).Trim();
            hdnInvoiceCreator_Email.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["invoiceCreatorEmail"]).Trim();
            //if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["InvoiceApprovalStatus"]).Trim() == "Approved")
            //{
            //    trvl_btnSave.Visible = false;
            //}
            lstTripType.Enabled = false;
            lstPOType.Enabled = false;

            dgTravelRequest.DataSource = null;
            dgTravelRequest.DataBind();


            if (dsMilestone.Tables[1].Rows.Count > 0)
            {
                dgTravelRequest.DataSource = dsMilestone.Tables[1];
                dgTravelRequest.DataBind();
                spMilestones.Visible = true;
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
        hdnIsFinalApprover.Value = "no";

        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), txtEmpCode.Text,hdnProject_Dept_Name.Value);

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
                if(Convert.ToInt32(hdn_curnt_Appr_ID.Value)>99)
                {
                    trvl_btnSave.Text = "Verify";
                    trvl_btnSave.ToolTip = "Verify";
                    hdnIsFinalApprover.Value = "yes";
                    idDirectTaxApplicable.Visible = true;
                    idPercentage.Visible = true;
                    idDirectTaxAmount.Visible = true;

                    get_DirectTaxTypeList();
                }
            }
        }
    }

    protected string GetInvoiceApprove_RejectList(string invoiceid,string EmpCode)
    {
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), EmpCode,hdnProject_Dept_Name.Value);
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


    public void get_Batched_Approved_PaymentHistory()
    {

        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_batched_Approved_PaymentHistory";

        spars[1] = new SqlParameter("@POWOID", SqlDbType.BigInt);
        if (Convert.ToString(lstTripType.SelectedValue) != "")
            spars[1].Value = Convert.ToDouble(lstTripType.SelectedValue);
        else
            spars[1].Value = DBNull.Value;

        dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        lblApprovedPaymentHistory.Visible = false;
        GrdPaymentHistory.DataSource = null;
        GrdPaymentHistory.DataBind();

        if (dsProjectsVendors.Tables[0].Rows.Count > 0)
        {
            lblApprovedPaymentHistory.Visible = true;
            GrdPaymentHistory.DataSource = dsProjectsVendors.Tables[0];
            GrdPaymentHistory.DataBind();
        }
        
    }
    #endregion




}   