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
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.Web;

public partial class InboxServiceRequest_Arch : System.Web.UI.Page
{

    #region Creative_Default_methods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;

    SP_Methods spm = new SP_Methods();
    string strempcode = "";
    //protected void lnkhome_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect(ReturnUrl("sitepathmain") + "default");
    //}

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    //protected void lnkcont_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect(ReturnUrl("sitepathmain") + "procs/Reembursementindex");
    //}
    #endregion

    #region Page_Events

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                Page.SmartNavigation = true;
                hdnEmpCode.Value = Convert.ToString(Session["Empcode"]);
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    if (Request.QueryString.Count > 0)
                        hdninboxtype.Value = Convert.ToString(Request.QueryString[0]);

                    //PopulateEmployeeLeaveData();
                   // InboxMobileRemReqstList();
                    checkTO_COS_ACC();

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    protected void lnkFuelDetails_Click(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnRemid.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
        //   hdnClaimsID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
        //Response.Redirect("TravelRequest.aspx?tripid=" + hdnTrip_Id.Value);
        Response.Redirect("~/procs/Payment_Req_Arch.aspx?rem_id=" + hdnRemid.Value + "&mngexp=0&inbtype=" + hdninboxtype.Value);
    }

    #endregion

    #region PageMethods
    private void InboxMobileRemReqstList()
    {
        try
        {
            // DataSet dsList = new DataSet();
            DataTable dtleaveInbox = new DataTable();
            dtleaveInbox = spm.GetMobileInbox(hdnEmpCode.Value);

            if (dtleaveInbox.Rows.Count > 0)
            {
                gvMngTravelRqstList.DataSource = dtleaveInbox;
                gvMngTravelRqstList.DataBind();
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void InboxMobileRemReqstList_ACC()
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Approved_Mobile_claims";
            //spars[0].Value = "get_Approved_Payment_claims";
            
            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = hdnEmpCode.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");
            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                gvMngTravelRqstList.DataSource = dsTrDetails;
                gvMngTravelRqstList.DataBind();
                 
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void checkTO_COS_ACC()
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_Check_COS_ACC_apprver_code_byType";

            spars[1] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[1].Value = hdninboxtype.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = hdnEmpCode.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");
            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                InboxTravelReqstList_TDCOSACC();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }


    protected void InboxTravelReqstList_TDCOSACC()
    {
        try
        {
            DataTable dsTrDetails = new DataTable();
            var start1 = "";
            var end1 = "";
            if (Convert.ToString(txtDate.Text).Trim() != "")
            {
                if (Convert.ToString(txtToDate.Text).Trim() != "")
                {
                    //dtpStartDate and dtpEndDate are my DateTimePickers
                    var start = DateTime.ParseExact(txtDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    start1 = start.ToString("yyyy-MM-dd");
                    var end = DateTime.ParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    end1 = end.ToString("yyyy-MM-dd");

                }
                else
                {
                    lblmessage.Text = "Please select To date";
                }

            }
            else
            {
                var start = DateTime.Now.Date.AddYears(-10);
                start1 = start.ToString("yyyy-MM-dd");
                var end = DateTime.Now.Date;
                end1 = end.ToString("yyyy-MM-dd");
            }


            dsTrDetails = spm.GetInboxServiceRequestRCH(Convert.ToString(hdnEmpCode.Value), start1, end1);
            //Travel Request Count
            gvMngTravelRqstList.DataSource = null;
            gvMngTravelRqstList.DataBind();
            if (dsTrDetails.Rows.Count > 0)
            {
                string strFilter = "";
                DataTable dtfilter = new DataTable();
                if (txtDate.Text != "" || txtToDate.Text != "" || txtApplicantName.Text != "")
                {

                    if (Convert.ToString(txtApplicantName.Text).Trim() != "")
                    {
                        var splitval = txtApplicantName.Text.Trim().Split('-')[1];
                        strFilter = " ( EmployeeName like'%" + Convert.ToString(splitval).Trim() + "%' or EmployeeCode like'%" + Convert.ToString(txtApplicantName.Text).Trim() + "%')";
                        dsTrDetails.DefaultView.RowFilter = strFilter;
                    }
                }
                gvMngTravelRqstList.DataSource = dsTrDetails;
                gvMngTravelRqstList.DataBind();

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    #endregion
    protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMngTravelRqstList.PageIndex = e.NewPageIndex;
        this.InboxTravelReqstList_TDCOSACC();

    }


    protected void claimmob_btnSubmit_Click(object sender, EventArgs e)
    {
        InboxTravelReqstList_TDCOSACC();
    }

    [System.Web.Services.WebMethod]
    public static List<string> SearchEmployees_M(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";

                strsql = "  Select t.empname from  ( " +
                               "  Select Emp_Code + ' - '  +Emp_Name as empname " +
                               "  from tbl_Employee_Mst  " +
                               "   where emp_status='Onboard' " +
                               "    " +
                               " ) t " +
                               " where t.empname like '%' + @SearchText + '%'   Order by t.empname ";

                cmd.CommandText = strsql;
                cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));

                ////                }


                cmd.Connection = conn;
                conn.Open();
                List<string> employees = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        employees.Add(sdr["empname"].ToString());
                    }
                }
                conn.Close();
                return employees;
            }
        }
    }

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnRemid.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
        //   hdnClaimsID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
        //Response.Redirect("TravelRequest.aspx?tripid=" + hdnTrip_Id.Value);
        Response.Redirect("~/procs/Service_Req_App.aspx?id=" + hdnRemid.Value + "&type=arr");
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {

    }

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {

    }
}