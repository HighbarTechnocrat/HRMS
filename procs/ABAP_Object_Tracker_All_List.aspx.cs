using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ABAP_Object_Tracker_All_List : System.Web.UI.Page
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
                    GetABAPObjectTrackerAllList();
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

        if (row.Cells[4].Text == "Draft")
        {
            Response.Redirect("ABAP_Object_Tracker_Submit_Plan.aspx?ABAPODId=" + hdnABAPODId.Value);
        }
        if (row.Cells[4].Text == "Pending")
        {
            Response.Redirect("ABAP_Object_Tracker_Plan_for_Approval.aspx?ABAPODId=" + hdnABAPODId.Value);
        }

        Response.Redirect("ABAP_Object_Tracker_Plan_View.aspx?ABAPODId=" + hdnABAPODId.Value);
    }

    #endregion

    #region Page Methods
 
    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton lnkEdit = (ImageButton)e.Row.FindControl("lnkEdit");
            if (lnkEdit != null)
            {
                lnkEdit.Visible = false;

                string sessionEmpcode = Session["Empcode"].ToString().Trim() ?? "";
                object createdByObj = DataBinder.Eval(e.Row.DataItem, "CreatedBy");

                if (createdByObj != null && !string.IsNullOrEmpty(sessionEmpcode))
                {
                    string createdBy = createdByObj.ToString().Trim();
                    if (string.Equals(createdBy, sessionEmpcode, StringComparison.OrdinalIgnoreCase))
                    {
                        lnkEdit.Visible = true; 
                    }
                }
            }
        }
    }

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        lblmessage.Text = "";
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        HDID.Value = Convert.ToString(gvABAPObjectPlanList.DataKeys[row.RowIndex].Values[0]).Trim();
        Response.Redirect("ABAP_Object_Tracker_Create_List.aspx?ABAPODId=" + HDID.Value);
    } 
    
    private void GetABAPObjectTrackerAllList()
    {
        try
        {
            DataSet objDS = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetABAPObjectTrackerAllList";

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

                string isCreatorLoginSame = objDS.Tables[0].Rows[0]["IsObjectCreate"].ToString().Trim();

                foreach (GridViewRow row in gvABAPObjectPlanList.Rows)
                { 
                    if (isCreatorLoginSame == "FALSE")
                    {
                        gvABAPObjectPlanList.Columns[5].Visible = false; 
                    }
                }

            }
            else
            {
                Response.Redirect("~/procs/ABAP_Object_Tracker_Index.aspx"); 
                 
            }
            
            //if (objDS.Tables[2].Rows.Count > 0)
            //{
            //    RecordCount.Text = "Record Count : " + Convert.ToString(objDS.Tables[2].Rows.Count);
            //    gvABAPObjectPlanList.DataSource = objDS.Tables[2];
            //    gvABAPObjectPlanList.DataBind();

            //}
            //else if (objDS.Tables[1].Rows.Count > 0)
            //{
            //    RecordCount.Text = "Record Count : " + Convert.ToString(objDS.Tables[1].Rows.Count);
            //    gvABAPObjectPlanList.DataSource = objDS.Tables[1];
            //    gvABAPObjectPlanList.DataBind();

            //}
            //else
            //{
            //    lblmessage.Text = "Record's not found.!";
            //    lblmessage.Visible = true;
            //}


        }
        catch (Exception ex)
        {

        }
    }
    #endregion
     
    protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvABAPObjectPlanList.PageIndex = e.NewPageIndex;
        this.GetABAPObjectTrackerAllList();
    }


}