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
public partial class Req_Requisition_View : System.Web.UI.Page
{
    #region CreativeMethods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    SP_Methods spm = new SP_Methods();
    //Code for Request Details Voew
    public DataTable dtTrDetails, dtApprovalDetails, dtEmp, dtextraApp;
    String strloginid = "";
    String strempcode = "";
    string Leavestatus = "";
    string IsApprover = "";
    string nxtapprcode;
    string nxtapprname = "", approveremailaddress = "";
    int apprid;
    int statusid;

    StringBuilder strbuild = new StringBuilder();
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }


            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    hdnEmpCpde.Value = Convert.ToString(Session["Empcode"]).Trim();
                    hdnLoginUserName.Value = Convert.ToString(Session["emp_loginName"]).Trim();
                    hdnLoginEmpEmail.Value = Convert.ToString(Session["LoginEmpmail"]).Trim();
                    
                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["QuestionnaireDocumentpath"]).Trim());

                    //GetRequisitionNo();
                    CheckHREmployee();
                    
                    GetSkillsetName();
                    GetPositionName();
                    GetPositionCriticality();
                    GetDepartmentMaster();
                    GetCompany_Location();
                    GetReasonRequisition();
                    GetPositionDesign();
                    GetPreferredEmpType();
                    GetlstPositionBand();
                    //GetInterviewer();
                    GetRecruiter();
                    // trvldeatils_delete_btn.Visible = false;
                    if (Request.QueryString.Count > 0)
                    {
                        hdnRecruitment_ReqID.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        //checkApprovalStatus_Submit();
                        //GetCuurentApprID();
                        CheckExtraApprovalEmp();
                        GetRecruitmentDetail();
                        PopulateEmployeeData();
                        //check_ISHR();
                        // trvldeatils_delete_btn.Visible = true;
                        lblheading.Text = "View Recruitment Requisition ";
                    }
                    else
                    {
                        Response.Redirect("~/procs/Req_RequisitionIndex.aspx");
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }




    #region PageMethods
    public void CheckHREmployee()
    {
        try
        {
            dtEmp = spm.Get_HRRequisition_Employee(Convert.ToString(Session["Empcode"]).Trim());
            if (dtEmp.Rows.Count > 0)
            {
                hdnHRDept.Value = (string)dtEmp.Rows[0]["Department_Name"].ToString().Trim();
                //hflEmpDesignation.Value = (string)dtEmp.Rows[0]["DesginationName"].ToString().Trim();
                //hflEmailAddress.Value = (string)dtEmp.Rows[0]["Emp_EmailAddress"];
                if ((string)dtEmp.Rows[0]["Department_Name"].ToString().Trim() == "Human Resources")
                {
                   
                }
                    
                
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }
    public void CheckExtraApprovalEmp()
    {
        try
        {
            dtextraApp = spm.Get_HRRequisition_ExtraApprovalEmp(Convert.ToString(Session["Empcode"]).Trim(), Convert.ToInt32(hdnRecruitment_ReqID.Value));
            if (dtextraApp.Rows.Count > 0)
            {
                hdnExtraAPP.Value = (string)dtextraApp.Rows[0]["app_remarks"].ToString().Trim();
               // hdnExtraAPPID.Value = (string)dtextraApp.Rows[0]["Appr_id"].ToString().Trim();
                //hdnHRDept.Value = (string)dtEmp.Rows[0]["Action"].ToString().Trim();
            }
            if (hdnExtraAPP.Value.ToString().Trim() == "HR")
            {
                Recruiter.Visible = true;
                listRecruiter.Enabled = false;
            }
            else
            {
                Recruiter.Visible = false;
            }
            
            lstPositionName.Enabled = false;
            lstPositionCriti.Enabled = false;
            lstSkillset.Enabled = false;
            lstPositionDept.Enabled = false;
            lstPositionDesign.Enabled = false;
            lstPositionLoca.Enabled = false;
            txtOtherDept.Enabled = false;
            txtPositionDesig.Enabled = false;

            txtNoofPosition.Enabled = false;
            txtAdditionSkill.Enabled = false;
            txttofilledIn.Enabled = false;
            txtSalaryRangeFrom.Enabled = false;
            txtSalaryRangeTo.Enabled = false;
            lstReasonForRequi.Enabled = false;

            lstPreferredEmpType.Enabled = false;
            lstPositionBand.Enabled = false;
            //trvl_localbtn.Enabled = false;
            txtEssentialQualifi.Enabled = false;
            txtDesiredQualifi.Enabled = false;

            txtRequiredExperiencefrom.Enabled = false;
            txtRequiredExperienceto.Enabled = false;
            lstRecommPerson.Enabled = false;
            txtJobDescription.Enabled = false;
            localtrvl_delete_btn.Enabled = false;
            //accmo_cancel_btn.Enabled = false;
            localtrvl_delete_btn.Visible = false;
            //accmo_cancel_btn.Visible = false;

            txtComments.Enabled = false;
            lstInterviewerOne.Enabled = false;
            lstInterviewerTwo.Enabled = false;
            txtInterviewerOptOne.Enabled = false;
            txtInterviewerOptTwo.Enabled = false;

            //trvldeatils_btnSave.Visible = false;
            //trvldeatils_delete_btn.Visible = false;
            // lnkuplodedfile.Style.Add("padding-bottom", "15px");
        
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }
    }
    public void PopulateEmployeeData()
    {
        try
        {
            dtEmp = spm.Get_Requisition_EmployeeData(hdnEmpCodePrve.Value.ToString().Trim());
            if (dtEmp.Rows.Count > 0)
            {
                hflEmpDepartment.Value = (string)dtEmp.Rows[0]["Department_Name"].ToString().Trim();
                hflEmpDesignation.Value = (string)dtEmp.Rows[0]["DesginationName"].ToString().Trim();
                hflEmailAddress.Value = (string)dtEmp.Rows[0]["Emp_EmailAddress"];
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }
    protected void GetCuurentApprID()
    {
        int capprid;
        string Actions = "";
        DataTable dtCApprID = new DataTable();
        dtCApprID = spm.GetCurrentRecruitApprID(Convert.ToInt32(hdnRecruitment_ReqID.Value), hdnEmpCpde.Value);
        capprid = (int)dtCApprID.Rows[0]["APPR_ID"];
        Actions = (string)dtCApprID.Rows[0]["Action"];
        hdnCurrentID.Value = capprid.ToString();

        if (Convert.ToString(hdnCurrentID.Value).Trim() == "")
        {
            lblmessage.Text = "Acton on this Request not yet taken by other approvals";
            return;
        }
        else if (Convert.ToString(Actions).Trim() != "Pending")
        {
            lblmessage.Text = "You already actioned for this request";
            return;
        }
    }
    public void GetSkillsetName()
    {
        DataTable dtSkillset = new DataTable();
        dtSkillset = spm.GetRecruitment_SkillsetName();
        if (dtSkillset.Rows.Count > 0)
        {
            lstSkillset.DataSource = dtSkillset;
            lstSkillset.DataTextField = "ModuleDesc";
            lstSkillset.DataValueField = "ModuleId";
            lstSkillset.DataBind();
            lstSkillset.Items.Insert(0, new ListItem("Select Skillset", "0"));

        }
    }
    public void GetPositionName()
    {
        DataTable dtPositionName = new DataTable();
        dtPositionName = spm.GetRecruitment_PositionTitle();
        if (dtPositionName.Rows.Count > 0)
        {
            lstPositionName.DataSource = dtPositionName;
            lstPositionName.DataTextField = "PositionTitle";
            lstPositionName.DataValueField = "PositionTitle_ID";
            lstPositionName.DataBind();
            lstPositionName.Items.Insert(0, new ListItem("Select Position", "0")); 
        }
    }
    public void GetPositionCriticality()
    {
        DataTable dtPositionCriti = new DataTable();
        dtPositionCriti = spm.GetRecruitment_Req_PositionCriticality();
        if (dtPositionCriti.Rows.Count > 0)
        {
            lstPositionCriti.DataSource = dtPositionCriti;
            lstPositionCriti.DataTextField = "PositionCriticality";
            lstPositionCriti.DataValueField = "PositionCriticality_ID";
            lstPositionCriti.DataBind();
            lstPositionCriti.Items.Insert(0, new ListItem("Select Criticality", "0"));
        }
    }
    public void GetDepartmentMaster()
    {
        DataTable dtPositionDept = new DataTable();
        dtPositionDept = spm.GetRecruitment_Req_DepartmentMaster();
        if (dtPositionDept.Rows.Count > 0)
        {
            lstPositionDept.DataSource = dtPositionDept;
            lstPositionDept.DataTextField = "Department_Name";
            lstPositionDept.DataValueField = "Department_id";
            lstPositionDept.DataBind();
            lstPositionDept.Items.Insert(0, new ListItem("Select Department", "0"));
			lstPositionDept.Enabled = false;
			//updated code
			//DataRow[] dr = dtPositionDept.Select("Department_Name = '" + txtReqDept.Text.ToString().Trim() + "'");
			//if (dr.Length > 0)
			//{
			//	string avalue = dr[0]["Department_id"].ToString();
			//	lstPositionDept.SelectedValue = avalue;
			//}
			//lstPositionDept.Items.FindByText(txtReqDept.Text).Selected = true;
			//lstPositionDept.Enabled = false;
		}
	}
    public void GetCompany_Location()
    {
        DataTable lstPosition = new DataTable();
        lstPosition = spm.GetRecruitment_Req_company_Location();
        if (lstPosition.Rows.Count > 0)
        {
            lstPositionLoca.DataSource = lstPosition;
            lstPositionLoca.DataTextField = "Location_name";
            lstPositionLoca.DataValueField = "comp_code";
            lstPositionLoca.DataBind();
            lstPositionLoca.Items.Insert(0, new ListItem("Select Position Location", "0"));

        }
    }
    public void GetReasonRequisition()
    {
        DataTable lstReasonFor = new DataTable();
        lstReasonFor = spm.GetRecruitment_Req_ReasonRequisition();
        if (lstReasonFor.Rows.Count > 0)
        {
            lstReasonForRequi.DataSource = lstReasonFor;
            lstReasonForRequi.DataTextField = "ReasonRequisition";
            lstReasonForRequi.DataValueField = "ReasonRequisition_ID";
            lstReasonForRequi.DataBind();
            lstReasonForRequi.Items.Insert(0, new ListItem("Select Reason Requisition", "0"));
        }
    }
    public void GetPositionDesign()
    {
        DataTable dtPositionDesign = new DataTable();
        dtPositionDesign = spm.GetRecruitment_Req_DesignationMaster();
        if (dtPositionDesign.Rows.Count > 0)
        {
            lstPositionDesign.DataSource = dtPositionDesign;
            lstPositionDesign.DataTextField = "DesginationName";
            lstPositionDesign.DataValueField = "Designation_iD";
            lstPositionDesign.DataBind();
            lstPositionDesign.Items.Insert(0, new ListItem("Select Position Designation", "0"));

            
        }
    }
    public void GetPreferredEmpType()
    {
        DataTable dtPositionName = new DataTable();
        dtPositionName = spm.GetRecruitment_Req_HRMS_Employment_Type();
        if (dtPositionName.Rows.Count > 0)
        {
            lstPreferredEmpType.DataSource = dtPositionName;
            lstPreferredEmpType.DataTextField = "Particulars";
            lstPreferredEmpType.DataValueField = "PID";
            lstPreferredEmpType.DataBind();
            lstPreferredEmpType.Items.Insert(0, new ListItem("Select Preferred Emp Type", "0"));
        }
    }
    public void GetlstPositionBand()
    {
        DataTable dtPositionBand = new DataTable();
        dtPositionBand = spm.GetRecruitment_Req_HRMS_BAND_MASTER();
        if (dtPositionBand.Rows.Count > 0)
        {
            lstPositionBand.DataSource = dtPositionBand;
            lstPositionBand.DataTextField = "BAND";
            lstPositionBand.DataValueField = "BAND";
            lstPositionBand.DataBind();
            lstPositionBand.Items.Insert(0, new ListItem("Select BAND", "0"));
        }
    }
    public void GetInterviewer(int ModuleId)
    {
        DataTable dtInterviewer = new DataTable();
        dtInterviewer = spm.GetRecruitment_Req_Screeners_Mst(ModuleId);
        if (dtInterviewer.Rows.Count > 0)
        {
            lstInterviewerOne.DataSource = dtInterviewer;
            lstInterviewerOne.DataTextField = "EmployeeName";
            lstInterviewerOne.DataValueField = "EmployeeCode";
            lstInterviewerOne.DataBind();
            lstInterviewerOne.Items.Insert(0, new ListItem("Select Screening By", "0"));

            //lstRecommPerson.DataSource = dtInterviewer;
            //lstRecommPerson.DataTextField = "EmployeeName";
            //lstRecommPerson.DataValueField = "EmployeeCode";
            //lstRecommPerson.DataBind();
            //lstRecommPerson.Items.Insert(0, new ListItem("Select Recommended Person", "0"));
        }
    }
    public void GetRequisitionNo()
    {
        DataSet dsReqNo = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_Req_REQUISTIONNO";
            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(Session["Empcode"]);
            dsReqNo = spm.getDatasetList(spars, "SP_GET_REQ_REQUISTIONNO");
            if (dsReqNo.Tables[0].Rows.Count > 0)
            {
                txtReqNumber.Text = Convert.ToString(dsReqNo.Tables[0].Rows[0]["MaxReq_ID"]).Trim();
                txtFromdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");// "MM/dd/yyyy""dd-MM-yyyy HH:mm:ss"
                txtReqName.Text = Convert.ToString(dsReqNo.Tables[1].Rows[0]["EmpName"]).Trim();
                txtReqDept.Text = Convert.ToString(dsReqNo.Tables[1].Rows[0]["Department"]).Trim();
                txtReqDesig.Text = Convert.ToString(dsReqNo.Tables[1].Rows[0]["Designation"]).Trim();
                txtReqEmail.Text = Convert.ToString(dsReqNo.Tables[1].Rows[0]["Emp_Emailaddress"]).Trim();
                // lstPositionName.SelectedValue = Convert.ToString(dsQuestDetails.Tables[0].Rows[0]["PositionTitle_ID"]).Trim();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    public void GetRecruiter()
    {
        DataTable dtInterviewer = new DataTable();
        dtInterviewer = spm.GetRecruitment_Recruiter();
        if (dtInterviewer.Rows.Count > 0)
        {
            listRecruiter.DataSource = dtInterviewer;
            listRecruiter.DataTextField = "EmpName";
            listRecruiter.DataValueField = "Emp_Code";
            listRecruiter.DataBind();
            listRecruiter.Items.Insert(0, new ListItem("Select Recruiter", "0"));
        }
    }
    private void GetRecruitmentDetail()
    {

        DataSet dsRecruitmentDetails = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "RecruitmentReq_Edit";
            spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
            spars[1].Value = Convert.ToInt32(hdnRecruitment_ReqID.Value);
            spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            spars[2].Value = Session["Empcode"].ToString();
            dsRecruitmentDetails = spm.getDatasetList(spars, "SP_Recruitment_Requisition_INSERT");

            if (dsRecruitmentDetails.Tables[0].Rows.Count > 0)
            {
                txtReqNumber.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionNumber"]).Trim();
                txtReqName.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["fullNmae"]).Trim();
                //DateTime Fromdate = DateTime.ParseExact(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionDate"].ToString(), "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                // var Timesheet_date = Fromdate.ToString("dd-MM-yyyy HH:mm:ss");
                // txtFromdate.Text = Fromdate.ToString("dd-MM-yyyy HH:mm:ss");
                txtFromdate.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionDate"]).ToString();
                txtReqDesig.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Designation"]).Trim();
                txtReqDept.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department"]).Trim();
                txtReqEmail.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
                hdnEmpCodePrve.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code"]).Trim();
                lstSkillset.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ModuleId"]).Trim();
                lstPositionName.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionTitle_ID"]).Trim();
                lstPositionCriti.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionCriticality_ID"]).Trim();
                lstPositionDept.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department_id"]).Trim();
                txtNoofPosition.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["NoOfPosition"]).Trim();

                lstPositionLoca.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["comp_code"]).Trim();
                txtOtherDept.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["OtherDepartment"]).Trim();
                txtPositionDesig.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionDesignationOther"]).Trim();
                txtAdditionSkill.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["AdditionalSkillset"]).Trim();
                txttofilledIn.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ToBeFilledIn_Days"]).Trim();
                txtSalaryRangeFrom.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["SalaryRangefrom_Lakh_Year"]).Trim();
                txtSalaryRangeTo.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["SalaryRangeto_Lakh_Year"]).Trim();
                lstReasonForRequi.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ReasonRequisition_ID"]).Trim();
                lstPreferredEmpType.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PID"]).Trim();
                lstPositionBand.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["BAND"]).Trim();

                txtEssentialQualifi.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["EssentialQualification"]).Trim();
                txtDesiredQualifi.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["DesiredQualification"]).Trim();
                txtRequiredExperiencefrom.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Required_ExperienceFrom_Years"]).Trim();
                txtRequiredExperienceto.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Required_ExperienceTo_Years"]).Trim();
                // txtRequiredExperienceto.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["NoOfPosition"]).Trim();
                lstRecommPerson.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RecommendedPerson"]).Trim();
				GetInterviewer(Convert.ToInt32(dsRecruitmentDetails.Tables[0].Rows[0]["ModuleId"]));
				txtComments.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Comments"]).Trim();
                txtInterviewerOptOne.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter1_OPT"]).Trim();
                txtInterviewerOptTwo.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter2_OPT"]).Trim();
                lstInterviewerOne.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter1"]).Trim();
                lstInterviewerTwo.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter2"]).Trim();
                listRecruiter.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RecruiterID"]).Trim();
                lnkuplodedfile.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["UploadData"]).Trim();
                hdnAssignQuestiID.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["AssignQuestionnaire_ID"]).Trim();
                FileName.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["UploadData"]).Trim();
                hflStatusID.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Status_ID"]).Trim();
				//hflStatusName.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Request_status"]).Trim();
				//HDNISDraft.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ISDraft"]).Trim();
				//hdnRecrStatus.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RecruitmentStatus"]).Trim();
				hdnBankDetailID.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["JD_BankDetail_ID"]).Trim();
				txtJobDescription.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["JobDescription"]).Trim();
				//GetFilterGD();

				int DeptID = 0;
				DeptID = Convert.ToInt32(lstPositionDept.SelectedValue);
				dtEmp = spm.Get_Requisition_EmployeeHOD(hdnEmpCodePrve.Value);
                if (dtEmp.Rows.Count > 0)
                {
                    hdnHOD.Value = "HOD";
                    if (Convert.ToInt32(hflStatusID.Value) == 3)
                    {
                        getHOD_Cancel_Approvallist(hdnEmpCodePrve.Value, hdnRecruitment_ReqID.Value, DeptID);
                    }
                    else
                    {
                        getHODApproverlist(hdnEmpCodePrve.Value, hdnRecruitment_ReqID.Value, DeptID);
                    }
                         getLWPML_HR_ApproverCode("");
                         hdnstaus.Value = "Final Approver";
                }
                else
                {
                        hdnHOD.Value = "Employee";
                    if (Convert.ToInt32(hflStatusID.Value) == 3)
                    {
                        get_Cancel_Approverlist(hdnEmpCodePrve.Value, hdnRecruitment_ReqID.Value, DeptID);
                    }
                    else
                    {
                        getApproverlist(hdnEmpCodePrve.Value, hdnRecruitment_ReqID.Value);
                    }
                }
                Div_Oth.Visible = true;
                lnkbtn_expdtls.Text = "-";
                DivTrvl.Visible = true;
                trvl_localbtn.Text = "-";
                Div_Locl.Visible = true;
                //DivAccm.Visible = true;
                trvl_accmo_btn.Text = "-";
                Div_Locl.Visible = true;
                trvl_localbtn.Text = "-";

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    private void get_Cancel_Approverlist(string EmpCode, string RecrutID,int DeptID)
    {
        DataTable dtapprover = new DataTable();
        //string strleavetype = Convert.ToString(txtLeaveType.Text).Trim();       
        dtapprover = spm.GetRequisitionApproverStatus_Cancellation(EmpCode, RecrutID,DeptID);
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();
        if (dtapprover.Rows.Count > 0)
        {
            DgvApprover.DataSource = dtapprover;
            DgvApprover.DataBind();
        }
    }
    private void getHOD_Cancel_Approvallist(string EmpCode, string RecrutID,int DeptID)
    {
        var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
        var getcompSelectedval = lstPositionLoca.SelectedItem.Value;
        var qtype = "GETREQUISITIONAPPROVERSTATUS";// GETREQUISITIONHODAPPROVERSTATUS
        var qtypeHOD = "GETREQUISITIONHODAPPROVERSTATUS";
        if (getcompSelectedText.Contains("Head Office"))
        {
            qtype = "GETREQUISITIONAPPROVERSTATUS_COMP";
            qtypeHOD = "GETREQUISITIONHODAPPROVERSTATUS_COMP"; 
        }
        DataTable dtapprover = new DataTable();
        //string strleavetype = Convert.ToString(txtLeaveType.Text).Trim();       
        dtapprover = spm.GetRequisitionHODApproverStatus_Cancel(EmpCode, RecrutID,DeptID,getcompSelectedval,qtypeHOD);
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();
        if (dtapprover.Rows.Count > 0)
        {
            DgvApprover.DataSource = dtapprover;
            DgvApprover.DataBind();
        }
    }
    private void getHODApproverlist(string EmpCode, string RecrutID,int DeptID)
    {
        var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
        var getcompSelectedval = lstPositionLoca.SelectedItem.Value;
        var qtype = "GETREQUISITIONAPPROVERSTATUS";// GETREQUISITIONHODAPPROVERSTATUS
        var qtypeHOD = "GETREQUISITIONHODAPPROVERSTATUS";
        if (getcompSelectedText.Contains("Head Office"))
        {
            qtype = "GETREQUISITIONAPPROVERSTATUS_COMP";
            qtypeHOD = "GETREQUISITIONHODAPPROVERSTATUS_COMP";
        }
        DataTable dtapprover = new DataTable();
        //string strleavetype = Convert.ToString(txtLeaveType.Text).Trim();       
        dtapprover = spm.GetRequisitionHODApproverStatus(EmpCode, RecrutID, DeptID,getcompSelectedval,qtypeHOD);
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();
        if (dtapprover.Rows.Count > 0)
        {
            DgvApprover.DataSource = dtapprover;
            DgvApprover.DataBind();
        }
    }

    protected void lstPositionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((Convert.ToString(lstPositionName.SelectedValue).Trim() != "" || Convert.ToString(lstPositionName.SelectedValue).Trim() != "0") && (Convert.ToString(lstSkillset.SelectedValue).Trim() != "" || Convert.ToString(lstSkillset.SelectedValue).Trim() != "0"))
        {
            GetFilterGD();
        }
    }
    protected void lstSkillset_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((Convert.ToString(lstPositionName.SelectedValue).Trim() != "" || Convert.ToString(lstPositionName.SelectedValue).Trim() != "0") && (Convert.ToString(lstSkillset.SelectedValue).Trim() != "" || Convert.ToString(lstSkillset.SelectedValue).Trim() != "0"))
        {
            GetFilterGD();
        }

    }
    private void GetFilterGD()
    {
        int Quest_ID = 0;
        txtJobDescription.Text = "";
        DataTable JDBankList = new DataTable();
        string Stype = "getAssignJDBank_Filter";
        JDBankList = spm.GetAssign_JDBankFilter(Stype, Convert.ToInt32(lstSkillset.SelectedValue), Convert.ToInt32(lstPositionName.SelectedValue));
        if (JDBankList.Rows.Count > 0)
        {
            txtJobDescription.Text = Convert.ToString(JDBankList.Rows[0]["JobDescription"]).Trim();
            hdnBankDetailID.Value = Convert.ToString(JDBankList.Rows[0]["JD_BankDetail_ID"]).Trim();
            //mobile_cancel.Visible = true;
        }
    }
    protected void check_ISHR()
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "CheckHR_SelectedLeaveRqst_isPending";
            spars[1] = new SqlParameter("@req_id", SqlDbType.Int);
            spars[1].Value = hdnRecruitment_ReqID.Value;
            spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[2].Value = hdnEmpCpde.Value;
            dsTrDetails = spm.getDatasetList(spars, "Usp_getRecruitmentEmp_Details_All");
            //Travel Request Count
            hdnisApprover_TDCOS.Value = "Approver";
            if (Convert.ToString(hdnhrappType.Value).Trim() != "1")
            {
                if (dsTrDetails.Tables[0].Rows.Count > 0)
                {
                    hdnisApprover_TDCOS.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_type"]).Trim();
                    //  btnReject.Visible = false;
                    hdnApproverTDCOS_status.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Action"]).Trim();
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    public void getLWPML_HR_ApproverCode(string strtype)
    {

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "LWPML_HREmpCode";
        spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[2].Value = hdnEmpCodePrve.Value;
        dsTrDetails = spm.getDatasetList(spars, "Usp_getRecruitmentEmp_Details_All");
        //Travel Desk Approver Code
        hdnisApprover_TDCOS.Value = "Approver";
        if (Convert.ToString(hdnhrappType.Value).Trim() != "1")
        {
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                hdnnextappcode.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_code"]).Trim();
                hdnapprid.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_id"]).Trim();
                hdnApproverid_LWPPLEmail.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();
                hdnisApprover_TDCOS.Value = "NA";
            }
        }
    }
   
    private void getApproverlist()
    {
        var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
        var getcompSelectedval = lstPositionLoca.SelectedItem.Value;
        var qtype = "GETREQUISITIONAPPROVERSTATUS";// GETREQUISITIONHODAPPROVERSTATUS
        //var qtypeHOD = "GETREQUISITIONHODAPPROVERSTATUS";
        if (getcompSelectedText.Contains("Head Office"))
        {
            qtype = "GETREQUISITIONAPPROVERSTATUS_COMP";
            //qtypeHOD = "GETREQUISITIONHODAPPROVERSTATUS_COMP";
        }
        DataTable dtapprover = new DataTable();
		//string strleavetype = Convert.ToString(txtLeaveType.Text).Trim();  
		int DeptID = Convert.ToInt32(lstPositionDept.SelectedValue);
        dtapprover = spm.GetRequisitionApproverStatus(hdnEmpCpde.Value, hdnRecruitment_ReqID.Value, DeptID,getcompSelectedval,qtype);
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();
        if (dtapprover.Rows.Count > 0)
        {
            DgvApprover.DataSource = dtapprover;
            DgvApprover.DataBind();
        }
    }
    
    #endregion

    

    


    
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(txtReqNumber.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Requisition Number";
            return;
        }
        

    }

    protected void trvl_localbtn_Click(object sender, EventArgs e)
    {
               
        if (Div_Locl.Visible)
        {
            Div_Locl.Visible = false;
            trvl_localbtn.Text = "+";
        }
        else
        {
            Div_Locl.Visible = true;
            trvl_localbtn.Text = "-";
        }

    }

    protected void localtrvl_cancel_btn_Click(object sender, EventArgs e)
    {

       

    }

   


    protected void lnkbtn_expdtls_Click(object sender, EventArgs e)
    {
        if (Div_Oth.Visible)
        {
            Div_Oth.Visible = false;
            lnkbtn_expdtls.Text = "+";
        }
        else
        {
            Div_Oth.Visible = true;
            lnkbtn_expdtls.Text = "-";
        }
    }
    private void getApproverlist(string EmpCode, string RecrutID)
    {
        var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
        var getcompSelectedval = lstPositionLoca.SelectedItem.Value;
        var qtype = "GETREQUISITIONAPPROVERSTATUS";// GETREQUISITIONHODAPPROVERSTATUS
        //var qtypeHOD = "GETREQUISITIONHODAPPROVERSTATUS";
        if (getcompSelectedText.Contains("Head Office"))
        {
            qtype = "GETREQUISITIONAPPROVERSTATUS_COMP";
            //qtypeHOD = "GETREQUISITIONHODAPPROVERSTATUS_COMP";
        }
        DataTable dtapprover = new DataTable();
		//string strleavetype = Convert.ToString(txtLeaveType.Text).Trim(); 
		int DeptID = Convert.ToInt32(lstPositionDept.SelectedValue);
		dtapprover = spm.GetRequisitionApproverStatus(EmpCode, RecrutID, DeptID,getcompSelectedval,qtype);
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();
        if (dtapprover.Rows.Count > 0)
        {
            DgvApprover.DataSource = dtapprover;
            DgvApprover.DataBind();
        }
    }

    
}