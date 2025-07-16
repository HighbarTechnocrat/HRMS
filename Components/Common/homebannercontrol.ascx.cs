using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class Components_Common_homebannercontrol : System.Web.UI.UserControl
{
    private void PopulateControl()
    {
        //try
        //{

            DataTable Dt_setting=new DataTable();
            Dt_setting = null;
            Dt_setting = settingmanager.GetSettingValue("topbanner", "H");
            if (Dt_setting.Rows.Count > 0)
            {
                if (Dt_setting.Rows[0]["value"].ToString() =="T")
                {
                    Control userbannerpanel = LoadControl(String.Format("../../Themes/{0}/LayoutControls/topheaderbanner.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
                    userbannerpanel.ID = "m_uxbannerpanel";
                    uxbannerpanel.Controls.Add(userbannerpanel);
                }
            }
            Dt_setting = null;
            Dt_setting = settingmanager.GetSettingValue("hpromobanner", "H");

            if (Dt_setting.Rows.Count > 0)
            {

                if (Dt_setting.Rows[0]["value"].ToString() == "T")
                {
                    Control userpromobanner = LoadControl(String.Format("../../Themes/{0}/LayoutControls/Promodefault.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
                    userpromobanner.ID = "m_uxpromobanner";
                    uxpromobanner.Controls.Add(userpromobanner);

                }
            }

            if (Dt_setting.Rows.Count > 0)
            {

                if (Dt_setting.Rows[0]["value"].ToString() == "T")
                {
                    Control userbanner = LoadControl(String.Format("../../Themes/{0}/LayoutControls/promobannerm.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
                    userbanner.ID = "m_uxpromobannerm";
                    uxpromobanner.Controls.Add(userbanner);

                }
            }

            Control userbirth = LoadControl(String.Format("../../Themes/{0}/LayoutControls/birthdays.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            uxbirth.ID = "m_uxuserbirth";
            uxbirth.Controls.Add(userbirth);

            Control usercatbanner = LoadControl(String.Format("../../Themes/{0}/LayoutControls/catproduct2.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            usercatbanner.ID = "m_uxarrivalproduct";
            uxcategorypanel.Controls.Add(usercatbanner);
        
                    //Control usernewarrivalpanel = LoadControl(String.Format("../../Themes/{0}/LayoutControls/arrivalproduct.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
                    //usernewarrivalpanel.ID = "m_uxcatproduct";
                    //uxnewarrivalpanel.Controls.Add(usernewarrivalpanel);



            Dt_setting = null;
            Dt_setting = settingmanager.GetSettingValue("hafproduct", "H");
            if (Dt_setting.Rows.Count > 0)
            {
                if (Dt_setting.Rows[0]["value"].ToString() == "T")
                {
                    Control userpanel = LoadControl(String.Format("../../Themes/{0}/LayoutControls/homearrivalfeatureproduct.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
                    userpanel.ID = "uxnewpanel";
                   // uxnewarrivalpanel.Controls.Add(userpanel);
                }
            }



            Control mywall = LoadControl(String.Format("../../Themes/{0}/LayoutControls/mywall.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            mywall.ID = "m_uxmywall";
            uxwall.Controls.Add(mywall);
 
        //}
        //catch (Exception ex)
        //{
        //    ErrorLog.WriteError(ex.ToString());
        //}
    }
    protected void Page_init(object sender, EventArgs e)
    {
        //try
        //{

            PopulateControl();
        //}
        //catch (Exception ex)
        //{
        //    ErrorLog.WriteError(ex.ToString());
        //}
    }
}