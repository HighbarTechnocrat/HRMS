using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Configuration;
public partial class AppreciationLetter_CreateRedmptionReq
 : System.Web.UI.Page
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
                hdnEmpCode.Value = Convert.ToString(Session["Empcode"]);
                hdnEmp_Name.Value = Convert.ToString(Session["emp_loginName"]);
                  
                if (!Page.IsPostBack)
                { 
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
     
    private void Redmption_Point_GridView()
    {
        try
        {
            DataSet dtleaveInbox = new DataSet();

            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "Redmption_Point_GridView";

            spars[1] = new SqlParameter("@send_to", SqlDbType.VarChar);
            spars[1].Value = hdnEmpCode.Value;

            dtleaveInbox = spm.getDatasetList(spars, "Appreciation_Letter");

            if (dtleaveInbox.Tables.Count > 0)
            {
                gvMngLeaveRqstList.DataSource = dtleaveInbox;
                gvMngLeaveRqstList.DataBind();
            }

            decimal remainingPointsValue = Convert.ToDecimal(gvMngLeaveRqstList.Rows[0].Cells[0].Text);
            if (remainingPointsValue > 0)
            {
                Redeempont.Visible = true;
                remark.Visible = true;
                btnIn.Visible = true;
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {

        if (Convert.ToString(txtRedeemPoint.Text).Trim() == "")
        {
            lblmessage.Text = "Please Enter Redeem Point";
            return;
        }

        int redeemPoint;
        if (!int.TryParse(txtRedeemPoint.Text.Trim(), out redeemPoint))
        {
            lblmessage.Text = "Please enter a valid number.";
            return;
        }

        if (redeemPoint <= 0)
        {
            lblmessage.Text = "Redeem points must be greater than zero.";
            return;
        }

        decimal remainingPointsValue = Convert.ToDecimal(gvMngLeaveRqstList.Rows[0].Cells[2].Text);
        decimal redeemPoints = Convert.ToDecimal(txtRedeemPoint.Text.Trim());
        if (redeemPoints > remainingPointsValue)
        {
            lblmessage.Text = "Redeem Point not greater than Balance.";
            return;
        }

        
        DataSet dsgoal = new DataSet();
        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "insert_Redmption_Point";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(hdnEmpCode.Value).Trim();

        spars[2] = new SqlParameter("@redeemed_point", SqlDbType.VarChar);
        spars[2].Value = Convert.ToString(txtRedeemPoint.Text).Trim();

        spars[3] = new SqlParameter("@remarkby_employee", SqlDbType.VarChar);
        spars[3].Value = Convert.ToString(txtRemark.Text).Trim();
 
        dsgoal = spm.getDatasetList(spars, "Appreciation_Letter");
         
        if (dsgoal.Tables[0].Rows.Count > 0)
        {
            hdn_AdminName.Value = dsgoal.Tables[0].Rows[0]["Emp_Name"].ToString();
            hdn_AdminEmail.Value = dsgoal.Tables[0].Rows[0]["Emp_Emailaddress"].ToString();
        }
        var srnoid = dsgoal.Tables[1].Rows[0]["MaxRedemptionID"];
        string redirectURL = Convert.ToString(ConfigurationManager.AppSettings["AppreciationLetterURL"]).Trim() + "?reqid=" + srnoid;
        string AdminEmail = hdn_AdminEmail.Value;
        string AdminName = hdn_AdminName.Value;
        string Emp_Name = hdnEmp_Name.Value;
        string strsubject = "Appreciation for Redemption Request: " + Emp_Name;

        StringBuilder strbuild = new StringBuilder();

        strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:100%'>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>");
        strbuild.Append("<tr><td>Dear <strong>" + AdminName + "</strong>,</td></tr>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>");
        strbuild.Append("<tr><td>This is to inform you that <strong>" + Emp_Name + "</strong> has submitted on Appreciation Letter Redemption Request in OneHR. Kindly review and approve the redemption request.</td></tr>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>");
        strbuild.Append("<tr><td  colspan=2 style='height:20px'><a href='" + redirectURL + "'> Please click here to review / approve </a></td></tr>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>");
        strbuild.Append("<tr><td>This is a system-generated email. Please do not reply.</td></tr>");
        strbuild.Append("</table>");

        spm.sendMail(AdminEmail, strsubject, strbuild.ToString(), "", "");   

        Response.Redirect("~/procs/Appreciation_Letter_index.aspx");
    }

   
}
