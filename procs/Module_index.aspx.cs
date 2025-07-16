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
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;



public partial class Module_index : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc="", dept="", subdept="", desg = "";
    public int did = 0;
    LeaveBalance bl = new LeaveBalance();
    SP_Methods spm = new SP_Methods();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();



 

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url;}
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }



            hflEmpCode.Value  = Convert.ToString(Session["Empcode"]);
        // Session["Empcode"] = "11536";
        //  Session["Empcode"] = "12443";

          //  Session["Empcode"] = "11536";
            //Session["Empcode"] = "11549";

            //Session["emp_loginName"] = "Ms. User M1";
           // Response.Write(Session["Empcode"]);

            //hflEmpCode.Value = "11111";
            //Session["empcode"] = "11111";

            //hflEmpCode.Value = "22222";
            //Session["empcode"] = "22222";


              lblmsg.Visible = false;
               
            //  lblmsg.Text =Convert.ToString(Session["Empcode"]); 
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/leaveindex");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                   
                    //txtReason.Text = "This is after function";
                    CheckApprover();
                    checkHR_Inbox();
                    DisplayProfileProperties();

                     loadorder();
                     this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    protected void CheckApprover()
    {
        DataTable dtApprovers = new DataTable();
        dtApprovers = spm.CheckApprovers(Convert.ToString(hflEmpCode.Value).Trim ());
    }
    protected void GetLeaveCount()
    {
       
        int LeaveCount = 0;
        LeaveCount = spm.GetLeaveInboxCount(Convert.ToString(hflEmpCode.Value).Trim());

        int AttLeaveCount = 0;
        AttLeaveCount = spm.getAttendance_InboxList_Count(Convert.ToString(hflEmpCode.Value).Trim());

       
    }
    protected void loadorder()
    {
        DataTable dtp = classpkg.getcountorderdetailbyemaild(Page.User.Identity.Name.ToString().Trim());
        if (dtp.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtp.Rows[0]["orderid"].ToString()) == 0)
            {
                lihistory.Visible = false;
            }
        }
        else
        {
            lihistory.Visible = true;
        }
    }
    private void DisplayProfileProperties()
    {
        try
        {
            Boolean varfindcity = false;

            MembershipUser user = Membership.GetUser(this.Page.User.Identity.Name.ToString().Trim());
            DataSet ds_userdetails = classaddress.getalluserbyusername(this.Page.User.Identity.Name.ToString().Trim());
            if (ds_userdetails.Tables.Count > 0)
            {
                if( ds_userdetails.Tables[0].Rows.Count > 0)
                {
                   // txtemail.Text = ds_userdetails.Tables[0].Rows[0]["username"].ToString();
                   // txtemailadress.Text = ds_userdetails.Tables[0].Rows[0]["emailid"].ToString();
                    //txtfirstname.Text = ds_userdetails.Tables[0].Rows[0]["firstname"].ToString();
                    //txtlastname.Text = ds_userdetails.Tables[0].Rows[0]["lastname"].ToString();
                    //txtaddress1.Text = ds_userdetails.Tables[0].Rows[0]["address"].ToString();
                   // txtpincode.Text = ds_userdetails.Tables[0].Rows[0]["pincode"].ToString();
                    //txtphone.Text = ds_userdetails.Tables[0].Rows[0]["telno"].ToString();
                    //txtmobile.Text = ds_userdetails.Tables[0].Rows[0]["mobileno"].ToString();
                    //txttempaddress.Text = ds_userdetails.Tables[0].Rows[0]["tempaddress"].ToString();
                    //txtaltemail.Text = ds_userdetails.Tables[0].Rows[0]["alternateemail"].ToString();
                    //txtextension.Text = ds_userdetails.Tables[0].Rows[0]["extentionno"].ToString();
                    //txtoffno.Text = ds_userdetails.Tables[0].Rows[0]["officemob"].ToString();
                    //txtaltno.Text = ds_userdetails.Tables[0].Rows[0]["alternatemob"].ToString();
                    //txtoffphone.Text = ds_userdetails.Tables[0].Rows[0]["officephone"].ToString();
                    //txtfaxno.Text = ds_userdetails.Tables[0].Rows[0]["faxno"].ToString();
                  //  txtloc.Text = ds_userdetails.Tables[0].Rows[0]["location"].ToString();
                    //txtdept.Text = ds_userdetails.Tables[0].Rows[0]["department"].ToString();
                    //txtsubdept.Text = ds_userdetails.Tables[0].Rows[0]["sub_department"].ToString();
                    //txtdesg.Text = ds_userdetails.Tables[0].Rows[0]["designation"].ToString();

                    //DateTime dob1 = new DateTime();

                    //if (ds_userdetails.Tables[0].Rows[0]["DOB1"].ToString().Length > 0 && ds_userdetails.Tables[0].Rows[0]["DOB1"].ToString() != null && ds_userdetails.Tables[0].Rows[0]["DOB1"].ToString() != "")
                    //{
                    //    dob1 = Convert.ToDateTime(ds_userdetails.Tables[0].Rows[0]["DOB1"]);
                    //    if (dob1.ToString("dd/MMM/yyyy") == "01/Jan/1900")
                    //    {
                    //        txtdob1.Text = "";
                    //    }
                    //    else
                    //    {
                    //        txtdob1.Text = dob1.ToString("dd/MMM/yyyy");
                    //    }
                    //}
                    //else
                    //{
                    //    txtdob1.Text = "";
                    //}


                    DateTime dob = new DateTime();

                    if (ds_userdetails.Tables[0].Rows[0]["DOB"].ToString().Length > 0 && ds_userdetails.Tables[0].Rows[0]["DOB"].ToString() != null && ds_userdetails.Tables[0].Rows[0]["DOB"].ToString() != "")
                    {
                        dob = Convert.ToDateTime(ds_userdetails.Tables[0].Rows[0]["DOB"]);
                        if (dob.ToString("dd/MMM/yyyy") == "01/Jan/1900")
                        {
                           // txtdob.Text = "";
                        }
                        else
                        {
                          //  txtdob.Text = dob.ToString("dd/MMM/yyyy");
                        }
                    }
                    else
                    {
                        //txtdob.Text = "";
                    }

                    string gen = ds_userdetails.Tables[0].Rows[0]["gender"].ToString();
                    if (gen == "M" || gen == "m")
                    {
                        //rbtnmale.Checked = true;
                    }
                    else
                    {
                        //rbtnfemale.Checked = true;
                    }


                    DataTable user2 = classreviews.getuseridbyemail(Page.User.Identity.Name);

                    if (user2.Rows.Count > 0)
                    {
                        userid = user2.Rows[0]["indexid"].ToString();
                        if (user2.Rows[0]["profilephoto"].ToString() != "")
                        {
                            pimg = user2.Rows[0]["profilephoto"].ToString().Trim();
                            if (user2.Rows[0]["profilephoto"].ToString().Trim() == "noimage1.png" || user2.Rows[0]["profilephoto"].ToString().Trim() == "noimage3.jpg")
                            {
                              //  removeprofile.Visible = false;
                            }
                            else
                            {
                               // removeprofile.Visible = true;
                            }
                            if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profilephoto/" + user2.Rows[0]["profilephoto"].ToString())))
                            {
                              //  imgprofile.Src = ReturnUrl("sitepath") + "images/profilephoto/" + user2.Rows[0]["profilephoto"].ToString();
                            }
                            else
                            {
                               // imgprofile.Src = "http://graph.facebook.com/" + user2.Rows[0]["profilephoto"].ToString() + "/picture?type=large";
                            }
                        }
                        else
                        {
                           // imgprofile.Src = ConfigurationManager.AppSettings["adminsitepath"]+ "images/noprofile.jpg";
                           // removeprofile.Visible = false;
                        }
                        if (File.Exists(Server.MapPath("~/themes/creative1.0/images/coverphoto/" + user2.Rows[0]["coverphoto"].ToString())))
                        {
                            cimg = user2.Rows[0]["coverphoto"].ToString().Trim();
                            //imgcover.Src = ReturnUrl("sitepath") + "images/coverphoto/" + user2.Rows[0]["coverphoto"].ToString();
                        }
                        else
                        {
                            //imgcover.Visible = false;
                            //removecover.Visible = false;
                        }
                    }
                    else
                    {
                       // imgprofile.Visible = false;
                        //imgcover.Visible = false;
                    }
                   
                   
                }
            }
            
        }
        catch (Exception ex)
        {

        }
    }

    protected void checkHR_Inbox()
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_Check_TD_COS_apprver_code";

            spars[1] = new SqlParameter("@exp_trvl", SqlDbType.VarChar);
            spars[1].Value = DBNull.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = hflEmpCode.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");
            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                get_Pending_LeaveReqstList_cnt();
                
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void get_Pending_LeaveReqstList_cnt()
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_inboxlst_cnt_HR";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = hflEmpCode.Value;
            dsTrDetails = spm.getDatasetList(spars, "Usp_getEmployee_Details_All");

            //Travel Request Count

             
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
}
