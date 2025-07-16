using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

public partial class Components_Common_homebrand : System.Web.UI.UserControl
{
    private void PopulateControl()
    {
        try
        {
            DataTable Dt_setting = new DataTable();
            Dt_setting = null;
            Dt_setting = settingmanager.GetSettingValue("hbrand", "H");
            if (Dt_setting.Rows.Count > 0)
            {
                if (Dt_setting.Rows[0]["value"].ToString() == "T")
                {
                    Control userbrandrpanel = LoadControl(String.Format("../../Themes/{0}/LayoutControls/homebrand.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
                    userbrandrpanel.ID = "m_uxbrandpanel";
                    uxbrandpanel.Controls.Add(userbrandrpanel);
                }
            }

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