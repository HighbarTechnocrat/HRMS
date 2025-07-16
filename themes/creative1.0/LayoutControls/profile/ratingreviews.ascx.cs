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

public partial class themes_creative1_LayoutControls_profile_ratingreviews : System.Web.UI.UserControl
{
    public static int userid;
    public static string pstname="";
    public static string username;
    public static string title;
    public static string catid="";
    public static int PageSize = 10, RecordCount;
    public static int PageSize1 = 10, RecordCount1;
    public static int pgi=1;
    

    public static int pageCount;
    public static int maxpage = 5;
    public static double dblmainpg;
    public static int maxpgcount;
    public static int maxpg = 1;

    public DataTable user;

    public static int pgi1 = 1;
    public static int pageCount1;
    public static double dblmainpg1;
    public static int maxpgcount1;
    public static int maxpg1 = 1;

    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;}
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            pgi = 1;
            pgi1 = 1;
            if(!IsPostBack)
            {
                catid = "";
                displaycat();
                loadgrid(pgi1);
                bindwall(pgi);
            }
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + Request.RawUrl);
        }   
    }
    public void loadgrid(int PageIndex)
    {
        if (Request.QueryString["userid"] != null && Request.QueryString["userid"] != "" && Request.QueryString.Count == 1)
        {
            if (Int32.TryParse(UrlRewritingVM.Decrypt(Request.QueryString["userid"]), out userid))
            {
                userid = Convert.ToInt32(commonclass.GetSafeIDFromURL(UrlRewritingVM.Decrypt(Request.QueryString["userid"].ToString())));
                DataTable dtuser = classaddress.getuserbyindexid(userid);
                if (dtuser.Rows.Count > 0)
                {
                    username = dtuser.Rows[0]["username"].ToString();
                }
                else
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
                }
            }
            else
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
            }
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
        }
        user = classreviews.getuserbyindexid(userid);
        DataSet ds = classreviews.getuserreviewdetails1(userid, PageIndex, PageSize1, out RecordCount1);
        DataTable dt = ds.Tables[0];
       
        if (dt.Rows.Count > 0)
        {
            lblmsg1.Visible = false;
            pnlmylike.Visible = true;
            rptuserreview.DataSource = dt;
            rptuserreview.DataBind();
            if (rptuserreview.Items.Count > 0)
            {
                for (int j = 0; j <= rptuserreview.Items.Count - 1; j++)
                {
                    Rating Rating1 = (Rating)rptuserreview.Items[j].FindControl("Rating1");
                    UpdatePanel upd = (UpdatePanel)rptuserreview.Items[j].FindControl("UpdatePanel1");
                    upd.Attributes.Add("class","updmovieinfo");
                    Label rating = (Label)rptuserreview.Items[j].FindControl("lblrating");
                    LinkButton lnkcomment = (LinkButton)rptuserreview.Items[j].FindControl("lnkcomm1");
                    Label reviews = (Label)rptuserreview.Items[j].FindControl("lblreviews");
                    Label lblrevid = (Label)rptuserreview.Items[j].FindControl("lblrevid");
                    Label lbllike = (Label)rptuserreview.Items[j].FindControl("lbllike1");
                    Label lbllikecount = (Label)rptuserreview.Items[j].FindControl("lbllikecount1");
                    LinkButton lnkread = (LinkButton)rptuserreview.Items[j].FindControl("read");
                    if (reviews.Text.Length > 150)
                    {
                        reviews.Text = reviews.Text.Substring(0, 85) + "...";
                        lnkread.Visible = true;
                    }
                    LinkButton lnklike = (LinkButton)rptuserreview.Items[j].FindControl("lnklike1");
                    Rating1.CurrentRating = Convert.ToInt32(dt.Rows[j]["ratingvalue"].ToString());
                    if (Convert.ToInt32(dt.Rows[j]["commentcount"].ToString()) > 0)
                    {
                        lnkcomment.Text = "<i class='fa fa-comment'></i> Comment" + " (" + dt.Rows[j]["commentcount"].ToString() + ")";
                    }
                    else
                    {
                        lnkcomment.Text = "<i class='fa fa-comment'></i> Comment";
                    }

                    string liketext = "", followtext = "";
                    DataTable like;
                    if (Page.User.Identity.IsAuthenticated)
                    {

                        like = classreviews.getreviewbyuserandreviewid(Convert.ToInt32(lblrevid.Text), Page.User.Identity.Name.ToString().Trim());
                        if (like.Rows.Count > 0)
                        {
                            liketext = like.Rows[0]["likeflag"].ToString();
                            if (like.Rows[0]["followflag"].ToString() == "Following")
                            {
                                followtext = "Unfollow";
                            }
                            else
                            {
                                followtext = "Follow";
                            }
                        }
                    }
                    else
                    {
                        liketext = "Like";
                        followtext = "Follow";
                        string strur = ReturnUrl("sitepathmain")+"user/"+userid;
                        lnklike.PostBackUrl = ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + strur;
                    }
                    if (Convert.ToInt32(dt.Rows[j]["likecount"].ToString()) > 0)
                    {
                        if (liketext == "Unlike")
                        {
                            liketext = "<font style='font-weight:400;'>" + liketext + "</font>";
                            lbllike.Text = "<i class='fa fa-thumbs-up' style=''></i> " + liketext;
                            lbllikecount.Text = " (" + dt.Rows[j]["likecount"].ToString() + ")";
                        }
                        else
                        {
                            lbllike.Text = "<i class='fa fa-thumbs-up'></i> " + liketext;
                            lbllikecount.Text = " (" + dt.Rows[j]["likecount"].ToString() + ")";
                        }
                    }
                    else
                    {
                        lbllike.Text = "<i class='fa fa-thumbs-up'></i> Like";
                        lbllikecount.Visible = false;
                    }

                }
            }
        }
        else
        {
	        if(Page.User.Identity.IsAuthenticated)
	        {
                if (Page.User.Identity.Name == username.ToString().Trim())
		        {
                    lblmsg1.Text = "You haven't Rated any post yet.";
			        //divmsg.InnerHtml="You haven't Rated any post yet.";
		        }
		        else
		        {
                    lblmsg1.Text = "User haven't Rated any post yet";
			        //divmsg.InnerHtml=""+user.Rows[0]["fullname"].ToString()+" haven't Rated any post yet";
		        }
            }
  	        else
	        {
                lblmsg1.Text = "User haven't Rated any post yet";
		        //divmsg.InnerHtml=""+user.Rows[0]["fullname"].ToString()+" haven't Rated any post yet";
	        }
                //divmsg.Visible = true;
                lblmsg1.Visible = true;
                pnlmylike.Visible = false;
                // profilediv.Visible = true;
        }
        if (maxpg1 == 1)
        {
            this.PopulatePager1(RecordCount1, PageIndex);
        }
        else
        {
            this.maxpager1(RecordCount1, PageIndex, maxpg1);
        }
    }
    protected void rptuserreview_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        bool iserror = false;
        if (Request.QueryString["userid"] != null)
        {
            userid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["userid"].ToString()));
        }
        if (e.CommandName == "cmdlike")
        {
          if (Page.User.Identity.IsAuthenticated)
          {
                    iserror = classreviews.insertdeletelikes(Page.User.Identity.Name, Convert.ToInt32(e.CommandArgument.ToString()));
                    if (!iserror)
                    {
                        loadgrid(pgi1);
                    }
          }
        }
        if (e.CommandName == "cmdcomment")
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "comments/" + UrlRewritingVM.Encrypt(e.CommandArgument.ToString()));
            }
        if(e.CommandName=="cmdread")
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "comments/" + UrlRewritingVM.Encrypt(e.CommandArgument.ToString()));
        }
    }

    // Wall code
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
            if (imagepath.ToString().Length > 0 && imagepath.ToString() != "noimage2.png" && imagepath.ToString() != "noimage2.gif" && imagepath.ToString() != "N/A" && imagepath.ToString() != "n/a" && imagepath.ToString() != "video.jpg")
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
                if (videocode.ToString().Length > 0 && videocode.ToString() != "" && videocode.ToString() != "N/A" && videocode.ToString() != "n/a")
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
                pnlvideo.Visible = false;
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

    public string getdocext(object ext)
    {
        string file = "";
        string ext1 = "";
        string rtnval = "";
        try
        {
            file = ext.ToString();
            if (file != "" && file.ToString() != "N/A" && file.ToString() != "n/a")
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

    protected void rptwall_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        bool iserror = false;
        try
        {
            if (e.CommandName.ToLower() == "cmdlike")
            {
                if (Page.User.Identity.IsAuthenticated)
                {
                    iserror = classreviews.insertdeletepostlikes(username, Convert.ToInt32(e.CommandArgument.ToString()));
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
                    iserror = classreviews.insertdeletepostdislikes(Page.User.Identity.Name, Convert.ToInt32(e.CommandArgument.ToString()));
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

        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
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

    public string getsubstr(object shortdesc)
    {
        string str1 = "";
        string str2 = "";
        try
        {
            str1 = Convert.ToString(shortdesc);
            str2 = Regex.Replace(str1, "<.*?>", String.Empty);
            str2 = str2.Substring(0, 100);
            str2 = str2 + "....";
            return str2;
        }
        catch (Exception ex)
        {
            return str1;
        }
    }

    public string productUrlrewriting(object productname, object productid)
    {
        string strurl = "";
        try
        {
            strurl = UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(productid.ToString()), "PDID");
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }

    public void bindwall(int PageIndex)
    {
        DataSet dswall = new DataSet();
        if (Request.QueryString["userid"] != null && Request.QueryString["userid"] != "")
        {
            if (Int32.TryParse(UrlRewritingVM.Decrypt(Request.QueryString["userid"]), out userid))
            {
                userid = Convert.ToInt32(commonclass.GetSafeIDFromURL(UrlRewritingVM.Decrypt(Request.QueryString["userid"].ToString())));
                DataTable dtuser = classaddress.getuserbyindexid(userid);
                if (dtuser.Rows.Count > 0)
                {
                    username = dtuser.Rows[0]["username"].ToString();
                }
                else
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
                }
            }
            else
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
            }
        }
        
        title="";
        catid = commonclass.GetSafeIDFromURL(catid);
        dswall = classproductimage.allpostbyuser2(username, title, catid, PageIndex, PageSize, out RecordCount);
        if(dswall.Tables.Count >0)
        {
            if(dswall.Tables[0].Rows.Count >0)
            {
                rptwall.DataSource = dswall.Tables[0];
                rptwall.DataBind();
                if (rptwall.Items.Count > 0)
                {
                    pnlmypst.Visible = true;
                    lblmsg.Visible = false;
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
                            DataTable dtlike = classreviews.getlikedetailsbyuserpostid(username, Convert.ToInt32(lblpid.Text));
                            if (dtlike.Rows.Count > 0)
                            {
                                //like Module
                                if (dtlike.Rows[0]["likeflag"].ToString().Trim() == "Unlike")
                                {
                                    liketext = "<font style='font-weight:400;color:#3199cc !important;'>" + "Like" + "</font>";
                                    lbllike.Text = "<i class='fa fa-thumbs-up' style='color:#3199cc !important;'></i> " + liketext;
                                    if (Convert.ToInt32(dtlike.Rows[0]["likecount"].ToString().Trim()) > 0)
                                    {
                                        lbllikecount.ForeColor = System.Drawing.Color.FromName("#3199cc");
                                        lbllikecount.Text = " (" + dtlike.Rows[0]["likecount"].ToString().Trim() + ")";
                                    }
                                    else
                                    {
                                        lbllikecount.Text = "";
                                    }
                                }
                                else
                                {
                                    lbllike.Text = "<i class='fa fa-thumbs-up'></i> " + dtlike.Rows[0]["likeflag"].ToString().Trim();
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
                                    liketext = "<font style='font-weight:400;color:#3199cc !important;'>" + "Dislike" + "</font>";
                                    lbldislike.Text = "<i class='fa fa-thumbs-down' style='color:#3199cc !important;'></i> " + liketext;
                                    if (Convert.ToInt32(dtlike.Rows[0]["dislikecount"].ToString().Trim()) > 0)
                                    {
                                        lbldislikecount.ForeColor = System.Drawing.Color.FromName("#3199cc");
                                        lbldislikecount.Text = " (" + dtlike.Rows[0]["dislikecount"].ToString().Trim() + ")";
                                    }
                                    else
                                    {
                                        lbldislikecount.Text = "";
                                    }
                                }
                                else
                                {
                                    lbldislike.Text = "<i class='fa fa-thumbs-down'></i> " + dtlike.Rows[0]["dislikeflag"].ToString().Trim();
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
                                    lnkcomment.Text = "<i class='fa fa-comment'></i> Comment" + " (" + dtlike.Rows[0]["commentcount"].ToString() + ")";
                                }
                                else
                                {
                                    lnkcomment.Text = "<i class='fa fa-comment'></i> Comment";
                                }
                            }
                            else
                            {
                                lbllike.Text = "<i class='fa fa-thumbs-up'></i> Like";
                                lbldislike.Text = "<i class='fa fa-thumbs-down'></i> Dislike";
                                lbllikecount.Visible = false;
                                lbldislikecount.Visible = false;
                                lnkcomment.Text = "<i class='fa fa-comment'></i> Comment";
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
                                    liketext = "<font style='font-weight:400;'>" + dtlike.Rows[0]["likeflag"].ToString().Trim() + "</font>";
                                    lbllike.Text = "<i class='fa fa-thumbs-up' style=''></i> " + liketext;
                                    if (Convert.ToInt32(dtlike.Rows[0]["likecount"].ToString().Trim()) > 0)
                                    {
                                        lbllikecount.Text = " (" + dtlike.Rows[0]["likecount"].ToString().Trim() + ")";
                                    }
                                    else
                                    {
                                        lbllikecount.Text = "";
                                    }
                                }
                                else
                                {
                                    lbllike.Text = "<i class='fa fa-thumbs-up'></i> " + dtlike.Rows[0]["likeflag"].ToString().Trim();
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
                                    liketext = "<font style='font-weight:400;'>" + "Dislike" + "</font>";
                                    lbldislike.Text = "<i class='fa fa-thumbs-down' style=''></i> " + liketext;
                                    if (Convert.ToInt32(dtlike.Rows[0]["dislikecount"].ToString().Trim()) > 0)
                                    {
                                        lbldislikecount.Text = " (" + dtlike.Rows[0]["dislikecount"].ToString().Trim() + ")";
                                    }
                                    else
                                    {
                                        lbldislikecount.Text = "";
                                    }
                                }
                                else
                                {
                                    lbldislike.Text = "<i class='fa fa-thumbs-down'></i> " + dtlike.Rows[0]["dislikeflag"].ToString().Trim();
                                    if (Convert.ToInt32(dtlike.Rows[0]["dislikecount"].ToString().Trim()) > 0)
                                    {
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
                                    lnkcomment.Text = "<i class='fa fa-comment'></i> Comment" + " (" + dtlike.Rows[0]["commentcount"].ToString() + ")";
                                }
                                else
                                {
                                    lnkcomment.Text = "<i class='fa fa-comment'></i> Comment";
                                }
                                lnklike.PostBackUrl = ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + Request.RawUrl.ToString();
                            }
                            else
                            {
                                lbllike.Text = "<i class='fa fa-thumbs-up'></i> Like";
                                lbldislike.Text = "<i class='fa fa-thumbs-down'></i> Dislike";
                                lbllikecount.Visible = false;
                                lbldislikecount.Visible = false;
                                lnkcomment.Text = "<i class='fa fa-comment'></i> Comment";
                            }
                        }

                    }
                }
                else
                {
                    if (Page.User.Identity.Name.ToString().Trim() == username.ToString().Trim())
                    {
                        lblmsg.Text = "You haven't uploded any post under this category";
                    }
                    else
                    {
                        lblmsg.Text = "No Post uploded by this user";
                    }
                    pnlmypst.Visible = false;
                    lblmsg.Visible = true;
                    
                }
                if (maxpg == 1)
                {
                    this.PopulatePager(RecordCount, PageIndex);
                }
                else
                {
                    this.maxpager(RecordCount, PageIndex, maxpg);
                }
            }
            else
            {
                if (Page.User.Identity.Name == username.ToString().Trim())
                {
                    //divmsg.InnerHtml = "You haven't Rated any post yet.";
                    lblmsg.Text = "You haven't uploded any post under this category";
                }
                else
                {
                    //divmsg.InnerHtml = user.Rows[0]["fullname"].ToString() + " haven't Rated any post yet";
                    lblmsg.Text = "No Post uploded by this user";
                }
                pnlmypst.Visible = false;
                lblmsg.Visible = true;
                lblmsg.Text = "No Post Uploded by this User";
            }
        }
    }

    private void PopulatePager(int recordCount, int currentPage)
    {
        lnknxt.Visible = false;
        lnkprev.Visible = false;
        double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
        pageCount = (int)Math.Ceiling(dblPageCount);
        dblmainpg = (double)((double)pageCount / Convert.ToDouble(maxpage));
        maxpgcount = (int)Math.Ceiling(dblmainpg);
        int pgno = 0;
        List<ListItem> pages = new List<ListItem>();
        if (pageCount > 0)
        {
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
            rptPager.Visible = false;
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
        rptPager.DataSource = pages;
        rptPager.DataBind();
    }

    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        pgi = pageIndex;
        this.bindwall(pgi);
    }

    public void displaycat()
    {
        DataTable dt = classcategory.getallcatsbyviewstatus();
        ddlposttype.DataSource = dt;
        ddlposttype.Items.Clear();
        ddlposttype.DataTextField = "categoryname";
        ddlposttype.DataValueField = "categoryid";
        ddlposttype.DataBind();
        ListItem item = new ListItem("Choose Post Type", "0",true);
        ddlposttype.Items.Insert(0, item);
    }

    protected void ddlposttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        catid = "";
        if (Convert.ToInt32(ddlposttype.SelectedItem.Value) == 0)
        {
            catid = "";
        }
        else
        {
            catid = ddlposttype.SelectedItem.Value.ToString();
        }
        maxpg = 1;
        bindwall(1);
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

    private void PopulatePager1(int recordCount, int currentPage)
    {
        lnknxt1.Visible = false;
        lnkprev1.Visible = false;
        double dblPageCount1 = (double)((decimal)recordCount / Convert.ToDecimal(PageSize1));
        pageCount1 = (int)Math.Ceiling(dblPageCount1);
        dblmainpg1 = (double)((double)pageCount1 / Convert.ToDouble(maxpage));
        maxpgcount1 = (int)Math.Ceiling(dblmainpg1);
        int pgno = 0;
        List<ListItem> pages = new List<ListItem>();
        if (pageCount1 > 0)
        {

            for (int i = 1; i <= maxpage; i++)
            {
                if (pgno == 0)
                {
                    if (maxpg1 * maxpage < pageCount1)
                    {
                        lnknxt1.Visible = true;
                    }
                    pgno = (maxpg1 * maxpage) - (maxpage - 1);
                    pages.Add(new ListItem(pgno.ToString(), pgno.ToString(), pgno != currentPage));
                }
                else
                {
                    pgno = pgno + 1;
                    if (pgno <= pageCount1)
                    {
                        pages.Add(new ListItem(pgno.ToString(), pgno.ToString(), pgno != currentPage));
                    }
                    else
                    {
                        break;
                    }
                }
                //pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
            }
            if (pageCount1 == 1)
            {
                rptPager1.Visible = false;
            }
            else
            {
                rptPager1.Visible = true;
            }
        }
        else
        {
            rptPager1.Visible = true;
        }
        rptPager1.DataSource = pages;
        rptPager1.DataBind();
    }

    private void maxpager1(int recordCount, int currentPage, int mpg)
    {
        rptPager1.Dispose();
        lnkprev1.Visible = false;
        lnknxt1.Visible = false;
        rptPager1.Visible = true;
        if (maxpg1 != 1)
        {
            lnkprev1.Visible = true;
        }
        int pgno = 0;
        List<ListItem> pages = new List<ListItem>();
        for (int i = 1; i <= maxpage; i++)
        {
            if (pgno == 0)
            {
                if (maxpg1 * maxpage < pageCount1)
                {
                    lnknxt1.Visible = true;
                }
                pgno = (maxpg1 * maxpage) - (maxpage - 1);
                pages.Add(new ListItem(pgno.ToString(), pgno.ToString(), pgno != currentPage));
            }
            else
            {
                pgno = pgno + 1;
                if (pgno <= pageCount1)
                {
                    pages.Add(new ListItem(pgno.ToString(), pgno.ToString(), pgno != currentPage));
                }
                else
                {
                    break;
                }
            }
        }
        rptPager1.DataSource = pages;
        rptPager1.DataBind();
    }

    protected void lnkprev1_Click(object sender, EventArgs e)
    {
        int pgno;
        maxpg1 = maxpg1 - 1;
        pgno = (maxpg1 * maxpage) - (maxpage - 1);
        pgi1 = pgno;
        loadgrid(pgno);
    }
    protected void lnknxt1_Click(object sender, EventArgs e)
    {
        int pgno;
        maxpg1 = maxpg1 + 1;
        pgno = (maxpg1 * maxpage) - (maxpage - 1);
        pgi1 = pgno;
        loadgrid(pgno);
    }
    protected void lnkPage1_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        pgi1 = pageIndex;
        this.loadgrid(pgi1);
    }
    public string getproductimage1(object imagepath)
    {
        string strurl = "";
        try
        {
            if (imagepath.ToString().Length > 0 && imagepath.ToString() != "noimage2.png" && imagepath.ToString() != "noimage2.gif" && imagepath.ToString() != null && imagepath.ToString() != "N/A" && imagepath.ToString() != "n/a")
            {
                string fileName = Request.PhysicalApplicationPath + "\\images\\450x300\\" + imagepath.ToString();
                if (File.Exists(fileName))
                {
                    pnlimg1.Visible = true;
                    pnlvideo1.Visible = false;
                    strurl = "<img src='" + ReturnUrl("sitepathmain") + "images/450x300/" + imagepath.ToString() + "' />";
                }
                else
                {
                    strurl = "<img src='" + ConfigurationManager.AppSettings["sitepathadmin"]+ "images/450x300/" + imagepath.ToString() + "' />";
                }
            }
            else
            {
                pnlimg1.Visible = false;
                strurl = "";
            }
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
    public string getpostvideo1(object videocode, object videourl)
    {
        string strurl = "";
        try
        {
            if (videourl.ToString().Length > 0 || videourl.ToString() != "" || videocode.ToString().Length > 0 || videocode.ToString() != "")
            {
                if (videocode.ToString().Length > 0 && videocode.ToString() != "" && videocode.ToString() != "N/A" && videocode.ToString() != "n/a")
                {
                    pnlimg1.Visible = false;
                    pnlvideo1.Visible = true;
                    strurl = videocode.ToString();
                }
                else
                {
                    if (videourl.ToString() != "N/A" && videourl.ToString() != "n/a")
                    {
                        pnlimg1.Visible = false;
                        pnlvideo1.Visible = true;
                        HttpCookie cookie = HttpContext.Current.Request.Cookies["internet"];
                        if(cookie!=null)
                        {
                            if (cookie.Value.ToString() == "true")
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
                        pnlvideo1.Visible = false;
                        strurl = "";
                    }
                }
            }
            else
            {
                pnlvideo1.Visible = false;
                strurl = "";
            }
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
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
   
    protected void btnhome_Click(object sender, EventArgs e)
    {
    }
    protected void btnmsg_Click(object sender, EventArgs e)
    {
        int topid = 0, topid1 = 0, i, j, catid1=0;
        lblmsgvalue.Visible = true;
        DataTable dt = classcategory.getCategoryByName("message");
        if (dt.Rows.Count > 0)
        {
            catid1 = Convert.ToInt32(dt.Rows[0]["categoryid"]);
        }
        DataTable ds = classproduct.gettopidproduct();
        if(ds.Rows.Count > 0)
        {
            topid = Convert.ToInt32(ds.Rows[0][0]);
        }
        i = Convert.ToInt32(classmessageboard.insertMsgboard(txttitle.Text.ToString(), (Convert.ToInt32(topid) + 1).ToString(), txtmsg.Text.ToString(), 'S', 'Y', Page.User.Identity.Name.ToString()));
        if (i > 0)
        {
            DataTable dttop = classproduct.gettopidproduct();
            if(dttop.Rows.Count > 0)
            {
                topid1 = Convert.ToInt32(dttop.Rows[0][0]);
            }
            if(topid1 > 0 && catid1 > 0)
            {
               j = Convert.ToInt32(classproduct.addcategorytoproduct((Convert.ToInt32(topid1)), catid1));
                if(j > 0)
                {
                    j = 0;
                    j = Convert.ToInt32(classmessageboard.insertTaguser(Convert.ToInt32(topid1), username));
                    if(j > 0)
                    {
                        lblmsgvalue.Text = "Message posted successfully.";
                        lblmsgvalue.ForeColor = System.Drawing.Color.Green;

                    }
                    else
                    {
                        lblmsgvalue.Text = "Failed to upload Message.";
                        lblmsgvalue.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblmsgvalue.Text = "Failed to upload Message.";
                    lblmsgvalue.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        else
        {
            lblmsgvalue.Text = "Failed to upload Message.";
            lblmsgvalue.ForeColor = System.Drawing.Color.Red;
        }
        txttitle.Text = "";
        txtmsg.Text = "";
    }
    public string productUrlrewriting2(object cattype, object productname, object productid)
    {
        string strurl = "";
        string cat = "";
        try
        {
            cat = cattype.ToString();
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
}
