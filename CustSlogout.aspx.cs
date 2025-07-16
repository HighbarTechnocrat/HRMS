using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;

public partial class CustSlogout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.Title = ConfigurationManager.AppSettings["PageTitle"];
            Session.Abandon();
            Session.Clear();
            FormsAuthentication.SignOut();
            Response.Redirect("~/cust_Login.aspx");
        } 
    }
}