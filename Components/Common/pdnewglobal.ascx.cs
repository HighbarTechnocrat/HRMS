using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Components_Common_pdnewglobal : System.Web.UI.UserControl
{
    private void PopulateControl()
    {
        //try
        //{
            Control userpdConttrol = LoadControl(String.Format("../../Themes/{0}/LayoutControls/pdnew/pdsummery.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            userpdConttrol.ID = "m_uxPdLayout";
            uxpdPanel.Controls.Add(userpdConttrol);

            Control useruxpdzommer = LoadControl(String.Format("../../Themes/{0}/LayoutControls/pdnew/zommer.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            useruxpdzommer.ID = "m_uxpdzommer";
            uxpdzommer.Controls.Add(useruxpdzommer);

        //}
        //catch (Exception ex)
        //{
        //    ErrorLog.WriteError(ex.ToString());
        //}
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
