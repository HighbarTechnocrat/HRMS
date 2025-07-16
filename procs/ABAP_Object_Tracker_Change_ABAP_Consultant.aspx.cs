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

public partial class ABAP_Object_Tracker_Change_ABAP_Consultant : System.Web.UI.Page
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
                    liPRM.Visible = false;
                    //if (Request.QueryString.Count > 0)
                    //{
                    //hdnABAPODUploadId.Value = Convert.ToString(Request.QueryString["ABAPODId"]).Trim();

                    DDLABAPDevDesc.Items.Insert(0, new ListItem("Select ABAP Development Desc", "0"));
                    getABAPConsultantMaster();
                    get_ABAPObject_For_ABAPperDetails();

                    //}

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
            liPRM.Visible = false;
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
                lblmessage.Text = "Changes to the ABAP Consultant have been restricted to project managers only.";
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return;
        }
    }

    public void get_ABAPObject_For_ABAPperDetails()
    {
        lblmessage.Text = "";
        if (DDLProjectLocation.SelectedValue != "0")
        {
            liPRM.Visible = true;
        }
        else
        {
            liPRM.Visible = false;
        }
        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetABAPDEVDetailsByProjectLocation";

        spars[1] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
        spars[1].Value = (Session["Empcode"]).ToString().Trim();

        spars[2] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
        spars[2].Value = DDLProjectLocation.SelectedValue == "0" ? "" : DDLProjectLocation.SelectedValue;

        spars[3] = new SqlParameter("@ABAPDevDesc", SqlDbType.VarChar);
        spars[3].Value = DDLABAPDevDesc.SelectedValue == "0" ? "" : DDLABAPDevDesc.SelectedValue;

        dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
        {
            gvABAPDevDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvABAPDevDetails.DataBind();
            rgs.Visible = true;
            trvl_btnSave.Visible = true;
        }
        else
        {
            gvABAPDevDetails.DataSource = null;
            rgs.Visible = false;
            gvABAPDevDetails.DataBind();
            trvl_btnSave.Visible = false;
            lblmessage.Text = "No record found.";
        }
        txtPRM.Text = dsABAPObjectPlanSubmitted.Tables[1].Rows.Count > 0 ? dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ProjectManager"].ToString().Trim() : "";
        hdnprojectManager.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows.Count > 0 ? dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ProjectManager"].ToString().Trim() : "";
        hdnprojectManagerEmail.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows.Count > 0 ? dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["projectManagerEmail"].ToString().Trim() : "";
        hdnprogramnager.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows.Count > 0 ? dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["programnager"].ToString().Trim() : "";
        hdnprogramnagerEmail.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows.Count > 0 ? dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["programnagerEmail"].ToString().Trim() : "";

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

    public void DDLProjectLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        //get_ABAPObject_For_ABAPperDetails();
        try
        {
            lblmessage.Text = "";
            var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getABAPDevelopmentDescListForABAPper";

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

                gvABAPDevDetails.DataSource = DS.Tables[1];
                gvABAPDevDetails.DataBind();
                rgs.Visible = true;
                liPRM.Visible = true;
                trvl_btnSave.Visible = true;
                txtPRM.Text = DS.Tables[2].Rows.Count > 0 ? DS.Tables[2].Rows[0]["ProjectManager"].ToString().Trim() : "";

            }
            else
            {
                if (DDLABAPDevDesc.Items.Count > 0)
                {
                    DDLABAPDevDesc.Items.Clear();
                    DDLABAPDevDesc.Items.Insert(0, new ListItem("Select ABAP Development Desc", "0"));
                }

                gvABAPDevDetails.DataSource = null;
                rgs.Visible = false;
                gvABAPDevDetails.DataBind();
                trvl_btnSave.Visible = false;
                lblmessage.Text = "No record found.";
            }

        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return;
        }

    }

    protected void updateABAPDev_btnSave_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }
        bool isChecked = false;
        lblmessage.Text = "";
        if (Convert.ToString(DDLProjectLocation.SelectedValue).Trim() == "0" || Convert.ToString(DDLProjectLocation.SelectedValue).Trim() == "")
        {
            lblmessage.Text = "Please select Project Location.";
            return;
        }

        if (Convert.ToString(DDLPlanABAPConsultant.SelectedValue).Trim() == "0" || Convert.ToString(DDLPlanABAPConsultant.SelectedValue).Trim() == "")
        {
            lblmessage.Text = "Please select FS Consultant.";
            return;
        }

        if (string.IsNullOrEmpty(txtRemarks.Text))
        {
            lblmessage.Text = "Kindly add a remark when making changes to the ABAP Consultant.";
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
            lblmessage.Text = "Select at least one record of using the checkbox before submitting.";
            lblmessage.Visible = true;
            return;
        }
        else
        {
            foreach (GridViewRow row in gvABAPDevDetails.Rows)
            {
                CheckBox chkBox = (CheckBox)row.FindControl("ABAPDevchkSelect");
                if (chkBox != null && chkBox.Checked)
                {
                    string RGSDetailsId = gvABAPDevDetails.DataKeys[row.RowIndex].Value.ToString();
                    var FunctionalConsultant = Convert.ToString(DDLPlanABAPConsultant.SelectedValue).ToString();
                    var objdata = UpdateConsultantDB("ChangeABAPDEVConsultant", RGSDetailsId, FunctionalConsultant, txtRemarks.Text);
                    if (objdata != null || objdata.Tables.Count > 0 || objdata.Tables[0].Rows.Count > 0)
                    {
                        if (objdata.Tables[0].Rows[0]["ResponseMsg"].ToString() == "Records udpate sucessfully.")
                        {
                            hdnNewABAPConsultantEmailId.Value = objdata.Tables[0].Rows[0]["NewABAPConsultantEmailId"].ToString();
                            hdnNewABAPConsultantName.Value = objdata.Tables[0].Rows[0]["NewABAPConsultantName"].ToString();

                            hdnOldABAPConsultantEmailId.Value = objdata.Tables[0].Rows[0]["OldABAPConsultantEmailId"].ToString();
                            hdnOldABAPConsultantName.Value = objdata.Tables[0].Rows[0]["OldABAPConsultantName"].ToString();

                            hdnDev_Desc.Value = objdata.Tables[0].Rows[0]["Dev_Desc"].ToString();

                            #region Send Email 
                            string strSubject = "";
                            strSubject = DDLProjectLocation.SelectedItem.Text.ToString().Trim() + " ABAP Object " + hdnDev_Desc.Value.ToString().Trim();
                            string Email_TO = hdnNewABAPConsultantEmailId.Value.ToString().Trim();
                            string Email_CC = hdnprogramnagerEmail.Value.ToString().Trim() + ";" + hdnOldABAPConsultantEmailId.Value.ToString().Trim();

                            StringBuilder strbuild = new StringBuilder();
                            strbuild.Append("<p style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'></p>");
                            strbuild.Append("<p>" + "Dear " + hdnNewABAPConsultantName.Value.ToString().Trim() + "</p>");
                            strbuild.Append("<p> This is to inform you that you have assigned to new object <b>" + hdnDev_Desc.Value.ToString().Trim() + "</b>. Please co-ordinatinate with <b>" + hdnOldABAPConsultantName.Value.ToString().Trim() + "</b>.</p>");

                            strbuild.Append("<br><br>This is an auto generated email, please do not reply!");

                            //spm.sendMail(Email_TO, strSubject, Convert.ToString(strbuild).Trim(), "", Email_CC);

                            #endregion
                        }
                    }

                }
            }

            Response.Redirect("~/procs/ABAP_Object_Tracker_Index.aspx");
        }
    }

    public DataSet UpdateConsultantDB(string qtype, string DetailsId, string FConsultantId, string Remark)
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
                new SqlParameter("@Remark", SqlDbType.VarChar) { Value = Remark.ToString().Trim() },
        };

            dsData = spm.getDatasetList(spars.ToArray(), "SP_ABAPObjectTracking");
            return dsData;
        }
        catch (Exception e)
        {
            lblmessage.Text = e.Message.ToString();
        }

        return dsData;
    }
    public void DDLABAPDevDesc_SelectedIndexChanged(object sender, EventArgs e)
    {
        get_ABAPObject_For_ABAPperDetails();
    }
    #endregion
}