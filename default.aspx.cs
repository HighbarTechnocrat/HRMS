using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class _default : System.Web.UI.Page
{
    public static string username = "", imgpath = "", name = "";
		SP_Methods spm = new SP_Methods();
    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
    {
		
         
        if (Page.User.Identity.IsAuthenticated)
        {
            username = Page.User.Identity.Name.ToString().Trim();
					
            DataTable dt = Classuserwidget.getwidgetAdminStatus(4);
            if(dt.Rows.Count > 0)
            {
                DataTable dtuser = Classuserwidget.getwidgetUserStatus(username, 4);
                if(dtuser.Rows.Count > 0)
                {
                    //DataTable dtadd = classaddress.getuserinfodetails(username);
					//Response.Write(username);
					//Response.End();
                    DataTable dtadd = classaddress.getuserbirthdaygreeting(username);
                    if (dtadd.Rows.Count > 0)
                    {
						  #region Set Sessions Variables
							username = Page.User.Identity.Name.ToString().Trim();

							DataTable dtUserlogin = spm.getuserinfodetails(Convert.ToString(username).Trim());
							  if (dtUserlogin.Rows.Count > 0)
							  {
								  if (Convert.ToString(dtUserlogin.Rows[0]["errmsg"]).Trim() == "")
								  {
									  Session["Empcode"] = dtUserlogin.Rows[0]["Emp_Code"].ToString();
									  Session["LoginEmpmail"] = Convert.ToString(username).Trim();
									  Session["emp_loginName"] = dtUserlogin.Rows[0]["Emp_Name"].ToString();
									  //m_valid = true;
								  }

							  }
                         #endregion
	
                        if (dtadd.Rows[0]["profilephoto"].ToString() == "" || dtadd.Rows[0]["profilephoto"].ToString() == null)
                        {
                            imgpath = ReturnUrl("mediapath") + "profile110x110/" + "noimage1.png";
                        }
                        else
                        {
                            imgpath = ReturnUrl("mediapath") + "profile110x110/" + dtadd.Rows[0]["profilephoto"].ToString(); ;
                        }
                        //name = dtadd.Rows[0]["firstname"].ToString() +" "+ dtadd.Rows[0]["lastname"].ToString();
                        name = dtadd.Rows[0]["fullname"].ToString();						
                        if (Cache["home"] == null)
                        {
                            Cache.Insert("home", "true", null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(24, 0, 0));
                            ltrpopup.Text = "<div class='divpopup'><div class='divclose'><a id='btnclose' class='b-close'>X</a></div><div class='loginregister'><h2 class='box-title'><font>Happy Birthday</font></h2><div class='popimage'><h4>" + name + "</h4><h5>Do not count the candles but notice the light they give. Do not count the years; looks at the life you live.\"Happy Birthday\"</h5><img width='25%' src='" + imgpath + "'></div></div></div>";
                        }
                    }
                    //Response.Write(Cache["show"].ToString());
                }
            }
			else{
				Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
			}
        }  
    }
    
}