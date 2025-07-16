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

public partial class procs_Ref_Candidate_View : System.Web.UI.Page
{
	public SqlDataAdapter sqladp;
	SP_Methods spm = new SP_Methods();
	HttpFileCollection fileCollection;
	public DataTable dtmainSkillSet, dtRecCandidate, dtSPOC;
	public DataSet dsCandidateData, dsCVSource;
	public int Leaveid, apprid;
	public string filename = "", approveremailaddress, message, strreqCandidate_ID;

	public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
			}

			if (Convert.ToString(Session["Empcode"]).Trim() == "")
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
			if (Page.User.Identity.IsAuthenticated == false)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
			}
			else
			{
				Page.SmartNavigation = true;
				if (!Page.IsPostBack)
				{
					hdfilefath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Ref_CandidateResume"]).Trim());
					getMainSkillset();

					Txt_CandidateExperence.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					TxtRelevantExpYrs.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					//Txt_CandidateBirthday.Attributes.Add("onkeypress", "return noanyCharecters(event);");

					if (Request.QueryString.Count > 0)
					{
						hdCandidate_ID.Value = Convert.ToString(Request.QueryString[0]).Trim();						
						GetRefCandidated();
					}
				}

			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}

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
				hdnEmpcode.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["EmpCode"]).Trim();
				hdnCreatedDate.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["CREATEDDate"]).Trim();
				txtApporvalcmd.Text= Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Approval_Remark"]).Trim();

				Txt_CandidateName.Enabled = false;
				Txt_CandidateEmail.Enabled = false;
				Txt_CandidateMobile.Enabled = false;
				lstCandidategender.Enabled = false;
				lstMainSkillset.Enabled = false;
				Txt_CandidateExperence.Enabled = false;
				TxtRelevantExpYrs.Enabled = false;
				Txt_Comments.Enabled = false;
				Txt_AdditionalSkillset.Enabled = false;
				if (dsRecruitmentDetails.Tables[1].Rows.Count > 0)
				{
					DgvApprover.DataSource = dsRecruitmentDetails.Tables[1];
					DgvApprover.DataBind();
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}
	#endregion
	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		try
		{
			
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
			//throw;
		}
	}



	protected void btnReject_Click(object sender, EventArgs e)
	{
		try
		{
			
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
			//throw;
		}

	}

}