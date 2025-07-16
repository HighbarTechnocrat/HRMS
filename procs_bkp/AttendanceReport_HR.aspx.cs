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
using System.Web;

public partial class AttendanceReport_HR : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    SqlConnect sql = new SqlConnect();
    SqlConnection scon = null;
    SqlCommand sCommand = null;
    SqlDataReader sReader = null;
    SqlDataAdapter sadp = null;
    public static string grade = "";
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
        }
        if (!Page.IsPostBack)
        {
            txtnoofDays.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
            hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();
            //hdnloginempcode.Value = "00630134";
            check_IsloginHR();
            getLeaveTypes();
            getYear();
            grade = "";

            DataSet dt = GetEmployeeData(Convert.ToString(Session["Empcode"]).Trim());
            if (dt.Tables[0] != null)
            {
                if (dt.Tables[1].Rows.Count > 0)
                {
                    grade = dt.Tables[1].Rows[0]["grade"].ToString();  //"UC"; 
                    //hdnGrade.Value = dt.Rows[0]["EMP_A_GRADE"].ToString();
                }
            }

        }

    }

    public DataSet GetEmployeeData(string empcode)
    {
        try
        {

            DataSet dtEmployee = new DataSet();
            SqlParameter[] spars = new SqlParameter[8];


            spars[0] = new SqlParameter("@EMP_CODE", SqlDbType.VarChar);
            spars[0].Value = empcode;  // "00008727";

            dtEmployee = spm.getDatasetList(spars, "SP_GETEMPDETAILS");



            //sql.con.ConnectionString = connection;
            //sql.con.Open();
            //sql.cmd.Connection = sql.con;
            //sql.cmd.CommandType = CommandType.StoredProcedure;
            //sql.cmd.CommandText = "SP_GETEMPDETAILS";
            //sql.cmd.Parameters.AddWithValue("@EMP_CODE", empcode);
            //sql.adp.SelectCommand = sql.cmd;
            //dtEmployee = new DataTable();
            //sql.adp.Fill(dtEmployee);



            return dtEmployee;


        }
        catch (Exception ex)
        {

            throw;
        }
        finally
        {
            sql.con.Close();

        }

    }


    private void getemployee_leaveDetails()
    {
        try
        {
            hdnSearchempcode.Value = "";
            String[] stremp;
            string selected_Employee_Name = "";
            if (hdnIsHr.Value == "HR")
            {
                stremp = Convert.ToString(txtempname.Text).Split('-');
                if (Convert.ToString(txtempname.Text).Trim() != "")
                {
                    if (stremp.Length <= 1)
                    {
                        lblmsg.Text = "Please select employee Name";
                        return;
                    }
                    hdnSearchempcode.Value = Convert.ToString(stremp[1]).Trim();
                    selected_Employee_Name = Convert.ToString(stremp[0]).Trim();
                }
            }
            else
            {
                stremp = Convert.ToString(txtempnameNonHr.Text).Split('-');
                if (Convert.ToString(txtempnameNonHr.Text).Trim() != "")
                {
                    if (stremp.Length <= 1)
                    {
                        lblmsg.Text = "Please select employee Name";
                        return;
                    }
                    hdnSearchempcode.Value = Convert.ToString(stremp[1]).Trim();
                    selected_Employee_Name = Convert.ToString(stremp[0]).Trim();
                }
            }

            string[] frmdate = txtFrmDate.Text.Split('/');
            string[] todate = txtToDate.Text.Split('/');


            #region get employee Leave details
            DataSet dsempLeaves = new DataSet();
            // dsempLeaves = null;
            SqlParameter[] spars = new SqlParameter[9];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);

            if (hdnIsHr.Value == "HR")
                spars[0].Value = "emp_AttendanceDtls_HR";
            else
                spars[0].Value = "emp_AttendanceDtls_NONHR";


            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            if (Convert.ToString(hdnSearchempcode.Value).Trim() != "" )
                spars[1].Value = Convert.ToString(hdnSearchempcode.Value);
            else
                spars[1].Value = DBNull.Value;

            spars[2] = new SqlParameter("@leavetypeid", SqlDbType.Int);
            if (Convert.ToString(ddlleaveTypeHR.SelectedValue).Trim() != "0")
                spars[2].Value = Convert.ToInt32(ddlleaveTypeHR.SelectedValue);
            else
                spars[2].Value = DBNull.Value;

            //spars[3] = new SqlParameter("@year", SqlDbType.VarChar);
            //if (Convert.ToString(ddlYear.SelectedValue).Trim() != "")
            //{
            //    spars[3].Value = Convert.ToString(ddlYear.SelectedValue);
            //}
            //else
            //    spars[3].Value = Convert.ToString("0");

            spars[3] = new SqlParameter("@frmyear", SqlDbType.VarChar);
            spars[3].Value = frmdate[1].ToString();

            spars[4] = new SqlParameter("@noleavedays", SqlDbType.Decimal);
            if (Convert.ToString(txtnoofDays.Text).Trim() != "" || Convert.ToString(txtnoofDays.Text).Trim() != "0")
                spars[4].Value = Convert.ToDouble(0+txtnoofDays.Text);
            else
                spars[4].Value = DBNull.Value;

            spars[5] = new SqlParameter("@loginempcode", SqlDbType.VarChar);
            spars[5].Value = hdnloginempcode.Value;

            spars[6] = new SqlParameter("@FrmMonth", SqlDbType.VarChar);
            spars[6].Value = ReturnMonth(frmdate[0].ToString());

            spars[7] = new SqlParameter("@ToMonth", SqlDbType.VarChar);
            spars[7].Value = ReturnMonth(todate[0].ToString());


            spars[8] = new SqlParameter("@toyear", SqlDbType.VarChar);
            spars[8].Value = todate[1].ToString();


            dsempLeaves = spm.getDatasetList(spars, "rpt_dataProcedure");

            #endregion


            if (dsempLeaves.Tables[0].Rows.Count > 0)
            {
                ReportViewer1.Visible = true;
                //ReportViewer1.LocalReport.Refresh();
                //   ReportViewer1.LocalReport.DataSources.Clear();




                // Create Report DataSource

                if (Convert.ToString(txtempname.Text).Trim() != "" || Convert.ToString(txtempnameNonHr.Text).Trim() != "")
                {
                    ReportParameter[] param = new ReportParameter[3];
                    param[0] = new ReportParameter("pyear", "From " + frmdate[0] + "-" + frmdate[1] + "  To - " + todate[0] + "-" + todate[1]);
                    param[1] = new ReportParameter("EmpCode", "Employee Code: " + hdnSearchempcode.Value);
                    param[2] = new ReportParameter("EmpName", "Employee Name: " + selected_Employee_Name);
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/AttendanceReport.rdlc");
                    ReportDataSource rds = new ReportDataSource("dsempleaverpt", dsempLeaves.Tables[0]);
                    ReportDataSource rds1 = new ReportDataSource("dsleave", dsempLeaves.Tables[2]);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    ReportViewer1.LocalReport.DataSources.Add(rds1);
                    ReportViewer1.LocalReport.SetParameters(param);
                    ReportViewer1.LocalReport.Refresh();
                }
                else
                {
                    ReportParameter[] param = new ReportParameter[1];
                    param[0] = new ReportParameter("pyear", "From " + frmdate[0] + "-" + frmdate[1] + "  To - " + todate[0] + "-" + todate[1]);
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/AttendanceReportMgr.rdlc");
                    ReportDataSource rds = new ReportDataSource("dsempleaverpt", dsempLeaves.Tables[0]);
                    //ReportDataSource rds1 = new ReportDataSource("dsleave", dsempLeaves.Tables[2]);

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    //ReportViewer1.LocalReport.DataSources.Add(rds1);
                    ReportViewer1.LocalReport.SetParameters(param);
                    ReportViewer1.LocalReport.Refresh();
                }


                
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "No Regularizations Found!";
                ReportViewer1.Visible = false;
                // ReportViewer1.LocalReport.DataSources.Clear();
                // ReportViewer1.LocalReport.Refresh();
            }


        }
        catch (Exception ex)
        {
        }
    }


    private int ReturnMonth(string month)
    {
        int mon;
        switch (month.ToUpper())
        {


            case "JAN": mon = 1;
                break;
            case "FEB": mon = 2;
                break;
            case "MAR": mon = 3;
                break;
            case "APR": mon = 4;
                break;
            case "MAY": mon = 5;
                break;
            case "JUN": mon = 6;
                break;
            case "JUL": mon = 7;
                break;
            case "AUG": mon = 8;
                break;
            case "SEP": mon = 9;
                break;
            case "OCT": mon = 10;
                break;
            case "NOV": mon = 11;
                break;
            case "DEC": mon = 12;
                break;

            default: mon = 0;
                break;
        }

        return mon;
    }


    private void getLeaveTypes()
    {
        try
        {
            lblmsg.Visible = false;



            #region get employee Leave details
            DataSet dsLeaveType = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "emp_Attendancetypes";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hdnloginempcode.Value);

            dsLeaveType = spm.getDatasetList(spars, "rpt_dataProcedure");
            if (dsLeaveType.Tables[0].Rows.Count > 0)
            {
                ddlleaveTypeHR.DataSource = dsLeaveType.Tables[0];
                ddlleaveTypeHR.DataTextField = "LeaveTypeName";
                ddlleaveTypeHR.DataValueField = "Leavetype_id";
                ddlleaveTypeHR.DataBind();
                ListItem item = new ListItem("ALL", "0");
                ddlleaveTypeHR.Items.Insert(0, item);

            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "No leave data for the year";
            }
            #endregion
        }
        catch (Exception ex)
        {

        }
    }

    private void getYear()
    {
        try
        {
            lblmsg.Visible = false;
            #region get employee Leave details
            DataSet dsLeaveYear = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "emp_leaveyear";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hdnloginempcode.Value);

            dsLeaveYear = spm.getDatasetList(spars, "rpt_dataProcedure");
            if (dsLeaveYear.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = dsLeaveYear.Tables[0];
                ddlYear.DataTextField = "EmpYear";
                ddlYear.DataValueField = "EmpYear";
                ddlYear.DataBind();
                //ListItem item = new ListItem("ALL", "0");
                ListItem item = new ListItem();
                //ddlYear.Items.Insert(0, item);

            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "No leave data for the year";
            }
            #endregion
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if (txtempname.Text.Trim() == "" && txtempnameNonHr.Text.Trim() == "")
        //{
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Enter employee Name sss";
        //    return;
        //}
        lblmsg.Visible = false;
        if (hdnIsHr.Value == "NHR")
        {
            if (txtempnameNonHr.Text.Trim() == "")
            {
                lblmsg.Visible = true;
                  lblmsg.Text = "Enter employee Name";
                   return;

            }
        }
        if (txtFrmDate.Text.Trim() == "")
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Enter From Month";
            return;
        }


        else if (txtToDate.Text.Trim() == "")
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Enter To Month";
            return;
        }

        else if ((txtempname.Text.Trim() != "" || txtempnameNonHr.Text.Trim() != "") && txtFrmDate.Text.Trim() != "" && txtToDate.Text.Trim() != "")
        {
            string[] frmdate = txtFrmDate.Text.Split('/');
            string[] todate = txtToDate.Text.Split('/');

            if (Convert.ToInt32(todate[1]) < Convert.ToInt32(frmdate[1]))
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Year of From month can not be less than To month";
                return;
            }


            if (Convert.ToInt32(todate[1]) == Convert.ToInt32(frmdate[1]))
            {
                if (ReturnMonth(todate[0]) < ReturnMonth(frmdate[0]))
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "From Month can not be greater than To Month";
                    return;

                }
            }
        }


        lblmsg.Visible = false;


        if (hdnIsHr.Value != "HR" && (grade != "UC" || grade != "DIR"))
        {
            DataTable ds = new DataTable();
            String[] stremp;
            stremp = Convert.ToString(txtempnameNonHr.Text).Split('-');

            if (stremp.Length > 1)
            {

                string Query = @"Select t.empname,t.Emp_Code from  (   Select Emp_Name as empname  ,Emp_Code   from tbl_Employee_Mst     where emp_status='Onboard'   ";
               // Query += "   and emp_location in (select Distinct location from tbl_htravel_leave_extra_approver where approver_emp_code like '" + Convert.ToString(HttpContext.Current.Session["Empcode"]) + "' )  ";
                Query += "  and Emp_Code in  ( Select Distinct em.Emp_Code from tbl_Employee_OMStructure om inner join tbl_Employee_Mst em on om.EMP_CODE=em.Emp_Code ";
                Query += "    where A_EMP_CODE='" + Convert.ToString(HttpContext.Current.Session["Empcode"]) + "'  and APPR_CODE='RM' and em.emp_status='Onboard' 	union 	Select Distinct c.Emp_Code 	From tbl_Employee_OMStructure a  ";
                Query += "    inner join tbl_Employee_Mst c on a.EMP_CODE=c.Emp_Code  where A_EMP_CODE in (		Select Distinct em.Emp_Code 	from tbl_Employee_OMStructure om ";
                Query += "    inner join tbl_Employee_Mst em on om.EMP_CODE=em.Emp_Code 	where A_EMP_CODE='" + Convert.ToString(HttpContext.Current.Session["Empcode"]) + "'  and APPR_CODE='RM' and em.emp_status='Onboard'    ) ";
                Query += "   and APPR_CODE ='RM' ";
                Query += "   )) t    where t.Emp_Code = '" + stremp[1].ToString().Trim() + "'  Order by t.empname ";

                ds = spm.getDataList_SQL(Query);

                if (ds != null)
                {
                    if (ds.Rows.Count > 0)
                    {
                        getemployee_leaveDetails();
                    }
                    else
                    {
                        lblmsg.Visible = true;
                        lblmsg.Text = "No Regularizations found!";
                    }
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "No Regularizations found!";
                }
            }

        }
        else
        {

            getemployee_leaveDetails();
        }


    }


    protected void check_IsloginHR()
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_Check_HR_apprver_code";

            spars[1] = new SqlParameter("@exp_trvl", SqlDbType.VarChar);
            spars[1].Value = DBNull.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = hdnloginempcode.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");
            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                txtempname.Visible = true;
                txtempnameNonHr.Visible = false;
                hdnIsHr.Value = "HR";

            }
            else
            {

                txtempname.Visible = false;
                txtempnameNonHr.Visible = true;
                hdnIsHr.Value = "NHR";
                // Response.Redirect(ReturnUrl("sitepathmain") + "procs/Leaves.aspx");

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }



    #region Search Employees


    [System.Web.Services.WebMethod]
    public static List<string> SearchEmployees(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";


                // Page page = (Page)HttpContext.Current.Handler;
                // HiddenField hdnGrade = (HiddenField)page.FindControl("hdnGrade");

                /*strsql = " select Distinct location from addressbook where  " +
                                "  location like   '%' + @SearchText + '%' order by location ";*/

                if (grade == "UC" || grade == "DIR")
                {

                    strsql = @" Select t.empname from  ( Select Distinct c.Emp_Code, Emp_Name + ' - '  + c.Emp_Code as empname 	From tbl_Employee_OMStructure a inner join tbl_Employee_Mst c on a.EMP_CODE=c.Emp_Code 
	                    where A_EMP_CODE in (select Distinct em.Emp_Code 	from tbl_Employee_OMStructure om inner join tbl_Employee_Mst em on om.EMP_CODE=em.Emp_Code 
	                    where  em.emp_status='Onboard' ) )t  where t.empname like '%' + @SearchText + '%'   Order by t.empname ";

                    cmd.CommandText = strsql;
                    cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                    //cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));
                }
                else
                {
                    strsql = "  Select t.empname from  ( " +
                               "  Select Emp_Name + ' - '  +Emp_Code as empname " +
                               "  from tbl_Employee_Mst  " +
                               "   where emp_status='Onboard' " +
                               "   and emp_location in (select Distinct location from tbl_htravel_leave_extra_approver where approver_emp_code like '%'+ @empcode  +'%' and approver_type in ('HRLWP','HRML')) " +


                  " ) t " +
                               "   where t.empname like '%' + @SearchText + '%'   Order by t.empname ";

                    cmd.CommandText = strsql;
                    cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                    cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));

                }


                cmd.Connection = conn;
                conn.Open();
                List<string> employees = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        employees.Add(sdr["empname"].ToString());
                    }
                }
                conn.Close();
                return employees;
            }
        }
    }



    [System.Web.Services.WebMethod]
    public static List<string> SearchEmployeesNonHR(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";


                // Page page = (Page)HttpContext.Current.Handler;
                // HiddenField hdnGrade = (HiddenField)page.FindControl("hdnGrade");

                /*strsql = " select Distinct location from addressbook where  " +
                                "  location like   '%' + @SearchText + '%' order by location ";*/

                if (grade == "UC" || grade == "DIR")
                {

                    strsql = @" Select t.empname from  ( Select Distinct c.Emp_Code, Emp_Name + ' - '  + c.Emp_Code as empname 	From  tbl_Employee_Mst c  )t  where t.empname like '%' + @SearchText + '%'   Order by t.empname ";

                    cmd.CommandText = strsql;
                    cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                    //cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));
                }
                else
                {
                    strsql = "  Select t.empname from  ( " +
                               "  Select Emp_Name + ' - '  +Emp_Code as empname " +
                               "  from tbl_Employee_Mst  " +
                               "   where emp_status='Onboard' " +
                        // "   and emp_location in (select Distinct location from tbl_htravel_leave_extra_approver where approver_emp_code like '%'+ @empcode  +'%') " +
                               "   and Emp_Code in  ( 		" +
                              "	Select Distinct em.Emp_Code " +
                                  "from tbl_Employee_OMStructure om inner join tbl_Employee_Mst em on om.EMP_CODE=em.Emp_Code " +
                              "	where A_EMP_CODE=@empcode  and APPR_CODE='RM' and em.emp_status='Onboard' " +
                              "	union " +
                              "	Select Distinct c.Emp_Code " +
                              "	From tbl_Employee_OMStructure a inner join tbl_Employee_Mst c on a.EMP_CODE=c.Emp_Code " +
                                  " where A_EMP_CODE in (" +
                                                  "		Select Distinct em.Emp_Code " +
                                                      "	from tbl_Employee_OMStructure om inner join tbl_Employee_Mst em on om.EMP_CODE=em.Emp_Code " +
                                                      "	where A_EMP_CODE='+@empcode +'  and APPR_CODE='RM' and em.emp_status='Onboard' " +
                                                  "   ) and APPR_CODE ='RM' " +

                  " )) t " +
                               "   where t.empname like '%' + @SearchText + '%'   Order by t.empname ";

                    cmd.CommandText = strsql;
                    cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                    cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));

                }


                cmd.Connection = conn;
                conn.Open();
                List<string> employees = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        employees.Add(sdr["empname"].ToString());
                    }
                }
                conn.Close();
                return employees;
            }
        }
    }



    #endregion

}