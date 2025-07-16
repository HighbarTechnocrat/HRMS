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
using System.Data.SqlClient;
using System.Xml;
using System.Text;
using System.IO;
using System.Data.Common;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;


public partial class ChangePassword : System.Web.UI.Page
{
    SP_Methods adm = new SP_Methods();
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc="", dept="", subdept="", desg = "";
    public int did = 0;

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url;}
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
            Session["chkbtnStatus"] = "";

            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

            txtEmpCode.Text = Convert.ToString(Session["Empcode"]).Trim();
            lblmessage.Visible = false; ;
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/ChangePassword");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {

                    editform.Visible = true;
                     this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    public static string HashSHA1(string value)
    {
        var sha1 = System.Security.Cryptography.SHA1.Create();
        var inputBytes = Encoding.ASCII.GetBytes(value);
        var hash = sha1.ComputeHash(inputBytes);

        var sb = new StringBuilder();
        for (var i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        string search_str = "";
        string oldpwds = "";
        string newpwds = "";

        if (OldPwd.Text.ToString() == "" || NewPwd.Text.ToString() == "")
        {
            lblmessage.Text = "Please enter Old & New Password!";
            lblmessage.Visible = true;
            return;
        }
        //string hashedPassword = HashSHA1(OldPwd + dbUserGuid);
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "Validate_Salt";
        string username = String.Format("{0}", Session["LoginEmpmail"].ToString());
        string userpwd = String.Format("{0}", Request.Form["password"]);
        spars[1] = new SqlParameter("@SearchString", SqlDbType.VarChar);
        if (username.ToString() == "")
            spars[1].Value = DBNull.Value;
        else
            spars[1].Value = username.ToString();

        spars[2] = new SqlParameter("@Pwd", SqlDbType.NVarChar);
        if (userpwd.ToString() == "")
            spars[2].Value = DBNull.Value;
        else
            spars[2].Value = userpwd.ToString();

        DataTable dt = adm.getDropdownList(spars, "SP_Admin_Validate_User");

        string dbUserGuid = "";
        if (dt.Rows.Count > 0)
        {
            string dbPassword = Convert.ToString(dt.Rows[0]["Pwd"]);
            dbUserGuid = Convert.ToString(dt.Rows[0]["UserGuid"]);
        }
        oldpwds = OldPwd.Text.ToString();
        newpwds = NewPwd.Text.ToString();

        string oldpassword = HashSHA1(oldpwds + dbUserGuid);
        Guid userGuid = System.Guid.NewGuid();
        string Newpassword = HashSHA1(newpwds + userGuid.ToString());
        spars = new SqlParameter[5];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "ChangePassword";

        spars[1] = new SqlParameter("@SearchString", SqlDbType.VarChar);
        if (Convert.ToString(Session["Empcode"]) == "")
            spars[1].Value = DBNull.Value;
        else
            spars[1].Value = Convert.ToString(Session["Empcode"]);

        spars[2] = new SqlParameter("@Pwd", SqlDbType.NVarChar);
        if (Newpassword.ToString() == "")
            spars[2].Value = DBNull.Value;
        else
            spars[2].Value = Newpassword.ToString();

        spars[3] = new SqlParameter("@OldPwd", SqlDbType.NVarChar);
        if (oldpassword.ToString() == "")
            spars[3].Value = DBNull.Value;
        else
            spars[3].Value = oldpassword.ToString();

        spars[4] = new SqlParameter("@Guid", SqlDbType.UniqueIdentifier);
        spars[4].Value = userGuid;

        dt = null;
        dt = adm.getDropdownList(spars, "SP_Admin_Validate_User");

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["msg"].ToString() == "")
            {
                lblmessage.Text = dt.Rows[0]["msg"].ToString();
                lblmessage.Visible = true;
            }
            else
            {
                lblmessage.Text = dt.Rows[0]["msg"].ToString();
                lblmessage.Visible = true;
            }
        }
    }
}
