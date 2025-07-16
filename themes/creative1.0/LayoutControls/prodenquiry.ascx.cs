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
using System.Net;
using System.Net.Mail;
using System.Web.Mail;
using System;
using System.Data;
using System.Configuration;

public partial class themes_creative1_0_LayoutControls_prodenquiry : System.Web.UI.UserControl
{
    private static int productId;
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
        {
            productId = Convert.ToInt32(Request.QueryString["p"].ToString());
        }
        if (!IsPostBack)
        {
            filluserdata();          
            loadproduct();
        }

       
    }

    public void filluserdata()
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            string username = string.Empty;
            username = Page.User.Identity.Name.ToString().Trim();
            DataSet ds_userdetails = classaddress.getalluserbyusername(this.Page.User.Identity.Name);
            string fname = ds_userdetails.Tables[0].Rows[0]["firstname"].ToString();
            string lname = ds_userdetails.Tables[0].Rows[0]["lastname"].ToString();
            string name = fname + " " + lname;
            txtname.Text = name;
            txtemailid.Text = ds_userdetails.Tables[0].Rows[0]["emailid"].ToString();
            txtcontact.Text = ds_userdetails.Tables[0].Rows[0]["mobileno"].ToString();
            txtadd.Text = ds_userdetails.Tables[0].Rows[0]["address"].ToString();
        }

    }

    protected void btncancel_Click(object sender, EventArgs e)
    {

        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public void loadproduct()
    {
        DataTable ds = classproduct.get_proc_ProductDescription_ProdFeature(productId);
        if (ds.Rows.Count > 0)
        {
            txtproduct.Text = ds.Rows[0]["productname"].ToString();
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        int i = Convert.ToInt32(clsenquiry.createprodenquiry(txtname.Text.Trim(), txtadd.Text.Trim(), txtemailid.Text.Trim(), txtcontact.Text.Trim(), productId, "", txtcomment.Text.Trim()));
        if (i > 0)
        {
            string body = Utilities.sendRegisterstatusEmailformat("PE", "", txtemailid.Text.Trim(), txtname.Text.Trim(), txtproduct.Text.Trim(), "", txtemailid.Text.Trim(), 0);
            pnlmsg.Visible = true;
            pnlenquiry.Visible = false;
        }

    }

}