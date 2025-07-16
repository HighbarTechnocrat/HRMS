using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CustsServiceReqApp_Cust : System.Web.UI.Page
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
            if (Request.QueryString.Count > 0)
            {
                hdnRemid_Type.Value = Convert.ToString(Request.QueryString[1]).Trim();
            }
            if (Convert.ToString(hdnRemid_Type.Value).Trim() == "emp")
            {
                if (Convert.ToString(Session["CustCode"]).Trim() == "" || Session["CustCode"] == null)
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "Cust_SessionEnd.aspx");
                }
            }


            //mobile_btnPrintPV.Visible = false;

            if (Page.User.Identity.IsAuthenticated == false)
            {
                if (Convert.ToString(hdnRemid_Type.Value).Trim() == "emp")
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "cust_login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Custs_MyServiceReq");
                }

            }
            else
            {

                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    txt_SPOCComment.Attributes.Add("maxlength", "500");
                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["CustomerServiceRequestPathpath"]).Trim());
                    TXT_AssginmentDate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                    editform.Visible = true;
                    divbtn.Visible = false;
                    divmsg.Visible = false;
                    hdnempcode.Value = Convert.ToString(Session["CustCode"]);

                    if (Request.QueryString.Count > 0)
                    {
                        hdnRemid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdnRemid_Type.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        var getType = Convert.ToString(Request.QueryString[1]).Trim();

                        if (getType == "emp")
                        {
                            empShow.Visible = false;
                            empShow2.Visible = false;
                            empShow12.Visible = false;

                            service_btnAssgine.Visible = false;
                            service_btnClose.Visible = false;
                            service_btnSendSPOC.Visible = false;

                            empShow1.Visible = false;
                            empShow.Visible = false;
                            empShow2.Visible = false;
                            empShow3.Visible = false;
                            empShow4.Visible = false;
                            empShow5.Visible = false;

                            service_btnEscelateToHOD.Visible = false;
                            service_btnEscelateToCEO.Visible = false;
                            service_btnClearText.Visible = false;
                            backToEmployee.Visible = true;
                        }

                        #region Commented code
                        /*else if (getType == "empEscalated")
                        {
                            empShow.Visible = false;
                            empShow2.Visible = false;
                            empShow12.Visible = false;

                            service_btnAssgine.Visible = false;
                            service_btnClose.Visible = false;
                            service_btnSendSPOC.Visible = false;

                            empShow1.Visible = false;
                            empShow.Visible = false;
                            empShow2.Visible = false;
                            empShow3.Visible = false;
                            empShow4.Visible = false;
                            empShow5.Visible = false;

                            service_btnEscelateToHOD.Visible = false;
                            service_btnEscelateToCEO.Visible = false;
                            service_btnClearText.Visible = false;
                            backToEmployee.Visible = false;
                            backToEscalatedService.Visible = true;
                        }
                        else
                        {
                            var empCode = Convert.ToString(Session["CustCode"]).Trim();
                            //var getSPOCStatus = spm.GETSPOCEXISTS(empCode);
                            var getSPOCStatus = Check_IsLoginEmployee_AsSPOC(empCode);
                            if (getSPOCStatus.Rows.Count > 0)
                            {
                                var Message = Convert.ToString(getSPOCStatus.Rows[0]["Message"]);
                                if (Message == "YES")
                                {
                                    empShow.Visible = true;
                                    empShow2.Visible = true;
                                    empShow12.Visible = true;
                                    ddlCategory.Enabled = true;
                                    divsh.Visible = false;
                                    ddlDepartment.Enabled = true;
                                    service_btnAssgine.Visible = true;
                                    service_btnClose.Visible = true;

                                    service_btnEscelateToHOD.Visible = false;
                                    service_btnEscelateToCEO.Visible = false;
                                    service_btnSendSPOC.Visible = false;
                                }
                                else
                                {
                                    lblAssgineTitle.InnerText = "Action Details";
                                    lblActionAssginDate.InnerText = "Action Date";
                                    lblActionAssginComment.InnerText = "Action Description";
                                    empShow.Visible = false;
                                    empShow2.Visible = false;
                                    empShow12.Visible = false;

                                    service_btnEscelateToHOD.Visible = false;
                                    service_btnEscelateToCEO.Visible = false;
                                    service_btnAssgine.Visible = false;

                                    service_btnSendSPOC.Visible = true;
                                    service_btnClose.Visible = true;
                                }
                            }
                            else
                            {
                                empShow.Visible = false;
                                empShow2.Visible = false;
                                empShow12.Visible = false;

                                service_btnEscelateToHOD.Visible = false;
                                service_btnEscelateToCEO.Visible = false;
                                service_btnAssgine.Visible = false;

                                service_btnSendSPOC.Visible = true;
                                service_btnClose.Visible = true;
                            }
                            backToSPOC.Visible = true;
                        }*/
                        #endregion
                    }

                    if (Convert.ToString(hdnRemid.Value).Trim() != "")
                    {
                        if (Request.QueryString.Count > 0)
                        {
                            GetServiceRequestDetails(hdnRemid.Value, hdnempcode.Value);

                            var getType = Convert.ToString(Request.QueryString[1]).Trim();
                            if (getType == "arr")
                            {
                                empShow.Visible = false;
                                empShow2.Visible = false;
                                empShow12.Visible = false;

                                service_btnAssgine.Visible = false;
                                service_btnClose.Visible = false;
                                service_btnSendSPOC.Visible = false;

                                empShow1.Visible = false;
                                empShow12.Visible = false;
                                empShow.Visible = false;
                                empShow2.Visible = false;
                                empShow3.Visible = false;
                                empShow4.Visible = false;
                                empShow5.Visible = false;
                                empShow7.Visible = false;

                                service_btnEscelateToHOD.Visible = false;
                                service_btnEscelateToCEO.Visible = false;
                                service_btnClearText.Visible = false;
                                backToEmployee.Visible = false;
                                backToSPOC.Visible = false;
                                backToArr.Visible = true;
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
        #region old Code
        /*
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

            var AssginetoDepartment = Convert.ToInt32(ddlDepartment.SelectedValue.ToString().Trim());
            if (AssginetoDepartment == 0)
            {
                lblmessage.Text = "Please select Assigned To Department.";
                return;
            }

            var catId = Convert.ToInt32(ddlCategory.SelectedValue.ToString().Trim());
            if (catId == 0)
            {
                lblmessage.Text = "Please select Assigned To Category.";
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
                lblmessage.Text = "Please enter Service Comment.";
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
                strfileName = "SR_" + txtEmpCode.Text + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
                filename = strfileName;
                uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ServiceRequestPathpath"]).Trim()), strfileName));
            }
            var empCode = Convert.ToString(txtEmpCode.Text).Trim();
            var Serviceid = Convert.ToInt32(Convert.ToString(hdnRemid.Value).Trim());
            var ServicesDepartmentId = Convert.ToInt32(Convert.ToString(ddlDepartment.SelectedValue).Trim());
            var ServiceDescription = Convert.ToString(txt_SPOCComment.Text).Trim();
            var ActionBy = Convert.ToString(Session["Empcode"]).Trim();
            var Assgineto = ddl_AssginmentEMP.SelectedValue.ToString().Trim();
            // var CategoryId= Convert.ToInt32(Convert.ToString(hdnCategoryId.Value).Trim());
            var CategoryId = Convert.ToInt32(Convert.ToString(ddlCategory.SelectedValue).Trim());
            spm.AssgineServiceRequestDetails(empCode, ServiceDescription, "UpdateAction", filename, Assgineto, '\0', ServicesDepartmentId, 2, 2, ActionBy, '\0', Serviceid, CategoryId);

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
            string redirectURL = Convert.ToString(ConfigurationManager.AppSettings["Service_Req_App"]).Trim() + "?id=" + getId + "&type=app";
            string requestDescription = ServiceDescription;
            DataTable dtEmpDetails = new DataTable();
            dtEmpDetails = spm.GetEmployeeData(Assgineto);
            var assgineToName = "";
            if (dtEmpDetails.Rows.Count > 0)
            {
                assgineToName = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
                // = Convert.ToString(dtEmpDetails.Rows[0]["DesginationName"]);
                // = Convert.ToString(dtEmpDetails.Rows[0]["Department_Name"]);
                // = Convert.ToString(dtEmpDetails.Rows[0]["Grade"]).Trim();
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

            spm.ServiceRequest_send_mailto_AssignedAgent(IsExceletd, serviceRequestno, toMailIDs, employeeName, createdDate, department, "", redirectURL, requestDescription);
            var actionByName = Session["emp_loginName"].ToString();
            var actionName = "Assigned";
            redirectURL = Convert.ToString(ConfigurationManager.AppSettings["Service_Req_App"]).Trim() + "?id=" + getId + "&type=emp";

            spm.ServiceRequest_send_mailto_AssignToEmployee(IsExceletd, serviceRequestno, employee_Email, actionName, actionByName, assgineToName, createdDate, department, "", redirectURL, requestDescription);

            //Send Email To Employee

            Response.Redirect("~/procs/Service.aspx");
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return;
        }
        */
        #endregion
        Updated_SendEscalate_CustomerService();
    }
    protected void service_btnSendSPOC_Click(object sender, EventArgs e)
    {

        #region Send back to SPOC
        /*
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
                lblmessage.Text = "Please enter Service Comment.";
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
                strfileName = "SR_" + txtEmpCode.Text + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
                filename = strfileName;
                uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ServiceRequestPathpath"]).Trim()), strfileName));
            }

            //Assgine SPOC User
            var AssigneTo = "";
            DataTable dtSPOC = new DataTable();
            var id = Convert.ToInt32(hdnDepartmentID.Value.ToString().Trim());
            dtSPOC = spm.GetSPOCData(id);
            if (dtSPOC.Rows.Count > 0)
            {
                // txtEmpName.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
                AssigneTo = Convert.ToString(dtSPOC.Rows[0]["SPOC_ID"]);

            }

            var empCode = Convert.ToString(txtEmpCode.Text).Trim();
            var Serviceid = Convert.ToInt32(Convert.ToString(hdnRemid.Value).Trim());
            var ServicesDepartmentId = Convert.ToInt32(Convert.ToString(hdnDepartmentID.Value.ToString()).Trim());
            var ServiceDescription = Convert.ToString(txt_SPOCComment.Text).Trim();
            var ActionBy = Convert.ToString(Session["Empcode"]).Trim();
            var Assgineto = AssigneTo;
            var CategoryId = Convert.ToInt32(Convert.ToString(hdnCategoryId.Value).Trim());
            spm.AssgineServiceRequestDetails(empCode, ServiceDescription, "UpdateAction", filename, Assgineto, '\0', ServicesDepartmentId, 4, 4, ActionBy, '\0', Serviceid, CategoryId);

            //Send Email To SPOC
            DataTable dtEmpDetails = new DataTable();
            dtEmpDetails = spm.GetEmployeeData(Assgineto);
            var assgineToName = "";
            string toMailIDs = "";
            if (dtEmpDetails.Rows.Count > 0)
            {
                assgineToName = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
                // = Convert.ToString(dtEmpDetails.Rows[0]["DesginationName"]);
                // = Convert.ToString(dtEmpDetails.Rows[0]["Department_Name"]);
                // = Convert.ToString(dtEmpDetails.Rows[0]["Grade"]).Trim();
                toMailIDs = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
            }

            var getId = Convert.ToString(hdnRemid.Value);
            string serviceRequestno = Convert.ToString(hdnServiceRequestNo.Value);


            string employeeName = txtEmpName.Text.ToString();
            var employee_Email = Txt_EmpEmail.Text.ToString();

            string createdDate = txtFirstCreatedDate.Text.ToString();
            string department = hdnDepartmentName.Value.ToString().Trim();
            string ccmailid = "";
            string redirectURL = Convert.ToString(ConfigurationManager.AppSettings["Service_Req_App"]).Trim() + "?id=" + getId + "&type=app";
            string requestDescription = ServiceDescription;
            var actionByName = Session["emp_loginName"].ToString();
            var actionActionDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            var strsubject = "Action update on your Service Request " + serviceRequestno + "";
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

            spm.ServiceRequest_send_mailto_HeadOrCEOToSPOC(IsCEO, serviceRequestno, toMailIDs, employeeName, createdDate, department, "", redirectURL, requestDescription, actionActionDate, actionByName);



            Response.Redirect("~/procs/Service.aspx");
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return;
        }
         */
        #endregion

        #region Close The customer Service Request
        DataSet ds = new DataSet();        

        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "update_Customer_ServiceRequest_Satisfied";

        spars[1] = new SqlParameter("@id", SqlDbType.Decimal);
        spars[1].Value = Convert.ToDecimal(hdnRemid.Value);

        spars[2] = new SqlParameter("@ServicesDescripation", SqlDbType.VarChar);
        if (Convert.ToString(txt_ServiceFeedback.Text).Trim() != "")
            spars[2].Value = Convert.ToString(txt_ServiceFeedback.Text).Trim();
        else
            spars[2].Value = DBNull.Value;

        ds = spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");

        Response.Redirect("Custs_MyServiceReq.aspx");
        #endregion
    }

    protected void service_btnEscelateToCEO_Click(object sender, EventArgs e)
    {
        //Escalate Customer Service Request to CEO
        Updated_SendEscalate_CustomerService();

        #region old code not required
        /*
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
                lblmessage.Text = "Please enter Service Comment.";
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
                strfileName = "SR_" + txtEmpCode.Text + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
                filename = strfileName;
                uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ServiceRequestPathpath"]).Trim()), strfileName));
            }

            //Assgine SPOC User
            var AssigneTo = "";
            DataTable dtCEO = new DataTable();
            //var id = Convert.ToInt32(hdnDepartmentID.Value.ToString().Trim());
            dtCEO = spm.GetCEOEmpCode();
            if (dtCEO.Rows.Count > 0)
            {
                // txtEmpName.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
                AssigneTo = Convert.ToString(dtCEO.Rows[0]["Emp_Code"]);

            }

            var empCode = Convert.ToString(txtEmpCode.Text).Trim();
            var Serviceid = Convert.ToInt32(Convert.ToString(hdnRemid.Value).Trim());
            var ServicesDepartmentId = Convert.ToInt32(Convert.ToString(hdnDepartmentID.Value).Trim());
            var ServiceDescription = Convert.ToString(txt_SPOCComment.Text).Trim();
            var ActionBy = Convert.ToString(Session["Empcode"]).Trim();
            var Assgineto = AssigneTo;
            var CategoryId = Convert.ToInt32(Convert.ToString(hdnCategoryId.Value).Trim());
            spm.AssgineServiceRequestDetails(empCode, ServiceDescription, "EscalatedUSER", filename, Assgineto, '\0', ServicesDepartmentId, 9, 9, ActionBy, '\0', Serviceid, CategoryId);

            //Send Email To CEO
            DataTable dtEmpDetails = new DataTable();
            dtEmpDetails = spm.GetEmployeeData(Assgineto);
            var assgineToName = "";
            string toMailIDs = "";
            if (dtEmpDetails.Rows.Count > 0)
            {
                assgineToName = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
                // = Convert.ToString(dtEmpDetails.Rows[0]["DesginationName"]);
                // = Convert.ToString(dtEmpDetails.Rows[0]["Department_Name"]);
                // = Convert.ToString(dtEmpDetails.Rows[0]["Grade"]).Trim();
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
                // = Convert.ToString(dtEmpDetails.Rows[0]["DesginationName"]);
                // = Convert.ToString(dtEmpDetails.Rows[0]["Department_Name"]);
                // = Convert.ToString(dtEmpDetails.Rows[0]["Grade"]).Trim();
                ccEmailAddress = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
            }
            //

            var getId = Convert.ToString(hdnRemid.Value);
            string serviceRequestno = Convert.ToString(hdnServiceRequestNo.Value);


            string employeeName = txtEmpName.Text.ToString();
            var employee_Email = Txt_EmpEmail.Text.ToString();

            string createdDate = txtFirstCreatedDate.Text.ToString();
            string department = hdnDepartmentName.Value.ToString().Trim();
            string ccmailid = ccEmailAddress;
            string redirectURL = Convert.ToString(ConfigurationManager.AppSettings["Service_Req_App"]).Trim() + "?id=" + getId + "&type=app";
            string requestDescription = ServiceDescription;
            var actionByName = Session["emp_loginName"].ToString();
            var actionActionDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            var strsubject = "Action update on your Service Request " + serviceRequestno + "";
            int IsCEO = 1;
            spm.ServiceRequest_send_mailto_EscalationHeadOrCEO(IsCEO, serviceRequestno, toMailIDs, employeeName, createdDate, department, ccmailid, redirectURL, requestDescription, actionActionDate);



            Response.Redirect("~/procs/MyService_Req.aspx");
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return;
        }
         */
        #endregion
    }

    protected void service_btnEscelateToHOD_Click(object sender, EventArgs e)
    {
        //Escalate Customer Service Request to PRM
        Updated_SendEscalate_CustomerService();
    }

    protected void service_btnClose_Click(object sender, EventArgs e)
    {
        //Escalate Customer Service Request to Delivery Head
        Updated_SendEscalate_CustomerService();

        #region Service Request Close
        /*
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
                lblmessage.Text = "Please enter Service Comment.";
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
                strfileName = "SR_" + txtEmpCode.Text + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
                filename = strfileName;
                uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["CustomerServiceRequestPathpat"]).Trim()), strfileName));
            }
            var empCode = Convert.ToString(txtEmpCode.Text).Trim();
            var Serviceid = Convert.ToInt32(Convert.ToString(hdnRemid.Value).Trim());
            var ServicesDepartmentId = Convert.ToInt32(Convert.ToString(ddlDepartment.SelectedValue).Trim());
            var ServiceDescription = Convert.ToString(txt_SPOCComment.Text).Trim();
            var ActionBy = Convert.ToString(Session["Empcode"]).Trim();
            var CategoryId = Convert.ToInt32(Convert.ToString(hdnCategoryId.Value).Trim());
            //var Assgineto = ddl_AssginmentEMP.SelectedValue.ToString().Trim();
            if (ServicesDepartmentId == 0)
            {
                ServicesDepartmentId = Convert.ToInt32(Convert.ToString(hdnDepartmentID.Value).Trim());
            }
            spm.AssgineServiceRequestDetails(empCode, ServiceDescription, "UpdateAction_CustomerService", filename, ActionBy, '\0', ServicesDepartmentId, 3, 3, ActionBy, '\0', Serviceid, CategoryId);



            //Send Email To Employee
            var getId = Convert.ToString(hdnRemid.Value);
            string serviceRequestno = Convert.ToString(hdnServiceRequestNo.Value);


            string employeeName = txtEmpName.Text.ToString();
            var employee_Email = Txt_EmpEmail.Text.ToString();

            string createdDate = txtFirstCreatedDate.Text.ToString();
            string department = hdnDepartmentName.Value.ToString().Trim();


            string requestDescription = ServiceDescription;
            DataTable dtEmpDetails = new DataTable();
            var actionByName = Session["emp_loginName"].ToString();
            var actionName = "Closed";
            string redirectURL = Convert.ToString(ConfigurationManager.AppSettings["CustomerService_Req_App_Cust"]).Trim() + "?id=" + getId + "&type=emp";

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
            var dtCEO = spm.GetCEOEmpCode();
            if (dtCEO.Rows.Count > 0)
            {
                getCEOEmpId = Convert.ToString(dtCEO.Rows[0]["Emp_Code"]);
            }
            if (getCEOEmpId != ActionBy)
            {
                spm.CustomerServiceRequest_send_mailto_ActionToEmployee(IsExceletd, serviceRequestno, employee_Email, actionName, actionByName, createdDate, department, "", redirectURL, requestDescription);
            }

            //Send Email To Employee



            Response.Redirect("~/procs/Service.aspx");
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return;
        }
        */
        #endregion
    }

    private void Updated_SendEscalate_CustomerService()
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
                lblmessage.Text = "Please enter Service Comment.";
                return;
            }

            lblmessage.Text = "";
            #region  Uplaod Single file

            
           /* if (uploadfile.HasFile)
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
                strfileName = "CSR_" + txtEmpCode.Text + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
                filename = strfileName;
                uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["CustomerServiceRequestPathpath"]).Trim()), strfileName));
            }*/
            #endregion
            //Assgine SPOC User
            var AssigneTo = "";
            DataTable dtCEO = new DataTable();
            //var id = Convert.ToInt32(hdnDepartmentID.Value.ToString().Trim());
            var id = Convert.ToInt32(hdnIsExceletd.Value);
            var rem_id = Convert.ToInt32(hdnRemid.Value);

            /*dtCEO = spm.get_CustomerService_Escaltions(rem_id, id);
            if (dtCEO.Rows.Count > 0)
            {   
                AssigneTo = Convert.ToString(dtCEO.Rows[0]["Emp_Code"]);
            }*/
            AssigneTo = hdnCustsEscalationEmpCode.Value;


            if (Convert.ToString(AssigneTo).Trim() == "")
            {
                lblmessage.Text = "Please login again";
                return;
            }

            var empCode = Convert.ToString(txtEmpCode.Text).Trim();
            var Serviceid = Convert.ToInt32(Convert.ToString(hdnRemid.Value).Trim());
            var ServicesDepartmentId = Convert.ToInt32(Convert.ToString(hdnDepartmentID.Value).Trim());
            var ServiceDescription = Convert.ToString(txt_SPOCComment.Text).Trim();
            var ActionBy = Convert.ToString(Session["CustCode"]).Trim();
            var Assgineto = AssigneTo;
            var CategoryId = Convert.ToInt32(Convert.ToString(hdnCategoryId.Value).Trim());
            var getdeptName = Convert.ToString(Txt_DeptName.Text).Trim(); //Technology & Innovation

            DataSet dsMaxLog = spm.AssgineCustomerServiceRequestDetails(empCode, ServiceDescription, "CustomerService_Escalated_Customer", filename, Assgineto, '\0', ServicesDepartmentId, Convert.ToInt32(hdnCustsStatusId.Value), Convert.ToInt32(hdnCustsStatusId.Value), ActionBy, '\0', Serviceid, CategoryId, Convert.ToInt32(hdnIsExceletd.Value));

            string getlogmaxid = Convert.ToString(dsMaxLog.Tables[0].Rows[0]["MaxServiceLogId"]);

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
            string redirectURL = Convert.ToString(ConfigurationManager.AppSettings["CustomerService_Req_App"]).Trim() + "?id=" + getId + "&type=app";
            string requestDescription = ServiceDescription;
            var actionByName = Session["custName"].ToString();
            var actionActionDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            //var strsubject = "Action update on your Service Request " + serviceRequestno + "";
            var strsubject = "CUSTOMERFIRST: -Action Update on Service Request Created for " + Txt_DeptName.Text + " by " + txtEmpName.Text + " (" + serviceRequestno + ")";
            int IsCEO = 0;

            string sccmail = "";
            // is Escalated to PRM
            if (Convert.ToInt32(hdnIsExceletd.Value) == 3)
            {
                sccmail = get_PM_PRM_DH_Emails("get_PM_Email");
            }

            //Is Escalated to DH
            if (Convert.ToInt32(hdnIsExceletd.Value) == 4)
            {
                sccmail = get_PM_PRM_DH_Emails("get_PM_PRM_Email");
            }

            // Is Escalated to CS

            if (Convert.ToInt32(hdnIsExceletd.Value) == 5)
            { 
                sccmail = get_PM_PRM_DH_Emails("get_PM_PRM_DH_Email");
            }
            // Is Escalated to CEO

            if (Convert.ToInt32(hdnIsExceletd.Value) == 6)
            {
                IsCEO = 1;

                sccmail = get_PM_PRM_DH_Emails("get_PM_PRM_DH_CS_Email");
            }

            if (Convert.ToString(ccmailid).Trim() != "")
                ccmailid = ccmailid + ";" + sccmail;
            else
                ccmailid = sccmail;

            if (Convert.ToString(ccmailid).Trim() != "")
            {
                // Replace commas with semicolons to have a consistent separator
                ccmailid = ccmailid.Replace(",", ";");

                // Split the string into an array based on the semicolon separator
                string[] emailArray = ccmailid.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                // Remove duplicates using Distinct() and join the emails back into a single string
                 ccmailid = string.Join(";", emailArray.Distinct());
            }

            spm.CustomerServiceRequest_send_mailto_EscalationHeadOrCEO(IsCEO, serviceRequestno, toMailIDs, employeeName, createdDate, department, ccmailid, redirectURL, requestDescription, actionActionDate);

            #region upload Mulitple File

            if (uploadfile.HasFile)
            {
                string Customerfilename = uploadfile.FileName;

                if (Convert.ToString(Customerfilename).Trim() != "")
                {
                    string InvoiceFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["CustomerServiceRequestPathpath"]).Trim() + "/");
                    bool folderExists = Directory.Exists(InvoiceFilePath);
                    if (!folderExists)
                    {
                        Directory.CreateDirectory(InvoiceFilePath);
                    }
                    Int32 t_cnt = 1;

                    if (uploadfile.HasFiles)
                    {
                        foreach (HttpPostedFile uploadedFile in uploadfile.PostedFiles)
                        {

                            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(uploadedFile.FileName);

                            string fileName = ReplaceInvalidChars(fileNameWithoutExtension);

                            if (uploadedFile.ContentLength > 0)
                            {
                                String InputFile = Path.GetExtension(uploadedFile.FileName);
                                string sinvoice_no = Regex.Replace(Convert.ToString(serviceRequestno), @"[^0-9a-zA-Z\._]", "_");
                                filename = "CSR_" + (t_cnt).ToString() + "_" + getlogmaxid.ToString() + "_" + strfromDate + fileName + InputFile;
                                uploadedFile.SaveAs(Path.Combine(InvoiceFilePath, filename));


                                spm.InsertInvoiceUploaded_Files(Convert.ToDouble(getlogmaxid), "CustomerService_Customer", Convert.ToString(filename).Trim(), "Customer", t_cnt);

                                t_cnt = t_cnt + 1;

                            }
                        }
                    }

                }


            }

            #endregion

            Response.Redirect("~/procs/Custs_MyServiceReq.aspx");
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return;
        }
    }


    public string ReplaceInvalidChars(string filename)
    {
        Regex illegalInFileName = new Regex(@"[#%&{}\!/<'>@+*?$|=]");
        string myString = illegalInFileName.Replace(filename, "_");
        //return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        return myString;
    }
    protected void service_btnClearText_Click(object sender, EventArgs e)
    {
        var getType = Convert.ToString(Request.QueryString[1]).Trim();
        if (getType == "emp")
        {
            Response.Redirect("~/procs/MyService_req.aspx");
        }
        else
        {
            Response.Redirect("~/procs/InboxServiceRequest.aspx");
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
                ddlCategory.Items.Clear();
                ddl_AssginmentEMP.DataSource = null;
                ddl_AssginmentEMP.DataBind();

                ddlCategory.DataSource = null;
                ddlCategory.DataBind();
                //BindEmpDDL(value);
                BindCategoryDDL(value);
                //var dptId = Convert.ToInt32(value);
                //var getDepartment = spm.GetEmpDepartment(dptId);
                //if (getDepartment.Rows.Count > 0)
                //{
                //    //DataTable dtleaveInbox = new DataTable();
                //    //dtleaveInbox = spm.GetSerivesRequestDepartment();
                //    ddl_AssginmentEMP.DataSource = getDepartment;
                //    //ddlDepartment.DataBind();
                //    ddl_AssginmentEMP.DataTextField = "EmployeeName";
                //    ddl_AssginmentEMP.DataValueField = "Emp_Code";
                //    ddl_AssginmentEMP.DataBind();
                //    ddl_AssginmentEMP.Items.Insert(0, new ListItem("Select Assigned To Employee", "0")); //updated code
                //}
                //else
                //{
                //    ddl_AssginmentEMP.DataSource = null;
                //}
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
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GET_Locations_employeeList";

            spars[1] = new SqlParameter("@departmentName", SqlDbType.VarChar);
            spars[1].Value = hdnCompcode.Value;


            dsTrDetails = spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                ddl_AssginmentEMP.DataSource = dsTrDetails.Tables[0];
                ddl_AssginmentEMP.DataTextField = "EmployeeName";
                ddl_AssginmentEMP.DataValueField = "Emp_Code";
                ddl_AssginmentEMP.DataBind();
            }
            ddl_AssginmentEMP.Items.Insert(0, new ListItem("Select Assigned To Employee", "0"));
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }



    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //hdnIsExceletd.Value = "0";
            //DataSet ds = new DataSet();

            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var id = Convert.ToString(gvServiceHistory.DataKeys[row.RowIndex].Values[0]).Trim();
            var getDetails = spm.CustomerService_GETHISTORYBYID(Convert.ToInt32(id));
            if (getDetails.Rows.Count > 0)
            {

                var getDate = Convert.ToString(getDetails.Rows[0]["ActionDate"].ToString());
                var getDescripation = Convert.ToString(getDetails.Rows[0]["ServicesDescripation"].ToString());
                var getFileName = Convert.ToString(getDetails.Rows[0]["FilePath"].ToString());
                var getAssignedDate = Convert.ToString(getDetails.Rows[0]["AssignedDate"].ToString());
                var getAssignedBY = Convert.ToString(getDetails.Rows[0]["AssignedBY"].ToString());

                var getActiondBY = Convert.ToString(getDetails.Rows[0]["ActionBy"].ToString());
                var getStatusName = Convert.ToString(getDetails.Rows[0]["StatusName"].ToString());


                txt_AssgimentShowDate.Text = getDate;
                txt_Service_Description.Text = getDescripation;
                //txt_Assigne_By.Text = getAssignedBY;
                //txt_ASDate.Text = getAssignedDate;
                
                bindFilePath.InnerText = getFileName;
                txt_ASDate.Text = getStatusName;
                txtActionBy.Text = getActiondBY;
                txt_Assigne_By.Text = getAssignedBY;
                txtActionBy.Text = getActiondBY;
                if (getStatusName == "Closed")
                {
                    txt_Assigne_By.Text = "";
                }
                else if (getStatusName == "Not Satisfied")
                {
                    txt_Assigne_By.Text = "";
                    // txt_ASDate.Text = "Pending";
                }

                DataSet dsfiles = getServiceLogFiles(Convert.ToInt32(id));
                liuploadedFile3.Visible = false;
                liuploadedFile4.Visible = false;
                gvUploadedFiles_History.DataSource = null;
                gvUploadedFiles_History.DataBind();
                if (dsfiles!=null)
                {
                    gvUploadedFiles_History.DataSource = dsfiles.Tables[0];
                    gvUploadedFiles_History.DataBind();
                    liuploadedFile3.Visible = true;
                    liuploadedFile4.Visible = true;
                }

            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }


    public void GetServiceRequestDetails(string id, string empCode)
    {
        try
        {
            hdnIsExceletd.Value = "0";
            DataSet ds = new DataSet();
            //  ds = spm.getServiceRequestDetail(Convert.ToInt32(id), empCode);

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CustomerService_GETApprovelPageDetail";

            spars[1] = new SqlParameter("@id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDecimal(hdnRemid.Value);

            spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(empCode);          

            ds = spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");

            var departmentId = ds.Tables[3].Rows[0]["ServiceDepartment"].ToString();
            hdnDepartmentID.Value = ds.Tables[3].Rows[0]["ServiceDepartment"].ToString();
            hdnCategoryId.Value = ds.Tables[3].Rows[0]["CategoryId"].ToString();
            hdnCurrentAssgineEMP.Value = ds.Tables[3].Rows[0]["AssignedTo"].ToString();

            var CustomerStatusId = 0;

            
            //Service Details
            if (ds.Tables[3].Rows.Count > 0)
            {
                if (Convert.ToString(ds.Tables[3].Rows[0]["cust_statusId"]).Trim() != "")
                    CustomerStatusId = Convert.ToInt32(ds.Tables[3].Rows[0]["cust_statusId"]);
            }
            //Assgine Emp Data
            if (ds.Tables[0].Rows.Count > 0)
            {
                var empdetail = ds.Tables[0];
                txtEmpName.Text = Convert.ToString(empdetail.Rows[0]["Emp_Name"]);
                Txt_DeptName.Text = Convert.ToString(empdetail.Rows[0]["Location_name"]);
                txtEmpCode.Text = Convert.ToString(empdetail.Rows[0]["Emp_Code"]);
                Txt_Designation.Text = Convert.ToString(empdetail.Rows[0]["Designation"]).Trim();
                Txt_EmpEmail.Text = Convert.ToString(empdetail.Rows[0]["Emp_Emailaddress"]);
                Txt_EmpMobile.Text = Convert.ToString(empdetail.Rows[0]["mobile"]);
                hdnCompcode.Value = Convert.ToString(empdetail.Rows[0]["comp_code"]);
            }
            //check Is Login employee is opend his Service Request
            if (Convert.ToString(empCode).Trim() != Convert.ToString(txtEmpCode.Text).Trim())
            {
                //Response.Redirect(ReturnUrl("sitepathmain") + "procs/Custs_MyServiceReq"); 
                Response.Redirect("Custs_MyServiceReq.aspx");
            }
            //Assigne  Service History
            gvServiceHistory.DataSource = null;
            gvServiceHistory.DataBind();
            if (ds.Tables[1].Rows.Count > 0)
            {
                var rowsCount = ds.Tables[1].Rows.Count;

                var getServiceLog = ds.Tables[1];

               // getFillDropdownlist()
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
                var getAssignedBY_Code = Convert.ToString(getRowData["AssignedTo"].ToString());

                var getFristRowData = ds.Tables[1].Rows[0];
                var getFiestAssignedDate = Convert.ToString(getFristRowData["AssignedDate"].ToString());
                var getFirstDescription = Convert.ToString(getFristRowData["ServicesDescripation"].ToString());
                var getFirstCategory = Convert.ToString(getFristRowData["CategoryTitle"].ToString());
                var getFirstFilePath = Convert.ToString(getFristRowData["FilePath"].ToString());
                txtFirstCreatedDate.Text = getFiestAssignedDate;
                lblCreateFile.InnerText = getFirstFilePath;
                txtFirstDeacription.Text = getFirstDescription;
                txt_category.Text = getFirstCategory;
                hdnServiceRequestNo.Value = getServiceRequestID;
                lblheading.Text = "Customer Service Request - " + getServiceRequestID;
                txt_AssgimentShowDate.Text = getDate;
                txt_Service_Description.Text = getDescripation;
                bindFilePath.InnerText = getFileName;
                txt_Assigne_By.Text = getAssignedBY;
                txt_Assign_By_EmpCode.Text = getAssignedBY_Code;
                txt_ASDate.Text = getStatusName;
                txtActionBy.Text = getActiondBY;

               DataSet dsfiles=  getServiceLogFiles(Convert.ToInt32(ds.Tables[1].Rows[0]["Id"].ToString()));
                

                if (Convert.ToString(getFileName).Trim() == "")
                {
                    liuploadedFile3.Visible = false;
                    liuploadedFile4.Visible = false;
                }
                if (dsfiles != null)
                {
                    gvuploadedFiles.DataSource = dsfiles;
                    gvuploadedFiles.DataBind();

                    if (dsfiles.Tables[0].Rows.Count <= 0)
                    {                        
                        liUploadedFiles1.Visible = false;
                        liUploadedFiles1.Visible = false;                        
                    }
                }
                if (getStatusName == "Closed")
                {
                    txt_Assigne_By.Text = "";

                }
                else if (getStatusName == "Not Satisfied")
                {
                    txt_Assigne_By.Text = "";
                    // txt_ASDate.Text = "Pending";
                }
                //var rows2 = from row in ds.Tables[1].AsEnumerable()
                //            where row.Field<string>("StatusName") == "Closed"
                //            select row;

                //int count2 = rows2.Count<DataRow>();
                //if (count2 == 1)
                //{
                //    hdnIsExceletd.Value = "1";//Escelate to HOD
                //}
                //else if (count2 == 2)
                //{
                //    hdnIsExceletd.Value = "2";//Escelate to CEO
                //}
                //else
                //{
                //    hdnIsExceletd.Value = "0";//
                //}

                if (getServiceLog.Rows.Count > 1)
                {
                    //  DataTable selectedTable = getServiceLog.AsEnumerable().Where(r => r.Field<string>("ReceivedDate") != null).CopyToDataTable();
                    gvServiceHistory.DataSource = getServiceLog;
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
                var getCat = ds.Tables[4];
                loadDropDownEmp(getEmpDetail, departmentId);
                var selectedDepartment = getEmpDetail.AsEnumerable().Where(r => r.Field<int>("DepartmentId") == Convert.ToInt32(departmentId)).Select(s => s.Field<string>("DepartmentName")).FirstOrDefault();
                txtSelectedDepartment.Text = selectedDepartment;
                getFillDropdownlist();
                // BindCategoryDDL(departmentId);
                ddlCategory.SelectedValue = Convert.ToString(hdnCategoryId.Value);
                BindEmpDDL(Convert.ToString(hdnCompcode.Value));
                ddl_AssginmentEMP.SelectedValue = txt_Assign_By_EmpCode.Text;
                var getCatogary = getCat.AsEnumerable().Where(r => r.Field<int>("Id") == Convert.ToInt32(Convert.ToString(hdnCategoryId.Value))).Select(s => s.Field<string>("CategoryTitle")).FirstOrDefault();
                txt_category.Text = getCatogary;
            }
            var getService = ds.Tables[3];
            var getStatus = Convert.ToInt32(getService.Rows[0]["Status"].ToString());

            liStatisfied3.Visible = false;
            liStatisfied4.Visible = false;
            divhr1.Visible = false;
            if (getStatus == 3)
            {
                liStatisfied3.Visible = true;
                liStatisfied4.Visible = true;
                divhr1.Visible = true;
            }

                #region Code for Escalation

                if (ds.Tables[3].Rows.Count > 0)
            {
                /*var departID = Convert.ToInt32(ds.Tables[3].Rows[0]["ServiceDepartment"].ToString());

                foreach (DataRow drow in ds.Tables[2].Rows)
                {
                    var value = Convert.ToInt32(drow["DepartmentId"].ToString());
                    if (departID == value)
                        hdnDepartmentName.Value = drow["DepartmentName"].ToString();
                }

                var getType = Convert.ToString(Request.QueryString[1]).Trim();
                var getService = ds.Tables[3];
                var getStatusCEO = Convert.ToInt32(getService.Rows[0]["Status"].ToString());

                if (getType == "emp")
                {
                    var getStatus = Convert.ToInt32(getService.Rows[0]["Status"].ToString());
                    if (getStatus == 3)
                    {
                        lblAssgineTitle.InnerText = "Escalation Details";
                        lblActionAssginDate.InnerText = "Escalation Date";
                        lblActionAssginComment.InnerText = "Escalation Description";
                        var getDayes = 8;
                        var diffDay = 0.00;
                        var getTodayDate = DateTime.Now.Date;
                        var lastupdatedDate = Convert.ToDateTime(getService.Rows[0]["AssignedDate"].ToString()).Date;//AssignedDate
                                                                                                                     //Call Day Count
                        string officeLocation = "HO-NaviMum";
                        var getDayesdt = spm.GetEscalationServiceCount(officeLocation, lastupdatedDate, getTodayDate);
                        if (getDayesdt.Rows.Count > 0)
                        {
                            diffDay = Convert.ToInt32(getDayesdt.Rows[0]["WORKINGDAY"]);
                        }
                        else
                        {
                            diffDay = (getTodayDate - lastupdatedDate).TotalDays;
                        }
                        var getDepartment = Convert.ToInt32(hdnDepartmentID.Value);
                        DataTable dtSPOC = spm.GetSPOCData(getDepartment);
                        if (dtSPOC.Rows.Count > 0)
                        {
                            getDayes = Convert.ToInt32(dtSPOC.Rows[0]["USER_ESCALATION"]);
                        }
                        if (getDayes >= diffDay)
                        {
                            empShow1.Visible = true;
                            empShow3.Visible = true;
                            divsh.Visible = true;
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
                                service_btnEscelateToCEO.Visible = false;
                                service_btnClearText.Visible = false;
                            }
                            else if (countHeadStatus == 1)
                            {
                                hdnIsExceletd.Value = "2";//Escelate to CEO
                                service_btnEscelateToHOD.Visible = false;
                                service_btnEscelateToCEO.Visible = true;
                                service_btnClearText.Visible = true;

                                lblAssgineTitle.InnerText = "Bad Service Feedback";
                                lblActionAssginDate.InnerText = "Feedback Date";
                                lblActionAssginComment.InnerText = "Feedback Description";
                            }
                            else if (count == 1)
                            {
                                hdnIsExceletd.Value = "1";//Escelate to HOD
                                service_btnEscelateToHOD.Visible = true;
                                service_btnEscelateToCEO.Visible = false;
                                service_btnClearText.Visible = true;
                            }
                            else if (count == 2)
                            {
                                hdnIsExceletd.Value = "2";//Escelate to CEO
                                service_btnEscelateToHOD.Visible = false;
                                service_btnEscelateToCEO.Visible = true;
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
                                service_btnEscelateToCEO.Visible = false;
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
                            service_btnEscelateToCEO.Visible = false;
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
                        service_btnEscelateToCEO.Visible = false;
                        service_btnClearText.Visible = false;
                    }
                }

                if (getStatusCEO == 9)
                {
                    service_btnSendSPOC.Visible = false;
                }
                */
            }

            #endregion


            #region New Escalation
            /*
                       empShow1.Visible = false;
                       empShow3.Visible = false;
                       divsh.Visible = false;
                       empShow4.Visible = false;
                       empShow5.Visible = false;

                       empShow12.Visible = false;
                       empShow.Visible = false;
                       empShow2.Visible = false;
                       empShow7.Visible = false;

                       service_btnEscelateToHOD.Visible = false;
                       service_btnClose.Visible = false;
                       service_btnEscelateToCEO.Visible = false;
                       service_btnClearText.Visible = false;

                       if (CustomerStatusId != 2 && CustomerStatusId != 3)
                       {
                           DataSet dsEscalation = Check_EscaltionLevels();
                       if (dsEscalation.Tables[0].Rows.Count > 0)
                       {

                           var getService = ds.Tables[3];

                           if (Convert.ToString(hdnRemid_Type.Value).Trim() == "emp")
                           {
                               var getStatus = Convert.ToInt32(getService.Rows[0]["Status"].ToString());

                               if (getStatus == 3)
                               {
                                   var Escalation = Convert.ToInt32(dsEscalation.Tables[0].Rows[0]["EscalationLevel_Id"]);
                                   var sPRM = Convert.ToString(ds.Tables[0].Rows[0]["PRM"]);
                                   var sDH = Convert.ToString(ds.Tables[0].Rows[0]["DH"]);
                                   var sCEO = Convert.ToString(ds.Tables[0].Rows[0]["CEO_Code"]);

                                   if (dsEscalation.Tables[1].Rows.Count >= 0)
                                   {
                                       if (Convert.ToString(dsEscalation.Tables[1].Rows[0]["isPMPRM"]).Trim() == "true")
                                       {
                                           Escalation = 4;
                                       }

                                   }

                                   DataSet dsPRMDH = new DataSet();
                                   // get_NextLevel_Escaltion_details
                                   if (Convert.ToString(Escalation).Trim() == "3")
                                   {
                                       dsPRMDH = get_NextLevel_Escaltion_details(sPRM);
                                   }
                                   if (Convert.ToString(Escalation).Trim() == "4")
                                   {
                                       dsPRMDH = get_NextLevel_Escaltion_details(sDH);
                                   }
                                   if (Convert.ToString(Escalation).Trim() == "5")
                                   {
                                       dsPRMDH = get_NextLevel_Escaltion_details(sCEO);
                                   }
                                   if (dsPRMDH != null)
                                   {
                                       if (dsPRMDH.Tables[0].Rows.Count > 0)
                                       {
                                           hdnCustsStatusId.Value = Convert.ToString(dsPRMDH.Tables[0].Rows[0]["StatusId"]).Trim(); // StattusID                               
                                           hdnCustsEscalationLevelId.Value = Convert.ToString(dsPRMDH.Tables[0].Rows[0]["EscalationLevel_Id"]).Trim(); // EscalationLevel_Id                               
                                           hdnCustsEscalationEmpCode.Value = Convert.ToString(dsPRMDH.Tables[0].Rows[0]["emp_code"]).Trim(); // emp_code                               
                                           hdnCustsEscalationPRMDHCEO.Value = Convert.ToString(dsPRMDH.Tables[0].Rows[0]["PRM_DH"]).Trim(); // PRM/DH/CEO                               

                                           //Escalate to PRM
                                           if (Convert.ToString(dsPRMDH.Tables[0].Rows[0]["EscalationLevel_Id"]).Trim() == "3")
                                           {
                                               lblAssgineTitle.InnerText = "Escalation Details";
                                               lblActionAssginDate.InnerText = "Escalation Date";
                                               lblActionAssginComment.InnerText = "Escalation Description";

                                               empShow1.Visible = true;
                                               empShow3.Visible = true;
                                               divsh.Visible = true;
                                               empShow4.Visible = true;
                                               empShow5.Visible = true;

                                               hdnIsExceletd.Value = Convert.ToString(dsPRMDH.Tables[0].Rows[0]["EscalationLevel_Id"]).Trim(); // PRM                               

                                               service_btnEscelateToHOD.Visible = true;
                                               service_btnEscelateToCEO.Visible = false;
                                               service_btnClose.Visible = false;
                                               service_btnClearText.Visible = true;
                                           }

                                           //Escalate to DH
                                           if (Convert.ToString(dsPRMDH.Tables[0].Rows[0]["EscalationLevel_Id"]).Trim() == "4")
                                           {
                                               lblAssgineTitle.InnerText = "Escalation Details";
                                               lblActionAssginDate.InnerText = "Escalation Date";
                                               lblActionAssginComment.InnerText = "Escalation Description";

                                               empShow1.Visible = true;
                                               empShow3.Visible = true;
                                               divsh.Visible = true;
                                               empShow4.Visible = true;
                                               empShow5.Visible = true;

                                               hdnIsExceletd.Value = Convert.ToString(dsPRMDH.Tables[0].Rows[0]["EscalationLevel_Id"]).Trim(); // DH
                                               service_btnEscelateToHOD.Visible = false;
                                               service_btnClose.Visible = true;
                                               service_btnEscelateToCEO.Visible = false;
                                               service_btnClearText.Visible = true;
                                           }
                                           //Escalate to CEO
                                           if (Convert.ToString(dsPRMDH.Tables[0].Rows[0]["EscalationLevel_Id"]).Trim() == "5")
                                           {
                                               lblAssgineTitle.InnerText = "Escalation Details";
                                               lblActionAssginDate.InnerText = "Escalation Date";
                                               lblActionAssginComment.InnerText = "Escalation Description";

                                               empShow1.Visible = true;
                                               empShow3.Visible = true;
                                               divsh.Visible = true;
                                               empShow4.Visible = true;
                                               empShow5.Visible = true;

                                               hdnIsExceletd.Value = Convert.ToString(dsPRMDH.Tables[0].Rows[0]["EscalationLevel_Id"]).Trim(); // CEO
                                               service_btnEscelateToHOD.Visible = false;
                                               service_btnClose.Visible = false;
                                               service_btnEscelateToCEO.Visible = true;
                                               service_btnClearText.Visible = true;
                                           }
                                       }
                                   }

                               }

                           }
                       }
                       }


             */

            #endregion

            // hide all escaltion dertails
            empShow1.Visible = false;
            empShow3.Visible = false;
            divsh.Visible = false;
            empShow4.Visible = false;
            empShow5.Visible = false;

            empShow12.Visible = false;
            empShow.Visible = false;
            empShow2.Visible = false;
            empShow7.Visible = false;

            service_btnEscelateToHOD.Visible = false;
            service_btnClose.Visible = false;
            service_btnEscelateToCEO.Visible = false;
            service_btnClearText.Visible = false;

            if (CustomerStatusId==2)
            {
                liStatisfied3.Visible = false;
                liStatisfied4.Visible = false;
                divhr1.Visible = false;
            }

             

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();
            throw;
        }

    }

    public DataSet getServiceLogFiles(int slogid)
    {
        
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CustomerService_LogFiles";

            spars[1] = new SqlParameter("@id", SqlDbType.Int);
            spars[1].Value = slogid;


        DataSet dslist = spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");

            return dslist;
        
    }
    public void loadDropDownEmp(DataTable dtleaveInbox, string id)
    {
        //DataTable dtleaveInbox = new DataTable();
        //dtleaveInbox = spm.GetSerivesRequestDepartment();
        ddlDepartment.DataSource = dtleaveInbox;
        //ddlDepartment.DataBind();
        ddlDepartment.DataTextField = "DepartmentName";
        ddlDepartment.DataValueField = "DepartmentId";
        ddlDepartment.SelectedValue = id;
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem("Select Assigned To Department", "0")); //updated code
        //BindEmpDDL(id);
    }


    #endregion

    #region PageMethods

    private DataTable Check_IsLoginEmployee_AsSPOC(string empCode)
    {
        DataTable dttrInbox = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "Check_CustomerService_PM_SPOC";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = empCode;
            dttrInbox = spm.getDataList(spars, "SP_INSERTSERVICE_REQUEST");

        }
        catch (Exception ex)
        {
        }

        return dttrInbox;
    }

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


    //public void getApproverdata()
    //{

    //    DataTable dtApproverEmailIds = new DataTable();
    //    dtApproverEmailIds = spm.GeTfuelApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToDecimal(hdnRemid.Value));
    //    //IsEnabledFalse (true);
    //    dspaymentVoucher_Apprs.Tables.Add(dtApproverEmailIds);
    //    lstApprover.Items.Clear();
    //    if (dtApproverEmailIds.Rows.Count > 0)
    //    {
    //        foreach (DataRow row in dtApproverEmailIds.Rows)
    //        {
    //            if (row[1].ToString() == "00002726")
    //            {
    //                CEOInList = "Y";
    //            }
    //        }
    //        hdnApprEmailaddress.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["Emp_Emailaddress"]);
    //        hdnApprId.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["APPR_ID"]);

    //        //lstApprover.DataSource = dtApproverEmailIds;
    //        //lstApprover.DataTextField = "names";
    //        //lstApprover.DataValueField = "APPR_ID";
    //        //lstApprover.DataBind();

    //        //DgvApprover.DataSource = null;
    //        //DgvApprover.DataBind();

    //        //if (dtApproverEmailIds.Rows.Count > 0)
    //        //{
    //        //    DgvApprover.DataSource = dtApproverEmailIds;
    //        //    DgvApprover.DataBind();
    //        //}

    //        hflapprcode.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["approver_emp_code"]);
    //    }
    //    else
    //    {
    //        lblmessage.Text = "Since you are not assigned under any approver you can not apply for any type of Travel request, please contact HR";

    //    }
    //}

    //public void getApproverdata_old()
    //{

    //    DataTable dtApproverEmailIds = new DataTable();
    //    dtApproverEmailIds = spm.GeTfuelApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), 0);
    //    //IsEnabledFalse (true);
    //    lstApprover.Items.Clear();
    //    if (dtApproverEmailIds.Rows.Count > 0)
    //    {
    //        hdnApprEmailaddress.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["Emp_Emailaddress"]);
    //        hdnApprId.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["APPR_ID"]);

    //        //lstApprover.DataSource = null;
    //        //lstApprover.DataBind();

    //        lstApprover.DataSource = dtApproverEmailIds;
    //        lstApprover.DataTextField = "Emp_Name";
    //        lstApprover.DataValueField = "APPR_ID";
    //        lstApprover.DataBind();

    //        hflapprcode.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["A_EMP_CODE"]);
    //    }
    //    else
    //    {
    //        lblmessage.Text = "Since you are not assigned under any approver you can not apply for any type of Travel request, please contact HR";

    //    }
    //}

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
    //protected string GetApprove_RejectList()
    //{
    //    StringBuilder sbapp = new StringBuilder();
    //    sbapp.Length = 0;
    //    sbapp.Capacity = 0;
    //    DataTable dtAppRej = new DataTable();
    //    //dtAppRej = spm.GetMobileApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
    //    dtAppRej = spm.GeTfuelApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToDecimal(hdnRemid.Value));
    //    if (dtAppRej.Rows.Count > 0)
    //    {
    //        sbapp.Append("<table>");
    //        for (int i = 0; i < dtAppRej.Rows.Count; i++)
    //        {
    //            sbapp.Append("<tr>");
    //            sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
    //            sbapp.Append("</tr>");
    //        }
    //        sbapp.Append("</table>");
    //    }
    //    return Convert.ToString(sbapp);
    //}
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
    //protected void mobile_btnPrintPV_Click(object sender, EventArgs e)
    //{
    //    getApproverdata();
    //    getPayementVoucher_forPrint();
    //}
    //Update This Function 20-01-2021

    //protected void chk_exception_CheckedChanged(object sender, EventArgs e)
    //{
    //    //if (chk_exception.Checked)
    //    //    hdnTravelConditionid.Value = "3";
    //    //else
    //    hdnTravelConditionid.Value = "2";
    //    getApproverdata();
    //}
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
            var getDepartment = spm.GetCategoryDepartment(dptId);
            if (getDepartment.Rows.Count > 0)
            {
                //DataTable dtleaveInbox = new DataTable();
                //dtleaveInbox = spm.GetSerivesRequestDepartment();
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
            /* DropDownList list = (DropDownList)sender;
             string value = (string)list.SelectedValue;
             if (value == "0")
             {
                 ddl_AssginmentEMP.DataSource = null;
             }
             else
             {
                 BindEmpDDL(value);
             }*/

        }
        catch (Exception)
        {

            throw;
        }
    }


    public void getFillDropdownlist()
    {
        try
        {

            SqlParameter[] spars = new SqlParameter[1];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getCustomerServiceCategoryList";

            DataSet dslist = spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");


            ddlDepartment.DataSource = dslist.Tables[0];
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentId";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("Select Department", "0"));
            ddlDepartment.SelectedIndex = 1;

            ddlCategory.DataSource = dslist.Tables[1];
            ddlCategory.DataTextField = "CategoryTitle";
            ddlCategory.DataValueField = "Id";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("Select Service Request Category", "0"));

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    public DataSet Check_EscaltionLevels()
    {

        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "CustomerService_EscaltionNextLevel";

        spars[1] = new SqlParameter("@id", SqlDbType.Int);
        spars[1].Value = Convert.ToInt32(hdnRemid.Value);

        DataSet dslist = spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");

        return dslist;


    }

    public DataSet get_NextLevel_Escaltion_details(string sEmpCode)
    {

        SqlParameter[] spars = new SqlParameter[3];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "Customer_Service_NextLevel_Escalation_details";

        spars[1] = new SqlParameter("@id", SqlDbType.Int);
        spars[1].Value = Convert.ToInt32(hdnRemid.Value);

        spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[2].Value = Convert.ToString(sEmpCode);

        return spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");
    }

    public DataTable Customer_GetSPOCData()
    {

        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "CustomerService_GETSPOCDETAIL_Escalation";

        spars[1] = new SqlParameter("@id", SqlDbType.Int);
        spars[1].Value = Convert.ToInt32(hdnRemid.Value);

        DataTable dslist = spm.getDataList(spars, "SP_INSERTSERVICE_REQUEST");

        return dslist;


    }

    public string get_PM_PRM_DH_Emails(string sqtype)
    {
        string semail = "";
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = sqtype;

        spars[1] = new SqlParameter("@id", SqlDbType.Int);
        spars[1].Value = Convert.ToInt32(hdnRemid.Value);

        DataSet dslist = spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");

        if (dslist != null)
        {
            if (dslist.Tables[0].Rows.Count > 0)
            {
                semail = Convert.ToString(dslist.Tables[0].Rows[0]["EmailList"]).Trim();
            }
        }
        return semail;


    }

    protected void btnSatisfied_Click(object sender, ImageClickEventArgs e)
    {
        empShow1.Visible = false;
        empShow3.Visible = false;
        divsh.Visible = false;
        empShow4.Visible = false;
        empShow5.Visible = false;

        empShow12.Visible = false;
        empShow.Visible = false;
        empShow2.Visible = false;
        empShow7.Visible = false;

        service_btnEscelateToHOD.Visible = false;
        service_btnClose.Visible = false;
        service_btnEscelateToCEO.Visible = false;
        service_btnClearText.Visible = false;

        service_btnSendSPOC.Visible = true;
        liStatisfied1.Visible = true;
        liStatisfied2.Visible = true;
    }

    protected void btnNotSatisfied_Click(object sender, ImageClickEventArgs e)
    {
        #region New Escalation

        liStatisfied1.Visible = false;
        liStatisfied2.Visible = false;
        service_btnSendSPOC.Visible = false;

        empShow1.Visible = false;
        empShow3.Visible = false;
        divsh.Visible = false;
        empShow4.Visible = false;
        empShow5.Visible = false;

        empShow12.Visible = false;
        empShow.Visible = false;
        empShow2.Visible = false;
        empShow7.Visible = false;

        service_btnEscelateToHOD.Visible = false;
        service_btnClose.Visible = false;
        service_btnEscelateToCEO.Visible = false;
        service_btnClearText.Visible = false;
        service_btnAssgine.Visible = false;

        //if (CustomerStatusId != 2 && CustomerStatusId != 3)
        //{
        DataSet dsEscalation = Check_EscaltionLevels();
        if (dsEscalation.Tables[0].Rows.Count > 0)
        {

            // var getService = ds.Tables[3];

            var Escalation = Convert.ToInt32(dsEscalation.Tables[0].Rows[0]["EscalationLevel_Id"]);
            var sPRM = Convert.ToString(dsEscalation.Tables[2].Rows[0]["PRM"]);
            var sDH = Convert.ToString(dsEscalation.Tables[2].Rows[0]["DH"]);
            var sCEO = Convert.ToString(dsEscalation.Tables[2].Rows[0]["CEO_Code"]);
            var sCS = Convert.ToString(dsEscalation.Tables[2].Rows[0]["CS_Code"]);

            if (dsEscalation.Tables[1].Rows.Count >= 0)
            {
                if (Convert.ToString(dsEscalation.Tables[1].Rows[0]["isPMPRM"]).Trim() == "true")
                {
                    Escalation = 4;
                }

            }

            DataSet dsPRMDH = new DataSet();
            // get_NextLevel_Escaltion_details
            if (Convert.ToString(Escalation).Trim() == "3")
            {
                dsPRMDH = get_NextLevel_Escaltion_details(sPRM);
            }
            if (Convert.ToString(Escalation).Trim() == "4")
            {
               
                dsPRMDH = get_NextLevel_Escaltion_details(sDH);
               // dsPRMDH = get_NextLevel_Escaltion_details(sCS);
            }
            if (Convert.ToString(Escalation).Trim() == "5")
            {
                dsPRMDH = get_NextLevel_Escaltion_details(sCS);
            }
            if (Convert.ToString(Escalation).Trim() == "6")
            {
                dsPRMDH = get_NextLevel_Escaltion_details(sCEO);
            }
            if (dsPRMDH != null)
            {
                if (dsPRMDH.Tables[0].Rows.Count > 0)
                {
                    hdnCustsStatusId.Value = Convert.ToString(dsPRMDH.Tables[0].Rows[0]["StatusId"]).Trim(); // StattusID                               
                    hdnCustsEscalationLevelId.Value = Convert.ToString(dsPRMDH.Tables[0].Rows[0]["EscalationLevel_Id"]).Trim(); // EscalationLevel_Id                               
                    hdnCustsEscalationEmpCode.Value = Convert.ToString(dsPRMDH.Tables[0].Rows[0]["emp_code"]).Trim(); // emp_code                               
                    hdnCustsEscalationPRMDHCEO.Value = Convert.ToString(dsPRMDH.Tables[0].Rows[0]["PRM_DH"]).Trim(); // PRM/DH/CEO                               

                    //Escalate to PRM
                    if (Convert.ToString(dsPRMDH.Tables[0].Rows[0]["EscalationLevel_Id"]).Trim() == "3")
                    {
                        lblAssgineTitle.InnerText = "Escalation Details";
                        lblActionAssginDate.InnerText = "Escalation Date";
                        lblActionAssginComment.InnerText = "Escalation Description";

                        empShow1.Visible = true;
                        empShow3.Visible = true;
                        divsh.Visible = true;
                        empShow4.Visible = true;
                        empShow5.Visible = true;

                        hdnIsExceletd.Value = Convert.ToString(dsPRMDH.Tables[0].Rows[0]["EscalationLevel_Id"]).Trim(); // PRM                               

                        service_btnEscelateToHOD.Visible = true;
                        service_btnClearText.Visible = true;
                        
                    }

                    //Escalate to DH
                    if (Convert.ToString(dsPRMDH.Tables[0].Rows[0]["EscalationLevel_Id"]).Trim() == "4")
                    {
                        lblAssgineTitle.InnerText = "Escalation Details";
                        lblActionAssginDate.InnerText = "Escalation Date";
                        lblActionAssginComment.InnerText = "Escalation Description";

                        empShow1.Visible = true;
                        empShow3.Visible = true;
                        divsh.Visible = true;
                        empShow4.Visible = true;
                        empShow5.Visible = true;

                        hdnIsExceletd.Value = Convert.ToString(dsPRMDH.Tables[0].Rows[0]["EscalationLevel_Id"]).Trim(); // DH
                        
                        service_btnClose.Visible = true;                        
                        service_btnClearText.Visible = true;
                        
                    }
                    //Escalate to CS
                    if (Convert.ToString(dsPRMDH.Tables[0].Rows[0]["EscalationLevel_Id"]).Trim() == "5")
                    {
                        lblAssgineTitle.InnerText = "Escalation Details";
                        lblActionAssginDate.InnerText = "Escalation Date";
                        lblActionAssginComment.InnerText = "Escalation Description";

                        empShow1.Visible = true;
                        empShow3.Visible = true;
                        divsh.Visible = true;
                        empShow4.Visible = true;
                        empShow5.Visible = true;

                        hdnIsExceletd.Value = Convert.ToString(dsPRMDH.Tables[0].Rows[0]["EscalationLevel_Id"]).Trim(); // CS                        
                        service_btnClearText.Visible = true;
                        service_btnAssgine.Visible = true;
                    }

                    //Escalate to CEO
                    if (Convert.ToString(dsPRMDH.Tables[0].Rows[0]["EscalationLevel_Id"]).Trim() == "6")
                    {
                        lblAssgineTitle.InnerText = "Escalation Details";
                        lblActionAssginDate.InnerText = "Escalation Date";
                        lblActionAssginComment.InnerText = "Escalation Description";

                        empShow1.Visible = true;
                        empShow3.Visible = true;
                        divsh.Visible = true;
                        empShow4.Visible = true;
                        empShow5.Visible = true;

                        hdnIsExceletd.Value = Convert.ToString(dsPRMDH.Tables[0].Rows[0]["EscalationLevel_Id"]).Trim(); // CEO                        
                        service_btnEscelateToCEO.Visible = true;
                        service_btnClearText.Visible = true;
                    }
                }
            }

        }
        // }




        #endregion


    }
}
