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

public partial class procs_KRA_DeemedApproval : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
        }
        if (Page.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/KRA_Index.aspx");
        }

        lblmessage.Text = "";

        
        if (!Page.IsPostBack)
        {
            hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();
            get_KRAPeriodList();
          //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ASD", "StartLoader();", true);
        }

    }
   
    //private void getDepartmentStatusReport()
    //{


    //    #region get KRA Team Status

    //    DataSet dtKRATeam = new DataSet();
    //    SqlParameter[] spars = new SqlParameter[4];

    //    spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
    //    spars[0].Value = "Rpt_Department_wise_status";

    //    spars[1] = new SqlParameter("@dept_id", SqlDbType.Int);
    //    if (Convert.ToString(lstDepartment.SelectedValue).Trim() != "0")
    //        spars[1].Value = Convert.ToInt32(lstDepartment.SelectedValue);
    //    else
    //        spars[1].Value = DBNull.Value;

    //    spars[2] = new SqlParameter("@period_id", SqlDbType.Int);
    //    spars[2].Value = Convert.ToInt32(lstPeriod.SelectedValue);

    //    spars[3] = new SqlParameter("@ApprovalType", SqlDbType.VarChar);
    //    if (DDLApprovalType.SelectedItem.Text == "Approval Type")
    //    {
    //        spars[3].Value = DBNull.Value;
    //    }
    //    else
    //    {
    //        spars[3].Value = DDLApprovalType.SelectedValue;
    //    }
    //    dtKRATeam = spm.getDatasetList(spars, "SP_KRA_Reports");

    //    #endregion

    //    try
    //    {
    //        string strpath = Server.MapPath("~/procs/KRADepartmentStatusRpt.rdlc");
    //        ReportViewer1.ProcessingMode = ProcessingMode.Local;
    //        ReportViewer1.LocalReport.ReportPath = strpath;

    //        ReportDataSource rds_summary = new ReportDataSource("dsDeptStatus_Summary", dtKRATeam.Tables[0]);
    //        ReportDataSource rds_KRANotSubmitted = new ReportDataSource("dsKRA_NotSubmitted", dtKRATeam.Tables[1]);
    //        ReportDataSource rds_KRASubmitted = new ReportDataSource("dsKRA_Submitted", dtKRATeam.Tables[2]);



    //        ReportViewer1.LocalReport.DataSources.Clear();
    //        ReportViewer1.LocalReport.DataSources.Add(rds_summary);
    //        ReportViewer1.LocalReport.DataSources.Add(rds_KRANotSubmitted);
    //        ReportViewer1.LocalReport.DataSources.Add(rds_KRASubmitted);
    //        ReportViewer1.LocalReport.Refresh();
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}

    public void get_KRAPeriodList()
    {
        DataSet dtPeriod = new DataSet();

        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_KRAPeriod_list";

        dtPeriod = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
        if (dtPeriod.Tables[0].Rows.Count > 0)
        {
            lstPeriod.DataSource = dtPeriod.Tables[0];
            lstPeriod.DataTextField = "period_name";
            lstPeriod.DataValueField = "period_id";
            lstPeriod.DataBind();
            lstPeriod.Items.Insert(0, new ListItem("Select Period", "0"));
        }

    }

    protected void lstPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet DSPeriod = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_KRAPeriod_SelectDate";
        if (lstPeriod.SelectedValue == "0")
        {
            Txt_FromDateKRA.Text = "";
            Txt_ToDateKRA.Text = "";
            btnback_mng.Enabled = false;
            btnback_mng.Visible = false;
        }
        else
        {
            spars[1] = new SqlParameter("@period_id", SqlDbType.VarChar);
            spars[1].Value = lstPeriod.SelectedValue;
            DSPeriod = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
            if (DSPeriod.Tables[0].Rows.Count > 0)
            {
                Txt_FromDateKRA.Text = DSPeriod.Tables[0].Rows[0]["KRA_submit_from_date"].ToString();
                Txt_ToDateKRA.Text = DSPeriod.Tables[0].Rows[0]["KRA_submit_to_date"].ToString();

                if (DSPeriod.Tables[1].Rows.Count > 0)
                {
                    btnback_mng.Visible = false;
                    lblmessage.Visible = true;
                    lblmessage.Text = "Deemed approvals can only be made after To Date.";

                }
                else
                {

                    lblmessage.Text = "";
                    lblmessage.Visible = true;
                    btnback_mng.Enabled = true;
                    btnback_mng.Visible = true;
                }
            }
        }
    }

    protected void btnback_mng_Click(object sender, EventArgs e)
    {
        try
        {

            if (Convert.ToString(lstPeriod.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select period";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ASD", "StopLoader();", true);
                return;
            }

            #region get Deemed Approval Records

            DataSet DSKRATeam = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "GetDeemedApprovalSubmitDateTo";

            spars[1] = new SqlParameter("@period_id", SqlDbType.VarChar);
            spars[1].Value = lstPeriod.SelectedValue;

            DSKRATeam = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

            string msgflag = "";

            //if (DSKRATeam != null)
            //{
            //    if (DSKRATeam.Tables.Count > 0)
            //    {
            //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ASD", "StopLoader();", true);
            //    }
            //}
            if (DSKRATeam.Tables[0].Rows.Count > 0)
            {
                FirsttimeInsertAcceptApprove(DSKRATeam);
                msgflag = "1";
            }
            if (DSKRATeam.Tables[1].Rows.Count > 0)
            {
                AcceptApproveFinalFlow(DSKRATeam);
                msgflag = "1";
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ASD", "StopLoader();", true);
            if (msgflag == "1")
            {
                lblmessage.Text = "Deemed Approval Automation Submitted Successfully";
               // btnback_mng.PostBackUrl= "procs/KRA_DeemedApproval.aspx";
            }

            #endregion
        }
        catch (Exception ex)
        {

        }

    }

    public void FirsttimeInsertAcceptApprove(DataSet DSKRATeam)
    {
        try
        {
            if (DSKRATeam.Tables[0].Rows.Count > 0)
        {
            string KRAID = "";
            for (int i = 0; i < DSKRATeam.Tables[0].Rows.Count; i++)
            {
                string Stremp_code = DSKRATeam.Tables[0].Rows[i]["emp_code"].ToString();
                Boolean StrisDraft = false;
                string StrRole_Id = DSKRATeam.Tables[0].Rows[i]["Role_Id"].ToString();
                string StrTemp_KRA_ID = DSKRATeam.Tables[0].Rows[i]["Temp_KRA_ID"].ToString();
                string StrPeriodName = DSKRATeam.Tables[0].Rows[i]["period_name"].ToString();
                string StrPeriodID = DSKRATeam.Tables[0].Rows[i]["Period_Id"].ToString();
                string StrEmpName = DSKRATeam.Tables[0].Rows[i]["Emp_Name"].ToString();
                string StrEmpEmailAddress = DSKRATeam.Tables[0].Rows[i]["Emp_Emailaddress"].ToString();

                DateTime dsysdate = DateTime.Now.Date;
                var year = dsysdate.Year;
                var month = dsysdate.Month;
                var day = dsysdate.Day;
                string StrKRAFilePath = Convert.ToString(StrPeriodName).Trim() + "_" + Convert.ToString(Stremp_code).Trim() + "_" + day + "." + month + "." + year + ".pdf"; //"testing.pdf";

                DataSet DSKRAInsert = new DataSet();
                SqlParameter[] sparsInsert = new SqlParameter[7];

                sparsInsert[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                sparsInsert[0].Value = "insert_KRA_mainDeemedAcceptance";
                sparsInsert[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
                sparsInsert[1].Value = Stremp_code.ToString();
                sparsInsert[2] = new SqlParameter("@IsDraft", SqlDbType.VarChar);
                sparsInsert[2].Value = StrisDraft;
                sparsInsert[3] = new SqlParameter("@KRA_ID", SqlDbType.VarChar);
                sparsInsert[3].Value = DBNull.Value;
                sparsInsert[4] = new SqlParameter("@Role_Id", SqlDbType.VarChar);
                sparsInsert[4].Value = StrRole_Id.ToString();
                sparsInsert[5] = new SqlParameter("@Temp_KRA_ID", SqlDbType.VarChar);
                sparsInsert[5].Value = StrTemp_KRA_ID.ToString();
                sparsInsert[6] = new SqlParameter("@KRAFilePath", SqlDbType.VarChar);
                sparsInsert[6].Value = StrKRAFilePath;

                DSKRAInsert = spm.getDatasetList(sparsInsert, "SP_KRA_Insert_Update");

                KRAID = DSKRAInsert.Tables[0].Rows[0]["MaxKRAID"].ToString();




                #region export KRA pdf if it's final approver

                DataSet DSKRAPDF = new DataSet();
                var my_date = DateTime.Now;
                SqlParameter[] sparsPDF = new SqlParameter[2];
                sparsPDF[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                sparsPDF[0].Value = "Rpt_get_employee_KRA_details";
                sparsPDF[1] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
                sparsPDF[1].Value = Convert.ToDecimal(KRAID);
                DSKRAPDF = spm.getDatasetList(sparsPDF, "SP_KRA_GETALL_DETAILS");


              //  string StrFileName = "KRAprt.rdlc";
 
                //string strpath = @"D:\HRMS\hrms\procs\" + StrFileName;
                string strpath = Server.MapPath("~/procs/KRAprt.rdlc");
                string StrKRAFiles = @"D:\HRMS\hrms\EmployeeKRA\";

                ReportViewer ReportViewer2 = new ReportViewer();
                ReportViewer2.ProcessingMode = ProcessingMode.Local;

                ReportViewer2.LocalReport.ReportPath = strpath;
                ReportDataSource rds = new ReportDataSource("dsKRA", DSKRAPDF.Tables[0]);
                ReportDataSource rds_goal = new ReportDataSource("dsKRA_Goals", DSKRAPDF.Tables[1]);
                ReportDataSource rds_Reviewee = new ReportDataSource("dsKRA_Reviewee", DSKRAPDF.Tables[2]);
                ReportDataSource rds_Reviewer = new ReportDataSource("dsKRA_Reviewer", DSKRAPDF.Tables[3]);
                ReportDataSource rds_Final_Reviewer = new ReportDataSource("dsKRA_FinalReviewer", DSKRAPDF.Tables[4]);

                ReportViewer2.LocalReport.DataSources.Clear();
                ReportViewer2.LocalReport.DataSources.Add(rds);
                ReportViewer2.LocalReport.DataSources.Add(rds_goal);
                ReportViewer2.LocalReport.DataSources.Add(rds_Reviewee);
                ReportViewer2.LocalReport.DataSources.Add(rds_Reviewer);
                ReportViewer2.LocalReport.DataSources.Add(rds_Final_Reviewer);


                byte[] Bytes = ReportViewer2.LocalReport.Render(format: "PDF", deviceInfo: "");
                using (FileStream stream = new FileStream(Server.MapPath(ConfigurationManager.AppSettings["KRAFiles"]) + StrKRAFilePath, FileMode.Create))
                {
                    stream.Write(Bytes, 0, Bytes.Length);
                }

                #endregion

                #region send mail to Reviewee for approved KRA

                string sApproverEmail_CC = "";

                DataSet dsKRAApprover = new DataSet();
                SqlParameter[] sparsEmail = new SqlParameter[4];
                sparsEmail[0] = new SqlParameter("@period_id", SqlDbType.Int);
                sparsEmail[0].Value = Convert.ToInt32(lstPeriod.SelectedValue);
                sparsEmail[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
                sparsEmail[1].Value = Stremp_code;
                sparsEmail[2] = new SqlParameter("@stype", SqlDbType.VarChar);
                sparsEmail[2].Value = "KRA_approvers";
                sparsEmail[3] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
                sparsEmail[3].Value = Convert.ToDecimal(KRAID);
                dsKRAApprover = spm.getDatasetList(sparsEmail, "SP_KRA_GETALL_DETAILS");

                for (Int32 irow = 0; irow < dsKRAApprover.Tables[0].Rows.Count; irow++)
                {
                    if (Convert.ToString(sApproverEmail_CC).Trim() == "")
                        sApproverEmail_CC = Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                    else
                        sApproverEmail_CC = Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() + ";" + sApproverEmail_CC;

                }
                StringBuilder strbuild = new StringBuilder();
                strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'>");
                strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
                strbuild.Append("<tr><td style='height:20px'></td></tr>");
                strbuild.Append("<tr><td colspan='2'>This is to inform you that your KRA for the period " + StrPeriodName.ToString().Trim() + " is approved. Please find attached digitally approved KRA copy for your reference.</td></tr>");
                strbuild.Append("<tr><td style='height:20px'></td></tr>");


                StringBuilder strbuild_Approvers = new StringBuilder();
                strbuild_Approvers.Append("<table cellpadding='5' cellspacing='0' style='font-size:11pt;font-family:Arial;font-style:Regular;width:100%;'>");
                strbuild_Approvers.Append("<tr><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Reviewer Name</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Status</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Reviewed On</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Reviewer Remarks</th></tr>");
                for (Int32 irow = 0; irow < dsKRAApprover.Tables[0].Rows.Count; irow++)
                {
                    strbuild_Approvers.Append("<tr><td style='width:40%;border: 1px solid #ccc'>" + Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["ApproverName"]).Trim() + " </td>");
                    strbuild_Approvers.Append("<td style='width:10%;border: 1px solid #ccc'>" + Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Status"]).Trim() + "</td>");
                    strbuild_Approvers.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["approved_on"]).Trim() + "</td>");
                    strbuild_Approvers.Append("<td style='width:35%;border: 1px solid #ccc'>" + Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Remarks"]).Trim() + "</td></tr>");
                }
                strbuild_Approvers.Append("</table>");
                string sattchedfileName = StrKRAFiles + StrKRAFilePath;
                string strsubject = "KRA approved for the period " + StrPeriodName.ToString().Trim() + " of " + Convert.ToString(StrEmpName).Trim();

                spm.sendMail(StrEmpEmailAddress.Trim(), strsubject, Convert.ToString(strbuild) + Convert.ToString(strbuild_Approvers), sattchedfileName, sApproverEmail_CC);

                #endregion
            }
        }
        }
        catch (Exception ex)
        {

        }
    }

    public void AcceptApproveFinalFlow(DataSet DSKRATeam)
    {
        try
        {
            if (DSKRATeam.Tables[1].Rows.Count > 0)
            {
                string KRAID = "";
                for (int i = 0; i < DSKRATeam.Tables[1].Rows.Count; i++)
                {
                    string Stremp_code = DSKRATeam.Tables[1].Rows[i]["emp_code"].ToString();
                    string StrRole_Id = DSKRATeam.Tables[1].Rows[i]["Role_Id"].ToString();
                    string StrTemp_KRA_ID = DSKRATeam.Tables[1].Rows[i]["Temp_KRA_ID"].ToString();
                    string StrPeriodName = DSKRATeam.Tables[1].Rows[i]["period_name"].ToString();
                    string StrPeriodID = DSKRATeam.Tables[1].Rows[i]["Period_Id"].ToString();
                    string StrEmpName = DSKRATeam.Tables[1].Rows[i]["Emp_Name"].ToString();
                    string StrEmpEmailAddress = DSKRATeam.Tables[1].Rows[i]["Emp_Emailaddress"].ToString();

                    DateTime dsysdate = DateTime.Now.Date;
                    var year = dsysdate.Year;
                    var month = dsysdate.Month;
                    var day = dsysdate.Day;
                    string StrKRAFilePath = Convert.ToString(StrPeriodName).Trim() + "_" + Convert.ToString(Stremp_code).Trim() + "_" + day + "." + month + "." + year + ".pdf"; //"testing.pdf";

                    DataSet DSKRAInsert = new DataSet();
                    SqlParameter[] sparsInsert = new SqlParameter[4];

                    sparsInsert[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                    sparsInsert[0].Value = "insert_KRA_mainDeemedAcceptanceReviewer";
                    sparsInsert[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
                    sparsInsert[1].Value = Stremp_code.ToString();
                    sparsInsert[2] = new SqlParameter("@Temp_KRA_ID", SqlDbType.VarChar);
                    sparsInsert[2].Value = StrTemp_KRA_ID.ToString();
                    sparsInsert[3] = new SqlParameter("@KRAFilePath", SqlDbType.VarChar);
                    sparsInsert[3].Value = StrKRAFilePath;
                    DSKRAInsert = spm.getDatasetList(sparsInsert, "SP_KRA_Insert_Update");

                    KRAID = DSKRAInsert.Tables[0].Rows[0]["MaxKRAID"].ToString();

                    DataSet dsKRAApproverforloop = new DataSet();
                    SqlParameter[] sparsApprovalLoop = new SqlParameter[4];

                    sparsApprovalLoop[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                    sparsApprovalLoop[0].Value = "KRA_approvers";
                    sparsApprovalLoop[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
                    sparsApprovalLoop[1].Value = Stremp_code.ToString();
                    sparsApprovalLoop[2] = new SqlParameter("@period_id", SqlDbType.Int);
                    sparsApprovalLoop[2].Value = Convert.ToInt32(lstPeriod.SelectedValue);
                    sparsApprovalLoop[3] = new SqlParameter("@KRA_ID", SqlDbType.VarChar);
                    sparsApprovalLoop[3].Value = Convert.ToDecimal(0);

                    dsKRAApproverforloop = spm.getDatasetList(sparsApprovalLoop, "SP_KRA_GETALL_DETAILS");

                    for (Int32 irow = 0; irow < dsKRAApproverforloop.Tables[0].Rows.Count; irow++)
                    {
                        string ApprovalEmpCode = Convert.ToString(dsKRAApproverforloop.Tables[0].Rows[irow]["Approver_emp_code"]).Trim();
                        string ApprovalID = Convert.ToString(dsKRAApproverforloop.Tables[0].Rows[irow]["approver_id"]).Trim();

                        DataSet dsKRAforloopUpdate = new DataSet();
                        SqlParameter[] sparsApprovalLoopUpdate = new SqlParameter[4];

                        sparsApprovalLoopUpdate[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                        sparsApprovalLoopUpdate[0].Value = "insert_KRA_mainDeemedReviewerUpdateApproval";
                        sparsApprovalLoopUpdate[1] = new SqlParameter("@Approver_emp_code", SqlDbType.VarChar);
                        sparsApprovalLoopUpdate[1].Value = ApprovalEmpCode;
                        sparsApprovalLoopUpdate[2] = new SqlParameter("@Approver_id", SqlDbType.Int);
                        sparsApprovalLoopUpdate[2].Value = Convert.ToInt32(ApprovalID);
                        sparsApprovalLoopUpdate[3] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
                        sparsApprovalLoopUpdate[3].Value = Convert.ToDecimal(KRAID);
                        dsKRAforloopUpdate = spm.getDatasetList(sparsApprovalLoopUpdate, "SP_KRA_Insert_Update");
                    }


                    #region export KRA pdf if it's final approver

                    DataSet DSKRAPDF = new DataSet();
                    var my_date = DateTime.Now;
                    SqlParameter[] sparsPDF = new SqlParameter[2];
                    sparsPDF[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                    sparsPDF[0].Value = "Rpt_get_employee_KRA_details";
                    sparsPDF[1] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
                    sparsPDF[1].Value = Convert.ToDecimal(KRAID);
                    DSKRAPDF = spm.getDatasetList(sparsPDF, "SP_KRA_GETALL_DETAILS");


                   // string StrFileName = "KRAprt.rdlc";
                   // string strpath = @"D:\HRMS\hrms\procs\" + StrFileName;
                    string strpath = Server.MapPath("~/procs/KRAprt.rdlc");
                    string StrKRAFiles = @"D:\HRMS\hrms\EmployeeKRA\";

                    ReportViewer ReportViewer2 = new ReportViewer();
                    ReportViewer2.ProcessingMode = ProcessingMode.Local;

                    ReportViewer2.LocalReport.ReportPath = strpath;
                    ReportDataSource rds = new ReportDataSource("dsKRA", DSKRAPDF.Tables[0]);
                    ReportDataSource rds_goal = new ReportDataSource("dsKRA_Goals", DSKRAPDF.Tables[1]);
                    ReportDataSource rds_Reviewee = new ReportDataSource("dsKRA_Reviewee", DSKRAPDF.Tables[2]);
                    ReportDataSource rds_Reviewer = new ReportDataSource("dsKRA_Reviewer", DSKRAPDF.Tables[3]);
                    ReportDataSource rds_Final_Reviewer = new ReportDataSource("dsKRA_FinalReviewer", DSKRAPDF.Tables[4]);

                    ReportViewer2.LocalReport.DataSources.Clear();
                    ReportViewer2.LocalReport.DataSources.Add(rds);
                    ReportViewer2.LocalReport.DataSources.Add(rds_goal);
                    ReportViewer2.LocalReport.DataSources.Add(rds_Reviewee);
                    ReportViewer2.LocalReport.DataSources.Add(rds_Reviewer);
                    ReportViewer2.LocalReport.DataSources.Add(rds_Final_Reviewer);


                    byte[] Bytes = ReportViewer2.LocalReport.Render(format: "PDF", deviceInfo: "");
                    using (FileStream stream = new FileStream(Server.MapPath(ConfigurationManager.AppSettings["KRAFiles"]) + StrKRAFilePath, FileMode.Create))
                    {
                        stream.Write(Bytes, 0, Bytes.Length);
                    }

                    #endregion

                    #region send mail to Reviewee for approved KRA

                    string sApproverEmail_CC = "";

                    DataSet dsKRAApprover = new DataSet();
                    SqlParameter[] sparsEmail = new SqlParameter[4];
                    sparsEmail[0] = new SqlParameter("@period_id", SqlDbType.Int);
                    sparsEmail[0].Value = Convert.ToInt32(lstPeriod.SelectedValue);
                    sparsEmail[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
                    sparsEmail[1].Value = Stremp_code;
                    sparsEmail[2] = new SqlParameter("@stype", SqlDbType.VarChar);
                    sparsEmail[2].Value = "KRA_approvers";
                    sparsEmail[3] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
                    sparsEmail[3].Value = Convert.ToDecimal(KRAID);
                    dsKRAApprover = spm.getDatasetList(sparsEmail, "SP_KRA_GETALL_DETAILS");

                    for (Int32 irow = 0; irow < dsKRAApprover.Tables[0].Rows.Count; irow++)
                    {
                        if (Convert.ToString(sApproverEmail_CC).Trim() == "")
                            sApproverEmail_CC = Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                        else
                            sApproverEmail_CC = Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() + ";" + sApproverEmail_CC;

                    }
                    StringBuilder strbuild = new StringBuilder();
                    strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'>");
                    strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
                    strbuild.Append("<tr><td style='height:20px'></td></tr>");
                    strbuild.Append("<tr><td colspan='2'>This is to inform you that your KRA for the period " + StrPeriodName.ToString().Trim() + " is approved. Please find attached digitally approved KRA copy for your reference.</td></tr>");
                    strbuild.Append("<tr><td style='height:20px'></td></tr>");


                    StringBuilder strbuild_Approvers = new StringBuilder();
                    strbuild_Approvers.Append("<table cellpadding='5' cellspacing='0' style='font-size:11pt;font-family:Arial;font-style:Regular;width:100%;'>");
                    strbuild_Approvers.Append("<tr><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Reviewer Name</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Status</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Reviewed On</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Reviewer Remarks</th></tr>");
                    for (Int32 irow = 0; irow < dsKRAApprover.Tables[0].Rows.Count; irow++)
                    {
                        strbuild_Approvers.Append("<tr><td style='width:40%;border: 1px solid #ccc'>" + Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["ApproverName"]).Trim() + " </td>");
                        strbuild_Approvers.Append("<td style='width:10%;border: 1px solid #ccc'>" + Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Status"]).Trim() + "</td>");
                        strbuild_Approvers.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["approved_on"]).Trim() + "</td>");
                        strbuild_Approvers.Append("<td style='width:35%;border: 1px solid #ccc'>" + Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Remarks"]).Trim() + "</td></tr>");
                    }
                    strbuild_Approvers.Append("</table>");
                    string sattchedfileName = StrKRAFiles + StrKRAFilePath;
                    string strsubject = "KRA approved for the period " + StrPeriodName.ToString().Trim() + " of " + Convert.ToString(StrEmpName).Trim();

                    spm.sendMail(StrEmpEmailAddress.Trim(), strsubject, Convert.ToString(strbuild) + Convert.ToString(strbuild_Approvers), sattchedfileName, sApproverEmail_CC);

                    #endregion
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    
}