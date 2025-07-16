using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ABAP_Object_Tracker_Submit_Plan : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();

    #region Creative_Default_methods

    public string userid;
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
                    FilePath.Value = Convert.ToString(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBInvoicefiles"]).Trim() + "/"));
                    getProjectLocation();

                    DDLProjectLocation.Enabled = false;
                    if (Request.QueryString.Count > 0)
                    {
                        CheckLoggedInUserRole();
                        hdnABAPODUploadId.Value = Convert.ToString(Request.QueryString["ABAPODId"]).Trim();
                        getFunctionalConsultant();
                        getABAPConsultantMaster();
                        get_ABAP_Object_Submitted_Plan_Details();
                        CheckAllSecondColumnValues(sender, e);

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


    #region ABAP Object Submitted Plan 
    //private void getProjectLocation()
    //{
    //    try
    //    {
    //        SqlParameter[] spars = new SqlParameter[2];
    //        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
    //        spars[0].Value = "GetDropDownprojectLocation";

    //        DataSet DS = spm.getDatasetList(spars, "SP_TASK_M_DETAILS");
    //        DDLProjectLocation.DataSource = DS.Tables[0];
    //        DDLProjectLocation.DataTextField = "Location_name";
    //        DDLProjectLocation.DataValueField = "comp_code";
    //        DDLProjectLocation.DataBind();
    //        DDLProjectLocation.Items.Insert(0, new ListItem("Select Project Location", "0"));
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
    private void getProjectLocation()
    {
        try
        {
            var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();

            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getLocationMaster";

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

        }
        catch (Exception ex)
        {

        }
    }

    public void get_ABAP_Object_Submitted_Plan_Details()
    {
        try
        {
            DataSet dsABAPObjectPlanSubmitted = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getABAPObjectTrackerPlanDetails"; //"getABAPObjectTrackerPlanDetailsForSubmit";

            spars[1] = new SqlParameter("@ABAPODUploadId", SqlDbType.VarChar);
            spars[1].Value = Convert.ToInt32(hdnABAPODUploadId.Value);

            spars[2] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
            spars[2].Value = (Session["Empcode"]).ToString().Trim();

            dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
            if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
            {
                gvDetailPlan.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvDetailPlan.DataBind();

                gvRGSDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvRGSDetails.DataBind();
                gvRGSDetails.Visible = false;
                divrgsconsultant.Visible = false;
                updatergs.Visible = false;

                gvFSDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvFSDetails.DataBind();
                gvFSDetails.Visible = false;
                divfsconsultant.Visible = false;
                lnk_addconsultantfs.Visible = false;

                gvABAPDevDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvABAPDevDetails.DataBind();
                gvABAPDevDetails.Visible = false;
                divAABAPDevconsultant.Visible = false;
                lnk_addconsultantabapdev.Visible = false;

                gvHBTTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvHBTTestDetails.DataBind();
                gvHBTTestDetails.Visible = false;
                divhbtconsultant.Visible = false;
                lnk_addconsultanthbttest.Visible = false;

                gvCTMTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvCTMTestDetails.DataBind();
                gvCTMTestDetails.Visible = false;
                divCTMName.Visible = false;
                divCTMEmail.Visible = false;
                lnk_addconsultantctmtest.Visible = false;

                gvUATSignOffDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvUATSignOffDetails.DataBind();
                gvUATSignOffDetails.Visible = false;

                gvGoLiveDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
                gvGoLiveDetails.DataBind();
                gvGoLiveDetails.Visible = false;

                DDLProjectLocation.SelectedValue = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ProjectLocation"].ToString().Trim();
                hdndowloadfile.Value = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FilePath"].ToString().Trim();

                if (dsABAPObjectPlanSubmitted.Tables[1].Rows.Count > 0)
                {
                    txtPRM.Text = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ProjectManager"].ToString().Trim();
                    hdnProjectManagerName.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ProjectManager"].ToString().Trim();
                    hdnDeliveryHeadMail.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["DeliveryHeadEMail"].ToString().Trim();
                }
                if (dsABAPObjectPlanSubmitted.Tables[2].Rows.Count > 0)
                {
                    hdnProgramManagerName.Value = dsABAPObjectPlanSubmitted.Tables[2].Rows[0]["ApproverName"].ToString().Trim();
                    hdnProgramManagerMail.Value = dsABAPObjectPlanSubmitted.Tables[2].Rows[0]["ProgramManagerEmail"].ToString().Trim();
                }

                DgvApprover.DataSource = dsABAPObjectPlanSubmitted.Tables[2];
                DgvApprover.DataBind();

            }
            else
            {
                lblmessage.Text = "";
            }
        }

        catch (Exception e)
        {

        }
    }


    private void getFunctionalConsultant()
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getConsultantMaster";

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
            DDLRGSFunctionalConsultant.DataSource = DS.Tables[0];
            DDLRGSFunctionalConsultant.DataTextField = "Emp_Name";
            DDLRGSFunctionalConsultant.DataValueField = "Emp_Code";
            DDLRGSFunctionalConsultant.DataBind();
            DDLRGSFunctionalConsultant.Items.Insert(0, new ListItem("Select RGS Functional Consultant", "0"));

            DDLFSFunctionalConsultant.DataSource = DS.Tables[0];
            DDLFSFunctionalConsultant.DataTextField = "Emp_Name";
            DDLFSFunctionalConsultant.DataValueField = "Emp_Code";
            DDLFSFunctionalConsultant.DataBind();
            DDLFSFunctionalConsultant.Items.Insert(0, new ListItem("Select FS Functional Consultant", "0"));

            //DDLCTMFunctionalConsultant.DataSource = DS.Tables[0];
            //DDLCTMFunctionalConsultant.DataTextField = "Emp_Name";
            //DDLCTMFunctionalConsultant.DataValueField = "Emp_Code";
            //DDLCTMFunctionalConsultant.DataBind();
            //DDLCTMFunctionalConsultant.Items.Insert(0, new ListItem("Select CTM Functional Consultant", "0"));

            DDLPlanConsultantTesting.DataSource = DS.Tables[0];
            DDLPlanConsultantTesting.DataTextField = "Emp_Name";
            DDLPlanConsultantTesting.DataValueField = "Emp_Code";
            DDLPlanConsultantTesting.DataBind();
            DDLPlanConsultantTesting.Items.Insert(0, new ListItem("Select Plan Consultant (For Testing)", "0"));

            //DDLUATConsultant.DataSource = DS.Tables[0];
            //DDLUATConsultant.DataTextField = "Emp_Name";
            //DDLUATConsultant.DataValueField = "Emp_Code";
            //DDLUATConsultant.DataBind();
            //DDLUATConsultant.Items.Insert(0, new ListItem("Select Project Location", "0"));
        }
        catch (Exception ex)
        {

        }
    }

    private void getABAPConsultantMaster()
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getABAPConsultantMaster";

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
            DDLPlanABAPConsultant.DataSource = DS.Tables[0];
            DDLPlanABAPConsultant.DataTextField = "Emp_Name";
            DDLPlanABAPConsultant.DataValueField = "Emp_Code";
            DDLPlanABAPConsultant.DataBind();
            DDLPlanABAPConsultant.Items.Insert(0, new ListItem("Select ABAP Consultant", "0"));
        }
        catch (Exception ex)
        {

        }
    }

    protected void RGS_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("RGSchkSelect");
            string empName = DataBinder.Eval(e.Row.DataItem, "RGSConsultant").ToString();

            if (!string.IsNullOrEmpty(empName))
            {
                chkSelect.Enabled = false;
            }
            else
            {
                chkSelect.Enabled = true;
            }
        }
    }
    protected void FS_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("FSchkSelect");
            string empName = DataBinder.Eval(e.Row.DataItem, "FSConsultant").ToString();

            if (!string.IsNullOrEmpty(empName))
            {
                chkSelect.Enabled = false;
            }
            else
            {
                chkSelect.Enabled = true;
            }
        }
    }
    protected void ABAPDev_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("ABAPDevchkSelect");
            string empName = DataBinder.Eval(e.Row.DataItem, "ABAPDevConsultant").ToString();

            if (!string.IsNullOrEmpty(empName))
            {
                chkSelect.Enabled = false;
            }
            else
            {
                chkSelect.Enabled = true;
            }
        }
    }
    protected void HBT_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("HBTchkSelect");
            string empName = DataBinder.Eval(e.Row.DataItem, "HBTConsultant").ToString();

            if (!string.IsNullOrEmpty(empName))
            {
                chkSelect.Enabled = false;
            }
            else
            {
                chkSelect.Enabled = true;
            }
        }
    }
    protected void CTM_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("CTMchkSelect");
            string empName = DataBinder.Eval(e.Row.DataItem, "CTMName").ToString();

            if (!string.IsNullOrEmpty(empName))
            {
                chkSelect.Enabled = false;
            }
            else
            {
                chkSelect.Enabled = true;
            }
        }
    }

    protected void CheckAllSecondColumnValues(object sender, EventArgs e)
    {
        bool allRGSAvailable = true;
        bool allFSAvailable = true;
        bool allABAPDevAvailable = true;
        bool allHBTTestAvailable = true;
        bool allCTMAvailable = true;
        //bool allCTMAvailablecons = true;

        foreach (GridViewRow row in gvRGSDetails.Rows)
        {
            string secondColumnValue = row.Cells[1].Text.Trim();
            if (secondColumnValue == "&nbsp;" || string.IsNullOrEmpty(secondColumnValue))
            {
                allRGSAvailable = false;
                break;
            }
        }

        foreach (GridViewRow row in gvFSDetails.Rows)
        {
            string secondColumnValue = row.Cells[1].Text.Trim();
            if (secondColumnValue == "&nbsp;" || string.IsNullOrEmpty(secondColumnValue))
            {
                allFSAvailable = false;
                break;
            }
        }


        foreach (GridViewRow row in gvABAPDevDetails.Rows)
        {
            string secondColumnValue = row.Cells[1].Text.Trim();
            if (secondColumnValue == "&nbsp;" || string.IsNullOrEmpty(secondColumnValue))
            {
                allABAPDevAvailable = false;
                break;
            }
        }

        foreach (GridViewRow row in gvHBTTestDetails.Rows)
        {
            string secondColumnValue = row.Cells[1].Text.Trim();
            if (secondColumnValue == "&nbsp;" || string.IsNullOrEmpty(secondColumnValue))
            {
                allHBTTestAvailable = false;
                break;
            }
        }

        foreach (GridViewRow row in gvCTMTestDetails.Rows)
        {
            string secondColumnValue = row.Cells[1].Text.Trim();
            if (secondColumnValue == "&nbsp;" || string.IsNullOrEmpty(secondColumnValue))
            {
                allCTMAvailable = false;
                break;
            }
        }

        //foreach (GridViewRow row in gvCTMTestDetails.Rows)
        //{
        //    string secondColumnValue = row.Cells[2].Text.Trim();
        //    if (secondColumnValue == "&nbsp;" || string.IsNullOrEmpty(secondColumnValue))
        //    {
        //        allCTMAvailablecons = false;
        //        break;
        //    }
        //}

        if (allRGSAvailable)
        {
            DDLRGSFunctionalConsultant.Enabled = false;
            updatergs.Enabled = false;
        }
        else
        {
            DDLRGSFunctionalConsultant.Enabled = true;
            updatergs.Enabled = true;
        }

        if (allFSAvailable)
        {
            DDLFSFunctionalConsultant.Enabled = false;
            lnk_addconsultantfs.Enabled = false;
        }
        else
        {
            DDLFSFunctionalConsultant.Enabled = true;
            lnk_addconsultantfs.Enabled = true;
        }

        if (allABAPDevAvailable)
        {
            DDLPlanABAPConsultant.Enabled = false;
            lnk_addconsultantabapdev.Enabled = false;
        }
        else
        {
            DDLPlanABAPConsultant.Enabled = true;
            lnk_addconsultantabapdev.Enabled = true;
        }


        if (allHBTTestAvailable)
        {
            DDLPlanConsultantTesting.Enabled = false;
            lnk_addconsultanthbttest.Enabled = false;
        }
        else
        {
            DDLPlanConsultantTesting.Enabled = true;
            lnk_addconsultanthbttest.Enabled = true;
        }

        if (allCTMAvailable)
        {
            txt_CTMName.Enabled = false;
            txt_CTMEmail.Enabled = false;
            lnk_addconsultantctmtest.Enabled = false;
        }
        else
        {
            txt_CTMName.Enabled = true;
            txt_CTMEmail.Enabled = true;
            lnk_addconsultantctmtest.Enabled = true;
        }
        //if (allCTMAvailablecons)
        //{
        //    DDLCTMFunctionalConsultant.Enabled = false;
        //    lnk_addconsultantctmtest.Enabled = false;
        //}
        //else
        //{
        //    txt_CTMName.Enabled = true;
        //    lnk_addconsultantctmtest.Enabled = true;
        //}
    }

    protected void btnABAPPlanSubmit_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }
        foreach (GridViewRow row in gvRGSDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string secondColumnValue = row.Cells[1].Text.Trim();
                if (secondColumnValue == "&nbsp;" || string.IsNullOrEmpty(secondColumnValue))
                {
                    lblmessage.Text = "RGS Consultant value cannot be empty.";
                    lblmessage.Visible = true;
                    return;
                }
            }
        }

        foreach (GridViewRow row in gvFSDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string secondColumnValue = row.Cells[1].Text.Trim();
                if (secondColumnValue == "&nbsp;" || string.IsNullOrEmpty(secondColumnValue))
                {
                    lblmessage.Text = "FS Consultant value cannot be empty.";
                    lblmessage.Visible = true;
                    return;
                }
            }
        }

        foreach (GridViewRow row in gvABAPDevDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string secondColumnValue = row.Cells[1].Text.Trim();
                if (secondColumnValue == "&nbsp;" || string.IsNullOrEmpty(secondColumnValue))
                {
                    lblmessage.Text = "ABAP Dev Consultant value cannot be empty.";
                    lblmessage.Visible = true;
                    return;
                }
            }
        }

        foreach (GridViewRow row in gvHBTTestDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string secondColumnValue = row.Cells[1].Text.Trim();
                if (secondColumnValue == "&nbsp;" || string.IsNullOrEmpty(secondColumnValue))
                {
                    lblmessage.Text = "HBT Consultant value cannot be empty.";
                    lblmessage.Visible = true;
                    return;
                }
            }
        }

        //foreach (GridViewRow row in gvCTMTestDetails.Rows)
        //{
        //    if (row.RowType == DataControlRowType.DataRow)
        //    {
        //        string secondColumnValue = row.Cells[1].Text.Trim();
        //        if (secondColumnValue == "&nbsp;" || string.IsNullOrEmpty(secondColumnValue))
        //        {
        //            lblmessage.Text = "Client Person value cannot be empty.";
        //            lblmessage.Visible = true;
        //            return;
        //        }
        //    }
        //}

        foreach (GridViewRow row in gvUATSignOffDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string secondColumnValue = row.Cells[1].Text.Trim();
                if (secondColumnValue == "&nbsp;" || string.IsNullOrEmpty(secondColumnValue))
                {
                    lblmessage.Text = "Consultant value cannot be empty.";
                    lblmessage.Visible = true;
                    return;
                }
            }
        }

        foreach (GridViewRow row in gvGoLiveDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string secondColumnValue = row.Cells[1].Text.Trim();
                if (secondColumnValue == "&nbsp;" || string.IsNullOrEmpty(secondColumnValue))
                {
                    lblmessage.Text = "Consultant value cannot be empty.";
                    lblmessage.Visible = true;
                    return;
                }
            }
        }


        //Submit Plan 
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "ABAPObjectPlanSubmit";

        spars[1] = new SqlParameter("@ABAPODUploadId", SqlDbType.VarChar);
        spars[1].Value = hdnABAPODUploadId.Value;

        DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
        {
            if (DS.Tables[0].Rows[0]["ResponseMsg"].ToString() == "ABAP object tracker plan submitted.")
            {
                #region Send Email 
                string strSubject = "";
                strSubject = DDLProjectLocation.SelectedItem.Text.ToString() + " ABAP Object Development Plan Submitted for Approval";

                string sApproverEmail_CC = "";
                sApproverEmail_CC = hdnDeliveryHeadMail.Value.ToString().Trim();
                string strInvoiceURL = "";
                strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["approverLink_ABAPObjectPlan"]).Trim() + "?ABAPODId=" + hdnABAPODUploadId.Value).Trim();
                StringBuilder strbuild = new StringBuilder();

                strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'>");
                strbuild.Append("<tr><td>Dear " + hdnProgramManagerName.Value + " </td></tr>");
                strbuild.Append("<tr><td style='height:20px'></td></tr>");
                strbuild.Append("<tr><td colspan='2'> The ABAP Object development plan for the project <b>" + DDLProjectLocation.SelectedItem.Text.ToString() + "</b> has been submitted by the " + hdnProjectManagerName.Value.ToString().Trim() + " and is now awaiting your review and approval.</td></tr>");
                strbuild.Append("<tr><td></td></tr>");
                strbuild.Append("</table>");

                strbuild.Append("<br/><p><a href=" + strInvoiceURL + ">Please click here to view ABAP Object Plan.</a></p>");


                strbuild.Append("<br><br>This is an auto generated email, please do not reply!");

                spm.sendMail(hdnProgramManagerMail.Value, strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);

                #endregion

            }
        }



        Response.Redirect("~/procs/ABAP_Object_Tracker_Index.aspx");


    }

    protected void addconsultant_rgs_btnSave_Click(object sender, EventArgs e)
    {
        bool isChecked = false;
        lbl_RGSConsultant_Error.Text = "";

        if (Convert.ToString(DDLRGSFunctionalConsultant.SelectedValue).Trim() == "0" || Convert.ToString(DDLRGSFunctionalConsultant.SelectedValue).Trim() == "")
        {
            lbl_RGSConsultant_Error.Text = "Please select RGS Consultant.";
            return;
        }
        foreach (GridViewRow row in gvRGSDetails.Rows)
        {
            CheckBox chkBox = (CheckBox)row.FindControl("RGSchkSelect");
            if (chkBox != null && chkBox.Checked)
            {
                isChecked = true;
                break;
            }
        }

        if (!isChecked)
        {
            lbl_RGSConsultant_Error.Text = "Select at least one record of RGS using the checkbox before submitting.";
            lbl_RGSConsultant_Error.Visible = true;
            return;
        }
        else
        {
            foreach (GridViewRow row in gvRGSDetails.Rows)
            {
                CheckBox chkBox = (CheckBox)row.FindControl("RGSchkSelect");
                if (chkBox != null && chkBox.Checked)
                {
                    string RGSDetailsId = gvRGSDetails.DataKeys[row.RowIndex].Value.ToString();
                    var FunctionalConsultant = Convert.ToString(DDLRGSFunctionalConsultant.SelectedValue).ToString();
                    UpdateConsultantDB("AddConsultantForRGS", RGSDetailsId, FunctionalConsultant, "");


                }
            }
            Response.Redirect("~/procs/ABAP_Object_Tracker_Submit_Plan.aspx?ABAPODId=" + hdnABAPODUploadId.Value);
        }

    }

    protected void addconsultant_fs_btnSave_Click(object sender, EventArgs e)
    {
        bool isChecked = false;
        lbl_fs_Error.Text = "";

        if (Convert.ToString(DDLFSFunctionalConsultant.SelectedValue).Trim() == "0" || Convert.ToString(DDLFSFunctionalConsultant.SelectedValue).Trim() == "")
        {
            lbl_fs_Error.Text = "Please select FS Consultant.";
            return;
        }
        foreach (GridViewRow row in gvFSDetails.Rows)
        {
            CheckBox chkBox = (CheckBox)row.FindControl("FSchkSelect");
            if (chkBox != null && chkBox.Checked)
            {
                isChecked = true;
                break;
            }
        }

        if (!isChecked)
        {
            lbl_fs_Error.Text = "Select at least one record of FS using the checkbox before submitting.";
            return;
        }
        else
        {
            foreach (GridViewRow row in gvFSDetails.Rows)
            {
                CheckBox chkBox = (CheckBox)row.FindControl("FSchkSelect");
                if (chkBox != null && chkBox.Checked)
                {
                    string FSDetailsId = gvFSDetails.DataKeys[row.RowIndex].Value.ToString();
                    var FunctionalConsultant = Convert.ToString(DDLFSFunctionalConsultant.SelectedValue).ToString();
                    var objdata = UpdateConsultantDB("AddConsultantForFS", FSDetailsId, FunctionalConsultant, "");
                    if (objdata != null || objdata.Tables.Count > 0 || objdata.Tables[0].Rows.Count > 0)
                    {

                    }
                }
            }

            Response.Redirect("~/procs/ABAP_Object_Tracker_Submit_Plan.aspx?ABAPODId=" + hdnABAPODUploadId.Value);
        }
    }

    protected void addconsultant_abapdev_btnSave_Click(object sender, EventArgs e)
    {
        bool isChecked = false;
        lbl_abapdev_Error.Text = "";

        if (Convert.ToString(DDLPlanABAPConsultant.SelectedValue).Trim() == "0" || Convert.ToString(DDLPlanABAPConsultant.SelectedValue).Trim() == "")
        {
            lbl_abapdev_Error.Text = "Please select ABAP Dev Consultant.";
            return;
        }
        foreach (GridViewRow row in gvABAPDevDetails.Rows)
        {
            CheckBox chkBox = (CheckBox)row.FindControl("ABAPDevchkSelect");
            if (chkBox != null && chkBox.Checked)
            {
                isChecked = true;
                break;
            }
        }

        if (!isChecked)
        {
            lbl_abapdev_Error.Text = "Select at least one record of ABAP Dev using the checkbox before submitting.";
            lbl_abapdev_Error.Visible = true;
            return;
        }
        else
        {
            foreach (GridViewRow row in gvABAPDevDetails.Rows)
            {
                CheckBox chkBox = (CheckBox)row.FindControl("ABAPDevchkSelect");
                if (chkBox != null && chkBox.Checked)
                {
                    string ABAPDevDetailsId = gvABAPDevDetails.DataKeys[row.RowIndex].Value.ToString();
                    var FunctionalConsultant = Convert.ToString(DDLPlanABAPConsultant.SelectedValue).ToString();
                    var objdata = UpdateConsultantDB("AddConsultantForABAPDev", ABAPDevDetailsId, FunctionalConsultant, "");
                    if (objdata != null || objdata.Tables.Count > 0 || objdata.Tables[0].Rows.Count > 0)
                    {
                    }
                }
            }

            Response.Redirect("~/procs/ABAP_Object_Tracker_Submit_Plan.aspx?ABAPODId=" + hdnABAPODUploadId.Value);
        }
    }

    protected void addconsultant_hbttest_btnSave_Click(object sender, EventArgs e)
    {
        bool isChecked = false;
        lbl_hbttest_Error.Text = "";

        if (Convert.ToString(DDLPlanConsultantTesting.SelectedValue).Trim() == "0" || Convert.ToString(DDLPlanConsultantTesting.SelectedValue).Trim() == "")
        {
            lbl_hbttest_Error.Text = "Please Select HBT Testing Consultant.";
            return;
        }
        foreach (GridViewRow row in gvHBTTestDetails.Rows)
        {
            CheckBox chkBox = (CheckBox)row.FindControl("HBTchkSelect");
            if (chkBox != null && chkBox.Checked)
            {
                isChecked = true;
                break;
            }
        }

        if (!isChecked)
        {
            lbl_hbttest_Error.Text = "Select at least one record of HBT Test using the checkbox before submitting.";
            lbl_hbttest_Error.Visible = true;
            return;
        }
        else
        {
            foreach (GridViewRow row in gvHBTTestDetails.Rows)
            {
                CheckBox chkBox = (CheckBox)row.FindControl("HBTchkSelect");
                if (chkBox != null && chkBox.Checked)
                {
                    string HBTDetailsId = gvHBTTestDetails.DataKeys[row.RowIndex].Value.ToString();
                    var FunctionalConsultant = Convert.ToString(DDLPlanConsultantTesting.SelectedValue).ToString();
                    var objdata = UpdateConsultantDB("AddConsultantForHBTTest", HBTDetailsId, FunctionalConsultant, "");
                    if (objdata != null || objdata.Tables.Count > 0 || objdata.Tables[0].Rows.Count > 0)
                    {
                    }
                }
            }

            Response.Redirect("~/procs/ABAP_Object_Tracker_Submit_Plan.aspx?ABAPODId=" + hdnABAPODUploadId.Value);
        }
    }

    protected void addconsultant_ctmtest_btnSave_Click(object sender, EventArgs e)
    {
        bool isChecked = false;
        lbl_ctmtest_Error.Text = "";

        if (Convert.ToString(txt_CTMName.Text).Trim() == "")
        {
            lbl_ctmtest_Error.Text = "Please Enter Customer Name For Testing.";
            return;
        }
        //if (Convert.ToString(txt_CTMEmail.Text).Trim() == "")
        //{
        //    lbl_ctmtest_Error.Text = "Please Enter Customer Email Id For Testing.";
        //    return;
        //}

        //if (Convert.ToString(DDLCTMFunctionalConsultant.SelectedValue).Trim() == "0" || Convert.ToString(DDLCTMFunctionalConsultant.SelectedValue).Trim() == "")
        //{
        //    lbl_hbttest_Error.Text = "Please Select CTM Testing Consultant.";
        //    return;
        //}
        foreach (GridViewRow row in gvCTMTestDetails.Rows)
        {
            CheckBox chkBox = (CheckBox)row.FindControl("CTMchkSelect");
            if (chkBox != null && chkBox.Checked)
            {
                isChecked = true;
                break;
            }
        }

        if (!isChecked)
        {
            lbl_ctmtest_Error.Text = "Select at least one record of CTM Test using the checkbox before submitting.";
            lbl_ctmtest_Error.Visible = true;
            return;
        }
        else
        {
            foreach (GridViewRow row in gvCTMTestDetails.Rows)
            {
                CheckBox chkBox = (CheckBox)row.FindControl("CTMchkSelect");
                if (chkBox != null && chkBox.Checked)
                {
                    string CTMDetailsId = gvCTMTestDetails.DataKeys[row.RowIndex].Value.ToString();
                    var objCTMName = txt_CTMName.Text.ToString();
                    var objCTMEmail = txt_CTMEmail.Text.ToString();
                    var objdata = UpdateConsultantDB("AddConsultantForCTMTest", CTMDetailsId, objCTMName, objCTMEmail);
                    if (objdata != null || objdata.Tables.Count > 0 || objdata.Tables[0].Rows.Count > 0)
                    {
                    }
                }
            }

            Response.Redirect("~/procs/ABAP_Object_Tracker_Submit_Plan.aspx?ABAPODId=" + hdnABAPODUploadId.Value);
        }
    }

    public DataSet UpdateConsultantDB(string qtype, string DetailsId, string FConsultantId, string CTMEmail)
    {
        DataSet dsData = new DataSet();
        try
        {
            List<SqlParameter> spars = new List<SqlParameter>
            {
                new SqlParameter("@qtype", SqlDbType.VarChar) { Value = qtype },
                new SqlParameter("@DetailsId", SqlDbType.VarChar) { Value = DetailsId },
                new SqlParameter("@FConsultantId", SqlDbType.VarChar) { Value = FConsultantId },
        };

            if (qtype == "AddConsultantForCTMTest")
            {
                spars.Add(new SqlParameter("@CTMEmail", SqlDbType.VarChar) { Value = CTMEmail });
            }

            dsData = spm.getDatasetList(spars.ToArray(), "SP_ABAPObjectTracking");
            return dsData;
        }
        catch (Exception e)
        {
            lbl_RGSConsultant_Error.Text = e.Message.ToString();
            lbl_RGSConsultant_Error.Visible = true;
        }

        return dsData;
    }
    public void DownloadFile(object sender, EventArgs e)
    {
        string filePath = Server.MapPath("~/ABAPTracker/" + hdndowloadfile.Value);

        using (var readStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        using (var workbook = new XLWorkbook(readStream))
        {
            IXLWorksheet worksheet = workbook.Worksheet(1);
            var usedRange = worksheet.RangeUsed();
            if (usedRange != null)
            {
                var rowsToRemoveBorders = usedRange.RowsUsed().Skip(2);
                foreach (var row in rowsToRemoveBorders)
                {
                    row.Style.Border.TopBorder = XLBorderStyleValues.None;
                    row.Style.Border.BottomBorder = XLBorderStyleValues.None;
                    row.Style.Border.LeftBorder = XLBorderStyleValues.None;
                    row.Style.Border.RightBorder = XLBorderStyleValues.None;
                }
            }


            worksheet.Protect("password123");
            int lastRow = worksheet.LastRowUsed().RowNumber();
            int lastColumn = worksheet.LastColumnUsed().ColumnNumber();
            worksheet.Range(1, 1, lastRow, lastColumn).Style.Protection.Locked = true;
            worksheet.Range(lastRow + 1, 1, 250, lastColumn).Style.Protection.Locked = false;
            using (var memoryStream = new MemoryStream())
            {
                workbook.SaveAs(memoryStream);
                memoryStream.Position = 0;

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + hdndowloadfile.Value);

                Response.BinaryWrite(memoryStream.ToArray());
                Response.Flush();
                Response.End();
            }

            //if (System.IO.File.Exists(filePath))
            //{
            //    Response.Clear();
            //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //    Response.AddHeader("Content-Disposition", "attachment;filename=" + hdndowloadfile.Value);
            //    Response.WriteFile(filePath);
            //    Response.End();
            //}
            //else
            //{
            //    Response.Write("File not found.");
            //}

        }
    }

    protected void btnViewABAPObjectDetailPlan_Click(object sender, EventArgs e)
    {
        Session["ddlProjectLoc"] = DDLProjectLocation.SelectedValue;
        Response.Redirect("ABAP_Object_Tracker_View_Detail_Plan.aspx?ABAPODId=" + hdnABAPODUploadId.Value);
    }

    protected void btnRGS_Details_Click(object sender, EventArgs e)
    {
        gvDetailPlan.Visible = true;
        if (gvRGSDetails.Visible)
        {
            gvRGSDetails.Visible = false;
            divrgsconsultant.Visible = false;
            updatergs.Visible = false;
            btnRGS_Details.Text = "+";
        }
        else
        {
            btnRGS_Details.Text = "-";
            gvRGSDetails.Visible = true;
            divrgsconsultant.Visible = true;
            updatergs.Visible = true;
        }
    }

    protected void btnFS_Details_Click(object sender, EventArgs e)
    {
        gvDetailPlan.Visible = true;
        if (gvFSDetails.Visible)
        {
            gvFSDetails.Visible = false;
            divfsconsultant.Visible = false;
            lnk_addconsultantfs.Visible = false;
            btnFS_Details.Text = "+";
        }
        else
        {
            gvFSDetails.Visible = true;
            divfsconsultant.Visible = true;
            lnk_addconsultantfs.Visible = true;
            btnFS_Details.Text = "-";

        }
    }

    protected void btnABAPDev_Details_Click(object sender, EventArgs e)
    {
        gvDetailPlan.Visible = true;
        if (gvABAPDevDetails.Visible)
        {
            gvABAPDevDetails.Visible = false;
            divAABAPDevconsultant.Visible = false;
            lnk_addconsultantabapdev.Visible = false;
            btnABAPDev_Details.Text = "+";
        }
        else
        {
            gvABAPDevDetails.Visible = true;
            divAABAPDevconsultant.Visible = true;
            lnk_addconsultantabapdev.Visible = true;
            btnABAPDev_Details.Text = "-";

        }
    }

    protected void btnHBT_Details_Click(object sender, EventArgs e)
    {
        gvDetailPlan.Visible = true;
        if (gvHBTTestDetails.Visible)
        {
            gvHBTTestDetails.Visible = false;
            divhbtconsultant.Visible = false;
            lnk_addconsultanthbttest.Visible = false;
            btnHBT_Details.Text = "+";
        }
        else
        {
            gvHBTTestDetails.Visible = true;
            divhbtconsultant.Visible = true;
            lnk_addconsultanthbttest.Visible = true;
            btnHBT_Details.Text = "-";
        }
    }

    protected void btnCTM_Details_Click(object sender, EventArgs e)
    {
        gvDetailPlan.Visible = true;
        if (gvCTMTestDetails.Visible)
        {
            lictmname.Visible = false;
            lictmmail.Visible = false;
            liupdatectmdetails.Visible = false;
            gvCTMTestDetails.Visible = false;
            divCTMName.Visible = false;
            divCTMEmail.Visible = false;
            lnk_addconsultantctmtest.Visible = false;
            btnCTM_Details.Text = "+";
        }
        else
        {
            lictmname.Visible = true;
            lictmmail.Visible = true;
            liupdatectmdetails.Visible = true;
            gvCTMTestDetails.Visible = true;
            divCTMName.Visible = true;
            divCTMEmail.Visible = true;
            lnk_addconsultantctmtest.Visible = true;
            btnCTM_Details.Text = "-";

        }
    }

    protected void btnUATSignOff_Details_Click(object sender, EventArgs e)
    {
        gvDetailPlan.Visible = true;
        if (gvUATSignOffDetails.Visible)
        {
            gvUATSignOffDetails.Visible = false;
            btnUATSignOff_Details.Text = "+";
        }
        else
        {
            gvUATSignOffDetails.Visible = true;
            btnUATSignOff_Details.Text = "-";
        }
    }

    protected void btnGoLive_Details_Click(object sender, EventArgs e)
    {
        gvDetailPlan.Visible = true;
        if (gvGoLiveDetails.Visible)
        {
            gvGoLiveDetails.Visible = false;
            btnGoLive_Details.Text = "+";
        }
        else
        {
            gvGoLiveDetails.Visible = true;
            btnGoLive_Details.Text = "-";
        }
    }

    private void CheckLoggedInUserRole()
    {
        var empcode = txtEmpCode.Text.ToString().Trim();

        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "CheckLoggedInUserRole";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = empcode;

        DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        return;
    }

    protected void btnAllDetails_Click(object sender, EventArgs e)
    {


        bool allVisible = lirgsdetails.Visible &&
                       lifsdetails.Visible &&
                       liabapdev.Visible &&
                       lihbttest.Visible &&
                       lictmtest.Visible &&
                       liuatsignoff.Visible &&
                       ligolive.Visible;
        if (allVisible)
        {
            btnAllDetails.Text = "CLICK TO SEE STAGE-WISE DETAILS";
            lirgsdetails.Visible = false;
            lifsdetails.Visible = false;
            liabapdev.Visible = false;
            lihbttest.Visible = false;
            lictmtest.Visible = false;
            liuatsignoff.Visible = false;
            ligolive.Visible = false;

            lictmname.Visible = false;
            lictmmail.Visible = false;
            liupdatectmdetails.Visible = false;
        }
        else
        {
            btnAllDetails.Text = "CLICK TO SEE STAGE-WISE DETAILS";
            lirgsdetails.Visible = true;
            lifsdetails.Visible = true;
            liabapdev.Visible = true;
            lihbttest.Visible = true;
            lictmtest.Visible = true;
            liuatsignoff.Visible = true;
            ligolive.Visible = true;

            lictmname.Visible = true;
            lictmmail.Visible = true;
            liupdatectmdetails.Visible = true;
        }

        return;
    }
    #endregion
}