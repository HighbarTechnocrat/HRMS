//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//public partial class ongoing : System.Web.UI.Page
//{
//    protected void Page_Load(object sender, EventArgs e)
//    {

//    }
//}


using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Drawing.Imaging;
using System.Collections.Generic;


public partial class AddIdeaCentral : System.Web.UI.Page
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
	 public string desc1;
    public string desc2;
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
	DateTime dtNow = DateTime.Now;
	
	
    protected void Page_Load(object sender, EventArgs e)
    {
	
        if (Int32.TryParse(Request.QueryString["id"], out ID))
        {
            if (Request.QueryString["id"] != "" && Request.QueryString["id"] != null)
            {
                projectid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["id"].ToString()));
            }
        }

        if (!IsPostBack)
        {
            pgi = 1;
            //loaddata(1);
           
        }

    }

    //sagar try below code for project search button 1dec 2017
    protected void lnksearch_Click(object sender, EventArgs e)
    {
        loaddata(1);
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
		int key = 0;
		string title = "";
		string username="";
		string description= "";
		username = Page.User.Identity.Name;
		DataTable ds = classproduct.gettopidproduct();
        int topid = Convert.ToInt32(ds.Rows[0][0]);
        string rfrom="", rto="", rtime="", to="";
        title = txttitle.Text.ToString();
		stopword(title);
		description = txtdesc.Text.ToString();
		if(title != "" && description != "")
		{
			key = Convert.ToInt32(classproduct.inserttextpost(title, (Convert.ToInt32(topid) + 1).ToString(), description, 'H', 'N', username, dtNow.ToString("MM/dd/yyyy"), dtNow.ToString("MM/dd/yyyy")));
			if(key>0)
			{
				DataTable dstxt = classproduct.gettopidproduct();
				int topid1 = Convert.ToInt32(dstxt.Rows[0][0]);
				classproduct.addcategorytoproduct((Convert.ToInt32(topid1)),63);
			}
			Response.Redirect(ConfigurationManager.AppSettings["sitepathmain"] + "ideacentral.aspx");
		}
		else
		{
			Response.Redirect(ConfigurationManager.AppSettings["sitepathmain"] + "addideacentral.aspx");
			
		}
		
		
	}

    public void loaddata(int PageIndex)
    {
        lblmsg.Visible = false;
        try
        {            
			DataSet dt = classcategory.getallIdeaCentral( PageIndex, PageSize, out RecordCount);

            pnlIdea.Visible = true;

            if (dt.Tables.Count > 0)
            {
                if (dt.Tables[0].Rows.Count > 0)
                {
                    pnlIdea.Visible = true;
                    
                }

                else
                {
                    pnlIdea.Visible = false;
                    lblmsg.Visible = true;
                    lblmsg.Text = "No Record Found  ";
                }

                this.PopulatePager(RecordCount, PageIndex);


            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    private void PopulatePager(int recordCount, int currentPage)
    {
        double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        List<ListItem> pages = new List<ListItem>();
        if (pageCount > 0)
        {
            for (int i = 1; i <= pageCount; i++)
            {
                pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
            }
            
        }
        else
        {
           
        }
       
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        pgi = pageIndex;
        this.loaddata(pageIndex);
        //string selectedValue = projectddl.SelectedItem.Value; 

    }
    public string getprojectURL(object projectid)
    {
        string strurl = "";
        try
        {
           // strurl = ReturnUrl("sitepathmain") + "projectdetail.aspx?projectid=" + UrlRewritingVM.Encrypt(projectid.ToString());
            
			strurl=ReturnUrl("sitepathmain")+ "ideacentral.aspx";
			return strurl;

        }
        catch (Exception ex)
        {
            return strurl;
        }
    }


    public void reset()
    {

        //txttitle.Text = "";
        //ddlprojectcat.SelectedIndex = 0;

    }
    protected void lnkreset_Click(object sender, EventArgs e)
    {
        //txttitle.Text = "";
        reset();
        loaddata(1);
        //loadallprojectadd();
    }
 public void stopword(string rword)
    {
        DataTable sw = classproduct.replacestopword();
        desc2 = rword;
        for (int j = 0; j < sw.Rows.Count; j++)
        {
            desc2 = desc2.ToString();
            string rword1;
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            rword1 = textInfo.ToTitleCase(sw.Rows[j]["stopword"].ToString().Trim());
            desc2 = desc2.ToString().Replace(rword1, sw.Rows[j]["replaceword"].ToString().Trim());
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString().ToUpper().Trim(), sw.Rows[j]["replaceword"].ToString().Trim());
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString().ToLower().Trim(), sw.Rows[j]["replaceword"].ToString().Trim());
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString().Trim(), sw.Rows[j]["replaceword"].ToString().Trim());
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + ',', sw.Rows[j]["replaceword"].ToString() + ',');
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + ';', sw.Rows[j]["replaceword"].ToString() + ';');
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + '.', sw.Rows[j]["replaceword"].ToString() + '.');
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + ':', sw.Rows[j]["replaceword"].ToString() + ':');
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + "&nbsp;", sw.Rows[j]["replaceword"].ToString() + "&nbsp;");
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + "!", sw.Rows[j]["replaceword"].ToString() + "!");
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + "@", sw.Rows[j]["replaceword"].ToString() + "@");
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + "#", sw.Rows[j]["replaceword"].ToString() + "#");
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + '*', sw.Rows[j]["replaceword"].ToString() + '*');
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + '_', sw.Rows[j]["replaceword"].ToString() + '_');
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + '&', sw.Rows[j]["replaceword"].ToString() + '&');
            desc2 = desc2.ToString().Replace('(' + sw.Rows[j]["stopword"].ToString() + ')', '(' + sw.Rows[j]["replaceword"].ToString() + ')');
            desc2 = desc2.ToString().Replace('{' + sw.Rows[j]["stopword"].ToString() + '}', '{' + sw.Rows[j]["replaceword"].ToString() + '}');
            desc2 = desc2.ToString().Replace('[' + sw.Rows[j]["stopword"].ToString() + ']', '[' + sw.Rows[j]["replaceword"].ToString() + ']');
            desc2 = desc2.ToString().Replace(sw.Rows[j]["stopword"].ToString() + '$', sw.Rows[j]["replaceword"].ToString() + '$');
        }
    }


}



