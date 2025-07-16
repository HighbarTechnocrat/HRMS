using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class themes_creative1 : System.Web.UI.UserControl
{

    SP_Methods spm = new SP_Methods();
    public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;}
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //popularcontrol();
                Populate_image();
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    protected void popularcontrol()
    {
        try
        { 
            creative.Common clsCommon = new creative.Common();
            DataSet ds = clsCommon.Read_XML("HomeBanner.xml", ReturnUrl("sitepathadmin") + "xml/");
            rptbanner.DataSource = ds.Tables[0];
            rptbanner.DataBind();
          
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    public void Populate_image()
    {
        try
        {

            DataSet dtEmp = new DataSet();
            SqlParameter[] spars = new SqlParameter[1];
            spars[0] = new SqlParameter("@QUERYMODE", SqlDbType.VarChar);
            spars[0].Value = "get_slider";

            dtEmp = spm.getDatasetList(spars, "SP_Gallery");

            if (dtEmp.Tables.Count > 0 && dtEmp.Tables[0].Rows.Count > 0)
            {   

                rptSlider.DataSource = dtEmp.Tables[0];
                rptSlider.DataBind();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();           
        }

    }
}