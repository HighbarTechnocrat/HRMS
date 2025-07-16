using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.IO;

public partial class themes_creative1_LayoutControls_profile_followers : System.Web.UI.UserControl
{
    public static int userid;
    public static string users;

    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;}
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/editprofile");
        }
        else
        {
            DisplayProfileProperties();
        }
    }

    private void DisplayProfileProperties()
    {
        //if (Request.QueryString["userid"] != null && Request.QueryString["userid"] != "" && Request.QueryString.Count == 1 && Request.QueryString["userid"].Length == 24)
        //{
        //    if (Int32.TryParse(UrlRewritingVM.Decrypt(Request.QueryString["userid"]), out userid))
        //    {
        if (Request.QueryString["userid"] != null && Request.QueryString["userid"] != "" && Request.QueryString.Count == 1)
        {
            //if (Int32.TryParse(UrlRewritingVM.Decrypt(Request.QueryString["userid"]), out userid))
            //{
                userid = Convert.ToInt32(commonclass.GetSafeIDFromURL(UrlRewritingVM.Decrypt(Request.QueryString["userid"].ToString())));
                DataTable dtuser = classaddress.getuserbyindexid(userid);
                if (dtuser.Rows.Count > 0)
                {
                    users = dtuser.Rows[0]["username"].ToString();
                    if (users == Page.User.Identity.Name.ToString())
                    {
                        viewbtn.Visible = false;
                        btnedit.Visible = false;
                    }
                    else
                    {
                        viewbtn.Visible = false;
                        btnedit.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
                }
            //}
            //else
            //{
            //    Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
            //}
        }
        else
        {
            users = Page.User.Identity.Name.ToString();
        }

        DataSet user = classaddress.getalluserbyusername(users);
        if (user.Tables.Count > 0)
        {
            if(user.Tables[0].Rows.Count > 0)
            {
                lblname.Text = user.Tables[0].Rows[0]["fullname"].ToString();
                
                if (user.Tables[0].Rows[0]["emailid"].ToString() != null && user.Tables[0].Rows[0]["emailid"].ToString() != "")
                {
                    lblemail.Text = user.Tables[0].Rows[0]["emailid"].ToString();
                }
                else
                {
                    lblemail.Text = "Not Available.";
                }
                //<%--wasim comment below code 23 oct 17--%>
                //if (user.Tables[0].Rows[0]["address"].ToString() != null && user.Tables[0].Rows[0]["address"].ToString() != "")
                //{
                //    lbladdress.Text = user.Tables[0].Rows[0]["address"].ToString();
                //}
                //else
                //{
                //    lbladdress.Text = "Not Available.";
                //}
                //<%--wasim comment above code 23 oct 17--%>

                //prajyot commented this for removing  doblbl 24-10-2017
                //if (user.Tables[0].Rows[0]["DOB"].ToString() != null && user.Tables[0].Rows[0]["DOB"].ToString() != "")
                //{
                //    DateTime dob = new DateTime();
                //    dob = Convert.ToDateTime(user.Tables[0].Rows[0]["DOB"]);
                    
                //    if (dob.ToString("dd/MMM/yyyy") == "01/Jan/1900")
                //    {
                //        lbldob.Text = "Not Available.";
                //    }
                //    else
                //    {
                //        lbldob.Text = dob.ToString("dd/MMM/yyyy");
                //    }
                //}
                //else
                //{
                //    lbldob.Text = "Not Available.";
                //}
                //if (user.Tables[0].Rows[0]["alternatemob"].ToString() != null && user.Tables[0].Rows[0]["alternatemob"].ToString() != "")
                //{
                //    lblaltmob.Text = user.Tables[0].Rows[0]["alternatemob"].ToString();
                //}
                //else
                //{
                //    lblaltmob.Text = "Not Available.";
                //}
                //lblmobno.Text = user.Tables[0].Rows[0]["mobileno"].ToString();
				  if (user.Tables[0].Rows[0]["mobileno"].ToString() != null && user.Tables[0].Rows[0]["mobileno"].ToString() != "")
                {
                    lblmobno.Text = user.Tables[0].Rows[0]["mobileno"].ToString();
                    lblmobno.Visible = false;
                    mob.Visible = false;
                    ls.Visible = false;
                }
                else
                {
                    // lblmobno.Text = "Not Available.";
					lblmobno.Visible = false;
					mob.Visible = false;
					ls.Visible = false;
                }
				
                if (user.Tables[0].Rows[0]["officemob"].ToString() != null && user.Tables[0].Rows[0]["officemob"].ToString() != "")
                {
                    lbloffmob.Text = user.Tables[0].Rows[0]["officemob"].ToString();
                }
                else
                {
                    lbloffmob.Text = "Not Available.";
                } 
                
                
                //<%--wasim comment below code 23 oct 17--%>
                //if (user.Tables[0].Rows[0]["telno"].ToString() != null && user.Tables[0].Rows[0]["telno"].ToString() != "")
                //{
                //    lbltelno.Text = user.Tables[0].Rows[0]["telno"].ToString();
                //}
                //else
                //{
                //    lbltelno.Text = "Not Available.";
                //}
                //if (user.Tables[0].Rows[0]["extentionno"].ToString() != null && user.Tables[0].Rows[0]["extentionno"].ToString() != "")
                //{
                //    lblextno.Text = user.Tables[0].Rows[0]["extentionno"].ToString();
                //}
                //else
                //{
                //    lblextno.Text = "Not Available.";
                //}
                //if (user.Tables[0].Rows[0]["officephone"].ToString() != null && user.Tables[0].Rows[0]["officephone"].ToString() != "")
                //{
                //    lblofftelno.Text = user.Tables[0].Rows[0]["officephone"].ToString();
                //}
                //else
                //{
                //    lblofftelno.Text = "Not Available.";
                //}
                //<%--wasim comment above code 23 oct 17--%>
                
              
                
                //if (user.Tables[0].Rows[0]["alternateemail"].ToString() != null && user.Tables[0].Rows[0]["alternateemail"].ToString() != "")
                //{
                //    lblaltemail.Text = user.Tables[0].Rows[0]["alternateemail"].ToString();
                //}
                //else
                //{
                //    lblaltemail.Text = "Not Available.";
                //}

                //if (user.Tables[0].Rows[0]["faxno"].ToString() != null && user.Tables[0].Rows[0]["faxno"].ToString() != "")
                //{
                //    lblfaxno.Text = user.Tables[0].Rows[0]["faxno"].ToString();
                //}
                //else
                //{
                //    lblfaxno.Text = "Not Available.";
                //}

                if (user.Tables[0].Rows[0]["location"].ToString() != null && user.Tables[0].Rows[0]["location"].ToString() != "")
                {
                    lblloc.Text = user.Tables[0].Rows[0]["location"].ToString();
                }
                else
                {
                    lblloc.Text = "Not Available.";
                }

                if (user.Tables[0].Rows[0]["department"].ToString() != null && user.Tables[0].Rows[0]["department"].ToString() != "")
                {
                    lbldept.Text = user.Tables[0].Rows[0]["department"].ToString();
                }
                else
                {
                    lbldept.Text = "Not Available.";
                }

                //if (user.Tables[0].Rows[0]["sub_department"].ToString() != null && user.Tables[0].Rows[0]["sub_department"].ToString() != "")
                //{
                //    lblsubdept.Text = user.Tables[0].Rows[0]["sub_department"].ToString();
                //}
                //else
                //{
                //    lblsubdept.Text = "Not Available.";
                //}

                if (user.Tables[0].Rows[0]["designation"].ToString() != null && user.Tables[0].Rows[0]["designation"].ToString() != "")
                {
                    lbldesg.Text = user.Tables[0].Rows[0]["designation"].ToString();
                }
                else
                {
                    lbldesg.Text = "Not Available.";
                }

                //<%--wasim comment above code 23 oct 17--%>
                //if (user.Tables[0].Rows[0]["tempaddress"].ToString() != null && user.Tables[0].Rows[0]["tempaddress"].ToString() != "")
                //{
                //    lbltempaddress.Text = user.Tables[0].Rows[0]["tempaddress"].ToString();
                //}
                //else
                //{
                //    lbltempaddress.Text = "Not Available.";
                //}
                //<%--wasim comment above code 23 oct 17--%>
            }
        }
        
    }

    protected void btnedit_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/editprofile");
    }
    protected void btnhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }
}
