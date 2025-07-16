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

public partial class ExitProcessReport : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    bool isMGR = false;
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
        }
        lblmessage.Text = "";
        if (!Page.IsPostBack)
        {
            var isCEO = false;
            var isHOD = false;
            hflCEO.Value = "NO";
            hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();
            hflEmpCode.Value = Convert.ToString(Session["Empcode"]).Trim();
            txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            txtFromdate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
            txtToDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            txtToDate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");

            ////hflEmpCode.Value = Convert.ToString(Session["Empcode"]);
            ////DataTable dtApprovers = new DataTable();
            ////dtApprovers = spm.CheckHOD(Convert.ToString(hflEmpCode.Value).Trim());
            ////if (dtApprovers.Rows.Count > 0)
            ////{
            ////    ddlDepartment.Enabled = false;
            ////}
            ////else
            ////{
            ////    ddlDepartment.Enabled = true;
            ////}
            ddlDepartment.Enabled = false;
            GetEmployeeDetails();

            var getLoginEmpDeg = hflEmpDesignation.Value;
           
            if (getLoginEmpDeg.Contains("Head"))
            {
                isCEO = true;
                isHOD = true;
            }

            DataTable dtApprovers = new DataTable();
            dtApprovers = spm.CheckHOD(Convert.ToString(hflEmpCode.Value).Trim());
            if (dtApprovers.Rows.Count > 0)
            {

                isMGR = false;
            }
            else
            {
                isMGR = true;
            }
            if (isMGR == true)
            {
                ddlDepartment.Visible = false;
                spn_dept.Visible = false;
            }
            else
            {
                ddlDepartment.Visible = true;
                spn_dept.Visible = true;
            }
            DataTable dtleaveInbox2 = new DataTable();
            dtleaveInbox2 = spm.GetCEOEmpCode();
            if (dtleaveInbox2.Rows.Count > 0)
            {
                var loginCode = Convert.ToString(hflEmpCode.Value);
                var CeoEmpCode = Convert.ToString(dtleaveInbox2.Rows[0]["Emp_Code"]);
                if (loginCode == CeoEmpCode)
                {
                    isCEO = true;
                    hflCEO.Value = "YES";
                    ddlDepartment.Enabled = true;
                }
                else
                {
                    isCEO = false;
                    hflCEO.Value = "NO";
                }    
            }

            if (check_IsloginHR())
            {
                isCEO = true;
                hflCEO.Value = "YES";
                ddlDepartment.Enabled = true;
            }

            if (Convert.ToString(hflEmpCode.Value) == "00002082")
            {
                ddlDepartment.Enabled = true;
            }

            if (Convert.ToString(hflEmpCode.Value) == "00631294" || Convert.ToString(hflEmpCode.Value) == "00630814" || Convert.ToString(hflEmpCode.Value) == "00003851" || Convert.ToString(hflEmpCode.Value) == "00630967" || Convert.ToString(hflEmpCode.Value) == "00631551" || Convert.ToString(hflEmpCode.Value) == "00631230"|| Convert.ToString(hflEmpCode.Value)== "00631087" || Convert.ToString(hflEmpCode.Value) == "00631230" || Convert.ToString(hflEmpCode.Value) == "00630008" || Convert.ToString(hflEmpCode.Value) == "00630674" || Convert.ToString(hflEmpCode.Value) == "00631551" || Convert.ToString(hflEmpCode.Value) == "00631300" || Convert.ToString(hflEmpCode.Value) == "00631257" || Convert.ToString(hflEmpCode.Value) == "00631276" || Convert.ToString(hflEmpCode.Value) == "00631710" || Convert.ToString(hflEmpCode.Value) == "00631609")
            {
                isCEO = true;
                isHOD = true;
                isMGR = false;
                hflCEO.Value = "YES";
                spn_dept.Visible = true;
                ddlDepartment.Visible = true;
                ddlDepartment.Enabled = true;
            }
  

            //if(isCEO==false)
            //    Response.Redirect("~/procs/Service.aspx");

            var getDepartment = hflEmpDepartment.Value;

            loadDropDownDepartment(getDepartment);
            //hdnloginempcode.Value = "00630134";     
            //if(isHOD==true)
            //{
            //    var getval = Convert.ToString(ddlDepartment.SelectedValue).Trim();
            //    if (getval == "2")
            //    {
            //        ddlDepartment.Items.FindByValue("8").Enabled = false;
            //        ddlDepartment.Items.FindByValue("10").Enabled = false;
            //        ddlDepartment.Items.FindByValue("0").Enabled = false;
            //        ddlDepartment.Items.FindByValue("2").Enabled = true;
            //    }
            //    else
            //    {
            //        ddlDepartment.Items.FindByValue("2").Enabled = false;
            //        ddlDepartment.Items.FindByValue("8").Enabled = true;
            //        ddlDepartment.Items.FindByValue("10").Enabled = true;
            //        ddlDepartment.Items.FindByValue("0").Enabled = false;
            //    }
            //}
            BindEmployeedetails();
            BindStatusDetails();
            hideDepartment.Visible = false;
        }

    }

    protected bool check_IsloginHR()
    {
        bool hr_code=false;
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
                hr_code= true;
            }
            else
            {
                hr_code = false;
                
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
        return hr_code;
    }

    public void GetEmployeeDetails()
    {
        try
        {
            DataTable dtEmpDetails = new DataTable();
            dtEmpDetails = spm.GetEmployeeData(Convert.ToString(hflEmpCode.Value));
            if (dtEmpDetails.Rows.Count > 0)
            {
                hflEmpDesignation.Value = Convert.ToString(dtEmpDetails.Rows[0]["DesginationName"]);
                hflEmpDepartment.Value = Convert.ToString(dtEmpDetails.Rows[0]["Department_Name"]);
                hflGrade.Value = Convert.ToString(dtEmpDetails.Rows[0]["Grade"]).Trim();
                hflEmailAddress.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
            }
            //  getApproverdata();  
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

            throw;
        }
    }
    private void getemployee_ExitSurveyDetails()
    {
        try
        {
            #region get employee Claim details
            DataSet dsExitSurveyDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            string strfromDate_RPt = "";
            string strToDate_RPt = "";

            spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
            spars[0].Value = "ExitProcess_SurveyForm_Report";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            if(Convert.ToString(ddlEmployee.SelectedValue)=="0")
            {
                spars[1].Value = null;
            }
            else
            {
                spars[1].Value = Convert.ToString(ddlEmployee.SelectedValue);
            }
            spars[2] = new SqlParameter("@Status_id", SqlDbType.Int);
            if (Convert.ToString(ddlStatus.SelectedValue) == "0")
            {
                spars[2].Value = null;
            }
            else
            {
                spars[2].Value = Convert.ToInt32(ddlStatus.SelectedValue);
            }
            dsExitSurveyDetails = spm.getDatasetList(spars, "spExitSurveyForm");

            #endregion

            ReportViewer2.LocalReport.Refresh();
            ReportViewer2.LocalReport.DataSources.Clear();
            ReportParameter[] param = new ReportParameter[2];
            param[0] = new ReportParameter("FromDate", Convert.ToString(strfromDate_RPt));
            param[1] = new ReportParameter("ToDate", Convert.ToString(strToDate_RPt));

            if (dsExitSurveyDetails.Tables[0].Rows.Count > 0)
            {
                ReportDataSource rdlEmployeeDetails = new ReportDataSource("dsEmployeeDetails", dsExitSurveyDetails.Tables[0]);
                ReportDataSource rdlQuestion_1_7_8_9_10 = new ReportDataSource("dsQuestion_1_7_8_9_10", dsExitSurveyDetails.Tables[1]);
                ReportDataSource rdlQuestion_2 = new ReportDataSource("dsQuestion_2", dsExitSurveyDetails.Tables[2]);
                ReportDataSource rdlQuestion_3 = new ReportDataSource("dsQuestion_3", dsExitSurveyDetails.Tables[3]);
				ReportDataSource rdlQuestion_4 = new ReportDataSource("dsQuestion_4", dsExitSurveyDetails.Tables[4]);
				ReportDataSource rdlQuestion_5 = new ReportDataSource("dsQuestion_5", dsExitSurveyDetails.Tables[5]);
				ReportDataSource rdlQuestion_6 = new ReportDataSource("dsQuestion_6", dsExitSurveyDetails.Tables[6]);
				ReportViewer2.ProcessingMode = ProcessingMode.Local;
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/ExitProcessSurvey.rdlc");
                ReportViewer2.LocalReport.DataSources.Add(rdlEmployeeDetails);
                ReportViewer2.LocalReport.DataSources.Add(rdlQuestion_1_7_8_9_10);
                ReportViewer2.LocalReport.DataSources.Add(rdlQuestion_2);
                ReportViewer2.LocalReport.DataSources.Add(rdlQuestion_3);
				ReportViewer2.LocalReport.DataSources.Add(rdlQuestion_4);
				ReportViewer2.LocalReport.DataSources.Add(rdlQuestion_5);
				ReportViewer2.LocalReport.DataSources.Add(rdlQuestion_6);
				ReportViewer2.LocalReport.SetParameters(param);
            }

        }
        catch (Exception ex)
        {
        }
    }

     public void Compare()
    {
        
    }
    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";

        if ((Convert.ToString(txtFromdate.Text).Trim() != "") && (Convert.ToString(txtToDate.Text).Trim() != ""))
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

            strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);


            DateTime startDate = Convert.ToDateTime(strfromDate);
            DateTime endDate = Convert.ToDateTime(strToDate);
            if (startDate > endDate)
            {
                lblmessage.Text = "From Date should be less than To Date ";
                txtFromdate.Text = "";

                return;
            }
            else
            {
                lblmessage.Text = "";
                // ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Start date should be greate than End date')", true);
            }
        }
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";

        if ((Convert.ToString(txtFromdate.Text).Trim() != "") && (Convert.ToString(txtToDate.Text).Trim() != ""))
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

            strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);


            DateTime startDate = Convert.ToDateTime(strfromDate);
            DateTime endDate = Convert.ToDateTime(strToDate);
            if (startDate > endDate)
            {
                lblmessage.Text = "To Date should be greater than From Date ";
                txtToDate.Text = "";
                
                return;
            }
            else
            {
                // ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Start date should be greate than End date')", true);
            }
        }
    }

    //protected void lstFromfor_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtClaimStatus.Text = lstFromfor.SelectedValue;        
    //    PopupControlExtender1.Commit(lstFromfor.SelectedValue);
       

    
    
    //}
    protected void btnSave_Click(object sender, EventArgs e)
    {
        getemployee_ExitSurveyDetails();

    }



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

//                if (grade == "UC" || grade == "DIR")
//                {

//                    strsql = @" Select t.empname from  ( Select Distinct c.Emp_Code, Emp_Name + ' - '  + c.Emp_Code as empname 	From tbl_Employee_OMStructure a inner join tbl_Employee_Mst c on a.EMP_CODE=c.Emp_Code 
//	                    where A_EMP_CODE in (select Distinct em.Emp_Code 	from tbl_Employee_OMStructure om inner join tbl_Employee_Mst em on om.EMP_CODE=em.Emp_Code 
//	                    where  em.emp_status='Onboard' ) )t  where t.empname like '%' + @SearchText + '%'   Order by t.empname ";

//                    cmd.CommandText = strsql;
//                    cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
//                    //cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));
//                }
//                else
//                {
                    strsql = "  Select t.empname from  ( " +
                               "  Select Emp_Code + ' - '  +Emp_Name as empname " +
                               "  from tbl_Employee_Mst  " +
                               "   where emp_status='Onboard' " +
                               "    " +
                               " ) t " +
                               " where t.empname like '%' + @SearchText + '%'   Order by t.empname ";

                    cmd.CommandText = strsql;
                    cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                //}


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

    public void loadDropDownDepartment(string selectVal)
    {
        DataTable dtleaveInbox = new DataTable();
        //dtleaveInbox = spm.GetSerivesRequestDepartment();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Emp_Department";

        dtleaveInbox = spm.getDropdownList(spars, "rpt_dataProcedure");
        if (dtleaveInbox.Rows.Count > 0)
        {
            ddlDepartment.DataSource = dtleaveInbox;
            //ddlDepartment.DataBind();
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentId";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("Select Department", "0")); //updated code

            DataRow[] dr = dtleaveInbox.Select("DepartmentName = '" + selectVal.ToString() + "'");
            if (dr.Length > 0)
            {
                string avalue = dr[0]["DepartmentId"].ToString();
                hflEmpDepartmentID.Value = avalue;
                ddlDepartment.SelectedValue = avalue;

            }
            else
            {
                hflEmpDepartmentID.Value = "0";
            }
        }
    }

    [System.Web.Services.WebMethod]
    public static List<string> SearchRequestId(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";


                strsql = "  select ServicesRequestID from tbl_ServiceRequestDetails WHERE ServicesRequestID LIKE '%' + @SearchText + '%'   Order by Id ";

                cmd.CommandText = strsql;
                cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                //}


                cmd.Connection = conn;
                conn.Open();
                List<string> ServicesRequestID = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ServicesRequestID.Add(sdr["ServicesRequestID"].ToString());
                    }
                }
                conn.Close();
                return ServicesRequestID;
            }
        }
    }
    public void BindEmployeedetails()
    {
        DataTable dtleaveInbox = new DataTable();
        SqlParameter[] spars = new SqlParameter[1];
        spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
        spars[0].Value = "getEmployeeListForResigned";
        dtleaveInbox = spm.getDropdownList(spars, "spExitSurveyForm");
        if (dtleaveInbox.Rows.Count > 0)
        {
            ddlEmployee.DataSource = dtleaveInbox;
            //ddlDepartment.DataBind();
            ddlEmployee.DataTextField = "Emp_Name";
            ddlEmployee.DataValueField = "Emp_Code";
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, new ListItem("Select Employee", "0"));
        }
    }
    public void BindStatusDetails()
    {
        DataTable dtleaveInbox = new DataTable();
        SqlParameter[] spars = new SqlParameter[1];
        spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
        spars[0].Value = "getExitProcessStatus";
        dtleaveInbox = spm.getDropdownList(spars, "spExitSurveyForm");
        if (dtleaveInbox.Rows.Count > 0)
        {
            ddlStatus.DataSource = dtleaveInbox;
            //ddlDepartment.DataBind();
            ddlStatus.DataTextField = "Request_status";
            ddlStatus.DataValueField = "Status_id";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("Select Status", "0"));
        }
    }

}