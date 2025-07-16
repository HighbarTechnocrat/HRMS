using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AjaxControlToolkit;
using System.Text.RegularExpressions;
using System.IO;

public partial class themes_creative1_LayoutControls_profile_favorites : System.Web.UI.UserControl
{
    public static int userid1;

    public static string userid;
    public static string pstname = "";
    public static int pid;
    public static string catid="";
    public string fname = "";
    public static int PageSize = 10, RecordCount;
    public static int pgi=1;
    public static string gid = "";
    public static string sdate = "";
    public static string edate = "";
    public static string title = "";

    public static int pageCount;
    public static int maxpage = 5;
    public static double dblmainpg;
    public static int maxpgcount;
    public static int maxpg = 1;

     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;}
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            if (Request.QueryString["userid"] != null && Request.QueryString["userid"] != "" && Request.QueryString.Count == 1 && Request.QueryString["userid"].Length == 24)
            {
                if (Int32.TryParse(UrlRewritingVM.Decrypt(Request.QueryString["userid"].ToString().Trim()), out userid1))
                {
                    DataTable dtuser = classaddress.getuserbyindexid(Convert.ToInt32(commonclass.GetSafeIDFromURL(userid1.ToString())));
                    if (dtuser.Rows.Count > 0)
                    {
                        userid = dtuser.Rows[0]["username"].ToString().Trim();
                    }
                    else
                    {
                        Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
                    }
                }
                else
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
                }
            }
            else
            {
                userid = Page.User.Identity.Name.ToString().Trim();
            }
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + Request.RawUrl);
        }
        if(!IsPostBack)
        {
            pgi = 1;
            if (Request.QueryString["c"] != "" && Request.QueryString["c"] != null)
            {
                catid = Request.QueryString["c"].ToString();
                getcatname();
                bindwall(pgi);
            }
            else
            {
                lblcatname.Text = "Favorites";
                bindwall(pgi);
            }
        }
    }

    public void getcatname()
    {
        if(catid != "" && catid != null)
        {
            DataTable dt = classproductimage.getcategorydetails(Convert.ToInt32(catid));
            if(dt.Rows.Count > 0)
            {
                lblcatname.Text = "Favorites " + dt.Rows[0]["categoryname"].ToString();
            }
            else
            {
                lblcatname.Text = "Favorites ";
            }
        }
    }

    public void bindwall(int PageIndex)
    {
        DataSet dswall = new DataSet();
        dswall = classproductimage.latestwallforallsearch1(catid, pstname, gid, fname, userid, title, sdate, edate, PageIndex, PageSize, out RecordCount);
        if(dswall.Tables.Count>0)
        {
            if(dswall.Tables[0].Rows.Count >0)
            {
                rptwall.DataSource = dswall.Tables[0];
                rptwall.DataBind();
                if (rptwall.Items.Count > 0)
                {
                    for (int i = 0; i < rptwall.Items.Count; i++)
                    {
                        LinkButton lnklike = (LinkButton)rptwall.Items[i].FindControl("lnklike");
                        Label lbllike = (Label)rptwall.Items[i].FindControl("lbllike");
                        LinkButton lnkcomment = (LinkButton)rptwall.Items[i].FindControl("lnkcomm");
                        Label lblpid = (Label)rptwall.Items[i].FindControl("lblpid");
                        Label lbllikecount = (Label)rptwall.Items[i].FindControl("lbllikecount");

                        LinkButton lnkdislike = (LinkButton)rptwall.Items[i].FindControl("lnkdislike");
                        Label lbldislike = (Label)rptwall.Items[i].FindControl("lbldislike");
                        Label lbldislikecount = (Label)rptwall.Items[i].FindControl("lbldislikecount");
                        string liketext = "";
                        if (Page.User.Identity.IsAuthenticated)
                        {
                            DataTable dtlike = classreviews.getlikedetailsbyuserpostid(userid, Convert.ToInt32(lblpid.Text));
                            if (dtlike.Rows.Count > 0)
                            {
                                //like count
                                if (dtlike.Rows[0]["likeflag"].ToString().Trim() == "Unlike")
                                {
                                    liketext = "<font style='font-weight:400;color:#338ec9 !important;'>" + "Like" + "</font>";
                                    lbllike.Text = "<i class='fa fa-thumbs-up' style='color:#338ec9 !important;'></i> " + liketext;
                                    if (Convert.ToInt32(dtlike.Rows[0]["likecount"].ToString().Trim()) > 0)
                                    {
                                        lbllikecount.ForeColor = System.Drawing.Color.FromName("#338ec9");
                                        lbllikecount.Text = " (" + dtlike.Rows[0]["likecount"].ToString().Trim() + ")";
                                    }
                                    else
                                    {
                                        lbllikecount.Text = "";
                                    }
                                }
                                else
                                {
                                    lbllike.Text = "<i class='fa fa-thumbs-up' style='font-weight:400;color:#777777 !important;'></i> " + dtlike.Rows[0]["likeflag"].ToString().Trim();
                                    lbllike.ForeColor = System.Drawing.Color.FromName("#777777");
                                    if (Convert.ToInt32(dtlike.Rows[0]["likecount"].ToString().Trim()) > 0)
                                    {
                                        lbllikecount.Text = " (" + dtlike.Rows[0]["likecount"].ToString().Trim() + ")";
                                    }
                                    else
                                    {
                                        lbllikecount.Text = "";
                                    }
                                }
                                //Dislike Module
                                if (dtlike.Rows[0]["dislikeflag"].ToString().Trim() == "Undislike")
                                {
                                    liketext = "<font style='font-weight:400;color:#338ec9 !important;'>" + "Dislike" + "</font>";
                                    lbldislike.Text = "<i class='fa fa-thumbs-down' style='color:#338ec9 !important;'></i> " + liketext;
                                    if (Convert.ToInt32(dtlike.Rows[0]["dislikecount"].ToString().Trim()) > 0)
                                    {
                                        lbldislikecount.ForeColor = System.Drawing.Color.FromName("#338ec9");
                                        lbldislikecount.Text = " (" + dtlike.Rows[0]["dislikecount"].ToString().Trim() + ")";
                                    }
                                    else
                                    {
                                        lbldislikecount.Text = "";
                                    }
                                }
                                else
                                {
                                    lbldislike.Text = "<i class='fa fa-thumbs-down' style='font-weight:400;color:#777777 !important;'></i> " + dtlike.Rows[0]["dislikeflag"].ToString().Trim();
                                    lbldislike.ForeColor = System.Drawing.Color.FromName("#777777");
                                    if (Convert.ToInt32(dtlike.Rows[0]["dislikecount"].ToString().Trim()) > 0)
                                    {
                                        lbldislikecount.Text = " (" + dtlike.Rows[0]["dislikecount"].ToString().Trim() + ")";
                                    }
                                    else
                                    {
                                        lbldislikecount.Text = "";
                                    }
                                }
                                //Comment Module
                                if (Convert.ToInt32(dtlike.Rows[0]["commentcount"].ToString()) > 0)
                                {
                                    lnkcomment.Text = "<i class='fa fa-comment' style='color:#777777 !important;'></i> Comment" + " (" + dtlike.Rows[0]["commentcount"].ToString() + ")";
                                    lnkcomment.ForeColor = System.Drawing.Color.FromName("#777777");
                                }
                                else
                                {
                                    lnkcomment.ForeColor = System.Drawing.Color.FromName("#777777");
                                    lnkcomment.Text = "<i class='fa fa-comment' style='color:#777777 !important;'></i> Comment";
                                }
                            }
                            else
                            {
                                lbllike.Text = "<i class='fa fa-thumbs-up' style='color:#777777 !important;'></i> Like";
                                lbldislike.Text = "<i class='fa fa-thumbs-down' style='color:#777777 !important;'></i> Dislike";
                                lbllikecount.Visible = false;
                                lbldislikecount.Visible = false;
                                lnkcomment.Text = "<i class='fa fa-comment' style='color:#777777 !important;'></i> Comment";
                            }
                        }
                        else
                        {
                            DataTable dtlike = classreviews.getlikedetailsbyuserpostid("", Convert.ToInt32(lblpid.Text));
                            if (dtlike.Rows.Count > 0)
                            {
                                //like module
                                if (dtlike.Rows[0]["likeflag"].ToString().Trim() == "Unlike")
                                {
                                    liketext = "<font style='font-weight:400;color:#777777 !important;'>" + dtlike.Rows[0]["likeflag"].ToString().Trim() + "</font>";
                                    lbllike.Text = "<i class='fa fa-thumbs-up' style='color:#777777 !important;'></i> " + liketext;
                                    if (Convert.ToInt32(dtlike.Rows[0]["likecount"].ToString().Trim()) > 0)
                                    {
                                        lbllikecount.ForeColor = System.Drawing.Color.FromName("color:#777777 !important;");
                                        lbllikecount.Text = " (" + dtlike.Rows[0]["likecount"].ToString().Trim() + ")";
                                    }
                                    else
                                    {
                                        lbllikecount.Text = "";
                                    }
                                }
                                else
                                {
                                    lbllike.Text = "<i class='fa fa-thumbs-up' style='color:#777777 !important;'></i> " + dtlike.Rows[0]["likeflag"].ToString().Trim();
                                    if (Convert.ToInt32(dtlike.Rows[0]["likecount"].ToString().Trim()) > 0)
                                    {
                                        lbllikecount.ForeColor = System.Drawing.Color.FromName("color:#777777 !important;");
                                        lbllikecount.Text = " (" + dtlike.Rows[0]["likecount"].ToString().Trim() + ")";
                                    }
                                    else
                                    {
                                        lbllikecount.Text = "";
                                    }
                                }
                                //Dislike Module
                                if (dtlike.Rows[0]["dislikeflag"].ToString().Trim() == "Undislike")
                                {
                                    liketext = "<font style='font-weight:400;color:#777777 !important;'>" + "Dislike" + "</font>";
                                    lbldislike.Text = "<i class='fa fa-thumbs-down' style='color:#777777 !important;'></i> " + liketext;
                                    if (Convert.ToInt32(dtlike.Rows[0]["dislikecount"].ToString().Trim()) > 0)
                                    {
                                        lbldislikecount.ForeColor = System.Drawing.Color.FromName("color:#777777 !important;");
                                        lbldislikecount.Text = " (" + dtlike.Rows[0]["dislikecount"].ToString().Trim() + ")";
                                    }
                                    else
                                    {
                                        lbldislikecount.Text = "";
                                    }
                                }
                                else
                                {
                                    lbldislike.Text = "<i class='fa fa-thumbs-down' style='color:#777777 !important;'></i> " + dtlike.Rows[0]["dislikeflag"].ToString().Trim();
                                    if (Convert.ToInt32(dtlike.Rows[0]["dislikecount"].ToString().Trim()) > 0)
                                    {
                                        lbldislikecount.ForeColor = System.Drawing.Color.FromName("color:#777777 !important;");
                                        lbldislikecount.Text = " (" + dtlike.Rows[0]["dislikecount"].ToString().Trim() + ")";
                                    }
                                    else
                                    {
                                        lbldislikecount.Text = "";
                                    }
                                }
                                //comment Module
                                if (Convert.ToInt32(dtlike.Rows[0]["commentcount"].ToString()) > 0)
                                {
                                    lnkcomment.ForeColor = System.Drawing.Color.FromName("#777777");
                                    lnkcomment.Text = "<i class='fa fa-comment' style='color:#777777 !important;'></i> Comment" + " (" + dtlike.Rows[0]["commentcount"].ToString() + ")";
                                }
                                else
                                {
                                    lnkcomment.ForeColor = System.Drawing.Color.FromName("#777777");
                                    lnkcomment.Text = "<i class='fa fa-comment' style='color:#777777 !important;'></i> Comment";
                                }
                                lnklike.PostBackUrl = ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + Request.RawUrl.ToString();
                            }
                            else
                            {
                                lbllike.Text = "<i class='fa fa-thumbs-up' style='color:#777777 !important;'></i> Like";
                                lbldislike.Text = "<i class='fa fa-thumbs-down' style='color:#777777 !important;'></i> Dislike";
                                lbllikecount.Visible = false;
                                lbldislikecount.Visible = false;
                                lnkcomment.Text = "<i class='fa fa-comment' style='color:#777777 !important;'></i> Comment";
                            }
                        }
                    }
                }
                if (maxpg == 1)
                {
                    lnkprev.Visible = false;
                    this.PopulatePager(RecordCount, PageIndex);
                }
                else
                {
                    this.maxpager(RecordCount, PageIndex, maxpg);
                }
            }
            else
            {
                pnlpst.Visible = false;
                lblmsg.Visible = true;
                lblmsg.Text = "No Post Uploded by this User";
            }
        }
        else
        {
            pnlpst.Visible = false;
            lblmsg.Visible = true;
            lblmsg.Text = "No Post Uploded by this User";
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

    public string getuser(object userid)
    {
        string strurl = "";
        try
        {
            strurl = ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(userid.ToString());
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }

    public string getuserimage(object imagepath)
    {
        string strurl = "";
        try
        {
            if (imagepath.ToString().Length > 0)
            {
                strurl = ReturnUrl("sitepath") + "images/profile55x55/" + imagepath.ToString();
            }
            else
            {
                strurl = ReturnUrl("sitepath") + "images/profile55x55/noimage1.png";
            }
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }

    public string getproductimage(object imagepath)
    {
        string strurl = "";
        try
        {
            if (imagepath.ToString().Length > 0 && imagepath.ToString() != "noimage2.png" && imagepath.ToString() != "noimage2.gif" && imagepath.ToString() != "N/A" && imagepath.ToString() != "n/a")
            {
                img1.Visible = true;
                pnlvideo.Visible = false;
                pstdoc.Visible = false;
                string fileName = Request.PhysicalApplicationPath + "\\images\\450x300\\" + imagepath.ToString();
                if (File.Exists(fileName))
                {
                    strurl = "<img src='" + ReturnUrl("sitepathmain") + "images/450x300/" + imagepath.ToString() + "' />";
                }
                else
                {
                    strurl = "<img src='" + ConfigurationManager.AppSettings["sitepathadmin"]+ "images/450x300/" + imagepath.ToString() + "' />";
                }
            }
            else
            {
                img1.Visible = false;
                strurl = "";
            }
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }

    public string getpostvideo(object videocode, object videourl)
    {

        string strurl = "";
        try
        {
            if (videourl.ToString().Length > 0 || videourl.ToString() != "" || videocode.ToString().Length > 0 || videocode.ToString() != "")
            {
                if (videocode.ToString().Length > 0 && videocode.ToString() != "" &&videocode.ToString() != "N/A" && videocode.ToString() != "n/a")
                {
                    img1.Visible = false;
                    pnlvideo.Visible = true;
                    pstdoc.Visible = false;
                    strurl = videocode.ToString();
                }
                else
                {
                    if (videourl.ToString() != "N/A" && videourl.ToString() != "n/a")
                    {
                        img1.Visible = false;
                        pnlvideo.Visible = true;
                        pstdoc.Visible = false;
                        HttpCookie cookie = HttpContext.Current.Request.Cookies["internet"];
                        if (cookie != null)
                        {
                            if (cookie.Value.ToString().ToLower().Trim() == "true")
                            {
                                strurl = "<video controls preload='metadata' class='videourl' data-setup='{}'><source src='" + ConfigurationManager.AppSettings["internetURL"].ToString().Trim() + videourl + "' type='video/mp4'></video>";
                            }
                            else
                            {
                                strurl = "<video controls preload='metadata' class='videourl' data-setup='{}'><source src='" + ConfigurationManager.AppSettings["intranetURL"].ToString().Trim() + videourl + "' type='video/mp4'></video>";
                            }

                        }
                    }
                    else
                    {
                        pnlvideo.Visible = false;
                        strurl = "";
                    }
                }
            }
            else
            {
                strurl = "";
            }
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }

    public string getpostdoc(object doc)
    {
        string file = "";
        try
        {
            if (doc.ToString().Length > 0 || doc.ToString() != "")
            {
                if (doc.ToString() != "N/A" && doc.ToString() != "n/a")
                {
                    img1.Visible = false;
                    pnlvideo.Visible = false;
                    pstdoc.Visible = true;
                    file = doc.ToString();
                }
                else
                {
                    file = "";
                    pstdoc.Visible = false;
                }
            }
            else
            {
                file = "";
                pstdoc.Visible = false;
            }
            return file;
        }
        catch (Exception ex)
        {
            return file;
        }
    }

    public string productUrlrewriting2(object productname, object productid)
    {
        string strurl = "";
        string cat = "";
        try
        {
            DataTable td = classproduct.get_product_cattype(Convert.ToInt32(productid));
            cat = td.Rows[0]["cattype"].ToString().Trim();
            if (cat == "I")
            {
                strurl = UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(productid.ToString().Trim()), "PG");
            }
            else
            {
                strurl = UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(productid.ToString().Trim()), "PDID");
            }
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }

    public string getdocext(object ext)
    {
        string file = "";
        string ext1 = "";
        string rtnval = "";
        try
        {
            file = ext.ToString();
            if (file != "" && file != "N/A" && file != "n/a")
            {
                int z = 0;
                z = file.LastIndexOf('.');
                int x = 0;
                x = file.Length;
                ext1 = file.Substring(z, x - z).ToLower();
                if (ext1 == ".pdf")
                {

                    rtnval = "<img class='extimg' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/pdf1hover.png" + "' />";
                }
                else if (ext1 == ".doc")
                {
                    rtnval = "<img class='extimg' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/doc1hover.png" + "' />";
                }
                else if (ext1 == ".docx")
                {
                    rtnval = "<img class='extimg' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/doc1hover.png" + "' />";
                }
                else if (ext1 == ".xls")
                {
                    rtnval = "<img class='extimg' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/xls1hover.png" + "' />";
                }
                else if (ext1 == ".xlsx")
                {
                    rtnval = "<img class='extimg' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/xls1hover.png" + "' />";
                }
                else if (ext1 == ".ppt")
                {
                    rtnval = "<img class='multidocext' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/ppthover.png" + "' />";
                }
                else if (ext1 == ".pptx")
                {
                    rtnval = "<img class='multidocext' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/ppthover.png" + "' />";
                }
            }
            return rtnval;
        }
        catch (Exception ex)
        {
            return rtnval;
        }


    }

    public string getshortdesc(object shortdesc)
    {
        string strurl = "";
        try
        {
            desc.InnerHtml = shortdesc.ToString().Trim();

            strurl = Regex.Replace(desc.InnerHtml, "<.*?>", String.Empty);

            if (strurl.Length > 200)
            {
                strurl = strurl.Substring(0, 200) + "...";
            }

            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }

    public string getdate(object date)
    {
        string strurl = "";
        try
        {
            DateTime dat = Convert.ToDateTime(date.ToString());
            DateTime dat2 = DateTime.Now;
            TimeSpan diff = dat2 - dat;
            int days = Convert.ToInt32(diff.Days);
            int hours = Convert.ToInt32(diff.Hours);
            int minutes = Convert.ToInt32(diff.Minutes);
            int seconds = Convert.ToInt32(diff.Seconds);
            if (days > 0)
            {
                if (days < 31)
                {
                    strurl = strurl + days + " days ago";
                }
                else
                {
                    if (days <= 365)
                    {
                        int monthsApart = 12 * (dat2.Year - dat.Year) + dat2.Month - dat.Month;
                        strurl = strurl + monthsApart + " months ago";
                    }
                    else
                    {
                        int yearpart = dat2.Year - dat.Year;
                        strurl = strurl + yearpart + " year ago";
                    }
                }
            }
            else if (hours > 0)
            {
                strurl = strurl + hours + " hours ";

                if (minutes > 0)
                {
                    strurl = strurl + minutes + " minutes ago";
                }
            }
            else if (minutes > 0)
            {
                strurl = strurl + minutes + " minutes ago";
            }
            else if (seconds > 0)
            {
                strurl = strurl + seconds + " seconds ago";
            }

            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }

    public string reviewUrlrewriting(object productname, object productid)
    {
        string strurl = "";
        try
        {
            strurl = UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(productid.ToString().Trim()), "PDID") + "#MainContent_pdnewglobal_m_uxPdLayout_ctl04_criticcornerdiv";
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }

    public string productfav(object productid)
    {
        string strurl = "";
        try
        {
            if (Page.User.Identity.IsAuthenticated == true)
            {
                pid = Convert.ToInt32(productid.ToString().Trim());
                DataTable dtrf = clswishlist.wishlist_GetSumOfProductFromQtyTable(Page.User.Identity.Name, Convert.ToInt32(pid));

                if (Convert.ToInt32(dtrf.Rows[0]["SumQty"]) > 0)
                {
                    strurl = " fa fa-heart";
                }
                else
                {
                    strurl = "fa fa-heart-o";
                }
            }
            else
            {
                strurl = "fa fa-heart-o";
            }
            return strurl;

        }
        catch (Exception ex)
        {
            return strurl;
        }
    }

    protected void rptwall_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        bool iserror = false;
        try
        {
            if (e.CommandName.ToLower() == "cmdlike")
            {
                if (Page.User.Identity.IsAuthenticated)
                {
                    iserror = classreviews.insertdeletepostlikes(Page.User.Identity.Name.ToString().Trim(), Convert.ToInt32(e.CommandArgument.ToString()));
                    bindwall(pgi);
                }
                else
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + Request.RawUrl);
                }
            }
            else if (e.CommandName.ToLower() == "cmddislike")
            {
                if (Page.User.Identity.IsAuthenticated)
                {
                    iserror = classreviews.insertdeletepostdislikes(Page.User.Identity.Name.ToString().Trim(), Convert.ToInt32(e.CommandArgument.ToString()));
                    bindwall(pgi);
                }
                else
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + Request.RawUrl);
                }
            }
            else if (e.CommandName.ToLower() == "cmdcomment")
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "comments/" + UrlRewritingVM.Encrypt(e.CommandArgument.ToString()));
            }
            else if(e.CommandName.ToLower() == "cmdremove")
            {
                bool m_index = clswishlist.Deletewishlistproduct1(Convert.ToInt32(e.CommandArgument),Page.User.Identity.Name.ToString());
                bindwall(pgi);
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    private void PopulatePager(int recordCount, int currentPage)
    {
        lnknxt.Visible = false;
        lnkprev.Visible = false;
        double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
        pageCount = (int)Math.Ceiling(dblPageCount);
        List<ListItem> pages = new List<ListItem>();
        if (pageCount > 0)
        {
            int pgno = 0;
            for (int i = 1; i <= maxpage; i++)
            {
                if (pgno == 0)
                {
                    if (maxpg * maxpage < pageCount)
                    {
                        lnknxt.Visible = true;
                    }
                    pgno = (maxpg * maxpage) - (maxpage - 1);
                    pages.Add(new ListItem(pgno.ToString(), pgno.ToString(), pgno != currentPage));
                }
                else
                {
                    pgno = pgno + 1;
                    if (pgno <= pageCount)
                    {
                        pages.Add(new ListItem(pgno.ToString(), pgno.ToString(), pgno != currentPage));
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (pageCount == 1)
            {
                rptPager.Visible = false;
            }
            else
            {
                rptPager.Visible = true;
            }
        }
        else
        {
            rptPager.Visible = true;
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
    }

    private void maxpager(int recordCount, int currentPage, int mpg)
    {
        rptPager.Dispose();
        lnkprev.Visible = false;
        lnknxt.Visible = false;
        rptPager.Visible = true;
        if (maxpg != 1)
        {
            lnkprev.Visible = true;
        }
        if (maxpg == 1 || maxpg < 1)
        {
            maxpg = 1;
            lnkprev.Visible = false;
        }
        int pgno = 0;
        List<ListItem> pages = new List<ListItem>();
        for (int i = 1; i <= maxpage; i++)
        {
            if (pgno == 0)
            {
                if (maxpg * maxpage < pageCount)
                {
                    lnknxt.Visible = true;
                }
                pgno = (maxpg * maxpage) - (maxpage - 1);
                pages.Add(new ListItem(pgno.ToString(), pgno.ToString(), pgno != currentPage));
            }
            else
            {
                pgno = pgno + 1;
                if (pgno <= pageCount)
                {
                    pages.Add(new ListItem(pgno.ToString(), pgno.ToString(), pgno != currentPage));
                }
                else
                {
                    break;
                }
            }
        }
        if (maxpgcount == maxpg || maxpgcount == 1)
        {
            lnknxt.Visible = false;
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
    }

    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        pgi = pageIndex;
        this.bindwall(pageIndex);
    }

    protected void lnkprev_Click(object sender, EventArgs e)
    {
        int pgno;
        maxpg = maxpg - 1;
        pgno = (maxpg * maxpage) - (maxpage - 1);
        pgi = pgno;
        bindwall(pgno);
    }
    protected void lnknxt_Click(object sender, EventArgs e)
    {
        int pgno;
        maxpg = maxpg + 1;
        pgno = (maxpg * maxpage) - (maxpage - 1);
        pgi = pgno;
        bindwall(pgno);
    }
}
