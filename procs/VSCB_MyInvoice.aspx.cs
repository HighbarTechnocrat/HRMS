using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSCB_MyInvoice : System.Web.UI.Page
{

    #region Creative_Default_methods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
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
                    getMngInvoiceReqstList();
                    get_MyInvoice_SearchParameter_List();                     
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnInvoiceId.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnMilestoneId.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
        hdnPOWOID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[3]).Trim();
        Boolean bIsSecurityDepositInvoice = false;
        if (Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[3]).Trim() != "")
        {
            bIsSecurityDepositInvoice = Convert.ToBoolean(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[3]);
        }

        if (bIsSecurityDepositInvoice == true)
            Response.Redirect("VSCB_CreateInvoice_SecurityDeposit.aspx?invid=" + hdnInvoiceId.Value + "&mngexp=1");
        else
            Response.Redirect("VSCB_CreateInvoice.aspx?invid=" + hdnInvoiceId.Value + "&mngexp=1");
    }

    protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMngTravelRqstList.PageIndex = e.NewPageIndex;
        this.getMngInvoiceReqstList();
    }
    #endregion

    #region Page Methods
    private void getMngInvoiceReqstList()
    {
        try
        {
            //DataTable dtTravelRequest = new DataTable();
            //dtTravelRequest = spm.getMyInvoiceList(strempcode);

            //gvMngTravelRqstList.DataSource = null;
            //gvMngTravelRqstList.DataBind();

            //if (dtTravelRequest.Rows.Count > 0)
            //{
            //    gvMngTravelRqstList.DataSource = dtTravelRequest;
            //    gvMngTravelRqstList.DataBind();
            //} 

            string[] strdate;
            string strPODate = "";
            if (Convert.ToString(lstPOWODate.SelectedValue).Trim() != "")
            {
                strdate = Convert.ToString(lstPOWODate.SelectedValue).Trim().Split('-');
                strPODate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }

            string strInvoiceDateDate = "";
            if (Convert.ToString(lstpInvoiceDate.SelectedValue).Trim() != "")
            {
                strdate = Convert.ToString(lstpInvoiceDate.SelectedValue).Trim().Split('-');
                strInvoiceDateDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }

            DataSet dsmyInvoice = new DataSet();
            SqlParameter[] spars = new SqlParameter[8];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_myInvoice_ListInvoice";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = strempcode;

            spars[2] = new SqlParameter("@POID", SqlDbType.BigInt);
            if (Convert.ToString(lstPOWONo.SelectedValue).Trim() != "")
                spars[2].Value = Convert.ToDouble(lstPOWONo.SelectedValue);
            else
                spars[2].Value = DBNull.Value;

            spars[3] = new SqlParameter("@PODate", SqlDbType.VarChar);
            if (Convert.ToString(strPODate).Trim() != "")
                spars[3].Value = Convert.ToString(strPODate).Trim();
            else
                spars[3].Value = DBNull.Value;

            spars[4] = new SqlParameter("@VendorId", SqlDbType.Int);
            if (Convert.ToString(lstVendorName.SelectedValue).Trim() != "")
                spars[4].Value = Convert.ToInt32(lstVendorName.SelectedValue);
            else
                spars[4].Value = DBNull.Value;

            spars[5] = new SqlParameter("@InvoiceID", SqlDbType.BigInt);
            if (Convert.ToString(lstInvoiceNo.SelectedValue).Trim() != "")
                spars[5].Value = Convert.ToDouble(lstInvoiceNo.SelectedValue);
            else
                spars[5].Value = DBNull.Value;

            spars[6] = new SqlParameter("@InvoiceDate", SqlDbType.VarChar);
            if (Convert.ToString(strInvoiceDateDate).Trim() != "")
                spars[6].Value = Convert.ToString(strInvoiceDateDate).Trim();
            else
                spars[6].Value = DBNull.Value;

            spars[7] = new SqlParameter("@StatuId", SqlDbType.Int);
            if (Convert.ToString(lstStatus.SelectedValue).Trim() != "")
                spars[7].Value = Convert.ToInt32(lstStatus.SelectedValue);
            else
                spars[7].Value = DBNull.Value;

            dsmyInvoice = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            gvMngTravelRqstList.DataSource = null;
            gvMngTravelRqstList.DataBind();
            RecordCount.Text = "";
            lblmessage.Text = "";
            if (dsmyInvoice.Tables[0].Rows.Count > 0)
            {
                RecordCount.Text = "Record Count : " + Convert.ToString(dsmyInvoice.Tables[0].Rows.Count);
                gvMngTravelRqstList.DataSource = dsmyInvoice.Tables[0];
                gvMngTravelRqstList.DataBind();
            }
            else
            {
                lblmessage.Text = "Record's not found.!";
                 lblmessage.Visible = true;
            }
        }
        catch (Exception ex)
        {

        }
    }
     

    private void get_MyInvoice_SearchParameter_List()
    {
        try
        {

            DataSet dsProjectsVendors = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_myInvoice_Search_parameterList";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = strempcode;


            dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            if (dsProjectsVendors.Tables[0].Rows.Count > 0)
            {
                lstPOWONo.DataSource = dsProjectsVendors.Tables[0];
                lstPOWONo.DataTextField = "PONumber";
                lstPOWONo.DataValueField = "POID";
                lstPOWONo.DataBind();                
            }
            lstPOWONo.Items.Insert(0, new ListItem("Select PO/WO Number.", ""));


            if (dsProjectsVendors.Tables[1].Rows.Count > 0)
            {
                lstPOWODate.DataSource = dsProjectsVendors.Tables[1];
                lstPOWODate.DataTextField = "PODate";
                lstPOWODate.DataValueField = "PODate";//POID
                lstPOWODate.DataBind(); 
            }
            lstPOWODate.Items.Insert(0, new ListItem("Select PO/WO Date", "")); 


            if (dsProjectsVendors.Tables[2].Rows.Count > 0)
            {
                lstInvoiceNo.DataSource = dsProjectsVendors.Tables[2];
                lstInvoiceNo.DataTextField = "InvoiceNo";
                lstInvoiceNo.DataValueField = "InvoiceID";
                lstInvoiceNo.DataBind();                
            }
            lstInvoiceNo.Items.Insert(0, new ListItem("Select Invoice No", ""));


            if (dsProjectsVendors.Tables[3].Rows.Count > 0)
            {
                lstVendorName.DataSource = dsProjectsVendors.Tables[3];
                lstVendorName.DataTextField = "Name";
                lstVendorName.DataValueField = "VendorID";
                lstVendorName.DataBind();             
            }
            lstVendorName.Items.Insert(0, new ListItem("Select Vendor Name", ""));

            if (dsProjectsVendors.Tables[4].Rows.Count > 0)
            {
                lstpInvoiceDate.DataSource = dsProjectsVendors.Tables[4]; 
                lstpInvoiceDate.DataTextField = "InvoiceDate";
                lstpInvoiceDate.DataValueField = "InvoiceDate";//Payment_ID
                lstpInvoiceDate.DataBind();
                
            }
            lstpInvoiceDate.Items.Insert(0, new ListItem("Select Invoice Date", ""));


            if (dsProjectsVendors.Tables[5].Rows.Count > 0)
            {
                lstStatus.DataSource = dsProjectsVendors.Tables[5];
                lstStatus.DataTextField = "Request_status";
                lstStatus.DataValueField = "Status_id";
                lstStatus.DataBind();               
            }
            lstStatus.Items.Insert(0, new ListItem("Select Status", ""));
        }
        catch (Exception)
        {

        }
    }
    #endregion 


    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        getMngInvoiceReqstList();
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {

        get_MyInvoice_SearchParameter_List();
        getMngInvoiceReqstList();

    }

     
}