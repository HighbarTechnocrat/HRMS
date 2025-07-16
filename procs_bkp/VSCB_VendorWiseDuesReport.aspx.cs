using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
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
using System.Data;
using Microsoft.Reporting.WebForms;

public partial class procs_VendorWiseDuesReport : System.Web.UI.Page
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
					//GetPOWONo();
					//GetVendorName();
					//GetPOWODate();
					//GetInvoiceNo();
					//GetPaymentRequestNo();
					//GetPaymentRequestDate();
					//GetPaymentStatus();
					//GetDepartment();
					//GetCostCentre();
					GetDropDownList();
					txtPaymntReqToDt.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					txtPaymntReqFrmDt.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}
	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		GetPayment_Request_List();
	}

	protected void mobile_btnBack_Click(object sender, EventArgs e)
	{
		lstInvoiceNo.SelectedIndex = -1;
		lstPOWODate.SelectedIndex = -1;
		lstPOWONo.SelectedIndex = -1;
		lstpaymentRequestDate.SelectedIndex = -1;
		lstPaymentRequestNo.SelectedIndex = -1;
		lstVendorName.SelectedIndex = -1;
		lstStatus.SelectedIndex = -1;
		lstCostCentre.SelectedIndex = -1;
		//lstDepartment.SelectedIndex = -1;
		txtPaymntReqFrmDt.Text = "";
		txtPaymntReqToDt.Text = "";
		lblmessage.Text = "";
		//GetPayment_Request_List();
	}
	#endregion
	#region PageMethod
	private void GetPayment_Request_List()
	{
		try
		{
			DataSet RequisitionList = new DataSet();
			string Payment_ID = string.Empty, Vendor_ID = string.Empty, InvoiceID = string.Empty, POID = string.Empty, Status_id = string.Empty, Dept_ID = string.Empty;
			lblmessage.Text = ""; DateTime PaymentDate;
			string QueueType = "", ReportInvoice = "", ReportPayment = "", strtoDate = "", POWODate = "", PayementRDates = "", CostCentre = "",
				ProjectName = "", Parameter = "", Parameter2 = "", ReportBy = "", FromDate = "", Todate = "" ;
			string[] strdate;

			POID = String.Join(",", lstPOWONo.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
			Vendor_ID = String.Join(",", lstVendorName.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
			InvoiceID = String.Join(",", lstInvoiceNo.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
			Payment_ID = String.Join(",", lstPaymentRequestNo.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
			Status_id = String.Join(",", lstStatus.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
			POWODate = String.Join(",", lstPOWODate.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
			PayementRDates = String.Join(",", lstpaymentRequestDate.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
			//Dept_ID = String.Join(",", lstDepartment.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
			CostCentre = String.Join(",", lstCostCentre.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());


			if (POWODate != "")
			{
				strdate = Convert.ToString(POWODate).Trim().Split('-');
				strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
				PaymentDate = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
				POWODate = PaymentDate.ToString("yyyy-MM-dd");
			}
			if (txtPaymntReqFrmDt.Text != "")
			{
				strdate = Convert.ToString(txtPaymntReqFrmDt.Text).Trim().Split('/');
				strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
				PaymentDate = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
				FromDate = PaymentDate.ToString("yyyy-MM-dd");
			}

			if (txtPaymntReqToDt.Text != "")
			{
				strdate = Convert.ToString(txtPaymntReqToDt.Text).Trim().Split('/');
				strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
				PaymentDate = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
				Todate = PaymentDate.ToString("yyyy-MM-dd");
			}

			QueueType = "SelectWOPOWiseDuesReport";
			ReportInvoice = "AuditTrailReport";
			RequisitionList = spm.GetVSCB_Payment_Request_ReportList(Session["Empcode"].ToString(), QueueType, POID, Vendor_ID, InvoiceID, Payment_ID, Status_id, POWODate, PayementRDates, CostCentre, Dept_ID, FromDate, Todate);
			if (RequisitionList.Tables[0].Rows.Count > 0)
			{
				if (POID != "")
				{
					if (!POID.ToLower().Contains(','))
					{
						ProjectName = "PO/WO No. : " + lstPOWONo.SelectedItem.Text;
					}
				}
				if (POWODate != "")
				{
					if (!POWODate.ToLower().Contains(','))
					{
						ProjectName = ProjectName + " PO/WO Date : " + lstPOWODate.SelectedItem.Text;
					}
				}
				if (Vendor_ID != "")
				{
					if (!Vendor_ID.ToLower().Contains(','))
					{
						ProjectName = ProjectName + " Vendor Name  : " + lstVendorName.SelectedItem.Text;
					}
				}
				if (InvoiceID != "")
				{
					if (!InvoiceID.ToLower().Contains(','))
					{
						Parameter = " Invoice No : " + lstInvoiceNo.SelectedItem.Text;
					}
				}
				//if (Dept_ID != "")
				//{
				//	if (!Dept_ID.ToLower().Contains(','))
				//	{
				//		Parameter = Parameter + " Department : " + lstDepartment.SelectedItem.Text;
				//	}
				//}
				if (CostCentre != "")
				{
					if (!CostCentre.ToLower().Contains(','))
					{
						Parameter = Parameter + " Cost Center : " + lstCostCentre.SelectedItem.Text;
					}
				}
				if (Status_id != "")
				{
					if (!Status_id.ToLower().Contains(','))
					{
						Parameter = Parameter + " Payment Status : " + lstStatus.SelectedItem.Text;
					}
				}
				if (txtPaymntReqFrmDt.Text != "")
				{
					if (!Payment_ID.ToLower().Contains(','))
					{
						Parameter2 = " Invoice From Date : " + txtPaymntReqFrmDt.Text.Replace('/', '-'); ;
					}
				}
				if (txtPaymntReqToDt.Text != "")
				{
					if (!PayementRDates.ToLower().Contains(','))
					{
						Parameter2 = Parameter2 + "  Invoice To Date : " + txtPaymntReqToDt.Text.Replace('/', '-'); ;
					}
				}
				//if (Payment_ID != "")
				//{
				//	if (!Payment_ID.ToLower().Contains(','))
				//	{
				//		Parameter2 = " Payment Request No : " + lstPaymentRequestNo.SelectedItem.Text;
				//	}
				//}
				//if (PayementRDates != "")
				//{
				//	if (!PayementRDates.ToLower().Contains(','))
				//	{
				//		Parameter2 = Parameter2 + "  Payment Request Date : " + lstpaymentRequestDate.SelectedItem.Text;
				//	}
				//}
				ReportViewer1.Visible = true;
				ReportViewer1.LocalReport.Refresh();
				ReportViewer1.LocalReport.DataSources.Clear();
				ReportParameter[] param = new ReportParameter[4];
				param[0] = new ReportParameter("ProjectName", ProjectName);
				param[1] = new ReportParameter("Parameter1", Parameter);
				param[2] = new ReportParameter("Parameter2", Parameter2);
				param[3] = new ReportParameter("ReportBy", Convert.ToString(""));
				
				ReportViewer1.ProcessingMode = ProcessingMode.Local;
				ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/Vscb_VendorWiseDeusReport.rdlc");
				ReportDataSource rds1 = new ReportDataSource();
				ReportDataSource rds = new ReportDataSource(ReportInvoice, RequisitionList.Tables[0]);
				//rds1 = new ReportDataSource(ReportPayment, RequisitionList.Tables[1]);
				ReportViewer1.LocalReport.DataSources.Clear();
				ReportViewer1.LocalReport.SetParameters(param);
				ReportViewer1.LocalReport.DataSources.Add(rds);
				//ReportViewer1.LocalReport.DataSources.Add(rds1);
				ReportViewer1.LocalReport.Refresh();


			}
			else
			{
				ReportViewer1.Visible = false;
				lblmessage.Text = "Record not available";
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
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectVendorWiseDuesPOWONo");
			lstPOWONo.DataSource = dtPOWONo;
			lstPOWONo.DataTextField = "PONumber";
			lstPOWONo.DataValueField = "POID";
			lstPOWONo.DataBind();
			lstPOWONo.Items.Insert(0, new ListItem("Select PO/WO No.", ""));
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
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectVendorWiseDuesPOWODate");
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
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectVendorWiseDuesVendorName");
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
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectVendorWiseDuesInvoiceNo");
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
	private void GetPaymentRequestNo()
	{
		try
		{
			dtPOWONo = new DataTable();
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectVendorWiseDuesPayRequestNo");
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
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectVendorWiseDuesPayRequestDate");
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
	//private void GetDepartment()
	//{
	//	try
	//	{
	//		dtPOWONo = new DataTable();
	//		dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectVendorWiseDuesDepartment");
	//		lstDepartment.DataSource = dtPOWONo;
	//		lstDepartment.DataTextField = "Department";
	//		lstDepartment.DataValueField = "Dept_ID";
	//		lstDepartment.DataBind();
	//		lstDepartment.Items.Insert(0, new ListItem("Select Deptment", ""));
	//	}
	//	catch (Exception)
	//	{

	//	}
	//}
	private void GetCostCentre()
	{
		try
		{
			dtPOWONo = new DataTable();
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectVendorWiseDuesCostCenter");
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
	private void GetPaymentStatus()
	{
		try
		{
			dtPOWONo = new DataTable();
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectVendorWiseDuesPaymentStatus");
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


	protected void txtPaymntReqFrmDt_TextChanged(object sender, EventArgs e)
	{
		string[] strdate;
		string strfromDate = "";
		string strToDate = "";

		if ((Convert.ToString(txtPaymntReqFrmDt.Text).Trim() != "") && (Convert.ToString(txtPaymntReqToDt.Text).Trim() != ""))
		{
			strdate = Convert.ToString(txtPaymntReqFrmDt.Text).Trim().Split('/');
			strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
			strdate = Convert.ToString(txtPaymntReqToDt.Text).Trim().Split('/');
			strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
			DateTime startDate = Convert.ToDateTime(strfromDate);
			DateTime endDate = Convert.ToDateTime(strToDate);
			if (startDate > endDate)
			{
				lblmessage.Text = "From Date should be less than To Date ";
				txtPaymntReqFrmDt.Text = "";
				return;
			}
			else
			{
				lblmessage.Text = "";
			}
		}
	}

	protected void txtPaymntReqToDt_TextChanged(object sender, EventArgs e)
	{
		string[] strdate;
		string strfromDate = "";
		string strToDate = "";

		if ((Convert.ToString(txtPaymntReqFrmDt.Text).Trim() != "") && (Convert.ToString(txtPaymntReqToDt.Text).Trim() != ""))
		{
			strdate = Convert.ToString(txtPaymntReqFrmDt.Text).Trim().Split('/');
			strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
			strdate = Convert.ToString(txtPaymntReqToDt.Text).Trim().Split('/');
			strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
			DateTime startDate = Convert.ToDateTime(strfromDate);
			DateTime endDate = Convert.ToDateTime(strToDate);
			if (startDate > endDate)
			{
				lblmessage.Text = "To Date should be greater than From Date ";
				txtPaymntReqToDt.Text = "";
				return;
			}
			else
			{
				lblmessage.Text = "";
			}
		}
	}

	private void GetDropDownList()
	{
		try
		{
			DataSet dsList = new DataSet();
			SqlParameter[] spars = new SqlParameter[2];

			spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
			spars[0].Value = "GetDropDownListForAuditTrailReport";

			spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(hdnEmpCode.Value).Trim();

			dsList = spm.getDatasetList(spars, "SP_VSCB_AuditTrai_Report");

			if (dsList != null)
			{
				if (dsList.Tables.Count > 0)
				{
					if (dsList.Tables[0].Rows.Count > 0)
					{
						lstPOWONo.DataSource = dsList.Tables[0];
						lstPOWONo.DataTextField = "PONumber";
						lstPOWONo.DataValueField = "POID";
						lstPOWONo.DataBind();
						lstPOWONo.Items.Insert(0, new ListItem("Select PO/WO No.", ""));
					}
					if (dsList.Tables[1].Rows.Count > 0)
					{
						lstVendorName.DataSource = dsList.Tables[1];
						lstVendorName.DataTextField = "Name";
						lstVendorName.DataValueField = "VendorID";
						lstVendorName.DataBind();
						lstVendorName.Items.Insert(0, new ListItem("Select Vendor Name", ""));
					}
					if (dsList.Tables[2].Rows.Count > 0)
					{
						lstPOWODate.DataSource = dsList.Tables[2];
						lstPOWODate.DataTextField = "PODate";
						lstPOWODate.DataValueField = "PODate";//POID
						lstPOWODate.DataBind();
						lstPOWODate.Items.Insert(0, new ListItem("Select PO/WO Date", ""));
					}
					if (dsList.Tables[3].Rows.Count > 0)
					{
						lstInvoiceNo.DataSource = dsList.Tables[3];
						lstInvoiceNo.DataTextField = "InvoiceNo";
						lstInvoiceNo.DataValueField = "InvoiceID";
						lstInvoiceNo.DataBind();
						lstInvoiceNo.Items.Insert(0, new ListItem("Select Invoice No", ""));
					}
					if (dsList.Tables[4].Rows.Count > 0)
					{
						lstPaymentRequestNo.DataSource = dsList.Tables[4];
						lstPaymentRequestNo.DataTextField = "PaymentReqNo";
						lstPaymentRequestNo.DataValueField = "Payment_ID";
						lstPaymentRequestNo.DataBind();
						lstPaymentRequestNo.Items.Insert(0, new ListItem("Select Payment Request No", ""));
					}
					if (dsList.Tables[5].Rows.Count > 0)
					{
						lstpaymentRequestDate.DataSource = dsList.Tables[5];
						lstpaymentRequestDate.DataTextField = "PaymentReqDate";
						lstpaymentRequestDate.DataValueField = "PaymentReqDate";//Payment_ID
						lstpaymentRequestDate.DataBind();
						lstpaymentRequestDate.Items.Insert(0, new ListItem("Select Payment Request Date", ""));
					}
					if (dsList.Tables[6].Rows.Count > 0)
					{
						lstStatus.DataSource = dsList.Tables[6];
						lstStatus.DataTextField = "PyamentStatus";
						lstStatus.DataValueField = "PaymentStatusID";
						lstStatus.DataBind();
						lstStatus.Items.Insert(0, new ListItem("Select Status", ""));
					}
					if (dsList.Tables[7].Rows.Count > 0)
					{
						lstCostCentre.DataSource = dsList.Tables[7];
						lstCostCentre.DataTextField = "CostCentre";
						lstCostCentre.DataValueField = "CostCentre";
						lstCostCentre.DataBind();
						lstCostCentre.Items.Insert(0, new ListItem("Select Cost Center", ""));
					}
				}
			}
		}
		catch (Exception ex)
		{
		}
	}
}