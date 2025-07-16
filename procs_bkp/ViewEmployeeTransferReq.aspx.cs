using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewEmployeeTransferReq : System.Web.UI.Page
{
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "", Wsch = "";
    public int did = 0;
    LeaveBalance bl = new LeaveBalance();
    SP_Methods spm = new SP_Methods();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    public DataTable dtEmp, dtleavebal, dtApprover, dtLeaveTypes, dtApproverEmailIds, dtMaxRequestId, dtIntermediate, dtLeaveCalculation, dtHolidays,dtleavedetails;
    public int Leaveid;
    public int leavetype, openbal, avail, rembal,weekendcount,publicholiday,apprid;
    double totaldays;
    public string filename = "", approveremailaddress,message;
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    #region Page Events
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
			if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {  
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
			
            lblmessage.Text = "";


            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

            lpm.Emp_Code = Session["Empcode"].ToString();
			
            lblmessage.Visible = true;
            //btnIn.Visible = false;
            btnback_mng.Visible = false;
            //   
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/EmployeeTransfer");
            }
            else
            {
               
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    txtFromdate.Text = DateTime.Now.AddDays(2).ToString("dd-MM-yyyy");
                    hdnReqid.Value = "";
                   
                    btnback_mng.Visible = false;
                    txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");

                    hdnlstfromfor.Value = "Full Day";
                    hdnlsttofor.Value = "Full Day";

                    editform.Visible = true;
                    divbtn.Visible = false;
                    lblmessage.Text = "";
                    this.lstApprover.SelectedIndex = 0;

                    hdnleaveconditiontypeid.Value = "12";
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                    DeleteAllTempEmp();
                    PopulateEmployeeData();                    
                    BindDDLProject();
                   // BindDDLReportingManager();
                    BindDDLShift(null);
                   
                    btnback_mng.Visible = false;
                    if (Request.QueryString.Count > 0)
                    {
                        lblheading.Text = "Employee Transfer Request";
                        // btnIn.Visible = false;
                        hide1.Visible = false;
                        hide2.Visible = false;
                        hide3.Visible = false;
                        btnBack.Visible = false;
                        hdnId.Value = Convert.ToString(Request.QueryString[0]);
                        HDCase.Value = "0";
                        BindRequestDetailById();
                    }
                    else
                    {
                        HDCase.Value = "1";
                        BindDDLEmployee();
                        BindTempEmpList();
                        BindDeploymentDDL();
                    }
                    btnback_mng.Visible = false;
                }
               // getTimesheetDetailsInDate();
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    #endregion

    #region page Methods

    public void PopulateEmployeeData()
    {
        try
        {
            txtEmpCode.Text = lpm.Emp_Code;
            //btnSave.Enabled = false; 
            dtEmp = spm.GetEmployeeData(lpm.Emp_Code);

            if (dtEmp.Rows.Count > 0)
            {
                lpm.Emp_Status = (string)dtEmp.Rows[0]["Emp_status"];

                //lblmessage.Text = "You are not allowed to apply for any type of leave sicne your employee status is in resignation";
                // IsEnabledFalse(false);                

                lpm.Emp_Name = (string)dtEmp.Rows[0]["Emp_Name"];
                lpm.Designation_name = (string)dtEmp.Rows[0]["DesginationName"];
                lpm.department_name = (string)dtEmp.Rows[0]["Department_Name"];
                lpm.Grade = Convert.ToString(dtEmp.Rows[0]["Grade"]).Trim();
                lpm.EmailAddress = (string)dtEmp.Rows[0]["Emp_EmailAddress"];
				//hheadyear.InnerText = " Leave Card - " + Convert.ToString(dtEmp.Rows[0]["years"]);
                //hheadyear.InnerText = " The below reflected leave balance is subject to changes, post closure of 2020 leave reconciliation on 18th Jan 2021";
                hflGrade.Value = lpm.Grade;
                hflEmailAddress.Value = lpm.EmailAddress;
                hflEmpName.Value = lpm.Emp_Name;
                hflEmpDepartment.Value = lpm.department_name;
                hflEmpDesignation.Value = lpm.Designation_name;
                //Call Leave Balance default.
               // getemployeeLeaveBalance();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }

    public void ClearControls()
    {
        txtFromdate.Text = "";
        lblmessage.Text = "";
        hdnFromFor.Value = "";
        hdnToDate.Value = "";
        hdnlsttofor.Value = "";
        hdnlstfromfor.Value = "";
        hdnOldLeaveCount.Value = "";
        hdnleavedays.Value = "";
        hdnLeaveStatus.Value = "";
        hflLeavestatus.Value = "";
        hdnlstfromfor.Value = "Full Day";
        hdnlsttofor.Value = "Full Day";
    }

    #endregion
   
    protected void btnBack_Click1(object sender, EventArgs e)
    {
        try
        {
            btnBack.Visible = true;
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            var getStatus = false;

            foreach (GridViewRow row in DgvApprover.Rows)
            {
                DropDownList dd = row.FindControl("ddlStatusType") as DropDownList;
                var selectedValue = (string)dd.SelectedValue;
                if (selectedValue != "0")               
                    getStatus = true;
                 else
                    getStatus = false;
            }
            var getRmSelected = false;
            foreach (GridViewRow row in DgvApprover.Rows)
            {
                DropDownList dd = row.FindControl("DDlReportingManager") as DropDownList;                
                var selectedValue = (string)dd.SelectedValue;
                if (selectedValue != "0")
                    getRmSelected = true;
                else
                    getRmSelected = false;
            }

            if (getStatus == false)
            {
                lblmessage.Text = "Please Select Employee Transfer Request Status.";
                return;
            }
            if (getRmSelected == false)
            {
                lblmessage.Text = "Please Select Reporting Manager for Employee Transfer Request.";
                return;
            }


            var ETRequestId = Convert.ToString(hdnRequestId.Value);
            var newProjectCode = Convert.ToString(hdnNewProjectId.Value);
            var TRequesterName = Convert.ToString(hdnCreatedBy.Value);            
            var trDate = txtFromdate.Text;
            var IsAnyRejected = false;
            var createdBy = lpm.Emp_Code;
            foreach (GridViewRow row in DgvApprover.Rows)
            {
                //find this control in this row
                DropDownList dd = row.FindControl("ddlStatusType") as DropDownList;
                DropDownList ddLRM = row.FindControl("DDlReportingManager") as DropDownList;
                var selectedValue = (string)dd.SelectedValue;
                var DDlRMM = "";
                if (ddLRM.SelectedValue != "0")
                {
                    DDlRMM = ddLRM.SelectedValue;
                }
                if(selectedValue!="0")
                {
                    
                    Label getId = row.FindControl("lblId") as Label;
                    var id = (string)getId.Text; 
                    Label getEmp_Code = row.FindControl("lblEmp_Code") as Label;
                    var emp_Code = (string)getEmp_Code.Text;
                    
                    Label getTypeName = row.FindControl("lblTypeName") as Label;
                    var deploymentName = (string)getTypeName.Text;
                    var getdiptypename = Convert.ToString(hdnCreatedBy.Value);

                   
                    var qtype = "UpdateApproveRequestApprToMain";
                  var getResult = spm.InsertEmployeeTransferRequestAppr(qtype, emp_Code, selectedValue, createdBy, Convert.ToInt32(id), DDlRMM);

                    getStatus = true;

                    if(selectedValue== "Approved")
                    {
                        var getds = getAllEmployeePendingData("GetEmployeePendingStatus", createdBy, emp_Code, trDate, ETRequestId);
                        var subject = "Transfer Request - " + ETRequestId + "  by " + TRequesterName + ". Transfer Date:" + trDate;
                        spm.SendEmailForEmployeeTransfer(getds, newProjectCode, txtFromdate.Text, subject, deploymentName);
                    }
                    else if(selectedValue == "Rejected")
                    {
                        IsAnyRejected = true;
                    }
                }               
            }
            if(IsAnyRejected==true)
            {
                var getEmpList = spm.Get_TransferRequestDetails("getEmployeeListByRejected", createdBy, ETRequestId);
                if (getEmpList.Rows.Count > 0)
                {
                    var link = "";
                    var subject = "OneHR : Reject : Transfer Request -" + ETRequestId;
                    var CreatedName = lpm.Emp_Name;
                    var getA_Emp_Email =Convert.ToString(getEmpList.Rows[0]["CreatedByEmail"]);
                    var getA_Emp_Name = Convert.ToString(getEmpList.Rows[0]["CreatedBYEmp"]);
                    spm.SendEmailForDMTransferAppr(getEmpList, subject, getA_Emp_Email, getA_Emp_Name, CreatedName, link);
                }
            }
            if (getStatus == false)
            {
                lblmessage.Text = "Please Select at list one Status.";
                return;
            }
            Response.Redirect("~/procs/EmployeeTransfer.aspx");
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);

            //throw;
        }

    }

    public void BindDDLEmployee()
    {
        try
        {
           var getEmpLogin= lpm.Emp_Code;
            ddlEmployee.DataSource = null;
            ddlEmployee.DataBind();
            var qtype = "Get_EmployeeDDL";
            var dtEmployee = getAllDLLData(qtype, getEmpLogin,0,null);
            if (dtEmployee.Rows.Count > 0)
            {
                ddlEmployee.DataSource = dtEmployee;
                ddlEmployee.DataTextField = "Emp_Name";
                ddlEmployee.DataValueField = "Emp_Code";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("Please select employee name", "0"));
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    public void BindDeploymentDDL()
    {
        try
        {
            SqlParameter[] spars1 = new SqlParameter[2];
            spars1[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars1[0].Value = "GetWFOWFHType";
            DataTable dt1 = spm.getDropdownList(spars1, "SP_WFO_WFH_DETAILS");
            DDLDeploymentType.DataSource = dt1;
            DDLDeploymentType.DataTextField = "TypeName";
            DDLDeploymentType.DataValueField = "Id"; 
            DDLDeploymentType.DataBind();
            //Add Default Item in the DropDownList
            DDLDeploymentType.Items.Insert(0, new ListItem("Please select Type", "0"));
        }
        catch (Exception)
        {
        }

    }

    public void BindDDLProject()
    {
        try
        {
            ddlProject.DataSource = null;
            ddlProject.DataBind();
            var qtype = "Get_ProjectDDL";
            var dtProject = getAllDLLData(qtype,null,0,null);
            if (dtProject.Rows.Count > 0)
            {
                ddlProject.DataSource = dtProject;
                ddlProject.DataTextField = "ProjectName";
                ddlProject.DataValueField = "comp_code";
                ddlProject.DataBind();
                ddlProject.Items.Insert(0, new ListItem("Please select project", "0"));
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public void BindDDLShift(string projectCode)
    {
        lblShift.InnerText = "";
        try
        {
            ddlTask.DataSource = null;
            ddlTask.DataBind();
            var qtype = "Get_ShiftDetailsDDL";
            var dtProject = getAllDLLData(qtype,null,0,projectCode);
            if (dtProject.Rows.Count > 0)
            {
                ddlTask.DataSource = dtProject;
                ddlTask.DataTextField = "Shift_Name";
                ddlTask.DataValueField = "Shift_Id";
                ddlTask.DataBind();
                ddlTask.Items.Insert(0, new ListItem("Please select shift", "0"));
                ddlTask.Items.FindByValue(Convert.ToString(dtProject.Rows[0]["Shift_Id"])).Selected = true;
                ddlTask.Enabled = false;
                txtTask.Text = Convert.ToString(dtProject.Rows[0]["Shift_Name"]);
                lblShift.InnerText = Convert.ToString(dtProject.Rows[0]["Shift_Name"]);
            }
            else
            {
                ddlTask.Items.Insert(0, new ListItem("Please select shift", "0"));
                ddlTask.Items.FindByValue(Convert.ToString("0")).Selected = true;
                ddlTask.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void BindTempEmpList()
    {
        try
        {
            var emp_code = lpm.Emp_Code;
            DgvApprover.DataSource = null;
            DgvApprover.DataBind();
            var qtype = "GetListEmployeeTemp";
            var dtEmpTempList = getAllDLLData(qtype, emp_code,0,null);
            if (dtEmpTempList.Rows.Count > 0)
            {
                DgvApprover.DataSource = dtEmpTempList;
                DgvApprover.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void DgvApprover_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        var emp_code = lpm.Emp_Code;
        //DgvApprover.DataSource = null;
        //DgvApprover.DataBind();
        var qtype = "GETTIMESHEETLIST";
        var dtTimesheet = spm.GetTimesheetList(qtype, emp_code);
        if (dtTimesheet.Rows.Count > 0)
        {
            DgvApprover.PageIndex = e.NewPageIndex;
            DgvApprover.DataSource = dtTimesheet;
            DgvApprover.DataBind();
        }
        //PopulateEmployeeLeaveData();
        //DgvApprover.PageIndex = e.NewPageIndex;
        //DgvApprover.DataSource = dsLeaveRequst;
        //DgvApprover.DataBind();
    }

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnId.Value = Convert.ToString(DgvApprover.DataKeys[row.RowIndex].Values[0]).Trim();
            if (hdnId.Value != "0")
            {
                var qtype = "DeleteEmpTemp";
                spm.InsertEmployeeTransferRequestTemp(qtype, null, null, null, Convert.ToInt32(hdnId.Value),0,"","");
                Response.Redirect("~/procs/CreateEmployeeTransferReq.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
           // btnIn.Visible = true;
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            if (ddlEmployee.SelectedValue.Trim() == "0")
            {
                lblmessage.Text = "Please select employee name";
                return;
            }
            if (DDLDeploymentType.SelectedValue.Trim() == "0") 
            {
                lblmessage.Text = "Please select Deployment Location";
                return;
            }


            var emp_code = ddlEmployee.SelectedValue.Trim();
            var createdBy = lpm.Emp_Code;
            var qtype = "InsertRequestTemp";
            var deploymenttype = DDLDeploymentType.SelectedValue;
            spm.InsertEmployeeTransferRequestTemp(qtype, emp_code, deploymenttype, createdBy,0,0,"","");
            BindTempEmpList();
            BindDDLEmployee();
            BindDeploymentDDL();
           // Response.Redirect("~/procs/CreateEmployeeTransferReq.aspx");
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    

    public void DeleteAllTempEmp()
    {
        try
        {
            var createdBy = lpm.Emp_Code;
            var qtype = "DeleteAllEmpTemp";
            spm.InsertEmployeeTransferRequestTemp(qtype, null, null, createdBy, 0,0,"","");
        }
        catch (Exception)
        {

        }
    }
    //Get All DLL List
    public DataTable getAllDLLData(string qtype, string a_Emp_Code,int id,string projectCode)
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[4];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = qtype;

            spars[1] = new SqlParameter("@CREATEDBY", SqlDbType.VarChar);
            spars[1].Value = a_Emp_Code;

            spars[2] = new SqlParameter("@Id", SqlDbType.Decimal);
            spars[2].Value = id;

            spars[3] = new SqlParameter("@NewProjectCode", SqlDbType.VarChar);
            spars[3].Value = projectCode;

            getdtDetails = spm.getEmployeeTransferDDLData(spars, "SP_EmployeeTransferRequest");
            return getdtDetails;
        }
        catch (Exception ex)
        {
            getdtDetails = new DataTable();
            return getdtDetails;
        }
    }

    protected void btnback_mng_Click(object sender, EventArgs e)
    {
        try
        {
            var id = Convert.ToInt32(hdnId.Value);
            //var createdBy = lpm.Emp_Code;
            var qtype = "UpdateCancelStatusAppr";
            spm.InsertEmployeeTransferRequestTemp(qtype, null, null, null, id,0,"","");
            Response.Redirect("~/procs/EmployeeTransfer.aspx");
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return;
        }
    }

    public void BindRequestDetailById()
    {
        try
        {
            lblShift.InnerText = "";
            var id = Convert.ToInt32(hdnId.Value);
            var emp_code = lpm.Emp_Code;
            DgvApprover.DataSource = null;
            DgvApprover.DataBind();
            var qtype = "GETDetailOfRequestByIdAppr";
            var dtEmpTempList = getAllDLLData(qtype, emp_code, id,null);
            if (dtEmpTempList.Rows.Count > 0)
            {
                DgvApprover.DataSource = dtEmpTempList;
                DgvApprover.DataBind();
                BindDDLShift(Convert.ToString(dtEmpTempList.Rows[0]["NewProject"]));
                ddlTask.Items.FindByValue(Convert.ToString(dtEmpTempList.Rows[0]["NewShiftId"])).Selected = true;
                ddlProject.Enabled = false;
                
                ddlTask.Enabled = false;
                //txtFromdate.Text = Convert.ToString(dtEmpTempList.Rows[0]["EffectiveDate"]);
                txtFromdate.Text = Convert.ToString(DateTime.Now.AddDays(2).ToString("dd-MM-yyyy"));
                lblheading.Text = "Employee Transfer Request - "+ Convert.ToString(dtEmpTempList.Rows[0]["RequestId"]);
                hdnRequestId.Value = Convert.ToString(dtEmpTempList.Rows[0]["RequestId"]);
                hdnCreatedBy.Value = Convert.ToString(dtEmpTempList.Rows[0]["CreatedBy"]);
                //lblCreatedBy.InnerText = Convert.ToString(dtEmpTempList.Rows[0]["CreatedBy"]);
                txt_CreatedBy.Text = Convert.ToString(dtEmpTempList.Rows[0]["CreatedBy"]);
               // lblCreatedOn.InnerText = Convert.ToString(dtEmpTempList.Rows[0]["CreatedOnDate"]);
                txt_CreatedOn.Text = Convert.ToString(dtEmpTempList.Rows[0]["CreatedOnDate"]);
                hdnNewProjectId.Value = Convert.ToString(dtEmpTempList.Rows[0]["NewProject"]);
                var status = Convert.ToString(dtEmpTempList.Rows[0]["Status"]);
                foreach (DataControlField col in DgvApprover.Columns)
                {
                    if (col.HeaderText == "Delete")
                    {
                        col.Visible = false;
                    }
                }
                if(status== "Pending")
                {
                    btnback_mng.Visible = true;
                    btnBack.Visible = true;
                }    
                else
                {
                    btnback_mng.Visible = false;
                    btnBack.Visible = false;
                }
                ddlProject.Items.FindByValue(Convert.ToString(dtEmpTempList.Rows[0]["NewProject"])).Selected = true;
                
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public DataSet getAllEmployeePendingData(string qtype, string a_Emp_Code, string emp_Code,string date,string requestId)
    {
        var getdtDetails = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[4];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = qtype;

            spars[1] = new SqlParameter("@CREATEDBY", SqlDbType.VarChar);
            spars[1].Value = a_Emp_Code;

            spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            spars[2].Value = emp_Code;//@ETRequestId

            spars[3] = new SqlParameter("@ETRequestId", SqlDbType.VarChar);
            spars[3].Value = requestId;//@@ETRequestId

            getdtDetails = spm.getEmployeeTransferEmailDetails(spars, "SP_EmployeeTransferRequest");
            return getdtDetails;
        }
        catch (Exception)
        {
            getdtDetails = new DataSet();
            return getdtDetails;
        }
    }


    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblShift.InnerText = "";
            var getId = Convert.ToString(ddlProject.SelectedValue);
            if(getId=="0")
            {
                lblmessage.Text = "Please select project";
                return;
            }
            else
            {
                BindDDLShift(getId);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    //public void BindDDLReportingManager()
    //{
    //    try
    //    {
    //        DDL_ReportingManager.DataSource = null;
    //        DDL_ReportingManager.DataBind();
    //        var qtype = "Get_EmployeeRMDDL";
    //        var dtProject = getAllDLLData(qtype, null, 0, null);
    //        if (dtProject.Rows.Count > 0)
    //        {
    //            DDL_ReportingManager.DataSource = dtProject;
    //            DDL_ReportingManager.DataTextField = "Emp_name";
    //            DDL_ReportingManager.DataValueField = "Emp_Code";
    //            DDL_ReportingManager.DataBind();
    //            DDL_ReportingManager.Items.Insert(0, new ListItem("Please select Employee", "0"));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

    private void BindReportingManagers(DropDownList ddl)
    {
        
        var qtype = "Get_EmployeeRMDDL";
        var dtProject = getAllDLLData(qtype, null, 0, null);
        if (dtProject.Rows.Count > 0)
        {
            ddl.DataSource = dtProject;
            ddl.DataTextField = "Emp_name"; 
            ddl.DataValueField = "Emp_Code"; 
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Reporting Manager", "0"));
        }
           
    }

    protected void DgvApprover_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlStatusType = (DropDownList)e.Row.FindControl("ddlStatusType");
            string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();
            if (ddlStatusType != null && ddlStatusType.Items.FindByValue(status) != null)
            {
                ddlStatusType.SelectedValue = status;
            }
            // Find the DropDownList control
            DropDownList ddlReportingManager = (DropDownList)e.Row.FindControl("DDlReportingManager");

            if (ddlReportingManager != null)
                // Call method to bind the dropdown list
                BindReportingManagers(ddlReportingManager);

            if (HDCase.Value == "0")
            {
                string categoryName = DataBinder.Eval(e.Row.DataItem, "RM").ToString();
                ListItem selectedItem = ddlReportingManager.Items.FindByValue(categoryName);
                if (selectedItem != null)
                {
                    ddlReportingManager.ClearSelection();
                    selectedItem.Selected = true;
                }
            }

        }
        }
    }



