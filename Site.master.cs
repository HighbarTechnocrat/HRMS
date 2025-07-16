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

public partial class SiteMaster : System.Web.UI.MasterPage
{
    public static int categoryId,ID;
    public static string topcatid, strNamelevelOne = "", strNamelevelTwo = "", strNamelevelThree = "", strdesc = "";
    public string sitepath = creativeconfiguration.SitePath;
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
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
        Getmetatag();

        WebClient client = new WebClient();
        //Stream stream = client.OpenRead(ConfigurationManager.AppSettings["adminsitepath"]+ "Menu/footer.html");
        Stream stream = client.OpenRead(ConfigurationManager.AppSettings["sitepathadmin"]+ "Menu/footer.html");
        StreamReader sr = new StreamReader(stream);
        string content = sr.ReadToEnd();
        cms1.Text = content;
    }
    private void Getmetatag()
    {
        HtmlHead head = (HtmlHead)Page.Header;
        HtmlMeta tag = new HtmlMeta();
        HtmlMeta tag1 = new HtmlMeta();
        string keyword;
        string description;
       
       if (Request.RawUrl.Contains("contactus"))
        {
            tag.Name = "description";
            tag.Content = "Contact us - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";
            tag1.Content = "Contact us - highbartech.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Contact us - highbartech.com ";
        }
        else if (Request.RawUrl.Contains("aboutus"))
        {
            tag.Name = "description";
            tag.Content = "About us - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "About us - highbartech.com";
            this.metakeyword.Controls.Add(tag1);

            Page.Title = "About us - highbartech.com ";
        }
        else if (Request.RawUrl.Contains("termscondition"))
        {
            tag.Name = "description";
            tag.Content = "Terms and Conditions - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Terms and Conditions - highbartech.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Terms and Conditions - highbartech.com ";
        }
        else if (Request.RawUrl.Contains("privacypolicy"))
        {
            tag.Name = "description";
            tag.Content = "Privacy Policy - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Privacy Policy - highbartech.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Privacy Policy - highbartech.com ";
        }
        else if (Request.RawUrl.Contains("rss"))
        {
            tag.Name = "description";
            tag.Content = "If you are searching about free Hindi movies online in India & Singapore then you have come to the right place. For more details visit us or call us at +65 98218080.";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "RSS - highbartech.com";
            this.metakeyword.Controls.Add(tag1);

            Page.Title = "RSS Feed, Free Hindi Movies Online Singapore, highbartech.com ";
        }
        else if (Request.RawUrl.Contains("editprofile"))
        {

            tag.Name = "description";
            tag.Content = "Edit Profile - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Edit Profile - highbartech.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Edit Profile - highbartech.com ";
        }
        else if (Request.RawUrl.Contains("changepassword"))
        {

            tag.Name = "description";
            tag.Content = "Change Password - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Change Password - highbartech.com";
            this.metakeyword.Controls.Add(tag1);

            Page.Title = "Change Password - highbartech.com ";
        }
        else if ( Request.RawUrl.Contains("pthistory") )
        {

	        tag.Name = "description";
	        tag.Content = "Point History - highbartech.com";
	        this.metadescription.Controls.Add(tag);
	        tag1.Name = "keywords";

	        tag1.Content = "Point History - highbartech.com";
	        this.metakeyword.Controls.Add(tag1);
	        Page.Title = "Point History - highbartech.com ";
        }
        else if (Request.RawUrl.Contains("shoppingcart"))
        {
            tag.Name = "description";
            tag.Content = "Shopping Cart - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Shopping Cart - highbartech.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Shopping Cart - highbartech.com ";
        }
        else if (Request.RawUrl.Contains("login"))
        {
            tag.Name = "description";
            tag.Content = "Login - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Login - highbartech.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Login - highbartech.com ";
        }
        else if (Request.RawUrl.Contains("postenquiry"))
        {
            tag.Name = "description";
            tag.Content = "Post Enquiry - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Post Enquiry - highbartech.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Post Enquiry - highbartech.com ";
        }
        
      
        else if (Request.RawUrl.Contains("wishlist"))
        {
            tag.Name = "description";
            tag.Content = "Favorites - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Favorites - highbartech.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Favorites - highbartech.com ";
        }
        else if (Request.RawUrl.Contains("generatesitemap"))
        {
            tag.Name = "description";
            tag.Content = "Generate XML - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Generate XML - highbartech.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Generate XML - highbartech.com";
        }
        else if (Request.RawUrl.Contains("checkout"))
        {
            tag.Name = "description";
            tag.Content = "Checkout - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Checkout - highbartech.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Checkout - highbartech.com";
        }

        else if (Request.RawUrl.Contains("donation"))
        {

            tag.Name = "description";
            tag.Content = "Donation - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Donation - highbartech.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Donation - highbartech.com";
           
        }

        else if (Request.RawUrl.Contains("director"))
        {

            tag.Name = "description";
            tag.Content = "Director - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Director - highbartech.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Director - highbartech.com";

        }

        else if (Request.RawUrl.Contains("subscribe"))
        {
            
            tag.Name = "description";
            tag.Content = "Subscribe - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Subscribe - highbartech.com";
            this.metakeyword.Controls.Add(tag1);

            Page.Title = "Subscribe - highbartech.com ";
        }

        else if (Request.RawUrl.Contains("summary"))
        {
           
            tag.Name = "description";
            tag.Content = "Summary - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Summary - highbartech.com";
            this.metakeyword.Controls.Add(tag1);

            Page.Title = "Summary - highbartech.com ";
        }

        else if (Request.RawUrl.Contains("Response"))
        {

            tag.Name = "description";
            tag.Content = "Response - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Response - highbartech.com";
            this.metakeyword.Controls.Add(tag1);

            Page.Title = "Response - highbartech.com ";
        }
       else if (Request.RawUrl.Contains("addpost"))
       {

           tag.Name = "description";
           tag.Content = "Add Post - highbartech.com";
           this.metadescription.Controls.Add(tag);
           tag1.Name = "keywords";

           tag1.Content = "Add Post - highbartech.com";
           this.metakeyword.Controls.Add(tag1);

           Page.Title = "Add Post - highbartech.com ";
       }
        else if (Request.RawUrl.Contains("ad"))
        {
           
            tag.Name = "description";
            tag.Content = "Director - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Director - highbartech.com";
            this.metakeyword.Controls.Add(tag1);

            Page.Title = "Director - highbartech.com ";
        }

        else if (Request.RawUrl.Contains("subscriptionhistory"))
        {

            tag.Name = "description";
            tag.Content = "Subscriptionhistory - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Subscriptionhistory - highbartech.com";
            this.metakeyword.Controls.Add(tag1);

            Page.Title = "Subscriptionhistory - highbartech.com ";
        }

        else if (Request.RawUrl.Contains("cancelresponse"))
        {

            tag.Name = "description";
            tag.Content = "Cancelresponse - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Cancelresponse - highbartech.com";
            this.metakeyword.Controls.Add(tag1);

            Page.Title = "Cancelresponse - highbartech.com ";
        }

        else if (Request.RawUrl.Contains("preference"))
        {

            tag.Name = "description";
            tag.Content = "Preference - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Preference - highbartech.com";
            this.metakeyword.Controls.Add(tag1);

            Page.Title = "Preference - highbartech.com ";
        }

        else if (Request.RawUrl.Contains("Response"))
        {

            tag.Name = "description";
            tag.Content = "Thank You - highbartech.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Thank You - highbartech.com";
            this.metakeyword.Controls.Add(tag1);

            Page.Title = "Thank You - highbartech.com ";

        }
       
    }

    private int getTopID()
    {
        int topid = 0;
        if (Request.RawUrl.Contains("ps"))
        {
            if (Int32.TryParse(Request.QueryString["c"], out ID))
            {
                if (Request.QueryString["c"] != "" && Request.QueryString["c"] != null)
                {
                    topid = Convert.ToInt32(Request.QueryString["c"].ToString());
                }
                else
                {
                    topid = 0;
                }
            }
        }
        else
        {
            if (Int32.TryParse(Request.QueryString["p"], out ID))
            {
                if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
                {
                    topid = Convert.ToInt32(Request.QueryString["p"].ToString());
                }
                else
                {
                    topid = 0;
                }
            }
           
        }
        return topid;
    }

}
