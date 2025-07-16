using ClosedXML.Excel;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;

public partial class Upload_Holiday : System.Web.UI.Page
{

    #region CreativeMethods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    #endregion

    SP_Methods spm = new SP_Methods();
    DataSet dspaymentVoucher_Apprs = new DataSet();
    String CEOInList = "N";
    double YearlymobileAmount = 0;
    #region PageEvents
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            //mobile_btnPrintPV.Visible = false;

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Upload_Holidaylist.aspx");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    hdnempcode.Value = Convert.ToString(Session["Empcode"]);
                    hdnTaskRefID.Value = "0"; 
                    lbl_Upload_Error.Text = "";
                    if (check_HR_Team() == false)
                    {
                        Response.Redirect("ReportsMenu.aspx");
                    }
                }

              
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    #endregion

    protected void upload_holiday_list(object sender, EventArgs e)
    {
        try
        {
            if (!uplTaskTemplate.HasFile)
            {
                lbl_Upload_Error.Text = "Please upload the template.";
                return;
            }
            string filename = uplTaskTemplate.FileName;
            string POWOFilePath = Server.MapPath(ConfigurationManager.AppSettings["Holiday_list"].Trim() + "/");

            if (!Directory.Exists(POWOFilePath))
            {
                Directory.CreateDirectory(POWOFilePath);
            }

            string fileExtension = Path.GetExtension(filename);
            string timestamp = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
            filename = "Holiday_List_" + timestamp + fileExtension;
            string filePath = Path.Combine(POWOFilePath, filename);

            uplTaskTemplate.SaveAs(filePath);

            DataTable dt = new DataTable();
            using (XLWorkbook workbook = new XLWorkbook(filePath))
            {
                IXLWorksheet worksheet = workbook.Worksheet(1);
                bool firstRow = true;
                string invalidCharsPattern = @"[#%&{}\!/<'>@+*?$,.|=]";
                foreach (IXLRow row in worksheet.RowsUsed())
                {
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.CellsUsed())
                        {
                            string columnName = cell.Value.ToString();
                            if (Regex.IsMatch(columnName, invalidCharsPattern))
                            {
                                lbl_Upload_Error.Text = "Invalid character found in column name: '" + columnName + "'";
                                return;
                            }
                            dt.Columns.Add(columnName);
                        }
                        firstRow = false;
                    }
                    else
                    {
                        dt.Rows.Add();
                        int cellIndex = 0;

                        foreach (IXLCell cell in row.CellsUsed())
                        {
                            string cellValue = cell.Value.ToString();

                            if (Regex.IsMatch(cellValue, invalidCharsPattern))
                            {
                                lbl_Upload_Error.Text = "Invalid character found in data: '" + cellValue + "'";
                                return;
                            }

                            dt.Rows[dt.Rows.Count - 1][cellIndex] = cellValue;
                            cellIndex++;

                            DataSet dsdate = new DataSet();
                            SqlParameter[] spars = new SqlParameter[2];

                            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                            spars[0].Value = "check_location";

                            cellValue = row.Cell(4).GetValue<string>().Trim();
                            spars[1] = new SqlParameter("@comp_code", SqlDbType.VarChar);
                            spars[1].Value = cellValue;


                            dsdate = spm.getDatasetList(spars, "SP_Admin_HolidayList");

                            if (dsdate == null || dsdate.Tables.Count == 0 || dsdate.Tables[0].Rows.Count == 0)
                            {
                                lbl_Upload_Error.Text = "The location is deactive: '" + cellValue + "'";
                                return;
                            }
                             
                            DataSet dsdl = new DataSet();
                            SqlParameter[] sparss = new SqlParameter[3]; 
                            sparss[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                            sparss[0].Value = "check_location_date_exist"; 
                            string locationValue = row.Cell(4).GetValue<string>().Trim();
                            sparss[1] = new SqlParameter("@comp_code", SqlDbType.VarChar);
                            sparss[1].Value = locationValue;
                            string dateValue = row.Cell(1).GetValue<DateTime>().ToString("yyyy-MM-dd"); 
                            sparss[2] = new SqlParameter("@holidayDate", SqlDbType.VarChar);
                            sparss[2].Value = dateValue; 
                            dsdl = spm.getDatasetList(sparss, "SP_Admin_HolidayList"); 
                            if (dsdl != null && dsdl.Tables.Count > 0 && dsdl.Tables[0].Rows.Count > 0)
                            {
                                // lbl_Upload_Error.Text = "The location and date already exist: '" + locationValue + " - " + dateValue + "'";
                                lbl_Upload_Error.Text = "Holiday already uploaded for this location :-'" + locationValue +"'";
                                return;
                            }


                        }

                    }
                }
            }
            if (dt.Rows.Count > 0)
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconnection"].ToString()))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();
                    int successCount = 0, failCount = 0;

                    try
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            try
                            {
                                using (SqlCommand cmd = new SqlCommand("SP_Admin_HolidayList", connection, transaction))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@qtype", "Insert_Holiday_list");

                                    DateTime holidayDate;
                                    if (DateTime.TryParse(row["Holiday_Date"].ToString(), out holidayDate))
                                    {
                                        cmd.Parameters.AddWithValue("@holidayDate", holidayDate.ToString("yyyy-MM-dd"));
                                    }
                                    else
                                    { 
                                        failCount++;
                                        continue;
                                    }
                                    string strUsername = Page.User.Identity.Name;
                                    cmd.Parameters.AddWithValue("@HolidayName", row["Holiday_Name"].ToString());
                                    cmd.Parameters.AddWithValue("@Weekday", row["Weekday"].ToString());
                                    cmd.Parameters.AddWithValue("@LocationCode", row["Location"].ToString());
                                    cmd.Parameters.AddWithValue("@Emp_Code", strUsername);

                                    cmd.ExecuteNonQuery();
                                    successCount++;  // Count successful inserts
                                }
                            }
                            catch (Exception rowEx)
                            {
                                failCount++;  
                            }
                        }

                        transaction.Commit(); 
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        lbl_Upload_Error.Text = "Transaction Error: " + ex.Message;
                    }
                }

                lbl_Upload_Error.Text = "Data uploaded successfully.";
            }
            else
            {
                lbl_Upload_Error.Text = "The Excel has no data.";
            }
        }
        catch (Exception ex)
        {
            lbl_Upload_Error.Text = "Error: " + ex.Message;
        }
    }

    protected void DownloadExcel_Click(object sender, EventArgs e)
    {
        string filePath = Server.MapPath("~/Holiday_list/Holiday_List_Template.xlsx"); // Ensure correct file path

        if (!System.IO.File.Exists(filePath))
        {
            Response.Write("File not found.");
            Response.End();
            return;
        }

        // Read the file into a byte array
        byte[] fileContent = System.IO.File.ReadAllBytes(filePath);

        // Send the file to the client
        Response.Clear();
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("Content-Disposition", "attachment; filename=Holiday_List_Template.xlsx");
        Response.BinaryWrite(fileContent);
        Response.Flush();
        Response.End(); // Use Response.CompleteRequest() in newer versions
    }

    public Boolean check_HR_Team()
    {
        Boolean isvalid = false;

        DataSet dsLocations = new DataSet();

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "check_HR_Team";

        spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();

        dsLocations = spm.getDatasetList(spars, "SP_Admin_HolidayList");

        if (dsLocations != null)
        {
            if (dsLocations.Tables[0].Rows.Count > 0)
            {
                isvalid = true;
            }
        }
        return isvalid;

    }

}