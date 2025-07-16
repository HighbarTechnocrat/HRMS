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
using System.Collections.Generic;

public partial class procs_Rec_RequisitionViewListApproved : System.Web.UI.Page
{
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public int did = 0;
    SP_Methods spm = new SP_Methods();
    DataSet dsRecruterInox;
    // public DataTable dtRecCandidate, dtcandidateDetails, dtmainSkillSet;


    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
            // lblmessage.Text = "";
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
            // lpm.Emp_Code = Session["Empcode"].ToString();
            // lblmessage.Visible = true;
            //   
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    GetSkillsetName();
                    GetCompany_Location();
                    GetDepartmentMaster();
                    GetlstPositionBand();
                    getMngRecruiterViewAllApprovedList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    #endregion

    #region page Methods

    public void GetSkillsetName()
    {
        DataTable dtSkillset = new DataTable();
        dtSkillset = spm.GetRecruitment_SkillsetName();
        if (dtSkillset.Rows.Count > 0)
        {
            lstSkillSet.DataSource = dtSkillset;
            lstSkillSet.DataTextField = "ModuleDesc";
            lstSkillSet.DataValueField = "ModuleId";
            lstSkillSet.DataBind();
            lstSkillSet.Items.Insert(0, new ListItem("Select Skillset", "0"));

        }
    }
    public void GetCompany_Location()
    {
        DataTable lstPosition = new DataTable();
        lstPosition = spm.GetRecruitment_Req_company_Location();
        if (lstPosition.Rows.Count > 0)
        {
            LstLocation.DataSource = lstPosition;
            LstLocation.DataTextField = "Location_name";
            LstLocation.DataValueField = "comp_code";
            LstLocation.DataBind();
            LstLocation.Items.Insert(0, new ListItem("Select Location", "0"));

        }
    }

    public void GetDepartmentMaster()
    {
        DataSet dsReqNo = new DataSet();
        try
        {
            DataTable dtEmployee = new DataTable();
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "SP_RequisitionViewStatusList";
            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(Session["Empcode"]);
            dtEmployee = spm.getMobileRemDataList(spars, "SP_GET_REQ_REQUISTIONNO");

            if (dtEmployee.Rows.Count > 0)
            {
                LstDepartment.DataSource = dtEmployee;
                LstDepartment.DataTextField = "Department_Name";
                LstDepartment.DataValueField = "Department_id";
                LstDepartment.DataBind();
                LstDepartment.Items.Insert(0, new ListItem("Select Department", "0"));
            }
            if (dtEmployee.Rows.Count == 1)
            {
                //LstDepartment.Enabled = false;
                LstDepartment.SelectedValue = dtEmployee.Rows[0]["Department_id"].ToString();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void GetlstPositionBand()
    {
        DataTable dtPositionBand = new DataTable();
        dtPositionBand = spm.GetRecruitment_Req_HRMS_BAND_MASTER();
        if (dtPositionBand.Rows.Count > 0)
        {
            Lstband.DataSource = dtPositionBand;
            Lstband.DataTextField = "BAND";
            Lstband.DataValueField = "BAND";
            Lstband.DataBind();
            Lstband.Items.Insert(0, new ListItem("Select BAND", "0"));
        }
    }

    private void getMngRecruiterViewAllApprovedList()
    {
        try
        {
            dsRecruterInox = spm.getRecruterInoxList(Convert.ToString(Session["Empcode"]).Trim(), "RequisitionViewAllApproved");
            gvRecruterInoxList.DataSource = dsRecruterInox.Tables[0];
            gvRecruterInoxList.DataBind();
            if (dsRecruterInox.Tables[0].Rows.Count > 0)
            {
                lbltotalRecords.Text = "Total Records :- " + dsRecruterInox.Tables[0].Rows.Count;

                lstRequisitionNo.DataSource = dsRecruterInox.Tables[0];
                lstRequisitionNo.DataTextField = "RequisitionNumber";
                lstRequisitionNo.DataValueField = "Recruitment_ReqID";
                lstRequisitionNo.DataBind();
                lstRequisitionNo.Items.Insert(0, new ListItem("Select Requisition No", "0"));
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        HFRecruitment_ReqID.Value = Convert.ToString(gvRecruterInoxList.DataKeys[row.RowIndex].Values[0]).Trim();
        Response.Redirect("~/procs/RequisitionView.aspx?Recruitment_ReqID=" + HFRecruitment_ReqID.Value);
    }

    #endregion

    protected void gvRecruterInoxList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRecruterInoxList.PageIndex = e.NewPageIndex;
        if (Lstband.SelectedValue == "0" && LstDepartment.SelectedValue == "0" && LstLocation.SelectedValue == "0" && lstRequisitionNo.SelectedValue == "0" && lstSkillSet.SelectedValue == "0")
        {
            getMngRecruiterViewAllApprovedList();
        }
        else
        {
            Searchmethod();
        }
    }


    private void Searchmethod()
    {
        try
        {
            int SkillSetID = 0, Recruitment_ReqID = 0, DeptID = 0;
            SkillSetID = Convert.ToString(lstSkillSet.SelectedValue).Trim() != "" ? Convert.ToInt32(lstSkillSet.SelectedValue) : 0;
            Recruitment_ReqID = Convert.ToString(lstRequisitionNo.SelectedValue).Trim() != "" ? Convert.ToInt32(lstRequisitionNo.SelectedValue) : 0;
            DeptID = Convert.ToString(LstDepartment.SelectedValue).Trim() != "" ? Convert.ToInt32(LstDepartment.SelectedValue) : 0;

            SqlParameter[] spars = new SqlParameter[8];
            DataSet DS = new DataSet();
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "RequisitionViewAllApprovedSearch";
            spars[1] = new SqlParameter("@emp_Code", SqlDbType.VarChar);
            spars[1].Value = Session["Empcode"].ToString();
            spars[2] = new SqlParameter("@SkillSetID", SqlDbType.Int);
            spars[2].Value = SkillSetID;
            spars[3] = new SqlParameter("@LocationID", SqlDbType.VarChar);
            spars[3].Value = LstLocation.SelectedValue;
            spars[4] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
            spars[4].Value = Recruitment_ReqID;
            spars[5] = new SqlParameter("@BandID", SqlDbType.VarChar);
            spars[5].Value = Lstband.SelectedValue;
            spars[6] = new SqlParameter("@Department_id", SqlDbType.Int);
            spars[6].Value = DeptID;
            DS = spm.getDatasetList(spars, "SP_Get_Rec_RequiterChangeIndex_Search");

            if (DS.Tables[0].Rows.Count > 0)
            {
                lbltotalRecords.Text = "Total Records :- " + DS.Tables[0].Rows.Count;
                gvRecruterInoxList.DataSource = DS.Tables[0];
                gvRecruterInoxList.DataBind();
                lblmessagesearch.Text = "";
            }
            else
            {
                lblmessagesearch.Text = "Requisition Record's not found";
                gvRecruterInoxList.DataSource = null;
                gvRecruterInoxList.DataBind();
                lbltotalRecords.Text = "";

            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        Searchmethod();
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        lstSkillSet.SelectedValue = "0";
        LstLocation.SelectedValue = "0";
        Lstband.SelectedValue = "0";
        LstDepartment.SelectedValue = "0";
        lblmessagesearch.Text = "";
        getMngRecruiterViewAllApprovedList();
    }
}