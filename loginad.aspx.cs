using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using FormsAuth;
using System.Collections;
using System.DirectoryServices;
public partial class login : System.Web.UI.Page
{
    static string LoginID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CheckCookie();
        }
        catch (Exception)
        {

        }
    }
    protected void Login_Click(Object sender, EventArgs e)
    {
        CreateCookie();
    }
    public void checklogin()
    {
        txtDomain.Text="highbartech.com";
			txtUsername.Text="user1";
			txtPassword.Text="pass@1234";
            String adPath = "highbartech.com";
        LdapAuthentication adAuth = new LdapAuthentication(adPath);
        try
        {
			
            if (true == adAuth.IsAuthenticated(txtDomain.Text, txtUsername.Text, txtPassword.Text))
            {
				//Response.Write("ok");
                String groups = adAuth.GetGroups();

                //Create the ticket, and add the groups.
                bool isCookiePersistent = chkPersist.Checked;
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, txtUsername.Text, DateTime.Now, DateTime.Now.AddMinutes(60), isCookiePersistent, groups);

                //Encrypt the ticket.
                String encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                //Create a cookie, and then add the encrypted ticket to the cookie as data.
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                if (true == isCookiePersistent)
                    authCookie.Expires = authTicket.Expiration;

                //Add the cookie to the outgoing cookies collection.
                Response.Cookies.Add(authCookie);

                //You can redirect now.
                Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUsername.Text, false));
            }
            else
            {
                errorLabel.Text = "Authentication did not succeed. Check user name and password.";
            }
        }
        catch (Exception ex)
        {
            errorLabel.Text = "Error authenticating. " + ex.Message;
        }
    }

    public void CheckCookie()
    {
        if (Request.Cookies["LDAPAUTH"] != null)
        {
            HttpCookie cookie = Request.Cookies["LDAPAUTH"];
            HttpCookie decodedCookie = CookieSecurityProvider.DecodeCookie(cookie);
            txtDomain.Text = decodedCookie["LDAPDOM"];
            txtUsername.Text = decodedCookie["LDAPUSER"];
            txtPassword.Text = decodedCookie["LDAPPASS"];
            ForceLogin();
        }
    }
    public void CreateCookie()
    {
        HttpCookie cookie = new HttpCookie("LDAPAUTH");
        cookie["LDAPDOM"] = txtDomain.Text;
        cookie["LDAPUSER"] = txtUsername.Text;
        cookie["LDAPPASS"] = txtPassword.Text;
        cookie.Expires = DateTime.Now.AddYears(1);
        cookie = CookieSecurityProvider.EncodeCookie(cookie);
        Response.Cookies.Add(cookie);
        ForceLogin();
    }

    public void ForceLogin()
    {
        CheckBox Persist = new CheckBox();
        Persist.Checked = true;
        AuthenticateEventArgs w = new AuthenticateEventArgs();
        w.Authenticated = true;
        FormsAuthentication.RedirectFromLoginPage("creativetest@gmail.com", Persist.Checked);
        Response.Redirect(ConfigurationManager.AppSettings["sitepathmain"]);
    }
    public void autologin(string strusername, string strfname, string strlname)
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

                AuthenticateEventArgs w = new AuthenticateEventArgs();
                w.Authenticated = true;
                FormsAuthentication.RedirectFromLoginPage(strusername, Persist.Checked);
                if (Session["referurl2"] != null)
                {
                    string str = Session["referurl2"].ToString();
                    // Response.Write(Session["referurl2"].ToString());
                    //  Response.End();
                    Response.Redirect("http://www.Intranet.com" + Session["referurl2"].ToString());
                }
            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["sitepath"] + "procs/editprofile");
            }
        }
        else
        {
            autoregister(strusername, strfname, strlname);
            AuthenticateEventArgs w = new AuthenticateEventArgs();
            w.Authenticated = true;
            FormsAuthentication.RedirectFromLoginPage(strusername, Persist.Checked);
            if (Session["referurl2"] != null)
            {
                string str = Session["referurl2"].ToString();
                // Response.Write(Session["referurl2"].ToString());
                //  Response.End();
                Response.Redirect("http://www.Intranet.com" + Session["referurl2"].ToString());
            }
        }

    }
    public void autoregister(string strname, string strfname, string strlname)
    {

        try
        {
            CreateUserWizard cuw = new CreateUserWizard();
            cuw.Email = strname;

            bool flagrole = Roles.IsUserInRole(strname, "Customer Service Representative");

            if (flagrole == false)
            {
                Roles.AddUserToRole(strname, "Customer Service Representative");
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
            string strpass = "csiauser";
            MembershipUser newuser = Membership.CreateUser(strname, strpass, strname);
            bool bln = classaddress.createFBAddress(strname, p.FirstName, p.LastName, p.Address1);
            if (Session["promoid"] != null)
            {
                DataTable dt_scrach = new DataTable();
                dt_scrach = classaddress.getscrach(Session["promoid"].ToString());
                string status = "A";
                if (dt_scrach.Rows.Count > 0)
                {
                    classaddress.createscrach(Session["promoid"].ToString(), "", cuw.Email, status);
                }
            }
            //removesession();
            //sendmail(strfname, strtitle, strlname, strname, strpass);
            //assign_points(strname);
        }
        catch (Exception ex)
        {
            string str = ex.Message.ToString();
            if (str.Contains("email address already in use"))
            {
                CheckBox Persist = new CheckBox();
                Persist.Checked = true;
                AuthenticateEventArgs w = new AuthenticateEventArgs();
                w.Authenticated = true;
                FormsAuthentication.RedirectFromLoginPage(strname, Persist.Checked);
            }
        }
    }
}