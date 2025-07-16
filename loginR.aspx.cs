using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class loginR : System.Web.UI.Page
{
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    public string username = "";
    SP_Methods sp = new SP_Methods();
    protected void Page_Load(object sender, EventArgs e)
    {

        //Response.Redirect("undermaintain.aspx");
        //return;

      /*  string tt = UrlRewritingVM.Decrypt("9nQ-8bwn0dYnjBaQIq6bTg==");
       Response.Write(tt);
       Response.End();
       */

		Int32 ichk=0;
		 string chekApprUrl = "";        
		  chekApprUrl =HttpContext.Current.Request.Url.AbsoluteUri;
        if (chekApprUrl.Contains("ReturnUrl"))
		{
        // Get first three characters.
		ichk = Convert.ToInt32(chekApprUrl.Length);
        string aaprURL = chekApprUrl.Substring(51, ichk - 51);
			//Response.Redirect(words[1]);
			//Response.Write(sub);
		//	Response.End();

        

		Session["aprurl"]=aaprURL;
		}
        if (Request.QueryString["user"] != null)
        {
			string duser = UrlRewritingVM.Decrypt(Request.QueryString["user"].ToString());
			            creative.Common cmn = new creative.Common();

		   string email = "";			
		   email = cmn.getADUserDetaildByName(UrlRewritingVM.Decrypt(duser));

			DataTable dtUserlogin = sp.getuserinfodetails(email);
            		   
            if (dtUserlogin.Rows.Count > 0)
            {
                if (Convert.ToString(dtUserlogin.Rows[0]["errmsg"]).Trim() == "")
                {
                    Session["Empcode"] = dtUserlogin.Rows[0]["Emp_Code"].ToString();
                    //Session["LoginEmpmail"] = Convert.ToString(UserName.Text).Trim();
                    Session["LoginEmpmail"] = dtUserlogin.Rows[0]["Emp_Emailaddress"].ToString();
                    Session["emp_loginName"] = dtUserlogin.Rows[0]["Emp_Name"].ToString();
                    //m_valid = true;

                    DataTable dtRecr_login = sp.getuserinfodetails_for_Recruitment(Convert.ToString(Session["Empcode"]).Trim());
                    if (dtRecr_login.Rows.Count > 0)
                    {
                        if (Convert.ToString(dtRecr_login.Rows[0]["errmsg"]).Trim() != "")
                        {
                            //Response.Redirect("http://172.18.37.5/recruitment/SessionEnd.aspx");
                        }
                    }

                }
            }
            #region Insert use log
            int i = (int)Application["NoOfVisitors"];
           // if (i == 1)
            //{
                SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconnection"].ToString());
                if (scon.State == ConnectionState.Closed || scon.State == ConnectionState.Broken)
                    scon.Open();
                SqlCommand com = new SqlCommand();
                com = scon.CreateCommand();
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "SP_insert_UserLog";
                com.Parameters.AddWithValue("@empcode", Session["Empcode"].ToString());
                com.Parameters.AddWithValue("@from_date", DateTime.Now.Date);
                com.Parameters.AddWithValue("@count", 1);
                com.ExecuteNonQuery();
            //}
            #endregion
            
		 
						 
            username = Request.QueryString["user"].ToString();
           //email = cmn.getADUserDetaildByName(UrlRewritingVM.Decrypt(username));
            if(email!=null&&email!="")
            {

               
                CheckBox Persist = new CheckBox();
                Persist.Checked = true;
                AuthenticateEventArgs w = new AuthenticateEventArgs();
                w.Authenticated = true;
                //Response.Write(UrlRewritingVM.Decrypt(username));
               // Response.End();
							
                FormsAuthentication.RedirectFromLoginPage(email, Persist.Checked);
             
			 if (Convert.ToString(Session["aprurl"]).Trim()!="")
			 {
                 
				  Response.Redirect(Convert.ToString(Session["aprurl"]).Trim());
			 }
			 else
			 {                 
                //Response.Redirect("Default.aspx");
                 Response.Redirect("http://localhost/hrms/procs/RecruitmentsPositions.aspx");
			 }
            }
        }
		
		//Response.Redirect("http://172.18.37.5/logon/login.aspx");
        Response.Redirect("http://localhost/hcc_Recruitment/login.aspx");

    }
}