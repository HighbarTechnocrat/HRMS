using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Text;
using System.Web.Security;

public partial class control_header : System.Web.UI.UserControl
{
    string username = string.Empty;
    string m_flag = string.Empty;
    string online = string.Empty;
    public static string skey = "";
    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;}
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if(Page.User.Identity.IsAuthenticated)
        {
            //SAGAR ADDED this GridLines HIDING username FROM control_header 23OCT2017
            lblname.Visible = false;
            bool flagleader = Roles.IsUserInRole(Page.User.Identity.Name.ToString().Trim(), "Location Leader");
            bool flagadmin = Roles.IsUserInRole(Page.User.Identity.Name.ToString().Trim(), "Administrator");
            bool flagsadmin = Roles.IsUserInRole(Page.User.Identity.Name.ToString().Trim(), "Super Administrator");
            bool flagsuperadmin = Roles.IsUserInRole(Page.User.Identity.Name.ToString().Trim(), "Super Admin");
           
            if (flagleader == true || flagadmin == true || flagsadmin == true || flagsuperadmin == true)
            {
                admin.Visible = true;
            }
            else
            {
                admin.Visible = false;
            }
            if (!IsPostBack)
            {
                login();
                loadorder();
                loadgrid();
                loadmsg();

                //SAGAR COMMENTED THIS METHOD FOR REMOVING ADDPOST METHOD 21SEPT2017
                //loadaddpost();

                //SAGAR COMMENTED THIS METHOD FOR REMOVING BROWSE METHOD 21SEPT2017
                //loadcats();
                popularcontrol2();
                loadhcclinks();
            }
        }
    }
    protected void loadorder()
    {
        if (Page.User.Identity.IsAuthenticated)
        {
           
            navuser.Visible = true;
            DataTable user = classreviews.getuseridbyemail(Page.User.Identity.Name);
            if (user.Rows.Count > 0)
            {
                hduserid.Value = user.Rows[0]["indexid"].ToString();
                HttpCookie ckey = HttpContext.Current.Request.Cookies["ASP.NET_SessionId"];
                if(ckey!=null)
                {
                    skey = ckey.Value.ToString().Trim();
                }
                HttpCookie cookie = HttpContext.Current.Request.Cookies["internet"];
                if (cookie != null)
                {
                    if (cookie.Value.ToString().Trim() == "true")
                    {
                        navchattrigger.HRef = ConfigurationManager.AppSettings["chatURL"] + "default.aspx?id=" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString())+"&skey="+skey+"&internet=true";
                    }
                    else
                    {
                        navchattrigger.HRef = "http://localhost/hccchat/" + "default.aspx?id=" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString()) + "&skey=" + skey + "&internet=false";
                    }
                }
                else
                {
                    navchattrigger.HRef = "http://localhost/hccchat/" + "default.aspx?id=" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString()) + "&skey=" + skey + "&internet=false";
                }
                
                lnkchatlink.HRef = ReturnUrl("chatapp").ToString() + "default.aspx?id=" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString());
                lnkprof.HRef = ReturnUrl("sitepathmain") + "profile/" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString());
                lnkrating.HRef = ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString());
                lnkfollowing.HRef = ReturnUrl("sitepathmain") + "following/" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString());
                lnkfollower.HRef = ReturnUrl("sitepathmain") + "followers/" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString());
                lnkviewprofile.HRef = ReturnUrl("sitepathmain") + "profile/" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString());
                lnkeditprofile.HRef = ReturnUrl("sitepathmain") + "procs/editprofile";

               //SAGAR COMMENTED THIS LINE FOR REMOVING GROUP CONTROL FROM FRONT END 19SEPT2017
                //lnkgroup.HRef = ReturnUrl("sitepathmain") + "groups/" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString());

                lnkfavorites.HRef = ReturnUrl("sitepathmain") + "favorites/" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString());
               // NEW CODE ADDED BY SONY SHOWING USER NAME OF USER ONLY ON DEFAULT PAGE 5OCT2017
                //if (Request.Url.AbsoluteUri.Contains("http://localhost/hrms/default.aspx") || Request.Url.AbsoluteUri.Contains("http://localhost/hrms/default"))
                //{
                //    lblname.Text = "Welcome " + user.Rows[0]["firstname"].ToString().Trim() + " " + user.Rows[0]["lastname"].ToString().Trim();
                //}
                //else
                //{
                //    lblname.Text = " ";
                //}
                lblfname.Text = user.Rows[0]["firstname"].ToString().Trim();
                
                DataTable dtadd = classaddress.getuserinfodetails(Page.User.Identity.Name);
                
                string proimg = "";
                if (dtadd.Rows.Count > 0)
                {   
                    if (dtadd.Rows[0]["profilephoto"].ToString().Trim() == "" || dtadd.Rows[0]["profilephoto"].ToString().Trim() == null)
                    {
                        proimg = "noimage1.png";
                    }
                    else
                    {
                        proimg = dtadd.Rows[0]["profilephoto"].ToString().Trim();
                    }
                }
                else
                {
                    proimg = "noimage1.png";
                }
                //profileimg.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/" + proimg;
                imgbigpic.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/" + proimg;
                m_flag = "U";
            }
        }
    }
    public void loadgrid()
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            DataTable user = classreviews.getuseridbyemail(Page.User.Identity.Name);
            if(user.Rows.Count > 0)
            {
                username = user.Rows[0]["username"].ToString();
            }
            else
            {
                username = Page.User.Identity.Name.ToString();
            }
            DataTable dt2 = new DataTable();

            DataTable dt1 = classreviews.getNotificationHeader(username);
            if (dt1.Rows.Count > 0)
            {
                dt2 = dt1.AsEnumerable().Take(10).CopyToDataTable();
                rptnotification.DataSource = dt2;
                rptnotification.DataBind();
                int count1 = dt1.Rows.Count;
                if (rptnotification.Items.Count > 0)
                {
                    divallnotify.Visible = true;
                    divmsg.Visible = false;
                    notificationsbody.Visible = true;


                    //notification_count.Text = count1.ToString();
                    hdcount.Value = count1.ToString(); 
                    for (int i = 0; i < rptnotification.Items.Count; i++)
                    {
                        Label lbldate = (Label)rptnotification.Items[i].FindControl("lbldate");
                        Image imgprofile = (Image)rptnotification.Items[i].FindControl("imgprofile");
                        if (dt2.Rows[i]["profilephoto"].ToString() != "")
                        {
                            if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profile55x55/" + dt2.Rows[i]["profilephoto"].ToString())))
                            {
                                imgprofile.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/" + dt2.Rows[i]["profilephoto"].ToString();
                            }
                            else
                            {
                                imgprofile.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/noimage1.png";
                            }
                        }
                        else
                        {
                            imgprofile.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/noimage1.png";
                        }
                        DateTime dat = Convert.ToDateTime(dt2.Rows[i]["eventdate"].ToString());
                        lbldate.Text = dat.ToString("dd-MMM-yyyy") + " " + dat.ToString("h:mm tt");
                    }
                    if (count1 > 10)
                    {
                        divallnotify.Visible = true;
                    }
                    else
                    {
                        divallnotify.Visible = true;
                    }
                }
                else
                {
                    divallnotify.Visible = true;
                    divmsg.Visible = true;
                    notification_count.Visible = false;
                    notification_count.Attributes.Add("style", "display:none;");
                    notificationsbody.Visible = false;
                }
            }
            else
            {
                divallnotify.Visible = true;
                divmsg.Visible = true;
                notification_count.Visible = false;
                notification_count.Attributes.Add("style", "display:none;");
                notificationsbody.Visible = false;
            }
        }
        else
        {
            divallnotify.Visible = true;
            divmsg.Visible = true;
            notification_count.Visible = false;
            notification_count.Attributes.Add("style", "display:none;");
            notificationsbody.Visible = false;
        }
    }

    //SAGAR COMMENTED THIS FOR REMOVING ADDPOST LOGIC 21SEPT2017 STARTS HERE
    //public void loadaddpost()
    //{
    //    try
    //    {
    //        DataSet dtcat = new DataSet();
    //        dtcat.ReadXml(ReturnUrl("adminsitepath") + "xml/addcats.xml");
    //        if (dtcat.Tables.Count > 0)
    //        {
    //            if (dtcat.Tables.Count > 0)
    //            {
    //                rptcat.DataSource = dtcat;
    //                rptcat.DataBind();
    //            }
    //        }
    //        else
    //        {
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
    //SAGAR COMMENTED THIS FOR REMOVING ADDPOST LOGIC 21SEPT2017 ENDS HERE


    //SAGAR COMMENTED THIS FOR REMOVING BROWSE SECTION LOGIC 21SEPT2017 STARTS HERE
    //public void loadcats()
    //{
    //    try
    //    {
    //        DataSet dtcat = new DataSet();
    //        dtcat.ReadXml(ReturnUrl("adminsitepath") + "xml/cats.xml");
    //        if (dtcat.Tables.Count > 0)
    //        {
    //            if (dtcat.Tables[0].Rows.Count > 0)
    //            {
    //                rptcats.DataSource = dtcat.Tables[0];
    //                rptcats.DataBind();
    //            }
    //        }
    //        else
    //        {
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
    //SAGAR COMMENTED THIS FOR REMOVING BROWSE SECTION LOGIC 21SEPT2017 ENDS HERE

    public string getAddPostURL(object catname, object cid)
    {
        string strurl = "";
        try
        {
            strurl = UrlRewritingVM.getUrlRewritingpost(catname.ToString().Trim(), UrlRewritingVM.Encrypt(cid.ToString().Trim()),"APS");
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
    public string getcategoryURL(object catname, object cid)
    {
        string strurl = "";
        try
        {
            strurl = UrlRewritingVM.getUrlRewritingInfo(catname.ToString().Trim(), UrlRewritingVM.Encrypt(cid.ToString().Trim()), "PS");
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
    public void loadmsg()
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            DataTable user = classreviews.getuseridbyemail(Page.User.Identity.Name);
            DataTable dt2 = new DataTable();
            DataTable dt1 = classreviews.getnewmessage(Page.User.Identity.Name.ToString().Trim());
            if (dt1.Rows.Count > 0)
            {
                dt2 = dt1.AsEnumerable().Take(10).CopyToDataTable();
                rptmsg.DataSource = dt2;
                rptmsg.DataBind();
                int count = dt1.Rows.Count;
                if (rptmsg.Items.Count > 0)
                {
                    msg_count.Text = count.ToString();
                    msghdcount.Value = count.ToString();
                    for (int i = 0; i < rptmsg.Items.Count; i++)
                    {
                        Label lbldate = (Label)rptmsg.Items[i].FindControl("lbldate");
                        Image imgprofile = (Image)rptmsg.Items[i].FindControl("imgprofile");
                        if (dt2.Rows[i]["profilepic"].ToString() != "")
                        {
                            if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profile55x55/" + dt2.Rows[i]["profilepic"].ToString())))
                            {
                                imgprofile.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/" + dt2.Rows[i]["profilepic"].ToString();
                            }
                            else
                            {
                                imgprofile.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/noimage1.png";
                            }
                        }
                        else
                        {
                            imgprofile.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/noimage1.png";
                        }
                        DateTime dat = Convert.ToDateTime(dt2.Rows[i]["eventdate"].ToString());
                        lbldate.Text = dat.ToString("dd-MMM-yyyy") + " " + dat.ToString("h:mm tt"); ;
                    }
                    if (count > 2)
                    {
                        msgtitleall.Visible = true;
                    }
                    else
                    {
                        msgtitleall.Visible = false;
                    }
                }
                else
                {
                    divmsg1.Visible = true;
                    pnkmsg.Visible = false;
                    msgtitleall.Visible = false;
                    msg_count.Visible = false;
                    msg_count.Attributes.Add("style", "display:none;");
                }
            }
            else
            {
                msghdcount.Value = dt1.Rows.Count.ToString();
                pnkmsg.Visible = false;
                msgtitleall.Visible = false;
                divmsg1.Visible = true;
                msg_count.Attributes.Add("style", "display:none !important;");
                msg_count.Visible = false;
            }
        }
        else
        {
            int hdcnt = 0;
            msghdcount.Value = hdcnt.ToString();
            divmsg1.Visible = true;
            msg_count.Attributes.Add("style", "display:none;");
            msg_count.Visible = false;
        }
    }
    public string getnotificationURL(object DisplayText)
    {
        string strAttrValue = "";
        bool iserror = false;
        try
        {
            iserror = classreviews.updatenotification(Page.User.Identity.Name, DisplayText.ToString());
            if (iserror == true)
            {
                DataTable user = classreviews.getuseridbyemail(DisplayText.ToString());
                if (user.Rows.Count > 0)
                {
                    strAttrValue = ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString());
                }
            }
            return strAttrValue;
        }
        catch (Exception ex)
        {
            return strAttrValue;
        }
    }
    protected void rptnotification_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
    }
    protected void rptmsg_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
    }
    protected void lnkuser_Click(object sender, EventArgs e)
    {
        var btn = (LinkButton)sender;
        var item = (RepeaterItem)btn.NamingContainer;
        var ddl = (Label)item.FindControl("follow");
        var ddlevent = (Label)item.FindControl("lblevent");
        var lblid = (Label)item.FindControl("lblpid");
        var lblindexid = (Label)item.FindControl("indexid");
        bool iserror = false;
        iserror = true;
        if (iserror == true)
        {
            if (ddlevent.Text == "follow")
            {
                classreviews.updatenotification(Page.User.Identity.Name, ddl.Text.ToString());
                DataTable user = classreviews.getuseridbyemail(ddl.Text.ToString());
                if (user.Rows.Count > 0)
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString()));
                }
            }
            else if (ddlevent.Text == "like" || ddlevent.Text == "comment" || ddlevent.Text == "post" || ddlevent.Text == "followerpost" )
            {
                int j =0;
                j = Convert.ToInt32(classreviews.updateNotificationReadStatus(Convert.ToInt32(lblindexid.Text.ToString()), "read"));
                 if (j > 0)
                 {
                     DataTable ds = classproduct.get_proc_ProductDescription_ProdFeature(Convert.ToInt32(lblid.Text.ToString().Trim()));
                     if (ds.Rows.Count > 0)
                     {
                         Response.Redirect(UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(ds.Rows[0]["productid"].ToString()), "PDID"));
                     }
                 }
            }
            else if(ddlevent.Text == "review")
            {
                int j = 0;
                j = Convert.ToInt32(classreviews.updateNotificationReadStatus(Convert.ToInt32(lblindexid.Text.ToString()), "read"));
                if (j > 0)
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "comments/" + UrlRewritingVM.Encrypt(lblid.Text.ToString()));
                }
            }
            else if (ddlevent.Text == "survey")
            {
                Response.Redirect(UrlRewritingVM.getUrlRewritingInfo("",  UrlRewritingVM.Encrypt(lblid.Text.ToString().Trim()), "SR"));
            }
        }
    }
    protected void lnkuser1_Click(object sender, EventArgs e)
    {
        var btn = (LinkButton)sender;
        var item = (RepeaterItem)btn.NamingContainer;
        var id = (Label)item.FindControl("lblmsgid");
        var ddlevent = (Label)item.FindControl("lblevent");
        if (ddlevent.Text == "message")
        {
            bool flag = classmailsend.UpdateUnreadStatus(Convert.ToInt32(id.Text.ToString()), "N");
            Response.Redirect(string.Format(ReturnUrl("sitepathmain") + "composemail.aspx?mid={0}", UrlRewritingVM.Encrypt(id.Text.ToString())));
        }
    }
    protected void btnSingOut_Click(object sender, EventArgs e)
    {
        if(Page.User.Identity.IsAuthenticated)
        {
            classaddress.UpdateUserStatus(Page.User.Identity.Name.ToString().Trim(), 0);
        }
        HttpCookie cookie = HttpContext.Current.Request.Cookies["internet"];
        if (cookie != null)
        {
            if (cookie.Value.ToString().Trim() == "true")
            {
                Session.Clear();
                Session.Abandon();
                Request.Cookies.Clear();
                FormsAuthentication.SignOut();
                Response.Redirect(ConfigurationManager.AppSettings["internetURL"].ToString().Trim());
            }
            else
            {
                Session.Clear();
                Session.Abandon();
                Request.Cookies.Clear();
                FormsAuthentication.SignOut();
                Response.Redirect(ConfigurationManager.AppSettings["intranetURL"].ToString().Trim());
            }
        }
        else
        {
            Session.Clear();
            Session.Abandon();
            Request.Cookies.Clear();
            FormsAuthentication.SignOut();
            Response.Redirect(ConfigurationManager.AppSettings["intranetURL"].ToString().Trim());
        }
    }
    protected void popularcontrol2()
    {
        try
        {
            creative.Common clsCommon = new creative.Common();
            DataSet ds = clsCommon.Read_XML("HomeBannermiddle.xml", ReturnUrl("sitepathadmin")+ "xml/");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    topheadpromo.Visible = true;
                    rptpromo.DataSource = ds.Tables[0];
                    rptpromo.DataBind();
                }
            }
            else
            {
                topheadpromo.Visible = false;
                lbllnkmsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
    public void loadhcclinks()
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies["internet"];
        if (cookie != null)
        {
            if (cookie.Value.ToString().Trim() == "true")
            {
                lnkpass.HRef = ConfigurationManager.AppSettings["ChangePasswordTrue"].ToString();
                lnkqst.HRef = ConfigurationManager.AppSettings["SecurityQstTrue"].ToString();
            }
            else
            {
                lnkpass.HRef = ConfigurationManager.AppSettings["ChangePassword"].ToString();
                lnkqst.HRef = ConfigurationManager.AppSettings["SecurityQst"].ToString();
            }
        }
        else
        {
            lnkpass.HRef = ConfigurationManager.AppSettings["ChangePassword"].ToString();
            lnkqst.HRef = ConfigurationManager.AppSettings["SecurityQst"].ToString();
        }
    }
    protected void lnkadmin_Click(object sender, EventArgs e)
    {
        MembershipUser currentUser = Membership.GetUser(Page.User.Identity.Name.ToString().Trim());
        if(currentUser != null)
        {
            Response.Redirect(ReturnUrl("sitepathadmin")+ "login.aspx?unamehigh=" + Page.User.Identity.Name.ToString().Trim());
        }
    }

    public void login()
    {
        MembershipUser user = Membership.GetUser(Page.User.Identity.Name.ToString().Trim());
        if (user != null)
        {
           online = user.IsOnline.ToString();
        }
    }
}