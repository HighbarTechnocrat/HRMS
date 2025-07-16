using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.IO;

public partial class themes_creative1_LayoutControls_profile_followers : System.Web.UI.UserControl
{
    public static int userid;
    public string username = "";
    DataTable grpdetails, following;
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        loadgrid();
    }
    public void loadgrid()
    {
        if (Request.QueryString["userid"] != null && Request.QueryString["userid"] != "" && Request.QueryString.Count == 1 && Request.QueryString["userid"].Length == 24)
        {
            if (Int32.TryParse(UrlRewritingVM.Decrypt(Request.QueryString["userid"]), out userid))
            {
                userid = Convert.ToInt32(commonclass.GetSafeIDFromURL(UrlRewritingVM.Decrypt(Request.QueryString["userid"].ToString().Trim())));
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
        DataTable user = classreviews.getuserbyindexid(userid);
        
        if (user.Rows.Count > 0)
        {
            grpdetails = classreviews.getusergroupsinfo(user.Rows[0]["username"].ToString());
            if(grpdetails.Rows.Count > 0)
            {
                rptfollowers.DataSource = grpdetails;
                rptfollowers.DataBind();
                DataTable follow;
                if (rptfollowers.Items.Count > 0)
                {
                    int m = 0;
                    for (int j = 0; j <= rptfollowers.Items.Count - 1; j++)
                    {
                        //LinkButton lnkusername = (LinkButton)rptfollowers.Items[j].FindControl("lnkusername");
                        Label grpid = (Label)rptfollowers.Items[j].FindControl("grpid");
                        m += 1;
                        UpdatePanel upd = (UpdatePanel)rptfollowers.Items[j].FindControl("UpdatePanel1");
                        upd.Attributes.Add("class", "updfollowing" + " color" + m);
                        if (m == 6)
                        {
                            m = 0;
                        }
                        follow = classreviews.getmembercount(grpid.Text.ToString());
                        Label membercount = (Label)rptfollowers.Items[j].FindControl("membercount");
                        membercount.Text = follow.Rows[0]["member"].ToString();
                    }
                }
            }
            else
            {
                if (Page.User.Identity.IsAuthenticated)
                {
                    if (Page.User.Identity.Name == user.Rows[0]["username"].ToString())
                    {
                        divmsg.InnerHtml = "No Groups in your list.";
                    }
                    else
                    {
                        divmsg.InnerHtml = "No Groups in list.";
                    }
                }
                else
                {
                    divmsg.InnerHtml = "No Groups in list.";
                }
                divmsg.Visible = true;
                profilediv.Visible = false;
            }
        }
    }

    protected void rptfollowers_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        
        bool iserror = false;

        if (Page.User.Identity.IsAuthenticated)
        {

            if (e.CommandName == "username")
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "grouppost/" + UrlRewritingVM.Encrypt(e.CommandArgument.ToString()));
            }

	        if (e.CommandName == "cmdimage")
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "grouppost/" + UrlRewritingVM.Encrypt(e.CommandArgument.ToString()));
            }
        }
    }
    public string groupUrlrewriting(object id)
    {
        string strurl = "";
        string cat = "";
        try
        {
            cat = id.ToString();
            strurl = ReturnUrl("sitepathmain") + "grouppost/" + UrlRewritingVM.Encrypt(cat);
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
}
