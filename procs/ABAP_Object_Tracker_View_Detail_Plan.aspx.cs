using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ABAP_Object_Tracker_View_Detail_Plan : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();

    #region Creative_Default_methods

    public string loc = "", dept = "", subdept = "", desg = "";
    public DataTable dtEmp;
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
                        DDLProjectLocation.SelectedValue = Session["ddlProjectLoc"].ToString().Trim();
                        hdnABAPODUploadId.Value = Convert.ToString(Request.QueryString["ABAPODId"]).Trim();
                        get_ABAP_Object_Submitted_Plan_Details_View();

                        btnTra_Details.PostBackUrl = "~/procs/ABAP_Object_Tracker_Plan_View.aspx?ABAPODId=" + hdnABAPODUploadId.Value;


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

        var objId = hdnABAPODUploadId.Value;

        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "ViewABAPObjectDetailPlan";

        spars[1] = new SqlParameter("@ABAPODUploadId", SqlDbType.VarChar);
        spars[1].Value = Convert.ToInt32(hdnABAPODUploadId.Value);

        dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
        {
            gvDetailPlan.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvDetailPlan.DataBind();

            txtPRM.Text = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ProjectManager"].ToString().Trim();

            //DDLProjectLocation.SelectedValue = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ProjectLocation"].ToString().Trim();
            //if (dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["UserRole"].ToString() == "PM" || dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["UserRole"].ToString() == "PRM")
            //{
            //    gvDetailPlan.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            //    gvDetailPlan.DataBind();

            //    txtPRM.Text = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ProjectManager"].ToString().Trim();

            //}
            //else if (dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["UserRole"].ToString() == "Consultant")
            //{
            //    rgs.Visible = false;

            //    if (dsABAPObjectPlanSubmitted.Tables[1].Rows.Count > 0)
            //    {
            //        rgs.Visible = true;
            //        gvDetailPlan.DataSource = dsABAPObjectPlanSubmitted.Tables[1];
            //        gvDetailPlan.DataBind();
            //    }


            //    txtPRM.Text = dsABAPObjectPlanSubmitted.Tables[6].Rows[0]["ProjectManager"].ToString().Trim();

            //}
        }
    }

    protected void gvDetailPlan_PreRender(object sender, EventArgs e)
    {
        GridView gridView = sender as GridView;

        if (gridView.HeaderRow == null)
            return;

        TableHeaderRow headerRow2 = new TableHeaderRow();

        for (int i = 0; i < gridView.Columns.Count; i++)
        {
            TableHeaderCell headerCell = new TableHeaderCell();
            if (i == 0)
                headerCell.Text = "Consultant Information"; 
            else if (i == 1)
                headerCell.Text = "Development Information"; 
                headerCell.Text = "Module Information";  
            
            headerRow2.Cells.Add(headerCell);
        }

        // Add the second header row above the regular header row
        gridView.Controls[0].Controls.AddAt(0, headerRow2);
    }

    #endregion

}