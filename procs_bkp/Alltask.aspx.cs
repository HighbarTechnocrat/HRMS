using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using ClosedXML.Excel;

public partial class procs_Alltask : System.Web.UI.Page
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

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            var empCode = Convert.ToString(Session["Empcode"]);
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    Page.SmartNavigation = true;
                    editform.Visible = true;
                    hdnTaskRefID.Value = "0";
                    getTaskRefID();
                    getTaskCreatedBY();
                    getMeetingDisTitle();
                    BindPage();
                    
                }
                else
                {
                }
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }


    private void getTaskRefID()
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetReferenceIDDropdown";
            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();
            DataSet DS = spm.getDatasetList(spars, "SP_TASK_M_DETAILS");
            ddlTaskRefId.DataSource = DS.Tables[0];
            ddlTaskRefId.DataTextField = "Task_Reference_ID";
            ddlTaskRefId.DataValueField = "ID";
            ddlTaskRefId.DataBind();
            ddlTaskRefId.Items.Insert(0, new ListItem("Select Task Reference Id", "0"));
        }
        catch (Exception ex)
        {

        }
    }

    private void getTaskCreatedBY()
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetCreatedByTaskDropdown";
            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();
            DataSet DS = spm.getDatasetList(spars, "SP_TASK_M_DETAILS");

            DDlCreatedBy.DataSource = DS.Tables[0];
            DDlCreatedBy.DataTextField = "Emp_Name";
            DDlCreatedBy.DataValueField = "Emp_Code";
            DDlCreatedBy.DataBind();
            DDlCreatedBy.Items.Insert(0, new ListItem("Select Task Created By", "0"));
        }
        catch (Exception ex)
        {

        }
    }


    private void getMeetingDisTitle()
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetMeetingDisTitleDropdown";

            DataSet DS = spm.getDatasetList(spars, "SP_TASK_M_DETAILS");
            DDLMeetingDiscussionTitle.DataSource = DS.Tables[0];
            DDLMeetingDiscussionTitle.DataTextField = "Meeting_Discussion_Title";
            DDLMeetingDiscussionTitle.DataValueField = "Meeting_Discussion_Title";
            DDLMeetingDiscussionTitle.DataBind();
            DDLMeetingDiscussionTitle.Items.Insert(0, new ListItem("Select Meeting Discussion Title", "0"));
        }
        catch (Exception ex)
        {

        }
    }

    private void BindPage()
    {
        SqlParameter[] spars = new SqlParameter[7];
        //spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        //spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();

        string[] strdate;
        string strfromDate = null;
        string strToDate = null;
        string ToDate = "";

        //@fromDate
        spars[0] = new SqlParameter("@TaskRFDate", SqlDbType.VarChar);
        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            //IsTaskRefDateShow = "Task Reference From Date : " + Convert.ToString(txtFromdate.Text).Trim() + "  -  Task Reference To Date : " + Convert.ToString(txtToDate.Text).Trim()+"";
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

            spars[0].Value = strfromDate;
        }
        else
        {
            spars[0].Value = "";
        }
        spars[1] = new SqlParameter("@TaskRTDate", SqlDbType.VarChar);
        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            ToDate = Convert.ToString(txtToDate.Text).Trim();

        }
        else
        {
            strToDate = DateTime.Today.ToString("yyyy") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("dd");
            ToDate = DateTime.Today.ToString("dd/MM/yyyy");
        }
        spars[1].Value = strToDate;

        spars[2] = new SqlParameter("@Id", SqlDbType.Decimal);
        if (Convert.ToString(ddlTaskRefId.SelectedValue).Trim() != "0")
        {
            spars[2].Value = Convert.ToDouble(ddlTaskRefId.SelectedValue);
        }
        else
        {
            spars[2].Value = null;
        }
        spars[3] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar);
        if (Convert.ToString(DDlCreatedBy.SelectedValue).Trim() != "0")
        {
            spars[3].Value = Convert.ToString(DDlCreatedBy.SelectedValue).Trim();
        }
        else
        {
            spars[3].Value = null;
        }
        spars[4] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[4].Value = "GetAllTaskGird";

        spars[5] = new SqlParameter("@MeetingTitleDis", SqlDbType.VarChar);
        if (Convert.ToString(DDLMeetingDiscussionTitle.SelectedValue).Trim() != "0")
        {
            spars[5].Value = Convert.ToString(DDLMeetingDiscussionTitle.SelectedValue).Trim();
        }
        else
        {
            spars[5].Value = null; 
        }

        spars[6] = new SqlParameter("@Searchtext", SqlDbType.VarChar);
        spars[6].Value = TxtSearch_task.Text.Trim();


        DataSet DS = spm.getDatasetList(spars, "SP_TASK_M_DETAILS");
        if (DS.Tables[0].Rows.Count > 0)
        {
            gv_MyTask.DataSource = DS.Tables[0];
            gv_MyTask.DataBind();
        }
    }

    public static void write2log(string strmsg)
    {

        System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Configuration.ConfigurationSettings.AppSettings["LogTestPath"] +
             "Log_" + DateTime.Now.Day.ToString() + ".txt", true);
        sw.WriteLine(strmsg);
        sw.Flush();
        sw.Close();
    }

    protected void gv_MyTask_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gv_MyTask.PageIndex = e.NewPageIndex;
            BindPage();
        }
        catch (Exception)
        {

            throw;
        }
    }


    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            if (txtFromdate.Text.ToString().Trim() == "")
            {
                lblmessage.Text = "Please select task reference from date";
                return;
            }
            var getDate = txtFromdate.Text.ToString();
            var getTodayDate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime StartDate = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime StartingDate = DateTime.ParseExact(getTodayDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (StartingDate < StartDate)
            {
                lblmessage.Text = "Please select date only today or less than today";
                txtFromdate.Text = "";
                txtToDate.Text = "";
            }
            else
            {

                lblmessage.Text = "";
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            if (txtFromdate.Text.ToString().Trim() == "")
            {
                lblmessage.Text = "Please select task reference from date";
                return;
            }
            if (txtToDate.Text.ToString().Trim() == "")
            {
                lblmessage.Text = "Please select task reference to date";
                return;
            }
            var getFromDate = txtFromdate.Text.ToString();
            var getToDate = txtToDate.Text.Trim();

            DateTime fromDate = DateTime.ParseExact(getFromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime toDate = DateTime.ParseExact(getToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (toDate < fromDate)
            {
                lblmessage.Text = "Please select to date less than from date";
                txtFromdate.Text = "";
                txtToDate.Text = "";
            }
            else
            {
                lblmessage.Text = "";
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void btnIn_Click(object sender, EventArgs e)
    {
        SqlParameter[] spars = new SqlParameter[7];
        string[] strdate;
        string strfromDate = null;
        string strToDate = null;
        string ToDate = "";

        //@fromDate
        spars[0] = new SqlParameter("@TaskRFDate", SqlDbType.VarChar);
        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        { strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            spars[0].Value = strfromDate;
        }
        else
        {
            spars[0].Value = "";
        }
        spars[1] = new SqlParameter("@TaskRTDate", SqlDbType.VarChar);
        if (Convert.ToString(txtToDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            ToDate = Convert.ToString(txtToDate.Text).Trim();

        }
        else
        {
            strToDate = DateTime.Today.ToString("yyyy") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("dd");
            ToDate = DateTime.Today.ToString("dd/MM/yyyy");
        }
        spars[1].Value = strToDate;

        spars[2] = new SqlParameter("@Id", SqlDbType.Decimal);
        if (Convert.ToString(ddlTaskRefId.SelectedValue).Trim() != "0")
        {
            spars[2].Value = Convert.ToDouble(ddlTaskRefId.SelectedValue);
        }
        else
        {
            spars[2].Value = null;
        }
        spars[3] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar);
        if (Convert.ToString(DDlCreatedBy.SelectedValue).Trim() != "0")
        {
            spars[3].Value = Convert.ToString(DDlCreatedBy.SelectedValue).Trim();
        }
        else
        {
            spars[3].Value = null;
        }

        spars[4] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[4].Value = "GetAllTaskGird";

        spars[5] = new SqlParameter("@MeetingTitleDis", SqlDbType.VarChar);
        if (Convert.ToString(DDLMeetingDiscussionTitle.SelectedValue).Trim() != "0")
        {
            spars[5].Value = Convert.ToString(DDLMeetingDiscussionTitle.SelectedValue).Trim();
        }
        else
        {
            spars[5].Value = null;
        }

        spars[6] = new SqlParameter("@Searchtext", SqlDbType.VarChar);
        spars[6].Value = TxtSearch_task.Text.Trim();

        DataSet DS = spm.getDatasetList(spars, "SP_TASK_M_DETAILS");
        if (DS.Tables[0].Rows.Count > 0)
        {
            gv_MyTask.DataSource = DS.Tables[0];
            gv_MyTask.DataBind();
        }
    }

    

    protected void btnReject_Click(object sender, EventArgs e)
    {
        txtFromdate.Text = "";
        txtToDate.Text = "";
        TxtSearch_task.Text = "";
        getTaskRefID();
        getTaskCreatedBY();
        getMeetingDisTitle();
        BindPage();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            GetdownloadExcel();
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public void GetdownloadExcel()
    {
        DataTable dtRequisitionDetails = new DataTable();
        lblmessage.Text = "";
        try
        {
            SqlParameter[] spars = new SqlParameter[8];
            string[] strdate;
            string strfromDate = null;
            string strToDate = null;
            string ToDate = "";

            //@fromDate
            spars[0] = new SqlParameter("@TaskRFDate", SqlDbType.VarChar);
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                spars[0].Value = strfromDate;
            }
            else
            {
                spars[0].Value = "";
            }
            spars[1] = new SqlParameter("@TaskRTDate", SqlDbType.VarChar);
            if (Convert.ToString(txtToDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtToDate.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                ToDate = Convert.ToString(txtToDate.Text).Trim();
            }
            else
            {
                strToDate = DateTime.Today.ToString("yyyy") + "-" + DateTime.Today.ToString("MM") + "-" + DateTime.Today.ToString("dd");
                ToDate = DateTime.Today.ToString("dd/MM/yyyy");
            }
            spars[1].Value = strToDate;

            spars[2] = new SqlParameter("@Id", SqlDbType.Decimal);
            if (Convert.ToString(ddlTaskRefId.SelectedValue).Trim() != "0")
            {
                spars[2].Value = Convert.ToDouble(ddlTaskRefId.SelectedValue);
            }
            else
            {
                spars[2].Value = null;
            }
            spars[3] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar);
            if (Convert.ToString(DDlCreatedBy.SelectedValue).Trim() != "0")
            {
                spars[3].Value = Convert.ToString(DDlCreatedBy.SelectedValue).Trim();
            }
            else
            {
                spars[3].Value = null;
            }
            spars[4] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[4].Value = "AllTaskDownloadExcel";
            spars[5] = new SqlParameter("@MeetingTitleDis", SqlDbType.VarChar);
            if (Convert.ToString(DDLMeetingDiscussionTitle.SelectedValue).Trim() != "0")
            {
                spars[5].Value = Convert.ToString(DDLMeetingDiscussionTitle.SelectedValue).Trim();
            }
            else
            {
                spars[5].Value = null;
            }

            //spars[6] = new SqlParameter("@Searchtext", SqlDbType.VarChar);
            //spars[6].Value = TxtSearch_task.Text.Trim();

            //spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            //spars[0].Value = "AllTaskDownloadExcel";
            //spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            //spars[1].Value = Convert.ToString(Session["Empcode"]);
            //spars[1] = new SqlParameter("@Created_By", SqlDbType.VarChar);
            //spars[1].Value = TaskRef_ID;

            dtRequisitionDetails = spm.getMobileRemDataList(spars, "SP_TASK_M_DETAILS");
            if (dtRequisitionDetails.Rows.Count > 0)
            {
                var newTable = new DataTable();
                newTable.Columns.Add("Task Reference ID");
                newTable.Columns.Add("Task Reference Date");
                newTable.Columns.Add("Meeting /Discussion Title");
                newTable.Columns.Add("Task ID");
                newTable.Columns.Add("Task Creation Date");
                newTable.Columns.Add("Task Description");
                newTable.Columns.Add("Project / Location");
                newTable.Columns.Add("Task Executor");
                newTable.Columns.Add("Task Supervisor");
                newTable.Columns.Add("Original Due Date");
                newTable.Columns.Add("Revised Due Date (A)");
                newTable.Columns.Add("Due Date Count");
                newTable.Columns.Add("Task Actual Closed Date (B)");
                newTable.Columns.Add("Overshoot Days (B-A)");
                newTable.Columns.Add("Status Title");

                foreach (DataRow item in dtRequisitionDetails.Rows)
                {
                    DataRow _dr = newTable.NewRow();
                    _dr["Task Reference ID"] = item["Task_Reference_ID"].ToString();
                    _dr["Task Reference Date"] = item["Task_Reference_Date"].ToString();
                    _dr["Meeting /Discussion Title"] = item["Meeting_Discussion_Title"].ToString();
                    _dr["Task ID"] = item["Task_ID"].ToString();
                    _dr["Task Creation Date"] = item["Task_Creation_Date"].ToString();
                    _dr["Task Description"] = item["Task_Description"].ToString();
                    _dr["Project / Location"] = item["Location_name"].ToString();
                    _dr["Task Executor"] = item["TaskExecuter"].ToString();
                    _dr["Task Supervisor"] = item["TaskSupervisor"].ToString();
                    _dr["Original Due Date"] = item["L_DueDate"].ToString();
                    _dr["Revised Due Date (A)"] = item["Due_Date"].ToString();
                    _dr["Due Date Count"] = item["Due_DateCount"].ToString();
                    _dr["Task Actual Closed Date (B)"] = item["ClosedDate"].ToString();
                    _dr["Overshoot Days (B-A)"] = item["OvershootDays"].ToString();
                    _dr["Status Title"] = item["StatusTitle"].ToString();
                    newTable.Rows.Add(_dr);
                }
                var dateTime = DateTime.Now.ToString("ddMMyyyyHHmmss");
                var excelName = "My Task Details_" + dateTime;
                //using (XLWorkbook wb = new XLWorkbook())
                //{
                //	wb.Worksheets.Add(newTable, "Task List");
                //	Response.Clear();
                //	Response.Buffer = true;
                //	Response.Charset = "";
                //	Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //	Response.AddHeader("content-disposition", "attachment;filename=" + excelName + ".xlsx");
                //	using (MemoryStream MyMemoryStream = new MemoryStream())
                //	{
                //		wb.SaveAs(MyMemoryStream);
                //		MyMemoryStream.WriteTo(Response.OutputStream);
                //		Response.Flush();
                //		Response.End();
                //	}
                //}
                var aCode = 65;
                //var excelName = "IR_Sheet_";
                using (XLWorkbook wb = new XLWorkbook())
                {

                    var ws = wb.Worksheets.Add("Task Details");
                    int rowIndex = 1; int i = 1;
                    int columnIndex = 0;
                    foreach (DataColumn column in newTable.Columns)
                    {
                        ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Value = column.ColumnName;
                        ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Font.Bold = true;
                        ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Fill.BackgroundColor = XLColor.Gainsboro;
                        ws.Column(i).Width = 25;
                        columnIndex++; i++;
                    }

                    rowIndex++;
                    foreach (DataRow row in newTable.Rows)
                    {
                        int valueCount = 0;
                        foreach (object rowValue in row.ItemArray)
                        {
                            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Value = rowValue;
                            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Style.Alignment.WrapText = true;
                            valueCount++;
                        }

                        rowIndex++;
                    }

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + excelName + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }

            }
            else
            {
                lblmessage.Text = "Record not available";
            }
        }
        catch (Exception)
        {

            throw;
        }
    }


    protected void lnk_Download_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string TaskRef_ID = "";
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            TaskRef_ID = Convert.ToString(gv_MyTask.DataKeys[row.RowIndex].Values[0]).Trim();
            GetdownloadExcelRowbyRow(TaskRef_ID);
        }
        catch (Exception)
        {

            throw;
        }
    }
    public void GetdownloadExcelRowbyRow(string TaskRef_ID)
    {
        DataTable dtRequisitionDetails = new DataTable();
        lblmessage.Text = "";
        try
        {
            SqlParameter[] spars = new SqlParameter[7];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "SupervisorExcelAllTask";
            //spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            //spars[1].Value = Convert.ToString(Session["Empcode"]);
            spars[1] = new SqlParameter("@Created_By", SqlDbType.VarChar);
            spars[1].Value = TaskRef_ID;
            dtRequisitionDetails = spm.getMobileRemDataList(spars, "SP_TASK_M_EXECUTER");
            if (dtRequisitionDetails.Rows.Count > 0)
            {
                var newTable = new DataTable();
                newTable.Columns.Add("Task Reference ID");
                newTable.Columns.Add("Task Reference Date");
                newTable.Columns.Add("Meeting /Discussion Title");
                newTable.Columns.Add("Task ID");
                newTable.Columns.Add("Task Creation Date");
                newTable.Columns.Add("Task Description");
                newTable.Columns.Add("Project / Location");
                newTable.Columns.Add("Task Executor");
                newTable.Columns.Add("Task Supervisor");
                newTable.Columns.Add("Original Due Date");
                newTable.Columns.Add("Revised Due Date (A)");
                newTable.Columns.Add("Task Actual Closed Date (B)");
                newTable.Columns.Add("Overshoot Days (B-A)");
                newTable.Columns.Add("Status Title");

                foreach (DataRow item in dtRequisitionDetails.Rows)
                {
                    DataRow _dr = newTable.NewRow();
                    _dr["Task Reference ID"] = item["Task_Reference_ID"].ToString();
                    _dr["Task Reference Date"] = item["Task_Reference_Date"].ToString();
                    _dr["Meeting /Discussion Title"] = item["Meeting_Discussion_Title"].ToString();
                    _dr["Task ID"] = item["Task_ID"].ToString();
                    _dr["Task Creation Date"] = item["Task_Creation_Date"].ToString();
                    _dr["Task Description"] = item["Task_Description"].ToString();
                    _dr["Project / Location"] = item["Location_name"].ToString();
                    _dr["Task Executor"] = item["TaskExecuter"].ToString();
                    _dr["Task Supervisor"] = item["TaskSupervisor"].ToString();
                    _dr["Original Due Date"] = item["L_DueDate"].ToString();
                    _dr["Revised Due Date (A)"] = item["Due_Date"].ToString();
                    _dr["Task Actual Closed Date (B)"] = item["ClosedDate"].ToString();
                    _dr["Overshoot Days (B-A)"] = item["OvershootDays"].ToString();
                    _dr["Status Title"] = item["StatusTitle"].ToString();
                    newTable.Rows.Add(_dr);
                }
                var dateTime = DateTime.Now.ToString("ddMMyyyyHHmmss");
                var excelName = "My Task Details_" + dateTime;
                var aCode = 65;
                //var excelName = "IR_Sheet_";
                using (XLWorkbook wb = new XLWorkbook())
                {

                    var ws = wb.Worksheets.Add("Task Details");
                    int rowIndex = 1; int i = 1;
                    int columnIndex = 0;
                    foreach (DataColumn column in newTable.Columns)
                    {
                        ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Value = column.ColumnName;
                        ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Font.Bold = true;
                        ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Fill.BackgroundColor = XLColor.Gainsboro;
                        ws.Column(i).Width = 25;
                        columnIndex++; i++;
                    }

                    rowIndex++;
                    foreach (DataRow row in newTable.Rows)
                    {
                        int valueCount = 0;
                        foreach (object rowValue in row.ItemArray)
                        {
                            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Value = rowValue;
                            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Style.Alignment.WrapText = true;
                            valueCount++;
                        }

                        rowIndex++;
                    }

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + excelName + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }

            }
            else
            {
                lblmessage.Text = "Record not available";
            }
        }
        catch (Exception)
        {

            throw;
        }
    }


}