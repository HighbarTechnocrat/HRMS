using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Xml;
using System.Text;
using System.IO;
using System.Data.Common;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;

public partial class procs_VSCB_ViewAllDetails : System.Web.UI.Page
{
    #region Creative_Default_methods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public DataTable dtPOWONo;
    public int did = 0;

    SP_Methods spm = new SP_Methods();
    string strempcode = "";
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void lnkcont_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/Reembursementindex");
    }

    #endregion
    protected void Page_Load(object sender, EventArgs e)
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                // Page.SmartNavigation = true;
                hdnEmpCode.Value = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {

                    if (Request.QueryString.Count > 0)
                    {
                        hdnInboxType.Value = Convert.ToString(Request.QueryString[0]).Trim();
                    }

                    get_DropdownList();
                    get_BatchApprovedList();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    private void get_DropdownList()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_BatchApprovedList_Search_DropdownList";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCode.Value;

        dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        if (dsList.Tables[0].Rows.Count > 0)
        {
            lstPOWONo.DataSource = dsList.Tables[0];
            lstPOWONo.DataTextField = "PONumber";
            lstPOWONo.DataValueField = "POID";
            lstPOWONo.DataBind();
        }
        lstPOWONo.Items.Insert(0, new ListItem("Select PO/WO Number", ""));

        if (dsList.Tables[1].Rows.Count > 0)
        {
            lstVendorName.DataSource = dsList.Tables[1];
            lstVendorName.DataTextField = "Name";
            lstVendorName.DataValueField = "VendorId";
            lstVendorName.DataBind();
        }
        lstVendorName.Items.Insert(0, new ListItem("Select Vendor Name", ""));

        if (dsList.Tables[2].Rows.Count > 0)
        {
            lstCostCentre.DataSource = dsList.Tables[2];
            lstCostCentre.DataTextField = "CostCentre";
            lstCostCentre.DataValueField = "CostCentre";
            lstCostCentre.DataBind();
        }
        lstCostCentre.Items.Insert(0, new ListItem("Select Cost Center", ""));

        if (dsList.Tables[3].Rows.Count > 0)
        {
            lstInvoiceNo.DataSource = dsList.Tables[3];
            lstInvoiceNo.DataTextField = "InvoiceNo";
            lstInvoiceNo.DataValueField = "InvoiceID";
            lstInvoiceNo.DataBind();
        }
        lstInvoiceNo.Items.Insert(0, new ListItem("Select Invoice No", ""));



        if (dsList.Tables[4].Rows.Count > 0)
        {
            lstBatchNo.DataSource = dsList.Tables[4];
            lstBatchNo.DataTextField = "Batch_No";
            lstBatchNo.DataValueField = "Batch_ID";
            lstBatchNo.DataBind();
        }
        lstBatchNo.Items.Insert(0, new ListItem("Select Batch Number", ""));

        hdnInvoiceFromDate.Value = Convert.ToString(dsList.Tables[5].Rows[0]["InvoiceFromDate"]);
        hdnInvoiceToDate.Value = Convert.ToString(dsList.Tables[5].Rows[0]["InvoiceReqToDate"]);
    }

    private void get_BatchApprovedList()
    {
        try
        {
            DataTable RequisitionList = new DataTable();
            int Vendor_ID = 0, InvoiceID = 0, POID = 0, batchId = 0;
            lblmessage.Text = "";
            string CostCentre = "",invoiceFromDate="", invoiceToDate = "", strfromDate="", strtoDate="";
            string[]  strInvdate;

            POID = Convert.ToString(lstPOWONo.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPOWONo.SelectedValue) : 0;
            Vendor_ID = Convert.ToString(lstVendorName.SelectedValue).Trim() != "" ? Convert.ToInt32(lstVendorName.SelectedValue) : 0;
            InvoiceID = Convert.ToString(lstInvoiceNo.SelectedValue).Trim() != "" ? Convert.ToInt32(lstInvoiceNo.SelectedValue) : 0;
            CostCentre = Convert.ToString(lstCostCentre.SelectedValue).Trim() != "" ? Convert.ToString(lstCostCentre.SelectedValue) : "";
            batchId = Convert.ToString(lstBatchNo.SelectedValue).Trim() != "" ? Convert.ToInt32(lstBatchNo.SelectedValue) : 0;

            if (Convert.ToString(txtFromdate.Text) == "" && POID == 0 && Vendor_ID == 0 && InvoiceID == 0 && CostCentre == "" && batchId==0)
            {
                invoiceFromDate = Convert.ToString(hdnInvoiceFromDate.Value);
            }
            else
            {
                if (Convert.ToString(txtFromdate.Text).Trim() != "")
                {
                    strInvdate = Convert.ToString(txtFromdate.Text).Trim().Split('-');
                    strfromDate = Convert.ToString(strInvdate[2]) + "-" + Convert.ToString(strInvdate[1]) + "-" + Convert.ToString(strInvdate[0]);
                    invoiceFromDate = Convert.ToString(strfromDate);
                }

            }
            if (Convert.ToString(txtToDate.Text) == "" && POID == 0 && Vendor_ID == 0 && InvoiceID == 0 && CostCentre == "" && batchId == 0)
            {
                invoiceToDate = Convert.ToString(hdnInvoiceToDate.Value);
            }
            else
            {
                if (Convert.ToString(txtToDate.Text).Trim() != "")
                {
                    strInvdate = Convert.ToString(txtToDate.Text).Trim().Split('-');
                    strtoDate = Convert.ToString(strInvdate[2]) + "-" + Convert.ToString(strInvdate[1]) + "-" + Convert.ToString(strInvdate[0]);
                    invoiceToDate = Convert.ToString(strtoDate);
                }

            }

            RequisitionList = spm.GetVSCB_BatchApprovedList(Convert.ToString(hdnEmpCode.Value), "getBatchedApprovedDetails", POID, Vendor_ID, InvoiceID, CostCentre, batchId,invoiceFromDate,invoiceToDate);

            RecordCount.Text = "";
            gvMngTravelRqstList.DataSource = null;
            gvMngTravelRqstList.DataBind();
            if (RequisitionList.Rows.Count > 0)
            {
                btnCancel.Visible = true;
                RecordCount.Text = "Record Count : " + Convert.ToString(RequisitionList.Rows.Count);
                gvMngTravelRqstList.DataSource = RequisitionList;
                gvMngTravelRqstList.DataBind();
            }
            else
            {
                btnCancel.Visible = false;
                lblmessage.Text = "Record's not found.!";
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        get_BatchApprovedList();
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        get_DropdownList();
        gvMngTravelRqstList.DataSource = null;
        gvMngTravelRqstList.DataBind();
        lblmessage.Text = "";
        RecordCount.Text = "";
        txtFromdate.Text = "";
        txtToDate.Text = "";
        btnCancel.Visible = false;
    }

    protected void lnkView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string InvoiceID = "", Batch_ID = "";
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            Batch_ID = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
            hdnPOID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
            InvoiceID = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
            hdnRecruitment_ReqID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[3]).Trim();

            Response.Redirect("VSCB_ViewAllDetailsApprovedBatch.aspx?Payment_ID=" + hdnRecruitment_ReqID.Value + "&POID=" + hdnPOID.Value + "&batchid=" + Batch_ID+ "&InvoiceID="+InvoiceID);

        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMngTravelRqstList.PageIndex = e.NewPageIndex;
        get_BatchApprovedList();
    }
    public void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "ApprovedBatchDetails_" + DateTime.Now + ".xls";

        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);

        gvMngTravelRqstList.AllowPaging = false;
        get_BatchApprovedList();
        this.gvMngTravelRqstList.Columns[0].Visible = false;
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

        gvMngTravelRqstList.GridLines = GridLines.Both;
        gvMngTravelRqstList.HeaderStyle.Font.Bold = true;
        gvMngTravelRqstList.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}