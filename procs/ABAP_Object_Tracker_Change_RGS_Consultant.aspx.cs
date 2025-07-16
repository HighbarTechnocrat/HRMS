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

public partial class ABAP_Object_Tracker_Change_RGS_Consultant : System.Web.UI.Page
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
                    getProjectLocation();
                    getFunctionalConsultant();
                    DDLABAPDevDesc.Items.Insert(0, new ListItem("Select ABAP Development Desc", "0"));
                    get_ABAP_Object_Submitted_Plan_FSDetails();
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
            spars[0].Value = "getUploadedLocationMaster";

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
                lbl_error.Text = "Changes to the RGS/FS/HBT/ABAP Consultant have been restricted to project managers only.";
            }
        }
        catch (Exception ex)
        {
            lbl_error.Text = ex.Message.ToString();
            return;
        }
    }

    public void get_ABAP_Object_Submitted_Plan_FSDetails()
    {

        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetRGSFSHBTDetailsByProjectLocation";

        spars[1] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
        spars[1].Value = (Session["Empcode"]).ToString().Trim();

        spars[2] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
        spars[2].Value = DDLProjectLocation.SelectedValue == "0" ? "" : DDLProjectLocation.SelectedValue;

        spars[3] = new SqlParameter("@ABAPDevDesc", SqlDbType.VarChar);
        spars[3].Value = DDLABAPDevDesc.SelectedValue == "0" ? "" : DDLABAPDevDesc.SelectedValue;

        dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
        {
            gvRGSDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvRGSDetails.DataBind();

            //gvFSDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            //gvFSDetails.DataBind();

            //gvHBTTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            //gvHBTTestDetails.DataBind();

            //gvCTMTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            //gvCTMTestDetails.DataBind();
            trvl_btnSave.Visible = true;
            litxtPRM.Visible = true;
        }
        else
        {
            gvRGSDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvRGSDetails.DataBind();

            //gvFSDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            //gvFSDetails.DataBind();     

            //gvHBTTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            //gvHBTTestDetails.DataBind();

            //gvCTMTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            //gvCTMTestDetails.DataBind();

            trvl_btnSave.Visible = false;
            litxtPRM.Visible = false;

        }
        txtPRM.Text = dsABAPObjectPlanSubmitted.Tables[1].Rows.Count > 0 ? dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ProjectMangaer"].ToString().Trim() : "";

    }

    private void getFunctionalConsultant()
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getConsultantMaster";

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");

            DDLFSFunctionalConsultant.DataSource = DS.Tables[0];
            DDLFSFunctionalConsultant.DataTextField = "Emp_Name";
            DDLFSFunctionalConsultant.DataValueField = "Emp_Code";
            DDLFSFunctionalConsultant.DataBind();
            DDLFSFunctionalConsultant.Items.Insert(0, new ListItem("Select FS Functional Consultant", "0"));


        }
        catch (Exception ex)
        {

        }
    }

    public void DDLProjectLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lbl_error.Text = "";

            var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();

            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getABAPDevelopmentDescList";

            spars[1] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
            spars[1].Value = getCreatedBy;

            spars[2] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
            spars[2].Value = DDLProjectLocation.SelectedValue;

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                DDLABAPDevDesc.DataSource = DS.Tables[0];
                DDLABAPDevDesc.DataTextField = "Development_Desc";
                DDLABAPDevDesc.DataValueField = "ABAPODId";
                DDLABAPDevDesc.DataBind();
                DDLABAPDevDesc.Items.Insert(0, new ListItem("Select ABAP Development Desc", "0"));

                gvRGSDetails.DataSource = DS.Tables[1];
                gvRGSDetails.DataBind();

                //gvFSDetails.DataSource = DS.Tables[1];
                //gvFSDetails.DataBind();

                //gvHBTTestDetails.DataSource = DS.Tables[1];
                //gvHBTTestDetails.DataBind();

                //gvCTMTestDetails.DataSource = DS.Tables[1];
                //gvCTMTestDetails.DataBind();

                trvl_btnSave.Visible = true;
                litxtPRM.Visible = true;
                txtPRM.Text = DS.Tables[2].Rows.Count > 0 ? DS.Tables[2].Rows[0]["ProjectMangaer"].ToString().Trim() : "";

            }
            else
            {
                gvRGSDetails.DataSource = DS.Tables[1];
                gvRGSDetails.DataBind();

                //gvFSDetails.DataSource = DS.Tables[1];
                //gvFSDetails.DataBind();

                //gvHBTTestDetails.DataSource = DS.Tables[1];
                //gvHBTTestDetails.DataBind();

                //gvCTMTestDetails.DataSource = DS.Tables[1];
                //gvCTMTestDetails.DataBind();


                DDLABAPDevDesc.Items.Insert(0, new ListItem("Select ABAP Development Desc", "0"));
                litxtPRM.Visible = false;

                //lbl_error.Text = "The upload of templates is restricted to project managers only.";
            }

        }
        catch (Exception ex)
        {
            lbl_error.Text = ex.Message.ToString();
            return;
        }
    }

    public void DDLABAPDevDesc_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_ABAP_Object_Submitted_Plan_FSDetails();
    }

    protected void updatefs_btnSave_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }
        bool isChecked = false;
        string chkboxval = "";
        lbl_FSConsultant_Error.Text = "";
        if (Convert.ToString(DDLProjectLocation.SelectedValue).Trim() == "0" || Convert.ToString(DDLProjectLocation.SelectedValue).Trim() == "")
        {
            lbl_FSConsultant_Error.Text = "Please select Project Location.";
            return;
        }

        if (Convert.ToString(DDLFSFunctionalConsultant.SelectedValue).Trim() == "0" || Convert.ToString(DDLFSFunctionalConsultant.SelectedValue).Trim() == "")
        {
            lbl_FSConsultant_Error.Text = "Please select FS Consultant.";
            return;
        }

        if ( string.IsNullOrEmpty(txtRemarks.Text))
        {
            lblRemark.Text = "Please enter the remark.";
            return;
        }

        foreach (GridViewRow row in gvRGSDetails.Rows)
        {
            string RGSDetailsId = gvRGSDetails.DataKeys[row.RowIndex].Value.ToString();
            var FunctionalConsultant = Convert.ToString(DDLFSFunctionalConsultant.SelectedValue).ToString();

            var objdata = UpdateConsultantDB("ChangeRGSConsultant", RGSDetailsId, FunctionalConsultant, "RGS", txtRemarks.Text);
            if (objdata != null || objdata.Tables.Count > 0 || objdata.Tables[0].Rows.Count > 0)
            {

            }
        }

        //foreach (GridViewRow row in gvFSDetails.Rows)
        //{
        //    string FSDetailsId = gvFSDetails.DataKeys[row.RowIndex].Value.ToString();
        //    var FunctionalConsultant = Convert.ToString(DDLFSFunctionalConsultant.SelectedValue).ToString();
        //    var objdata = UpdateConsultantDB("ChangeRGSFSHBTCTMConsultant", FSDetailsId, FunctionalConsultant, "FS", txtRemarks.Text);
        //    if (objdata != null || objdata.Tables.Count > 0 || objdata.Tables[0].Rows.Count > 0)
        //    {

        //    }
        //}

        //foreach (GridViewRow row in gvFSDetails.Rows)
        //{
        //    string HBTTestDetailsId = gvFSDetails.DataKeys[row.RowIndex].Value.ToString();
        //    var FunctionalConsultant = Convert.ToString(DDLFSFunctionalConsultant.SelectedValue).ToString();
        //    var objdata = UpdateConsultantDB("ChangeRGSFSHBTCTMConsultant", HBTTestDetailsId, FunctionalConsultant, "HBT", txtRemarks.Text);
        //    if (objdata != null || objdata.Tables.Count > 0 || objdata.Tables[0].Rows.Count > 0)
        //    {

        //    }
        //}

        //foreach (GridViewRow row in gvCTMTestDetails.Rows)
        //{
        //    string CTMTestDetailsId = gvCTMTestDetails.DataKeys[row.RowIndex].Value.ToString();
        //    var FunctionalConsultant = Convert.ToString(DDLFSFunctionalConsultant.SelectedValue).ToString();
        //    var objdata = UpdateConsultantDB("ChangeRGSFSHBTCTMConsultant", CTMTestDetailsId, FunctionalConsultant, "CTM", txtRemarks.Text);
        //    if (objdata != null || objdata.Tables.Count > 0 || objdata.Tables[0].Rows.Count > 0)
        //    {

        //    }
        //}


        #region Send Email 
        string strSubject = "";
        strSubject = "OneHR: Changed FS Consultant against the ABAP Object Plan";
        string sApproverEmail_CC = "";

        //        if (Convert.ToString(sApproverEmail_CC).Trim() == "")
        //      {      sApproverEmail_CC = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();   }
        //        else
        //      {      sApproverEmail_CC = Convert.ToString(dsMilestone.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim() + ";" + sApproverEmail_CC;    }

        string strInvoiceURL = "";
        strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["approverLink_ABAPObjectPlan"]).Trim() + "?ABAPODId=" + hdnABAPODUploadId.Value).Trim();
        StringBuilder strbuild = new StringBuilder();

        strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:80%'>");
        strbuild.Append("<tr><td>Dear Sir/Madam </td></tr>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>");
        strbuild.Append("<tr><td colspan='2'> You are selected as Cosultant for this ABAP Object Plan.</td></tr>");
        strbuild.Append("<tr><td style='height:40px'></td></tr>");
        //spm.sendMail(objdata.Tables[0].Rows[0]["ToEmailId"].ToString().Trim(), strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);
        #endregion

        Response.Redirect("~/procs/ABAP_Object_Tracker_Index.aspx");

    }

    public DataSet UpdateConsultantDB(string qtype, string DetailsId, string FConsultantId, string objStage, string Remark)
    {
        DataSet dsData = new DataSet();
        try
        {
            List<SqlParameter> spars = new List<SqlParameter>
            {
                new SqlParameter("@qtype", SqlDbType.VarChar) { Value = qtype },
                new SqlParameter("@DetailsId", SqlDbType.VarChar) { Value = DetailsId },
                new SqlParameter("@FConsultantId", SqlDbType.VarChar) { Value = FConsultantId },
                new SqlParameter("@CreatedBy", SqlDbType.VarChar) { Value = Session["Empcode"].ToString().Trim() },
                new SqlParameter("@objStage", SqlDbType.VarChar) { Value = objStage },
                new SqlParameter("@Remark", SqlDbType.VarChar) { Value = Remark },
            };

            dsData = spm.getDatasetList(spars.ToArray(), "SP_ABAPObjectTracking");
            return dsData;
        }
        catch (Exception e)
        {
            lbl_FSConsultant_Error.Text = e.Message.ToString();
            lbl_FSConsultant_Error.Visible = true;
        }

        return dsData;
    }

    #endregion
}