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
using System.Xml;
using System.Text;
using System.IO;
using System.Data.Common;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;

public partial class ThankyouCard_Index : System.Web.UI.Page
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
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/TravelRequisition_Index");
			}
			else
			{
				Page.SmartNavigation = true;
				if (!Page.IsPostBack)
				{
                    CheckIsReport_Admin_Booking();
                    getMngTravelReqstCount();
                    

				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}



    public void CheckIsReport_Admin_Booking()
    {
        var getdtDetails = new DataTable();
        try
        {
            report.Visible = false;
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShow_Hr";
            spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);
            spars[2] = new SqlParameter("@ReportName", SqlDbType.NVarChar);
            spars[2].Value = "thankyoucardreport";
            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_thank_you_card");
             
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    report.Visible = true;
                }
            }
        }
        catch (Exception)
        {
        }
    }

    private void getMngTravelReqstCount()
    {
        try
        {
            DataSet dsList = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "getTrvaelRequisitions_list_Pending_Count";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value).Trim();

            dsList = spm.getDatasetList(spars, "SP_Employee_TravelRequisitons_Dtls");
             
            if (dsList.Tables[0].Rows.Count > 0)
            {
                lnk_leaverequest.Text = "Inbox Travel Requisitions (" + dsList.Tables[0].Rows[0]["pendingCount"] + ")";
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnk_leaverequest_Click(object sender, EventArgs e)
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "delete_GoalMeasurements_Temp_EmpKRATemplate";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(hflEmpCode.Value).Trim();

        dsList = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
        Response.Redirect("KRA_Create.aspx");
    }

    
}