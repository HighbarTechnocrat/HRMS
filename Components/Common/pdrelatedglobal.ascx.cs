using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Components_Common_pdrelatedglobal : System.Web.UI.UserControl
{
    private void PopulateControl ( )
        {
        try
            {
                Control userpdConttrol_1 = LoadControl(String.Format("../../Themes/{0}/LayoutControls/pd/pdrelatedproduct.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            userpdConttrol_1.ID = "m_uxPdLayout1";
            uxpdPanel.Controls.Add ( userpdConttrol_1 );

           
            }
        catch ( Exception ex )
            {
            ErrorLog.WriteError ( ex.ToString ( ) );
            }
        }
    protected void Page_init ( object sender , EventArgs e )
        {
        try
        {

        PopulateControl ( );
        }
        catch ( Exception ex )
        {
	        ErrorLog.WriteError(ex.ToString());
        }
        }
}