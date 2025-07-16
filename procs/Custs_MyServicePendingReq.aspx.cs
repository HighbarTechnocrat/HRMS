using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Custs_MyServicePendingReq : System.Web.UI.Page
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
    int pageSize = 5; // Set your desired page size

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void lnkcont_Click(object sender, EventArgs e)
    {
        /*int pageNumber = ViewState["PageNumber"] != null ? (int)ViewState["PageNumber"] : 1;       

        pageNumber++; // Increment the page number for the next set of records
        InboxMobileRemReqstList(pageNumber, pageSize);

        ViewState["PageNumber"] = pageNumber; // Store the current page number in ViewState
        */
        
    }

    #endregion

    #region Page_Events

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["CustCode"]).Trim() == "" || Session["CustCode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "Cust_SessionEnd.aspx");
            }

            if (Convert.ToString(Session["CustCode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "cust_login.aspx");

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "cust_login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Custs_MyServiceReq");
            }
            else
            {
                Page.SmartNavigation = true;
                hdnEmpCode.Value = Convert.ToString(Session["CustCode"]);
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    InboxMobileRemReqstList_AllRecords();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    protected void lnkFuelDetails_Click(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnRemid.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
        Response.Redirect("~/procs/CustsServiceReqApp_Cust.aspx?id=" + hdnRemid.Value + "&type=emp");
    }

    #endregion

    #region PageMethods
    private void InboxMobileRemReqstList_AllRecords()
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "Get_CustomerAction_Pending_ServiceReqList";

            spars[1] = new SqlParameter("@Custcode", SqlDbType.VarChar);
            spars[1].Value = hdnEmpCode.Value;

            DataSet dslist = spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");
            if (dslist != null)
            {
                if (dslist.Tables[0].Rows.Count > 0)
                {
                    gvMngTravelRqstList.DataSource = dslist;
                    gvMngTravelRqstList.DataBind();
                }

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }






    #endregion



  
}
