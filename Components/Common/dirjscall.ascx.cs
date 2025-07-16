using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Components_Common_dirjscall : System.Web.UI.UserControl
{
    private void PopulateControl()
    {
        try
        {
            Control userjspanel = LoadControl(String.Format("../../Themes/{0}/LayoutControls/dirjs.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            userjspanel.ID = "m_uxjspanel";
            uxjspanel.Controls.Add(userjspanel);

            Control usercsspanel = LoadControl(String.Format("../../Themes/{0}/LayoutControls/defaultcss.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            usercsspanel.ID = "m_uxcsspanel";
            uxcsspanel.Controls.Add(usercsspanel);
        }
        catch (Exception ex)
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
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
}