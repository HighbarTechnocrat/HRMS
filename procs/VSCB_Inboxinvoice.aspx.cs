using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSCB_Inboxinvoice : System.Web.UI.Page
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
                    get_InboxInvoices_DropdownList();
                    getMngInvoiceReqstList();
                    if (Convert.ToString(strempcode).Trim() == "99999999")
                    {
                        gvMngTravelRqstList.Columns[10].Visible = true;
                    }
                    else
                    {
                        gvMngTravelRqstList.Columns[10].Visible = false;
                    }

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

        //LinkButton btn = (LinkButton)sender;
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnInvoiceId.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnMilestoneId.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
        hdnPOWOID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim(); 
        
        Response.Redirect("VSCB_ApproveInvoice.aspx?invid=" + hdnInvoiceId.Value);
    }

    #endregion

    #region Page Methods

    private void get_InboxInvoices_DropdownList()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Invoice_Inbox_Search_DropdownList";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = strempcode;

        dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        if (dsList.Tables[0].Rows.Count > 0)
        {
            lstPOWONo.DataSource = dsList.Tables[0];
            lstPOWONo.DataTextField = "PONumber";
            lstPOWONo.DataValueField = "POID";
            lstPOWONo.DataBind();
        }
        lstPOWONo.Items.Insert(0, new ListItem("Select PO/WO Number", "0"));


        if (dsList.Tables[1].Rows.Count > 0)
        {
            lstPOWODate.DataSource = dsList.Tables[1];
            lstPOWODate.DataTextField = "PODate";
            lstPOWODate.DataValueField = "PODate";
            lstPOWODate.DataBind();
        }
        lstPOWODate.Items.Insert(0, new ListItem("Select PO/WO Date", "0"));


        if (dsList.Tables[2].Rows.Count > 0)
        {
            lstVendorName.DataSource = dsList.Tables[2];
            lstVendorName.DataTextField = "Name";
            lstVendorName.DataValueField = "VendorId";
            lstVendorName.DataBind();
        }
        lstVendorName.Items.Insert(0, new ListItem("Select Vendor Name", "0"));

        if (dsList.Tables[3].Rows.Count > 0)
        {
            lstInvoiceNo.DataSource = dsList.Tables[3];
            lstInvoiceNo.DataTextField = "InvoiceNo";
            lstInvoiceNo.DataValueField = "InvoiceID";
            lstInvoiceNo.DataBind();
        }
        lstInvoiceNo.Items.Insert(0, new ListItem("Select Invoice No", "0"));

        if (dsList.Tables[4].Rows.Count > 0)
        {
            lstpInvoiceDate.DataSource = dsList.Tables[4];
            lstpInvoiceDate.DataTextField = "InvoiceDate";
            lstpInvoiceDate.DataValueField = "InvoiceDate";
            lstpInvoiceDate.DataBind();
        }
        lstpInvoiceDate.Items.Insert(0, new ListItem("Select Invoice Date", "0"));


        if (dsList.Tables[5].Rows.Count > 0)
        {
            lstStatus.DataSource = dsList.Tables[5];
            lstStatus.DataTextField = "Request_status";
            lstStatus.DataValueField = "Status_id";
            lstStatus.DataBind();
        }
        lstStatus.Items.Insert(0, new ListItem("Select Approval Status", "0")); 
         
    }

    private void getMngInvoiceReqstList()
    {
        try
        { 
            string[] strdate;
            string strPODate = "";
            if (Convert.ToString(lstPOWODate.SelectedValue).Trim() != "0")
            {
                strdate = Convert.ToString(lstPOWODate.SelectedValue).Trim().Split('-');
                strPODate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }

            string strInvoiceDateDate = "";
            if (Convert.ToString(lstpInvoiceDate.SelectedValue).Trim() != "0")
            {
                strdate = Convert.ToString(lstpInvoiceDate.SelectedValue).Trim().Split('-');
                strInvoiceDateDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }

           

            DataSet dsmyInvoice = new DataSet();
            SqlParameter[] spars = new SqlParameter[8];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Pending_InvoiceList";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = strempcode;

            spars[2] = new SqlParameter("@POID", SqlDbType.BigInt);
            if (Convert.ToString(lstPOWONo.SelectedValue).Trim() != "0")
                spars[2].Value = Convert.ToDouble(lstPOWONo.SelectedValue);
            else
                spars[2].Value = DBNull.Value;

            spars[3] = new SqlParameter("@PODate", SqlDbType.VarChar);
            if (Convert.ToString(strPODate).Trim() != "")
                spars[3].Value = Convert.ToString(strPODate).Trim();
            else
                spars[3].Value = DBNull.Value;

            spars[4] = new SqlParameter("@VendorId", SqlDbType.Int);
            if (Convert.ToString(lstVendorName.SelectedValue).Trim() != "0")
                spars[4].Value = Convert.ToInt32(lstVendorName.SelectedValue);
            else
                spars[4].Value = DBNull.Value;

            spars[5] = new SqlParameter("@InvoiceID", SqlDbType.BigInt);
            if (Convert.ToString(lstInvoiceNo.SelectedValue).Trim() != "0")
                spars[5].Value = Convert.ToDouble(lstInvoiceNo.SelectedValue);
            else
                spars[5].Value = DBNull.Value;

            spars[6] = new SqlParameter("@InvoiceDate", SqlDbType.VarChar);
            if (Convert.ToString(strInvoiceDateDate).Trim() != "")
                spars[6].Value = Convert.ToString(strInvoiceDateDate).Trim();
            else
                spars[6].Value = DBNull.Value;

            spars[7] = new SqlParameter("@Statu_Appr", SqlDbType.VarChar);
            //if (Convert.ToString(lstStatus.SelectedValue).Trim() != "0")
            //    spars[7].Value = Convert.ToString(lstStatus.SelectedItem.Text).Trim();
            //else
                spars[7].Value = "Pending";

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
    #endregion 


    protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMngTravelRqstList.PageIndex = e.NewPageIndex;
        this.getMngInvoiceReqstList();
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        getMngInvoiceReqstList();
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {

    }
}