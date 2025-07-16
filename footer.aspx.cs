using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Net;
using System.IO;
using System.Configuration;

public partial class footer : System.Web.UI.Page
{
     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            getfooterlinks();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {

    }
    public void getfooterlinks()
    {
        try
        {
            string cat = Request.QueryString["catname"].ToString();
            #region backbutton

            if (cat.ToLower().Trim() == "policies")
            {
                gobackbtn.Visible = true;
                gobackbtn.HRef = ReturnUrl("sitepathmain") + "PolicyProcedure.aspx";
                gobackbtn.InnerText = "Policy & Procedure";
            }
            if ((cat.ToLower().Trim() == "sustainabilityreport") || (cat.ToLower().Trim() == "waterpolicy") || (cat.ToLower().Trim() == "csrpolicy") || (cat.ToLower().Trim() == "hivaids"))
            {
                gobackbtn.Visible = true;
                gobackbtn.HRef = ReturnUrl("sitepathmain") + "csr.aspx";
                gobackbtn.InnerText = "Policy & Procedure : CSR";
            }
            if ((cat.ToLower().Trim() == "grouppresentation") || (cat.ToLower().Trim() == "brandguidelines"))
            {
                gobackbtn.Visible = true;
                gobackbtn.HRef = ReturnUrl("sitepathmain") + "cc.aspx";
                gobackbtn.InnerText = "Policy & Procedure : CC";
            }
            if ((cat.ToLower().Trim() == "environmentknowledge") || (cat.ToLower().Trim() == "safetyawareness") || (cat.ToLower().Trim() == "strategicdocuments") || (cat.ToLower().Trim() == "corporatemanual"))
            {
                gobackbtn.Visible = true;
                gobackbtn.HRef = ReturnUrl("sitepathmain") + "IMS.aspx";
                gobackbtn.InnerText = "Policy & Procedure : IMS";
            }
            else if ((cat.ToLower().Trim() == "presentations") || (cat.ToLower().Trim() == "marketing") || (cat.ToLower().Trim() == "hbtpolicies"))
            {

                gobackbtn.HRef = ReturnUrl("sitepathmain") + "policyprocedure.aspx";
                gobackbtn.Visible = true;
                gobackbtn.Title = "Policy-Procedure";
                gobackbtn.InnerText = "Policy-Procedure";
            }
        #endregion

            string cat2;
            DataTable dtcat = clscms.searchnewsbyname(cat);

            if (dtcat.Rows.Count > 0)
            {
                cat2 = dtcat.Rows[0]["short_desc"].ToString().ToLower().Trim();
                if (cat.Trim() == cat2)
                {
                    WebClient client = new WebClient();
                    Stream stream = client.OpenRead(ConfigurationManager.AppSettings["sitepathadmin"]+ "Menu/" + cat + ".html");
                    StreamReader sr = new StreamReader(stream);
                    string content = sr.ReadToEnd();
                    staticfooter.InnerHtml = content;
                    //sr.Close();
                    //stream.Close();
                    //client.Dispose();
                }
                DataTable dtsub = new DataTable();
                
                dtsub = clscms.getparentlinkdetails(Convert.ToInt32(dtcat.Rows[0]["indexid"]));
                if (dtsub.Rows.Count > 0)
                {
                    //ltcss.Visible = true;
                    //ltcss.Text = "<style>.commpagesdiv{width:60% !important;}</style>";
                    staticfooter.Attributes.Add("style", "width:68%;display:inline-block;float:left;");
                    pnllinks.Attributes.Add("style", "width:26%;display:inline-block;margin:0 0 0 5%;");
                    //Jayesh Commented below code to hide Related links menu section 3oct2017
                    rptsubcat.DataSource = dtsub;
                    rptsubcat.DataBind();
                    //Jayesh Commented above code to hide Related links menu section 3oct2017

                }
                else
                {
                    //ltcss.Visible = true;
                    //ltcss.Text = "<style>.commpagesdiv{width:88% !important;} .commpagesdiv1{width: 88% !important;}</style>";
                    staticfooter.Attributes.Add("style", "width:100%;display:inline-block;");
                    pnllinks.Visible = false;
                    //    Jayesh Commented below code to hide Related links menu section 3oct2017
                    rptsubcat.Visible = false;
                    //    Jayesh Commented above code to hide Related links menu section 3oct2017
                }
            }

        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
    public string productUrlrewriting(object categoryname)
    {
        string strurl = "";
        try
        {
            strurl = UrlRewritingVM.getUrlRewritingInfo(categoryname, "", "F");
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
    public static string ProcessUrl(string url)
    {
        string newUrl = url;
        //newUrl = newUrl.ToLower();
        //replace spaces with hyphens(-)
        //newUrl = newUrl.Replace(" ", "-");
        newUrl = newUrl.Replace("'", "");
        //newUrl = newUrl.Replace("%20", "-");
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
        newUrl = newUrl.Replace(" ", "");
        newUrl = newUrl.Replace("%2c", ",");
        return newUrl;
    }
}