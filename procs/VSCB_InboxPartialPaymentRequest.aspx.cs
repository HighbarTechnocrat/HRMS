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

public partial class procs_VSCB_InboxPartialPaymentRequest : System.Web.UI.Page
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
					GetPOWONo();
					GetVendorName();
					GetPOWODate();
					GetInvoiceNo();
					GetPaymentRequestNo();
					GetPaymentRequestDate();
					GetPaymentStatus();
					GetDepartment();
					GetCostCentreDepartment();
					GetPayment_Request_PeindingList();
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
			string Result = "";
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			hdnRecruitment_ReqID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
			hdnPOID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
			Result = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
			if (Result == "True")
			{
				Response.Redirect("VSCB_CreatePartialPaymentRequest.aspx?Payment_ID=" + hdnRecruitment_ReqID.Value + "&POID=" + hdnPOID.Value);
			}
			else
			{
				Response.Redirect("VSCB_CreatePartialPay_Without_PO.aspx?Payment_ID=" + hdnRecruitment_ReqID.Value + "&Type=APP");
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
		GetPayment_Request_PeindingList();
	}

	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		GetPayment_Request_PeindingList();
	}

	protected void mobile_btnBack_Click(object sender, EventArgs e)
	{
		lstInvoiceNo.SelectedIndex = -1;
		lstPOWODate.SelectedIndex = -1;
		lstPOWONo.SelectedIndex = -1;
		lstpaymentRequestDate.SelectedIndex = -1;
		lstPaymentRequestNo.SelectedIndex = -1;
		lstVendorName.SelectedIndex = -1;
		lstDepartment.SelectedIndex = -1;
		lstStatus.SelectedIndex = -1;
		lstCostCentre.SelectedIndex = -1;
		txtPaymentRequestamt.Text = "";
		lblmessage.Text = "";
		GetPayment_Request_PeindingList();
	}
	#endregion
	#region PageMethod
	private void GetPayment_Request_PeindingList()
	{
		try
		{
			DataTable RequisitionList = new DataTable();
			int Payment_ID = 0, Vendor_ID = 0, InvoiceID = 0, POID = 0, Status_id = 0, Dept_ID=0;
			lblmessage.Text = ""; DateTime PaymentDate;
			string strtoDate = "", POWODate = "", PayementRDates = "", PayementAmt = "",CostCentre="";
			string[] strdate;
			POID = Convert.ToString(lstPOWONo.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPOWONo.SelectedValue) : 0;
			Vendor_ID = Convert.ToString(lstVendorName.SelectedValue).Trim() != "" ? Convert.ToInt32(lstVendorName.SelectedValue) : 0;
			InvoiceID = Convert.ToString(lstInvoiceNo.SelectedValue).Trim() != "" ? Convert.ToInt32(lstInvoiceNo.SelectedValue) : 0;
			Payment_ID = Convert.ToString(lstPaymentRequestNo.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPaymentRequestNo.SelectedValue) : 0;
			Status_id = Convert.ToString(lstStatus.SelectedValue).Trim() != "" ? Convert.ToInt32(lstStatus.SelectedValue) : 0;
			POWODate = Convert.ToString(lstPOWODate.SelectedValue).Trim() != "" ? Convert.ToString(lstPOWODate.SelectedValue) : "";
			PayementRDates = Convert.ToString(lstpaymentRequestDate.SelectedValue).Trim() != "" ? Convert.ToString(lstpaymentRequestDate.SelectedValue) : "";
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
			if (PayementRDates != "")
			{
				strdate = Convert.ToString(PayementRDates).Trim().Split('-');
				strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
				PaymentDate = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
				PayementRDates = PaymentDate.ToString("yyyy-MM-dd");
			}
			RequisitionList = spm.GetVSCB_Payment_Request_PendingList(Session["Empcode"].ToString(), "SelectPartialPaymentRequestList", POID, Vendor_ID, InvoiceID, Payment_ID, Status_id, POWODate, PayementRDates, PayementAmt, Dept_ID,CostCentre);
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
	private void GetPOWONo()
	{
		try
		{
			dtPOWONo = new DataTable();
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "Select_POWONo_Partial");
			lstPOWONo.DataSource = dtPOWONo;
			lstPOWONo.DataTextField = "PONumber";
			lstPOWONo.DataValueField = "POID";
			lstPOWONo.DataBind();
			lstPOWONo.Items.Insert(0, new ListItem("Select PO/WO Number.", ""));
		}
		catch (Exception)
		{

		}

	}
	private void GetPOWODate()
	{
		try
		{
			dtPOWONo = new DataTable();
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "Select_POWODate_Partial");
			lstPOWODate.DataSource = dtPOWONo;
			lstPOWODate.DataTextField = "PODate";
			lstPOWODate.DataValueField = "PODate";//POID
			lstPOWODate.DataBind();
			lstPOWODate.Items.Insert(0, new ListItem("Select PO/WO Date", ""));
		}
		catch (Exception)
		{

		}
	}
	private void GetVendorName()
	{
		try
		{
			dtPOWONo = new DataTable();
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectVendorName_Partial");
			lstVendorName.DataSource = dtPOWONo;
			lstVendorName.DataTextField = "Name";
			lstVendorName.DataValueField = "VendorID";
			lstVendorName.DataBind();
			lstVendorName.Items.Insert(0, new ListItem("Select Vendor Name", ""));
		}
		catch (Exception)
		{

		}
	}
	private void GetInvoiceNo()
	{
		try
		{
			dtPOWONo = new DataTable();
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectInvoiceNo_Partial");
			lstInvoiceNo.DataSource = dtPOWONo;
			lstInvoiceNo.DataTextField = "InvoiceNo";
			lstInvoiceNo.DataValueField = "InvoiceID";
			lstInvoiceNo.DataBind();
			lstInvoiceNo.Items.Insert(0, new ListItem("Select Invoice No", ""));
		}
		catch (Exception)
		{

		}
	}
	private void GetCostCentreDepartment()
	{
		try
		{
			dtPOWONo = new DataTable();
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectCostCenter_Partial");
			lstCostCentre.DataSource = dtPOWONo;
			lstCostCentre.DataTextField = "CostCentre";
			lstCostCentre.DataValueField = "CostCentre";
			lstCostCentre.DataBind();
			lstCostCentre.Items.Insert(0, new ListItem("Select Cost Center", ""));
		}
		catch (Exception)
		{

		}
	}
	private void GetDepartment()
	{
		try
		{
			dtPOWONo = new DataTable();
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectDepartment_Partial");
			lstDepartment.DataSource = dtPOWONo;
			lstDepartment.DataTextField = "Department";
			lstDepartment.DataValueField = "Dept_ID";
			lstDepartment.DataBind();
			lstDepartment.Items.Insert(0, new ListItem("Select Department", ""));
		}
		catch (Exception)
		{

		}
	}
	private void GetPaymentRequestNo()
	{
		try
		{
			dtPOWONo = new DataTable();
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectPaymentRequestNo_Partial");
			lstPaymentRequestNo.DataSource = dtPOWONo;
			lstPaymentRequestNo.DataTextField = "PaymentReqNo";
			lstPaymentRequestNo.DataValueField = "Payment_ID";
			lstPaymentRequestNo.DataBind();
			lstPaymentRequestNo.Items.Insert(0, new ListItem("Select Payment Request No", ""));
		}
		catch (Exception)
		{

		}
	}
	private void GetPaymentRequestDate()
	{
		try
		{
			dtPOWONo = new DataTable();
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectPaymentRequestDate_Partial");
			lstpaymentRequestDate.DataSource = dtPOWONo;
			lstpaymentRequestDate.DataTextField = "PaymentReqDate";
			lstpaymentRequestDate.DataValueField = "PaymentReqDate";//Payment_ID
			lstpaymentRequestDate.DataBind();
			lstpaymentRequestDate.Items.Insert(0, new ListItem("Select Payment Request Date", ""));
		}
		catch (Exception)
		{

		}
	}
	private void GetPaymentStatus()
	{
		try
		{
			dtPOWONo = new DataTable();
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectMyPaymentStatus");
			lstStatus.DataSource = dtPOWONo;
			lstStatus.DataTextField = "PyamentStatus";
			lstStatus.DataValueField = "PaymentStatusID";
			lstStatus.DataBind();
			lstStatus.Items.Insert(0, new ListItem("Select Status", ""));
		}
		catch (Exception)
		{

		}
	}
	#endregion
}