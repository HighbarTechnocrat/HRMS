using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Drawing;
using System.Configuration;
using System.Text.RegularExpressions;
using AjaxControlToolkit;
using System.IO;

public partial class ps : System.Web.UI.Page
{
    public static string userid;
    public static string pstname="";
    public static int pid;
    public static string catid="";
    public string fname = "";
    public static int PageSize = 12, RecordCount;
    public static int pgi=1;
    public static string gid="";
    public static string sdate="";
    public static string edate="";
    public static string title="";
    public static string favusername = "";
    public string uname = "";
    public int cid = 0;
    //public int tcid = 0;
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
            userid = Page.User.Identity.Name.ToString().Trim();
        }
        if (!IsPostBack)
        {
            txtsdate.Attributes.Add("readonly", "readonly");
            txtedate.Attributes.Add("readonly", "readonly");
            if (Request.QueryString["c"] != "" && Request.QueryString["c"] != null && Request.QueryString.Count == 1 && Request.QueryString["c"].Length == 24)
            {
               
                if (Int32.TryParse(UrlRewritingVM.Decrypt(Request.QueryString["c"]), out cid))
                {
                    pgi = 1;
                    catid = UrlRewritingVM.Decrypt(Request.QueryString["c"].ToString());

                    //sony added this code to display records of category achievements and timeout in ads type format
                    //Response.Write("Hello" + catid);
                    //Response.End();
                    //Session["tempcatid"] = catid;                 
                    getcatname();
                    bindwall(pgi);

                    #region backbutton
                    string currentPage = lblcatname.Text;

                    if ( (currentPage.ToLower().Trim() == "photos") || (currentPage.ToLower().Trim() == "water solutions") || (currentPage.ToLower().Trim() == "transportation") || (currentPage.ToLower().Trim() == "buildings &  industrial plants") || (currentPage.ToLower().Trim() == "nuclear power & special projects"))
                    {
                        gobackbtn.HRef = ReturnUrl("sitepathmain") + "default.aspx";
                        gobackbtn.Visible = true;
                        gobackbtn.Title = "Home";
                        gobackbtn.InnerText = "Home";
                    }
                    else if (currentPage.ToLower().Trim() == "video gallery")
                    {
                        gobackbtn.HRef = ReturnUrl("sitepathmain") + "default.aspx";
                        gobackbtn.Visible = true;
                        gobackbtn.Title = "Home";
                        gobackbtn.InnerText = "Home";
                    }
                    else if ((currentPage.ToLower().Trim() == "photo gallery") || (currentPage.ToLower().Trim() == "suggestion") || (currentPage.ToLower().Trim() == "achievements") || (currentPage.ToLower().Trim() == "time out") || (currentPage.ToLower().Trim() == "people speak") || (currentPage.ToLower().Trim() == "meet highbarians") || (currentPage.ToLower().Trim() == "news") || (currentPage.ToLower().Trim() == "training"))
                    {
                        gobackbtn.HRef = ReturnUrl("sitepathmain") +  "default.aspx";
                        gobackbtn.Visible = true;
                        gobackbtn.Title = "Home";
                        gobackbtn.InnerText = "Home";
                    }
                    else if (currentPage.ToLower().Trim() == "interviews")
                    {
                        gobackbtn.HRef = ReturnUrl("sitepathmain") + "Communications.aspx";
                        gobackbtn.Visible = true;
                        gobackbtn.Title = "Communications";
                        gobackbtn.InnerText = "Communications";
                    }
                    else if ((currentPage.ToLower().Trim() == "project managers conference - photographs") || (currentPage.ToLower().Trim() == "project managers conference - videos"))
                    {

                        gobackbtn.HRef = ReturnUrl("sitepathmain") + "pmmeets.aspx";
                        gobackbtn.Visible = true;
                        gobackbtn.Title = "Project Managers Conference";
                        gobackbtn.InnerText = "Project Managers Conference";
                    }
                    #endregion
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
    }
    public void getcatname()
    {
        try
        {
            int cid;
           // string posttype;
            if(Int32.TryParse(catid, out cid))
            {
                DataTable dt = classproductimage.getcategorydetails(Convert.ToInt32(commonclass.GetSafeIDFromURL(catid)));
                
                if (dt.Rows.Count > 0)
                {

                   lblcatname.Text = dt.Rows[0]["categoryname"].ToString();
                  // Response.Write(lblcatname.Text);
                 //  Response.End();
               
                   
                }
                else
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
                }

                if (catid == "28" || catid == "44" || catid == "33")
                {
                    lnksearch.Visible = false;
                    lnkreset.Visible = false;
                    txtsearch.Visible = false;
                    // Response.Write("Test");
                }                      
            }
            else
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
            }
        }
             
        catch(Exception ex)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
        }
    }
    public void bindwall(int PageIndex)
    {
        pnlpst.Visible = true;
        lblmsg.Visible = false;
        DataSet dswall = new DataSet();
        if (txtsdate.Text == "")
        {
            sdate = "";
        }
        if (txtedate.Text == "")
        {
            edate = "";
        }
        if (txtsearch.Text == "")
        {
            pstname = "";
        }
        else
        {
            pstname = commonclass.GetSafeSearchString(pstname);
        }
        gid = "";
        DataTable grp = classnews.grpListbyuser(userid);
        if (grp.Rows.Count > 0)
        {
            for (int i = 0; i < grp.Rows.Count; i++)
            {
                if (grp.Rows.Count == 1)
                {
                    gid = gid + grp.Rows[i]["grpid"].ToString();
                }
                else if (i == (Convert.ToInt32(grp.Rows.Count) - 1))
                {
                    gid = gid + grp.Rows[i]["grpid"].ToString();
                }
                else
                {
                    gid = gid + grp.Rows[i]["grpid"].ToString() + ",";
                }
            }
        }

        dswall = classproductimage.browserWallAllSearch(catid, pstname, gid, uname, favusername, title, sdate, edate, PageIndex, PageSize, out RecordCount);
		rptwall.DataSource = null;
        rptwall.DataBind();
        rptwallTemp.DataSource = null;
        rptwallTemp.DataBind();

        if (dswall.Tables.Count >0)
        {
            if(dswall.Tables[0].Rows.Count > 0)
            {
                rptwall.DataSource = dswall.Tables[0];
                rptwall.DataBind();
                rptwallTemp.DataSource = dswall.Tables[0];
                rptwallTemp.DataBind();
               
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
                            string currentPage = lblcatname.Text;
                            if ((currentPage.ToLower().Trim() == "project managers conference - photographs") || (currentPage.ToLower().Trim() == "photos") || (currentPage.ToLower().Trim() == "water solutions") || (currentPage.ToLower().Trim() == "transportation") || (currentPage.ToLower().Trim() == "buildings &  industrial plants") || (currentPage.ToLower().Trim() == "nuclear power & special projects"))
                            {

                                DataTable dt = classproduct.getmultiplepostimage1(Convert.ToInt32(lblpid.Text));
                                Repeater childRepeater = (Repeater)rptwallTemp.Items[i].FindControl("rptprojectimages1");
                                foreach (DataRow dr in dt.Rows)
                                {
                                    //  if (dr["showonstatus"].ToString() == "Y")
                                    //  dr.Delete();
                                }
                                dt.AcceptChanges();
                                if (dt.Rows.Count > 0)
                                {
                                    childRepeater.DataSource = dt;
                                    childRepeater.DataBind();
                                }

                            }
                            else
                            {
                                Repeater childRepeater = (Repeater)rptwallTemp.Items[i].FindControl("rptprojectimages1");
                                childRepeater.DataSource = dswall.Tables[0];
                                childRepeater.DataBind();
                            }
                           
                            DataTable dtlike = classreviews.getlikedetailsbyuserpostid(userid, Convert.ToInt32(lblpid.Text));
                            if (dtlike.Rows.Count > 0)
                            {
                                //Like Module
                                if (dtlike.Rows[0]["likeflag"].ToString().Trim() == "Unlike")
                                {
                                    liketext = "<font style='font-weight:400;color:#3199cc !important;'>" + "Like" + "</font>";
                                    lbllike.Text = "<i class='fa fa-thumbs-up' style='color:#3199cc !important;'></i> " + liketext;
                                    if (Convert.ToInt32(dtlike.Rows[0]["likecount"].ToString().Trim()) > 0)
                                    {
                                        lbllikecount.Text = " (" + dtlike.Rows[0]["likecount"].ToString().Trim() + ")";
                                        lbllikecount.ForeColor = System.Drawing.Color.FromName("#3199cc");
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
                                lbldislikecount.Visible = false;
                                lnkcomment.Text = "<i class='fa fa-comment'></i> Comment";
                            }
                        }
                        else
                        {
                            DataTable dtlike = classreviews.getlikedetailsbyuserpostid("", Convert.ToInt32(lblpid.Text));
                            if (dtlike.Rows.Count > 0)
                            {
                                //Like Module
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

                                //Comment Module
                                if (Convert.ToInt32(dtlike.Rows[0]["commentcount"].ToString()) > 0)
                                {
                                    lnkcomment.Text = "<i class='fa fa-comment'></i> Comment" + " (" + dtlike.Rows[0]["commentcount"].ToString() + ")";
                                }
                                else
                                {
                                    lnkcomment.Text = "<i class='fa fa-comment'></i> Comment";
                                }
                                lnklike.PostBackUrl = ReturnUrl("sitepath") + "login.aspx?ReturnUrl=" + Request.RawUrl.ToString();
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
                    pnlpst.Visible = false;
                    lblmsg.Visible = true;
                    //lblmsg.Text = "No Post Found In " + lblcatname.Text.ToString();
                    lblmsg.Text = "Coming Soon";
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
                pnlpst.Visible = false;
                lblmsg.Visible = true;
                //lblmsg.Text = "No Post Found In " + lblcatname.Text.ToString();
                lblmsg.Text = "Coming Soon";
            }
        }
        else
        {
            pnlpst.Visible = false;
            lblmsg.Visible = true;
            //lblmsg.Text = "No Post Found In " + lblcatname.Text.ToString();
            lblmsg.Text = "Coming Soon";
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
   
    public string productUrlrewriting2(object cattype, object productname, object productid)
    {
        string strurl = "";
        string cat = "";
        try
        {
            cat = cattype.ToString();
            //sagar commented below original working line 19dec2017 
            //if (cat == "I")
            //sagar commented above original working line 19dec2017 

            //sagar added below line for changing display of achievemnt and timeout on ps page 19dec2017 
            if (cat == "I" && catid!="44" && catid!="33")
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
                Response.Redirect(ReturnUrl("sitepathmain") + "comments/" + e.CommandArgument.ToString());
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
        dblmainpg = (double)((double)pageCount / Convert.ToDouble(maxpage));
        maxpgcount = (int)Math.Ceiling(dblmainpg);
        int pgno = 0;
        List<ListItem> pages = new List<ListItem>();
        if (pageCount > 0)
        {
            if (maxpgcount > 1)
            {
                lnknxt.Visible = true;
            }
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
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        pgi = pageIndex;
		rptwall.DataSource = null;
        rptwall.DataBind();
        this.bindwall(pageIndex);
    }
    protected void lnksearch_Click(object sender, EventArgs e)
    {
        //pstname = txtsearch.Text.ToString();
        //sdate = commonclass.GetSafeDate(txtsdate.Text.ToString().Trim());
        //edate = commonclass.GetSafeDate(txtedate.Text.ToString().Trim());
        //bindwall(1);
        string connstr = "";
		rptwall.DataSource = null;
        rptwall.DataBind();
        rptwallTemp.DataSource = null;
        rptwallTemp.DataBind();
        connstr = commonclass.GetSafeURLForBrowsePostPage(Request.Url.AbsoluteUri.ToString());
        if (connstr.ToLower() == "true")
        {
            if (Request.QueryString["c"] != "" && Request.QueryString["c"] != null && Request.QueryString.Count == 1)
            {
                if (Int32.TryParse(UrlRewritingVM.Decrypt(Request.QueryString["c"]), out cid))
                {
                    catid = UrlRewritingVM.Decrypt(Request.QueryString["c"].ToString());
                    pstname = txtsearch.Text.ToString();
                    sdate = commonclass.GetSafeDate(txtsdate.Text.ToString().Trim());
                    edate = commonclass.GetSafeDate(txtedate.Text.ToString().Trim());
                    bindwall(1);
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
    }
    protected void lnkprev_Click(object sender, EventArgs e)//star contact
    {
        int pgno;
        maxpg = maxpg - 1;
        pgno = (maxpg * maxpage) - (maxpage - 1);
        pgi = pgno;
		
		rptwall.DataSource = null;
        rptwall.DataBind();
        bindwall(pgno);
    }
    protected void lnknxt_Click(object sender, EventArgs e)//starcontact
    {
        int pgno;
        maxpg = maxpg + 1;
        pgno = (maxpg * maxpage) - (maxpage - 1);
        pgi = pgno;
		rptwall.DataSource = null;
        rptwall.DataBind();
        bindwall(pgno);
    }
    protected void lnkreset_Click(object sender, EventArgs e)
    {
        txtsearch.Text = "";
        txtsdate.Text = "";
        txtedate.Text = "";
        pstname = "";
        sdate = "";
        edate = "";
		rptwall.DataSource = null;
        rptwall.DataBind();
        bindwall(1);
    }

//sagar added belowmethod working for file like download link for 26dec2017 starts here
    public string getFileUrl(object filename)
    {
        string strurl = "";
        try
        {
            string fileName = Request.PhysicalApplicationPath + "\\files\\" + filename.ToString().Trim();
            if (File.Exists(fileName))
            {
                strurl = ReturnUrl("sitepathmain") + "files/" + filename;
            }
            else
            {
                strurl = ConfigurationManager.AppSettings["sitepathadmin"] + "files/" + filename;
            }
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
    //sagar added belowmethod working for file like download link for 26dec2017 ends here

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
                    // strurl = "<img src='" + ConfigurationManager.AppSettings["sitepathadmin"] + "images/bigproduct/" + imagepath.ToString() + "' />";
                }
                else
                {
                    strurl = "<img src='" + ConfigurationManager.AppSettings["sitepathadmin"] + "images/450x300/" + imagepath.ToString() + "' />";
                    //strurl = "<img src='" + ConfigurationManager.AppSettings["sitepathadmin"] + "images/bigproduct/" + imagepath.ToString() + "' />";
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
                if (videocode.ToString() != "N/A" && videocode.ToString() != "n/a" && videocode.ToString().Length > 0 && videocode.ToString() != "")
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
                                //strurl = "<video controls preload='metadata' class='videourl' data-setup='{}'><source src='" + ConfigurationManager.AppSettings["internetURL"].ToString().Trim() + videourl + "' type='video/mp4'></video>";
                                strurl = "<video controls preload='metadata' class='videourl' data-setup='{}'><source src='" + videourl + "' type='video/mp4'></video>";
                            }
                            else
                            {
                                //strurl = "<video controls preload='metadata' class='videourl' data-setup='{}'><source src='" + ConfigurationManager.AppSettings["intranetURL"].ToString().Trim() + videourl + "' type='video/mp4'></video>";
                                strurl = "<video controls preload='metadata' class='videourl' data-setup='{}'><source src='" + videourl + "' type='video/mp4'></video>";
                            }
                        }
                        else
                        {
                            //pstvideo.InnerHtml = "<font color='red' class='loadvideomsg' size='16'>Failed To load Video</font>";
                            strurl = "<video controls preload='metadata' class='videourl' data-setup='{}'><source src='" + videourl + "' type='video/mp4'></video>";

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
                    //sagar added below Panel1.Visible = false; and Panel2.Visible = true; for document type diffrent display 22dec2017  
                    Panel1.Visible = false;
                    Panel2.Visible = true;
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
    protected void rptwallTemp_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        rptwall.Visible = false;
        string currentPage = lblcatname.Text;
        (e.Item.FindControl("DocName") as System.Web.UI.HtmlControls.HtmlControl).Visible = false;
        (e.Item.FindControl("DocDesc") as System.Web.UI.HtmlControls.HtmlControl).Visible = false;
        (e.Item.FindControl("uInfo") as System.Web.UI.HtmlControls.HtmlControl).Visible = false;
        (e.Item.FindControl("ucomm") as System.Web.UI.HtmlControls.HtmlControl).Visible = false;
        (e.Item.FindControl("img1") as System.Web.UI.HtmlControls.HtmlControl).Visible = false;
        (e.Item.FindControl("pnlvideo") as System.Web.UI.HtmlControls.HtmlControl).Visible = false;
         (e.Item.FindControl("Panel4") as System.Web.UI.HtmlControls.HtmlControl).Visible = false;
         (e.Item.FindControl("pnlimg1") as System.Web.UI.HtmlControls.HtmlControl).Visible = false;

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (currentPage.ToLower().Trim() == "training")
             {              
                (e.Item.FindControl("DocName") as System.Web.UI.HtmlControls.HtmlControl).Visible = false;
                (e.Item.FindControl("DocDesc") as System.Web.UI.HtmlControls.HtmlControl).Visible = false;
                rptwall.Visible = true;
             }
            if (currentPage.ToLower().Trim() == "meet highbarians" || currentPage.ToLower().Trim() == "people speak" || currentPage.ToLower().Trim() == "video gallery" || currentPage.ToLower().Trim() == "interviews" || currentPage.ToLower().Trim() == "project managers conference - videos")
             {
                 (e.Item.FindControl("pnlvideo") as System.Web.UI.HtmlControls.HtmlControl).Visible = true;
                 (e.Item.FindControl("uInfo") as System.Web.UI.HtmlControls.HtmlControl).Visible = true;
             }
            if ((currentPage.ToLower().Trim() == "project managers conference - photographs") || (currentPage.ToLower().Trim() == "photos") || (currentPage.ToLower().Trim() == "water solutions") || (currentPage.ToLower().Trim() == "transportation") || (currentPage.ToLower().Trim() == "buildings &  industrial plants") || (currentPage.ToLower().Trim() == "nuclear power & special projects"))
             {
                 (e.Item.FindControl("img1") as System.Web.UI.HtmlControls.HtmlControl).Visible = true;
                 (e.Item.FindControl("uInfo") as System.Web.UI.HtmlControls.HtmlControl).Visible = true;
                 (e.Item.FindControl("pnlimg1") as System.Web.UI.HtmlControls.HtmlControl).Visible = true;

             }            
             if (currentPage.ToLower().Trim() == "photo gallery" || currentPage.ToLower().Trim() == "achievements" || currentPage.ToLower().Trim() == "suggestion" || currentPage.ToLower().Trim() == "time out" || currentPage.ToLower().Trim() == "news")
             {
                 (e.Item.FindControl("img1") as System.Web.UI.HtmlControls.HtmlControl).Visible = true;
                 (e.Item.FindControl("uInfo") as System.Web.UI.HtmlControls.HtmlControl).Visible = true;
                 (e.Item.FindControl("Panel4") as System.Web.UI.HtmlControls.HtmlControl).Visible = true;
             }
        }
    }
}