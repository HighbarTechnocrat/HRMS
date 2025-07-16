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
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;


public partial class newsdetail : System.Web.UI.Page
{
   public static string searchtype = "";
   public static string user = "";
   public static string emailid = "";
   public static string fname = "";
   public static string loc = "";
   public static string dept = "";
   public static string subdept = "";
   public static string desg = "";
   public static string mobile="";
   public static int contact;
   public static int indexid;
   public static string file = "";
   public static int PageSize=15,RecordCount;
   public static int PageSize1 = 15, RecordCount1;
   public static int pgi=1;
   public static int pgi1=1;

    //all contact
   public static int pageCount;
   public static int maxpage = 5;
   public static double dblmainpg;
   public static int maxpgcount;
   public static int maxpg = 1;

    //star contact
   public static int pageCount1;
   public static double dblmainpg1;
   public static int maxpgcount1;
   public static int maxpg1 = 1;
   

   public string contactno;

    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;}
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Page.User.Identity.IsAuthenticated)
        {
             txtallsearch.Focus();
            user = Page.User.Identity.Name.ToString().Trim();
             if (!Page.IsPostBack)
             {
                 maxpg = 1;
                 maxpg1 = 1;
                 pgi = 1; 
                 pgi1 = 1;
                 bindstarcontact(1);
                 bindallcontact(1);
             }
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnURL=" + Request.Url.Fragment);
        }
    }
    public string getuserimage(object imagepath)
    {
        string strurl = "";
        try
        {
            if (imagepath.ToString().Length > 0)
            {
                strurl = ReturnUrl("sitepath") + "images/profile55x55/" + imagepath.ToString();
            }
            else
            {
                strurl = ReturnUrl("sitepath") + "images/profile55x55/noimage1.png";
            }
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
    public void bindstarcontact(int PageIndex1)
    {
        try
        {
            rptPager1.Visible = false;
            
            DataTable dt = classcontact.getstarcontactFullsearch(user, fname, fname, emailid, mobile,loc,dept,subdept,desg, PageIndex1, PageSize1, out RecordCount1);
            if (dt.Rows.Count > 0)
            {
                pnlstar.Visible = true;
                pnlstar1.Visible = true;
                lblmsg.Visible = false;
                rptstar.DataSource = dt;
                rptstar.DataBind();
                if (rptstar.Items.Count > 0)
                {
                    for (int j = 0; j < rptstar.Items.Count; j++)
                    {
                        Label followbtn1 = (Label)rptstar.Items[j].FindControl("followbtn1");
                        LinkButton lnkfollow1 = (LinkButton)rptstar.Items[j].FindControl("lnkfollow1");
                        DataTable dt1 = classreviews.getfollower(Page.User.Identity.Name, lnkfollow1.CommandArgument.ToString());
                        if (dt1.Rows.Count > 0)
                        {
                            lnkfollow1.ToolTip = "Unfollow";
                            followbtn1.Text = "<i class='fa fa-user-plus followcolor' aria-hidden='true'></i>";
                        }
                        else
                        {
                            lnkfollow1.ToolTip = "Follow";
                            followbtn1.Text = "<i class='fa fa-user-plus' aria-hidden='true'></i>";
                        }
                    }
                }
                alter2();
                if (maxpg1 == 1)
                {
                    lnkprev.Visible = false;
                    this.PopulatePager1(RecordCount1, PageIndex1);
                }
                else
                {
                    this.maxpagerstar(RecordCount1, PageIndex1, maxpg1);
                }
            }
            else
            {
                if(pgi1>1)
                {
                   
                    if (pageCount1 == (maxpg1 * maxpage) - (maxpage - 1))
                    {
                        pageCount1 = pageCount1 - 1;
                        maxpg1 = maxpg1 - 1;
                        pgi1 = maxpg1 * maxpage;
                        bindstarcontact(pgi1);
                    }
                    else
                    {
                        pgi1 = pgi1 - 1;
                        bindstarcontact(pgi1);
                    }
                }
                else if (fname != "" && fname != null)
                {
                    pnlstar1.Visible = false;
                    lblmsg.Visible = true;
                    lblmsg.Text = "No user exist with such Name...";
                }
                else if (emailid != "" && emailid != null)
                {
                    pnlstar1.Visible = false;
                    lblmsg.Visible = true;
                    lblmsg.Text = "No user exist with such Email-ID...";
                }
                else if (loc != "" && loc != null)
                {
                    pnlstar1.Visible = false;
                    lblmsg.Visible = true;
                    lblmsg.Text = "No user exist with such Location...";
                }
                else if (dept != "" && dept != null)
                {
                    pnlstar1.Visible = false;
                    lblmsg.Visible = true;
                    lblmsg.Text = "No user exist with such Department...";
                }
                else if (desg != "" && desg != null)
                {
                    pnlstar1.Visible = false;
                    lblmsg.Visible = true;
                    lblmsg.Text = "No user exist with such Designation...";
                }
                else
                {
                    pnlstar.Visible = false;
                }
            }
                    
        }
        catch (Exception ex)
        {
            
        }
    }
    public void bindallcontact(int PageIndex)
    {
        try
        {
            rptPager.Visible = false;
            //lnkprev1.Visible = false;
            //lnknxt1.Visible = false;
            
            DataTable dt = classcontact.getAllcontactFullsearch(user, fname, fname, emailid, mobile, loc, dept, subdept, desg, PageIndex, PageSize, out RecordCount);
            if(dt.Rows.Count>0)
            {
                pnlallcntc.Visible = true;
                pnlallcntc1.Visible = true;
                lblmsg1.Visible = false;
                rptall.DataSource = dt;
                rptall.DataBind();
                if (rptall.Items.Count > 0)
                {
                    for (int j = 0; j <= rptall.Items.Count - 1; j++)
                    {
                        //Image followbtn = (Image)rptall.Items[j].FindControl("followbtn");
                        Label followbtn = (Label)rptall.Items[j].FindControl("followbtn");
                        LinkButton lnkfollow = (LinkButton)rptall.Items[j].FindControl("lnkfollow");
                        DataTable dt1 = classreviews.getfollower(Page.User.Identity.Name, lnkfollow.CommandArgument.ToString());
                        if (dt1.Rows.Count > 0)
                        {
                            lnkfollow.ToolTip = "Unfollow";
                            followbtn.Text = "<i class='fa fa-user-plus followcolor' aria-hidden='true'></i>";
                        }
                        else
                        {
                            lnkfollow.ToolTip = "Follow";
                            followbtn.Text = "<i class='fa fa-user-plus' aria-hidden='true'></i>";
                        }
                    }
                }
                alter();
                if(maxpg==1)
                {
                    lnkprev1.Visible = false;
                    this.PopulatePager(RecordCount, PageIndex);
                }
                else
                {
                    this.maxpagerall(RecordCount, PageIndex,maxpg);
                }
            }
            else
            {
                if(pgi>1)
                {
                   
                    if (pageCount == (maxpg * maxpage) - (maxpage - 1))
                    {
                        pageCount = pageCount - 1;
                        maxpg = maxpg - 1;
                        pgi = maxpg * maxpage;
                        bindallcontact(pgi);
                    }
                    else
                    {
                        pgi = pgi - 1;
                        bindallcontact(pgi);
                    }
                }
                else if (fname != "" && fname != null)
                {
                    pnlallcntc1.Visible = false;
                    lblmsg1.Visible = true;
                    lblmsg1.Text = "No user exist with such Name...";
                }
                else if (emailid != "" && emailid != null)
                {
                    pnlallcntc1.Visible = false;
                    lblmsg1.Visible = true;
                    lblmsg1.Text = "No user exist with such Email-ID...";
                }
                else if (loc != "" && loc != null)
                {
                    pnlallcntc1.Visible = false;
                    lblmsg1.Visible = true;
                    lblmsg1.Text = "No user exist with such Location...";
                }
                else if (dept != "" && dept != null)
                {
                    pnlallcntc1.Visible = false;
                    lblmsg1.Visible = true;
                    lblmsg1.Text = "No user exist with such Department...";
                }
                else if (desg != "" && desg != null)
                {
                    pnlallcntc1.Visible = false;
                    lblmsg1.Visible = true;
                    lblmsg1.Text = "No user exist with such Designation...";
                }
                else
                {
                    pnlallcntc.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void alter()
    {
        if(rptall.Items.Count>0)
        {
            for(int i=0;i<rptall.Items.Count;i++)
            {
                LinkButton lnk = (LinkButton)rptall.Items[i].FindControl("lnkusername");
                lnk.PostBackUrl = ReturnUrl("sitepathmain").ToString() + "profile/" + UrlRewritingVM.Encrypt(lnk.CommandArgument.ToString());
            }
        }
    }
    public void alter2()
    {
        if (rptstar.Items.Count > 0)
        {
            for (int i = 0; i < rptstar.Items.Count; i++)
            {
                LinkButton lnk = (LinkButton)rptstar.Items[i].FindControl("lnkusername");
                lnk.PostBackUrl = ReturnUrl("sitepathmain").ToString() + "profile/" + UrlRewritingVM.Encrypt(lnk.CommandArgument.ToString());
            }
        }
    }
    protected void rptall_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        bool iserror = false;
        try
        {
            if (e.CommandName.ToLower() == "addstar")
            {
                if (Page.User.Identity.IsAuthenticated)
                {
                    iserror = classcontact.createcontact(e.CommandArgument.ToString(), Page.User.Identity.Name);
                    bindstarcontact(pgi1);
                    bindallcontact(pgi);
                }
            }
            else if (e.CommandName.ToLower() == "follow")
            {
                if (Page.User.Identity.IsAuthenticated)
                {
                    classreviews.insertdeletefollowing(Page.User.Identity.Name, e.CommandArgument.ToString());
                    bindstarcontact(pgi1);
                    bindallcontact(pgi);
                }
            }
            else if (e.CommandName.ToLower() == "sentmsg")
            {
                if (Page.User.Identity.IsAuthenticated)
                {
                    DataTable dt = classaddress.getindexidbyusername(e.CommandArgument.ToString());
                    Response.Redirect(ReturnUrl("sitepathmain") + "composemessage/" + UrlRewritingVM.Encrypt(dt.Rows[0]["indexid"].ToString()));
                }
            }

        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    protected void rptstar_ItemCommand(object source, RepeaterCommandEventArgs e1)
    {
        
        try
        {
            if (e1.CommandName.ToLower() == "deletestar")
            {
                if (Page.User.Identity.IsAuthenticated)
                {
                    classcontact.deletestarcontact(e1.CommandArgument.ToString());
                    bindstarcontact(pgi1);
                    bindallcontact(pgi);

                }
            }
            else if (e1.CommandName.ToLower() == "follow1")
            {
                if (Page.User.Identity.IsAuthenticated)
                {
                    classreviews.insertdeletefollowing(Page.User.Identity.Name, e1.CommandArgument.ToString());
                    bindstarcontact(pgi1);
                    bindallcontact(pgi);
                }
            }
            else if (e1.CommandName.ToLower() == "sentmsg")
            {
                if (Page.User.Identity.IsAuthenticated)
                {
                    DataTable dt = classaddress.getindexidbyusername(e1.CommandArgument.ToString());
                    Response.Redirect(ReturnUrl("sitepathmain") + "composemessage/" + UrlRewritingVM.Encrypt(dt.Rows[0]["indexid"].ToString()));
                }
            }

        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    private void PopulatePager(int recordCount, int currentPage)// all contact
    {
        //lnknxt1.Visible = false;
        //lnkprev1.Visible = false;
        double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
        pageCount = (int)Math.Ceiling(dblPageCount);
        dblmainpg = (double)((double)pageCount / Convert.ToDouble(maxpage));
        maxpgcount = (int)Math.Ceiling(dblmainpg);
        int pgno = 0;
        List<ListItem> pages = new List<ListItem>();
        if (pageCount > 0)
        {
            for (int i = 1; i <= maxpage; i++)
            {
                if (pgno == 0)
                {
                    if (maxpg * maxpage < pageCount)
                    {
                        lnknxt1.Visible = true;
                    }
                    pgno = (maxpg * maxpage) - (maxpage - 1);
                    pages.Add(new ListItem(pgno.ToString(), pgno.ToString(), pgno != currentPage));
                }
                else
                {
                    pgno = pgno + 1;
                    if (pgno <= pageCount)
                    {
                        pages.Add(new ListItem(pgno.ToString(), pgno.ToString(), pgno != currentPage));
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (pageCount == 1)
            {
                rptPager.Visible = false;
            }
            else
            {
                rptPager.Visible = true;
            }
        }
        else
        {
            rptPager.Visible = false;
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
    }
    private void maxpagerall(int recordCount,int currentPage, int mpg)// allcontact
    {
        rptPager.Dispose();
        rptPager.Visible = false;
        if(maxpg > 1)
        {
            lnkprev1.Visible = true;
        }

        if (maxpg == 1 || maxpg < 1)
        {
            maxpg = 1;
            lnkprev1.Visible = false;
        }

        int pgno=0;
        List<ListItem> pages = new List<ListItem>();
        for (int i = 1; i <= maxpage; i++)
        {
            if(pgno==0)
            {
                if (maxpg * maxpage < pageCount)
                {
                    lnknxt1.Visible = true;
                }
                pgno = (maxpg * maxpage )- (maxpage-1);
                pages.Add(new ListItem(pgno.ToString(), pgno.ToString(), pgno != currentPage));
            }
            else
            {
                pgno=pgno+1;
                if(pgno<=pageCount)
                {
                    pages.Add(new ListItem(pgno.ToString(), pgno.ToString(), pgno != currentPage));
                }
                else
                {
                    break;
                }
            }
            rptPager.Visible = true;
        }
        if (maxpgcount == maxpg || maxpgcount == 1)
        {
            lnknxt1.Visible = false;
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
    }
    private void PopulatePager1(int recordCount, int currentPage)// star contact
    {
        //lnknxt.Visible = false;
        //lnkprev.Visible = false;
        
        double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize1));
        pageCount1 = (int)Math.Ceiling(dblPageCount);
        dblmainpg1 = (double)((double)pageCount1 / Convert.ToDouble(maxpage));
        maxpgcount1 = (int)Math.Ceiling(dblmainpg1);
        int pgno = 0;
        List<ListItem> pages = new List<ListItem>();
        if (pageCount1 > 0)
        {
            for (int i = 1; i <= maxpage; i++)
            {
                if (pgno == 0)
                {
                    if (maxpg1 * maxpage < pageCount1)
                    {
                        lnknxt.Visible = true;
                    }
                    pgno = (maxpg1 * maxpage) - (maxpage - 1);
                    pages.Add(new ListItem(pgno.ToString(), pgno.ToString(), pgno != currentPage));
                }
                else
                {
                    pgno = pgno + 1;
                    if (pgno <= pageCount1)
                    {
                        pages.Add(new ListItem(pgno.ToString(), pgno.ToString(), pgno != currentPage));
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (pageCount1 == 1)
            {
                rptPager1.Visible = false;
            }
            else
            {
                rptPager1.Visible = true;
            }
        }
        else
        {
            rptPager1.Visible = false;
        }
        rptPager1.DataSource = pages;
        rptPager1.DataBind();
    }
    private void maxpagerstar(int recordCount,int currentPage, int mpg)// star contact
    {
        rptPager1.Dispose();
        rptPager1.Visible = true;
        if (maxpg1 > 1)
        {
            lnkprev.Visible = true;
        }

        if (maxpg1 == 1 || maxpg1 < 1)
        {
            maxpg1 = 1;
            lnkprev.Visible = false;
        }
        int pgno = 0;
        List<ListItem> pages = new List<ListItem>();
        for (int i = 1; i <= maxpage; i++)
        {
            if (pgno == 0)
            {
                if (maxpg1 * maxpage < pageCount1)
                {
                    lnknxt.Visible = true;
                }
                pgno = (maxpg1 * maxpage) - (maxpage - 1);
                pages.Add(new ListItem(pgno.ToString(), pgno.ToString(), pgno != currentPage));
            }
            else
            {
                pgno = pgno + 1;
                if (pgno <= pageCount1)
                {
                    pages.Add(new ListItem(pgno.ToString(), pgno.ToString(), pgno != currentPage));
                }
                else
                {
                    break;
                }
            }
        }
        if (maxpgcount1 == maxpg1 || maxpgcount1 == 1 )
        {
            lnknxt.Visible = false;
        }
        rptPager1.DataSource = pages;
        rptPager1.DataBind();
    }
    protected void Page_Changed(object sender, EventArgs e)// all contact
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        pgi = pageIndex;
        this.bindallcontact(pgi);
        bindstarcontact(pgi1);
    }
    protected void lnkPage1_Click(object sender, EventArgs e)// star contact
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        pgi1 = pageIndex;
        this.bindstarcontact(pgi1);
        bindallcontact(pgi);
    }
    protected void lnkstarcontact_Click(object sender, EventArgs e)
    {
        search();
        bindstarcontact(pgi1);
        bindallcontact(pgi);
    }
    
    public void search()
    {
        
       searchtype = ddlserch.SelectedItem.Text.ToString().Trim();
        if (searchtype == "Name")
        {
            if (txtstarsearch.Text.Length > 0 && txtstarsearch.Text.ToString() != "")
            {
                fname = commonclass.GetSafeSearchString(txtstarsearch.Text.ToString());
                emailid = "";
                loc = "";
                dept = "";
                desg = "";
            }
            else
            {
                reset();
            }
        }
        else if (searchtype == "Email-ID")
        {
            if (txtstarsearch.Text.Length > 0 && txtstarsearch.Text.ToString() != "")
            {
                emailid = commonclass.GetSafeSearchString(txtstarsearch.Text.ToString());
                fname = "";
                loc = "";
                dept = "";
                desg = "";
            }
            else
            {
                reset();
            }
        }
        else if (searchtype == "Location")
        {
            if (txtstarsearch.Text.Length > 0 && txtstarsearch.Text.ToString() != "")
            {
                loc = commonclass.GetSafeSearchString(txtstarsearch.Text.ToString());
                emailid = "";
                desg = "";
                dept = "";
                fname = "";
            }
            else
            {
                reset();
            }
        }
        else if (searchtype == "Department")
        {
            if (txtstarsearch.Text.Length > 0 && txtstarsearch.Text.ToString() != "")
            {
                dept = commonclass.GetSafeSearchString(txtstarsearch.Text.ToString());
                fname = "";
                loc = "";
                emailid = "";
                desg = "";
            }
            else
            {
                reset();
            }
        }
        else if (searchtype == "Designation")
        {
            if (txtstarsearch.Text.Length > 0 && txtstarsearch.Text.ToString() != "")
            {
                desg = commonclass.GetSafeSearchString(txtstarsearch.Text.ToString());
                emailid = "";
                fname = "";
                loc = "";
                dept = "";
            }
            else
            {
                reset();
            }
        }
    }
    

  

    protected void lnkadllcontact_Click(object sender, EventArgs e)
    {
        bindallcontact(pgi);
        bindstarcontact(pgi1);
    }
    protected void lnknxt1_Click(object sender, EventArgs e)//allcontact
    {
        int pgno;
        maxpg = maxpg + 1;
        pgno = (maxpg * maxpage) - (maxpage - 1);
        pgi = pgno;
        bindallcontact(pgno);
        bindstarcontact(1);
    }
    protected void lnkprev1_Click(object sender, EventArgs e)//allcontact
    {
        int pgno;
        maxpg = maxpg - 1;
        pgno = (maxpg * maxpage) - (maxpage - 1);
        pgi = pgno;
        bindallcontact(pgno);
        bindstarcontact(1);
    }
    protected void lnkprev_Click(object sender, EventArgs e)//star contact
    {
        int pgno;
        maxpg1 = maxpg1 - 1;
        pgno = (maxpg1 * maxpage) - (maxpage - 1);
        pgi1 = pgno;
        bindstarcontact(pgno);
        bindallcontact(1);
    }
    protected void lnknxt_Click(object sender, EventArgs e)//starcontact
    {
        int pgno;
        maxpg1 = maxpg1 + 1;
        pgno = (maxpg1 * maxpage) - (maxpage - 1);
        pgi1 = pgno;
        bindstarcontact(pgno);
        bindallcontact(1);
    }
  
    protected void lnkreset_Click(object sender, EventArgs e)
    {
        reset();
        bindstarcontact(1);
        bindallcontact(1);
    }
    public void reset()
    {
        txtallsearch.Text = "";
        txtstarsearch.Text = "";
        fname = "";
        emailid = "";
        loc = "";
        dept = "";
        desg = "";
        ddlserch.SelectedIndex = 0;
        maxpg = 1;
        maxpg1 = 1;
        pgi = 1;
        pgi1 = 1;
    }
}
