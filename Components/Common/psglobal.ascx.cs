using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Components_Common_psglobal : System.Web.UI.UserControl
{
    private void PopulateControl()
    {
        try
        {
            Control userpsConttrol = LoadControl(String.Format("../../Themes/{0}/LayoutControls/ps.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            userpsConttrol.ID = "m_uxPsLayout";
            uxpsPanel.Controls.Add(userpsConttrol);

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