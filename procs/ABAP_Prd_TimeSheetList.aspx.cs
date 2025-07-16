using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_ABAP_Prd_TimeSheetList : System.Web.UI.Page
{
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public int did = 0;
    SP_Methods spm = new SP_Methods();
    DataSet dsRecruterInox;
    // public DataTable dtRecCandidate, dtcandidateDetails, dtmainSkillSet;


    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
            // lblmessage.Text = "";
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
            // lpm.Emp_Code = Session["Empcode"].ToString();
            // lblmessage.Visible = true;
            //   
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/abap_index");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    getTimeListList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    #endregion

    #region page Methods


    private void getTimeListList()
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "GetTimeSheetList";
            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();

            DataSet DS = spm.getDatasetList(spars, "SP_ABAP_Productivity_CompletionSheet");
            gvTimeSheetList.DataSource = DS.Tables[0];
            gvTimeSheetList.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        
        HFIDD.Value = Convert.ToString(gvTimeSheetList.DataKeys[row.RowIndex].Values[0]).Trim();
        Response.Redirect("~/procs/ABAP_Prd_TimeSheetAdd.aspx?ID=" + HFIDD.Value);
    }


    #endregion

    protected void gvTimeSheetList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTimeSheetList.PageIndex = e.NewPageIndex;
        getTimeListList();
    }
   
}