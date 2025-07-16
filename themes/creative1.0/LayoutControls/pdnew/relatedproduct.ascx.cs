using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using AjaxControlToolkit;

public partial class themes_creative1_LayoutControls_pdnew_relatedproduct : System.Web.UI.UserControl
{
    private static int productId;

     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
        {
            productId = Convert.ToInt32(Request.QueryString["p"].ToString());
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
            //pnlrelated.Visible = true;
            rptarrival.DataSource = dthome;
            rptarrival.DataBind();
            if(rptarrival.Items.Count>0)
            {
                for(int i=0;i<rptarrival.Items.Count-1;i++)
                {
                    Label lblpid = (Label)rptarrival.Items[i].FindControl("lblpid");
                    LinkButton lnkfav = (LinkButton)rptarrival.Items[i].FindControl("lnkfav");
                    Rating rating1 = (Rating)rptarrival.Items[i].FindControl("Rating1");
                    Label lblpkg = (Label)rptarrival.Items[i].FindControl("lblpkg");

                    #region package
                    if (lblpkg.Text == "Free")
                    {
                        lblpkg.Attributes.Add("class", "freepkgr");
                    }
                    if (lblpkg.Text == "Premium")
                    {
                        lblpkg.Attributes.Add("class", "premiumpkgr");
                    }
                    if (lblpkg.Text == "special")
                    {
                        lblpkg.Attributes.Add("class", "spacialpkgr");
                    }
                    #endregion

                    #region rating
                    DataTable rating = classreviews.gettotalrating(productId);
                    if(rating.Rows.Count>0)
                    {
                        if(Convert.ToInt32(rating.Rows[0]["total"].ToString())>0)
                        {
                            int rate = (Convert.ToInt32(rating.Rows[0]["total"].ToString())) / (Convert.ToInt32(rating.Rows[0]["count"].ToString()));
                            if(rate>0)
                            {
                                rating1.CurrentRating = rate;
                            }
                            else
                            {
                                rating1.Visible = false;
                            }
                        }
                    }
                    #endregion

                    #region favourite
                    if (Page.User.Identity.IsAuthenticated)
                    {
                        DataTable dtrf = clswishlist.wishlist_GetSumOfProductFromQtyTable(Page.User.Identity.Name, Convert.ToInt32(lblpid.Text));

                        if (Convert.ToInt32(dtrf.Rows[0]["SumQty"]) > 0)
                        {
                            lnkfav.ToolTip = "Saved";
                        }
                        else
                        {
                            lnkfav.ToolTip = "Add to favorites";
                        }
                    }
                    else
                    {
                        lnkfav.ToolTip = "Login to add favorites";
                    }
                    #endregion
                }
            }
        }
        else
        {
            //pnlrelated.Visible = false;
            filmsyoumaylikediv.Visible = false;
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
}