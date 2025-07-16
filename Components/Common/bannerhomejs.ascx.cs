using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Components_Common_bannerhomejs : System.Web.UI.UserControl
{
    private void PopulateControl()
    {
        try
        {
            Control userbannerjspanel = LoadControl(String.Format("../../Themes/{0}/LayoutControls/bannerjs.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
        userbannerjspanel.ID = "m_uxbannerjspanel";
        uxbannerjspanel.Controls.Add(userbannerjspanel);

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