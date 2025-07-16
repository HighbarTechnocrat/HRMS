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

public partial class procs_ApprovedTimesheet_Req : System.Web.UI.Page
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
    string strempcode = "";
    //protected void lnkhome_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect(ReturnUrl("sitepathmain") + "default");
    //}
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                hdnEmpCode.Value = Convert.ToString(Session["Empcode"]);
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    hdnhrappType.Value = "0";
                    if (Request.QueryString.Count > 0)
                        hdnhrappType.Value = Convert.ToString(Request.QueryString[0]);
                    Fill_EmpName();
                    ApprovedTimeSheetList();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    public void Fill_EmpName()
    {

        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetApprovedTimesheetEmployeeList";
        spars[1] = new SqlParameter("@app_Emp_Code", SqlDbType.VarChar);
        spars[1].Value = strempcode.ToString();

        DataTable dt = spm.getDataList(spars, "SP_Timesheet_Auto_Submit");
        lstEmpCode.DataSource = dt;
        lstEmpCode.DataTextField = "Emp_Name";
        lstEmpCode.DataValueField = "Emp_Code";
        lstEmpCode.DataBind();
        ListItem item = new ListItem("Select Employee Name", "0");
        lstEmpCode.Items.Insert(0, item);
    }

    private void ApprovedTimeSheetList()
    {
        try
        {
            DataTable DTApprovedTS = new DataTable();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetApprovedTimesheetList";
            spars[1] = new SqlParameter("@app_Emp_Code", SqlDbType.VarChar);
            spars[1].Value = strempcode.ToString();
            spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);

            if(lstEmpCode.SelectedItem.Text == "Select Employee Name")
            {
                spars[2].Value = DBNull.Value;
            }
            else
            {
                spars[2].Value = lstEmpCode.SelectedValue;
            }

            DTApprovedTS = spm.getData_FromCode(spars, "SP_Timesheet_Auto_Submit");
            if (DTApprovedTS.Rows.Count > 0)
            {
                gvMngLeaveRqstList.DataSource = DTApprovedTS;
                gvMngLeaveRqstList.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void gvMngLeaveRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        
        gvMngLeaveRqstList.PageIndex = e.NewPageIndex;
        ApprovedTimeSheetList();
    }

    protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnReqid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
            if (hdnReqid.Value != "0")
            {
                Response.Redirect("Timesheet_Req_Approved.aspx?reqid=" + hdnReqid.Value);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        ApprovedTimeSheetList();
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        Fill_EmpName();
        ApprovedTimeSheetList();
    }
}