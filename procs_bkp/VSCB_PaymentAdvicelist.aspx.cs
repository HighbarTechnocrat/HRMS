using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_VSCB_PaymentAdvicelist : System.Web.UI.Page
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
					Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/vscb_index");
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
						GetCostCentreDepartment();
					    GetBatch_No();
					   //GetBatch_Request_List_Report();
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
		   GetBatch_Request_List_Report();
		}

		protected void mobile_btnBack_Click(object sender, EventArgs e)
		{

			lstPOWONo.SelectedIndex = -1;
			lstVendorName.SelectedIndex = -1;
			lstCostCentre.SelectedIndex = -1;
			lstBatchNo.SelectedIndex = -1;
			lblmessage.Text = "";
		    GetBatch_Request_List_Report();
		}
		#endregion
		#region PageMethod
		private void GetBatch_Request_List_Report()
		{
			try
			{
				DataTable RequisitionList = new DataTable();
				int Vendor_ID = 0, POID = 0, Dept_ID = 0, Batch_ID = 0;
				lblmessage.Text = "";
				string CostCentor = "", POWONo = "", VendorName = "", Status = "";
				POID = Convert.ToString(lstPOWONo.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPOWONo.SelectedValue) : 0;
				Vendor_ID = Convert.ToString(lstVendorName.SelectedValue).Trim() != "" ? Convert.ToInt32(lstVendorName.SelectedValue) : 0;
			   CostCentor = Convert.ToString(lstCostCentre.SelectedValue).Trim() != "" ? Convert.ToString(lstCostCentre.SelectedValue) : "";
			    Batch_ID = Convert.ToString(lstBatchNo.SelectedValue).Trim() != "" ? Convert.ToInt32(lstBatchNo.SelectedValue) : 0;
			    RequisitionList = spm.GetVSCB_Payment_Request_PendingList(Session["Empcode"].ToString(), "Select_Batch_APP_Report", POID, Vendor_ID, Batch_ID, 0, 0, "", "", "", Dept_ID, CostCentor);
				if (RequisitionList.Rows.Count > 0)
				{
					ReportViewer1.Visible = true;
					ReportViewer1.LocalReport.Refresh();
					ReportViewer1.LocalReport.DataSources.Clear();
					if (Convert.ToString(lstPOWONo.SelectedValue).Trim() != "")
					{
						POWONo = "PO/WO Number - " + Convert.ToString(lstPOWONo.SelectedItem.Text).Trim();
					}
					if (Convert.ToString(lstVendorName.SelectedValue).Trim() != "")
					{
						VendorName = "Vendor Name - " + Convert.ToString(lstVendorName.SelectedItem.Text).Trim();
					}
					if (Convert.ToString(lstCostCentre.SelectedValue).Trim() != "")
					{
						CostCentor = "Cost Centre - " + Convert.ToString(lstCostCentre.SelectedItem.Text).Trim();
					}
					if (Convert.ToString(lstBatchNo.SelectedValue).Trim() != "")
					{
						Status = "Batch No - " + Convert.ToString(lstBatchNo.SelectedItem.Text).Trim();
					}

					ReportParameter[] param = new ReportParameter[4];
					param[0] = new ReportParameter("ProjectName", POWONo);
					param[1] = new ReportParameter("Parameter1", VendorName);
					param[2] = new ReportParameter("Parameter2", CostCentor + "  " + Status);
					param[3] = new ReportParameter("ReportBy", Convert.ToString(""));

					ReportViewer1.ProcessingMode = ProcessingMode.Local;
					ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/Vscb_Payment_Advice_List.rdlc");
					ReportDataSource rds = new ReportDataSource("Payment_List", RequisitionList);
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

	private void GetCostCentreDepartment()
		{
			try
			{
				dtPOWONo = new DataTable();
				dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "Select_CostCenter_Batch_APP");
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
	private void GetBatch_No()
	{
		try
		{    
			dtPOWONo = new DataTable();
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "Select_Batch_No_APP");
			lstBatchNo.DataSource = dtPOWONo;
			lstBatchNo.DataTextField = "Batch_No";
			lstBatchNo.DataValueField = "Batch_ID";
			lstBatchNo.DataBind();
			lstBatchNo.Items.Insert(0, new ListItem("Select Batch No", ""));
		}
		catch (Exception)
		{
		}
	}
	private void GetPOWONo()
		{
			try
			{
				dtPOWONo = new DataTable();
				dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "Select_POWONo_Batch_APP");
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
	private void GetVendorName()
		{
			try
			{
				dtPOWONo = new DataTable();
				dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "Select_Vendor_Batch_APP");
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

		#endregion
}