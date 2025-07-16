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

public partial class Rec_RecruiterCandidateView : System.Web.UI.Page
{
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public int did = 0;
    SP_Methods spm = new SP_Methods();
    DataSet dsRecruterInox;
   // public DataTable dtRecCandidate, dtcandidateDetails, dtmainSkillSet;


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
           // lblmessage.Text = "";
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
            login_EmpCode.Value = Session["Empcode"].ToString();
           // lblmessage.Visible = true;
            //   
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    PopulateEmployeeData();
                    //FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["CandidateDocPath"]).Trim());
                    FilePath.Value =(Convert.ToString("D:\\HRMS\\Candidates\\files\\DocumentLocker\\").Trim());
                    if (Request.QueryString.Count > 0)
                    {

                        HFCandidateID.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        /// hdnexp_id.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        BindAllGrid();
                    }
                    else
                    {
                        HFCandidateID.Value = "0";
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

 
    public void TypeCheckRecruiterOREnterviewerSchedular()
    {
        DataSet DSInterviewerSchedulars = new DataSet();
        DSInterviewerSchedulars = spm.GetInterviewerSchedularsEmpCode();
        string str = Convert.ToString(Session["Empcode"]).Trim();
        DataRow[] dr2 = DSInterviewerSchedulars.Tables[0].Select("InterviewerSchedularEmpCode='" + str + "'");
        if (dr2.Length > 0)
        {
            DataRow[] dr3 = DSInterviewerSchedulars.Tables[1].Select("RecruiterEmpCode='" + str + "'");
            if (dr3.Length > 0)
            {
                Response.Redirect("~/procs/RecruiterViewEdit.aspx?Recruitment_ReqID=" + HFRecruitment_ReqID.Value);
            }
            else
            {
                Response.Redirect("~/procs/InterviewerSchedulerView.aspx?Recruitment_ReqID=" + HFRecruitment_ReqID.Value);
            }
        }
        else
        {
            Response.Redirect("~/procs/RecruiterViewEdit.aspx?Recruitment_ReqID=" + HFRecruitment_ReqID.Value);
        }
    }

  
    protected void lnkEditFeedBack_Click(object sender, ImageClickEventArgs e)
    {
        //ImageButton btn = (ImageButton)sender;
        //GridViewRow row = (GridViewRow)btn.NamingContainer;

        //HFRecruitment_ReqID.Value = Convert.ToString(GVInterviewfeedback.DataKeys[row.RowIndex].Values[0]).Trim();
        //HFCandidateID.Value = Convert.ToString(GVInterviewfeedback.DataKeys[row.RowIndex].Values[1]).Trim();
        //HFCandidateScheduleRound_ID.Value = Convert.ToString(GVInterviewfeedback.DataKeys[row.RowIndex].Values[2]).Trim();

        //if (hdLinkType.Value == "RECIRescedule")
        //{
        //  Response.Redirect("~/procs/RecruiterReschedule.aspx?ReqID=" + HFRecruitment_ReqID.Value + "&CanID=" + HFCandidateID.Value + "&CSRID=" + HFCandidateScheduleRound_ID.Value);
        //}

    }

    #endregion

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
       // Searchmethod();
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        //getMngRecruterInoxList();
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
    private void GetCandidateDetails(DataTable dataTable)
    {
        try
        {
            // DataSet dsList = new DataSet();
            DataTable dtleaveInbox = new DataTable();
            //dtleaveInbox = spm.GetCandidateDDL("GetCandidateDetails", id);
            dtleaveInbox = dataTable;
            if (dtleaveInbox != null)
            {
                if (dtleaveInbox.Rows.Count > 0)
                {
                    DDl_Salutation.SelectedValue = Convert.ToString(dtleaveInbox.Rows[0]["salutation"]);
                    txtFirstName.Text = Convert.ToString(dtleaveInbox.Rows[0]["first_name"]);
                    txtLastName.Text = Convert.ToString(dtleaveInbox.Rows[0]["last_name"]);
                    txtMiddelName.Text = Convert.ToString(dtleaveInbox.Rows[0]["MiddleName"]);
                    txtMotherName.Text = Convert.ToString(dtleaveInbox.Rows[0]["MOTHER_NAME"]);
                    txtDOB.Text = Convert.ToString(dtleaveInbox.Rows[0]["DateOfBirth"]);
                    ddlGender.SelectedValue = Convert.ToString(dtleaveInbox.Rows[0]["Gender"]);
                    DDl_Mar_Stat.SelectedValue = Convert.ToString(dtleaveInbox.Rows[0]["ISMARRIED"]);
                    txtBloodGroup.Text = Convert.ToString(dtleaveInbox.Rows[0]["BloodGroup"]);
                    txtEmailAddress.Text = Convert.ToString(dtleaveInbox.Rows[0]["Emailaddress"]);
                    txtMobileNo.Text = Convert.ToString(dtleaveInbox.Rows[0]["mobile"]);
                    txtTelephoneNo.Text = Convert.ToString(dtleaveInbox.Rows[0]["TelephoneNo"]);
                    txtCAddress.Text = Convert.ToString(dtleaveInbox.Rows[0]["C_ADD"]);
                    txtCPIN.Text = Convert.ToString(dtleaveInbox.Rows[0]["C_PIN"]);
                    txtCCity.Text = Convert.ToString(dtleaveInbox.Rows[0]["C_CITY"]);
                    txtCState.Text = Convert.ToString(dtleaveInbox.Rows[0]["C_STATE"]);
                    txtCCountry.Text = Convert.ToString(dtleaveInbox.Rows[0]["C_COUNTRY"]);
                    txtPAddress.Text = Convert.ToString(dtleaveInbox.Rows[0]["P_ADD"]);
                    txtPPIN.Text = Convert.ToString(dtleaveInbox.Rows[0]["P_PIN"]);
                    txtPCity.Text = Convert.ToString(dtleaveInbox.Rows[0]["P_CITY"]);
                    txtPState.Text = Convert.ToString(dtleaveInbox.Rows[0]["P_STATE"]);
                    txtPCountry.Text = Convert.ToString(dtleaveInbox.Rows[0]["P_COUNTRY"]);
                    txtEmergencyCName.Text = Convert.ToString(dtleaveInbox.Rows[0]["EmergencyContactPerson"]);
                    txtEmergencyCNo.Text = Convert.ToString(dtleaveInbox.Rows[0]["EmergencyContactNo"]);
                    txtNameAsPerPAN.Text = Convert.ToString(dtleaveInbox.Rows[0]["Name_As_Per_PAN"]);
                    txtPAN.Text = Convert.ToString(dtleaveInbox.Rows[0]["PAN_No"]);
                    txtNameAsPerAadhaar.Text = Convert.ToString(dtleaveInbox.Rows[0]["Name_As_Per_Aadhaar"]);
                    txtAadhaarNo.Text = Convert.ToString(dtleaveInbox.Rows[0]["Aadhaar_No"]);

                    ddlPFAccount.SelectedValue = Convert.ToString(dtleaveInbox.Rows[0]["Previous_Employer_PF"]);
                    txtPFNumber.Text = Convert.ToString(dtleaveInbox.Rows[0]["PF_Account_No"]);
                    txtUANNumber.Text = Convert.ToString(dtleaveInbox.Rows[0]["UAN"]);
                    ddlPensionAccount.SelectedValue = Convert.ToString(dtleaveInbox.Rows[0]["Previous_Employer_Pension"]);
                    txtPentionAccNo.Text = Convert.ToString(dtleaveInbox.Rows[0]["Pention_Account_No"]);

                    txtBankName1.Text = Convert.ToString(dtleaveInbox.Rows[0]["BANK_NAME"]);
                    txtBankAccNo.Text = Convert.ToString(dtleaveInbox.Rows[0]["BANK_AC"]);
                    txtIFSCCode.Text = Convert.ToString(dtleaveInbox.Rows[0]["IFSC_CODE"]);
                    txtBankAccName.Text = Convert.ToString(dtleaveInbox.Rows[0]["Bank_Acc_Name"]);
                    txtBranchNumber.Text = Convert.ToString(dtleaveInbox.Rows[0]["Bank_Branch_No"]);
                    txtMICRCode.Text = Convert.ToString(dtleaveInbox.Rows[0]["MICR_Code"]);
                    txtBankAddress.Text = Convert.ToString(dtleaveInbox.Rows[0]["Bank_Address"]);

                    txtNameAsPerPassport.Text = Convert.ToString(dtleaveInbox.Rows[0]["Name_As_Per_Passport"]);
                    txtPlaceOfIssue.Text = Convert.ToString(dtleaveInbox.Rows[0]["Place_Of_Issue"]);
                    txtPassportNo.Text = Convert.ToString(dtleaveInbox.Rows[0]["Passport_No"]);
                    txtDateOfIssue.Text = Convert.ToString(dtleaveInbox.Rows[0]["P_Date_Issue"]);
                    txtDateOfExpiry.Text = Convert.ToString(dtleaveInbox.Rows[0]["P_Expiry_Date"]);
                    if (Convert.ToString(dtleaveInbox.Rows[0]["IsPassport"]) != "")
                    {
                        var getSelectedValues4 = ddl_Passport.SelectedValue.ToString();
                        ddl_Passport.Items.FindByValue(getSelectedValues4).Selected = false;

                        chk_IsPassport.Checked = Convert.ToBoolean(dtleaveInbox.Rows[0]["IsPassport"]);
                        if (chk_IsPassport.Checked == true)
                        {
                            ddl_Passport.Items.FindByValue("Yes").Selected = true;
                        }
                        else
                        {
                            ddl_Passport.Items.FindByValue("No").Selected = true;
                        }
                    }
                    if (Convert.ToString(dtleaveInbox.Rows[0]["IsCompleted"]) != "")
                    {
                        txtRemark_1.Text= Convert.ToString(dtleaveInbox.Rows[0]["RecruiterRemark"]);
                        var getSelectedValues4 = ddl_Status.SelectedValue.ToString();
                        ddl_Status.Items.FindByValue(getSelectedValues4).Selected = false;

                        var getVal = Convert.ToBoolean(dtleaveInbox.Rows[0]["IsCompleted"]);
                        if(getVal==true)
                        {
                            ddl_Status.Items.FindByValue("Completed").Selected = true;
                        }
                        else
                        {
                            ddl_Status.Items.FindByValue("In-Completed").Selected = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void GetDocumentType(DataTable dt)
    {
        try
        {           
            if (dt.Rows.Count > 0)
            {
                gvCandidateDocument.DataSource = dt;
                gvCandidateDocument.DataBind();
            }

        }
        catch (Exception ex)
        {

        }
    }
    private void BindAllGrid()
    {
        try
        {
            DDl_Salutation.Enabled = false;
            gvCandidateDocument.DataSource = null;
            gvCandidateDocument.DataBind();
            var getVal = Convert.ToDouble(HFCandidateID.Value);
            if(getVal!=0)
            {
                var getds = spm.getRecruitmentCandiateDetailsByCandidate("getRecruitmentInboxById", getVal);
                if(getds != null)
                {
                    if(getds.Tables.Count>0)
                    {
                        var getCandidateDetails = getds.Tables[0];
                        var getDocumentDetails = getds.Tables[1];
                        var getFamilyDetails = getds.Tables[2];
                        var getEducationDetails = getds.Tables[3];
                        var getProjectDetails = getds.Tables[4];
                        //
                        GetCandidateDetails(getCandidateDetails);
                        GetDocumentType(getDocumentDetails);
                        BindFamilyDetailsGRID(getFamilyDetails);
                        BindEduactionDetailsGRID(getEducationDetails);
                        BindProjectDetailsGRID(getProjectDetails);
                    }
                }
            }
        }
        catch (Exception)
        {
        }
    }

    protected void txtDOB_TextChanged(object sender, EventArgs e)
    {

    }

    protected void ddlPFAccount_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlPensionAccount_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddl_Passport_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void txtDateOfIssue_TextChanged(object sender, EventArgs e)
    {

    }

    protected void txtDateOfExpiry_TextChanged(object sender, EventArgs e)
    {

    }

    protected void chk_IsPassport_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void chkSameAsPer_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblErrorMessage.Text = "";
            if (ddl_Status.SelectedValue == "0")
            {
                lblErrorMessage.Text = "Please Select Status.";
                return;
            }
            var getStatus = Convert.ToString(ddl_Status.SelectedValue);
            var IsCompleted = false;
            var remark = "";
            if (getStatus== "Completed")
            {
                IsCompleted = true;
            }
            else
            {
                if(txtRemark_1.Text=="")
                {
                    lblErrorMessage.Text = "Please enter remark.";
                    return;
                }
            }
            remark = txtRemark_1.Text.ToString();
            var candidateId = Convert.ToDouble(HFCandidateID.Value);
            var createdBy = Convert.ToString(Session["Empcode"]).Trim();
            spm.UpdateCandidateCompletedStatus("UpdateCompletedStatus", createdBy, candidateId, IsCompleted, remark);
            //Sending Emal Details
            if(IsCompleted==false)
            {
                var candidateName = txtFirstName.Text + " " + txtMiddelName.Text + " " + txtLastName.Text;
                var rec_Name = Convert.ToString(login_EmpName.Value);
                var toEmail = txtEmailAddress.Text;
                var subject = "HIGHBAR : Pre-Joining form send back for correction. Please Correct & Re-submit.";
                var ccEmail = Convert.ToString(login_EmpEmail.Value);
                var URL = "https://ess.highbartech.com/Candidates/Can_login.aspx";
                spm.send_mailto_Candidate_Re_Submit(candidateName, rec_Name, toEmail, subject, remark, ccEmail, URL);
            }
            //End Sending Email Details
            Response.Redirect("~/procs/Rec_RecruiterCandidateInbox.aspx");

        }
        catch (Exception ex)
        {

        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {

    }

    protected void ddl_Status_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(ddl_Status.SelectedValue== "In-Completed")
            {
                showRemarkDiv.Visible = true;
            }
            else
            {
                showRemarkDiv.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void PopulateEmployeeData()
    {
        try
        {
            
            //btnSave.Enabled = false; 
            var dtEmp = spm.GetEmployeeData(Convert.ToString(login_EmpCode.Value));

            if (dtEmp.Rows.Count > 0)
            {               
                login_EmpName.Value = (string)dtEmp.Rows[0]["Emp_Name"];
               // lpm.Designation_name = (string)dtEmp.Rows[0]["DesginationName"];
               // lpm.department_name = (string)dtEmp.Rows[0]["Department_Name"];
               // lpm.Grade = Convert.ToString(dtEmp.Rows[0]["Grade"]).Trim();
                login_EmpEmail.Value = (string)dtEmp.Rows[0]["Emp_EmailAddress"];               
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }
}