using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class procs_ExitProcess_ClearanceInbox : System.Web.UI.Page
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
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    DataTable dtExitFrm;

    string strempcode = "";
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;

                    getClearanceInbox();

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    private void getClearanceInbox()
    {
        try
        {
            // DataSet dsList = new DataSet();

            dtExitFrm = spm.GetClearanceInbox(strempcode);
            gvMngLeaveRqstList.DataSource = null;
            gvMngLeaveRqstList.DataBind();
            if (dtExitFrm.Rows.Count > 0)
            {
                gvMngLeaveRqstList.DataSource = dtExitFrm;
                gvMngLeaveRqstList.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnReqid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[0]).Trim();

            Response.Redirect("ExitProcess_ClearanceApproveForm.aspx?resignationid=" + hdnReqid.Value + "&Type=Add");

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    protected void gvMngLeaveRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        getClearanceInbox();
        gvMngLeaveRqstList.PageIndex = e.NewPageIndex;
        gvMngLeaveRqstList.DataSource = dtExitFrm;
        gvMngLeaveRqstList.DataBind();
    }

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnReqid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[0]).Trim();

            Response.Redirect("ExitProcess_ClearanceApproveForm.aspx?resignationid=" + hdnReqid.Value + "&Type=Add");

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
}