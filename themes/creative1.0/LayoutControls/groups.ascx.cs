using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class themes_creative : System.Web.UI.UserControl
{
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Page.User.Identity.IsAuthenticated == true)
        {
            loadgrpbyuser(Page.User.Identity.Name.ToString());
        }
        else
        {
            loadgrp();
        }
    }
    public void loadgrp()
    {
        try
        {
            DataTable grp = classnews.topgrpList();
            if (grp.Rows.Count > 0)
            {
                if (grp.Rows.Count > 4)
                {
                    lnkview.Visible = true;
                }
                else
                {
                    lnkview.Visible = false;
                }
                pnlgrp.Visible = true;
                rptgrp.DataSource = grp;
                rptgrp.DataBind();
                alter();
            }
            else
            {
                pnlgrp.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    public void alter()
    {
        if(rptgrp.Items.Count>0)
        {
            for(int i=0;i<rptgrp.Items.Count;i++)
            {
                Label lblgid = (Label)rptgrp.Items[i].FindControl("lblgid");
                Label lblcount = (Label)rptgrp.Items[i].FindControl("lblcount");
                if(lblgid.Text.ToString().Trim()!="")
                {
                    DataTable review = classreviews.getmembercounts(Convert.ToInt32(lblgid.Text.Trim().ToString()));
                    if (review.Rows.Count > 0)
                    {
                        lblcount.Text = "(" + review.Rows[0]["member"].ToString() + "+)";
                    }
                    else
                    {

                    }
                }

            }

        }
        
    }
    public string getgroup(object grpid)
    {
        string strurl = "";
        try
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                strurl = ReturnUrl("sitepathmain") + "grouppost/" + UrlRewritingVM.Encrypt(grpid.ToString());
                return strurl;
            }
            else
            {
                strurl = ReturnUrl("sitepathmain") + "login.aspx";
                return strurl;
            }

        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
    public void loadgrpbyuser(string username)
    {
        try
        {
            DataTable grp = classnews.grpListbyuser(username);
            if (grp.Rows.Count > 0)
            {
                pnlgrp.Visible = true;
                if (grp.Rows.Count > 4)
                {
                    lnkview.Visible = true;
                }
                else
                {
                    lnkview.Visible = false;
                }
                rptgrp.DataSource = grp;
                rptgrp.DataBind();
            }
            else
            {
                pnlgrp.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    protected void lnkview_Click(object sender, EventArgs e)
    {
        DataSet ds = classaddress.GetuserId(Page.User.Identity.Name.ToString());
        Response.Redirect(ReturnUrl("sitepathmain") + "groups/" + UrlRewritingVM.Encrypt(ds.Tables[0].Rows[0]["indexid"].ToString()));
    }
}