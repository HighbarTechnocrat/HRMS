using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Xml;

public partial class Themes_SecondTheme_LayoutControls_pdnew_pdfproduct : System.Web.UI.UserControl
{
   private static int productId;

    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        
       
        if (!IsPostBack)
        {
            if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
            {
                productId = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["p"].ToString()));
                hdproductid.Value = productId.ToString().Trim();
                //countuserproduct();
            }
            loaddata();
            // SONY COMMENTED this to HIDE DISPLAY of VIEWS 
          //  loadviews();
          //  loadproduct();
            //loaddirector();
            //loadactor();
            //loadproducer();
            //loadattribute();
        }
    }
   public void loaddata()
   {
       if (Request.QueryString["pdivreviewcount"] != "" && Request.QueryString["p"] != null)
       {
           productId = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["p"].ToString().Trim()));
       }
       DataTable dtm = classreviews.getratingdetails(Page.User.Identity.Name, productId);
       if (dtm.Rows.Count == 0)
       {
           gotocritics.Visible = false;
       }
       DataTable dtd = classdirector.getallactorsproducerbyid(Convert.ToDecimal(productId));
       if (dtd.Rows.Count > 0)
       {

           if (dtd.Rows[0]["video_length"].ToString() == "")
           {
               divlength.Visible = false;
           }
           else
           {
               divlength.Visible = true;
               lbltime.Text = dtd.Rows[0]["video_length"].ToString();
           }
       }
       
       DataTable rating = classreviews.gettotalrating(productId);
       if (rating.Rows.Count > 0)
       {
           if (Convert.ToInt32(rating.Rows[0]["count"].ToString()) > 0)
           {
               reviewcount.Text = rating.Rows[0]["count"].ToString();
               float avg = (Convert.ToInt32(rating.Rows[0]["total"].ToString()) / Convert.ToInt32(rating.Rows[0]["count"].ToString()));
               lblrating.Text = avg.ToString();
               if(lblrating.Text=="0")
               {
                   divrating.Visible = false;
               }
           }
           else
           {
               divrating.Visible = false;
               divreviewcount.Visible = true;
               lblrating.Text = "0";
	           reviewcount.Text ="0";
           }
       }
       int cnt = 0;
       DataTable dtv = classproduct.get_proc_ProductDescription_ProdFeature(productId);
       if (dtv.Rows.Count > 0)
       {
           lblview.Text = dtv.Rows[0]["visitcount"].ToString();

       }
       else
       {
           lblview.Text = "0";
       }

       DataTable dt2 = classproduct.getproductlikecountcomment(productId.ToString());
       if(dt2.Rows.Count>0)
       {
           lbllike.Text = dt2.Rows[0]["likecount"].ToString();
           lbldislike.Text = dt2.Rows[0]["dislikecount"].ToString();
           lblfav.Text = dt2.Rows[0]["favcount"].ToString();
       }
       else
       {
           lblfav.Text = "0";
           lbllike.Text = "0";
       }

       //new comment
       //DataTable dtr = classproduct.get_proc_movietrailor_productid(productId);
       //if (dtr.Rows.Count > 0)
       //{
       //    string strmovie = Convert.ToString(dtr.Rows[0]["vedioembed"]);
       //    if (strmovie == "")
       //    {
       //        diview.Visible = false;
       //    }
       //}
       //new comment end here


       //DataTable actors = classdirector.getallactorsproducerbyid(Convert.ToDecimal(productId));
       //if (actors.Rows.Count > 0)
       //{

       //    if (actors.Rows[0]["movie_actor"] != "")
       //    {
       //        lblactors.Text = Convert.ToString(actors.Rows[0]["movie_actor"]);
       //        divactor.Visible = true;
       //    }
       //    else
       //    {
       //        lblactors.Text = "";
       //        divactor.Visible = false;
       //    }
       //}

       //else
       //{
       //    lblactors.Text = "";
       //    divactor.Visible = false;

       //}
   }
   //public void loadproduct()
       // {
       //     DataTable ds = classproduct.get_proc_ProductDescription_ProdFeature(productId);
       //     if (ds.Rows.Count > 0)
       //       {
       //           if (ds.Rows[0]["shortdescription"].ToString() == "")
       //           {
       //               pnlshortdesc.Visible = false;

       //           }

       //           else
       //           {
       //               pnlshortdesc.Visible = true;
       //               lbl_productDescription.Text = ds.Rows[0]["shortdescription"].ToString();
       //           }
       //      }

       // }
       //public void loaddirector()
       //{
       //    if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
       //    {

       //        productId = Convert.ToInt32(Request.QueryString["p"].ToString());
       //    }

       //    DataTable dtd = classdirector.getalldirectorbyid(Convert.ToDecimal(productId));
       //    if (dtd.Rows.Count > 0)
       //    {
       //        divdir.Visible = true;
       //        string strdpath = "";
       //        for (int ic = 0; ic < dtd.Rows.Count; ic++)
       //        {
       //            DataRow drc = dtd.Rows[ic];

       //            if (drc["directorname"] != System.DBNull.Value)
       //            {

       //               if (ic == (dtd.Rows.Count - 1))  

       //                {

       //                  strdpath = UrlRewritingVM.getUrlRewritingInfo("", drc["directorid"].ToString(), "BD");

       //                  ltrdir.Text += "<a ID='linkcontact' runat='server' href='" + strdpath + "'>" +  drc["directorname"].ToString() + "</a>";

       //                }
       //                else
       //                {


       //              strdpath = ReturnUrl("sitepathmain") + "ad/" + drc["directorid"].ToString();

       //                  ltrdir.Text += "<a title='" + drc["directorname"].ToString() + "' ID='linkcontact' runat='server' href='" + strdpath + "'>"

       //           + drc["directorname"].ToString() +",   " + " </a>";

       //                }
       //            }

       //      }
       //    }
       //    else
       //    {
       //        divdir.Visible = false;
       //    }
       //}
       //public void loadactor()
       //{
       //    if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
       //    {

       //        productId = Convert.ToInt32(Request.QueryString["p"].ToString());
       //    }
       //    DataTable dtd = classdirector.getallactorsproducerbyid(Convert.ToDecimal(productId));
       //    if (dtd.Rows.Count > 0)
       //    {

       //        if (dtd.Rows[0]["movie_actor"] != "")
       //        {
       //            lblactors.Text = Convert.ToString(dtd.Rows[0]["movie_actor"]);
       //            divactor.Visible = true;
       //        }
       //        else
       //        {
       //            lblactors.Text = "";
       //            divactor.Visible = false;
       //        }
       //    }

       //    else
       //    {
       //        lblactors.Text = "";
       //        divactor.Visible = false;

       //    }
       //}
       //public void loadproducer()
       //{
       //    if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
       //    {

       //        productId = Convert.ToInt32(Request.QueryString["p"].ToString());
       //    }
       //    DataTable dtd = classdirector.getallactorsproducerbyid(Convert.ToDecimal(productId));
       //    if (dtd.Rows.Count > 0)
       //    {

       //        if (dtd.Rows[0]["video_length"].ToString() == "")
       //        {
       //            divl.Visible = false;
       //        }
       //        else
       //        {
       //            divl.Visible = true;
       //            lbltime.Text = dtd.Rows[0]["video_length"].ToString();
       //        }


       //        if (dtd.Rows[0]["movie_producer"].ToString() == "")
       //        {
       //            divlang.Visible = false;

       //        }
       //        else
       //        {
       //            lblproducer.Text = dtd.Rows[0]["movie_producer"].ToString();
       //            divlang.Visible = true;
       //        }

       //    }

       //}
   public void loadviews()
   {
       int cnt = 0;

       if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
       {

           productId = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["p"].ToString().Trim()));
       }

       DataTable dtv = classproduct.get_proc_ProductDescription_ProdFeature(productId);
       if (dtv.Rows.Count > 0)
       {
           cnt = Convert.ToInt32(dtv.Rows[0]["visitcount"].ToString()) + 1;
           classproduct.createviewscnt(productId, cnt);

           DataTable dtcnt = classdirector.getallvideocntbyid(productId);
           if (dtcnt.Rows.Count > 0)
           {

               diview.Visible = true;
               lblview.Text = dtcnt.Rows[0]["visitcount"].ToString();

           }

       }

   }
       //public void loadattribute()
       //{
       //    string strpath = creativeconfiguration.SitePathMain;

       //    DataTable dtatr = classattributegroup.geattributegroup_prodid(productId);
       //    rptattrgr.DataSource = dtatr;
       //    rptattrgr.DataBind();
       //   for (int j = 0; j < rptattrgr.Items.Count; j++)
       //   {
       //       Label lblattr = (Label)rptattrgr.Items[j].FindControl("lblattrgrid");
       //       Literal ltrattributes = (Literal)rptattrgr.Items[j].FindControl("ltrattributes");
       //       string strid = lblattr.Text;
       //       string strpspath = strpath + "ps/";

       //       if (strid != null || strid != "")
       //       {

       //         DataTable dtp = classattributegroup.getattributeidbyattrgrid(Convert.ToString(productId), strid, strpspath);

       //           if (dtp.Rows.Count > 0)
       //           {

       //               for (int i = 0; i < dtp.Rows.Count; i++)
       //               {
       //                       DataRow dr = dtp.Rows[i];
       //                       if (dr["Name"] != System.DBNull.Value)
       //                       {
       //                           if (i == (dtp.Rows.Count - 1))
       //                           {

       //                        ltrattributes.Text += "<a title='" + dr["Name"].ToString() + "' ID='linkcontact' runat='server' href='" + dr["attributeurl"].ToString() + "'>" + dr["Name"].ToString() +  " </a>";
       //                           }
       //                           else
       //                           {
       //                               ltrattributes.Text += "<a title='" + dr["Name"].ToString() + "' ID='linkcontact' runat='server' href='" + dr["attributeurl"].ToString() + "'>" + dr["Name"].ToString() + ",  " + " </a>";
       //                           }
       //                       }
       //                   }

       //                 divattrgr.Visible = true;
       //               }
       //           else
       //           {
       //               divattrgr.Visible = false;
       //           }
       //           }
       //       }
       //   }
   public void countuserproduct()
       {
           if (Page.User.Identity.IsAuthenticated)
           {
               string catid = "";
               int count = 0;
               bool flagu = true;
               string username = Convert.ToString(Page.User.Identity.Name);
               if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
               {
                   productId = Convert.ToInt32(Request.QueryString["p"].ToString());
               }

               #region productcategory count
               DataTable dtc = classrecommend.getproductcategorybyid(productId);

             if (dtc.Rows.Count > 0)
             {

                   for (int j = 0; j < dtc.Rows.Count; j++)
                   {
                       catid = Convert.ToString(dtc.Rows[j]["categoryid"]);
                       DataTable dtcatcnt = classrecommend.getcategoryusercount(username, Convert.ToDecimal(catid));
                       if (dtcatcnt.Rows.Count > 0)
                       {
                           DataTable dtparcat = classrecommend.getcategoryparentcount(Convert.ToInt32(catid));
                           if (dtparcat.Rows.Count > 0)
                           {
                               #region get parentcategory
                               if (Convert.ToInt32(dtparcat.Rows[0]["ParentID"]) == 0)
                               {

                                   if (Convert.ToInt32(dtcatcnt.Rows[0]["categorycount"].ToString()) > 0)
                                   {
                                       DataTable dt = classrecommend.getcategoryusercountb_category_usercount(Convert.ToDecimal(catid), username);
                                       if (dt.Rows.Count > 0)
                                       {
                                           count = Convert.ToInt32(dt.Rows[0]["cat_count"].ToString());

                                           int cnt = count + 1;

                                           flagu = classrecommend.updateusercategorycntbysearch(Convert.ToDecimal(catid), username, cnt);
                                       }
                                   }
                                   else
                                   {
                                       flagu = classrecommend.createuserctegorycntbysearch(username, Convert.ToDecimal(catid), 1);
                                   }
                               }

                               #endregion
                               else
                               {
                                   #region parentcountupdate
                                   decimal parentcatid = 0;
                                   parentcatid = Convert.ToDecimal(dtparcat.Rows[0]["ParentID"]);

                                   DataTable dtp = classrecommend.getcategoryusercount(username, Convert.ToDecimal(parentcatid));
                                   if (dtp.Rows.Count > 0)
                                   {
                                       if (Convert.ToInt32(dtp.Rows[0]["categorycount"].ToString()) > 0)
                                       {
                                           bool flagp = classrecommend.updatecategory_category_userupdateparentcount(Convert.ToInt32(parentcatid), Convert.ToString(Page.User.Identity.Name));

                                       }

                                   }
                                   #endregion
                                   #region addsubcategory count
                                   if (Convert.ToInt32(dtcatcnt.Rows[0]["categorycount"].ToString()) > 0)
                                   {
                                       DataTable dt = classrecommend.getcategoryusercountb_category_usercount(Convert.ToDecimal(catid), username);
                                       if (dt.Rows.Count > 0)
                                       {
                                           count = Convert.ToInt32(dt.Rows[0]["cat_count"].ToString());
                                           int cnt = count + 1;
                                           flagu = classrecommend.updateusercategorycntbysearch(Convert.ToDecimal(catid), username, cnt);
                                       }
                                   }
                                   else
                                   {
                                       flagu = classrecommend.createuserctegorycntbysearch(username, Convert.ToDecimal(catid), 1);
                                   }
                                   #endregion
                               }

                           }

                       }
                   }
               #endregion
               #region product_user
                   DataTable dtpcnt = classrecommend.getproductusercount(username, productId);

                   if (dtpcnt.Rows.Count > 0)
                   {
                       if (Convert.ToInt32(dtpcnt.Rows[0]["productcount"].ToString()) > 0)
                       {
                           DataTable dt = classrecommend.getproductusercountb_product_usercount(productId, username);
                           if (dt.Rows.Count > 0)
                           {
                               count = Convert.ToInt32(dt.Rows[0]["prod_count"].ToString());
                               int cnt = count + 1;
                               flagu = classrecommend.updateuserproductcntbysearch(productId, username, cnt);
                           }
                       }
                       else
                       {
                           bool flag = classrecommend.createuserproductcntbysearch(productId, username, 1);
                       }
                   }
                   #endregion
               }
           }
       }
   //public string onclick_hlnkattribute(Object attributxml)
       //{
       //    string strurlcat = "";
       //     strurlcat =  Convert.ToString(attributxml);
       //    return strurlcat;
       //}
}
