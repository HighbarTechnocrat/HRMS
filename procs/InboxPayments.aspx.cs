using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class InboxPayments : System.Web.UI.Page
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
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void lnkcont_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/Reembursementindex");
    }
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
                    GetgetTravelVoucherEmployee();
                    //PopulateEmployeeLeaveData();
                    InboxMobileRemReqstList();
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

    public void GetgetTravelVoucherEmployee()
    {

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_getPaymentSearchEmployeeName";
        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCode.Value;
        spars[2] = new SqlParameter("@trappr", SqlDbType.VarChar);
        spars[2].Value = hdninboxtype.Value;

        dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            DDL_txtApplicantName.DataSource = dsTrDetails.Tables[0];
            DDL_txtApplicantName.DataTextField = "empname";
            DDL_txtApplicantName.DataValueField = "Emp_Code";
            DDL_txtApplicantName.DataBind();
            DDL_txtApplicantName.Items.Insert(0, new ListItem("Select  Employee", "0"));
        }

        //DataTable lstPosition = new DataTable();
        //lstPosition = spm.getTravelVoucherEmployee_List();
        //if (lstPosition.Rows.Count > 0)
        //{
        //    DDL_txtApplicantName.DataSource = lstPosition;
        //    DDL_txtApplicantName.DataTextField = "empname";
        //    DDL_txtApplicantName.DataValueField = "Emp_Code";
        //    DDL_txtApplicantName.DataBind();
        //    DDL_txtApplicantName.Items.Insert(0, new ListItem("Select  Employee", "0"));
        //}
    }
    protected void lnkFuelDetails_Click(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnRemid.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
        //   hdnClaimsID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
        //Response.Redirect("TravelRequest.aspx?tripid=" + hdnTrip_Id.Value);
        Response.Redirect("~/procs/Payment_Req_App.aspx?rem_id=" + hdnRemid.Value + "&mngexp=0&inbtype=" + hdninboxtype.Value);
    }

    #endregion

    #region PageMethods
    private void InboxMobileRemReqstList()
    {
        try
        {
            HdnFTypeCheck.Value = "1";
            // DataSet dsList = new DataSet();
            DataTable dtleaveInbox = new DataTable();
            dtleaveInbox = spm.GetPaymentVoucherInbox(hdnEmpCode.Value);

            if (dtleaveInbox.Rows.Count > 0)
            {
                string strFilter = "";
                if (txtDate.Text != "" || DDL_txtApplicantName.SelectedItem.Text != "")
                {
                    String[] stremp;
                    stremp = Convert.ToString(DDL_txtApplicantName.SelectedItem.Text).Split('-');
                    //strFilter = "Rem_Month='" + txtDate.Text + "' and ( Applicant like'%" + Convert.ToString(txtApplicantName.Text).Trim() + "%' or empCode like'%" + Convert.ToString(txtApplicantName.Text).Trim() + "%')";
                    if (Convert.ToString(txtDate.Text).Trim() != "")
                    {
                        strFilter = "Rem_Month='" + txtDate.Text + "'";
                    }
                    if (Convert.ToString(DDL_txtApplicantName.SelectedItem.Text).Trim() != "")
                    {
                        if (Convert.ToString(txtDate.Text).Trim() != "")
                        {
                            if (DDL_txtApplicantName.SelectedItem.Text == "Select  Employee")
                            {
                                strFilter = "Rem_Month='" + txtDate.Text + "'";
                            }
                            else
                            {
                                strFilter = "Rem_Month='" + txtDate.Text + "' and ( Applicant like'%" + Convert.ToString(stremp[1]).Trim() + "%' or empCode like'%" + Convert.ToString(stremp[0]).Trim() + "%')";
                            }
                        }
                        else
                        {
                            if (DDL_txtApplicantName.SelectedItem.Text == "Select  Employee")
                            {
                                // strFilter = "Rem_Month='" + txtDate.Text + "'";
                            }
                            else
                            {
                                strFilter = " ( Applicant like'%" + Convert.ToString(stremp[1]).Trim() + "%' or empCode like'%" + Convert.ToString(stremp[0]).Trim() + "%')";
                            }
                        }
                    }
                    DataRow[] foundRows;
                    foundRows = dtleaveInbox.Select(strFilter);
                    if (foundRows.Length >= 1)
                    {
                        DataTable dt = foundRows.CopyToDataTable();
                        gvMngTravelRqstList.DataSource = dt;
                        gvMngTravelRqstList.DataBind();
                    }
                }
                else
                {
                    gvMngTravelRqstList.DataSource = dtleaveInbox;
                    gvMngTravelRqstList.DataBind();
                }

                //if (gvMngTravelRqstList.Columns.Count > 1)
                //{
                //    gvMngTravelRqstList.Columns[5].Visible = false;
                //}
            }

        }
        catch (Exception ex)
        {

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
            HdnFTypeCheck.Value = "2";
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_COS_ACC_PendingLst_claim_Payment";

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
                    String[] stremp;
                    stremp = Convert.ToString(DDL_txtApplicantName.SelectedItem.Text).Split('-');
                    //strFilter = "Rem_Month='" + txtDate.Text + "' and ( Applicant like'%" + Convert.ToString(txtApplicantName.Text).Trim() + "%' or empCode like'%" + Convert.ToString(txtApplicantName.Text).Trim() + "%')";
                    if (Convert.ToString(txtDate.Text).Trim() != "")
                    {
                        strFilter = "Rem_Month='" + txtDate.Text + "'";
                    }
                    if (Convert.ToString(DDL_txtApplicantName.SelectedItem.Text).Trim() != "")
                    {
                        if (Convert.ToString(txtDate.Text).Trim() != "")
                        {
                            if (DDL_txtApplicantName.SelectedItem.Text == "Select  Employee")
                            {
                                strFilter = "Rem_Month='" + txtDate.Text + "'";
                            }
                            else
                            {
                                strFilter = "Rem_Month='" + txtDate.Text + "' and ( Applicant like'%" + Convert.ToString(stremp[1]).Trim() + "%' or empCode like'%" + Convert.ToString(stremp[0]).Trim() + "%')";
                            }
                        }
                        else
                        {
                            if (DDL_txtApplicantName.SelectedItem.Text == "Select  Employee")
                            {
                                // strFilter = "Rem_Month='" + txtDate.Text + "'";
                            }
                            else
                            {
                                strFilter = " ( Applicant like'%" + Convert.ToString(stremp[1]).Trim() + "%' or empCode like'%" + Convert.ToString(stremp[0]).Trim() + "%')";
                            }
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


    protected void gvMngTravelRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMngTravelRqstList.PageIndex = e.NewPageIndex;
        if (HdnFTypeCheck.Value == "2")
        {
            this.InboxTravelReqstList_TDCOSACC();
        }
        else
        {
            InboxMobileRemReqstList();
        }
    }

    protected void claimmob_btnSubmit_Click(object sender, EventArgs e)
    {
        if (HdnFTypeCheck.Value == "2")
        {
            this.InboxTravelReqstList_TDCOSACC();
        }
        else
        {
            InboxMobileRemReqstList();
        }
       
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        GetgetTravelVoucherEmployee();
        txtDate.Text = "";
        if (HdnFTypeCheck.Value == "2")
        {
            this.InboxTravelReqstList_TDCOSACC();
        }
        else
        {
            InboxMobileRemReqstList();
        }

    }
}
