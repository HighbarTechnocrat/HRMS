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

public partial class VSCB_CreateMilestone : System.Web.UI.Page
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


                    createPOWO_PDF();

                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBMilestonefiles"]).Trim() + "/");
                    hdnSingPOCopyFilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBSignPOWOfiles"]).Trim());

                    txtQty.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtRate.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");
                    txtSecurity_DepositAmt.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");
                    txtCGSTPer.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");
                    txtSGSTPer.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");
                    txtIGSTPer.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");
                    txtMilestone.Attributes.Add("maxlength", txtMilestone.MaxLength.ToString());
                    txt_remakrs.Attributes.Add("maxlength", txt_remakrs.MaxLength.ToString());
                    txtMilestoneDueDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");

                    btnTra_Details.Visible = false;
                    GetPODetails_FromTally();
                    //GetHPODetails();
                    //GetPOTypes();                    
                    //Get_CurrecnyList();
                    //getLoginEmployeeDetails();

                    if (Request.QueryString.Count > 0)
                    {
                        hdnPOWOID.Value = Request.QueryString[0];
                        hdnstype_Main.Value = "Update";

                        GetPODetails_List();
                        get_PWODetails_MilestonesList_Update();


                        spnPOWOPaidAmt.Visible = true;
                        liPOWOPaidAmt.Visible = true;
                        spnPOWOBalAmt.Visible = true;
                        
                        liPOWOBalAmt.Visible = true;

                        spnMilestoneSettelmentAmt.Visible = true;
                        liMilestoneSettelmentAmt.Visible = true;
                        txtPO_Settlment_Amt.Visible = true;
                        txtPoPaidAmt_WithOutDT.Visible = true;
                        txtPOWOBalanceAmt.Visible = true;
                        txtDirectTaxAmt.Visible = true;
                        spnPOWODirectTaxAmt.Visible = true;
                        liPOWODirectTaxAmt.Visible = true;

                        liSettlement_1.Visible = true;
                        liSettlement_2.Visible = true;
                        liSettlement_3.Visible = true;

                        liPOWODShortClosedAmt.Visible = true;
                        hdnApprovedPO_FileName.Value = Convert.ToString(Regex.Replace(Convert.ToString(lstTripType.SelectedItem.Text), @"[^0-9a-zA-Z\._]", "_")).Trim() + ".pdf";
                        hdnApprovedPO_FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBApprovedPOWOfiles"]).Trim());


                        if (Convert.ToString(txtPOWOShortClosedAmt.Text).Trim() != "" && Convert.ToString(txtPOWOShortClosedAmt.Text).Trim() != "0.00")
                        {

                            txtPOWOShortClosedAmt.Visible = true;
                            spnPOWODShortClosedAmt.Visible = true;
                            liPOWODShortClosedAmt.Visible = true;
                        }
                    }

                }

            }

        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

        //liAmountWithTax.Visible = true;
        //liPOWODirectTaxAmt.Visible = true;
        //liPOWOPaidAmt.Visible = true;
        //liMilestoneSettelmentAmt.Visible = true;
        //liPOWOBalAmt.Visible = true;
        //liPOWODShortClosedAmt.Visible = true;
        //liSettlement_1.Visible = true;
        //liSettlement_2.Visible = true;
        //liSettlement_3.Visible = true;
        //liPOWOStatus.Visible = true;
    }

    private void createPOWO_PDF()
    {
        #region to create All POWO PDF
         if (Convert.ToString(txtEmpCode.Text).Trim() == "00631295")
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString);
            DataSet ds = new DataSet();
            SqlCommand sqlComm = new SqlCommand("SP_VSCB_Reports_Details", cn);
            sqlComm.Parameters.AddWithValue("@stype", "get_POWO_ID");

            sqlComm.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = sqlComm;
            da.Fill(ds);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                    if (Convert.ToString(ds.Tables[0].Rows[i]["powo_signCopy"]).Trim() != "")
                    {
                        if (File.Exists(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBSignPOWOfiles"]).Trim() + Convert.ToString(ds.Tables[0].Rows[i]["powo_signCopy"]))))
                        {
                            string spo_number = Regex.Replace(Convert.ToString(ds.Tables[0].Rows[i]["PONumber"]), @"[^0-9a-zA-Z\._]", "_");
                            File.Copy(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBSignPOWOfiles"]).Trim() + Convert.ToString(ds.Tables[0].Rows[i]["powo_signCopy"])), Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBApprovedPOWOfiles"]).Trim()) + spo_number + ".pdf", true);
                        }
                        else
                        {
                            clsDownloadPOWO.POWODownload_Word_PDFNew(Convert.ToString(ds.Tables[0].Rows[i]["poid"]).Trim(), Convert.ToString(ds.Tables[0].Rows[i]["PONumber"]));

                        }
                    }
                    else
                    {
                        clsDownloadPOWO.POWODownload_Word_PDFNew(Convert.ToString(ds.Tables[0].Rows[i]["poid"]).Trim(), Convert.ToString(ds.Tables[0].Rows[i]["PONumber"]));

                    }

                }
                }

            }
        }  
        #endregion
    }

    protected void lstTripType_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Convert.ToString(lstTripType.SelectedValue).Trim() =="" || Convert.ToString(lstTripType.SelectedValue).Trim() == "0")
        {
            return;
        }

        

        DataTable dtPOWODetails = new DataTable();
        clear_POWO_Cntrls();
        //delete Temp Milestone

        DgvApprover.DataSource = null;
        DgvApprover.DataBind();
        btnApprove.Visible = false;
        dtPOWODetails = spm.getTallyPODetails(Convert.ToDouble(lstTripType.SelectedValue), Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(lstCurrency.SelectedItem.Text).Trim());
        if (dtPOWODetails.Rows.Count > 0)
        {
            btnApprove.Visible = true;
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
		string PoWONo = "";
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnSrno.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnpamentStatusid.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[1]).Trim();
		hdnMstoneId.Value = Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[3]).Trim();

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
		PoWONo =Convert.ToString(lstTripType.SelectedItem.Text).Trim();
		dsMilestone = spm.getMilestoneFrom_Temptable_edit_New(txtEmpCode.Text, Convert.ToInt32(hdnMstoneId.Value), PoWONo);

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
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }


        #region validations

        if (DgvApprover.Rows.Count <= 0)
        {
            lblmessage.Text = "Since you are not assigned under any approver you can not create PO/ WO, please contact HR";
            return;
        } 

        if (Convert.ToString(hdnCondintionId.Value).Trim() == "")
        {
            lblmessage.Text = "PO/ WO Condition not created. Please contact to Admin";
            return;
        }


        if (Convert.ToString(lstPOType.SelectedValue).Trim() == "0")
        {
            lblmessage.Text = "Please Select PO/ WO Type.";
            return;
        }


        if (Convert.ToString(lstTripType.SelectedValue).Trim() == "0")
        {
            lblmessage.Text = "Please Select PO/ WO Number.";
            return;
        }

        if (Convert.ToString(lstPOType.SelectedValue).Trim() == "0")
        {
            lblmessage.Text = "Please Select PO/ WO Type.";
            return;
        }
        if (dgTravelRequest.Rows.Count <= 0)
        {
            lblmessage.Text = "Please Add Milestone.";
            return;
        }

        if (Convert.ToString(ddlHPOTYPE.SelectedValue).Trim() == "0")
        {
            lblmessage.Text = "Please Select PO/ WO Title.";
            return;
        }

        if (Convert.ToString(hdnPOWOID.Value).Trim() == "")
        {
            if (Convert.ToString(txt_POWOContent_description.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter PO/ WO Content.";
                return;
            }
        }



        if (Convert.ToString(lstPOType.SelectedValue).Trim() == "0")
        {
            lblmessage.Text = "Please Select Currency Name.";
            return;
        }
        
        //if (Convert.ToString(txtShippingAddress.Text).Trim() == "")
        //{
        //    lblmessage.Text = "Please enter Shipping Address.";
        //    return;
        //}
        if (Convert.ToString(txtShippingAddress.Text).Trim().Length>500)
        {
            lblmessage.Text = "Shipping Address Allow Maximum 500 Character only.";
            return;
        }

        if ( Convert.ToString(txtPORemarks.Text).Trim().Length > 500)
        {
            lblmessage.Text = "PO Remarks Allow Maximum 500 Character only.";
            return;
        }

        #region comment by Sanjay Upload POWO file not required
        /* if (Convert.ToString(hdnPOWOID.Value).Trim() == "")
         {
             if (!POUploadfile.HasFile)
             {
                 lblmessage.Text = "Please Upload PO File.";
                 return;
             }
         }*/
        #endregion

        #region Check Milestone Amount with POWO Amount

       
        decimal dTotalMilestoneAmount = 0;
        Decimal dPOWOAmount = 0;

        dPOWOAmount = Math.Round(Convert.ToDecimal(txtBasePOWOWAmt.Text), 2);
        foreach (GridViewRow row in dgTravelRequest.Rows)
        {
            if (Convert.ToString(row.Cells[7].Text).Trim() != "")
            {
                dTotalMilestoneAmount += Convert.ToDecimal(row.Cells[7].Text);
            }
        }

        if (dTotalMilestoneAmount != dPOWOAmount)
        {
            lblmessage.Text = "Overall milestone amount is required upto PO/ WO amount. Please correct and try again!";
            return;
        }

        if (dTotalMilestoneAmount > dPOWOAmount)
        {
            lblmessage.Text = "Overall Milestone amount Exceeding PO/ WO Amount. Please correct and try again!";
            return;
        }

        #endregion

        #endregion

        string[] strdate;
        string strfromDate = "";

        #region date formatting
        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('-');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        #endregion

          
        string strStype = "insert_main";

        double dPOWOID = 0;
        Decimal dPOWOAmt_WithTax = 0;
        if (Convert.ToString(hdnPOWOID.Value).Trim() != "")
        {
			//strStype = "update_Milestone"; Temp table Remove
			strStype = "update_Milestone_New";
			dPOWOID = Convert.ToDouble(hdnPOWOID.Value);
        }
        foreach (GridViewRow row in dgTravelRequest.Rows)
        {
            if (Convert.ToString(row.Cells[10].Text).Trim() != "")
            {
                // if (Convert.ToString(hdnSrno.Value).Trim() != Convert.ToString(dgTravelRequest.DataKeys[row.RowIndex].Values[0]).Trim())
                dPOWOAmt_WithTax += Convert.ToDecimal(row.Cells[11].Text);
            }
        }

        double dMaxReqId = 0;
        double dProject_Dept_Id = 0;
        if (Convert.ToString(hdnPrj_Dept_Id.Value).Trim() != "")
            dProject_Dept_Id = Convert.ToDouble(hdnPrj_Dept_Id.Value);
        DataSet dtApproverEmailIds;

        if (Convert.ToString(hdnPOWOID.Value).Trim() != "")
            dtApproverEmailIds = spm.get_POWO_Approver_List(Convert.ToDouble(hdnPOWOID.Value), Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(txtProject.Text).Trim(), Convert.ToDecimal(txtBasePOWOWAmt.Text), Convert.ToInt32(lstPOType.SelectedValue));
        else
            dtApproverEmailIds = spm.get_POWO_Approver_List(0, Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(txtProject.Text).Trim(), Convert.ToDecimal(txtBasePOWOWAmt.Text), Convert.ToInt32(lstPOType.SelectedValue));

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


            Boolean isshortClose = false;

            if (Convert.ToString(hdnIsShorClose.Value).Trim() == "1")
            {
                isshortClose = true;
            }
            DataSet dtMaxreqID = new DataSet();
            decimal dSecurity_Deposit_Amt = 0;
            if(Convert.ToString(txtSecurity_DepositAmt.Text).Trim()!="")
                dSecurity_Deposit_Amt= Convert.ToDecimal(txtSecurity_DepositAmt.Text);

            dtMaxreqID = spm.InsertUpdateMilestoneMain(txtEmpCode.Text, strStype, Convert.ToString(lstTripType.SelectedItem.Text).Trim(), strfromDate, Convert.ToString(txtPOTitle.Text), Convert.ToInt32(hdnVendorId.Value), Convert.ToInt32(lstPOType.SelectedValue), dPOWOAmount, hdnCompCode.Value, dPOWOID, dProject_Dept_Id, dPOWOAmt_WithTax, 1, Convert.ToInt32(hdnCondintionId.Value), Convert.ToInt32(hdnRangeId.Value), isshortClose, Convert.ToString(txt_POWOContent_description.Text), Convert.ToInt32(lstCurrency.SelectedValue), Convert.ToInt32(ddlHPOTYPE.SelectedValue),Convert.ToString(txtShippingAddress.Text).Trim(), Convert.ToString(txtPORemarks.Text).Trim(), dSecurity_Deposit_Amt);
            dMaxReqId = Convert.ToDouble(dtMaxreqID.Tables[0].Rows[0]["MaxReqID"]);

             
            if (isshortClose == false)
            {
                spm.Insert_UpdatePOWO_ApproverDetails("InsertPOWO_Approver", dMaxReqId, sApproverEmpCode, iAppr_id, "Pending", "", "", "");

                #region insert or upload multiple files
                if (POUploadfile.HasFile)
                {
                    string filename = POUploadfile.FileName;
                    string MilestoneFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBMilestonefiles"]).Trim() + "/");
                    bool folderExists = Directory.Exists(MilestoneFilePath);
                    if (!folderExists)
                    {
                        Directory.CreateDirectory(MilestoneFilePath);
                    }
                    String InputFile = System.IO.Path.GetExtension(POUploadfile.FileName);
                    filename = txtEmpCode.Text + "_" + Convert.ToString(dMaxReqId).Trim() + "_Milestone_PO" + InputFile;
                    POUploadfile.SaveAs(Path.Combine(MilestoneFilePath, filename));
                    spm.InsertInvoiceUploaded_Files(dMaxReqId, "INSERT_MilestonePOFile", Convert.ToString(filename).Trim(), "INSERT_MilestonePOFile", 0);
                }
                if (ploadexpfile.HasFile)
                {
                    string filename = ploadexpfile.FileName;

                    if (Convert.ToString(filename).Trim() != "")
                    {

                        string MilestoneFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBMilestonefiles"]).Trim() + "/");
                        bool folderExists = Directory.Exists(MilestoneFilePath);
                        if (!folderExists)
                        {
                            Directory.CreateDirectory(MilestoneFilePath);
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
                                strfileName = txtEmpCode.Text + "_" + Convert.ToString(dMaxReqId).Trim() + "_Milestone" + (t_cnt + 1).ToString() + InputFile;
                                filename = strfileName;
                                uploadfileName.SaveAs(Path.Combine(MilestoneFilePath, strfileName));


                                spm.InsertInvoiceUploaded_Files(dMaxReqId, "INSERT", Convert.ToString(strfileName).Trim(), "Milestone", i + 1);

                                t_cnt = t_cnt + 1;
                            }
                        }

                    }


                }
                #endregion

                #region Send Email to Next Approver 

                string strSubject = "OneHR: Request for - PO / WO Approval - " + Convert.ToString(lstTripType.SelectedItem.Text).Trim();

                string strPOWOURL = "";
                strPOWOURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["approverLinkPOWO_VSCB"]).Trim() + "?poid=" + dMaxReqId).Trim();


                DataSet dsformatAmt = spm.getFormated_Amount("get_formated_currncy_Amount", Convert.ToString(lstCurrency.SelectedItem.Text), Convert.ToDecimal(txtBasePOWOWAmt.Text), Convert.ToDecimal(dPOWOAmt_WithTax), 0, 0, 0);


                #region get approvers  approver list 
                DataSet dsApproverEmailIds = spm.get_POWO_Approver_List(dMaxReqId, Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(txtProject.Text).Trim(), Convert.ToDecimal(txtBasePOWOWAmt.Text), Convert.ToInt32(lstPOType.SelectedValue));
                StringBuilder strbuild_Approvers = new StringBuilder();
                strbuild_Approvers.Append("<table cellpadding='5' cellspacing='0' style='font-size:11pt;font-family:Arial;font-style:Regular;width:100%;'>");
                strbuild_Approvers.Append("<tr><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Approver Name</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Status</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Approved On</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Approver Remarks</th></tr>");
                for (Int32 irow = 0; irow < dsApproverEmailIds.Tables[0].Rows.Count; irow++)
                {
                    strbuild_Approvers.Append("<tr><td style='width:40%;border: 1px solid #ccc'>" + Convert.ToString(dsApproverEmailIds.Tables[0].Rows[irow]["ApproverName"]).Trim() + " </td>");
                    strbuild_Approvers.Append("<td style='width:10%;border: 1px solid #ccc'>" + Convert.ToString(dsApproverEmailIds.Tables[0].Rows[irow]["Status"]).Trim() + "</td>");
                    strbuild_Approvers.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(dsApproverEmailIds.Tables[0].Rows[irow]["approved_on"]).Trim() + "</td>");
                    strbuild_Approvers.Append("<td style='width:35%;border: 1px solid #ccc'>" + Convert.ToString(dsApproverEmailIds.Tables[0].Rows[irow]["Remarks"]).Trim() + "</td></tr>");
                }
                strbuild_Approvers.Append("</table>");
                #endregion

                StringBuilder strbuild = new StringBuilder();

                strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'>");
                strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
                strbuild.Append("<tr><td style='height:20px'></td></tr>");
                strbuild.Append("<tr><td colspan='2'>" + txtEmpName.Text + " has created an PO/ WO with the following details and requested your approval.</td></tr>");
                strbuild.Append("<tr><td style='height:20px'></td></tr>");

                strbuild.Append("<tr><td style='width:20%'>PO/ WO No :-</td><td>" + Convert.ToString(lstTripType.SelectedItem.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td style='width:20%'>PO/ WO Type :-</td><td>" + Convert.ToString(lstPOType.SelectedItem.Text).Trim() + "</td></tr>");
                //strbuild.Append("<tr><td style='width:20%'>PO/ WO Title :-</td><td>" + Convert.ToString(txtPOTitle.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td style='width:20%'>PO/ WO Date :-</td><td>" + Convert.ToString(txtFromdate.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td style='width:20%'>Vendor Name :-</td><td>" + Convert.ToString(txtVendor.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td style='width:20%'>Cost Center :-</td><td>" + Convert.ToString(txtProject.Text).Trim() + "</td></tr>");
                strbuild.Append("<tr><td style='width:20%'>PO/ WO Amount (without Tax) :-</td><td>" + Convert.ToString(dsformatAmt.Tables[0].Rows[0]["Amount_A"]).Trim() + "</td></tr>");
                strbuild.Append("<tr><td style='width:20%'>PO/ WO Amount (with Tax) :-</td><td>" + Convert.ToString(dsformatAmt.Tables[0].Rows[0]["Amount_B"]).Trim() + "</td></tr>");

                //strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(txtCostCenter.Text).Trim() + "</td></tr>");

                strbuild.Append("<tr><td style='height:20px'></td></tr>");


                strbuild.Append("<tr><td style='height:40px'></td></tr>");
                strbuild.Append("<tr><td style='height:20px'><a href='" + strPOWOURL + "'>Please Click here for your action</a></td></tr>");

                strbuild.Append("</table>  <br /><br />");

                strbuild.Append(strbuild_Approvers);

                spm.sendMail_VSCB(sApproverEmail, strSubject, Convert.ToString(strbuild).Trim(), "", "");

                #endregion
            }
        }
        else
        {
            lblmessage.Text = "PO/WO Milestone creation failed. Please contact to admin.";
            return;
        }


        Response.Redirect("VSCB_MyPOWOMilestone.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        if (Convert.ToString(hdnShortClose_Cancelled.Value).Trim() == "cancel")
        {
            if (Convert.ToString(txt_remakrs.Text).Trim() == "")
            {
                lblCancellationRemarks_msg.Text = "Please enter Cancellation Remarks";
                return;
            }
        }
        else
        {
            if (Convert.ToString(txt_remakrs.Text).Trim() == "")
            {
                lblCancellationRemarks_msg.Text = "Please enter Shortclose Remarks";
                return;
            }
        }

        #region Check all Milestone is Shortclose
        /*
         DataTable dsInvoice = new DataTable();
         string strInvoice_Invoice = "";
         string strInvoice_Payment = "";
         string strInvoice_Batch = "";

         SqlParameter[] spars = new SqlParameter[3];

         spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
         spars[0].Value = "get_AllMilestone_Shortclose";

         spars[1] = new SqlParameter("@MstoneID", SqlDbType.Decimal);
         spars[1].Value = Convert.ToDecimal(hdnMstoneId.Value);

         spars[2] = new SqlParameter("@POWOID", SqlDbType.Decimal);
         spars[2].Value = Convert.ToDecimal(hdnPOWOID.Value);

         dsInvoice = spm.getDataList(spars, "SP_VSCB_GETALL_DETAILS");

         if (dsInvoice.Rows.Count > 0)
         {
             for (Int32 irow = 0; irow < dsInvoice.Rows.Count; irow++)
             {
                 if (Convert.ToString(dsInvoice.Rows[irow]["checkremarks"]).Trim() == "InvoiceProcess")
                 {
                     if (Convert.ToString(strInvoice_Invoice).Trim() == "")
                         strInvoice_Invoice = Convert.ToString(dsInvoice.Rows[irow]["InvoiceNo"]).Trim();
                     else
                         strInvoice_Invoice = strInvoice_Invoice + ";" + Convert.ToString(dsInvoice.Rows[irow]["InvoiceNo"]).Trim();
                 }
                 if (Convert.ToString(dsInvoice.Rows[irow]["checkremarks"]).Trim() == "PaymentProcess")
                 {
                     if (Convert.ToString(strInvoice_Payment).Trim() == "")
                         strInvoice_Payment = Convert.ToString(dsInvoice.Rows[irow]["InvoiceNo"]).Trim();
                     else
                         strInvoice_Payment = strInvoice_Payment + ";" + Convert.ToString(dsInvoice.Rows[irow]["InvoiceNo"]).Trim();
                 }
                 if (Convert.ToString(dsInvoice.Rows[irow]["checkremarks"]).Trim() == "BatchProcess")
                 {
                     if (Convert.ToString(strInvoice_Batch).Trim() == "")
                         strInvoice_Batch = Convert.ToString(dsInvoice.Rows[irow]["InvoiceNo"]).Trim();
                     else
                         strInvoice_Batch = strInvoice_Batch + ";" + Convert.ToString(dsInvoice.Rows[irow]["InvoiceNo"]).Trim();
                 }

             }

             string strmsg = "you can't shortclose this PO/WO because some invoices are found under process";
             string strmsg_invoice = "";
             string strmsg_payment = "";
             string strmsg_batch = "";
             bool blnchk = false;

             if (Convert.ToString(strInvoice_Invoice).Trim() != "")
             {
                 strmsg_invoice = "Invoice process.Please cancel this Invoices " + strInvoice_Invoice;
                 blnchk = true;
             }
             if (Convert.ToString(strInvoice_Payment).Trim() != "")
             {
                 strmsg_payment = " Payment process.Please cancel this Payment Request " + strInvoice_Payment;
                 blnchk = true;
             }
             if (Convert.ToString(strInvoice_Batch).Trim() != "")
             {
                 strmsg_batch = "Batch process.Please cancel this Batch Request " + strInvoice_Batch;
                 blnchk = true;
             }

             if(blnchk==true)
             {
                 // lblmessage.Text = strmsg + Convert.ToString(strInvoice_Invoice).Trim() + Convert.ToString(strmsg_payment).Trim() + Convert.ToString(strmsg_batch).Trim();
                 lblmessage.Text = strmsg;
               //  lblMilestoneMsg.Text = strmsg;
                 return;

             }

         }
         */
        #endregion



        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
        if (Convert.ToString(hdnShortClose_Cancelled.Value).Trim() == "shortclose")
            spars[0].Value = "UpdatePOWO_ForShortClose";
        else
            spars[0].Value = "UpdatePOWO_ForCancelled";

        spars[1] = new SqlParameter("@POWOID", SqlDbType.Decimal);
        spars[1].Value = Convert.ToDecimal(lstTripType.SelectedValue);

        spars[2] = new SqlParameter("@PONumber", SqlDbType.VarChar);
        spars[2].Value = Convert.ToString(lstTripType.SelectedItem.Text);

        spars[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
        if (Convert.ToString(txt_remakrs.Text).Trim() != "")
            spars[3].Value = Convert.ToString(txt_remakrs.Text);
        else
            spars[3].Value = DBNull.Value;

        DataSet dShortClose = spm.getDatasetList(spars, "SP_VSCB_INSERT_POWOTAlly");

        if (Convert.ToString(hdnShortClose_Cancelled.Value).Trim() != "shortclose")
        {
            #region Send Email to POWO Creator and approver if Request Reject by Approver

            string strSubject = "OneHR: - " + Convert.ToString(lstTripType.SelectedItem.Text).Trim() + " Cancelled ";
            string sApproverEmail_CC = "";

            #region get Previous approvers emails & approver list

            DataSet dsformatAmt = spm.getFormated_Amount("get_formated_currncy_Amount", Convert.ToString(lstCurrency.SelectedItem.Text), Convert.ToDecimal(txtBasePOWOWAmt.Text), Convert.ToDecimal(txtBasePOWOWAmt.Text), 0, 0, 0);


            DataSet dsMilestone = spm.get_POWO_Approver_List(Convert.ToDouble(lstTripType.SelectedValue), Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(txtProject.Text).Trim(), Convert.ToDecimal(txtBasePOWOWAmt.Text), Convert.ToInt32(lstPOType.SelectedValue));

            for (Int32 irow = 0; irow < dsMilestone.Tables[0].Rows.Count; irow++)
            {
                if (Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() != "" && Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() != "Pending")
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

            #endregion




            StringBuilder strbuild = new StringBuilder();

            strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'>");
            strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td colspan='2'>" + txtEmpName.Text + " has created an PO/ WO with the following details and requested your approval.</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");

            strbuild.Append("<tr><td style='width:20%'>PO/ WO No :-</td><td>" + Convert.ToString(lstTripType.SelectedItem.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td style='width:20%'>PO/ WO Type :-</td><td>" + Convert.ToString(lstPOType.SelectedItem.Text).Trim() + "</td></tr>");
            //strbuild.Append("<tr><td style='width:20%'>PO/ WO Title :-</td><td>" + Convert.ToString(txtPOTitle.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td style='width:20%'>PO/ WO Date :-</td><td>" + Convert.ToString(txtFromdate.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td style='width:20%'>Vendor Name :-</td><td>" + Convert.ToString(txtVendor.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td style='width:20%'>Cost Center :-</td><td>" + Convert.ToString(txtProject.Text).Trim() + "</td></tr>");
            strbuild.Append("<tr><td style='width:20%'>PO/ WO Amount (without Tax) :-</td><td>" + Convert.ToString(dsformatAmt.Tables[0].Rows[0]["Amount_A"]).Trim() + "</td></tr>");
            strbuild.Append("<tr><td style='width:20%'>PO/ WO Amount (with Tax) :-</td><td>" + Convert.ToString(dsformatAmt.Tables[0].Rows[0]["Amount_B"]).Trim() + "</td></tr>");

            //strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(txtCostCenter.Text).Trim() + "</td></tr>");

            strbuild.Append("<tr><td style='height:20px'></td></tr>");


            strbuild.Append("</table>  <br /><br />");


            strbuild.Append(strbuild_Approvers);

            spm.sendMail_VSCB(sApproverEmail_CC, strSubject, Convert.ToString(strbuild).Trim(), "", "");

            #endregion
        }
        //Response.Write(lstTripType.SelectedValue);
        //Response.Write(lstTripType.SelectedItem.Text);
        //Response.End();

        Response.Redirect("VSCB_MyPOWOMilestone.aspx");
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
        txtBasePOWOWAmt.Text = "";

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
        DataSet dtPOWODetails = new DataSet();
        dtPOWODetails = spm.getTallyPOWO_List(Convert.ToString(txtEmpCode.Text).Trim());
        if (dtPOWODetails.Tables[0].Rows.Count > 0)
        {
            lstTripType.DataSource = dtPOWODetails.Tables[0];
            lstTripType.DataTextField = "TPONumber";
            lstTripType.DataValueField = "TID";
            lstTripType.DataBind();
            lstTripType.Items.Insert(0, new ListItem("Select PO/WO Number", "0"));
        }
        if (dtPOWODetails.Tables[1].Rows.Count > 0)
        {
            lstPOType.DataSource = dtPOWODetails.Tables[1];
            lstPOType.DataTextField = "POType";
            lstPOType.DataValueField = "POTypeID";
            lstPOType.DataBind();
            lstPOType.Items.Insert(0, new ListItem("Select PO/WO Type", "0"));
        }

        if (dtPOWODetails.Tables[2].Rows.Count > 0)
        {
            lstCurrency.DataSource = dtPOWODetails.Tables[2];
            lstCurrency.DataTextField = "CurName";
            lstCurrency.DataValueField = "CurID";
            lstCurrency.DataBind();
            lstCurrency.Items.Insert(0, new ListItem("Select Currency Name", "0"));
        }

        if (dtPOWODetails.Tables[3].Rows.Count > 0)
        {
            txtEmpName.Text = Convert.ToString(dtPOWODetails.Tables[3].Rows[0]["Emp_Name"]).Trim();
        }

        if (dtPOWODetails.Tables[4].Rows.Count > 0)
        {
            ddlHPOTYPE.DataSource = dtPOWODetails.Tables[4];
            ddlHPOTYPE.DataTextField = "HPOTypeName";
            ddlHPOTYPE.DataValueField = "HPOTypeID";
            ddlHPOTYPE.DataBind();
            ddlHPOTYPE.Items.Insert(0, new ListItem("Select PO Title", "0"));
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

    public void Get_CurrecnyList()
    {
        DataTable dtPOWODetails = new DataTable();
        dtPOWODetails = spm.getCurrencyList();
        if (dtPOWODetails.Rows.Count > 0)
        {
            lstCurrency.DataSource = dtPOWODetails;
            lstCurrency.DataTextField = "CurName";
            lstCurrency.DataValueField = "CurID";
            lstCurrency.DataBind();
            lstCurrency.Items.Insert(0, new ListItem("Select Currency Name", "0"));
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
        dsMilestone = spm.getMilestoneFrom_Temptable(txtEmpCode.Text, Convert.ToDouble(lstTripType.SelectedValue), Convert.ToString(lstCurrency.SelectedItem.Text));

        if (dsMilestone.Tables[0].Rows.Count > 0)
        {
            dgTravelRequest.DataSource = dsMilestone.Tables[0];
            dgTravelRequest.DataBind();

        }
    }

    public void get_PWODetails_MilestonesList_Update()
    {

        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.getMilestone_edit_View(txtEmpCode.Text, Convert.ToDouble(hdnPOWOID.Value));

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
            lstCurrency.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CurID"]).Trim();
            lstCurrency.Enabled = false;
            txtPOWOPaidAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Paid_Amt"]).Trim();
            txtPoPaidAmt_WithOutDT.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POPiadAmount_withoutDT"]).Trim();
            txtPOWOBalanceAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Bal_Amt"]).Trim();
            txtDirectTaxAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["DirectTaxCollection_Amt"]).Trim();
            txtCostCenter.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["costCenter"]).Trim();
            txtPO_Settlment_Amt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_settlementAmt"]).Trim();

            txtPOWOShortClosedAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["ShortClose_Amt"]).Trim();

            txtPOtype.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POType"]);
            txtPOtype.ToolTip = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POType"]);

            ddlHPOTYPE.SelectedValue = Convert.ToString(dsMilestone.Tables[0].Rows[0]["HPOTypeID"]);
            divScurity_Diposit.Visible = false;
            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["HPOTypeID"]).Trim()== Convert.ToString(ConfigurationManager.AppSettings["VSCBAggrmentPOType"]).Trim())
            {
                txtSecurity_DepositAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Security_Deposit_Amt"]);
                divScurity_Diposit.Visible = true;
                txtSecurity_DepositAmt.Enabled = false;
            }

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["ShippingAddress"]).Trim() != "")
            {
                txtShippingAddress.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["ShippingAddress"]);
            }
            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["PORemarks"]).Trim() != "")
            {
                txtPORemarks.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PORemarks"]);
            }
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
                    hdnSingPOCopyFileName.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["powo_signCopy"]).Trim();
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



            ddlHPOTYPE.Enabled = false;
            lstTripType.Enabled = false;
            lstPOType.Enabled = false;
            lnkfile_PO.Visible = true;

            spnPOWOStatus.Visible = true;
            liPOWOStatus.Visible = true;

            txtPOStatus.Visible = true;
            spnAmountWithTax.Visible = true;
            liAmountWithTax.Visible = true;
            txtPOWOAmt.Visible = true;
            btnCancel.Visible = false;

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

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["appr_StatusId"]).Trim() == "1")
            {
                trvl_btnSave.Visible = false;
            }

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["appr_StatusId"]).Trim() == "2")
            {
                //If uncomment when if Short close Milestone allowed.
                btnCancel.Visible = true;
                trvl_btnSave.Visible = false;

            }

            if (Convert.ToString(txtPOWOBalanceAmt.Text).Trim() != "")
            {
                if (Convert.ToDecimal(txtPOWOBalanceAmt.Text) <= 0)
                {
                    //If uncomment when if Short close Milestone allowed.
                    btnCancel.Visible = false;
                    trvl_btnSave.Visible = false;
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
                trvl_btnSave.Visible = false;
                gvuploadedFiles.Columns[1].Visible = false;
                btnTra_Details.Visible = false;
                btnCancel.Visible = false;
            }



            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["appr_StatusId"]).Trim() == "4" || Convert.ToString(dsMilestone.Tables[0].Rows[0]["appr_StatusId"]).Trim() == "3")
            {
                trvl_btnSave.Visible = false;
            }
            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["appr_StatusId"]).Trim() == "5")
            {
                gvuploadedFiles.Columns[1].Visible = true;
				ddlHPOTYPE.Enabled = true;


			}
            else
            {
                gvuploadedFiles.Columns[1].Visible = false;
				ddlHPOTYPE.Enabled = false;
			}

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["PaymentStatusID"]).Trim() == "2")
            {
                trvl_btnSave.Visible = false;
            }

            if (dsMilestone.Tables[3].Rows.Count > 0)
            {
                btnCancel.Text = "Short Close";
                btnCancel.ToolTip = "Short Close";
                hdnShortClose_Cancelled.Value = "shortclose";
                liRemarks_1.Visible = true;
                liRemarks_2.Visible = true;
                liRemarks_3.Visible = true;

            }
            else
            {
                btnCancel.Text = "Cancel PO/ WO";
                btnCancel.ToolTip = "Cancel PO/ WO";
                hdnShortClose_Cancelled.Value = "cancel";
                liRemarks_1.Visible = true;
                liRemarks_2.Visible = true;
                liRemarks_3.Visible = true;

            }

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["appr_StatusId"]).Trim() == "2")
            {
                string spo_number = Regex.Replace(Convert.ToString(lstTripType.SelectedItem.Text), @"[^0-9a-zA-Z\._]", "_");

                if (!File.Exists(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBApprovedPOWOfiles"]).Trim() + spo_number + ".pdf")))
                    clsDownloadPOWO.POWODownload_Word_PDFNew(Convert.ToString(hdnPOWOID.Value), Convert.ToString(lstTripType.SelectedItem.Text));
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
        trvl_btnSave.Visible = false;
        btnTra_Details.Visible = false;
        if (dsApprover != null)
        {

            if (dsApprover.Tables[0].Rows.Count > 0)
            {

                DgvApprover.DataSource = dsApprover.Tables[0];
                DgvApprover.DataBind();
                trvl_btnSave.Visible = true;
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

        #region PO download RDLC Code not required

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

        #endregion
    }

    protected void btnback_mng_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/VSCB_MyPOWOMilestone.aspx");

    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        DataSet dsPOWOContent = new DataSet();

        #region get POWO Content 

        SqlParameter[] spars = new SqlParameter[7];
        string HFChkFlagDraftCopy = "0";
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Insert_POWO_TempDraftCopy";

        spars[1] = new SqlParameter("@POWOID", SqlDbType.Decimal);
        spars[1].Value = Convert.ToDouble(lstTripType.SelectedValue);

        if (Request.QueryString.Count == 0)
        {
            HFChkFlagDraftCopy = "0";
            spars[2] = new SqlParameter("@CurrencyName", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(lstCurrency.SelectedItem.Text); //"EN-IN";
            spars[3] = new SqlParameter("@POHTMLContain", SqlDbType.VarChar);
            spars[3].Value = txt_POWOContent_description.Text;
        }
        else
        {
            HFChkFlagDraftCopy = "1";
            spars[2] = new SqlParameter("@CurrencyName", SqlDbType.VarChar);
            spars[2].Value = "";
            spars[3] = new SqlParameter("@POHTMLContain", SqlDbType.VarChar);
            spars[3].Value = "";
        }
        spars[4] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[4].Value = txtEmpCode.Text;

        spars[5] = new SqlParameter("@ShipingAddress_P", SqlDbType.VarChar);
        if (Convert.ToString(txtShippingAddress.Text).Trim() != "")
            spars[5].Value = Convert.ToString(txtShippingAddress.Text).Trim();
        else
            spars[5].Value = DBNull.Value;


        spars[6] = new SqlParameter("@HPOTYPEID", SqlDbType.Int);
        if (ddlHPOTYPE.SelectedValue != "0")
            spars[6].Value = Convert.ToInt32(ddlHPOTYPE.SelectedValue);
        else
            spars[6].Value = 1;


        dsPOWOContent = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");

        string url = "VSCB_DraftCopy.aspx?ID=" + HFChkFlagDraftCopy + "";
        string s = "window.open('" + url + "', 'popup_window', 'width=800,height=790,scrollbars=no, menubar=no,resizable=no,scrollbars=yes,toolbar=no,directories=no,location=no');";
        //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        ///window.open(url, 'popUpWindow', 'height=' + height + ', width=' + width + ', resizable=yes, scrollbars=yes, toolbar=yes')
        //ClientScript.RegisterStartupScript(this.GetType(), "popupWindow(url,'','window',200,100)","",true); 
        ClientScript.RegisterStartupScript(this.GetType(), "popupwindow(" + url + ")", s, true);

        #endregion 



        #region following Draft Copy Code not required

        /*DataSet dsPOWOContent = new DataSet();

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
          */
        #endregion
    }
    public void GetHPODetails()
    {
        DataTable dtHPODetails = new DataTable();
        dtHPODetails = spm.getHPO_List();
        if (dtHPODetails.Rows.Count > 0)
        {
            ddlHPOTYPE.DataSource = dtHPODetails;
            ddlHPOTYPE.DataTextField = "HPOTypeName";
            ddlHPOTYPE.DataValueField = "HPOTypeID";
            ddlHPOTYPE.DataBind();
            ddlHPOTYPE.Items.Insert(0, new ListItem("Select PO Title", "0"));
        }
    }



    protected void ddlHPOTYPE_SelectedIndexChanged(object sender, EventArgs e)
    {
        divScurity_Diposit.Visible = false;
        txtSecurity_DepositAmt.Text = "";
        if (Convert.ToString(ddlHPOTYPE.SelectedValue).Trim() == "3")
        {
            divScurity_Diposit.Visible = true;
        }
    }
}