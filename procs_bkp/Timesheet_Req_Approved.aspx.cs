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

public partial class procs_Timesheet_Req_Approved : System.Web.UI.Page
{

    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;

    SP_Methods spm = new SP_Methods();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    DataTable dtLeaveDetails;
    DataSet dsLeaveRequst;
    string strempcode = "";

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    #region Page Events
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }


            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                lpm.Emp_Code = strempcode;
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString.Count > 0)
                    {
                        //s  hdnReqid.Value = "1";
                        hdnReqid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                    }
                    editform.Visible = true;

                    getMngLeaveReqstList(Convert.ToDouble(hdnReqid.Value));
                    PopulateEmployeeData();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
    protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            //hdnReqid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
            //hdnleaveTypeid.Value = Convert.ToString(gvMngLeaveRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
            //   getSelectedEmpLeaveDetails_View();
            //Response.Redirect("Leave_Req_View.aspx?reqid=" + hdnReqid.Value);
            if (hdnleaveTypeid.Value == "11")
            {
                Response.Redirect("Encash_leave.aspx?reqid=" + hdnReqid.Value);
            }
            else
            {
                Response.Redirect("Attend_Req.aspx?reqid=" + hdnReqid.Value);
            }




        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    //protected void gvMngLeaveRqstList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    getMngLeaveReqstList(Convert.ToInt32(hdnReqid.Value));
    //    gvMngLeaveRqstList.PageIndex = e.NewPageIndex;
    //    gvMngLeaveRqstList.DataSource = dsLeaveRequst;
    //    gvMngLeaveRqstList.DataBind();
    //}
    #endregion

    #region Page Methods
    private void getMngLeaveReqstList(double id)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        string strSelfApprover = "";
        try
        {
            //GetStart Date And End Date
            var getdtStartDate = spm.GetWeekStartDateEndDate(id);
            if (getdtStartDate.Rows.Count > 0)
            {
                var startDate = Convert.ToString(getdtStartDate.Rows[0]["StartDateByid"]);
                var EndDate = Convert.ToString(getdtStartDate.Rows[0]["EndDateById"]);
                hdnStartDate.Value = startDate;
                hdnEndDate.Value = EndDate;
            }

            DataSet dsTimesheet = new DataSet();
            dsTimesheet = spm.GetApprovedTimesheetRMInboxDetail(id);


            if (dsTimesheet.Tables.Count > 0)
            {
                var dsTrDetails = dsTimesheet.Tables[0];

                if (dsTrDetails.Rows.Count > 0)
                {
                    var getempCode = Convert.ToString(dsTrDetails.Rows[0]["emp_code"]);
                    var req_Date = Convert.ToString(dsTrDetails.Rows[0]["Updated_on"]);
                    lpm.Emp_Code = getempCode;
                    txtDate.Text = req_Date;
                    GridView1.DataSource = dsTrDetails;
                    GridView1.DataBind();
                    var dtTotalTime = dsTimesheet.Tables[1];
                    BindHTML(dsTrDetails, dtTotalTime);
                    //Display details
                    var dtdate = dsTimesheet.Tables[2];//Get All Date 
                    var dt1 = dsTimesheet.Tables[3];// Date 1
                    var dt2 = dsTimesheet.Tables[4];// Date 2
                    var dt3 = dsTimesheet.Tables[5];// Date 3
                    var dt4 = dsTimesheet.Tables[6];// Date 4
                    var dt5 = dsTimesheet.Tables[7];// Date 5
                    var dt6 = dsTimesheet.Tables[8];// Date 6
                    var dt7 = dsTimesheet.Tables[9];// Date 7
                    BindHTMLDetails(dtdate, dt1, dt2, dt3, dt4, dt5, dt6, dt7);

                    HFSubmitedDate.Value = Convert.ToString(dsTimesheet.Tables[10].Rows[0]["Record"]);

                    if (HFSubmitedDate.Value == "0")
                    {
                        btnback_mng.Visible = false;
                    }

                }
                else
                {
                   // btnIn.Visible = false;
                }
            }



        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void BindHTML(DataTable dt, DataTable dtTime)
    {
        //Populating a DataTable from database.
        // DataTable dt = this.GetData();
        //Building an HTML string.
        StringBuilder html = new StringBuilder();
        //Table start.
        html.Append("<table runat='server' border = '1' cellspacing='0' width:'137% !important'; id='gvMain' style='border-collapse: collapse; border-color: black;'>");
        //Building the Header row.
        html.Append("<tr class=''>");
        foreach (DataColumn column in dt.Columns)
        {

            if (column.ColumnName == "Updated_on" || column.ColumnName == "Emp_Code")
            {
                //string strDate = column.ColumnName.Substring(8, 2); // +"-" + column.ColumnName.Substring(5, 2) + "-" + column.ColumnName.Substring(0, 4);
                ////html.Append(column.ColumnName);
                //html.Append(strDate);
            }
            else if (column.ColumnName == "Project Name" || column.ColumnName == "Activity Desc")
            {
                html.Append("<th style='background-color: #C7D3D4;'>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            else
            {
                Button btn = new Button();
                btn.ID = column.ColumnName;
                btn.Text = column.ColumnName;
                btn.Click += btnTimesheetDetails_Click;

                html.Append("<th style='background-color: #C7D3D4;'>");
                html.Append(btn.Text);
                html.Append("</th>");
            }

        }
        html.Append("</tr>");
        // {background-color: rgb(201, 76, 76);}
        //Building the Data rows.
        foreach (DataRow row in dt.Rows)
        {
            html.Append("<tr class=''>");

            foreach (DataColumn column in dt.Columns)
            {
                var name = column.ColumnName.ToString();
                if (name == "Updated_on" || name == "Emp_Code")
                {
                    //row[column.ColumnName] = "H";
                    ////html.Append("<td style='background-color: rgb(255, 153, 51);'>");
                    //html.Append("<td style='color: #FF9933;background-color: #FF9933;width:8px;'>");
                }
                else if (name == "Project Name" || name == "Activity Desc")
                {
                    //row[column.ColumnName] = "W";
                    //cell.BackColor = System.Drawing.Color.FromArgb(252, 92, 84);
                    html.Append("<td width='20px'>");
                    html.Append(row[column.ColumnName]);
                    html.Append("</td>");
                    // html.Append("<td style='color: rgb(253, 242, 185);background-color: rgb(253, 242, 185);width:8px;'>");
                }
                else
                {
                    var getDate = column.ColumnName.ToString();
                    var splitDate = getDate.Split('-');
                    var finaldate = splitDate[2] + "-" + splitDate[1] + "-" + splitDate[0];
                    var getDt = spm.GetTimesheetRegInbox(lpm.Emp_Code, finaldate);
                    var getVal = Convert.ToString(getDt.Rows[0]["ISWORKING"]);
                    if (getVal == "WK")
                    {
                        html.Append("<td width='20px' style='color: black;background-color: #C0C0C0;width:8px;'>");
                    }
                    else if (getVal == "HO")
                    {
                        html.Append("<td width='20px' style='color: black;background-color: #FF9933;width:8px;'>");
                    }
                    else if (getVal == "LE")
                    {
                        html.Append("<td width='20px' style='color: black;background-color: yellow;width:8px;'>");
                    }
                    else
                    {
                        html.Append("<td width='20px'>");
                    }

                    html.Append(row[column.ColumnName]);
                    //html.Append(row[column.ColumnName]);
                    html.Append("</td>");
                }
                // html.Append("<td>");

            }
            html.Append("</tr>");
        }
        foreach (DataRow row in dtTime.Rows)
        {
            html.Append("<tr class=''>");
            html.Append("<td colspan='2' style='text-align:center !important'>Total</td>");
            foreach (DataColumn column in dtTime.Columns)
            {
                //var name = column.ColumnName.ToString();
                //var getDate = column.ColumnName.ToString();
                //var splitDate = getDate.Split('-');
                //var finaldate = splitDate[2] + "-" + splitDate[1] + "-" + splitDate[0];
                //var getDt = spm.GetTimesheetRegInbox(lpm.Emp_Code, finaldate);
                //var getVal = Convert.ToString(getDt.Rows[0]["ISWORKING"]);
                //if (getVal == "WK")
                //{
                //    html.Append("<td>");
                //    //html.Append("</td>");
                //}
                //else if (getVal == "HO")
                //{
                //    html.Append("<td>");
                //   // html.Append("</td>");
                //}
                //else if (getVal == "LE")
                //{
                //    html.Append("<td>");
                //   // html.Append("</td>");
                //}
                //else
                //{
                //    //html.Append("<td width='20px'>");


                //}
                html.Append("<td>");
                html.Append(row[column.ColumnName]);
                html.Append("</td>");

                //html.Append(row[column.ColumnName]);

            }
            html.Append("</tr>");
        }
        //Table end.

        //Leave Type Hours as per Shift
        foreach (DataRow row in dtTime.Rows)
        {
            html.Append("<tr class='GridViewScrollItem' style='color: black;background-color: #f7d240;'>");
            html.Append("<td colspan='2' style='text-align:center !important'>Leave Type</td>");
            foreach (DataColumn column in dtTime.Columns)
            {
                html.Append("<td style='text-align:center !important'>");
                var getDate = column.ColumnName.ToString();
                var splitDate = getDate.Split('-');
                var finaldate = splitDate[2] + "-" + splitDate[1] + "-" + splitDate[0];
                var startDateText = hdnStartDate.Value;
                DateTime StartDate = DateTime.ParseExact(startDateText, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime CurrentDate = DateTime.ParseExact(finaldate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                //if (StartDate <= CurrentDate)
                //{
                var getDt = spm.GetTimesheetRegInbox(lpm.Emp_Code, finaldate);
                var getVal = Convert.ToString(getDt.Rows[0]["ISWORKING"]);
                if (getVal == "WK")
                {
                    html.Append("");
                }
                else if (getVal == "HO")
                {
                    html.Append("");
                }
                else if (getVal == "LE")
                {
                    var getFor_FromLeave = Convert.ToString(getDt.Rows[0]["For_FromLeave"]);
                    var getFor_ToLeave = Convert.ToString(getDt.Rows[0]["For_ToLeave"]);
                    var getReason = Convert.ToString(getDt.Rows[0]["Reason"]);//TotalCount
                    if (getReason == "Incomplete Timesheet")
                    {
                        if ((getFor_FromLeave == "First Half" || getFor_FromLeave == "Second Half") || (getFor_ToLeave == "First Half" || getFor_ToLeave == "Second Half"))
                        {
                            //html.Append("Half Day");
                            html.Append("TS");
                        }
                        else
                        {
                            //html.Append("Fullday");
                            html.Append("TS");
                        }
                    }
                    else if (getReason == "Missing Punch")
                    {
                        if ((getFor_FromLeave == "First Half" || getFor_FromLeave == "Second Half") || (getFor_ToLeave == "First Half" || getFor_ToLeave == "Second Half"))
                        {
                            //html.Append("Half Day");
                            html.Append("ATT");
                        }
                        else
                        {
                            //html.Append("Fullday");
                            html.Append("ATT");
                        }
                    }
                    else
                    {
                        html.Append("Leave");
                    }
                }
                //}
                //else
                //{
                //    html.Append("");
                //}
                html.Append("</td>");
            }
            html.Append("</tr>");
        }
        //Table end.

        html.Append("</table>");
        //Append the HTML string to Placeholder.
        PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
    }

    public void BindHTMLDetails(DataTable dtDate, DataTable dt1, DataTable dt2, DataTable dt3, DataTable dt4, DataTable dt5, DataTable dt6, DataTable dt7)
    {
        try
        {
            //Get Date
            if (dtDate.Rows.Count > 0)
            {
                btn1.InnerText = Convert.ToString(dtDate.Rows[0]["D1"]);
                btn2.InnerText = Convert.ToString(dtDate.Rows[0]["D2"]);
                btn3.InnerText = Convert.ToString(dtDate.Rows[0]["D3"]);
                btn4.InnerText = Convert.ToString(dtDate.Rows[0]["D4"]);
                btn5.InnerText = Convert.ToString(dtDate.Rows[0]["D5"]);
                btn6.InnerText = Convert.ToString(dtDate.Rows[0]["D6"]);
                btn7.InnerText = Convert.ToString(dtDate.Rows[0]["D7"]);
            }
            //
            //Dt1
            var getDt1 = dt1;
            if (getDt1.Rows.Count > 0)
            {
                StringBuilder html = new StringBuilder();
                //Table start.
                html.Append("<table runat='server' border = '1' cellspacing='0' width:'101% !important;' id='gvMain1' style='border-collapse: collapse; border-color: black;width: 100% !important;'>");
                //Building the Header row.
                html.Append("<tr class=''>");
                html.Append("<th style='background-color: #C7D3D4;'>Project Name</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Activity Desc</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Hours</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Description</th>");
                html.Append("</tr>");
                foreach (DataRow item in getDt1.Rows)
                {
                    html.Append("<tr class=''>");
                    html.Append("<td style='width: 23% !important;'>" + Convert.ToString(item["ProjectName"]) + "</td>");
                    html.Append("<td style='width: 25% !important;'>" + Convert.ToString(item["Activity_Desc"]) + "</td>");
                    html.Append("<td style='width: 5% !important;'>" + Convert.ToString(item["Hours"]) + "</td>");
                    html.Append("<td>" + Convert.ToString(item["Description"]) + "</td>");
                    html.Append("</tr>");
                }
                //Table end.
                html.Append("</table>");
                //Append the HTML string to Placeholder.
                PlaceHolder2.Controls.Add(new Literal { Text = html.ToString() });
            }
            else
            {
                StringBuilder html = new StringBuilder();
                //Table start.
                html.Append("<table runat='server' border = '1' cellspacing='0' width:'101% !important;' id='gvMain1' style='border-collapse: collapse; border-color: black;width: 100% !important;'>");
                //Building the Header row.
                html.Append("<tr class=''>");
                html.Append("<th style='background-color: #C7D3D4;'>Project Name</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Activity Desc</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Hours</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Description</th>");
                html.Append("</tr>");
                html.Append("<tr class=''>");
                html.Append("<td colspan='4' style='text-align: center;'>No Data Found</td>");
                html.Append("</tr>");
                //Table end.
                html.Append("</table>");
                //Append the HTML string to Placeholder.
                PlaceHolder2.Controls.Add(new Literal { Text = html.ToString() });
            }
            //Dt2
            var getDt2 = dt2;
            if (getDt2.Rows.Count > 0)
            {
                StringBuilder html = new StringBuilder();
                //Table start.
                html.Append("<table runat='server' border = '1' cellspacing='0' width:'100% !important;' id='gvMain2' style='border-collapse: collapse; border-color: black;width: 100% !important;'>");
                //Building the Header row.
                html.Append("<tr class=''>");
                html.Append("<th style='background-color: #C7D3D4;'>Project Name</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Activity Desc</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Hours</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Description</th>");
                html.Append("</tr>");
                foreach (DataRow item in getDt2.Rows)
                {
                    html.Append("<tr class=''>");
                    html.Append("<td style='width: 23% !important;'>" + Convert.ToString(item["ProjectName"]) + "</td>");
                    html.Append("<td style='width: 25% !important;'>" + Convert.ToString(item["Activity_Desc"]) + "</td>");
                    html.Append("<td style='width: 5% !important;'>" + Convert.ToString(item["Hours"]) + "</td>");
                    html.Append("<td>" + Convert.ToString(item["Description"]) + "</td>");
                    html.Append("</tr>");
                }
                //Table end.
                html.Append("</table>");
                //Append the HTML string to Placeholder.
                PlaceHolder3.Controls.Add(new Literal { Text = html.ToString() });
            }
            else
            {
                StringBuilder html = new StringBuilder();
                //Table start.
                html.Append("<table runat='server' border = '1' cellspacing='0' width:'101% !important;' id='gvMain1' style='border-collapse: collapse; border-color: black;width: 100% !important;'>");
                //Building the Header row.
                html.Append("<tr class=''>");
                html.Append("<th style='background-color: #C7D3D4;'>Project Name</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Activity Desc</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Hours</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Description</th>");
                html.Append("</tr>");
                html.Append("<tr class=''>");
                html.Append("<td colspan='4' style='text-align: center;'>No Data Found</td>");
                html.Append("</tr>");
                //Table end.
                html.Append("</table>");
                //Append the HTML string to Placeholder.
                PlaceHolder3.Controls.Add(new Literal { Text = html.ToString() });
            }
            //Dt3
            var getDt3 = dt3;
            if (getDt3.Rows.Count > 0)
            {
                StringBuilder html = new StringBuilder();
                //Table start.
                html.Append("<table runat='server' border = '1' cellspacing='0' width:'100% !important;' id='gvMain3' style='border-collapse: collapse; border-color: black;width: 100% !important;'>");
                //Building the Header row.
                html.Append("<tr class=''>");
                html.Append("<th style='background-color: #C7D3D4;'>Project Name</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Activity Desc</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Hours</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Description</th>");
                html.Append("</tr>");
                foreach (DataRow item in getDt3.Rows)
                {
                    html.Append("<tr class=''>");
                    html.Append("<td style='width: 23% !important;'>" + Convert.ToString(item["ProjectName"]) + "</td>");
                    html.Append("<td style='width: 25% !important;'>" + Convert.ToString(item["Activity_Desc"]) + "</td>");
                    html.Append("<td style='width: 5% !important;'>" + Convert.ToString(item["Hours"]) + "</td>");
                    html.Append("<td>" + Convert.ToString(item["Description"]) + "</td>");
                    html.Append("</tr>");
                }

                //Table end.
                html.Append("</table>");
                //Append the HTML string to Placeholder.
                PlaceHolder4.Controls.Add(new Literal { Text = html.ToString() });
            }
            else
            {
                StringBuilder html = new StringBuilder();
                //Table start.
                html.Append("<table runat='server' border = '1' cellspacing='0' width:'101% !important;' id='gvMain1' style='border-collapse: collapse; border-color: black;width: 100% !important;'>");
                //Building the Header row.
                html.Append("<tr class=''>");
                html.Append("<th style='background-color: #C7D3D4;'>Project Name</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Activity Desc</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Hours</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Description</th>");
                html.Append("</tr>");
                html.Append("<tr class=''>");
                html.Append("<td colspan='4' style='text-align: center;'>No Data Found</td>");
                html.Append("</tr>");
                //Table end.
                html.Append("</table>");
                //Append the HTML string to Placeholder.
                PlaceHolder4.Controls.Add(new Literal { Text = html.ToString() });
            }
            //Dt4
            var getDt4 = dt4;
            if (getDt4.Rows.Count > 0)
            {
                StringBuilder html = new StringBuilder();
                //Table start.
                html.Append("<table runat='server' border = '1' cellspacing='0' width:'100% !important;' id='gvMain4' style='border-collapse: collapse; border-color: black;width: 100% !important;'>");
                //Building the Header row.
                html.Append("<tr class=''>");
                html.Append("<th style='background-color: #C7D3D4;'>Project Name</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Activity Desc</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Hours</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Description</th>");
                html.Append("</tr>");
                foreach (DataRow item in getDt4.Rows)
                {
                    html.Append("<tr class=''>");
                    html.Append("<td style='width: 23% !important;'>" + Convert.ToString(item["ProjectName"]) + "</td>");
                    html.Append("<td style='width: 25% !important;'>" + Convert.ToString(item["Activity_Desc"]) + "</td>");
                    html.Append("<td style='width: 5% !important;'>" + Convert.ToString(item["Hours"]) + "</td>");
                    html.Append("<td>" + Convert.ToString(item["Description"]) + "</td>");
                    html.Append("</tr>");
                }
                //Table end.
                html.Append("</table>");
                //Append the HTML string to Placeholder.
                PlaceHolder5.Controls.Add(new Literal { Text = html.ToString() });
            }
            else
            {
                StringBuilder html = new StringBuilder();
                //Table start.
                html.Append("<table runat='server' border = '1' cellspacing='0' width:'101% !important;' id='gvMain1' style='border-collapse: collapse; border-color: black;width: 100% !important;'>");
                //Building the Header row.
                html.Append("<tr class=''>");
                html.Append("<th style='background-color: #C7D3D4;'>Project Name</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Activity Desc</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Hours</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Description</th>");
                html.Append("</tr>");
                html.Append("<tr class=''>");
                html.Append("<td colspan='4' style='text-align: center;'>No Data Found</td>");
                html.Append("</tr>");
                //Table end.
                html.Append("</table>");
                //Append the HTML string to Placeholder.
                PlaceHolder5.Controls.Add(new Literal { Text = html.ToString() });
            }
            //Dt4
            var getDt5 = dt5;
            if (getDt5.Rows.Count > 0)
            {
                StringBuilder html = new StringBuilder();
                //Table start.
                html.Append("<table runat='server' border = '1' cellspacing='0' width:'100% !important;' id='gvMain5' style='border-collapse: collapse; border-color: black;width: 100% !important;'>");
                //Building the Header row.
                html.Append("<tr class=''>");
                html.Append("<th style='background-color: #C7D3D4;'>Project Name</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Activity Desc</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Hours</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Description</th>");
                html.Append("</tr>");
                foreach (DataRow item in getDt5.Rows)
                {
                    html.Append("<tr class=''>");
                    html.Append("<td style='width: 23% !important;'>" + Convert.ToString(item["ProjectName"]) + "</td>");
                    html.Append("<td style='width: 25% !important;'>" + Convert.ToString(item["Activity_Desc"]) + "</td>");
                    html.Append("<td style='width: 5% !important;'>" + Convert.ToString(item["Hours"]) + "</td>");
                    html.Append("<td>" + Convert.ToString(item["Description"]) + "</td>");
                    html.Append("</tr>");
                }
                //Table end.
                html.Append("</table>");
                //Append the HTML string to Placeholder.
                PlaceHolder6.Controls.Add(new Literal { Text = html.ToString() });
            }
            else
            {
                StringBuilder html = new StringBuilder();
                //Table start.
                html.Append("<table runat='server' border = '1' cellspacing='0' width:'101% !important;' id='gvMain1' style='border-collapse: collapse; border-color: black;width: 100% !important;'>");
                //Building the Header row.
                html.Append("<tr class=''>");
                html.Append("<th style='background-color: #C7D3D4;'>Project Name</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Activity Desc</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Hours</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Description</th>");
                html.Append("</tr>");
                html.Append("<tr class=''>");
                html.Append("<td colspan='4' style='text-align: center;'>No Data Found</td>");
                html.Append("</tr>");
                //Table end.
                html.Append("</table>");
                //Append the HTML string to Placeholder.
                PlaceHolder6.Controls.Add(new Literal { Text = html.ToString() });
            }
            //Dt6
            var getDt6 = dt6;
            if (getDt6.Rows.Count > 0)
            {
                StringBuilder html = new StringBuilder();
                //Table start.
                html.Append("<table runat='server' border = '1' cellspacing='0' width:'100% !important;' id='gvMain6' style='border-collapse: collapse; border-color: black;width: 100% !important;'>");
                //Building the Header row.
                html.Append("<tr class=''>");
                html.Append("<th style='background-color: #C7D3D4;'>Project Name</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Activity Desc</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Hours</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Description</th>");
                html.Append("</tr>");
                foreach (DataRow item in getDt6.Rows)
                {
                    html.Append("<tr class=''>");
                    html.Append("<td style='width: 23% !important;'>" + Convert.ToString(item["ProjectName"]) + "</td>");
                    html.Append("<td style='width: 25% !important;'>" + Convert.ToString(item["Activity_Desc"]) + "</td>");
                    html.Append("<td style='width: 5% !important;'>" + Convert.ToString(item["Hours"]) + "</td>");
                    html.Append("<td>" + Convert.ToString(item["Description"]) + "</td>");
                    html.Append("</tr>");
                }
                //Table end.
                html.Append("</table>");
                //Append the HTML string to Placeholder.
                PlaceHolder7.Controls.Add(new Literal { Text = html.ToString() });
            }
            else
            {
                StringBuilder html = new StringBuilder();
                //Table start.
                html.Append("<table runat='server' border = '1' cellspacing='0' width:'101% !important;' id='gvMain1' style='border-collapse: collapse; border-color: black;width: 100% !important;'>");
                //Building the Header row.
                html.Append("<tr class=''>");
                html.Append("<th style='background-color: #C7D3D4;'>Project Name</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Activity Desc</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Hours</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Description</th>");
                html.Append("</tr>");
                html.Append("<tr class=''>");
                html.Append("<td colspan='4' style='text-align: center;'>No Data Found</td>");
                html.Append("</tr>");
                //Table end.
                html.Append("</table>");
                //Append the HTML string to Placeholder.
                PlaceHolder7.Controls.Add(new Literal { Text = html.ToString() });
            }
            //Dt7
            var getDt7 = dt7;
            if (getDt7.Rows.Count > 0)
            {
                StringBuilder html = new StringBuilder();
                //Table start.
                html.Append("<table runat='server' border = '1' cellspacing='0' width:'100% !important;' id='gvMain7' style='border-collapse: collapse; border-color: black;width: 100% !important;'>");
                //Building the Header row.
                html.Append("<tr class=''>");
                html.Append("<th style='background-color: #C7D3D4;'>Project Name</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Activity Desc</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Hours</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Description</th>");
                html.Append("</tr>");
                foreach (DataRow item in getDt7.Rows)
                {
                    html.Append("<tr class=''>");
                    html.Append("<td style='width: 23% !important;'>" + Convert.ToString(item["ProjectName"]) + "</td>");
                    html.Append("<td style='width: 25% !important;'>" + Convert.ToString(item["Activity_Desc"]) + "</td>");
                    html.Append("<td style='width: 5% !important;'>" + Convert.ToString(item["Hours"]) + "</td>");
                    html.Append("<td>" + Convert.ToString(item["Description"]) + "</td>");
                    html.Append("</tr>");
                }

                //Table end.
                html.Append("</table>");
                //Append the HTML string to Placeholder.
                PlaceHolder8.Controls.Add(new Literal { Text = html.ToString() });
            }
            else
            {
                StringBuilder html = new StringBuilder();
                //Table start.
                html.Append("<table runat='server' border = '1' cellspacing='0' width:'101% !important;' id='gvMain1' style='border-collapse: collapse; border-color: black;width: 100% !important;'>");
                //Building the Header row.
                html.Append("<tr class=''>");
                html.Append("<th style='background-color: #C7D3D4;'>Project Name</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Activity Desc</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Hours</th>");
                html.Append("<th style='background-color: #C7D3D4;'>Description</th>");
                html.Append("</tr>");
                html.Append("<tr class=''>");
                html.Append("<td colspan='4' style='text-align: center;'>No Data Found</td>");
                html.Append("</tr>");
                //Table end.
                html.Append("</table>");
                //Append the HTML string to Placeholder.
                PlaceHolder8.Controls.Add(new Literal { Text = html.ToString() });
            }
        }
        catch (Exception)
        {

        }
    }
    #endregion


    #region code Not in use

    public void PopulateEmployeeData()
    {
        try
        {
            // txtEmpCode.Text = lpm.Emp_Code;
            //btnSave.Enabled = false; 
            var dtEmp = spm.GETEMPDETAIL_ATT(lpm.Emp_Code);

            if (dtEmp.Rows.Count > 0)
            {
                lpm.Emp_Status = (string)dtEmp.Rows[0]["Emp_status"];

                //lblmessage.Text = "You are not allowed to apply for any type of leave sicne your employee status is in resignation";
                // IsEnabledFalse(false);                
                txt_EmpCode.Text = lpm.Emp_Code;
                txtEmp_Name.Text = (string)dtEmp.Rows[0]["Emp_Name"];
                txtEmp_Desigantion.Text = (string)dtEmp.Rows[0]["DesginationName"];
                txtEmp_Department.Text = (string)dtEmp.Rows[0]["Department_Name"];
                txt_SapModule.Text = Convert.ToString(dtEmp.Rows[0]["ModuleDesc"]).Trim();
                txt_Project.Text = (string)dtEmp.Rows[0]["Project"];
                lpm.Grade = Convert.ToString(dtEmp.Rows[0]["Grade"]).Trim();
                lpm.EmailAddress = (string)dtEmp.Rows[0]["Emp_EmailAddress"];
                hdnEmpEmail.Value = (string)dtEmp.Rows[0]["Emp_EmailAddress"];
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }

    #endregion



    
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void btnback_mng_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            var Action = "Correction";
            var id = Convert.ToDouble(hdnReqid.Value);
            var remark = txtRemark.Text.ToString();
            //Send Email With 
            spm.ApproveCurrectionTimeSheet("GetApprovedCurrectionTimesheet", id, remark, Action);

            //Send Email With 
            string startDate = hdnStartDate.Value.ToString();
            string endDate = hdnEndDate.Value.ToString();
            var loginEmp_Name = Convert.ToString(Session["emp_loginName"]);
            var toEmailAddress = hdnEmpEmail.Value.ToString();
            // Response.Redirect("TimesheetRecord_Curr.aspx?reqid=" + hdnReqid.Value + "&app=a");
            string reg_App = "http://localhost/hrms/login.aspx?ReturnUrl=procs/TimesheetRecord_Curr.aspx?reqid=" + hdnReqid.Value + "&app=a";
            string redirectURL = Convert.ToString(reg_App).Trim();
            var returnUrl = redirectURL;
            var subject = "OneHR - Correction Timesheet " + startDate + " - " + endDate;

            spm.Timesheet_Email_To_Employee(toEmailAddress, txtEmp_Name.Text.ToString(), loginEmp_Name, returnUrl, subject, startDate, endDate, remark, 0);
            //
            Response.Redirect("~/procs/InboxTimesheet_Req.aspx");

        }
        catch (Exception ex)
        {

        }
    }

    protected void btnTimesheetDetails_Click(object sender, EventArgs e)
    {

    }

    

}