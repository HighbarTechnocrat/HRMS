using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Net;

public partial class Themes_SecondTheme_LayoutControls_pdnew_zommer : System.Web.UI.UserControl
{
    private static int productId;
    public string strur;
    public string ipaddress;
    public string countrycode;
    public static string indexid1;

     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
        {
            productId = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["p"].ToString()));
        }
        
        if (!IsPostBack)
        {
            ipaddress = getRemoteAddr();
            countrycode = GetMaxMindOmniData(ipaddress);
            int countrycount = classcountry.getcountrycount(productId, countrycode);
            if (countrycount > 0)
            {

                lnktrailor.Visible = true;
                lnktrailormob.Visible = true;
                lnkmovie.Visible = false;
                divwatch.Visible = false;
                //lnkwatch.Visible = false;
            }
            else
            {
                lnktrailor.Visible = true;
                lnktrailormob.Visible = true;
                lnkmovie.Visible = true;
                divwatch.Visible = true;
                //lnkwatch.Visible = false;
            }
            displaybigimage();
            loaddata();
            
            getpremiumpackagestatus(Page.User.Identity.Name.ToString());
            getspecialpackagestatus(Page.User.Identity.Name.ToString(), Convert.ToDecimal(productId));
            DataTable dt = classproduct.getproductbigimage(productId);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["bigimage"].ToString().Trim() == "noimage2.gif")
                {
                    home.Visible = false;
                    divwatch.Visible = false;
					strhdiv.Attributes.Add("class", "sticky-hidden-div sticky-hidden-noimage");  
                }
				else
				{
                    home.Visible = false;
                    divwatch.Visible = false;
                    strhdiv.Attributes.Add("class", "sticky-hidden-div sticky-hidden-noimage");  
					//strhdiv.Attributes.Add("class", "sticky-hidden-div");  
				}
            }
        }
        //}
        //catch (Exception ex)
        //{
        //    ErrorLog.WriteError(ex.ToString());
        //}
    }
    public void displaybigimage()
    {
        DataTable dt = classproduct.getproductbigimage(productId);
        if (dt.Rows.Count > 0)
        {
           //bigpic.ImageUrl = ConfigurationManager.AppSettings["sitepathadmin"]+ "images/bigproduct/" + dt.Rows[0]["bigimage"].ToString();
           // home.Attributes.Add("style", "background-image: url('" + ConfigurationManager.AppSettings["sitepathadmin"]+ "images/bigproduct/" + dt.Rows[0]["bigimage"].ToString() + "')");
            home.Attributes.Add("style", "background-image: url('" + ReturnUrl("sitepathmain") + "coverphoto/" + dt.Rows[0]["bigimage"].ToString() + "')");
        }
        else
        {
           //bigpic.Visible = false;
        }
    }

    public void loaddata()
    {
     
        if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
        {
            productId = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["p"].ToString()));
        }

        DataTable ds = classproduct.get_proc_ProductDescription_ProdFeature(productId);
        if (ds.Rows.Count > 0)
        {
            string strflag = ds.Rows[0]["parentflag"].ToString();
            

            #region order status
            if (Page.User.Identity.IsAuthenticated)
            {
                if (strflag == "Premium")
                {
                    DataTable dtord = classpkg.getorderdetailbyemaild(Page.User.Identity.Name, "P");
                    if (dtord.Rows.Count > 0)
                    {
                        lnkmovie.Visible = true;
                        divwatch.Visible = true;
                        lnkmovie.Enabled = true;
                    }
                    else
                    {
                        divsubscribe.Visible = true;
                        lnkmovie.Attributes.Add("style", "cursor:default;");
                        lnkmovie.Enabled = false;
                        divwatch.Visible = false;
                        //lnksubscribe.Text = "Subscribe to Premium";
                    }
                }
                if (strflag == "Special")
                {
                    DataTable dtord = classpkg.getspecialorderdetailbyemaild(Page.User.Identity.Name, "S", Convert.ToDecimal(productId));
                    if (dtord.Rows.Count > 0)
                    {
                        divwatch.Visible = true;
                        lnkmovie.Enabled = true;
                    }
                    else
                    {
                        divsubscribe.Visible = true;
                        lnkmovie.Attributes.Add("style", "cursor:default;");
                        lnkmovie.Enabled = false;
                        lnkmovie.Visible = false;
                        divwatch.Visible = false;
                        //lnksubscribe.Text = "Buy special to watch";
                    }
                }
            }
            else
            {
                if (strflag == "Premium")
                {
                    //lnksubscribe.Text = "Subscribe to Premium";
                    lnkmovie.Enabled = false;
                    divwatch.Visible = false;
                    divsubscribe.Visible = true;
                    
                }
                if (strflag == "Special")
                {
                    //lnksubscribe.Text = "Buy special to watch";
                    //spanpkg.Visible = true;
                    lnkmovie.Enabled = false;
                    divwatch.Visible = false;
                    divsubscribe.Visible = true;
                }
            }
            #endregion
            #region trailor
            DataTable dtr = classproduct.get_proc_movietrailor_productid(productId);
            if (dtr.Rows.Count > 0)
            {
                string strtrailor = Convert.ToString(dtr.Rows[0]["videotrailerembed"]);
                if (strtrailor == "")
                {
                    lnktrailor.Visible = false;
                    lnktrailor.Attributes.Add("style", "cursor:default;");
                    lnktrailormob.Visible = false;
                }
                string strmovie = Convert.ToString(dtr.Rows[0]["vedioembed"]);
                if (strmovie == "")
                {
                    lnkmovie.Visible = false;
                    divsubscribe2.Attributes.Add("class", "");
                    divwatch2.Attributes.Add("class", "");
                    divsubscribe2.Attributes.Add("style", "height:120px;");
                    divwatch2.Attributes.Add("style", "height:120px;");
                }
            }
            #endregion
            string strurl = UrlRewritingVM.getUrlRewritingInfo((ds.Rows[0]["productname"].ToString()), UrlRewritingVM.Encrypt(productId.ToString().Trim()), "PD");
            string strdpath = "http://pinterest.com/pin/create/button/?url=" + strurl + "&amp;media=" + ConfigurationManager.AppSettings["sitepathadmin"]+ "images/bigproduct/" + ds.Rows[0]["bigimage"].ToString() + "&amp;description=" +UrlRewritingVM.ProcessUrl(ds.Rows[0]["shortdescription"].ToString());
            ltrimage.Text = "<a href='" + strdpath + "' target= '_blank' count-layout='none'><i class='fa fa-pinterest-p'></i></a>";
            ltrimage2.Text = "<a href='" + strdpath + "' target= '_blank' count-layout='none'><i class='fa fa-pinterest-p'></i></a>";

            googleshare.HRef = "https://plus.google.com/share?url=" + strurl;
            googlesharemob.HRef = "https://plus.google.com/share?url={" + strurl + "}";
        }
        else
        {
        }

        if(Page.User.Identity.IsAuthenticated)
        {
            popup.Visible = true;
            DataTable dtm = classreviews.getratingdetails(Page.User.Identity.Name, productId);
            {
                if (dtm.Rows.Count == 0)
                {
                    gotocritics.Visible = false;
                    gotocriticsmob.Visible = false;
                }
            }
            DataTable dtrf = clswishlist.wishlist_GetSumOfProductFromQtyTable(Page.User.Identity.Name, Convert.ToInt32(productId));
            if (Convert.ToInt32(dtrf.Rows[0]["SumQty"]) > 0)
            {
                lnkheart.Attributes.Add("style", "color:#338EC9;");
                lnkheartmob.Attributes.Add("style", "color:#338EC9;");
                lnkfav.ToolTip = "Saved";
            }
            else
            {
                lnkheart.Attributes.Add("style", "color:#FFF !important;");
                lnkheartmob.Attributes.Add("style", "color:#FFF !important;");
                lnkfav.ToolTip = "Add to favorites";
            }
            
            DataTable dtlike = classreviews.getlikedetailsbyuserpostid(Page.User.Identity.Name, Convert.ToInt32(productId));
            if (dtlike.Rows.Count > 0)
            {
                if (dtlike.Rows[0]["likeflag"].ToString().Trim() == "Unlike")
                {
                    lnklike.Attributes.Add("style", "color:#338EC9 !important;");
                    lnkthumbup.Attributes.Add("style", "color:#338EC9 !important;");
                    lnkthumbupmob.Attributes.Add("style", "color:#338EC9 !important;");
                    lnklike.ToolTip = "Liked";
                }
                else
                {
                    lnklike.Attributes.Add("style", "color:#FFFFFF !important;");
                    lnkthumbup.Attributes.Add("style", "color:#FFFFFF !important;");
                    lnkthumbupmob.Attributes.Add("style", "color:#FFFFFF !important;");
                    lnklike.ToolTip = "Like this post";
                }

                if (dtlike.Rows[0]["dislikeflag"].ToString().Trim() == "Undislike")
                {
                    lnkdislike.Attributes.Add("style", "color:#338EC9 !important;");
                    lnkthumbdown.Attributes.Add("style", "color:#338EC9 !important;");
                    lnkthumbdownmob.Attributes.Add("style", "color:#338EC9 !important;");
                    lnkdislike.ToolTip = "Disliked";
                }
                else
                {
                    lnkdislike.Attributes.Add("style", "color:#FFFFFF !important;");
                    lnkthumbdown.Attributes.Add("style", "color:#FFFFFF !important;");
                    lnkthumbdownmob.Attributes.Add("style", "color:#FFFFFF !important;");
                    lnkdislike.ToolTip = "Dislike this post";
                }
            }
            else
            {
                lnklike.Attributes.Add("style", "color:#FFFFFF !important;");
                lnkthumbup.Attributes.Add("style", "color:#FFFFFF !important;");
                lnkthumbupmob.Attributes.Add("style", "color:#FFFFFF !important;");
                lnklike.ToolTip = "Like this post";

                lnkdislike.Attributes.Add("style", "color:#FFFFFF !important;");
                lnkthumbdown.Attributes.Add("style", "color:#FFFFFF !important;");
                lnkthumbdownmob.Attributes.Add("style", "color:#FFFFFF !important;");
                lnkdislike.ToolTip = "Dislike this post";
            }
        }
        else
        {
            popup.Visible = false;
            popupmob.Visible = false;
            lnkpopup_trigger.Visible = true;
            lnkpopup_triggermob.Visible = true;
            if (ds.Rows.Count > 0)
            {
                string strur = UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(productId.ToString()), "PDID");
                lnkpopup_trigger.NavigateUrl = ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + strur;
                lnkpopup_triggermob.NavigateUrl = ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + strur;
            }
            lnkfav.ToolTip = "Login to add favorites";
	        DataTable dtm = classreviews.getratingdetailsbymovieid(productId);
            if (dtm.Rows.Count > 0)
            {
		        gotocritics.Visible=true;
            }
	        else
	        {
		        gotocritics.Visible=false;
	        }
        }
       
    }

    public void getpremiumpackagestatus(string useremail)
    {
        try
        {
        DataTable dtordp = classpkg.getstatusorderdetailbyemaild(useremail);

        if (dtordp.Rows.Count > 0)
        {
            int orderid = Convert.ToInt32(dtordp.Rows[0]["orderid"]);

            bool strurlex = classpkgorder.packagevalidity_status(useremail, Convert.ToDecimal(orderid));

        }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
    public void getspecialpackagestatus(string useremail, decimal productid)
    {
        try
        {
            if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
            {

                productId = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["p"].ToString().Trim()));

            }
            DataTable dtorsp = classpkg.getstatusspecialorderdetailbyemaild(useremail, Convert.ToDecimal(productid));

            if (dtorsp.Rows.Count > 0)
            {
                int orderid = Convert.ToInt32(dtorsp.Rows[0]["orderid"]);

                bool strurlex =
               classpkgorder.packagevalidity_status(useremail, Convert.ToDecimal(orderid));

            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

     public static string getRemoteAddr(bool GetLan = false)
     {
         try
         {
         String ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
         if (string.IsNullOrEmpty(ip))
         {
             ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
         }
         return ip;
         }
         catch
         {
             return null;
         }
     }
     private string GetMaxMindOmniData(string IP)
     {
         System.Uri objUrl = new System.Uri("https://geoip.maxmind.com/a?l=n1iX3mXeKAK1&i=" + IP);
         System.Net.WebRequest objWebReq;
         System.Net.WebResponse objResp;
         System.IO.StreamReader sReader;
         string strReturn = string.Empty;
         try
         {
             objWebReq = System.Net.WebRequest.Create(objUrl);
             objResp = objWebReq.GetResponse();
             sReader = new System.IO.StreamReader(objResp.GetResponseStream());
             strReturn = sReader.ReadToEnd();
             sReader.Close();
             objResp.Close();
         }
         catch (Exception ex)
         {
         }
         finally
         {
             objWebReq = null;
         }
         return strReturn;
     }
     protected void lnkmovie_Click(object sender, EventArgs e)
     {
         if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
         {

             productId = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["p"].ToString().Trim()));

         }
         DataTable dsp = classproduct.get_proc_ProductDescription_ProdFeature(productId);
         if (dsp.Rows.Count > 0)
         {
             string strprdnm = dsp.Rows[0]["productname"].ToString();
             string parentflag = dsp.Rows[0]["parentflag"].ToString();
             if (Page.User.Identity.IsAuthenticated)
             {

                 string struname = "";
                 DataTable dta = classaddress.getuserinfodetails(Page.User.Identity.Name);
                 if (dta.Rows.Count > 0)
                 {
                     struname = (dta.Rows[0]["username"]).ToString().Trim();
                 }

                 Response.Redirect(ReturnUrl("sitepathmain") + "watch.aspx?p=" + encryptPassword("p=" + productId) + "&em=" + encryptPassword("em=" + struname.ToLower().Trim()));

             }
             else
             {

                 string strurl = ReturnUrl("sitepathmain") + "watch.aspx?p=" + encryptPassword("p=" + productId);

                 Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?p=" + productId + "&ReturnUrl=" + strurl);
             }
         }

     }

     protected void lnktrailor_Click(object sender, EventArgs e)
     {
         if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
         {
             productId = Convert.ToInt32(Request.QueryString["p"].ToString());
         }
         DataTable dsp = classproduct.get_proc_ProductDescription_ProdFeature(productId);
         string strprdnm = dsp.Rows[0]["productname"].ToString();
         strur = UrlRewritingVM.getUrlRewritingInfo(strprdnm, productId, "t");
         Response.Redirect(strur);
     }
     protected void lnksubscribe_Click(object sender, EventArgs e)
     {
         DataTable dsp = classproduct.get_proc_ProductDescription_ProdFeature(productId);
         if (dsp.Rows.Count > 0)
         {
             string pkgflag = dsp.Rows[0]["parentflag"].ToString();
             string strprdnm = dsp.Rows[0]["productname"].ToString();

             if (pkgflag == "Premium")
             {
                 if (Page.User.Identity.IsAuthenticated)
                 {
                     Response.Redirect(ReturnUrl("sitepathmain") + "subscribe");
                 }
                 else
                 {
                     strur = UrlRewritingVM.getUrlRewritingInfo(strprdnm, productId, "EP");
                     Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?" + strur);
                 }
             }
             if (pkgflag == "Special")
             {
                 if (Page.User.Identity.IsAuthenticated)
                 {
                     string pid = "s=" + Convert.ToString(productId);
                     strur = UrlRewritingVM.getUrlRewritingInfo("", encryptPassword(pid), "s");
                     Response.Redirect(strur);
                 }
                 else
                 {
                     strur = UrlRewritingVM.getUrlRewritingInfo(strprdnm, productId, "SP");
                     Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?" + strur);
                 }
             }
         }
     }
     public string encryptPassword(string strText)
     {
         return Encrypt(strText);
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
     protected void lnklike_Click(object sender, EventArgs e)
     {
         if (Page.User.Identity.IsAuthenticated)
         {
             bool iserror = classreviews.insertdeletepostlikes(Page.User.Identity.Name, productId);
             if(!iserror)
             {
                 loaddata();
             }
         }
     }
     protected void lnkfav_Click(object sender, EventArgs e)
     {
         if (Page.User.Identity.IsAuthenticated)
         {
             DataTable dtrf = clswishlist.wishlist_GetSumOfProductFromQtyTable(Page.User.Identity.Name, Convert.ToInt32(productId));
             if (dtrf.Rows.Count > 0)
             {
                 if (Convert.ToInt32(dtrf.Rows[0]["SumQty"]) > 0)
                 {
                     bool m_index = clswishlist.Deletewishlistproduct1(productId, Page.User.Identity.Name.ToString());
                     loaddata();
                 }
                 else
                 {
                     bool strstring = clswishlist.insertwishlist(Page.User.Identity.Name, productId, "1", "", "");
                     DataSet dtuserid = classaddress.GetuserId(Page.User.Identity.Name.ToString());
                     loaddata();
                 }
             }   
         }
     }
     protected void lnkdislike_Click(object sender, EventArgs e)
     {
         if (Page.User.Identity.IsAuthenticated)
         {
             bool iserror = classreviews.insertdeletepostdislikes(Page.User.Identity.Name, productId);
             if (!iserror)
             {
                 loaddata();
             }
         }
     }
     protected void lnkenquiry_Click(object sender, EventArgs e)
     {
         if (Page.User.Identity.IsAuthenticated)
         {
             Response.Redirect(ReturnUrl("sitepathmain") + "enquiry/" + UrlRewritingVM.Encrypt(productId.ToString()));
         }
     }
}
