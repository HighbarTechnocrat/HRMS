using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class upload_gallery_images : System.Web.UI.Page
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
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    
    string strempcode = "";

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

  
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            lblmessage.Text = "";
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
             
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/upload_gallery_images");
            }
            else
            {
                Page.SmartNavigation = true;
                FilePath.Value = Convert.ToString(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["UploadupdatePhoto"]).Trim()));
                strempcode = Session["Empcode"].ToString();
                lpm.Emp_Code = strempcode;
                if (!Page.IsPostBack)
                { 

                    hdnReqid.Value = "";
                    editform.Visible = true;
                    //if (Request.QueryString.Count > 0)
                    //{
                    //    hdnReqid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                    //}
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                    //if (Convert.ToString(hdnReqid.Value).Trim() != "")
                    //{
                    //    hdnId.Value = Convert.ToString(hdnReqid.Value).Trim();
                    //    if (hdnId.Value != "0")
                    //    {
                    //       // getedit_image(Convert.ToDouble(hdnId.Value));
                    //    }
                    //}
                    get_category_list();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                    if (check_IsAccess() == false)
                    {
                        Response.Redirect("~/PersonalDocuments.aspx");
                    }

                    Bindlist();
                }
               
                 


            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    public Boolean check_IsAccess()
    {
        Boolean isvalid = false;

        DataSet dsLocations = new DataSet();

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@QUERYMODE", SqlDbType.VarChar);
        spars[0].Value = "CheckIsShow_uploaded_image_page";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = strempcode;

        dsLocations = spm.getDatasetList(spars, "SP_Gallery");
        isvalid = false;
        if (dsLocations != null)
        {
            if (dsLocations.Tables[0].Rows.Count > 0)
            {

                //  var getStatus = Convert.ToString(dsLocations.Rows[0]["IsAccess"]);
                if (Convert.ToString(dsLocations.Tables[0].Rows[0]["IsAccess"]) == "SHOW")
                {
                    isvalid = true;
                }
            }

        }
        return isvalid;

    }
    protected void btnIn_Click(object sender, EventArgs e)
    { 
        if (uplodmultiple.PostedFiles.Count > 0)
        {
            string FilePath = "";
            if (ddl_category.SelectedValue == "1")
            {
                 FilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Banner_images"]).Trim());
            }
            else
            {
                FilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Gallery_images"]).Trim());
            }
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            if (uplodmultiple.PostedFiles.Count > 0)
            {
                foreach (HttpPostedFile uploadedFile in uplodmultiple.PostedFiles)
                {
                    
                        string originalFileName = Path.GetFileNameWithoutExtension(uploadedFile.FileName);
                        string fileExtension = Path.GetExtension(uploadedFile.FileName); 
                        string filename = originalFileName + fileExtension; 
                        string fullFilePath = Path.Combine(FilePath, filename); 
                        string image_tooltip = txt_tooltip.Text.Trim();
                        string image_url = txt_url.Text.Trim();
                         string image_category = ddl_category.SelectedValue.ToString();

                    if (btnIn.Text != "Update")
                        {

                        if (Convert.ToString(ddl_category.SelectedValue).Trim() == "0")
                        {
                            lblmessage.Text = "Please Select Image Category";
                            return;
                        }

                        if (Convert.ToString(txt_tooltip.Text).Trim() == "")
                        {
                            lblmessage.Text = "Please Enter Image Title/Tool Tip";
                            return;
                        }

                        if (!uplodmultiple.HasFile)
                            {
                                lblmessage.Text = " Please upload  Image";
                                return;
                            }
                        uploadedFile.SaveAs(fullFilePath);
                        spm.insert_gallery_banner_images("INSERT",image_tooltip, filename, image_url, image_category);
                        }
                        else
                        {

                        if (Convert.ToString(ddl_category.SelectedValue).Trim() == "0")
                        {
                            lblmessage.Text = "Please Select Image Category";
                            return;
                        }

                        if (Convert.ToString(txt_tooltip.Text).Trim() == "")
                        {
                            lblmessage.Text = "Please Enter Image Title/Tool Tip";
                            return;
                        }

                        var id = hdnId.Value; 
                        string QUERYMODE = "UPDATE";

                            DataSet dsgoal = new DataSet();
                            SqlParameter[] spars = new SqlParameter[7];

                            spars[0] = new SqlParameter("@QUERYMODE", SqlDbType.VarChar);
                            spars[0].Value = QUERYMODE;

                            spars[1] = new SqlParameter("@id", SqlDbType.Int);
                            spars[1].Value = id;

                            spars[2] = new SqlParameter("@Image_Des", SqlDbType.VarChar);
                            spars[2].Value = image_tooltip;

                            if (uplodmultiple.HasFile)
                            {
                                uploadedFile.SaveAs(fullFilePath);
                                spars[3] = new SqlParameter("@Image_Name", SqlDbType.VarChar);
                                spars[3].Value = filename;
                            }
                            spars[4] = new SqlParameter("@Active", SqlDbType.VarChar);
                            if (rbtnActive.Checked)
                            {
                                spars[4].Value = "A";  // Active
                            }
                            else if (rbtnDeactive.Checked)
                            {
                                spars[4].Value = "D";  // Deactive
                            }

                        spars[5] = new SqlParameter("@Image_URL", SqlDbType.VarChar);
                        spars[5].Value = image_url;

                        spars[6] = new SqlParameter("@Category_id", SqlDbType.VarChar);
                        spars[6].Value = image_category;

                        dsgoal = spm.getDatasetList(spars, "SP_Gallery");
 
                         
                    }
                }
            }

            get_category_list();
            Bindlist();
            hdnId.Value = "";
            txt_tooltip.Text = "";
            txt_url.Text = "";
            btnIn.Text = "Submit";

        }
 
    }

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnId.Value = Convert.ToString(DgvApprover.DataKeys[row.RowIndex].Values[0]).Trim();
            if (hdnId.Value != "0")
            {
                //  Response.Redirect("upload_gallery_images.aspx?reqid=" + hdnId.Value);
                //getedit_image(Convert.ToInt32(hdnId.Value));
                getedit_image(Convert.ToDouble(hdnId.Value));
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    public void getedit_image(double id)
    {
        try
        {
            txt_tooltip.Text = "";
            txt_url.Text = "";
            var dtlist = spm.getedit_gellery_bannerimage(id);
            if (dtlist.Rows.Count > 0)
            {
                rbtnActive.Visible = true;
                rbtnDeactive.Visible = true;
                ddl_category.SelectedValue = Convert.ToString(dtlist.Rows[0]["category_id"]);
                txt_tooltip.Text = Convert.ToString(dtlist.Rows[0]["Image_Des"]);
                txt_url.Text = Convert.ToString(dtlist.Rows[0]["Image_URL"]);
            }
            btnIn.Text = "Update";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void Bindlist()
    {
        try
        {
             
            DgvApprover.DataSource = null;
            DgvApprover.DataBind();
            var QUERYMODE = "GetPhotoSliderList"; 
            var dtlist = spm.Getgallery_banner_images_list(QUERYMODE);
            if (dtlist.Rows.Count > 0)
            {
                DgvApprover.DataSource = dtlist;
                DgvApprover.DataBind();
            } 

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void DgvApprover_PageIndexChanging(object sender, GridViewPageEventArgs e)
    { 
        
        var dtlist = spm.Getgallery_banner_images_list("GetPhotoSliderList");
        if (dtlist.Rows.Count > 0)
        {
            DgvApprover.PageIndex = e.NewPageIndex;
            DgvApprover.DataSource = dtlist;
            DgvApprover.DataBind();
        } 
    }

    public void get_category_list()
    {

        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@QUERYMODE", SqlDbType.VarChar);
        spars[0].Value = "get_category";
         
        dsProjectsVendors = spm.getDatasetList(spars, "SP_Gallery"); 

        if (dsProjectsVendors.Tables[0].Rows.Count > 0)
        {

            ddl_category.DataSource = dsProjectsVendors.Tables[0];
            ddl_category.DataTextField = "category";
            ddl_category.DataValueField = "category_id";
            ddl_category.DataBind();

        }
        ddl_category.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Image Category", "0"));



    }


    protected void ddl_category_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Convert.ToString(ddl_category.SelectedValue).Trim() != "0")
        {
            DgvApprover.DataSource = null;
            DgvApprover.DataBind();
            var dtlist = spm.Getgallery_banner_images_list("GetPhotoSliderList_ByCategory", Convert.ToString(ddl_category.SelectedValue).Trim());
            if (dtlist.Rows.Count > 0)
            {
                DgvApprover.DataSource = dtlist;
                DgvApprover.DataBind();
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        get_category_list();
        Bindlist();
        hdnId.Value = "";
        txt_tooltip.Text = "";
        txt_url.Text = "";
        btnIn.Text = "Submit";
    }
}
