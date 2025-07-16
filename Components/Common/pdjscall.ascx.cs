using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Components_Common_pdjscall : System.Web.UI.UserControl
{
    private void PopulateControl()
    {
        try
        {
            Control userjspanel = LoadControl(String.Format("../../Themes/{0}/LayoutControls/pd/pdjs.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            userjspanel.ID = "m_uxjspanel";
            uxjspanel.Controls.Add(userjspanel);

        }
        catch ( Exception ex )
        {
	        ErrorLog.WriteError(ex.ToString());
        }
    }
    protected void Page_init(object sender, EventArgs e)
    {
        try
        {
        
            PopulateControl();

        }
        catch ( Exception ex )
        {
	        ErrorLog.WriteError(ex.ToString());
        }
    }
}