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



public partial class MyAttend_Reg_Req_App : System.Web.UI.Page
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
                    if (Request.QueryString.Count > 0)
                    {
                        //s  hdnReqid.Value = "1";
                        hdnReqid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                    }
                    editform.Visible = true;

                    getMngLeaveReqstList(Convert.ToInt32(hdnReqid.Value));
                    PopulateEmployeeData();
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
    protected void gvMngLeaveRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        getMngLeaveReqstList(Convert.ToInt32(hdnReqid.Value));
        gvMngLeaveRqstList.PageIndex = e.NewPageIndex;
        gvMngLeaveRqstList.DataSource = dsLeaveRequst;
        gvMngLeaveRqstList.DataBind();
    }
    #endregion

    #region Page Methods
    private void getMngLeaveReqstList(int id)
    {
        gvMngLeaveRqstList.DataSource = null;
        gvMngLeaveRqstList.DataBind();
        string strSelfApprover = "";
        try
        {
            DataTable dsTrDetails = new DataTable();

            dsTrDetails = spm.GetMyRegularizationInboxDetail(id);

            if (dsTrDetails.Rows.Count > 0)
            {
                var getempCode = Convert.ToString(dsTrDetails.Rows[0]["emp_code"]);
                var req_Date = Convert.ToString(dsTrDetails.Rows[0]["RequestedDate"]);
                lpm.Emp_Code = getempCode;
                txtDate.Text = req_Date;
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
                txtEmp_Name.Text = (string)dtEmp.Rows[0]["Emp_Name"];
                txtEmp_Desigantion.Text = (string)dtEmp.Rows[0]["DesginationName"];
                txtEmp_Department.Text = (string)dtEmp.Rows[0]["Department_Name"];
                txt_Project.Text = (string)dtEmp.Rows[0]["Project"];
                lpm.Grade = Convert.ToString(dtEmp.Rows[0]["Grade"]).Trim();
                lpm.EmailAddress = (string)dtEmp.Rows[0]["Emp_EmailAddress"];
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

            //Find the DropDownList in the Row
            DropDownList ddlLeaveType = (e.Row.FindControl("ddlAproveReject") as DropDownList);

            //Add Default Item in the DropDownList
            ddlLeaveType.Items.Insert(0, new ListItem("Select", "0"));
            ddlLeaveType.Items.Insert(1, new ListItem("Approve", "Approve"));
            ddlLeaveType.Items.Insert(2, new ListItem("Reject", "Reject"));
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
                DropDownList ddl1 = row.FindControl("ddlAproveReject") as DropDownList;
                if (ddl1 != null)
                {
                    var getValddl = Convert.ToString(ddl1.SelectedValue);
                    if (getValddl == "0")
                    {
                        status = 1;
                    }
                }
            }

            if (status == 0)
            {
                var emp_Code = lpm.Emp_Code;
                var CreatedBy = lpm.Emp_Code;
                var qtype = "UpdateAppDetail";
                var Reg_Id = 0;
                var Leave_Reg_Id = 0;
                var Remark = "";
                var LeaveStatus = "";

                emp_Code = txt_EmpCode.Text.ToString();
                CreatedBy = lpm.Emp_Code;
                foreach (GridViewRow row in gvMngLeaveRqstList.Rows)
                {
                    //Finding Dropdown control  
                    DropDownList ddl1 = row.FindControl("ddlAproveReject") as DropDownList;
                    if (ddl1 != null)
                    {
                        var getValddl = Convert.ToString(ddl1.SelectedValue);
                        if (getValddl != "0")
                        {
                            Label Leave_Status = row.FindControl("lblLeave_Status") as Label;
                            var gettxtLeave_StatusRemark = Leave_Status.Text.Trim();

                            Label lblLeave_Req_id = row.FindControl("lblLeave_Req_id") as Label;
                            var getlblatt_id = lblLeave_Req_id.Text.Trim();

                            Label lblReg_id = row.FindControl("lblReg_id") as Label;
                            var getlblReg_id = lblReg_id.Text.Trim();


                            //DateTime temp = DateTime.ParseExact((row.FindControl("lblDate") as Label).Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                            //string att_Date = temp.ToString("yyyy-MM-dd");
                            string att_Date = null;
                            Remark = gettxtLeave_StatusRemark;
                            Leave_Reg_Id = Convert.ToInt32(getlblatt_id);
                            Reg_Id = Convert.ToInt32(getlblReg_id);
                            LeaveStatus = ddl1.SelectedItem.Text;
                            spm.InsertAttDetails(emp_Code, qtype, Reg_Id, Leave_Reg_Id, "Approve", att_Date, LeaveStatus, CreatedBy, Remark,"");
                        }
                    }
                }
                Response.Redirect("~/procs/Attendance.aspx");
                getMngLeaveReqstList(Convert.ToInt32(hdnReqid.Value));
            }
            else
            {
                lblmessage.Text = "Please select approve / reject options";
            }
        }
        catch (Exception ex)
        {

        }
    }
}
