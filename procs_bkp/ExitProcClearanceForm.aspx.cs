using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_ExitProcClearanceForm : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    public DataTable dt, dtGetApprover;
    string Emp_Code;
    bool hasKeys;
    int ResignationID;
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
            lblmessage.Text = "";
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

            Emp_Code = Session["Empcode"].ToString();
            txtDateRelease.Attributes.Add("onkeypress", "return noanyCharecters(event);");

            lblmessage.Visible = true;
            //   
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                Page.SmartNavigation = true;

                if (!Page.IsPostBack)
                {
					hasKeys = Request.QueryString.HasKeys();
                    if (hasKeys)
                    {
							LoadUserData(Session["Empcode"].ToString(), Convert.ToInt32(Request.QueryString["resignationid"]));
                        //GetExitSurveyData(Convert.ToInt32(Request.QueryString["resignationid"]));
                        //SetControl();
                    }
                    else
                    {
                        LoadUserData(Session["Empcode"].ToString(),0);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    public void LoadUserData(string empCode,int ResignationID)
    {
        dt = spm.getUserDetailsForExitProcess(empCode, ResignationID, "ExitClearanceForm");
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToString(dt.Rows[0]["ExitClearanceEligible"]) == "Yes")
            {
                txtProjectName.Text = Convert.ToString(dt.Rows[0]["emp_projectName"]);
                txtResignationDate.Text = Convert.ToString(dt.Rows[0]["ResignationDate"]);
                txtName.Text = Convert.ToString(dt.Rows[0]["Emp_Name"]);
                txtDesignationGrade.Text = Convert.ToString(dt.Rows[0]["DesginationGrade"]);
                txtDoJ.Text = Convert.ToString(dt.Rows[0]["JoiningDate"]);
                txtLastWorkingDay.Text = Convert.ToString(dt.Rows[0]["LastWorkingDate"]);
                ResignationID = Convert.ToInt32(dt.Rows[0]["ResignationID"]);
                txtReleaseDate.Text= Convert.ToString(dt.Rows[0]["HrReleaseDate"]);
                //txtDateRelease.Text= Convert.ToDateTime(dt.Rows[0]["HrReleaseDate"].ToString());
                hdnRsID.Value = Convert.ToString(dt.Rows[0]["ResignationID"]);
                if (Convert.ToString(dt.Rows[0]["ExitClearanceFilled"]) == "Yes")
                {
                    lblmessage.Text = "You have already submitted Exit Clearance form.";
                    txtDateRelease.Enabled = false;
                    mobile_btnSave.Visible = false;
                }
            }
            else
            {
                lblmessage.Text = "You are not eligible to submit Exit Clearance Form.";
                txtDateRelease.Enabled = false;
                mobile_btnSave.Visible = false;
            }

        }
        else
        {
            lblmessage.Text = "You do not have access of this page.";
            txtDateRelease.Enabled = false;
            mobile_btnSave.Visible = false;
        }
    }
    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
		try
		{
			string confirmValue = hdnYesNo.Value.ToString();
			//if (confirmValue != "Yes")
			//{
			//	return;
			//}

			string[] strdate;
			string strrlsDate = "";
			if (Convert.ToString(txtReleaseDate.Text).Trim() != "")
			{
				strdate = Convert.ToString(txtReleaseDate.Text).Trim().Split('/');
				strrlsDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
			}
			DateTime ddt = DateTime.ParseExact(strrlsDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

			string[] strLwddate;
			string strrlsLwdDate = "";
			if (Convert.ToString(txtLastWorkingDay.Text).Trim() != "")
			{
				strLwddate = Convert.ToString(txtLastWorkingDay.Text).Trim().Split('/');
				strrlsLwdDate = Convert.ToString(strLwddate[2]) + "-" + Convert.ToString(strLwddate[1]) + "-" + Convert.ToString(strLwddate[0]);
			}
			DateTime dLwd = DateTime.ParseExact(strrlsLwdDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

			//if (ddt < dLwd)
			//{
			//    lblmessage.Text = "Release date can not be lesser then last working date.";

			//    return;
			//}

			bool clrData = spm.InserExitClearanceFormData(Convert.ToInt32(hdnRsID.Value), strrlsDate);
			dtGetApprover = spm.getExitCleranceApprover(Emp_Code);
			if (dtGetApprover.Rows.Count > 0)
			{
				for (int i = 0; i < dtGetApprover.Rows.Count; i++)
				{
                    DataRow[] dttt = dtGetApprover.Select("ClearanceDeptID=101");
                    if (dttt.Length == 0 && (Convert.ToInt32(dtGetApprover.Rows[i]["ClearanceDeptID"]) == 106))
                    {
                        string emp_name = "", LWD = "";
                        StringBuilder strbuild = new StringBuilder();
                        SqlParameter[] spars = new SqlParameter[2];
                        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                        spars[0].Value = "ResignationMail_EmployeeDetails";
                        spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                        spars[1].Value = Emp_Code;
                        DataTable tt = spm.getData_FromCode(spars, "SP_ExitProcess");

                        string ResigForm_AppLink = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ExitProcess_ClearanceApproveForm.aspx";
                        //string ResigForm_AppLink = "http://192.168.21.193/hrms/login.aspx?ReturnUrl=procs/ExitProcess_ClearanceApproveForm.aspx";
                        //string ResigForm_AppLink = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ExitProcess_ClearanceApproveForm.aspx";
                        string redirectURL = Convert.ToString(ResigForm_AppLink).Trim() + "?resignationid=" + Convert.ToInt32(hdnRsID.Value) + "&Type=Add";

                        if (tt.Rows.Count > 0)
                        {
                            strbuild.Length = 0;
                            strbuild.Clear();

                            strbuild.Append("<table border='1'>");

                            foreach (DataRow row in tt.Rows)
                            {
                                if (Convert.ToString(row["emp_location"].ToString()) == "HO-NaviMum")
                                {

                                    strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
                                    + "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
                                    + "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
                                    + "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
                                    + "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
                                    + "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
                                    + "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
                                    + "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
                                    + "</td></tr><tr><td>Reporting Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
                                    + "</td></tr><tr><td>HOD: </td><td>" + Convert.ToString(row["DH"].ToString())
                                    + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
                                    + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
                                    + "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
                                   + "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
                                    + "</td></tr>");
                                }
                                else
                                {
                                    strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
                                    + "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
                                    + "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
                                    + "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
                                    + "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
                                    + "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
                                    + "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
                                    + "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
                                    + "</td></tr><tr><td>Project Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
                                    + "</td></tr><tr><td>Delivery Manager: </td><td>" + Convert.ToString(row["DM"].ToString())
                                    + "</td></tr><tr><td>Program Manager: </td><td>" + Convert.ToString(row["PRM"].ToString())
                                    + "</td></tr><tr><td>Delivery Head: </td><td>" + Convert.ToString(row["DH"].ToString())
                                    + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
                                    + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
                                    + "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
                                    + "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
                                    + "</td></tr>");
                                }
                                emp_name = Convert.ToString(row["Emp_Name"].ToString());
                                LWD = Convert.ToString(row["LastWorkingDate"].ToString());
                            }
                            strbuild.Append("</table>");
                        }

                        string strsubject = txtName.Text + " has filled Clearance Form";
                        spm.SendMailToClearanceApprover(Convert.ToString(dtGetApprover.Rows[i]["Emp_Emailaddress"]), strsubject, txtName.Text, Convert.ToString(strbuild), "", redirectURL, LWD);

                    }

                    if (dttt.Length == 0 && (Convert.ToInt32(dtGetApprover.Rows[i]["ClearanceDeptID"]) == 102))
                    {
                        string emp_name = "", LWD = "";
                        StringBuilder strbuild = new StringBuilder();
                        SqlParameter[] spars = new SqlParameter[2];
                        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                        spars[0].Value = "ResignationMail_EmployeeDetails";
                        spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                        spars[1].Value = Emp_Code;
                        DataTable tt = spm.getData_FromCode(spars, "SP_ExitProcess");

                        string ResigForm_AppLink = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ExitProcess_ClearanceApproveForm.aspx";
                        //string ResigForm_AppLink = "http://192.168.21.193/hrms/login.aspx?ReturnUrl=procs/ExitProcess_ClearanceApproveForm.aspx";
                        //string ResigForm_AppLink = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ExitProcess_ClearanceApproveForm.aspx";
                        string redirectURL = Convert.ToString(ResigForm_AppLink).Trim() + "?resignationid=" + Convert.ToInt32(hdnRsID.Value) + "&Type=Add";

                        if (tt.Rows.Count > 0)
                        {
                            strbuild.Length = 0;
                            strbuild.Clear();

                            strbuild.Append("<table border='1'>");

                            foreach (DataRow row in tt.Rows)
                            {
                                if (Convert.ToString(row["emp_location"].ToString()) == "HO-NaviMum")
                                {

                                    strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
                                    + "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
                                    + "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
                                    + "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
                                    + "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
                                    + "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
                                    + "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
                                    + "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
                                    + "</td></tr><tr><td>Reporting Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
                                    + "</td></tr><tr><td>HOD: </td><td>" + Convert.ToString(row["DH"].ToString())
                                    + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
                                    + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
                                    + "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
                                   + "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
                                    + "</td></tr>");
                                }
                                else
                                {
                                    strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
                                    + "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
                                    + "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
                                    + "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
                                    + "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
                                    + "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
                                    + "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
                                    + "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
                                    + "</td></tr><tr><td>Project Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
                                    + "</td></tr><tr><td>Delivery Manager: </td><td>" + Convert.ToString(row["DM"].ToString())
                                    + "</td></tr><tr><td>Program Manager: </td><td>" + Convert.ToString(row["PRM"].ToString())
                                    + "</td></tr><tr><td>Delivery Head: </td><td>" + Convert.ToString(row["DH"].ToString())
                                    + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
                                    + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
                                    + "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
                                    + "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
                                    + "</td></tr>");
                                }
                                emp_name = Convert.ToString(row["Emp_Name"].ToString());
                                LWD = Convert.ToString(row["LastWorkingDate"].ToString());
                            }
                            strbuild.Append("</table>");
                        }

                        string strsubject = txtName.Text + " has filled Clearance Form";
                        spm.SendMailToClearanceApprover(Convert.ToString(dtGetApprover.Rows[i]["Emp_Emailaddress"]), strsubject, txtName.Text, Convert.ToString(strbuild), "", redirectURL, LWD);

                    }

                    if (dttt.Length == 0 && (Convert.ToInt32(dtGetApprover.Rows[i]["ClearanceDeptID"]) == 103))
                    {
                        string emp_name = "", LWD = "";
                        StringBuilder strbuild = new StringBuilder();
                        SqlParameter[] spars = new SqlParameter[2];
                        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                        spars[0].Value = "ResignationMail_EmployeeDetails";
                        spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                        spars[1].Value = Emp_Code;
                        DataTable tt = spm.getData_FromCode(spars, "SP_ExitProcess");

                        string ResigForm_AppLink = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ExitProcess_ClearanceApproveForm.aspx";
                        //string ResigForm_AppLink = "http://192.168.21.193/hrms/login.aspx?ReturnUrl=procs/ExitProcess_ClearanceApproveForm.aspx";
                        //string ResigForm_AppLink = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ExitProcess_ClearanceApproveForm.aspx";
                        string redirectURL = Convert.ToString(ResigForm_AppLink).Trim() + "?resignationid=" + Convert.ToInt32(hdnRsID.Value) + "&Type=Add";

                        if (tt.Rows.Count > 0)
                        {
                            strbuild.Length = 0;
                            strbuild.Clear();

                            strbuild.Append("<table border='1'>");

                            foreach (DataRow row in tt.Rows)
                            {
                                if (Convert.ToString(row["emp_location"].ToString()) == "HO-NaviMum")
                                {

                                    strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
                                    + "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
                                    + "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
                                    + "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
                                    + "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
                                    + "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
                                    + "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
                                    + "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
                                    + "</td></tr><tr><td>Reporting Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
                                    + "</td></tr><tr><td>HOD: </td><td>" + Convert.ToString(row["DH"].ToString())
                                    + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
                                    + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
                                    + "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
                                   + "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
                                    + "</td></tr>");
                                }
                                else
                                {
                                    strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
                                    + "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
                                    + "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
                                    + "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
                                    + "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
                                    + "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
                                    + "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
                                    + "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
                                    + "</td></tr><tr><td>Project Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
                                    + "</td></tr><tr><td>Delivery Manager: </td><td>" + Convert.ToString(row["DM"].ToString())
                                    + "</td></tr><tr><td>Program Manager: </td><td>" + Convert.ToString(row["PRM"].ToString())
                                    + "</td></tr><tr><td>Delivery Head: </td><td>" + Convert.ToString(row["DH"].ToString())
                                    + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
                                    + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
                                    + "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
                                    + "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
                                    + "</td></tr>");
                                }
                                emp_name = Convert.ToString(row["Emp_Name"].ToString());
                                LWD = Convert.ToString(row["LastWorkingDate"].ToString());
                            }
                            strbuild.Append("</table>");
                        }

                        string strsubject = txtName.Text + " has filled Clearance Form";
                        spm.SendMailToClearanceApprover(Convert.ToString(dtGetApprover.Rows[i]["Emp_Emailaddress"]), strsubject, txtName.Text, Convert.ToString(strbuild), "", redirectURL, LWD);

                    }


                    bool result = spm.InserExitClearanceFormApproverDetails(Emp_Code, Convert.ToInt32(dtGetApprover.Rows[i]["ClearanceDeptID"]), Convert.ToString(dtGetApprover.Rows[i]["ClearanceDeptEmpCode"]));
                     //if (Convert.ToInt32(dtGetApprover.Rows[i]["ClearanceDeptID"]) != 105 )
                    //if ((Convert.ToInt32(dtGetApprover.Rows[i]["ClearanceDeptID"]) != 104) && (Convert.ToInt32(dtGetApprover.Rows[i]["ClearanceDeptID"]) != 105))
                    if ((Convert.ToInt32(dtGetApprover.Rows[i]["ClearanceDeptID"]) != 104) && (Convert.ToInt32(dtGetApprover.Rows[i]["ClearanceDeptID"]) != 105) && (Convert.ToInt32(dtGetApprover.Rows[i]["ClearanceDeptID"]) != 102) && (Convert.ToInt32(dtGetApprover.Rows[i]["ClearanceDeptID"]) != 103) && (Convert.ToInt32(dtGetApprover.Rows[i]["ClearanceDeptID"]) != 106))
                    {
						//Get To mail-ids
						string emp_name = "", LWD = "";
						StringBuilder strbuild = new StringBuilder();
						SqlParameter[] spars = new SqlParameter[2];
						spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
						spars[0].Value = "ResignationMail_EmployeeDetails";
						spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
						spars[1].Value = Emp_Code;
						DataTable tt = spm.getData_FromCode(spars, "SP_ExitProcess");

						string ResigForm_AppLink = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ExitProcess_ClearanceApproveForm.aspx";
						//string ResigForm_AppLink = "http://192.168.21.193/hrms/login.aspx?ReturnUrl=procs/ExitProcess_ClearanceApproveForm.aspx";
						//string ResigForm_AppLink = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ExitProcess_ClearanceApproveForm.aspx";
						string redirectURL = Convert.ToString(ResigForm_AppLink).Trim() + "?resignationid=" + Convert.ToInt32(hdnRsID.Value) + "&Type=Add";

						if (tt.Rows.Count > 0)
						{
							strbuild.Length = 0;
							strbuild.Clear();

							strbuild.Append("<table border='1'>");

							foreach (DataRow row in tt.Rows)
							{
								if (Convert.ToString(row["emp_location"].ToString()) == "HO-NaviMum")
								{

									strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
									+ "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
									+ "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
									+ "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
									+ "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
									+ "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
									+ "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
									+ "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
									+ "</td></tr><tr><td>Reporting Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
									+ "</td></tr><tr><td>HOD: </td><td>" + Convert.ToString(row["DH"].ToString())
									+ "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
									+ "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
									+ "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
								   + "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
									+ "</td></tr>");
								}
								else
								{
									strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
									+ "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
									+ "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
									+ "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
									+ "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
									+ "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
									+ "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
									+ "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
									+ "</td></tr><tr><td>Project Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
									+ "</td></tr><tr><td>Delivery Manager: </td><td>" + Convert.ToString(row["DM"].ToString())
									+ "</td></tr><tr><td>Program Manager: </td><td>" + Convert.ToString(row["PRM"].ToString())
									+ "</td></tr><tr><td>Delivery Head: </td><td>" + Convert.ToString(row["DH"].ToString())
									+ "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
									+ "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
									+ "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
									+ "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
									+ "</td></tr>");
								}
								emp_name = Convert.ToString(row["Emp_Name"].ToString());
								LWD = Convert.ToString(row["LastWorkingDate"].ToString());
							}
							strbuild.Append("</table>");
						}

						string strsubject = txtName.Text + " has filled Clearance Form";
                        spm.SendMailToClearanceApprover(Convert.ToString(dtGetApprover.Rows[i]["Emp_Emailaddress"]), strsubject, txtName.Text, Convert.ToString(strbuild), "", redirectURL, LWD);
					}
				}
				ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ASD", "StopLoader();", true);
				Response.Redirect("ExitProcess_Index.aspx");
			}
		}
		catch (Exception EX)
		{
			ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ASD", "StopLoader();", true);
			Response.Write(EX.ToString());
		}
    }




    protected void mobile_cancel_Click(object sender, EventArgs e)
    {
		
	}

    protected void txtDateRelease_TextChanged(object sender, EventArgs e)
    {
        string[] strdate;
        string strrlsDate = "";
        if (Convert.ToString(txtDateRelease.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtDateRelease.Text).Trim().Split('/');
            strrlsDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

            DateTime ddt = DateTime.ParseExact(strrlsDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            if (ddt < DateTime.Today)
            {
                lblmessage.Text = "Release date can not less than last working date.";

                return;
            }
        }
    }
}