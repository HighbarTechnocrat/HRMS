using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

public partial class themes_creative1_LayoutControls_pd_moviereview : System.Web.UI.UserControl
{

    private static int productid;
    static int ratevalue;
    string username = "";
    string strurl = "";
    string strprdnm = "";

     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
        {
            productid = Convert.ToInt32(Request.QueryString["p"].ToString());
        }
        //if (!IsPostBack)
        //{
            if (Page.User.Identity.IsAuthenticated)
            {
                bindrating();
                moviefollower();
                //loadgrid();
            }
       // }
    }
    public void bindrating()
    {
        //try
        ////{
            if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
            {

                productid = Convert.ToInt32(Request.QueryString["p"].ToString());

            }

            DataTable ds = classproduct.get_proc_ProductDescription_ProdFeature(productid);
            if (ds.Rows.Count > 0)
            {
                string strprdnm = ds.Rows[0]["productname"].ToString();
                lblprdname.Text = strprdnm.ToString();
            }

            DataTable objadapter1 = classreviews.getratingdetails(Page.User.Identity.Name, productid);
            if (objadapter1.Rows.Count != 0)
            {
                if (objadapter1.Rows[0]["ratingvalue"].ToString() != "")
                {
                    ratevalue = Convert.ToInt32(objadapter1.Rows[0]["ratingvalue"].ToString());
                    hdvalue2.Value = ratevalue.ToString();
                }
                else
                {

                }
                if (objadapter1.Rows[0]["reviewtext"].ToString() != "")
                {
                    txtreview.Text = objadapter1.Rows[0]["reviewtext"].ToString();
                }
            }



        //}
        //catch (Exception ex)
        //{
        //    ErrorLog.WriteError(ex.ToString());
        //}

    }
    protected void lnkrev_onclick(object sender, EventArgs e)
    {
        divstar.Visible = true;

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
        {

            productid = Convert.ToInt32(Request.QueryString["p"].ToString());

        }
        if (Page.User.Identity.IsAuthenticated == true)
        {
            string UserName = Page.User.Identity.Name;

            ratevalue = Convert.ToInt32(hdvalue2.Value);

            if (validateuser() == 0)
            {
                DataTable objadapter1 = classreviews.getratingdetailsbyusernamemovieid(Page.User.Identity.Name, productid);

                if (objadapter1.Rows.Count != 0)
                {
                    classreviews.insertupdatereview(productid, UserName, txtreview.Text, ratevalue);
                }
                else
                {
                    classreviews.insertrating(productid, UserName, ratevalue, txtreview.Text, "S");
                }

                DataTable ds = classproduct.get_proc_ProductDescription_ProdFeature(productid);
                if (ds.Rows.Count > 0)
                {
                    strprdnm = ds.Rows[0]["productname"].ToString();
                    strurl = UrlRewritingVM.getUrlRewritingInfo(strprdnm.ToString(), productid, "PD");
                    moviefollower();
                }

            }
        }
        else
        {
            if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
            {

                productid = Convert.ToInt32(Request.QueryString["p"].ToString());

            }


            DataTable ds = classproduct.get_proc_ProductDescription_ProdFeature(productid);
            if (ds.Rows.Count > 0)
            {
                strprdnm = ds.Rows[0]["productname"].ToString();
                strurl = UrlRewritingVM.getUrlRewritingInfo(strprdnm.ToString(), productid, "PD");
            }
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + strurl);

        }

    }


protected void rptmoviereview_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
     
	bool iserror;
        if (Page.User.Identity.IsAuthenticated)
        {
           if (e.CommandName == "follow")
            {
                iserror = classreviews.insertdeletefollowing(Page.User.Identity.Name, e.CommandArgument.ToString());
                if (!iserror)
                {
                    moviefollower();		    
                }
            }
            if (e.CommandName == "username")
            {
		
                DataTable dt = classreviews.getuseridbyemail(e.CommandArgument.ToString());
                Response.Redirect(ReturnUrl("sitepathmain") + "reviewratings.aspx?userid=" + dt.Rows[0]["indexid"].ToString());
            }
        }
    }


    private int validateuser()
    {
        int validate = 0;
        //if (txtreview.Text.ToString() == "" && RadioButton1.Checked == false && RadioButton2.Checked == false && RadioButton3.Checked == false && RadioButton4.Checked == false && RadioButton5.Checked == false)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Please select rating!!!')", true);
        //    validate = 1;
        //}
        //else
        //{
        //    validate = 0;
        //}
        return validate;
    }

    public void moviefollower()
    {
	//lblcount.Text=(400-txtreview.Text.Length)+" characters remaining";
        if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
        {
            productid = Convert.ToInt32(Request.QueryString["p"].ToString());
        }
        DataTable dtm = classreviews.getfollowersdetailsbymovieid(Convert.ToString(productid));
        if (dtm.Rows.Count > 0)
        {
            rptmoviereview.DataSource = dtm;
            rptmoviereview.DataBind();
	    DataTable dt3;
            for (int i = 0; i <= rptmoviereview.Items.Count - 1; i++)
            {
		Label rating=(Label)rptmoviereview.Items[i].FindControl("lblratval");
		hdvalue.Value=rating.Text;
		Image img = (Image)rptmoviereview.Items[i].FindControl("imgprofile");
                if (dtm.Rows[i]["profilephoto"].ToString() != "")
                {
                    img.ImageUrl = "https://graph.facebook.com/" + dtm.Rows[i]["profilephoto"].ToString() + "/picture?type=large";
                }
                else
                {
                    img.ImageUrl = ReturnUrl("sitepath")+"images/noprofile.jpg";
                }
		Literal ltrjs = (Literal)rptmoviereview.Items[i].FindControl("ltrjs");
                ltrjs.Text ="<div id='jqxRating"+i+"' Style='margin-left:100px;'></div><script type='text/javascript'>$(document).ready(function () {$('#jqxRating"+i+"').jqxRating({ width: 200, height: 30,disabled:true, value:"+hdvalue.Value+" });});</script>";
		LinkButton lnkfollow=(LinkButton)rptmoviereview.Items[i].FindControl("lnkfollow");
		LinkButton lnkusername=(LinkButton)rptmoviereview.Items[i].FindControl("lnkusername");
                Panel pnlike = (Panel)rptmoviereview.Items[i].FindControl("pnlike");
                Label lblcnt = (Label)pnlike.FindControl("lblcnt");
                Label lblrate = (Label)rptmoviereview.Items[i].FindControl("lblratval");
                Label lblcomment = (Label)rptmoviereview.Items[i].FindControl("lblcommentcount");
                if(Convert.ToInt32(lblcomment.Text)>0)
                {
                    lblcomment.Text = "(" + Convert.ToInt32(lblcomment.Text)+")";
                }
                else
                {
                    lblcomment.Text="";
                }
                lblrate.Text = ratevalue.ToString();
		dt3 = classreviews.getfollower(Page.User.Identity.Name, lnkusername.CommandArgument.ToString());
		if (Page.User.Identity.Name == lnkusername.CommandArgument.ToString())
                {
                    lnkfollow.Visible = false;
                }
                if (dt3.Rows.Count > 0)
                {
                    lnkfollow.Text = "Unfollow";
                }
                else
                {
                    lnkfollow.Text = "Follow";
                }
                if (lblcnt.Text.ToString().Trim() == "0")
                {
                    pnlike.Visible = false;
                }
                else
                {
                    pnlike.Visible = true;
                }

            }
        }
       
    }
  protected void lnklike_click(object sender, EventArgs e)
    {
        var btn = (LinkButton)sender;
        var item = (RepeaterItem)btn.NamingContainer;
         username=Convert.ToString(Page.User.Identity.Name);
        if (Request.QueryString["p"] != "" && Request.QueryString["p"] != null)
        {
            productid = Convert.ToInt32(Request.QueryString["p"].ToString());
        }
        LinkButton lnklike = item.FindControl("lnklike") as LinkButton;
         Label lblrevid = item.FindControl("lblrevid") as Label;
        
            DataTable dtl = classreviews.getuserlikescountbyrevieid(username, Convert.ToString(lblrevid.Text));
           
            moviefollower();
         
    }



  public string productUrlrewriting(object productname, object productid)
  {
      string strurl = "";
      try
      {

          strurl = UrlRewritingVM.getUrlRewritingInfo(productname, productid, "PD");

          return strurl;
      }
      catch (Exception ex)
      {
          return strurl;
      }
  }

  protected void rptmoviereview_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        string username = Convert.ToString(Page.User.Identity.Name);
        LinkButton lnklike = (LinkButton)e.Item.FindControl("lnklike");
        Label lblrevid = (Label)e.Item.FindControl("lblrevid");
        Label lbluseremail = (Label)e.Item.FindControl("lbluseremail");

        DataTable dtc = classreviews.getlikecountsbyusername(username, Convert.ToDecimal(lblrevid.Text));
        if (dtc.Rows.Count > 0)
        {
            lnklike.Text = Convert.ToString(dtc.Rows[0]["likeflag"]);
           
        }

    }

  protected void lnkcomm_onclick(object sender,EventArgs e)
  {
      var btn = (LinkButton)sender;
      var item = (RepeaterItem)btn.NamingContainer;
      Label lblrevid = item.FindControl("lblrevid") as Label;
      Response.Redirect(ReturnUrl("sitepathmain") + "reviews.aspx?reviewid=" + Convert.ToString(lblrevid.Text));

  }
}
