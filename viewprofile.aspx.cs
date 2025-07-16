using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class followers : System.Web.UI.Page
{
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
     {
         #region backbutton
         string urlName = "";
         if (Request.UrlReferrer != null)
         {
           urlName=  Request.UrlReferrer.ToString();
         }
            if (urlName.Contains("contacts"))
            {
                gobackbtn.HRef = ReturnUrl("sitepathmain") + "contacts.aspx";
                gobackbtn.InnerText = "Contacts";
            }
            else if (urlName.Contains("KeyPersonnel"))
            {
                gobackbtn.HRef = ReturnUrl("sitepathmain") + "KeyPersonnel.aspx";
                gobackbtn.InnerText = "Key Personnel";
            }
            else
            {
                gobackbtn.HRef = ReturnUrl("sitepathmain") + "default.aspx";
                gobackbtn.InnerText = "Home";
            }
       #endregion

     }
}