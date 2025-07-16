using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;
using System.IO;

public partial class themes_creative1_LayoutControls_checkout : System.Web.UI.UserControl
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Page.User.Identity.IsAuthenticated == true)
                {
                    tab_1.Style.Add("opacity", "1 !important");
                    tab_2.Style.Add("opacity", "0.4 !important");
                    tab_3.Style.Add("opacity", "0.4 !important");
                    MainView.ActiveViewIndex = 0;
                    Binddata();
                    BindGridcart();


                }
                else
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "login");
                }

            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    public string CheckCorrectAnswer(object productname, object productid)
    {
        return UrlRewritingVM.getUrlRewritingInfo(productname, productid, "PD");
    }
    protected void rptcartlist_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                bool m_index = clscartlist.Deletecartlistproduct(index);

                BindGridcart();
                Response.Redirect(Request.RawUrl);
                deletemsg.Visible = true;
            }
            if (e.CommandName == "change")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string ipadd = getRemoteAddr();
                for (int j = 0; j <= rptcartlist.Items.Count - 1; j++)
                {
                    Label lblourprice = (Label)rptcartlist.Items[j].FindControl("lblourprice");
                    Label lblproductid = (Label)rptcartlist.Items[j].FindControl("lblproductid");
                    TextBox txtquantity = (TextBox)rptcartlist.Items[j].FindControl("txtquantity");

                    if (Regex.IsMatch(txtquantity.Text, @"^[0-9]{1,40}$"))
                    {

                        if (Convert.ToDecimal(txtquantity.Text.ToString()) == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Quantity');", true);
                        }
                        else
                        {
                            bool strstring = ClassForUpdateQty.UpdateQtyInTable(Profile.UserName, Convert.ToInt32(lblproductid.Text), txtquantity.Text, lblourprice.Text, 0, ipadd);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Quantity');", true);
                    }

                }
                BindGridcart();
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    protected void rptaddress_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "address")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                DataSet ds_user = clscheckout.get_shippingaddress_from_id(Convert.ToInt32(index.ToString().Trim()));
                if (ds_user.Tables[0].Rows.Count > 0)
                {
                    lbladdressid.Text = index.ToString().Trim();
                    lbladdress.Text = ds_user.Tables[0].Rows[0]["firstname"].ToString() + "  " + ds_user.Tables[0].Rows[0]["lastname"].ToString() + " <br> " + ds_user.Tables[0].Rows[0]["address"].ToString() + ",<br> " + ds_user.Tables[0].Rows[0]["city"].ToString() + ",<br>" + ds_user.Tables[0].Rows[0]["state"].ToString() + ",<br> " + ds_user.Tables[0].Rows[0]["country"].ToString() + "  " + ds_user.Tables[0].Rows[0]["pincode"].ToString() + " <br> " + ds_user.Tables[0].Rows[0]["mobileno"].ToString();
                    txtsendordersms.Text = ds_user.Tables[0].Rows[0]["mobileno"].ToString();
                }
                pnlshipaddresss.Visible = true;
                Tab2_Click(source, e);
            }

            if (e.CommandName == "removeaddress")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                bool m_index = clscheckout.deleteshippingaddress(index);
                Binddata();
                Response.Redirect(Request.RawUrl);
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
        
    }
    protected void rptcartlist_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
    }
    private void Binddata()
    {
        try
        {
            string username = string.Empty;
            string m_flag = string.Empty;
            if (Page.User.Identity.IsAuthenticated)
            {
                username = Page.User.Identity.Name.ToString().Trim();
                DataSet ds_user = clscheckout.get_shippingaddress_getuserdetails(username.ToString().Trim());
                if (ds_user.Tables[0].Rows.Count > 0)
                {
                    pnlor.Visible = true;
                    pnlprevadd.Visible = true;
                    rptaddress.DataSource = ds_user.Tables[0];
                    rptaddress.DataBind();
                }
                else
                {
                    pnlor.Visible = true;
                    pnlprevadd.Visible = true;
                }

                DataSet ds_defaultuser = clscheckout.get_addressbook_getuserdetailsbyusername(username.ToString().Trim());
                if (ds_defaultuser.Tables[0].Rows.Count > 0)
                {
                    rptdefaultadd.DataSource = ds_defaultuser.Tables[0];
                    rptdefaultadd.DataBind();
                }
                else
                {
                }

            }
            fillcountry();
            //fillstate();
            // fillcity1();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    public void fillcountry()
    {
        try
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
            ListItem item = new ListItem("--Choose Country--", "0");
            ddlcountry.Items.Insert(0, item);

            ListItem item1 = new ListItem("--Choose State--", "0");
            ddlstate.Items.Insert(0, item1);

            ListItem item2 = new ListItem("--Choose City--", "0");
            ddlcity1.Items.Insert(0, item2);

            //if (ddlcountry.SelectedItem.Text != "--Choose Country--")
            //{
            //    fillstate();
            //}
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }


    }

    public void fillstate()
    {
       
       DropDownList ddl = new DropDownList();

        ddl = ddlcountry;
       
        source = new SqlConnection(creativeconfiguration.DbConnectionString);
        string SqlQuery1 = "select statename,stateid from state where countryid=" + ddl.SelectedItem.Value.ToString() + " order by statename asc";
        sqladp = new SqlDataAdapter(SqlQuery1, source);
        DataTable dt = new DataTable();
        sqladp.Fill(dt);
        ddlstate.DataSource = dt;
        ddlstate.Items.Clear();
        ddlstate.DataTextField = "statename";
        ddlstate.DataValueField = "stateid";
        ddlstate.DataBind();
        ListItem item = new ListItem("--Choose State--", "0");
        ddlstate.Items.Insert(0, item);

    }

    public void fillcity1()
    {
        DropDownList ddl = new DropDownList();
        ddl = ddlstate;
        source = new SqlConnection(creativeconfiguration.DbConnectionString);
        string SqlQuery1 = "select cityname,cityid from city where stateid=" + ddl.SelectedItem.Value.ToString() + " order by cityname asc";
        sqladp = new SqlDataAdapter(SqlQuery1, source);
        DataTable dt = new DataTable();
        sqladp.Fill(dt);
        ddlcity1.DataSource = dt;
        ddlcity1.Items.Clear();
        ddlcity1.DataTextField = "cityname";
        ddlcity1.DataValueField = "cityid";
        DropDownList ddcitylist = new DropDownList();
        TextBox txtci = new TextBox();
        int i = 0;
        ddcitylist = ddlcity1;
        ddlcity1.DataBind();
        ListItem lst3 = new ListItem();
        ListItem item = new ListItem("--Choose City--", "0");
        ddcitylist.Items.Insert(0, item);
   
    }

    protected void Tab1_Click(object sender, EventArgs e)
    {
        tab_1.Style.Add("opacity", "1 !important");
        tab_2.Style.Add("opacity", "0.4 !important");
        tab_3.Style.Add("opacity", "0.4 !important");
        MainView.ActiveViewIndex = 0;
        Binddata();
        pnlshipaddresss.Visible = false;
    }

    protected void Tab2_Click(object sender, EventArgs e)
    {
        if (pnlshipaddresss.Visible == true)
        {
            tab_1.Style.Add("opacity", "0.4 !important");
            tab_2.Style.Add("opacity", "1 !important");
            tab_3.Style.Add("opacity", "0.4 !important");
            MainView.ActiveViewIndex = 1;
            BindGridcart();
        }        
    }
    protected void Tab3_Click(object sender, EventArgs e)
    {
        if (pnlshipaddresss.Visible == true)
        {
            tab_1.Style.Add("opacity", "0.4 !important");
            tab_2.Style.Add("opacity", "0.4 !important");
            tab_3.Style.Add("opacity", "1 !important");
            MainView.ActiveViewIndex = 2;
        }
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
    private void BindGridcart()
    {
        deletemsg.Visible = false;
        string username = string.Empty;
        string m_flag = string.Empty;
        if (Page.User.Identity.IsAuthenticated)
        {
            username = Page.User.Identity.Name.ToString().Trim();
            m_flag = "U";
        }
        else
        {
            username = getRemoteAddr();
            m_flag = "P";
        }
        decimal shiprate = 0;
        DataSet ds_wishlist = new DataSet();
        ds_wishlist = clscartlist.get_cartlist_from_username(username.ToString().Trim(), m_flag.ToString());
        lblgrandtotal.Text = "0";
        lblamtpay.Text = "0";
        lbltotalitem.Text = "0";
        lbltotalsubtotal.Text = "0";
        if (ds_wishlist.Tables[0].Rows.Count > 0)
        {
            rptcartlist.DataSource = ds_wishlist.Tables[0];
            rptcartlist.DataBind();
            for (int j = 0; j <= rptcartlist.Items.Count - 1; j++)
            {
                Label lblourprice = (Label)rptcartlist.Items[j].FindControl("lblourprice");
                Label lblsubtotal = (Label)rptcartlist.Items[j].FindControl("lblsubtotal");
                TextBox txtquantity = (TextBox)rptcartlist.Items[j].FindControl("txtquantity");
                decimal rate1;

                rate1 = Convert.ToDecimal(lblourprice.Text);
                lblsubtotal.Text = string.Format("{0:F0}", ((rate1) * Convert.ToDecimal(txtquantity.Text)));
                lblgrandtotal.Text = Convert.ToString(Convert.ToDecimal(lblgrandtotal.Text) + Convert.ToDecimal(lblsubtotal.Text));
              
                lbltotalsubtotal.Text = Convert.ToString(Convert.ToDecimal(lbltotalsubtotal.Text) + Convert.ToDecimal(lblsubtotal.Text));
                lbltotalitem.Text = Convert.ToString(Convert.ToDecimal(lbltotalitem.Text) + Convert.ToDecimal(txtquantity.Text));

                Label lblshiprate = (Label)rptcartlist.Items[j].FindControl("lblshiprate");
                shiprate = shiprate + Convert.ToDecimal(lblshiprate.Text) * Convert.ToDecimal(txtquantity.Text);
            }
            if (shiprate != 0)
            {
                divship.Visible = true;
                divstotal.Visible = true;
                divsrate.Visible = true;
                decimal ship1;
                ship1 = Convert.ToDecimal(lbltotalsubtotal.Text) + shiprate;
                lblship.Text = string.Format("{0:F0}", Convert.ToInt32(shiprate));
                lblgrandtotal.Text = string.Format("{0:F0}", Convert.ToInt32(ship1));
                lblsrate.Text = string.Format("{0:F0}", Convert.ToInt32(shiprate));
                lblsubtotal.Text = string.Format("{0:F0}", Convert.ToInt32(lbltotalsubtotal.Text));
                lblamtpay.Text = string.Format("{0:F0}", Convert.ToInt32(ship1));
            }
            else
            {
                divship.Visible = false;
                divstotal.Visible = false;
                divsrate.Visible = false;
                lblgrandtotal.Text = lbltotalsubtotal.Text;
                lblsubtotal.Text = lbltotalsubtotal.Text;
                lblamtpay.Text = lblgrandtotal.Text;
            }

        }
        else
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "shoppingcart");
            rptcartlist.DataSource = null;
            rptcartlist.DataBind();
        }
    }
    protected void btntab1save_Click(object sender, EventArgs e)
    {
        if (m_validate() == true)
        {
            string landmark = "";
            if (txtlandmark.Text.Trim() != "")
            {
                landmark = txtlandmark.Text.Trim();
            }
            else
            {
                landmark = "";
            }
            bool bln = clscheckout.shippingaddress_insertupdateuserinfo(Page.User.Identity.Name.ToString().Trim(), Convert.ToString(txtfirstname.Text.Trim()), Convert.ToString(txtlastname.Text.Trim()), Convert.ToString(txtaddress1.Text.Trim()), Convert.ToString(txtmobile.Text.Trim()), Convert.ToInt32(ddlcountry.SelectedValue), Convert.ToInt32(ddlstate.SelectedValue), Convert.ToInt32(ddlcity1.SelectedValue), "", "", "", landmark);
          //  bool dt_address = classaddress.createaddress(Page.User.Identity.Name.ToString().Trim(), txtfirstname.Text.Trim(), txtlastname.Text.Trim(), Page.User.Identity.Name.ToString().Trim(), txtaddress1.Text.Trim(), "", ddlcity1.SelectedItem.Text.Trim(), txtpincode.Text.Trim(), ddlstate.SelectedItem.Text.Trim(), ddlcountry.SelectedItem.Text.Trim(), "", txtmobile.Text.Trim(), 'Y', 'Y', "", getRemoteAddr(), "91", "");

            Binddata();

            DataTable dtshipadd = clscheckout.gettopshippingaddress();

            DataSet ds_user = clscheckout.get_shippingaddress_from_id(Convert.ToInt32(dtshipadd.Rows[0]["indexid"]));
            if (ds_user.Tables[0].Rows.Count > 0)
            {

                lbladdress.Text = ds_user.Tables[0].Rows[0]["firstname"].ToString() + "  " + ds_user.Tables[0].Rows[0]["lastname"].ToString() + " <br> " + ds_user.Tables[0].Rows[0]["address"].ToString() + ",<br> " + ds_user.Tables[0].Rows[0]["city"].ToString() + ",<br>" + ds_user.Tables[0].Rows[0]["state"].ToString() + ",<br> " + ds_user.Tables[0].Rows[0]["country"].ToString() + "  " + ds_user.Tables[0].Rows[0]["pincode"].ToString() + " <br> " + ds_user.Tables[0].Rows[0]["mobileno"].ToString();

            }

            pnlshipaddresss.Visible = true;
            Tab2_Click(sender, e);    

        }
        else
        {

        }         
    }

    protected bool m_validate()
    {
        bool Validate = true;
        if (txtfirstname.Text.Trim() == "")
        {
            Validate = false;
        }
        if (txtlastname.Text.Trim() == "")
        {
            Validate = false;
        }
        if (txtaddress1.Text.Trim() == "")
        {
            Validate = false;
        }
      
        if (txtpincode.Text.Trim() == "")
        {
            Validate = false;
        }
        if (txtmobile.Text.Trim() == "")
        {
            Validate = false;
        }
        if (ddlcity1.SelectedItem.Text.Trim() == "--Choose City--")
        {
            Validate = false;
        }
        if (ddlstate.SelectedItem.Text.Trim() == "--Choose State--")
        {
            Validate = false;
        }
        if (ddlcountry.SelectedItem.Text.Trim() == "--Choose Country--")
        {
            Validate = false;
        }
        return Validate;
    }

    protected void btncontinue_Click(object sender, EventArgs e)
    {       
          //  Tab3_Click(sender, e); 
        //Response.Write(lbladdressid.Text);
        //Response.End();
        int outorderid = 0;
        decimal srate = 0;
        if (Convert.ToString(lblsrate.Text) != "")
        {
            srate = Convert.ToDecimal(lblsrate.Text);
        }
        outorderid = classorder.insertorder(Page.User.Identity.Name, txtfirstname.Text.Trim(), txtlastname.Text.Trim(), Convert.ToDecimal(lbltotalsubtotal.Text.ToString()), srate, Convert.ToDecimal(lblgrandtotal.Text.ToString()), "Credit Card", "Payment Failed", Page.User.Identity.Name);
        int orderid = outorderid;
        for (int j = 0; j <= rptcartlist.Items.Count - 1; j++)
        {
            Label lblproductid = (Label)rptcartlist.Items[j].FindControl("lblproductid");
            Label lblid = (Label)rptcartlist.Items[j].FindControl("lblid");
            Label lblpnumber = (Label)rptcartlist.Items[j].FindControl("lblpnumber");
            Label lblourprice = (Label)rptcartlist.Items[j].FindControl("lblourprice");
            Label lblsubtotal = (Label)rptcartlist.Items[j].FindControl("lblsubtotal");
            Label lblshiprate = (Label)rptcartlist.Items[j].FindControl("lblshiprate");
            TextBox txtquantity = (TextBox)rptcartlist.Items[j].FindControl("txtquantity");
            classorder.insertorderitem(outorderid, Convert.ToInt32(lblproductid.Text.Trim()), lblid.Text.Trim(), lblpnumber.Text.Trim(), Convert.ToInt32(txtquantity.Text.Trim()), Convert.ToDecimal(lblourprice.Text), Convert.ToDecimal(lblshiprate.Text), (Convert.ToDecimal(lblourprice.Text) * Convert.ToDecimal(txtquantity.Text.Trim())), Convert.ToDecimal(lblgrandtotal.Text), Page.User.Identity.Name, Convert.ToInt32(lbladdressid.Text));
        }

        Session["orderid"] = Convert.ToString(outorderid);
        Session["payamt"] = Convert.ToDouble(lblamtpay.Text.Trim());
        Response.Redirect(ReturnUrl("sitepathmain") + "response");
    }

    protected void btnpaycontinue_Click(object sender, EventArgs e)
    {
       // int outorderid = 0;
       // decimal srate = 0;
       //     if(Convert.ToString(lblsrate.Text)!="")
       //     {
       //         srate=Convert.ToDecimal(lblsrate.Text);
       //     }
       //     outorderid = classorder.insertorder(Page.User.Identity.Name, txtfirstname.Text.Trim(), txtlastname.Text.Trim(), Convert.ToDecimal(lbltotalsubtotal.Text.ToString()), srate, Convert.ToDecimal(lblgrandtotal.Text.ToString()), rbtpaymentlist.SelectedItem.Text, "Payment Failed", Page.User.Identity.Name);
       //int orderid = outorderid;
       //for (int j = 0; j <= rptcartlist.Items.Count - 1; j++)
       //{
       //    Label lblproductid = (Label)rptcartlist.Items[j].FindControl("lblproductid");
       //    Label lblid = (Label)rptcartlist.Items[j].FindControl("lblid");
       //    Label lblpnumber = (Label)rptcartlist.Items[j].FindControl("lblpnumber");
       //    Label lblourprice = (Label)rptcartlist.Items[j].FindControl("lblourprice");
       //    Label lblsubtotal = (Label)rptcartlist.Items[j].FindControl("lblsubtotal");
       //    Label lblshiprate = (Label)rptcartlist.Items[j].FindControl("lblshiprate");
       //    TextBox txtquantity = (TextBox)rptcartlist.Items[j].FindControl("txtquantity");
       //    classorder.insertorderitem(outorderid, Convert.ToInt32(lblproductid.Text.Trim()), lblid.Text.Trim(), lblpnumber.Text.Trim(), Convert.ToInt32(txtquantity.Text.Trim()), Convert.ToDecimal(lblourprice.Text), Convert.ToDecimal(lblshiprate.Text), (Convert.ToDecimal(lblourprice.Text) * Convert.ToDecimal(txtquantity.Text.Trim())), Convert.ToDecimal(lblgrandtotal.Text), Page.User.Identity.Name);
       //}


        //Response.Redirect(ReturnUrl("sitepathmain") + "thanks.aspx?&totalamount=" + encryptPassword("totalamount=" + lblamtpay.Text) + "&id=" + encryptPassword("id=" + orderid));
    }
   

    public string encryptPassword(string strText)
    {
        return Encrypt(strText);
    }
    public string Encrypt(string message)
    {
        UTF8Encoding textConverter = new UTF8Encoding();
        RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();

        //Convert the data to a byte array.
        byte[] toEncrypt = textConverter.GetBytes(message);

        //Get an encryptor.
        ICryptoTransform encryptor = rc2CSP.CreateEncryptor(ScrambleKey, ScrambleIV);

        //Encrypt the data.
        MemoryStream msEncrypt = new MemoryStream();
        CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

        //Write all data to the crypto stream and flush it.
        // Encode length as first 4 bytes
        byte[] length = new byte[4];
        length[0] = (byte)(message.Length & 0xFF);
        length[1] = (byte)((message.Length >> 8) & 0xFF);
        length[2] = (byte)((message.Length >> 16) & 0xFF);
        length[3] = (byte)((message.Length >> 24) & 0xFF);
        csEncrypt.Write(length, 0, 4);
        csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
        csEncrypt.FlushFinalBlock();

        //Get encrypted array of bytes.
        byte[] encrypted = msEncrypt.ToArray();

        // Convert to Base64 string
        string b64 = Convert.ToBase64String(encrypted);

        // Protect against URLEncode/Decode problem
        string b64mod = b64.Replace('+', '@');

        // Return a URL encoded string
        return HttpUtility.UrlEncode(b64mod);
    }

    public byte[] ScrambleKey
    {
        set
        {
            byte[] key = value;
            if (null == key)
            {
                // Use existing key if non provided
                key = ScrambleKey;
            }
            Session["ScrambleKey"] = key;
        }
        get
        {
            byte[] key = (byte[])Session["ScrambleKey"];
            if (null == key)
            {
                RC2CryptoServiceProvider rc2 = new RC2CryptoServiceProvider();
                rc2.GenerateKey();
                key = rc2.Key;
                Session["ScrambleKey"] = key;
            }
            return key;
        }
    }
    // Initialization vector management for scrambling support
    public byte[] ScrambleIV
    {
        set
        {
            byte[] key = value;
            if (null == key)
            {
                key = ScrambleIV;
            }
            Session["ScrambleIV"] = key;
        }
        get
        {
            byte[] key = (byte[])Session["ScrambleIV"];
            if (null == key)
            {
                RC2CryptoServiceProvider rc2 = new RC2CryptoServiceProvider();
                rc2.GenerateIV();
                key = rc2.IV;
                Session["ScrambleIV"] = key;
            }
            return key;
        }
    }


    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddslist1 = new DropDownList();
        ddslist1 = ddlcountry;

        if (ddslist1.SelectedItem.Value != "--Choose Country--")
        {
            fillstate();
        }
    }


    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddslist = new DropDownList();
        ddslist = ddlstate;
        if (ddslist.SelectedItem.Value != "--Choose State--")
        {
            fillcity1();
        }
    }

    protected void lnkchangeadd_Click(object sender, EventArgs e)
    {

        txtfirstname.Text = "";
        txtlastname.Text = "";
        txtaddress1.Text = "";
        txtlandmark.Text = "";
        ddlcountry.SelectedIndex = 0;
        ddlstate.SelectedIndex = 0;
        ddlcity1.SelectedIndex = 0;
        txtpincode.Text = "";
        txtmobile.Text = "";
        Tab1_Click(sender, e);
    }


    protected void rptdefaultaddress_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "address")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            DataSet ds_user = clscheckout.addressbook_from_indexid(Convert.ToInt32(index.ToString().Trim()));
            if (ds_user.Tables[0].Rows.Count > 0)
            {
                lbladdressid.Text = index.ToString().Trim();
                lbladdress.Text = ds_user.Tables[0].Rows[0]["firstname"].ToString() + "  " + ds_user.Tables[0].Rows[0]["lastname"].ToString() + " <br> " + ds_user.Tables[0].Rows[0]["address"].ToString() + ",<br> " + ds_user.Tables[0].Rows[0]["city"].ToString() + ",<br>" + ds_user.Tables[0].Rows[0]["state"].ToString() + ",<br> " + ds_user.Tables[0].Rows[0]["country"].ToString() + "  " + ds_user.Tables[0].Rows[0]["pincode"].ToString() + " <br> " + ds_user.Tables[0].Rows[0]["mobileno"].ToString();
                txtsendordersms.Text = ds_user.Tables[0].Rows[0]["mobileno"].ToString();
            }
            pnlshipaddresss.Visible = true;
            Tab2_Click(source, e);
        }
    }

}