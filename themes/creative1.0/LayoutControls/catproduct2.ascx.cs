using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Data.Common;
using System.Configuration;
using System.Data.SqlClient;

public partial class themes_creative_LayoutControls_catproduct2 : System.Web.UI.UserControl
{
    public string user = "";
    public  string catid = "";
    public string fname = "";
    public static int surveyid;
    public static int indexid;
    string linkbutton1;
    public static string file = "";
    public Int32 Leave_Cnt = 0;
    SP_Methods spm = new SP_Methods();
    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;}
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
                //string user = new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent()).Identity.Name;
                //string Name = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                ////Response.Write("Username := " + user + "<br>" + Name);
                //lbladsid.Text = "Username := " + user;
                //meeet();
                //taskmeeetings();
                //SAGAR COMMENTED THIS FOR REMOVING QUICK LINKS FRONT END 28SEPT2017
                //loadhcclinks();
                //popularcontrol();
                //SAGAR COMMENTED THIS FOR REMOVING PHOTO GALLERY FROM THE FRONT END 28SEPT2017
                loadsingleimg();

                //SAGAR COMMENTED THIS FOR REMOVING PHOTO GALLERY FROM THE FRONT END 28SEPT2017
                //loadimg();
                //SAGAR COMMENTED THIS FOR REMOVING NEWSLETTER FROM THE FRONT END 28SEPT2017
                //loaddocument();

                //JAYESH COMMENTED BELOW CODE TO HIDE ARTICLE FUNCTIONALITY FORM HOME PAGE 28sep2017
                loadads();
                //JAYESH COMMENTED ABOVE CODE TO HIDE ARTICLE FUNCTIONALITY FORM HOME PAGE 28sep2017

                //SAGAR COMMENTED THIS FOR REMOVING FUNZONE FROM THE FRONT END 28SEPT2017
                //loadfunzone();
                //SAGAR COMMENTED THIS FOR REMOVING VIDEO FROM THE FRONT END 28SEPT2017
                // loadvideo();
                //loadsurvey();
                //SAGAR COMMENTED THIS FOR REMOVING PROJECT FROM THE FRONT END 28SEPT2017 
                //loaduniversal();

                //meeting();
                //Comment by Sanjay on 14.01.2025 meetings();
                //Comment by Sanjay on 14.01.2025 task();
                ////30-10-2020
                ////Leave_Cnt = 0;
                ////int LeaveCount = 0;
                ////if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
                ////{
                ////    LeavesHead.InnerText = "Leaves";
                ////}
                ////else
                ////{
                ////    LeaveCount = spm.GetLeaveInboxCount(Convert.ToString(Session["Empcode"]).Trim());
                ////    Leave_Cnt = Leave_Cnt + LeaveCount;
                ////    get_Pending_LeaveReqstList_cnt(Convert.ToString(Session["Empcode"]).Trim());
                ////    if (Leave_Cnt > 0)
                ////        LeavesHead.InnerText = "Leaves (" + Convert.ToString(Leave_Cnt) + ")";
                ////    else
                ////        LeavesHead.InnerText = "Leaves";
                ////}
                ////30-10-2020
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    public void get_Pending_LeaveReqstList_cnt(string emp_code)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_inboxlst_cnt_HR";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = emp_code;

            dsTrDetails = spm.getDatasetList(spars, "Usp_getEmployee_Details_All");

            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                Leave_Cnt = Leave_Cnt + Convert.ToInt32(dsTrDetails.Tables[0].Rows[0]["leave_reqst_pending"]);
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    //public void loadhcclinks()
    //{
    //    HttpCookie cookieuser = HttpContext.Current.Request.Cookies["user"];
    //    if (cookieuser != null)
    //    {
    //        if (cookieuser.Value.ToString().Trim() == "Highbar")
    //        {
    //            pnlhcclinks.Visible =true;
    //        }
    //        else
    //        {
    //            pnlhcclinks.Visible = false;
    //        }
    //    }
    //    else
    //    {
    //        pnlhcclinks.Visible = false;
    //        //changes by sony
    //        //pnlhcclinks.Visible = false;
    //    }
    //    HttpCookie cookie = HttpContext.Current.Request.Cookies["internet"];
    //    if (cookie != null)
    //    {
    //        if (cookie.Value.ToString().Trim() == "true")
    //        {
    //            lnkzp.HRef = ConfigurationManager.AppSettings["ZP1"].ToString();
    //            lnkzc.HRef = ConfigurationManager.AppSettings["ZC1"].ToString();
    //            lnktbc.HRef = ConfigurationManager.AppSettings["TBC1"].ToString();
    //        }
    //        else
    //        {
    //            lnkzp.HRef = ConfigurationManager.AppSettings["ZP2"].ToString();
    //            lnkzc.HRef = ConfigurationManager.AppSettings["ZC2"].ToString();
    //            lnktbc.HRef = ConfigurationManager.AppSettings["TBC2"].ToString();
    //        }
    //    }
    //    else
    //    {
    //        lnkzp.HRef = ConfigurationManager.AppSettings["ZP2"].ToString();
    //        lnkzc.HRef = ConfigurationManager.AppSettings["ZC2"].ToString();
    //        lnktbc.HRef = ConfigurationManager.AppSettings["TBC2"].ToString();
    //    }
    //}
    public string GetSafeFileName(string Filename)
    {
        string newStr = "";
        Filename = Filename.Replace("<", newStr);
        Filename = Filename.Replace(">", newStr);
        Filename = Filename.Replace(" ", newStr);
        Filename = Filename.Replace("%", newStr);
        Filename = Filename.Replace("*", newStr);
        Filename = Filename.Replace("|", newStr);
        Filename = Filename.Replace("-", newStr);
        Filename = Filename.Replace("#", newStr);
        Filename = Filename.Replace("&", newStr);
        Filename = Filename.Replace("@", newStr);
        Filename = Filename.Replace("!", newStr);
        Filename = Filename.Replace("$", newStr);
        Filename = Filename.Replace(":", newStr);
        Filename = Filename.Replace(";", newStr);
        Filename = Filename.Replace("(", newStr);
        Filename = Filename.Replace(")", newStr);
        Filename = Filename.Replace("+", newStr);
        Filename = Filename.Replace("/", newStr);
        Filename = Filename.Replace(".", newStr);
        Filename = Filename.Replace("  ", newStr);
        Filename = Filename.Replace(",", newStr);
        Filename = Filename.Replace("'", newStr);
        Filename = Filename.Replace("+", newStr);
        return Filename.ToLower();
    }

    //protected void taskmeeetings()
    //{
    //        string name;
    //        if (Page.User.Identity.IsAuthenticated)
    //        {

    //            DataTable user = classreviews.getuseridbyemail(Page.User.Identity.Name);
    //            name = user.Rows[0]["username"].ToString().Trim();
    //            DataTable task = classcategory.gettaskpost(catid);
    //            //if (catid == "46")//task
    //            //{
    //            if (user.Rows.Count > 0)
    //            {
    //                //DataTable task = classcategory.gettaskpost(catid);
    //                rptfun.DataSource = user;
    //                rptfun.DataBind();
    //                for (int j = 0; j < rptcategname.Items.Count; j++)
    //                {
    //                    Label lbltask = (Label)rptcategname.Items[j].FindControl("productname");
    //                }
    //                //if (catid == "31")//meeting
    //                //{
    //                //    DataTable task = classcategory.gettaskpost(catid);
    //                //    //rptcatproduct.DataSource=task;
    //                //    //rptcatproduct.DataBind();
    //                //}
    //                //Response.Write(name);
    //                //Response.End();
    //            }

    //        }
    //}

    // SAGAR ADDED BELOW working code for NEW TASK AND MEETINGS PANEL ,recored fetch from database 30nov2017
    protected void task()
    {
        //creative.Common clsCommon = new creative.Common();
        string username = Page.User.Identity.Name.ToString().Trim();
         DataTable dt = classaddress.task(username);
        //DataSet dtc = classaddress.taskdemo();
        if (dt.Rows.Count > 0)
        {
         taskS.DataSource = dt.AsEnumerable().Take(3).CopyToDataTable();//dt
        taskS.DataBind();
        
        //Response.Write(username);
        //Response.End();
		}
    }
  protected void emptyRepeater_PreRender(object sender, EventArgs e)
    {
        lblEmptyRepeater.Visible = (taskS.Items.Count == 0);
    }
    protected void meetings()
    {
        //creative.Common clsCommon = new creative.Common();
        string username = Page.User.Identity.Name.ToString().Trim();
         //string username = Page.User.Identity.Name.ToString().Trim();
        DataTable dt = classaddress.meetings(username);
        if (dt.Rows.Count > 0)
        {
            meetingS.DataSource = dt.AsEnumerable().Take(3).CopyToDataTable();//dt
			meetingS.DataBind();
		}
    }
	
	   protected void emptyRepeater_PreRender1(object sender, EventArgs e)
    {
        lblEmptyRepeater1.Visible = (meetingS.Items.Count == 0);
    }
	
   //SAGAR ADDED above working code for  NEW TASK AND MEETINGS PANEL ,recored fetch from database 30nov2017
//sagar added below code for tsk and event 25nov2017 starts here
    //string str = Page.User.Identity.Name;
    //categoryname

    ////sagar added above code for task and event 25nov2017 end here
    
    //protected void meeting()
    //{
    //    try
    //    {
    //        string posttype = "";
    //        //string username = Page.User.Identity.Name;
    //        //Response.Write(username);
    //        //Response.End();
    //        creative.Common clsCommon = new creative.Common();
    //        DataTable dthome = classcategory.getallcatsbyhomestatus();
    //        if (dthome.Rows.Count > 0)
    //        {

    //            rpes.DataSource = dthome;
    //            rpes.DataBind();
                
    //            for (int j = 0; j < rpes.Items.Count; j++)
    //            {
    //                string username = Page.User.Identity.Name.ToString().Trim();
    //                DataSet dt = classaddress.task(username);//Page.User.Identity.Name.ToString().Trim()

    //                //if (dt.Tables.Count > 0)
    //                //{
    //                //    if (dt.Tables[0].Rows.Count > 0)
    //                //    {

    //                        Repeater rpes1 = (Repeater)rpes.Items[j].FindControl("rpes1");
    //                        //    Label lblid1 = (Label)rptcategname.Items[j].FindControl("lbcats1");
    //                        //    LinkButton lnkb = (LinkButton)rptcategname.Items[j].FindControl("lnkview");
    //                        //    posttype = dthome.Rows[j]["cattype"].ToString().Trim();
    //                        //    string strid = lblid1.Text;

    //                        //    if (strid != null || strid != "")
    //                        //    {
    //                        //        DataSet dt = classproject.getmeetings(username);
    //                        //        if (dt.Tables.Count > 0)
    //                        //        {
    //                        rpes1.DataSource = dt;
    //                        rpes1.DataBind();
    //                       //Label l1 = dt.Tables[0].Rows[0]["username"].ToString();
    //                        //                if (rpes1.Items.Count > 0)
    //                        //                {
    //                        //                    if (rpes1.Items.Count >= 6)
    //                        //                    {
    //                        //                        lnkb.Visible = true;
    //                        //                    }
    //                        //                    else
    //                        //                    {
    //                        //                        lnkb.Visible = true;
    //                        //                    }
    //                        //                    for (int i = 0; i < rpes1.Items.Count; i++)
    //                        //                    {
    //                        //                        Label lbleventdate1 = (Label)rpes1.Items[i].FindControl("lbleventdate1");
    //                        //                        Label lblicon1 = (Label)rpes1.Items[i].FindControl("lblicon1");
    //                        //                        if (posttype == "E")
    //                        //                        {
    //                        //                            lbleventdate1.Visible = true;
    //                        //                            lblicon1.Visible = false;
    //                        //                            lbleventdate1.Text = Convert.ToDateTime(lbleventdate1.Text.Trim()).ToString("dd MMM yy");
    //                        //                            //wasim remove time in task and meeting above code(hh:mm) 13 oct 17
    //                        //                        }


    //                        //                        else
    //                        //                        {
    //                        //                            lbleventdate1.Visible = false;
    //                        //                        }
    //                        //                    }

    //                        //                }
    //                        //            }
    //                    //}
    //                    //}

    //                //}

    //            }


    //            //}
    //        }
    //        //Response.Write(posttype);
    //        //Response.End();
    //    }
    //    //try
    //    //{
    //    //    //creative.Common clsCommon = new creative.Common();
    //    //    DataTable dt = classproject.getmeetings(username);
    //    //    if (dthome.Rows.Count > 0)
    //    //    {
    //    //        rpes.DataSource = dthome;
    //    //        rpes.DataBind();
    //    //        //for (int j = 0; j < rpes.Items.Count; j++)
    //    //        //{
    //    //        //    Label ll= (Label)rpes.Items[j].FindControl("ll");
    //    //        //}
    //    //    }
    //    //}
    //    catch (Exception ex)
    //    {
    //        ErrorLog.WriteError(ex.ToString());

    //    }
    //}
    //protected void meeet()
    //{
    //    string username = Page.User.Identity.Name;
    //    DataTable dt = classproject.getmeetings(username);
    //    //if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
    //    //{
    //        Repeater reps1 = FindControl("reps1") as Repeater;
    //        reps1.DataSource = dt;
    //        reps1.DataBind();


    //    //}
    //}

    //protected void popularcontrol() // announcement
    //{

    //    try
    //    {
    //        string posttype = "";

    //        creative.Common clsCommon = new creative.Common();
    //        DataTable dthome = classcategory.getallcatsbyhomestatus();

    //        if (dthome.Rows.Count > 0)
    //        {
    //            rptcategname.DataSource = dthome;
    //            rptcategname.DataBind();
    //            //LinkButton lk = (LinkButton)rptcategname.Items[j].FindControl("lk1");

    //            for (int j = 0; j < rptcategname.Items.Count; j++)
    //            {
    //                posttype = dthome.Rows[j]["cattype"].ToString().Trim();
    //                Label lblid = (Label)rptcategname.Items[j].FindControl("lbcatlid");
    //                LinkButton lnkb = (LinkButton)rptcategname.Items[j].FindControl("lnkview");


    //                string strid = lblid.Text;


    //                //  Response.Write("SONY" + lblid.Text);

    //                if (strid != null || strid != "")
    //                {
    //                    Repeater rptcatproduct = (Repeater)rptcategname.Items[j].FindControl("rptcatproduct");
    //                    Panel pnlcat = (Panel)rptcategname.Items[j].FindControl("pnlcat");
    //                    //DataTable dtp = classcategory.getproductidbycatidsdisplayhome(string);
    //                    DataSet dtp = new DataSet();
    //                    string xmlfilename;
    //                    xmlfilename = GetSafeFileName(dthome.Rows[j]["categoryname"].ToString());

    //                    string xmlfileuser = Request.PhysicalApplicationPath + "\\xml\\" + xmlfilename + ".xml";
    //                    string xmlfileadmin = ReturnUrl("adminsitepath") + "xml/" + xmlfilename + ".xml";
    //                    string localPath = new Uri(xmlfileadmin).LocalPath;

    //                    if (File.Exists(xmlfileuser))
    //                    {
    //                        DateTime udt = Convert.ToDateTime(File.GetLastWriteTime(xmlfileuser).ToString());
    //                        DateTime adt = Convert.ToDateTime(File.GetLastWriteTime(Server.MapPath(localPath)).ToString());
    //                        if (udt > adt)
    //                        {
    //                            dtp.ReadXml(ReturnUrl("sitepathmain") + "xml/" + xmlfilename + ".xml");
    //                            if (dtp.Tables.Count > 0)
    //                            {
    //                                if (dtp.Tables[0].Rows.Count > 0)
    //                                {
    //                                    rptcatproduct.DataSource = dtp;
    //                                    rptcatproduct.DataBind();
    //                                    if (rptcatproduct.Items.Count > 0)
    //                                    {
    //                                        if (rptcatproduct.Items.Count >= 6)
    //                                        {
    //                                            lnkb.Visible = true;
    //                                        }
    //                                        else
    //                                        {
    //                                            lnkb.Visible = true;
    //                                        }
    //                                        //if (catid == "46")
    //                                        //{
    //                                        //    lk.Visible = false;
    //                                        //}
    //                                        //else { }
    //                                        for (int i = 0; i < rptcatproduct.Items.Count; i++)
    //                                        {
    //                                            Label lbleventdate = (Label)rptcatproduct.Items[i].FindControl("lbleventdate");
    //                                            Label lblicon = (Label)rptcatproduct.Items[i].FindControl("lblicon");
    //                                            //sony commented this
    //                                            //  if (posttype == "E")
    //                                            //sony addedthis


    //                                            if (posttype == "E")
    //                                            {
    //                                                lbleventdate.Visible = true;
    //                                                lblicon.Visible = false;
    //                                                lbleventdate.Text = Convert.ToDateTime(lbleventdate.Text.Trim()).ToString("dd MMM yy");
    //                                                //wasim remove time in task and meeting above code(hh:mm) 13 oct 17
    //                                            }


    //                                            else
    //                                            {
    //                                                lbleventdate.Visible = false;
    //                                            }

    //                                            //sony try code
    //                                            //Response.Write(strid+"HELLO" + int.Parse(strid));

    //                                            //if (int.Parse(strid) == 46)
    //                                            // {
    //                                            //     lbleventdate.Visible = false;
    //                                            //     Response.Write("id is 46 only");

    //                                            // }
    //                                            //else
    //                                            //    Response.Write("id is NOT 46 only");

    //                                        }
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    pnlcat.Visible = false;
    //                                }
    //                            }
    //                            else
    //                            {
    //                                pnlcat.Visible = false;
    //                            }
    //                            dtp.Dispose();
    //                        }
    //                        else
    //                        {
    //                            dtp.ReadXml(ReturnUrl("adminsitepath") + "xml/" + xmlfilename + ".xml");
    //                            if (dtp.Tables.Count > 0)
    //                            {
    //                                if (dtp.Tables[0].Rows.Count > 0)
    //                                {
    //                                    rptcatproduct.DataSource = dtp;
    //                                    rptcatproduct.DataBind();
    //                                    if (rptcatproduct.Items.Count > 0)
    //                                    {
    //                                        if (rptcatproduct.Items.Count >= 6)
    //                                        {
    //                                            lnkb.Visible = true;
    //                                        }
    //                                        else
    //                                        {
    //                                            lnkb.Visible = true;
    //                                        }
    //                                        for (int i = 0; i < rptcatproduct.Items.Count; i++)
    //                                        {
    //                                            Label lbleventdate = (Label)rptcatproduct.Items[i].FindControl("lbleventdate");
    //                                            Label lblicon = (Label)rptcatproduct.Items[i].FindControl("lblicon");
    //                                            if (posttype == "E")
    //                                            {
    //                                                lbleventdate.Visible = true;
    //                                                lblicon.Visible = false;
    //                                                lbleventdate.Text = Convert.ToDateTime(lbleventdate.Text.Trim()).ToString("dd MMM yy");
    //                                                //wasim remove time in task and meeting (hh:mm) 13 oct 17
    //                                            }
    //                                            else
    //                                            {
    //                                                lbleventdate.Visible = false;
    //                                            }
    //                                        }
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    pnlcat.Visible = false;
    //                                }
    //                            }
    //                            else
    //                            {
    //                                pnlcat.Visible = false;
    //                            }
    //                            dtp.Dispose();
    //                        }
    //                    }
    //                    else
    //                    {
    //                        dtp.ReadXml(ReturnUrl("adminsitepath") + "xml/" + xmlfilename + ".xml");
    //                        if (dtp.Tables.Count > 0)
    //                        {
    //                            if (dtp.Tables[0].Rows.Count > 0)
    //                            {
    //                                rptcatproduct.DataSource = dtp;
    //                                rptcatproduct.DataBind();
    //                                if (rptcatproduct.Items.Count > 0)
    //                                {
    //                                    if (rptcatproduct.Items.Count >= 6)
    //                                    {
    //                                        lnkb.Visible = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        lnkb.Visible = true;
    //                                    }
    //                                    for (int i = 0; i < rptcatproduct.Items.Count; i++)
    //                                    {
    //                                        Label lbleventdate = (Label)rptcatproduct.Items[i].FindControl("lbleventdate");
    //                                        Label lblicon = (Label)rptcatproduct.Items[i].FindControl("lblicon");
    //                                        if (posttype == "E")
    //                                        {

    //                                            lbleventdate.Visible = true;
    //                                            lblicon.Visible = false;
    //                                            lbleventdate.Text = Convert.ToDateTime(lbleventdate.Text.Trim()).ToString("dd MMM yy");
    //                                            //wasim remove time in task and meeting (hh:mm) 13 oct 17
    //                                        }
    //                                        else
    //                                        {
    //                                            lbleventdate.Visible = false;
    //                                        }
    //                                    }
    //                                }
    //                            }
    //                            else
    //                            {
    //                                pnlcat.Visible = false;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            pnlcat.Visible = false;
    //                        }
    //                        dtp.Dispose();
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLog.WriteError(ex.ToString());
    //    }
    //}

    protected void rptcategname_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
    }

    protected void rptcatproduct_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
    }
    
    public string onclick_hlnkcategory(Object catId, Object catname)
    {
        string strurl = "";
       
        strurl = UrlRewritingVM.getUrlRewritingInfo(catname, UrlRewritingVM.Encrypt(catId.ToString().Trim()),"PS");
        
        return strurl;
    }

    //public string onclick_hlnkcategory1(Object catId)
    //{
    //    string catname = "";
    //    string strurl = "";

    //    strurl = UrlRewritingVM.getUrlRewritingInfo(catname, UrlRewritingVM.Encrypt(catId.ToString().Trim()), "PS");

    //    return strurl;
    //}


    //SAGAR COMMENTED THIS FOR REMOVING PHOTO GALLERY FROM THE FRONT END 28SEPT2017 STARTS HERE
    //public void loadimg()
    //{
    //    try
    //    {
    //        creative.Common clsCommon = new creative.Common();
    //        DataTable dthome = classcategory.getallcatsforimg();
    //        if (dthome.Rows.Count > 0)
    //        {
    //            rptcatimg.DataSource = dthome;
    //            rptcatimg.DataBind();
    //            for (int j = 0; j < rptcatimg.Items.Count; j++)
    //            {
    //                Label lblimgid = (Label)rptcatimg.Items[j].FindControl("lblimgid");
    //                LinkButton lnkb = (LinkButton)rptcatimg.Items[j].FindControl("lnkview");
    //                string strid = lblimgid.Text;
    //                if (strid != null || strid != "")
    //                {
    //                    Repeater rptimg = (Repeater)rptcatimg.Items[j].FindControl("rptimg");
    //                    Panel panimg = (Panel)rptcatimg.Items[j].FindControl("pnlphoto");
    //                    //DataTable grp = classproductimage.latestimage(Convert.ToInt32(strid));
    //                    DataSet grp = new DataSet();
    //                    string xmlfilename;
    //                    xmlfilename = GetSafeFileName(dthome.Rows[j]["categoryname"].ToString());
    //                    string xmlfileuser = Request.PhysicalApplicationPath + "\\xml\\" + xmlfilename + ".xml";
    //                    string xmlfileadmin = ReturnUrl("adminsitepath") + "xml/" + xmlfilename + ".xml";
    //                    string localPath = new Uri(xmlfileadmin).LocalPath;

    //                    if (File.Exists(xmlfileuser))
    //                    {
    //                        DateTime udt = Convert.ToDateTime(File.GetLastWriteTime(xmlfileuser).ToString());
    //                        DateTime adt = Convert.ToDateTime(File.GetLastWriteTime(Server.MapPath(localPath)).ToString());
    //                        if (udt > adt)
    //                        {
    //                            grp.ReadXml(ReturnUrl("sitepathmain") + "xml/" + xmlfilename + ".xml");
    //                            if (grp.Tables.Count > 0)
    //                            {
    //                                if (grp.Tables[0].Rows.Count > 0)
    //                                {
    //                                    rptimg.DataSource = grp;//grp.Tables[0].AsEnumerable().Take(4).CopyToDataTable();
    //                                    rptimg.DataBind();
    //                                    panimg.Visible = true;
    //                                    int cnt = rptimg.Items.Count;
    //                                    if (cnt > 5)
    //                                    {
    //                                        lnkb.Visible = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        lnkb.Visible = true;
    //                                    }
    //                                    for (int i = 0; i < cnt; i++)
    //                                    {
    //                                        Image img = (Image)rptimg.Items[i].FindControl("imghome");
    //                                        string fileName = Request.PhysicalApplicationPath + "\\images\\180x120\\" + grp.Tables[0].Rows[i]["bigimage"].ToString();
    //                                        if (File.Exists(fileName))
    //                                        {
    //                                            img.ImageUrl = ReturnUrl("sitepathmain") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
    //                                        }
    //                                        else
    //                                        {
    //                                            img.ImageUrl = ReturnUrl("sitepathadmin") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
    //                                        }
    //                                        img.Attributes.Add("class", "gallary-image");
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    panimg.Visible = false;
    //                                }
    //                            }
    //                            else
    //                            {
    //                                panimg.Visible = false;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            grp.ReadXml(ReturnUrl("adminsitepath") + "xml/" + xmlfilename + ".xml");
    //                            if (grp.Tables.Count > 0)
    //                            {
    //                                if (grp.Tables[0].Rows.Count > 0)
    //                                {
    //                                    rptimg.DataSource = grp;//grp.Tables[0].AsEnumerable().Take(4).CopyToDataTable();
    //                                    rptimg.DataBind();
    //                                    panimg.Visible = true;
    //                                    int cnt = rptimg.Items.Count;
    //                                    if (cnt >= 5)
    //                                    {
    //                                        lnkb.Visible = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        lnkb.Visible = true;
    //                                    }
    //                                    for (int i = 0; i < cnt; i++)
    //                                    {
    //                                        Image img = (Image)rptimg.Items[i].FindControl("imghome");
    //                                        string fileName = Request.PhysicalApplicationPath + "\\images\\180x120\\" + grp.Tables[0].Rows[i]["bigimage"].ToString();
    //                                        if (File.Exists(fileName))
    //                                        {
    //                                            img.ImageUrl = ReturnUrl("sitepathmain") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
    //                                        }
    //                                        else
    //                                        {
    //                                            img.ImageUrl = ReturnUrl("sitepathadmin") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
    //                                        }
    //                                        img.Attributes.Add("class", "gallary-image");
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    panimg.Visible = false;
    //                                }
    //                            }
    //                            else
    //                            {
    //                                panimg.Visible = false;
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        grp.ReadXml(ReturnUrl("adminsitepath") + "xml/" + xmlfilename + ".xml");
    //                        if (grp.Tables.Count > 0)
    //                        {
    //                            if (grp.Tables[0].Rows.Count > 0)
    //                            {
    //                                rptimg.DataSource = grp;//grp.Tables[0].AsEnumerable().Take(4).CopyToDataTable();
    //                                rptimg.DataBind();
    //                                panimg.Visible = true;
    //                                int cnt = rptimg.Items.Count;
    //                                if (cnt >= 5)
    //                                {
    //                                    lnkb.Visible = true;
    //                                }
    //                                else
    //                                {
    //                                    lnkb.Visible = true;
    //                                }
    //                                for (int i = 0; i < cnt; i++)
    //                                {
    //                                    Image img = (Image)rptimg.Items[i].FindControl("imghome");
    //                                    string fileName = Request.PhysicalApplicationPath + "\\images\\180x120\\" + grp.Tables[0].Rows[i]["bigimage"].ToString();
    //                                    if (File.Exists(fileName))
    //                                    {
    //                                        img.ImageUrl = ReturnUrl("sitepathmain") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
    //                                    }
    //                                    else
    //                                    {
    //                                        img.ImageUrl = ReturnUrl("sitepathadmin") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
    //                                    }

    //                                    img.Attributes.Add("class", "gallary-image");
    //                                }
    //                            }
    //                            else
    //                            {
    //                                panimg.Visible = false;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            panimg.Visible = false;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLog.WriteError(ex.ToString());
    //    }
    //}
    //SAGAR COMMENTED THIS FOR REMOVING PHOTO GALLERY FROM THE FRONT END 28SEPT2017 ENDS HERE

//SAGAR ADDED THIS FOR ADDING TIME Out IN THE FRONT END 2OCT2017 STARTS HERE
    public void loadsingleimg()
    {
        try
        {
            creative.Common clsCommon = new creative.Common();
            DataTable dthome = classcategory.getallcatsforimg();
            if (dthome.Rows.Count > 0)
            {

                rptcatimg.DataSource = dthome;
                rptcatimg.DataBind();
                for (int j = 0; j < rptcatimg.Items.Count; j++)
                {
                    
                    Label lblimgid = (Label)rptcatimg.Items[j].FindControl("lblimgid");
                    LinkButton lnkb = (LinkButton)rptcatimg.Items[j].FindControl("lnkview");
                    string strid = lblimgid.Text;
                    if (strid != null || strid != "")
                    {
                        Repeater rptimg = (Repeater)rptcatimg.Items[j].FindControl("rptimg");
                        Panel panimg = (Panel)rptcatimg.Items[j].FindControl("pnlphoto");
                        //DataTable grp = classproductimage.latestimage(Convert.ToInt32(strid));
                        DataSet grp = new DataSet();
                        string xmlfilename;
                        xmlfilename = GetSafeFileName(dthome.Rows[j]["categoryname"].ToString());
                        string xmlfileuser = Request.PhysicalApplicationPath + "\\xml\\" + xmlfilename + ".xml";
                        string xmlfileadmin = ReturnUrl("adminsitepath") + "xml/" + xmlfilename + ".xml";
                        string localPath = new Uri(xmlfileadmin).LocalPath;

                        if (File.Exists(xmlfileuser))
                        {
                            DateTime udt = Convert.ToDateTime(File.GetLastWriteTime(xmlfileuser).ToString());
                            DateTime adt = Convert.ToDateTime(File.GetLastWriteTime(Server.MapPath(localPath)).ToString());
                            if (udt > adt)
                            {
                                grp.ReadXml(ReturnUrl("sitepathmain") + "xml/" + xmlfilename + ".xml");
                                if (grp.Tables.Count > 0)
                                {
                                    if (grp.Tables[0].Rows.Count > 0)
                                    {
                                        //SAGAR CHANGED BELOW LINE grp;//grp.Tables[0].AsEnumerable().Take(4).CopyToDataTable(); TO grp.Tables[0].AsEnumerable().Take(1).CopyToDataTable(); FOR SHOWING ONLY 1 POST FROM ACHIVEMENTS AND TIME OFF 16OCT2017
                                        rptimg.DataSource = grp.Tables[0].AsEnumerable().Take(1).CopyToDataTable();
                                        rptimg.DataBind();
                                        panimg.Visible = true;
                                        int cnt = rptimg.Items.Count;
                                        if (cnt > 3)
                                        {
                                            cnt = 3;
                                            lnkb.Visible = true;
                                        }
                                        else
                                        {
                                            lnkb.Visible = true;
                                        }

                                        for (int i = 0; i < cnt; i++)
                                        {
                                            Image img = (Image)rptimg.Items[i].FindControl("imghome");
                                            string fileName = Request.PhysicalApplicationPath + "\\images\\180x120\\" + grp.Tables[0].Rows[i]["bigimage"].ToString();
                                            if (File.Exists(fileName))
                                            {
                                                img.ImageUrl = ReturnUrl("sitepathmain") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
                                            }
                                            else
                                            {
                                                img.ImageUrl = ReturnUrl("sitepathadmin") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
                                            }
                                            img.Attributes.Add("class", "gallary-image");
                                        }
                                    }
                                    else
                                    {
                                        panimg.Visible = false;
                                    }
                                }
                                else
                                {
                                    panimg.Visible = false;
                                }
                            }
                            else
                            {
                                grp.ReadXml(ReturnUrl("adminsitepath") + "xml/" + xmlfilename + ".xml");
                                if (grp.Tables.Count > 0)
                                {
                                    if (grp.Tables[0].Rows.Count > 0)
                                    {
                                        //SAGAR CHANGED BELOW LINE grp;//grp.Tables[0].AsEnumerable().Take(4).CopyToDataTable(); TO grp.Tables[0].AsEnumerable().Take(1).CopyToDataTable(); FOR SHOWING ONLY 1 POST FROM ACHIVEMENTS AND TIME OFF 16OCT2017
                                        rptimg.DataSource = grp.Tables[0].AsEnumerable().Take(1).CopyToDataTable();
                                        rptimg.DataBind();
                                        panimg.Visible = true;
                                        int cnt = rptimg.Items.Count;
                                        if (cnt >= 3)
                                        {
                                            cnt = 3;
                                            lnkb.Visible = true;
                                        }
                                        else
                                        {
                                            lnkb.Visible = true;
                                        }
                                        for (int i = 0; i < cnt; i++)
                                        {
                                            Image img = (Image)rptimg.Items[i].FindControl("imghome");
                                            string fileName = Request.PhysicalApplicationPath + "\\images\\180x120\\" + grp.Tables[0].Rows[i]["bigimage"].ToString();
                                            if (File.Exists(fileName))
                                            {
                                                img.ImageUrl = ReturnUrl("sitepathmain") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
                                            }
                                            else
                                            {
                                                img.ImageUrl = ReturnUrl("sitepathadmin") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
                                            }
                                            img.Attributes.Add("class", "gallary-image");
                                        }
                                    }
                                    else
                                    {
                                        panimg.Visible = false;
                                    }
                                }
                                else
                                {
                                    panimg.Visible = false;
                                }
                            }
                        }
                        else
                        {
                            grp.ReadXml(ReturnUrl("adminsitepath") + "xml/" + xmlfilename + ".xml");
                            if (grp.Tables.Count > 0)
                            {
                                if (grp.Tables[0].Rows.Count > 0)
                                {
                                    //SAGAR CHANGED BELOW LINE grp;//grp.Tables[0].AsEnumerable().Take(4).CopyToDataTable(); TO grp.Tables[0].AsEnumerable().Take(1).CopyToDataTable(); FOR SHOWING ONLY 1 POST FROM ACHIVEMENTS AND TIME OFF 16OCT2017
                                    rptimg.DataSource = grp.Tables[0].AsEnumerable().Take(1).CopyToDataTable();
                                    rptimg.DataBind();
                                    panimg.Visible = true;
                                    int cnt = rptimg.Items.Count;
                                    if (cnt >= 3)
                                    {
                                        cnt = 3;
                                       // Comment by R1 on 01.10.2018 View option not required lnkb.Visible = true;
                                        lnkb.Visible = false;
                                    }
                                    else
                                    {
                                        // Comment by R1 on 01.10.2018 View option not required lnkb.Visible = true;
                                        lnkb.Visible = false;
                                        
                                    }
                                    for (int i = 0; i < cnt; i++)
                                    {
                                        Image img = (Image)rptimg.Items[i].FindControl("imghome");
                                        string fileName = Request.PhysicalApplicationPath + "\\images\\180x120\\" + grp.Tables[0].Rows[i]["bigimage"].ToString();
                                        if (File.Exists(fileName))
                                        {
                                            img.ImageUrl = ReturnUrl("sitepathmain") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
                                        }
                                        else
                                        {
                                            img.ImageUrl = ReturnUrl("sitepathadmin") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
                                        }

                                        img.Attributes.Add("class", "gallary-image");
                                    }
                                }
                                else
                                {
                                    panimg.Visible = false;
                                }
                            }
                            else
                            {
                                panimg.Visible = false;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    //SAGAR COMMENTED THIS FOR REMOVING NEWSLETTER FROM THE FRONT END 28SEPT2017 STARTS HERE
    //public void loaddocument()
    //{
    //    try
    //    {
    //        creative.Common clsCommon = new creative.Common();
    //        DataTable dthome = classcategory.getallcatsfordoc();
    //        if (dthome.Rows.Count > 0)
    //        {
    //            rptcatdoc.DataSource = dthome;
    //            rptcatdoc.DataBind();
    //            for (int j = 0; j < rptcatdoc.Items.Count; j++)
    //            {
    //                Label lbldocid = (Label)rptcatdoc.Items[j].FindControl("lbldocid");
    //                LinkButton lnkb = (LinkButton)rptcatdoc.Items[j].FindControl("lnkview");
    //                string strid = lbldocid.Text;
    //                if (strid != null || strid != "")
    //                {
    //                    Repeater rptdoc = (Repeater)rptcatdoc.Items[j].FindControl("rptdoc");
    //                    Panel pandoc = (Panel)rptcatdoc.Items[j].FindControl("pandoc");
    //                    //DataTable doc = classcategory.getproductidbycatidsdisplayhome(strid);
    //                    //DataTable grp = classproductimage.latestdoc(Convert.ToInt32(strid));
    //                    DataSet grp = new DataSet();
    //                    string xmlfilename;
    //                    xmlfilename = GetSafeFileName(dthome.Rows[j]["categoryname"].ToString());

    //                    string xmlfileuser = Request.PhysicalApplicationPath + "\\xml\\" + xmlfilename + ".xml";
    //                    string xmlfileadmin = ReturnUrl("adminsitepath") + "xml/" + xmlfilename + ".xml";
    //                    string localPath = new Uri(xmlfileadmin).LocalPath;

    //                    if (File.Exists(xmlfileuser))
    //                    {
    //                        DateTime udt = Convert.ToDateTime(File.GetLastWriteTime(xmlfileuser).ToString());
    //                        DateTime adt = Convert.ToDateTime(File.GetLastWriteTime(Server.MapPath(localPath)).ToString());
    //                        if (udt > adt)
    //                        {
    //                            grp.ReadXml(ReturnUrl("sitepathmain") + "xml/" + xmlfilename + ".xml");
    //                            if (grp.Tables.Count > 0)
    //                            {
    //                                if (grp.Tables[0].Rows.Count > 0)
    //                                {
    //                                    pandoc.Visible = true;
    //                                    rptdoc.DataSource = grp;
    //                                    rptdoc.DataBind();
    //                                    int cnt = grp.Tables[0].Rows.Count;
    //                                    if (cnt >= 6)
    //                                    {
    //                                        lnkb.Visible = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        lnkb.Visible = true;
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    pandoc.Visible = false;
    //                                }
    //                            }
    //                            else
    //                            {
    //                                pandoc.Visible = false;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            grp.ReadXml(ReturnUrl("adminsitepath") + "xml/" + xmlfilename + ".xml");
    //                            if (grp.Tables.Count > 0)
    //                            {
    //                                if (grp.Tables[0].Rows.Count > 0)
    //                                {
    //                                    pandoc.Visible = true;
    //                                    rptdoc.DataSource = grp;
    //                                    rptdoc.DataBind();
    //                                    int cnt = grp.Tables[0].Rows.Count;
    //                                    if (cnt >= 6)
    //                                    {
    //                                        lnkb.Visible = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        lnkb.Visible = true;
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    pandoc.Visible = false;
    //                                }
    //                            }
    //                            else
    //                            {
    //                                pandoc.Visible = false;
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        grp.ReadXml(ReturnUrl("adminsitepath") + "xml/" + xmlfilename + ".xml");
    //                        if (grp.Tables.Count > 0)
    //                        {
    //                            if (grp.Tables[0].Rows.Count > 0)
    //                            {
    //                                pandoc.Visible = true;
    //                                rptdoc.DataSource = grp;
    //                                rptdoc.DataBind();
    //                                int cnt = grp.Tables[0].Rows.Count;
    //                                if (cnt >= 6)
    //                                {
    //                                    lnkb.Visible = true;
    //                                }
    //                                else
    //                                {
    //                                    lnkb.Visible = true;
    //                                }
    //                            }
    //                            else
    //                            {
    //                                pandoc.Visible = false;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            pandoc.Visible = false;
    //                        }
    //                    }

    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLog.WriteError(ex.ToString());
    //    }
    //}
    ////SAGAR COMMENTED THIS FOR REMOVING NEWSLETTER FROM THE FRONT END 28SEPT2017 ENDS HERE

    //JAYESH COMMENTED BELOW CODE TO HIDE ARTICLE FUNCTIONALITY FORM HOME PAGE 28sep2017
    public void loadads()
    {
        try
        {
            creative.Common clsCommon = new creative.Common();
            DataTable dthome = classcategory.getallcatsforads();
            //Response.Write("test ads");
            if (dthome.Rows.Count > 0)
            {

                //Response.Write("test ads");
                
                rptcatads.DataSource = dthome;
                rptcatads.DataBind();
                //Response.Write("test ads");
                for (int j = 0; j < rptcatads.Items.Count; j++)
                {
                   
                    //Response.Write("test ads");
                    Label lbladsid = (Label)rptcatads.Items[j].FindControl("lbladsid");                 
                    LinkButton lnkb = (LinkButton)rptcatads.Items[j].FindControl("lnkview");

                    string strid = lbladsid.Text;
                    //Response.Write("test ads");
                    if (strid != null || strid != "")
                    {
                        Repeater rptads = (Repeater)rptcatads.Items[j].FindControl("rptads");
                        Panel panads = (Panel)rptcatads.Items[j].FindControl("panads");
                        //Response.Write("test ads");
                        //DataTable grp = classproductimage.latestads(Convert.ToInt32(strid));


                        DataSet grp = new DataSet();
                        //Response.Write("test ads");
                        string xmlfilename;
                        xmlfilename = GetSafeFileName(dthome.Rows[j]["categoryname"].ToString());
                        string xmlfileuser = Request.PhysicalApplicationPath + "\\xml\\" + xmlfilename + ".xml";
                        string xmlfileadmin = ReturnUrl("adminsitepath") + "xml/" + xmlfilename + ".xml";
                        string localPath = new Uri(xmlfileadmin).LocalPath;

                        if (File.Exists(xmlfileuser))
                        {
                           
                            DateTime udt = Convert.ToDateTime(File.GetLastWriteTime(xmlfileuser).ToString());
                            DateTime adt = Convert.ToDateTime(File.GetLastWriteTime(Server.MapPath(localPath)).ToString());
                            if (udt > adt)
                            {
                               
                                grp.ReadXml(ReturnUrl("sitepathmain") + "xml/" + xmlfilename + ".xml");
                                if (grp.Tables.Count > 0)
                                {
                                    if (grp.Tables[0].Rows.Count > 0)
                                    {
                                 
                                        int cnt = grp.Tables[0].Rows.Count;
                                       
                                        if (cnt >= 3)
                                        {
                                            cnt = 3;
                                            lnkb.Visible = true;
                                        }
                                        else
                                        {
                                            lnkb.Visible = true;
                                        }
                                        panads.Visible = true;
                                        //SAGAR CHANGED Take(4) TO Take(3) PROPERTY FROM BELOW LINE FOR DISPAYING ONLY 3 RECORDS IN NEWS SECTION  16OCT2017
                                        rptads.DataSource = grp.Tables[0].AsEnumerable().Take(3).CopyToDataTable();
                                        rptads.DataBind();
                                        for (int i = 0; i < cnt; i++)
                                        {
                                          
                                           
                                            Image img = (Image)rptads.Items[i].FindControl("imgads");
                                            LinkButton lnkuser = (LinkButton)rptads.Items[i].FindControl("lnkuser");
                                            Label lbldocuser = (Label)rptads.Items[i].FindControl("lbldocuser");
                                            DataTable dtuser = classaddress.Getuserbyemail(lbldocuser.Text.ToString().Trim());
                                            if (dtuser.Rows.Count > 0)
                                            {
                                               
                                                lnkuser.PostBackUrl = ReturnUrl("sitepathmain").ToString() + "user/" + dtuser.Rows[0]["indexid"].ToString().Trim();
                                              
                                            }
                                            string fileName = Request.PhysicalApplicationPath + "\\images\\180x120\\" + grp.Tables[0].Rows[i]["bigimage"].ToString();
                                            if (File.Exists(fileName))
                                            {
                                                img.ImageUrl = ReturnUrl("sitepathmain") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
                                            }
                                            else
                                            {
                                                img.ImageUrl = ReturnUrl("sitepathadmin") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        panads.Visible = false;
                                    }
                                }
                                else
                                {
                                    panads.Visible = false;
                                }
                            }
                            else
                            {
                                grp.ReadXml(ReturnUrl("adminsitepath") + "xml/" + xmlfilename + ".xml");
                                if (grp.Tables.Count > 0)
                                {
                                    if (grp.Tables[0].Rows.Count > 0)
                                    {
                                        int cnt = grp.Tables[0].Rows.Count;
                                        if (cnt >= 3)
                                        {
                                            cnt = 3;
                                            lnkb.Visible = true;
                                        }
                                        else
                                        {
                                            lnkb.Visible = true;
                                        }
                                        panads.Visible = true;
                                        //SAGAR CHNGED Take(4) TO Take(3) PROPERTY FROM BELOW LINE FOR DISPAYING ONLY 3 RECORDS IN NEWS SECTION 16OCT2017
                                        rptads.DataSource = grp.Tables[0].AsEnumerable().Take(3).CopyToDataTable();
                                        rptads.DataBind();
                                        for (int i = 0; i < cnt; i++)
                                        {
                                            Image img = (Image)rptads.Items[i].FindControl("imgads");
                                            LinkButton lnkuser = (LinkButton)rptads.Items[i].FindControl("lnkuser");
                                            Label lbldocuser = (Label)rptads.Items[i].FindControl("lbldocuser");
                                            DataTable dtuser = classaddress.Getuserbyemail(lbldocuser.Text.ToString().Trim());
                                            if (dtuser.Rows.Count > 0)
                                            {
                                                lnkuser.PostBackUrl = ReturnUrl("sitepathmain").ToString() + "user/" + dtuser.Rows[0]["indexid"].ToString().Trim();
                                            }
                                            string fileName = Request.PhysicalApplicationPath + "\\images\\180x120\\" + grp.Tables[0].Rows[i]["bigimage"].ToString();
                                            if (File.Exists(fileName))
                                            {
                                                img.ImageUrl = ReturnUrl("sitepathmain") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
                                            }
                                            else
                                            {
                                                img.ImageUrl = ReturnUrl("sitepathadmin") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
                                            }
                                           
                                        }
                                    }
                                    else
                                    {
                                        panads.Visible = false;
                                    }
                                }
                                else
                                {
                                    panads.Visible = false;
                                }
                            }
                        }
                        else
                        {
                            grp.ReadXml(ReturnUrl("adminsitepath") + "xml/" + xmlfilename + ".xml");
                            if (grp.Tables.Count > 0)
                            {
                                if (grp.Tables[0].Rows.Count > 0)
                                {
                                    int cnt = grp.Tables[0].Rows.Count;
                                    if (cnt >= 3)
                                    {
                                        cnt = 3;
                                        lnkb.Visible = true;
                                    }
                                    else
                                    {
                                        lnkb.Visible = true;
                                    }
                                    panads.Visible = true;
                                    //SAGAR CHNGED Take(4) TO Take(3) PROPERTY FROM BELOW LINE FOR DISPAYING ONLY 3 RECORDS IN NEWS SECTION 16OCT2017
                                    rptads.DataSource = grp.Tables[0].AsEnumerable().Take(3).CopyToDataTable();
                                    rptads.DataBind();
                                    for (int i = 0; i < cnt; i++)
                                    {
                                        Image img = (Image)rptads.Items[i].FindControl("imgads");
                                        string fileName = Request.PhysicalApplicationPath + "\\images\\180x120\\" + grp.Tables[0].Rows[i]["bigimage"].ToString();
                                        if (File.Exists(fileName))
                                        {
                                            img.ImageUrl = ReturnUrl("sitepathmain") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
                                        }
                                        else
                                        {
                                            img.ImageUrl = ReturnUrl("sitepathadmin") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
                                        }
                                    }
                                }
                                else
                                {
                                    panads.Visible = false;
                                }
                            }
                            else
                            {
                                panads.Visible = false;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    //JAYESH COMMENTED ABOVE CODE TO HIDE ARTICLE FUNCTIONALITY FORM HOME PAGE 28sep2017



    // SAGAR COMMENTED THIS FOR REMOVING PROJECTS FROM THE FRONT END 28SEPT2017 STARTS HERE
    //public void loaduniversal()
    //{
    //    try
    //    {
    //        creative.Common clsCommon = new creative.Common();
    //        DataTable dthome = classcategory.getAllCategoryByCattype("U");
    //        if (dthome.Rows.Count > 0)
    //        {
    //            rptcatuniversal.DataSource = dthome;
    //            rptcatuniversal.DataBind();
    //            for (int j = 0; j < rptcatuniversal.Items.Count; j++)
    //            {
    //                Label lblcid = (Label)rptcatuniversal.Items[j].FindControl("lblcatid");
    //                LinkButton lnkb = (LinkButton)rptcatuniversal.Items[j].FindControl("lnkview");
    //                string strid = lblcid.Text;
    //                if (strid != null || strid != "")
    //                {
    //                    Repeater rptubniversal = (Repeater)rptcatuniversal.Items[j].FindControl("rptuniversal");
    //                    Panel pnluniversal = (Panel)rptcatuniversal.Items[j].FindControl("pnluniversal");
    //                    // DataTable doc = classcategory.getproductidbycatidsdisplayhome(strid);
    //                    //DataTable grp = classproductimage.latestdoc(Convert.ToInt32(strid));
    //                    DataSet dsuniversal = new DataSet();
    //                    string xmlfilename;
    //                    xmlfilename = GetSafeFileName(dthome.Rows[j]["categoryname"].ToString());

    //                    string xmlfileuser = Request.PhysicalApplicationPath + "\\xml\\" + xmlfilename + ".xml";
    //                    string xmlfileadmin = ReturnUrl("adminsitepath")+ "xml/" + xmlfilename + ".xml";
    //                    string localPath = new Uri(xmlfileadmin).LocalPath;

    //                    if (File.Exists(xmlfileuser))
    //                    {
    //                        DateTime udt = Convert.ToDateTime(File.GetLastWriteTime(xmlfileuser).ToString());
    //                        DateTime adt = Convert.ToDateTime(File.GetLastWriteTime(Server.MapPath(localPath)).ToString());
    //                        if (udt > adt)
    //                        {
    //                            dsuniversal.ReadXml(ReturnUrl("sitepathmain") + "xml/" + xmlfilename + ".xml");
    //                            if (dsuniversal.Tables.Count > 0)
    //                            {
    //                                if (dsuniversal.Tables[0].Rows.Count > 0)
    //                                {
    //                                    pnluniversal.Visible = true;
    //                                    rptubniversal.DataSource = dsuniversal;
    //                                    rptubniversal.DataBind();
    //                                    int cnt = dsuniversal.Tables[0].Rows.Count;
    //                                    if (cnt >= 6)
    //                                    {
    //                                        lnkb.Visible = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        lnkb.Visible = true;
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    pnluniversal.Visible = false;
    //                                }
    //                            }
    //                            else
    //                            {
    //                                pnluniversal.Visible = false;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            dsuniversal.ReadXml(ReturnUrl("adminsitepath") + "xml/" + xmlfilename + ".xml");
    //                            if (dsuniversal.Tables.Count > 0)
    //                            {
    //                                if (dsuniversal.Tables[0].Rows.Count > 0)
    //                                {
    //                                    pnluniversal.Visible = true;
    //                                    rptubniversal.DataSource = dsuniversal;
    //                                    rptubniversal.DataBind();
    //                                    int cnt = dsuniversal.Tables[0].Rows.Count;
    //                                    if (cnt >= 6)
    //                                    {
    //                                        lnkb.Visible = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        lnkb.Visible = true;
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    pnluniversal.Visible = false;
    //                                }
    //                            }
    //                            else
    //                            {
    //                                pnluniversal.Visible = false;
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        dsuniversal.ReadXml(ReturnUrl("adminsitepath") + "xml/" + xmlfilename + ".xml");
    //                        if (dsuniversal.Tables.Count > 0)
    //                        {
    //                            if (dsuniversal.Tables[0].Rows.Count > 0)
    //                            {
    //                                pnluniversal.Visible = true;
    //                                rptubniversal.DataSource = dsuniversal;
    //                                rptubniversal.DataBind();
    //                                int cnt = dsuniversal.Tables[0].Rows.Count;
    //                                if (cnt >= 6)
    //                                {
    //                                    lnkb.Visible = true;
    //                                }
    //                                else
    //                                {
    //                                    lnkb.Visible = true;
    //                                }
    //                            }
    //                            else
    //                            {
    //                                pnluniversal.Visible = false;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            pnluniversal.Visible = false;
    //                        }
    //                    }

    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLog.WriteError(ex.ToString());
    //    }
    //}
    //SAGAR COMMENTED THIS FOR REMOVING PROJECT FROM THE FRONT END 28SEPT2017 ENDS HERE

    //SAGAR COMMENTED THIS FOR REMOVING VIDEO FROM THE FRONT END 28SEPT2017 STARTS HERE
    //public void loadvideo()
    //{
    //    try
    //    {
    //        creative.Common clsCommon = new creative.Common();
    //        DataTable dthome = classcategory.getallcatsforvideo();
    //        if(dthome.Rows.Count > 0)
    //        {

    //            rptcatvideo.DataSource = dthome;
    //            rptcatvideo.DataBind();
    //            for (int j = 0; j < rptcatvideo.Items.Count; j++)
    //            {
    //                Label lblvideoid = (Label)rptcatvideo.Items[j].FindControl("lblvideoid");
    //                LinkButton lnkb = (LinkButton)rptcatvideo.Items[j].FindControl("lnkview");
    //                string strid = lblvideoid.Text;
    //                if (strid != null || strid != "")
    //                {
    //                    Repeater rptvideo = (Repeater)rptcatvideo.Items[j].FindControl("rptvideo");
    //                    Panel panvideo = (Panel)rptcatvideo.Items[j].FindControl("pnlvideo");
    //                    //DataTable grp = classproductimage.latestvideo(Convert.ToInt32(strid));
    //                    DataSet grp = new DataSet();
    //                    string xmlfilename;
    //                    xmlfilename = GetSafeFileName(dthome.Rows[j]["categoryname"].ToString());

    //                    string xmlfileuser = Request.PhysicalApplicationPath + "\\xml\\" + xmlfilename + ".xml";
    //                    string xmlfileadmin = ReturnUrl("adminsitepath")+ "xml/" + xmlfilename + ".xml";
    //                    string localPath = new Uri(xmlfileadmin).LocalPath;

    //                    if (File.Exists(xmlfileuser))
    //                    {
    //                        DateTime udt = Convert.ToDateTime(File.GetLastWriteTime(xmlfileuser).ToString());
    //                        DateTime adt = Convert.ToDateTime(File.GetLastWriteTime(Server.MapPath(localPath)).ToString());
    //                        if (udt > adt)
    //                        {
    //                            grp.ReadXml(ReturnUrl("sitepathmain") + "xml/" + xmlfilename + ".xml");
    //                            if (grp.Tables.Count > 0)
    //                            {
    //                                if(grp.Tables[0].Rows.Count > 0)
    //                                {
    //                                    panvideo.Visible = true;
    //                                    rptvideo.DataSource = grp;
    //                                    rptvideo.DataBind();
    //                                    int cnt = rptvideo.Items.Count;
    //                                    if (cnt >= 6)
    //                                    {
    //                                        lnkb.Visible = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        lnkb.Visible = true;
    //                                    }
    //                                    for (int i = 0; i < cnt; i++)
    //                                    {
    //                                        Image img = (Image)rptvideo.Items[i].FindControl("videoimg");
    //                                        string fileName = Request.PhysicalApplicationPath + "\\images\\180x120\\" + grp.Tables[0].Rows[i]["bigimage"].ToString();
    //                                        if (File.Exists(fileName))
    //                                        {
    //                                            img.ImageUrl = ReturnUrl("sitepathmain") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
    //                                        }
    //                                        else
    //                                        {
    //                                            img.ImageUrl = ReturnUrl("sitepathadmin") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
    //                                        }
    //                                        img.Attributes.Add("class", "gallary-image");
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    panvideo.Visible = false;
    //                                }
    //                            }
    //                            else
    //                            {
    //                                panvideo.Visible = false;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            grp.ReadXml(ReturnUrl("adminsitepath") + "xml/" + xmlfilename + ".xml");
    //                            if (grp.Tables.Count > 0)
    //                            {
    //                                if(grp.Tables[0].Rows.Count > 0)
    //                                {
    //                                    panvideo.Visible = true;
    //                                    rptvideo.DataSource = grp;
    //                                    rptvideo.DataBind();
    //                                    int cnt = rptvideo.Items.Count;
    //                                    if (cnt >= 6)
    //                                    {
    //                                        lnkb.Visible = true;
    //                                    }
    //                                    else
    //                                    {
    //                                        lnkb.Visible = true;
    //                                    }
    //                                    for (int i = 0; i < cnt; i++)
    //                                    {
    //                                        Image img = (Image)rptvideo.Items[i].FindControl("videoimg");
    //                                        string fileName = Request.PhysicalApplicationPath + "\\images\\180x120\\" + grp.Tables[0].Rows[i]["bigimage"].ToString();
    //                                        if (File.Exists(fileName))
    //                                        {
    //                                            img.ImageUrl = ReturnUrl("sitepathmain") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
    //                                        }
    //                                        else
    //                                        {
    //                                            img.ImageUrl = ReturnUrl("sitepathadmin") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
    //                                        }
    //                                        img.Attributes.Add("class", "gallary-image");
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    panvideo.Visible = false;
    //                                }
    //                            }
    //                            else
    //                            {
    //                                panvideo.Visible = false;
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        grp.ReadXml(ReturnUrl("adminsitepath") + "xml/" + xmlfilename + ".xml");
    //                        if (grp.Tables.Count > 0)
    //                        {
    //                            if(grp.Tables[0].Rows.Count > 0)
    //                            {
    //                                panvideo.Visible = true;
    //                                rptvideo.DataSource = grp;
    //                                rptvideo.DataBind();
    //                                int cnt = rptvideo.Items.Count;
    //                                if (cnt >= 6)
    //                                {
    //                                    lnkb.Visible = true;
    //                                }
    //                                else
    //                                {
    //                                    lnkb.Visible = true;
    //                                }
    //                                for (int i = 0; i < cnt; i++)
    //                                {
    //                                    Image img = (Image)rptvideo.Items[i].FindControl("videoimg");
    //                                    string fileName = Request.PhysicalApplicationPath + "\\images\\180x120\\" + grp.Tables[0].Rows[i]["bigimage"].ToString();
    //                                    if (File.Exists(fileName))
    //                                    {
    //                                        img.ImageUrl = ReturnUrl("sitepathmain") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
    //                                    }
    //                                    else
    //                                    {
    //                                        img.ImageUrl = ReturnUrl("sitepathadmin") + "images/180x120/" + grp.Tables[0].Rows[i]["bigimage"].ToString();
    //                                    }
    //                                    img.Attributes.Add("class", "gallary-image");
    //                                }
    //                            }
    //                            else
    //                            {
    //                                panvideo.Visible = false;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            panvideo.Visible = false;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLog.WriteError(ex.ToString());
    //    }
    //}
    //SAGAR COMMENTED THIS FOR REMOVING VIDEO FROM THE FRONT END 28SEPT2017 ENDS HERE

    //SAGAR COMMENTED THIS FOR REMOVING FUNZONE FROM THE FRONT END 28SEPT2017 STARTS HERE
    //public void loadfunzone()
    //{
    //    try
    //    {
    //        //DataTable grp = classproductimage.latestfunzone();
    //        DataSet grp = new DataSet();
    //        string xmlfileuser = Request.PhysicalApplicationPath + "\\xml\\funzone.xml";
    //        string xmlfileadmin = ReturnUrl("adminsitepath")+ "xml/funzone.xml";
    //        string localPath = new Uri(xmlfileadmin).LocalPath;
    //        if (File.Exists(xmlfileuser))
    //        {
    //            DateTime udt = Convert.ToDateTime(File.GetLastWriteTime(xmlfileuser).ToString());
    //            DateTime adt = Convert.ToDateTime(File.GetLastWriteTime(Server.MapPath(localPath)).ToString());
    //            if (udt > adt)
    //            {
    //                grp.ReadXml(ReturnUrl("sitepathmain") + "xml/funzone.xml");
    //                if (grp.Tables.Count > 0)
    //                {
    //                    if(grp.Tables[0].Rows.Count > 0)
    //                    {
    //                        int cnt = grp.Tables[0].Rows.Count;
    //                        if (cnt >= 6)
    //                        {
    //                            lnkview.Visible = true;
    //                        }
    //                        else
    //                        {
    //                            lnkview.Visible = true;
    //                        }
    //                        panfun.Visible = true;
    //                        rptfun.DataSource = grp;
    //                        rptfun.DataBind();
    //                    }
    //                    else
    //                    {
    //                        lnkview.Visible = false;
    //                        panfun.Visible = false;
    //                    }
    //                }
    //                else
    //                {
    //                    lnkview.Visible = false;
    //                    panfun.Visible = false;
    //                }
    //            }
    //            else
    //            {
    //                grp.ReadXml(ReturnUrl("adminsitepath") + "xml/funzone.xml");
    //                if (grp.Tables.Count > 0)
    //                {
    //                    if(grp.Tables[0].Rows.Count > 0)
    //                    {
    //                        int cnt = grp.Tables[0].Rows.Count;
    //                        if (cnt >= 6)
    //                        {
    //                            lnkview.Visible = true;
    //                        }
    //                        else
    //                        {
    //                            lnkview.Visible = true;
    //                        }
    //                        panfun.Visible = true;
    //                        rptfun.DataSource = grp;
    //                        rptfun.DataBind();
    //                    }
    //                    else
    //                    {
    //                        lnkview.Visible = false;
    //                        panfun.Visible = false;
    //                    }
    //                }
    //                else
    //                {
    //                    lnkview.Visible = false;
    //                    panfun.Visible = false;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            grp.ReadXml(ReturnUrl("adminsitepath") + "xml/funzone.xml");
    //            if (grp.Tables.Count > 0)
    //            {
    //                if(grp.Tables[0].Rows.Count > 0)
    //                {
    //                    int cnt = grp.Tables[0].Rows.Count;
    //                    if (cnt >= 6)
    //                    {
    //                        lnkview.Visible = true;
    //                    }
    //                    else
    //                    {
    //                        lnkview.Visible = true;
    //                    }
    //                    panfun.Visible = true;
    //                    rptfun.DataSource = grp;
    //                    rptfun.DataBind();
    //                }
    //                else
    //                {
    //                    lnkview.Visible = false;
    //                    panfun.Visible = false;
    //                }
    //            }
    //            else
    //            {
    //                lnkview.Visible = false;
    //                panfun.Visible = false;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLog.WriteError(ex.ToString());
    //    }
    //}
    //SAGAR COMMENTED THIS FOR REMOVING FUNZONE FROM THE FRONT END 28SEPT2017 ENDS HERE

    //public void loadsurvey()
    //{
    //    try
    //    {
    //        DataTable grp = classcategory.getallsurveylist();
    //        if (grp.Rows.Count > 0)
    //        {
    //            pnlsurvey.Visible = true;
    //            rptsurvey.DataSource = grp;
    //            rptsurvey.DataBind();
    //            if(Page.User.Identity.IsAuthenticated)
    //            {
    //                if (rptsurvey.Items.Count > 0)
    //                {
    //                    for (int i = 0; i < rptsurvey.Items.Count; i++)
    //                    {
    //                        user = Page.User.Identity.Name;
    //                        Label lblpid = (Label)rptsurvey.Items[i].FindControl("lblpid");
    //                        surveyid = Convert.ToInt32(lblpid.Text);
    //                        LinkButton lnksurvey = (LinkButton)rptsurvey.Items[i].FindControl("lnksurvey");
    //                        LinkButton lnksurveyupl = (LinkButton)rptsurvey.Items[i].FindControl("lnksurveyupl");
    //                        DataTable dwn = classnews.getdownloaduser(surveyid, user);
    //                        if (dwn.Rows.Count > 0)
    //                        {
    //                            lnksurveyupl.Visible = true;
    //                        }
    //                        else
    //                        {
    //                            lnksurvey.Visible = true;
    //                        }
    //                    }
    //                }
    //            }

    //        }
    //        else
    //        {
    //            pnlsurvey.Visible = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLog.WriteError(ex.ToString());
    //    }
    //}

    public string productUrlrewriting(object productname, object productid)
    {
        string strurl = "";
        try
        {

            strurl = UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(productid.ToString().Trim()), "PDID");
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }

    public string galleryUrlrewriting(object productid)
    {
        string strurl = "";
        try
        {
            //sony commented this to have the detailed description page link to be a normal post (PD) style display rather than a photogallery detailed type (PG) page display
           //strurl = UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(productid.ToString().Trim()), "PG");
            strurl = UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(productid.ToString().Trim()), "PDID");
            //strurl = UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(productid.ToString().Trim()), "PD");
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }

    //public string getuser(object userid)
    //{
    //    string strurl = "";
    //    try
    //    {
    //        strurl = ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(userid.ToString());
    //        return strurl;
    //    }
    //    catch (Exception ex)
    //    {
    //        return strurl;
    //    }
    //}

    public string getsubstr(object shortdesc)
    {
        string str1 = "";
        string str2 = "";
        try {
            str1 = Convert.ToString(shortdesc);
            str2 = Regex.Replace(str1, "<.*?>", String.Empty);
            str2 = str2.Substring(0, 100);
            str2 = str2 + "....";
            return str2;
        }
        catch(Exception ex)
        {
            return str1;
        }
    }

    public string getfullname(object uname)
    {
        string str1 = "";
        try
        {
            DataTable dt1 = classaddress.GetuserFullname(uname.ToString().Trim());
            if(dt1.Rows.Count > 0)
            {
                str1 = dt1.Rows[0]["fullname"].ToString().Trim();
            }
            return str1;
        }
        catch (Exception ex)
        {
            return str1;
        }
    }
    public bool getdocvisible(object uname)
    {
        try
        {
            if(uname.ToString().Trim().ToLower()=="Highbar first")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            return true;
        }
    }
    public string getdocext(object ext)
    {
        string file = "";
        string ext1 = "";
        string rtnval = "";
        try
        {
            file = ext.ToString();
            if (file != "")
            {
                int z = 0;
                z = file.LastIndexOf('.');
                int x = 0;
                x = file.Length;
                ext1 = file.Substring(z, x - z).ToLower();
                if (ext1 == ".pdf")
                {

                    rtnval = "<img class='multidocext' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/pdf1hover.png" + "' />";
                }
                else if (ext1 == ".doc")
                {
                    rtnval = "<img class='multidocext' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/doc1hover.png" + "' />";
                }
                else if (ext1 == ".docx")
                {
                    rtnval = "<img class='multidocext' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/doc1hover.png" + "' />";
                }
                else if (ext1 == ".xls")
                {
                    rtnval = "<img class='multidocext' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/xls1hover.png" + "' />";
                }
                else if (ext1 == ".xlsx")
                {
                    rtnval = "<img class='multidocext' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/xls1hover.png" + "' />";
                }
                else if (ext1 == ".ppt")
                {
                    rtnval = "<img class='multidocext' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/ppthover.png" + "' />";
                }
                else if (ext1 == ".pptx")
                {
                    rtnval = "<img class='multidocext' src='" + ReturnUrl("sitepathmain") + "themes/creative1.0/images/icons/ppthover.png" + "' />";
                }
            }
            return rtnval;
        }
        catch (Exception ex)
        {
            return rtnval;
        }
    }
    public string getpostvideo(object videocode, object videourl)
    {
        string strurl = "";
        try
        {
            if (videourl.ToString().Length > 0 || videourl.ToString() != "" || videocode.ToString().Length > 0 || videocode.ToString() != "")
            {
                if (videocode.ToString().Length > 0 && videocode.ToString() != "" && videocode.ToString() != "N/A" && videocode.ToString() != "n/a")
                {
                    strurl = videocode.ToString();
                }
                else
                {
                    if (videourl.ToString() != "N/A" && videourl.ToString() != "n/a")
                    {
                        strurl = "<video controls preload='metadata' class='videourl' data-setup='{}'><source src='" + videourl + "' type='video/mp4'></video>";
                    }
                    else
                    {
                        strurl = "";
                    }
                }
            }
            else
            {
                strurl = "";
            }
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
    protected void rptsurvey_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }

    protected void rptsurvey_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "lnkupload")
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "surveydetail.aspx?surveyid=" + UrlRewritingVM.Encrypt(e.CommandArgument.ToString()));
        }
        if(e.CommandName.ToLower() == "lnkdownload")
        {
            surveyid = Convert.ToInt32(e.CommandArgument.ToString());
            DataTable dt = classproduct.getsinglesurveyuser(surveyid);
            file = dt.Rows[0]["docfile"].ToString();
            downloadsurvey();
            Stream stream = null;
            int bytesToRead = 10000;

            // Buffer to read bytes in chunk size specified above
            byte[] buffer = new Byte[bytesToRead];

            // The number of bytes read
            try
            {
                //Create a WebRequest to get the file
                //HttpWebRequest fileReq = (HttpWebRequest)HttpWebRequest.Create("http://admin.csia.in.iis3004.shared-servers.com/" + "ExcelData/files/" + file);
                HttpWebRequest fileReq = (HttpWebRequest)HttpWebRequest.Create(ReturnUrl("sitepathadmin") + "ExcelData/files/" + file);
                //Create a response for this request
                HttpWebResponse fileResp = (HttpWebResponse)fileReq.GetResponse();

                if (fileReq.ContentLength > 0)
                    fileResp.ContentLength = fileReq.ContentLength;

                //Get the Stream returned from the response
                stream = fileResp.GetResponseStream();

                // prepare the response to the client. resp is the client Response
                var resp = HttpContext.Current.Response;

                //Indicate the type of data being sent
                resp.ContentType = getextension(file);

                //Name the file 
                resp.AddHeader("Content-Disposition", "attachment; filename=\"" + file + "\"");
                resp.AddHeader("Content-Length", fileResp.ContentLength.ToString());

                int length;
                do
                {
                    // Verify that the client is connected.
                    if (resp.IsClientConnected)
                    {
                        // Read data into the buffer.
                        length = stream.Read(buffer, 0, bytesToRead);

                        // and write it out to the response's output stream
                        resp.OutputStream.Write(buffer, 0, length);

                        // Flush the data
                        resp.Flush();

                        //Clear the buffer
                        buffer = new Byte[bytesToRead];
                    }
                    else
                    {
                        // cancel the download if client has disconnected
                        length = -1;
                    }
                } while (length > 0); //Repeat until no data is read


            }
            finally
            {
                if (stream != null)
                {
                    //Close the input stream
                    stream.Close();
                }
            }
        }
    }

    public void downloadsurvey()
    {
        if (Page.User.Identity.IsAuthenticated)
        {

            user = Page.User.Identity.Name;
            DataTable dt = classnews.getalluserbyemailid(user);
            fname = dt.Rows[0]["fullname"].ToString();
            DataTable dwn = classnews.getdownloaduser(surveyid, user);
            if (dwn.Rows.Count > 0)
            {
                classnews.updatedownloaduser(surveyid, user);
            }
            else
            {
                classnews.addsurveyuser(surveyid, user, fname);
            }
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
        }
    }

    public static string getextension(string filename)
    {
        int z = 0;
        z = filename.LastIndexOf('.');
        int x = 0;
        x = filename.Length;
        string ext = filename.Substring(z, x - z);
        string cnttyp = "";
        if (ext == ".doc" || ext == ".docx")
        {
            cnttyp = "Application/msword";
        }
        
        if (ext == ".xls" || ext == ".xlsx")
        {
            cnttyp = "vnd.openxmlformats-officedocument.spreadsheetml.sheet";
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
}