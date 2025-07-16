using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyKRA : System.Web.UI.Page
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/KRA_Index");
            }
            else
            {
                

                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                   
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
        hdnPOWOID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();


        #region   Delete Temp KRA details
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "delete_GoalMeasurements_Temp";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(hdnEmpCode.Value).Trim();

        spars[2] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
        if (Convert.ToString(hdnPOWOID.Value).Trim() != "")
            spars[2].Value = Convert.ToDecimal(hdnPOWOID.Value);
        else
            spars[2].Value = DBNull.Value;



        dsList = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

        #endregion


        Response.Redirect("KRA_Create.aspx?kraid=" + hdnPOWOID.Value + "&mngexp=1");
    }

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
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "my_submitted_KRA_list";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = strempcode;

             

            dsmyInvoice = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

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


    protected void mobile_btnSave_Click(object sender, EventArgs e)
    { 
        getMngTravelReqstList();
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
       
        getMngTravelReqstList();

    }

     
}