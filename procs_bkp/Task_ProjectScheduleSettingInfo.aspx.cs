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
using Microsoft.Reporting.WebForms;
using System.Linq;
using ClosedXML.Excel;

public partial class procs_Task_ProjectScheduleSettingInfo : System.Web.UI.Page
{
    #region CreativeMethods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    private static Random random = new Random();
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    #endregion

    SP_Methods spm = new SP_Methods();
    DataSet dspaymentVoucher_Apprs = new DataSet();
    String CEOInList = "N";
    double YearlymobileAmount = 0;

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            var appType = "";
            var empCode = Convert.ToString(Session["Empcode"]);
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            //mobile_btnPrintPV.Visible = false;

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["TaskMonitoringPath"]).Trim());
                    ProjectDuration();
                    GetRecordGVBind();
                }
                else
                {
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void ProjectDuration()
    {


        SqlParameter[] spars = new SqlParameter[1];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetDropDownTypeProjectSetting";

        DataTable dtTaskDetails = spm.getMobileRemDataList(spars, "SP_TASK_M_DETAILS");
        ddlProjectLocation.DataSource = dtTaskDetails;
        ddlProjectLocation.DataTextField = "LocationName";
        ddlProjectLocation.DataValueField = "comp_code";
        ddlProjectLocation.DataBind();
        ddlProjectLocation.Items.Insert(0, new ListItem("Select Project / Location Code", "0"));
    }

    public void GetRecordGVBind()
    {
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetProjectScheduleBindSetting";
        spars[1] = new SqlParameter("@comp_code", SqlDbType.VarChar);
        if (ddlProjectLocation.SelectedValue == "0")
        {
            spars[1].Value = DBNull.Value;
        }
        else
        {
            spars[1].Value = ddlProjectLocation.SelectedValue;
        }


        DataTable dtTaskDetails = spm.getMobileRemDataList(spars, "SP_TASK_M_DETAILS");
        gv_MyProcessedTaskExecuterList.DataSource = null;
        gv_MyProcessedTaskExecuterList.DataBind();
        if (dtTaskDetails.Rows.Count > 0)
        {
            lblcount.Text = "Record Count -: " + dtTaskDetails.Rows.Count;
            gv_MyProcessedTaskExecuterList.DataSource = dtTaskDetails;
            gv_MyProcessedTaskExecuterList.DataBind();
        }
        else
        {
            lblcount.Text = "";
            lblmessage.Text = "Record not available";
        }
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
       // InsertCandidateLoginDetail_ELC();
        GetRecordGVBind();
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        ProjectDuration();
        GetRecordGVBind();
    }

    protected void gv_MyProcessedTaskExecuterList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_MyProcessedTaskExecuterList.PageIndex = e.NewPageIndex;
        GetRecordGVBind();
    }

    protected void Link_AddNewProjectSchedule_Click(object sender, EventArgs e)
    {
        Response.Redirect("Task_CreateProjectScheduleSetting.aspx");
    }

    protected void TaskExecuter_Edit_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        string strValue = Convert.ToString(gv_MyProcessedTaskExecuterList.DataKeys[row.RowIndex].Values[0]).Trim();

        Response.Redirect("~/procs/Task_CreateProjectScheduleSetting.aspx?ID=" + strValue);

    }

    //private void InsertCandidateLoginDetail_ELC()
    //{
    //    try
    //    {
    //        DataSet DSCandidateLoginDetail = new DataSet();

    //        string StrCanName = "Malairaman";
    //        string[] SplitCanName = StrCanName.Split(' ');
    //        string Candidatemailpwd = RandomString(8);
    //        string hashedPassword = HashSHA1(Candidatemailpwd + "malairmn@live.com");

    //        if (SplitCanName.Length == 3)
    //        {
    //            StrCanName = SplitCanName[0].Trim() + " " + SplitCanName[2].Trim();
    //        }

            
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message);
    //        //throw;
    //    }
    //}

    //public static string RandomString(int length)
    //{
    //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@";
    //    return new string(Enumerable.Repeat(chars, length)
    //      .Select(s => s[random.Next(s.Length)]).ToArray());
    //}

    //public static string HashSHA1(string value)
    //{
    //    var sha1 = System.Security.Cryptography.SHA1.Create();
    //    var inputBytes = Encoding.ASCII.GetBytes(value);
    //    var hash = sha1.ComputeHash(inputBytes);

    //    var sb = new StringBuilder();
    //    for (var i = 0; i < hash.Length; i++)
    //    {
    //        sb.Append(hash[i].ToString("X2"));
    //    }
    //    return sb.ToString();
    //}

}