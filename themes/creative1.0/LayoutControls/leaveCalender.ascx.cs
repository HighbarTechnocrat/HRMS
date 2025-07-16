using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

public partial class themes_creative_LayoutControls_leaveCalender : System.Web.UI.UserControl
{
  
        int chkLocationID;
        DataSet dsEmpLeave, dsEmpdetails, dsHolidays, dsweekends, dsweekendloc;
        string userlogin = "";
        string fweekend = "";
        string sweekend = "";
        string loginempcode ="";

        public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }


        protected void Page_Load(object sender, EventArgs e)
        {
            //string strmonth = "";
            //string stryear = "";
            //strmonth =Convert.ToString(Calendar1.ToString());
            
            //Response.Write("Month--" + strmonth + "  Year ---" + stryear);
            //Response.End();

            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            loginempcode = Convert.ToString(Session["empcode"]);


            if (!IsPostBack)
            {
                
                Calendar1.VisibleDate = DateTime.Today;
                lblError.Visible = false;
                Calendar1.SelectedDate = System.DateTime.Now;
                hdnmonthvalue_New.Value ="01-" + System.DateTime.Now.Date.Month.ToString() + "-" + System.DateTime.Now.Date.Year.ToString() ;

                dsEmpLeave = GetCurrentMonthEmpWiseData_test(loginempcode.ToString(), Convert.ToString(hdnmonthvalue_New.Value));
                dsHolidays = GetCurrentMonthData(Convert.ToString(hdnmonthvalue_New.Value));
                dsweekends = GetWeekends();
               // Add New Method
               dsweekendloc = GetWeekendslocationW();
              // END New Method

            if (dsweekends.Tables[0].Rows.Count > 0)
                 {
                     string[] str = dsweekends.Tables[0].Rows[0]["OFF_DAYS_NAME"].ToString().Split(',');


                     int len = str.GetUpperBound(0);
                     if (len > 0)
                     {
                         fweekend = str[0].Trim();
                         sweekend = str[1].Trim();
                     }
                     else
                     {
                         fweekend = str[0].Trim();
                         sweekend = str[0].Trim();
                     }
                 }
            }
            else
            {
                Calendar1.VisibleDate = DateTime.Today;
                lblError.Visible = false;
                Calendar1.SelectedDate = System.DateTime.Now;
                hdnmonthvalue_New.Value = "01-" + System.DateTime.Now.Date.Month.ToString() + "-" + System.DateTime.Now.Date.Year.ToString();

                dsEmpLeave = GetCurrentMonthEmpWiseData_test(loginempcode.ToString(), Convert.ToString(hdnmonthvalue_New.Value));
                dsHolidays = GetCurrentMonthData(Convert.ToString(hdnmonthvalue_New.Value));
                dsweekends = GetWeekends();
                // Add New Method
                dsweekendloc = GetWeekendslocationW();
               // END New Method

            if (dsweekends.Tables[0].Rows.Count > 0)
                {
                    string[] str = dsweekends.Tables[0].Rows[0]["OFF_DAYS_NAME"].ToString().Split(',');


                    int len = str.GetUpperBound(0);
                    if (len > 0)
                    {
                        fweekend = str[0].Trim();
                        sweekend = str[1].Trim();
                    }
                    else
                    {
                        fweekend = str[0].Trim();
                        sweekend = str[0].Trim();
                    }
                }
            }

            //Calendar1.Attributes.Add("onlcik", "return noanyCharecters(event);");
           
             
          // Calendar1.SelectedDate = System.DateTime.Now;
           // lblError.Text = Calendar1.SelectedDate.ToShortDateString();
           //lblError.Text = Convert.ToString((TitleFormat)Calendar1.SelectionMode);
             

            
            //dsEmpLeave = GetCurrentMonthEmpWiseData(loginempcode.ToString(), "");
            
            //dsHolidays = GetCurrentMonthData("");
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            #region MyRegion
            try
            {
                DateTime nextDate;
                DateTime LeaveDate;                
                DateTime AbsentDateOthers;

                if (e.Day.IsOtherMonth)
                {
                    e.Cell.Text = "";
                    e.Cell.BackColor = System.Drawing.Color.White;
                    e.Cell.ForeColor = System.Drawing.Color.White;

                }
                
                
                    #region test
                    e.Cell.Attributes.Add("onclick", "return noanyCharecters(event);");
                    DateTime dateValue = new DateTime();
                    dateValue = e.Day.Date;
                    String sp_dateValue = e.Day.Date.Day.ToString() + "-" + e.Day.Date.Month.ToString() + "-" + e.Day.Date.Year.ToString();
                    string sunday = dateValue.DayOfWeek.ToString();
                    //dsEmpLeave = GetCurrentMonthEmpWiseData_test(loginempcode.ToString(), Convert.ToString(dateValue));                
                    //dsEmpLeave = GetCurrentMonthEmpWiseData_test(loginempcode.ToString(), Convert.ToString(sp_dateValue));
                
                //t    dsEmpLeave = GetCurrentMonthEmpWiseData_test(loginempcode.ToString(), Convert.ToString(hdnmonthvalue_New.Value));

                   // dsHolidays = GetCurrentMonthData(Convert.ToString(dateValue));
                //t     dsHolidays = GetCurrentMonthData(Convert.ToString(hdnmonthvalue_New.Value));
                

                    //hdnmonthvalue.Value = Convert.ToString(dateValue);
                     hdnmonthvalue.Value = Convert.ToString(hdnmonthvalue_New.Value);

                    #region Check Sunday
                     if (sunday == fweekend || sunday == sweekend)
                    //if (sunday == "Sunday")
                    {
                        e.Cell.ForeColor = System.Drawing.Color.Red;
                    }
                    if (e.Day.IsOtherMonth)
                    {
                        e.Day.IsSelectable = false;
                        e.Cell.Enabled = false;
                    }
                    #endregion

                    if (System.DateTime.Now.Date == e.Day.Date)
                    {
                        
                        e.Cell.BorderColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
                        e.Cell.BorderWidth = 3;
                        e.Cell.ToolTip = "Today";
                    }

                    chkLocationID = 1;
                    #region for Location other than 1
                    if (chkLocationID == 2)
                    {
                        DataSet dsEmpAbsentOthers = GetAbsentDataForOthers(Convert.ToString(loginempcode).Trim());
                        if (dsEmpAbsentOthers.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drAbsent1 in dsEmpAbsentOthers.Tables[0].Rows)
                            {
                                AbsentDateOthers = (DateTime)drAbsent1["Date"];
                                if (AbsentDateOthers == e.Day.Date)
                                {
                                    e.Cell.BackColor = System.Drawing.Color.Red;

                                    e.Cell.ToolTip = "Absent";
                                }
                            }
                        }
                    }
                    else
                    {
                        //Loop for Check Absent data
                        /*DataSet dsEmpAbsent = GetAbsentData(Convert.ToString(loginempcode).Trim());
                        if (dsEmpAbsent.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drAbsent in dsEmpAbsent.Tables[0].Rows)
                            {
                                AbsentDate = (DateTime)drAbsent["Date"];
                                if (AbsentDate == e.Day.Date)
                                {
                                    e.Cell.BackColor = System.Drawing.Color.Red;
                                    e.Cell.ToolTip = "Absent"; //dsHolidays.Tables[0].Rows[0]["HolidayName"].ToString();
                                }
                            }
                        }*/

                    }
                    #endregion

                    #region Check Day is Holiday
                    foreach (DataRow dr in dsHolidays.Tables[0].Rows)
                    {
                        if (Convert.ToString(dr["HolidayDate"]) == string.Empty)
                        {
                            e.Cell.BackColor = System.Drawing.Color.Transparent;
                        }
                        else
                        {
                            nextDate = (DateTime)dr["HolidayDate"];
                            if (nextDate == e.Day.Date)
                            {
                                // e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#2C778C");
                                e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9933");
                                //e.Cell.BackColor = System.Drawing.Color.Red;
                                e.Cell.ToolTip = dr["HolidayName"].ToString();

                            }
                        }
                    }
            #endregion

            #region Check Day is Weekend from Add Location Alternate

            if (dsweekendloc.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsweekendloc.Tables[0].Rows)
                {
                    if (Convert.ToString(dr["HolidayDate"]) == string.Empty)
                    {
                        e.Cell.BackColor = System.Drawing.Color.Transparent;
                    }
                    else
                    {
                        nextDate = (DateTime)dr["HolidayDate"];
                        if (nextDate == e.Day.Date)
                        {
                            e.Cell.ForeColor = System.Drawing.Color.Red;

                        }
                    }
                }
            }
            #endregion


            #region Check Day is employee Leave & Travel Status
            if (dsEmpLeave.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr1 in dsEmpLeave.Tables[0].Rows)
                        {
                            LeaveDate = (DateTime)dr1["thedate"];
                            if (LeaveDate == e.Day.Date)
                            {
                                StringBuilder table = new StringBuilder();

                                if (Convert.ToString(dr1["Status_id"]).Trim() == "6")
                                    table.Append("Employee Attendance Details\n");
                                else
                                    table.Append("Employee Leave Details\n");

                                if (Convert.ToString(dr1["LeaveDays"]).Trim() != "0")
                                    table.Append("Leave Days: " + dr1["LeaveDays"].ToString() + "\n");

                                table.Append("Leave Type: " + dr1["LeaveTypeName"].ToString() + "\n");
                                table.Append("Leave Reason: " + dr1["LeaveReason"].ToString() + "\n");
                                table.Append("Leave Status: " + Convert.ToString(dr1["StatusName"]));

                                //////Attendance Regualrzatin
                                ////if (Convert.ToString(dr1["Status_id"]).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leaveStatusid_AR"]).Trim())
                                ////    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#C5BE97");


                                //////Correction
                                ////if (Convert.ToString(dr1["Status_id"]).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leaveStatusid_C"]).Trim())
                                ////    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#3A7DCE");
                                
                                //e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#60497B");

                                //Pending
                                if (Convert.ToString(dr1["Status_id"]).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leaveStatusid_P"]).Trim())
                                    e.Cell.BackColor = System.Drawing.Color.Yellow;
                                    //e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#3A7DCE");

                                //Approved
                                if (Convert.ToString(dr1["Status_id"]).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leaveStatusid_A"]).Trim())
                                    e.Cell.BackColor = System.Drawing.Color.Green;
                                    //e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#D17F7D");



                                //Travel Approved
                                if (Convert.ToString(dr1["Status_id"]).Trim() == Convert.ToString(ConfigurationManager.AppSettings["Approvedstatusid_Trvl"]).Trim())
                                {
                                    table.Length = 0;
                                    table.Append("Employee Travel Details\n");
                                    table.Append("Travel Type: " + dr1["LeaveTypeName"].ToString() + "\n");
                                    table.Append("Travel Reason: " + dr1["LeaveReason"].ToString() + "\n");
                                    table.Append("Travel Status: " + Convert.ToString(dr1["StatusName"]));
                                    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#00b050");
                                }
                                //Travel Pending
                                if (Convert.ToString(dr1["Status_id"]).Trim() == Convert.ToString(ConfigurationManager.AppSettings["Approvedstatusid_L"]).Trim())
                                {
                                    table.Length = 0;
                                    table.Append("Employee Travel Details\n");
                                    table.Append("Travel Type: " + dr1["LeaveTypeName"].ToString() + "\n");
                                    table.Append("Travel Reason: " + dr1["LeaveReason"].ToString() + "\n");
                                    table.Append("Travel Status: " + Convert.ToString(dr1["StatusName"]));
                                    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#7030A0");
                                }


                                e.Cell.ToolTip = table.ToString();

                            }
                        }
                    }
                    #endregion

                    if (e.Day.IsOtherMonth)
                    {
                        e.Cell.Text = "";
                        e.Cell.BackColor = System.Drawing.Color.White;
                        e.Cell.ForeColor = System.Drawing.Color.White;
                    }

                    //lblError.Text = Convert.ToString(e.Day.Date.Add); 
                    #endregion
                

            }
            catch (Exception ex)
            {
                lblError.Visible = false;
                //lblError.Text = "*" + ex.Message;
                 lblError.Text = "*";
            }

            #endregion
        }


        #region Calendar Function
        protected DataSet GetCurrentMonthEmpWiseData(string uempcode,string strmonths)
        {
            DataSet dsMonth = new DataSet();

            //string connString = ConfigurationManager.AppSettings["sqlconnection"];
            string connString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            SqlConnection conn = new SqlConnection(connString);
            SqlConnection dbConnection = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "GET_YEARLY_LEAVE_EMPWISE";
            cmd.Parameters.Add("@EMP_ID", SqlDbType.VarChar).Value =uempcode;

            if (Convert.ToString(strmonths).Trim() != "")
                cmd.Parameters.Add("@sMonth", SqlDbType.VarChar).Value = strmonths;
            else
                cmd.Parameters.Add("@sMonth", SqlDbType.VarChar).Value = DBNull.Value;

            //String query;
            //query = "select LT.LeaveReason,LS.LeaveTypeName,LT.Fromdate,LT.Todate from dbo.tblLeaveTransactionMst as LT LEFT JOIN EMPLOYEE.DBO.TBLEMPLOYEEMST AS E ON E.EMP_ID=LT.REQUESTORID left join dbo.tblLeaveTypesMst AS LS ON LT.Leavetypeid=LS.Leavetypeid where E.EMP_LOGIN_ID ='" + EMP_LOGIN_ID + "' AND DATEPART(YEAR,Fromdate) = DATEPART(YEAR,GETDATE())";
            //query = "select LT.LeaveReason,LS.LeaveTypeName,LT.Fromdate,LT.Todate,Dt from dbo.tblLeaveTransactionMst as LT LEFT JOIN EMPLOYEE.DBO.TBLEMPLOYEEMST AS E ON E.EMP_ID=LT.REQUESTORID left join dbo.tblLeaveTypesMst AS LS ON LT.Leavetypeid=LS.Leavetypeid INNER JOIN (SELECT * from fnGetDatesInRange('01/01/2010', '12/31/2010')) AS A ON A.Dt Between LT.Fromdate AND LT.Todate where E.EMP_LOGIN_ID = '" + EMP_LOGIN_ID + "' AND DATEPART(YEAR,Fromdate) = DATEPART(YEAR,GETDATE())"; 
            //SqlCommand dbCommand = new SqlCommand(query, dbConnection);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sqlDataAdapter.Fill(ds);

            return ds;
        }
        protected DataSet GetCurrentMonthEmpWiseData_test(string uempcode, string strmonths)
        {
            DataSet dsMonth = new DataSet();

            //string connString = ConfigurationManager.AppSettings["sqlconnection"];
            string connString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            SqlConnection conn = new SqlConnection(connString);
            SqlConnection dbConnection = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "GET_YEARLY_LEAVE_EMPWISE_test";
            cmd.Parameters.Add("@EMP_ID", SqlDbType.VarChar).Value = uempcode;

            if (Convert.ToString(strmonths).Trim() != "")
                cmd.Parameters.Add("@sMonth", SqlDbType.VarChar).Value = strmonths;
            else
                cmd.Parameters.Add("@sMonth", SqlDbType.VarChar).Value = DBNull.Value;

            //String query;
            //query = "select LT.LeaveReason,LS.LeaveTypeName,LT.Fromdate,LT.Todate from dbo.tblLeaveTransactionMst as LT LEFT JOIN EMPLOYEE.DBO.TBLEMPLOYEEMST AS E ON E.EMP_ID=LT.REQUESTORID left join dbo.tblLeaveTypesMst AS LS ON LT.Leavetypeid=LS.Leavetypeid where E.EMP_LOGIN_ID ='" + EMP_LOGIN_ID + "' AND DATEPART(YEAR,Fromdate) = DATEPART(YEAR,GETDATE())";
            //query = "select LT.LeaveReason,LS.LeaveTypeName,LT.Fromdate,LT.Todate,Dt from dbo.tblLeaveTransactionMst as LT LEFT JOIN EMPLOYEE.DBO.TBLEMPLOYEEMST AS E ON E.EMP_ID=LT.REQUESTORID left join dbo.tblLeaveTypesMst AS LS ON LT.Leavetypeid=LS.Leavetypeid INNER JOIN (SELECT * from fnGetDatesInRange('01/01/2010', '12/31/2010')) AS A ON A.Dt Between LT.Fromdate AND LT.Todate where E.EMP_LOGIN_ID = '" + EMP_LOGIN_ID + "' AND DATEPART(YEAR,Fromdate) = DATEPART(YEAR,GETDATE())"; 
            //SqlCommand dbCommand = new SqlCommand(query, dbConnection);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sqlDataAdapter.Fill(ds);
            return ds;
        }

        public DataSet GetEmpDetailsForID(string EMP_LOGIN_ID)
        {
            string connstring = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(connstring);
            string HRLogin = string.Empty;
            string PresidentLogin = string.Empty;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "GET_EMP_DETAILS_FORID";
            cmd.Parameters.Add("@EMP_LOGIN_ID", SqlDbType.VarChar).Value = EMP_LOGIN_ID;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            //ds.Tables[0].Columns.Add("HR");
            //ds.Tables[0].Columns.Add("President");
            //ds.Tables[0].Rows.Add();
            //int count = ds.Tables[0].Rows.Count;
            ////int count1 = ds.Tables[1].Rows.Count;
            ////ds.Tables[0].Rows[count - 1]["HR"] = HRLogin.ToString();
            //ds.Tables[0].Rows[count - 1]["President"] = PresidentLogin.ToString();
            return ds;
        }

        protected DataSet GetCurrentMonthData(string strmonth)
        {   
            DataSet dsMonth = new DataSet();
            string connString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            //String connString = cs.ConnectionString;
            SqlConnection dbConnection = new SqlConnection(connString);
            String query;
            
            //query = "select HM.HolidayDate,HM.HolidayName from dbo.tblHolidayMst as HM left join dbo.tblHolidayMapMst as HMM on HMM.Holidayid = HM.HoildayID where datepart(year,HM.HolidayDate)= datepart(year,getdate()) AND HMM.Locationid=" + LocationID;
            if (Convert.ToString(strmonth).Trim() == "")
                query = " select HM.Holiday_Date as HolidayDate,HM.Holiday_Name  as HolidayName from dbo.tblHolidayMst as HM  where datepart(year,HM.Holiday_Date)= datepart(year,getdate()) and IsWeekend='H'  and Location in ( " +
                        " select emp_location " +
                         " from tbl_Employee_Mst where Emp_Code ='" + loginempcode + "')";
            else
                query = " select HM.Holiday_Date as HolidayDate,HM.Holiday_Name  as HolidayName from dbo.tblHolidayMst as HM  where datepart(year,HM.Holiday_Date) in (Year('" + strmonth + "'),Year('" + strmonth + "')+1) and IsWeekend='H' and Location in ( " +
                        " select emp_location " +
                       " from tbl_Employee_Mst where Emp_Code ='" + loginempcode + "')";
            
            SqlCommand dbCommand = new SqlCommand(query, dbConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(dbCommand);
            sqlDataAdapter.Fill(dsMonth);
            return dsMonth;
        }

        protected DataSet GetWeekends()
        {
            DataSet dsMonth = new DataSet();
            string connString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            //String connString = cs.ConnectionString;
            SqlConnection dbConnection = new SqlConnection(connString);
            String query;

            query = "select OFF_DAYS_NAME from TBL_HRMS_WORKSCHEDULE where SCHEDULE_ID = (select SCHEDULE_ID from tbl_hmst_company_Location where comp_code = " +
                    " (select emp_location from tbl_Employee_Mst where Emp_Code = '" + loginempcode + "'))";

            SqlCommand dbCommand = new SqlCommand(query, dbConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(dbCommand);
            sqlDataAdapter.Fill(dsMonth);
            return dsMonth;
        }

    protected DataSet GetWeekendslocationW()
    {
        DataSet dsWL = new DataSet();
        string connString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        SqlConnection conn = new SqlConnection(connString);
        //String connString = cs.ConnectionString;
        SqlConnection dbConnection = new SqlConnection(connString);
        String query;

        query = " select HM.Holiday_Date as HolidayDate,HM.Holiday_Name  as HolidayName from dbo.tblHolidayMst as HM  where datepart(year,HM.Holiday_Date) in (Year(GetDate())) and IsWeekend='W' and Location in ( " +
                        " select emp_location " +
                       " from tbl_Employee_Mst where Emp_Code ='" + loginempcode + "')";

        SqlCommand dbCommand = new SqlCommand(query, dbConnection);
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(dbCommand);
        sqlDataAdapter.Fill(dsWL);
        return dsWL;
    }

    protected DataSet GetAbsentDataForOthers(string LEAVEEMPCODE)
        {
            DataSet dsMonth = new DataSet();
            string connstring = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connstring);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "GET_ABSENT_DETAILS_OTHER_LOCATION";
            cmd.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = LEAVEEMPCODE;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        protected DataSet GetAbsentData(string LEAVEEMPCODE)
        {
            DataSet dsMonth = new DataSet();
            string connstring = ConfigurationManager.AppSettings["sqlconnection"];//"Server=172.16.4.1;Database=SmartSal;User ID=sa;Password=Saadmin;Trusted_Connection=False;"; //ConfigurationManager.AppSettings["ConnectionStringLeave"];
            SqlConnection conn = new SqlConnection(connstring);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "GET_ABSENT_DETAILS_MYLAVASA";
            cmd.Parameters.Add("@LEAVEEMPCODE", SqlDbType.VarChar).Value = LEAVEEMPCODE;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }


        #endregion
        
        //protected void txtcalMonths_TextChanged(object sender, EventArgs e)
        //{
        //    dsEmpLeave = GetCurrentMonthEmpWiseData(loginempcode.ToString(), Convert.ToString(txtcalMonths.Text).Trim());

        //    if (dsEmpLeave.Tables[1].Rows.Count> 0)
        //    {
        //        DateTime dt = new DateTime();
        //        dt = Convert.ToDateTime(dsEmpLeave.Tables[1].Rows[0]["caldate"]);
        //        Calendar1.SelectedDate = dt;
        //        Calendar1.VisibleDate = dt;
        //    }
        //    dsHolidays = GetCurrentMonthData();
        //}

        protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            hdnmonthvalue_New.Value = "01-" + e.NewDate.Date.Month.ToString() +"-"+ e.NewDate.Date.Year.ToString();
            dsEmpLeave = GetCurrentMonthEmpWiseData_test(loginempcode.ToString(), Convert.ToString(hdnmonthvalue_New.Value));
            dsHolidays = GetCurrentMonthData(Convert.ToString(hdnmonthvalue_New.Value));
        }
}




