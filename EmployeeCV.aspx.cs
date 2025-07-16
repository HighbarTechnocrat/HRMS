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

public partial class EmployeeCV : System.Web.UI.Page
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
                    txtJobDescription.Attributes.Add("maxlength", "500");
                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["EmployeeCVDCFilePath"]).Trim());
                    //TXT_AssginmentDate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                    editform.Visible = true;
                    divbtn.Visible = false;
                    divmsg.Visible = false;
                    hdnempcode.Value = Convert.ToString(Session["Empcode"]);

                    txt_FD_DOB.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txt_YearOfPassing.Attributes.Add("onkeypress", "return noanyCharecters(event);");

                    txt_CD_FromDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txt_CD_ToDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");

                    txt_PD_FromDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txt_PD_ToDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");

                    txt_DD_FromDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txt_DD_ToDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");

                    //txt_FD_ContectNo.Attributes.Add("onkeypress", "return onCharOnlyNumber_Mobile(event);");

                    txt_TotalDomainExp.Attributes.Add("onkeypress", "return onCharOnlyNumber_EXP(event);");
                    txt_Sabbatical_Educational_Break.Attributes.Add("onkeypress", "return onCharOnlyNumber_EXP(event);");
                    txt_TotalDomainExp.Attributes.Add("onkeyup", "return sumOfExp(event);");
                    txt_TotalSAPExp.Attributes.Add("onkeypress", "return onCharOnlyNumber_EXP(event);");
                    txt_TotalSAPExp.Attributes.Add("onkeyup", "return sumOfExp(event);");

                    txt_TotalOverallExp.Attributes.Add("onkeypress", "return noanyCharecters(event);");

                    //  txt_TotalOverallExp.Attributes.Add("onkeypress", "return onCharOnlyNumber_EXP(event);");
                    txt_GradeMarks.Attributes.Add("onkeypress", "return onCharOnlyNumber_EXP(event);");
                    txt_TotalMark.Attributes.Add("onkeypress", "return onCharOnlyNumber_EXP(event);");
                    // chk_IsActive.Attributes.Add("onkeypress", "return onCharOnlyNumber_EXP(event);");
                    txt_GradeMarks.Enabled = false;
                    txt_YearOfPassing.Enabled = false;
                    txt_TotalMark.Enabled = false;
                    txt_CD_FromDate.Enabled = false;
                    txt_CD_ToDate.Enabled = false;
                    txt_CD_CertificationNo.Enabled = false;
                    BindEmpDetails(Convert.ToString(Session["Empcode"]), "All");
                   
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                    hdnFamilyDetailID.Value = "0";

                    txt_P_Date_Issue.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txt_P_Date_Expiry.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                }
                else
                {
                    //Set Post back
                    var isMarried = hdnIsMarried.Value.ToString();
                    foreach (ListItem itm in ddl_Relation.Items)
                    {
                        if (itm.Value == "1" || itm.Value == "2")
                        {
                            itm.Attributes.Add("disabled", "disabled");
                        }

                        if (isMarried == "Married")
                        {
                            //rowCount = 3;
                            if (itm.Value == "3")
                                itm.Attributes.Add("disabled", "disabled");
                        }
                    }
                    //
                    //DataTable dtEduactionDetails = new DataTable();
                    //dtEduactionDetails = gv_EducationDetails.DataSource as DataTable;
                    //DataTable dt = (DataTable)gv_EducationDetails.DataSource;
                    //if (dtEduactionDetails.Rows.Count > 0)
                    //{
                    //    foreach (ListItem itm in ddl_Qualification.Items)
                    //    {
                    //        if (dtEduactionDetails.AsEnumerable().Any(row => row.Field<int>("QualificationId") == int.Parse(itm.Value.ToString())))
                    //        {
                    //            var getId = int.Parse(itm.Value.ToString());
                    //            if (getId == 1 || getId == 2 || getId == 3)
                    //            {
                    //                itm.Attributes.Add("disabled", "disabled");
                    //            }
                    //        }
                    //    }
                    //}
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
                    //Bind Employee Details
                    var getEmpDetailsTable = getDS.Tables[0];
                    if (getEmpDetailsTable.Rows.Count > 0)
                    {
                        txtEmpName.Text = getEmpDetailsTable.Rows[0]["Emp_Name"].ToString();
                        Txt_ProjectCode.Text = getEmpDetailsTable.Rows[0]["emp_projectName"].ToString();
                        txtEmpCode.Text = getEmpDetailsTable.Rows[0]["Emp_Code"].ToString();
                        Txt_Band.Text = getEmpDetailsTable.Rows[0]["grade"].ToString();
                        Txt_Department.Text = getEmpDetailsTable.Rows[0]["Department"].ToString();
                        txt_Module1.Text = getEmpDetailsTable.Rows[0]["Module1"].ToString();
                        txt_Designation.Text = getEmpDetailsTable.Rows[0]["Designation"].ToString();
                        txt_Module2.Text = getEmpDetailsTable.Rows[0]["Module2"].ToString();
                        Txt_DOB.Text = getEmpDetailsTable.Rows[0]["EMPDOB"].ToString();
                        txt_Module3.Text = getEmpDetailsTable.Rows[0]["Module3"].ToString();
                        txt_RM.Text = getEmpDetailsTable.Rows[0]["ReportingManager"].ToString();
                        txtOtherModule.Text = getEmpDetailsTable.Rows[0]["OTHER_MODULES"].ToString();
                        txt_PM.Text = getEmpDetailsTable.Rows[0]["ProgramManager"].ToString();
                        txt_DOJ.Text = getEmpDetailsTable.Rows[0]["DOJ"].ToString();
                        txt_DM.Text = getEmpDetailsTable.Rows[0]["DeliveryManager"].ToString();
                        txt_EmailAddress.Text = getEmpDetailsTable.Rows[0]["Emp_Emailaddress"].ToString();
                        txt_HOD.Text = getEmpDetailsTable.Rows[0]["HOD"].ToString();
                        txt_MobileNumber.Text = getEmpDetailsTable.Rows[0]["mobile"].ToString();
                        txt_passportNo.Text = getEmpDetailsTable.Rows[0]["PASSPORT"].ToString();
                        txt_Passport_Place_Issue.Text = getEmpDetailsTable.Rows[0]["PASS_PLACEOFISSUE"].ToString();
                        txt_P_EmailAddress.Text = getEmpDetailsTable.Rows[0]["PERSONAL_EMAIL"].ToString();
                        txt_P_Date_Issue.Text = getEmpDetailsTable.Rows[0]["PASS_DATEOFISSUE"].ToString();
                        txt_BloodGroup.Text = getEmpDetailsTable.Rows[0]["BLOOD_GRP"].ToString();
                        txt_P_Date_Expiry.Text = getEmpDetailsTable.Rows[0]["PASS_EXPIRYDATE"].ToString();
                        txt_MaritalStatus.Text = getEmpDetailsTable.Rows[0]["ISMARRIED"].ToString();
                        isMarried = getEmpDetailsTable.Rows[0]["ISMARRIED"].ToString();
                        hdnIsMarried.Value = getEmpDetailsTable.Rows[0]["ISMARRIED"].ToString();
                        txt_P_Address.Text = getEmpDetailsTable.Rows[0]["P_ADD"].ToString();
                        txt_ECP_Name.Text = getEmpDetailsTable.Rows[0]["CONTACT_PER_1_NAME"].ToString();
                        txt_PAN.Text = getEmpDetailsTable.Rows[0]["PAN"].ToString();
                        txt_ECP_Number.Text = getEmpDetailsTable.Rows[0]["CONTACT_PER_1_NO"].ToString();
                        txt_BankName.Text = getEmpDetailsTable.Rows[0]["BANK_NAME"].ToString();
                        txt_C_Address.Text = getEmpDetailsTable.Rows[0]["C_ADD"].ToString();
                        txt_BankACCNo.Text = getEmpDetailsTable.Rows[0]["BANK_AC"].ToString();
                        txt_AdharNo.Text = getEmpDetailsTable.Rows[0]["AADHAR"].ToString();
                        txt_IFSCCode.Text = getEmpDetailsTable.Rows[0]["IFSC_CODE"].ToString();
                        txt_Gender.Text = getEmpDetailsTable.Rows[0]["Gender"].ToString();
                        txt_EPF.Text = getEmpDetailsTable.Rows[0]["EPFO_NO"].ToString();
                        txt_UAN.Text = getEmpDetailsTable.Rows[0]["UANNO"].ToString();
                        txt_Name_As_Aadhar.Text = getEmpDetailsTable.Rows[0]["Name_As_Per_Aadhaar"].ToString();
                        txt_Name_As_PAN.Text = getEmpDetailsTable.Rows[0]["Name_As_Per_PAN"].ToString();
                        txt_Name_As_Passport.Text = getEmpDetailsTable.Rows[0]["Name_As_Per_Passport"].ToString();

                        if(Convert.ToString(getEmpDetailsTable.Rows[0]["IsCertification"]).Trim()!="")
                        {
                            chk_Completed_Certification.Checked = Convert.ToBoolean(getEmpDetailsTable.Rows[0]["IsCertification"]);
                        }

                        var getTotalDomainExperience = getEmpDetailsTable.Rows[0]["TotalDomainExperience"].ToString();
                        var getTotalSAPExperience = getEmpDetailsTable.Rows[0]["TotalSAPExperience"].ToString();                       
                        var getOverallWorkExperience = getEmpDetailsTable.Rows[0]["OverallWorkExperience"].ToString();
                        var getEducationalBreak = getEmpDetailsTable.Rows[0]["EducationalBreak"].ToString();

                        txt_TotalDomainExp.Text = getTotalDomainExperience;
                        txt_TotalSAPExp.Text = getTotalSAPExperience;
                        txt_TotalOverallExp.Text = getOverallWorkExperience.ToString();
                        txt_Sabbatical_Educational_Break.Text = getEducationalBreak.ToString();
                        txt_EducationalB.Text = getEducationalBreak.ToString();

                        lnk_DE_Save.Visible = false;
                        lnk_DE_Update.Visible = false;
                        var getProjectDetails = getDS.Tables[10];
                        var getDomainDetails = getDS.Tables[12];
                        var projectExp = "";
                        var domainExp = "";
                        //if(getProjectDetails!=null)
                        //{
                        //    if(getProjectDetails.Rows.Count>0)
                        //    {
                        //        var getm1 = 0;
                        //        var getm2 = 0;
                        //        foreach (DataRow item in getProjectDetails.Rows)
                        //        {
                        //            var getExp = "";
                        //            var getStatus = Convert.ToBoolean(item["IsCurrentProject"].ToString());
                        //            //if(getStatus==false)
                        //            //{
                        //            getExp = Convert.ToString(item["TotalProjectExp"]);
                        //            if (getExp != "")
                        //            {
                        //                var split = getExp.Split('.');
                        //                getm1 += Convert.ToInt32(split[0]);
                        //                getm2 += Convert.ToInt32(split[1]);
                        //            }

                        //            //}
                        //        }
                        //        if (getm2 >= 12)
                        //        {
                        //            getm1 = getm1 + 1;
                        //            getm2 = getm2 - 12;
                        //        }
                        //        projectExp = getm1 + "." + getm2;
                        //    }
                        //}
                        //if (getDomainDetails != null)
                        //{
                        //    if (getDomainDetails.Rows.Count > 0)
                        //    {
                        //        var getm1 = 0;
                        //        var getm2 = 0;
                        //        foreach (DataRow item in getDomainDetails.Rows)
                        //        {
                        //            var getExp = "";
                        //            var getStatus = Convert.ToBoolean(item["IsPreviousExp"].ToString());
                        //            //if (getStatus == false)
                        //            //{
                        //            getExp = Convert.ToString(item["TotalDomainExp"]);
                        //            if (getExp != "")
                        //            {
                        //                var split = getExp.Split('.');
                        //                getm1 += Convert.ToInt32(split[0]);
                        //                getm2 += Convert.ToInt32(split[1]);
                        //            }
                        //            //}
                        //        }
                        //        if (getm2 >= 12)
                        //        {
                        //            getm1 = getm1 + 1;
                        //            getm2 = getm2 - 12;
                        //        }
                        //        domainExp = getm1 + "." + getm2;
                        //    }
                        //}
                        //if (projectExp != "" || domainExp != "")
                        //{
                        //    var Total = "";
                        //    if (projectExp != "" && domainExp != "")
                        //    {
                        //        var getsplitD = domainExp.Split('.');
                        //        var getsplitSAP = projectExp.Split('.');
                        //        var total1 = Convert.ToInt32(getsplitD[1]) + Convert.ToInt32(getsplitSAP[1]);
                        //        var total0 = Convert.ToInt32(getsplitD[0]) + Convert.ToInt32(getsplitSAP[0]);
                        //        if (total1 >= 12)
                        //        {
                        //            total0 = total0 + 1;
                        //            total1 = total1 - 12;
                        //        }

                        //        var tempDExp = Convert.ToDecimal(getTotalDomainExperience);
                        //        var tempSAPExp = Convert.ToDecimal(getTotalSAPExperience);
                        //        Total = total0 + "." + total1;
                        //    }
                        //    else
                        //    {
                        //        if (projectExp != "")
                        //            Total = projectExp;
                        //        if (domainExp != "")
                        //            Total = domainExp;
                        //    }
                            
                        //    txt_TotalDomainExp.Text = domainExp;
                        //    txt_TotalSAPExp.Text = projectExp;
                        //    txt_TotalOverallExp.Text = Total.ToString();
                            
                        //    lnk_DE_Save.Visible = false;
                        //    lnk_DE_Update.Visible = false;
                        //}
                        txtJobDescription.Text = Convert.ToString(getEmpDetailsTable.Rows[0]["ProfileSummary"]);
                        SummaryofExperience();
                    }
                    
                    //End Bind Employee Details                                       
                    if (type == "All")
                    {
                        //Call Family
                        var getFamilyDetailsDT = getDS.Tables[1];
                        var getRelationDT = getDS.Tables[2];
                        BindFamilyDetails(getFamilyDetailsDT, getRelationDT, isMarried);
                        //Call Eduaction
                        var getEduactionDetails = getDS.Tables[3];
                        var getEducation_Type = getDS.Tables[4];
                        var getBoards = getDS.Tables[5];
                        var getDegree = getDS.Tables[6];
                        var getStream = getDS.Tables[16];
                        BindEduactionDetails(getEduactionDetails, getEducation_Type, getBoards, getDegree, getStream);
                        //Certification
                        var getCertificationDetails = getDS.Tables[7];
                        var getCertification = getDS.Tables[8];
                        var getModule = getDS.Tables[9];
                        BindCertificationDetails(getCertificationDetails, getCertification, getModule);
                        //Project
                        var getProjectDetails = getDS.Tables[10];
                        var getProjectType = getDS.Tables[11];
                        var getInsustryType = getDS.Tables[14];
                        var getRoleType = getDS.Tables[13];
                        var getOrgName = getDS.Tables[17];
                        var getOrgType = getDS.Tables[18];
                        var getRegionDetails = getDS.Tables[21];
                        BindProjectTypeDDL(getProjectType);
                        BindIndustryTypeDDL(getInsustryType);
                        BindRolePDTypeDDL(getRoleType);
                        BindModulePDTypeDDL(getModule);
                        BindPDOrgNameDDL(getOrgName);
                        BindPDOrgTypeDDL(getOrgType);
                        BindPDRegionDDL(getRegionDetails);
                        BindProjectDetailsGRID(getProjectDetails);
                        //Domain
                        var getDomainDetails = getDS.Tables[12];
                        var getInsustryTypeDD = getDS.Tables[14];
                        var getRoleDD = getDS.Tables[13];
                        var getDomainDD = getDS.Tables[15];
                        BindIndustryTypeDDDDL(getInsustryTypeDD);
                        BindDomainDDDDL(getDomainDD);
                        BindRoleDDDDL(getRoleDD);
                        BindDDOrgNameDDL(getOrgName);
                        BindDDOrgTypeDDL(getOrgType);
                        BindDomainDetailsGRID(getDomainDetails);
                        //Document
                        var getDocumentType = getDS.Tables[19];
                        var getDocumentDetails = getDS.Tables[20];
                        BindDocument(getDocumentType, getDocumentDetails);
                    }
                    //Bind Family Details 
                    if (type == "Family")
                    {
                        var getFamilyDetailsDT = getDS.Tables[1];
                        var getRelationDT = getDS.Tables[2];
                        BindFamilyDetails(getFamilyDetailsDT, getRelationDT, isMarried);
                    }
                    //End family Details
                    //Education Details
                    if (type == "Education")
                    {
                        var getEduactionDetails = getDS.Tables[3];
                        var getEducation_Type = getDS.Tables[4];
                        var getBoards = getDS.Tables[5];
                        var getDegree = getDS.Tables[6];
                        var getStream = getDS.Tables[16];
                        BindEduactionDetails(getEduactionDetails, getEducation_Type, getBoards, getDegree, getStream);
                    }
                    //End Eduaction Details
                    //Certification Details
                    if (type == "Certification")
                    {
                        var getCertificationDetails = getDS.Tables[7];
                        var getCertification = getDS.Tables[8];
                        var getModule = getDS.Tables[9];
                        BindCertificationDetails(getCertificationDetails, getCertification, getModule);
                    }
                    //End Eduaction Details
                    if (type == "Project")
                    {
                        var getModule = getDS.Tables[9];
                        var getProjectDetails = getDS.Tables[10];
                        var getProjectType = getDS.Tables[11];
                        var getInsustryType = getDS.Tables[14];
                        var getRoleType = getDS.Tables[13];
                        var getOrgName = getDS.Tables[17];
                        var getOrgType = getDS.Tables[18];
                        var getRegionDetails = getDS.Tables[21];
                        BindProjectTypeDDL(getProjectType);
                        BindIndustryTypeDDL(getInsustryType);
                        BindRolePDTypeDDL(getRoleType);
                        BindModulePDTypeDDL(getModule);
                        BindPDOrgNameDDL(getOrgName);
                        BindPDOrgTypeDDL(getOrgType);
                        BindPDRegionDDL(getRegionDetails);
                        BindProjectDetailsGRID(getProjectDetails);
                    }
                    if (type == "Domain")
                    {
                        var getDomainDetails = getDS.Tables[12];
                        var getInsustryTypeDD = getDS.Tables[14];
                        var getRoleDD = getDS.Tables[13];
                        var getDomainDD = getDS.Tables[15];
                        var getOrgName = getDS.Tables[17];
                        var getOrgType = getDS.Tables[18];
                        BindDDOrgNameDDL(getOrgName);
                        BindDDOrgTypeDDL(getOrgType);
                        BindIndustryTypeDDDDL(getInsustryTypeDD);
                        BindDomainDDDDL(getDomainDD);
                        BindRoleDDDDL(getRoleDD);
                        BindDomainDetailsGRID(getDomainDetails);
                    }
                    if (type == "Document")
                    {
                        var getDocumentType = getDS.Tables[19];
                        var getDocumentDetails = getDS.Tables[20];
                        BindDocument(getDocumentType, getDocumentDetails);
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    // Bind Family Details

    private void BindFamilyDetails(DataTable dtFamilyDetails, DataTable dtRelation, string isMarried)
    {
        try
        {
            var getFamilyDetailsDT = dtFamilyDetails;
            var getRelationDT = dtRelation;
            BindRelationDDL(getRelationDT);
            int rowCount = 2;
            foreach (ListItem itm in ddl_Relation.Items)
            {
                if (itm.Value == "1" || itm.Value == "2")
                {
                    itm.Attributes.Add("disabled", "disabled");
                }

                if (isMarried == "Married")
                {
                    rowCount = 3;
                    if (itm.Value == "3")
                        itm.Attributes.Add("disabled", "disabled");
                }
                if (getFamilyDetailsDT.AsEnumerable().Any(row => row.Field<int>("RelationId") == 3 && itm.Value == "3"))
                {
                    itm.Attributes.Add("disabled", "disabled");
                }
            }
            if (getFamilyDetailsDT.Rows.Count > 0)
            {
                var rows = getRelationDT.AsEnumerable().Where(r => r.Field<int>("Id") == 1 || r.Field<int>("Id") == 2);
                if (rowCount == 3)
                {
                    rows = getRelationDT.AsEnumerable().Where(r => r.Field<int>("Id") == 1 || r.Field<int>("Id") == 2 || r.Field<int>("Id") == 3);
                }

                foreach (DataRow item in rows)
                {
                    //var getI = ;
                    if (!getFamilyDetailsDT.AsEnumerable().Any(row => row.Field<int>("RelationId") == int.Parse(item["Id"].ToString())))
                    {
                        DataRow _dr = getFamilyDetailsDT.NewRow();
                        _dr["Id"] = 0;
                        _dr["Name"] = "";
                        _dr["RelationName"] = item["Relation"].ToString();
                        _dr["RelationId"] = int.Parse(item["Id"].ToString());
                        _dr["DOB"] = "";
                        _dr["ContactNo"] = "";
                        getFamilyDetailsDT.Rows.Add(_dr);
                    }                    
                }
                BindFamilyDetailsGRID(getFamilyDetailsDT);
            }
            else
            {
                var rows = getRelationDT.AsEnumerable().Where(r => r.Field<int>("Id") == 1 || r.Field<int>("Id") == 2);
                if (rowCount == 3)
                {
                    rows = getRelationDT.AsEnumerable().Where(r => r.Field<int>("Id") == 1 || r.Field<int>("Id") == 2 || r.Field<int>("Id") == 3);
                }
                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Add("Id");
                dt.Columns.Add("Name");
                dt.Columns.Add("RelationName");
                dt.Columns.Add("RelationId");
                dt.Columns.Add("DOB");
                dt.Columns.Add("ContactNo");
                foreach (DataRow item in rows)
                {
                    DataRow _dr = dt.NewRow();
                    _dr["Id"] = 0;
                    _dr["Name"] = "";
                    _dr["RelationName"] = item["Relation"].ToString();
                    _dr["RelationId"] = int.Parse(item["Id"].ToString());
                    _dr["DOB"] = "";
                    _dr["ContactNo"] = "";
                    dt.Rows.Add(_dr);
                }
                BindFamilyDetailsGRID(dt);
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void BindRelationDDL(DataTable dataTable)
    {
        ddl_Relation.DataSource = null;
        ddl_Relation.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            ddl_Relation.DataSource = dataTable;
            ddl_Relation.DataTextField = "Relation";
            ddl_Relation.DataValueField = "Id";
            ddl_Relation.DataBind();
            ddl_Relation.Items.Insert(0, new ListItem("Select Relation", "0"));
        }
    }
    private void BindFamilyDetailsGRID(DataTable dataTable)
    {
        dg_FimalyDetails.DataSource = null;
        dg_FimalyDetails.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            dg_FimalyDetails.DataSource = dataTable;
            dg_FimalyDetails.DataBind();
        }
        else
        {

        }
    }
    private void ResetFamilyDetails()
    {
        hdnFamilyDetailID.Value = "0";
        btn_FD_Update.Visible = false;
        btn_FD_Cancel.Visible = false;
        btn_FD_Save.Visible = true;
        txt_FD_Name.Text = "";
        txt_FD_DOB.Text = "";
        txt_FD_ContectNo.Text = "";
        BindEmpDetails(Convert.ToString(Session["Empcode"]), "Family");
    }
    //End
    //Eduaction Details
    private void BindEduactionDetails(DataTable dtEduactionDetails, DataTable dtEducationType, DataTable dtBoards, DataTable dtDegree, DataTable dtStream)
    {
        try
        {
            BindEducationTypeDDL(dtEducationType);
            BindBoardDDL(dtBoards);
            BindDegreeDDL(dtDegree);
            BindStreamDDL(dtStream);
            if (dtEduactionDetails.Rows.Count > 0)
            {
                foreach (ListItem itm in ddl_Qualification.Items)
                {
                    if (dtEduactionDetails.AsEnumerable().Any(row => row.Field<int?>("QualificationId") == int.Parse(itm.Value.ToString())))
                    {
                        var getId = int.Parse(itm.Value.ToString());
                        if (getId == 1 || getId == 2 || getId == 3)
                        {
                            itm.Attributes.Add("disabled", "disabled");
                        }
                    }
                }
            }

            BindEduactionDetailsGRID(dtEduactionDetails);
        }
        catch (Exception ex)
        {
        }
    }
    private void BindEducationTypeDDL(DataTable dataTable)
    {
        ddl_Qualification.DataSource = null;
        ddl_Qualification.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            ddl_Qualification.DataSource = dataTable;
            ddl_Qualification.DataTextField = "EducationType";
            ddl_Qualification.DataValueField = "EducationTypeID";
            ddl_Qualification.DataBind();
            ddl_Qualification.Items.Insert(0, new ListItem("Select Qualification ", "0"));
        }
    }
    private void BindBoardDDL(DataTable dataTable)
    {
        ddl_Board.DataSource = null;
        ddl_Board.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            ddl_Board.DataSource = dataTable;
            ddl_Board.DataTextField = "Board";
            ddl_Board.DataValueField = "Id";
            ddl_Board.DataBind();
            ddl_Board.Items.Insert(0, new ListItem("Select Board ", "0"));
        }
    }
    private void BindDegreeDDL(DataTable dataTable)
    {
        ddl_Degree.DataSource = null;
        ddl_Degree.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            ddl_Degree.DataSource = dataTable;
            ddl_Degree.DataTextField = "Degree";
            ddl_Degree.DataValueField = "Id";
            ddl_Degree.DataBind();
            ddl_Degree.Items.Insert(0, new ListItem("Select Degree ", "0"));
        }
    }

    private void BindStreamDDL(DataTable dataTable)
    {
        ddl_Stream.DataSource = null;
        ddl_Stream.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            ddl_Stream.DataSource = dataTable;
            ddl_Stream.DataTextField = "Stream";
            ddl_Stream.DataValueField = "Id";
            ddl_Stream.DataBind();
            ddl_Stream.Items.Insert(0, new ListItem("Select Stream ", "0"));
        }
    }
    private void BindEduactionDetailsGRID(DataTable dataTable)
    {
        gv_EducationDetails.DataSource = null;
        gv_EducationDetails.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            gv_EducationDetails.DataSource = dataTable;
            gv_EducationDetails.DataBind();
        }
        else
        {

        }
    }
    //End  Eduaction Details
    //Certification Details
    private void BindCertificationDetails(DataTable dtCertificationDetails, DataTable dtCertification, DataTable dtModule)
    {
        try
        {
            BindCertificationDDL(dtCertification);
            BindModuleDDL(dtModule);
            BindCertificationDetailsGRID(dtCertificationDetails);
        }
        catch (Exception ex)
        {
        }
    }
    private void BindCertificationDDL(DataTable dataTable)
    {
        ddl_CD_Certification.DataSource = null;
        ddl_CD_Certification.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            ddl_CD_Certification.DataSource = dataTable;
            ddl_CD_Certification.DataTextField = "Certification";
            ddl_CD_Certification.DataValueField = "Id";
            ddl_CD_Certification.DataBind();
            ddl_CD_Certification.Items.Insert(0, new ListItem("Select Certification ", "0"));
        }
    }
    private void BindModuleDDL(DataTable dataTable)
    {
        ddl_CD_Module.DataSource = null;
        ddl_CD_Module.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            ddl_CD_Module.DataSource = dataTable;
            ddl_CD_Module.DataTextField = "Module";
            ddl_CD_Module.DataValueField = "Id";
            ddl_CD_Module.DataBind();
            ddl_CD_Module.Items.Insert(0, new ListItem("Select Module ", "0"));
        }
    }
    private void BindCertificationDetailsGRID(DataTable dataTable)
    {
        gv_CertificationDetails.DataSource = null;
        gv_CertificationDetails.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            gv_CertificationDetails.DataSource = dataTable;
            gv_CertificationDetails.DataBind();
        }
        else
        {

        }
    }

    //End certification Details
    //Project And Domain
    private void BindProjectTypeDDL(DataTable dataTable)
    {
        ddl_PD_ProjectType.DataSource = null;
        ddl_PD_ProjectType.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            ddl_PD_ProjectType.DataSource = dataTable;
            ddl_PD_ProjectType.DataTextField = "ProjectType";
            ddl_PD_ProjectType.DataValueField = "Id";
            ddl_PD_ProjectType.DataBind();
            ddl_PD_ProjectType.Items.Insert(0, new ListItem("Select Project Type ", "0"));
        }
    }
    private void BindProjectDetailsGRID(DataTable dataTable)
    {
        gv_ProjectDetails.DataSource = null;
        gv_ProjectDetails.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            gv_ProjectDetails.DataSource = dataTable;
            gv_ProjectDetails.DataBind();
        }
        else
        {

        }
    }
    private void BindDomainDetailsGRID(DataTable dataTable)
    {
        gv_DomainDetails.DataSource = null;
        gv_DomainDetails.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            gv_DomainDetails.DataSource = dataTable;
            gv_DomainDetails.DataBind();
        }
        else
        {

        }
    }
    private void BindIndustryTypeDDL(DataTable dataTable)
    {
        lst_PD_IndustryType.DataSource = null;
        lst_PD_IndustryType.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            lst_PD_IndustryType.DataSource = dataTable;
            lst_PD_IndustryType.DataTextField = "IndustryType";
            lst_PD_IndustryType.DataValueField = "Id";
            lst_PD_IndustryType.DataBind();
            lst_PD_IndustryType.Items.Insert(0, new ListItem("Select Industry Type ", "0"));
        }
    }
    private void BindRolePDTypeDDL(DataTable dataTable)
    {
        ddl_PD_Role.DataSource = null;
        ddl_PD_Role.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            ddl_PD_Role.DataSource = dataTable;
            ddl_PD_Role.DataTextField = "Designation";
            ddl_PD_Role.DataValueField = "Id";
            ddl_PD_Role.DataBind();
            ddl_PD_Role.Items.Insert(0, new ListItem("Select Role ", "0"));
        }
    }

    private void BindModulePDTypeDDL(DataTable dataTable)
    {
        ddl_PD_Module.DataSource = null;
        ddl_PD_Module.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            ddl_PD_Module.DataSource = dataTable;
            ddl_PD_Module.DataTextField = "Module";
            ddl_PD_Module.DataValueField = "Id";
            ddl_PD_Module.DataBind();
            ddl_PD_Module.Items.Insert(0, new ListItem("Select Module ", "0"));
        }
    }
    private void BindIndustryTypeDDDDL(DataTable dataTable)
    {
        lst_DD_IndustryType.DataSource = null;
        lst_DD_IndustryType.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            lst_DD_IndustryType.DataSource = dataTable;
            lst_DD_IndustryType.DataTextField = "IndustryType";
            lst_DD_IndustryType.DataValueField = "Id";
            lst_DD_IndustryType.DataBind();
            lst_DD_IndustryType.Items.Insert(0, new ListItem("Select Industry Type ", "0"));
        }
    }
    private void BindDomainDDDDL(DataTable dataTable)
    {
        ddl_DD_Domain.DataSource = null;
        ddl_DD_Domain.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            ddl_DD_Domain.DataSource = dataTable;
            ddl_DD_Domain.DataTextField = "Domain";
            ddl_DD_Domain.DataValueField = "Id";
            ddl_DD_Domain.DataBind();
            ddl_DD_Domain.Items.Insert(0, new ListItem("Select Domain", "0"));
        }
    }
    private void BindRoleDDDDL(DataTable dataTable)
    {
        ddl_DD_Role.DataSource = null;
        ddl_DD_Role.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            ddl_DD_Role.DataSource = dataTable;
            ddl_DD_Role.DataTextField = "Designation";
            ddl_DD_Role.DataValueField = "Id";
            ddl_DD_Role.DataBind();
            ddl_DD_Role.Items.Insert(0, new ListItem("Select Role", "0"));
        }
    }
    private void BindPDOrgNameDDL(DataTable dataTable)
    {
        ddl_PD_OrgName.DataSource = null;
        ddl_PD_OrgName.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            ddl_PD_OrgName.DataSource = dataTable;
            ddl_PD_OrgName.DataTextField = "OrganisationName";
            ddl_PD_OrgName.DataValueField = "Id";
            ddl_PD_OrgName.DataBind();
            ddl_PD_OrgName.Items.Insert(0, new ListItem("Select Organisation Name ", "0"));
        }
    }
    private void BindPDOrgTypeDDL(DataTable dataTable)
    {
        ddl_PD_OrgType.DataSource = null;
        ddl_PD_OrgType.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            ddl_PD_OrgType.DataSource = dataTable;
            ddl_PD_OrgType.DataTextField = "OrganisationType";
            ddl_PD_OrgType.DataValueField = "Id";
            ddl_PD_OrgType.DataBind();
            ddl_PD_OrgType.Items.Insert(0, new ListItem("Select Organisation Type ", "0"));
        }
    }
    private void BindPDRegionDDL(DataTable dataTable)
    {
        ddl_Region.DataSource = null;
        ddl_Region.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            ddl_Region.DataSource = dataTable;
            ddl_Region.DataTextField = "Region";
            ddl_Region.DataValueField = "Id";
            ddl_Region.DataBind();
            ddl_Region.Items.Insert(0, new ListItem("Select Region ", "0"));
        }
    }
    private void BindDDOrgNameDDL(DataTable dataTable)
    {
        ddl_DD_OrgName.DataSource = null;
        ddl_DD_OrgName.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            ddl_DD_OrgName.DataSource = dataTable;
            ddl_DD_OrgName.DataTextField = "OrganisationName";
            ddl_DD_OrgName.DataValueField = "Id";
            ddl_DD_OrgName.DataBind();
            ddl_DD_OrgName.Items.Insert(0, new ListItem("Select Organisation Name ", "0"));
        }
    }
    private void BindDDOrgTypeDDL(DataTable dataTable)
    {
        ddl_DD_OrgType.DataSource = null;
        ddl_DD_OrgType.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            ddl_DD_OrgType.DataSource = dataTable;
            ddl_DD_OrgType.DataTextField = "OrganisationType";
            ddl_DD_OrgType.DataValueField = "Id";
            ddl_DD_OrgType.DataBind();
            ddl_DD_OrgType.Items.Insert(0, new ListItem("Select Organisation Type ", "0"));
        }
    }
    //End Project And Domain
    //Bind Document 
    private void BindDocument(DataTable dtDLL, DataTable dtGrid)
    {
        try
        {
            BindDocumentTypeDDL(dtDLL);
            if (dtGrid.Rows.Count > 0)
            {
                foreach (ListItem itm in ddl_DocumentName.Items)
                {
                    if (dtGrid.AsEnumerable().Any(row => row.Field<int>("DocumentTypeId") == int.Parse(itm.Value.ToString())))
                    {
                        itm.Attributes.Add("disabled", "disabled");
                    }
                }
            }
            BindDocumentDetailsGRID(dtGrid);
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    private void BindDocumentTypeDDL(DataTable dataTable)
    {
        ddl_DocumentName.DataSource = null;
        ddl_DocumentName.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            ddl_DocumentName.DataSource = dataTable;
            ddl_DocumentName.DataTextField = "DocumentType";
            ddl_DocumentName.DataValueField = "Id";
            ddl_DocumentName.DataBind();
            ddl_DocumentName.Items.Insert(0, new ListItem("Select Document Type", "0"));
        }
    }
    private void BindDocumentDetailsGRID(DataTable dataTable)
    {
        gv_Documents.DataSource = null;
        gv_Documents.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            //lnk_FinalSubmit.Visible = true;
            gv_Documents.DataSource = dataTable;
            gv_Documents.DataBind();
        }
        else
        {
           // lnk_FinalSubmit.Visible = false;
        }
    }
    //End Bind Document

    #endregion
    // Family Details
    protected void btn_FD_Save_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            Label1.Text = "";
            LinkButton btn = (LinkButton)sender;
            if (btn.Text == "Save")
            {
                // var get 
                if (Convert.ToString(txt_FD_Name.Text).Trim() == "")
                {
                    Label1.Text = "Please enter name";
                    return;
                }
                if (Convert.ToString(txt_FD_DOB.Text).Trim() == "")
                {
                    Label1.Text = "Please select date of birth";
                    return;
                }
                if (Convert.ToString(ddl_Relation.Text).Trim() == "0")
                {
                    Label1.Text = "Please select relation";
                    return;
                }
                if (Convert.ToString(txt_FD_ContectNo.Text).Trim() == "")
                {
                    Label1.Text = "Please enter contact number";
                    return;
                }
                var getdate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime tempF = DateTime.ParseExact(txt_FD_DOB.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime tempT = DateTime.ParseExact(getdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (tempF >= tempT)
                {
                    Label1.Text = "Future date not allowed.";
                    txt_FD_DOB.Text = "";
                    return;
                }
                var getDBO = "";
                var getName = Convert.ToString(txt_FD_Name.Text).Trim();
                getDBO = Convert.ToString(txt_FD_DOB.Text).Trim();
                var tempDate = getDBO.Split('/');
                getDBO = tempDate[2] + "-" + tempDate[1] + "-" + tempDate[0];
                var getRealtionId = int.Parse(Convert.ToString(ddl_Relation.Text).Trim());
                var getContactNo = Convert.ToString(txt_FD_ContectNo.Text).Trim();
                var getId = "0";
                var empCode = Convert.ToString(hdnempcode.Value).Trim();
                var result = spm.InsertUpdateFamilyDetails(int.Parse(getId), empCode, getName, getRealtionId, getDBO, getContactNo);
                if (result == true)
                {
                    ResetFamilyDetails();
                }
                else
                {
                    Label1.Text = "Something went wrong";
                    return;
                }
            }
            else if (btn.Text == "Update")
            {
                // var get 
                if (Convert.ToString(txt_FD_Name.Text).Trim() == "")
                {
                    Label1.Text = "Please enter name";
                    return;
                }
                if (Convert.ToString(txt_FD_DOB.Text).Trim() == "")
                {
                    Label1.Text = "Please select date of birth";
                    return;
                }
                if (Convert.ToString(ddl_Relation.Text).Trim() == "0")
                {
                    Label1.Text = "Please select relation";
                    return;
                }
                if (Convert.ToString(txt_FD_ContectNo.Text).Trim() == "")
                {
                    Label1.Text = "Please enter contact number";
                    return;
                }
                var getdate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime tempF = DateTime.ParseExact(txt_FD_DOB.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime tempT = DateTime.ParseExact(getdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (tempF >= tempT)
                {
                    Label1.Text = "Future date not allowed.";
                    txt_FD_DOB.Text = "";
                    return;
                }
                var getName = Convert.ToString(txt_FD_Name.Text).Trim();
                var getDBO = Convert.ToString(txt_FD_DOB.Text).Trim();
                var tempDate = getDBO.Split('/');
                getDBO = tempDate[2] + "-" + tempDate[1] + "-" + tempDate[0];
                var getRealtionId = int.Parse(Convert.ToString(ddl_Relation.Text).Trim());
                var getContactNo = Convert.ToString(txt_FD_ContectNo.Text).Trim();
                var getId = Convert.ToString(hdnFamilyDetailID.Value).Trim();
                var empCode = Convert.ToString(hdnempcode.Value).Trim();
                var result = spm.InsertUpdateFamilyDetails(int.Parse(getId), empCode, getName, getRealtionId, getDBO, getContactNo);
                if (result == true)
                {
                    ResetFamilyDetails();
                }
                else
                {
                    Label1.Text = "Something went wrong";
                    return;
                }
            }
            else if (btn.Text == "Cancel")
            {
                ResetFamilyDetails();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
            return;
        }
    }
    protected void ddl_Relation_SelectedIndexChanged(object sender, EventArgs e)
    {
        //return false;
    }
    protected void txt_FD_DOB_TextChanged(object sender, EventArgs e)
    {

    }
    protected void lnk_FD_Edit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            var getSelectedValues = ddl_Relation.SelectedValue.ToString();
            ddl_Relation.Items.FindByValue(getSelectedValues).Selected = false;
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(dg_FimalyDetails.DataKeys[row.RowIndex].Values[0]).Trim();
            var relationId = Convert.ToString(dg_FimalyDetails.DataKeys[row.RowIndex].Values[1]).Trim();
            if (fId != "0")
            {
                var getds = spm.getCVDetailById(int.Parse(fId), "GetFamilyDetailsBYId");
                hdnFamilyDetailID.Value = fId;
                if (getds != null)
                {
                    if (getds.Tables.Count > 0)
                    {
                        var getFamilyDetail = getds.Tables[0];
                        var getRelationDDL = getds.Tables[1];
                        if (getFamilyDetail.Rows.Count > 0)
                        {
                            btn_FD_Save.Visible = false;
                            txt_FD_Name.Text = getFamilyDetail.Rows[0]["Name"].ToString();
                            txt_FD_DOB.Text = getFamilyDetail.Rows[0]["DBO"].ToString();
                            txt_FD_ContectNo.Text = getFamilyDetail.Rows[0]["ContactNo"].ToString();
                            var getRelationId = getFamilyDetail.Rows[0]["RelationId"].ToString();

                            if(getRelationId!=null&& getRelationId!="")
                            {
                                foreach (ListItem itm in ddl_Relation.Items)
                                {
                                    if (itm.Value != getRelationId)
                                    {
                                        itm.Attributes.Add("disabled", "disabled");
                                    }
                                    else
                                    {
                                        itm.Selected = true;
                                    }
                                }
                            }

                          
                            btn_FD_Update.Visible = true;
                            btn_FD_Cancel.Visible = true;
                        }
                        else
                        {

                        }
                    }
                }
            }
            else
            {
                //  ddl_Relation.SelectedItem. = false;
                btn_FD_Update.Visible = true;
                btn_FD_Cancel.Visible = true;
                btn_FD_Save.Visible = false;
                foreach (ListItem itm in ddl_Relation.Items)
                {
                    if (itm.Value != relationId)
                    {
                        itm.Attributes.Add("disabled", "disabled");
                    }
                    else
                    {
                        itm.Attributes.Add("disabled", "");
                    }
                }
                ddl_Relation.Items.FindByValue(relationId).Selected = true;
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
            return;
        }

    }
    // End Family Details
    //Education details
    protected void ddl_Qualification_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_Board_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_Degree_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lnk_ED_Edit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            var getSelectedValues = ddl_Qualification.SelectedValue.ToString();
            var getSelectedValues1 = ddl_Board.SelectedValue.ToString();
            var getSelectedValues2 = ddl_Degree.SelectedValue.ToString();
            var getSelectedValues3 = ddl_Stream.SelectedValue.ToString();
            ddl_Qualification.Items.FindByValue(getSelectedValues).Selected = false;
            ddl_Board.Items.FindByValue(getSelectedValues1).Selected = false;
            ddl_Degree.Items.FindByValue(getSelectedValues2).Selected = false;
            ddl_Stream.Items.FindByValue(getSelectedValues3).Selected = false;
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(gv_EducationDetails.DataKeys[row.RowIndex].Values[0]).Trim();
            if (fId != "0")
            {
                var getds = spm.getCVDetailById(int.Parse(fId), "getEduactionDetailsById");
                hdnEduactonDetailID.Value = fId;
                if (getds != null)
                {
                    if (getds.Tables.Count > 0)
                    {
                        var getEduactionDetail = getds.Tables[0];
                        if (getEduactionDetail.Rows.Count > 0)
                        {
                            lnk_ed_Save.Visible = false;

                            txt_Institute.Text = getEduactionDetail.Rows[0]["University_Institute"].ToString();
                            txt_Stream.Text = getEduactionDetail.Rows[0]["Stream"].ToString();
                            txt_YearOfPassing.Text = getEduactionDetail.Rows[0]["YearOfPassing"].ToString();
                            txt_GradeMarks.Text = getEduactionDetail.Rows[0]["MarksObtained"].ToString();
                            txt_TotalMark.Text = getEduactionDetail.Rows[0]["TotalMarks"].ToString();
                            
                            if(getEduactionDetail.Rows[0]["Iscompleted"].ToString()!="")
                            {
                                chk_ED_Iscompleted.Checked = Convert.ToBoolean(getEduactionDetail.Rows[0]["Iscompleted"].ToString());
                                if (Convert.ToBoolean(getEduactionDetail.Rows[0]["Iscompleted"].ToString()) == false)
                                {
                                    txt_GradeMarks.Enabled = false;
                                    txt_YearOfPassing.Enabled = false;
                                    txt_TotalMark.Enabled = false;
                                }
                                else
                                {
                                    txt_GradeMarks.Enabled = true;
                                    txt_YearOfPassing.Enabled = true;
                                    txt_TotalMark.Enabled = true;
                                }
                            }                           
                            var getBoardId = getEduactionDetail.Rows[0]["BoardId"].ToString();
                            var getDegreeId = getEduactionDetail.Rows[0]["DegreeId"].ToString();
                            var getStreamId = getEduactionDetail.Rows[0]["StreamID"].ToString();
                            if(getBoardId!=null&&getBoardId!="")
                            {
                                ddl_Board.Items.FindByValue(getBoardId).Selected = true;
                            }
                            if(getDegreeId!=null&& getDegreeId!="")
                            {
                                ddl_Degree.Items.FindByValue(getDegreeId).Selected = true;
                            }
                            if(getStreamId!=""&&getStreamId!=null)
                            {
                                ddl_Stream.Items.FindByValue(getStreamId).Selected = true;
                            }                            

                            var getQualificationId = getEduactionDetail.Rows[0]["QualificationId"].ToString();
                            if(getQualificationId!=null&&getQualificationId!="")
                            {
                                foreach (ListItem itm in ddl_Qualification.Items)
                                {
                                    if (itm.Value != getQualificationId)
                                    {
                                        itm.Attributes.Add("disabled", "disabled");
                                    }
                                    else
                                    {
                                        itm.Selected = true;
                                    }
                                }
                            }                           

                            lnk_ed_Update.Visible = true;
                            lnk_ed_Cancel.Visible = true;
                        }
                        else
                        {

                        }
                    }
                }
            }
            else
            {
                Label2.Text = "Something went wrong.";
            }
        }
        catch (Exception ex)
        {
            Label2.Text = ex.Message;
        }
    }
    protected void lnk_ed_Save_Click(object sender, EventArgs e)
    {
        try
        {
            decimal getGrede = 0;
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            Label2.Text = "";
            LinkButton btn = (LinkButton)sender;
            if (btn.Text == "Save")
            {
                if (Convert.ToString(ddl_Qualification.SelectedValue).Trim() == "0")
                {
                    Label2.Text = "Please select qualification.";
                    return;
                }
                if (Convert.ToString(txt_Institute.Text).Trim() == "")
                {
                    Label2.Text = "Please enter institute name.";
                    return;
                }
                if (Convert.ToString(ddl_Board.SelectedValue).Trim() == "0")
                {
                    Label2.Text = "Please select board.";
                    return;
                }
                if (Convert.ToString(ddl_Degree.SelectedValue).Trim() == "0")
                {
                    Label2.Text = "Please select degree.";
                    return;
                }
                if (Convert.ToString(ddl_Stream.SelectedValue).Trim() == "0")
                {
                    Label2.Text = "Please select stream.";
                    return;
                }
                if (Convert.ToBoolean(chk_ED_Iscompleted.Checked) == true)
                {
                    if (Convert.ToString(txt_YearOfPassing.Text).Trim() == "")
                    {
                        Label2.Text = "Please select date of passing.";
                        return;
                    }
                    var getdate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime tempF1 = DateTime.ParseExact(txt_YearOfPassing.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime tempT1 = DateTime.ParseExact(getdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (tempT1 < tempF1)
                    {
                        Label2.Text = "Future date not allowed.";
                        txt_YearOfPassing.Text = "";
                        return;
                    }
                    if (Convert.ToString(txt_GradeMarks.Text).Trim() == "")
                    {
                        Label2.Text = "Please enter grade marks";
                        return;
                    }
                    if (Convert.ToString(txt_TotalMark.Text).Trim() == "")
                    {
                        Label2.Text = "Please enter total marks";
                        return;
                    }
                    var getTotal = Convert.ToDecimal(txt_TotalMark.Text.Trim());
                    var getMarkobtained = Convert.ToDecimal(txt_GradeMarks.Text.Trim());

                    if (getMarkobtained > getTotal)
                    {
                        Label2.Text = "Marks obtained can't be greater than total marks";
                        return;
                    }
                    var getGrade = ((getMarkobtained / getTotal) * 100);
                    getGrede = getGrade;
                }

                var getQulificationId = int.Parse(Convert.ToString(ddl_Qualification.SelectedValue).Trim());
                var getBoardId = int.Parse(Convert.ToString(ddl_Board.SelectedValue).Trim());
                var getDegreeId = int.Parse(Convert.ToString(ddl_Degree.SelectedValue).Trim());
                var getInstitute = Convert.ToString(txt_Institute.Text).Trim();
                var getStream = Convert.ToString(ddl_Stream.SelectedValue).Trim();
                string getYOP = "";
                decimal? getGMark = null;
                decimal? getObtainMark = null;
                decimal? getTotalMarks = null;
                var getIsCompleted = Convert.ToBoolean(chk_ED_Iscompleted.Checked);
                if (getIsCompleted == true)
                {
                    var YearOfPassing = Convert.ToString(txt_YearOfPassing.Text).Trim();
                    var tempSplitDate = YearOfPassing.Split('/');
                    getYOP = tempSplitDate[2] + "-" + tempSplitDate[1] + "-" + tempSplitDate[0];

                    getObtainMark = decimal.Parse(Convert.ToString(txt_GradeMarks.Text).Trim());
                    getGMark = getGrede;
                    getTotalMarks = decimal.Parse(Convert.ToString(txt_TotalMark.Text).Trim());

                }
                var empCode = Convert.ToString(hdnempcode.Value).Trim();
                //return;
                var getStatus = spm.InsertUpdateEduactionDetails(0, empCode, getQulificationId, getBoardId, getDegreeId, getStream, getYOP, getGMark, getInstitute, getTotalMarks, getIsCompleted, getObtainMark);
                if (getStatus == true)
                {
                    ClearEduactionDetails();
                }
                else
                {
                    Label2.Text = "Something went wrong. Please try again";
                    return;
                }
            }
            else if (btn.Text == "Update")
            {
                if (Convert.ToString(ddl_Qualification.SelectedValue).Trim() == "0")
                {
                    Label2.Text = "Please select qualification.";
                    return;
                }
                if (Convert.ToString(txt_Institute.Text).Trim() == "")
                {
                    Label2.Text = "Please enter institute name.";
                    return;
                }
                if (Convert.ToString(ddl_Board.SelectedValue).Trim() == "0")
                {
                    Label2.Text = "Please select board.";
                    return;
                }
                if (Convert.ToString(ddl_Degree.SelectedValue).Trim() == "0")
                {
                    Label2.Text = "Please select degree.";
                    return;
                }
                if (Convert.ToString(ddl_Stream.SelectedValue).Trim() == "0")
                {
                    Label2.Text = "Please select stream.";
                    return;
                }
                if (Convert.ToBoolean(chk_ED_Iscompleted.Checked) == true)
                {
                    if (Convert.ToString(txt_YearOfPassing.Text).Trim() == "")
                    {
                        Label2.Text = "Please select date of passing.";
                        return;
                    }
                    var getdate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime tempF1 = DateTime.ParseExact(txt_YearOfPassing.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime tempT1 = DateTime.ParseExact(getdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (tempT1 < tempF1)
                    {
                        Label2.Text = "Future date not allowed.";
                        txt_YearOfPassing.Text = "";
                        return;
                    }
                    if (Convert.ToString(txt_GradeMarks.Text).Trim() == "")
                    {
                        Label2.Text = "Please enter grade marks";
                        return;
                    }
                    if (Convert.ToString(txt_TotalMark.Text).Trim() == "")
                    {
                        Label2.Text = "Please enter total marks";
                        return;
                    }
                    var getTotal = Convert.ToDecimal(txt_TotalMark.Text.Trim());
                    var getMarkobtained = Convert.ToDecimal(txt_GradeMarks.Text.Trim());

                    if (getMarkobtained > getTotal)
                    {
                        Label2.Text = "Please enter obtained mark less than total marks";
                        return;
                    }
                    var getGrade = ((getMarkobtained / getTotal) * 100);
                    getGrede = getGrade;
                }

                var getQulificationId = int.Parse(Convert.ToString(ddl_Qualification.SelectedValue).Trim());
                var getBoardId = int.Parse(Convert.ToString(ddl_Board.SelectedValue).Trim());
                var getDegreeId = int.Parse(Convert.ToString(ddl_Degree.SelectedValue).Trim());
                var getInstitute = Convert.ToString(txt_Institute.Text).Trim();
                var getStream = Convert.ToString(ddl_Stream.SelectedValue).Trim();
                string getYOP = ""; ;
                decimal? getGMark = null;
                decimal? getObtainMark = null;
                decimal? getTotalMarks = null;
                var getIsCompleted = Convert.ToBoolean(chk_ED_Iscompleted.Checked);
                if (getIsCompleted == true)
                {
                    var YearOfPassing = Convert.ToString(txt_YearOfPassing.Text).Trim();
                    var tempSplitDate = YearOfPassing.Split('/');
                    getYOP = tempSplitDate[2] + "-" + tempSplitDate[1] + "-" + tempSplitDate[0];
                    getObtainMark = decimal.Parse(Convert.ToString(txt_GradeMarks.Text).Trim());
                    getGMark = getGrede;
                    getTotalMarks = decimal.Parse(Convert.ToString(txt_TotalMark.Text).Trim());
                }
                var empCode = Convert.ToString(hdnempcode.Value).Trim();
                var id = int.Parse(Convert.ToString(hdnEduactonDetailID.Value).Trim());
                var getStatus = spm.InsertUpdateEduactionDetails(id, empCode, getQulificationId, getBoardId, getDegreeId, getStream, getYOP, getGMark, getInstitute, getTotalMarks, getIsCompleted, getObtainMark);
                if (getStatus == true)
                {
                    ClearEduactionDetails();
                }
                else
                {
                    Label2.Text = "Something went wrong. Please try again";
                    return;
                }
            }
            else if (btn.Text == "Cancel")
            {
                ClearEduactionDetails();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            Label2.Text = "Something went wrong. please try again";
            return;
        }
    }
    private void ClearEduactionDetails()
    {
        lnk_ed_Update.Visible = false;
        lnk_ed_Cancel.Visible = false;
        lnk_ed_Save.Visible = true;

        txt_Institute.Text = "";
        txt_Stream.Text = "";
        txt_YearOfPassing.Text = "";
        txt_GradeMarks.Text = "";
        txt_TotalMark.Text = "";
        chk_ED_Iscompleted.Checked = false;
        txt_GradeMarks.Enabled = false;
        txt_YearOfPassing.Enabled = false;
        txt_TotalMark.Enabled = false;
        hdnEduactonDetailID.Value = "0";
        BindEmpDetails(Convert.ToString(Session["Empcode"]), "Education");
    }
    // End Education Details

    // Certification Details
    protected void lnk_CD_Save_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            Label3.Text = "";
            LinkButton btn = (LinkButton)sender;
            if (btn.Text == "Save")
            {
                if (Convert.ToString(ddl_CD_Certification.SelectedValue).Trim() == "0")
                {
                    Label3.Text = "Please select certification.";
                    return;
                }
                if (Convert.ToString(txt_CD_InstituteName.Text).Trim() == "")
                {
                    Label3.Text = "Please enter institute name.";
                    return;
                }
                if (Convert.ToString(ddl_CD_Module.SelectedValue).Trim() == "0")
                {
                    Label3.Text = "Please select module.";
                    return;
                }
                var getVal = chk_CD_Isompleted.Checked;
                if (getVal == true)
                {
                    if (Convert.ToString(txt_CD_CertificationNo.Text).Trim() == "")
                    {
                        Label3.Text = "Please enter certification no.";
                        return;
                    }
                    if (Convert.ToString(txt_CD_FromDate.Text).Trim() == "")
                    {
                        Label3.Text = "Please select from date.";
                        return;
                    }
                    if (Convert.ToString(txt_CD_ToDate.Text).Trim() == "")
                    {
                        Label3.Text = "Please select to date.";
                        return;
                    }
                    var getdate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime tempF1 = DateTime.ParseExact(txt_CD_FromDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime tempT1 = DateTime.ParseExact(getdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (tempT1 < tempF1)
                    {
                        Label3.Text = "Future date not allowed.";
                        txt_CD_FromDate.Text = "";
                        return;
                    }
                    DateTime tempF = DateTime.ParseExact(txt_CD_FromDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime tempT = DateTime.ParseExact(txt_CD_ToDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (tempF >= tempT)
                    {
                        Label3.Text = "From date cannot be grater than to date";
                        //txtFromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        txt_CD_ToDate.Text = "";
                        return;
                    }                 
                }

                var getDertificationId = int.Parse(Convert.ToString(ddl_CD_Certification.SelectedValue).Trim());
                var getModuleId = int.Parse(Convert.ToString(ddl_CD_Module.SelectedValue).Trim());

                var getInstitute = Convert.ToString(txt_CD_InstituteName.Text).Trim();
                var getCertificationNo = Convert.ToString(txt_CD_CertificationNo.Text).Trim();
                var getFromDate = "";
                var getToDate = "";
                var empCode = Convert.ToString(hdnempcode.Value).Trim();
                if (getVal == true)
                {
                    var tempFromDate = Convert.ToString(txt_CD_FromDate.Text).Trim();
                    var tempToDate = Convert.ToString(txt_CD_ToDate.Text).Trim();
                    var tempSplitDate = tempFromDate.Split('/');
                    getFromDate = tempSplitDate[2] + "-" + tempSplitDate[1] + "-" + tempSplitDate[0];
                    tempSplitDate = tempToDate.Split('/');
                    getToDate = tempSplitDate[2] + "-" + tempSplitDate[1] + "-" + tempSplitDate[0];

                    //var CheckIsValidFromDate = spm.IsCheckDate("CheckCertificationDate", getFromDate, empCode);
                    //if (CheckIsValidFromDate != null)
                    //{
                    //    if (CheckIsValidFromDate.Rows.Count > 0)
                    //    {
                    //        var getMessage = Convert.ToString(CheckIsValidFromDate.Rows[0]["MESSAGE"].ToString());
                    //        if (getMessage == "Exist")
                    //        {
                    //            Label3.Text = "From date cannot be exist in date range";
                    //            txt_CD_FromDate.Text = "";
                    //            return;
                    //        }
                    //    }
                    //}
                    //var CheckIsValidToDate = spm.IsCheckDate("CheckCertificationDate", getToDate, empCode);
                    //if (CheckIsValidToDate != null)
                    //{
                    //    if (CheckIsValidToDate.Rows.Count > 0)
                    //    {
                    //        var getMessage = Convert.ToString(CheckIsValidToDate.Rows[0]["MESSAGE"].ToString());
                    //        if (getMessage == "Exist")
                    //        {
                    //            Label3.Text = "To date cannot be exist in date range";
                    //            txt_CD_ToDate.Text = "";
                    //            return;
                    //        }
                    //    }
                    //}
                }


                var getStatus = spm.InsertUpdateCertificationDetails(0, empCode, getDertificationId, getInstitute, getModuleId, getFromDate, getToDate, getVal, getCertificationNo);
                if (getStatus == true)
                {
                    ClearCertificationDetails();
                }
                else
                {
                    Label3.Text = "Something went wrong. Please try again";
                    return;
                }
            }
            else if (btn.Text == "Update")
            {
                if (Convert.ToString(ddl_CD_Certification.SelectedValue).Trim() == "0")
                {
                    Label3.Text = "Please select certification.";
                    return;
                }
                if (Convert.ToString(txt_CD_InstituteName.Text).Trim() == "")
                {
                    Label3.Text = "Please enter institute name.";
                    return;
                }
                if (Convert.ToString(ddl_CD_Module.SelectedValue).Trim() == "0")
                {
                    Label3.Text = "Please select module.";
                    return;
                }
                var getVal = chk_CD_Isompleted.Checked;
                if (getVal == true)
                {
                    //txt_CD_CertificationNo

                    if (Convert.ToString(txt_CD_CertificationNo.Text).Trim() == "")
                    {
                        Label3.Text = "Please enter certification no.";
                        return;
                    }
                    if (Convert.ToString(txt_CD_FromDate.Text).Trim() == "")
                    {
                        Label3.Text = "Please select from date.";
                        return;
                    }
                    if (Convert.ToString(txt_CD_ToDate.Text).Trim() == "")
                    {
                        Label3.Text = "Please select to date.";
                        return;
                    }
                    var getdate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime tempF1 = DateTime.ParseExact(txt_CD_FromDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime tempT1 = DateTime.ParseExact(getdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (tempT1 < tempF1)
                    {
                        Label3.Text = "Future date not allowed.";
                        txt_CD_FromDate.Text = "";
                        return;
                    }
                    DateTime tempF = DateTime.ParseExact(txt_CD_FromDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime tempT = DateTime.ParseExact(txt_CD_ToDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (tempF >= tempT)
                    {
                        Label3.Text = "From date cannot be grater than to date";
                        //txtFromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        txt_CD_ToDate.Text = "";
                        return;
                    }
                }

                var getDertificationId = int.Parse(Convert.ToString(ddl_CD_Certification.SelectedValue).Trim());
                var getModuleId = int.Parse(Convert.ToString(ddl_CD_Module.SelectedValue).Trim());

                var getInstitute = Convert.ToString(txt_CD_InstituteName.Text).Trim();
                var getCertificationNo = Convert.ToString(txt_CD_CertificationNo.Text).Trim();
                var getFromDate = "";
                var getToDate = "";
                var empCode = Convert.ToString(hdnempcode.Value).Trim();
                var Id = int.Parse(Convert.ToString(hdnCertificationDetailID.Value).Trim());
                if (getVal == true)
                {
                    var tempFromDate = Convert.ToString(txt_CD_FromDate.Text).Trim();
                    var tempToDate = Convert.ToString(txt_CD_ToDate.Text).Trim();
                    var tempSplitDate = tempFromDate.Split('/');
                    getFromDate = tempSplitDate[2] + "-" + tempSplitDate[1] + "-" + tempSplitDate[0];
                    tempSplitDate = tempToDate.Split('/');
                    getToDate = tempSplitDate[2] + "-" + tempSplitDate[1] + "-" + tempSplitDate[0];

                    ////Check Date
                    //var CheckIsValidFromDate = spm.IsCheckDateById("CheckCertificationDateById", getFromDate, Id, empCode);
                    //if (CheckIsValidFromDate != null)
                    //{
                    //    if (CheckIsValidFromDate.Rows.Count > 0)
                    //    {
                    //        var getMessage = Convert.ToString(CheckIsValidFromDate.Rows[0]["MESSAGE"].ToString());
                    //        if (getMessage == "Exist")
                    //        {
                    //            Label3.Text = "From date cannot be exist in date range";
                    //            txt_CD_FromDate.Text = "";
                    //            return;
                    //        }
                    //    }
                    //}
                    //var CheckIsValidToDate = spm.IsCheckDateById("CheckCertificationDateById", getToDate, Id,empCode);
                    //if (CheckIsValidToDate != null)
                    //{
                    //    if (CheckIsValidToDate.Rows.Count > 0)
                    //    {
                    //        var getMessage = Convert.ToString(CheckIsValidToDate.Rows[0]["MESSAGE"].ToString());
                    //        if (getMessage == "Exist")
                    //        {
                    //            Label3.Text = "To date cannot be exist in date range";
                    //            txt_CD_ToDate.Text = "";
                    //            return;
                    //        }
                    //    }
                    //}
                }

              
                var getStatus = spm.InsertUpdateCertificationDetails(Id, empCode, getDertificationId, getInstitute, getModuleId, getFromDate, getToDate, getVal, getCertificationNo);
                if (getStatus == true)
                {
                    ClearCertificationDetails();
                }
                else
                {
                    Label3.Text = "Something went wrong. Please try again";
                    return;
                }
            }
            else if (btn.Text == "Cancel")
            {
                ClearCertificationDetails();
            }
            else
            {
                Label3.Text = "Something went wrong. please try again";
                return;
            }
        }
        catch (Exception ex)
        {
            Label3.Text = "Something went wrong. please try again";
            return;
        }
    }
    protected void ddl_CD_Certification_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_CD_Module_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddl_CD_Certification_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void lnk_Certi_D_Edit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Label3.Text = "";
            var getSelectedValues = ddl_CD_Certification.SelectedValue.ToString();
            var getSelectedValues1 = ddl_CD_Module.SelectedValue.ToString();
            ddl_CD_Certification.Items.FindByValue(getSelectedValues).Selected = false;
            ddl_CD_Module.Items.FindByValue(getSelectedValues1).Selected = false;
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(gv_CertificationDetails.DataKeys[row.RowIndex].Values[0]).Trim();
            if (fId != "0")
            {
                var getds = spm.getCVDetailById(int.Parse(fId), "getCertificationById");
                hdnCertificationDetailID.Value = fId;
                if (getds != null)
                {
                    if (getds.Tables.Count > 0)
                    {
                        var getCertificationDetail = getds.Tables[0];
                        if (getCertificationDetail.Rows.Count > 0)
                        {
                            lnk_CD_Save.Visible = false;

                            txt_CD_InstituteName.Text = getCertificationDetail.Rows[0]["InstituteName"].ToString();
                            txt_CD_CertificationNo.Text = getCertificationDetail.Rows[0]["CertificationNo"].ToString();
                            txt_CD_FromDate.Text = getCertificationDetail.Rows[0]["ValidFromDate"].ToString();
                            txt_CD_ToDate.Text = getCertificationDetail.Rows[0]["ValidTODate"].ToString();

                            var getCertificationIdd = getCertificationDetail.Rows[0]["CertificationId"].ToString();
                            var getModuleId = getCertificationDetail.Rows[0]["ModuleId"].ToString();
                            // var txt_CD_CertificationNo = getCertificationDetail.Rows[0]["CertificationNo"].ToString();
                            if(getCertificationIdd!=null&& getCertificationIdd!="")
                            {
                                ddl_CD_Certification.Items.FindByValue(getCertificationIdd).Selected = true;
                            }
                            if (getModuleId != null && getModuleId != "")
                            {
                                ddl_CD_Module.Items.FindByValue(getModuleId).Selected = true;
                            }

                            
                            var isCompleted = false;
                            if (getCertificationDetail.Rows[0]["Iscompleted"].ToString() != "")
                            {
                                isCompleted = Convert.ToBoolean(getCertificationDetail.Rows[0]["Iscompleted"].ToString());
                                chk_CD_Isompleted.Checked = Convert.ToBoolean(getCertificationDetail.Rows[0]["Iscompleted"].ToString());
                            }

                            if (isCompleted == true)
                            {
                                txt_CD_FromDate.Enabled = true;
                                txt_CD_ToDate.Enabled = true;
                                txt_CD_CertificationNo.Enabled = true;
                            }
                            else
                            {
                                txt_CD_FromDate.Enabled = false;
                                txt_CD_ToDate.Enabled = false;
                                txt_CD_CertificationNo.Enabled = false;

                                txt_CD_FromDate.Text = "";
                                txt_CD_ToDate.Text = "";
                                txt_CD_CertificationNo.Text = "";
                            }


                            //var getQualificationId = getCertificationDetail.Rows[0]["QualificationId"].ToString();
                            //foreach (ListItem itm in ddl_Qualification.Items)
                            //{
                            //    if (itm.Value != getQualificationId)
                            //    {
                            //        itm.Attributes.Add("disabled", "disabled");
                            //    }
                            //    else
                            //    {
                            //        itm.Selected = true;
                            //    }
                            //}

                            lnk_CD_Update.Visible = true;
                            lnk_CD_Cancel.Visible = true;
                        }
                        else
                        {

                        }
                    }
                }
            }
            else
            {
                Label3.Text = "Something went wrong.";
            }
        }
        catch (Exception ex)
        {
            Label3.Text = ex.Message;
        }
    }
    private void ClearCertificationDetails()
    {
        lnk_CD_Update.Visible = false;
        lnk_CD_Cancel.Visible = false;
        lnk_CD_Save.Visible = true;
        chk_CD_Isompleted.Checked = false;

        txt_CD_ToDate.Text = "";
        txt_CD_FromDate.Text = "";
        txt_CD_InstituteName.Text = "";
        txt_CD_CertificationNo.Text = "";
        hdnCertificationDetailID.Value = "0";
        txt_CD_ToDate.Enabled = false;
        txt_CD_FromDate.Enabled = false;
        txt_CD_CertificationNo.Enabled = false;

        BindEmpDetails(Convert.ToString(Session["Empcode"]), "Certification");
    }
    //End CertificationDetails
    //Start Project Details
    protected void ddl_PD_ProjectType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txt_PD_FromDate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void lnk_PD_Save_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            Label4.Text = "";
            LinkButton btn = (LinkButton)sender;
            if (btn.Text == "Save")
            {
                if (Convert.ToString(ddl_PD_ProjectType.SelectedValue).Trim() == "0")
                {
                    Label4.Text = "Please select project type.";
                    return;
                }
                var getSelectedIType = false;
                var getIndustryType = "";
                foreach (ListItem item in lst_PD_IndustryType.Items)
                {
                    if (item.Selected)
                    {
                        if (item.Value != "" && item.Value != "0")
                        {
                            getSelectedIType = true;
                            if (getIndustryType == "")
                            {
                                getIndustryType = item.Value;
                            }
                            else
                            {
                                getIndustryType = getIndustryType + "," + item.Value;
                            }
                        }
                    }
                }
                if (getSelectedIType == false)
                {
                    Label4.Text = "Please select industry type.";
                    return;
                }
                if (Convert.ToString(ddl_PD_Role.SelectedValue).Trim() == "0")
                {
                    Label4.Text = "Please select role.";
                    return;
                }
                if (Convert.ToString(ddl_PD_Module.SelectedValue).Trim() == "0")
                {
                    Label4.Text = "Please select module.";
                    return;
                }

                if (Convert.ToString(ddl_PD_OrgName.SelectedValue).Trim() == "0")
                {
                    Label4.Text = "Please enter organisation name.";
                    return;
                }
                if (Convert.ToString(ddl_PD_OrgType.SelectedValue).Trim() == "0")
                {
                    Label4.Text = "Please enter organisation type.";
                    return;
                }
              
                if (Convert.ToString(txt_PD_ProductImplemented.Text).Trim() == "")
                {
                    Label4.Text = "Please enter product implemented.";
                    return;
                }
                if (Convert.ToString(txt_PD_Summary.Text).Trim() == "")
                {
                    Label4.Text = "Please enter brief summary of role.";
                    return;
                }
                if (Convert.ToString(txt_PD_ClientName.Text).Trim() == "")
                {
                    Label4.Text = "Please enter client name.";
                    return;
                }
                if (Convert.ToString(ddl_Region.SelectedValue).Trim() == "0")
                {
                    Label4.Text = "Please select region.";
                    return;
                }
                if (Convert.ToString(txt_PD_FromDate.Text).Trim() == "")
                {
                    Label4.Text = "Please select from date.";
                    return;
                }
                var getdate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime tempF1 = DateTime.ParseExact(txt_PD_FromDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime tempT1 = DateTime.ParseExact(getdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (tempT1 < tempF1)
                {
                    Label4.Text = "Future date not allowed.";
                    txt_PD_FromDate.Text = "";
                    return;
                }
                if (chk_PD_IsCurrentProject.Checked == false)
                {                    
                    if (Convert.ToString(txt_PD_ToDate.Text).Trim() == "")
                    {
                        Label4.Text = "Please select to date.";
                        return;
                    }
                    
                    tempF1 = DateTime.ParseExact(txt_PD_ToDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (tempT1 < tempF1)
                    {
                        Label4.Text = "Future date not allowed.";
                        txt_PD_ToDate.Text = "";
                        return;
                    }

                    DateTime tempF = DateTime.ParseExact(txt_PD_FromDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime tempT = DateTime.ParseExact(txt_PD_ToDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (tempF >= tempT)
                    {
                        Label4.Text = "From date cannot be grater than to date";
                        txt_PD_ToDate.Text = "";
                        return;
                    }
                }
                var getProjectTypeId = int.Parse(Convert.ToString(ddl_PD_ProjectType.SelectedValue).Trim());

                var getClientName = Convert.ToString(txt_PD_ClientName.Text).Trim();
                //var getIndustryType = Convert.ToString(txt_PD_IndustryType.Text).Trim();
                var getSummary = Convert.ToString(txt_PD_Summary.Text).Trim();

                var tempFromDate = Convert.ToString(txt_PD_FromDate.Text).Trim();
             
                var getFromDate = "";
                var getToDate = "";
                var empCode = Convert.ToString(hdnempcode.Value).Trim();
                var tempSplitDate = tempFromDate.Split('/');
                getFromDate = tempSplitDate[2] + "-" + tempSplitDate[1] + "-" + tempSplitDate[0];

                // Check Date
                //var CheckIsValidFromDate = spm.IsCheckDate("CheckProjectDate", getFromDate, empCode);
                //if (CheckIsValidFromDate != null)
                //{
                //    if (CheckIsValidFromDate.Rows.Count > 0)
                //    {
                //        var getMessage = Convert.ToString(CheckIsValidFromDate.Rows[0]["MESSAGE"].ToString());
                //        if (getMessage == "Exist")
                //        {
                //            Label4.Text = "From date cannot be exist in date range";
                //            txt_PD_FromDate.Text = "";
                //            return;
                //        }
                //    }
                //}
                if (chk_PD_IsCurrentProject.Checked == false)
                {
                    var tempToDate = Convert.ToString(txt_PD_ToDate.Text).Trim();
                    tempSplitDate = tempToDate.Split('/');
                    getToDate = tempSplitDate[2] + "-" + tempSplitDate[1] + "-" + tempSplitDate[0];
                    //var CheckIsValidToDate = spm.IsCheckDate("CheckProjectDate", getToDate, empCode);
                    //if (CheckIsValidToDate != null)
                    //{
                    //    if (CheckIsValidToDate.Rows.Count > 0)
                    //    {
                    //        var getMessage = Convert.ToString(CheckIsValidToDate.Rows[0]["MESSAGE"].ToString());
                    //        if (getMessage == "Exist")
                    //        {
                    //            Label4.Text = "To date cannot be exist in date range";
                    //            //Label4.Text = "To date cannot be exist in date range";
                    //            txt_PD_ToDate.Text = "";
                    //            return;
                    //        }
                    //    }
                    //}
                }
                var getRoleId = int.Parse(Convert.ToString(ddl_PD_Role.SelectedValue).Trim());
                var getModuleId = int.Parse(Convert.ToString(ddl_PD_Module.SelectedValue).Trim());
                var getIsCurrentProject = chk_PD_IsCurrentProject.Checked;
                var getProductImplemented = Convert.ToString(txt_PD_ProductImplemented.Text).Trim();
                var getOrganationName = Convert.ToString(ddl_PD_OrgName.SelectedValue).Trim();
                var getOrganationType = Convert.ToInt32(ddl_PD_OrgType.SelectedValue); 
                var getRegionId = Convert.ToInt32(ddl_Region.SelectedValue); 

                var getStatus = spm.InsertUpdateProjectDetails(0, empCode, getProjectTypeId, getClientName, getIndustryType, getSummary, getFromDate, getToDate, getIsCurrentProject, getProductImplemented, getOrganationName, getRoleId, getModuleId, getOrganationType, getRegionId);
                if (getStatus == true)
                {
                    SummaryofExperience();
                    ClearProjectDetails();
                    
                }
                else
                {
                    Label4.Text = "Something went wrong. Please try again";
                    return;
                }
            }
            else if (btn.Text == "Update")
            {
                if (Convert.ToString(ddl_PD_ProjectType.SelectedValue).Trim() == "0")
                {
                    Label4.Text = "Please select project type.";
                    return;
                }
                var getSelectedIType = false;
                var getIndustryType = "";
                foreach (ListItem item in lst_PD_IndustryType.Items)
                {
                    if (item.Selected)
                    {
                        if (item.Value != "" && item.Value != "0")
                        {
                            getSelectedIType = true;
                            if (getIndustryType == "")
                            {
                                getIndustryType = item.Value;
                            }
                            else
                            {
                                getIndustryType = getIndustryType + "," + item.Value;
                            }
                        }
                    }
                }
                if (getSelectedIType == false)
                {
                    Label4.Text = "Please select industry type.";
                    return;
                }
                if (Convert.ToString(ddl_PD_Role.SelectedValue).Trim() == "0")
                {
                    Label4.Text = "Please select role.";
                    return;
                }
                if (Convert.ToString(ddl_PD_Module.SelectedValue).Trim() == "0")
                {
                    Label4.Text = "Please select module.";
                    return;
                }

                if (Convert.ToString(ddl_PD_OrgName.SelectedValue).Trim() == "0")
                {
                    Label4.Text = "Please enter organisation name.";
                    return;
                }
                if (Convert.ToString(ddl_PD_OrgType.SelectedValue).Trim() == "0")
                {
                    Label4.Text = "Please enter organisation type.";
                    return;
                }
               
                if (Convert.ToString(txt_PD_ProductImplemented.Text).Trim() == "")
                {
                    Label4.Text = "Please enter product implemented.";
                    return;
                }
                if (Convert.ToString(txt_PD_Summary.Text).Trim() == "")
                {
                    Label4.Text = "Please enter brief summary of role.";
                    return;
                }
                if (Convert.ToString(txt_PD_ClientName.Text).Trim() == "")
                {
                    Label4.Text = "Please enter client name.";
                    return;
                }
                if (Convert.ToString(ddl_Region.SelectedValue).Trim() == "0")
                {
                    Label4.Text = "Please select region.";
                    return;
                }
                if (Convert.ToString(txt_PD_FromDate.Text).Trim() == "")
                {
                    Label4.Text = "Please select from date.";
                    return;
                }
                var getdate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime tempF1 = DateTime.ParseExact(txt_PD_FromDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime tempT1 = DateTime.ParseExact(getdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (tempT1 < tempF1)
                {
                    Label4.Text = "Future date not allowed.";
                    txt_PD_FromDate.Text = "";
                    return;
                }
                if (chk_PD_IsCurrentProject.Checked == false)
                {
                    
                    if (Convert.ToString(txt_PD_ToDate.Text).Trim() == "")
                    {
                        Label4.Text = "Please select to date.";
                        return;
                    }
                    
                    tempF1 = DateTime.ParseExact(txt_PD_ToDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (tempT1 < tempF1)
                    {
                        Label4.Text = "Future date not allowed.";
                        txt_PD_ToDate.Text = "";
                        return;
                    }

                    DateTime tempF = DateTime.ParseExact(txt_PD_FromDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime tempT = DateTime.ParseExact(txt_PD_ToDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (tempF >= tempT)
                    {
                        Label4.Text = "From date cannot be grater than to date";
                        txt_PD_ToDate.Text = "";
                        return;
                    }
                }
                var getProjectTypeId = int.Parse(Convert.ToString(ddl_PD_ProjectType.SelectedValue).Trim());

                var getClientName = Convert.ToString(txt_PD_ClientName.Text).Trim();
                //var getIndustryType = Convert.ToString(txt_PD_IndustryType.Text).Trim();
                var getSummary = Convert.ToString(txt_PD_Summary.Text).Trim();
                var empCode = Convert.ToString(hdnempcode.Value).Trim();
                var tempFromDate = Convert.ToString(txt_PD_FromDate.Text).Trim();
                var tempToDate = Convert.ToString(txt_PD_ToDate.Text).Trim();
                var getFromDate = "";
                var getToDate = "";
                var tempSplitDate = tempFromDate.Split('/');
                getFromDate = tempSplitDate[2] + "-" + tempSplitDate[1] + "-" + tempSplitDate[0];
                // Check Date
                var id = int.Parse(Convert.ToString(hdnProjectDetailID.Value).Trim());
                //var CheckIsValidFromDate = spm.IsCheckDateById("CheckProjectDateById", getFromDate, id, empCode);
                //if (CheckIsValidFromDate != null)
                //{
                //    if (CheckIsValidFromDate.Rows.Count > 0)
                //    {
                //        var getMessage = Convert.ToString(CheckIsValidFromDate.Rows[0]["MESSAGE"].ToString());
                //        if (getMessage == "Exist")
                //        {
                //            Label4.Text = "From date cannot be exist in date range";
                //            txt_PD_FromDate.Text = "";
                //            return;
                //        }
                //    }
                //}
                
                if (chk_PD_IsCurrentProject.Checked == false)
                {                   
                    tempSplitDate = tempToDate.Split('/');
                    getToDate = tempSplitDate[2] + "-" + tempSplitDate[1] + "-" + tempSplitDate[0];
                    
                    //var CheckIsValidToDate = spm.IsCheckDateById("CheckProjectDateById", getToDate, id,empCode);
                    //if (CheckIsValidToDate != null)
                    //{
                    //    if (CheckIsValidToDate.Rows.Count > 0)
                    //    {
                    //        var getMessage = Convert.ToString(CheckIsValidToDate.Rows[0]["MESSAGE"].ToString());
                    //        if (getMessage == "Exist")
                    //        {
                    //            Label4.Text = "To date cannot be exist in date range";
                    //            txt_PD_ToDate.Text = "";
                    //            return;
                    //        }
                    //    }
                    //}

                }
                var getRoleId = int.Parse(Convert.ToString(ddl_PD_Role.SelectedValue).Trim());
                var getModuleId = int.Parse(Convert.ToString(ddl_PD_Module.SelectedValue).Trim());
                var getIsCurrentProject = chk_PD_IsCurrentProject.Checked;
                var getProductImplemented = Convert.ToString(txt_PD_ProductImplemented.Text).Trim();
                var getOrganationName = Convert.ToString(ddl_PD_OrgName.SelectedValue).Trim();
                var getOrganationType = Convert.ToInt32(ddl_PD_OrgType.SelectedValue);             
                var getRegionId = Convert.ToInt32(ddl_Region.SelectedValue);             
                

                var getStatus = spm.InsertUpdateProjectDetails(id, empCode, getProjectTypeId, getClientName, getIndustryType, getSummary, getFromDate, getToDate, getIsCurrentProject, getProductImplemented, getOrganationName, getRoleId, getModuleId, getOrganationType, getRegionId);
                if (getStatus == true)
                {
                    SummaryofExperience();
                    ClearProjectDetails();
                }
                else
                {
                    Label4.Text = "Something went wrong. Please try again";
                    return;
                }
            }
            else if (btn.Text == "Cancel")
            {
                ClearProjectDetails();
            }
            else
            {
                Label4.Text = "Something went wrong. Please try again";
                return;
            }
        }
        catch (Exception ex)
        {
            Label4.Text = "Something went wrong. please try again";
            return;
        }
    }
    protected void lnk_PD_Edit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Label4.Text = "";
            var getSelectedValues = ddl_PD_ProjectType.SelectedValue.ToString();
            ddl_PD_ProjectType.Items.FindByValue(getSelectedValues).Selected = false;

            var getRole = ddl_PD_Role.SelectedValue.ToString();
            ddl_PD_Role.Items.FindByValue(getRole).Selected = false;
            var getModule = ddl_PD_Module.SelectedValue.ToString();
            ddl_PD_Module.Items.FindByValue(getModule).Selected = false;

            var getOrgName = ddl_PD_OrgName.SelectedValue.ToString();
            ddl_PD_OrgName.Items.FindByValue(getOrgName).Selected = false;

            var getOrgType = ddl_PD_OrgType.SelectedValue.ToString();
            ddl_PD_OrgType.Items.FindByValue(getOrgType).Selected = false;

            var getRegionIds = ddl_Region.SelectedValue.ToString();
            ddl_Region.Items.FindByValue(getRegionIds).Selected = false;

            foreach (ListItem itm in lst_PD_IndustryType.Items)
            {
                if (itm.Selected)
                {
                    itm.Selected = false;
                }
            }

            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(gv_ProjectDetails.DataKeys[row.RowIndex].Values[0]).Trim();
            if (fId != "0")
            {
                var getds = spm.getCVDetailById(int.Parse(fId), "getProjectDetailById");
                hdnProjectDetailID.Value = fId;
                if (getds != null)
                {
                    if (getds.Tables.Count > 0)
                    {
                        var getProjectDetail = getds.Tables[0];
                        if (getProjectDetail.Rows.Count > 0)
                        {
                            lnk_PD_Save.Visible = false;

                            txt_PD_ClientName.Text = getProjectDetail.Rows[0]["ClientName"].ToString();
                            txt_PD_IndustryType.Text = getProjectDetail.Rows[0]["IndustryType"].ToString();
                            txt_PD_Summary.Text = getProjectDetail.Rows[0]["BriefSummaryOfRole"].ToString();
                            txt_PD_FromDate.Text = getProjectDetail.Rows[0]["FromDate"].ToString();
                            txt_PD_ToDate.Text = getProjectDetail.Rows[0]["ToDate"].ToString();
                            //Add New Column
                            txt_PD_ProductImplemented.Text = getProjectDetail.Rows[0]["ProductImplemented"].ToString();
                            txt_PD_Organisation.Text = getProjectDetail.Rows[0]["OrganisationName"].ToString();

                            var getProjectTypeId = getProjectDetail.Rows[0]["ProjectTypeId"].ToString();
                            foreach (ListItem itm in ddl_PD_ProjectType.Items)
                            {
                                if (itm.Value == getProjectTypeId)
                                {
                                    itm.Selected = true;
                                }
                            }
                            var getRoleId = getProjectDetail.Rows[0]["RoleId"].ToString();
                            foreach (ListItem itm in ddl_PD_Role.Items)
                            {
                                if (itm.Value == getRoleId)
                                {
                                    itm.Selected = true;
                                }
                            }
                            var getModuleId = getProjectDetail.Rows[0]["ModuleId"].ToString();
                            foreach (ListItem itm in ddl_PD_Module.Items)
                            {
                                if (itm.Value == getModuleId)
                                {
                                    itm.Selected = true;
                                }
                            }
                            var getOrganisationNameId = getProjectDetail.Rows[0]["OrganisationName"].ToString();
                            foreach (ListItem itm in ddl_PD_OrgName.Items)
                            {
                                if (itm.Value == getOrganisationNameId)
                                {
                                    itm.Selected = true;
                                }
                            }
                            var getOrganisationTypeId = getProjectDetail.Rows[0]["OrganisationType"].ToString();
                            foreach (ListItem itm in ddl_PD_OrgType.Items)
                            {
                                if (itm.Value == getOrganisationTypeId)
                                {
                                    itm.Selected = true;
                                }
                            }
                            var getIndustryType = getProjectDetail.Rows[0]["IndustryType"].ToString();
                            if(getIndustryType!=null&& getIndustryType!="")
                            {
                                var splitType = getIndustryType.Split(',');
                                foreach (var item in splitType)
                                {
                                    var getV = item.ToString();
                                    foreach (ListItem itm in lst_PD_IndustryType.Items)
                                    {
                                        if (itm.Value == getV)
                                        {
                                            itm.Selected = true;
                                        }
                                    }
                                }
                            }

                            var getRegionId = getProjectDetail.Rows[0]["RegionId"].ToString();
                            foreach (ListItem itm in ddl_Region.Items)
                            {
                                if (itm.Value == getRegionId)
                                {
                                    itm.Selected = true;
                                }
                            }

                            var isCompleted = false;
                            if (getProjectDetail.Rows[0]["IsCurrentProject"].ToString() != "")
                            {
                                isCompleted = Convert.ToBoolean(getProjectDetail.Rows[0]["IsCurrentProject"].ToString());
                                chk_PD_IsCurrentProject.Checked = Convert.ToBoolean(getProjectDetail.Rows[0]["IsCurrentProject"].ToString());
                            }

                            if (isCompleted == true)
                            {
                                //txt_PD_FromDate.Enabled = false;
                                txt_PD_ToDate.Enabled = false;
                            }

                            lnk_PD_Update.Visible = true;
                            lnk_PD_Cancel.Visible = true;
                        }
                        else
                        {

                        }
                    }
                }
            }
            else
            {
                Label4.Text = "Something went wrong.";
            }
        }
        catch (Exception ex)
        {
            Label4.Text = ex.Message;
        }
    }
    private void ClearProjectDetails()
    {
        lnk_PD_Update.Visible = false;
        lnk_PD_Cancel.Visible = false;
        lnk_PD_Save.Visible = true;

        txt_PD_ToDate.Text = "";
        txt_PD_FromDate.Text = "";
        txt_PD_Summary.Text = "";
        txt_PD_IndustryType.Text = "";
        txt_PD_ClientName.Text = "";
        txt_PD_FromDate.Enabled = true;
        txt_PD_ToDate.Enabled = true;
        chk_PD_IsCurrentProject.Checked = false;
        txt_PD_ProductImplemented.Text = "";
        txt_PD_Organisation.Text = "";
        hdnProjectDetailID.Value = "0";
        BindEmpDetails(Convert.ToString(Session["Empcode"]), "Project");
    }
    // End Project Details 
    //Domain Exp Details
    protected void lnk_DE_Save_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            Label6.Text = "";
            LinkButton btn = (LinkButton)sender;
            if (btn.Text == "Save")
            {
                if (Convert.ToString(txt_TotalDomainExp.Text).Trim() == "")
                {
                    Label6.Text = "Please enter total domain experience.";
                    return;
                }
                if (Convert.ToString(txt_TotalSAPExp.Text).Trim() == "")
                {
                    Label6.Text = "Please enter total sap experience.";
                    return;
                }
                if (Convert.ToString(txt_TotalOverallExp.Text).Trim() == "")
                {
                    Label6.Text = "Please enter total overall experience.";
                    return;
                }
                var getTotalExp = (Convert.ToString(txt_TotalDomainExp.Text).Trim());
                var getTotalSAPExp = (Convert.ToString(txt_TotalSAPExp.Text).Trim());
                var getTotalOverAllExp = (Convert.ToString(txt_TotalOverallExp.Text).Trim());
                var getEMP = Convert.ToString(hdnempcode.Value).Trim();
                var result = spm.InsertUpdateTotalEXPDetails(getEMP, getTotalExp, getTotalSAPExp, getTotalOverAllExp,"","");
                if (result == true)
                {
                    BindEmpDetails(Convert.ToString(Session["Empcode"]), "All");
                }
                else
                {
                    Label6.Text = "Something went wrong.";
                    return;
                }
            }
            else if (btn.Text == "Update")
            {
                if (Convert.ToString(txt_TotalDomainExp.Text).Trim() == "")
                {
                    Label6.Text = "Please enter total domain experience.";
                    return;
                }
                if (Convert.ToString(txt_TotalSAPExp.Text).Trim() == "")
                {
                    Label6.Text = "Please enter total sap experience.";
                    return;
                }
                if (Convert.ToString(txt_TotalOverallExp.Text).Trim() == "")
                {
                    Label6.Text = "Please enter total overall experience.";
                    return;
                }
                var getTotalExp = (Convert.ToString(txt_TotalDomainExp.Text).Trim());
                var getTotalSAPExp = (Convert.ToString(txt_TotalSAPExp.Text).Trim());
                var getTotalOverAllExp = (Convert.ToString(txt_TotalOverallExp.Text).Trim());
                var getEMP = Convert.ToString(hdnempcode.Value).Trim();
                var result = spm.InsertUpdateTotalEXPDetails(getEMP, getTotalExp, getTotalSAPExp, getTotalOverAllExp,"","");
                if (result == true)
                {
                    BindEmpDetails(Convert.ToString(Session["Empcode"]), "All");
                }
                else
                {
                    Label6.Text = "Something went wrong.";
                    return;
                }
            }
            else
            {
                Label6.Text = "Action not found";
            }
        }
        catch (Exception ex)
        {
            Label6.Text = ex.Message;
        }
    }
    protected void txt_DD_ToDate_TextChanged(object sender, EventArgs e)
    {

    }
    protected void lnk_DD_Save_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            Label5.Text = "";
            LinkButton btn = (LinkButton)sender;
            if (btn.Text == "Save")
            {
                if (Convert.ToString(ddl_DD_OrgName.SelectedValue).Trim() == "0")
                {
                    Label5.Text = "Please select organisation name.";
                    return;
                }
                if (Convert.ToString(ddl_DD_OrgType.SelectedValue).Trim() == "0")
                {
                    Label5.Text = "Please select organisation type.";
                    return;
                }
                if (Convert.ToString(ddl_DD_Domain.SelectedValue).Trim() == "0")
                {
                    Label5.Text = "Please select domain name.";
                    return;
                }
                var getSelectedIType = false;
                var getIndustryType = "";
                foreach (ListItem item in lst_DD_IndustryType.Items)
                {
                    if (item.Selected)
                    {
                        if (item.Value != "" && item.Value != "0")
                        {
                            getSelectedIType = true;
                            if (getIndustryType == "")
                            {
                                getIndustryType = item.Value;
                            }
                            else
                            {
                                getIndustryType = getIndustryType + "," + item.Value;
                            }
                        }
                    }
                }
                if (getSelectedIType == false)
                {
                    Label4.Text = "Please select industry type.";
                    return;
                }
                //if (Convert.ToString(txt_DD_IndustryType.Text).Trim() == "")
                //{
                //    Label5.Text = "Please enter industry type.";
                //    return;
                //}
                if (Convert.ToString(ddl_DD_Role.SelectedValue).Trim() == "0")
                {
                    Label5.Text = "Please select role name.";
                    return;
                }
                if (Convert.ToString(txt_DD_Responsibilities.Text).Trim() == "")
                {
                    Label5.Text = "Please enter responsibilities.";
                    return;
                }
                if (Convert.ToString(txt_DD_FromDate.Text).Trim() == "")
                {
                    Label5.Text = "Please select from date.";
                    return;
                }
                var getdate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime tempF1 = DateTime.ParseExact(txt_DD_FromDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime tempT1 = DateTime.ParseExact(getdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (tempT1 < tempF1)
                {
                    Label5.Text = "Future date not allowed.";
                    txt_DD_FromDate.Text = "";
                    return;
                }
                if (chk_DD_IsComplited.Checked==false)
                {                    
                    if (Convert.ToString(txt_DD_ToDate.Text).Trim() == "")
                    {
                        Label5.Text = "Please select to date.";
                        return;
                    }                   
                    tempF1 = DateTime.ParseExact(txt_DD_ToDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (tempT1 < tempF1)
                    {
                        Label5.Text = "Future date not allowed.";
                        txt_DD_ToDate.Text = "";
                        return;
                    }

                    DateTime tempF = DateTime.ParseExact(txt_DD_FromDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime tempT = DateTime.ParseExact(txt_DD_ToDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (tempF >= tempT)
                    {
                        Label5.Text = "From date cannot be grater than to date";
                        txt_DD_ToDate.Text = "";
                        return;
                    }
                }
                

                var getOrganisation = Convert.ToString(ddl_DD_OrgName.SelectedValue).Trim();
                var getOrganisationType = Convert.ToInt32(ddl_DD_OrgType.SelectedValue);
                var getDomain = Convert.ToString(ddl_DD_Domain.SelectedValue).Trim();
                var getRoleId = Convert.ToInt32(ddl_DD_Role.SelectedValue);
                var getSummary = Convert.ToString(txt_DD_Responsibilities.Text).Trim();

                var tempFromDate = Convert.ToString(txt_DD_FromDate.Text).Trim();
                var tempToDate = Convert.ToString(txt_DD_ToDate.Text).Trim();
                var getIsComplite = chk_DD_IsComplited.Checked;
                var empCode = Convert.ToString(hdnempcode.Value).Trim();
                var getFromDate = "";
                var getToDate = "";
                var tempSplitDate = tempFromDate.Split('/');
                getFromDate = tempSplitDate[2] + "-" + tempSplitDate[1] + "-" + tempSplitDate[0];
                // Check Date
                //var CheckIsValidFromDate = spm.IsCheckDate("CheckDomainDate", getFromDate, empCode);
                //if (CheckIsValidFromDate != null)
                //{
                //    if (CheckIsValidFromDate.Rows.Count > 0)
                //    {
                //        var getMessage = Convert.ToString(CheckIsValidFromDate.Rows[0]["MESSAGE"].ToString());
                //        if (getMessage == "Exist")
                //        {
                //            Label5.Text = "From date cannot be exist in date range";
                //            txt_DD_FromDate.Text = "";
                //            return;
                //        }
                //    }
                //}
                if (getIsComplite==false)
                {                    
                    tempSplitDate = tempToDate.Split('/');
                    getToDate = tempSplitDate[2] + "-" + tempSplitDate[1] + "-" + tempSplitDate[0];                   

                    
                    //var CheckIsValidToDate = spm.IsCheckDate("CheckDomainDate", getToDate, empCode);
                    //if (CheckIsValidToDate != null)
                    //{
                    //    if (CheckIsValidToDate.Rows.Count > 0)
                    //    {
                    //        var getMessage = Convert.ToString(CheckIsValidToDate.Rows[0]["MESSAGE"].ToString());
                    //        if (getMessage == "Exist")
                    //        {
                    //            Label5.Text = "To date cannot be exist in date range";
                    //            txt_DD_ToDate.Text = "";
                    //            return;
                    //        }
                    //    }
                    //}
                }               


                var getStatus = spm.InsertUpdateDomainDetails(0, empCode, getOrganisation, getDomain, getIndustryType, getSummary, getFromDate, getToDate, getRoleId, getOrganisationType, getIsComplite);
                if (getStatus == true)
                {
                    SummaryofExperience();
                    ClearDomainDetails();
                }
                else
                {
                    Label5.Text = "Something went wrong. Please try again";
                    return;
                }
            }
            else if (btn.Text == "Update")
            {
                if (Convert.ToString(ddl_DD_OrgName.SelectedValue).Trim() == "0")
                {
                    Label5.Text = "Please select organisation name.";
                    return;
                }
                if (Convert.ToString(ddl_DD_OrgType.SelectedValue).Trim() == "0")
                {
                    Label5.Text = "Please select organisation type.";
                    return;
                }
                if (Convert.ToString(ddl_DD_Domain.SelectedValue).Trim() == "0")
                {
                    Label5.Text = "Please select domain name.";
                    return;
                }
                var getSelectedIType = false;
                var getIndustryType = "";
                foreach (ListItem item in lst_DD_IndustryType.Items)
                {
                    if (item.Selected)
                    {
                        if (item.Value != "" && item.Value != "0")
                        {
                            getSelectedIType = true;
                            if (getIndustryType == "")
                            {
                                getIndustryType = item.Value;
                            }
                            else
                            {
                                getIndustryType = getIndustryType + "," + item.Value;
                            }
                        }
                    }
                }
                if (getSelectedIType == false)
                {
                    Label4.Text = "Please select industry type.";
                    return;
                }
                //if (Convert.ToString(txt_DD_IndustryType.Text).Trim() == "")
                //{
                //    Label5.Text = "Please enter industry type.";
                //    return;
                //}
                if (Convert.ToString(ddl_DD_Role.SelectedValue).Trim() == "0")
                {
                    Label5.Text = "Please select role name.";
                    return;
                }
                if (Convert.ToString(txt_DD_Responsibilities.Text).Trim() == "")
                {
                    Label5.Text = "Please enter responsibilities.";
                    return;
                }
                if (Convert.ToString(txt_DD_FromDate.Text).Trim() == "")
                {
                    Label5.Text = "Please select from date.";
                    return;
                }
                var getdate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime tempF1 = DateTime.ParseExact(txt_DD_FromDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime tempT1 = DateTime.ParseExact(getdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (tempT1 < tempF1)
                {
                    Label5.Text = "Future date not allowed.";
                    txt_DD_FromDate.Text = "";
                    return;
                }
                if (chk_DD_IsComplited.Checked==false)
                {
                    
                    if (Convert.ToString(txt_DD_ToDate.Text).Trim() == "")
                    {
                        Label5.Text = "Please select to date.";
                        return;
                    }

                   
                    tempF1 = DateTime.ParseExact(txt_DD_ToDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (tempT1 < tempF1)
                    {
                        Label5.Text = "Future date not allowed.";
                        txt_DD_ToDate.Text = "";
                        return;
                    }

                    DateTime tempF = DateTime.ParseExact(txt_DD_FromDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime tempT = DateTime.ParseExact(txt_DD_ToDate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (tempF >= tempT)
                    {
                        Label5.Text = "From date cannot be grater than to date";
                        txt_DD_ToDate.Text = "";
                        return;
                    }
                }                

                var getOrganisation = Convert.ToString(ddl_DD_OrgName.SelectedValue).Trim();
                var getOrganisationType = Convert.ToInt32(ddl_DD_OrgType.SelectedValue);
                var getDomain = Convert.ToString(ddl_DD_Domain.SelectedValue).Trim();
                var getRoleId = Convert.ToInt32(ddl_DD_Role.SelectedValue);
                var getSummary = Convert.ToString(txt_DD_Responsibilities.Text).Trim();

                var tempFromDate = Convert.ToString(txt_DD_FromDate.Text).Trim();
                var tempToDate = Convert.ToString(txt_DD_ToDate.Text).Trim();
                var getIsComplite = chk_DD_IsComplited.Checked;
                var empCode = Convert.ToString(hdnempcode.Value).Trim();
                var id = int.Parse(Convert.ToString(hdnDomainDetailID.Value).Trim());
                var getFromDate = "";
                var getToDate = "";
                var tempSplitDate = tempFromDate.Split('/');
                getFromDate = tempSplitDate[2] + "-" + tempSplitDate[1] + "-" + tempSplitDate[0];
                // Check Date
                //var CheckIsValidFromDate = spm.IsCheckDateById("CheckDomainDateById", getFromDate, id, empCode);
                //if (CheckIsValidFromDate != null)
                //{
                //    if (CheckIsValidFromDate.Rows.Count > 0)
                //    {
                //        var getMessage = Convert.ToString(CheckIsValidFromDate.Rows[0]["MESSAGE"].ToString());
                //        if (getMessage == "Exist")
                //        {
                //            Label5.Text = "From date cannot be exist in date range";
                //            txt_DD_FromDate.Text = "";
                //            return;
                //        }
                //    }
                //}
                if (getIsComplite==false)
                {                   
                    tempSplitDate = tempToDate.Split('/');
                    getToDate = tempSplitDate[2] + "-" + tempSplitDate[1] + "-" + tempSplitDate[0];    

                    //var CheckIsValidToDate = spm.IsCheckDateById("CheckDomainDateById", getToDate, id, empCode);
                    //if (CheckIsValidToDate != null)
                    //{
                    //    if (CheckIsValidToDate.Rows.Count > 0)
                    //    {
                    //        var getMessage = Convert.ToString(CheckIsValidToDate.Rows[0]["MESSAGE"].ToString());
                    //        if (getMessage == "Exist")
                    //        {
                    //            Label5.Text = "To date cannot be exist in date range";
                    //            txt_DD_ToDate.Text = "";
                    //            return;
                    //        }
                    //    }
                    //}
                }              
                var getStatus = spm.InsertUpdateDomainDetails(id, empCode, getOrganisation, getDomain, getIndustryType, getSummary, getFromDate, getToDate, getRoleId, getOrganisationType, getIsComplite);
                if (getStatus == true)
                {
                    SummaryofExperience();
                    ClearDomainDetails();
                }
                else
                {
                    Label5.Text = "Something went wrong. Please try again";
                    return;
                }
            }
            else if (btn.Text == "Cancel")
            {
                ClearDomainDetails();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            Label5.Text = "Something went wrong. please try again";
            return;
        }
    }
    protected void lnk_DD_Edit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            var getDomain = ddl_DD_Domain.SelectedValue.ToString();
            ddl_DD_Domain.Items.FindByValue(getDomain).Selected = false;

            var getRoleId = ddl_DD_Role.SelectedValue.ToString();
            ddl_DD_Role.Items.FindByValue(getRoleId).Selected = false;
            var getOrgName = ddl_DD_OrgName.SelectedValue.ToString();
            ddl_DD_OrgName.Items.FindByValue(getOrgName).Selected = false;
            var getOrgType = ddl_DD_OrgType.SelectedValue.ToString();
            ddl_DD_OrgType.Items.FindByValue(getOrgType).Selected = false;

            foreach (ListItem itm in lst_DD_IndustryType.Items)
            {
                if (itm.Selected)
                {
                    itm.Selected = false;
                }
            }

            Label5.Text = "";
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(gv_DomainDetails.DataKeys[row.RowIndex].Values[0]).Trim();
            if (fId != "0")
            {
                var getds = spm.getCVDetailById(int.Parse(fId), "getDomainDetailById");
                hdnDomainDetailID.Value = fId;
                if (getds != null)
                {
                    if (getds.Tables.Count > 0)
                    {
                        var getDomainDetail = getds.Tables[0];
                        if (getDomainDetail.Rows.Count > 0)
                        {
                            lnk_DD_Save.Visible = false;

                            txt_DD_Organisation.Text = getDomainDetail.Rows[0]["OrganisationName"].ToString();
                            txt_DD_Domain.Text = getDomainDetail.Rows[0]["Domain"].ToString();
                            txt_DD_IndustryType.Text = getDomainDetail.Rows[0]["IndustryType"].ToString();

                            txt_DD_Responsibilities.Text = getDomainDetail.Rows[0]["RoleAndResponsibilities"].ToString();
                            txt_DD_FromDate.Text = getDomainDetail.Rows[0]["FromDate"].ToString();
                            txt_DD_ToDate.Text = getDomainDetail.Rows[0]["ToDate"].ToString();


                            var getRoleIds = getDomainDetail.Rows[0]["RoleId"].ToString();
                            if(getRoleIds!=null&& getRoleIds!="")
                            {
                                foreach (ListItem itm in ddl_DD_Role.Items)
                                {
                                    if (itm.Value == getRoleIds)
                                    {
                                        itm.Selected = true;
                                    }
                                }
                            }                            

                            var getDomainIds = getDomainDetail.Rows[0]["DomainId"].ToString();
                            if(getDomainIds!=null&& getDomainIds!="")
                            {
                                foreach (ListItem itm in ddl_DD_Domain.Items)
                                {
                                    if (itm.Value == getDomainIds)
                                    {
                                        itm.Selected = true;
                                    }
                                }
                            }
                            

                            var getOrgNameId = getDomainDetail.Rows[0]["OrganisationNameId"].ToString();
                            if(getOrgNameId!=""&& getOrgNameId!=null)
                            {
                                foreach (ListItem itm in ddl_DD_OrgName.Items)
                                {
                                    if (itm.Value == getOrgNameId)
                                    {
                                        itm.Selected = true;
                                    }
                                }
                            }                            

                            var getOrgTypeId = getDomainDetail.Rows[0]["OrganisationTypeId"].ToString();
                            if(getOrgTypeId!=null&& getOrgTypeId!="")
                            {
                                foreach (ListItem itm in ddl_DD_OrgType.Items)
                                {
                                    if (itm.Value == getOrgTypeId)
                                    {
                                        itm.Selected = true;
                                    }
                                }
                            }
                           
                            var isCompleted = false;
                            if (getDomainDetail.Rows[0]["IsPreviousExp"].ToString() != "")
                            {
                                isCompleted = Convert.ToBoolean(getDomainDetail.Rows[0]["IsPreviousExp"].ToString());
                                chk_DD_IsComplited.Checked = Convert.ToBoolean(getDomainDetail.Rows[0]["IsPreviousExp"].ToString());
                            }

                            var getIndustryType = getDomainDetail.Rows[0]["IndustryType"].ToString();
                            if(getIndustryType!=null&& getIndustryType!="")
                            {
                                var splitType = getIndustryType.Split(',');
                                foreach (var item in splitType)
                                {
                                    var getV = item.ToString();
                                    foreach (ListItem itm in lst_DD_IndustryType.Items)
                                    {
                                        if (itm.Value == getV)
                                        {
                                            itm.Selected = true;
                                        }
                                    }
                                }
                            }
                            
                            if(isCompleted==true)
                            {
                               // txt_DD_FromDate.Enabled = false;
                                txt_DD_ToDate.Enabled = false;
                            }

                            lnk_DD_Update.Visible = true;
                            lnk_DD_Cancel.Visible = true;
                        }
                        else
                        {

                        }
                    }
                }
            }
            else
            {
                Label5.Text = "Something went wrong.";
            }
        }
        catch (Exception ex)
        {
            Label5.Text = ex.Message;
        }
    }
    private void ClearDomainDetails()
    {
        lnk_DD_Update.Visible = false;
        lnk_DD_Cancel.Visible = false;
        lnk_DD_Save.Visible = true;

        txt_DD_Organisation.Text = "";
        txt_DD_Domain.Text = "";
        txt_DD_IndustryType.Text = "";
        txt_DD_Responsibilities.Text = "";
        txt_DD_FromDate.Text = "";
        txt_DD_ToDate.Text = "";
        hdnDomainDetailID.Value = "0";
        BindEmpDetails(Convert.ToString(Session["Empcode"]), "Domain");
    }
    //End Domain Details

    protected void chk_ED_Iscompleted_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            var getVal = chk_ED_Iscompleted.Checked;
            if (getVal == true)
            {
                txt_GradeMarks.Enabled = true;
                txt_YearOfPassing.Enabled = true;
                txt_TotalMark.Enabled = true;
            }
            else
            {
                txt_GradeMarks.Enabled = false;
                txt_YearOfPassing.Enabled = false;
                txt_TotalMark.Enabled = false;

                txt_GradeMarks.Text = "";
                txt_YearOfPassing.Text = "";
                txt_TotalMark.Text = "";
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void chk_CD_Isompleted_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            var getVal = chk_CD_Isompleted.Checked;
            if (getVal == true)
            {
                txt_CD_FromDate.Enabled = true;
                txt_CD_ToDate.Enabled = true;
                txt_CD_CertificationNo.Enabled = true;
            }
            else
            {
                txt_CD_FromDate.Enabled = false;
                txt_CD_ToDate.Enabled = false;
                txt_CD_CertificationNo.Enabled = false;

                txt_CD_FromDate.Text = "";
                txt_CD_ToDate.Text = "";
                txt_CD_CertificationNo.Text = "";
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void chk_PD_IsCurrentProject_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            var getVal = chk_PD_IsCurrentProject.Checked;
            if (getVal == false)
            {
                //txt_PD_FromDate.Enabled = true;
                txt_PD_ToDate.Enabled = true;
                //txt_PD_FromDate.Text = "";
                txt_PD_ToDate.Text = "";
            }
            else
            {
                //var CheckIsCurrentProject = spm.IsCheckDate("CheckIsCurrentProject", "", Convert.ToString(Session["Empcode"]));
                //if (CheckIsCurrentProject != null)
                //{
                //    if (CheckIsCurrentProject.Rows.Count > 0)
                //    {
                //        var getMessage = Convert.ToString(CheckIsCurrentProject.Rows[0]["MESSAGE"].ToString());
                //        if (getMessage == "Exist")
                //        {
                //            //Label4.Text = "To date cannot be exist in date range";
                //            Label4.Text = "Only One Project can be Saved as Current Project";
                //            // txt_PD_ToDate.Text = "";
                //            chk_PD_IsCurrentProject.Checked = false;
                //            return;
                //        }
                //    }
                //}
                // txt_PD_FromDate.Enabled = false;
                txt_PD_ToDate.Enabled = false;
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void lnk_FD_Delete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(dg_FimalyDetails.DataKeys[row.RowIndex].Values[0]).Trim();
            if (fId != null)
            {
                var id = int.Parse(fId);
                var result = spm.DeleteCVDetails(id, "DeleteFamilyDetails");
                if (result == true)
                {
                    ResetFamilyDetails();
                }
                else
                {
                    Label1.Text = "Something went wrong , Please try again.";
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message.ToString();
            return;
        }
    }

    protected void lnk_ED_Delete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(gv_EducationDetails.DataKeys[row.RowIndex].Values[0]).Trim();
            if (fId != null)
            {
                var id = int.Parse(fId);
                var result = spm.DeleteCVDetails(id, "DeleteEducationDetails");
                if (result == true)
                {
                    ClearEduactionDetails();
                }
                else
                {
                    Label2.Text = "Something went wrong , Please try again.";
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Label2.Text = ex.Message.ToString();
            return;
        }
    }

    protected void lnk_CD_Delete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(gv_CertificationDetails.DataKeys[row.RowIndex].Values[0]).Trim();
            if (fId != null)
            {
                var id = int.Parse(fId);
                var result = spm.DeleteCVDetails(id, "DeleteCertificationDetails");
                if (result == true)
                {
                    ClearCertificationDetails();
                }
                else
                {
                    Label3.Text = "Something went wrong , Please try again.";
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Label3.Text = ex.Message.ToString();
            return;
        }
    }

    protected void lnk_PD_Delete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(gv_ProjectDetails.DataKeys[row.RowIndex].Values[0]).Trim();
            if (fId != null)
            {
                var id = int.Parse(fId);
                var result = spm.DeleteCVDetails(id, "DeleteProjectDetails");
                if (result == true)
                {
                    ClearProjectDetails();
                }
                else
                {
                    Label4.Text = "Something went wrong , Please try again.";
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Label4.Text = ex.Message.ToString();
            return;
        }
    }

    protected void lnk_DD_Delete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(gv_DomainDetails.DataKeys[row.RowIndex].Values[0]).Trim();
            if (fId != null)
            {
                var id = int.Parse(fId);
                var result = spm.DeleteCVDetails(id, "DeleteDomainDetails");
                if (result == true)
                {
                    ClearDomainDetails();
                }
                else
                {
                    Label5.Text = "Something went wrong , Please try again.";
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Label5.Text = ex.Message.ToString();
            return;
        }
    }

    protected void chk_DD_IsComplited_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            var getVal = chk_DD_IsComplited.Checked;
            if (getVal == false)
            {
                //txt_DD_FromDate.Enabled = true;
                txt_DD_ToDate.Enabled = true;
                //txt_DD_FromDate.Text = "";
                txt_DD_ToDate.Text = "";
            }
            else
            {
                //var CheckIsCurrentProject = spm.IsCheckDate("CheckIsCurrentDomain", "", Convert.ToString(Session["Empcode"]));
                //if (CheckIsCurrentProject != null)
                //{
                //    if (CheckIsCurrentProject.Rows.Count > 0)
                //    {
                //        var getMessage = Convert.ToString(CheckIsCurrentProject.Rows[0]["MESSAGE"].ToString());
                //        if (getMessage == "Exist")
                //        {
                //            //Label5.Text = "To date cannot be exist in date range";
                //            Label5.Text = "Only One domain details can be Saved as Current";
                //            // txt_PD_ToDate.Text = "";
                //            chk_DD_IsComplited.Checked = false;
                //            return;
                //        }
                //    }
                //}
                // txt_DD_FromDate.Enabled = false;
                txt_DD_ToDate.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    //Document Details
    private void ClearDocumentDetails()
    {
        lnk_FileUpdate.Visible = false;
        lnk_FileCancel.Visible = false;
        lnk_FileSave.Visible = true;
        hdnFileDetailID.Value = "0";
        BindEmpDetails(Convert.ToString(Session["Empcode"]), "Document");
    }
    protected void lnk_FileSave_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            Label7.Text = "";
            var filename = "";
            LinkButton btn = (LinkButton)sender;
            if (btn.Text == "Save")
            {
                if (Convert.ToString(ddl_DocumentName.SelectedValue).Trim() == "0")
                {
                    Label7.Text = "Please select document type.";
                    return;
                }
                if (uploadfile.HasFile)
                {
                    filename = uploadfile.FileName;
                }
                if (Convert.ToString(filename).Trim() == "")
                {
                    Label7.Text = "Please select file";
                    return;
                }
                var documentType = Convert.ToInt32(ddl_DocumentName.SelectedValue);
                var empCode = Convert.ToString(hdnempcode.Value).Trim();
                var fileName = filename;
                var filePath = "";
                if (Convert.ToString(filename).Trim() != "")
                {
                    //var docName = Convert.ToString(ddl_DocumentName.SelectedItem.Text);

                    //var getdate = Convert.ToDateTime(Txt_DateCreate.Text);
                    DateTime loadedDate = DateTime.Now;
                    var strfromDate = loadedDate.ToString("ddMMyyyyHHmm");
                    filename = uploadfile.FileName;
                    var strfileName = "";
                    strfileName = empCode + "_DT_ID_" + documentType + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
                    filePath = strfileName;
                    uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["EmployeeCVDCFilePath"]).Trim()), strfileName));
                }

                var getStatus = spm.InsertUpdateDocumentDetails(0, empCode, fileName, filePath, documentType, true);
                if (getStatus == true)
                {
                    ClearDocumentDetails();
                }
                else
                {
                    Label7.Text = "Something went wrong. Please try again";
                    return;
                }
            }
            else if (btn.Text == "Update")
            {
                if (Convert.ToString(ddl_DocumentName.SelectedValue).Trim() == "0")
                {
                    Label7.Text = "Please select document type.";
                    return;
                }
                if (uploadfile.HasFile)
                {
                    filename = uploadfile.FileName;
                }

                var documentType = Convert.ToInt32(ddl_DocumentName.SelectedValue);
                var empCode = Convert.ToString(hdnempcode.Value).Trim();
                var fileName = filename;
                var filePath = "";
                if (Convert.ToString(filename).Trim() != "")
                {
                    // Delete Exist FIle
                    var tempFile = hdnFilePath.Value;
                    string path = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["EmployeeCVDCFilePath"]).Trim()), tempFile);
                    FileInfo file = new FileInfo(path);
                    if (file.Exists)//check file exsit or not  
                    {
                        file.Delete();
                    }
                    //End 
                    //var getdate = Convert.ToDateTime(Txt_DateCreate.Text);
                    DateTime loadedDate = DateTime.Now;
                    var strfromDate = loadedDate.ToString("ddMMyyyyHHmm");
                    filename = uploadfile.FileName;
                    var strfileName = "";
                    strfileName = empCode + "_DT_ID_" + documentType + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
                    filePath = strfileName;
                    uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["EmployeeCVDCFilePath"]).Trim()), strfileName));
                }
                else
                {
                    filename = hdnFileName.Value;
                    filePath = hdnFilePath.Value;
                }
                var id = Convert.ToInt32(hdnFileDetailID.Value);
                var getStatus = spm.InsertUpdateDocumentDetails(id, empCode, fileName, filePath, documentType, true);
                if (getStatus == true)
                {
                    ClearDocumentDetails();
                }
                else
                {
                    Label7.Text = "Something went wrong. Please try again";
                    return;
                }
            }
            else if (btn.Text == "Cancel")
            {
                ClearDomainDetails();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            Label5.Text = "Something went wrong. please try again";
            return;
        }
    }
    protected void lnk_File_Edit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            var ddlDocumentId = ddl_DocumentName.SelectedValue.ToString();
            ddl_DocumentName.Items.FindByValue(ddlDocumentId).Selected = false;

            Label5.Text = "";
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(gv_Documents.DataKeys[row.RowIndex].Values[0]).Trim();
            if (fId != "0")
            {
                var getds = spm.getCVDetailById(int.Parse(fId), "GetDocumentDetailsBYId");
                hdnFileDetailID.Value = fId;
                if (getds != null)
                {
                    if (getds.Tables.Count > 0)
                    {
                        var getDomainDetail = getds.Tables[0];
                        if (getDomainDetail.Rows.Count > 0)
                        {
                            lnk_FileSave.Visible = false;

                            hdnFileName.Value = getDomainDetail.Rows[0]["FileName"].ToString();
                            hdnFilePath.Value = getDomainDetail.Rows[0]["FilePath"].ToString();
                            var getDocumentTypeId = getDomainDetail.Rows[0]["DocumentTypeId"].ToString();
                            foreach (ListItem itm in ddl_DocumentName.Items)
                            {
                                if (itm.Value == getDocumentTypeId)
                                {
                                    itm.Attributes.Add("disabled", "");
                                    itm.Selected = true;

                                }
                            }
                            lnk_FileUpdate.Visible = true;
                            lnk_FileCancel.Visible = true;
                        }
                        else
                        {

                        }
                    }
                }
            }
            else
            {
                Label5.Text = "Something went wrong.";
            }
        }
        catch (Exception ex)
        {
            Label5.Text = ex.Message;
        }
    }
    protected void lnk_File_Delete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(gv_Documents.DataKeys[row.RowIndex].Values[0]).Trim();
            var FilePath = Convert.ToString(gv_Documents.DataKeys[row.RowIndex].Values[1]).Trim();
            if (fId != null)
            {
                var id = int.Parse(fId);
                var result = spm.DeleteCVDetails(id, "DeleteDocumentDetails");
                if (result == true)
                {

                    string path = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["EmployeeCVDCFilePath"]).Trim()), FilePath);
                    FileInfo file = new FileInfo(path);
                    if (file.Exists)//check file exsit or not  
                    {
                        file.Delete();
                    }
                    ClearDocumentDetails();
                }
                else
                {
                    Label2.Text = "Something went wrong , Please try again.";
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Label2.Text = ex.Message.ToString();
            return;
        }
    }

    //End Document Details
    //Final Submistion
    protected void lnk_FinalSubmit_Click(object sender, EventArgs e)
    {
        try
        {
           
            if (Convert.ToString(txtJobDescription.Text).Trim() == "")
            {
                Label8.Text = "Please enter profile summary.";
                BindEmpDetails(Convert.ToString(Session["Empcode"]), "Document");
                return;
            }
            var getEMP = Convert.ToString(hdnempcode.Value).Trim();
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            var getStatus = spm.IsCheckCVUpdate("CheckIsCVDone", getEMP);
            if(getStatus!=null)
            {
                if(getStatus.Rows.Count>0)
                {
                    var status= getStatus.Rows[0]["MESSAGE"].ToString();
                    if(status!="OK")
                    {
                        Label8.Text = status;
                        BindEmpDetails(Convert.ToString(Session["Empcode"]), "Document");
                        return;
                    }
                }
                else
                {
                    Label8.Text = "Something went wrong.";
                    BindEmpDetails(Convert.ToString(Session["Empcode"]), "Document");
                    return;
                }
            }

            Label8.Text = "";
            Label6.Text = "";
            var getTotalExp = (Convert.ToString(txt_TotalDomainExp.Text).Trim());
            var getTotalSAPExp = (Convert.ToString(txt_TotalSAPExp.Text).Trim());
            var getTotalOverAllExp = (Convert.ToString(txt_TotalOverallExp.Text).Trim());
            var getSabbatical_Educational_Break = (Convert.ToString(txt_Sabbatical_Educational_Break.Text).Trim());
            var descripation = Convert.ToString(txtJobDescription.Text).Trim();
            if (getTotalExp == "")
                getTotalExp = "0.0";

            if (getTotalSAPExp == "")
                getTotalSAPExp = "0.0";

            if (getTotalOverAllExp == "")
                getTotalOverAllExp = "0.0";

            if (getSabbatical_Educational_Break == "")
                getSabbatical_Educational_Break = "0.0";

            var result = spm.InsertUpdateTotalEXPDetails(getEMP, getTotalExp, getTotalSAPExp, getTotalOverAllExp, descripation, getSabbatical_Educational_Break);
            if (result == true)
            {
                //Update Profile
                var PersonalEmailID = Convert.ToString(txt_P_EmailAddress.Text).Trim();
                var BloodGroup = Convert.ToString(txt_BloodGroup.Text).Trim();
                var P_Address = Convert.ToString(txt_P_Address.Text).Trim();
                var C_Address = Convert.ToString(txt_C_Address.Text).Trim();
                var passportNo = Convert.ToString(txt_passportNo.Text).Trim();
                var passportPlaceIssue = Convert.ToString(txt_Passport_Place_Issue.Text).Trim();
                var P_Date_Issue = "";
                var P_Date_Expiry = "";
                if (Convert.ToString(txt_P_Date_Issue.Text).Trim() != "")
                {
                    var getTempDate = Convert.ToString(txt_P_Date_Issue.Text).Trim();
                    var split = getTempDate.Split('/');
                    P_Date_Issue = split[2] + "-" + split[1] + "-" + split[0];
                }
                if (Convert.ToString(txt_P_Date_Expiry.Text).Trim() != "")
                {
                    var getTempDate = Convert.ToString(txt_P_Date_Expiry.Text).Trim();
                    var split = getTempDate.Split('/');
                    P_Date_Expiry = split[2] + "-" + split[1] + "-" + split[0];
                }
                var ECP_Name = Convert.ToString(txt_ECP_Name.Text).Trim();
                var ECP_Number = Convert.ToString(txt_ECP_Number.Text).Trim();
                var PAN = Convert.ToString(txt_PAN.Text).Trim();
                var AdharNo = Convert.ToString(txt_AdharNo.Text).Trim();
                var BankName = Convert.ToString(txt_BankName.Text).Trim();
                var BankACCNo = Convert.ToString(txt_BankACCNo.Text).Trim();
                var IFSCCode = Convert.ToString(txt_IFSCCode.Text).Trim();
                var EPF = Convert.ToString(txt_EPF.Text).Trim();
                var UAN = Convert.ToString(txt_UAN.Text).Trim();
                var OtherModule = Convert.ToString(txtOtherModule.Text).Trim();
                var emp_code = Convert.ToString(Session["Empcode"]);
                var Completed_Certification = chk_Completed_Certification.Checked;
                // var getSabbatical_Educational_Break = (Convert.ToString(txt_Sabbatical_Educational_Break.Text).Trim());
                //Get Name
                var Name_As_Passport = Convert.ToString(txt_Name_As_Passport.Text).Trim();
                var Name_As_PAN = Convert.ToString(txt_Name_As_PAN.Text).Trim();
                var Name_As_Aadhar = Convert.ToString(txt_Name_As_Aadhar.Text).Trim();
                //End Name
                var getStatus1 = spm.UpdateEmployeeProfile("UpdateEmpProfile", emp_code, PersonalEmailID, BloodGroup, P_Address, C_Address, passportNo, P_Date_Issue, P_Date_Expiry, ECP_Name, ECP_Number, PAN, AdharNo, BankName, BankACCNo, IFSCCode, EPF, UAN, OtherModule, Completed_Certification, getSabbatical_Educational_Break,passportPlaceIssue,Name_As_Aadhar,Name_As_PAN,Name_As_Passport);

                BindEmpDetails(Convert.ToString(Session["Empcode"]), "All");
            }
            else
            {
                Label8.Text = "Something went wrong.";
                return;
            }
        }
        catch (Exception ex)
        {
            Label8.Text = ex.Message;
        }
    }


    protected void lnk_Update_Profile_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            var PersonalEmailID = Convert.ToString(txt_P_EmailAddress.Text).Trim();
            var BloodGroup = Convert.ToString(txt_BloodGroup.Text).Trim();
            var P_Address = Convert.ToString(txt_P_Address.Text).Trim();
            var C_Address = Convert.ToString(txt_C_Address.Text).Trim();
            var passportNo = Convert.ToString(txt_passportNo.Text).Trim();
            var P_Date_Issue = "";
            var P_Date_Expiry = "";
            var passportPlaceIssue = Convert.ToString(txt_Passport_Place_Issue.Text).Trim();
            if (Convert.ToString(txt_P_Date_Issue.Text).Trim()!="")
            {
                var getTempDate = Convert.ToString(txt_P_Date_Issue.Text).Trim();
                var split = getTempDate.Split('/');
                P_Date_Issue = split[2] + "-" + split[1] + "-" + split[0];
            }
            if (Convert.ToString(txt_P_Date_Expiry.Text).Trim() != "")
            {
                var getTempDate = Convert.ToString(txt_P_Date_Expiry.Text).Trim();
                var split = getTempDate.Split('/');
                P_Date_Expiry = split[2] + "-" + split[1] + "-" + split[0];
            }
            //var P_Date_Issue = Convert.ToString(txt_P_Date_Issue.Text).Trim();
            //var P_Date_Expiry = Convert.ToString(txt_P_Date_Expiry.Text).Trim();
            var ECP_Name = Convert.ToString(txt_ECP_Name.Text).Trim();
            var ECP_Number = Convert.ToString(txt_ECP_Number.Text).Trim();
            var PAN = Convert.ToString(txt_PAN.Text).Trim();
            var AdharNo = Convert.ToString(txt_AdharNo.Text).Trim();
            var BankName = Convert.ToString(txt_BankName.Text).Trim();
            var BankACCNo = Convert.ToString(txt_BankACCNo.Text).Trim();
            var IFSCCode = Convert.ToString(txt_IFSCCode.Text).Trim();
            var EPF = Convert.ToString(txt_EPF.Text).Trim();
            var UAN = Convert.ToString(txt_UAN.Text).Trim();
            var OtherModule = Convert.ToString(txtOtherModule.Text).Trim();
            var emp_code= Convert.ToString(Session["Empcode"]);
            //Get Name
            var Name_As_Passport = Convert.ToString(txt_Name_As_Passport.Text).Trim();
            var Name_As_PAN = Convert.ToString(txt_Name_As_PAN.Text).Trim();
            var Name_As_Aadhar = Convert.ToString(txt_Name_As_Aadhar.Text).Trim();
            //End Name

            var Completed_Certification = chk_Completed_Certification.Checked;

            var getSabbatical_Educational_Break = (Convert.ToString(txt_Sabbatical_Educational_Break.Text).Trim());

            if (getSabbatical_Educational_Break == "")
                getSabbatical_Educational_Break = "0.0";
            var getStatus = spm.UpdateEmployeeProfile("UpdateEmpProfile", emp_code, PersonalEmailID, BloodGroup, P_Address, C_Address, passportNo, P_Date_Issue, P_Date_Expiry, ECP_Name, ECP_Number, PAN, AdharNo, BankName, BankACCNo, IFSCCode, EPF, UAN, OtherModule, Completed_Certification, getSabbatical_Educational_Break, passportPlaceIssue,Name_As_Aadhar,Name_As_PAN,Name_As_Passport);
            BindEmpDetails(Convert.ToString(Session["Empcode"]), "All");
            SummaryofExperience();
        }
        catch (Exception)
        {

        }
    }

    protected void ddl_Region_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void SummaryofExperience()
    {
        try
        {
            var emp_code = Convert.ToString(Session["Empcode"]);
            var getDetails = spm.getReportAccessTable("getSummaryofExperience", emp_code);
            if(getDetails!=null)
            {
                if(getDetails.Rows.Count>0)
                {
                    var projectExp = getDetails.Rows[0]["TotalPDExp"].ToString();
                    var domainExp = getDetails.Rows[0]["TotalDDExp"].ToString();
                    var EducationalBreak = getDetails.Rows[0]["EducationalBreak"].ToString();
                    if(EducationalBreak=="" || EducationalBreak == null)
                    {
                        EducationalBreak = "0.0";
                    }
                    if(projectExp != ""|| domainExp != "")
                    {
                        var Total = "";
                        if (projectExp != "" && domainExp != "")
                        {
                            var getsplitD = domainExp.Split('.');
                            if (getsplitD.Length != 2)
                            {
                                domainExp = getsplitD[0] + "." + "0";                                
                                getsplitD = domainExp.Split('.');
                            }
                                
                           
                            var getsplitSAP = projectExp.Split('.');
                            if (getsplitSAP.Length != 2)
                            {
                                domainExp = getsplitSAP[0] + "." + "0";
                                getsplitSAP = projectExp.Split('.');
                            }

                            var total1 = Convert.ToInt32(getsplitD[1]) + Convert.ToInt32(getsplitSAP[1]);
                            var total0 = Convert.ToInt32(getsplitD[0]) + Convert.ToInt32(getsplitSAP[0]);
                            if (total1 >= 12)
                            {
                                total0 = total0 + 1;
                                total1 = total1 - 12;
                            }

                            Total = total0 + "." + total1;
                        }
                        else
                        {
                            if (projectExp != "")
                                Total = projectExp;
                            if (domainExp != "")
                                Total = domainExp;
                        }
                        //var Total
                        if(EducationalBreak!="0.0" && Total!="")
                        {
                            var getSplitTotal = Total.Split('.');
                            var getSplitEducational = EducationalBreak.Split('.');
                            var total1 = 0;
                            if(Convert.ToInt32(getSplitTotal[1])< Convert.ToInt32(getSplitEducational[1]))
                            {
                                total1 = Convert.ToInt32(getSplitEducational[1]) - Convert.ToInt32(getSplitTotal[1]);
                            }
                            else
                            {
                                total1 = Convert.ToInt32(getSplitTotal[1]) - Convert.ToInt32(getSplitEducational[1]);
                            }
                            var total0 = Convert.ToInt32(getSplitTotal[0]) - Convert.ToInt32(getSplitEducational[0]);
                            if (total1 >= 12)
                            {
                                total0 = total0 + 1;
                                total1 = total1 - 12;
                            }
                            else if(total1 > 1)
                            {
                                var h = 0 + "." + total1;
                                var conver1t = Convert.ToDouble(total0) - Convert.ToDouble(h);
                                var getval = Convert.ToString(conver1t);
                                var split = getval.Split('.');
                                var split1 = "";
                                var split2 = "";
                                if (split.Length==2)
                                {
                                    split1 = split[0];
                                    var g = Convert.ToInt32(split[1]) + 2;
                                    split2 = g.ToString();
                                }
                                else
                                {
                                    split1 = split[0];
                                    split2 = "0";
                                }
                                var Final = split1 + "." + split2;
                                total0 = Convert.ToInt32(split1);
                                total1 =Convert.ToInt32(split2);
                            }

                            Total = total0 + "." + total1;
                        }

                        txt_TotalDomainExp.Text = domainExp;
                        txt_TotalSAPExp.Text = projectExp;
                        txt_Sabbatical_Educational_Break.Text = EducationalBreak;
                        txt_TotalOverallExp.Text = Total.ToString();

                        var getTotalExp = (Convert.ToString(txt_TotalDomainExp.Text).Trim());
                        var getTotalSAPExp = (Convert.ToString(txt_TotalSAPExp.Text).Trim());
                        var getTotalOverAllExp = (Convert.ToString(txt_TotalOverallExp.Text).Trim());
                        var getSabbatical_Educational_Break = (Convert.ToString(txt_Sabbatical_Educational_Break.Text).Trim());
                        var descripation = Convert.ToString(txtJobDescription.Text).Trim();
                        if (getTotalExp == "")
                            getTotalExp = "0.0";

                        if (getTotalSAPExp == "")
                            getTotalSAPExp = "0.0";

                        if (getTotalOverAllExp == "")
                            getTotalOverAllExp = "0.0";

                        if (getSabbatical_Educational_Break == "")
                            getSabbatical_Educational_Break = "0.0";

                        var result = spm.InsertUpdateTotalEXPDetails(emp_code, getTotalExp, getTotalSAPExp, getTotalOverAllExp, descripation, getSabbatical_Educational_Break);


                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
}
