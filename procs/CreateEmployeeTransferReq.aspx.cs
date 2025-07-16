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
using System.Collections.Generic;

public partial class CreateEmployeeTransferReq : System.Web.UI.Page
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
    public DataTable dtEmp, dtleavebal, dtApprover, dtLeaveTypes, dtApproverEmailIds, dtMaxRequestId, dtIntermediate, dtLeaveCalculation, dtHolidays, dtleavedetails;
    public int Leaveid;
    public int leavetype, openbal, avail, rembal, weekendcount, publicholiday, apprid;
    double totaldays;
    public string filename = "", approveremailaddress, message;
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

                    if (Convert.ToString(Session["Empcode"]).Trim() == "00002082")
                    {
                        idReportingManager.Visible = true;
                        hdnIsDHLogin.Value = "true";
                    }
                    DeleteAllTempEmp();
                    PopulateEmployeeData();
                    BindDDLProject();
                    BindDDLShift(null);
                    BindDDL_EmployeeTransfer_Reasons();
                    BindReportingManagers(ddlbenchReportingManager);
                    btnback_mng.Visible = false;
                    if (Request.QueryString.Count > 0)
                    {
                        // BindDDLReportingManager();
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
                        BindDDLEmployee("");
                        BindTempEmpList();
                        BindDeploymentDDL();
                    }

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
            if (DgvApprover.Rows.Count == 0)
            {
                lblmessage.Text = "Please select any employee name";
                return;
            }
            if (ddlProject.Text.Trim() == "0")
            {
                lblmessage.Text = "Please enter project name";
                return;
            }
            if (ddlTask.Text.Trim() == "0")
            {
                lblmessage.Text = "Please select shift name";
                return;
            }

            //if (DDL_ReportingManager.SelectedValue == "0")
            //{
            //    lblmessage.Text = "Please select Reporting Manager"; 
            //    return;
            //}

            var createdBy = lpm.Emp_Code;
            // var RMName = Convert.ToString(DDL_ReportingManager.SelectedValue.ToString());
            var qtype = "InsertRequestMain";
            var getid = Convert.ToString(spm.InsertEmployeeTransferRequestTemp(qtype, null, null, createdBy, 0,0,"",""));
            // var getid = "0";
            if (getid != "")
            {
                #region Code to insert grid details
                qtype = "INSERTRequestDetails";
                var ETRequestId = getid;
                var newProjectCode = Convert.ToString(ddlProject.SelectedValue.ToString());
                var newShiftId = Convert.ToInt32(ddlTask.SelectedValue.ToString());
                var trDate = txtFromdate.Text;
                var TRequesterName = Convert.ToString(hflEmpName.Value);
                var tempDate = txtFromdate.Text.Split('-');
                var withEffectedDate = tempDate[2] + "-" + tempDate[1] + "-" + tempDate[0];

                
                foreach (GridViewRow row in DgvApprover.Rows)
                {
                    var getEmpCode = row.Cells[0].Text;
                    var getdiptypename = row.Cells[6].Text;
                    HiddenField Hddiploymenttype = (HiddenField)row.FindControl("Hddiploymenttype");

                    HiddenField hdnBenchReasonId = (HiddenField)row.FindControl("hdnBenchReasonId");
                    HiddenField hdnBenchRM = (HiddenField)row.FindControl("hdnBenchRM");
                    string benchReason_Remarks = ""; 

                    var getdiptype = Hddiploymenttype.Value;
                    var RMProject = "";
                    int ibenchReasonId = 0;
                    if (Convert.ToString(ddlProject.SelectedValue).Trim().ToLower().Contains("bench"))
                    {
                        RMProject = Convert.ToString(hdnBenchRM.Value).Trim();
                        benchReason_Remarks = Convert.ToString(row.Cells[7].Text).Trim();
                        ibenchReasonId = Convert.ToInt32(hdnBenchReasonId.Value);
                    }
                    spm.InsertEmployeeTransferRequestDetail(qtype, getEmpCode, createdBy, newProjectCode, newShiftId, withEffectedDate, ETRequestId, getdiptype, RMProject, benchReason_Remarks, ibenchReasonId);

                    SqlParameter[] spars = new SqlParameter[3];
                    spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                    spars[0].Value = "GetDMfromRequest";
                    spars[1] = new SqlParameter("@NewProjectCode", SqlDbType.VarChar);
                    spars[1].Value = newProjectCode;
                    spars[2] = new SqlParameter("@ETRequestId", SqlDbType.VarChar);
                    spars[2].Value = ETRequestId;

                    DataTable getdtDetails = spm.getEmployeeTransferDDLData(spars, "SP_EmployeeTransferRequest");

                    if (getdtDetails.Rows.Count > 0)
                    {
                        HDCaseApproved.Value = "Approved";
                        var getds = getAllEmployeePendingData("GetEmployeePendingStatus", createdBy, getEmpCode, trDate, ETRequestId);
                        var subject = "Transfer Request - " + ETRequestId + "  by " + TRequesterName + ". Transfer Date:" + trDate;
                        spm.SendEmailForEmployeeTransfer(getds, newProjectCode, txtFromdate.Text, subject, getdiptypename);
                    }

                    if (HDCaseApproved.Value != "Approved")
                    {
                        //  Send Email Employee
                        if (newProjectCode != "P-F-Bench")
                        {
                            var getds = getAllEmployeePendingData("GetEmployeePendingStatus", createdBy, getEmpCode, trDate, ETRequestId);
                            var subject = "Transfer Request - " + ETRequestId + "  by " + TRequesterName + ". Transfer Date:" + trDate;
                            spm.SendEmailForEmployeeTransfer(getds, newProjectCode, txtFromdate.Text, subject, getdiptypename);
                        }
                    }

                }
                #endregion

                if (HDCaseApproved.Value != "Approved")
                {
                    if (newProjectCode == "P-F-Bench")
                    {
                        var getData = spm.Get_TransferRequestDetails("getDHEMPCodeAppr", "", ETRequestId);
                        if (getData != null)
                        {
                            if (getData.Rows.Count > 0)
                            {
                                foreach (DataRow item in getData.Rows)
                                {
                                    var getA_Emp_Code = Convert.ToString(item["Appr_Emp_Code"]);
                                    var getA_Emp_Name = Convert.ToString(item["Emp_Name"]);
                                    var getA_Emp_Email = Convert.ToString(item["Emp_Emailaddress"]);
                                    var getMainId = Convert.ToString(item["MainId"]);
                                    var getEmpList = spm.Get_TransferRequestDetails("getEmployeeListByDHEMPCodeAppr", getA_Emp_Code, ETRequestId);
                                    if (getEmpList.Rows.Count > 0)
                                    {
                                        var getApp_Id = getMainId;
                                        //var link = "http://localhost/hrms/procs/EmployeeTransferReqInbox.aspx";
                                        var link = "https://ess.highbartech.com/hrms/login.aspx?ReturnUrl=procs/ViewEmployeeTransferReq.aspx?reqid=" + getApp_Id + "&app=b";
                                        var subject = "OneHR: Transfer Request for Approval -" + ETRequestId;
                                        var CreatedName = Convert.ToString(hflEmpName.Value);
                                        spm.SendEmailForDMTransferAppr(getEmpList, subject, getA_Emp_Email, getA_Emp_Name, CreatedName, link);
                                    }
                                }
                            }
                        }
                        //Sending Email To
                    }
                }
            }
            DeleteAllTempEmp();
            Response.Redirect("~/procs/EmployeeTransfer.aspx");

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);

            //throw;
        }

    }

    public void BindDDLEmployee(string slocationCode)
    {
        try
        {
            var getEmpLogin = lpm.Emp_Code;
            ddlEmployee.DataSource = null;
            ddlEmployee.DataBind();
            var qtype = "Get_EmployeeDDL";
            var dtEmployee = getAllDLLData(qtype, getEmpLogin, 0,slocationCode);
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
            var dtProject = getAllDLLData(qtype, "", 0, "");
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

    public void BindDDL_EmployeeTransfer_Reasons()
    {
        try
        {
            var getdtDetails = new DataTable();
            SqlParameter[] spars = new SqlParameter[1];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "Get_BenchTranferReasonList";
            getdtDetails = spm.getEmployeeTransferDDLData(spars, "SP_EmployeeTransferRequest");

            ddlbenchReasonsList.DataSource = null;
            ddlbenchReasonsList.DataBind();

            if (getdtDetails.Rows.Count > 0)
            {
                ddlbenchReasonsList.DataSource = getdtDetails;
                ddlbenchReasonsList.DataTextField = "Bench_Transfer_reason";
                ddlbenchReasonsList.DataValueField = "BTR_ID";
                ddlbenchReasonsList.DataBind();
            }
            ddlbenchReasonsList.Items.Insert(0, new ListItem("select Bench Reason", "0"));
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
            var dtProject = getAllDLLData(qtype, "", 0, projectCode);
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
            var dtEmpTempList = getAllDLLData(qtype, emp_code, 0, "");
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
                BindTempEmpList();
                //Response.Redirect("~/procs/CreateEmployeeTransferReq.aspx");
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

            if (ddlProject.SelectedValue.Trim() == "0")
            {
                lblmessage.Text = "Please select Project/ Location name";
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

            if (Convert.ToString(ddlProject.SelectedValue).Trim().ToLower().Contains("bench"))
            {

                if (Convert.ToString(ddlbenchReasonsList.SelectedValue).Trim() == "0")
                {
                    lblmessage.Text = "Please select Bench Reason For Transfer";
                    return;
                }

                if (Convert.ToString(txtBenchRemsrkas.Text).Trim() == "")
                {
                    lblmessage.Text = "Please enter Remarks for Bench Transfer";
                    return;
                }

                if (Convert.ToString(hdnIsDHLogin).Trim() == "true")
                {

                    if (Convert.ToString(ddlbenchReportingManager.SelectedValue).Trim() == "0")
                    {
                        lblmessage.Text = "Please Select Reporting Manager for Bench Transfer";
                        return;
                    }
                }

            }

            if (ddlEmployee.SelectedValue.Trim() != "0")
            {

                DataSet dsList = new DataSet();
                SqlParameter[] spars = new SqlParameter[2];

                spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                spars[0].Value = "Get_EmployeeLastLocation";

                spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
                spars[1].Value = Convert.ToString(ddlEmployee.SelectedValue).Trim();

                dsList = spm.getDatasetList(spars, "SP_EmployeeTransferRequest");

                if (dsList.Tables[0].Rows.Count >0)
                {
                    if(Convert.ToString(dsList.Tables[0].Rows[0]["NewProject"]).Trim().ToLower()==Convert.ToString(ddlProject.SelectedValue).Trim().ToLower())
                    {
                        lblmessage.Text = "Selected Employee already deployed on Selected Location/ Project";
                        return;
                    }
                   
                }

            }

                var emp_code = ddlEmployee.SelectedValue.Trim();
            var createdBy = lpm.Emp_Code;
            var qtype = "InsertRequestTemp";
            var deploymenttype = DDLDeploymentType.SelectedValue;
            int employeeTransfer_Reason = 0;
            string employeeTransfer_Remarks = "";
            string bench_RM = "";
            if (Convert.ToString(ddlbenchReasonsList.SelectedValue).Trim()!="0")
            {
                employeeTransfer_Reason = Convert.ToInt32(ddlbenchReasonsList.SelectedValue);
            }
            if (Convert.ToString(txtBenchRemsrkas.Text).Trim() != "")
            {
                employeeTransfer_Remarks = Convert.ToString(txtBenchRemsrkas.Text).Trim();
            }
            if (Convert.ToString(ddlbenchReportingManager.SelectedValue).Trim() != "0")
            {
                bench_RM = Convert.ToString(ddlbenchReportingManager.SelectedValue).Trim();
            }

            spm.InsertEmployeeTransferRequestTemp(qtype, emp_code, deploymenttype, createdBy, 0, employeeTransfer_Reason, employeeTransfer_Remarks, bench_RM);

            BindTempEmpList();
            BindDDLEmployee("");
            BindDeploymentDDL();
            //BindDDLProject();
            BindDDL_EmployeeTransfer_Reasons();
            BindReportingManagers(ddlbenchReportingManager);
            txtBenchRemsrkas.Text = "";

            if (Convert.ToString(ddlProject.SelectedValue).Trim().ToLower().Contains("bench"))
            {
                DgvApprover.Columns[6].Visible = true;
                DgvApprover.Columns[6].Visible = true;
                if (Convert.ToString(hdnIsDHLogin.Value).Trim() == "true")
                    DgvApprover.Columns[8].Visible = true;
                else
                    DgvApprover.Columns[8].Visible = false;

            }
            else
            {
                DgvApprover.Columns[6].Visible = false;
                DgvApprover.Columns[7].Visible = false;
                DgvApprover.Columns[8].Visible = false;

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {


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
    public DataTable getAllDLLData(string qtype, string a_Emp_Code, int id, string projectCode)
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
            if (Convert.ToString(projectCode).Trim() != "")
                spars[3].Value = projectCode;
            else
                spars[3].Value = DBNull.Value;

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
            var qtype = "UpdateCancelStatus";
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
            var qtype = "GETDetailOfRequestById";
            var dtEmpTempList = getAllDLLData(qtype, emp_code, id, "");
            if (dtEmpTempList.Rows.Count > 0)
            {
                DgvApprover.DataSource = dtEmpTempList;
                DgvApprover.DataBind();
                ddlProject.Items.FindByValue(Convert.ToString(dtEmpTempList.Rows[0]["NewProject"])).Selected = true;
                BindDDLShift(Convert.ToString(dtEmpTempList.Rows[0]["NewProject"]));
                ddlTask.Items.FindByValue(Convert.ToString(dtEmpTempList.Rows[0]["NewShiftId"])).Selected = true;
                ddlProject.Enabled = false;
                ddlTask.Enabled = false;
                liSelectedShift_1.Visible = true;
                //DropDownProjectEdit(ddlProject.SelectedValue);

                txtFromdate.Text = Convert.ToString(dtEmpTempList.Rows[0]["EffectiveDate"]);
                lblheading.Text = "Employee Transfer Request - " + Convert.ToString(dtEmpTempList.Rows[0]["RequestId"]);
                var status = Convert.ToString(dtEmpTempList.Rows[0]["Status"]);
                foreach (DataControlField col in DgvApprover.Columns)
                {
                    if (col.HeaderText == "Delete")
                    {
                        col.Visible = false;
                    }
                }
                if (status == "Pending")
                {
                    btnback_mng.Visible = true;
                }
                else
                {
                    IMPMsg.Visible = false;
                }


                if (Convert.ToString(ddlProject.SelectedValue).Trim().ToLower().Contains("bench"))
                {
                    DgvApprover.Columns[6].Visible = true;
                    DgvApprover.Columns[7].Visible = true;
                    if (Convert.ToString(hdnIsDHLogin.Value).Trim() == "true")
                        DgvApprover.Columns[8].Visible = true;
                    else
                        DgvApprover.Columns[8].Visible = false;

                }
                else
                {
                    DgvApprover.Columns[6].Visible = false;
                    DgvApprover.Columns[7].Visible = false;
                    DgvApprover.Columns[8].Visible = false;

                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public DataSet getAllEmployeePendingData(string qtype, string a_Emp_Code, string emp_Code, string date, string requestId)
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
            if (getId == "0")
            {
                lblmessage.Text = "Please select project";
                liSelectedShift_1.Visible = false;
                liSelectedShift_2.Visible = false;
                ddlProject.Enabled = true;
                return;
            }
            else
            {
                BindDDLShift(getId);
                DropDownProject(getId);
                liSelectedShift_1.Visible = true;
                liSelectedShift_2.Visible = true;
                ddlProject.Enabled = false;
                BindDDLEmployee(Convert.ToString(getId));

                if (Convert.ToString(ddlProject.SelectedValue).Trim().ToLower().Contains("bench"))
                {
                    libench_Reason_1.Visible = true;
                    libench_Reason_2.Visible = true;
                    libench_Reason_Remarks_1.Visible = true;
                    libench_Reason_Remarks_2.Visible = true;
                }

            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void DropDownProject(string str)
    {
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetDMfromLocation";
        spars[1] = new SqlParameter("@NewProjectCode", SqlDbType.VarChar);
        spars[1].Value = str;

        DataTable getdtDetails = spm.getEmployeeTransferDDLData(spars, "SP_EmployeeTransferRequest");
        if (getdtDetails.Rows.Count > 0)
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == getdtDetails.Rows[0]["DH"].ToString())
            {
                BindTempEmpList();
                if (ddlProject.SelectedValue == "P-F-Bench")
                {
                    foreach (GridViewRow row in DgvApprover.Rows)
                    {
                        TableCell reportingManagerCell = row.Cells[6];
                        if (reportingManagerCell != null)
                        {
                            DgvApprover.Columns[6].Visible = true;
                            reportingManagerCell.Visible = true;
                        }
                    }
                }
                else
                {
                    foreach (GridViewRow row in DgvApprover.Rows)
                    {
                        TableCell reportingManagerCell = row.Cells[6];
                        if (reportingManagerCell != null)
                        {
                            DgvApprover.Columns[6].Visible = false;
                            reportingManagerCell.Visible = false;
                        }
                    }
                }
            }
        }
    }

    public void DropDownProjectEdit(string str)
    {
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetDMfromLocation";
        spars[1] = new SqlParameter("@NewProjectCode", SqlDbType.VarChar);
        spars[1].Value = str;

        DataTable getdtDetails = spm.getEmployeeTransferDDLData(spars, "SP_EmployeeTransferRequest");

        if (getdtDetails.Rows.Count > 0)
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == getdtDetails.Rows[0]["DH"].ToString())
            {
                if (getdtDetails.Rows[0]["comp_code"].ToString() == "P-F-Bench")
                {
                    foreach (GridViewRow row in DgvApprover.Rows)
                    {
                        TableCell reportingManagerCell = row.Cells[6];
                        if (reportingManagerCell != null)
                        {
                            DgvApprover.Columns[6].Visible = true;
                            reportingManagerCell.Visible = true;
                        }
                    }
                }
                else
                {
                    foreach (GridViewRow row in DgvApprover.Rows)
                    {
                        TableCell reportingManagerCell = row.Cells[6];
                        if (reportingManagerCell != null)
                        {
                            DgvApprover.Columns[6].Visible = false;
                            reportingManagerCell.Visible = false;
                        }
                    }
                }

            }
            else
            {
                foreach (GridViewRow row in DgvApprover.Rows)
                {
                    TableCell reportingManagerCell = row.Cells[6];
                    if (reportingManagerCell != null)
                    {
                        DgvApprover.Columns[6].Visible = false;
                        reportingManagerCell.Visible = false;
                    }
                }
            }
        }
    }
    private void BindReportingManagers(DropDownList ddl)
    {

        var qtype = "Get_EmployeeRMDDL";
        var dtProject = getAllDLLData(qtype, "", 0, "");
        if (dtProject.Rows.Count > 0)
        {
            ddl.DataSource = dtProject;
            ddl.DataTextField = "Emp_name";
            ddl.DataValueField = "Emp_Code";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("Select Reporting Manager", "0"));
        }

    }
     
}


