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
using System.Data.SqlClient;


public partial class InboxExecuter : System.Web.UI.Page
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
  
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
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
                    Page.SmartNavigation = true;
                    editform.Visible = true;
                    var TaskId = "";
                    var TaskRefId = "";
                    BindTaskExecuterGrid(empCode);
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

    private void BindTaskExecuterGrid(string empCode)
    {
        try
        {
            gv_TaskExecuterList.DataSource = null;
            gv_TaskExecuterList.DataBind();
            var TaskId = "";
            var TaskRefId = "";
            var getResult = spm.getTaskExecuterDDL(empCode, "GetTaskExecuterInbox", TaskId, TaskRefId, Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), 0, Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), Convert.ToBoolean(0), 0, Convert.ToBoolean(0), 0, Convert.ToBoolean(0), Convert.ToBoolean(0));
            var getTaskExecuterList = getResult.Tables[0];

            if (getTaskExecuterList.Rows.Count > 0)
            {
                gv_TaskExecuterList.DataSource = getTaskExecuterList;
                gv_TaskExecuterList.DataBind();
            }
           
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }


    protected void TaskExecuter_Edit_Click(object sender, EventArgs e)
    {
        try
        {
			Session.Remove("AssignedTo");
			LinkButton btn = (LinkButton)sender;

            string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
            string TaskExecuter_EditVal = commandArgs[0];
            string Task_Ref_Id = commandArgs[1];
          //  string Task_Id = commandArgs[2];

            Response.Redirect("~/procs/TaskExecuter_Edit.aspx?Task_Id=" + TaskExecuter_EditVal + "&TaskRefId=" + Task_Ref_Id + "&flag=" + "NA");
        }
        catch (Exception ex)
        {

        }

    }
    protected void gv_TaskExecuterList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gv_TaskExecuterList.PageIndex = e.NewPageIndex;
            this.BindTaskExecuterGrid(Convert.ToString(Session["Empcode"]));
        }
        catch (Exception)
        {

            throw;
        }
    }
}