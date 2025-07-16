//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//public partial class Themes_SecondTheme_LayoutControls_logins : System.Web.UI.UserControl
//{
//    protected void Page_Load(object sender, EventArgs e)
//    {

//    }
//}



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
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using ASPSnippets.GoogleAPI;
using System.Security.Cryptography;
using System.Text;

public partial class Themes_SecondTheme_LayoutControls_login : System.Web.UI.UserControl
{
    public static string urlpath = "", action = "", mainpath = "";
    public static int i = 0;
    OpenIdRelyingParty openid = new OpenIdRelyingParty();
    private Random random = new Random();
    public string strur;
    public static int productId = 0;
    SP_Methods sp = new SP_Methods();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!Page.IsPostBack)
            {
                //Request.Cookies.Clear();
                if (Request.QueryString["internet"] != null && Request.QueryString["internet"] != "")
                {
                    if (Request.QueryString["internet"].ToString() == "true")
                    {
                        HttpCookie cookie = new HttpCookie("internet", "true");
                        Response.Cookies.Add(cookie);
                        urlpath = ConfigurationManager.AppSettings["ZS1"].ToString();
                        action = ConfigurationManager.AppSettings["ZA1"].ToString();
                        mainpath = ConfigurationManager.AppSettings["internetURL"].ToString();
                    }
                    else
                    {
                        HttpCookie cookie = new HttpCookie("internet", "false");
                        Response.Cookies.Add(cookie);
                        urlpath = ConfigurationManager.AppSettings["ZS2"].ToString();
                        action = ConfigurationManager.AppSettings["ZA2"].ToString();
                        mainpath = ConfigurationManager.AppSettings["intranetURL"].ToString();
                    }
                }
                else
                {
                    HttpCookie cookie = new HttpCookie("internet", "false");
                    Response.Cookies.Add(cookie);
                    urlpath = ConfigurationManager.AppSettings["ZS2"].ToString();
                    action = ConfigurationManager.AppSettings["ZA2"].ToString();
                    mainpath = ConfigurationManager.AppSettings["intranetURL"].ToString();
                }

                if (Request.QueryString["user"] != null && Request.QueryString["user"] != "")
                {
                    if (Request.QueryString["user"].ToString() == "Highbar")
                    {
                        HttpCookie usercookie = new HttpCookie("user", "Highbar");
                        Response.Cookies.Add(usercookie);
                        //HttpCookie cookie = new HttpCookie("internet", "false");
                        //Response.Cookies.Add(cookie);
                    }
                    else
                    {
                        HttpCookie usercookie = new HttpCookie("user", "");
                        Response.Cookies.Add(usercookie);
                        ////HttpCookie cookie = new HttpCookie("internet", "true");
                        ////Response.Cookies.Add(cookie);
                    }
                }
                else
                {
                    HttpCookie usercookie = new HttpCookie("user", "");
                    Response.Cookies.Add(usercookie);
                }
                UserName.Focus();
                //if (Page.User.Identity.IsAuthenticated)
                //{
                //    Response.Redirect(ReturnUrl("sitepathmain") + "default");
                //}
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
    public bool Is_valid()
    {
        bool m_valid = false;
        lblSuccess.Visible = false;
		 #region Set Sessions Variables
			string	username_N = Page.User.Identity.Name.ToString().Trim();
			 
				DataTable dtUserlogin_N = sp.getuserinfodetails(Convert.ToString(UserName.Text).Trim());
				  if (dtUserlogin_N.Rows.Count > 0)
				  {
					  if (Convert.ToString(dtUserlogin_N.Rows[0]["errmsg"]).Trim() == "")
					  {
						  Session["Empcode"] = dtUserlogin_N.Rows[0]["Emp_Code"].ToString();
						  Session["LoginEmpmail"] = Convert.ToString(UserName.Text).Trim();
						  Session["emp_loginName"] = dtUserlogin_N.Rows[0]["Emp_Name"].ToString();
						  //m_valid = true;
					  }

				  }
		 #endregion

         #region removed on 11.09.2018
            /*  MembershipUser user = Membership.GetUser(UserName.Text);
        if (user != null)
        {
            DataTable dtUserlogin = sp.getuserinfodetails(Convert.ToString(UserName.Text).Trim()  + "@highbartech.com");
            if (dtUserlogin.Rows.Count > 0)
            {
                if (Convert.ToString(dtUserlogin.Rows[0]["errmsg"]).Trim() == "")
                {
                    Session["Empcode"] = dtUserlogin.Rows[0]["Emp_Code"].ToString();
                    //Session["LoginEmpmail"] = Convert.ToString(UserName.Text).Trim();
                    Session["LoginEmpmail"] = dtUserlogin.Rows[0]["Emp_Emailaddress"].ToString();
                    Session["emp_loginName"] = dtUserlogin.Rows[0]["Emp_Name"].ToString();
                    //m_valid = true;
                }
                else
                {
                    lblerror.Text = Convert.ToString(dtUserlogin.Rows[0]["errmsg"]).Trim();
                    lblerror.Visible = true;
                    return false;
                }
            }
            //Response.Write("hello2" + user);
            // Response.End();
            if (user.IsLockedOut)
            {
                lblformsg.Visible = false;
                lblerror.Visible = true;
                FailureText.Visible = false;
                lblerror.Text = "Your account is locked. Please contact administrator.";
                string strindex = "";
                string fname = "";
                string lname = "";
                DataTable dtadd = classaddress.getuserinfodetails(UserName.Text);
                if (dtadd.Rows.Count > 0)
                {
                    strindex = dtadd.Rows[0]["indexid"].ToString();
                    fname = dtadd.Rows[0]["firstname"].ToString();
                    lname = dtadd.Rows[0]["lastname"].ToString();
                    //string body = Utilities.sendRegisterstatusEmailformat("L", "", UserName.Text, fname, lname, "", UserName.Text, Convert.ToInt32(strindex));
                }
            }
            else
            {
                lblerror.Visible = false;
                FailureText.Visible = false;
                lbluser.Visible = false;
                lblpwd.Visible = true;
                lblpwd.Text = "Password does not match";
                Password.Focus();
                lblformsg.Visible = false;
            }
        }
        else
        {

            string pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(UserName.Text.Trim(), pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            if (!match.Success)
            {

                lbluser.Visible = false;
                lbluser.Text = "<span class='donationerror'>Please enter a valid Email ID.</span>";
            }
            else
            {
                // Response.Write("hello3");
                FailureText.Visible = false;
                lbluser.Visible = true;
                lbluser.Text = "Email Id does not exist";
            }

            lblformsg.Visible = false;
            UserName.Focus();
        }

        if (UserName.Text == "")
        {
            lbluser.Visible = false;
        }

        if (Password.Text == "")
        {
            lblpwd.Visible = false;
        }  */
                  #endregion
        return m_valid;
    }
    protected void Login1_Authenticate(object sender, EventArgs e)
    {
        string txtusername = UserName.Text;
        string txtpassword = Password.Text;
        lblSuccess.Visible = false;
        CheckBox ch = new CheckBox();
        if (Is_valid() == false)
        {
            //string currenturl_N = Request.Url.AbsoluteUri;

            /*Commented by R1 for Active Directory Authentication
			classaddress.updatevisitcount(txtusername);
            FormsAuthentication.RedirectFromLoginPage(txtusername, false);
            Response.Redirect(ReturnUrl("sitepathmain"));*/

            if (Membership.ValidateUser(txtusername, txtpassword))
            {
                //Session["internet"] = "true";
                string currenturl = Request.Url.AbsoluteUri;

                classaddress.updatevisitcount(txtusername);
                FormsAuthentication.RedirectFromLoginPage(txtusername, false);
                Response.Redirect(ReturnUrl("sitepathmain"));
                ////  if (Request.Url.AbsoluteUri.Contains("192.168.0.172/hrms/"))
                //  if (Request.Url.AbsoluteUri.Contains("HRMS_ADMIN"))
                //  {
                //      HttpCookie cookie = new HttpCookie("internet", "false");
                //      Session["internet"] = "false";
                //      Response.Cookies.Add(cookie);
                //    //  Response.Redirect("http://192.168.0.172/hrms/default.aspx");
                //      Response.Redirect("http://localhost/hrms/default.aspx");
                //  }
                ////  else if (Request.Url.AbsoluteUri.Contains("192.168.0.172/HRMS_ADMIN/"))
                //  else if (Request.Url.AbsoluteUri.Contains("intranet.highbartech.com"))
                //  {
                //      HttpCookie cookie = new HttpCookie("internet", "true");
                //      Session["internet"] = "true";
                //      Response.Cookies.Add(cookie);
                //    //  Response.Redirect("http://192.168.0.172/HRMS_ADMIN/default.aspx");
                //      Response.Redirect("http://intranet.highbartech.com/default.aspx");
                //  }

                if ((Request.QueryString["ReturnUrl"]) != null)
                {
                    geturl();
                }
                else
                {
                    try
                    {
                        if (Session["referurl2"] != null)
                        {
                            if (Session["referurl2"].ToString() == "/url")
                            {
                                Response.Redirect(ReturnUrl("sitepathmain") + "default");
                            }
                            else if (Session["referurl2"].ToString().Contains("index"))
                            {
                                Response.Redirect(ReturnUrl("sitepathmain") + "default");
                            }
                            else
                            {
                                string str = Session["referurl2"].ToString();
                                Response.Redirect(ReturnUrl("sitepathmain") + Session["referurl2"].ToString());
                            }
                        }
                        else
                        {
                            Response.Redirect(ReturnUrl("sitepathmain") + "default");
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.WriteError(Session["referurl2"].ToString());
                    }

                }

            }
        }
    }
    public void getpremiumpackagestatus(string useremail)
    {

        DataTable dtordp = classpkg.getstatusorderdetailbyemaild(useremail);

        if (dtordp.Rows.Count > 0)
        {
            int orderid = Convert.ToInt32(dtordp.Rows[0]["orderid"]);

            bool strurlex =
               classpkgorder.packagevalidity_status(useremail, Convert.ToDecimal(orderid));

        }

    }
    public void getspecialpackagestatus(string useremail, decimal productid)
    {
        if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
        {

            productid = Convert.ToInt32(Request.QueryString["p"].ToString().Trim());

        }
        DataTable dtorsp = classpkg.getstatusspecialorderdetailbyemaild(useremail, Convert.ToDecimal(productid));

        if (dtorsp.Rows.Count > 0)
        {
            int orderid = Convert.ToInt32(dtorsp.Rows[0]["orderid"]);

            bool strurlex =
               classpkgorder.packagevalidity_status(useremail, Convert.ToDecimal(orderid));


        }

    }
    private void RedirectTo(string url)
    {
        string redirectURL = Page.ResolveClientUrl(url);
        string script = "window.location = '" + redirectURL + "';";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
    }
    protected void btnsubmit_onclick(object sender, EventArgs e)
    {

        MembershipUser username = Membership.GetUser(txt_forgetpwd.Text);
        lbluser.Visible = false;
        lblpwd.Visible = false;
        if (username != null)
        {
            if (username.IsLockedOut)
            {
                lblformsg.Visible = false;
                lblerror.Visible = true;
                lblerror.Text = "Your account is locked. Please contact administrator.";
                try
                {
                    string userName = Page.User.Identity.Name;
                    MembershipUser memberuser = Membership.GetUser(userName);

                    DataTable dtadd = classaddress.getuserinfodetails(userName);
                    string strindex = "";
                    string fname = "";
                    string lname = "";
                    if (dtadd.Rows.Count > 0)
                    {
                        strindex = dtadd.Rows[0]["indexid"].ToString();
                        fname = dtadd.Rows[0]["firstname"].ToString();
                        lname = dtadd.Rows[0]["lastname"].ToString();

                        string body = Utilities.sendRegisterstatusEmailformat("L", "", userName, fname, lname, "", userName, Convert.ToInt32(strindex));

                    }
                }
                catch (Exception ex)
                {
                }
            }
            else
            {


                string pass = username.GetPassword();
                DataTable dtadd = classaddress.getuserinfodetails(txt_forgetpwd.Text);
                string strindex = "";
                string fname = "";
                string lname = "";
                if (dtadd.Rows.Count > 0)
                {
                    strindex = dtadd.Rows[0]["indexid"].ToString();
                    fname = dtadd.Rows[0]["firstname"].ToString();
                    lname = dtadd.Rows[0]["lastname"].ToString();
                    string body = Utilities.sendRegisterstatusEmailformat("FP", "", txt_forgetpwd.Text.Trim(), fname, lname, pass, txt_forgetpwd.Text.Trim(), Convert.ToInt32(strindex));



                    FailureText.Visible = false;
                    lblerror.Visible = false;
                    txt_forgetpwd.Text = "";
                    lblformsg.Visible = true;
                    lblformsg.Text = "<font color='green'>Your password has been sent to your email id!</font>";
                    //divf.Attributes.Add("style", "display:none");
                }
            }


        }

        else
        {
            if (txt_forgetpwd.Text == "")
            {
                // divf.Attributes.Add("style","display:block");
                lblformsg.Visible = false;
            }


        }
    }
    public void geturl()
    {
        string m_productidurl = HttpUtility.UrlDecode(Request.QueryString["ReturnUrl"]);
        string txtusername = UserName.Text;
        string txtpassword = Password.Text;
        #region check usersubscription
        if ((Request.QueryString["p"]) != "" && (Request.QueryString["p"]) != null)
        {
            productId = Convert.ToInt32((Request.QueryString["p"].ToString()));
        }

        DataTable ds = classproduct.get_proc_ProductDescription_ProdFeature(productId);
        if (ds.Rows.Count > 0)
        {
            string strprdnm = ds.Rows[0]["productname"].ToString();
            string strflag = ds.Rows[0]["parentflag"].ToString();
            #region premium
            if (strflag == "Premium")
            {

                getpremiumpackagestatus(Convert.ToString(txtusername));

                DataTable dtpord = classpkg.getorderdetailbyemaild(Convert.ToString(txtusername), "P");

                if (dtpord.Rows.Count > 0)
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "watch.aspx?p=" + encryptPassword("p=" + productId) + "&em=" + encryptPassword("em=" + txtusername));
                }
                else
                {
                    Response.Redirect(Request.QueryString["ReturnUrl"]);
                }

            }
            #endregion

            #region special
            else if (strflag == "Special")
            {
                getspecialpackagestatus(Convert.ToString(txtusername), Convert.ToDecimal(productId));
                DataTable dtord = classpkg.getspecialorderdetailbyemaild(txtusername, "S", Convert.ToDecimal(productId));
                if (dtord.Rows.Count > 0)
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "watch.aspx?p=" + encryptPassword("p=" + productId) + "&em=" + encryptPassword("em=" + txtusername));
                }
                else
                {

                    string pid = "s=" + Convert.ToString(productId);
                    strur = UrlRewritingVM.getUrlRewritingInfo("", encryptPassword(pid), "s");
                    Response.Redirect(strur);

                }
            }
            else
            {
                if (Request.QueryString["ReturnUrl"].Contains("watch.aspx"))
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "watch.aspx?p=" + encryptPassword("p=" + productId) + "&em=" + encryptPassword("em=" + txtusername.ToString().Trim()));
                }
                else
                {
                    Response.Redirect(Request.QueryString["ReturnUrl"]);
                }



            }


            #endregion


        }

        #endregion

    }
    public static string ProcessUrl(string url)
    {
        string newUrl = url;
        newUrl = newUrl.ToLower();
        //replace spaces with hyphens(-)
        newUrl = newUrl.Replace(" ", "-");
        newUrl = newUrl.Replace("  ", "-");
        newUrl = newUrl.Replace("'", "");
        newUrl = newUrl.Replace("%20", "-");
        newUrl = newUrl.Replace("&", "and");
        newUrl = newUrl.Replace("!", "");
        newUrl = newUrl.Replace("@", "");
        newUrl = newUrl.Replace("#", "");
        newUrl = newUrl.Replace("$", "");
        newUrl = newUrl.Replace("%", "");
        newUrl = newUrl.Replace("^", "");
        newUrl = newUrl.Replace("*", "");
        newUrl = newUrl.Replace("(", "");
        newUrl = newUrl.Replace(")", "");
        newUrl = newUrl.Replace("{", "");
        newUrl = newUrl.Replace("}", "");
        newUrl = newUrl.Replace("[", "");
        newUrl = newUrl.Replace("]", "");
        newUrl = newUrl.Replace("<", "");
        newUrl = newUrl.Replace(">", "");
        newUrl = newUrl.Replace("_", "");
        newUrl = newUrl.Replace("|", "");
        newUrl = newUrl.Replace("~", "");
        newUrl = newUrl.Replace(". ", "");
        newUrl = newUrl.Replace("./", "/");
        newUrl = newUrl.Replace(" .", "");
        newUrl = newUrl.Replace("`", "");
        newUrl = newUrl.Replace("+", "-");
        newUrl = newUrl.Replace('"', ' ');
        newUrl = newUrl.Replace("%3d", "=");
        newUrl = newUrl.Replace("  ", " ");
        newUrl = newUrl.Replace("   ", " ");
        newUrl = newUrl.Replace(" ", "-");
        newUrl = newUrl.Replace("%2c", ",");
        newUrl = newUrl.Replace("%0A", "-");
        newUrl = newUrl.Replace("%0a", "-");
        newUrl = newUrl.Replace("\n", "-");
        newUrl = newUrl.Replace("\r", "-");
        newUrl = newUrl.Replace("---", "-");
        return newUrl;
    }
    #region Google plus login
    protected void googlelogin()
    {


        string streturnurl = string.Empty;
        GoogleConnect.ClientId = "548602790148-apt9249kqb20pjatqv5l0a7lh8jep19r.apps.googleusercontent.com";
        GoogleConnect.ClientSecret = "uwjaGTZaNrEoIGbPs5g5S9ku";

        if ((Request.QueryString["ReturnUrl"]) != null)
        {
            if (streturnurl.Contains("p="))
            {
                geturl();
            }
        }
        else
        {
            GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split('?')[0];
        }


        if (!string.IsNullOrEmpty(Request.QueryString["code"]))
        {

            string code = Request.QueryString["code"];
            string json = GoogleConnect.Fetch("me", code);

            GoogleProfile profile = new JavaScriptSerializer().Deserialize<GoogleProfile>(json);

            string emailid = string.Empty;
            string name = string.Empty;
            string birthdate = string.Empty;
            string phone = string.Empty;
            string gender = string.Empty;
            string fname = string.Empty;
            string lname = string.Empty;

            name = profile.DisplayName;
            emailid = profile.Emails.Find(email => email.Type == "account").Value;
            gender = profile.Gender;

            string[] delimiter = { " ", " " };
            string[] strtmfrom = name.Split(delimiter, StringSplitOptions.None);
            fname = strtmfrom[0].ToString();
            lname = strtmfrom[1].ToString();

            autologin(emailid, fname, lname, streturnurl);

        }
        if (Request.QueryString["error"] == "access_denied")
        {

        }


    }
    public class GoogleProfile
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public Image Image { get; set; }
        public List<Email> Emails { get; set; }
        public string Gender { get; set; }
        public string ObjectType { get; set; }
    }
    public class Email
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
    public class Image
    {
        public string Url { get; set; }
    }
    protected void Login(object sender, EventArgs e)
    {

        GoogleConnect.Authorize("profile", "email");
    }
    protected void getupdatecart()
    {
        DataSet ds_update = new DataSet();
        ds_update = clscartlist.get_update_ipaddress_cartlist(UserName.Text, getRemoteAddr());
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
    public void autologin(string strusername, string strfname, string strlname, string streturnurl)
    {
        CheckBox Persist = new CheckBox();
        Persist.Checked = true;
        bool admin_flag = false;

        MembershipUser currentUser = Membership.GetUser(strusername);

        if (Membership.GetUser(strusername) != null)
        {
            string strpassword = Convert.ToString(currentUser.GetPassword());
            admin_flag = Membership.ValidateUser(strusername, strpassword);

            if (admin_flag == true)
            {

                if (streturnurl.Contains("p="))
                {
                    string strUrl = streturnurl.Substring(streturnurl.LastIndexOf("=") + 1);
                    if (strUrl != "")
                    {
                        FormsAuthentication.RedirectFromLoginPage(strusername, Persist.Checked);
                        geturlgoogle(Convert.ToInt32(strUrl), strusername);

                    }
                    else
                    {

                        AuthenticateEventArgs w = new AuthenticateEventArgs();
                        w.Authenticated = true;
                        FormsAuthentication.RedirectFromLoginPage(strusername, Persist.Checked);
                    }
                }

                else
                {
                    AuthenticateEventArgs w = new AuthenticateEventArgs();
                    w.Authenticated = true;
                    FormsAuthentication.RedirectFromLoginPage(strusername, Persist.Checked);
                    if (Session["referurl2"] != null)
                    {
                        string str = Session["referurl2"].ToString();
                        // Response.Write(Session["referurl2"].ToString());
                        //  Response.End();
                        Response.Redirect(ReturnUrl("sitepathmain") + Session["referurl2"].ToString());
                    }
                }

            }
            else
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "procs/editprofile");
            }
            // GoogleConnect.Clear();
        }
        else
        {
            autoregister(strusername, strfname, strlname);
            AuthenticateEventArgs w = new AuthenticateEventArgs();
            w.Authenticated = true;
            FormsAuthentication.RedirectFromLoginPage(strusername, Persist.Checked);
            //GoogleConnect.Clear();
        }

    }
    public void geturlgoogle(int prd_id, string strusername)
    {

        //string m_productidurl = HttpUtility.UrlDecode(Request.QueryString["ReturnUrl"]);
        string txtusername = strusername;

        #region check usersubscription

        DataTable ds = classproduct.get_proc_ProductDescription_ProdFeature(prd_id);
        if (ds.Rows.Count > 0)
        {
            string strprdnm = ds.Rows[0]["productname"].ToString();
            string strflag = ds.Rows[0]["parentflag"].ToString();
            #region premium
            if (strflag == "Premium")
            {

                getpremiumpackagestatus(Convert.ToString(txtusername));

                DataTable dtpord = classpkg.getorderdetailbyemaild(Convert.ToString(txtusername), "P");

                if (dtpord.Rows.Count > 0)
                {



                    Response.Redirect(ReturnUrl("sitepathmain") + "watch.aspx?p=" + encryptPassword("p=" + Convert.ToString(prd_id)) + "&em=" + encryptPassword("em=" + txtusername));
                }
                else
                {

                    Response.Redirect("http://testim.com.iis1101.shared-servers.com/subscribe");
                }

            }
            #endregion

            #region special
            else if (strflag == "Special")
            {
                getspecialpackagestatus(Convert.ToString(txtusername), Convert.ToDecimal(prd_id));
                DataTable dtord = classpkg.getspecialorderdetailbyemaild(txtusername, "S", Convert.ToDecimal(prd_id));
                if (dtord.Rows.Count > 0)
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "watch.aspx?p=" + encryptPassword("p=" + Convert.ToString(prd_id)) + "&em=" + encryptPassword("em=" + txtusername));
                }
                else
                {

                    string pid = "s=" + Convert.ToString(prd_id);
                    string strur = UrlRewritingVM.getUrlRewritingInfo("", encryptPassword(Convert.ToString(pid)), "s");
                    Response.Redirect(strur);

                }
            }
            else
            {

                Response.Redirect(ReturnUrl("sitepathmain") + "watch.aspx?p=" + encryptPassword("p=" + Convert.ToString(prd_id)) + "&em=" + encryptPassword("em=" + txtusername.ToString().Trim()));

            }


            #endregion


        }

        #endregion

    }
    public void autoregister(string strname, string strfname, string strlname)
    {

        CreateUserWizard cuw = new CreateUserWizard();
        cuw.Email = strname;

        bool flagrole = Roles.IsUserInRole(strname, "User");

        if (flagrole == false)
        {
            Roles.AddUserToRole(strname, "User");
        }
        ProfileCommon p = (ProfileCommon)ProfileCommon.Create(strname, true);
        p.FirstName = strfname;
        p.LastName = strlname;
        p.Address1 = "";

        DataTable dt = new DataTable();

        p.City = "";
        p.State = "";
        p.Country = "";
        p.countrycode = "";

        p.LandPhone = "";
        p.MobileNo = "";
        p.PinCode = "";
        p.addsource = "";
        p.Mailings = false;

        p.AssignedPoints = "50";
        p.Save();

        string strpass = GenerateRandomCode();
        MembershipUser newuser = Membership.CreateUser(strname, strpass, strname);
        bool bln = classaddress.createFBAddress(strname, p.FirstName, p.LastName, p.Address1);
        //if (Session["promoid"] != null)
        //{
        //    DataTable dt_scrach = new DataTable();
        //    dt_scrach = classaddress.getscrach(Session["promoid"].ToString());
        //    string status = "A";
        //    if (dt_scrach.Rows.Count > 0)
        //    {
        //        classaddress.createscrach(Session["promoid"].ToString(), "", cuw.Email, status);
        //    }
        //}
        //classaddress.createaddress1(strname, p.FirstName, p.LastName, strname, p.Address1, "", "", p.PinCode, "", p.Country, p.LandPhone, p.MobileNo, 'Y', 'Y', "", "", p.countrycode, "", "", "");
        removesession();
        assign_points(strname);
        sendmail(strfname, "", strlname, strname, strpass);


    }
    public void removesession()
    {
        if (Session["promoid"] != null)
        {
            Session.Remove("promoid");
        }
    }
    public void assign_points(string username)
    {

        DataTable dtb = classpoint.getbonuspoint();
        DateTime dto = DateTime.Now;
        if (dtb.Rows.Count > 0)
        {

            int strbonus = Convert.ToInt32(dtb.Rows[0]["bonuspoint"]);
            bool flag = classpassbook.createmypassbook(username, "default", strbonus, "Registration Points", 0, Convert.ToString(dto), "C");


            bool flagpt = classpassbook.addassignpoints(username, strbonus, Convert.ToString(dto));
        }

    }
    private string GenerateRandomCode()
    {
        string s = "";
        for (int i = 0; i < 6; i++)
            s = String.Concat(s, this.random.Next(10).ToString());
        return s;
    }
    public void sendmail(string strFname, string title, string strLname, string strusername, string strPassword)
    {
        try
        {
            int userid = 0;
            DataTable dtuser = classaddress.getindexidbyusername(strusername);
            if (dtuser.Rows.Count > 0)
            {
                userid = Convert.ToInt32(dtuser.Rows[0]["indexid"].ToString());
            }
            string body = Utilities.sendRegisterstatusEmailformat("R", "", strusername, Convert.ToString(strFname), Convert.ToString(strLname), strPassword, strusername, (userid));

        }
        catch (Exception ex)
        {

        }

    }
    #endregion Google plus login
    public string encryptPassword(string strText)
    {
        return Encrypt(strText);
    }
    public byte[] ScrambleKey
    {
        set
        {
            byte[] key = value;
            if (null == key)
            {
                // Use existing key if non provided
                key = ScrambleKey;
            }
            Session["ScrambleKey"] = key;
        }
        get
        {
            byte[] key = (byte[])Session["ScrambleKey"];
            if (null == key)
            {
                RC2CryptoServiceProvider rc2 = new RC2CryptoServiceProvider();
                rc2.GenerateKey();
                key = rc2.Key;
                Session["ScrambleKey"] = key;
            }
            return key;
        }
    }
    // Initialization vector management for scrambling support
    public byte[] ScrambleIV
    {
        set
        {
            byte[] key = value;
            if (null == key)
            {
                key = ScrambleIV;
            }
            Session["ScrambleIV"] = key;
        }
        get
        {
            byte[] key = (byte[])Session["ScrambleIV"];
            if (null == key)
            {
                RC2CryptoServiceProvider rc2 = new RC2CryptoServiceProvider();
                rc2.GenerateIV();
                key = rc2.IV;
                Session["ScrambleIV"] = key;
            }
            return key;
        }
    }
    public string Encrypt(string message)
    {
        UTF8Encoding textConverter = new UTF8Encoding();
        RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();

        //Convert the data to a byte array.
        byte[] toEncrypt = textConverter.GetBytes(message);

        //Get an encryptor.
        ICryptoTransform encryptor = rc2CSP.CreateEncryptor(ScrambleKey, ScrambleIV);

        //Encrypt the data.
        MemoryStream msEncrypt = new MemoryStream();
        CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

        //Write all data to the crypto stream and flush it.
        // Encode length as first 4 bytes
        byte[] length = new byte[4];
        length[0] = (byte)(message.Length & 0xFF);
        length[1] = (byte)((message.Length >> 8) & 0xFF);
        length[2] = (byte)((message.Length >> 16) & 0xFF);
        length[3] = (byte)((message.Length >> 24) & 0xFF);
        csEncrypt.Write(length, 0, 4);
        csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
        csEncrypt.FlushFinalBlock();

        //Get encrypted array of bytes.
        byte[] encrypted = msEncrypt.ToArray();

        // Convert to Base64 string
        string b64 = Convert.ToBase64String(encrypted);

        // Protect against URLEncode/Decode problem
        string b64mod = b64.Replace('+', '@');

        // Return a URL encoded string
        return HttpUtility.UrlEncode(b64mod);
    }
}
