using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public partial class themes_creative1_LayoutControls_profile_following : System.Web.UI.UserControl
{
    public static int userid;
    public string username="";

    public static int PageSize = 12, RecordCount;
    public static int pgi = 1;

    public static int pageCount;
    public static int maxpage = 5;
    public static double dblmainpg;
    public static int maxpgcount;
    public static int maxpg = 1;
    DataSet ds;

    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;}
    protected void Page_Load(object sender, EventArgs e)
    {
        lnknxt.Visible = false;
        lnkprev.Visible = false;
        loadgrid(pgi);
    }
    public void loadgrid(int PageIndex)
    {
        if (Request.QueryString["userid"] != null && Request.QueryString["userid"] != "" && Request.QueryString.Count == 1 && Request.QueryString["userid"].Length == 24)
        {
            if (Int32.TryParse(UrlRewritingVM.Decrypt(Request.QueryString["userid"].ToString().Trim()), out userid))
            {
                userid = Convert.ToInt32(commonclass.GetSafeIDFromURL(UrlRewritingVM.Decrypt(Request.QueryString["userid"].ToString().Trim())));
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
        DataTable user = classreviews.getuserbyindexid(userid);
        if(user.Rows.Count > 0)
        {
            DataTable dt4;
            DataTable dt5, dt, follow;
            ds = classreviews.getuserFollowingAllSearch("", user.Rows[0]["username"].ToString(), "", PageIndex, PageSize, out RecordCount);
            if(ds.Tables.Count >0)
            {
                dt4 = ds.Tables[0];
                dt = classreviews.getfollowinglist(user.Rows[0]["username"].ToString());
                if(dt.Rows.Count > 0)
                {
                    hdfollowing.Value = dt.Rows.Count.ToString();
                }
                else
                {
                     hdfollowing.Value = "0";
                }
                if (dt4.Rows.Count > 0)
                {
                    rptfollowing.DataSource = dt4;
                    rptfollowing.DataBind();

                    if (rptfollowing.Items.Count > 0)
                    {

                        int m = 0;
                        for (int j = 0; j <= rptfollowing.Items.Count - 1; j++)
                        {
                            LinkButton lnkusername = (LinkButton)rptfollowing.Items[j].FindControl("lnkusername");
                            LinkButton lnkfollow = (LinkButton)rptfollowing.Items[j].FindControl("lnkfollow");
                            LinkButton lnkimage = (LinkButton)rptfollowing.Items[j].FindControl("lnkimage");
                            m += 1;
                            UpdatePanel upd = (UpdatePanel)rptfollowing.Items[j].FindControl("UpdatePanel1");
                            upd.Attributes.Add("class", "updfollowing" + " color" + m);
                            if (m == 6)
                            {
                                m = 0;
                            }
                            if (Page.User.Identity.IsAuthenticated)
                            {
                                dt5 = classreviews.getfollower(Page.User.Identity.Name, lnkusername.CommandArgument.ToString());
                                if (dt5.Rows.Count > 0)
                                {
                                    lnkfollow.Text = "<i class='fa fa-user-plus'></i> Unfollow";
                                }
                                else
                                {
                                    lnkfollow.Text = "<i class='fa fa-user-plus'></i> Follow";
                                }
                            }
                            else
                            {
                                lnkfollow.Text = "<i class='fa fa-user-plus'></i> Follow";
                                string strur = ReturnUrl("sitepathmain") + "following/" + userid;
                                lnkfollow.PostBackUrl = ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + strur;
                            }

                            follow = classreviews.getuseridbyemail(lnkusername.CommandArgument.ToString());
                            Image img = (Image)rptfollowing.Items[j].FindControl("imgprofile");
                            if (follow.Rows[0]["profilephoto"].ToString() != "")
                            {
                                if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profilephoto/" + follow.Rows[0]["profilephoto"].ToString())))
                                {
                                    img.ImageUrl = ReturnUrl("sitepath") + "images/profile110x110/" + follow.Rows[0]["profilephoto"].ToString();
                                }
                                else
                                {
                                    img.ImageUrl = "https://graph.facebook.com/" + follow.Rows[0]["profilephoto"].ToString() + "/picture?type=large";
                                }
                            }
                            else
                            {
                                img.ImageUrl = ReturnUrl("sitepath") + "images/profile110x110/noimage1.png";
                            }
                            if (Page.User.Identity.Name == lnkusername.CommandArgument.ToString())
                            {
                                lnkfollow.Enabled = false;
                                lnkfollow.CssClass = "self";
                                lnkfollow.Text = "<i class='fa fa-user-plus'></i> Self";
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
                    else
                    {
                        if (pgi > 1)
                        {
                            if (pageCount == (maxpg * maxpage) - (maxpage - 1))
                            {
                                pageCount = pageCount - 1;
                                maxpg = maxpg - 1;
                                pgi = maxpg * maxpage;
                                loadgrid(pgi);
                            }
                            else
                            {
                                pgi = pgi - 1;
                                loadgrid(pgi);
                            }
                        }
                        else
                        {
                            noRecordMsg(user.Rows[0]["username"].ToString(), user.Rows[0]["fullname"].ToString());
                        }
                    }
                }
                else
                {
                    noRecordMsg(user.Rows[0]["username"].ToString(), user.Rows[0]["fullname"].ToString());
                }
            }
            else
            {
                noRecordMsg(user.Rows[0]["username"].ToString(), user.Rows[0]["fullname"].ToString());
            }
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
        }
    }
    public void noRecordMsg(string uname, string fname)
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            if (Page.User.Identity.Name == uname.ToString().Trim())
            {
                divmsg.InnerHtml = "You haven't followed anyone.";
            }
            else
            {
                divmsg.InnerHtml = "" + fname.Trim() + " haven't followed anyone.";
            }
        }
        else
        {
            divmsg.InnerHtml = "" + fname.Trim() + " haven't followed anyone.";
        }
        divmsg.Visible = true;
        profilediv.Visible = false;
    }

    protected void rptfollowing_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        LinkButton lnkfollow = (LinkButton)e.Item.FindControl("lnkfollow");

        bool iserror = false;

        if (Page.User.Identity.IsAuthenticated)
        {
            if (e.CommandName == "follow")
            {
                iserror = classreviews.insertdeletefollowers(Page.User.Identity.Name, e.CommandArgument.ToString());
                if (!iserror)
                {
                    loadgrid(pgi);
                    if (lnkfollow.Text == "<i class='fa fa-user-plus'></i> Follow")
                        {
                            DataTable notify = classreviews.getemailnotification(e.CommandArgument.ToString());
                            if(notify.Rows.Count>0)
                            {
                                if (notify.Rows[0]["follow"].ToString().Trim() == "Y")
                                {
                                    DataTable dt = classreviews.getuseridbyemail(Page.User.Identity.Name);
                                    if (dt.Rows.Count > 0)
                                    {                              
                                        email(e.CommandArgument.ToString(), dt.Rows[0]["fullname"].ToString(), dt.Rows[0]["indexid"].ToString());
                                    }
                                }
                                else
                                {
                                    DataTable dt = classreviews.getuseridbyemail(Page.User.Identity.Name);
                                    if (dt.Rows.Count > 0)
                                    {
                                        email(e.CommandArgument.ToString(), dt.Rows[0]["fullname"].ToString(), dt.Rows[0]["indexid"].ToString());
                                    }
                                }
                            }
                        }
                }
            }
            if (e.CommandName == "username")
            {
                DataTable dt = classreviews.getuseridbyemail(e.CommandArgument.ToString());
                Response.Redirect(ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(dt.Rows[0]["indexid"].ToString()));
            }
            if (e.CommandName == "cmdimage")
            {
                DataTable dt = classreviews.getuseridbyemail(e.CommandArgument.ToString());
                Response.Redirect(ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(dt.Rows[0]["indexid"].ToString()));
            }
        }
    }

    public void email(string mailto, string follower, string followerid)
    {

        DataTable dt = classreviews.getuseridbyemail(mailto);
        if (dt.Rows.Count > 0)
        {
            username = dt.Rows[0]["fullname"].ToString();
        }
        string imagepath = "http://www.Intranet.com/themes/creative1.0/images/logo.png";
        string body = "<html><body>";
        body += "<div style='width:500px;float:left;min-height:320px;padding:20px;border:1px solid #ccc;'><div style='float:left;width:96%;height:20px;padding:2%;font-family:Arial;font-weight:bold;font-size:16px;'>Dear " + username + ",</div><div style='float: left; width: 96%; height: 20px; padding: 2%; font-family: Arial; font-size: 14px;'><a target='blank' href='" + ReturnUrl("sitepathmain") + "user/" + followerid + "' style='color:#1155CC;'>" + follower + " </a>&nbsp; followed you. Boom!</div><div style='float: left; width: 96%; height: 20px; padding: 2%; font-family: Arial; font-size: 14px;'>To Unsubscribe from this type of notification,<a target='blank' style='text-decoration: none; color: #1155CC;' href='" + ReturnUrl("sitepathmain") + "procs/preference' > click here.</a></div><div style='float: left; width: 96%; height: 30px; padding: 2%; font-family: Arial; font-size: 14px;'>If you have any questions or need further assistance, please contact us at support@Intranet.com.</div><div style='float: left; width: 96%; height: 20px; padding: 2%; font-family: Arial; font-size: 14px;'>Sincerely,</div><div style='float: left; width: 96%; height: 20px; padding: 2%; font-family: Arial; font-size: 14px;'>Team Intranet.com</div><div style='float: left; width: 96%; height: 20px; padding: 2%; font-family: Arial; font-size: 14px;'><a href='www.Intranet.com'><img src='" + imagepath + "' /></a></div></div>";
        body += "</body></html>";
        string mailfrom = creativeconfiguration.adminemail;
        DateTime dat = Convert.ToDateTime(DateTime.Now.ToString());
        string timespan = dat.ToString("HH:mm") + " on " + dat.ToString("dd-MMM-yyyy");
        //Utilities.SendMail(mailfrom, mailto, follower + " Followed you on my Intranet.com as of " + timespan, body);
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
        this.loadgrid(pageIndex);
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
}
