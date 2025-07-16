using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Text;
using System.Web.Security;

public partial class themes_creative1 : System.Web.UI.UserControl
{
    public static string username = "",userid="";
    public static int grpid;
    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
    {
        username = "";

            loaddata();
        
      
    }
    public void loaddata()
    {
            if (Request.QueryString["grpid"] != null)
            {
                 grpid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["grpid"].ToString()));
                 DataTable grp = classreviews.getgrpbygrpid(grpid);
                 if (grp.Rows.Count > 0)
                 {
                     lnkhead.HRef = ReturnUrl("sitepathmain") + "grouppost/" + grpid;
                //JAYESH COMMENTED BELOW LINE TO HIDE USER NAME ON editprofile.aspx 14oct2017
                //lnkhead2.HRef = ReturnUrl("sitepathmain") + "grouppost/" + grpid;
                //JAYESH COMMENTED BELOW LINE TO HIDE USER NAME ON editprofile.aspx 14oct2017
              string grpimg = "";
                     if (grp.Rows[0]["grpcoverimg"].ToString().Length > 0 && grp.Rows[0]["grpcoverimg"].ToString() != "" && grp.Rows[0]["grpcoverimg"].ToString() != null)
                     {
                    //JAYESH COMMENTED BELOW LINE TO HIDE COVER IMAGE ON editprofile.aspx 14oct2017
                    //imgcover.Visible = true;
                    //imgcover.Src= ConfigurationManager.AppSettings["sitepathadmin"]+ "images/grpcover/" + grp.Rows[0]["grpcoverimg"].ToString().Trim();
                    //JAYESH COMMENTED ABOVE LINE TO HIDE COVER IMAGE ON editprofile.aspx 14oct2017
                }
                     else
                     {
                    //JAYESH COMMENTED BELOW LINE TO HIDE COVER IMAGE ON editprofile.aspx 14oct2017
                    //imgcover.Visible = false;
                    //JAYESH COMMENTED ABOVE LINE TO HIDE COVER IMAGE ON editprofile.aspx 14oct2017
                }
                if (grp.Rows[0]["grpimg"].ToString().Trim() == "" || grp.Rows[0]["grpimg"].ToString().Trim() == null)
                     {
                         grpimg = "noimage1.png";
                     }
                     else
                     {
                         grpimg = grp.Rows[0]["grpimg"].ToString().Trim();
                     }
                     imghead.Src = ConfigurationManager.AppSettings["sitepathadmin"]+ "images/grpimages/thumbnail/" + grpimg;

                     imghead.Alt = grp.Rows[0]["grpname"].ToString().Trim();
                //JAYESH COMMENTED BELOW LINE TO HIDE USER NAME ON editprofile.aspx 14oct2017
                //lblheadname.Text = grp.Rows[0]["grpname"].ToString().Trim();
                //JAYESH COMMENTED ABOVE LINE TO HIDE USER NAME ON editprofile.aspx 14oct2017
            }
        }
            else if (Request.QueryString["userid"] != null && Request.QueryString["userid"] != "")
            {
                userid = UrlRewritingVM.Decrypt(Request.QueryString["userid"].ToString().Trim());

                DataTable user = classreviews.getuserbyindexid(Convert.ToInt32(userid));
                if (user.Rows.Count > 0)
                {
                    username = user.Rows[0]["username"].ToString().Trim();
                }
            }
            else
            {
                if (Page.User.Identity.IsAuthenticated)
                {
                    username = Page.User.Identity.Name.ToString().Trim();
                }
            }
        if(username != "" && username != null && username.Length > 0)
        {
            DataTable dtadd = classaddress.getuserinfodetails(username);
            if (dtadd.Rows.Count > 0)
            {
                
                /*wasim commented below line to cancel link on user profile photo 04jan2018*/
               //  lnkhead.HRef = ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(dtadd.Rows[0]["indexid"].ToString());
                /*wasim commented Above line to cancel link on user profile photo 04jan2018*/

                //JAYESH COMMENTED BELOW LINE TO HIDE USER NAME ON editprofile.aspx 14oct2017
                //lnkhead2.HRef = ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(dtadd.Rows[0]["indexid"].ToString());
                //JAYESH COMMENTED ABOVE LINE TO HIDE USER NAME ON editprofile.aspx 14oct2017
                string proimg = "";
                if (dtadd.Rows.Count > 0)
                {
                    if (dtadd.Rows[0]["profilephoto"].ToString().Trim() == "" || dtadd.Rows[0]["profilephoto"].ToString().Trim() == null)
                    {
                        
                        proimg = "noimage1.png";
                    }
                    else
                    {
                        proimg = dtadd.Rows[0]["profilephoto"].ToString().Trim();
                    }
                    if (dtadd.Rows[0]["coverphoto"].ToString().Trim() == "" || dtadd.Rows[0]["coverphoto"].ToString().Trim() == null)
                    {
                        //JAYESH COMMENTED BELOW LINE TO HIDE COVER IMAGE ON editprofile.aspx 14oct2017
                        //imgcover.Visible = false;
                        //JAYESH COMMENTED ABOVE LINE TO HIDE COVER IMAGE ON editprofile.aspx 14oct2017

                    }
                    else
                    {
                        //JAYESH COMMENTED BELOW LINE TO HIDE COVER IMAGE ON editprofile.aspx 14oct2017
                        //imgcover.Visible = true;
                        //imgcover.Src = ReturnUrl("sitepath") + "images/coverphoto/" + dtadd.Rows[0]["coverphoto"].ToString().Trim().ToString();
                        //JAYESH COMMENTED ABOVE LINE TO HIDE COVER IMAGE ON editprofile.aspx 14oct2017
                    }
                }
                else
                {
                    proimg = "noimage1.png";
                }
                imghead.Src = ReturnUrl("sitepath") + "images/profile110x110/" + proimg;

                imghead.Alt = dtadd.Rows[0]["fullname"].ToString().Trim();
                //lblheadname.Text = dtadd.Rows[0]["fullname"].ToString().Trim();
            }
        }
            
    }
}