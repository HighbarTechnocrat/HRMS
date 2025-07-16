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

public partial class OrgEmployee_Report : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    
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
            getemployee_ReimbursmentDetails();
            hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();
            //hdnloginempcode.Value = "00630134";        
        }

    }

    private void getemployee_ReimbursmentDetails()
    {
        try
        {
            #region get employee Claim details
            DataSet dsempreimburstment = new DataSet();
            SqlParameter[] spars = new SqlParameter[1];
            string[] strdate;
            string strfromDate = "";
            string strToDate = "";

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            //spars[0].Value = "Allemp_paymntmbilefuel_reimbursmnt";
            spars[0].Value = "OrgEmployee_Report";

            dsempreimburstment = spm.getDatasetList(spars, "rpt_dataProcedure");

            #endregion

            ReportViewer2.LocalReport.Refresh();
            ReportViewer2.LocalReport.DataSources.Clear();

            if (dsempreimburstment.Tables[0].Rows.Count > 0)
            {
                ReportDataSource rdsFuel = new ReportDataSource("dsEmployeeRpt", dsempreimburstment.Tables[0]);
                ReportViewer2.ProcessingMode = ProcessingMode.Local;
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/OrgEmployee_Rpt.rdlc");
                ReportViewer2.LocalReport.DataSources.Add(rdsFuel);
            }

            //ReportViewer2.LocalReport.SetParameters(param);

        }
        catch (Exception ex)
        {
        }
    }

     public void Compare()
    {
        
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        getemployee_ReimbursmentDetails();

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
                           "   and emp_location in (select Distinct location from tbl_htravel_leave_extra_approver where approver_emp_code like '%'+ @empcode  +'%' and approver_type in ('ACC','RACC')) " +


              " ) t " +
                           "   where t.empname like '%' + @SearchText + '%'   Order by t.empname ";

                cmd.CommandText = strsql;
                cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));

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
}