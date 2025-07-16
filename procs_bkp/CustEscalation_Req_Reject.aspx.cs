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
public partial class CustEscalation_Req_Reject : System.Web.UI.Page
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
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
			}
			else
			{

				Page.SmartNavigation = true;
				if (!Page.IsPostBack)
				{
					txt_SPOCComment.Attributes.Add("maxlength", "500");
					FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["CustomerFristPathpath"]).Trim());
					TXT_AssginmentDate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
					txtDeliveryDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					txtResponceDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					TxtActionDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					editform.Visible = true;
					divbtn.Visible = false;
					divmsg.Visible = false;
					hdnempcode.Value = Convert.ToString(Session["Empcode"]);

					if (Request.QueryString.Count > 0)
					{
						hdnRemid.Value = Convert.ToString(Request.QueryString[0]).Trim();
						hdnRemid_Type.Value = Convert.ToString(Request.QueryString[1]).Trim();
						var getType = Convert.ToString(Request.QueryString[1]).Trim();
						
					}

					if (Convert.ToString(hdnRemid.Value).Trim() != "")
					{
						if (Request.QueryString.Count > 0)
						{
							checkRejectStatus_Submit();
							GetServiceRequestDetails(hdnRemid.Value, hdnempcode.Value);
							ActionDetails();
							var getType = Convert.ToString(Request.QueryString[1]).Trim();
							if (getType == "Rej")
							{
								empShow.Visible = false;
								empShow12.Visible = false;
								empShow2.Visible = false;
								ResponseTime.Visible = false;
								Delivery.Visible = false;
								divsh.Visible = false;
							}
						}
					}

					this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}

	//Close Service Request TO SPOC,HOD,CEO and Assgine By
	protected void service_btnAssgine_Click(object sender, EventArgs e)
	{

		try
		{
			var filename = "";
			string strfromDate = "";
			var strfileName = "";
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}

			var AssginetoDepartment = Convert.ToString(ddlDepartment.SelectedValue.ToString().Trim());
			if (AssginetoDepartment == "0")
			{
				lblmessage.Text = "Please select Assigned To Project Name.";
				return;
			}


			var AssginetoEmployee = Convert.ToString(ddl_AssginmentEMP.SelectedValue.ToString().Trim());
			if (AssginetoEmployee == "0")
			{
				lblmessage.Text = "Please select Assigned To Employee.";
				return;
			}
			if (Convert.ToString(txt_SPOCComment.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter customer first comment.";
				return;
			}

			lblmessage.Text = "";
			if (uploadfile.HasFile)
			{
				filename = uploadfile.FileName;
			}

			if (Convert.ToString(filename).Trim() != "")
			{
				//var getdate = Convert.ToDateTime(Txt_DateCreate.Text);
				DateTime loadedDate = DateTime.Now;
				strfromDate = loadedDate.ToString("ddMMyyyyHHmmss");
				filename = uploadfile.FileName;
				strfileName = "";
				strfileName = "CF_" + txtEmpCode.Text + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
				filename = strfileName;
				uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["CustomerFristPathpath"]).Trim()), strfileName));
			}
			var empCode = Convert.ToString(txtEmpCode.Text).Trim();
			var Serviceid = Convert.ToInt32(Convert.ToString(hdnRemid.Value).Trim());
			var ServicesDepartmentId = Convert.ToString(Convert.ToString(ddlDepartment.SelectedValue).Trim());
			var ServiceDescription = Convert.ToString(txt_SPOCComment.Text).Trim();
			var ActionBy = Convert.ToString(Session["Empcode"]).Trim();
			var Assgineto = ddl_AssginmentEMP.SelectedValue.ToString().Trim();
			// var CategoryId= Convert.ToInt32(Convert.ToString(hdnCategoryId.Value).Trim());
			var CategoryId = 0;//Convert.ToInt32(Convert.ToString(ddlCategory.SelectedValue).Trim());
			spm.AssgineCustEscalationDetails(empCode, ServiceDescription, "UpdateAction", filename, Assgineto, "", ServicesDepartmentId, 2, 2, ActionBy, '\0', Serviceid, CategoryId);

			//Send Email To Assgine
			//Send Email To SPOC
			var getId = Convert.ToString(hdnRemid.Value);
			string serviceRequestno = Convert.ToString(hdnServiceRequestNo.Value);
			string toMailIDs = "";

			string employeeName = txtEmpName.Text.ToString();
			var employee_Email = Txt_EmpEmail.Text.ToString();

			string createdDate = txtFirstCreatedDate.Text.ToString();
			string department = ddlDepartment.SelectedItem.ToString().Trim();
			string ccmailid = "";
			string redirectURL = Convert.ToString(ConfigurationManager.AppSettings["Customer_Req_App"]).Trim() + "?id=" + getId + "&type=app";
			string requestDescription = ServiceDescription;
			DataTable dtEmpDetails = new DataTable();
			dtEmpDetails = spm.GetEmployeeData(Assgineto);
			var assgineToName = "";
			if (dtEmpDetails.Rows.Count > 0)
			{
				assgineToName = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
				toMailIDs = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
			}
			var countForEx = Convert.ToInt32(hdnIsExceletd.Value);
			var IsExceletd = 0;
			if (countForEx == 0)
			{
				IsExceletd = 0;
			}
			else
			{
				IsExceletd = 1;
			}

			//spm.CustEscalation_send_mailto_AssignedAgent(IsExceletd, serviceRequestno, toMailIDs, employeeName, createdDate, department, "", redirectURL, requestDescription);
			var actionByName = Session["emp_loginName"].ToString();
			var actionName = "Assigned";
			redirectURL = Convert.ToString(ConfigurationManager.AppSettings["Customer_Req_App"]).Trim() + "?id=" + getId + "&type=emp";

			//spm.CustEscalation_send_mailto_AssignToEmployee(IsExceletd, serviceRequestno, employee_Email, actionName, actionByName, assgineToName, createdDate, department, "", redirectURL, requestDescription);

			//Send Email To Employee

			Response.Redirect("~/procs/CustEscalation.aspx");
		}
		catch (Exception ex)
		{
			lblmessage.Text = ex.Message.ToString();
			return;
		}

	}
	protected void mobile_btnPrintPV_Click(object sender, EventArgs e)
	{
		try
		{
			var filename = "";
			string strfromDate = "";
			var strfileName = "";
			string[] Delivery, Responses;
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value");
			string DeliveryDate = string.Empty, ResponseDate = string.Empty;
			if (confirmValue != "Yes")
			{
				return;
			}

			if (txtResponceDate.Text.Trim() == "")
			{
				lblmessage.Text = "Please enter Responce Date";
				return;
			}

			if (txtDeliveryDate.Text.Trim() == "")
			{
				lblmessage.Text = "Please enter Delivery Date";
				return;
			}
			if (Convert.ToString(txt_SPOCComment.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter Responce Remark.";
				return;
			}
			if (txtResponceDate.Text != "")
			{
				Responses = Convert.ToString(txtResponceDate.Text).Trim().Split('/');
				ResponseDate = Convert.ToString(Responses[2]) + "-" + Convert.ToString(Responses[1]) + "-" + Convert.ToString(Responses[0]);
				DateTime STDate = DateTime.ParseExact(ResponseDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
				ResponseDate = STDate.ToString();
			}
			if (txtDeliveryDate.Text != "")
			{
				Delivery = Convert.ToString(txtDeliveryDate.Text).Trim().Split('/');
				DeliveryDate = Convert.ToString(Delivery[2]) + "-" + Convert.ToString(Delivery[1]) + "-" + Convert.ToString(Delivery[0]);
				DateTime STDate = DateTime.ParseExact(DeliveryDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
				DeliveryDate = STDate.ToString();
			}
			lblmessage.Text = "";
			if (uploadfile.HasFile)
			{
				filename = uploadfile.FileName;
			}

			if (Convert.ToString(filename).Trim() != "")
			{
				//var getdate = Convert.ToDateTime(Txt_DateCreate.Text);
				DateTime loadedDate = DateTime.Now;
				strfromDate = loadedDate.ToString("ddMMyyyyHHmmss");
				filename = uploadfile.FileName;
				strfileName = "";
				strfileName = "CF_" + txtEmpCode.Text + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
				filename = strfileName;
				uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["CustomerFristPathpath"]).Trim()), strfileName));
			}
			var empCode = Convert.ToString(txtEmpCode.Text).Trim();
			var Serviceid = Convert.ToInt32(Convert.ToString(hdnRemid.Value).Trim());
			var ServicesDepartmentId = Convert.ToString(Convert.ToString(ddlDepartment.SelectedValue).Trim());
			var ServiceDescription = Convert.ToString(txt_SPOCComment.Text).Trim();
			var ActionBy = Convert.ToString(Session["Empcode"]).Trim();
			var Assgineto = Convert.ToString(Session["Empcode"]).Trim();//ddl_AssginmentEMP.SelectedValue.ToString().Trim();
																		// var CategoryId= Convert.ToInt32(Convert.ToString(hdnCategoryId.Value).Trim());
			var CategoryId = 0;//Convert.ToInt32(Convert.ToString(ddlCategory.SelectedValue).Trim());
			var ResponseDate1 = Convert.ToString(txtResponceDate.Text.Replace("/", "-"));
			spm.ResponceCustEscalationDetails(empCode, ServiceDescription, "UpdateResponce", filename, Assgineto, "", ServicesDepartmentId, 5, 5, ActionBy, '\0', Serviceid, CategoryId, ResponseDate, DeliveryDate);

			//Send Email To Assgine
			//Send Email To SPOC
			var getId = Convert.ToString(hdnRemid.Value);
			string serviceRequestno = Convert.ToString(hdnServiceRequestNo.Value);
			string toMailIDs = "";

			string employeeName = txtEmpName.Text.ToString();
			var employee_Email = Txt_EmpEmail.Text.ToString();

			string createdDate = txtFirstCreatedDate.Text.ToString();
			string department = ddlDepartment.SelectedItem.ToString().Trim();
			string ccmailid = "";
			string redirectURL = Convert.ToString(ConfigurationManager.AppSettings["Customer_Req_App"]).Trim() + "?id=" + getId + "&type=app";
			string requestDescription = ServiceDescription;
			DataTable dtEmpDetails = new DataTable();
			dtEmpDetails = spm.GetEmployeeData(Assgineto);
			var assgineToName = "";
			if (dtEmpDetails.Rows.Count > 0)
			{
				assgineToName = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
				toMailIDs = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
			}
			var countForEx = Convert.ToInt32(hdnIsExceletd.Value);
			var IsExceletd = 0;
			if (countForEx == 0)
			{
				IsExceletd = 0;
			}
			else
			{
				IsExceletd = 1;
			}

			//spm.CustEscalation_send_mailto_AssignedAgent(IsExceletd, serviceRequestno, toMailIDs, employeeName, createdDate, department, "", redirectURL, requestDescription);
			var actionByName = Session["emp_loginName"].ToString();
			var actionName = "Response";
			//redirectURL = Convert.ToString(ConfigurationManager.AppSettings["Customer_Req_App"]).Trim() + "?id=" + getId + "&type=emp";

			//spm.CustEscalation_send_mailto_Update_Response(IsExceletd, serviceRequestno, employee_Email, actionName, actionByName, assgineToName, createdDate, department, "", redirectURL, requestDescription, ResponseDate1, txtDeliveryDate.Text.Replace("/", "-"));

			//Send Email To Employee

			Response.Redirect("~/procs/CustEscalation.aspx");
		}
		catch (Exception ex)
		{
			lblmessage.Text = ex.Message.ToString();
			return;
		}
	}
	protected void service_btnSendSPOC_Click(object sender, EventArgs e)
	{

		try
		{
			var filename = "";
			string strfromDate = "";
			var strfileName = "";
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}
			if (Convert.ToString(txt_SPOCComment.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter Customer first Comment.";
				return;
			}

			lblmessage.Text = "";
			if (uploadfile.HasFile)
			{
				filename = uploadfile.FileName;
			}

			if (Convert.ToString(filename).Trim() != "")
			{
				//var getdate = Convert.ToDateTime(Txt_DateCreate.Text);
				DateTime loadedDate = DateTime.Now;
				strfromDate = loadedDate.ToString("ddMMyyyyHHmmss");
				filename = uploadfile.FileName;
				strfileName = "";
				strfileName = "CF_" + txtEmpCode.Text + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
				filename = strfileName;
				uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["CustomerFristPathpath"]).Trim()), strfileName));
			}

			//Assgine SPOC User
			var AssigneTo = "";
			DataTable dtSPOC = new DataTable();
			var id = Convert.ToString(hdnDepartmentID.Value.ToString().Trim());
			dtSPOC = spm.GetCustEscalationSPOCData(id);
			if (dtSPOC.Rows.Count > 0)
			{
				// txtEmpName.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
				AssigneTo = Convert.ToString(dtSPOC.Rows[0]["SPOC_ID"]);

			}

			var empCode = Convert.ToString(txtEmpCode.Text).Trim();
			var Serviceid = Convert.ToInt32(Convert.ToString(hdnRemid.Value).Trim());
			var ServicesDepartmentId = Convert.ToString(Convert.ToString(hdnDepartmentID.Value.ToString()).Trim());
			var ServiceDescription = Convert.ToString(txt_SPOCComment.Text).Trim();
			var ActionBy = Convert.ToString(Session["Empcode"]).Trim();
			var Assgineto = AssigneTo;
			var CategoryId = 0;//Convert.ToInt32(Convert.ToString(hdnCategoryId.Value).Trim());
			spm.AssgineCustEscalationDetails(empCode, ServiceDescription, "UpdateAction", filename, Assgineto, "", ServicesDepartmentId, 4, 4, ActionBy, '\0', Serviceid, CategoryId);

			//Send Email To SPOC
			DataTable dtEmpDetails = new DataTable();
			dtEmpDetails = spm.GetEmployeeData(Assgineto);
			var assgineToName = "";
			string toMailIDs = "";
			if (dtEmpDetails.Rows.Count > 0)
			{
				assgineToName = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);

				toMailIDs = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
			}

			var getId = Convert.ToString(hdnRemid.Value);
			string serviceRequestno = Convert.ToString(hdnServiceRequestNo.Value);


			string employeeName = txtEmpName.Text.ToString();
			var employee_Email = Txt_EmpEmail.Text.ToString();

			string createdDate = txtFirstCreatedDate.Text.ToString();
			string department = hdnDepartmentName.Value.ToString().Trim();
			string ccmailid = "";
			string redirectURL = Convert.ToString(ConfigurationManager.AppSettings["Customer_Req_App"]).Trim() + "?id=" + getId + "&type=app";
			string requestDescription = ServiceDescription;
			var actionByName = Session["emp_loginName"].ToString();
			var actionActionDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
			var strsubject = "Action update on your Customer First " + serviceRequestno + "";
			var getvalClose = Convert.ToInt32(hdnIsExceletd.Value);
			int IsCEO = 0;
			if (getvalClose == 1)
			{
				IsCEO = 0;
			}
			else if (getvalClose == 2)
			{
				IsCEO = 1;
			}
			else
			{
				IsCEO = 2;
			}

			//spm.CustEscalation_send_mailto_HeadOrCEOToSPOC(IsCEO, serviceRequestno, toMailIDs, employeeName, createdDate, department, "", redirectURL, requestDescription, actionActionDate, actionByName);



			Response.Redirect("~/procs/CustEscalation.aspx");
		}
		catch (Exception ex)
		{
			lblmessage.Text = ex.Message.ToString();
			return;
		}

	}
	protected void service_btnEscelateToCEO_Click(object sender, EventArgs e)
	{
		ClearActions();
	}

	// protected void service_btnEscelateToCEO_Click(object sender, EventArgs e)
	// {
	//     try
	//     {
	//         var filename = "";
	//         string strfromDate = "";
	//         var strfileName = "";
	//         string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
	//         if (confirmValue != "Yes")
	//         {
	//             return;
	//         }
	//         if (Convert.ToString(txt_SPOCComment.Text).Trim() == "")
	//         {
	//             lblmessage.Text = "Please enter  Comment.";
	//             return;
	//         }

	//         lblmessage.Text = "";
	//         if (uploadfile.HasFile)
	//         {
	//             filename = uploadfile.FileName;
	//         }

	//         if (Convert.ToString(filename).Trim() != "")
	//         {
	//             //var getdate = Convert.ToDateTime(Txt_DateCreate.Text);
	//             DateTime loadedDate = DateTime.Now;
	//             strfromDate = loadedDate.ToString("ddMMyyyyHHmmss");
	//             filename = uploadfile.FileName;
	//             strfileName = "";
	//             strfileName = "CF_" + txtEmpCode.Text + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
	//             filename = strfileName;
	//             uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["CustomerFristPathpath"]).Trim()), strfileName));
	//         }

	//         //Assgine SPOC User
	//         var AssigneTo = "";
	//         DataTable dtCEO = new DataTable();
	//         //var id = Convert.ToInt32(hdnDepartmentID.Value.ToString().Trim());
	//         dtCEO = spm.GetCustEscalationCEOEmpCode();
	//         if (dtCEO.Rows.Count > 0)
	//         {
	//             // txtEmpName.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
	//             AssigneTo = Convert.ToString(dtCEO.Rows[0]["Emp_Code"]);

	//         }

	//         var empCode = Convert.ToString(txtEmpCode.Text).Trim();
	//         var Serviceid = Convert.ToInt32(Convert.ToString(hdnRemid.Value).Trim());
	//         var ServicesDepartmentId = Convert.ToString(Convert.ToString(hdnDepartmentID.Value).Trim());
	//         var ServiceDescription = Convert.ToString(txt_SPOCComment.Text).Trim();
	//         var ActionBy = Convert.ToString(Session["Empcode"]).Trim();
	//         var Assgineto = AssigneTo;
	//var CategoryId = 0; Convert.ToInt32(Convert.ToString(hdnCategoryId.Value).Trim());
	//         spm.AssgineCustEscalationDetails(empCode, ServiceDescription, "EscalatedUSER", filename, Assgineto, "", ServicesDepartmentId, 9, 9, ActionBy, '\0', Serviceid,CategoryId);

	//         //Send Email To CEO
	//         DataTable dtEmpDetails = new DataTable();
	//         dtEmpDetails = spm.GetEmployeeData(Assgineto);
	//         var assgineToName = "";
	//         string toMailIDs = "";
	//         if (dtEmpDetails.Rows.Count > 0)
	//         {
	//             assgineToName = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);              
	//             toMailIDs = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
	//         }

	//         //
	//         var currentAssgine = Convert.ToString(hdnCurrentAssgineEMP.Value);
	//         dtEmpDetails = spm.GetEmployeeData(currentAssgine);
	//         var CurrentassgineToName = "";
	//         string ccEmailAddress = "";
	//         if (dtEmpDetails.Rows.Count > 0)
	//         {
	//             CurrentassgineToName = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);              
	//             ccEmailAddress = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
	//         }

	//         var getId = Convert.ToString(hdnRemid.Value);
	//         string serviceRequestno = Convert.ToString(hdnServiceRequestNo.Value);


	//         string employeeName = txtEmpName.Text.ToString();
	//         var employee_Email = Txt_EmpEmail.Text.ToString();

	//         string createdDate = txtFirstCreatedDate.Text.ToString();
	//         string department = hdnDepartmentName.Value.ToString().Trim();
	//         string ccmailid = ccEmailAddress;
	//         string redirectURL = Convert.ToString(ConfigurationManager.AppSettings["Customer_Req_App"]).Trim() + "?id=" + getId + "&type=app";
	//         string requestDescription = ServiceDescription;
	//         var actionByName = Session["emp_loginName"].ToString();
	//         var actionActionDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
	//         var strsubject = "Action update on your Customer First Request " + serviceRequestno + "";
	//         int IsCEO = 1;
	//         spm.CustEscalation_send_mailto_EscalationHeadOrCEO(IsCEO, serviceRequestno, toMailIDs, employeeName, createdDate, department, ccmailid, redirectURL, requestDescription, actionActionDate);



	//         Response.Redirect("~/procs/MyCustEscalation_Req.aspx");
	//     }
	//     catch (Exception ex)
	//     {
	//         lblmessage.Text = ex.Message.ToString();
	//         return;
	//     }
	// }

	protected void service_btnEscelateToHOD_Click(object sender, EventArgs e)
	{
		try
		{
			var filename = "";
			string strfromDate = "";
			var strfileName = "";
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}
			if (Convert.ToString(txt_SPOCComment.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter customer first Comment.";
				return;
			}

			lblmessage.Text = "";
			if (uploadfile.HasFile)
			{
				filename = uploadfile.FileName;
			}

			if (Convert.ToString(filename).Trim() != "")
			{
				//var getdate = Convert.ToDateTime(Txt_DateCreate.Text);
				DateTime loadedDate = DateTime.Now;
				strfromDate = loadedDate.ToString("ddMMyyyyHHmmss");
				filename = uploadfile.FileName;
				strfileName = "";
				strfileName = "CF_" + txtEmpCode.Text + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
				filename = strfileName;
				uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["CustomerFristPathpath"]).Trim()), strfileName));
			}

			//Assgine SPOC User
			var AssigneTo = "";
			DataTable dtCEO = new DataTable();
			var id = Convert.ToInt32(hdnDepartmentID.Value.ToString().Trim());
			dtCEO = spm.GetCustEscalationDEPARTMENTHOD(id);
			if (dtCEO.Rows.Count > 0)
			{
				// txtEmpName.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
				AssigneTo = Convert.ToString(dtCEO.Rows[0]["Emp_Code"]);

			}

			var empCode = Convert.ToString(txtEmpCode.Text).Trim();
			var Serviceid = Convert.ToInt32(Convert.ToString(hdnRemid.Value).Trim());
			var ServicesDepartmentId = Convert.ToString(Convert.ToString(hdnDepartmentID.Value).Trim());
			var ServiceDescription = Convert.ToString(txt_SPOCComment.Text).Trim();
			var ActionBy = Convert.ToString(Session["Empcode"]).Trim();
			var Assgineto = AssigneTo;
			var CategoryId = 0;//Convert.ToInt32(Convert.ToString(hdnCategoryId.Value).Trim());
			var getdeptName = Convert.ToString(hdnDepartmentName.Value).Trim(); //Technology & Innovation

			spm.AssgineCustEscalationDetails(empCode, ServiceDescription, "EscalatedUSER", filename, Assgineto, "", ServicesDepartmentId, 12, 12, ActionBy, '\0', Serviceid, CategoryId);



			//Send Email To HOD
			DataTable dtEmpDetails = new DataTable();
			dtEmpDetails = spm.GetEmployeeData(Assgineto);
			var assgineToName = "";
			string toMailIDs = "";
			if (dtEmpDetails.Rows.Count > 0)
			{
				assgineToName = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);

				toMailIDs = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
			}

			//
			var currentAssgine = Convert.ToString(hdnCurrentAssgineEMP.Value);
			dtEmpDetails = spm.GetEmployeeData(currentAssgine);
			var CurrentassgineToName = "";
			string ccEmailAddress = "";
			if (dtEmpDetails.Rows.Count > 0)
			{
				CurrentassgineToName = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);

				ccEmailAddress = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
			}
			//

			var getId = Convert.ToString(hdnRemid.Value);
			string serviceRequestno = Convert.ToString(hdnServiceRequestNo.Value);


			string employeeName = txtEmpName.Text.ToString();
			var employee_Email = Txt_EmpEmail.Text.ToString();

			string createdDate = txtFirstCreatedDate.Text.ToString();
			string department = getdeptName;
			//string ccmailid = "";
			string ccmailid = ccEmailAddress;
			string redirectURL = Convert.ToString(ConfigurationManager.AppSettings["Customer_Req_App"]).Trim() + "?id=" + getId + "&type=app";
			string requestDescription = ServiceDescription;
			var actionByName = Session["emp_loginName"].ToString();
			var actionActionDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
			var strsubject = "Action update on your Service Request " + serviceRequestno + "";
			int IsCEO = 0;
			spm.CustEscalation_send_mailto_EscalationHeadOrCEO(IsCEO, serviceRequestno, toMailIDs, employeeName, createdDate, department, ccmailid, redirectURL, requestDescription, actionActionDate);


			Response.Redirect("~/procs/MyCustEscalation_Req.aspx");
		}
		catch (Exception ex)
		{
			lblmessage.Text = ex.Message.ToString();
			return;
		}
	}


	protected void service_btnClose_Click(object sender, EventArgs e)
	{
		string strclaim_month = "";
		string[] strdate;
		string strfromDate = "";
		decimal recm_amount = 0;
		lblmessage.Visible = true;
		try
		{
			var filename = "";
			var strfileName = "";
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}

			if (Convert.ToString(txt_SPOCComment.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter Customer First Remark.";
				return;
			}

			lblmessage.Text = "";
			if (uploadfile.HasFile)
			{
				filename = uploadfile.FileName;
			}

			if (Convert.ToString(filename).Trim() != "")
			{
				//var getdate = Convert.ToDateTime(Txt_DateCreate.Text);
				DateTime loadedDate = DateTime.Now;
				strfromDate = loadedDate.ToString("ddMMyyyyHHmmss");
				filename = uploadfile.FileName;
				strfileName = "";
				strfileName = "CF_" + txtEmpCode.Text + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
				filename = strfileName;
				uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["CustomerFristPathpath"]).Trim()), strfileName));
			}
			var empCode = Convert.ToString(txtEmpCode.Text).Trim();
			var Serviceid = Convert.ToInt32(Convert.ToString(hdnRemid.Value).Trim());
			var ServicesDepartmentId = Convert.ToString(Convert.ToString(ddlDepartment.SelectedValue).Trim());
			var ServiceDescription = Convert.ToString(txt_SPOCComment.Text).Trim();
			var ActionBy = Convert.ToString(Session["Empcode"]).Trim();
			var CategoryId = 0;//Convert.ToInt32(Convert.ToString(hdnCategoryId.Value).Trim());          
			if (ServicesDepartmentId == "0")
			{
				ServicesDepartmentId = Convert.ToString(Convert.ToString(hdnDepartmentID.Value).Trim());
			}
			spm.AssgineCustEscalationDetails(empCode, ServiceDescription, "UpdateAction", filename, ActionBy, "", ServicesDepartmentId, 3, 3, ActionBy, '\0', Serviceid, CategoryId);



			//Send Email To Employee
			var getId = Convert.ToString(hdnRemid.Value);
			string serviceRequestno = Convert.ToString(hdnServiceRequestNo.Value);
			string toMailIDs = "";

			string employeeName = txtEmpName.Text.ToString();
			var employee_Email = Txt_EmpEmail.Text.ToString();

			string createdDate = txtFirstCreatedDate.Text.ToString();
			string department = hdnDepartmentName.Value.ToString().Trim();
			//string ccmailid = "";
			string redirectURL = Convert.ToString(ConfigurationManager.AppSettings["Customer_Req_App"]).Trim() + "?id=" + getId + "&type=app";
			string requestDescription = ServiceDescription;
			DataTable dtEmpDetails = new DataTable();
			//dtEmpDetails = spm.GetEmployeeData(Assgineto);
			var actionByName = Session["emp_loginName"].ToString();
			var actionName = "Closed";
			redirectURL = Convert.ToString(ConfigurationManager.AppSettings["Customer_Req_App"]).Trim() + "?id=" + getId + "&type=emp";

			var countForEx = Convert.ToInt32(hdnIsExceletd.Value);
			var IsExceletd = 0;
			if (countForEx == 1)
			{
				IsExceletd = 1;
			}
			else if (countForEx == 2)
			{
				IsExceletd = 2;
			}
			else if (IsExceletd == 0)
			{
				IsExceletd = 0;
			}
			else
			{
				IsExceletd = 10;
			}

			var getCEOEmpId = "";
			var dtCEO = spm.GetCustEscalationCEOEmpCode();
			if (dtCEO.Rows.Count > 0)
			{
				getCEOEmpId = Convert.ToString(dtCEO.Rows[0]["Emp_Code"]);
			}
			string ccMailIDs = GetCustEscalation_CCMail_Deatils(Serviceid, ActionBy);
			if (getCEOEmpId != ActionBy)
			{
				//spm.CustEscalation_send_mailto_ActionToEmployee(IsExceletd, serviceRequestno, employee_Email, actionName, actionByName, createdDate, department, "", redirectURL, requestDescription);
			}
			//Send Email To Employee
			Response.Redirect("~/procs/CustEscalation.aspx");
		}
		catch (Exception ex)
		{
			lblmessage.Text = ex.Message.ToString();
			return;
		}
	}

	public string GetCustEscalation_CCMail_Deatils(int Serviceid, string Assgineto)
	{

		try
		{
			String sbapp = "";
			DataTable dsTrDetails = new DataTable();
			dsTrDetails = spm.GetCustEscalation_CCMail_Deatils("GETCCSENDMAILESCALATION", Serviceid, Assgineto);
			if (dsTrDetails.Rows.Count > 0)
			{
				for (int i = 0; i < dsTrDetails.Rows.Count; i++)
				{
					if (Convert.ToString(sbapp).Trim() == "")
						sbapp = Convert.ToString(dsTrDetails.Rows[0]["Emp_Emailaddress"]).Trim();
					else
						sbapp = sbapp + ";" + Convert.ToString(dsTrDetails.Rows[i]["Emp_Emailaddress"]).Trim();

				}
			}
			return Convert.ToString(sbapp);
		}
		catch (Exception)
		{

			throw;
		}


	}
	protected void service_btnClearText_Click(object sender, EventArgs e)
	{
		var getType = Convert.ToString(Request.QueryString[1]).Trim();
		if (getType == "emp")
		{
			Response.Redirect("~/procs/MyCustEscalation_Req.aspx");
		}
		else
		{
			Response.Redirect("~/procs/InboxCustEscalationApp.aspx");
		}
	}

	protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			DropDownList list = (DropDownList)sender;
			string value = (string)list.SelectedValue;
			if (value == "0")
			{
				ddl_AssginmentEMP.Items.Clear();
				ddlCategory.Items.Clear();
				ddl_AssginmentEMP.DataSource = null;
				ddl_AssginmentEMP.DataBind();

				ddlCategory.DataSource = null;
				ddlCategory.DataBind();
			}
			else
			{
				ddl_AssginmentEMP.Items.Clear();
				//ddlCategory.Items.Clear();
				ddl_AssginmentEMP.DataSource = null;
				ddl_AssginmentEMP.DataBind();

				//ddlCategory.DataSource = null;
				// ddlCategory.DataBind();
				BindEmpDDL(value);

			}

		}
		catch (Exception)
		{

			throw;
		}
	}

	public void BindEmpDDL(string departmentId)
	{
		try
		{

			// var dptId = Convert.ToInt32(departmentId);
			var getDepartment = spm.GetCustEscalationEmpDepartment(departmentId);
			if (getDepartment.Rows.Count > 0)
			{
				ddl_AssginmentEMP.DataSource = getDepartment;
				ddl_AssginmentEMP.DataTextField = "EmployeeName";
				ddl_AssginmentEMP.DataValueField = "Emp_Code";
				ddl_AssginmentEMP.DataBind();
				ddl_AssginmentEMP.Items.Insert(0, new ListItem("Select Assigned To Employee", "0")); //updated code
			}
			else
			{
				ddl_AssginmentEMP.DataSource = null;
			}


		}
		catch (Exception)
		{

			throw;
		}
	}
	protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
	{
		try
		{
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			var id = Convert.ToString(gvServiceHistory.DataKeys[row.RowIndex].Values[0]).Trim();
			var getDetails = spm.GETCustEscalationHISTORYBYID(Convert.ToInt32(id));
			if (getDetails.Rows.Count > 0)
			{
				var getDate = Convert.ToString(getDetails.Rows[0]["ActionDate"].ToString());
				var getDescripation = Convert.ToString(getDetails.Rows[0]["ServicesDescripation"].ToString());
				var getFileName = Convert.ToString(getDetails.Rows[0]["FilePath"].ToString());
				var getAssignedDate = Convert.ToString(getDetails.Rows[0]["AssignedDate"].ToString());
				var getAssignedBY = Convert.ToString(getDetails.Rows[0]["AssignedBY"].ToString());
				var getActiondBY = Convert.ToString(getDetails.Rows[0]["ActionBy"].ToString());
				var getStatusName = Convert.ToString(getDetails.Rows[0]["StatusName"].ToString());
				var ResponseDates = Convert.ToString(getDetails.Rows[0]["ResponseDate"].ToString());
				var DeliveryDates = Convert.ToString(getDetails.Rows[0]["DeliveryDate"].ToString());

				txt_AssgimentShowDate.Text = getDate;
				txt_Service_Description.Text = getDescripation;
				//txt_Assigne_By.Text = getAssignedBY;
				//txt_ASDate.Text = getAssignedDate;
				bindFilePath.InnerText = getFileName;
				txt_ASDate.Text = getStatusName;
				txtActionBy.Text = getActiondBY;
				txt_Assigne_By.Text = getAssignedBY;
				if (!string.IsNullOrEmpty(DeliveryDates))
				{

					txtResponseDates.Text = ResponseDates.Substring(0, 10);
					txtDeliveryDates.Text = DeliveryDates;
					Response2.Visible = true;
					Response1.Visible = true;
				}
				else
				{

					txtResponseDates.Text = "";
					txtDeliveryDates.Text = "";
				}

				if (getStatusName == "Closed")
				{
					//txt_Assigne_By.Text = "";
				}
				else if (getStatusName == "Not Satisfied")
				{
					txt_Assigne_By.Text = "";
					// txt_ASDate.Text = "Pending";
				}


			}
		}
		catch (Exception ex)
		{
			lblmessage.Text = ex.Message.ToString();
		}
	}

	private void checkRejectStatus_Submit()
	{
		try
		{
			DataTable dtTrDetails = new DataTable();
			SqlParameter[] spars = new SqlParameter[3];
			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = "GETRejectPageDetail";
			spars[1] = new SqlParameter("@id", SqlDbType.Int); 
			spars[1].Value = Convert.ToInt32(hdnRemid.Value);
			spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
			spars[2].Value = Convert.ToString(hdnempcode.Value).Trim();
			dtTrDetails = spm.getMobileRemDataList(spars, "SP_INSERTCUST_ESCALATION");
			if (dtTrDetails.Rows.Count == 0)
			{
				Response.Redirect("~/procs/InboxCustEscalationApp.aspx");				
			}
			if (dtTrDetails.Rows.Count > 0)
			{
				if (Convert.ToString(dtTrDetails.Rows[0]["Status"]) != "Pending")
				{					
					Response.Redirect("~/procs/InboxCustEscalationApp.aspx");
				}
			}

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}


	}
	public void GetServiceRequestDetails(string id, string empCode)
	{
		try
		{
			hdnIsExceletd.Value = "0";
			DataSet ds = new DataSet();
			ds = spm.getCustEscalationDetail(Convert.ToInt32(id), empCode);
			var departmentId = ds.Tables[3].Rows[0]["ServiceDepartment"].ToString();
			hdnDepartmentID.Value = ds.Tables[3].Rows[0]["ServiceDepartment"].ToString();
			//hdnCategoryId.Value = ds.Tables[3].Rows[0]["CategoryId"].ToString();
			hdnCurrentAssgineEMP.Value = ds.Tables[3].Rows[0]["AssignedTo"].ToString();
			hdnStatusID.Value = ds.Tables[3].Rows[0]["Status"].ToString();
			//Assgine Emp Data
			if (ds.Tables[0].Rows.Count > 0)
			{
				var empdetail = ds.Tables[0];
				txtEmpName.Text = Convert.ToString(empdetail.Rows[0]["Emp_Name"]);
				Txt_DeptName.Text = Convert.ToString(empdetail.Rows[0]["Department"]);
				txtEmpCode.Text = Convert.ToString(empdetail.Rows[0]["Emp_Code"]);
				Txt_Designation.Text = Convert.ToString(empdetail.Rows[0]["Designation"]).Trim();
				Txt_EmpEmail.Text = Convert.ToString(empdetail.Rows[0]["Emp_Emailaddress"]);
				Txt_EmpMobile.Text = Convert.ToString(empdetail.Rows[0]["mobile"]);
			}
			//Assigne  Service History
			gvServiceHistory.DataSource = null;
			gvServiceHistory.DataBind();
			if (ds.Tables[1].Rows.Count > 0)
			{
				var rowsCount = ds.Tables[1].Rows.Count;

				var getServiceLog = ds.Tables[1];
				//Asgine Value For History detail
				var getRowData = ds.Tables[1].Rows[rowsCount - 1];

				var getDate = Convert.ToString(getRowData["ActionDate"].ToString());
				var getDescripation = Convert.ToString(getRowData["ServicesDescripation"].ToString());
				var getFileName = Convert.ToString(getRowData["FilePath"].ToString());
				var getServiceRequestID = Convert.ToString(getRowData["ServicesRequestID"].ToString());
				var getAssignedDate = Convert.ToString(getRowData["AssignedDate"].ToString());
				var getAssignedBY = Convert.ToString(getRowData["AssignedBY"].ToString());
				var getActiondBY = Convert.ToString(getRowData["ActionBy"].ToString());
				var getStatusName = Convert.ToString(getRowData["StatusName"].ToString());

				var getFristRowData = ds.Tables[1].Rows[0];
				var getFiestAssignedDate = Convert.ToString(getFristRowData["AssignedDate"].ToString());
				var getFirstDescription = Convert.ToString(getFristRowData["ServicesDescripation"].ToString());
				//var getFirstCategory = Convert.ToString(getFristRowData["CategoryTitle"].ToString());
				var getFirstFilePath = Convert.ToString(getFristRowData["FilePath"].ToString());
				txtFirstCreatedDate.Text = getFiestAssignedDate;
				lblCreateFile.InnerText = getFirstFilePath;
				txtFirstDeacription.Text = getFirstDescription;
				txtSelectedDepartment.Text = Convert.ToString(getFristRowData["Location_name"].ToString());
				txt_category.Text = Convert.ToString(getFristRowData["ModeName"].ToString());
				TXTSeverity.Text = Convert.ToString(getFristRowData["Severity_Name"].ToString());
				TXTCustomerSatisfaction.Text = Convert.ToString(getFristRowData["Cust_Satisfaction_Name"].ToString());
				TXTImpactProject.Text = Convert.ToString(getFristRowData["Cust_Impact_Name"].ToString());
				txtRolePerson.Text = Convert.ToString(getFristRowData["CustRole_Name"].ToString());


				//txt_category.Text = getFirstCategory;
				hdnServiceRequestNo.Value = getServiceRequestID;
				lblheading.Text = "Customer First Incident - " + getServiceRequestID;
				txt_AssgimentShowDate.Text = getDate;
				txt_Service_Description.Text = getDescripation;
				bindFilePath.InnerText = getFileName;
				txt_Assigne_By.Text = getAssignedBY;
				txt_ASDate.Text = getStatusName;
				txtActionBy.Text = getActiondBY;
				if (getStatusName == "Closed")
				{
					txt_Assigne_By.Text = "";
					mobile_btnPrintPV.Visible = false;
					Delivery.Visible = false;

				}
				if (hdnStatusID.Value.ToString() == "2")
				{
					service_btnClose.Visible = false;

				}
				if (hdnStatusID.Value.ToString() == "5")
				{
					mobile_btnPrintPV.Visible = false;
					service_btnCreateAction.Style.Add("display", "inline-block");
					service_btnClose.Visible = false;
					//Delivery.Style.Add("display", "none");
					Deleverydate1.Visible = false;
					txtDeliveryDate.Visible = false;
					Responselabel.InnerText = "Date";
					txtResponceDate.Enabled = false;
				}
				else if (getStatusName == "Not Satisfied")
				{
					txt_Assigne_By.Text = "";
					// txt_ASDate.Text = "Pending";
				}
				var rows2 = from row in ds.Tables[1].AsEnumerable()
							where row.Field<string>("StatusName") == "Closed"
							select row;

				int count2 = rows2.Count<DataRow>();
				if (count2 == 1)
				{
					hdnIsExceletd.Value = "1";//Escelate to HOD
				}
				else if (count2 == 2)
				{
					hdnIsExceletd.Value = "2";//Escelate to CEO
				}
				else
				{
					hdnIsExceletd.Value = "0";//
				}

				if (getServiceLog.Rows.Count > 1)
				{
					DataTable selectedTable = getServiceLog.AsEnumerable().Where(r => r.Field<string>("ReceivedDate") != null).CopyToDataTable();
					gvServiceHistory.DataSource = selectedTable;
					gvServiceHistory.DataBind();
				}
				else
				{
					gvServiceHistory.DataSource = getServiceLog;
					gvServiceHistory.DataBind(); ;
				}

			}

			if (ds.Tables[2].Rows.Count > 0)
			{
				var getEmpDetail = ds.Tables[2];
				//var getCat = ds.Tables[4];
				loadDropDownEmp(getEmpDetail, departmentId);
				var selectedDepartment = getEmpDetail.AsEnumerable().Where(r => r.Field<string>("DepartmentId") == Convert.ToString(departmentId)).Select(s => s.Field<string>("DepartmentName")).FirstOrDefault();
				// txtSelectedDepartment.Text = selectedDepartment;
				// BindCategoryDDL(departmentId);
				// ddlCategory.SelectedValue = Convert.ToString(hdnCategoryId.Value);
				BindEmpDDL(Convert.ToString(departmentId));
				//var getCatogary = getCat.AsEnumerable().Where(r => r.Field<int>("Id") == Convert.ToInt32(Convert.ToString(hdnCategoryId.Value))).Select(s => s.Field<string>("CategoryTitle")).FirstOrDefault();
				//txt_category.Text = getCatogary;
			}
			if (ds.Tables[3].Rows.Count > 0)
			{
				var departID = Convert.ToString(ds.Tables[3].Rows[0]["ServiceDepartment"].ToString());
				var RespDate = Convert.ToString(ds.Tables[3].Rows[0]["ResponseDate"].ToString());
				var DelivaryDate = Convert.ToString(ds.Tables[3].Rows[0]["DeliveryDate"].ToString());
				txtEscalationBy.Text = Convert.ToString(ds.Tables[3].Rows[0]["EscalationRaisedBy"].ToString());
				txtEmailID.Text = Convert.ToString(ds.Tables[3].Rows[0]["EmailID"].ToString());
				txtIncidentNumber.Text = Convert.ToString(ds.Tables[3].Rows[0]["ServicesRequestID"].ToString());
				hdnDelivaryDate.Value = DelivaryDate;
				hdnResponseDate.Value = RespDate;
				//foreach (DataRow drow in ds.Tables[2].Rows)
				//{
				//    var value = Convert.ToInt32(drow["DepartmentId"].ToString());
				//    if (departID == value)
				//        hdnDepartmentName.Value = drow["DepartmentName"].ToString();
				//}

				var getType = Convert.ToString(Request.QueryString[1]).Trim();
				var getService = ds.Tables[3];
				var getStatusCEO = Convert.ToInt32(getService.Rows[0]["Status"].ToString());

				if (getType == "emp")
				{
					service_btnCreateAction.Style.Add("display", "none");
					service_btnClose.Visible = false;
					var getStatus = Convert.ToInt32(getService.Rows[0]["Status"].ToString());
					if (getStatus == 3)
					{
						lblAssgineTitle.InnerText = "Escalation Details";
						lblActionAssginDate.InnerText = "Escalation Date";
						lblActionAssginComment.InnerText = "Escalation Description";
						//add by bharat mainkar 11-10-21
						empShow1.Visible = false;
						empShow4.Visible = false;
						empShow5.Visible = false;
						empShow14.Visible = false;
						//end 
						var getDayes = 8;
						var diffDay = 0.00;
						var getTodayDate = DateTime.Now.Date;
						var lastupdatedDate = Convert.ToDateTime(getService.Rows[0]["AssignedDate"].ToString()).Date;//AssignedDate
																													 //Call Day Count
						string officeLocation = "HO-NaviMum";
						var getDayesdt = spm.GetCustEscalationServiceCount(officeLocation, lastupdatedDate, getTodayDate);
						if (getDayesdt.Rows.Count > 0)
						{
							diffDay = Convert.ToInt32(getDayesdt.Rows[0]["WORKINGDAY"]);
						}
						else
						{
							diffDay = (getTodayDate - lastupdatedDate).TotalDays;
						}
						var getDepartment = Convert.ToString(hdnDepartmentID.Value);
						DataTable dtSPOC = spm.GetCustEscalationSPOCData(getDepartment);
						if (dtSPOC.Rows.Count > 0)
						{
							getDayes = Convert.ToInt32(dtSPOC.Rows[0]["USER_ESCALATION"]);
						}
						if (getDayes >= diffDay)
						{
							empShow1.Visible = true;
							empShow3.Visible = true;
							empShow4.Visible = true;
							empShow5.Visible = true;
							var rows = from row in ds.Tables[1].AsEnumerable()
									   where row.Field<string>("StatusName") == "Closed"
									   select row;

							var rowsCEO = from row in ds.Tables[1].AsEnumerable() where row.Field<string>("StatusName") == "Escalated - CEO" select row;
							var rowsHead = from row in ds.Tables[1].AsEnumerable() where row.Field<string>("StatusName") == "Escalated - HR Head(Auto)" || row.Field<string>("StatusName") == "Escalated - IT Head(Auto)" select row;

							int count = rows.Count<DataRow>();
							int countCEOStatus = rowsCEO.Count<DataRow>();
							int countHeadStatus = rowsHead.Count<DataRow>();


							// int numberOfRecords = ds.Tables[1].Select("StatusName = Closed").Length;
							if (countCEOStatus == 1)
							{
								empShow1.Visible = false;
								empShow12.Visible = false;
								empShow.Visible = false;
								empShow2.Visible = false;
								empShow3.Visible = false;
								empShow4.Visible = false;
								empShow5.Visible = false;
								empShow7.Visible = false;
								service_btnEscelateToHOD.Visible = false;
								//service_btnEscelateToCEO.Visible = false;
								service_btnClearText.Visible = false;
							}
							else if (countHeadStatus == 1)
							{
								hdnIsExceletd.Value = "2";//Escelate to CEO
								service_btnEscelateToHOD.Visible = false;

								service_btnClearText.Visible = true;

								lblAssgineTitle.InnerText = "Bad Service Feedback";
								lblActionAssginDate.InnerText = "Feedback Date";
								lblActionAssginComment.InnerText = "Feedback Description";
							}
							else if (count == 1)
							{
								hdnIsExceletd.Value = "1";//Escelate to HOD
								service_btnEscelateToHOD.Visible = true;
								//service_btnEscelateToCEO.Visible = false;
								service_btnClearText.Visible = true;

								//add by bharat mainkar 11-10-21
								empShow1.Visible = false;
								empShow4.Visible = false;
								empShow5.Visible = false;
								empShow14.Visible = false;
								empShow3.Visible = false;
								empShow7.Visible = false;
								//end 
							}
							else if (count == 2)
							{
								hdnIsExceletd.Value = "2";//Escelate to CEO
								service_btnEscelateToHOD.Visible = false;

								service_btnClearText.Visible = true;

								lblAssgineTitle.InnerText = "Bad Service Feedback";
								lblActionAssginDate.InnerText = "Feedback Date";
								lblActionAssginComment.InnerText = "Feedback Description";
							}
							else
							{
								empShow1.Visible = false;
								empShow12.Visible = false;
								empShow.Visible = false;
								empShow2.Visible = false;
								empShow3.Visible = false;
								empShow4.Visible = false;
								empShow5.Visible = false;
								empShow7.Visible = false;

								service_btnEscelateToHOD.Visible = false;
								// service_btnEscelateToCEO.Visible = false;
								service_btnClearText.Visible = false;
							}
						}
						else
						{
							empShow1.Visible = false;
							empShow12.Visible = false;
							empShow.Visible = false;
							empShow2.Visible = false;
							empShow3.Visible = false;
							empShow4.Visible = false;
							empShow5.Visible = false;
							empShow7.Visible = false;
							service_btnEscelateToHOD.Visible = false;
							//service_btnEscelateToCEO.Visible = false;
							service_btnClearText.Visible = false;
						}

					}
					else
					{
						empShow1.Visible = false;
						empShow12.Visible = false;
						empShow.Visible = false;
						empShow2.Visible = false;
						empShow3.Visible = false;
						empShow4.Visible = false;
						empShow5.Visible = false;
						empShow7.Visible = false;
						service_btnCreateAction.Style.Add("display", "none");
						service_btnEscelateToHOD.Visible = false;
						//service_btnEscelateToCEO.Visible = false;
						service_btnClearText.Visible = false;
					}
				}

				if (getStatusCEO == 9)
				{
					service_btnSendSPOC.Visible = false;
				}
			}
			// Bind Depart Ment
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
			Response.End();
			throw;
		}

	}
	public void loadDropDownEmp(DataTable dtleaveInbox, string id)
	{
		ddlDepartment.DataSource = dtleaveInbox;
		//ddlDepartment.DataBind();
		ddlDepartment.DataTextField = "DepartmentName";
		ddlDepartment.DataValueField = "DepartmentId";
		ddlDepartment.SelectedValue = id;
		ddlDepartment.DataBind();
		ddlDepartment.Items.Insert(0, new ListItem("Select Assigned To Project", "0")); //updated code

	}


	#endregion

	#region PageMethods

	private void getPayementVoucher_forPrint()
	{
		try
		{


			#region get payment Voucher details
			DataSet dspaymentVoucher = new DataSet();
			SqlParameter[] spars = new SqlParameter[2];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "mobile_claim";

			spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
			spars[1].Value = Convert.ToDecimal(hdnRemid.Value);

			dspaymentVoucher = spm.getDatasetList(spars, "rpt_dataProcedure");

			#endregion

			if (dspaymentVoucher.Tables[0].Rows.Count > 0)
			{
				ReportViewer ReportViewer1 = new ReportViewer();


				ReportViewer1.LocalReport.Refresh();
				ReportViewer1.LocalReport.DataSources.Clear();

				ReportParameter[] param = new ReportParameter[19];
				param[0] = new ReportParameter("pdocno", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Vouno"]));
				param[1] = new ReportParameter("ppvdate", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Rem_Sub_Date"]));
				param[2] = new ReportParameter("pempName", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Emp_Name"]));
				param[3] = new ReportParameter("pempCode", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Empcode"]));
				param[4] = new ReportParameter("pRsinwords", Convert.ToString(dspaymentVoucher.Tables[2].Rows[0]["AmountinWords"]));
				param[7] = new ReportParameter("pBankName", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["tperiod"]));

				#region Cost Cente & Bank Details
				if (dspaymentVoucher.Tables[3].Rows.Count > 0)
				{
					param[5] = new ReportParameter("pCostCenterCode", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["Cost_Center"]));
					param[6] = new ReportParameter("pCostCenterNM", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["Cost_Center_desc"]));

					//param[7] = new ReportParameter("pBankName", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["BANK_name"]));
					param[8] = new ReportParameter("pBankAccCode", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["BANK_acc"]));
					param[9] = new ReportParameter("pBankIFSCcode", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["BANK_ifsc"]));
					param[10] = new ReportParameter("pBankBranch", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["BANK_Branch"]));

					param[11] = new ReportParameter("pRbnkName", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["R_BANK_name"]));
					param[12] = new ReportParameter("pRBnkAccCode", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["R_BANK_acc"]));
					param[13] = new ReportParameter("pRbnkIFSCcode", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["R_BANK_ifsc"]));
					param[14] = new ReportParameter("pRbnkBranch", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["R_BANK_Branch"]));
				}
				else
				{
					param[5] = new ReportParameter("pCostCenterCode", Convert.ToString(""));
					param[6] = new ReportParameter("pCostCenterNM", Convert.ToString(""));

					//param[7] = new ReportParameter("pBankName", Convert.ToString(""));
					param[8] = new ReportParameter("pBankAccCode", Convert.ToString(""));
					param[9] = new ReportParameter("pBankIFSCcode", Convert.ToString(""));
					param[10] = new ReportParameter("pBankBranch", Convert.ToString(""));

					param[11] = new ReportParameter("pRbnkName", Convert.ToString(""));
					param[12] = new ReportParameter("pRBnkAccCode", Convert.ToString(""));
					param[13] = new ReportParameter("pRbnkIFSCcode", Convert.ToString(""));
					param[14] = new ReportParameter("pRbnkBranch", Convert.ToString(""));
				}
				#endregion

				param[15] = new ReportParameter("pContact", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["mobile"]));
				param[16] = new ReportParameter("PAlt_Contact", "");
				param[17] = new ReportParameter("pProjectName", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Project_Name"]));
				param[18] = new ReportParameter("pDept_Name", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Dept_Name"]));

				// Create Report DataSource
				ReportDataSource rds = new ReportDataSource("dspaymentVoucher", dspaymentVoucher.Tables[0]);
				ReportDataSource rdsApprs = new ReportDataSource("dspaymentVoucher_Apprs", dspaymentVoucher_Apprs.Tables[0]);

				ReportViewer1.ProcessingMode = ProcessingMode.Local;
				ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/PaymentVoucher.rdlc");
				ReportViewer1.LocalReport.DataSources.Add(rds);
				ReportViewer1.LocalReport.DataSources.Add(rdsApprs);
				ReportViewer1.LocalReport.SetParameters(param);


				#region Create payment Voucher PDF file
				Warning[] warnings;
				string[] streamIds;
				string mimeType = string.Empty;
				string encoding = string.Empty;
				string extension = string.Empty;
				DataTable DataTable1 = new DataTable();
				ReportViewer1.LocalReport.EnableExternalImages = true;
				ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
				byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
				Response.Buffer = true;
				Response.Clear();
				Response.ContentType = mimeType;
				Response.AddHeader("content-disposition", "attachment; filename=paymentVoucherDetails." + extension);
				try
				{
					Response.BinaryWrite(bytes);
				}
				catch (Exception ex)
				{
					Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error while generating PDF.');", true);
					Console.WriteLine(ex.StackTrace);
				}

				#endregion


			}

		}
		catch (Exception ex)
		{
		}
	}

	private void checkApprovalStatus_Submit()
	{
		try
		{

			SqlParameter[] spars = new SqlParameter[3];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "check_MobReimbstReqAppr_Status";

			spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
			spars[1].Value = Convert.ToDecimal(hdnRemid.Value);

			spars[2] = new SqlParameter("@apprempcode", SqlDbType.VarChar);
			spars[2].Value = Convert.ToString(hdnempcode.Value);

			DataTable dtTrDetails = spm.getMobileRemDataList(spars, "[SP_GETALLreembursement_DETAILS]");
			if (dtTrDetails.Rows.Count == 0)
			{
				Response.Redirect("~/procs/InboxMobile.aspx?app=" + hdnInboxType.Value);
			}
			if (dtTrDetails.Rows.Count > 0)
			{
				if (Convert.ToString(dtTrDetails.Rows[0]["pvappstatus"]) != "Pending")
				{
					Response.Redirect("~/procs/InboxMobile.aspx?app=" + hdnInboxType.Value);
				}
			}

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}


	}

	public void GetEmployeeDetails()
	{
		try
		{
			DataTable dtEmpDetails = new DataTable();
			dtEmpDetails = spm.GetEmployeeData(txtEmpCode.Text);
			if (dtEmpDetails.Rows.Count > 0)
			{

				hflempName.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
				hflEmpDesignation.Value = Convert.ToString(dtEmpDetails.Rows[0]["DesginationName"]);
				hflEmpDepartment.Value = Convert.ToString(dtEmpDetails.Rows[0]["Department_Name"]);
				hflGrade.Value = Convert.ToString(dtEmpDetails.Rows[0]["Grade"]).Trim();
				hflEmailAddress.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
			}
			//  getApproverdata();  
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
			Response.End();
			throw;
		}
	}

	public void GetEmployeeDetails_loginemployee()
	{
		try
		{
			DataTable dtEmpDetails = new DataTable();
			dtEmpDetails = spm.GetEmployeeData(hdnempcode.Value);
			if (dtEmpDetails.Rows.Count > 0)
			{

				hdnloginemployee_name.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);

			}
			//  getApproverdata();  
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
			Response.End();
			throw;
		}
	}
	public void AssigningSessions()
	{

		//Session["Fromdate"] = txtFromdate_sub.Text;
		Session["ReqEmpCode"] = txtEmpCode.Text;
		Session["Grade"] = hflGrade.Value;

		Session["TrDays"] = hdnTrdays.Value;


	}

	private void InsertMobileRem_DatatoTempTables_trvl()
	{
		try
		{

			SqlParameter[] spars = new SqlParameter[3];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "sp_MobileRem_insert_mainData_toTempTabls";

			spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
			spars[1].Value = Convert.ToDecimal(hdnRemid.Value);

			spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
			spars[2].Value = txtEmpCode.Text;

			DataTable dtMaxTripID = spm.InsertorUpdateData(spars, "[SP_GETALLreembursement_DETAILS]");

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}


	}
	public void getApproverdata()
	{

		DataTable dtApproverEmailIds = new DataTable();
		dtApproverEmailIds = spm.GeTfuelApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToDecimal(hdnRemid.Value),"",0);
		//IsEnabledFalse (true);
		dspaymentVoucher_Apprs.Tables.Add(dtApproverEmailIds);
		lstApprover.Items.Clear();
		if (dtApproverEmailIds.Rows.Count > 0)
		{
			foreach (DataRow row in dtApproverEmailIds.Rows)
			{
				if (row[1].ToString() == "00002726")
				{
					CEOInList = "Y";
				}
			}
			hdnApprEmailaddress.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["Emp_Emailaddress"]);
			hdnApprId.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["APPR_ID"]);

			//lstApprover.DataSource = dtApproverEmailIds;
			//lstApprover.DataTextField = "names";
			//lstApprover.DataValueField = "APPR_ID";
			//lstApprover.DataBind();

			//DgvApprover.DataSource = null;
			//DgvApprover.DataBind();

			//if (dtApproverEmailIds.Rows.Count > 0)
			//{
			//    DgvApprover.DataSource = dtApproverEmailIds;
			//    DgvApprover.DataBind();
			//}

			hflapprcode.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["approver_emp_code"]);
		}
		else
		{
			lblmessage.Text = "Since you are not assigned under any approver you can not apply for any type of Travel request, please contact HR";

		}
	}
	public void getMobRemlsDetails_usingRemid()
	{
		try
		{
			DataTable dtTrDetails = new DataTable();
			SqlParameter[] spars = new SqlParameter[3];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "sp_getMainMobileRequest_forApproval";

			spars[1] = new SqlParameter("@rem_id", SqlDbType.Int);
			spars[1].Value = hdnRemid.Value;

			spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
			spars[2].Value = txtEmpCode.Text;

			dtTrDetails = spm.getMobileRemDataList(spars, "[SP_GETALLreembursement_DETAILS]");
			if (dtTrDetails.Rows.Count > 0)
			{
				txtEmpCode.Text = Convert.ToString(dtTrDetails.Rows[0]["Emp_Code"]);
				txtEmpName.Text = Convert.ToString(dtTrDetails.Rows[0]["Emp_Name"]);
				//txtFromdate_sub.Text = Convert.ToString(dtTrDetails.Rows[0]["created_on"]);
				//txtAmount.Text = Convert.ToString(dtTrDetails.Rows[0]["TotalAmount_Claimed"]);
				//  Txt_COSRecommended.Text = Convert.ToString(dtTrDetails.Rows[0]["cos_recommendation"]);
				//Txt_HOD_Recm_Amt.Text = Convert.ToString(dtTrDetails.Rows[0]["HOD_recm_Amt"]);
				//Txt_CFO_Recm_Amt.Text = Convert.ToString(dtTrDetails.Rows[0]["CFO_recm_Amt"]);
				hdnReqEmailaddress.Value = Convert.ToString(dtTrDetails.Rows[0]["Emp_Emailaddress"]);
				hdnTravelConditionid.Value = Convert.ToString(dtTrDetails.Rows[0]["Rem_Conditionid"]);
				hflGrade.Value = Convert.ToString(dtTrDetails.Rows[0]["grade"]);
				//Txt_ProjectName.Text = Convert.ToString(dtTrDetails.Rows[0]["Project_Name"]);
				Txt_DeptName.Text = Convert.ToString(dtTrDetails.Rows[0]["Dept_Name"]);
				//txtFromdate_N.Text = Convert.ToString(dtTrDetails.Rows[0]["From_date"]);
				//txtTodate_N.Text = Convert.ToString(dtTrDetails.Rows[0]["To_date"]);
				//Txt_BillNo.Text = Convert.ToString(dtTrDetails.Rows[0]["Billno"]);
				//txtBilltype.Text = Convert.ToString(dtTrDetails.Rows[0]["BillType"]).Trim();
				// txtReason.Text = Convert.ToString(dtTrDetails.Rows[0]["Reason"]).Trim();
				lblheading.Text = "Mobile bill Voucher - " + Convert.ToString(dtTrDetails.Rows[0]["Vouno"]);
				hdnvouno.Value = Convert.ToString(dtTrDetails.Rows[0]["Vouno"]);
				//if (hdnTravelConditionid.Value == "3")
				//    chk_exception.Checked = true;
				//else
				//  chk_exception.Checked = false;
			}

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());

		}
	}
	public void getApproverdata_old()
	{

		DataTable dtApproverEmailIds = new DataTable();
		dtApproverEmailIds = spm.GeTfuelApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), 0,"",0);
		//IsEnabledFalse (true);
		lstApprover.Items.Clear();
		if (dtApproverEmailIds.Rows.Count > 0)
		{
			hdnApprEmailaddress.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["Emp_Emailaddress"]);
			hdnApprId.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["APPR_ID"]);

			//lstApprover.DataSource = null;
			//lstApprover.DataBind();

			lstApprover.DataSource = dtApproverEmailIds;
			lstApprover.DataTextField = "Emp_Name";
			lstApprover.DataValueField = "APPR_ID";
			lstApprover.DataBind();

			hflapprcode.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["A_EMP_CODE"]);
		}
		else
		{
			lblmessage.Text = "Since you are not assigned under any approver you can not apply for any type of Travel request, please contact HR";

		}
	}

	protected void GetCuurentApprID()
	{

		string Actions = "";
		DataTable dtCApprID = new DataTable();
		dtCApprID = spm.GetCurrentApprIDMobile(Convert.ToInt32(hdnRemid.Value), hdnempcode.Value);
		hdnCurrentApprID.Value = Convert.ToString(dtCApprID.Rows[0]["APPR_ID"]);
		Actions = Convert.ToString(dtCApprID.Rows[0]["Action"]);

		if (Convert.ToString(hdnCurrentApprID.Value).Trim() == "")
		{
			lblmessage.Text = "Acton on this REquest not yet taken by other approvals";
			return;
		}
		else if (Convert.ToString(Actions).Trim() != "Pending")
		{
			lblmessage.Text = "You already actioned for this request";
			return;
		}
	}

	private void getApproverlist()
	{
		DataTable dtapprover = new DataTable();
		dtapprover = spm.GetMobileApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
		lstApprover.Items.Clear();
		if (dtapprover.Rows.Count > 0)
		{
			lstApprover.DataSource = dtapprover;
			lstApprover.DataTextField = "names";
			lstApprover.DataValueField = "names";
			lstApprover.DataBind();

		}
		else
		{
			lblmessage.Text = "There is no request for approver.";
		}
	}

	public void getnextAppIntermediate()
	{
		//Check if Cureent login is Final Approver
		if (Convert.ToString(hdnTravelConditionid.Value) == "1")
		{

		}
		DataTable dsapproverNxt = new DataTable();
		dsapproverNxt = spm.GetMobilelNextApproverDetails(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
		if (dsapproverNxt.Rows.Count > 0)
		{
			hdnNextApprId.Value = Convert.ToString(dsapproverNxt.Rows[0]["APPR_ID"]);
			hdnNextApprCode.Value = Convert.ToString(dsapproverNxt.Rows[0]["A_EMP_CODE"]);
			hdnNextApprName.Value = Convert.ToString(dsapproverNxt.Rows[0]["Emp_Name"]);
			hdnNextApprEmail.Value = Convert.ToString(dsapproverNxt.Rows[0]["Emp_Emailaddress"]);

			//DataTable dtintermediateemail = new DataTable();
			//dtintermediateemail = spm.TravelNextIntermediateName(Convert.ToInt32(hdnCurrentApprID.Value), txtEmpCode.Text);
			//if (dtintermediateemail.Rows.Count > 0)
			//{
			//    hdnIntermediateEmail.Value = (string)dtintermediateemail.Rows[0]["emp_emailaddress"];
			//}
		}
		else
		{
			hdnstaus.Value = "Final Approver";

			//For  Previous approver   
			getPreviousApprovesEmailList();

			//hdnIntermediateEmail.Value = "";
			//DataTable dtPreInt = new DataTable();
			//dtPreInt = spm.FuelPreviousIntermidaterDetails(txtEmpCode.Text, Convert.ToInt32(hdnCurrentApprID.Value));
			//if (dtPreInt.Rows.Count > 0)
			//{

			//    for (int i = 0; i < dtPreInt.Rows.Count; i++)
			//    {
			//        if (Convert.ToString(hdnIntermediateEmail.Value).Trim() == "")
			//        {
			//            hdnIntermediateEmail.Value = Convert.ToString(dtPreInt.Rows[i]["Emp_Emailaddress"]).Trim();
			//        }
			//        else
			//        {
			//            hdnIntermediateEmail.Value += ";" + Convert.ToString(dtPreInt.Rows[i]["Emp_Emailaddress"]).Trim();
			//        }
			//    }
			//}
		}

	}

	private void get_HOD_ACC_CFO_details_ForNextApprover(string strstype)
	{
		try
		{
			DataSet dsTrDetails = new DataSet();
			SqlParameter[] spars = new SqlParameter[5];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			if (Convert.ToString(strstype) == "HOD" || Convert.ToString(strstype) == "RCFO" || Convert.ToString(strstype) == "RACC")
				//if (Convert.ToString(strstype) == "HOD" || Convert.ToString(strstype) == "RCFO" || Convert.ToString(strstype) == "RCOS" || Convert.ToString(strstype) == "RACC")
				spars[0].Value = "get_next_Approver_dtls_MobClaim";
			else
				spars[0].Value = "get_ACC_HOD_isApproved_claim";

			spars[1] = new SqlParameter("@trappr", SqlDbType.VarChar);
			spars[1].Value = strstype;

			spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
			spars[2].Value = txtEmpCode.Text;

			spars[3] = new SqlParameter("@rem_id", SqlDbType.VarChar);
			spars[3].Value = hdnRemid.Value;

			spars[4] = new SqlParameter("@conditiontypeid", SqlDbType.Int);
			spars[4].Value = hdnTravelConditionid.Value;


			dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

			if (dsTrDetails.Tables[0].Rows.Count > 0)
			{
				hdnstaus.Value = "";
				hdnApprovalACCHOD_mail.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();
				hdnApprovalACCHOD_Code.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_code"]).Trim();
				hdnApprovalACCHOD_ID.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_id"]).Trim();
				hdnApprovalACCHOD_Name.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_remarks"]).Trim();

			}
			if (dsTrDetails.Tables[1].Rows.Count > 0)
			{
				if (dsTrDetails.Tables[0].Rows.Count == 0)
				{
					hdnApprovalACCHOD_mail.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["approver_emp_mail"]).Trim();
					hdnApprovalACCHOD_Code.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["approver_emp_code"]).Trim();
					hdnApprovalACCHOD_ID.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["app_id"]).Trim();
					hdnApprovalACCHOD_Name.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["app_remarks"]).Trim();
					hdnstaus.Value = "";
				}
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}
	private void checkisCOSor_ACC_ClaimApproved()
	{


		try
		{
			DataSet dsTrDetails = new DataSet();
			SqlParameter[] spars = new SqlParameter[2];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			// spars[0].Value = "check_COS_ACC_HOD_isApproved_claim";
			spars[0].Value = "check_COS_ACC_HOD_isApproved_claim_Mobile";


			spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
			spars[1].Value = hdnRemid.Value;
			dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

			if (dsTrDetails.Tables[0].Rows.Count > 0)
			{
				for (Int32 irow = 0; irow < dsTrDetails.Tables[0].Rows.Count; irow++)
				{
					if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Action"]).Trim() == "Pending" && (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_type"]).Trim() == "RCOS" || Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_type"]).Trim() == "RCFO"))
					{
						hdnCurrentApprID.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Appr_id"]).Trim();
						get_HOD_ACC_CFO_details_ForNextApprover("RACC");

					}

				}
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}
	protected string GetApprove_RejectList()
	{
		StringBuilder sbapp = new StringBuilder();
		sbapp.Length = 0;
		sbapp.Capacity = 0;
		DataTable dtAppRej = new DataTable();
		//dtAppRej = spm.GetMobileApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
		dtAppRej = spm.GeTfuelApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToDecimal(hdnRemid.Value),"",0);
		if (dtAppRej.Rows.Count > 0)
		{
			sbapp.Append("<table>");
			for (int i = 0; i < dtAppRej.Rows.Count; i++)
			{
				sbapp.Append("<tr>");
				sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
				sbapp.Append("</tr>");
			}
			sbapp.Append("</table>");
		}




		return Convert.ToString(sbapp);
	}
	private void getPreviousApprovesEmailList()
	{
		DataTable dtPreApp = new DataTable();
		dtPreApp = spm.MobilePreviousApproverDetails(Convert.ToInt32(hdnRemid.Value));
		if (dtPreApp.Rows.Count > 0)
		{

			for (int i = 0; i < dtPreApp.Rows.Count; i++)
			{
				if (Convert.ToString(hflEmailAddress.Value).Trim() == "")
				{
					hflEmailAddress.Value = Convert.ToString(dtPreApp.Rows[i]["Emp_Emailaddress"]).Trim();
				}
				else
				{
					hflEmailAddress.Value += ";" + Convert.ToString(dtPreApp.Rows[i]["Emp_Emailaddress"]).Trim();
				}
			}
		}
	}
	protected string getRejectionCorrectionmailList()
	{
		string email_ids = "";
		DataTable dtApproverEmailIds = new DataTable();
		dtApproverEmailIds = spm.MobileApproverDetails_Rejection_cancellation(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), "get_MobileApproverDetails_mail_rejection_correction");
		lstApprover.Items.Clear();
		if (dtApproverEmailIds.Rows.Count > 0)
		{
			for (int irow = 0; irow < dtApproverEmailIds.Rows.Count; irow++)
			{
				if (Convert.ToString(email_ids).Trim() == "")
					email_ids = Convert.ToString(dtApproverEmailIds.Rows[irow]["Emp_Emailaddress"]).Trim();
				else
					email_ids = email_ids + ";" + Convert.ToString(dtApproverEmailIds.Rows[irow]["Emp_Emailaddress"]).Trim();
			}
		}

		return email_ids;

	}

	public void getMobileClaimUploadedFiles()
	{

		DataTable dtTrDetails = new DataTable();
		SqlParameter[] spars = new SqlParameter[3];

		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "get_MobileClaim_UploadedFilesList";

		spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
		spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

		spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
		spars[2].Value = Convert.ToDecimal(hdnRemid.Value);

		dtTrDetails = spm.getDataList(spars, "SP_GETALLreembursement_DETAILS");

		//gvuploadedFiles.DataSource = null;
		//gvuploadedFiles.DataBind();
		//if (dtTrDetails.Rows.Count > 0)
		//{
		//    gvuploadedFiles.DataSource = dtTrDetails;
		//    gvuploadedFiles.DataBind();
		//}
	}
	#endregion


	protected void lnkviewfile_Click(object sender, EventArgs e)
	{
		try
		{

			LinkButton btn = (LinkButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			//Int32 ifileid = Convert.ToInt32(gvuploadedFiles.DataKeys[row.RowIndex].Values[0]);
			LinkButton lnkviewfile = (LinkButton)row.FindControl("lnkviewfile");

			String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ClaimsDocumentpath"]).Trim()), lnkviewfile.Text);
			Response.ContentType = ContentType;
			Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
			Response.WriteFile(strfilepath);
			Response.End();
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}

	//Update This Function 20-01-2021

	protected void chk_exception_CheckedChanged(object sender, EventArgs e)
	{
		//if (chk_exception.Checked)
		//    hdnTravelConditionid.Value = "3";
		//else
		hdnTravelConditionid.Value = "2";
		getApproverdata();
	}
	protected void lnkuplodedfile_Click(object sender, EventArgs e)
	{
		try
		{
			String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ClaimsDocumentpath"]).Trim()), lnkuplodedfile.Text);

			Response.ContentType = ContentType;
			Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
			Response.WriteFile(strfilepath);
			Response.End();
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}

	public void BindCategoryDDL(string departmentId)
	{
		try
		{

			ddl_AssginmentEMP.DataSource = null;
			ddl_AssginmentEMP.DataBind();
			var dptId = Convert.ToInt32(departmentId);
			var getDepartment = spm.GetCustEscalationCategoryDepartment(dptId);
			if (getDepartment.Rows.Count > 0)
			{
				ddlCategory.DataSource = getDepartment;
				//ddlDepartment.DataBind();
				ddlCategory.DataTextField = "CategoryTitle";
				ddlCategory.DataValueField = "Id";
				ddlCategory.DataBind();

				ddlCategory.Items.Insert(0, new ListItem("Select Category", "0")); //updated code
			}
			else
			{
				ddlCategory.DataSource = null;
				ddlCategory.DataBind();
			}


		}
		catch (Exception)
		{

			throw;
		}
	}




	protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			DropDownList list = (DropDownList)sender;
			string value = (string)list.SelectedValue;
			if (value == "0")
			{
				ddl_AssginmentEMP.DataSource = null;
			}
			else
			{
				BindEmpDDL(value);
			}

		}
		catch (Exception)
		{

			throw;
		}
	}

	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		try
		{
			var filename = "";
			string strfromDate = "";
			var strfileName = "";
			string[] Responses, Delivary;
			string confirmValue = hdnYesNo.Value.ToString();
			string DeliveryDate = string.Empty, ResponseDate = string.Empty, TaskAction = "No";
			DateTime STDate = new DateTime();
			if (confirmValue != "Yes")
			{
				return;
			}
			lblActionMSG.Text = "";
			ModalPopupExtenderIRSheet.Show();
			if (TxtActionDate.Text.Trim() == "")
			{
				lblActionMSG.Text = "Please enter Action Date";
				return;
			}

			if (txtActionTitle.Text.Trim() == "")
			{
				lblActionMSG.Text = "Please enter Action Title";
				return;
			}
			if (Convert.ToString(txtRemarks.Text).Trim() == "")
			{
				lblActionMSG.Text = "Please enter Action Remark.";
				return;
			}
			if (TxtActionDate.Text != "")
			{
				Responses = Convert.ToString(TxtActionDate.Text).Trim().Split('/');
				ResponseDate = Convert.ToString(Responses[2]) + "-" + Convert.ToString(Responses[1]) + "-" + Convert.ToString(Responses[0]);
				STDate = DateTime.ParseExact(ResponseDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
				ResponseDate = STDate.ToString();
			}

			Delivary = Convert.ToString(hdnDelivaryDate.Value).Trim().Split('-');
			string result = Convert.ToString(Delivary[2]) + "-" + Convert.ToString(Delivary[1]) + "-" + Convert.ToString(Delivary[0]);
			DateTime STDelivaryDate = DateTime.ParseExact(result, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
			//var STDate1 = STDate;
			if (STDate > STDelivaryDate)
			{
				lblActionMSG.Text = "Please enter Action Date less than Delivary Date.";
				return;
			}
			if (ckckTask.Checked == true)
			{
				TaskAction = "Yes";
			}
			else
			{
				TaskAction = "No";
			}
			if (Convert.ToString(filename).Trim() != "")
			{
				////var getdate = Convert.ToDateTime(Txt_DateCreate.Text);
				//DateTime loadedDate = DateTime.Now;
				//strfromDate = loadedDate.ToString("ddMMyyyyHHmmss");
				//filename = uploadfile.FileName;
				//strfileName = "";
				//strfileName = "CF_" + txtEmpCode.Text + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
				//filename = strfileName;
				//uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["CustomerFristPathpath"]).Trim()), strfileName));
			}
			//var empCode = Convert.ToString(txtEmpCode.Text).Trim();
			var Cust_ActionID = Convert.ToString(hdnCust_ActionID.Value).Trim() != "" ? Convert.ToInt32(hdnCust_ActionID.Value) : 0;
			var Serviceid = Convert.ToInt32(Convert.ToString(hdnRemid.Value).Trim());
			var ActionBy = Convert.ToString(Session["Empcode"]).Trim();

			spm.ActionCustEscalationDetails(ActionBy, Cust_ActionID, Serviceid, "InseretAction", txtActionTitle.Text.Trim(), txtRemarks.Text.Trim(), ResponseDate, TaskAction,"");
			ClearActions();
			ActionDetails();
			ModalPopupExtenderIRSheet.Hide();
			//Response.Redirect("~/procs/CustEscalation.aspx");
		}
		catch (Exception ex)
		{
			lblActionMSG.Text = ex.Message.ToString();
			return;
		}

	}
	public void ClearActions()
	{
		TxtActionDate.Text = "";
		txtActionTitle.Text = "";
		txtRemarks.Text = "";
		ckckTask.Checked = false;
		mobile_btnSave.Text = "Submit";
		hdnCust_ActionID.Value = "0";
		TaskDate1.Visible = false;
		txtTaskCompletedDate.Visible = false;
		//mobile_btnSave.Visible = true;
		//mobile_btnSave.Style.Add("display", "inline-block");
	}

	public void ActionDetails()
	{
		try
		{
			var Serviceid = Convert.ToInt32(Convert.ToString(hdnRemid.Value).Trim());
			var getAction = spm.GetCustEscalation_ActionDeatils("SelectAction", Serviceid);

			if (getAction.Rows.Count > 0)
			{
				GridActionDeatils.DataSource = getAction;
				GridActionDeatils.DataBind();
				var selectedTable = getAction.AsEnumerable().Where(r => r.Field<string>("TaskCompleted") == "No").FirstOrDefault();
				if (selectedTable == null)
				{
					//service_btnClose.Visible = true;
				}
				else
				{ service_btnClose.Visible = false; }


				liAction.Visible = true;
				liAction.Visible = true;
				empShow11.Visible = true;
				liAction2.Visible = true;
				liAction3.Visible = true;
			}
			else
			{
				empShow11.Visible = false;
				liAction.Visible = false;
				liAction.Visible = false;
				liAction2.Visible = false;
				liAction3.Visible = false;
			}
		}
		catch (Exception)
		{

			throw;
		}
	}
	protected void lnkActionEdit_Click(object sender, ImageClickEventArgs e)
	{
		ImageButton btn = (ImageButton)sender;
		GridViewRow row = (GridViewRow)btn.NamingContainer;
		int Serviceid = Convert.ToInt32(GridActionDeatils.DataKeys[row.RowIndex].Values[0]);
		DataTable empdetail = spm.GetCustEscalation_ActionDeatils("SelectEditAction", Serviceid);
		TxtActionDate.Text = Convert.ToString(empdetail.Rows[0]["ActionDate"]).Replace('-', '/');
		txtActionTitle.Text = Convert.ToString(empdetail.Rows[0]["ActionTitle"]);
		txtRemarks.Text = Convert.ToString(empdetail.Rows[0]["Remarks"]);
		hdnCust_ActionID.Value = Convert.ToString(empdetail.Rows[0]["Cust_ActionID"]);
		var CreateBy = Convert.ToString(empdetail.Rows[0]["CreatedBy"]);
		string result = Convert.ToString(empdetail.Rows[0]["TaskCompleted"]).Trim();
		if (result == "Yes")
		{
			ckckTask.Checked = true;
			txtTaskCompletedDate.Text = Convert.ToString(empdetail.Rows[0]["TaskCompletedDate"]).Trim().Replace("-", "/");
			TaskDate1.Visible = true;
			txtTaskCompletedDate.Visible = true;

		}
		else
		{
			txtTaskCompletedDate.Text = "";
			ckckTask.Checked = false;
			TaskDate1.Visible = false;
			txtTaskCompletedDate.Visible = false;
		}

		//mobile_btnSave.Style.Add("display", "none");
		lblActions.Text = "Edit Action Details";
		mobile_btnSave.Text = "Update";
		//if (CreateBy.ToString() != Convert.ToString(Session["Empcode"]).Trim())
		{
			mobile_btnSave.Visible = false;
		}
		//else
		//{
		//	mobile_btnSave.Visible = false;
		//}
		//TxtActionDate.Enabled = false;
		//txtActionTitle.Enabled = false;
		//txtRemarks.Enabled = false;
		//ckckTask.Enabled = false;
		this.ModalPopupExtenderIRSheet.Show();
	}

	protected void trvldeatils_delete_btn_Click(object sender, EventArgs e)
	{
		ClearActions();

		//TxtActionDate.Enabled = true;
		//txtActionTitle.Enabled = true;
		//txtRemarks.Enabled = true;
		//ckckTask.Enabled = true;
	}

	protected void Customer_Reject_Click(object sender, EventArgs e)
	{
		try
		{
			var filename = "";
			string strfromDate = "";
			var strfileName = "";
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}
			if (Convert.ToString(txt_SPOCComment.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter Closure Confirmation Description.";
				return;
			}

			lblmessage.Text = "";
			if (uploadfile.HasFile)
			{
				filename = uploadfile.FileName;
			}

			if (Convert.ToString(filename).Trim() != "")
			{
				//var getdate = Convert.ToDateTime(Txt_DateCreate.Text);
				DateTime loadedDate = DateTime.Now;
				strfromDate = loadedDate.ToString("ddMMyyyyHHmmss");
				filename = uploadfile.FileName;
				strfileName = "";
				strfileName = "CF_" + txtEmpCode.Text + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
				filename = strfileName;
				uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["CustomerFristPathpath"]).Trim()), strfileName));
			}

			//Assgine SPOC User
			var AssigneTo = "";
			DataTable dtCEO = new DataTable();
			var id = Convert.ToInt32(hdnRemid.Value.ToString().Trim());
			dtCEO = spm.GetCustEscalation_EmployeeeTo("CustEscalation_AssignTo", id);
			if (dtCEO.Rows.Count > 0)
			{
				// txtEmpName.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
				AssigneTo = Convert.ToString(dtCEO.Rows[0]["AssignedTo"]);
			}
			var empCode = Convert.ToString(txtEmpCode.Text).Trim();
			var Serviceid = Convert.ToInt32(Convert.ToString(hdnRemid.Value).Trim());
			var ServicesDepartmentId = Convert.ToString(Convert.ToString(hdnDepartmentID.Value).Trim());
			var ServiceDescription = Convert.ToString(txt_SPOCComment.Text).Trim();
			var ActionBy = Convert.ToString(Session["Empcode"]).Trim();
			var Assgineto = AssigneTo;
			var CategoryId = 0;//Convert.ToInt32(Convert.ToString(hdnCategoryId.Value).Trim());
			var getdeptName = Convert.ToString(hdnDepartmentName.Value).Trim(); //Technology & Innovation

			spm.AssgineCustEscalationDetails(empCode, ServiceDescription, "EscalatedUSER", filename, Assgineto, "", ServicesDepartmentId, 12, 12, ActionBy, '\0', Serviceid, CategoryId);



			//Send Email To HOD
			DataTable dtEmpDetails = new DataTable();
			dtEmpDetails = spm.GetEmployeeData(AssigneTo);
			var assgineToName = "";
			string toMailIDs = "";
			if (dtEmpDetails.Rows.Count > 0)
			{
				assgineToName = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);

				toMailIDs = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
			}

			//
			//var currentAssgine = Convert.ToString(hdnCurrentAssgineEMP.Value);
			//dtEmpDetails = spm.GetEmployeeData(currentAssgine);
			//var CurrentassgineToName = "";
			//string ccEmailAddress = "";
			//if (dtEmpDetails.Rows.Count > 0)
			//{
			//	CurrentassgineToName = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);

			//	ccEmailAddress = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
			//}
			//

			var getId = Convert.ToString(hdnRemid.Value);
			string serviceRequestno = Convert.ToString(hdnServiceRequestNo.Value);


			string employeeName = txtEmpName.Text.ToString();
			var employee_Email = Txt_EmpEmail.Text.ToString();

			string createdDate = txtFirstCreatedDate.Text.ToString();
			string department = getdeptName;
			//string ccmailid = "";
			//string ccmailid = ccEmailAddress;
			string redirectURL = Convert.ToString(ConfigurationManager.AppSettings["Customer_Req_App"]).Trim() + "?id=" + getId + "&type=app";
			string requestDescription = ServiceDescription;
			var actionByName = Session["emp_loginName"].ToString();
			var actionActionDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
			var strsubject = "Action update on your Service Request " + serviceRequestno + "";
			int IsCEO = 0;
			var CoordinatorCmd = GetCustEscalation_SPOC(Serviceid, "");
			var ResponseCmd = GetCustEscalation_ResponseCmd(Serviceid, Assgineto);
			var ClosureCmd = GetCustEscalation_ClosureCmd(Serviceid, Assgineto);
			string ccMailIDs = GetCustEscalation_CCMail_Deatils(Serviceid, Assgineto);
			spm.CustEscalation_send_mailto_EscalationReject(IsCEO,"Rejected", actionByName, assgineToName, serviceRequestno, toMailIDs, employeeName, createdDate, txtSelectedDepartment.Text.Trim(), ccMailIDs, redirectURL, requestDescription, ClosureCmd, txtEscalationBy.Text.Trim(), txtEmailID.Text.Trim(), txt_category.Text.Trim(), TXTCustomerSatisfaction.Text.Trim(), TXTSeverity.Text.Trim(), txtRolePerson.Text.Trim(), txtFirstDeacription.Text.Trim(), CoordinatorCmd, ResponseCmd);
			Response.Redirect("~/procs/InboxCustEscalationApp.aspx");
		}
		catch (Exception ex)
		{
			lblmessage.Text = ex.Message.ToString();
			return;
		}

	}
	public string GetCustEscalation_SPOC(int Serviceid, string Assgineto)
	{

		try
		{
			String sbapp = "";
			DataTable dsTrDetails = new DataTable();
			dsTrDetails = spm.GetCustEscalation_CCMail_Deatils("GETESCALATIONSPOC", Serviceid, "");
			if (dsTrDetails.Rows.Count > 0)
			{
				sbapp = Convert.ToString(dsTrDetails.Rows[0]["ServicesDescripation"]).Trim();
			}
			return Convert.ToString(sbapp);
		}
		catch (Exception)
		{

			throw;
		}


	}
	public string GetCustEscalation_ResponseCmd(int Serviceid, string Assgineto)
	{

		try
		{
			String sbapp = "";
			DataTable dsTrDetails = new DataTable();
			dsTrDetails = spm.GetCustEscalation_CCMail_Deatils("GETESCALATIONRESPONSE", Serviceid, Assgineto);
			if (dsTrDetails.Rows.Count > 0)
			{
				sbapp = Convert.ToString(dsTrDetails.Rows[0]["ServicesDescripation"]).Trim();
			}
			return Convert.ToString(sbapp);
		}
		catch (Exception)
		{

			throw;
		}


	}
	public string GetCustEscalation_ClosureCmd(int Serviceid, string Assgineto)
	{

		try
		{
			String sbapp = "";
			DataTable dsTrDetails = new DataTable();
			dsTrDetails = spm.GetCustEscalation_CCMail_Deatils("GETESCALATIONRCLOSURE", Serviceid, Assgineto);
			if (dsTrDetails.Rows.Count > 0)
			{
				sbapp = Convert.ToString(dsTrDetails.Rows[0]["ServicesDescripation"]).Trim();
			}
			return Convert.ToString(sbapp);
		}
		catch (Exception)
		{

			throw;
		}


	}
	protected void Service_Approval_Click(object sender, EventArgs e)
	{
		string strclaim_month = "";
		string[] strdate;
		string strfromDate = "";
		decimal recm_amount = 0;
		lblmessage.Visible = true;
		try
		{
			var filename = "";
			var strfileName = "";
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}

			if (Convert.ToString(txt_SPOCComment.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter Customer First Remark.";
				return;
			}

			lblmessage.Text = "";
			if (uploadfile.HasFile)
			{
				filename = uploadfile.FileName;
			}

			if (Convert.ToString(filename).Trim() != "")
			{
				//var getdate = Convert.ToDateTime(Txt_DateCreate.Text);
				DateTime loadedDate = DateTime.Now;
				strfromDate = loadedDate.ToString("ddMMyyyyHHmmss");
				filename = uploadfile.FileName;
				strfileName = "";
				strfileName = "CF_" + txtEmpCode.Text + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
				filename = strfileName;
				uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["CustomerFristPathpath"]).Trim()), strfileName));
			}
			var AssigneTo = "";
			DataTable dtCEO = new DataTable();
			var id = Convert.ToInt32(hdnRemid.Value.ToString().Trim());
			dtCEO = spm.GetCustEscalation_EmployeeeTo("CustEscalation_AssignTo", id);
			if (dtCEO.Rows.Count > 0)
			{
				// txtEmpName.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
				AssigneTo = Convert.ToString(dtCEO.Rows[0]["AssignedTo"]);
			}
			var empCode = Convert.ToString(txtEmpCode.Text).Trim();
			var Serviceid = Convert.ToInt32(Convert.ToString(hdnRemid.Value).Trim());
			var ServicesDepartmentId = Convert.ToString(Convert.ToString(ddlDepartment.SelectedValue).Trim());
			var ServiceDescription = Convert.ToString(txt_SPOCComment.Text).Trim();
			var ActionBy = Convert.ToString(Session["Empcode"]).Trim();
			var CategoryId = 0;//Convert.ToInt32(Convert.ToString(hdnCategoryId.Value).Trim());          
			if (ServicesDepartmentId == "0")
			{
				ServicesDepartmentId = Convert.ToString(Convert.ToString(hdnDepartmentID.Value).Trim());
			}
			spm.AssgineCustEscalationDetails(empCode, ServiceDescription, "UpdateAction", filename, ActionBy, "", ServicesDepartmentId, 13, 13, ActionBy, '\0', Serviceid, CategoryId);



			//Send Email To Employee
			var getId = Convert.ToString(hdnRemid.Value);
			string serviceRequestno = Convert.ToString(hdnServiceRequestNo.Value);
			//string toMailIDs = "";

			string employeeName = txtEmpName.Text.ToString();
			var employee_Email = Txt_EmpEmail.Text.ToString();

			string createdDate = txtFirstCreatedDate.Text.ToString();
			string department = hdnDepartmentName.Value.ToString().Trim();
			//string ccmailid = "";
			string redirectURL = Convert.ToString(ConfigurationManager.AppSettings["Customer_Req_App"]).Trim() + "?id=" + getId + "&type=app";
			string requestDescription = ServiceDescription;
			//DataTable dtEmpDetails = new DataTable();
			//dtEmpDetails = spm.GetEmployeeData(Assgineto);
			var actionByName = Session["emp_loginName"].ToString();
			var actionName = "Closed";
			redirectURL = Convert.ToString(ConfigurationManager.AppSettings["Customer_Req_App"]).Trim() + "?id=" + getId + "&type=emp";

			//var countForEx = Convert.ToInt32(hdnIsExceletd.Value);
			//var IsExceletd = 0;
			//if (countForEx == 1)
			//{
			//	IsExceletd = 1;
			//}
			//else if (countForEx == 2)
			//{
			//	IsExceletd = 2;
			//}
			//else if (IsExceletd == 0)
			//{
			//	IsExceletd = 0;
			//}
			//else
			//{
			//	IsExceletd = 10;
			//}

			//Send Email To HOD
			DataTable dtEmpDetails = new DataTable();
			dtEmpDetails = spm.GetEmployeeData(AssigneTo);
			var assgineToName = "";
			string toMailIDs = "";
			if (dtEmpDetails.Rows.Count > 0)
			{
				assgineToName = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);

				toMailIDs = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
			}
			var CoordinatorCmd = GetCustEscalation_SPOC(Serviceid, "");
			var ResponseCmd = GetCustEscalation_ResponseCmd(Serviceid, AssigneTo);
			var ClosureCmd = GetCustEscalation_ClosureCmd(Serviceid, AssigneTo);
			string ccMailIDs = GetCustEscalation_CCMail_Deatils(Serviceid, AssigneTo);
			//if (getCEOEmpId != ActionBy)
			{
				spm.CustEscalation_send_mailto_EscalationClosed(0, actionName, actionByName.Trim(), serviceRequestno, toMailIDs, employeeName, createdDate, txtSelectedDepartment.Text.Trim(), ccMailIDs, redirectURL, requestDescription, ClosureCmd, txtEscalationBy.Text.Trim(), txtEmailID.Text.Trim(), txt_category.Text.Trim(), TXTCustomerSatisfaction.Text.Trim(), TXTSeverity.Text.Trim(), txtRolePerson.Text.Trim(), txtFirstDeacription.Text.Trim(), CoordinatorCmd, ResponseCmd);
			}
			//Send Email To Employee
			Response.Redirect("~/procs/CustEscalation.aspx");
		}
		catch (Exception ex)
		{
			lblmessage.Text = ex.Message.ToString();
			return;
		}

	}
}
