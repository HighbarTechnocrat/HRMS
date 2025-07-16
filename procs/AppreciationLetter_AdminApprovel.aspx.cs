using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;

public partial class AppreciationLetter_AdminApprovel : System.Web.UI.Page
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Appreciation_Letter_index");
            }
            else
            {
                Page.SmartNavigation = true;
                hdnLogin_Name.Value = Convert.ToString(Session["emp_loginName"]);
                if (!Page.IsPostBack)
                {
                    if (check_admin_report() == false)
                    {
                        Response.Redirect("Appreciation_Letter_index.aspx");
                    }

                    if (Request.QueryString.Count > 0)
                    {
                        hdnReqid.Value = Convert.ToString(Request.QueryString[0]).Trim();

                    }

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
            spars[0].Value = "View_Approvel_Redmption_Byadmin";
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
        Response.Redirect("AppreciationLetter_View_ApprovelReq.aspx");
    }
    protected void btnIn_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString();
            if (confirmValue != "Yes")
            {
                return;
            }

            if (Convert.ToString(txtAdmin_Point.Text).Trim() == "")
            {
                lblmessage.Text = "Please Enter Redeem Point";
                return;
            }

            decimal RedeemPoint = Convert.ToDecimal(txtRedeemPoint.Text.Trim());
            decimal AdminPoint = Convert.ToDecimal(txtAdmin_Point.Text.Trim());
            if (RedeemPoint < AdminPoint)
            {
                lblmessage.Text = "Admin Point is greater than Redeem Point.";
                return;
            }

            int redeemPoint;
            if (!int.TryParse(txtAdmin_Point.Text.Trim(), out redeemPoint))
            {
                lblmessage.Text = "Please enter a valid number.";
                return;
            }

            if (redeemPoint <= 0)
            {
                lblmessage.Text = "Redeem points must be greater than zero.";
                return;
            }

            DataSet dsgoal = new DataSet();
            SqlParameter[] spars = new SqlParameter[5];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "update_Redmption_Byadmin";

            spars[1] = new SqlParameter("@actionby_admin", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();

            spars[2] = new SqlParameter("@admin_Redeem_Point", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(txtAdmin_Point.Text).Trim();

            spars[3] = new SqlParameter("@remarkby_admin", SqlDbType.VarChar);
            spars[3].Value = Convert.ToString(txtremark_admin.Text).Trim();

            spars[4] = new SqlParameter("@sr_no", SqlDbType.Int);
            spars[4].Value = Convert.ToString(hdnReqid.Value).Trim();

            dsgoal = spm.getDatasetList(spars, "Appreciation_Letter");

            if (dsgoal.Tables[0].Rows.Count > 0)
            {
                hdn_emp_name.Value = dsgoal.Tables[0].Rows[0]["Emp_Name"].ToString();
                hdn_emp_mail.Value = dsgoal.Tables[0].Rows[0]["Emp_Emailaddress"].ToString();
            }

            string empEmail = hdn_emp_mail.Value;
            string AdminName = hdnLogin_Name.Value;
            string Emp_Name = hdn_emp_name.Value;
            string strsubject = "Appreciation for Redemption Request Approved-Admin";

            StringBuilder strbuild = new StringBuilder();

            strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:100%'>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td>Dear <strong>" + Emp_Name + "</strong>,</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td>This is to inform you that <strong>" + AdminName + "</strong> has Approved your Appreciation Letter Redemption Request in OneHR.</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td>This is a system-generated email. Please do not reply.</td></tr>");
            strbuild.Append("</table>");

            spm.sendMail(empEmail, strsubject, strbuild.ToString(), "", "");

            Response.Redirect("~/procs/AppreciationLetter_View_ApprovelReq.aspx");

        }
        catch (Exception ex)
        { }
    }

}
