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

public partial class procs_Ref_CandidateInfo : System.Web.UI.Page
{
	public SqlDataAdapter sqladp;

	public string loc = "", dept = "", subdept = "", desg = "", Wsch = "";
	SP_Methods spm = new SP_Methods();
	HttpFileCollection fileCollection;
	public DataTable dtmainSkillSet, dtRecCandidate, dtSPOC;
	public DataSet dsCandidateData, dsCVSource;
	public int Leaveid, apprid;
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

					Txt_CandidateExperence.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					TxtRelevantExpYrs.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtNoticePeriod.Attributes.Add("onkeypress", "return onOnlyNumber(event);");
                    //Txt_CandidateBirthday.Attributes.Add("onkeypress", "return noanyCharecters(event);");

                    if (Request.QueryString.Count > 0)
					{
						hdCandidate_ID.Value = Convert.ToString(Request.QueryString[0]).Trim();
						GetRefCandidated();
						lnkback.Visible = true;
					}
				}

			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}



	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		try
		{
            Int32 inoticePeriod = 0;
            int Candidate_ID = 0;
			string EmployeeName = "", JobSitesID = "", AssigneeMailID = "", AssigneName = "", AssigneTo = "";
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
            /*
			if (Convert.ToString(lstMaritalStatus.SelectedValue).Trim() == "" || Convert.ToString(lstMaritalStatus.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Marital Status";
				return;
			}
			if (Convert.ToString(lstCandidategender.SelectedValue).Trim() == "" || Convert.ToString(lstCandidategender.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Gender";
				return;
			}*/

			if (Convert.ToString(lstMainSkillset.SelectedValue).Trim() == "0" || Convert.ToString(lstMainSkillset.SelectedValue).Trim() == "")
			{
				lblmessage.Text = "Please select Main Skillset";
				return;
			}

            /*if (Convert.ToString(Txt_CandidateExperence.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter Total Experience In(Year)";
				return;
			}
            if (Convert.ToString(txtNoticePeriod.Text).Trim() == "" )
            {
                lblmessage.Text = "Please enter Notice Period (Days)";
                return;
            }

            if (Convert.ToString(txtNoticePeriod.Text).Trim() != "")
            {
                inoticePeriod = Convert.ToInt32(txtNoticePeriod.Text);

                if (inoticePeriod == 0)
                {
                    lblmessage.Text = "Please enter Notice Period (Days)";
                    return;
                }
            }


            if (Convert.ToString(TxtRelevantExpYrs.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter Relevant Exp In(Year)";
				return;
			}
			if (Convert.ToString(Txt_CandidateExperence.Text).Trim() != "" && Convert.ToString(TxtRelevantExpYrs.Text).Trim() != "")
			{
				if (Convert.ToDecimal(Txt_CandidateExperence.Text) < Convert.ToDecimal(TxtRelevantExpYrs.Text))
				{
					lblmessage.Text = "Please Total Experience In(Year) is Greater than Relevant Exp Yrs";
					return;
				}
			}
			if (Convert.ToString(Txt_AdditionalSkillset.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter Additional Skillset";
				return;
			}  */

            if (Convert.ToString(uploadfile.FileName).Trim() == "" && lnkuplodedfile.Text == "")
			{
				lblmessage.Text = "Please upload Resume";
				return;
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
				string strtoDate = "", CreatedDate = "";
				string strfileName = "";
				Candidate_ID = Convert.ToString(hdCandidate_ID.Value).Trim() != "" ? Convert.ToInt32(hdCandidate_ID.Value) : 0;
				if (Convert.ToString(uploadfile.FileName).Trim() != "")
				{
					filename = uploadfile.FileName;

					string Dates = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
					string strremoveSpace = Txt_CandidateName.Text;
					strremoveSpace = Regex.Replace(strremoveSpace, @"[^0-9a-zA-Z\._]", "_");
					strfileName = Txt_CandidateName.Text.ToString().Trim() + "_" + Dates + Path.GetExtension(uploadfile.FileName);
					filename = strfileName;
					hdfilename.Value = strfileName;
				}
                
				if (Candidate_ID != 0 && strfileName != "")
				{
					hdfilename.Value = Candidate_ID + "_" + hdfilename.Value;
				}

				//dtSPOC = spm.GetRef_Candidated_SPOCData();
				//if (dtSPOC.Rows.Count > 0)
				//{
				//	AssigneTo = Convert.ToString(dtSPOC.Rows[0]["SPOC_ID"]);
				//	AssigneName = Convert.ToString(dtSPOC.Rows[0]["Emp_Name"]);
				//	AssigneeMailID = Convert.ToString(dtSPOC.Rows[0]["Emp_Emailaddress"]);
				//}
				AssigneeMailID = RecruiterCCmail();
				AssigneName = "Recruiter Team";
				if (dtSPOC.Rows.Count > 0)
				{					
					//Dear Colleagues,
					//AssigneTo = Convert.ToString(dtSPOC.Rows[0]["SPOC_ID"]);
					//AssigneName = Convert.ToString(dtSPOC.Rows[0]["Emp_Name"]);
					//AssigneeMailID = Convert.ToString(dtSPOC.Rows[0]["Emp_Emailaddress"]);
				}
				EmployeeName = Session["emp_loginName"].ToString();
				CreatedDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

                inoticePeriod = 0; 
                if(Convert.ToString(txtNoticePeriod.Text).Trim()!="")
                {
                    inoticePeriod = Convert.ToInt32(txtNoticePeriod.Text);
                }
				dtRecCandidate = spm.Insert_Referral_Candidate(Convert.ToString(Session["Empcode"]).Trim(), Candidate_ID, AssigneTo, Convert.ToString(lstCandidategender.SelectedValue).Trim(), Convert.ToString(lstMaritalStatus.SelectedValue), Convert.ToString(Txt_CandidateName.Text).Trim(), Convert.ToString(strtoDate),Convert.ToString(Txt_CandidateEmail.Text).Trim(), Convert.ToString(Txt_CandidateMobile.Text).Trim(),Convert.ToString(TxtRelevantExpYrs.Text).Trim(), Convert.ToString(Txt_CandidateExperence.Text).Trim(), Convert.ToInt32(lstMainSkillset.SelectedValue), 1,Convert.ToString(Txt_Comments.Text).Trim(), Convert.ToString(Txt_AdditionalSkillset.Text).Trim(), hdfilename.Value, inoticePeriod);
				string strLeaveRstURL = "";
				string Gender = "", Maritalstatus = "";
				if (dtRecCandidate.Rows.Count > 0)
				{
					string FileSaved = "";
					if (Candidate_ID == 0)
					{
						FileSaved = (dtRecCandidate.Rows[0]["Result"].ToString() + "_" + hdfilename.Value.ToString().Trim());
					}
					else
					{
						FileSaved = hdfilename.Value.ToString().Trim();
					}
					uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateResumeDocpath"]).Trim()), FileSaved));
                    //strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["Link_CandidateApporvalReferral"]).Trim() + "?Ref_Candidated_ID=" + Convert.ToString(dtRecCandidate.Rows[0]["Result"].ToString());

                    //Gender = Convert.ToString(lstCandidategender.SelectedValue).Trim() == "1" ? Convert.ToString("Male") : "Female";
                    //Maritalstatus = Convert.ToString(lstMaritalStatus.SelectedValue).Trim() == "1" ? Convert.ToString("Married") : "UnMarried";
                    if (Convert.ToString(lstCandidategender.SelectedValue).Trim() != "")
                        Gender = Convert.ToString(lstCandidategender.SelectedValue).Trim();

                    if (Convert.ToString(lstMaritalStatus.SelectedValue).Trim() != "")
                        Maritalstatus = Convert.ToString(lstMaritalStatus.SelectedValue).Trim();

                    spm.Ref_Candidated_send_mailto_SPOC(Txt_CandidateName.Text.Trim(), AssigneeMailID, EmployeeName, AssigneName, CreatedDate, Txt_CandidateEmail.Text.Trim(), "", Txt_CandidateMobile.Text.Trim(), Gender, Maritalstatus, lstMainSkillset.SelectedItem.Text, Txt_CandidateExperence.Text.Trim(), TxtRelevantExpYrs.Text.Trim(), Txt_AdditionalSkillset.Text.Trim(), Txt_Comments.Text.Trim(), strLeaveRstURL,inoticePeriod);
					Response.Redirect("~/procs/MyRef_Candidate_Index.aspx");
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

	private string RecruiterCCmail()
	{
		try
		{
			dtSPOC = spm.GetRef_CandidatedList("", "RecruiterTeam");
			String sbapp = "";
			if (dtSPOC.Rows.Count > 0)
			{
				for (int i = 0; i < dtSPOC.Rows.Count; i++)
				{
					if (Convert.ToString(sbapp).Trim() == "")
						sbapp = Convert.ToString(dtSPOC.Rows[0]["Emp_Emailaddress"]).Trim();
					else
						sbapp = sbapp + ";" + Convert.ToString(dtSPOC.Rows[i]["Emp_Emailaddress"]).Trim();
				}
			}
			return Convert.ToString(sbapp);
		}
		catch (Exception)
		{
			throw;
		}
	}


	protected void mobile_cancel_Click(object sender, EventArgs e)
	{
		//Response.Redirect("~/procs/SearchCandidate.aspx");
	}




	#endregion

	#region page Methods
	private void getMainSkillset()
	{
		try
		{
			dtmainSkillSet = spm.GetRef_CandidatedList("", "SelectSkillSet");
			lstMainSkillset.DataSource = dtmainSkillSet;
			lstMainSkillset.DataTextField = "ModuleDesc";
			lstMainSkillset.DataValueField = "ModuleId";
			lstMainSkillset.DataBind();
			lstMainSkillset.Items.Insert(0, new ListItem("Select SkillSet", ""));
		}
		catch (Exception)
		{

		}
	}
	private void GetRefCandidated()
	{
		DataSet dsRecruitmentDetails = new DataSet();
		try
		{
			SqlParameter[] spars = new SqlParameter[3];
			spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
			spars[0].Value = "RefEdit";
			spars[1] = new SqlParameter("@Ref_Candidate_ID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdCandidate_ID.Value);
			spars[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[2].Value = Session["Empcode"].ToString();
			dsRecruitmentDetails = spm.getDatasetList(spars, "sp_Ref_CreateCandidate");

			if (dsRecruitmentDetails.Tables[0].Rows.Count > 0)
			{
				Txt_CandidateName.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Ref_CandidateName"]).Trim();
				Txt_CandidateEmail.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Ref_CandidateEmail"]).Trim();
				Txt_CandidateMobile.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Ref_CandidateMobile"]).Trim();
				lstCandidategender.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Ref_CandidateGender"]).Trim();
				lstMaritalStatus.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Maritalstatus"]).Trim();
				lstMainSkillset.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ModuleId"]).Trim();
				Txt_CandidateExperence.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Ref_CandidateTotalExperience"]).Trim();
				TxtRelevantExpYrs.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Ref_CandidateRelevantExperience"]).Trim();
				Txt_Comments.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Comments"]).Trim();
				lnkuplodedfile.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Ref_UploadResume"]).Trim();
				hdfilename.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Ref_UploadResume"]).Trim();
				Txt_AdditionalSkillset.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["AdditionalSkillset"]).Trim();
				hdnStatusID.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["StatusID"]).Trim();
                txtNoticePeriod.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Ref_CandidateNoticePeriod"]).Trim();

                


                if (dsRecruitmentDetails.Tables[1].Rows.Count > 0)
				{
					DgvApprover.DataSource = dsRecruitmentDetails.Tables[1];
					DgvApprover.DataBind();
					GRDReferral.Visible = true;
				}
				if (hdnStatusID.Value != "3")
				{
					mobile_btnSave.Text = "Update";
					mobile_btnSave.ToolTip = "Update";
					mobile_btnSave.Visible = false;
					Txt_CandidateName.Enabled = false;
					Txt_CandidateEmail.Enabled = false;
					Txt_CandidateMobile.Enabled = false;
					lstCandidategender.Enabled = false;
					lstMaritalStatus.Enabled = false;
					lstMainSkillset.Enabled = false;
					Txt_CandidateExperence.Enabled = false;
					TxtRelevantExpYrs.Enabled = false;
					Txt_Comments.Enabled = false;
					Txt_AdditionalSkillset.Enabled = false;
                    txtNoticePeriod.Enabled = false;
                }
			}
			else
			{
				Response.Redirect("~/procs/Ref_Employee_Index.aspx", false);
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}
	#endregion
}