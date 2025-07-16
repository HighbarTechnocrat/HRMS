using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_VSCB_ProductList : System.Web.UI.Page
{
    #region Creative_Default_methods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int RecordCountDelete = 0;
    public int did = 0;

    SP_Methods spm = new SP_Methods();
    string strempcode = "";
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void lnkcont_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/SampleForm");
    }

    #endregion
    public DataTable dtPOWONo;
    #region Page_Events

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }


            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");


            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/vscb_index");
            }
            else
            {


                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    hdnEmpCode.Value = Session["Empcode"].ToString();
                    get_myPOWOMilestone_DropdownList();
                    getMngTravelReqstList();


                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    //protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    //{

    //    ImageButton btn = (ImageButton)sender;
    //    GridViewRow row = (GridViewRow)btn.NamingContainer;
    //    hdnPOWOID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
    //    Response.Redirect("VSCB_VendorCreate.aspx?VendorID=" + hdnPOWOID.Value + "");
    //}

    protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMngTravelRqstList.PageIndex = e.NewPageIndex;
        this.getMngTravelReqstList();
    }
    #endregion

    #region Page Methods


    private void getMngTravelReqstList()
    {
        try
        {
            DataSet dsmyInvoice = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_msgProductList";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = strempcode;

            spars[2] = new SqlParameter("@spaymentId", SqlDbType.VarChar);
            if (Convert.ToString(lstproductName.SelectedValue).Trim() != "0")
                spars[2].Value = lstproductName.SelectedItem.Text.Trim(); 
            else
                spars[2].Value = DBNull.Value;

            spars[3] = new SqlParameter("@POType", SqlDbType.VarChar);
            if (Convert.ToString(lstUOMCode.SelectedValue).Trim() != "0")
                spars[3].Value = lstUOMCode.SelectedItem.Text.Trim();
            else
                spars[3].Value = DBNull.Value;

            dsmyInvoice = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            gvMngTravelRqstList.DataSource = null;
            gvMngTravelRqstList.DataBind();
            RecordCount.Text = "";
            lblmessage.Text = "";

            if (dsmyInvoice.Tables[0].Rows.Count > 0)
                {
                    RecordCountDelete = dsmyInvoice.Tables[0].Rows.Count;
                    RecordCount.Text = "Record Count : " + Convert.ToString(dsmyInvoice.Tables[0].Rows.Count);
                    gvMngTravelRqstList.DataSource = dsmyInvoice.Tables[0];
                    gvMngTravelRqstList.DataBind();
                }
              else
                {
                    lblmessage.Text = "Record's not found.!";
                    lblmessage.Visible = true;
                }
              localtrvl_btnSave.Visible = true;
        }
        catch (Exception ex)
        {

        }
    }

    private void get_myPOWOMilestone_DropdownList()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Product_dropdown_List";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = strempcode;

        dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            if (dsList.Tables[0].Rows.Count > 0)
            {
                lstproductName.DataSource = dsList.Tables[0];
                lstproductName.DataTextField = "Item_detail";
                lstproductName.DataValueField = "Item_details";
                lstproductName.DataBind();
            }
            if (dsList.Tables[1].Rows.Count > 0)
            {
               lstUOMCode.DataSource = dsList.Tables[1];
               lstUOMCode.DataTextField = "UOM";
               lstUOMCode.DataValueField = "UOMS";
               lstUOMCode.DataBind();
            }
        lstproductName.Items.Insert(0, new ListItem("Select Product Name", "0"));
        lstUOMCode.Items.Insert(0, new ListItem("Select UOM", "0"));
    }

    #endregion

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        getMngTravelReqstList();
        lblmessage.Text = "";
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        get_myPOWOMilestone_DropdownList();
        getMngTravelReqstList();
        lblmessage.Text = "";

    }


    protected void localtrvl_btnSave_Click(object sender, EventArgs e)
    {
        Response.Redirect("VSCB_productCreate.aspx");
    }
}