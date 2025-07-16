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

public partial class Leave_Req_View : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    LeaveBalance bl = new LeaveBalance();
    SP_Methods spm = new SP_Methods();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    public DataTable dtEmp, dtleavebal, dtApprover, dtLeaveTypes, dtApproverEmailIds, dtMaxRequestId, dtIntermediate, dtLeaveCalculation, dtHolidays, dtleavedetails;
    public int Leaveid;
    public int leavetype, openbal, avail, rembal, leaveconditionid, totaldays, weekendcount, publicholiday;
    public string filename = "", approveremailaddress, message;
    string holidaydate;

    //Code for Request Details Voew

    String strloginid = "";
    String strempcode = "";
    string Leavestatus = "";
    int statusid;

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


            lblmessage.Text = "";
            // txtToDate.Text = hdlDate.Value;
            lblmessage.Visible = true;
            //   
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                Page.SmartNavigation = true;
                if (Request.QueryString.Count > 0)
                {
                    //s  hdnReqid.Value = "1";
                    hdnReqid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                }
             
                if (!Page.IsPostBack)
                {
                    //txtFromdate.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    //txtToDate.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                                        
                    txtFromfor.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtToFor.Attributes.Add("onkeypress", "return noanyCharecters(event);");

                    txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtToDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");

                    strempcode = Convert.ToString(Session["empcode"]);
                    editform.Visible = true;
                    divbtn.Visible = false;
                    //   divmsg.Visible = false;
                    lblmessage.Text = "";
                     
                    hdnlstfromfor.Value = "Full Day";
                    hdnlsttofor.Value = "Full Day";

                    PopulateEmployeeData();
                    getSelectedEmpLeaveDetails_View();                     
                    setEnableControls();
                    if (Convert.ToString(hflLeavestatus.Value).Trim() == "Approved" )
                    {
                        btnMod.Enabled = false;
                        btnCancel.Enabled = false;
                    }
                    if (Convert.ToString(hflstatusid.Value).Trim() == "2" )
                    {
                        btnMod.Enabled = false;
                        btnCancel.Enabled = true;
                    }
                    if (Convert.ToString(hflstatusid.Value).Trim() == "3" || Convert.ToString(hflstatusid.Value).Trim() == "4")
                    {
                        btnMod.Enabled = true;
                        btnCancel.Enabled = true;
                    }

                    lstApprover.SelectedIndex = 0;

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                    txtLeaveType.BackColor = Color.FromArgb(235, 235, 228);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    private void getSelectedEmpLeaveDetails_View()
    {
        try
        {
           //vmngLeave.Visible = false;
            DataSet dsList = new DataSet();
            DataTable dsapprover = new DataTable();
            DataTable dtlvstatus = new DataTable();

            dtlvstatus = spm.GetLeaveStatus(hdnReqid.Value);

            if (dtlvstatus.Rows.Count > 0)
            {
              Leavestatus   = (string)dtlvstatus.Rows[0]["Status"];
              hflstatusid.Value = (string)dtlvstatus.Rows[0]["Status_id"];
              approveremailaddress = (string)dtlvstatus.Rows[0]["EmailAddress"];

              for (int i = 0; i < dtlvstatus.Rows.Count; i++ )
              {
                  if (Convert.ToString(dtlvstatus.Rows[i]["Status"]).Trim ()=="Approved")
                  {
                     if(Convert.ToString ( hflApproverEmail.Value).Trim ()=="")
                      {
                          hflApproverEmail.Value = Convert.ToString(dtlvstatus.Rows[i]["EmailAddress"]).Trim();
                          hflLeavestatus.Value= Convert.ToString(dtlvstatus.Rows[i]["Status"]).Trim();
                           hflstatusid.Value = Convert.ToString(dtlvstatus.Rows[i]["Status_id"]).Trim();
                      }
                     else
                     {
                         hflApproverEmail.Value +=";" + Convert.ToString(dtlvstatus.Rows[i]["EmailAddress"]).Trim();
                         hflLeavestatus.Value = Convert.ToString(dtlvstatus.Rows[i]["Status"]).Trim();
                         hflstatusid.Value = Convert.ToString(dtlvstatus.Rows[i]["Status_id"]).Trim();
                     }

                  }
                  else
                  {
                          hflApproverEmail.Value = Convert.ToString(dtlvstatus.Rows[i]["EmailAddress"]).Trim();
                          hflLeavestatus.Value= Convert.ToString(dtlvstatus.Rows[i]["Status"]).Trim();
                           hflstatusid.Value = Convert.ToString(dtlvstatus.Rows[i]["Status_id"]).Trim();
                  }
              }
           
            }
           


           
            dsList = spm.getLeaveRequest_MngEdit(hdnReqid.Value);
            if (dsList.Tables != null)
            {
                if (dsList.Tables[0].Rows.Count > 0)
                {
                    txtEmpCode.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Code"]).Trim();
                    hflEmpName.Value  = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Name"]).Trim();
                    hflEmpDesignation.Value  = Convert.ToString(dsList.Tables[0].Rows[0]["DesginationName"]).Trim();
                    hflEmpDepartment.Value  = Convert.ToString(dsList.Tables[0].Rows[0]["Department_Name"]).Trim();
                    txtLeaveType.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Leave_Type_Description"]).Trim();
                    hdnleaveType.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Leave_Type"]).Trim();
                    txtFromdate.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Leave_FromDate"]).Trim();
                    txtToDate.Text   = Convert.ToString(dsList.Tables[0].Rows[0]["Leave_ToDate"]).Trim();
                    txtLeaveDays.Text = Convert.ToString(dsList.Tables[0].Rows[0]["LeaveDays"]).Trim();
                    txtReason.Text = Convert.ToString(dsList.Tables[0].Rows[0]["Reason"]).Trim();
                    hdnleaveid.Value = Convert.ToString(dsList.Tables[0].Rows[0]["leaveTypeid"]).Trim();
                    hdnOldLeaveCount.Value = Convert.ToString(dsList.Tables[0].Rows[0]["LeaveDays"]).Trim();
                    lstLeaveType.SelectedValue  = Convert.ToString(dsList.Tables[0].Rows[0]["leaveTypeid"]).Trim();
                    hdnleaveconditiontypeid.Value = Convert.ToString(dsList.Tables[0].Rows[0]["LeaveConditionTypeid"]).Trim();
                    txtFromfor.Text = Convert.ToString(dsList.Tables[0].Rows[0]["For_From"]).Trim();
                    txtToFor.Text = Convert.ToString(dsList.Tables[0].Rows[0]["For_To"]).Trim();
                    hdnlstfromfor.Value = Convert.ToString(dsList.Tables[0].Rows[0]["For_From"]).Trim();
                    hdnlsttofor.Value = Convert.ToString(dsList.Tables[0].Rows[0]["For_To"]).Trim();
                    lstFromfor.SelectedItem.Text = Convert.ToString(dsList.Tables[0].Rows[0]["For_From"]).Trim();
                    lstTofor.SelectedItem.Text = Convert.ToString(dsList.Tables[0].Rows[0]["For_To"]).Trim();
                    hflstatusid.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Status_id"]).Trim();
                    if (Convert.ToString(dsList.Tables[0].Rows[0]["Status_id"]).Trim() == "5")
                        hflLeavestatus.Value = "Correction";

                }              
            }

            getApproverlist(strempcode, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value));
            
          
            //hflLeavestatus.Value.Equals("Approved") 
  
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    private void getApproverlist(string strempcodes, string reqid, int leavecondtiontypeid)
    {
        DataTable dtapprover = new DataTable();
            dtapprover = spm.GetApproverStatus(strempcodes, reqid, leavecondtiontypeid,"",0);
        
            //if (dtapprover .Rows.Count > 0)
            //{
            //    lstApprover.DataSource = dtapprover ;
            //    lstApprover.DataTextField = "names";
            //    lstApprover.DataValueField = "names";
            //    lstApprover.DataBind();
            //    //hflapprcode.Value =(string) dtapprover.Rows[0]["emp_code"];

            //}
            //else
            //{
            //    lblmessage.Text = "There is no request for approver.";
            //}
            DgvApprover.DataSource = null;
            DgvApprover.DataBind();

            if (dtapprover.Rows.Count > 0)
            {
                DgvApprover.DataSource = dtapprover;
                DgvApprover.DataBind();
            }
    }
    private void setEnableControls()
    {
        if (Convert.ToString(hflstatusid.Value).Trim() != "1" )
        {
            txtFromdate.Enabled = false;
            txtToDate.Enabled = false;
            txtFromfor.Enabled = false;
            txtToFor.Enabled = false;
            txtReason.Enabled = false;
            btnCancel.Enabled = false;
            btnMod.Enabled = false; 
        }
        if (Convert.ToString(hflstatusid.Value).Trim() == "5")
        {
            txtFromdate.Enabled = true;
            txtToDate.Enabled = true;
            txtFromfor.Enabled = true;
            txtToFor.Enabled = true;
            txtReason.Enabled = true;
            btnCancel.Enabled = true;
            btnMod.Enabled = true;
        }
    }

    public void PopulateEmployeeData()
    {
        try
        {

            
            lpm.Emp_Code = Session["Empcode"].ToString();
            txtEmpCode.Text = lpm.Emp_Code;
            //  btnMod.Enabled = false;

            //    txtEmpCode.Enabled = false; 

            //  BindControls();
            dtEmp = spm.GetEmployeeData(lpm.Emp_Code);
            //txtReason.Text = "This is after function";
            if (dtEmp.Rows.Count > 0)
            {

                //myTextBox.Text = (string) dt.Rows[0]["name"];
                lpm.Emp_Status = (string)dtEmp.Rows[0]["Emp_status"];
                if (lpm.Emp_Status == "Resgined")
                {
                    lblmessage.Text = "You are not allowed to apply for any type of leave sicne your employee status is in resignation";
                    //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Information", "alert('You are not allowed to apply for any type of leave sicne your employee status is in resignation')", true);
                    //Response.Write("You are not allowed to apply for any type of leave sicne your employee status is in resignation");
                }
                else
                {

                    lpm.Emp_Name = (string)dtEmp.Rows[0]["Emp_Name"];
                    lpm.Designation_name = (string)dtEmp.Rows[0]["DesginationName"];
                    lpm.department_name = (string)dtEmp.Rows[0]["Department_Name"];
                    lpm.Grade = (string)dtEmp.Rows[0]["Grade"];
                    lpm.EmailAddress = (string)dtEmp.Rows[0]["Emp_EmailAddress"];

                    //txtEmpName.Text = lpm.Emp_Name;
                    //txtDepartment.Text = lpm.department_name;
                    //txtDesig.Text = lpm.Designation_name ;
                    hflGrade.Value = lpm.Grade;
                    hflEmailAddress.Value = lpm.EmailAddress;
                    hflEmpName.Value = lpm.Emp_Name;
                    hflEmpDepartment.Value = lpm.department_name;
                    hflEmpDesignation.Value = lpm.Designation_name;


                    dtleavebal = spm.GetLeaveBalance(lpm.Emp_Code);
                    dgLeaveBalance.DataSource = dtleavebal;
                    dgLeaveBalance.DataBind();

                    dtApprover = spm.GetApproverName(lpm.Emp_Code);

                    if (dtApprover.Rows.Count > 0)
                    {

                        ListBox3.DataSource = dtApprover;
                        ListBox3.DataTextField = "Emp_Name";
                        ListBox3.DataValueField = "APPR_ID";
                        ListBox3.DataBind();



                    }

                    dtIntermediate = spm.GetIntermediateName(lpm.Emp_Code,0,"");
                    if (dtIntermediate.Rows.Count > 0)
                    {
                        lstIntermediate.DataSource = dtIntermediate;
                        lstIntermediate.DataTextField = "Emp_Name";
                        lstIntermediate.DataValueField = "APPR_ID";
                        lstIntermediate.DataBind();
                    }



                    dtLeaveTypes = spm.GetLeaveType(strempcode);
                    lstLeaveType.DataSource = dtLeaveTypes;
                    lstLeaveType.DataTextField = "Leave_Type_Description";
                    lstLeaveType.DataValueField = "Leavetype_id";
                    lstLeaveType.DataBind();




                    //ddlLeaveType.DataSource = dtLeaveTypes;
                    //ddlLeaveType.DataTextField = "Leave_Type_Description";
                    //ddlLeaveType.DataValueField = "Leavetype_id";
                    //ddlLeaveType.DataBind();





                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }
        //BindLeaveRequestProperties();
        //lpm.Emp_Code = txtEmpCode.Text;


    }

    public void BindLeaveRequestProperties()
    {

        //string[] strdate;
        //string strfromDate = "";
        //string strToDate = "";
        //#region date formatting
        //if (Convert.ToString(txtFromdate.Text).Trim() != "")
        //{
        //    strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
        //    strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        //}
        //if (Convert.ToString(txtToDate.Text).Trim() != "")
        //{
        //    strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
        //    strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        //}

        //#endregion


        lpm.Emp_Code = txtEmpCode.Text;
        lpm.LeaveDays = Convert.ToDouble(txtLeaveDays.Text);
      //  lpm.Leave_Type_id = Convert.ToInt32(txtLeaveType.Text);
        //lpm.Leave_FromDate = Convert.ToDateTime(txtFromdate.Text);
        lpm.Leave_FromDate = txtFromdate.Text ;
        lpm.Leave_From_for = txtFromfor.Text;
        // lpm.Leave_ToDate = Convert.ToDateTime(txtToDate.Text);
        lpm.Leave_ToDate = txtToDate.Text ;
        lpm.Leave_To_For = txtToFor.Text;
        lpm.Reason = txtReason.Text;
        lpm.Grade = hflGrade.Value.ToString();
        lpm.Approvers_code = hflapprcode.Value;
      //  lpm.appr_id = Convert.ToInt32(lstApprover.SelectedValue);
        lpm.EmailAddress = hflEmailAddress.Value;
        lpm.Emp_Name = hflEmpName.Value;
        //Convert.ToInt32(lstApprover.SelectedValue);
    }


    protected void txtLeaveDays_TextChanged(object sender, EventArgs e)
    {


    }
    protected void txtFromdate_TextChanged_old(object sender, EventArgs e)
    {
        if (Convert.ToString(lstLeaveType.SelectedValue).Trim() == "5")
        {
            txtToDate.Text = txtFromdate.Text;
            txtToDate.Enabled = false;
        }

        LeavedaysCalcualation();
        


    }
    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txtLeaveType.Text).Trim() == "")
        {
            lblmessage.Text = "Please select the leave type";
            txtFromdate.Text = "";
            return;
        }

        hdnlsttofor.Value = Convert.ToString(lstTofor.SelectedValue);
        hdnlstfromfor.Value = Convert.ToString(lstFromfor.SelectedValue);

        if (Convert.ToString(txtFromfor.Text).Trim() == "First Half")
        {
            txtToDate.Text = txtFromdate.Text;
            txtToDate.Enabled = false;
        }

        if (Convert.ToString(lstLeaveType.SelectedValue).Trim() == "5")
        {
            txtToDate.Text = txtFromdate.Text;
            txtToDate.Enabled = false;
        }
        else
        {
            txtToDate.Enabled = true;
        }

        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            LeavedaysCalcualation();


        }

        //  lblmessage.Text = "";
        // txtToDate.Text = txtFromdate.Text;



    }
    protected void txtToDate_TextChanged_old(object sender, EventArgs e)
    {

        if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
        {
            lblmessage.Text = "";
            LeavedaysCalcualation();
        }

    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            lblmessage.Text = "";
            hdnlsttofor.Value = Convert.ToString(lstTofor.SelectedValue);
            hdnlstfromfor.Value = Convert.ToString(lstFromfor.SelectedValue);

            LeavedaysCalcualation();

            #region LeaveConditionid
            //if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
            //{
            //  //  hdnleaveconditiontypeid.Value = Convert.ToString(spm.getLeaveConditionTypeId(Convert.ToInt32(lstLeaveType.SelectedValue), Convert.ToDouble(txtLeaveDays.Text)));
            //    getApproverdata();
            //    getIntermidateslist();
            //}
            #endregion

        }


    }
    protected void ddlFromFor_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MyLeave_Req.aspx");
    }
    protected void btnSave_Click1(object sender, EventArgs e)
    {
        try
        {
            #region Check All Parameters Selected
            lblmessage.Text = "";
            if (Convert.ToString(lstLeaveType.SelectedValue).Trim() == "")
            {
                lblmessage.Text = "Please select Leave Type";
                return;
            }
            if (Convert.ToString(txtFromdate.Text).Trim() == "")
            {
                lblmessage.Text = "Please select From Date";
                return;
            }
            if (Convert.ToString(txtToDate.Text).Trim() == "")
            {
                lblmessage.Text = "Please select To Date";
                return;
            }
            if (Convert.ToString(txtLeaveDays.Text).Trim() == "")
            {
                lblmessage.Text = "Leave Days not calculated.Please try again";
                return;
            }
            #endregion

            lblmessage.Text = "";

            BindLeaveRequestProperties();
            if (Convert.ToString(hdnFromFor.Value).Trim() == "First Half" || Convert.ToString(hdnFromFor.Value).Trim() == "Second Half")
            {
                txtToDate.Text = txtFromdate.Text;
            }
            if (hflLeavestatus.Value.Equals("Pending") || (hflLeavestatus.Value.Equals("Correction") && hflstatusid.Value == "5") || ((hflLeavestatus.Value.Equals("Approved") && hflstatusid.Value == "2")))
            {
                string[] strdate;
                string strfromDate = "";
                string strToDate = "";
                #region date formatting
                if (Convert.ToString(txtFromdate.Text).Trim() != "")
                {
                    strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                    strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                }
                if (Convert.ToString(txtToDate.Text).Trim() != "")
                {
                    strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                    strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                }

                #endregion

               // spm.UpdateLeaveRequest(lpm.Emp_Code,hdnReqid.Value ,lpm.Leave_FromDate, lpm.Leave_From_for, txtToDate.Text, txtReason.Text, lpm.LeaveDays, lpm.Reason, Convert.ToDouble(hdnOldLeaveCount.Value), Convert.ToInt32(hdnleaveid.Value));
               // spm.UpdateLeaveRequest(lpm.Emp_Code, hdnReqid.Value, strfromDate, lpm.Leave_From_for, strToDate, txtReason.Text, lpm.LeaveDays, lpm.Reason, Convert.ToDouble(hdnOldLeaveCount.Value), Convert.ToInt32(hdnleaveid.Value));
                spm.UpdateLeaveRequest(lpm.Emp_Code, hdnReqid.Value, strfromDate, lpm.Leave_From_for, strToDate, lpm.Leave_To_For, lpm.LeaveDays, lpm.Reason, Convert.ToDouble(hdnOldLeaveCount.Value), Convert.ToInt32(hdnleaveid.Value), Convert.ToInt32(hdnleaveconditiontypeid.Value),"");
                spm.InsertApproverRequest(hflapprcode.Value, Convert.ToInt32(hdnaproverid.Value), Convert.ToDecimal(dtMaxRequestId.Rows[0]["maxreqid"]));

                String strLeaveRstURL = "";
                strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_LR"]).Trim() + "?reqid=" + Convert.ToDecimal(dtMaxRequestId.Rows[0]["maxreqid"]) + "&itype=0";

                if (lstLeaveType.SelectedValue == "3")
                    spm.send_mailto_RM_Approver(lpm.Emp_Name, lpm.EmailAddress, hflApproverEmail.Value, "Leave Request", txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, lpm.Leave_FromDate, lpm.Leave_From_for, lpm.Leave_ToDate, lpm.Leave_To_For, Convert.ToString(ConfigurationManager.AppSettings["MLMailId_HR"]).Trim(), "","","");
                else if (lstLeaveType.SelectedValue=="4")
                    spm.send_mailto_RM_Approver(lpm.Emp_Name, lpm.EmailAddress, hflApproverEmail.Value, "Leave Request", txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, lpm.Leave_FromDate, lpm.Leave_From_for, lpm.Leave_ToDate, lpm.Leave_To_For, Convert.ToString(ConfigurationManager.AppSettings["LWPMailId_HR"]).Trim(), "","","");
                else
                    spm.send_mailto_RM_Approver(lpm.Emp_Name, lpm.EmailAddress, hflApproverEmail.Value, "Leave Request", txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, lpm.Leave_FromDate, lpm.Leave_From_for, lpm.Leave_ToDate, lpm.Leave_To_For, "", strLeaveRstURL,"","");

                lblmessage.Text = "Leave Request Modified and send for the approval";
                Response.Redirect("~/procs/MyLeave_Req.aspx");
             }
    
            else
            {
           
                lblmessage.Text = "Leave Modification is not allowed if it's pending for other approvals";
            }
            ClearCntrols();
     
        }
        catch (Exception)
        {

            throw;
        }


    }

    private void ClearCntrols()
    {
        txtLeaveDays.Text = "";
        txtReason.Text = "";
        txtToDate.Text = "";
        txtLeaveType.Text = "";
        txtFromdate.Text = "";
        lstApprover.Items.Clear();

    }
    protected void lstLeaveType_SelectedIndexChanged_old(object sender, EventArgs e)
    {
        txtLeaveType.Text = lstLeaveType.SelectedItem.Text;
        PopupControlExtender3.Commit(lstLeaveType.SelectedItem.Text);

        txtFromdate.Text = "";
        txtToDate.Text = "";
        txtLeaveDays.Text = "";
        lblmessage.Text = "";

        if (Convert.ToString(lstLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_TO"]).Trim())
        {
            txtToDate.Enabled = false;
            txtToFor.Enabled = false;
        }
        else
        {
            txtToDate.Enabled = true;
            txtToFor.Enabled = true;
        }
    }
    protected void lstLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {

        txtLeaveType.Text = lstLeaveType.SelectedItem.Text;
        lblmessage.Text = "";
        PopupControlExtender3.Commit(lstLeaveType.SelectedItem.Text);
        htnleavetypeid.Value = "";
        htnleavetypeid.Value = lstLeaveType.SelectedValue;
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "validateLeaveType(" + htnleavetypeid.Value + ");", true);

    }
    protected void lstToFor_SelectedIndexChanged_old(object sender, EventArgs e)
    {
        txtToFor.Text = lstTofor.SelectedValue;
        PopupControlExtender2.Commit(lstTofor.SelectedValue);
    }
    protected void lstToFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtToFor.Text = lstTofor.SelectedValue;

        PopupControlExtender2.Commit(lstTofor.SelectedValue);
        hdnlsttofor.Value = Convert.ToString(lstTofor.SelectedValue);
        hdnlstfromfor.Value = Convert.ToString(lstFromfor.SelectedValue);
        TextBox1_TextChanged(sender, e);

        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "validateToFor(" + txtLeaveDays.Text + ");", true);


    }
    public void ClearControls()
    {
        txtLeaveType.Text = "";
        txtFromdate.Text = "";
        txtToDate.Text = "";
        txtFromfor.Text = "";
        txtToFor.Text = "";
        txtReason.Text = "";
        txtLeaveDays.Text = "";
        lblmessage.Text = "";
    }
    protected void lstFromfor_SelectedIndexChanged_old(object sender, EventArgs e)
    {
        
        txtFromfor.Text = lstFromfor.SelectedValue;
        PopupControlExtender1.Commit(lstFromfor.SelectedValue);
      

       //LeavedaysCalcualation();
        //if (Convert.ToString(lstLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_TO"]).Trim() && (txtFromfor.Text == "First Half"))
        //{
        //    lstTofor.Enabled = false;
        //    hdlDate.Value = txtFromdate.Text;
        //    txtToDate.Text = txtFromdate.Text;
        //    txtToDate.Enabled = false;

        //    txtLeaveDays.Text = " " + 0.5;

        //}
        //else if (Convert.ToString(lstLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_SL"]).Trim() && (txtFromfor.Text == "Second Half") || (txtFromfor.Text == "Full Day"))
        //{
        //    txtToDate.Enabled = true;

        //}

        hdnleavedayschk.Value = "0";
        hdnFromFor.Value = txtFromfor.Text;
        if (txtFromfor.Text == "First Half")
        {
            hdnleavedayschk.Value = "0.5";
            txtLeaveDays.Text = hdnleavedayschk.Value;

        }
        LeavedaysCalcualation();



        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "validateFromFor(" + lstLeaveType.SelectedValue + ",'" + lstFromfor.SelectedValue + "'," + hdnleavedayschk.Value + ");", true);

      
    }
    protected void lstFromfor_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFromfor.Text = lstFromfor.SelectedValue;
        //  txtToDate.Text=txtFromdate.Text;
        PopupControlExtender1.Commit(lstFromfor.SelectedValue);
        hdnlstfromfor.Value = Convert.ToString(lstFromfor.SelectedValue);
        hdnlsttofor.Value = Convert.ToString(lstTofor.SelectedValue);
        hdnFromFor.Value = txtFromfor.Text;

        if (Convert.ToString(txtFromfor.Text).Trim() == "First Half")
        {
            txtToDate.Text = txtFromdate.Text;

        }
        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            // LeavedaysCalcualation();
        }
        TextBox1_TextChanged(sender, e);

        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "validateFromFor(" + lstLeaveType.SelectedValue + ",'" + lstFromfor.SelectedValue + "'," + hdnleavedays.Value + ",'" + hdnToDate.Value + "');", true);

    }

    protected void LeavedaysCalcualation()
    {

        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "From date  cannot be blank";
            return;
        }

        if (Convert.ToString(txtToDate.Text).Trim() == "")
        {
            lblmessage.Text = "To date cannot be blank";
            return;
        }

        if (Convert.ToString(hdnlstfromfor.Value).Trim() == "First Half")
        {
            hdnToDate.Value = txtFromdate.Text;
        }

        string[] strdate;
        string strfromDate = "";
        string strToDate = "";
        #region date formatting
        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        #endregion

        lpm.Emp_Code = txtEmpCode.Text;
        int lvid;

        lvid = Convert.ToInt32(lstLeaveType.SelectedValue);

        DataSet dtleavedetails_t = new DataSet();
        //dtleavedetails_t = spm.GetLeaveDetails(strfromDate, strToDate, lvid, lpm.Emp_Code, "");
        if (dtleavedetails_t.Tables[0].Rows.Count > 0)
        {
            totaldays = (int)dtleavedetails_t.Tables[0].Rows[0]["TotalDays"];
            message = (string)dtleavedetails_t.Tables[0].Rows[0]["Message"];
            weekendcount = (int)dtleavedetails_t.Tables[0].Rows[0]["TotalWeekends"];
            publicholiday = (int)dtleavedetails_t.Tables[0].Rows[0]["NoofPublicHoliday"];
        }

        if (Convert.ToString(message).Trim() != "")
        {
            lblmessage.Text = Convert.ToString(message).Trim();
            txtToDate.Text = "";
            txtFromdate.Text = "";
            txtLeaveDays.Text = "";
            hdnFromFor.Value = "";
            hdnlstfromfor.Value = "";
            hdnlsttofor.Value = "";
            return;
        }


        if (Convert.ToString(totaldays).Trim() == "0")
        {
            return;
        }
        #region Check Time of more than One days
        if (lvid == 5 && totaldays > 1)
        {
            lblmessage.Text = "You can not apply more then 1 TO at same time";
            txtLeaveDays.Text = "";
            return;
        }
        #endregion


        #region  get Leave days

        if (totaldays != 0)
        {
            double days = 0;

            days = totaldays;
           
            if (days >= 1)
            {

                if (Convert.ToString(hdnlstfromfor.Value).Trim() == "Second Half" && Convert.ToString(hdnlsttofor.Value).Trim() == "Full Day")
                {
                    days = days - 0.5;
                }
                else if (Convert.ToString(hdnlstfromfor.Value).Trim() == "First Half")
                {
                    days = days - 0.5;
                }
                else if (Convert.ToString(hdnlstfromfor.Value).Trim() == "Second Half" && Convert.ToString(hdnlsttofor.Value).Trim() == "First Half")
                {
                    days = days - 1.0;
                }
                else if (Convert.ToString(hdnlstfromfor.Value).Trim() == "Full Day" && Convert.ToString(hdnlsttofor.Value).Trim() == "First Half")
                {
                    days = days - 0.5;
                }
            }

            txtLeaveDays.Text = days.ToString();
            hdnleavedays.Value = txtLeaveDays.Text;
        }

        #endregion




        #region if Employee Sick levae More than 5 days ----Upload file
        if (Convert.ToString(lstLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_SL"]).Trim() && (totaldays >= 5))
        {
            uploadfile.Enabled = true;
        }
        else
        {
            uploadfile.Enabled = false;
        }
        #endregion

        #region calculate  LeaveConditionid
        if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
        {
            hdnleaveconditiontypeid.Value = Convert.ToString(spm.getLeaveConditionTypeId(Convert.ToInt32(lstLeaveType.SelectedValue), Convert.ToDouble(txtLeaveDays.Text)));
            getApproverlist(txtEmpCode.Text, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value));
        }
        #endregion

    }

    protected void LeavedaysCalcualation_old()
    {
        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "From date or To date cannot be blank";
            return;
        }

        string[] strdate;
        string strfromDate = "";
        string strToDate = "";
        #region date formatting
        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        #endregion

        lpm.Emp_Code = txtEmpCode.Text;
        int lvid;
        double Days = 0;

        lvid = Convert.ToInt32(lstLeaveType.SelectedValue);

        DataSet dtleavedetails_t = new DataSet();
     //   dtleavedetails_t = spm.GetLeaveDetails(strfromDate, strToDate, lvid, lpm.Emp_Code,hdnReqid.Value);
        if (dtleavedetails_t.Tables[0].Rows.Count > 0)
        {
            totaldays = (int)dtleavedetails_t.Tables[0].Rows[0]["TotalDays"];
            message = (string)dtleavedetails_t.Tables[0].Rows[0]["Message"];
            weekendcount = (int)dtleavedetails_t.Tables[0].Rows[0]["TotalWeekends"];
            publicholiday = (int)dtleavedetails_t.Tables[0].Rows[0]["NoofPublicHoliday"];

        }

        if (Convert.ToString(message).Trim() != "")
        {
            lblmessage.Text = Convert.ToString(message).Trim();
            txtToDate.Text = "";
            txtFromdate.Text = "";
            return;
        }



        if (publicholiday > 0)
        {
            lblmessage.Text = "You can not apply leaves on public holiday ";
            txtLeaveDays.Text = "";
            return;
        }


        if (lvid == 5 && totaldays > 1)
        {
            lblmessage.Text = "You can not apply more then 1 TO at same time";
            txtLeaveDays.Text = "";
            return;

        }
        if (lvid == 5 && totaldays == 1)
        {
            if (Convert.ToString(txtFromfor.Text).Trim() == "First Half" || Convert.ToString(txtFromfor.Text).Trim() == "Second Half")
            {
                hdnleavedayschk.Value = "0.5";
            }
            else
            {
                hdnleavedayschk.Value = "1";
            }
        }

        if (lvid != 5)
        {
            if (totaldays != 0)
            {
                double days = 0;
                if (txtLeaveType.Text == "Sick Leave" || txtLeaveType.Text == "Maternity Leave")
                {
                    days = totaldays - weekendcount;
                    //txtLeaveDays.Text = days.ToString();
                    btnMod.Enabled = true;

                }
                else if (txtLeaveType.Text == "Privilege Leave")
                {
                    days = totaldays;
                    //txtLeaveDays.Text = totaldays.ToString();
                    btnMod.Enabled = true;
                }
                else
                {
                    days = totaldays;
                    //txtLeaveDays.Text = totaldays.ToString();
                    btnMod.Enabled = true;

                }

                if (Convert.ToString(txtFromfor.Text).Trim() == "Second Half" && Convert.ToString(txtToFor.Text).Trim() == "Full Day")
                {
                    days = days - 0.5;
                }
                else if (Convert.ToString(txtFromfor.Text).Trim() == "First Half")
                {
                    days = days - 0.5;
                }
                else if (Convert.ToString(txtFromfor.Text).Trim() == "Second Half" && Convert.ToString(txtToFor.Text).Trim() == "First Half")
                {
                    days = days - 1.0;
                }
                else
                {

                }
                hdnleavedayschk.Value = days.ToString();
                if (txtFromdate.Text == "")
                {
                    txtLeaveDays.Text = "";
                }
                else
                {
                    txtLeaveDays.Text = days.ToString();
                }


            }
        }



        // int lvdays = Convert.ToInt32(txtLeaveDays.Text);
        if (Convert.ToString(lstLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_SL"]).Trim() && (totaldays >= 5))
        {
            uploadfile.Enabled = true;
        }
        else
        {
            uploadfile.Enabled = false;
        }

        #region calculate  LeaveConditionid
        if (Convert.ToString(txtLeaveDays.Text).Trim() != "")
        {
            hdnleaveconditiontypeid.Value = Convert.ToString(spm.getLeaveConditionTypeId(Convert.ToInt32(lstLeaveType.SelectedValue), Convert.ToDouble(txtLeaveDays.Text)));
            getApproverlist(txtEmpCode.Text, hdnReqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value));
        }
        #endregion
    }

    protected void txtLeaveType_TextChanged(object sender, EventArgs e)
    {

        //txtFromdate.Text = "";
        //txtToDate.Text = "";
        //txtLeaveDays.Text = "";
        //lblmessage.Text = "";

        //if (Convert.ToString(lstLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_TO"]).Trim())
        //{
        //    txtToDate.Enabled = false;
        //    txtToFor.Enabled = false;
        //}
        //else
        //{
        //    txtToDate.Enabled = true;
        //    txtToFor.Enabled = true;
        //}
    }
  
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        #region Check All Parameters Selected
        lblmessage.Text = "";
        if (Convert.ToString(lstLeaveType.SelectedValue).Trim() == "")
        {
            lblmessage.Text = "Please select Leave Type";
            return;
        }
        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "Please select From Date";
            return;
        }
        if (Convert.ToString(txtToDate.Text).Trim() == "")
        {
            lblmessage.Text = "Please select To Date";
            return;
        }
        if (Convert.ToString(txtLeaveDays.Text).Trim() == "")
        {
            lblmessage.Text = "Leave Days not calculated.Please try again";
            return;
        }
        #endregion

        BindLeaveRequestProperties();

        if (hflLeavestatus.Value.Equals("Pending") || ((hflLeavestatus.Equals("Approved") && hflstatusid.Value == "2")))
        {
            spm.CancelLeaveRequest(hdnReqid.Value, lpm.Emp_Code, lpm.LeaveDays, Convert.ToInt32(hdnleaveid.Value),"");

            //spm.send_mailto_Cancel_Intermediate(hflEmailAddress.Value, hflApproverEmail.Value, "Leave Request Canceled", txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, lpm.Leave_FromDate, lpm.Leave_From_for, lpm.Leave_ToDate, lpm.Leave_To_For);

            lblmessage.Text = "Leave Cancelation Done and Notification has been send to your Reporting Manager";
            Response.Redirect("~/procs/MyLeave_Req.aspx");
        }

        else
        {
           
            lblmessage.Text = "Leave cancellation is not allowed if it's pending for other approvals";
        }
        ClearCntrols();

        
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        LeavedaysCalcualation();
       
    }
    protected void txtFromfor_TextChanged(object sender, EventArgs e)
    {
        LeavedaysCalcualation();
    }
}
 