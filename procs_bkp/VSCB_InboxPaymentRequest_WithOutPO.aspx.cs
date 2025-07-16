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

public partial class procs_VSCB_InboxPaymentRequest_WithOutPO : System.Web.UI.Page
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
					GetVendorName();
					GetInvoiceNo();
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
			string InvoiceWithPO = "";
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			hdnRecruitment_ReqID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
			//hdnPOID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
			//InvoiceWithPO = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
			Response.Redirect("VSCB_Payment_Without_PO.aspx?InvoiceID=" + hdnRecruitment_ReqID.Value + "&Type=Create");		
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}
	protected void lnkView_Click(object sender, EventArgs e)
	{
		try
		{
			int Payment_ID = 0;
			LinkButton btn = (LinkButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;			
			hdnRecruitment_ReqID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
			Response.Redirect("VSCB_Payment_Without_PO.aspx?InvoiceID=" + hdnRecruitment_ReqID.Value + "&Type=Create");

		}
		catch (Exception)
		{

			throw;
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
		lstVendorName.SelectedIndex = -1;
		lstStatus.SelectedIndex = -1;
		lstCostCentre.SelectedIndex = -1;
		lstDepartment.SelectedIndex = -1;
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
			int Payment_ID = 0, Vendor_ID = 0, InvoiceID = 0, POID = 0, Status_id = 0, Dept_ID = 0;
			lblmessage.Text = ""; 
			string POWODate = "", PayementRDates = "", PayementAmt = "", CostCentor = "";
			Vendor_ID = Convert.ToString(lstVendorName.SelectedValue).Trim() != "" ? Convert.ToInt32(lstVendorName.SelectedValue) : 0;
			InvoiceID = Convert.ToString(lstInvoiceNo.SelectedValue).Trim() != "" ? Convert.ToInt32(lstInvoiceNo.SelectedValue) : 0;
			Status_id = Convert.ToString(lstStatus.SelectedValue).Trim() != "" ? Convert.ToInt32(lstStatus.SelectedValue) : 0;
            Dept_ID = Convert.ToString(lstDepartment.SelectedValue).Trim() != "" ? Convert.ToInt32(lstDepartment.SelectedValue) : 0;
			CostCentor = Convert.ToString(lstCostCentre.SelectedValue).Trim() != "" ? Convert.ToString(lstCostCentre.SelectedValue) : "";
			
			RequisitionList = spm.GetVSCB_Payment_Request_PendingList(Session["Empcode"].ToString(), "SelectInvoiceRequestList", POID, Vendor_ID, InvoiceID, Payment_ID, Status_id, POWODate, PayementRDates, PayementAmt, Dept_ID, CostCentor);
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
	private void GetCostCentreDepartment()
	{
		try
		{
			dtPOWONo = new DataTable();
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectCostCenter_Invoice");
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
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectMyDepartment_Invoice");
			lstDepartment.DataSource = dtPOWONo;
			lstDepartment.DataTextField = "Department";
			lstDepartment.DataValueField = "Dept_ID";
			lstDepartment.DataBind();
			lstDepartment.Items.Insert(0, new ListItem("Select Project/Department", ""));
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
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectVendor_WithOutPO");
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
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectInvoice_WithOutPO");
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