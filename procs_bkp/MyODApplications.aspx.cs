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

public partial class MyODApplications : System.Web.UI.Page
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
                  // PopulateEmployeeLeaveData();
                    if (Request.QueryString.Count > 0)
                        hdnhrappType.Value = Convert.ToString(Request.QueryString[0]);

             

                    InboxLeaveReqstList();

                    //if (Convert.ToString(hdnhrappType.Value) == "1")
                    //checkHR_Inbox();
                     this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    protected void checkHR_Inbox()
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_Check_TD_COS_apprver_code";

            spars[1] = new SqlParameter("@exp_trvl", SqlDbType.VarChar);
            spars[1].Value = "HRML";

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = hdnEmpCode.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");
            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                hdnApproverType.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_type"]).Trim();
                InboxLeaveReqstList_HR();

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    private void InboxLeaveReqstList()
    {
        try
        {
            // DataSet dsList = new DataSet();
            DataTable dtleaveInbox = new DataTable();
            dtleaveInbox = spm.GetODApplicationInbox(strempcode, "MyAllRequest");
  
                if (dtleaveInbox.Rows.Count > 0)
                {
                    gvMngLeaveRqstList.DataSource = dtleaveInbox;
                    gvMngLeaveRqstList.DataBind();
                }
           
        }
        catch (Exception ex)
        {

        }
    }

    protected void InboxLeaveReqstList_HR()
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getAll_OD_LWP_HR";

            spars[1] = new SqlParameter("@empCode", SqlDbType.VarChar);
            spars[1].Value = hdnEmpCode.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETLEAVEINBOX");
            //Travel Request Count
            gvMngLeaveRqstList.DataSource = null;
            gvMngLeaveRqstList.DataBind();
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                gvMngLeaveRqstList.DataSource = dsTrDetails.Tables[0];
                gvMngLeaveRqstList.DataBind();

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }


    #region Not in use code 

    public void PopulateEmployeeLeaveData()
    {
        try
        {
           
            //lpm.Emp_Code = "00630134";



            dtLeaveDetails = spm.GetAttRegInbox(lpm.Emp_Code);
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
    protected void gvMngLeaveRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        PopulateEmployeeLeaveData();
        gvMngLeaveRqstList.PageIndex = e.NewPageIndex;
        gvMngLeaveRqstList.DataSource = dsLeaveRequst;
        gvMngLeaveRqstList.DataBind();
    }

    
    protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnReqid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
            var StatusId = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
            if (hdnReqid.Value != "0")
            {
                if(StatusId=="1")
                {
                    Response.Redirect("ODApplication_App.aspx?reqid=" + hdnReqid.Value + "&type=mod");
                }
                else
                {
                    Response.Redirect("ODApplication_App.aspx?reqid=" + hdnReqid.Value + "&type=arr");
                }
                
            }      


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }


   
}
