using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Security;
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

public partial class Themes_FirstTheme_LayoutControls_topheaderlogo : System.Web.UI.UserControl
{
    public static int i = 0;
    OpenIdRelyingParty openid = new OpenIdRelyingParty();
    private Random random = new Random();
    
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        WebClient client = new WebClient();
        Stream stream = client.OpenRead(ConfigurationManager.AppSettings["sitepathadmin"]+ "Menu/menu.html");
        StreamReader sr = new StreamReader(stream);
        string content = sr.ReadToEnd();
        desktop.InnerHtml = content;
        //DataTable dt = classreviews.getuseridbyemail(Page.User.Identity.Name);
        //if (dt.Rows.Count > 0)
        //{
        //    if(dt.Rows[0]["profilephoto"].ToString()!="")
        //    {
        //        imgprofile.Src = "https://graph.facebook.com/" + dt.Rows[0]["profilephoto"].ToString() + "/picture?type=large";
        //    }
        //    else
        //    {
        //        imgprofile.Src =ReturnUrl("sitepath")+"images/noprofile.jpg";
        //    }
        //    lnkuser.HRef = ReturnUrl("sitepathmain")+"reviewratings.aspx?userid="+dt.Rows[0]["indexid"].ToString();
        //}
        carttotal();
  if (Request.RawUrl.Contains("default"))
        {
spanlogo.Attributes.Add("class","sitelogo-beta");
        }
        else
        {
spanlogo.Attributes.Add("class","sitelogo");
         }
        if (!IsPostBack)
        {         
            loadorder();
            loadgrid();
        }
        else
        {
        }
    }

    public void loadgrid()
    {
        if(Page.User.Identity.IsAuthenticated)
        { 
        DataTable user = classreviews.getuseridbyemail(Page.User.Identity.Name);
        DataTable dt2 = classreviews.getnotification(user.Rows[0]["username"].ToString());
        if(dt2.Rows.Count>0)
        {
            rptnotification.DataSource = dt2;
            rptnotification.DataBind();
	    
            if (rptnotification.Items.Count > 0)
           {
               notification_count.Text = rptnotification.Items.Count.ToString();
               for (int i = 0; i < rptnotification.Items.Count; i++)
                {
                //LinkButton lnkuser = (LinkButton)rptnotify.Items[i].FindControl("lnkuser");
                    Label lbldate = (Label)rptnotification.Items[i].FindControl("lbldate");
                    Image imgprofile = (Image)rptnotification.Items[i].FindControl("imgprofile");
                if (dt2.Rows[i]["profilephoto"].ToString() != "")
                {
                    if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profilephoto/" + dt2.Rows[i]["profilephoto"].ToString())))
                    {
                        imgprofile.ImageUrl = ReturnUrl("sitepath") + "images/profilephoto/" + dt2.Rows[i]["profilephoto"].ToString();
                    }
                    else
                    {
                        imgprofile.ImageUrl = "https://graph.facebook.com/" + dt2.Rows[i]["profilephoto"].ToString() + "/picture?type=large";
                    }

                }
                else
                {
                    imgprofile.ImageUrl = ConfigurationManager.AppSettings["adminsitepath"]+ "images/noprofile.jpg";
                }
               //lnkuser.PostBackUrl = ReturnUrl("sitepathmain") + "user/" + dt2.Rows[i]["indexid"].ToString();
                DateTime dat = Convert.ToDateTime(dt2.Rows[i]["followingdate"].ToString());
                lbldate.Text = dat.ToString("dd-MMM-yyyy");
            }

        }
        else
        {
            divmsg.Visible = true;
            notification_count.Visible = false;
	    notification_count.Attributes.Add("style","display:none;");
            notificationsbody.Visible = false;
        }
        }
        else
        {
	    notificationsbody.Visible = false;
            divmsg.Visible = true;
            notification_count.Visible = false;
            //divnotify.Visible = false;
        }
       }
        else
        {
            lnkprofile.Visible = false;
            divnotify.Visible = false;
            notification_count.Visible = false;
            notificationsbody.Visible = false;
        }
        
    }

    protected void rptnotification_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //bool iserror;
        //if (e.CommandName == "cmdnotify")
        //{
        //    try
        //    {
        //        iserror = classreviews.updatenotification(Page.User.Identity.Name, e.CommandArgument.ToString());
        //        if (iserror == true)
        //        {
        //            DataTable user = classreviews.getuseridbyemail(e.CommandArgument.ToString());
        //            if (user.Rows.Count > 0)
        //            {
        //                Response.Redirect(ReturnUrl("sitepathmain") + "user/" + user.Rows[0]["indexid"].ToString());
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }

    protected void btnSingOut_Click(object sender, EventArgs e)
    {
     
        Session.Clear();
        Session.Abandon();
        Request.Cookies.Clear();
        FormsAuthentication.SignOut();
        Response.Redirect(ReturnUrl("sitepathmain") + "default", true);
    }
    protected void loadorder()
    {
        DataTable dtp= classpkg.getcountorderdetailbyemaild(Page.User.Identity.Name);
        if (dtp.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtp.Rows[0]["orderid"].ToString()) == 0)
            {

                lihistory.Visible = false;
            }
        }
        else
        {

            lihistory.Visible = true;
        }


    }
    protected void carttotal()
    {
        string username = string.Empty;
        string m_flag = string.Empty;
        if (Page.User.Identity.IsAuthenticated)
        {
            username = Page.User.Identity.Name.ToString().Trim();
            DataTable dtadd = classaddress.getuserinfodetails(username);          
             string strfname="";
             if (dtadd.Rows.Count > 0)
             {
                 lnkprofile.HRef = ReturnUrl("sitepathmain") + "user/" + dtadd.Rows[0]["indexid"].ToString();
                 viewprofile.HRef = ReturnUrl("sitepathmain") + "user/" + dtadd.Rows[0]["indexid"].ToString();
                 strfname = dtadd.Rows[0]["firstname"].ToString();
             }
             else
             {
                 strfname = "Guest";
             }
            if (strfname.Length <= 20)
            {
                lblfirstname.Text = strfname;
            }
            else
            {
                strfname = (dtadd.Rows[0]["firstname"].ToString()).Substring(0, 20);
                lblfirstname.Text = strfname + "...";
            }
            m_flag = "U";
        }
        else
        {
            username = getRemoteAddr();
            m_flag = "P";
        }
        DataSet ds_wishlist = new DataSet();
        ds_wishlist = clscartlist.get_cartlist_count_from_username(username.ToString().Trim());

        if (ds_wishlist.Tables[0].Rows.Count > 0)
        {
            if (ds_wishlist.Tables[0].Rows[0]["m_count"].ToString() != "0")
            {
                lblcarttotal1.Text = ds_wishlist.Tables[0].Rows[0]["m_count"].ToString();
            }
        }
        else
        {
        }
    }
    public string getRemoteAddr()
    {
        string UserIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (UserIPAddress == null)
        {
            UserIPAddress = Request.ServerVariables["REMOTE_ADDR"];
        }
        return UserIPAddress;
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        string strurl = "";
        if (txtsearch.Text.ToString().Trim() != "")
        {
            ExecuteSearch(txtsearch.Text.ToString().Trim());
            strurl = UrlRewritingVM.getUrlRewritingInfo(txtsearch.Text.ToString().Trim(), "", "SA");
            Response.Redirect(strurl);
        }
        //else
        //{
        //    strurl = UrlRewritingVM.getUrlRewritingInfo(txtsearch.Text.ToString().Trim(), "", "SA");
        //    Response.Redirect(strurl);
        //   //Response.Redirect(ReturnUrl("sitepathmain") + "default");
        //}
    }
    private void ExecuteSearch(string strsearchTextBox)
    {
        string sitepath = creativeconfiguration.SitePath;
        strsearchTextBox = HttpUtility.UrlDecode(strsearchTextBox);
        if (txtsearch.Text.Trim() != "")
        {
            updatecount(strsearchTextBox);
        }
    }
    private void updatecount(string strname)
    {
        string strcatsessionsdecode = HttpUtility.UrlDecode(strname);
        DataSet ds = classkeyword.getsearchlistBySearchbyname(strcatsessionsdecode);
        if (ds.Tables[0].Rows.Count > 0)
        {
            int keyid = Convert.ToInt32(ds.Tables[0].Rows[0]["keywordid"]);
            int freq = Convert.ToInt32(ds.Tables[0].Rows[0]["keyordfrequency"]);
            freq = freq + 1;
            bool flag1 = classkeyword.updatekeywordfrequency(Convert.ToDecimal(keyid),Convert.ToDecimal(freq));
        }
        else
        {
            bool flag = classkeyword.createkeyword(strcatsessionsdecode, 1, "", 'H', Page.User.Identity.ToString());
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }
    protected void lbtnback_Click(object sender, EventArgs e)
    {
      
    }
    #region Google plus login
    //protected void HandleOpenIDProviderResponse()
    //{
    //    var response = openid.GetResponse();

    //    if (response != null)
    //    {
    //        switch (response.Status)
    //        {
    //            case AuthenticationStatus.Authenticated:


    //                var fetchResponse = response.GetExtension<FetchResponse>();
    //                Session["FetchResponse"] = fetchResponse;
    //                var response2 = Session["FetchResponse"] as FetchResponse;
    //                string email = string.Empty;
    //                string name = string.Empty;
    //                string birthdate = string.Empty;
    //                string phone = string.Empty;
    //                string gender = string.Empty;
    //                email = response2.GetAttributeValue(WellKnownAttributes.Contact.Email);
    //                name = GetFullname(response2.GetAttributeValue(WellKnownAttributes.Name.First), response2.GetAttributeValue(WellKnownAttributes.Name.Last));
    //                birthdate = response2.GetAttributeValue(WellKnownAttributes.BirthDate.WholeBirthDate);
    //                phone = response2.GetAttributeValue(WellKnownAttributes.Contact.Phone.Mobile);
    //                gender = response2.GetAttributeValue(WellKnownAttributes.Person.Gender);

    //                autologin(email, response2.GetAttributeValue(WellKnownAttributes.Name.First), response2.GetAttributeValue(WellKnownAttributes.Name.Last));
    //                break;
    //            case AuthenticationStatus.Canceled:
    //                //lblAlertMsg.Text = "Cancelled.";
    //                break;
    //            case AuthenticationStatus.Failed:
    //                //lblAlertMsg.Text = "Login Failed.";
    //                break;
    //        }
    //    }
    //    else
    //    {
    //        return;

    //    }

    //}
    //protected void OpenLogin_Click(object src, CommandEventArgs e)
    //{
    //    string discoveryUri = e.CommandArgument.ToString();
    //    var b = new UriBuilder(Request.Url) { Query = "" };
    //    var req = openid.CreateRequest(discoveryUri, b.Uri, b.Uri);

    //    var fetchRequest = new FetchRequest();
    //    fetchRequest.Attributes.AddRequired(WellKnownAttributes.Contact.Email);
    //    fetchRequest.Attributes.AddRequired(WellKnownAttributes.Name.First);
    //    fetchRequest.Attributes.AddRequired(WellKnownAttributes.Name.Last);
    //    fetchRequest.Attributes.AddRequired(WellKnownAttributes.Person.Gender);
    //    fetchRequest.Attributes.AddRequired(WellKnownAttributes.Contact.Phone.Mobile);
    //    fetchRequest.Attributes.AddRequired(WellKnownAttributes.BirthDate.WholeBirthDate);
    //    req.AddExtension(fetchRequest);
    //    req.RedirectToProvider();

    //}
    //private static string GetFullname(string first, string last)
    //{
    //    var _first = first ?? "";
    //    var _last = last ?? "";

    //    if (string.IsNullOrEmpty(_first) || string.IsNullOrEmpty(_last))
    //        return "";

    //    return _first + " " + _last;
    //}
    //public void autologin(string strusername, string strfname, string strlname)
    //{
    //    CheckBox Persist = new CheckBox();
    //    Persist.Checked = true;
    //    bool admin_flag = false;

    //    MembershipUser currentUser = Membership.GetUser(strusername);
    //    // bool flag1 = Roles.IsUserInRole(strusername, "Customer");
    //    if (Membership.GetUser(strusername) != null)
    //    {
    //        string strpassword = Convert.ToString(currentUser.GetPassword());
    //        admin_flag = Membership.ValidateUser(strusername, strpassword);

    //        if (admin_flag == true)
    //        {
    //            AuthenticateEventArgs w = new AuthenticateEventArgs();
    //            w.Authenticated = true;
    //            FormsAuthentication.RedirectFromLoginPage(strusername, Persist.Checked);
    //        }
    //        else
    //        {

    //            Response.Redirect(ReturnUrl("sitepath") + "procs/editprofile.aspx");
    //        }
    //    }
    //    else
    //    {
    //        autoregister(strusername, strfname, strlname);
    //        AuthenticateEventArgs w = new AuthenticateEventArgs();
    //        w.Authenticated = true;
    //        FormsAuthentication.RedirectFromLoginPage(strusername, Persist.Checked);

    //    }

    //}
    //public void autoregister(string strname, string strfname, string strlname)
    //{

    //    CreateUserWizard cuw = new CreateUserWizard();
    //    cuw.Email = strname;

    //    bool flagrole = Roles.IsUserInRole(strname, "Customer");

    //    if (flagrole == false)
    //    {
    //        Roles.AddUserToRole(strname, "Customer");
    //    }
    //    ProfileCommon p = (ProfileCommon)ProfileCommon.Create(strname, true);
    //    p.FirstName = strfname;
    //    p.LastName = strlname;
    //    p.Address1 = "";
    //    //p.title = strtitle;
    //    DataTable dt = new DataTable();
       
    //    p.City = "";
    //    p.State = "";
    //    p.Country = "India";
    //    p.countrycode = "+91";
    //    //}
    //    p.LandPhone = "";
    //    p.MobileNo = "";
    //    p.PinCode = "";
    //    p.addsource = "";
    //    p.Mailings = false;
    //    p.AssignedPoints = "200";
    //    p.Save();

    //    string strpass = GenerateRandomCode();
    //    MembershipUser newuser = Membership.CreateUser(strname, strpass, strname);

    //    classaddress.createaddress1(strname, p.FirstName, p.LastName, strname, p.Address1, "", "", p.PinCode, "", p.Country, p.LandPhone, p.MobileNo, 'Y', 'Y', "", "", p.countrycode, "", "", "");

    //    assign_points(strname);
    //    //sendmail(strfname, strtitle, strlname, strname, strpass);


    //}
    //public void assign_points(string username)
    //{

    //    int strbonus = 50;
    //    bool flag = classaddress.createmypassbook(username, "default", strbonus, "Registration Points", "C");

    //}
    //private string GenerateRandomCode()
    //{
    //    string s = "";
    //    for (int i = 0; i < 6; i++)
    //        s = String.Concat(s, this.random.Next(10).ToString());
    //    return s;
    //}
    //public void sendmail(string strFname, string title, string strLname, string strusername, string strPassword)
    //{
    //    int userid = 0;
    //    DataTable dtuser = classaddress.getindexidbyusername(strusername);
    //    if (dtuser.Rows.Count > 0)
    //    {
    //        userid = Convert.ToInt32(dtuser.Rows[0]["indexid"].ToString());
    //    }

    //    string frommail = ConfigurationManager.AppSettings["adminmail"].ToString();
    //    MailAddress SendFrom = new MailAddress(frommail);
    //    string mailto = strusername;

    //    string subject = strFname + " " + strLname + "(Member Id: " + userid + ")" + " Register Successful on Intranet.com";
    //    string body = "";
    //    body += "<!DOCTYPE HTML PUBLIC ' -//W3C//DTD HTML 4.01 Transitional//EN'>";
    //    body += "<html>";
    //    body += "<head>";

    //    body += "</html>";
    //    body += "<body>";
    //    body += "<table>";
    //    body += "<Font face='Arial' size='2' color='#333333'>";
    //    body += "<b>Dear " + strFname + " " + strLname + ",</b>";
    //    body += "<br><br>";
    //    // Utilities.SendMail(frommail, mailto, subject, body);
    //}
    #endregion Google plus login
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
                    strAttrValue = ReturnUrl("sitepathmain") + "user/" + user.Rows[0]["indexid"].ToString();
                }
                
            }
            return strAttrValue;
        }
        catch (Exception ex)
        {
            return strAttrValue;
        }
    }
    protected void lnkuser_Click(object sender, EventArgs e)
    {
        var btn =  (LinkButton)sender;
        var item = (RepeaterItem)btn.NamingContainer;
        var ddl =  (Label) item.FindControl("follow");
        bool iserror = false;
        iserror = classreviews.updatenotification(Page.User.Identity.Name, ddl.Text.ToString());
        if (iserror == true)
        {
            DataTable user = classreviews.getuseridbyemail(ddl.Text.ToString());
            if (user.Rows.Count > 0)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "user/" + user.Rows[0]["indexid"].ToString());
            }

        }
    }
}
