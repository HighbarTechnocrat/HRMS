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

public partial class AttendReport : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
        }
        if (!Page.IsPostBack)
        {
            hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();
            //hdnloginempcode.Value = "00630134";
            getLeaveTypes();
            getYear();    
            
        }

    }

    private void getemployee_leaveDetails()
    {
        try
        {
            #region get employee Leave details
            DataSet dsempLeaves = new DataSet();
           // dsempLeaves = null;
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "emp_AttendanceDtls";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);            
            spars[1].Value = Convert.ToString(hdnloginempcode.Value);

            spars[2] = new SqlParameter("@leavetypeid", SqlDbType.Int);
            if (Convert.ToString(DropDownList1.SelectedValue).Trim() != "0")
                spars[2].Value = Convert.ToInt32(DropDownList1.SelectedValue);
            else
                spars[2].Value = DBNull.Value;

            spars[3] = new SqlParameter("@year", SqlDbType.VarChar);
            if (Convert.ToString(ddlYear.SelectedValue).Trim() != "")
            {
                spars[3].Value = Convert.ToString(ddlYear.SelectedValue);
            }
            else
                spars[3].Value = Convert.ToString("0");

            dsempLeaves = spm.getDatasetList(spars, "rpt_dataProcedure");

            #endregion


            if (dsempLeaves.Tables[0].Rows.Count > 0)
            {
                ReportViewer1.Visible = true;
                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.LocalReport.DataSources.Clear();

                ReportParameter[] param = new ReportParameter[3];
                //param[0] = new ReportParameter("pyear", Convert.ToString(dsempLeaves.Tables[1].Rows[0]["pyear"]));
                 param[0] = new ReportParameter("pyear", "For the Year: "+ Convert.ToString(ddlYear.SelectedValue));
                 param[1] = new ReportParameter("EmpCode", "Employee Code: " + Convert.ToString(Session["Empcode"]));
                param[2] = new ReportParameter("EmpName", "Employee Name: " + Convert.ToString(Session["emp_loginName"]));

                // Create Report DataSource
            

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
                ReportViewer1.Visible = false;
               // ReportViewer1.LocalReport.DataSources.Clear();
               // ReportViewer1.LocalReport.Refresh();
            }
           

        }
        catch (Exception ex)
        {
        }
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
                DropDownList1.DataSource = dsLeaveType.Tables[0];
                DropDownList1.DataTextField = "LeaveTypeName";
                DropDownList1.DataValueField = "Leavetype_id";
                DropDownList1.DataBind();
                ListItem item = new ListItem("ALL", "0");
                DropDownList1.Items.Insert(0, item);
                
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "No Regularizations for the year";
            }
            #endregion
        }
        catch(Exception ex)
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
                lblmsg.Text = "No Regularizations for the year";
            }
            #endregion
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        getemployee_leaveDetails();

    }

  
}