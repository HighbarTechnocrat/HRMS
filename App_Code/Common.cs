using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using System.Web.Security;
using FormsAuth;
using System.Web.Hosting;

namespace creative
{
    public class Common
    {
        public Common()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region READ WRITE XML
        public void Write_XML(DataSet DS, string FileName, string FilePath)
        {
            StreamWriter XMLFile = new StreamWriter(FilePath + FileName, false);
            DS.WriteXml(XMLFile);
            XMLFile.Close();
        }
        public DataSet Read_XML(string FileName, string FilePath)
        {
            DataSet DS = new DataSet();
            DS.ReadXml(FilePath + FileName);
            return DS;
        }
        public bool fillcombo(DropDownList dropdowname, DataTable m_dt, string f_text, string f_value, string f_t_text)
        {
            dropdowname.DataSource = m_dt;
            //dropdowname.Items.Clear();
            dropdowname.DataTextField = f_text;
            dropdowname.DataValueField = f_value;
            dropdowname.DataBind();
            ListItem item = new ListItem(f_t_text, "0");
            dropdowname.Items.Insert(0, item);
            return true;
        }
        #endregion

        public string getADUserDetaildByName_Portal(string name)
        {
            using (HostingEnvironment.Impersonate())
            {
                //// Commented by Highbartech on 21-05-2020
                var attributeName = "sAMAccountName";
                //var attributeName = "mailNickname";
                var searchString = name;
                string[] str = name.Split('.');
                string[] str1;
                string fname = "";
                string lname = "";

                int len = str.GetUpperBound(0);
                if (len > 0)
                {
                    fname = str[0];
                    lname = str[1];
                }
                else
                {
                    fname = str[0];
                }
                if (Convert.ToString(str[1]) != "")
                {
                    str1 = str[1].Split('@');
                    len = str1.GetUpperBound(0);
                    if (len > 0)
                    {
                        lname = str1[0];
                    }
                    else
                    {
                        lname = "";
                    }
                }
                else
                {
                    lname = "";
                }
                string email = name.ToString();

                if (name.ToString() != "")
                {
                    autologin(name, fname, lname);
                }
                else
                {
                    autoregister(name, fname, lname);
                }
                //// Commented by Highbartech on 21-05-2020
                return email;
            }

        }
        public string getADUserDetaildByName(string name)
        {
            using (HostingEnvironment.Impersonate())
            {
                //// Commented by Highbartech on 21-05-2020
                ////var attributeName = "sAMAccountName";
                //////var attributeName = "mailNickname";
                ////var searchString = name;
                ////var ent = new DirectoryEntry("LDAP://172.16.0.213/DC=highbartech,DC=com");
                ////DirectorySearcher mySearcher = new DirectorySearcher(ent);
                ////mySearcher.PropertiesToLoad.Add("cn");
                ////mySearcher.PropertiesToLoad.Add("sn");
                ////mySearcher.PropertiesToLoad.Add("mail");
                ////mySearcher.PropertiesToLoad.Add("mobile");
                ////mySearcher.PropertiesToLoad.Add("telephoneNumber");
                ////mySearcher.PropertiesToLoad.Add("name");
                ////mySearcher.PropertiesToLoad.Add("postalCode");
                ////mySearcher.PropertiesToLoad.Add("co");
                ////mySearcher.PropertiesToLoad.Add("st");
                ////mySearcher.PropertiesToLoad.Add("I");
                ////mySearcher.PropertiesToLoad.Add("streetAddress");
                //////mySearcher.PropertiesToLoad.Add("Gender");
                ////mySearcher.Filter = string.Format("(&(objectcategory=user)({0}={1}))", attributeName, searchString);

                ////SearchResult userResult = mySearcher.FindOne();

                ////string email = (String)userResult.Properties["mail"][0];
                ////string aDname = (String)userResult.Properties["name"][0];
                ////string[] str = aDname.Split(' ');
                ////string fname = "";
                ////string lname = "";

                ////int len = str.GetUpperBound(0);
                ////if (len > 0)
                ////{
                ////    fname = str[0];
                ////    lname = str[1];
                ////}
                ////else
                ////{
                ////    fname = str[0];
                ////}
                //////if (Convert.ToString(str[1]) != "")
                //////{
                //////    lname = str[1];
                //////}
                //////else
                //////{
                //////    lname = "";
                //////}
					
                ////string mobile = "";
                ////if (userResult.Properties["mobile"].Count > 0)
                ////{
                ////    mobile = (String)userResult.Properties["mobile"][0];
                ////    mobile = mobile.Replace(" ", "");
                ////}

                ////string telephone = "";
                ////if (userResult.Properties["telephoneNumber"].Count > 0)
                ////{
                ////    telephone = (String)userResult.Properties["telephoneNumber"][0];
                ////    telephone = telephone.Replace(" ", "");
                ////}

                ////string streetaddress = "";
                ////if (userResult.Properties["streetAddress"].Count > 0)
                ////    streetaddress = (String)userResult.Properties["streetAddress"][0];
                ////// string gender = (String)userResult.Properties["Gender"][0];

                //////if (Membership.GetUser(email) != null)
                //////{
                //// //   autologin(email, fname, lname);
                //////}
                //////else
                //////{
                //////    autoregister(email, fname, lname);
                //////}
                //// Commented by Highbartech on 21-05-2020
                string email = "ashok..wani@highbartech.com";
                return email;
            }

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
                FormsAuthentication.RedirectFromLoginPage(strusername, Persist.Checked);
            }

        }
        public void autoregister(string strusername, string strfname, string strlname)
        {
            Membership.CreateUser(strusername, "hccuser2017", strusername);
	    bool flagrole = Roles.IsUserInRole(strusername, "User");
            if (flagrole == false)
            {
                Roles.AddUserToRole(strusername, "User");
            }
          //  bool bln = classaddress.createFBAddress(strusername, strfname, strlname, "");
        }
    }
}
