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

public partial class VSCB_CreateInvoiceACC : System.Web.UI.Page
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

                 
                lblheading.Text = "Create Invoice";
                if (!Page.IsPostBack)
                {
                    FilePath.Value = Convert.ToString(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBInvoicefiles"]).Trim() + "/"));

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


                    PopulateEmployeeData();

                    GetPODetails_List();
                    GetPOTypes();
                    get_Approver_List();
                    CheckIs_Invoice_Wthout_POWO();


                    if (Request.QueryString.Count > 0)
                    {
                        //GetPODetails_List_Invoice_View();
                         
                        get_InvoiceDetails_MilestonesList_Update();
                        if(Convert.ToString(hdnInvoiceApprStatusId.Value).Trim()!="2")
                        get_Approver_List();
                        txtInvoiceNo.Enabled = false;
                        trvl_btnSave.Text = "Update Invoice";

                        livoucher_1.Visible = true;
                        livoucher_2.Visible = true;
                        livoucher_3.Visible = true;
                        //get_Approved_Reject_Approver_List();

                        if(Convert.ToString(lnkfile_Invoice.Text).Trim()=="")
                        {
                            iduploaded_Invoice_1.Visible = true;
                            iduploaded_Invoice_2.Visible = true;
                            iduploaded_Invoice_3.Visible = true;
                        }
                        else
                        {
                            idspnUploadedInvoice.Visible = true;
                        }
                            trvl_btnSave.Visible = false;
                         
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

   
     
    protected void lnkTravelDetailsEdit_Click(object sender, EventArgs e)
    {

        //ImageButton btn = (ImageButton)sender;
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnSrno.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnpamentStatusid.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[1]).Trim();
        hdnMilestoneID.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[2]).Trim();
       
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

            txtInvoiceDate.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Invoicedate"]).Trim();



             


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

            if(Convert.ToDouble(txtAmtWithOutTax.Text)>10000)
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
        lstCostCenter.SelectedValue = lstProjectDept.SelectedValue; 
        get_Approver_List();

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
             
            txtInvoiceNo.Enabled = false;
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

            txtCGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Amt"]).Trim();
            txtSGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Amt"]).Trim();
            txtIGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Amt"]).Trim();
            if(Convert.ToString(txtCGST_Per.Text).Trim()!="" && Convert.ToString(txtCGST_Per.Text).Trim() != "0.00")
            {
                txtIGST_Per.Enabled = false;
            }
            else if (Convert.ToString(txtIGST_Per.Text).Trim() != "" && Convert.ToString(txtIGST_Per.Text).Trim() != "0.00")
            {
                txtCGST_Per.Enabled = false;
                txtSGST_Per.Enabled = false;
            }

            lnkfile_Invoice.Text= Convert.ToString(dsMilestone.Tables[0].Rows[0]["Invoice_File"]).Trim();             
            lnkfile_Invoice.Visible = true;

            if(Convert.ToString(dsMilestone.Tables[0].Rows[0]["Status_id"]).Trim()=="2")
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
    }

    private void Create_Invoice_WithPOWO()
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
        if (Convert.ToString(hdnInvoiceId.Value)=="0")
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
            DataSet dtApproverEmailIds =spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), txtEmpCode.Text, hdnProject_Dept_Name.Value, sPotypeid); //spm.VSCB_GetApproverEmailID("get_Approvers_matrix", Convert.ToString(txtProject.Text).Trim(), 0);

        if (dtApproverEmailIds.Tables[0].Rows.Count > 0)
        {
            string sApproverEmpCode = "";
            int iAppr_id = 0;
            string sApproverEmail = "";
            string sApprover_name = "";

            sApproverEmpCode = Convert.ToString(dtApproverEmailIds.Tables[0].Rows[0]["APPR_Emp_Code"]).Trim();
            sApprover_name = Convert.ToString(dtApproverEmailIds.Tables[0].Rows[0]["ApproverName"]).Trim();
            sApproverEmail = Convert.ToString(dtApproverEmailIds.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
            iAppr_id = Convert.ToInt32(dtApproverEmailIds.Tables[0].Rows[0]["APPR_ID"]);

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
            dtMaxreqID = spm.InsertUpdate_Invoice(txtEmpCode.Text, sInvoiceType, Convert.ToDouble(hdnMilestoneID.Value), Convert.ToString(txtInvoiceNo.Text).Trim(), strInvoiceDate, Convert.ToDecimal(txtAmtWithOutTax.Text), rCGST, rSGST, rIGST, rCGST_Per, rSGST_Per, rIGST_Per, Convert.ToDecimal(txtAmtWithTax_Invoice.Text), dMilestoneBalAmt, 1, 1, blnIswithPO, dProject_Dept_id, iVendorId, Convert.ToString(hdnInvoiceId.Value),Convert.ToString(txtVoucherNo.Text),strSupplierInvoiceDate,sPotypeid,Convert.ToString(txtInvocieRemarks.Text),Convert.ToDouble(lstTripType.SelectedValue),false,"Account");

            dMaxReqId = Convert.ToDouble(dtMaxreqID.Tables[0].Rows[0]["MaxReqID"]);


            spm.InsertInvoice_ApproverDetails("Insertinvoice_Approver", dMaxReqId, sApproverEmpCode, iAppr_id, "Pending", "", "", "","");


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

                }


            }
            #endregion


            #region Send Email to 1st Approver

            DataSet dsMilestone = new DataSet();
            dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(dMaxReqId), txtEmpCode.Text, hdnProject_Dept_Name.Value,Convert.ToString(sPotypeid));

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
                strSubject = "OneHR: Request for - Invoice Approval - " + Convert.ToString(txtInvoiceNo.Text).Trim() ;
            else
                strSubject = "OneHR: Request for - Invoice Approval - " + Convert.ToString(txtInvoiceNo.Text).Trim() + " against " + Convert.ToString(lstTripType.SelectedItem.Text).Trim();

            string strInvoiceURL = "";
            strInvoiceURL=Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["approverLink_VSCB"]).Trim() + "?invid=" + dMaxReqId).Trim();
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
                strbuild.Append("<tr><td>Department :-</td><td>" + Convert.ToString(lstProjectDept.SelectedItem.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Vendor Name :-</td><td>" + Convert.ToString(lstVendors.SelectedItem.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(lstCostCenter.SelectedItem.Text).Trim() + "</td></tr>");
            }

            if (chkIsInvoiceWithoutPO.Checked == false)
            {
                strbuild.Append("<tr><td style='height:20px'></td></tr>");
                strbuild.Append("<tr><td>Milestone Particular :-</td><td>" + Convert.ToString(txtMilestoneName_Invoice.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Milestone Amount :-</td><td>" + Convert.ToString(txtMilestoneAmt_Invoice.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Milestone Balance Amount :-</td><td>" + Convert.ToString(txtMilestoneBalanceAmt_Invoice.Text).Trim() + "</td></tr>");

                strbuild.Append("<tr><td style='height:20px'></td></tr>");
                strbuild.Append("<tr><td>PO/WO Title :-</td><td>" + Convert.ToString(txtPOTitle.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>PO/WO No :-</td><td>" + Convert.ToString(lstTripType.SelectedItem.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>PO/WO Amount :-</td><td>" + Convert.ToString(txtPOWOAmt.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Department :-</td><td>" + Convert.ToString(txtProject.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(txtCostCenter.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>PO/WO Date :-</td><td>" + Convert.ToString(txtFromdate.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td>Vendor Name :-</td><td>" + Convert.ToString(txtVendor.Text).Trim() + "</td></tr>");
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


        DataSet dtApproverEmailIds = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), txtEmpCode.Text, hdnProject_Dept_Name.Value,Convert.ToString(lstPOType.SelectedValue)); //spm.VSCB_GetApproverEmailID("get_Approvers_matrix", Convert.ToString(txtProject.Text).Trim(), 0);

        if (dtApproverEmailIds.Tables[0].Rows.Count > 0)
        {
            string sApproverEmpCode = "";
            int iAppr_id = 0;
            string sApproverEmail = "";
            string sApprover_name = "";

            sApproverEmpCode = Convert.ToString(dtApproverEmailIds.Tables[0].Rows[0]["APPR_Emp_Code"]).Trim();
            sApprover_name = Convert.ToString(dtApproverEmailIds.Tables[0].Rows[0]["ApproverName"]).Trim();
            sApproverEmail = Convert.ToString(dtApproverEmailIds.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
            iAppr_id = Convert.ToInt32(dtApproverEmailIds.Tables[0].Rows[0]["APPR_ID"]);

            double dMaxReqId = 0;

            DataSet dtMaxreqID = new DataSet();
            dtMaxreqID = spm.InsertUpdate_Invoice(txtEmpCode.Text, "Insertinvoice", 0, Convert.ToString(txtInvoiceNo.Text).Trim(), strInvoiceDate, Convert.ToDecimal(txtAmtWithOutTax.Text), rCGST, rSGST, rIGST, rCGST_Per, rSGST_Per, rIGST_Per, Convert.ToDecimal(txtAmtWithTax_Invoice.Text), dMilestoneBalAmt, 1, 1, blnIswithPO, dProject_Dept_id, iVendorId,"","","","",Convert.ToString(txtInvocieRemarks.Text),Convert.ToDouble(lstTripType.SelectedValue),false,"");

            dMaxReqId = Convert.ToDouble(dtMaxreqID.Tables[0].Rows[0]["MaxReqID"]);


            spm.InsertInvoice_ApproverDetails("Insertinvoice_Approver", dMaxReqId, sApproverEmpCode, iAppr_id, "Pending", "", "", "","");


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

            idWithoutInv_InvoiceType_1.Visible = true;
            idWithoutInv_InvoiceType_2.Visible = true;
            idWithoutInv_InvoiceType_3.Visible = true;

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


        }
        else
        {
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

            idWithoutInv_InvoiceType_1.Visible = false;
            idWithoutInv_InvoiceType_2.Visible = false;
            idWithoutInv_InvoiceType_3.Visible = false;

            idWithoutInv_InvoiceRemarks_1.Visible = false;
            idWithoutInv_InvoiceRemarks_2.Visible = false;
            idWithoutInv_InvoiceRemarks_3.Visible = false;




            hdnTotalInvoiceAmt.Value = "0";

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
        dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), EmpCode, hdnProject_Dept_Name.Value,Convert.ToString(lstPOType.SelectedValue));
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
        spars[0].Value = "get_Approved_POWOList_ACC"; // "get_Approved_POWOList_CreateInvoice";

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

    public void GetPODetails_List_Invoice_View()
    {

        DataSet dtPOWODetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value ="get_Approved_POWOList";

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

    public void getMilestonesList()
    {
        dgTravelRequest.DataSource = null;
        dgTravelRequest.DataBind();


        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.getMilestoneFrom_Temptable(txtEmpCode.Text, Convert.ToDouble(lstTripType.SelectedValue), "EN-IN");

        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
            dgTravelRequest.DataSource = dsMilestone.Tables[0];
            dgTravelRequest.DataBind();

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
            txtPaidAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Paid_Amt"]).Trim();
            txtBalanceAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Bal_Amt"]).Trim();
            txtDiretTaxAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["DirectTaxCollection_Amt"]).Trim();
            spnPOWOSignCopy.Visible = false;
            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["powo_signCopy"]).Trim() != "")
            {
                lnkfile_PO.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["powo_signCopy"]).Trim();
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


            if(isShortClosed==true)
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


        }

        get_Approver_List();
    }

    public void get_InvoiceDetails_MilestonesList_Update()
    {

        get_ProjectDept_Vendor_List();
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
                lstCostCenter.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Prj_Dept_id"]).Trim();
                lstDisplayPOTypes.SelectedValue=Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTypeID"]).Trim();
                lstDisplayPOTypes.Enabled = false;
                txtInvocieRemarks.Text= Convert.ToString(dsMilestone.Tables[0].Rows[0]["Invoice_Remarks"]).Trim(); 

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


                if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["powo_signCopy"]).Trim() != "")
                {
                    lnkfile_PO.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["powo_signCopy"]).Trim();
                    lnkfile_PO.Visible = true;
                    spnPOWOSignCopy.Visible = true;
                }

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

            txtMilestoneName_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MilestoneName"]).Trim();
            txtMilestoneAmt_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MilestoneAmt"]).Trim();
            txtMilestoneBalanceAmt_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["BalanceMilestoneAmt"]).Trim();
            txtMilestoneDirectTaxAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Collect_TDS_Amt"]).Trim();
            txtMilestonePaidAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MilestonePaidAmt"]).Trim();


            txtInvoiceNo.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["InvoiceNo"]).Trim();
            txtInvoiceDate.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["InvoiceDate"]).Trim();
            txtAmtWithOutTax.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["AmtWithoutTax"]).Trim();
            txtAmtWithTax_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["AmtWithTax"]).Trim();
            txtCGST_Per.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Per"]).Trim();
            txtSGST_Per.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Per"]).Trim();
            txtIGST_Per.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Per"]).Trim();

            txtCGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Amt"]).Trim();
            txtSGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Amt"]).Trim();
            txtIGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Amt"]).Trim();

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Per"]).Trim() != "" && Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Per"]).Trim() != "0.00")
            {
                txtIGST_Per.Enabled = false;
            }
            else if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Amt"]).Trim() != "" && Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Amt"]).Trim() != "0.00")
            {
                txtCGST_Per.Enabled = false;
                txtSGST_Per.Enabled = false;
            }

            hdnCGSTPer_O.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Per"]).Trim();
            hdnSGSTPer_O.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Per"]).Trim();
            hdnIGSTPer_O.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Per"]).Trim();

            lnkfile_Invoice.Visible = true;
            lnkfile_Invoice.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Invoice_File"]).Trim();

            lblheading.Text = "Invoice -" + txtInvoiceNo.Text + "  Status -" + Convert.ToString(dsMilestone.Tables[0].Rows[0]["InvoiceApprovalStatus"]).Trim();


            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["Status_id"]).Trim() == "3" || Convert.ToString(dsMilestone.Tables[0].Rows[0]["Status_id"]).Trim() == "4")
            {
                 
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

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["Status_id"]).Trim() == "2") //account approved or final approved invoice can not allowed to cancel
            {
                trvl_btnSave.Visible = false;
                 
                txtAmtWithOutTax.Enabled = false;
                txtInvoiceDate.Enabled = false;
                txtInvoiceNo.Enabled = false;
                txtCGST_Per.Enabled = false;
                txtSGST_Per.Enabled = false;
                txtIGST_Amt.Enabled = false;
                txtSupplierInvoiceDt.Enabled = false;
                txtVoucherNo.Enabled = false;
 

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

              
            }

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["InvoiceApprovalStatus"]).Trim() == "Correction")
            {
                trvl_btnSave.Visible = true;
            }

            if (Convert.ToString(hdnInvoiceApprStatusId.Value).Trim() == "2")
            {
                DgvApprover.DataSource = dsMilestone.Tables[3];
                DgvApprover.DataBind();
            }


        }




    }

    public void get_Approver_List()
    {
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();


        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), txtEmpCode.Text, hdnProject_Dept_Name.Value,Convert.ToString(lstPOType.SelectedValue));

        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
            DgvApprover.DataSource = dsMilestone.Tables[0];
            DgvApprover.DataBind();
            trvl_btnSave.Visible = true;
           // btnCancel.Visible = true;
            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["APPR_Emp_Code"]).Trim()=="99999999")
            {
                trvl_btnSave.Visible = false;
                
            }
        }
    }


    public void get_Approved_Reject_Approver_List()
    {
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();


        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), txtEmpCode.Text, hdnProject_Dept_Name.Value,Convert.ToString(lstPOType.SelectedValue));

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

        #region get Approver List as Per Invoice Type
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();


        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.get_Invoice_Approver_List(Convert.ToDouble(hdnInvoiceId.Value), txtEmpCode.Text, hdnProject_Dept_Name.Value, Convert.ToString(lstDisplayPOTypes.SelectedValue));

        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
            DgvApprover.DataSource = dsMilestone.Tables[0];
            DgvApprover.DataBind();
            trvl_btnSave.Visible = true;
           // btnCancel.Visible = true;
            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["APPR_Emp_Code"]).Trim() == "99999999")
            {
                trvl_btnSave.Visible = false;
                
            }
        }
        #endregion

    }

    protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
    {
        #region insert or upload multiple files
        string strInvoiceDate_FileName = "";
        string[] strdate; 
        if (Convert.ToString(txtInvoiceDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtInvoiceDate.Text).Trim().Split('-');          
            strInvoiceDate_FileName = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
        }

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
            // filename = Convert.ToString(txtInvoiceNo.Text).Trim().Replace("/", "-") + InputFile; // txtEmpCode.Text + "_" + Convert.ToString(dMaxReqId).Trim() + "_Milestone_PO" + InputFile;
            string sinvoice_no = Regex.Replace(Convert.ToString(txtInvoiceNo.Text), @"[^0-9a-zA-Z\._]", "_");
            filename = sinvoice_no + "_" + strInvoiceDate_FileName + InputFile;
            InvoiceUploadfile.SaveAs(Path.Combine(MilestoneFilePath, filename));
            spm.InsertInvoiceUploaded_Files(Convert.ToDouble(hdnInvoiceId.Value), "", Convert.ToString(filename).Trim(), "Uplaod_InvoiceFile_ACC", 0);
            Response.Redirect("~/procs/VSCB_MyInvoiceACC.aspx");
        }
        else
        {
            lblmessage.Text = "Please Select Invoice file";
        }

        #endregion
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }


        string strInvoiceDate_FileName = "";
        string[] strdate;
        if (Convert.ToString(txtInvoiceDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtInvoiceDate.Text).Trim().Split('-');
            strInvoiceDate_FileName = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
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
                            // strfileName = txtEmpCode.Text + "_" + Convert.ToString(hdnInvoiceId.Value).Trim() + "_Invoice" + (t_cnt).ToString() + InputFile;
                            // filename = strfileName;
                            string sinvoice_no = Regex.Replace(Convert.ToString(txtInvoiceNo.Text), @"[^0-9a-zA-Z\._]", "_");
                            filename = "Supporting_" + (t_cnt).ToString() + "_" + sinvoice_no + "_" + strInvoiceDate_FileName + InputFile;

                            uploadedFile.SaveAs(Path.Combine(InvoiceFilePath, filename));
                            spm.InsertInvoiceUploaded_Files(Convert.ToDouble(hdnInvoiceId.Value), "INSERT", Convert.ToString(filename).Trim(), "Invoice", t_cnt);

                            t_cnt = t_cnt + 1;
                        }



                    }
                }


            }


        }

        Response.Redirect("~/procs/VSCB_MyInvoiceACC.aspx");
    }



    
}