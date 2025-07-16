using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ABAP_Object_Tracker_Upload : System.Web.UI.Page
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/ABAP_Object_Tracker_Index.aspx");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    hdnempcode.Value = Convert.ToString(Session["Empcode"]);
                    hdnTaskRefID.Value = "0";
                    getProjectLocation();
                    BindData("All");
                    lbl_Upload_Error.Text = "";
                }
                else
                {

                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    #endregion

    #region PageMethods
    private void BindData(string qtype)
    {
        try
        {
            var empCode = Convert.ToString(Session["Empcode"]);
            var getVal = Convert.ToDouble(hdnTaskRefID.Value);
            var getResult = spm.getTaskMonitoringDDL(empCode, getVal, "GetAllDDL_TEMP_Create");
            if (getResult != null)
            {
                if (getResult.Tables.Count > 0)
                {
                    if (qtype == "All")
                    {
                        var getMeetingType = getResult.Tables[0];
                        var getTask_M_Reference = getResult.Tables[1];
                        var getAttendeeDDL = getResult.Tables[2];
                        var getSupervisorDDL = getResult.Tables[6];
                        var getTaskMR_Details = getResult.Tables[3];
                        if (getTaskMR_Details.Rows.Count > 0)
                        {
                            hdnTaskRefID.Value = Convert.ToString(getTaskMR_Details.Rows[0]["ID"]);

                        }

                    }
                    else
                    {
                        //hdnTaskRefID.Value = "0";
                    }

                }
            }

        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private void getProjectLocation()
    {
        try
        {
            var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();

            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getLocationMaster";

            spars[1] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
            spars[1].Value = getCreatedBy;

            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");

            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                DDLProjectLocation.DataSource = DS.Tables[0];
                DDLProjectLocation.DataTextField = "Location_name";
                DDLProjectLocation.DataValueField = "comp_code";
                DDLProjectLocation.DataBind();
                DDLProjectLocation.Items.Insert(0, new ListItem("Select Project Location", "0"));
            }
            else
            {
                DDLProjectLocation.Items.Insert(0, new ListItem("Select Project Location", "0"));
                lbl_ProjectSelect_Error.Text = "The upload of templates is restricted to project managers only.";
                tsktemplate_btnSave.Enabled = false;
            }
        }
        catch (Exception ex)
        {

        }
    }
    #endregion

    #region ABAP Object Tracking 
    protected void AOTtemplate_btnSave_Click(object sender, EventArgs e)
    {
        lbl_ProjectSelect_Error.Text = "";
        lbl_Upload_Error.Text = "";

        try
        {
            if (Convert.ToString(DDLProjectLocation.SelectedValue).Trim() == "0" || Convert.ToString(DDLProjectLocation.SelectedValue).Trim() == "")
            {
                lbl_ProjectSelect_Error.Text = "Please select Project Location.";
                return;
            }
            if (!uplTaskTemplate.HasFile)
            {
                lbl_Upload_Error.Text = "Please upload the template.";
            }

            LinkButton btn = (LinkButton)sender;
            int objparam = Convert.ToInt32(btn.CommandArgument);

            DataTable dtTaskTemp = new DataTable();
            string Fulltext;
            if (uplTaskTemplate.HasFile)
            {
                string filename = uplTaskTemplate.FileName;
                string POWOFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ABAPObjectfiles"]).Trim() + "/");
                bool folderExists = Directory.Exists(POWOFilePath);
                if (!folderExists)
                {
                    Directory.CreateDirectory(POWOFilePath);
                }
                String InputFile = System.IO.Path.GetExtension(uplTaskTemplate.FileName);

                string str = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
                filename = "ABAPObjectTracker" + str + InputFile;
                uplTaskTemplate.SaveAs(Path.Combine(POWOFilePath, filename));
                string powoUplaodedFile = POWOFilePath + filename;
                string read = System.IO.Path.GetFullPath(powoUplaodedFile);
                bool hasEmptyValues = false;
                int columnIndexToCheck = 0;

                using (XLWorkbook workbook = new XLWorkbook(read))
                {
                    IXLWorksheet worksheet = workbook.Worksheet(1);

                    bool firstRow = true;
                    Dictionary<string, int> rowSet = new Dictionary<string, int>();
                    if (worksheet.RowsUsed().Count() > 1)
                    {
                        int rowIndex = 0;
                        int columnCount = worksheet.LastColumnUsed().ColumnNumber();
                        var headerRow1 = worksheet.Row(1);
                        var headerRow2 = worksheet.Row(2);
                        string[] header1Values = new string[columnCount];
                        for (int col = 1; col <= columnCount; col++)
                        {
                            if (worksheet.Cell(1, col).IsMerged())
                            {
                                var mergedRange = worksheet.Cell(1, col).MergedRange();
                                string headerValue = mergedRange.FirstCell().Address.ToString();
                                for (int mergedCol = col; mergedCol < col + mergedRange.ColumnCount(); mergedCol++)
                                {
                                    header1Values[mergedCol - 1] = headerValue;
                                }
                                col += mergedRange.ColumnCount() - 1;
                            }
                            else
                            {
                                header1Values[col - 1] = worksheet.Cell(1, col).Address.ToString();
                            }
                        }

                        for (int col = 1; col <= columnCount; col++)
                        {
                            string header = "";
                            if (!string.IsNullOrEmpty(headerRow2.Cell(col).Value.ToString()))
                            {
                                header = header1Values[col - 1] + "_" + headerRow2.Cell(col).Address.ToString();
                            }
                            else
                            {
                                header = header1Values[col - 1];
                            }
                            dtTaskTemp.Columns.Add(header);
                        }


                        for (int row = 3; row <= worksheet.LastRowUsed().RowNumber(); row++)
                        {
                            var dataRow = dtTaskTemp.NewRow();
                            bool isRowValid = true;
                            bool is27thColumnFilled = false; // Track if 27th column has value

                            for (int col = 1; col <= columnCount; col++)
                            {
                                string cellValue = worksheet.Cell(row, col).Value.ToString();

                                if (string.IsNullOrEmpty(cellValue))
                                {
                                    string columnHeader = "";

                                    string header1 = headerRow1.Cell(col).Value.ToString();
                                    string header2 = headerRow2.Cell(col).Value.ToString();

                                    string pattern = @"(\b\d{2}\.\d{2}\.\d{4}\b)";
                                    if (Regex.IsMatch(header2, pattern))
                                    {
                                        header2 = Regex.Replace(header2, pattern, "").Trim();
                                    }



                                    if (!string.IsNullOrEmpty(header2))
                                    {
                                        if (!string.IsNullOrEmpty(header1))
                                        {
                                            columnHeader = header1 + "_" + header2;
                                        }
                                        else
                                        {
                                            columnHeader = header2;
                                        }
                                    }
                                    else
                                    {
                                        columnHeader = header1;
                                    }

                                    if (col <= 26) 
                                    {
                                        lbl_Upload_Error.Text = columnHeader + "</b> is missing in row " + row + ".";
                                        return;
                                    }
                                    else if (col == 27) 
                                    {
                                        is27thColumnFilled = false;
                                    }
                                    else if (col > 27 && col <= 32 && is27thColumnFilled) 
                                    {
                                        lbl_Upload_Error.Text = columnHeader + "</b> is missing in row " + row + ".";
                                        return;
                                    }
                                }
                                else
                                {
                                    if (col == 27)
                                    {
                                        is27thColumnFilled = true;
                                    }
                                }

                                dataRow[col - 1] = cellValue;
                                //dataRow[col - 1] = worksheet.Cell(row, col).Value.ToString();
                            }

                            dtTaskTemp.Rows.Add(dataRow);
                        }
                    }
                    else
                    {
                        lbl_Upload_Error.Text = "The entire sheet is empty; no rows are available.";
                        return;
                    }
                }

                if (dtTaskTemp.Rows.Count > 0)
                {
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconnection"].ToString()))
                    {
                        connection.Open();
                        SqlTransaction transaction = connection.BeginTransaction();

                        int rowIndex1 = 1;
                        foreach (DataRow row in dtTaskTemp.Rows)
                        {
                            var ColB1 = row["B1"].ToString().Split('/');
                            var ColF1 = row["F1"].ToString().Split('/');
                            var ColH1 = row["H1"].ToString().Split('/');
                            var ColI1 = row["I1"].ToString().Split('/');
                            var ColJ1 = row["J1"].ToString().Split('/');
                            var ColQ1_Q2 = row["Q1_Q2"].ToString().Split('/');
                            var ColAA1_AB2 = row["AA1_AB2"].ToString();

                            //string ColB1_1 = Convert.ToString(ColB1[0]).Trim();
                            //string ColF1_1 = Convert.ToString(ColF1[0]).Trim();
                            //string ColH1_1 = Convert.ToString(ColH1[0]).Trim();
                            //string ColI1_1 = Convert.ToString(ColI1[0]).Trim();
                            //string ColJ1_1 = Convert.ToString(ColJ1[0]).Trim();
                            //string ColQ1_Q2_1 = Convert.ToString(ColQ1_Q2[0]).Trim();
                           // string ColAA1_AB2_1 = Convert.ToString(ColAA1_AB2[0]).Trim();

                            rowIndex1++;
                            //var data = GetMasterIdsBasedOnColumns("GetMasterIdsBasedOnColumns", Convert.ToString(ColB1[0]).Trim(), Convert.ToString(ColF1[0]).Trim(), Convert.ToString(ColH1[0]).Trim(), Convert.ToString(ColI1[0]).Trim(), Convert.ToString(ColJ1[1]).Trim(), Convert.ToString(ColQ1_Q2[1]).Trim(), Convert.ToString(ColAA1_AB2).Trim());
                            var data = GetMasterIdsBasedOnColumns("GetMasterIdsBasedOnColumns", Convert.ToString(ColB1[0]).Trim(), Convert.ToString(ColF1[0]).Trim(), Convert.ToString(ColH1[0]).Trim(), Convert.ToString(ColI1[0]).Trim(), Convert.ToString(ColJ1[0]).Trim(), Convert.ToString(ColQ1_Q2[0]).Trim(),"");

                            if (data != null && data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0)
                            {
                                // Check for ModuleId
                                if (string.IsNullOrEmpty(data.Tables[0].Rows[0]["ModuleId"].ToString()))
                                {
                                    lbl_Upload_Error.Text = "Selected module does not exist.";
                                    transaction.Rollback();
                                    return;
                                }

                                // Check for FCategoryId
                                if (string.IsNullOrEmpty(data.Tables[0].Rows[0]["FCategoryId"].ToString()))
                                {
                                    lbl_Upload_Error.Text = "Selected frice Category does not exist.";
                                    transaction.Rollback();
                                    return;
                                }

                                // Check for PriorityId
                                if (string.IsNullOrEmpty(data.Tables[0].Rows[0]["PriorityId"].ToString()))
                                {
                                    lbl_Upload_Error.Text = "Selected priority does not exist.";
                                    transaction.Rollback();
                                    return;
                                }

                                // Check for ComplexityId
                                if (string.IsNullOrEmpty(data.Tables[0].Rows[0]["ComplexityId"].ToString()))
                                {
                                    lbl_Upload_Error.Text = "Selected complexity does not exist.";
                                    transaction.Rollback();
                                    return;
                                }

                                // Check for FunctionalConsultant
                                if (string.IsNullOrEmpty(data.Tables[0].Rows[0]["FunctionalConsultant"].ToString()))
                                {
                                    lbl_Upload_Error.Text = "Selected functional consultant does not exist.";
                                    transaction.Rollback();
                                    return;
                                }

                                // Check for PlanABAPConsultant
                                if (string.IsNullOrEmpty(data.Tables[0].Rows[0]["PlanABAPConsultant"].ToString()))
                                {
                                    lbl_Upload_Error.Text = "Selected ABAP consultant does not exist.";
                                    transaction.Rollback();
                                    return;
                                }

                                //// Check for ResuableStatus
                                //if (string.IsNullOrEmpty(data.Tables[0].Rows[0]["ResuableStatus"].ToString()))
                                //{
                                //    lbl_Upload_Error.Text = "Selected Resuable Status does not exist.";
                                //    transaction.Rollback();
                                //    return;
                                //}
                            }
                        }


                        int ABAPODUploadId = 0;
                        int rowIndex = 1;
                        var ProjectLocation = Convert.ToString(DDLProjectLocation.SelectedValue).ToString();
                        var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();
                        var getStatus = InsertLocABAPODTracker("InsertLocABAPODTracker", ProjectLocation, getCreatedBy, filename, connection, transaction);
                        if (getStatus != null && getStatus.Tables[0] != null && getStatus.Tables.Count > 0)
                        {
                            if (getStatus.Tables[0].Rows[0]["ID"].ToString() == "0")
                            {
                                lbl_Upload_Error.Text = getStatus.Tables[0].Rows[0]["ResponseMsg"].ToString();
                                return;
                            }

                            var objABAPUploadId = Convert.ToInt32(getStatus.Tables[0].Rows[0]["ID"]);

                            try
                            {
                                foreach (DataRow row in dtTaskTemp.Rows)
                                {
                                    rowIndex++;
                                    ABAPODUploadId = ABAP_Object_Final_Submit(objparam, row, objABAPUploadId, connection, transaction);
                                }

                                transaction.Commit();
                                Response.Redirect("~/procs/ABAP_Object_Tracker_Submit_Plan.aspx?ABAPODId=" + ABAPODUploadId, false);
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                lbl_Upload_Error.Text = "Error during insertion: The statement has been terminated.";
                                return;
                            }
                            finally
                            {
                                connection.Close();
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lbl_Upload_Error.Text = ex.Message;
            return;
        }
    }

    protected int ABAP_Object_Final_Submit(int inputParam, DataRow dr, int objABAPUploadId, SqlConnection connection, SqlTransaction transaction)
    {
        #region Save ABAP Object Tracking Details Data into Main Table.
        var getSrNo = dr["A1"].ToString();
        var getInterface = dr["C1"].ToString();
        var getDevelopmentDescription = dr["D1"].ToString();
        var getScope = string.IsNullOrEmpty(dr["E1"].ToString()) ? lbl_Upload_Error.Text = "Scope is required" : dr["E1"].ToString();
        var getPriority = dr["G1"].ToString();

        var getRGSPlannedStartDate = string.IsNullOrEmpty(ParseDate(dr["K1_K2"].ToString())) ? "" : ParseDate(dr["K1_K2"].ToString());
        var getRGSPlannedFinishDate = string.IsNullOrEmpty(ParseDate(dr["K1_L2"].ToString())) ? "" : ParseDate(dr["K1_L2"].ToString());
        var getRGSPlannedSubmissionDate = string.IsNullOrEmpty(ParseDate(dr["K1_M2"].ToString())) ? "" : ParseDate(dr["K1_M2"].ToString());
        var getRGSPlannedApprovalDate = string.IsNullOrEmpty(ParseDate(dr["K1_N2"].ToString())) ? "" : ParseDate(dr["K1_N2"].ToString());
        var getFSPlannedStartDate = string.IsNullOrEmpty(ParseDate(dr["O1_O2"].ToString())) ? "" : ParseDate(dr["O1_O2"].ToString());
        var getFSPlannedFinishDate = string.IsNullOrEmpty(ParseDate(dr["O1_P2"].ToString())) ? "" : ParseDate(dr["O1_P2"].ToString());
        var getABAPDevDuration = string.IsNullOrEmpty(dr["Q1_R2"].ToString()) ? "0" : dr["Q1_R2"].ToString();
        var getABAPDevPlannedStartDate = string.IsNullOrEmpty(ParseDate(dr["Q1_S2"].ToString())) ? "" : ParseDate(dr["Q1_S2"].ToString());
        var getABAPDevPlannedFinishDate = string.IsNullOrEmpty(ParseDate(dr["Q1_T2"].ToString())) ? "" : ParseDate(dr["Q1_T2"].ToString());
        var getHBTTestPlannedStartDate = string.IsNullOrEmpty(ParseDate(dr["U1_U2"].ToString())) ? "" : ParseDate(dr["U1_U2"].ToString());
        var getHBTTestPlannedFinishDate = string.IsNullOrEmpty(ParseDate(dr["U1_V2"].ToString())) ? "" : ParseDate(dr["U1_V2"].ToString());
        var getCTMPlannedStartDate = string.IsNullOrEmpty(ParseDate(dr["W1_W2"].ToString())) ? "" : ParseDate(dr["W1_W2"].ToString());
        var getCTMPlannedFinishDate = string.IsNullOrEmpty(ParseDate(dr["W1_X2"].ToString())) ? "" : ParseDate(dr["W1_X2"].ToString());
        var getUATSignOffPlannedDate = string.IsNullOrEmpty(ParseDate(dr["Y1_Y2"].ToString())) ? "" : ParseDate(dr["Y1_Y2"].ToString());
        var getGoLivePlannedDate = string.IsNullOrEmpty(ParseDate(dr["Z1"].ToString())) ? "" : ParseDate(dr["Z1"].ToString());
        var getReusableClientName = string.IsNullOrEmpty(dr["AA1_AA2"].ToString()) ? "" : dr["AA1_AA2"].ToString();
        var getAdditionalEfforts = string.IsNullOrEmpty(dr["AA1_AC2"].ToString()) ? "" : dr["AA1_AC2"].ToString();
        var getpercentage = string.IsNullOrEmpty(dr["AA1_AD2"].ToString()) ? "" : dr["AA1_AD2"].ToString();
        var getReusableRemarks = string.IsNullOrEmpty(dr["AA1_AE2"].ToString()) ? "" : dr["AA1_AE2"].ToString();
        var getTcode = string.IsNullOrEmpty(dr["AA1_AF2"].ToString()) ? "" : dr["AA1_AF2"].ToString();

        var ColB1 = dr["B1"].ToString().Split('/');
        var ColF1 = dr["F1"].ToString().Split('/');
        var ColH1 = dr["H1"].ToString().Split('/');
        var ColI1 = dr["I1"].ToString().Split('/');
        var ColJ1 = dr["J1"].ToString().Split('/');
        var ColQ1_Q2 = dr["Q1_Q2"].ToString().Split('/');
        var ColAA1_AB2 = dr["AA1_AB2"].ToString();

        //var GetData = GetMasterIdsBasedOnColumns("GetMasterIdsBasedOnColumns", Convert.ToString(ColB1[0]).Trim(), Convert.ToString(ColF1[0]).Trim(), Convert.ToString(ColH1[0]).Trim(), Convert.ToString(ColI1[0]).Trim(), Convert.ToString(ColJ1[1]).Trim(), Convert.ToString(ColQ1_Q2[1]).Trim(), Convert.ToString(ColAA1_AB2).Trim());
        var GetData = GetMasterIdsBasedOnColumns("GetMasterIdsBasedOnColumns", Convert.ToString(ColB1[0]).Trim(), Convert.ToString(ColF1[0]).Trim(), Convert.ToString(ColH1[0]).Trim(), Convert.ToString(ColI1[0]).Trim(), Convert.ToString(ColJ1[0]).Trim(), Convert.ToString(ColQ1_Q2[0]).Trim(), Convert.ToString(ColAA1_AB2).Trim());

        string getModule = "";
        string getFRICECategory = "";
        string getPriorityType = "";
        string getComplexity = "";
        string getFunctionalConsultant = "";
        string getPlanABAPConsultant = "";
        string getReusableStatus = "";

        if (GetData != null && GetData.Tables.Count > 0 && GetData.Tables[0].Rows.Count > 0)
        {
            getModule = GetData.Tables[0] != null && GetData.Tables[0].Rows.Count > 0 && GetData.Tables[0].Columns.Contains("ModuleId") ? GetData.Tables[0].Rows[0]["ModuleId"].ToString() : "";
            getFRICECategory = GetData.Tables[0] != null && GetData.Tables[0].Rows.Count > 0 && GetData.Tables[0].Columns.Contains("FCategoryId") ? GetData.Tables[0].Rows[0]["FCategoryId"].ToString() : "";
            getPriorityType = GetData.Tables[0] != null && GetData.Tables[0].Rows.Count > 0 && GetData.Tables[0].Columns.Contains("PriorityId") ? GetData.Tables[0].Rows[0]["PriorityId"].ToString() : "";
            getComplexity = GetData.Tables[0] != null && GetData.Tables[0].Rows.Count > 0 && GetData.Tables[0].Columns.Contains("ComplexityId") ? GetData.Tables[0].Rows[0]["ComplexityId"].ToString() : "";
            getFunctionalConsultant = GetData.Tables[0] != null && GetData.Tables[0].Rows.Count > 0 && GetData.Tables[0].Columns.Contains("FunctionalConsultant") ? GetData.Tables[0].Rows[0]["FunctionalConsultant"].ToString() : "";
            getPlanABAPConsultant = GetData.Tables[0] != null && GetData.Tables[0].Rows.Count > 0 && GetData.Tables[0].Columns.Contains("PlanABAPConsultant") ? GetData.Tables[0].Rows[0]["PlanABAPConsultant"].ToString() : "";
            getReusableStatus = GetData.Tables[0] != null && GetData.Tables[0].Rows.Count > 0 && GetData.Tables[0].Columns.Contains("ResuableStatus") ? GetData.Tables[0].Rows[0]["ResuableStatus"].ToString() : "";

        }

        var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();
        var getStatus = InsertUpdateABAPObjectTracker("InsertPlannedABAPObjectTracker", objABAPUploadId, getSrNo, Convert.ToInt32(getModule), getInterface, getDevelopmentDescription, getScope, Convert.ToInt32(getFRICECategory), Convert.ToInt32(getPriority), Convert.ToInt32(getPriorityType), Convert.ToInt32(getComplexity),
                              getFunctionalConsultant, getRGSPlannedStartDate, getRGSPlannedFinishDate, getRGSPlannedSubmissionDate, getRGSPlannedApprovalDate, getFSPlannedStartDate, getFSPlannedFinishDate, getPlanABAPConsultant, getABAPDevDuration,
                              getABAPDevPlannedStartDate, getABAPDevPlannedFinishDate, getHBTTestPlannedStartDate, getHBTTestPlannedFinishDate, getCTMPlannedStartDate, getCTMPlannedFinishDate, getUATSignOffPlannedDate, getGoLivePlannedDate, getCreatedBy, getReusableClientName,
                              getAdditionalEfforts, getpercentage, getReusableRemarks, getTcode, getReusableStatus, connection, transaction);
        if (getStatus.ToString() == "0")
        {
            return 0;
        }


        hdnTaskRefID.Value = getStatus.Tables[0].Rows[0]["ID"].ToString();
        var getABAPODId = Convert.ToInt32(getStatus.Tables[0].Rows[0]["ID"]);

        return Convert.ToInt32(getABAPODId);
        #endregion Save Task Details Data into Main Table as Draft 
    }

    private string ParseDate(string dateString, string format = "dd.MM.yyyy")
    {
        DateTime parsedDate;
        return DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate)
            ? parsedDate.ToString("yyyy-MM-dd")
            : string.Empty;
    }

    protected void DownloadExcel_Click(object sender, EventArgs e)
    {

        string filePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ABAPObjectfiles"]).Trim() + "/ABAP Object Tracker Template.xlsx");
        if (!System.IO.File.Exists(filePath))
        {
            return;
        }

        byte[] fileContent = System.IO.File.ReadAllBytes(filePath);

        DataSet GetData = new DataSet();
        List<string> masterDataTypes = new List<string>
        {
            "getModuleMaster",
            "getFCategoryMaster",
            "getPriorityMaster",
            "getComplexityMaster",
            "getConsultantMaster",
            "getABAPConsultantMaster",
            "getReusableStatusMaster"
        };

        foreach (var dataType in masterDataTypes)
        {
            DataTable fetchedData = fetchMasterDataAndExportToExcel(dataType);
            if (fetchedData != null)
            {
                fetchedData.TableName = dataType;
                GetData.Tables.Add(fetchedData);
            }
        }


        int mainSheetColumnIndex = 0;
        using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            using (XLWorkbook workBook = new XLWorkbook(stream))
            {

                #region Code to Add Module Master Data to an Excel Sheet.
                var moduleMasterWorksheet = workBook.Worksheets.Add("Module Master Data");
                for (int i = 0; i < GetData.Tables[0].Columns.Count; i++)
                {
                    moduleMasterWorksheet.Cell(1, i + 1).Value = GetData.Tables[0].Columns[i].ColumnName;
                }

                for (int i = 0; i < GetData.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < GetData.Tables[0].Columns.Count; j++)
                    {
                        moduleMasterWorksheet.Cell(i + 2, j + 1).Value = GetData.Tables[0].Rows[i][j];
                    }
                }

                moduleMasterWorksheet.Columns().AdjustToContents();
                moduleMasterWorksheet.Protect("ABAPObject@123");

                var mainWorksheet = workBook.Worksheets.FirstOrDefault();
                mainSheetColumnIndex = 2;
                var dropdownRange = mainWorksheet.Range("B3:B1000");
                var dataValidation = dropdownRange.SetDataValidation();
                var validationRange = "B2:B" + GetData.Tables[0].Rows.Count;
                dataValidation.List(moduleMasterWorksheet.Range(validationRange));
                dataValidation.IgnoreBlanks = true;
                dataValidation.InCellDropdown = true;
                #endregion


                #region Code to Add FRICE Category Master Data to an Excel Sheet.
                var fCategoryMasterWorksheet = workBook.Worksheets.Add("FRICE Category Data");
                for (int i = 0; i < GetData.Tables[1].Columns.Count; i++)
                {
                    fCategoryMasterWorksheet.Cell(1, i + 1).Value = GetData.Tables[1].Columns[i].ColumnName;
                }

                for (int i = 0; i < GetData.Tables[1].Rows.Count; i++)
                {
                    for (int j = 0; j < GetData.Tables[1].Columns.Count; j++)
                    {
                        fCategoryMasterWorksheet.Cell(i + 2, j + 1).Value = GetData.Tables[1].Rows[i][j];
                    }
                }

                fCategoryMasterWorksheet.Columns().AdjustToContents();
                fCategoryMasterWorksheet.Protect("ABAPObject@123");

                mainSheetColumnIndex = 6;
                var dropdownRange1 = mainWorksheet.Range("F3:F1000");
                var dataValidation1 = dropdownRange1.SetDataValidation();
                var validationRange1 = "B2:B" + (GetData.Tables[1].Rows.Count + 1); ;
                dataValidation1.List(fCategoryMasterWorksheet.Range(validationRange1));
                //dataValidation1.List(fCategoryMasterWorksheet.Range("B2:B" + (GetData.Tables[1].Rows.Count + 1).ToString()));
                dataValidation1.IgnoreBlanks = true;
                dataValidation1.InCellDropdown = true;
                #endregion


                #region Code to Add Priority Master Data to an Excel Sheet.
                var priorityMasterWorksheet = workBook.Worksheets.Add("Priority Master Data");
                for (int i = 0; i < GetData.Tables[2].Columns.Count; i++)
                {
                    priorityMasterWorksheet.Cell(1, i + 1).Value = GetData.Tables[2].Columns[i].ColumnName;
                }

                for (int i = 0; i < GetData.Tables[2].Rows.Count; i++)
                {
                    for (int j = 0; j < GetData.Tables[2].Columns.Count; j++)
                    {
                        priorityMasterWorksheet.Cell(i + 2, j + 1).Value = GetData.Tables[2].Rows[i][j];
                    }
                }

                priorityMasterWorksheet.Columns().AdjustToContents();
                priorityMasterWorksheet.Protect("ABAPObject@123");

                mainSheetColumnIndex = 8;
                var dropdownRange2 = mainWorksheet.Range("H3:H1000");
                var dataValidation2 = dropdownRange2.SetDataValidation();
                var validationRange2 = "B2:B" + (GetData.Tables[2].Rows.Count + 1); ;
                dataValidation2.List(priorityMasterWorksheet.Range(validationRange2));
                //dataValidation2.List(priorityMasterWorksheet.Range("B2:B" + (GetData.Tables[2].Rows.Count + 1).ToString()));
                dataValidation2.IgnoreBlanks = true;
                dataValidation2.InCellDropdown = true;
                #endregion


                #region Code to Add Complexity Master Data to an Excel Sheet.
                var complexityMasterWorksheet = workBook.Worksheets.Add("Complexity Master Data");
                for (int i = 0; i < GetData.Tables[3].Columns.Count; i++)
                {
                    complexityMasterWorksheet.Cell(1, i + 1).Value = GetData.Tables[3].Columns[i].ColumnName;
                }

                for (int i = 0; i < GetData.Tables[3].Rows.Count; i++)
                {
                    for (int j = 0; j < GetData.Tables[3].Columns.Count; j++)
                    {
                        complexityMasterWorksheet.Cell(i + 2, j + 1).Value = GetData.Tables[3].Rows[i][j];
                    }
                }

                complexityMasterWorksheet.Columns().AdjustToContents();
                complexityMasterWorksheet.Protect("ABAPObject@123");

                mainSheetColumnIndex = 9;
                var dropdownRange3 = mainWorksheet.Range("I3:I1000");
                var dataValidation3 = dropdownRange3.SetDataValidation();
                var validationRange3 = "B2:B" + (GetData.Tables[3].Rows.Count + 1); ;
                dataValidation3.List(complexityMasterWorksheet.Range(validationRange3));
                //dataValidation3.List(complexityMasterWorksheet.Range("B2:B" + (GetData.Tables[3].Rows.Count + 1).ToString()));
                dataValidation3.IgnoreBlanks = true;
                dataValidation3.InCellDropdown = true;
                #endregion


                #region Code to Add Consultant Master Data to an Excel Sheet.
                var consultantmasterWorksheet = workBook.Worksheets.Add("Consultant Master Data");
                for (int i = 0; i < GetData.Tables[4].Columns.Count; i++)
                {
                    consultantmasterWorksheet.Cell(1, i + 1).Value = GetData.Tables[4].Columns[i].ColumnName;
                }

                for (int i = 0; i < GetData.Tables[4].Rows.Count; i++)
                {
                    for (int j = 0; j < GetData.Tables[4].Columns.Count; j++)
                    {
                        consultantmasterWorksheet.Cell(i + 2, j + 1).Value = GetData.Tables[4].Rows[i][j];
                    }
                }

                consultantmasterWorksheet.Columns().AdjustToContents();
                consultantmasterWorksheet.Protect("ABAPObject@123");

                mainSheetColumnIndex = 10;
                var dropdownRange4 = mainWorksheet.Range("J3:J1000");
                var dataValidation4 = dropdownRange4.SetDataValidation();
                var validationRange4 = "B2:B" + (GetData.Tables[4].Rows.Count + 1); ;
                dataValidation4.List(consultantmasterWorksheet.Range(validationRange4));
                //dataValidation4.List(consultantmasterWorksheet.Range("B2:B" + (GetData.Tables[4].Rows.Count + 1).ToString()));
                dataValidation4.IgnoreBlanks = true;
                dataValidation4.InCellDropdown = true;
                #endregion


                #region Code to Add ABAP Consultant Master Data to an Excel Sheet.
                var abapconsultantmasterWorksheet = workBook.Worksheets.Add("ABAP Consultant Master Data");
                for (int i = 0; i < GetData.Tables[5].Columns.Count; i++)
                {
                    abapconsultantmasterWorksheet.Cell(1, i + 1).Value = GetData.Tables[5].Columns[i].ColumnName;
                }

                for (int i = 0; i < GetData.Tables[5].Rows.Count; i++)
                {
                    for (int j = 0; j < GetData.Tables[5].Columns.Count; j++)
                    {
                        abapconsultantmasterWorksheet.Cell(i + 2, j + 1).Value = GetData.Tables[5].Rows[i][j];
                    }
                }

                abapconsultantmasterWorksheet.Columns().AdjustToContents();
                abapconsultantmasterWorksheet.Protect("ABAPObject@123");

                mainSheetColumnIndex = 17;
                var dropdownRange5 = mainWorksheet.Range("Q3:Q1000");
                var dataValidation5 = dropdownRange5.SetDataValidation();
                var validationRange5 = "B2:B" + (GetData.Tables[5].Rows.Count + 1); ;
                dataValidation5.List(abapconsultantmasterWorksheet.Range(validationRange5));
                //dataValidation5.List(abapconsultantmasterWorksheet.Range("B2:B" + (GetData.Tables[5].Rows.Count + 1).ToString()));
                dataValidation5.IgnoreBlanks = true;
                dataValidation5.InCellDropdown = true;
                #endregion


                #region Code to Add Reusable Status Master Data to an Excel Sheet.
                var reusablemasterWorksheet = workBook.Worksheets.Add("Reusable Status Master Data");
                for (int i = 0; i < GetData.Tables[6].Columns.Count; i++)
                {
                    reusablemasterWorksheet.Cell(1, i + 1).Value = GetData.Tables[6].Columns[i].ColumnName;
                }

                for (int i = 0; i < GetData.Tables[6].Rows.Count; i++)
                {
                    for (int j = 0; j < GetData.Tables[6].Columns.Count; j++)
                    {
                        reusablemasterWorksheet.Cell(i + 2, j + 1).Value = GetData.Tables[6].Rows[i][j];
                    }
                }

                reusablemasterWorksheet.Columns().AdjustToContents();
                reusablemasterWorksheet.Protect("ABAPObject@123");

                mainSheetColumnIndex = 28;
                var dropdownRange6 = mainWorksheet.Range("AB3:AB1000");
                var dataValidation6 = dropdownRange6.SetDataValidation();
                var validationRange6 = "B2:B" + (GetData.Tables[6].Rows.Count + 1);
                dataValidation6.List(reusablemasterWorksheet.Range(validationRange6));
                dataValidation6.IgnoreBlanks = true;
                dataValidation6.InCellDropdown = true;
                #endregion

                // Save the workbook to a memory stream
                using (var memoryStream = new MemoryStream())
                {
                    workBook.SaveAs(memoryStream);
                    byte[] byteArray = memoryStream.ToArray();

                    // Send the file to the client
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment; filename=ABAP Object Tracker Template.xlsx");
                    Response.BinaryWrite(byteArray);
                    Response.End();
                }
            }
        }
    }

    public DataTable fetchMasterDataAndExportToExcel(string qtype)
    {
        DataTable dsMasterData = new DataTable();
        SqlParameter[] spars = new SqlParameter[5];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = qtype;

        dsMasterData = spm.getDataList(spars, "SP_ABAPObjectTracking");
        return dsMasterData;
    }

    public DataSet GetMasterIdsBasedOnColumns(string qtype, string Module, string FRICECategory, string PriorityType, string Complexity, string FunctionalConsultant, string PlanABAPConsultant, string ResuableStatus)
    {
        DataSet dsProjectsVendors = new DataSet();
        List<SqlParameter> spars = new List<SqlParameter>
        {
            new SqlParameter("@qtype", SqlDbType.VarChar) { Value = qtype },
            new SqlParameter("@Module", SqlDbType.VarChar) { Value = Module },
            new SqlParameter("@FRICECategory", SqlDbType.VarChar) { Value = FRICECategory },
            new SqlParameter("@PriorityType", SqlDbType.VarChar) { Value = PriorityType },
            new SqlParameter("@Complexity", SqlDbType.VarChar) { Value = Complexity },
            new SqlParameter("@FunctionalConsultant", SqlDbType.VarChar) { Value = FunctionalConsultant },
            new SqlParameter("@PlanABAPConsultant", SqlDbType.VarChar) { Value = PlanABAPConsultant },
            new SqlParameter("@ResuableStatus", SqlDbType.VarChar) { Value = ResuableStatus}
        };

        dsProjectsVendors = spm.getDatasetList(spars.ToArray(), "SP_ABAPObjectTracking");
        return dsProjectsVendors;

    }

    public DataSet InsertLocABAPODTracker(string qtype, string ProjectLocation, string CreatedBy, string filename, SqlConnection connection, SqlTransaction transaction)
    {
        DataSet dsData = new DataSet();
        try
        {
            List<SqlParameter> spars = new List<SqlParameter>
            {
                new SqlParameter("@qtype", SqlDbType.VarChar) { Value = qtype },
                new SqlParameter("@ProjectLocation", SqlDbType.VarChar) { Value = ProjectLocation },
                new SqlParameter("@CreatedBy", SqlDbType.VarChar) { Value = CreatedBy },
                new SqlParameter("@filename", SqlDbType.VarChar) { Value = filename },
            };

            using (SqlCommand cmd = new SqlCommand("SP_ABAPObjectTracking", connection, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(spars.ToArray());
                cmd.CommandTimeout = 180;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dsData);
                }
            }
            //dsData = spm.getDatasetList(spars.ToArray(), "SP_ABAPObjectTracking");
            return dsData;
        }
        catch (Exception e)
        {

        }
        return dsData;
    }

    public DataSet InsertUpdateABAPObjectTracker(string qtype, int objABAPUploadId, string getSrNo, int getModule, string getInterface, string getDevelopmentDescription, string getScope,
        int getFRICECategory, int getPriority, int getPriorityType, int getComplexity, string getFunctionalConsultant, string getRGSPlannedStartDate, string getRGSPlannedFinishDate,
        string getRGSPlannedSubmissionDate, string getRGSPlannedApprovalDate, string getFSPlannedStartDate, string getFSPlannedFinishDate, string getABAPDevPlannedABAPer,
        string getABAPDevDuration, string getABAPDevPlannedStartDate, string getABAPDevPlannedFinishDate, string getHBTTestPlannedStartDate, string getHBTTestPlannedFinishDate,
        string getCTMPlannedStartDate, string getCTMPlannedFinishDate, string getUATSignOffPlannedDate, string getGoLivePlannedDate, string getCreatedBy, string getReusableClientName,
        string getAdditionalEfforts, string getpercentage, string getReusableRemarks, string getTcode, string getReusableStatus, SqlConnection connection,SqlTransaction transaction)
    {
        DataSet dsMasterData = new DataSet();
        try
        {
            List<SqlParameter> spars = new List<SqlParameter>
             {
                new SqlParameter("@qtype", SqlDbType.VarChar) { Value = qtype },
                new SqlParameter("@ABAPODUploadId", SqlDbType.VarChar) { Value = objABAPUploadId },
                new SqlParameter("@SrNo", SqlDbType.VarChar) { Value = getSrNo },
                new SqlParameter("@Module", SqlDbType.VarChar) { Value = getModule },
                new SqlParameter("@Interface", SqlDbType.VarChar) { Value = getInterface },
                new SqlParameter("@DevelopmentDescription", SqlDbType.VarChar) { Value = getDevelopmentDescription },
                new SqlParameter("@Scope", SqlDbType.VarChar) { Value = getScope },
                new SqlParameter("@FRICECategory", SqlDbType.VarChar) { Value = getFRICECategory },
                new SqlParameter("@Priority", SqlDbType.VarChar) { Value = getPriority },
                new SqlParameter("@PriorityType", SqlDbType.VarChar) { Value = getPriorityType },
                new SqlParameter("@Complexity", SqlDbType.VarChar) { Value = getComplexity },
                new SqlParameter("@FunctionalConsultant", SqlDbType.VarChar) { Value = getFunctionalConsultant },
                new SqlParameter("@RGSPlannedStartDate", SqlDbType.VarChar) { Value = getRGSPlannedStartDate },
                new SqlParameter("@RGSPlannedFinishDate", SqlDbType.VarChar) { Value = getRGSPlannedFinishDate },
                new SqlParameter("@RGSPlannedSubmissionDate", SqlDbType.VarChar) { Value = getRGSPlannedSubmissionDate },
                new SqlParameter("@RGSPlannedApprovalDate", SqlDbType.VarChar) { Value = getRGSPlannedApprovalDate },
                new SqlParameter("@FSPlannedStartDate", SqlDbType.VarChar) { Value = getFSPlannedStartDate },
                new SqlParameter("@FSPlannedFinishDate", SqlDbType.VarChar) { Value = getFSPlannedFinishDate },
                new SqlParameter("@ABAPDevPlannedABAPer", SqlDbType.VarChar) { Value = getABAPDevPlannedABAPer },
                new SqlParameter("@ABAPDevDuration", SqlDbType.VarChar) { Value = getABAPDevDuration },
                new SqlParameter("@ABAPDevPlannedStartDate", SqlDbType.VarChar) { Value = getABAPDevPlannedStartDate },
                new SqlParameter("@ABAPDevPlannedFinishDate", SqlDbType.VarChar) { Value = getABAPDevPlannedFinishDate },
                new SqlParameter("@HBTTestPlannedStartDate", SqlDbType.VarChar) { Value = getHBTTestPlannedStartDate },
                new SqlParameter("@HBTTestPlannedFinishDate", SqlDbType.VarChar) { Value = getHBTTestPlannedFinishDate },
                new SqlParameter("@CTMPlannedStartDate", SqlDbType.VarChar) { Value = getCTMPlannedStartDate },
                new SqlParameter("@CTMPlannedFinishDate", SqlDbType.VarChar) { Value = getCTMPlannedFinishDate },
                new SqlParameter("@UATSignOffPlannedDate", SqlDbType.VarChar) { Value = getUATSignOffPlannedDate },
                new SqlParameter("@GoLivePlannedDate", SqlDbType.VarChar) { Value = getGoLivePlannedDate },
                new SqlParameter("@CreatedBy", SqlDbType.VarChar) { Value = getCreatedBy },
                new SqlParameter("@ReusableClientName", SqlDbType.VarChar) { Value = getReusableClientName },
                new SqlParameter("@ReusableAdditionalEfforts", SqlDbType.VarChar) { Value = getAdditionalEfforts },
                new SqlParameter("@Reusablepercentage", SqlDbType.VarChar) { Value = getpercentage },
                new SqlParameter("@ReusableRemarks", SqlDbType.VarChar) { Value = getReusableRemarks },
                new SqlParameter("@ReusableTcode", SqlDbType.VarChar) { Value = getTcode },
                new SqlParameter("@ReusableStatusId", SqlDbType.VarChar) { Value = getReusableStatus },

              };

            using (SqlCommand cmd = new SqlCommand("SP_ABAPObjectTracking", connection, transaction))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(spars.ToArray());
                cmd.CommandTimeout = 180;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dsMasterData);
                }
            }

            return dsMasterData;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            throw;
        }
    }



    #endregion
}