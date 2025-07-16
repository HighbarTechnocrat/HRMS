<%@ Application Language="C#"  %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Import Namespace="System.Security.Principal" %>
<%@ Import Namespace="System.Security.Cryptography" %>
<script runat="server">
    
    void Application_BeginRequest(object sender, EventArgs e)
    {


      Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddHours(-1));
        Response.Cache.SetNoStore();
        
        //HttpCompress((HttpApplication)sender);
        String fullOrigionalpath = Request.Url.ToString();
        
                //added for Bad URL by Iqbal
        //if (fullOrigionalpath.Contains(".aspx"))
        //{
        //    string turl = fullOrigionalpath.Substring(fullOrigionalpath.IndexOf(".aspx"));
        //    Match match = Regex.Match(turl, @"(.*).aspx/(.+)$", RegexOptions.IgnoreCase);      
        //    //if (match.Success)                
        //////added for Bad URL by Iqbal 23aug2017 REPLACED ABOVE IF CODE WITH THIS BELOW NEW CODE.

        //    if (!fullOrigionalpath.Contains("settings.aspx/UpdateSettings") && !fullOrigionalpath.Contains("manageservice.aspx/GetReminders") && !fullOrigionalpath.Contains("MyJsonResponse.ashx?id=") && match.Success)
        //    {
        //        throw new HttpException(404, "Not found");
        //    }
        //}
        
        String[] sElements = fullOrigionalpath.Split('/');
        String[] sFilePath = sElements[sElements.Length - 1].Split('.');
        string lastCharacter = fullOrigionalpath.Substring(fullOrigionalpath.Length - 1);
        
            if (!fullOrigionalpath.Contains(".aspx") && sFilePath.Length == 1)
            {
                if (!string.IsNullOrEmpty(sFilePath[0].Trim()))
                {
                    if (sFilePath[0].Contains(UrlRewritingVM.ChangeURL("sitepath")))
                    {
                        sFilePath[0] = sFilePath[0] + "/";
                    }
                    Context.RewritePath(sFilePath[0] + ".aspx");
                }
                //else
                //{
                //    Context.RewritePath(sFilePath[0] + "default.aspx");
                //}
            }

            if (fullOrigionalpath.Contains(".html"))
            {
                if (!fullOrigionalpath.Contains(".html") && sFilePath.Length == 1)
                {
                    string sp = fullOrigionalpath.Substring(fullOrigionalpath.LastIndexOf('/') + 1);
                    if (sp != "")
                    {
                        if (!string.IsNullOrEmpty(sFilePath[0].Trim()))
                        {
                            if (sFilePath[0].Contains(UrlRewritingVM.ChangeURL("sitepath")))
                            {
                                sFilePath[0] = sFilePath[0] + "/";
                            }
                            Context.RewritePath(sFilePath[0] + ".html");
                        }
                        else
                        {
                            Context.RewritePath(sFilePath[0] + "default.aspx");
                        }
                    }
                }
            }
        
       // Code that runs on application startup
       if (!fullOrigionalpath.EndsWith("/"))
        {
            string sp = fullOrigionalpath.Substring(fullOrigionalpath.LastIndexOf('/') + 1);
            if (sp != "")
            {
                System.Data.DataTable dtcat = clscms.searchnewsbyname(sp);
                if (dtcat.Rows.Count > 0)
                {
                    HttpContext.Current.RewritePath("~/footer.aspx?catname=" + sp);
                }
               
                //if (sp.ToLower().ToString().Contains("rss"))
                //{
                   
                //        HttpContext.Current.RewritePath("~/rss.aspx");
                   
                //}
                //if (sp.ToLower().ToString().Contains("contactus"))
                //{

                //    HttpContext.Current.RewritePath("~/contactus.aspx");

                //}

            }           
       }


       //if (fullOrigionalpath.Contains("/w/"))
       //{
       //    string[] separateURL = fullOrigionalpath.Split('/');
       //    string sp = separateURL[5];
       //    System.Data.DataTable dtcats = clscms.searchPSPDbyname(sp, "w");
       //    if (dtcats.Rows.Count > 0)
       //    {
       //        HttpContext.Current.RewritePath("~/watch.aspx?p=" + dtcats.Rows[0]["productid"].ToString());
       //    }
       //}


       //Check If it is a new session or not , if not then do the further checks
       //if (Request.Cookies["ASP.NET_SessionId"] != null && Request.Cookies["ASP.NET_SessionId"].Value != null)
       //{
       //    string newSessionID = Request.Cookies["ASP.NET_SessionID"].Value;
       //    //Check the valid length of your Generated Session ID
       //    if (newSessionID.Length <= 24)
       //    {
       //        //Log the attack details here
       //        Response.Cookies["TriedTohack"].Value = "True";
       //       // throw new HttpException("Invalid Request");
       //    }

       //    //Genrate Hash key for this User,Browser and machine and match with the Entered NewSessionID
       //    if (GenerateHashKey() != newSessionID.Substring(24))
       //    {
       //        //Log the attack details here
       //        Response.Cookies["TriedTohack"].Value = "True";
       //       // throw new HttpException("Invalid Request");
       //    }

       //    //Use the default one so application will work as usual//ASP.NET_SessionId
       //    Request.Cookies["ASP.NET_SessionId"].Value = Request.Cookies["ASP.NET_SessionId"].Value.Substring(0, 24);
       //}

      
    }
    
    void Application_EndRequest(object sender, EventArgs e) 
    {
        //Pass the custom Session ID to the browser.
        //if (Response.Cookies["ASP.NET_SessionId"] != null)
        //{
        //    Response.Cookies["ASP.NET_SessionId"].Value = Request.Cookies["ASP.NET_SessionId"].Value + GenerateHashKey();
        //}
    }
    
    void Application_Start(object sender, EventArgs e) 
    {
        Application["NoOfVisitors"] =0;
        
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs
        Exception ex = new Exception();
        Response.Write(ex.Message);
        
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
        Application.Lock();
        Application["NoOfVisitors"] = (int)Application["NoOfVisitors"] + 1;
        
        Session["Empcode"] = "";
        Application.UnLock();
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
    void Application_AuthenticateRequest(object sender, EventArgs e)
    {
         
        
        
    }
    //private string GenerateHashKey()
    //{
    //    StringBuilder myStr = new StringBuilder();
    //    myStr.Append(Request.Browser.Browser);
    //    myStr.Append(Request.Browser.Platform);
    //    myStr.Append(Request.Browser.MajorVersion);
    //    myStr.Append(Request.Browser.MinorVersion);
    //    //myStr.Append(Request.LogonUserIdentity.User.Value);
    //    SHA1 sha = new SHA1CryptoServiceProvider();
    //    byte[] hashdata = sha.ComputeHash(Encoding.UTF8.GetBytes(myStr.ToString()));
    //    return Convert.ToBase64String(hashdata);
    //}
    //private void HttpCompress(HttpApplication app)
    //{
    //    try
    //    {
    //        string accept = app.Request.Headers["Accept-Encoding"];
    //        if (accept != null && accept.Length > 0)
    //        {
    //            if (CompressScript(Request.ServerVariables["SCRIPT_NAME"]))
    //            {
    //                System.IO.Stream stream = app.Response.Filter;
    //                accept = accept.ToLower();
    //                if (accept.Contains("gzip"))
    //                {
    //                    app.Response.Filter = new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Compress);
    //                    app.Response.AppendHeader("Content-Encoding", "gzip");
    //                }
    //                else if (accept.Contains("deflate"))
    //                {
    //                    app.Response.Filter = new System.IO.Compression.DeflateStream(stream, System.IO.Compression.CompressionMode.Compress);
    //                    app.Response.AppendHeader("Content-Encoding", "deflate");
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //handle the exception
    //    }
    //}
    //private bool CompressScript(string scriptName)
    //{
    //    if (scriptName.ToLower().Contains(".aspx")) return true;
    //    if (scriptName.ToLower().Contains(".axd")) return false;
    //    if (scriptName.ToLower().Contains(".js")) return false;
    //    return true;
    //}
</script>
