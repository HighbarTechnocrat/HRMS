using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_MyEscalatedService : System.Web.UI.Page
{
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

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }
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
                Page.SmartNavigation = true;
                hdnEmpCode.Value = Convert.ToString(Session["Empcode"]);
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    if (Request.QueryString.Count > 0)
                        hdninboxtype.Value = Convert.ToString(Request.QueryString[0]);

                    MyEscalatedServiceList();

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    private void MyEscalatedServiceList()
    {
        try
        {
            // DataSet dsList = new DataSet();
            DataTable dtleaveInbox = new DataTable();
            int status;
            if (ddlSearchStatus.SelectedIndex > 0)
            {
                status = Convert.ToInt32(ddlSearchStatus.SelectedValue);
            }
            else
            {
                status = 0;
            }

            dtleaveInbox = spm.GetEscalatedSerivesRequest(hdnEmpCode.Value,status);
            gvMyEscalatedService.DataSource = null;
            gvMyEscalatedService.DataBind();
            if (dtleaveInbox.Rows.Count > 0)
            {
                gvMyEscalatedService.DataSource = dtleaveInbox;
                gvMyEscalatedService.DataBind();
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void lnkcont_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/Reembursementindex");
    }
    protected void lnkView_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnRemid.Value = Convert.ToString(gvMyEscalatedService.DataKeys[row.RowIndex].Values[0]).Trim();
        //   hdnClaimsID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
        //Response.Redirect("TravelRequest.aspx?tripid=" + hdnTrip_Id.Value);
        // Response.Redirect("~/procs/Service_Req_App.aspx?id=" + hdnRemid.Value);
        Response.Redirect("~/procs/Service_Req_App.aspx?id=" + hdnRemid.Value + "&type=empEscalated");
    }

    protected void gvMyEscalatedService_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMyEscalatedService.PageIndex = e.NewPageIndex;
        this.MyEscalatedServiceList();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        MyEscalatedServiceList();
    }
}