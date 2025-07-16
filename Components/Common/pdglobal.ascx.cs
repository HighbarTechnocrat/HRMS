using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Components_Common_pdglobal : System.Web.UI.UserControl
{
    private void PopulateControl()
    {
        try
        {
            Control userpdConttrol = LoadControl(String.Format("../../Themes/{0}/LayoutControls/pd/pdsummery.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            userpdConttrol.ID = "m_uxPdLayout";
            uxpdPanel.Controls.Add(userpdConttrol);

            Control useruxpdzommer = LoadControl(String.Format("../../Themes/{0}/LayoutControls/pd/zommer.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            useruxpdzommer.ID = "m_uxpdzommer";
            uxpdzommer.Controls.Add(useruxpdzommer);

            Control useruxbreadcum = LoadControl(String.Format("../../Themes/{0}/LayoutControls/pd/pdbreadcum.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            useruxbreadcum.ID = "m_uxbreadcum";
            uxpdbreadcum.Controls.Add(useruxbreadcum);




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
