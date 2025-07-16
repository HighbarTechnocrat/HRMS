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

public partial class procs_ABAP_ObjectCompletionReport : System.Web.UI.Page
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
            hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();
            hflEmpCode.Value = Convert.ToString(Session["Empcode"]).Trim();
            txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            txtFromdate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
            txtToDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            txtToDate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
           
            ObjectTypeBindANDEmployee();


        }

    }

   

    

   
    
    private void getABAP_EmployeeObjectReport()
    {
        try
        {
            #region get ABAP Employee Object details
            DataSet dsABAPObject = new DataSet();
            SqlParameter[] spars = new SqlParameter[6];
            string[] strdate;
            string strfromDate = "";
            string strToDate = "";
            string strfromDate_RPt = "";
            string strToDate_RPt = "";



            spars[0] = new SqlParameter("@from_date", SqlDbType.VarChar);
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
            spars[1] = new SqlParameter("@to_date", SqlDbType.VarChar);
            if (Convert.ToString(txtToDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                strToDate_RPt = Convert.ToString(strdate[0]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[2]);
                spars[1].Value = strToDate;
            }
            else
            {
                strToDate = DateTime.Today.ToString("yyyy") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("dd");
                strToDate_RPt = DateTime.Today.ToString("dd") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("yyyy");
                spars[1].Value = strToDate;
            }
            
                spars[2] = new SqlParameter("@ObjectID", SqlDbType.Int);
                if (Convert.ToString(ddlObjectType.SelectedValue).Trim() != "0" && Convert.ToString(ddlObjectType.SelectedValue).Trim() != "")
                {
                    spars[2].Value = Convert.ToString(ddlObjectType.SelectedValue);
                }
                else
                {
                    spars[2].Value = null;
                }

            spars[3] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            if (Convert.ToString(DDLEmployee.SelectedValue).Trim() != "0" && Convert.ToString(DDLEmployee.SelectedValue).Trim() != "")
            {
                spars[3].Value = Convert.ToString(DDLEmployee.SelectedValue);
            }
            else
            {
                spars[3].Value = null;
            }
            //@status
            spars[4] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[4].Value = "GetABAP_ObjectTypeEmployeeReport";

            spars[5] = new SqlParameter("@Comp_Code", SqlDbType.VarChar);
            if (Convert.ToString(DDLprojectName.SelectedValue).Trim() != "0" && Convert.ToString(DDLprojectName.SelectedValue).Trim() != "")
            {
                spars[5].Value = Convert.ToString(DDLprojectName.SelectedValue); 
            }
            else
            {
                spars[5].Value = null;
            }

            dsABAPObject = spm.getDatasetList(spars, "SP_ABAP_ObjectCompletionReport");

            #endregion

            ReportViewer2.LocalReport.Refresh();
            ReportViewer2.LocalReport.DataSources.Clear();
            ReportParameter[] param = new ReportParameter[2];
            param[0] = new ReportParameter("FromDate", Convert.ToString(strfromDate_RPt));
            param[1] = new ReportParameter("ToDate", Convert.ToString(strToDate_RPt));

            if (dsABAPObject.Tables[0].Rows.Count > 0)
            {
                ReportDataSource rdsFuel = new ReportDataSource("DS_Summary", dsABAPObject.Tables[0]);
                ReportDataSource rdsFuel_detail = new ReportDataSource("DS_ObjectCompleted", dsABAPObject.Tables[1]);
                ReportDataSource rdsFuel_detail_Sep = new ReportDataSource("DS_ObjectNotCompleted", dsABAPObject.Tables[2]);
                ReportDataSource rdsFuel_detail_onboard = new ReportDataSource("DS_ObjectAllDetail", dsABAPObject.Tables[3]);

                ReportViewer2.ProcessingMode = ProcessingMode.Local;
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/ABAP_ObjectCompletionReport.rdlc");
                ReportViewer2.LocalReport.DataSources.Add(rdsFuel);
                ReportViewer2.LocalReport.DataSources.Add(rdsFuel_detail);
                ReportViewer2.LocalReport.DataSources.Add(rdsFuel_detail_Sep);
                ReportViewer2.LocalReport.DataSources.Add(rdsFuel_detail_onboard);
                ReportViewer2.LocalReport.SetParameters(param);
            }

        }
        catch (Exception ex)
        {
        }
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

        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "From date  cannot be blank";
            return;
        }
        getABAP_EmployeeObjectReport();

    }



    

    public void ObjectTypeBindANDEmployee()
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "GetObjectTypeAndEmployee";

            DataSet DS = spm.getDatasetList(spars, "SP_ABAP_ObjectCompletionReport");
            if (DS.Tables[0].Rows.Count > 0)
            {
                ddlObjectType.DataSource = DS.Tables[0];
                ddlObjectType.DataTextField = "ObjectType";
                ddlObjectType.DataValueField = "ObjectID";
                ddlObjectType.DataBind();
                ddlObjectType.Items.Insert(0, new ListItem("Select Object Type", "0"));
            }
            if (DS.Tables[1].Rows.Count > 0)
            {
                DDLEmployee.DataSource = DS.Tables[1];
                DDLEmployee.DataTextField = "EmpName";
                DDLEmployee.DataValueField = "Emp_Code";
                DDLEmployee.DataBind();
                DDLEmployee.Items.Insert(0, new ListItem("Select Employee", "0"));
            }

            if (DS.Tables[2].Rows.Count > 0)
            {
                DDLprojectName.DataSource = DS.Tables[2];
                DDLprojectName.DataTextField = "LocationName";
                DDLprojectName.DataValueField = "Comp_Code";
                DDLprojectName.DataBind();
                DDLprojectName.Items.Insert(0, new ListItem("Select Project", "0"));
            }
        }
        catch (Exception ex)
        {

        }
    }

  


    

    
}