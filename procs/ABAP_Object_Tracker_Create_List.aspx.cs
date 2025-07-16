using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class ABAP_Object_Tracker_Create_List : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;

    SP_Methods spm = new SP_Methods();
    string strempcode = "";
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void lnkcont_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/SampleForm");
    }

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/ABAP_Object_Tracker_Index.aspx");
            }
            else
            {
                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString.Count > 0)
                    {

                        LinkBtnSavePopup.Visible = false;
                        if (!string.IsNullOrEmpty(Request.QueryString["ABAPODId"]))
                        {
                            HDID.Value = Request.QueryString["ABAPODId"].Trim();
                            LinkBtnSavePopup.Text = "Submit";
                            LinkBtnSavePopup.Visible = true;
                        }
                        if (!string.IsNullOrEmpty(Request.QueryString["Update"]))
                        {

                            hdnABAPODUploadId.Value = Request.QueryString["Update"].Trim();
                            hdnABAPODIdId.Value = Request.QueryString["ABAPODId"].Trim();
                            LinkBtnSavePopup.Text = "Update";
                            lblheading.Text = "Update ABAP Object Plan";
                            LinkBtnSavePopup.Visible = true;
                        }
                    }

                    GetABAPTrackerObjectDetail();
                    GetAllMasterData();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";

                    datergsstart.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    datergsfinish.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    datergssub.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    datergsapprovel.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    datefsstart.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    datefsfinish.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    dateabapstart.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    dateabapfinish.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    datehbtstart.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    datehbtfinish.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    datectmstart.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    datectmfinish.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    dateplanned.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    dategolive.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    RgsRevActualPrepStart.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    RgsRevActualPrepFinish.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    RevActualSubmit.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    RevActualApprove.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtFsRevisedStartdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtFsRevisedFinishdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtAbapRevisedStart.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtAbapRevisedFinish.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtRevisedHbtStartDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtRevisedHbtFinishDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtRevisedCtmStartDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtRevisedCtmFinishDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtRevisedUatdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtRevisedGoliveDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");

                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }


    public void GetAllMasterData()
    {
        DataSet DS = new DataSet();

        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetAllMasterData";

        DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");


        if (DS != null)
        {
            if (DS.Tables[0].Rows.Count > 0)
            {
                ddlModule.DataSource = DS.Tables[0];
                ddlModule.DataTextField = "ModuleDesc";
                ddlModule.DataValueField = "ModuleId";
                ddlModule.DataBind();
                ddlModule.Items.Insert(0, new ListItem("Select Module", "0"));
            }

            if (DS.Tables[1].Rows.Count > 0)
            {
                ddlFRICE_Category.DataSource = DS.Tables[1];
                ddlFRICE_Category.DataTextField = "FCategoryName";
                ddlFRICE_Category.DataValueField = "FCategoryId";
                ddlFRICE_Category.DataBind();
                ddlFRICE_Category.Items.Insert(0, new ListItem("Select Frice Category", "0"));
            }

            if (DS.Tables[2].Rows.Count > 0)
            {
                ddlPriorityType.DataSource = DS.Tables[2];
                ddlPriorityType.DataTextField = "PriorityName";
                ddlPriorityType.DataValueField = "PriorityId";
                ddlPriorityType.DataBind();
                ddlPriorityType.Items.Insert(0, new ListItem("Select Priority Type", "0"));
            }

            if (DS.Tables[3].Rows.Count > 0)
            {
                ddlComplexity.DataSource = DS.Tables[3];
                ddlComplexity.DataTextField = "ComplexityName";
                ddlComplexity.DataValueField = "ComplexityId";
                ddlComplexity.DataBind();
                ddlComplexity.Items.Insert(0, new ListItem("Select Complexity", "0"));
            }

            if (DS.Tables[4].Rows.Count > 0)
            {
                ddlFunctional_Consultant.DataSource = DS.Tables[4];
                ddlFunctional_Consultant.DataTextField = "Emp_Name";
                ddlFunctional_Consultant.DataValueField = "Emp_Code";
                ddlFunctional_Consultant.DataBind();
                ddlFunctional_Consultant.Items.Insert(0, new ListItem("Select Functional Consultant", "0"));
            }

            if (DS.Tables[5].Rows.Count > 0)
            {
                ddlabapConsultant.DataSource = DS.Tables[5];
                ddlabapConsultant.DataTextField = "Emp_Name";
                ddlabapConsultant.DataValueField = "Emp_Code";
                ddlabapConsultant.DataBind();
                ddlabapConsultant.Items.Insert(0, new ListItem("Select ABAP Consultant", "0"));
            }

        }

    }

    protected void GetABAPTrackerObjectDetail()
    {
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetABAPTrackerObjectDetail";
        spars[1] = new SqlParameter("@ABAPODId", SqlDbType.VarChar);
        spars[1].Value = hdnABAPODUploadId.Value;

        DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");

        if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
        {
            RgsRevActualPrepStartdate.Visible = true;
            RgsRevActualPrepFinishdate.Visible = true;
            RevActualSubmitdate.Visible = true;
            RevActualApprovedate.Visible = true;

            FsRevisedStartdate.Visible = true;
            FsRevisedFinishdate.Visible = true;

            AbapRevisedStartdate.Visible = true;
            AbapRevisedFinishdate.Visible = true;

            RevisedHbtStartDate.Visible = true;
            RevisedHbtFinishDate.Visible = true;

            RevisedCtmStartDate.Visible = true;
            RevisedCtmFinishDate.Visible = true;

            RevisedUatdate.Visible = true;

            RevisedGoliveDate.Visible = true;

            ddlModule.SelectedValue = DS.Tables[0].Rows[0]["ModuleId"].ToString();
            txtInterface.Text = DS.Tables[0].Rows[0]["Interface"].ToString();
            txtDescription.Text = DS.Tables[0].Rows[0]["Development_Desc"].ToString();
            txtScope.Text = DS.Tables[0].Rows[0]["Scope"].ToString();
            ddlFRICE_Category.SelectedValue = DS.Tables[0].Rows[0]["FCategoryId"].ToString();
            txtPriority.Text = DS.Tables[0].Rows[0]["Priority"].ToString();
            ddlPriorityType.SelectedValue = DS.Tables[0].Rows[0]["PriorityId"].ToString();
            ddlComplexity.SelectedValue = DS.Tables[0].Rows[0]["ComplexityId"].ToString();
            ddlFunctional_Consultant.SelectedValue = DS.Tables[0].Rows[0]["FSConsultantId"].ToString();

            datergsstart.Text = DS.Tables[0].Rows[0]["PlannedPrepStart"].ToString();
            datergsfinish.Text = DS.Tables[0].Rows[0]["PlannedPrepFinish"].ToString();
            datergssub.Text = DS.Tables[0].Rows[0]["PlannedSubmit"].ToString();
            datergsapprovel.Text = DS.Tables[0].Rows[0]["PlannedApprove"].ToString();
            RgsRevActualPrepStart.Text = DS.Tables[0].Rows[0]["RgsRevActualPrepStart"].ToString();
            RgsRevActualPrepFinish.Text = DS.Tables[0].Rows[0]["RgsRevActualPrepFinish"].ToString();
            RevActualApprove.Text = DS.Tables[0].Rows[0]["RgsRevActualApprove"].ToString();
            RevActualSubmit.Text = DS.Tables[0].Rows[0]["RgsRevActualSubmit"].ToString();

            datefsstart.Text = DS.Tables[0].Rows[0]["FSPlannedStart"].ToString();
            datefsfinish.Text = DS.Tables[0].Rows[0]["FSPlannedFinish"].ToString();
            txtFsRevisedStartdate.Text = DS.Tables[0].Rows[0]["FsRevisedStart"].ToString();
            txtFsRevisedFinishdate.Text = DS.Tables[0].Rows[0]["FsRevisedFinish"].ToString();

            ddlabapConsultant.SelectedValue = DS.Tables[0].Rows[0]["PlannedABAPerId"].ToString();
            txtDuration.Text = DS.Tables[0].Rows[0]["DevDuaration"].ToString();
            dateabapstart.Text = DS.Tables[0].Rows[0]["ABAPPlannedStart"].ToString();
            dateabapfinish.Text = DS.Tables[0].Rows[0]["ABAPPlannedFinish"].ToString();
            txtAbapRevisedStart.Text = DS.Tables[0].Rows[0]["AbapRevisedStart"].ToString();
            txtAbapRevisedFinish.Text = DS.Tables[0].Rows[0]["AbapRevisedFinish"].ToString();

            datehbtstart.Text = DS.Tables[0].Rows[0]["HBTPlannedStart"].ToString();
            datehbtfinish.Text = DS.Tables[0].Rows[0]["HBTPlannedFinish"].ToString();
            txtRevisedHbtStartDate.Text = DS.Tables[0].Rows[0]["HBTRevisedStartDate"].ToString();
            txtRevisedHbtFinishDate.Text = DS.Tables[0].Rows[0]["HBTRevisedFinishDate"].ToString();

            datectmstart.Text = DS.Tables[0].Rows[0]["CTMPlannedStart"].ToString();
            datectmfinish.Text = DS.Tables[0].Rows[0]["CTMPlannedFinish"].ToString();
            txtRevisedCtmStartDate.Text = DS.Tables[0].Rows[0]["CTMRevisedStart"].ToString();
            txtRevisedCtmFinishDate.Text = DS.Tables[0].Rows[0]["CTMRevisedFinish"].ToString();

            dateplanned.Text = DS.Tables[0].Rows[0]["UATPlanned_Date"].ToString();
            txtRevisedUatdate.Text = DS.Tables[0].Rows[0]["UATRevised_Date"].ToString();

            dategolive.Text = DS.Tables[0].Rows[0]["GLPlannedDate"].ToString();
            txtRevisedGoliveDate.Text = DS.Tables[0].Rows[0]["GLRevisedDate"].ToString();

            txtclientname.Text = DS.Tables[0].Rows[0]["ReusableClientName"].ToString();
            ddlStatus.SelectedValue = DS.Tables[0].Rows[0]["ReusableStatus"].ToString();
            txtefforts.Text = DS.Tables[0].Rows[0]["ReusableAdditonalEffort"].ToString();
            txtPercentage.Text = DS.Tables[0].Rows[0]["ReusablePercent"].ToString();
            txtremark.Text = DS.Tables[0].Rows[0]["ReusableDetailsRemarks"].ToString();
            txttcode.Text = DS.Tables[0].Rows[0]["Custom_Tcode"].ToString();

            datergsstart.Enabled = false;
            datergsfinish.Enabled = false;
            datergssub.Enabled = false;
            datergsapprovel.Enabled = false;

            datefsstart.Enabled = false;
            datefsfinish.Enabled = false;

            txtDuration.Enabled = false;
            dateabapstart.Enabled = false;
            dateabapfinish.Enabled = false;

            datehbtstart.Enabled = false;
            datehbtfinish.Enabled = false;

            datectmstart.Enabled = false;
            datectmfinish.Enabled = false;

            dateplanned.Enabled = false;

            dategolive.Enabled = false;

            if (DS.Tables[0].Rows[0]["RGSStatus"].ToString() == "7")
            {
                RgsRevActualPrepStart.Enabled = false;
                RgsRevActualPrepFinish.Enabled = false;
                RevActualSubmit.Enabled = false;
                RevActualApprove.Enabled = false;
            }

            if (DS.Tables[0].Rows[0]["FSStatus"].ToString() == "8" || DS.Tables[0].Rows[0]["FSStatus"].ToString() == "20")
            {
                txtFsRevisedStartdate.Enabled = false;
                txtFsRevisedFinishdate.Enabled = false;

            }

            if (DS.Tables[0].Rows[0]["ABAPDevStatus"].ToString() == "10")
            {
                ddlabapConsultant.Enabled = false;
                txtAbapRevisedStart.Enabled = false;
                txtAbapRevisedFinish.Enabled = false;
            }

            if (DS.Tables[0].Rows[0]["HBTTestStatus"].ToString() == "12" || DS.Tables[0].Rows[0]["HBTTestStatus"].ToString() == "17")
            {
                txtRevisedHbtStartDate.Enabled = false;
                txtRevisedHbtFinishDate.Enabled = false;

            }
            if (DS.Tables[0].Rows[0]["CTMStatus"].ToString() == "17")
            {
                txtRevisedCtmStartDate.Enabled = false;
                txtRevisedCtmFinishDate.Enabled = false;

            }

            if (DS.Tables[0].Rows[0]["UAT_Status"].ToString() == "7")
            {
                txtRevisedUatdate.Enabled = false;

            }
            if (DS.Tables[0].Rows[0]["PRD_Status"].ToString() == "24")
            {
                txtRevisedGoliveDate.Enabled = false;

            }
        }


    }

    protected void LinkBtnBackPopup_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["Update"]))
        {
            Response.Redirect("ABAP_Object_Tracker_Plan_View.aspx?ABAPODId=" + hdnABAPODIdId.Value);
        }
        else
        {
            Response.Redirect("ABAP_Object_Tracker_All_List.aspx");
        }
    }

    protected void LinkBtnSavePopup_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlModule.SelectedValue == "0")
            {
                lblmessage.Text = "Please select the module.";
                return;
            }
            if (txtInterface.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter the interface.";
                return;
            }
            if (txtDescription.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter the development description.";
                return;
            }
            if (txtScope.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter the scope.";
                return;
            }
            if (ddlFRICE_Category.SelectedValue == "0")
            {
                lblmessage.Text = "Please select the frice category.";
                return;
            }
            if (txtPriority.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter the priority.";
                return;
            }
            if (ddlPriorityType.SelectedValue == "0")
            {
                lblmessage.Text = "Please select the priority type.";
                return;
            }
            if (ddlComplexity.SelectedValue == "0")
            {
                lblmessage.Text = "Please select the complexity.";
                return;
            }
            if (ddlFunctional_Consultant.SelectedValue == "0")
            {
                lblmessage.Text = "Please select the functional consultant.";
                return;
            }
            if (datergsstart.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter the rgs planned preparation start date.";
                return;
            }
            if (datergsfinish.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter the rgs planned preparation finish date.";
                return;
            }
            if (datergssub.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter the rgs planned submission date.";
                return;
            }
            if (datergsapprovel.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter the rgs planned approval date.";
                return;
            }
            if (datefsstart.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter the fs planned start date.";
                return;
            }
            if (datefsfinish.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter the fs planned finish date.";
                return;
            }
            if (ddlabapConsultant.SelectedValue == "0")
            {
                lblmessage.Text = "Please select the planned abap consultant.";
                return;
            }
            if (txtDuration.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter the abap development duration.";
                return;
            }
            if (dateabapstart.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter the abap development planned start date.";
                return;
            }
            if (dateabapfinish.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter the abap development planned finish date.";
                return;
            }
            if (datehbtstart.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter the hbt testing planned start date.";
                return;
            }
            if (datehbtfinish.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter the hbt testing planned finish date.";
                return;
            }
            if (datectmstart.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter the ctm testing planned start date.";
                return;
            }
            if (datectmfinish.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter the ctm testing planned finish date.";
                return;
            }
            if (dateplanned.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter the uat sign off planned date.";
                return;
            }
            if (dategolive.Text.Trim() == "")
            {
                lblmessage.Text = "Please enter the go-live planned date.";
                return;
            }

            string[] strdate;
            string strdatergsstart = "";
            string strdatergsfinish = "";
            string strdatergssub = "";
            string strdatergsapprovel = "";

            string strdatefsstart = "";
            string strdatefsfinish = "";

            string strdateabapstart = "";
            string strdateabapfinish = "";

            string strdatehbtstart = "";
            string strdatehbtfinish = "";

            string strdatectmstart = "";
            string strdatectmfinish = "";

            string strdateplanned = "";

            string strdategolive = "";

            string strRgsRevActualPrepStart = "";
            string strRgsRevActualPrepFinish = "";
            string strRevActualSubmit = "";
            string strRevActualApprove = "";

            string strtxtFsRevisedFinishdate = "";
            string strtxtFsRevisedStartdate = "";

            string strtxtAbapRevisedStart = "";
            string strtxtAbapRevisedFinish = "";

            string strRevisedHbtStartDate = "";
            string strRevisedHbtFinishDate = "";

            string strRevisedCtmStartDate = "";
            string strRevisedCtmFinishDate = "";

            string strRevisedUatdate = "";
            string strRevisedGoliveDate = "";

            if (Convert.ToString(datergsstart.Text).Trim() != "")
            {
                strdate = Convert.ToString(datergsstart.Text).Trim().Split('/');
                strdatergsstart = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }
            if (Convert.ToString(datergsfinish.Text).Trim() != "")
            {
                strdate = Convert.ToString(datergsfinish.Text).Trim().Split('/');
                strdatergsfinish = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }
            if (Convert.ToString(datergssub.Text).Trim() != "")
            {
                strdate = Convert.ToString(datergssub.Text).Trim().Split('/');
                strdatergssub = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }
            if (Convert.ToString(datergsapprovel.Text).Trim() != "")
            {
                strdate = Convert.ToString(datergsapprovel.Text).Trim().Split('/');
                strdatergsapprovel = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(datefsstart.Text).Trim() != "")
            {
                strdate = Convert.ToString(datefsstart.Text).Trim().Split('/');
                strdatefsstart = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }
            if (Convert.ToString(datefsfinish.Text).Trim() != "")
            {
                strdate = Convert.ToString(datefsfinish.Text).Trim().Split('/');
                strdatefsfinish = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(dateabapstart.Text).Trim() != "")
            {
                strdate = Convert.ToString(dateabapstart.Text).Trim().Split('/');
                strdateabapstart = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }
            if (Convert.ToString(datefsfinish.Text).Trim() != "")
            {
                strdate = Convert.ToString(dateabapfinish.Text).Trim().Split('/');
                strdateabapfinish = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(datehbtstart.Text).Trim() != "")
            {
                strdate = Convert.ToString(datehbtstart.Text).Trim().Split('/');
                strdatehbtstart = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }
            if (Convert.ToString(datehbtfinish.Text).Trim() != "")
            {
                strdate = Convert.ToString(datehbtfinish.Text).Trim().Split('/');
                strdatehbtfinish = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(datectmstart.Text).Trim() != "")
            {
                strdate = Convert.ToString(datectmstart.Text).Trim().Split('/');
                strdatectmstart = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }
            if (Convert.ToString(datectmfinish.Text).Trim() != "")
            {
                strdate = Convert.ToString(datectmfinish.Text).Trim().Split('/');
                strdatectmfinish = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(dateplanned.Text).Trim() != "")
            {
                strdate = Convert.ToString(dateplanned.Text).Trim().Split('/');
                strdateplanned = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(dategolive.Text).Trim() != "")
            {
                strdate = Convert.ToString(dategolive.Text).Trim().Split('/');
                strdategolive = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(RgsRevActualPrepStart.Text).Trim() != "")
            {
                strdate = Convert.ToString(RgsRevActualPrepStart.Text).Trim().Split('/');
                strRgsRevActualPrepStart = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(RgsRevActualPrepFinish.Text).Trim() != "")
            {
                strdate = Convert.ToString(RgsRevActualPrepFinish.Text).Trim().Split('/');
                strRgsRevActualPrepFinish = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(RevActualSubmit.Text).Trim() != "")
            {
                strdate = Convert.ToString(RevActualSubmit.Text).Trim().Split('/');
                strRevActualSubmit = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(RevActualApprove.Text).Trim() != "")
            {
                strdate = Convert.ToString(RevActualApprove.Text).Trim().Split('/');
                strRevActualApprove = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(txtFsRevisedStartdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFsRevisedStartdate.Text).Trim().Split('/');
                strtxtFsRevisedStartdate = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(txtFsRevisedFinishdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFsRevisedFinishdate.Text).Trim().Split('/');
                strtxtFsRevisedFinishdate = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(txtAbapRevisedStart.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtAbapRevisedStart.Text).Trim().Split('/');
                strtxtAbapRevisedStart = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(txtAbapRevisedFinish.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtAbapRevisedFinish.Text).Trim().Split('/');
                strtxtAbapRevisedFinish = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(txtRevisedHbtStartDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtRevisedHbtStartDate.Text).Trim().Split('/');
                strRevisedHbtStartDate = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(txtRevisedHbtFinishDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtRevisedHbtFinishDate.Text).Trim().Split('/');
                strRevisedHbtFinishDate = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(txtRevisedCtmStartDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtRevisedCtmStartDate.Text).Trim().Split('/');
                strRevisedCtmStartDate = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(txtRevisedCtmFinishDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtRevisedCtmFinishDate.Text).Trim().Split('/');
                strRevisedCtmFinishDate = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(txtRevisedUatdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtRevisedUatdate.Text).Trim().Split('/');
                strRevisedUatdate = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(txtRevisedGoliveDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtRevisedGoliveDate.Text).Trim().Split('/');
                strRevisedGoliveDate = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            string sType = "";
            if (Convert.ToString(hdnABAPODUploadId.Value).Trim() != "")
            {
                sType = "UpdateABAPObjectDetail";
            }
            else
            {
                sType = "InsertNewABAPObject";
            }

            DataSet dsgoal = new DataSet();
            SqlParameter[] spars = new SqlParameter[49];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = sType;

            spars[1] = new SqlParameter("@ABAPODUploadId", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(HDID.Value).Trim();

            spars[2] = new SqlParameter("@Module", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(ddlModule.SelectedValue).Trim();

            spars[3] = new SqlParameter("@Interface", SqlDbType.VarChar);
            spars[3].Value = Convert.ToString(txtInterface.Text).Trim();

            spars[4] = new SqlParameter("@DevelopmentDescription", SqlDbType.VarChar);
            spars[4].Value = Convert.ToString(txtDescription.Text).Trim();

            spars[5] = new SqlParameter("@Scope", SqlDbType.VarChar);
            spars[5].Value = Convert.ToString(txtScope.Text).Trim();

            spars[6] = new SqlParameter("@FRICECategory", SqlDbType.VarChar);
            spars[6].Value = Convert.ToString(ddlFRICE_Category.SelectedValue).Trim();

            spars[7] = new SqlParameter("@Priority", SqlDbType.Int);
            spars[7].Value = Convert.ToString(txtPriority.Text).Trim();

            spars[8] = new SqlParameter("@PriorityType", SqlDbType.VarChar);
            spars[8].Value = Convert.ToString(ddlPriorityType.SelectedValue).Trim();

            spars[9] = new SqlParameter("@Complexity", SqlDbType.VarChar);
            spars[9].Value = Convert.ToString(ddlComplexity.SelectedValue).Trim();

            spars[10] = new SqlParameter("@FunctionalConsultant", SqlDbType.VarChar);
            spars[10].Value = Convert.ToString(ddlFunctional_Consultant.SelectedValue).Trim();

            spars[11] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
            spars[11].Value = strempcode;

            spars[12] = new SqlParameter("@RGSPlannedStartDate", SqlDbType.VarChar);
            spars[12].Value = strdatergsstart;

            spars[13] = new SqlParameter("@RGSPlannedFinishDate", SqlDbType.VarChar);
            spars[13].Value = strdatergsfinish;

            spars[14] = new SqlParameter("@RGSPlannedSubmissionDate", SqlDbType.VarChar);
            spars[14].Value = strdatergssub;

            spars[15] = new SqlParameter("@RGSPlannedApprovalDate", SqlDbType.VarChar);
            spars[15].Value = strdatergsapprovel;

            spars[16] = new SqlParameter("@FSPlannedStartDate", SqlDbType.VarChar);
            spars[16].Value = strdatefsstart;

            spars[17] = new SqlParameter("@FSPlannedFinishDate", SqlDbType.VarChar);
            spars[17].Value = strdatefsfinish;

            spars[18] = new SqlParameter("@ABAPDevPlannedABAPer", SqlDbType.VarChar);
            spars[18].Value = Convert.ToString(ddlabapConsultant.SelectedValue).Trim();

            spars[19] = new SqlParameter("@ABAPDevDuration", SqlDbType.VarChar);
            spars[19].Value = Convert.ToString(txtDuration.Text).Trim();

            spars[20] = new SqlParameter("@ABAPDevPlannedStartDate", SqlDbType.VarChar);
            spars[20].Value = strdateabapstart;

            spars[21] = new SqlParameter("@ABAPDevPlannedFinishDate", SqlDbType.VarChar);
            spars[21].Value = strdateabapfinish;

            spars[22] = new SqlParameter("@HBTTestPlannedStartDate", SqlDbType.VarChar);
            spars[22].Value = strdatehbtstart;

            spars[23] = new SqlParameter("@HBTTestPlannedFinishDate", SqlDbType.VarChar);
            spars[23].Value = strdatehbtfinish;

            spars[24] = new SqlParameter("@CTMPlannedStartDate", SqlDbType.VarChar);
            spars[24].Value = strdatectmstart;

            spars[25] = new SqlParameter("@CTMPlannedFinishDate", SqlDbType.VarChar);
            spars[25].Value = strdatectmfinish;

            spars[26] = new SqlParameter("@UATSignOffPlannedDate", SqlDbType.VarChar);
            spars[26].Value = strdateplanned;

            spars[27] = new SqlParameter("@GoLivePlannedDate", SqlDbType.VarChar);
            spars[27].Value = strdategolive;

            spars[28] = new SqlParameter("@ReusableClientName", SqlDbType.VarChar);
            spars[28].Value = Convert.ToString(txtclientname.Text).Trim();

            spars[29] = new SqlParameter("@ReusableStatusId", SqlDbType.VarChar);
            spars[29].Value = Convert.ToString(ddlStatus.SelectedValue).Trim();

            spars[30] = new SqlParameter("@ReusableAdditionalEfforts", SqlDbType.VarChar);
            spars[30].Value = Convert.ToString(txtefforts.Text).Trim();

            spars[31] = new SqlParameter("@Reusablepercentage", SqlDbType.VarChar);
            spars[31].Value = Convert.ToString(txtPercentage.Text).Trim();

            spars[32] = new SqlParameter("@ReusableRemarks", SqlDbType.VarChar);
            spars[32].Value = Convert.ToString(txtremark.Text).Trim();

            spars[33] = new SqlParameter("@ReusableTcode", SqlDbType.VarChar);
            spars[33].Value = Convert.ToString(txttcode.Text).Trim();

            spars[34] = new SqlParameter("@ABAPODId", SqlDbType.VarChar);
            spars[34].Value = Convert.ToString(hdnABAPODUploadId.Value).Trim();

            if (sType == "UpdateABAPObjectDetail")
            {
                spars[35] = new SqlParameter("@RgsRevActualPrepStart", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(strRgsRevActualPrepStart))
                {
                    spars[35].Value = strRgsRevActualPrepStart;
                }
                else
                {
                    spars[35].Value = DBNull.Value;
                }
                spars[36] = new SqlParameter("@RgsRevActualPrepFinish", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(strRgsRevActualPrepFinish))
                {
                    spars[36].Value = strRgsRevActualPrepFinish;
                }
                else
                {
                    spars[36].Value = DBNull.Value;
                }
                spars[37] = new SqlParameter("@RgsRevActualSubmit", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(strRevActualSubmit))
                {
                    spars[37].Value = strRevActualSubmit;
                }
                else
                {
                    spars[37].Value = DBNull.Value;
                }
                spars[38] = new SqlParameter("@RgsRevActualApprove", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(strRevActualApprove))
                {
                    spars[38].Value = strRevActualApprove;
                }
                else
                {
                    spars[38].Value = DBNull.Value;
                }
                spars[39] = new SqlParameter("@FsRevisedStartdate", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(strtxtFsRevisedStartdate))
                {
                    spars[39].Value = strtxtFsRevisedStartdate;
                }
                else
                {
                    spars[39].Value = DBNull.Value;
                }
                spars[40] = new SqlParameter("@FsRevisedFinishdate", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(strtxtFsRevisedFinishdate))
                {
                    spars[40].Value = strtxtFsRevisedFinishdate;
                }
                else
                {
                    spars[40].Value = DBNull.Value;
                }
                spars[41] = new SqlParameter("@abapRevisedStart", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(strtxtAbapRevisedStart))
                {
                    spars[41].Value = strtxtAbapRevisedStart;
                }
                else
                {
                    spars[41].Value = DBNull.Value;
                }
                spars[42] = new SqlParameter("@abapRevisedFinish", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(strtxtAbapRevisedFinish))
                {
                    spars[42].Value = strtxtAbapRevisedFinish;
                }
                else
                {
                    spars[42].Value = DBNull.Value;
                }
                spars[43] = new SqlParameter("@HbtRevisedStart", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(strRevisedHbtStartDate))
                {
                    spars[43].Value = strRevisedHbtStartDate;
                }
                else
                {
                    spars[43].Value = DBNull.Value;
                }
                spars[44] = new SqlParameter("@HbtRevisedFinish", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(strRevisedHbtFinishDate))
                {
                    spars[44].Value = strRevisedHbtFinishDate;
                }
                else
                {
                    spars[44].Value = DBNull.Value;
                }
                spars[45] = new SqlParameter("@CtmRevisedStart", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(strRevisedCtmStartDate))
                {
                    spars[45].Value = strRevisedCtmStartDate;
                }
                else
                {
                    spars[45].Value = DBNull.Value;
                }
                spars[46] = new SqlParameter("@CtmRevisedFinish", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(strRevisedCtmFinishDate))
                {
                    spars[46].Value = strRevisedCtmFinishDate;
                }
                else
                {
                    spars[46].Value = DBNull.Value;
                }
                spars[47] = new SqlParameter("@UatRevised_Date", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(strRevisedUatdate))
                {
                    spars[47].Value = strRevisedUatdate;
                }
                else
                {
                    spars[47].Value = DBNull.Value;
                }
                spars[48] = new SqlParameter("@GoliveRevisedDate", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(strRevisedGoliveDate))
                {
                    spars[48].Value = strRevisedGoliveDate;
                }
                else
                {
                    spars[48].Value = DBNull.Value;
                }
            }
            if (dsgoal != null)
            {
                dsgoal = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
            }
            else
            {
                lblmessage.Text = "The record not inssert.";
                return;
            }

            Response.Redirect("ABAP_Object_Tracker_All_List.aspx");
        }

        catch (Exception)
        {
            Console.WriteLine("Error: ");
            throw;
        }
    }

}
