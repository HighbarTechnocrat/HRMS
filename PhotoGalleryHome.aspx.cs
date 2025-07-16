using System;
using System.Data;
using System.Data.SqlClient;

public partial class GalleryHome : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;

    SP_Methods spm = new SP_Methods();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    private void Page_Load(object sender, System.EventArgs e)
    {

        hdngallryid.Value = Convert.ToString(Request.QueryString["gallaryid"]).Trim();
        lblheading.Text = "";

        Populate_image();
    }


    public void Populate_image()
    {
        try
        {

            DataSet dtimg = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@QUERYMODE", SqlDbType.VarChar);
            spars[0].Value = "get_images";

            spars[1] = new SqlParameter("@Category_id", SqlDbType.VarChar);
            spars[1].Value = hdngallryid.Value;

            dtimg = spm.getDatasetList(spars, "SP_Gallery");

            if (dtimg.Tables.Count > 0 && dtimg.Tables[0].Rows.Count > 0)
            {
                rptSlider.DataSource = dtimg.Tables[0];
                rptSlider.DataBind();
            }
            if (dtimg.Tables[1].Rows.Count > 0)
            {
                lblheading.Text = Convert.ToString(dtimg.Tables[1].Rows[0]["category"]).Trim();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }

}