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

public partial class procs_VSCB_POApproval_Status : System.Web.UI.Page
{
	#region Creative_Default_methods
	SqlConnection source;
	public SqlDataAdapter sqladp;
	public DataTable dtPOWONo;
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
		Response.Redirect(ReturnUrl("sitepathmain") + "procs/Reembursementindex");
	}

	#endregion
	#region PageEvent
	protected void Page_Load(object sender, EventArgs e)
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
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
			}
			else
			{
				// Page.SmartNavigation = true;
				hdnEmpCode.Value = Session["Empcode"].ToString();
				if (!Page.IsPostBack)
				{

					if (Request.QueryString.Count > 0)
					{
						hdnInboxType.Value = Convert.ToString(Request.QueryString[0]).Trim();
					}
                    get_ApprovalStatusReport_DropdownList();

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
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
		
		lstPOWONo.SelectedIndex = -1;
		lstVendorName.SelectedIndex = -1;
		lstCostCentre.SelectedIndex = -1;
		//lstStatus.SelectedIndex = -1;
		lblmessage.Text = "";
        txtFromdate.Text = "";
        txtToDate.Text = "";
        Reports();

    }
    #endregion
    #region PageMethod

    private void get_ApprovalStatusReport_DropdownList()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        if (hdnEmpCode.Value == "99999999")
        {
            spars[0].Value = "get_POWOApprovalStatusreport_Dropdown_List_Account";
        }
        else
        {
            spars[0].Value = "get_POWOApprovalStatusreport_Dropdown_List";
        }


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
            lstCostCentre.DataSource = dsList.Tables[2];
            lstCostCentre.DataTextField = "CostCentre";
            lstCostCentre.DataValueField = "Dept_ID";
            lstCostCentre.DataBind();
        }
        lstCostCentre.Items.Insert(0, new ListItem("Select Cost Center", "0"));

        if (dsList.Tables[3].Rows.Count > 0)
        {
            mobile_cancel.Visible = true;
        }

    }

    private void Reports()
    {
        DataSet dsApprovalStatusReport = new DataSet();

        #region get Approval Status Report

        string[] strInvdate;
        string strfromDate = "";
        string strToDate = "";

        SqlParameter[] spars = new SqlParameter[7];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_POWO_Approval_Report";

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
        if (Convert.ToString(lstCostCentre.SelectedValue).Trim() != "0")
            spars[4].Value = Convert.ToInt32(lstCostCentre.SelectedValue);
        else
            spars[4].Value = DBNull.Value;

        spars[5] = new SqlParameter("@FromDate", SqlDbType.VarChar);
        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            strInvdate = Convert.ToString(txtFromdate.Text).Trim().Split('-');
            strfromDate = Convert.ToString(strInvdate[2]) + "-" + Convert.ToString(strInvdate[1]) + "-" + Convert.ToString(strInvdate[0]);
            spars[5].Value = strfromDate;
        }
        else
        {
            spars[5].Value = DBNull.Value;
        }
        spars[6] = new SqlParameter("@ToDate", SqlDbType.VarChar);
        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            strInvdate = Convert.ToString(txtToDate.Text).Trim().Split('-');
            strToDate = Convert.ToString(strInvdate[2]) + "-" + Convert.ToString(strInvdate[1]) + "-" + Convert.ToString(strInvdate[0]);

            spars[6].Value = strToDate;
        }
        else
        {
            spars[6].Value = DBNull.Value;
        }

        dsApprovalStatusReport = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");

        if (dsApprovalStatusReport.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsApprovalStatusReport.Tables[0].Rows.Count; i++)
            {
                string strPOID = dsApprovalStatusReport.Tables[0].Rows[i]["POID"].ToString();

                if (dsApprovalStatusReport.Tables[1].Rows.Count > 0)
                {
                    DataRow[] dr = dsApprovalStatusReport.Tables[1].Select("POID = '"+ Convert.ToInt32(strPOID) + "'");
                    if (dr.Length == 0)
                    {
                    }
                    if (dr.Length == 1)
                    {
                        dsApprovalStatusReport.Tables[0].Rows[i]["Approver1"] = dr[0].ItemArray[6];
                    }
                    if (dr.Length == 2)
                    {
                        dsApprovalStatusReport.Tables[0].Rows[i]["Approver1"] = dr[0].ItemArray[6];
                        dsApprovalStatusReport.Tables[0].Rows[i]["Approver2"] = dr[1].ItemArray[6];
                    }
                    if (dr.Length == 3)
                    {
                        dsApprovalStatusReport.Tables[0].Rows[i]["Approver1"] = dr[0].ItemArray[6];
                        dsApprovalStatusReport.Tables[0].Rows[i]["Approver2"] = dr[1].ItemArray[6];
                        dsApprovalStatusReport.Tables[0].Rows[i]["Approver3"] = dr[2].ItemArray[6];
                    }
                    if (dr.Length == 4)
                    {
                        dsApprovalStatusReport.Tables[0].Rows[i]["Approver1"] = dr[0].ItemArray[6];
                        dsApprovalStatusReport.Tables[0].Rows[i]["Approver2"] = dr[1].ItemArray[6];
                        dsApprovalStatusReport.Tables[0].Rows[i]["Approver3"] = dr[2].ItemArray[6];
                        dsApprovalStatusReport.Tables[0].Rows[i]["Approver4"] = dr[3].ItemArray[6];
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
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/Vscb_POWO_ApprovalStatus.rdlc");
            ReportDataSource rds = new ReportDataSource("POWO_Approval_Status", dsApprovalStatusReport.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.Refresh();
        }
        catch (Exception ex)
        {

        }
    }


    #endregion

    protected void mobile_cancel_Click(object sender, EventArgs e)
    {

    }

    protected void lstPOWONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reports();
    }

    protected void lstVendorName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reports();
    }

    protected void lstCostCentre_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reports();
    }
}