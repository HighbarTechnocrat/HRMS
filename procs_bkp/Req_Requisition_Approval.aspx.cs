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
public partial class Req_Requisition_Approval : System.Web.UI.Page
{
    #region CreativeMethods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "", strapprovermails;
    public int did = 0;
    SP_Methods spm = new SP_Methods();
    Recruitment_Requisition_Parameter Req = new Recruitment_Requisition_Parameter();
    //Code for Request Details Voew
    public DataTable dtTrDetails, dtApprovalDetails, dtRecruitment, dtEmp, dtIntermediate, dtextraApp, dtInterviewer, dtfilter;
    String strloginid = "";
    String strempcode = "", Approvers_code;
    string Leavestatus = "";
    string IsApprover = "";
    string nxtapprcode;
    string strFilter = "";
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
                    txtNoofPosition.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    //txtSalaryRangeFrom.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txttofilledIn.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    // txtRequiredExperiencefrom.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    //txtRequiredExperienceto.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");

                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["QuestionnaireDocumentpath"]).Trim());

                    //GetRequisitionNo();
                    CheckHREmployee();

                    GetSkillsetName();
                    GetPositionName();
                    GetPositionCriticality();
                    GetDepartmentMaster();
					GetRec_Req_Location_Prosepect();
                    GetReasonRequisition();
                    GetPositionDesign();
                    GetPreferredEmpType();
                    GetlstPositionBand();
                    GetInterviewer(0);

                    GetRecruiter();
                    // trvldeatils_delete_btn.Visible = false;
                    if (Request.QueryString.Count > 0)
                    {
                        hdnRecruitment_ReqID.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        CheckExtraApprovalEmp();
                        checkApprovalStatus_Submit();
                        GetCuurentApprID();
                        GetRecruitmentDetail();
                        check_ISHR();
                        PopulateEmployeeData();
                        // trvldeatils_delete_btn.Visible = true;
                        lblheading.Text = "Approval Recruitment Requisition ";
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
    public void CheckHREmployee()
    {
        try
        {
            dtEmp = spm.Get_HRRequisition_Employee(Convert.ToString(Session["Empcode"]).Trim());
            if (dtEmp.Rows.Count > 0)
            {
                hdnHRDept.Value = (string)dtEmp.Rows[0]["Department_Name"].ToString().Trim();
            }


            //hflEmpDesignation.Value = (string)dtEmp.Rows[0]["DesginationName"].ToString().Trim();
            //hflEmailAddress.Value = (string)dtEmp.Rows[0]["Emp_EmailAddress"];
            // if ((string)dtEmp.Rows[0]["Department_Name"].ToString().Trim() == "Human Resources")


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
                hdnExtraAPPID.Value = (string)dtextraApp.Rows[0]["Appr_id"].ToString().Trim();
                //hdnHRDept.Value = (string)dtEmp.Rows[0]["Action"].ToString().Trim();
            }
            if (hdnExtraAPP.Value.ToString().Trim() == "HR")
            {
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
                //txtJobDescription.Enabled = false;
                localtrvl_delete_btn.Enabled = false;
                accmo_cancel_btn.Enabled = false;
                localtrvl_delete_btn.Visible = false;
                accmo_cancel_btn.Visible = false;

                txtComments.Enabled = false;
                lstInterviewerTwo.Enabled = false;
                lstInterviewerOne.Enabled = false;
                txtInterviewerOptOne.Enabled = false;
                txtInterviewerOptTwo.Enabled = false;
                Recruiter.Visible = true;
                trvldeatils_btnSave.Visible = false;
                trvldeatils_delete_btn.Visible = false;
                lnkuplodedfile.Style.Add("padding-bottom", "15px");
                localtrvl_btnSave.Text = "Assign To Recruiter";
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }
    }
    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            int Quest_ID = 0;
            DataTable QuestionnaireList = new DataTable();
            string Stype = "FilterQuestionnaire";
            #region Check For Blank Fields
            if (Convert.ToString(lstSkillsetQue.SelectedValue).Trim() == "" || Convert.ToString(lstSkillsetQue.SelectedValue).Trim() == "0")
            {
                Label2.Text = "Please select Skill Set";
                ModalPopupExtenderLogin.Show();
                return;

            }
            if (Convert.ToString(lstPositionQue.SelectedValue).Trim() == "" || Convert.ToString(lstPositionQue.SelectedValue).Trim() == "0")
            {
                Label2.Text = "Please select Position Title";
                ModalPopupExtenderLogin.Show();
                return;
            }
            //Label2.Visible = false;
            //string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 

            #endregion
            QuestionnaireList = spm.GetAssign_QuestionnaireFilter(Stype, Convert.ToInt32(lstSkillsetQue.SelectedValue), Convert.ToInt32(lstPositionQue.SelectedValue));
            gvMngTravelRqstList.DataSource = null;
            gvMngTravelRqstList.DataBind();
            if (QuestionnaireList.Rows.Count > 0)
            {
                Label2.Text = "";
                gvMngTravelRqstList.DataSource = QuestionnaireList;
                gvMngTravelRqstList.DataBind();
                mobile_cancel.Visible = true;
            }
            else
            {
                Label2.Text = "No Questionnaires for the selected combination. Please contact Administrator.";
                mobile_cancel.Visible = false;
            }
            ModalPopupExtenderLogin.Show();
        }
        catch (Exception ex)
        {

        }

    }

    [System.Web.Services.WebMethod]
    public static List<string> SearchInterviewerOne(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";

                strsql = "  Select t.empname from  ( " +
                               "  Select Emp_Code + '-'  +Emp_Name as empname " +
                               "  from tbl_Employee_Mst  " +
                               "   where emp_status='Onboard' " +
                               "    " +
                               " ) t " +
                               " where t.empname like '%' + @SearchText + '%'   Order by t.empname ";

                cmd.CommandText = strsql;
                cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));

                ////                }


                cmd.Connection = conn;
                conn.Open();
                List<string> employees = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        employees.Add(sdr["empname"].ToString());
                    }
                }
                conn.Close();
                return employees;
            }
        }
    }


    
    #region PageMethods

    private void checkApprovalStatus_Submit()
    {
        try
        {
            // DataTable dtTrDetails = new DataTable();

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "check_RequisitionAppr_Status";

            spars[1] = new SqlParameter("@req_id", SqlDbType.Int);
            spars[1].Value = Convert.ToInt32(hdnRecruitment_ReqID.Value);

            spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(hdnEmpCpde.Value).Trim();
            dtTrDetails = spm.getMobileRemDataList(spars, "Usp_getRecruitmentEmp_Details_All");
            if (dtTrDetails.Rows.Count == 0)
            {
                Response.Redirect("~/procs/Req_RequisitionIndex.aspx?itype=Pending");
                // GetecruitmentDetail();
            }

            if (dtTrDetails.Rows.Count > 0)
            {
                if (Convert.ToString(dtTrDetails.Rows[0]["pvappstatus"]) != "Pending")
                {
                    //GetecruitmentDetail();
                    Response.Redirect("~/procs/Req_RequisitionIndex.aspx?itype=Pending");
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
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

            lstSkillsetQue.DataSource = dtSkillset;
            lstSkillsetQue.DataTextField = "ModuleDesc";
            lstSkillsetQue.DataValueField = "ModuleId";
            lstSkillsetQue.DataBind();
            lstSkillsetQue.Items.Insert(0, new ListItem("Select Skillset", "0"));

            lstDGSkillset.DataSource = dtSkillset;
            lstDGSkillset.DataTextField = "ModuleDesc";
            lstDGSkillset.DataValueField = "ModuleId";
            lstDGSkillset.DataBind();
            lstDGSkillset.Items.Insert(0, new ListItem("Select Skillset", "0"));


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

            lstPositionQue.DataSource = dtPositionName;
            lstPositionQue.DataTextField = "PositionTitle";
            lstPositionQue.DataValueField = "PositionTitle_ID";
            lstPositionQue.DataBind();
            lstPositionQue.Items.Insert(0, new ListItem("Select Position", "0"));

            lstDGPosition.DataSource = dtPositionName;
            lstDGPosition.DataTextField = "PositionTitle";
            lstDGPosition.DataValueField = "PositionTitle_ID";
            lstDGPosition.DataBind();
            lstDGPosition.Items.Insert(0, new ListItem("Select Position", "0"));
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
			//lstPositionDept.Enabled = false;
			////updated code
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
    public void GetRec_Req_Location_Prosepect()
    {
        DataTable lstPosition = new DataTable();
        lstPosition = spm.GetRec_Req_Location_Prosepect();
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
    //public void GetInterviewer1()
    //{

    //    dtInterviewer = spm.GetRecruitment_Req_Employee_Mst();
    //    if (dtInterviewer.Rows.Count > 0)
    //    { }
    //}
    public void GetInterviewer(int ModuleId)
    {
        DataTable dtInterv = new DataTable();
        //dtInterv = spm.GetRecruitment_Req_Screeners_Mst(ModuleId);
		SqlParameter[] spars1 = new SqlParameter[5];
		spars1[0] = new SqlParameter("@Stype", SqlDbType.VarChar);
		spars1[0].Value = "sp_Req_Screeners_Mst_New";
		spars1[1] = new SqlParameter("@moduleID", SqlDbType.Int);
		spars1[1].Value = ModuleId;
		//spars1[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
		//spars1[2].Value = Convert.ToString(hdnEmpCpde.Value).Trim();
		dtInterv = spm.getMobileRemDataList(spars1, "SP_GETREQUISTIONLIST_DETAILS");
		if (dtInterv.Rows.Count > 0)
		{
            lstInterviewerOne.DataSource = dtInterv;
            lstInterviewerOne.DataTextField = "EmployeeName";
            lstInterviewerOne.DataValueField = "EmployeeCode";
            lstInterviewerOne.DataBind();
		}
		lstInterviewerOne.Items.Insert(0, new ListItem("Select Screening By", "0"));


	}
	public void GetRecruiter()
    {
        DataTable dtInterviewer = new DataTable();
		//dtInterviewer = spm.GetRecruitment_Recruiter();
		SqlParameter[] spars1 = new SqlParameter[5];
		spars1[0] = new SqlParameter("@Stype", SqlDbType.VarChar);
		spars1[0].Value = "sp_Req_Recruiter_New";
		//spars1[1] = new SqlParameter("@moduleID", SqlDbType.Int);
		//spars1[1].Value = ModuleId;
		dtInterviewer = spm.getMobileRemDataList(spars1, "SP_GETREQUISTIONLIST_DETAILS");
		if (dtInterviewer.Rows.Count > 0)
        {
            listRecruiter.DataSource = dtInterviewer;
            listRecruiter.DataTextField = "EmpName";
            listRecruiter.DataValueField = "Emp_Code";
            listRecruiter.DataBind();
            listRecruiter.Items.Insert(0, new ListItem("Select Recruiter", "0"));
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


                lstPositionDesign.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Designation_iD"]).Trim();
                // lstReasonForRequi.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ReasonRequisition_ID"]).Trim();
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
                lstRecommPerson.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RecommendedPerson"]).Trim();
                txtComments.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Comments"]).Trim();				
				GetInterviewer(Convert.ToInt32(dsRecruitmentDetails.Tables[0].Rows[0]["ModuleId"]));

				txtInterviewerOptOne.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter1_OPT"]).Trim();
                txtInterviewerOptTwo.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter2_OPT"]).Trim();
                lstInterviewerOne.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter1"]).Trim();
                lstInterviewerTwo.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter2"]).Trim();
                lnkuplodedfile.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["UploadData"]).Trim();
                hdnAssignQuestiID.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["AssignQuestionnaire_ID"]).Trim();
				hdnBankDetailID.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["JD_BankDetail_ID"]).Trim();
				txtJobDescription.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["JobDescription"]).Trim();
				FileName.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["UploadData"]).Trim();
								
				//GetFilterGD();
				dtEmp = spm.Get_Requisition_EmployeeHOD(hdnEmpCodePrve.Value);
				int DeptID = Convert.ToInt32(lstPositionDept.SelectedValue);
				if (dtEmp.Rows.Count > 0)
                {
                    var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
                    var getcompSelectedval = lstPositionLoca.SelectedItem.Value;
                    var qtype = "get_NextApproverDetails_mail_COMP";// GETREQUISITIONHODAPPROVERSTATUS
                                                               //var qtypeHOD = "GETREQUISITIONHODAPPROVERSTATUS";
                    if (getcompSelectedText.Contains("Head Office"))
                    {
                        qtype = "get_NextApproverDetails_mail";
                        //qtypeHOD = "GETREQUISITIONHODAPPROVERSTATUS_COMP";
                    }
                    hdnHOD.Value = "HOD";
					getApproverlist(hdnEmpCodePrve.Value, hdnRecruitment_ReqID.Value, DeptID);
					//getIntermidateslist();
					DataTable dsapproverNxt = new DataTable();
					dsapproverNxt = spm.GetNextRecruit_ApproverDetails(hdnEmpCodePrve.Value, Convert.ToInt32(hdnRecruitment_ReqID.Value), DeptID,getcompSelectedval,qtype);
					if (dsapproverNxt.Rows.Count > 0)
					{
						apprid = (int)dsapproverNxt.Rows[0]["APPR_ID"];
						nxtapprcode = (string)dsapproverNxt.Rows[0]["A_EMP_CODE"];
						nxtapprname = (string)dsapproverNxt.Rows[0]["Emp_Name"];
						approveremailaddress = (string)dsapproverNxt.Rows[0]["Emp_Emailaddress"];
						hdnnextappcode.Value = nxtapprcode;
						hdnapprid.Value = apprid.ToString();
						hflApproverEmail.Value = approveremailaddress;
					}
					else
					{
						hdnstaus.Value = "Final Approver";
						//For  Previous approver   
						//getPreviousApprovesEmailList();
						getLWPML_HR_ApproverCode("");
						hdnIntermediateEmail.Value = "";
					}
				}
                else
                {
                    hdnHOD.Value = "Employee";
                    var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
                    var getcompSelectedval = lstPositionLoca.SelectedItem.Value;
                    var qtype = "get_NextApproverDetails_mail_COMP";// GETREQUISITIONHODAPPROVERSTATUS                                                              
                    if (getcompSelectedText.Contains("Head Office"))
                    {
                        qtype = "get_NextApproverDetails_mail";
                    }
                    getApproverlist(hdnEmpCodePrve.Value, hdnRecruitment_ReqID.Value, DeptID);
                    //getIntermidateslist();
                    DataTable dsapproverNxt = new DataTable();
                    dsapproverNxt = spm.GetNextRecruit_ApproverDetails(hdnEmpCodePrve.Value, Convert.ToInt32(hdnRecruitment_ReqID.Value), DeptID,getcompSelectedval,qtype);
                    if (dsapproverNxt.Rows.Count > 0)
                    {
                        apprid = (int)dsapproverNxt.Rows[0]["APPR_ID"];
                        nxtapprcode = (string)dsapproverNxt.Rows[0]["A_EMP_CODE"];
                        nxtapprname = (string)dsapproverNxt.Rows[0]["Emp_Name"];
                        approveremailaddress = (string)dsapproverNxt.Rows[0]["Emp_Emailaddress"];
                        hdnnextappcode.Value = nxtapprcode;
                        hdnapprid.Value = apprid.ToString();
                        hflApproverEmail.Value = approveremailaddress;

                        DataTable dtIntermediateEmail = new DataTable();
                        dtIntermediateEmail = spm.GetNextRecruit_IntermediateName(Convert.ToInt32(hdnCurrentID.Value), Convert.ToInt32(hdnRecruitment_ReqID.Value), hdnEmpCodePrve.Value);
                        if (dtIntermediateEmail.Rows.Count > 0)
                        {
                            hdnIntermediateEmail.Value = (string)dtIntermediateEmail.Rows[0]["Emp_Emailaddress"];
                        }
                    }
                    else
                    {
                        hdnstaus.Value = "Final Approver";
                        //For  Previous approver   
                        getPreviousApprovesEmailList();
                        getLWPML_HR_ApproverCode("");
                        hdnIntermediateEmail.Value = "";
                       
                    }
                }
                Div_Oth.Visible = true;
                lnkbtn_expdtls.Text = "-";
                //DivTrvl.Visible = true;
                //btnTra_Details.Text = "-";
                //DivAccm.Visible = true;
                //trvl_accmo_btn.Text = "-";
                //Div_Locl.Visible = true;
                //trvl_localbtn.Text = "-";
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    private void getHODApproverlist(string EmpCode, string RecrutID,int DeptID)
    {
        DataTable dtapprover = new DataTable();
        //string strleavetype = Convert.ToString(txtLeaveType.Text).Trim();       
        dtapprover = spm.GetRequisitionHODApproverStatus(EmpCode, RecrutID, DeptID,"","");
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();
        if (dtapprover.Rows.Count > 0)
        {
            DgvApprover.DataSource = dtapprover;
            DgvApprover.DataBind();
        }
    }
    protected void getIntermidateslist()
    {
        dtIntermediate = new DataTable();
        dtIntermediate = spm.Get_Requisition_IntermediateName(hdnEmpCodePrve.Value);
        if (dtIntermediate.Rows.Count > 0)
        {
            lstIntermediate.DataSource = dtIntermediate;
            lstIntermediate.DataTextField = "Emp_Name";
            lstIntermediate.DataValueField = "A_EMP_CODE";
            //lstIntermediate.DataValueField = "APPR_ID";
            lstIntermediate.DataBind();
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
    private void GetFilterGD()
    {
        int Quest_ID = 0;
        txtJobDescription.Text = "";
		hdnBankDetailID.Value = "";
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
	private void GetQuestionnaire()
	{
		int Quest_ID = 0;
		DataTable QuestionnaireList = new DataTable();
		string Stype = "FilterQuestionnaire";
		lnkuplodedfile.Text = "";
		hdnAssignQuestiID.Value = "";
		QuestionnaireList = spm.GetAssign_QuestionnaireFilter(Stype, Convert.ToInt32(lstSkillset.SelectedValue), Convert.ToInt32(lstPositionName.SelectedValue));
		if (QuestionnaireList.Rows.Count > 0)
		{
			lnkuplodedfile.Text = Convert.ToString(QuestionnaireList.Rows[0]["UploadData"]).Trim();
			hdnAssignQuestiID.Value = Convert.ToString(QuestionnaireList.Rows[0]["AssignQuestionnaire_ID"]).Trim();
			FileName.Value = lnkuplodedfile.Text;
		}


	}
	private void getApproverlist()
    {
        DataTable dtapprover = new DataTable();
		//string strleavetype = Convert.ToString(txtLeaveType.Text).Trim();     
		int DeptID = Convert.ToInt32(lstPositionDept.SelectedValue);
        dtapprover = spm.GetRequisitionApproverStatus(hdnEmpCpde.Value, hdnRecruitment_ReqID.Value, DeptID,"","");
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();
        if (dtapprover.Rows.Count > 0)
        {
            DgvApprover.DataSource = dtapprover;
            DgvApprover.DataBind();
        }
    }
    private void getPreviousApprovesEmailList()
    {
        DataTable dtPreApp = new DataTable();
        dtPreApp = spm.GetPrevious_Recruit_ApproverDetails(hdnEmpCodePrve.Value, Convert.ToInt32(hdnRecruitment_ReqID.Value));
        if (dtPreApp.Rows.Count > 0)
        {

            for (int i = 0; i < dtPreApp.Rows.Count; i++)
            {
                if (Convert.ToString(hflApproverEmail.Value).Trim() == "")
                {
                    hdnPreviousApprMails.Value = Convert.ToString(dtPreApp.Rows[i]["Emp_Emailaddress"]).Trim();
                }
                else
                {
                    hdnPreviousApprMails.Value += ";" + Convert.ToString(dtPreApp.Rows[i]["Emp_Emailaddress"]).Trim();
                }
            }
        }
    }
    #endregion

    protected void trvl_accmo_btn_Click(object sender, EventArgs e)
    {
        int Recruitment_ReqID = 0, AssignQuestiID = 0;
        string Stype = "RequestSubmitted";
        int ISDraft = 0;
        try
        {
            

            if (hdnHOD.Value == "HOD")
            {
               // SaveReRecruitmentHOD(Stype, ISDraft);
				SaveReRecruitment(Stype, ISDraft);
			}
            else
            {
                SaveReRecruitment(Stype, ISDraft);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
    {  //Reject Requisition 

		try
		{


			if (hdnHOD.Value == "HOD")
			{
				//RejectRecruitmentHOD();
				RejectRecruitment();
			}
			else
			{
				RejectRecruitment();
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
		
    }
	private void RejectRecruitment()
	{
		try
		{
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}
			if (Convert.ToString((txtApprovercmt.Text).Trim()) == "")
			{
				lblmessage.Text = "Please mention the comment before rejecting the Recruitment request";
				return;
			}

			strapprovermails = getApproversList_HR();
			string strInsertmediaterlist = "";
			// strInsertmediaterlist = GetIntermediatesList();

			string strHREmailForCC = "";
			//strHREmailForCC = Get_HREmail_ForCC(Convert.ToString(hdnEmpCodePrve.Value));
			if (Convert.ToString(strapprovermails).Trim() != "")
			{
				strapprovermails = ";" + strHREmailForCC;
			}
			else
			{
				strapprovermails = strHREmailForCC;
			}
			string RequiredByDate = "";
			RequiredByDate = GetRequiredByDate();
			string ApprovalList = "";
			spm.Rreject_Requisition_Request(Convert.ToInt32(hdnRecruitment_ReqID.Value), hdnEmpCpde.Value, Convert.ToInt32(hdnCurrentID.Value), txtApprovercmt.Text);
			ApprovalList = GetRequisitionApprove_RejectList(hdnEmpCodePrve.Value, hdnRecruitment_ReqID.Value, Convert.ToInt32(lstPositionDept.SelectedValue));
			spm.send_mail_Req_Requisition__Rejection_Correction(hflEmailAddress.Value, hdnLoginUserName.Value, "Recruitment - Rejection of Requisition " + txtReqNumber.Text + " Request", txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, txtApprovercmt.Text, strapprovermails, ApprovalList, txtReqName.Text, Convert.ToString(strInsertmediaterlist).Trim());

			Response.Redirect("~/procs/Req_RequisitionIndex.aspx?itype=Pending");
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}
	private void RejectRecruitmentHOD()
	{
		try
		{
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}
			if (Convert.ToString((txtApprovercmt.Text).Trim()) == "")
			{
				lblmessage.Text = "Please mention the comment before rejecting the Recruitment request";
				return;
			}
			strapprovermails = "";
			//strapprovermails = getApproversList_HR();
			string strInsertmediaterlist = "";

			string strHREmailForCC = "";
			//if (Convert.ToString(strapprovermails).Trim() != "")
			//{
			//	strapprovermails = ";" + strHREmailForCC;
			//}
			//else
			//{
			//	strapprovermails = strHREmailForCC;
			//}
			string RequiredByDate = "";
			RequiredByDate = GetRequiredByDate();
			string ApprovalList = "";
			spm.Rreject_Requisition_Request(Convert.ToInt32(hdnRecruitment_ReqID.Value), hdnEmpCpde.Value, Convert.ToInt32(hdnCurrentID.Value), txtApprovercmt.Text);
			ApprovalList = GetHODRequisitionApprove_RejectList(hdnEmpCodePrve.Value, hdnRecruitment_ReqID.Value.ToString(), Convert.ToInt32(lstPositionDept.SelectedValue));
			spm.send_mail_Req_Requisition__Rejection_Correction(hflEmailAddress.Value, hdnLoginUserName.Value, "Recruitment - Rejection of Requisition " + txtReqNumber.Text + " Request", txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, txtApprovercmt.Text, strapprovermails, ApprovalList, txtReqName.Text, Convert.ToString(strInsertmediaterlist).Trim());

			Response.Redirect("~/procs/Req_RequisitionIndex.aspx?itype=Pending");
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}

	protected void mobile_cancel_Click(object sender, EventArgs e)
    {

        lnkuplodedfile.Text = "";
        #region Check For Blank Fields
        int Count = gvMngTravelRqstList.Rows.Count;
        if (Count == 0)
        {
            Label2.Text = "Please select data ";
            ModalPopupExtenderLogin.Show();
            return;
        }
        HiddenField txtPostId = new HiddenField();
        HiddenField txtQutId = new HiddenField();

        foreach (GridViewRow gvrow in gvMngTravelRqstList.Rows)
        {
            CheckBox chk = (CheckBox)gvrow.FindControl("CheckBox1");
            if (chk != null & !chk.Checked)
            {
                Label2.Text = "Please select check box";
                ModalPopupExtenderLogin.Show();
                return;
            }
            else
            {
                txtPostId = (HiddenField)gvrow.FindControl("hfId");
                txtQutId = (HiddenField)gvrow.FindControl("AssignQuestiID");
            }
        }
        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        //if (confirmValue != "Yes")
        //{
        //    return;
        //}
        #endregion       
        //lnkuplodedfile.Attributes.Add("OnClientClick", "DownloadFile("+ txtPostId.Value +");");
        hdnAssignQuestiID.Value = txtQutId.Value;
        FileName.Value = txtPostId.Value;
        lnkuplodedfile.Text = txtPostId.Value;
    }

    private void SaveReRecruitment(string Stype, int ISDraft)
    {
        int Recruitment_ReqID = 0, AssignQuestiID = 0;
        //string Stype = "RequestSubmitted";
        string EmpCode = "", RecruiterCode = "";
        try
        {

            #region Check For Blank Fields


            if (Convert.ToString(txtReqNumber.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Requisition Number";
                return;
            }
            if (Convert.ToString(lstPositionName.SelectedValue).Trim() == "" || Convert.ToString(lstPositionName.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Position Title";
                return;
            }

            if (Convert.ToString(lstPositionCriti.SelectedValue).Trim() == "" || Convert.ToString(lstPositionCriti.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Position Criticality";
                return;
            }
            if (Convert.ToString(lstSkillset.SelectedValue).Trim() == "" || Convert.ToString(lstSkillset.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Skill Set";
                return;
            }
            if (Convert.ToString(lstPositionDept.SelectedValue).Trim() == "" || Convert.ToString(lstPositionDept.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Department Name";
                return;
            }
            //if (Convert.ToString(lstPositionDesign.SelectedValue).Trim() == "" || Convert.ToString(lstPositionDesign.SelectedValue).Trim() == "0")
            //{
            //    lblmessage.Text = "Please select Position Designation";
            //    return;
            //}
            if (Convert.ToString(lstPositionLoca.SelectedValue).Trim() == "" || Convert.ToString(lstPositionLoca.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Position Location";
                return;
            }
            if (Convert.ToString(lstPositionDept.SelectedValue).Trim() == "" || Convert.ToString(lstPositionName.SelectedValue).Trim() == "0")
            {
                if (Convert.ToString(lstPositionDept.Text).Trim() == "Other")
                {
                    if (Convert.ToString(txtOtherDept.Text).Trim() == "")
                    {
                        lblmessage.Text = "Please enter Other Department";
                        return;
                    }
                }
            }

            if (Convert.ToString(lstPositionDesign.SelectedValue).Trim() == "" || Convert.ToString(lstPositionDesign.SelectedValue).Trim() == "0")
            {
                if (Convert.ToString(lstPositionDesign.Text).Trim() == "Other")
                {
                    if (Convert.ToString(txtPositionDesig.Text).Trim() == "")
                    {
                        lblmessage.Text = "Please enter Position Designation Other";
                        return;
                    }
                }

            }
            if (Convert.ToString(txtNoofPosition.Text).Trim() == "" || Convert.ToString(txtNoofPosition.Text).Trim() == "0")
            {
                lblmessage.Text = "Please enter No of Position";
                return;
            }

            if (Convert.ToString(txttofilledIn.Text).Trim() == "" || Convert.ToString(txttofilledIn.Text).Trim() == "0")
            {
                lblmessage.Text = "Please  enter To Be Filled In(Days)";
                return;
            }
			
			if (Convert.ToString(lstReasonForRequi.SelectedValue).Trim() == "" || Convert.ToString(lstReasonForRequi.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Reason For Requisition";
                return;
            }

            if (Convert.ToString(lstPreferredEmpType.SelectedValue).Trim() == "" || Convert.ToString(lstPreferredEmpType.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Preferred Employment Type";
                return;
            }
            if (Convert.ToString(lstPositionBand.SelectedValue).Trim() == "" || Convert.ToString(lstPositionBand.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Band";
                return;
            }
			if (Convert.ToString(txtSalaryRangeFrom.Text).Trim() == "" || Convert.ToString(txtSalaryRangeFrom.Text).Trim() == "0")
			{
				lblmessage.Text = "Please  enter Salary Range From (Lakh/Year)";
				return;
			}
			if (Convert.ToString(txtSalaryRangeTo.Text).Trim() == "" || Convert.ToString(txtSalaryRangeTo.Text).Trim() == "0")
			{
				lblmessage.Text = "Please  enter Salary Range To (Lakh/Year)";
				return;
			}
			if (Convert.ToString(txtSalaryRangeFrom.Text).Trim() != "")
			{
				if (Convert.ToDecimal(txtSalaryRangeFrom.Text) > Convert.ToDecimal(txtSalaryRangeTo.Text))
				{
					lblmessage.Text = "Please  enter Salary Range To  greater than from(Lakh/Year)";
					return;
				}
			}

			if (Convert.ToString(txtRequiredExperiencefrom.Text).Trim() == "" || Convert.ToString(txtRequiredExperiencefrom.Text).Trim() == "0")
            {
                lblmessage.Text = "Please  enter  Required Experience(Year) From";
                return;
            }
            if (Convert.ToString(txtRequiredExperienceto.Text).Trim() == "" || Convert.ToString(txtRequiredExperienceto.Text).Trim() == "0")
            {
                lblmessage.Text = "Please  enter  Required Experience (Year) To";
                return;
            }
            if (Convert.ToString(txtRequiredExperiencefrom.Text).Trim() != "")
            {
                if (Convert.ToDecimal(txtRequiredExperiencefrom.Text) > Convert.ToDecimal(txtRequiredExperienceto.Text))
                {
                    lblmessage.Text = "Please  enter  Required Experience (Year) To  gheter than from Experience (Year)";
                    return;
                }
            }
            if (Convert.ToString(txtEssentialQualifi.Text).Trim() == "")
            {
                lblmessage.Text = "Please  enter Essential Qualification";
                return;
            }
            if (Convert.ToString(txtDesiredQualifi.Text).Trim() == "")
            {
                lblmessage.Text = "Please  enter Desired Qualification";
                return;
            }


			//if (Convert.ToString(txtJobDescription.Text).Trim() == "")
			//{
			//    lblmessage.Text = "Please  enter Job Description";
			//    return;
			//}

			if (Convert.ToString(txtJobDescription.Text).Trim() == "")
			{
				if (Convert.ToString(hdnBankDetailID.Value).Trim() == "")
				{
					lblmessage.Text = "No JD from bank for the selected combination. Please contact Administrator.";
					return;
				}
			}
			//if (Convert.ToString(hdnAssignQuestiID.Value).Trim() == "")
			//{
			//	lblmessage.Text = "No Questionnaires for the selected combination. Please contact Administrator.";
			//	return;
			//}
			if (Convert.ToString(lstInterviewerOne.SelectedValue).Trim() == "" || Convert.ToString(lstInterviewerOne.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Screening by ";
                return;
            }
            //if (hdnHRDept.Value.Trim() == "Human Resources")
            if (hdnExtraAPP.Value.ToString().Trim() == "HR")
            {
                if (Convert.ToString(listRecruiter.SelectedValue).Trim() == "" || Convert.ToString(listRecruiter.SelectedValue).Trim() == "0")
                {
                    lblmessage.Text = "Please select Recruiter";
                    return;
                }
            }

            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }

            #endregion

            Recruitment_ReqID = Convert.ToString(hdnRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdnRecruitment_ReqID.Value) : 0;
            AssignQuestiID = Convert.ToString(hdnAssignQuestiID.Value).Trim() != "" ? Convert.ToInt32(hdnAssignQuestiID.Value) : 0;

            //if (hdnHRDept.Value.Trim() == "Human Resources")
            if (hdnExtraAPP.Value.ToString().Trim() == "HR")
            {
                RecruiterCode = listRecruiter.SelectedValue;
            }

            if (Convert.ToString(Session["Empcode"]).Trim() != "" || Session["Empcode"] != null)
            {
                EmpCode = Convert.ToString(Session["Empcode"]).ToString();
            }


            Req.Recruitment_ReqID = Convert.ToString(hdnRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdnRecruitment_ReqID.Value) : 0;
            Req.Stype = Stype;
            Req.RequisitionNumber = txtReqNumber.Text;
            Req.RequisitionDate = DateTime.ParseExact(txtFromdate.Text.ToString(), "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            Req.Emp_Code = EmpCode;
            Req.PositionCriticality_ID = Convert.ToString(lstPositionCriti.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionCriti.SelectedValue) : 0;
            Req.PositionTitle_ID = Convert.ToString(lstPositionName.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionName.SelectedValue) : 0;
            Req.NoOfPosition = txtNoofPosition.Text;
            Req.Department_id = Convert.ToString(lstPositionDept.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionDept.SelectedValue) : 0;
            Req.PositionDesignationOther = txtPositionDesig.Text;

            Req.loc_code = Convert.ToString(lstPositionLoca.SelectedValue).Trim() != "" ? lstPositionLoca.SelectedValue : "0";
            Req.OtherDepartment = txtOtherDept.Text;

            Req.ModuleId = Convert.ToString(lstSkillset.SelectedValue).Trim() != "" ? Convert.ToInt32(lstSkillset.SelectedValue) : 0;
            Req.ReasonRequisition_ID = Convert.ToString(lstReasonForRequi.SelectedValue).Trim() != "" ? Convert.ToInt32(lstReasonForRequi.SelectedValue) : 0;
            Req.AdditionalSkillset = txtAdditionSkill.Text;
            Req.Designation_ID = Convert.ToString(lstPositionDesign.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionDesign.SelectedValue) : 0;

            Req.PID = Convert.ToString(lstPreferredEmpType.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPreferredEmpType.SelectedValue) : 0;
            Req.BAND = lstPositionBand.SelectedValue;
            Req.SalaryRangefrom_Lakh_Year = Convert.ToString(txtSalaryRangeFrom.Text).Trim() != "" ? Convert.ToDecimal(txtSalaryRangeFrom.Text) : 0;
            Req.SalaryRangeto_Lakh_Year = Convert.ToString(txtSalaryRangeTo.Text).Trim() != "" ? Convert.ToDecimal(txtSalaryRangeTo.Text) : 0;
            Req.ToBeFilledIn_Days = txttofilledIn.Text;
            Req.EssentialQualification = txtEssentialQualifi.Text;
            Req.DesiredQualification = txtDesiredQualifi.Text;

            Req.Required_ExperienceFrom_Years = Convert.ToString(txtRequiredExperiencefrom.Text).Trim() != "" ? Convert.ToDecimal(txtRequiredExperiencefrom.Text) : 0;
            Req.Required_ExperienceTo_Years = Convert.ToString(txtRequiredExperienceto.Text).Trim() != "" ? Convert.ToDecimal(txtRequiredExperienceto.Text) : 0;
            Req.RecommendedPerson = Convert.ToString(lstRecommPerson.SelectedValue).Trim();
            Req.JD_BankDetail_ID = Convert.ToString(hdnBankDetailID.Value).Trim() != "" ? Convert.ToInt32(hdnBankDetailID.Value) : 0;
            Req.Assign_Questionnaire_ID = Convert.ToString(hdnAssignQuestiID.Value).Trim() != "" ? Convert.ToInt32(hdnAssignQuestiID.Value) : 0;
            Req.Comments = txtComments.Text;
            Req.Emp_Code_Inter1 = Convert.ToString(lstInterviewerOne.SelectedValue).Trim();
            Req.Emp_Code_Inter2 = Convert.ToString(lstInterviewerTwo.SelectedValue).Trim();
            Req.Emp_Code_Inter1_OPT = "";// Convert.ToString(txtInterviewerOptOne.Text).Trim() != "" ? txtInterviewerOptOne.Text.Trim().Split('-')[0] : "";
            Req.Emp_Code_Inter2_OPT = "";// Convert.ToString(txtInterviewerOptTwo.Text).Trim() != "" ? txtInterviewerOptTwo.Text.Trim().Split('-')[0] : "";
            Req.RecruiterID = RecruiterCode;
            Req.Status_ID = 1;
            Req.ISDraft = ISDraft;
            dtRecruitment = spm.InsertRecruitmentRequisition(Req);

            String strRecRstURL = "";
            strRecRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_RequiCR"]).Trim() + "?Req_Requi_ID=" + Convert.ToInt32(dtRecruitment.Rows[0]["Recruit_ReqID"]);
            String strRecruitmentURL = "";
            strRecruitmentURL = Convert.ToString(ConfigurationManager.AppSettings["AssignRecLink_Requi"]).Trim() + "?Recruitment_ReqID=" + Convert.ToInt32(dtRecruitment.Rows[0]["Recruit_ReqID"]);

            if (Convert.ToString((hdnstaus.Value).Trim()) != "")
            {
                getLWPML_HR_ApproverCode("");
                if (Convert.ToString(hdnisApprover_TDCOS.Value).Trim() != "Approver" && Convert.ToString(hdnCurrentID.Value).Trim() != Convert.ToString(hdnapprid.Value).Trim())
                {
                    if (Convert.ToString(hdnisApprover_TDCOS.Value).Trim() != "Approver")
                    {
                        //strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_LR"]).Trim() + "?reqid=" + Convert.ToDecimal(hdnReqid.Value) + "&itype=1";
                        getLWPML_HR_ApproverCode("");
                        spm.Insert_Requisition_ApproverRequest(hdnnextappcode.Value, Convert.ToInt32(hdnapprid.Value), Convert.ToInt32(hdnRecruitment_ReqID.Value));
                    }
                    spm.Update_Requisition_AppRequest(Convert.ToInt32(hdnRecruitment_ReqID.Value), "Approved", txtApprovercmt.Text, Convert.ToString(("").Trim()), Convert.ToInt32(hdnCurrentID.Value));

                }
                else
                {
                    spm.Update_Requisition_AppRequest(Convert.ToInt32(hdnRecruitment_ReqID.Value), "Approved", txtApprovercmt.Text, Convert.ToString((hdnstaus.Value).Trim()), Convert.ToInt32(hdnCurrentID.Value));
                }
            }
            else
            {
                spm.Update_Requisition_AppRequest(Convert.ToInt32(hdnRecruitment_ReqID.Value), "Approved", txtApprovercmt.Text, Convert.ToString((hdnstaus.Value).Trim()), Convert.ToInt32(hdnCurrentID.Value));
            }
            strapprovermails = getApproversList_HR();
			int DeptID = Convert.ToInt32(lstPositionDept.SelectedValue);
			getApproverlist(hdnEmpCodePrve.Value, hdnRecruitment_ReqID.Value, DeptID);
            //string strApproverlist = "";
            //strApproverlist = GetRequisitionApprove_RejectList1(hdnEmpCpde.Value, (dtRecruitment.Rows[0]["Recruit_ReqID"]).ToString());
            string strInsertmediaterlist = "";
            strInsertmediaterlist = GetIntermediatesList();
			string RequiredByDate = "";
			RequiredByDate = GetRequiredByDate();
			if (Convert.ToString((hdnstaus.Value).Trim()) == "")
            {
                spm.Insert_Requisition_ApproverRequest(hdnnextappcode.Value, Convert.ToInt32(hdnapprid.Value), Convert.ToInt32(hdnRecruitment_ReqID.Value));
				string ApprovalList = "";
				ApprovalList = GetRequisitionApprove_RejectList(hdnEmpCodePrve.Value, hdnRecruitment_ReqID.Value, Convert.ToInt32(lstPositionDept.SelectedValue));

				spm.send_mailto_Req_Requisition_Next_Approver(txtReqName.Text, hflEmailAddress.Value, hflApproverEmail.Value, "Recruitment - Request for " + Convert.ToString(txtReqNumber.Text), txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text , lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtComments.Text, txtNoofPosition.Text, ApprovalList, Convert.ToString(strInsertmediaterlist), strRecRstURL);
                //strapprovermails = getApproversList_HR();
                //spm.send_mailto_Req_Requisition_Next_Approver_Intermediate(hflEmailAddress.Value, strapprovermails, "Request for " + Convert.ToString(txtReqNumber.Text), lstSkillset.SelectedItem.Text + "-" + lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtComments.Text, txtNoofPosition.Text, hdnLoginUserName.Value, txtApprovercmt.Text, GetRequisitionApprove_RejectList(hdnEmpCpde.Value, (dtRecruitment.Rows[0]["Recruit_ReqID"]).ToString()), txtReqName.Text, Convert.ToString(strInsertmediaterlist), "");
            }
            else
            {
                //hdnApproverid_LWPPLEmail.Value    hdnisApprover_TDCOS.Value = "Approver";
                if (Convert.ToString(hdnisApprover_TDCOS.Value).Trim() != "Approver")
                {
                    if (RecruiterCode == "")
                    {
						string ApprovalList = "";
						ApprovalList = GetRequisitionApprove_RejectList(hdnEmpCodePrve.Value, hdnRecruitment_ReqID.Value, Convert.ToInt32(lstPositionDept.SelectedValue));
						spm.send_mailto_Req_Requisition_Next_Approver(txtReqName.Text, hflEmailAddress.Value, hdnApproverid_LWPPLEmail.Value, "Recruitment - Request for " + Convert.ToString(txtReqNumber.Text), txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtComments.Text, txtNoofPosition.Text, ApprovalList, Convert.ToString(strInsertmediaterlist), strRecRstURL);
                    }
                    else
                    {
                        strapprovermails = getApproversList_HR();
                       // spm.send_mailto_Req_Requisition_Next_Approver_Intermediate(hflEmailAddress.Value, strapprovermails, "Recruitment - Request for " + Convert.ToString(txtReqNumber.Text), lstSkillset.SelectedItem.Text + "-" + lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtComments.Text, txtNoofPosition.Text, hdnLoginUserName.Value, txtApprovercmt.Text, GetRequisitionApprove_RejectList(hdnEmpCodePrve.Value, (dtRecruitment.Rows[0]["Recruit_ReqID"]).ToString()), txtReqName.Text, Convert.ToString(strInsertmediaterlist), "");
                        getRecruiteAssignCode(RecruiterCode);
						string ApprovalList = "";
						ApprovalList = GetRequisitionApprove_RejectList(hdnEmpCodePrve.Value, hdnRecruitment_ReqID.Value, Convert.ToInt32(lstPositionDept.SelectedValue));

						//Rec team mail send
						spm.send_mailto_Req_Requisition_Assign_Recruite(txtReqName.Text, hflEmailAddress.Value, hdnRecruiteEmailID.Value, "Recruitment - Request for " + Convert.ToString(txtReqNumber.Text), txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text , lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtComments.Text, txtNoofPosition.Text, ApprovalList, Convert.ToString(strInsertmediaterlist), strRecruitmentURL,txtJobDescription.Text.Trim(), txtEssentialQualifi.Text.Trim(), txtDesiredQualifi.Text.Trim(), txtComments.Text.Trim());
					}
                }
            }

			Response.Redirect("~/procs/Req_RequisitionIndex.aspx?itype=Pending");
		}
		catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
	
	private void SaveReRecruitmentHOD(string Stype, int ISDraft)
    {
        int Recruitment_ReqID = 0, AssignQuestiID = 0;
        //string Stype = "RequestSubmitted";
        string EmpCode = "", RecruiterCode = "";
        try
        {

            #region Check For Blank Fields


            if (Convert.ToString(txtReqNumber.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Requisition Number";
                return;
            }
            if (Convert.ToString(lstPositionName.SelectedValue).Trim() == "" || Convert.ToString(lstPositionName.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Position Title";
                return;
            }

            if (Convert.ToString(lstPositionCriti.SelectedValue).Trim() == "" || Convert.ToString(lstPositionCriti.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Position Criticality";
                return;
            }
            if (Convert.ToString(lstSkillset.SelectedValue).Trim() == "" || Convert.ToString(lstSkillset.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Skill Set";
                return;
            }
            if (Convert.ToString(lstPositionDept.SelectedValue).Trim() == "" || Convert.ToString(lstPositionDept.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Department Name";
                return;
            }
            //if (Convert.ToString(lstPositionDesign.SelectedValue).Trim() == "" || Convert.ToString(lstPositionDesign.SelectedValue).Trim() == "0")
            //{
            //    lblmessage.Text = "Please select Position Designation";
            //    return;
            //}
            if (Convert.ToString(lstPositionLoca.SelectedValue).Trim() == "" || Convert.ToString(lstPositionLoca.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Position Location";
                return;
            }
            if (Convert.ToString(lstPositionDept.SelectedValue).Trim() == "" || Convert.ToString(lstPositionName.SelectedValue).Trim() == "0")
            {
                if (Convert.ToString(lstPositionDept.Text).Trim() == "Other")
                {
                    if (Convert.ToString(txtOtherDept.Text).Trim() == "")
                    {
                        lblmessage.Text = "Please enter Other Department";
                        return;
                    }
                }
            }

            if (Convert.ToString(lstPositionDesign.SelectedValue).Trim() == "" || Convert.ToString(lstPositionDesign.SelectedValue).Trim() == "0")
            {
                if (Convert.ToString(lstPositionDesign.Text).Trim() == "Other")
                {
                    if (Convert.ToString(txtPositionDesig.Text).Trim() == "")
                    {
                        lblmessage.Text = "Please enter Position Designation Other";
                        return;
                    }
                }

            }
            if (Convert.ToString(txtNoofPosition.Text).Trim() == "" || Convert.ToString(txtNoofPosition.Text).Trim() == "0")
            {
                lblmessage.Text = "Please enter No of Position";
                return;
            }

            if (Convert.ToString(txttofilledIn.Text).Trim() == "" || Convert.ToString(txttofilledIn.Text).Trim() == "0")
            {
                lblmessage.Text = "Please  enter To Be Filled In(Days)";
                return;
            }
            //if (Convert.ToString(txtSalaryRangeFrom.Text).Trim() == "" || Convert.ToString(txtSalaryRangeFrom.Text).Trim() == "0")
            //{
            //    lblmessage.Text = "Please  enter Salary Range From (Lakh/Year)";
            //    return;
            //}
            //if (Convert.ToString(txtSalaryRangeTo.Text).Trim() == "" || Convert.ToString(txtSalaryRangeTo.Text).Trim() == "0")
            //{
            //    lblmessage.Text = "Please  enter Salary Range To (Lakh/Year)";
            //    return;
            //}

            //if (Convert.ToString(txtSalaryRangeFrom.Text).Trim() != "")
            //{
            //    if (Convert.ToDecimal(txtSalaryRangeFrom.Text) > Convert.ToDecimal(txtSalaryRangeTo.Text))
            //    {
            //        lblmessage.Text = "Please  enter Salary Range To  gheter than from(Lakh/Year)";
            //        return;
            //    }
            //}
            if (Convert.ToString(lstReasonForRequi.SelectedValue).Trim() == "" || Convert.ToString(lstReasonForRequi.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Reason For Requisition";
                return;
            }

            if (Convert.ToString(lstPreferredEmpType.SelectedValue).Trim() == "" || Convert.ToString(lstPreferredEmpType.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Preferred Employment Type";
                return;
            }
            if (Convert.ToString(lstPositionBand.SelectedValue).Trim() == "" || Convert.ToString(lstPositionBand.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Band";
                return;
            }

            if (Convert.ToString(txtRequiredExperiencefrom.Text).Trim() == "" || Convert.ToString(txtRequiredExperiencefrom.Text).Trim() == "0")
            {
                lblmessage.Text = "Please  enter  Required Experience(Year) From";
                return;
            }
            if (Convert.ToString(txtRequiredExperienceto.Text).Trim() == "" || Convert.ToString(txtRequiredExperienceto.Text).Trim() == "0")
            {
                lblmessage.Text = "Please  enter  Required Experience (Year) To";
                return;
            }
            if (Convert.ToString(txtRequiredExperiencefrom.Text).Trim() != "")
            {
                if (Convert.ToDecimal(txtRequiredExperiencefrom.Text) > Convert.ToDecimal(txtRequiredExperienceto.Text))
                {
                    lblmessage.Text = "Please  enter  Required Experience (Year) To  gheter than from Experience (Year)";
                    return;
                }
            }
            if (Convert.ToString(txtEssentialQualifi.Text).Trim() == "")
            {
                lblmessage.Text = "Please  enter Essential Qualification";
                return;
            }
            if (Convert.ToString(txtDesiredQualifi.Text).Trim() == "")
            {
                lblmessage.Text = "Please  enter Desired Qualification";
                return;
            }


			//if (Convert.ToString(txtJobDescription.Text).Trim() == "")
			//{
			//    lblmessage.Text = "Please  enter Job Description";
			//    return;
			//}

			if (Convert.ToString(txtJobDescription.Text).Trim() == "")
			{
				if (Convert.ToString(hdnBankDetailID.Value).Trim() == "")
				{
					lblmessage.Text = "No JD from bank for the selected combination. Please contact Administrator.";
					return;
				}
			}
			//if (Convert.ToString(hdnAssignQuestiID.Value).Trim() == "")
			//{
			//	lblmessage.Text = "No Questionnaires for the selected combination. Please contact Administrator.";
			//	return;
			//}
			if (Convert.ToString(lstInterviewerOne.SelectedValue).Trim() == "" || Convert.ToString(lstInterviewerOne.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Screening By";
                return;
            }


            //if (Convert.ToString(lstInterviewerTwo.SelectedValue).Trim() == "" || Convert.ToString(lstInterviewerTwo.SelectedValue).Trim() == "0")
            //{
            //    lblmessage.Text = "Please select Interviewer (2st Level)";
            //    return;
            //}

            //if (hdnHRDept.Value.Trim() == "Human Resources")
            if (hdnExtraAPP.Value.ToString().Trim() == "HR")
            {
                if (Convert.ToString(listRecruiter.SelectedValue).Trim() == "" || Convert.ToString(listRecruiter.SelectedValue).Trim() == "0")
                {
                    lblmessage.Text = "Please select Recruiter";
                    return;
                }
            }

            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }

            #endregion

            Recruitment_ReqID = Convert.ToString(hdnRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdnRecruitment_ReqID.Value) : 0;
            AssignQuestiID = Convert.ToString(hdnAssignQuestiID.Value).Trim() != "" ? Convert.ToInt32(hdnAssignQuestiID.Value) : 0;

            //if (hdnHRDept.Value.Trim() == "Human Resources")
            if (hdnExtraAPP.Value.ToString().Trim() == "HR")
            {
                RecruiterCode = listRecruiter.SelectedValue;
            }

            if (Convert.ToString(Session["Empcode"]).Trim() != "" || Session["Empcode"] != null)
            {
                EmpCode = Convert.ToString(Session["Empcode"]).ToString();
            }
            DateTime Fromdate = DateTime.ParseExact(txtFromdate.Text.ToString(), "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            //    dtRecruitment = spm.InsertRecruitmentRequisition(Stype, txtReqNumber.Text, Fromdate, EmpCode, Convert.ToInt32(lstPositionCriti.SelectedValue), Convert.ToInt32(lstPositionName.SelectedValue), txtNoofPosition.Text, Convert.ToInt32(lstPositionDept.SelectedValue), lstPositionLoca.SelectedValue,
            //txtOtherDept.Text, Convert.ToInt32(lstSkillset.SelectedValue),
            //Convert.ToInt32(lstReasonForRequi.SelectedValue), txtAdditionSkill.Text, Convert.ToInt32(lstPositionDesign.SelectedValue), txtPositionDesig.Text, Convert.ToInt32(lstPreferredEmpType.SelectedValue), lstPositionBand.SelectedValue, Convert.ToDecimal(txtSalaryRangeFrom.Text), Convert.ToDecimal(txtSalaryRangeTo.Text),
            //txttofilledIn.Text, txtEssentialQualifi.Text, txtDesiredQualifi.Text, Convert.ToDecimal(txtRequiredExperiencefrom.Text), Convert.ToDecimal(txtRequiredExperienceto.Text), txtRecommPerson.Text, Convert.ToInt32(hdnBankDetailID.Value), AssignQuestiID, txtComments.Text, lstInterviewerOne.SelectedValue, txtInterviewerOptOne.Text, lstInterviewerTwo.SelectedValue, txtInterviewerOptTwo.Text, RecruiterCode, "1", ISDraft, Recruitment_ReqID);
            Req.Recruitment_ReqID = Convert.ToString(hdnRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdnRecruitment_ReqID.Value) : 0;
            Req.Stype = Stype;
            Req.RequisitionNumber = txtReqNumber.Text;
            Req.RequisitionDate = DateTime.ParseExact(txtFromdate.Text.ToString(), "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            Req.Emp_Code = EmpCode;
            Req.PositionCriticality_ID = Convert.ToString(lstPositionCriti.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionCriti.SelectedValue) : 0;
            Req.PositionTitle_ID = Convert.ToString(lstPositionName.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionName.SelectedValue) : 0;
            Req.NoOfPosition = txtNoofPosition.Text;
            Req.Department_id = Convert.ToString(lstPositionDept.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionDept.SelectedValue) : 0;
            Req.PositionDesignationOther = txtPositionDesig.Text;

            Req.loc_code = Convert.ToString(lstPositionLoca.SelectedValue).Trim() != "" ? lstPositionLoca.SelectedValue : "0";
            Req.OtherDepartment = txtOtherDept.Text;

            Req.ModuleId = Convert.ToString(lstSkillset.SelectedValue).Trim() != "" ? Convert.ToInt32(lstSkillset.SelectedValue) : 0;
            Req.ReasonRequisition_ID = Convert.ToString(lstReasonForRequi.SelectedValue).Trim() != "" ? Convert.ToInt32(lstReasonForRequi.SelectedValue) : 0;
            Req.AdditionalSkillset = txtAdditionSkill.Text;
            Req.Designation_ID = Convert.ToString(lstPositionDesign.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionDesign.SelectedValue) : 0;

            Req.PID = Convert.ToString(lstPreferredEmpType.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPreferredEmpType.SelectedValue) : 0;
            Req.BAND = lstPositionBand.SelectedValue;
            Req.SalaryRangefrom_Lakh_Year = Convert.ToString(txtSalaryRangeFrom.Text).Trim() != "" ? Convert.ToDecimal(txtSalaryRangeFrom.Text) : 0;
            Req.SalaryRangeto_Lakh_Year = Convert.ToString(txtSalaryRangeTo.Text).Trim() != "" ? Convert.ToDecimal(txtSalaryRangeTo.Text) : 0;
            Req.ToBeFilledIn_Days = txttofilledIn.Text;
            Req.EssentialQualification = txtEssentialQualifi.Text;
            Req.DesiredQualification = txtDesiredQualifi.Text;
            
            Req.Required_ExperienceFrom_Years = Convert.ToString(txtRequiredExperiencefrom.Text).Trim() != "" ? Convert.ToDecimal(txtRequiredExperiencefrom.Text) : 0;
            Req.Required_ExperienceTo_Years = Convert.ToString(txtRequiredExperienceto.Text).Trim() != "" ? Convert.ToDecimal(txtRequiredExperienceto.Text) : 0;
            Req.RecommendedPerson = Convert.ToString(lstRecommPerson.SelectedValue).Trim();
            Req.JD_BankDetail_ID = Convert.ToString(hdnBankDetailID.Value).Trim() != "" ? Convert.ToInt32(hdnBankDetailID.Value) : 0;
            Req.Assign_Questionnaire_ID = Convert.ToString(hdnAssignQuestiID.Value).Trim() != "" ? Convert.ToInt32(hdnAssignQuestiID.Value) : 0;
            Req.Comments = txtComments.Text;
            Req.Emp_Code_Inter1 = Convert.ToString(lstInterviewerOne.SelectedValue).Trim();
            Req.Emp_Code_Inter2 = Convert.ToString(lstInterviewerTwo.SelectedValue).Trim();
            Req.Emp_Code_Inter1_OPT = "";// Convert.ToString(txtInterviewerOptOne.Text).Trim() != "" ? txtInterviewerOptOne.Text.Trim().Split('-')[0] : "";
            Req.Emp_Code_Inter2_OPT = "";// Convert.ToString(txtInterviewerOptTwo.Text).Trim() != "" ? txtInterviewerOptTwo.Text.Trim().Split('-')[0] : "";

            Req.RecruiterID = RecruiterCode;
            Req.Status_ID = 1;
            Req.ISDraft = ISDraft;
            dtRecruitment = spm.InsertRecruitmentRequisition(Req);


            String strRecruitmentURL = "";
            strRecruitmentURL = Convert.ToString(ConfigurationManager.AppSettings["AssignRecLink_Requi"]).Trim() + "?Recruitment_ReqID=" + Convert.ToInt32(dtRecruitment.Rows[0]["Recruit_ReqID"]);

			String strRecRstURL = "";
			strRecRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_RequiCR"]).Trim() + "?Req_Requi_ID=" + Convert.ToInt32(dtRecruitment.Rows[0]["Recruit_ReqID"]);

			string RequiredByDate = "";
			RequiredByDate = GetRequiredByDate();
			if (Convert.ToString((hdnstaus.Value).Trim()) != "")
			{
				if (Convert.ToString(hdnisApprover_TDCOS.Value).Trim() == "HRLWP")
				{
					int deptID = Convert.ToInt32(lstPositionDept.SelectedValue);
					getLWPML_HR_ApproverCode("");

					spm.Update_Requisition_AppRequest(Convert.ToInt32(hdnRecruitment_ReqID.Value), "Approved", txtApprovercmt.Text, Convert.ToString(hdnstaus.Value).Trim(), Convert.ToInt32(hdnCurrentID.Value));
					strapprovermails = getApproversList_HR();
					string strInsertmediaterlist = "";
					string Apporvallist = "";

					//spm.send_mailto_Req_Requisition_Next_Approver_Intermediate(hflEmailAddress.Value, "", "Recruitment - Request for " + Convert.ToString(txtReqNumber.Text), lstSkillset.SelectedItem.Text + "-" + lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtComments.Text, txtNoofPosition.Text, hdnLoginUserName.Value, txtApprovercmt.Text, GetHODRequisitionApprove_RejectList(hdnEmpCodePrve.Value, (dtRecruitment.Rows[0]["Recruit_ReqID"]).ToString()), txtReqName.Text, Convert.ToString(strInsertmediaterlist), "");
					getRecruiteAssignCode(RecruiterCode);

					Apporvallist = GetHODRequisitionApprove_RejectList(hdnEmpCodePrve.Value, (dtRecruitment.Rows[0]["Recruit_ReqID"]).ToString(), deptID);
					//Rec team mail send
					spm.send_mailto_Req_Requisition_Assign_Recruite(txtReqName.Text, hflEmailAddress.Value, hdnRecruiteEmailID.Value, "Recruitment - Request for " + Convert.ToString(txtReqNumber.Text), txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtComments.Text, txtNoofPosition.Text, Apporvallist, Convert.ToString(strInsertmediaterlist), strRecruitmentURL, txtJobDescription.Text.Trim(), txtEssentialQualifi.Text.Trim(), txtDesiredQualifi.Text.Trim(), txtComments.Text.Trim());

				}
				else
				{
					
					getLWPML_HR_ApproverCode("");
					spm.Insert_Requisition_ApproverRequest(hdnnextappcode.Value, Convert.ToInt32(hdnapprid.Value), Convert.ToInt32(hdnRecruitment_ReqID.Value));
					
					spm.Update_Requisition_AppRequest(Convert.ToInt32(hdnRecruitment_ReqID.Value), "Approved", txtApprovercmt.Text, Convert.ToString(("").Trim()), Convert.ToInt32(hdnCurrentID.Value));
					string ApprovalList = "";
					ApprovalList = GetHODRequisitionApprove_RejectList(hdnEmpCodePrve.Value, hdnRecruitment_ReqID.Value, Convert.ToInt32(lstPositionDept.SelectedValue));
					spm.send_mailto_Req_Requisition_Next_Approver(txtReqName.Text, hflEmailAddress.Value, hdnApproverid_LWPPLEmail.Value, "Recruitment - Request for " + Convert.ToString(txtReqNumber.Text), txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtComments.Text, txtNoofPosition.Text, ApprovalList, Convert.ToString(""), strRecRstURL);

				}
			}

			Response.Redirect("~/procs/Req_RequisitionIndex.aspx?itype=Pending");
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
	private string GetRequiredByDate()
	{
		string[] strdate;
		string strtoDate = "", RequiredByDate = "";
		DateTime RequiredDate;
		int Days;
		try
		{
			if (txtFromdate.Text != "")
			{
				Days = Convert.ToInt32(txttofilledIn.Text);
				strdate = Convert.ToString(txtFromdate.Text).Trim().Split('-');

				strtoDate = Convert.ToString(strdate[2].Substring(0, 4)) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
				DateTime ddt = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
				RequiredDate = ddt.AddDays(Days);
				RequiredByDate = RequiredDate.ToString("dd-MM-yyyy");
			}
		}
		catch (Exception ex)
		{
			return RequiredByDate;
			//Response.Write(ex.Message.ToString());
		}
		return RequiredByDate;
	}
	public void getRecruiteAssignCode(string strtype)
    {

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "RecruiterEmpCode";
        spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[2].Value = strtype;
        dsTrDetails = spm.getDatasetList(spars, "Usp_getRecruitmentEmp_Details_All");
        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            hdnRecruiteName.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Emp_Name"]).Trim();
            hdnRecruiteEmailID.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
            //hdnisApprover_TDCOS.Value = "NA";
        }
    }
    protected string GetIntermediatesList()
    {
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;

        //dsapprover = spm.GetApproverStatus(txtempocde_lap.Text, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value));

        if (lstIntermediate.Items.Count > 0)
        {
            sbapp.Append("<table>");
            for (int i = 0; i < lstIntermediate.Items.Count; i++)
            {
                sbapp.Append("<tr>");
                //sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
                sbapp.Append("<td>" + Convert.ToString(lstIntermediate.Items[i].Text).Trim() + "</td>");
                sbapp.Append("</tr>");
            }
            sbapp.Append("</table>");
        }

        return Convert.ToString(sbapp);
    }
    protected String getApproversList_HR()
    {
        String email_ids = "";
        try
        {
            var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
            var getcompSelectedval = lstPositionLoca.SelectedItem.Value;
            var qtype = "get_ApproverDetails_mail_rejection_correction_COMP";
            if (getcompSelectedText.Contains("Head Office"))
            {
                qtype = "get_ApproverDetails_mail_rejection_correction";
            }

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = qtype;

            spars[1] = new SqlParameter("@Req_id", SqlDbType.Int);
            spars[1].Value = hdnRecruitment_ReqID.Value;

            spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[2].Value = hdnEmpCodePrve.Value;

            spars[3] = new SqlParameter("@Apremp_code", SqlDbType.VarChar);
            spars[3].Value = Convert.ToString(Session["Empcode"]).Trim();

            spars[4] = new SqlParameter("@comp_code", SqlDbType.VarChar);
            spars[4].Value = Convert.ToString(getcompSelectedval).Trim();

            dsTrDetails = spm.getDatasetList(spars, "Usp_getRecruitmentEmp_Details_All");
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                for (int irow = 0; irow < dsTrDetails.Tables[0].Rows.Count; irow++)
                {
                    if (Convert.ToString(email_ids).Trim() == "")
                        email_ids = Convert.ToString(dsTrDetails.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                    else
                        email_ids = email_ids + ";" + Convert.ToString(dsTrDetails.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                }
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
        return email_ids;

    }
    protected string Is_Self_Approver(string strEmpCode)
    {
        string strSelfApprover = "";
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Is_Self_Approver";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = strEmpCode;

            dsTrDetails = spm.getDatasetList(spars, "SP_GET_REQ_REQUISTIONNO");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                strSelfApprover = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["self_approver"]).Trim();
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

        return strSelfApprover;

    }
    protected string GetRequisitionApprove_RejectList(string EmpCode, string RecrutID,int DeptID)
    {
        var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
        var getcompSelectedval = lstPositionLoca.SelectedItem.Value;
        var qtype = "GETREQUISITIONAPPROVERSTATUS_COMP";// GETREQUISITIONHODAPPROVERSTATUS
        //var qtypeHOD = "GETREQUISITIONHODAPPROVERSTATUS";
        if (getcompSelectedText.Contains("Head Office"))
        {
            qtype = "GETREQUISITIONAPPROVERSTATUS";
            //qtypeHOD = "GETREQUISITIONHODAPPROVERSTATUS_COMP";
        }
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        dtAppRej = spm.GetRequisitionApproverStatus(EmpCode, RecrutID, DeptID, getcompSelectedval, qtype);
        if (dtAppRej.Rows.Count > 0)
        {
            sbapp.Append("<table>");
            for (int i = 0; i < dtAppRej.Rows.Count; i++)
            {
                sbapp.Append("<tr>");
                sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
                sbapp.Append("</tr>");
            }
            sbapp.Append("</table>");
        }
        return Convert.ToString(sbapp);
    }

    protected string GetHODRequisitionApprove_RejectList(string EmpCode, string RecrutID,int DeptID)
    {
        var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
        var getcompSelectedval = lstPositionLoca.SelectedItem.Value;
        //var qtype = "GETREQUISITIONAPPROVERSTATUS";// GETREQUISITIONHODAPPROVERSTATUS
        var qtypeHOD = "GETREQUISITIONHODAPPROVERSTATUS_COMP";
        if (getcompSelectedText.Contains("Head Office"))
        {
           // qtype = "GETREQUISITIONAPPROVERSTATUS_COMP";
            qtypeHOD = "GETREQUISITIONHODAPPROVERSTATUS";
        }
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        dtAppRej = spm.GetRequisitionHODApproverStatus(EmpCode, RecrutID,DeptID, getcompSelectedval, qtypeHOD);
        if (dtAppRej.Rows.Count > 0)
        {
            sbapp.Append("<table>");
            for (int i = 0; i < dtAppRej.Rows.Count; i++)
            {
                sbapp.Append("<tr>");
                sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
                sbapp.Append("</tr>");
            }
            sbapp.Append("</table>");
        }
        return Convert.ToString(sbapp);
    }

    protected string Get_HREmail_ForCC(string strEmpCode)
    {
        String sbapp = "";

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_HREmail_ID";

        spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        spars[1].Value = strEmpCode;

        dsTrDetails = spm.getDatasetList(spars, "SP_GET_REQ_REQUISTIONNO");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsTrDetails.Tables[0].Rows.Count; i++)
            {
                if (Convert.ToString(sbapp).Trim() == "")
                    sbapp = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();
                else
                    sbapp = ";" + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();

            }
        }

        return Convert.ToString(sbapp);
    }

    protected void lstPositionName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((Convert.ToString(lstPositionName.SelectedValue).Trim() != "" || Convert.ToString(lstPositionName.SelectedValue).Trim() != "0") && (Convert.ToString(lstSkillset.SelectedValue).Trim() != "" || Convert.ToString(lstSkillset.SelectedValue).Trim() != "0"))
        {
            GetFilterGD();
			GetQuestionnaire();

		}
    }
    protected void lstSkillset_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((Convert.ToString(lstPositionName.SelectedValue).Trim() != "" || Convert.ToString(lstPositionName.SelectedValue).Trim() != "0") && (Convert.ToString(lstSkillset.SelectedValue).Trim() != "" || Convert.ToString(lstSkillset.SelectedValue).Trim() != "0"))
        {
            GetFilterGD();
			GetQuestionnaire();
			lstInterviewerOne.SelectedIndex = -1;
			GetInterviewer(Convert.ToInt32(lstSkillset.SelectedValue));
		}

    }


    //protected void btnTra_Details_Click(object sender, EventArgs e)
    //{


    //    if (DivTrvl.Visible)
    //    {
    //        DivTrvl.Visible = false;
    //        btnTra_Details.Text = "+";
    //    }
    //    else
    //    {

    //        DivTrvl.Visible = true;
    //        btnTra_Details.Text = "-";
    //    }
    //}

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(txtReqNumber.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Requisition Number";
            return;
        }
        if (DivAccm.Visible)
        {
            DivAccm.Visible = false;
            trvl_accmo_btn.Text = "+";
        }
        else
        {
            DivAccm.Visible = true;
            trvl_accmo_btn.Text = "-";

        }

    }

    protected void trvl_localbtn_Click(object sender, EventArgs e)
    {
        //if (Convert.ToString(lstPositionName.SelectedValue).Trim() == "" || Convert.ToString(lstPositionName.SelectedValue).Trim() == "0")
        //{
        //    lblmessage.Text = "Please select Position Title";
        //    return;
        //}

        //if (Convert.ToString(lstPositionCriti.SelectedValue).Trim() == "" || Convert.ToString(lstPositionCriti.SelectedValue).Trim() == "0")
        //{
        //    lblmessage.Text = "Please select Position Criticality";
        //    return;
        //}
        //if (Convert.ToString(lstSkillset.SelectedValue).Trim() == "" || Convert.ToString(lstSkillset.SelectedValue).Trim() == "0")
        //{
        //    lblmessage.Text = "Please select Skill Set";
        //    return;
        //}
        //if (Convert.ToString(lstPositionDept.SelectedValue).Trim() == "" || Convert.ToString(lstPositionDept.SelectedValue).Trim() == "0")
        //{
        //    lblmessage.Text = "Please select Department Name";
        //    return;
        //}
        //if (Convert.ToString(lstPositionDesign.SelectedValue).Trim() == "" || Convert.ToString(lstPositionDesign.SelectedValue).Trim() == "0")
        //{
        //    lblmessage.Text = "Please select Position Designation";
        //    return;
        //}
        //if (Convert.ToString(lstPositionLoca.SelectedValue).Trim() == "" || Convert.ToString(lstPositionLoca.SelectedValue).Trim() == "0")
        //{
        //    lblmessage.Text = "Please select Position Location";
        //    return;
        //}
        //if (Convert.ToString(lstPositionDept.SelectedValue).Trim() == "" || Convert.ToString(lstPositionName.SelectedValue).Trim() == "0")
        //{
        //    if (Convert.ToString(lstPositionDept.Text).Trim() == "Other")
        //    {
        //        if (Convert.ToString(txtOtherDept.Text).Trim() == "")
        //        {
        //            lblmessage.Text = "Please enter Other Department";
        //            return;
        //        }
        //    }
        //}

        //if (Convert.ToString(lstPositionDesign.SelectedValue).Trim() == "" || Convert.ToString(lstPositionDesign.SelectedValue).Trim() == "0")
        //{
        //    if (Convert.ToString(lstPositionDesign.Text).Trim() == "Other")
        //    {
        //        if (Convert.ToString(txtPositionDesig.Text).Trim() == "")
        //        {
        //            lblmessage.Text = "Please enter Position Designation Other";
        //            return;
        //        }
        //    }

        //}
        //if (Convert.ToString(txtNoofPosition.Text).Trim() == "" || Convert.ToString(txtNoofPosition.Text).Trim() == "0")
        //{
        //    lblmessage.Text = "Please enter No of Position";
        //    return;
        //}

        //if (Convert.ToString(txttofilledIn.Text).Trim() == "" || Convert.ToString(txttofilledIn.Text).Trim() == "0")
        //{
        //    lblmessage.Text = "Please  enter To Be Filled In(Days)";
        //    return;
        //}
        //if (Convert.ToString(txtSalaryRangeFrom.Text).Trim() == "" || Convert.ToString(txtSalaryRangeFrom.Text).Trim() == "0")
        //{
        //    lblmessage.Text = "Please  enter Salary Range From (Lakh/Year)";
        //    return;
        //}
        //if (Convert.ToString(txtSalaryRangeTo.Text).Trim() == "" || Convert.ToString(txtSalaryRangeTo.Text).Trim() == "0")
        //{
        //    lblmessage.Text = "Please  enter Salary Range To (Lakh/Year)";
        //    return;
        //}
        //if (Convert.ToString(lstReasonForRequi.SelectedValue).Trim() == "" || Convert.ToString(lstReasonForRequi.SelectedValue).Trim() == "0")
        //{
        //    lblmessage.Text = "Please select Reason For Requisition";
        //    return;
        //}

        //if (Convert.ToString(lstPreferredEmpType.SelectedValue).Trim() == "" || Convert.ToString(lstPreferredEmpType.SelectedValue).Trim() == "0")
        //{
        //    lblmessage.Text = "Please select Preferred Employment Type";
        //    return;
        //}
        //if (Convert.ToString(lstPositionBand.SelectedValue).Trim() == "" || Convert.ToString(lstPositionBand.SelectedValue).Trim() == "0")
        //{
        //    lblmessage.Text = "Please select Band";
        //    return;
        //}
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

        try
        {

            int Quest_ID = 0;
            DataTable JDBankList = new DataTable();
            string Stype = "getAssignJDBank_Filter";

            #region Check For Blank Fields
            if (Convert.ToString(lstDGSkillset.SelectedValue).Trim() == "" || Convert.ToString(lstDGSkillset.SelectedValue).Trim() == "0")
            {
                lblmsg3.Text = "Please select Skill Set";
                ModalPopupExtenderDG.Show();
                return;
            }
            if (Convert.ToString(lstDGPosition.SelectedValue).Trim() == "" || Convert.ToString(lstDGPosition.SelectedValue).Trim() == "0")
            {
                lblmsg3.Text = "Please select Position Title";
                ModalPopupExtenderDG.Show();
                return;
            }
            //string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            //if (confirmValue != "Yes")
            //{
            //    return;
            //}
            #endregion
            JDBankList = spm.GetAssign_JDBankFilter(Stype, Convert.ToInt32(lstDGSkillset.SelectedValue), Convert.ToInt32(lstDGPosition.SelectedValue));
            grdGetGD.DataSource = null;
            grdGetGD.DataBind();
            if (JDBankList.Rows.Count > 0)
            {
                lblmsg3.Text = "";
                grdGetGD.DataSource = JDBankList;
                grdGetGD.DataBind();
                ModalPopupExtenderDG.Show();
                Oth_btnDelete.Visible = true;
            }
            else
            {
                lblmsg3.Text = "No JD from bank for the selected combination. Please contact Administrator.";
                Oth_btnDelete.Visible = false;
            }
            ModalPopupExtenderDG.Show();
        }
        catch (Exception ex)
        {

        }

    }

    protected void Oth_btnDelete_Click(object sender, EventArgs e)
    {
        int Quest_ID = 0;
        txtJobDescription.Text = "";
        DataTable JDBankList = new DataTable();
        string Stype = "getAssignJDBank_Filter";
        JDBankList = spm.GetAssign_JDBankFilter(Stype, Convert.ToInt32(lstDGSkillset.SelectedValue), Convert.ToInt32(lstDGPosition.SelectedValue));
        if (JDBankList.Rows.Count > 0)
        {
            txtJobDescription.Text = Convert.ToString(JDBankList.Rows[0]["JobDescription"]).Trim();
            hdnBankDetailID.Value = Convert.ToString(JDBankList.Rows[0]["JD_BankDetail_ID"]).Trim();
            //mobile_cancel.Visible = true;
        }

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
    private void getApproverlist(string EmpCode, string RecrutID,int DeptID)
    {
        var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
        var getcompSelectedval = lstPositionLoca.SelectedItem.Value;
        var qtype = "GETREQUISITIONAPPROVERSTATUS_COMP";// GETREQUISITIONHODAPPROVERSTATUS
        //var qtypeHOD = "GETREQUISITIONHODAPPROVERSTATUS";
        if (getcompSelectedText.Contains("Head Office"))
        {
            qtype = "GETREQUISITIONAPPROVERSTATUS";
            //qtypeHOD = "GETREQUISITIONHODAPPROVERSTATUS_COMP";
        }
        DataTable dtapprover = new DataTable();
        //string strleavetype = Convert.ToString(txtLeaveType.Text).Trim();       
        dtapprover = spm.GetRequisitionApproverStatus(EmpCode, RecrutID, DeptID, getcompSelectedval, qtype);
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();
        if (dtapprover.Rows.Count > 0)
        {
            DgvApprover.DataSource = dtapprover;
            DgvApprover.DataBind();
        }
    }
	//Send Back For Correction
	protected void trvldeatils_delete_btn_Click(object sender, EventArgs e)
    {
		
		try
		{
			if (hdnHOD.Value == "HOD")
			{
				//RequistionCorrectionHOD();
				RequistionCorrection();
			}
			else
			{
				RequistionCorrection();
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}
	private void RequistionCorrection()
	{
		try
		{
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}
			if (Convert.ToString((txtApprovercmt.Text).Trim()) == "")
			{
				lblmessage.Text = "Please mention the comment before Send Back For Correction the Recruitment request";
				return;
			}
			strapprovermails = getApproversList_HR();
			string strInsertmediaterlist = "";
			//strInsertmediaterlist = GetIntermediatesList();

			string strHREmailForCC = "";
			//strHREmailForCC = Get_HREmail_ForCC(Convert.ToString(hdnEmpCodePrve.Value));
			if (Convert.ToString(strapprovermails).Trim() != "")
			{
				strapprovermails = ";" + strHREmailForCC;
			}
			else
			{
				strapprovermails = strHREmailForCC;
			}
			string RequiredByDate = "";
			RequiredByDate = GetRequiredByDate();
			//spm.Rreject_Requisition_Request(Convert.ToInt32(hdnRecruitment_ReqID.Value), hdnEmpCpde.Value, Convert.ToInt32(hdnCurrentID.Value), txtApprovercmt.Text);
			spm.Correction_Requisition_Request(Convert.ToInt32(hdnRecruitment_ReqID.Value), hdnEmpCpde.Value, Convert.ToInt32(hdnCurrentID.Value), txtApprovercmt.Text);
			string ApprovalList = "";
			ApprovalList = GetRequisitionApprove_RejectList(hdnEmpCodePrve.Value, hdnRecruitment_ReqID.Value, Convert.ToInt32(lstPositionDept.SelectedValue));

			spm.send_mail_Req_Requisition__Rejection_Correction(hflEmailAddress.Value, hdnLoginUserName.Value, "Recruitment - Correction of  Requisition Request " + txtReqNumber.Text + " Request", txtReqNumber.Text, txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, txtApprovercmt.Text, strapprovermails, ApprovalList, txtReqName.Text, Convert.ToString(strInsertmediaterlist).Trim());
			Response.Redirect("~/procs/Req_RequisitionIndex.aspx?itype=Pending");
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}
	private void RequistionCorrectionHOD()
	{
		try
		{
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}
			if (Convert.ToString((txtApprovercmt.Text).Trim()) == "")
			{
				lblmessage.Text = "Please mention the comment before Send Back For Correction the Recruitment request";
				return;
			}
			strapprovermails = getApproversList_HR();
			string strInsertmediaterlist = "";
			//strInsertmediaterlist = GetIntermediatesList();

			string strHREmailForCC = "";
			//strHREmailForCC = Get_HREmail_ForCC(Convert.ToString(hdnEmpCodePrve.Value));
			if (Convert.ToString(strapprovermails).Trim() != "")
			{
				strapprovermails = ";" + strHREmailForCC;
			}
			else
			{
				strapprovermails = strHREmailForCC;
			}
			string RequiredByDate = "";
			RequiredByDate = GetRequiredByDate();
			//spm.Rreject_Requisition_Request(Convert.ToInt32(hdnRecruitment_ReqID.Value), hdnEmpCpde.Value, Convert.ToInt32(hdnCurrentID.Value), txtApprovercmt.Text);
			spm.Correction_Requisition_Request(Convert.ToInt32(hdnRecruitment_ReqID.Value), hdnEmpCpde.Value, Convert.ToInt32(hdnCurrentID.Value), txtApprovercmt.Text);
			string ApprovalList = "";
			ApprovalList = GetHODRequisitionApprove_RejectList(hdnEmpCodePrve.Value, hdnRecruitment_ReqID.Value, Convert.ToInt32(lstPositionDept.SelectedValue));

			spm.send_mail_Req_Requisition__Rejection_Correction(hflEmailAddress.Value, hdnLoginUserName.Value, "Recruitment - Correction of  Requisition Request " + txtReqNumber.Text + " Request", txtReqNumber.Text, txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, txtApprovercmt.Text, strapprovermails, ApprovalList, txtReqName.Text, Convert.ToString(strInsertmediaterlist).Trim());
			Response.Redirect("~/procs/Req_RequisitionIndex.aspx?itype=Pending");
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}

	protected void lstPositionBand_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			if (lstPositionBand.SelectedIndex > 0)
			{
				string Band = lstPositionBand.SelectedValue.Trim();
				CTC_Rage_With_Band(Band);
			}
			else
			{
				txtSalaryRangeFrom.Text = "";
				txtSalaryRangeTo.Text = "";
			}
		}
		catch (Exception)
		{

			throw;
		}
	}
	private void CTC_Rage_With_Band(string Band)
	{
		try
		{
			int Recruitment_ReqID = 0;
			Recruitment_ReqID = Convert.ToString(hdnRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdnRecruitment_ReqID.Value) : 0;
			string EmpCode = "";
			if (Convert.ToString(Session["Empcode"]).Trim() != "" || Session["Empcode"] != null)
			{
				EmpCode = Convert.ToString(Session["Empcode"]).ToString();
			}
			DataTable dtCTCRange = new DataTable();
			SqlParameter[] spars = new SqlParameter[7];
			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = "Recruitment_BAND";
			spars[1] = new SqlParameter("@ID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(Recruitment_ReqID);
			spars[2] = new SqlParameter("@EmpCode", SqlDbType.NVarChar);
			spars[2].Value = Convert.ToString(EmpCode);
			spars[3] = new SqlParameter("@Band", SqlDbType.NVarChar);
			spars[3].Value = Convert.ToString(Band);
			dtCTCRange = spm.getMobileRemDataList(spars, "SP_Rec_CTC_With_Band");
			if (dtCTCRange.Rows.Count > 0)
			{
				txtSalaryRangeFrom.Text = Convert.ToString(dtCTCRange.Rows[0]["SalAverage"]).Trim();
				txtSalaryRangeTo.Text = Convert.ToString(dtCTCRange.Rows[0]["SalMax"]).Trim();
			}
			else
			{
				string msg = "Salary range not set for the selected Band. Please contact HR!.";
				txtSalaryRangeFrom.Text = "";
				txtSalaryRangeTo.Text = "";
				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);

			}
		}
		catch (Exception)
		{

			throw;
		}
	}
}