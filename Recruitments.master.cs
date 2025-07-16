using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Net;
using System.IO;
using System.Configuration;
using System.Web.Security;

public partial class Recruitments : System.Web.UI.MasterPage
{
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            if ((Request.UrlReferrer == null || Request.Url.Host != Request.UrlReferrer.Host))
            {
                //Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
                if (!Page.User.Identity.IsAuthenticated)
                {
                    if (Request.Url.AbsoluteUri.Contains("intranet.highbartech.com"))
                    {
                        Response.Redirect(ConfigurationManager.AppSettings["internetURL"]);
                    }
                    else if (Request.Url.AbsoluteUri.Contains("localhost"))
                    //else if (Request.Url.AbsoluteUri.Contains("HRMS_ADMIN"))
                    {
                        Response.Redirect(ConfigurationManager.AppSettings["intranetURL"]);
                    }
                    else
                    {
                        //Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
                        Response.Redirect(ReturnUrl("sitepathmain") + "sessionendR.aspx");
                    }
                }
            }
        }
        if (Request.Url.AbsoluteUri.Contains("intranet.highbartech.com"))
        //if(Request.Url.AbsoluteUri.Contains("192.168.0.172/hrms/"))
        {
            if (!Page.User.Identity.IsAuthenticated)
            {
                //     Response.Redirect("http://192.168.0.172/hrms/login.aspx");
                Response.Redirect(ConfigurationManager.AppSettings["internetURL"]);
            }
        }
        //else if(Request.Url.AbsoluteUri.Contains("HRMS_ADMIN"))
        else if (Request.Url.AbsoluteUri.Contains("172.18.37.5"))
        //else if (Request.Url.AbsoluteUri.Contains("192.168.0.172/HRMS_ADMIN/"))
        {
            if (!Page.User.Identity.IsAuthenticated)
            {
                //Response.Redirect("http://192.168.0.172/HRMS_ADMIN/login.aspx");
                Response.Redirect(ConfigurationManager.AppSettings["intranetURL"]);
            }
        }
        else
        {
            //Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
            Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
        }

        //form1.Action = Request.RawUrl;

	string pageName = Path.GetFileName(Request.Path);
		string HPath = "http://localhost/hrms/procs/";
		if (Convert.ToString(Session["EmpStatus"]).Trim() == "Resigned")
		{
			Response.Redirect(HPath + "ExitProcess_Index.aspx");
		}
		else
		{
			form1.Action = Request.RawUrl;
		}
	metatag();
    }
    public void metatag()
    {
        HtmlHead head = (HtmlHead)Page.Header;
        HtmlMeta tag = new HtmlMeta();
        HtmlMeta tag1 = new HtmlMeta();


        if (Request.RawUrl.Contains("default"))
        {
            //  Page.Title = "Intranet";
            Page.Title = "HRMS";
            tag.Name = "description";
            tag.Content = "highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "highbartech.com";
            this.metakeyword.Controls.Add(tag1);
        }

        else if (Request.RawUrl.Contains("recommend"))
        {
            //Page.Title = "Recommend-Intranet";
            Page.Title = "Recommend-HRMS";
            tag.Name = "description";
            tag.Content = "Recommend-highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Recommend-highbartech.com";
            this.metakeyword.Controls.Add(tag1);
        }
        else
        {
            //Page.Title = "Intranet";
            Page.Title = "HRMS";
            tag.Name = "description";
            tag.Content = "highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "highbartech.com";
            this.metakeyword.Controls.Add(tag1);
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        //First, check for the existence of the Anti-XSS cookie
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        Guid requestCookieGuidValue;

        //If the CSRF cookie is found, parse the token from the cookie.
        //Then, set the global page variable and view state user
        //key. The global variable will be used to validate that it matches 
        //in the view state form field in the Page.PreLoad method.
        if (requestCookie != null
            && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            //Set the global token variable so the cookie value can be
            //validated against the value in the view state form field in
            //the Page.PreLoad method.
            _antiXsrfTokenValue = requestCookie.Value;

            //Set the view state user key, which will be validated by the
            //framework during each request
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        //If the CSRF cookie is not found, then this is a new session.
        else
        {
            //Generate a new Anti-XSRF token
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");

            //Set the view state user key, which will be validated by the
            //framework during each request
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            //Create the non-persistent CSRF cookie
            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                //Set the HttpOnly property to prevent the cookie from
                //being accessed by client side script
                HttpOnly = true,

                //Add the Anti-XSRF token to the cookie value
                Value = _antiXsrfTokenValue
            };

            //If we are using SSL, the cookie should be set to secure to
            //prevent it from being sent over HTTP connections
            if (FormsAuthentication.RequireSSL &&
                Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }

            //Add the CSRF cookie to the response
            Response.Cookies.Set(responseCookie);
        }

        Page.PreLoad += master_Page_PreLoad;
    }
    protected void master_Page_PreLoad(object sender, EventArgs e)
    {
        //During the initial page load, add the Anti-XSRF token and user
        //name to the ViewState
        if (!IsPostBack)
        {
            //Set Anti-XSRF token
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;

            //If a user name is assigned, set the user name
            ViewState[AntiXsrfUserNameKey] =
                   Page.User.Identity.Name ?? String.Empty;
        }
        //During all subsequent post backs to the page, the token value from
        //the cookie should be validated against the token in the view state
        //form field. Additionally user name should be compared to the
        //authenticated users name
        else
        {
            //Validate the Anti-XSRF token
            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] !=
                     (Page.User.Identity.Name ?? String.Empty))
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionendR.aspx");
                //throw new InvalidOperationException("Validation of " +
                //                    "Anti-XSRF token failed.");
            }
        }
    }
}
