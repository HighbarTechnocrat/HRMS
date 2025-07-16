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

public partial class VSCB_ApprovedPOWOMilestone : System.Web.UI.Page
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
                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBMilestonefiles"]).Trim() + "/");
                    hdnSingPOCopyFilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBSignPOWOfiles"]).Trim());
                    txtQty.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtRate.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");
                    txtCGSTPer.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");
                    txtSGSTPer.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");
                    txtIGSTPer.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");
                    txtMilestone.Attributes.Add("maxlength", txtMilestone.MaxLength.ToString());


                    GetPOTypes();
                    getLoginEmployeeDetails();

                    if (Request.QueryString.Count > 0)
                    {
                        hdnPOWOID.Value = Request.QueryString[0];

                        check_POWO_IsForLoginEployeee();
                        get_PWODetails_MilestonesList_Update();


                        spnPOWOPaidAmt.Visible = true;
                        spnPOWOBalAmt.Visible = true;
                        txtPOWOPaidAmt.Visible = false;
                        spnPOWOSettelmentAmt.Visible = false;
                        txtPOWOSettelmentAmt.Visible = false;

                        txtPoPaidAmt_WithOutDT.Visible = true;
                        txtPoPaidAmt_WithOutDT.Visible = true;
                        txtPOWOBalanceAmt.Visible = true;
                        txtDirectTaxAmt.Visible = true;
                        spnPOWODirectTaxAmt.Visible = true;

                        spnPOWOSettelmentAmt.Visible = true;
                        txtPOWOSettelmentAmt.Visible = true;

                        if (Convert.ToString(txtPOWOShortClosedAmt.Text).Trim() != "" && Convert.ToString(txtPOWOShortClosedAmt.Text).Trim() != "0.00")
                        {
                            txtPOWOShortClosedAmt.Visible = true;
                            spnPOWODShortClosedAmt.Visible = true;
                        }


                        hdnApprovedPO_FileName.Value = Convert.ToString(Regex.Replace(Convert.ToString(txtTriptype.Text), @"[^0-9a-zA-Z\._]", "_")).Trim() + ".pdf";
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
    protected void trvldeatils_cancel_btn_Click(object sender, EventArgs e)
    {
        DivTrvl.Visible = false;
        lblMilestoneMsg.Text = "";
        lblmessage.Text = "";

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
        dsMilestone = spm.get_Approved_Milestone_View(Convert.ToDouble(hdnPOWOID.Value), Convert.ToInt32(hdnSrno.Value));

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

            txtMilestonePaidAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MilestonePaidAmt"]).Trim();
            txtMilestoneBalanceAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Milesstone_Balance_Amt"]).Trim();
            txtMilestoneDirectTaxAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Collect_TDS_Amt"]).Trim();

            txtCGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CGST_Amt"]).Trim();
            txtSGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["SGST_Amt"]).Trim();
            txtIGST_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["IGST_Amt"]).Trim();


            //trvldeatils_delete_btn.Visible = true; 
            txtMilestone.Enabled = false;
            txtCGSTPer.Enabled = false;
            txtSGSTPer.Enabled = false;
            txtIGSTPer.Enabled = false;


            txtAmount.Enabled = false;
            txtRate.Enabled = false;
            txtQty.Enabled = false;
            txtAmtWithTax.Enabled = false;
            txtAmtWithTax.ReadOnly = false;
            txtAmount.Enabled = false;
            txtAmount.ReadOnly = false;

            hdnMstoneId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["MstoneID"]).Trim();

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

            if (Convert.ToString(row.Cells[5].Text).Trim() != "")
            {
                if (Convert.ToString(hdnSrno.Value).Trim() != Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[0]).Trim())
                    dTotalMilestoneAmount += Convert.ToDecimal(row.Cells[5].Text);
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

        //    trvldeatils_btnSave.Text = "Submit";

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

    public void check_POWO_IsForLoginEployeee()
    {
        DataSet dsEmployee = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Check_POWO_forLoginEmployee";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode.Text);

        spars[2] = new SqlParameter("@POWOID", SqlDbType.BigInt);
        spars[2].Value = Convert.ToDouble(hdnPOWOID.Value);

        dsEmployee = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        if (dsEmployee.Tables[0].Rows.Count == 0)
        {
            Response.Redirect("~/procs/vscb_index.aspx");
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



    public void get_PWODetails_MilestonesList_Update()
    {

        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.get_Approved_POWOMilestone_Details(txtEmpCode.Text, Convert.ToDouble(hdnPOWOID.Value));

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
            txtTriptype.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PONumber"]).Trim();
            txtPOWOPaidAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Paid_Amt"]).Trim();
            txtPoPaidAmt_WithOutDT.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POPiadAmount_withoutDT"]).Trim();
            txtPOWOBalanceAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Bal_Amt"]).Trim();
            txtDirectTaxAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["DirectTaxCollection_Amt"]).Trim();
            txtCostCenter.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["costCenter"]).Trim();
            txtCurrency.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CurName"]).Trim();
            txtPOWOSettelmentAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_settlementAmt"]).Trim();

            txtPOWOShortClosedAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["ShortClose_Amt"]).Trim();

            lblPOWO_Content.InnerHtml = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_WO_Text"]).Trim();
            hdnCondintionId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["cond_id"]).Trim();
            hdnRangeId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Range_type_id"]).Trim();
            hdnAppr_StatusId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["appr_StatusId"]).Trim();

            hdnCompCode.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Comp_Code"]).Trim();
            hdnVendorId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["VendorID"]).Trim();
            hdnPrj_Dept_Id.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Prj_Dept_Id"]).Trim();
            lnkfile_PO.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_File"]).Trim();

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["IsShortClose"]).Trim() != "")
                blnIsShortclose = Convert.ToBoolean(dsMilestone.Tables[0].Rows[0]["IsShortClose"]);


            txtPOWOTitle.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["HPOTypeName"]).Trim();
            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Security_Deposit_Amt"]).Trim() != "")
            {
                txtSecurity_DepositAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Security_Deposit_Amt"]).Trim();
                if (Convert.ToDecimal(dsMilestone.Tables[0].Rows[0]["PO_Security_Deposit_Amt"]) > 0)
                {
                    divScurity_Diposit.Visible = true;
                }
            }

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_WO_Text"]).Trim() != "")
            {
                liPOWOContent_1.Visible = true;
                liPOWOContent_2.Visible = true;
                liPOWOContent_3.Visible = true;
            }

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["appr_StatusId"]).Trim() == "2")
            {
                if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["powo_signCopy"]).Trim() != "")
                {
                    lnkViewPOWO_SignCopy.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["powo_signCopy"]).Trim();
                    hdnSingPOCopyFileName.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["powo_signCopy"]).Trim();
                    liViewPOWO_SignCopy_1.Visible = true;
                    liViewPOWO_SignCopy_2.Visible = true;
                    liViewPOWO_SignCopy_3.Visible = true;
                    lnkViewPOWO_SignCopy.Visible = true;
                    spnViewSingCopy.Visible = true;
                }
                btnCorrection.Visible = true;
                btnApprove.Visible = false;
            }
            else
            {
                btnCorrection.Visible = false;
                btnApprove.Visible = true;
            }


            lstPOType.Enabled = false;
            lnkfile_PO.Visible = true;

            spnPOWOStatus.Visible = true;
            txtPOStatus.Visible = true;
            spnAmountWithTax.Visible = true;
            txtPOWOAmt.Visible = true;


            dgTravelRequest.DataSource = null;
            dgTravelRequest.DataBind();


            if (dsMilestone.Tables[1].Rows.Count > 0)
            {
                dgTravelRequest.DataSource = dsMilestone.Tables[1];
                dgTravelRequest.DataBind();
            }


            getMilestoneUploadedFiles();

            if (dsMilestone.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["appr_StatusId"]).Trim() == "1")
                {
                    #region get Approver as per amount

                    //get Approver List
                    DataSet dsApprover = null;
                    DgvApprover.DataSource = null;
                    DgvApprover.DataBind();

                    dsApprover = spm.get_POWO_Approver_List(Convert.ToDouble(hdnPOWOID.Value), Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(txtProject.Text).Trim(), Convert.ToDecimal(txtBasePOWOWAmt.Text), Convert.ToInt32(lstPOType.SelectedValue));

                    if (dsApprover != null)
                    {

                        if (dsApprover.Tables[0].Rows.Count > 0)
                        {
                            DgvApprover.DataSource = dsApprover.Tables[0];
                            DgvApprover.DataBind();
                        }

                    }
                    #endregion
                }
                else
                {
                    DataSet dsApprover = new DataSet();

                    SqlParameter[] spars = new SqlParameter[2];

                    spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                    spars[0].Value = "get_POWO_Approvers";

                    spars[1] = new SqlParameter("@POWOID", SqlDbType.BigInt);
                    spars[1].Value = Convert.ToDouble(hdnPOWOID.Value);

                    dsApprover = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
                    if (dsApprover != null)
                    {

                        if (dsApprover.Tables[0].Rows.Count > 0)
                        {
                            DgvApprover.DataSource = dsApprover.Tables[0];
                            DgvApprover.DataBind();
                        }

                    }
                }
            }


            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["appr_StatusId"]).Trim() == "2")
            {
                string spo_number = Regex.Replace(Convert.ToString(txtTriptype.Text), @"[^0-9a-zA-Z\._]", "_");

                if (!File.Exists(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBApprovedPOWOfiles"]).Trim() + spo_number + ".pdf")))
                    clsDownloadPOWO.POWODownload_Word_PDFNew(Convert.ToString(hdnPOWOID.Value), Convert.ToString(txtTriptype.Text));
            }
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
            spnSupportingFiles.Visible = true;
        }
    }


    #endregion


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

        //string sponumber = Regex.Replace(Convert.ToString(txtTriptype.Text), @"[^0-9a-zA-Z\._]", "_");
        //String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBApprovedPOWOfiles"]).Trim()), sponumber + ".pdf");

        //try
        //{

        //    if (File.Exists(strfilepath))
        //    {
        //        Response.ContentType = ContentType;
        //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
        //        Response.WriteFile(strfilepath);
        //        Response.End();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Response.Write(ex.Message.ToString());
        //}

        #region PO download RDLC Code not required

        DataSet dsPOWOContent = new DataSet();

        #region get POWO Content 


        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_POWO_Content_FromTally";

        spars[1] = new SqlParameter("@POWO_Number", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtTriptype.Text).Trim();  //"PO/042021/00001"; 

        spars[2] = new SqlParameter("@POWOID", SqlDbType.Decimal);
        spars[2].Value = Convert.ToDouble(hdnPOWOID.Value);  //"PO/042021/00001";


        dsPOWOContent = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");

        #endregion

        try
        {



            string strpath = Server.MapPath("~/procs/VSCB_Rpt_POWO_Content_New.rdlc");
            string PowoNumber = Convert.ToString(txtTriptype.Text).Trim().Replace("/", "-");

            LocalReport ReportViewer2 = new LocalReport();
            ReportViewer2.ReportPath = strpath;// Server.MapPath(strpath);
            ReportViewer2.DataSources.Clear();
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
            ReportViewer2.DataSources.Add(rds_5);
            ReportViewer2.DataSources.Add(rds);
            ReportViewer2.DataSources.Add(rds_2);
            ReportViewer2.DataSources.Add(rds_3);
            ReportViewer2.DataSources.Add(rds_4);


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
            // Response.AppendHeader("Content-Disposition", "attachment; filename=POWOContent." + extension);
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

        #endregion

    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        DataSet dsPOWOContent = new DataSet();

        #region get POWO Content 

        SqlParameter[] spars = new SqlParameter[5];
        string HFChkFlagDraftCopy = "0";
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Insert_POWO_TempDraftCopy";

        spars[1] = new SqlParameter("@POWOID", SqlDbType.Decimal);
        spars[1].Value = Convert.ToDouble(hdnPOWOID.Value);


        HFChkFlagDraftCopy = "1";
        spars[2] = new SqlParameter("@CurrencyName", SqlDbType.VarChar);
        spars[2].Value = "";
        spars[3] = new SqlParameter("@POHTMLContain", SqlDbType.VarChar);
        spars[3].Value = "";

        spars[4] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[4].Value = txtEmpCode.Text;

        dsPOWOContent = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");

        string url = "VSCB_DraftCopy.aspx?ID=" + HFChkFlagDraftCopy + "";
        string s = "window.open('" + url + "', 'popup_window', 'width=800,height=790,scrollbars=no, menubar=no,resizable=no,scrollbars=yes,toolbar=no,directories=no,location=no');";
        //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        ///window.open(url, 'popUpWindow', 'height=' + height + ', width=' + width + ', resizable=yes, scrollbars=yes, toolbar=yes')
        //ClientScript.RegisterStartupScript(this.GetType(), "popupWindow(url,'','window',200,100)","",true); 
        ClientScript.RegisterStartupScript(this.GetType(), "popupwindow(" + url + ")", s, true);

        #endregion

        #region Draft Copy code not required


        #region get POWO Content 

        //SqlParameter[] spars = new SqlParameter[3];

        //spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        //spars[0].Value = "get_ViewDraftCopy_FromTally";
        //spars[1] = new SqlParameter("@POWO_Number", SqlDbType.VarChar);
        //spars[1].Value = Convert.ToString(txtTriptype.Text).Trim();  //"PO/042021/00001"; 
        //spars[2] = new SqlParameter("@POWOID", SqlDbType.Decimal);
        //spars[2].Value = Convert.ToDouble(hdnPOWOID.Value);  //"PO/042021/00001";

        //dsPOWOContent = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");

        #endregion

        //try
        //{
        //    string strpath = Server.MapPath("~/procs/VSCB_Rpt_ViewDraftCopy.rdlc");
        //    string PowoNumber = Convert.ToString(txtTriptype.Text).Trim().Replace("/", "-");

        //    LocalReport ReportViewer2 = new LocalReport();
        //    ReportViewer2.ReportPath = strpath;// Server.MapPath(strpath);
        //    ReportDataSource rds = new ReportDataSource("dspowoContent", dsPOWOContent.Tables[0]);
        //    ReportDataSource rds_2 = new ReportDataSource("dsowoDetails", dsPOWOContent.Tables[1]);
        //    ReportDataSource rds_3 = new ReportDataSource("dsMilestone", dsPOWOContent.Tables[2]);
        //    ReportDataSource rds_4 = new ReportDataSource("dsPOWOAmountinWords", dsPOWOContent.Tables[3]);

        //    ReportDataSource rds_5 = new ReportDataSource("dsPOPreparedBy", dsPOWOContent.Tables[4]);
        //    ReportDataSource rds_6 = new ReportDataSource("dsPOCheckedBy", dsPOWOContent.Tables[5]);
        //    ReportDataSource rds_7 = new ReportDataSource("dsPOApprovedBY_HOD", dsPOWOContent.Tables[6]);
        //    ReportDataSource rds_8 = new ReportDataSource("dsPOApprovedBY_COO", dsPOWOContent.Tables[7]);
        //    ReportDataSource rds_9 = new ReportDataSource("dsPOApprovedBY_CEO", dsPOWOContent.Tables[8]);

        //    //if (Convert.ToString(dsPOWOContent.Tables[1].Rows[0]["Approver_Count"]).Trim() == "2")
        //    //{
        //    // ReportDataSource rds_6 = new ReportDataSource("dsPOCheckedBy", dsPOWOContent.Tables[5]);
        //    //  ReportDataSource rds_7 = new ReportDataSource("dsPOApprovedBY_HOD_COO", dsPOWOContent.Tables[6]);

        //    //  ReportViewer2.DataSources.Add(rds_7);
        //    //}


        //    ReportViewer2.DataSources.Clear();
        //    ReportViewer2.DataSources.Add(rds);
        //    ReportViewer2.DataSources.Add(rds_2);
        //    ReportViewer2.DataSources.Add(rds_3);
        //    ReportViewer2.DataSources.Add(rds_4);

        //    ReportViewer2.DataSources.Add(rds_5);
        //    ReportViewer2.DataSources.Add(rds_6);
        //    ReportViewer2.DataSources.Add(rds_7);
        //    ReportViewer2.DataSources.Add(rds_8);
        //    ReportViewer2.DataSources.Add(rds_9);
        //    ReportViewer2.Refresh();

        //    Warning[] warnings;
        //    string[] streamIds;
        //    string contentType;
        //    string encoding;
        //    string extension;

        //    //Export the RDLC Report to Byte Array.
        //    byte[] bytes = ReportViewer2.Render("pdf", null, out contentType, out encoding, out extension, out streamIds, out warnings);

        //    //Download the RDLC Report in Word, Excel, PDF and Image formats.
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.Charset = "";
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.ContentType = contentType;
        //    // Response.AppendHeader("Content-Disposition", "attachment; filename=DraftCopy." + extension);
        //    Response.AppendHeader("Content-Disposition", "attachment; filename=DraftCopy_" + PowoNumber + "." + extension);
        //    Response.BinaryWrite(bytes);
        //    Response.Flush();
        //    Response.End();

        //}
        //catch (Exception ex)
        //{
        //    Response.Write(ex.Message.ToString());
        //    Response.End();
        //}

        #endregion
    }
}