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

public partial class Req_Vendor_Details : System.Web.UI.Page
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
    #region PageEvents
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
                // Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    GetReq_VendorList();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnVenderID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
            Response.Redirect("Req_Vendor_Create.aspx?Vendor_ID=" + hdnVenderID.Value);
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    #endregion
    #region PageMethods
    private void GetReq_VendorList()
    {
        try
        {
            // DataSet dsList = new DataSet();
            DataTable QuestionnaireList = new DataTable();
            QuestionnaireList = spm.Get_Rec_VendorDetails();
            gvMngTravelRqstList.DataSource = null;
            gvMngTravelRqstList.DataBind();
            if (QuestionnaireList.Rows.Count > 0)
            {
                gvMngTravelRqstList.DataSource = QuestionnaireList;
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
		gvMngTravelRqstList.PageIndex = e.NewPageIndex;
		GetReq_VendorList();
	}
}