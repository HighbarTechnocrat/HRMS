using System;
using System.Text;
using System.Collections;
using System.DirectoryServices;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

namespace FormsAuth
{
    public class LdapAuthentication
    {
        private String _path;
        private String _filterAttribute;
        public static string email = "",fname="",lname="",mobile="",telephone="",streetaddress="";
        public LdapAuthentication(String path)
        {
            _path = path;
        }

        public string IsAuthenticated(String domain, String username, String pwd)
        {
            String domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);

            try
            {	//Bind to the native AdsObject to force authentication.			
                Object obj = entry.NativeObject;
                //Object email = entry.Properties;

                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("sn");
                search.PropertiesToLoad.Add("mail");
                search.PropertiesToLoad.Add("mobile");
                search.PropertiesToLoad.Add("telephoneNumber");
                search.PropertiesToLoad.Add("name");
                search.PropertiesToLoad.Add("postalCode");
                search.PropertiesToLoad.Add("co");
                search.PropertiesToLoad.Add("st");
                search.PropertiesToLoad.Add("I");
                search.PropertiesToLoad.Add("streetAddress");
                SearchResult result = search.FindOne();

                if (null == result)
                {
                    return "";
                }

                //Update the new path to the user in the directory.
                _path = result.Path;
                _filterAttribute = (String)result.Properties["cn"][0];
                email=(String)result.Properties["mail"][0];
                string name = (String)result.Properties["name"][0];
                string[] str = name.Split(' ');
                fname = str[0];
                lname = str[1];
                mobile = (String)result.Properties["mobile"][0];
                telephone = (String)result.Properties["telephoneNumber"][0];
                streetaddress = (String)result.Properties["streetAddress"][0];
                autologin(email, fname, lname);
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. " + ex.Message);
            }

            return email;
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
                    //if (Session["referurl2"] != null)
                    //{
                    //    string str = Session["referurl2"].ToString();
                    //    // Response.Write(Session["referurl2"].ToString());
                    //    //  Response.End();
                    //    Response.Redirect("http://www.Intranet.com" + Session["referurl2"].ToString());
                    //}
                }
                else
                {
                   // Response.Redirect(ConfigurationManager.AppSettings["sitepath"] + "procs/editprofile");
                }
            }
            else
            {
                autoregister(strusername, strfname, strlname);
                AuthenticateEventArgs w = new AuthenticateEventArgs();
                w.Authenticated = true;
                //FormsAuthentication.RedirectFromLoginPage(strusername, Persist.Checked);
                //if (Session["referurl2"] != null)
                //{
                //    string str = Session["referurl2"].ToString();
                //    // Response.Write(Session["referurl2"].ToString());
                //    //  Response.End();
                //    Response.Redirect("http://www.Intranet.com" + Session["referurl2"].ToString());
                //}
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
                string strpass = "csia@123";
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
        public String GetGroups()
        {
            DirectorySearcher search = new DirectorySearcher(_path);
            search.Filter = "(cn=" + _filterAttribute + ")";
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupNames = new StringBuilder();

            try
            {
                SearchResult result = search.FindOne();

                int propertyCount = result.Properties["memberOf"].Count;

                String dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    dn = (String)result.Properties["memberOf"][propertyCounter];

                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }

                    groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                    groupNames.Append("|");

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error obtaining group names. " + ex.Message);
            }
            return groupNames.ToString();
        }
    }
}