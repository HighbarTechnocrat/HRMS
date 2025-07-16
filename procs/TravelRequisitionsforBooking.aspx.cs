using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_TravelRequisitionsforBooking : System.Web.UI.Page
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/TravelRequisition_Index");
            }
            else
            {


                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    hdnEmpCode.Value = Session["Empcode"].ToString();
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

    protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    {

        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnPOWOID.Value = Convert.ToString(TravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
        Response.Redirect("travelRequisitionBooking_Form.aspx?TrvlReqID=" + hdnPOWOID.Value + "&b=a&c=a");
    }

    protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        TravelRqstList.PageIndex = e.NewPageIndex;
        this.getMngTravelReqstList();
    }
    #endregion

    #region Page Methods


    private void getMngTravelReqstList()
    {
        try
        {
            DataSet dsmyInvoice = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "getTrvaelRequisitions_list_Booking";  

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = strempcode;
 
            dsmyInvoice = spm.getDatasetList(spars, "SP_Employee_TravelRequisitons_Dtls");

            TravelRqstList.DataSource = null;
            TravelRqstList.DataBind();
            RecordCount.Text = "";
            lblmessage.Text = "";

            if (dsmyInvoice.Tables[0].Rows.Count > 0)
            {
                if (dsmyInvoice.Tables[0].Rows.Count > 0)
                {
                    RecordCountDelete = dsmyInvoice.Tables[0].Rows.Count;
                    RecordCount.Text = "Record Count : " + Convert.ToString(dsmyInvoice.Tables[0].Rows.Count);
                    TravelRqstList.DataSource = dsmyInvoice.Tables[0];
                    TravelRqstList.DataBind();
                }
                else
                {
                    lblmessage.Text = "Record's not found.!";
                    lblmessage.Visible = true;
                }
             }
           
        }
        catch (Exception ex)
        {

        }
    }

   

    #endregion


    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        getMngTravelReqstList();
        lblmessage.Text = "";
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
       
        getMngTravelReqstList();
        lblmessage.Text = "";

    }


    protected void localtrvl_btnSave_Click(object sender, EventArgs e)
    {
        Response.Redirect("VSCB_VendorCreate.aspx");
    }

    protected void gvMngTravelRqstList_SelectedIndexChanged(object sender, EventArgs e)
    {

    } 
}