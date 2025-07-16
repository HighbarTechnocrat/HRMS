using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Collections;
using System.DirectoryServices;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;


/// <summary>
/// Summary description for LdapAuthentication_hbt
/// </summary>
public class LdapAuthentication_hbt
{
    private String _path;
    private String _filterAttribute;

    public LdapAuthentication_hbt(String path)
	{
        _path = path;        
	}
    //public bool IsAuthenticated(String domain, String username, String pwd)
      public String IsAuthenticated(String domain, String username, String pwd)
        {
            String domainAndUsername = domain + @"\" + "ashok..wani";
            username = "ashok..wani";

            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, "$Sanjay963");
            try
            {	//Bind to the native AdsObject to force authentication.			
                Object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);
			   HttpContext.Current.Response.Write("R1 1 " + Convert.ToString(username));
			   HttpContext.Current.Response.End();
                search.Filter = "(SAMAccountName=" + username + ")";
                //search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("mail");  // e-mail addressead
                SearchResult result = search.FindOne();
				HttpContext.Current.Response.Write("R1 " + Convert.ToString(username));
			   HttpContext.Current.Response.End();

                if (null == result)
                {
                    return "false";
                }

                //Update the new path to the user in the directory.
                _path = result.Path;
                //_filterAttribute = (String)result.Properties["cn"][0];
                _filterAttribute = (String)result.Properties["mail"][0];
                return  _filterAttribute;
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. " + ex.Message);
            }

            return "true";
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