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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
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
                    BindDDLShift(null);
                   
                    btnback_mng.Visible = false;
                    if (Request.QueryString.Count > 0)
                    {
                        lblheading.Text = "Employee Transfer Request";
                        // btnIn.Visible = false;
                        hide1.Visible = false;
                        hide2.Visible = false;
                        btnBack.Visible = false;
                        hdnId.Value = Convert.ToString(Request.QueryString[0]);  
                        BindRequestDetailById();
                    }
                    else
                    {
                        BindDDLEmployee();
                        BindTempEmpList();
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
            var createdBy = lpm.Emp_Code;  
            var qtype = "InsertRequestMain";
            var getid=Convert.ToString(spm.InsertEmployeeTransferRequestTemp(qtype,null,createdBy,0));
            if (getid != "")
            {
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
                   var getEmpCode= row.Cells[0].Text;
                    spm.InsertEmployeeTransferRequestDetail(qtype, getEmpCode, createdBy, newProjectCode, newShiftId, withEffectedDate, ETRequestId);
                    //Send Email Employee

                    var getds = getAllEmployeePendingData("GetEmployeePendingStatus", createdBy, getEmpCode, trDate, ETRequestId);
                    var subject = "Transfer Request - "+ ETRequestId + "  by "+ TRequesterName + ". Transfer Date:"+ trDate;
                    spm.SendEmailForEmployeeTransfer(getds, newProjectCode, txtFromdate.Text, subject);
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
                spm.InsertEmployeeTransferRequestTemp(qtype, null, null, Convert.ToInt32(hdnId.Value));
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
            var emp_code = ddlEmployee.SelectedValue.Trim();
            var createdBy = lpm.Emp_Code;
            var qtype = "InsertRequestTemp";
            spm.InsertEmployeeTransferRequestTemp(qtype, emp_code, createdBy,0);
            BindTempEmpList();
            BindDDLEmployee();
           // Response.Redirect("~/procs/CreateEmployeeTransferReq.aspx");
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
            spm.InsertEmployeeTransferRequestTemp(qtype, null, createdBy, 0);
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
            var qtype = "UpdateCancelStatus";
            spm.InsertEmployeeTransferRequestTemp(qtype, null, null, id);
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
            var dtEmpTempList = getAllDLLData(qtype, emp_code, id,null);
            if (dtEmpTempList.Rows.Count > 0)
            {
                DgvApprover.DataSource = dtEmpTempList;
                DgvApprover.DataBind();
                ddlProject.Items.FindByValue(Convert.ToString(dtEmpTempList.Rows[0]["NewProject"])).Selected = true;
                BindDDLShift(Convert.ToString(dtEmpTempList.Rows[0]["NewProject"]));
                ddlTask.Items.FindByValue(Convert.ToString(dtEmpTempList.Rows[0]["NewShiftId"])).Selected = true;
                ddlProject.Enabled = false;
                ddlTask.Enabled = false;
                txtFromdate.Text = Convert.ToString(dtEmpTempList.Rows[0]["EffectiveDate"]);
                lblheading.Text = "Employee Transfer Request - "+ Convert.ToString(dtEmpTempList.Rows[0]["RequestId"]);
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
                }    
                else
                {
                    IMPMsg.Visible = false;
                }
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
}


