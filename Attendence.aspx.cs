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

public partial class Attendence : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    String strloginid = "";
    String strempcode;
    SP_Methods spm = new SP_Methods();

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
            strempcode = Session["Empcode"].ToString();

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    divbtn.Visible = false;
                    divmsg.Visible = false;

                    // txtRequest_Date.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtRequest_Date.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    DisplayProfileProperties();
                    getSelectedEmpDetails_View();
                    getEmpAttendanceDetails_List();
                    getApproverdata();
                    this.lstApprover.SelectedIndex = 0;
                    if (dgAttendanceReg.Rows.Count == 0)
                    {
                        att_submit.Visible = false;
                        lblmessage.Text = "No records for Attendance Regularization.";

                    }
                    loadorder();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
    protected void dgAttendanceReg_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlcategory = (DropDownList)e.Row.FindControl("ddlcategory");
                TextBox txtReason = (TextBox)e.Row.FindControl("txtReason");
                string strtime = "";
                string strInOut = "";
                strtime = Convert.ToString(e.Row.Cells[2].Text).Trim();
                strInOut = Convert.ToString(e.Row.Cells[1].Text).Trim();
                get_AttendanceLeaveTypeList(ddlcategory, strInOut, strtime);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(txtRequest_Date.Text).Trim() == "")
        {
            lblmessage.Text = "Please Select Attendance regularize date";
            return;
        }
        Add_Attendance_Regularise_emp();
    }
    #endregion

    #region Page Methods

    private void getSelectedEmpDetails_View()
    {
        try
        {
            DataTable dtEmp = spm.GetEmployeeData(strempcode);
            if (dtEmp.Rows.Count > 0)
            {
                //myTextBox.Text = (string) dt.Rows[0]["name"];
                txtEmpCode.Text = strempcode;
                txtEmpName.Text = Convert.ToString(dtEmp.Rows[0]["Emp_Name"]).Trim();
                txtDesignation.Text = Convert.ToString(dtEmp.Rows[0]["DesginationName"]).Trim();
                txtDepartment.Text = Convert.ToString(dtEmp.Rows[0]["Department_Name"]).Trim();
                hdnemp_email.Value = Convert.ToString(dtEmp.Rows[0]["Emp_EmailAddress"]).Trim();
                txtRequest_Date.Text = Convert.ToString(dtEmp.Rows[0]["reqdate"]).Trim();
                hflGrade.Value = Convert.ToString(dtEmp.Rows[0]["grade"]).Trim();

            }
            else
            {
                Response.Write("Invalid Employee Code");
            }

        }
        catch (Exception ex)
        {

        }
    }
    private void getEmpAttendanceDetails_List()
    {
        try
        {
            dgAttendanceReg.Visible = true;
            DataSet dsList = new DataSet();
            dsList = spm.getAttendanceRegularisation_Edit(strempcode);
            dgAttendanceReg.DataSource = null;
            dgAttendanceReg.DataBind();
            hdnisSelfAppr.Value = "";
            if (dsList.Tables != null)
            {
                if (dsList.Tables[0].Rows.Count > 0)
                {
                    dgAttendanceReg.DataSource = dsList.Tables[0];
                    dgAttendanceReg.DataBind();
                }
                if (dsList.Tables[1].Rows.Count > 0)
                {
                    hdnisSelfAppr.Value = Convert.ToString(dsList.Tables[1].Rows[0]["emp_IsSelftAppr"]);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void getApproverdata()
    {
        hdnleaveconditiontypeid.Value = "11";
        DataSet ldstmp = new DataSet();
        DataTable dtApproverEmailIds;
        //dtApproverEmailIds = spm.GetApproverEmailID(strempcode, hflGrade.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value));
        if (Convert.ToString(hdnisSelfAppr.Value).Trim()=="Y")
            ldstmp = get_HRApproval_mailsDtls();
        else
        ldstmp = spm.GetApproverEmailID_Attendance(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value));
        //IsEnabledFalse (true);
        lstApprover.Items.Clear();
        if (ldstmp.Tables[0].Rows.Count > 0)
        {
            hdnAppr_Cnt.Value = Convert.ToString(ldstmp.Tables[0].Rows.Count);
            string strApprname = "";
            for (Int32 irow = 0; irow < ldstmp.Tables[0].Rows.Count; irow++)
            {
                strApprname = Convert.ToString(ldstmp.Tables[0].Rows[irow]["Emp_Name"]).Trim();
                if (irow == 0)
                {
                    ldstmp.Tables[0].Rows[irow]["Emp_Name"] = "Approver 1: " + strApprname;
                }
                if (irow == 1)
                {
                    ldstmp.Tables[0].Rows[irow]["Emp_Name"] = "Approver 2: " + strApprname;
                }
                if (irow == 2)
                {
                    ldstmp.Tables[0].Rows[irow]["Emp_Name"] = "Approver 3: " + strApprname;
                }
            }

            dtApproverEmailIds = ldstmp.Tables[0];

            hdnApproverMailid.Value = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];
            hdnApproverid.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["APPR_ID"]).Trim();

            //lstApprover.DataSource = null;
            //lstApprover.DataBind();

            lstApprover.DataSource = dtApproverEmailIds;
            lstApprover.DataTextField = "Emp_Name";
            lstApprover.DataValueField = "APPR_ID";
            lstApprover.DataBind();

            //hdn.Approvers_code = (string)dtApproverEmailIds.Rows[0]["A_EMP_CODE"];
            hflapprcode.Value = (string)dtApproverEmailIds.Rows[0]["A_EMP_CODE"];

        }
        else
        {
            lblmessage.Text = "Since you are not assigned under any approver you can not apply  for any leave, please contact HR";

        }


    }

    private DataSet get_HRApproval_mailsDtls()
    {
        DataSet dsList = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_HREmailAttend_isSelfAppr";

            spars[1] = new SqlParameter("@approver_type", SqlDbType.VarChar);
            spars[1].Value = "HRLWP";

            spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;


            
            dsList = spm.getDatasetList(spars, "Usp_getEmployee_Details_All");

            if (dsList.Tables != null)
            {
                if (dsList.Tables[0].Rows.Count > 0)
                {
                    hdnHR_ApproverCode.Value = Convert.ToString(dsList.Tables[0].Rows[0]["A_EMP_CODE"]).Trim();
                    hdnHR_EMailID.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
                    hdnHR_Appr_id.Value = Convert.ToString(dsList.Tables[0].Rows[0]["APPR_ID"]).Trim();
                    hdnHR_Appr_Name.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Name"]).Trim();
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
        return dsList;
    }

    private void get_AttendanceLeaveTypeList(DropDownList ddlcategory, string strInOut, string strtime)
    {
        try
        {
            String strInOutTime = "";
            strInOutTime = Convert.ToString(strtime).Trim().Replace(':', '.');
            string strLC_EG = "";
            if (Convert.ToString(strInOut).Trim() == "In")
            {
                if (Convert.ToString(strInOutTime).Trim() != "")
                {
                    if (Convert.ToDouble(strInOutTime) > 10.00)
                    {
                        Double dct = Convert.ToDouble(strInOutTime);
                        strLC_EG = "LC";
                    }
                }
            }

            if (Convert.ToString(strInOut).Trim() == "Out")
            {
                if (Convert.ToString(strInOutTime).Trim() != "")
                {
                    if (Convert.ToDouble(strInOutTime) < 16.00)
                    {
                        Double dct = Convert.ToDouble(strInOutTime);
                        strLC_EG = "EG";
                    }
                }
            }

            DataSet dsList = new DataSet();
            dsList = spm.getAttendance_TypeList(strLC_EG, strInOut);
            ddlcategory.Items.Clear();
            if (dsList.Tables[0].Rows.Count > 0)
            {
                ddlcategory.DataSource = dsList.Tables[0];
                // Modified by R1 on 03-10-2018
                //ddlcategory.DataTextField = "Leave_Type_Description";
                ddlcategory.DataTextField = "Leave_Type";
                // Modified by R1 on 03-10-2018
                ddlcategory.DataValueField = "Leavetype_id";
                ddlcategory.DataBind();
            }

            ddlcategory.Items.Insert(0, new ListItem("Select Category", "0"));
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    protected string GetApprove_List()
    {
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();

        if (lstApprover.Items.Count > 0)
        {
            sbapp.Append("<table style='width:300px'>");
            for (int i = 0; i < lstApprover.Items.Count; i++)
            {
                sbapp.Append("<tr>");
                //sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
                sbapp.Append("<td>" + Convert.ToString(lstApprover.Items[i].Text) + "</td>");
                sbapp.Append("</tr>");
            }
            sbapp.Append("</table>");
        }

        return Convert.ToString(sbapp);
    }
    private void Add_Attendance_Regularise_emp()
    {
        string[] strdate;
        string strreqDate;
        Int32 iapp_id = 0;

        if (Convert.ToString(Session["chkbtnStatus_Appr"]).Trim() != "")
        {
            if (dgAttendanceReg.Rows.Count <= 0)
            {
                Response.Redirect("~/procs/Leaves.aspx");
            }
        }
        Session["chkbtnStatus_Appr"] = "Approved button Event is Submitted";

        #region commented Code
        //DataSet dsApproverid = new DataSet();
        //dsApproverid = spm.getEmp_immediateApprover_id(strempcode);

        //if (dsApproverid.Tables != null)
        //{
        //    if (dsApproverid.Tables[0].Rows.Count > 0)
        //    {
        //        strappr_empcpde = Convert.ToString(dsApproverid.Tables[0].Rows[0]["A_EMP_CODE"]).Trim();
        //        hdnApproverMailid.Value = Convert.ToString(dsApproverid.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();

        //        if (Convert.ToString(dsApproverid.Tables[0].Rows[0]["APPR_ID"]).Trim() != "")
        //            iapp_id = Convert.ToInt32(dsApproverid.Tables[0].Rows[0]["APPR_ID"]);
        //    }
        //}
        //else
        //{
        //    lblmessage.Text = "you can not regularise attendance .Please contact to admin....";
        //    return;
        //}
        #endregion
        iapp_id = Convert.ToInt32(hdnApproverid.Value);
        Boolean blnchk = false;

        #region Validatoins for Attendance
        foreach (GridViewRow gvr in dgAttendanceReg.Rows)
        {
            CheckBox chkisRegularise = (CheckBox)gvr.FindControl("chkisRegularise");
            TextBox txtReason = (TextBox)gvr.FindControl("txtReason");
            DropDownList ddlcategory = (DropDownList)gvr.FindControl("ddlcategory");

            //Commented by R1 on 01-10-2018
            //if (chkisRegularise.Checked == true)
            //{
                if (Convert.ToString(ddlcategory.SelectedValue).Trim() == "0")
                {
                    lblmessage.Text = "Please select Category to regularise attendance.";
                    return;
                }
                //Added by R1 on 03-10-2018
                if (Convert.ToString(ddlcategory.SelectedValue).Trim() == "10")
                {
                    lblmessage.Text = "please apply Leave for the date where (LV-Leave) Category is selected";
                    return;
                }
                //Added by R1 on 03-10-2018
                if (Convert.ToString(txtReason.Text).Trim() == "")
                {
                    lblmessage.Text = "Please enter Reason/Remark for selected record.";
                    return;
                }
            //Commented by R1 on 01-10-2018
            //}
                //Commented by R1 on 01-10-2018
            //if (chkisRegularise.Checked)
            //{
            //    blnchk = true;
            //}
            //Commented by R1 on 01-10-2018
                blnchk = true;
        }
        //Commented by R1 on 01-10-2018
        //if (blnchk == false)
        //{
        //    lblmessage.Text = "Please select record to regularise attendance.";
        //    return;
        //}
        //Commented by R1 on 01-10-2018
        #endregion

        #region date formatting
        if (Convert.ToString(txtRequest_Date.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtRequest_Date.Text).Trim().Split('-');
            strreqDate = Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]) + "-" + Convert.ToString(strdate[2]);
        }
        else
        {
            strreqDate = "";
        }

        #endregion


        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }

        
        String strApproverList = "";
        strApproverList = Convert.ToString(GetApprove_List());

        DataSet dtleavedetails_t = new DataSet();
        dtleavedetails_t = spm.InsertAttendanceRequest(strempcode, strreqDate);
        if (Convert.ToString(dtleavedetails_t.Tables[0].Rows[0]["Req_id"]).Trim() != "")
        {
            hdnReqid.Value = Convert.ToString(dtleavedetails_t.Tables[0].Rows[0]["Req_id"]);
            spm.InsertAtt_ApproverRequest(Convert.ToString(hflapprcode.Value).Trim(), iapp_id, Convert.ToDecimal(hdnReqid.Value));
        }

        StringBuilder strsql = new StringBuilder();

        strsql.Append("Insert into tbl_emp_attendance_Regularize (req_id,att_id,emp_code,att_date,att_type,att_time,att_status,att_category,att_reason_remark,regularise_dt,is_selected,request_date,is_exprt_sap) values");

        StringBuilder strsql_approver = new StringBuilder();
        //strsql_approver.Append("Insert into tbl_emp_attendance_approval_status (att_id,emp_code,approver_id,approver_status,approver_emp_code,created_on) values");
        strsql_approver.Append("Insert into tbl_attendance_req_details (req_id,att_id,emp_code,approver_id,approver_status,approver_emp_code,created_on) values");

        StringBuilder strval = new StringBuilder();
        StringBuilder strval_approver = new StringBuilder();

        string[] strattdate;
        string strattDate;
        StringBuilder strbuidValues = new StringBuilder();
        StringBuilder strbuidValues_approver = new StringBuilder();
        Decimal dattid = 0;

        foreach (GridViewRow gvr in dgAttendanceReg.Rows)
        {
            CheckBox chkisRegularise = (CheckBox)gvr.FindControl("chkisRegularise");
            TextBox txtReason = (TextBox)gvr.FindControl("txtReason");
            DropDownList ddlcategory = (DropDownList)gvr.FindControl("ddlcategory");

            //Commented by R1 on 01-10-2018
            //if (chkisRegularise.Checked)
            //{
                if (Convert.ToString(ddlcategory.SelectedValue) != "0")
                {

                    #region date formatting
                    if (Convert.ToString(gvr.Cells[0].Text).Trim() != "")
                    {
                        strattdate = Convert.ToString(gvr.Cells[0].Text).Trim().Split('/');
                        strattDate = Convert.ToString(strattdate[1]) + "-" + Convert.ToString(strattdate[0]) + "-" + Convert.ToString(strattdate[2]);
                    }
                    else
                    {
                        strattDate = "";
                    }

                    #endregion

                    dattid = Convert.ToDecimal(dgAttendanceReg.DataKeys[gvr.RowIndex].Values[0]);

                    strval.Clear();
                    strval.Length = 0;

                    strval_approver.Clear();
                    strval_approver.Length = 0;

                    strval.Append("  (" + Convert.ToInt32(hdnReqid.Value) + "," + dattid + ",'" + strempcode + "','" + strattDate + "','" + Convert.ToString(gvr.Cells[1].Text).Trim() + "','" + Convert.ToString(gvr.Cells[2].Text).Trim() + "','" + Convert.ToString(gvr.Cells[3].Text).Trim() + "','" + Convert.ToString(ddlcategory.SelectedItem.Text) + "',");
                    if (Convert.ToString(txtReason.Text).Trim() != "")
                        strval.Append(" '" + Convert.ToString(txtReason.Text).Trim().Replace("'","") + "'");
                    else
                        strval.Append(" NULL");

                    strval.Append(" ,'" + strreqDate + "','true',GETDATE(),'N') ");


                    if (Convert.ToString(strbuidValues).Trim() == "")
                        strbuidValues.Append(strval);
                    else
                        strbuidValues.Append("," + strval);

                    strval_approver.Append(" (" + Convert.ToInt32(hdnReqid.Value) + "," + dattid + ",'" + strempcode + "'," + iapp_id + ",'Pending','" + hflapprcode.Value + "',getdate())");

                    if (Convert.ToString(strbuidValues_approver).Trim() == "")
                        strbuidValues_approver.Append(strval_approver);
                    else
                        strbuidValues_approver.Append("," + strval_approver);

                }
            //Commented by R1 on 01-10-2018
            //}
        }
        Int32 irecupdate = 0;
        if (Convert.ToString(strbuidValues).Trim() != "")
            irecupdate = spm.add_Regularise_Attendance_emp(Convert.ToString(strsql).Trim() + Convert.ToString(strbuidValues).Trim() + "; " + Convert.ToString(strsql_approver).Trim() + Convert.ToString(strbuidValues_approver).Trim());

        if (irecupdate != 0)
        {
            lblmessage.Text = "Attendance Regularization updated successfully.";
            //Send Mail to Approver.
            StringBuilder strbuild = new StringBuilder();
            strbuild.Length = 0;
            strbuild.Clear();

            strbuild.Append("<table border='1'>");
            strbuild.Append("<tr style='background-color:#C5BE97'><td>Date</td><td>Type</td><td>Time</td><td>Status</td><td>Category</td><td>Reason/Remarks</td></tr>");
            foreach (GridViewRow gvr in dgAttendanceReg.Rows)
            {
                CheckBox chkisRegularise = (CheckBox)gvr.FindControl("chkisRegularise");
                TextBox txtReason = (TextBox)gvr.FindControl("txtReason");
                DropDownList ddlcategory = (DropDownList)gvr.FindControl("ddlcategory");
                //Commented by R1 on 01-10-2018
                //if (chkisRegularise.Checked)
                //{
                    if (Convert.ToString(ddlcategory.SelectedValue) != "0")
                    {
                        strbuild.Append("<tr><td>" + Convert.ToString(gvr.Cells[0].Text) + "</td><td>" + Convert.ToString(gvr.Cells[1].Text) + "</td><td>" + Convert.ToString(gvr.Cells[2].Text) + "</td><td>" + Convert.ToString(gvr.Cells[3].Text) + "</td><td>" + Convert.ToString(ddlcategory.SelectedItem.Text) + "</td><td>" + Convert.ToString(txtReason.Text) + "</td></tr>");
                    }
                //Commented by R1 on 01-10-2018
                //}
            }
            strbuild.Append("</table>");

            String strAttendRstURL = "";
            if (Convert.ToString(hdnisSelfAppr.Value) == "Y")
                strAttendRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_AR"]).Trim() + "?ec=" + Convert.ToString(strempcode) + "&reqid=" + hdnReqid.Value + "&appid=" + hdnApproverid.Value + "&app_type=1";
            else
            strAttendRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_AR"]).Trim() + "?ec=" + Convert.ToString(strempcode) + "&reqid=" + hdnReqid.Value + "&appid=" + hdnApproverid.Value + "&app_type=0";

            spm.send_mailto_Attenance_RM_Approver(txtEmpName.Text, hdnApproverMailid.Value, "Request for Attendance Regularisation of " + txtEmpName.Text, Convert.ToString(strbuild), strAttendRstURL, strApproverList);

            //Fill Datagridview
            getEmpAttendanceDetails_List();

            if (dgAttendanceReg.Rows.Count <= 0)
            {
                Response.Redirect("~/procs/Leaves.aspx");
            }

        }

    }

    #endregion

    #region Code not in use
  
    public void BindLeaveRequestProperties()
    {
        //   lpm.Emp_Code = txtEmpCode.Text;
        //   lpm.LeaveDays = Convert.ToInt32(txtLeaveDays.Text);
        //   lpm.Leave_Type_id = Convert.ToInt32(ddlLeaveType.SelectedValue);
        ////   lpm.Leave_FromDate = Convert.ToDateTime(txtFromdate.Text);
        //   lpm.Leave_From_for = ddlFromFor.Text;
        ////   lpm.Leave_ToDate = Convert.ToDateTime(txtToDate.Text);
        //   lpm.Leave_To_For = ddlToFor.Text;
        //   lpm.Reason = txtReason.Text;
        //   lpm.Grade = hflGrade.Value.ToString();
        //   lpm.Approvers_code = hflapprcode.Value;
        //   lpm.appr_id = Convert.ToInt32(lstApprover.SelectedValue);
        //   lpm.EmailAddress = hflEmailAddress.Value;
        //   lpm.Emp_Name = hflEmpName.Value;

    }   
    protected void loadorder()
    {
        DataTable dtp = classpkg.getcountorderdetailbyemaild(Page.User.Identity.Name.ToString().Trim());
        if (dtp.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtp.Rows[0]["orderid"].ToString()) == 0)
            {
                lihistory.Visible = false;
            }
        }
        else
        {
            lihistory.Visible = true;
        }
    }
    private void DisplayProfileProperties()
    {
        try
        {
            Boolean varfindcity = false;

            MembershipUser user = Membership.GetUser(this.Page.User.Identity.Name.ToString().Trim());
            DataSet ds_userdetails = classaddress.getalluserbyusername(this.Page.User.Identity.Name.ToString().Trim());
            if (ds_userdetails.Tables.Count > 0)
            {
                if (ds_userdetails.Tables[0].Rows.Count > 0)
                {
                    // txtemail.Text = ds_userdetails.Tables[0].Rows[0]["username"].ToString();
                    // txtemailadress.Text = ds_userdetails.Tables[0].Rows[0]["emailid"].ToString();
                    //txtfirstname.Text = ds_userdetails.Tables[0].Rows[0]["firstname"].ToString();
                    //txtlastname.Text = ds_userdetails.Tables[0].Rows[0]["lastname"].ToString();
                    //txtaddress1.Text = ds_userdetails.Tables[0].Rows[0]["address"].ToString();
                    // txtpincode.Text = ds_userdetails.Tables[0].Rows[0]["pincode"].ToString();
                    //txtphone.Text = ds_userdetails.Tables[0].Rows[0]["telno"].ToString();
                    //txtmobile.Text = ds_userdetails.Tables[0].Rows[0]["mobileno"].ToString();
                    //txttempaddress.Text = ds_userdetails.Tables[0].Rows[0]["tempaddress"].ToString();
                    //txtaltemail.Text = ds_userdetails.Tables[0].Rows[0]["alternateemail"].ToString();
                    //txtextension.Text = ds_userdetails.Tables[0].Rows[0]["extentionno"].ToString();
                    //txtoffno.Text = ds_userdetails.Tables[0].Rows[0]["officemob"].ToString();
                    //txtaltno.Text = ds_userdetails.Tables[0].Rows[0]["alternatemob"].ToString();
                    //txtoffphone.Text = ds_userdetails.Tables[0].Rows[0]["officephone"].ToString();
                    //txtfaxno.Text = ds_userdetails.Tables[0].Rows[0]["faxno"].ToString();
                    //  txtloc.Text = ds_userdetails.Tables[0].Rows[0]["location"].ToString();
                    //txtdept.Text = ds_userdetails.Tables[0].Rows[0]["department"].ToString();
                    //txtsubdept.Text = ds_userdetails.Tables[0].Rows[0]["sub_department"].ToString();
                    //txtdesg.Text = ds_userdetails.Tables[0].Rows[0]["designation"].ToString();

                    //DateTime dob1 = new DateTime();

                    //if (ds_userdetails.Tables[0].Rows[0]["DOB1"].ToString().Length > 0 && ds_userdetails.Tables[0].Rows[0]["DOB1"].ToString() != null && ds_userdetails.Tables[0].Rows[0]["DOB1"].ToString() != "")
                    //{
                    //    dob1 = Convert.ToDateTime(ds_userdetails.Tables[0].Rows[0]["DOB1"]);
                    //    if (dob1.ToString("dd/MMM/yyyy") == "01/Jan/1900")
                    //    {
                    //        txtdob1.Text = "";
                    //    }
                    //    else
                    //    {
                    //        txtdob1.Text = dob1.ToString("dd/MMM/yyyy");
                    //    }
                    //}
                    //else
                    //{
                    //    txtdob1.Text = "";
                    //}


                    DateTime dob = new DateTime();

                    if (ds_userdetails.Tables[0].Rows[0]["DOB"].ToString().Length > 0 && ds_userdetails.Tables[0].Rows[0]["DOB"].ToString() != null && ds_userdetails.Tables[0].Rows[0]["DOB"].ToString() != "")
                    {
                        dob = Convert.ToDateTime(ds_userdetails.Tables[0].Rows[0]["DOB"]);
                        if (dob.ToString("dd/MMM/yyyy") == "01/Jan/1900")
                        {
                            // txtdob.Text = "";
                        }
                        else
                        {
                            //  txtdob.Text = dob.ToString("dd/MMM/yyyy");
                        }
                    }
                    else
                    {
                        //txtdob.Text = "";
                    }

                    string gen = ds_userdetails.Tables[0].Rows[0]["gender"].ToString();
                    if (gen == "M" || gen == "m")
                    {
                        //rbtnmale.Checked = true;
                    }
                    else
                    {
                        //rbtnfemale.Checked = true;
                    }


                    DataTable user2 = classreviews.getuseridbyemail(Page.User.Identity.Name);

                    if (user2.Rows.Count > 0)
                    {
                        userid = user2.Rows[0]["indexid"].ToString();
                        if (user2.Rows[0]["profilephoto"].ToString() != "")
                        {
                            pimg = user2.Rows[0]["profilephoto"].ToString().Trim();
                            if (user2.Rows[0]["profilephoto"].ToString().Trim() == "noimage1.png" || user2.Rows[0]["profilephoto"].ToString().Trim() == "noimage3.jpg")
                            {
                                //  removeprofile.Visible = false;
                            }
                            else
                            {
                                // removeprofile.Visible = true;
                            }
                            if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profilephoto/" + user2.Rows[0]["profilephoto"].ToString())))
                            {
                                //  imgprofile.Src = ReturnUrl("sitepath") + "images/profilephoto/" + user2.Rows[0]["profilephoto"].ToString();
                            }
                            else
                            {
                                // imgprofile.Src = "http://graph.facebook.com/" + user2.Rows[0]["profilephoto"].ToString() + "/picture?type=large";
                            }
                        }
                        else
                        {
                            // imgprofile.Src = ConfigurationManager.AppSettings["adminsitepath"]+ "images/noprofile.jpg";
                            // removeprofile.Visible = false;
                        }
                        if (File.Exists(Server.MapPath("~/themes/creative1.0/images/coverphoto/" + user2.Rows[0]["coverphoto"].ToString())))
                        {
                            cimg = user2.Rows[0]["coverphoto"].ToString().Trim();
                            //imgcover.Src = ReturnUrl("sitepath") + "images/coverphoto/" + user2.Rows[0]["coverphoto"].ToString();
                        }
                        else
                        {
                            //imgcover.Visible = false;
                            //removecover.Visible = false;
                        }
                    }
                    else
                    {
                        // imgprofile.Visible = false;
                        //imgcover.Visible = false;
                    }

                    fillcountry();
                    country country = classaddress.GetCountryDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["country"].ToString()));
                    // ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByText(country.countryName.Trim()));

                    fillstate(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["country"].ToString()));
                    states state = classaddress.GetStateDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["state"].ToString()));
                    // ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByText(state.statename.Trim()));

                    fillcity(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["state"].ToString()));

                    city city = classaddress.GetCityDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["city"].ToString()));

                    //  ddlcity1.SelectedValue = ds_userdetails.Tables[0].Rows[0]["city"].ToString().Trim();
                    // txtcity.Text = ddlcity1.SelectedItem.Text.Trim();
                }
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnSave_Click(object sender, System.EventArgs e)
    {

    }
    protected void btnSaveChanges_Click(object sender, System.EventArgs e)
    {




    }
    public void mywall()
    {
        //DataSet dswall = classproduct.gettopmywall();
        //dswall = saveXml2(dswall, "mywall.xml");
        //if (!dswall.Tables[0].Columns.Contains("videoembed") || !dswall.Tables[0].Columns.Contains("filename") || !dswall.Tables[0].Columns.Contains("movietrailorcode") || !dswall.Tables[0].Columns.Contains("bigimage"))
        //{
        //    if (!dswall.Tables[0].Columns.Contains("videoembed"))
        //    {
        //        dswall.Tables[0].Columns.Add("videoembed");
        //    }
        //    if (!dswall.Tables[0].Columns.Contains("movietrailorcode"))
        //    {
        //        dswall.Tables[0].Columns.Add("movietrailorcode");
        //    }
        //    if (!dswall.Tables[0].Columns.Contains("filename"))
        //    {
        //        dswall.Tables[0].Columns.Add("filename");
        //    }
        //    if (!dswall.Tables[0].Columns.Contains("filename"))
        //    {
        //        dswall.Tables[0].Columns.Add("filename");
        //    }
        //    saveXml(dswall, "mywall.xml");
        //}
    }
    //public DataSet saveXml2(DataSet ds, string filename)
    //{
    //    //string fpath = Server.MapPath("~/xml") + "\\" + filename;
    //    //StreamWriter myStreamWriter = new StreamWriter(@"" + fpath);
    //    //ds.WriteXml(myStreamWriter);
    //    //myStreamWriter.Close();
    //    //return ds;
    //}
    public void saveXml(DataSet ds, string filename)
    {
        //string fpath = Server.MapPath("~/xml") + "\\" + filename;
        //StreamWriter myStreamWriter = new StreamWriter(@"" + fpath);
        //ds.WriteXml(myStreamWriter);
        //myStreamWriter.Close();
    }
    //public string GetSafeFileName(string Filename)
    //{
    //    //string newStr = "";
    //    //Filename = Filename.Replace("<", newStr);
    //    //Filename = Filename.Replace(">", newStr);
    //    //Filename = Filename.Replace(" ", newStr);
    //    //Filename = Filename.Replace("%", newStr);
    //    //Filename = Filename.Replace("*", newStr);
    //    //Filename = Filename.Replace("|", newStr);
    //    //Filename = Filename.Replace("-", newStr);
    //    //Filename = Filename.Replace("#", newStr);
    //    //Filename = Filename.Replace("&", newStr);
    //    //Filename = Filename.Replace("@", newStr);
    //    //Filename = Filename.Replace("!", newStr);
    //    //Filename = Filename.Replace("$", newStr);
    //    //Filename = Filename.Replace(" ", newStr);
    //    //return Filename;
    //}
    //public string ReplaceFileName(string str)
    //{
    //    //StringBuilder sb = new StringBuilder();

    //    //for (int i = 0; i < str.Length; i++)
    //    //{
    //    //    if (char.IsLetterOrDigit(str[i]) || char.IsSymbol('.'))
    //    //    {
    //    //        sb.Append(str[i]);
    //    //    }
    //    //}
    //    //return sb.ToString();
    //}
    protected void FCLoginView_ViewChanged(object sender, System.EventArgs e)
    {
        // DisplayProfileProperties();
    }
    public void fillcountry()
    {
        //ddlcountry.Items.Clear();
        //ProfileCommon profile = this.Profile;
        //profile = this.Profile.GetProfile(this.Page.User.Identity.Name);
        //source = new SqlConnection(creativeconfiguration.DbConnectionString);
        //string SqlQuery1 = "select distinct ltrim(rtrim(countryname)) as countryname,countryID from country order by countryname asc";
        //sqladp = new SqlDataAdapter(SqlQuery1, source);
        //DataTable dt = new DataTable();
        //sqladp.Fill(dt);
        //ddlcountry.DataSource = dt;
        //ddlcountry.Items.Clear();
        //ddlcountry.DataTextField = "countryname";
        //ddlcountry.DataValueField = "countryID";
        //ddlcountry.DataBind();
        //ListItem item = new ListItem("--Choose Country--", "0");
        //ddlcountry.Items.Insert(0, item);

        //if (ddlcountry.SelectedItem.Text != "--Choose Country--")
        //{
        //    fillstate(Convert.ToInt32(ddlcountry.SelectedValue));
        //    fillcity(Convert.ToInt32(ddlstate.SelectedValue));
        //}

        //else
        //{

        //    ListItem item1 = new ListItem("--Choose State--", "0");
        //    ddlstate.Items.Insert(0, item1);

        //    ListItem item2 = new ListItem("--Choose City--", "0");
        //    ddlcity1.Items.Insert(0, item2);
        //}

    }
    public void fillstate(int country)
    {
        //DropDownList ddl = new DropDownList();

        //ddl = ddlcountry;
        //source = new SqlConnection(creativeconfiguration.DbConnectionString);
        //string SqlQuery1 = "select ltrim(rtrim(statename)) as statename,stateid from state where countryid=" + country + " order by statename asc";
        //sqladp = new SqlDataAdapter(SqlQuery1, source);
        //DataTable dt = new DataTable();
        //sqladp.Fill(dt);
        //ddlstate.DataSource = dt;
        //ddlstate.Items.Clear();
        //ddlstate.DataTextField = "statename";
        //ddlstate.DataValueField = "stateid";
        //ddlstate.DataBind();
        //ListItem item = new ListItem("--Choose State--", "0");
        //ddlstate.Items.Insert(0, item);

        //if (ddlstate.SelectedItem.Text != "--Choose State--")
        //{           
        //    fillcity(Convert.ToInt32(ddlstate.SelectedValue));
        //}

        //else
        //{

        //    ListItem item2 = new ListItem("--Choose City--", "0");
        //    ddlcity1.Items.Insert(0, item2);
        //}

    }
    public void fillcity(int city)
    {
        //DropDownList ddl = new DropDownList();

        //ddl = ddlstate;

        //source = new SqlConnection(creativeconfiguration.DbConnectionString);
        //string SqlQuery1 = "select ltrim(rtrim(cityname)) as cityname,cityid from city where stateid=" + city + " order by cityname asc";

        //sqladp = new SqlDataAdapter(SqlQuery1, source);
        //DataTable dt = new DataTable();
        //sqladp.Fill(dt);

        //ddlcity1.DataSource = dt;
        //ddlcity1.Items.Clear();
        //ddlcity1.DataTextField = "cityname";
        //ddlcity1.DataValueField = "cityid";

        //DropDownList ddcitylist = new DropDownList();
        //TextBox txtci = new TextBox();
        //int i = 0;

        //ddcitylist = ddlcity1;
        ////ddcitylist.Items.Clear();
        //ListItem lst3 = new ListItem();

        //ddlcity1.DataBind();
        //ListItem item = new ListItem("--Choose City--", "0");
        //ddcitylist.Items.Insert(0, item);

    }
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DropDownList ddslist1 = new DropDownList();
        //ddslist1 = ddlcountry;

        //if (ddslist1.SelectedItem.Value != "--Choose Country--")
        //{
        //    fillstate(Convert.ToInt32(ddlcountry.SelectedItem.Value.ToString()));
        //    fillcity(Convert.ToInt32(ddlstate.SelectedValue));
        //}
    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DropDownList ddslist = new DropDownList();
        //ddslist = ddlstate;
        //if (ddslist.SelectedItem.Value != "--Choose State--")
        //{
        //    fillcity(Convert.ToInt32(ddslist.SelectedItem.Value.ToString()));
        //}
    }
    protected void ddlcity1_SelectedIndexChanged1(object sender, EventArgs e)
    {

        //if (ddlcity1.SelectedValue == "Others")
        //{
        //    pnlothercity.Visible = false;
        //    txtothercity.Text = "";
        //    txtothercity.Visible = false;
        //}
        //else
        //{
        //    txtothercity.Visible = false;
        //}
    }
    protected void ddlprofile_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlprofile.SelectedItem.Value.ToString() == "edit")
        //{
        //    Response.Redirect(ReturnUrl("sitepathmain") + "procs/SampleForm");
        //}
        //else if (ddlprofile.SelectedItem.Value.ToString() == "pwd")
        //{
        //    Response.Redirect(ReturnUrl("sitepathmain") + "procs/changepassword");
        //}
        //else if (ddlprofile.SelectedItem.Value.ToString() == "wishlist")
        //{
        //    Response.Redirect(ReturnUrl("sitepathmain") + "procs/wishlist");
        //}
        //else if (ddlprofile.SelectedItem.Value.ToString() == "preference")
        //{
        //    Response.Redirect(ReturnUrl("sitepathmain") + "procs/preference");
        //}
        //else if (ddlprofile.SelectedItem.Value.ToString() == "subscription")
        //{
        //    Response.Redirect(ReturnUrl("sitepathmain") + "procs/subscriptionhistory");
        //}
        //else if (ddlprofile.SelectedItem.Value.ToString() == "pthistory")
        //{
        //    Response.Redirect(ReturnUrl("sitepathmain") + "procs/pthistory");
        //}
        //else if (ddlprofile.SelectedItem.Value.ToString() == "logout")
        //{
        //    Session.Abandon();
        //    Request.Cookies.Clear();
        //    FormsAuthentication.SignOut();
        //    Response.Redirect(ReturnUrl("sitepathmain") + "default", true);           
        //}

    }
    protected void btnSingOut_Click(object sender, EventArgs e)
    {
        //Session.Abandon();
        //Request.Cookies.Clear();
        //FormsAuthentication.SignOut();
        //Response.Redirect(ReturnUrl("sitepathmain") + "default", true);
    }
    protected void removeprofile_Click(object sender, EventArgs e)
    {
        //bool iserror;
        //try
        //{
        //    iserror=classreviews.insertupdateprofilephoto(Page.User.Identity.Name, "");
        //    if (iserror==false)
        //    {
        //        lblstatus.Text = "Please try again !";               
        //    }
        //    else
        //    {
        //        lblstatus.Visible = true;
        //        filedelete(Request.PhysicalApplicationPath + "themes/creative1.0/images/profile110x110",pimg);
        //        filedelete(Request.PhysicalApplicationPath + "themes/creative1.0/images/profile55x55", pimg);
        //        filedelete(Request.PhysicalApplicationPath + "themes/creative1.0/images/profilephoto", pimg);
        //        lblstatus.Text = "Profile photo removed successfully !";
        //        imgprofile.Visible = false;
        //        removeprofile.Visible = false;
        //    }
        //}
        //catch(Exception ex)
        //{
        //    lblstatus.Text = "Please try again !";
        //}
    }
    protected void removecover_Click(object sender, EventArgs e)
    {
        //bool iserror;
        //try
        //{
        //    iserror = classreviews.insertupdatecoverphoto(Page.User.Identity.Name, "");
        //    if (iserror==false)
        //    {
        //        lblstatus2.Text = "Please try again !";
        //    }
        //    else
        //    {
        //        lblstatus2.Visible = true;
        //        filedelete(Request.PhysicalApplicationPath + "themes/creative1.0/images/coverphoto", cimg);
        //        lblstatus2.Text = "Cover photo removed successfully !";
        //        //imgcover.Visible = false;
        //        //removecover.Visible = false;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    lblstatus2.Text = "Please try again !";
        //}
    }
    protected void profileupload_Click(object sender, EventArgs e)
    {

    }
    protected void lnkcont_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/SampleForm");
    }
    //File delete function
    private void filedelete(string path, string filename)
    {
        //string[] st;
        //st = Directory.GetFiles(path);
        //path += "\\" + filename;
        //int i;
        //if (filename != "noimage1.png" && filename != "noimage3.jpg")
        //{
        //    for (i = 0; i < st.Length; i++)
        //    {
        //        try
        //        {
        //            if (st.GetValue(i).ToString() == path)
        //            {
        //                File.Delete(st.GetValue(i).ToString());
        //            }
        //        }
        //        catch { }
        //    }
        //}
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlsubdept_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddldesg_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txtFromdate.Text = ""; 

        //txtToDate.Text = "";
        //txtLeaveDays.Text = "";


        //if (Convert.ToString(ddlLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_TO"]).Trim())
        //{
        //    txtToDate.Enabled = false;          
        //    ddlToFor.Enabled = false;
        //}
        //else
        //{
        //    txtToDate.Enabled = true;
        //    ddlToFor.Enabled = true;
        //}
    }
    protected void txtLeaveDays_TextChanged(object sender, EventArgs e)
    {
        //int lvdays = Convert.ToInt32(txtLeaveDays.Text);
        //if (Convert.ToString(ddlLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_SL"]).Trim() && (lvdays >= 5))
        //{
        //    uploadprofile.Enabled = false;
        //}
        //else
        //{
        //    uploadprofile.Enabled = true;
        //}

    }
    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        //if (IsPostBack == false)
        //{
        //    DateTime FromDate = Convert.ToDateTime(txtFromdate.Text );
        //    if (FromDate < DateTime.Now)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Information", "alert('Not allowed to apply leave for back dates')", true);
        //        txtFromdate.Text = "";
        //    }
        //}
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        //lpm.Emp_Code = txtEmpCode.Text;
        //; int lvid;
        //double Days = 0;

        //lvid = Convert.ToInt32(ddlLeaveType.SelectedValue);



        ////DateTime FromDate = Convert.ToDateTime(txtFromdate.Text);
        ////DateTime ToDate = Convert.ToDateTime(txtToDate.Text);

        //// Function count no of sat and sun between the Leave Dates
        //int noofweekends = spm.GetWeekends(FromDate, ToDate);

        //// Getting the leave balance based on leave type
        //dtLeaveCalculation = spm.LeaveCalculation(lpm.Emp_Code, lvid);
        //lpm.leave_balance = (double)dtLeaveCalculation.Rows[0]["Balance"];


        //// Gets the date of Holiday based on the selected date
        //dtHolidays = spm.GetHolidayDate(FromDate, ToDate);

        //if (dtHolidays.Rows.Count > 0)
        //{
        //    holidaydate = (DateTime)dtHolidays.Rows[0]["Holiday_Date"];
        //}


        //// Logic to cross check the from date and to date Criteria
        //if (ToDate < FromDate)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", "alert('To Date date should be greate than From Date date')", true);
        //    txtToDate.Text = " ";
        //    txtFromdate.Text = " ";
        //}
        //else if ((FromDate == holidaydate || ToDate == holidaydate))
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", "alert('Holidays cannot be applied on public holiday')", true);
        //    txtToDate.Text = " ";
        //}

        //else
        //{
        //    TimeSpan objTimeSpan = ToDate - FromDate;
        //    Days = Convert.ToDouble(objTimeSpan.TotalDays);
        //    Days = Days + 1;


        //}
        //if ((noofweekends > 0) && (lvid == 1))
        //{
        //    //Days = Days - noofweekends;
        //    txtLeaveDays.Text = Days.ToString();

        //}

        //else if ((noofweekends > 0) && (lvid == 2))
        //{
        //    Days = Days - noofweekends;
        //    txtLeaveDays.Text = Days.ToString();

        //}

        //else
        //{
        //    txtLeaveDays.Text = Days.ToString();
        //}



        //if (Days > lpm.leave_balance)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", "alert('Due to inadequate balance, you can not apply for Leave.')", true);
        //    txtToDate.Text = " ";
        //    txtLeaveDays.Text = " ";

        //}


    }
    protected void ddlFromFor_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click1(object sender, EventArgs e)
    {
        //    try
        //    {
        //        BindLeaveRequestProperties();

        //        MailMessage mail = new MailMessage();
        //        SmtpClient SmtpServer = new SmtpClient();
        //        StringBuilder strbuild = new StringBuilder();


        //        #region fileUpload
        //        if (Convert.ToString(ddlLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_SL"]).Trim() && lpm.LeaveDays > 5)
        //        {
        //            if (uploadprofile.HasFile)
        //            {
        //                filename = Path.Combine(Server.MapPath(""), uploadprofile.FileName);
        //                uploadprofile.SaveAs(filename);

        //            }
        //        }
        //        else
        //        {
        //            filename = "Not Applicable";
        //        }
        //        #endregion

        //        #region LeaveConditionid
        //        if (Convert.ToString(ddlLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_PL"]).Trim() && (lpm.LeaveDays <= 15))
        //        {
        //            leaveconditionid = 2;
        //        }
        //        else if (Convert.ToString(ddlLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_PL"]).Trim() && (lpm.LeaveDays > 15))
        //        {
        //            leaveconditionid = 1;
        //        }
        //        #endregion

        //        #region MaxRequestId

        //        dtMaxRequestId = spm.GetMaxRequestId();
        //        //reqid = (int)dtMaxRequestId.Rows[0]["Request_id"];
        //        //reqid = reqid + 1;

        //        #endregion

        //        #region MethodsCall

        //        spm.InsertLeaveRequest(lpm.Emp_Code, lpm.Leave_Type_id, leaveconditionid, lpm.Leave_FromDate, lpm.Leave_From_for, lpm.Leave_ToDate, lpm.Leave_To_For, lpm.LeaveDays, lpm.Reason, filename);
        //        spm.InsertApproverRequest(lpm.Approvers_code, lpm.appr_id);

        //        dtApproverEmailIds = spm.GetApproverEmailID(lpm.Emp_Code, lpm.Grade, leaveconditionid);
        //        approveremailaddress = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];

        //        spm.send_mailto_RM_Approver(lpm.Emp_Name, lpm.EmailAddress, approveremailaddress, "Leave Request", "Peivillege Leave", lpm.LeaveDays.ToString(), lpm.Reason, lpm.Leave_FromDate.ToShortDateString(), lpm.Leave_From_for, lpm.Leave_ToDate.ToShortDateString(), lpm.Leave_To_For);


        //        //    (string ReqMailID, string toMailIDs, string strsubject, string tType, string tDays, string tRemarks, string tFrom, string tFrom_For, string tTo, string tTo_For, string AppMailID, string Rej_Remarks)
        //        //   spm.sendMail(approveremailaddress, "Leave Request", Convert.ToString(strbuild).Trim(), filename);

        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Information", "alert('Leave Request Submiteed and Email has been send to your Reporting Manager for Approver')", true);

        //        lblmessage.Text = "Leave Request Submiteed and Email has been send to your Reporting Manager for Approver"; 

        //        #endregion

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }


        //}
    }

    #endregion
}
