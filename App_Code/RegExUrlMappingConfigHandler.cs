using System;
using System.Data;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for RegExUrlMappingConfigHandler
/// </summary>
public class RegExUrlMappingConfigHandler : IConfigurationSectionHandler
{
	public RegExUrlMappingConfigHandler()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    XmlNode _Section;

   public object Create(object parent, object configContext, System.Xml.XmlNode section) 
   { 
     _Section = section; 
     return this; 
   } 

   internal bool Enabled() 
   { 
     if (_Section.Attributes["enabled"].Value.ToLower() == "true")
     { 
        return true; 
     } 
     else 
     { 
        return false; 
     } 
   } 

   internal string MappedUrl(string url) 
   { 
        Regex oReg;
        foreach (XmlNode x in _Section.ChildNodes)
        { 
            oReg = new Regex(x.Attributes["url"].Value.ToLower()); 
            if (oReg.Match(url).Success)
            { 
                return oReg.Replace(url, x.Attributes["mappedUrl"].Value.ToLower()); 
            } 
        } 
        return "";           
   }    

 } 


