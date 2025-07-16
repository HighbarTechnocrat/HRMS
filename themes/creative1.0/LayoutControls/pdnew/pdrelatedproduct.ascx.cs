using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using AjaxControlToolkit;

public partial class themes_secondtheme_LayoutControls_pdnew_pdrelatedproduct : System.Web.UI.UserControl
{
    private static int productId;
    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;}
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
        {
            productId = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["p"].ToString()));
        }
        if (!IsPostBack)
        {
            popularcontrol();  
        }
    }
    protected void popularcontrol()
    {
        DataTable dthome = classproduct.get_proc_Productrelated(productId);
        if (dthome.Rows.Count > 0)
        {
            rptarrival.DataSource = dthome;
            rptarrival.DataBind();
        }
        else
        {
            filmsyoumaylikediv.Visible = false;
        }
    }
     public string productUrlrewriting(object productname, object productid)
     {
         string strurl = "";
         try
         {
             strurl = UrlRewritingVM.getUrlRewritingInfo(productname, UrlRewritingVM.Encrypt(productid.ToString()), "PD");
             return strurl;
         }
         catch (Exception ex)
         {
             return strurl;
         }
     }
     protected void rptarrival_ItemDataBound(object sender, RepeaterItemEventArgs e)
     {
         Label lblpkg = (Label)e.Item.FindControl("lblpkg");
         LinkButton lnkfav = (LinkButton)e.Item.FindControl("lnkfav");
         Rating Rating1 = (Rating)e.Item.FindControl("Rating1");
         Label productid1 = (Label)e.Item.FindControl("lblpid");
         LinkButton lblmessage = (LinkButton)e.Item.FindControl("lblmessage");

         Label lblimgsmall = (Label)e.Item.FindControl("lblimgsmall");
         Literal ltrimage = (Literal)e.Item.FindControl("ltrimage");
         Label lblpname = (Label)e.Item.FindControl("lblpname");
         Label lblshortdesr = (Label)e.Item.FindControl("lblshortdesr");
         LinkButton lnksavemsg = (LinkButton)e.Item.FindControl("lnksavemessage");
         Panel pnlpkgs = (Panel)e.Item.FindControl("pnlpkgs");

         int PID = 0;
         PID = Convert.ToInt32(productid1.Text);

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


         reviews objrate = classreviews.getrating(PID);

         DataTable dtrc = classreviews.getproductratingcnt_id(productid1.Text);

         #region rating

         if (Convert.ToInt32(dtrc.Rows[0]["ratingcount"].ToString()) != 0)
         {
             Rating1.Visible = true;
             Rating1.CurrentRating = objrate.rating;
         }
         else
         {
             Rating1.Visible = false;
         }

         #endregion

         #region favourite
         if (Page.User.Identity.IsAuthenticated)
         {
             DataTable dtrf = clswishlist.wishlist_GetSumOfProductFromQtyTable(Page.User.Identity.Name, Convert.ToInt32(productid1.Text));
             if (Convert.ToInt32(dtrf.Rows[0]["SumQty"]) > 0)
             {
                 pnlpkgs.Visible = false;
                 lblmessage.Text = "Saved";
             }
             else
             {
                 pnlpkgs.Visible = false;
                 lblmessage.Enabled = true;
                 lblmessage.Text = "Add to favorites";
             }

         }
         else
         {
             lblmessage.Text = "Login to add favorites";
         }
         #endregion

         #region fetchpinit
         string url = UrlRewritingVM.getUrlRewritingInfo(lblpname.Text.ToString(), productid1.Text, "PD");
         string strdpath = "http://pinterest.com/pin/create/button/?url=" + url.ToString() + "&amp;media=" + ConfigurationManager.AppSettings["sitepathadmin"]+ "images/smallproduct/" + lblimgsmall.Text.ToString() + "&amp;description=" + lblshortdesr.Text.ToString();

         ltrimage.Text = "<a href='" + strdpath + "' target= '_blank' count-layout='none'><i class='fa fa-pinterest-p'></i></a>";


         #endregion
     }

     protected void rptarrival_ItemCommand(object source , RepeaterCommandEventArgs e)
     {
         
     }

    
     public string productfav(object productid)
     {
         string strurl = "";
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
 }
    
  
   
