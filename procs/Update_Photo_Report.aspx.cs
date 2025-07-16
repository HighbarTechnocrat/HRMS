
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.Reporting.WebForms;
  
public partial class Update_Photo_Report : System.Web.UI.Page
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
             
            hflCEO.Value = "NO";
            hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();
            hflEmpCode.Value = Convert.ToString(Session["Empcode"]).Trim();
            
 
           
 
                // Adding items to the DropDownList
                emp_status.Items.Insert(0, new ListItem("Select Employee Status", "0"));
                emp_status.Items.Add(new ListItem("Approved", "1"));
                emp_status.Items.Add(new ListItem("Pending", "2"));
                emp_status.Items.Add(new ListItem("Correction", "3"));
               // emp_status.Items.Add(new ListItem("No", ""));
            
            

            var getDepartment = hflEmpDepartment.Value;
            var thankyoucard = HiddenFieldid.Value;
            loadDropDownemployeename(getDepartment);
            
            
        }

    }

   

    private void Reports()
    {
        DataSet dsApprovalStatusReport = new DataSet();

        SqlParameter[] spars = new SqlParameter[3];
         
        spars[0] = new SqlParameter("@QUERYMODE", SqlDbType.VarChar);
        spars[0].Value = "get_photo_report";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        if (Convert.ToString(ddlempname.SelectedValue).Trim() != "0")
            spars[1].Value = Convert.ToString(ddlempname.SelectedValue).Trim();
        else
            spars[1].Value = DBNull.Value;


        spars[2] = new SqlParameter("@Pending_approved", SqlDbType.Int);
        if (Convert.ToString(emp_status.SelectedValue).Trim() != "0" && Convert.ToString(emp_status.SelectedValue).Trim() != "")
            spars[2].Value = Convert.ToString(emp_status.SelectedValue);
        else
            spars[2].Value = null;



        dsApprovalStatusReport = spm.getDatasetList(spars, "SP_Update_Photo");

        ReportViewer1.LocalReport.Refresh();
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportParameter[] param = new ReportParameter[2];
        

        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/update_photo_report.rdlc");

        ReportDataSource rdsFuel = new ReportDataSource("updatephoto", dsApprovalStatusReport.Tables[0]);
        ReportViewer1.LocalReport.DataSources.Add(rdsFuel); 
        ReportViewer1.LocalReport.Refresh();

    }
     
    protected void btnSave_Click(object sender, EventArgs e)
    { 
        Reports(); 
    }
     
    public void loadDropDownemployeename(string selectVal)
    {
        DataTable dtleaveInbox = new DataTable();
        //dtleaveInbox = spm.GetSerivesRequestDepartment();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@QUERYMODE", SqlDbType.VarChar);
        spars[0].Value = "employee_name";

        dtleaveInbox = spm.getDropdownList(spars, "SP_Update_Photo");
        if (dtleaveInbox.Rows.Count > 0)
        {
            ddlempname.DataSource = dtleaveInbox;
            //ddlDepartment.DataBind();
            ddlempname.DataTextField = "Emp_Name";
            ddlempname.DataValueField = "Emp_Code";
            ddlempname.DataBind();
            ddlempname.Items.Insert(0, new ListItem("Select Employee Name", "0")); //updated code

            
        }
    }
     
}