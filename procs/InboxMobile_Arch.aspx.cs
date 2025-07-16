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

public partial class myaccount_InboxMobileReimbursement_ACC : System.Web.UI.Page
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
                    GetgetTravelVoucherEmployee();
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
        Response.Redirect("~/procs/Mobile_Req_Arch.aspx?rem_id=" + hdnRemid.Value + "&mngexp=0&inbtype=" + hdninboxtype.Value);
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
           DataSet dsTrDetails = new DataSet();           
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Approved_Mobile_claims";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = hdnEmpCode.Value;

            spars[2] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[2].Value = hdninboxtype.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");
            //Travel Request Count
           gvMngTravelRqstList.DataSource = null;
            gvMngTravelRqstList.DataBind();
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                string strFilter = "";
                if (txtDate.Text != "" || DDL_txtApplicantName.SelectedItem.Text != "")
                {
                    //strFilter = "Rem_Month='" + txtDate.Text + "' and ( Applicant like'%" + Convert.ToString(txtApplicantName.Text).Trim() + "%' or empCode like'%" + Convert.ToString(txtApplicantName.Text).Trim() + "%')";
                    if (Convert.ToString(txtDate.Text).Trim() != "")
                    {
                        strFilter = "Rem_Month='" + txtDate.Text + "'";
                    }
                    if (Convert.ToString(DDL_txtApplicantName.SelectedItem.Text).Trim() == "Select  Employee")
                    {
                        
                    }
                    else
                    {
                        String[] stremp;
                        stremp = Convert.ToString(DDL_txtApplicantName.SelectedItem.Text).Split('-');
                        if (Convert.ToString(txtDate.Text).Trim() != "")
                        {
                            strFilter = "Rem_Month='" + txtDate.Text + "' and ( Applicant like'%" + Convert.ToString(stremp[1]).Trim() + "%' or empCode like'%" + Convert.ToString(stremp[0]).Trim() + "%')";
                        }
                        else
                        {
                            strFilter = " ( Applicant like'%" + Convert.ToString(stremp[1]).Trim() + "%' or empCode like'%" + Convert.ToString(stremp[0]).Trim() + "%')";
                        }
                    }
                    DataRow[] foundRows;
                    foundRows = dsTrDetails.Tables[0].Select(strFilter);
                    if (foundRows.Length >= 1)
                    {
                        DataTable dt = foundRows.CopyToDataTable();
                        gvMngTravelRqstList.DataSource = dt;
                        gvMngTravelRqstList.DataBind();
                    }
                }
                else
                {
                    gvMngTravelRqstList.DataSource = dsTrDetails.Tables[0];
                    gvMngTravelRqstList.DataBind();
                }
                //gvMngTravelRqstList.DataSource = dsTrDetails.Tables[0];
                //gvMngTravelRqstList.DataBind();

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    #endregion


    public void GetgetTravelVoucherEmployee()
    {
        DataTable lstPosition = new DataTable();
        lstPosition = spm.getTravelVoucherEmployee_List();
        if (lstPosition.Rows.Count > 0)
        {
            DDL_txtApplicantName.DataSource = lstPosition;
            DDL_txtApplicantName.DataTextField = "empname";
            DDL_txtApplicantName.DataValueField = "Emp_Code";
            DDL_txtApplicantName.DataBind();
            DDL_txtApplicantName.Items.Insert(0, new ListItem("Select  Employee", "0"));
        }
    }

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

                ////                if (grade == "UC" || grade == "DIR")
                ////                {

                ////                    strsql = @" Select t.empname from  ( Select Distinct c.Emp_Code, Emp_Name + ' - '  + c.Emp_Code as empname 	From tbl_Employee_OMStructure a inner join tbl_Employee_Mst c on a.EMP_CODE=c.Emp_Code 
                ////	                    where A_EMP_CODE in (select Distinct em.Emp_Code 	from tbl_Employee_OMStructure om inner join tbl_Employee_Mst em on om.EMP_CODE=em.Emp_Code 
                ////	                    where  em.emp_status='Onboard' and om.EMP_CODE != @empcode  ) )t  where t.empname like '%' + @SearchText + '%'   Order by t.empname ";

                ////                    cmd.CommandText = strsql;
                ////                    cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                ////                    cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));
                ////                }
                ////                else
                ////                {
                strsql = "  Select t.empname from  ( " +
                    // "  Select Emp_Name + ' - '  +Emp_Code as empname " +
                          "  Select Emp_Name as empname " +
                           "  from tbl_Employee_Mst  " +
                           "   where emp_status='Onboard' " +
                           "   and emp_location in (select Distinct location from tbl_htravel_leave_extra_approver where approver_emp_code like '%'+ @empcode  +'%' and approver_type in ('RACC','ACC')) " +
//" and Emp_Code in (select Distinct Emp_Code from Reimb_Emp_Fuel_elig_matrix) " +
              " ) t " +
                           "   where t.empname like '%' + @SearchText + '%'   Order by t.empname ";

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
}