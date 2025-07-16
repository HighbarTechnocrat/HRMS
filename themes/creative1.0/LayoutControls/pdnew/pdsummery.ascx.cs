using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Themes_SecondTheme_LayoutControls_pdnew_pdsummery : System.Web.UI.UserControl
{
    private void PopulateControl()
    {
        //try
        //{
            Control userproductsummary = LoadControl(String.Format("../../LayoutControls/pdnew/pdsumtitle.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            uxproductsummary.ID = "m_uxproductsummary";
            uxproductsummary.Controls.Add(userproductsummary);

            //Control userpshop = LoadControl(String.Format("psshop.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            //uxpshop.ID = "m_uxpshop";
            //uxpshop.Controls.Add(userpshop);


            Control userpfproduct = LoadControl(String.Format("../../LayoutControls/pdnew/pdfproduct.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            uxpfproduct.ID = "m_uxpfproduct";
            uxpfproduct.Controls.Add(userpfproduct);

            //Control moviebanner = LoadControl(String.Format("../../LayoutControls/pdnew/moviebanner.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            //uxmoviebanner.ID = "m_uxmoviebanner";
            //uxmoviebanner.Controls.Add(moviebanner);

            Control userrelateprod = LoadControl(String.Format("../../LayoutControls/pdnew/pdrelatedproduct.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            uxodrelateprod.ID = "m_userrelateprod";
            uxodrelateprod.Controls.Add(userrelateprod);

            Control reviewrating = LoadControl(String.Format("../../LayoutControls/pdnew/pdreview.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            uxreview.ID = "m_uxpdrating";
            uxreview.Controls.Add(reviewrating);


            //Control moviereview = LoadControl(String.Format("moviereview.ascx", ConfigurationManager.AppSettings["projectname"].ToString()));
            //uxpnlreview.ID = "m_uxpnlreview";
            //uxpnlreview.Controls.Add(moviereview);
        //}
        //catch (Exception ex)
        //{
        //    ErrorLog.WriteError(ex.ToString());
        //}
    }
    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} 
    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
            PopulateControl();
        //}
        //catch (Exception ex)
        //{
        //    ErrorLog.WriteError(ex.ToString());
        //}
    }
}
