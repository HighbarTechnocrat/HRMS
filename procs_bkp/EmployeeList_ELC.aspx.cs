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
using System.Collections.Generic;

public partial class procs_EmployeeList_ELC : System.Web.UI.Page
{
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public int did = 0;
    SP_Methods spm = new SP_Methods();
   

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    #region Page Events

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
                    GetEmpcode();
                    GetDepartmentMaster();
                    GetDesignation();
                    GetlstPositionBand();
                    GetMainModule();
                    GVELCmethod();
                    if(!CheckIsELCReportShow())
                    {
                        Response.Redirect("~/procs/ReportsMenu.aspx");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    #endregion

    #region page Methods

    public void GetEmpcode()
    {
        SqlParameter[] spars = new SqlParameter[1];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetEmpcodeList";
        DataTable dt = spm.getDropdownList(spars, "SP_ELC_EmployeeLifeCycleList");
        lstEmployeeName.DataSource = dt;
        lstEmployeeName.DataTextField = "Emp_Name";
        lstEmployeeName.DataValueField = "Emp_Code";
        lstEmployeeName.DataBind();
        ListItem item = new ListItem("Select Employee Name", "0");
        lstEmployeeName.Items.Insert(0, item);
    }

    public void GetDepartmentMaster()
    {
        SqlParameter[] spars = new SqlParameter[1];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetDepartmentNameList";
        DataTable dt = spm.getDropdownList(spars, "SP_ELC_EmployeeLifeCycleList");
        LstDepartment.DataSource = dt;
        LstDepartment.DataTextField = "Department_Name";
        LstDepartment.DataValueField = "Department_id";
        LstDepartment.DataBind();
        ListItem item = new ListItem("Select Department", "0");
        LstDepartment.Items.Insert(0, item);
    }

    public void GetDesignation()
    {
        SqlParameter[] spars = new SqlParameter[1];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetDesignationList";
        DataTable dt = spm.getDropdownList(spars, "SP_ELC_EmployeeLifeCycleList");
        LstDesignation.DataSource = dt;
        LstDesignation.DataTextField = "DesginationName";
        LstDesignation.DataValueField = "Designation_iD";
        LstDesignation.DataBind();
        ListItem item = new ListItem("Select Designation", "0");
        LstDesignation.Items.Insert(0, item);
    }

    public void GetlstPositionBand()
    {
        SqlParameter[] spars = new SqlParameter[1];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetBandList";
        DataTable dt = spm.getDropdownList(spars, "SP_ELC_EmployeeLifeCycleList");
        Lstband.DataSource = dt;
        Lstband.DataTextField = "BAND";
        Lstband.DataValueField = "BAND";
        Lstband.DataBind();
        ListItem item = new ListItem("Select BAND", "0");
        Lstband.Items.Insert(0, item);
    }

    public void GetMainModule()
    {
        SqlParameter[] spars = new SqlParameter[1];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar); 
        spars[0].Value = "GetMainModuleList";
        DataTable dt = spm.getDropdownList(spars, "SP_ELC_EmployeeLifeCycleList");
        lstSkillSet.DataSource = dt;
        lstSkillSet.DataTextField = "ModuleDesc";
        lstSkillSet.DataValueField = "ModuleId";
        lstSkillSet.DataBind();
        ListItem item = new ListItem("Select Main Module", "0");
        lstSkillSet.Items.Insert(0, item);
    }
    

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        HFEMP_ID.Value = Convert.ToString(gvEmployeeListList.DataKeys[row.RowIndex].Values[0]).Trim();
        Response.Redirect("~/procs/EmployeeView_ELC.aspx?Emp_id=" + HFEMP_ID.Value);
    }

    #endregion

    private void GVELCmethod()
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[6];
            DataSet DS = new DataSet();
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetELCList";
            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = lstEmployeeName.SelectedValue;
            spars[2] = new SqlParameter("@DeptID", SqlDbType.VarChar);
            spars[2].Value = LstDepartment.SelectedValue;
            spars[3] = new SqlParameter("@desgID", SqlDbType.VarChar);
            spars[3].Value = LstDesignation.SelectedValue;
            spars[4] = new SqlParameter("@BandID", SqlDbType.VarChar);
            spars[4].Value = Lstband.SelectedValue;
            spars[5] = new SqlParameter("@ModuleID", SqlDbType.VarChar);
            spars[5].Value = lstSkillSet.SelectedValue;
            
            DS = spm.getDatasetList(spars, "SP_ELC_EmployeeLifeCycleList");

            if (DS.Tables[0].Rows.Count > 0)
            {
                gvEmployeeListList.DataSource = DS.Tables[0];
                gvEmployeeListList.DataBind();
                lblmessagesearch.Text = "";
            }
            else
            {
                lblmessagesearch.Text = "Record's not found";
                gvEmployeeListList.DataSource = null;
                gvEmployeeListList.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        GVELCmethod();
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        lstEmployeeName.SelectedValue = "0";
        LstDepartment.SelectedValue = "0";
        LstDesignation.SelectedValue = "0";
        Lstband.SelectedValue = "0";
        lstSkillSet.SelectedValue = "0";
        lblmessagesearch.Text = "";
        GVELCmethod();
    }

    protected void gvEmployeeListList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmployeeListList.PageIndex = e.NewPageIndex;
        GVELCmethod();
    }
    public bool CheckIsELCReportShow()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShowLOPReport";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "ELCReport";



            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                   // lnk_ELC.Visible = true;
                     return true;
                }
                else
                {
                    //lnk_ELC.Visible = false;
                     return false;
                }
            }
            return false;
        }
        catch (Exception)
        {
             return false;
        }
    }
}