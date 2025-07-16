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
using System.Web.Security;
using System.Net.Mime;
using System.Net;
//using System.Text.RegularExpressions;

public partial class Themes_SecondTheme_LayoutControls_pdnew_pdsumtitle : System.Web.UI.UserControl
{
    private static int productId;
    public static string catid ;
    public static string catname;
    //public static string catId = "";
    public string strur;
    public string ipaddress;
    public string countrycode;
    public static string img="";
    public static string exticon = "";
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
    {
       
        //        catid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["pid"]));
        //catId = Convert.ToInt32(Request.QueryString["catid"]);
            
        //Response.Write(catid);
        //Response.End();
        //ShowFavoriteArticles(productId);
        try
        {

            //if (Request.QueryString["c"] != null || Request.QueryString["c"] != "")
            //{
            //    catid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["c"].ToString()));
            //}
            //Response.Write(catid);
            //Response.End();
            //if (Request.QueryString["c"] != "" && Request.QueryString["c"] != null)
            //{
            //    catid = UrlRewritingVM.Decrypt(Request.QueryString["c"].ToString());
            //}



              //if (Int32.TryParse(UrlRewritingVM.Decrypt(Request.QueryString["c"]), out cid))
              //  {
              //     catid = UrlRewritingVM.Decrypt(Request.QueryString["c"].ToString());
              //    }
            //Response.Write(catid);
            //Response.End();
          
            if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
            {
                productId = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["p"].ToString()));
            }

            //   public void linkview(){
            //DataTable dtc = classrecommend.getproductcategorybyid(productId);
            //if (dtc.Rows.Count > 0)
            //{


            //}
            //   }
            //Response.Write(catid);
            //Response.End();
            //if (dtc.Rows.Count > 0)
            //{

            //    for (int j = 0; j < dtc.Rows.Count; j++)
            //    {
            //        catid = Convert.ToString(dtc.Rows[j]["categoryid"]);
            //        catname = Convert.ToString(dtc.Rows[j]["categoryname"]);
            //    }
            //}
            //Response.Write(catid);
            //Response.End();
                         //Response.Write(catname);
                         //Response.End();
            //if (Request.QueryString["c"] != "" && Request.QueryString["c"] != null)
            //{
            //    catid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["c"].ToString()));
            //}
            //Response.Write(productId);
            //Response.End();
            //Response.Write(catid);
            //Response.End();
            if (!IsPostBack)
            {
                
                ipaddress = getRemoteAddr();
                countrycode = GetMaxMindOmniData(ipaddress);
                int countrycount = classcountry.getcountrycount(productId, countrycode);
                if (countrycount > 0)
                {

                    lnktrailor.Visible = true;
                    divtrailer.Visible = true;
                    lnkprodenquiry.Visible = false;
                }
                else
                {
                    lnkprodenquiry.Visible = true;
                }
                
                loaddata();
                //linkview();
                
                //ShowFavoriteArticles(productId);
              
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
   

    public void loaddata() 
    {
        if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
        {
            productId = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["p"].ToString()));
        }
      
        //DataTable dtc = classrecommend.getproductbycategorynameid(productId);
        //if (dtc.Rows.Count > 0)
        //{
        //    ln.DataSource = dtc;
        //    ln.DataBind();
        //}
        DataTable dir = classdirector.getalldirectorbyid(productId);
        if (dir.Rows.Count > 0)
        {
            lnkdirector.HRef = ReturnUrl("sitepathmain") + "ad/" + dir.Rows[0]["directorid"].ToString();
            lbldirectorname.Text = dir.Rows[0]["directorname"].ToString();
        }
        else
        {
            director.Visible = false;
            lbldirectorname.Visible = false;
        }

        DataTable dt1 = classproduct.getuserbypost(productId);
        if(dt1.Rows.Count>0)
        {
            //SAGAR COMMENTED BELOW CODE FOR REMOVING NAME OF THE USER WHO POSTED THE POST 9OCT2017 STARTS HERE
            //lnkuser.HRef = ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(dt1.Rows[0]["indexid"].ToString());

            //if (dt1.Rows[0]["profilephoto"].ToString().Length > 0)
            //{
            //    profileimg.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/" + dt1.Rows[0]["profilephoto"].ToString(); ;
            //}
            //else
            //{
            //    profileimg.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/noimage1.png";
            //}
            //lbluser.Text = dt1.Rows[0]["fullname"].ToString();
            //SAGAR COMMENTED BELOW CODE FOR REMOVING NAME OF THE USER WHO POSTED THE POST 9OCT2017 ENDS HERE

            DateTime pdt,start,end;

            pdt = Convert.ToDateTime(dt1.Rows[0]["createdon"].ToString());
            //SAGAR COMMENTED THIS FOR REMOVING LOGIC OF POSTED DATE ,START DATE AND END DATE OF SINGLE POST 12OCT2017 STARTS HERE
            //lbldate.Text = pdt.ToString("dd MMM, yyyy");
            //lblpdate.Text = pdt.Day.ToString();
            //lblpmonth.Text = pdt.ToString("MMM");
            start = Convert.ToDateTime(dt1.Rows[0]["startdate"].ToString());
            //lblsdate.Text = start.Day.ToString();
            //lblsmonth.Text = start.ToString("MMM");
            end = Convert.ToDateTime(dt1.Rows[0]["enddate"].ToString());
            //lbledate.Text = end.Day.ToString();
            //lblemonth.Text = end.ToString("MMM");
            //SAGAR COMMENTED THIS FOR REMOVING LOGIC OF POSTED DATE ,START DATE AND END DATE OF SINGLE POST 12OCT2017 ENDS HERE
        }
        else
        {
            //div1.Visible = false;
            //div2.Visible = false;
        }

       
        DataTable ds = classproduct.get_proc_ProductDescription_ProdFeature(productId);
        if (ds.Rows.Count > 0)
        {
             lblproductname.Text = ds.Rows[0]["productname"].ToString();
             lbldescription.Text = ds.Rows[0]["shortdescription"].ToString();
             DataTable dtmuldoc = classUniversal.getUnivesalFileByProdIDandFiletype(productId,"D");
             if (dtmuldoc.Rows.Count > 0)
             {
                 div3.Visible = false;
                 #region Multiple document display section
                 rptdoc.DataSource = dtmuldoc;
                 rptdoc.DataBind();
                 if (rptdoc.Items.Count > 1)
                 {
                     multipledoc.Visible = true;
                     for (int i = 0; i < rptdoc.Items.Count; i++)
                      {
                          Image extimg1 = (Image)rptdoc.Items[i].FindControl("extimg1");
                          exticon = extimg1.AlternateText;
                          int z = 0;
                          z = exticon.LastIndexOf('.');
                          int x = 0;
                          x = exticon.Length;
                          string ext = exticon.Substring(z, x - z);
                          if (ext == ".pdf")
                          {
                              extimg1.ImageUrl = ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/pdf1hover.png";
                          }
                          else if (ext == ".doc")
                          {
                              extimg1.ImageUrl = ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/doc1hover.png";
                          }
                          else if (ext == ".docx")
                          {
                              extimg1.ImageUrl = ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/doc1hover.png";
                          }
                          else if (ext == ".xls")
                          {
                              extimg1.ImageUrl = ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/xls1hover.png";
                          }
                          else if (ext == ".xlsx")
                          {
                              extimg1.ImageUrl = ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/xls1hover.png";
                          }
                          else if (ext == ".ppt")
                          {
                              extimg1.ImageUrl = ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/ppthover.png";
                          }
                          else if (ext == ".pptx")
                          {
                              extimg1.ImageUrl = ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/ppthover.png";
                          }
                      }
                 }
                 #endregion
             }
             else
             {
                 #region Single document display section
                 string file = "";
                 file = ds.Rows[0]["filename"].ToString();
                 if (file != "" && file != "N/A" && file != "n/a" && file != "null")
                 {
                     div3.Visible = true;
                     pstimg.Visible = false;
                     pstvideo.Visible = false;
                     int z = 0;
                     z = file.LastIndexOf('.');
                     int x = 0;
                     x = file.Length;
                     string ext = file.Substring(z, x - z);
                     if (ext == ".pdf")
                     {
                         extimg.ImageUrl = ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/pdf1hover.png";
                     }
                     else if (ext == ".doc")
                     {
                         extimg.ImageUrl = ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/doc1hover.png";
                     }
                     else if (ext == ".docx")
                     {
                         extimg.ImageUrl = ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/doc1hover.png";
                     }
                     else if (ext == ".xls")
                     {
                         extimg.ImageUrl = ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/xls1hover.png";
                     }
                     else if (ext == ".xlsx")
                     {
                         extimg.ImageUrl = ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/xls1hover.png";
                     }
                     else if (ext == ".ppt")
                     {
                         extimg.ImageUrl = ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/ppthover.png";
                     }
                     else if (ext == ".pptx")
                     {
                         extimg.ImageUrl = ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/ppthover.png";
                     }
                     fileload.Text = file;
                     string filepath = Request.PhysicalApplicationPath + "\\files\\" + file;
                     if (File.Exists(filepath))
                     {
                         download.HRef = ReturnUrl("sitepathmain") + "files/" + file;
                     }
                     else
                     {
                         download.HRef = ConfigurationManager.AppSettings["sitepathadmin"]+ "files/" + file;
                     }
                 }
                 else
                 {
                     div3.Visible = false;
                 }
                 #endregion
             }
           

            string strflag = ds.Rows[0]["parentflag"].ToString();
            if (ds.Rows[0]["parentflag"].ToString() == "Free")
            {
                spanpkg.Visible = false;
            }
            if (ds.Rows[0]["product_year"].ToString() == "Year")
            {
                lblyear.Text = "";
            }
            else
            {
                if (dir.Rows.Count > 0)
                {
                    lblyear.Text = "&copy; " + ds.Rows[0]["product_year"].ToString() + ", ";
                }
                else
                {
                    lblyear.Text = "&copy; " + ds.Rows[0]["product_year"].ToString();
                }
            }

             DataTable mulimg = classUniversal.getUnivesalFileByProdIDandFiletype(productId,"I");
             if (mulimg.Rows.Count > 0)
             {
                 #region Multiple image display section
                 pstimg.Visible = true;
                 rptmulimage.DataSource = mulimg;
                 rptmulimage.DataBind();
                 if (rptmulimage.Items.Count > 1)
                 {
                     multiplepstimg.Visible = true;

                     for (int i = 0; i < rptmulimage.Items.Count; i++)
                     {
                         Image imgbanner1 = (Image)rptmulimage.Items[i].FindControl("imgbanner1");
                         
                         img = imgbanner1.AlternateText;

                         if (img == "" || img == "noimage2.png" || img == "noimage2.gif" || img == "N/A" || img == "n/a")
                         {
                             imgbanner1.Visible = false;
                         }
                         else
                         {
                             string fileName = Request.PhysicalApplicationPath + "\\images\\bigproduct\\" + img.ToString();
                             if (File.Exists(fileName))
                             {
                                 imgbanner1.ImageUrl = ReturnUrl("sitepathmain") + "images/bigproduct/" + img;
                             }
                             else
                             {
                                 imgbanner1.ImageUrl = ConfigurationManager.AppSettings["sitepathadmin"]+ "images/bigproduct/" + img;
                             }
                         }
                     }
                 }
                 #endregion
             }
             else
             {
                 #region Single Image display section
                 DataTable dt = classproduct.getproductbigimage(productId);
                 if (dt.Rows.Count > 0)
                 {
                     img = dt.Rows[0]["bigimage"].ToString().Trim();
                     if (img == "" || img == "noimage2.png" || img == "noimage2.gif" || img == "N/A" || img == "n/a")
                     {
                         imgbanner.Visible = false;
                         pstimg.Visible = false;
                     }
                     else
                     {
                         pstimg.Visible = true;
                         imgbanner.Visible = true;
                         string fileName = Request.PhysicalApplicationPath + "\\images\\bigproduct\\" + img.ToString();
                         if (File.Exists(fileName))
                         {
                             imgbanner.ImageUrl = ReturnUrl("sitepathmain") + "images/bigproduct/" + dt.Rows[0]["bigimage"].ToString().Trim();
                             downloadimg.HRef = ReturnUrl("sitepathmain") + "images/bigproduct/" + dt.Rows[0]["bigimage"].ToString().Trim();
                         }
                         else
                         {
                             imgbanner.ImageUrl = ConfigurationManager.AppSettings["sitepathadmin"]+ "images/bigproduct/" + dt.Rows[0]["bigimage"].ToString().Trim();
                             downloadimg.HRef = ConfigurationManager.AppSettings["sitepathadmin"]+ "images/bigproduct/" + dt.Rows[0]["bigimage"].ToString().Trim();
                         }
                     }
                 }
                 #endregion
             }          

            #region video display section
            string videourl = "";
            string videocode ="";
            videourl = ds.Rows[0]["movietrailorcode"].ToString();
            videocode = ds.Rows[0]["videoembed"].ToString();
            if (videourl != "" || videourl.Length > 0 || videocode != "" || videocode.Length > 0)
            {
                if (videocode.ToString() != "N/A" && videocode.ToString() != "n/a" && videocode != "" && videocode.Length > 0)
                {
                    pstvideo.InnerHtml = videocode;
                    pstimg.Visible = false;
                }
                else
                {
                    if (videourl.ToString() != "N/A" && videourl.ToString() != "n/a")
                    {
                        pstimg.Visible = false;
                        pstvideo.InnerHtml = "";
                        HttpCookie cookie = HttpContext.Current.Request.Cookies["internet"];
                        if (cookie != null)
                        {
                            if (cookie.Value.ToString().ToLower().Trim() == "true")
                            {
                                pstvideo.InnerHtml = "<video controls preload='metadata' class='videourl' data-setup='{}'><source src='" +  videourl + "' type='video/mp4'></video>";
                            }
                            else
                            {
                                pstvideo.InnerHtml = "<video controls preload='metadata' class='videourl' data-setup='{}'><source src='" +  videourl + "' type='video/mp4'></video>";
                            }
                        }
                        else
                        {
                            //pstvideo.InnerHtml = "<font color='red' class='loadvideomsg' size='16'>Failed To load Video</font>";
							pstvideo.InnerHtml = "<video controls preload='metadata' class='videourl' data-setup='{}'><source src='" +  videourl + "' type='video/mp4'></video>";
                        }
                    }
                    else
                    {
                        pstvideo.InnerHtml = "";
                        pstvideo.Visible = false;
                    }
                }
            }
            else
            {
                pstimg.Visible = true;
                pstvideo.Visible = false;
            }
             #endregion

            DataTable dtd = classdirector.getallactorsproducerbyid(Convert.ToDecimal(productId));
            DataTable actors = classdirector.getallactorsproducerbyid(Convert.ToDecimal(productId));
            if (actors.Rows.Count > 0)
            {
                if (actors.Rows[0]["movie_actor"] != "")
                {
                    lblactors.Text = Convert.ToString(actors.Rows[0]["movie_actor"]);
                    divactor.Visible = true;
                }
                else
                {
                    lblactors.Text = "";
                    divactor.Visible = false;
                }
            }
            else
            {
                lblactors.Text = "";
                divactor.Visible = false;
            }
            #region order status
            if (Page.User.Identity.IsAuthenticated)
            {
                if (strflag == "Premium")
                {
                    DataTable dtord = classpkg.getorderdetailbyemaild(Page.User.Identity.Name, "P");
                    if (dtord.Rows.Count > 0)
                    {
                        spanpkg.Visible = false;
                        lnkprodenquiry.Visible = true;
                        divwatch.Visible = true;
                        lnkprodenquiry.Enabled = true;
                    }
                    else
                    {
                        lnkprodenquiry.Attributes.Add("style", "cursor:default;");
                        lnkprodenquiry.Enabled = false;
                        lnksubscribe.Text = "<i class='fa fa-play'></i>Subscribe to View";
                        lnkprodenquiry.Visible = false;
                        lnksubscribe.Visible = true;
                        spanpkg.Visible = true;
                    }
                }
                if (strflag == "Special")
                {
                    DataTable dtord = classpkg.getspecialorderdetailbyemaild(Page.User.Identity.Name, "S", Convert.ToDecimal(productId));
                    if (dtord.Rows.Count > 0)
                    {
                        divwatch.Visible = true;
                        lnkprodenquiry.Enabled = true;
                        spanpkg.Visible = false;
                    }
                    else
                    {
                        lnkprodenquiry.Attributes.Add("style", "cursor:default;");
                        spanpkg.Visible = true;
                        lnkprodenquiry.Enabled = false;
                        lnkprodenquiry.Visible = false;
                        lnksubscribe.Text = "<i class='fa fa-play'></i>Pay to View";
                    }
                }
            }
            else
            {
                if (strflag == "Premium")
                {
                    lnksubscribe.Text = "<i class='fa fa-play'></i>Subscribe to View";
                    spanpkg.Visible = true;
                    lnkprodenquiry.Enabled = false;
                    lnkprodenquiry.Visible = false;
                }
                if (strflag == "Special")
                {
                    lnksubscribe.Text = "<i class='fa fa-play'></i>Pay to View";
                    spanpkg.Visible = true;
                    lnkprodenquiry.Enabled = false;
                    lnkprodenquiry.Visible = false;
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
                    divtrailer.Attributes.Add("style", "display:none;");
                    lnktrailor.Attributes.Add("style", "cursor:default;display:none;");
                }
                string strmovie = Convert.ToString(dtr.Rows[0]["vedioembed"]);
                if (strmovie == "")
                {
                    divwatch.Visible = false;
                    watchadmin.Visible = false;
                    lblyear.Visible = false;
                }
            }
            #endregion
        }
    }
    public string productUrlrewriting(object productname, object productid)
    {
        string strurl = "";
        try
        {
            strurl = UrlRewritingVM.getUrlRewritingInfo(productname, UrlRewritingVM.Encrypt(productid.ToString().Trim()), "PD");
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }

    public string getImageUrl(object filename)
    {
        string strurl = "";
        try
        {
            string fileName1 = Request.PhysicalApplicationPath + "\\images\\bigproduct\\" + filename.ToString();
            if (File.Exists(fileName1))
            {
                strurl = ReturnUrl("sitepathmain") + "images/bigproduct/" +filename.ToString().Trim();
            }
            else
            {
                strurl = ConfigurationManager.AppSettings["sitepathadmin"]+ "images/bigproduct/" + filename.ToString().Trim();
            }
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }

    private void ShowFavoriteArticles(int productId)
    {
        creative.Common clsCommon = new creative.Common();
        DataTable dthome = classcategory.getallcategoryid(productId);
        //catid = dthome.["categoryid"].ToString();
        //Response.Write(catid);
        //Response.End();



    }
   // sagar added below code for view all button redirect to ps page 30nov2017 starts here
    //public string onclick_hlnkcategory(Object catid,Object catname)
    //{
    //    string strurl = "";
    //    strurl = UrlRewritingVM.getUrlRewritingInfo(catname, UrlRewritingVM.Encrypt(catid.ToString().Trim()), "PS");
    //    return strurl;
        //Response.Write(catid);
        //Response.End();
    //}
   // sagar added below code for view all button redirect to ps page 30nov2017 ends here
        //   Response.Write(catid);
        //Response.End();
    public string getFileUrl(object filename)
    {
        string strurl = "";
        try
        {
            string fileName = Request.PhysicalApplicationPath + "\\files\\" + filename.ToString().Trim();
            if (File.Exists(fileName))
            {
                strurl = ReturnUrl("sitepathmain") + "files/" + filename;
            }
            else
            {
                strurl = ConfigurationManager.AppSettings["sitepathadmin"]+ "files/" + filename;
            }
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
    public static string getextension(string filename)
    {
        int z = 0;
        z = filename.LastIndexOf('.');
        int x = 0;
        x = filename.Length;
        string ext = filename.Substring(z, x - z);
        string cnttyp="";
        if(filename.ToString() != "N/A" && filename.ToString() != "n/a")
        {
            if (ext == ".doc" || ext == ".docx")
            {
                cnttyp = "Application/msword";
            }

            if (ext == ".xls" || ext == ".xlsx")
            {
                cnttyp = "Application/x-msexcel";
            }


            if (ext == ".jpg" || ext == ".jpeg")
            {
                cnttyp = "image/jpeg";
            }

            if (ext == ".gif")
            {
                cnttyp = "image/GIF";
            }

            if (ext == ".png")
            {
                cnttyp = "image/png";
            }

            if (ext == ".pdf")
            {
                cnttyp = "Aaplication/pdf";
            }
            return cnttyp;
        }
        else
        {
            cnttyp = "";
            return cnttyp;
        }
         
    }
    public static string getRemoteAddr(bool GetLan = false)
    {
        String ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(ip))
        {
            ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        return ip;
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
    protected void lnksubscribe_click(object sender, EventArgs e)
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
    public void getpremiumpackagestatus(string useremail)
    {
        //try
        //{
        DataTable dtordp = classpkg.getstatusorderdetailbyemaild(useremail);

        if (dtordp.Rows.Count > 0)
        {
            int orderid = Convert.ToInt32(dtordp.Rows[0]["orderid"]);

            bool strurlex =
               classpkgorder.packagevalidity_status(useremail, Convert.ToDecimal(orderid));

        }
        //}
        //catch (Exception ex)
        //{
        //    ErrorLog.WriteError(ex.ToString());
        //}

    }
    public void getspecialpackagestatus(string useremail, decimal productid)
    {
        try
        {
            if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
            {

                productId = Convert.ToInt32(Request.QueryString["p"].ToString().Trim());

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
    protected void lnktrailor_Click(object sender, EventArgs e)
    {
        //try
        //{
        if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
        {

            productId = Convert.ToInt32(Request.QueryString["p"].ToString());

        }
        DataTable dsp = classproduct.get_proc_ProductDescription_ProdFeature(productId);
        string strprdnm = dsp.Rows[0]["productname"].ToString();
        strur = UrlRewritingVM.getUrlRewritingInfo(strprdnm, productId, "t");

        Response.Redirect(strur);

        //}
        //catch (Exception ex)
        //{
        //    ErrorLog.WriteError(ex.ToString());
        //}
    }
    protected void lnkprodenquiry_Click(object sender, EventArgs e)
    {
        //try
        //{
        if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
        {

            productId = Convert.ToInt32(Request.QueryString["p"].ToString().Trim());

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

        //}
        //catch (Exception ex)
        //{
        //    ErrorLog.WriteError(ex.ToString());
        //}
    }
    protected void lnkadminwatch_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
        {

            productId = Convert.ToInt32(Request.QueryString["p"].ToString().Trim());

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

    // sagar added below code for view all button redirect to ps page with repeater control 30nov2017 starts here
    //public void linkview()
    //{
    //    DataTable dtc = classrecommend.getproductbycategorynameid(productId);
       //// var panads = rptdoc.Items[0].FindControl("panads") as Repeater;
        //if (dtc.Rows.Count > 0)
        //{
            ////var ln = RepeaterOuter.Items[0].FindControl("RepeaterInner") as Repeater;
            //ln.DataSource = dtc;
            //ln.DataBind();
            ////lblcatname.Text = dtc.Rows[0]["categoryname"].ToString();
            ////lblcatid.Text = dtc.Rows[0]["categoryid"].ToString();
            ////catid = dtc.Rows[0]["categoryname"].ToString();
            ////catname = dtc.Rows[0]["categoryname"].ToString();
            ////Response.Write(catid);
            ////Response.End();
        //}
    //}
    // sagar added above code for view all button redirect to ps page with repeater control 30nov2017 ends here
    
    protected void imgdownload_Click(object sender, EventArgs e)
    {
        if(Page.User.Identity.IsAuthenticated)
        {
         if(img!="")
         {
             string filePath = Server.MapPath("~/images/bigproduct/" + img);  
                Response.ContentType = getextension(img);
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                Response.WriteFile(filePath);
                Response.End();

          }
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
        }  
    }
    protected void imgdownload1_Click(object sender, EventArgs e)
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            if (img != "")
            {
                string filePath = Server.MapPath("~/coverphoto/" + img);
                Response.ContentType = getextension(img);
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                Response.WriteFile(filePath);
                Response.End();

            }
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
        }
    }

    //protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    DataTable dtc = classrecommend.getproductbycategorynameid(productId);
    //    if (dtc.Rows.Count > 0)
    //    {
    //        Repeater1.DataSource = dtc;
    //        Repeater1.DataBind();

    //    }
    //}


    // sagar added below code for view all button redirect to ps page without repeater control 13dec2017 starts here
    protected void viewall_Click(object sender, EventArgs e)
    {
        DataTable dtc = classrecommend.getproductbycategorynameid(productId);
      
            catid = dtc.Rows[0]["categoryid"].ToString();
            catname = dtc.Rows[0]["categoryname"].ToString();
         
        string strurl = "";
        strurl = UrlRewritingVM.getUrlRewritingInfo(catname, UrlRewritingVM.Encrypt(catid.ToString().Trim()), "PS");
        Response.Redirect(strurl);
         //Response.End();
    }
    // sagar added above code for view all button redirect to ps page without repeater control 13dec2017 ends here
}
