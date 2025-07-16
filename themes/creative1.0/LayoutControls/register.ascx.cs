
using System.Text.RegularExpressions;
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
using System.Web.Mail;

public partial class Themes_SecondTheme_LayoutControls_register : System.Web.UI.UserControl
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    protected void Page_Load(object sender, EventArgs e)
    {
        Label lbl_chkagree = ((Label)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("lbl_chkagree"));

        lbl_chkagree.Visible = false;
        if (!Page.IsPostBack)
        {
            fillcountry();
        }
    }
    protected void CreateUserWizard1_CreatingUser(object sender, LoginCancelEventArgs e)
    {
        CreateUserWizard cuw = (CreateUserWizard)sender;
        cuw.Email = cuw.UserName;

    }

    protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
    {
        CreateUserWizard cuw = (CreateUserWizard)sender;
        cuw.Email = cuw.UserName;
        Roles.AddUserToRole((sender as CreateUserWizard).UserName, "Customer");
        Label lbl_chkagree = ((Label)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("lbl_chkagree"));

        CheckBox chkagree = ((CheckBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("chk_agree"));
        Label lblstatus = ((Label)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("lblstatus"));
      
        lblstatus.Visible = false;
        TextBox txtfirstname = ((TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("txtfirstname"));

        TextBox txtlastname = ((TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("txtlastname"));
        TextBox txtmobileno = ((TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("txtmobileno"));
        TextBox txtaddress1 = ((TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("txtaddress1"));
        DropDownList ddlcountry = (DropDownList)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("ddlcountry");
        DropDownList ddlstate = (DropDownList)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("ddlstate");
        DropDownList ddlcity = (DropDownList)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("ddlcity");
        MembershipUserCollection email_member = Membership.FindUsersByEmail(cuw.UserName);
        bool bln_result = false;
      
        if (chkagree.Checked==false || email_member.Count == 0)
        {
            lbl_chkagree.Visible = true;
           // lbl_chkagree.Text = "Please tick the above checkbox to agree Terms & Conditions and continue further.";
            bln_result = true;
        }
        else
        {
            lbl_chkagree.Visible = false;
            bln_result = false;
        }

        if (bln_result == false)
        {
            //bool bln = classprofile.createprofile(Convert.ToString(cuw.UserName), Convert.ToString(txtfirstname.Text.Trim()), Convert.ToString(txtlastname.Text.Trim()));
            //ProfileCommon p = (ProfileCommon)ProfileCommon.Create(Convert.ToString(cuw.UserName), true);
            //p.FirstName = Convert.ToString(txtfirstname.Text.Trim());
            //p.LastName = Convert.ToString(txtlastname.Text.Trim());
            //p.Newsletter = "N";
            //p.AssignedPoints = "200";
            //p.Save();
           // classaddress.createaddress(cuw.UserName, p.FirstName, p.LastName, cuw.UserName, p.Address1, p.Address2, p.City, p.PinCode, p.State, p.Country, p.LandPhone, p.MobileNo, 'Y', 'Y', p.addsource, "", p.countrycode, p.citycode);

            bool bln = classaddress.createaddressregister(cuw.UserName, Convert.ToString(txtfirstname.Text.Trim()), Convert.ToString(txtlastname.Text.Trim()), Convert.ToString(txtaddress1.Text.Trim()), Convert.ToString(txtmobileno.Text.Trim()), Convert.ToInt32(ddlcountry.SelectedValue), Convert.ToInt32(ddlstate.SelectedValue), Convert.ToInt32(ddlcity.SelectedValue), "", "", "");
            CheckBox Persist = new CheckBox();
            Persist.Checked = true;
            DataTable dtadd = classaddress.getuserinfodetails(cuw.UserName);
            string strindex = "";
            if (dtadd.Rows.Count > 0)
            {
                strindex = dtadd.Rows[0]["indexid"].ToString();
            }
             MembershipUser Current = Membership.GetUser(cuw.UserName);

            string pass = Current.GetPassword().ToString();
            if (bln == true)
            {

                string body = Utilities.sendRegisterstatusEmailformat("R", "", cuw.UserName, Convert.ToString(txtfirstname.Text.Trim()), Convert.ToString(txtlastname.Text.Trim()), pass, cuw.UserName, Convert.ToInt32(strindex));
               
                AuthenticateEventArgs w = new AuthenticateEventArgs();
                w.Authenticated = true;
                FormsAuthentication.RedirectFromLoginPage(cuw.UserName, Persist.Checked);
                if (Request.QueryString["ReturnUrl"] != null)
                {
                    if (Request.QueryString["ReturnUrl"].Contains("shoppingcart") || Request.QueryString["ReturnUrl"].Contains("wishlist"))
                    {
                        addproduct();
                        Response.Redirect(Request.QueryString["ReturnUrl"]);
                    }
                    else
                    {
                        Response.Redirect(Request.QueryString["ReturnUrl"]);
                    }
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["sitepathmain"] + "default");
                }

            }
        }
        else
        {
            lbl_chkagree.Visible = true;
        }
       
    }


   


    public void fillcountry()
    {
        DropDownList ddlcountry = ((DropDownList)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("ddlcountry"));
        DropDownList ddlstate = ((DropDownList)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("ddlstate"));
        DropDownList ddlcity = ((DropDownList)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("ddlcity"));      
        ddlcountry.Items.Clear();
        ProfileCommon profile = this.Profile;
        profile = this.Profile.GetProfile(this.Page.User.Identity.Name);
        source = new SqlConnection(creativeconfiguration.DbConnectionString);
        string SqlQuery1 = "select distinct countryname,countryID from country order by countryname asc";
        sqladp = new SqlDataAdapter(SqlQuery1, source);
        DataTable dt = new DataTable();
        sqladp.Fill(dt);
        ddlcountry.DataSource = dt;
        ddlcountry.Items.Clear();
        ddlcountry.DataTextField = "countryname";
        ddlcountry.DataValueField = "countryID";
        ddlcountry.DataBind();
        ListItem item = new ListItem("--Choose Country--", "0");
        ddlcountry.Items.Insert(0, item);

        ListItem item1 = new ListItem("--Choose State--", "0");
        ddlstate.Items.Insert(0, item1);

        ListItem item2 = new ListItem("--Choose City--", "0");
        ddlcity.Items.Insert(0, item2);  

        if (ddlcountry.SelectedItem.Text != "--Choose Country--")
        {
            fillstate(Convert.ToInt32(ddlcountry.SelectedValue));
        }
    }

    public void fillstate(int country)
    {
        DropDownList ddlstate = ((DropDownList)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("ddlstate"));
       
        source = new SqlConnection(creativeconfiguration.DbConnectionString);
        string SqlQuery1 = "select statename,stateid from state where countryid=" + country + " order by statename asc";
        sqladp = new SqlDataAdapter(SqlQuery1, source);
        DataTable dt = new DataTable();
        sqladp.Fill(dt);
        ddlstate.DataSource = dt;
        ddlstate.Items.Clear();
        ddlstate.DataTextField = "statename";
        ddlstate.DataValueField = "stateid";
        ddlstate.DataBind();

        ListItem item1 = new ListItem("--Choose State--", "0");
        ddlstate.Items.Insert(0, item1);

        if (ddlstate.SelectedItem.Text != "--Choose State--")
        {

            fillcity(Convert.ToInt32(ddlstate.SelectedValue));
        }
    }

    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlcountry = ((DropDownList)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("ddlcountry"));

        if (ddlcountry.SelectedItem.Value != "--Choose Country--")
        {
            fillstate(Convert.ToInt32(ddlcountry.SelectedItem.Value.ToString()));
        }
    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlstate = ((DropDownList)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("ddlstate"));
        if (ddlstate.SelectedItem.Value != "--Choose State--")
        {
            fillcity(Convert.ToInt32(ddlstate.SelectedItem.Value.ToString()));
        }
    }
    public bool IsEmail(string Email)/*Added by vijay*/
    {
        string strRegex = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(Email))
            return (true);
        else
            return (false);
    }

    protected void UserName_TextChanged(object sender, EventArgs e)
    {

        Label lbl = new Label();
        lbl = ((Label)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("lblstatus"));
        TextBox txtUserName = ((TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("UserName"));
        bool memail = Convert.ToBoolean(IsEmail(txtUserName.Text));
       
        if (!string.IsNullOrEmpty(txtUserName.Text))
        {
            MembershipUserCollection u = Membership.FindUsersByEmail(txtUserName.Text);
            string strcheck = "";
            foreach (MembershipUser x in u)
            {
                lbl.Visible = true;
                if (x.UserName == txtUserName.Text)
                {
                    lbl.Text = "";
                    lbl.Text = "Email ID already exist";
                    lbl.ForeColor = System.Drawing.Color.Red;
                    strcheck = "Test";
                }
            }
            if (strcheck != "Test")
            {
                lbl.Visible = false;
            }
        }
    }


    public void fillcity(int city)
    {
        DropDownList ddlcity = ((DropDownList)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("ddlcity"));      
        source = new SqlConnection(creativeconfiguration.DbConnectionString);
        string SqlQuery1 = "select ltrim(rtrim(cityname)) as cityname,cityid from city where stateid=" + city + " order by cityname asc";

        sqladp = new SqlDataAdapter(SqlQuery1, source);
        DataTable dt = new DataTable();
        sqladp.Fill(dt);

        ddlcity.DataSource = dt;
        ddlcity.Items.Clear();
        ddlcity.DataTextField = "cityname";
        ddlcity.DataValueField = "cityid";

        ddlcity.DataBind();

        ListItem item2 = new ListItem("--Choose City--", "0");
        ddlcity.Items.Insert(0, item2);  

    }

    public string getRemoteAddr()
    {
        string UserIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (UserIPAddress == null)
        {
            UserIPAddress = Request.ServerVariables["REMOTE_ADDR"];
        }
        return UserIPAddress;
    }


    public void addproduct()
    {
        int productId = 0;
        string username;
        if (Request.QueryString["ReturnUrl"] != null)
        {
            if (Request.QueryString["ReturnUrl"].Contains("shoppingcart"))
            {
                if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
                {
                    productId = Convert.ToInt32(Request.QueryString["p"].ToString());
                }
                TextBox TextBox1 = ((TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("UserName"));
                username = TextBox1.Text.Trim().ToString();
                DataTable dt = new DataTable();
                int CartValue = 0;
                dt = ClassForUpdateQty.SumOfQtyFormProductTable(username, productId);
                if (dt.Rows.Count > 0)
                {
                    CartValue = Convert.ToInt32(dt.Rows[0]["SumQty"].ToString());
                }
                if (CartValue == 0)
                {
                    DataTable ds = classproduct.get_proc_ProductDescription_ProdFeature(productId);
                    if (ds.Rows.Count > 0)
                    {
                        string ourprice = ds.Rows[0]["ourprice"].ToString();
                        bool strstring = ClassForUpdateQty.UpdateQtyInTable(username, productId, "1", Convert.ToString(ourprice), 0, getRemoteAddr());
                    }
                }
                Response.Redirect(Request.QueryString["ReturnUrl"]);
            }
            else if (Request.QueryString["ReturnUrl"].Contains("wishlist"))
            {
                if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
                {
                    productId = Convert.ToInt32(Request.QueryString["p"].ToString());
                }
                TextBox TextBox1 = ((TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("UserName"));
                username = TextBox1.Text.Trim().ToString();

                DataTable dt = new DataTable();
                int wishlistValue = 0;
                dt = clswishlist.wishlist_GetSumOfProductFromQtyTable(username, productId);
                if (dt.Rows.Count > 0)
                {
                    wishlistValue = Convert.ToInt32(dt.Rows[0]["SumQty"].ToString());
                }
                if (wishlistValue == 0)
                {
                    DataTable ds = classproduct.get_proc_ProductDescription_ProdFeature(productId);
                    if (ds.Rows.Count > 0)
                    {
                        string ourprice = ds.Rows[0]["ourprice"].ToString();
                        bool strstring = clswishlist.insertwishlist(username, productId, "1", Convert.ToString(ourprice), getRemoteAddr());
                    }
                }
                Response.Redirect(ConfigurationManager.AppSettings["sitepathmain"] + "procs/wishlist");

            }
        }
        else
        {
            Response.Redirect(ConfigurationManager.AppSettings["sitepathmain"] + "default");
        }
    }



}