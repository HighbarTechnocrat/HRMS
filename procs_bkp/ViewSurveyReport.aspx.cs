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

public partial class ViewSurveyReport : System.Web.UI.Page
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/customerFirst");
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
                    // checkTO_COS_ACC();
                    getdepartmentdetails();

                    DataTable dtHOD = new DataTable();
                    dtHOD = spm.CheckCustomerFIRSTHOD(Convert.ToString(hdnEmpCode.Value));
                    if (dtHOD.Rows.Count > 0)
                    {
                        ddlDepartment.DataSource = null;
                        ddlDepartment.DataBind();
                        ddlDepartment.DataSource = dtHOD;
                        ddlDepartment.DataTextField = "DeptName";
                        ddlDepartment.DataValueField = "DeptId";
                        ddlDepartment.DataBind();
                        ddlDepartment.Items.Insert(0, new ListItem("ALL", "0")); //updated code added by manisha 15/03/2021
                    }

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
            var empcode = "";
            if (Convert.ToString(txtDate.Text).Trim() != "")
            {
                if (Convert.ToString(txtToDate.Text).Trim() != "")
                {
                    //dtpStartDate and dtpEndDate are my DateTimePickers
                    var start = DateTime.ParseExact(txtDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    start1  = start.ToString("yyyy-MM-dd");
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
            int? departmentId =null;
            string surveyNo = null;
            string responseStatus = null;
            if(ddlDepartment.SelectedValue!="0")
            {
                departmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
            }
            else
            {
                departmentId = null;// added else condition by manisha on 15/03/2021
            }
            if (txtSurveyNumber.Text.ToString().Trim() != "")
            {
                surveyNo = txtSurveyNumber.Text.ToString().Trim();
            }
            if (txtSurveyNumber.Text.ToString().Trim() != "")
            {
                surveyNo = txtSurveyNumber.Text.ToString().Trim();
            }
            responseStatus = ddlResponseStatus.SelectedValue.ToString();
            DataTable dtCEOEMPCODE = new DataTable();
            dtCEOEMPCODE = spm.GetCEOEmpCode();
            if (dtCEOEMPCODE.Rows.Count > 0)
            {
                var loginCode = Convert.ToString(hdnEmpCode.Value);
                var CeoEmpCode = Convert.ToString(dtCEOEMPCODE.Rows[0]["Emp_Code"]);
                if (loginCode == CeoEmpCode)
                {
                    dsTrDetails = spm.GetViewSurveyReport(null, surveyNo, start1, end1, departmentId, responseStatus);
                }
                else
                {
                    dsTrDetails = spm.GetViewSurveyReport(Convert.ToString(hdnEmpCode.Value), surveyNo, start1, end1, departmentId, responseStatus);
                }
            }
            else
            {
                dsTrDetails = spm.GetViewSurveyReport(Convert.ToString(hdnEmpCode.Value), surveyNo, start1, end1, departmentId, responseStatus);
            }
            gvMngTravelRqstList.DataSource = null;
            gvMngTravelRqstList.DataBind();
            if (dsTrDetails.Rows.Count > 0)
            {
                string strFilter = "";
                DataTable dtfilter = new DataTable();
                gvMngTravelRqstList.DataSource = dsTrDetails;
                gvMngTravelRqstList.DataBind();

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    private void getdepartmentdetails()
    {
        try
        {
            ddlDepartment.DataSource = null;          
            ddlDepartment.DataBind();
            var departmnt = new DataTable();
            departmnt = spm.GetDepartmentDetailsForCF();
            if(departmnt.Rows.Count>0)
            {
                ddlDepartment.DataSource = departmnt;
                ddlDepartment.DataTextField = "DeptName";
                ddlDepartment.DataValueField = "DeptId";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("All", "0")); //updated code
            }
            else
            {
                //ddlDepartment.Items.Insert(0, new ListItem("Select Business Unit", "0")); //updated code commented by Manisha-15/03/2021 and added below line
                ddlDepartment.Items.Insert(0, new ListItem("Select Business Unit", "0")); //updated code
            }
            
        }
        catch (Exception)
        {

            throw;
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
    public static List<string> SearchSurveyId(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";


                strsql = "  select distinct SurveyNo from tbl_cf_CreateSyrvey WHERE SurveyNo LIKE '%' + @SearchText + '%'   Order by SurveyNo ";

                cmd.CommandText = strsql;
                cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                //}


                cmd.Connection = conn;
                conn.Open();
                List<string> SurveyNo = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        SurveyNo.Add(sdr["SurveyNo"].ToString());
                    }
                }
                conn.Close();
                return SurveyNo;
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
        Response.Redirect("~/procs/CustomerFirst_App.aspx?id=" + hdnRemid.Value + "&type=arr");
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {

    }

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {

    }
}