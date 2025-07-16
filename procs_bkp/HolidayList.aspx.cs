using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;


public partial class procs_HolidayList : System.Web.UI.Page
{
    DataTable dtdocument;
    SP_Methods spm = new SP_Methods();
    int DocID;
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
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
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }
    }
    public void LoadData()
    {
        dtdocument = spm.GetHolidayList(Convert.ToString(Session["Empcode"]).Trim());
        dghbtHoliday.DataSource = null;
        dghbtHoliday.DataBind();
        if (dtdocument.Rows.Count > 0)
        {
            lblheading.Text = "Holiday List For "+Convert.ToString(dtdocument.Rows[0]["LocatioName"]);
            dghbtHoliday.DataSource = dtdocument;
            dghbtHoliday.DataBind();
        }
    }
}