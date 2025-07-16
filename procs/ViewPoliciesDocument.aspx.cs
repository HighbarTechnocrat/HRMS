using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;

public partial class procs_ViewPoliciesDocument : System.Web.UI.Page
{
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {
        //string fileName = Request.QueryString["fileName"];
        ////string path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
        //WebClient client = new WebClient();
        //Byte[] buffer = client.DownloadData(fileName);
        //HttpContext.Current.Response.Clear();
        //HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");
        //HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=" + fileName);
        //HttpContext.Current.Response.BinaryWrite(buffer);
        //HttpContext.Current.Response.End();
        if (Request.QueryString["fileName"] != null)
        {
            LoadFile();
        }
        if (Request.QueryString["fileName2"] != null)
        {
            string OnlyfileName = Request.QueryString["fileName2"];
            string filepath = Convert.ToString(ConfigurationManager.AppSettings["DocumentPolicyPath"]).Trim();
            string fileName = filepath + OnlyfileName;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('ViewPoliciesDocument.aspx?fileName=" + fileName + "','_blank');", true);
        }
    }

    public void LoadFile()
    {
        string fileName = Request.QueryString["fileName"];

        if (fileName.ToLower().Contains(".pdf"))
        {
            WebClient client = new WebClient();
            Byte[] buffer = client.DownloadData(fileName);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=" + fileName);
            HttpContext.Current.Response.BinaryWrite(buffer);
            HttpContext.Current.Response.End();
        }
        else
        {
            string strfilename = fileName.Split('/').Last();

            string filepath = Convert.ToString(ConfigurationManager.AppSettings["DocumentPolicyPath"]).Trim();

            string strURL = filepath + strfilename;
            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + strURL + "\"");
            byte[] data = req.DownloadData(strURL);
            response.BinaryWrite(data);
            response.End();
        }
    }
}