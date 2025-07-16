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


public partial class newsdetail : System.Web.UI.Page
{
    int newsid;
    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["newsid"] != null && Request.QueryString["newsid"] != "" && Request.QueryString.Count == 1)
            {
                if (Int32.TryParse(UrlRewritingVM.Decrypt(Request.QueryString["newsid"].ToString().Trim()), out newsid))
                {
                    newsid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["newsid"]));
                    if (newsid != 0)
                    {
                        DataTable dt = classnews.getsinglenewsuser(newsid);
                        bind(newsid);
                        this.Title = creativeconfiguration.SiteName + ": News: " + (dt.Rows[0]["newstitle"]).ToString();
                    }
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
    }

    public void bind(int newsid)
    {
        try
        {
            DataTable dt = classnews.getsinglenewsuser(newsid);
            rptrnewsdetail.DataSource = dt;
            rptrnewsdetail.DataBind();
        }
        catch (Exception ex)
        { 
        
        }
        
       
    }
    protected void lbtnback_Click(object sender, EventArgs e)
    {
    Response.Redirect("news.aspx");
    }
    //Jayesh_Sagar added below code(stable) to display news images on newsdetail.aspx page 21nov2017
    public string getuserimage(object imagepath)
    {
        string strurl = "";
        try
        {
            if (imagepath.ToString().Length > 0)
            {
                strurl = ReturnUrl("sitepathadmin") + "images/news/" + imagepath.ToString();//ReturnUrl("sitepath")
            }
            else
            {
                strurl = ReturnUrl("sitepath") + "images/news/noimage1.png";
            }
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
    //Jayesh_Sagar added above code(stable) to display news images on newsdetail.aspx page 21nov2017


}
