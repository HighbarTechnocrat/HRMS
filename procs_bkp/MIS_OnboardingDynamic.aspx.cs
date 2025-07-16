using ClosedXML.Excel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_MIS_OnboardingDynamic : System.Web.UI.Page
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
            hflEmpCode.Value = Convert.ToString(Session["Empcode"]).Trim();
            txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            txtFromdate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
            txtToDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            txtToDate.Attributes.Add("onkeyDown", "return noanyCharecters(event);");


        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "Please select from date";
            return;
        }
        // getemployee_ReimbursmentDetails();

        string[] strdate;
        string strfromDate = "";
        string strToDate = "";

        strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
        strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

        DataSet dsDates = new DataSet();
        SqlParameter[] spars = new SqlParameter[4];
        spars[0] = new SqlParameter("@from_date", SqlDbType.VarChar);
        spars[0].Value = strfromDate;

        spars[1] = new SqlParameter("@to_date", SqlDbType.VarChar);
        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            spars[1].Value = strToDate;
        }
        else
        {
            spars[1].Value = DBNull.Value;
        }

        spars[2] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[2].Value = "getLastyears";

        dsDates = spm.getDatasetList(spars, "rpt_MISReport");

        if (dsDates != null)
        {
            if (dsDates.Tables[0].Rows.Count > 0)
            {
                for (Int32 irow = 0; irow < dsDates.Tables[0].Rows.Count; irow++)
                {
                    string sfromdate_report = Convert.ToString(dsDates.Tables[0].Rows[irow]["FromDate_Report"]).Trim();
                    string stodate_report = Convert.ToString(dsDates.Tables[0].Rows[irow]["ToDate_Report"]).Trim();

                    getemployee_ReimbursmentDetails(Convert.ToString(dsDates.Tables[0].Rows[irow]["FromDate"]).Trim(), Convert.ToString(dsDates.Tables[0].Rows[irow]["ToDate"]).Trim(), irow + 1, sfromdate_report, stodate_report);
                }
            }
        }




    }


    protected void Lnk_Export_Click(object sender, EventArgs e)
    {

        using (XLWorkbook wb = new XLWorkbook())
        {
            DataTable dt1 = ViewState["OnboardingData1"] as DataTable;
            DataTable dt2 = ViewState["OnboardingData2"] as DataTable;
            DataTable dt3 = ViewState["OnboardingData3"] as DataTable;

            //if (ViewState["OnboardingData1"] is DataTable dt1)
            wb.Worksheets.Add(dt1, "Year 1");
          //  if (ViewState["OnboardingData2"] is DataTable dt2)
                wb.Worksheets.Add(dt2, "Year 2");
            //if (ViewState["OnboardingData3"] is DataTable dt3)
                wb.Worksheets.Add(dt3, "Year 3");

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=OnBoardingData.xlsx");

            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }


        //DataTable dt = ViewState["OnboardingData"] as DataTable;

        //using (XLWorkbook wb = new XLWorkbook())
        //{
        //    var ws = wb.Worksheets.Add(dt, "OnBoardingData");
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.Charset = "";
        //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    Response.AddHeader("content-disposition", "attachment;filename=OnBoardingData.xlsx");

        //    using (MemoryStream MyMemoryStream = new MemoryStream())
        //    {
        //        wb.SaveAs(MyMemoryStream);
        //        MyMemoryStream.WriteTo(Response.OutputStream);
        //        Response.Flush();
        //        Response.End();
        //    }
        //}
    }

    protected void GVOnboatdingData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string status = e.Row.Cells[0].Text.Trim();

            if (status == "Closing" || status == "Monthly Attrition Rate (%)")
            {
                e.Row.BackColor = System.Drawing.Color.LightBlue;
            }
        }
    }


    private void getemployee_ReimbursmentDetails(string sfromdate, string stodate, int gridviewNo, string sfrom_report,string stodate_report)
    {
        try
        {
            #region get employee Claim details
            DataSet dsempreimburstment = new DataSet();
            SqlParameter[] spars = new SqlParameter[43];

            spars[0] = new SqlParameter("@from_date", SqlDbType.VarChar);
            spars[0].Value = sfromdate;

            spars[1] = new SqlParameter("@to_date", SqlDbType.VarChar);
            spars[1].Value = stodate;

            spars[2] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[2].Value = "onBoardingDynamicReport";

            dsempreimburstment = spm.getDatasetList(spars, "rpt_MISReport");

            if (dsempreimburstment.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsempreimburstment.Tables[0].Rows)
                {
                    for (int i = 1; i < dsempreimburstment.Tables[0].Columns.Count; i++) // Assuming column 0 is 'Status'
                    {
                        if (dsempreimburstment.Tables[0].Columns[i].DataType == typeof(double) || dsempreimburstment.Tables[0].Columns[i].DataType == typeof(decimal))
                        {
                            string status = row["Status"].ToString();
                            // Convert specific statuses to integer format
                            if (status == "Opening" || status == "Separated" || status == "Onboarded" || status == "Closing" || status == "Resigned" || status == "Retained" || status == "SeparatedVoluntary" ||
                                status == "SeparatedInVoluntary" || status == "Yettobeseparatedinsystem")
                            {
                                row[i] = Convert.ToInt32(row[i]); // Convert to integer
                            }
                            else
                            {
                                row[i] = Convert.ToDouble(row[i]).ToString("0.00"); // Keep 2 decimal places
                            }

                            if (status == "BankRow1" || status == "BankRow2" || status == "BankRow3")
                            {
                                row[i] = DBNull.Value;
                            }

                            if (status == "Yettobeseparatedinsystem" )
                            {
                                if (Convert.ToInt32(row[i]) ==0)
                                row[i] = DBNull.Value;
                            }
                        }
                    }
                }

                foreach (DataRow row in dsempreimburstment.Tables[0].Rows)
                {
                    if (Convert.ToString(row["Status"]).Trim() == "BankRow1" || Convert.ToString(row["Status"]).Trim() == "BankRow2" || Convert.ToString(row["Status"]).Trim() == "BankRow3")
                    {
                        row["Status"] = "";
                    }

                    if (row["Status"] != DBNull.Value)
                    {

                        //row["Status"] = row["Status"].ToString().Replace("Opening", "Opening (A)");
                        //row["Status"] = row["Status"].ToString().Replace("Onboarded", "Onboarded (B)");
                        //row["Status"] = row["Status"].ToString().Replace("Separated", "Separated (C)");  
                        // row["Status"] = row["Status"].ToString().Replace("MonthlyAttritionRate", "Monthly Attrition Rate (%)");
                        // row["Status"] = row["Status"].ToString().Replace("YTDAttritionRate", "YTD Attrition Rate (%)");                             

                        if (Convert.ToString(row["Status"]).Trim() == "SeparatedVoluntary")
                        {
                            row["Status"] = "Separated Voluntary";
                        }
                        if (Convert.ToString(row["Status"]).Trim() == "SeparatedInVoluntary")
                        {
                            row["Status"] = "Separated InVoluntary";
                        }
                        //if (Convert.ToString(row["Status"]).Trim()=="VoluentaryResignation")
                        //{
                        //    row["Status"] = "voluntary Resignation";
                        //}

                        if (Convert.ToString(row["Status"]).Trim() == "Yettobeseparatedinsystem")
                        {
                            row["Status"] = "ALWP but Separation is Pending";
                        }

                        if (Convert.ToString(row["Status"]).Trim() == "AnnualisedOverallAttritionRate")
                        {
                            row["Status"] = "Annualised Overall Attrition Rate (%)";
                        }
                        if (Convert.ToString(row["Status"]).Trim() == "AnnualisedvoluntaryAttritionRate")
                        {
                            row["Status"] = "Annualised voluntary Attrition Rate (%)";
                        }
                        if (Convert.ToString(row["Status"]).Trim() == "AnnualisedOverallResignationRate")
                        {
                            row["Status"] = "Annualised Overall Resignation Rate (%)";
                        }
                        if (Convert.ToString(row["Status"]).Trim() == "ResignationRetentionRate")
                        {
                            row["Status"] = "Annualised Resignation Retention Rate (%) ";
                        }
                        if (Convert.ToString(row["Status"]).Trim() == "AnnualisedNetResignationRate")
                        {
                            row["Status"] = "Annualised Net Resignation Rate (%)";
                        }

                    }
                }



               // ViewState["OnboardingData"] = dsempreimburstment.Tables[0];

                 
                
                 


                if (gridviewNo == 1)
                {
                    lblGVOnboardingData_period.Text = "Onboarding Report For - "+ sfrom_report + " - " + stodate_report;
                    ViewState["OnboardingData1"] = dsempreimburstment.Tables[0];
                    GVOnboardingData.DataSource = dsempreimburstment.Tables[0];
                    GVOnboardingData.DataBind();
                }

                if (gridviewNo == 2)
                {
                    lblGVOnboardingData2_period.Text = "Onboarding Report For - " + sfrom_report + " - " + stodate_report;
                    ViewState["OnboardingData2"] = dsempreimburstment.Tables[0];
                    GVOnboardingData2.DataSource = dsempreimburstment.Tables[0];
                    GVOnboardingData2.DataBind();
                }


                if (gridviewNo == 3)
                {
                    lblGVOnboardingData3_period.Text = "Onboarding Report For - " + sfrom_report + " - " + stodate_report;
                    ViewState["OnboardingData3"] = dsempreimburstment.Tables[0];
                    GVOnboardingData3.DataSource = dsempreimburstment.Tables[0];
                    GVOnboardingData3.DataBind();
                }

                Lnk_Export.Visible = true;
            }

            #endregion

        }
        catch (Exception ex)
        {
        }
    }


    private void getemployee_ReimbursmentDetails()
    {
        try
        {
            #region get employee Claim details
            DataSet dsempreimburstment = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];
            string[] strdate;
            string strfromDate = "";
            string strToDate = "";
            string strfromDate_RPt = "";
            string strToDate_RPt = "";


            spars[0] = new SqlParameter("@from_date", SqlDbType.VarChar);
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                strfromDate_RPt = Convert.ToString(strdate[0]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[2]);
                spars[0].Value = strfromDate;
            }
            else
            {
                spars[0].Value = null;
            }
            //@toDate
            spars[1] = new SqlParameter("@to_date", SqlDbType.VarChar);
            if (Convert.ToString(txtToDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                strToDate_RPt = Convert.ToString(strdate[0]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[2]);
                spars[1].Value = strToDate;
            }
            else
            {
                strToDate = DateTime.Today.ToString("yyyy") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("dd");
                strToDate_RPt = DateTime.Today.ToString("dd") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("yyyy");
                spars[1].Value = strToDate;
            }
            spars[2] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[2].Value = "onBoardingDynamicReport";


            dsempreimburstment = spm.getDatasetList(spars, "rpt_dataProcedure");

            if (dsempreimburstment.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsempreimburstment.Tables[0].Rows)
                {
                    for (int i = 1; i < dsempreimburstment.Tables[0].Columns.Count; i++) // Assuming column 0 is 'Status'
                    {
                        if (dsempreimburstment.Tables[0].Columns[i].DataType == typeof(double) || dsempreimburstment.Tables[0].Columns[i].DataType == typeof(decimal))
                        {
                            string status = row["Status"].ToString();
                            // Convert specific statuses to integer format
                            if (status == "Opening" || status == "Separated" || status == "Onboarded" || status == "Closing" || status == "Resigned" || status == "Retained" || status == "SeparatedVoluntary" ||
                                status == "SeparatedInVoluntary" || status == "Yettobeseparatedinsystem")
                            {
                                row[i] = Convert.ToInt32(row[i]); // Convert to integer
                            }
                            else
                            {
                                row[i] = Convert.ToDouble(row[i]).ToString("0.00"); // Keep 2 decimal places
                            }

                            if (status == "BankRow1" || status == "BankRow2" || status == "BankRow3")
                            {
                                row[i] = DBNull.Value;
                            }
                        }
                    }
                }

                foreach (DataRow row in dsempreimburstment.Tables[0].Rows)
                {
                    if (Convert.ToString(row["Status"]).Trim() == "BankRow1" || Convert.ToString(row["Status"]).Trim() == "BankRow2" || Convert.ToString(row["Status"]).Trim() == "BankRow3")
                    {
                        row["Status"] = "";
                    }

                    if (row["Status"] != DBNull.Value)
                    {

                        //row["Status"] = row["Status"].ToString().Replace("Opening", "Opening (A)");
                        //row["Status"] = row["Status"].ToString().Replace("Onboarded", "Onboarded (B)");
                        //row["Status"] = row["Status"].ToString().Replace("Separated", "Separated (C)");  
                        // row["Status"] = row["Status"].ToString().Replace("MonthlyAttritionRate", "Monthly Attrition Rate (%)");
                        // row["Status"] = row["Status"].ToString().Replace("YTDAttritionRate", "YTD Attrition Rate (%)");                             

                        if (Convert.ToString(row["Status"]).Trim() == "SeparatedVoluntary")
                        {
                            row["Status"] = "Separated Voluntary";
                        }
                        if (Convert.ToString(row["Status"]).Trim() == "SeparatedInVoluntary")
                        {
                            row["Status"] = "Separated InVoluntary";
                        }
                        //if (Convert.ToString(row["Status"]).Trim()=="VoluentaryResignation")
                        //{
                        //    row["Status"] = "voluntary Resignation";
                        //}

                        if (Convert.ToString(row["Status"]).Trim() == "Yettobeseparatedinsystem")
                        {
                            row["Status"] = "ALWP but Separation is Pending";
                        }

                        if (Convert.ToString(row["Status"]).Trim() == "AnnualisedOverallAttritionRate")
                        {
                            row["Status"] = "Annualised Overall Attrition Rate (%)";
                        }
                        if (Convert.ToString(row["Status"]).Trim() == "AnnualisedvoluntaryAttritionRate")
                        {
                            row["Status"] = "Annualised voluntary Attrition Rate (%)";
                        }
                        if (Convert.ToString(row["Status"]).Trim() == "AnnualisedOverallResignationRate")
                        {
                            row["Status"] = "Annualised Overall Resignation Rate (%)";
                        }
                        if (Convert.ToString(row["Status"]).Trim() == "ResignationRetentionRate")
                        {
                            row["Status"] = "Resignation Retention Rate (%)";
                        }
                        if (Convert.ToString(row["Status"]).Trim() == "AnnualisedNetResignationRate")
                        {
                            row["Status"] = "Annualised Net Resignation Rate (%)";
                        }

                    }
                }



                ViewState["OnboardingData"] = dsempreimburstment.Tables[0];
                GVOnboardingData.DataSource = dsempreimburstment.Tables[0];
                GVOnboardingData.DataBind();
                Lnk_Export.Visible = true;
            }

            #endregion

        }
        catch (Exception ex)
        {
        }
    }
}