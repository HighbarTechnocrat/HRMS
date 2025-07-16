using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;

/// <summary>
/// Summary description for commonclass
/// </summary>
public class commonclass
{
	public commonclass()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string GetSafeSearchString(string inputstr)
    {
        string newStr = "";
        inputstr = inputstr.Replace("~", newStr);
        inputstr = inputstr.Replace("`", newStr);
        inputstr = inputstr.Replace("<", newStr);
        inputstr = inputstr.Replace(">", newStr);
        inputstr = inputstr.Replace("(", newStr);
        inputstr = inputstr.Replace(")", newStr);
        inputstr = inputstr.Replace("{", newStr);
        inputstr = inputstr.Replace("}", newStr);
        inputstr = inputstr.Replace("%", newStr);
        inputstr = inputstr.Replace("*", newStr);
        inputstr = inputstr.Replace("|", newStr);
        inputstr = inputstr.Replace("-", newStr);
        inputstr = inputstr.Replace("--", newStr);
        inputstr = inputstr.Replace("#", newStr);
        inputstr = inputstr.Replace("&", newStr);
        inputstr = inputstr.Replace("!", newStr);
        inputstr = inputstr.Replace("$", newStr);
        inputstr = inputstr.Replace("\"", newStr);
        inputstr = inputstr.Replace("\\", newStr);
        inputstr = inputstr.Replace("/", newStr);
        inputstr = inputstr.Replace("//", newStr);
        inputstr = inputstr.Replace("'", newStr);
        inputstr = inputstr.Replace("+", newStr);
        inputstr = inputstr.Replace("=", newStr);
        inputstr = inputstr.Replace(",", newStr);
        inputstr = inputstr.Replace("?", newStr);
        inputstr = inputstr.Replace("^", newStr);
        inputstr = inputstr.Replace(":", newStr);
        inputstr = inputstr.Replace(";", newStr);
        inputstr = inputstr.Replace("+", newStr);
        return inputstr;
    }

    public static string GetSafeDate(string inputstr)
    {
        string newStr = "";
        inputstr = inputstr.Replace("~", newStr);
        inputstr = inputstr.Replace("`", newStr);
        inputstr = inputstr.Replace("<", newStr);
        inputstr = inputstr.Replace(">", newStr);
        inputstr = inputstr.Replace("(", newStr);
        inputstr = inputstr.Replace(")", newStr);
        inputstr = inputstr.Replace("{", newStr);
        inputstr = inputstr.Replace("}", newStr);
        inputstr = inputstr.Replace("%", newStr);
        inputstr = inputstr.Replace("*", newStr);
        inputstr = inputstr.Replace("|", newStr);
        inputstr = inputstr.Replace("-", newStr);
        inputstr = inputstr.Replace("--", newStr);
        inputstr = inputstr.Replace("#", newStr);
        inputstr = inputstr.Replace("&", newStr);
        inputstr = inputstr.Replace("!", newStr);
        inputstr = inputstr.Replace("$", newStr);
        inputstr = inputstr.Replace("\"", newStr);
        inputstr = inputstr.Replace("\\", newStr);
        //inputstr = inputstr.Replace("/", newStr);
        inputstr = inputstr.Replace("@", newStr);
        inputstr = inputstr.Replace(".", newStr);
        inputstr = inputstr.Replace("//", newStr);
        inputstr = inputstr.Replace("'", newStr);
        inputstr = inputstr.Replace("+", newStr);
        inputstr = inputstr.Replace("=", newStr);
        inputstr = inputstr.Replace(",", newStr);
        inputstr = inputstr.Replace("?", newStr);
        inputstr = inputstr.Replace("^", newStr);
        inputstr = inputstr.Replace(":", newStr);
        inputstr = inputstr.Replace(";", newStr);
        inputstr = inputstr.Replace("+", newStr);
        return inputstr;
    }

    public static string GetSafeIDFromURL(string inputstr)
    {
        string newStr = "";
        inputstr = inputstr.Replace("~", newStr);
        inputstr = inputstr.Replace("`", newStr);
        inputstr = inputstr.Replace("<", newStr);
        inputstr = inputstr.Replace(">", newStr);
        inputstr = inputstr.Replace("(", newStr);
        inputstr = inputstr.Replace(")", newStr);
        inputstr = inputstr.Replace("{", newStr);
        inputstr = inputstr.Replace("}", newStr);
        inputstr = inputstr.Replace("%", newStr);
        inputstr = inputstr.Replace("*", newStr);
        inputstr = inputstr.Replace("|", newStr);
        inputstr = inputstr.Replace("-", newStr);
        inputstr = inputstr.Replace("--", newStr);
        inputstr = inputstr.Replace("#", newStr);
        inputstr = inputstr.Replace("&", newStr);
        inputstr = inputstr.Replace("!", newStr);
        inputstr = inputstr.Replace("$", newStr);
        inputstr = inputstr.Replace("\"", newStr);
        inputstr = inputstr.Replace("\\", newStr);
        inputstr = inputstr.Replace("/", newStr);
        inputstr = inputstr.Replace("@", newStr);
        inputstr = inputstr.Replace(".", newStr);
        inputstr = inputstr.Replace("//", newStr);
        inputstr = inputstr.Replace("'", newStr);
        inputstr = inputstr.Replace("+", newStr);
        inputstr = inputstr.Replace("=", newStr);
        inputstr = inputstr.Replace(",", newStr);
        inputstr = inputstr.Replace("?", newStr);
        inputstr = inputstr.Replace("^", newStr);
        inputstr = inputstr.Replace(":", newStr);
        inputstr = inputstr.Replace(";", newStr);
        inputstr = inputstr.Replace("+", newStr);
        inputstr = inputstr.Replace(" ", newStr);
        return inputstr;
    }

    public static string GetSafeFlagFromURL(string inputstr)
    {
        string newStr = "";
        inputstr = inputstr.Replace("~", newStr);
        inputstr = inputstr.Replace("`", newStr);
        inputstr = inputstr.Replace("<", newStr);
        inputstr = inputstr.Replace(">", newStr);
        inputstr = inputstr.Replace("(", newStr);
        inputstr = inputstr.Replace(")", newStr);
        inputstr = inputstr.Replace("{", newStr);
        inputstr = inputstr.Replace("}", newStr);
        inputstr = inputstr.Replace("%", newStr);
        inputstr = inputstr.Replace("*", newStr);
        inputstr = inputstr.Replace("|", newStr);
        inputstr = inputstr.Replace("-", newStr);
        inputstr = inputstr.Replace("--", newStr);
        inputstr = inputstr.Replace("#", newStr);
        inputstr = inputstr.Replace("&", newStr);
        inputstr = inputstr.Replace("!", newStr);
        inputstr = inputstr.Replace("$", newStr);
        inputstr = inputstr.Replace("\"", newStr);
        inputstr = inputstr.Replace("\\", newStr);
        inputstr = inputstr.Replace("/", newStr);
        inputstr = inputstr.Replace("@", newStr);
        inputstr = inputstr.Replace(".", newStr);
        inputstr = inputstr.Replace("//", newStr);
        inputstr = inputstr.Replace("'", newStr);
        inputstr = inputstr.Replace("+", newStr);
        inputstr = inputstr.Replace("=", newStr);
        inputstr = inputstr.Replace(",", newStr);
        inputstr = inputstr.Replace("?", newStr);
        inputstr = inputstr.Replace("^", newStr);
        inputstr = inputstr.Replace(":", newStr);
        inputstr = inputstr.Replace(";", newStr);
        inputstr = inputstr.Replace("+", newStr);
        inputstr = inputstr.Replace("0", newStr);
        inputstr = inputstr.Replace("1", newStr);
        inputstr = inputstr.Replace("2", newStr);
        inputstr = inputstr.Replace("3", newStr);
        inputstr = inputstr.Replace("4", newStr);
        inputstr = inputstr.Replace("5", newStr);
        inputstr = inputstr.Replace("6", newStr);
        inputstr = inputstr.Replace("7", newStr);
        inputstr = inputstr.Replace("8", newStr);
        inputstr = inputstr.Replace("9", newStr);
        inputstr = inputstr.Replace(" ", newStr);
        return inputstr;
    }

    public static string GetSafeURLForBrowsePostPage(string inputstr)
    {
        string returnurl = "";
        //Jayesh comment and added below line for search post by postname online 26oct2017
        //if (inputstr.Contains("intranet.highbartech.com/hrms/ps.aspx"))
        if (inputstr.Contains("HRMS_ADMIN.com.websols.shared-servers.com/ps.aspx"))
        //Jayesh comment and added below line for search post by postname online 26oct2017
        {
            returnurl = "true";
        }
        //else if (inputstr.Contains("HRMS_ADMIN/hrms/ps.aspx"))
        else if (inputstr.Contains("localhost/hrms/ps.aspx"))
        {
            returnurl = "true";
        }
        else
        {
            returnurl = "false";
        }
        return returnurl;
    }
    public static string GetSafeURLForAddPostPage(string inputstr)
    {
        string returnurl = "";
        if (inputstr.Contains("intranet.highbartech.com/hrms/addpost.aspx"))
        {
            returnurl = "true";
        }
        //else if (inputstr.Contains("HRMS_ADMIN/hrms/addpost.aspx"))
        else if (inputstr.Contains("localhost/hrms/addpost.aspx"))
        {
            returnurl = "true";
        }
        else
        {
            returnurl = "false";
        }
        return returnurl;
    }
}