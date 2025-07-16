using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class procs_VSCB_POWODelete : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();

    #region Creative_Default_methods


    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
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

                    txtQty.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtRate.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");
                    txtCGSTPer.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");
                    txtSGSTPer.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");
                    txtIGSTPer.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");
                    txtMilestone.Attributes.Add("maxlength", txtMilestone.MaxLength.ToString());
                    txt_remakrs.Attributes.Add("maxlength", txt_remakrs.MaxLength.ToString());
                    txtMilestoneDueDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");

                    btnTra_Details.Visible = false;
                    //GetPODetails_FromTally();
                    getPOWOList_ForDelete();
                    GetPOTypes();
                    getLoginEmployeeDetails();

                    if (Request.QueryString.Count > 0)
                    {
                        hdnPOWOID.Value = Request.QueryString[0];
                        lblheading.Text = "PO / WO Detail - " + Request.QueryString[2];
                        hdnstype_Main.Value = "Update";

                       // GetPODetails_List();
                        get_PWODetails_MilestonesList_Update();


                        spnPOWOPaidAmt.Visible = true;
                        spnPOWOBalAmt.Visible = true;
                        txtPoPaidAmt_WithOutDT.Visible = true;
                        txtPOWOBalanceAmt.Visible = true;
                        txtDirectTaxAmt.Visible = true;
                        spnPOWODirectTaxAmt.Visible = true;
                        if (Convert.ToString(txtPOWOShortClosedAmt.Text).Trim() != "" && Convert.ToString(txtPOWOShortClosedAmt.Text).Trim() != "0.00")
                        {

                            txtPOWOShortClosedAmt.Visible = true;
                            spnPOWODShortClosedAmt.Visible = true;
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

    protected void lstTripType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtPOWODetails = new DataTable();
        clear_POWO_Cntrls();
        //delete Temp Milestone

        btnReject.Visible = true;

        DgvApprover.DataSource = null;
        DgvApprover.DataBind();

        dtPOWODetails = spm.getTallyPODetails(Convert.ToDouble(lstTripType.SelectedValue), Convert.ToString(txtEmpCode.Text).Trim(), "EN-IN");
        if (dtPOWODetails.Rows.Count > 0)
        {
            txtPOTitle.Text = Convert.ToString(dtPOWODetails.Rows[0]["TPOTitle"]);
            txtPOTitle.ToolTip = Convert.ToString(dtPOWODetails.Rows[0]["TPOTitle"]);

            txtFromdate.Text = Convert.ToString(dtPOWODetails.Rows[0]["TPODate"]);
            txtVendor.Text = Convert.ToString(dtPOWODetails.Rows[0]["TVendorName"]);
            txtGSTIN_No.Text = Convert.ToString(dtPOWODetails.Rows[0]["GSTIN_No"]);
            // txtPOWOAmt.Text = Convert.ToString(dtPOWODetails.Rows[0]["POWOAmt"]);
            txtPOStatus.Text = Convert.ToString(dtPOWODetails.Rows[0]["POStatus"]);
            txtProject.Text = Convert.ToString(dtPOWODetails.Rows[0]["Project_Name"]);
            txtPOWOBalanceAmt.Text = Convert.ToString(dtPOWODetails.Rows[0]["POWOAmt"]);
            txtBasePOWOWAmt.Text = Convert.ToString(dtPOWODetails.Rows[0]["POWOAmt"]);
            txtCostCenter.Text = Convert.ToString(dtPOWODetails.Rows[0]["costCenter"]);


            lstPOType.SelectedValue = Convert.ToString(dtPOWODetails.Rows[0]["POTypeId"]);
            txtPOtype.Text = Convert.ToString(dtPOWODetails.Rows[0]["POWOType"]);
            txtPOtype.ToolTip = Convert.ToString(dtPOWODetails.Rows[0]["POWOType"]);

            if (Convert.ToString(dtPOWODetails.Rows[0]["PO_WO_Text"]).Trim() != "")
            {
                lblPOWO_Content.InnerHtml = Convert.ToString(dtPOWODetails.Rows[0]["PO_WO_Text"]);
                liPOWOContent_1.Visible = false;
                liPOWOContent_2.Visible = false;
                liPOWOContent_3.Visible = false;
            }

            hdnCompCode.Value = Convert.ToString(dtPOWODetails.Rows[0]["Comp_Code"]);
            hdnPrj_Dept_Id.Value = Convert.ToString(dtPOWODetails.Rows[0]["Prj_Dept_Id"]);
            hdnVendorId.Value = Convert.ToString(dtPOWODetails.Rows[0]["VendorID"]);

            txtCGSTPer.Enabled = true;
            txtSGSTPer.Enabled = true;
            txtIGSTPer.Enabled = true;


            getApprovers_AsPerPOWOType();


            if (Convert.ToString(dtPOWODetails.Rows[0]["GSTIN_No"]) == "")
            {
                txtCGSTPer.Enabled = false;
                txtSGSTPer.Enabled = false;
                txtIGSTPer.Enabled = false;
            }



        }



        clear_Milestone_Cntrls();
        btnTra_Details.Visible = false;
        getMilestonesList();

    }
    protected void btnTra_Details_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(lstTripType.SelectedValue).Trim() == "0")
        {
            lblmessage.Text = "Please select PO/ WO Number";
            return;
        }
        trvldeatils_btnSave.Text = "Add Milestone";
        if (DivTrvl.Visible)
        {
            DivTrvl.Visible = false;
            btnTra_Details.Text = "+";
        }
        else
        {
            DivTrvl.Visible = true;
            btnTra_Details.Text = "-";
            spnMilestoneDirectTaxAmt.Visible = false;
            spnMilestonePaidAmt.Visible = false;
            spnMilestoneBalAmt.Visible = false;
            txtMilestonePaidAmt.Visible = false;
            txtMilestoneDirectTaxAmt.Visible = false;
            txtMilestoneBalanceAmt.Visible = false;

            txtMilestone.Enabled = true;
            txtQty.Enabled = true;
            txtRate.Enabled = true;
            txtCGSTPer.Enabled = true;
            txtSGSTPer.Enabled = true;
            txtIGSTPer.Enabled = true;


        }

        //if (btnTra_Details.Text == "+")
        //{
        //    trvl_btnSave.Visible = true;
        //    btnCancel.Visible = true;
        //    btnback_mng.Visible = true;
        //}
        //else
        //{
        //    trvl_btnSave.Visible = false;
        //    btnCancel.Visible = false;
        //    btnback_mng.Visible = false;
        //}
        clear_Milestone_Cntrls();


    }


    protected void trvldeatils_delete_btn_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow rowcolor in dgTravelRequest.Rows)
        {
            Boolean isShortClose = Convert.ToBoolean(dgTravelRequest.DataKeys[rowcolor.RowIndex].Values[2]);
            if (isShortClose == true)
            {
                rowcolor.BackColor = Color.FromName("#E56E94");
                LinkButton lnkTravelDetailsEdit = rowcolor.FindControl("lnkTravelDetailsEdit") as LinkButton;
                lnkTravelDetailsEdit.Visible = false;
            }
        }

        //validate Check any Invoice created tehn don't allowed to short close
        string strInvoice = "";
        #region get Invoice List
        DataTable dsInvoice = new DataTable();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Invoice_Nos_ForShortClose";

        spars[1] = new SqlParameter("@MstoneID", SqlDbType.Decimal);
        spars[1].Value = Convert.ToDecimal(hdnMstoneId.Value);

        spars[2] = new SqlParameter("@POWOID", SqlDbType.Decimal);
        spars[2].Value = Convert.ToDecimal(hdnPOWOID.Value);

        dsInvoice = spm.getDataList(spars, "SP_VSCB_GETALL_DETAILS");

        #endregion
        if (dsInvoice.Rows.Count > 0)
        {
            for (Int32 irow = 0; irow < dsInvoice.Rows.Count; irow++)
            {
                if (Convert.ToString(strInvoice).Trim() == "")
                    strInvoice = Convert.ToString(dsInvoice.Rows[irow]["InvoiceNo"]).Trim();
                else
                    strInvoice = strInvoice + ";" + Convert.ToString(dsInvoice.Rows[irow]["InvoiceNo"]).Trim();

            }

            if (Convert.ToString(strInvoice).Trim() != "")
            {
                lblmessage.Text = "you can't shortclose this milestone because some invoices are found under process.Please cancel this Invoices " + strInvoice;
                return;
            }
        }


        hdnIsShorClose.Value = "1";
        #region Short Close Milestone

        SqlParameter[] spars_m = new SqlParameter[3];

        spars_m[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars_m[0].Value = "ShortClose_Milestone";

        spars_m[1] = new SqlParameter("@MstoneID", SqlDbType.Decimal);
        spars_m[1].Value = Convert.ToDecimal(hdnMstoneId.Value);

        spars_m[2] = new SqlParameter("@POID", SqlDbType.Decimal);
        spars_m[2].Value = Convert.ToDecimal(hdnPOWOID.Value);


        dsInvoice = spm.getDataList(spars_m, "SP_VSCB_CreateMilestone_Details");

        DivTrvl.Visible = false;
        lilblShortClosemsg_1.Visible = true;
        lilblShortClosemsg_2.Visible = true;
        lilblShortClosemsg_2.Visible = true;
        lblShortCloseMsg.Visible = true;
        lblShortCloseMsg.Text = "Click on Submit button to save the changes. ";
        #endregion



    }

    protected void trvldeatils_cancel_btn_Click(object sender, EventArgs e)
    {
        DivTrvl.Visible = false;
        btnTra_Details.Text = "+";
        lblMilestoneMsg.Text = "";
        lblmessage.Text = "";
        trvldeatils_btnSave.Visible = true;
        //Response.Redirect("~/procs/travel_Exp.aspx");
        //Response.Redirect("~/procs/TravelRequest_exp.aspx?tripid=" + hdnTripid.Value + "&expid=" + hdnexp_id.Value);
    }



    protected void lnkTravelDetailsEdit_Click(object sender, EventArgs e)
    {

        // ImageButton btn = (ImageButton)sender;
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnSrno.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnpamentStatusid.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[1]).Trim();

        DivTrvl.Visible = true;
        btnTra_Details.Text = "-";

        lblShortCloseMsg.Visible = false;
        lilblShortClosemsg_1.Visible = false;
        lilblShortClosemsg_2.Visible = false;
        lilblShortClosemsg_3.Visible = false;

        foreach (GridViewRow rowcolor in dgTravelRequest.Rows)
        {
            Boolean isShortClose = Convert.ToBoolean(dgTravelRequest.DataKeys[rowcolor.RowIndex].Values[2]);
            if (isShortClose == true)
            {
                rowcolor.BackColor = Color.FromName("#E56E94");
                LinkButton lnkTravelDetailsEdit = rowcolor.FindControl("lnkTravelDetailsEdit") as LinkButton;
                lnkTravelDetailsEdit.Visible = false;
            }
        }



        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.getDeletePOWOFrom_Temptable_edit(txtEmpCode.Text, Convert.ToInt32(hdnSrno.Value));

        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToString(hdnPOWOID.Value).Trim() != "")
            {
                spnMilestoneBalAmt.Visible = true;
                spnMilestonePaidAmt.Visible = true;
                txtMilestonePaidAmt.Visible = true;
                txtMilestoneBalanceAmt.Visible = true;
                spnMilestoneDirectTaxAmt.Visible = true;
                txtMilestoneDirectTaxAmt.Visible = true;
            }

            txtMilestone.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MilestoneName"]).Trim();
            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Per"]).Trim() != "0.00")
                txtCGSTPer.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Per"]).Trim();

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Per"]).Trim() != "0.00")
                txtSGSTPer.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Per"]).Trim();

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Per"]).Trim() != "0.00")
                txtIGSTPer.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Per"]).Trim();

            txtAmtWithTax.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["AmtWithTax"]).Trim();
            txtAmount.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Amount"]).Trim();
            txtRate.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Rate"]).Trim();
            txtQty.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Quantity"]).Trim();
            txtMilestoneDueDate.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Milestone_due_date"]);


            txtMilestonePaidAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MilestonePaidAmt"]).Trim();
            txtMilestoneBalanceAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Milesstone_Balance_Amt"]).Trim();
            txtMilestoneDirectTaxAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Collect_TDS_Amt"]).Trim();

            txtCGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Amt"]).Trim();
            txtSGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Amt"]).Trim();
            txtIGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Amt"]).Trim();


            trvldeatils_btnSave.Text = "Update Milestone";
            //trvldeatils_delete_btn.Visible = true;
            trvldeatils_btnSave.Visible = true;
            txtMilestone.Enabled = true;
            txtCGSTPer.Enabled = true;
            txtSGSTPer.Enabled = true;
            txtIGSTPer.Enabled = true;


            txtAmount.Enabled = true;
            txtRate.Enabled = true;
            txtQty.Enabled = true;
            txtAmtWithTax.Enabled = false;
            txtAmtWithTax.ReadOnly = false;
            txtMilestoneDueDate.Enabled = false;
            txtAmount.Enabled = false;
            txtAmount.ReadOnly = false;

            hdnMstoneId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MstoneID"]).Trim();
            if (Convert.ToBoolean(dsMilestone.Tables[0].Rows[0]["IsShortClone"]) == true)
            {
                trvldeatils_delete_btn.Visible = false;
                trvldeatils_btnSave.Visible = false;

                txtMilestone.Enabled = false;
                txtCGSTPer.Enabled = false;
                txtSGSTPer.Enabled = false;
                txtIGSTPer.Enabled = false;

                txtAmtWithTax.Enabled = false;
                txtAmount.Enabled = false;
                txtRate.Enabled = false;
                txtQty.Enabled = false;
            }

            if (Convert.ToString(hdnAppr_StatusId.Value).Trim() == "2" || Convert.ToString(hdnAppr_StatusId.Value).Trim() == "4")
            {
                trvldeatils_btnSave.Visible = false;
                txtQty.Enabled = false;
                txtRate.Enabled = false;
                txtCGSTPer.Enabled = false;
                txtSGSTPer.Enabled = false;
                txtIGSTPer.Enabled = false;
                txtMilestone.Enabled = false;
            }


        }
        if (dsMilestone.Tables[1].Rows.Count > 0)
        {
            if (Convert.ToString(dsMilestone.Tables[1].Rows[0]["InvoiceID"]).Trim() != "")
            {
                trvldeatils_btnSave.Visible = false;
                txtMilestone.Enabled = false;
                txtCGSTPer.Enabled = false;
                txtSGSTPer.Enabled = false;
                txtIGSTPer.Enabled = false;

                txtAmtWithTax.Enabled = false;
                txtAmount.Enabled = false;
                txtRate.Enabled = false;
                txtQty.Enabled = false;
            }
        }


        txtCGSTPer.Enabled = false;
        txtSGSTPer.Enabled = false;
        txtIGSTPer.Enabled = false;
        txtRate.Enabled = false;
        txtQty.Enabled = false;
        txtMilestone.Enabled = false;
        trvldeatils_btnSave.Visible = false;

    }

    protected void trvl_btnSave_Click(object sender, EventArgs e)
    {

        //DataSet dsCCMailID = new DataSet();
        //SqlParameter[] sparss = new SqlParameter[3];

        //sparss[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        //sparss[0].Value = "get_POWOList_DeleteEmailCC";
        //sparss[1] = new SqlParameter("@POWOID", SqlDbType.VarChar);
        //sparss[1].Value = lstTripType.SelectedValue;
        //sparss[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        //sparss[2].Value = Convert.ToString(Session["Empcode"]);
        //dsCCMailID = spm.getDatasetList(sparss, "SP_VSCB_GETALL_DETAILS");


            DataSet dsApprovedPOWO = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "POWOApproved_Delete";
            spars[1] = new SqlParameter("@POWOID", SqlDbType.VarChar);
            spars[1].Value = lstTripType.SelectedValue;
            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(Session["Empcode"]);

            dsApprovedPOWO = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
            lblmessage.Text = "";

        //string strSubject = "OneHR: Delete PO / WO - " + Convert.ToString(lstTripType.SelectedItem.Text).Trim();
        //string ToEmailAddress = dsCCMailID.Tables[1].Rows[0]["ToEmp_Emailaddress"].ToString();
        //string CCEmailAddress = dsCCMailID.Tables[0].Rows[0]["CCEmailAddress"].ToString();
        //string StrContain = "";

        //spm.send_mailto_POWODelete(ToEmailAddress, CCEmailAddress, strSubject, StrContain);

        if (dsApprovedPOWO != null)
        {
            if (dsApprovedPOWO.Tables[0].Rows.Count > 0)
                Response.Redirect("VSCB_ApprovedPOWOList.aspx?isdelete=y");
            else
                Response.Redirect("VSCB_ApprovedPOWOList.aspx");
        }
        else
        {
            Response.Redirect("VSCB_ApprovedPOWOList.aspx");
        }
    }

   

    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        lblMilestoneMsg.Text = "";

        if (Convert.ToString(txtQty.Text).Trim() == "")
        {
            lblMilestoneMsg.Text = "Please enter correct Quantity.";
            return;
        }

        if (Convert.ToString(txtRate.Text).Trim() == "")
        {
            lblMilestoneMsg.Text = "Please enter correct Rate.";
            return;
        }

        if (Convert.ToString(txtRate.Text).Trim() != "")
        {
            string[] strdate;
            strdate = Convert.ToString(txtRate.Text).Trim().Split('.');
            if (strdate.Length > 2)
            {
                txtRate.Text = "0";
                txtAmount.Text = "0";
                lblMilestoneMsg.Text = "Please enter correct Rate.";
                return;
            }

            Decimal dfare = 0;
            dfare = Convert.ToDecimal(txtRate.Text);
            if (dfare == 0)
            {
                txtAmount.Text = "0";
                lblMilestoneMsg.Text = "Please enter correct Rate.";
                return;
            }
        }

        Decimal first_number = 0;
        Decimal second_number = 0;
        Decimal result = 0;
        first_number = Convert.ToDecimal(txtQty.Text);
        second_number = Convert.ToDecimal(txtRate.Text);

        result = first_number * second_number;

        txtAmount.Text = Convert.ToString(Math.Round(result, 2));
        Decimal rCGST = 0;
        Decimal rSGST = 0;
        Decimal rIGST = 0;

        if (Convert.ToString(txtCGSTPer.Text).Trim() != "")
        {
            rCGST = Math.Round(Convert.ToDecimal(txtCGSTPer.Text) * Convert.ToDecimal(txtAmount.Text) / 100, 2);
        }
        if (Convert.ToString(txtSGSTPer.Text).Trim() != "")
        {
            rSGST = Math.Round(Convert.ToDecimal(txtSGSTPer.Text) * Convert.ToDecimal(txtAmount.Text) / 100, 2);
        }
        if (Convert.ToString(txtIGSTPer.Text).Trim() != "")
        {
            rIGST = Math.Round(Convert.ToDecimal(txtIGSTPer.Text) * Convert.ToDecimal(txtAmount.Text) / 100, 2);
        }

        txtAmtWithTax.Text = Convert.ToString(rCGST + rSGST + rIGST + Convert.ToDecimal(txtAmount.Text)).Trim();

        if (OverallPOWOAmount_Validation() == false)
        {
            return;
        }


    }

    protected void txtCGSTPer_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txtCGSTPer.Text).Trim() == "" || Convert.ToString(txtCGSTPer.Text).Trim() == "0")
        {
            txtCGSTPer.Text = "";
            txtSGSTPer.Text = "";
            txtIGSTPer.Text = "";
            txtIGSTPer.Enabled = true;
        }
        else
        {
            txtSGSTPer.Text = Convert.ToString(txtCGSTPer.Text).Trim();
            txtIGSTPer.Enabled = false;

        }
        Check_get_GSTPercentage();
    }

    protected void txtSGSTPer_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txtSGSTPer.Text).Trim() == "" || Convert.ToString(txtSGSTPer.Text).Trim() == "0")
        {
            txtCGSTPer.Text = "";
            txtSGSTPer.Text = "";
            txtIGSTPer.Text = "";
            txtIGSTPer.Enabled = true;
        }
        else
        {
            txtCGSTPer.Text = Convert.ToString(txtSGSTPer.Text).Trim();
            txtIGSTPer.Enabled = false;

        }
        Check_get_GSTPercentage();
    }

    protected void txtIGSTPer_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txtIGSTPer.Text).Trim() != "")
        {
            txtCGSTPer.Text = "";
            txtSGSTPer.Text = "";
            txtCGSTPer.Enabled = false;
            txtSGSTPer.Enabled = false;
        }
        else
        {
            txtCGSTPer.Text = "";
            txtSGSTPer.Text = "";
            txtCGSTPer.Enabled = true;
            txtSGSTPer.Enabled = true;


        }
        Check_get_GSTPercentage();
    }

    protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
    {
        lblMilestoneMsg.Text = "";

        #region Add or Edit Milestone Validation

        string sMiletoneDueDate = "";
        string[] strMilestoneDuedate;

        if (Convert.ToString(txtMilestone.Text).Trim() == "")
        {
            lblMilestoneMsg.Text = "Please enter Milestone Particulars.";
            return;
        }
        if (Convert.ToString(txtMilestoneDueDate.Text).Trim() == "")
        {
            lblMilestoneMsg.Text = "Please enter Milestone Due Date.";
            return;
        }
        if (Convert.ToString(txtQty.Text).Trim() == "")
        {
            lblMilestoneMsg.Text = "Please enter Quantity.";
            return;
        }
        if (Convert.ToString(txtRate.Text).Trim() == "")
        {
            lblMilestoneMsg.Text = "Please enter Rate.";
            return;
        }
        if (Convert.ToString(txtAmount.Text).Trim() == "" || Convert.ToString(txtAmount.Text).Trim() == "0")
        {
            return;
        }

        if (Convert.ToString(txtRate.Text).Trim() != "")
        {
            string[] strdate;
            strdate = Convert.ToString(txtRate.Text).Trim().Split('.');
            if (strdate.Length > 2)
            {
                txtRate.Text = "0";
                lblMilestoneMsg.Text = "Please enter correct Rate.";
                return;
            }

            Decimal dfare = 0;
            dfare = Convert.ToDecimal(txtRate.Text);
            if (dfare == 0)
            {
                lblMilestoneMsg.Text = "Please enter correct Rate.";
                return;
            }
        }

        if (Convert.ToString(txtCGSTPer.Text).Trim() != "")
        {
            string[] strdate;
            strdate = Convert.ToString(txtCGSTPer.Text).Trim().Split('.');
            if (strdate.Length > 2)
            {
                txtCGSTPer.Text = "0";
                txtAmtWithTax.Text = "0";
                lblMilestoneMsg.Text = "Please enter correct CGST Percentage.";
                return;
            }

            Decimal dfare = 0;
            dfare = Convert.ToDecimal(txtCGSTPer.Text);
            if (dfare == 0)
            {
                lblMilestoneMsg.Text = "Please enter correct CGST Percentage.";
                return;
            }
        }


        if (Convert.ToString(txtSGSTPer.Text).Trim() != "")
        {
            string[] strdate;
            strdate = Convert.ToString(txtSGSTPer.Text).Trim().Split('.');
            if (strdate.Length > 2)
            {
                txtSGSTPer.Text = "0";
                txtAmtWithTax.Text = "0";
                lblMilestoneMsg.Text = "Please enter correct SGST Percentage.";
                return;
            }

            Decimal dfare = 0;
            dfare = Convert.ToDecimal(txtSGSTPer.Text);
            if (dfare == 0)
            {
                lblMilestoneMsg.Text = "Please enter correct SGST Percentage.";
                return;
            }
        }

        if (Convert.ToString(txtIGSTPer.Text).Trim() != "")
        {
            string[] strdate;
            strdate = Convert.ToString(txtIGSTPer.Text).Trim().Split('.');
            if (strdate.Length > 2)
            {
                txtIGSTPer.Text = "0";
                txtAmtWithTax.Text = "0";
                lblMilestoneMsg.Text = "Please enter correct IGST Percentage.";
                return;
            }

            Decimal dfare = 0;
            dfare = Convert.ToDecimal(txtIGSTPer.Text);
            if (dfare == 0)
            {
                lblMilestoneMsg.Text = "Please enter correct IGST Percentage.";
                return;
            }
        }

        if (Convert.ToString(txtBasePOWOWAmt.Text).Trim() == "")
        {
            lblMilestoneMsg.Text = "Please check PO/ WO Amount (Without GST).";
            return;
        }

        #region Milestone Due Date Vaidation

        DataSet dsEmployee = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        if (Convert.ToString(txtMilestoneDueDate.Text).Trim() != "")
        {
            strMilestoneDuedate = Convert.ToString(txtMilestoneDueDate.Text).Trim().Split('/');
            sMiletoneDueDate = Convert.ToString(strMilestoneDuedate[2]) + "-" + Convert.ToString(strMilestoneDuedate[1]) + "-" + Convert.ToString(strMilestoneDuedate[0]);
        }

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Check_MilestoneDate_Validation";

        spars[1] = new SqlParameter("@InvoiceDate", SqlDbType.VarChar);
        spars[1].Value = sMiletoneDueDate;

        DataSet dsMilestoneduedate = new DataSet();
        dsMilestoneduedate = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        if (dsMilestoneduedate.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToString(dsMilestoneduedate.Tables[0].Rows[0]["emsg"]).Trim() == "yes")
            {
                lblMilestoneMsg.Text = "Milestone Due date back dated not allowed.";
                return;
            }
        }

        #endregion

        #endregion

        #region Check Milestone Amount with POWO Amount
        //  DataTable dtMilestoneAmount = spm.getMilestoneAmount(Convert.ToString(lstTripType.SelectedItem.Text).Trim());

        // decimal dPOWOBalanceAmt = 0;
        decimal dTotalMilestoneAmount = 0;
        Decimal dPOWOAmount = 0;

        //if (dtMilestoneAmount.Rows.Count > 0)
        //{

        //    dPOWOBalanceAmt = Convert.ToDecimal(dtMilestoneAmount.Rows[0]["PO_Bal_Amt"]);
        //}
        //Decimal dPOWOAmount =  dPOWOBalanceAmt; 
        //if(dPOWOBalanceAmt==0)
        //{

        //    dPOWOAmount = Math.Round(Convert.ToDecimal(txtBasePOWOWAmt.Text), 2);
        //}
        dPOWOAmount = Math.Round(Convert.ToDecimal(txtBasePOWOWAmt.Text), 2);

        foreach (GridViewRow row in dgTravelRequest.Rows)
        {

            if (Convert.ToString(row.Cells[7].Text).Trim() != "")
            {
                if (Convert.ToString(hdnSrno.Value).Trim() != Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[0]).Trim())
                    dTotalMilestoneAmount += Convert.ToDecimal(row.Cells[7].Text);
            }
        }
        if (Convert.ToString(txtAmount.Text).Trim() != "")
        {
            dTotalMilestoneAmount += Convert.ToDecimal(txtAmount.Text);
        }

        if (dTotalMilestoneAmount > dPOWOAmount)
        {
            lblMilestoneMsg.Text = "Overall Milestone amount Exceeding PO/WO Amount. Please correct and try again!";
            return;
        }

        if (dTotalMilestoneAmount >= dPOWOAmount)
        {
            btnTra_Details.Visible = false;
        }
        #endregion

        #region Insert Milestone details on temp Table
        Decimal dAmount = 0;
        decimal drate = 0;
        decimal dCGST_Per = 0;
        decimal dSGST_Per = 0;
        decimal dIGST_Per = 0;
        decimal dCGST_Amt = 0;
        decimal dSGST_Amt = 0;
        decimal dIGST_Amt = 0;


        if (Convert.ToString(txtMilestoneDueDate.Text).Trim() != "")
        {
            strMilestoneDuedate = Convert.ToString(txtMilestoneDueDate.Text).Trim().Split('/');
            sMiletoneDueDate = Convert.ToString(strMilestoneDuedate[2]) + "-" + Convert.ToString(strMilestoneDuedate[1]) + "-" + Convert.ToString(strMilestoneDuedate[0]);
        }

        string stype = "InsertTempTable";
        if (trvldeatils_btnSave.Text == "Update Milestone")
            stype = "UpdateTempTable";

        if (Convert.ToString(txtAmount.Text).Trim() != "")
            dAmount = Math.Round(Convert.ToDecimal(txtAmount.Text), 2);

        decimal dAmountWithTax = 0;

        if (Convert.ToString(txtRate.Text).Trim() != "")
            drate = Math.Round(Convert.ToDecimal(txtRate.Text), 2);

        if (Convert.ToString(txtCGSTPer.Text).Trim() != "")
            dCGST_Per = Math.Round(Convert.ToDecimal(txtCGSTPer.Text), 2);
        if (Convert.ToString(txtSGSTPer.Text).Trim() != "")
            dSGST_Per = Math.Round(Convert.ToDecimal(txtSGSTPer.Text), 2);
        if (Convert.ToString(txtIGSTPer.Text).Trim() != "")
            dIGST_Per = Math.Round(Convert.ToDecimal(txtIGSTPer.Text), 2);

        if (Convert.ToString(txtCGSTPer.Text).Trim() != "")
        {
            dCGST_Amt = Math.Round(Convert.ToDecimal(txtCGSTPer.Text) * Convert.ToDecimal(txtAmount.Text) / 100, 2);
        }
        if (Convert.ToString(txtSGSTPer.Text).Trim() != "")
        {
            dSGST_Amt = Math.Round(Convert.ToDecimal(txtSGSTPer.Text) * Convert.ToDecimal(txtAmount.Text) / 100, 2);
        }
        if (Convert.ToString(txtIGSTPer.Text).Trim() != "")
        {
            dIGST_Amt = Math.Round(Convert.ToDecimal(txtIGSTPer.Text) * Convert.ToDecimal(txtAmount.Text) / 100, 2);
        }

        if (Convert.ToString(txtAmtWithTax.Text).Trim() != "")
        {
            dAmountWithTax = Math.Round(Convert.ToDecimal(txtAmtWithTax.Text), 2);
        }

        int paymentstatusid = 1;
        bool IsShortClone = false;
        double dMilestoneId = 0;
        if (Convert.ToString(hdnIsShorClose.Value).Trim() == "1")
        {
            IsShortClone = true;
        }

        spm.InsertMilestoneDetails(Convert.ToString(hdnSrno.Value), Convert.ToDouble(lstTripType.SelectedValue), Convert.ToString(txtMilestone.Text).Trim(), Convert.ToInt32(txtQty.Text), drate, dAmount, dCGST_Per, dSGST_Per, dIGST_Per, dCGST_Amt, dSGST_Amt, dIGST_Amt, dAmountWithTax, Convert.ToString(txtEmpCode.Text), stype, paymentstatusid, IsShortClone, dMilestoneId, "", sMiletoneDueDate);
        DivTrvl.Visible = false;
        btnTra_Details.Text = "+";


        getMilestonesList();




        #endregion
    }

    #endregion

    #region PageMethods

    private Boolean OverallPOWOAmount_Validation()
    {
        Boolean IsValid = true;
        #region Check Milestone Amount with POWO Amount
        //  DataTable dtMilestoneAmount = spm.getMilestoneAmount(Convert.ToString(lstTripType.SelectedItem.Text).Trim());

        // decimal dPOWOBalanceAmt = 0;
        decimal dTotalMilestoneAmount = 0;
        Decimal dPOWOAmount = 0;

        //if (dtMilestoneAmount.Rows.Count > 0)
        //{

        //    dPOWOBalanceAmt = Convert.ToDecimal(dtMilestoneAmount.Rows[0]["PO_Bal_Amt"]);
        //}
        //Decimal dPOWOAmount =  dPOWOBalanceAmt; 
        //if(dPOWOBalanceAmt==0)
        //{

        //    dPOWOAmount = Math.Round(Convert.ToDecimal(txtBasePOWOWAmt.Text), 2);
        //}
        dPOWOAmount = Math.Round(Convert.ToDecimal(txtBasePOWOWAmt.Text), 2);

        foreach (GridViewRow row in dgTravelRequest.Rows)
        {

            if (Convert.ToString(row.Cells[7].Text).Trim() != "")
            {
                if (Convert.ToString(hdnSrno.Value).Trim() != Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[0]).Trim())
                    dTotalMilestoneAmount += Convert.ToDecimal(row.Cells[7].Text);
            }
        }
        if (Convert.ToString(txtAmount.Text).Trim() != "")
        {
            dTotalMilestoneAmount += Convert.ToDecimal(txtAmount.Text);
        }

        if (dTotalMilestoneAmount > dPOWOAmount)
        {
            lblMilestoneMsg.Text = "Overall Milestone amount Exceeding PO/WO Amount. Please correct and try again!";
            IsValid = false;
        }

        if (dTotalMilestoneAmount >= dPOWOAmount)
        {
            btnTra_Details.Visible = false;
        }
        #endregion

        return IsValid;
    }
    private void Check_get_GSTPercentage()
    {
        lblMilestoneMsg.Text = "";
        #region GST Percentage Validation


        if (Convert.ToString(txtCGSTPer.Text).Trim() != "")
        {
            string[] strdate;
            strdate = Convert.ToString(txtCGSTPer.Text).Trim().Split('.');
            if (strdate.Length > 2)
            {
                txtCGSTPer.Text = "0";
                txtAmtWithTax.Text = "0";
                lblMilestoneMsg.Text = "Please enter correct CGST Percentage.";
                return;
            }

            //Decimal dfare = 0;
            //dfare = Convert.ToDecimal(txtCGSTPer.Text);
            //if (dfare == 0)
            //{
            //    lblmessage.Text = "Please enter correct CGST Percentage.";
            //    return;
            //}
        }


        if (Convert.ToString(txtSGSTPer.Text).Trim() != "")
        {
            string[] strdate;
            strdate = Convert.ToString(txtSGSTPer.Text).Trim().Split('.');
            if (strdate.Length > 2)
            {
                txtSGSTPer.Text = "0";
                txtAmtWithTax.Text = "0";
                lblMilestoneMsg.Text = "Please enter correct SGST Percentage.";
                return;
            }

            //Decimal dfare = 0;
            //dfare = Convert.ToDecimal(txtSGSTPer.Text);
            //if (dfare == 0)
            //{
            //    lblmessage.Text = "Please enter correct SGST Percentage.";
            //    return;
            //}
        }

        if (Convert.ToString(txtIGSTPer.Text).Trim() != "")
        {
            string[] strdate;
            strdate = Convert.ToString(txtIGSTPer.Text).Trim().Split('.');
            if (strdate.Length > 2)
            {
                txtIGSTPer.Text = "0";
                txtAmtWithTax.Text = "0";
                lblMilestoneMsg.Text = "Please enter correct IGST Percentage.";
                return;
            }

            //Decimal dfare = 0;
            //dfare = Convert.ToDecimal(txtIGSTPer.Text);
            //if (dfare == 0)
            //{
            //    lblmessage.Text = "Please enter correct IGST Percentage.";
            //    return;
            //}
        }

        #endregion

        Decimal rCGST = 0;
        Decimal rSGST = 0;
        Decimal rIGST = 0;

        Decimal first_number = 0;
        Decimal second_number = 0;
        Decimal result = 0;
        if (Convert.ToString(txtQty.Text).Trim() != "")
            first_number = Convert.ToDecimal(txtQty.Text);

        if (Convert.ToString(txtRate.Text).Trim() != "")
            second_number = Convert.ToDecimal(txtRate.Text);

        result = first_number * second_number;

        txtAmount.Text = Convert.ToString(Math.Round(result, 2));

        if (Convert.ToString(txtCGSTPer.Text).Trim() != "")
        {
            rCGST = Math.Round(Convert.ToDecimal(txtCGSTPer.Text) * Convert.ToDecimal(txtAmount.Text) / 100, 2);
        }
        if (Convert.ToString(txtSGSTPer.Text).Trim() != "")
        {
            rSGST = Math.Round(Convert.ToDecimal(txtSGSTPer.Text) * Convert.ToDecimal(txtAmount.Text) / 100, 2);
        }
        if (Convert.ToString(txtIGSTPer.Text).Trim() != "")
        {
            rIGST = Math.Round(Convert.ToDecimal(txtIGSTPer.Text) * Convert.ToDecimal(txtAmount.Text) / 100, 2);
        }

        txtCGST_Amt.Text = Convert.ToString(rCGST).Trim();
        txtSGST_Amt.Text = Convert.ToString(rSGST).Trim();
        txtIGST_Amt.Text = Convert.ToString(rIGST).Trim();
        txtAmtWithTax.Text = Convert.ToString(rCGST + rSGST + rIGST + Convert.ToDecimal(txtAmount.Text)).Trim();


        if (OverallPOWOAmount_Validation() == false)
        {
            return;
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
        txtPOWOBalanceAmt.Text = "";
        txtPOWOPaidAmt.Text = "";
        txtPoPaidAmt_WithOutDT.Text = "";


        lstPOType.SelectedValue = "0";

    }
    public void clear_Milestone_Cntrls()
    {
        lblMilestoneMsg.Text = "";
        lblmessage.Text = "";
        txtMilestone.Text = "";
        txtCGSTPer.Text = "";
        txtSGSTPer.Text = "";
        txtIGSTPer.Text = "";
        txtAmtWithTax.Text = "";
        txtAmount.Text = "";
        txtRate.Text = "";
        txtQty.Text = "";
        hdnSrno.Value = "";
        txtMilestoneDirectTaxAmt.Text = "";
        txtMilestoneBalanceAmt.Text = "";
        txtMilestonePaidAmt.Text = "";
        txtCGST_Amt.Text = "";
        txtSGST_Amt.Text = "";
        txtIGST_Amt.Text = "";
        txtMilestoneDueDate.Text = "";

        //    trvldeatils_btnSave.Text = "Submit";

    }
    public void GetPODetails_FromTally()
    {
        DataTable dtPOWODetails = new DataTable();
        dtPOWODetails = spm.getTallyPOWOList(Convert.ToString(txtEmpCode.Text).Trim());
        if (dtPOWODetails.Rows.Count > 0)
        {
            lstTripType.DataSource = dtPOWODetails;
            lstTripType.DataTextField = "TPONumber";
            lstTripType.DataValueField = "TID";
            lstTripType.DataBind();
            lstTripType.Items.Insert(0, new ListItem("Select PO/WO Number", "0"));

        }


    }

    public void GetPODetails_List()
    {
        DataTable dtPOWODetails = new DataTable();
        dtPOWODetails = spm.getPOWOList(txtEmpCode.Text);
        if (dtPOWODetails.Rows.Count > 0)
        {
            lstTripType.DataSource = dtPOWODetails;
            lstTripType.DataTextField = "PONumber";
            lstTripType.DataValueField = "POID";
            lstTripType.DataBind();
            lstTripType.Items.Insert(0, new ListItem("Select PO/WO Number", "0"));

        }
    }


    public void getPOWOList_ForDelete()
    {
        DataSet dsEmployee = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_PO_Numbers_List_delete";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode.Text);

        dsEmployee = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        if (dsEmployee.Tables[0].Rows.Count > 0)
        {
            lstTripType.DataSource = dsEmployee.Tables[0];
            lstTripType.DataTextField = "PONumber";
            lstTripType.DataValueField = "POID";
            lstTripType.DataBind();
            lstTripType.Items.Insert(0, new ListItem("Select PO/WO Number", "0"));
        }
    }

    public void getLoginEmployeeDetails()
    {
        DataSet dsEmployee = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_employee_info";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode.Text);

        dsEmployee = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        if (dsEmployee.Tables[0].Rows.Count > 0)
        {
            txtEmpName.Text = Convert.ToString(dsEmployee.Tables[0].Rows[0]["Emp_Name"]).Trim();
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
        dsMilestone = spm.getMilestoneFrom_Temptable(txtEmpCode.Text, Convert.ToDouble(lstTripType.SelectedValue), "EN-IN");

        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
            dgTravelRequest.DataSource = dsMilestone.Tables[0];
            dgTravelRequest.DataBind();


        }
    }

    public void get_PWODetails_MilestonesList_Update()
    {

        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.getDelete_edit_View(txtEmpCode.Text, Convert.ToDouble(hdnPOWOID.Value));

        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
            Boolean blnIsShortclose = false;
            hdnPOWOID.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POID"]).Trim();
            hdnMstoneId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POID"]).Trim();
            txtFromdate.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PODate"]);
            txtProject.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Project_Name"]).Trim();
            txtPOStatus.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PyamentStatus"]).Trim();
            hdnPOTypeId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTypeID"]).Trim();
            txtPOTitle.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTitle"]).Trim();
            txtPOTitle.ToolTip = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTitle"]).Trim();
            txtVendor.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Name"]).Trim();
            txtGSTIN_No.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["GSTIN_NO"]).Trim();
            txtPOWOAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POWOAmt"]).Trim();
            txtBasePOWOWAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POWO_T_BaseAmt"]).Trim();
            lstPOType.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTypeID"]).Trim();
            lstTripType.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POID"]).Trim();
            txtPOWOPaidAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Paid_Amt"]).Trim();
            txtPoPaidAmt_WithOutDT.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POPiadAmount_withoutDT"]).Trim();
            txtPOWOBalanceAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Bal_Amt"]).Trim();
            txtDirectTaxAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["DirectTaxCollection_Amt"]).Trim();
            txtCostCenter.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["costCenter"]).Trim();

            txtPOWOShortClosedAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["ShortClose_Amt"]).Trim();

            txtPOtype.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POType"]);
            txtPOtype.ToolTip = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POType"]);

            txt_POWOContent_description.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_WO_Text"]).Trim();
            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["Remarks"]).Trim() != "")
            {
                txt_remakrs.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Remarks"]).Trim();
                txt_remakrs.Enabled = false;
            }

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_WO_Text"]).Trim() != "")
            {
                // lblPOWO_Content.InnerHtml = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_WO_Text"]).Trim();
                liPOWOContent_1.Visible = false;
                liPOWOContent_2.Visible = false;
                liPOWOContent_3.Visible = false;

            }

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["appr_StatusId"]).Trim() == "2")
            {
                if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["powo_signCopy"]).Trim() != "")
                {
                    lnkViewPOWO_SignCopy.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["powo_signCopy"]).Trim();
                    liViewPOWO_SignCopy_1.Visible = true;
                    liViewPOWO_SignCopy_2.Visible = true;
                    liViewPOWO_SignCopy_3.Visible = true;
                    lnkViewPOWO_SignCopy.Visible = true;
                }

                btnCorrection.Visible = true;
            }

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["appr_StatusId"]).Trim() == "1")
            {
                btnApprove.Visible = true;
            }
            hdnCondintionId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["cond_id"]).Trim();
            hdnRangeId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Range_type_id"]).Trim();
            hdnAppr_StatusId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["appr_StatusId"]).Trim();

            hdnCompCode.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Comp_Code"]).Trim();
            hdnVendorId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["VendorID"]).Trim();
            hdnPrj_Dept_Id.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Prj_Dept_Id"]).Trim();
            lnkfile_PO.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_File"]).Trim();

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["IsShortClose"]).Trim() != "")
                blnIsShortclose = Convert.ToBoolean(dsMilestone.Tables[0].Rows[0]["IsShortClose"]);




            lstTripType.Enabled = false;
            lstPOType.Enabled = false;
            lnkfile_PO.Visible = true;

            spnPOWOStatus.Visible = true;
            txtPOStatus.Visible = true;
            spnAmountWithTax.Visible = true;
            txtPOWOAmt.Visible = true;
           

            dgTravelRequest.DataSource = null;
            dgTravelRequest.DataBind();

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["POWO_T_BaseAmt"]).Trim() == Convert.ToString(dsMilestone.Tables[0].Rows[0]["TotalMilestoneAmt"]).Trim())
            {
                btnTra_Details.Visible = false;
            }

            if (dsMilestone.Tables[1].Rows.Count > 0)
            {
                dgTravelRequest.DataSource = dsMilestone.Tables[1];
                dgTravelRequest.DataBind(); 
            }

            if (dsMilestone.Tables[5].Rows.Count > 0)
            {
                spnPaymentHistory.Visible = true;
                dgPaymentHistory.DataSource = dsMilestone.Tables[5];
                dgPaymentHistory.DataBind();
            }


            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["appr_StatusId"]).Trim() == "1")
            {
              
            }

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["appr_StatusId"]).Trim() == "2")
            {
                //If uncomment when if Short close Milestone allowed.
               
               

            }

            if (Convert.ToString(txtPOWOBalanceAmt.Text).Trim() != "")
            {
                if (Convert.ToDecimal(txtPOWOBalanceAmt.Text) <= 0)
                {
                    //If uncomment when if Short close Milestone allowed.
                  
                  //  trvl_btnSave.Visible = false;
                }

            }
            getMilestoneUploadedFiles();

            #region get Approver as per amount

            //get Approver List
            DataSet dsApprover = null;
            DgvApprover.DataSource = null;
            DgvApprover.DataBind();

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["appr_StatusId"]).Trim() == "2")
            {
                DgvApprover.DataSource = dsMilestone.Tables[4];
                DgvApprover.DataBind();
            }
            else
            {
                dsApprover = spm.get_POWO_Approver_List(Convert.ToDouble(hdnPOWOID.Value), Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(txtProject.Text).Trim(), Convert.ToDecimal(txtBasePOWOWAmt.Text), Convert.ToInt32(lstPOType.SelectedValue));
            }
            if (dsApprover != null)
            {

                if (dsApprover.Tables[0].Rows.Count > 0)
                {
                    DgvApprover.DataSource = dsApprover.Tables[0];
                    DgvApprover.DataBind();
                }

            }
            #endregion


            if (blnIsShortclose == true)
            {
               
                gvuploadedFiles.Columns[1].Visible = false;
                btnTra_Details.Visible = false;
              
            }



            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["appr_StatusId"]).Trim() == "4" || Convert.ToString(dsMilestone.Tables[0].Rows[0]["appr_StatusId"]).Trim() == "3")
            {
              //  trvl_btnSave.Visible = false;
            }
            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["appr_StatusId"]).Trim() == "5")
            {
                gvuploadedFiles.Columns[1].Visible = true;

            }
            else
            {
                gvuploadedFiles.Columns[1].Visible = false;
            }

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["PaymentStatusID"]).Trim() == "2")
            {
              //  trvl_btnSave.Visible = false;
            }

            if (dsMilestone.Tables[3].Rows.Count > 0)
            {
              
                hdnShortClose_Cancelled.Value = "shortclose";
                liRemarks_1.Visible = true;
                liRemarks_2.Visible = true;
                liRemarks_3.Visible = true;

            }
            else
            {
              
                hdnShortClose_Cancelled.Value = "cancel";
                liRemarks_1.Visible = true;
                liRemarks_2.Visible = true;
                liRemarks_3.Visible = true;

            }

            hdnApprovedPO_FileName.Value = Convert.ToString(Regex.Replace(Convert.ToString(lstTripType.SelectedItem.Text), @"[^0-9a-zA-Z\._]", "_")).Trim() + ".pdf";
            hdnApprovedPO_FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBApprovedPOWOfiles"]).Trim());

        }
    }

    public string ReplaceInvalidChars(string filename)
    {
        Regex illegalInFileName = new Regex(@"[#%&{}\!/<'>@+*?$|=]");
        string myString = illegalInFileName.Replace(filename, "_");
        //return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        return myString;
    }

    public void getMilestoneUploadedFiles()
    {

        DataSet dsFiles = spm.getVSCB_UploadedFiles("get_VSCB_UploadedFiels", "Milestone", Convert.ToDouble(hdnPOWOID.Value), Convert.ToString(hdnSrno.Value).Trim(), "");

        gvuploadedFiles.DataSource = null;
        gvuploadedFiles.DataBind();
        if (dsFiles.Tables[0].Rows.Count > 0)
        {
            gvuploadedFiles.DataSource = dsFiles;
            gvuploadedFiles.DataBind();
        }
    }


    public void getApprovers_AsPerPOWOType()
    {
        #region get Approver as per amount

        //get Approver List
        DataSet dsApprover = null;
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();
        if (Convert.ToString(hdnPOWOID.Value).Trim() != "")
            dsApprover = spm.get_POWO_Approver_List(Convert.ToDouble(hdnPOWOID.Value), Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(txtProject.Text).Trim(), Convert.ToDecimal(txtBasePOWOWAmt.Text), Convert.ToInt32(lstPOType.SelectedValue));
        else
            dsApprover = spm.get_POWO_Approver_List(0, Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(txtProject.Text).Trim(), Convert.ToDecimal(txtBasePOWOWAmt.Text), Convert.ToInt32(lstPOType.SelectedValue));
       // trvl_btnSave.Visible = false;
        btnTra_Details.Visible = false;
        if (dsApprover != null)
        {

            if (dsApprover.Tables[0].Rows.Count > 0)
            {

                DgvApprover.DataSource = dsApprover.Tables[0];
                DgvApprover.DataBind();
              //  trvl_btnSave.Visible = true;
                btnTra_Details.Visible = true;
            }
            if (dsApprover.Tables[1].Rows.Count > 0)
            {
                hdnCondintionId.Value = Convert.ToString(dsApprover.Tables[1].Rows[0]["cond_id"]).Trim();
                hdnRangeId.Value = Convert.ToString(dsApprover.Tables[1].Rows[0]["Range_type_id"]).Trim();
            }
        }
        #endregion
    }

    #endregion

    protected void lnkDeleteexpFile_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            Int32 ifileid = Convert.ToInt32(gvuploadedFiles.DataKeys[row.RowIndex].Values[0]);
            LinkButton lnkviewfile = (LinkButton)row.FindControl("lnkviewfile");
            hdnfileid.Value = "";
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBMilestonefiles"]).Trim()), lnkviewfile.Text);

            if (System.IO.File.Exists(strfilepath))
            {
                System.IO.File.Delete(strfilepath);
                hdnSrno.Value = Convert.ToString(ifileid);

                spm.InsertInvoiceUploaded_Files(Convert.ToDouble(hdnMstoneId.Value), "deletefile_srno", "", "Milestone", ifileid);
                getMilestoneUploadedFiles();
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    protected void gvuploadedFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void lnkfile_PO_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBMilestonefiles"]).Trim()), lnkfile_PO.Text);
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



    protected void dgTravelRequest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // e.Row.Attributes.Add("style", "cursor:help;");
        //if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Alternate)
        //{

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Boolean isShortClose = Convert.ToBoolean(dgTravelRequest.DataKeys[e.Row.RowIndex].Values[2]);
            // e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='orange'");
            //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E56E94'");
            if (isShortClose == true)
            {
                e.Row.BackColor = Color.FromName("#E56E94");
                //e.Row.ForeColor = Color.White;
                LinkButton lnkTravelDetailsEdit = e.Row.FindControl("lnkTravelDetailsEdit") as LinkButton;
                lnkTravelDetailsEdit.Visible = false;
            }
        }
        //}
        //else
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='orange'");
        //        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='gray'");
        //        e.Row.BackColor = Color.FromName("gray");
        //    }
        //}


    }



    protected void lstPOType_ForApproval_SelectedIndexChanged(object sender, EventArgs e)
    {
        getApprovers_AsPerPOWOType();
    }

    protected void lnkViewPOWO_SignCopy_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBSignPOWOfiles"]).Trim()), lnkViewPOWO_SignCopy.Text);
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

    protected void btnCorrection_Click(object sender, EventArgs e)
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
            Response.AppendHeader("Content-Disposition", "attachment; filename=POWO_" + PowoNumber + "." + extension);
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

    protected void btnback_mng_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/VSCB_ApprovedPOWOList.aspx");

    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        DataSet dsPOWOContent = new DataSet();

        #region get POWO Content 

        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_ViewDraftCopy_FromTally";
        spars[1] = new SqlParameter("@POWO_Number", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(lstTripType.SelectedItem.Text).Trim();  //"PO/042021/00001"; 
        spars[2] = new SqlParameter("@POWOID", SqlDbType.Decimal);
        spars[2].Value = Convert.ToDouble(hdnPOWOID.Value);  //"PO/042021/00001";

        dsPOWOContent = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");

        #endregion

        try
        {
            string strpath = Server.MapPath("~/procs/VSCB_Rpt_ViewDraftCopy.rdlc");
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
            //Response.AppendHeader("Content-Disposition", "attachment; filename=DraftCopy." + extension);
            Response.AppendHeader("Content-Disposition", "attachment; filename=DraftCopy_" + PowoNumber + "." + extension);
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

    protected void btnReject_Click(object sender, EventArgs e)
    {
        DataSet dsPOWOContent = new DataSet();

        #region get POWO Content 

        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_ViewDraftCopy_FromTallyWithoutSubmitData";
        spars[1] = new SqlParameter("@POWO_Number", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(lstTripType.SelectedItem.Text).Trim();  //"PO/042021/00001"; 
        spars[2] = new SqlParameter("@POWOID", SqlDbType.Decimal);
        spars[2].Value = Convert.ToDouble(lstTripType.SelectedValue);  //"PO/042021/00001";

        dsPOWOContent = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");

        #endregion

        try
        {
            string strpath = Server.MapPath("~/procs/VSCB_Rpt_ViewDraftCopy.rdlc");
            string PowoNumber = Convert.ToString(lstTripType.SelectedItem.Text).Trim().Replace("/", "-");

            LocalReport ReportViewer2 = new LocalReport();
            ReportViewer2.ReportPath = strpath;// Server.MapPath(strpath);

            // txt_POWOContent_description

            dsPOWOContent.Tables[0].Rows[0]["powocontent"] = dsPOWOContent.Tables[0].Rows[0]["PCont"] + "  " + txt_POWOContent_description.Text;
            dsPOWOContent.Tables[0].AcceptChanges();

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
            //Response.AppendHeader("Content-Disposition", "attachment; filename=DraftCopy." + extension);
            Response.AppendHeader("Content-Disposition", "attachment; filename=DraftCopy_" + PowoNumber + "." + extension);
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

}