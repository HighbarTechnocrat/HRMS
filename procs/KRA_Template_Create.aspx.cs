using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Linq;
using ClosedXML.Excel;

public partial class KRA_Template_Create : System.Web.UI.Page
{
	SP_Methods spm = new SP_Methods();

	#region Creative_Default_methods


	public static string dob1;
	public string userid;
	public static string pimg = "";
	public static string cimg = "";
	public string loc = "", dept = "", subdept = "", desg = "";
	public int did = 0;
	public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
	protected void lnkhome_Click(object sender, EventArgs e)
	{
		Response.Redirect(ReturnUrl("sitepathmain") + "default");
	}
	#endregion


	#region Page_Events
	private void Page_Load(object sender, System.EventArgs e)
	{
		try
		{
			if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
			}

			lblmessage.Text = "";
			lbl_goal_msg.Text = "";
			lbl_Measurement_msg.Text = "";
			if (Convert.ToString(Session["Empcode"]).Trim() == "")
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

			txtEmpCode.Text = Convert.ToString(Session["Empcode"]);

			if (Page.User.Identity.IsAuthenticated == false)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/KRA_index.aspx");
			}
			else
			{
				Page.SmartNavigation = true;

				if (!Page.IsPostBack)
				{
					//  txt_goal_description.Attributes.Add("maxlength", "2");
					txt_remakrs.Attributes.Add("maxlength", txt_remakrs.MaxLength.ToString());
					// txt_Weightage.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					txt_Weightage.Attributes.Add("onkeypress", "return onCharOnlyNumber_dot(event);");
					txt_goal_seq_no.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					txt_measurement_seq_no.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");

					hdngoal_id.Value = "";
					hdnKRA_id.Value = "";

					delete_KRAdata_temptable();
					if (Request.QueryString.Count > 0)
					{
						hdnKRA_id.Value = Request.QueryString[0];
						hdnKRA_Role_id.Value = Request.QueryString[1];
						hdnstype_Main.Value = "Update";
						btnback.Visible = true;


					}


					getKRA_Roles();
					getKRA_Units();
					//get_goal_measurement_Details_New();
					get_Role_Employee_List();
					get_Employee_AssignedList_temp();

					if (Convert.ToString(hdnKRA_Role_id.Value).Trim() != "")
					{
						lstRoles.SelectedValue = hdnKRA_Role_id.Value;

					}
					if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
					{
						//check_Selected_Roles();
						lstRoles.Enabled = false;
						btnCancel.Visible = false;
						delete_KRAdata_temptable();
						check_Selected_Roles();
						setVisible_Cntrl(false);
						litrvlgrid.Visible = true;
						getKRA_Weightage();
						//accmo_delete_btn.Visible = true;



					}

					idOtherRole.Visible = false;
					idspnOtherRole.Visible = false;
					txtRoleName.Visible = false;
					if (lstRoles.SelectedValue == "1")
					{
						idOtherRole.Visible = true;
						txtRoleName.Visible = true;
						idspnOtherRole.Visible = true;
					}

				}

			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}

	protected void btnback_mng_Click(object sender, EventArgs e)
	{
		try
		{


			#region Validation

			if (Convert.ToString(txt_Weightage.Text).Trim() == "")
			{
				lbl_goal_msg.Text = "Please enter Weightage.";
				return;
			}

			if (Convert.ToString(txt_Weightage.Text).Trim() != "")
			{
				string[] strdate;
				strdate = Convert.ToString(txt_Weightage.Text).Trim().Split('.');
				if (strdate.Length > 2)
				{
					lbl_goal_msg.Text = "Please enter correct Weightage.";
					return;
				}

				Decimal dfare = 0;
				dfare = Convert.ToDecimal(txt_Weightage.Text);
				if (dfare == 0)
				{
					lbl_goal_msg.Text = "Please enter correct Weightage.";
					return;
				}

				if (dfare > 100)
				{
					lbl_goal_msg.Text = "Please enter correct Weightage.";
					return;
				}
			}

			if (Convert.ToString(txt_goal_title.Text).Trim() == "")
			{
				lblAddGoalMsg.Text = "";
				lbl_goal_msg.Text = "Please enter Goal Title";
				return;
			}
			if (Convert.ToString(txt_Weightage.Text).Trim() == "" || Convert.ToDecimal(txt_Weightage.Text) <= 0)
			{
				lbl_goal_msg.Text = "Please enter Weightage";
				lblAddGoalMsg.Text = "";
				return;
			}

			if (dgMeasurementslist.Rows.Count == 0)
			{
				lbl_goal_msg.Text = "Please Add Measurement Details.";
				lblAddGoalMsg.Text = "";
				return;
			}

			if (Convert.ToString(txt_goal_seq_no.Text).Trim() != "")
			{
				if (Convert.ToString(txt_goal_seq_no.Text).Trim() != Convert.ToString(hdnGoal_Seq_no_old.Value).Trim())
				{
					DataSet dsGoalSeqNo = check_goal_Sequance_no_exist();
					if (dsGoalSeqNo.Tables[0].Rows.Count > 0)
					{
						if (Convert.ToString(dsGoalSeqNo.Tables[0].Rows[0]["goal_seq_no"]).Trim() != "")
						{
							lbl_goal_msg.Text = "Please check Goal Sequence Number already exist";
							lblAddGoalMsg.Text = "";
							return;
						}
					}
				}
			}

			if (Convert.ToDecimal(txt_Weightage.Text) > 100)
			{
				lbl_goal_msg.Text = "Please make sure total of weightage is 100 and then try again to Submit";
				lblAddGoalMsg.Text = "";
				return;
			}
			//check goal title already exist 
			DataSet tdsgoal = new DataSet();
			tdsgoal = check_goal_already_exist();
			if (tdsgoal.Tables.Count > 0)
			{
				if (Convert.ToString(hdnGoal_Title_old.Value).Trim().ToLower() != Convert.ToString(txt_goal_title.Text).Trim().ToLower())
					if (tdsgoal.Tables[0].Rows.Count > 0)
					{
						if (Convert.ToString(tdsgoal.Tables[0].Rows[0]["Goal_Title"]).Trim() != "")
						{
							lbl_goal_msg.Text = "Please check Goal Title already exist";
							lblAddGoalMsg.Text = "";
							return;
						}
					}

				//// //check  Weightage not more than 100
				if (tdsgoal.Tables[1].Rows.Count > 0)
				{
					if (Convert.ToString(tdsgoal.Tables[1].Rows[0]["Weightage"]).Trim() != "")
					{
						decimal iWeightageCnt = Convert.ToDecimal(tdsgoal.Tables[1].Rows[0]["Weightage"]) + Convert.ToDecimal(txt_Weightage.Text);

						if (iWeightageCnt > 100)
						{
							if (Convert.ToString(hdnWwighrage_old.Value).Trim() != "")
							{
								iWeightageCnt = iWeightageCnt - Convert.ToDecimal(hdnWwighrage_old.Value);
							}
						}

						if (iWeightageCnt > 100)
						{
							lbl_goal_msg.Text = "Please make sure total of weightage is 100 and then try again to Submit";
							lblAddGoalMsg.Text = "";
							return;
						}
					}
				}
			}


			#endregion
			#region insert goal into temp table

			DataTable dsgoal = new DataTable();
			SqlParameter[] spars = new SqlParameter[8];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			if (Convert.ToString(hdn_flg_goal.Value) == "update")
				spars[0].Value = "update_Template_goal_temp";
			else
				spars[0].Value = "insert_Template_goal_temp";

			spars[1] = new SqlParameter("@Goal_Id", SqlDbType.Int);
			if (Convert.ToString(hdngoal_id.Value).Trim() != "")
				spars[1].Value = Convert.ToInt32(hdngoal_id.Value);
			else
				spars[1].Value = DBNull.Value;

			spars[2] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
			if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
				spars[2].Value = Convert.ToDecimal(hdnKRA_id.Value);
			else
				spars[2].Value = DBNull.Value;

			spars[3] = new SqlParameter("@Goal_Title", SqlDbType.VarChar);
			spars[3].Value = Convert.ToString(txt_goal_title.Text).Trim();

			spars[4] = new SqlParameter("@Goal_Description", SqlDbType.VarChar);
			spars[4].Value = Convert.ToString(txt_goal_description.Text).Trim();

			spars[5] = new SqlParameter("@emp_code", SqlDbType.VarChar);
			spars[5].Value = Convert.ToString(txtEmpCode.Text).Trim();

			spars[6] = new SqlParameter("@Weightage", SqlDbType.Decimal);
			spars[6].Value = Convert.ToDecimal(txt_Weightage.Text);

			spars[7] = new SqlParameter("@goal_seq_no", SqlDbType.Int);
			if (Convert.ToString(txt_goal_seq_no.Text).Trim() != "")
				spars[7].Value = Convert.ToInt32(txt_goal_seq_no.Text);
			else
				spars[7].Value = DBNull.Value;

			dsgoal = spm.getDataList(spars, "SP_KRA_Insert_Update");

			hdngoal_id.Value = Convert.ToString(dsgoal.Rows[0]["maxgaolId"]).Trim();




			hdngoal_id.Value = "";
			txt_goal_title.Text = "";
			txt_goal_description.Text = "";
			txt_Weightage.Text = "";
			btnback_mng.Text = "Add Goal";
			hdn_flg_goal.Value = "";
			hdngoal_id.Value = "";
			txt_goal_seq_no.Text = "";
			hdnWwighrage_old.Value = "";
			hdnGoal_Title_old.Value = "";
			hdnGoal_Seq_no_old.Value = "";
			trvldeatils_delete_btn.Visible = false;
			dgMeasurementslist.DataSource = null;
			dgMeasurementslist.DataBind();
			get_goal_measurement_Details_New();

			lblAddGoalMsg.Visible = false;





			#endregion



		}
		catch (Exception ex)
		{

		}

	}

	protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
	{
		try
		{
			#region Validation
			if (Convert.ToString(txt_Measurement_dtls.Text).Trim() == "")
			{
				lbl_Measurement_msg.Text = "Please enter Measurement Details.";
				return;
			}
			if (Convert.ToString(lstUnit.SelectedValue).Trim() == "0")
			{
				lbl_Measurement_msg.Text = "Please select Unit.";
				return;
			}
			if (Convert.ToString(txt_Mqty.Text).Trim() == "")
			{
				lbl_Measurement_msg.Text = "Please enter Quantity.";
				return;
			}

			if (Convert.ToString(txt_measurement_seq_no.Text).Trim() != "")
			{
				if (Convert.ToString(txt_measurement_seq_no.Text).Trim() != Convert.ToString(hdnMeasurement_Seq_no_old.Value).Trim())
				{
					DataSet dsGoalSeqNo = check_Measurement_Sequance_no_exist();
					if (dsGoalSeqNo.Tables[0].Rows.Count > 0)
					{
						if (Convert.ToString(dsGoalSeqNo.Tables[0].Rows[0]["measurement_seq_no"]).Trim() != "")
						{
							lbl_Measurement_msg.Text = "Please check Measurement Sequence Number already exist";
							return;
						}
					}
				}
			}

			#endregion




			#region insert Measurement into temp table

			DataTable dsgoal = new DataTable();
			SqlParameter[] spars = new SqlParameter[10];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			if (Convert.ToString(hdn_flg_measurement.Value) == "update")
			{
				spars[0].Value = "update_Template_Measurement_temp_temp";
				lblAddGoalMsg.Text = "Please click on Update Goal button to save the changes";
			}
			else
			{
				spars[0].Value = "insert_Template_Measurement_temp_temp";
				lblAddGoalMsg.Text = "Please click on Add Goal button to save the changes";
			}

			spars[1] = new SqlParameter("@Goal_Id", SqlDbType.Int);
			if (Convert.ToString(hdngoal_id.Value).Trim() != "")
				spars[1].Value = Convert.ToInt32(hdngoal_id.Value);
			else
				spars[1].Value = DBNull.Value;

			spars[2] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
			if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
				spars[2].Value = Convert.ToDecimal(hdnKRA_id.Value);
			else
				spars[2].Value = DBNull.Value;

			spars[3] = new SqlParameter("@Measurement_id", SqlDbType.Int);
			if (Convert.ToString(hdnMeasurement_id.Value).Trim() != "")
				spars[3].Value = Convert.ToInt32(hdnMeasurement_id.Value);
			else
				spars[3].Value = DBNull.Value;

			spars[4] = new SqlParameter("@Measurement_Details", SqlDbType.VarChar);
			spars[4].Value = Convert.ToString(txt_Measurement_dtls.Text).Trim();

			spars[5] = new SqlParameter("@emp_code", SqlDbType.VarChar);
			spars[5].Value = Convert.ToString(txtEmpCode.Text).Trim();

			spars[6] = new SqlParameter("@unit_id", SqlDbType.Int);
			spars[6].Value = Convert.ToInt32(lstUnit.SelectedValue);

			spars[7] = new SqlParameter("@Quantity", SqlDbType.VarChar);
			spars[7].Value = Convert.ToString(txt_Mqty.Text).Trim();

			spars[8] = new SqlParameter("@remarks", SqlDbType.VarChar);
			spars[8].Value = Convert.ToString(txt_remakrs.Text).Trim();

			spars[9] = new SqlParameter("@measurement_seq_no", SqlDbType.Int);
			if (Convert.ToString(txt_measurement_seq_no.Text).Trim() != "")
				spars[9].Value = Convert.ToInt32(txt_measurement_seq_no.Text);
			else
				spars[9].Value = DBNull.Value;



			dsgoal = spm.getDataList(spars, "SP_KRA_Insert_Update");

			#endregion

			//fill Measurement Gird from temp table

			//clear controls after add Measurement to temp table
			txt_Measurement_dtls.Text = "";
			lstUnit.SelectedValue = "0";
			txt_Mqty.Text = "";
			txt_remakrs.Text = "";
			txt_measurement_seq_no.Text = "";
			btnTra_Details.Text = "+";
			DivTrvl.Visible = false;
			trvldeatils_btnSave.Text = "Add Measurement";
			hdn_flg_measurement.Value = "";
			hdnMeasurement_Seq_no_old.Value = "";
			lblAddGoalMsg.Visible = true;

			DataSet dsMeasuremnsts = getMeasurements_temp_List(Convert.ToString(hdngoal_id.Value));
			dgMeasurementslist.DataSource = dsMeasuremnsts.Tables[0];
			dgMeasurementslist.DataBind();



		}
		catch (Exception ex)
		{

		}
	}

	protected void btnCancel_Click(object sender, EventArgs e)
	{
		string confirmValue = hdnYesNo.Value.ToString();
		if (confirmValue != "Yes")
		{
			return;
		}

		#region validations

		if (dgKRA_Details.Rows.Count <= 0)
		{
			lblmessage.Text = "Please enter Goal and Measurements details";
			return;
		}

		#endregion



		SubmitKRA(true);



	}

	protected void rptKRA_Details_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
		{
			string goalId = (e.Item.FindControl("hdnGoal_Id_R") as HiddenField).Value;
			Repeater rptMeasurements = e.Item.FindControl("rptMeasurements") as Repeater;
			Repeater rptUnits = e.Item.FindControl("rptUnits") as Repeater;
			Repeater rptQuantity = e.Item.FindControl("rptQuantity") as Repeater;

			DataSet dsMeasuremnsts = getMeasurements_List(goalId);
			rptMeasurements.DataSource = dsMeasuremnsts.Tables[0];
			rptMeasurements.DataBind();

			rptUnits.DataSource = dsMeasuremnsts.Tables[0];
			rptUnits.DataBind();

			rptQuantity.DataSource = dsMeasuremnsts.Tables[0];
			rptQuantity.DataBind();


		}
	}

	protected void lnk_edit_Measurement_Click(object sender, EventArgs e)
	{
		try
		{
			// ImageButton btn = (ImageButton)sender;
			LinkButton btn = (LinkButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			hdnMeasurement_id.Value = Convert.ToString(dgMeasurementslist.DataKeys[row.RowIndex].Values[0]).Trim();

			DataSet dsmeasurement_dtl = spm.KRA_get_Template_Mesurements_detials_edit(txtEmpCode.Text, Convert.ToInt32(hdnMeasurement_id.Value)); ;
			txt_Measurement_dtls.Text = "";
			lstUnit.SelectedValue = "0";
			hdnMeasurement_Seq_no_old.Value = "";
			txt_Mqty.Text = "";
			txt_remakrs.Text = "";

			if (dsmeasurement_dtl.Tables[0].Rows.Count > 0)
			{
				txt_Measurement_dtls.Text = Convert.ToString(dsmeasurement_dtl.Tables[0].Rows[0]["Measurement_Details"]).Trim();
				lstUnit.SelectedValue = Convert.ToString(dsmeasurement_dtl.Tables[0].Rows[0]["unit_id"]).Trim();
				txt_Mqty.Text = Convert.ToString(dsmeasurement_dtl.Tables[0].Rows[0]["Quantity"]).Trim();
				txt_remakrs.Text = Convert.ToString(dsmeasurement_dtl.Tables[0].Rows[0]["remarks"]).Trim();
				txt_measurement_seq_no.Text = Convert.ToString(dsmeasurement_dtl.Tables[0].Rows[0]["measurement_seq_no"]).Trim();
				hdnMeasurement_Seq_no_old.Value = Convert.ToString(dsmeasurement_dtl.Tables[0].Rows[0]["measurement_seq_no"]).Trim();

				hdn_flg_measurement.Value = "update";
				trvldeatils_btnSave.Text = "Update Measurement";
				DivTrvl.Visible = true;
				btnTra_Details.Text = "-";
			}
		}
		catch (Exception ex)
		{

		}

	}

	protected void trvldeatils_delete_btn_Click1(object sender, EventArgs e)
	{

		string confirmValue = hdnYesNo.Value.ToString();
		if (confirmValue != "Yes")
		{
			return;
		}

		#region Delete Goal and Measurement from temp 
		DataSet dsgoal = new DataSet();
		SqlParameter[] spars = new SqlParameter[3];

		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "delete_Template_temp_goal_measurement";

		spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
		spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

		spars[2] = new SqlParameter("@Goal_Id", SqlDbType.Int);
		if (Convert.ToString(hdngoal_id.Value).Trim() != "")
			spars[2].Value = Convert.ToInt32(hdngoal_id.Value);
		else
			spars[2].Value = DBNull.Value;

		dsgoal = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
		get_goal_measurement_Details_New();

		trvldeatils_delete_btn.Visible = false;
		dgMeasurementslist.DataSource = null;
		dgMeasurementslist.DataBind();
		txt_goal_title.Text = "";
		txt_Weightage.Text = "";
		txt_goal_description.Text = "";
		txt_goal_seq_no.Text = "";
		hdn_flg_goal.Value = "";
		btnback_mng.Text = "Add Goal";


		#endregion

	}

	protected void lnkedit_goal_Click(object sender, EventArgs e)
	{

		LinkButton btn = (LinkButton)sender;
		GridViewRow row = (GridViewRow)btn.NamingContainer;
		hdngoal_id.Value = Convert.ToString(dgKRA_Details.DataKeys[row.RowIndex].Values[1]).Trim();

		// DivTrvl.Visible = true;
		//btnTra_Details.Text = "-";



		DataSet dsMeasuremnsts = getMeasurements_list_temp_to_temmp(Convert.ToString(hdngoal_id.Value)); //getMeasurements_temp_List(Convert.ToString(hdngoal_id.Value));
		dgMeasurementslist.DataSource = dsMeasuremnsts.Tables[0];
		dgMeasurementslist.DataBind();
		li1.Visible = true;
		// litrvldetail.Visible = true;
		setVisible_MeasurementsCntrols(true);
		trvldeatils_delete_btn.Visible = false;
		txt_goal_title.Text = "";
		txt_goal_description.Text = "";
		txt_Weightage.Text = "";
		txt_goal_seq_no.Text = "";
		if (dsMeasuremnsts.Tables.Count > 1)
		{
			if (dsMeasuremnsts.Tables[1].Rows.Count > 0)
			{
				txt_goal_title.Text = Convert.ToString(dsMeasuremnsts.Tables[1].Rows[0]["Goal_Title"]).Trim();
				hdnGoal_Title_old.Value = Convert.ToString(dsMeasuremnsts.Tables[1].Rows[0]["Goal_Title"]).Trim();
				txt_goal_description.Text = Convert.ToString(dsMeasuremnsts.Tables[1].Rows[0]["Goal_Description"]).Trim();
				txt_Weightage.Text = Convert.ToString(dsMeasuremnsts.Tables[1].Rows[0]["Weightage"]).Trim();
				hdnWwighrage_old.Value = Convert.ToString(dsMeasuremnsts.Tables[1].Rows[0]["Weightage"]).Trim();
				txt_goal_seq_no.Text = Convert.ToString(dsMeasuremnsts.Tables[1].Rows[0]["goal_seq_no"]).Trim();
				hdnGoal_Seq_no_old.Value = Convert.ToString(dsMeasuremnsts.Tables[1].Rows[0]["goal_seq_no"]).Trim();
				hdn_flg_goal.Value = "update";
				btnback_mng.Text = "Update Goal";


				setVisible_GoalCntrols(true);
			}



		}

	}

	protected void btnTra_Details_Click(object sender, EventArgs e)
	{
		trvldeatils_btnSave.Text = "Add Measurement";
		if (DivTrvl.Visible)
		{
			DivTrvl.Visible = false;
			btnTra_Details.Text = "+";
		}
		else
		{
			DivTrvl.Visible = true;
			btnTra_Details.Text = "-";
		}


	}


	protected void trvl_btnSave_Click(object sender, EventArgs e)
	{
		string confirmValue = hdnYesNo.Value.ToString();
		if (confirmValue != "Yes")
		{
			return;
		}

		if (Convert.ToString(lstRoles.SelectedValue).Trim() == "1")
		{
			if (Convert.ToString(txtRoleName.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter New Role Name";
				return;
			}
		}

		if (Convert.ToString(lstRoles.SelectedValue).Trim() == "0")
		{
			lblmessage.Text = "Please Select Role Name";
			return;
		}

		if (dgKRA_Details.Rows.Count <= 0)
		{
			lblmessage.Text = "Please enter Goal and Measurements details";
			return;
		}

		#region validations


		//check goal title already exist 
		DataSet tdsgoal = new DataSet();
		tdsgoal = check_goal_already_exist();
		if (tdsgoal.Tables.Count > 0)
		{
			//// //check  Weightage not less than 100
			if (tdsgoal.Tables[1].Rows.Count > 0)
			{
				if (Convert.ToString(tdsgoal.Tables[1].Rows[0]["Weightage"]).Trim() != "")
				{
					Int32 iWeightageCnt = Convert.ToInt32(tdsgoal.Tables[1].Rows[0]["Weightage"]);

					if (iWeightageCnt < 100)
					{
						lbl_goal_msg.Text = "Please make sure total of weightage is 100 and then try again to Submit";
						return;
					}
				}
			}
		}



		//if(blnchk==false)
		//{
		//    lblmessage.Text = "Please enter Goal and Measurements details";
		//    return;
		//} 

		#endregion


		// System.Threading.Thread.Sleep(2147483647);
		//  System.Threading.Thread.Sleep(100);
		SubmitKRA(false);


	}

	protected void trvldeatils_delete_btn_Click(object sender, EventArgs e)
	{

	}

	protected void trvldeatils_cancel_btn_Click(object sender, EventArgs e)
	{

		DivTrvl.Visible = false;
		txt_Measurement_dtls.Text = "";
		lstUnit.SelectedValue = "0";
		txt_Mqty.Text = "";
		txt_remakrs.Text = "";
		txt_measurement_seq_no.Text = "";
		btnTra_Details.Text = "+";
		DivTrvl.Visible = false;
		trvldeatils_btnSave.Text = "Add Measurement";
		hdn_flg_measurement.Value = "";
		hdnMeasurement_Seq_no_old.Value = "";

	}

	protected void accmo_delete_btn_Click(object sender, EventArgs e)
	{
		try
		{

			DataSet dtDepartment = new DataSet();

			SqlParameter[] spars = new SqlParameter[5];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "Insert_Existing_Template_to_Employee";

			spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
			spars[1].Value = txtEmpCode.Text;

			spars[2] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
			if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
				spars[2].Value = Convert.ToDecimal(hdnKRA_id.Value);
			else
				spars[2].Value = DBNull.Value;

			spars[3] = new SqlParameter("@period_id", SqlDbType.Int);
			if (Convert.ToString(hdnPeriod_id.Value).Trim() != "")
				spars[3].Value = Convert.ToInt32(hdnPeriod_id.Value);
			else
				spars[3].Value = DBNull.Value;

			spars[4] = new SqlParameter("@Role_Id", SqlDbType.Int);
			if (Convert.ToString(hdnKRA_Role_id.Value).Trim() != "")
				spars[4].Value = Convert.ToInt32(hdnKRA_Role_id.Value);
			else
				spars[4].Value = DBNull.Value;

			dtDepartment = spm.getDatasetList(spars, "SP_KRA_Insert_Update");
			Get_EmailToNewEmployeeViewAccepttheKRA(hdnPeriod_id.Value, txtEmpCode.Text.Trim());

			Response.Redirect("KRATemplate.aspx");


		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}

	private void Get_EmailToNewEmployeeViewAccepttheKRA(string PeriodID, string EmpCode)
	{
		DataSet DSmail = new DataSet();
		SqlParameter[] spars = new SqlParameter[4];

		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "EmailToNewEmployeeViewAccepttheKRA";

		spars[1] = new SqlParameter("@period_id", SqlDbType.Int);
		spars[1].Value = Convert.ToInt32(PeriodID);

		spars[2] = new SqlParameter("@Assigned_By", SqlDbType.VarChar);
		spars[2].Value = EmpCode;

		DSmail = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

		if (DSmail.Tables[0].Rows.Count > 0)
		{

			for (int i = 0; i < DSmail.Tables[0].Rows.Count; i++)
			{
				string StrEmpAdress = DSmail.Tables[0].Rows[i]["Emp_Emailaddress"].ToString();
				string StrName = DSmail.Tables[0].Rows[i]["Emp_Name"].ToString();
				string Stryear = DSmail.Tables[0].Rows[i]["period_name"].ToString();

				string StrContain1 = "This is to inform you that you are assigned with a KRA for the year <b>" + Stryear + ".</b> Please View &";
				string StrContain2 = "Accept the KRA on highest priority.";
				string StrContain3 = "This is an auto generated email, please do not reply!";
				string StrSubject = "OneHR:-KRA for the year  " + Stryear + "  is Assigned to you.View & Accept the KRA";

				spm.send_mailto_NewEmployeeViewAcceptthe_KRA(StrEmpAdress, StrSubject, StrContain1, StrContain2, StrContain3, StrName);
			}

		}

	}

	private void CombineColumnCells(GridViewRow currentRow, int colIndex, string fieldName)
	{
		TableCell currentCell = currentRow.Cells[colIndex];

		if (currentCell.Visible)
		{
			Object currentValue = dgKRA_Details.DataKeys[currentRow.RowIndex].Values[fieldName];


			for (int nextRowIndex = currentRow.RowIndex + 1; nextRowIndex < dgKRA_Details.Rows.Count; nextRowIndex++)
			{
				Object nextValue = dgKRA_Details.DataKeys[nextRowIndex].Values[fieldName];

				if (nextValue.ToString() == currentValue.ToString())
				{
					GridViewRow nextRow = dgKRA_Details.Rows[nextRowIndex];
					TableCell nextCell = nextRow.Cells[colIndex];
					currentCell.RowSpan = Math.Max(1, currentCell.RowSpan) + 1;
					nextCell.Visible = false;
				}
				else
				{
					break;
				}
			}
		}
	}

	private void CombineColumnCells_View(GridViewRow currentRow_V, int colIndex_V, string fieldName__V)
	{
		TableCell currentCell = currentRow_V.Cells[colIndex_V];

		if (currentCell.Visible)
		{
			Object currentValue = dgKRATemplateView.DataKeys[currentRow_V.RowIndex].Values[fieldName__V];

			for (int nextRowIndex = currentRow_V.RowIndex + 1; nextRowIndex < dgKRATemplateView.Rows.Count; nextRowIndex++)
			{
				Object nextValue = dgKRATemplateView.DataKeys[nextRowIndex].Values[fieldName__V];

				if (nextValue.ToString() == currentValue.ToString())
				{
					GridViewRow nextRow = dgKRATemplateView.Rows[nextRowIndex];
					TableCell nextCell = nextRow.Cells[colIndex_V];
					currentCell.RowSpan = Math.Max(1, currentCell.RowSpan) + 1;
					nextCell.Visible = false;
				}
				else
				{
					break;
				}
			}
		}
	}



	protected void lstUnit_SelectedIndexChanged(object sender, EventArgs e)
	{
		txt_Mqty.Enabled = true;
		txt_Mqty.Text = "";
		if (Convert.ToString(lstUnit.SelectedItem.Text).Trim() == "Adherence to Completion")
		{
			txt_Mqty.Text = "As agreed";
			txt_Mqty.Enabled = false;
		}

		if (Convert.ToString(lstUnit.SelectedItem.Text).Trim() == "Ratio")
        	{
            		txt_Mqty.Text = "As agreed";
            		txt_Mqty.Enabled = false;
        	}
        	
	       if (Convert.ToString(lstUnit.SelectedItem.Text).Trim() == "INR" || Convert.ToString(lstUnit.SelectedItem.Text).Trim() == "Score")
               {
        	    txt_Mqty.Text = "As per target";
	            txt_Mqty.Enabled = false;
        	}
 
	}

	#endregion



	#region PageMethods

	private void SubmitKRA(Boolean isDraft)
	{

		#region Add Template Role
		if (Convert.ToString(lstRoles.SelectedValue).Trim() == "1")
		{
			if (Convert.ToString(txtRoleName.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter New Role Name";
				return;
			}
		}

		if (Convert.ToString(lstRoles.SelectedValue).Trim() == "0")
		{
			lblmessage.Text = "Please Select Role Name";
			return;
		}
		if (Convert.ToString(txtRoleName.Text).Trim() != "")
		{
			DataSet lds = new DataSet();

			lds = spm.KRA_Insert_Update_Role("Insert_Role", Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(txtRoleName.Text).Trim());

			if (lds.Tables[0].Rows.Count > 0)
			{
				if (Convert.ToString(lds.Tables[0].Rows[0]["msg"]).Trim() == "error")
				{
					lblmessage.Text = "Role Name already exist";
					return;
				}

				if (Convert.ToString(lds.Tables[0].Rows[0]["msg"]).Trim() == "insert")
					hdnKRA_Role_id.Value = Convert.ToString(lds.Tables[0].Rows[0]["roleid"]).Trim();

			}

		}
		#endregion

		#region insert or update KRA
		string sType = "insert_KRA_Template_main";

		if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
			sType = "update_KRA_Template_main";

		DataSet dsgoal = new DataSet();
		SqlParameter[] spars = new SqlParameter[6];

		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = sType;

		spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
		spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

		spars[2] = new SqlParameter("@IsDraft", SqlDbType.Bit);
		spars[2].Value = isDraft;

		spars[3] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
		if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
			spars[3].Value = Convert.ToDecimal(hdnKRA_id.Value);
		else
			spars[3].Value = DBNull.Value;

		spars[4] = new SqlParameter("@Role_Id", SqlDbType.Int);
		if (Convert.ToString(txtRoleName.Text).Trim() != "")
			spars[4].Value = Convert.ToInt32(hdnKRA_Role_id.Value);
		else
			spars[4].Value = Convert.ToInt32(lstRoles.SelectedValue);

		spars[5] = new SqlParameter("@period_id", SqlDbType.Int);
		spars[5].Value = Convert.ToInt32(hdnPeriod_id.Value);


		dsgoal = spm.getDatasetList(spars, "SP_KRA_Insert_Update");
		double dMaxReqId = 0;
		dMaxReqId = Convert.ToDouble(dsgoal.Tables[0].Rows[0]["MaxKRAID"]);

		Get_EmailToNewEmployeeViewAccepttheKRA(hdnPeriod_id.Value, txtEmpCode.Text.Trim());

		#endregion

		#region add Employee to template
		//string sinsert = " Delete tbl_KRA_Template_to_Employee where Temp_KRA_ID="+dMaxReqId+";insert into tbl_KRA_Template_to_Employee(Temp_KRA_ID,emp_code) values ";
		//string svalues = "";

		//foreach (ListItem item in ddl_Employees.Items)
		//{
		//    if (item.Selected==true)
		//    {
		//        if (item.Value != "" && item.Value != "0")
		//        {
		//            if (Convert.ToString(svalues).Trim() == "")
		//                svalues = "(" + dMaxReqId +", '" + item.Value + "')";  // "'" + item.Value + "'";
		//            else
		//                svalues = svalues + "," + "(" + dMaxReqId + ", '" + item.Value + "')";
		//        }
		//    }
		//}


		//if (Convert.ToString(svalues).Trim()!="")
		//{
		//    DataTable dtTeamRpt = spm.getDataList_SQL(sinsert + svalues);
		//}
		#endregion

		txtRoleName.Text = "";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ASD", "StopLoader();", true);
        Response.Redirect("KRATemplate.aspx");
	}
	private void CopyKRA(Boolean isDraft)
	{

		#region Add Template Role
		if (Convert.ToString(lstRoles.SelectedValue).Trim() != "1")
		{
			if (Convert.ToString(txt_NewRoleName.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter New Role Name";
				return;
			}
		}

		if (Convert.ToString(lstRoles.SelectedValue).Trim() == "0")
		{
			lblmessage.Text = "Please Select Role Name";
			return;
		}
		if (Convert.ToString(txt_NewRoleName.Text).Trim() != "")
		{
			DataSet lds = new DataSet();

			lds = spm.KRA_Insert_Update_Role("Insert_Copy_Role", Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(txt_NewRoleName.Text).Trim());

			if (lds.Tables[0].Rows.Count > 0)
			{
				if (Convert.ToString(lds.Tables[0].Rows[0]["msg"]).Trim() == "error")
				{
					lblmessage.Text = "Role Name already exist";
					return;
				}

				if (Convert.ToString(lds.Tables[0].Rows[0]["msg"]).Trim() == "insert")
					hdnKRA_Role_id.Value = Convert.ToString(lds.Tables[0].Rows[0]["roleid"]).Trim();
			}
		}
		#endregion

		#region insert or update KRA
		string sType = "insert_Copy_KRA_Template_main";

		//if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
		//	sType = "update_KRA_Template_main";

		DataSet dsgoal = new DataSet();
		SqlParameter[] spars = new SqlParameter[6];

		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = sType;

		spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
		spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

		spars[2] = new SqlParameter("@Old_Role_Id", SqlDbType.Int);
		spars[2].Value = Convert.ToInt32(lstRoles.SelectedValue);

		spars[3] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
		if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
			spars[3].Value = Convert.ToDecimal(hdnKRA_id.Value);
		else
			spars[3].Value = DBNull.Value;

		spars[4] = new SqlParameter("@Role_Id", SqlDbType.Int);
		if (Convert.ToString(txt_NewRoleName.Text).Trim() != "")
			spars[4].Value = Convert.ToInt32(hdnKRA_Role_id.Value);
		else
			spars[4].Value = DBNull.Value;

		spars[5] = new SqlParameter("@period_id", SqlDbType.Int);
		spars[5].Value = Convert.ToInt32(hdnPeriod_id.Value);


		dsgoal = spm.getDatasetList(spars, "SP_KRA_Insert_Update");
		double dMaxReqId = 0;
		dMaxReqId = Convert.ToDouble(dsgoal.Tables[0].Rows[0]["MaxKRAID"]);
		//Get_EmailToNewEmployeeViewAccepttheKRA(hdnPeriod_id.Value, txtEmpCode.Text.Trim());
		#endregion
		txt_NewRoleName.Text = "";
		Response.Redirect("KRA_Template_Create.aspx?kraid=" + dMaxReqId + "&mngexp=" + hdnKRA_Role_id.Value + "");
	}

	public string ReplaceInvalidChars(string filename)
	{
		Regex illegalInFileName = new Regex(@"[#%&{}\!/<'>@+*?$|=]");
		string myString = illegalInFileName.Replace(filename, "_");
		//return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
		return myString;
	}

	public void get_Role_Employee_List()
	{
		DataSet dtDepartment = new DataSet();

		SqlParameter[] spars = new SqlParameter[4];

		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		//if (Convert.ToString(hdnKRA_id.Value).Trim() == "")
		//    spars[0].Value = "get_Role_employees_New";
		//else
		spars[0].Value = "get_Role_employees";

		spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
		spars[1].Value = txtEmpCode.Text;

		spars[2] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
		if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
			spars[2].Value = Convert.ToDecimal(hdnKRA_id.Value);
		else
			spars[2].Value = DBNull.Value;

		spars[3] = new SqlParameter("@period_id", SqlDbType.Int);
		if (Convert.ToString(hdnPeriod_id.Value).Trim() != "")
			spars[3].Value = Convert.ToInt32(hdnPeriod_id.Value);
		else
			spars[3].Value = DBNull.Value;



		dtDepartment = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

		ddl_Employees.DataSource = null;
		ddl_Employees.DataBind();
		if (dtDepartment.Tables[0].Rows.Count > 0)
		{

			ddl_Employees.DataSource = dtDepartment.Tables[0];
			ddl_Employees.DataTextField = "Emp_Name";
			ddl_Employees.DataValueField = "emp_code";
			ddl_Employees.DataBind();

			//for (int i2 = 0; i2 < dtDepartment.Tables[0].Rows.Count; i2++)
			//{
			//    for (int i = 0; i < dtDepartment.Tables[1].Rows.Count; i++)
			//    {
			//        if (ddl_Employees.Items[i2].Value.ToString().Trim() == dtDepartment.Tables[1].Rows[i]["emp_code"].ToString().Trim())
			//        {
			//            ddl_Employees.Items[i2].Selected = true;
			//            ddl_Employees.Items[i2].Text = dtDepartment.Tables[1].Rows[i]["Emp_Name"].ToString();
			//            ddl_Employees.Items[i2].Value = dtDepartment.Tables[1].Rows[i]["emp_code"].ToString();
			//            ddl_Employees.Items[i2].Enabled = true;                                           
			//        }
			//    }
			//}


		}


	}

	private DataSet check_goal_already_exist()
	{
		DataSet dsgoal = new DataSet();
		try
		{
			SqlParameter[] spars = new SqlParameter[3];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "check_Template_Goal_already_exist";

			spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

			spars[2] = new SqlParameter("@Goal_Title", SqlDbType.VarChar);
			spars[2].Value = Convert.ToString(txt_goal_title.Text).Trim();

			dsgoal = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
		}
		catch (Exception ex)
		{

		}
		return dsgoal;
	}

	private DataSet check_goal_Sequance_no_exist()
	{
		DataSet dsgoal = new DataSet();
		try
		{
			SqlParameter[] spars = new SqlParameter[3];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "check_duplicate_Template_goal_Sequance_No";

			spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

			spars[2] = new SqlParameter("@goal_seq_no", SqlDbType.Int);
			if (Convert.ToString(txt_goal_seq_no.Text).Trim() != "")
				spars[2].Value = Convert.ToInt32(txt_goal_seq_no.Text);
			else
				spars[2].Value = DBNull.Value;


			dsgoal = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
		}
		catch (Exception ex)
		{

		}
		return dsgoal;
	}

	private DataSet check_Measurement_Sequance_no_exist()
	{
		DataSet dsgoal = new DataSet();
		try
		{
			SqlParameter[] spars = new SqlParameter[4];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "check_duplicate_measurement_Sequance_No_Template";

			spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

			spars[2] = new SqlParameter("@Goal_Id", SqlDbType.Int);
			if (Convert.ToString(hdngoal_id.Value).Trim() != "")
				spars[2].Value = Convert.ToInt32(hdngoal_id.Value);
			else
				spars[2].Value = DBNull.Value;

			spars[3] = new SqlParameter("@goal_seq_no", SqlDbType.Int);
			if (Convert.ToString(txt_measurement_seq_no.Text).Trim() != "")
				spars[3].Value = Convert.ToInt32(txt_measurement_seq_no.Text);
			else
				spars[3].Value = DBNull.Value;


			dsgoal = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
		}
		catch (Exception ex)
		{

		}
		return dsgoal;
	}

	private void getKRA_Units()
	{
		DataSet dsunits = spm.KRA_getunits();
		if (dsunits.Tables[0].Rows.Count > 0)
		{
			lstUnit.DataSource = dsunits.Tables[0];
			lstUnit.DataTextField = "unit_short_name";
			lstUnit.DataValueField = "unit_id";
			lstUnit.DataBind();
			lstUnit.Items.Insert(0, new ListItem("Select Unit", "0"));
		}
	}

	public void get_goal_measurement_Details()
	{
		DataSet dtgoal = new DataSet();

		SqlParameter[] spars = new SqlParameter[2];

		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "get_goal_list";

		spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
		spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

		dtgoal = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

	}

	public void get_goal_measurement_Details_View()
	{
		DataSet dtgoal = new DataSet();
		SqlParameter[] spars = new SqlParameter[4];
		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "get_Template_goal_grid_view";

		spars[1] = new SqlParameter("@roleid", SqlDbType.Int);
		spars[1].Value = Convert.ToInt32(lstRoles.SelectedValue);

		spars[2] = new SqlParameter("@period_id", SqlDbType.Int);
		spars[2].Value = Convert.ToInt32(hdnPeriod_id.Value);

		spars[3] = new SqlParameter("@empcode", SqlDbType.VarChar);
		spars[3].Value = Convert.ToString(txtEmpCode.Text);

		dtgoal = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

		dgKRATemplateView.DataSource = null;
		dgKRATemplateView.DataBind();


		if (dtgoal.Tables[0].Rows.Count > 0)
		{
			litrvlgrid.Visible = true;
			dgKRATemplateView.DataSource = dtgoal.Tables[0];
			dgKRATemplateView.DataBind();


		}

	}

	public void get_goal_measurement_Details_New()
	{
		DataSet dtgoal = new DataSet();

		SqlParameter[] spars = new SqlParameter[2];

		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "get_Template_goal_gridview";

		spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
		spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

		dtgoal = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

		dgKRA_Details.DataSource = null;
		dgKRA_Details.DataBind();


		if (dtgoal.Tables[0].Rows.Count > 0)
		{
			dgKRA_Details.DataSource = dtgoal.Tables[0];
			dgKRA_Details.DataBind();


		}

	}

	private DataSet getMeasurements_temp_List(string igoalid)
	{

		DataSet dtmeasurement = new DataSet();
		SqlParameter[] spars = new SqlParameter[3];

		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "get_Template_measurements_temp_list";

		spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
		spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

		spars[2] = new SqlParameter("@Goal_Id", SqlDbType.Int);
		if (Convert.ToString(igoalid).Trim() != "")
			spars[2].Value = Convert.ToInt32(igoalid);
		else
			spars[2].Value = DBNull.Value;


		dtmeasurement = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");


		return dtmeasurement;


	}

	private DataSet getMeasurements_List(string igoalid)
	{

		DataSet dtmeasurement = new DataSet();
		SqlParameter[] spars = new SqlParameter[3];

		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "get_measurements_list";

		spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
		spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

		spars[2] = new SqlParameter("@Goal_Id", SqlDbType.Int);
		if (Convert.ToString(igoalid).Trim() != "")
			spars[2].Value = Convert.ToInt32(igoalid);
		else
			spars[2].Value = DBNull.Value;


		dtmeasurement = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");


		return dtmeasurement;


	}


	private void get_Employee_AssignedList_temp()
	{

		DataSet dtmeasurement = new DataSet();
		SqlParameter[] spars = new SqlParameter[4];

		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "get_temp_employee_assigned_list_temp";

		spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
		spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

		spars[2] = new SqlParameter("@period_id", SqlDbType.Int);
		if (Convert.ToString(hdnPeriod_id.Value).Trim() != "")
			spars[2].Value = Convert.ToInt32(hdnPeriod_id.Value);
		else
			spars[2].Value = DBNull.Value;

		spars[3] = new SqlParameter("@roleid", SqlDbType.Int);
		if (Convert.ToString(hdnKRA_Role_id.Value).Trim() != "")
			spars[3].Value = Convert.ToInt32(hdnKRA_Role_id.Value);
		else
			spars[3].Value = DBNull.Value;

		dtmeasurement = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

		dgAssignedEmployee.DataSource = null;
		dgAssignedEmployee.DataBind();

		if (dtmeasurement.Tables[0].Rows.Count > 0)
		{
			dgAssignedEmployee.DataSource = dtmeasurement.Tables[0];
			dgAssignedEmployee.DataBind();
		}



	}
	private DataSet getMeasurements_list_temp_to_temmp(string igoalid)
	{

		DataSet dtmeasurement = new DataSet();
		SqlParameter[] spars = new SqlParameter[3];

		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "get_Template_measurements_list_temptotemp";

		spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
		spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

		spars[2] = new SqlParameter("@Goal_Id", SqlDbType.Int);
		if (Convert.ToString(igoalid).Trim() != "")
			spars[2].Value = Convert.ToInt32(igoalid);
		else
			spars[2].Value = DBNull.Value;


		dtmeasurement = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");


		return dtmeasurement;


	}

	private void getKRA_Roles()
	{
		//DataSet dsKRARoles = spm.KRA_getRoles(Convert.ToString(txtEmpCode.Text).Trim());

		DataSet dsKRARoles = new DataSet();
		SqlParameter[] spars = new SqlParameter[2];

		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		if (Request.QueryString.Count > 0)
			spars[0].Value = "get_Roles_list";
		else
			spars[0].Value = "get_Roles_list_byemployee";

		spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
		spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();



		dsKRARoles = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

		if (dsKRARoles.Tables[0].Rows.Count > 0)
		{
			lstRoles.DataSource = dsKRARoles.Tables[0];
			lstRoles.DataTextField = "Role_Name";
			lstRoles.DataValueField = "Role_Id";
			lstRoles.DataBind();

		}
		lstRoles.Items.Insert(0, new ListItem("Select Role", "0"));
		lstRoles.Items.Insert(1, new ListItem("Other", "1"));
		if (dsKRARoles.Tables[1].Rows.Count > 0)
		{
			hdnPeriod_id.Value = Convert.ToString(dsKRARoles.Tables[1].Rows[0]["period_id"]);
		}



	}


	public Boolean Check_IsReviwer_IsAssign_ForSelected_Employee(string strempcode)
	{
		DataSet dsChkReviwer = new DataSet();

		Boolean blnCheckReviwer = true;
		SqlParameter[] spars = new SqlParameter[2];

		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "Check_Employee_KRA_Reviwer_Assigned";

		spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
		spars[1].Value = Convert.ToString(strempcode).Trim();

		dsChkReviwer = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

		if (dsChkReviwer.Tables[0].Rows.Count > 0)
		{
			blnCheckReviwer = false;
		}

		return blnCheckReviwer;

	}

	#endregion





	public void delete_KRAdata_temptable()
	{
		#region   Delete Temp KRA details
		DataSet dsList = new DataSet();
		SqlParameter[] spars1 = new SqlParameter[3];

		spars1[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars1[0].Value = "delete_GoalMeasurements_Temp_Template_other";

		spars1[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
		spars1[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

		spars1[2] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
		if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
			spars1[2].Value = Convert.ToDecimal(hdnKRA_id.Value);
		else
			spars1[2].Value = DBNull.Value;

		dsList = spm.getDatasetList(spars1, "SP_KRA_GETALL_DETAILS");

		#endregion
	}




	protected void LinkButton1_Click(object sender, EventArgs e)
	{
		//if(Convert.ToString(hdnPeriod_id.Value).Trim()=="")
		//{
		//    lblmessage.Text = "KRA current period is not active.Please contact to admin";
		//    return;
		//}


		lblEmployee_Reviwee_ErrorMsg.Text = "";
		foreach (ListItem item in ddl_Employees.Items)
		{
			if (item.Selected == true)
			{
				if (Check_IsReviwer_IsAssign_ForSelected_Employee(Convert.ToString(item.Value).Trim()) == true)
				{
					lblEmployee_Reviwee_ErrorMsg.Text = "Reviewer not assigned for " + item.Text + ".please contact to HR.";
					item.Selected = false;
					return;
				}
			}

		}

		string strempcode = "";
		foreach (ListItem item in ddl_Employees.Items)
		{
			if (item.Selected == true)
			{
				if (item.Value != "" && item.Value != "0")
				{
					strempcode = item.Value; // "'" + item.Value + "'";                   
				}

				DataSet dtmeasurement = new DataSet();
				SqlParameter[] spars = new SqlParameter[6];

				spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
				spars[0].Value = "Insert_Template_to_Employee_Temp";

				spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
				spars[1].Value = Convert.ToString(strempcode).Trim();

				spars[2] = new SqlParameter("@period_id", SqlDbType.Int);
				spars[2].Value = Convert.ToInt32(hdnPeriod_id.Value);

				spars[3] = new SqlParameter("@Assigned_By", SqlDbType.VarChar);
				spars[3].Value = Convert.ToString(txtEmpCode.Text).Trim();



				dtmeasurement = spm.getDatasetList(spars, "SP_KRA_Insert_Update");
			}
		}

		get_Employee_AssignedList_temp();
		get_Role_Employee_List();
	}

	protected void lnk_Atend_Delete_Click(object sender, ImageClickEventArgs e)
	{
		try
		{
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			var empcode = Convert.ToString(dgAssignedEmployee.DataKeys[row.RowIndex].Values[0]).Trim();

			#region delete employee from temp
			DataSet dtmeasurement = new DataSet();
			SqlParameter[] spars = new SqlParameter[3];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "delete_temp_employee_assigned";

			spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(empcode).Trim();

			spars[2] = new SqlParameter("@Assigned_By", SqlDbType.VarChar);
			spars[2].Value = Convert.ToString(txtEmpCode.Text).Trim();

			dtmeasurement = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

			get_Employee_AssignedList_temp();
			get_Role_Employee_List();

			#endregion



		}
		catch (Exception ex)
		{

		}
	}

	protected void dgKRA_Details_DataBound(object sender, EventArgs e)
	{



		for (int currentRowIndex = 0; currentRowIndex < dgKRA_Details.Rows.Count; currentRowIndex++)
		{
			GridViewRow currentRow = dgKRA_Details.Rows[currentRowIndex];
			CombineColumnCells(currentRow, 0, "Goal_Id");
			CombineColumnCells(currentRow, 1, "Goal_Id");
			CombineColumnCells(currentRow, 2, "Goal_Id");
			CombineColumnCells(currentRow, 7, "Goal_Id");
			// CombineColumnCells(currentRow, 8, "Goal_Id");
		}
	}

	protected void dgKRATemplateView_DataBound(object sender, EventArgs e)
	{
		for (int currentRowIndex = 0; currentRowIndex < dgKRATemplateView.Rows.Count; currentRowIndex++)
		{
			GridViewRow currentRow = dgKRATemplateView.Rows[currentRowIndex];
			CombineColumnCells_View(currentRow, 0, "Goal_Id");
			CombineColumnCells_View(currentRow, 1, "Goal_Id");
			CombineColumnCells_View(currentRow, 2, "Goal_Id");
		}
	}


	public void setVisible_Cntrl(bool blnflg)
	{

		liGoalTitle.Visible = blnflg;
		liWeightage.Visible = blnflg;
		liblank1.Visible = blnflg;

		ligoadexcription.Visible = blnflg;
		liblank2.Visible = blnflg;
		liblank3.Visible = blnflg;

		ligoalSeq.Visible = blnflg;
		liblank4.Visible = blnflg;
		liblank5.Visible = blnflg;

		litrvldetail.Visible = blnflg;
		liblank6.Visible = blnflg;
		liblank7.Visible = blnflg;
		// DivTrvl.Visible = blnflg;
		li1.Visible = blnflg;

		litrvlgrid.Visible = blnflg;
		btnback_mng.Visible = blnflg;
	}

	public void setVisible_GoalCntrols(bool blnflg)
	{
		liGoalTitle.Visible = blnflg;
		liWeightage.Visible = blnflg;
		liblank1.Visible = blnflg;

		ligoadexcription.Visible = blnflg;
		liblank2.Visible = blnflg;
		liblank3.Visible = blnflg;

		ligoalSeq.Visible = blnflg;
		liblank4.Visible = blnflg;
		liblank5.Visible = blnflg;

		trvldeatils_delete_btn.Visible = blnflg;
		btnback_mng.Visible = blnflg;
	}

	public void setVisible_MeasurementsCntrols(bool blnflg)
	{
		litrvldetail.Visible = blnflg;
		liblank6.Visible = blnflg;
		liblank7.Visible = blnflg;
	}




	public void check_Selected_Roles()
	{
		#region Check Role is Created by Login Employee

		DataSet dtmeasurement = new DataSet();
		SqlParameter[] spars = new SqlParameter[4];

		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "check_IsCreate_Role";

		spars[1] = new SqlParameter("@roleid", SqlDbType.Int);
		spars[1].Value = Convert.ToInt32(lstRoles.SelectedValue);

		spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
		spars[2].Value = Convert.ToString(txtEmpCode.Text);

		spars[3] = new SqlParameter("@period_id", SqlDbType.Int);
		spars[3].Value = Convert.ToInt32(hdnPeriod_id.Value);


		dtmeasurement = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

		if (dtmeasurement.Tables[0].Rows.Count > 0)
		{
			lblmessage.Text = "";
			//btnback_mng.Visible = true;
			trvl_btnSave.Visible = true;
			btnCancel.Visible = true;

			idRoleCreatedby.Visible = true;
			txtRoleCreatedBy.Visible = true;
			liRoleCreatedBy.Visible = true;
			liRoleCreatedBy_2.Visible = true;
			liRoleCreatedBy_3.Visible = true;

			dgKRATemplateView.DataSource = null;
			dgKRATemplateView.DataBind();
			accmo_delete_btn.Visible = false;

			setVisible_Cntrl(true);

			txtRoleCreatedBy.Text = Convert.ToString(dtmeasurement.Tables[0].Rows[0]["Emp_Name"]);

			//if (Convert.ToString(hdnKRA_id.Value).Trim() == "")
			//{
			if (dtmeasurement.Tables[2].Rows.Count > 0)
			{

				hdnKRA_id.Value = Convert.ToString(dtmeasurement.Tables[2].Rows[0]["Temp_KRA_ID"]).Trim();
				hdnstype_Main.Value = "update";
				if (Convert.ToBoolean(dtmeasurement.Tables[2].Rows[0]["IsDraft"]) == false)
				{
					btnCancel.Visible = false;
				}
				setVisible_Cntrl(true);
				#region   Delete Temp KRA details
				DataSet dsList = new DataSet();
				SqlParameter[] spars1 = new SqlParameter[3];

				spars1[0] = new SqlParameter("@stype", SqlDbType.VarChar);
				spars1[0].Value = "delete_GoalMeasurements_Temp_Template";

				spars1[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
				spars1[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

				spars1[2] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
				if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
					spars1[2].Value = Convert.ToDecimal(hdnKRA_id.Value);
				else
					spars1[2].Value = DBNull.Value;

				dsList = spm.getDatasetList(spars1, "SP_KRA_GETALL_DETAILS");

				#endregion

				get_goal_measurement_Details_New();
				//get_Role_Employee_List();
				get_Employee_AssignedList_temp();
				// }
			}
		}
		else
		{

			lblmessage.Text = "KRA Template not created for this selected role.";
			btnback_mng.Visible = false;
			trvl_btnSave.Visible = false;
			accmo_delete_btn.Visible = false;
			setVisible_Cntrl(false);


			idRoleCreatedby.Visible = true;
			txtRoleCreatedBy.Visible = true;
			liRoleCreatedBy.Visible = true;
			liRoleCreatedBy_2.Visible = true;
			liRoleCreatedBy_3.Visible = true;
			txtRoleCreatedBy.Text = Convert.ToString(dtmeasurement.Tables[1].Rows[0]["Emp_Name"]);
            HDRoleCreatedBy.Value = Convert.ToString(dtmeasurement.Tables[1].Rows[0]["Role_Created_By"]);

            SPNewRole.Visible = true;
			txt_NewRoleName.Visible = true;
			lnk_CopyKRA.Visible = true;


			//if Template create for Selected row then show on Grid
			if (dtmeasurement.Tables[2].Rows.Count > 0)
			{
				lblmessage.Text = "";
				idRoleCreatedby.InnerText = "Role Template Created By";


				hdnKRA_id.Value = Convert.ToString(dtmeasurement.Tables[2].Rows[0]["Temp_KRA_ID"]).Trim();

				get_goal_measurement_Details_View();
				get_Role_Employee_List();
				get_Employee_AssignedList_temp();
				accmo_delete_btn.Visible = true;


				litrvlgrid.Visible = true;


			}
		}

		#endregion


		#region Check KRA Template available for edit if any employee not submit KRA
		DataSet dsList_1 = new DataSet();
		SqlParameter[] spars2 = new SqlParameter[4];

		spars2[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars2[0].Value = "check_ForTemplateEdit_KRACreated";

		spars2[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
		spars2[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

		spars2[2] = new SqlParameter("@roleid", SqlDbType.Int);
		spars2[2].Value = Convert.ToInt32(lstRoles.SelectedValue);

		spars2[3] = new SqlParameter("@period_id", SqlDbType.Int);
		spars2[3].Value = Convert.ToInt32(hdnPeriod_id.Value);

		dsList_1 = spm.getDatasetList(spars2, "SP_KRA_GETALL_DETAILS");
		if (dsList_1.Tables[0].Rows.Count > 0)
		{
			liGoalTitle.Visible = false;
			liWeightage.Visible = false;
			liblank1.Visible = false;
			ligoadexcription.Visible = false;
			liblank2.Visible = false;
			liblank3.Visible = false;
			ligoalSeq.Visible = false;
			liblank4.Visible = false;
			liblank5.Visible = false;

			litrvldetail.Visible = false;
			liblank6.Visible = false;
			liblank7.Visible = false;

			dgKRATemplateView.Visible = false;
			btnTra_Details.Visible = false;
			spntrvldtls.Visible = false;
			btnCancel.Visible = false;
			if (dsList_1.Tables[2].Rows.Count > 0)
			{
				foreach (GridViewRow row in dgKRA_Details.Rows)
				{
					LinkButton lnkedit_goal = (LinkButton)row.FindControl("lnkedit_goal");
					lnkedit_goal.Visible = false;
				}
			}

		}

		#endregion
	}


	private void getKRA_Weightage()
	{
		#region Check Role is Created by Login Employee

		DataSet dtmeasurement = new DataSet();
		SqlParameter[] spars = new SqlParameter[4];

		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "get_Temp_KRA_Weightage";

		spars[1] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
		spars[1].Value = Convert.ToDecimal(hdnKRA_id.Value);


		dtmeasurement = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

		if (dtmeasurement.Tables[0].Rows.Count > 0)
		{
			if (Convert.ToString(dtmeasurement.Tables[0].Rows[0]["Weightage"]).Trim() != "")
			{
				if (Convert.ToInt32(dtmeasurement.Tables[0].Rows[0]["Weightage"]) < 100)
				{
					setVisible_Cntrl(true);
				}

			}

		}

		#endregion
	}


	protected void txtRoleName_TextChanged(object sender, EventArgs e)
	{
		if (Convert.ToString(lstRoles.SelectedValue).Trim() == "1")
		{
			if (Convert.ToString(txtRoleName.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter New Role Name";
				return;
			}

			#region check Role name already exsist
			DataSet dtmeasurement = new DataSet();
			SqlParameter[] spars = new SqlParameter[2];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "check_Role_Name_AlreadyExists";

			spars[1] = new SqlParameter("@RoleName", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(txtRoleName.Text).Trim();


			dtmeasurement = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

			if (dtmeasurement.Tables[0].Rows.Count > 0)
			{
				txtRoleName.Text = "";
				lblmessage.Text = "Role with same name already exist ! Please create Different Role name or you can select the Role from the dropdown.";
				return;
			}
			#endregion
		}
	}

	protected void lstRoles_SelectedIndexChanged(object sender, EventArgs e)
	{

		idOtherRole.Visible = false;
		idspnOtherRole.Visible = false;
		txtRoleName.Visible = false;
		idRoleCreatedby.Visible = false;

		txtRoleCreatedBy.Visible = false;
		txtRoleCreatedBy.Text = "";

		liRoleCreatedBy.Visible = false;
		liRoleCreatedBy_2.Visible = false;
		liRoleCreatedBy_3.Visible = false;

		hdnKRA_id.Value = "";
		hdngoal_id.Value = "";
		hdnKRA_id.Value = "";
		hdnstype_Main.Value = "";
		btnCancel.Visible = false;


		dgAssignedEmployee.DataSource = null;
		dgAssignedEmployee.DataBind();
		dgKRA_Details.DataSource = null;
		dgKRA_Details.DataBind();
		dgMeasurementslist.DataSource = null;
		dgMeasurementslist.DataBind();
		dgKRATemplateView.DataSource = null;
		dgKRATemplateView.DataBind();

		if (Convert.ToString(lstRoles.SelectedValue).Trim() == "1")
		{
			idOtherRole.Visible = true;
			idspnOtherRole.Visible = true;
			txtRoleName.Visible = true;
			hdnKRA_Role_id.Value = "";
			setVisible_Cntrl(true);
			delete_KRAdata_temptable();
			btnback_mng.Visible = true;
			trvl_btnSave.Visible = true;
			txtRoleName.Text = "";
			btnCancel.Visible = true;

		}
		if (Convert.ToInt32(lstRoles.SelectedValue) > 1)
		{
			hdnKRA_Role_id.Value = lstRoles.SelectedValue;
			delete_KRAdata_temptable();
			#region Check Role is Created by Login Employee

			DataSet dtmeasurement = new DataSet();
			SqlParameter[] spars = new SqlParameter[4];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "check_IsCreate_Role";

			spars[1] = new SqlParameter("@roleid", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(lstRoles.SelectedValue);

			spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
			spars[2].Value = Convert.ToString(txtEmpCode.Text);

			spars[3] = new SqlParameter("@period_id", SqlDbType.Int);
			spars[3].Value = Convert.ToInt32(hdnPeriod_id.Value);


			dtmeasurement = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
			if (dtmeasurement.Tables[0].Rows.Count > 0)
			{
				lblmessage.Text = "";
				//btnback_mng.Visible = true;
				trvl_btnSave.Visible = true;
				btnCancel.Visible = true;

				idRoleCreatedby.Visible = true;
				txtRoleCreatedBy.Visible = true;
				liRoleCreatedBy.Visible = true;
				liRoleCreatedBy_2.Visible = true;
				liRoleCreatedBy_3.Visible = true;

				dgKRATemplateView.DataSource = null;
				dgKRATemplateView.DataBind();
				accmo_delete_btn.Visible = false;
			}

			#endregion
		}


	}


	protected void lstRoles_1_SelectedIndexChanged(object sender, EventArgs e)
	{
		idOtherRole.Visible = false;
		idspnOtherRole.Visible = false;
		txtRoleName.Visible = false;
		idRoleCreatedby.Visible = false;

		txtRoleCreatedBy.Visible = false;
		txtRoleCreatedBy.Text = "";

		liRoleCreatedBy.Visible = false;
		liRoleCreatedBy_2.Visible = false;
		liRoleCreatedBy_3.Visible = false;

		hdnKRA_id.Value = "";
		hdngoal_id.Value = "";
		hdnKRA_id.Value = "";
		hdnstype_Main.Value = "";
		txt_NewRoleName.Text = "";
		txt_NewRoleName.Visible = false;
		SPNewRole.Visible = false;
		lnk_CopyKRA.Visible = false;
		btnCancel.Visible = false;


		dgAssignedEmployee.DataSource = null;
		dgAssignedEmployee.DataBind();
		dgKRA_Details.DataSource = null;
		dgKRA_Details.DataBind();
		dgMeasurementslist.DataSource = null;
		dgMeasurementslist.DataBind();
		dgKRATemplateView.DataSource = null;
		dgKRATemplateView.DataBind();


		if (Convert.ToString(lstRoles.SelectedValue).Trim() == "1")
		{
			idOtherRole.Visible = true;
			idspnOtherRole.Visible = true;
			txtRoleName.Visible = true;
			hdnKRA_Role_id.Value = "";
			setVisible_Cntrl(true);
			delete_KRAdata_temptable();
			btnback_mng.Visible = true;
			trvl_btnSave.Visible = true;
			txtRoleName.Text = "";
			btnCancel.Visible = true;

		}

		if (Convert.ToInt32(lstRoles.SelectedValue) > 1)
		{
			hdnKRA_Role_id.Value = lstRoles.SelectedValue;
			delete_KRAdata_temptable();
			check_Selected_Roles();

			#region Check Template create for this selected role

			//DataSet dtmeasurement = new DataSet();
			//SqlParameter[] spars = new SqlParameter[2];

			//spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			//spars[0].Value = "check_IsKRATemplate_Create_ForRole";

			//spars[1] = new SqlParameter("@department_id", SqlDbType.Int);
			//spars[1].Value = Convert.ToInt32(lstRoles.SelectedValue);  

			//dtmeasurement = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

			//if(dtmeasurement.Tables[0].Rows.Count>0)
			//{
			//    lblmessage.Text = "KRA Template is created for this selected role.";
			//    btnback_mng.Visible = false;
			//    trvl_btnSave.Visible = false;

			//}
			#endregion


		}




	}



	protected void dgKRA_Details_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{

			int totalgoalid = Convert.ToInt32(e.Row.Cells[0].Text);

			if (totalgoalid == 99)
			{
				LinkButton lnkedit_goal = (LinkButton)e.Row.FindControl("lnkedit_goal");
				lnkedit_goal.Visible = false;
				e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#4F81BD");
				e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
				e.Row.Cells[0].ForeColor = System.Drawing.ColorTranslator.FromHtml("#4F81BD");
				e.Row.Cells[0].Text = "";
			}

		}
	}

	protected void dgKRATemplateView_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			int totalgoalid = Convert.ToInt32(e.Row.Cells[0].Text);

			if (totalgoalid == 99)
			{
				e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#4F81BD");
				e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
				e.Row.Cells[0].ForeColor = System.Drawing.ColorTranslator.FromHtml("#4F81BD");
				e.Row.Cells[0].Text = "";
			}

		}
	}

	protected void txt_Weightage_TextChanged(object sender, EventArgs e)
	{
		if (Convert.ToString(txt_Weightage.Text).Trim() != "")
		{
			Decimal dfare = 0;
			string[] strdate;
			strdate = Convert.ToString(txt_Weightage.Text).Trim().Split('.');
			if (strdate.Length > 2)
			{
				txt_Weightage.Text = "";
				lbl_goal_msg.Text = "Please enter correct Weightage.";
				return;
			}

			if (strdate.Length > 1)
			{
				if (strdate[1].Length > 2)
				{
					txt_Weightage.Text = "";
					lbl_goal_msg.Text = "Please enter correct Weightage.";
					return;
				}
			}

			dfare = Convert.ToDecimal(txt_Weightage.Text);
			if (dfare == 0)
			{
				txt_Weightage.Text = "";
				lbl_goal_msg.Text = "Please enter correct Weightage.";
				return;
			}

			if (dfare > 100)
			{
				txt_Weightage.Text = "";
				lbl_goal_msg.Text = "Please enter correct Weightage.";
				return;
			}
		}

	}

	protected void txt_NewRoleName_TextChanged(object sender, EventArgs e)
	{
		if (Convert.ToString(lstRoles.SelectedValue).Trim() != "1")
		{
			if (Convert.ToString(txt_NewRoleName.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter New Role Name";
				return;
			}

			#region check Role name already exsist
			DataSet dtmeasurement = new DataSet();
			SqlParameter[] spars = new SqlParameter[2];
			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "check_Role_Name_AlreadyExists";
			spars[1] = new SqlParameter("@RoleName", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(txt_NewRoleName.Text).Trim();
			dtmeasurement = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

			if (dtmeasurement.Tables[0].Rows.Count > 0)
			{
				txt_NewRoleName.Text = "";
				lblmessage.Text = "Role with same name already exist ! Please create Different Role name.";
				return;
			}
			#endregion
		}
	}

	protected void lnk_CopyKRA_Click(object sender, EventArgs e)
	{
		string confirmValue = hdnYesNo.Value.ToString();
		if (confirmValue != "Yes")
		{
			return;
		}

		if (Convert.ToString(lstRoles.SelectedValue).Trim() == "0")
		{
			lblmessage.Text = "Please Select Role Name";
			return;
		}

		if (Convert.ToString(lstRoles.SelectedValue).Trim() != "1")
		{
			if (Convert.ToString(txt_NewRoleName.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter New Role Name";
				return;
			}
		}

		//if (dgKRA_Details.Rows.Count <= 0)
		//{
		//	lblmessage.Text = "Please enter Goal and Measurements details";
		//	return;
		//}

		#region validations

		//check goal title already exist 
		DataSet tdsgoal = new DataSet();
		tdsgoal = check_goal_already_exist();
		if (tdsgoal.Tables.Count > 0)
		{
			//// //check  Weightage not less than 100
			if (tdsgoal.Tables[1].Rows.Count > 0)
			{
				if (Convert.ToString(tdsgoal.Tables[1].Rows[0]["Weightage"]).Trim() != "")
				{
					Int32 iWeightageCnt = Convert.ToInt32(tdsgoal.Tables[1].Rows[0]["Weightage"]);

					if (iWeightageCnt < 100)
					{
						lbl_goal_msg.Text = "Please make sure total of weightage is 100 and then try again to Submit";
						return;
					}
				}
			}
		}
		#endregion
		CopyKRA(false);
	}

	protected void Lnk_Export_Click(object sender, EventArgs e)
	{
		try
		{
			lblmessage.Text = "";
			   string confirmValue = hdnYesNo.Value.ToString();
			if (confirmValue != "Yes")
			{
				return;
			}

			if (Convert.ToString(lstRoles.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please Select Role Name";
				return;
			}


            DataSet dtgoal = new DataSet();

            if (Request.QueryString.Count > 0)
            {
                SqlParameter[] spars1 = new SqlParameter[2];
                spars1[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                spars1[0].Value = "get_Template_goal_Export";

                spars1[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
                spars1[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

                dtgoal = spm.getDatasetList(spars1, "SP_KRA_GETALL_DETAILS");
            }
            else
            {
                SqlParameter[] spars = new SqlParameter[4];

                spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                spars[0].Value = "get_Template_goal_grid_view";

                spars[1] = new SqlParameter("@roleid", SqlDbType.Int);
                spars[1].Value = Convert.ToInt32(lstRoles.SelectedValue);

                spars[2] = new SqlParameter("@period_id", SqlDbType.Int);
                spars[2].Value = Convert.ToInt32(hdnPeriod_id.Value);

                spars[3] = new SqlParameter("@empcode", SqlDbType.VarChar);
                spars[3].Value = Convert.ToString(txtEmpCode.Text);

                dtgoal = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
            }

            DataTable newIRSummary = new DataTable();


            if (dtgoal.Tables[0].Rows.Count > 0)
			{
				string RoleName = "";
				if (Convert.ToString(lstRoles.SelectedValue).Trim() == "1")
				{
					RoleName = Convert.ToString(txtRoleName.Text);
				}
				else
				{
					RoleName = Convert.ToString(lstRoles.SelectedItem.Text).Trim();
				}
				LocalReport ReportViewer1 = new LocalReport();
				string filename = "KRA_Tempate";
				//string dsysdate = DateTime.Now.ToString("ddMMyyyy_HHmmss");
				filename = filename + "_" + RoleName + ".xls";
				ReportViewer1.ReportPath = Server.MapPath("~/procs/Kra_GoalExport.rdlc");

                string Name = "";
                if (Request.QueryString.Count > 0)
                {
                     Name = Convert.ToString("<b>Role Name</b> : " + RoleName) + "&nbsp;&nbsp;&nbsp;&nbsp;" + Convert.ToString(" <b>Emp Name</b> : ") + Convert.ToString(Session["emp_loginName"]);
                }
                else
                {
                    Name = Convert.ToString("<b>Role Name</b> : " + RoleName) + "&nbsp;&nbsp;&nbsp;&nbsp;" + Convert.ToString(" <b>Emp Name</b> : ") + Convert.ToString(txtRoleCreatedBy.Text.Trim());
                }

                ReportParameter[] param = new ReportParameter[1];
				param[0] = new ReportParameter("RoleName", Name);
				//param[1] = new ReportParameter("Emp_Name", Convert.ToString(""));
				ReportDataSource rds = new ReportDataSource("Kra_GoalDetails", dtgoal.Tables[0]);
				ReportViewer1.DataSources.Clear();
				ReportViewer1.DataSources.Add(rds);
				ReportViewer1.SetParameters(param);
				Warning[] warnings;
				string[] streamIds;
				string contentType = string.Empty;
				string encoding = string.Empty;
				string extension = string.Empty;
				byte[] bytes = ReportViewer1.Render("EXCEL", null, out contentType, out encoding, out extension, out streamIds, out warnings);
				Response.Clear();
				Response.Buffer = true;
				Response.ContentType = contentType;
				Response.AddHeader("content-disposition", "attachment; filename=" + filename);

				Response.BinaryWrite(bytes);
				Response.Flush();
				Response.End();
				#region Export to Excel
				//	newIRSummary = new DataTable();
				//	newIRSummary.Columns.Add("Sr.No");
				//	newIRSummary.Columns.Add("Goal Title");
				//	newIRSummary.Columns.Add("Weightage");
				//	newIRSummary.Columns.Add("Measurement Details");
				//	newIRSummary.Columns.Add("Unit");
				//	newIRSummary.Columns.Add("Quantity");

				//	foreach (DataRow item in dtgoal.Tables[0].Rows)
				//	{
				//		DataRow _dr = newIRSummary.NewRow();
				//		_dr["Sr.No"] = item["goal_seq_no"].ToString();
				//		_dr["Goal Title"] = item["Goal_title"].ToString();
				//		_dr["Weightage"] = item["Weightage"].ToString();
				//		_dr["Measurement Details"] = item["Measurement_Details"].ToString();
				//		_dr["Unit"] = item["unit_short_name"].ToString();
				//		_dr["Quantity"] = item["Quantity"].ToString();

				//		newIRSummary.Rows.Add(_dr);
				//	}
				//	string RoleName = "";
				//	if (Convert.ToString(lstRoles.SelectedValue).Trim() == "1")
				//	{
				//		RoleName = Convert.ToString(txtRoleName.Text);
				//	}
				//	else
				//	{
				//		RoleName = Convert.ToString(lstRoles.SelectedItem.Text).Trim();
				//	}
				//	var aCode = 65;
				//	var excelName = "KRA_Template_" + RoleName;
				//	using (XLWorkbook wb = new XLWorkbook())
				//	{

				//		var ws = wb.Worksheets.Add("KRA Goal Details");

				//		ws.Cell(1, 2).Value = "Role Name : " + RoleName;
				//		//ws.Cell(2, 2).Style.Font.Bold = true;
				//		//ws.Cell(2, 3).Value = lstRoles.SelectedItem.Text;

				//		ws.Cell(3, 2).Value = "Employee  Name : " +Convert.ToString(Session["emp_loginName"]);
				//		//ws.Cell(3, 2).Style.Font.Bold = true;
				//		//ws.Cell(3, 2).Value = Convert.ToString(Session["emp_loginName"]);

				//		ws.Cell(5, 2).Value = "Goal Details";
				//		ws.Cell(5, 2).Style.Font.Bold = true;
				//		ws.Cell(5, 2).Style.Font.SetFontSize(14);
				//		ws.Cell(5, 2).Style.Fill.BackgroundColor = XLColor.BlueBell;
				//		//ws.Cell(4, 2).Characters[start_pos, len].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);

				//		int rowIndex = 5; int i = 1;
				//		int columnIndex = 0;
				//		rowIndex = rowIndex + 1;
				//		columnIndex = 0;
				//		int j = 1;
				//		foreach (DataColumn column in newIRSummary.Columns)
				//		{
				//			ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Value = column.ColumnName;
				//			ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Font.Bold = true;
				//			ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
				//			ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Fill.BackgroundColor = XLColor.Gainsboro;
				//			ws.Column(1).Width = 5;
				//			ws.Column(j).Width = 15;
				//			ws.Column(2).Width = 50;
				//			ws.Column(4).Width = 50;
				//			columnIndex++; j++;
				//		}

				//		int RowID = (rowIndex + newIRSummary.Rows.Count);
				//		rowIndex++;
				//		System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
				//		foreach (DataRow row in newIRSummary.Rows)
				//		{
				//			int valueCount = 0;
				//			foreach (object rowValue in row.ItemArray)
				//			{
				//				//ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).is
				//				ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Value = HttpUtility.HtmlDecode(Convert.ToString(rowValue));//rx.Replace(rowValue.ToString(), ""); //StripHTML(Convert.ToString(rowValue));
				//				ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
				//				ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Style.Alignment.WrapText = true;
				//				ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), RowID)).Style.Fill.BackgroundColor = XLColor.Yellow;
				//				ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), RowID)).Style.Font.Bold = true;
				//				ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), RowID)).RichText. = true;
				//				valueCount++;
				//			}
				//			//ws.Row(16).Style.Fill.BackgroundColor = XLColor.Green;
				//			rowIndex++;
				//		}
				//		Response.Clear();
				//		Response.Buffer = true;
				//		Response.Charset = "";
				//		Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
				//		Response.AddHeader("content-disposition", "attachment;filename=" + excelName + ".xlsx");
				//		using (MemoryStream MyMemoryStream = new MemoryStream())
				//		{
				//			wb.SaveAs(MyMemoryStream);
				//			MyMemoryStream.WriteTo(Response.OutputStream);
				//			Response.Flush();
				//			Response.End();
				//		}
				//	}


				#endregion
			}
			else
			{
				lblmessage.Text = "Record Not Found....";
				
			}

			}
				catch (Exception)
		{

			throw;
		}
	}
	
	
}