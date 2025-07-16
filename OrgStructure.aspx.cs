using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class OrgStructure : System.Web.UI.Page
{
    public static string emp_code = "";
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    emp_code = "";
                    if (Request.QueryString.Count > 0)
                    {
                        emp_code = Convert.ToString(Request.QueryString[0]).Trim();
                    }
                    
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    [System.Web.Services.WebMethod]
    public static List<object> GetChartData()
    {
        string query = "";
        if (emp_code.ToString() == "")
        {
            //string query = "SELECT EmployeeId, Name, Designation, ReportingManager";
            query = "SELECT case when e.EMPLOYMENT_TYPE = 1 then CAST(cast(e1.EMP_CODE as int) as varchar(10)) else e1.EMP_CODE end as EmployeeId ,";
            query += " e.Emp_Name as Name ,e.Designation as Designation,";
            query += " case when ee.EMPLOYMENT_TYPE = 1 then CAST(cast(e1.A_EMP_CODE as int) as varchar(10)) else e1.A_EMP_CODE end as ReportingManager ";
            query += " , (select COUNT(*) as cnt from tbl_Employee_OMStructure where A_EMP_CODE = e1.EMP_CODE and APPR_ID = 1 and EMP_CODE != A_EMP_CODE and Emp_Code not in (select Emp_Code from tbl_Employee_Mst where emp_status != 'Onboard')) as cnt, e1.EMP_CODE, e1.A_EMP_CODE";
            query += " FROM tbl_Employee_OMStructure e1";
            //query += " LEFT JOIN tbl_Employee_OMStructure e2 ON e1.A_EMP_CODE = e2.EMP_CODE and e2.APPR_ID = 1";
            query += " inner join tbl_Employee_Mst e on e1.EMP_CODE = e.Emp_Code and e.emp_status = 'Onboard'";
            query += " inner join tbl_Employee_Mst ee on e1.A_EMP_CODE = ee.Emp_Code and ee.emp_status = 'Onboard'";
            //query += " where e1.APPR_ID = 1 and e1.EMP_CODE in (select distinct(HOD) from tblDepartmentMaster)";
            //query += " or e1.A_EMP_CODE in (select distinct(HOD) from tblDepartmentMaster))";
            query += " where e1.APPR_ID = 1 and e1.A_EMP_CODE in (select EMP_CODE from tbl_Employee_OMStructure where Emp_Code = A_EMP_CODE)";
            query += " and e1.Emp_Code not in (select Emp_Code from tbl_Employee_Mst where emp_status != 'Onboard')";
        }
        else
        {
             query = "SELECT case when e.EMPLOYMENT_TYPE = 1 then CAST(cast(e1.EMP_CODE as int) as varchar(10)) else e1.EMP_CODE end as EmployeeId ,";
             query += " e.Emp_Name as Name ,e.Designation as Designation,";
             query += " case when e.EMPLOYMENT_TYPE = 1 then CAST(cast(e1.EMP_CODE as int) as varchar(10)) else e1.EMP_CODE end as ReportingManager ";
             query += " , (select COUNT(*) as cnt from tbl_Employee_OMStructure where A_EMP_CODE = e1.EMP_CODE and APPR_ID = 1 and EMP_CODE != A_EMP_CODE and Emp_Code not in (select Emp_Code from tbl_Employee_Mst where emp_status != 'Onboard')) as cnt, e1.EMP_CODE, e1.A_EMP_CODE";
             query += " FROM tbl_Employee_OMStructure e1";
             query += " inner join tbl_Employee_Mst e on e1.EMP_CODE = e.Emp_Code and e.emp_status = 'Onboard'";
             query += " inner join tbl_Employee_Mst ee on e1.A_EMP_CODE = ee.Emp_Code and ee.emp_status = 'Onboard'";
             query += " where e1.APPR_ID = 1 and e1.EMP_CODE in ('" + emp_code.ToString() + "')";
             query += " and e1.Emp_Code not in (select Emp_Code from tbl_Employee_Mst where emp_status != 'Onboard')";
             query += " union";
            //string query = "SELECT EmployeeId, Name, Designation, ReportingManager";
            query += " SELECT case when e.EMPLOYMENT_TYPE = 1 then CAST(cast(e1.EMP_CODE as int) as varchar(10)) else e1.EMP_CODE end as EmployeeId ,";
            query += " e.Emp_Name as Name ,e.Designation as Designation,";
            query += " case when ee.EMPLOYMENT_TYPE = 1 then CAST(cast(e1.A_EMP_CODE as int) as varchar(10)) else e1.A_EMP_CODE end as ReportingManager ";
            query += " , (select COUNT(*) as cnt from tbl_Employee_OMStructure where A_EMP_CODE = e1.EMP_CODE and APPR_ID = 1 and EMP_CODE != A_EMP_CODE and Emp_Code not in (select Emp_Code from tbl_Employee_Mst where emp_status != 'Onboard')) as cnt, e1.EMP_CODE, e1.A_EMP_CODE";
            query += " FROM tbl_Employee_OMStructure e1";
            //query += " LEFT JOIN tbl_Employee_OMStructure e2 ON e1.A_EMP_CODE = e2.EMP_CODE and e2.APPR_ID = 1";
            query += " inner join tbl_Employee_Mst e on e1.EMP_CODE = e.Emp_Code and e.emp_status = 'Onboard'";
            query += " inner join tbl_Employee_Mst ee on e1.A_EMP_CODE = ee.Emp_Code and ee.emp_status = 'Onboard'";
            //query += " where e1.APPR_ID = 1 and e1.EMP_CODE in (select distinct(HOD) from tblDepartmentMaster)";
            //query += " or e1.A_EMP_CODE in (select distinct(HOD) from tblDepartmentMaster))";
            query += " where e1.APPR_ID = 1 and e1.A_EMP_CODE in ('" + emp_code.ToString() + "')";
            query += " and e1.Emp_Code not in (select Emp_Code from tbl_Employee_Mst where emp_status != 'Onboard')";
        }
        string constr = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                List<object> chartData = new List<object>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        chartData.Add(new object[]
                    {
                         sdr["EmployeeId"], sdr["Name"], sdr["Designation"] , sdr["ReportingManager"] , sdr["cnt"], sdr["EMP_CODE"], sdr["A_EMP_CODE"]
                    });
                    }
                }
                con.Close();
                return chartData;
            }
        }
    }
}