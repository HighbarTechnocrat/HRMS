using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_VSCB_Inbox_ADV_Payment : System.Web.UI.Page
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
					if (Convert.ToString(hdnInboxType.Value).Trim() == "Pending")
					{
						//GetPOWONo();
						//GetVendorName();
						//GetPOWODate();
						//GetPaymentRequestNo();
						//GetPaymentStatus();
						//GetCostCentreDepartment();

                        get_fill_Dropdownlist();
						GetPayment_Request_PendingList();
					}
					else
					{
						GetPaymentStatus();
						Get_Approval_DropDownlist();
						GetPayment_Request_PendingList();
					}
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
			string InvoiceWithPO = "True";
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			hdnRecruitment_ReqID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
			hdnPOID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
			//InvoiceWithPO = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
			if (Convert.ToString(hdnInboxType.Value).Trim() == "Pending")
			{
				Response.Redirect("VSCB_Advance_Pay_Approval.aspx?Payment_ID=" + hdnRecruitment_ReqID.Value + "&POID=" + hdnPOID.Value + "&Type=Pending");
			}
			else
			{
				Response.Redirect("VSCB_Advance_Pay_Approval.aspx?Payment_ID=" + hdnRecruitment_ReqID.Value + "&POID=" + hdnPOID.Value +"&Type=View");
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
		GetPayment_Request_PendingList();
	}

	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		GetPayment_Request_PendingList();
	}

	protected void mobile_btnBack_Click(object sender, EventArgs e)
	{
		
		lstPOWODate.SelectedIndex = -1;
		lstPOWONo.SelectedIndex = -1;
		lstPaymentRequestNo.SelectedIndex = -1;
		lstVendorName.SelectedIndex = -1;
		lstStatus.SelectedIndex = -1;
		lstCostCentre.SelectedIndex = -1;
		lblmessage.Text = "";
		GetPayment_Request_PendingList();
	}
	#endregion

	#region PageMethod

	public void Get_Approval_DropDownlist()
	{
		
		try
		{
			SqlParameter[] spars = new SqlParameter[3];
			spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
			spars[0].Value = "Select_ADV_POWONo_APP";
			spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(hdnEmpCode.Value);
			DataSet getdtDetails = new DataSet();
			getdtDetails = spm.getDatasetList(spars, "sp_VSCB_CreatePOWO_Users");
			if (getdtDetails.Tables[0].Rows.Count > 0)
			{
				lstPOWONo.DataSource = getdtDetails.Tables[0];
				lstPOWONo.DataTextField = "PONumber";
				lstPOWONo.DataValueField = "POID";
				lstPOWONo.DataBind();
			}
			lstPOWONo.Items.Insert(0, new ListItem("Select PO/WO Number.", ""));
			if (getdtDetails.Tables[1].Rows.Count > 0)
			{
				lstVendorName.DataSource = getdtDetails.Tables[1];
				lstVendorName.DataTextField = "Name";
				lstVendorName.DataValueField = "VendorID";
				lstVendorName.DataBind();
			}
			lstVendorName.Items.Insert(0, new ListItem("Select Vendor Name", ""));
			if (getdtDetails.Tables[2].Rows.Count > 0)
			{
				lstPOWODate.DataSource = getdtDetails.Tables[2];
				lstPOWODate.DataTextField = "PODate";
				lstPOWODate.DataValueField = "PODate";//POID
				lstPOWODate.DataBind();
				
			}
			lstPOWODate.Items.Insert(0, new ListItem("Select PO/WO Date", ""));
			if (getdtDetails.Tables[3].Rows.Count > 0)
			{
				lstCostCentre.DataSource = getdtDetails.Tables[3];
				lstCostCentre.DataTextField = "CostCentre";
				lstCostCentre.DataValueField = "CostCentre";
				lstCostCentre.DataBind();
				
			}
			lstCostCentre.Items.Insert(0, new ListItem("Select Cost Center", ""));
			if (getdtDetails.Tables[4].Rows.Count > 0)
			{
				lstPaymentRequestNo.DataSource = getdtDetails.Tables[4];
				lstPaymentRequestNo.DataTextField = "PaymentReqNo";
				lstPaymentRequestNo.DataValueField = "Payment_ID";
				lstPaymentRequestNo.DataBind();
			}
			lstPaymentRequestNo.Items.Insert(0, new ListItem("Select Payment Request No", ""));
		}
		catch (Exception)
		{
			// return false;
		}
	}

	private void GetPayment_Request_PendingList()
	{
		try
		{
			DataTable RequisitionList = new DataTable();
			int Payment_ID = 0, Vendor_ID = 0, InvoiceID = 0, POID = 0, Status_id = 0, Dept_ID = 0;
			lblmessage.Text = ""; DateTime PaymentDate;
			string strtoDate = "", POWODate = "", PayementRDates = "", PayementAmt = "", CostCentor = "", Qtype = "" ;
			string[] strdate;
			POID = Convert.ToString(lstPOWONo.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPOWONo.SelectedValue) : 0;
			Vendor_ID = Convert.ToString(lstVendorName.SelectedValue).Trim() != "" ? Convert.ToInt32(lstVendorName.SelectedValue) : 0;
			Payment_ID = Convert.ToString(lstPaymentRequestNo.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPaymentRequestNo.SelectedValue) : 0;
			Status_id = Convert.ToString(lstStatus.SelectedValue).Trim() != "" ? Convert.ToInt32(lstStatus.SelectedValue) : 0;
			POWODate = Convert.ToString(lstPOWODate.SelectedValue).Trim() != "" ? Convert.ToString(lstPOWODate.SelectedValue) : "";
			CostCentor = Convert.ToString(lstCostCentre.SelectedValue).Trim() != "" ? Convert.ToString(lstCostCentre.SelectedValue) : "";

			if (POWODate != "")
			{
				strdate = Convert.ToString(POWODate).Trim().Split('-');
				strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
				PaymentDate = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
				POWODate = PaymentDate.ToString("yyyy-MM-dd");
			}
			if (Convert.ToString(hdnInboxType.Value).Trim() == "Pending")
			{
				Qtype = "SelectPending_ADV_PaymentList";
			}
			else
			{
				Qtype = "Select_APP_ADV_PaymentList";
			}
			RequisitionList = spm.GetVSCB_Payment_Request_PendingList(Session["Empcode"].ToString(), Qtype, POID, Vendor_ID, InvoiceID, Payment_ID, Status_id, POWODate, PayementRDates, PayementAmt, Dept_ID, CostCentor);
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
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "Select_ADV_CostCenter_Pending");
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
	
 

    public void get_fill_Dropdownlist()
    {

        try
		{
			SqlParameter[] spars = new SqlParameter[3];

			spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
			spars[0].Value = "fill_Advpayment_dropdowns_Pending";

			spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(hdnEmpCode.Value);

			DataSet getdtDetails = new DataSet();
			getdtDetails = spm.getDatasetList(spars, "sp_VSCB_CreatePOWO_Users");
			if (getdtDetails.Tables[0].Rows.Count > 0)
			{
				lstPOWONo.DataSource = getdtDetails.Tables[0];
				lstPOWONo.DataTextField = "PONumber";
				lstPOWONo.DataValueField = "POID";
				lstPOWONo.DataBind();
			}
			lstPOWONo.Items.Insert(0, new ListItem("Select PO/WO Number.", ""));
			if (getdtDetails.Tables[1].Rows.Count > 0)
			{
				lstVendorName.DataSource = getdtDetails.Tables[1];
				lstVendorName.DataTextField = "Name";
				lstVendorName.DataValueField = "VendorID";
				lstVendorName.DataBind();
			}
			lstVendorName.Items.Insert(0, new ListItem("Select Vendor Name", ""));
			if (getdtDetails.Tables[2].Rows.Count > 0)
			{
				lstPOWODate.DataSource = getdtDetails.Tables[2];
				lstPOWODate.DataTextField = "PODate";
				lstPOWODate.DataValueField = "PODate";//POID
				lstPOWODate.DataBind();
				
			}
			lstPOWODate.Items.Insert(0, new ListItem("Select PO/WO Date", ""));
			if (getdtDetails.Tables[3].Rows.Count > 0)
			{
				lstCostCentre.DataSource = getdtDetails.Tables[3];
				lstCostCentre.DataTextField = "CostCentre";
				lstCostCentre.DataValueField = "CostCentre";
				lstCostCentre.DataBind();
				
			}
			lstCostCentre.Items.Insert(0, new ListItem("Select Cost Center", ""));
			if (getdtDetails.Tables[4].Rows.Count > 0)
			{
				lstPaymentRequestNo.DataSource = getdtDetails.Tables[4];
				lstPaymentRequestNo.DataTextField = "PaymentReqNo";
				lstPaymentRequestNo.DataValueField = "Payment_ID";
				lstPaymentRequestNo.DataBind();
			}
			lstPaymentRequestNo.Items.Insert(0, new ListItem("Select Payment Request No", ""));
		}
		catch (Exception)
		{
			// return false;
		}


    }


     
    private void GetPOWODate()
	{
		try
		{
			dtPOWONo = new DataTable();
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "Select_ADV_POWODate_Pending");
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
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "Select_ADV_Vendor_Pending");
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
	
	private void GetPaymentRequestNo()
	{
		try
		{
			dtPOWONo = new DataTable();
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "Select_ADV_PayNo_Pending");
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