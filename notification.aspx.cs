using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Xml;
using System.Text;
using System.IO;
using System.Data.Common;
using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;

public partial class newsdetails : System.Web.UI.Page
{
    private int newsid = 0, ID;
    public static int PageSize = 20, RecordCount;
    public static int pgi=1;
    public static string icon="";

    public static int pageCount;
    public static int maxpage = 5;
    public static double dblmainpg;
    public static int maxpgcount;
    public static int maxpg = 1;

    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;}
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Page.User.Identity.IsAuthenticated== true)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != "" && Request.QueryString["id"] != null)
                {
                    if (Int32.TryParse(UrlRewritingVM.Decrypt(Request.QueryString["id"]), out ID))
                    {
                        newsid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["id"].ToString()));
                    }
                }
                loadgrid(pgi);
            }
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + Request.RawUrl);
        }
    }
    public void loadgrid(int PageIndex)
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            DataTable user = classreviews.getuseridbyemail(Page.User.Identity.Name.ToString().Trim());
            if(user.Rows.Count > 0)
            {
                DataSet dt2 = classreviews.getnotificationall1(user.Rows[0]["username"].ToString().Trim(), PageIndex, PageSize, out RecordCount);
                if(dt2.Tables.Count > 0)
                {
                    if (dt2.Tables[0].Rows.Count > 0)
                    {
                        pnlnews.Visible = true;
                        rptnotification.DataSource = dt2;
                        rptnotification.DataBind();
                        int count1 = dt2.Tables[0].Rows.Count;
                        if (rptnotification.Items.Count > 0)
                        {
                            lblmsg.Visible = false;
                            for (int i = 0; i < rptnotification.Items.Count; i++)
                            {
                                Label lbltxtmsg = (Label)rptnotification.Items[i].FindControl("lbltxtmsg");
                                Label lbldate = (Label)rptnotification.Items[i].FindControl("lbldate");
                                Image imgprofile = (Image)rptnotification.Items[i].FindControl("imgprofile");
                                if (dt2.Tables[0].Rows[i]["profilephoto"].ToString() != "" && dt2.Tables[0].Rows[i]["profilephoto"].ToString() != null)
                                {
                                    if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profilephoto/" + dt2.Tables[0].Rows[i]["profilephoto"].ToString())))
                                    {
                                        imgprofile.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/" + dt2.Tables[0].Rows[i]["profilephoto"].ToString();
                                    }
                                }
                                else
                                {
                                    imgprofile.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/noimage1.png";
                                }

                                if (dt2.Tables[0].Rows[i]["eventflag"].ToString() == "follow")
                                {
                                    DataTable fdt = classfollower.followeduserdetail(Convert.ToInt32(dt2.Tables[0].Rows[i]["productid"]));
                                    if (fdt.Rows.Count > 0)
                                    {
                                        lbltxtmsg.Visible = true;
                                        if (fdt.Rows[0]["username"].ToString() == Page.User.Identity.Name.ToString())
                                        {
                                            lbltxtmsg.Text = "" + "You";
                                        }
                                        else
                                        {
                                            lbltxtmsg.Text = "" + fdt.Rows[0]["fullname"].ToString();
                                        }
                                    }
                                    else
                                    {
                                        lbltxtmsg.Text = "";
                                    }
                                }
                                else
                                {
                                    lbltxtmsg.Visible = false;
                                }
                                DateTime dat = Convert.ToDateTime(dt2.Tables[0].Rows[i]["eventdate"].ToString());
                                lbldate.Text = getdate(dat);
                            }
                        }
                        alter();
                    }
                    else
                    {
                        pnlnews.Visible = false;
                        lblmsg.Text = "No notification found.";
                        lblmsg.Visible = true;
                    }
                    //this.PopulatePager(RecordCount, PageIndex);
                }
            }
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
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        pgi = pageIndex;
        this.loadgrid(pageIndex);
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
    public string geticons(object i)
    {
        string strurl = "";
        try
        {
            if (i.ToString() == "follow")
            {
                strurl = strurl + "<div class='like-o-thumbs-up'><i class='fa fa-user-plus'></i></div>";
            }
            else if (i.ToString() == "like" || i.ToString() == "reviewlike")
            {
                strurl = strurl + "<div class='like-o-thumbs-up'><i class='fa fa-thumbs-up'></i></div>";
            }
            else if (i.ToString() == "dislike")
            {
                strurl = strurl + "<div class='like-o-thumbs-up'><i class='fa fa-thumbs-down'></i></div>";
            }
            else if (i.ToString() == "comment" || i.ToString() == "review")
            {
                strurl = strurl + "<div class='like-o-thumbs-up'><i class='fa fa-comment'></i></div>";
            }
            else if (i.ToString() == "post")
            {
                strurl = strurl + "<div class='like-o-thumbs-up'><i class='fa fa-clipboard'></i></div>";
            }
            else if (i.ToString() == "followerpost")
            {
                strurl = strurl + "<div class='like-o-thumbs-up'><i class='fa fa-user-plus'></i></div>";
            }
            else if (i.ToString() == "survey")
            {
                strurl = strurl + "<div class='like-o-thumbs-up'><i class='fa fa-clipboard'></i></div>";
            }
            else if (i.ToString() == "grouppost")
            {
                strurl = strurl + "<div class='like-o-thumbs-up'><i class='fa fa-users'></i></div>";
            }
            else if (i.ToString() == "favourite")
            {
                strurl = strurl + "<div class='like-o-thumbs-up'><i class='fa fa-heart'></i></div>";
            }
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
    public void alter()
    {
        if (rptnotification.Items.Count > 0)
        {
            for (int i = 0; i < rptnotification.Items.Count; i++)
            {
                Label indexid = (Label)rptnotification.Items[i].FindControl("indexid");
                LinkButton lnkread = (LinkButton)rptnotification.Items[i].FindControl("lnkread");
                string liketext = "";
                if (Page.User.Identity.IsAuthenticated)
                {
                    DataTable dtlike = classreviews.getnotificationReadstatus(Convert.ToInt32(indexid.Text.ToString()));
                    if (dtlike.Rows.Count > 0)
                    {
                        if (dtlike.Rows[0]["readflag"].ToString().Trim() == "R" || dtlike.Rows[0]["readflag"].ToString().Trim() == "D")
                        {
                            lnkread.Text = "<i class='fa fa-check-circle-o' style='color:#338EC9 !important;float:right;'></i> " + liketext;
                            lnkread.ToolTip = "Read";
                        }
                        else
                        {
                            lnkread.Text = "<i class='fa fa-check-circle-o' style='color:#646464 !important;float:right;'></i> " + liketext;
                            lnkread.ToolTip = "Mark as read";
                        }
                    }
                    else
                    {
                        lnkread.Text = liketext;
                        lnkread.ToolTip = "";
                    }
                }
            }
        }
    }
    public string getuser(object userid)
    {
        string strurl = "";
        try
        {
            strurl = ReturnUrl("sitepathmain") + "profile/" + UrlRewritingVM.Encrypt(userid.ToString());
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
    protected void rptnotification_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToLower() == "cmdread")
            {
                int i = 0;
                if (Page.User.Identity.IsAuthenticated)
                {
                    i = Convert.ToInt32(classreviews.updateNotificationReadStatus(Convert.ToInt32(e.CommandArgument.ToString()), "read"));
                    if (i > 0)
                    {
                        loadgrid(pgi);
                    }
                }
                else
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + Request.RawUrl);
                }
            }
            else
            {
                DataTable dt = classreviews.getnotificationbyid(Convert.ToInt32(e.CommandArgument.ToString()));
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["eventflag"].ToString().Trim() == "follow")
                    {
                        int i = 0;
                        if (Page.User.Identity.IsAuthenticated)
                        {
                            i = Convert.ToInt32(classreviews.updateNotificationReadStatus(Convert.ToInt32(e.CommandArgument.ToString()), ""));
                            if (i > 0)
                            {
                                Response.Redirect(ReturnUrl("sitepathmain") + "profile/" + UrlRewritingVM.Encrypt(dt.Rows[0]["userid"].ToString()));
                            } 
                        }
                        else
                        {
                            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + Request.RawUrl);
                        }
                    }
                    else if (dt.Rows[0]["eventflag"].ToString().Trim() == "like" || dt.Rows[0]["eventflag"].ToString().Trim() == "comment" || dt.Rows[0]["eventflag"].ToString().Trim() == "followerpost" || dt.Rows[0]["eventflag"].ToString().Trim() == "dislike" || dt.Rows[0]["eventflag"].ToString().Trim() == "favourite")
                    {
                        int j = 0;
                        if(Page.User.Identity.IsAuthenticated)
                        {
                            j = Convert.ToInt32(classreviews.updateNotificationReadStatus(Convert.ToInt32(e.CommandArgument.ToString()), ""));
                            if (j > 0)
                            {
                                DataTable ds = classproduct.get_proc_ProductDescription_ProdFeature(Convert.ToInt32(dt.Rows[0]["productid"].ToString().Trim()));
                                if (ds.Rows.Count > 0)
                                {
                                    Response.Redirect(UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(ds.Rows[0]["productid"].ToString()), "PDID"));
                                }
                            }
                        }
                        else
                        {
                            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + Request.RawUrl);
                        }
                    }
                    else if (dt.Rows[0]["eventflag"].ToString().Trim() == "survey")
                    {
                        Response.Redirect(UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(dt.Rows[0]["productid"].ToString().Trim()), "SR"));
                    }
                    else if (dt.Rows[0]["eventflag"].ToString().Trim() == "review" || dt.Rows[0]["eventflag"].ToString().Trim() == "reviewlike")
                    {
                        int j = 0;
                        j = Convert.ToInt32(classreviews.updateNotificationReadStatus(Convert.ToInt32(e.CommandArgument.ToString()), ""));
                        if (j > 0)
                        {
                            Response.Redirect(ReturnUrl("sitepathmain") + "comments/" + UrlRewritingVM.Encrypt(dt.Rows[0]["productid"].ToString().Trim()));  
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
    protected void lnkprev_Click(object sender, EventArgs e)
    {
        int pgno;
        maxpg = maxpg - 1;
        pgno = (maxpg * maxpage) - (maxpage - 1);
        pgi = pgno;
        loadgrid(pgno);
    }
    protected void lnknxt_Click(object sender, EventArgs e)
    {
        int pgno;
        maxpg = maxpg + 1;
        pgno = (maxpg * maxpage) - (maxpage - 1);
        pgi = pgno;
        loadgrid(pgno);
    }
    protected void lnkreadall_Click(object sender, EventArgs e)
    {
        
    }
}