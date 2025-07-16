using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class procs_Rec_RecruiterInboxClosed : System.Web.UI.Page
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
                    string strreqType = "";
                    if (Request.QueryString.Count > 0)
                    {
                       // hdLinkType.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdLinkType.Value = "VRRClosed";
                    }
                   Search_Candidate_Name(strreqType);
                    getMngRecruterInoxList();
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


    private void Search_Candidate_Name(string strreqType)
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            DataSet DS = new DataSet();
            spars[0] = new SqlParameter("@strreqType", SqlDbType.NVarChar);
            spars[0].Value = strreqType;
            spars[1] = new SqlParameter("@strreqempcode", SqlDbType.VarChar);
            spars[1].Value = Session["Empcode"].ToString();
            DS = spm.getDatasetList(spars, "SP_GetRecruterInoxList");
            if (DS.Tables[0].Rows.Count > 0)
            {
                lstCandidateName.DataSource = DS.Tables[0];
                lstCandidateName.DataTextField = "CandidateName";
                lstCandidateName.DataValueField = "Candidate_ID";
                lstCandidateName.DataBind();
            }
            lstCandidateName.Items.Insert(0, new ListItem("Select Candidate Name", "0"));
        }
        catch (Exception ex)
        {
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

    private void getMngRecruterInoxList()
    {
        try
        {
                Status.Visible = true;
            dsRecruterInox = spm.getRecruterInoxList(Convert.ToString(Session["Empcode"]).Trim(), hdLinkType.Value);

            gvRecruterInoxList.DataSource = dsRecruterInox.Tables[0];
            gvRecruterInoxList.DataBind();

            if (dsRecruterInox.Tables[0].Rows.Count > 0)
            {
                lstRequisitionNo.DataSource = dsRecruterInox.Tables[0];
                lstRequisitionNo.DataTextField = "RequisitionNumber";
                lstRequisitionNo.DataValueField = "Recruitment_ReqID";
                lstRequisitionNo.DataBind();
                lstRequisitionNo.Items.Insert(0, new ListItem("Select Requisition No", "0"));
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
        HFRecruitment_ReqID.Value = Convert.ToString(gvRecruterInoxList.DataKeys[row.RowIndex].Values[0]).Trim();
       
        if (hdLinkType.Value == "VRRClosed")
        {
            Session.Add("RecruiterList", "VRRClosed");
            Response.Redirect("~/procs/RecruiterViewEditClosed.aspx?Recruitment_ReqID=" + HFRecruitment_ReqID.Value);
        }
    }

    #endregion

    protected void gvRecruterInoxList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRecruterInoxList.PageIndex = e.NewPageIndex;
        if (Lstband.SelectedValue == "0" && LstDepartment.SelectedValue == "0" && LstLocation.SelectedValue == "0" && lstRequisitionNo.SelectedValue == "0" && lstSkillSet.SelectedValue == "0")
        {
            getMngRecruterInoxList();
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
            int SkillSetID = 0, Recruitment_ReqID = 0, DeptID = 0, StatusID = 0, Candidate_ID = 0;
            string StatusName = "";
            SkillSetID = Convert.ToString(lstSkillSet.SelectedValue).Trim() != "" ? Convert.ToInt32(lstSkillSet.SelectedValue) : 0;
            Recruitment_ReqID = Convert.ToString(lstRequisitionNo.SelectedValue).Trim() != "" ? Convert.ToInt32(lstRequisitionNo.SelectedValue) : 0;
            DeptID = Convert.ToString(LstDepartment.SelectedValue).Trim() != "" ? Convert.ToInt32(LstDepartment.SelectedValue) : 0;
            StatusName = Convert.ToString(LstRequistionStatus.SelectedValue).Trim() != "" ? Convert.ToString(LstRequistionStatus.SelectedValue) : "";
            Candidate_ID = Convert.ToString(lstCandidateName.SelectedValue).Trim() != "" ? Convert.ToInt32(lstCandidateName.SelectedValue) : 0;

            if (Request.QueryString.Count > 0)
            {
                hdLinkType.Value = Convert.ToString(Request.QueryString[0]).Trim();
            }
           
            SqlParameter[] spars = new SqlParameter[9];
            DataSet DS = new DataSet();
            spars[0] = new SqlParameter("@strreqType", SqlDbType.VarChar);
            spars[0].Value = hdLinkType.Value;
            spars[1] = new SqlParameter("@strreqempcode", SqlDbType.VarChar);
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
            spars[7] = new SqlParameter("@StatusName", SqlDbType.NVarChar);
            spars[7].Value = StatusName;
            spars[8] = new SqlParameter("@Candidate_ID", SqlDbType.Int);
            spars[8].Value = Candidate_ID;
            DS = spm.getDatasetList(spars, "SP_GetRecruterInoxList_Search");

            if (DS.Tables[0].Rows.Count > 0)
            {
                gvRecruterInoxList.DataSource = DS.Tables[0];
                gvRecruterInoxList.DataBind();

                lblmessagesearch.Text = "";
            }
            else
            {
                lblmessagesearch.Text = "Requisition Record's not found";
                
                gvRecruterInoxList.DataSource = null;
                gvRecruterInoxList.DataBind();

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
        LstRequistionStatus.SelectedValue = "0";
        lstCandidateName.SelectedValue = "0";
        lblmessagesearch.Text = "";
        getMngRecruterInoxList();
    }
}