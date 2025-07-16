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
using System.Linq;

public partial class KRA_TemplateReport : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
        }
        if (Page.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/KRA_Index.aspx");
        }

        FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["KRAFiles"]).Trim());
        if (!Page.IsPostBack)
        {
            hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();
            FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["KRAFiles"]).Trim());

			get_DepartmentList();
        } 
    }
	public void get_DepartmentList()
	{
		DataSet dtDepartment = new DataSet();
		SqlParameter[] spars = new SqlParameter[3];
		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "get_Department_list";
		spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
		spars[1].Value = hdnloginempcode.Value;
		spars[2] = new SqlParameter("@ReportName", SqlDbType.NVarChar);
		spars[2].Value = "KRATemplateStatus";
		dtDepartment = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
		if (dtDepartment.Tables[0].Rows.Count > 0)
		{
			lstDepartment.DataSource = dtDepartment.Tables[0];
			lstDepartment.DataTextField = "Department_Name";
			lstDepartment.DataValueField = "Department_id";
			lstDepartment.DataBind();
			//lstDepartment.Items.Insert(0, new ListItem(" All Department", "0"));
		}

	}
	public Boolean CheckIsReportShow_ResetKRA()
    {
        Boolean blncheck = false;
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShow_ResetKRA";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hdnloginempcode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "View_ALL_KRA_Template_Report";



            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_KRA_GETALL_DETAILS");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    blncheck = true;
                }

            }
            // return false;
        }
        catch (Exception)
        {
             return false;
        }

        return blncheck;
    }

    private void getKRA_Template_StatusReport(string Dept_IDs)
    {


        #region get KRA Team Status
        DataSet dtKRATeam = new DataSet();
        SqlParameter[] spars = new SqlParameter[4];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Rpt_KRA_Template_status";
        spars[1] = new SqlParameter("@empcode", SqlDbType.NVarChar);
        spars[1].Value = Convert.ToString(hdnloginempcode.Value).Trim();
        spars[2] = new SqlParameter("@Dept_IDs", SqlDbType.NVarChar);
		if (Convert.ToString(Dept_IDs).Trim() != "")
			spars[2].Value = Dept_IDs;
		else
			spars[2].Value = DBNull.Value;
		spars[3] = new SqlParameter("@ReportName", SqlDbType.NVarChar);
		spars[3].Value = "KRATemplateStatus";
		dtKRATeam = spm.getDatasetList(spars, "SP_KRA_Reports");
        #endregion

        try
        {
            string strpath = Server.MapPath("~/procs/KRATemplate_StatusRpt.rdlc");
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = strpath;

            ReportDataSource rds_summary = new ReportDataSource("dsKRA_DpetTemplateStatus", dtKRATeam.Tables[0]);
            ReportDataSource rds_KRANotSubmitted = new ReportDataSource("ds_KRA_Template_Assign", dtKRATeam.Tables[1]);
            ReportDataSource rds_KRASubmitted = new ReportDataSource("ds_KRA_Template_NotAssign", dtKRATeam.Tables[2]);


            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds_summary);
            ReportViewer1.LocalReport.DataSources.Add(rds_KRANotSubmitted);
            ReportViewer1.LocalReport.DataSources.Add(rds_KRASubmitted);
            ReportViewer1.LocalReport.Refresh();
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnback_mng_Click(object sender, EventArgs e)
    {
        if(Convert.ToString(lstPeriod.SelectedValue).Trim()=="0")
        {
            lblmessage.Text = "Please select period";
            return;
        }

        string strempcode = "";
        //foreach (ListItem item in ddl_Employees.Items)
        //{
        //    if (item.Selected)
        //    {
        //        if (item.Value != "" && item.Value != "0")
        //        {
        //            if (Convert.ToString(strempcode).Trim() == "")
        //                strempcode = "'" + item.Value + "'";
        //            else
        //                strempcode = strempcode + ",'" + item.Value + "'";


        //        }
        //    }
        //}

        string strwhere = "";
        string strOrderby = "order by Emp_Name;";
        string strsql = " Select  KRA_ID,e.Emp_code,Emp_Name,k.band,p.period_name,k.project_location,d.Department_Name,ds.DesginationName,k.submitted_on,k.approved_On,k.Approved_KRA_path as KRAFileName " +
                       " from tblDepartmentMaster d " +
                       " inner join tbl_Employee_Mst e on d.Department_id = e.dept_id " +
                       " inner join tbl_KRA_trn_Main k on k.department_id = d.Department_id and k.emp_code = e.Emp_Code " +
                       "  inner join tbl_KRA_mst_period p on p.period_id = k.period_id " +
                       " inner join tbldesignationMaster ds on ds.Designation_iD = k.designation_id " +
                       " where k.period_id=" + lstPeriod.SelectedValue + " and k.department_id in (Select department_id  from tblDepartmentMaster where HOD='"+ hdnloginempcode .Value+ "')";

            if(Convert.ToString(strempcode).Trim()!="")
            {
                strwhere = " and k.emp_code in ("+strempcode+")";
            }

        strsql = strsql + Convert.ToString(strwhere).Trim() + strOrderby;
        DataTable dtTeamRpt = spm.getDataList_SQL(strsql);
         

    }
   
    public void get_Departmen_Employee_List()
    {
        DataSet dtDepartment = new DataSet();

        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Department_Employee_KRA_list";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = hdnloginempcode.Value;

        spars[2] = new SqlParameter("@period_id", SqlDbType.Int);
        if (Convert.ToString(lstPeriod.SelectedValue).Trim() != "0")
            spars[2].Value = Convert.ToInt32(lstPeriod.SelectedValue);
        else
            spars[2].Value = DBNull.Value;

           dtDepartment = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
      
        //ddl_Employees.DataSource = null;
        //ddl_Employees.DataBind();
        //if (dtDepartment.Tables[0].Rows.Count > 0)
        //{
          
        //    ddl_Employees.DataSource = dtDepartment.Tables[0];
        //    ddl_Employees.DataTextField = "Emp_Name";
        //    ddl_Employees.DataValueField = "emp_code";
        //    ddl_Employees.DataBind();

            //foreach (DataRow item in dtDepartment.Tables[0].Rows)
            //{
            //    var getName = item["Emp_Name"].ToString();
            //    foreach (ListItem itm in ddl_Employees.Items)
            //    {
            //        if (itm.Text == getName)
            //        {
            //            itm.Attributes.Add("disabled", "disabled");
            //        }
            //    }
            //}

       // }
       

    }

    //public void get_KRARoleList()
    //{
    //    DataSet dtPeriod= new DataSet();

    //    SqlParameter[] spars = new SqlParameter[2];

    //    spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
    //    spars[0].Value = "get_Roles_list";

    //    spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
    //   // spars[1].Value = hdnloginempcode.Value;


    //    dtPeriod = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
    //    if (dtPeriod.Tables[0].Rows.Count > 0)
    //    {
    //        //lstPeriod.DataSource = dtPeriod.Tables[0];
    //        //lstPeriod.DataTextField = "Role_Name";
    //        //lstPeriod.DataValueField = "Role_Id";
    //        //lstPeriod.DataBind();
    //        //lstPeriod.Items.Insert(0, new ListItem("Select Role", "0"));
    //    }

    //}


protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		string DeptIDs = string.Empty;
		DeptIDs = String.Join(",", lstDepartment.Items.OfType<ListItem>().Where(x => x.Selected == true).Select(x => x.Value).ToArray<string>());
		getKRA_Template_StatusReport(DeptIDs);
	}
}