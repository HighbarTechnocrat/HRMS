using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

 
public partial class AppreciationLetter_ApprovedRedeemedReq : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;

    SP_Methods spm = new SP_Methods();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    DataTable dtLeaveDetails;
    DataSet dsLeaveRequst;
    string strempcode = "";

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    #region Page Events
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Update_Photo_index");
            }
            else
            {
                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();

                if (!Page.IsPostBack)
                {
                    if (check_admin_report() == false)
                    {
                        Response.Redirect("Appreciation_Letter_index.aspx");
                    }

                    if (Request.QueryString.Count > 0)
                    {
                        //s  hdnReqid.Value = "1";
                        hdnReqid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        //srno.Text = hdnReqid.Value;
                    }
                    editform.Visible = true;

                    PopulateEmployeeData();
                    Redmption_Point_GridView();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    #endregion



    #region  

    public Boolean check_admin_report()
    {
        Boolean isvalid = false;

        DataSet dsLocations = new DataSet();

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "check_admin_report";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();

        dsLocations = spm.getDatasetList(spars, "Appreciation_Letter");

        if (dsLocations != null)
        {
            if (dsLocations.Tables[0].Rows.Count > 0)
            {
                isvalid = true;
            }
        }
        return isvalid;

    }
    public void PopulateEmployeeData()
    {
        try
        {
            DataSet dtEmp = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "View_Approved_Redmption_Byadmin";
            spars[1] = new SqlParameter("@sr_no", SqlDbType.VarChar);
            spars[1].Value = hdnReqid.Value;
            dtEmp = spm.getDatasetList(spars, "Appreciation_Letter");
            if (dtEmp.Tables[0].Rows.Count > 0)
            {
                txt_EmpCode.Text = dtEmp.Tables[0].Rows[0]["Emp_Code"].ToString();
                txtEmp_Name.Text = dtEmp.Tables[0].Rows[0]["Emp_Name"].ToString();
                txtEmp_Desigantion.Text = dtEmp.Tables[0].Rows[0]["Designation"].ToString();
                txtEmp_Department.Text = dtEmp.Tables[0].Rows[0]["Department"].ToString();
                txtRedeemPoint.Text = dtEmp.Tables[0].Rows[0]["redeemed_point"].ToString();
                txtRemark.Text = dtEmp.Tables[0].Rows[0]["remarkby_employee"].ToString();
                txtAdmin_Point.Text = dtEmp.Tables[0].Rows[0]["admin_Redeem_Point"].ToString();
                txtremark_admin.Text = dtEmp.Tables[0].Rows[0]["remarkby_admin"].ToString();
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }
    private void Redmption_Point_GridView()
    {
        try
        {
            DataSet dtleaveInbox = new DataSet();

            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "Redmption_Point_GridView";

            spars[1] = new SqlParameter("@send_to", SqlDbType.VarChar);
            spars[1].Value = txt_EmpCode.Text;

            dtleaveInbox = spm.getDatasetList(spars, "Appreciation_Letter");

            if (dtleaveInbox.Tables.Count > 0)
            {
                gvMngLeaveRqstList.DataSource = dtleaveInbox;
                gvMngLeaveRqstList.DataBind();
            }

        }
        catch (Exception ex)
        {

        }
    }

    #endregion
    protected void btnback_mng_Click(object sender, EventArgs e)
    {
        Response.Redirect("AppreciationLetter_ViewApprovedRedeemedReq.aspx");
    }
     
}
