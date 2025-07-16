using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Mail;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;


public partial class control_enquiry : System.Web.UI.UserControl
{
    public static int prodid=0;
    public static int userid=0;
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        if(Page.User.Identity.IsAuthenticated)
        {
            if (!(Page.IsPostBack))
            {
                txtproductname.Attributes.Add("readonly", "readonly");
                if (Request.QueryString["pid"].ToString().Length == 24 && Request.QueryString["pid"] != "" && Request.QueryString["pid"] != null && Request.QueryString["pid"] != "0" && Request.QueryString.Count == 1)
                {
                    if (Request.QueryString.Count == 1)
                    {
                        if (Int32.TryParse(UrlRewritingVM.Decrypt(Request.QueryString["pid"]), out prodid))
                        {
                            prodid = Convert.ToInt32(commonclass.GetSafeIDFromURL(UrlRewritingVM.Decrypt(Request.QueryString["pid"].ToString().Trim())));
                            txtproductname.Visible = true;
                            txtproductname.ReadOnly = true;
                        }
                        else
                        {
                            Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
                    }
                }
                else
                {
                    prodid = 0;
                    txtproductname.Visible = false;
                    liprod.Visible = false;
                }
                loaddata();
            }
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnURL=" + Request.Url.Fragment);
        }
    }
  
    public void loaddata()
    {
        if(prodid !=0)
        {
            DataTable ds = classproduct.get_proc_ProductDescription_ProdFeature(prodid);
            if (ds.Rows.Count > 0)
            {
                txtproductname.Text = ds.Rows[0]["productname"].ToString();
            }
        }
    }

    protected void btnsend_Click(object sender, EventArgs e)
    {
        int i = 0;
        i = Convert.ToInt32(classenquiry.createenquiry(Page.User.Identity.Name.ToString(), txtsubject.Text.ToString(), txtusername.Text.ToString().Trim(),txtcontact.Text.ToString().Trim(), prodid, "", txtcomment.Text.ToString()));
        if( i > 0)
        {
            senddiv.Visible = true;
            lblError.ForeColor = System.Drawing.Color.Green;
            lblError.Text = "Enquiry Send Successfully!!!";
            reset();
        }
        else
        {
            senddiv.Visible = false;
            lblError.ForeColor = System.Drawing.Color.Red;
            lblError.Text = "Failed to send enquiry!!!";
            enqform.Visible = true;
        }
    }
    public void reset()
    {
        txtsubject.Text = "";
        txtcontact.Text = "";
        txtproductname.Text = "";
        txtcomment.Text = "";
        txtusername.Text = "";
        enqform.Visible = false;
    }

    protected void lnkcont_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "enquiry/");
    }
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }
}
