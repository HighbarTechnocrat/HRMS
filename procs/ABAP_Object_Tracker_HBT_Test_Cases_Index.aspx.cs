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

public partial class ABAP_Object_Tracker_HBT_Test_Cases_Index : System.Web.UI.Page
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
                    GetHBTTestCasesApprovalList();

                }

            }

        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    #endregion


    #region ABAP Object Submitted Plan 

    public void GetHBTTestCasesApprovalList()
    {
        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetHBTTestCasesApprovalList";

        spars[1] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
        spars[1].Value = (Session["Empcode"]).ToString().Trim();

        dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
        {
            gvHBTTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvHBTTestDetails.DataBind();
        }
        else
        {
            gvHBTTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvHBTTestDetails.DataBind();
        }

    }


    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnHBTDetailId.Value = Convert.ToString(gvHBTTestDetails.DataKeys[row.RowIndex].Values[0]).Trim();


        txtConsultant.Text = row.Cells[1].Text;
        txtDevDesc.Text = row.Cells[2].Text;
        txtModule.Text = row.Cells[2].Text;
        txtInterface.Text = row.Cells[4].Text;
        txtFCategory.Text = row.Cells[5].Text;
        txtPriorityOrder.Text = row.Cells[6].Text;
        txtPriority.Text = row.Cells[7].Text;
        txtComplexity.Text = row.Cells[8].Text;
        txtPlannedStart.Text = row.Cells[9].Text;
        txtPlannedFinish.Text = row.Cells[10].Text;
        txtRevisedStart.Text = row.Cells[11].Text.Trim();
        txtRevisedFinish.Text = row.Cells[12].Text;
        txtActualStart.Text = row.Cells[13].Text;
        txtActualFinish.Text = row.Cells[14].Text;
        txtRemark.Text = row.Cells[15].Text;
        txtHBTStatus.Text = row.Cells[17].Text;
        //txtTestCaseFileAttached.Text = row.Cells[18].Text;
        lnkIRsheetExport.CommandArgument = row.Cells[18].Text;
        this.ModalPopupExtenderIRSheet.Show();

    }


    protected void LinkBtnBackPopup_Click(object sender, EventArgs e)
    {
        //DivIDPopup.Visible = false;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton lnkIRsheetExport = (ImageButton)e.Row.FindControl("lnkIRsheetExport");
            ImageButton lnkEdit = (ImageButton)e.Row.FindControl("lnkEdit");
            string testCaseFileAttached = DataBinder.Eval(e.Row.DataItem, "TestCaseFileAttached") as string;
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
            string filePath = Server.MapPath("~/ABAPTracker/HBTTest/" + commandArgument);

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
                Response.Write("File not found.");
            }
        }





    }


    protected void lnkbtnclick_ApproveHbtTestCases(object sender, EventArgs e)
    {
        int hbtDetailsId = Convert.ToInt32(hdnHBTDetailId.Value);
        var objdata = UpdateHBTTestCaseApprovalStatus("UpdateHBTTestCaseApprovalStatus", hbtDetailsId, TxtApprovalRemark.Text);
        if(objdata != null && objdata.Tables.Count > 0 && objdata.Tables[0].Rows.Count > 0 )
        {
            var ApproverEmail = objdata.Tables[0].Rows[0]["apprEmail"].ToString();
            var ConsultantEmail = objdata.Tables[0].Rows[0]["consEmail"].ToString();

            #region Send Email 
            string strSubject = "";
            strSubject = "One HR: Uploaded HBT Test Cases Approved.";
            string sApproverEmail_CC = "";

            sApproverEmail_CC = Convert.ToString(objdata.Tables[0].Rows[0]["consEmail"].ToString()).Trim();
            string strInvoiceURL = "";
            StringBuilder strbuild = new StringBuilder();

            strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:80%'>");
            strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td colspan='2'> Hello, ABAP Uploaded HBT Test Cases Approved by Project Manager.</td></tr>");

            strbuild.Append("<tr><td style='height:40px'></td></tr>");
            strbuild.Append("<tr><td style='height:20px'><a href='" + strInvoiceURL + "'> Please Click here to view approved ABAP Object </a></td></tr>");

            //spm.sendMail(ApproverEmail, strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);

            #endregion

            Response.Redirect("ABAP_Object_Tracker_HBT_Test_Cases_Index.aspx");
        }

        return;
    }

    protected void lnkbtnclick_SendForCottectHbtTestCases(object sender, EventArgs e)
    {
        if(string.IsNullOrEmpty(TxtApprovalRemark.Text))
        {
            lblRemarkError.Text = "Please enter the remark while send for correction.";
        }

        int hbtDetailsId = Convert.ToInt32(hdnHBTDetailId.Value);

        var objdata = UpdateHBTTestCaseApprovalStatus("HBTTestCaseSendforCorrection", hbtDetailsId, TxtApprovalRemark.Text);
        if (objdata != null || objdata.Tables.Count > 0 || objdata.Tables[0].Rows.Count > 0)
        {
            var ApproverEmail = objdata.Tables[0].Rows[0]["apprEmail"].ToString();
            var ConsultantEmail = objdata.Tables[0].Rows[0]["consEmail"].ToString();

            #region Send Email 
            string strSubject = "";
            strSubject = "One HR: Uploaded HBT Test Case Send for Correction.";
            string sApproverEmail_CC = "";

            sApproverEmail_CC = Convert.ToString(objdata.Tables[0].Rows[0]["consEmail"].ToString()).Trim();
            string strInvoiceURL = "";
            StringBuilder strbuild = new StringBuilder();

            strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:80%'>");
            strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td colspan='2'> Hello, ABAP Uploaded HBT Test Case Send for Correction by Project Manager.</td></tr>");

            strbuild.Append("<tr><td style='height:40px'></td></tr>");
            strbuild.Append("<tr><td style='height:20px'><a href='" + strInvoiceURL + "'> Please Click here to view approved ABAP Object </a></td></tr>");

            //spm.sendMail(ApproverEmail, strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);

            #endregion

            
            Response.Redirect("ABAP_Object_Tracker_ApprovedList.aspx");
       }

        return ;
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

    #endregion
}