//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//public partial class completed : System.Web.UI.Page
//{
//    protected void Page_Load(object sender, EventArgs e)
//    {

//    }
//}



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

public partial class completed : System.Web.UI.Page
{
    private int projectid = 0, ID;
    public static string searchtype = "";
    //private int projectcatid ;
    public static string projectcatid = "";
    public static string projectstatusid = "";
    public static string projecttitle = "";
    public static string projectcatname = "";
    public static string projectstate = "";
    public static string projectyear = "";
    
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

        txtprjyear.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");

        if (!IsPostBack)
        {
            pgi = 1;
            loaddata(1);
            loadallprojectadd();
            loadallprojectstatus();
            loadallproject_States();
        }

    }

    //sagar try below code for project search button 1dec 2017
    protected void lnksearch_Click(object sender, EventArgs e)
    {
        loaddata(1);
    }
    public void loadallprojectadd()
    {
        DataTable dt = classproject.getallprojectcatlists();
        ddlprojectcat.DataSource = dt;
        ddlprojectcat.DataTextField = "projectcategory";
        ddlprojectcat.DataValueField = "projectcatid";
        ddlprojectcat.DataBind();
        ListItem item = new ListItem("ALL", "0");
        ddlprojectcat.Items.Insert(0, item);
    }

    public void loadallproject_States()
    {
        //DataTable dt = classproject.getallprojectstatuslists();
        DataTable dt = classproject.getallprojectstateList();
        ddlprojectStates.DataSource = dt;
        ddlprojectStates.DataTextField = "projectstate";
        ddlprojectStates.DataValueField = "projectstate";
        ddlprojectStates.DataBind();
        ListItem item = new ListItem("ALL", "0");
        ddlprojectStates.Items.Insert(0, item);

    }


    public void loadallprojectstatus()
    {
        DataTable dt = classproject.getallprojectstatuslists();
        ddlprojectstatus.DataSource = dt;
        ddlprojectstatus.DataTextField = "projectstatus";
        ddlprojectstatus.DataValueField = "projectstatusid";
        ddlprojectstatus.DataBind();
        ListItem item = new ListItem("ALL", "0");
        ddlprojectstatus.Items.Insert(0, item);
    }
    public void loaddata(int PageIndex)
    {
        lblmsg.Visible = false;
        try
        {
            projectstatusid = ddlprojectstatus.SelectedValue.ToString().Trim();
            projectcatid = ddlprojectcat.SelectedValue.ToString().Trim();
            projectstate = ddlprojectStates.SelectedValue.ToString().Trim();
            projectyear = Convert.ToString(txtprjyear.Text).Trim();
            //Response.Write(projectcatid);
            //Response.End();

            if (Convert.ToString(projectstatusid).Trim() == "")
                projectstatusid = "0";

            if (Convert.ToString(projectcatid).Trim() == "")
                projectcatid = "0";

            if (Convert.ToString(projectstate).Trim() == "")
                projectstate = "0";

            if (Convert.ToString(projectyear).Trim() == "")
                projectyear = "0";

            //rptPager.Visible = true;
            if (txttitle.Text.ToString().Trim() == "")
            {
                projecttitle = "";
            }
            else
            {
                projecttitle = commonclass.GetSafeSearchString(txttitle.Text.ToString().Trim());
            }

            //DataSet dt = classproject.projectdetails1( PageIndex, PageSize, out RecordCount);
            //DataSet dt = classproject.completedprojectsearch(projectcatid, projecttitle, PageIndex, PageSize, out RecordCount);
            DataSet dt = classproject.completedprojectsearch(projectcatid, projecttitle, PageIndex, PageSize, out RecordCount, projectstatusid, projectstate, projectyear);
            if (dt.Tables.Count > 0)
            {
                if (dt.Tables[0].Rows.Count > 0)
                {
                    pnlproject.Visible = true;
                    rptrproject.DataSource = dt.Tables[0];
                    rptrproject.DataBind();
                    if (rptrproject.Items.Count > 0)
                    {
                        pnlproject.Visible = true;
                        lblmsg.Visible = false;
                    }
                    else
                    {
                        pnlproject.Visible = false;
                        lblmsg.Visible = true;
                        lblmsg.Text = "No Project Found  ";
                    }
                }

                else
                {
                    pnlproject.Visible = false;
                    lblmsg.Visible = true;
                    lblmsg.Text = "No Project Found  ";
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
        // string selectedValue = projectddl.SelectedItem.Value; 

    }
    public string getprojectURL(object projectid)
    {
        string strurl = "";
        try
        {

            //strurl = ReturnUrl("sitepathmain") + "projectdetailc.aspx?projectid=" + UrlRewritingVM.Encrypt(projectid.ToString());
            strurl = ReturnUrl("sitepathmain") + "projectdetail.aspx?projectid=" + UrlRewritingVM.Encrypt(projectid.ToString());
            return strurl;

        }
        catch (Exception ex)
        {
            return strurl;
        }
    }


    public void reset()
    {

        txttitle.Text = "";
        ddlprojectcat.SelectedIndex = 0;

    }
    protected void lnkreset_Click(object sender, EventArgs e)
    {
        //txttitle.Text = "";
        reset();
        loaddata(1);
        //loadallprojectadd();
    }


}



