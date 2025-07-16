using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Invoicecollection_projectlocation : System.Web.UI.Page
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
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }
    #endregion


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

            hflEmpCode.Value = Convert.ToString(Session["Empcode"]);

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/invoicecollection_projectlocation.aspx");
            }
            else
            {
                Page.SmartNavigation = true;
                ProjectandLocation_access();
                if (!Page.IsPostBack)
                {
                    GetProjectList();
                    fillGridView_ProjectLocation("");
                     

                }
            }

        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    public void ProjectandLocation_access()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
            spars[0].Value = "check_Employee_access";
            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);
            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "Invoicecollection_projectlocation";
            getdtDetails = spm.getTeamReportAllDDL(spars, "sp_Invoice_Collection_Report");

            if(getdtDetails.Rows.Count<=0)
            {
                Response.Redirect(ReturnUrl("sitepathmain")+"Default.aspx");
            }

        }
        catch (Exception)
        {
        }
    }

    protected void trvl_btnSave_Click(object sender, EventArgs e)
    {
        // Validate input fields
        if (string.IsNullOrWhiteSpace(txtProjectName.Text))
        {
            lblmessage.Text = "Please enter Project Name";
            return;
        }

        if (lstLocationCode.SelectedValue.Trim() == "0")
        {
            lblmessage.Text = "Please Select Location Code";
            return;
        }

        if (Convert.ToString(hdnInvoiceId.Value).Trim() != "")
        {
            SqlParameter[] sparso = new SqlParameter[4];
            sparso[0] = new SqlParameter("@QueueType", SqlDbType.VarChar)
            {
                Value = "Updateinvocecollecation"
            };
            sparso[1] = new SqlParameter("@Project_Name", SqlDbType.VarChar)
            {
                Value = txtProjectName.Text.Trim()
            };
            sparso[2] = new SqlParameter("@Location_Code", SqlDbType.VarChar)
            {
                Value = lstLocationCode.SelectedValue.Trim()
            };
            sparso[3] = new SqlParameter("@Location_No", SqlDbType.VarChar)
            {
                Value =hdnInvoiceId.Value.Trim()
            };
            DataTable dt = spm.getData_FromCode(sparso, "sp_Invoice_Collection_Report");

            if (dt != null && dt.Rows.Count > 0)
            {
                lblmessage.Text = "Project Name and Location Code updated successfully";
                txtProjectName.Text = "";
                fillGridView_ProjectLocation("");
                GetProjectList();
            }
            else
            {
                lblmessage.Text = "Failed to add Project Name and Location Code. Please try again.";
            }
            hdnInvoiceId.Value = "";
        }
        else
        {
            SqlParameter[] sparso = new SqlParameter[3];
            sparso[0] = new SqlParameter("@QueueType", SqlDbType.VarChar)
            {
                Value = "InsertInvCollectionReport"
            };
            sparso[1] = new SqlParameter("@Project_Name", SqlDbType.VarChar)
            {
                Value = txtProjectName.Text.Trim()
            };
            sparso[2] = new SqlParameter("@Location_Code", SqlDbType.VarChar)
            {
                Value = lstLocationCode.SelectedValue.Trim()
            };

            // Execute the stored procedure to insert the project and location
            DataTable dt = spm.getData_FromCode(sparso, "sp_Invoice_Collection_Report");
            if (dt != null && dt.Rows.Count > 0)
            {
                lblmessage.Text = "Project Name and Location Code added successfully";
                txtProjectName.Text = "";
                fillGridView_ProjectLocation("");
                GetProjectList();
                hdnInvoiceId.Value = "";
            }
            else
            {
                lblmessage.Text = "Failed to add Project Name and Location Code. Please try again.";
            }
        }
    }
     

    public void GetProjectList()
    { 
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
        spars[0].Value = "Get_Project";

        DataTable dt = spm.getDropdownList(spars, "sp_Invoice_Collection_Report");
        lstLocationCode.DataSource = dt;
        lstLocationCode.DataTextField = "Location_name";
        lstLocationCode.DataValueField = "comp_code";
        lstLocationCode.DataBind();
        lstLocationCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Project Name", "0"));       
    }

    protected void DgvApprover_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Dvproject.PageIndex = e.NewPageIndex;

        if (Convert.ToString(lstLocationCode.SelectedValue).Trim() == "0")
            this.fillGridView_ProjectLocation("");
        else
            this.fillGridView_ProjectLocation(Convert.ToString(lstLocationCode.SelectedValue).Trim());
    }


    private void fillGridView_ProjectLocation(string slocationCode)
    {
       
        Dvproject.DataSource = null;
        Dvproject.DataBind(); 
        var dt = spm.GetLocationCode("GetLocationCode", slocationCode);
        if (dt.Rows.Count > 0)
        {
            Dvproject.DataSource = dt;
            Dvproject.DataBind();
        }

        
    }

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnInvoiceId.Value = Convert.ToString(Dvproject.DataKeys[row.RowIndex].Values[0]).Trim();

        lblmessage.Text = "";

        DataSet dsProjectLocation = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
        spars[0].Value = "GetLocationNO";

        spars[1] = new SqlParameter("@Location_No", SqlDbType.VarChar);
        spars[1].Value = hdnInvoiceId.Value;

        dsProjectLocation = spm.getDatasetList(spars, "sp_Invoice_Collection_Report");
        txtProjectName.Text = "";
        lstLocationCode.SelectedValue = "0";

        if (dsProjectLocation !=null)
        {
            if (dsProjectLocation.Tables[0].Rows.Count>0)
            {
                txtProjectName.Text = Convert.ToString(dsProjectLocation.Tables[0].Rows[0]["Project_Name"]);
                lstLocationCode.SelectedValue = Convert.ToString(dsProjectLocation.Tables[0].Rows[0]["comp_code"]);                
            }
        }
    }
     



    protected void lstLocationCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmessage.Text = "";
         if (Convert.ToString(lstLocationCode.SelectedValue).Trim() == "0")
            this.fillGridView_ProjectLocation("");
        else
            this.fillGridView_ProjectLocation(Convert.ToString(lstLocationCode.SelectedValue).Trim());
    }
}


