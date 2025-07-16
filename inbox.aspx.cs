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

public partial class inbox : System.Web.UI.Page
{
    private SqlDataAdapter da;
    int mailid;
    Boolean chk = false;
    public static int deleteCount = 0;
    public string username = "";
    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind(); 
        if (Page.User.Identity.IsAuthenticated)
        {
            username = Page.User.Identity.Name;
        
        string flag = Convert.ToString(Request.QueryString["flag"]);
        if (!Page.IsPostBack)
        {
            txtfromdate.Attributes.Add("readonly", "readonly");
            txttodate.Attributes.Add("readonly", "readonly");
            loadgrid();
        }
        if (flag == "Y")
        {
            // dispalayTd("<b>Product added successfully.</b>", true);
            lblmsg1.Visible = true;
            lblmsg1.Text = "<font color='green'><b>Message sent successfully</b></font>";
        }
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnURL=" + Request.Url.Fragment);
        }
    }
    protected void gvsendmail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Find the checkbox control in header and add an attribute
            ((CheckBox)e.Row.FindControl("cbSelectAll")).Attributes.Add("onclick", "javascript:SelectAll('" +
                     ((CheckBox)e.Row.FindControl("cbSelectAll")).ClientID + "')");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblUnreadFlag = (Label)e.Row.FindControl("lblFlag");
            Label lblFrom = (Label)e.Row.FindControl("lnbtnedit1");
            Label lblDate = (Label)e.Row.FindControl("lblDate");
            LinkButton lblSubject = (LinkButton)e.Row.FindControl("lbltitle");
            LinkButton lnbtnedit = (LinkButton)e.Row.FindControl("lnbtnedit");

            string UnreadFlag = Convert.ToString(lblUnreadFlag.Text.Trim());
            if (UnreadFlag == "Y")
            {
                lblFrom.Attributes.Add("style", "font-weight:Bold;");
                lblSubject.Attributes.Add("style", "font-weight:Bold;");
                lblDate.Attributes.Add("style", "font-weight:Bold;");
                lnbtnedit.Attributes.Add("style", "font-weight:Bold;");
            }
        }


    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        string search = commonclass.GetSafeSearchString(txtsearch.Text.ToString().Trim());
        string fromdate, todate;
        //fromdate = commonclass.GetSafeIDFromURL(txtfromdate.Text.ToString().Trim());
        //todate = commonclass.GetSafeIDFromURL(txttodate.Text.ToString().Trim());

        fromdate = txtfromdate.Text.ToString().Trim();
        todate = txttodate.Text.ToString().Trim();
        MembershipUser user = Membership.GetUser(username);
        string frommail = user.UserName;
        DataTable dtsearch = new DataTable();
        dtsearch = classmailsend.searchmessageloginbox1(search, fromdate, todate, frommail);
        if (dtsearch.Rows.Count > 0)
        {
            gvsendmail.Visible = true;
            gvsendmail.DataSource = dtsearch;
            gvsendmail.DataBind();
            alter();
            ViewState["datatable"] = dtsearch;
            divsearch.Visible = false;

        }
        else
        {
            gvsendmail.Visible = false;
            divsearch.Visible = true;
            lblmessage.Text = "<font style='color:Red;display:inline-block;font-size:16px;margin:10px 0 0;'>No message found!</font>";
        }
    }
    protected void gvsendmail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToLower() == "edititem")
            {
                mailid = Convert.ToInt32(e.CommandArgument.ToString());
                bool flag = classmailsend.UpdateUnreadStatus(mailid, "N");
                Response.Redirect(string.Format(ReturnUrl("sitepathmain") + "composemail.aspx?ID={0}", UrlRewritingVM.Encrypt(mailid.ToString())));
            }
            else if (e.CommandName.ToLower() == "deleteitem")
            {
                mailid = Convert.ToInt32(e.CommandArgument);
                classmailsend.UpdateUnreadStatus(mailid, "N");
                classmailsend.deletemessagelog(mailid);
                loadgrid();
            }
            else if (e.CommandName.ToLower().Trim() == "replyitem")
            {
                mailid = Convert.ToInt32(e.CommandArgument.ToString());
                bool flag = classmailsend.UpdateUnreadStatus(mailid, "N");
                Response.Redirect(string.Format(ReturnUrl("sitepathmain") + "composemail.aspx?mid={0}", UrlRewritingVM.Encrypt(mailid.ToString())));
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void gvsendmail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dt = (DataTable)ViewState["datatable"];
        gvsendmail.PageIndex = e.NewPageIndex;
        string s = e.NewPageIndex.ToString();
        gvsendmail.DataSource = dt;
        gvsendmail.DataBind();
        alter();
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
    private void dispalayTd(string mess, bool val)
    {
        foreach (HtmlTableRow trow in mytable.Rows)
        {
            HtmlTableCell mytd = (HtmlTableCell)trow.FindControl("mytd");
            lblmessagenew.Text = mess;
            mytd.Visible = val;

        }
    }
    protected void ddlstmore_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
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
                lblmessage.Text = "<font style=\"color:Red\">Please select message to change status</font>";
                ddlstmore.SelectedIndex = 0;
            }
            else
            {
                if (ddlstmore.SelectedIndex!=0)
                {
                    if (ddlstmore.SelectedValue.ToString().Trim()=="D")
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
                    else if (ddlstmore.SelectedValue.ToString().Trim() == "Y")
                    {
                        for (int i = 0; i < gvsendmail.Rows.Count; i++)
                        {
                            GridViewRow rows;
                            rows = gvsendmail.Rows[i];
                            mailid = Convert.ToInt32(gvsendmail.DataKeys[i].Value);
                            CheckBox chkaprove = (CheckBox)rows.FindControl("CheckBox1");
                            if (((CheckBox)rows.FindControl("CheckBox1")).Checked == true)
                            {
                                int ID;
                                ID = Convert.ToInt32(gvsendmail.DataKeys[i].Value.ToString());
                                bool flag = classmailsend.UpdateUnreadStatus(ID, "Y");
                            }
                        }
                        divsearch.Visible = true;
                        lblmessage.Text = "<font style=\"color:Green\">Message status has been changed successfully</font>";
                        ddlstmore.SelectedIndex = 0;
                    }
                    else if (ddlstmore.SelectedValue.ToString().Trim() == "N")
                    {
                        for (int i = 0; i < gvsendmail.Rows.Count; i++)
                        {
                            GridViewRow rows;
                            rows = gvsendmail.Rows[i];
                            mailid = Convert.ToInt32(gvsendmail.DataKeys[i].Value);
                            CheckBox chkaprove = (CheckBox)rows.FindControl("CheckBox1");
                            if (((CheckBox)rows.FindControl("CheckBox1")).Checked == true)
                            {
                                int ID;
                                ID = Convert.ToInt32(gvsendmail.DataKeys[i].Value.ToString());
                                bool flag = classmailsend.UpdateUnreadStatus(ID, "N");

                            }
                        }
                        divsearch.Visible = true;
                        lblmessage.Text = "<font style=\"color:Green\">Message status has been changed successfully</font>";
                        ddlstmore.SelectedIndex = 0;
                    }
                    else
                    {

                    }
                     
                }
            }
            
            loadgrid();
        }

        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    public void loadgrid()
    {
        ddllabels.SelectedIndex = 1;
        MembershipUser user = Membership.GetUser(username);
        string frommail = user.UserName;
        DataTable dt = new DataTable();
        dt = classmailsend.messageloglist(frommail);
        if (dt.Rows.Count > 0)
        {
            gvsendmail.Visible = true;
            divsearch.Visible = false;
            ViewState["datatable"] = dt;
            gvsendmail.DataSource = dt;
            gvsendmail.DataBind();
            trsearch.Visible = true;
            alter();
            lblnomsg.Visible = false;
        }
        else
        {
            gvsendmail.Visible = false;
            ddlstmore.Visible = false;
            divsearch.Visible = true;
            lblmessage.Text = "<font style='color:Red;display:inline-block;font-size:16px;margin:100px 0 0;'>No message found!</font>";
            tblsearch.Visible = false;
            lblmessage11.Visible = false;
            btnDelte.Visible = false;
        }
    }
    public void alter()
    {
        if (gvsendmail.Rows.Count > 0)
        {
            for(int i=0;i<gvsendmail.Rows.Count;i++)
            {
                LinkButton lnktitle = (LinkButton)gvsendmail.Rows[i].FindControl("lbltitle");
                Label lblmsg = (Label)gvsendmail.Rows[i].FindControl("lblmsg");
                string title = lnktitle.Text.ToString().Trim();
                string messgae=lblmsg.Text.ToString().Trim();
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
    public string getcalimg()
    {
        string strurl = "";
        try
        {
            strurl = ReturnUrl("medaipath") + "icons/timepng"; 
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
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
            if(y2==y1)
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
        if(ddllabels.SelectedIndex==0)
        {
            Response.Redirect(ReturnUrl("sitepathmain")+"composemail.aspx");
        }
        else if(ddllabels.SelectedIndex==2)
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