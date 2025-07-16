using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using AjaxControlToolkit;

public partial class Themes_SecondTheme_LayoutControls_catpreference : System.Web.UI.UserControl
{
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Page.User.Identity.IsAuthenticated)
                {
                    popularcontrol();
                }

            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    protected void popularcontrol()
    {
        try
        {
            creative.Common clsCommon = new creative.Common();
            string username = Page.User.Identity.Name;
            DataTable dthome = classrecommend.getallcategorybypreferencesbyusername(username);

            if (dthome.Rows.Count > 0)
            {
                rptcategname.DataSource = dthome;
                rptcategname.DataBind();
                for (int j = 0; j < rptcategname.Items.Count; j++)
                {
                    Label lblname = (Label)rptcategname.Items[j].FindControl("lblname");
                    Label lblid = (Label)rptcategname.Items[j].FindControl("lbcatlid");
                    HyperLink lnkb = (HyperLink)rptcategname.Items[j].FindControl("lnkview");
                 
                    string strid = lblid.Text;
                    if (strid != null || strid != "")
                    {
                        Repeater rptcatproduct = (Repeater)rptcategname.Items[j].FindControl("rptcatproduct");

                        Panel pnlcatname = (Panel)rptcategname.Items[j].FindControl("pnlcatname");
                        DataTable dtp = classrecommend.getproductidbycatidsdisplay_recommendation(strid, username);

                        if (dtp.Rows.Count > 0)
                        {
                           
                            rptcatproduct.DataSource = dtp;
                            rptcatproduct.DataBind();
                           pnlcatname.Visible = true;
                        }

                        else
                        {
                          pnlcatname.Visible = false;
                            lblname.Text = "";
                            
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
   
    public string productUrlrewriting(object productname, object productid)
    {
        string strurl = "";
        try
        {
            strurl = UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(productid.ToString().Trim()), "PDID");
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
    protected void rptcategname_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        int PID = 0;
        Label lblprod = (Label)e.Item.FindControl("lblpid");
        Label lblpkg = (Label)e.Item.FindControl("lblpkg");
        Rating ratingcat = (Rating)e.Item.FindControl("ratingcat");
        //LinkButton lnkfav = (LinkButton)e.Item.FindControl("lnkfav");
        LinkButton lblmessage = (LinkButton)e.Item.FindControl("lblmessage");
        Label lblimgsmall = (Label)e.Item.FindControl("lblimgsmall");
        Literal ltrimage = (Literal)e.Item.FindControl("ltrimage");
        Label lblpname = (Label)e.Item.FindControl("lblpname");
        Label lblshortdesr = (Label)e.Item.FindControl("lblshortdesr");

        PID = Convert.ToInt32(lblprod.Text);
        if (lblpkg.Text == "Free")
        {
            lblpkg.Attributes.Add("class", "freepkg");
        }
        if (lblpkg.Text == "Premium")
        {
            lblpkg.Attributes.Add("class", "premiumpkg");
            lblpkg.Text = "Subscription";
        }
        if (lblpkg.Text == "special")
        {
            lblpkg.Attributes.Add("class", "spacialpkg");
            lblpkg.Text = "Pay per view";
        }
        #region favorite

        if (Page.User.Identity.IsAuthenticated)
        {
            DataTable dtrc = clswishlist.wishlist_GetSumOfProductFromQtyTable(Page.User.Identity.Name, Convert.ToInt32(lblprod.Text));

            if (Convert.ToInt32(dtrc.Rows[0]["SumQty"]) > 0)
            {
               
                lblmessage.Text = "Saved";
                lblmessage.Attributes.Add("style", "cursor:default");
                lblmessage.Enabled = false;

            }
            else
            {

                lblmessage.Text = "Add to favourite";
                lblmessage.Attributes.Add("style", "cursor:default");
                lblmessage.Enabled = false;
            }
        }

        else
        {

            lblmessage.Text = "Login to add favourite";
            lblmessage.Attributes.Add("style", "cursor:default");
            lblmessage.Enabled = false;
        }

        #endregion
        #region rating


        DataTable dtr = classreviews.getproductrating_id(PID);
        reviews objrate = classreviews.getrating(PID);
        ratingcat.CurrentRating = objrate.rating;

        DataTable dtrf = classreviews.getproductratingcnt_id(lblprod.Text);
        if (dtrf.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtrf.Rows[0]["ratingcount"].ToString()) != 0)
            {
                ratingcat.Visible = true;
                ratingcat.CurrentRating = objrate.rating;
            }
            else
            {
                ratingcat.Visible = false;
            }
        }

        #endregion
        #region fetchpinit
        string url = UrlRewritingVM.getUrlRewritingInfo(lblpname.Text.ToString(), lblprod.Text, "PD");
        string strdpath = "http://pinterest.com/pin/create/button/?url=" + url.ToString() + "&amp;media=" + ConfigurationManager.AppSettings["sitepathadmin"]+ "images/smallproduct/" + lblimgsmall.Text.ToString() + "&amp;description=" + lblshortdesr.Text.ToString();

        ltrimage.Text = "<a href='" + strdpath + "' target= '_blank' count-layout='none'><i class='fa fa-pinterest-p'></i></a>";


        #endregion
        int rowsCount = rptcategname.Items.Count;
        RepeaterItem rptitem;

        for (int i = 0; i < rowsCount; i++)
        {
            rptitem = rptcategname.Items[i];
            Literal ltrcat = (Literal)rptitem.FindControl("ltrcat");
            Literal ltrjs = (Literal)rptitem.FindControl("ltrjs");
            ltrcat.Text = "<div id='carousel" + i + "' class='IM-carousel'>";
            //ltrjs.Text = "<script type='text/javascript'>jQuery('document').ready(function (e){$('#carousel" + i + "').IMCarousel({items: 5,lazyLoad : true,itemsDesktop: [1280, 5],itemsDesktopSmall: [1024, 4]});});</Script>";

        }

    }
    public string productfav(object productid)
    {
        string strurl = "";
        try
        {
            if (Page.User.Identity.IsAuthenticated == true)
            {
                int pid = Convert.ToInt32(productid.ToString().Trim());
                DataTable dtrf = clswishlist.wishlist_GetSumOfProductFromQtyTable(Page.User.Identity.Name, Convert.ToInt32(pid));

                if (Convert.ToInt32(dtrf.Rows[0]["SumQty"]) > 0)
                {
                    strurl = "heart-fill";
                }
                else
                {
                    strurl = "heart";
                }
            }
            else
            {
                strurl = "heart";
            }
            return strurl;

        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
    protected void rptcatproduct_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
    }
}