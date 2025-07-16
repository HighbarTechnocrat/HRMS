using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;

public partial class myaccount_FuelReimbMonthlyReport : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    
    public static string con = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
        }
        if (!Page.IsPostBack)
        {
            hdnloginempcode.Value = Convert.ToString(Session["Empcode"]).Trim();           

        }
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetEmployee(string prefixText)
    {

        DataSet dt = new DataSet();
        DataTable Result = new DataTable();
        string str = "select Emp_Name from tbl_Employee_Mst where emp_km_range is not null and Emp_Name like '" + prefixText + "%'";
        SqlDataAdapter da = new SqlDataAdapter(str, con);        
        da.Fill(dt);
        List<string> Output = new List<string>();
        for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
            Output.Add(dt.Tables[0].Rows[i][0].ToString());
        return Output;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {

            DataSet dsempreimburstment = new DataSet();
           
                SqlParameter[] spars = new SqlParameter[3];

                spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                spars[0].Value = "GetFuelReimbMonthlyRep";
				
				spars[1] = new SqlParameter("@emp_name", SqlDbType.VarChar);
                if (Convert.ToString(txtemp.Text).Trim() != "")
                    spars[1].Value = txtemp.Text.Trim();
                else
                    spars[1].Value = DBNull.Value;

                spars[2] = new SqlParameter("@month", SqlDbType.VarChar);
                if (Convert.ToString(txtMonth.Text).Trim() == "")
                spars[2].Value = DBNull.Value ;
                else
                 spars[2].Value = txtMonth.Text.Trim();


                dsempreimburstment = spm.getDatasetList(spars, "rpt_dataProcedure");

           



            #region Report Viewer
            ReportViewer2.LocalReport.Refresh();
            ReportViewer2.LocalReport.DataSources.Clear();

//if (dsempreimburstment.Tables[0].Rows.Count > 0)
//{

                ReportDataSource rdspayment = new ReportDataSource("dsFuel_ReimnbMonthly", dsempreimburstment.Tables[0]);

                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/procs/Fuel_ReimbMonthlyRep.rdlc");
                ReportViewer2.LocalReport.DataSources.Add(rdspayment);

           // }

            ReportViewer2.LocalReport.Refresh();

            // ReportViewer2.LocalReport.SetParameters(param);

            #endregion
        }
        catch (Exception ex)
        {
            
           
        }


        
    }

}