using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InboxODApplication : System.Web.UI.Page
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
    public DataTable dtEmp;
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
                    editform.Visible = true;

                    hdnhrappType.Value = "0";
                    // PopulateEmployeeLeaveData();
                    if (Request.QueryString.Count > 0)
                        hdnhrappType.Value = Convert.ToString(Request.QueryString[0]);


                    hdnleaveconditiontypeid.Value = "13";
                    hdnAppr_id.Value = "0";

                    InboxLeaveReqstList();
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


    protected string GetApprove_RejectList(string strReqid)
    {
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        string strleavetype = Convert.ToString(type_id.Value).Trim();

        dtAppRej = spm.GetApproverStatusWFO(hdn_emp_code.Value, strReqid, Convert.ToInt32(hdnleaveconditiontypeid.Value), strleavetype, 0);
        if (dtAppRej.Rows.Count > 0)
        {
            sbapp.Append("<table>");
            for (int i = 0; i < dtAppRej.Rows.Count; i++)
            {
                sbapp.Append("<tr>");
                sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
                sbapp.Append("</tr>");
            }
            sbapp.Append("</table>");
        }

        return Convert.ToString(sbapp);
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in gvMngLeaveRqstList.Rows)
            {
                CheckBox CHK_clearancefromSubmitted = (CheckBox)row.FindControl("CHK_clearancefromSubmitted");
                if (CHK_clearancefromSubmitted.Checked == true)
                {
                    hdnReqid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[0]).Trim();

                    DataSet dsTrDetailss = new DataSet();
                    SqlParameter[] sparss = new SqlParameter[3];

                    sparss[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                    sparss[0].Value = "get_employee_data";

                    sparss[1] = new SqlParameter("@id", SqlDbType.Int);
                    sparss[1].Value = Convert.ToDecimal(hdnReqid.Value);

                    dsTrDetailss = spm.getDatasetList(sparss, "SP_WFO_WFH_DETAILS");
                    if (dsTrDetailss.Tables != null)
                    {
                        if (dsTrDetailss.Tables[0].Rows.Count > 0)
                        {
                            hdn_emp_code.Value = Convert.ToString(dsTrDetailss.Tables[0].Rows[0]["Emp_Code"]).Trim();
                            hdn_emp_email.Value = Convert.ToString(dsTrDetailss.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
                            hdn_Reason.Value = Convert.ToString(dsTrDetailss.Tables[0].Rows[0]["Reason"]).Trim();
                            hdn_emp_name.Value = Convert.ToString(dsTrDetailss.Tables[0].Rows[0]["Employee_Name"]).Trim();
                            hdn_from_date.Value = Convert.ToString(dsTrDetailss.Tables[0].Rows[0]["From_Date"]).Trim();
                            hdn_to_date.Value = Convert.ToString(dsTrDetailss.Tables[0].Rows[0]["To_Date"]).Trim();
                            hdn_for_from.Value = Convert.ToString(dsTrDetailss.Tables[0].Rows[0]["From_Duration"]).Trim();
                            hdn_for_to.Value = Convert.ToString(dsTrDetailss.Tables[0].Rows[0]["To_Duration"]).Trim();
                            hdn_aprover_name.Value = Convert.ToString(dsTrDetailss.Tables[0].Rows[0]["Approver_Name"]).Trim();
                            hdnAppr_id.Value = Convert.ToString(dsTrDetailss.Tables[0].Rows[0]["Appr_id"]).Trim();
                            type_id.Value = Convert.ToString(dsTrDetailss.Tables[0].Rows[0]["TypeName"]).Trim();


                            lblmessage.Text = "";

                            #region get First Approver id
                            var nextAppEmail = "";
                            var nextAppAppr_Id = 0;
                            var nextAppEmp_Code = "";
                            var req_Id = Convert.ToDouble(hdnReqid.Value);
                            var getNextApprovar = spm.GetNextApproverDetailsWFH(hdn_emp_code.Value, Convert.ToDouble(hdnReqid.Value), 13, "get_NextApproverDetails_mail");
                            if (getNextApprovar != null)
                            {
                                if (getNextApprovar.Rows.Count > 0)
                                {
                                    nextAppEmail = Convert.ToString(getNextApprovar.Rows[0]["Emp_Emailaddress"].ToString());
                                    nextAppAppr_Id = Convert.ToInt32(getNextApprovar.Rows[0]["APPR_ID"].ToString());
                                    nextAppEmp_Code = Convert.ToString(getNextApprovar.Rows[0]["A_EMP_CODE"].ToString());
                                }
                            }
                            var Appr_id = Convert.ToInt32(hdnAppr_id.Value);
                            var Created_By = Convert.ToString(Session["Empcode"].ToString());
                            var Reamrk = "";
                            #endregion
                            if (nextAppEmail == "")
                            {
                                var strInsertmediaterlist = "";
                                //Final Approval
                                spm.INSERTUPDATEAPP(req_Id, Appr_id, "UpdateFinalApprovar", hdn_emp_code.Value, Created_By, Reamrk, Created_By);
                                spm.send_mailto_RequesterWFH(hdn_emp_email.Value, "", "Request for " + type_id.Value, type_id.Value, lpm.LeaveDays.ToString(), hdn_Reason.Value, hdn_from_date.Value, hdn_for_from.Value, hdn_to_date.Value, hdn_for_to.Value, hdn_aprover_name.Value, "", GetApprove_RejectList(Convert.ToString(req_Id)), hdn_emp_name.Value, Convert.ToString(strInsertmediaterlist));
                            }
                            else
                            {
                                //Update Current Approvar  http://localhost/hrms/Default.aspx
                                spm.INSERTUPDATEAPP(req_Id, Appr_id, "UpdateApplicationApprovar", hdn_emp_code.Value, Created_By, Reamrk, Created_By);
                                spm.INSERTUPDATEAPP(req_Id, nextAppAppr_Id, "InsertNextApplicationApprovar", hdn_emp_code.Value, Created_By, Reamrk, nextAppEmp_Code);
                                var strInsertmediaterlist = "";
                                var strLeaveRstURL = "https://ess.highbartech.com/hrms/procs/ODApplication_App.aspx?reqid=" + req_Id + "&type=app";
                                spm.send_mailto_Next_ApproverWFO(hdn_emp_email.Value, nextAppEmail, "Request for " + type_id.Value, type_id.Value, "", hdn_Reason.Value, hdn_from_date.Value, hdn_for_from.Value, hdn_to_date.Value, hdn_for_to.Value, GetApprove_RejectList(hdnReqid.Value), hdn_emp_name.Value, Convert.ToString(strInsertmediaterlist), strLeaveRstURL);
                                spm.send_mailto_RequesterWFH(hdn_emp_email.Value, "", "Request for " + type_id.Value, type_id.Value, lpm.LeaveDays.ToString(), hdn_Reason.Value, hdn_from_date.Value, hdn_for_from.Value, hdn_to_date.Value, hdn_for_to.Value, hdn_aprover_name.Value, "", GetApprove_RejectList(Convert.ToString(req_Id)), hdn_emp_name.Value, Convert.ToString(strInsertmediaterlist));

                            }
                        }
                    }


                }
            }
            Response.Redirect("~/procs/Attendance.aspx");
        }
        catch (Exception ex)
        { }
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
            dtleaveInbox = spm.GetODApplicationInbox(strempcode, "WFHApprovalInbox");

            if (dtleaveInbox.Rows.Count > 0)
            {
                gvMngLeaveRqstList.DataSource = dtleaveInbox;
                gvMngLeaveRqstList.DataBind();
                btnAprover.Visible = true;

                lblmessage.Text = "Note:- Upon submission, all selected Outdoor Duty (OD) records will be approved. Please review and confirm the details before Approve.";
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


    #region Not in use code 

    public void PopulateEmployeeLeaveData()
    {
        try
        {

            //lpm.Emp_Code = "00630134";



            dtLeaveDetails = spm.GetAttRegInbox(lpm.Emp_Code);
            gvMngLeaveRqstList.DataSource = dtLeaveDetails;
            gvMngLeaveRqstList.DataBind();



        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }


    }




    #endregion
    protected void gvMngLeaveRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        PopulateEmployeeLeaveData();
        gvMngLeaveRqstList.PageIndex = e.NewPageIndex;
        gvMngLeaveRqstList.DataSource = dsLeaveRequst;
        gvMngLeaveRqstList.DataBind();
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
                Response.Redirect("ODApplication_App.aspx?reqid=" + hdnReqid.Value + "&type=app");
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }



}
