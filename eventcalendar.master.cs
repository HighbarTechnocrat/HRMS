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
using System.Globalization;
using System.Text.RegularExpressions;


public partial class home : System.Web.UI.MasterPage
{
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.User.Identity.IsAuthenticated)
        {
            Response.Redirect(ReturnUrl("sitepath") + "login.aspx?ReturnUrl=" + Request.RawUrl);
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

        WebClient client = new WebClient();
       /* add by krishna*/
        Stream stream = client.OpenRead(ConfigurationManager.AppSettings["sitepathadmin"]+ "Menu/footer.html");

        StreamReader sr = new StreamReader(stream);
        string content = sr.ReadToEnd();
        cms1.Text = content;

    }
    public void metatag()
    {
        HtmlHead head = (HtmlHead)Page.Header;
        HtmlMeta tag = new HtmlMeta();
        HtmlMeta tag1 = new HtmlMeta();
     
      
          if (Request.RawUrl.Contains("default"))
          {
            //  Page.Title = "Intranet";
              Page.Title = "Chhatrapati Shivaji International Airport";
              tag.Name = "description";
              tag.Content = "Intranet.com";
              this.metadescription.Controls.Add(tag);
              tag1.Name = "keywords";

              tag1.Content = "Intranet.com";
              this.metakeyword.Controls.Add(tag1);
          }

          else if (Request.RawUrl.Contains("recommend"))
          {
              //Page.Title = "Recommend-Intranet";
              Page.Title = "Recommend-Chhatrapati Shivaji International Airport";
              tag.Name = "description";
              tag.Content = "Recommend-Intranet.com";
              this.metadescription.Controls.Add(tag);
              tag1.Name = "keywords";

              tag1.Content = "Recommend-Intranet.com";
              this.metakeyword.Controls.Add(tag1);
          }
          else
          {
              //Page.Title = "Intranet";
              Page.Title = "Chhatrapati Shivaji International Airport";
              tag.Name = "description";
              tag.Content = "Intranet.com";
              this.metadescription.Controls.Add(tag);
              tag1.Name = "keywords";

              tag1.Content = "Intranet.com";
              this.metakeyword.Controls.Add(tag1);
          }
    }
}
