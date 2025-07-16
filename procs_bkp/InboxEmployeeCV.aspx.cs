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
using System.Threading;

public partial class InboxEmployeeCV : System.Web.UI.Page
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
                    txt_From.Attributes.Add("onkeypress", "return onCharOnlyNumber_EXP(event);");
                    txt_To.Attributes.Add("onkeypress", "return onCharOnlyNumber_EXP(event);");

                    txt_From_DE.Attributes.Add("onkeypress", "return onCharOnlyNumber_EXP(event);");
                    txt_To_DE.Attributes.Add("onkeypress", "return onCharOnlyNumber_EXP(event);");
                    txt_From_SAP.Attributes.Add("onkeypress", "return onCharOnlyNumber_EXP(event);");
                    txt_To_SAP.Attributes.Add("onkeypress", "return onCharOnlyNumber_EXP(event);");

                    txt_YearOfPassing.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txt_ProjectNoType.Attributes.Add("onkeypress", "return onCharOnlyNumber_Mobile(event);");

                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["EmployeeCVDCFilePath"]).Trim());
                    //TXT_AssginmentDate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                    editform.Visible = true;
                    divbtn.Visible = false;
                    divmsg.Visible = false;
                    hdnempcode.Value = Convert.ToString(Session["Empcode"]);
                    //BindEmpDetails(Convert.ToString(Session["Empcode"]), "All");
                    BindAllDDL();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
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
    private void BindAllDDL()
    {
        try
        {
            var getDs = spm.getCVDetailInbox(0, "GETALLDLL", "", "", "", "", "", null, null, "", "", "", "", "", "", "", "", "", "", "", "", null, null, null, null, null, "", "",false,"");
            if (getDs != null)
            {
                if (getDs.Tables.Count > 0)
                {
                    ddl_Department.DataSource = null;
                    ddl_Department.DataBind();

                    ddl_Designation.DataSource = null;
                    ddl_Designation.DataBind();

                    ddl_Project.DataSource = null;
                    ddl_Project.DataBind();

                    ddl_Module.DataSource = null;
                    ddl_Module.DataBind();

                    ddl_Employee.DataSource = null;
                    ddl_Employee.DataBind();

                    lstQualification.DataSource = null;
                    lstQualification.DataBind();

                    lstDegree.DataSource = null;
                    lstDegree.DataBind();

                    lstCertification.DataSource = null;
                    lstCertification.DataBind();

                    lstProject.DataSource = null;
                    lstProject.DataBind();

                    lstIndustry.DataSource = null;
                    lstIndustry.DataBind();

                    lstDesignation.DataSource = null;
                    lstDesignation.DataBind();

                    lstDomain.DataSource = null;
                    lstDomain.DataBind();

                    ddl_Band.DataSource = null;
                    ddl_Band.DataBind();

                    lstUniversity.DataSource = null;
                    lstUniversity.DataBind();

                    lstOrganisationName.DataSource = null;
                    lstOrganisationName.DataBind();

                    lstOrganisationType.DataSource = null;
                    lstOrganisationType.DataBind();

                    lst_Region.DataSource = null;
                    lst_Region.DataBind();

                    lst_Stream.DataSource = null;
                    lst_Stream.DataBind();
                    //Add new 
                    lstCertification_Code.DataSource = null;
                    lstCertification_Code.DataBind();
                    var getEmployee_Mst = getDs.Tables[0];
                    var getDepartmentMaster = getDs.Tables[1];
                    var getDesignationMaster = getDs.Tables[2];
                    var getCompany_Location = getDs.Tables[3];
                    var getMODULE = getDs.Tables[4];
                    var getEducation = getDs.Tables[5];
                    var getDegree = getDs.Tables[6];
                    var getCertification = getDs.Tables[7];
                    var getProjectType = getDs.Tables[8];
                    var getIndustryType = getDs.Tables[9];
                    var getRoleDesignation = getDs.Tables[10];
                    var getDomain = getDs.Tables[11];
                    var getBand = getDs.Tables[12];
                    var getBoard = getDs.Tables[13];
                    var getOrganisationName = getDs.Tables[14];
                    var getOrganisationType = getDs.Tables[15];
                    var getRegion = getDs.Tables[16];
                    var getStream = getDs.Tables[17];
                    var getCertificationCode = getDs.Tables[18];


                    //Department
                    if (getDepartmentMaster.Rows.Count > 0)
                    {
                        ddl_Department.DataSource = getDepartmentMaster;
                        ddl_Department.DataTextField = "Department_Name";
                        ddl_Department.DataValueField = "Department_id";
                        ddl_Department.DataBind();
                        ddl_Department.Items.Insert(0, new ListItem("Select Department", "0"));
                    }
                    //Desgination
                    if (getDesignationMaster.Rows.Count > 0)
                    {
                        ddl_Designation.DataSource = getDesignationMaster;
                        ddl_Designation.DataTextField = "DesginationName";
                        ddl_Designation.DataValueField = "Designation_iD";
                        ddl_Designation.DataBind();
                        ddl_Designation.Items.Insert(0, new ListItem("Select Desgination", "0"));
                    }
                    //Location
                    if (getCompany_Location.Rows.Count > 0)
                    {
                        ddl_Project.DataSource = getCompany_Location;
                        ddl_Project.DataTextField = "Location_name";
                        ddl_Project.DataValueField = "comp_code";
                        ddl_Project.DataBind();
                        ddl_Project.Items.Insert(0, new ListItem("Select Project", "0"));
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
                    //Employee
                    if (getEmployee_Mst.Rows.Count > 0)
                    {
                        ddl_Employee.DataSource = getEmployee_Mst;
                        ddl_Employee.DataTextField = "Emp_Name";
                        ddl_Employee.DataValueField = "Emp_Code";
                        ddl_Employee.DataBind();
                        ddl_Employee.Items.Insert(0, new ListItem("Select Employee", "0"));
                    }
                    //Education Type
                    if (getEducation.Rows.Count > 0)
                    {
                        lstQualification.DataSource = getEducation;
                        lstQualification.DataTextField = "EducationType";
                        lstQualification.DataValueField = "EducationTypeID";
                        lstQualification.DataBind();
                        lstQualification.Items.Insert(0, new ListItem("Select Qualification", "0"));
                    }
                    //Degree Type
                    if (getDegree.Rows.Count > 0)
                    {
                        lstDegree.DataSource = getDegree;
                        lstDegree.DataTextField = "Degree";
                        lstDegree.DataValueField = "Id";
                        lstDegree.DataBind();
                        lstDegree.Items.Insert(0, new ListItem("Select Degree", "0"));
                    }
                    //Certification Type
                    if (getCertification.Rows.Count > 0)
                    {
                        lstCertification.DataSource = getCertification;
                        lstCertification.DataTextField = "Certification";
                        lstCertification.DataValueField = "Id";
                        lstCertification.DataBind();
                        lstCertification.Items.Insert(0, new ListItem("Select Certification", "0"));
                    }
                    //Project Type
                    if (getProjectType.Rows.Count > 0)
                    {
                        lstProject.DataSource = getProjectType;
                        lstProject.DataTextField = "ProjectType";
                        lstProject.DataValueField = "Id";
                        lstProject.DataBind();
                        lstProject.Items.Insert(0, new ListItem("Select Project Type", "0"));
                    }
                    //Industry Type
                    if (getIndustryType.Rows.Count > 0)
                    {
                        lstIndustry.DataSource = getIndustryType;
                        lstIndustry.DataTextField = "IndustryType";
                        lstIndustry.DataValueField = "Id";
                        lstIndustry.DataBind();
                        lstIndustry.Items.Insert(0, new ListItem("Select Industry Type", "0"));
                    }
                    //Designation Type
                    if (getRoleDesignation.Rows.Count > 0)
                    {
                        lstDesignation.DataSource = getRoleDesignation;
                        lstDesignation.DataTextField = "Designation";
                        lstDesignation.DataValueField = "Id";
                        lstDesignation.DataBind();
                        lstDesignation.Items.Insert(0, new ListItem("Select Role/Designation", "0"));
                    }
                    //Domain Type
                    if (getDomain.Rows.Count > 0)
                    {
                        lstDomain.DataSource = getDomain;
                        lstDomain.DataTextField = "Domain";
                        lstDomain.DataValueField = "Id";
                        lstDomain.DataBind();
                        lstDomain.Items.Insert(0, new ListItem("Select Domain", "0"));
                    }
                    //
                    if (getBand.Rows.Count > 0)
                    {
                        ddl_Band.DataSource = getBand;
                        ddl_Band.DataTextField = "BAND";
                        ddl_Band.DataValueField = "BAND";
                        ddl_Band.DataBind();
                        ddl_Band.Items.Insert(0, new ListItem("Select Band", "0"));
                    }
                    if (getBoard.Rows.Count > 0)
                    {
                        lstUniversity.DataSource = getBoard;
                        lstUniversity.DataTextField = "Board";
                        lstUniversity.DataValueField = "Id";
                        lstUniversity.DataBind();
                        lstUniversity.Items.Insert(0, new ListItem("Select University", "0"));
                    }
                    if (getOrganisationName.Rows.Count > 0)
                    {
                        lstOrganisationName.DataSource = getOrganisationName;
                        lstOrganisationName.DataTextField = "OrganisationName";
                        lstOrganisationName.DataValueField = "Id";
                        lstOrganisationName.DataBind();
                        lstOrganisationName.Items.Insert(0, new ListItem("Select Organisation Name", "0"));
                    }
                    if (getOrganisationType.Rows.Count > 0)
                    {
                        lstOrganisationType.DataSource = getOrganisationType;
                        lstOrganisationType.DataTextField = "OrganisationType";
                        lstOrganisationType.DataValueField = "Id";
                        lstOrganisationType.DataBind();
                        lstOrganisationType.Items.Insert(0, new ListItem("Select Organisation Type", "0"));
                    }
                    if (getRegion.Rows.Count > 0)
                    {
                        lst_Region.DataSource = getRegion;
                        lst_Region.DataTextField = "Region";
                        lst_Region.DataValueField = "Id";
                        lst_Region.DataBind();
                        lst_Region.Items.Insert(0, new ListItem("Select Region ", "0"));
                    }
                    if (getStream.Rows.Count > 0)
                    {
                        lst_Stream.DataSource = getStream;
                        lst_Stream.DataTextField = "Stream";
                        lst_Stream.DataValueField = "Id";
                        lst_Stream.DataBind();
                        lst_Stream.Items.Insert(0, new ListItem("Select Stream ", "0"));
                    }
                    //Certification Code
                    if (getCertificationCode.Rows.Count > 0)
                    {
                        lstCertification_Code.DataSource = getCertificationCode;
                        lstCertification_Code.DataTextField = "CourseCode";
                        lstCertification_Code.DataValueField = "CourseCode";
                        lstCertification_Code.DataBind();
                        lstCertification_Code.Items.Insert(0, new ListItem("Select Certification Code", "0"));
                    }
                }
            }

        }
        catch (Exception)
        {

            throw;
        }
    }
    private void BindEmpDetails(string empCode, string type)
    {
        try
        {
            var getDS = spm.getCVEMPDetailById(empCode);
            if (getDS != null)
            {
                var isMarried = "";
                if (getDS.Tables.Count > 0)
                {

                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void BindGrid()
    {
        try
        {

            var ddlDepartment = "";
            var ddlDesignation = "";
            var ddlProject = "";
            var ddlmodule = "";
            var ddlQualification = "";
            var ddlDegree = "";
            var ddlCertification = "";
            var ddlCertification_Code = "";
            var ddlProjectType = "";
            var ddlIndustryType = "";
            var ddlRole = "";
            var ddlDomain = "";
            var ddlBand = "";
            var ddlUniversity = "";
            var ddlOrganisationType = "";
            var ddlOrganisationName = "";
            var ddlStream = "";
            var ddlRegion = "";
            var ddlSelect = string.Empty;
            int? year = null;
            var ddlEmployee = string.Empty;
            decimal? overAllFrom = null;
            decimal? overAllTo = null;
            //
            decimal? projectFrom = null;
            decimal? projectTo = null;
            //getddl Department
            decimal? domainFrom = null;
            decimal? domainTo = null;
            // Department
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
            //DDL Designations
            foreach (ListItem item in ddl_Designation.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlDesignation == "")
                        {
                            ddlDesignation = item.Value;
                        }
                        else
                        {
                            ddlDesignation = ddlDesignation + "|" + item.Value;
                        }
                    }
                }
            }
            //DDL ddlProject
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
            // Qualification
            foreach (ListItem item in lstQualification.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlQualification == "")
                        {
                            ddlQualification = item.Value;
                        }
                        else
                        {
                            ddlQualification = ddlQualification + "|" + item.Value;
                        }
                    }
                }
            }
            // Degree
            foreach (ListItem item in lstDegree.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlDegree == "")
                        {
                            ddlDegree = item.Value;
                        }
                        else
                        {
                            ddlDegree = ddlDegree + "|" + item.Value;
                        }
                    }
                }
            }
            //Certifiation
            foreach (ListItem item in lstCertification.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlCertification == "")
                        {
                            ddlCertification = item.Value;
                        }
                        else
                        {
                            ddlCertification = ddlCertification + "|" + item.Value;
                        }
                    }
                }
            }
            //Project Type
            var getProjectTypeText = "";
            foreach (ListItem item in lstProject.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlProjectType == "")
                        {
                            ddlProjectType = item.Value;
                        }
                        else
                        {
                            ddlProjectType = ddlProjectType + "|" + item.Value;
                        }
                        //
                        if (getProjectTypeText == "")
                        {
                            getProjectTypeText = item.Text;
                        }
                        else
                        {
                            getProjectTypeText = getProjectTypeText + "|" + item.Text;
                        }
                    }
                }
            }
            //Industry
            foreach (ListItem item in lstIndustry.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlIndustryType == "")
                        {
                            ddlIndustryType = item.Value;
                        }
                        else
                        {
                            ddlIndustryType = ddlIndustryType + "|" + item.Value;
                        }
                    }
                }
            }
            // Role And Designation
            foreach (ListItem item in lstDesignation.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlRole == "")
                        {
                            ddlRole = item.Value;
                        }
                        else
                        {
                            ddlRole = ddlRole + "|" + item.Value;
                        }
                    }
                }
            }
            //Domain
            foreach (ListItem item in lstDomain.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlDomain == "")
                        {
                            ddlDomain = item.Value;
                        }
                        else
                        {
                            ddlDomain = ddlDomain + "|" + item.Value;
                        }
                    }
                }
            }
            //Band
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
            // University
            foreach (ListItem item in lstUniversity.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlUniversity == "")
                        {
                            ddlUniversity = item.Value;
                        }
                        else
                        {
                            ddlUniversity = ddlUniversity + "|" + item.Value;
                        }
                    }
                }
            }
            // Organisation Type
            foreach (ListItem item in lstOrganisationType.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlOrganisationType == "")
                        {
                            ddlOrganisationType = item.Value;
                        }
                        else
                        {
                            ddlOrganisationType = ddlOrganisationType + "|" + item.Value;
                        }
                    }
                }
            }
            // Organisation Name
            foreach (ListItem item in lstOrganisationName.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlOrganisationName == "")
                        {
                            ddlOrganisationName = item.Value;
                        }
                        else
                        {
                            ddlOrganisationName = ddlOrganisationName + "|" + item.Value;
                        }
                    }
                }
            }
            // Region
            foreach (ListItem item in lst_Region.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlRegion == "")
                        {
                            ddlRegion = item.Value;
                        }
                        else
                        {
                            ddlRegion = ddlRegion + "|" + item.Value;
                        }
                    }
                }
            }
            // Stream
            foreach (ListItem item in lst_Stream.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlStream == "")
                        {
                            ddlStream = item.Value;
                        }
                        else
                        {
                            ddlStream = ddlStream + "|" + item.Value;
                        }
                    }
                }
            }
            //Certifiation Code
            foreach (ListItem item in lstCertification_Code.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlCertification_Code == "")
                        {
                            ddlCertification_Code = item.Value;
                        }
                        else
                        {
                            ddlCertification_Code = ddlCertification_Code + "|" + item.Value;
                        }
                    }
                }
            }
            //ddlSelect
            if (ddl_Status.SelectedValue != "" && ddl_Status.SelectedValue != "0")
            {
                ddlSelect = ddl_Status.SelectedValue;
            }

            // Employee Code
            if (ddl_Employee.SelectedValue != "" && ddl_Employee.SelectedValue != "0")
            {
                ddlEmployee = ddl_Employee.SelectedValue;
            }
            //Year Of Passing
            if (txt_YearOfPassing.Text != "")
            {
                year = Convert.ToInt32(txt_YearOfPassing.Text);
            }
            //ddlSelect
            if (txt_From.Text.Trim() != "")
            {
                overAllFrom = decimal.Parse(txt_From.Text.Trim());
            }
            if (txt_To.Text.Trim() != "")
            {
                overAllTo = decimal.Parse(txt_To.Text.Trim());
            }
            if (txt_From_DE.Text.Trim() != "")
            {
                domainFrom = decimal.Parse(txt_From_DE.Text.Trim());
            }
            if (txt_To_DE.Text.Trim() != "")
            {
                domainTo = decimal.Parse(txt_To_DE.Text.Trim());
            }
            if (txt_From_SAP.Text.Trim() != "")
            {
                projectFrom = decimal.Parse(txt_From_SAP.Text.Trim());
            }
            if (txt_To_SAP.Text.Trim() != "")
            {
                projectTo = decimal.Parse(txt_To_SAP.Text.Trim());
            }
            var IsCompletedCertification = chk_Completed_Certification.Checked;

            var getDS = spm.getCVDetailInbox(0, "GetInboxDetails", ddlEmployee, ddlDepartment, ddlDesignation, ddlProject, ddlmodule, overAllFrom, overAllTo, ddlSelect, ddlQualification, ddlDegree, ddlCertification, ddlProjectType, ddlIndustryType, ddlRole, ddlDomain, ddlBand, ddlUniversity, ddlOrganisationType, ddlOrganisationName, year, projectFrom, projectTo, domainFrom, domainTo, ddlRegion, ddlStream, IsCompletedCertification,ddlCertification_Code);
            if (getDS != null)
            {
                if (getDS.Tables.Count > 0)
                {
                    var getDetails = getDS.Tables[0];
                    if (getDetails.Rows.Count > 0)
                    {
                        var getPageCount = 1;
                        if(getDetails.Rows.Count>10)
                        {
                            getPageCount = (int)getDetails.Rows.Count / 10; 
                        }
                        spanNoRe.Visible = true;
                        spanNoRe.InnerText = "No Of Records: " + getDetails.Rows.Count + " | Page Count: " + getPageCount;

                        var IsUpdated = false;
                        var status = "Compliant";
                        foreach (DataRow item1 in getDetails.Rows)
                        {
                            //m = m + " Test5 + "+getDetails.Rows.Count;
                            bool IsDepartment = true, IsBand = true, IsModule = true, IsDesignation = true, IsEmployee = true, IsProject = true, IsQualification = true;
                            bool IsUniversity = true, IsDegree = true, IsProjectType = true, IsCertification = true, IsRole = true, IsDomain = true, IsOrganisationName = true;
                            bool IsOrganisationType = true, IsIndustryType = true, Isyear = true, IsoverAllFrom = true, IsdomainFrom = true, IsprojectFrom = true, IsStream = true, IsRegion = true;
                            bool IsCertification_Code = true;
                            var getEmpId = Convert.ToString(item1["Emp_Code"]);
                            var getDeptId = Convert.ToString(item1["DeptId"]);
                            var getDesigId = Convert.ToString(item1["DesigId"]);
                            var getgrade = Convert.ToString(item1["grade"]);
                            var getEmpLocation = Convert.ToString(item1["EmpLocation"]);
                            var getTotalSAPExperience = Convert.ToString(item1["TotalSAPExperience"]);
                            var getOverallWorkExperience = Convert.ToString(item1["OverallWorkExperience"]);
                            var getTotalDomainExperience = Convert.ToString(item1["TotalDomainExperience"]);
                            if (ddlDepartment != "")
                            {
                                var splitVal = ddlDepartment.Split('|');
                                if (splitVal.Contains(getDeptId))
                                {
                                    IsDepartment = true;
                                }
                                else
                                {
                                    IsDepartment = false;
                                }
                            }
                            if (ddlBand != "")
                            {
                                var splitVal = ddlBand.Split('|');
                                if (splitVal.Contains(getgrade))
                                {
                                    IsBand = true;
                                }
                                else
                                {
                                    IsBand = false;
                                }
                            }
                            if (ddlmodule != "")
                            {
                                var splitVal = ddlmodule.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckModuleExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsModule = true;
                                        }
                                        else
                                        {
                                            IsModule = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsModule = false;
                                    }
                                }
                            }
                            if (ddlDesignation != "")
                            {
                                var splitVal = ddlDesignation.Split('|');
                                if (splitVal.Contains(getDesigId))
                                {
                                    IsDesignation = true;
                                }
                                else
                                {
                                    IsDesignation = false;
                                }
                            }
                            if (ddlEmployee != "")
                            {
                                var splitVal = ddlEmployee;
                                if (splitVal.Contains(getEmpId))
                                {
                                    IsEmployee = true;
                                }
                                else
                                {
                                    IsEmployee = false;
                                }
                            }
                            if (ddlProject != "")
                            {
                                var splitVal = ddlProject.Split('|');
                                if (splitVal.Contains(getEmpLocation))
                                {
                                    IsProject = true;
                                }
                                else
                                {
                                    IsProject = false;
                                }
                            }
                            if (ddlQualification != "")
                            {
                                var splitVal = ddlQualification.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckQualificationExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsQualification = true;
                                        }
                                        else
                                        {
                                            IsQualification = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsQualification = false;
                                    }
                                }
                            }
                            if (ddlUniversity != "")
                            {
                                var splitVal = ddlUniversity.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckBoardExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsUniversity = true;
                                        }
                                        else
                                        {
                                            IsUniversity = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsUniversity = false;
                                    }
                                }
                            }
                            if (ddlDegree != "")
                            {
                                var splitVal = ddlDegree.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckDegreeExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsDegree = true;
                                        }
                                        else
                                        {
                                            IsDegree = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsDegree = false;
                                    }
                                }
                            }
                            if (ddlProjectType != "")
                            {
                                var splitVal = ddlProjectType.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckProjectTypeExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsProjectType = true;
                                        }
                                        else
                                        {
                                            IsProjectType = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsProjectType = false;
                                    }
                                }
                            }
                            
                            if (ddlCertification != "")
                            {
                                var splitVal = ddlCertification.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckCertificationExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsCertification = true;
                                        }
                                        else
                                        {
                                            IsCertification = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsCertification = false;
                                    }
                                }
                            }
                            if (ddlRole != "")
                            {
                                var splitVal = ddlRole.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckRoleExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsRole = true;
                                        }
                                        else
                                        {
                                            IsRole = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsRole = false;
                                    }
                                }
                            }
                            if (ddlDomain != "")
                            {
                                var splitVal = ddlDomain.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckDomainExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsDomain = true;
                                        }
                                        else
                                        {
                                            IsDomain = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsDomain = false;
                                    }
                                }
                            }
                            if (ddlOrganisationName != "")
                            {
                                var splitVal = ddlOrganisationName.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckOrganisationNameExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsOrganisationName = true;
                                        }
                                        else
                                        {
                                            IsOrganisationName = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsOrganisationName = false;
                                    }
                                }
                            }
                            if (ddlOrganisationType != "")
                            {
                                var splitVal = ddlOrganisationType.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckOrganisationTypeExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsOrganisationName = true;
                                        }
                                        else
                                        {
                                            IsOrganisationName = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsOrganisationName = false;
                                    }
                                }
                            }
                            if (ddlRegion != "")
                            {
                                var splitVal = ddlRegion.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckRegionExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsRegion = true;
                                        }
                                        else
                                        {
                                            IsRegion = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsRegion = false;
                                    }
                                }
                            }
                            if (ddlStream != "")
                            {
                                var splitVal = ddlStream.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckStreamExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsStream = true;
                                        }
                                        else
                                        {
                                            IsStream = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsStream = false;
                                    }
                                }
                            }
                            if (ddlIndustryType != "")
                            {
                                var splitVal = ddlIndustryType.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckIndustryTypeExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsIndustryType = true;
                                        }
                                        else
                                        {
                                            IsIndustryType = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsIndustryType = false;
                                    }
                                }
                            }
                            if (year != null)
                            {
                                var getStatus = spm.CheckIsIdsExists("CheckYearExists", getEmpId, Convert.ToInt32(year));
                                if (getStatus.Rows.Count > 0)
                                {
                                    var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                    if (getMsg == "EXISTS")
                                    {
                                        Isyear = true;
                                    }
                                    else
                                    {
                                        Isyear = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    Isyear = false;
                                }
                            }
                            if (overAllFrom != null)
                            {
                                if (overAllFrom >= Convert.ToDecimal(getOverallWorkExperience) && overAllTo <= Convert.ToDecimal(getOverallWorkExperience))
                                {
                                    IsoverAllFrom = true;
                                }
                                else
                                {
                                    IsoverAllFrom = false;
                                }
                            }
                            if (domainFrom != null)
                            {
                                if (domainFrom >= Convert.ToDecimal(getTotalDomainExperience) && domainTo <= Convert.ToDecimal(getTotalDomainExperience))
                                {
                                    IsdomainFrom = true;
                                }
                                else
                                {
                                    IsdomainFrom = false;
                                }
                            }
                            if (projectFrom != null)
                            {
                                if (projectFrom >= Convert.ToDecimal(getTotalSAPExperience) && projectTo <= Convert.ToDecimal(getTotalSAPExperience))
                                {
                                    IsprojectFrom = true;
                                }
                                else
                                {
                                    IsprojectFrom = false;
                                }
                            }

                            if (ddlCertification_Code != "")
                            {
                                var splitVal = ddlCertification_Code.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsCertificationCodeExists("CheckCertificationCodeExists", getEmpId, Convert.ToString(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsCertification_Code = true;
                                        }
                                        else
                                        {
                                            IsCertification_Code = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsCertification_Code = false;
                                    }
                                }
                            }

                            if (IsDepartment == true && IsBand == true && IsModule == true && IsDesignation == true && IsEmployee == true && IsProject == true && IsQualification == true && IsUniversity == true && IsDegree == true && IsProjectType == true && IsCertification == true && IsRole == true && IsDomain == true && IsOrganisationName == true && IsOrganisationType == true && IsIndustryType == true && Isyear == true && IsoverAllFrom == true && IsdomainFrom == true && IsprojectFrom == true && IsRegion == true && IsStream == true && IsCertification_Code==true)
                            {
                                item1["Criteria"] = "Compliant";
                            }
                        }

                        if (Convert.ToString(ddl_Condition.SelectedValue).Trim() != "0")
                        {
                            if (Convert.ToString(txt_ProjectNoType.Text).Trim() != "" && Convert.ToString(txt_ProjectNoType.Text).Trim() != "0")
                            {
                                var dt2 = new DataTable();
                                var newTable = new DataTable();

                                newTable.Columns.Add("Emp_Code");
                                newTable.Columns.Add("Emp_id");
                                newTable.Columns.Add("Emp_Name");
                                newTable.Columns.Add("Department");
                                newTable.Columns.Add("Designation");
                                newTable.Columns.Add("emp_projectName");
                                newTable.Columns.Add("DOJ");
                                newTable.Columns.Add("Module1");
                                newTable.Columns.Add("Module2");
                                newTable.Columns.Add("Module3");
                                newTable.Columns.Add("emp_status");
                                newTable.Columns.Add("IsComplete");
                                newTable.Columns.Add("Criteria");
                                newTable.Columns.Add("DeptId");
                                newTable.Columns.Add("DesigId");
                                newTable.Columns.Add("grade");
                                newTable.Columns.Add("OverallWorkExperience");
                                newTable.Columns.Add("TotalSAPExperience");
                                newTable.Columns.Add("TotalDomainExperience");
                                newTable.Columns.Add("EmpLocation");
                                newTable.Columns.Add("SecondaryModule");
                                newTable.Columns.Add("FLCI");
                                newTable.Columns.Add("Rollout");
                                newTable.Columns.Add("Migration");
                                newTable.Columns.Add("Support");
                                newTable.Columns.Add("Enhancements");
                                newTable.Columns.Add("Others");
                                newTable.Columns.Add("HighestQualification");
                                var getNumber = Convert.ToInt32(txt_ProjectNoType.Text);
                                var getVal = Convert.ToString(ddl_Condition.SelectedValue).Trim();
                                var split = getProjectTypeText.Split('|');
                                foreach (var item in split)
                                {
                                    if (item.ToString() == "Full Life Cycle Implementation")
                                    {
                                        if (getVal == "GT")
                                        {
                                            //Greater Than - >
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("FLCI") > getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                            // var getResult = getDetails.AsEnumerable().Any(row => row.Field<int>("QualificationId") == int.Parse(itm.Value.ToString()))
                                        }
                                        else if (getVal == "LT")
                                        {
                                            //Less Than - <
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("FLCI") < getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else if (getVal == "GTE")
                                        {
                                            //Greater Than Equal To - >=
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("FLCI") >= getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else if (getVal == "LTE")
                                        {
                                            //Less Than Equal TO - <=
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("FLCI") <= getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else if (getVal == "EQ")
                                        {
                                            // Equal TO - =
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("FLCI") == getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else if (item.ToString() == "Rollout")
                                    {
                                        if (getVal == "GT")
                                        {
                                            //Greater Than - >
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Rollout") > getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);

                                        }
                                        else if (getVal == "LT")
                                        {
                                            //Less Than - <
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Rollout") < getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else if (getVal == "GTE")
                                        {
                                            //Greater Than Equal To - >=
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Rollout") >= getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else if (getVal == "LTE")
                                        {
                                            //Less Than Equal TO - <=
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Rollout") <= getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else if (getVal == "EQ")
                                        {
                                            // Equal TO - =
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Rollout") == getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else if (item.ToString() == "Migration")
                                    {
                                        if (getVal == "GT")
                                        {
                                            //Greater Than - >
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Migration") > getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);

                                        }
                                        else if (getVal == "LT")
                                        {
                                            //Less Than - <
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Migration") < getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else if (getVal == "GTE")
                                        {
                                            //Greater Than Equal To - >=
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Migration") >= getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else if (getVal == "LTE")
                                        {
                                            //Less Than Equal TO - <=
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Migration") <= getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else if (getVal == "EQ")
                                        {
                                            // Equal TO - =
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Migration") == getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else if (item.ToString() == "Support")
                                    {
                                        if (getVal == "GT")
                                        {
                                            //Greater Than - >
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Support") > getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else if (getVal == "LT")
                                        {
                                            //Less Than - <
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Support") < getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else if (getVal == "GTE")
                                        {
                                            //Greater Than Equal To - >=
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Support") >= getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else if (getVal == "LTE")
                                        {
                                            //Less Than Equal TO - <=
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Support") <= getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else if (getVal == "EQ")
                                        {
                                            // Equal TO - =
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Support") == getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else if (item.ToString() == "Enhancements")
                                    {
                                        if (getVal == "GT")
                                        {
                                            //Greater Than - >
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Enhancements") > getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else if (getVal == "LT")
                                        {
                                            //Less Than - <
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Enhancements") < getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else if (getVal == "GTE")
                                        {
                                            //Greater Than Equal To - >=
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Enhancements") >= getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else if (getVal == "LTE")
                                        {
                                            //Less Than Equal TO - <=
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Enhancements") <= getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else if (getVal == "EQ")
                                        {
                                            // Equal TO - =
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Enhancements") == getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else//Other
                                    {
                                        if (getVal == "GT")
                                        {
                                            //Greater Than - >
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Others") > getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else if (getVal == "LT")
                                        {
                                            //Less Than - <
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Others") < getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else if (getVal == "GTE")
                                        {
                                            //Greater Than Equal To - >=
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Others") >= getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else if (getVal == "LTE")
                                        {
                                            //Less Than Equal TO - <=
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Others") <= getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else if (getVal == "EQ")
                                        {
                                            // Equal TO - =
                                            getDetails.AsEnumerable().Where(x => x.Field<int?>("Others") == getNumber).CopyToDataTable(newTable, LoadOption.OverwriteChanges);
                                        }
                                        else
                                        {

                                        }
                                    }
                                }
                                //Bind Datatable
                                gv_EmployeeDetails.DataSource = newTable;
                                gv_EmployeeDetails.DataBind();
                            }
                            else
                            {
                                lblmessage.Text = "Enter No Of Project Worked On";
                                return;
                            }
                        }
                        else
                        {

                            gv_EmployeeDetails.DataSource = getDetails;
                            gv_EmployeeDetails.DataBind();
                        }

                    }

                }
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message;
            return;
        }
    }
    #endregion

    protected void lnk_ed_Search_Click(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            gv_EmployeeDetails.DataSource = null;
            gv_EmployeeDetails.DataBind();

            if (rbWildSearch.Checked == true)
            {
                WildSearch();
            }
            else
            {

                if (txt_From.Text.Trim() != "")
                {
                    if (txt_To.Text.Trim() == "")
                    {
                        lblmessage.Text = "Please enter both  overall experience from and to";
                        return;
                    }
                    else
                    {
                        var getFrom = decimal.Parse(txt_From.Text.Trim());
                        var getTo = decimal.Parse(txt_To.Text.Trim());
                        if (getFrom >= getTo)
                        {
                            lblmessage.Text = "from overall experience value can't be greater than to overall experience";
                            txt_To.Text = "";
                            return;
                        }
                    }
                }
                if (txt_From.Text.Trim() == "" && txt_To.Text.Trim() != "")
                {
                    lblmessage.Text = "Please enter both  SAP experience from and to ";

                    return;
                }
                //SAP
                if (txt_From_SAP.Text.Trim() != "")
                {
                    if (txt_To_SAP.Text.Trim() == "")
                    {
                        lblmessage.Text = "Please enter both  SAP experience from and to";
                        return;
                    }
                    else
                    {
                        var getFrom = decimal.Parse(txt_From_SAP.Text.Trim());
                        var getTo = decimal.Parse(txt_To_SAP.Text.Trim());
                        if (getFrom >= getTo)
                        {
                            lblmessage.Text = "from SAP experience value can't be greater than to SAP experience";
                            txt_To_SAP.Text = "";
                            return;
                        }
                    }
                }
                if (txt_From_SAP.Text.Trim() == "" && txt_To_SAP.Text.Trim() != "")
                {
                    lblmessage.Text = "Please enter both  SAP experience from and to ";

                    return;
                }
                //SAP
                if (txt_From_DE.Text.Trim() != "")
                {
                    if (txt_To_DE.Text.Trim() == "")
                    {
                        lblmessage.Text = "Please enter both  domain experience from and to";
                        return;
                    }
                    else
                    {
                        var getFrom = decimal.Parse(txt_From_DE.Text.Trim());
                        var getTo = decimal.Parse(txt_To_DE.Text.Trim());
                        if (getFrom >= getTo)
                        {
                            lblmessage.Text = "from domain experience value can't be greater than to SAP experience";
                            txt_From_DE.Text = "";
                            return;
                        }
                    }
                }
                if (txt_From_DE.Text.Trim() == "" && txt_To_DE.Text.Trim() != "")
                {
                    lblmessage.Text = "Please enter both  domain experience from and to ";

                    return;
                }
                // lblmessage.Text = "Teeeeee;";
                BindGrid();
            }


        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message;
            Label2.Text = ex.Message;
            return;
        }

    }

    protected void lnk_ed_Clear_Click(object sender, EventArgs e)
    {
        try
        {
            BindAllDDL();
            txt_From.Text = "";
            txt_To.Text = "";
            txt_From_DE.Text = "";
            txt_To_DE.Text = "";
            txt_From_SAP.Text = "";
            txt_To_SAP.Text = "";
            txt_YearOfPassing.Text = "";
            txt_Wild_Search.Text = "";
            txt_ProjectNoType.Text = "";//ddl_Condition
            var getSelectedValues = ddl_Condition.SelectedValue.ToString();
            ddl_Condition.Items.FindByValue(getSelectedValues).Selected = false;
            ddl_Condition.Items.FindByValue("0").Selected = true;
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnk_ed_Download_Click(object sender, EventArgs e)
    {
        try
        {

            lblmessage.Text = "";

            if (rbWildSearch.Checked == true)
            {
                WildSearchDownload();
            }
            else
            {
                if (txt_From.Text.Trim() != "")
                {
                    if (txt_To.Text.Trim() == "")
                    {
                        lblmessage.Text = "Please enter both  overall experience from and to";
                        return;
                    }
                    else
                    {
                        var getFrom = decimal.Parse(txt_From.Text.Trim());
                        var getTo = decimal.Parse(txt_To.Text.Trim());
                        if (getFrom >= getTo)
                        {
                            lblmessage.Text = "from overall experience value can't be greater than to overall experience";
                            txt_To.Text = "";
                            return;
                        }
                    }
                }
                if (txt_From.Text.Trim() == "" && txt_To.Text.Trim() != "")
                {
                    lblmessage.Text = "Please enter both  SAP experience from and to ";

                    return;
                }
                //SAP
                if (txt_From_SAP.Text.Trim() != "")
                {
                    if (txt_To_SAP.Text.Trim() == "")
                    {
                        lblmessage.Text = "Please enter both  SAP experience from and to";
                        return;
                    }
                    else
                    {
                        var getFrom = decimal.Parse(txt_From_SAP.Text.Trim());
                        var getTo = decimal.Parse(txt_To_SAP.Text.Trim());
                        if (getFrom >= getTo)
                        {
                            lblmessage.Text = "from SAP experience value can't be greater than to SAP experience";
                            txt_To_SAP.Text = "";
                            return;
                        }
                    }
                }
                if (txt_From_SAP.Text.Trim() == "" && txt_To_SAP.Text.Trim() != "")
                {
                    lblmessage.Text = "Please enter both  SAP experience from and to ";

                    return;
                }
                //SAP
                if (txt_From_DE.Text.Trim() != "")
                {
                    if (txt_To_DE.Text.Trim() == "")
                    {
                        lblmessage.Text = "Please enter both  domain experience from and to";
                        return;
                    }
                    else
                    {
                        var getFrom = decimal.Parse(txt_From_DE.Text.Trim());
                        var getTo = decimal.Parse(txt_To_DE.Text.Trim());
                        if (getFrom >= getTo)
                        {
                            lblmessage.Text = "from domain experience value can't be greater than to SAP experience";
                            txt_From_DE.Text = "";
                            return;
                        }
                    }
                }
                if (txt_From_DE.Text.Trim() == "" && txt_To_DE.Text.Trim() != "")
                {
                    lblmessage.Text = "Please enter both  domain experience from and to ";

                    return;
                }
                DownloadGridData();
            }

        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message;
        }
    }

    protected void lnk_ED_View_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        var fId = Convert.ToString(gv_EmployeeDetails.DataKeys[row.RowIndex].Values[0]).Trim();
        if (fId != null)
        {
            Response.Redirect("ViewEmployeeCV.aspx?reqid=" + fId + "");
        }
    }


    protected void gv_EmployeeDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_EmployeeDetails.PageIndex = e.NewPageIndex;
        this.BindGrid();
    }

    protected void lnk_Download_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(gv_EmployeeDetails.DataKeys[row.RowIndex].Values[0]).Trim();
            if (fId != null)
            {
                //BindEmpDetails(Convert.ToInt32(fId));
                var getDS = spm.getCVReportById(Convert.ToInt32(fId), "GetDetailsByEmpId");
                DownlodWord(getDS);
                // System.Threading.Tasks.Task.Delay();
                // Thread.Sleep(5000);
                // DownlodPDF(getDS);
            }
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    private void DownlodPDF(DataSet ds)
    {
        try
        {
            var getDS = ds;
            if (getDS != null)
            {
                StringBuilder strbuild = new StringBuilder();
                var isMarried = "";
                if (getDS.Tables.Count > 0)
                {
                    var getEmployeeDetails = getDS.Tables[0];
                    var getSNAPSHOT = getDS.Tables[1];
                    var getProjectDetails = getDS.Tables[2];
                    var getDomainDetails = getDS.Tables[3];
                    var getEduactionDetails = getDS.Tables[4];
                    var getCertificationDetails = getDS.Tables[5];
                    var getEmpName = Convert.ToString(getEmployeeDetails.Rows[0]["Emp_Name"]);
                    ReportViewer ReportViewer1 = new ReportViewer();
                    // Create Report DataSource
                    ReportDataSource rdEmployeeDetails = new ReportDataSource("dsEmployeeDetails", getEmployeeDetails);
                    ReportDataSource rdSNAPSHOT = new ReportDataSource("dsSAPSNAPSHOT", getSNAPSHOT);
                    ReportDataSource rdProjectDetails = new ReportDataSource("dsProjectDetails", getProjectDetails);
                    ReportDataSource rdDomainDetails = new ReportDataSource("dsDomainDetails", getDomainDetails);
                    ReportDataSource rdEduactionDetails = new ReportDataSource("dsEducationDetails", getEduactionDetails);
                    ReportDataSource rdCertificationDetails = new ReportDataSource("dsCertificationDetails", getCertificationDetails);

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/EmployeeCVReport.rdlc");
                    ReportViewer1.LocalReport.DataSources.Add(rdEmployeeDetails);
                    ReportViewer1.LocalReport.DataSources.Add(rdSNAPSHOT);
                    ReportViewer1.LocalReport.DataSources.Add(rdProjectDetails);
                    ReportViewer1.LocalReport.DataSources.Add(rdDomainDetails);
                    ReportViewer1.LocalReport.DataSources.Add(rdEduactionDetails);
                    ReportViewer1.LocalReport.DataSources.Add(rdCertificationDetails);
                    //ReportViewer1.LocalReport.SetParameters(param);


                    #region Create payment Voucher PDF file
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;
                    DataTable DataTable1 = new DataTable();
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                    byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                    Response.Buffer = true;
                    //Response.ClearHeaders();
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename=" + getEmpName + "_CV." + extension);
                    try
                    {
                        Response.BinaryWrite(bytes);
                        // Response.ClearHeaders();
                        // Response.Flush();
                        //Response.Clear();
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error while generating PDF.');", true);
                        Console.WriteLine(ex.StackTrace);
                    }


                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void DownlodWord(DataSet ds)
    {
        try
        {
            var getDS = ds;
            if (getDS != null)
            {
                StringBuilder strbuild = new StringBuilder();
                var isMarried = "";
                if (getDS.Tables.Count > 0)
                {
                    var getEmployeeDetails = getDS.Tables[0];
                    var getSNAPSHOT = getDS.Tables[1];
                    var getProjectDetails = getDS.Tables[2];
                    var getDomainDetails = getDS.Tables[3];
                    var getEduactionDetails = getDS.Tables[4];
                    var getCertificationDetails = getDS.Tables[5];
                    var getEmpName = Convert.ToString(getEmployeeDetails.Rows[0]["Emp_Name"]);
                    ReportViewer ReportViewer1 = new ReportViewer();
                    // Create Report DataSource
                    ReportDataSource rdEmployeeDetails = new ReportDataSource("dsEmployeeDetails", getEmployeeDetails);
                    ReportDataSource rdSNAPSHOT = new ReportDataSource("dsSAPSNAPSHOT", getSNAPSHOT);
                    ReportDataSource rdProjectDetails = new ReportDataSource("dsProjectDetails", getProjectDetails);
                    ReportDataSource rdDomainDetails = new ReportDataSource("dsDomainDetails", getDomainDetails);
                    ReportDataSource rdEduactionDetails = new ReportDataSource("dsEducationDetails", getEduactionDetails);
                    ReportDataSource rdCertificationDetails = new ReportDataSource("dsCertificationDetails", getCertificationDetails);

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/EmployeeCVReport.rdlc");
                    ReportViewer1.LocalReport.DataSources.Add(rdEmployeeDetails);
                    ReportViewer1.LocalReport.DataSources.Add(rdSNAPSHOT);
                    ReportViewer1.LocalReport.DataSources.Add(rdProjectDetails);
                    ReportViewer1.LocalReport.DataSources.Add(rdDomainDetails);
                    ReportViewer1.LocalReport.DataSources.Add(rdEduactionDetails);
                    ReportViewer1.LocalReport.DataSources.Add(rdCertificationDetails);
                    //ReportViewer1.LocalReport.SetParameters(param);


                    #region Create payment Voucher PDF file
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;
                    DataTable DataTable1 = new DataTable();
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                    byte[] bytes = ReportViewer1.LocalReport.Render("WORD", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename=" + getEmpName + "_CV." + extension);
                    try
                    {
                        Response.BinaryWrite(bytes);
                        //Response.Flush();
                        //Response.Clear();
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error while generating PDF.');", true);
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    #endregion
    #endregion

    protected void lnk_Download_PDF_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(gv_EmployeeDetails.DataKeys[row.RowIndex].Values[0]).Trim();
            if (fId != null)
            {
                //BindEmpDetails(Convert.ToInt32(fId));
                var getDS = spm.getCVReportById(Convert.ToInt32(fId), "GetDetailsByEmpId");
                // DownlodWord(getDS);
                // System.Threading.Tasks.Task.Delay();
                // Thread.Sleep(5000);
                DownlodPDF(getDS);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }


    private void DownloadGridData()
    {
        try
        {
            var ddlDepartment = "";
            var ddlDesignation = "";
            var ddlProject = "";
            var ddlmodule = "";
            var ddlQualification = "";
            var ddlDegree = "";
            var ddlCertification = "";
            var ddlCertification_Code = "";
            var ddlProjectType = "";
            var ddlIndustryType = "";
            var ddlRole = "";
            var ddlDomain = "";
            var ddlBand = "";
            var ddlUniversity = "";
            var ddlOrganisationType = "";
            var ddlOrganisationName = "";
            var ddlStream = "";
            var ddlRegion = "";
            var ddlSelect = string.Empty;
            int? year = null;
            var ddlEmployee = string.Empty;
            decimal? overAllFrom = null;
            decimal? overAllTo = null;
            //
            decimal? projectFrom = null;
            decimal? projectTo = null;
            //getddl Department
            decimal? domainFrom = null;
            decimal? domainTo = null;
            // Department
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
            //DDL Designations
            foreach (ListItem item in ddl_Designation.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlDesignation == "")
                        {
                            ddlDesignation = item.Value;
                        }
                        else
                        {
                            ddlDesignation = ddlDesignation + "|" + item.Value;
                        }
                    }
                }
            }
            //DDL ddlProject
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
            // Qualification
            foreach (ListItem item in lstQualification.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlQualification == "")
                        {
                            ddlQualification = item.Value;
                        }
                        else
                        {
                            ddlQualification = ddlQualification + "|" + item.Value;
                        }
                    }
                }
            }
            // Degree
            foreach (ListItem item in lstDegree.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlDegree == "")
                        {
                            ddlDegree = item.Value;
                        }
                        else
                        {
                            ddlDegree = ddlDegree + "|" + item.Value;
                        }
                    }
                }
            }
            //Certifiation
            foreach (ListItem item in lstCertification.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlCertification == "")
                        {
                            ddlCertification = item.Value;
                        }
                        else
                        {
                            ddlCertification = ddlCertification + "|" + item.Value;
                        }
                    }
                }
            }
            //Project Type
            var getProjectTypeText = "";
            foreach (ListItem item in lstProject.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlProjectType == "")
                        {
                            ddlProjectType = item.Value;
                        }
                        else
                        {
                            ddlProjectType = ddlProjectType + "|" + item.Value;
                        }
                        //
                        if (getProjectTypeText == "")
                        {
                            getProjectTypeText = item.Text;
                        }
                        else
                        {
                            getProjectTypeText = getProjectTypeText + "|" + item.Text;
                        }
                    }
                }
            }
            //Industry
            foreach (ListItem item in lstIndustry.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlIndustryType == "")
                        {
                            ddlIndustryType = item.Value;
                        }
                        else
                        {
                            ddlIndustryType = ddlIndustryType + "|" + item.Value;
                        }
                    }
                }
            }
            // ROle And Designation
            foreach (ListItem item in lstDesignation.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlRole == "")
                        {
                            ddlRole = item.Value;
                        }
                        else
                        {
                            ddlRole = ddlRole + "|" + item.Value;
                        }
                    }
                }
            }
            //Domain
            foreach (ListItem item in lstDomain.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlDomain == "")
                        {
                            ddlDomain = item.Value;
                        }
                        else
                        {
                            ddlDomain = ddlDomain + "|" + item.Value;
                        }
                    }
                }
            }
            //Band
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
            // University
            foreach (ListItem item in lstUniversity.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlUniversity == "")
                        {
                            ddlUniversity = item.Value;
                        }
                        else
                        {
                            ddlUniversity = ddlUniversity + "|" + item.Value;
                        }
                    }
                }
            }
            // Organisation Type
            foreach (ListItem item in lstOrganisationType.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlOrganisationType == "")
                        {
                            ddlOrganisationType = item.Value;
                        }
                        else
                        {
                            ddlOrganisationType = ddlOrganisationType + "|" + item.Value;
                        }
                    }
                }
            }
            // Organisation Name
            foreach (ListItem item in lstOrganisationName.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlOrganisationName == "")
                        {
                            ddlOrganisationName = item.Value;
                        }
                        else
                        {
                            ddlOrganisationName = ddlOrganisationName + "|" + item.Value;
                        }
                    }
                }
            }
            //ddlSelect
            if (ddl_Status.SelectedValue != "" && ddl_Status.SelectedValue != "0")
            {
                ddlSelect = ddl_Status.SelectedValue;
            }

            // Employee Code
            if (ddl_Employee.SelectedValue != "" && ddl_Employee.SelectedValue != "0")
            {
                ddlEmployee = ddl_Employee.SelectedValue;
            }
            //Year Of Passing
            if (txt_YearOfPassing.Text != "")
            {
                year = Convert.ToInt32(txt_YearOfPassing.Text);
            }
            //ddlSelect
            if (txt_From.Text.Trim() != "")
            {
                overAllFrom = decimal.Parse(txt_From.Text.Trim());
            }
            if (txt_To.Text.Trim() != "")
            {
                overAllTo = decimal.Parse(txt_To.Text.Trim());
            }
            if (txt_From_DE.Text.Trim() != "")
            {
                domainFrom = decimal.Parse(txt_From_DE.Text.Trim());
            }
            if (txt_To_DE.Text.Trim() != "")
            {
                domainTo = decimal.Parse(txt_To_DE.Text.Trim());
            }
            if (txt_From_SAP.Text.Trim() != "")
            {
                projectFrom = decimal.Parse(txt_From_SAP.Text.Trim());
            }

            if (txt_To_SAP.Text.Trim() != "")
            {
                projectTo = decimal.Parse(txt_To_SAP.Text.Trim());
            }

            // Region
            foreach (ListItem item in lst_Region.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlRegion == "")
                        {
                            ddlRegion = item.Value;
                        }
                        else
                        {
                            ddlRegion = ddlRegion + "|" + item.Value;
                        }
                    }
                }
            }
            // Stream
            foreach (ListItem item in lst_Stream.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlStream == "")
                        {
                            ddlStream = item.Value;
                        }
                        else
                        {
                            ddlStream = ddlStream + "|" + item.Value;
                        }
                    }
                }
            }
            //Certifiation Code
            foreach (ListItem item in lstCertification_Code.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        if (ddlCertification_Code == "")
                        {
                            ddlCertification_Code = item.Value;
                        }
                        else
                        {
                            ddlCertification_Code = ddlCertification_Code + "|" + item.Value;
                        }
                    }
                }
            }

            var IsCompletedCertification = chk_Completed_Certification.Checked;
            var getDS = spm.getCVDetailInbox(0, "GetInboxDetails", ddlEmployee, ddlDepartment, ddlDesignation, ddlProject, ddlmodule, overAllFrom, overAllTo, ddlSelect, ddlQualification, ddlDegree, ddlCertification, ddlProjectType, ddlIndustryType, ddlRole, ddlDomain, ddlBand, ddlUniversity, ddlOrganisationType, ddlOrganisationName, year, projectFrom, projectTo, domainFrom, domainTo, ddlRegion, ddlStream, IsCompletedCertification,ddlCertification_Code);
            if (getDS != null)
            {
                if (getDS.Tables.Count > 0)
                {

                    var getDetails = getDS.Tables[0];

                    if (getDetails.Rows.Count > 0)
                    {
                        var IsUpdated = false;
                        var status = "Compliant";
                        foreach (DataRow item1 in getDetails.Rows)
                        {
                            bool IsDepartment = true, IsBand = true, IsModule = true, IsDesignation = true, IsEmployee = true, IsProject = true, IsQualification = true;
                            bool IsUniversity = true, IsDegree = true, IsProjectType = true, IsCertification = true, IsRole = true, IsDomain = true, IsOrganisationName = true;
                            bool IsOrganisationType = true, IsIndustryType = true, Isyear = true, IsoverAllFrom = true, IsdomainFrom = true, IsprojectFrom = true, IsStream = true, IsRegion = true;
                            bool IsCertified = true, IsCertification_Code=true;
                            var getEmpId = Convert.ToString(item1["Emp_Code"]);
                            var getDeptId = Convert.ToString(item1["DeptId"]);
                            var getDesigId = Convert.ToString(item1["DesigId"]);
                            var getgrade = Convert.ToString(item1["grade"]);
                            var getEmpLocation = Convert.ToString(item1["EmpLocation"]);
                            var getTotalSAPExperience = Convert.ToString(item1["TotalSAPExperience"]);
                            var getOverallWorkExperience = Convert.ToString(item1["OverallWorkExperience"]);
                            var getTotalDomainExperience = Convert.ToString(item1["TotalDomainExperience"]);
                            if (ddlDepartment != "")
                            {
                                var splitVal = ddlDepartment.Split('|');
                                if (splitVal.Contains(getDeptId))
                                {
                                    IsDepartment = true;
                                }
                                else
                                {
                                    IsDepartment = false;
                                }
                            }
                            if (ddlBand != "")
                            {
                                var splitVal = ddlBand.Split('|');
                                if (splitVal.Contains(getgrade))
                                {
                                    IsBand = true;
                                }
                                else
                                {
                                    IsBand = false;
                                }
                            }
                            if (ddlmodule != "")
                            {
                                var splitVal = ddlmodule.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckModuleExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsModule = true;
                                        }
                                        else
                                        {
                                            IsModule = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsModule = false;
                                    }
                                }
                            }
                            if (ddlDesignation != "")
                            {
                                var splitVal = ddlDesignation.Split('|');
                                if (splitVal.Contains(getDesigId))
                                {
                                    IsDesignation = true;
                                }
                                else
                                {
                                    IsDesignation = false;
                                }
                            }
                            if (ddlEmployee != "")
                            {
                                var splitVal = ddlEmployee;
                                if (splitVal.Contains(getEmpId))
                                {
                                    IsEmployee = true;
                                }
                                else
                                {
                                    IsEmployee = false;
                                }
                            }
                            if (ddlProject != "")
                            {
                                var splitVal = ddlProject.Split('|');
                                if (splitVal.Contains(getEmpLocation))
                                {
                                    IsProject = true;
                                }
                                else
                                {
                                    IsProject = false;
                                }
                            }
                            if (ddlQualification != "")
                            {
                                var splitVal = ddlQualification.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckQualificationExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsQualification = true;
                                        }
                                        else
                                        {
                                            IsQualification = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsQualification = false;
                                    }
                                }
                            }
                            if (ddlUniversity != "")
                            {
                                var splitVal = ddlUniversity.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckBoardExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsUniversity = true;
                                        }
                                        else
                                        {
                                            IsUniversity = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsUniversity = false;
                                    }
                                }
                            }
                            if (ddlDegree != "")
                            {
                                var splitVal = ddlDegree.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckDegreeExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsDegree = true;
                                        }
                                        else
                                        {
                                            IsDegree = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsDegree = false;
                                    }
                                }
                            }
                            if (ddlProjectType != "")
                            {
                                var splitVal = ddlProjectType.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckProjectTypeExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsProjectType = true;
                                        }
                                        else
                                        {
                                            IsProjectType = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsProjectType = false;
                                    }
                                }
                            }
                            if (ddlCertification != "")
                            {
                                var splitVal = ddlCertification.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckCertificationExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsCertification = true;
                                        }
                                        else
                                        {
                                            IsCertification = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsCertification = false;
                                    }
                                }
                            }
                            if (ddlRole != "")
                            {
                                var splitVal = ddlRole.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckRoleExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsRole = true;
                                        }
                                        else
                                        {
                                            IsRole = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsRole = false;
                                    }
                                }
                            }
                            if (ddlDomain != "")
                            {
                                var splitVal = ddlDomain.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckDomainExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsDomain = true;
                                        }
                                        else
                                        {
                                            IsDomain = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsDomain = false;
                                    }
                                }
                            }
                            if (ddlOrganisationName != "")
                            {
                                var splitVal = ddlOrganisationName.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckOrganisationNameExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsOrganisationName = true;
                                        }
                                        else
                                        {
                                            IsOrganisationName = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsOrganisationName = false;
                                    }
                                }
                            }
                            if (ddlOrganisationType != "")
                            {
                                var splitVal = ddlOrganisationType.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckOrganisationTypeExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsOrganisationName = true;
                                        }
                                        else
                                        {
                                            IsOrganisationName = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsOrganisationName = false;
                                    }
                                }
                            }
                            if (ddlIndustryType != "")
                            {
                                var splitVal = ddlIndustryType.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckIndustryTypeExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsIndustryType = true;
                                        }
                                        else
                                        {
                                            IsIndustryType = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsIndustryType = false;
                                    }
                                }
                            }
                            if (year != null)
                            {
                                var getStatus = spm.CheckIsIdsExists("CheckYearExists", getEmpId, Convert.ToInt32(year));
                                if (getStatus.Rows.Count > 0)
                                {
                                    var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                    if (getMsg == "EXISTS")
                                    {
                                        Isyear = true;
                                    }
                                    else
                                    {
                                        Isyear = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    Isyear = false;
                                }
                            }
                            if (overAllFrom != null)
                            {
                                if (overAllFrom >= Convert.ToDecimal(getOverallWorkExperience) && overAllTo <= Convert.ToDecimal(getOverallWorkExperience))
                                {
                                    IsoverAllFrom = true;
                                }
                                else
                                {
                                    IsoverAllFrom = false;
                                }
                            }
                            if (domainFrom != null)
                            {
                                if (domainFrom >= Convert.ToDecimal(getTotalDomainExperience) && domainTo <= Convert.ToDecimal(getTotalDomainExperience))
                                {
                                    IsdomainFrom = true;
                                }
                                else
                                {
                                    IsdomainFrom = false;
                                }
                            }
                            if (projectFrom != null)
                            {
                                if (projectFrom >= Convert.ToDecimal(getTotalSAPExperience) && projectTo <= Convert.ToDecimal(getTotalSAPExperience))
                                {
                                    IsprojectFrom = true;
                                }
                                else
                                {
                                    IsprojectFrom = false;
                                }
                            }

                            if (ddlRegion != "")
                            {
                                var splitVal = ddlRegion.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckRegionExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsRegion = true;
                                        }
                                        else
                                        {
                                            IsRegion = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsRegion = false;
                                    }
                                }
                            }
                            if (ddlStream != "")
                            {
                                var splitVal = ddlStream.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsIdsExists("CheckStreamExists", getEmpId, Convert.ToInt32(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsStream = true;
                                        }
                                        else
                                        {
                                            IsStream = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsStream = false;
                                    }
                                }
                            }
                            // Certification Code
                            if (ddlCertification_Code != "")
                            {
                                var splitVal = ddlCertification_Code.Split('|');
                                foreach (var item in splitVal)
                                {
                                    var getStatus = spm.CheckIsCertificationCodeExists("CheckCertificationCodeExists", getEmpId, Convert.ToString(item));
                                    if (getStatus.Rows.Count > 0)
                                    {
                                        var getMsg = getStatus.Rows[0]["MESSAGE"].ToString();
                                        if (getMsg == "EXISTS")
                                        {
                                            IsCertification_Code = true;
                                        }
                                        else
                                        {
                                            IsCertification_Code = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        IsCertification_Code = false;
                                    }
                                }
                            }
                            if (IsDepartment == true && IsBand == true && IsModule == true && IsDesignation == true && IsEmployee == true && IsProject == true && IsQualification == true && IsUniversity == true && IsDegree == true && IsProjectType == true && IsCertification == true && IsRole == true && IsDomain == true && IsOrganisationName == true && IsOrganisationType == true && IsIndustryType == true && Isyear == true && IsoverAllFrom == true && IsdomainFrom == true && IsprojectFrom == true && IsStream == true && IsRegion == true && IsCertification_Code==true)
                            {
                                item1["Criteria"] = "Compliant";
                            }
                        }

                        if (getDetails.Rows.Count > 0)
                        {
                            var newTable = new DataTable();
                            newTable.Columns.Add("Employee Code");
                            newTable.Columns.Add("Employee Name");
                            newTable.Columns.Add("Department");
                            newTable.Columns.Add("Designation");
                            newTable.Columns.Add("Band");
                            newTable.Columns.Add("Project");
                            newTable.Columns.Add("Highest Qualification");
                            newTable.Columns.Add("Primary Module");
                            newTable.Columns.Add("Secondary Module");
                            newTable.Columns.Add("Date Of Joining");
                            newTable.Columns.Add("Total SAP Experience");
                            newTable.Columns.Add("Total Domain Experience");
                            newTable.Columns.Add("Over all Work Experience");
                            newTable.Columns.Add("Full Life Cycle Implementation");
                            newTable.Columns.Add("Rollout");
                            newTable.Columns.Add("Migration");
                            newTable.Columns.Add("Support");
                            newTable.Columns.Add("Enhancements");
                            newTable.Columns.Add("Others");
                            newTable.Columns.Add("Criteria");
			    newTable.Columns.Add("Is Certification Completed");
                            newTable.Columns.Add("Certification Name");
                            foreach (DataRow item in getDetails.Rows)
                            {
                                DataRow _dr = newTable.NewRow();
                                _dr["Employee Code"] = item["Emp_Code"].ToString();
                                _dr["Employee Name"] = item["Emp_Name"].ToString();
                                _dr["Department"] = item["Department"].ToString();
                                _dr["Designation"] = item["Designation"].ToString();
                                _dr["Band"] = item["grade"].ToString();
                                _dr["Project"] = item["emp_projectName"].ToString();
                                _dr["Highest Qualification"] = item["HighestQualification"].ToString();
                                _dr["Primary Module"] = item["Module1"].ToString();//Primary Module/ Secondry
                                _dr["Secondary Module"] = item["SecondaryModule"].ToString();//Primary Module/ Secondry
                                _dr["Date Of Joining"] = item["DOJ"].ToString();
                                _dr["Total SAP Experience"] = item["TotalSAPExperience"].ToString();
                                _dr["Total Domain Experience"] = item["TotalDomainExperience"].ToString();
                                _dr["Over all Work Experience"] = item["OverallWorkExperience"].ToString();
                                _dr["Full Life Cycle Implementation"] = item["FLCI"].ToString();
                                _dr["Rollout"] = item["Rollout"].ToString();
                                _dr["Migration"] = item["Migration"].ToString();
                                _dr["Support"] = item["Support"].ToString();
                                _dr["Enhancements"] = item["Enhancements"].ToString();
                                _dr["Others"] = item["Others"].ToString();
                                _dr["Criteria"] = item["Criteria"].ToString();
                                _dr["Is Certification Completed"] = item["IsCertification"].ToString();
                                _dr["Certification Name"] = item["CertificationName"].ToString();
                                newTable.Rows.Add(_dr);
                            }
                            var dateTime = DateTime.Now.ToString("ddMMyyyyHHmmss");
                            var excelName = "Employee CV Automation_" + dateTime;
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(newTable, "Employee List");
                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=" + excelName + ".xlsx");
                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }

                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message;
        }
    }


    protected void rbWildSearch_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbWildSearch.Checked == true)
            {
                txt_Wild_Search.Enabled = true;

                show1.Visible = false;
                show2.Visible = false;
                show3.Visible = false;
                show4.Visible = false;
                show5.Visible = false;
                show6.Visible = false;
                show7.Visible = false;
                showCHK1.Visible = false;
                show8.Visible = false;
                show9.Visible = false;
                show10.Visible = false;
                show11.Visible = false;
                show12.Visible = false;
                show13.Visible = false;
                show13_1.Visible = false;
                show14.Visible = false;
                show15.Visible = false;
                show16.Visible = false;
                show17.Visible = false;
                show18.Visible = false;
                show19.Visible = false;
                show20.Visible = false;
                show21.Visible = false;
                show22.Visible = false;
                show23.Visible = false;
                show24.Visible = false;
                show25.Visible = false;
                show26.Visible = false;
                show27.Visible = false;
                show28.Visible = false;
                show29.Visible = false;
                show30.Visible = false;
                show31.Visible = false;
                show32.Visible = false;
                show33.Visible = false;
            }
            else if (rbNormalSearch.Checked == true)
            {
                txt_Wild_Search.Enabled = false;
                show1.Visible = true;
                show2.Visible = true;
                show3.Visible = true;
                show4.Visible = true;
                show5.Visible = true;
                show6.Visible = true;
                show7.Visible = true;
                showCHK1.Visible = true;
                show8.Visible = true;
                show9.Visible = true;
                show10.Visible = true;
                show11.Visible = true;
                show12.Visible = true;
                show13.Visible = true;
                show13_1.Visible = true;
                show14.Visible = true;
                show15.Visible = true;
                show16.Visible = true;
                show17.Visible = true;
                show18.Visible = true;
                show19.Visible = true;
                show20.Visible = true;
                show21.Visible = true;
                show22.Visible = true;
                show23.Visible = true;
                show24.Visible = true;
                show25.Visible = true;
                show26.Visible = true;
                show27.Visible = true;
                show28.Visible = true;
                show29.Visible = true;
                show30.Visible = true;
                show31.Visible = true;
                show32.Visible = true;
                show33.Visible = true;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void WildSearch()
    {
        try
        {
            var getSearch = txt_Wild_Search.Text.Trim();
            if (getSearch != "")
            {
                var result = spm.getCVDetailInbox(0, "GetInboxDetailsWildSearch", "", "", "", "", "", null, null, "", "", "", "", "", "", "", "", "", "", "", "", null, null, null, null, null, "", getSearch,false,"");
                if (result != null)
                {
                    gv_EmployeeDetails.DataSource = null;
                    gv_EmployeeDetails.DataBind();
                    if (result.Tables.Count > 0)
                    {
                        var getDetails = result.Tables[0];
                        if (getDetails != null)
                        {
                            if (getDetails.Rows.Count > 0)
                            {
                                gv_EmployeeDetails.DataSource = getDetails;
                                gv_EmployeeDetails.DataBind();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void WildSearchDownload()
    {
        try
        {
            var getSearch = txt_Wild_Search.Text.Trim();
            if (getSearch != "")
            {
                var result = spm.getCVDetailInbox(0, "GetInboxDetailsWildSearch", "", "", "", "", "", null, null, "", "", "", "", "", "", "", "", "", "", "", "", null, null, null, null, null, "", getSearch,false,"");
                if (result != null)
                {
                    if (result.Tables.Count > 0)
                    {
                        var getDetails = result.Tables[0];
                        if (getDetails != null)
                        {
                            if (getDetails.Rows.Count > 0)
                            {
                                var newTable = new DataTable();
                                newTable.Columns.Add("Employee Code");
                                newTable.Columns.Add("Employee Name");
                                newTable.Columns.Add("Department");
                                newTable.Columns.Add("Designation");
                                newTable.Columns.Add("Band");
                                newTable.Columns.Add("Project");
                                newTable.Columns.Add("Highest Qualification");
                                newTable.Columns.Add("Primary Module");
                                newTable.Columns.Add("Secondary Module");
                                newTable.Columns.Add("Date Of Joining");
                                newTable.Columns.Add("Total SAP Experience");
                                newTable.Columns.Add("Total Domain Experience");
                                newTable.Columns.Add("Over all Work Experience");
                                newTable.Columns.Add("Full Life Cycle Implementation");
                                newTable.Columns.Add("Rollout");
                                newTable.Columns.Add("Migration");
                                newTable.Columns.Add("Support");
                                newTable.Columns.Add("Enhancements");
                                newTable.Columns.Add("Others");
                                newTable.Columns.Add("Criteria");
				newTable.Columns.Add("Is Certification Completed");
                                newTable.Columns.Add("Certification Name");
                                foreach (DataRow item in getDetails.Rows)
                                {
                                    DataRow _dr = newTable.NewRow();
                                    _dr["Employee Code"] = item["Emp_Code"].ToString();
                                    _dr["Employee Name"] = item["Emp_Name"].ToString();
                                    _dr["Department"] = item["Department"].ToString();
                                    _dr["Designation"] = item["Designation"].ToString();
                                    _dr["Band"] = item["grade"].ToString();
                                    _dr["Project"] = item["emp_projectName"].ToString();
                                    _dr["Highest Qualification"] = item["HighestQualification"].ToString();
                                    _dr["Primary Module"] = item["Module1"].ToString();//Primary Module/ Secondry
                                    _dr["Secondary Module"] = item["SecondaryModule"].ToString();//Primary Module/ Secondry
                                    _dr["Date Of Joining"] = item["DOJ"].ToString();
                                    _dr["Total SAP Experience"] = item["TotalSAPExperience"].ToString();
                                    _dr["Total Domain Experience"] = item["TotalDomainExperience"].ToString();
                                    _dr["Over all Work Experience"] = item["OverallWorkExperience"].ToString();
                                    _dr["Full Life Cycle Implementation"] = item["FLCI"].ToString();
                                    _dr["Rollout"] = item["Rollout"].ToString();
                                    _dr["Migration"] = item["Migration"].ToString();
                                    _dr["Support"] = item["Support"].ToString();
                                    _dr["Enhancements"] = item["Enhancements"].ToString();
                                    _dr["Others"] = item["Others"].ToString();
                                    _dr["Criteria"] = item["Criteria"].ToString();
				    _dr["Is Certification Completed"] = item["IsCertification"].ToString();
                                    _dr["Certification Name"] = item["CertificationName"].ToString();
                                    newTable.Rows.Add(_dr);
                                }
                                var dateTime = DateTime.Now.ToString("ddMMyyyyHHmmss");
                                var excelName = "Employee CV Automation_" + dateTime;
                                using (XLWorkbook wb = new XLWorkbook())
                                {
                                    wb.Worksheets.Add(newTable, "Employee List");
                                    Response.Clear();
                                    Response.Buffer = true;
                                    Response.Charset = "";
                                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                    Response.AddHeader("content-disposition", "attachment;filename=" + excelName + ".xlsx");
                                    using (MemoryStream MyMemoryStream = new MemoryStream())
                                    {
                                        wb.SaveAs(MyMemoryStream);
                                        MyMemoryStream.WriteTo(Response.OutputStream);
                                        Response.Flush();
                                        Response.End();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
}