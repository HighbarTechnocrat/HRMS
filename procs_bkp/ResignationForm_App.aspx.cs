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


public partial class ResignationForm_App : System.Web.UI.Page
{

    #region Creative_Default_methods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    DataTable dsapprover = new DataTable();
    SP_Methods spm = new SP_Methods();
    string strempcode = ""; string typ = ""; int Id; int ResignationID;

    String strloginid = "";
    string Leavestatus = "";
    string IsApprover = "";
    string nxtapprcode;
    string nxtapprname = "";
    int apprid;
    int statusid;
    public string filename = "", approveremailaddress, message;

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }


    #endregion

    #region Page_Events

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                Page.SmartNavigation = true;
                hdnEmpCode.Value = Convert.ToString(Session["Empcode"]);


                if (!Page.IsPostBack)
                {
                    string strfilepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ResigAttachmentPath"]).Trim());
                    FilePath.Value = strfilepath;
                    editform.Visible = true;
                    Fill_ResignationReason();
                    txtFromdate_N.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtEmpComment.Attributes.Add("maxlength", "200");

                    if (Request.QueryString.Count > 0)
                    {
                        hdnId.Value = Convert.ToString(Request.QueryString[0]);
                        Id = Convert.ToInt32(hdnId.Value);
                        hdnResignationID.Value = hdnId.Value;
                        string ApproverEmpCode = CheckIsLoggedInUserAsApprover(Id);
                        if (Convert.ToString(Session["Empcode"]) == ApproverEmpCode)
                        {
                            GetPendingResignationDetails(Id);
                            GetApproversStatus(txtEmpCode.Text, Convert.ToInt32(hdnResignationID.Value));
                            GetCurrentApprID(hdnEmpCode.Value, Convert.ToInt32(hdnResignationID.Value));
                            BindgvHistory(txtEmpCode.Text.ToString());
                            mobile_btnSave.Visible = true;
                            claimmob_btnSubmit.Visible = true;
                            mobile_cancel.Visible = true;
                            mobile_btnBack.Visible = false;
                        }
                        else
                        {
                            lblmessage.Text = "You dont have access to this page";
                            mobile_btnSave.Visible = false;
                            claimmob_btnSubmit.Visible = false;
                            mobile_cancel.Visible = false;
                            mobile_btnBack.Visible = false;
                            dvhome.Visible = false;
                            ddlResignationReason.SelectedValue = "0";
                            ddlResignationReason.Enabled = false;
                            txtNoticePeriod.Text = "";
                            txtAppComment.Enabled = false;
                        }
                    }
                    if (Request.QueryString.Count > 1)
                    {
                        hdnId.Value = Convert.ToString(Request.QueryString[0]);
                        Id = Convert.ToInt32(hdnId.Value);
                        hdnResignationID.Value = hdnId.Value;
                        GetProcessedResignationByResigId(Id);
                        GetApproversStatus(txtEmpCode.Text, Convert.ToInt32(hdnResignationID.Value));
                        GetCurrentApprID(hdnEmpCode.Value, Convert.ToInt32(hdnResignationID.Value));
                        BindgvHistory(txtEmpCode.Text.ToString());
                        mobile_btnSave.Visible = false;
                        claimmob_btnSubmit.Visible = false;
                        mobile_cancel.Visible = false;
                        mobile_btnBack.Visible = true;
                        lblmessage.Text = "";
                    }

                    

                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    #endregion

    #region PageMethods
    public void GetEmpType(string EmpCode)
    {
        DataTable Dt = spm.GetEmploymentType(EmpCode);
        string EmpType = "";
        if (Dt.Rows.Count > 0)
        {
            txtNoticePeriod.Text = "";
            EmpType= Dt.Rows[0]["EMPLOYMENT_TYPE"].ToString();
            if (EmpType == "2")
            {
                txtNoticePeriod.Text = "30(day)s";
            }
            else
            {
                txtNoticePeriod.Text = "90(day)s";
            }
        }
    }
    public void Fill_ResignationReason()
    {
        DataTable Dt = spm.GetResignationReason();
        ddlResignationReason.DataSource = Dt;

        ddlResignationReason.DataTextField = "Particulars";
        ddlResignationReason.DataValueField = "pid";
        ddlResignationReason.DataBind();
        ListItem item = new ListItem("Select Resignation Reason", "0");
        ddlResignationReason.Items.Insert(0, item);
    }

    public void GetPendingResignationDetails(int Id)
    {
        try
        {
            DataTable dtResigDetails = new DataTable();
            dtResigDetails = spm.GetPendingResignationByResigId(Id, hdnEmpCode.Value);
            if (dtResigDetails.Rows.Count > 0)
            {
                txtEmpCode.Text = Convert.ToString(dtResigDetails.Rows[0]["Emp_Code"]);
                txtEmpName.Text = Convert.ToString(dtResigDetails.Rows[0]["Emp_Name"]);
                txtEmpLocation.Text = Convert.ToString(dtResigDetails.Rows[0]["emp_location"]);
                txtEmpDesig.Text = Convert.ToString(dtResigDetails.Rows[0]["Designation"]);
                txtEmpDept.Text = Convert.ToString(dtResigDetails.Rows[0]["Department"]);
                txtBand.Text = Convert.ToString(dtResigDetails.Rows[0]["Band"]);
                txtDoj.Text = Convert.ToString(dtResigDetails.Rows[0]["doj"]);
                txtFromdate_N.Text = Convert.ToString(dtResigDetails.Rows[0]["ResignationDate"].ToString());
                txtTodate_N.Text = Convert.ToString(dtResigDetails.Rows[0]["LastWorkingDate"].ToString());
                txtEmpComment.Text = Convert.ToString(dtResigDetails.Rows[0]["Remarks"].ToString());
                ddlResignationReason.SelectedValue = dtResigDetails.Rows[0]["ReasonID"].ToString();
                lnkuplodedfile.Text = Convert.ToString(dtResigDetails.Rows[0]["fileNames"]).Trim();
                hdn_Attchment.Value = Convert.ToString(dtResigDetails.Rows[0]["fileNames"]).Trim();
                GetEmpType(txtEmpCode.Text);
                mobile_btnBack.Visible = true;
                mobile_btnSave.Visible = false;
                claimmob_btnSubmit.Visible = false;
                mobile_cancel.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void GetProcessedResignationByResigId(int Id)
    {
        try
        {
            DataTable dtResigDetails = new DataTable();
            dtResigDetails = spm.GetProcessedResignationByResigId(Id, hdnEmpCode.Value);
            if (dtResigDetails.Rows.Count > 0)
            {
                txtEmpCode.Text = Convert.ToString(dtResigDetails.Rows[0]["Emp_Code"]);
                txtEmpName.Text = Convert.ToString(dtResigDetails.Rows[0]["Emp_Name"]);
                txtEmpLocation.Text = Convert.ToString(dtResigDetails.Rows[0]["emp_location"]);
                txtEmpDesig.Text = Convert.ToString(dtResigDetails.Rows[0]["Designation"]);
                txtEmpDept.Text = Convert.ToString(dtResigDetails.Rows[0]["Department"]);
                txtBand.Text = Convert.ToString(dtResigDetails.Rows[0]["Band"]);
                txtDoj.Text = Convert.ToString(dtResigDetails.Rows[0]["doj"]);
                txtFromdate_N.Text = Convert.ToString(dtResigDetails.Rows[0]["ResignationDate"].ToString());
                txtTodate_N.Text = Convert.ToString(dtResigDetails.Rows[0]["LastWorkingDate"].ToString());
                
                txtEmpComment.Text = Convert.ToString(dtResigDetails.Rows[0]["Remarks"].ToString());
                ddlResignationReason.SelectedValue = dtResigDetails.Rows[0]["ReasonID"].ToString();
                lnkuplodedfile.Text = Convert.ToString(dtResigDetails.Rows[0]["fileNames"]).Trim();
                hdn_Attchment.Value = Convert.ToString(dtResigDetails.Rows[0]["fileNames"]).Trim();
                txtAppComment.Text = Convert.ToString(dtResigDetails.Rows[0]["AppRemarks"].ToString());
                GetEmpType(txtEmpCode.Text);
                mobile_btnBack.Visible = true;
                mobile_btnSave.Visible = false;
                claimmob_btnSubmit.Visible = false;
                mobile_cancel.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void GetApproversStatus(string empcode, int ResignationID)
    {
        dsapprover = spm.GetExitProcApproverStatus(empcode, ResignationID);
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();

        if (dsapprover.Rows.Count > 0)
        {
            DgvApprover.DataSource = dsapprover;
            DgvApprover.DataBind();
        }
    }
    //get current approver details-
    protected void GetCurrentApprID(string empcode, int ResignationID)
    {
        int capprid;
        string Actions = "";
        DataTable dtCApprID = new DataTable();
        dtCApprID = spm.GetExitProcCurrentApprID(empcode, ResignationID);
        capprid = (int)dtCApprID.Rows[0]["APPR_ID"];
        Actions = (string)dtCApprID.Rows[0]["Action"];
        hdnCurrentID.Value = capprid.ToString();

        if (Convert.ToString(hdnCurrentID.Value).Trim() == "")
        {
            lblmessage.Text = "Acton on this Request not yet taken by other approvals";
            return;
        }
        else if (Convert.ToString(Actions).Trim() != "Pending")
        {
            lblmessage.Text = "You already actioned for this request";
            return;
        }
    }

    //Get Next Approver Details-
    protected void GetNxtApprAndIntermediateDetails(string empcode, int ResignationID)
    {
        DataTable dsapproverNxt = new DataTable();
        dsapproverNxt = spm.GetExitProcNextApproverDetails(empcode, ResignationID);
        if (dsapproverNxt.Rows.Count > 0)
        {
            apprid = (int)dsapproverNxt.Rows[0]["APPR_ID"];
            nxtapprcode = (string)dsapproverNxt.Rows[0]["A_EMP_CODE"];
            nxtapprname = (string)dsapproverNxt.Rows[0]["Emp_Name"];
            approveremailaddress = (string)dsapproverNxt.Rows[0]["Emp_Emailaddress"];
            hdnnextappcode.Value = nxtapprcode;
            hdnapprid.Value = apprid.ToString();
            hflApproverEmail.Value = approveremailaddress;

            DataTable dtIntermediateEmail = new DataTable();
            dtIntermediateEmail = spm.GetExitProcNextIntermediateName(Convert.ToInt32(hdnCurrentID.Value), ResignationID, txtEmpCode.Text);
            if (dtIntermediateEmail.Rows.Count > 0)
            {
                hdnIntermediateEmail.Value = (string)dtIntermediateEmail.Rows[0]["Emp_Emailaddress"];
            }
        }
        else
        {
            hdnstaus.Value = "Final Approver";
        }
    }

    public string CheckIsLoggedInUserAsApprover(int ResignationID)
    {
        string ApproverEmpCode = "";
        DataTable dtapprover = spm.GetApproverDetails(ResignationID);
        if (dtapprover.Rows.Count > 0)
        {
            ApproverEmpCode = Convert.ToString(dtapprover.Rows[0]["ApproverEmpCode"]);
        }
        return ApproverEmpCode;
    }

    protected void BindgvHistory(string empcode)
    {
        dsapprover = spm.GetResignationHistory(empcode);
        gvHistory.DataSource = null;
        gvHistory.DataBind();

        if (dsapprover.Rows.Count > 0)
        {
            gvHistory.DataSource = dsapprover;
            gvHistory.DataBind();
        }
    }

    #endregion

    #region Approve, Reject,Cancel Buttons
    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        //Save-Approve
        int StatusID = 2;
        lblmessage.Text = "";

        if (Convert.ToString(txtAppComment.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter comment";
            return;
        }

        
        GetNxtApprAndIntermediateDetails(txtEmpCode.Text, Convert.ToInt32(hdnResignationID.Value));
        if (Convert.ToString((hdnstaus.Value).Trim()) != "")//Final Approver
        {
            StatusID = 1;
            //final Approver  eg. wanisir

            //1.update resignation details with status --qtype-UpdateResignation
            //2.Update AppStatus table with status as approved
            //3.send mail to All as resignation acceptance

            //1.update resignation details with status --qtype-UpdateResignation
            spm.ExitProcUpdateResignationAppr(Convert.ToInt32(hdnResignationID.Value), 2);

            //------Update approvalStatus as Approved
            spm.ExitProcUpdateAppStatus(Convert.ToInt32(hdnResignationID.Value), txtAppComment.Text.ToString(), 2, hdnEmpCode.Value);

            //Send Acceptance Mail only at Final approver--its pending
            //Get CC mail-ids
            string cc_email = "";
            SqlParameter[] spars1 = new SqlParameter[2];
            spars1[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars1[0].Value = "ResignMail_ApproverDetails_cc";

            spars1[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars1[1].Value = hdnEmpCode.Value.ToString();

            DataTable tt2 = spm.getData_FromCode(spars1, "SP_ExitProcess");
            if (tt2.Rows.Count > 0)
            {
                cc_email = "";

                foreach (DataRow row in tt2.Rows)
                {
                    cc_email = cc_email + Convert.ToString(row["email"].ToString()) + ";";
                }
            }
            //===================================
            string emp_name = "", Emp_EmailID="";
            StringBuilder strbuild = new StringBuilder();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "ResignationMail_EmployeeDetails";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = txtEmpCode.Text.ToString();

            DataTable tt = spm.getData_FromCode(spars, "SP_ExitProcess");

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
                            + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
                            + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
                            + "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
						   + "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
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
                            + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
                            + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
                            + "</td></tr><tr><td>Last Working Date subject to policy guidelines: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
							+ "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
							+ "</td></tr>");


                    }
                    emp_name = Convert.ToString(row["Emp_Name"].ToString());
					Emp_EmailID = Convert.ToString(row["Emp_Emailaddress"].ToString());

				}
				strbuild.Append("</table>");
            }
            string strsubject = "Resignation acknowledgment";//hdnIntermediateEmail.Value,
			spm.SendMailOnFinalApprResignationAcceptance(Emp_EmailID, strsubject, emp_name, Convert.ToString(strbuild), cc_email);
        }
        else
        {
            //ex Raj sir

            //1.update resignation details with status--qtype-UpdateResignation
            //2.UpdateAppstatus as Approved
            //3.InsertAppStatus new entry of wani sir as pending

            //1.update resignation details with status--qtype-UpdateResignation
            spm.ExitProcUpdateResignation(Convert.ToInt32(hdnResignationID.Value), 1);

            //------Update approvalStatus as Approved
            spm.ExitProcUpdateAppStatus(Convert.ToInt32(hdnResignationID.Value), txtAppComment.Text.ToString(), StatusID, hdnEmpCode.Value);

            //3.InsertAppStatus new entry of wani sir as pending
            SqlParameter[] spars2 = new SqlParameter[6];
            spars2[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars2[1] = new SqlParameter("@ResignationID", SqlDbType.Int);
            spars2[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars2[3] = new SqlParameter("@A_EmpCode", SqlDbType.VarChar);
            spars2[4] = new SqlParameter("@StatusID", SqlDbType.Int);
            spars2[5] = new SqlParameter("@Appr_Id", SqlDbType.Int);

            spars2[0].Value = "InsertAppStatus";
            spars2[1].Value = Convert.ToInt32(hdnResignationID.Value);
            spars2[2].Value = txtEmpCode.Text.ToString();
            spars2[3].Value = nxtapprcode;
            spars2[4].Value = '1';
            spars2[5].Value = apprid;
            spm.Insert_Data(spars2, "SP_ExitProcess");

            string emp_name = "";
            StringBuilder strbuild = new StringBuilder();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "ResignationMail_EmployeeDetails";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = txtEmpCode.Text.ToString();

            DataTable tt = spm.getData_FromCode(spars, "SP_ExitProcess");

            //Send Mail-To Next approver
            #region MailSend functionality
            string ResigForm_AppLink = "http://localhost//hrms/login.aspx?ReturnUrl=procs/ResignationForm_App.aspx";
            string redirectURL = Convert.ToString(ResigForm_AppLink).Trim() + "?id=" + Convert.ToInt32(hdnResignationID.Value);

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
                        + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
                        + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
                        + "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
						+ "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())

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
                        + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
                        + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
                        + "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
						+ "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())

						+ "</td></tr>");
                    }
                    emp_name = Convert.ToString(row["Emp_Name"].ToString());
                }
                strbuild.Append("</table>");
            }
            string strsubject = emp_name + " resigned";
            spm.SendMailOnResignation(hflApproverEmail.Value, strsubject, emp_name, Convert.ToString(strbuild), "", redirectURL);
            #endregion
        }

        lblmessage.Visible = true;
        lblmessage.Text = "Resignation Reuqest Approved Successfully";
        Response.Redirect("~/procs/InboxResignations.aspx");
    }


    protected void claimmob_btnSubmit_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }


        //Rejected
        int StatusID = 4;//Rejected
        lblmessage.Text = "";

        if (Convert.ToString(txtAppComment.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter comment";
            return;
        }

       
        GetNxtApprAndIntermediateDetails(txtEmpCode.Text, Convert.ToInt32(hdnResignationID.Value));
        spm.ExitProcUpdateResignation(Convert.ToInt32(hdnResignationID.Value), StatusID);
        spm.ExitProcUpdateAppStatus(Convert.ToInt32(hdnResignationID.Value), txtAppComment.Text.ToString(), StatusID, hdnEmpCode.Value);

        int apprid = 0;
        string Approvers_code = "";
        string approveremailaddress = "";
        DataTable dtApproverEmailIds = spm.GetExitProcApproverDetails_Prev(txtEmpCode.Text.ToString(), Convert.ToInt32(hdnResignationID.Value));

        if (dtApproverEmailIds.Rows.Count > 0)
        {
            approveremailaddress = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];
            apprid = (int)dtApproverEmailIds.Rows[0]["APPR_ID"];
            Approvers_code = (string)dtApproverEmailIds.Rows[0]["A_EMP_CODE"];
            hflapprcode.Value = Approvers_code;
        }
        foreach (DataRow row in dtApproverEmailIds.Rows)
        {
            hdnApprEmailaddress.Value = hdnApprEmailaddress.Value + Convert.ToString(row["Emp_Emailaddress"].ToString()) + ";";
        }
        string emp_name = "", empemailId = "";
        StringBuilder strbuild = new StringBuilder();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "ResignationMail_EmployeeDetails";

        spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        spars[1].Value = txtEmpCode.Text.ToString();

        DataTable tt = spm.getData_FromCode(spars, "SP_ExitProcess");

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
                        + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
                        + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
                        + "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
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
                        + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
                        + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
                        + "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
                        + "</td></tr>");


                }
                emp_name = Convert.ToString(row["Emp_Name"].ToString());
                empemailId = Convert.ToString(row["Emp_Emailaddress"].ToString());
            }
            strbuild.Append("</table>");
        }
        string cc_email = hdnApprEmailaddress.Value;
        string strsubject = "Your Resignation is Rejected";
        spm.SendMailOnResignationRejection(empemailId, strsubject, emp_name, Convert.ToString(strbuild), cc_email);
        lblmessage.Visible = true;
        lblmessage.Text = "Resignation Reuqest Rejected Successfully";
        Response.Redirect("~/procs/InboxResignations.aspx");
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {   //Back
        Response.Redirect("~/procs/InboxResignations.aspx");
    }
    protected void mobile_cancel_Click(object sender, EventArgs e)
    {   //Cancel  
        Response.Redirect("~/procs/ProcessedResignations.aspx");
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
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    }

    //protected void lnkuplodedfile_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ResigAttachmentPath"]).Trim()), lnkuplodedfile.Text.Trim());

    //        Response.ContentType = ContentType;
    //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
    //        Response.WriteFile(strfilepath);
    //        Response.End();
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message.ToString());
    //    }
    //}
}