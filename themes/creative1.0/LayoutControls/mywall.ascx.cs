using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;

public partial class themes_creative : System.Web.UI.UserControl
{
    public static int userid,PageCount=10;
    public static string emailid;
    public int pid;
    public string fname = null;

    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        if(Page.User.Identity.IsAuthenticated)
        {
            emailid = Page.User.Identity.Name;
        }
       if(!Page.IsPostBack)
       {
           //bindwall(10);
       }
    }

    //public void bindwall(int pagecount)
    //{
    //    DataSet dswall = new DataSet();
    //    string xmlfileuser = Request.PhysicalApplicationPath + "\\xml\\mywall.xml";
    //    string xmlfileadmin = ReturnUrl("sitepathadmin") + "xml/mywall.xml";
    //    string localPath = new Uri(xmlfileadmin).LocalPath;

    //    if (File.Exists(xmlfileuser))
    //    {
    //        DateTime udt = Convert.ToDateTime(File.GetLastWriteTime(xmlfileuser).ToString());
    //        DateTime adt = Convert.ToDateTime(File.GetLastWriteTime(Server.MapPath(localPath)).ToString());
    //        if (udt > adt)
    //        {
    //            dswall.ReadXml(ReturnUrl("sitepathmain") + "xml/mywall.xml");
    //            if (dswall.Tables.Count > 0)
    //            {
    //                if(dswall.Tables[0].Rows.Count > 0)
    //                {
    //                    if (dswall.Tables[0].Rows.Count + 10 > pagecount)
    //                    {
    //                        lnknext.Visible = true;
    //                        rptwall.DataSource = dswall.Tables[0].AsEnumerable().Take(pagecount).CopyToDataTable();
    //                        rptwall.DataBind();
    //                        alter();
    //                    }
    //                    else
    //                    {
    //                        lnknext.Visible = false;
    //                        rptwall.DataSource = dswall.Tables[0].AsEnumerable().Take(pagecount).CopyToDataTable();
    //                        rptwall.DataBind();
    //                        alter();
    //                    }
    //                }
    //                else
    //                {
    //                    {
    //                        DataTable dt = classproduct.GetWall();
    //                        if (dt.Rows.Count > 0)
    //                        {
    //                            rptwall.DataSource = dt;
    //                            rptwall.DataBind();
    //                            lnknext.Visible = false;
    //                            alter();
    //                        }
    //                    }
    //                }
                    
    //            }
    //            else
    //            {
    //                DataTable dt = classproduct.GetWall();
    //                if (dt.Rows.Count > 0)
    //                {
    //                    rptwall.DataSource = dt;
    //                    rptwall.DataBind();
    //                    lnknext.Visible = false;
    //                    alter();
                        
    //                }
    //            }
    //        }
    //        else
    //        {
    //            dswall.ReadXml(ReturnUrl("sitepathadmin") + "xml/mywall.xml");
    //            if (dswall.Tables.Count > 0)
    //            {
    //                if (dswall.Tables[0].Rows.Count > 0)
    //                {
    //                    if (dswall.Tables[0].Rows.Count + 10 > pagecount)
    //                    {
    //                        lnknext.Visible = true;
    //                        rptwall.DataSource = dswall.Tables[0].AsEnumerable().Take(pagecount).CopyToDataTable();
    //                        rptwall.DataBind();
    //                        alter();
    //                    }
    //                    else
    //                    {
    //                        lnknext.Visible = false;
    //                        rptwall.DataSource = dswall.Tables[0].AsEnumerable().Take(pagecount).CopyToDataTable();
    //                        rptwall.DataBind();
    //                        alter();
    //                    }
                        
    //                }
    //                else
    //                {
    //                    DataTable dt = classproduct.GetWall();
    //                    if (dt.Rows.Count > 0)
    //                    {
    //                        rptwall.DataSource = dt;
    //                        rptwall.DataBind();
    //                        lnknext.Visible = false;
    //                        alter();
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                DataTable dt = classproduct.GetWall();
    //                if (dt.Rows.Count > 0)
    //                {
    //                    rptwall.DataSource = dt;
    //                    rptwall.DataBind();
    //                    lnknext.Visible = false;
    //                    alter();
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        dswall.ReadXml(ReturnUrl("sitepathadmin") + "xml/mywall.xml");
    //        if (dswall.Tables.Count > 0)
    //        {
    //            if (dswall.Tables[0].Rows.Count > 0)
    //            {
    //                if (dswall.Tables[0].Rows.Count + 10 > pagecount)
    //                {
    //                    lnknext.Visible = true;
    //                    rptwall.DataSource = dswall.Tables[0].AsEnumerable().Take(pagecount).CopyToDataTable();
    //                    rptwall.DataBind();
    //                    alter();
    //                }
    //                else
    //                {
    //                    lnknext.Visible = false;
    //                    rptwall.DataSource = dswall.Tables[0].AsEnumerable().Take(pagecount).CopyToDataTable();
    //                    rptwall.DataBind();
    //                    alter();
    //                }
    //            }
    //            else
    //            {
    //                DataTable dt = classproduct.GetWall();
    //                if (dt.Rows.Count > 0)
    //                {
    //                    rptwall.DataSource = dt;
    //                    rptwall.DataBind();
    //                    lnknext.Visible = false;
    //                    alter();
    //                }
    //            }
    //        }
    //        else
    //        {
    //            DataTable dt = classproduct.GetWall();
    //            if (dt.Rows.Count > 0)
    //            {
    //                rptwall.DataSource = dt;
    //                rptwall.DataBind();
    //                lnknext.Visible = false;
    //                alter();
    //            }
    //        }
    //    }
    //}

    //public void alter()
    //{
    //    if (rptwall.Items.Count > 0)
    //    {
    //        for (int i = 0; i < rptwall.Items.Count; i++)
    //        {
    //            LinkButton lnklike = (LinkButton)rptwall.Items[i].FindControl("lnklike");

    //            Label lbllike = (Label)rptwall.Items[i].FindControl("lbllike");
    //            LinkButton lnkcomment = (LinkButton)rptwall.Items[i].FindControl("lnkcomm");
    //            Label lblpid = (Label)rptwall.Items[i].FindControl("lblpid");
    //            Label lbllikecount = (Label)rptwall.Items[i].FindControl("lbllikecount");

    //            LinkButton lnkdislike = (LinkButton)rptwall.Items[i].FindControl("lnkdislike");
    //            Label lbldislike = (Label)rptwall.Items[i].FindControl("lbldislike");
    //            Label lbldislikecount = (Label)rptwall.Items[i].FindControl("lbldislikecount");
    //            string liketext = "";
    //            if (Page.User.Identity.IsAuthenticated)
    //            {
    //                DataTable dtlike = classreviews.getlikedetailsbyuserpostid(emailid, Convert.ToInt32(lblpid.Text));
    //                if (dtlike.Rows.Count > 0)
    //                {
    //                    // Like Module
    //                    if (dtlike.Rows[0]["likeflag"].ToString().Trim() == "Unlike")
    //                    {
    //                        liketext = "<font style='font-weight:400;color:#3199cc !important;'>" + "Like" + "</font>";
    //                        lbllike.Text = "<i class='fa fa-thumbs-up' style='color:#3199cc !important;'></i> " + liketext;
    //                        if (Convert.ToInt32(dtlike.Rows[0]["likecount"].ToString().Trim()) > 0)
    //                        {
    //                            lbllikecount.ForeColor = System.Drawing.Color.FromName("#3199cc");
    //                            lbllikecount.Text = " (" + dtlike.Rows[0]["likecount"].ToString().Trim() + ")";
    //                        }
    //                        else
    //                        {
    //                            lbllikecount.Text = "";
    //                        }
    //                    }
    //                    else
    //                    {
    //                        lbllike.Text = "<i class='fa fa-thumbs-up'></i> " + dtlike.Rows[0]["likeflag"].ToString().Trim();
    //                        if (Convert.ToInt32(dtlike.Rows[0]["likecount"].ToString().Trim()) > 0)
    //                        {
    //                            lbllikecount.Text = " (" + dtlike.Rows[0]["likecount"].ToString().Trim() + ")";
    //                        }
    //                        else
    //                        {
    //                            lbllikecount.Text = "";
    //                        }
    //                    }
    //                    //Dislike Module
    //                    if (dtlike.Rows[0]["dislikeflag"].ToString().Trim() == "Undislike")
    //                    {
    //                        liketext = "<font style='font-weight:400;color:#3199cc !important;'>" + "Dislike" + "</font>";
    //                        lbldislike.Text = "<i class='fa fa-thumbs-down' style='color:#3199cc !important;'></i> " + liketext;
    //                        if (Convert.ToInt32(dtlike.Rows[0]["dislikecount"].ToString().Trim()) > 0)
    //                        {
    //                            lbldislikecount.ForeColor = System.Drawing.Color.FromName("#3199cc");
    //                            lbldislikecount.Text = " (" + dtlike.Rows[0]["dislikecount"].ToString().Trim() + ")";
    //                        }
    //                        else
    //                        {
    //                            lbldislikecount.Text = "";
    //                        }
    //                    }
    //                    else
    //                    {
    //                        lbldislike.Text = "<i class='fa fa-thumbs-down'></i> " + dtlike.Rows[0]["dislikeflag"].ToString().Trim();
    //                        if (Convert.ToInt32(dtlike.Rows[0]["dislikecount"].ToString().Trim()) > 0)
    //                        {
    //                            lbldislikecount.Text = " (" + dtlike.Rows[0]["dislikecount"].ToString().Trim() + ")";
    //                        }
    //                        else
    //                        {
    //                            lbldislikecount.Text = "";
    //                        }
    //                    }

    //                    //Comment Module
    //                    if (Convert.ToInt32(dtlike.Rows[0]["commentcount"].ToString()) > 0)
    //                    {
    //                        lnkcomment.Text = "<i class='fa fa-comment'></i> Comment" + " (" + dtlike.Rows[0]["commentcount"].ToString() + ")";
    //                    }
    //                    else
    //                    {
    //                        lnkcomment.Text = "<i class='fa fa-comment'></i> Comment";
    //                    }
    //                }
    //                else
    //                {
    //                    lbllike.Text = "<i class='fa fa-thumbs-up'></i> Like";
    //                    lbldislike.Text = "<i class='fa fa-thumbs-down'></i> Dislike";
    //                    lbllikecount.Visible = false;
    //                    lbldislikecount.Visible = false;
    //                    lnkcomment.Text = "<i class='fa fa-comment'></i> Comment";
    //                }
    //            }
    //            else
    //            {
    //                DataTable dtlike = classreviews.getlikedetailsbyuserpostid("", Convert.ToInt32(lblpid.Text));
    //                if (dtlike.Rows.Count > 0)
    //                {
    //                    //like Module
    //                    if (dtlike.Rows[0]["likeflag"].ToString().Trim() == "Unlike")
    //                    {
    //                        liketext = "<font style=font-weight:400;'>" + dtlike.Rows[0]["likeflag"].ToString().Trim() + "</font>";
    //                        lbllike.Text = "<i class='fa fa-thumbs-up' style='></i> " + liketext;
    //                        if (Convert.ToInt32(dtlike.Rows[0]["likecount"].ToString().Trim()) > 0)
    //                        {
    //                            lbllikecount.Text = " (" + dtlike.Rows[0]["likecount"].ToString().Trim() + ")";
    //                        }
    //                        else
    //                        {
    //                            lbllikecount.Text = "";
    //                        }
    //                    }
    //                    else
    //                    {
    //                        lbllike.Text = "<i class='fa fa-thumbs-up'></i> " + dtlike.Rows[0]["likeflag"].ToString().Trim();
    //                        if (Convert.ToInt32(dtlike.Rows[0]["likecount"].ToString().Trim()) > 0)
    //                        {
    //                            lbllikecount.Text = " (" + dtlike.Rows[0]["likecount"].ToString().Trim() + ")";
    //                        }
    //                        else
    //                        {
    //                            lbllikecount.Text = "";
    //                        }
    //                    }
    //                    //Dislike Module
    //                    if (dtlike.Rows[0]["dislikeflag"].ToString().Trim() == "Undislike")
    //                    {
    //                        liketext = "<font style='font-weight:400;'>" + "Dislike" + "</font>";
    //                        lbldislike.Text = "<i class='fa fa-thumbs-down' style=''></i> " + liketext;
    //                        if (Convert.ToInt32(dtlike.Rows[0]["dislikecount"].ToString().Trim()) > 0)
    //                        {
    //                            lbldislikecount.Text = " (" + dtlike.Rows[0]["dislikecount"].ToString().Trim() + ")";
    //                        }
    //                        else
    //                        {
    //                            lbldislikecount.Text = "";
    //                        }
    //                    }
    //                    else
    //                    {
    //                        lbldislike.Text = "<i class='fa fa-thumbs-down'></i> " + dtlike.Rows[0]["dislikeflag"].ToString().Trim();
    //                        if (Convert.ToInt32(dtlike.Rows[0]["dislikecount"].ToString().Trim()) > 0)
    //                        {
    //                            lbldislikecount.Text = " (" + dtlike.Rows[0]["dislikecount"].ToString().Trim() + ")";
    //                        }
    //                        else
    //                        {
    //                            lbldislikecount.Text = "";
    //                        }
    //                    }
    //                    //comment Module
    //                    if (Convert.ToInt32(dtlike.Rows[0]["commentcount"].ToString()) > 0)
    //                    {
    //                        lnkcomment.Text = "<i class='fa fa-comment'></i> Comment" + " (" + dtlike.Rows[0]["commentcount"].ToString() + ")";
    //                    }
    //                    else
    //                    {
    //                        lnkcomment.Text = "<i class='fa fa-comment'></i> Comment";
    //                    }
    //                    lnklike.PostBackUrl = ReturnUrl("sitepathmain") + "login.aspx";
    //                }
    //                else
    //                {
    //                    lbllike.Text = "<i class='fa fa-thumbs-up'></i> Like";
    //                    lbldislike.Text = "<i class='fa fa-thumbs-down'></i> Dislike";
    //                    lbllikecount.Visible = false;
    //                    lbldislikecount.Visible = false;
    //                    lnkcomment.Text = "<i class='fa fa-comment'></i> Comment";
    //                }
    //            }
    //        }
    //    }   
    //}

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

    //public string getproductimage(object imagepath)
    //{
    //    string strurl = "";
    //    try
    //    {
    //        if (imagepath.ToString().Length > 0 && imagepath.ToString() != "noimage2.png" && imagepath.ToString() != "noimage2.gif" && imagepath.ToString() != "N/A" && imagepath.ToString() != "n/a")
    //        {
    //            pstdoc.Visible = false;
    //            pnlvideo.Visible = false;
    //            img1.Visible = true;
    //            string fileName = Request.PhysicalApplicationPath + "\\images\\450x300\\" + imagepath.ToString();
    //            if (File.Exists(fileName))
    //            {
    //                strurl = "<img src='" + ReturnUrl("sitepathmain") + "images/450x300/" + imagepath.ToString() + "' />";
    //            }
    //            else
    //            {
    //                strurl = "<img src='" + ReturnUrl("sitepathadmin") + "images/450x300/" + imagepath.ToString() + "' />";
    //            }
    //        }
    //        else
    //        {
    //            strurl = "";
    //        }
    //        return strurl;
    //    }
    //    catch (Exception ex)
    //    {
    //        return strurl;
    //    }
    //}

    //public string getpostvideo(object videocode,object videourl)
    //{ 
    //    string strurl = "";
    //    try
    //    {
    //        if (videourl.ToString().Length > 0 || videourl.ToString() != "" || videocode.ToString().Length > 0 || videocode.ToString() != "")
    //        {
    //            if (videocode.ToString().Length > 0 && videocode.ToString() != "" && videocode.ToString() != "N/A" && videocode.ToString() != "n/a")
    //            {
    //                img1.Visible = false;
    //                pstdoc.Visible = false;
    //                pnlvideo.Visible = true;
    //                strurl = videocode.ToString();
    //            }
    //            else
    //            {
    //                if (videourl.ToString() != "N/A" && videourl.ToString() !="n/a")
    //                {
    //                    img1.Visible = false;
    //                    pstdoc.Visible = false;
    //                    pnlvideo.Visible = true;
    //                    HttpCookie cookie = HttpContext.Current.Request.Cookies["internet"];
    //                    if (cookie != null)
    //                    {
    //                        if (cookie.Value.ToString().ToLower().Trim() == "true")
    //                        {
    //                            strurl = "<video controls preload='metadata' class='videourl' data-setup='{}'><source src='" + ConfigurationManager.AppSettings["internetURL"].ToString().Trim() + videourl + "' type='video/mp4'></video>";
    //                        }
    //                        else
    //                        {
    //                            strurl = "<video controls preload='metadata' class='videourl' data-setup='{}'><source src='" + ConfigurationManager.AppSettings["intranetURL"].ToString().Trim() + videourl + "' type='video/mp4'></video>";
    //                        }
    //                    }
    //                    //strurl = "<video controls preload='metadata' class='videourl' data-setup='{}'><source src='" + videourl + "' type='video/mp4'></video>";
    //                }
    //                else
    //                {
    //                    strurl = "";
    //                }
    //            }
    //        }
    //        else
    //        {
    //            strurl = "";
    //        }
    //        return strurl;
    //    }
    //    catch (Exception ex)
    //    {
    //        return strurl;
    //    }
    //}

    //public string getpostdoc(object doc)
    //{
    //    string file = "";
    //    try
    //    {
    //        if (doc.ToString().Length > 0 || doc.ToString() != "")
    //        {
    //            if(doc.ToString() != "N/A" && doc.ToString() !="n/a")
    //            {
    //                img1.Visible = false;
    //                pnlvideo.Visible = false;
    //                pstdoc.Visible = true;
    //                file = doc.ToString();
    //            }
    //            else
    //            {
    //                file = "";
    //                pstdoc.Visible = false;
    //            }
    //        }
    //        else
    //        {
    //            file = "";
    //            pstdoc.Visible = false;
    //        }
    //        return file;
    //    }
    //    catch (Exception ex)
    //    {
    //        return file;
    //    }
    //}

    //public string getdocext(object ext)
    //{
    //     string file = "";
    //     string ext1 = "";
    //     string rtnval="";
    //     try
    //     {
    //         file = ext.ToString();
    //         if (file != "" && file.ToString() != "N/A" && file.ToString() != "n/a")
    //         {
    //             int z = 0;
    //             z = file.LastIndexOf('.');
    //             int x = 0;
    //             x = file.Length;
    //             ext1 = file.Substring(z, x - z).ToLower();
    //             if (ext1 == ".pdf")
    //             {

    //                 rtnval = "<img class='extimg' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/pdf1hover.png" + "' />";
    //             }
    //             else if (ext1 == ".doc")
    //             {
    //                 rtnval = "<img class='extimg' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/doc1hover.png" + "' />";
    //             }
    //             else if (ext1 == ".docx")
    //             {
    //                 rtnval = "<img class='extimg' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/doc1hover.png" + "' />";
    //             }
    //             else if (ext1 == ".xls")
    //             {
    //                 rtnval = "<img class='extimg' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/xls1hover.png" + "' />";
    //             }
    //             else if (ext1 == ".xlsx")
    //             {
    //                 rtnval = "<img class='extimg' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/xls1hover.png" + "' />";
    //             }
    //             else if (ext1 == ".ppt")
    //             {
    //                 rtnval = "<img class='multidocext' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/ppthover.png" + "' />";
    //             }
    //             else if (ext1 == ".pptx")
    //             {
    //                 rtnval = "<img class='multidocext' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/ppthover.png" + "' />";
    //             }
    //         }
    //         return rtnval;
    //     }
    //     catch (Exception ex)
    //     {
    //         return rtnval;
    //     }
    //}

    //public string getshortdesc(object shortdesc)
    //{
    //    string strurl = "";
    //    try
    //    {
    //        desc.InnerHtml=shortdesc.ToString().Trim();

    //        strurl = Regex.Replace(desc.InnerHtml, "<.*?>", String.Empty);

    //        if(strurl.Length>200)
    //        {
    //            strurl = strurl.Substring(0,200)+"...";
    //        }

    //        return strurl;
    //    }
    //    catch (Exception ex)
    //    {
    //        return strurl;
    //    }
    //}

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
            if(days>0)
            {
                if(days<31)
                {
                    strurl = strurl + days + " days ago";
                }
                else 
                {
                    if(days <= 365)
                    {
                        int monthsApart = 12 * (dat2.Year - dat.Year) + dat2.Month - dat.Month;
                        strurl = strurl + monthsApart + " months ago";
                    }
                    else
                    {
                        int yearpart =dat2.Year-dat.Year;
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

    public string productUrlrewriting2(object cattype,object productname, object productid)
    {
        string strurl = "";
        string cat="";
        try
        {
            cat = cattype.ToString();
            if(cat=="I")
            {
                strurl = UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(productid.ToString()), "PG");
            }
            else
            {
                strurl = UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(productid.ToString()), "PDID");
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
            strurl = UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(productid.ToString()), "PDID") + "#MainContent_pdnewglobal_m_uxPdLayout_ctl04_criticcornerdiv";
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

    //protected void rptwall_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    bool iserror = false;
    //    try
    //    {
    //        if (e.CommandName.ToLower() == "cmdlike")
    //        {
    //            if (Page.User.Identity.IsAuthenticated)
    //            {
    //                iserror = classreviews.insertdeletepostlikes(Page.User.Identity.Name.ToString().Trim(), Convert.ToInt32(e.CommandArgument.ToString()));
    //                alter();
    //            }
    //            else
    //            {
    //                Response.Redirect(ReturnUrl("sitepathmain")+"login.aspx?ReturnUrl="+Request.RawUrl);
    //            }
    //        }
    //        else if (e.CommandName.ToLower() == "cmddislike")
    //        {
    //            if (Page.User.Identity.IsAuthenticated)
    //            {
    //                iserror = classreviews.insertdeletepostdislikes(Page.User.Identity.Name, Convert.ToInt32(e.CommandArgument.ToString()));
    //                alter();
    //            }
    //            else
    //            {
    //                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + Request.RawUrl);
    //            }
    //        }
    //        else if (e.CommandName.ToLower() == "cmdcomment")
    //        {
    //            Response.Redirect(ReturnUrl("sitepathmain") + "comments/" + UrlRewritingVM.Encrypt(e.CommandArgument.ToString()));
    //        }
    //    }
    //    catch(Exception ex)
    //    {
    //        ErrorLog.WriteError(ex.ToString());
    //    }
    //}

    //protected void lnknext_Click(object sender, EventArgs e)
    //{
    //    PageCount += 10;
    //    bindwall(PageCount);
    //}
    //protected void Page_Changed(object sender, EventArgs e)
    //{
    //    PageCount += 10;
    //    this.bindwall(PageCount);
    //}
}