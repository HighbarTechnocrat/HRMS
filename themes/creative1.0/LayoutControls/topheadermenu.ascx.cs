using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Data;
using System.Configuration;
using System.Web.Security;

public partial class Themes_FirstTheme_LayoutControls_topheadermenu : System.Web.UI.UserControl
{
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {

        WebClient client_mobile = new WebClient();
        //Stream stream_mobile = client_mobile.OpenRead(ConfigurationManager.AppSettings["adminsitepath"]+ "Menu/mobilemenu.html");
        Stream stream_mobile = client_mobile.OpenRead(ConfigurationManager.AppSettings["adminsitepath"]+ "Menu/menu.html");
        StreamReader sr_mobile = new StreamReader(stream_mobile);
        string content_mobile = sr_mobile.ReadToEnd();
        mobile.InnerHtml = content_mobile;
        carttotal();
        if (!IsPostBack)
        {
            loadorder();
        }
    }

    protected void loadorder()
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            DataTable user = classreviews.getuseridbyemail(Page.User.Identity.Name);
            if (user.Rows.Count > 0)
            {
                lnkprofile.HRef = ReturnUrl("sitepathmain") + "user/" + user.Rows[0]["indexid"].ToString();
            }
        }
        DataTable dtp = classpkg.getcountorderdetailbyemaild(Page.User.Identity.Name);
        if (dtp.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtp.Rows[0]["orderid"].ToString()) == 0)
            {

                lihistory.Visible = false;
            }
        }
        else
        {

            lihistory.Visible = true;
        }


    }

    protected void carttotal()
    {
        string username = string.Empty;
        string m_flag = string.Empty;
        if (Page.User.Identity.IsAuthenticated)
        {
            username = Page.User.Identity.Name.ToString().Trim();
            DataTable dtadd = classaddress.getuserinfodetails(username);
            string strfname = "";
            if (dtadd.Rows.Count > 0)
            {
                strfname = dtadd.Rows[0]["firstname"].ToString();
            }
            else
            {
                strfname = "Guest";
            }
         
            lblfirstname.Text = strfname;
          

            m_flag = "U";
        }
        //else
        //{
        //    username = getRemoteAddr();
        //    m_flag = "P";
        //}

    }
    protected void btnSingOut_Click(object sender, EventArgs e)
    {

        Session.Clear();
        Session.Abandon();
        Request.Cookies.Clear();
        FormsAuthentication.SignOut();
        Response.Redirect(ReturnUrl("sitepathmain") + "default", true);
    }


    protected void btnsearch_Click(object sender, EventArgs e)
    {
        string strurl = "";
        if (txtsearch.Text.ToString().Trim() != "")
        {
            ExecuteSearch(txtsearch.Text.ToString().Trim());
            strurl = UrlRewritingVM.getUrlRewritingInfo(txtsearch.Text.ToString().Trim(), "", "SA");
            Response.Redirect(strurl);
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "default");
        }
    }

    private void ExecuteSearch(string strsearchTextBox)
    {
        string sitepath = creativeconfiguration.SitePath;
        strsearchTextBox = HttpUtility.UrlDecode(strsearchTextBox);
        if (txtsearch.Text.Trim() != "")
        {
            updatecount(strsearchTextBox);
        }
    }

    private void updatecount(string strname)
    {
        string strcatsessionsdecode = HttpUtility.UrlDecode(strname);
        DataSet ds = classkeyword.getsearchlistBySearchbyname(strcatsessionsdecode);
        if (ds.Tables[0].Rows.Count > 0)
        {
            int keyid = Convert.ToInt32(ds.Tables[0].Rows[0]["keywordid"]);
            int freq = Convert.ToInt32(ds.Tables[0].Rows[0]["keyordfrequency"]);
            freq = freq + 1;
            bool flag1 = classkeyword.updatekeywordfrequency(Convert.ToDecimal(keyid), Convert.ToDecimal(freq));
        }
        else
        {
            bool flag = classkeyword.createkeyword(strcatsessionsdecode, 1, "", 'S', Page.User.Identity.ToString());
        }
    }

   
}