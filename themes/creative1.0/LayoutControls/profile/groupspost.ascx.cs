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
public partial class themes_creative1_LayoutControls_profile_groups : System.Web.UI.UserControl
{
    public static int userid;
    public static string pstname = "";
    public static string emailid;
    public int pid;
    public string fname = null;
    public string grpid="";
    public static int PageSize = 10, RecordCount;
    public static int pgi=1;

    public static int pageCount;
    public static int maxpage = 5;
    public static double dblmainpg;
    public static int maxpgcount;
    public static int maxpg = 1;

    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;}
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                emailid = Page.User.Identity.Name;
            }
            if (Request.QueryString["grpid"] != "" && Request.QueryString["grpid"] != null && Request.QueryString["grpid"].Length == 24 && Request.QueryString.Count == 1)
            {
                int gid;
                if (Int32.TryParse(UrlRewritingVM.Decrypt(Request.QueryString["grpid"]), out gid))
                {
                    grpid = commonclass.GetSafeIDFromURL(UrlRewritingVM.Decrypt(Request.QueryString["grpid"].ToString()));
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
            
            if (Page.User.Identity.IsAuthenticated)
            {
                if (!Page.IsPostBack)
                {
                    pgi = 1;
                    bindwall(pgi);
                }

                //popularcontrol();
                //loadimg();
                //loaddocument();
                //loadads();
                //loadfunzone();
                //loadvideo();
            }
            else
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + Request.RawUrl);
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
        string posttype = "";
        creative.Common clsCommon = new creative.Common();
        DataTable dthome = classcategory.getallcatsbyhomestatus();
        rptcategname.DataSource = dthome;
        rptcategname.DataBind();
        for (int j = 0; j < rptcategname.Items.Count; j++)
        {
            posttype = dthome.Rows[j]["cattype"].ToString().Trim();
            Label lblid = (Label)rptcategname.Items[j].FindControl("lbcatlid");
            LinkButton lnkb = (LinkButton)rptcategname.Items[j].FindControl("lnkview");
            string strid = lblid.Text;
            if (strid != null || strid != "")
            {
                Repeater rptcatproduct = (Repeater)rptcategname.Items[j].FindControl("rptcatproduct");
                Panel pnlcat = (Panel)rptcategname.Items[j].FindControl("pnlcat");
                DataTable dtp = classcategory.getgrouptextpost(strid,grpid);

                if (dtp.Rows.Count > 0)
                {
                    if (dtp.Rows.Count > 5)
                    {
                        lnkb.Visible = true;
                    }
                    else
                    {
                        lnkb.Visible = false;
                    }
                    rptcatproduct.DataSource = dtp;
                    rptcatproduct.DataBind();
                    if (rptcatproduct.Items.Count > 0)
                    {
                        for (int i = 0; i < rptcatproduct.Items.Count; i++)
                        {
                            Label lbleventdate = (Label)rptcatproduct.Items[i].FindControl("lbleventdate");
                            Label lblicon = (Label)rptcatproduct.Items[i].FindControl("lblicon");
                            if (posttype == "E")
                            {
                                lbleventdate.Visible = true;
                                lblicon.Visible = false;
                                lbleventdate.Text = Convert.ToDateTime(lbleventdate.Text.Trim()).ToString("dd MMM");
                            }
                            else
                            {
                                lbleventdate.Visible = false;
                            }
                        }
                    }

                }
                else
                {
                    pnlcat.Visible = false;
                }
            }
        }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    protected void rptcategname_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
    }

    protected void rptcatproduct_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
    }

    public string onclick_hlnkcategory(Object catId, Object catname)
    {
        string strurl = "";
        strurl = UrlRewritingVM.getUrlRewritingInfo(catname, catId, "PS");
        return strurl;
    }

    public void loadimg()
    {
        try
        {
            creative.Common clsCommon = new creative.Common();
            DataTable dthome = classcategory.getallcatsforimg();
            rptcatimg.DataSource = dthome;
            rptcatimg.DataBind();
            for (int j = 0; j < rptcatimg.Items.Count; j++)
            {
                Label lblimgid = (Label)rptcatimg.Items[j].FindControl("lblimgid");

                string strid = lblimgid.Text;
                if (strid != null || strid != "")
                {
                    Repeater rptimg = (Repeater)rptcatimg.Items[j].FindControl("rptimg");
                    Panel panimg = (Panel)rptcatimg.Items[j].FindControl("panimg");
                    DataTable grp = classproductimage.latestgroupimage(Convert.ToInt32(strid),Convert.ToInt32(grpid));
                    if (grp.Rows.Count > 0)
                    {
                        panimg.Visible = true;
                        rptimg.DataSource = grp;
                        rptimg.DataBind();
                    }
                    else
                    {
                        panimg.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    public void loaddocument()
    {
        try
        {
            creative.Common clsCommon = new creative.Common();
            DataTable dthome = classcategory.getallcatsfordoc();
            rptcatdoc.DataSource = dthome;
            rptcatdoc.DataBind();
            for (int j = 0; j < rptcatdoc.Items.Count; j++)
            {
                Label lbldocid = (Label)rptcatdoc.Items[j].FindControl("lbldocid");

                string strid = lbldocid.Text;
                if (strid != null || strid != "" || strid != "n/a" || strid != "N/A")
                {
                    Repeater rptdoc = (Repeater)rptcatdoc.Items[j].FindControl("rptdoc");
                    Panel pandoc = (Panel)rptcatdoc.Items[j].FindControl("pandoc");

                    DataTable grp = classproductimage.latestgrpdoc(Convert.ToInt32(strid), Convert.ToInt32(grpid));
                    if (grp.Rows.Count > 0)
                    {
                        pandoc.Visible = true;
                        rptdoc.DataSource = grp;
                        rptdoc.DataBind();
                    }
                    else
                    {
                        pandoc.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    public void loadads()
    {
        try
        {
            creative.Common clsCommon = new creative.Common();
            DataTable dthome = classcategory.getallcatsforads();
            rptcatads.DataSource = dthome;
            rptcatads.DataBind();
            for (int j = 0; j < rptcatads.Items.Count; j++)
            {
                Label lbladsid = (Label)rptcatads.Items[j].FindControl("lbladsid");

                string strid = lbladsid.Text;
                if (strid != null || strid != "" || strid != "n/a" || strid != "N/A")
                {
                    Repeater rptads = (Repeater)rptcatads.Items[j].FindControl("rptads");
                    Panel panads = (Panel)rptcatads.Items[j].FindControl("panads");
                    DataTable grp = classproductimage.latestgroupads(Convert.ToInt32(strid), Convert.ToInt32(grpid));
                    if (grp.Rows.Count > 0)
                    {
                        panads.Visible = true;
                        rptads.DataSource = grp;
                        rptads.DataBind();
                    }
                    else
                    {
                        panads.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    public void loadvideo()
    {
        try
        {
            creative.Common clsCommon = new creative.Common();
            DataTable dthome = classcategory.getallcatsforvideo();
            rptcatvideo.DataSource = dthome;
            rptcatvideo.DataBind();
            for (int j = 0; j < rptcatvideo.Items.Count; j++)
            {
                Label lblvideoid = (Label)rptcatvideo.Items[j].FindControl("lblvideoid");

                string strid = lblvideoid.Text;
                if (strid != null || strid != "" || strid != "n/a" || strid != "N/A")
                {
                    Repeater rptvideo = (Repeater)rptcatvideo.Items[j].FindControl("rptvideo");
                    Panel panvideo = (Panel)rptcatvideo.Items[j].FindControl("panvideo");
                    DataTable grp = classproductimage.latestgroupvideo(Convert.ToInt32(strid), Convert.ToInt32(grpid));
                    if (grp.Rows.Count > 0)
                    {
                        panvideo.Visible = true;
                        rptvideo.DataSource = grp;
                        rptvideo.DataBind();
                    }
                    else
                    {
                        panvideo.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    public void loadfunzone()
    {
        try
        {
            DataTable grp = classproductimage.latestgroupfunzone(Convert.ToInt32(grpid));
            if (grp.Rows.Count > 0)
            {
                panfun.Visible = true;
                rptfun.DataSource = grp;
                rptfun.DataBind();
            }
            else
            {
                panfun.Visible = false;
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
            strurl = UrlRewritingVM.getUrlRewritingInfo(productname, UrlRewritingVM.Encrypt(productid.ToString()), "PD");
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

    public void bindwall(int PageIndex)
    {
        DataSet dswall = new DataSet();
        dswall = classproductimage.latestgroupwall1(grpid, PageIndex, PageSize, out RecordCount);
       // dswall.ReadXml(ConfigurationManager.AppSettings["adminsitepath"]+ "xml/mywall.xml");
        if(dswall.Tables.Count>0)
        {
            if(dswall.Tables[0].Rows.Count > 0)
            {
                rptwall.DataSource = dswall.Tables[0];//.AsEnumerable().Take(10).CopyToDataTable();
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
                        DataTable dtlike = classreviews.getlikedetailsbyuserpostid(emailid, Convert.ToInt32(lblpid.Text));
                        if (dtlike.Rows.Count > 0)
                        {
                            //like Module
                            if (dtlike.Rows[0]["likeflag"].ToString().Trim() == "Unlike")
                            {
                                liketext = "<font style='color:#338ec9 !important;;font-weight:400;'>" + "Like" + "</font>";
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
                        }
                        else
                        {
                            lbllike.Text = "<i class='fa fa-thumbs-up'></i> Like";
                            lbldislike.Text = "<i class='fa fa-thumbs-down'></i> Dislike";
                            lbllikecount.Visible = false;
                            lbllikecount.Visible = false;
                            lbldislikecount.Visible = false;
                            lnkcomment.Text = "<i class='fa fa-comment'></i> Comment";
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
                lblmsg.Text = "No Post Uploded in this Group";
            }
        }
        else
        {
            pnlpst.Visible = false;
            lblmsg.Visible = true;
            lblmsg.Text = "No Post Uploded in this Group";
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
        img1.Visible = true;
        string strurl = "";
        try
        {
            if (imagepath.ToString().Length > 0 && imagepath.ToString() != "noimage2.png" && imagepath.ToString() != "noimage2.gif" &&( imagepath.ToString() != "n/a" || imagepath.ToString() != "N/A") && imagepath.ToString() != "video.jpg")
            {
                pstdoc.Visible = false;
                pnlvideo.Visible = false;
                img1.Visible = true;
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
            if (file != "" || file.ToString() != "N/A" || file.ToString() != "n/a")
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
            strurl = UrlRewritingVM.getUrlRewritingInfo(productname, UrlRewritingVM.Encrypt(productid.ToString()), "PD") + "#MainContent_pdnewglobal_m_uxPdLayout_ctl04_criticcornerdiv";
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
                    iserror = classreviews.insertdeletepostlikes(Page.User.Identity.Name, Convert.ToInt32(e.CommandArgument.ToString()));
                    bindwall(pgi);
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

    public string getdocext1(object ext)
    {
        string file = "";
        string ext1 = "";
        string rtnval = "";
        try
        {
            file = ext.ToString();
            if (file != "")
            {
                int z = 0;
                z = file.LastIndexOf('.');
                int x = 0;
                x = file.Length;
                ext1 = file.Substring(z, x - z);
                if (ext1 == ".pdf")
                {

                    rtnval = "<img class='multidocext' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/pdf1hover.png" + "' />";
                }
                else if (ext1 == ".doc")
                {
                    rtnval = "<img class='multidocext' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/doc1hover.png" + "' />";
                }
                else if (ext1 == ".docx")
                {
                    rtnval = "<img class='multidocext' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/doc1hover.png" + "' />";
                }
                else if (ext1 == ".xls")
                {
                    rtnval = "<img class='multidocext' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/xls1hover.png" + "' />";
                }
                else if (ext1 == ".xlsx")
                {
                    rtnval = "<img class='multidocext' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/xls1hover.png" + "' />";
                }
            }
            return rtnval;
        }
        catch (Exception ex)
        {
            return rtnval;
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

    protected void lnkprev_Click(object sender, EventArgs e)//star contact
    {
        int pgno;
        maxpg = maxpg - 1;
        pgno = (maxpg * maxpage) - (maxpage - 1);
        pgi = pgno;
        bindwall(pgno);
    }
    protected void lnknxt_Click(object sender, EventArgs e)//starcontact
    {
        int pgno;
        maxpg = maxpg + 1;
        pgno = (maxpg * maxpage) - (maxpage - 1);
        pgi = pgno;
        bindwall(pgno);
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

    public string galleryUrlrewriting(object productid)
    {
        string strurl = "";
        try
        {
            strurl = UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(productid.ToString().Trim()), "PG");
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
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
