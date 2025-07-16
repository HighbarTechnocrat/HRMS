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
using ClosedXML.Excel;


public partial class EmployeeDetailsReport : System.Web.UI.Page
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
                if (!Page.IsPostBack)
                {
                    Page.SmartNavigation = true;
                    editform.Visible = true;
                   // divmsg.Visible = false;
                    BindAllDDL();

                    

                }
                else
                {

                }
            }
         
              
        }
        catch (Exception ex)
        {
           
        }
    }

    public class CustomReportCredentials : IReportServerCredentials
    {
        private string _UserName;
        private string _PassWord;
        private string _DomainName;

        public CustomReportCredentials(string UserName, string PassWord, string DomainName)
        {
            _UserName = UserName;
            _PassWord = PassWord;
            _DomainName = DomainName;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public ICredentials NetworkCredentials
        {
            get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string user,
         out string password, out string authority)
        {
            authCookie = null;
            user = password = authority = null;
            return false;
        }
    }

    private void BindAllDDL()
    {
        try
        {
           
            var getDs = spm.getEmployeeDetail();
            if(getDs !=null)
            {
                if(getDs.Tables.Count>0)
                {
                    write2log(Convert.ToString(getDs.Tables.Count));
                    ddl_Department.DataSource = null;
                    ddl_Department.DataBind();

                    ddl_Module.DataSource = null;
                    ddl_Module.DataBind();

                    ddl_Band.DataSource = null;
                    ddl_Band.DataBind();

                    ddl_Project.DataSource = null;
                    ddl_Project.DataBind();

                    ddl_Employee.DataSource = null;
                    ddl_Employee.DataBind();

                    var getEmployee_Mst = getDs.Tables[0];
                   
                    var getDepartmentMaster = getDs.Tables[1];
                  
                    var getMODULE = getDs.Tables[2];
                   
                    var getProjectType = getDs.Tables[3];
                  
                    var getBand = getDs.Tables[4];
                   

                    //Department
                    if (getDepartmentMaster.Rows.Count > 0)
                    {
                      
                        ddl_Department.DataSource = getDepartmentMaster;
                        ddl_Department.DataTextField = "Department_Name";
                        ddl_Department.DataValueField = "Department_id";
                        ddl_Department.DataBind();
                        ddl_Department.Items.Insert(0, new ListItem("Select Department", "0"));
                    }

                    //MODULE
                    if (getMODULE.Rows.Count > 0)
                    {
                        ddl_Module.DataSource = getMODULE;
                        ddl_Module.DataTextField = "ModuleDesc";
                        ddl_Module.DataValueField = "ModuleId";
                        ddl_Module.DataBind();
                        ddl_Module.Items.Insert(0, new ListItem("Select Module", "0"));
                    }

                    if (getBand.Rows.Count > 0)
                    {
                        ddl_Band.DataSource = getBand;
                        ddl_Band.DataTextField = "BAND";
                        ddl_Band.DataValueField = "BAND";
                        ddl_Band.DataBind();
                        ddl_Band.Items.Insert(0, new ListItem("Select Band", "0"));
                    }

                    if (getProjectType.Rows.Count > 0)
                    {
                        ddl_Project.DataSource = getProjectType;
                        ddl_Project.DataTextField = "ProjectType";
                        ddl_Project.DataValueField = "Id";
                        ddl_Project.DataBind();
                        ddl_Project.Items.Insert(0, new ListItem("Select Project", "0"));
                    }

                    //Employee
                    if (getEmployee_Mst.Rows.Count > 0)
                    {
                        ddl_Employee.DataSource = getEmployee_Mst;
                        ddl_Employee.DataTextField = "Emp_Name";
                        ddl_Employee.DataValueField = "Emp_Code";
                        ddl_Employee.DataBind();
                        ddl_Employee.Items.Insert(0, new ListItem("Select Employee", "0"));
                    }
                }
            }

        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void View_Report(object sender, EventArgs e)
    {
        var ddlDepartment = "";
        var ddlProject = "";
        var ddlmodule = "";
        var ddlBand = "";
        var ddlEmployee = string.Empty;



        foreach (ListItem item in ddl_Department.Items)
        {

         
            if (item.Selected)
            {
              
                if (item.Value != "" && item.Value != "0")
                {
                    if (ddlDepartment == "")
                    {
                        ddlDepartment = item.Value;
                    }
                    else
                    {
                        ddlDepartment = ddlDepartment + "|" + item.Value;
                    }
                }
            }
        }

        foreach (ListItem item in ddl_Project.Items)
        {
            if (item.Selected)
            {
                if (item.Value != "" && item.Value != "0")
                {
                    if (ddlProject == "")
                    {
                        ddlProject = item.Value;
                    }
                    else
                    {
                        ddlProject = ddlProject + "|" + item.Value;
                    }
                }
            }
        }
        //DDL Module
        foreach (ListItem item in ddl_Module.Items)
        {
            if (item.Selected)
            {
                if (item.Value != "" && item.Value != "0")
                {
                    if (ddlmodule == "")
                    {
                        ddlmodule = item.Value;
                    }
                    else
                    {
                        ddlmodule = ddlmodule + "|" + item.Value;
                    }
                }
            }
        }

        foreach (ListItem item in ddl_Band.Items)
        {
            if (item.Selected)
            {
                if (item.Value != "" && item.Value != "0")
                {
                    if (ddlBand == "")
                    {
                        ddlBand = item.Value;
                    }
                    else
                    {
                        ddlBand = ddlBand + "|" + item.Value;
                    }
                }
            }
        }

        if (ddl_Employee.SelectedValue != "" && ddl_Employee.SelectedValue != "0")
        {
            ddlEmployee = ddl_Employee.SelectedValue;
        }

      

        BindReport(ddlEmployee, ddlBand, ddlmodule, ddlProject, ddlDepartment);

       

    }



    public void BindReport(string Emp, string Band,string Module,string Project,string Dept)
    {
      
        try
        {

            ServicePointManager
    .ServerCertificateValidationCallback +=
    (sender, cert, chain, sslPolicyErrors) => true;

            string SSRSReportPath1 = ConfigurationManager.AppSettings["SSRSReportPath"];
          
          
            ReportParameter[] parameters = new ReportParameter[5];
            parameters[0] = new ReportParameter("DepartmentId", Dept);
            parameters[1] = new ReportParameter("ModuleId", Module);
            parameters[2] = new ReportParameter("ProjectId", Project);
            parameters[3] = new ReportParameter("EmployeeCode", Emp);
            parameters[4] = new ReportParameter("BandId", Band);

            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("Administrator", "9BmCpxCUsMkmn@", "onehrdb");
             ReportViewer1.ServerReport.ReportServerCredentials = irsc;
           // ReportViewer1.ServerReport.ReportServerUrl = new Uri("http://mayur-m-hbt/ReportServer");
            ReportViewer1.ServerReport.ReportServerUrl = new Uri("https://onehrdb/ReportServer");
            ReportViewer1.ServerReport.ReportPath = SSRSReportPath1;
           
            ReportViewer1.ServerReport.SetParameters(parameters);
            ReportViewer1.ServerReport.Refresh();

        }
        catch (Exception ex)
        { 
           
         
        }
      

    }

    public static void write2log(string strmsg)
    {

        System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Configuration.ConfigurationSettings.AppSettings["LogTestPath"] +
             "Log_" + DateTime.Now.Day.ToString() + ".txt", true);
        sw.WriteLine(strmsg);
        sw.Flush();
        sw.Close();
     }







}