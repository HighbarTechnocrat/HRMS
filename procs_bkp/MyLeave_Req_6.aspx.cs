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



public partial class MyLeave_Req_6 : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc="", dept="", subdept="", desg = "";
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

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url;}

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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;


                    //   PopulateEmployeeLeaveData();
                    getMngLeaveReqstList();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
    protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnReqid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
            hdnleaveTypeid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
            //   getSelectedEmpLeaveDetails_View();
            //Response.Redirect("Leave_Req_View.aspx?reqid=" + hdnReqid.Value);
            if (hdnleaveTypeid.Value == "11")
            {
                Response.Redirect("Encash_leave.aspx?reqid=" + hdnReqid.Value);
            }
            else
            {
                Response.Redirect("Leave_Req_6.aspx?reqid=" + hdnReqid.Value);
            }




        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    protected void gvMngLeaveRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        getMngLeaveReqstList();
        gvMngLeaveRqstList.PageIndex = e.NewPageIndex;
        gvMngLeaveRqstList.DataSource = dsLeaveRequst;
        gvMngLeaveRqstList.DataBind();
    }
    #endregion

    #region Page Methods
    private void getMngLeaveReqstList()
    {
        try
        {
            // DataSet dsList = new DataSet();

            dsLeaveRequst = spm.getLeaveRequest_MngList(strempcode);
            gvMngLeaveRqstList.DataSource = null;
            gvMngLeaveRqstList.DataBind();
            if (dsLeaveRequst.Tables != null)
            {
                if (dsLeaveRequst.Tables[0].Rows.Count > 0)
                {
                    gvMngLeaveRqstList.DataSource = dsLeaveRequst.Tables[0];
                    gvMngLeaveRqstList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    #endregion


    #region code Not in use

    public void PopulateEmployeeLeaveData()
    {
        try
        {
           
           // lpm.Emp_Code = "00630134";



            dtLeaveDetails = spm.MyLeave_Req(lpm.Emp_Code);
            gvMngLeaveRqstList.DataSource = dtLeaveDetails;
            gvMngLeaveRqstList.DataBind();
                

             
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();
           
            throw;
        }
       
    
    }   

    #endregion


}
