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

public partial class Req_JD_Bank_Search : System.Web.UI.Page
{
    #region CreativeMethods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    SP_Methods spm = new SP_Methods();
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    #endregion
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
                //strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    GetSkillsetName();
                    GetPositionName();
                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["QuestionnaireDocumentpath"]).Trim());
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    #region PageMethod
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
    public void GetPositionName()
    {
        DataTable dtPositionName = new DataTable();
        dtPositionName = spm.GetRecruitment_PositionTitle();
        if (dtPositionName.Rows.Count > 0)
        {
            lstPositionName.DataSource = dtPositionName;
            lstPositionName.DataTextField = "PositionTitle";
            lstPositionName.DataValueField = "PositionTitle_ID";
            lstPositionName.DataBind();
            lstPositionName.Items.Insert(0, new ListItem("Select Position", "0"));
        }
    }
    #endregion

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            int Quest_ID = 0;
            DataTable JDBankList = new DataTable();
            string Stype = "getAssignJDBank_Filter";
            #region Check For Blank Fields
            if (Convert.ToString(lstSkillset.SelectedValue).Trim() == "" || Convert.ToString(lstSkillset.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Skill Set";
                return;
            }
            if (Convert.ToString(lstPositionName.SelectedValue).Trim() == "" || Convert.ToString(lstPositionName.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Position Title";
                return;
            }
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            #endregion
             JDBankList = spm.GetAssign_JDBankFilter(Stype, Convert.ToInt32(lstSkillset.SelectedValue), Convert.ToInt32(lstPositionName.SelectedValue));
            gvMngTravelRqstList.DataSource = null;
            gvMngTravelRqstList.DataBind();
            if (JDBankList.Rows.Count > 0)
            {
                gvMngTravelRqstList.DataSource = JDBankList;
                gvMngTravelRqstList.DataBind();
                mobile_cancel.Visible = true;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void mobile_cancel_Click(object sender, EventArgs e)
    {
        #region Check For Blank Fields
        int Count = gvMngTravelRqstList.Rows.Count;
        if (Count == 0)
        {
            lblmessage.Text = "Please select data ";
            return;
        }
        foreach (GridViewRow gvrow in gvMngTravelRqstList.Rows)
        {
            CheckBox chk = (CheckBox)gvrow.FindControl("CheckBox1");
            if (chk != null & !chk.Checked)
            {
                lblmessage.Text = "Please select check box";
                return;
            }

        }
        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }
        #endregion
        lblmessage.Text = (gvMngTravelRqstList.DataKeys[0].Value.ToString());
        Response.Redirect("Req_Requisition_Create.aspx");

    }

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnQuest_ID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
        Response.Redirect("Req_JD_Bank_create.aspx?Quest_ID=" + hdnQuest_ID.Value+ "&Filter_id=1");
    }
}