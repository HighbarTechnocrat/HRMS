using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_ABAP_Prd_TimeSheet_PMApprove : System.Web.UI.Page
{
    #region Creative_Default_methods
    SqlConnection source;
    public SqlDataAdapter sqladp;

    SP_Methods spm = new SP_Methods();
    string strempcode = ""; int AddnewCheck = 0;
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }


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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/abap_index");
            }
            else
            {
                Page.SmartNavigation = true;
                hdnEmpCode.Value = Convert.ToString(Session["Empcode"]);
                CheckABAPRights();
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString.Count > 0)
                    {
                        hdnId.Value = Convert.ToString(Request.QueryString[0]);
                        Get_RecordsEdit();
                        PopulateEmployeeData();
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

    public void CheckABAPRights()
    {

        SqlParameter[] sparsd = new SqlParameter[2];
        sparsd[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        sparsd[0].Value = "GetApproverPageButton";
        sparsd[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        sparsd[1].Value = Convert.ToString(Session["Empcode"].ToString());
        DataSet DSApprover = spm.getDatasetList(sparsd, "SP_ABAP_Productivity_CompletionSheet");

        if (DSApprover.Tables[1].Rows.Count <= 0)
        { 
            Response.Redirect("~/procs/Timesheet.aspx");
        }
        
        
    }

    public void PopulateEmployeeData()
    {
        try
        {
            DataTable dtEmp = spm.GetEmployeeData(Convert.ToString(hdnEmpCode.Value).Trim());
            if (dtEmp.Rows.Count > 0)
            { 
                hflEmpName.Value = (string)dtEmp.Rows[0]["Emp_Name"]; 

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }

    public void Get_RecordsEdit()
    {
        SqlParameter[] spars = new SqlParameter[3];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "PM_GetRecordEdit";
        spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCode.Value;
        spars[2] = new SqlParameter("@MainID", SqlDbType.Int);
        spars[2].Value = hdnId.Value;
        DataSet DS = spm.getDatasetList(spars, "SP_ABAP_Productivity_CompletionSheet");

        if (DS.Tables[0].Rows.Count > 0)
        {
            txtEmpCode.Text = Convert.ToString(DS.Tables[0].Rows[0]["Emp_Code"]).ToString();
            txtEmpName.Text = Convert.ToString(DS.Tables[0].Rows[0]["Emp_Name"]).ToString();
            txtDepartment.Text = Convert.ToString(DS.Tables[0].Rows[0]["Department_Name"]).ToString();
            txtDesignation.Text = Convert.ToString(DS.Tables[0].Rows[0]["DesginationName"]).ToString();
            txtFromdate.Text = Convert.ToString(DS.Tables[0].Rows[0]["Start_Datee"]).ToString();
            txtToDate.Text = Convert.ToString(DS.Tables[0].Rows[0]["End_datee"]).ToString();
            txtFromdate.Enabled = false;
            txtToDate.Enabled = false;
            dgTimesheetView.DataSource = DS.Tables[0];
            dgTimesheetView.DataBind();

        }
    }

    #endregion
    

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        var Flagcheck = "0";
       
        if (confirmValue != "Yes")
        {
            return;
        }
        foreach (GridViewRow crow in dgTimesheetView.Rows)
        {
            TextBox TxtPMRemarks = (TextBox)crow.FindControl("TxtPMRemarks");
            DropDownList lstIsCompleted = (DropDownList)crow.FindControl("lstIsCompleted");

            if (Convert.ToString(TxtPMRemarks.Text).Trim() != "")
            {
                Flagcheck = "1";

            }

            if (lstIsCompleted.SelectedValue == "0")
            {
                lblmessage.Text = "Please select Approver Status.";
                return;
            }

            if (lstIsCompleted.SelectedValue == "No")
            {
                if (Convert.ToString(TxtPMRemarks.Text).Trim() == "")
                {
                    lblmessage.Text = "Please enter Approver Remarks if Approver Status No.";
                    return;
                }
            }
            if (Convert.ToString(lstIsCompleted.SelectedValue).Trim() == "Yes")
            {
                Flagcheck = "1";
            }




            }
        if (Flagcheck == "0")
        {
            lblmessage.Text = "Please enter Approver Remarks.";
            return;
        }

        

        foreach (GridViewRow crow in dgTimesheetView.Rows)
        {
            DropDownList lstIsCompleted = (DropDownList)crow.FindControl("lstIsCompleted");
            TextBox        TxtPMRemarks = (TextBox)crow.FindControl("TxtPMRemarks");
            HiddenField HDUpdateID = (HiddenField)crow.FindControl("HDUpdateID");
            HiddenField HDComp_Code = (HiddenField)crow.FindControl("HDComp_Code");

            //if (Convert.ToString(TxtPMRemarks.Text).Trim() != "")
            //{
                    SqlParameter[] spars = new SqlParameter[7];
                    spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                    spars[0].Value = "UpdatePMApprove";

                    spars[1] = new SqlParameter("@PMEmpCode", SqlDbType.VarChar);
                    spars[1].Value = Convert.ToString(Session["Empcode"]).ToString();

                    spars[2] = new SqlParameter("@PMStatus", SqlDbType.VarChar);
                    spars[2].Value = Convert.ToString(lstIsCompleted.SelectedValue);

                    spars[3] = new SqlParameter("@PMRemarks", SqlDbType.VarChar);
                    if (Convert.ToString(TxtPMRemarks.Text.Trim()) != "")
                        spars[3].Value = Convert.ToString(TxtPMRemarks.Text.Trim());
                    else
                        spars[3].Value = DBNull.Value;

                    spars[4] = new SqlParameter("@DetailID", SqlDbType.VarChar);
                    spars[4].Value = Convert.ToString(HDUpdateID.Value);

                    spars[5] = new SqlParameter("@MainID", SqlDbType.VarChar);
                    spars[5].Value = Convert.ToString(hdnId.Value);

                     spars[6] = new SqlParameter("@Comp_Code", SqlDbType.VarChar);
                     spars[6].Value = Convert.ToString(HDComp_Code.Value);

            DataSet DS = spm.getDatasetList(spars, "SP_ABAP_Productivity_CompletionSheet");

           // }
        }

        SqlParameter[] sparmail = new SqlParameter[3];
        sparmail[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        sparmail[0].Value = "GetEmailApproverToABAP";
        sparmail[1] = new SqlParameter("@MainID", SqlDbType.VarChar);
        sparmail[1].Value = hdnId.Value;
        sparmail[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        sparmail[2].Value = Convert.ToString(Session["Empcode"]).ToString();

        DataSet DSEmail = spm.getDatasetList(sparmail, "SP_ABAP_Productivity_CompletionSheet");
        string StrSubject = "OneHr :- Approve ABAP Object Completion Sheet";

        spm.SendEmailForApproverToABAP(DSEmail, txtFromdate.Text.Trim(), txtToDate.Text.Trim(), StrSubject,Convert.ToString(hflEmpName.Value));

        Response.Redirect("~/procs/ABAP_Prd_TimeSheetAppList.aspx");
    }

    protected void mobile_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/ABAP_Prd_TimeSheetAppList.aspx");
    }
}