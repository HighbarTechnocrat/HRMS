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
using System;
using System.Data;
using System.Configuration;

public partial class postenquiry : System.Web.UI.Page
{
    private int productId=0, ID;
    SqlConnection source;
    public SqlDataAdapter sqladp;
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Int32.TryParse(Request.QueryString["p"], out ID))
            {
                if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
                {
                    productId = Convert.ToInt32(Request.QueryString["p"].ToString());
                }
            }

            if (!Page.IsPostBack)
            {
                txtname.Focus();
                fillcountry();
                geteventtype();
                
                if (productId != 0)
                {
                    getdatabyproductdetails();
                    filluserdata();
                }
                else
                {
                    if (Page.User.Identity.IsAuthenticated)
                    {
                       
                        filluserdata();
                        loaddata();
                    }
                }

            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    protected void submitbtn_Click(object sender, EventArgs e)
    {

       
        try
        {
            bool iserror = false;
            TimeSpan time = DateTime.Now.TimeOfDay;
           
            if ((Convert.ToDateTime(txtstartdate.Text) + time) >= DateTime.Now)
            {

            }
            else
            {
                lblstartdate.Visible = true;
                iserror = true;
                lblstartdate.Text = "Start date entered should be greater than current date";
            }

            if (Convert.ToDateTime(txtenddate.Text) >= Convert.ToDateTime(txtstartdate.Text))
            {

            }
            else
            {
                lblenddate.Visible = true;
                iserror = true;
                lblenddate.Text = "End date entered should be greater than or equal to start date";
            }
            decimal bfrom = 0;
            decimal bto = 0;
            if (Convert.ToDecimal(txtbfrom.Text) > 0)
            {
                bfrom = Convert.ToDecimal(txtbfrom.Text);
               
            }
            else
            {
                iserror = true;
                lblbudgetfrom.Visible = true;
                lblbudgetfrom.Text = "Budget from cannot be enter zero";
            }
          
            //}
            if (Convert.ToDecimal(txtbto.Text) > 0)
            {

                if (Convert.ToDecimal(txtbfrom.Text) < Convert.ToDecimal(txtbto.Text))
                {
                   
                    bto = Convert.ToDecimal(txtbto.Text);
                }
                else
                {
                    lblbudgetto.Visible = true;
                    iserror = true;
                    lblbudgetfrom.Visible = false;
                    lblbudgetto.Text = "Budget to entered should be greater than Budget from";
                }
            }
            else
            {
                iserror = true;
                lblbudgetto.Visible = true;
                lblbudgetto.Text = "Budget to cannot be enter zero";
            }

            DateTime dtstart = DateTime.Now;
            dtstart = dtstart.AddDays(5);
            txtstartdate.Text = dtstart.ToString("dd/mm/yyyy");

            if (!iserror)
            {
                
             
                int i = Convert.ToInt32(classaddress.insertpostenquiery(txtname.Text.Trim(), txtcountrycode.Text.Trim(), txtcontact.Text, txtemailid.Text.Trim(), txtcname.Text.Trim(), txtadd1.Text.Trim(), txtadd2.Text.Trim(), Convert.ToString(txtpostcode.Text), Convert.ToString(ddlcountry.SelectedValue), Convert.ToString(ddlstate.SelectedValue), Convert.ToString(ddlcity.SelectedValue), Convert.ToString(ddlmcontact.SelectedValue), txtccode.Text, txtcmobile.Text, txtcemail.Text.Trim(), Convert.ToDateTime(txtstartdate.Text), Convert.ToDateTime(txtenddate.Text), Convert.ToInt32(txtnattence.Text), txtvenue.Text.Trim(), Convert.ToString(ddlLayout.SelectedValue), Convert.ToString(ddlavrequired.SelectedValue), bfrom, bto));
                if (i > 0)
                {
                    DataTable dtenqid = classaddress.gettopenquiryid();
                    int enqid = Convert.ToInt32(dtenqid.Rows[0]["id"].ToString());

                    int[] iSelIndicesListBox1 = ddletype1.GetSelectedIndices();

                    for (int iSelIndex = 0; iSelIndex < iSelIndicesListBox1.Length; iSelIndex++)
                    {
                        string str1r = ddletype1.Items[iSelIndicesListBox1[iSelIndex]].Value;
                        int catid = Convert.ToInt32(str1r);
                        classaddress.insertenquirytocategory(enqid, catid);
                    }

                    int[] iSelIndicesListBox2 = ddlproductlist.GetSelectedIndices();
                    string productlist = "";
                    for (int iSelIndex = 0; iSelIndex < iSelIndicesListBox2.Length; iSelIndex++)
                    {
                        string str1r = ddlproductlist.Items[iSelIndicesListBox2[iSelIndex]].Value;

                        int pid = Convert.ToInt32(str1r);
                        classaddress.insertenquirytoproduct(enqid, pid);
                        if (iSelIndicesListBox2.Length == 1)
                        {
                            productlist = ddlproductlist.Items[iSelIndicesListBox2[iSelIndex]].Text;
                        }
                        else
                        {
                            productlist = productlist + ddlproductlist.Items[iSelIndicesListBox2[iSelIndex]].Text + ",";
                        }
                    }
                    productlist = productlist.Remove(productlist.Length - 1);
                   
                    string body = Utilities.sendRegisterstatusEmailformat("PE", "", txtemailid.Text.Trim(), txtname.Text.Trim(), productlist, "", txtemailid.Text.Trim(), 0);

                    pnlmsg.Visible = true;
                    pnlenquiry.Visible = false;

                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }


    public void getdatabyproductdetails()
    {
        GetSelectedItemsInListBox(Convert.ToInt32(productId));
        SelectlistBoxItemproduct(Convert.ToString(productId));
    }

    public void loaddata()
    {
        string username = string.Empty;
        string m_flag = string.Empty;
        string productid = "";
        if (Page.User.Identity.IsAuthenticated)
        {
            username = Page.User.Identity.Name.ToString().Trim();
            m_flag = "U";

           
            DataSet dscartproduct = new DataSet();
            dscartproduct = clscartlist.get_cartlist_from_username(username.ToString().Trim(), m_flag.ToString());
            if (dscartproduct.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dscartproduct.Tables[0].Rows.Count; i++)
                {
                    productid = (dscartproduct.Tables[0].Rows[i]["productid"]).ToString();
                    GetSelectedItemsInListBox(Convert.ToInt32(productid));                                                         
                }
            }
           
            if (dscartproduct.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dscartproduct.Tables[0].Rows.Count; i++)
                {

                     productid = (dscartproduct.Tables[0].Rows[i]["productid"]).ToString();
                  
                    SelectlistBoxItemproduct(productid);                   

                }
            }
            
        }
    }


    public void filluserdata()
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            string username = string.Empty;
            username = Page.User.Identity.Name.ToString().Trim();
            DataSet ds_userdetails = classaddress.getalluserbyusername(this.Page.User.Identity.Name);
            string fname = ds_userdetails.Tables[0].Rows[0]["firstname"].ToString();
            string lname = ds_userdetails.Tables[0].Rows[0]["lastname"].ToString();
            string name = fname + " " + lname;
            txtname.Text = name;
            txtcountrycode.Text = "+91";
            txtcontact.Text = ds_userdetails.Tables[0].Rows[0]["mobileno"].ToString();
            txtemailid.Text = ds_userdetails.Tables[0].Rows[0]["emailid"].ToString();
            fillcountry();
            country country = classaddress.GetCountryDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["country"].ToString()));
            ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByText(country.countryName.Trim()));
            fillstate(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["country"].ToString()));
            //getting state name throough state id
            states state = classaddress.GetStateDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["state"].ToString()));
            ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByText(state.statename.Trim()));

            fillcity(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["state"].ToString()));
            //getting CITY name throough city id

            city city = classaddress.GetCityDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["city"].ToString()));

            ddlcity.SelectedIndex = ddlcity.Items.IndexOf(ddlcity.Items.FindByText(city.cityname.Trim()));
        }

    }

    private void GetSelectedItemsInListBox(int productid)
    {
        DataTable dtcategory = classaddress.getallcategoryid(productid);
        if (dtcategory.Rows.Count > 0)
        {
            for (int i = 0; i < dtcategory.Rows.Count; i++)
            {
                string categoryname = (dtcategory.Rows[i]["categoryid"]).ToString();
                getproductlist(Convert.ToInt32(categoryname));               
                SelectlistBoxItemcategory(categoryname);
            }
        }
    }
    private void SelectlistBoxItemproduct(string strproduct)
    {
       
        for (int i = 0; i < ddlproductlist.Items.Count; i++)
        {                    
            if (ddlproductlist.Items[i].Value == strproduct)
            {
               // Response.Write(strproduct);
                ddlproductlist.Items[i].Selected = true;
            }
        }
    }

    private void SelectlistBoxItemcategory(string strproduct)
    {
        for (int i = 0; i < ddletype1.Items.Count; i++)
        {
            if (ddletype1.Items[i].Value == strproduct)
            {
                ddletype1.Items[i].Selected = true;
            }
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
       
        Response.Redirect(ReturnUrl("sitepath")+"default.aspx");
    }

    public void fillcountry()
    {
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
        ListItem item = new ListItem("Select Country", "0");
        ddlcountry.Items.Insert(0, item);

        if (ddlcountry.SelectedItem.Text != "Select Country")
        {

            fillstate(Convert.ToInt32(ddlcountry.SelectedValue));
        }
    }

    public void fillstate(int country)
    {

        DropDownList ddl = new DropDownList();

        ddl = ddlcountry;
        ddlstate.Enabled = true;
        ddlcity.Enabled = false;
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
        ListItem item = new ListItem("Select State", "0");
        ddlstate.Items.Insert(0, item);

        if (ddlstate.SelectedItem.Text != "Select City")
        {
            fillcity(Convert.ToInt32(ddlstate.SelectedValue));
        }

    }

    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddslist1 = new DropDownList();
        ddslist1 = ddlcountry;
       
        if (ddslist1.SelectedItem.Value != "Select Country")
        {
            ddlstate.Enabled = true;
            ddlcity.Enabled = false;
            fillstate(Convert.ToInt32(ddlcountry.SelectedItem.Value.ToString()));
        }
    }

    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddslist = new DropDownList();
        ddslist = ddlstate;
        if (ddslist.SelectedItem.Value != "Select State")
        {
            fillcity(Convert.ToInt32(ddslist.SelectedItem.Value.ToString()));
        }
    }

    protected void ddletype_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddletype = new DropDownList();
      
      
        getproductlist(Convert.ToInt32(ddletype1.SelectedItem.Value.ToString()));
       // }
    }

    public void fillcity(int city)
    {
        DropDownList ddl = new DropDownList();

        ddl = ddlstate;
        ddlcity.Enabled = true;
        source = new SqlConnection(creativeconfiguration.DbConnectionString);
        string SqlQuery1 = "select ltrim(rtrim(cityname)) as cityname,cityid from city where stateid=" + city + " order by cityname asc";

        sqladp = new SqlDataAdapter(SqlQuery1, source);
        DataTable dt = new DataTable();
        sqladp.Fill(dt);

        ddlcity.DataSource = dt;
        ddlcity.Items.Clear();
        ddlcity.DataTextField = "cityname";
        ddlcity.DataValueField = "cityid";

        DropDownList ddcitylist = new DropDownList();
        TextBox txtci = new TextBox();
        int i = 0;

        ddcitylist = ddlcity;
     
        ListItem lst3 = new ListItem();

        ddlcity.DataBind();
        ListItem item = new ListItem("Select City", "0");
        ddcitylist.Items.Insert(0, item);
       
    }

    public void geteventtype()
    {

    

        DataTable dtetype = classproduct.getparentbycategoryid();
        ddletype1.DataSource = dtetype;
        ddletype1.Items.Clear();
        ddletype1.DataTextField = "categoryname";
        ddletype1.DataValueField = "categoryid";
        ddletype1.DataBind();
      
    }

    public void getproductlist(int catid)
    {
        
        ddlproductlist.DataSource = classproduct.getproductlistdetails(catid);
        ddlproductlist.Items.Clear();
        ddlproductlist.DataTextField = "productname";
        ddlproductlist.DataValueField = "productid";
        ddlproductlist.DataBind();
       

    }


}