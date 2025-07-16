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

public partial class procs_ABAP_Object_Tracker_CTM_Test_Upload_Test_Cases : System.Web.UI.Page
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


            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
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
                    getProjectLocation();
                    //GetHBTTestStatusMaster();
                    string previousValue = Session["Previouslocation"] as string;
                    if (!string.IsNullOrEmpty(previousValue))
                    {
                        DDLProjectLocation.SelectedValue = previousValue;
                    }
                    GetHBTTestingSignOffDetailsByLocation();

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

    private void getProjectLocation()
    {
        try
        {
            var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();

            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetLocationForFunctionalConsultant";

            spars[1] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
            spars[1].Value = getCreatedBy;

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                DDLProjectLocation.DataSource = DS.Tables[0];
                DDLProjectLocation.DataTextField = "Location_name";
                DDLProjectLocation.DataValueField = "comp_code";
                DDLProjectLocation.DataBind();
                DDLProjectLocation.Items.Insert(0, new ListItem("Select Project Location", "0"));
            }
            else
            {
                DDLProjectLocation.Items.Insert(0, new ListItem("Select Project Location", "0"));
                lbl_error.Text = "The upload of templates is restricted to project managers only.";
            }
        }
        catch (Exception ex)
        {
            lbl_error.Text = ex.Message.ToString();
            return;
        }
    }

    public void GetHBTTestingSignOffDetailsByLocation()
    {

        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetCTM_TestingSignOffDetailsByLocation";

        spars[1] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
        spars[1].Value = (Session["Empcode"]).ToString().Trim();

        spars[2] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
        spars[2].Value = DDLProjectLocation.SelectedValue == "0" ? "" : DDLProjectLocation.SelectedValue;

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
        txtPRM.Text = dsABAPObjectPlanSubmitted.Tables[1].Rows.Count > 0 ? dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ProgramManager"].ToString().Trim() : "";

    }

    public void DDLProjectLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        liProjectManager.Visible = true;
        GetHBTTestingSignOffDetailsByLocation();
    }

    protected void UploadHBTTestCases_btnSave_Click(object sender, EventArgs e)
    {
        lbl_Upload_Error.Text = "";
        string filename = "";


        if (!uplHBTTestCases.HasFile)
        {
            this.ModalPopupExtenderIRSheet.Show();
            lbl_Upload_Error.Text = "Please upload the template.";
            return;
        }
        else if (uplHBTTestCases.HasFile)
        {
            filename = uplHBTTestCases.FileName;
            string HBTTestCaseFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ABAPObjectCTMTest"]).Trim() + "/");
            bool folderExists = Directory.Exists(HBTTestCaseFilePath);
            if (!folderExists)
            {
                Directory.CreateDirectory(HBTTestCaseFilePath);
            }
            String InputFile = System.IO.Path.GetExtension(uplHBTTestCases.FileName);

            string str = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
            filename = "CTMTestCase" + str + InputFile;
            uplHBTTestCases.SaveAs(Path.Combine(HBTTestCaseFilePath, filename));
            string powoUplaodedFile = HBTTestCaseFilePath + filename;
            string read = System.IO.Path.GetFullPath(powoUplaodedFile);
        }


        var HBTTestDetailsId = hdnHBTDetailId.Value;
        var objdata = UploadHBTTestCases("UploadCTMTestCases", HBTTestDetailsId, filename);
        if (objdata != null || objdata.Tables.Count > 0 || objdata.Tables[0].Rows.Count > 0)
        {
            #region Send Email 
            //string strSubject = "";
            //strSubject = "OneHR: UPload HBT Test Cases against the ABAP Object Plan";
            //string sApproverEmail_CC = "";

            ////        if (Convert.ToString(sApproverEmail_CC).Trim() == "") {
            ////            sApproverEmail_CC = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
            ////        } 
            ////          else
            ////            sApproverEmail_CC = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() + ";" + sApproverEmail_CC;

            //string strInvoiceURL = "";
            //strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["approverLink_ABAPObjectPlan"]).Trim() + "?ABAPODId=" + hdnABAPODUploadId.Value).Trim();
            //StringBuilder strbuild = new StringBuilder();

            //strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:80%'>");
            //strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
            //strbuild.Append("<tr><td style='height:20px'></td></tr>");
            //strbuild.Append("<tr><td colspan='2'> You are selected as Cosultant for this ABAP Object Plan.</td></tr>");
            //strbuild.Append("<tr><td style='height:40px'></td></tr>");
            ////strbuild.Append("<tr><td style='height:20px'><a href='" + strInvoiceURL + "'> Please Click here to view approved ABAP Object </a></td></tr>");

            //spm.sendMail(objdata.Tables[0].Rows[0]["ToEmailId"].ToString().Trim(), strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);

            #endregion
        }
        Session["Previouslocation"] = DDLProjectLocation.SelectedValue;
        Response.Redirect("~/procs/ABAP_Object_Tracker_CTM_Test_Upload_Test_Cases.aspx");

    }

    public DataSet UploadHBTTestCases(string qtype, string DetailsId, string FileName)
    {
        DataSet dsData = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[5];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = qtype;

            spars[1] = new SqlParameter("@DetailsId", SqlDbType.VarChar);
            spars[1].Value = DetailsId;

            spars[2] = new SqlParameter("@filename", SqlDbType.VarChar);
            spars[2].Value = FileName;

            spars[3] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
            spars[3].Value = (Session["Empcode"]).ToString().Trim();

            dsData = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
            return dsData;
        }
        catch (Exception e)
        {
        }

        return dsData;
    }

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnHBTDetailId.Value = Convert.ToString(gvHBTTestDetails.DataKeys[row.RowIndex].Values[0]).Trim();

        txtConsultant.Text = HttpUtility.HtmlDecode(row.Cells[1].Text);
        txtDevDesc.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
        txtModule.Text = HttpUtility.HtmlDecode(row.Cells[3].Text);
        txtInterface.Text = HttpUtility.HtmlDecode(row.Cells[4].Text);
        txtFCategory.Text = HttpUtility.HtmlDecode(row.Cells[6].Text);
        txtPriorityOrder.Text = HttpUtility.HtmlDecode(row.Cells[7].Text);
        txtPriority.Text = HttpUtility.HtmlDecode(row.Cells[8].Text);
        txtComplexity.Text = HttpUtility.HtmlDecode(row.Cells[9].Text);
        txtPlannedStart.Text = HttpUtility.HtmlDecode(row.Cells[12].Text);
        txtPlannedFinish.Text = HttpUtility.HtmlDecode(row.Cells[13].Text);
        txtRevisedStart.Text = HttpUtility.HtmlDecode(row.Cells[14].Text.Trim());
        txtRevisedFinish.Text = HttpUtility.HtmlDecode(row.Cells[15].Text);
        txtActualStart.Text = HttpUtility.HtmlDecode(row.Cells[16].Text);
        txtActualFinish.Text = HttpUtility.HtmlDecode(row.Cells[17].Text);
        txtRemark.Text = HttpUtility.HtmlDecode(row.Cells[18].Text);
        txtHBTStatus.Text = HttpUtility.HtmlDecode(row.Cells[19].Text);
        this.ModalPopupExtenderIRSheet.Show();

    }


    protected void LinkBtnBackPopup_Click(object sender, EventArgs e)
    {
        //DivIDPopup.Visible = false;
    }

    //private void GetHBTTestStatusMaster()
    //{
    //    try
    //    {
    //        var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();

    //        SqlParameter[] spars = new SqlParameter[2];
    //        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
    //        spars[0].Value = "GetHBTTestStatusMaster";

    //        DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
    //        if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
    //        {
    //            ddlStatus.DataSource = DS.Tables[0];
    //            ddlStatus.DataTextField = "StatusName";
    //            ddlStatus.DataValueField = "StatusId";
    //            ddlStatus.DataBind();
    //            ddlStatus.Items.Insert(0, new ListItem("Select Status", "0"));
    //        }
    //        else
    //        {
    //            DDLProjectLocation.Items.Insert(0, new ListItem("Select Status", "0"));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lbl_error.Text = ex.Message.ToString();
    //        return;
    //    }
    //}

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
            string filePath = Server.MapPath("~/ABAPTracker/CTMTest/" + commandArgument);

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
    #endregion
}