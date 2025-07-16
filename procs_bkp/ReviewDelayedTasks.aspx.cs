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
using ClosedXML.Excel;

public partial class procs_ReviewDelayedTasks : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    LeaveBalance bl = new LeaveBalance();
    SP_Methods spm = new SP_Methods();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
            if (!Page.IsPostBack)
            {
                Session["chkbtnStatus_Appr"] = "";
                hflEmpCode.Value = Convert.ToString(Session["Empcode"]);
                getDelayedTaskDetails();
               
                                 //Session value remove
                Session.Remove("ddlTaskExecuter");
                Session.Remove("ddlTaskRefId");
                Session.Remove("ddlTaskId");
                Session.Remove("ddlStatus");
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    //private void getDelayedTaskPendingCount()
    //{
    //    DataTable dtresult = new DataTable();
    //    try
    //    {
    //        SqlParameter[] spars = new SqlParameter[3];
    //        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
    //        spars[0].Value = "GetDelayedTaskPendingCount";
    //        spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
    //        spars[1].Value = Convert.ToString(Session["Empcode"]);
    //        dtresult = spm.getMobileRemDataList(spars, "SP_TASK_M_DETAILS_DASHBOARD");
    //        if (dtresult.Rows.Count > 0)
    //        {
    //            if (Convert.ToInt32(dtresult.Rows[0]["DelayedTask"]) > 0)
    //            {
    //                dgDelayed.DataSource = dtresult;
    //                dgDelayed.DataBind();
    //                dvDelayed.Visible = true;
    //            }
    //        }
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}
    private void getDelayedTaskDetails()
    {
        DataTable dtresult = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetDelayedTaskList";
            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(Session["Empcode"]);
            dtresult = spm.getMobileRemDataList(spars, "SP_TASK_M_DETAILS_DASHBOARD");
            GRDDelayedList.DataSource = null;
            GRDDelayedList.DataBind();
            if (dtresult.Rows.Count > 0)
            {
                GRDDelayedList.DataSource = dtresult;
                GRDDelayedList.DataBind();

                //if (DvDelayedList.Visible && hdnDelayedList.Value == "")
                //{
                //    DvDelayedList.Visible = false;
                //    dgDelayed.Rows[0].CssClass = "dgDelayedHide";
                //    Session.Remove("DelayedTask");
                //}
                //else
                //{
                //    dgDelayed.Rows[0].CssClass = "dgDelayedShow";
                //    DvDelayedList.Visible = true;
                //}

            }

        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void DelayTask_Edit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
            string TaskId = commandArgs[0];
            string Task_Ref_Id = commandArgs[1];
            Session["DelayedTask"] = "Yes";
            Response.Redirect("~/procs/TaskExecuter_EditView.aspx?Task_Id=" + TaskId + "&TaskRefId=" + Task_Ref_Id + "&flag=" + "sp");
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void GRDDelayedList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GRDDelayedList.PageIndex = e.NewPageIndex;
            getDelayedTaskDetails();
        }
        catch (Exception)
        {

            throw;
        }
    }

   
}