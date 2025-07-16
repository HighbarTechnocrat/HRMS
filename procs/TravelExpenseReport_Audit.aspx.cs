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

public partial class TravelExpenseReport_Audit : System.Web.UI.Page
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
            GetgetTravelVoucherEmployee();
            hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();
            //hdnloginempcode.Value = "00630134";        
        }

    }


    public void GetgetTravelVoucherEmployee()
    {
        DataTable lstPosition = new DataTable();
        lstPosition = spm.getTravelVoucherEmployee_List();
        if (lstPosition.Rows.Count > 0)
        {
            DDL_txtemp.DataSource = lstPosition;
            DDL_txtemp.DataTextField = "empname";
            DDL_txtemp.DataValueField = "Emp_Code";
            DDL_txtemp.DataBind();
            DDL_txtemp.Items.Insert(0, new ListItem("Select  Employee", "0"));
        }
    }

    private void getemployee_ReimbursmentDetails()
    {
        try
        {
            #region get employee Claim details
            DataSet dsempreimburstment = new DataSet();
            SqlParameter[] spars = new SqlParameter[6];
            string[] strdate;
            string strfromDate = "";
            string strToDate = "";

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            //spars[0].Value = "Allemp_paymntmbilefuel_reimbursmnt";
            spars[0].Value = "Allemp_Travel_Expenses_Audit";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hdnloginempcode.Value);

            spars[2] = new SqlParameter("@emp_name", SqlDbType.VarChar);
            if (Convert.ToString(DDL_txtemp.SelectedItem.Text).Trim() != "")
            {
                String[] stremp;
                string selected_Employee_code = "";
                stremp = Convert.ToString(DDL_txtemp.SelectedItem.Text).Split('-');
                if (Convert.ToString(DDL_txtemp.SelectedItem.Text).Trim() != "")
                {
                    selected_Employee_code = Convert.ToString(stremp[0]).Trim();
                }
                if (selected_Employee_code == "Select  Employee")
                {
                    spars[2].Value = null;
                }
                else
                {
                    spars[2].Value = Convert.ToString(selected_Employee_code.ToString());
                }
            }
            else
                spars[2].Value = null;

            spars[3] = new SqlParameter("@Request_status", SqlDbType.VarChar);
            if (Convert.ToString(ddlClaimStatus.SelectedValue).Trim() != "")
            {
                spars[3].Value = Convert.ToString(ddlClaimStatus.SelectedValue);
            }
            else
                spars[3].Value = null;
            spars[4] = new SqlParameter("@from_date", SqlDbType.VarChar);
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                spars[4].Value = strfromDate;
            }
            else
                 spars[4].Value = null;

            spars[5] = new SqlParameter("@to_date", SqlDbType.VarChar);
            if (Convert.ToString(txtToDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

                spars[5].Value = strToDate;
            }
            else
            {
                //strToDate = Convert.ToString("1900") + "-" + Convert.ToString("01") + "-" + Convert.ToString("01");
                strToDate = DateTime.Today.ToString("yyyy") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("dd");
                spars[5].Value = strToDate;
                //spars[5].Value = null;
            }
            dsempreimburstment = spm.getDatasetList(spars, "rpt_dataProcedure");

            #endregion

            ReportParameter[] param = new ReportParameter[4];
            if (dsempreimburstment.Tables[1].Rows.Count > 0)
                param[0] = new ReportParameter("pyear", Convert.ToString(dsempreimburstment.Tables[1].Rows[0]["pyear"]));
            else
                param[0] = new ReportParameter("pyear","2020");

            if (dsempreimburstment.Tables[2].Rows.Count > 0)
            {
                param[1] = new ReportParameter("pempname", Convert.ToString(dsempreimburstment.Tables[2].Rows[0]["Emp_Name"]));
                param[2] = new ReportParameter("pempcode", Convert.ToString(dsempreimburstment.Tables[2].Rows[0]["Emp_Code"]));
            }
            if (dsempreimburstment.Tables[0].Rows.Count > 0)
                param[3] = new ReportParameter("pPaymentClaimhead", Convert.ToString("Travel Expense Details"));
            else
                param[3] = new ReportParameter("pPaymentClaimhead", Convert.ToString("No Travel Expense Vouchers during the year"));

            ReportViewer2.LocalReport.Refresh();
            ReportViewer2.LocalReport.DataSources.Clear();
            if (dsempreimburstment.Tables[0].Rows.Count > 0)
            {
                // Create Report DataSource :- for payment Voucher
                ReportDataSource rdspayment = new ReportDataSource("dspaymnt_reimbursmnt", dsempreimburstment.Tables[0]);
                ReportViewer2.ProcessingMode = ProcessingMode.Local;
                //ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/PaymentVoucher_reimbursment.rdlc");
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/TravelExpense_Audit.rdlc");
                ReportViewer2.LocalReport.DataSources.Add(rdspayment);
            }
            ReportViewer2.LocalReport.SetParameters(param);

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