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
public partial class procs_VSCB_ApprovedPaymentRequestViewForChange : System.Web.UI.Page
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
		Response.Redirect(ReturnUrl("sitepathmain") + "procs/VSCB_Index.aspx");
	}

    #endregion

    #region PageEvent

    
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
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/vscb_index");
			}
			else
			{
				hdnEmpCode.Value = Session["Empcode"].ToString();
				if (!Page.IsPostBack)
				{

					if (Request.QueryString.Count > 0)
					{
						hdnInboxType.Value = Convert.ToString(Request.QueryString[0]).Trim();
					}

					get_DropdownList();
					GetPayment_Request_List();
					this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}
	protected void lnkView_Click(object sender, ImageClickEventArgs e)
	{
		try
		{
			string IsInvoiceWithPO = "";
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			hdnRecruitment_ReqID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
			hdnPOID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
			IsInvoiceWithPO = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
			if (IsInvoiceWithPO == "True")
			{
				Response.Redirect("VSCB_PaymentRequestView.aspx?Payment_ID=" + hdnRecruitment_ReqID.Value + "&POID=" + hdnPOID.Value+"&IsAmtApprover=1");
			}
			else
			{
				Response.Redirect("VSCB_PaymentApproval_Without_PO.aspx?Payment_ID=" + hdnRecruitment_ReqID.Value + "&Type=View");
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}

	protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		gvMngTravelRqstList.PageIndex = e.NewPageIndex;
		GetPayment_Request_List();
	}

	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		GetPayment_Request_List();
	}

	protected void mobile_btnBack_Click(object sender, EventArgs e)
	{
		get_DropdownList();
		txtPaymentRequestamt.Text = "";
		txtFromdate.Text = "";
		txtToDate.Text = "";
		lblmessage.Text = "";
		GetPayment_Request_List();
	}
	#endregion

	#region PageMethod
	private void GetPayment_Request_List()
	{
		try
		{
			DataTable RequisitionList = new DataTable();
			int Payment_ID = 0, Vendor_ID = 0, InvoiceID = 0, POID = 0, Status_id = 0, Dept_ID = 0;
			lblmessage.Text = ""; DateTime PaymentDate;
			string strtoDate = "", POWODate = "", PayementRDates = "", PayementAmt = "", CostCentre = "", PayementReqFromDates = "", PayementReqToDates = "", strfromDate, strToDate;
			string[] strdate, strPayReqdate;
			POID = Convert.ToString(lstPOWONo.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPOWONo.SelectedValue) : 0;
			Vendor_ID = Convert.ToString(lstVendorName.SelectedValue).Trim() != "" ? Convert.ToInt32(lstVendorName.SelectedValue) : 0;
			InvoiceID = Convert.ToString(lstInvoiceNo.SelectedValue).Trim() != "" ? Convert.ToInt32(lstInvoiceNo.SelectedValue) : 0;
			Payment_ID = Convert.ToString(lstPaymentRequestNo.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPaymentRequestNo.SelectedValue) : 0;
			POWODate = Convert.ToString(lstPOWODate.SelectedValue).Trim() != "" ? Convert.ToString(lstPOWODate.SelectedValue) : "";
			PayementAmt = Convert.ToString(txtPaymentRequestamt.Text).Trim() != "" ? Convert.ToString(txtPaymentRequestamt.Text) : "";
			Dept_ID = Convert.ToString(lstDepartment.SelectedValue).Trim() != "" ? Convert.ToInt32(lstDepartment.SelectedValue) : 0;
			CostCentre = Convert.ToString(lstCostCentre.SelectedValue).Trim() != "" ? Convert.ToString(lstCostCentre.SelectedValue) : "";

			if (POWODate != "")
			{
				strdate = Convert.ToString(POWODate).Trim().Split('-');
				strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
				PaymentDate = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
				POWODate = PaymentDate.ToString("yyyy-MM-dd");
			}
			if (Convert.ToString(txtFromdate.Text) == "" && POID == 0 && Vendor_ID == 0 && InvoiceID == 0 && Payment_ID == 0 && Status_id == 0 && POWODate == "" && PayementAmt == "" && Dept_ID == 0 && CostCentre == "")
			{
				PayementReqFromDates = Convert.ToString(hdnPayementReqFromDate.Value);
			}
			else
			{
				if (Convert.ToString(txtFromdate.Text).Trim() != "")
				{
					strPayReqdate = Convert.ToString(txtFromdate.Text).Trim().Split('-');
					strfromDate = Convert.ToString(strPayReqdate[2]) + "-" + Convert.ToString(strPayReqdate[1]) + "-" + Convert.ToString(strPayReqdate[0]);
					PayementReqFromDates = Convert.ToString(strfromDate);
				}

			}
			if (Convert.ToString(txtToDate.Text) == "" && POID == 0 && Vendor_ID == 0 && InvoiceID == 0 && Payment_ID == 0 && Status_id == 0 && POWODate == "" && PayementAmt == "" && Dept_ID == 0 && CostCentre == "")
			{
				PayementReqToDates = Convert.ToString(hdnPayementReqToDate.Value);
			}
			else
			{
				if (Convert.ToString(txtToDate.Text).Trim() != "")
				{
					strPayReqdate = Convert.ToString(txtToDate.Text).Trim().Split('-');
					strtoDate = Convert.ToString(strPayReqdate[2]) + "-" + Convert.ToString(strPayReqdate[1]) + "-" + Convert.ToString(strPayReqdate[0]);
					PayementReqToDates = Convert.ToString(strtoDate);
				}

			}
			RequisitionList = spm.GetVSCB_Payment_Request_ApprovedListForChange(Session["Empcode"].ToString(), "getApprovedPaymentRequestListForAmtPaidAccChange", POID, Vendor_ID, InvoiceID, Payment_ID, Status_id, POWODate, PayementRDates, PayementAmt, Dept_ID, CostCentre, PayementReqFromDates, PayementReqToDates);

			RecordCount.Text = "";
			gvMngTravelRqstList.DataSource = null;
			gvMngTravelRqstList.DataBind();
			if (RequisitionList.Rows.Count > 0)
			{
				RecordCount.Text = "Record Count : " + Convert.ToString(RequisitionList.Rows.Count);
				gvMngTravelRqstList.DataSource = RequisitionList;
				gvMngTravelRqstList.DataBind();
			}
			else
			{
				lblmessage.Text = "Record's not found.!";
			}

		}
		catch (Exception ex)
		{

		}
	}
	private void get_DropdownList()
	{
		DataSet dsList = new DataSet();
		SqlParameter[] spars = new SqlParameter[2];

		spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
		spars[0].Value = "get_PaymentApprovedListForAmtPaidAccChange_Search_DropdownList";

		spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
		spars[1].Value = hdnEmpCode.Value;

		dsList = spm.getDatasetList(spars, "sp_VSCB_CreatePOWO_Users");

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
			lstPOWODate.DataSource = dsList.Tables[1];
			lstPOWODate.DataTextField = "PODate";
			lstPOWODate.DataValueField = "PODate";
			lstPOWODate.DataBind();
		}
		lstPOWODate.Items.Insert(0, new ListItem("Select PO/WO Date", ""));

		if (dsList.Tables[2].Rows.Count > 0)
		{
			lstVendorName.DataSource = dsList.Tables[2];
			lstVendorName.DataTextField = "Name";
			lstVendorName.DataValueField = "VendorId";
			lstVendorName.DataBind();
		}
		lstVendorName.Items.Insert(0, new ListItem("Select Vendor Name", ""));



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
			lstPaymentRequestNo.DataSource = dsList.Tables[4];
			lstPaymentRequestNo.DataTextField = "PaymentReqNo";
			lstPaymentRequestNo.DataValueField = "Payment_ID";
			lstPaymentRequestNo.DataBind();
		}
		lstPaymentRequestNo.Items.Insert(0, new ListItem("Select Payment Request No", ""));


		if (dsList.Tables[5].Rows.Count > 0)
		{
			lstCostCentre.DataSource = dsList.Tables[5];
			lstCostCentre.DataTextField = "CostCentre";
			lstCostCentre.DataValueField = "CostCentre";
			lstCostCentre.DataBind();
		}
		lstCostCentre.Items.Insert(0, new ListItem("Select Cost Center", ""));

		hdnPayementReqFromDate.Value = Convert.ToString(dsList.Tables[6].Rows[0]["PayementReqFromDate"]);
		hdnPayementReqToDate.Value = Convert.ToString(dsList.Tables[6].Rows[0]["PayementReqToDate"]);

	}
	#endregion
}