using System;

using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for RegExUrlMappingModule
/// </summary>
public class RegExUrlMappingModule : RegExUrlMappingBaseModule
{
    public RegExUrlMappingModule()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public override void Rewrite(string requestedPath, HttpApplication app)
    {
        RegExUrlMappingConfigHandler config = ((RegExUrlMappingConfigHandler)(ConfigurationManager.GetSection("system.web/RegExUrlMapping")));
        string pathOld;
        string pathNew = "a";
        if (config.Enabled())
        {
            pathOld = app.Request.RawUrl;
            string requestedPage = app.Request.RawUrl;
            if (requestedPage.IndexOf("?") > -1)
            {
                requestedPage = requestedPage.Substring(0, requestedPage.IndexOf("?"));
            }
            string appVirtualPath = app.Request.ApplicationPath;
            if (requestedPage.Length >= appVirtualPath.Length)
            {
                if (requestedPage.Substring(0, appVirtualPath.Length) == appVirtualPath)
                {
                    requestedPage = requestedPage.Substring(appVirtualPath.Length);
                    if (requestedPage.Length > 1)
                    {
                        if (requestedPage.Substring(0, 1) == "/")
                        {
                            requestedPage = "~" + requestedPage;
                            //if (requestedPage.IndexOf(".", 2) > -1)
                            //{
                            //    pathNew = "";
                            //}
                        }
                        else
                        {
                            requestedPage = "~/" + requestedPage;
                        }
                    }
                }
            }
            if (pathNew == "")
            {
                goto Found;
            }
            else
            {
                pathNew = config.MappedUrl(requestedPage.Trim());
                
            }

        Found:
            if (pathNew.Length > 0)
            {
                if (pathNew.IndexOf("?") > -1)
                {
                    if (pathOld.IndexOf("?") > -1)
                    {
                        pathNew += "&" + Right(pathOld, pathOld.Length - pathOld.IndexOf("?") - 1);
                    }
                    else
                    {
                        if (pathOld.IndexOf("?") > -1)
                        {
                            pathNew += Right(pathOld, pathOld.Length - pathOld.IndexOf("?"));
                        }
                    }
                }
             
                HttpContext.Current.RewritePath(pathNew);
                //ProcessUrl(pathNew);
                //ProcessUrl1(pathNew);
            }
        }
   }
    public string Right(string Original, int Count)
    {
        // same thing as above.
        if (Original == null || Original == string.Empty || Original.Length < Count)
        {
            return Original;
        }
        else
        {
            // blah blah blah
            return Original.Substring(Original.Length - Count);
        }
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
        //newUrl = newUrl;
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
       // newUrl = newUrl.ToLower();
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
