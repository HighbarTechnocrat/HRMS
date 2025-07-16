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

public partial class VSCB_ApprovePOWOMilestone : System.Web.UI.Page
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/vscb_index.aspx");
            }
            else
            {
                Page.SmartNavigation = true;

                if (!Page.IsPostBack)
                {
                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBMilestonefiles"]).Trim() + "/");
                    txt_remakrs.Attributes.Add("maxlength", txt_remakrs.MaxLength.ToString());
                    if (Request.QueryString.Count > 0)
                    {
                        hdnPOWOID.Value = Request.QueryString[0];                        
                        get_PWODetails_MilestonesList_Update(); 
                        
                    }
                     
                }

            }

        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    } 
      

    protected void trvl_btnSave_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }
        #region Comment by Sanjay due to PO/WO Sign copy upload not required
        /*if (Convert.ToString(hdnNext_Appr_Empcode.Value).Trim() == "")
        {
            if (!POWO_SignCopyUploadfile.HasFile)
            {
                lblmessage.Text = "Please download PO/ WO Content and Upload the sign copy PO/ WO.";
                return;
            }
        }*/

        #endregion

        if (Convert.ToString(hdnNext_Appr_Empcode.Value).Trim() == "")
        {

            if (POWO_SignCopyUploadfile.HasFile)
            {
                string filename = POWO_SignCopyUploadfile.FileName;
                string MilestoneFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBSignPOWOfiles"]).Trim() + "/");
                bool folderExists = Directory.Exists(MilestoneFilePath);
                if (!folderExists)
                {
                    Directory.CreateDirectory(MilestoneFilePath);
                }
                String InputFile = System.IO.Path.GetExtension(POWO_SignCopyUploadfile.FileName);
                filename = txtEmpCode.Text + "_" + Convert.ToString(hdnPOWOID.Value).Trim() + "_Sign_PO_WO" + InputFile;
                POWO_SignCopyUploadfile.SaveAs(Path.Combine(MilestoneFilePath, filename));
                POWOUploaded_Files(Convert.ToString(filename).Trim());
            }

            //update current approver status
            spm.Insert_UpdatePOWO_ApproverDetails("UpdatePOWO_FinalApproval", Convert.ToDouble(hdnPOWOID.Value), hdncurrent_Appr_Empcode.Value, Convert.ToInt32(hdncurrent_Appr_Id.Value), "Approved", Convert.ToString(txt_remakrs.Text).Trim(), "", "");
             
        }
        else
        {
            //insert next approver and update current approver status
            spm.Insert_UpdatePOWO_ApproverDetails("InsertPOWO_Approver", Convert.ToDouble(hdnPOWOID.Value), hdnNext_Appr_Empcode.Value, Convert.ToInt32(hdnNext_Appr_Id.Value), "Pending", Convert.ToString(txt_remakrs.Text).Trim(), Convert.ToString(hdncurrent_Appr_Empcode.Value), Convert.ToString(hdncurrent_Appr_Id.Value));

        }

        if (Convert.ToString(hdnNext_Appr_Empcode.Value).Trim() == "")
        {
            #region insert the POID for Save the Approved PO in Approved Folder
                 string sponumber = Regex.Replace(Convert.ToString(txtPOWO_Number.Text), @"[^0-9a-zA-Z\._]", "_"); 
                 DataSet dsPOWOContent_Convert = new DataSet();

                SqlParameter[] spars = new SqlParameter[3];

                spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                spars[0].Value = "insert_POWO_ID_Convert";

                spars[1] = new SqlParameter("@POWO_Number", SqlDbType.VarChar);
                spars[1].Value = Convert.ToString(txtPOWO_Number.Text).Trim();

                spars[2] = new SqlParameter("@POWOID", SqlDbType.Decimal);
                spars[2].Value = Convert.ToDouble(hdnPOWOID.Value);
                dsPOWOContent_Convert = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");

            #endregion
        }


        #region Send Email to next Approver and  if Its Final Approver then send to POWO Creator

        string strSubject =  "OneHR: Request for - PO/ WO Approval - "+ Convert.ToString(txtPOWO_Number.Text).Trim(); 
        string sApproverEmail_CC = "";

        #region get Previous approvers emails & approver list
        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.get_POWO_Approver_List(Convert.ToDouble(hdnPOWOID.Value), Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(txtProject.Text).Trim(), Convert.ToDecimal(txtBasePOWOWAmt.Text), Convert.ToInt32(hdnPOTypeId_ForApproval.Value));
        for (Int32 irow = 0; irow < dsMilestone.Tables[0].Rows.Count; irow++)
        {
            if (Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() != "" && Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() != "Pending" && Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() != Convert.ToString(hdncurrent_Appr_EmpEmail.Value).Trim())
            {
                if (Convert.ToString(sApproverEmail_CC).Trim() == "")
                    sApproverEmail_CC = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                else
                    sApproverEmail_CC = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() + ";" + sApproverEmail_CC;
            }
        }

        StringBuilder strbuild_Approvers = new StringBuilder();
        strbuild_Approvers.Append("<table cellpadding='5' cellspacing='0' style='font-size:11pt;font-family:Arial;font-style:Regular;width:100%;'>");
        strbuild_Approvers.Append("<tr><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Approver Name</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Status</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Approved On</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Approver Remarks</th></tr>");
        for (Int32 irow = 0; irow < dsMilestone.Tables[0].Rows.Count; irow++)
        {
            strbuild_Approvers.Append("<tr><td style='width:40%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["ApproverName"]).Trim() + " </td>");
            strbuild_Approvers.Append("<td style='width:10%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() + "</td>");
            strbuild_Approvers.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["approved_on"]).Trim() + "</td>");
            strbuild_Approvers.Append("<td style='width:35%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Remarks"]).Trim() + "</td></tr>");
        }
        strbuild_Approvers.Append("</table>");

        #endregion




        string strPOWOURL = "";
        strPOWOURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["approverLinkPOWO_VSCB"]).Trim() + "?poid=" + hdnPOWOID.Value).Trim();
        StringBuilder strbuild = new StringBuilder();

        strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'>");
        strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>");
        strbuild.Append("<tr><td colspan='2'> " + hdncurrent_Appr_Name.Value + " has approved an PO/ WO with the following details.</td></tr>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>");



        strbuild.Append("<tr><td>PO/ WO No :-</td><td>" + Convert.ToString(txtPOWO_Number.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>PO/ WO Type :-</td><td>" + Convert.ToString(txtPOtype.Text).Trim() + "</td></tr>");
        //strbuild.Append("<tr><td>PO/ WO Title :-</td><td>" + Convert.ToString(txtPOTitle.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>PO/ WO Date :-</td><td>" + Convert.ToString(txtFromdate.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>Vendor Name :-</td><td>" + Convert.ToString(txtVendor.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(txtProject.Text).Trim() + "</td></tr>"); 
        // strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(txtCostCenter.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>PO/ WO Amount (without Tax) :-</td><td>" + Convert.ToString(txtBasePOWOWAmt.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>PO/ WO Amount (with Tax) :-</td><td>" + Convert.ToString(txtPOWOAmt.Text).Trim() + "</td></tr>"); 

        strbuild.Append("<tr><td style='height:20px'></td></tr>");
 


        if (Convert.ToString(hdnIsFinalApprover.Value).Trim() != "yes")
        {
            strbuild.Append("<tr><td style='height:40px'></td></tr>");
            strbuild.Append("<tr><td style='height:20px'><a href='" + strPOWOURL + "'> Please Click here for your action </a></td></tr>");
        }

        strbuild.Append("</table>  <br /><br />");

        strbuild.Append(strbuild_Approvers);

        if (Convert.ToString(hdnIsFinalApprover.Value).Trim().ToLower() == "yes")
        {
            spm.sendMail_VSCB(hdnPOWOCreator_Email.Value, strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);
        }
        else
        {
            spm.sendMail_VSCB(hdnNext_Appr_EmpEmail.Value, strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);
        }


        #endregion


        if (Convert.ToString(hdnNext_Appr_Empcode.Value).Trim() == "")
        {
            //POWODownload_Word_PDFNew(Convert.ToString(hdnPOWOID.Value), Convert.ToString(txtPOWO_Number.Text));
            string spo_number = Regex.Replace(Convert.ToString(txtPOWO_Number.Text), @"[^0-9a-zA-Z\._]", "_");

            if (!File.Exists(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBApprovedPOWOfiles"]).Trim() + spo_number + ".pdf")))
                clsDownloadPOWO.POWODownload_Word_PDFNew(Convert.ToString(hdnPOWOID.Value), Convert.ToString(txtPOWO_Number.Text));
        }
        Response.Redirect("VSCB_InboxPOWO.aspx");
    }




     

   

    protected void gvuploadedFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void lnkfile_PO_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBMilestonefiles"]).Trim()), lnkfile_PO.Text);
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnRejectYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        if(Convert.ToString(txt_remakrs.Text).Trim()=="")
        {
            lblmessage.Text = "Please enter rejection remarks .";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ASD", "StopLoader();", true);
            return;
        }

        //update current approver Reject status
        spm.Insert_UpdatePOWO_ApproverDetails("UpdatePOWO_Reject", Convert.ToDouble(hdnPOWOID.Value), hdncurrent_Appr_Empcode.Value, Convert.ToInt32(hdncurrent_Appr_Id.Value), "Reject", Convert.ToString(txt_remakrs.Text).Trim(), hdncurrent_Appr_Empcode.Value, Convert.ToString(hdncurrent_Appr_Id.Value));


        #region Send Email to POWO Creator and approver if Request Reject by Approver

        string strSubject = "OneHR: - " + Convert.ToString(txtPOWO_Number.Text).Trim() +" Rejected ";
        string sApproverEmail_CC = "";

        #region get Previous approvers emails & approver list
        DataSet dsMilestone = new DataSet();
       

        dsMilestone = spm.get_POWO_Approver_List(Convert.ToDouble(hdnPOWOID.Value), Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(txtProject.Text).Trim(), Convert.ToDecimal(txtBasePOWOWAmt.Text), Convert.ToInt32(hdnPOTypeId_ForApproval.Value));
        for (Int32 irow = 0; irow < dsMilestone.Tables[0].Rows.Count; irow++)
        {
            if (Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() != "" && Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() != "Pending" && Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() != Convert.ToString(hdncurrent_Appr_EmpEmail.Value).Trim())
            {
                if (Convert.ToString(sApproverEmail_CC).Trim() == "")
                    sApproverEmail_CC = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                else
                    sApproverEmail_CC = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() + ";" + sApproverEmail_CC;
            }
        }

        StringBuilder strbuild_Approvers = new StringBuilder();
        strbuild_Approvers.Append("<table cellpadding='5' cellspacing='0' style='font-size:11pt;font-family:Arial;font-style:Regular;width:100%;'>");
        strbuild_Approvers.Append("<tr><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Approver Name</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Status</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Approved On</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Approver Remarks</th></tr>");
        for (Int32 irow = 0; irow < dsMilestone.Tables[0].Rows.Count; irow++)
        {
            strbuild_Approvers.Append("<tr><td style='width:40%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["ApproverName"]).Trim() + " </td>");
            strbuild_Approvers.Append("<td style='width:10%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() + "</td>");
            strbuild_Approvers.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["approved_on"]).Trim() + "</td>");
            strbuild_Approvers.Append("<td style='width:35%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Remarks"]).Trim() + "</td></tr>");
        }
        strbuild_Approvers.Append("</table>");

        #endregion



         
        StringBuilder strbuild = new StringBuilder();

        strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'>");
        strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>");
        strbuild.Append("<tr><td colspan='2'>This is to inform you that "+ hdncurrent_Appr_Name.Value + " has rejected a PO/ WO <b>"+Convert.ToString(txtPOWO_Number.Text)+"</b> with the following details.</td></tr>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>"); 

        strbuild.Append("<tr><td>PO/ WO No :-</td><td>" + Convert.ToString(txtPOWO_Number.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>PO/ WO Type :-</td><td>" + Convert.ToString(txtPOtype.Text).Trim() + "</td></tr>");
        //strbuild.Append("<tr><td>PO/ WO Title :-</td><td>" + Convert.ToString(txtPOTitle.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>PO/ WO Date :-</td><td>" + Convert.ToString(txtFromdate.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>Vendor Name :-</td><td>" + Convert.ToString(txtVendor.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(txtProject.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>PO/ WO Amount (without Tax) :-</td><td>" + Convert.ToString(txtBasePOWOWAmt.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>PO/ WO Amount (with Tax) :-</td><td>" + Convert.ToString(txtPOWOAmt.Text).Trim() + "</td></tr>");
        
       // strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(txtCostCenter.Text).Trim() + "</td></tr>");
        
         

        strbuild.Append("<tr><td style='height:20px'></td></tr>"); 

        strbuild.Append("</table>  <br /><br />");

        strbuild.Append(strbuild_Approvers);
         
        spm.sendMail_VSCB(hdnPOWOCreator_Email.Value, strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);

        #endregion 

        Response.Redirect("VSCB_InboxPOWO.aspx");

    }

    protected void lnkDownload_POContent_Click(object sender, EventArgs e)
    {
        DataSet dsPOWOContent = new DataSet();

        #region get POWO Content 


        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_POWO_TermsContent_FromTally";

        spars[1] = new SqlParameter("@POWO_Number", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtPOWO_Number.Text).Trim();  //"PO/042021/00001"; 

        spars[2] = new SqlParameter("@POWOID", SqlDbType.Decimal);
        spars[2].Value = Convert.ToDouble(hdnPOWOID.Value);  //"PO/042021/00001";


        dsPOWOContent = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");

        #endregion

        try
        {



            string strpath = Server.MapPath("~/procs/VSCB_Rpt_POWO_Terms.rdlc");

            LocalReport ReportViewer2 = new LocalReport();
            ReportViewer2.ReportPath = strpath;// Server.MapPath(strpath);
            ReportDataSource rds = new ReportDataSource("dspowoContent", dsPOWOContent.Tables[0]);  

            ReportViewer2.DataSources.Clear();
            ReportViewer2.DataSources.Add(rds);
          
            ReportViewer2.Refresh();

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
            Response.AppendHeader("Content-Disposition", "attachment; filename=POWOContent." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();
        }

    }

    protected void btnCorrection_Click(object sender, EventArgs e)
    {

        string confirmValue = hdnCorrectionYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        if (Convert.ToString(txt_remakrs.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Correction remarks .";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ASD", "StopLoader();", true);
            return;
        }

        //update current approver Reject status
        spm.Insert_UpdatePOWO_ApproverDetails("UpdatePOWO_Correction", Convert.ToDouble(hdnPOWOID.Value), hdncurrent_Appr_Empcode.Value, Convert.ToInt32(hdncurrent_Appr_Id.Value), "Reject", Convert.ToString(txt_remakrs.Text).Trim(), hdncurrent_Appr_Empcode.Value, Convert.ToString(hdncurrent_Appr_Id.Value));


        #region Send Email to POWO Creator and approver if Request send for Correction by Approver

        string strSubject = "OneHR: - " + Convert.ToString(txtPOWO_Number.Text).Trim() + " sent back for correction ";
        string sApproverEmail_CC = "";


        string strPOWOURL = "";
        strPOWOURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["CorrectionLinkPOWO_VSCB"]).Trim() + "?poid=" + hdnPOWOID.Value).Trim()+ "&mngexp=1";

        #region get Previous approvers emails & approver list
        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.get_POWO_Approver_List(Convert.ToDouble(hdnPOWOID.Value), Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(txtProject.Text).Trim(), Convert.ToDecimal(txtBasePOWOWAmt.Text), Convert.ToInt32(hdnPOTypeId_ForApproval.Value));
        for (Int32 irow = 0; irow < dsMilestone.Tables[0].Rows.Count; irow++)
        {
            if (Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() != "" && Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() != "Pending" && Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() != Convert.ToString(hdncurrent_Appr_EmpEmail.Value).Trim())
            {
                if (Convert.ToString(sApproverEmail_CC).Trim() == "")
                    sApproverEmail_CC = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                else
                    sApproverEmail_CC = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() + ";" + sApproverEmail_CC;
            }
        }

        StringBuilder strbuild_Approvers = new StringBuilder();
        strbuild_Approvers.Append("<table cellpadding='5' cellspacing='0' style='font-size:11pt;font-family:Arial;font-style:Regular;width:100%;'>");
        strbuild_Approvers.Append("<tr><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Approver Name</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Status</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Approved On</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Approver Remarks</th></tr>");
        for (Int32 irow = 0; irow < dsMilestone.Tables[0].Rows.Count; irow++)
        {
            strbuild_Approvers.Append("<tr><td style='width:40%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["ApproverName"]).Trim() + " </td>");
            strbuild_Approvers.Append("<td style='width:10%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Status"]).Trim() + "</td>");
            strbuild_Approvers.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["approved_on"]).Trim() + "</td>");
            strbuild_Approvers.Append("<td style='width:35%;border: 1px solid #ccc'>" + Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Remarks"]).Trim() + "</td></tr>");
        }
        strbuild_Approvers.Append("</table>");

        #endregion




        StringBuilder strbuild = new StringBuilder();

        strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'>");
        strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>");       
        strbuild.Append("<tr><td colspan='2'>"+ hdncurrent_Appr_Name.Value + " has Sent back the PO/ WO <b>" + Convert.ToString(txtPOWO_Number.Text) + "</b> for correction. Please correct the same as instructed and resend for approval.</td></tr>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>");

        strbuild.Append("<tr><td>PO/ WO No :-</td><td>" + Convert.ToString(txtPOWO_Number.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>PO/ WO Type :-</td><td>" + Convert.ToString(txtPOtype.Text).Trim() + "</td></tr>");
        // strbuild.Append("<tr><td>PO/ WO Title :-</td><td>" + Convert.ToString(txtPOTitle.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>PO/ WO Date :-</td><td>" + Convert.ToString(txtFromdate.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>Vendor Name :-</td><td>" + Convert.ToString(txtVendor.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(txtProject.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>PO/ WO Amount (without Tax) :-</td><td>" + Convert.ToString(txtBasePOWOWAmt.Text).Trim() + "</td></tr>");
        strbuild.Append("<tr><td>PO/ WO Amount (with Tax) :-</td><td>" + Convert.ToString(txtPOWOAmt.Text).Trim() + "</td></tr>");
        
        //strbuild.Append("<tr><td>Cost Center :-</td><td>" + Convert.ToString(txtCostCenter.Text).Trim() + "</td></tr>");
         
        

        strbuild.Append("<tr><td style='height:20px'></td></tr>");


        strbuild.Append("<tr><td style='height:40px'></td></tr>");
        strbuild.Append("<tr><td style='height:20px'><a href='" + strPOWOURL + "'>Please click here to open the PO/ WO </a></td></tr>");

        strbuild.Append("</table>  <br /><br />");

        strbuild.Append(strbuild_Approvers);

        spm.sendMail_VSCB(hdnPOWOCreator_Email.Value, strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);

        #endregion 

        Response.Redirect("VSCB_InboxPOWO.aspx");
    }

    #endregion

    #region PageMethods

    private void POWOUploaded_Files(string strFileName)
    {
        DataSet dtgoal = new DataSet();

        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "Upload_POWO_SignCopy";

        spars[1] = new SqlParameter("@powo_signCopy", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(strFileName).Trim();

        spars[2] = new SqlParameter("@POID", SqlDbType.BigInt);
        spars[2].Value = Convert.ToDouble(hdnPOWOID.Value);

        dtgoal = spm.getDatasetList(spars, "SP_VSCB_CreateMilestone_Details");
        
    }

    private DataSet get_POWO_Approver_List()
    {
        DataSet dtApprovers = new DataSet();

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_POWO_Approvers"; 

        spars[1] = new SqlParameter("@POWOID", SqlDbType.BigInt);
        spars[1].Value = Convert.ToDouble(hdnPOWOID.Value);

        dtApprovers = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        return dtApprovers;

    }

    public void get_PWODetails_MilestonesList_Update()
    {

        DataSet dsMilestone = new DataSet();
        dsMilestone = spm.get_POWO_Details_ForApproval(txtEmpCode.Text, Convert.ToDouble(hdnPOWOID.Value));

        if (dsMilestone.Tables[0].Rows.Count > 0)
        { 
            hdnPOWOID.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POID"]).Trim();
            hdnMstoneId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POID"]).Trim();
            txtPOWO_Number.Text= Convert.ToString(dsMilestone.Tables[0].Rows[0]["PONumber"]).Trim();
            hdnPOTypeId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTypeID"]).Trim();
            txtPOtype.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POType"]).Trim(); 
            hdnPOTypeId_ForApproval.Value= Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTypeID"]).Trim();

            txtFromdate.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PODate"]);
            txtProject.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Project_Name"]).Trim();
            txtPOStatus.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PyamentStatus"]).Trim();
            txtCurrency.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["CurName"]).Trim();
             
            txtPOTitle.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTitle"]).Trim();
            txtPOTitle.ToolTip = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POTitle"]).Trim();
            txtVendor.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Name"]).Trim();
            txtGSTIN_No.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["GSTIN_NO"]).Trim();
            txtPOWOAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POWOAmt"]).Trim();
            txtBasePOWOWAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POWO_T_BaseAmt"]).Trim();            
            txtCostCenter.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["costCenter"]).Trim();

            txtPOWOTitle.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["HPOTypeName"]).Trim();            
            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Security_Deposit_Amt"]).Trim() != "")
            {
                txtSecurity_DepositAmt.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_Security_Deposit_Amt"]).Trim();
                if (Convert.ToDecimal(dsMilestone.Tables[0].Rows[0]["PO_Security_Deposit_Amt"]) > 0)
                {
                    divScurity_Diposit.Visible = true;
                }
            }

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_WO_Text"]).Trim() != "")
            {
                lblPOWO_Content.InnerHtml = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_WO_Text"]).Trim();
                liPOWOContent_1.Visible = true;
                liPOWOContent_2.Visible = true;
                liPOWOContent_3.Visible = true;
            }
            hdnCondintionId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["cond_id"]).Trim();
            hdnRangeId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Range_type_id"]).Trim();
            hdnAppr_StatusId.Value= Convert.ToString(dsMilestone.Tables[0].Rows[0]["appr_StatusId"]).Trim(); 

            hdnCompCode.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Comp_Code"]).Trim();
            hdnVendorId.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["VendorID"]).Trim();
            hdnPrj_Dept_Id.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["Prj_Dept_Id"]).Trim();
            lnkfile_PO.Text = Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_File"]).Trim();


            hdnPOWOCreator_Name.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POWOCreator_Name"]).Trim();
            hdnPOWOCreator_Email.Value = Convert.ToString(dsMilestone.Tables[0].Rows[0]["POWO_Creator_Emailid"]).Trim();

            if (Convert.ToString(dsMilestone.Tables[0].Rows[0]["PO_File"]).Trim() != "")
            {
                lnkfile_PO.Visible = true;
                spnUploadedPOWOFile.Visible = true;

                liPOUploadedFile_1.Visible = true;
                liPOUploadedFile_2.Visible = true;
                liPOUploadedFile_3.Visible = true;

            }
           // spnPOWOStatus.Visible = true;
           // txtPOStatus.Visible = true;
            spnAmountWithTax.Visible = true;
            txtPOWOAmt.Visible = true; 

            dgTravelRequest.DataSource = null;
            dgTravelRequest.DataBind();
            if (dsMilestone.Tables[1].Rows.Count > 0)
            {
                dgTravelRequest.DataSource = dsMilestone.Tables[1];
                dgTravelRequest.DataBind();
            }
             
            getMilestoneUploadedFiles();

            #region get Approver as per amount

            //get Approver List
            DataSet dsApprover = null;
            DgvApprover.DataSource = null;
            DgvApprover.DataBind();

            dsApprover = spm.get_POWO_Approver_List(Convert.ToDouble(hdnPOWOID.Value), Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(txtProject.Text).Trim(), Convert.ToDecimal(txtBasePOWOWAmt.Text), Convert.ToInt32(hdnPOTypeId_ForApproval.Value));

            if (dsApprover != null)
            { 
                if (dsApprover.Tables[0].Rows.Count > 0)
                {
                    DgvApprover.DataSource = dsApprover.Tables[0];
                    DgvApprover.DataBind();
                    hdnIsFinalApprover.Value = "no";
                    for (Int32 irow = 0; irow < dsApprover.Tables[0].Rows.Count; irow++)
                    {
                        //getCurrent Approver details
                        if (Convert.ToString(dsApprover.Tables[0].Rows[irow]["Status"]).Trim() == "Pending")
                        {
                            hdncurrent_Appr_Name.Value = Convert.ToString(dsApprover.Tables[0].Rows[irow]["ApproverName"]).Trim();
                            hdncurrent_Appr_EmpEmail.Value = Convert.ToString(dsApprover.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                            hdncurrent_Appr_Empcode.Value = Convert.ToString(dsApprover.Tables[0].Rows[irow]["APPR_Emp_Code"]).Trim();
                            hdncurrent_Appr_Id.Value = Convert.ToString(dsApprover.Tables[0].Rows[irow]["APPR_ID"]).Trim();                            
                            break;
                        }
                        if (Convert.ToString(dsApprover.Tables[0].Rows[irow]["APPR_Emp_Code"]).Trim() == Convert.ToString(txtEmpCode.Text).Trim() && Convert.ToString(dsApprover.Tables[0].Rows[irow]["Status"]).Trim() == "Approved")
                        {
                            Response.Redirect("VSCB_InboxPOWO.aspx");
                        }
                    }

                    for (Int32 irow = 0; irow < dsApprover.Tables[0].Rows.Count; irow++)
                    {
                        //get Nexr Approver details
                        if (Convert.ToString(dsApprover.Tables[0].Rows[irow]["Status"]).Trim() == "")
                        {
                            hdnNext_Appr_Name.Value = Convert.ToString(dsApprover.Tables[0].Rows[irow]["ApproverName"]).Trim();
                            hdnNext_Appr_EmpEmail.Value = Convert.ToString(dsApprover.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                            hdnNext_Appr_Empcode.Value = Convert.ToString(dsApprover.Tables[0].Rows[irow]["APPR_Emp_Code"]).Trim();
                            hdnNext_Appr_Id.Value = Convert.ToString(dsApprover.Tables[0].Rows[irow]["APPR_ID"]).Trim();
                            
                            break;
                        }
                    }

                } 
            }

            if (Convert.ToString(hdnNext_Appr_Empcode.Value).Trim() == "")
            {

                #region comment by Sanjay File Upload not require
                //spnUploadSignCopy.Visible = true;
                //POWO_SignCopyUploadfile.Visible = true;
                /*  if (Convert.ToString(lblPOWO_Content.InnerText).Trim() != "")
                 {
                     lnkDownload_POContent.Visible = true;
                     liPOWOContent_Download_1.Visible = true;
                     liPOWOContent_Download_2.Visible = true;
                     liPOWOContent_Download_3.Visible = true;

                 }*/
                #endregion
                hdnIsFinalApprover.Value = "yes";
            }
            #endregion


        }
        else
        {
            Response.Redirect("~/procs/vscb_index.aspx");
        }

    }

    public string ReplaceInvalidChars(string filename)
    {
        Regex illegalInFileName = new Regex(@"[#%&{}\!/<'>@+*?$|=]");
        string myString = illegalInFileName.Replace(filename, "_");
        //return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        return myString;
    }

    public void getMilestoneUploadedFiles()
    {

        DataSet dsFiles = spm.getVSCB_UploadedFiles("get_VSCB_UploadedFiels", "Milestone", Convert.ToDouble(hdnPOWOID.Value), Convert.ToString(hdnSrno.Value).Trim(), "");

        gvuploadedFiles.DataSource = null;
        gvuploadedFiles.DataBind();
        if (dsFiles.Tables[0].Rows.Count > 0)
        {
            gvuploadedFiles.DataSource = dsFiles;
            gvuploadedFiles.DataBind();
            spnUploadedSupportingFiles.Visible = true;

            liSupportingDoc_1.Visible = true;
            liSupportingDoc_2.Visible = true;
            liSupportingDoc_3.Visible = true;

        }
    }


    #endregion




    protected void btnApprove_Click(object sender, EventArgs e)
    {

        DataSet dsPOWOContent = new DataSet();

        #region get POWO Content 

            SqlParameter[] spars = new SqlParameter[5];
            string HFChkFlagDraftCopy = "0";
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Insert_POWO_TempDraftCopy";

            spars[1] = new SqlParameter("@POWOID", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDouble(hdnPOWOID.Value);

        
            HFChkFlagDraftCopy = "1";
            spars[2] = new SqlParameter("@CurrencyName", SqlDbType.VarChar);
            spars[2].Value = "";
            spars[3] = new SqlParameter("@POHTMLContain", SqlDbType.VarChar);
            spars[3].Value = "";
        
            spars[4] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[4].Value = txtEmpCode.Text;

        dsPOWOContent = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");

        string url = "VSCB_DraftCopy.aspx?ID=" + HFChkFlagDraftCopy + "";
        string s = "window.open('" + url + "', 'popup_window', 'width=800,height=790,scrollbars=no, menubar=no,resizable=no,scrollbars=yes,toolbar=no,directories=no,location=no');";
        //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        ///window.open(url, 'popUpWindow', 'height=' + height + ', width=' + width + ', resizable=yes, scrollbars=yes, toolbar=yes')
        //ClientScript.RegisterStartupScript(this.GetType(), "popupWindow(url,'','window',200,100)","",true); 
        ClientScript.RegisterStartupScript(this.GetType(), "popupwindow(" + url + ")", s, true);

        #endregion 

        #region Draft Copy code not required
        /*
        DataSet dsPOWOContent = new DataSet();

        #region get POWO Content 

        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_ViewDraftCopy_FromTally";
        spars[1] = new SqlParameter("@POWO_Number", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtPOWO_Number.Text).Trim();  //"PO/042021/00001"; 
        spars[2] = new SqlParameter("@POWOID", SqlDbType.Decimal);
        spars[2].Value = Convert.ToDouble(hdnPOWOID.Value);  //"PO/042021/00001";

        dsPOWOContent = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");

        #endregion

        try
        {
            string strpath = Server.MapPath("~/procs/VSCB_Rpt_ViewDraftCopy.rdlc");
            string PowoNumber = Convert.ToString(txtPOWO_Number.Text).Trim().Replace("/", "-");

            LocalReport ReportViewer2 = new LocalReport();
            ReportViewer2.ReportPath = strpath;// Server.MapPath(strpath);
            ReportDataSource rds = new ReportDataSource("dspowoContent", dsPOWOContent.Tables[0]);
            ReportDataSource rds_2 = new ReportDataSource("dsowoDetails", dsPOWOContent.Tables[1]);
            ReportDataSource rds_3 = new ReportDataSource("dsMilestone", dsPOWOContent.Tables[2]);
            ReportDataSource rds_4 = new ReportDataSource("dsPOWOAmountinWords", dsPOWOContent.Tables[3]);

            ReportDataSource rds_5 = new ReportDataSource("dsPOPreparedBy", dsPOWOContent.Tables[4]);
            ReportDataSource rds_6 = new ReportDataSource("dsPOCheckedBy", dsPOWOContent.Tables[5]);
            ReportDataSource rds_7 = new ReportDataSource("dsPOApprovedBY_HOD", dsPOWOContent.Tables[6]);
            ReportDataSource rds_8 = new ReportDataSource("dsPOApprovedBY_COO", dsPOWOContent.Tables[7]);
            ReportDataSource rds_9 = new ReportDataSource("dsPOApprovedBY_CEO", dsPOWOContent.Tables[8]);

            //if (Convert.ToString(dsPOWOContent.Tables[1].Rows[0]["Approver_Count"]).Trim() == "2")
            //{
            // ReportDataSource rds_6 = new ReportDataSource("dsPOCheckedBy", dsPOWOContent.Tables[5]);
            //  ReportDataSource rds_7 = new ReportDataSource("dsPOApprovedBY_HOD_COO", dsPOWOContent.Tables[6]);

            //  ReportViewer2.DataSources.Add(rds_7);
            //}


            ReportViewer2.DataSources.Clear();
            ReportViewer2.DataSources.Add(rds);
            ReportViewer2.DataSources.Add(rds_2);
            ReportViewer2.DataSources.Add(rds_3);
            ReportViewer2.DataSources.Add(rds_4);

            ReportViewer2.DataSources.Add(rds_5);
            ReportViewer2.DataSources.Add(rds_6);
            ReportViewer2.DataSources.Add(rds_7);
            ReportViewer2.DataSources.Add(rds_8);
            ReportViewer2.DataSources.Add(rds_9);
            ReportViewer2.Refresh();

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
            Response.AppendHeader("Content-Disposition", "attachment; filename=DraftCopy_" + PowoNumber + "." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();
        }

        */
        #endregion
    }


}