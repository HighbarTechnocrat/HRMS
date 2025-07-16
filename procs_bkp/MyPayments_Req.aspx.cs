using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyPayments_Req : System.Web.UI.Page
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
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/Voucher");
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Voucher");
            }
            else
            {
                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;


                    //   PopulateEmployeeLeaveData();

                    getMngMobileReqstList();

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
        hdnRemid.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
     //   hdnClaimsID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
        //Response.Redirect("TravelRequest.aspx?tripid=" + hdnTrip_Id.Value); clmid=1&remid=1
        Response.Redirect("Payment_Req.aspx?clmid=0&rem_id=" + hdnRemid.Value + "&inb=0");
    }
    #endregion

    #region Page Methods
    private void getMngMobileReqstList()
    {
        try
        {

            DataTable dtTravelRequest = new DataTable();
            dtTravelRequest = spm.getPaymentVoucherRem_MngList(strempcode);

            gvMngTravelRqstList.DataSource = null;
            gvMngTravelRqstList.DataBind();

            if (dtTravelRequest.Rows.Count > 0)
            {
                gvMngTravelRqstList.DataSource = dtTravelRequest;
                gvMngTravelRqstList.DataBind();
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
        this.getMngMobileReqstList();
    }
}