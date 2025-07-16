using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AppraisalProcess_Status : System.Web.UI.Page
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
            //Empcode_Appr
            if (Convert.ToString(Session["Empcode_Appr"]).Trim() == "" || Session["Empcode_Appr"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "procs/Appraisal_login.aspx");
            }

            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
			}
			if (Convert.ToString(Session["Empcode"]).Trim() == "")
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
			if (Page.User.Identity.IsAuthenticated == false)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/appraisalindex");
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
                    ////GetDepartment();
                    //GetCostCentreDepartment();
                    GetDropDown_Status_List();
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
		lstEmployeeName.SelectedIndex = -1;
		lstPOWODate.SelectedIndex = -1;
		lstLocationName.SelectedIndex = -1;
		lstpaymentRequestDate.SelectedIndex = -1;
		lstPaymentRequestNo.SelectedIndex = -1;
		lstDepartmentName.SelectedIndex = -1;
		lstStatus.SelectedIndex = -1;
		lstCostCentre.SelectedIndex = -1;
		//lstDepartment.SelectedIndex = -1;
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
		   

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[6];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "getAppraisal_Process_Status";

            spars[1] = new SqlParameter("@pyear", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString("2022-23").Trim();

            spars[2] = new SqlParameter("@DepaertmentName", SqlDbType.VarChar);
            if (Convert.ToString(lstDepartmentName.SelectedValue).Trim() != "")
                spars[2].Value = Convert.ToString(lstDepartmentName.SelectedValue).Trim();
            else
                spars[2].Value = DBNull.Value;

             spars[3] = new SqlParameter("@EmployeeName", SqlDbType.VarChar);
            if (Convert.ToString(lstEmployeeName.SelectedValue).Trim() != "")
                spars[3].Value = Convert.ToString(lstEmployeeName.SelectedValue).Trim();
            else
                spars[3].Value = DBNull.Value;

            spars[4] = new SqlParameter("@LocationCode", SqlDbType.VarChar);
            if (Convert.ToString(lstLocationName.SelectedValue).Trim() != "")
                spars[4].Value = Convert.ToString(lstLocationName.SelectedValue).Trim();
            else
                spars[4].Value = DBNull.Value;

            spars[5] = new SqlParameter("@AssessStatus", SqlDbType.VarChar);
            if (Convert.ToString(lstStatus.SelectedValue).Trim() != "")
                spars[5].Value = Convert.ToString(lstStatus.SelectedValue).Trim();
            else
                spars[5].Value = DBNull.Value;

            dsTrDetails = spm.ApprgetDatasetList(spars, "[SP_GETALL_Appraisal_DETAILS]");
            RecordCount.Text = ""; 
            gvMngTravelRqstList.DataSource = null;
			gvMngTravelRqstList.DataBind();
			if (dsTrDetails.Tables[0].Rows.Count > 0)
			{
				RecordCount.Text = "Record Count : " + Convert.ToString(dsTrDetails.Tables[0].Rows.Count);
				gvMngTravelRqstList.DataSource = dsTrDetails.Tables[0];
				gvMngTravelRqstList.DataBind();
				btnCancel.Visible = true;
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
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectCostCenter_View");
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
	//private void GetDepartment()
	//{
	//	try
	//	{
	//		dtPOWONo = new DataTable();
	//		dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "Select_Department_View");
	//		lstDepartment.DataSource = dtPOWONo;
	//		lstDepartment.DataTextField = "Department";
	//		lstDepartment.DataValueField = "Dept_ID";
	//		lstDepartment.DataBind();
	//		lstDepartment.Items.Insert(0, new ListItem("Select Project/Department", ""));
	//	}
	//	catch (Exception)
	//	{
	//	}
	//}
	private void GetPOWONo()
	{
		try
		{
			dtPOWONo = new DataTable();
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "Select_POWONo_View");
			lstLocationName.DataSource = dtPOWONo;
			lstLocationName.DataTextField = "PONumber";
			lstLocationName.DataValueField = "POID";
			lstLocationName.DataBind();
			lstLocationName.Items.Insert(0, new ListItem("Select PO/WO No.", ""));
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
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "Select_POWODate_View");
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
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectVendorName_View");
			lstDepartmentName.DataSource = dtPOWONo;
			lstDepartmentName.DataTextField = "Name";
			lstDepartmentName.DataValueField = "VendorID";
			lstDepartmentName.DataBind();
			lstDepartmentName.Items.Insert(0, new ListItem("Select Vendor Name", ""));
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
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectInvoiceNo_View");
			lstEmployeeName.DataSource = dtPOWONo;
			lstEmployeeName.DataTextField = "InvoiceNo";
			lstEmployeeName.DataValueField = "InvoiceID";
			lstEmployeeName.DataBind();
			lstEmployeeName.Items.Insert(0, new ListItem("Select Invoice No", ""));
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
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectPaymentRequestNo_View");
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
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectPaymentRequestDate_View");
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
			dtPOWONo = spm.GetVSCB_POWONoList(hdnEmpCode.Value, "SelectPaymentStatus_View");
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
						lstLocationName.DataSource = dsList.Tables[0];
						lstLocationName.DataTextField = "PONumber";
						lstLocationName.DataValueField = "POID";
						lstLocationName.DataBind();
						lstLocationName.Items.Insert(0, new ListItem("Select PO/WO No.", ""));
					}
					if (dsList.Tables[1].Rows.Count > 0)
					{
						lstDepartmentName.DataSource = dsList.Tables[1];
						lstDepartmentName.DataTextField = "Name";
						lstDepartmentName.DataValueField = "VendorID";
						lstDepartmentName.DataBind();
						lstDepartmentName.Items.Insert(0, new ListItem("Select Vendor Name", ""));
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
						lstEmployeeName.DataSource = dsList.Tables[3];
						lstEmployeeName.DataTextField = "InvoiceNo";
						lstEmployeeName.DataValueField = "InvoiceID";
						lstEmployeeName.DataBind();
						lstEmployeeName.Items.Insert(0, new ListItem("Select Invoice No", ""));
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
	public void ExportToExcel(object sender, EventArgs e)
	{
		Response.Clear();
		Response.Buffer = true;
		Response.ClearContent();
		Response.ClearHeaders();
		Response.Charset = "";
		string FileName = "Appraisal_Process_Status_" + DateTime.Now + ".xls";

		StringWriter strwritter = new StringWriter();
		HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);

		gvMngTravelRqstList.AllowPaging = false;
		GetPayment_Request_PeindingList();
	
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

    private void GetDropDown_Status_List()
    {

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "getAppraisal_Process_Status_Dropdownlist";

        spars[1] = new SqlParameter("@location", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString("1100");
         

        dsTrDetails = spm.ApprgetDatasetList(spars, "[SP_GETALL_Appraisal_DETAILS]");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            lstLocationName.DataSource = dsTrDetails.Tables[0];
            lstLocationName.DataTextField = "emp_location";
            lstLocationName.DataValueField = "emp_location";
            lstLocationName.DataBind();
            lstLocationName.Items.Insert(0, new ListItem("Select Location Name.", ""));
        }
        if (dsTrDetails.Tables[1].Rows.Count > 0)
        {
            lstDepartmentName.DataSource = dsTrDetails.Tables[1];
            lstDepartmentName.DataTextField = "Department";
            lstDepartmentName.DataValueField = "Department";
            lstDepartmentName.DataBind();
            lstDepartmentName.Items.Insert(0, new ListItem("Select Department Name.", ""));
        }
        if (dsTrDetails.Tables[2].Rows.Count > 0)
        {
            lstEmployeeName.DataSource = dsTrDetails.Tables[2];
            lstEmployeeName.DataTextField = "empname";
            lstEmployeeName.DataValueField = "Emp_code";
            lstEmployeeName.DataBind();
            lstEmployeeName.Items.Insert(0, new ListItem("Select Employee Name.", ""));
        }
        if (dsTrDetails.Tables[3].Rows.Count > 0)
        {
            lstStatus.DataSource = dsTrDetails.Tables[3];
            lstStatus.DataTextField = "Appr_Status_Name";
            lstStatus.DataValueField = "Appr_Status_Name";
            lstStatus.DataBind();
            lstStatus.Items.Insert(0, new ListItem("Select Appraisal Process Status.", ""));
        }
    }
}