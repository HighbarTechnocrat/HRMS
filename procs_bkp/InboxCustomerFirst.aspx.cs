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

public partial class InboxCustomerFirst : System.Web.UI.Page
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/customerFirst");
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

                    var HODCode = "";
                    var PMCode = "";
                    var loginCode = Convert.ToString(hdnEmpCode.Value);
                    //Get HOD AND PM Code
                    DataTable dtHOD = new DataTable();
                    dtHOD = spm.CheckCustomerFIRSTHOD(Convert.ToString(hdnEmpCode.Value));
                    if (dtHOD.Rows.Count > 0)
                    {
                        HODCode = Convert.ToString(dtHOD.Rows[0]["Hod"]);
                        PMCode = Convert.ToString(dtHOD.Rows[0]["PMCode"]);
                    }

                    DataTable dtCEOEMPCODE = new DataTable();
                    //  dtCEOEMPCODE = spm.GetCEOEmpCode();
                    dtCEOEMPCODE = spm.Get_CSH_CEOEmpCode();
                    if (dtCEOEMPCODE.Rows.Count > 0)
                    {                        
                        var CeoEmpCode = Convert.ToString(dtCEOEMPCODE.Rows[0]["Emp_Code"]);
                        if (loginCode == CeoEmpCode)
                        {
                            hdnISCEO.Value = "3";
                            InboxCEO();
                        }
                        else
                        {
                            if(loginCode==PMCode)
                            {
                                hdnISCEO.Value = "1";
                                InboxHODPM(Convert.ToString(Session["Empcode"]));
                            }
                            else if(loginCode == HODCode)
                            {
                                hdnISCEO.Value = "2";
                                InboxHODPM(Convert.ToString(Session["Empcode"]));
                            }
                            else
                            {
                                hdnISCEO.Value = "0";
                            }                            
                        }
                    }
                    else
                    {
                        if (loginCode == PMCode)
                        {
                            hdnISCEO.Value = "1";
                            InboxHODPM(Convert.ToString(Session["Empcode"]));
                        }
                        else if (loginCode == HODCode)
                        {
                            hdnISCEO.Value = "2";
                            InboxHODPM(Convert.ToString(Session["Empcode"]));
                        }
                        else
                        {
                            hdnISCEO.Value = "0";
                        }
                    }
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
       var id = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnRemid.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
        //   hdnClaimsID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
        //Response.Redirect("TravelRequest.aspx?tripid=" + hdnTrip_Id.Value);
        Response.Redirect("~/procs/CustomerFirst_App.aspx?id=" + id + "&type=app");
    }

    #endregion

    #region PageMethods
    private void InboxPM(string HODEmpCode)
    {
        try
        {
            // DataSet dsList = new DataSet();
            DataTable dtHodInbox = new DataTable();
            dtHodInbox = spm.GetPM_INBOX(HODEmpCode);

            gvMngTravelRqstList.DataSource = null;
            gvMngTravelRqstList.DataBind();
            if (dtHodInbox.Rows.Count > 0)
            {
                gvMngTravelRqstList.DataSource = dtHodInbox;
                gvMngTravelRqstList.DataBind();

            }

        }
        catch (Exception ex)
        {

        }
    }
    private void InboxHOD(string HODEmpCode)
    {
        try
        {
            // DataSet dsList = new DataSet();
            DataTable dtHodInbox = new DataTable();
            dtHodInbox = spm.GetHOD_INBOX(HODEmpCode);

            gvMngTravelRqstList.DataSource = null;
            gvMngTravelRqstList.DataBind();
            if (dtHodInbox.Rows.Count > 0)
            {
                gvMngTravelRqstList.DataSource = dtHodInbox;
                gvMngTravelRqstList.DataBind();

            }

        }
        catch (Exception ex)
        {

        }
    }

    private void InboxHODPM(string HODEmpCode)
    {
        try
        {
            // DataSet dsList = new DataSet();
            DataTable dtHodInbox = new DataTable();
            dtHodInbox = spm.GetPMORHOD_INBOX(HODEmpCode);

            gvMngTravelRqstList.DataSource = null;
            gvMngTravelRqstList.DataBind();
            if (dtHodInbox.Rows.Count > 0)
            {
                gvMngTravelRqstList.DataSource = dtHodInbox;
                gvMngTravelRqstList.DataBind();

            }

        }
        catch (Exception ex)
        {

        }
    }

    private void InboxCEO()
    {
        try
        {
            // DataSet dsList = new DataSet();
            DataTable dtHodInbox = new DataTable();
            dtHodInbox = spm.GetCEO_INBOX();

            gvMngTravelRqstList.DataSource = null;
            gvMngTravelRqstList.DataBind();
            if (dtHodInbox.Rows.Count > 0)
            {
                gvMngTravelRqstList.DataSource = dtHodInbox;
                gvMngTravelRqstList.DataBind();

            }

        }
        catch (Exception ex)
        {

        }
    }
    #endregion


    protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        var isCEO = Convert.ToInt32(hdnISCEO.Value);
        if(isCEO==1)
        {
            gvMngTravelRqstList.PageIndex = e.NewPageIndex;
            this.InboxHODPM(Convert.ToString(Session["Empcode"]));
        }
        else if(isCEO==2)
        {
            gvMngTravelRqstList.PageIndex = e.NewPageIndex;
            this.InboxHODPM(Convert.ToString(Session["Empcode"]));
        }
        else
        {
            gvMngTravelRqstList.PageIndex = e.NewPageIndex;
            this.InboxCEO();
        }
    }
}
