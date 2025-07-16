using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;


public partial class procs_Check_In_Out_New : System.Web.UI.Page
{
	public SqlDataAdapter sqladp;
	public static string dob1;
	public string userid;
	public static string pimg = "";
	public static string cimg = "";
	public string loc = "", dept = "", subdept = "", desg = "", Wsch = "";
	public int did = 0;
	LeaveBalance bl = new LeaveBalance();
	SP_Methods spm = new SP_Methods();
	Leave_Request_Parameters lpm = new Leave_Request_Parameters();
	public DataTable dtEmp, dtleavebal, dtApprover, dtApproverEmailIds, dtMaxRequestId, dtHolidays, dtleavedetails;
	public int Leaveid;
	public int leavetype, openbal, avail, rembal, weekendcount, publicholiday, apprid;
	double totaldays;
	public string filename = "", approveremailaddress, message;

	bool blnisLastworkingdatePassed = false;

	protected void lnkhome_Click(object sender, EventArgs e)
	{
		Response.Redirect(ReturnUrl("sitepathmain") + "default");
	}
	public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
			}

			lblmessage.Text = "";

			if (Convert.ToString(Session["Empcode"]).Trim() == "")
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

			lpm.Emp_Code = Session["Empcode"].ToString();
			txtEmpCode.Text = lpm.Emp_Code;
			lblmessage.Visible = true;
			if (Page.User.Identity.IsAuthenticated == false)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
			}
			else
			{
				Page.SmartNavigation = true;
				if (!Page.IsPostBack)
				{
					hdnReqid.Value = "";
					hdnTypeId.Value = "0";
					get_Shiftdetails();
					get_CheckIn();
					txtFromdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
					btnback_mng.Visible = false;
					txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					hdnlstfromfor.Value = "Full Day";
					hdnlsttofor.Value = "Full Day";

					editform.Visible = true;
					divbtn.Visible = false;
					lblmessage.Text = "";
					this.lstApprover.SelectedIndex = 0;

					hdnleaveconditiontypeid.Value = "12";
					this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
					SetAttTimeSheetToday();
					GetDeploymentTypeStatus();

			 		 PopulateEmployeeData();
                    			 if (blnisLastworkingdatePassed == true)
			                  {
                        				lblmessage.Text = "Check-In/ Check-Out not allowed.Your Last working date has passed.";                       
				                        btnIn.Visible = false;
                    			  }
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}
	#region page Methods

     public void PopulateEmployeeData()
     {
        try
        {  
            //btnSave.Enabled = false; 
            dtEmp = spm.GetEmployeeData(lpm.Emp_Code);

            if (dtEmp.Rows.Count > 0)
            {
                blnisLastworkingdatePassed = false;

                if (Convert.ToString(dtEmp.Rows[0]["IsLastWorkingDatePast"]).Trim() == "yes")
                    blnisLastworkingdatePassed = true;                
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

     }

	public void get_Shiftdetails()
	{
		try
		{
			DataSet dtTrDetails = new DataSet();
			SqlParameter[] spars = new SqlParameter[2];
			TimeSpan Instart = new TimeSpan();
			TimeSpan CurTime = new TimeSpan();

			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = "Get_details";
			spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
			spars[1].Value = lpm.Emp_Code.ToString();
			dtTrDetails = spm.getDatasetList(spars, "[SP_Admin_Shift_Change]");
			if (dtTrDetails.Tables[0].Rows.Count > 0)
			{
				In_range.Text = "Shift In Time Range: " + Convert.ToString(dtTrDetails.Tables[0].Rows[0]["In_range"]);
				Out_range.Text = "Shift Out Time Range: " + Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Out_range"]);
				Instart = TimeSpan.Parse(Convert.ToString(dtTrDetails.Tables[0].Rows[0]["In_Start"]));
				CurTime = TimeSpan.Parse(DateTime.Now.TimeOfDay.ToString());
				var getFullHr = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["FullDay_Hrs"]).Split(':')[0];
				var getFullHr1 = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["FullDay_Hrs"]).Split(':')[1];
				var getHalfHr = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["HalfDay_Hrs"]).Split(':')[0];
				var getHalfHr1 = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["HalfDay_Hrs"]).Split(':')[1];
				var getHalfTime = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Half_Time"]).Split(':')[0] + ":" + Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Half_Time"]).Split(':')[1];
				hdnHalfTime.Value = getHalfTime;
				if (getHalfHr1 == "00")
				{
					getHalfHr1 = "";
				}
				else
				{
					getHalfHr1 = ":" + getHalfHr1;
				}

				if (getFullHr1 == "00")
				{
					getFullHr1 = "";
				}
				else
				{
					getFullHr1 = ":" + getFullHr1;
				}
				Label1.Text = "* Shift Full Day  " + getFullHr + "" + getFullHr1 + " Hours & Half Day " + getHalfHr + "" + getHalfHr1 + " Hours & Shift Half day Time:-" + getHalfTime;
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());

		}
	}
	public void get_CheckIn()
	{
		try
		{
			Txt_InTime.Visible = false;
			Txt_OutTime.Visible = false;
			hdnisTimeInShow.Value = "0";
			hdnisTimeoutShow.Value = "0";
			DataSet dtTrDetails = new DataSet();
			SqlParameter[] spars = new SqlParameter[2];
			TimeSpan Instart = new TimeSpan();
			TimeSpan CurTime = new TimeSpan();

			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = "GetEmpAtt_New_List";//Get_CheckIn
			spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
			spars[1].Value = lpm.Emp_Code.ToString();
			dtTrDetails = spm.getDatasetList(spars, "[SP_Attendance]");
			if (dtTrDetails.Tables[0].Rows.Count > 0)
			{
				Txt_CheckIn.Visible = true;
				Txt_InTime.Visible = false;
				Txt_CheckIn.Text = "Checked In at: " + Convert.ToString(dtTrDetails.Tables[0].Rows[0]["att_time"]);
				btnIn.Enabled = false;
				btnIn.Visible = false;
				hdnisTimeInShow.Value = "1";
				liout.Visible = true;
				//btnBack.Visible = true;
				if (dtTrDetails.Tables[0].Rows.Count > 1)
				{
					Txt_CheckOut.Visible = true;
					Txt_OutTime.Visible = false;
					Txt_CheckOut.Text = "Checked Out at: " + Convert.ToString(dtTrDetails.Tables[0].Rows[1]["att_time"]);
					btnBack.Enabled = false;
					btnBack.Visible = false;
					hdnisTimeoutShow.Value = "1";
					
				}
	
			}
			else
			{
				btnIn.Enabled = true;
				btnBack.Visible = false;
				liout.Visible = false;
				hdnisTimeoutShow.Value = "1";
			}
			if (dtTrDetails.Tables[1].Rows.Count > 0)
			{
				DgvApprover.DataSource = dtTrDetails.Tables[1];
				DgvApprover.DataBind();
			}

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());

		}
	}
	private void SetAttTimeSheetToday()
	{
		DataSet dtTrDetails = new DataSet();
		SqlParameter[] spars = new SqlParameter[2];
		spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
		spars[0].Value = "GET_WORKSCHEDULE_NEW";//"GETWORKSCHEDULE";
		spars[1] = new SqlParameter("@Emp_code", SqlDbType.VarChar);
		if (lpm.Emp_Code.ToString() == "")
			spars[1].Value = DBNull.Value;
		else
			spars[1].Value = lpm.Emp_Code.ToString();
		dtTrDetails = spm.getDatasetList(spars, "SP_Attendance_CountLeave");
		if (dtTrDetails.Tables[0].Rows.Count > 0)
		{
			var getToday = DateTime.Now.DayOfWeek.ToString();
			var getDays = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["OFF_DAYS_NAME"]);
			var splitData = getDays.Split(',');
			foreach (string item in splitData)
			{
				if (getToday == item)
				{
					btnIn.Visible = false;
					btnBack.Visible = false;
					hdnisTimeInShow.Value = "1";
					hdnisTimeoutShow.Value = "1";
				}
			}
		}
		if (dtTrDetails.Tables[1].Rows.Count > 0)
		{
			var getDays = Convert.ToString(dtTrDetails.Tables[1].Rows[0]["ISWORKING"]);
			if (getDays == "HO")
			{
				btnIn.Visible = false;
				btnBack.Visible = false;
				hdnisTimeInShow.Value = "1";
				hdnisTimeoutShow.Value = "1";
			}
		}
		}
	protected void IsEnabledFalse(Boolean blnSetControl)
	{
		txtFromdate.Enabled = blnSetControl;

		btnIn.Enabled = blnSetControl;
	}
	public void BindLeaveRequestProperties()
	{

		string[] strdate;
		string strfromDate = "";
		string strToDate = "";
		#region date formatting
		if (Convert.ToString(txtFromdate.Text).Trim() != "")
		{
			strdate = Convert.ToString(txtFromdate.Text).Trim().Split('-');
			strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
		}

		#endregion


		lpm.Emp_Code = txtEmpCode.Text;
		lpm.Leave_FromDate = strfromDate;
		lpm.Leave_ToDate = strToDate;
		lpm.Grade = hflGrade.Value.ToString();
		lpm.Approvers_code = hflapprcode.Value;
		lpm.appr_id = Convert.ToInt32(lstApprover.SelectedIndex);
		lpm.EmailAddress = hflEmailAddress.Value;
		lpm.Emp_Name = hflEmpName.Value;
	}
	private void setenablefalseConttols()
	{
		if (Convert.ToString(hdnLeaveStatus.Value).Trim() != "1" && Convert.ToString(hdnLeaveStatus.Value).Trim() != "5")
		{
			txtFromdate.Enabled = false;
		}
		if (hdnAppr_status.Value == "Approved" && Convert.ToString(hdnLeaveStatus.Value).Trim() == "1")
		{
			txtFromdate.Enabled = false;
		}
	}
	public void GetDeploymentTypeStatus()
	{
		try
		{
			var finaldate = DateTime.Now.ToString("yyyy-MM-dd");
			var getDt = spm.GetDeploymentTypeStatus("GetDeploymentStatus", lpm.Emp_Code, finaldate);

			if (getDt != null)
			{
				if (getDt.Rows.Count > 0)
				{
					var TypeId = Convert.ToInt32(getDt.Rows[0]["TypeId"]);
					hdnTypeId.Value = Convert.ToString(TypeId);
					if (TypeId == 1)
					{
                         
                        var TypeName = Convert.ToString(getDt.Rows[0]["TypeName"]);
						var IPAddress = Convert.ToString(getDt.Rows[0]["IPAddress"]);
						var getIPAddress = GetIPAddress();
						 lbltest.Text = Convert.ToString(getIPAddress);
						if (IPAddress != getIPAddress)
						{
							hdnTypeId.Value = "2";
							var getToDayODStatus = spm.GetDeploymentTypeStatus("CheckToDayODApplication", lpm.Emp_Code, finaldate);
							if (getToDayODStatus != null)
							{
								if (getToDayODStatus.Rows.Count > 0)
								{
									var getMessage = getToDayODStatus.Rows[0]["Message"].ToString();
									if (getMessage == "YES")
									{
										var getHalfDayTime = Convert.ToDateTime(hdnHalfTime.Value.ToString());
										var ConvertHalfDayTime = getHalfDayTime.ToString("HH:mm:ss");
										var CurrentDateTime = DateTime.Now.ToString("HH:mm:ss");
										var getStatus = getToDayODStatus.Rows[0]["Status"].ToString();
										var convertCurrentTime = Convert.ToDateTime(CurrentDateTime).TimeOfDay;
										var ConvertHalfDayTime1 = Convert.ToDateTime(ConvertHalfDayTime).TimeOfDay;
										if (getStatus == "First Half")
										{
											if (convertCurrentTime > ConvertHalfDayTime1)
											{
												btnBack.Visible = false;
												hdnisTimeoutShow.Value = "1";
												lblmessage.Text = " You have to be in Head Office Network in order to Check In / Check Out!";
												lblmessage.Visible = true;
											}
										}
										else if (getStatus == "Second Half")
										{
											if (convertCurrentTime < ConvertHalfDayTime1)
											{
												btnIn.Visible = false;
												hdnisTimeInShow.Value = "1";
												lblmessage.Text = " You have to be in Head Office Network in order to Check In / Check Out!";
												lblmessage.Visible = true;
											}
										}
										else
										{

										}
									}
									else
									{
										btnIn.Visible = false;
										btnBack.Visible = false;
										hdnisTimeInShow.Value = "1";
										hdnisTimeoutShow.Value = "1";

										//lblmessage.Text = "You are not entitled for Attendance." + TypeName;
										lblmessage.Text = "You have to be in Head Office Network in order to Check In / Check Out!";
										lblmessage.Visible = true;
									}
								}
								else
								{
									btnIn.Visible = false;
									btnBack.Visible = false;
									hdnisTimeInShow.Value = "1";
									hdnisTimeoutShow.Value = "1";

									//lblmessage.Text = "You are not entitled for Attendance." + TypeName;
									lblmessage.Text = "You have to be in Head Office Network in order to Check In / Check Out!";
									lblmessage.Visible = true;
								}
							}
							else
							{
								btnIn.Visible = false;
								btnBack.Visible = false;
								hdnisTimeInShow.Value = "1";
								hdnisTimeoutShow.Value = "1";

								//lblmessage.Text = "You are not entitled for Attendance." + TypeName;
								lblmessage.Text = "You have to be in Head Office Network in order to Check In / Check Out!";
								lblmessage.Visible = true;
							}

						}
					}
                    if (TypeId == 2)
                    {

                        DataSet getip = new DataSet();
                        SqlParameter[] spars = new SqlParameter[2];
                        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                        spars[0].Value = "project_code_ipaddress";
                        spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                        spars[1].Value = lpm.Emp_Code.ToString();
                        getip = spm.getDatasetList(spars, "SP_WFO_WFH_DETAILS"); 
                        if (getip != null)
                        {
                            if (getip.Tables[0].Rows.Count > 0)
                            {
                                var ips = getip.Tables[0].Rows[0]["IPAddress"].ToString().Split(',');
                                var IPAddress = new List<string>();
                                foreach (var a in ips)
                                {
                                    IPAddress.Add(a);
                                } 
                                var project = Convert.ToString(getip.Tables[0].Rows[0]["Project_Code"]);
                                var getIPAddress = GetIPAddress(); 
                                lbltest.Text = Convert.ToString(getIPAddress);
                                if (!IPAddress.Contains(getIPAddress))
                                {
                                    hdnTypeId.Value = "2";
                                    var getToDayODStatus = spm.GetDeploymentTypeStatus("CheckToDayODApplication", lpm.Emp_Code, finaldate);
                                    if (getToDayODStatus != null)
                                    {
                                        if (getToDayODStatus.Rows.Count > 0)
                                        {
                                            var getMessage = getToDayODStatus.Rows[0]["Message"].ToString();
                                            if (getMessage == "YES")
                                            {
                                                var getHalfDayTime = Convert.ToDateTime(hdnHalfTime.Value.ToString());
                                                var ConvertHalfDayTime = getHalfDayTime.ToString("HH:mm:ss");
                                                var CurrentDateTime = DateTime.Now.ToString("HH:mm:ss");
                                                var getStatus = getToDayODStatus.Rows[0]["Status"].ToString();
                                                var convertCurrentTime = Convert.ToDateTime(CurrentDateTime).TimeOfDay;
                                                var ConvertHalfDayTime1 = Convert.ToDateTime(ConvertHalfDayTime).TimeOfDay;
                                                if (getStatus == "First Half")
                                                {
                                                    if (convertCurrentTime > ConvertHalfDayTime1)
                                                    {
                                                        btnBack.Visible = false;
                                                        hdnisTimeoutShow.Value = "2";
                                                        lblmessage.Text = " You have to be in Client Office Network in order to Check In / Check Out!";
                                                        lblmessage.Visible = true;
                                                    }
                                                }
                                                else if (getStatus == "Second Half")
                                                {
                                                    if (convertCurrentTime < ConvertHalfDayTime1)
                                                    {
                                                        btnIn.Visible = false;
                                                        hdnisTimeInShow.Value = "2";
                                                        lblmessage.Text = " You have to be in Client Office Network in order to Check In / Check Out!";
                                                        lblmessage.Visible = true;
                                                    }
                                                }
                                            }

                                            else
                                            {
                                                btnIn.Visible = false;
                                                btnBack.Visible = false;
                                                hdnisTimeInShow.Value = "2";
                                                hdnisTimeoutShow.Value = "2"; 
                                                //lblmessage.Text = "You are not entitled for Attendance." + TypeName;
                                                lblmessage.Text = "You have to be in Client Office Network in order to Check In / Check Out!";
                                                lblmessage.Visible = true;
                                            }
                                        }
                                        else
                                        {
                                            btnIn.Visible = false;
                                            btnBack.Visible = false;
                                            hdnisTimeInShow.Value = "2";
                                            hdnisTimeoutShow.Value = "2"; 
                                            //lblmessage.Text = "You are not entitled for Attendance." + TypeName;
                                            lblmessage.Text = "You have to be in Client Office Network in order to Check In / Check Out!";
                                            lblmessage.Visible = true;
                                        }
                                    }
                                    else
                                    {
                                        btnIn.Visible = false;
                                        btnBack.Visible = false;
                                        hdnisTimeInShow.Value = "2";
                                        hdnisTimeoutShow.Value = "2"; 
                                        //lblmessage.Text = "You are not entitled for Attendance." + TypeName;
                                        lblmessage.Text = "You have to be in Client Office Network in order to Check In / Check Out!";
                                        lblmessage.Visible = true;
                                    }

                                }
                            }
                        }

                    }
                }

			}
		}
		catch (Exception ex)
		{
		}
	}

    protected string GetIPAddress()
    {
        string ip = "";
        try
        {
            string _ipList = HttpContext.Current.Request.ServerVariables["HTTP_X_CLUSTER_CLIENT_IP"];
            if (!string.IsNullOrWhiteSpace(_ipList))
            {
                ip = _ipList.Split(',')[0].Trim();
            }
            else
            {
                _ipList = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrWhiteSpace(_ipList))
                {
                    ip = _ipList.Split(',')[0].Trim();
                }
                else
                {
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
            }

            // Handle local IPv6 loopback (::1)
            if (ip == "::1")
            {
                // If necessary, resolve to machine's local IP (e.g., LAN IP) or use a placeholder
                ip = GetLocalIPAddress();
            }
        }
        catch (Exception ex)
        {
            ip = "110.173.184.58.1"; // Default or fallback IP
        }

        return ip;
    }

    private string GetLocalIPAddress()
    {
        try
        {
            var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (var addr in host.AddressList)
            {
                // Return IPv4 address
                if (addr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return addr.ToString();
                }
            }
        }
        catch
        {
            // Fallback local IP if unable to resolve
            return "127.0.0.1";
        }

        return "127.0.0.1";
    }

    //protected string GetIPAddress()
    //{
    //	string ip = "";
    //	try
    //	{
    //		string _ipList = "";
    //		//string _ipList = HttpContext.Current.Request.Headers["CF-CONNECTING-IP"].ToString();

    //		_ipList = HttpContext.Current.Request.ServerVariables["HTTP_X_CLUSTER_CLIENT_IP"];
    //		if (!string.IsNullOrWhiteSpace(_ipList))
    //		{
    //			ip += _ipList.Split(',')[0].Trim();
    //		}
    //		else
    //		{
    //			_ipList = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
    //			if (!string.IsNullOrWhiteSpace(_ipList))
    //			{
    //				ip += _ipList.Split(',')[0].Trim();
    //			}
    //			else
    //			{
    //				ip += HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString().Trim();
    //			}
    //		}
    //	}
    //	catch (Exception ex)
    //	{
    //		ip = "110.173.184.58.1";
    //	}
    //	return ip;
    //}
    public string SesstionID()
	{
		string returnString = "";
		try
		{
			var SesstionId = HttpContext.Current.Session.SessionID;
			//var resultString = Regex.Replace(SesstionId, "[^0-9]+", string.Empty);
			returnString = SesstionId;
		}
		catch (Exception)
		{
		}
		return returnString;
	}
	#endregion
	protected void btnIn_Click(object sender, EventArgs e)
	{
		try
		{

			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}
			//var getTime=
			#region Check All Parameters Selected
			lblmessage.Text = "";
			if (Convert.ToString(txtFromdate.Text).Trim() == "")
			{
				lblmessage.Text = "Please select From Date";
				return;
			}
			
			#endregion

			lblmessage.Text = "";
			string[] strdate;
			string strfromDate = "";
			string strtoDate = "";
			string strfromDate_tt = "";

			#region date formatting
			if (Convert.ToString(txtFromdate.Text).Trim() != "")
			{
				strdate = Convert.ToString(txtFromdate.Text).Trim().Split('-');
				strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
				strfromDate_tt = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
			}

			#endregion
			BindLeaveRequestProperties();

			hdnPLwithSL_succession.Value = "";
			#region MethodsCall
			string att_status = "";

			SqlParameter[] spars1 = new SqlParameter[9];

			spars1[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars1[1] = new SqlParameter("@Id", SqlDbType.Int);
			spars1[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
			spars1[3] = new SqlParameter("@att_date", SqlDbType.VarChar);
			spars1[4] = new SqlParameter("@att_type", SqlDbType.VarChar);
			spars1[5] = new SqlParameter("@att_time", SqlDbType.NVarChar);
			spars1[6] = new SqlParameter("@att_status", SqlDbType.NVarChar);
			spars1[7] = new SqlParameter("@IpAddress", SqlDbType.NVarChar);
			spars1[8] = new SqlParameter("@Type_Id", SqlDbType.Int);


			spars1[0].Value = "Insert";
			spars1[1].Value = DBNull.Value;
			spars1[2].Value = lpm.Emp_Code.ToString();
			if (strfromDate_tt.ToString() == "")
				spars1[3].Value = DBNull.Value;
			else
				spars1[3].Value = strfromDate_tt.ToString();

			spars1[4].Value = "In";
			// spars1[5].Value = hdnInTime.Value.ToString();
			spars1[5].Value = null;
			spars1[6].Value = att_status;
			spars1[7].Value = SesstionID();
			spars1[8].Value = Convert.ToInt32(hdnTypeId.Value);


			spm.Insert_Data(spars1, "SP_Attendance");
			//ClearControls();
			get_CheckIn();
			//getAttTimeSheet();
			
			lblmessage.Text = "You have successfully checked-In";
			ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ASD", "StopLoader();", true);
			Response.Redirect("~/procs/Attendance.aspx");

			#endregion

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);

			//throw;
		}

	}

	protected void btnBack_Click1(object sender, EventArgs e)
	{
		try
		{

			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}

			#region Check All Parameters Selected
			lblmessage.Text = "";
			if (Convert.ToString(txtFromdate.Text).Trim() == "")
			{
				lblmessage.Text = "Please select From Date";
				return;
			}
			#endregion

			lblmessage.Text = "";
			string[] strdate;
			string strfromDate = "";
			string strtoDate = "";
			string strfromDate_tt = "";

			#region date formatting
			if (Convert.ToString(txtFromdate.Text).Trim() != "")
			{
				strdate = Convert.ToString(txtFromdate.Text).Trim().Split('-');
				strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
				strfromDate_tt = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
			}

			#endregion
			BindLeaveRequestProperties();
			hdnPLwithSL_succession.Value = "";
			#region MethodsCall
			string att_status = "";

			SqlParameter[] spars1 = new SqlParameter[9];

			spars1[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars1[1] = new SqlParameter("@Id", SqlDbType.Int);
			spars1[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
			spars1[3] = new SqlParameter("@att_date", SqlDbType.VarChar);
			spars1[4] = new SqlParameter("@att_type", SqlDbType.VarChar);
			spars1[5] = new SqlParameter("@att_time", SqlDbType.NVarChar);
			spars1[6] = new SqlParameter("@att_status", SqlDbType.NVarChar);
			spars1[7] = new SqlParameter("@IpAddress", SqlDbType.NVarChar);
			spars1[8] = new SqlParameter("@Type_Id", SqlDbType.Int);

			spars1[0].Value = "Update";
			spars1[1].Value = DBNull.Value;
			spars1[2].Value = lpm.Emp_Code.ToString();
			if (strfromDate_tt.ToString() == "")
				spars1[3].Value = DBNull.Value;
			else
				spars1[3].Value = strfromDate_tt.ToString();

			spars1[4].Value = "Out";
			//spars1[5].Value = hdnOutTime.Value.ToString();
			spars1[5].Value = null;
			spars1[6].Value = att_status;
			spars1[7].Value = SesstionID();
			spars1[8].Value = Convert.ToInt32(hdnTypeId.Value);


			spm.Insert_Data(spars1, "SP_Attendance");
			//ClearControls();
			get_CheckIn();
			//getAttTimeSheet();
			lblmessage.Text = "You have successfully checked-out";
			ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ASD", "StopLoader();", true);
            Response.Redirect("~/procs/Attendance.aspx");

            #endregion

            

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);

			//throw;
		}

	}
	

}