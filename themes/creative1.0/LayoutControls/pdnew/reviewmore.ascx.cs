using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using AjaxControlToolkit;
using System.IO;


public partial class themes_creative1_LayoutControls_pdnew_reviewmore : System.Web.UI.UserControl
{
    private static int productid;
    static int ratevalue;
    string username = "";
    string strurl = "";
    string strprdnm = "";

     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
        {
            productid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["p"].ToString()));
        }

        //try
        //{
            moviefollower();
        //}
        //catch (Exception ex)
        //{

        //}
    }

    public void moviefollower()
    {
        if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
        {
            productid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["p"].ToString()));
        }
        if (Page.User.Identity.IsAuthenticated)
        {
            DataTable dtm = classreviews.getratingdetails(Page.User.Identity.Name.ToString().Trim(), productid);
            lblproductname.Text = dtm.Rows[0]["productname"].ToString();
            lblyear.Text = "&copy; "+dtm.Rows[0]["product_year"].ToString();
            DataTable dir = classdirector.getalldirectorbyid(Convert.ToInt32(productid));
            if (dir.Rows.Count > 0)
            {
                lnkdirector.HRef = ReturnUrl("sitepathmain") + "ad/" + dir.Rows[0]["directorid"].ToString();
                lbldirectorname.Text = dir.Rows[0]["directorname"].ToString();
            }
            else
            {
                director.Visible = false;
            }
            if (dtm.Rows.Count > 0)
            {
                rptmoviereview.DataSource = dtm;
                rptmoviereview.DataBind();
                DataTable dt3;
                for (int i = 0; i <= rptmoviereview.Items.Count - 1; i++)
                {
                    Rating Rating1 = (Rating)rptmoviereview.Items[i].FindControl("Rating1");
                    Label rating = (Label)rptmoviereview.Items[i].FindControl("lblratval");
                    HiddenField hd = (HiddenField)rptmoviereview.Items[i].FindControl("hdvalue");
                    Rating1.CurrentRating = Convert.ToInt32(hd.Value);
                    Image img = (Image)rptmoviereview.Items[i].FindControl("imgprofile");
                    if (dtm.Rows[i]["profilephoto"].ToString() != "")
                    {
                        if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profile55x55/" + dtm.Rows[i]["profilephoto"].ToString())))
                        {
                            img.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/" + dtm.Rows[i]["profilephoto"].ToString();
                        }
                        else
                        {
                            img.ImageUrl = "https://graph.facebook.com/" + dtm.Rows[i]["profilephoto"].ToString() + "/picture?type=large";
                        }
                    }
                    else
                    {
                        img.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/noimage1.jpg";
                    }
                    LinkButton lnkfollow = (LinkButton)rptmoviereview.Items[i].FindControl("lnkfollow");
                    LinkButton lnkread = (LinkButton)rptmoviereview.Items[i].FindControl("read");
                    LinkButton lnkcomm = (LinkButton)rptmoviereview.Items[i].FindControl("lnkcomm");
                    LinkButton lnkusername = (LinkButton)rptmoviereview.Items[i].FindControl("lnkusername");
                    LinkButton lnklike = (LinkButton)rptmoviereview.Items[i].FindControl("lnklike");
                    Label lblcnt = (Label)rptmoviereview.Items[i].FindControl("lblcnt");
                    Label lblrate = (Label)rptmoviereview.Items[i].FindControl("lblratval");
                    Label lblreviews = (Label)rptmoviereview.Items[i].FindControl("lblreviews");
                    Label lblrevid = (Label)rptmoviereview.Items[i].FindControl("lblrevid");

                    Label lblcomment = (Label)rptmoviereview.Items[i].FindControl("lblcommentcount");

                    if (Convert.ToInt32(lblcomment.Text) > 0)
                    {
                        lnkcomm.Text = "<i class='fa fa-comment'></i> Comment (" + lblcomment.Text + ")";
                    }
                    else
                    {
                        lnkcomm.Text = "<i class='fa fa-comment'></i> Comment";
                    }
                    lblrate.Text = ratevalue.ToString();
                    dt3 = classreviews.getfollower(Page.User.Identity.Name, lnkusername.CommandArgument.ToString());
                    if (Page.User.Identity.Name == lnkusername.CommandArgument.ToString())
                    {
                        lnkfollow.Visible = false;
                    }
                    if (dt3.Rows.Count > 0)
                    {
                        lnkfollow.Text = "<i class='fa fa-user-plus'></i> Unfollow";
                    }
                    else
                    {
                        lnkfollow.Text = "<i class='fa fa-user-plus'></i> Follow";
                    }



                    string liketext = "", followtext = "";
                    DataTable like;
                    if (Page.User.Identity.IsAuthenticated)
                    {

                        like = classreviews.getreviewbyuserandreviewid(Convert.ToInt32(lblrevid.Text), Page.User.Identity.Name);
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
                    }
                    if (Convert.ToInt32(lblcnt.Text.ToString().Trim()) > 0)
                    {
                        lnklike.Text = "<i class='fa fa-thumbs-up'></i> " + liketext + " (" + lblcnt.Text.ToString().Trim() + ")";
                    }
                    else
                    {
                        lnklike.Text = "<i class='fa fa-thumbs-up'></i> Like";
                    }

                    
                }
            }
            else
            {
                criticcornerdiv.Visible = false;
            }
        }
        else
        {
            DataTable dtm = classreviews.getratingdetailsbymovieid(productid);
            lblproductname.Text = dtm.Rows[0]["productname"].ToString();
            lblyear.Text = "&copy; " + dtm.Rows[0]["product_year"].ToString();
            DataTable dir = classdirector.getalldirectorbyid(Convert.ToInt32(productid));
            if (dir.Rows.Count > 0)
            {
                lnkdirector.HRef = ReturnUrl("sitepathmain") + "ad/" + dir.Rows[0]["directorid"].ToString();
                lbldirectorname.Text = dir.Rows[0]["directorname"].ToString();
            }
            else
            {
                director.Visible = false;
            }
            if (dtm.Rows.Count > 0)
            {
                rptmoviereview.DataSource = dtm;
                rptmoviereview.DataBind();
                
                for (int i = 0; i <= rptmoviereview.Items.Count - 1; i++)
                {
                    Rating Rating1 = (Rating)rptmoviereview.Items[i].FindControl("Rating1");
                    Label rating = (Label)rptmoviereview.Items[i].FindControl("lblratval");
                    Rating1.CurrentRating = Convert.ToInt32(rating.Text);
                    Image img = (Image)rptmoviereview.Items[i].FindControl("imgprofile");
                    if (dtm.Rows[i]["profilephoto"].ToString() != "")
                    {
                        if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profilephoto/" + dtm.Rows[i]["profilephoto"].ToString())))
                        {
                            img.ImageUrl = ReturnUrl("sitepath") + "images/profilephoto/" + dtm.Rows[i]["profilephoto"].ToString();
                        }
                        else
                        {
                            img.ImageUrl = "https://graph.facebook.com/" + dtm.Rows[i]["profilephoto"].ToString() + "/picture?type=large";
                        }
                    }
                    else
                    {
                        img.ImageUrl = ReturnUrl("sitepath") + "images/noprofile.jpg";
                    }
                    
                    LinkButton lnkfollow = (LinkButton)rptmoviereview.Items[i].FindControl("lnkfollow");
                    LinkButton lnkcomm = (LinkButton)rptmoviereview.Items[i].FindControl("lnkcomm");
                    LinkButton lnkusername = (LinkButton)rptmoviereview.Items[i].FindControl("lnkusername");
                    LinkButton lnklike = (LinkButton)rptmoviereview.Items[i].FindControl("lnklike");

                    string strur = ReturnUrl("sitepathmain") + "reviewmore/" + productid;
                    lnklike.PostBackUrl = ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + strur;
                    lnkfollow.PostBackUrl = ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + strur;
                    Label lblcnt = (Label)rptmoviereview.Items[i].FindControl("lblcnt");
                    Label lblrate = (Label)rptmoviereview.Items[i].FindControl("lblratval");
                    Label lblcomment = (Label)rptmoviereview.Items[i].FindControl("lblcommentcount");
                    Label lblreviews = (Label)rptmoviereview.Items[i].FindControl("lblreviews");
                    LinkButton lnkread = (LinkButton)rptmoviereview.Items[i].FindControl("read");
                   
                    if (Convert.ToInt32(lblcomment.Text) > 0)
                    {
                        lnkcomm.Text = "<i class='fa fa-comment'></i> Comment (" + lblcomment.Text + ")";
                    }
                    else
                    {
                        lnkcomm.Text = "<i class='fa fa-comment'></i> Comment";
                    }
                    lblrate.Text = ratevalue.ToString();
                    lnkfollow.Text = "<i class='fa fa-user-plus'></i> Follow";
                    if (lblcnt.Text.ToString().Trim() == "0")
                    {
                        lnklike.Text = "<i class='fa fa-thumbs-up'></i> Like";
                    }
                    else
                    {
                        lnklike.Text = "<i class='fa fa-thumbs-up'></i> Like (" + lblcnt.Text + ")";
                    }

                }
            }
            else
            {
                criticcornerdiv.Visible = false;
            }
        }

    }

    protected void rptmoviereview_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        bool iserror;
        string returnuerl = "";
        if (e.CommandName == "follow")
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                iserror = classreviews.insertdeletefollowing(Page.User.Identity.Name, e.CommandArgument.ToString());
                if (!iserror)
                {
                    moviefollower();
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
        if (e.CommandName == "cmdcomment")
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "comments/" + UrlRewritingVM.Encrypt(e.CommandArgument.ToString()));
        }
        if (e.CommandName == "cmdlike")
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                iserror = classreviews.insertdeletelikes(Page.User.Identity.Name, Convert.ToInt32(e.CommandArgument.ToString()));
                if (!iserror)
                {
                    moviefollower();
                }
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
        Utilities.SendMail(mailfrom, mailto, follower + " Followed you on my Intranet.com as of " + timespan, body);
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
