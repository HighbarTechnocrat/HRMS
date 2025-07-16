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

public partial class procs_Ref_CandidateReport : System.Web.UI.Page
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
                    HFPathresume.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateResumeDocpath"]).Trim());

                    GetSkillsetName();
                    GetReferenceBy();
                    GetRecruiterName();
                    //  GetCandidateName();
                    GetCandidateStatus();
                    getRefCandidateList();
                   

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


   
    public void GetSkillsetName()
    {
        SqlParameter[] spars = new SqlParameter[1];
        DataSet DS = new DataSet();
        spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
        spars[0].Value = "sp_Req_HRMS_MODULE_Name";
        DS = spm.getDatasetList(spars, "sp_Ref_SearchCandidateReferral");
        if (DS.Tables[0].Rows.Count > 0)
        {
            lstSkillSet.DataSource = DS.Tables[0];
            lstSkillSet.DataTextField = "ModuleDesc";
            lstSkillSet.DataValueField = "ModuleId";
            lstSkillSet.DataBind();
            lstSkillSet.Items.Insert(0, new ListItem("Select Skillset", "0"));

        }
    }
    public void GetReferenceBy()
    {
        SqlParameter[] spars = new SqlParameter[1];
        DataSet DS = new DataSet();
        spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
        spars[0].Value = "GetReferenceBy";
        DS = spm.getDatasetList(spars, "sp_Ref_SearchCandidateReferral");

        if (DS.Tables[0].Rows.Count > 0)
        {
            lstReferenceBy.DataSource = DS.Tables[0];
            lstReferenceBy.DataTextField = "ReferenceBy";
            lstReferenceBy.DataValueField = "EmpCode";
            lstReferenceBy.DataBind();
            lstReferenceBy.Items.Insert(0, new ListItem("Select Refer. By", "0"));
        }
    }

    public void GetRecruiterName()
    {
        SqlParameter[] spars = new SqlParameter[1];
        DataSet DS = new DataSet();
        spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
        spars[0].Value = "GetRecruiterName";

        DS = spm.getDatasetList(spars, "sp_Ref_SearchCandidateReferral");

        if (DS.Tables[0].Rows.Count > 0)
        {
            lstRecuitername.DataSource = DS.Tables[0];
            lstRecuitername.DataTextField = "RecruiterName";
            lstRecuitername.DataValueField = "Emp_Code";
            lstRecuitername.DataBind();
            lstRecuitername.Items.Insert(0, new ListItem("Select Recruiter", "0"));
        }
    }

    public void GetCandidateName()
    {
        SqlParameter[] spars = new SqlParameter[1];
        DataSet DS = new DataSet();
        spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
        spars[0].Value = "GetReferenceCandidateName";

        DS = spm.getDatasetList(spars, "sp_Ref_SearchCandidateReferral");

        if (DS.Tables[0].Rows.Count > 0)
        {
            DDlRefCandidateName.DataSource = DS.Tables[0];
            DDlRefCandidateName.DataTextField = "Ref_CandidateName";
            DDlRefCandidateName.DataValueField = "Ref_Candidate_ID";
            DDlRefCandidateName.DataBind();
            DDlRefCandidateName.Items.Insert(0, new ListItem("Select Refer Candidate", "0"));
        }
    }

    public void GetCandidateStatus()
    {
        SqlParameter[] spars = new SqlParameter[1];
        DataSet DS = new DataSet();
        spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
        spars[0].Value = "GetListRefCandidate";

        DS = spm.getDatasetList(spars, "sp_Ref_SearchCandidateReferral");

        if (DS.Tables[1].Rows.Count > 0)
        {
            DDLRefCandidateStatus.DataSource = DS.Tables[1];
            DDLRefCandidateStatus.DataTextField = "Ref_StatusID";
            DDLRefCandidateStatus.DataValueField = "Ref_StatusID";
            DDLRefCandidateStatus.DataBind();
            DDLRefCandidateStatus.Items.Insert(0, new ListItem("Select Refer Candidate Status", "0"));
        }
    }


    private void getRefCandidateList()
    {
        try
        {
            if (TxtExpTo.Text.Trim() != "" && txtExpfrom.Text.Trim() == "")
            {
                lblmessagesearch.Text = "Enter the Exp. From";
                TxtExpTo.Text = "";
                return;
            }
            if (txtExpfrom.Text.Trim() != "" && TxtExpTo.Text.Trim() == "")
            {
                lblmessagesearch.Text = "Enter the Exp. To";
                txtExpfrom.Text = "";
                return;
            }
            if (txttodate.Text.Trim() != "" && txtfromdate.Text.Trim() == "") 
            {
                lblmessagesearch.Text = "Select the Refer. From Date";
                txttodate.Text = "";
                return; 
            }

            int SkillSetID = 0, RefCandID=0;
            SkillSetID = Convert.ToString(lstSkillSet.SelectedValue).Trim() != "" ? Convert.ToInt32(lstSkillSet.SelectedValue) : 0;
            RefCandID = Convert.ToString(DDlRefCandidateName.SelectedValue).Trim() != "" ? Convert.ToInt32(DDlRefCandidateName.SelectedValue) : 0;

            string[] fromdate, todate;
            string StartDate = "", EndDate = "";
            DateTime? STDate = null;
            DateTime? EDDate = null;

            if (txtfromdate.Text != "")
            {
                fromdate = Convert.ToString(txtfromdate.Text).Trim().Split('/');
                StartDate = Convert.ToString(fromdate[2]) + "-" + Convert.ToString(fromdate[1]) + "-" + Convert.ToString(fromdate[0]);
                STDate = DateTime.ParseExact(StartDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                if (txttodate.Text.Trim() != "")
                {
                    todate = Convert.ToString(txttodate.Text).Trim().Split('/');
                    EndDate = Convert.ToString(todate[2]) + "-" + Convert.ToString(todate[1]) + "-" + Convert.ToString(todate[0]);
                }
                else
                {
                    EndDate = DateTime.Today.ToString("yyyy") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("dd");
                }
                EDDate = DateTime.ParseExact(EndDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

            }

            SqlParameter[] spars = new SqlParameter[10];
            DataSet DS = new DataSet();
            spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
            spars[0].Value = "GetListRefCandidate";
            spars[1] = new SqlParameter("@SkilID", SqlDbType.Int);
            spars[1].Value = SkillSetID;
            spars[2] = new SqlParameter("@ReferBy", SqlDbType.VarChar);
            spars[2].Value = lstReferenceBy.SelectedValue;
            spars[3] = new SqlParameter("@RequirterID", SqlDbType.VarChar);
            spars[3].Value = lstRecuitername.SelectedValue;
            spars[4] = new SqlParameter("@ExpFrom", SqlDbType.VarChar);
            if (txtExpfrom.Text.Trim() == "")
            {
                spars[4].Value = "0";
            }
            else
            {
                spars[4].Value = txtExpfrom.Text.Trim();
            }
            spars[5] = new SqlParameter("@ExpTo", SqlDbType.VarChar);

            if (TxtExpTo.Text.Trim() == "")
            {
                spars[5].Value = "0";
            }
            else
            {
                spars[5].Value = TxtExpTo.Text.Trim();
            }
            spars[6] = new SqlParameter("@Datefrom", SqlDbType.DateTime);
            spars[6].Value = STDate;
            spars[7] = new SqlParameter("@DateTo", SqlDbType.DateTime);
            spars[7].Value = EDDate;
            spars[8] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[8].Value = txtRefCandidateSearch.Text.Trim();
            spars[9] = new SqlParameter("@StatusIDref", SqlDbType.VarChar);
            spars[9].Value = DDLRefCandidateStatus.SelectedValue;

            DS = spm.getDatasetList(spars, "sp_Ref_SearchCandidateReferral");

            if (DS.Tables[0].Rows.Count > 0)
            {
                GVRefCandidate.DataSource = DS.Tables[0];
                GVRefCandidate.DataBind();
                lblmessagesearch.Text = "";
            }
            else
            {
                lblmessagesearch.Text = "Refer. Candidate Record's not found";
                GVRefCandidate.DataSource = null;
                GVRefCandidate.DataBind();

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
        HFRecruitment_ReqID.Value = Convert.ToString(GVRefCandidate.DataKeys[row.RowIndex].Values[0]).Trim();
        if (hdLinkType.Value == "InRec")
        {
            Response.Redirect("~/procs/RecruiterSendShortListing.aspx?Recruitment_ReqID=" + HFRecruitment_ReqID.Value);
        }
        if (hdLinkType.Value == "VRR")
        {
            Session.Add("RecruiterList", "VRR");
            
        }
        if (hdLinkType.Value == "VRRIS")
        {
            Session.Add("RecruiterList", "VRRIS");
            
        }
        if (hdLinkType.Value == "RECISL")
        {
            Response.Redirect("~/procs/SheduleInterviewer.aspx?Recruitment_ReqID=" + HFRecruitment_ReqID.Value);
        }
    }
    #endregion

    protected void GVRefCandidate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVRefCandidate.PageIndex = e.NewPageIndex;
        getRefCandidateList();
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        getRefCandidateList();
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        lstSkillSet.SelectedValue = "0";
        lstReferenceBy.SelectedValue = "0";
        lstRecuitername.SelectedValue = "0";
        lblmessagesearch.Text = "";
        txtfromdate.Text = "";
        txttodate.Text = ""; 
        txtExpfrom.Text= "";
        TxtExpTo.Text = "";
        txtRefCandidateSearch.Text = "";
        DDlRefCandidateName.SelectedValue = "0";
        DDLRefCandidateStatus.SelectedValue = "0";
        getRefCandidateList();
    }

    protected void txtfromdate_TextChanged(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";

        if ((Convert.ToString(txtfromdate.Text).Trim() != "") && (Convert.ToString(txttodate.Text).Trim() != ""))
        {
            strdate = Convert.ToString(txtfromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

            strdate = Convert.ToString(txttodate.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);


            DateTime startDate = Convert.ToDateTime(strfromDate);
            DateTime endDate = Convert.ToDateTime(strToDate);
            if (startDate > endDate)
            {
                lblmessagesearch.Text = "From Date should be less than To Date ";
                txtfromdate.Text = "";
                return;
            }
            else
            {
                lblmessagesearch.Text = "";
                // ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Start date should be greate than End date')", true);
            }
        }
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";

        if ((Convert.ToString(txtfromdate.Text).Trim() != "") && (Convert.ToString(txttodate.Text).Trim() != ""))
        {
            strdate = Convert.ToString(txtfromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

            strdate = Convert.ToString(txttodate.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);


            DateTime startDate = Convert.ToDateTime(strfromDate);
            DateTime endDate = Convert.ToDateTime(strToDate);
            if (startDate > endDate)
            {
                lblmessagesearch.Text = "To Date should be greater than From Date ";
                txttodate.Text = "";

                return;
            }
            else
            {
                // ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Start date should be greate than End date')", true);
            }
        }
    }
}