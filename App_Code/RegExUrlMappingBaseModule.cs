using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for RegExUrlMappingBaseModule
/// </summary>
public class RegExUrlMappingBaseModule : System.Web.IHttpModule
{
    public RegExUrlMappingBaseModule()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public virtual void Init(HttpApplication app)
    {
        app.AuthorizeRequest += new EventHandler(this.BaseModuleRewriter_AuthorizeRequest);
    }

    public virtual void Dispose()
    {

    }
    void BaseModuleRewriter_AuthorizeRequest(object sender, EventArgs e)
    {
        HttpApplication app = ((HttpApplication)(sender));
        Rewrite(app.Request.Path, app);
    }
    public virtual void Rewrite(string requestedPath, HttpApplication app)
    {

    }
    public string ProcessUrl(string url)
    {
        string newUrl = String.Empty;
        char[] charUrl = url.ToCharArray();
        //remove unwanted characters and return only alphabet letters of numeric digits
        foreach (char c in charUrl)
        {
            if (char.IsLetterOrDigit(c) || char.IsSeparator(c) || c.ToString() == "-")
                newUrl += c.ToString();
        }
        //convert URL to lowercase
        //newUrl = newUrl.ToLower();
        //replace spaces with hyphens(-)
        newUrl = newUrl.Replace(" ", "-");
        //return new url
        return newUrl;
    }
    public string ProcessUrl1(string url)
    {
        string newUrl = String.Empty;
        char[] charUrl = url.ToCharArray();
        //remove unwanted characters and return only alphabet letters of numeric digits
        foreach (char c in charUrl)
        {
            if (char.IsLetterOrDigit(c) || char.IsSeparator(c) || c.ToString() == "-")
                newUrl += c.ToString();
        }
        //convert URL to lowercase
        //newUrl = newUrl.ToLower();
        //replace spaces with hyphens(-)
        newUrl = newUrl.Replace("          ", " ");
        newUrl = newUrl.Replace("         ", " ");
        newUrl = newUrl.Replace("        ", " ");
        newUrl = newUrl.Replace("       ", " ");
        newUrl = newUrl.Replace("      ", " ");
        newUrl = newUrl.Replace("     ", " ");
        newUrl = newUrl.Replace("    ", " ");
        newUrl = newUrl.Replace("   ", " ");
        newUrl = newUrl.Replace("  ", " ");
        newUrl = newUrl.Replace(" ", "-");
        //return new url
        return newUrl;
    }
}
