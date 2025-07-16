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



public partial class Site_Attendance : System.Web.UI.Page
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

                    getSelectedEmpDetails_View();
                    getEmpAttendanceDetails_List();
                    getApproverdata();
                    this.lstApprover.SelectedIndex = 0;
                    if (dgAttendanceReg.Rows.Count == 0)
                    {
                        att_submit.Visible = false;
                        lblmessage.Text = "No records for Attendance Regularization.";

                    }

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
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    TextBox ddlcategory = (TextBox)e.Row.FindControl("txtAbsent");
            //    TextBox txtReason = (TextBox)e.Row.FindControl("txtPresent_Days");
            //    string strtime = "";
            //    string strInOut = "";
            //    strtime = Convert.ToString(e.Row.Cells[2].Text).Trim();
            //    strInOut = Convert.ToString(e.Row.Cells[1].Text).Trim();
            //    //get_AttendanceLeaveTypeList(ddlcategory, strInOut, strtime);
            //}
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
            dsList = spm.GetSite_Attendance(strempcode);
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
                //if (dsList.Tables[1].Rows.Count > 0)
                //{
                //    hdnisSelfAppr.Value = Convert.ToString(dsList.Tables[1].Rows[0]["emp_IsSelftAppr"]);
                //}
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
            TextBox txtReason = (TextBox)gvr.FindControl("txtReason");
            DropDownList ddlcategory = (DropDownList)gvr.FindControl("ddlcategory");

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
                blnchk = true;
        }

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
            TextBox txtReason = (TextBox)gvr.FindControl("txtReason");
            DropDownList ddlcategory = (DropDownList)gvr.FindControl("ddlcategory");

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

                if (Convert.ToString(ddlcategory.SelectedValue) != "0")
                {
                    strbuild.Append("<tr><td>" + Convert.ToString(gvr.Cells[0].Text) + "</td><td>" + Convert.ToString(gvr.Cells[1].Text) + "</td><td>" + Convert.ToString(gvr.Cells[2].Text) + "</td><td>" + Convert.ToString(gvr.Cells[3].Text) + "</td><td>" + Convert.ToString(ddlcategory.SelectedItem.Text) + "</td><td>" + Convert.ToString(txtReason.Text) + "</td></tr>");
                }

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

}
