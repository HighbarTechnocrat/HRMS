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


public partial class MyTask_Cancel : System.Web.UI.Page
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
                    hdnTaskRefID.Value = "0";
                    var getVal = Convert.ToDouble(hdnTaskRefID.Value);


                    var getResult = spm.getTaskMonitoringDDL(empCode, getVal, "GetMyTaskGirdforcancel");
                    var getMyTaskList = getResult.Tables[0];

                    if (getMyTaskList.Rows.Count > 0)
                    {
                        BindMyTaskGrid(getMyTaskList);
                    }
                    else
                    {
                        lblmessage.Text = "Data Not found";
                    }
                }
                else
                {

                }
            }


        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }

    private void BindMyTaskGrid(DataTable dataTable)
    {
        try
        {
            gv_MyTask.DataSource = null;
            gv_MyTask.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                gv_MyTask.DataSource = dataTable;
                gv_MyTask.DataBind();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }



    protected void MyTask_Edit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            var MyTask_EditVal = Convert.ToDouble(btn.CommandArgument.Trim());
            // Response.Redirect("~/procs/TaskMonitoring.aspx");
            Response.Redirect("~/procs/Cancel_Task.aspx?id=" + MyTask_EditVal);
        }
        catch (Exception)
        {

            throw;
        }



    }
    public static void write2log(string strmsg)
    {

        System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Configuration.ConfigurationSettings.AppSettings["LogTestPath"] +
             "Log_" + DateTime.Now.Day.ToString() + ".txt", true);
        sw.WriteLine(strmsg);
        sw.Flush();
        sw.Close();
    }

    private void BindPage()
    {

        var empCode = Convert.ToString(Session["Empcode"]);
        var getVal = Convert.ToDouble(hdnTaskRefID.Value);

        var getResult = spm.getTaskMonitoringDDL(empCode, getVal, "GetMyTaskGirdforcancel");
        var getMyTaskList = getResult.Tables[0];

        if (getMyTaskList.Rows.Count > 0)
        {
            BindMyTaskGrid(getMyTaskList);
        }
        else
        {
            lblmessage.Text = "Data Not found";
        }
    }






    protected void gv_MyTask_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gv_MyTask.PageIndex = e.NewPageIndex;
            BindPage();
        }
        catch (Exception)
        {

            throw;
        }
    }
}