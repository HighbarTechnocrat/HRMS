using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ABAP_Object_Tracker_Request_NotSubmitted : System.Web.UI.Page
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
            

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/ABAP_Object_Tracker_Index.aspx");
            }
            else
            {
                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    getABAPObjectRequestNotSubmittedList();
                    
                this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    protected void lnkABAPPlanDetails_Click(object sender, EventArgs e)
    {

        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        
        hdnABAPODId.Value = Convert.ToString(gvABAPObjectPlanList.DataKeys[row.RowIndex].Values[0]).Trim(); 
        
        Response.Redirect("ABAP_Object_Tracker_Submit_Plan.aspx?ABAPODId=" + hdnABAPODId.Value);
    }

    #endregion

    #region Page Methods
    

    private void getABAPObjectRequestNotSubmittedList()
    {
        try
        { 
            DataSet objDS= new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetABAPObjectRequestNotSubmittedList";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = (Session["Empcode"]).ToString().Trim();
            
            objDS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");

            gvABAPObjectPlanList.DataSource = null;
            gvABAPObjectPlanList.DataBind();
            RecordCount.Text = "";
            lblmessage.Text = "";
            if (objDS.Tables[0].Rows.Count > 0)
            {
                RecordCount.Text = "Record Count : " + Convert.ToString(objDS.Tables[0].Rows.Count);
                gvABAPObjectPlanList.DataSource = objDS.Tables[0];
                gvABAPObjectPlanList.DataBind(); 
                
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
    
   
}