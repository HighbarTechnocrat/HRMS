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
using System.Linq;

public partial class procs_ViewCandidate : System.Web.UI.Page
{
	public SqlDataAdapter sqladp;
	public static string dob1;
	public string userid;
	public static string pimg = "";
	public static string cimg = "";
	public string loc = "", dept = "", subdept = "", desg = "", Wsch = "";
	public int did = 0;
	public DataTable dtcandidateDetails, dtIRsheetcount, dtMerge, NewDTValue, DTMaintable, DTInterviews, NewDT;
	HttpFileCollection fileCollection;
	SP_Methods spm = new SP_Methods();
	Recruitment_Candidate_Parameter Candidate = new Recruitment_Candidate_Parameter();
	public DataTable dtmainSkillSet, dtRecCandidate, dtEducation, dtCompanyInfo;
	public DataSet dsCandidateData, dsCVSource, dtIrSheetReport;
	public int Leaveid;
	public int leavetype, openbal, avail, rembal, weekendcount, publicholiday, apprid;
	double totaldays;
	public string filename = "", multiplefilename = "", multiplefilenameadd = "", approveremailaddress, message, strreqCandidate_ID;


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

			lblmessage.Text = "";


			if (Convert.ToString(Session["Empcode"]).Trim() == "")
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

			// lpm.Emp_Code = Session["Empcode"].ToString();

			lblmessage.Visible = true;
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
					hdfilefath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateResumeDocpath"]).Trim());
					getMainSkillset();
                    CVSourceOtherBind();
                    getCVSource();
					GetInterviewer();
					GetJobSites();
					GetVendorName();
					getEducationType();
					GetCandidateInfdataBind();

					Session.Remove("dtCompanyInfo");
					Session.Remove("dtEducation");
					if (Request.QueryString.Count > 0)
					{
						hdCandidate_ID.Value = Convert.ToString(Request.QueryString[0]).Trim();
						PopulateCandidateData();
						GetRequisitiodetailsCandidate();
						//get_Candidate_multipleUploaded_Files();
					}

					Txt_CandidateBirthday.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					Txt_CandidateAge.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					//   Txt_CandidateMobile.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					Txt_CandidateExperence.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					Txt_CurrentCTC_Fixed.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					TxtCurrentCTC_Variable.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					Txt_CandidateBirthday.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					txtComEndDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					txtComStartDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					txtYearofPassing.Attributes.Add("onkeypress", "return noanyCharecters(event);");
				}

			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}
	private void GetRequisitiodetailsCandidate()
	{
		try
		{
			 //dsCandidateData = new DataSet();
			SqlParameter[] spars = new SqlParameter[9];
			spars[0] = new SqlParameter("@Mode", SqlDbType.NVarChar);
			spars[0].Value = "GetRequisitiodetailsCandidate";
			spars[1] = new SqlParameter("@Candidate_ID", SqlDbType.NVarChar);
			spars[1].Value = Convert.ToInt32(hdCandidate_ID.Value);
			var dsCandidateRecq = spm.getServiceRequestReportCount(spars, "sp_RecCreateCandidate");
			if (dsCandidateRecq != null)
			{
				if (dsCandidateRecq.Tables.Count > 0)
				{
					var getTable1 = dsCandidateRecq.Tables[0];
					if (getTable1.Rows.Count > 0)
					{
						gvMngTravelRqstList.DataSource = getTable1;
						gvMngTravelRqstList.DataBind();

					}
				}
			}
		}
		catch (Exception ex)
		{

			throw;
		}
	}

    private void CVSourceOtherBind()
    {
        DDlOther.Items.Add("Select other");
        DDlOther.Items.Add("Campus Interview");
        DDlOther.Items.Add("Walk in Interview");
        DDlOther.Items.Add("Social Media like Facebook,Twitter,Quora,Instagram");
	DDlOther.Items.Add("Mass mail");
	DDlOther.Items.Add("Freelancer");
    }
    private void GetCandidateInfdataBind()
	{
		DataSet DSRecwisedatacandidate = new DataSet();
		DSRecwisedatacandidate = spm.GetCanInfoRecruitmentwisedataBind();

		DDLBaseLocationPreference.DataSource = DSRecwisedatacandidate.Tables[0];
		DDLBaseLocationPreference.DataTextField = "BaseLocationPreference";
		DDLBaseLocationPreference.DataValueField = "BaseLocationPreferenceID";
		DDLBaseLocationPreference.DataBind();
		DDLBaseLocationPreference.Items.Insert(0, new ListItem("-- Select --", "0"));

		DDLprojecthandled.DataSource = DSRecwisedatacandidate.Tables[1];
		DDLprojecthandled.DataTextField = "TypeofProject";
		DDLprojecthandled.DataValueField = "TypeProjecthandledID";
		DDLprojecthandled.DataBind();
		DDLprojecthandled.Items.Insert(0, new ListItem("-- Select --", "0"));

		DDLDomainExperence.DataSource = DSRecwisedatacandidate.Tables[2];
		DDLDomainExperence.DataTextField = "TotalDomainExperience";
		DDLDomainExperence.DataValueField = "DomainExperienceID";
		DDLDomainExperence.DataBind();
		DDLDomainExperence.Items.Insert(0, new ListItem("-- Select --", "0"));

		DDLSAPExperence.DataSource = DSRecwisedatacandidate.Tables[3];
		DDLSAPExperence.DataTextField = "SAPExperience";
		DDLSAPExperence.DataValueField = "SAPExperienceID";
		DDLSAPExperence.DataBind();
		DDLSAPExperence.Items.Insert(0, new ListItem("-- Select --", "0"));

		DDLImplementationprojectWorkedOn.DataSource = DSRecwisedatacandidate.Tables[4];
		DDLImplementationprojectWorkedOn.DataTextField = "ImplementationProjectsWorkOn";
		DDLImplementationprojectWorkedOn.DataValueField = "ImplementationProjectsWorkOnID";
		DDLImplementationprojectWorkedOn.DataBind();
		DDLImplementationprojectWorkedOn.Items.Insert(0, new ListItem("-- Select --", "0"));

		DDLSupportproject.DataSource = DSRecwisedatacandidate.Tables[5];
		DDLSupportproject.DataTextField = "SupportProject";
		DDLSupportproject.DataValueField = "TotalSupportProjectID";
		DDLSupportproject.DataBind();
		DDLSupportproject.Items.Insert(0, new ListItem("-- Select --", "0"));

		DDLPhaseWorkimplementation.DataSource = DSRecwisedatacandidate.Tables[6];
		DDLPhaseWorkimplementation.DataTextField = "PhasesImplementationWork";
		DDLPhaseWorkimplementation.DataValueField = "PhasesImplementationWorkId";
		DDLPhaseWorkimplementation.DataBind();
		DDLPhaseWorkimplementation.Items.Insert(0, new ListItem("-- Select --", "0"));

		DDLRolesPlaydImplementation.DataSource = DSRecwisedatacandidate.Tables[7];
		DDLRolesPlaydImplementation.DataTextField = "RoleImplementationProject";
		DDLRolesPlaydImplementation.DataValueField = "RoleImplementationProjectID";
		DDLRolesPlaydImplementation.DataBind();
		DDLRolesPlaydImplementation.Items.Insert(0, new ListItem("-- Select --", "0"));

		DDLnatureOfIndustryClient.DataSource = DSRecwisedatacandidate.Tables[8];
		DDLnatureOfIndustryClient.DataTextField = "NatureIndustryClient";
		DDLnatureOfIndustryClient.DataValueField = "NatureIndustryClientID";
		DDLnatureOfIndustryClient.DataBind();
		DDLnatureOfIndustryClient.Items.Insert(0, new ListItem("-- Select --", "0"));

		DDLCommunicationSkill.DataSource = DSRecwisedatacandidate.Tables[9];
		DDLCommunicationSkill.DataTextField = "CheckCommunicationSkill";
		DDLCommunicationSkill.DataValueField = "CheckCommunicationSkillID";
		DDLCommunicationSkill.DataBind();
		DDLCommunicationSkill.Items.Insert(0, new ListItem("-- Select --", "0"));



	}

	protected void mobile_cancel_Click(object sender, EventArgs e)
	{
		Response.Redirect("~/procs/SearchCandidate.aspx");
	}

	#endregion

	#region page Methods

	public void PopulateCandidateData()
	{
		try
		{
			//strreqCandidate_ID = Request.QueryString["reqCandidate_ID"];
			dsCandidateData = spm.get_Rec_CandidateList(hdCandidate_ID.Value);
			if (dsCandidateData.Tables[0].Rows.Count > 0)
			{
				hdCandidate_ID.Value = dsCandidateData.Tables[0].Rows[0]["Candidate_ID"].ToString();
				Txt_CandidateName.Text = dsCandidateData.Tables[0].Rows[0]["CandidateName"].ToString();
				Txt_CandidateEmail.Text = dsCandidateData.Tables[0].Rows[0]["CandidateEmail"].ToString();
				Txt_CandidateMobile.Text = dsCandidateData.Tables[0].Rows[0]["CandidateMobile"].ToString();
				lstCandidategender.SelectedValue = dsCandidateData.Tables[0].Rows[0]["CandidateGender"].ToString();
				lstMaritalStatus.SelectedValue = dsCandidateData.Tables[0].Rows[0]["Maritalstatus"].ToString();
				Txt_CandidateCurrentLocation.Text = dsCandidateData.Tables[0].Rows[0]["CandidateCurrentLocation"].ToString();
				Txt_CandidateBirthday.Text = dsCandidateData.Tables[0].Rows[0]["CandidateBirthday"].ToString();
				Txt_CandidatePAN.Text = dsCandidateData.Tables[0].Rows[0]["CandidatePAN"].ToString();
				Txt_CandidateExperence.Text = dsCandidateData.Tables[0].Rows[0]["CandidateExperience_Years"].ToString();
				TxtCurrentCTC_Total.Text = dsCandidateData.Tables[0].Rows[0]["CandidateCurrentCTC"].ToString();
				TxtRelevantExpYrs.Text = dsCandidateData.Tables[0].Rows[0]["RelevantExpYrs"].ToString();
				TxtCurrentCTC_Variable.Text = dsCandidateData.Tables[0].Rows[0]["CandidateExpectedCTC"].ToString();
				lstCVSource.SelectedValue = dsCandidateData.Tables[0].Rows[0]["CVSource_ID"].ToString();
				lnkuplodedfile.Text = dsCandidateData.Tables[0].Rows[0]["UploadResume"].ToString();
				hdfilename.Value = dsCandidateData.Tables[0].Rows[0]["UploadResume"].ToString();
				filename = dsCandidateData.Tables[0].Rows[0]["UploadResume"].ToString();
				lstMainSkillset.SelectedValue = dsCandidateData.Tables[0].Rows[0]["ModuleId"].ToString();
				Txt_AdditionalSkillset.Text = dsCandidateData.Tables[0].Rows[0]["AdditionalSkillset"].ToString();

				Txt_Comments.Text = dsCandidateData.Tables[0].Rows[0]["Comments"].ToString();
				lstInterviewerOne.SelectedValue = dsCandidateData.Tables[0].Rows[0]["ReferredBy"].ToString();
				lstJobSites.SelectedValue = dsCandidateData.Tables[0].Rows[0]["JobSitesID"].ToString();
				lstVendorName.SelectedValue = dsCandidateData.Tables[0].Rows[0]["VenderID"].ToString();
				txtAdharNo.Text = dsCandidateData.Tables[0].Rows[0]["AdharNo"].ToString();
				//Txt_ReferredBy.Text = dsCandidateData.Tables[0].Rows[0]["Others"].ToString();
                DDlOther.SelectedValue= dsCandidateData.Tables[0].Rows[0]["Others"].ToString();
                Txt_CurrentCTC_Fixed.Text = dsCandidateData.Tables[0].Rows[0]["CurrentCTC_Fixed"].ToString();
				TxtCurrentCTC_Variable.Text = dsCandidateData.Tables[0].Rows[0]["CurrentCTC_Variable"].ToString();
				TxtExpCTC_Fixed.Text = dsCandidateData.Tables[0].Rows[0]["ExpCTC_Fixed"].ToString();
				TxtExpCTC_Variable.Text = dsCandidateData.Tables[0].Rows[0]["ExpCTC_Variable"].ToString();
				TxtExpCTC_Total.Text = dsCandidateData.Tables[0].Rows[0]["ExpCTC_Total"].ToString();
				DDlCurrentlyonnotice.SelectedValue = dsCandidateData.Tables[0].Rows[0]["CurrentlyOnNotice"].ToString();
				Txt_NoticePeriodInday.Text = dsCandidateData.Tables[0].Rows[0]["NoticePeriod"].ToString();

				//Candidate.CurrentLocation = Txt_CandidateCurrentLocation.Text = dsCandidateData.Tables[0].Rows[0]["ExpCTC_Total"].ToString();
				Txt_NativeHomeLocation.Text = dsCandidateData.Tables[0].Rows[0]["NativeLocation"].ToString();
				Txt_BaseLocationcurrentcompany.Text = dsCandidateData.Tables[0].Rows[0]["LocationCurrentCompany"].ToString();
				DDLBaseLocationPreference.SelectedValue = dsCandidateData.Tables[0].Rows[0]["BaseLocationPreferenceID"].ToString();
				DDLRelocateTravelAnyLocation.SelectedValue = dsCandidateData.Tables[0].Rows[0]["Travelanylocations"].ToString();
				Txt_TravelContraintPandemicSituation.Text = dsCandidateData.Tables[0].Rows[0]["TravelPandemicSituation"].ToString();
				DDLOpenToTravel.SelectedValue = dsCandidateData.Tables[0].Rows[0]["OpentoTravel"].ToString();
				DDlpayrollsCompany.SelectedValue = dsCandidateData.Tables[0].Rows[0]["Candidatepayroll"].ToString();
				DDLBreakInService.SelectedValue = dsCandidateData.Tables[0].Rows[0]["Candidateanybreakservice"].ToString();
				DDLImplementationprojectWorkedOn.SelectedValue = dsCandidateData.Tables[0].Rows[0]["ImplementationProjectsWorkOnID"].ToString();
				DDLDomainExperence.SelectedValue = dsCandidateData.Tables[0].Rows[0]["DomainExperienceID"].ToString();
				DDLSAPExperence.SelectedValue = dsCandidateData.Tables[0].Rows[0]["SAPExperienceID"].ToString();
				DDLSupportproject.SelectedValue = dsCandidateData.Tables[0].Rows[0]["TotalSupportProjectID"].ToString();
				DDLPhaseWorkimplementation.SelectedValue = dsCandidateData.Tables[0].Rows[0]["PhasesImplementationWorkId"].ToString();
				DDLRolesPlaydImplementation.SelectedValue = dsCandidateData.Tables[0].Rows[0]["RoleImplementationProjectID"].ToString();

				DDLprojecthandled.SelectedValue = dsCandidateData.Tables[0].Rows[0]["TypeProjecthandledID"].ToString();
				DDLnatureOfIndustryClient.SelectedValue = dsCandidateData.Tables[0].Rows[0]["NatureIndustryClientID"].ToString();
				DDLCommunicationSkill.SelectedValue = dsCandidateData.Tables[0].Rows[0]["CheckCommunicationSkillID"].ToString();
				Txt_CurrentRoleorganization.Text = dsCandidateData.Tables[0].Rows[0]["CurrentRoleorganization"].ToString();
				Txt_RoleDomaincompany.Text = dsCandidateData.Tables[0].Rows[0]["RoleDomainCompany"].ToString();
				TxtReasonforBreak.Text = dsCandidateData.Tables[0].Rows[0]["Reasonforbreak"].ToString();
				Txt_lookingforChange.Text = dsCandidateData.Tables[0].Rows[0]["lookingChangeReason"].ToString();
				getBirthYear();
				CVSource();
			}

			if (dsCandidateData.Tables[1].Rows.Count > 0)
			{
				gvotherfile.DataSource = dsCandidateData.Tables[1];
				gvotherfile.DataBind();

			}
			if (dsCandidateData.Tables[2].Rows.Count > 0)
			{
				DgvEducationDetails.DataSource = dsCandidateData.Tables[2];
				DgvEducationDetails.DataBind();
				Session.Add("dtEducation", dsCandidateData.Tables[2]);

			}
			if (dsCandidateData.Tables[3].Rows.Count > 0)
			{
				GrdCompanyInfo.DataSource = dsCandidateData.Tables[3];
				GrdCompanyInfo.DataBind();
				Session.Add("dtCompanyInfo", dsCandidateData.Tables[3]);
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
			Response.End();

			throw;
		}

	}
	private void getMainSkillset()
	{
		dtmainSkillSet = spm.GetMainSkillset();
		lstMainSkillset.DataSource = dtmainSkillSet;
		lstMainSkillset.DataTextField = "ModuleDesc";
		lstMainSkillset.DataValueField = "ModuleId";
		lstMainSkillset.DataBind();
		lstMainSkillset.Items.Insert(0, new ListItem("Select SkillSet", "0"));
	}
	private void getCVSource()
	{
		dsCVSource = spm.GetCVSource();
		lstCVSource.DataSource = dsCVSource;
		lstCVSource.DataTextField = "CVSource";
		lstCVSource.DataValueField = "CVSource_ID";
		lstCVSource.DataBind();
		lstCVSource.Items.Insert(0, new ListItem("Select CVSource", "0"));
	}
	public void GetInterviewer()
	{
		DataTable dtIntervie = new DataTable();
		dtIntervie = spm.GetRecruitment_Req_Employee_Mst();
		if (dtIntervie.Rows.Count > 0)
		{
			lstInterviewerOne.DataSource = dtIntervie;
			lstInterviewerOne.DataTextField = "EmployeeName";
			lstInterviewerOne.DataValueField = "EmployeeCode";
			lstInterviewerOne.DataBind();
			lstInterviewerOne.Items.Insert(0, new ListItem("Select Referred By", "0"));
		}
	}
	public void GetJobSites()
	{
		DataTable dtIntervie = new DataTable();
		dtIntervie = spm.GetRecruitment_Req_JobSites();
		if (dtIntervie.Rows.Count > 0)
		{
			lstJobSites.DataSource = dtIntervie;
			lstJobSites.DataTextField = "JobSitesName";
			lstJobSites.DataValueField = "JobSitesID";
			lstJobSites.DataBind();
			lstJobSites.Items.Insert(0, new ListItem("Select Job Sites Name", "0"));
		}
	}
	public void GetVendorName()
	{
		DataTable dtIntervie = new DataTable();
		dtIntervie = spm.GetRecruitment_Req_VendorDetails();
		if (dtIntervie.Rows.Count > 0)
		{
			lstVendorName.DataSource = dtIntervie;
			lstVendorName.DataTextField = "VenderName";
			lstVendorName.DataValueField = "VenderID";
			lstVendorName.DataBind();
			lstVendorName.Items.Insert(0, new ListItem("Select Vendor Name", "0"));
		}
	}
	private void getEducationType()
	{
		DataTable dtEducation = new DataTable();
		dtEducation = spm.GetRecruitment_Req_EducationType();
		lstEducationType.DataSource = dtEducation;
		lstEducationType.DataTextField = "EducationType";
		lstEducationType.DataValueField = "EducationTypeID";
		lstEducationType.DataBind();
		lstEducationType.Items.Insert(0, new ListItem("Select Education Type", "0"));
	}
	static string CalculateYourAge(DateTime Dob)
	{
		DateTime Now = DateTime.Now;
		int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
		DateTime PastYearDate = Dob.AddYears(Years);
		int Months = 0;
		for (int i = 1; i <= 12; i++)
		{
			if (PastYearDate.AddMonths(i) == Now)
			{
				Months = i;
				break;
			}
			else if (PastYearDate.AddMonths(i) >= Now)
			{
				Months = i - 1;
				break;
			}
		}
		int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
		return String.Format(" {0} Year(s) {1} Month(s)", Years, Months, Days);
	}

	[System.Web.Services.WebMethod]
	public static List<string> Searchempcode(string prefixText, int count)
	{
		using (SqlConnection conn = new SqlConnection())
		{
			conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
			using (SqlCommand cmd = new SqlCommand())
			{
				string strsql = "";
				strsql = "Select (first_name + ' ' +last_name) as 'empName' from tbl_Employee_Mst " +
						   "   where (first_name + ' ' +last_name) like '%' + @SearchText + '%' order by (first_name + ' ' +last_name) asc";
				cmd.CommandText = strsql;
				cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
				//cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));
				cmd.Connection = conn;
				conn.Open();
				List<string> employees = new List<string>();
				using (SqlDataReader sdr = cmd.ExecuteReader())
				{
					while (sdr.Read())
					{
						employees.Add(sdr["empName"].ToString());
					}
				}
				conn.Close();
				return employees;
			}
		}
	}

	#endregion
	protected void lstCVSource_SelectedIndexChanged(object sender, EventArgs e)
	{

        DDlOther.SelectedValue = "Select other";
        DDlOther.Visible = false;
        //Txt_ReferredBy.Text = "";
        //Txt_ReferredBy.Enabled = false;
        lstInterviewerOne.Visible = false;
		lstVendorName.Visible = false;
		lstJobSites.Visible = false;
		lbltext.Visible = true;
		lbltext.Text = "";
		spanIDreferredby.Visible = false;
		lstVendorName.SelectedIndex = -1;
		lstJobSites.SelectedIndex = -1;
		lstInterviewerOne.SelectedIndex = -1;
		if (lstCVSource.SelectedValue == "3")
		{
			lbltext.Text = "Referred By";
			lstInterviewerOne.Visible = true;
			spanIDreferredby.Visible = true;
		}
		if (lstCVSource.SelectedValue == "1")
		{
			lbltext.Text = "Vendors";
			lstVendorName.Visible = true;
			spanIDreferredby.Visible = true;
		}
		if (lstCVSource.SelectedValue == "2")
		{
			lbltext.Text = "Job Sites";
			lstJobSites.Visible = true;
			spanIDreferredby.Visible = true;
		}
        if (lstCVSource.SelectedValue == "4")
        {
            lbltext.Text = "Others";
            DDlOther.Visible = true;
            spanIDreferredby.Visible = true;
            //Txt_ReferredBy.Enabled = true;
            //spanIDreferredby.Visible = false;
            //Txt_ReferredBy.Visible = true;
            //lbltext.Visible = false;
        }
        //if (lstCVSource.SelectedValue == "4")
        //{
        //	// lbltext.Text = "Others";  
        //	Txt_ReferredBy.Enabled = true;
        //	spanIDreferredby.Visible = false;
        //	Txt_ReferredBy.Visible = true;
        //	lbltext.Visible = false;
        //}

    }

	private void CVSource()
	{

        //Txt_ReferredBy.Enabled = false;
        DDlOther.Visible = false;
		lstInterviewerOne.Visible = false;
		lstVendorName.Visible = false;
		lstJobSites.Visible = false;
		lbltext.Visible = true;
		lbltext.Text = "";
		spanIDreferredby.Visible = false;

		if (lstCVSource.SelectedValue == "3")
		{
			lbltext.Text = "Referred By";
			lstInterviewerOne.Visible = true;
			spanIDreferredby.Visible = true;
		}
		if (lstCVSource.SelectedValue == "1")
		{
			lbltext.Text = "Vendors";
			lstVendorName.Visible = true;
			spanIDreferredby.Visible = true;
		}
		if (lstCVSource.SelectedValue == "2")
		{
			lbltext.Text = "Job Sites";
			lstJobSites.Visible = true;
			spanIDreferredby.Visible = true;
		}
        if (lstCVSource.SelectedValue == "4")
        {
            lbltext.Text = "Others";
            DDlOther.Visible = true;
            spanIDreferredby.Visible = true;
            //Txt_ReferredBy.Enabled = true;
            //spanIDreferredby.Visible = false;
            //Txt_ReferredBy.Visible = true;
            //lbltext.Visible = false;
        }
        //if (lstCVSource.SelectedValue == "4")
        //{
        //	// lbltext.Text = "Others";  
        //	Txt_ReferredBy.Enabled = true;
        //	spanIDreferredby.Visible = false;
        //	Txt_ReferredBy.Visible = true;
        //	lbltext.Visible = false;
        //}
    }
	public void get_Candidate_multipleUploaded_Files()
	{
		try
		{
			dsCandidateData = spm.getSearchCandidateList(hdCandidate_ID.Value);
			if (dsCandidateData.Tables[1].Rows.Count > 0)
			{
				gvotherfile.DataSource = dsCandidateData.Tables[1];
				gvotherfile.DataBind();
			}
			//gvotherfile.DataSource = null;
			//gvotherfile.DataBind();


		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());

		}

	}

	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		try
		{
			string vendorID = "", JobSitesID = "", ReferredBy = "", Other = "";
            #region Check All Parameters Selected

            string confirmValue = hdnYesNo.Value.ToString();
			if (confirmValue != "Yes")
			{
				return;
			}

			lblmessage.Text = "";

			if (Convert.ToString(Txt_CandidateName.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter name";
				return;
			}
			if (Convert.ToString(Txt_CandidateEmail.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter Email";
				return;
			}
			if (Convert.ToString(Txt_CandidateMobile.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter Mobile No.";
				return;
			}
			if (Convert.ToString(Txt_CandidateMobile.Text).Trim() != "")
			{
				int count = Txt_CandidateMobile.Text.Length;
				if (count < 10)
				{
					lblmessage.Text = "Please enter proper Mobile No.";
					return;
				}
			}
			//if (Convert.ToString(Txt_CandidateCurrentLocation.Text).Trim() == "")
			//{
			//    lblmessage.Text = "Please enter Current Location";
			//    return;
			//}
			if (Convert.ToString(lstCandidategender.SelectedValue).Trim() == "" || Convert.ToString(lstCandidategender.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Gender";
				return;
			}
			if (Convert.ToString(lstMaritalStatus.SelectedValue).Trim() == "" || Convert.ToString(lstMaritalStatus.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Marital Status";
				return;
			}
			if (Convert.ToString(lstMainSkillset.SelectedValue).Trim() == "0" || Convert.ToString(lstMainSkillset.SelectedValue).Trim() == "")
			{
				lblmessage.Text = "Please select Main Skillset";
				return;
			}


			if (Convert.ToString(Txt_CandidateExperence.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter Experience(Years)";
				return;
			}
			if (Convert.ToString(TxtRelevantExpYrs.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter Relevant Exp(Years)";
				return;
			}
			if (Convert.ToString(Txt_AdditionalSkillset.Text).Trim() == "")
			{
				lblmessage.Text = "Please select Additional Skillset";
				return;
			}
			if (Convert.ToString(lstCVSource.SelectedValue).Trim() == "" || Convert.ToString(lstCVSource.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select CV Source";
				return;
			}
			if (Convert.ToString(lstCVSource.SelectedValue).Trim() != "")
			{
				if (lstCVSource.SelectedValue == "3")
				{
					if (lstInterviewerOne.SelectedValue == "" || lstInterviewerOne.SelectedValue == "0")
					{
						lblmessage.Text = "Please select referred by";
						return;
					}
				}
				if (lstCVSource.SelectedValue == "1")
				{
					if (lstVendorName.SelectedValue == "" || lstVendorName.SelectedValue == "0")
					{
						lblmessage.Text = "Please select Vendor Name";
						return;
					}
				}
				if (lstCVSource.SelectedValue == "2")
				{
					if (lstJobSites.SelectedValue == "" || lstJobSites.SelectedValue == "0")
					{
						lblmessage.Text = "Please select job sites name";
						return;
					}
				}
				if (lstCVSource.SelectedValue == "4")
				{
                    if (DDlOther.SelectedValue == "" || DDlOther.SelectedValue == "Select other")
                    {
                        lblmessage.Text = "Please select other ";
                        return;
                    }
                    //if (Txt_ReferredBy.Text == "")
                    //{
                    //	lblmessage.Text = "Please enter other name";
                    //	return;
                    //}
                }
			}

			//Added By Manisha 08-02-2022
			//Txt_CurrentCTC_Fixed
			if (Convert.ToString(Txt_CurrentCTC_Fixed.Text).Trim() != "")
			{
				decimal valueOne = string.IsNullOrEmpty(Txt_CurrentCTC_Fixed.Text) ? 0 : Convert.ToDecimal(Txt_CurrentCTC_Fixed.Text);
				decimal valueTwo = string.IsNullOrEmpty(TxtCurrentCTC_Variable.Text) ? 0 : Convert.ToDecimal(TxtCurrentCTC_Variable.Text);
				TxtCurrentCTC_Total.Text = (valueOne + valueTwo).ToString("N2");
			}

			//TxtExpCTC_Fixed
			if (Convert.ToString(TxtExpCTC_Fixed.Text).Trim() != "")
			{
				decimal valueOne = string.IsNullOrEmpty(TxtExpCTC_Fixed.Text) ? 0 : Convert.ToDecimal(TxtExpCTC_Fixed.Text);
				decimal valueTwo = string.IsNullOrEmpty(TxtExpCTC_Variable.Text) ? 0 : Convert.ToDecimal(TxtExpCTC_Variable.Text);
				TxtExpCTC_Total.Text = (valueOne + valueTwo).ToString("N2");
			}


			if (Convert.ToString(lnkuplodedfile.Text).Trim() == "")
			{
				if (Convert.ToString(uploadfile.FileName).Trim() == "")
				{

					lblmessage.Text = "Please upload Resume";
					return;
				}
			}
			else
			{
				if (Convert.ToString(uploadfile.FileName).Trim() != "")
				{
					string file = "";
					file = (Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateResumeDocpath"]).Trim()), lnkuplodedfile.Text.ToString().Trim()));

					if ((System.IO.File.Exists(file)))
					{
						System.IO.File.Delete(file);
					}
				}

			}

			#endregion

			if (lblmessage.Text.Trim() == "")
			{
				string[] strdate;
				string strtoDate = "";
				string ResumeFileName = "";
				if (Txt_CandidateBirthday.Text != "")
				{
					strdate = Convert.ToString(Txt_CandidateBirthday.Text).Trim().Split('/');
					strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
					//DateTime ddt = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
				}

				//if (Convert.ToString(lnkuplodedfile.Text).Trim() != "")
				{
					if (Convert.ToString(uploadfile.FileName).Trim() != "")
					{
						filename = uploadfile.FileName;
						string strfileName = "";
						// string str = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");+ Txt_CandidateMobile.Text
						string strremoveSpace = Txt_CandidateName.Text;
						strremoveSpace = Regex.Replace(strremoveSpace, @"\s", "");
						ResumeFileName = hdCandidate_ID.Value + "_" + strremoveSpace + Path.GetExtension(uploadfile.FileName);
						filename = ResumeFileName;
						hdfilename.Value = ResumeFileName;
						uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateResumeDocpath"]).Trim()), ResumeFileName));

					}
				}
				if (Convert.ToString(uploadotherfile.FileName).Trim() != "")
				{
					HttpFileCollection fileCollection = Request.Files;
					for (int i = 0; i < fileCollection.Count; i++)
					{
						HttpPostedFile uploadfileName = fileCollection[i];
						string fileName = Path.GetFileName(uploadfileName.FileName);
						if (uploadfileName.ContentLength > 0)
						{
							if (fileName != uploadfile.FileName)
							{
								multiplefilename = fileName;
								string strfileName = "";
								string strremoveSpace = hdCandidate_ID.Value + "_" + multiplefilename;
								// string strremoveSpace = Txt_CandidateName.Text;
								strremoveSpace = Regex.Replace(strremoveSpace, @"\s", "");
								//  strfileName =  Txt_CandidateMobile.Text + "_" + strremoveSpace;
								strfileName = strremoveSpace;
								multiplefilename = strfileName;
								uploadfileName.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateResumeDocpath"]).Trim()), strfileName));
								multiplefilenameadd += strfileName + ",";
								//multiplefilenameadd.TrimEnd(',');
							}
						}
					}
				}
				filename = ResumeFileName != "" ? ResumeFileName : lnkuplodedfile.Text;
				if (lstCVSource.SelectedValue == "3")
				{

					ReferredBy = lstInterviewerOne.SelectedValue;
				}
				if (lstCVSource.SelectedValue == "1")
				{
					vendorID = lstVendorName.SelectedValue;
				}
				if (lstCVSource.SelectedValue == "2")
				{
					JobSitesID = lstJobSites.SelectedValue;
				}
				if (lstCVSource.SelectedValue == "4")
				{
                    //Txt_ReferredBy.Text.Trim();
                    Other = DDlOther.SelectedValue;
                }
				Candidate.Candidate_ID = Convert.ToInt32(hdCandidate_ID.Value);
				Candidate.empcode = Convert.ToString(Session["Empcode"]).Trim();
				Candidate.CandidateName = Txt_CandidateName.Text.Trim();
				Candidate.EmailAddress = Txt_CandidateEmail.Text.Trim();
				Candidate.CandidateBirthday = strtoDate;
				Candidate.CandidateMobile = Txt_CandidateMobile.Text.Trim();
				Candidate.CandidateGender = Convert.ToInt32(lstCandidategender.SelectedValue);
				Candidate.Maritalstatus = Convert.ToInt32(lstMaritalStatus.SelectedValue);
				Candidate.ModuleId = Convert.ToInt32(lstMainSkillset.SelectedValue);
				Candidate.CandidatePAN = Txt_CandidatePAN.Text.Trim();
				Candidate.AdharNo = txtAdharNo.Text.Trim();
				Candidate.CandidateExperience_Years = Txt_CandidateExperence.Text.Trim();
				Candidate.AdditionalSkillset = Txt_AdditionalSkillset.Text.Trim();
				Candidate.Comments = Txt_Comments.Text.Trim();
				Candidate.CVSource_ID = Convert.ToInt32(lstCVSource.SelectedValue);
				Candidate.VenderID = vendorID;
				Candidate.JobSitesID = JobSitesID;
				Candidate.ReferredBy = ReferredBy;
                //Candidate.Others = Txt_ReferredBy.Text.Trim();
                Candidate.Others = Other;
                Candidate.RelevantExpYrs = TxtRelevantExpYrs.Text.Trim();
				Candidate.CurrentCTC_Fixed = Txt_CurrentCTC_Fixed.Text.Trim();
				Candidate.CurrentCTC_Variable = TxtCurrentCTC_Variable.Text.Trim();
				Candidate.CandidateCurrentCTC = TxtCurrentCTC_Total.Text.Trim();
				Candidate.ExpCTC_Fixed = TxtExpCTC_Fixed.Text.Trim();
				Candidate.ExpCTC_Variable = TxtExpCTC_Variable.Text.Trim();
				Candidate.ExpCTC_Total = TxtExpCTC_Total.Text.Trim();
				Candidate.CurrentlyOnNotice = DDlCurrentlyonnotice.SelectedValue.Trim();
				Candidate.NoticePeriod = Txt_NoticePeriodInday.Text.Trim();
				Candidate.CurrentLocation = Txt_CandidateCurrentLocation.Text.Trim();
				Candidate.NativeLocation = Txt_NativeHomeLocation.Text.Trim();
				Candidate.LocationCurrentCompany = Txt_BaseLocationcurrentcompany.Text.Trim();
				Candidate.BaseLocationPreferenceID = DDLBaseLocationPreference.SelectedValue.Trim();
				Candidate.Travelanylocations = DDLRelocateTravelAnyLocation.SelectedValue.Trim();
				Candidate.TravelPandemicSituation = Txt_TravelContraintPandemicSituation.Text.Trim();
				Candidate.OpentoTravel = DDLOpenToTravel.SelectedValue.Trim();
				Candidate.Candidatepayroll = DDlpayrollsCompany.SelectedValue.Trim();
				Candidate.Candidateanybreakservice = DDLBreakInService.SelectedValue.Trim();
				Candidate.ImplementationProjectsWorkOnID = DDLImplementationprojectWorkedOn.SelectedValue.Trim();
				Candidate.DomainExperienceID = DDLDomainExperence.SelectedValue.Trim();
				Candidate.SAPExperienceID = DDLSAPExperence.SelectedValue.Trim();
				Candidate.TotalSupportProjectID = DDLSupportproject.SelectedValue.Trim();
				Candidate.PhasesImplementationWorkId = DDLPhaseWorkimplementation.SelectedValue.Trim();
				Candidate.RoleImplementationProjectID = DDLRolesPlaydImplementation.SelectedValue.Trim();
				Candidate.TypeProjecthandledID = DDLprojecthandled.SelectedValue.Trim();
				Candidate.NatureIndustryClientID = DDLnatureOfIndustryClient.SelectedValue.Trim();
				Candidate.CheckCommunicationSkillID = DDLCommunicationSkill.SelectedValue.Trim();
				Candidate.CurrentRoleorganization = Txt_CurrentRoleorganization.Text.Trim();
				Candidate.RoleDomainCompany = Txt_RoleDomaincompany.Text.Trim();
				Candidate.Reasonforbreak = TxtReasonforBreak.Text.Trim();
				Candidate.lookingChangeReason = Txt_lookingforChange.Text.Trim();
				Candidate.UploadResume = filename;
				Candidate.multiplefilenameadd = multiplefilenameadd;

				dtRecCandidate = spm.InsertRecCreateCandidate(Candidate);
				//dtRecCandidate = spm.InsertRecCreateCandidate1(Convert.ToString(Session["Empcode"]).Trim(), hdCandidate_ID.Value.ToString().Trim(), lstCandidategender.SelectedValue, lstMaritalStatus.SelectedValue, Txt_CandidateName.Text.Trim(), Convert.ToString(strtoDate), Txt_CandidateCurrentLocation.Text.Trim(), Txt_CandidateEmail.Text.Trim(), Txt_CandidateMobile.Text.Trim(), Txt_CandidatePAN.Text.Trim(), Txt_CandidateExperence.Text.Trim(), Txt_CurrentCTC_Fixed.Text.Trim(), TxtCurrentCTC_Variable.Text.Trim(), lstCVSource.SelectedValue, lstMainSkillset.SelectedValue, ReferredBy, vendorID, JobSitesID, Txt_ReferredBy.Text.Trim(), Txt_Comments.Text.Trim(), "", Txt_AdditionalSkillset.Text.Trim(),"", txtAdharNo.Text.Trim(), filename, multiplefilenameadd);
				if (dtRecCandidate.Rows.Count > 0)
				{
					Response.Redirect("~/procs/SearchCandidate.aspx");
				}
				else
				{
					lblmessage.Text = "Please check mobile no , email id already exists";
					return;
				}
			}
			else
			{
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
			//throw;
		}

	}
	protected void Txt_CandidateBirthday_TextChanged(object sender, EventArgs e)
	{
		getBirthYear();
	}
	private void getBirthYear()
	{
		if (Txt_CandidateBirthday.Text != "")
		{
			string[] strdate;
			string strtoDate = "";
			strdate = Convert.ToString(Txt_CandidateBirthday.Text).Trim().Split('/');
			strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
			DateTime ddt = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
			string text = CalculateYourAge(ddt);
			Txt_CandidateAge.Text = text;
		}
	}

	protected void lnkdeleteFiles_Click(object sender, EventArgs e)
	{
		string FileName = "", file = "";
		LinkButton btn = (LinkButton)sender;
		GridViewRow row = (GridViewRow)btn.NamingContainer;
		// hdnRecruitment_ReqID.Value = Convert.ToString(gvotherfile.DataKeys[row.RowIndex].Values[0]).Trim();
		FileName = Convert.ToString(gvotherfile.DataKeys[row.RowIndex].Values[1]).Trim();
		if (FileName != "")
		{
			file = hdfilefath.Value.ToString().Trim() + FileName.ToString().Trim();
			if ((System.IO.File.Exists(file)))
			{
				System.IO.File.Delete(file);
				dtRecCandidate = spm.getDeleteMultipleFile(FileName);
				if (dtRecCandidate.Rows.Count > 0)
				{
					get_Candidate_multipleUploaded_Files();
				}
			}
		}

	}


	protected void trvldeatils_cancel_btn_Click(object sender, EventArgs e)
	{

		try
		{
			string[] strdate, strdate1;
			string StartDate = "", Startdatecheck = "";
			int CandCompanyID = 0;
			string EndDate = "", Stype = "INSERTMaster";
			#region Check All Parameters Selected

			lblCompany.Text = "";


			if (Convert.ToString(txtCompanyName.Text).Trim() == "")
			{
				lblCompany.Text = "Please enter Name of Company ";
				return;
			}
			if (Convert.ToString(txtComDesignation.Text).Trim() == "")
			{
				lblCompany.Text = "Please enter Designation";
				return;
			}
			if (Convert.ToString(txtComStartDate.Text).Trim() == "")
			{
				lblCompany.Text = "Please enter Start Date ";
				return;
			}

			#endregion
			strdate = Convert.ToString(txtComStartDate.Text).Trim().Split('/');
			StartDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
			Startdatecheck = Convert.ToString(strdate[0]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[2]);

			if (txtComEndDate.Text != "")
			{
				strdate1 = Convert.ToString(txtComEndDate.Text).Trim().Split('/');
				EndDate = Convert.ToString(strdate1[2]) + "-" + Convert.ToString(strdate1[1]) + "-" + Convert.ToString(strdate1[0]);
			}
			if ((Convert.ToString(Session["dtCompanyInfo"]).Trim() != "" || Session["dtCompanyInfo"] != null) && lnkbtn_expdtls.Text == "ADD")
			{
				DataTable dt = (DataTable)Session["dtCompanyInfo"];
				if (dt.Rows.Count > 0)
				{
					// string abc = "01-01-2014";              
					int numberOfRecords = dt.Select("StartDate ='" + Startdatecheck + "'").Length;
					//if (targetDt.Ticks > d1.Ticks && targetDt.Ticks < d2.Ticks)
					//{
					//	// targetDt is in between d1 and d2
					//}
					//foreach (DataRow dr in dt.Rows)
					//{
					//	dr["StartDate"] = DateTime.Parse((dr["StartDate"].ToString())).ToString("dd/MM/yyyy");
					//}
					if (numberOfRecords > 0)
					{
						lblCompany.Text = "Record already exists!.";
						return;
					}
				}
			}
			if (lnkbtn_expdtls.Text != "ADD")
			{
				CandCompanyID = Convert.ToString(hdnCandCompanyID.Value).Trim() != "" ? Convert.ToInt32(hdnCandCompanyID.Value) : 0;
			}
			dtCompanyInfo = spm.Insert_Temp_Candid_Company_Detail(Stype, CandCompanyID, txtCompanyName.Text.Trim(), txtComDesignation.Text.Trim(), StartDate, EndDate, Txt_CandidateMobile.Text.Trim(), "", Convert.ToInt32(hdCandidate_ID.Value), Convert.ToString(Session["Empcode"]).Trim());
			if (dtCompanyInfo.Rows.Count > 0)
			{
				GrdCompanyInfo.DataSource = dtCompanyInfo;
				GrdCompanyInfo.DataBind();
				CompanyClear();
				Session.Add("dtCompanyInfo", dtCompanyInfo);
				lnkbtn_expdtls.Text = "ADD"; lnkbtn_expdtls.ToolTip = "ADD";
			}
			else
			{
				lblCompany.Text = "Record already exists!.";
			}


		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
			//throw;
		}
	}

	private void EducationClear()
	{
		lstEducationType.SelectedIndex = -1;
		DDLFullTime.SelectedIndex = -1;
		txtCollegeName.Text = "";
		txtYearofPassing.Text = "";
		txtFinalScore.Text = "";
		txtUniversity.Text = "";
		txtDiscipline.Text = "";
		txtDiscipline.Enabled = false;
		DDLFullTime.Enabled = false;
	}
	protected void BindEducationData()
	{
		//dtEducation = new DataTable();
		//dtEducation.Columns.Add("EducationType", typeof(string));
		//dtEducation.Columns.Add("CollegeName", typeof(string));
		//dtEducation.Columns.Add("YearofPassing", typeof(string));
		//dtEducation.Columns.Add("FinalScore", typeof(string));
		//dtEducation.Columns.Add("CREATEDBY", typeof(string));
		//dtEducation.Columns.Add("EducationTypeID", typeof(string));
		//dtEducation.Columns.Add("Candidate_ID", typeof(string));

		//Session.Add("dtEducation", dtEducation);

		//dtCompanyInfo = new DataTable();
		//dtCompanyInfo.Columns.Add("NameofCompany", typeof(string));
		//dtCompanyInfo.Columns.Add("CandDesignation", typeof(string));
		//dtCompanyInfo.Columns.Add("StartDate", typeof(string));
		//dtCompanyInfo.Columns.Add("EndDate", typeof(string));
		//dtCompanyInfo.Columns.Add("CREATEDBY", typeof(string));
		//dtCompanyInfo.Columns.Add("Candidate_ID", typeof(string));

		//Session.Add("dtCompanyInfo", dtCompanyInfo);
	}

	protected void trvldeatils_delete_btn_Click(object sender, EventArgs e)
	{
		try
		{

			#region Check All Parameters Selected
			string Stype = "INSERTMaster";
			lblEducation.Text = "";
			int CandEducationID = 0;
			decimal FinalScore = 0;

			if (Convert.ToString(lstEducationType.SelectedValue).Trim() == "" || Convert.ToString(lstEducationType.SelectedValue).Trim() == "0")
			{
				lblEducation.Text = "Please Select  Education Type";
				return;
			}

			if (Convert.ToString(lstEducationType.SelectedValue).Trim() == "5")
			{
				if (Convert.ToString(DDLFullTime.SelectedValue).Trim() == "" || Convert.ToString(DDLFullTime.SelectedValue).Trim() == "0")
				{
					lblEducation.Text = "Please Select  Full Time / Part Time";
					return;
				}
			}

			if (Convert.ToString(txtCollegeName.Text).Trim() == "")
			{
				lblEducation.Text = "Please enter College / Institute/Professional body";
				return;
			}
			if (Convert.ToString(txtUniversity.Text).Trim() == "")
			{
				lblEducation.Text = "Please enter University Name / Board";
				return;
			}
			if (Convert.ToString(txtYearofPassing.Text).Trim() == "")
			{
				lblEducation.Text = "Please enter Year of Passing ";
				return;
			}
			#endregion
			if (Convert.ToInt32(lstEducationType.SelectedValue) == 1 || Convert.ToInt32(lstEducationType.SelectedValue) == 2 || Convert.ToInt32(lstEducationType.SelectedValue) == 3)
			{
				if ((Convert.ToString(Session["dtEducation"]).Trim() != "" || Session["dtEducation"] != null) && trvl_localbtn.Text == "ADD")
				{
					DataTable dt = (DataTable)Session["dtEducation"];
					if (dt.Rows.Count > 0)
					{
						//var rows2 = from row in dt.AsEnumerable()
						//            where row.Field<string>("EducationTypeID") == "1" select row;
						int numberOfRecords = dt.Select("EducationTypeID = " + Convert.ToInt32(lstEducationType.SelectedValue) + "").Length;

						if (numberOfRecords > 0)
						{
							lblEducation.Text = "Record already exists!.";
							return;
						}
					}
				}
			}
			if (trvl_localbtn.Text != "ADD")
			{
				CandEducationID = Convert.ToString(hdnCandEducationID.Value).Trim() != "" ? Convert.ToInt32(hdnCandEducationID.Value) : 0;
			}
			// FinalScore = Convert.ToString(txtFinalScore.Text.Trim()) != "" ? Convert.ToDecimal(txtFinalScore.Text.Trim()) : 0;
			dtEducation = spm.Insert_Temp_Candid_Education_Detail(Stype, CandEducationID, Convert.ToInt32(lstEducationType.SelectedValue), txtCollegeName.Text.Trim(), txtYearofPassing.Text.Trim(), Convert.ToString(txtFinalScore.Text.Trim()), DDLFullTime.SelectedValue, txtUniversity.Text.Trim(), txtDiscipline.Text.Trim(), Txt_CandidateMobile.Text.Trim(), "", Convert.ToInt32(hdCandidate_ID.Value), Convert.ToString(Session["Empcode"]).Trim());
			if (dtEducation.Rows.Count > 0)
			{
				DgvEducationDetails.DataSource = dtEducation;
				DgvEducationDetails.DataBind();
				EducationClear();
				Session.Add("dtEducation", dtEducation);
				trvl_localbtn.Text = "ADD"; trvl_localbtn.ToolTip = "ADD";
				lstEducationType.Enabled = true;
			}
			else
			{
				lblEducation.Text = "Record already exists!.";
			}

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
			//throw;
		}

	}
	private void CompanyClear()
	{

		txtCompanyName.Text = "";
		txtComEndDate.Text = "";
		txtComStartDate.Text = "";
		txtComDesignation.Text = "";
	}



	protected void btn_del_Click(object sender, ImageClickEventArgs e)
	{
		DataTable DTEdit = new DataTable();
		string CandEducationID = "0";
		ImageButton btn = (ImageButton)sender;
		GridViewRow row = (GridViewRow)btn.NamingContainer;
		CandEducationID = Convert.ToString(DgvEducationDetails.DataKeys[row.RowIndex].Values[0]).Trim();
		hdnCandEducationID.Value = CandEducationID;
		DTEdit = spm.Insert_Temp_Candid_Education_Detail("DeleteMaster", Convert.ToInt32(CandEducationID), 0, "", "", "", "", "", "", Txt_CandidateMobile.Text.Trim(), "", Convert.ToInt32(hdCandidate_ID.Value), Convert.ToString(Session["Empcode"]).Trim());
		if (DTEdit.Rows.Count > 0)
		{
			DgvEducationDetails.DataSource = DTEdit;
			DgvEducationDetails.DataBind();
			Session.Add("dtEducation", DTEdit);
		}
	}

	protected void btn_Comp_Click(object sender, ImageClickEventArgs e)
	{
		DataTable DTEdit = new DataTable();
		string CandCompanyID = "0";
		ImageButton btn = (ImageButton)sender;
		GridViewRow row = (GridViewRow)btn.NamingContainer;
		CandCompanyID = Convert.ToString(GrdCompanyInfo.DataKeys[row.RowIndex].Values[0]).Trim();
		// hdnCandCompanyID.Value = CandEducationID;
		// DTEdit = spm.Insert_Temp_Candid_Education_Detail("Delete", Convert.ToInt32(CandCompanyID), 0, "", "", Convert.ToDecimal(0), Txt_CandidateMobile.Text.Trim(), Convert.ToString(Session["Empcode"]).Trim());
		DTEdit = spm.Insert_Temp_Candid_Company_Detail("DeleteMaster", Convert.ToInt32(CandCompanyID), "", "", "", "", Txt_CandidateMobile.Text.Trim(), "", Convert.ToInt32(hdCandidate_ID.Value), Convert.ToString(Session["Empcode"]).Trim());

		if (DTEdit.Rows.Count > 0)
		{
			GrdCompanyInfo.DataSource = DTEdit;
			GrdCompanyInfo.DataBind();
			Session.Add("dtCompanyInfo", DTEdit);
		}
	}

	protected void txtComStartDate_TextChanged(object sender, EventArgs e)
	{
		lblCompany.Text = "";
		if (txtComEndDate.Text.Trim() != "")
		{
			if (Date_Validation() == false)
			{
				lblCompany.Text = "Start date cannot be greater than End Date";
				return;
			}
		}

	}

	//protected void txtComEndDate_TextChanged(object sender, EventArgs e)
	//{
	//    if (txtComStartDate.Text.Trim() != "" && txtComEndDate.Text.Trim() != "")
	//    {
	//        if (Date_Validation() == false)
	//        {
	//            lblCompany.Text = "Start date cannot be greater than End Date";
	//            return;
	//        }
	//    }

	//}
	private Boolean Date_Validation()
	{
		lblCompany.Text = "";
		Boolean blnValid = false;
		string[] strdate, strdate1;
		string StartDate = "", EndDate = "";
		strdate = Convert.ToString(txtComStartDate.Text).Trim().Split('/');
		StartDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
		DateTime ddt = DateTime.ParseExact(StartDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

		strdate1 = Convert.ToString(txtComEndDate.Text).Trim().Split('/');
		EndDate = Convert.ToString(strdate1[2]) + "-" + Convert.ToString(strdate1[1]) + "-" + Convert.ToString(strdate1[0]);
		DateTime ddt2 = DateTime.ParseExact(EndDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

		if (ddt < ddt2)
		{

			blnValid = true;
		}

		return blnValid;
	}

	protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
	{
		DataTable DTEdit = new DataTable();
		string CandEducationID = "0";
		ImageButton btn = (ImageButton)sender;
		GridViewRow row = (GridViewRow)btn.NamingContainer;
		CandEducationID = Convert.ToString(DgvEducationDetails.DataKeys[row.RowIndex].Values[0]).Trim();
		hdnCandEducationID.Value = CandEducationID;
		DTEdit = spm.Insert_Temp_Candid_Education_Detail("SelectMaster", Convert.ToInt32(CandEducationID), 0, "", "", "", "", "", "", Txt_CandidateMobile.Text.Trim(), "", Convert.ToInt32(hdCandidate_ID.Value), Convert.ToString(Session["Empcode"]).Trim());
		if (DTEdit.Rows.Count > 0)
		{
			lstEducationType.SelectedValue = Convert.ToString(DTEdit.Rows[0]["EducationTypeID"]).Trim();
			txtCollegeName.Text = Convert.ToString(DTEdit.Rows[0]["CollegeName"]).Trim();
			txtYearofPassing.Text = Convert.ToString(DTEdit.Rows[0]["YearofPassing"]).Trim();
			txtFinalScore.Text = Convert.ToString(DTEdit.Rows[0]["FinalScore"]).Trim();
			txtDiscipline.Text = Convert.ToString(DTEdit.Rows[0]["PGDiscipline"]).Trim();
			DDLFullTime.SelectedValue = Convert.ToString(DTEdit.Rows[0]["PGType"]).Trim();
			txtUniversity.Text = Convert.ToString(DTEdit.Rows[0]["PGUniversityName"]).Trim();
			// Convert.ToString(hdnQuest_ID.Value).Trim() != "" ? Convert.ToInt32(hdnQuest_ID.Value) : 0;
			trvl_localbtn.Text = "Modify"; trvl_localbtn.ToolTip = "Modify";
			lstEducationType.Enabled = false;
			if (lstEducationType.SelectedValue == "5")
			{
				DDLFullTime.Enabled = true;
				txtDiscipline.Enabled = true;
				spfull.Visible = true;
			}
			else if(lstEducationType.SelectedValue == "4")
			{
				DDLFullTime.Enabled = false;
				txtDiscipline.Enabled = true;
				spfull.Visible = false;
				DDLFullTime.SelectedValue = "Full Time";
			}
			else
			{
				DDLFullTime.Enabled = false;
				txtDiscipline.Enabled = false;
				spfull.Visible = false;
				DDLFullTime.SelectedValue = "Full Time";
			}

		}

	}

	protected void lnkCmpEdit_Click(object sender, ImageClickEventArgs e)
	{
		DataTable DTEdit = new DataTable();
		string CandCompanyID = "0";
		ImageButton btn = (ImageButton)sender;
		GridViewRow row = (GridViewRow)btn.NamingContainer;
		CandCompanyID = Convert.ToString(GrdCompanyInfo.DataKeys[row.RowIndex].Values[0]).Trim();
		hdnCandCompanyID.Value = CandCompanyID;
		// DTEdit = spm.Insert_Temp_Candid_Education_Detail("Delete", Convert.ToInt32(CandCompanyID), 0, "", "", Convert.ToDecimal(0), Txt_CandidateMobile.Text.Trim(), Convert.ToString(Session["Empcode"]).Trim());
		DTEdit = spm.Insert_Temp_Candid_Company_Detail("SelectMaster", Convert.ToInt32(CandCompanyID), "", "", "", "", Txt_CandidateMobile.Text.Trim(), "", Convert.ToInt32(hdCandidate_ID.Value), Convert.ToString(Session["Empcode"]).Trim());

		if (DTEdit.Rows.Count > 0)
		{
			txtCompanyName.Text = Convert.ToString(DTEdit.Rows[0]["NameofCompany"]).Trim();
			txtComDesignation.Text = Convert.ToString(DTEdit.Rows[0]["CandDesignation"]).Trim();
			txtComStartDate.Text = Convert.ToString(DTEdit.Rows[0]["StartDate"]).Trim();
			txtComEndDate.Text = Convert.ToString(DTEdit.Rows[0]["EndDate"]).Trim();
			// Convert.ToString(hdnQuest_ID.Value).Trim() != "" ? Convert.ToInt32(hdnQuest_ID.Value) : 0;
			lnkbtn_expdtls.Text = "Modify"; lnkbtn_expdtls.ToolTip = "Modify";

		}

	}

	protected void Txt_CurrentCTC_Fixed_TextChanged(object sender, EventArgs e)
	{
		decimal valueOne = string.IsNullOrEmpty(Txt_CurrentCTC_Fixed.Text) ? 0 : Convert.ToDecimal(Txt_CurrentCTC_Fixed.Text);
		decimal valueTwo = string.IsNullOrEmpty(TxtCurrentCTC_Variable.Text) ? 0 : Convert.ToDecimal(TxtCurrentCTC_Variable.Text);
		TxtCurrentCTC_Total.Text = (valueOne + valueTwo).ToString("N2");

	}

	protected void TxtExpCTC_Fixed_TextChanged(object sender, EventArgs e)
	{
		decimal valueOne = string.IsNullOrEmpty(TxtExpCTC_Fixed.Text) ? 0 : Convert.ToDecimal(TxtExpCTC_Fixed.Text);
		decimal valueTwo = string.IsNullOrEmpty(TxtExpCTC_Variable.Text) ? 0 : Convert.ToDecimal(TxtExpCTC_Variable.Text);
		TxtExpCTC_Total.Text = (valueOne + valueTwo).ToString("N2");
	}

	protected void lstEducationType_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (lstEducationType.SelectedValue =="5")
		{
			DDLFullTime.Enabled = true;
			txtDiscipline.Enabled = true;
			spfull.Visible = true;
		}
		else if (lstEducationType.SelectedValue == "4")
		{
			DDLFullTime.Enabled = false;
			txtDiscipline.Enabled = true;
			spfull.Visible = false;
			DDLFullTime.SelectedValue = "Full Time";
		}
		else
		{
			DDLFullTime.Enabled = false;
			txtDiscipline.Enabled = false;
			spfull.Visible = false;
			DDLFullTime .SelectedValue= "Full Time";
		}
	}
	protected void Txt_CandidateMobile_TextChanged(object sender, EventArgs e)
	{
		lblmessage.Text = "";
		DataTable Dt = spm.IsMobileExistsInMaster(Txt_CandidateMobile.Text.ToString().Trim());
		if (Dt.Rows.Count > 0)
		{
			lblmessage.Text = "Mobile No already exists!.";
			return;
		}
		else
		{
			lblmessage.Text = "";
		}
	}
	protected void Txt_CandidateEmail_TextChanged(object sender, EventArgs e)
	{
		lblmessage.Text = "";
		DataTable Dt = spm.IsEmailExistsInMaster(Txt_CandidateEmail.Text.ToString().Trim());
		if (Dt.Rows.Count > 0)
		{
			lblmessage.Text = "Email already exists!.";
			return;
		}
		else
		{
			lblmessage.Text = "";
		}
	}

	protected void localtrvl_btnSave_Click(object sender, EventArgs e)
	{
		try
		{
			string vendorID = "", JobSitesID = "", ReferredBy = "", Other = ""; string ResumeFileName = "";
			#region Check All Parameters Selected

			string confirmValue = hdnYesNo.Value.ToString();
			if (confirmValue != "Yes")
			{
				return;
			}

			lblmessage.Text = "";

			if (Convert.ToString(Txt_CandidateName.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter name";
				return;
			}
			if (Convert.ToString(Txt_CandidateEmail.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter Email";
				return;
			}
			if (Convert.ToString(Txt_CandidateMobile.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter Mobile No.";
				return;
			}
			if (Convert.ToString(Txt_CandidateMobile.Text).Trim() != "")
			{
				int count = Txt_CandidateMobile.Text.Length;
				if (count < 10)
				{
					lblmessage.Text = "Please enter proper Mobile No.";
					return;
				}
			}
			
				if (Convert.ToString(uploadfile.FileName).Trim() != "")
				{
					string file = "";
					file = (Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateResumeDocpath"]).Trim()), lnkuplodedfile.Text.ToString().Trim()));

					if ((System.IO.File.Exists(file)))
					{
						System.IO.File.Delete(file);
					}
				}

			#endregion

			if (lblmessage.Text.Trim() == "")
			{
				string[] strdate;
				string strtoDate = "";
				if (Txt_CandidateBirthday.Text != "")
				{
					strdate = Convert.ToString(Txt_CandidateBirthday.Text).Trim().Split('/');
					strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
					//DateTime ddt = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
				}

				
					if (Convert.ToString(uploadfile.FileName).Trim() != "")
					{
						filename = uploadfile.FileName;
						string strfileName = "";
						// string str = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");+ Txt_CandidateMobile.Text
						string strremoveSpace = Txt_CandidateName.Text;
						strremoveSpace = Regex.Replace(strremoveSpace, @"\s", "");
						ResumeFileName = hdCandidate_ID.Value + "_" + strremoveSpace + Path.GetExtension(uploadfile.FileName);
						filename = ResumeFileName;
						hdfilename.Value = ResumeFileName;
						uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateResumeDocpath"]).Trim()), ResumeFileName));

					}
				
				if (Convert.ToString(uploadotherfile.FileName).Trim() != "")
				{
					HttpFileCollection fileCollection = Request.Files;
					for (int i = 0; i < fileCollection.Count; i++)
					{
						HttpPostedFile uploadfileName = fileCollection[i];
						string fileName = Path.GetFileName(uploadfileName.FileName);
						if (uploadfileName.ContentLength > 0)
						{
							if (fileName != uploadfile.FileName)
							{
								multiplefilename = fileName;
								string strfileName = "";
								string strremoveSpace = hdCandidate_ID.Value + "_" + multiplefilename;
								// string strremoveSpace = Txt_CandidateName.Text;
								strremoveSpace = Regex.Replace(strremoveSpace, @"\s", "");
								//  strfileName =  Txt_CandidateMobile.Text + "_" + strremoveSpace;
								strfileName = strremoveSpace;
								multiplefilename = strfileName;
								uploadfileName.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateResumeDocpath"]).Trim()), strfileName));
								multiplefilenameadd += strfileName + ",";
								//multiplefilenameadd.TrimEnd(',');
							}
						}
					}
				}
				filename = ResumeFileName != "" ? ResumeFileName : lnkuplodedfile.Text;

				if (lstCVSource.SelectedValue == "3")
				{

					ReferredBy = lstInterviewerOne.SelectedValue;
				}
				if (lstCVSource.SelectedValue == "1")
				{
					vendorID = lstVendorName.SelectedValue;
				}
				if (lstCVSource.SelectedValue == "2")
				{
					JobSitesID = lstJobSites.SelectedValue;
				}
				if (lstCVSource.SelectedValue == "4")
				{
					Other = DDlOther.SelectedValue;
					//Txt_ReferredBy.Text.Trim();
				}

				//Added By Manisha 03-02-2022
				//Txt_CurrentCTC_Fixed
				if (Convert.ToString(Txt_CurrentCTC_Fixed.Text).Trim() != "")
				{
					decimal valueOne = string.IsNullOrEmpty(Txt_CurrentCTC_Fixed.Text) ? 0 : Convert.ToDecimal(Txt_CurrentCTC_Fixed.Text);
					decimal valueTwo = string.IsNullOrEmpty(TxtCurrentCTC_Variable.Text) ? 0 : Convert.ToDecimal(TxtCurrentCTC_Variable.Text);
					TxtCurrentCTC_Total.Text = (valueOne + valueTwo).ToString("N2");
				}

				//TxtExpCTC_Fixed
				if (Convert.ToString(TxtExpCTC_Fixed.Text).Trim() != "")
				{
					decimal valueOne1 = string.IsNullOrEmpty(TxtExpCTC_Fixed.Text) ? 0 : Convert.ToDecimal(TxtExpCTC_Fixed.Text);
					decimal valueTwo1 = string.IsNullOrEmpty(TxtExpCTC_Variable.Text) ? 0 : Convert.ToDecimal(TxtExpCTC_Variable.Text);
					TxtExpCTC_Total.Text = (valueOne1 + valueTwo1).ToString("N2");
				}
				Candidate.Candidate_ID = Convert.ToString(hdCandidate_ID.Value).Trim() != "" ? Convert.ToInt32(hdCandidate_ID.Value) : 0;
				Candidate.empcode = Convert.ToString(Session["Empcode"]).Trim();
				Candidate.CandidateName = Txt_CandidateName.Text.Trim();
				Candidate.EmailAddress = Txt_CandidateEmail.Text.Trim();
				Candidate.CandidateBirthday = strtoDate;
				Candidate.CandidateMobile = Txt_CandidateMobile.Text.Trim();
				Candidate.CandidateGender = Convert.ToInt32(lstCandidategender.SelectedValue);
				Candidate.Maritalstatus = Convert.ToInt32(lstMaritalStatus.SelectedValue);
				Candidate.ModuleId = Convert.ToInt32(lstMainSkillset.SelectedValue);
				Candidate.CandidatePAN = Txt_CandidatePAN.Text.Trim();
				Candidate.AdharNo = txtAdharNo.Text.Trim();
				Candidate.CandidateExperience_Years = Txt_CandidateExperence.Text.Trim();
				Candidate.AdditionalSkillset = Txt_AdditionalSkillset.Text.Trim();
				Candidate.Comments = Txt_Comments.Text.Trim();
				Candidate.CVSource_ID = Convert.ToInt32(lstCVSource.SelectedValue);
				Candidate.VenderID = vendorID;
				Candidate.JobSitesID = JobSitesID;
				Candidate.ReferredBy = ReferredBy;
				//Candidate.Others = Txt_ReferredBy.Text.Trim();
				Candidate.Others = Other;
				Candidate.RelevantExpYrs = TxtRelevantExpYrs.Text.Trim();
				Candidate.CurrentCTC_Fixed = Txt_CurrentCTC_Fixed.Text.Trim();
				Candidate.CurrentCTC_Variable = TxtCurrentCTC_Variable.Text.Trim();
				Candidate.CandidateCurrentCTC = TxtCurrentCTC_Total.Text.Trim();
				Candidate.ExpCTC_Fixed = TxtExpCTC_Fixed.Text.Trim();
				Candidate.ExpCTC_Variable = TxtExpCTC_Variable.Text.Trim();
				Candidate.ExpCTC_Total = TxtExpCTC_Total.Text.Trim();
				Candidate.CurrentlyOnNotice = DDlCurrentlyonnotice.SelectedValue.Trim();
				Candidate.NoticePeriod = Txt_NoticePeriodInday.Text.Trim();
				Candidate.CurrentLocation = Txt_CandidateCurrentLocation.Text.Trim();
				Candidate.NativeLocation = Txt_NativeHomeLocation.Text.Trim();
				Candidate.LocationCurrentCompany = Txt_BaseLocationcurrentcompany.Text.Trim();
				Candidate.BaseLocationPreferenceID = DDLBaseLocationPreference.SelectedValue.Trim();
				Candidate.Travelanylocations = DDLRelocateTravelAnyLocation.SelectedValue.Trim();
				Candidate.TravelPandemicSituation = Txt_TravelContraintPandemicSituation.Text.Trim();
				Candidate.OpentoTravel = DDLOpenToTravel.SelectedValue.Trim();
				Candidate.Candidatepayroll = DDlpayrollsCompany.SelectedValue.Trim();
				Candidate.Candidateanybreakservice = DDLBreakInService.SelectedValue.Trim();
				Candidate.ImplementationProjectsWorkOnID = DDLImplementationprojectWorkedOn.SelectedValue.Trim();
				Candidate.DomainExperienceID = DDLDomainExperence.SelectedValue.Trim();
				Candidate.SAPExperienceID = DDLSAPExperence.SelectedValue.Trim();
				Candidate.TotalSupportProjectID = DDLSupportproject.SelectedValue.Trim();
				Candidate.PhasesImplementationWorkId = DDLPhaseWorkimplementation.SelectedValue.Trim();
				Candidate.RoleImplementationProjectID = DDLRolesPlaydImplementation.SelectedValue.Trim();
				Candidate.TypeProjecthandledID = DDLprojecthandled.SelectedValue.Trim();
				Candidate.NatureIndustryClientID = DDLnatureOfIndustryClient.SelectedValue.Trim();
				Candidate.CheckCommunicationSkillID = DDLCommunicationSkill.SelectedValue.Trim();
				Candidate.CurrentRoleorganization = Txt_CurrentRoleorganization.Text.Trim();
				Candidate.RoleDomainCompany = Txt_RoleDomaincompany.Text.Trim();
				Candidate.Reasonforbreak = TxtReasonforBreak.Text.Trim();
				Candidate.lookingChangeReason = Txt_lookingforChange.Text.Trim();
				Candidate.UploadResume =  filename;
				Candidate.multiplefilenameadd = multiplefilenameadd;
				dtRecCandidate = spm.Insert_SaveasDraft_Candidate(Candidate);
				if (dtRecCandidate.Rows.Count > 0)
				{
					Response.Redirect("~/procs/SearchCandidate.aspx");
				}
				else
				{
					lblmessage.Text = "Record already exists";
					return;
				}
			}
			else
			{
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
			//throw;
		}
	}

	protected void lnkView_Click(object sender, ImageClickEventArgs e)
	{
		ImageButton btn = (ImageButton)sender;
		GridViewRow row = (GridViewRow)btn.NamingContainer;
		hdRecruitment_ReqID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
		hdCandidate_ID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
		txtRec_No.Text = row.Cells[1].Text;
		txtPositionInterviwed.Text =row.Cells[4].Text;
		txtpostionTitle.Text =row.Cells[3].Text;
		getIrSheetSummary();
		this.ModalPopupExtenderIRSheet.Show();
	}
	private void getIrSheetSummary()
	{

		//txtRec_No.Text = txtReqNumber.Text;
		//txtCandidateName.Text = txtName.Text;
		//txtPositionInterviwed.Text = lstSkillset.SelectedItem.Text;
		//txttotalExperince.Text = TxtTotalExperienceYrs.Text;
		//txtRelevantExp.Text = TxtRelevantExpYrs.Text;
		//txtpostionTitle.Text = lstPositionName.SelectedItem.Text;
		//this.DgvIrSheetSummary.Columns.RemoveAt(this.DgvIrSheetSummary.Columns.Count - 1);
		DgvIrSheetSummary.DataSource = null;
		DgvIrSheetSummary.DataBind();
		GrdIRIntSummary.DataSource = null;
		GrdIRIntSummary.DataBind();
		DataTable dtMerged = new DataTable();
		dtIrSheetReport = new DataSet();
		dtIrSheetReport = spm.Get_Rec_Recruit_IrSheetDetails("GetIrSheetSummary", Convert.ToInt32(hdRecruitment_ReqID.Value), Convert.ToInt32(hdCandidate_ID.Value));
		if (dtIrSheetReport.Tables[0].Rows.Count > 0)
		{
			dtMerged = new DataTable();
			DTInterviews = new DataTable();
			DTMaintable = new DataTable();

			if (dtIrSheetReport.Tables[3].Rows.Count > 0)
			{
				DTInterviews = GenerateColumn(dtIrSheetReport.Tables[3]);
			}
			if (dtIrSheetReport.Tables[1].Rows.Count > 0)
			{
				DTMaintable = getData(dtIrSheetReport.Tables[0], dtIrSheetReport.Tables[1]);
			}
			if (dtIrSheetReport.Tables[3].Rows.Count > 0)
			{
				dtMerged = MergeTables(DTMaintable, DTInterviews);
			}
			if (dtIrSheetReport.Tables[2].Rows.Count > 0)
			{
				GrdIRIntSummary.DataSource = dtIrSheetReport.Tables[2];
				GrdIRIntSummary.DataBind();
			}
			int cou = DgvIrSheetSummary.Columns.Count - 1;
			for (int i = cou; 1 < i; i--)
			{
				DgvIrSheetSummary.Columns.RemoveAt(i);
			}

			string RoundName = "";
			int B = 0, C = 0;
			if (dtIrSheetReport.Tables[3].Rows.Count > 0)

			{
				foreach (DataColumn col in DTInterviews.Columns)
				{
					if (C == 2)
					{
						B++;
						C = 0;
					}
					RoundName = NewDT.Rows[B]["InterviewRound"].ToString();
					var NewHeaderName = Regex.Replace(col.ColumnName, @"[\d-]", string.Empty);
					BoundField bfield = new BoundField();
					bfield.DataField = col.ColumnName;
					bfield.HeaderText = NewHeaderName + " - " + RoundName;
					bfield.ItemStyle.BorderColor = Color.Navy;
					DgvIrSheetSummary.Columns.Add(bfield);
					C++;
				}
			}

			DgvIrSheetSummary.DataSource = dtMerged;
			this.DgvIrSheetSummary.DataBind();
			this.updatePanel.Update();

		}
	}
	private DataTable GenerateColumn(DataTable dt)
	{
		NewDT = new DataTable();
		NewDTValue = new DataTable();
		DataView view = new DataView(dt);
		NewDT = view.ToTable(true, "InterviewRound");

		DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
		for (int i = 0; i < NewDT.Rows.Count; i++)
		{
			dcol = new DataColumn("Observation" + i, typeof(System.String));
			NewDTValue.Columns.Add(dcol);
			dcol = new DataColumn("Rating" + i, typeof(System.String));
			NewDTValue.Columns.Add(dcol);
		}
		DataRow dr;
		int m = 0;
		int A = 1;
		int Pre = 0, Next = 0;
		dr = NewDTValue.NewRow();
		dr["Observation" + m] = "";
		dr["Rating" + m] = "";
		NewDTValue.Rows.Add(dr);
		//m++;
		for (int j = 0; j < dt.Rows.Count; j++)
		{

			if (Convert.ToInt32(dt.Rows[j]["IsRound"]) == 1)
			{
				dr = NewDTValue.NewRow();
				dr["Observation" + m] = Convert.ToString(dt.Rows[j]["Observation_Remarks"]).Trim();
				dr["Rating" + m] = Convert.ToString(dt.Rows[j]["RatingName"]).Trim();
				NewDTValue.Rows.Add(dr);
			}
			else
			{
				Pre = Convert.ToInt32(dt.Rows[j - 1]["IsRound"]);
				Next = Convert.ToInt32(dt.Rows[j]["IsRound"]);
				if (Pre != Next)
				{
					m++;
					A = 1;
				}
				NewDTValue.Rows[A]["Observation" + m] = Convert.ToString(dt.Rows[j]["Observation_Remarks"]).Trim();
				NewDTValue.Rows[A]["Rating" + m] = Convert.ToString(dt.Rows[j]["RatingName"]).Trim();
				A++;
			}
		}

		return NewDTValue;
	}
	public DataTable getData(DataTable dt1, DataTable dt2)
	{
		try
		{
			int MainID = 0;
			dtMerge = new DataTable();
			dtMerge.Columns.Add("Main_Type_ID", typeof(string));
			dtMerge.Columns.Add("Heading", typeof(string));
			dtMerge.Columns.Add("Ishedeing", typeof(string));
			dtMerge.Columns.Add("SubType_ID", typeof(string));
			dtMerge.Columns.Add("SubType_Rating", typeof(string));
			DataRow dr;
			for (int i = 0; i < dt1.Rows.Count; i++)
			{
				dr = dtMerge.NewRow();
				dr["Main_Type_ID"] = Convert.ToString(dt1.Rows[i]["Main_Type_ID"]).Trim();
				dr["Heading"] = Convert.ToString(dt1.Rows[i]["Heading"]).Trim();
				dr["Ishedeing"] = 0;
				dr["SubType_ID"] = 0;
				dr["SubType_Rating"] = 'N';
				dtMerge.Rows.Add(dr);
				MainID = Convert.ToInt32(dt1.Rows[i]["Main_Type_ID"]);
				for (int j = 0; j < dt2.Rows.Count; j++)
				{
					//var valueFormain2
					if (MainID == Convert.ToInt32(dt2.Rows[j]["Main_Type_ID"]))
					{
						dr = dtMerge.NewRow();
						dr["Main_Type_ID"] = Convert.ToString(dt2.Rows[j]["Main_Type_ID"]).Trim();
						dr["Heading"] = Convert.ToString(dt2.Rows[j]["Heading"]).Trim();
						dr["Ishedeing"] = Convert.ToString(dt2.Rows[j]["Ishedeing"]).Trim();
						dr["SubType_ID"] = Convert.ToString(dt2.Rows[j]["SubType_ID"]).Trim();
						dr["SubType_Rating"] = Convert.ToString(dt2.Rows[j]["SubType_Rating"]).Trim();
						dtMerge.Rows.Add(dr);
					}
				}
			}
			//DgvIrSheet.DataSource = dtMerge;
			//DgvIrSheet.DataBind();
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
		return dtMerge;
	}
	public static DataTable MergeTables(DataTable baseTable, params DataTable[] additionalTables)
	{
		DataTable merged = baseTable;
		foreach (DataTable dt in additionalTables)
		{
			merged = AddTable(merged, dt);
		}
		return merged;
	}
	public static DataTable AddTable(DataTable baseTable, DataTable additionalTable)
	{
		// Build combined table columns
		DataTable merged = baseTable.Clone();                  // Include all columns from t1 in result.
		foreach (DataColumn col in additionalTable.Columns)
		{
			string newColumnName = col.ColumnName;
			merged.Columns.Add(newColumnName, col.DataType);
		}
		// Add all rows from both tables
		var bt = baseTable.AsEnumerable();
		var at = additionalTable.AsEnumerable();
		var mergedRows = bt.Zip(at, (r1, r2) => r1.ItemArray.Concat(r2.ItemArray).ToArray());
		foreach (object[] rowFields in mergedRows)
		{
			merged.Rows.Add(rowFields);
		}
		return merged;
	}
	protected void DgvIrSheetSummary_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string lsDataKeyValue = DgvIrSheetSummary.DataKeys[e.Row.RowIndex].Values[1].ToString();
			string lsDataKeyValue2 = DgvIrSheetSummary.DataKeys[e.Row.RowIndex].Values[2].ToString();

			if (e.Row.Cells[1].Text.Trim() == "&nbsp;")
			{
				e.Row.Visible = false;
			}

			if (Convert.ToString(lsDataKeyValue).Trim() == "0")
			{
				e.Row.Cells[1].Style["font-size"] = "16px";
				e.Row.Cells[1].Style["color"] = "#000066";
			}

			if (Convert.ToString(lsDataKeyValue).Trim() == "1")
			{
				e.Row.Cells[1].Font.Bold = true;
				e.Row.Cells[1].Style["font-size"] = "13px";
				//e.Row.BackColor = Color.Teal;//HotPink  Teal
				e.Row.Cells[1].ForeColor = Color.DodgerBlue;
			}


		}
	}
}