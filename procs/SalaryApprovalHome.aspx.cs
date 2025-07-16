using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_SalaryApprovalHome : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    public DataTable dt, dtExitSurvey, dtEmpCode;
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
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
            }


            string Emp_Code = Session["Empcode"].ToString();
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "/default.aspx");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    if (CheckUserAccess(Emp_Code))
                    {
                        dt = spm.GetPendingSalaryStatusUpdateForRM(Session["Empcode"].ToString());
                        if (dt.Rows.Count > 0)
                        {
                            lnk_leaverequest.Text = "Update Salary Status("+Convert.ToString(dt.Rows.Count) +")";
                        }
                        else
                        {
                            lnk_leaverequest.Text = "Update Salary Status(" + Convert.ToString(dt.Rows.Count) + ")";
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {

            ErrorLog.WriteError(ex.ToString());
        }
    }
    public bool CheckUserAccess(string empCode)
    {
        bool useraccess = false;
        
        dt = spm.getEmpDetailsForSalaryApproval(empCode);
        if (dt.Rows.Count <= 0)
        {
            lblmsg.Text = "You have not access of this page.";
            //aSalApp.HRef = "../default.aspx";
            useraccess = false;
        }
        else
        {
            useraccess = true;
        }

        DataSet DSConsultants = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
        spars[0].Value = "GetEmpDataCheckAcessConsultants";
        spars[1] = new SqlParameter("@EMPCode", SqlDbType.VarChar);
        spars[1].Value = empCode;
        DSConsultants = spm.getDatasetList(spars, "spGetEmpDetailsForSalary");
        if (DSConsultants.Tables[0].Rows.Count > 0)
        {
            lblmsg.Text = "";
            //aSalApp.HRef = "../default.aspx";
            useraccess = true;
        }

        return useraccess;
    }
}