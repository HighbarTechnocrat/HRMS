using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class outbox : System.Web.UI.Page
{
    int mailid;
    Boolean chk = false;
    public static int deleteCount = 0;
    public string username = "";
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;}
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Page.User.Identity.IsAuthenticated)
        {
            username = Page.User.Identity.Name;
            if (!Page.IsPostBack)
            {
                txtfromdate.Attributes.Add("readonly", "readonly");
                txttodate.Attributes.Add("readonly", "readonly");
                loadgrid();
            }
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnURL=" + Request.Url.Fragment);
        }
        
    }
    public void loadgrid()
    {
        ddllabels.SelectedIndex = 2;
        MembershipUser user = Membership.GetUser(username);
        string tomail = user.UserName;
        DataTable dt = new DataTable();
        dt = classmailsend.msgloglistreceiver(tomail);        
        if (dt.Rows.Count > 0)
        {
            ViewState["datatable"] = dt;
            gvsendmail.DataSource = dt;
            gvsendmail.DataBind();
            gvsendmail.Visible = true;
            btnDelte.Visible = true;
            alter();
        }
        else
        {
            gvsendmail.Visible = false;
            divsearch.Visible = true;
            lblmessage.Text = "<font style='color:Red;display:inline-block;font-size:16px;margin:10px 0 0;'>No message found!</font>";
            btnDelte.Visible = false;
            tblsearch.Visible = false;
        }
    }
    public void alter()
    {
        if (gvsendmail.Rows.Count > 0)
        {
            for (int i = 0; i < gvsendmail.Rows.Count; i++)
            {
                Label lnktitle = (Label)gvsendmail.Rows[i].FindControl("lbltitle");
                Label lblmsg = (Label)gvsendmail.Rows[i].FindControl("lblmsg");
                string title = lnktitle.Text.ToString().Trim();
                string messgae = lblmsg.Text.ToString().Trim();
                if (title.Length > 80)
                {
                    lnktitle.Text = title.Substring(0, 80) + "...";
                }
                else
                {
                    lnktitle.Text = title;
                }
                if (messgae.Length > 80)
                {
                    if (title.Length > 80)
                    {
                        lnktitle.Text = lnktitle.Text + "<span class='msgdesc'> - " + messgae.Substring(0, 40) + "...</span>";
                    }
                    else
                    {
                        lnktitle.Text = lnktitle.Text + "<span class='msgdesc'> - " + messgae.Substring(0, 80) + "...</span>";
                    }

                }
                else
                {
                    lnktitle.Text = lnktitle.Text + "<span class='msgdesc'> - " + messgae + "</span>";
                }
            }
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        string search = commonclass.GetSafeSearchString(txtsearch.Text.ToString().Trim());
        string fromdate, todate;
        //commented by Hameed
        //fromdate = commonclass.GetSafeIDFromURL(txtfromdate.Text.ToString().Trim());
        //todate = commonclass.GetSafeIDFromURL(txttodate.Text.ToString().Trim());


        //added by Hameed
        fromdate = txtfromdate.Text.ToString().Trim();
        todate = txttodate.Text.ToString().Trim();

        MembershipUser user = Membership.GetUser(username);
        string tomail = user.UserName;

        DataTable dt = new DataTable();
        dt = classmailsend.searchmessagelogoutbox1(search, fromdate, todate, tomail);
        
        if (dt.Rows.Count > 0)
        {
            ViewState["datatable"] = dt;
            gvsendmail.DataSource = dt;
            gvsendmail.DataBind();
            gvsendmail.Visible = true;
            alter();
            divsearch.Visible = false;
            btnDelte.Visible = true;
        }
        else
        {
            divsearch.Visible = true;
            gvsendmail.Visible = false;
            lblmessage.Text = "<font style='color:Red;display:inline-block;font-size:16px;margin:100px 0 0;'>No message found!</font>";
            btnDelte.Visible = false;
        }
    }
    protected void gvsendmail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dt = (DataTable)ViewState["datatable"];
        gvsendmail.PageIndex = e.NewPageIndex;
        string s = e.NewPageIndex.ToString();
        gvsendmail.DataSource = dt;
        gvsendmail.DataBind();
    }
    protected void gvsendmail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Find the checkbox control in header and add an attribute
            ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                    ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");

        }
    }
    protected void gvvendor_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "deleteitem")
        {
            mailid = Convert.ToInt32(e.CommandArgument);
            classmailsend.deletemessagelog(mailid);
            loadgrid();
        }
        else if (e.CommandName.ToLower() == "replyitem")
        {
            mailid = Convert.ToInt32(e.CommandArgument);
            Response.Redirect(string.Format(ReturnUrl("sitepathmain") + "composemail.aspx?rid={0}", UrlRewritingVM.Encrypt(mailid.ToString())));
        }
    }
    protected void btnDelte_Click(object sender, EventArgs e)
    {
        int count = 0, count1 = 0;

        for (int i = 0; i < gvsendmail.Rows.Count; i++)
        {
            GridViewRow rows;
            rows = gvsendmail.Rows[i];
            if (((CheckBox)rows.FindControl("CheckBox1")).Checked == true)
            {
                count++;
            }
        }
        if (count == 0)
        {
            divsearch.Visible = true;
            lblmessage.Text = "<font style=\"color:Red\">Please select message to delete</font>";
            lblmessage.Focus();
        }
        else
        {
            for (int i = 0; i < gvsendmail.Rows.Count; i++)
            {
                GridViewRow rows;
                rows = gvsendmail.Rows[i];
                mailid = Convert.ToInt32(gvsendmail.DataKeys[i].Value);
                CheckBox chkaprove = (CheckBox)rows.FindControl("CheckBox1");
                if (((CheckBox)rows.FindControl("CheckBox1")).Checked == true)
                {
                    classmailsend.deletemessagelog(mailid);
                    divsearch.Visible = true;
                    lblmessage.Text = "<font style=\"color:Green\">Message has been deleted successfully</font>";
                }
            }
        }
        loadgrid();
    }
    public string getdate(object date)
    {
        string strurl = "";
        try
        {
            DateTime dat = Convert.ToDateTime(date.ToString());
            DateTime dat2 = DateTime.Now;
            TimeSpan diff = dat2 - dat;
            int y1, y2;
            y1 = Convert.ToInt32(dat.ToString("yyyy"));
            y2 = Convert.ToInt32(dat2.ToString("yyyy"));
            int days = Convert.ToInt32(diff.Days);
            int hours = Convert.ToInt32(diff.Hours);
            int minutes = Convert.ToInt32(diff.Minutes);
            int seconds = Convert.ToInt32(diff.Seconds);
            if (y2 == y1)
            {
                if (days > 0)
                {
                    strurl = strurl + dat.ToString("dd MMM");
                }
                else if (hours > 0)
                {
                    strurl = strurl + dat.ToString("t");
                }
                else if (minutes > 0)
                {
                    strurl = strurl + dat.ToString("t");
                }
                else if (seconds > 0)
                {
                    strurl = strurl + "00:" + seconds.ToString() + " sec";
                }
            }
            else
            {
                strurl = strurl + dat.ToString("d");
            }


            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
    protected void ddllabels_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddllabels.SelectedIndex == 0)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "composemail.aspx");
        }
        else if (ddllabels.SelectedIndex == 1)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "inbox.aspx");
        }
        else if (ddllabels.SelectedIndex == 2)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "outbox.aspx");
        }

    }
    protected void lnkreset_Click(object sender, EventArgs e)
    {
        txtsearch.Text = "";
        txttodate.Text = "";
        txtfromdate.Text = "";
        loadgrid();
    }
}