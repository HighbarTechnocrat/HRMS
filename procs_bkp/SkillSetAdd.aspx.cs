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

public partial class procs_SkillSetAdd : System.Web.UI.Page
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
                    GetReq_ScreenerList();
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

    private void GetReq_ScreenerList()
    {
        try
        {
            int Quest_ID = 0;
            lblmessage.Text = "";
            SqlParameter[] spars = new SqlParameter[2];
            DataSet DS = new DataSet();
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Get_moduleList";
            DS = spm.getDatasetList(spars, "SP_Rec_Module_List");
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
        if (lstSkillset.SelectedValue == "0")
        {
            GetReq_ScreenerList(); 
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
            int Quest_ID = 0;
            lblmessage.Text = "";
            SqlParameter[] spars = new SqlParameter[2];
            DataSet DS = new DataSet();
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Get_moduleList";
            spars[1] = new SqlParameter("@ModuleId", SqlDbType.Int);
            spars[1].Value = lstSkillset.SelectedValue;
            DS = spm.getDatasetList(spars, "SP_Rec_Module_List");
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
                // lbltotalRecords.Text = "";
                lblmessage.Text = "Record not available";
            }

        }
        catch (Exception ex)
        {

        }
    }


    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            HDModuleID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
            SqlParameter[] spars = new SqlParameter[2];
            DataSet DS = new DataSet();
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Get_moduleData";
            spars[1] = new SqlParameter("@ModuleId", SqlDbType.Int);
            spars[1].Value = HDModuleID.Value;
            DS = spm.getDatasetList(spars, "SP_Rec_Module_List");
            if (DS.Tables[0].Rows.Count > 0)
            {
                Txt_Skillset.Text = DS.Tables[0].Rows[0]["ModuleDesc"].ToString();
                mobile_btnSave.Text = "Update SkillSet";

            }

            }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }


    protected void Link_BtnSearch_Click(object sender, EventArgs e)
    {
        Searchmethod();
    }

    protected void Link_BtnSearchClear_Click(object sender, EventArgs e)
    {
        lstSkillset.SelectedIndex = -1; 
        GetReq_ScreenerList();
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        if (Txt_Skillset.Text.Trim() == "")
        {
            lblmessage.Text = "Enter Skill Set";
            return;
        }

        if (lblmessage.Text == "")
        {
            SqlParameter[] spars = new SqlParameter[5];
            DataSet DS = new DataSet();
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            if (HDModuleID.Value == "")
            {
                spars[0].Value = "INSERT";
                spars[1] = new SqlParameter("@ModuleDesc", SqlDbType.VarChar);
                spars[1].Value = Txt_Skillset.Text.Trim();
                spars[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                spars[2].Value = Convert.ToString(Session["Empcode"]).Trim();
            }
            else
            {
                spars[0].Value = "Update";
                spars[1] = new SqlParameter("@ModuleDesc", SqlDbType.VarChar);
                spars[1].Value = Txt_Skillset.Text.Trim();
                spars[2] = new SqlParameter("@ModuleId", SqlDbType.Int);
                spars[2].Value = HDModuleID.Value;

                spars[3] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                spars[3].Value = Convert.ToString(Session["Empcode"]).Trim();
            }

            
            DS = spm.getDatasetList(spars, "SP_Rec_Module_List");
            Searchmethod();
            if (DS.Tables[0].Rows.Count > 0)
            {
                string strRecords = DS.Tables[0].Rows[0]["Record"].ToString();
                
                if (HDModuleID.Value != "")
                {
                    if (strRecords == "1")
                    {
                        lblmessage.Text = "SkillSet Updated successfully";
                    }
                    else
                    {
                        lblmessage.Text = "SkillSet Already Exit";
                    }
                }
                else
                {
                    if (strRecords == "1")
                    {
                        lblmessage.Text = "SkillSet added successfully";
                    }
                }
            }

            // Response.Redirect("~/procs/Requisition_Index.aspx");

        }

    }

    protected void mobile_cancel_Click(object sender, EventArgs e)
    {
        Txt_Skillset.Text = "";
        HDModuleID.Value = "";
        Searchmethod();
        mobile_btnSave.Text = "Add SkillSet";

    }
}