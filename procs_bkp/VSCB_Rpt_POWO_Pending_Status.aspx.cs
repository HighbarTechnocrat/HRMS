using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_VSCB_Rpt_POWO_Pending_Status : System.Web.UI.Page
{
    #region Creative_Default_methods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;

    SP_Methods spm = new SP_Methods();
    string strempcode = "";
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void lnkcont_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/SampleForm");
    }

    #endregion
    public DataTable dtPOWONo;
    #region Page_Events

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/vscb_index");
            }
            else
            {
                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    hdnEmpCode.Value = Session["Empcode"].ToString();
                    get_ApprovalStatusReport_DropdownList();
                    Reports();
                   
                   // this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        Reports();

    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {

        get_ApprovalStatusReport_DropdownList();
        Reports();
    }
    protected void mobile_cancel_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        string ccMailIDsEmployee = "";
        var ddlEmployee = "";

        foreach (ListItem item in lstCCEmployeeEmailAddress.Items)
        {
            if (item.Selected)
            {
                if (item.Value != "" && item.Value != "0")
                {
                   
                    if (ddlEmployee == "")
                    {
                        ddlEmployee = item.Text;
                        ccMailIDsEmployee =  item.Value;
                    }
                    else
                    {
                        ccMailIDsEmployee = ccMailIDsEmployee + ";" + item.Value;
                        ddlEmployee = ddlEmployee + "," + item.Text;
                    }
                }
            }
        }

        if (ccMailIDsEmployee.Trim() == "")
        {
            lblmessage.Visible = true;
            lblmessage.Text = "Select Email Address";
            return;
        }
        else
        {
           Export_Reports(ccMailIDsEmployee);
        }

        Reports();

    }


    protected void gvMngPaymentList_Batch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMngPaymentList_Batch.PageIndex = e.NewPageIndex;
        Reports();
    }

    protected void gvMngPaymentList_Batch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[8].Text.Trim() != "")
            {
                if (e.Row.Cells[8].Text.Trim().Contains("Approved"))
                {
                    e.Row.Cells[8].Style["background-color"] = "Green";
                }
                else if (e.Row.Cells[8].Text.Trim().Contains("Pending"))
                {
                    e.Row.Cells[8].Style["background-color"] = "Yellow";
                }
            }
            if (e.Row.Cells[9].Text.Trim() != "")
            {
                if (e.Row.Cells[9].Text.Trim().Contains("Approved"))
                {
                    e.Row.Cells[9].Style["background-color"] = "Green";
                }
                else if (e.Row.Cells[9].Text.Trim().Contains("Pending"))
                {
                    e.Row.Cells[9].Style["background-color"] = "Yellow";
                }
            }
            if (e.Row.Cells[10].Text.Trim() != "")
            {
                if (e.Row.Cells[10].Text.Trim().Contains("Approved"))
                {
                    e.Row.Cells[10].Style["background-color"] = "Green";
                }
                else if (e.Row.Cells[10].Text.Trim().Contains("Pending"))
                {
                    e.Row.Cells[10].Style["background-color"] = "Yellow";
                }
            }
            if (e.Row.Cells[11].Text.Trim() != "")
            {
                if (e.Row.Cells[11].Text.Trim().Contains("Approved"))
                {
                    e.Row.Cells[11].Style["background-color"] = "Green";
                }
                else if (e.Row.Cells[11].Text.Trim().Contains("Pending"))
                {
                    e.Row.Cells[11].Style["background-color"] = "Yellow";
                }
            }

        }
    }
    #endregion

    #region Page Methods

    private void get_ApprovalStatusReport_DropdownList()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Pending_POWOApprovalStatusreport_Dropdown_List";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCode.Value;

        dsList = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");

        if (dsList.Tables[0].Rows.Count > 0)
        {
            lstPOWONo.DataSource = dsList.Tables[0];
            lstPOWONo.DataTextField = "PONumber";
            lstPOWONo.DataValueField = "POID";
            lstPOWONo.DataBind();
        }
        lstPOWONo.Items.Insert(0, new ListItem("Select PO/WO No", "0"));

        if (dsList.Tables[1].Rows.Count > 0)
        {
            lstVendorName.DataSource = dsList.Tables[1];
            lstVendorName.DataTextField = "Name";
            lstVendorName.DataValueField = "VendorID";
            lstVendorName.DataBind();
        }
        lstVendorName.Items.Insert(0, new ListItem("Select Vendor Name", "0"));

        if (dsList.Tables[2].Rows.Count > 0)
        {
            lstCostCenter.DataSource = dsList.Tables[2];
            lstCostCenter.DataTextField = "CostCentre";
            lstCostCenter.DataValueField = "Dept_ID";
            lstCostCenter.DataBind();
        }
        lstCostCenter.Items.Insert(0, new ListItem("Select Cost Center", "0"));

        if (dsList.Tables[3].Rows.Count > 0)
        {
            lstCCEmployeeEmailAddress.DataSource = dsList.Tables[3];
            lstCCEmployeeEmailAddress.DataTextField = "Emp_CodeAddress";
            lstCCEmployeeEmailAddress.DataValueField = "Emp_Emailaddress";
            lstCCEmployeeEmailAddress.DataBind();
        }
        lstCCEmployeeEmailAddress.Items.Insert(0, new ListItem("Select Employee Email Address", "0"));
    }


    private void Reports()
    {
        DataSet dsApprovalStatusReport = new DataSet();

        #region get Approval Status Report


        SqlParameter[] spars = new SqlParameter[5];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Pending_POWO_Approval_Report";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCode.Value;

        spars[2] = new SqlParameter("@POWOID", SqlDbType.BigInt);
        if (Convert.ToString(lstPOWONo.SelectedValue).Trim() != "0")
            spars[2].Value = Convert.ToDouble(lstPOWONo.SelectedValue);
        else
            spars[2].Value = DBNull.Value;


        spars[3] = new SqlParameter("@VendorId", SqlDbType.Int);
        if (Convert.ToString(lstVendorName.SelectedValue).Trim() != "0")
            spars[3].Value = Convert.ToInt32(lstVendorName.SelectedValue);
        else
            spars[3].Value = DBNull.Value;

        spars[4] = new SqlParameter("@Costcenter", SqlDbType.NVarChar);
        if (Convert.ToString(lstCostCenter.SelectedValue).Trim() != "0") 
            spars[4].Value = Convert.ToInt32(lstCostCenter.SelectedValue);
        else
            spars[4].Value = DBNull.Value;

        dsApprovalStatusReport = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");
        try
        {
            if (dsApprovalStatusReport.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsApprovalStatusReport.Tables[0].Rows.Count; i++)
                {
                    string strPOID = dsApprovalStatusReport.Tables[0].Rows[i]["POID"].ToString();

                    if (dsApprovalStatusReport.Tables[1].Rows.Count > 0)
                    {
                        DataRow[] dr = dsApprovalStatusReport.Tables[1].Select("POID = '" + Convert.ToInt32(strPOID) + "'");
                        if (dr.Length == 0)
                        {
                        }
                        if (dr.Length == 1)
                        {
                            dsApprovalStatusReport.Tables[0].Rows[i]["Approver1"] = dr[0].ItemArray[6];

                            if (dr[0].ItemArray[8].ToString() == "Pending")
                            {
                                dsApprovalStatusReport.Tables[0].Rows[i]["EmailAddress"] = dr[0].ItemArray[7];
                            }


                        }
                        if (dr.Length == 2)
                        {
                            dsApprovalStatusReport.Tables[0].Rows[i]["Approver1"] = dr[0].ItemArray[6];
                            dsApprovalStatusReport.Tables[0].Rows[i]["Approver2"] = dr[1].ItemArray[6];

                            if (dr[0].ItemArray[8].ToString() == "Pending")
                            {
                                dsApprovalStatusReport.Tables[0].Rows[i]["EmailAddress"] = dr[0].ItemArray[7];
                            }
                            if (dr[1].ItemArray[8].ToString() == "Pending")
                            {
                                dsApprovalStatusReport.Tables[0].Rows[i]["EmailAddress"] = dr[1].ItemArray[7];
                            }
                        }
                        if (dr.Length == 3)
                        {
                            dsApprovalStatusReport.Tables[0].Rows[i]["Approver1"] = dr[0].ItemArray[6];
                            dsApprovalStatusReport.Tables[0].Rows[i]["Approver2"] = dr[1].ItemArray[6];
                            dsApprovalStatusReport.Tables[0].Rows[i]["Approver3"] = dr[2].ItemArray[6];

                            if (dr[0].ItemArray[8].ToString() == "Pending")
                            {
                                dsApprovalStatusReport.Tables[0].Rows[i]["EmailAddress"] = dr[0].ItemArray[7];
                            }
                            if (dr[1].ItemArray[8].ToString() == "Pending")
                            {
                                dsApprovalStatusReport.Tables[0].Rows[i]["EmailAddress"] = dr[1].ItemArray[7];
                            }
                            if (dr[2].ItemArray[8].ToString() == "Pending")
                            {
                                dsApprovalStatusReport.Tables[0].Rows[i]["EmailAddress"] = dr[2].ItemArray[7];
                            }
                        }
                        if (dr.Length == 4)
                        {
                            dsApprovalStatusReport.Tables[0].Rows[i]["Approver1"] = dr[0].ItemArray[6];
                            dsApprovalStatusReport.Tables[0].Rows[i]["Approver2"] = dr[1].ItemArray[6];
                            dsApprovalStatusReport.Tables[0].Rows[i]["Approver3"] = dr[2].ItemArray[6];
                            dsApprovalStatusReport.Tables[0].Rows[i]["Approver4"] = dr[3].ItemArray[6];

                            if (dr[0].ItemArray[8].ToString() == "Pending")
                            {
                                dsApprovalStatusReport.Tables[0].Rows[i]["EmailAddress"] = dr[0].ItemArray[7];
                            }
                            if (dr[1].ItemArray[8].ToString() == "Pending")
                            {
                                dsApprovalStatusReport.Tables[0].Rows[i]["EmailAddress"] = dr[1].ItemArray[7];
                            }
                            if (dr[2].ItemArray[8].ToString() == "Pending")
                            {
                                dsApprovalStatusReport.Tables[0].Rows[i]["EmailAddress"] = dr[2].ItemArray[7];
                            }
                            if (dr[3].ItemArray[8].ToString() == "Pending")
                            {
                                dsApprovalStatusReport.Tables[0].Rows[i]["EmailAddress"] = dr[3].ItemArray[7];
                            }

                        }
                        if (dr.Length == 5)
                        {

                        }

                    }
                }
            }

            dsApprovalStatusReport.AcceptChanges();

            var getTable = dsApprovalStatusReport.Tables[0];
            for (int i = 0; i < dsApprovalStatusReport.Tables[0].Rows.Count; i++)
            {
                string strEmailAddress = dsApprovalStatusReport.Tables[0].Rows[i]["EmailAddress"].ToString();
                DataRow[] drs = getTable.Select("EmailAddress = '" + strEmailAddress.Trim() + "'");
                DataTable dt2 = getTable.Clone();
                foreach (DataRow d in drs)
                {
                    dt2.ImportRow(d);
                }
            }

            gvMngPaymentList_Batch.DataSource = dsApprovalStatusReport.Tables[0];
            gvMngPaymentList_Batch.DataBind();
        
        }
        catch (Exception ex)
        {

        }
        #endregion
    }
    private void Export_Reports(string ccMailIDsEmployee)
    {
        DataSet dsApprovalStatusReport = new DataSet();

        #region get Approval Status Report


        SqlParameter[] spars = new SqlParameter[5];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Pending_POWO_Approval_Report";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCode.Value;

        spars[2] = new SqlParameter("@POWOID", SqlDbType.BigInt);
        if (Convert.ToString(lstPOWONo.SelectedValue).Trim() != "0")
            spars[2].Value = Convert.ToDouble(lstPOWONo.SelectedValue);
        else
            spars[2].Value = DBNull.Value;


        spars[3] = new SqlParameter("@VendorId", SqlDbType.Int);
        if (Convert.ToString(lstVendorName.SelectedValue).Trim() != "0")
            spars[3].Value = Convert.ToInt32(lstVendorName.SelectedValue);
        else
            spars[3].Value = DBNull.Value;

        spars[4] = new SqlParameter("@Costcenter", SqlDbType.NVarChar);
        if (Convert.ToString(lstCostCenter.SelectedValue).Trim() != "0")
            spars[4].Value = Convert.ToInt32(lstCostCenter.SelectedValue);
        else
            spars[4].Value = DBNull.Value;

        dsApprovalStatusReport = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");

        if (dsApprovalStatusReport.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsApprovalStatusReport.Tables[0].Rows.Count; i++)
            {
                string strPOID = dsApprovalStatusReport.Tables[0].Rows[i]["POID"].ToString();
                if (dsApprovalStatusReport.Tables[1].Rows.Count > 0)
                {
                    DataRow[] dr = dsApprovalStatusReport.Tables[1].Select("POID = '" + Convert.ToInt32(strPOID) + "'");
                    if (dr.Length == 0)
                    {
                    }
                    if (dr.Length == 1)
                    {
                        dsApprovalStatusReport.Tables[0].Rows[i]["Approver1"] = dr[0].ItemArray[6];

                        if (dr[0].ItemArray[8].ToString() == "Pending")
                        {
                            dsApprovalStatusReport.Tables[0].Rows[i]["EmailAddress"] = dr[0].ItemArray[7];
                        }
                    }
                    if (dr.Length == 2)
                    {
                        dsApprovalStatusReport.Tables[0].Rows[i]["Approver1"] = dr[0].ItemArray[6];
                        dsApprovalStatusReport.Tables[0].Rows[i]["Approver2"] = dr[1].ItemArray[6];

                        if (dr[0].ItemArray[8].ToString() == "Pending")
                        {
                            dsApprovalStatusReport.Tables[0].Rows[i]["EmailAddress"] = dr[0].ItemArray[7];
                        }
                        if (dr[1].ItemArray[8].ToString() == "Pending")
                        {
                            dsApprovalStatusReport.Tables[0].Rows[i]["EmailAddress"] = dr[1].ItemArray[7];
                        }
                    }
                    if (dr.Length == 3)
                    {
                        dsApprovalStatusReport.Tables[0].Rows[i]["Approver1"] = dr[0].ItemArray[6];
                        dsApprovalStatusReport.Tables[0].Rows[i]["Approver2"] = dr[1].ItemArray[6];
                        dsApprovalStatusReport.Tables[0].Rows[i]["Approver3"] = dr[2].ItemArray[6];

                        if (dr[0].ItemArray[8].ToString() == "Pending")
                        {
                            dsApprovalStatusReport.Tables[0].Rows[i]["EmailAddress"] = dr[0].ItemArray[7];
                        }
                        if (dr[1].ItemArray[8].ToString() == "Pending")
                        {
                            dsApprovalStatusReport.Tables[0].Rows[i]["EmailAddress"] = dr[1].ItemArray[7];
                        }
                        if (dr[2].ItemArray[8].ToString() == "Pending")
                        {
                            dsApprovalStatusReport.Tables[0].Rows[i]["EmailAddress"] = dr[2].ItemArray[7];
                        }
                    }
                    if (dr.Length == 4)
                    {
                        dsApprovalStatusReport.Tables[0].Rows[i]["Approver1"] = dr[0].ItemArray[6];
                        dsApprovalStatusReport.Tables[0].Rows[i]["Approver2"] = dr[1].ItemArray[6];
                        dsApprovalStatusReport.Tables[0].Rows[i]["Approver3"] = dr[2].ItemArray[6];
                        dsApprovalStatusReport.Tables[0].Rows[i]["Approver4"] = dr[3].ItemArray[6];

                        if (dr[0].ItemArray[8].ToString() == "Pending")
                        {
                            dsApprovalStatusReport.Tables[0].Rows[i]["EmailAddress"] = dr[0].ItemArray[7];
                        }
                        if (dr[1].ItemArray[8].ToString() == "Pending")
                        {
                            dsApprovalStatusReport.Tables[0].Rows[i]["EmailAddress"] = dr[1].ItemArray[7];
                        }
                        if (dr[2].ItemArray[8].ToString() == "Pending")
                        {
                            dsApprovalStatusReport.Tables[0].Rows[i]["EmailAddress"] = dr[2].ItemArray[7];
                        }
                        if (dr[3].ItemArray[8].ToString() == "Pending")
                        {
                            dsApprovalStatusReport.Tables[0].Rows[i]["EmailAddress"] = dr[3].ItemArray[7];
                        }
                    }
                    if (dr.Length == 5)
                    {
                    }
                }
            }
        }

        dsApprovalStatusReport.AcceptChanges();
        #endregion

        try
        {
            string ToMailID = "", ccMailIDs = "", ColorName = "white";

            ccMailIDs = ccMailIDsEmployee.ToString();

            var getTable = dsApprovalStatusReport.Tables[0];
            DataTable dt = dsApprovalStatusReport.Tables[2];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strEmailAddress = dt.Rows[i]["Approval_EmpEmailAddress"].ToString();
                DataRow[] drs = getTable.Select("EmailAddress = '" + strEmailAddress.Trim() + "'");
                DataTable dt2 = getTable.Clone();
                foreach (DataRow d in drs)
                {
                    dt2.ImportRow(d);
                }

                if (dt2.Rows.Count > 0)
                {

                    StringBuilder strbuild = new StringBuilder();
                    strbuild.Length = 0;
                    strbuild.Clear();
                    strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'><tr><td>   ");
                    strbuild.Append("</td></tr>");
                    strbuild.Append("<tr><td> Dear Sir, </td></tr>");
                    strbuild.Append("<tr><td style='height:15px'></td></tr>");
                    strbuild.Append("<tr><td>Following PO / WO are pending for approval with approver marked with yellow colour. Please approve the PO / WO from OneHr, so that it goes to next approver.  </td></tr>");
                    strbuild.Append("<tr><td style='height:10px'></td></tr>");
                    strbuild.Append("</table>");
                    strbuild.Append("<table style='border: 1px solid Navy;border-collapse: collapse;width:100%'>");
                    strbuild.Append("<tr style='background-color: #B8DBFD;'>");
                    strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>PO/WO No</th>");
                    strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>PO/WO Amount</th>");
                    strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>PO/WO Created By</th>");
                    strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>Cost Center</th>");
                    strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>Vendor Name</th>");
                    strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>Resource Name</th>");
                    strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>Approver-1</th>");
                    strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>Approver-2</th>");
                    strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>Approver-3</th>");
                    strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>Approver-4</th>");
                    // strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>Approver-5</th>");
                    strbuild.Append("</tr>");
                    foreach (DataRow items in dt2.Rows)
                    {

                        string strTOEmail = Convert.ToString(items["EmailAddress"]).Trim();

                        strbuild.Append("<tr style='text-align:left !important;border:1px solid black;color:#000000'>");
                        strbuild.Append("<td style='border: 1px solid black;'>" + Convert.ToString(items["PONumber"]).Trim() + "</td>");
                        strbuild.Append("<td style='border: 1px solid black;'>" + Convert.ToString(items["POWOAmount"]).Trim() + "</td>");
                        strbuild.Append("<td style='border: 1px solid black;'>" + Convert.ToString(items["POWOCreatedBy"]).Trim() + "</td>");
                        strbuild.Append("<td style='border: 1px solid black;'>" + Convert.ToString(items["CostCenter"]).Trim() + "</td>");
                        strbuild.Append("<td style='border: 1px solid black;'>" + Convert.ToString(items["VendorName"]).Trim() + "</td>");
                        strbuild.Append("<td style='border: 1px solid black;'>" + Convert.ToString(items["ResourceName"]).Trim() + "</td>");

                        if (Convert.ToString(items["Approver1"]).Trim().Contains("Pending"))
                        {
                            ColorName = "yellow";
                        }
                        else if (Convert.ToString(items["Approver1"]).Trim().Contains("Approved"))
                        {
                            ColorName = "green";
                        }
                        else { ColorName = "white"; }
                        strbuild.Append("<td style='border: 1px solid black;background-color:" + ColorName + ";'>" + Convert.ToString(items["Approver1"]).Trim() + "</td>");

                        if (Convert.ToString(items["Approver2"]).Trim().Contains("Pending"))
                        {
                            ColorName = "yellow";
                        }
                        else if (Convert.ToString(items["Approver2"]).Trim().Contains("Approved"))
                        {
                            ColorName = "green";
                        }
                        else { ColorName = "white"; }
                        strbuild.Append("<td style='border: 1px solid black;background-color:" + ColorName + ";'>" + Convert.ToString(items["Approver2"]).Trim() + "</td>");
                        if (Convert.ToString(items["Approver3"]).Trim().Contains("Pending"))
                        {
                            ColorName = "yellow";
                        }
                        else if (Convert.ToString(items["Approver3"]).Trim().Contains("Approved"))
                        {
                            ColorName = "green";
                        }
                        else { ColorName = "white"; }
                        strbuild.Append("<td style='border: 1px solid black;background-color:" + ColorName + ";'>" + Convert.ToString(items["Approver3"]).Trim() + "</td>");
                        if (Convert.ToString(items["Approver4"]).Trim().Contains("Pending"))
                        {
                            ColorName = "yellow";
                        }
                        else if (Convert.ToString(items["Approver4"]).Trim().Contains("Approved"))
                        {
                            ColorName = "green";
                        }
                        else { ColorName = "white"; }
                        strbuild.Append("<td style='border: 1px solid black;background-color:" + ColorName + ";'>" + Convert.ToString(items["Approver4"]).Trim() + "</td>");
                        strbuild.Append("</tr>");
                    }
                    var subject = "OneHR :- PO/WO Pending for approval";
                    spm.sendMail_VSCB(strEmailAddress, subject, Convert.ToString(strbuild).Trim(), "", ccMailIDs);

                    lstCostCenter.SelectedIndex = -1;
                    lstPOWONo.SelectedIndex = -1;
                    lstVendorName.SelectedIndex = -1;
                }
            }


            //if (getTable3.Rows.Count > 0)
            //{
            //    foreach (DataRow items in getTable3.Rows)
            //    {
            //        StringBuilder strbuild = new StringBuilder();
            //        strbuild.Length = 0;
            //        strbuild.Clear();
            //        strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'><tr><td>   ");
            //        strbuild.Append("</td></tr>");
            //        strbuild.Append("<tr><td> Dear Sir, </td></tr>");
            //        strbuild.Append("<tr><td style='height:15px'></td></tr>");
            //        strbuild.Append("<tr><td>Following PO / WO are pending for approval with approver marked with yellow colour. Please approve the PO / WO from OneHr, so that it goes to next approver.  </td></tr>");
            //        strbuild.Append("<tr><td style='height:10px'></td></tr>");
            //        strbuild.Append("</table>");
            //        strbuild.Append("<table style='border: 1px solid Navy;border-collapse: collapse;width:100%'>");
            //        strbuild.Append("<tr style='background-color: #B8DBFD;'>");
            //        strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>PO/WO No</th>");
            //        strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>PO/WO Amount</th>");
            //        strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>PO/WO Created By</th>");
            //        strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>Cost Center</th>");
            //        strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>Vendor Name</th>");
            //        strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>Resource Name</th>");
            //        strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>Approver-1</th>");
            //        strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>Approver-2</th>");
            //        strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>Approver-3</th>");
            //        strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>Approver-4</th>");
            //        // strbuild.Append("<th style='border: 1px solid Navy;border-collapse: collapse;'>Approver-5</th>");
            //        strbuild.Append("</tr>");

            //        string strTOEmail = Convert.ToString(items["EmailAddress"]).Trim();

            //        strbuild.Append("<tr style='text-align:left !important;border:1px solid black;color:#000000'>");
            //        strbuild.Append("<td style='border: 1px solid black;'>" + Convert.ToString(items["PONumber"]).Trim() + "</td>");
            //        strbuild.Append("<td style='border: 1px solid black;'>" + Convert.ToString(items["POWOAmount"]).Trim() + "</td>");
            //        strbuild.Append("<td style='border: 1px solid black;'>" + Convert.ToString(items["POWOCreatedBy"]).Trim() + "</td>");
            //        strbuild.Append("<td style='border: 1px solid black;'>" + Convert.ToString(items["CostCenter"]).Trim() + "</td>");
            //        strbuild.Append("<td style='border: 1px solid black;'>" + Convert.ToString(items["VendorName"]).Trim() + "</td>");
            //        strbuild.Append("<td style='border: 1px solid black;'>" + Convert.ToString(items["ResourceName"]).Trim() + "</td>");

            //        if (Convert.ToString(items["Approver1"]).Trim().Contains("Pending"))
            //        {
            //            ColorName = "yellow";
            //        }
            //        else if (Convert.ToString(items["Approver1"]).Trim().Contains("Approved"))
            //        {
            //            ColorName = "green";
            //        }
            //        else { ColorName = "white"; }
            //        strbuild.Append("<td style='border: 1px solid black;background-color:" + ColorName + ";'>" + Convert.ToString(items["Approver1"]).Trim() + "</td>");

            //        if (Convert.ToString(items["Approver2"]).Trim().Contains("Pending"))
            //        {
            //            ColorName = "yellow";
            //        }
            //        else if (Convert.ToString(items["Approver2"]).Trim().Contains("Approved"))
            //        {
            //            ColorName = "green";
            //        }
            //        else { ColorName = "white"; }
            //        strbuild.Append("<td style='border: 1px solid black;background-color:" + ColorName + ";'>" + Convert.ToString(items["Approver2"]).Trim() + "</td>");
            //        if (Convert.ToString(items["Approver3"]).Trim().Contains("Pending"))
            //        {
            //            ColorName = "yellow";
            //        }
            //        else if (Convert.ToString(items["Approver3"]).Trim().Contains("Approved"))
            //        {
            //            ColorName = "green";
            //        }
            //        else { ColorName = "white"; }
            //        strbuild.Append("<td style='border: 1px solid black;background-color:" + ColorName + ";'>" + Convert.ToString(items["Approver3"]).Trim() + "</td>");
            //        if (Convert.ToString(items["Approver4"]).Trim().Contains("Pending"))
            //        {
            //            ColorName = "yellow";
            //        }
            //        else if (Convert.ToString(items["Approver4"]).Trim().Contains("Approved"))
            //        {
            //            ColorName = "green";
            //        }
            //        else { ColorName = "white"; }
            //        strbuild.Append("<td style='border: 1px solid black;background-color:" + ColorName + ";'>" + Convert.ToString(items["Approver4"]).Trim() + "</td>");
            //        strbuild.Append("</tr>");

            //        var subject = "OneHR :- PO/WO Pending for approval";
            //        spm.sendMail_VSCB(strTOEmail, subject, Convert.ToString(strbuild).Trim(), "", ccMailIDs);
            //    }

            //    lstCostCenter.SelectedIndex = -1;
            //    lstPOWONo.SelectedIndex = -1;
            //    lstVendorName.SelectedIndex = -1;
            //}
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ASD", "StopLoader();", true);
            Response.Redirect("VSCB_POApproval_Status.aspx");

        }
        catch (Exception ex)
        {

        }
    }



    #endregion

    protected void lstPOWONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reports();
    }

    protected void lstVendorName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reports();
    }

    protected void lstCostCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reports();
    }
}