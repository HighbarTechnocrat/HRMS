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

public partial class LOP_EmployeeReportSeparation : System.Web.UI.Page
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
            txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            txtFromdate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
            txtToDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            txtToDate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");

           
            GetEmployeeDetails();
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
    private void getemployee_ReimbursmentDetails()
    {
        try
        {
            #region get employee Claim details
            DataSet dsempreimburstment = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];
            string[] strdate;
            string strfromDate = "";
            string strToDate = "";
            string strfromDate_RPt = "";
            string strToDate_RPt = "",ToDate="";
            string empName = "";
            string IsShow = "";
            //@fromDate
            spars[0] = new SqlParameter("@FromDate", SqlDbType.VarChar);
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                strfromDate_RPt = Convert.ToString(strdate[0]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[2]);
                spars[0].Value = strfromDate;
            }
            else
            {
                spars[0].Value = null;
            }
            //@toDate
            spars[1] = new SqlParameter("@ToDate", SqlDbType.VarChar);
            if (Convert.ToString(txtToDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                strToDate_RPt = Convert.ToString(strdate[0]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[2]);
                spars[1].Value = strToDate;
				ToDate = Convert.ToString(txtToDate.Text).Trim();

			}
            else
            {
                strToDate = DateTime.Today.ToString("yyyy") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("dd");
                strToDate_RPt = DateTime.Today.ToString("dd") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("yyyy");
                spars[1].Value = strToDate;
				ToDate = DateTime.Today.ToString("dd-MM-yyyy");

			}
            //@ServiceDepartment
            spars[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            if (Convert.ToString(ddlEmployee.SelectedValue).Trim() != "0" && Convert.ToString(ddlEmployee.SelectedValue).Trim() != "")
            {
                spars[2].Value = Convert.ToString(ddlEmployee.SelectedValue);
                empName= Convert.ToString(ddlEmployee.SelectedItem.Text);
                IsShow = "Y";
            }
            else
            {
                spars[2].Value = null;
            }
            //@status
            spars[3] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[3].Value = "GetLOPReportsDetails";

            dsempreimburstment = spm.getDatasetList(spars, "SP_LOP_Report_Detail");

            #endregion

            ReportViewer2.LocalReport.Refresh();
            ReportViewer2.LocalReport.DataSources.Clear();
            ReportParameter[] param = new ReportParameter[4];

            param[0] = new ReportParameter("StartDate", Convert.ToString(txtFromdate.Text));
            param[1] = new ReportParameter("EndDate", Convert.ToString(ToDate));
            param[2] = new ReportParameter("secoundReportHide", Convert.ToString(IsShow));
            param[3] = new ReportParameter("EmpName", Convert.ToString(empName));

            
            if (dsempreimburstment.Tables[0].Rows.Count > 0)
            {
                // Create Report DataSource :- for payment Voucher
                ReportDataSource dtLOCReport = new ReportDataSource("dsLOPDetails", dsempreimburstment.Tables[0]);
                //ReportDataSource dtLOCReport1 = new ReportDataSource("dsLOPEmployeeDetails", dsempreimburstment.Tables[1]);
                ReportViewer2.ProcessingMode = ProcessingMode.Local;
                //ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/PaymentVoucher_reimbursment.rdlc");
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/LOPEmployeeDetails.rdlc");
                ReportViewer2.LocalReport.DataSources.Add(dtLOCReport);
                //ReportViewer2.LocalReport.DataSources.Add(dtLOCReport1);
                //ReportViewer2.ShowToolBar = true;
            }
            else
            {
                // Create Report DataSource :- for payment Voucher
                ReportDataSource dtLOCReport = new ReportDataSource("dsLOPDetails", dsempreimburstment.Tables[0]);
              //  ReportDataSource dtLOCReport1 = new ReportDataSource("dsLOPEmployeeDetails", dsempreimburstment.Tables[1]);
                ReportViewer2.ProcessingMode = ProcessingMode.Local;
                //ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/PaymentVoucher_reimbursment.rdlc");
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/LOPEmployeeDetails.rdlc");
                ReportViewer2.LocalReport.DataSources.Add(dtLOCReport);
               // ReportViewer2.LocalReport.DataSources.Add(dtLOCReport1);
                //ReportViewer2.ShowToolBar = true;
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
                ddlEmployee.Enabled = true;
                getAllEmpDLLData();
                // ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Start date should be greate than End date')", true);
            }
        }
        else
        {
            lblmessage.Text = "Please select first From Date ";
            txtToDate.Text = "";
            return;
        }
    }

    //protected void lstFromfor_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtClaimStatus.Text = lstFromfor.SelectedValue;        
    //    PopupControlExtender1.Commit(lstFromfor.SelectedValue);
       

    
    
    //}
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "Please Enter From Date";
            return;
        }


        getemployee_ReimbursmentDetails();

    }


    public void getAllEmpDLLData()
    {
        var getdtDetails = new DataTable();
        try
        {
            ddlEmployee.Items.Clear();
            SqlParameter[] spars = new SqlParameter[1];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetAllEmployee";

            
            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
            if(getdtDetails.Rows.Count>0)
            {
                //ddlEmployee.Enabled = true;
                
                ddlEmployee.DataSource = getdtDetails;
                //ddlDepartment.DataBind();
                ddlEmployee.DataTextField = "Emp_Name";
                ddlEmployee.DataValueField = "Emp_Code";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("Select Employee Name", "0")); //updated code
            }
        }
        catch (Exception)
        {
            
        }
    }

}