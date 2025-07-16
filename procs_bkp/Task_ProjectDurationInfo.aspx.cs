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

public partial class procs_Task_ProjectDurationInfo : System.Web.UI.Page
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

                    if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != string.Empty)
                    {
                        HFIDQString.Value = Request.QueryString["ID"];
                    }

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
        

        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetDropDownTypeProjectSchedule";

        spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();

        DataSet  dtTaskDetails = spm.getDatasetList(spars, "SP_TASK_M_DETAILS");

        if (HFIDQString.Value == "1")
        {
            ddlProjectLocation.DataSource = dtTaskDetails.Tables[0];
        }
        else
        {
            ddlProjectLocation.DataSource = dtTaskDetails.Tables[1];
        }
        ddlProjectLocation.DataTextField = "LocationName";
        ddlProjectLocation.DataValueField = "comp_code";
        ddlProjectLocation.DataBind();
        ddlProjectLocation.Items.Insert(0, new ListItem("Select Project / Location Code", "0"));
    }

    public void GetRecordGVBind()
    {
        SqlParameter[] spars = new SqlParameter[3];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetProjectScheduleBind";
        spars[1] = new SqlParameter("@comp_code", SqlDbType.VarChar);
        if (ddlProjectLocation.SelectedValue == "0")
        {
            spars[1].Value = DBNull.Value;
        }
        else
        {
            spars[1].Value = ddlProjectLocation.SelectedValue;
        }

        spars[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        spars[2].Value = Convert.ToString(Session["Empcode"]).Trim();

        DataSet DS = spm.getDatasetList(spars, "SP_TASK_M_DETAILS");
        gv_MyProcessedTaskExecuterList.DataSource = null;
        gv_MyProcessedTaskExecuterList.DataBind();

        if (HFIDQString.Value == "1")
        {
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblcount.Text = "Record Count -: " + DS.Tables[0].Rows.Count;
                gv_MyProcessedTaskExecuterList.DataSource = DS.Tables[0];
                gv_MyProcessedTaskExecuterList.DataBind();
                GVRecordsDetails.DataSource = null;
                GVRecordsDetails.DataBind();
                BTN_Back.Visible = false;
            }
            else
            {
                lblcount.Text = "";
                lblmessage.Text = "Record not available";
            }
        }
        else
        {
            if (DS.Tables[2].Rows.Count > 0)
            {
                lblcount.Text = "Record Count -: " + DS.Tables[2].Rows.Count;
                gv_MyProcessedTaskExecuterList.DataSource = DS.Tables[2];
                gv_MyProcessedTaskExecuterList.DataBind();
                GVRecordsDetails.DataSource = null;
                GVRecordsDetails.DataBind();
                BTN_Back.Visible = false;
            }
            else
            {
                lblcount.Text = "";
                lblmessage.Text = "Record not available";
            }
        }

        
    }

    public void GetRecordGVBindSelected(string Comp_Code)
    {
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetProjectScheduleBind";
        spars[1] = new SqlParameter("@comp_code", SqlDbType.VarChar);
        spars[1].Value = Comp_Code;

        DataSet DS = spm.getDatasetList(spars, "SP_TASK_M_DETAILS");
        GVRecordsDetails.DataSource = null;
        GVRecordsDetails.DataBind();
        if (DS.Tables[1].Rows.Count > 0)
        {
            GVRecordsDetails.DataSource = DS.Tables[1];
            GVRecordsDetails.DataBind();
            Label1.Visible = true;
            BTN_Back.Visible = true;
        }
        else
        {
            
            Label1.Visible = false;
            BTN_Back.Visible = false;
        }
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        GetRecordGVBind();
        Label1.Visible = false ;
        BTN_Back.Visible = false;
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        ProjectDuration();
        GetRecordGVBind();
        Label1.Visible = false;
        BTN_Back.Visible = false;

    }

    protected void gv_MyProcessedTaskExecuterList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_MyProcessedTaskExecuterList.PageIndex = e.NewPageIndex;
        GetRecordGVBind();
    }

    protected void gv_MyProcessedTaskExecuterList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");

        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");
            e.Row.Cells[1].Attributes.Add("onmouseover", "this.style.color='#F28820'");
            e.Row.Cells[1].Attributes.Add("onmouseout", "this.style.color='Blue'");
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gv_MyProcessedTaskExecuterList, "Select$" + e.Row.RowIndex);
        }
    }

    protected void gv_MyProcessedTaskExecuterList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = gv_MyProcessedTaskExecuterList.SelectedIndex;
        string CompCode = (gv_MyProcessedTaskExecuterList.DataKeys[selectedIndex]["comp_code"]).ToString();
        GetRecordGVBindSelected(CompCode);
        Session["dgselectedIndex"] = selectedIndex;
        foreach (GridViewRow row in gv_MyProcessedTaskExecuterList.Rows)
        {
            row.CssClass = row.CssClass.Replace("dgSupervisor", String.Empty);
        }
    }

    protected void BTN_Back_Click(object sender, EventArgs e)
    {
	Response.Redirect("Task_ProjectDurationInfo.aspx?ID="+Convert.ToString(HFIDQString.Value));
        //ProjectDuration();
        //GetRecordGVBind();
        //Label1.Visible = false;
        //BTN_Back.Visible = false;
    }
}