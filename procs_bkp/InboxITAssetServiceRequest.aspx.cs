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

public partial class InboxITAssetServiceRequest : System.Web.UI.Page
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

    #region Page_Events

    private void Page_Load(object sender, System.EventArgs e)
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
                Page.SmartNavigation = true;
                hdnEmpCode.Value = Convert.ToString(Session["Empcode"]);
               
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    if (Request.QueryString.Count > 0)
                        hdninboxtype.Value = Convert.ToString(Request.QueryString[0]);
                    InboxMobileRemReqstList();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    protected void lnkFuelDetails_Click(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnRemid.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnStatusId.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
        hdnReqType.Value= Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
        if (hdnStatusId.Value == "1" || hdnStatusId.Value == "3")
        {
            if (hdnReqType.Value == "New Joinee")
            {
                Response.Redirect("~/procs/ITAssetService_Req.aspx?id=" + hdnRemid.Value + "&type=app");
            }
            else
            {
                Response.Redirect("~/procs/ITAssetService_RepairReplaceReq.aspx?id=" + hdnRemid.Value + "&type=app");
            }
        }
        if (hdnStatusId.Value == "2")
        {
            if (hdnReqType.Value == "New Joinee")
            {
                Response.Redirect("~/procs/ITAssetService_Req_App.aspx?id=" + hdnRemid.Value + "&type=app");
            }
            else
            {
                Response.Redirect("~/procs/ITAssetService_RepairReplaceReqApp.aspx?id=" + hdnRemid.Value + "&type=app");
            }
        }
    }

    #endregion

    #region PageMethods
    private void InboxMobileRemReqstList()
    {
        try
        {
            var loginCode = hdnEmpCode.Value;
            DataTable dtCustodianCODE = new DataTable();
            DataTable dtleaveInbox1 = new DataTable();
            dtCustodianCODE = spm.GetAllCustodianDetails();
            if (dtCustodianCODE.Rows.Count > 0)
            {
                var result = dtCustodianCODE.Select("EmpCode like '%" + loginCode + "%'");
                if (result != null)
                {
                    DataTable dtleaveInbox = new DataTable();
                    dtleaveInbox = spm.GetCustodianINBOX(hdnEmpCode.Value);
                    if (dtleaveInbox.Rows.Count > 0)
                    {
                        gvMngTravelRqstList.DataSource = dtleaveInbox;
                        gvMngTravelRqstList.DataBind();
                    }
                }
            }

            DataTable dtITHODCODE = new DataTable();
            dtITHODCODE = spm.GetITHod();
            if (dtITHODCODE.Rows.Count > 0)
            {
                var ITHodCode = Convert.ToString(dtITHODCODE.Rows[0]["HOD"]);


                if (loginCode == ITHodCode)
                {
                    dtleaveInbox1 = spm.GetITHodINBOX(hdnEmpCode.Value);
                    if (dtleaveInbox1.Rows.Count > 0)
                    {
                        gvMngTravelRqstList.DataSource = dtleaveInbox1;
                        gvMngTravelRqstList.DataBind();
                    }
                    else
                    {
                        gvMngTravelRqstList.DataSource = null;
                        gvMngTravelRqstList.DataBind();
                    }
                }
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
        this.InboxMobileRemReqstList();
    }
}
