using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class themes_creative1_LayoutControls_birthday : System.Web.UI.UserControl
{
    string user = null;
    string fname = null;
    int userid = 0;

     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if(Page.User.Identity.IsAuthenticated)
            {
                user = Page.User.Identity.Name.ToString().Trim();
                loaddata();
                if (Request.QueryString["userid"] != null)
                {
                    userid = Convert.ToInt32(UrlRewritingVM.Decrypt(Request.QueryString["userid"].ToString()));
                }
            }
        }
    }

    public void loaddata()
    {
        //sony commented this entire segment to avoid error temporarily
        try
        {
            //DataTable dt = Classuserwidget.getwidgetAdminStatus(1);
            //if (dt.Rows.Count > 0)
            //{
            //    DataTable dtuser = Classuserwidget.getwidgetUserStatus(user, 1);
            //    if (dtuser.Rows.Count > 0)
            //    {
                    DataSet dtbday = new DataSet();
                   
                    string xmlfileuser = Request.PhysicalApplicationPath + "\\xml\\birthday.xml";
                    string xmlfileadmin = ConfigurationManager.AppSettings["adminsitepath"]+ "xml/birthday.xml";
                    string localPath = new Uri(xmlfileadmin).LocalPath;

                    if (File.Exists(xmlfileuser))
                    {
                        DateTime udt = Convert.ToDateTime(File.GetLastWriteTime(xmlfileuser).ToString());

                        DateTime adt = Convert.ToDateTime(File.GetLastWriteTime(Server.MapPath(localPath)).ToString());

                        if (udt > adt)
                        {
                            dtbday.ReadXml(ReturnUrl("sitepathmain") + "xml/birthday.xml");
                            if (dtbday.Tables.Count > 0)
                            {
                                bday1.Visible = true;
                                lblEmptyRepeater.Visible = false;

                                //pnlnews.Visible = true;
                                rptbday.DataSource = dtbday;
                                rptbday.DataBind();
                                
                            }
                            else
                            {
                                //bday1.Visible = false;
                                bday1.Visible = true;
                            }
                        }
                        else
                        {
                            dtbday.ReadXml(ConfigurationManager.AppSettings["adminsitepath"]+ "xml/birthday.xml");
                            if (dtbday.Tables.Count > 0)
                            {
                                bday1.Visible = true;
                                lblEmptyRepeater.Visible = false;
                                //pnlnews.Visible = true;
                                rptbday.DataSource = dtbday;
                                rptbday.DataBind();
                            }
                            else
                            {
                               // bday1.Visible = false;
                                bday1.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        dtbday.ReadXml(ConfigurationManager.AppSettings["adminsitepath"]+ "xml/birthday.xml");
                        if (dtbday.Tables.Count > 0)
                        {
                            bday1.Visible = true;
                            lblEmptyRepeater.Visible = false;
                            //pnlnews.Visible = true;
                            rptbday.DataSource = dtbday;
                            rptbday.DataBind();
                        }
                        else
                        {
                           // bday1.Visible = false;
                            bday1.Visible = true;
                        }
                    }
        //        }
        //        else
        //        {
        //            //bday1.Visible = false;
        //            bday1.Visible = true;
        //        }
        //    }
        //    else
        //    {
        //       // bday1.Visible = false;
        //        bday1.Visible = true;
        //    }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    public string getuser(object userid)
    {
        string strurl = "";
        try
        {
            strurl = ReturnUrl("sitepathmain") + "composemessage/" + UrlRewritingVM.Encrypt(userid.ToString());
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
}


//DateTime birthday = Convert.ToDateTime("1991/12/24");
//        int years = DateTime.Now.Year - birthday.Year;
//        birthday = birthday.AddYears(years);
//        DateTime check = DateTime.Now.AddDays(7);
//        if ((birthday > DateTime.Now) && (birthday < check))
//        {
//            lbbirthday.Text = ("This week is your birthday !!!");
//        }
//        else
//        {
//            lbbirthday.Text = ("This week no birthday");       
//        }

  //if (File.Exists(xmlfileuser))
  //                  {
  //                      DateTime udt = Convert.ToDateTime(File.GetLastWriteTime(xmlfileuser).ToString());
  //                      DateTime adt = Convert.ToDateTime(File.GetLastWriteTime(Server.MapPath(localPath)).ToString());
  //                      if (udt > adt)
  //                      {
  //                          dtbday.ReadXml(ReturnUrl("sitepathmain") + "xml/birthday.xml");
  //                          if (dtbday.Tables.Count > 0)
  //                          {
  //                              bday1.Visible = true;
  //                              //pnlnews.Visible = true;
  //                              rptbday.DataSource = dtbday;
  //                              rptbday.DataBind();
                                
  //                          }
  //                          else
  //                          {
  //                              bday1.Visible = false;
  //                          }
  //                      }
  //                      else
  //                      {
  //                          dtbday.ReadXml(ConfigurationManager.AppSettings["adminsitepath"]+ "xml/birthday.xml");
  //                          if (dtbday.Tables.Count > 0)
  //                          {
  //                              bday1.Visible = true;
  //                              //pnlnews.Visible = true;
  //                              rptbday.DataSource = dtbday;
  //                              rptbday.DataBind();
  //                          }
  //                          else
  //                          {
  //                              bday1.Visible = false;
  //                          }
  //                      }
  //                  }

/////                        //DateTime check = DateTime.Now.AddDays(7);
//if ((udt > DateTime.Now) && (udt < check))