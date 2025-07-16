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

public partial class CustEscalationReport_Audit_Summary : System.Web.UI.Page
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
            var isCEO = false;
            var isHOD = false;
            hflCEO.Value = "NO";
            hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();
            hflEmpCode.Value = Convert.ToString(Session["Empcode"]).Trim();
            GetEmployeeDetails();

            var getLoginEmpDeg = hflEmpDesignation.Value;
           
           //if (getLoginEmpDeg.Contains("Head"))
            //{
            //    isCEO = true;
            //    isHOD = true;
            //}

            if (IsShowHODReport())
            {
                isCEO = true;
                isHOD = true;
            }

            DataTable dtleaveInbox2 = new DataTable();
            dtleaveInbox2 = spm.GetCustEscalationCEOEmpCode();
            if (dtleaveInbox2.Rows.Count > 0)
            {
                var loginCode = Convert.ToString(hflEmpCode.Value);
                var CeoEmpCode = Convert.ToString(dtleaveInbox2.Rows[0]["Emp_Code"]);
                if (loginCode == CeoEmpCode || loginCode == "00630814" || loginCode == "00630967" || loginCode== "00630727" || loginCode== "00631294" || loginCode == "00631019")
                {
                    isCEO = true;
                    hflCEO.Value = "YES";
                }                  
            }


            if(isCEO==false)
                Response.Redirect("~/procs/CustEscalation.aspx");

            var getDepartment = Convert.ToString(hflDPTID.Value);

            loadDropDownDepartment(getDepartment);
            loadDropDownStatus();
            BindSERVICEREQUESTID(getDepartment);
            BindCREATEDEMP(getDepartment);
            //hdnloginempcode.Value = "00630134";     
            //if (isHOD==true)
            //{
            //    //ddlDepartment.Enabled = false;
            //    var getval = Convert.ToString(ddlDepartment.SelectedValue).Trim();
            //    foreach (ListItem item in ddlDepartment.Items)
            //    {
            //        if (getval == "8" || getval == "10")
            //        {
            //            if (item.Value == "8" || item.Value == "10")
            //            {
            //                ddlDepartment.Items.FindByValue(item.Value).Enabled = true;
            //            }
            //            else
            //            {
            //                ddlDepartment.Items.FindByValue(item.Value).Enabled = false;
            //            }
            //        }
            //        else
            //        {
            //            if(getval == item.Value)
            //            {
            //                ddlDepartment.Items.FindByValue(item.Value).Enabled = true;
            //            }
            //            else
            //            {
            //                ddlDepartment.Items.FindByValue(item.Value).Enabled = false;
            //            }
            //        }                      
                      
            //    }
            //    //if (getval == "8" || getval == "10")
            //    //{
            //    //    ddlDepartment.Items.FindByValue("8").Enabled = true;
            //    //    ddlDepartment.Items.FindByValue("10").Enabled = true;
            //    //    ddlDepartment.Items.FindByValue("7").Enabled = false;
            //    //    ddlDepartment.Items.FindByValue("0").Enabled = false;
            //    //    ddlDepartment.Items.FindByValue("2").Enabled = false;
            //    //}
            //    //else if(getval == "7")
            //    //{
            //    //    ddlDepartment.Items.FindByValue("8").Enabled = false;
            //    //    ddlDepartment.Items.FindByValue("10").Enabled = false;
            //    //    ddlDepartment.Items.FindByValue("7").Enabled = true;
            //    //    ddlDepartment.Items.FindByValue("0").Enabled = false;
            //    //    ddlDepartment.Items.FindByValue("2").Enabled = false;
            //    //}
            //    //else 
            //    //{
            //    //    ddlDepartment.Items.FindByValue("8").Enabled = false;
            //    //    ddlDepartment.Items.FindByValue("10").Enabled = false;
            //    //    ddlDepartment.Items.FindByValue("7").Enabled = false;
            //    //    ddlDepartment.Items.FindByValue("0").Enabled = false;
            //    //    ddlDepartment.Items.FindByValue("2").Enabled = true;
            //    //}
            //}

        }

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
    private void getemployee_ReimbursmentDetails()
    {
        try
        {
            #region get employee Claim details
            DataSet dsempreimburstment = new DataSet();
            SqlParameter[] spars = new SqlParameter[9];
            string[] strdate;
            string strfromDate = "";
            string strToDate = "";
            string assgineTo = "";
            //@fromDate
            spars[0] = new SqlParameter("@fromDate", SqlDbType.VarChar);
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                spars[0].Value = strfromDate;
            }
            else
            {
                spars[0].Value = null;
            }
            //@toDate
            spars[1] = new SqlParameter("@toDate", SqlDbType.VarChar);
            if (Convert.ToString(txtToDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                spars[1].Value = strToDate;
            }
            else
            {
                strToDate = DateTime.Today.ToString("yyyy") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("dd");
                spars[1].Value = strToDate;
            }
            //@CategoryId
            spars[2] = new SqlParameter("@CategoryId", SqlDbType.Int);
            if (Convert.ToString(ddlCategory.SelectedValue).Trim() != "0" && Convert.ToString(ddlCategory.SelectedValue).Trim() != "")
            {
                spars[2].Value = Convert.ToString(ddlCategory.SelectedValue);
            }
            else
            {
                spars[2].Value = null;
            }
            //@ServiceDepartment
            spars[3] = new SqlParameter("@ServiceDepartment", SqlDbType.Int);
            if (Convert.ToString(ddlDepartment.SelectedValue).Trim() != "0" && Convert.ToString(ddlDepartment.SelectedValue).Trim() != "00")
            {
                spars[3].Value = Convert.ToString(ddlDepartment.SelectedValue);
            }
            else
            {
                spars[3].Value = null;
            }
            //@status
            spars[4] = new SqlParameter("@status", SqlDbType.Int);
            if (Convert.ToString(ddlStatus.SelectedValue).Trim() != "0")
            {
                spars[4].Value = Convert.ToString(ddlStatus.SelectedValue);
            }
            else
            {
                spars[4].Value = null;
            }
            //@AssignedTo
            spars[5] = new SqlParameter("@AssignedTo", SqlDbType.VarChar);
            if (Convert.ToString(ddlAssignedto.SelectedValue).Trim() != "0" && Convert.ToString(ddlAssignedto.SelectedValue).Trim() != "")
            {
                spars[5].Value = Convert.ToString(ddlAssignedto.SelectedValue);
            }
            else
            {
                spars[5].Value = null;
            }
            //@Escalated
            spars[6] = new SqlParameter("@Escalated", SqlDbType.VarChar);
            if (Convert.ToString(ddlEscalated.SelectedValue).Trim() != "")
            {
                spars[6].Value = Convert.ToString(ddlEscalated.SelectedValue);
            }
            else
            {
                spars[6].Value = null;
            }
            //@emp_name
            spars[7] = new SqlParameter("@emp_name", SqlDbType.VarChar);
            if (Convert.ToString(ddl_CreatedEmp.SelectedValue).Trim() != "0")
            {
               // String[] stremp;
                string selected_Employee_code = "";
               // stremp = Convert.ToString(txtemp.Text).Split('-');
               // if (Convert.ToString(txtemp.Text).Trim() != "")
                //{
                    selected_Employee_code = Convert.ToString(ddl_CreatedEmp.SelectedValue).Trim();
                // }
                assgineTo = selected_Employee_code;
                 spars[7].Value = Convert.ToString(selected_Employee_code.ToString());
            }
            else
            {
                spars[7].Value = null;
            }

            //@serviceRequestId
            spars[8] = new SqlParameter("@serviceRequestId", SqlDbType.VarChar);
            if (Convert.ToString(ddl_ServiceRequestID.SelectedValue).Trim() != "0")
            {
                spars[8].Value = Convert.ToString(ddl_ServiceRequestID.SelectedValue).Trim();
            }
            else
            {
                spars[8].Value = null;
            }
            //SqlParameter[] spars1 = new SqlParameter[9];
            //spars1 = spars;
          //  dsempreimburstment = spm.getServiceRequestReport(spars, "SP_SERVICE_REQUEST_REPORT");
            var dsempreimburstment1 = spm.getServiceRequestReportCount(spars, "SP_CUST_ESCALATION_REPORT_Count");

            #endregion

            ReportParameter[] param = new ReportParameter[6];
            if (dsempreimburstment1.Tables[0].Rows.Count > 0)
                param[0] = new ReportParameter("pyear", Convert.ToString(DateTime.Now.Year));
            else
                param[0] = new ReportParameter("pyear","2018");

            if (dsempreimburstment1.Tables[0].Rows.Count > 0)
            {
                param[1] = new ReportParameter("pempname", Convert.ToString(assgineTo));
                param[2] = new ReportParameter("pempcode", Convert.ToString(""));
            }

            if (dsempreimburstment1.Tables[0].Rows.Count > 0)
                param[3] = new ReportParameter("pPaymentClaimhead", Convert.ToString("Service Request details"));
            else
                param[3] = new ReportParameter("pPaymentClaimhead", Convert.ToString("No Service Request during the year"));

            

            ReportViewer2.LocalReport.Refresh();
            ReportViewer2.LocalReport.DataSources.Clear();

            if (dsempreimburstment1.Tables[0].Rows.Count > 0)
            {
                // Create Report DataSource :- for payment Voucher
               // ReportDataSource serviceRequestDS = new ReportDataSource("ServiceRequestDataSet", dsempreimburstment.Tables[0]);
                ReportDataSource serviceRequestDS1 = new ReportDataSource("ServiceRequestSummery", dsempreimburstment1.Tables[0]);
                ReportDataSource serviceRequestDS2 = new ReportDataSource("ServiceRequestDepartmentSummary", dsempreimburstment1.Tables[1]);
                ReportViewer2.ProcessingMode = ProcessingMode.Local;
                //ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/PaymentVoucher_reimbursment.rdlc");
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/CustEscalation_Audit_Summary.rdlc");
               // ReportViewer2.LocalReport.DataSources.Add(serviceRequestDS);
                ReportViewer2.LocalReport.DataSources.Add(serviceRequestDS1);
                ReportViewer2.LocalReport.DataSources.Add(serviceRequestDS2);


            }
            else
            {
                // Create Report DataSource :- for payment Voucher
               // ReportDataSource serviceRequestDS = new ReportDataSource("ServiceRequestDataSet", dsempreimburstment.Tables[0]);
                ReportDataSource serviceRequestDS1 = new ReportDataSource("ServiceRequestSummery", dsempreimburstment1.Tables[0]);
                ReportDataSource serviceRequestDS2 = new ReportDataSource("ServiceRequestDepartmentSummary", dsempreimburstment1.Tables[1]);
                ReportViewer2.ProcessingMode = ProcessingMode.Local;
                //ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/PaymentVoucher_reimbursment.rdlc");
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/CustEscalation_Audit_Summary.rdlc");
               // ReportViewer2.LocalReport.DataSources.Add(serviceRequestDS);
                ReportViewer2.LocalReport.DataSources.Add(serviceRequestDS1);
                ReportViewer2.LocalReport.DataSources.Add(serviceRequestDS2);
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
        try
        {
            if(Convert.ToString(ddlDepartment.SelectedValue)=="0")
            {
                lblmessage.Text = "Please select department";
                return;
            }
            getemployee_ReimbursmentDetails();
        }
        catch (Exception)
        {

        }

       

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

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList list = (DropDownList)sender;
            string value = (string)list.SelectedValue;
            if (value == "0")
            {
                ddlCategory.Items.Clear();
                ddlCategory.DataSource = null;
                ddlCategory.DataBind();
                ddlAssignedto.Items.Clear();
                ddlAssignedto.DataSource = null;
                ddlAssignedto.DataBind();

                ddl_CreatedEmp.Items.Clear();
                ddl_CreatedEmp.DataSource = null;
                ddl_CreatedEmp.DataBind();
                ddl_ServiceRequestID.Items.Clear();
                ddl_ServiceRequestID.DataSource = null;
                ddl_ServiceRequestID.DataBind();
            }
            else
            {
                BindCategoryDDL(value);
                BindSERVICEREQUESTID(value);
                BindCREATEDEMP(value);
                //BindEmpDDL(value);

            }
        }
        catch (Exception)
        {

        }
    }
    public void loadDropDownDepartment(string selectVal)
    {
        DataTable dtleaveInbox = new DataTable();
        //dtleaveInbox = spm.GetSerivesRequestDepartment();
        dtleaveInbox = GETDEPARTMENTDDLREPORT();
        ddlDepartment.DataSource = dtleaveInbox;
        //ddlDepartment.DataBind();
        ddlDepartment.DataTextField = "DepartmentName";
        ddlDepartment.DataValueField = "DepartmentId";        
        ddlDepartment.DataBind();
        var isCEO = Convert.ToString(hflCEO.Value);
        if(isCEO== "NO")
        {
            ddlDepartment.Items.Insert(0, new ListItem("Select Department", "0")); //updated code
        }
        else
        {
            ddlDepartment.Items.Insert(0, new ListItem("Select Department", "0")); //updated code
            ddlDepartment.Items.Insert(1, new ListItem("ALL", "00")); //updated code
        }
        

        hflEmpDepartmentID.Value = selectVal;
        ddlDepartment.SelectedValue = selectVal;

        BindCategoryDDL(ddlDepartment.SelectedValue);
        //DataRow[] dr = dtleaveInbox.Select("DepartmentName = '" + selectVal.ToString()+"'");
        //if (dr.Length > 0)
        //{
        //    string avalue = dr[0]["DepartmentId"].ToString();
        //    hflEmpDepartmentID.Value = avalue;
        //    ddlDepartment.SelectedValue = avalue;
        //    //BindEmpDDL(avalue);
        //}
        //else
        //{
        //    hflEmpDepartmentID.Value = "0";
        //}
        //BindCategoryDDL(ddlDepartment.SelectedValue);
    }
    public void BindCategoryDDL(string departmentId)
    {
        try
        {

            var dptId = Convert.ToInt32(departmentId);
            var getDepartment = spm.GetCustEscalationCategoryDepartment(dptId);
            if (getDepartment.Rows.Count > 0)
            {
                //DataTable dtleaveInbox = new DataTable();
                //dtleaveInbox = spm.GetSerivesRequestDepartment();
                ddlCategory.DataSource = getDepartment;
                //ddlDepartment.DataBind();
                ddlCategory.DataTextField = "CategoryTitle";
                ddlCategory.DataValueField = "Id";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("Select Category", "0")); //updated code
            }
            else
            {
                ddlCategory.DataSource = null;
                ddlCategory.DataBind();
            }


        }
        catch (Exception)
        {

            throw;
        }
    }
    public void BindEmpDDL(string departmentId)
    {
        try
        {
            var isCEOVAl = Convert.ToString(hflCEO.Value);
            var dptId = Convert.ToInt32(departmentId);
            var getDepartment = new DataTable();
           //s getDepartment = spm.GetEmpDepartment(dptId);
            getDepartment = GETAssginedEMP("GETDDLFORSERVICECREATEReport", dptId);
           
            if (getDepartment.Rows.Count > 0)
            {
                //DataTable dtleaveInbox = new DataTable();
                //dtleaveInbox = spm.GetSerivesRequestDepartment();
                ddlAssignedto.DataSource = getDepartment;
                //ddlDepartment.DataBind();
                ddlAssignedto.DataTextField = "EmployeeName";
                ddlAssignedto.DataValueField = "Emp_Code";
                ddlAssignedto.DataBind();
                ddlAssignedto.Items.Insert(0, new ListItem("Select Assigned To", "0")); //updated code
            }
            else
            {
                ddlAssignedto.Items.Clear();
                ddlAssignedto.DataSource = null;
                ddlAssignedto.DataBind();
            }


        }
        catch (Exception)
        {

            throw;
        }
    }

    public void loadDropDownStatus()
    {
        DataTable dtleaveInbox = new DataTable();
        dtleaveInbox = GETSTATUSDDLREPORT();
        ddlStatus.DataSource = dtleaveInbox;
        //ddlDepartment.DataBind();
        ddlStatus.DataTextField = "StatusTitle";
        ddlStatus.DataValueField = "Id";
        ddlStatus.DataBind();
        ddlStatus.Items.Insert(0, new ListItem("Select Status", "0")); //updated code
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


                strsql = "  select ServicesRequestID from tbl_CustEscalationDetails WHERE ServicesRequestID LIKE '%' + @SearchText + '%'   Order by Id ";

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
    
   public bool IsShowHODReport()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "IsDepartmentHOD";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);
            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_INSERTCUST_ESCALATION");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                var DepartmentId = Convert.ToString(getdtDetails.Rows[0]["DepartmentId"]);
                hflDPTID.Value = DepartmentId;
                if (getStatus == "SHOW")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }

    public DataTable GETDEPARTMENTDDLREPORT()
    {
        var getdtDetails = new DataTable();
        try
        {            
            var isCEO = Convert.ToString(hflCEO.Value);

            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "DDLDEPARTMENTReport";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            spars[2] = new SqlParameter("@IsCEO", SqlDbType.VarChar);
            spars[2].Value = isCEO;

            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_INSERTCUST_ESCALATION");
            return getdtDetails;
        }
        catch (Exception)
        {
            return getdtDetails;
        }
    }

    public DataTable GETSTATUSDDLREPORT()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[1];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "DDLSTATUSLISTReport";           
            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_INSERTCUST_ESCALATION");
            return getdtDetails;
        }
        catch (Exception)
        {
            return getdtDetails;
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList list = (DropDownList)sender;
            string value = (string)list.SelectedValue;
            if (value == "0")
            {
                ddlCategory.Items.Clear();
                ddlCategory.DataSource = null;
                ddlCategory.DataBind();
                ddlAssignedto.Items.Clear();
                ddlAssignedto.DataSource = null;
                ddlAssignedto.DataBind();
            }
            else
            {
                //BindCategoryDDL(value);
                BindEmpDDL(value);

            }
        }
        catch (Exception)
        {

        }
    }
    public void BindCREATEDEMP(string departmentId)
    {
        try
        {
            ddl_CreatedEmp.Items.Clear();
            ddl_CreatedEmp.DataSource = null;
            ddl_CreatedEmp.DataBind();
            var isCEO = Convert.ToString(hflCEO.Value);
            var deptId = Convert.ToInt32(departmentId);
            DataTable dtleaveInbox = new DataTable();
            dtleaveInbox = GETCREATEDSRDDLREPORT("DDLEMPLISTFORREPORT", isCEO, deptId);
            ddl_CreatedEmp.DataSource = dtleaveInbox;
            //ddlDepartment.DataBind();
            ddl_CreatedEmp.DataTextField = "Emp_Name";
            ddl_CreatedEmp.DataValueField = "Emp_Code";
            ddl_CreatedEmp.DataBind();
            ddl_CreatedEmp.Items.Insert(0, new ListItem("Select Created Name", "0")); //updated code
        }
        catch (Exception)
        {

        }
        
    }
    public void BindSERVICEREQUESTID(string departmentId)
    {
        try
        {
            
            ddl_ServiceRequestID.Items.Clear();
            ddl_ServiceRequestID.DataSource = null;
            ddl_ServiceRequestID.DataBind();
            var isCEO = Convert.ToString(hflCEO.Value);
            var deptId = Convert.ToInt32(departmentId);
            DataTable dtleaveInbox = new DataTable();
            dtleaveInbox = GETCREATEDSRDDLREPORT("DDLServiceRequestIds",isCEO,deptId);
            ddl_ServiceRequestID.DataSource = dtleaveInbox;
            //ddlDepartment.DataBind();
            ddl_ServiceRequestID.DataTextField = "ServicesRequestID";
            ddl_ServiceRequestID.DataValueField = "ServicesRequestID";
            ddl_ServiceRequestID.DataBind();
            ddl_ServiceRequestID.Items.Insert(0, new ListItem("Select Services Request ID", "0")); //updated code
        }
        catch (Exception)
        {

        }

    }
    public DataTable GETCREATEDSRDDLREPORT(string qtype,string isCEO,int departmentId)
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = qtype;
            spars[1] = new SqlParameter("@IsCEO", SqlDbType.VarChar);
            spars[1].Value = isCEO;
            spars[2] = new SqlParameter("@ServiceDepartment", SqlDbType.Int);
            spars[2].Value = departmentId;

            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_INSERTCUST_ESCALATION");
            return getdtDetails;
        }
        catch (Exception)
        {
            return getdtDetails;
        }
    }

    public DataTable GETAssginedEMP(string qtype,int categoryId)
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = qtype;
            spars[1] = new SqlParameter("@CategoryId", SqlDbType.Int);
            spars[1].Value = categoryId;

            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_INSERTCUST_ESCALATION");
            return getdtDetails;
        }
        catch (Exception)
        {
            return getdtDetails;
        }
    }
}