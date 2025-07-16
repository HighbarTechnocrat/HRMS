using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using System.Linq;
public partial class Create_Task : System.Web.UI.Page
{

	#region CreativeMethods
	SqlConnection source;
	public SqlDataAdapter sqladp;
	public static string dob1;
	public string userid;
	public static string pimg = "";
	public static string cimg = "";
	public string loc = "", dept = "", subdept = "", desg = "";
	public int did = 0;
	protected void lnkhome_Click(object sender, EventArgs e)
	{
		Response.Redirect(ReturnUrl("sitepathmain") + "default");
	}

	public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
	#endregion

	SP_Methods spm = new SP_Methods();
	DataSet dspaymentVoucher_Apprs = new DataSet();
	String CEOInList = "N";
	double YearlymobileAmount = 0;
	#region PageEvents
	private void Page_Load(object sender, System.EventArgs e)
	{
		try
		{
			if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
			}

			//mobile_btnPrintPV.Visible = false;

			if (Page.User.Identity.IsAuthenticated == false)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
			}
			else
			{

				Page.SmartNavigation = true;
				if (!Page.IsPostBack)
				{
					// txt_SPOCComment.Attributes.Add("maxlength", "500");
					txt_Metting_Title.Attributes.Add("maxlength", "250");
					txt_TaskDescripation.Attributes.Add("maxlength", "500");
					txt_TaskRemark.Attributes.Add("maxlength", "500");
					txt_Metting_Dis.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					txt_Due_Date.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["TaskMonitoringPath"]).Trim());
					//TXT_AssginmentDate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
					editform.Visible = true;
					divbtn.Visible = false;
					divmsg.Visible = false;
					hdnempcode.Value = Convert.ToString(Session["Empcode"]);
					txt_Ref_Date.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
					txt_Created_Date.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
					txt_Created_By.Text = Convert.ToString(Session["emp_loginName"]);
					hdnTaskRefID.Value = "0";
					hdnAttendeeID.Value = "0";
					hdnOldTaskRefID.Value = "0";
					DeleteTempAttendee();
					BindData("All");
					this.ddl_Attendees.Attributes.Add("disabled", "");
					this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
					//hdnFamilyDetailID.Value = "0";
				}
				else
				{

				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}



	#endregion

	#region PageMethods
	private void BindData(string qtype)
	{
		try
		{
			var empCode = Convert.ToString(Session["Empcode"]);
			var getVal = Convert.ToDouble(hdnTaskRefID.Value);
			var getResult = spm.getTaskMonitoringDDL(empCode, getVal, "GetAllDDL_TEMP");
			if (getResult != null)
			{
				if (getResult.Tables.Count > 0)
				{
					if (qtype == "All")
					{
						var getMeetingType = getResult.Tables[0];
						BindMettingType(getMeetingType);
						var getTask_M_Reference = getResult.Tables[1];
						BindMettingRefId(getTask_M_Reference);
						var getAttendeeDDL = getResult.Tables[2];
						var getSupervisorDDL = getResult.Tables[6];
						BindAttendee(getAttendeeDDL);
						BindOrganizer(getAttendeeDDL);
						//DataView view = new DataView(getSupervisorDDL);
						//DataTable distinctValues = view.ToTable(true, "Emp_Code", "Emp_Name");
						BindSupervisor(getSupervisorDDL);
						BindExecutor(getSupervisorDDL);
						var getTaskMR_Details = getResult.Tables[3];
						if (getTaskMR_Details.Rows.Count > 0)
						{
							hdnTaskRefID.Value = Convert.ToString(getTaskMR_Details.Rows[0]["ID"]);
							txt_Ref_Date.Text = Convert.ToString(getTaskMR_Details.Rows[0]["Task_Reference_Date"]);
							txt_Metting_Title.Text = Convert.ToString(getTaskMR_Details.Rows[0]["Meeting_Discussion_Title"]);
							txt_Metting_Dis.Text = Convert.ToString(getTaskMR_Details.Rows[0]["Meeting_Discussion_Date"]);
							var Meeting_Type_Id = Convert.ToString(getTaskMR_Details.Rows[0]["Meeting_Type_Id"]);
							var mettingId = ddl_Meeting_Type.SelectedValue.ToString();
							ddl_Meeting_Type.Items.FindByValue(mettingId).Selected = false;
							ddl_Meeting_Type.Items.FindByValue(Meeting_Type_Id).Selected = true;

							var Old_Task_Ref_Id = Convert.ToString(getTaskMR_Details.Rows[0]["Old_Task_Ref_Id"]);
							if (Old_Task_Ref_Id != "")
							{
								var OldTaskRefId = ddl_Meeting_Id.SelectedValue.ToString();
								ddl_Meeting_Id.Items.FindByValue(OldTaskRefId).Selected = false;
								ddl_Meeting_Id.Items.FindByValue(Meeting_Type_Id).Selected = true;

								dg_OldTaskDetails.DataSource = null;
								dg_OldTaskDetails.DataBind();
								var getid = Convert.ToDouble(ddl_Meeting_Id.SelectedValue);
								if (getid != 0)
								{
									var getResult_Old = spm.getTempAttendee("GetRefTaskIds", "", getid);
									if (getResult_Old.Rows.Count > 0)
									{
										dg_OldTaskDetails.DataSource = getResult_Old;
										dg_OldTaskDetails.DataBind();
									}
									else
									{

									}
								}
								ddl_Meeting_Id.Enabled = false;
							}

						}
						else
						{
							//hdnTaskRefID.Value = "0";
						}
						var getTaskDetails = getResult.Tables[4];
						if (getTaskDetails.Rows.Count > 0)
						{
							BindGridTask(getTaskDetails);
						}
						var getGridAttendee = getResult.Tables[5];
						if (getGridAttendee.Rows.Count > 0)
						{
							BindGridAttendee(getGridAttendee);
						}
					}
					else if (qtype == "BindDDLAttendee")
					{
						var getAttendeeDDL = getResult.Tables[2];
						BindAttendee(getAttendeeDDL);
						var getSupervisorDDL = getResult.Tables[6];
						// BindAttendee(getAttendeeDDL);
						BindSupervisor(getSupervisorDDL);
						BindExecutor(getSupervisorDDL);
						var getOrganizerDDL = getResult.Tables[7];
						if (getOrganizerDDL.Rows.Count > 0)
						{
							BindOrganizer(getOrganizerDDL);
						}
					}

				}
			}

		}
		catch (Exception ex)
		{

			throw;
		}
	}
	private void BindMettingType(DataTable dataTable)
	{
		try
		{
			var tempMeetingId = ddl_Meeting_Type.SelectedValue.ToString();
			if (tempMeetingId == "" || tempMeetingId == "0")
			{
				ddl_Meeting_Type.DataSource = null;
				ddl_Meeting_Type.DataBind();
				if (dataTable.Rows.Count > 0)
				{
					ddl_Meeting_Type.DataSource = dataTable;
					ddl_Meeting_Type.DataTextField = "MeetingType";
					ddl_Meeting_Type.DataValueField = "Id";
					ddl_Meeting_Type.DataBind();
					ddl_Meeting_Type.Items.Insert(0, new ListItem("Select Meeting Type", "0"));
				}
			}
		}
		catch (Exception ex)
		{
			lblmessage.Text = ex.Message.ToString();
		}
	}
	private void BindMettingRefId(DataTable dataTable)
	{
		try
		{
			var tempMeetingId = ddl_Meeting_Id.SelectedValue.ToString();
			if (tempMeetingId == "" || tempMeetingId == "0")
			{
				ddl_Meeting_Id.DataSource = null;
				ddl_Meeting_Id.DataBind();
				if (dataTable.Rows.Count > 0)
				{
					ddl_Meeting_Id.DataSource = dataTable;
					ddl_Meeting_Id.DataTextField = "Task_Reference_ID";
					ddl_Meeting_Id.DataValueField = "ID";
					ddl_Meeting_Id.DataBind();
					ddl_Meeting_Id.Items.Insert(0, new ListItem("Select Meeting Reference", "0"));
				}
				else
				{
					ddl_Meeting_Id.Items.Insert(0, new ListItem("Select Meeting Reference", "0"));
				}
			}
		}
		catch (Exception ex)
		{
			lblmessage.Text = ex.Message.ToString();
		}
	}
	private void BindAttendee(DataTable dataTable)
	{
		try
		{
			ddl_Attendees.DataSource = null;
			ddl_Attendees.DataBind();
			if (dataTable.Rows.Count > 0)
			{
				ddl_Attendees.DataSource = dataTable;
				ddl_Attendees.DataTextField = "Emp_Name";
				ddl_Attendees.DataValueField = "Emp_Code";
				ddl_Attendees.DataBind();
				ddl_Attendees.Items.Insert(0, new ListItem("Select Attendee", "0"));
			}
		}
		catch (Exception ex)
		{
			lblmessage.Text = ex.Message.ToString();
		}
	}

	private void BindOrganizer(DataTable dataTable)
	{
		try
		{
			ddl_Organizer.DataSource = null;
			ddl_Organizer.DataBind();
			if (dataTable.Rows.Count > 0)
			{
				ddl_Organizer.DataSource = dataTable;
				ddl_Organizer.DataTextField = "Emp_Name";
				ddl_Organizer.DataValueField = "Emp_Code";
				ddl_Organizer.DataBind();
				ddl_Organizer.Items.Insert(0, new ListItem("Select Organizer", "0"));
			}
		}
		catch (Exception ex)
		{
			lblmessage.Text = ex.Message.ToString();
		}
	}
	private void BindGridAttendee(DataTable dataTable)
	{
		//try
		//{
		dg_ATT_Details.DataSource = null;
		dg_ATT_Details.DataBind();
		if (dataTable.Rows.Count > 0)
		{
			dg_ATT_Details.DataSource = dataTable;
			dg_ATT_Details.DataBind();

			var rows2 = from row in dataTable.AsEnumerable()
						where row.Field<Boolean>("IsOrganizer") == true
						select row;
			int count2 = rows2.Count<DataRow>();
			if (count2 > 0)
			{
				btn_ATT_Save.Visible = true;
				this.ddl_Attendees.Attributes.Remove("disabled");
				btn_Org_Save.Visible = false;
				ddl_Organizer.Enabled = false;
			}
			else
			{
				btn_ATT_Save.Visible = false;
				this.ddl_Attendees.Attributes.Add("disabled", "");
				btn_Org_Save.Visible = true;
				ddl_Organizer.Enabled = true;
			}
			foreach (DataRow item in dataTable.Rows)
			{
				var getName = item["Attendee_Name"].ToString();

				foreach (ListItem itm in ddl_Attendees.Items)
				{
					if (itm.Text == getName)
					{
						itm.Attributes.Add("disabled", "disabled");
					}
				}
				foreach (ListItem itm in ddl_Organizer.Items)
				{
					if (itm.Text == getName)
					{
						itm.Attributes.Add("disabled", "disabled");
					}
				}
			}
		}
		else
		{
			btn_ATT_Save.Visible = false;
			this.ddl_Attendees.Attributes.Add("disabled", "");
			btn_Org_Save.Visible = true;
			ddl_Organizer.Enabled = true;
		}

		//}
		//catch (Exception ex)
		//{
		//	lblmessage.Text = ex.Message.ToString();
		//}
	}
	private void DeleteTempAttendee()
	{
		//try
		//{
		var getEmpCode = Convert.ToString(Session["Empcode"]);
		spm.InsertUpdateTempAttendee(0, 0, "DELETETempAttendee", getEmpCode, getEmpCode, false);
		//}
		//catch (Exception ex)
		//{

		//}
	}
	private void ClearAttendee()
	{
		chk_Ref_Select.Checked = false;
		//btn_ATT_Save.Visible = true;
		btn_ATT_Update.Visible = false;
		btn_FD_Cancel.Visible = false;
		//btn_Org_Save.Visible = true;
		btn_Org_Update.Visible = false;
		BindData("BindDDLAttendee");
	}
	private void ClearTask()
	{
		//try
		//{
		BindData("All");
		lnk_Task_Update.Visible = false;
		lnk_Task_Cancel.Visible = false;
		lnk_Task_Create.Visible = true;
		chk_for_Info.Checked = false;
		txt_TaskDescripation.Text = "";
		txt_TaskRemark.Text = "";
		txt_Due_Date.Text = "";
		txt_Reminder_Day.Text = "1";
		txt_Reminder_Repe_Day.Text = "1";
		chk_Reminder_Due_Date.Checked = true;
		chk_Escalation_Due_Date.Checked = true;
		chk_Escalation_Repeate.Checked = false;

		ddl_Task_Supervisor.Enabled = true;
		ddl_Task_Executor.Enabled = true;
		txt_Due_Date.Enabled = true;
		// txt_TaskDescripation.Enabled = true;
		//txt_TaskRemark.Enabled = true;
		uploadfile.Enabled = true;
		chk_Reminder_Due_Date.Enabled = true;
		chk_Reminder_Due_Date.Checked = true;

		txt_Reminder_Day.Text = "1";
		txt_Reminder_Day.Enabled = true;

		chk_Escalation_Due_Date.Enabled = true;
		chk_Escalation_Due_Date.Checked = true;

		txt_Reminder_Repe_Day.Text = "1";
		txt_Reminder_Repe_Day.Enabled = true;
		//}
		//catch (Exception)
		//{

		//	throw;
		//}
	}
	private void BindGridTask(DataTable dataTable)
	{
		try
		{
			dv_TaskList.DataSource = null;
			dv_TaskList.DataBind();
			if (dataTable.Rows.Count > 0)
			{
				IsShowTaskList.Visible = true;
				IsShowTaskList1.Visible = true;
				IsShowTaskList12.Visible = true;
				IsShowTaskList2.Visible = true;
				IsShowTaskList3.Visible = true;
				IsShowTaskList4.Visible = true;
				ddl_Meeting_Id.Enabled = true;
				dv_TaskList.DataSource = dataTable;
				dv_TaskList.DataBind();
			}
			else
			{

			}
		}
		catch (Exception ex)
		{
			lblmessage.Text = ex.Message.ToString();
		}
	}
	private void BindSupervisor(DataTable dataTable)
	{
		try
		{
			ddl_Task_Supervisor.DataSource = null;
			ddl_Task_Supervisor.DataBind();
			if (dataTable.Rows.Count > 0)
			{
				ddl_Task_Supervisor.DataSource = dataTable;
				ddl_Task_Supervisor.DataTextField = "Emp_Name";
				ddl_Task_Supervisor.DataValueField = "Emp_Code";
				ddl_Task_Supervisor.DataBind();
				ddl_Task_Supervisor.Items.Insert(0, new ListItem("Select Supervisor", "0"));
			}
		}
		catch (Exception ex)
		{
			lblmessage.Text = ex.Message.ToString();
		}
	}
	private void BindExecutor(DataTable dataTable)
	{
		try
		{
			ddl_Task_Executor.DataSource = null;
			ddl_Task_Executor.DataBind();
			if (dataTable.Rows.Count > 0)
			{
				ddl_Task_Executor.DataSource = dataTable;
				ddl_Task_Executor.DataTextField = "Emp_Name";
				ddl_Task_Executor.DataValueField = "Emp_Code";
				ddl_Task_Executor.DataBind();
				ddl_Task_Executor.Items.Insert(0, new ListItem("Select Executor", "0"));
			}
		}
		catch (Exception ex)
		{
			lblmessage.Text = ex.Message.ToString();
		}
	}
	#endregion
	//Add New refarance
	protected void btn_Ref_Add_Click(object sender, EventArgs e)
	{
		try
		{
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}
			if (Convert.ToString(txt_Metting_Title.Text).Trim() == "")
			{
				Label2.Text = "Enter meeting title.";
				return;
			}
			if (Convert.ToString(txt_Metting_Dis.Text).Trim() == "")
			{
				Label2.Text = "Enter meeting date.";
				return;
			}
			if (Convert.ToString(ddl_Meeting_Type.SelectedValue).Trim() == "0" || Convert.ToString(ddl_Meeting_Type.SelectedValue).Trim() == "")
			{
				Label2.Text = "Select meeting type.";
				return;
			}
			var IsSave = false;
			//Click on Add reference
			foreach (GridViewRow row in dg_OldTaskDetails.Rows)
			{
				//Finding Dropdown control  
				CheckBox ddl1 = row.FindControl("chk_Ref_Select") as CheckBox;
				if (ddl1.Checked == true)
				{
					IsSave = true;
					var empCode = Convert.ToString(Session["Empcode"]);
					//Add refanace 
					var getOldTaskRef = Convert.ToDouble(0);
					var getMettingOldref = ddl_Meeting_Id.SelectedValue.ToString();

					if (getMettingOldref != "")
					{
						getOldTaskRef = Convert.ToDouble(getMettingOldref);
						hdnOldTaskRefID.Value = getOldTaskRef.ToString();
					}
					else
					{
						hdnOldTaskRefID.Value = getOldTaskRef.ToString();
					}
					var getRefAdd = Convert.ToDouble(hdnTaskRefID.Value);
					if (getRefAdd == 0)
					{
						var getMettingDiscussionTitle = Convert.ToString(txt_Metting_Title.Text).ToString();
						var getMettingDiscussionDate = "";
						var getTempMeetingDate = Convert.ToString(txt_Metting_Dis.Text).ToString();
						var splitmeetingDate = getTempMeetingDate.Split('/');
						getMettingDiscussionDate = splitmeetingDate[2] + "-" + splitmeetingDate[1] + "-" + splitmeetingDate[0];
						var getMettingDiscussionType = Convert.ToString(ddl_Meeting_Type.SelectedValue).ToString();

						var getStatus = spm.InsertUpdateTaskDetails(0, 0, "AddTaskReference_Temp", empCode, empCode, getMettingDiscussionTitle, getMettingDiscussionDate, Convert.ToInt32(getMettingDiscussionType), false, getOldTaskRef, "", "", "", "", "", "", false, 0, false, 0, false, 0);
						if (Convert.ToDouble(getStatus) != 0)
						{
							hdnTaskRefID.Value = getStatus.ToString();
							getRefAdd = Convert.ToDouble(getStatus);
						}
					}
					Label lblTask_id = row.FindControl("lblTask_id") as Label;
					var getlblTask_id = Convert.ToInt32(lblTask_id.Text.Trim());
					var getStatusTask = spm.InsertUpdateTaskDetails(getlblTask_id, getRefAdd, "AddTaskRef_Temp", empCode, empCode, "", "", 0, false, getOldTaskRef, "", "", "", "", "", "", false, 0, false, 0, false, 0);

				}
			}
			if (IsSave == true)
			{
				ClearTask();
			}
			else
			{
				Label2.Text = "Select any one task.";
				return;
			}
		}
		catch (Exception ex)
		{

		}
	}

	protected void btn_FD_Cancel_Click(object sender, EventArgs e)
	{
		try
		{
			// Click on cancel reference
		}
		catch (Exception ex)
		{

		}
	}

	protected void ddl_Meeting_Id_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			showOldRef.Visible = true;
			showOldRef.Visible = false;
			dg_OldTaskDetails.DataSource = null;
			dg_OldTaskDetails.DataBind();
			CheckOrganizer_Attendee();
			var getid = Convert.ToDouble(ddl_Meeting_Id.SelectedValue);
			if (getid != 0)
			{
				var getResult = spm.getTempAttendee("GetRefTaskIds", "", getid);
				if (getResult.Rows.Count > 0)
				{
					dg_OldTaskDetails.DataSource = getResult;
					dg_OldTaskDetails.DataBind();
				}
				else
				{
					showOldRef.Visible = false;
				}

			}
			else
			{

			}
		}
		catch (Exception ex)
		{

		}
	}
	//
	protected void ddl_Meeting_Type_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			if (ddl_Task_Supervisor.SelectedIndex > 0)
			{
				CheckOrganizer_Attendee();
			}
		}
		catch (Exception)
		{

			throw;
		}

	}

	private void CheckOrganizer_Attendee()
	{
		try
		{
			var getAtt = false;
			var getOrg = false;
			Label3.Text = "";
			if (dg_ATT_Details.Rows.Count > 0)
			{
				foreach (GridViewRow row in dg_ATT_Details.Rows)
				{
					if (row.RowType == DataControlRowType.DataRow)
					{
						CheckBox chkRow = (row.Cells[0].FindControl("chk_Ref_Select") as CheckBox);
						if (chkRow.Checked)
						{
							getOrg = true;
						}
					}
				}
				if (dg_ATT_Details.Rows.Count >= 2)
				{
					getAtt = true;
				}
			}
			if (getOrg == false)
			{
				ddl_Task_Supervisor.SelectedIndex = -1;
				ddl_Task_Executor.SelectedIndex = -1;
				Label3.Text = "Please add Organizer.";
				return;
			}
			if (getAtt == false)
			{
				ddl_Task_Supervisor.SelectedIndex = -1;
				ddl_Task_Executor.SelectedIndex = -1;
				Label3.Text = "Please add attendees.";
				return;
			}
		}
		catch (Exception)
		{

			throw;
		}
	}
	protected void txt_Metting_Dis_TextChanged(object sender, EventArgs e)
	{
		Label2.Text = "";
		try
		{
			var getDate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
			DateTime tempF = DateTime.ParseExact(txt_Metting_Dis.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
			DateTime tempT = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
			if (tempF > tempT)
			{
				Label2.Text = "Meeting/Dicussion Date cannot be future date";
				txt_Metting_Dis.Text = "";
				return;
			}
		}
		catch (Exception ex)
		{

		}
	}

	protected void ddl_Attendees_SelectedIndexChanged(object sender, EventArgs e)
	{

	}
	//Click on Add Attedee  
	protected void btn_ATT_Save_Click(object sender, EventArgs e)
	{
		//try
		//{
		string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
		if (confirmValue != "Yes")
		{
			return;
		}
		Label1.Text = "";
		var empCode = Convert.ToString(Session["Empcode"]);
		var getVal = Convert.ToInt32(hdnTaskRefID.Value);
		var IsSelect = false;
		var count = 0;
		var getOrganizer = chk_Ref_Select.Checked;
		foreach (ListItem item in ddl_Attendees.Items)
		{
			if (item.Selected)
			{
				if (item.Value != "" && item.Value != "0")
				{
					IsSelect = true;
					count++;
				}
			}
		}

		if (IsSelect == false)
		{
			Label1.Text = "Please select attendee";
			return;
		}
		if (count >= 2 && getOrganizer == true)
		{
			Label1.Text = "Please select only one organizer";
			return;
		}

		// var getAttendee = Convert.ToString(ddl_Attendees.SelectedValue).Trim();
		var getAttendee = "";

		//if (getOrganizer == true)
		//{
		//	var getOrginserStatus = spm.getTempAttendee("CheckIsOrginser", empCode, getVal);
		//	if (getOrginserStatus != null)
		//	{
		//		if (getOrginserStatus.Rows.Count > 0)
		//		{
		//			var getMsg = Convert.ToString(getOrginserStatus.Rows[0]["Message"]);
		//			if (getMsg == "Exists")
		//			{
		//				Label1.Text = "Please select one organizer ";
		//				return;
		//			}
		//		}
		//	}
		//}
		var qtype = "";
		if (getVal == 0)
		{
			qtype = "AddTempAttendee";
		}
		else
		{
			qtype = "AddTempAttendee";
		}
		var getStatus = false;
		foreach (ListItem item in ddl_Attendees.Items)
		{
			if (item.Selected)
			{
				if (item.Value != "" && item.Value != "0")
				{
					getAttendee = item.Value;
					getStatus = spm.InsertUpdateTempAttendee(0, getVal, qtype, getAttendee, empCode, getOrganizer);
				}
			}
		}
		//  var getAttendeeList = new DataTable();
		//var 
		if (getStatus == false)
		{
			Label1.Text = "Something went wrong";
			return;
		}
		else
		{
			ClearAttendee();
			if (getVal == 0)
			{
				var getAttendeeList = spm.getTempAttendee("GetTempAttendee", empCode, 0);
				BindGridAttendee(getAttendeeList);
			}
			else
			{
				var getAttendeeList = spm.getTempAttendee("GetTempAttendee", empCode, getVal);
				BindGridAttendee(getAttendeeList);
			}
		}

		//}
		//catch (Exception ex)
		//{

		//}
	}
	//Click Attendee Update
	protected void btn_ATT_Update_Click(object sender, EventArgs e)
	{
		try
		{
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}
			Label1.Text = "";
			var empCode = Convert.ToString(Session["Empcode"]);
			var getVal = Convert.ToInt32(hdnTaskRefID.Value);
			var getId = Convert.ToInt32(hdnAttendeeID.Value);
			var IsSelect = false;
			var count = 0;
			var getOrganizer = chk_Ref_Select.Checked;
			foreach (ListItem item in ddl_Attendees.Items)
			{
				if (item.Selected)
				{
					if (item.Value != "" && item.Value != "0")
					{
						IsSelect = true;
						count++;
					}
				}
			}

			if (IsSelect == false)
			{
				Label1.Text = "Please select attendee";
				return;
			}
			if (count >= 2 && getOrganizer == true)
			{
				Label1.Text = "Please select only one organizer";
				return;
			}
			//if (Convert.ToString(ddl_Attendees.SelectedValue).Trim() == "0")
			//{
			//    Label1.Text = "Please select relation";
			//    return;
			//}
			//var getAttendee = Convert.ToString(ddl_Attendees.SelectedValue).Trim();
			var getAttendee = "";

			//if (getOrganizer == true)
			//{
			//	var getOrginserStatus = spm.getTempAttendee("CheckIsOrginserUpdate", empCode, getId);
			//	if (getOrginserStatus != null)
			//	{
			//		if (getOrginserStatus.Rows.Count > 0)
			//		{
			//			var getMsg = Convert.ToString(getOrginserStatus.Rows[0]["Message"]);
			//			if (getMsg == "Exists")
			//			{
			//				Label1.Text = "Please select only one organizer ";
			//				return;
			//			}
			//		}
			//	}
			//}
			var qtype = "";
			if (getVal == 0)
			{
				qtype = "UpdateTempAttendee";
			}
			else
			{
				qtype = "UpdateTempAttendee";
			}
			var getStatus = false;
			foreach (ListItem item in ddl_Attendees.Items)
			{
				if (item.Selected)
				{
					if (item.Value != "" && item.Value != "0")
					{
						getAttendee = item.Value;
						getStatus = spm.InsertUpdateTempAttendee(getId, getVal, qtype, getAttendee, empCode, getOrganizer);
					}
				}
			}

			// var getStatus = spm.InsertUpdateTempAttendee(getId, getVal, qtype, getAttendee, empCode, getOrganizer);
			if (getStatus == false)
			{
				Label1.Text = "Something went wrong";
				return;
			}
			else
			{
				ClearAttendee();
				if (getVal == 0)
				{
					var getAttendeeList = spm.getTempAttendee("GetTempAttendee", empCode, 0);
					BindGridAttendee(getAttendeeList);
				}
				else
				{
					var getAttendeeList = spm.getTempAttendee("GetTempAttendee", empCode, getVal);
					BindGridAttendee(getAttendeeList);
				}
			}

		}
		catch (Exception ex)
		{

		}
	}
	//Click On cancel Attendee 
	protected void btn_ATT_Cancel_Click(object sender, EventArgs e)
	{
		try
		{

		}
		catch (Exception ex)
		{

		}
	}
	//Click on Edit attendee grid
	protected void lnk_Atend_Edit_Click(object sender, ImageClickEventArgs e)
	{
		try
		{
			lblmessage.Text = "";
			// var getSelectedValues = ddl_Attendees.SelectedValue.ToString();
			// ddl_Attendees.Items.FindByValue(getSelectedValues).Selected = false;
			foreach (ListItem itm in ddl_Attendees.Items)
			{
				if (itm.Selected)
				{
					itm.Selected = false;
				}
			}
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			var fId = Convert.ToString(dg_ATT_Details.DataKeys[row.RowIndex].Values[0]).Trim();
			if (fId != "0")
			{
				var getds = new DataTable();
				var getVal = Convert.ToInt32(hdnTaskRefID.Value);
				if (getVal == 0)
				{
					getds = spm.getTempAttendee("GetTempAttendeeById", "", Convert.ToDouble(fId));
				}
				else
				{
					getds = spm.getTempAttendee("GetTempAttendeeById", "", Convert.ToDouble(fId));
				}
				hdnAttendeeID.Value = fId;
				if (getds != null)
				{
					if (getds.Rows.Count > 0)
					{

						if (getds.Rows.Count > 0)
						{


							var getEmpId = getds.Rows[0]["Emp_Code"].ToString();
							chk_Ref_Select.Checked = Convert.ToBoolean(getds.Rows[0]["IsOrganizer"].ToString());
							if (Convert.ToBoolean(getds.Rows[0]["IsOrganizer"].ToString()) != true)
							{
								btn_ATT_Save.Visible = false;
								foreach (ListItem itm in ddl_Attendees.Items)
								{
									if (itm.Value == getEmpId)
									{
										itm.Selected = true;
									}
								}
								btn_ATT_Update.Visible = true;
								btn_FD_Cancel.Visible = true;
							}
							else
							{
								ddl_Organizer.SelectedValue = getEmpId;
								btn_Org_Save.Visible = false;
								btn_Org_Update.Visible = true;
							}



						}
						else
						{

						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			Label1.Text = ex.Message;
			return;
		}
	}
	//Click on Delete Attendee Grid
	protected void lnk_Atend_Delete_Click(object sender, ImageClickEventArgs e)
	{
		try
		{
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			var fId = Convert.ToString(dg_ATT_Details.DataKeys[row.RowIndex].Values[0]).Trim();
			if (fId != null)
			{
				var empCode = Convert.ToString(Session["Empcode"]);
				var id = int.Parse(fId);
				var result = false;
				var getVal = Convert.ToInt32(hdnTaskRefID.Value);
				if (getVal == 0)
				{
					result = spm.DeleteAttendeeDetails(Convert.ToDouble(fId), "DeleteTempAttendeeById");
				}
				else
				{
					result = spm.DeleteAttendeeDetails(Convert.ToDouble(fId), "DeleteTempAttendeeById");
				}
				if (result == true)
				{

				}
				else
				{
					Label1.Text = "Something went wrong , Please try again.";
					return;
				}

				ClearAttendee();
				if (getVal == 0)
				{
					var getAttendeeList = spm.getTempAttendee("GetTempAttendee", empCode, 0);
					BindGridAttendee(getAttendeeList);
				}
			}
		}
		catch (Exception ex)
		{

		}
	}

	protected void txt_Due_Date_TextChanged(object sender, EventArgs e)
	{
		Label3.Text = "";
		try
		{
			var getDate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
			DateTime tempF = DateTime.ParseExact(txt_Due_Date.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
			DateTime tempT = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
			if (tempF < tempT)
			{
				Label3.Text = "Task due date cannot be less than today date";
				txt_Due_Date.Text = "";
				return;
			}
		}
		catch (Exception ex)
		{

		}
		return;
	}
	//Click on Create Task
	protected void lnk_Task_Create_Click(object sender, EventArgs e)
	{
		//try
		//{
		string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
		if (confirmValue != "Yes")
		{
			return;
		}
		LinkButton btn = (LinkButton)sender;
		if (btn.Text == "Create")
		{
			if (Convert.ToString(txt_Metting_Title.Text).Trim() == "")
			{
				Label2.Text = "Please enter Meeting/Discussion Title.";
				return;
			}
			if (Convert.ToString(txt_Metting_Dis.Text).Trim() == "")
			{
				Label2.Text = "Please enter Meeting/Discussion Date.";
				return;
			}
			if (Convert.ToString(ddl_Meeting_Type.SelectedValue).Trim() == "0" || Convert.ToString(ddl_Meeting_Type.SelectedValue).Trim() == "")
			{
				Label2.Text = "Please enter Meeting/Discussion Type.";
				return;
			}
			var getAtt = false;
			var getOrg = false;
			if (dg_ATT_Details.Rows.Count > 0)
			{
				foreach (GridViewRow row in dg_ATT_Details.Rows)
				{
					if (row.RowType == DataControlRowType.DataRow)
					{
						CheckBox chkRow = (row.Cells[0].FindControl("chk_Ref_Select") as CheckBox);
						if (chkRow.Checked)
						{
							getOrg = true;
						}
					}
				}
				if (dg_ATT_Details.Rows.Count >= 2)
				{
					getAtt = true;
				}
			}
			if (getOrg == false)
			{
				Label1.Text = "Please add Organizer.";
				return;
			}
			if (getAtt == false)
			{
				Label1.Text = "Please add attendees.";
				return;
			}
			if (chk_for_Info.Checked == false)
			{
				if (Convert.ToString(ddl_Task_Supervisor.SelectedValue).Trim() == "0" || Convert.ToString(ddl_Task_Supervisor.SelectedValue).Trim() == "")
				{
					Label3.Text = "Please add atleast one Organizer.";
					return;
				}
				if (Convert.ToString(ddl_Task_Executor.SelectedValue).Trim() == "0" || Convert.ToString(ddl_Task_Executor.SelectedValue).Trim() == "")
				{
					Label3.Text = "Please select task executor.";
					return;
				}
			}
			if (Convert.ToString(txt_TaskDescripation.Text).Trim() == "")
			{
				Label3.Text = "Please enter task descripation.";
				return;
			}
			if (Convert.ToString(txt_TaskRemark.Text).Trim() == "")
			{
				Label3.Text = "Please enter task remark.";
				return;
			}
			if (chk_for_Info.Checked == false)
			{
				if (Convert.ToString(txt_Due_Date.Text).Trim() == "")
				{
					Label3.Text = "Please select task due date.";
					return;
				}
				if (Convert.ToBoolean(chk_Reminder_Due_Date.Checked) == true)
				{
					if (Convert.ToString(txt_Reminder_Day.Text).Trim() == "")
					{
						Label3.Text = "Please enter reminder before day.";
						return;
					}
				}
				if (Convert.ToBoolean(chk_Escalation_Due_Date.Checked) == true)
				{
					if (Convert.ToString(txt_Reminder_Repe_Day.Text).Trim() == "")
					{
						Label3.Text = "Please enter escalation mail frequency.";
						return;
					}
				}
			}
			//getting Values
			var filename = "";
			var getRefId = Convert.ToDouble(hdnTaskRefID.Value);
			var getOldTaskRef = Convert.ToDouble(hdnOldTaskRefID.Value);
			var getMettingDiscussionTitle = Convert.ToString(txt_Metting_Title.Text).ToString();
			var getMettingDiscussionDate = "";
			var getTempMeetingDate = Convert.ToString(txt_Metting_Dis.Text).ToString();
			var splitmeetingDate = getTempMeetingDate.Split('/');
			getMettingDiscussionDate = splitmeetingDate[2] + "-" + splitmeetingDate[1] + "-" + splitmeetingDate[0];
			var getMettingDiscussionType = Convert.ToString(ddl_Meeting_Type.SelectedValue).ToString();
			//var getMettingOldref = ddl_Meeting_Id.SelectedValue.ToString();
			//if (getMettingOldref != "")
			//{
			//    getOldTaskRef = Convert.ToDouble(getMettingOldref);
			//}
			var getTaskSupervisor = Convert.ToString(ddl_Task_Supervisor.SelectedValue).ToString();
			var getTaskExecutor = Convert.ToString(ddl_Task_Executor.SelectedValue).ToString();
			var getFor_Info = Convert.ToBoolean(chk_for_Info.Checked);
			var getTaskDescripation = Convert.ToString(txt_TaskDescripation.Text).ToString();
			var getTaskRemark = Convert.ToString(txt_TaskRemark.Text).ToString();
			var getDueDate = Convert.ToString(txt_Due_Date.Text).ToString();
			var FinalDueDate = "";
			var getReminder_Due_Date = false;
			var getReminderDay = 0;
			var getEscalationDay = 0;
			var getEscalation_Due_Date = false;
			var getEscalation_Repeate = false;
			if (chk_for_Info.Checked == false)
			{
				var splitDueDate = getDueDate.Split('/');
				FinalDueDate = splitDueDate[2] + "-" + splitDueDate[1] + "-" + splitDueDate[0];
				if (uploadfile.HasFile)
				{
					filename = uploadfile.FileName;
				}
				if (Convert.ToString(filename).Trim() != "")
				{
					//var getdate = Convert.ToDateTime(Txt_DateCreate.Text);
					DateTime loadedDate = DateTime.Now;
					var strfromDate = loadedDate.ToString("ddMMyyyyHHmmss");
					filename = uploadfile.FileName;
					var strfileName = "";
					strfileName = "TASK_" + getTaskExecutor + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
					filename = strfileName;
					uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["TaskMonitoringPath"]).Trim()), strfileName));
				}
				getReminder_Due_Date = Convert.ToBoolean(chk_Reminder_Due_Date.Checked);

				if (getReminder_Due_Date == true)
				{
					getReminderDay = Convert.ToInt32(txt_Reminder_Day.Text);
				}
				getEscalation_Due_Date = Convert.ToBoolean(chk_Escalation_Due_Date.Checked);
				if (getEscalation_Due_Date == true)
				{
					getEscalationDay = Convert.ToInt32(txt_Reminder_Repe_Day.Text);
				}
				getEscalation_Repeate = Convert.ToBoolean(chk_Escalation_Repeate.Checked);
			}
			var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();
			var getStatus = spm.InsertUpdateTaskDetails(0, getRefId, "INSERTTASKDETAILS_TEMP", getCreatedBy, getCreatedBy, getMettingDiscussionTitle, getMettingDiscussionDate, Convert.ToInt32(getMettingDiscussionType), getFor_Info, getOldTaskRef, getTaskSupervisor, getTaskExecutor, getTaskDescripation, getTaskRemark, FinalDueDate, filename, getReminder_Due_Date, getReminderDay, getEscalation_Due_Date, getEscalationDay, getEscalation_Repeate, 0);
			if (Convert.ToDouble(getStatus) != 0)
			{
				hdnTaskRefID.Value = getStatus.ToString();
				ClearTask();
			}
			else
			{
				Label3.Text = "Something went wrong. please try again";
				return;
			}
		}
		else if (btn.Text == "Update")
		{
			if (chk_for_Info.Checked == false)
			{
				if (Convert.ToString(ddl_Task_Supervisor.SelectedValue).Trim() == "0" || Convert.ToString(ddl_Task_Supervisor.SelectedValue).Trim() == "")
				{
					Label3.Text = "Please select task supervisor.";
					return;
				}
				if (Convert.ToString(ddl_Task_Executor.SelectedValue).Trim() == "0" || Convert.ToString(ddl_Task_Executor.SelectedValue).Trim() == "")
				{
					Label3.Text = "Please select task executor.";
					return;
				}
			}
			if (Convert.ToString(txt_TaskDescripation.Text).Trim() == "")
			{
				Label3.Text = "Please enter task descripation.";
				return;
			}
			if (Convert.ToString(txt_TaskRemark.Text).Trim() == "")
			{
				Label3.Text = "Please enter task remark.";
				return;
			}
			if (chk_for_Info.Checked == false)
			{
				if (Convert.ToString(txt_Due_Date.Text).Trim() == "")
				{
					Label3.Text = "Please select task due date.";
					return;
				}
				if (Convert.ToBoolean(chk_Reminder_Due_Date.Checked) == true)
				{
					if (Convert.ToString(txt_Reminder_Day.Text).Trim() == "")
					{
						Label3.Text = "Please enter reminder before day.";
						return;
					}
				}
				if (Convert.ToBoolean(chk_Escalation_Due_Date.Checked) == true)
				{
					if (Convert.ToString(txt_Reminder_Repe_Day.Text).Trim() == "")
					{
						Label3.Text = "Please enter escalation mail frequency.";
						return;
					}
				}
			}
			//getting Values
			var filename = "";
			var getRefId = Convert.ToDouble(hdnTaskRefID.Value);
			var getOldTaskRef = Convert.ToDouble(hdnOldTaskRefID.Value);
			var getMettingDiscussionTitle = Convert.ToString(txt_Metting_Title.Text).ToString();
			var getMettingDiscussionDate = "";
			var getTempMeetingDate = Convert.ToString(txt_Metting_Dis.Text).ToString();
			var splitmeetingDate = getTempMeetingDate.Split('/');
			getMettingDiscussionDate = splitmeetingDate[2] + "-" + splitmeetingDate[1] + "-" + splitmeetingDate[0];
			var getMettingDiscussionType = Convert.ToString(ddl_Meeting_Type.SelectedValue).ToString();
			//var getMettingOldref = ddl_Meeting_Id.SelectedValue.ToString();
			//if (getMettingOldref != "")
			//{
			//    getOldTaskRef = Convert.ToDouble(getMettingOldref);
			//}
			var getTaskSupervisor = Convert.ToString(ddl_Task_Supervisor.SelectedValue).ToString();
			var getTaskExecutor = Convert.ToString(ddl_Task_Executor.SelectedValue).ToString();
			var getFor_Info = Convert.ToBoolean(chk_for_Info.Checked);
			var getTaskDescripation = Convert.ToString(txt_TaskDescripation.Text).ToString();
			var getTaskRemark = Convert.ToString(txt_TaskRemark.Text).ToString();
			var getDueDate = Convert.ToString(txt_Due_Date.Text).ToString();
			var FinalDueDate = "";
			var getReminder_Due_Date = false;
			var getReminderDay = 0;
			var getEscalation_Repeate = false;
			var getEscalationDay = 0;
			var getEscalation_Due_Date = false;

			if (chk_for_Info.Checked == false)
			{
				var splitDueDate = getDueDate.Split('/');
				FinalDueDate = splitDueDate[2] + "-" + splitDueDate[1] + "-" + splitDueDate[0];
				if (uploadfile.HasFile)
				{
					filename = uploadfile.FileName;
				}
				if (Convert.ToString(filename).Trim() != "")
				{
					//var getdate = Convert.ToDateTime(Txt_DateCreate.Text);
					DateTime loadedDate = DateTime.Now;
					var strfromDate = loadedDate.ToString("ddMMyyyyHHmmss");
					filename = uploadfile.FileName;
					var strfileName = "";
					strfileName = "TASK_" + getTaskExecutor + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
					filename = strfileName;
					uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["TaskMonitoringPath"]).Trim()), strfileName));
				}
				getReminder_Due_Date = Convert.ToBoolean(chk_Reminder_Due_Date.Checked);
				if (getReminder_Due_Date == true)
				{
					getReminderDay = Convert.ToInt32(txt_Reminder_Day.Text);
				}
				getEscalation_Due_Date = Convert.ToBoolean(chk_Escalation_Due_Date.Checked);
				if (getEscalation_Due_Date == true)
				{
					getEscalationDay = Convert.ToInt32(txt_Reminder_Repe_Day.Text);
				}
				getEscalation_Repeate = Convert.ToBoolean(chk_Escalation_Repeate.Checked);
			}
			var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();
			var id = Convert.ToInt32(hdnTaskID.Value);
			var getStatus = spm.InsertUpdateTaskDetails(id, getRefId, "UPDATETASKDETAILS_TEMP", getCreatedBy, getCreatedBy, getMettingDiscussionTitle, getMettingDiscussionDate, Convert.ToInt32(getMettingDiscussionType), getFor_Info, getOldTaskRef, getTaskSupervisor, getTaskExecutor, getTaskDescripation, getTaskRemark, FinalDueDate, filename, getReminder_Due_Date, getReminderDay, getEscalation_Due_Date, getEscalationDay, getEscalation_Repeate, 0);
			if (Convert.ToDouble(getStatus) != 0)
			{
				hdnTaskRefID.Value = getStatus.ToString();
				ClearTask();
				gv_Documents.DataSource = null;
				gv_Documents.DataBind();
			}
			else
			{
				Label3.Text = "Something went wrong. please try again";
				return;
			}
		}
		else if (btn.Text == "Cancel")
		{
			ClearTask();
			gv_Documents.DataSource = null;
			gv_Documents.DataBind();
		}
		//}
		//catch (Exception ex)
		//{
		//    Label1.Text = "Something went wrong. please try again";
		//    return;
		//}
	}
	//Click on Edit Task on grid
	protected void lnk_TLI_Edit_Click(object sender, ImageClickEventArgs e)
	{
		try
		{
			var getSelectedValues = ddl_Task_Executor.SelectedValue.ToString();
			var getSelectedValues1 = ddl_Task_Supervisor.SelectedValue.ToString();

			ddl_Task_Executor.Items.FindByValue(getSelectedValues).Selected = false;
			ddl_Task_Supervisor.Items.FindByValue(getSelectedValues1).Selected = false;
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			var fId = Convert.ToString(dv_TaskList.DataKeys[row.RowIndex].Values[0]).Trim();
			if (fId != "0")
			{
				var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();
				var TaskRefID = Convert.ToInt32(hdnTaskRefID.Value);
				var getdsDetails = spm.getTaskDetailsById(getCreatedBy, Convert.ToDouble(TaskRefID), Convert.ToDouble(fId), "getTaskDetailsById_Temp");
				if (getdsDetails.Tables.Count > 0)
				{
					hdnTaskID.Value = fId;
					var getds = getdsDetails.Tables[0];
					var getForOnlyInfo = Convert.ToBoolean(getds.Rows[0]["For_Information_Only"]);
					if (getForOnlyInfo == false)
					{
						ddl_Task_Supervisor.Enabled = true;
						ddl_Task_Executor.Enabled = true;
						txt_Due_Date.Enabled = true;
						uploadfile.Enabled = true;
						chk_Reminder_Due_Date.Enabled = true;
						txt_Reminder_Day.Enabled = true;

						chk_Escalation_Due_Date.Enabled = true;
						chk_Escalation_Repeate.Enabled = true;
						txt_Reminder_Repe_Day.Enabled = true;

						var getSupervisor = Convert.ToString(getds.Rows[0]["Task_Supervisor"]);
						var getExecutor = Convert.ToString(getds.Rows[0]["Task_Executer"]);
						ddl_Task_Executor.Items.FindByValue(getExecutor).Selected = true;
						ddl_Task_Supervisor.Items.FindByValue(getSupervisor).Selected = true;
						chk_for_Info.Checked = Convert.ToBoolean(getds.Rows[0]["For_Information_Only"]);
						chk_Reminder_Due_Date.Checked = Convert.ToBoolean(getds.Rows[0]["Remainder_Before_Due_Date"]);
						chk_Escalation_Due_Date.Checked = Convert.ToBoolean(getds.Rows[0]["Escalation_Mail_After_Due_Date"]);
						chk_Escalation_Repeate.Checked = Convert.ToBoolean(getds.Rows[0]["Repeated_Escalation_Mail"]);

						if (Convert.ToString(getds.Rows[0]["Reminder_Before_Days"]) != "")
						{
							txt_Reminder_Day.Text = Convert.ToString(getds.Rows[0]["Reminder_Before_Days"]);
						}
						if (Convert.ToString(getds.Rows[0]["Escalation_Mail_Frequency_Days"]) != "")
						{
							txt_Reminder_Repe_Day.Text = Convert.ToString(getds.Rows[0]["Escalation_Mail_Frequency_Days"]);
						}
						txt_Due_Date.Text = Convert.ToString(getds.Rows[0]["DUEDATE"]);
						var getDocumentTable = getdsDetails.Tables[2];
						gv_Documents.DataSource = null;
						gv_Documents.DataBind();
						// var getDocumentTable = getdsTables.Tables[2];
						if (getDocumentTable.Rows.Count > 0)
						{
							gv_Documents.DataSource = getDocumentTable;
							gv_Documents.DataBind();

						}
					}
					else
					{
						//ClearTask();
						ddl_Task_Supervisor.Enabled = false;
						ddl_Task_Executor.Enabled = false;
						txt_Due_Date.Enabled = false;
						//  txt_TaskDescripation.Enabled = false;
						// txt_TaskRemark.Enabled = false;
						uploadfile.Enabled = false;

						chk_Reminder_Due_Date.Enabled = false;
						chk_Reminder_Due_Date.Checked = false;

						txt_Reminder_Day.Text = "";
						txt_Reminder_Day.Enabled = false;

						chk_Escalation_Due_Date.Enabled = false;
						chk_Escalation_Due_Date.Checked = false;

						chk_Escalation_Repeate.Enabled = false;

						txt_Reminder_Repe_Day.Text = "";
						txt_Reminder_Repe_Day.Enabled = false;
					}
					txt_TaskDescripation.Text = Convert.ToString(getds.Rows[0]["Task_Description"]);
					txt_TaskRemark.Text = Convert.ToString(getds.Rows[0]["Task_Remarks"]);

					lnk_Task_Update.Visible = true;
					lnk_Task_Cancel.Visible = true;
					lnk_Task_Create.Visible = false;
				}
			}
		}
		catch (Exception ex)
		{

		}
	}

	protected void lnk_Final_Submit_Click(object sender, EventArgs e)
	{
		//try
		//{
		if (Convert.ToString(txt_Metting_Title.Text).Trim() == "")
		{
			Label2.Text = "Please enter Meeting/Discussion Title.";
			return;
		}
		if (Convert.ToString(txt_Metting_Dis.Text).Trim() == "")
		{
			Label2.Text = "Please enter Meeting/Discussion Date.";
			return;
		}
		if (Convert.ToString(ddl_Meeting_Type.SelectedValue).Trim() == "0" || Convert.ToString(ddl_Meeting_Type.SelectedValue).Trim() == "")
		{
			Label2.Text = "Please enter Meeting/Discussion Type.";
			return;
		}
		var IsOrg = false;
		var getAtt = false;
		foreach (GridViewRow row in dg_ATT_Details.Rows)
		{
			//Finding Dropdown control  
			CheckBox ddl1 = row.FindControl("chk_Ref_Select") as CheckBox;
			if (ddl1.Checked == true)
			{
				IsOrg = true;
			}
			if (dg_ATT_Details.Rows.Count >= 2)
			{
				getAtt = true;
			}
		}
		if (IsOrg == false)
		{
			Label1.Text = "Please add atleast one Organizer.";
			return;
		}
		if (getAtt == false)
		{
			Label1.Text = "Please add attendees.";
			return;
		}

		var getTempMettingOldref = ddl_Meeting_Id.SelectedValue.ToString();
		if (getTempMettingOldref != "0")
		{
			var IsTaskRef = false;
			//Click on Add reference
			foreach (GridViewRow row in dg_OldTaskDetails.Rows)
			{
				//Finding Dropdown control  
				CheckBox ddl1 = row.FindControl("chk_Ref_Select") as CheckBox;
				if (ddl1.Checked == true)
				{
					IsTaskRef = true;
				}
			}
			if (IsTaskRef == false)
			{
				Label2.Text = "Please select atleast one task reference.";
				return;
			}
		}
		var getTaskCount = dv_TaskList.Rows.Count;
		if (getTaskCount < 1)
		{
			Label2.Text = "Please add atleast one task ";
			return;
		}
		var getRefId = Convert.ToDouble(hdnTaskRefID.Value);
		var getOldTaskRef = Convert.ToDouble(0);
		var getMettingDiscussionTitle = Convert.ToString(txt_Metting_Title.Text).ToString();
		var getMettingDiscussionDate = "";
		var getTempMeetingDate = Convert.ToString(txt_Metting_Dis.Text).ToString();
		var splitmeetingDate = getTempMeetingDate.Split('/');
		getMettingDiscussionDate = splitmeetingDate[2] + "-" + splitmeetingDate[1] + "-" + splitmeetingDate[0];
		var getMettingDiscussionType = Convert.ToString(ddl_Meeting_Type.SelectedValue).ToString();
		var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();
		var getStatus = spm.InsertUpdateTaskDetails(0, 0, "UpdateTaskRef", getCreatedBy, getCreatedBy, getMettingDiscussionTitle, getMettingDiscussionDate, Convert.ToInt32(getMettingDiscussionType), false, getOldTaskRef, "", "", "", "", "", "", false, 0, false, 0, false, 0);
		hdnTaskRefID.Value = getStatus.ToString();
		var getList = spm.getTaskMonitoringDDL(getCreatedBy, 0, "GetTempTaskList");
		if (getList != null)
		{
			if (getList.Tables.Count > 0)
			{
				var getTable = getList.Tables[0];
				if (getTable != null)
				{
					if (getTable.Rows.Count > 0)
					{
						foreach (DataRow item in getTable.Rows)
						{
							var getTaskRefId = Convert.ToDouble(getStatus);
							getRefId = getTaskRefId;
							var getTaskSupervisor = Convert.ToString(item["Task_Supervisor"]).ToString();
							var getTaskExecutor = Convert.ToString(item["Task_Executer"]).ToString();
							var getFor_Info = Convert.ToBoolean(item["For_Information_Only"]);
							var getTaskDescripation = Convert.ToString(item["Task_Description"]).ToString();
							var getTaskRemark = Convert.ToString(item["Task_Remarks"]).ToString();
							var getDueDate = "";
							var getReminder_Due_Date = false;
							var getReminderDay = 0;
							var getEscalation_Due_Date = false;
							var getEscalationDay = 0;
							var getEscalation_Repeate = false;
							if (getFor_Info == false)
							{
								getDueDate = Convert.ToString(item["DueDate"]).ToString();
								getReminder_Due_Date = Convert.ToBoolean(item["Remainder_Before_Due_Date"]);
								getReminderDay = Convert.ToInt32(item["Reminder_Before_Days"]);
								getEscalation_Due_Date = Convert.ToBoolean(item["Escalation_Mail_After_Due_Date"]);
								getEscalationDay = Convert.ToInt32(item["Escalation_Mail_Frequency_Days"]);
								getEscalation_Repeate = Convert.ToBoolean(item["Repeated_Escalation_Mail"]);
							}
							var id = Convert.ToInt32(item["Id"]);
							var tempTaskRefId = Convert.ToInt32(item["Task_Ref_id"]);
							var getStatus1 = spm.InsertUpdateTaskDetails(id, getTaskRefId, "AddNewTaskDetails", getCreatedBy, getCreatedBy, getMettingDiscussionTitle, getMettingDiscussionDate, Convert.ToInt32(getMettingDiscussionType), getFor_Info, getOldTaskRef, getTaskSupervisor, getTaskExecutor, getTaskDescripation, getTaskRemark, getDueDate, "", getReminder_Due_Date, getReminderDay, getEscalation_Due_Date, getEscalationDay, getEscalation_Repeate, tempTaskRefId);

						}
					}
				}
			}
		}
		// Sending Email
		var getTaskRef = Convert.ToDouble(hdnTaskRefID.Value);
		var getDs = spm.getTaskMonitoringReport("getTaskEmaildetails", "", getTaskRef, 0);
		if (getDs != null)
		{
			if (getDs.Tables.Count > 0)
			{
				var getTaskRefDetails = getDs.Tables[0];

				if (getTaskRefDetails.Rows.Count > 0)
				{
					var getTaskDetails = getDs.Tables[1];
					var getAllAttendee = getDs.Tables[2];
					var getAllExecuter = getDs.Tables[3];
					var appendAttendee = "";
					foreach (DataRow item in getAllAttendee.Rows)
					{
						if (appendAttendee == "")
						{
							appendAttendee = Convert.ToString(item["Emp_Emailaddress"]);
						}
						else
						{
							appendAttendee = appendAttendee + ";" + Convert.ToString(item["Emp_Emailaddress"]);
						}
					}
					var Task_Reference_ID = Convert.ToString(getTaskRefDetails.Rows[0]["Task_Reference_ID"]);
					var Meeting_Discussion_Title = Convert.ToString(getTaskRefDetails.Rows[0]["Meeting_Discussion_Title"]);
					var Meeting_Discussion_Date = Convert.ToString(getTaskRefDetails.Rows[0]["Meeting_Discussion_Date"]);
					var MeetingType = Convert.ToString(getTaskRefDetails.Rows[0]["MeetingType"]);
					spm.Task_Monitoring_All_Attendee(getTaskDetails, appendAttendee, Meeting_Discussion_Title, Meeting_Discussion_Date, MeetingType, Task_Reference_ID, "");
					var link = "http://localhost/hrms/login.aspx?ReturnUrl=procs/InboxExecuter.aspx";
					foreach (DataRow item in getAllExecuter.Rows)
					{
						var getExecuter = Convert.ToString(item["Task_Executer"]);
						var getTaskEmailDetails = spm.getTaskMonitoringReport("getTaskDetailsByExecuter", getExecuter, getTaskRef, 0);
						if (getTaskEmailDetails != null)
						{
							if (getTaskEmailDetails.Tables.Count > 0)
							{
								var getTaskDetails1 = getTaskEmailDetails.Tables[0];
								if (getTaskDetails1.Rows.Count > 0)
								{
									var ExecuterEmail = Convert.ToString(getTaskDetails1.Rows[0]["ExecuterEmail"]);
									var SupervisorEmail = Convert.ToString(getTaskDetails1.Rows[0]["SupervisorEmail"]);
									SupervisorEmail = "";
									spm.Task_Monitoring_Executer(getTaskDetails1, ExecuterEmail, Meeting_Discussion_Title, Meeting_Discussion_Date, MeetingType, Task_Reference_ID, link, SupervisorEmail);
								}
							}
						}
					}

				}
			}
		}

		//Add Old Ref 
		var getMettingOldref = ddl_Meeting_Id.SelectedValue.ToString();
		if (getMettingOldref != "0")
		{
			//Click on Add reference
			foreach (GridViewRow row in dg_OldTaskDetails.Rows)
			{
				//Finding Dropdown control  
				CheckBox ddl1 = row.FindControl("chk_Ref_Select") as CheckBox;
				if (ddl1.Checked == true)
				{
					var empCode = Convert.ToString(Session["Empcode"]);
					//Add refanace 
					getOldTaskRef = Convert.ToDouble(0);
					getMettingOldref = ddl_Meeting_Id.SelectedValue.ToString();

					if (getMettingOldref != "")
					{
						getOldTaskRef = Convert.ToDouble(getMettingOldref);
						hdnOldTaskRefID.Value = getOldTaskRef.ToString();
					}
					else
					{
						hdnOldTaskRefID.Value = getOldTaskRef.ToString();
					}
					Label lblTask_id = row.FindControl("lblTask_id") as Label;
					var getlblTask_id = Convert.ToInt32(lblTask_id.Text.Trim());
					var getStatusTask = spm.InsertUpdateTaskDetails(getlblTask_id, getRefId, "AddTaskRef", empCode, empCode, "", "", 0, false, getOldTaskRef, "", "", "", "", "", "", false, 0, false, 0, false, 0);

				}
			}
		}
		DeleteTempAttendee();
		//End Email
		Response.Redirect("~/procs/TaskMonitoring.aspx");
		//}
		//catch (Exception ex)
		//{

		//}
	}

	protected void chk_for_Info_CheckedChanged(object sender, EventArgs e)
	{
		try
		{
			var getVal = chk_for_Info.Checked;
			if (getVal == true)
			{
				//ClearTask();
				ddl_Task_Supervisor.Enabled = false;
				ddl_Task_Executor.Enabled = false;
				txt_Due_Date.Enabled = false;
				//  txt_TaskDescripation.Enabled = false;
				// txt_TaskRemark.Enabled = false;
				uploadfile.Enabled = false;

				chk_Reminder_Due_Date.Enabled = false;
				chk_Reminder_Due_Date.Checked = false;

				txt_Reminder_Day.Text = "";
				txt_Reminder_Day.Enabled = false;

				chk_Escalation_Due_Date.Enabled = false;
				chk_Escalation_Due_Date.Checked = false;

				chk_Escalation_Repeate.Enabled = false;

				txt_Reminder_Repe_Day.Text = "";
				txt_Reminder_Repe_Day.Enabled = false;

			}
			else
			{
				// ClearTask();
				ddl_Task_Supervisor.Enabled = true;
				ddl_Task_Executor.Enabled = true;
				txt_Due_Date.Enabled = true;
				// txt_TaskDescripation.Enabled = true;
				//txt_TaskRemark.Enabled = true;
				uploadfile.Enabled = true;
				chk_Reminder_Due_Date.Enabled = true;
				chk_Reminder_Due_Date.Checked = true;

				txt_Reminder_Day.Text = "1";
				txt_Reminder_Day.Enabled = true;

				chk_Escalation_Due_Date.Enabled = true;
				chk_Escalation_Due_Date.Checked = true;

				chk_Escalation_Repeate.Enabled = true;

				txt_Reminder_Repe_Day.Text = "1";
				txt_Reminder_Repe_Day.Enabled = true;
			}
		}
		catch (Exception)
		{

		}
	}

	protected void lnk_File_Delete_Click(object sender, ImageClickEventArgs e)
	{
		try
		{
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			var fId = Convert.ToString(gv_Documents.DataKeys[row.RowIndex].Values[0]).Trim();
			var FilePath = Convert.ToString(gv_Documents.DataKeys[row.RowIndex].Values[1]).Trim();
			if (fId != null)
			{
				var id = int.Parse(fId);
				var result = spm.DeleteAttendeeDetails(id, "DeleteDocumentDetails_Temp");
				if (result == true)
				{

					string path = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["TaskMonitoringPath"]).Trim()), FilePath);
					FileInfo file = new FileInfo(path);
					if (file.Exists)//check file exsit or not  
					{
						file.Delete();
					}
					//ClearDocumentDetails();
					var getDocumentTable = spm.getTempAttendee("GetDocumentDetails_Temp", "", Convert.ToDouble(hdnTaskID.Value));
					gv_Documents.DataSource = null;
					gv_Documents.DataBind();
					// var getDocumentTable = getdsTables.Tables[2];
					if (getDocumentTable.Rows.Count > 0)
					{
						gv_Documents.DataSource = getDocumentTable;
						gv_Documents.DataBind();

					}
				}
				else
				{
					Label2.Text = "Something went wrong , Please try again.";
					return;
				}
			}
		}
		catch (Exception ex)
		{
			Label2.Text = ex.Message.ToString();
			return;
		}
	}
	//Save Orgnaz..
	protected void btn_Org_Save_Click(object sender, EventArgs e)
	{
		try
		{
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}
			Label1.Text = "";
			var empCode = Convert.ToString(Session["Empcode"]);
			var getVal = Convert.ToInt32(hdnTaskRefID.Value);
			var IsSelect = false;
			var count = 0;
			var getOrganizer = true; //chk_Ref_Select.Checked;

			if (ddl_Organizer.SelectedValue == "" || ddl_Organizer.SelectedValue == "0")
			{
				Label1.Text = "Please select Organizer";
				return;
			}

			var getAttendee = "";
			if (getOrganizer == true)
			{
				var getOrginserStatus = spm.getTempAttendee("CheckIsOrginser", empCode, getVal);
				if (getOrginserStatus != null)
				{
					if (getOrginserStatus.Rows.Count > 0)
					{
						var getMsg = Convert.ToString(getOrginserStatus.Rows[0]["Message"]);
						if (getMsg == "Exists")
						{
							Label1.Text = "Please select one organizer ";
							return;
						}
					}
				}
			}
			var qtype = "";
			if (getVal == 0)
			{
				qtype = "AddTempAttendee";
			}
			else
			{
				qtype = "AddTempAttendee";
			}
			var getStatus = false;

			if (ddl_Organizer.SelectedIndex > 0)
			{
				getAttendee = ddl_Organizer.SelectedValue;
				getStatus = spm.InsertUpdateTempAttendee(0, getVal, qtype, getAttendee, empCode, getOrganizer);
			}
			if (getStatus == false)
			{
				Label1.Text = "Something went wrong";
				return;
			}
			else
			{
				ClearAttendee();
				if (getVal == 0)
				{
					var getAttendeeList = spm.getTempAttendee("GetTempAttendee", empCode, 0);
					BindGridAttendee(getAttendeeList);
				}
				else
				{
					var getAttendeeList = spm.getTempAttendee("GetTempAttendee", empCode, getVal);
					BindGridAttendee(getAttendeeList);
				}
			}


		}
		catch (Exception ex)
		{

		}

	}
	//Update Orgnaz...
	protected void btn_Org_Update_Click(object sender, EventArgs e)
	{
		try
		{
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}
			Label1.Text = "";
			var empCode = Convert.ToString(Session["Empcode"]);
			var getVal = Convert.ToInt32(hdnTaskRefID.Value);
			var getId = Convert.ToInt32(hdnAttendeeID.Value);
			var IsSelect = false;
			var count = 0;
			var getOrganizer = true;

			if (ddl_Organizer.SelectedValue == "" || ddl_Organizer.SelectedValue == "0")
			{
				Label1.Text = "Please select Organizer";
				return;
			}
			if (count >= 2 && getOrganizer == true)
			{
				Label1.Text = "Please select only one organizer";
				return;
			}

			var getAttendee = "";
			if (getOrganizer == true)
			{
				var getOrginserStatus = spm.getTempAttendee("CheckIsOrginserUpdate", empCode, getId);
				if (getOrginserStatus != null)
				{
					if (getOrginserStatus.Rows.Count > 0)
					{
						var getMsg = Convert.ToString(getOrginserStatus.Rows[0]["Message"]);
						if (getMsg == "Exists")
						{
							Label1.Text = "Please select only one organizer ";
							return;
						}
					}
				}
			}
			var qtype = "";
			if (getVal == 0)
			{
				qtype = "UpdateTempAttendee";
			}
			else
			{
				qtype = "UpdateTempAttendee";
			}
			var getStatus = false;
			if (ddl_Organizer.SelectedIndex > 0)
			{
				getAttendee = ddl_Organizer.SelectedValue;
				getStatus = spm.InsertUpdateTempAttendee(getId, getVal, qtype, getAttendee, empCode, getOrganizer);
			}

			if (getStatus == false)
			{
				Label1.Text = "Something went wrong";
				return;
			}
			else
			{
				ClearAttendee();
				if (getVal == 0)
				{
					var getAttendeeList = spm.getTempAttendee("GetTempAttendee", empCode, 0);
					BindGridAttendee(getAttendeeList);
				}
				else
				{
					var getAttendeeList = spm.getTempAttendee("GetTempAttendee", empCode, getVal);
					BindGridAttendee(getAttendeeList);
				}
			}

		}
		catch (Exception ex)
		{

		}
	}
}
