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


public partial class ExecuterProcessedTask : System.Web.UI.Page
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
            var appType = "";
            var empCode = Convert.ToString(Session["Empcode"]);
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            //mobile_btnPrintPV.Visible = false;

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    appType = Convert.ToString(Request.QueryString["app"]);
					hdnType.Value = appType;
					Page.SmartNavigation = true;
                    
					if (hdnType.Value == "ex")
                    {
						EmpType.InnerText = "Task Supervisor";
						BindAllDLL("ExecuterDLL", "getExecuterDLL");
						liStatus.Visible = false;
						if (Session["ddlTaskExecuter"] != null)
						{	
						    ddlTaskExecuter.SelectedValue = Session["ddlTaskExecuter"].ToString();
							ddlTaskExecuter_SelectedIndexChanged(sender, e);
							if (Session["ddlTaskRefId"] != null)
							{
								ddlTaskRefId.SelectedValue = Session["ddlTaskRefId"].ToString();
								ddlTaskRefId_SelectedIndexChanged(sender, e);
							}
							if (Session["ddlTaskId"] != null)
							{
								ddlTaskId.SelectedValue = Session["ddlTaskId"].ToString();							
							}
							if (Session["ddlStatus"] != null)
							{
								ddlStatus.SelectedValue = Session["ddlStatus"].ToString();
							}
						}
							GetFilter_Task_Process("GetMyProcessedTaskExecuter");
                    }
                    else if (hdnType.Value == "su")
                    {
						EmpType.InnerText = "Task Executor";
						BindAllDLL("SupervisorDLL", "getSupervisorDLL");
						if (Session["ddlTaskExecuter"] != null)
						{
							ddlTaskExecuter.SelectedValue = Session["ddlTaskExecuter"].ToString();
							ddlTaskExecuter_SelectedIndexChanged(sender, e);
							if (Session["ddlTaskRefId"] != null)
							{
								ddlTaskRefId.SelectedValue = Session["ddlTaskRefId"].ToString();
								ddlTaskRefId_SelectedIndexChanged(sender, e);
							}
							if (Session["ddlTaskId"] != null)
							{
								ddlTaskId.SelectedValue = Session["ddlTaskId"].ToString();
							}
							if (Session["ddlStatus"] != null)
							{
								ddlStatus.SelectedValue = Session["ddlStatus"].ToString();
							}
						}
						GetFilter_Task_Process("GetMyProcessedTaskSuper");
						
					}
                    else
                    {

                    }

                }
                else
                {

                }
            }
         
              
        }
        catch (Exception ex)
        {
           
        }
    }
	public void BindAllDLL(string qtype, string Type)
	{
		try
		{
			var loginEmp_Code = Convert.ToString(Session["Empcode"]);
			var getDs = spm.getTaskMonitoringReport(Type, loginEmp_Code, 0, 0);
			if (getDs != null)
			{
				if (getDs.Tables.Count > 0)
				{
					var getRefIds = getDs.Tables[0];
					var getTaskIds = getDs.Tables[1];
					var getExecuter = getDs.Tables[2];
					var getStatus = getDs.Tables[3];
					ddlTaskRefId.DataSource = null;
					ddlTaskRefId.DataBind();

					ddlTaskId.DataSource = null;
					ddlTaskId.DataBind();

					ddlTaskExecuter.DataSource = null;
					ddlTaskExecuter.DataBind();

					ddlStatus.DataSource = null;
					ddlStatus.DataBind();
					if (getRefIds != null)
					{
						if (getRefIds.Rows.Count > 0)
						{
							ddlTaskRefId.DataSource = getRefIds;
							ddlTaskRefId.DataTextField = "Task_Reference_ID";
							ddlTaskRefId.DataValueField = "ID";
							ddlTaskRefId.DataBind();
							ddlTaskRefId.Items.Insert(0, new ListItem("Select Task Reference id", "0"));
						}
					}
					
					if (getTaskIds != null)
					{
						if (getTaskIds.Rows.Count > 0)
						{
							ddlTaskId.DataSource = getTaskIds;
							ddlTaskId.DataTextField = "Task_ID";
							ddlTaskId.DataValueField = "ID";
							ddlTaskId.DataBind();
							ddlTaskId.Items.Insert(0, new ListItem("Select Task id", "0"));
						}
					}
					if (getStatus != null)
					{
						if (getStatus.Rows.Count > 0)
						{
							ddlStatus.DataSource = getStatus;
							ddlStatus.DataTextField = "StatusName";
							ddlStatus.DataValueField = "Id";
							ddlStatus.DataBind();
							ddlStatus.Items.Insert(0, new ListItem("Select Status", "0"));
						}
					}
					if (qtype == "SupervisorDLL")
					{
						if (getExecuter != null)
						{
							if (getExecuter.Rows.Count > 0)
							{
								ddlTaskExecuter.DataSource = getExecuter;
								ddlTaskExecuter.DataTextField = "Emp_Name";
								ddlTaskExecuter.DataValueField = "Emp_Code";
								ddlTaskExecuter.DataBind();
								ddlTaskExecuter.Items.Insert(0, new ListItem("Select Executor", "0"));
							}
						}


					}
					else if (qtype == "ExecuterDLL")
					{
						ddlTaskExecuter.DataSource = null;
						ddlTaskExecuter.DataBind();
						if (getExecuter != null)
						{
							if (getExecuter.Rows.Count > 0)
							{
								ddlTaskExecuter.DataSource = getExecuter;
								ddlTaskExecuter.DataTextField = "Emp_Name";
								ddlTaskExecuter.DataValueField = "Emp_Code";
								ddlTaskExecuter.DataBind();
								ddlTaskExecuter.Items.Insert(0, new ListItem("Select Supervisor", "0"));
							}
						}
					}
				}
			}
		}
		catch (Exception)
		{

		}
	}
	private void BindMyTaskGrid(string empCode,string qtype)
    {
        try
        {
           
            var getResult = spm.getTaskExecuterDDL(empCode, qtype, "", "", Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), 0, Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), Convert.ToString(""), Convert.ToBoolean(0), 0, Convert.ToBoolean(0), 0, Convert.ToBoolean(0), Convert.ToBoolean(0));
            var getMyTaskList = getResult.Tables[0];
           
            gv_MyProcessedTaskExecuterList.DataSource = null;
            gv_MyProcessedTaskExecuterList.DataBind();
            if (getMyTaskList.Rows.Count > 0)
           {
                gv_MyProcessedTaskExecuterList.DataSource = getMyTaskList;
                gv_MyProcessedTaskExecuterList.DataBind();
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

    protected void MyProcessedTaskExecuter_Edit_Click(object sender, EventArgs e)
    {
        try
        {           
            LinkButton btn = (LinkButton)sender;

            string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
            string TaskId = commandArgs[0];
            string Task_Ref_Id = commandArgs[1];
            //var appType = Convert.ToString(Request.QueryString["app"]);

            if (hdnType.Value == "ex")
            {
                Response.Redirect("~/procs/TaskExecuter_Edit.aspx?Task_Id=" + TaskId + "&TaskRefId=" + Task_Ref_Id + "&flag=" + "ep");
            }
            else if (hdnType.Value== "su")
            {
				Session.Remove("AssignedBy");
				Session.Remove("DelayedTask");
				Response.Redirect("~/procs/TaskExecuter_Edit.aspx?Task_Id=" + TaskId + "&TaskRefId=" + Task_Ref_Id + "&flag=" + "sp");
            }
            
        }
        catch (Exception ex)
        {

        }

    }

    protected void gv_MyProcessedTaskExecuterList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
			Session.Remove("AssignedBy");
			Session.Remove("DelayedTask");
			var empCode = Convert.ToString(Session["Empcode"]);
           // var appType = Convert.ToString(Request.QueryString["app"]);
            gv_MyProcessedTaskExecuterList.PageIndex = e.NewPageIndex;
            if (hdnType.Value == "ex")
            {
                //this.BindMyTaskGrid(empCode, "GetMyProcessedTaskExecuter");
				GetFilter_Task_Process("GetMyProcessedTaskExecuter");
			}
            else if (hdnType.Value == "su")
            {
				//this.BindMyTaskGrid(empCode, "GetMyProcessedTaskSuper");
				GetFilter_Task_Process("GetMyProcessedTaskSuper");

			}

        }
        catch (Exception)
        {

        }
    }

	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		//var appType = Convert.ToString(Request.QueryString["app"]);
		if (hdnType.Value == "ex")
		{
			GetFilter_Task_Process("GetMyProcessedTaskExecuter");
		}
		else if (hdnType.Value == "su")
		{
		  GetFilter_Task_Process("GetMyProcessedTaskSuper");
	    }
	}

	protected void mobile_btnBack_Click(object sender, EventArgs e)
	{

		//var appType = Convert.ToString(Request.QueryString["app"]);
		if (hdnType.Value == "ex")
		{
			BindAllDLL("ExecuterDLL", "getExecuterDLL");
			GetFilter_Task_Process("GetMyProcessedTaskExecuter");
		}
		else if (hdnType.Value == "su")
		{
			BindAllDLL("SupervisorDLL", "getSupervisorDLL");
			GetFilter_Task_Process("GetMyProcessedTaskSuper");

		}
	}
	public void GetFilter_Task_Process(string Stype)
	{
		DataTable dtTaskDetails = new DataTable();
		lblmessage.Text = "";
		try
		{
			int TaskId = 0, TaskRefId = 0, Status_id = 0;
			lblmessage.Text = "";
			string Executer = "";
			
			Executer = Convert.ToString(ddlTaskExecuter.SelectedValue).Trim() != "" ? Convert.ToString(ddlTaskExecuter.SelectedValue) : "0";
			TaskRefId = Convert.ToString(ddlTaskRefId.SelectedValue).Trim() != "" ? Convert.ToInt32(ddlTaskRefId.SelectedValue) : 0;
			TaskId = Convert.ToString(ddlTaskId.SelectedValue).Trim() != "" ? Convert.ToInt32(ddlTaskId.SelectedValue) : 0;
			Status_id = Convert.ToString(ddlStatus.SelectedValue).Trim() != "" ? Convert.ToInt32(ddlStatus.SelectedValue) : 0;
			//POWODate = Convert.ToString(lstPOWODate.SelectedValue).Trim() != "" ? Convert.ToString(lstPOWODate.SelectedValue) : "";
				Session["ddlTaskExecuter"] = Executer;
				Session["ddlTaskRefId"] = TaskRefId;
				Session["ddlTaskId"] = TaskId;
				Session["ddlStatus"] = Status_id;
			
			SqlParameter[] spars = new SqlParameter[7];
			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = Stype;
			spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(Session["Empcode"]);
			spars[2] = new SqlParameter("@TaskId", SqlDbType.VarChar);
			spars[2].Value = TaskId;
			spars[3] = new SqlParameter("@TaskRefId", SqlDbType.VarChar);
			spars[3].Value = TaskRefId;
			spars[4] = new SqlParameter("@Executer", SqlDbType.VarChar);
			spars[4].Value = Executer;
			spars[5] = new SqlParameter("@ActionStatus", SqlDbType.Int);
			spars[5].Value = Status_id;
			dtTaskDetails = spm.getMobileRemDataList(spars, "SP_TASK_M_EXECUTER");
			gv_MyProcessedTaskExecuterList.DataSource = null;
			gv_MyProcessedTaskExecuterList.DataBind();
			if (dtTaskDetails.Rows.Count > 0)
			{
				lblcount.Text = "Record Count -: "+ dtTaskDetails.Rows.Count;
				gv_MyProcessedTaskExecuterList.DataSource = dtTaskDetails;
				gv_MyProcessedTaskExecuterList.DataBind();
			}
			else
			{
				lblcount.Text = "";
				lblmessage.Text = "Record not available";
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}

	protected void ddlTaskExecuter_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			var getEmpCode = ddlTaskExecuter.SelectedValue.ToString();
			if (hdnType.Value == "ex")
			{
				if (getEmpCode != "0")
				{
					BindTaskRef("get_Executer_Supervisor", getEmpCode);
				}
				else
				{
					BindAllDLL("ExecuterDLL", "getExecuterDLL");
				}
			}
			else if (hdnType.Value == "su")
			{
				if (getEmpCode != "0")
				{
					BindTaskRef("getSupervisor_Executer", getEmpCode);
				}
				else
				{
					BindAllDLL("SupervisorDLL", "getSupervisorDLL");
				}
			}
		}
		catch (Exception)
		{

		}
	}
	protected void ddlTaskRefId_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			var getEmpCode = ddlTaskExecuter.SelectedValue.ToString();
			var getVal = ddlTaskRefId.SelectedValue.ToString();
			if (hdnType.Value == "ex")
			{
				if (getVal != "0")
				{
					BindTaskId("get_Executer_TaskID", Convert.ToDouble(getVal), getEmpCode);
				}
				else if (getEmpCode != "0")
				{
					BindTaskRef("get_Executer_Supervisor", getEmpCode);
				}
				else
				{
					BindAllDLL("ExecuterDLL", "getExecuterDLL");
				}
			}
			else if (hdnType.Value == "su")
			{
				if (getVal != "0")
				{
					BindTaskId("getSupervisor_TaskID", Convert.ToDouble(getVal), getEmpCode);
				}
				else if (getEmpCode != "0")
				{
					BindTaskRef("getSupervisor_Executer", getEmpCode);
				}
				else
				{
					BindAllDLL("SupervisorDLL", "getSupervisorDLL");
				}
			}
		}
		catch (Exception)
		{

		}

	}

	private void BindTaskRef(string Qtype,string Executer)
	{
		try
		{
			SqlParameter[] spars = new SqlParameter[5];
			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = Qtype;
			spars[1] = new SqlParameter("@Task_Supervisor", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(Session["Empcode"]);
			spars[2] = new SqlParameter("@Task_Executer", SqlDbType.VarChar);
			spars[2].Value = Executer;
			spars[3] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[3].Value = "";
			var getDs = spm.getDatasetList(spars, "SP_TASK_M_Report");
			if (getDs != null)
			{
				if (getDs.Tables.Count > 0)
				{
					ddlTaskRefId.DataSource = null;
					ddlTaskRefId.DataBind();
					ddlTaskId.DataSource = null;
					ddlTaskId.DataBind();
					var getRefIds = getDs.Tables[0];
					var getTaskIds = getDs.Tables[1];
					if (getRefIds != null)
					{
						if (getRefIds.Rows.Count > 0)
						{
							ddlTaskRefId.DataSource = getRefIds;
							ddlTaskRefId.DataTextField = "Task_Reference_ID";
							ddlTaskRefId.DataValueField = "ID";
							ddlTaskRefId.DataBind();
							ddlTaskRefId.Items.Insert(0, new ListItem("Select Task Reference id", "0"));
						}
					}
					if (getTaskIds != null)
					{
						if (getTaskIds.Rows.Count > 0)
						{
							ddlTaskId.DataSource = getTaskIds;
							ddlTaskId.DataTextField = "Task_ID";
							ddlTaskId.DataValueField = "ID";
							ddlTaskId.DataBind();
							ddlTaskId.Items.Insert(0, new ListItem("Select Task id", "0"));
						}
					}
				}
			}

		}
		catch (Exception ex)
		{

		}
	}
	private void BindTaskId( string Qtype,double taskRefId, string Executer)
	{
		try
		{
			SqlParameter[] spars = new SqlParameter[5];
			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = Qtype; 
			spars[1] = new SqlParameter("@Task_Supervisor", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(Session["Empcode"]);
			spars[2] = new SqlParameter("@Task_Executer", SqlDbType.VarChar);
			spars[2].Value = Executer;			
			spars[3] = new SqlParameter("@Task_Ref_Id", SqlDbType.Decimal);
			spars[3].Value = taskRefId;
			spars[4] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[4].Value = "";
			var getDs = spm.getDatasetList(spars, "SP_TASK_M_Report");
			if (getDs != null)
			{
				if (getDs.Tables.Count > 0)
				{

					ddlTaskId.DataSource = null;
					ddlTaskId.DataBind();
					var getTaskIds = getDs.Tables[0];
					if (getTaskIds != null)
					{
						if (getTaskIds.Rows.Count > 0)
						{
							ddlTaskId.DataSource = getTaskIds;
							ddlTaskId.DataTextField = "Task_ID";
							ddlTaskId.DataValueField = "ID";
							ddlTaskId.DataBind();
							ddlTaskId.Items.Insert(0, new ListItem("Select Task id", "0"));
						}
					}
				}
			}

		}
		catch (Exception ex)
		{

		}
	}

}