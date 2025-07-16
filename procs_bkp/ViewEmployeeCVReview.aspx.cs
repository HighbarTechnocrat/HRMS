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

public partial class ViewEmployeeCVReview : System.Web.UI.Page
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
                    BindEmpDetails(Convert.ToInt32(hdnempcode.Value));
                    PopulateEmployeeData();
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
    private void BindEmpDetails(int id)
    {
        try
        {
            var getDS = spm.getCVDetailById(id, "GetDetailsByEmpId");
            if (getDS != null)
            {
                //lblmessage.Text = Convert.ToString(getDS.Tables.Count);
                var isMarried = "";
                if (getDS.Tables.Count > 0)
                {
                    //Bind Employee Details
                    var getEmpDetailsTable = getDS.Tables[0];
                    //lblmessage.Text = Convert.ToString(getEmpDetailsTable.Rows.Count);
                   // lblmessage.Text = Convert.ToString(id);
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

                        txt_MotherName.Text = getEmpDetailsTable.Rows[0]["MOTHER_NAME"].ToString();
                        txt_FatherName.Text = getEmpDetailsTable.Rows[0]["FatherName"].ToString();
                        var getIsAnyCertificate = ddl_IsAnyCertificateDone.SelectedValue.ToString();
                        ddl_IsAnyCertificateDone.Items.FindByValue(getIsAnyCertificate).Selected = false;
                        if (Convert.ToString(getEmpDetailsTable.Rows[0]["IsCertification"]).Trim() != "")
                        {
                            chk_Completed_Certification.Checked = Convert.ToBoolean(getEmpDetailsTable.Rows[0]["IsCertification"]);

                            if (Convert.ToBoolean(getEmpDetailsTable.Rows[0]["IsCertification"]) == true)
                            {
                                ddl_IsAnyCertificateDone.Items.FindByValue("Yes").Selected = true;
                            }
                            else
                            {
                                ddl_IsAnyCertificateDone.Items.FindByValue("No").Selected = true;
                            }
                        }
                        else
                        {
                            ddl_IsAnyCertificateDone.Items.FindByValue("0").Selected = true;
                        }
                        if (Convert.ToString(getEmpDetailsTable.Rows[0]["Is_Passport"]).Trim() != "")
                        {
                            var getSelectedValues4 = ddl_IsPassport.SelectedValue.ToString();
                            ddl_IsPassport.Items.FindByValue(getSelectedValues4).Selected = false;
                            //chk_Passport.Checked = Convert.ToBoolean(getEmpDetailsTable.Rows[0]["Is_Passport"]);
                            if (Convert.ToBoolean(getEmpDetailsTable.Rows[0]["Is_Passport"]) == true)
                            {
                                ddl_IsPassport.Items.FindByValue("Yes").Selected = true;
                            }
                            else
                            {
                                ddl_IsPassport.Items.FindByValue("No").Selected = true;
                            }
                        }
                        else
                        {

                        }
                        var getisAnyProject = ddl_IsAnyProject.SelectedValue.ToString();
                        ddl_IsAnyProject.Items.FindByValue(getisAnyProject).Selected = false;
                        if (getEmpDetailsTable.Rows[0]["IsAnyProjectDone"].ToString() != "")
                        {
                            if (Convert.ToBoolean(getEmpDetailsTable.Rows[0]["IsAnyProjectDone"].ToString()) == true)
                            {
                                ddl_IsAnyProject.Items.FindByValue("Yes").Selected = true;
                                ddl_IsAnyProject.Enabled = false;
                            }
                            else
                            {
                                ddl_IsAnyProject.Items.FindByValue("No").Selected = true;
                            }
                        }
                        else
                        {
                            ddl_IsAnyProject.Items.FindByValue("0").Selected = true;
                        }
                        var getisAnyDomain = ddl_IsDomain.SelectedValue.ToString();
                        ddl_IsDomain.Items.FindByValue(getisAnyDomain).Selected = false;
                        if (getEmpDetailsTable.Rows[0]["IsAnyDomainExperience"].ToString() != "")
                        {
                            if (Convert.ToBoolean(getEmpDetailsTable.Rows[0]["IsAnyDomainExperience"].ToString()) == true)
                            {
                                ddl_IsDomain.Items.FindByValue("Yes").Selected = true;
                                ddl_IsDomain.Enabled = false;
                            }
                            else
                            {
                                ddl_IsDomain.Items.FindByValue("No").Selected = true;
                            }
                        }
                        else
                        {
                            ddl_IsDomain.Items.FindByValue("0").Selected = true;
                        }
                        var getTotalDomainExperience = getEmpDetailsTable.Rows[0]["TotalDomainExperience"].ToString();
                        var getTotalSAPExperience = getEmpDetailsTable.Rows[0]["TotalSAPExperience"].ToString();
                        var getOverallWorkExperience = getEmpDetailsTable.Rows[0]["OverallWorkExperience"].ToString();
                        var getEducationalBreak = getEmpDetailsTable.Rows[0]["EducationalBreak"].ToString();
                        var getTotalNONSAPExperience = getEmpDetailsTable.Rows[0]["TotalNONSAPExperience"].ToString();

                        txt_TotalDomainExp.Text = getTotalDomainExperience;
                        txt_TotalSAPExp.Text = getTotalSAPExperience;
                        txt_TotalOverallExp.Text = getOverallWorkExperience.ToString();
                       // txt_Non_SAPExp.Text = getTotalNONSAPExperience.ToString();
                        txt_Sabbatical_Educational_Break.Text = getEducationalBreak.ToString();
                        // txt_EducationalB.Text = getEducationalBreak.ToString();
                        txtJobDescription.Text = Convert.ToString(getEmpDetailsTable.Rows[0]["ProfileSummary"]);
                    }
                    var getFamilyDetailsDT = getDS.Tables[1];
                    BindFamilyDetailsGRID(getFamilyDetailsDT);
                    var getEduactionDetails = getDS.Tables[2];
                    BindEduactionDetailsGRID(getEduactionDetails);
                    var getCertificationDetails = getDS.Tables[3];
                    BindCertificationDetailsGRID(getCertificationDetails);
                    var getProjectDetails = getDS.Tables[4];
                    BindProjectDetailsGRID(getProjectDetails);
                    var getDomainDetails = getDS.Tables[5];
                    BindDomainDetailsGRID(getDomainDetails);
                    var getDocumentDetails = getDS.Tables[6];
                    BindDocumentDetailsGRID(getDocumentDetails);
                    //End Bind Employee Details                                       
                   
                }
            }
        }
        catch (Exception ex)
        {
            lblmessage.Visible = true;
            lblmessage.Text = ex.ToString();
            
        }
    }
    // Bind Family Details   
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
    //End
    //Eduaction Details
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
    
    //End Project And Domain
    //Bind Document     
    private void BindDocumentDetailsGRID(DataTable dataTable)
    {
        gv_Documents.DataSource = null;
        gv_Documents.DataBind();
        if (dataTable.Rows.Count > 0)
        {
            gv_Documents.DataSource = dataTable;
            gv_Documents.DataBind();
        }
        else
        {

        }
    }
    //End Bind Document

    #endregion



    protected void lnk_FinalSubmit_Click(object sender, EventArgs e)
    {

    }

    protected void chk_Completed_Certification_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void ddl_IsPassport_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddl_IsPassport_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }

    protected void ddl_IsAnyCertificateDone_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void chk_Completed_Certification_CheckedChanged1(object sender, EventArgs e)
    {

    }

    protected void ddl_IsAnyProject_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void lnk_Status_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            Label1.Text = "";
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            if(ddl_Aprover_Status.SelectedValue=="0")
            {
                Label1.Text = "Please Select Status";
                return;
            }
            else if(ddl_Aprover_Status.SelectedValue == "Correction")
            {
                if(txt_ApproverRemark.Text=="")
                {
                    Label1.Text = "Please enter remark";
                    return;
                }
                var getA_Emp_Code = Convert.ToString(hdnA_Emp_Code.Value);
                var getA_Emp_Name = Convert.ToString(hdnA_Emp_Name.Value);
                var getA_Emp_Email = Convert.ToString(hdnA_Emp_Email.Value);
                var getEmp_Name = Convert.ToString(txtEmpName.Text);
                var getEmp_Email = Convert.ToString(txt_EmailAddress.Text);
                var getEmp_Code = Convert.ToString(txtEmpCode.Text);
                var getRemark = Convert.ToString(txt_ApproverRemark.Text);
                //Insert By Review
                spm.InsertUpdateCVReview("UpdateCVLogByHR", getEmp_Code, getA_Emp_Code, getRemark, "Correction");//Approved
                //End
                var URL = "http://localhost/hrms/login.aspx?ReturnUrl=procs/EmployeeCV.aspx";
                var subject = "OneHR: CV send back for correction. Please Correct & Re-submit.";
                spm.send_mailto_Employee_Re_Submit_Profile(getEmp_Name, getA_Emp_Name, getEmp_Email, subject, getRemark, getA_Emp_Email, URL);
            }
            else
            {                
                var getA_Emp_Code = Convert.ToString(hdnA_Emp_Code.Value);
                var getA_Emp_Name = Convert.ToString(hdnA_Emp_Name.Value);
                var getA_Emp_Email = Convert.ToString(hdnA_Emp_Email.Value);
                var getEmp_Name = Convert.ToString(txtEmpName.Text);
                var getEmp_Email = Convert.ToString(txt_EmailAddress.Text);
                var getEmp_Code = Convert.ToString(txtEmpCode.Text);
                var getRemark = Convert.ToString(txt_ApproverRemark.Text);
                //Insert By Review
                spm.InsertUpdateCVReview("UpdateCVLogByHRApproved", getEmp_Code, getA_Emp_Code, getRemark, "Approved");//Approved
                //End
                var URL = "http://localhost/hrms/login.aspx?ReturnUrl=procs/EmployeeCV.aspx";
                var subject = "OneHR: CV reviewed and approved.";
                spm.send_mailto_Employee_Approved_Profile(getEmp_Name, getA_Emp_Name, getEmp_Email, subject, getRemark, getA_Emp_Email, URL);

            }
            Response.Redirect("~/procs/EmployeeCVReviewInbox.aspx");
        }
        catch (Exception ex)
        {
        }
    }
    public void PopulateEmployeeData()
    {
        try
        {  
           var dtEmp = spm.GetEmployeeData(Convert.ToString(Session["Empcode"]));
            if (dtEmp.Rows.Count > 0)
            {
                var Emp_Name = (string)dtEmp.Rows[0]["Emp_Name"];
                var Emp_Code = Convert.ToString(Session["Empcode"]);
                var EmailAddress = (string)dtEmp.Rows[0]["Emp_EmailAddress"];
                hdnA_Emp_Code.Value = Emp_Code;
                hdnA_Emp_Name.Value = Emp_Name;
                hdnA_Emp_Email.Value = EmailAddress;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();
        }
    }
}
