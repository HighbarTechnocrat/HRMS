using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;


public partial class Components_Common_topheaderpd : System.Web.UI.UserControl
{
    private void PopulateControl()
    {
        try
        {

            Control userConttrollogo = LoadControl(String.Format("../../Themes/{0}/LayoutControls/topheaderlogo.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            userConttrollogo.ID = "m_uxlogopanel";
            uxlogopanel.Controls.Remove(userConttrollogo);
            uxlogopanel.Controls.Add(userConttrollogo);

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