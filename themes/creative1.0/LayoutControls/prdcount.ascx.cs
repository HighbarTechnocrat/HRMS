using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using AjaxControlToolkit;

public partial class Themes_SecondTheme_LayoutControls_prdcount : System.Web.UI.UserControl
{
    public static int productid;
    public string strur;
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
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
            string username = Convert.ToString(Page.User.Identity.Name);
            DataTable dthome = classrecommend.getproductsbyvisitcount_product(username);

            if (dthome.Rows.Count > 0)
            {
                pnlvisit.Visible = true;
                rptproductcount.DataSource = dthome;
                rptproductcount.DataBind();
            }

            else
            {
                pnlvisit.Visible = false;
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

            strurl = UrlRewritingVM.getUrlRewritingInfo(productname, productid, "PD");
            return strurl;

        }
        catch (Exception ex)
        {
            return strurl;
        }
    }

    public void lnkview_onclick(Object sender, EventArgs e)
    {
        string strurl = "";
        strurl = UrlRewritingVM.getUrlRewritingInfo("", "", "R");
        Response.Redirect(strurl);
    }

    protected void rptproductcount_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "favorite")
        {
            LinkButton lnkfav = (LinkButton)e.Item.FindControl("lnkfav");
            if (Page.User.Identity.IsAuthenticated == true)
            {
                int productid = Convert.ToInt32(e.CommandArgument);
                DataTable dtrf = clswishlist.wishlist_GetSumOfProductFromQtyTable(Page.User.Identity.Name, Convert.ToInt32(productid));

                if (Convert.ToInt32(dtrf.Rows[0]["SumQty"]) > 0)
                {
                    bool m_index = clswishlist.Deletewishlist_user(productid, Page.User.Identity.Name);
                }
                else
                {
                    bool strstring = clswishlist.insertwishlist(Page.User.Identity.Name, Convert.ToInt32(productid), "1", "", "");

                }
                popularcontrol();
            }
            else
            {



            }

        }
    }

    protected void rptproductcount_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lblpkg = (Label)e.Item.FindControl("lblpkg");
       // LinkButton lnkfav = (LinkButton)e.Item.FindControl("lnkfav");
        Rating Rating1 = (Rating)e.Item.FindControl("Rating1");
        Label productid1 = (Label)e.Item.FindControl("lblpid");

        LinkButton lblmessage = (LinkButton)e.Item.FindControl("lblmessage");
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
        if (lblpkg.Text.ToLower() == "Special".ToLower())
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
               
                lblmessage.Text = "Saved";
                lblmessage.Attributes.Add("style", "cursor:default");
                lblmessage.Enabled = false;
            }
            else
            {
                lblmessage.Text = "Add to favorites";
                lblmessage.Attributes.Add("style", "cursor:default");
               
                lblmessage.Enabled = false;
            }

        }
        else
        {
            lblmessage.Text = "Login to add favorites";
            lblmessage.Attributes.Add("style", "cursor:default");
            lblmessage.Enabled = false;
            lblmessage.PostBackUrl = ReturnUrl("sitepath") + "login";
        }
        #endregion

    }

    public string getcountOfAll()
    {
        string strAttrValue = "";


        return strAttrValue;

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
}