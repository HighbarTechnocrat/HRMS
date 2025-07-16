using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class KRA_Appr : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();

    #region Creative_Default_methods


    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0; 
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }
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

            lblmessage.Text = "";
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

               txtEmpCode.Text = Convert.ToString(Session["Empcode"]);

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/KRA_Index.aspx");
            }
            else
            {
                Page.SmartNavigation = true;

                if (!Page.IsPostBack)
                {
                    txt_remakrs.Attributes.Add("maxlength", txt_remakrs.MaxLength.ToString());
                    hdngoal_id.Value = "";
                    hdnKRA_id.Value = "";

                    //getApproverType_employeeLogin();
 
                    if (Request.QueryString.Count > 0)
                    {
                        hdnKRA_id.Value = Request.QueryString[0];
                        hdnstype_Main.Value = "Update";
                        
                    }
                     
                    getEmployeeDetails();                   
                    get_goal_measurement_Details_New();
                    get_KRA_Approvers();
                    //generate_pdf();
		   check_LoginEmployee_IsApproved_KRARequest();
                }

            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
    #endregion

    #region Page_Events
   

 

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        if (Convert.ToString(txt_remakrs.Text).Trim() == "")
        {
            lblmessage.Text = "Please mention the comment before Send for Correction";
            return;
        }
        //update status if current approer send for correction

        spm.KRA_insert_CurrentApprover_details(Convert.ToDouble(hdnKRA_id.Value), Convert.ToString(hdncurrent_Appr_Empcode.Value), Convert.ToInt32(hdncurrent_Appr_Id.Value), "Correction", Convert.ToString(txt_remakrs.Text).Trim(), false,"");

        string strsubject = "KRA Correction Request for " + Convert.ToString(txtEmployee_name.Text);
        string sApproverEmail_CC = "";
        //insert next approver status as Approved

        string strKRAURL = "";
        strKRAURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["CorrectionLink_KRA"]).Trim() + "?kraid=" + hdnKRA_id.Value).Trim() + "&mngexp=1";

        #region send mail to Next Approver

        DataSet dsKRAApprover = new DataSet();
            dsKRAApprover = spm.get_KRA_Approver_List(Convert.ToInt32(hdnPeriod_id.Value), txtemp_code.Text, "KRA_approvers", Convert.ToDecimal(hdnKRA_id.Value));
            for (Int32 irow = 0; irow < dsKRAApprover.Tables[0].Rows.Count; irow++)
            {
                if (Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Status"]).Trim() != "" && Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Status"]).Trim() != "Pending" && Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() != Convert.ToString(hdncurrent_Appr_EmpEmail.Value).Trim())
                {
                    if (Convert.ToString(sApproverEmail_CC).Trim() == "")
                        sApproverEmail_CC = Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                    else
                        sApproverEmail_CC = Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() + ";" + sApproverEmail_CC;
                }
            }

            StringBuilder strbuild = new StringBuilder();
            strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'>");
            strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td colspan='2'> This is to inform you that "+ hdncurrent_Appr_Name.Value +" has sent back your KRA for Correction. Please Correct & resubmit the same for approval.</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");

            strbuild.Append("<tr><td style='height:20px'><a href='" + strKRAURL + "'> Click Here to correct & submit. </a></td></tr>");
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
            spm.sendMail_KRA(txtEmp_email.Text, strsubject, Convert.ToString(strbuild) + Convert.ToString(strbuild_Approvers), "", sApproverEmail_CC);
            #endregion
       



        Response.Redirect("KRA_Inbox.aspx");

    }

     


    protected void lnkdelete_goal_Click(object sender, EventArgs e)
    {
         
    }

    protected void lnkedit_goal_Click(object sender, EventArgs e)
    {
       // Reference the Repeater Item using Button.
         RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem; 
        hdngoal_id.Value = (item.FindControl("hdnGoal_Id_R") as HiddenField).Value;

        Response.Redirect("KRA_goalView.aspx?kraid="+hdnKRA_id.Value +"&goalid="+hdngoal_id.Value);
    }

    

    protected void lnk_download_kra_file_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["KRASupportedfiles"]).Trim()), lnk_download_kra_file.Text);
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

    protected void trvl_btnSave_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        DateTime dsysdate = DateTime.Now.Date;
        var year = dsysdate.Year;
        var month = dsysdate.Month;
        var day = dsysdate.Day;
        //KRA File Name Period_Empcode_KRA.Final.Approved.date.pdf
        string FileName = Convert.ToString(txtPeriod.Text).Trim() + "_" + Convert.ToString(txtemp_code.Text).Trim() + "_" + day + "." + month + "." + year + ".pdf"; //"testing.pdf";

        string strKRAURL = "";
        strKRAURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["approverLink_KRA"]).Trim() + "?kraid=" + hdnKRA_id.Value).Trim();

        string strsubject = "Request for KRA Approval :-" + Convert.ToString(txtEmployee_name.Text);
        string sApproverEmail_CC = "";
        //insert next approver status as Approved
        if (Convert.ToString(hdnemplogin_type.Value).Trim() == "")
        {
            spm.KRA_insert_CurrentApprover_details(Convert.ToDouble(hdnKRA_id.Value), Convert.ToString(hdncurrent_Appr_Empcode.Value), Convert.ToInt32(hdncurrent_Appr_Id.Value), "Approved", Convert.ToString(txt_remakrs.Text).Trim(), false,"");


            //spm.KRA_insert_NextApprover_details(Convert.ToDouble(hdnKRA_id.Value), hdnNext_Appr_Empcode.Value, Convert.ToInt32(hdnNext_Appr_Id.Value), "Pending", "");
            if (hdnNext_Appr_Empcode.Value != "")
            {
                spm.KRA_insert_NextApprover_details(Convert.ToDouble(hdnKRA_id.Value), hdnNext_Appr_Empcode.Value, Convert.ToInt32(hdnNext_Appr_Id.Value), "Pending", "");
            }


            #region send mail to Next Approver

            DataSet dsKRAApprover = new DataSet();
            dsKRAApprover = spm.get_KRA_Approver_List(Convert.ToInt32(hdnPeriod_id.Value), txtemp_code.Text, "KRA_approvers",Convert.ToDecimal(hdnKRA_id.Value));
            for (Int32 irow = 0; irow < dsKRAApprover.Tables[0].Rows.Count; irow++)
            {
                if (Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Status"]).Trim() != "" && Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Status"]).Trim() != "Pending" && Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() != Convert.ToString(hdncurrent_Appr_EmpEmail.Value).Trim())
                {
                    if (Convert.ToString(sApproverEmail_CC).Trim() == "")
                        sApproverEmail_CC = Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                    else
                        sApproverEmail_CC = Convert.ToString(dsKRAApprover.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() + ";" + sApproverEmail_CC;
                }
            }

            StringBuilder strbuild = new StringBuilder();
            strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'>");
            strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td colspan='2'> " + txtEmployee_name.Text + "  has submitted KRA for the period " + Convert.ToString(txtPeriod.Text).Trim() + ". Please Review & Approve.</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");

            strbuild.Append("<tr><td style='height:20px'><a href='" + strKRAURL + "'> Click Here to review & approve. </a></td></tr>");
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
        
            spm.sendMail_KRA(hdnNext_Appr_EmpEmail.Value, strsubject, Convert.ToString(strbuild) + Convert.ToString(strbuild_Approvers), "", sApproverEmail_CC);
            #endregion
        }

        if (Convert.ToString(hdnemplogin_type.Value).Trim() == "Final Reviewer")
        {
            spm.KRA_insert_CurrentApprover_details(Convert.ToDouble(hdnKRA_id.Value), Convert.ToString(hdncurrent_Appr_Empcode.Value), Convert.ToInt32(hdncurrent_Appr_Id.Value), "Approved", Convert.ToString(txt_remakrs.Text).Trim(), true,FileName);

        }



        #region export KRA pdf if it's final approver
        if (Convert.ToString(hdnemplogin_type.Value).Trim() == "Final Reviewer")
        {
            DataSet dtKRA = new DataSet();

            #region get KRA

            var my_date = DateTime.Now;

            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Rpt_get_employee_KRA_details";

            spars[1] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
            spars[1].Value = hdnKRA_id.Value;

            dtKRA = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

            #endregion

            string strpath = Server.MapPath("~/procs/KRAprt.rdlc");

            LocalReport ReportViewer2 = new LocalReport();
            ReportViewer2.ReportPath = strpath;// Server.MapPath(strpath);
            ReportDataSource rds = new ReportDataSource("dsKRA", dtKRA.Tables[0]);
            ReportDataSource rds_goal = new ReportDataSource("dsKRA_Goals", dtKRA.Tables[1]);
            ReportDataSource rds_Reviewee = new ReportDataSource("dsKRA_Reviewee", dtKRA.Tables[2]);
            ReportDataSource rds_Reviewer = new ReportDataSource("dsKRA_Reviewer", dtKRA.Tables[3]);
            ReportDataSource rds_Final_Reviewer = new ReportDataSource("dsKRA_FinalReviewer", dtKRA.Tables[4]);

            ReportViewer2.DataSources.Clear();
            ReportViewer2.DataSources.Add(rds);
            ReportViewer2.DataSources.Add(rds_goal);
            ReportViewer2.DataSources.Add(rds_Reviewee);
            ReportViewer2.DataSources.Add(rds_Reviewer);
            ReportViewer2.DataSources.Add(rds_Final_Reviewer);

            //Export the RDLC Report to Byte Array.
            // byte[] bytes = ReportViewer2.Render("pdf", null, out contentType, out encoding, out extension, out streamIds, out warnings); 

            byte[] Bytes = ReportViewer2.Render(format: "PDF", deviceInfo: "");

            using (FileStream stream = new FileStream(Server.MapPath(ConfigurationManager.AppSettings["KRAFiles"]) + FileName, FileMode.Create))
            {
                stream.Write(Bytes, 0, Bytes.Length);
            }

        }
        #endregion


        if (Convert.ToString(hdnemplogin_type.Value).Trim() == "Final Reviewer")
        { 
            #region send mail to Reviewee for approved KRA

            DataSet dsKRAApprover = new DataSet();
            dsKRAApprover = spm.get_KRA_Approver_List(Convert.ToInt32(hdnPeriod_id.Value), txtemp_code.Text, "KRA_approvers", Convert.ToDecimal(hdnKRA_id.Value));

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
            strbuild.Append("<tr><td colspan='2'>This is to inform you that your KRA for the period " + txtPeriod.Text + " is approved. Please find attached digitally approved KRA copy for your reference.</td></tr>");
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

           //FileName = "2022-2023_00630902_12.4.2022.pdf";
            string sattchedfileName = Server.MapPath(ConfigurationManager.AppSettings["KRAFiles"]) + FileName;

            strsubject = "KRA approved for the period " + txtPeriod.Text + " of " + Convert.ToString(txtEmployee_name.Text).Trim();
            spm.sendMail_KRA(txtEmp_email.Text, strsubject, Convert.ToString(strbuild) + Convert.ToString(strbuild_Approvers), sattchedfileName, sApproverEmail_CC);

            #endregion
        }


        Response.Redirect("KRA_Inbox.aspx");

 }

    protected void trvldeatils_delete_btn_Click(object sender, EventArgs e)
    {

    }
    protected void trvldeatils_cancel_btn_Click(object sender, EventArgs e)
    {

    }

    protected void dgKRA_Details_DataBound(object sender, EventArgs e)
    {
        for (int currentRowIndex = 0; currentRowIndex < dgKRA_Details.Rows.Count; currentRowIndex++)
        {
            GridViewRow currentRow = dgKRA_Details.Rows[currentRowIndex];
            CombineColumnCells(currentRow, 0, "Goal_Id");
            CombineColumnCells(currentRow, 1, "Goal_Id");
            CombineColumnCells(currentRow, 2, "Goal_Id");
          //  CombineColumnCells(currentRow, 6, "Goal_Id");
        }

    }

    #endregion

    #region PageMethods

     private void check_LoginEmployee_IsApproved_KRARequest()
    {
        try
        {
            #region Check LoginApprover is Approver for Selected Request 
            if (Request.QueryString.Count > 0)
            {
                SqlParameter[] spars = new SqlParameter[3];

                spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                spars[0].Value = "get_IsEmployee_KRA_Approver";

                spars[1] = new SqlParameter("@approver_emp_code", SqlDbType.VarChar);
                spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

                spars[2] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
                if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
                    spars[2].Value = Convert.ToDouble(hdnKRA_id.Value);
                else
                    spars[2].Value = DBNull.Value;

                DataSet dsLoginemp = new DataSet();
                dsLoginemp = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

                if (dsLoginemp.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(dsLoginemp.Tables[0].Rows[0]["action"]).Trim() == "Approved" || Convert.ToString(dsLoginemp.Tables[0].Rows[0]["action"]).Trim() == "Reject")
                    {
                        txt_remakrs.Enabled = false;
                        trvl_btnSave.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("KRA_index.aspx");
                }
            }
            #endregion
        }
        catch (Exception ex)
        {

        }
    }


    private void getApproverType_employeeLogin()
    {
        DataSet dtgoal = new DataSet();

        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_login_Reviewee";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim(); 

        dtgoal = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");
        if(dtgoal.Tables[0].Rows.Count>0)
        {
            hdnemplogin_type.Value = Convert.ToString(dtgoal.Tables[0].Rows[0]["appr_code"]).Trim();
            if (Convert.ToString(dtgoal.Tables[0].Rows[0]["KRA_ID"]).Trim() !="")
            hdnKRA_id.Value = Convert.ToString(dtgoal.Tables[0].Rows[0]["KRA_ID"]).Trim();
        }

    }
 

    public string ReplaceInvalidChars(string filename)
        {
            Regex illegalInFileName = new Regex(@"[#%&{}\!/<'>@+*?$|=]");
            string myString = illegalInFileName.Replace(filename, "_");
            //return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
            return myString;
        }
    private void getEmployeeDetails()
    {
       try
        {
            DataSet dsEmpdtls = new DataSet();
            if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
                dsEmpdtls = spm.KRA_getEmployeedetails_KRA(Convert.ToDecimal(hdnKRA_id.Value));
            else
                dsEmpdtls = spm.KRA_getEmployeedetails(txtEmpCode.Text);

            if (dsEmpdtls.Tables[0].Rows.Count > 0)
            {
                txtemp_code.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["Emp_Code"]).Trim();
                txtEmployee_name.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["Emp_Name"]).Trim();
                txtEmp_email.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
                txtPosition.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["designation"]).Trim();
                txtDepartment.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["department"]).Trim();
                txtLocation.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["department"]).Trim();
                txtLocation.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["emp_projectName"]).Trim();
                txtPeriod.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["KAR_Period"]).Trim();
                hdnPeriod_id.Value = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["KAR_Period_id"]).Trim();
                txtKRAFromDt.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["KAR_from_dt"]).Trim();
                txtKTATodt.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["KAR_to_dt"]).Trim();
                txtKRA_SubmitDt.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["kra_submit_date"]).Trim();
                lnk_download_kra_file.Text = "";
                spnFileupload.Visible = false;
                if (Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["FilePath"]).Trim() != "")
                {
                    lnk_download_kra_file.Text = Convert.ToString(dsEmpdtls.Tables[0].Rows[0]["FilePath"]).Trim();
                    lnk_download_kra_file.Visible = true;
                    spnFileupload.Visible = true;
                }
            }
        }
        catch(Exception ex)
        {

        }
    }
     
    
     

    public void get_goal_measurement_Details_New()
    {
        DataSet dtgoal = new DataSet();

        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_goal_gridview_Approver_KRA_ID";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtemp_code.Text).Trim();

        spars[2] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
        if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
            spars[2].Value = Convert.ToDecimal(hdnKRA_id.Value);
        else
            spars[2].Value = DBNull.Value;


        dtgoal = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

        dgKRA_Details.DataSource = null;
        dgKRA_Details.DataBind();


        if (dtgoal.Tables[0].Rows.Count > 0)
        {

            dgKRA_Details.DataSource = dtgoal.Tables[0];
            dgKRA_Details.DataBind();

        }

    }



    private DataSet getMeasurements_temp_List(string igoalid)
    {

        DataSet dtmeasurement = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_measurements_temp_list";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtemp_code.Text).Trim();

        spars[2] = new SqlParameter("@Goal_Id", SqlDbType.Int);
        if (Convert.ToString(igoalid).Trim() != "")
            spars[2].Value = Convert.ToInt32(igoalid);
        else
            spars[2].Value = DBNull.Value;


        dtmeasurement = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");


        return dtmeasurement;


    }

    private DataSet getMeasurements_List(string igoalid)
    {
        
            DataSet dtmeasurement = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);         
            spars[0].Value = "get_measurements_list_by_KRA_ID";
         
            spars[1] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
            if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
                spars[1].Value = Convert.ToDecimal(hdnKRA_id.Value);
            else
            spars[1].Value = DBNull.Value;

            spars[2] = new SqlParameter("@Goal_Id", SqlDbType.Int);
        if (Convert.ToString(igoalid).Trim() != "")
            spars[2].Value = Convert.ToInt32(igoalid);
        else
            spars[2].Value = DBNull.Value;


        dtmeasurement = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");


        return dtmeasurement;


    }

    private DataSet getMeasurements_list_temp_to_temmp(string igoalid)
    {

        DataSet dtmeasurement = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_measurements_list_temptotemp";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtemp_code.Text).Trim();

        spars[2] = new SqlParameter("@Goal_Id", SqlDbType.Int);
        if (Convert.ToString(igoalid).Trim() != "")
            spars[2].Value = Convert.ToInt32(igoalid);
        else
            spars[2].Value = DBNull.Value;


        dtmeasurement = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");


        return dtmeasurement;


    }

    public void get_KRA_Approvers()
    {
        DgvApprover.DataSource = null;
        DgvApprover.DataBind();

        decimal dKRAID = 0;
        if (Convert.ToString(hdnKRA_id.Value).Trim() != "")
            dKRAID = Convert.ToDecimal(hdnKRA_id.Value);

        DataSet dsApprovers = new DataSet();
        dsApprovers = spm.get_KRA_Approver_List(Convert.ToInt32(hdnPeriod_id.Value), txtemp_code.Text, "KRA_approvers", dKRAID);

        if (dsApprovers.Tables[0].Rows.Count > 0)
        {
            DgvApprover.DataSource = dsApprovers.Tables[0];
            DgvApprover.DataBind();
            hdnemplogin_type.Value = "";
            for (Int32 irow=0;irow< dsApprovers.Tables[0].Rows.Count;irow++)
            {
                //getCurrent Approver details
                if(Convert.ToString(dsApprovers.Tables[0].Rows[irow]["Status"]).Trim()=="Pending")
                {
                    hdncurrent_Appr_Name.Value = Convert.ToString(dsApprovers.Tables[0].Rows[irow]["Emp_Name"]).Trim();
                    hdncurrent_Appr_EmpEmail.Value = Convert.ToString(dsApprovers.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                    hdncurrent_Appr_Empcode.Value = Convert.ToString(dsApprovers.Tables[0].Rows[irow]["Approver_emp_code"]).Trim();
                    hdncurrent_Appr_Id.Value = Convert.ToString(dsApprovers.Tables[0].Rows[irow]["approver_id"]).Trim();
                    if (Convert.ToString(dsApprovers.Tables[0].Rows[irow]["appr_code"]).Trim() == "Final Reviewer")
                    {
                        hdnemplogin_type.Value = "Final Reviewer";
                    }
                    break;
                }
                if (Convert.ToString(dsApprovers.Tables[0].Rows[irow]["Approver_emp_code"]).Trim() ==Convert.ToString(txtEmpCode.Text).Trim() && Convert.ToString(dsApprovers.Tables[0].Rows[irow]["Status"]).Trim()=="Approved")
                {
                   // Response.Redirect("KRA_Inbox.aspx");
                }
             }

            for (Int32 irow = 0; irow < dsApprovers.Tables[0].Rows.Count; irow++)
            {
                //get Nexr Approver details
                if (Convert.ToString(dsApprovers.Tables[0].Rows[irow]["Status"]).Trim() == "")
                {
                    hdnNext_Appr_Name.Value = Convert.ToString(dsApprovers.Tables[0].Rows[irow]["Emp_Name"]).Trim();
                    hdnNext_Appr_EmpEmail.Value = Convert.ToString(dsApprovers.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                    hdnNext_Appr_Empcode.Value = Convert.ToString(dsApprovers.Tables[0].Rows[irow]["Approver_emp_code"]).Trim();
                    hdnNext_Appr_Id.Value = Convert.ToString(dsApprovers.Tables[0].Rows[irow]["approver_id"]).Trim();

                    
                    break;
                }
            }
        }
    }



    private void CombineColumnCells(GridViewRow currentRow, int colIndex, string fieldName)
    {
        TableCell currentCell = currentRow.Cells[colIndex];

        if (currentCell.Visible)
        {
            Object currentValue = dgKRA_Details.DataKeys[currentRow.RowIndex].Values[fieldName];

            for (int nextRowIndex = currentRow.RowIndex + 1; nextRowIndex < dgKRA_Details.Rows.Count; nextRowIndex++)
            {
                Object nextValue = dgKRA_Details.DataKeys[nextRowIndex].Values[fieldName];

                if (nextValue.ToString() == currentValue.ToString())
                {
                    GridViewRow nextRow = dgKRA_Details.Rows[nextRowIndex];
                    TableCell nextCell = nextRow.Cells[colIndex];
                    currentCell.RowSpan = Math.Max(1, currentCell.RowSpan) + 1;
                    nextCell.Visible = false;
                }
                else
                {
                    break;
                }
            }
        }
    }

    #endregion

    public void generate_pdf()
    {
        try
        {
            DataSet dtKRA = new DataSet();

            #region get KRA


            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Rpt_get_employee_KRA_details";

            spars[1] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
            spars[1].Value = hdnKRA_id.Value;

            dtKRA = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

            #endregion

            string strpath = Server.MapPath("~/procs/KRAprt.rdlc");
            try
            {
                LocalReport ReportViewer2 = new LocalReport();

                // ReportViewer2.ProcessingMode = ProcessingMode.Local;
                //  ReportViewer2.LocalReport.ReportPath = strpath;// Server.MapPath(strpath);
                ReportViewer2.ReportPath = strpath;// Server.MapPath(strpath);
                ReportDataSource rds = new ReportDataSource("dsKRA", dtKRA.Tables[0]);
                ReportDataSource rds_goal = new ReportDataSource("dsKRA_Goals", dtKRA.Tables[1]);
                ReportDataSource rds_Reviewee = new ReportDataSource("dsKRA_Reviewee", dtKRA.Tables[2]);
                ReportDataSource rds_Reviewer = new ReportDataSource("dsKRA_Reviewer", dtKRA.Tables[3]);
                ReportDataSource rds_Final_Reviewer = new ReportDataSource("dsKRA_FinalReviewer", dtKRA.Tables[4]);


                //ReportViewer2.LocalReport.DataSources.Clear();
                //ReportViewer2.LocalReport.DataSources.Add(rds);
                //ReportViewer2.LocalReport.DataSources.Add(rds_goal);
                //ReportViewer2.LocalReport.DataSources.Add(rds_Reviewee);
                //ReportViewer2.LocalReport.DataSources.Add(rds_Reviewer);
                //ReportViewer2.LocalReport.DataSources.Add(rds_Final_Reviewer); 

                ReportViewer2.DataSources.Clear();
                ReportViewer2.DataSources.Add(rds);
                ReportViewer2.DataSources.Add(rds_goal);
                ReportViewer2.DataSources.Add(rds_Reviewee);
                ReportViewer2.DataSources.Add(rds_Reviewer);
                ReportViewer2.DataSources.Add(rds_Final_Reviewer);

                Warning[] warnings;
                string[] streamIds;
                string contentType;
                string encoding;
                string extension;

                //Export the RDLC Report to Byte Array.
                byte[] bytes = ReportViewer2.Render("pdf", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                //Download the RDLC Report in Word, Excel, PDF and Image formats.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + txtPeriod.Text.ToString() + "_" + txtemp_code.Text.ToString() + "_27.6.2022." + extension);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();

            }
            catch (Exception ex)
            {

            }






            LocalReport report = new LocalReport();


        }
        catch (Exception ex)
        {

        }
    }

    protected void btnback_mng_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dtKRA = new DataSet();

            #region get KRA


            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Rpt_get_employee_KRA_details";

            spars[1] = new SqlParameter("@KRA_ID", SqlDbType.Decimal);
            spars[1].Value = hdnKRA_id.Value;

            dtKRA = spm.getDatasetList(spars, "SP_KRA_GETALL_DETAILS");

            #endregion
           
            string strpath = Server.MapPath("~/procs/KRAprt.rdlc");
            try
            {
                LocalReport ReportViewer2 = new LocalReport();

                // ReportViewer2.ProcessingMode = ProcessingMode.Local;
                //  ReportViewer2.LocalReport.ReportPath = strpath;// Server.MapPath(strpath);
                ReportViewer2.ReportPath = strpath;// Server.MapPath(strpath);
                ReportDataSource rds = new ReportDataSource("dsKRA", dtKRA.Tables[0]);
                 ReportDataSource rds_goal = new ReportDataSource("dsKRA_Goals", dtKRA.Tables[1]);
                 ReportDataSource rds_Reviewee = new ReportDataSource("dsKRA_Reviewee", dtKRA.Tables[2]);
                 ReportDataSource rds_Reviewer = new ReportDataSource("dsKRA_Reviewer", dtKRA.Tables[3]);
                 ReportDataSource rds_Final_Reviewer = new ReportDataSource("dsKRA_FinalReviewer", dtKRA.Tables[4]);


                //ReportViewer2.LocalReport.DataSources.Clear();
                //ReportViewer2.LocalReport.DataSources.Add(rds);
                //ReportViewer2.LocalReport.DataSources.Add(rds_goal);
                //ReportViewer2.LocalReport.DataSources.Add(rds_Reviewee);
                //ReportViewer2.LocalReport.DataSources.Add(rds_Reviewer);
                //ReportViewer2.LocalReport.DataSources.Add(rds_Final_Reviewer); 

                ReportViewer2.DataSources.Clear();
                ReportViewer2.DataSources.Add(rds);
                ReportViewer2.DataSources.Add(rds_goal);
                ReportViewer2.DataSources.Add(rds_Reviewee);
                ReportViewer2.DataSources.Add(rds_Reviewer);
                ReportViewer2.DataSources.Add(rds_Final_Reviewer);

                Warning[] warnings;
                string[] streamIds;
                string contentType;
                string encoding;
                string extension;

                //Export the RDLC Report to Byte Array.
                byte[] bytes = ReportViewer2.Render("pdf", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                //Download the RDLC Report in Word, Excel, PDF and Image formats.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + txtPeriod.Text.ToString() + "_" + txtemp_code.Text.ToString() + "_27.6.2022." + extension);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End(); 

            }
            catch (Exception ex)
            {

            }

           
           

           

            LocalReport report = new LocalReport();
           

        }
        catch(Exception ex)
        {

        }
    }

    protected void dgKRA_Details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            int totalgoalid = Convert.ToInt32(e.Row.Cells[0].Text);

            if (totalgoalid == 99)
            {
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#4F81BD");
                e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                e.Row.Cells[0].ForeColor = System.Drawing.ColorTranslator.FromHtml("#4F81BD");
                e.Row.Cells[0].Text = "";
            }

        }
    }
}