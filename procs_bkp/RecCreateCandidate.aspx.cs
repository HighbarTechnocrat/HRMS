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

public partial class procs_RecCreateCandidate : System.Web.UI.Page
{
	public SqlDataAdapter sqladp;
	public static string dob1;
	public string userid;
	public static string pimg = "";
	public static string cimg = "";
	public string loc = "", dept = "", subdept = "", desg = "", Wsch = "";
	public int did = 0;
	LeaveBalance bl = new LeaveBalance();
	SP_Methods spm = new SP_Methods();
	Recruitment_Candidate_Parameter Candidate = new Recruitment_Candidate_Parameter();
	HttpFileCollection fileCollection;
	public DataTable dtmainSkillSet, dtRecCandidate, dtEducation, dtCompanyInfo;
	public DataSet dsCandidateData, dsCVSource;
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
					getCVSource();
					GetInterviewer();
                    CVSourceOtherBind();
                    GetJobSites();
					GetVendorName();
					getEducationType();
					GetCandidateInfoRecruitmentwisedataBind();
					// BindEducationData();
					Txt_CandidateAge.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					//   Txt_CandidateMobile.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					Txt_CandidateExperence.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					Txt_CandidateBirthday.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					txtComEndDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					txtComStartDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					txtYearofPassing.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					Session.Remove("dtCompanyInfo");
					Session.Remove("dtEducation");

				}

			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
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

    private void GetCandidateInfoRecruitmentwisedataBind()
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
	protected void Txt_CandidateBirthday_TextChanged(object sender, EventArgs e)
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
			if (Convert.ToString(lstCVSource.SelectedValue).Trim() != "0")
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
                        lblmessage.Text = "Please select other";
                        return;
                    }
                    //if (Txt_ReferredBy.Text == "")
                    //{
                    //	lblmessage.Text = "Please enter other name";
                    //	return;
                    //}
                }
			}

			if (Convert.ToString(uploadfile.FileName).Trim() == "" && lnkuplodedfile.Text == "")
			{
				lblmessage.Text = "Please upload Resume";
				return;
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
				//if (Request.QueryString["reqCandidate_ID"] == null)
				//{
				//	strreqCandidate_ID = "";
				//}
				//else
				//{
				//	strreqCandidate_ID = Request.QueryString["reqCandidate_ID"];
				//}
				if (Convert.ToString(uploadfile.FileName).Trim() != "")
				{
					filename = uploadfile.FileName;
					string strfileName = "";
					string strremoveSpace = Txt_CandidateName.Text;
					strremoveSpace = Regex.Replace(strremoveSpace, @"\s", "");
					strfileName = Txt_CandidateName.Text.ToString().Trim() + Path.GetExtension(uploadfile.FileName);
					filename = strfileName;
					hdfilename.Value = strfileName;
				}
				if (Convert.ToString(uploadotherfile.FileName).Trim() != "")
				{
					fileCollection = Request.Files;
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
								string strremoveSpace = multiplefilename;
								strremoveSpace = Regex.Replace(strremoveSpace, @"\s", "");
								strfileName = strremoveSpace;
								multiplefilename = strfileName;
								// uploadfileName.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateResumeDocpath"]).Trim()), strfileName));
								multiplefilenameadd += strfileName + ",";
							}
						}
					}
				}
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
				Candidate.UploadResume = filename;
				Candidate.multiplefilenameadd = multiplefilenameadd;

				dtRecCandidate = spm.InsertRecCreateCandidate(Candidate);
				//dtRecCandidate = spm.InsertRecCreateCandidate(Convert.ToString(Session["Empcode"]).Trim(), strreqCandidate_ID, lstCandidategender.SelectedValue, lstMaritalStatus.SelectedValue, Txt_CandidateName.Text.Trim(), Convert.ToString(strtoDate), Txt_CandidateCurrentLocation.Text.Trim(), Txt_CandidateEmail.Text.Trim(), Txt_CandidateMobile.Text.Trim(), Txt_CandidatePAN.Text.Trim(), Txt_CandidateExperence.Text.Trim(), Txt_CandidateCurrentCTC.Text.Trim(), Txt_CandidateExpectedCTC.Text.Trim(), lstCVSource.SelectedValue, lstMainSkillset.SelectedValue, ReferredBy, vendorID, JobSitesID, Txt_ReferredBy.Text.Trim(), Txt_Comments.Text.Trim(), Txt_EducationQualifacation.Text.Trim(), Txt_AdditionalSkillset.Text.Trim(), Txt_Certifications.Text.Trim(), txtAdharNo.Text.Trim(), hdfilename.Value, multiplefilenameadd);
				//if (dtRecCandidate.Rows[0]["Result"].ToString() == "1")
				if (dtRecCandidate.Rows.Count > 0)
				{

					uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateResumeDocpath"]).Trim()), dtRecCandidate.Rows[0]["Result"].ToString() + "_" + hdfilename.Value.ToString().Trim()));

					if (Convert.ToString(uploadotherfile.FileName).Trim() != "")
					{
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
									string strremoveSpace = multiplefilename;
									strremoveSpace = Regex.Replace(strremoveSpace, @"\s", "");
									strfileName = strremoveSpace;
									uploadfileName.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateResumeDocpath"]).Trim()), dtRecCandidate.Rows[0]["Result"].ToString() + "_" + strfileName));
									//multiplefilenameadd += strfileName + ",";
									//multiplefilenameadd.TrimEnd(',');
								}
							}
						}
					}
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

	protected void lstCVSource_SelectedIndexChanged(object sender, EventArgs e)
	{

        //Txt_ReferredBy.Text = "";
        //Txt_ReferredBy.Enabled = false;
        DDlOther.SelectedValue = "Select other";
        DDlOther.Visible = false;

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
        //if (lstCVSource.SelectedValue == "4")
        //{
        //	// lbltext.Text = "Others";  
        //	Txt_ReferredBy.Enabled = true;
        //	spanIDreferredby.Visible = false;
        //	Txt_ReferredBy.Visible = true;
        //	lbltext.Visible = false;
        //}
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

    }

	protected void mobile_cancel_Click(object sender, EventArgs e)
	{
		Response.Redirect("~/procs/SearchCandidate.aspx");
	}

	protected void btnTra_Details_Click(object sender, EventArgs e)
	{
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
		

	}
	protected void trvl_accmo_btn_Click(object sender, EventArgs e)
	{
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
		


	}

	#endregion

	#region page Methods


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


	protected void trvldeatils_cancel_btn_Click(object sender, EventArgs e)
	{

		try
		{
			string[] strdate, strdate1;
			string StartDate = "", Startdatecheck = "";
			//string[] strdate1;
			//Txt_CandidateMobile.ReadOnly = true;
			string EndDate = "", Stype = "INSERT";
			#region Check All Parameters Selected

			lblCompany.Text = "";
			if (Convert.ToString(Txt_CandidateMobile.Text).Trim() != "")
			{
				int count = Txt_CandidateMobile.Text.Length;
				if (count < 10)
				{
					lblCompany.Text = "Please enter proper mobile no.";
					return;
				}
			}
			else
			{
				lblCompany.Text = "Please enter mobile no.";
				return;
			}

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
			if (Convert.ToString(txtComEndDate.Text).Trim() != "")
			{
				strdate1 = Convert.ToString(txtComEndDate.Text).Trim().Split('/');
				EndDate = Convert.ToString(strdate1[2]) + "-" + Convert.ToString(strdate1[1]) + "-" + Convert.ToString(strdate1[0]);
				//DateTime ddt = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
			}

			if (lnkbtn_expdtls.Text == "Modify")
			{
				dtCompanyInfo = spm.Update_Temp_Candid_Company_Detail("Update", Convert.ToInt32(hdnCandCompanyID.Value), txtCompanyName.Text.Trim(), txtComDesignation.Text.Trim(), StartDate, EndDate, Txt_CandidateMobile.Text.Trim(), Txt_CandidateEmail.Text.Trim(), 0, Convert.ToString(Session["Empcode"]).Trim());

				if (dtCompanyInfo.Rows.Count > 0)
				{
					GrdCompanyInfo.DataSource = dtCompanyInfo;
					GrdCompanyInfo.DataBind();
					CompanyClear();
					Session.Add("dtCompanyInfo", dtCompanyInfo);
					lnkbtn_expdtls.Text = "ADD";
				}
			}
			else
			{
				if (Convert.ToString(Session["dtCompanyInfo"]).Trim() != "" || Session["dtCompanyInfo"] != null)
				{
					DataTable dt = (DataTable)Session["dtCompanyInfo"];
					if (dt.Rows.Count > 0)
					{
						// string abc = "01-01-2014";              
						int numberOfRecords = dt.Select("StartDate ='" + Startdatecheck + "'").Length;
						if (numberOfRecords > 0)
						{
							lblCompany.Text = "Record already exists!.";
							return;
						}
					}
				}

				dtCompanyInfo = spm.Insert_Temp_Candid_Company_Detail(Stype, Convert.ToInt32(0), txtCompanyName.Text.Trim(), txtComDesignation.Text.Trim(), StartDate, EndDate, Txt_CandidateMobile.Text.Trim(), Txt_CandidateEmail.Text.Trim(), 0, Convert.ToString(Session["Empcode"]).Trim());

				if (dtCompanyInfo.Rows.Count > 0)
				{
					GrdCompanyInfo.DataSource = dtCompanyInfo;
					GrdCompanyInfo.DataBind();
					CompanyClear();
					Session.Add("dtCompanyInfo", dtCompanyInfo);
				}
				else
				{
					lblCompany.Text = "Mobile No or Email id already exists!.";
				}
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
		//DDLFullTime.SelectedIndex = -1;
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
		dtEducation = new DataTable();
		dtEducation.Columns.Add("EducationType", typeof(string));
		dtEducation.Columns.Add("CollegeName", typeof(string));
		dtEducation.Columns.Add("YearofPassing", typeof(string));
		dtEducation.Columns.Add("FinalScore", typeof(string));
		dtEducation.Columns.Add("CREATEDBY", typeof(string));
		dtEducation.Columns.Add("EducationTypeID", typeof(string));
		dtEducation.Columns.Add("Candidate_ID", typeof(string));

		Session.Add("dtEducation", dtEducation);

		dtCompanyInfo = new DataTable();
		dtCompanyInfo.Columns.Add("NameofCompany", typeof(string));
		dtCompanyInfo.Columns.Add("CandDesignation", typeof(string));
		dtCompanyInfo.Columns.Add("StartDate", typeof(string));
		dtCompanyInfo.Columns.Add("EndDate", typeof(string));
		dtCompanyInfo.Columns.Add("CREATEDBY", typeof(string));
		dtCompanyInfo.Columns.Add("Candidate_ID", typeof(string));

		Session.Add("dtCompanyInfo", dtCompanyInfo);
	}

	protected void trvldeatils_delete_btn_Click(object sender, EventArgs e)
	{
		try
		{
			bool flag = false;
			#region Check All Parameters Selected
				string Stype = "INSERT";
				lblEducation.Text = "";
				// Txt_CandidateMobile.ReadOnly = true;
				int CandEducationID = 0;
				decimal FinalScore = 0;
				if (Convert.ToString(Txt_CandidateMobile.Text).Trim() != "")
				{
					int count = Txt_CandidateMobile.Text.Length;
					if (count < 10)
					{
						lblEducation.Text = "Please enter proper Mobile No.";
						return;
					}
				}
				else
				{
					lblEducation.Text = "Please enter Mobile No.";
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

			if (trvl_localbtn.Text == "Modify")
			{
				CandEducationID = Convert.ToString(hdnCandEducationID.Value).Trim() != "" ? Convert.ToInt32(hdnCandEducationID.Value) : 0;
				//FinalScore = Convert.ToString(txtFinalScore.Text.Trim()) != "" ? Convert.ToDecimal(txtFinalScore.Text.Trim()) : 0;
				dtEducation = spm.Update_Temp_Candid_Education_Detail("Update", CandEducationID, Convert.ToInt32(lstEducationType.SelectedValue), txtCollegeName.Text.Trim(), txtYearofPassing.Text.Trim(), Convert.ToString(txtFinalScore.Text.Trim()), DDLFullTime.SelectedValue, txtUniversity.Text.Trim(), txtDiscipline.Text, Txt_CandidateMobile.Text.Trim(), Txt_CandidateEmail.Text.Trim(), 0, Convert.ToString(Session["Empcode"]).Trim());
				if (dtEducation.Rows.Count > 0)
				{
					DgvEducationDetails.DataSource = dtEducation;
					DgvEducationDetails.DataBind();
					EducationClear();
					Session.Add("dtEducation", dtEducation);
					trvl_localbtn.Text ="ADD";
				}
			}
			else
			{

				if (Convert.ToInt32(lstEducationType.SelectedValue) == 1 || Convert.ToInt32(lstEducationType.SelectedValue) == 2)
				{
					if (Convert.ToString(Session["dtEducation"]).Trim() != "" || Session["dtEducation"] != null)
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
                            else
                            {
								flag = true;
							}
						}
					}
				}

				//if (flag == true)
				//{
					CandEducationID = Convert.ToString(hdnCandEducationID.Value).Trim() != "" ? Convert.ToInt32(hdnCandEducationID.Value) : 0;
					//FinalScore = Convert.ToString(txtFinalScore.Text.Trim()) != "" ? Convert.ToDecimal(txtFinalScore.Text.Trim()) : 0;
					dtEducation = spm.Insert_Temp_Candid_Education_Detail(Stype, CandEducationID, Convert.ToInt32(lstEducationType.SelectedValue), txtCollegeName.Text.Trim(), txtYearofPassing.Text.Trim(), Convert.ToString(txtFinalScore.Text.Trim()), DDLFullTime.SelectedValue, txtUniversity.Text.Trim(), txtDiscipline.Text, Txt_CandidateMobile.Text.Trim(), Txt_CandidateEmail.Text.Trim(), 0, Convert.ToString(Session["Empcode"]).Trim());
					if (dtEducation.Rows.Count > 0)
					{
						DgvEducationDetails.DataSource = dtEducation;
						DgvEducationDetails.DataBind();
						EducationClear();
						Session.Add("dtEducation", dtEducation);
					}
					else
					{
						lblEducation.Text = "Mobile No or Email id already exists!.";
					}
				//}
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
		DTEdit = spm.Insert_Temp_Candid_Education_Detail("Delete", Convert.ToInt32(CandEducationID), 0, "", "", "","","","", Txt_CandidateMobile.Text.Trim(), "", 0, Convert.ToString(Session["Empcode"]).Trim());
		if (DTEdit.Rows.Count > 0)
		{
			DgvEducationDetails.DataSource = DTEdit;
			DgvEducationDetails.DataBind();
			Session.Add("dtEducation", DTEdit);
			//lstEducationType.SelectedValue = Convert.ToString(DTEdit.Rows[0]["EducationTypeID"]).Trim();
			//txtCollegeName.Text = Convert.ToString(DTEdit.Rows[0]["CollegeName"]).Trim();
			//txtYearofPassing.Text = Convert.ToString(DTEdit.Rows[0]["YearofPassing"]).Trim();
			//txtFinalScore.Text = Convert.ToString(DTEdit.Rows[0]["FinalScore"]).Trim();
			// hdnCandEducationID.Value= Convert.ToString(DTEdit.Rows[0]["CandEducationID"]).Trim();
			//trvl_localbtn.Text = "Modify";
		}
        else
        {
			DgvEducationDetails.DataSource = null;
			DgvEducationDetails.DataBind();
		}
	}
	protected void btn_Edit_Click(object sender, ImageClickEventArgs e)
	{
		DataTable DTEdit = new DataTable();
		string CandEducationID = "0";
		ImageButton btn = (ImageButton)sender;
		GridViewRow row = (GridViewRow)btn.NamingContainer;
		CandEducationID = Convert.ToString(DgvEducationDetails.DataKeys[row.RowIndex].Values[0]).Trim();
		hdnCandEducationID.Value = CandEducationID;
		DTEdit = spm.Get_Education_Detail_ByCandEducationID("Select", Convert.ToInt32(CandEducationID), Txt_CandidateMobile.Text.Trim());
		if (DTEdit.Rows.Count > 0)
		{
            lstEducationType.SelectedValue = Convert.ToString(DTEdit.Rows[0]["EducationTypeID"]).Trim(); 
			DDLFullTime.SelectedItem.Text= Convert.ToString(DTEdit.Rows[0]["PGType"]).Trim();
			DDLFullTime.SelectedValue= Convert.ToString(DTEdit.Rows[0]["PGType"]).Trim();
			txtCollegeName.Text = Convert.ToString(DTEdit.Rows[0]["CollegeName"]).Trim();
			txtUniversity.Text= Convert.ToString(DTEdit.Rows[0]["PGUniversityName"]).Trim();
			txtYearofPassing.Text = Convert.ToString(DTEdit.Rows[0]["YearofPassing"]).Trim();
            txtFinalScore.Text = Convert.ToString(DTEdit.Rows[0]["FinalScore"]).Trim();
			txtDiscipline.Text= Convert.ToString(DTEdit.Rows[0]["PGDiscipline"]).Trim();
			hdnCandEducationID.Value = Convert.ToString(DTEdit.Rows[0]["CandEducationID"]).Trim();
			if(Convert.ToString(DTEdit.Rows[0]["EducationTypeID"]).Trim()=="5")
            {
				DDLFullTime.Enabled = true;
			}
            else { DDLFullTime.Enabled = false; }
            trvl_localbtn.Text = "Modify";
        }
	}


	protected void btn_Comp_Click(object sender, ImageClickEventArgs e)
	{
		DataTable DTEdit = new DataTable();
		string CandCompanyID = "0";
		ImageButton btn = (ImageButton)sender;
		GridViewRow row = (GridViewRow)btn.NamingContainer;
		CandCompanyID = Convert.ToString(GrdCompanyInfo.DataKeys[row.RowIndex].Values[0]).Trim();
		hdnCandCompanyID.Value = CandCompanyID;
		// DTEdit = spm.Insert_Temp_Candid_Education_Detail("Delete", Convert.ToInt32(CandCompanyID), 0, "", "", Convert.ToDecimal(0), Txt_CandidateMobile.Text.Trim(), Convert.ToString(Session["Empcode"]).Trim());
		DTEdit = spm.Insert_Temp_Candid_Company_Detail("Delete", Convert.ToInt32(CandCompanyID), "", "", "", "", Txt_CandidateMobile.Text.Trim(), "", 0, Convert.ToString(Session["Empcode"]).Trim());

		if (DTEdit.Rows.Count > 0)
		{
			GrdCompanyInfo.DataSource = DTEdit;
			GrdCompanyInfo.DataBind();
			Session.Add("dtCompanyInfo", DTEdit);
		}
	}
	
    protected void btn_CompEdit_Click(object sender, ImageClickEventArgs e)
	{
		DataTable DTEdit = new DataTable();
		string CandCompanyID = "0";
		ImageButton btn = (ImageButton)sender;
		GridViewRow row = (GridViewRow)btn.NamingContainer;
		CandCompanyID = Convert.ToString(GrdCompanyInfo.DataKeys[row.RowIndex].Values[0]).Trim();
		hdnCandCompanyID.Value = CandCompanyID;
		DTEdit = spm.Get_Company_Detail_ByCandCompanyID("Select", Convert.ToInt32(CandCompanyID), Txt_CandidateMobile.Text.Trim());
		if (DTEdit.Rows.Count > 0)
		{
			txtCompanyName.Text = Convert.ToString(DTEdit.Rows[0]["NameofCompany"]).Trim();
			txtComDesignation.Text = Convert.ToString(DTEdit.Rows[0]["CandDesignation"]).Trim();
			txtComStartDate.Text = Convert.ToString(DTEdit.Rows[0]["StartDate"]).Trim();
			txtComEndDate.Text = Convert.ToString(DTEdit.Rows[0]["EndDate"]).Trim();
			hdnCandCompanyID.Value = Convert.ToString(DTEdit.Rows[0]["CandCompanyID"]).Trim();
			lnkbtn_expdtls.Text = "Modify";
		}
	}
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

	protected void txtComStartDate_TextChanged(object sender, EventArgs e)
	{
		lblCompany.Text = "";
		if (txtComStartDate.Text.Trim() != "" && txtComEndDate.Text.Trim() != "")
		{
			if (Date_Validation() == false)
			{
				lblCompany.Text = "Start date cannot be greater than End Date";
				return;
			}
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
		if (lstEducationType.SelectedValue == "5")
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
			DDLFullTime.SelectedValue = "Full Time";
			if (lstEducationType.SelectedValue == "0")
            {
				lblEducation.Text = "";
				trvl_localbtn.Text = "ADD";
				//DDLFullTime.SelectedIndex = -1;
				txtCollegeName.Text = "";
				txtYearofPassing.Text = "";
				txtFinalScore.Text = "";
				txtUniversity.Text = "";
				txtDiscipline.Text = "";
			}
		}
	}

	protected void Txt_CandidateMobile_TextChanged(object sender, EventArgs e)
	{
		lblmessage.Text = "";
	    DataTable Dt = spm.IsMobileExists(Txt_CandidateMobile.Text.ToString().Trim());
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
		string email = Txt_CandidateEmail.Text;
		Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
		Match match = regex.Match(email);
		if (!match.Success)
		{
			lblmessage.Text = email + " is Invalid Email Address";
			Txt_CandidateEmail.Text = "";
			return;
		}
		DataTable Dt = spm.IsEmailExists(Txt_CandidateEmail.Text.ToString().Trim());
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
					string strremoveSpace = Txt_CandidateName.Text;
					strremoveSpace = Regex.Replace(strremoveSpace, @"\s", "");
					strfileName = Txt_CandidateName.Text.ToString().Trim() + Path.GetExtension(uploadfile.FileName);
					filename = strfileName;
					hdfilename.Value = strfileName;
				}
				if (Convert.ToString(uploadotherfile.FileName).Trim() != "")
				{
					fileCollection = Request.Files;
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
								string strremoveSpace = multiplefilename;
								strremoveSpace = Regex.Replace(strremoveSpace, @"\s", "");
								strfileName = strremoveSpace;
								multiplefilename = strfileName;
								// uploadfileName.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateResumeDocpath"]).Trim()), strfileName));
								multiplefilenameadd += strfileName + ",";
							}
						}
					}
				}
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
				Candidate.UploadResume = filename;
				Candidate.multiplefilenameadd = multiplefilenameadd;
				dtRecCandidate = spm.Insert_SaveasDraft_Candidate(Candidate);
				if (dtRecCandidate.Rows.Count > 0)
				{

					uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateResumeDocpath"]).Trim()), dtRecCandidate.Rows[0]["Result"].ToString() + "_" + hdfilename.Value.ToString().Trim()));

					if (Convert.ToString(uploadotherfile.FileName).Trim() != "")
					{
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
									string strremoveSpace = multiplefilename;
									strremoveSpace = Regex.Replace(strremoveSpace, @"\s", "");
									strfileName = strremoveSpace;
									uploadfileName.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateResumeDocpath"]).Trim()), dtRecCandidate.Rows[0]["Result"].ToString() + "_" + strfileName));
									//multiplefilenameadd += strfileName + ",";
									//multiplefilenameadd.TrimEnd(',');
								}
							}
						}
					}
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
}