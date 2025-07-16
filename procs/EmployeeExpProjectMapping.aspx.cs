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
using System.Linq;

public partial class EmployeeExpProjectMapping : System.Web.UI.Page
{

    #region CreativeMethods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    #endregion

    SP_Methods spm = new SP_Methods();
    DataSet dspaymentVoucher_Apprs = new DataSet();
    String CEOInList = "N";
    double YearlymobileAmount = 0;
    #region PageEvents
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            //mobile_btnPrintPV.Visible = false;

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {

                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    // txt_SPOCComment.Attributes.Add("maxlength", "500");
                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["EmployeeCVDCFilePath"]).Trim());
                    //TXT_AssginmentDate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                    editform.Visible = true;
                    divbtn.Visible = false;
                    divmsg.Visible = false;
                    if (Request.QueryString.Count > 0)
                    {
                        hdnempcode.Value = Convert.ToString(Request.QueryString[0]).Trim();
                    }
                    else
                    {
                        // hdnempcode.Value = Convert.ToString(Session["Empcode"]);
                        hdnempcode.Value = "0";
                    }
                    //lblmessage.Text = "testing";
                    BindEmpDetails("");
                    BindProjectDetails(Convert.ToString(Session["Empcode"]));
                    BindExpDetails("");
                    getMngRecruterInoxList();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                    hdnFamilyDetailID.Value = "0";
                }
                else
                {
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    #endregion

    #region PageMethods
    private void BindEmpDetails(string id)
    {
        try
        {
            var getDS = spm.getEmployeeExpComp("GetEmpList", "");
            if (getDS != null)
            {
                ddl_EmpName.DataSource = null;
                ddl_EmpName.DataBind();

                ddl_Search_Employee.DataSource = null;
                ddl_Search_Employee.DataBind();
                if (getDS.Rows.Count > 0)
                {
                    ddl_EmpName.DataSource = getDS;
                    ddl_EmpName.DataTextField = "Emp_Name";
                    ddl_EmpName.DataValueField = "Emp_Code";
                    ddl_EmpName.DataBind();
                    ddl_EmpName.Items.Insert(0, new ListItem("Select Employee", "0"));

                    ddl_Search_Employee.DataSource = getDS;
                    ddl_Search_Employee.DataTextField = "Emp_Name";
                    ddl_Search_Employee.DataValueField = "Emp_Code";
                    ddl_Search_Employee.DataBind();
                    ddl_Search_Employee.Items.Insert(0, new ListItem("Select Employee", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            lblmessage.Visible = true;
            lblmessage.Text = ex.ToString();
            
        }
    }
    private void BindProjectDetails(string id)
    {
        try
        {
            var getDS = spm.getEmployeeExpComp("GetLocationList", id);
            if (getDS != null)
            {
                ddl_ProjectName.DataSource = null;
                ddl_ProjectName.DataBind();

                ddl_Search_Project.DataSource = null;
                ddl_Search_Project.DataBind();
                if (getDS.Rows.Count > 0)
                {
                    ddl_ProjectName.DataSource = getDS;
                    ddl_ProjectName.DataTextField = "Location_name";
                    ddl_ProjectName.DataValueField = "comp_code";
                    ddl_ProjectName.DataBind();
                    ddl_ProjectName.Items.Insert(0, new ListItem("Select Project", "0"));

                    ddl_Search_Project.DataSource = getDS;
                    ddl_Search_Project.DataTextField = "Location_name";
                    ddl_Search_Project.DataValueField = "comp_code";
                    ddl_Search_Project.DataBind();
                    ddl_Search_Project.Items.Insert(0, new ListItem("Select Project", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            lblmessage.Visible = true;
            lblmessage.Text = ex.ToString();

        }
    }
    private void BindExpDetails(string id)
    {
        try
        {
            var getDS = spm.getEmployeeExpComp("GetPaymentVoucherList", "");
            if (getDS != null)
            {
                ddl_Expenses.DataSource = null;
                ddl_Expenses.DataBind();

                ddl_Search_Exp.DataSource = null;
                ddl_Search_Exp.DataBind();

                if (getDS.Rows.Count > 0)
                {
                    ddl_Expenses.DataSource = getDS;
                    ddl_Expenses.DataTextField = "pv";
                    ddl_Expenses.DataValueField = "pv_id";
                    ddl_Expenses.DataBind();
                    ddl_Expenses.Items.Insert(0, new ListItem("Select Expenses Type", "0"));

                    ddl_Search_Exp.DataSource = getDS;
                    ddl_Search_Exp.DataTextField = "pv";
                    ddl_Search_Exp.DataValueField = "pv_id";
                    ddl_Search_Exp.DataBind();
                    ddl_Search_Exp.Items.Insert(0, new ListItem("Select Expenses Type", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            lblmessage.Visible = true;
            lblmessage.Text = ex.ToString();

        }
    }
    #endregion

    protected void ddl_EmpName_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddl_ProjectName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            if (ddl_ProjectName.SelectedValue=="0")
            {
                ddl_Expenses.Enabled = false;
                ddl_EmpName.Attributes.Add("disabled", "disabled");
                lblmessage.Text = "Please select project name.";
                return;
            }
            else
            {
                ddl_Expenses.Enabled = true;
                ddl_EmpName.Attributes.Remove("disabled");
                BindEmpDetailsByComp(Convert.ToString(ddl_ProjectName.SelectedValue));
            }
            
        }
        catch (Exception ex)
        {
            
        }
    }

    protected void ddl_Expenses_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public void ResetControls()
    {
        BindEmpDetails("");


        ddl_Expenses.DataSource = null;
        ddl_Expenses.DataBind();

        var getVal =Convert.ToString(ddl_ProjectName.SelectedValue);
        var getVal1 =Convert.ToString(ddl_Expenses.SelectedValue);
        
        ddl_ProjectName.Items.FindByValue(getVal).Selected = false;
        ddl_ProjectName.Items.FindByValue("0").Selected = true;

        ddl_Expenses.Items.FindByValue(getVal1).Selected = false;
        ddl_Expenses.Items.FindByValue("0").Selected = true;

        ddl_Expenses.Enabled = false;
        ddl_EmpName.Attributes.Add("disabled", "disabled");
       
    }
    protected void btn_FD_Save_Click(object sender, EventArgs e)
    {
        try
        {
            var ddlEmp_Code = "";
            var ddlProject = "";
            var ddlExp = "";          
            foreach (ListItem item in ddl_EmpName.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlEmp_Code == "")
                        {
                            ddlEmp_Code = item.Value;
                        }
                        else
                        {
                            ddlEmp_Code = ddlEmp_Code + "|" + item.Value;
                        }
                    }
                }
            }
            if (ddl_ProjectName.SelectedValue == "0")
            {
                lblmessage.Text = "Please Select project";
                return;
            }
            if (ddl_Expenses.SelectedValue == "0")
            {
                lblmessage.Text = "Please Select expenses";
                return;
            }
            if (ddlEmp_Code=="")
            {
                lblmessage.Text = "Please Select employee name";
                return;
            }
            ddlProject =Convert.ToString(ddl_ProjectName.SelectedValue);
            var createdBy= Convert.ToString(Session["Empcode"]);
            var getStatus = spm.InsertUpdateExpEmpMapping("InsertEmpExpMapping", ddlEmp_Code, ddlProject, Convert.ToDouble(ddl_Expenses.SelectedValue), createdBy);
            if(getStatus==false)
            {
                lblmessage.Text = "something went wrong";
                return;
            }
            else
            {
                getMngRecruterInoxList();
                ResetControls();
                //Response.Redirect("~/PersonalDocuments.aspx");
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message;
            return;
        }
    }


    protected void gvExpMappingList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvExpMappingList.PageIndex = e.NewPageIndex;
            if(Convert.ToString(ddl_Search_Exp.SelectedValue) != "0" || Convert.ToString(ddl_Search_Project.SelectedValue) != "0" || Convert.ToString(ddl_Search_Employee.SelectedValue) != "0")
            {
                getMngRecruterInoxListSearch();
            }
            else
            {
                getMngRecruterInoxList();
            }
            
        }
        catch (Exception)
        {
        }
    }   
    private void getMngRecruterInoxList()
    {
        try
        {
            gvExpMappingList.DataSource = null;
            gvExpMappingList.DataBind();
            var ds = spm.getEmployeeExpComp("GetListExpEMPMapping", Convert.ToString(Session["Empcode"]).Trim());
            gvExpMappingList.DataSource = ds;
            gvExpMappingList.DataBind();

        }
        catch (Exception ex)
        {

        }
    }

    protected void lnk_Search_Click(object sender, EventArgs e)
    {
        try
        {
            getMngRecruterInoxListSearch();
        }
        catch (Exception ex)
        {
        }
    }
    private void getMngRecruterInoxListSearch()
    {
        try
        {
            var ddlEmp_Code = "";
            var ddlExp_Id = "";
            var ddlComp_Code = "";
            foreach (ListItem item in ddl_Search_Employee.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlEmp_Code == "")
                        {
                            ddlEmp_Code = item.Value;
                        }
                        else
                        {
                            ddlEmp_Code = ddlEmp_Code + "|" + item.Value;
                        }
                    }
                }
            }
            foreach (ListItem item in ddl_Search_Exp.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlExp_Id == "")
                        {
                            ddlExp_Id = item.Value;
                        }
                        else
                        {
                            ddlExp_Id = ddlExp_Id + "|" + item.Value;
                        }
                    }
                }
            }
            foreach (ListItem item in ddl_Search_Project.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlComp_Code == "")
                        {
                            ddlComp_Code = item.Value;
                        }
                        else
                        {
                            ddlComp_Code = ddlComp_Code + "|" + item.Value;
                        }
                    }
                }
            }
            
            gvExpMappingList.DataSource = null;
            gvExpMappingList.DataBind();
            var getDT = spm.getEmployeeExpCompSearch("GetListExpEMPMappingSearch", ddlEmp_Code, ddlComp_Code, ddlExp_Id);
            if (getDT != null)
            {
                if (getDT.Rows.Count > 0)
                {
                    gvExpMappingList.DataSource = getDT;
                    gvExpMappingList.DataBind();
                }
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void lnk_Delete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var id = Convert.ToString(gvExpMappingList.DataKeys[row.RowIndex].Values[0]).Trim();
            var status = Convert.ToString(gvExpMappingList.DataKeys[row.RowIndex].Values[1]).Trim();
            if (id != "0")
            {
                var createdBy = Convert.ToString(Session["Empcode"]);
                var getStatus = spm.ActiveInActiveExpEmpMapping("DELETEEmpExpMapping", Convert.ToDouble(id), createdBy, false);
                if (getStatus == false)
                {
                    lblmessage.Text = "something went wrong";
                    return;
                }
                else
                {
                    getMngRecruterInoxListSearch();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var id = Convert.ToString(gvExpMappingList.DataKeys[row.RowIndex].Values[0]).Trim();
            var status = Convert.ToString(gvExpMappingList.DataKeys[row.RowIndex].Values[1]).Trim();
            if (id != "0")
            {
                var createdBy = Convert.ToString(Session["Empcode"]);
                var IsStatus = false;
                if(status=="Active")
                {
                    IsStatus = false;
                }
                else if(status == "Inactive")
                {
                    IsStatus = true;
                }
                var getStatus = spm.ActiveInActiveExpEmpMapping("ActiveInActiveEmpExpMapping", Convert.ToDouble(id), createdBy, IsStatus);
                if (getStatus == false)
                {
                    lblmessage.Text = "something went wrong";
                    return;
                }
                else
                {
                    getMngRecruterInoxListSearch();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void BindEmpDetailsByComp(string Comp_Code)
    {
        try
        {
            var getDS = spm.getEmployeeExpComp("GetEmpListByCompCode", Comp_Code);
            if (getDS != null)
            {
                ddl_EmpName.DataSource = null;
                ddl_EmpName.DataBind();
                if (getDS.Rows.Count > 0)
                {
                    ddl_EmpName.DataSource = getDS;
                    ddl_EmpName.DataTextField = "Emp_Name";
                    ddl_EmpName.DataValueField = "Emp_Code";
                    ddl_EmpName.DataBind();
                    ddl_EmpName.Items.Insert(0, new ListItem("Select Employee", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            lblmessage.Visible = true;
            lblmessage.Text = ex.ToString();

        }
    }

    protected void Chk_IsStatus_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox btn = (CheckBox)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var id = Convert.ToString(gvExpMappingList.DataKeys[row.RowIndex].Values[0]).Trim();
            var status = Convert.ToString(gvExpMappingList.DataKeys[row.RowIndex].Values[1]).Trim();
            if (id != "0")
            {
                var createdBy = Convert.ToString(Session["Empcode"]);
                var IsStatus = false;
                if (status == "Active")
                {
                    IsStatus = false;
                }
                else if (status == "Inactive")
                {
                    IsStatus = true;
                }
                var getStatus = spm.ActiveInActiveExpEmpMapping("ActiveInActiveEmpExpMapping", Convert.ToDouble(id), createdBy, IsStatus);
                if (getStatus == false)
                {
                    lblmessage.Text = "something went wrong";
                    return;
                }
                else
                {
                    getMngRecruterInoxListSearch();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
}
