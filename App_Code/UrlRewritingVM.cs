using System;
using System.Data;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Web.SessionState;
using System.Configuration;



    /// <summary>

    /// <summary>
    /// Summary description for UrlRewritingVM
    /// </summary>


    //public class date
    //{
    //    //// create date time 2008-03-09 16:05:07.123
    //DateTime CurrentDateTime = DateTime.Now;
    //string new = CurrentDateTime.Text;
    //if(new = 2008-03-09 16:05:07.123)
    //{

    //}


    public static class UrlRewritingVM 
    {
       // public static string catid = "";

          public static int tcid=0;
         
         //sony added this
     //   HttpContext.Current.Session["tempcatid"]="0";
       //   public static int tempcatid = 0;
          //public static string tempcatid = HttpContext.Current.Session["tempcatid"].ToString();
          

        public static string getUrlRewritingInfo(object ur_name, object ur_id, string ur_type)
        {
            //SAGAR ADDED BELOW CODE FOR ADDING TIME LIMITATION IN PROJECT 17OCT2017 STARTS HERE
            //string date = "2017/10/17";
            //string currenttime = DateTime.Now.ToString("yyyy/MM/dd");

            //if (date == Convert.ToString(currenttime))
            //{
            //    HttpContext.Current.Response.Redirect("errorpg.aspx");
            //}
            //SAGAR ADDED BELOW CODE FOR ADDING TIME LIMITATION IN PROJECT 17OCT2017 ENDS HERE
			
            string sitepath = "";//creativeconfiguration.SitePathMain;
            string UrlRewriting = "";
            string urname = Convert.ToString(ur_name);
            string urid = Convert.ToString(ur_id);
                 

            sitepath = UrlRewritingVM.ChangeURL("sitepathmain");

            if (creativeconfiguration.EnableUrlRewriting)
            {
                
                if (ur_type == "PD")
                {
                    UrlRewriting = ProcessUrl(sitepath + "pd/" + ProcessUrlPD(urname.Trim())) + "/" + urid.ToString();
                }
                if (ur_type == "PDID")
                {
                    UrlRewriting = ProcessUrl(sitepath + "pd/") + urid.ToString();
                }
                if (ur_type == "PG")
                {
                //SAGAR COMMENTED BELOW LINE FOR REMOVING PHOTO-GALLERY METHOD FROM PHOTO CATEGORY 26OCT2017 
                UrlRewriting = ProcessUrl(sitepath + "photo-gallery/") + urid.ToString();

                //SAGAR ADDED BELOW LINE FOR PHOTO WITH DESCRIPTION WHEN WE OPEN ONE PHOTO FROM PHOTO GALLERY 26OCT2017
               // UrlRewriting = ProcessUrl(sitepath + "pd/") + urid.ToString();

                //sony commented above line and added below IF code to change link of achievements and time out as on 24 oct 2017
                //  HttpContext.Current.Response.Write(Decrypt(System.Web.Request.QueryString["c"]) + "HELLO" + Decrypt(urid.ToString().Trim()));
                //  Session["UserName"] = tbUserName.Text;

                    //sony added this code to display records of category achievements and timeout in ads type format
                    // Response.Write("Hello"+catid);
                    // Response.End();
                    //Session["tempcatid"];
                    //int tempcatid = 
                 //   HttpContext.Current.Response.Write("<br>HELLO" + UrlRewritingVM.tcid + urname + urid);
               
                //   httpContext.Current.Response.Write(Decrypt(HttpContext.Current.session["c"]) + "HELLO" + Decrypt(urid.ToString().Trim()));
                /*				  
                                  if(Decrypt(System.Web.Request.QueryString["c"]))
                                  if (Session["address"] == null)
                                   {
                                       HttpContext.Current.Response.Write("HELLO");
                                   }
                                   */
                /*
				Response.Write("HELLO"+urid.Trim());
				
					Response.End();
					if (urid.Trim() = 44)
					{
						Response.Write("HELLO"+urid.Trim());
						Response.End();
					}
					else
					{
					UrlRewriting = ProcessUrl(sitepath + "photo-gallery/") + urid.ToString();
					}
					*/
            }
            if (ur_type == "SR")
                {
                    UrlRewriting = ProcessUrl(sitepath + "survey/") + urid.ToString();
                }

                if (ur_type == "F")
                {
                    UrlRewriting = ProcessUrl(sitepath + "" + urname.ToLower().Trim());
                }
                if (ur_type == "PS")
                {
                    UrlRewriting = ProcessUrl(sitepath + "ps/") + urid.ToString().Trim();
                }
                if (ur_type == "R")
                {
                    UrlRewriting = ProcessUrl(sitepath + "ps/recent/");
                }
                if (ur_type == "SH")
                {
                    UrlRewriting = "p=" + urid + "&ReturnUrl=" + sitepath + "shoppingcart";
                }
                if (ur_type == "WH")
                {
                    UrlRewriting = "p=" + urid + "&ReturnUrl=" + sitepath + "procs/wishlist";
                }
                if (ur_type == "PE")
                {
                    UrlRewriting = ProcessUrl(sitepath + "enquiry/") + urid.ToString().Trim();
                }
                if (ur_type == "SA")
                {
                    UrlRewriting = ProcessUrl(sitepath + "search/" + urname.ToString().Trim() + "/");
                }

                if (ur_type == "w")
                {
                    UrlRewriting = sitepath + "watch/" + HttpUtility.UrlEncode(Encrypt(urid.Trim())) + "/" + ProcessUrlPD(urname.ToString().Trim());

                }

                if (ur_type == "t")
                {

                    UrlRewriting = sitepath + "trailor/" + (urid.ToLower().Trim()) + "/" + ProcessUrlPD(urname.ToString().Trim());

                }

                if (ur_type == "BD")
                {
                    UrlRewriting = ProcessUrl(sitepath + "ad/") + urid.ToString().Trim();
                }


                if (ur_type == "k")
                {

                    UrlRewriting = sitepath + "summary/premium/" + (urid.Trim());

                }

                if (ur_type == "s")
                {

                    UrlRewriting = sitepath + "summary/buy/" + (urid.Trim());
                }

                if (ur_type == "ws")
                {

                    UrlRewriting = (sitepath + "procs/wishlist");
                }
                if (ur_type == "o")
                {
                    UrlRewriting = ProcessUrl(sitepath + "o/" + urid.Trim() + "/" + urname.ToString().Trim());

                }

                if (ur_type == "pop")
                {
                    UrlRewriting = ProcessUrl(sitepath + "pop/" + urid.Trim() + "/" + urname.ToString().Trim());

                }

                if (ur_type == "EP")
                {
                    UrlRewriting = "p=" + urid + "&ReturnUrl=" + sitepath + "subscribe";
                }

                if (ur_type == "SP")
                {

                    UrlRewriting = "p=" + urid + "&ReturnUrl=" + sitepath + "summary/buy/";
                }


                if (ur_type == "PA")
                {
                    DataTable dta = classattribute.getattributegrbyattrid(urid.ToString().Trim());
                    if (dta.Rows.Count > 0)
                    {
                        string grnm = Convert.ToString(dta.Rows[0]["Name"]);

                        UrlRewriting = ProcessUrl(sitepath + "ps/" + grnm.Trim() + "/" + urname.Trim() + "/" + urid.ToString().Trim());
                    }
                }

            }
            else
            {
                if (ur_type == "PD")
                {
                    UrlRewriting = sitepath + "pdnew.aspx?p=" + urid;
                }
                if (ur_type == "SH")
                {
                    UrlRewriting = "p=" + urid + "&ReturnUrl=" + sitepath + "shoppingcart.aspx";
                }
                if (ur_type == "WH")
                {
                    UrlRewriting = "p=" + urid + "&ReturnUrl=" + sitepath + "procs/wishlist";
                }

                if (ur_type == "PS")
                {
                    UrlRewriting = sitepath + "ps.aspx?c=" + urid.ToString();
                }
                if (ur_type == "PE")
                {
                    UrlRewriting = sitepath + "productenquiry.aspx?p=" + urid.ToString();
                }
                if (ur_type == "SA")
                {
                    UrlRewriting = sitepath + "search.aspx?s=" + urname.ToString();
                }

                if (ur_type == "w")
                {
                    UrlRewriting = sitepath + "watch.aspx?p=" + urid;
                }

                if (ur_type == "t")
                {
                    UrlRewriting = sitepath + "wtrailor.aspx?p=" + urid;
                }
                if (ur_type == "BD")
                {
                    UrlRewriting = sitepath + "artistdetail.aspx?a=" + urid;
                }

                if (ur_type == "k")
                {
                    UrlRewriting = sitepath + "summery.aspx?k=" + urid;

                }

                if (ur_type == "s")
                {
                    UrlRewriting = sitepath + "summery.aspx?s=" + urid;

                }
            }
            return UrlRewriting;
        }

        //sony addedthis 
        //public static string makesession()
        //{
        //    string tempcatid = "";
        //    string tempcatid = HttpContext.Current.Request.Session["tempcatid"];

        //}

        //public static string Encrypt(string clearText)
        //{
        //    string EncryptionKey = "MAKV2SPBNI99212";
        //    byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(clearBytes, 0, clearBytes.Length);
        //                cs.Close();
        //            }
        //            clearText = Convert.ToBase64String(ms.ToArray());
        //        }
        //    }
        //   clearText = clearText.Replace("+", "-");
        //    return clearText;
        //}
        //public static string Decrypt(string cipherText)
        //{
        //    string EncryptionKey = "MAKV2SPBNI99212";
        //    cipherText = cipherText.Replace("-", "+");
        //    cipherText = cipherText.Replace(" ", "+");
        //    byte[] cipherBytes = Convert.FromBase64String(cipherText);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(cipherBytes, 0, cipherBytes.Length);
        //                cs.Close();
        //            }
        //            cipherText = Encoding.Unicode.GetString(ms.ToArray());
        //        }
        //    }
        //    return cipherText;
        //}
        public static string getUrlRewritingpost(object catname, object ur_id, string ur_type)
        {
            string UrlRewriting = "";

            string urid = Convert.ToString(ur_id);
            string sitepath = "";// creativeconfiguration.SitePathMain;
            sitepath = UrlRewritingVM.ChangeURL("sitepathmain");
            if (creativeconfiguration.EnableUrlRewriting)
            {
                if (ur_type == "APS")
                {
                    UrlRewriting = ProcessUrl(sitepath + "addpost/") + urid.ToString();
                }
            }
            else
            {
                if (ur_type == "APS")
                {
                    UrlRewriting = sitepath + "addpost.aspx?pid=" + urid;
                }
            }
            return UrlRewriting;
        }
        public static string getUrlRewritingProductSearch(int urcatid, string ur_brand, string ur_brandname, string ur_type)
        {
            string UrlRewriting = "";
            string urcatname = "";
            string urUrlid = "";
            if (urcatid != 0)
            {
                CategoryDetail mycategory = classcategory.getcategorydetails(urcatid);
                urcatname = Convert.ToString(mycategory.categoryname);
                urUrlid = Convert.ToString(urcatid);
            }
            string ur_brandid = Convert.ToString(ur_brand);
            string sitepath = "";//creativeconfiguration.SitePathMain;
            sitepath = UrlRewritingVM.ChangeURL("sitepathmain");
            if (creativeconfiguration.EnableUrlRewriting)
            {
                if (ur_type == "PS")
                {
                    if (ur_brandid.ToLower().Trim() != "")
                    {
                        UrlRewriting = ProcessUrl(sitepath + "ps/" + urcatname.ToLower().Trim() + "/" + urUrlid.ToString() + "/" + ur_brandname.ToString() + "/" + ur_brandid.ToString());
                    }
                    else
                    {
                        if (urcatname.ToLower().Trim() != "")
                        {
                            UrlRewriting = ProcessUrl(sitepath + "ps/" + urcatname.ToLower().Trim() + "/" + urUrlid.ToString().ToLower().Trim());
                        }
                        else
                        {
                            UrlRewriting = ProcessUrl(sitepath + "ps/all/");
                        }
                    }
                }
            }
            else
            {
                if (ur_type == "PS")
                {
                    if (ur_brandid.ToLower().Trim() == "")
                    {
                        UrlRewriting = sitepath + "ps.aspx?c=" + urcatid.ToString();
                    }
                    else
                    {
                        UrlRewriting = sitepath + "ps.aspx?c=" + urcatid.ToString() + "&br=" + ur_brandid;
                    }
                }
            }

            return UrlRewriting;
        }
        public static string getUrlRewritingProductSearchattr(int urcatid, string ur_brand, string ur_attr, string ur_aatrname, string ur_type)
        {
            string UrlRewriting = "";
            CategoryDetail mycategory = classcategory.getcategorydetails(urcatid);
            string urcatname = Convert.ToString(mycategory.categoryname);
            string urUrlid = Convert.ToString(urcatid);
            string ur_brandid = Convert.ToString(ur_brand);
            string ur_attrid = Convert.ToString(ur_attr);
            string sitepath = "";// creativeconfiguration.SitePathMain;
            sitepath = UrlRewritingVM.ChangeURL("sitepathmain");
            string urbrandname = "";

            if (ur_brandid != "")
            {
                DataTable dtart = classartist.getidbyartist(Convert.ToInt32(ur_brandid));
                urbrandname = dtart.Rows[0]["directorname"].ToString();
            }
            if (creativeconfiguration.EnableUrlRewriting)
            {
                if (ur_type == "PS")
                {
                    if (ur_brandid.ToLower().Trim() == "")
                    {
                        UrlRewriting = ProcessUrl(sitepath + "ps/" + urcatname.ToLower().Trim() + "/" + ur_brandid.ToString() + "/" + urUrlid.ToString() + "/" + ur_aatrname.ToString());
                    }
                    else
                    {
                        UrlRewriting = ProcessUrl(sitepath + "ps/" + urcatname.ToLower().Trim() + "/" + urUrlid.ToString() + "/" + ur_attrid.ToString());
                    }
                }

                else if (ur_type == "AS")
                {
                    UrlRewriting = ProcessUrl(sitepath + "as/" + urbrandname.ToString() + "/" + ur_brandid.ToString());
                }
            }
            else
            {
                if (ur_type == "PS")
                {
                    if (ur_brandid.ToLower().Trim() == string.Empty)
                    {
                        if (ur_brandid == "")
                        {
                            ur_brandid = "0";
                        }
                        if (ur_brandid.ToString().Trim() == "0")
                        {
                            UrlRewriting = sitepath + "ps.aspx?c=" + urcatid.ToString();
                        }
                        else
                        {
                            UrlRewriting = sitepath + "ps.aspx?c=" + urcatid.ToString() + "&br=" + ur_brandid;
                        }
                    }
                    else
                    {
                        if (ur_brandid.ToString() == string.Empty)
                        {
                            ur_brandid = "0";
                        }
                        if (ur_brandid.ToString().Trim() == "0")
                        {
                            UrlRewriting = sitepath + "ps.aspx?c=" + urcatid.ToString() + "&a=" + ur_attr;
                        }
                        else
                        {
                            UrlRewriting = sitepath + "ps.aspx?c=" + urcatid.ToString() + "&br=" + ur_brandid + "&a=" + ur_attr;
                        }
                    }
                }
            }
            return UrlRewriting;
        }
        public static string Encrypt(string clearText)
        {
            try
            {
                license exp = new license();
                string EncryptionKey = exp.key;
                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                using (Aes encryptor = Aes.Create())
                {
                    //encryptor.
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                    }
                }
                clearText = clearText.Replace("+", "-").Replace("/", "_");
            }
            catch (Exception)
            {

            }

            return clearText;
        }
        public static string Decrypt(string cipherText)
        {
            try
            {
                license exp = new license();
                string EncryptionKey = exp.key;
                cipherText = cipherText.Replace("-", "+").Replace("_", "/");
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            encryptor.Padding = PaddingMode.PKCS7;
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.FlushFinalBlock();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception)
            {

            }

            return cipherText;
        }
        public static string ProcessUrl(string url)
        {
            string newUrl = url;
            newUrl = newUrl.ToLower();
            //replace spaces with hyphens(-)
            newUrl = newUrl.Replace(" ", "-");
            newUrl = newUrl.Replace("  ", "-");
            newUrl = newUrl.Replace("'", "");
            newUrl = newUrl.Replace("%20", "-");
            newUrl = newUrl.Replace("&", "");
            newUrl = newUrl.Replace("!", "");
            newUrl = newUrl.Replace("@", "");
            newUrl = newUrl.Replace(",", "");
            newUrl = newUrl.Replace("and", "");
            newUrl = newUrl.Replace("#", "");
            newUrl = newUrl.Replace("$", "");
            newUrl = newUrl.Replace("%", "");
            newUrl = newUrl.Replace("^", "");
            newUrl = newUrl.Replace("*", "");
            newUrl = newUrl.Replace("(", "");
            newUrl = newUrl.Replace(")", "");
            newUrl = newUrl.Replace("{", "");
            newUrl = newUrl.Replace("}", "");
            newUrl = newUrl.Replace("[", "");
            newUrl = newUrl.Replace("]", "");
            newUrl = newUrl.Replace("<", "");
            newUrl = newUrl.Replace(">", "");
            newUrl = newUrl.Replace("_", "");
            newUrl = newUrl.Replace("|", "");
            newUrl = newUrl.Replace("~", "");
            newUrl = newUrl.Replace(". ", "");
            newUrl = newUrl.Replace("./", "/");
            newUrl = newUrl.Replace(" .", "");
            newUrl = newUrl.Replace("`", "");
            newUrl = newUrl.Replace("+", "-");
            newUrl = newUrl.Replace('"', ' ');
            newUrl = newUrl.Replace("%3d", "=");
            newUrl = newUrl.Replace("  ", " ");
            newUrl = newUrl.Replace("   ", " ");
            newUrl = newUrl.Replace(" ", "-");
            newUrl = newUrl.Replace("%2c", ",");
            newUrl = newUrl.Replace("%0A", "-");
            newUrl = newUrl.Replace("%0a", "-");
            newUrl = newUrl.Replace("\n", "-");
            newUrl = newUrl.Replace("\r", "-");
            newUrl = newUrl.Replace("---", "-");
            newUrl = newUrl.Replace("--", "-");
            return newUrl;
        }
        public static string ProcessUrlPD(string url)
        {
            string newUrl = url;
            newUrl = newUrl.ToLower();
            newUrl = newUrl.Replace(":", "-");
            newUrl = newUrl.Replace("%0A", "-");
            newUrl = newUrl.Replace("%0a", "-");
            newUrl = newUrl.Replace("\n", "-");
            newUrl = newUrl.Replace("\r", "-");
            newUrl = newUrl.Replace(".", "-");
            newUrl = newUrl.Replace(" ", "-");
            newUrl = newUrl.Replace("  ", "-");
            newUrl = newUrl.Replace("'", "");
            newUrl = newUrl.Replace("%20", "-");
            newUrl = newUrl.Replace("&", "and");
            newUrl = newUrl.Replace("!", "");
            newUrl = newUrl.Replace("@", "");
            newUrl = newUrl.Replace("#", "");
            newUrl = newUrl.Replace("$", "");
            newUrl = newUrl.Replace("%", "");
            newUrl = newUrl.Replace("^", "");
            newUrl = newUrl.Replace("*", "");
            newUrl = newUrl.Replace("(", "");
            newUrl = newUrl.Replace(")", "");
            newUrl = newUrl.Replace("{", "");
            newUrl = newUrl.Replace("}", "");
            newUrl = newUrl.Replace("[", "");
            newUrl = newUrl.Replace("]", "");
            newUrl = newUrl.Replace("<", "");
            newUrl = newUrl.Replace(">", "");
            newUrl = newUrl.Replace("_", "");
            newUrl = newUrl.Replace("|", "");
            newUrl = newUrl.Replace("~", "");
            newUrl = newUrl.Replace(". ", "");
            newUrl = newUrl.Replace("./", "/");
            newUrl = newUrl.Replace(" .", "");
            newUrl = newUrl.Replace("`", "");
            newUrl = newUrl.Replace("+", "-");
            newUrl = newUrl.Replace('"', ' ');
            newUrl = newUrl.Replace("%3d", "=");
            newUrl = newUrl.Replace("  ", " ");
            newUrl = newUrl.Replace("   ", " ");
            newUrl = newUrl.Replace(" ", "-");
            newUrl = newUrl.Replace("%2c", ",");
            newUrl = newUrl.Replace("%0A", "-");
            newUrl = newUrl.Replace("%0a", "-");
            newUrl = newUrl.Replace("\n", "-");
            newUrl = newUrl.Replace("\r", "-");
            newUrl = newUrl.Replace("---", "-");
            newUrl = newUrl.Replace("--", "-");
            return newUrl;
        }

        #region Highbar Production server Link
        public static string ChangeURL(string filepath)
        {
            string strpath = "";
            HttpCookie cookie = HttpContext.Current.Request.Cookies["internet"];
            //string sessionvalue = "no session";
            //if (HttpContext.Current.Session["internet"] != null)
            //    sessionvalue = HttpContext.Current.Session["internet"].ToString();
            //HttpContext.Current.Response.Write(sessionvalue);
			//String strSitepath = "http://localhost/hrms/";
			String strSitepath = "http://localhost/hrms/";
            if (cookie != null)
            {
                if (cookie.Value.ToString().ToLower().Trim() == "true")
                {
                    if (filepath == "sitepathmain")
                    {
                        strpath = "http://intranet.highbartech.com/hrms/";
                    }
                    if (filepath == "adminpath")
                    {
                        strpath = "http://intranet.highbartech.com/hrmsadmin/";
                    }
                    else if (filepath == "sitepathadmin")
                    {
                        strpath = "http://intranet.highbartech.com/hrmsadmin/";
                    }
                    else if (filepath == "adminsitepath")
                    {
                        strpath = "http://intranet.highbartech.com/hrmsadmin/";
                    }
                    else if (filepath == "css")
                    {
                        strpath = "http://intranet.highbartech.com/hrms/CSS/creative1.0/";
                    }
                    else if (filepath == "controls")
                    {
                        strpath = "http://intranet.highbartech.com/hrms/themes/creative1.0/LayoutControls/";
                    }
                    else if (filepath == "js")
                    {
                        strpath = "http://intranet.highbartech.com/hrms/themes/creative1.0/js/";
                    }
                    else if (filepath == "sitepath")
                    {
                        strpath = "http://intranet.highbartech.com/hrms/themes/creative1.0/";
                    }
                    else if (filepath == "image")
                    {
                        strpath = "http://intranet.highbartech.com/hrms/images/";
                    }
                    else if (filepath == "mediapath")
                    {
                        strpath = "http://intranet.highbartech.com/hrms/themes/creative1.0/images/";
                    }
                    else if (filepath == "chatapp")
                    {
                        strpath = "http://intranet.highbartech.com/hccchat/";
                    }
                    else
                    {
                        strpath = "http://intranet.highbartech.com/hrms/";
                    }
                }
                else
                {
                    if (filepath == "sitepathmain")
                    {
                        strpath = "http://localhost/hrms/";
                    }
                    if (filepath == "adminpath")
                    {
                        strpath = "http://localhost/hrmsadmin/";
                    }
                    else if (filepath == "sitepathadmin")
                    {
                        strpath = "http://localhost/hrmsadmin/";
                    }
                    else if (filepath == "adminsitepath")
                    {
                        strpath = "http://localhost/hrmsadmin/";
                    }
                    else if (filepath == "css")
                    {
                        strpath = "http://localhost/hrms/CSS/creative1.0/";
                    }
                    else if (filepath == "controls")
                    {
                        strpath = "http://localhost/hrms/themes/creative1.0/LayoutControls/";
                    }
                    else if (filepath == "js")
                    {
                        strpath = "http://localhost/hrms/themes/creative.0/js/";
                    }
                    else if (filepath == "sitepath")
                    {
                        strpath = "http://localhost/hrms/themes/creative1.0/";
                    }
                    else if (filepath == "image")
                    {
                        strpath = "http://localhost/hrms/images/";
                    }
                    else if (filepath == "mediapath")
                    {
                        strpath = "http://localhost/hrms/themes/creative1.0/images/";
                    }
                    else if (filepath == "chatapp")
                    {
                        strpath = "http://localhost/hccchat/";
                    }
                    else
                    {
                        strpath = "http://localhost/hrms/";
                    }
                }
            }
            else
            {
                string URL = HttpContext.Current.Request.Url.AbsoluteUri;
                //if (URL.Contains("HRMS_ADMIN"))
                if (URL.Contains("localhost"))
                {
                    strpath = "http://localhost/hrms/";
                }
                else if (URL.Contains("intranet.highbartech.com"))
                {
                    strpath = "http://intranet.highbartech.com/hrms/";
                }
                else if (string.IsNullOrWhiteSpace(URL))
                {
                    strpath = "http://localhost/hrms/";
                }

                //added by sony
                if (filepath == "sitepathmain")
                {
                    strpath = "http://localhost/hrms/";
                }
                if (filepath == "adminpath")
                {
                    strpath = "http://localhost/hrmsadmin/";
                }
                else if (filepath == "sitepathadmin")
                {
                    strpath = "http://localhost/hrmsadmin/";
                }
                else if (filepath == "adminsitepath")
                {
                    strpath = "http://localhost/hrmsadmin/";
                }
                else if (filepath == "css")
                {
                    strpath = "http://localhost/hrms/CSS/creative1.0/";
                }
                else if (filepath == "controls")
                {
                    strpath = "http://localhost/hrms/themes/creative1.0/LayoutControls/";
                }
                else if (filepath == "js")
                {
                    strpath = "http://localhost/hrms/themes/creative.0/js/";
                }
                else if (filepath == "sitepath")
                {
                    strpath = "http://localhost/hrms/themes/creative1.0/";
                }
                else if (filepath == "image")
                {
                    strpath = "http://localhost/hrms/images/";
                }
                else if (filepath == "mediapath")
                {
                    strpath = "http://localhost/hrms/themes/creative1.0/images/";
                }
                else if (filepath == "chatapp")
                {
                    strpath = "http://localhost/hccchat/";
                }
                else
                {
                    strpath = "http://localhost/hrms/";
                }
                //sony code ends here
            }
			
			#region Return URL Code Created 
             //strpath ="http://localhost/hrms/";
            
            if (filepath == "ongoingProjectonHome")
            {
                strpath = strSitepath + "ongoing.aspx";
            }
            
            if (filepath == "LeaveonHome")
            {
                strpath = strSitepath + "procs/Leaves.aspx";
            }
            if (filepath == "AttendanceonHome")
            {
                //strpath = strSitepath + "procs/ComingSoon.aspx";
                strpath = strSitepath + "procs/Attendance.aspx";
            }
            if (filepath == "TravelonHome")
            {
                //strpath = strSitepath + "procs/travelindex.aspx";
                strpath = strSitepath + "procs/ComingSoon.aspx";
            }
            if (filepath == "EmployeeRef")
            {
                //strpath = strSitepath + "procs/travelindex.aspx";
                strpath = strSitepath + "procs/ComingSoon.aspx";
            }
            if (filepath == "Timesheet")
            {
                //strpath = strSitepath + "procs/ComingSoon.aspx";
                strpath = strSitepath + "procs/Timesheet.aspx";
            }
            if (filepath == "reimbursementonHome")
            {
                //strpath = strSitepath + "procs/Reembursementindex.aspx";
                strpath = strSitepath + "Claims.aspx";
            }

            if (filepath == "trainingonHome")
            {
                strpath = strSitepath + "ps/2StP8p8qHBLWgynfFDTiUA==";
            }

            if (filepath == "EmpDirectonHome")
            {
                strpath = strSitepath + "contacts.aspx";
            }
            
            if (filepath == "newsletteronHome")
            {
                strpath = strSitepath + "newslettersnew.aspx";
            }

            if (filepath == "KeyPersonnel")
            {
                strpath = strSitepath + "KeyPersonnel.aspx";
            }
            if (filepath == "Helpdesk")
            {
                //strpath = strSitepath + "procs/travelindex.aspx";
                //strpath = strSitepath + "procs/ComingSoon.aspx";
                strpath = strSitepath + "procs/Service.aspx";
            }
            if (filepath == "homegallery")
            {
                strpath = strSitepath + "GalleryHome.aspx";
            }
            if (filepath == "AchivemnetonHome")
            {
                //strpath = strSitepath + "pd/8MYXO648FEiHqQ-zE5Dn0w==";
				strpath = strSitepath + "ps/xEUkUkcMeJdzvZ5XMig6vw==";
            }
            if (filepath == "ProceduresonHome")
            {
                strpath = strSitepath + "PolicyProcedure.aspx";
            }
            if (filepath == "KnowledgeCentonHome")
            {
                strpath = strSitepath + "KnowledgeCenter.aspx";
            }
            if (filepath == "CommunicationonHome")
            {
                strpath = strSitepath + "Communications.aspx";
            }
            if (filepath == "TimeOutonHome")
            {
                //strpath = strSitepath + "pd/4yjPj0GRCGXpQICmAI6STA==";
				strpath = strSitepath + "ps/OQtopggfHQG3pnGausNHAw==";
            }
            if (filepath == "IMSonprocedure")
            {
                strpath = strSitepath + "IMS.aspx";
            }
            if (filepath == "HRonprocedure")
            {
                strpath = strSitepath + "hr.aspx";
            }
            if (filepath == "COSonprocedure")
            {
                strpath = strSitepath + "cos.aspx";
            }
            if (filepath == "CConprocedure")
            {
                strpath = strSitepath + "cc.aspx";
            }
            if (filepath == "CSRonprocedure")
            {
                strpath = strSitepath + "csr.aspx";
            }
            if (filepath == "policiesonprocedure")
            {
                strpath = strSitepath + "policies";
            }
            if (filepath == "projectOnOngoingpage")
            {
                strpath = strSitepath + "projectdetail.aspx";
            }
			if (filepath == "pressreleaseOnCommunication")
            {
                strpath = strSitepath + "pressrelease.aspx";
            }
			
			 //Added on 20.12.2018
            if (filepath == "compeletedprojectHome")
            {
                strpath = strSitepath + "completed.aspx";
            }
            if (filepath == "gallaryhome")
            {
                strpath = strSitepath + "GalleryHome.aspx";
            }

            if (filepath == "HydroPower")
            {
                //strpath = strSitepath + "ps/6b0kgTGNAapd3n28TD_vig==";
                strpath = strSitepath + "ps/OQtopggfHQG3pnGausNHAw==";
            }

            if (filepath == "NuclearSpecialProjects")
            {
                strpath = strSitepath + "ps/Ut7_RTkgp0vF_-ZEIiFlZQ==";
            }
            if (filepath == "Transportation")
            {
                strpath = strSitepath + "ps/hqjrtcrY5IJTEd2cPsDBFw==";
            }
            if (filepath == "WaterSolutions")
            {
                strpath = strSitepath + "ps/LDvv5iC7MC4UPbvLBTVjRQ==";
            }

            if (filepath == "BuildingsIndustrialPlants")
            {
                strpath = strSitepath + "ps/0JKuwKKfZXmayODjj6E9tQ==";
            }
            if (filepath == "hccurlmain")
            {
                strpath = "http://localhost/hrms" ;
            }
            //****************************//
			
			
			if (filepath == "Appraisal")
            {
                //strpath = strSitepath + "procs/Appraisalindex.aspx";
                strpath = strSitepath + "procs/Requisition_Index.aspx";
                //strpath = strSitepath + "procs/ComingSoon.aspx";
            }

            if (filepath == "ideacntralhome")
            {
                //strpath = strSitepath + "ps.aspx?c=uuU0JEd0-9BdrjPdeKlAmg==";
				strpath = strSitepath + "pmmeets.aspx";
            }
			
			if (filepath == "myfiles")
            {
                strpath = strSitepath + "PersonalDocuments.aspx";
            }

			
            #endregion
			
  
            return strpath;
        }
        #endregion

        //#region Creative Offline
        //public static string ChangeURL(string filepath)
        //{
        //    string strpath = "";
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies["internet"];
        //    if (cookie != null)
        //    {
        //        if (cookie.Value.ToString().ToLower().Trim() == "true")
        //        {
        //            if (filepath == "sitepathmain")
        //            {
        //                strpath = "http://192.168.0.172/hrms/";
        //            }
        //            else if (filepath == "css")
        //            {
        //                strpath = "http://192.168.0.172/hrms/CSS/creative1.0/";
        //            }
        //            else if (filepath == "controls")
        //            {
        //                strpath = "http://192.168.0.172/hrms/themes/creative1.0/LayoutControls/";
        //            }
        //            else if (filepath == "js")
        //            {
        //                strpath = "http://192.168.0.172/hrms/themes/creative1.0/js/";
        //            }
        //            else if (filepath == "sitepath")
        //            {
        //                strpath = "http://192.168.0.172/hrms/themes/creative1.0/";
        //            }
        //            else if (filepath == "image")
        //            {
        //                strpath = "http://192.168.0.172/hrms/images/";
        //            }
        //            else if (filepath == "mediapath")
        //            {
        //                strpath = "http://192.168.0.172/hrms/themes/creative1.0/images/";
        //            }
        //            else if (filepath == "chatjs")
        //            {
        //                strpath = "http://192.168.0.172/hrms/themes/creative1.0/images/";
        //            }
        //            else if (filepath == "chatapp")
        //            {
        //                strpath = "http://192.168.0.72/creativechat/";
        //            }
        //            else
        //            {
        //                strpath = "http://192.168.0.172/hrms/";
        //            }
        //        }
        //        else
        //        {
        //            if (filepath == "sitepathmain")
        //            {
        //                strpath = "http://192.168.0.172/HRMS_ADMIN/";
        //            }
        //            else if (filepath == "css")
        //            {
        //                strpath = "http://192.168.0.172/HRMS_ADMIN/CSS/creative1.0/";
        //            }
        //            else if (filepath == "controls")
        //            {
        //                strpath = "http://192.168.0.172/HRMS_ADMIN/themes/creative1.0/LayoutControls/";
        //            }
        //            else if (filepath == "js")
        //            {
        //                strpath = "http://192.168.0.172/HRMS_ADMIN/themes/creative1.0/js/";
        //            }
        //            else if (filepath == "sitepath")
        //            {
        //                strpath = "http://192.168.0.172/HRMS_ADMIN/themes/creative1.0/";
        //            }
        //            else if (filepath == "image")
        //            {
        //                strpath = "http://192.168.0.172/HRMS_ADMIN/images/";
        //            }
        //            else if (filepath == "mediapath")
        //            {
        //                strpath = "http://192.168.0.172/HRMS_ADMIN/themes/creative1.0/images/";
        //            }
        //            else if (filepath == "chatjs")
        //            {
        //                strpath = "http://192.168.0.172/HRMS_ADMIN/themes/creative1.0/images/";
        //            }
        //            else if (filepath == "chatapp")
        //            {
        //                strpath = "http://192.168.0.72/creativechat/";
        //            }
        //            else
        //            {
        //                strpath = "http://192.168.0.172/HRMS_ADMIN/";
        //            }
        //        }
        //    }
        //    else
        //    {
        //        string URL = HttpContext.Current.Request.Url.AbsoluteUri;
        //        string path = "";
        //        if (URL.Contains("192.168.0.172/HRMS_ADMIN/"))
        //        {
        //            path = "http://192.168.0.172/HRMS_ADMIN/";
        //        }
        //        else if (URL.Contains("192.168.0.172/hrms/"))
        //        {
        //            path = "http://192.168.0.172/hrms/";
        //        }
        //    }
        //    return strpath;
        //}
        //#endregion

        #region Highbar Online Demo
        //public static string ChangeURL(string filepath)
        //{
        //    string strpath = "";
        //    if (HttpContext.Current.Request.Cookies["internet"] != null)
        //    {
        //        if (HttpContext.Current.Request.Cookies["internet"].Value.ToString().ToLower() == "true")
        //        {
        //            if (filepath == "sitepathmain")
        //            {
        //                strpath = "http://highbartech.com.iis3004.shared-servers.com/";
        //            }
        //            else if (filepath == "css")
        //            {
        //                strpath = "http://highbartech.com.iis3004.shared-servers.com/CSS/creative1.0/";
        //            }
        //            else if (filepath == "controls")
        //            {
        //                strpath = "http://highbartech.com.iis3004.shared-servers.com/themes/creative1.0/LayoutControls/";
        //            }
        //            else if (filepath == "js")
        //            {
        //                strpath = "http://highbartech.com.iis3004.shared-servers.com/themes/creative1.0/js/";
        //            }
        //            else if (filepath == "sitepath")
        //            {
        //                strpath = "http://highbartech.com.iis3004.shared-servers.com/themes/creative1.0/";
        //            }
        //            else if (filepath == "image")
        //            {
        //                strpath = "http://highbartech.com.iis3004.shared-servers.com/images/";
        //            }
        //            else if (filepath == "mediapath")
        //            {
        //                strpath = "http://highbartech.com.iis3004.shared-servers.com/images/";
        //            }
        //            else
        //            {
        //                strpath = "";
        //            }
        //        }
        //        else
        //        {
        //            if (filepath == "sitepathmain")
        //            {
        //                strpath = "http://www.highbartech.com.iis3004.databasemart.net/";
        //            }
        //            else if (filepath == "css")
        //            {
        //                strpath = "http://www.highbartech.com.iis3004.databasemart.net/CSS/creative1.0/";
        //            }
        //            else if (filepath == "controls")
        //            {
        //                strpath = "http://www.highbartech.com.iis3004.databasemart.net/themes/creative1.0/LayoutControls/";
        //            }
        //            else if (filepath == "js")
        //            {
        //                strpath = "http://www.highbartech.com.iis3004.databasemart.net/themes/creative1.0/js/";
        //            }
        //            else if (filepath == "sitepath")
        //            {
        //                strpath = "http://www.highbartech.com.iis3004.databasemart.net/themes/creative1.0/";
        //            }
        //            else if (filepath == "image")
        //            {
        //                strpath = "http://www.highbartech.com.iis3004.databasemart.net/images/";
        //            }
        //            else if (filepath == "mediapath")
        //            {
        //                strpath = "http://www.highbartech.com.iis3004.databasemart.net/themes/creative1.0/images/";
        //            }
        //            else
        //            {
        //                strpath = "";
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (filepath == "sitepathmain")
        //        {
        //            strpath = "http://www.highbartech.com.iis3004.databasemart.net/";
        //        }
        //        else if (filepath == "css")
        //        {
        //            strpath = "http://www.highbartech.com.iis3004.databasemart.net/CSS/creative1.0/";
        //        }
        //        else if (filepath == "controls")
        //        {
        //            strpath = "http://www.highbartech.com.iis3004.databasemart.net/themes/creative1.0/LayoutControls/";
        //        }
        //        else if (filepath == "js")
        //        {
        //            strpath = "http://www.highbartech.com.iis3004.databasemart.net/themes/creative1.0/js/";
        //        }
        //        else if (filepath == "sitepath")
        //        {
        //            strpath = "http://www.highbartech.com.iis3004.databasemart.net/themes/creative1.0/";
        //        }
        //        else if (filepath == "image")
        //        {
        //            strpath = "http://www.highbartech.com.iis3004.databasemart.net/images/";
        //        }
        //        else if (filepath == "mediapath")
        //        {
        //            strpath = "http://www.highbartech.com.iis3004.databasemart.net/themes/creative1.0/images/";
        //        }
        //        else
        //        {
        //            strpath = "";
        //        }
        //    }
        //    return strpath;
        //}
        #endregion
    }
//}
