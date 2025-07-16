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

public partial class ABAP_Object_Tracker_Change_Status_ABAP_Timesheet : System.Web.UI.Page
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
        spars[0].Value = "GetABAPersTimsheetDetails";

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
                var objActivityDesc = HttpUtility.HtmlDecode(row.Cells[2].Text.ToString().Trim());

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
                        TextBox[] textboxes = new TextBox[]
                         {
                            (TextBox)row.FindControl("txt"),
                            (TextBox)row.FindControl("txt"),
                            (TextBox)row.FindControl("txt"),
                            (TextBox)row.FindControl("txtfirst"),
                            (TextBox)row.FindControl("txtSecond"),
                            (TextBox)row.FindControl("txtThird"),
                            (TextBox)row.FindControl("txtFourth"),
                            (TextBox)row.FindControl("txtFifth"),
                            (TextBox)row.FindControl("txtSixth"),
                            (TextBox)row.FindControl("txtSeventh")
                        };

                        for (int i = 0; i < dsGetTimesheetHours.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = dsGetTimesheetHours.Tables[0].Rows[i];
                            for (int j = 3; j < dr.ItemArray.Length; j++)
                            {
                                string dsvalue = dsGetTimesheetHours.Tables[0].Columns[i + 3].ToString();
                                string gvcolumnHeader = gvTimesheet.HeaderRow.Cells[i + 3].Text;
                                if (dsvalue == gvcolumnHeader)
                                {
                                    textboxes[j].Text = dr.ItemArray[j].ToString();
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
                    // Add the onblur event to call the JavaScript function
                    txtBox.Attributes["onblur"] = "validateTimesheet('" + txtBox.ClientID + "')";
                }
            }


            for (int i = 3; i < gvTimesheet.Columns.Count; i++)
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
            //for (int i = 3; i < gvTimesheet.Columns.Count; i++)
            //{
            //    var getDate = gvTimesheet.Columns[i].HeaderText.ToString();
            //    var splitDate = getDate.Split('-');
            //    var finaldate = splitDate[2] + "-" + splitDate[1] + "-" + splitDate[0];
            //    var getDt = spm.GetTimesheetRegInbox(Session["Empcode"].ToString()  , finaldate);
            //    var getVal = Convert.ToString(getDt.Rows[0]["ISWORKING"]);
            //    if (getVal == "WK")
            //    {
            //        e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#C0C0C0");
            //    }
            //    else if (getVal == "HO")
            //    {
            //        e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#FF9933");
            //    }
            //    else if (getVal == "LE")
            //    {
            //        e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
            //    }
            //    else
            //    {
            //        e.Row.Cells[i].BackColor = System.Drawing.Color.Transparent;
            //    }
            //}
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
                    string objDevDesc = HttpUtility.HtmlDecode(row.Cells[2].Text);

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
                            string headerText = gvTimesheet.HeaderRow.Cells[i + 3].Text;
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

        for (int i = 3; i < gvTimesheet.Rows.Count; i++)
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

    #endregion

}