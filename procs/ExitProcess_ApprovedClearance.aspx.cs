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
public partial class procs_ExitProcess_ApprovedClearance : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int RecordCountDelete = 0;
    public int did = 0;
   
    SP_Methods spm = new SP_Methods();
    string strempcode = "";

    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    DataTable dtExitFrm;

   // string strempcode = "";

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void lnkcont_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/Reembursementindex");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                    
                        {
                            editform.Visible = true;
                    hdnEmpCode.Value = Session["Empcode"].ToString();
                    get_myPOWOMilestone_DropdownList();
                    getMngTravelReqstList();
                    getApprovedClearance();
                            
                            this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                        }
                if (Request.QueryString.Count > 0)
                {
                    hdnInboxType.Value = Convert.ToString(Request.QueryString[0]).Trim();
                    if (hdnInboxType.Value == "Pending")
                    {
                        //GetEmployeeName("Retention_M_Pending_Emp");
                        //GetDepartmentMaster("Retention_M_Pending_Dept");
                        //GetLocation_Name("Retention_M_Pending_Location");
                        //GetRetentionEmployee("Retention_Mode_Pending_List");
                    }
                    else
                    {
                        //GetEmployeeName("Retention_M_APP_Emp");
                        //GetDepartmentMaster("Retention_M_APP_Dept");
                        //GetLocation_Name("Retention_M_APP_Location");
                        //GetRetentionEmployee("Retention_Mode_APP_List");
                    }

                }  
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    private void getApprovedClearance()
    {
        try
        {
            // DataSet dsList = new DataSet();

            dtExitFrm = spm.GetApprovedClearance(strempcode);
            gvMngLeaveRqstList.DataSource = null;
            gvMngLeaveRqstList.DataBind();
            if (dtExitFrm.Rows.Count > 0)
            {
                gvMngLeaveRqstList.DataSource = dtExitFrm;
                gvMngLeaveRqstList.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnReqid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[0]).Trim();

            Response.Redirect("ExitProcess_ClearanceApproveForm.aspx?ResignationID=" + hdnReqid.Value + "&Type=Approved");

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    //protected void gvMngLeaveRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvMngLeaveRqstList.PageIndex = e.NewPageIndex;
    //    if (hdnInboxType.Value == "Pending")
    //    {
    //        //GetRetentionEmployee("Retention_Mode_Pending_List");
    //    }
    //    else
    //    {
    //        //GetRetentionEmployee("Retention_Mode_APP_List");
    //    }

    //    getApprovedClearance();
    //    gvMngLeaveRqstList.PageIndex = e.NewPageIndex;
    //    gvMngLeaveRqstList.DataSource = dtExitFrm;
    //    gvMngLeaveRqstList.DataBind();
    //}

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnReqid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[0]).Trim();

            Response.Redirect("ExitProcess_ClearanceApproveForm.aspx?ResignationID=" + hdnReqid.Value + "&Type=ApprovedView");

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    protected void gvMngLeaveRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMngLeaveRqstList.PageIndex = e.NewPageIndex;
        this.getMngTravelReqstList();
    }

    private void getMngTravelReqstList()
    {
        DataTable dtRequisitionDetails = new DataTable();
            lblmessage.Text = "";
        try
        {
            DataSet dsmyInvoice = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
            spars[0].Value = "GetApprovedClearance";

            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = strempcode;

            // Searching Parameter
            spars[2] = new SqlParameter("@ExitClearanceTextAns", SqlDbType.VarChar);
            if (Convert.ToString(lstProjets.SelectedValue).Trim() != "0")
                spars[2].Value = Convert.ToString(lstProjets.SelectedValue).Trim();
            else
                spars[2].Value = DBNull.Value;

            spars[3] = new SqlParameter("@VerifyBy", SqlDbType.VarChar);
            if (Convert.ToString(lstEmployeeName.SelectedValue).Trim() != "0")
                spars[3].Value = Convert.ToString(lstEmployeeName.SelectedValue).Trim();
            else
                spars[3].Value = DBNull.Value;

            dsmyInvoice = spm.getDatasetList(spars, "spExitProc_ClearanceForm");

            gvMngLeaveRqstList.DataSource = null;
            gvMngLeaveRqstList.DataBind();
            RecordCount.Text = "";
            lblmessage.Text = "";
            
            if (dsmyInvoice.Tables[0].Rows.Count > 0)
            {
                if (dsmyInvoice.Tables[0].Rows.Count > 0)
                {
                    RecordCountDelete = dsmyInvoice.Tables[0].Rows.Count;
                    RecordCount.Text = "Record Count : " + Convert.ToString(dsmyInvoice.Tables[0].Rows.Count);
                    gvMngLeaveRqstList.DataSource = dsmyInvoice.Tables[0];
                    gvMngLeaveRqstList.DataBind();

                 }
                else
                {
                    lblmessage.Text = "Record's not found.!";
                    lblmessage.Visible = true;
                }
            }
            
        }
        catch (Exception ex)
        {
            
        }
    }

    //protected void mobile_btnSave_Click(object sender, EventArgs e)
    //{
    //    if (hdnInboxType.Value == "Pending")
    //    {
    //        GetRetentionEmployee("Retention_Mode_Pending_List");
    //    }
    //    else
    //    {
    //        GetRetentionEmployee("Retention_Mode_APP_List");
    //    }
    //}
    //protected void mobile_btnBack_Click(object sender, EventArgs e)
    //{
    //    lstEmployeeName.SelectedIndex = -1;
    //    lstPositionDept.SelectedIndex = -1;
    //    lstLocation.SelectedIndex = -1;
    //    lblmessage.Text = "";
    //    if (hdnInboxType.Value == "Pending")
    //    {
    //        GetEmployeeName("Retention_M_Pending_Emp");
    //        GetDepartmentMaster("Retention_M_Pending_Dept");
    //        GetLocation_Name("Retention_M_Pending_Location");
    //        GetRetentionEmployee("Retention_Mode_Pending_List");
    //    }
    //    else
    //    {
    //        GetEmployeeName("Retention_M_APP_Emp");
    //        GetDepartmentMaster("Retention_M_APP_Dept");
    //        GetLocation_Name("Retention_M_APP_Location");
    //        GetRetentionEmployee("Retention_Mode_APP_List");
    //    }
    //}
    protected void lnkView_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            hdnRetentionID.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
            //string Rec_ID = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[2]).Trim();
            Response.Redirect("ExitProcess_Mo_Approval.aspx?RetentionID=" + hdnRetentionID.Value + "&Type=View");
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    protected void gvMngTravelRqstList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToString(hdnInboxType.Value).Trim() == "View")
            {
                ImageButton imgBtn = (ImageButton)e.Row.FindControl("lnkView");
                imgBtn.Visible = true;
                ImageButton imgBtn1 = (ImageButton)e.Row.FindControl("lnkEdit");
                imgBtn1.Visible = false;
            }
            else
            {
                ImageButton imgBtn = (ImageButton)e.Row.FindControl("lnkEdit");
                imgBtn.Visible = true;

            }
        }
    }
    //public void GetEmployeeName(string Qtype)
    //{
    //    DataTable dtSkillset = new DataTable();
    //    dtSkillset = spm.GetApprovedClearance(strempcode);
    //    if (dtSkillset.Rows.Count > 0)
    //    {
    //        lstEmployeeName.DataSource = dtSkillset;
    //        lstEmployeeName.DataTextField = "Emp_Name";
    //        lstEmployeeName.DataValueField = "Emp_Code";
    //        lstEmployeeName.DataBind();
    //    }
    //    lstEmployeeName.Items.Insert(0, new ListItem("Select Employee Name", "0"));
    //}
    //public void GetDepartmentMaster(string Qtype)
    //{
    //    DataTable dtPositionDept = new DataTable();
    //    dtPositionDept = spm.GetApprovedClearance(strempcode);
    //    if (dtPositionDept.Rows.Count > 0)
    //    {
    //        lstPositionDept.DataSource = dtPositionDept;
    //        lstPositionDept.DataTextField = "Department_Name";
    //        lstPositionDept.DataValueField = "Department_id";
    //        lstPositionDept.DataBind();
    //    }
    //    lstPositionDept.Items.Insert(0, new ListItem("Select Department", "0"));
    //}
    //public void GetLocation_Name(string Qtype)
    //{
    //    DataTable dtPositionDept = new DataTable();
    //    dtPositionDept = spm.GetApprovedClearance(strempcode);
    //    if (dtPositionDept.Rows.Count > 0)
    //    {
    //        lstLocation.DataSource = dtPositionDept;
    //        lstLocation.DataTextField = "emp_projectName";
    //        lstLocation.DataValueField = "comp_code";
    //        lstLocation.DataBind();
    //    }
    //    lstLocation.Items.Insert(0, new ListItem("Select Project Name", "0"));
    //}
    //public void GetRetentionEmployee(string Qtype)
    //{
    //    DataTable dtRequisitionDetails = new DataTable();
    //    lblmessage.Text = "";
    //    try
    //    {
    //        int Dept_id = 0;
    //        string EmpCode = "", comp_code = "";
    //        if (lstEmployeeName.SelectedIndex > 0)
    //        {
    //            EmpCode = lstEmployeeName.SelectedValue;
    //        }
    //        if (lstPositionDept.SelectedIndex > 0)
    //        {
    //            Dept_id = Convert.ToInt32(lstPositionDept.SelectedValue);
    //        }
    //        if (lstLocation.SelectedIndex > 0)
    //        {
    //            comp_code = Convert.ToString(lstLocation.SelectedValue);
    //        }
    //        SqlParameter[] spars = new SqlParameter[5];
    //        spars[0] = new SqlParameter("@Qtype", SqlDbType.VarChar);
    //        spars[0].Value = Qtype;
    //        spars[1] = new SqlParameter("@EmpCode", SqlDbType.NVarChar);
    //        spars[1].Value = EmpCode;
    //        spars[2] = new SqlParameter("@Dept_id", SqlDbType.Int);
    //        spars[2].Value = Dept_id;
    //        spars[3] = new SqlParameter("@Comp_code", SqlDbType.NVarChar);
    //        spars[3].Value = comp_code;
    //        spars[4] = new SqlParameter("@AppEmpCode", SqlDbType.NVarChar);
    //        spars[4].Value = Convert.ToString(Session["Empcode"]);
    //        dtRequisitionDetails = spm.getMobileRemDataList(spars, "SP_Employee_Retention_Details");
    //        gvMngLeaveRqstList.DataSource = null;
    //        gvMngLeaveRqstList.DataBind();
    //        if (dtRequisitionDetails.Rows.Count > 0)
    //        {
    //            gvMngLeaveRqstList.DataSource = dtRequisitionDetails;
    //            gvMngLeaveRqstList.DataBind();

    //        }
    //        else
    //        {
    //            lblmessage.Text = "Record not available";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message.ToString());
    //    }

    //}


    public void get_myPOWOMilestone_DropdownList()
    {
        DataSet dsLocations = new DataSet();
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
        spars[0].Value = "getLocation_Times";

        spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        spars[1].Value = strempcode;

        dsLocations = spm.getDatasetList(spars, "spExitProc_ClearanceForm");

        if (dsLocations != null)
        {
            if (dsLocations.Tables[0].Rows.Count > 0)
            {
                lstEmployeeName.DataSource = dsLocations.Tables[0];
                lstEmployeeName.DataTextField = "Emp_Name";
                lstEmployeeName.DataValueField = "Emp_Code";
                lstEmployeeName.DataBind();

            }
            lstEmployeeName.Items.Insert(0, new ListItem("Select Employee Name", "0"));

            if (dsLocations.Tables[1].Rows.Count > 0)
            {
                lstProjets.DataSource = dsLocations.Tables[1];
                lstProjets.DataTextField = "emp_projectName";
                lstProjets.DataValueField = "emp_projectName";
                lstProjets.DataBind();
            }
            lstProjets.Items.Insert(0, new ListItem("Select Project", "0"));

        }

        
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        getMngTravelReqstList();
        lblmessage.Text = "";
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        get_myPOWOMilestone_DropdownList();
        getMngTravelReqstList();
        lblmessage.Text = "";

    }

}