using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Components_Common_donation : System.Web.UI.UserControl
{
    
     private void PopulateControl()
    {
        try
        {

            Control userConttrobreadcrum = LoadControl(String.Format("../../Themes/{0}/LayoutControls/basicbreadcum.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            userConttrobreadcrum.ID = "m_uxuserConttrobreadcrum";
            uxbreadcrum.Controls.Remove ( userConttrobreadcrum );
            uxbreadcrum.Controls.Add ( userConttrobreadcrum );

            Control userConttrodonate = LoadControl (String.Format ( "../../Themes/{0}/LayoutControls/donation.ascx" , ConfigurationManager.AppSettings ["projectname"].ToString ( ) ) );
            userConttrodonate.ID = "m_uxdonate";
            uxdonatePanel.Controls.Remove ( userConttrodonate );
            uxdonatePanel.Controls.Add ( userConttrodonate );
        }

        catch ( Exception ex )
        {
	        ErrorLog.WriteError(ex.ToString());
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