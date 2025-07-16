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
using LnDPortal.Business.Entity;
using DocumentFormat.OpenXml.Office2010.Excel;

public partial class InboxAttend_Req : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
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

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }


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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Attendance");
            }
            else
            {
                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                hdnEmpCode.Value = Convert.ToString(Session["Empcode"]);
                if (!Page.IsPostBack)
                {
                    hdnhrappType.Value = "0";
                    // PopulateEmployeeLeaveData();
                    if (Request.QueryString.Count > 0)
                    {
                        hdnhrappType.Value = Convert.ToString(Request.QueryString[0]);
                        hdnReqids.Value = Convert.ToString(Request.QueryString[0]).Trim();

                    }
                    InboxLeaveReqstList();

                    // PopulateEmployeeData();
                    //if (Convert.ToString(hdnhrappType.Value) == "1")
                    //checkHR_Inbox();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";

                }

            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    //protected void lnkLeaveDetails_Clicks(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ImageButton btn = (ImageButton)sender;
    //        GridViewRow row = (GridViewRow)btn.NamingContainer;
    //        hdnReqid.Value = Convert.ToString(gvSelectedEmployeeAtt.DataKeys[row.RowIndex].Values[0]).Trim();
    //        hdnleaveTypeid.Value = Convert.ToString(gvSelectedEmployeeAtt.DataKeys[row.RowIndex].Values[1]).Trim();
    //        //   getSelectedEmpLeaveDetails_View();
    //        //Response.Redirect("Leave_Req_View.aspx?reqid=" + hdnReqid.Value);
    //        //if (hdnleaveTypeid.Value == "11")
    //        //{
    //        //    Response.Redirect("Encash_leave.aspx?reqid=" + hdnReqid.Value);
    //        //}
    //        //else
    //        //{
    //        //    Response.Redirect("Attend_Req.aspx?reqid=" + hdnReqid.Value);
    //        //}




    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message.ToString());
    //    }
    //}

     
  
    protected void checkHR_Inbox()
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_Check_TD_COS_apprver_code";

            spars[1] = new SqlParameter("@exp_trvl", SqlDbType.VarChar);
            spars[1].Value = "HRML";

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = hdnEmpCode.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");
            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                hdnApproverType.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_type"]).Trim();
                InboxLeaveReqstList_HR();

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }


    private void InboxLeaveReqstList()
    {
        try
        {
            // DataSet dsList = new DataSet();
            DataTable dtleaveInbox = new DataTable();
            dtleaveInbox = spm.GetAttRegInbox(strempcode);
            btnIn.Visible = false;
            if (dtleaveInbox.Rows.Count > 0)
            {
                gvMngLeaveRqstList.DataSource = dtleaveInbox;
                gvMngLeaveRqstList.DataBind();
                btnIn.Visible = true;
               lblmessage.Text = "Note:- Upon submission, all selected attendance records will be approved. Please review and confirm the details before submitting.";
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void InboxLeaveReqstList_HR()
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getAll_OD_LWP_HR";

            spars[1] = new SqlParameter("@empCode", SqlDbType.VarChar);
            spars[1].Value = hdnEmpCode.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETLEAVEINBOX");
            //Travel Request Count
            gvMngLeaveRqstList.DataSource = null;
            gvMngLeaveRqstList.DataBind();
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                gvMngLeaveRqstList.DataSource = dsTrDetails.Tables[0];
                gvMngLeaveRqstList.DataBind();

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }



    
    protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnReqid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
            if (hdnReqid.Value != "0")
            {
                Response.Redirect("Attend_Reg_Req_App.aspx?reqid=" + hdnReqid.Value);
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

        protected void gvMngLeaveRqstList_DataBounds(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //Find the DropDownList in the Row
            DropDownList ddlLeaveType = (e.Row.FindControl("ddlAproveReject") as DropDownList);
            if (ddlLeaveType != null)
            {
                ddlLeaveType.Items.Add(new System.Web.UI.WebControls.ListItem("Select", "0"));
                ddlLeaveType.Items.Add(new System.Web.UI.WebControls.ListItem("Approve", "Approve"));
                ddlLeaveType.Items.Add(new System.Web.UI.WebControls.ListItem("Reject", "Reject"));
            }
        }

    }

    protected void btnIn_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        foreach (GridViewRow row in gvMngLeaveRqstList.Rows)
        {
            CheckBox CHK_clearancefromSubmitted = (CheckBox)row.FindControl("CHK_clearancefromSubmitted");
            if (CHK_clearancefromSubmitted.Checked == true)
            {
                try
                {
                    //string emp_Code = "00631321";
                    hdnReqid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
                    Int32 Reg_Id = Convert.ToInt32(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[0]);
                    string Selectedemp_Code = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
                    string Selectedemp_Name = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
                    Int32 Leave_Reg_Id = Convert.ToInt32(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[3]);
                    var Remark = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[4]).Trim();

                    lblmessage.Text = "";

                    //Finding Dropdown control  
                    // DropDownList ddl1 = row.FindControl("ddlAproveReject") as DropDownList;                   
                    //if (ddl1 != null)
                    //{
                    //    var getValddl = Convert.ToString(ddl1.SelectedValue);
                    //    if (Convert.ToString(ddl1.SelectedValue).Trim() == "0")
                    //    {
                    //        lblmessage.Text = "Please Select Approv/ Reject option for this employee " + Selectedemp_Name;
                    //        return;
                    //    }
                    //}

                    try
                    {

                        var CreatedBy = strempcode;
                        var qtype = "UpdateAppDetail";                        
                        var LeaveStatus = "Approve";
                        var gettxtRemark = "Approve";
                        string att_Date = null;
                        spm.InsertAttDetails(Selectedemp_Code, qtype, Reg_Id, Leave_Reg_Id, "Approve", att_Date, LeaveStatus, CreatedBy, "Approve", gettxtRemark);
 
                    }
                    catch (Exception ex)
                    {

                    }

                    #region Code for att
                    /*
                    PopulateEmployeeData(emp_Code);
                    getMngLeaveReqstList(Convert.ToInt32(hdnReqid.Value));
                    try
                    {    
                        int status = 0;                       
                        if (status == 0)
                        {
                            DataTable dt = new DataTable();
                            DataRow dr;
                            dt.Columns.Add(new DataColumn("att_date"));
                            dt.Columns.Add(new DataColumn("IN_OUT_TYPE"));
                            dt.Columns.Add(new DataColumn("IN_OUT_TIME"));
                            dt.Columns.Add(new DataColumn("IN_OUT_STATUS"));
                            dt.Columns.Add(new DataColumn("Deduction"));
                            dt.Columns.Add(new DataColumn("Leave_Status"));
                            dt.Columns.Add(new DataColumn("Remark"));
                            dt.Columns.Add(new DataColumn("ApproveReject"));
                            dt.Columns.Add(new DataColumn("ApproveRemark"));


                            var CreatedBy = strempcode;
                            var qtype = "UpdateAppDetail";
                            var Reg_Id = 0;
                            var Leave_Reg_Id = 0;
                            var Remark = "";
                            var LeaveStatus = "";

                            foreach (GridViewRow rowsAtt in gvSelectedEmployeeAtt.Rows)
                            {
                                //Finding Dropdown control  
                                DropDownList ddl1 = rowsAtt.FindControl("ddlAproveReject") as DropDownList;
                                ddl1.SelectedValue = "Approve";
                                if (ddl1 != null)
                                {
                                    var getValddl = Convert.ToString(ddl1.SelectedValue);
                                    if (getValddl != "0")
                                    {
                                        Label Leave_Status = rowsAtt.FindControl("lblLeave_Status") as Label;
                                        var gettxtLeave_StatusRemark = Convert.ToString(Leave_Status.Text).Trim();

                                        Label lblLeave_Req_id = rowsAtt.FindControl("lblLeave_Req_id") as Label;
                                        var getlblatt_id = Convert.ToString(lblLeave_Req_id.Text).Trim();

                                        Label lblReg_id = rowsAtt.FindControl("lblReg_id") as Label;
                                        var getlblReg_id = Convert.ToString(lblReg_id.Text).Trim();

                                        //TextBox txtRemark = rowsAtt.FindControl("txtRemark") as TextBox;
                                        //var gettxtRemark = txtRemark.Text.Trim();
                                        var gettxtRemark = "Approve";

                                        string att_Date = null;
                                        Remark = gettxtLeave_StatusRemark;
                                        Leave_Reg_Id = Convert.ToInt32(getlblatt_id);
                                        Reg_Id = Convert.ToInt32(getlblReg_id);
                                        LeaveStatus = ddl1.SelectedItem.Text;

                                        spm.InsertAttDetails(emp_Code, qtype, Reg_Id, Leave_Reg_Id, "Approve", att_Date, LeaveStatus, CreatedBy, "Approve", gettxtRemark);

                                        string LeaveStatusMail = "";
                                        if (LeaveStatus == "Approve")
                                        {
                                            LeaveStatusMail = "Approved";
                                        }
                                        if (LeaveStatus == "Reject")
                                        {
                                            LeaveStatusMail = "Rejected";
                                        }
                                        dr = dt.NewRow();
                                        dr["att_date"] = rowsAtt.Cells[0].Text;
                                        dr["IN_OUT_TYPE"] = rowsAtt.Cells[1].Text;
                                        dr["IN_OUT_TIME"] = rowsAtt.Cells[2].Text;
                                        dr["IN_OUT_STATUS"] = rowsAtt.Cells[3].Text;
                                        dr["Deduction"] = rowsAtt.Cells[4].Text;
                                        dr["Leave_Status"] = rowsAtt.Cells[5].Text;
                                        dr["Remark"] = rowsAtt.Cells[6].Text;
                                        //dr["ApproveReject"] = LeaveStatus;
                                        dr["ApproveReject"] = LeaveStatusMail;
                                        dr["ApproveRemark"] = gettxtRemark;
                                        dt.Rows.Add(dr);
                                    }
                                }
                            }
                            //Send Email With 
                            spm.Att_Send_mailto_Emp_App(dt, hdnEmpEmail.Value, txtEmp_Name.Text);
                           // PopulateEmployeeData();


                        }

                    }
                    catch (Exception ex)
                    {

                    }
                    */
                    #endregion

                }
                catch (Exception ex)
                {

                }
            }

          

            }

        foreach (GridViewRow row in gvMngLeaveRqstList.Rows)
        {
            CheckBox CHK_clearancefromSubmitted = (CheckBox)row.FindControl("CHK_clearancefromSubmitted");
            if (CHK_clearancefromSubmitted.Checked == true)
            {
                hdnReqid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
                Int32 Reg_Id = Convert.ToInt32(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[0]);
                string Selectedemp_Code = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
                string Selectedemp_Name = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
                Int32 Leave_Reg_Id = Convert.ToInt32(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[3]);
                var Remark = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[4]).Trim();

                DataSet dsList = new DataSet();
                SqlParameter[] spars = new SqlParameter[2];
                spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                spars[0].Value = "GetAttendance_list_bulkApproved";

                spars[1] = new SqlParameter("@Reg_Id", SqlDbType.Int);
                spars[1].Value = Reg_Id;

                dsList = spm.getDatasetList(spars, "SP_Attendance_Regular");

                //Send Email With 
                var dtEmp = spm.GETEMPDETAIL_ATT(Selectedemp_Code);

                if (dsList != null)
                {
                    if (dsList.Tables[0].Rows.Count > 0)
                    {
                        spm.Att_Send_mailto_Emp_App(dsList.Tables[0], Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim(), Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Name"]).Trim());
                    }
                }
            }
        }
        InboxLeaveReqstList();
	Response.Redirect("Attendance.aspx");
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)sender;
        foreach (GridViewRow row in gvMngLeaveRqstList.Rows)
        {
            CheckBox chkRow = (CheckBox)row.FindControl("CHK_clearancefromSubmitted");
            if (chkRow != null)
            {
                chkRow.Checked = chkAll.Checked;
            }

        }

    }

}
