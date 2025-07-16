using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Services;
using System.Web.Security;
using System.IO;

public partial class manageservice : System.Web.UI.Page
{
    public static int PageSize = 1000, PageIndex = 1, RecordCount = 0;
    public static string username = "", flag = "";

    public static string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Url.AbsoluteUri.Contains("intranet.highbartech.com") || Request.Url.AbsoluteUri.Contains("localhost"))
        //if (Request.Url.AbsoluteUri.Contains("intranet.highbartech.com") || Request.Url.AbsoluteUri.Contains("HRMS_ADMIN"))
        //if (Request.Url.AbsoluteUri.Contains("192.168.0.172/HRMS_ADMIN/") || Request.Url.AbsoluteUri.Contains("192.168.0.172/hrms/"))
        {
            if (Request.Url.AbsoluteUri.Contains("UpdateSettings") || Request.Url.AbsoluteUri.Contains("InsertUpdateSettings") || Request.Url.AbsoluteUri.Contains("GetData") || Request.Url.AbsoluteUri.Contains("GetReminders") || Request.Url.AbsoluteUri.Contains("UpdateReminderStatus") || Request.Url.AbsoluteUri.Contains("UpdateCounter"))
            {
                if (!Page.User.Identity.IsAuthenticated)
                {
                    Response.Redirect(ReturnUrl("sitepathmain"));
                }
            }
            else
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
            }
        }
        else
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "errorpg.aspx");
        }
    }

    [WebMethod]
    public static string InsertUpdateSettings(string widget)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            username = HttpContext.Current.User.Identity.Name.ToString();
            flag = "F";
            Classuserwidget.insertupdatewidgetsettings(widget, username, flag);
        }
        return "success";
    }
    [WebMethod]
    public static string UpdateSettings(string id, string flag)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            username = HttpContext.Current.User.Identity.Name.ToString();
            Classuserwidget.updateUserwidget(Convert.ToInt32(id), username, flag);
        }
        return "success";
    }
    [WebMethod]
    public static string GetChatData(string content)
    {
        string data = content;
        StringReader theReader = new StringReader(data);
        DataSet theDataSet = new DataSet();
        theDataSet.ReadXml(theReader);
        theDataSet.Tables[1].Columns.Add("imageURL");
        if(theDataSet.Tables[1].Rows.Count>0)
        {
            for(int i=0;i<theDataSet.Tables[1].Rows.Count;i++)
            {
                int id = Convert.ToInt32(theDataSet.Tables[1].Rows[i]["userid"].ToString());
                DataTable dt = classaddress.getuserbyindexid(id);
                if(dt.Rows.Count>0)
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath("~/themes/creative1.0/images/profile55x55/" + dt.Rows[0]["profilephoto"].ToString())))
                    {
                        theDataSet.Tables[1].Rows[i]["imageURL"]= ReturnUrl("sitepathmain") + "images/profile110x110/" +  dt.Rows[0]["profilephoto"].ToString();
                    }
                    else
                    {
                        theDataSet.Tables[1].Rows[i]["imageURL"] = "https://graph.facebook.com/" + dt.Rows[0]["profilephoto"].ToString() + "/picture?type=large";
                    }
                }
            }
            return theDataSet.GetXml();
        }
        else
        {
            return "";
        }
        
    }
    [WebMethod]
    public static string GetData(string userid)
    {
        try
        {
            DataRow[] dtrow;
            int msgcount = 0;
            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            string data = "";
            string search = userid+"_";
           // string path = HttpContext.Current.Server.MapPath(@"~\xml\chat_data\");
            string path = "E:\\chatapp\\xml\\chat_data";
            FileInfo[] files = Directory.GetFiles(path, search, SearchOption.AllDirectories).Select(f => new FileInfo(f)).OrderBy(f => f.LastWriteTime).ToArray();
            if (files.Length > 0)
            {
                if (File.Exists(files[files.Length - 1].FullName.ToString()))
                {
                    ds.ReadXml(files[files.Length - 1].FullName.ToString());
                    dtrow = ds.Tables[0].Select("rflag='U' and to='" + userid + "'");
                    if (dtrow.Length > 0)
                    {
                        msgcount = msgcount+dtrow.Length;
                    }
                    else
                    {
                    }
                }
            }
            search = "_"+ userid;
            FileInfo[] files2 = Directory.GetFiles(path, search, SearchOption.AllDirectories).Select(f => new FileInfo(f)).OrderBy(f => f.LastWriteTime).ToArray();
            if (files2.Length > 0)
            {
                if (File.Exists(files2[files2.Length - 1].FullName.ToString()))
                {
                    ds.ReadXml(files2[files2.Length - 1].FullName.ToString());
                    dtrow = ds.Tables[0].Select("rflag='U' and to='" + userid + "'");
                    if (dtrow.Length > 0)
                    {
                        msgcount = msgcount + dtrow.Length;
                    }
                    else
                    {
                    }
                }
            }
            DataTable dtnew = new DataTable();
            dtnew.Columns.Add("msgcount", typeof(System.String));
            var row = dtnew.NewRow();
            row["msgcount"] = msgcount;
            dtnew.Rows.Add(row);
            ds2.Tables.Add(dtnew);
            return ds2.GetXml();
        }
        catch (Exception ex)
        {
            return "";
        }
    }
    [WebMethod]
    public static string GetReminders(string rtime)
    {
        try
        {
            DateTime time1 = Convert.ToDateTime(rtime);
            DataTable dt1, dt2;
            DataSet ds = new DataSet();
            DataRow[] dtrow,dtrow2;
            username=HttpContext.Current.User.Identity.Name.ToString();
            string currentTime=DateTime.Now.ToShortDateString();
            DataTable dt = classreminder.displayReminder_Bydate(username);
            if(dt.Rows.Count>0)
            {
                dtrow = dt.Select("isallday='N'");
                dt1=dtrow.CopyToDataTable();
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        int cmp=0;
                        string rstatus = dt1.Rows[i]["reminderstatus"].ToString();
                        if(rstatus!=""&&rstatus!=null)
                        {
                            DateTime rstatus1 = Convert.ToDateTime(Convert.ToDateTime(rstatus).ToShortDateString());
                            DateTime rstatus2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                            cmp = DateTime.Compare(rstatus1,rstatus2);
                            if(cmp<0)
                            {
                                cmp = 1;
                            }
                        }
                        else
                        {
                            cmp = 1;
                        }
                        string rtime2 = dt1.Rows[i]["reminder_time"].ToString();
                        DateTime time2 = Convert.ToDateTime(rtime2);
                        int result = DateTime.Compare(time1, time2);
                        if(cmp==1)
                        {
                            if (result > 0 || result == 0)
                            {

                            }
                            else
                            {
                                dt1.Rows[i].Delete();
                            }
                        }
                        else
                        {
                            dt1.Rows[i].Delete();
                        }
                        
                    }
                }
                dtrow2 = dt.Select("isallday='Y'");
                if (dtrow2.Length > 0)
                {
                    for (int i = 0; i < dtrow.Length; i++)
                    {

                    }
                }
                ds.Tables.Add(dt1);
                return ds.GetXml();
            }
            else
            {
                return "";
            }
            
        }
        catch (Exception ex)
        {
            return "";
        }
    }
    [WebMethod]
    public static string UpdateReminderStatus(string id)
    {
        int i = 0;
        try
        {
            string rmid = id;
            i=Convert.ToInt32(classreminder.Update_ReminderStatus(id));
            return i.ToString();
        }
        catch (Exception ex)
        {
            return i.ToString();
        }
    }
    [WebMethod]
    public static string UpdateCounter(string id)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dtcounter = new DataTable();
            dtcounter = classproduct.getproductlikecountcomment(id);
            if(dtcounter.Rows.Count>0)
            {
                ds.Tables.Add(dtcounter);
                return ds.GetXml();
            }
            else
            {
                return "";
            }
        }
        catch(Exception ex)
        {
            return "";
        }
    }
}