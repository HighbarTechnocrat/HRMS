using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Components_Common_login : System.Web.UI.UserControl
{

	private void PopulateControl()
	{
        try
        {

        //Control userConttrobreadcrum = LoadControl(String.Format("../../Themes/{0}/LayoutControls/basicbreadcum.ascx" , ConfigurationManager.AppSettings ["projectname"].ToString()));
        //userConttrobreadcrum.ID = "m_uxuserConttrobreadcrum";
        //uxbreadcrum.Controls.Remove(userConttrobreadcrum);
        //uxbreadcrum.Controls.Add(userConttrobreadcrum);

		Control userConttrologin = LoadControl(String.Format("../../Themes/{0}/LayoutControls/login.ascx" , ConfigurationManager.AppSettings ["projectname"].ToString()));
		userConttrologin.ID = "m_uxlogin";
		uxloginPanel.Controls.Remove(userConttrologin);
		uxloginPanel.Controls.Add(userConttrologin);

        //SAGAR UNCOMMENTED THIS FOR REGISTRING USER 23OCT2017 STARTS HERE
        Control userControlregister = LoadControl(String.Format("../../Themes/{0}/LayoutControls/register.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
        userControlregister.ID = "m_uxregister";
        uxregisterPanel.Controls.Remove(userControlregister);
        uxregisterPanel.Controls.Add(userControlregister);
        //SAGAR UNCOMMENTED THIS FOR REGISTRING USER 23OCT2017 ENDS HERE

		Control userCtrloginbenifit = LoadControl(String.Format("../../Themes/{0}/LayoutControls/loginbenifits.ascx" , ConfigurationManager.AppSettings ["projectname"].ToString()));
		userCtrloginbenifit.ID = "m_uxloginbenefit";
		uxloginbenifitPanel.Controls.Remove(userCtrloginbenifit);
		uxloginbenifitPanel.Controls.Add(userCtrloginbenifit);


        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
	}
	protected void Page_init(object sender , EventArgs e)
	{
        //try
        //{
			//if (!IsPostBack)
			//{
			PopulateControl();
			//}
        //}
        //catch ( Exception ex )
        //{
        //    ErrorLog.WriteError(ex.ToString());
        //}
	}
	protected void Login_Click(object sender , EventArgs e)
	{
		uxloginPanel.Visible = true;
		uxregisterPanel.Visible = false;
	}
	protected void signup_Click(object sender , EventArgs e)
	{
		uxloginPanel.Visible = false;
		uxregisterPanel.Visible = true;
	}
}