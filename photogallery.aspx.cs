using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Web.Security;
using System.Net.Mime;
using System.Net;
using AjaxControlToolkit;


public partial class pdnew : System.Web.UI.Page
{
    private static int productId;
    static int ratevalue;
    public string liketext = "";
    string strurl = "";
    public static int hlink = 0;
    public static int count = 0;
    public static int nxtcount = 0;
    public string url="";
    public static string returnurl = "";

     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            if (!IsPostBack)
            {
                if (hlink == 0)
                {
                    nxtcount = 0;
                    if (Request.UrlReferrer != null)
                    {
                        Session["RefUrl"] = Request.UrlReferrer.ToString();
                        returnurl = Request.UrlReferrer.ToString();
                    }
                }
                loaddata();
                if (nxtcount == count)
                {
                    if (nxtcount > 0)
                    {
                        lnkprevious.Visible = true;
                    }
                }
            }
            else
            {
                loaddata();
            }
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + Request.RawUrl);
        }
    }
    public void loaddata()
    {
        if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
        {
            string pid = UrlRewritingVM.Decrypt(Request.QueryString["p"]);
            if (Int32.TryParse(pid.Trim(), out productId))
            {
                productId = Convert.ToInt32(commonclass.GetSafeIDFromURL(UrlRewritingVM.Decrypt(Request.QueryString["p"].ToString())));
            }
            else
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
            }

            DataTable dt1 = classproduct.getuserbypost(productId);
            if (dt1.Rows.Count > 0)
            {
                lnkuser.HRef = ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(dt1.Rows[0]["indexid"].ToString());
                lnkname.PostBackUrl = ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(dt1.Rows[0]["indexid"].ToString());
                profileimg.Src = ReturnUrl("sitepath") + "images/profile55x55/" + dt1.Rows[0]["profilephoto"].ToString();
                lnkname.Text = dt1.Rows[0]["fullname"].ToString();
                DateTime pdt = Convert.ToDateTime(dt1.Rows[0]["createdon"].ToString());
                lblDate.Text = pdt.ToString("dd MMMM yyyy");
            }
            DataTable ds = classproduct.get_proc_ProductDescription_ProdFeature(productId);
            if (ds.Rows.Count > 0)
            {
                //Jayesh_Sagar uncomment below lines to display the title and description on (photogallery) page 31oct2017
                lblproductname.Text = ds.Rows[0]["productname"].ToString();
             
                lbldescription.Text = ds.Rows[0]["shortdescription"].ToString();
                //Jayesh_Sagar uncomment Above lines to display the title and description on (photogallery) page 31oct2017
            }
            loadImageDeatils();

            loadImageComments();

            #region Multiple Image Nxt/Prev Link
            DataTable mulimg = classproduct.getmultiplepostimage1(productId);
            if (mulimg.Rows.Count > 0)
            {
                if (Convert.ToInt32(mulimg.Rows.Count) == 1)
                {
                    nxtcount = 0;
                }
 
                string img = mulimg.Rows[nxtcount]["bigimage"].ToString().Trim();
                if (mulimg.Rows.Count > 1)
                {
                    count = mulimg.Rows.Count;
                    if (nxtcount == 0)
                    {
                        hlink = nxtcount;
                        lnknext.Visible = true;
                        lnkprevious.Visible = false;
                    }
                    if (nxtcount == count - 1)
                    {
                        hlink++;
                        lnkprevious.Visible = true;
                        lnknext.Visible = false;
                    }
                }
                else
                {
                    lnknext.Visible = false;
                    lnkprevious.Visible = false;
                }

                if (img == "" || img == "noimage2.png" || img == "noimage2.gif")
                {
                    imgbanner.Visible = false;
                }
                else
                {
                    string fileName = Request.PhysicalApplicationPath + "\\images\\bigproduct\\" + img.ToString();
                    if (File.Exists(fileName))
                    {
                        imgbanner.ImageUrl = ReturnUrl("sitepathmain") + "images/bigproduct/" + img;
                    }
                    else
                    {
                        imgbanner.ImageUrl = ConfigurationManager.AppSettings["sitepathadmin"]+ "images/bigproduct/" + img;
                    }
                }
            }
            else
            {
                lnknext.Visible = false;
                lnkprevious.Visible = false;
            }
            #endregion
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
        }
    }
    public void loadImageDeatils()
    {
        #region Photo Gallery Like Favourite Comment
        if (Page.User.Identity.IsAuthenticated)
        {
            #region Favourite
            DataTable dtrf = clswishlist.wishlist_GetSumOfProductFromQtyTable(Page.User.Identity.Name, Convert.ToInt32(productId));
            if (dtrf.Rows.Count > 0)
            {
                if (Convert.ToInt32(dtrf.Rows[0]["SumQty"]) > 0)
                {
                    lblfav.Text = "<i  class='fa fa-heart' ></i>";
                    lblfav.Attributes.Add("style", "color:#3199cc !important");
                    lnkfav.ToolTip = "Remove favorite";
                }
                else
                {
                    lblfav.Text = "<i  class='fa fa-heart'></i>";
                    lblfav.Attributes.Add("style", "color:#606060 !important");
                    lnkfav.ToolTip = "Add to favorite";
                }
            }
            else
            {
                lblfav.Text = "<i  class='fa fa-heart'></i>";
                lblfav.Attributes.Add("style", "color:#797C80 !important");
                lnkfav.ToolTip = "Add to favorite";
            }
            #endregion
            #region like dislike commentcount
            DataTable dtlike = classreviews.getlikedetailsbyuserpostid(Page.User.Identity.Name.ToString(), productId);
            if (dtlike.Rows.Count > 0)
            {
                //Like module
                if (dtlike.Rows[0]["likeflag"].ToString().Trim() == "Unlike" || dtlike.Rows[0]["likeflag"].ToString().Trim() == "unlike")
                {
                    liketext = "<font style='font-weight:400;color:#3199cc !important;'>" + "Like" + "</font>";
                    lbllike.Text = "<i class='fa fa-thumbs-up' style='color:#3199cc !important;' ></i> " + liketext;

                    if (Convert.ToInt32(dtlike.Rows[0]["likecount"].ToString().Trim()) > 0)
                    {
                        lbllikecount.ForeColor = System.Drawing.Color.FromName("#3199cc !important");
                        lbllikecount.Text = "(" + dtlike.Rows[0]["likecount"].ToString().Trim() + ")";
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
                        lbllikecount.ForeColor = System.Drawing.Color.FromName("#606060  !important");
                        lbllikecount.Text = " (" + dtlike.Rows[0]["likecount"].ToString().Trim() + ")";
                    }
                    else
                    {
                        lbllikecount.Text = "";
                    }
                }

                //Dislike Module
                if (dtlike.Rows[0]["dislikeflag"].ToString().Trim() == "Undislike" || dtlike.Rows[0]["dislikeflag"].ToString().Trim() == "Undislike")
                {
                    liketext = "<font style='font-weight:400;color:#3199cc !important;'>" + "Dislike" + "</font>";
                    lbldislike.Text = "<i class='fa fa-thumbs-down' style='color:#3199cc !important;'></i> " + liketext;
                    if (Convert.ToInt32(dtlike.Rows[0]["dislikecount"].ToString().Trim()) > 0)
                    {
                        lbldislikecount.ForeColor = System.Drawing.Color.FromName("#3199cc !important");
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
                        lbllikecount.ForeColor = System.Drawing.Color.FromName("#606060  !important");
                        lbldislikecount.Text = " (" + dtlike.Rows[0]["dislikecount"].ToString().Trim() + ")";
                    }
                    else
                    {
                        lbldislikecount.Text = "";
                    }
                }

                if (Convert.ToInt32(dtlike.Rows[0]["commentcount"].ToString()) > 0)
                {
                    lnkcomm.Text = "<i class='fa fa-comment'></i> Comment" + " (" + dtlike.Rows[0]["commentcount"].ToString() + ")";
                }
                else
                {
                    lnkcomm.Text = "<i class='fa fa-comment'></i> Comment";
                }
            }
            else
            {
                lbllike.Text = "<i class='fa fa-thumbs-up'></i> Like";
                lbllikecount.Visible = false;
                lbldislike.Text = "<i class='fa fa-thumbs-down'></i> Dislike";
                lbldislikecount.Visible = false;
                lnkcomm.Text = "<i class='fa fa-comment'></i> Comment";
            }
            #endregion
            #region Created By Profile
            DataTable user = classreviews.getuseridbyemail(Page.User.Identity.Name);
            if (user.Rows.Count > 0)
            {
                lnkprofile2.HRef = ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString());
                imgProfile2.Src = ReturnUrl("mediapath") + "profile55x55/" + user.Rows[0]["profilephoto"].ToString();
                lblname.Text = user.Rows[0]["fullname"].ToString().Trim();
            }
            #endregion
        }
        else
        {
            lnkfav.ToolTip = "";
            #region like dislike comment
            DataTable dtlike = classreviews.getlikedetailsbyuserpostid("", productId);
            if (dtlike.Rows.Count > 0)
            {
                if (dtlike.Rows[0]["likeflag"].ToString().Trim() == "Unlike")
                {
                    liketext = "<font style='font-weight:400;'>" + dtlike.Rows[0]["likeflag"].ToString().Trim() + "</font>";
                    lbllike.Text = "<i class='fa fa-thumbs-up' ></i> " + liketext;
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

                if (Convert.ToInt32(dtlike.Rows[0]["commentcount"].ToString()) > 0)
                {
                    lnkcomm.Text = "<i class='fa fa-comment'></i> Comment" + " (" + dtlike.Rows[0]["commentcount"].ToString() + ")";
                }
                else
                {
                    lnkcomm.Text = "<i class='fa fa-comment'></i> Comment";
                }
                lnklike.PostBackUrl = ReturnUrl("sitepathmain") + "login.aspx";
            }
            else
            {
                lbllike.Text = "<i class='fa fa-thumbs-up'></i> Like";
                lbllikecount.Visible = false;
                lbldislike.Text = "<i class='fa fa-thumbs-down'></i> Dislike";
                lbldislikecount.Visible = false;
                lnkcomm.Text = "<i class='fa fa-comment'></i> Comment";
            }
            lnklogin.HRef = ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + Request.RawUrl;
            divmsg.Visible = true;
            divcomment.Visible = false;
            #endregion
        }
        #endregion
    }
    public void loadImageComments()
    {
        #region Photo gallery Comment Div
        if (Page.User.Identity.IsAuthenticated)
        {
            #region Comment Repeater
            DataTable dtm = classreviews.getratingdetails(Page.User.Identity.Name, productId);
            if (dtm.Rows.Count > 0)
            {
                //CommentText.Text = dtm.Rows[0]["reviewtext"].ToString().Trim();
                //hdvalue.Value = dtm.Rows[0]["ratingvalue"].ToString().Trim();
                //if (dtm.Rows.Count >= 9)
                //{
                //    dtm = SelectTopDataRow(dtm, 9);
                //}
                //else
                //{
                //    dtm = SelectTopDataRow(dtm, dtm.Rows.Count);
                //}
                commentlist.Visible = true;
                rptmoviereview.DataSource = dtm;
                rptmoviereview.DataBind();
                DataTable dt3;
                for (int i = 0; i <= rptmoviereview.Items.Count - 1; i++)
                {
                    Rating Rating1 = (Rating)rptmoviereview.Items[i].FindControl("Rating2");
                    Label rating = (Label)rptmoviereview.Items[i].FindControl("lblratval");
                    HiddenField hd = (HiddenField)rptmoviereview.Items[i].FindControl("hdvalue2");
                    Rating1.CurrentRating = Convert.ToInt32(hd.Value);
                    Image img = (Image)rptmoviereview.Items[i].FindControl("imgprofile");
                    Label lbldate = (Label)rptmoviereview.Items[i].FindControl("lbldate");
                    DateTime pdt = Convert.ToDateTime(lbldate.Text.ToString().Trim());
                    lbldate.Text = pdt.ToString("dd MMMM yyyy");
                    LinkButton lnkfollow = (LinkButton)rptmoviereview.Items[i].FindControl("lnkfollow");
                    LinkButton lnkcomm = (LinkButton)rptmoviereview.Items[i].FindControl("lnkcomm2");
                    LinkButton lnkusername = (LinkButton)rptmoviereview.Items[i].FindControl("lnkusername");
                    LinkButton lnklike = (LinkButton)rptmoviereview.Items[i].FindControl("lnklike2");
                    Label lbllike = (Label)rptmoviereview.Items[i].FindControl("lbllike2");
                    Label lbllikecount = (Label)rptmoviereview.Items[i].FindControl("lbllikecount2");
                    Label lblcnt = (Label)rptmoviereview.Items[i].FindControl("lblcnt2");
                    Label lblrevid = (Label)rptmoviereview.Items[i].FindControl("lblrevid");
                    Label lblreviews = (Label)rptmoviereview.Items[i].FindControl("lblreviews");
                    Label lblrate = (Label)rptmoviereview.Items[i].FindControl("lblratval");
                    Label lblcomment = (Label)rptmoviereview.Items[i].FindControl("lblcommentcount");

                    if (Convert.ToInt32(lblcomment.Text) > 0)
                    {
                        lnkcomm.Text = "<i class='fa fa-comment'></i> Comment (" + lblcomment.Text + ")";
                    }
                    else
                    {
                        lnkcomm.Text = "<i class='fa fa-comment'></i> Comment";
                    }

                    lnkcomm.PostBackUrl = ReturnUrl("sitepathmain") + "comments/" + UrlRewritingVM.Encrypt(dtm.Rows[i]["reviewid"].ToString());
                    lblrate.Text = ratevalue.ToString();
                    dt3 = classreviews.getfollower(Page.User.Identity.Name, lnkusername.CommandArgument.ToString());

                    if (dt3.Rows.Count > 0)
                    {
                        lnkfollow.Text = "<i class='fa fa-user-plus'></i> Unfollow";
                    }
                    else
                    {
                        lnkfollow.Text = "<i class='fa fa-user-plus'></i> Follow";
                    }
                    #region Comment Follow
                    string followtext = "";
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
                    if (Page.User.Identity.Name == lnkusername.CommandArgument.ToString())
                    {
                        lnkfollow.Enabled = false;
                        lnkfollow.Visible = false;
                        lnkfollow.CssClass = "self";
                        lnkfollow.Text = "<i class='fa fa-user-plus'></i> Self";
                    }
                    #endregion
                    #region Comment Like
                    if (Convert.ToInt32(lblcnt.Text.ToString().Trim()) > 0)
                    {
                        if (liketext == "Unlike")
                        {
                            liketext = "<font style='font-weight:400;'>" + liketext + "</font>";
                            lbllike.Text = "<i class='fa fa-thumbs-up' ></i> " + liketext;
                            lbllikecount.Text = " (" + lblcnt.Text.ToString().Trim() + ")";
                        }
                        else
                        {
                            lbllike.Text = "<i class='fa fa-thumbs-up'></i> " + liketext;
                            lbllikecount.Text = " (" + lblcnt.Text.ToString().Trim() + ")";
                        }
                    }
                    else
                    {
                        lbllike.Text = "<i class='fa fa-thumbs-up'></i> Like";
                        lbllikecount.Visible = false;
                    }
                    #endregion
                    #region Comment User Profile
                    if (dtm.Rows[i]["profilephoto"].ToString() != "")
                    {
                        if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profile55x55/" + dtm.Rows[i]["profilephoto"].ToString())))
                        {
                            img.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/" + dtm.Rows[i]["profilephoto"].ToString();
                        }
                    }
                    else
                    {
                        img.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/noimage1.png";
                    }
                    #endregion
                }
            }
            else
            {
                commentlist.Visible = false;
            }
            #endregion
        }
        else
        {
            strurl = UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(productId.ToString()), "PG");
            btnsendmail.PostBackUrl = ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + strurl;
            DataTable dtm = classreviews.getratingdetailsbymovieid(productId);
            if (dtm.Rows.Count > 0)
            {
                commentlist.Visible = true;
                rptmoviereview.DataSource = dtm;
                rptmoviereview.DataBind();
                for (int i = 0; i <= rptmoviereview.Items.Count - 1; i++)
                {
                    Rating Rating1 = (Rating)rptmoviereview.Items[i].FindControl("Rating2");
                    Label rating = (Label)rptmoviereview.Items[i].FindControl("lblratval");
                    Rating1.CurrentRating = Convert.ToInt32(rating.Text);
                    Image img = (Image)rptmoviereview.Items[i].FindControl("imgprofile");
                    if (dtm.Rows[i]["profilephoto"].ToString() != "")
                    {
                        if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profile55x55/" + dtm.Rows[i]["profilephoto"].ToString())))
                        {
                            img.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/" + dtm.Rows[i]["profilephoto"].ToString();
                        }
                    }
                    else
                    {
                        img.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/noimage1.png";
                    }
                    DataTable dthome = classproduct.get_proc_Productrelated(productId);
                    if (dthome.Rows.Count == 0)
                    {
                        //gotoyoumaylike.Visible = false;
                    }
                    LinkButton lnkfollow = (LinkButton)rptmoviereview.Items[i].FindControl("lnkfollow");
                    LinkButton lnkcomm = (LinkButton)rptmoviereview.Items[i].FindControl("lnkcomm2");
                    LinkButton lnkusername = (LinkButton)rptmoviereview.Items[i].FindControl("lnkusername");
                    LinkButton lnklike = (LinkButton)rptmoviereview.Items[i].FindControl("lnklike2");
                    Label lbllike = (Label)rptmoviereview.Items[i].FindControl("lbllike2");
                    Label lbllikecount = (Label)rptmoviereview.Items[i].FindControl("lbllikecount2");

                    strurl = UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(productId.ToString()), "PG");
                    lnklike.PostBackUrl = ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + strurl;
                    lnkfollow.PostBackUrl = ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + strurl;
                    Label lblcnt = (Label)rptmoviereview.Items[i].FindControl("lblcnt2");
                    Label lblrate = (Label)rptmoviereview.Items[i].FindControl("lblratval");
                    Label lblcomment = (Label)rptmoviereview.Items[i].FindControl("lblcommentcount");
                    Label lblreviews = (Label)rptmoviereview.Items[i].FindControl("lblreviews");
                    LinkButton lnkread = (LinkButton)rptmoviereview.Items[i].FindControl("read");
                    Label lbldate = (Label)rptmoviereview.Items[i].FindControl("lbldate");
                    DateTime pdt = Convert.ToDateTime(lbldate.Text.ToString().Trim());
                    lbldate.Text = pdt.ToString("dd MMMM yyyy");
                    if (Convert.ToInt32(lblcomment.Text) > 0)
                    {
                        lnkcomm.Text = "<i class='fa fa-comment'></i> Comment (" + lblcomment.Text + ")";
                    }
                    else
                    {
                        lnkcomm.Text = "<i class='fa fa-comment'></i> Comment";
                    }
                    lnkcomm.PostBackUrl = ReturnUrl("sitepathmain") + "comments/" + UrlRewritingVM.Encrypt(dtm.Rows[i]["reviewid"].ToString());
                    lblrate.Text = ratevalue.ToString();
                    lnkfollow.Text = "<i class='fa fa-user-plus'></i> Follow";
                    if (lblcnt.Text.ToString().Trim() == "0")
                    {
                        lbllike.Text = "<i class='fa fa-thumbs-up'></i> Like";
                        lbllikecount.Visible = false;
                    }
                    else
                    {
                        lbllike.Text = "<i class='fa fa-thumbs-up'></i> Like ";
                        lbllikecount.Text = "(" + lblcnt.Text + ")";
                    }
                }
            }
            else
            {
                commentlist.Visible = false;
            }
        }
        #endregion
    }
    public DataTable SelectTopDataRow(DataTable dt, int count)
    {
        DataTable dtn = dt.Clone();
        for (int i = 0; i < count; i++)
        {
            dtn.ImportRow(dt.Rows[i]);
        }
        return dtn;
    }
    public string galleryUrlrewriting(object productid)
    {
        string strurl = "";
        try
        {
            strurl = UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(productid.ToString()), "PG");
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
    protected void lnklike_Click(object sender, EventArgs e)
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            bool iserror = classreviews.insertdeletepostlikes(Page.User.Identity.Name,productId);
            loadImageDeatils();
        }
    }
    protected void lnkprevious_Click(object sender, EventArgs e)
    {
        DataTable mulimg = classproduct.getmultiplepostimage1(productId);
        if (mulimg.Rows.Count > 0)
        {
            if (mulimg.Rows.Count > 1)
            {
                nxtcount = nxtcount - 1;
                Response.Redirect(UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(mulimg.Rows[nxtcount]["productid"].ToString().Trim()), "PG"));
            }
            else
            {
                lnknext.Visible = false;
                lnkprevious.Visible = false;
            }
        }
        else
        {
        }
    }
    protected void lnknext_Click(object sender, EventArgs e)
    {
        DataTable mulimg = classproduct.getmultiplepostimage1(productId);
        if (mulimg.Rows.Count > 0)
        {
            if (mulimg.Rows.Count > 1)
            {
                nxtcount = nxtcount + 1;
                hlink = nxtcount;
                Response.Redirect(UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(mulimg.Rows[nxtcount]["productid"].ToString().Trim()), "PG"));
            }
            else
            {
                lnknext.Visible = false;
                lnkprevious.Visible = false;
            }
        }
        else
        {
        }
    }
    protected void btnsendmail_Click(object sender, EventArgs e)
    {
        if(Page.User.Identity.IsAuthenticated)
        {
            string ratevalue = hdvalue.Value;
            string txtvalue = CommentText.Text;
            DataTable review = classreviews.getreviewbyuserandproductid(Page.User.Identity.Name, productId);
            if (review.Rows.Count > 0)
            {
                classreviews.insertrating(productId, Page.User.Identity.Name,0, txtvalue, "S");
            }
            else
            {
                classreviews.insertrating(productId, Page.User.Identity.Name,0, txtvalue, "S");
            }
            CommentText.Text = "";
            hdvalue.Value = "0";
            hlink++;
            loadImageComments();
        }

    }
    protected void rptmoviereview_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        LinkButton lnkfollow = (LinkButton)e.Item.FindControl("lnkfollow");
        LinkButton lnklike = (LinkButton)e.Item.FindControl("lnklike2");
        bool iserror;

        if (e.CommandName == "follow")
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                iserror = classreviews.insertdeletefollowing(Page.User.Identity.Name, e.CommandArgument.ToString());
                if (!iserror)
                {
                    if (lnkfollow.Text == "<i class='fa fa-user-plus'></i> Follow")
                    {
                        DataTable notify = classreviews.getemailnotification(e.CommandArgument.ToString());
                        if (notify.Rows.Count > 0)
                        {
                            if (notify.Rows[0]["follow"].ToString().Trim() == "Y")
                            {
                                DataTable dt = classreviews.getuseridbyemail(Page.User.Identity.Name);
                                if (dt.Rows.Count > 0)
                                {
                                   // email(e.CommandArgument.ToString(), dt.Rows[0]["fullname"].ToString(), dt.Rows[0]["indexid"].ToString());
                                }
                            }
                        }
                        else
                        {
                            DataTable dt = classreviews.getuseridbyemail(Page.User.Identity.Name);
                            if (dt.Rows.Count > 0)
                            {
                               // email(e.CommandArgument.ToString(), dt.Rows[0]["fullname"].ToString(), dt.Rows[0]["indexid"].ToString());
                            }
                        }
                    }
                    loadImageComments();
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
                    loadImageComments();
                }
            }
            //else
            //{
            //    Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "pdnew.aspx?p=" + productid);
            //}
        }
    }
    protected void lnkdislike_Click(object sender, EventArgs e)
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            bool iserror = classreviews.insertdeletepostdislikes(Page.User.Identity.Name, productId);
            if (!iserror)
            {
                loadImageDeatils();
            }
        }
    }
    protected void lnkfav_Click(object sender, EventArgs e)
    {
        if (Page.User.Identity.IsAuthenticated)
        {
             DataTable dtrf = clswishlist.wishlist_GetSumOfProductFromQtyTable(Page.User.Identity.Name.ToString().Trim(), Convert.ToInt32(productId));
             if(dtrf.Rows.Count > 0)
             {
                 if (Convert.ToInt32(dtrf.Rows[0]["SumQty"]) > 0)
                 {
                      bool m_index = clswishlist.Deletewishlistproduct1(productId, Page.User.Identity.Name.ToString());
                      loadImageDeatils();
                 }
                 else
                 {
                     bool strstring = clswishlist.insertwishlist(Page.User.Identity.Name, productId, "1", "", "");
                     DataSet dtuserid = classaddress.GetuserId(Page.User.Identity.Name.ToString());
                     loadImageDeatils();
                 }
             }          
        }
    }
    protected void HyperLink1_Click(object sender, EventArgs e)
    {
        hlink = 0;
        //object refUrl = Session["RefUrl"];
        object refUrl = returnurl;
        if (refUrl != null)			
            Response.Redirect((string)refUrl);
    }
}