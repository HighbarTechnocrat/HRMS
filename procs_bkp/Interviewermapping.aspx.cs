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

public partial class procs_Interviewermapping : System.Web.UI.Page
{
    #region Creative_Default_methods
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
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/Reembursementindex");
    }

    #endregion
    #region PageEvents
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                // Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    GetSkillsetName();
                    GetInterviewRound();
                    GetEmployeeNameSearch();
                    GetReq_InterviewerList();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }


    #endregion
    #region PageMethods
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

    public void GetEmployeeNameSearch()
    {
        SqlParameter[] spars = new SqlParameter[2];
        DataSet DS = new DataSet();
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Get_EmpNameDropDownSearch";
        DS = spm.getDatasetList(spars, "SP_Rec_Assign_InterviewerList");
        if (DS.Tables[0].Rows.Count > 0)
        {
            DDLEmpNameSearch.DataSource = DS.Tables[0];
            DDLEmpNameSearch.DataTextField = "Emp_Name";
            DDLEmpNameSearch.DataValueField = "InterEmpCode";
            DDLEmpNameSearch.DataBind();
            DDLEmpNameSearch.Items.Insert(0, new ListItem("Search By Employee Name", "0"));
        }
    }

    public void GetInterviewRound()
    {
        SqlParameter[] spars = new SqlParameter[2];
        DataSet DS = new DataSet();
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_Req_InterviewRound";
        DS = spm.getDatasetList(spars, "SP_Rec_Assign_InterviewerList");
        if (DS.Tables[0].Rows.Count > 0)
        {
            DDLinterviewRound.DataSource = DS.Tables[0];
            DDLinterviewRound.DataTextField = "InterviewRound";
            DDLinterviewRound.DataValueField = "InterviewRound_ID";
            DDLinterviewRound.DataBind();
           // DDLinterviewRound.Items.Insert(0, new ListItem("Select Interview Round", "0"));
        }
    }

    private void GetReq_InterviewerList()
    {
        try
        {
            int Quest_ID = 0;
            lblmessage.Text = "";
            SqlParameter[] spars = new SqlParameter[2];
            DataSet DS = new DataSet();
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Get_interviewerList";
            DS = spm.getDatasetList(spars, "SP_Rec_Assign_InterviewerList");
            if (DS.Tables[0].Rows.Count > 0)
            {
                gvMngTravelRqstList.DataSource = null;
                gvMngTravelRqstList.DataBind();
                if (DS.Tables[0].Rows.Count > 0)
                {
                    gvMngTravelRqstList.DataSource = DS.Tables[0];
                    gvMngTravelRqstList.DataBind();
                    lbltotalRecords.Text = "Total Records :- " + DS.Tables[0].Rows.Count;
                    lblmessage.Text = "";
                }
            }
            else
            {
                lbltotalRecords.Text = "";
            }
        }
        catch (Exception ex)
        {

        }
    }

    #endregion
    protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMngTravelRqstList.PageIndex = e.NewPageIndex;
        if (DDLEmpNameSearch.SelectedValue == "0" && lstSkillset.SelectedValue == "0" && DDLinterviewRound.SelectedValue =="")
        {
            GetReq_InterviewerList();
        }
        else
        {
            Searchmethod();
        }
    }

    public void Searchmethod()
    {
        try
        {
            var ddlinterviewRound = "";
            var isSelected = false;

            // ddlSkillSet
            foreach (ListItem item in DDLinterviewRound.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        isSelected = true;
                        if (ddlinterviewRound == "")
                        {
                            ddlinterviewRound = item.Value;
                        }
                        else
                        {
                            ddlinterviewRound = ddlinterviewRound + "|" + item.Value;
                        }
                    }
                }
            }

            int Quest_ID = 0;
            lblmessage.Text = "";
            SqlParameter[] spars = new SqlParameter[5];
            DataSet DS = new DataSet();
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Get_interviewerList";
            spars[1] = new SqlParameter("@ModuleId", SqlDbType.Int);
            spars[1].Value = lstSkillset.SelectedValue;
            spars[2] = new SqlParameter("@InterviewRoundID", SqlDbType.VarChar);
            spars[2].Value = ddlinterviewRound;
            spars[3] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[3].Value = DDLEmpNameSearch.SelectedValue;
            DS = spm.getDatasetList(spars, "SP_Rec_Assign_InterviewerList");

            if (DS.Tables[0].Rows.Count > 0)
            {
                gvMngTravelRqstList.DataSource = null;
                gvMngTravelRqstList.DataBind();
                if (DS.Tables[0].Rows.Count > 0)
                {
                    gvMngTravelRqstList.DataSource = DS.Tables[0];
                    gvMngTravelRqstList.DataBind();
                    lbltotalRecords.Text = "Total Records :- " + DS.Tables[0].Rows.Count;
                    lblmessage.Text = "";
                }
            }
            else
            {
                //lbltotalRecords.Text = "";
                lblmessage.Text = "Record not available";
            }

        }
        catch (Exception ex)
        {

        }
    }

    public void SearchmethodSave()
    {
        try
        {
            var ddlinterviewRound = "";
            var isSelected = false;

            // ddlSkillSet
            foreach (ListItem item in DDLinterviewRound.Items)
            {
                if (item.Selected)
                {
                    if (item.Value != "" && item.Value != "0")
                    {
                        isSelected = true;
                        if (ddlinterviewRound == "")
                        {
                            ddlinterviewRound = item.Value;
                        }
                        else
                        {
                            ddlinterviewRound = ddlinterviewRound + "|" + item.Value;
                        }
                    }
                }
            }

            int Quest_ID = 0;
            
            SqlParameter[] spars = new SqlParameter[5];
            DataSet DS = new DataSet();
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Get_interviewerList";
            spars[1] = new SqlParameter("@ModuleId", SqlDbType.Int);
            spars[1].Value = lstSkillset.SelectedValue;
            //spars[2] = new SqlParameter("@InterviewRoundID", SqlDbType.VarChar);
            //spars[2].Value = ddlinterviewRound;
            spars[2] = new SqlParameter("@InterviewRoundID", SqlDbType.VarChar);
            spars[2].Value = "";
            spars[3] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[3].Value = DDLEmpNameSearch.SelectedValue;
            DS = spm.getDatasetList(spars, "SP_Rec_Assign_InterviewerList");

            if (DS.Tables[0].Rows.Count > 0)
            {
                gvMngTravelRqstList.DataSource = null;
                gvMngTravelRqstList.DataBind();
                if (DS.Tables[0].Rows.Count > 0)
                {
                    gvMngTravelRqstList.DataSource = DS.Tables[0];
                    gvMngTravelRqstList.DataBind();
                    lbltotalRecords.Text = "Total Records :- " + DS.Tables[0].Rows.Count;
                }
            }
            else
            {
                //lbltotalRecords.Text = "";
                lblmessage.Text = "Record not available";
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void Link_BtnSearch_Click(object sender, EventArgs e)
    {
        Searchmethod();
    }

    protected void Link_BtnSearchClear_Click(object sender, EventArgs e)
    {
        DDLEmpNameSearch.SelectedIndex = -1;
        lstSkillset.SelectedIndex = -1;
        lstPositionName.Enabled = false;
        DDLinterviewRound.SelectedIndex = -1;
        GetReq_InterviewerList();

    }

    public void GetEmployeeName()
    {
        SqlParameter[] spars = new SqlParameter[2];
        DataSet DS = new DataSet();
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_Req_EmployeeName";
        spars[1] = new SqlParameter("@ModuleId", SqlDbType.Int);
        spars[1].Value = lstSkillset.SelectedValue;
        DS = spm.getDatasetList(spars, "SP_Rec_Assign_InterviewerList");

        if (DS.Tables[0].Rows.Count > 0)
        {
            lstPositionName.DataSource = DS.Tables[0];
            lstPositionName.DataTextField = "Emp_Name";
            lstPositionName.DataValueField = "Emp_Code";
            lstPositionName.DataBind();
            lstPositionName.Items.Insert(0, new ListItem("Select Employee", "0"));
        }

        //if (DS.Tables[1].Rows.Count > 0)
        //{
        //    for (int i = 0; i < DS.Tables[1].Rows.Count; i++)
        //    {
        //        string strempcode = DS.Tables[1].Rows[i]["EmployeeCode"].ToString();
        //        string itemValue = strempcode.Trim();
        //        if (lstPositionName.Items.FindByValue(itemValue) != null)
        //        {
        //            string itemText = lstPositionName.Items.FindByValue(itemValue).Text;
        //            ListItem li = new ListItem();
        //            li.Text = itemText;
        //            li.Value = itemValue;
        //            lstPositionName.Items.Remove(li);
        //        }
        //    }
        //}
    }

    protected void lstSkillset_SelectedIndexChanged(object sender, EventArgs e)
    {
        Searchmethod();
        lstPositionName.Enabled = true;
        GetEmployeeName();
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        if (lstSkillset.SelectedValue == "0")
        {
            lblmessage.Text = "Select Skill Set";
            return;
        }
        if (DDLinterviewRound.SelectedValue == "0" || DDLinterviewRound.SelectedValue == "")
        {
            lblmessage.Text = "Select Interview Round";
            return;
        }
        if (lstPositionName.SelectedValue == "0")
        {
            lblmessage.Text = "Select Employee Name";
            return;
        }
       

        if (lblmessage.Text == "")
        {
            string selectedValues = string.Empty;
            foreach (ListItem li in DDLinterviewRound.Items) 
            {
                if (li.Selected == true)
                {
                    //selectedValues = li.Value ;
                    SqlParameter[] spars = new SqlParameter[5];
                    DataSet DS = new DataSet();
                    spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                    spars[0].Value = "INSERT";
                    spars[1] = new SqlParameter("@ModuleId", SqlDbType.Int);
                    spars[1].Value = lstSkillset.SelectedValue;
                    spars[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                    spars[2].Value = lstPositionName.SelectedValue;
                    spars[3] = new SqlParameter("@InterviewRoundID", SqlDbType.VarChar);
                    spars[3].Value = li.Value;
                    spars[4] = new SqlParameter("@EmpCodelogin", SqlDbType.VarChar);
                    spars[4].Value = Session["Empcode"].ToString();
                    DS = spm.getDatasetList(spars, "SP_Rec_Assign_InterviewerList");
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        string strRecords = DS.Tables[0].Rows[0]["Record"].ToString();
                        if (strRecords == "1")
                        {
                            lblmessage.Text = "Interviewer added successfully";
                        }
                    }
                }
            }
            SearchmethodSave();
            GetEmployeeNameSearch();
            lstPositionName.SelectedIndex = -1;
            lstSkillset.SelectedIndex = -1;
            DDLinterviewRound.SelectedIndex = -1;
            // Response.Redirect("~/procs/Requisition_Index.aspx");

        }

    }

    //protected void DDLinterviewRound_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Searchmethod();
    //}
}