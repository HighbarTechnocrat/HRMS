using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ABAP_Object_Tracker_Change_Status_ABAP_Timesheet_New : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();

    #region Creative_Default_methods

    public DataSet dsDirecttaxSectionList = new DataSet();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }
    #endregion

    #region Page_Events

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            lblTimesheetErrorMsg.Text = "";
            txtEmpCode.Text = Convert.ToString(Session["Empcode"]);

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/ABAP_Object_Tracker_Index.aspx");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    GetWeekStartDateEndDate();
                    btn_ViewTimesheet_Click();
                    GetMasterList();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    #endregion

    #region ABAP Object Submitted Plan 

    protected void ibdownloadbtn_Click(object sender, EventArgs e)
    {
        ImageButton button = sender as ImageButton;
        if (button != null)
        {
            string commandArgument = button.CommandArgument;
            string filePath = Server.MapPath("~/ABAPTracker/FS_Attachment/" + commandArgument);

            if (System.IO.File.Exists(filePath))
            {
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + commandArgument);
                Response.WriteFile(filePath);
                Response.End();
            }
            else
            {
                Response.Write("File not found.");
            }
        }





    }

    public void GetWeekStartDateEndDate()
    {
        try
        {

            var getStartDate = spm.GetWeekStartDateEndDate(0);
            if (getStartDate.Rows.Count > 0)
            {
                var startDate = Convert.ToString(getStartDate.Rows[0]["StartDate"]);
                var endDate = Convert.ToString(getStartDate.Rows[getStartDate.Rows.Count - 1]["StartDate"]);
                hdnStartDate.Value = startDate;
                hdnEndDate.Value = endDate;

            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private Boolean Check_isLogin_ABAP()
    {
        Boolean Validate_ABAP = false;
        try
        {
            SqlParameter[] sparsError = new SqlParameter[2];
            sparsError[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            sparsError[0].Value = "check_isABAP_Developer";

            sparsError[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            sparsError[1].Value = Convert.ToString(Session["Empcode"]).Trim();
            DataSet DS = spm.getDatasetList(sparsError, "SP_ABAP_Productivity_CompletionSheet");

            if (DS.Tables[0].Rows.Count > 0)
            {
                Validate_ABAP = true;
            }

        }
        catch (Exception ex)
        {
        }
        return Validate_ABAP;
    }

    public void btn_ViewTimesheet_Click()
    {
        var abapdetailids = Session["CommaSeperABAPDetailsId"].ToString();
        var firstDate = DateTime.ParseExact(hdnStartDate.Value, "dd-MM-yyyy", null).ToString("yyyy-MM-dd");
        var secondDate = DateTime.ParseExact(hdnEndDate.Value, "dd-MM-yyyy", null).ToString("yyyy-MM-dd");

        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[5];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetABAPersTimsheetDetailsNew";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = (Session["Empcode"]).ToString().Trim();

        spars[2] = new SqlParameter("@TS_ABAPDetailsId", SqlDbType.VarChar);
        spars[2].Value = abapdetailids;

        spars[3] = new SqlParameter("@WeekStartDate", SqlDbType.VarChar);
        spars[3].Value = firstDate;

        spars[4] = new SqlParameter("@WeekEndDate", SqlDbType.VarChar);
        spars[4].Value = secondDate;

        dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsABAPObjectPlanSubmitted.Tables[0].Columns.Count; i++)
            {
                gvTimesheet.Columns[i].HeaderText = dsABAPObjectPlanSubmitted.Tables[0].Columns[i].ColumnName;
            }

            ABAP.Visible = true;
            gvTimesheet.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvTimesheet.DataBind();


            foreach (GridViewRow row in gvTimesheet.Rows)
            {
                var objProjectLocation = Session["projectCode"].ToString();
                var objActivityDesc = HttpUtility.HtmlDecode(row.Cells[4].Text.ToString().Trim());

                DataSet dsGetTimesheetHours = new DataSet();
                SqlParameter[] spars1 = new SqlParameter[7];

                spars1[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                spars1[0].Value = "GetABAPersTimsheetDetailsHours";

                spars1[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
                spars1[1].Value = (Session["Empcode"]).ToString().Trim();

                spars1[2] = new SqlParameter("@TS_ABAPDetailsId", SqlDbType.VarChar);
                spars1[2].Value = abapdetailids;

                spars1[3] = new SqlParameter("@WeekStartDate", SqlDbType.VarChar);
                spars1[3].Value = firstDate;

                spars1[4] = new SqlParameter("@WeekEndDate", SqlDbType.VarChar);
                spars1[4].Value = secondDate;

                spars1[5] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
                spars1[5].Value = objProjectLocation;

                spars1[6] = new SqlParameter("@Activity_desc", SqlDbType.VarChar);
                spars1[6].Value = objActivityDesc;

                dsGetTimesheetHours = spm.getDatasetList(spars1, "SP_ABAPObjectTracking");
                if (dsGetTimesheetHours != null && dsGetTimesheetHours.Tables[0].Rows.Count > 0)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        for (int i = 0; i < dsGetTimesheetHours.Tables[0].Rows.Count; i++)
                        {
                            TextBox txtFirst = (TextBox)row.FindControl("txtFirst");
                            TextBox txtSecond = (TextBox)row.FindControl("txtSecond");
                            TextBox txtThird = (TextBox)row.FindControl("txtThird");
                            TextBox txtFourth = (TextBox)row.FindControl("txtFourth");
                            TextBox txtFifth = (TextBox)row.FindControl("txtFifth");
                            TextBox txtSixth = (TextBox)row.FindControl("txtSixth");
                            TextBox txtSeventh = (TextBox)row.FindControl("txtSeventh");

                            DataRow dr = dsGetTimesheetHours.Tables[0].Rows[i];
                            if (dr != null)
                            {
                                if (HttpUtility.HtmlDecode(row.Cells[4].Text) == dr[2].ToString() && HttpUtility.HtmlDecode(row.Cells[3].Text) == dr[1].ToString())
                                {
                                    txtFirst.Text = dr[3].ToString();
                                    txtSecond.Text = dr[4].ToString();
                                    txtThird.Text = dr[5].ToString();
                                    txtFourth.Text = dr[6].ToString();
                                    txtFifth.Text = dr[7].ToString();
                                    txtSixth.Text = dr[8].ToString();
                                    txtSeventh.Text = dr[9].ToString();
                                }
                            }
                        }
                    }
                }

            }

        }
        else
        {
            gvTimesheet.DataSource = null;
            gvTimesheet.DataBind();
        }



    }

    protected void gvTimesheet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string[] textBoxIDs = new string[] { "txtfirst", "txtSecond", "txtThird", "txtFourth", "txtFifth", "txtSixth", "txtSeventh" };

            foreach (string txtBoxID in textBoxIDs)
            {
                TextBox txtBox = (TextBox)e.Row.FindControl(txtBoxID);
                if (txtBox != null)
                {
                    txtBox.Attributes["onblur"] = "validateTimesheet('" + txtBox.ClientID + "')";
                }
            }


            for (int i = 5; i < gvTimesheet.Columns.Count; i++)
            {
                var getDate = gvTimesheet.Columns[i].HeaderText.ToString();
                var splitDate = getDate.Split('-');
                var finaldate = splitDate[2] + "-" + splitDate[1] + "-" + splitDate[0];

                var getDt = spm.GetTimesheetRegInbox(Session["Empcode"].ToString(), finaldate);
                var getVal = Convert.ToString(getDt.Rows[0]["ISWORKING"]);

                System.Drawing.Color cellColor;
                if (getVal == "WK")
                {
                    cellColor = System.Drawing.ColorTranslator.FromHtml("#C0C0C0");
                }
                else if (getVal == "HO")
                {
                    cellColor = System.Drawing.ColorTranslator.FromHtml("#FF9933");
                }
                else if (getVal == "LE")
                {
                    cellColor = System.Drawing.Color.Yellow;
                }
                else
                {
                    cellColor = System.Drawing.Color.Transparent;
                }

                e.Row.Cells[i].BackColor = cellColor;
                TextBox txtBox = (TextBox)e.Row.Cells[i].FindControl("txtBoxID");
                if (txtBox != null)
                {
                    txtBox.BackColor = cellColor;
                }
            }
        }
    }

    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        //if (confirmValue != "Yes")
        //{
        //    return;
        //}

        bool isValid = true;
        if (string.IsNullOrEmpty(Session["Empcode"].ToString()))
        {
            lblTimesheetErrorMsg.Text = "Emplpoyee Code not found. Session is expired.";
            return;
        }
        if (string.IsNullOrEmpty(Session["projectCode"].ToString()))
        {
            lblTimesheetErrorMsg.Text = "Project Location not found. Session is expired.";
            return;
        }
        double totalSum = 0;

        foreach (GridViewRow row in gvTimesheet.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                TextBox[] textboxes = new TextBox[]
                {
                    (TextBox)row.FindControl("txtfirst"),
                    (TextBox)row.FindControl("txtSecond"),
                    (TextBox)row.FindControl("txtThird"),
                    (TextBox)row.FindControl("txtFourth"),
                    (TextBox)row.FindControl("txtFifth"),
                    (TextBox)row.FindControl("txtSixth"),
                    (TextBox)row.FindControl("txtSeventh")
                };

                foreach (var textbox in textboxes)
                {
                    if (textbox != null && !string.IsNullOrWhiteSpace(textbox.Text))
                    {
                        var strdate = textbox.Text.Trim().Split(':');
                        double cellValue = 0;
                        if (double.TryParse(strdate[0], out cellValue))
                        {
                            totalSum += cellValue;
                        }

                        if (strdate.Length == 2)
                        {
                            if (strdate[0].Length != 2 || strdate[1].Length != 2)
                            {
                                lblTimesheetErrorMsg.Text = "Please enter a valid hours in the format hh:mm.";
                                return;
                            }
                            else
                            {
                                if (Convert.ToInt32(strdate[0].ToString()) >= 24)
                                {
                                    lblTimesheetErrorMsg.Text = "Please enter less than 24 hours.";
                                    return;
                                }
                                if (Convert.ToInt32(strdate[1].ToString()) > 59)
                                {
                                    lblTimesheetErrorMsg.Text = "Please enter less than 60 minutes.";
                                    return;
                                }
                                if (Convert.ToInt32(strdate[0].ToString()) <= 00 && Convert.ToInt32(strdate[1].ToString()) <= 00)
                                {
                                    lblTimesheetErrorMsg.Text = "Please enter a valid hours in the format hh:mm.";
                                    return;
                                }
                            }
                        }
                        else
                        {
                            lblTimesheetErrorMsg.Text = "Please enter a valid hours in the format hh:mm.";
                            return;
                        }
                    }
                    isValid = false;
                }

                //if (totalSum > 24)
                //{
                //    lblTimesheetErrorMsg.Text = "Please enter less than 24 hours for the entire day.";
                //    return;
                //}
            }
            isValid = true;
        }

        if (isValid == true)
        {
            int taskId = 0;
            string comp_location_code = "";

            //Delete existing records based on timsheetdate
            DeleteTimesheetRecord(gvTimesheet);

            //Insert timesheet
            foreach (GridViewRow row in gvTimesheet.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string objABAPDetailsId = HttpUtility.HtmlDecode(row.Cells[1].Text);
                    string objDevDesc = HttpUtility.HtmlDecode(row.Cells[4].Text);

                    TextBox[] textboxes = new TextBox[]
                    {
                        (TextBox)row.FindControl("txtfirst"),
                        (TextBox)row.FindControl("txtSecond"),
                        (TextBox)row.FindControl("txtThird"),
                        (TextBox)row.FindControl("txtFourth"),
                        (TextBox)row.FindControl("txtFifth"),
                        (TextBox)row.FindControl("txtSixth"),
                        (TextBox)row.FindControl("txtSeventh")
                    };

                    for (int i = 0; i < textboxes.Length; i++)
                    {
                        if (textboxes[i] != null)
                        {
                            string projectCode = Session["projectCode"].ToString();
                            string headerText = gvTimesheet.HeaderRow.Cells[i + 5].Text;
                            DateTime parsedDate = DateTime.ParseExact(headerText, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            string Timesheet_date = parsedDate.ToString("yyyy-MM-dd");
                            string textBoxValue = textboxes[i].Text.Trim();
                            if (!string.IsNullOrEmpty(textBoxValue))
                            {
                                #region Check Project Location is available in Timsheet Location Master List, if not then insert in Timsheet Location Master List.
                                if (!string.IsNullOrEmpty(projectCode.ToString().Trim()))
                                {
                                    DataSet objDS = new DataSet();
                                    SqlParameter[] spars = new SqlParameter[5];
                                    spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                                    spars[0].Value = "InsertNewProjectLocationforABAP";

                                    spars[1] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
                                    spars[1].Value = projectCode.ToString().Trim();

                                    objDS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
                                    if (objDS != null && objDS.Tables.Count > 0)
                                    {
                                        if (objDS.Tables[0].Rows.Count > 0)
                                        {
                                            comp_location_code = Convert.ToString(objDS.Tables[0].Rows[0]["comp_code"]);
                                        }
                                    }
                                }
                                #endregion

                                #region Check Dev Description is available in Task Master List, if not then insert in task master list.
                                if (!string.IsNullOrEmpty(objDevDesc))
                                {
                                    DataSet objDS = new DataSet();
                                    SqlParameter[] spars = new SqlParameter[5];
                                    spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                                    spars[0].Value = "InsertNewTaskforABAP";

                                    spars[1] = new SqlParameter("@Task_Desc", SqlDbType.VarChar);
                                    spars[1].Value = objDevDesc.ToString().Trim();

                                    spars[2] = new SqlParameter("@TimesheetView", SqlDbType.VarChar);
                                    spars[2].Value = "ABAP";

                                    objDS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
                                    if (objDS != null && objDS.Tables.Count > 0)
                                    {
                                        if (objDS.Tables[0].Rows.Count > 0)
                                        {
                                            taskId = Convert.ToInt32(objDS.Tables[0].Rows[0]["ActivityId"]);
                                        }
                                    }
                                }
                                #endregion

                                DataSet objTimesheetDS = new DataSet();
                                var emp_code = Session["Empcode"].ToString();
                                var hour = textBoxValue;
                                var status = "Created";
                                var Description = "";

                                //Check Total Hr Is not in 24 Hr 
                                if (!CheckIsNot24Hr(Timesheet_date, hour, 0, emp_code))
                                {
                                    lblTimesheetErrorMsg.Text = "Please enter less than 24 hours for the entire day.";
                                    return;
                                }

                                spm.InsertTimeSheet("INSERTTIMESHEET", 0, emp_code, Timesheet_date, projectCode, taskId, hour, status, Description);
                                lblTimesheetErrorMsg.Text = "You have add timesheet successfully ";
                            }
                        }
                    }
                }
            }
            Response.Redirect("~/procs/ABAP_Object_Tracker_Change_Status_ABAP.aspx");

        }
    }

    public bool CheckIsNot24Hr(string timesheetDate, string Hours, double id, string emp_code)
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[5];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetTotalHRDayTimesheet";

            spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            spars[1].Value = emp_code;

            spars[2] = new SqlParameter("@Timesheet_date", SqlDbType.VarChar);
            spars[2].Value = timesheetDate;

            spars[3] = new SqlParameter("@Hours", SqlDbType.VarChar);
            spars[3].Value = Hours;

            spars[4] = new SqlParameter("@Id", SqlDbType.BigInt);
            spars[4].Value = id;

            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_Timesheet");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["Message"]);
                var gethr = Convert.ToString(getdtDetails.Rows[0]["HR"]);
                if (getStatus == "INSERT")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public void DeleteTimesheetRecord(GridView gv)
    {
        string projectCode = Session["projectCode"].ToString();
        DataSet objDS = new DataSet();

        for (int i = 5; i < gvTimesheet.Rows.Count; i++)
        {
            string timesheetcolumnheader = gvTimesheet.HeaderRow.Cells[i].Text.Trim();
            string[] parts = timesheetcolumnheader.Split('-');
            timesheetcolumnheader = parts[2] + "-" + parts[1] + "-" + parts[0];

            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "DeleteExstingTimesheetForABAP";

            spars[1] = new SqlParameter("@Timesheet_date", SqlDbType.VarChar);
            spars[1].Value = timesheetcolumnheader.ToString().Trim();

            spars[2] = new SqlParameter("@ProjectName", SqlDbType.VarChar);
            spars[2].Value = projectCode.ToString().Trim();

            objDS = spm.getDatasetList(spars, "SP_Timesheet");
        }


    }

    protected void btn_AddOtherTask_Click(object sender, EventArgs e)
    {
        if (liDate.Visible)
        {
            btn_AddOtherTask.Text = "+";
            liDate.Visible = false;
            liHrs.Visible = false;
            liProject.Visible = false;
            liTask.Visible = false;
            btn_SaveAddedTask.Visible = false;
            lblmessage.Visible = false;
        }
        else
        {
            btn_AddOtherTask.Text = "-";
            liDate.Visible = true;
            liHrs.Visible = true;
            liProject.Visible = true;
            liTask.Visible = true;
            btn_SaveAddedTask.Visible = true;
            lblmessage.Visible = true;
        }
    }

    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            var getDate = txtFromdate.Text.ToString();
            var startDateText = hdnStartDate.Value;
            var endDateText = hdnEndDate.Value;
            DateTime StartDate = DateTime.ParseExact(startDateText, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            DateTime EndDate = DateTime.ParseExact(endDateText, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            DateTime SelectedDate = DateTime.ParseExact(getDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime Today = DateTime.Now;

            if (Today < SelectedDate)
            {
                lblmessage.Text = "Please select date only today or less than today";
                txtFromdate.Text = "";
                return;
            }
            else if (StartDate > SelectedDate || EndDate < SelectedDate)
            {
                lblmessage.Text = "Please select date range between " + startDateText + "  to " + endDateText;
                txtFromdate.Text = "";
                return;
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
    #endregion

    protected void btn_SaveAddedTask_Click(object send, EventArgs e)
    {
        if (txtFromdate.Text.Trim() == "")
        {
            lblmessage.Text = "Please select date";
            return;
        }
        if (txtHours.Text.Trim() == "")
        {
            lblmessage.Text = "Please enter hours";
            return;
        }
        if (ddlProject.SelectedValue.Trim() == "0")
        {
            lblmessage.Text = "Please select project";
            return;
        }
        if (ddlTask.SelectedValue.Trim() == "0")
        {
            lblmessage.Text = "Please select task";
            return;
        }
        if (Convert.ToString(txtHours.Text).Trim() != "")
        {
            var strdate = Convert.ToString(txtHours.Text).Trim().Split(':');
            if (strdate.Length == 2)
            {
                if (strdate[0].Length != 2 || strdate[1].Length != 2)
                {
                    lblmessage.Text = "Please enter correct  hours.";
                    return;
                }
                else
                {
                    if (Convert.ToInt32(strdate[0].ToString()) >= 24)
                    {
                        lblmessage.Text = "Please enter less than 24 hours.";
                        return;
                    }
                    if (Convert.ToInt32(strdate[1].ToString()) > 59)
                    {
                        lblmessage.Text = "Please enter correct hours.";
                        return;
                    }
                    if (Convert.ToInt32(strdate[0].ToString()) <= 00 && Convert.ToInt32(strdate[1].ToString()) <= 00)
                    {
                        lblmessage.Text = "Please enter correct hours.";
                        return;
                    }
                }
            }
            else
            {
                lblmessage.Text = "Please enter correct hours.";
                return;
            }
        }

        //Save as Draft in temp table in sql
        SqlParameter[] spars = new SqlParameter[6];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "SaveOtherTaskAsDraft";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();

        spars[2] = new SqlParameter("@Timesheet_date", SqlDbType.VarChar);
        spars[2].Value = txtFromdate.Text.Trim();

        spars[3] = new SqlParameter("@ProjectLocation", SqlDbType.VarChar);
        spars[3].Value = ddlProject.SelectedValue.Trim();

        spars[4] = new SqlParameter("@TaskId", SqlDbType.VarChar);
        spars[4].Value = ddlTask.SelectedValue.Trim();

        spars[5] = new SqlParameter("@Hours", SqlDbType.VarChar);
        spars[5].Value = txtHours.Text.Trim();

        DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToString(DS.Tables[0].Rows[0]["ResponseMsg"]).Trim() == "Temp record added successfully.")
            {
                txtFromdate.Text = "";
                txtHours.Text = "";
                ddlProject.SelectedValue = "0";
                ddlTask.SelectedValue = "0";
                lblmessage.Text = "Other task added successfully.";
                btn_ViewTimesheet_Click();
            }
        }
    }


    public void GetMasterList()
    {
        #region Get Project Location and Task List
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "GetOtherTask";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();

        DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (DS != null && DS.Tables.Count > 0)
        {
            if (DS.Tables[0].Rows.Count > 0)
            {
                ddlProject.DataSource = DS.Tables[0];
                ddlProject.DataTextField = "Location_name";
                ddlProject.DataValueField = "comp_code";
                ddlProject.DataBind();
                ddlProject.Items.Insert(0, new ListItem("Select Project Location", "0"));

            }
            if (DS.Tables[1].Rows.Count > 0)
            {
                ddlTask.DataSource = DS.Tables[1];
                ddlTask.DataTextField = "Development_Desc";
                ddlTask.DataValueField = "Activity_Id";
                ddlTask.DataBind();
                ddlTask.Items.Insert(0, new ListItem("Select Task Location", "0"));
            }
        }
        #endregion
    }
}