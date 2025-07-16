using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Xml;
using System.Text;
using System.IO;
using System.Data.Common;
using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;

public partial class humanresource_policies : System.Web.UI.Page
{
    private int projectid = 0, ID;
    public static string searchtype = "";
    //private int projectcatid ;
    public static string projectcatid = "";
    public static string projectstatusid = "";
    public static string projecttitle = "";
    public static string projectcatname = "";
    //public static string all = "";
    //public static string all = "";
    //public static string all = "";
    //public static string all = "";
    public static int PageSize = 10, PageIndex, RecordCount;
    public static int pgi;
    public static int pageCount;
    public static double dblmainpg;
    public static int maxpgcount;
    public static int maxpage = 5;
    public static int maxpg = 1;
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
   

    //sagar try below code for project search button 1dec 2017
   
   

    
          

            //DataSet dt = classproject.projectdetails1( PageIndex, PageSize, out RecordCount);
        
         
        

   
    public string getprojectURL(object projectid)
    {
        string strurl = "";
        try
        {
            strurl = ReturnUrl("sitepathmain") + "projectdetail.aspx?projectid=" + UrlRewritingVM.Encrypt(projectid.ToString());
            return strurl;

        }
        catch (Exception ex)
        {
            return strurl;
        }
    }


  

}



