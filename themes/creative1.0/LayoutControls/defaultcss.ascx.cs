using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;

public partial class themes_secondtheme_LayoutControls_defaultcss : System.Web.UI.UserControl
{
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Page_Init(object sender, EventArgs e)
    {

        HtmlLink myHtmlLink = new HtmlLink();
        myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/boilerplate.css", ConfigurationManager.AppSettings["projectname"].ToString());
        myHtmlLink.Attributes.Add("rel", "stylesheet");
        myHtmlLink.Attributes.Add("type", "text/css");
        myHtmlLink.Attributes.Add("media", "all");
        Page.Header.Controls.Add(myHtmlLink);


        myHtmlLink = new HtmlLink();
        myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/layout.css", ConfigurationManager.AppSettings["projectname"].ToString());
        myHtmlLink.Attributes.Add("rel", "stylesheet");
        myHtmlLink.Attributes.Add("type", "text/css");
        myHtmlLink.Attributes.Add("media", "all");
        Page.Header.Controls.Add(myHtmlLink);

        myHtmlLink = new HtmlLink();
        myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/accountdropdown/accountdropdown.css", ConfigurationManager.AppSettings["projectname"].ToString());
        myHtmlLink.Attributes.Add("rel", "stylesheet");
        myHtmlLink.Attributes.Add("type", "text/css");
        myHtmlLink.Attributes.Add("media", "all");
        Page.Header.Controls.Add(myHtmlLink);

        myHtmlLink = new HtmlLink();
        myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/footerstyle.css", ConfigurationManager.AppSettings["projectname"].ToString());
        myHtmlLink.Attributes.Add("rel", "stylesheet");
        myHtmlLink.Attributes.Add("type", "text/css");
        myHtmlLink.Attributes.Add("media", "all");
        Page.Header.Controls.Add(myHtmlLink);


        myHtmlLink = new HtmlLink();
        myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/header/header.css", ConfigurationManager.AppSettings["projectname"].ToString());
        myHtmlLink.Attributes.Add("rel", "stylesheet");
        myHtmlLink.Attributes.Add("type", "text/css");
        myHtmlLink.Attributes.Add("media", "all");
        Page.Header.Controls.Add(myHtmlLink);

        myHtmlLink = new HtmlLink();
        myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/navigation/navi.css", ConfigurationManager.AppSettings["projectname"].ToString());
        myHtmlLink.Attributes.Add("rel", "stylesheet");
        myHtmlLink.Attributes.Add("type", "text/css");
        myHtmlLink.Attributes.Add("media", "all");
        Page.Header.Controls.Add(myHtmlLink);

        myHtmlLink = new HtmlLink();
        myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/navigation/mobile-navi-dd.css", ConfigurationManager.AppSettings["projectname"].ToString());
        myHtmlLink.Attributes.Add("rel", "stylesheet");
        myHtmlLink.Attributes.Add("type", "text/css");
        myHtmlLink.Attributes.Add("media", "all");
        Page.Header.Controls.Add(myHtmlLink);

        myHtmlLink = new HtmlLink();
        myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/search/search.css", ConfigurationManager.AppSettings["projectname"].ToString());
        myHtmlLink.Attributes.Add("rel", "stylesheet");
        myHtmlLink.Attributes.Add("type", "text/css");
        myHtmlLink.Attributes.Add("media", "all");
        Page.Header.Controls.Add(myHtmlLink);

        myHtmlLink = new HtmlLink();
        myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/sns/sns.css", ConfigurationManager.AppSettings["projectname"].ToString());
        myHtmlLink.Attributes.Add("rel", "stylesheet");
        myHtmlLink.Attributes.Add("type", "text/css");
        myHtmlLink.Attributes.Add("media", "all");
        Page.Header.Controls.Add(myHtmlLink);

        myHtmlLink = new HtmlLink();
        myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/scrollbar/scrollbar.css", ConfigurationManager.AppSettings["projectname"].ToString());
        myHtmlLink.Attributes.Add("rel", "stylesheet");
        myHtmlLink.Attributes.Add("type", "text/css");
        myHtmlLink.Attributes.Add("media", "all");
        Page.Header.Controls.Add(myHtmlLink);


        myHtmlLink = new HtmlLink();
        myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/promo/promom.css", ConfigurationManager.AppSettings["projectname"].ToString());
        myHtmlLink.Attributes.Add("rel", "stylesheet");
        myHtmlLink.Attributes.Add("type", "text/css");
        myHtmlLink.Attributes.Add("media", "all");
        Page.Header.Controls.Add(myHtmlLink);


        myHtmlLink = new HtmlLink();
        myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/loginpopup/loginpopup.css", ConfigurationManager.AppSettings["projectname"].ToString());
        myHtmlLink.Attributes.Add("rel", "stylesheet");
        myHtmlLink.Attributes.Add("type", "text/css");
        myHtmlLink.Attributes.Add("media", "all");
        Page.Header.Controls.Add(myHtmlLink);

        myHtmlLink = new HtmlLink();
        myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/fontawesome/font-awesome.css", ConfigurationManager.AppSettings["projectname"].ToString());
        myHtmlLink.Attributes.Add("rel", "stylesheet");
        myHtmlLink.Attributes.Add("type", "text/css");
        myHtmlLink.Attributes.Add("media", "all");
        Page.Header.Controls.Add(myHtmlLink);
		





        #region defaultcss
        if (Request.Url.AbsoluteUri.Contains("default"))
        {
	        myHtmlLink = new HtmlLink();
	        myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/banner/banner.css" , ConfigurationManager.AppSettings ["projectname"].ToString());
	        myHtmlLink.Attributes.Add("rel" , "stylesheet");
	        myHtmlLink.Attributes.Add("type" , "text/css");
            myHtmlLink.Attributes.Add("media", "all");
	        Page.Header.Controls.Add(myHtmlLink);

	   myHtmlLink = new HtmlLink();
	   myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/carousel/carousel.css" , ConfigurationManager.AppSettings ["projectname"].ToString());
	   myHtmlLink.Attributes.Add("rel" , "stylesheet");
	   myHtmlLink.Attributes.Add("type" , "text/css");
       myHtmlLink.Attributes.Add("media", "all");
	   Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/loginregister/loginregister.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/promo/promo.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);


        }
        #endregion
        #region productenquiry
        if (Request.RawUrl.ToString().Contains("productenquiry"))
        {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/commonpages/commonpages.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);
        }

        #endregion
        #region rsscss
        if (Request.RawUrl.ToString().Contains("rss"))
         {
            HtmlLink myHtmlLink2 = new HtmlLink();
            myHtmlLink2.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/rss/rss.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink2.Attributes.Add("rel", "stylesheet");
            myHtmlLink2.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink2);
         }
        #endregion
        
        #region ps
        if (Request.RawUrl.ToString().Contains("ps"))
        {
            if (Request.RawUrl.ToString().Contains("pd"))
            {
                myHtmlLink = new HtmlLink();
                myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/zoomerpopup/zoomer.css", ConfigurationManager.AppSettings["projectname"].ToString());
                myHtmlLink.Attributes.Add("rel", "stylesheet");
                myHtmlLink.Attributes.Add("type", "text/css");
                myHtmlLink.Attributes.Add("media", "all");
                Page.Header.Controls.Add(myHtmlLink);
            }

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/productsummerypopup/productsummery.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/productrow/productrow.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);


	   myHtmlLink = new HtmlLink();
	   myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/productfilter/productfilter.css" , ConfigurationManager.AppSettings ["projectname"].ToString());
	   myHtmlLink.Attributes.Add("rel" , "stylesheet");
	   myHtmlLink.Attributes.Add("type" , "text/css");
       myHtmlLink.Attributes.Add("media", "all");
	   Page.Header.Controls.Add(myHtmlLink);

	   myHtmlLink = new HtmlLink();
	   myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/productfilter/refinesearch.css" , ConfigurationManager.AppSettings ["projectname"].ToString());
	   myHtmlLink.Attributes.Add("rel" , "stylesheet");
	   myHtmlLink.Attributes.Add("type" , "text/css");
       myHtmlLink.Attributes.Add("media", "all");
	   Page.Header.Controls.Add(myHtmlLink);



	   myHtmlLink = new HtmlLink();
	   myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/productrow/productrow-search.css" , ConfigurationManager.AppSettings ["projectname"].ToString());
	   myHtmlLink.Attributes.Add("rel" , "stylesheet");
	   myHtmlLink.Attributes.Add("type" , "text/css");
       myHtmlLink.Attributes.Add("media", "all");
	   Page.Header.Controls.Add(myHtmlLink);

        }
        #endregion
        #region search
        if (Request.RawUrl.ToString().Contains("/search/"))
           {


            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/productrow/productrow.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/productrow/productrow-search.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/productfilter/productfilter.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/productfilter/refinesearch.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

           

           }
        #endregion

         string url1 = Request.Url.AbsoluteUri;

        
       
        #region pd
        //if (Request.RawUrl.ToString().Contains("pd"))
        //{
         if (url1.Contains("pd.aspx"))
         {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/productsummery/productsummery.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/zoomer/zoomer.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);
            

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/relatedproduct/relatedproduct.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

        }
        #endregion


        #region login
        if (Request.RawUrl.ToString().Contains("login"))
          {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/loginregister/loginregister.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/commonpages/commonpages.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

          }

        #endregion
        #region wishlist
        if (Request.RawUrl.ToString().Contains("wishlist"))
        {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/wishlist/wishlist.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/procs/myaccountpanel.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);


            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/commonpages/commonpages.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);
        }
        #endregion

        #region checkout
        if (Request.RawUrl.ToString().Contains("shoppingcart") || Request.RawUrl.ToString().Contains("checkout"))
        {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/cartlist/cartlist.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);
        }
        #endregion

        #region editprofile
        if (Request.RawUrl.ToString().Contains("editprofile"))
        {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/procs/editprofile.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/procs/myaccountpanel.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/commonpages/commonpages.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);
        }

        #endregion

        #region changepasswd
        if (Request.RawUrl.ToString().Contains("changepassword"))
        {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/procs/changepassword.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/procs/myaccountpanel.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/commonpages/commonpages.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);
        }

        #endregion


	    #region points
        if ( Request.RawUrl.ToString().Contains("pthistory") )
        {
	        myHtmlLink = new HtmlLink();
	        myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/procs/pthistory.css" , ConfigurationManager.AppSettings ["projectname"].ToString());
	        myHtmlLink.Attributes.Add("rel" , "stylesheet");
	        myHtmlLink.Attributes.Add("type" , "text/css");
            myHtmlLink.Attributes.Add("media", "all");
	        Page.Header.Controls.Add(myHtmlLink);

	        myHtmlLink = new HtmlLink();
	        myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/procs/myaccountpanel.css" , ConfigurationManager.AppSettings ["projectname"].ToString());
	        myHtmlLink.Attributes.Add("rel" , "stylesheet");
	        myHtmlLink.Attributes.Add("type" , "text/css");
            myHtmlLink.Attributes.Add("media", "all");
	        Page.Header.Controls.Add(myHtmlLink);

	        myHtmlLink = new HtmlLink();
	        myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/commonpages/commonpages.css" , ConfigurationManager.AppSettings ["projectname"].ToString());
	        myHtmlLink.Attributes.Add("rel" , "stylesheet");
	        myHtmlLink.Attributes.Add("type" , "text/css");
            myHtmlLink.Attributes.Add("media", "all");
	        Page.Header.Controls.Add(myHtmlLink);
        }
	    #endregion

        #region subscriptionhistory
        if ( Request.RawUrl.ToString().Contains("subscriptionhistory") )
        {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/procs/subscription.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

	        myHtmlLink = new HtmlLink();
	        myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/procs/myaccountpanel.css" , ConfigurationManager.AppSettings ["projectname"].ToString());
	        myHtmlLink.Attributes.Add("rel" , "stylesheet");
	        myHtmlLink.Attributes.Add("type" , "text/css");
            myHtmlLink.Attributes.Add("media", "all");
	        Page.Header.Controls.Add(myHtmlLink);

	        myHtmlLink = new HtmlLink();
	        myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/commonpages/commonpages.css" , ConfigurationManager.AppSettings ["projectname"].ToString());
	        myHtmlLink.Attributes.Add("rel" , "stylesheet");
	        myHtmlLink.Attributes.Add("type" , "text/css");
            myHtmlLink.Attributes.Add("media", "all");
	        Page.Header.Controls.Add(myHtmlLink);
        }
        #endregion
        #region checkout
        if (Request.RawUrl.ToString().Contains("checkout"))
        {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/checkout/checkouttabs.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/checkout/checkoutpages.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);
        }
        #endregion

        #region thanks
        if (Request.RawUrl.ToString().Contains("thanks"))
        {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/thanks/thanks.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

        }
        #endregion


        #region success
        if (Request.RawUrl.ToString().Contains("success"))
        {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/thanks/thanks.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);


	   myHtmlLink = new HtmlLink();
	   myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/commonpages/commonpages.css" , ConfigurationManager.AppSettings ["projectname"].ToString());
	   myHtmlLink.Attributes.Add("rel" , "stylesheet");
	   myHtmlLink.Attributes.Add("type" , "text/css");
       myHtmlLink.Attributes.Add("media", "all");
	   Page.Header.Controls.Add(myHtmlLink);
        }
        #endregion
        #region sitemap/enquiry
        if (Request.RawUrl.ToString().Contains("postenquiry") || Request.RawUrl.ToString().Contains("enquiry")|| Request.RawUrl.ToString().Contains("generatesitemap"))
        {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/postenquiry/postenquiry.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

        }
        #endregion

        #region response
        if (Request.RawUrl.ToString().Contains("response"))
        {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/thanks/thanks.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/commonpages/commonpages.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);
        }
        #endregion

        #region contactus
        if (Request.Url.AbsoluteUri.Contains("contactus"))
        {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/commonpages/commonpages.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/contact/contact.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);


        }
        #endregion
        #region aboutus
        if (Request.Url.AbsoluteUri.Contains("aboutus"))
        {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/commonpages/commonpages.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);
        }
        #endregion
        #region package
        if (Request.RawUrl.ToString().Contains("subscribe"))
        {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/package/package-detail.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

           

        }
        #endregion

        #region summery
        if (Request.RawUrl.ToString().Contains("summary"))
        {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/operator/oprator.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);



        }
        #endregion
        string url = Request.Url.AbsoluteUri;

        #region footer
        if (url.Contains("footer"))
        {
            String fullOrigionalpath = Request.Url.ToString();

            string cat = fullOrigionalpath.Replace(ReturnUrl("sitepathmain") + "footer.aspx?catname=", "");
           
            DataTable dtcat = clscms.searchnewsbyname(cat);

            if (dtcat.Rows.Count > 0)
            {
                myHtmlLink = new HtmlLink();
                myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/commonpages/commonpages.css", ConfigurationManager.AppSettings["projectname"].ToString());
                myHtmlLink.Attributes.Add("rel", "stylesheet");
                myHtmlLink.Attributes.Add("type", "text/css");
                myHtmlLink.Attributes.Add("media", "all");
                Page.Header.Controls.Add(myHtmlLink);

            }
        }
        #endregion

        #region donation
        if (Request.Url.AbsoluteUri.Contains("donation"))
        {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/donate/donation.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/loginregister/loginregister.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/commonpages/commonpages.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

        }
        #endregion

        #region viewall
        if (Request.RawUrl.ToString().Contains("viewall"))
        {
           
           myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/productrow/productrow-search.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);


        }
        #endregion

        #region director
        if (Request.RawUrl.ToString().Contains("director"))
        {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/artist/artist.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/commonpages/commonpages.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

        }
        #endregion

        #region directordetail

        

      
       
        
        //if (Request.RawUrl.ToString().Contains("pd"))
        //{
        if (url1.Contains("artistdetail.aspx"))
         {
        //if (Request.RawUrl.ToString().Contaartistdetail.aspxins("ad"))
        // {

          myHtmlLink = new HtmlLink();
          myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/artist/artistinfo.css", ConfigurationManager.AppSettings["projectname"].ToString());
          myHtmlLink.Attributes.Add("rel", "stylesheet");
          myHtmlLink.Attributes.Add("type", "text/css");
          myHtmlLink.Attributes.Add("media", "all");
          Page.Header.Controls.Add(myHtmlLink);

          myHtmlLink = new HtmlLink();
          myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/relatedproduct/relatedproduct.css", ConfigurationManager.AppSettings["projectname"].ToString());
          myHtmlLink.Attributes.Add("rel", "stylesheet");
          myHtmlLink.Attributes.Add("type", "text/css");
          myHtmlLink.Attributes.Add("media", "all");
          Page.Header.Controls.Add(myHtmlLink);


      }
      #endregion

        #region directordetail
        if (Request.RawUrl.ToString().Contains("Response"))
        {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/commonpages/commonpages.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/thanks/thanks.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);
        }
        #endregion

	    #region popup

        if ( Request.RawUrl.ToString().Contains("pop") )
        {

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/popup/popup.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);
	
        }
	    #endregion

        #region recommandation
        if (Request.RawUrl.ToString().Contains("recommendation"))
        {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/procs/myaccountpanel.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/commonpages/commonpages.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);


            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/procs/recommandation.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);


            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/carousel/carousel.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);
        }

        #endregion
        #region preference
        if (Request.RawUrl.ToString().Contains("preference"))
        {
           

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/procs/myaccountpanel.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/commonpages/commonpages.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");

            Page.Header.Controls.Add(myHtmlLink);


            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/procs/preference.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");

            Page.Header.Controls.Add(myHtmlLink);
        }

        #endregion
        #region recommandationuser
        if (Request.RawUrl.ToString().Contains("recommend"))
        {


            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/procs/myaccountpanel.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/commonpages/commonpages.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);


            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/procs/recommandation.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);


            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/carousel/carousel.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);
        }

        #endregion

        #region error
        if (Request.RawUrl.ToString().Contains("errorpg"))
        {
	        myHtmlLink = new HtmlLink();
	        myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/commonpages/commonpages.css" , ConfigurationManager.AppSettings ["projectname"].ToString());
	        myHtmlLink.Attributes.Add("rel" , "stylesheet");
	        myHtmlLink.Attributes.Add("type" , "text/css");
            myHtmlLink.Attributes.Add("media", "all");
	        Page.Header.Controls.Add(myHtmlLink);

	        myHtmlLink = new HtmlLink();
	        myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/errorpage/errorpage.css" , ConfigurationManager.AppSettings ["projectname"].ToString());
	        myHtmlLink.Attributes.Add("rel" , "stylesheet");
	        myHtmlLink.Attributes.Add("type" , "text/css");
            myHtmlLink.Attributes.Add("media", "all");
	        Page.Header.Controls.Add(myHtmlLink);
        }

        #endregion
        #region defaultcss
        if (Request.Url.AbsoluteUri.Contains("arrivalmoreproduct"))
        {
            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/carousel/carousel.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/loginregister/loginregister.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);

            myHtmlLink = new HtmlLink();
            myHtmlLink.Href = String.Format(ReturnUrl("sitepathmain") + "CSS/{0}/promo/promo.css", ConfigurationManager.AppSettings["projectname"].ToString());
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");
            myHtmlLink.Attributes.Add("media", "all");
            Page.Header.Controls.Add(myHtmlLink);
        }
        #endregion
    }
}