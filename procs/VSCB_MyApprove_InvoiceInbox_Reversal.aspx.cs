using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_VSCB_MyApprove_InvoiceInbox_Reversal : System.Web.UI.Page
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

    protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    {

        //LinkButton btn = (LinkButton)sender;
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnInvoiceId.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnMilestoneId.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
        hdnPOWOID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();

        Response.Redirect("VSCB_ApproveInvoiceInbox_Reversal.aspx?invid=" + hdnInvoiceId.Value + "&mngexp=1");

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
                    get_InboxInvoices_DropdownList();
                    getMngInvoiceReqstList();

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

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
            string[] strdate;
            string strPODate = "";

            string[] strInvdate;
            string strfromDate = "";
            string strToDate = "";

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
            SqlParameter[] spars = new SqlParameter[12];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_InvoiceInbox_List_Approvers_Reversal";

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

            spars[5] = new SqlParameter("@InvoiceNo", SqlDbType.VarChar);
            if (Convert.ToString(lstInvoiceNo.SelectedValue).Trim() != "0")
                spars[5].Value = Convert.ToString(lstInvoiceNo.SelectedValue);
            else
                spars[5].Value = DBNull.Value;

            spars[6] = new SqlParameter("@InvoiceDate", SqlDbType.VarChar);
            if (Convert.ToString(strInvoiceDateDate).Trim() != "")
                spars[6].Value = Convert.ToString(strInvoiceDateDate).Trim();
            else
                spars[6].Value = DBNull.Value;

            spars[7] = new SqlParameter("@StatuId", SqlDbType.Int);
            if (Convert.ToString(lstStatus.SelectedValue).Trim() != "0")
                spars[7].Value = Convert.ToInt32(lstStatus.SelectedValue);
            else
                spars[7].Value = DBNull.Value;

            spars[8] = new SqlParameter("@Prj_Dept_id", SqlDbType.Int);
            if (Convert.ToString(lstDepartment.SelectedValue).Trim() != "0")
                spars[8].Value = Convert.ToInt32(lstDepartment.SelectedValue);
            else
                spars[8].Value = DBNull.Value;

            spars[9] = new SqlParameter("@Project_Dept_Name", SqlDbType.VarChar);
            if (Convert.ToString(lstcostCenter.SelectedValue).Trim() != "0")
                spars[9].Value = Convert.ToString(lstcostCenter.SelectedValue).Trim();
            else
                spars[9].Value = DBNull.Value;

            spars[10] = new SqlParameter("@InvoiceFromDate", SqlDbType.VarChar);
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strInvdate = Convert.ToString(txtFromdate.Text).Trim().Split('-');
                strfromDate = Convert.ToString(strInvdate[2]) + "-" + Convert.ToString(strInvdate[1]) + "-" + Convert.ToString(strInvdate[0]);
                spars[10].Value = strfromDate;
            }
            else
            {
                if (Convert.ToString(lstPOWONo.SelectedValue).Trim() != "0" || Convert.ToString(strPODate).Trim() != "" || Convert.ToString(lstVendorName.SelectedValue).Trim() != "0" ||
                    Convert.ToString(lstInvoiceNo.SelectedValue).Trim() != "0" || Convert.ToString(lstcostCenter.SelectedValue).Trim() != "0")
                    spars[10].Value = DBNull.Value;
                else
                    spars[10].Value = Convert.ToString(hdnInvoiceFrom_date.Value).Trim();
            }

            spars[11] = new SqlParameter("@InvoiceToDate", SqlDbType.VarChar);
            if (Convert.ToString(txtToDate.Text).Trim() != "")
            {
                strInvdate = Convert.ToString(txtToDate.Text).Trim().Split('-');
                strToDate = Convert.ToString(strInvdate[2]) + "-" + Convert.ToString(strInvdate[1]) + "-" + Convert.ToString(strInvdate[0]);

                spars[11].Value = strToDate;
            }
            else
            {
                if (Convert.ToString(lstPOWONo.SelectedValue).Trim() != "0" || Convert.ToString(strPODate).Trim() != "" || Convert.ToString(lstVendorName.SelectedValue).Trim() != "0" ||
                    Convert.ToString(lstInvoiceNo.SelectedValue).Trim() != "0" || Convert.ToString(lstcostCenter.SelectedValue).Trim() != "0")
                    spars[11].Value = DBNull.Value;
                else
                    spars[11].Value = Convert.ToString(hdnInvoiceTo_date.Value).Trim();
            }

            dsmyInvoice = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            gvMngTravelRqstList.DataSource = null;
            gvMngTravelRqstList.DataBind();
            RecordCount.Text = "";
            //lblmessage.Text = "";
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

    private void get_InboxInvoices_DropdownList()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_MyApprovedInvoiceInbox_Search_DropdownList_Reversal";

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
            lstInvoiceNo.DataValueField = "InvoiceNo";
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

        if (dsList.Tables[6].Rows.Count > 0)
        {
            lstDepartment.DataSource = dsList.Tables[6];
            lstDepartment.DataTextField = "Project_Dept_Name";
            lstDepartment.DataValueField = "Dept_ID";
            lstDepartment.DataBind();
        }
        lstDepartment.Items.Insert(0, new ListItem("Select Project/Department", "0"));

        if (dsList.Tables[7].Rows.Count > 0)
        {
            lstcostCenter.DataSource = dsList.Tables[7];
            lstcostCenter.DataTextField = "CostCentre";
            lstcostCenter.DataValueField = "CostCentre";
            lstcostCenter.DataBind();
        }
        lstcostCenter.Items.Insert(0, new ListItem("Select Cost Center", "0"));

        if (dsList.Tables[8].Rows.Count > 0)
        {
            hdnInvoiceFrom_date.Value = Convert.ToString(dsList.Tables[8].Rows[0]["InvoiceFromDate"]).Trim();
            hdnInvoiceTo_date.Value = Convert.ToString(dsList.Tables[8].Rows[0]["InvoiceToDate"]).Trim();
        }

    }

    #endregion 


    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        getMngInvoiceReqstList();

    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {

        get_InboxInvoices_DropdownList();
        getMngInvoiceReqstList();
        txtFromdate.Text = "";
        txtToDate.Text = "";
    }
}