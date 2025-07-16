using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;


public partial class procs_SalaryProcessed : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    public DataTable dt, dtExitSurvey, dtEmpCode;
    string Emp_Code;
    bool hasKeys;
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

            Emp_Code = Session["Empcode"].ToString();
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    dt = spm.GetProcessedSalary(Session["Empcode"].ToString());
                    dgSalApproved.Visible = true;
                    dgSalApproved.DataSource = null;
                    dgSalApproved.DataBind();
                    if (dt.Rows.Count > 0)
                    {
                        dgSalApproved.DataSource = dt;
                        dgSalApproved.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {

            ErrorLog.WriteError(ex.ToString());
        }
    }

    protected void dgSalApproved_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName=="ViewData")
        {
            Response.Redirect("SalaryApproval.aspx?RMSalStatusId=" + e.CommandArgument);
        }
    }
}