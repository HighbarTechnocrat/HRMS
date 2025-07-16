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

public partial class newsdetails : System.Web.UI.Page
{
    private int newsid = 0, ID;
    public static int PageSize = 20, RecordCount;
    public static int pgi=1;
    public static string icon="";

    public static int pageCount;
    public static int maxpage = 5;
    public static double dblmainpg;
    public static int maxpgcount;
    public static int maxpg = 1;

     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        if(Page.User.Identity.IsAuthenticated== true)
        {
            if (!IsPostBack)
            {
                ListMainCategories();
            }
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepath") + "login.aspx?ReturnUrl=" + Request.RawUrl);
        }
    }

    private void ListMainCategories()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["GlobalConnection"].ConnectionString);
        DataTable dt = new DataTable();
        SqlDataAdapter adp = new SqlDataAdapter("SELECT * FROM faqcategory", con);
        adp.Fill(dt);
        Repeatercat.DataSource = dt;
        Repeatercat.DataBind();
        if(Repeatercat.Items.Count>0)
        {
            pnlfaq.Visible = true;
        }
        else
        {
            pnlfaq.Visible = false;
        }
    }

    protected void DateRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Repeater subRepeter = (Repeater)e.Item.FindControl("SubRepeater");

        int faqcatid = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "faqcatid"));

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["GlobalConnection"].ConnectionString);
        string syntax = "SELECT * FROM faq WHERE faqcatid=@faqcatid";
        SqlCommand cmd = new SqlCommand(syntax, con); // commandText and connection
        cmd.Parameters.AddWithValue("faqcatid", faqcatid);

        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        adp.Fill(dt);

        subRepeter.DataSource = dt;
        subRepeter.DataBind();
    }

}