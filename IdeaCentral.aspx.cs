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

public partial class IdeaCentral : System.Web.UI.Page
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
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
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
            loaddata(1);
           
        }

    }

    //sagar try below code for project search button 1dec 2017
    protected void lnksearch_Click(object sender, EventArgs e)
    {
        loaddata(1);
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
		string strurl = "";
        try
        {
           // strurl = ReturnUrl("sitepathmain") + "projectdetail.aspx?projectid=" + UrlRewritingVM.Encrypt(projectid.ToString());
            
			strurl=ReturnUrl("sitepathmain")+ "AddIdeaCentral.aspx";
			//return strurl;

        }
        catch (Exception ex)
        {
           // return strurl;
        }
		
	}

    public void loaddata(int PageIndex)
    {
        lblmsg.Visible = false;
        try
        {            
			DataSet dt = classcategory.getallIdeaCentral( PageIndex, PageSize, out RecordCount);
			
            if (dt.Tables.Count > 0)
            {
                if (dt.Tables[0].Rows.Count > 0)
                {
                    pnlIdea.Visible = true;
                    rptridea.DataSource = dt.Tables[0];
                    rptridea.DataBind();
                    if (rptridea.Items.Count > 0)
                    {
                        pnlIdea.Visible = true;
                        lblmsg.Visible = false;
                    }
                    else
                    {
                        pnlIdea.Visible = false;
                        lblmsg.Visible = true;
                        lblmsg.Text = "No Record Found  ";
                    }
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
            if (pageCount == 1)
            {
                rptPager.Visible = true;
            }
            else
            {
                rptPager.Visible = true;
            }
        }
        else
        {
            rptPager.Visible = true;
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
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
            
			strurl=ReturnUrl("sitepathmain")+ "AddIdeaCentral.aspx";
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


}



