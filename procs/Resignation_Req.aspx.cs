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
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;


public partial class Resignation_Req : System.Web.UI.Page
{

    #region CreativeMethods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    #endregion

    SP_Methods spm = new SP_Methods();
    DataSet dspaymentVoucher_Apprs = new DataSet();

    #region PageEvents
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    txtReason.Attributes.Add("maxlength", "200");
                    editform.Visible = true;
                    divbtn.Visible = false;
                    //divmsg.Visible = false;
                    mobile_cancel.Visible = false;
                    btnTra_Details.Visible = false;
                    txtFromdate_N.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtEmpCode.Text = Convert.ToString(Session["Empcode"]);
                    hdnTravelConditionid.Value = "2";
                    hdnRemid.Value = "0";
                    hdnClaimid.Value = "0";
                    GetEmployeeDetails();

                    txtFromdateold.Text = DateTime.Today.ToString("dd/MM/yyyy").Replace("-", "/");
                    txtFromdateold.Enabled = false;
                    AssigningSessions();
                    hdnGrade.Value = Convert.ToString(Session["Grade"]);
                    //txtFromdateold.Text = ;
                    if (Request.QueryString.Count > 0)
                    {
                        hdnClaimid.Value = "1"; // Convert.ToString(Request.QueryString[0]).Trim(); 
                        hdnRemid.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        mobile_btnSave.Visible = false;
                        uploadfile.Enabled = false;
                        txtFromdate_N.Enabled = false;
                        lstBilltype.Enabled = false;
                        txtBilltype.Enabled = false;
                        txtReason.Enabled = false;
                    }
                    if (Convert.ToString(hdnRemid.Value).Trim() != "0" && Convert.ToString(hdnRemid.Value).Trim() != "")
                    {
                        mobile_cancel.Visible = true;
                        getMobRemlsDetails_usingRemid();

                        if (Request.QueryString.Count > 2)
                        {
                            InsertMobileRem_DatatoTempTables_trvl();
                        }
                    }
                    /*by Highbartech on 11-06-2020*/
                    //getMobileClaimDetails();
                    getClaimDetails();
                    /*by Highbartech on 11-06-2020*/
                    getApproverdata();
                    if (Convert.ToString(hdnMobRemStatusM.Value).Trim() == "2" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "3" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "4" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "5")
                    {
                        mobile_btnSave.Visible = false;
                        mobile_cancel.Visible = true;
                        btnTra_Details.Visible = false;
                        // dgMobileClaim.Enabled = false;
                    }
                    if (Convert.ToString(hdnMobRemStatus_dtls.Value).Trim() == "Approved")
                    {
                        mobile_btnSave.Visible = false;
                        mobile_cancel.Visible = true;
                        btnTra_Details.Visible = false;
                    }
                    //getMobileClaimDetails();
                    if (Convert.ToString(hdnMobRemStatusM.Value).Trim() == "5")
                    {
                        mobile_btnSave.Visible = false;
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

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";
        string strfromDateN = "";
        string strtoDateN = "";
        string filename = "";
        String strfileName = "";

        getTravle_Desk_COS_ApproverCode();

        lblmessage.Text = "";

        if (Convert.ToString(txtFromdateold.Text).Trim() == "")
        {
            lblmessage.Text = "Please select Date";
            return;
        }
        if (Convert.ToString(txtFromdate_N.Text).Trim() == "")
        {
            lblmessage.Text = "Please select Resignation Date";
            return;
        }
        if (Convert.ToString(txtTodate_N.Text).Trim() == "")
        {
            lblmessage.Text = "Last Working day Date is empty";
            return;
        }
        if (Convert.ToString(txtBilltype.Text).Trim() == "")
        {
            lblmessage.Text = "Please select Resignation Reason";
            return;
        }
        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }
        if (uploadfile.HasFile)
        {
            filename = uploadfile.FileName;
        }

        if (Convert.ToString(lnkuplodedfile.Text).Trim() == "")
        {
            if (Convert.ToString(filename).Trim() == "")
            {
                lblmessage.Text = "Please upload File!";
                return;
            }
        }

        decimal eligamount = 0, enteredamount = 0;
        #region date formatting
        string strclaim_month = "";
        if (Convert.ToString(txtTodate_N.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtTodate_N.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[1]) + "_" + Convert.ToString(strdate[2]);
            strclaim_month = Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[2]);
        }
        #endregion

        if (Convert.ToString(filename).Trim() != "")
        {
            filename = uploadfile.FileName;
            strfileName = "";
            strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + txtBilltype.Text.ToString() + Convert.ToString("_Resignation").Trim() + Path.GetExtension(uploadfile.FileName);
            filename = strfileName;
            uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Resignationpath"]).Trim()), strfileName));
        }
        if (Convert.ToString(txtFromdate_N.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate_N.Text).Trim().Split('/');
            strfromDateN = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(txtTodate_N.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtTodate_N.Text).Trim().Split('/');
            strtoDateN = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        #region Check Duplicate Claim on Emp_Code, Expense Type, Bill Date, Bill no
        hdnsptype.Value = "checkDuplicate";

        DataTable dt = spm.CheckDuplicateResignationDetails(txtEmpCode.Text, hdnsptype.Value);
        if (dt.Rows.Count > 0)
        {
            lblmessage.Text = "You already have an active Resigantion!";
            return;
        }
        #endregion

        hdnsptype.Value = "InsertTempTable";
        if (Convert.ToString(hdnClaimid.Value).Trim() != "" && Convert.ToString(hdnClaimid.Value).Trim() != "0")
            hdnsptype.Value = "updateTempTable";

        spm.InsertResignationClaimDetails(Convert.ToInt32(hdnRemid.Value), strtoDateN, enteredamount, txtEmpCode.Text, Convert.ToDecimal(0), hdnsptype.Value, "Yes", filename, hdnClaimid.Value, "", txtReason.Text, "", txtBilltype.Text.ToString().Trim());

        if (Convert.ToString(txtFromdateold.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdateold.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        DataTable dtMaxRempID = new DataTable();
        int status = 1;
        int maxRemid = 0;

        dtMaxRempID = spm.InsertResignationMain(strfromDate, Convert.ToDecimal(0), "", txtEmpCode.Text, status, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToString(hdnRemid.Value), "", "", strfromDateN, strtoDateN);
        maxRemid = Convert.ToInt32(dtMaxRempID.Rows[0]["maxRemid"]);

        if (maxRemid == 0)
            return;

        hdnRemid.Value = Convert.ToString(maxRemid);
        spm.InsertResignationClaimDetails(maxRemid, "", 0, txtEmpCode.Text, 0, "InsertMainTable", "", "", "", "", "", "", "");

        SqlParameter[] spars1 = new SqlParameter[12];

        spars1[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars1[1] = new SqlParameter("@Id", SqlDbType.Int);
        spars1[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
        spars1[3] = new SqlParameter("@Resig_Date", SqlDbType.VarChar);
        spars1[4] = new SqlParameter("@RESIGN_REASON", SqlDbType.VarChar);
        spars1[5] = new SqlParameter("@DATE_OF_SEPARATION", SqlDbType.VarChar);
        spars1[6] = new SqlParameter("@TENURE", SqlDbType.VarChar);
        spars1[7] = new SqlParameter("@EMP_STATUS", SqlDbType.VarChar);
        spars1[8] = new SqlParameter("@RELIEVING_LETTER_PATH", SqlDbType.VarChar);
        spars1[9] = new SqlParameter("@RELIEVING_LETTER_DATE", SqlDbType.VarChar);
        spars1[10] = new SqlParameter("@CREATEDBY", SqlDbType.VarChar);
        spars1[11] = new SqlParameter("@UPDATEDBY", SqlDbType.VarChar);

        spars1[0].Value = "Insert_Resignation";
        spars1[1].Value = DBNull.Value;
        spars1[2].Value = txtEmpCode.Text.ToString();
        if (strfromDateN.ToString() == "")
            spars1[3].Value = DBNull.Value;
        else
            spars1[3].Value = strfromDateN.ToString();

        spars1[4].Value = lstBilltype.SelectedValue.ToString();
        if (strtoDateN.ToString() == "")
            spars1[5].Value = DBNull.Value;
        else
            spars1[5].Value = strtoDateN.ToString();

        spars1[6].Value = DBNull.Value;
        spars1[7].Value = "1";
        spars1[8].Value = DBNull.Value;
        spars1[9].Value = DBNull.Value;
        spars1[10].Value = txtEmpCode.Text.ToString();
        spars1[11].Value = txtEmpCode.Text.ToString();

        spm.Insert_Data(spars1, "SP_Admin_Employee_Exit");

        String strmobeRemURL = "";

        if (DgvApprover.Rows.Count == 1)
            strmobeRemURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_Resignation"]).Trim() + "?rem_id=" + hdnRemid.Value + "&mngexp=0&inbtype=RACC";
        else
            strmobeRemURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_Resignation"]).Trim() + "?rem_id=" + hdnRemid.Value + "&mngexp=0&inbtype=APP";


        //spm.InsertResignationApproverDetails(hflapprcode.Value, Convert.ToInt32(hdnApprId.Value), maxRemid, "");
        spm.InsertResignationApproverDetails(hdnApprovalCOS_Code.Value, Convert.ToInt32(hdnApprovalCOS_ID.Value), maxRemid, "");

        string emp_name = "";
        StringBuilder strbuild = new StringBuilder();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "ResignationMail_EmployeeDetails";

        spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
        spars[1].Value = txtEmpCode.Text.ToString();

        DataTable tt = spm.getData_FromCode(spars, "SP_Admin_Employee_Join");
        //project myproject = classproject.getsingleprojects(projectid);

        if (tt.Rows.Count > 0)
        {
            strbuild.Length = 0;
            strbuild.Clear();

            strbuild.Append("<table border='1'>");

            foreach (DataRow row in tt.Rows)
            {
                if (Convert.ToString(row["emp_location"].ToString()) == "HO-NaviMum")
                {
                    strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
                        + "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
                        + "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
                        + "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
                        + "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
                        + "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
                        + "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
                        + "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
                        + "</td></tr><tr><td>Reporting Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
                        + "</td></tr><tr><td>HOD: </td><td>" + Convert.ToString(row["DH"].ToString())
                        + "</td></tr>");

                }
                else
                {
                    strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
                        + "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
                        + "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
                        + "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
                        + "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
                        + "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
                        + "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
                        + "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
                        + "</td></tr><tr><td>Project Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
                        + "</td></tr><tr><td>Delivery Manager: </td><td>" + Convert.ToString(row["DM"].ToString())
                        + "</td></tr><tr><td>Program Manager: </td><td>" + Convert.ToString(row["PRM"].ToString())
                        + "</td></tr><tr><td>Delivery Head: </td><td>" + Convert.ToString(row["DH"].ToString())
                        + "</td></tr>");

                }
                emp_name = Convert.ToString(row["Emp_Name"].ToString());
            }
            strbuild.Append("</table>");
        }
        //spm.Resignation_send_mailto_RM_Approver(txtEmpName.Text, hdnApprEmailaddress.Value, "Request for " + txtBilltype.Text.ToString() + " bill - " + Convert.ToString(hdnvouno.Value), "", "0", GetApprove_RejectList(Convert.ToDecimal(maxRemid)), txtEmpName.Text, "", strmobeRemURL, "", strclaim_month);
        spm.Resignation_send_mailto_RM_Approver(txtEmpName.Text, hdnApprovalCOS_mail.Value, "Resignation of " + Convert.ToString(hdnApprovalCOS_Name.Value), "", "0","", txtEmpName.Text, "", strmobeRemURL, Convert.ToString(strbuild), strclaim_month);
        spm.Resignation_send_mailto_Others(txtEmpName.Text, hdnApprEmailaddress.Value, "Resignation of " + Convert.ToString(hdnApprovalCOS_Name.Value), "", "0", "", txtEmpName.Text, "", strmobeRemURL, Convert.ToString(strbuild), strclaim_month);
        lblmessage.Visible = true;
        lblmessage.Text = "Resignation Reuqest Submitted Successfully";
        Response.Redirect("~/procs/ResignationMenu.aspx");

    }

    protected void btnTra_Details_Click(object sender, EventArgs e)
    {

        string[] strdate;
        string strfromDate = "";
        string filename = "";
        String strfileName = "";
        string rfilename = "";
        String strRfileName = "";

        if (uploadfile.HasFile)
        {
            filename = uploadfile.FileName;
        }

        if (Convert.ToString(lnkuplodedfile.Text).Trim() == "")
        {
            if (Convert.ToString(filename).Trim() == "")
            {
                lblmessage.Text = "Please upload Bill to Claim!";
                return;
            }
        }


        decimal eligamount = 0, enteredamount = 0;

        if (Convert.ToString(filename).Trim() != "")
        {
            filename = uploadfile.FileName;
            strfileName = "";
            strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + Convert.ToString("Mobile_Bill").Trim() + Path.GetExtension(uploadfile.FileName);
            filename = strfileName;
            uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ClaimsDocumentpath"]).Trim()), strfileName));
        }

        hdnsptype.Value = "InsertTempTable";
        if (Convert.ToString(hdnClaimid.Value).Trim() != "" && Convert.ToString(hdnClaimid.Value).Trim() != "0")
            hdnsptype.Value = "updateTempTable";

        spm.InsertMobileClaimDetails(Convert.ToInt32(hdnRemid.Value), txtTodate_N.Text, enteredamount, txtEmpCode.Text, Convert.ToDecimal(0), hdnsptype.Value, "Yes", filename, hdnClaimid.Value, rfilename, txtReason.Text, "0", txtBilltype.Text.ToString());

    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        AssigningSessions();
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnClaimid.Value = Convert.ToString(dgMobileClaim.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnRemid.Value = Convert.ToString(dgMobileClaim.DataKeys[row.RowIndex].Values[1]).Trim();

        Response.Redirect("~/procs/MobileClaim.aspx?clmid=" + hdnClaimid.Value + "&remid=" + hdnRemid.Value);
    }

    protected void mobile_cancel_Click(object sender, EventArgs e)
    {
        getTravle_Desk_COS_ApproverCode();
        string emp_name = "";
        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }

        string strclaim_month = "";
        string[] strdate;
        string strfromDate = "";

        if (Convert.ToString(strfromDate).Trim() != "")
        {
            strdate = Convert.ToString(strfromDate).Trim().Split('/');
            strclaim_month = Convert.ToString(strdate[0]) + "-" + Convert.ToString(strdate[1]);
        }
        //}

        hdnEligible.Value = "Cancellation";
        string strapprovermails = "";
        getTravle_Desk_COS_ApproverCode();
        strapprovermails = GetApprove_RejectList(Convert.ToDecimal(hdnRemid.Value));

        spm.InsertResignationClaimDetails(Convert.ToInt32(hdnRemid.Value), "", 0, txtEmpCode.Text, 0, Convert.ToString("CancelMobRem"), "", "", "", "", "", "", "");

        SqlParameter[] spars1 = new SqlParameter[12];

        spars1[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars1[1] = new SqlParameter("@Id", SqlDbType.Int);
        spars1[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
        spars1[3] = new SqlParameter("@Resig_Date", SqlDbType.VarChar);
        spars1[4] = new SqlParameter("@RESIGN_REASON", SqlDbType.VarChar);
        spars1[5] = new SqlParameter("@DATE_OF_SEPARATION", SqlDbType.VarChar);
        spars1[6] = new SqlParameter("@TENURE", SqlDbType.VarChar);
        spars1[7] = new SqlParameter("@EMP_STATUS", SqlDbType.VarChar);
        spars1[8] = new SqlParameter("@RELIEVING_LETTER_PATH", SqlDbType.VarChar);
        spars1[9] = new SqlParameter("@RELIEVING_LETTER_DATE", SqlDbType.VarChar);
        spars1[10] = new SqlParameter("@CREATEDBY", SqlDbType.VarChar);
        spars1[11] = new SqlParameter("@UPDATEDBY", SqlDbType.VarChar);

        spars1[0].Value = "Update_Resignation";
        spars1[1].Value = DBNull.Value;
        spars1[2].Value = txtEmpCode.Text.ToString();
        spars1[3].Value = DBNull.Value;
        spars1[4].Value = DBNull.Value;
        spars1[5].Value = DBNull.Value;
        spars1[6].Value = DBNull.Value;
        spars1[7].Value = "2";
        spars1[8].Value = DBNull.Value;
        spars1[9].Value = DBNull.Value;
        spars1[10].Value = txtEmpCode.Text.ToString();
        spars1[11].Value = txtEmpCode.Text.ToString();

        spm.Insert_Data(spars1, "SP_Admin_Employee_Exit");

        StringBuilder strbuild = new StringBuilder();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "ResignationMail_EmployeeDetails";

        spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
        spars[1].Value = txtEmpCode.Text.ToString();

        DataTable tt = spm.getData_FromCode(spars, "SP_Admin_Employee_Join");
        //project myproject = classproject.getsingleprojects(projectid);

        if (tt.Rows.Count > 0)
        {
            strbuild.Length = 0;
            strbuild.Clear();

            strbuild.Append("<table border='1'>");

            foreach (DataRow row in tt.Rows)
            {
                if (Convert.ToString(row["emp_location"].ToString()) == "HO-NaviMum")
                {
                    strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
                        + "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
                        + "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
                        + "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
                        + "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
                        + "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
                        + "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
                        + "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
                        + "</td></tr><tr><td>Reporting Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
                        + "</td></tr><tr><td>HOD: </td><td>" + Convert.ToString(row["DH"].ToString())
                        + "</td></tr>");

                }
                else
                {
                    strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
                        + "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
                        + "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
                        + "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
                        + "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
                        + "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
                        + "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
                        + "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
                        + "</td></tr><tr><td>Project Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
                        + "</td></tr><tr><td>Delivery Manager: </td><td>" + Convert.ToString(row["DM"].ToString())
                        + "</td></tr><tr><td>Program Manager: </td><td>" + Convert.ToString(row["PRM"].ToString())
                        + "</td></tr><tr><td>Delivery Head: </td><td>" + Convert.ToString(row["DH"].ToString())
                        + "</td></tr>");

                }
                emp_name = Convert.ToString(row["Emp_Name"].ToString());
            }
            strbuild.Append("</table>");
        }
        hdnApprEmailaddress.Value = hdnApprEmailaddress.Value + ";" + hdnApprovalCOS_mail.Value;
        spm.Resignation_send_mail_Cancellation(txtEmpName.Text, hdnApprEmailaddress.Value, "Resignation Cancellation of " + Convert.ToString(hdnApprovalCOS_Name.Value), "", "0", "", txtEmpName.Text, "", "", Convert.ToString(strbuild), strclaim_month);
        //spm.Fuel_send_mail_Cancel(txtEmpName.Text, hdnApprEmailaddress.Value, "Request for " + txtBilltype.Text.ToString() + " bill - " + Convert.ToString(hdnvouno.Value), "", "0", GetApprove_RejectList(Convert.ToDecimal(hdnRemid.Value)), txtEmpName.Text, "", "", "", strclaim_month);
        Response.Redirect("~/procs/ResignationMenu.aspx");
    }

    protected void dgMobileClaim_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            if (Convert.ToString(hdnMobRemStatusM.Value).Trim() == "2" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "3" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "4")
            {
                e.Row.Cells[4].Visible = false;
            }
            else
            {
                e.Row.Cells[4].Visible = true;
            }
            if (Convert.ToString(hdnMobRemStatus_dtls.Value).Trim() == "Approved")
            {
                e.Row.Cells[4].Visible = false;
            }

        }
    }

    protected void lnkuplodedfile_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ClaimsDocumentpath"]).Trim()), lnkuplodedfile.Text);

            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
            Response.WriteFile(strfilepath);
            Response.End();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    #endregion

    #region PageMethods

    public void GetMobileEligibility_New()
    {

        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.GetMobilelEligibilityMatrix_Emp_Grade(Convert.ToString(hflGrade.Value), Convert.ToString(txtEmpCode.Text), Convert.ToString(1));
        //dtApproverEmailIds = spm.GetMobilelEligibilityMatrix(Convert.ToString(hdnGrade.Value));

        if (dtApproverEmailIds.Rows.Count <= 0)
        {
            btnTra_Details.Visible = false;
            mobile_btnSave.Visible = false;
            mobile_cancel.Visible = false;
            lblmessage.Text = "Sorry You are not entitled for mobile claims!";
        }
        if (dtApproverEmailIds.Rows.Count > 0)
        {
            if (Convert.ToString(dtApproverEmailIds.Rows[0]["Eligibility"]).Trim() != "")
            {
                if (Convert.ToDecimal(dtApproverEmailIds.Rows[0]["Eligibility"]) <= 0)
                {
                    btnTra_Details.Visible = false;
                    mobile_btnSave.Visible = false;
                    mobile_cancel.Visible = false;
                    lblmessage.Text = "Sorry You are not entitled for mobile claims!";
                }
            }
        }

    }

    public void GetMobileEligibility()
    {
        string[] strdate;
        string strfromDate = "";

        if (Convert.ToString(txtTodate_N.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtTodate_N.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);

            DataTable dtApproverEmailIds = new DataTable();
            dtApproverEmailIds = spm.GetMobilelEligibilityMatrix_Emp_Grade(Convert.ToString(hdnGrade.Value), Convert.ToString(txtEmpCode.Text), Convert.ToString(strfromDate));
            //dtApproverEmailIds = spm.GetMobilelEligibilityMatrix(Convert.ToString(hdnGrade.Value));

            if (dtApproverEmailIds.Rows.Count > 0)
            {
                //txtElgAmnt.Text = Convert.ToString(dtApproverEmailIds.Rows[0]["Eligibility"]);
                //txtElgAmnt.Enabled = false;
                if (Convert.ToString(dtApproverEmailIds.Rows[0]["view_mobile"]).Trim() == "N")
                {
                    mobile_btnSave.Enabled = false;
                    mobile_btnSave.Visible = false;
                    uploadfile.Enabled = false;
                    //uploadRcpt.Enabled = false;
                    lblmessage.Text = "Sorry Due to location change, You are not entitled for mobile claims!";
                }

            }
            else
            {
                //lblmessage.Visible = true;
                mobile_btnSave.Enabled = false;
                uploadfile.Enabled = false;
                //uploadRcpt.Enabled = false;
                lblmessage.Text = "Sorry You are not entitled for mobile claims!";
            }
        }

    }

    public void GetEmployeeDetails()
    {
        try
        {
            DataTable dtEmpDetails = new DataTable();
            dtEmpDetails = spm.GetEmployeeData(txtEmpCode.Text);
            if (dtEmpDetails.Rows.Count > 0)
            {
                txtEmpName.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
                hflEmpDesignation.Value = Convert.ToString(dtEmpDetails.Rows[0]["DesginationName"]);
                hflEmpDepartment.Value = Convert.ToString(dtEmpDetails.Rows[0]["Department_Name"]);
                hflGrade.Value = Convert.ToString(dtEmpDetails.Rows[0]["Grade"]).Trim();
                hflEmailAddress.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
            }
            //  getApproverdata();  
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

            throw;
        }
    }
    public void AssigningSessions()
    {

        Session["Fromdate"] = txtFromdateold.Text;
        Session["ReqEmpCode"] = txtEmpCode.Text;
        Session["Grade"] = hflGrade.Value;
        Session["TrDays"] = hdnTrdays.Value;

        //Response.Write(Convert.ToString(Session["Fromdate"]));
        //Response.End();

    }
    public void getMobileClaimDetails()
    {
        DataTable dtMobileDetails = new DataTable();
        dtMobileDetails = spm.GetMobileClaimDetails_Reqstpage(txtEmpCode.Text);

        dgMobileClaim.DataSource = null;
        dgMobileClaim.DataBind();

        if (dtMobileDetails.Rows.Count > 0)
        {
            btnTra_Details.Visible = false;
            dgMobileClaim.DataSource = dtMobileDetails;
            dgMobileClaim.DataBind();

            #region Calulate Total Claim Amount
            //txtAmountTot.Text = "0";
            //txtAmountTot.Enabled = false;
            double dtotclaimAmt = 0, dttotalEligibility = 0, comapringamount = 1.5;
            for (Int32 irow = 0; irow < dgMobileClaim.Rows.Count; irow++)
            {
                if (Convert.ToString(dgMobileClaim.Rows[irow].Cells[1].Text).Trim() != "")
                    dtotclaimAmt += Convert.ToDouble(dgMobileClaim.Rows[irow].Cells[1].Text);

                if (Convert.ToString(dgMobileClaim.Rows[irow].Cells[2].Text).Trim() != "")
                    dttotalEligibility += Convert.ToDouble(dgMobileClaim.Rows[irow].Cells[2].Text);
            }

            if (dtotclaimAmt <= dttotalEligibility)
            {
                hdnTravelConditionid.Value = "1";
            }
            else
            {
                if (dtotclaimAmt < dttotalEligibility * comapringamount)
                {
                    hdnTravelConditionid.Value = "2";
                }
                else if (dtotclaimAmt >= dttotalEligibility * comapringamount)
                {
                    hdnTravelConditionid.Value = "3";
                }
            }
            //txtAmountTot.Text = Convert.ToString(dtotclaimAmt);
            #endregion
        }

    }
    private void InsertMobileRem_DatatoTempTables_trvl()
    {
        try
        {

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_ResignationRem_insert_mainData_toTempTabls";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDecimal(hdnRemid.Value);

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            DataTable dtMaxTripID = spm.InsertorUpdateData(spars, "[SP_GETALLreembursement_DETAILS]");

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }


    }

    public void getMobRemlsDetails_usingRemid()
    {
        try
        {
            DataSet dtTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_getMainRequest_Resignation";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Int);
            spars[1].Value = hdnRemid.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;
            //dtTrDetails = spm.getMobileRemDataList(spars, "[SP_GETALLreembursement_DETAILS]");
            dtTrDetails = spm.getDatasetList(spars, "[SP_GETALLreembursement_DETAILS]");
            if (dtTrDetails.Tables[0].Rows.Count > 0)
            {
                txtFromdateold.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["created_on"]);
                hdnMobRemStatusM.Value = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Ren_Status"]);
                hdnTravelConditionid.Value = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Rem_Conditionid"]);

                if (dtTrDetails.Tables[1].Rows.Count > 0)
                {
                    for (Int32 irow = 0; irow < dtTrDetails.Tables[1].Rows.Count; irow++)
                    {
                        if (Convert.ToString(dtTrDetails.Tables[1].Rows[irow]["Action"]).Trim() == "Approved")
                        {
                            hdnMobRemStatus_dtls.Value = Convert.ToString(dtTrDetails.Tables[1].Rows[irow]["Action"]).Trim();
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    }
    public void getApproverdata()
    {

        DataTable dtApproverEmailIds = new DataTable();
        if (Convert.ToString(hdnRemid.Value).Trim() == "")
            dtApproverEmailIds = spm.GeTResignationApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), 0);
        else
            dtApproverEmailIds = spm.GeTResignationApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToDecimal(hdnRemid.Value));

        dspaymentVoucher_Apprs.Tables.Add(dtApproverEmailIds);

        lstApprover.Items.Clear();
        if (dtApproverEmailIds.Rows.Count > 0)
        {
            //hdnApprEmailaddress.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["Emp_Emailaddress"]);
            hdnApprId.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["APPR_ID"]);

            DgvApprover.DataSource = null;
            DgvApprover.DataBind();

            if (dtApproverEmailIds.Rows.Count > 0)
            {
                DgvApprover.DataSource = dtApproverEmailIds;
                DgvApprover.DataBind();
            }
            hflapprcode.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["approver_emp_code"]);
            foreach (DataRow row in dtApproverEmailIds.Rows)
            {
                hdnApprEmailaddress.Value = hdnApprEmailaddress.Value + Convert.ToString(row["Emp_Emailaddress"].ToString()) + ";";
            }
            //hflapprcode.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["A_EMP_CODE"]);
        }
        else
        {
            lblmessage.Text = "Since you are not assigned under any approver you can not apply for any type of Travel request, please contact HR";

        }
    }

    protected string GetApprove_RejectList(decimal dmaxremid)
    {
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        //dtAppRej = spm.GetMobileApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
        dtAppRej = spm.GeTfuelApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), dmaxremid);
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

    public void getTravle_Desk_COS_ApproverCode()
    {

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_ACCCOS_apprver_code_Rem";

        spars[1] = new SqlParameter("@trappr", SqlDbType.VarChar);
        //spars[1].Value ="RCOS";
        spars[1].Value = "HRML";

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = txtEmpCode.Text;

        dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

        //ACC Approver Code
        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            hdnApprovalCOS_Code.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_code"]).Trim();
            hdnApprovalCOS_ID.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_id"]).Trim();
            hdnApprovalCOS_mail.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();
            hdnApprovalCOS_Name.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_remarks"]).Trim();

        }

    }

    public void checkPastMoths_AlreadySubmits()
    {
        try
        {
            lblmessage.Text = "";
            #region date formatting

            string[] strdate;
            string strfromDate = "";
            string strToDate = "";


            if (Convert.ToString(Session["Fromdate"]).Trim() != "")
            {
                strdate = Convert.ToString(Session["Fromdate"]).Trim().Split('/');
                strToDate = Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]) + "/" + Convert.ToString(strdate[2]);
            }

            if (Convert.ToString(txtTodate_N.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtTodate_N.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            #endregion

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@sMonth", SqlDbType.VarChar);
            spars[0].Value = strToDate;

            spars[1] = new SqlParameter("@sclaimdate", SqlDbType.VarChar);
            spars[1].Value = strfromDate;

            spars[2] = new SqlParameter("@Empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            spars[3] = new SqlParameter("@Billtype", SqlDbType.VarChar);
            spars[3].Value = txtBilltype.Text;

            dsTrDetails = spm.getDatasetList(spars, "sp_check_mobile_rem_validation");

            btnTra_Details.Visible = false;
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["msg"]) != "")
                {
                    txtFromdate_N.Text = "";
                    txtTodate_N.Text = "";
                    lblmessage.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["msg"]);
                    btnTra_Details.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }

    }

    private void getClaimDetails()
    {

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "getResignationclaimdetails_edit";

        spars[1] = new SqlParameter("@claimsid", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(hdnClaimid.Value);

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = Convert.ToString(txtEmpCode.Text);

        spars[3] = new SqlParameter("@rem_id", SqlDbType.VarChar);
        spars[3].Value = Convert.ToString(hdnRemid.Value);

        dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            txtBilltype.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["BillType"]).Trim();
            lstBilltype.SelectedValue = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["BillType"]).Trim();
            hdnBilltype.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["BillType"]).Trim();
            //lblheading.Text = "Mobile bill Voucher - " + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Vouno"]);
            hdnvouno.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Vouno"]);
            txtFromdate_N.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["From_date"]).Trim();
            txtTodate_N.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["To_date"]).Trim();
            lnkuplodedfile.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["UploadFile"]).Trim();
            txtReason.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Reason"]).Trim();
        }


    }
    #endregion

    protected void txtFromdate_N_TextChanged(object sender, EventArgs e)
    {
        try
        {

            DateTime endDate = DateTime.ParseExact(txtFromdate_N.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            Int64 addedDays = 89;
            endDate = endDate.AddDays(addedDays);
            DateTime end = endDate;
            txtTodate_N.Text = end.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            //txtAmount.Text = "";
            if (Convert.ToString(txtTodate_N.Text).Trim() != "" && Convert.ToString(txtBilltype.Text).Trim() != "")
            {
                checkPastMoths_AlreadySubmits();
                GetMobileEligibility();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    }
    protected void lstBilltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtBilltype.Text = lstBilltype.SelectedItem.Text;
        PopupControlExtender2.Commit(lstBilltype.SelectedItem.Text);
        lblmessage.Text = "";
        hdnBilltype.Value = "";
        hdnBilltype.Value = lstBilltype.SelectedValue;
    }
}
