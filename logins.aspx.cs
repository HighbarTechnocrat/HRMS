using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class logins : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string user = new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent()).Identity.Name;
        string Name = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        Response.Write("Username := " + user + "<br>" + Name);
		
    }
}