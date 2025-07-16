using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

public partial class login3 : System.Web.UI.Page
{
    SP_Methods adm = new SP_Methods();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {


        //comment should be removed while deployment
        //HttpCookie cookie = HttpContext.Current.Request.Cookies["internet"];
        //if (cookie.Value != null)
        //{
        //    if (cookie.Value.ToString().Trim() == "true")
        //    {
        //        Response.Redirect(ConfigurationManager.AppSettings["internetURL"].ToString().Trim());
        //    }
        //    else
        //    {
        //        Response.Redirect(ConfigurationManager.AppSettings["intranetURL"].ToString().Trim());
        //    }
        //}
        //else
        //{
        //    string URL = Request.Url.AbsoluteUri;
        //    string path = "";
        //    if (URL.Contains("HRMS_ADMIN"))
        //    {
        //        path = ConfigurationManager.AppSettings["intranetURL"].ToString().Trim();
        //    }
        //    else if (URL.Contains("intranet.highbartech.com"))
        //    {
        //        path = ConfigurationManager.AppSettings["internetURL"].ToString().Trim();
        //    }
        //    //commented by sony to make it work offline
        //    //Response.Redirect(path);
        //    Response.Redirect(path);
        //}


        

        //SAGAR ADDED BELOW CODE FOR ADDING TIME LIMITATION IN PROJECT 17OCT2017 STARTS HERE
        //string date = "2017/10/17";
        //string currenttime = DateTime.Now.ToString("yyyy/MM/dd");

        //if (date == Convert.ToString(currenttime))
        //{
        //    HttpContext.Current.Response.Redirect("errorpg.aspx");
        //}
        //SAGAR ADDED BELOW CODE FOR ADDING TIME LIMITATION IN PROJECT 17OCT2017 ENDS HERE
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        string search_str = "";

        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "Validate";
        string username = String.Format("{0}", Request.Form["email"]);
        string userpwd = String.Format("{0}", Request.Form["password"]);
        spars[1] = new SqlParameter("@SearchString", SqlDbType.VarChar);
        if (username.ToString() == "")
            spars[1].Value = DBNull.Value;
        else
            spars[1].Value = username.ToString();

        spars[2] = new SqlParameter("@Pwd", SqlDbType.NVarChar);
        if (userpwd.ToString() == "")
            spars[2].Value = DBNull.Value;
        else
            spars[2].Value = userpwd.ToString();

        DataTable dt = adm.getDropdownList(spars, "SP_Admin_Validate_User");

        if (dt.Rows.Count>0)
        {
            Session["Empcode"] = dt.Rows[0]["Emp_Code"].ToString();
            //Session["LoginEmpmail"] = Convert.ToString(UserName.Text).Trim();
            Session["LoginEmpmail"] = dt.Rows[0]["Emp_Emailaddress"].ToString();
            Session["emp_loginName"] = dt.Rows[0]["Emp_Name"].ToString();
            Response.Redirect("Default.aspx");
        }
    }
}