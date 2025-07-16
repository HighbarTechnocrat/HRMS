using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_Rec_InterviewerInbox : System.Web.UI.Page
{
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public int did = 0;
    SP_Methods spm = new SP_Methods();
    DataSet dsInterviewerInox;
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

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
            // lpm.Emp_Code = Session["Empcode"].ToString();
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
                    GetSkillsetName();
                    GetCompany_Location();
                    GetDepartmentMaster();
                    GetlstPositionBand();
                    GetCandidateName();
                    getMngInterviewerInoxList();
                    if (hdLinkType.Value == "InShPInter" || hdLinkType.Value== "InVRInter")
                    {
                        isIntrviewShow.Visible = true;
                    }
                    else
                    {
                        isIntrviewShow.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    public void GetSkillsetName()
    {
        DataTable dtSkillset = new DataTable();
        dtSkillset = spm.GetRecruitment_SkillsetName();
        if (dtSkillset.Rows.Count > 0)
        {
            lstSkillSet.DataSource = dtSkillset;
            lstSkillSet.DataTextField = "ModuleDesc";
            lstSkillSet.DataValueField = "ModuleId";
            lstSkillSet.DataBind();
            lstSkillSet.Items.Insert(0, new ListItem("Select Skillset", "0"));

        }
    }
    public void GetCompany_Location()
    {
        DataTable lstPosition = new DataTable();
        lstPosition = spm.GetRecruitment_Req_company_Location();
        if (lstPosition.Rows.Count > 0)
        {
            LstLocation.DataSource = lstPosition;
            LstLocation.DataTextField = "Location_name";
            LstLocation.DataValueField = "comp_code";
            LstLocation.DataBind();
            LstLocation.Items.Insert(0, new ListItem("Select Location", "0"));

        }
    }
    public void GetDepartmentMaster()
    {
        DataTable dtPositionDept = new DataTable();
        dtPositionDept = spm.GetRecruitment_Req_DepartmentMaster();
        if (dtPositionDept.Rows.Count > 0)
        {
            LstDepartment.DataSource = dtPositionDept;
            LstDepartment.DataTextField = "Department_Name";
            LstDepartment.DataValueField = "Department_id";
            LstDepartment.DataBind();
            LstDepartment.Items.Insert(0, new ListItem("Select Department", "0"));

        }
    }
    public void GetlstPositionBand()
    {
        DataTable dtPositionBand = new DataTable();
        dtPositionBand = spm.GetRecruitment_Req_HRMS_BAND_MASTER();
        if (dtPositionBand.Rows.Count > 0)
        {
            Lstband.DataSource = dtPositionBand;
            Lstband.DataTextField = "BAND";
            Lstband.DataValueField = "BAND";
            Lstband.DataBind();
            Lstband.Items.Insert(0, new ListItem("Select BAND", "0"));
        }
    }
    public void GetCandidateName()
    {
        DataTable dtSkillset = new DataTable();
        dtSkillset = spm.GetRecruitment_CandidateName(Convert.ToString(Session["Empcode"]).Trim());
        if (dtSkillset.Rows.Count > 0)
        {
            ddlCandidateId.DataSource = dtSkillset;
            ddlCandidateId.DataTextField = "CandidateName";
            ddlCandidateId.DataValueField = "Candidate_ID";
            ddlCandidateId.DataBind();
            ddlCandidateId.Items.Insert(0, new ListItem("Select Candidate ", "0"));

        }
    }
    private void getMngInterviewerInoxList()
    {
        try
        {
            if (Request.QueryString.Count > 0)
            {
                hdLinkType.Value = Convert.ToString(Request.QueryString[0]).Trim();
            }
            if (hdLinkType.Value == "InPInter")
            {
                lblheading.Text = "Screening - Inbox";
            }
            if (hdLinkType.Value == "InVRInter")
            {
                lblheading.Text = "Recruitment Interviewer List";               
            }
			if (hdLinkType.Value == "InScreeningList")
			{
				lblheading.Text = " Recruitment Screening List";
			}
			if (hdLinkType.Value == "InShPInter")
            {
                lblheading.Text = "Interviewer List- Inbox";
            }
            

           dsInterviewerInox = spm.getInterviewerInoxList(Convert.ToString(Session["Empcode"]).Trim(), hdLinkType.Value);
            if (dsInterviewerInox.Tables[0].Rows.Count > 0)
            {
                if (hdLinkType.Value == "InShPInter")
                {
                    GVInterviewfeedback.DataSource = dsInterviewerInox.Tables[1];
                    GVInterviewfeedback.DataBind();
                    if (dsInterviewerInox.Tables[1].Rows.Count > 0)
                    {
                        lstRequisitionNo.DataSource = dsInterviewerInox.Tables[1];
                        lstRequisitionNo.DataTextField = "RequisitionNumber";
                        lstRequisitionNo.DataValueField = "Recruitment_ReqID";
                        lstRequisitionNo.DataBind();
                        lstRequisitionNo.Items.Insert(0, new ListItem("Select Requisition No", "0"));
                    }

                }
                else
                {
                    gvInterviewerInoxList.DataSource = dsInterviewerInox.Tables[0];
                    gvInterviewerInoxList.DataBind();
                    if (dsInterviewerInox.Tables[0].Rows.Count > 0)
                    {
                        lstRequisitionNo.DataSource = dsInterviewerInox.Tables[0];
                        lstRequisitionNo.DataTextField = "RequisitionNumber";
                        lstRequisitionNo.DataValueField = "Recruitment_ReqID";
                        lstRequisitionNo.DataBind();
                        lstRequisitionNo.Items.Insert(0, new ListItem("Select Requisition No", "0"));
                    }
                    GVInterviewfeedback.DataSource = null;
                    GVInterviewfeedback.DataBind();
                }
                
            }
            else
            {
                if (dsInterviewerInox.Tables[1].Rows.Count > 0)
                {
                    GVInterviewfeedback.DataSource = dsInterviewerInox.Tables[1];
                    GVInterviewfeedback.DataBind();
                    if (dsInterviewerInox.Tables[1].Rows.Count > 0)
                    {
                        lstRequisitionNo.DataSource = dsInterviewerInox.Tables[1];
                        lstRequisitionNo.DataTextField = "RequisitionNumber";
                        lstRequisitionNo.DataValueField = "Recruitment_ReqID";
                        lstRequisitionNo.DataBind();
                        lstRequisitionNo.Items.Insert(0, new ListItem("Select Requisition No", "0"));
                    }

                }
                gvInterviewerInoxList.DataSource = null;
                gvInterviewerInoxList.DataBind();
            }


        }
        catch (Exception ex)
        {

        }
    }

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        //Response.Redirect("InterviewerShortList.aspx?Recruitment_ReqID=" + HFRecruitment_ReqID.Value);
         HFRecruitment_ReqID.Value = Convert.ToString(gvInterviewerInoxList.DataKeys[row.RowIndex].Values[0]).Trim();
        // HFCandidateID.Value = Convert.ToString(gvInterviewerInoxList.DataKeys[row.RowIndex].Values[1]).Trim();
       
        if (hdLinkType.Value == "InVRInter")
        {
			Session.Add("InterviewList", "InVRInter");
			Response.Redirect("~/procs/RecruiterView.aspx?Recruitment_ReqID=" + HFRecruitment_ReqID.Value);
        }
		if (hdLinkType.Value == "InScreeningList")
		{
			Session.Add("InterviewList", "InScreeningList");
			Response.Redirect("~/procs/RecruiterView.aspx?Recruitment_ReqID=" + HFRecruitment_ReqID.Value);
		}
		if (hdLinkType.Value == "InPInter")
        {
            Response.Redirect("~/procs/InterviewerShortList.aspx?ReqID=" + HFRecruitment_ReqID.Value);
        }
       
    }

    protected void lnkEditFeedBack_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
       
         HFRecruitment_ReqID.Value = Convert.ToString(GVInterviewfeedback.DataKeys[row.RowIndex].Values[0]).Trim();
         HFCandidateID.Value = Convert.ToString(GVInterviewfeedback.DataKeys[row.RowIndex].Values[1]).Trim();
         HFCanRID.Value = Convert.ToString(GVInterviewfeedback.DataKeys[row.RowIndex].Values[2]).Trim();

        if (hdLinkType.Value == "InShPInter")
        {
            Response.Redirect("~/procs/InterviewerFeedbac.aspx?ReqID=" + HFRecruitment_ReqID.Value + "&CanID=" + HFCandidateID.Value + "&CanSRID=" + HFCanRID.Value);
        }
       
    }

    protected void gvInterviewerInoxList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInterviewerInoxList.PageIndex = e.NewPageIndex;
        if (Lstband.SelectedValue == "0" && LstDepartment.SelectedValue == "0" && LstLocation.SelectedValue == "0" && lstRequisitionNo.SelectedValue == "0" && lstSkillSet.SelectedValue == "0")
        {
            getMngInterviewerInoxList();
        }
        else
        {
            Searchmethod();
        }
    }

    protected void GVInterviewfeedback_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVInterviewfeedback.PageIndex = e.NewPageIndex;
        
        if (Lstband.SelectedValue == "0" && LstDepartment.SelectedValue == "0" && LstLocation.SelectedValue == "0" && lstRequisitionNo.SelectedValue == "0" && lstSkillSet.SelectedValue == "0")
        {
            getMngInterviewerInoxList();
        }
        else
        {
            Searchmethod();
        }
    }

    private void Searchmethod()
    {
        try
        {
            lblmessagesearch.Text = "";
            int SkillSetID = 0, Recruitment_ReqID = 0, DeptID = 0;
            int candidateId = 0;
            SkillSetID = Convert.ToString(lstSkillSet.SelectedValue).Trim() != "" ? Convert.ToInt32(lstSkillSet.SelectedValue) : 0;
            Recruitment_ReqID = Convert.ToString(lstRequisitionNo.SelectedValue).Trim() != "" ? Convert.ToInt32(lstRequisitionNo.SelectedValue) : 0;
            DeptID = Convert.ToString(LstDepartment.SelectedValue).Trim() != "" ? Convert.ToInt32(LstDepartment.SelectedValue) : 0;
            candidateId = Convert.ToString(ddlCandidateId.SelectedValue).Trim() != "" ? Convert.ToInt32(ddlCandidateId.SelectedValue) : 0;

            if (Request.QueryString.Count > 0)
            {
                hdLinkType.Value = Convert.ToString(Request.QueryString[0]).Trim();
            }
            if (hdLinkType.Value == "InPInter")
            {
                lblheading.Text = "Screening - Inbox";
            }
            if (hdLinkType.Value == "InVRInter")
            {
                lblheading.Text = "Recruitment Interviewer List";
            }
            if (hdLinkType.Value == "InScreeningList")
            {
                lblheading.Text = " Recruitment Screening List";
            }
            if (hdLinkType.Value == "InShPInter")
            {
                lblheading.Text = "Interviewer List- Inbox";
            }


            SqlParameter[] spars = new SqlParameter[9];
            DataSet DS = new DataSet();
            spars[0] = new SqlParameter("@strInterviewerType", SqlDbType.VarChar);
            spars[0].Value = hdLinkType.Value;
            spars[1] = new SqlParameter("@strInterviewerempcode", SqlDbType.VarChar);
            spars[1].Value = Session["Empcode"].ToString();
            spars[2] = new SqlParameter("@SkillSetID", SqlDbType.Int);
            spars[2].Value = SkillSetID;
            spars[3] = new SqlParameter("@LocationID", SqlDbType.VarChar);
            spars[3].Value = LstLocation.SelectedValue;
            spars[4] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
            spars[4].Value = Recruitment_ReqID;
            spars[5] = new SqlParameter("@BandID", SqlDbType.VarChar);
            spars[5].Value = Lstband.SelectedValue;
            spars[6] = new SqlParameter("@DeptID", SqlDbType.Int);
            spars[6].Value = DeptID;
            spars[7] = new SqlParameter("@CandidateId", SqlDbType.Int);
            spars[7].Value = candidateId;
            DS = spm.getDatasetList(spars, "SP_getInterviewerInoxList_Search");

            if (DS.Tables[0].Rows.Count > 0)
            {
                if (hdLinkType.Value == "InShPInter")
                {
                    GVInterviewfeedback.DataSource = DS.Tables[1];
                    GVInterviewfeedback.DataBind();
                }
                else
                {
                    gvInterviewerInoxList.DataSource = DS.Tables[0];
                    gvInterviewerInoxList.DataBind();
                    GVInterviewfeedback.DataSource = null;
                    GVInterviewfeedback.DataBind();
                }
            }
            else
            {
                if (hdLinkType.Value == "InVRInter")
                {
                    lblmessagesearch.Text = "Requisition Record's not found";
                }
                else
                {
                    if (hdLinkType.Value == "InScreeningList")
                    {
                        lblmessagesearch.Text = "Requisition Record's not found";
                    }
                    else
                    {
                        if (hdLinkType.Value == "InPInter")
                        {
                            lblmessagesearch.Text = "Requisition Record's not found";
                        }
                        else
                        {

                            if (DS.Tables[1].Rows.Count > 0)
                            {
                                GVInterviewfeedback.DataSource = DS.Tables[1];
                                GVInterviewfeedback.DataBind();
                            }
                            else
                            {
                                lblmessagesearch.Text = "Requisition Record's not found";
                                GVInterviewfeedback.DataSource = null;
                                GVInterviewfeedback.DataBind();
                            }
                        }
                    }
                }
                gvInterviewerInoxList.DataSource = null;
                gvInterviewerInoxList.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        Searchmethod();
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        lstSkillSet.SelectedValue = "0";
        LstLocation.SelectedValue = "0";
        Lstband.SelectedValue = "0";
        LstDepartment.SelectedValue = "0";
        lblmessagesearch.Text = "";
        getMngInterviewerInoxList();
    }
}