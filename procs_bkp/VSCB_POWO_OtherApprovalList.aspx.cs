using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_VSCB_POWO_OtherApprovalList : System.Web.UI.Page
{

	#region Creative_Default_methods
	SqlConnection source;
	public SqlDataAdapter sqladp;
	public static string dob1;
	public string userid;
	public static string pimg = "";
	public static string cimg = "";
	public string loc = "", dept = "", subdept = "", desg = "";
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
		Response.Redirect(ReturnUrl("sitepathmain") + "procs/SampleForm");
	}

	#endregion
	public DataTable dtPOWONo;
	#region Page_Events

	private void Page_Load(object sender, System.EventArgs e)
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


				Page.SmartNavigation = true;
				strempcode = Session["Empcode"].ToString();
				if (!Page.IsPostBack)
				{
					editform.Visible = true;
					hdnEmpCode.Value = Session["Empcode"].ToString();
					getMngPOWOReqstList();
					getCostCenterList();
					 
					this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}

	protected void lnkLeaveDetails_Click(object sender, EventArgs e)
	{

		LinkButton btn = (LinkButton)sender;
		GridViewRow row = (GridViewRow)btn.NamingContainer;
		hdnTallycode.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
		hdnCostCenter.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();

		if (Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[3]).Trim() == "0")
		{
			if (Convert.ToString(lstPOType.SelectedValue).Trim() == "")
			{
				lblmessage.Text = "Please Select PO/ WO Type";
				return;
			}
		}

		txtCostCenter.Text = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
		txtProject.Text = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();

		if (Convert.ToString(lstPOType.SelectedValue).Trim() != "")
		{
			hdnPOTypeID.Value = Convert.ToString(lstPOType.SelectedValue).Trim();
			txtPOType.Text = Convert.ToString(lstPOType.SelectedItem.Text).Trim();
		}

		if (Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[3]).Trim() != "0")
		{
			hdnPOTypeID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[3]).Trim();
		}

		getApproversList();
		divApprovers.Visible = true;
		gvMngTravelRqstList.Visible = false;
		RecordCount.Visible = false;
		divSearchCostCenter.Visible = false;
		divSeachButtons.Visible = false;

		chkApprover1.Checked = false;
		chkApprover2.Checked = false;
		chkApprover3.Checked = false;
		chkApprover4.Checked = false;
		getSelected_CostCenter_Approver();

	}

	protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		gvMngTravelRqstList.PageIndex = e.NewPageIndex;
		this.getMngPOWOReqstList();
	}
	#endregion

	#region Page Methods
	private void getSelected_CostCenter_Approver()
	{
		try
		{
			DataSet dsProjectsVendors = new DataSet();
			SqlParameter[] spars = new SqlParameter[3];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "get_selected_CostCenter_Approvers_PRM";

			spars[1] = new SqlParameter("@costcenter", SqlDbType.VarChar);
			if (Convert.ToString(hdnTallycode.Value).Trim() != "")
				spars[1].Value = Convert.ToString(hdnTallycode.Value).Trim();
			else
				spars[1].Value = DBNull.Value;

			spars[2] = new SqlParameter("@POTypeID", SqlDbType.VarChar);
			if (Convert.ToString(hdnPOTypeID.Value).Trim() != "")
				spars[2].Value = Convert.ToString(hdnPOTypeID.Value).Trim();
			else
				spars[2].Value = DBNull.Value;
			dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
			chkApprover1.Checked = false;
			chkApprover2.Checked = false;
			chkApprover3.Checked = false;
			chkApprover4.Checked = false;
			hdnFisrtAppr_Code.Value = "";
			hdnSecondAppr_Code.Value = "";
			hdnThirdAppr_Code.Value = "";
			hdnFourthAppr_Code.Value = "";

			if (dsProjectsVendors.Tables[0].Rows.Count > 0)
			{
				if (dsProjectsVendors.Tables[0].Rows.Count == 4)
				{
					lstAppr1.SelectedValue = Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["Emp_Code"]).Trim();
					lstAppr2.SelectedValue = Convert.ToString(dsProjectsVendors.Tables[0].Rows[1]["Emp_Code"]).Trim();
					lstAppr3.SelectedValue = Convert.ToString(dsProjectsVendors.Tables[0].Rows[2]["Emp_Code"]).Trim();
					lstAppr4.SelectedValue = Convert.ToString(dsProjectsVendors.Tables[0].Rows[3]["Emp_Code"]).Trim();

					hdnFisrtAppr_Code.Value = Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["Emp_Code"]).Trim();
					hdnSecondAppr_Code.Value = Convert.ToString(dsProjectsVendors.Tables[0].Rows[1]["Emp_Code"]).Trim();
					hdnThirdAppr_Code.Value = Convert.ToString(dsProjectsVendors.Tables[0].Rows[2]["Emp_Code"]).Trim();
					hdnFourthAppr_Code.Value = Convert.ToString(dsProjectsVendors.Tables[0].Rows[3]["Emp_Code"]).Trim();


					if (Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["IS_SELECTED"]).Trim() == "Y")
						chkApprover1.Checked = true;

					if (Convert.ToString(dsProjectsVendors.Tables[0].Rows[1]["IS_SELECTED"]).Trim() == "Y")
						chkApprover2.Checked = true;

					if (Convert.ToString(dsProjectsVendors.Tables[0].Rows[2]["IS_SELECTED"]).Trim() == "Y")
						chkApprover3.Checked = true;

					if (Convert.ToString(dsProjectsVendors.Tables[0].Rows[3]["IS_SELECTED"]).Trim() == "Y")
						chkApprover4.Checked = true;


				}
				if (dsProjectsVendors.Tables[0].Rows.Count == 3)
				{
					lstAppr1.SelectedValue = Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["Emp_Code"]).Trim();
					lstAppr2.SelectedValue = Convert.ToString(dsProjectsVendors.Tables[0].Rows[1]["Emp_Code"]).Trim();
					lstAppr3.SelectedValue = Convert.ToString(dsProjectsVendors.Tables[0].Rows[2]["Emp_Code"]).Trim();

					hdnFisrtAppr_Code.Value = Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["Emp_Code"]).Trim();
					hdnSecondAppr_Code.Value = Convert.ToString(dsProjectsVendors.Tables[0].Rows[1]["Emp_Code"]).Trim();
					hdnThirdAppr_Code.Value = Convert.ToString(dsProjectsVendors.Tables[0].Rows[2]["Emp_Code"]).Trim();

					if (Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["IS_SELECTED"]).Trim() == "Y")
						chkApprover1.Checked = true;

					if (Convert.ToString(dsProjectsVendors.Tables[0].Rows[1]["IS_SELECTED"]).Trim() == "Y")
						chkApprover2.Checked = true;

					if (Convert.ToString(dsProjectsVendors.Tables[0].Rows[2]["IS_SELECTED"]).Trim() == "Y")
						chkApprover3.Checked = true;


				}
				if (dsProjectsVendors.Tables[0].Rows.Count == 2)
				{
					lstAppr1.SelectedValue = Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["Emp_Code"]).Trim();
					lstAppr2.SelectedValue = Convert.ToString(dsProjectsVendors.Tables[0].Rows[1]["Emp_Code"]).Trim();

					hdnFisrtAppr_Code.Value = Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["Emp_Code"]).Trim();
					hdnSecondAppr_Code.Value = Convert.ToString(dsProjectsVendors.Tables[0].Rows[1]["Emp_Code"]).Trim();

					if (Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["IS_SELECTED"]).Trim() == "Y")
						chkApprover1.Checked = true;

					if (Convert.ToString(dsProjectsVendors.Tables[0].Rows[1]["IS_SELECTED"]).Trim() == "Y")
						chkApprover2.Checked = true;

				}
				if (dsProjectsVendors.Tables[0].Rows.Count == 1)
				{
					lstAppr1.SelectedValue = Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["Emp_Code"]).Trim();
					hdnFisrtAppr_Code.Value = Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["Emp_Code"]).Trim();

					if (Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["IS_SELECTED"]).Trim() == "Y")
						chkApprover1.Checked = true;

				}
			}

			if (dsProjectsVendors.Tables[1].Rows.Count > 0)
			{
				txtPOType.Text = Convert.ToString(dsProjectsVendors.Tables[1].Rows[0]["POType"]).Trim();
			}
		}
		catch (Exception)
		{

		}
	}

	private void getMngPOWOReqstList()
	{
		try
		{
			DataSet dsmyInvoice = new DataSet();
			SqlParameter[] spars = new SqlParameter[4];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "get_CostCenter_POWO_Approvers_PRM_List";

			spars[1] = new SqlParameter("@Project_Dept_Name", SqlDbType.VarChar);
			if (Convert.ToString(lstCostCenter.SelectedValue).Trim() != "")
				spars[1].Value = Convert.ToString(lstCostCenter.SelectedValue).Trim();
			else
				spars[1].Value = DBNull.Value;

			spars[2] = new SqlParameter("@POTypeID", SqlDbType.VarChar);
			if (Convert.ToString(lstPOType.SelectedValue).Trim() != "")
				spars[2].Value = Convert.ToString(lstPOType.SelectedValue).Trim();
			else
				spars[2].Value = DBNull.Value;

            spars[3] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[3].Value = strempcode;

            dsmyInvoice = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

			gvMngTravelRqstList.DataSource = null;
			gvMngTravelRqstList.DataBind();
			RecordCount.Text = "";
			lblmessage.Text = "";
			if (dsmyInvoice.Tables[0].Rows.Count > 0)
			{
				RecordCount.Text = "Record Count : " + Convert.ToString(dsmyInvoice.Tables[0].Rows.Count);
				gvMngTravelRqstList.DataSource = dsmyInvoice.Tables[0];
				gvMngTravelRqstList.DataBind();
			}
			else
			{
				lblmessage.Text = "Record's not found.!";
				lblmessage.Visible = true;
			}
		}
		catch (Exception ex)
		{

		}
	}

	private string getCostCenter_Approver(string sTallyCode, string sApprID, string sAppr_empCode)
	{
		DataSet dsProjectsVendors = new DataSet();
		string sInsertUpdateFlg = "Insert_POWO_Approvers_PRM";
		try
		{
			SqlParameter[] spars = new SqlParameter[5];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "get_CostCenter_POWO_Approvers_PRM";

			spars[1] = new SqlParameter("@costcenter", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(sTallyCode);

			spars[2] = new SqlParameter("@fileType", SqlDbType.VarChar);
			spars[2].Value = Convert.ToString(sApprID);

			spars[3] = new SqlParameter("@empcode", SqlDbType.VarChar);
			spars[3].Value = Convert.ToString(sAppr_empCode);

			spars[4] = new SqlParameter("@POTypeID", SqlDbType.VarChar);
			spars[4].Value = Convert.ToString(hdnPOTypeID.Value);

			dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
			if (dsProjectsVendors != null)
			{
				if (dsProjectsVendors.Tables.Count > 0)
				{
					if (dsProjectsVendors.Tables[0].Rows.Count > 0)
					{
						sInsertUpdateFlg = "Update_POWO_Approvers_PRM";
						if (dsProjectsVendors.Tables[0].Rows.Count == 1)
						{
							hdn_OldEmp_Approver_Code.Value = Convert.ToString(dsProjectsVendors.Tables[0].Rows[0]["Emp_Code"]).Trim();
						}
					}
				}
			}


		}
		catch (Exception)
		{

		}
		return sInsertUpdateFlg;
	}

	private void getCostCenterList()
	{
		try
		{

			DataSet dsProjectsVendors = new DataSet();
			SqlParameter[] spars = new SqlParameter[2];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "get_CostcenterList_ForDH_HOD_dropdown_List";

			spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
			spars[1].Value = strempcode;


			dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            if (dsProjectsVendors.Tables[0].Rows.Count > 0)
            {
                lstPOType.DataSource = dsProjectsVendors.Tables[0];
                lstPOType.DataTextField = "POType";
                lstPOType.DataValueField = "POTypeID";
                lstPOType.DataBind();
            }
            lstPOType.Items.Insert(0, new ListItem("Select PO Type", ""));

            if (dsProjectsVendors.Tables[1].Rows.Count > 0)
			{
				lstCostCenter.DataSource = dsProjectsVendors.Tables[1];
				lstCostCenter.DataTextField = "Tallycode";
				lstCostCenter.DataValueField = "Tallycode";
				lstCostCenter.DataBind();
			}
			lstCostCenter.Items.Insert(0, new ListItem("Select CostCenter", ""));

            
        }
		catch (Exception)
		{

		}
	}

	 

	private void getApproversList()
	{
		try
		{

			DataSet dsProjectsVendors = new DataSet();
			SqlParameter[] spars = new SqlParameter[2];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "get_Approvers_List";

			spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
			spars[1].Value = strempcode;


			dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

			if (dsProjectsVendors.Tables[0].Rows.Count > 0)
			{
				lstAppr1.DataSource = dsProjectsVendors.Tables[0];
				lstAppr1.DataTextField = "Emp_Name";
				lstAppr1.DataValueField = "Emp_Code";
				lstAppr1.DataBind();

				lstAppr2.DataSource = dsProjectsVendors.Tables[0];
				lstAppr2.DataTextField = "Emp_Name";
				lstAppr2.DataValueField = "Emp_Code";
				lstAppr2.DataBind();

				lstAppr3.DataSource = dsProjectsVendors.Tables[0];
				lstAppr3.DataTextField = "Emp_Name";
				lstAppr3.DataValueField = "Emp_Code";
				lstAppr3.DataBind();

				lstAppr4.DataSource = dsProjectsVendors.Tables[0];
				lstAppr4.DataTextField = "Emp_Name";
				lstAppr4.DataValueField = "Emp_Code";
				lstAppr4.DataBind();



			}
			lstAppr1.Items.Insert(0, new ListItem("Select Select Approver", ""));
			lstAppr2.Items.Insert(0, new ListItem("Select Select Approver", ""));
			lstAppr3.Items.Insert(0, new ListItem("Select Select Approver", ""));
			lstAppr4.Items.Insert(0, new ListItem("Select Select Approver", ""));


		}
		catch (Exception)
		{

		}
	}

	private void insert_update_Invoice_Payment_Approvers(string sQueryType, string sTallyCode, string sApproverEmp_Id, string sApproverEmp_code,
	string isSelected, string sOldApprover_empcode)
	{
		DataSet dsProjectsVendors = new DataSet();
		try
		{
			SqlParameter[] spars = new SqlParameter[7];

			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = sQueryType;

			spars[1] = new SqlParameter("@Tallycode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(sTallyCode);

			spars[2] = new SqlParameter("@APPR_ID", SqlDbType.VarChar);
			spars[2].Value = Convert.ToString(sApproverEmp_Id);

			spars[3] = new SqlParameter("@APPR_Emp_Code", SqlDbType.VarChar);
			spars[3].Value = Convert.ToString(sApproverEmp_code);

			spars[4] = new SqlParameter("@IS_SELECTED", SqlDbType.VarChar);
			spars[4].Value = Convert.ToString(isSelected);

			spars[5] = new SqlParameter("@APPR_Emp_Code_old", SqlDbType.VarChar);
			if (Convert.ToString(sOldApprover_empcode).Trim() != "")
				spars[5].Value = Convert.ToString(sOldApprover_empcode);
			else
				spars[5].Value = DBNull.Value;

			spars[6] = new SqlParameter("@POTypeId", SqlDbType.VarChar);
			spars[6].Value = Convert.ToString(hdnPOTypeID.Value).Trim();

			dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_insert_update_Invocie_paymentApprovers");

		}
		catch (Exception)
		{

		}

	}

	#endregion


	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		chkApprover1.Checked = false;
		chkApprover2.Checked = false;
		chkApprover3.Checked = false;
		chkApprover4.Checked = false;

		hdnFisrtAppr_Code.Value = "";
		hdnSecondAppr_Code.Value = "";
		hdnThirdAppr_Code.Value = "";
		hdnFourthAppr_Code.Value = "";

		gvMngTravelRqstList.Visible = true;
		RecordCount.Visible = true;
		divApprovers.Visible = false;
		divSeachButtons.Visible = true;
		divSearchCostCenter.Visible = true;

		getCostCenterList();
		getMngPOWOReqstList();
	}

	protected void mobile_btnBack_Click(object sender, EventArgs e)
	{
		chkApprover1.Checked = false;
		chkApprover2.Checked = false;
		chkApprover3.Checked = false;
		chkApprover4.Checked = false;

		hdnFisrtAppr_Code.Value = "";
		hdnSecondAppr_Code.Value = "";
		hdnThirdAppr_Code.Value = "";
		hdnFourthAppr_Code.Value = "";

		gvMngTravelRqstList.Visible = true;
		RecordCount.Visible = true;
		divApprovers.Visible = false;
		divSeachButtons.Visible = true;
		divSearchCostCenter.Visible = true;

		
		getCostCenterList();
		getMngPOWOReqstList();

	}

	protected void lstCostCenter_SelectedIndexChanged(object sender, EventArgs e)
	{
		chkApprover1.Checked = false;
		chkApprover2.Checked = false;
		chkApprover3.Checked = false;
		chkApprover4.Checked = false;

		hdnFisrtAppr_Code.Value = "";
		hdnSecondAppr_Code.Value = "";
		hdnThirdAppr_Code.Value = "";
		hdnFourthAppr_Code.Value = "";
		gvMngTravelRqstList.Visible = true;
		RecordCount.Visible = true;
		divApprovers.Visible = false;
		getMngPOWOReqstList();
	}



	protected void btnback_mng_Click(object sender, EventArgs e)
	{
		chkApprover1.Checked = false;
		chkApprover2.Checked = false;
		chkApprover3.Checked = false;
		chkApprover4.Checked = false;

		hdnFisrtAppr_Code.Value = "";
		hdnSecondAppr_Code.Value = "";
		hdnThirdAppr_Code.Value = "";
		hdnFourthAppr_Code.Value = "";

		txtPOType.Text = "";
		hdnPOTypeID.Value = "";

		gvMngTravelRqstList.Visible = true;
		RecordCount.Visible = true;
		divSeachButtons.Visible = true;
		divSearchCostCenter.Visible = true;
		divApprovers.Visible = false;

		 
		getCostCenterList();
		getMngPOWOReqstList();
	}

	protected void trvl_btnSave_Click(object sender, EventArgs e)
	{
		try
		{
			string s1Appr = "";
			string s2Appr = "";
			string s3Appr = "";
			string s4Appr = "";
			string s1Appr_selected = "N";
			string s2Appr_selected = "N";
			string s3Appr_selected = "N";
			string s4Appr_selected = "N";
			string sInsertUpdateFlg = "Insert_POWO_Approvers_PRM";

			#region Add or Update Approver
			if (Convert.ToString(lstAppr1.SelectedValue).Trim() != "")
			{
				s1Appr = Convert.ToString(lstAppr1.SelectedValue).Trim();
				hdnFisrtAppr_ID.Value = "1";
			}
			else
			{
				hdnFisrtAppr_ID.Value = "";
			}
			if (Convert.ToString(lstAppr2.SelectedValue).Trim() != "")
			{
				s2Appr = Convert.ToString(lstAppr2.SelectedValue).Trim();
				hdnSecondAppr_ID.Value = "7";
			}
			else
			{
				hdnSecondAppr_ID.Value = "";
			}
			if (Convert.ToString(lstAppr3.SelectedValue).Trim() != "")
			{
				s3Appr = Convert.ToString(lstAppr3.SelectedValue).Trim();
				hdnThirdAppr_ID.Value = "8";
			}
			else
			{
				hdnThirdAppr_ID.Value = "";
			}
			if (Convert.ToString(lstAppr4.SelectedValue).Trim() != "")
			{
				s4Appr = Convert.ToString(lstAppr4.SelectedValue).Trim();
				hdnFourthAppr_ID.Value = "9";
			}
			else
			{
				hdnFourthAppr_ID.Value = "";
			}
			if (chkApprover1.Checked)
				s1Appr_selected = "Y";

			if (chkApprover2.Checked)
				s2Appr_selected = "Y";

			if (chkApprover3.Checked)
				s3Appr_selected = "Y";

			if (chkApprover4.Checked)
				s4Appr_selected = "Y";

			if (Convert.ToString(lstAppr1.SelectedValue).Trim() != "")
			{
				#region check Approver is Assign
				sInsertUpdateFlg = getCostCenter_Approver(Convert.ToString(hdnTallycode.Value).Trim(), hdnFisrtAppr_ID.Value, Convert.ToString(lstAppr1.SelectedValue).Trim());
				#endregion

				insert_update_Invoice_Payment_Approvers(sInsertUpdateFlg, Convert.ToString(hdnTallycode.Value).Trim(), hdnFisrtAppr_ID.Value, lstAppr1.SelectedValue, s1Appr_selected, hdn_OldEmp_Approver_Code.Value);
			}
			if (Convert.ToString(lstAppr2.SelectedValue).Trim() != "")
			{
				#region check Approver is Assign
				sInsertUpdateFlg = getCostCenter_Approver(Convert.ToString(hdnTallycode.Value).Trim(), hdnSecondAppr_ID.Value, Convert.ToString(lstAppr2.SelectedValue).Trim());
				#endregion
				insert_update_Invoice_Payment_Approvers(sInsertUpdateFlg, Convert.ToString(hdnTallycode.Value).Trim(), hdnSecondAppr_ID.Value, lstAppr2.SelectedValue, s2Appr_selected, hdn_OldEmp_Approver_Code.Value);
			}
			if (Convert.ToString(lstAppr3.SelectedValue).Trim() != "")
			{
				#region check Approver is Assign
				sInsertUpdateFlg = getCostCenter_Approver(Convert.ToString(hdnTallycode.Value).Trim(), hdnThirdAppr_ID.Value, Convert.ToString(lstAppr3.SelectedValue).Trim());
				#endregion
				insert_update_Invoice_Payment_Approvers(sInsertUpdateFlg, Convert.ToString(hdnTallycode.Value).Trim(), hdnThirdAppr_ID.Value, lstAppr3.SelectedValue, s3Appr_selected, hdn_OldEmp_Approver_Code.Value);
			}
			if (Convert.ToString(lstAppr4.SelectedValue).Trim() != "")
			{
				#region check Approver is Assign
				sInsertUpdateFlg = getCostCenter_Approver(Convert.ToString(hdnTallycode.Value).Trim(), hdnFourthAppr_ID.Value, Convert.ToString(lstAppr4.SelectedValue).Trim());
				#endregion
				insert_update_Invoice_Payment_Approvers(sInsertUpdateFlg, Convert.ToString(hdnTallycode.Value).Trim(), hdnFourthAppr_ID.Value, lstAppr4.SelectedValue, s4Appr_selected, hdn_OldEmp_Approver_Code.Value);
			}
			divSearchCostCenter.Visible = true;
			divSeachButtons.Visible = true;
			RecordCount.Visible = true;
			gvMngTravelRqstList.Visible = true;
			divApprovers.Visible = false;

			hdnPOTypeID.Value = "";
			txtPOType.Text = "";
			getMngPOWOReqstList();
			#endregion
		}
		catch (Exception ex)
		{

		}
	}

}