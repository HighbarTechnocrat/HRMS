using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using HtmlAgilityPack;


public partial class pdnew : System.Web.UI.MasterPage
{
    public static int categoryId, ID;
    public static string topcatid, strNamelevelOne = "", strNamelevelTwo = "", strNamelevelThree = "", strdesc = "";
    public string sitepath = creativeconfiguration.SitePath;
    string strValue;
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
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
                    //else if (Request.Url.AbsoluteUri.Contains("HRMS_ADMIN"))
                    else if (Request.Url.AbsoluteUri.Contains("localhost"))
                    {
                        Response.Redirect(ConfigurationManager.AppSettings["intranetURL"]);
                    }
                    else
                    {
                        Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
                    }
                }
            }
        }
        try
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
            Getmetatag();

            WebClient client = new WebClient();
            //Stream stream = client.OpenRead(ConfigurationManager.AppSettings["sitepathadmin"]+ "Menu/footer.html");
            //StreamReader sr = new StreamReader(stream);
            //string content = sr.ReadToEnd();
            //SAGAR COMMENTED THIS FOR REMOVING FOOTER ON PAGE WHEN WE OPEN ONE POST OF PHOTO 12OCT2017
            //cms1.Text = content;
            //int p_id = Convert.ToInt32(Request.QueryString["p"].ToString());

            //sagar added below working code added for photogallery not working on client side format exceptions issue 19dec2017
            bool result;
            int p_id;
            result = Int32.TryParse(Request.QueryString["p"].ToString(), out p_id);
            
            getProductImg(p_id);
            getProductDesc(p_id);
            //lnklogo.HRef = ReturnUrl("sitepath");
            
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    public string getProductImg(object productid)
    {
        DataTable dt = classproduct.getproductdetails(Convert.ToInt32(productid));
        if (dt.Rows.Count > 0)
        {
            string strimg = Convert.ToString(dt.Rows[0]["bigimage"].ToString().Trim());

            if (strimg == "noimage2.gif")
            {
                DataTable moviebanner = classreviews.getmoviebannerimage(Request.RawUrl.ToString());
                if (moviebanner.Rows.Count > 0)
                {
                    strimg = moviebanner.Rows[0]["imagename"].ToString().Trim();
                    strValue = ConfigurationManager.AppSettings["sitepathadmin"]+ "images/moviebanner/" + strimg;
                }
            }
            else
            {
                strValue = ConfigurationManager.AppSettings["sitepathadmin"]+ "images/bigproduct/" + strimg;
            }
        }
        return strValue;
    }
    public string getProductTitle(object productid)
    {
        DataTable dt = classproduct.getproductdetails(Convert.ToInt32(productid));
        if (dt.Rows.Count > 0)
        {
            string strname = Convert.ToString(dt.Rows[0]["productname"]);
            // strValue = "I am watching " + strname + " on Intranet.com.";
            strValue = strname;
        }
        return strValue;
    }
    public string getFavicon()
    {
        strValue = ReturnUrl("sitepathmain")+"images/favicon.ico";
        return strValue;
    }
    public string getProductDesc(object productid)
    {
        try
        {
            string strdesc;
            DataTable dt = classproduct.getproductdetails(Convert.ToInt32(productid));
            if (dt.Rows.Count > 0)
            {
                HtmlDocument maindoc = new HtmlDocument();
                strdesc = Convert.ToString(dt.Rows[0]["shortdescription"]);
                if (strdesc.Length > 200)
                {
                    maindoc.LoadHtml(strdesc);
                    strdesc = maindoc.DocumentNode.InnerText.Substring(0, 200).ToLower() + "...";
                }
                else
                {
                    maindoc.LoadHtml(strdesc);
                    strdesc = maindoc.DocumentNode.InnerText;
                }
                // strValue = "I am watching " + strname + " on Intranet.com.";
                strValue = strdesc;
            }
        }
        catch(Exception ex)
        {

        }
        
        return strValue;
    }
    private void Getmetatag()
    {
        HtmlHead head = (HtmlHead)Page.Header;
        HtmlMeta tag = new HtmlMeta();
        HtmlMeta tag1 = new HtmlMeta();
        string keyword;
        string description;

        if (Request.RawUrl.Contains("ps"))
        {

            DataTable dt = classproduct.getmetacategorydetails(getTopID());
            if (dt.Rows.Count > 0)
            {

                tag.Name = "description";
                tag.Content = dt.Rows[0]["description"].ToString();
                this.metadescription.Controls.Add(tag);
                tag1.Name = "keywords";

                tag1.Content = dt.Rows[0]["keyword"].ToString();
                this.metakeyword.Controls.Add(tag1);

                if (dt.Rows[0]["title"].ToString().Trim() != "")
                {
                    Page.Title = dt.Rows[0]["title"].ToString();
                }
                else
                {
                    DataTable ds = classproduct.getcategorybyiddetails(getTopID());
                    if (ds.Rows.Count > 0)
                    {
                        Page.Title = ds.Rows[0]["categoryname"].ToString();
                    }
                }
            }
            else
            {
                if (getTopID() != 0)
                {
                    DataTable ds = classproduct.getcategorybyiddetails(getTopID());
                    if (ds.Rows.Count > 0)
                    {
                        Page.Title = ds.Rows[0]["categoryname"].ToString();
                    }
                }
                else
                {
                    Page.Title = "ALL";
                }
            }

        }
        else if (Request.RawUrl.Contains("pd"))
        {
            DataTable dt = classproduct.getmetaproductdetails(getTopID());
            if (dt.Rows.Count > 0)
            {
                tag.Name = "description";
                tag.Content = dt.Rows[0]["description"].ToString();
                this.metadescription.Controls.Add(tag);
                tag1.Name = "keywords";

                tag1.Content = dt.Rows[0]["keyword"].ToString();
                this.metakeyword.Controls.Add(tag1);
                if (dt.Rows[0]["title"].ToString().Trim() != "")
                {
                    Page.Title = dt.Rows[0]["title"].ToString();
                }
                else
                {
                    DataTable ds = classproduct.get_proc_ProductDescription_ProdFeature(getTopID());
                    if (ds.Rows.Count > 0)
                    {
                        Page.Title = ds.Rows[0]["productname"].ToString();
                    }
                }
            }
            else
            {
                DataTable ds = classproduct.get_proc_ProductDescription_ProdFeature(getTopID());
                if (ds.Rows.Count > 0)
                {
                    Page.Title = ds.Rows[0]["productname"].ToString();
                }

            }

        }
        else if (Request.RawUrl.Contains("search"))
        {
            if (Request.QueryString["s"] != "" && Request.QueryString["s"] != null)
            {
                string m_search = Convert.ToString(Request.QueryString["s"]);
                m_search = m_search.Replace("-", " ");
                tag.Name = "description";
                tag.Content = m_search;
                this.metadescription.Controls.Add(tag);
                tag1.Name = "keywords";

                tag1.Content = m_search;
                this.metakeyword.Controls.Add(tag1);
                Page.Title = m_search;
            }
        }

        else if (Request.RawUrl.Contains("contactus"))
        {
            tag.Name = "description";
            tag.Content = "Contact us - Intranet.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Contact us - Intranet.com";
            this.metakeyword.Controls.Add(tag1);

            Page.Title = "Contact us - Intranet.com ";
        }
        else if (Request.RawUrl.Contains("aboutus"))
        {
            tag.Name = "description";
            tag.Content = "About us - Intranet.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "About us - Intranet.com";
            this.metakeyword.Controls.Add(tag1);

            Page.Title = "About us - Intranet.com ";
        }
        else if (Request.RawUrl.Contains("termscondition"))
        {
            tag.Name = "description";
            tag.Content = "Terms and Conditions - Intranet.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Terms and Conditions - Intranet.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Terms and Conditions - Intranet.com ";
        }
        else if (Request.RawUrl.Contains("privacypolicy"))
        {
            tag.Name = "description";
            tag.Content = "Privacy Policy - Intranet.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Privacy Policy - Intranet.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Privacy Policy - Intranet.com ";
        }
        else if (Request.RawUrl.Contains("rss"))
        {
            tag.Name = "description";
            tag.Content = "RSS - Intranet.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "RSS - Intranet.com";
            this.metakeyword.Controls.Add(tag1);

            Page.Title = "RSS - Intranet.com ";
        }
        else if (Request.RawUrl.Contains("editprofile"))
        {

            tag.Name = "description";
            tag.Content = "Edit Profile - Intranet.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Edit Profile - Intranet.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Edit Profile - Intranet.com ";
        }
        else if (Request.RawUrl.Contains("changepassword"))
        {

            tag.Name = "description";
            tag.Content = "Change Password - Intranet.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Change Password - Intranet.com";
            this.metakeyword.Controls.Add(tag1);

            Page.Title = "Change Password - Intranet.com ";
        }
        else if (Request.RawUrl.Contains("shoppingcart"))
        {
            tag.Name = "description";
            tag.Content = "Shopping Cart - Intranet.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Shopping Cart - Intranet.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Shopping Cart - Intranet.com ";
        }
        else if (Request.RawUrl.Contains("login"))
        {
            tag.Name = "description";
            tag.Content = "Login - Intranet.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Login - Intranet.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Login - Intranet.com ";
        }
        else if (Request.RawUrl.Contains("postenquiry"))
        {
            tag.Name = "description";
            tag.Content = "Post Enquiry - Intranet.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Post Enquiry - Intranet.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Post Enquiry - Intranet.com ";
        }
        else if (Request.RawUrl.Contains("news"))
        {
            tag.Name = "description";
            tag.Content = "News - Intranet.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "News - Intranet.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "News - Intranet.com ";
        }
        else if (Request.RawUrl.Contains("testimonial"))
        {
            tag.Name = "description";
            tag.Content = "Testimonials - Intranet.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Testimonials - Intranet.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Testimonials - Intranet.com";
        }
        else if (Request.RawUrl.Contains("enquiry"))
        {
            tag.Name = "description";
            tag.Content = "Product Enquiry - Intranet.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Product Enquiry - Intranet.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Product Enquiry - Intranet.com";
        }
        else if (Request.RawUrl.Contains("bespoke"))
        {
            tag.Name = "description";
            tag.Content = "Bespoke - Intranet.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Bespoke - Intranet.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Bespoke - Intranet.com";
        }
        else if (Request.RawUrl.Contains("designinterlace"))
        {
            tag.Name = "description";
            tag.Content = "Design Interlace - Intranet.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Design Interlace - Intranet.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Design Interlace - Intranet.com";
        }
        else if (Request.RawUrl.Contains("wishlist"))
        {
            tag.Name = "description";
            tag.Content = "Wishlist - Intranet.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Wishlist - Intranet.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "Wishlist - Intranet.com";
        }
        else if (Request.RawUrl.Contains("ad"))
        {
            tag.Name = "description";
            tag.Content = "Director - Intranet.com";
            this.metadescription.Controls.Add(tag);
            tag1.Name = "keywords";

            tag1.Content = "Director - Intranet.com";
            this.metakeyword.Controls.Add(tag1);
            Page.Title = "director - Intranet.com";
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
