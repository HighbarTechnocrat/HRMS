using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class procs_ABAP_Prd_TimeSheet_PMApprovedview : System.Web.UI.Page
{
    #region Creative_Default_methods
    SqlConnection source;
    public SqlDataAdapter sqladp;

    SP_Methods spm = new SP_Methods();
    string strempcode = ""; int AddnewCheck = 0;
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }


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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/abap_index");
            }
            else
            {
                Page.SmartNavigation = true;
                hdnEmpCode.Value = Convert.ToString(Session["Empcode"]);
                CheckABAPRights();
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString.Count > 0)
                    {
                        hdnId.Value = Convert.ToString(Request.QueryString[0]);
                        Get_RecordsEdit();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    #endregion

    #region PageMethods

    public void CheckABAPRights()
    {

        SqlParameter[] sparsd = new SqlParameter[2];
        sparsd[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        sparsd[0].Value = "GetApproverPageButton";
        sparsd[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        sparsd[1].Value = Convert.ToString(Session["Empcode"].ToString());
        DataSet DSApprover = spm.getDatasetList(sparsd, "SP_ABAP_Productivity_CompletionSheet");

        if (DSApprover.Tables[1].Rows.Count > 0)
        {
        }
        else
        {
            Response.Redirect("~/procs/Timesheet.aspx");
        }


    }

    public void Get_RecordsEdit()
    {
        SqlParameter[] spars = new SqlParameter[3];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "PM_GetRecordApprovedView";
        spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCode.Value;
        spars[2] = new SqlParameter("@MainID", SqlDbType.Int);
        spars[2].Value = hdnId.Value;
        DataSet DS = spm.getDatasetList(spars, "SP_ABAP_Productivity_CompletionSheet");

        if (DS.Tables[0].Rows.Count > 0)
        {
            txtEmpCode.Text = Convert.ToString(DS.Tables[0].Rows[0]["Emp_Code"]).ToString();
            txtEmpName.Text = Convert.ToString(DS.Tables[0].Rows[0]["Emp_Name"]).ToString();
            txtDepartment.Text = Convert.ToString(DS.Tables[0].Rows[0]["Department_Name"]).ToString();
            txtDesignation.Text = Convert.ToString(DS.Tables[0].Rows[0]["DesginationName"]).ToString();
            txtFromdate.Text = Convert.ToString(DS.Tables[0].Rows[0]["Start_Datee"]).ToString();
            txtToDate.Text = Convert.ToString(DS.Tables[0].Rows[0]["End_datee"]).ToString();
            txtFromdate.Enabled = false;
            txtToDate.Enabled = false;
            dgTimesheetView.DataSource = DS.Tables[0];
            dgTimesheetView.DataBind();

        }
    }

    #endregion


    protected void mobile_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/ABAP_Prd_TimeSheetAppListView.aspx");
    }
}