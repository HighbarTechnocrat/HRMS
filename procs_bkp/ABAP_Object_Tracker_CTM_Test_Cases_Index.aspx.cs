using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_ABAP_Object_Tracker_CTM_Test_Cases_Index : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();

    #region Creative_Default_methods

    public DataSet dsDirecttaxSectionList = new DataSet();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
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


            txtEmpCode.Text = Convert.ToString(Session["Empcode"]);

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/ABAP_Object_Tracker_Index.aspx");
            }
            else
            {
                Page.SmartNavigation = true;

                if (!Page.IsPostBack)
                {
                    GetCTMTestCasesApprovalList();
                    // hdndownloadflagcheck.Value = "";
                }

            }

        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    #endregion


    #region CTM Object Submitted Plan 

    public void GetCTMTestCasesApprovalList()
    {
        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetCTMTestCasesApprovalList";

        spars[1] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
        spars[1].Value = (Session["Empcode"]).ToString().Trim();

        dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
        {
            gvCTMTestDetails.Visible = true;
            gvCTMTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvCTMTestDetails.DataBind();

        }
        else
        {
            gvCTMTestDetails.DataSource = null;
            gvCTMTestDetails.DataBind();
            gvCTMTestDetails.Visible = false;
            lblmessage.Text = "No record found";
            Response.Redirect("~/procs/ABAP_Object_Tracker_Index.aspx");

        }

    }

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        lbl_error.Text = "";
        DivIDPopup.Visible = true;

        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnHBTDetailId.Value = Convert.ToString(gvCTMTestDetails.DataKeys[row.RowIndex].Values[0]).Trim();

        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetCTMTestCasesApprovalDetails";

        spars[1] = new SqlParameter("@DetailsId", SqlDbType.VarChar);
        spars[1].Value = hdnHBTDetailId.Value.Trim();

        dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables.Count > 0 && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
        {
            txtCTMName.Text = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMName"].ToString();
            txtDevDesc.Text = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Development_Desc"].ToString();
            txtModule.Text = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ModuleDesc"].ToString();
            txtInterface.Text = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Interface"].ToString();
            txtScope.Text = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Scope"].ToString();
            txtFCategory.Text = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FCategoryName"].ToString();
            txtPriorityOrder.Text = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["PriorityName"].ToString();
            txtPriority.Text = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Priority"].ToString();
            txtComplexity.Text = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ComplexityName"].ToString();
            txtPlannedStart.Text = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMPlannedStart"].ToString();
            txtPlannedFinish.Text = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMPlannedFinish"].ToString();
            txtRevisedStart.Text = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMRevisedStart"].ToString();
            txtRevisedFinish.Text = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMRevisedFinish"].ToString();
            txtActualStart.Text = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMActualStart"].ToString();
            txtActualFinish.Text = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["CTMActualFinish"].ToString();
            txtRemark.Text = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Remark"].ToString();
            txtHBTStatus.Text = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["StatusName"].ToString();

            lnkUATSingOffDownload.Text = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["UATSingOffFile"].ToString();

            hdnbLocationName.Value = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["Location_name"].ToString();

            gvuploadedFiles.DataSource = null;
            gvuploadedFiles.DataBind();
            gvuploadedFiles.DataSource = dsABAPObjectPlanSubmitted.Tables[1].Rows.Count > 0 ? dsABAPObjectPlanSubmitted.Tables[1] : null;
            gvuploadedFiles.DataBind();

            hdnprojectManager.Value = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["projectManager"].ToString().Trim();
            hdnprojectManagerEmail.Value = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["projectManagerEmail"].ToString().Trim();
            hdnprogramnager.Value = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["programnager"].ToString().Trim();
            hdnprogramnagerEmail.Value = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["programnagerEmail"].ToString().Trim();

        }
        this.ModalPopupExtenderIRSheet.Show();
        //hdndownloadflagcheck.Value = "EditDownload";


    }

    protected void LinkBtnBackPopup_Click(object sender, EventArgs e)
    {
        DivIDPopup.Visible = false;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton lnkIRsheetExport = (ImageButton)e.Row.FindControl("lnkIRsheetExport");
            ImageButton lnkEdit = (ImageButton)e.Row.FindControl("lnkEdit");
            string testCaseFileAttached = DataBinder.Eval(e.Row.DataItem, "CTMTestCasesFiles") as string;
            if (!string.IsNullOrEmpty(testCaseFileAttached))
            {
                //lnkEdit.Visible = true;
                lnkIRsheetExport.Visible = true;
            }
            else
            {
                //lnkEdit.Visible = false;
                lnkIRsheetExport.Visible = false;
            }
        }
    }

    public void ibdownloadbtn_Click(object sender, EventArgs e)
    {
        ImageButton button = sender as ImageButton;
        if (button != null)
        {
            string commandArgument = button.CommandArgument;
            string filePath = Server.MapPath("~/ABAPTracker/CTMTestCase/" + commandArgument);

            if (System.IO.File.Exists(filePath))
            {
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + commandArgument);
                Response.WriteFile(filePath);
                Response.End();
            }
            else
            {
                lbl_error.Text = "File Not Found.";
                return;
            }
        }
        //if (hdndownloadflagcheck.Value != "")
        //{
        //    this.ModalPopupExtenderIRSheet.Show();
        //}
    }


    protected void lnkbtnclick_ApproveHbtTestCases(object sender, EventArgs e)
    {
        int hbtDetailsId = Convert.ToInt32(hdnHBTDetailId.Value);
        var objdata = UpdateHBTTestCaseApprovalStatus("Update_CTMTestCaseApprovalStatus", hbtDetailsId, TxtApprovalRemark.Text);
        if (objdata != null && objdata.Tables.Count > 0 && objdata.Tables[0].Rows.Count > 0)
        {
            if (objdata.Tables[0].Rows[0]["ResponseMgs"].ToString() == "CTM Test cases approved successfully.")
            {
                if (objdata.Tables[1].Rows.Count > 0)
                {
                    hdnFSConsultantEmail.Value = objdata.Tables[1].Rows[0]["FSConsultantEmail"].ToString().Trim();
                    hdnFSConsultantName.Value = objdata.Tables[1].Rows[0]["FSConsultantName"].ToString().Trim();
                }
                if (objdata.Tables[2].Rows.Count > 0)
                {
                    hdnABAPperEmail.Value = objdata.Tables[2].Rows[0]["ABAPperEmail"].ToString().Trim();
                    hdnABAPperName.Value = objdata.Tables[2].Rows[0]["ABAPperName"].ToString().Trim();
                }

                if (objdata.Tables[3].Rows.Count > 0)
                {
                    hdnHBTConsultantEmail.Value = objdata.Tables[3].Rows[0]["HBTConsultantEmail"].ToString().Trim();
                    hdnHBTConsultantName.Value = objdata.Tables[3].Rows[0]["HBTConsultantName"].ToString().Trim();
                }
                if (objdata.Tables[4].Rows.Count > 0)
                {
                    hdnCTMConsultantEmail.Value = objdata.Tables[4].Rows[0]["CTMConsultantEmail"].ToString().Trim();
                    hdnCTMConsultantName.Value = objdata.Tables[4].Rows[0]["CTMConsultantName"].ToString().Trim();
                }


                if (objdata.Tables[0].Rows[0]["ResponseStatusVal"].ToString() == "17")
                {
                    #region Send Email to ABAP Planned Consultant
                    string strSubject1 = "";
                    strSubject1 = hdnbLocationName.Value.ToString().Trim() + " ABAP Object " + txtDevDesc.Text.ToString().Trim();
                    string Email_TO1 = Convert.ToString(objdata.Tables[2].Rows[0]["ABAPperEmail"]).Trim();
                    string Email_CC1 = hdnprojectManagerEmail.Value.ToString().Trim();

                    string strInvoiceURL = "";
                    strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["ABAPStatus_ABAPObjectPlan"]).Trim());
                    StringBuilder strbuild1 = new StringBuilder();
                    strbuild1.Append("<p style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'></p>");
                    strbuild1.Append("<p>" + "Dear " + Convert.ToString(objdata.Tables[2].Rows[0]["ABAPperName"]).Trim() + "</p>");
                    strbuild1.Append("<p> This is to inform you that <b>" + hdnprojectManager.Value.ToString().Trim() + "</b> has Approved the object <b> " + txtDevDesc.Text.ToString().Trim() + "</b>. Now you can upload the Soruce Code. Please click on below link for action.</p>");
                    strbuild1.Append("<table cellpadding='5' cellspacing='0' style='font-size:11pt;font-family:Arial;font-style:Regular;width:100%;'>");
                    strbuild1.Append("<tr><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Object Name</th> <th style='background-color: #C7D3D4;border: 1px solid #ccc'>Module</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Interface</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Plan Start Date</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Plan End Date</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Revised Start Date</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Revised End date</th>" +
                                         "</tr>");
                    strbuild1.Append("<tr><td style='width:38%;border: 1px solid #ccc'>" + Convert.ToString(txtDevDesc.Text).Trim() + " </td>");
                    strbuild1.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(txtModule.Text).Trim() + "</td>");
                    strbuild1.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(txtInterface.Text).Trim() + "</td>");
                    strbuild1.Append("<td style='width:8%;border: 1px solid #ccc'>" + Convert.ToString(txtPlannedStart.Text).Trim() + "</td>");
                    strbuild1.Append("<td style='width:8%;border: 1px solid #ccc'>" + Convert.ToString(txtPlannedFinish.Text).Trim() + "</td>");
                    strbuild1.Append("<td style='width:8%;border: 1px solid #ccc'>" + Convert.ToString(txtRevisedStart.Text).Trim() + "</td>");
                    strbuild1.Append("<td style='width:8%;border: 1px solid #ccc'>" + Convert.ToString(txtRevisedFinish.Text).Trim() + "</td></tr>");
                    strbuild1.Append("</table>");
                    strbuild1.Append("<p><a href=" + strInvoiceURL + ">Please click here to view ABAP Object Plan.</a></p>");
                    strbuild1.Append("<br><br>This is an auto generated email, please do not reply!");

                    spm.sendMail(Email_TO1, strSubject1, Convert.ToString(strbuild1).Trim(), "", Email_CC1);
                    #endregion




                    #region CTM Approval Send Email 
                    string strSubject = "";
                    strSubject = hdnbLocationName.Value.ToString().Trim() + " ABAP Object " + txtDevDesc.Text.ToString().Trim();
                    string Email_TO = hdnCTMConsultantEmail.Value.ToString().Trim();
                    string Email_CC = hdnprojectManagerEmail.Value.ToString().Trim() +"; " + hdnprogramnagerEmail.Value.ToString().Trim();

                    StringBuilder strbuild = new StringBuilder();
                    strbuild.Append("<p style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'></p>");
                    strbuild.Append("<p>" + "Dear " + hdnCTMConsultantName.Value.ToString().Trim() + "</p>");
                    strbuild.Append("<p> This is to inform you that <b>" + hdnprojectManager.Value.ToString().Trim() + "</b> has Approved the object <b> " + txtDevDesc.Text.ToString().Trim() + "</b>.</p>");
                    strbuild.Append("<table cellpadding='5' cellspacing='0' style='font-size:11pt;font-family:Arial;font-style:Regular;width:100%;'>");
                    strbuild.Append("<tr><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Object Name</th> <th style='background-color: #C7D3D4;border: 1px solid #ccc'>Module</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Interface</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Plan Start Date</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Plan End Date</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Revised Start Date</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Revised End date</th>" +
                                         "</tr>");
                    strbuild.Append("<tr><td style='width:38%;border: 1px solid #ccc'>" + Convert.ToString(txtDevDesc.Text).Trim() + " </td>");
                    strbuild.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(txtModule.Text).Trim() + "</td>");
                    strbuild.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(txtInterface.Text).Trim() + "</td>");
                    strbuild.Append("<td style='width:8%;border: 1px solid #ccc'>" + Convert.ToString(txtPlannedStart.Text).Trim() + "</td>");
                    strbuild.Append("<td style='width:8%;border: 1px solid #ccc'>" + Convert.ToString(txtPlannedFinish.Text).Trim() + "</td>");
                    strbuild.Append("<td style='width:8%;border: 1px solid #ccc'>" + Convert.ToString(txtRevisedStart.Text).Trim() + "</td>");
                    strbuild.Append("<td style='width:8%;border: 1px solid #ccc'>" + Convert.ToString(txtRevisedFinish.Text).Trim() + "</td></tr>");
                    strbuild.Append("</table>");
                    strbuild.Append("<br><br>This is an auto generated email, please do not reply!");

                    spm.sendMail(Email_TO, strSubject, Convert.ToString(strbuild).Trim(), "", Email_CC);
                    #endregion
                }
            }




            //var ApproverEmail = objdata.Tables[0].Rows[0]["apprEmail"].ToString();
            //var ConsultantEmail = objdata.Tables[0].Rows[0]["consEmail"].ToString();

            //#region Send Email 
            //string strSubject = "";
            //strSubject = "One HR: Uploaded CTM Test Cases Approved.";
            //string sApproverEmail_CC = "";

            //sApproverEmail_CC = Convert.ToString(objdata.Tables[0].Rows[0]["consEmail"].ToString()).Trim();
            //string strInvoiceURL = "";
            //StringBuilder strbuild = new StringBuilder();

            //strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:80%'>");
            //strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
            //strbuild.Append("<tr><td style='height:20px'></td></tr>");
            //strbuild.Append("<tr><td colspan='2'> Hello, ABAP Uploaded CTM Test Cases Approved by Project Manager.</td></tr>");

            //strbuild.Append("<tr><td style='height:40px'></td></tr>");
            //strbuild.Append("<tr><td style='height:20px'><a href='" + strInvoiceURL + "'> Please Click here to view approved ABAP Object </a></td></tr>");

            //spm.sendMail(ApproverEmail, strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);

            //#endregion

            Response.Redirect("ABAP_Object_Tracker_CTM_Test_Cases_Index.aspx");
        }

        return;
    }

    protected void lnkbtnclick_SendForCottectHbtTestCases(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtApprovalRemark.Text))
        {
            lblRemarkError.Text = "Please enter the remark while send for correction.";
        }

        int hbtDetailsId = Convert.ToInt32(hdnHBTDetailId.Value);

        var objdata = UpdateHBTTestCaseApprovalStatus("CTMTestCaseSendforCorrection", hbtDetailsId, TxtApprovalRemark.Text);
        if (objdata != null || objdata.Tables.Count > 0 || objdata.Tables[0].Rows.Count > 0)
        {
            var ApproverEmail = objdata.Tables[0].Rows[0]["apprEmail"].ToString();
            var ConsultantEmail = objdata.Tables[0].Rows[0]["consEmail"].ToString();

            if (objdata.Tables[0].Rows[0]["ResponseMgs"].ToString() == "CTM Test case send for correction.")
            {
                if (objdata.Tables[1].Rows.Count > 0)
                {
                    hdnFSConsultantEmail.Value = objdata.Tables[1].Rows[0]["FSConsultantEmail"].ToString().Trim();
                    hdnFSConsultantName.Value = objdata.Tables[1].Rows[0]["FSConsultantName"].ToString().Trim();
                }
                if (objdata.Tables[2].Rows.Count > 0)
                {
                    hdnABAPperEmail.Value = objdata.Tables[2].Rows[0]["ABAPperEmail"].ToString().Trim();
                    hdnABAPperName.Value = objdata.Tables[2].Rows[0]["ABAPperName"].ToString().Trim();
                }

                if (objdata.Tables[3].Rows.Count > 0)
                {
                    hdnHBTConsultantEmail.Value = objdata.Tables[3].Rows[0]["HBTConsultantEmail"].ToString().Trim();
                    hdnHBTConsultantName.Value = objdata.Tables[3].Rows[0]["HBTConsultantName"].ToString().Trim();
                }
                if (objdata.Tables[4].Rows.Count > 0)
                {
                    hdnCTMConsultantEmail.Value = objdata.Tables[4].Rows[0]["CTMConsultantEmail"].ToString().Trim();
                    hdnCTMConsultantName.Value = objdata.Tables[4].Rows[0]["CTMConsultantName"].ToString().Trim();
                }

                if (objdata.Tables[0].Rows[0]["ResponseStatusVal"].ToString() == "18")
                {
                    #region Send Email 
                    string strSubject = "";
                    strSubject = hdnbLocationName.Value.ToString().Trim() + " ABAP Object " + txtDevDesc.Text.ToString().Trim();
                    string Email_TO = hdnCTMConsultantEmail.Value.ToString().Trim();
                    string Email_CC = hdnprojectManagerEmail.Value.ToString().Trim();

                    string strInvoiceURL = "";
                    strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["CTMTesting_ABAPObjectPlan"]).Trim());
                    StringBuilder strbuild = new StringBuilder();
                    strbuild.Append("<p style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'></p>");
                    strbuild.Append("<p>" + "Dear " + hdnCTMConsultantName.Value.ToString().Trim() + "</p>");
                    strbuild.Append("<p> This is to inform you that <b>" + hdnprojectManager.Value.ToString().Trim() + "</b> has Send for the correction of object <b> " + txtDevDesc.Text.ToString().Trim() + "</b>. . Please take the action on this Object.</p>");
                    strbuild.Append("<table cellpadding='5' cellspacing='0' style='font-size:11pt;font-family:Arial;font-style:Regular;width:100%;'>");
                    strbuild.Append("<tr><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Object Name</th> <th style='background-color: #C7D3D4;border: 1px solid #ccc'>Module</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Interface</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Plan Start Date</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Plan End Date</th><th style='background-color: #C7D3D4;border: 1px solid #ccc'>Revised Start Date</th>" +
                                         "<th style='background-color: #C7D3D4;border: 1px solid #ccc'>Revised End date</th>" +
                                         "</tr>");
                    strbuild.Append("<tr><td style='width:38%;border: 1px solid #ccc'>" + Convert.ToString(txtDevDesc.Text).Trim() + " </td>");
                    strbuild.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(txtModule.Text).Trim() + "</td>");
                    strbuild.Append("<td style='width:15%;border: 1px solid #ccc'>" + Convert.ToString(txtInterface.Text).Trim() + "</td>");
                    strbuild.Append("<td style='width:8%;border: 1px solid #ccc'>" + Convert.ToString(txtPlannedStart.Text).Trim() + "</td>");
                    strbuild.Append("<td style='width:8%;border: 1px solid #ccc'>" + Convert.ToString(txtPlannedFinish.Text).Trim() + "</td>");
                    strbuild.Append("<td style='width:8%;border: 1px solid #ccc'>" + Convert.ToString(txtRevisedStart.Text).Trim() + "</td>");
                    strbuild.Append("<td style='width:8%;border: 1px solid #ccc'>" + Convert.ToString(txtRevisedFinish.Text).Trim() + "</td></tr>");
                    strbuild.Append("</table>");
                    strbuild.Append("<br><br>This is an auto generated email, please do not reply!");

                    spm.sendMail(Email_TO, strSubject, Convert.ToString(strbuild).Trim(), "", Email_CC);
                    #endregion
                }
            }

            //#region Send Email 
            //string strSubject = "";
            //strSubject = "One HR: Uploaded CTM Test Case Send for Correction.";
            //string sApproverEmail_CC = "";

            //sApproverEmail_CC = Convert.ToString(objdata.Tables[0].Rows[0]["consEmail"].ToString()).Trim();
            //string strInvoiceURL = "";
            //StringBuilder strbuild = new StringBuilder();

            //strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:80%'>");
            //strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
            //strbuild.Append("<tr><td style='height:20px'></td></tr>");
            //strbuild.Append("<tr><td colspan='2'> Hello, ABAP Uploaded CTM Test Case Send for Correction by Project Manager.</td></tr>");

            //strbuild.Append("<tr><td style='height:40px'></td></tr>");
            //strbuild.Append("<tr><td style='height:20px'><a href='" + strInvoiceURL + "'> Please Click here to view approved ABAP Object </a></td></tr>");

            //spm.sendMail(ApproverEmail, strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);

            //#endregion

            Response.Redirect("ABAP_Object_Tracker_CTM_Test_Cases_Index.aspx");
        }

        return;
    }

    public DataSet UpdateHBTTestCaseApprovalStatus(string qtype, int hbtDetailsId, string Remark)
    {
        DataSet objdata = new DataSet();

        try
        {
            List<SqlParameter> spars = new List<SqlParameter>
            {
                new SqlParameter("@qtype", SqlDbType.VarChar) { Value = qtype },
                new SqlParameter("@DetailsId", SqlDbType.VarChar) { Value = hbtDetailsId},
                new SqlParameter("@Remark", SqlDbType.VarChar) { Value = Remark.ToString().Trim() },
                new SqlParameter("@CreatedBy", SqlDbType.VarChar) { Value = (Session["Empcode"]).ToString().Trim() },
            };

            objdata = spm.getDatasetList(spars.ToArray(), "SP_ABAPObjectTracking");
            return objdata;
        }
        catch (Exception e)
        {
            lblSubmitError.Text = e.Message.ToString();
        }

        return objdata;
    }

    protected void DownloadUATSingOffFile(object sender, EventArgs e)
    {
        lblmessagesub.Text = "";
        LinkButton linkButton = (LinkButton)sender;
        if (linkButton != null)
        {

            string filePath = Server.MapPath("~/ABAPTracker/UATSingOff/" + linkButton.Text);

            if (System.IO.File.Exists(filePath))
            {
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + linkButton.Text);
                Response.WriteFile(filePath);
                Response.End();
            }
            else
            {
                lblmessagesub.Text = "File Not Found.";
                PnlIrSheet.Visible = true;
                DivIDPopup.Visible = true;
                return;
            }
        }
        //if (hdndownloadflagcheck.Value != "")
        //{
        //    this.ModalPopupExtenderIRSheet.Show();
        //}
    }

    #endregion
}