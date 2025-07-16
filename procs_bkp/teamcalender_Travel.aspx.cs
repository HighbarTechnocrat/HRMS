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

public partial class teamcalender_Travel : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() == "")
        {
            Response.Redirect("login.aspx");
        }

        if (!Page.IsPostBack)
        {
            hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();
            //getUserList_1();
        }

    }


    private void getUserList_1()
    {
        try
        {
            if (Convert.ToString(hdnloginempcode.Value).Trim() != "")
            {
                string[] strdate;
                string strfromDate = "";
                #region date formatting
                if (Convert.ToString(txtRequest_Date.Text).Trim() != "")
                {
                    strdate = Convert.ToString(txtRequest_Date.Text).Trim().Split('/');

                    //strfromDate = Convert.ToString(strdate[2]) + "-" +
                    strfromDate = Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]) + "-01";
                }

                #endregion

                DataSet dsemployees = new DataSet();
                dsemployees = spm.GetTeamCalenderTravelReqstList(hdnloginempcode.Value, strfromDate);
                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.LocalReport.DataSources.Clear();
                // Create Report DataSource
                ReportDataSource rds = new ReportDataSource("DataSet1", dsemployees.Tables[0]);

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/TeamCalender_trvl.rdlc");
                ReportViewer1.LocalReport.DataSources.Add(rds);


                #region Create payment Voucher PDF file
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                DataTable DataTable1 = new DataTable();
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                byte[] bytes = ReportViewer1.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=TeamCalenderDetails." + extension);
                try
                {
                    Response.BinaryWrite(bytes);
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error while generating Excel.');", true);
                    Console.WriteLine(ex.StackTrace);
                }

                #endregion

            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        getUserList_1();
    }
}