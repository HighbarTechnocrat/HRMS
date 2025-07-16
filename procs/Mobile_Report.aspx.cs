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

public partial class myaccount_MobilereimbursmentReport : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
        }
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
            #region get employee Reimbursment details
            DataSet dsempreimburstment = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

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


            spars[3] = new SqlParameter("@month", SqlDbType.VarChar);
            if (Convert.ToString(txtMonth.Text).Trim() == "")
                spars[3].Value = DBNull.Value;
            else
                spars[3].Value = txtMonth.Text.Trim();

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
                param[4] = new ReportParameter("pMobileClaimhead", Convert.ToString("Mobile Claim details"));
            else
                param[4] = new ReportParameter("pMobileClaimhead", Convert.ToString("No Mobile Claims during the year"));

            if (dsempreimburstment.Tables[2].Rows.Count > 0)
                param[5] = new ReportParameter("pFuelClaimhead", Convert.ToString("Fuel Claim details"));
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
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/Mobile_reimbursment.rdlc");
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

    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        getemployee_ReimbursmentDetails();

    }


}