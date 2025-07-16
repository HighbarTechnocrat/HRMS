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



public partial class MyAttend_Req : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc="", dept="", subdept="", desg = "";
    public int did = 0;
   
    SP_Methods spm = new SP_Methods();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    DataTable dtLeaveDetails;
    DataSet dsLeaveRequst;
    string strempcode = "";

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url;}

    #region Page Events
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }


            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                lpm.Emp_Code = strempcode;
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;


                    PopulateEmployeeData();
                    getMngLeaveReqstList();

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
    protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnReqid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
            hdnleaveTypeid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
            //   getSelectedEmpLeaveDetails_View();
            //Response.Redirect("Leave_Req_View.aspx?reqid=" + hdnReqid.Value);
            if (hdnleaveTypeid.Value == "11")
            {
                Response.Redirect("Encash_leave.aspx?reqid=" + hdnReqid.Value);
            }
            else
            {
                Response.Redirect("Attend_Req.aspx?reqid=" + hdnReqid.Value);
            }




        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    //protected void gvMngLeaveRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    getMngLeaveReqstList();
    //    gvMngLeaveRqstList.PageIndex = e.NewPageIndex;
    //    gvMngLeaveRqstList.DataSource = dsLeaveRequst;
    //    gvMngLeaveRqstList.DataBind();
    //}
    #endregion

    #region Page Methods
    private void getMngLeaveReqstList()
    {
        gvMngLeaveRqstList.DataSource = null;
        gvMngLeaveRqstList.DataBind();
        string strSelfApprover = "";
        try
        {
            DataTable dsTrDetails = new DataTable();

            dsTrDetails = spm.GetRegularization(lpm.Emp_Code);

            if (dsTrDetails.Rows.Count > 0)
            {
                gvMngLeaveRqstList.DataSource = dsTrDetails;
                gvMngLeaveRqstList.DataBind();

            }
            else
            {
                btnIn.Visible = false;
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    #endregion


    #region code Not in use

    public void PopulateEmployeeData()
    {
        try
        {
           // txtEmpCode.Text = lpm.Emp_Code;
            //btnSave.Enabled = false; 
           var dtEmp = spm.GETEMPDETAIL_ATT(lpm.Emp_Code);

            if (dtEmp.Rows.Count > 0)
            {
                lpm.Emp_Status = (string)dtEmp.Rows[0]["Emp_status"];

                //lblmessage.Text = "You are not allowed to apply for any type of leave sicne your employee status is in resignation";
                // IsEnabledFalse(false);                
                txt_EmpCode.Text = lpm.Emp_Code;
                HDNResignValue.Value = Convert.ToString(dtEmp.Rows[0]["Resig_Date"]).Trim();
                txtEmp_Name.Text = (string)dtEmp.Rows[0]["Emp_Name"];
                txtEmp_Desigantion.Text = (string)dtEmp.Rows[0]["DesginationName"];
                txtEmp_Department.Text = (string)dtEmp.Rows[0]["Department_Name"];
                txt_Project.Text = (string)dtEmp.Rows[0]["Project"];
                lpm.Grade = Convert.ToString(dtEmp.Rows[0]["Grade"]).Trim();
                lpm.EmailAddress = (string)dtEmp.Rows[0]["Emp_EmailAddress"];
                hdnLeaveCount.Value = Convert.ToString(dtEmp.Rows[0]["SL_LeaveBalance"]);
                hdnLeaveCountFix.Value = Convert.ToString(dtEmp.Rows[0]["SL_LeaveBalance"]);
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



    protected void gvMngLeaveRqstList_DataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var getType = spm.GetLeaveType();
            if(getType.Rows.Count>0)
            {
                //Find the DropDownList in the Row
                DropDownList ddlLeaveType = (e.Row.FindControl("ddlLeaveType") as DropDownList);
                ddlLeaveType.DataSource = getType;
                ddlLeaveType.DataTextField = "Leave_Type_Description";
                ddlLeaveType.DataValueField = "Leavetype_id";
                ddlLeaveType.DataBind();
                
                //Add Default Item in the DropDownList
                ddlLeaveType.Items.Insert(0, new ListItem("Please select","0"));

                //Select the Country of Customer in DropDownList
                string lblLeaveId = (e.Row.FindControl("lblLeaveId") as Label).Text;
                string lblLeaveType = Convert.ToString(e.Row.Cells[4].Text);  
                // var AssignedDate = Convert.ToDateTime(row["AssignedDate"].ToString()).Date;
                DateTime temp = DateTime.ParseExact((e.Row.FindControl("lblDate") as Label).Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                string str = temp.ToString("yyyy-MM-dd");
               // DateTime dt = DateTime.ParseExact(lblDate, "dd-MM-yyyy",CultureInfo.InvariantCulture);
                DateTime dt1 = DateTime.Now.AddDays(-1).Date;
                if (temp == dt1)
                {
                    ddlLeaveType.Items.FindByValue("9").Enabled = true;
                    if (lblLeaveType == "SL")
                        ddlLeaveType.Items.FindByValue("2").Enabled = false;
                }
                else
                {
                    ddlLeaveType.Items.FindByValue("9").Enabled = false;
                    if (lblLeaveType == "SL")
                    {
                        ddlLeaveType.Items.FindByValue("2").Enabled = false;
                        ddlLeaveType.Enabled = false;
                    }
                }
                // get Count For Regular
                var qtype = "GetCountRegularMonth";
                var getDt = spm.CheckIsEXISTSRegularization(qtype, str, lpm.Emp_Code, 0);
                if (getDt.Rows.Count > 0)
                {
                    var getStatus = Convert.ToInt32(getDt.Rows[0]["TotalCount"]);
                    if (getStatus > 3)
                    {
                        if (ddlLeaveType.Items.FindByValue("9") != null)
                        {
                            ddlLeaveType.Items.FindByValue("9").Enabled = false;
                        }

                    }
                }
                if(HDNResignValue.Value !="")
                {
                    DataRow[] dr3 = getType.Select("Leavetype_id=2");
                    if (dr3.Length > 0)
                    {
                        string itemValue = "2";
                        if (ddlLeaveType.Items.FindByValue(itemValue) != null)
                        {
                            string itemText = ddlLeaveType.Items.FindByValue(itemValue).Text;
                            ListItem li = new ListItem();
                            li.Text = itemText;
                            li.Value = itemValue;
                            ddlLeaveType.Items.Remove(li);
                        }
                    }
                }
                

                //ddlLeaveType.Items.FindByValue(lblLeaveId).Selected = true;
            }
            
        }

    }

    protected void btnIn_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            int status = 0;
            foreach (GridViewRow row in gvMngLeaveRqstList.Rows)
            {
                //Finding Dropdown control  
                DropDownList ddl1 = row.FindControl("ddlLeaveType") as DropDownList;
                if (ddl1 != null)
                {
                    var getValddl =Convert.ToInt32(ddl1.SelectedValue);
                    if(getValddl!=0)
                    {
                        var emp_Code = lpm.Emp_Code;
                        TextBox lblatt_id = row.FindControl("lblatt_id") as TextBox;
                        var getlblatt_id = lblatt_id.Text.Trim();
                        var Leave_Reg_Id = Convert.ToInt32(getlblatt_id);
                        DateTime temp = DateTime.ParseExact((row.FindControl("lblDate") as Label).Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        string att_Date = temp.ToString("yyyy-MM-dd");
                        var qtype = "CheckIsAttendance_Regular";
                        var getDt = spm.CheckIsEXISTSRegularization(qtype, att_Date, emp_Code, Leave_Reg_Id);
                        if (getDt.Rows.Count > 0)
                        {
                            var getStatus = Convert.ToString(getDt.Rows[0]["StatusMessage"]);
                            if (getStatus == "Yes")
                            {
                                Response.Redirect("~/procs/Attendance.aspx");
                            }
                        }

                        status = 1;
                    }
                }
            }

            if(status==1)
            {
                var emp_Code = lpm.Emp_Code;
                var CreatedBy = lpm.Emp_Code;
                var qtype = "Insert";
                var Reg_Id = 0;
                var Leave_Reg_Id = 0;
                var Remark = "";
                var LeaveStatus = "";
                var getId = spm.InsertAttDetails(emp_Code, qtype, 0,0, "Pending", "", "", CreatedBy,"","");

                if(getId!=null)
                {
                    var id = Convert.ToInt32(getId);
                    Reg_Id = Convert.ToInt32(getId);
                    qtype = "InsertDetails";
                    emp_Code = lpm.Emp_Code;
                    CreatedBy = lpm.Emp_Code;
                    foreach (GridViewRow row in gvMngLeaveRqstList.Rows)
                    {
                        //Finding Dropdown control  
                        DropDownList ddl1 = row.FindControl("ddlLeaveType") as DropDownList;
                        if (ddl1 != null)
                        {
                            var getValddl = Convert.ToInt32(ddl1.SelectedValue);
                            if (getValddl != 0)
                            {                                
                                TextBox txtRemark = row.FindControl("txtRemark") as TextBox;
                                var gettxtRemark = txtRemark.Text.Trim();
                               
                                TextBox lblatt_id = row.FindControl("lblatt_id") as TextBox;
                                var getlblatt_id = lblatt_id.Text.Trim();

                                DateTime temp = DateTime.ParseExact((row.FindControl("lblDate") as Label).Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                string att_Date = temp.ToString("yyyy-MM-dd");

                                Remark = gettxtRemark;
                                Leave_Reg_Id = Convert.ToInt32(getlblatt_id);
                                LeaveStatus = ddl1.SelectedItem.Text;
                                spm.InsertAttDetails(emp_Code, qtype, Reg_Id, Leave_Reg_Id, "", att_Date, LeaveStatus, CreatedBy, Remark,"");
                            }
                        }
                    }

                    //Check Is SelfApprove
                    var getDs = spm.CheckIsSelfApprove(emp_Code, Convert.ToInt32(getId));
                    
                    var getApproveTable = getDs.Tables[0];
                    var getDtailsTable = getDs.Tables[1];
                    if(getApproveTable.Rows.Count>0)
                    {
                        var gety = Convert.ToString(getApproveTable.Rows[0]["emp_IsSelftAppr"]).Trim();
                        if(gety=="Y")
                        {
                            qtype = "UpdateAppDetail";
                            if (getDtailsTable.Rows.Count>0)
                            {
                                foreach (DataRow row in getDtailsTable.Rows)
                                {
                                    string app_Comment = "Self Approve";
                                    string att_Date = null;
                                    Remark = "Self Approve";
                                    Leave_Reg_Id = Convert.ToInt32(row["Leave_Req_id"]);
                                    Reg_Id = Convert.ToInt32(getId);
                                    LeaveStatus = "Approve";
                                    spm.InsertAttDetails(emp_Code, qtype, Reg_Id, Leave_Reg_Id, "Approve", att_Date, LeaveStatus, CreatedBy, Remark, app_Comment);
                                }
                                
                            }                           
                        }
                        else
                        {
                            //Send Email To RM
                            var getRmDt = spm.GetRMDETAIL(emp_Code);
                            if (getRmDt.Rows.Count > 0)
                            {
                                var getEmail = Convert.ToString(getRmDt.Rows[0]["Emp_Emailaddress"]);
                                //"Attend_Reg_Req_App.aspx?reqid=" + hdnReqid.Value
                                string reg_App = "https://ess.highbartech.com/hrms/login.aspx?ReturnUrl=procs/Attend_Reg_Req_App.aspx";
                                string redirectURL = Convert.ToString(reg_App).Trim() + "?reqid=" + Reg_Id;
                               spm.ATT_send_mailto_Employee(getEmail, txtEmp_Name.Text.Trim(), redirectURL);
                            }
                        }
                    }
                    else
                    {
                        //Send Email To RM
                        var getRmDt = spm.GetRMDETAIL(emp_Code);
                        if (getRmDt.Rows.Count > 0)
                        {
                            var getEmail = Convert.ToString(getRmDt.Rows[0]["Emp_Emailaddress"]);
                            //"Attend_Reg_Req_App.aspx?reqid=" + hdnReqid.Value
                            string reg_App = "https://ess.highbartech.com/hrms/login.aspx?ReturnUrl=procs/Attend_Reg_Req_App.aspx";
                            string redirectURL = Convert.ToString(reg_App).Trim() + "?reqid=" + Reg_Id;
                           spm.ATT_send_mailto_Employee(getEmail, txtEmp_Name.Text.Trim(), redirectURL);
                        }
                    }
                    
                }
                Response.Redirect("~/procs/Attendance.aspx");
                //getMngLeaveReqstList();
            }
            else
            {
                lblmessage.Text = "Please select atleast one record for regularization.";
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            DropDownList list = (DropDownList)sender;
            string value = list.SelectedValue;
            if(value== "2")
            {
                int line = ((GridViewRow)((DropDownList)sender).Parent.Parent).RowIndex;
                var getLeaveDay =Convert.ToDouble(gvMngLeaveRqstList.Rows[line].Cells[5].Text.ToString());
                var getLeave = 0.00;
                if (hdnLeaveCount.Value.ToString() == "")
                {
                    getLeave = 0;
                }
                else
                {
                    getLeave = Convert.ToDouble(hdnLeaveCount.Value);
                }
                
                var getLeaveFix = Convert.ToDouble(hdnLeaveCountFix.Value);
                var getTotalBalance = (getLeave - getLeaveDay);
                if(getLeave < getLeaveDay)
                {
                    var getType = spm.GetLeaveType();
                    list.DataSource = null;
                    list.DataBind();
                    lblmessage.Text = "You have Insufficient Sick Leave Balance. You can regularize only "+ getLeaveFix + " days against Sick Leave";
                    list.DataSource = getType;
                    list.DataTextField = "Leave_Type_Description";
                    list.DataValueField = "Leavetype_id";
                    list.DataBind();

                    //Add Default Item in the DropDownList
                    list.Items.Insert(0, new ListItem("Please select", "0"));

                    string lblLeaveType = Convert.ToString(gvMngLeaveRqstList.Rows[line].Cells[4].Text.ToString());
                    // var AssignedDate = Convert.ToDateTime(row["AssignedDate"].ToString()).Date;
                    DateTime temp = DateTime.ParseExact(gvMngLeaveRqstList.Rows[line].Cells[0].Text.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    string str = temp.ToString("yyyy-MM-dd");
                    // DateTime dt = DateTime.ParseExact(lblDate, "dd-MM-yyyy",CultureInfo.InvariantCulture);
                    DateTime dt1 = DateTime.Now.AddDays(-1).Date;
                    if (temp == dt1)
                    {
                        list.Items.FindByValue("9").Enabled = true;
                        if (lblLeaveType == "SL")
                            list.Items.FindByValue("2").Enabled = false;
                    }
                    else
                    {
                        list.Items.FindByValue("9").Enabled = false;
                        if (lblLeaveType == "SL")
                        {
                            list.Items.FindByValue("2").Enabled = false;
                            list.Enabled = false;
                        }
                    }


                    return;
                }
                else
                {
                    hdnLeaveCount.Value = Convert.ToString(getTotalBalance);
                }
            }
            else
            {
                var count = 0;
                foreach (GridViewRow row in gvMngLeaveRqstList.Rows)
                {
                    //find this control in this row
                    DropDownList dd = row.FindControl("ddlLeaveType") as DropDownList;
                    var selectedValue= (string)dd.SelectedValue;
                    if (selectedValue == "2")
                        count++;
                }
                var getLeaveFix = 0.00;
                if (hdnLeaveCountFix.Value.ToString() == "")
                {
                    getLeaveFix = 0;
                }
                else
                {
                    getLeaveFix = Convert.ToDouble(hdnLeaveCountFix.Value);
                }

                var getTotalBalance = (getLeaveFix - count);
                hdnLeaveCount.Value = Convert.ToString(getTotalBalance);
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.ToString();
        }
    }
}
