using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class themes_creative1 : System.Web.UI.UserControl
{
    public static int userid, PageCount = 5;
    public static string emailid;
    public int pid;
    public string fname = null;

     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bindwall(PageCount);
        }
    }
    public void bindwall(int pagecount)
    {
        DataSet dswall = new DataSet();
        dswall.ReadXml(ConfigurationManager.AppSettings["adminsitepath"] + "xml/mywall.xml");
        if(dswall.Tables.Count>0)
        {
            rptwall.DataSource = dswall.Tables[0].AsEnumerable().Take(pagecount).CopyToDataTable();
            rptwall.DataBind();
            alter();
        }
    }
    public void alter()
    {
        if(rptwall.Items.Count>0)
        {
            for(int i=0;i<rptwall.Items.Count;i++)
            {
                Label lblcount = (Label)rptwall.Items[i].FindControl("lblcount");
                Label lblpid = (Label)rptwall.Items[i].FindControl("lblpid");
                DataTable dtlike = classreviews.getlikedetailsbyuserpostid("", Convert.ToInt32(lblpid.Text));
                if (dtlike.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtlike.Rows[0]["likecount"].ToString())>0)
                    {
                        lblcount.Visible = true;
                        lblcount.Text = " <i class='fa fa-thumbs-o-up'></i> " + dtlike.Rows[0]["likecount"].ToString().Trim(); 
                    }                   
                }
            }
        }
        else
        {
            woffice_wiki.Visible = false;
        }
    }
    public string productUrlrewriting2(object cattype, object productname, object productid)
    {
        string strurl = "";
        string cat = "";
        try
        {
            cat = cattype.ToString();
            if (cat == "I")
            {
                strurl = UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(productid.ToString().Trim()), "PG");
            }
            else
            {
                strurl = UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(productid.ToString().Trim()), "PDID");
            }
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
}