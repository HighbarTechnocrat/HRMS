using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Components_Common_loginjscall : System.Web.UI.UserControl
{
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

    private void PopulateControl()
    {
        try
        {
            Control userloginjspanel = LoadControl(String.Format("../../Themes/{0}/LayoutControls/loginjs.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            userloginjspanel.ID = "m_uxloginjspanel";
            uxloginjspannel.Controls.Add(userloginjspanel);

            Control usercsspanel = LoadControl(String.Format("../../Themes/{0}/LayoutControls/defaultcss.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            usercsspanel.ID = "m_uxcsspanel";
            uxcsspanel.Controls.Add(usercsspanel);
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
}