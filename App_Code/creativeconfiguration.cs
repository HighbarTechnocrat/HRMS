using System;
using System.Configuration;

/// <summary>
/// Repository for Global Hind configuration settings
/// </summary>
public static class creativeconfiguration
{
  // Caches the connection string
  private readonly static string dbConnectionString;
  // Caches the data provider name 
  private readonly static string dbProviderName;
  // Store the name of your shop
  private readonly static string sitepath;
  private readonly static string sitepathmain;

  private readonly static string siteName;
  private readonly static string adminmailemail;

  private readonly static string sitepathadmin;
  // Initialize various properties in the constructor
    static creativeconfiguration()
    {
   
    dbConnectionString = ConfigurationManager.ConnectionStrings["GlobalConnection"].ConnectionString;
    dbProviderName = ConfigurationManager.ConnectionStrings["GlobalConnection"].ProviderName;
   
    sitepath = UrlRewritingVM.ChangeURL("sitepath");
    sitepathmain = UrlRewritingVM.ChangeURL("sitepathmain");

    siteName = ConfigurationManager.AppSettings["SiteName"];
    adminmailemail = ConfigurationManager.AppSettings["adminmail"];

    sitepathadmin = ConfigurationManager.AppSettings ["adminsitepath"];
   
    }

 
  public static string DbConnectionString
  {
    get
    {
      return dbConnectionString;
    }
  }

  // Returns the data provider name
  public static string DbProviderName
  {
    get
    {
      return dbProviderName;
    }
  }
  
  public static bool EnableUrlRewriting
  {
    get
    {
        return bool.Parse(ConfigurationManager.AppSettings["EnableUrlRewriting"]);
    }
 }
  
    public static string SitePath
    {
        get
        {
            return sitepath;
        }
    }

    public static string SitePathMain
    {
        get
        {
            return sitepathmain;
        }
    }

    public static string adminemail
    {
        get
        {
            return adminmailemail;
        }
    }

    public static string SiteName
    {
        get
        {
            return siteName;
        }
    }
    public static string MailServer
    {
        get
        {
            return ConfigurationManager.AppSettings["MailServer"];
        }
    }

    public static string SitepathAdmin
        {
        get
            {
            return sitepathadmin;
            }
        }
}
