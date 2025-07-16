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
using System.Web.Mail;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web.Services;
public partial class myaccount_editprofile : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1,username="";
    public string userid;
    public static string pimg = "";
    public static string cimg = "";

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    [WebMethod]
    public static string UpdateSettings(string id, string flag)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            username = HttpContext.Current.User.Identity.Name.ToString();
            Classuserwidget.updateUserwidget(Convert.ToInt32(id), username, flag);
        }
        return "success";
    }
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            username = Page.User.Identity.Name.ToString();

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/settings");
            }
            else
            {
                Page.SmartNavigation = true;
                loaduserwidget();
                editform.Visible = true;
                if (!Page.IsPostBack)
                {

                    divbtn.Visible = false;
                    divmsg.Visible = false;
                    this.Title = creativeconfiguration.SiteName + ": Edit Widget ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
    protected void insertwidget()
    {
        DataTable dtw = Classuserwidget.getwidget();
        if (dtw.Rows.Count > 0)
        {
            for (int j = 0; j < dtw.Rows.Count; j++)
            {
                int wid = Convert.ToInt32(dtw.Rows[j]["widget_id"]);
                Classuserwidget.insertUserwidget(wid, Page.User.Identity.Name.ToString(), "T");
            }
        }
    }    
    public void loadwidget()
    {
        DataTable dtw = Classuserwidget.getwidget();
        if(dtw.Rows.Count > 0)
        {
            rptwidget.DataSource = dtw;
            rptwidget.DataBind();
        }
    }
    public void loaduserwidget()
    {
        DataTable dtuw = Classuserwidget.getuserwidget(Page.User.Identity.Name.ToString());
        if(dtuw.Rows.Count > 0)
        {
            userwidget();
        }
        else
        {
            insertwidget();
            userwidget();
        }
    }
    public void userwidget()
    {
        DataTable dtuw = Classuserwidget.getuserwidget(Page.User.Identity.Name.ToString());
        if (dtuw.Rows.Count > 0)
        {
            rptwidget.DataSource = dtuw;
            rptwidget.DataBind();
            if (rptwidget.Items.Count > 0)
            {
                for (int i = 0; i < rptwidget.Items.Count; i++)
                {
                    Literal ltscript = (Literal)rptwidget.Items[i].FindControl("ltscript");
                    ltscript.Text = "<script type='text/javascript'>$(document).ready(function(){var cid='#MainContent_rptwidget_chkwidgetonoff_" + i + "';});new DG.OnOffSwitch({ el: '#MainContent_rptwidget_chkwidgetonoff_" + i + "', textOn: 'On', textOff: 'Off',listener:function(id, checked){ var id='#MainContent_rptwidget_chkwidgetonoff_" + i + "';if(checked==true){$(id).attr('checked',true);var hid='#MainContent_rptwidget_hfwid_" + i + "';$(hid).val('T');var wid='#MainContent_rptwidget_lblid_" + i + "';UpdateSettings($(wid).html(),'T');}else{$(id).attr('checked',false);var hid='#MainContent_rptwidget_hfwid_" + i + "';$(hid).val('F');var wid='#MainContent_rptwidget_lblid_" + i + "';UpdateSettings($(wid).html(),'F');}} });</script>";
                }
            }
        }
    }
    public bool getonoff(object flag)
    {
        bool status = true;
        try
        {
            if (flag.ToString().Trim() == "T")
            {
                status = true;
                return status;
            }
            else
            {
                status = false;
                return status;
            }
        }
        catch (Exception ex)
        {
            return status;
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if(Page.User.Identity.IsAuthenticated)
        {
            Classuserwidget.resetuserwidget(Page.User.Identity.Name.ToString());
            lblmessage.Visible = true;
            lblmessage.ForeColor = System.Drawing.Color.Green;
            lblmessage.Text = "Default Setting Reset";
            userwidget();
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/settings");
        }
    }
    protected void chkwidgetonoff_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < rptwidget.Items.Count; i++)
        {
            CheckBox chkwidgetonoff = (CheckBox)rptwidget.Items[i].FindControl("chkwidgetonoff");
            HiddenField hfwid = (HiddenField)rptwidget.Items[i].FindControl("hfwid");
            if (chkwidgetonoff.Checked == true)
            {
                Classuserwidget.updateUserwidget(Convert.ToInt32(hfwid.Value), Page.User.Identity.Name.ToString(), "T");
                lblmessage.Visible = true;
                lblmessage.ForeColor = System.Drawing.Color.Green;
                lblmessage.Text = "Widget setting saved successfully";
            }
            else
            {
                Classuserwidget.updateUserwidget(Convert.ToInt32(hfwid.Value), Page.User.Identity.Name.ToString(), "F");
                lblmessage.Visible = true;
                lblmessage.ForeColor = System.Drawing.Color.Green;
                lblmessage.Text = "Widget setting saved successfully";
            }
        }
    }
}
