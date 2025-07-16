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

public partial class themes_creative1_LayoutControls_profile_reviewbanner : System.Web.UI.UserControl
{
    public static int reviewid;
    public string username = "";
    public string reviewuser = "";
    public string userid;
    DataTable movie;
    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
    {
        txtImagename1.Focus();
        if (Request.QueryString["reviewid"] != null && Request.QueryString["reviewid"] != "")
        {
            if (Int32.TryParse(UrlRewritingVM.Decrypt(Request.QueryString["reviewid"]), out reviewid))
            {
                reviewid = Convert.ToInt32(commonclass.GetSafeIDFromURL(UrlRewritingVM.Decrypt(Request.QueryString["reviewid"].ToString())));
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
        if(!IsPostBack)
        {
            loaddata();
        }
    }
    public void loaddata()
    {
        //try
        //{
            if (Request.QueryString["reviewid"] != null)
            {
                reviewid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["reviewid"].ToString()));
            }
            movie = classreviews.getreviewdetailsbyreviewid(reviewid);
            if (movie.Rows.Count > 0)
            {
                DataTable pd = classproduct.getuserbypropductid(Convert.ToInt32(movie.Rows[0]["productid"].ToString()));
                if (pd.Rows.Count > 0)
                {
                    lnkdirector.HRef = ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(pd.Rows[0]["indexid"].ToString());
                    lbldirectorname.Text = pd.Rows[0]["fullname"].ToString();
                    DateTime posteddate = new DateTime();
                    posteddate= Convert.ToDateTime(pd.Rows[0]["createdon"].ToString());
                    lblyear.Text = posteddate.ToString("dd MMMM, yyyy");
                    DataTable mulimg = classproduct.GetAllChildData(Convert.ToInt32(movie.Rows[0]["productid"].ToString()));
                    if(mulimg.Rows.Count > 0)
                    {
                        if (mulimg.Rows[0]["bigimage"].ToString() != "" && mulimg.Rows[0]["bigimage"].ToString() != "noimage2.gif" && mulimg.Rows[0]["bigimage"].ToString() != "noimage2.png" && mulimg.Rows[0]["bigimage"].ToString() != "n/a" && mulimg.Rows[0]["bigimage"].ToString() != "N/A")
                        {
                            string fileName = Request.PhysicalApplicationPath + "\\images\\bigproduct\\" + mulimg.Rows[0]["bigimage"].ToString();
                            if (File.Exists(fileName))
                            {
                                coverphoto.Attributes.Add("style", "background-image:url('" + ReturnUrl("sitepathmain") + "images/bigproduct/" + mulimg.Rows[0]["bigimage"].ToString() + "')");
                            }
                            else
                            {
                                coverphoto.Attributes.Add("style", "background-image:url('" + ConfigurationManager.AppSettings["sitepathadmin"]+ "images/bigproduct/" + mulimg.Rows[0]["bigimage"].ToString() + "')");
                            }
                        }
                        else
                        {
                            coverphoto.Attributes.Add("style", "disply:none;");
                            coverphoto.Visible = false;
                        }
                    }
                    else
                    {
                        coverphoto.Attributes.Add("style", "disply:none;");
                        coverphoto.Visible = false;
                    }
                }
                else
                {
                    director.Visible = false;
                }
                string rtnurl = ReturnUrl("sitepathmain") + "comments/" + UrlRewritingVM.Encrypt(reviewid.ToString());
                lnklogin.HRef = ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + rtnurl;
                reviewuser = movie.Rows[0]["username"].ToString();
                lblproductname.Text = movie.Rows[0]["productname"].ToString();
                lnkproduct.HRef = productUrlrewriting(movie.Rows[0]["productname"].ToString(), movie.Rows[0]["productid"].ToString());
                if (movie.Rows[0]["profilephoto"].ToString() != "")
                {
                    if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profilephoto/" + movie.Rows[0]["profilephoto"].ToString())))
                    {
                        imgprofile.Src = ReturnUrl("sitepath") + "images/profile55x55/" + movie.Rows[0]["profilephoto"].ToString();
                    }
                    else
                    {
                        imgprofile.Src = "https://graph.facebook.com/" + movie.Rows[0]["profilephoto"].ToString() + "/picture?type=large";
                    }
                }
                else
                {
                    imgprofile.Src = ReturnUrl("sitepath") + "images/profile55x55/noimage1.png";
                }
                lnkimage.HRef = ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(movie.Rows[0]["indexid"].ToString());
                lnkusername.HRef = ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(movie.Rows[0]["indexid"].ToString());
                lblusername.Text = movie.Rows[0]["fullname"].ToString();
                Rating1.CurrentRating = Convert.ToInt32(movie.Rows[0]["ratingvalue"].ToString());
                DateTime dat = Convert.ToDateTime(movie.Rows[0]["reviewdate"].ToString());
                lbldate.Text = dat.ToString("dd-MMM-yyyy");
                if (Convert.ToInt32(movie.Rows[0]["commentcount"].ToString()) > 0)
                {
                    lnkcomm.Text = "<i class='fa fa-comment'></i> Comment (" + movie.Rows[0]["commentcount"].ToString() + ")";
                }
                else
                {
                    lnkcomm.Text = "<i class='fa fa-comment'></i> Comment";
                }
                string liketext = "", followtext = "";
                DataTable like;
                if (Page.User.Identity.IsAuthenticated)
                {

                    like = classreviews.getreviewbyuserandreviewid(reviewid, Page.User.Identity.Name);
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
                    divmsg.Visible = false;
                }
                else
                {
                    string strur = ReturnUrl("sitepathmain") + "comments/" + reviewid;
                    lnklike.PostBackUrl = ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + strur;
                    //lnkfollow.PostBackUrl = ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + strur;
                    writereview.Visible = false;
                    divmsg.Visible = true;
                    enterreply.Visible = false;
                    liketext = "Like";
                    followtext = "Follow";
                }
                if (Convert.ToInt32(movie.Rows[0]["likecount"].ToString()) > 0)
                {
                    if (liketext == "Unlike")
                    {
                        liketext = "<font style='font-weight:400;'>" + liketext + "</font>";
                        lbllike.Text = "<i class='fa fa-thumbs-up' style=''></i> " + liketext;
                        lbllikecount.Text = " (" + movie.Rows[0]["likecount"].ToString() + ")";
                    }
                    else
                    {
                        lbllike.Text = "<i class='fa fa-thumbs-up'></i> " + liketext;
                        lbllikecount.Text = " (" + movie.Rows[0]["likecount"].ToString() + ")";
                    }
                }
                else
                {
                    lbllike.Text = "<i class='fa fa-thumbs-up'></i> Like";
                    lbllikecount.Visible = false;
                }

                lblreviews.Text = movie.Rows[0]["reviewtext"].ToString();

            }
            DataTable user = classreviews.getuseridbyemail(Page.User.Identity.Name);
            if (user.Rows.Count > 0)
            {
                if (user.Rows[0]["profilephoto"].ToString() != "")
                {
                    if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profilephoto/" + user.Rows[0]["profilephoto"].ToString())))
                    {
                        imgprofile2.Src = ReturnUrl("sitepath") + "images/profile55x55/" + user.Rows[0]["profilephoto"].ToString();
                    }
                    else
                    {
                        imgprofile2.Src = "https://graph.facebook.com/" + user.Rows[0]["profilephoto"].ToString() + "/picture?type=large";
                    }
                }
                else
                {
                    imgprofile2.Src = ReturnUrl("sitepath") + "images/profile55x55/noimage1.png";
                }
                lblusername2.Text = user.Rows[0]["fullname"].ToString();
                lnkusername2.HRef = ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString());
                userid = user.Rows[0]["indexid"].ToString();
            }

            DataTable comment = classreviews.getcommentsbyreviewid(reviewid);
            if (comment.Rows.Count > 0)
            {
                rptmoviereview.DataSource = comment;
                rptmoviereview.DataBind();
                if (rptmoviereview.Items.Count > 0)
                {
                    for (int i = 0; i < rptmoviereview.Items.Count; i++)
                    {
                        Label lbldate = (Label)rptmoviereview.Items[i].FindControl("lbldate");
                        LinkButton lnkuser = (LinkButton)rptmoviereview.Items[i].FindControl("lnkusername");
                        Image imgprofile = (Image)rptmoviereview.Items[i].FindControl("imgprofile");
                        LinkButton lnkimage = (LinkButton)rptmoviereview.Items[i].FindControl("lnkimage");
                        lnkimage.PostBackUrl = ReturnUrl("sitepathmain") + "user/" + comment.Rows[i]["indexid"].ToString();
                        if (comment.Rows[i]["profilephoto"].ToString() != "")
                        {
                            if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profilephoto/" + comment.Rows[i]["profilephoto"].ToString())))
                            {
                                imgprofile.ImageUrl = ReturnUrl("sitepath") + "images/profilephoto/" + comment.Rows[i]["profilephoto"].ToString();
                            }
                            else
                            {
                                imgprofile.ImageUrl = "https://graph.facebook.com/" + comment.Rows[i]["profilephoto"].ToString() + "/picture?type=large";
                            }
                        }
                        else
                        {
                            imgprofile.ImageUrl = ConfigurationManager.AppSettings["adminsitepath"]+ "images/noprofile.jpg";
                        }
                        lnkuser.PostBackUrl = ReturnUrl("sitepathmain") + "user/" + comment.Rows[i]["indexid"].ToString();
                        DateTime dat = Convert.ToDateTime(comment.Rows[i]["commentdate"].ToString());
                        lbldate.Text = dat.ToString("dd-MMM-yyyy") + "&nbsp;&nbsp;&nbsp;" + dat.ToString("HH:mm");
                    }
                }
            }
            else
            {
                commentlist.Visible = false;
            }

        //}
        //catch (Exception ex)
        //{
           
        //}
    }
    protected void lnklike_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["reviewid"] != null)
        {
            reviewid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["reviewid"].ToString()));
        }
        bool iserror;
        if (Page.User.Identity.IsAuthenticated)
        {
            iserror = classreviews.insertdeletelikes(Page.User.Identity.Name,reviewid);
            if (!iserror)
            {
                loaddata();
            }
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login");
        }
    }
    protected void lnksubmit_Click(object sender, EventArgs e)
    {
        bool iserror;
        
        if (Request.QueryString["reviewid"] != null)
        {
            reviewid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["reviewid"].ToString()));
        }
        string strFromTextArea = txtImagename1.Value.Trim();
        if (Page.User.Identity.IsAuthenticated)
        {

            if (strFromTextArea == "")
            {
                loaddata();
                iserror = true;
                lblerror.Visible = true;
                lblerror.Text = "Please enter comment !";
            }
            else
            {
                lblerror.Visible = false;
                iserror = false;
            }

            if (!iserror)
            {
                iserror = classreviews.insercommentbyreviewid(Page.User.Identity.Name, Convert.ToInt32(reviewid), strFromTextArea);
                loaddata();
                commentlist.Visible = true;
                txtImagename1.Value = "";
                DataTable dt = classreviews.getuseridbyemail(Page.User.Identity.Name);
                if (reviewuser != Page.User.Identity.Name)
                {
                    DataTable notify = classreviews.getemailnotification(movie.Rows[0]["username"].ToString());
                    if (notify.Rows.Count > 0)
                    {
                        if (notify.Rows[0]["comments"].ToString().Trim() == "Y")
                        { 
                            if (dt.Rows.Count > 0)
                            {
                                email(movie.Rows[0]["username"].ToString(), dt.Rows[0]["fullname"].ToString(), dt.Rows[0]["indexid"].ToString());
                            }
                        }
                    }
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            email(movie.Rows[0]["username"].ToString(), dt.Rows[0]["fullname"].ToString(), dt.Rows[0]["indexid"].ToString());
                        }
                    }                   
                }
                
            }
        }
    }

    public void email(string mailto, string follower, string followerid)
    {
        if(Page.User.Identity.IsAuthenticated)
        {
            DataTable dt = classreviews.getuseridbyemail(Page.User.Identity.Name);
            if (dt.Rows.Count > 0)
            {
                username = dt.Rows[0]["fullname"].ToString();
            }
            string imagepath = "http://www.Intranet.com/themes/creative1.0/images/logo.png";
            string body = "<html><body>";
            body += "<div style='width:500px;float:left;min-height:320px;padding:20px;border:1px solid #ccc;'><div style='float:left;width:96%;height:20px;padding:2%;font-family:Arial;font-weight:bold;font-size:16px;'>Dear " + lblusername.Text + ",</div><div style='float: left; width: 96%; height: 40px; padding: 2%; font-family: Arial; font-size: 14px;'><a target='blank' href='" + ReturnUrl("sitepathmain") + "user/" + followerid + "' style='color:#1155CC;'>" + follower + " </a>&nbsp; commented on your review. Boom!<br><br><a target='blank' href='" + ReturnUrl("sitepathmain") + "comments/" + reviewid + "' style='color:#1155CC;text-decoration:none;'>Click here</a> to see the comment.</div><div style='float: left; width: 96%; height: 20px; padding: 2%; font-family: Arial; font-size: 14px;'>To Unsubscribe from this type of notification,<a target='blank' style='text-decoration: none; color: #1155CC;' href='" + ReturnUrl("sitepathmain") + "procs/preference' > click here.</a></div><div style='float: left; width: 96%; height: 30px; padding: 2%; font-family: Arial; font-size: 14px;'>If you have any questions or need further assistance, please contact us at support@Intranet.com.</div><div style='float: left; width: 96%; height: 20px; padding: 2%; font-family: Arial; font-size: 14px;'>Sincerely,</div><div style='float: left; width: 96%; height: 20px; padding: 2%; font-family: Arial; font-size: 14px;'>Team Intranet.com</div><div style='float: left; width: 96%; height: 20px; padding: 2%; font-family: Arial; font-size: 14px;'><a href='www.Intranet.com'><img src='" + imagepath + "' /></a></div></div>";
            body += "</body></html>";
            //Response.Write(body);
            //Response.End();
            string mailfrom = creativeconfiguration.adminemail;
            DateTime dat = Convert.ToDateTime(DateTime.Now.ToString());
            string timespan = dat.ToString("HH:mm") + " on " + dat.ToString("dd-MMM-yyyy");
            Utilities.SendMail(mailfrom, mailto, username + " Commented on your Review on my Intranet.com as of " + timespan, body);
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
}