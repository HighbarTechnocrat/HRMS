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
using System.Text;

public partial class procs_ExitProccess_InterviewForm : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    public DataTable dt, dtExitInterview;
    string Emp_Code;
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

            bool hasKeys = Request.QueryString.HasKeys();
            if (hasKeys)
            {
                ResignationID = Convert.ToInt32(Request.QueryString["resignationid"]);
            }

            Emp_Code = Session["Empcode"].ToString();

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
                    if (ResignationID != 0)
                    {
                        ResignationID = Convert.ToInt32(Request.QueryString["resignationid"]);
                        LoadUserData(ResignationID);
                        if (Request.QueryString["Type"] == "View")
                        {
                            GetExitInterviewData(Convert.ToInt32(Request.QueryString["resignationid"]));
                            SetControl();
                            lblmsg2.Text = "";
                            mobile_btnReject.Visible = false;
                        }
                    }
                    else
                    {
                        LoadUserData(ResignationID);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    public void LoadUserData(int ResignationId)
    {
        DataSet dt = spm.getUserDetailsForExitInterview(ResignationId, "ExitInterviewForm");
        if (dt.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToString(dt.Tables[0].Rows[0]["ExitMyExitIntEligible"]) == "Yes")
            {
                if (Convert.ToString(dt.Tables[0].Rows[0]["RMEMPCode"]) == Emp_Code || Convert.ToString(dt.Tables[0].Rows[0]["Emp_Code"]) == Emp_Code)
                {
                    txtfullName.Text = Convert.ToString(dt.Tables[0].Rows[0]["Name"]);
                    txtempCode.Text = Convert.ToString(dt.Tables[0].Rows[0]["RMEMPCode"]);
                    txtFullNameResignedReportee.Text = Convert.ToString(dt.Tables[0].Rows[0]["Emp_Name"]);
                    txtEmpCodeResignedReportee.Text = Convert.ToString(dt.Tables[0].Rows[0]["Emp_Code"]);
                    txtLWD.Text = Convert.ToString(dt.Tables[0].Rows[0]["LastWorkingDate"]);

                    if (Convert.ToString(dt.Tables[0].Rows[0]["ExitInterviewFilled"]) == "Yes" && Request.QueryString["Type"] != "View")
                    {
                        if (Convert.ToBoolean(dt.Tables[1].Rows[0]["isdraft"]) == true)
                        {


                            for (Int32 irow = 0; irow < dt.Tables[1].Rows.Count; irow++)
                            {
                                #region get Question 1 answer
                                if (Convert.ToString(dt.Tables[1].Rows[irow]["ExitIntQstID"]).Trim() == "6")
                                {
                                    foreach (ListItem item in rdbtnlst6.Items)
                                    {
                                        if (Convert.ToString(item.Value).Trim() == Convert.ToString(dt.Tables[1].Rows[irow]["ExitIntQstOptID"]).Trim())
                                        {
                                            item.Selected = true;
                                        }
                                    }
                                }
                                #endregion

                                #region get Question 2 answer
                                if (Convert.ToString(dt.Tables[1].Rows[irow]["ExitIntQstID"]).Trim() == "7")
                                {
                                    foreach (ListItem item in checklst7.Items)
                                    {
                                        if (Convert.ToString(item.Value).Trim() == Convert.ToString(dt.Tables[1].Rows[irow]["ExitIntQstOptID"]).Trim())
                                        {
                                            item.Selected = true;
                                        }
                                    }
                                }
                                #endregion

                                #region get Question 3,4,5,6 answers 
                                if (Convert.ToString(dt.Tables[1].Rows[irow]["ExitIntQstID"]).Trim() == "8")
                                {
                                    txt8.Text = Convert.ToString(dt.Tables[1].Rows[irow]["ExitIntTextAns"]).Trim();
                                }
                                if (Convert.ToString(dt.Tables[1].Rows[irow]["ExitIntQstID"]).Trim() == "9")
                                {
                                    txt9.Text = Convert.ToString(dt.Tables[1].Rows[irow]["ExitIntTextAns"]).Trim();
                                }
                                if (Convert.ToString(dt.Tables[1].Rows[irow]["ExitIntQstID"]).Trim() == "10")
                                {
                                    txt10.Text = Convert.ToString(dt.Tables[1].Rows[irow]["ExitIntTextAns"]).Trim();
                                }
                                if (Convert.ToString(dt.Tables[1].Rows[irow]["ExitIntQstID"]).Trim() == "11")
                                {
                                    txt11.Text = Convert.ToString(dt.Tables[1].Rows[irow]["ExitIntTextAns"]).Trim();
                                }
                                #endregion
                            }

                        }
                        else
                        {
                            lblmessage.Text = "You have already submitted Exit Interview Form.";
                            lblmsg2.Text = "";
                            SetControl();
                            mobile_btnSave.Visible = false;
                            mobile_btnReject.Visible = false;
                        }
                    }
                }
                else
                {
                    lblmessage.Text = "You do not have access of this page.";
                    lblmsg2.Text = "";
                    SetControl();
                }
            }
            else
            {
                lblmessage.Text = "You are not eligible to submit Exit Interview form.";
                lblmsg2.Text = "";
                SetControl();
                mobile_btnSave.Visible = false;
                mobile_btnReject.Visible = false;
            }
        }
    }
 

    private void SendmailEmployee(int ResignationID)
	{
		try
		{
			string Emp_Emailaddress = "";
			//Get To mail-ids
			string emp_name = "", LWD = "";
			StringBuilder strbuild = new StringBuilder();
			SqlParameter[] spars = new SqlParameter[2];
			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = "ResignationMail_EmployeeDetails";
			spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[1].Value = txtEmpCodeResignedReportee.Text;
			DataTable tt = spm.getData_FromCode(spars, "SP_ExitProcess");
			string ResigForm_AppLink = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ExitProcess_SurveyForm.aspx";
			//string ResigForm_AppLink = "http://192.168.21.193/hrms/login.aspx?ReturnUrl=procs/ExitProccess_InterviewForm.aspx";
			//string ResigForm_AppLink = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ExitProccess_InterviewForm.aspx";

			string redirectURL = Convert.ToString(ResigForm_AppLink).Trim() + "?resignationid=" + ResignationID;

			if (tt.Rows.Count > 0)
			{
				strbuild.Length = 0;
				strbuild.Clear();
				strbuild = new StringBuilder();
				strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'><tr><td>");
				strbuild.Append("</td></tr>");
				strbuild.Append("<tr><td style='height:10px'></td></tr>");
				strbuild.Append("<tr><td> Dear "+ txtFullNameResignedReportee.Text.Trim() + ",</td></tr>");
				strbuild.Append("<tr><td>This is to inform you that <b>" + txtfullName.Text.Trim() + "</b> has filled & submitted the Interview Exit Form. Please submit Exit Survey & Clearance Form and ensure it is approved by all respective Departments before your last working day with the company.</td></tr>");
				strbuild.Append("<tr><td> Following are the details for your reference.</td></tr>");
				strbuild.Append("<tr><td style='height:10px'></td></tr>");
				strbuild.Append("<tr><td>");
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
					Emp_Emailaddress = Convert.ToString(row["Emp_Emailaddress"].ToString());
					LWD = Convert.ToString(row["LastWorkingDate"].ToString());
				}
				strbuild.Append("</table>");
				strbuild.Append("</td></tr>");
				//strbuild.Append("</td></tr>");
				strbuild.Append("<tr><td style='height:20px'></td></tr>");
				strbuild.Append("<tr><td style='height:20px'><a href='" + redirectURL + "' target='_blank'> Click here to take action</td></tr>");
				strbuild.Append("<tr><td style='height:20px'>This is a system generated mail, Please do not reply.</td></tr>");
				strbuild.Append("</table>");
			}
			string strsubject = "OneHR: Exit Interview Form Submitted for "+txtFullNameResignedReportee.Text.Trim() ;
            spm.sendMail(Emp_Emailaddress, strsubject, Convert.ToString(strbuild), "", "");




        }
		catch (Exception)
		{

			throw;
		}
	}
    public void GetExitInterviewData(int ResignationID)
    {
        
        dtExitInterview = spm.GetExitInterviewDataByResignationID(ResignationID);
        if (dtExitInterview.Rows.Count > 0)
        {
            for (int i = 0; i < dtExitInterview.Rows.Count; i++)
            {
                if (Convert.ToString(dtExitInterview.Rows[i]["ExitIntQstID"]) == "6")
                {
                    foreach (ListItem item in rdbtnlst6.Items)
                    {
                        if (Convert.ToString(dtExitInterview.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                        {
                            item.Selected = true;
                        }
                    }
                }
                else if (Convert.ToString(dtExitInterview.Rows[i]["ExitIntQstID"]) == "7")
                {
                    foreach (ListItem item in checklst7.Items)
                    {
                        if (Convert.ToString(dtExitInterview.Rows[i]["OptionResultValue"]) == (item.Value.ToString()))
                        {
                            item.Selected = true;
                        }
                    }
                }
                else if (Convert.ToString(dtExitInterview.Rows[i]["ExitIntQstID"]) == "8")
                {
                    txt8.Text = Convert.ToString(dtExitInterview.Rows[i]["ExitIntTextAns"]);
                }
                else if (Convert.ToString(dtExitInterview.Rows[i]["ExitIntQstID"]) == "9")
                {
                    txt9.Text = Convert.ToString(dtExitInterview.Rows[i]["ExitIntTextAns"]);
                }
                else if (Convert.ToString(dtExitInterview.Rows[i]["ExitIntQstID"]) == "10")
                {
                    txt10.Text = Convert.ToString(dtExitInterview.Rows[i]["ExitIntTextAns"]);
                }
                else if (Convert.ToString(dtExitInterview.Rows[i]["ExitIntQstID"]) == "11")
                {
                    txt11.Text = Convert.ToString(dtExitInterview.Rows[i]["ExitIntTextAns"]);
                }

            }
        }
    }
    public void SetControl()
    {
        mobile_btnSave.Visible = false;
        mobile_cancel.Visible = false;
        txt8.Enabled = false;
        txt9.Enabled = false;
        txt10.Enabled = false;
        txt11.Enabled = false;
        rdbtnlst6.Enabled = false;
        checklst7.Enabled = false;
    }

    protected void mobile_cancel_Click(object sender, EventArgs e)
    {

    }

    private void submitExitInterview_form(Boolean blnIsdraft)
    {
        int ExitIntQstID;
        int ExitIntQstOptID;
        int OptionResultValue;
        string ExitIntTextAns = "";

        #region Delete Exit Interview details
        bool resultFinal_1 = spm.deleteExitInview_details(Emp_Code, "deleteExitInterview_form", ResignationID);
        #endregion
        for (int i = 6; i <= 11; i++)
        {

            if (i == 6)
            {
                foreach (ListItem item in rdbtnlst6.Items)
                {
                    string selectedValue = "";
                    if (item.Selected)
                    {
                        selectedValue = item.Value;
                        ExitIntQstID = i;
                        ExitIntQstOptID = Convert.ToInt32(selectedValue);
                        OptionResultValue = Convert.ToInt32(selectedValue);
                        ExitIntTextAns = "";
                        bool result = spm.InserExitInterviewFormDetails(ResignationID, Emp_Code, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns, blnIsdraft);
                    }
                }
            }
            else if (i == 7)
            {
                foreach (ListItem item in checklst7.Items)
                {
                    string selectedValue = "";
                    if (item.Selected)
                    {
                        selectedValue = item.Value;
                        ExitIntQstID = i;
                        ExitIntQstOptID = Convert.ToInt32(selectedValue);
                        OptionResultValue = Convert.ToInt32(selectedValue);
                        ExitIntTextAns = "";
                        bool result = spm.InserExitInterviewFormDetails(ResignationID, Emp_Code, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns, blnIsdraft);
                    }
                }
            }
            else if (i == 8)
            {
                ExitIntQstID = i;
                ExitIntQstOptID = 0;
                OptionResultValue = 0;
                ExitIntTextAns = txt8.Text;
                bool result = spm.InserExitInterviewFormDetails(ResignationID, Emp_Code, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns, blnIsdraft);
            }
            else if (i == 9)
            {
                ExitIntQstID = i;
                ExitIntQstOptID = 0;
                OptionResultValue = 0;
                ExitIntTextAns = txt9.Text;
                bool result = spm.InserExitInterviewFormDetails(ResignationID, Emp_Code, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns, blnIsdraft);
            }
            else if (i == 10)
            {
                ExitIntQstID = i;
                ExitIntQstOptID = 0;
                OptionResultValue = 0;
                ExitIntTextAns = txt10.Text;
                bool result = spm.InserExitInterviewFormDetails(ResignationID, Emp_Code, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns, blnIsdraft);
            }
            else if (i == 11)
            {
                ExitIntQstID = i;
                ExitIntQstOptID = 0;
                OptionResultValue = 0;
                ExitIntTextAns = txt11.Text;
                bool result = spm.InserExitInterviewFormDetails(ResignationID, Emp_Code, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns, blnIsdraft);
            }


        }

        if (blnIsdraft == false)
        {
            bool resultFinal = spm.UpdateUserDataInAdminExit(Emp_Code, "UpdateInAdminExitInterview", ResignationID);
            SendmailEmployee(ResignationID);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }


        #region Validation 
        bool blnIsValid = false;
        lblAccomodationmsg.Text = "";
        lblExitMsg.Text = "";
        lbltxt8msg.Text = "";
        lbltxt9msg.Text = "";
        lbltxt10msg.Text = "";
        lbltxt11msg.Text = "";

        foreach (ListItem item in rdbtnlst6.Items)
        {
            if (item.Selected)
            {
                blnIsValid = true;
                break;
            }
        }

        if (blnIsValid == false)
        {
            lblAccomodationmsg.Text = "Please select at least one option";
            return;
        }

        blnIsValid = false;
        foreach (ListItem item in checklst7.Items)
        {
            if (item.Selected)
            {
                blnIsValid = true;
                break;
            }
        }

        if (blnIsValid == false)
        {
            lblExitMsg.Text = "Please select at least one option";
            return;
        }

        if (Convert.ToString(txt8.Text).Trim() == "")
        {
            lbltxt8msg.Text = "This field is mandatory.";
            return;
        }
        if (Convert.ToString(txt9.Text).Trim() == "")
        {
            lbltxt9msg.Text = "This field is mandatory.";
            return;
        }
        if (Convert.ToString(txt10.Text).Trim() == "")
        {
            lbltxt10msg.Text = "This field is mandatory.";
            return;
        }
        if (Convert.ToString(txt11.Text).Trim() == "")
        {
            lbltxt11msg.Text = "This field is mandatory.";
            return;
        }
        #endregion

        submitExitInterview_form(false);
        Response.Redirect("ExitProcess_Index.aspx"); 

    }

    protected void mobile_btnReject_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }
        submitExitInterview_form(true);
        Response.Redirect("ExitProcess_Index.aspx");
    }
}