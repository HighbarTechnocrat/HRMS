using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using LnDPortal.Business.Entity;
//using LnDPortal.Business.Entity;
using LnDPortal.Business;
using System.Net;

public partial class control_menu : System.Web.UI.UserControl
{
    static string LoginID;
    public static string MenuLinkReferese = "";

    public static string CmsMenuFileContent="";
    static double EmployeeID;
    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
    {
     // Response.End();
        //if (Session["PeopleLogin"]!=null)
        //{
        //    LoginID = Session["PeopleLogin"].ToString();
        //    //Response.Write("LoginID=" + LoginID);
          
        //}
        //else
        //{
            
       // }
        
       //Response.Write(LoginID);
        Users User = new Users();
        //Response.Write("session=" + Session["EmpNo"]);
        ////Response.End();
        //if (Session["EmpNo"]!=null)
        //{
        //    LoginID = Session["EmpNo"].ToString();
        //}
        //else
        //{
        LoginID = Users.GetLoginID(Page.User.Identity.Name);
        // LoginID = Users.GetLoginID("designer1");
        //}
        User = Users.GetUsersDetailsByLoginID(LoginID);
        lblFullName.Text = User.EmployeeName;
        EmployeeID = User.EmployeeId;

       // string UserPriorityRole = Users_Roles.getUserPriorityRole(User.EmployeeId);
        MenuLinkReferese = Utility.sitepathLnDPortal;
        //switch (UserPriorityRole)
        //{
        //    case "Administrator": MenuLinkReferese += Utility.AdminFolder + "/";
        //        break;
        //    case "Employee": MenuLinkReferese += Utility.EmployeeFolder + "/";
        //        break;
        //    case "Supervisor": MenuLinkReferese += Utility.SupervisorFolder + "/";
        //        break;
        //    case "Trainer": MenuLinkReferese += Utility.TrainerFolder + "/";
        //        break;
        //    default:
        //        break;
        //}
        string url = Utility.sitepathCMS+"menu/menu.html";
        try
        {
          //  WebClient client = new WebClient();
           // CmsMenuFileContent = client.DownloadString(url);
            string strCMSMenuPhysicalPath = ConfigurationManager.AppSettings["CMSPhysicalPath"].ToString() + "menu\\menu.html";
            CmsMenuFileContent = System.IO.File.ReadAllText(@strCMSMenuPhysicalPath);
        }
        catch (Exception ex)
        {
            // handle error
           // Response.Write(ex.Message);
        }

        bool Admin = Users_Roles.CheckUserInRole(User.EmployeeId, "Administrator");
       
        if (Admin)
        {
            tdEmployeeMenu.Visible = false;
            tdAdminMenu.Visible = true;
            tdAdminlink.Visible = true;
            tdAdminIcon.Visible = true;
            SitemapAdmin.Visible = true;
            SitemapSuperVisior.Visible = false;
            SitemapEmployee.Visible = false;
            //SitemapTrainer.Visible = false;
        }
        else
        {
            tdAdminMenu.Visible = false;
            tdAdminlink.Visible = false;
            tdAdminIcon.Visible = false;
            SitemapSuperVisior.Visible = true;
            SitemapEmployee.Visible = true;
            SitemapAdmin.Visible = false;
           DataSet Ds =  Users.getUsersBySupervisorcode(EmployeeID);

           //Response.Write(Ds.Tables[0].Rows.Count);
            // if user is supervisor
           if (Ds.Tables[0].Rows.Count > 0)
           {
               tsSupervisorMenu.Visible = true;
               tdEmployeeMenu.Visible = false;
               SitemapSuperVisior.Visible = true;
               SitemapEmployee.Visible = false;
               SitemapAdmin.Visible = false;
           }
           else
           {
               tdEmployeeMenu.Visible = true;
               tsSupervisorMenu.Visible = false;
               SitemapSuperVisior.Visible = false;
               SitemapEmployee.Visible = true;
               SitemapAdmin.Visible = false;
           }
          
           //Response.Write(tdEmployeeMenu.Visible);

          
           

        }

       // Response.Write(CmsMenuFileContent);

    }
}
