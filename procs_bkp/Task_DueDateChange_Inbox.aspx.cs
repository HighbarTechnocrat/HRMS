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

public partial class Task_DueDateChange_Inbox : System.Web.UI.Page
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
    #region PageEvents
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
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

                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {

                    // hdnRemid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["TaskMonitoringPath"]).Trim());
                    //TXT_AssginmentDate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                    editform.Visible = true;
                    divbtn.Visible = false;
                    divmsg.Visible = false;
                    hdnempcode.Value = Convert.ToString(Session["Empcode"]);

                    // hdnTaskRefID.Value = Convert.ToString(Request.QueryString[0]); ;
                    hdnAttendeeID.Value = "0";

                    BindData("All");
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";

                }
                else
                {

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
    private void BindData(string qtype)
    {
        try
        {
            var empCode = Convert.ToString(Session["Empcode"]);
            //var getVal = Convert.ToDouble(hdnTaskID.Value);
            var getResult = spm.getTaskMonitoringDDL(empCode, 0, "getDueDateChangeRequest");
            if (getResult != null)
            {
                if (getResult.Tables.Count > 0)
                {
                    dv_TaskList.DataSource = null;
                    dv_TaskList.DataBind();
                    var gettable = getResult.Tables[0];
                    if (gettable.Rows.Count > 0)
                    {
                        dv_TaskList.DataSource = gettable;
                        dv_TaskList.DataBind();
                    }
                    else
                    {

                    }
                }
            }

        }
        catch (Exception ex)
        {

            throw;
        }
    }

    #endregion


    protected void lnk_TLI_Edit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            var MyTask_EditVal = Convert.ToDouble(btn.CommandArgument.Trim());
            // Response.Redirect("~/procs/TaskMonitoring.aspx");
            Response.Redirect("~/procs/Task_D_DateChange.aspx?id=" + MyTask_EditVal);
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void dv_TaskList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            dv_TaskList.PageIndex = e.NewPageIndex;
            this.BindData("All");
        }
        catch (Exception)
        {

            throw;
        }
    }
}
