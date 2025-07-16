using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class GalleryHome : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
     

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    private void Page_Load(object sender, System.EventArgs e)
    {
      

    }
     
}