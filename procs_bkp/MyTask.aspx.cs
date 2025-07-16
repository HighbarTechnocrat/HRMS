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
using System.Linq;
using ClosedXML.Excel;


public partial class MyTask : System.Web.UI.Page
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
                    var getVal = Convert.ToDouble(hdnTaskRefID.Value);
                 

                    var getResult = spm.getTaskMonitoringDDL(empCode, getVal, "GetMyTaskGird");
                    var getMyTaskList = getResult.Tables[0];

                    if (getMyTaskList.Rows.Count > 0)
                    {
                        BindMyTaskGrid(getMyTaskList);
                    }
                    else
                    {
                        lblmessage.Text = "Data Not found";
                    }                 
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

    private void BindMyTaskGrid(DataTable dataTable)
    {
        try
        {           
            gv_MyTask.DataSource = null;
            gv_MyTask.DataBind();
            if (dataTable.Rows.Count > 0)
            {
                gv_MyTask.DataSource = dataTable;
                gv_MyTask.DataBind();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }


    
    protected void MyTask_Edit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            var MyTask_EditVal = Convert.ToDouble(btn.CommandArgument.Trim());
           // Response.Redirect("~/procs/TaskMonitoring.aspx");
            Response.Redirect("~/procs/Update_Task.aspx?id=" + MyTask_EditVal);
        }
        catch (Exception)
        {

            throw;
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

    private void BindPage()
    {

        var empCode = Convert.ToString(Session["Empcode"]);
        var getVal = Convert.ToDouble(hdnTaskRefID.Value);

        var getResult = spm.getTaskMonitoringDDL(empCode, getVal, "GetMyTaskGird");
        var getMyTaskList = getResult.Tables[0];

        if (getMyTaskList.Rows.Count > 0)
        {
            BindMyTaskGrid(getMyTaskList);
        }
        else
        {
            lblmessage.Text = "Data Not found";
        }
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

	protected void lnk_Download_Click(object sender, ImageClickEventArgs e)
	{
		try
		{
			string TaskRef_ID = "";
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			TaskRef_ID = Convert.ToString(gv_MyTask.DataKeys[row.RowIndex].Values[0]).Trim();
			GetdownloadExcel(TaskRef_ID);
		}
		catch (Exception)
		{

			throw;
		}
	}
	public void GetdownloadExcel(string TaskRef_ID)
	{
		DataTable dtRequisitionDetails = new DataTable();
		lblmessage.Text = "";
		try
		{
			SqlParameter[] spars = new SqlParameter[7];
			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = "SupervisorExcel";
			spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(Session["Empcode"]);
			spars[2] = new SqlParameter("@Created_By", SqlDbType.VarChar);
			spars[2].Value = TaskRef_ID;
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
                //newTable.Columns.Add("isactive");
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
                    //_dr["isactive"] = item["isactive"].ToString();
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
}