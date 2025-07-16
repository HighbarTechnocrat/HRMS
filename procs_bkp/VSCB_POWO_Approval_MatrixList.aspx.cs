using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_VSCB_POWO_Approval_MatrixList : System.Web.UI.Page
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
                    get_POWOApprovalMatrix_DropdownList();
                    get_POWOApprovalMatrixList();
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

        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdncostcenterId.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();

        Response.Redirect("VSCB_POWO_Approval_Matrix.aspx?prjid=" + hdncostcenterId.Value);

       // lnkLeaveDetails_Click

    }

    protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMngTravelRqstList.PageIndex = e.NewPageIndex;
        this.get_POWOApprovalMatrixList();
    }
    #endregion

    #region Page Methods


    private void get_POWOApprovalMatrixList()
    {
        try
        {
            DataSet dsmyInvoice = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_POWOApprovalMatrix_Namaste_LookUp";

            spars[1] = new SqlParameter("@costcenter", SqlDbType.VarChar);
            if (Convert.ToString(lstCostCenter.SelectedValue).Trim() != "0")
                spars[1].Value = Convert.ToString(lstCostCenter.SelectedValue);
            else
                spars[1].Value = DBNull.Value;

            spars[2] = new SqlParameter("@Searchempcode", SqlDbType.VarChar);
            if (Convert.ToString(lstEmployees.SelectedValue).Trim() != "0")
                spars[2].Value = Convert.ToString(lstEmployees.SelectedValue);
            else
                spars[2].Value = DBNull.Value;

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
        }
        catch (Exception ex)
        {
        }
    }

    private void get_POWOApprovalMatrix_DropdownList()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_POWOApprovalMatrix_Namaste_LookUp_dropdown_List";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCode.Value;

        dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        if (dsList.Tables[0].Rows.Count > 0)
        {
            lstCostCenter.DataSource = dsList.Tables[0];
            lstCostCenter.DataTextField = "Tallycode";
            lstCostCenter.DataValueField = "Dept_ID";
            lstCostCenter.DataBind();
        }
        lstCostCenter.Items.Insert(0, new ListItem("Select Cost Center", "0"));

        if (dsList.Tables[1].Rows.Count > 0)
        {
            lstEmployees.DataSource = dsList.Tables[1];
            lstEmployees.DataTextField = "EmpNameCode";
            lstEmployees.DataValueField = "Emp_Code";
            lstEmployees.DataBind();
        }
        lstEmployees.Items.Insert(0, new ListItem("Select Employee Name", "0"));
    }

    #endregion
    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        get_POWOApprovalMatrixList();
        lblmessage.Text = "";
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        get_POWOApprovalMatrix_DropdownList();
        get_POWOApprovalMatrixList();
        lblmessage.Text = "";

    }
    protected void localtrvl_btnSave_Click(object sender, EventArgs e)
    {
        Response.Redirect("VSCB_POWO_Approval_Matrix.aspx");
    }
}