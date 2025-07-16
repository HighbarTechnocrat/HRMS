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

public partial class myaccount_PVreimbursmentReport : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
        }
        lblmessage.Text = "";
        if (!Page.IsPostBack)
        {
            hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();
            //hdnloginempcode.Value = "00630134";        
        }

    }

    private void getemployee_ReimbursmentDetails()
    {
        try
        {
            #region get employee Claim details
            DataSet dsempreimburstment = new DataSet();
            SqlParameter[] spars = new SqlParameter[6];
            string[] strdate;
            string strfromDate = "";
            string strToDate = "";

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Allemp_paymntmbilefuel_reimbursmnt";
            

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hdnloginempcode.Value);

            spars[2] = new SqlParameter("@emp_name", SqlDbType.VarChar);
            if (Convert.ToString(txtemp.Text).Trim() != "")
            {
                spars[2].Value = Convert.ToString(txtemp.Text.ToLower());
            }
            else
                spars[2].Value = null;

            spars[3] = new SqlParameter("@Request_status", SqlDbType.VarChar);
            if (Convert.ToString(lstFromfor.SelectedValue).Trim() != "")
            {
                spars[3].Value = Convert.ToString(lstFromfor.SelectedValue);
            }
            else
                spars[3].Value = null;


            spars[4] = new SqlParameter("@from_date", SqlDbType.VarChar);
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                spars[4].Value = strfromDate;
               
            }
            else
                 spars[4].Value = null;

            spars[5] = new SqlParameter("@to_date", SqlDbType.VarChar);
            if (Convert.ToString(txtToDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

                spars[5].Value = strToDate;
            }
            else
            {
                //strToDate = Convert.ToString("1900") + "-" + Convert.ToString("01") + "-" + Convert.ToString("01");
                strToDate = DateTime.Today.ToString("yyyy") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("dd");
                spars[5].Value = strToDate;
                //spars[5].Value = null;
            }
            dsempreimburstment = spm.getDatasetList(spars, "rpt_dataProcedure");

            #endregion

            ReportParameter[] param = new ReportParameter[6];
            if (dsempreimburstment.Tables[3].Rows.Count > 0)
                param[0] = new ReportParameter("pyear", Convert.ToString(dsempreimburstment.Tables[3].Rows[0]["pyear"]));
            else
                param[0] = new ReportParameter("pyear","2018");

            if (dsempreimburstment.Tables[4].Rows.Count > 0)
            {
                param[1] = new ReportParameter("pempname", Convert.ToString(dsempreimburstment.Tables[4].Rows[0]["Emp_Name"]));
                param[2] = new ReportParameter("pempcode", Convert.ToString(dsempreimburstment.Tables[4].Rows[0]["Emp_Code"]));
            }
            if (dsempreimburstment.Tables[0].Rows.Count > 0)
                param[3] = new ReportParameter("pPaymentClaimhead", Convert.ToString("Payment Voucher Claim details"));
            else
                param[3] = new ReportParameter("pPaymentClaimhead", Convert.ToString("No Payment Voucher during the year"));

            if (dsempreimburstment.Tables[1].Rows.Count > 0)
                param[4] = new ReportParameter("pMobileClaimhead", Convert.ToString("Mobile Claims Claim details"));
            else
                param[4] = new ReportParameter("pMobileClaimhead", Convert.ToString("No Mobile Claims during the year"));

            if (dsempreimburstment.Tables[2].Rows.Count > 0)
                param[5] = new ReportParameter("pFuelClaimhead", Convert.ToString("Fuel Claim Claim details"));
            else
                param[5] = new ReportParameter("pFuelClaimhead", Convert.ToString("No Fuel Claims during the year"));

            ReportViewer2.LocalReport.Refresh();
            ReportViewer2.LocalReport.DataSources.Clear();

            if (dsempreimburstment.Tables[3].Rows.Count > 0)
            {
                // Create Report DataSource :- for payment Voucher
                ReportDataSource rdspayment = new ReportDataSource("dspaymnt_reimbursmnt", dsempreimburstment.Tables[0]);
                ReportDataSource rdsmobile = new ReportDataSource("ds_MobileRemReport", dsempreimburstment.Tables[1]);
                ReportDataSource rdsFuel = new ReportDataSource("dsfuel_reimbursmnt", dsempreimburstment.Tables[2]);
                ReportViewer2.ProcessingMode = ProcessingMode.Local;
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/PaymentVoucher_reimbursment.rdlc");
                
                ReportViewer2.LocalReport.DataSources.Add(rdspayment);
                ReportViewer2.LocalReport.DataSources.Add(rdsmobile);
                ReportViewer2.LocalReport.DataSources.Add(rdsFuel);
            }

            ReportViewer2.LocalReport.SetParameters(param);

        }
        catch (Exception ex)
        {
        }
    }

     public void Compare()
    {
        
    }
    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";

        if ((Convert.ToString(txtFromdate.Text).Trim() != "") && (Convert.ToString(txtToDate.Text).Trim() != ""))
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

            strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);


            DateTime startDate = Convert.ToDateTime(strfromDate);
            DateTime endDate = Convert.ToDateTime(strToDate);
            if (startDate > endDate)
            {
                lblmessage.Text = "From Date should be less than To Date ";
                txtFromdate.Text = "";

                return;
            }
            else
            {
                lblmessage.Text = "";
                // ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Start date should be greate than End date')", true);
            }
        }
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";

        if ((Convert.ToString(txtFromdate.Text).Trim() != "") && (Convert.ToString(txtToDate.Text).Trim() != ""))
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

            strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);


            DateTime startDate = Convert.ToDateTime(strfromDate);
            DateTime endDate = Convert.ToDateTime(strToDate);
            if (startDate > endDate)
            {
                lblmessage.Text = "To Date should be greater than From Date ";
                txtToDate.Text = "";
                
                return;
            }
            else
            {
                // ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Start date should be greate than End date')", true);
            }
        }
    }

    protected void lstFromfor_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtClaimStatus.Text = lstFromfor.SelectedValue;        
        PopupControlExtender1.Commit(lstFromfor.SelectedValue);
       

    
    
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        getemployee_ReimbursmentDetails();

    }


}