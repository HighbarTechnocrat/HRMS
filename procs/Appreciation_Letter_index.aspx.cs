using System;
using System.Data; 
using System.Web.UI;
using System.Data.SqlClient;

public partial class Appreciation_Letter_index : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    LeaveBalance bl = new LeaveBalance();
    SP_Methods spm = new SP_Methods();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();



    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            hflEmpCode.Value = Convert.ToString(Session["Empcode"]);
            lblmsg.Visible = false;

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Appreciation_Letter_index");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    getRedmptionRequestCount();
                    CheckIsReport_MC_Member();
                    CheckIsReport_Admin();
                    created_new_letter();

                }
 
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
     
    public void CheckIsReport_MC_Member()
    {
        var getdtDetails = new DataTable();
        try
        {
            lnk_MobACC.Visible = false;
            lnk_leaveinbox.Visible = false;
            lnk_reimbursmentReport.Visible = false;
           SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShow_mcmember";
            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value); 
            getdtDetails = spm.getTeamReportAllDDL(spars, "Appreciation_Letter");

            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    lnk_MobACC.Visible = true;
                    lnk_leaveinbox.Visible = true;
                    lnk_reimbursmentReport.Visible = true;
                }
            }
        }
        catch (Exception)
        {
        }
    }

    public void CheckIsReport_Admin()
    {
        var getdtDetails = new DataTable();
        try
        {
            lnk_report.Visible = false;
            lnk_leaverequest.Visible = false;
            lnk_viewbtn.Visible = false;
            lnk_approvebtn.Visible = false;
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShow_admin";
            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);
            getdtDetails = spm.getTeamReportAllDDL(spars, "Appreciation_Letter");

            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    lnk_report.Visible = true;
                    lnk_leaverequest.Visible = true;
                    lnk_viewbtn.Visible = true;
                    lnk_approvebtn.Visible = true;
                }
            }
        }
        catch (Exception)
        {
        }
    }

    public void created_new_letter()
    {
        var getdtDetails = new DataTable();
        try
        {
            lnk_Deemed_Approval.Visible = false;
            lnk_Index_Acc_Invoices.Visible = false;
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "created_new_letter";
            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);
            getdtDetails = spm.getTeamReportAllDDL(spars, "Appreciation_Letter");

            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {

                  lnk_Index_Acc_Invoices.Visible = true;
                    lnk_Deemed_Approval.Visible = true;
                }
            }
        }
        catch (Exception)
        {
        }
    }

    private void getRedmptionRequestCount()
    {
        try
        {
            DataSet dsList = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getPending_Count";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value).Trim();

            dsList = spm.getDatasetList(spars, "Appreciation_Letter");

            if (dsList.Tables[0].Rows.Count > 0)
            {
                lnk_viewbtn.Text = "Inbox Redmption Request (" + dsList.Tables[0].Rows[0]["pendingCount"] + ")";
            }
        }
        catch (Exception ex)
        {

        }
    }
}