using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Xml;
using System.Text;
using System.IO;
using System.Data.Common;
using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;

public partial class newslettersnew : System.Web.UI.Page
{
    private int projectid = 0, ID;
    public static string searchtype = "";
    //private int projectcatid ;
    public static string projectcatid = "";
    public static string projectstatusid = "";
    public static string projecttitle = "";
    public static string projectcatname = "";
    //public static string all = "";
    //public static string all = "";
    //public static string all = "";
    //public static string all = "";
    public static int PageSize = 10, PageIndex, RecordCount;
    public static int pgi;
    public static int pageCount;
    public static double dblmainpg;
    public static int maxpgcount;
    public static int maxpage = 5;
    public static int maxpg = 1;
    SP_Methods spm = new SP_Methods();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }


    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
        //    if (!Page.IsPostBack)
        //    {
        //        HDEmpCode.Value = Convert.ToString(Session["Empcode"]);
        //        GetEmployeeDetails();

        //    }
        //}
        //catch (Exception ex)
        //{
        //    ErrorLog.WriteError(ex.ToString());
        //}
    }

    //public void GetEmployeeDetails()
    //{
    //    try
    //    {
    //        DataTable dtEmpDetails = new DataTable();
    //        dtEmpDetails = spm.GetEmployeeData(HDEmpCode.Value);
    //        if (dtEmpDetails.Rows.Count > 0)
    //        {
    //            if (Convert.ToString(dtEmpDetails.Rows[0]["EMPLOYMENT_TYPE"]) == "3")
    //            {
    //                lblmsg.Visible = true;
    //                Div1.Visible = false;
    //                Div2.Visible = false;
    //                DivMessage.Visible = true;
    //            }
    //            else
    //            {
    //                lblmsg.Visible = false;
    //                Div1.Visible = true;
    //                Div2.Visible = true;
    //                DivMessage.Visible = false;
    //            }

    //        }
    
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message.ToString());
    //        Response.End();
    //        throw;
    //    }
    //}

    //sagar try below code for project search button 1dec 2017






    //DataSet dt = classproject.projectdetails1( PageIndex, PageSize, out RecordCount);





    public string getprojectURL(object projectid)
    {
        string strurl = "";
        try
        {
            strurl = ReturnUrl("sitepathmain") + "projectdetail.aspx?projectid=" + UrlRewritingVM.Encrypt(projectid.ToString());
            return strurl;

        }
        catch (Exception ex)
        {
            return strurl;
        }
    }


  

}



