using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class themes_creative1_LayoutControls_bithdays : System.Web.UI.UserControl
{
    string user = null;
    string fname = null;
    int userid = 0;

     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            loaddata();
            if (Request.QueryString["userid"] != null)
            {
                userid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["userid"].ToString()));

            }

            if (Page.User.Identity.IsAuthenticated)
            {
                user = Page.User.Identity.Name;
            }

            SqlConnection sqlcon1 = new SqlConnection(ConfigurationManager.ConnectionStrings["GlobalConnection"].ConnectionString);
            sqlcon1.Open();
            string cmdstr = "select username from addressbook where username='" + user + "';";
            SqlCommand cmd = new SqlCommand(cmdstr, sqlcon1);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                fname = reader["username"].ToString();
            }

        }
       
    }
    public void getdays()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("id");
        dt.Columns.Add("name");
        dt.Columns.Add("birthdate");
        DataRow dr = dt.NewRow();
        dr["id"] = "201";
        dr["name"] = "Shaikh Hameed";
        dr["birthdate"] = "10/June/1994";
        dt.Rows.Add(dr);
        DataRow dr2 = dt.NewRow();
        dr2["id"] = "202";
        dr2["name"] = "Cretative Test";
        dr2["birthdate"] = "10/March/2006";
        dt.Rows.Add(dr2);

        if (dt.Rows.Count > 0)
        {
            rptrnews.DataSource = dt;
            rptrnews.DataBind();
            ltrcat.Text = "<div id='carousel' class='IM-carousel'>";
        }
        else
        {
            divbd.Visible = false;
        }
    }

    public void loaddata()
    {
        try
        {
            //DataTable dtbday = classnews.topbdayList();
            DataSet dtbday = new DataSet();
            dtbday.ReadXml(ConfigurationManager.AppSettings["adminsitepath"]+ "xml/birthday.xml");
            if (dtbday.Tables.Count > 0)
            {
                bday.Visible = true;
                //pnlnews.Visible = true;
                rptbday.DataSource = dtbday;
                rptbday.DataBind();
            }
            else
            {
                bday.Visible = false;
            }


        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    public void loadgrp()
    {
        try
        {
            DataTable grp = classnews.topgrpList();
            if (grp.Rows.Count > 0)
            {
                if(grp.Rows.Count >4)
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

    public void loadgrpbyuser(string username)
    {
        try
        {
            DataTable grp = classnews.grpListbyuser(username);
            if (grp.Rows.Count > 0)
            {
                rptgrp.DataSource = grp;
                rptgrp.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    public string getuser(object userid)
    {
        string strurl = "";
        try
        {
            strurl = ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(userid.ToString());
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }

    public string getgroup(object grpid)
    {
        string strurl = "";
        try
        {
            if(Page.User.Identity.IsAuthenticated)
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

    protected void lnkview_Click(object sender, EventArgs e)
    {
        DataSet ds = classaddress.GetuserId(Page.User.Identity.Name.ToString());
        Response.Redirect(ReturnUrl("sitepathmain") + "groups/" + UrlRewritingVM.Encrypt(ds.Tables[0].Rows[0]["indexid"].ToString()));
    }
}