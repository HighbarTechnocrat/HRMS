using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ABAP_Object_Tracker_Plan_View : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();

    #region Creative_Default_methods
    
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    public DataTable dtEmp;
    public DataSet dsDirecttaxSectionList = new DataSet();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
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

            lblmessage.Text = "";

            txtEmpCode.Text = Convert.ToString(Session["Empcode"]);

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/ABAP_Object_Tracker_Index.aspx");
            }
            else
            {
                Page.SmartNavigation = true;

                if (!Page.IsPostBack)
                {
                    getProjectLocation();
                    PopulateEmployeeData();
                    DDLProjectLocation.Enabled = false;
                    if (Request.QueryString.Count > 0)
                    {
                        hdnABAPODUploadId.Value = Convert.ToString(Request.QueryString["ABAPODId"]).Trim();
                        get_ABAP_Object_Submitted_Plan_Details_View();
                    }

                }

            }

        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
    #endregion

    #region PageMethods
    public void PopulateEmployeeData()
    {
        try
        {
            dtEmp = spm.GetEmployeeData(txtEmpCode.Text);
            if (dtEmp.Rows.Count > 0)
            {
                lpm.Emp_Code = txtEmpCode.Text;
                lpm.Emp_Status = (string)dtEmp.Rows[0]["Emp_status"];
                lpm.Emp_Name = (string)dtEmp.Rows[0]["Emp_Name"];
                lpm.Designation_name = (string)dtEmp.Rows[0]["DesginationName"];
                lpm.department_name = (string)dtEmp.Rows[0]["Department_Name"];
                lpm.Grade = Convert.ToString(dtEmp.Rows[0]["Grade"]).Trim();
                lpm.EmailAddress = (string)dtEmp.Rows[0]["Emp_EmailAddress"];

                hflGrade.Value = lpm.Grade;
                hflEmailAddress.Value = lpm.EmailAddress;
                hflEmpName.Value = lpm.Emp_Name;
                hflEmpDepartment.Value = lpm.department_name;
                hflEmpDesignation.Value = lpm.Designation_name;

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }
    #endregion
    
    #region ABAP Object Submitted Plan 
    private void getProjectLocation()
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetDropDownprojectLocation";

            DataSet DS = spm.getDatasetList(spars, "SP_TASK_M_DETAILS");
            DDLProjectLocation.DataSource = DS.Tables[0];
            DDLProjectLocation.DataTextField = "Location_name";
            DDLProjectLocation.DataValueField = "comp_code";
            DDLProjectLocation.DataBind();
            DDLProjectLocation.Items.Insert(0, new ListItem("Select Project Location", "0"));
        }
        catch (Exception ex)
        {

        }
    }

    public void get_ABAP_Object_Submitted_Plan_Details_View()
    {

        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "getABAPObjectTrackerApprovedDetails";

        spars[1] = new SqlParameter("@ABAPODUploadId", SqlDbType.VarChar);
        spars[1].Value = Convert.ToInt32(hdnABAPODUploadId.Value);

        spars[2] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
        spars[2].Value = (Session["Empcode"]).ToString().Trim();

        dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
        {
            DDLProjectLocation.SelectedValue = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ProjectLocation"].ToString().Trim();
            if (dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["UserRole"].ToString() == "PM" || dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["UserRole"].ToString() == "PRM")
            {
                licolortext.Visible = true;
                gvDetailPlan.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvDetailPlan.DataBind();

                gvRGSDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvRGSDetails.DataBind();
                gvRGSDetails.Visible = false;

                gvFSDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvFSDetails.DataBind();
                gvFSDetails.Visible = false;

                gvABAPDevDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvABAPDevDetails.DataBind();
                gvABAPDevDetails.Visible = false;

                gvHBTTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvHBTTestDetails.DataBind();
                gvHBTTestDetails.Visible = false;

                gvCTMTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvCTMTestDetails.DataBind();
                gvCTMTestDetails.Visible = false;

                gvUATSignOffDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvUATSignOffDetails.DataBind();
                gvUATSignOffDetails.Visible = false;

                gvGoLiveDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvGoLiveDetails.DataBind();
                gvGoLiveDetails.Visible = false;

                txtPRM.Text = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ProjectManager"].ToString().Trim();

                DgvApprover.DataSource = dsABAPObjectPlanSubmitted.Tables[2];
                DgvApprover.DataBind();
            }
            else if (dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["UserRole"].ToString() == "Consultant")
            {
                licolortext.Visible = false;
                rgs.Visible = false;
                fs.Visible = false;
                liabapdev.Visible = false;
                hbttest.Visible = false;
                ctmtesting.Visible = false;
                uatsignoff.Visible = false;
                golive.Visible = false;

                Span2.Visible = false;
                if (dsABAPObjectPlanSubmitted.Tables[1].Rows.Count > 0)
                {
                    rgs.Visible = true;
                    gvRGSDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[1];
                    gvRGSDetails.DataBind();
                    //gvRGSDetails.Visible = false;
                    btnRGS_Details.Text = "-";
                    btnABAPDev_Details.Visible = false;
                    abapdev.Visible = false;
                    liabapdev.Visible = false;
                }

                if (dsABAPObjectPlanSubmitted.Tables[2].Rows.Count > 0)
                {
                    fs.Visible = true;
                    gvFSDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[2];
                    gvFSDetails.DataBind();
                    gvFSDetails.Visible = false;
                    btnABAPDev_Details.Visible = false;
                    abapdev.Visible = false;
                    liabapdev.Visible = false;
                }

                if (dsABAPObjectPlanSubmitted.Tables[3].Rows.Count > 0)
                {
                    hbttest.Visible = true;
                    gvHBTTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[3];
                    gvHBTTestDetails.DataBind();
                    gvHBTTestDetails.Visible = false;
                    btnABAPDev_Details.Visible = false;
                    abapdev.Visible = false;
                    liabapdev.Visible = false;
                }

                if (dsABAPObjectPlanSubmitted.Tables[4].Rows.Count > 0)
                {
                    liabapdev.Visible = true;
                    gvABAPDevDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[4];
                    gvABAPDevDetails.DataBind();
                    btnABAPDev_Details.Text = "-";
                    //gvABAPDevDetails.Visible = false;

                    btnRGS_Details.Visible = false;
                    btnFS_Details.Visible = false;
                    btnHBT_Details.Visible = false;
                    btnCTM_Details.Visible = false;
                    btnUATSignOff_Details.Visible = false;
                }

                if (dsABAPObjectPlanSubmitted.Tables[5].Rows.Count > 0)
                {
                    ctmtesting.Visible = true;
                    gvCTMTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[5];
                    gvCTMTestDetails.DataBind();
                    gvCTMTestDetails.Visible = false;
                    btnABAPDev_Details.Visible = false;
                    abapdev.Visible = false;
                    liabapdev.Visible = false;
                }
                liuatsignoff.Visible = false;
                ligolive.Visible = false;
                btnUATSignOff_Details.Visible = false;
                btnGoLive_Details.Visible = false;

                txtPRM.Text = dsABAPObjectPlanSubmitted.Tables[6].Rows[0]["ProjectManager"].ToString().Trim();

            }
        }
    }    
    protected void RGS_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("RGSchkSelect");
            string empName = DataBinder.Eval(e.Row.DataItem, "RGSConsultant").ToString();

            if (!string.IsNullOrEmpty(empName))
            {
                chkSelect.Enabled = false;
            }
            else
            {
                chkSelect.Enabled = true;
            }
        }
    }
    protected void FS_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("FSchkSelect");
            string empName = DataBinder.Eval(e.Row.DataItem, "FSConsultant").ToString();

            if (!string.IsNullOrEmpty(empName))
            {
                chkSelect.Enabled = false;
            }
            else
            {
                chkSelect.Enabled = true;
            }
        }
    }
    protected void ABAPDev_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("ABAPDevchkSelect");
            string empName = DataBinder.Eval(e.Row.DataItem, "ABAPDevConsultant").ToString();

            if (!string.IsNullOrEmpty(empName))
            {
                chkSelect.Enabled = false;
            }
            else
            {
                chkSelect.Enabled = true;
            }
        }
    }
    protected void HBT_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("HBTchkSelect");
            string empName = DataBinder.Eval(e.Row.DataItem, "HBTConsultant").ToString();

            if (!string.IsNullOrEmpty(empName))
            {
                chkSelect.Enabled = false;
            }
            else
            {
                chkSelect.Enabled = true;
            }
        }
    }
    protected void CTM_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("CTMchkSelect");
            string empName = DataBinder.Eval(e.Row.DataItem, "CTMConsultant").ToString();

            if (!string.IsNullOrEmpty(empName))
            {
                chkSelect.Enabled = false;
            }
            else
            {
                chkSelect.Enabled = true;
            }
        }
    }
    protected void UAT_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("UATchkSelect");
            string empName = DataBinder.Eval(e.Row.DataItem, "UATConsultant").ToString();

            if (!string.IsNullOrEmpty(empName))
            {
                chkSelect.Enabled = false;
            }
            else
            {
                chkSelect.Enabled = true;
            }
        }
    }
    protected void ABAP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
            string empName = DataBinder.Eval(e.Row.DataItem, "RGSConsultant").ToString();

            if (!string.IsNullOrEmpty(empName))
            {
                chkSelect.Enabled = false;
            }
            else
            {
                chkSelect.Enabled = true;
            }
        }
    }
    protected void btnABAPPlanSubmit_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvRGSDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string secondColumnValue = row.Cells[1].Text.Trim();
                if (string.IsNullOrEmpty(secondColumnValue))
                {
                    lblmessage.Text = "Consultant value cannot be empty.";
                    lblmessage.Visible = true;
                }
            }

        }

        foreach (GridViewRow row in gvFSDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string secondColumnValue = row.Cells[1].Text.Trim();
                if (string.IsNullOrEmpty(secondColumnValue))
                {
                    lblmessage.Text = "Consultant value cannot be empty.";
                    lblmessage.Visible = true;
                }
            }

        }


        foreach (GridViewRow row in gvRGSDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string secondColumnValue = row.Cells[1].Text.Trim();
                if (string.IsNullOrEmpty(secondColumnValue))
                {
                    lblmessage.Text = "Error: Second column value cannot be empty.";
                    lblmessage.Visible = true;
                }
            }

        }

        foreach (GridViewRow row in gvRGSDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string secondColumnValue = row.Cells[1].Text.Trim();
                if (string.IsNullOrEmpty(secondColumnValue))
                {
                    lblmessage.Text = "Error: Second column value cannot be empty.";
                    lblmessage.Visible = true;
                }
            }

        }
    }
    protected void btnViewABAPObjectDetailPlan_Click(object sender, EventArgs e)
    {
        Session["ddlProjectLoc"] = DDLProjectLocation.SelectedValue;
        Response.Redirect("ABAP_Object_Tracker_View_Detail_Plan.aspx?ABAPODId=" + hdnABAPODUploadId.Value);
    }
    protected void btnRGS_Details_Click(object sender, EventArgs e)
    {


        gvDetailPlan.Visible = true;
        if (gvRGSDetails.Visible)
        {
            gvRGSDetails.Visible = false;
            btnRGS_Details.Text = "+";
        }
        else
        {
            btnRGS_Details.Text = "-";
            gvRGSDetails.Visible = true;
        }
    }
    protected void btnFS_Details_Click(object sender, EventArgs e)
    {
        gvDetailPlan.Visible = true;
        if (gvFSDetails.Visible)
        {
            gvFSDetails.Visible = false;
            btnFS_Details.Text = "+";
        }
        else
        {
            gvFSDetails.Visible = true;
            btnFS_Details.Text = "-";

        }
    }
    protected void btnABAPDev_Details_Click(object sender, EventArgs e)
    {
        gvDetailPlan.Visible = true;
        if (gvABAPDevDetails.Visible)
        {
            gvABAPDevDetails.Visible = false;
            btnABAPDev_Details.Text = "+";
        }
        else
        {
            gvABAPDevDetails.Visible = true;
            btnABAPDev_Details.Text = "-";

        }
    }
    protected void btnHBT_Details_Click(object sender, EventArgs e)
    {
        gvDetailPlan.Visible = true;
        if (gvHBTTestDetails.Visible)
        {
            gvHBTTestDetails.Visible = false;
            btnHBT_Details.Text = "+";
        }
        else
        {
            gvHBTTestDetails.Visible = true;
            btnHBT_Details.Text = "-";
        }
    }
    protected void btnCTM_Details_Click(object sender, EventArgs e)
    {
        gvDetailPlan.Visible = true;
        if (gvCTMTestDetails.Visible)
        {
            gvCTMTestDetails.Visible = false;
            btnCTM_Details.Text = "+";
        }
        else
        {
            gvCTMTestDetails.Visible = true;
            btnCTM_Details.Text = "-";

        }
    }
    protected void btnUATSignOff_Details_Click(object sender, EventArgs e)
    {
        gvDetailPlan.Visible = true;
        if (gvUATSignOffDetails.Visible)
        {
            gvUATSignOffDetails.Visible = false;
            btnUATSignOff_Details.Text = "+";
        }
        else
        {
            gvUATSignOffDetails.Visible = true;
            btnUATSignOff_Details.Text = "-";
        }
    }
    protected void btnGoLive_Details_Click(object sender, EventArgs e)
    {
        gvDetailPlan.Visible = true;
        if (gvGoLiveDetails.Visible)
        {
            gvGoLiveDetails.Visible = false;
            btnGoLive_Details.Text = "+";
        }
        else
        {
            gvGoLiveDetails.Visible = true;
            btnGoLive_Details.Text = "-";
        }
    }
    protected void btnAllDetails_Click(object sender, EventArgs e)
    {


        bool allVisible = lirgsdetails.Visible &&
                       lifsdetails.Visible &&
                       liabapdev.Visible &&
                       lihbttest.Visible &&
                       lictmtest.Visible &&
                       liuatsignoff.Visible &&
                       ligolive.Visible;
        if (allVisible)
        {
            btnAllDetails.Text = "CLICK TO SEE STAGE-WISE DETAILS";
            colorshades.Visible = false;
            lirgsdetails.Visible = false;
            lifsdetails.Visible = false;
            liabapdev.Visible = false;
            lihbttest.Visible = false;
            lictmtest.Visible = false;
            liuatsignoff.Visible = false;
            ligolive.Visible = false;

        }
        else
        {
            btnAllDetails.Text = "CLICK TO SEE STAGE-WISE DETAILS";
            colorshades.Visible = true;
            lirgsdetails.Visible = true;
            lifsdetails.Visible = true;
            liabapdev.Visible = true;
            lihbttest.Visible = true;
            lictmtest.Visible = true;
            liuatsignoff.Visible = true;
            ligolive.Visible = true;

        }

        return;
    }
    protected void gvRGSDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string status = DataBinder.Eval(e.Row.DataItem, "RGSStatusName").ToString();
            switch (status)
            {
                case "Hold":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF4500");  // OrangeRed
                    e.Row.ForeColor = System.Drawing.Color.White;
                    break;

                case "Started":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFA500");  // Orange
                    e.Row.ForeColor = System.Drawing.Color.White;
                    break;

                case "Submitted":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#90EE90");  // LightGreen
                    e.Row.ForeColor = System.Drawing.Color.Black; // Ensure the text is legible
                    break;

                case "Delayed":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#CD5C5C");  // IndianRed
                    e.Row.ForeColor = System.Drawing.Color.White;
                    break;

                case "Send For Approval":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FEFE97");  // Yellow
                    e.Row.ForeColor = System.Drawing.Color.Black;  // Black text for better readability
                    break;

                case "Approved":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#4CAF50");  // Green
                    e.Row.ForeColor = System.Drawing.Color.White;  // White text for better contrast
                    break;
                    
                case "Submit For Functional Testing":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ADD8E6");  // LightBlue
                    e.Row.ForeColor = System.Drawing.Color.Black;  // Black text for better contrast
                    break;

                case "Passed":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#3CB371");  // MediumSeaGreen
                    e.Row.ForeColor = System.Drawing.Color.White;  // White text for contrast
                    break;

                case "Failed":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");  // Red
                    e.Row.ForeColor = System.Drawing.Color.White;  // White text for contrast
                    break;

                case "Accept":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#006400");  // DarkGreen
                    e.Row.ForeColor = System.Drawing.Color.White;  // White text for contrast
                    break;

                case "Reject":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#8B0000");  // DarkRed
                    e.Row.ForeColor = System.Drawing.Color.White;  // White text for contrast
                    break;

                case "Go Live":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#4169E1");  // RoyalBlue
                    e.Row.ForeColor = System.Drawing.Color.White;  // White text for contrast
                    break;

                default:
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");  // White
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    break;
            }


        }
    }
    protected void gvFSDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string status = DataBinder.Eval(e.Row.DataItem, "FSStatusName").ToString();
            switch (status)
            {
                case "Hold":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF4500");  // OrangeRed
                    e.Row.ForeColor = System.Drawing.Color.White;
                    break;

                case "Started":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFA500");  // Orange
                    e.Row.ForeColor = System.Drawing.Color.White;
                    break;

                case "Submitted":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#90EE90");  // LightGreen
                    e.Row.ForeColor = System.Drawing.Color.Black; // Ensure the text is legible
                    break;

                case "Delayed":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#CD5C5C");  // IndianRed
                    e.Row.ForeColor = System.Drawing.Color.White;
                    break;

                case "Send For Approval":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FEFE97");  // Yellow
                    e.Row.ForeColor = System.Drawing.Color.Black;  // Black text for better readability
                    break;

                case "Approved":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#4CAF50");  // Green
                    e.Row.ForeColor = System.Drawing.Color.White;  // White text for better contrast
                    break;

                case "Submit For Functional Testing":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ADD8E6");  // LightBlue
                    e.Row.ForeColor = System.Drawing.Color.Black;  // Black text for better contrast
                    break;

                case "Passed":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#3CB371");  // MediumSeaGreen
                    e.Row.ForeColor = System.Drawing.Color.White;  // White text for contrast
                    break;

                case "Failed":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");  // Red
                    e.Row.ForeColor = System.Drawing.Color.White;  // White text for contrast
                    break;

                case "Accept":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#006400");  // DarkGreen
                    e.Row.ForeColor = System.Drawing.Color.White;  // White text for contrast
                    break;

                case "Reject":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#8B0000");  // DarkRed
                    e.Row.ForeColor = System.Drawing.Color.White;  // White text for contrast
                    break;

                case "Go Live":
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#4169E1");  // RoyalBlue
                    e.Row.ForeColor = System.Drawing.Color.White;  // White text for contrast
                    break;

                default:
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");  // White
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    break;
            }


        }
    }
    #endregion

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        // Remove paging if applicable
        gvDetailPlan.AllowPaging = false;

        // Rebind data if needed
        // BindGrid(); // Uncomment if needed to reload data

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=DetailPlanExport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                // Hide buttons or controls not to export
                gvDetailPlan.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    // Required override for exporting controls
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms the GridView was rendered
    }

}