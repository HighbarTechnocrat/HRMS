using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls; 
using Microsoft.Reporting.WebForms;
 public partial class Appreciation_Letter_Report : System.Web.UI.Page
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
            if (check_admin_report() == false)
            {
                Response.Redirect("Appreciation_Letter_index.aspx");
            }
             
            hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();
            hflEmpCode.Value = Convert.ToString(Session["Empcode"]).Trim();
            txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            txtFromdate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
            txtToDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            txtToDate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
             
            GetEmployeeDetails(); 
            var getLoginEmpDeg = hflEmpDesignation.Value;

            
            var getDepartment = hflEmpDepartment.Value;
            var thankyoucard = HiddenFieldid.Value;
            loadDropDownemployeename(getDepartment);
            loadDropDownthankyoucard(thankyoucard);
             

        }

    }

    public Boolean check_admin_report()
    {
        Boolean isvalid = false;

        DataSet dsLocations = new DataSet();

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "check_admin_report";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();

        dsLocations = spm.getDatasetList(spars, "Appreciation_Letter");

        if (dsLocations != null)
        {
            if (dsLocations.Tables[0].Rows.Count > 0)
            {
                isvalid = true;
            }
        }
        return isvalid;

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

    private void Reports()
    {
        DataSet dsApprovalStatusReport = new DataSet();

        SqlParameter[] spars = new SqlParameter[5];
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";
       
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetAppreciationLetter_report";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        if (Convert.ToString(ddlempname.SelectedValue).Trim() != "0")
            spars[1].Value = Convert.ToString(ddlempname.SelectedValue).Trim();
        else
            spars[1].Value = DBNull.Value;


        spars[2] = new SqlParameter("@Appreciation_id", SqlDbType.Int);
        if (Convert.ToString(ddlthankyoucard.SelectedValue).Trim() != "0" && Convert.ToString(ddlthankyoucard.SelectedValue).Trim() != "")
            spars[2].Value = Convert.ToString(ddlthankyoucard.SelectedValue);
        else
            spars[2].Value = null;


        spars[3] = new SqlParameter("@fromDate", SqlDbType.VarChar);
        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            spars[3].Value = strfromDate;
        }
        else
        {
            spars[3].Value = null;
        }

        spars[4] = new SqlParameter("@toDate", SqlDbType.VarChar);
        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            spars[4].Value = strToDate;
        }
        else
        {
            spars[4].Value = null;
        }

        dsApprovalStatusReport = spm.getDatasetList(spars, "Appreciation_Letter");

        ReportViewer1.LocalReport.Refresh(); 
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/Appreciation_Letter.rdlc");

        
            ReportDataSource rdsFuel = new ReportDataSource("AppreciationLetter_Count", dsApprovalStatusReport.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Add(rdsFuel);
         

        ReportDataSource rdsFuel_detail = new ReportDataSource("AppreciationLetter_Details", dsApprovalStatusReport.Tables[1]);
        ReportViewer1.LocalReport.DataSources.Add(rdsFuel_detail);
 
        ReportViewer1.LocalReport.Refresh();

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

     
    protected void btnSave_Click(object sender, EventArgs e)
    {

        Reports();

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

    public void loadDropDownthankyoucard(string selectVals)
    {
        DataTable dtleaveInboxs = new DataTable();
        //dtleaveInbox = spm.GetSerivesRequestDepartment();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "Appreciation_Letter_reportlist";

        dtleaveInboxs = spm.getDropdownList(spars, "Appreciation_Letter");
        if (dtleaveInboxs.Rows.Count > 0)
        {
            ddlthankyoucard.DataSource = dtleaveInboxs;
            //ddlDepartment.DataBind();
            ddlthankyoucard.DataTextField = "Appreciation_Letter";
            ddlthankyoucard.DataValueField = "Appreciation_id";
            ddlthankyoucard.DataBind();
            ddlthankyoucard.Items.Insert(0, new ListItem("Select Appreciation Letter", "0")); //updated code

            DataRow[] dr = dtleaveInboxs.Select("Appreciation_Letter = '" + selectVals.ToString() + "'");
            if (dr.Length > 0)
            {
                string avalue = dr[0]["id"].ToString();
                HiddenFieldid.Value = avalue;
                ddlthankyoucard.SelectedValue = avalue;

            }
            else
            {
                HiddenFieldid.Value = "0";
            }
        }
    }

    public void thankyoucard_access()
    {

        DataSet getdtDetails = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "thankyoucard_access";

            spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            if (Convert.ToString(Session["empcode"]).Trim() != "0" && Convert.ToString(Session["empcode"]).Trim() != "")
            {
                spars[1].Value = Convert.ToString(Session["empcode"]).Trim();
            }


            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "Thankyou_Card";

            getdtDetails = spm.getDatasetList(spars, "SP_thank_you_card");
            //ddlempname.Visible = false;
            //spn_dept.Visible = false;
            if (getdtDetails.Tables[0].Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Tables[0].Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    ddlempname.Visible = true;
                    spn_dept.Visible = true;
                }
                else
                {
                    // Make them invisible if not accessible


                }
            }
            // return false;
        }
        catch (Exception ex)
        {
            // return false;
        }
    }

    public void loadDropDownemployeename(string selectVal)
    {
        DataTable dtleaveInbox = new DataTable();
        //dtleaveInbox = spm.GetSerivesRequestDepartment();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "employee_name";

        dtleaveInbox = spm.getDropdownList(spars, "Appreciation_Letter");
        if (dtleaveInbox.Rows.Count > 0)
        {
            ddlempname.DataSource = dtleaveInbox;
            //ddlDepartment.DataBind();
            ddlempname.DataTextField = "Emp_Name";
            ddlempname.DataValueField = "Emp_Code";
            ddlempname.DataBind();
            ddlempname.Items.Insert(0, new ListItem("Select Employee Name", "0")); //updated code

            DataRow[] dr = dtleaveInbox.Select("Emp_Name = '" + selectVal.ToString() + "'");
            if (dr.Length > 0)
            {
                string avalue = dr[0]["Emp_Code"].ToString();
                hflEmpDepartmentID.Value = avalue;
                ddlempname.SelectedValue = avalue;

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


}