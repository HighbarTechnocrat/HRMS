using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Cust_Service_Req : System.Web.UI.Page
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

    #region PageEvents
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["CustCode"]).Trim() == "" || Session["CustCode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "Cust_sessionend.aspx");
            }

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "Cust_Login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Cust_Service_Req");
            }
            else
            {
                txtEmpCode.Text = Convert.ToString(Session["CustCode"]);
                hdnCustEmailaddress.Value = Convert.ToString(Session["CustEmailaddress"]);
                hdncustName.Value = Convert.ToString(Session["custName"]);

                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    Txt_Service_Desription.Attributes.Add("maxlength", "500");

                    getFillDropdownlist();
                    getPMPRMDetails();
                    Txt_DateCreate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:MM:ss");

                    editform.Visible = true;
                    divbtn.Visible = false;

                    mobile_cancel.Visible = false;




                }
                else
                {

                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        string filename = "";
        String strfileName = "";
        string strfromDate = "";
        try
        {
            var id = Convert.ToInt32(ddlDepartment.SelectedValue.ToString().Trim());
            if (id == 0)
            {
                lblmessage.Text = "Please select department.";
                return;
            }
            var idCategory = Convert.ToInt32(ddlCategory.SelectedValue.ToString().Trim());
            if (idCategory == 0)
            {
                lblmessage.Text = "Please select Service Request Category.";
                return;
            }
            if (Convert.ToString(Txt_Service_Desription.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Service Request Descripation.";
                return;
            }

            lblmessage.Text = "";


            #region Single file upload
            /*
            if (uploadfile.HasFile)
            {
                filename = uploadfile.FileName;
            }

            if (Convert.ToString(filename).Trim() != "")
            {
                DateTime loadedDate = DateTime.Now;
                strfromDate = loadedDate.ToString("ddMMyyyyHHmmss");
                filename = uploadfile.FileName;
                strfileName = "";
                strfileName = "CSR_" + txtEmpCode.Text + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
                filename = strfileName;
                uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["CustomerServiceRequestPathpath"]).Trim()), strfileName));
            }
            */
            #endregion

            //var AssigneTo = hdnPM_Code.Value;
            var AssigneTo = hdnPRM_Code.Value;

            hdnsptype.Value = "CustomerServiceReqInsert";

            DataSet dsmax= spm.InsertCustomerServiceRequestDetails(txtEmpCode.Text, Txt_Service_Desription.Text, hdnsptype.Value, filename, AssigneTo, '\0', id, 1, 1, Convert.ToString(txtEmpCode.Text).Trim(), '\0', idCategory);

            if (dsmax != null)
            {
                string getmaxid = Convert.ToString(dsmax.Tables[1].Rows[0]["maxRemid"]);
                string getlogmaxid = Convert.ToString(dsmax.Tables[1].Rows[0]["MaxServiceLogId"]);
                var serviceRequestDetail = spm.GetCustomerServiceRequestById(Convert.ToInt32(getmaxid));

                if (serviceRequestDetail.Rows.Count > 0)
                {

                    string serviceRequestno = Convert.ToString(serviceRequestDetail.Rows[0]["ServicesRequestID"]);
                    string toMailIDs = "";
                    string employeeName = Session["custName"].ToString();
                    string createdDate = Convert.ToDateTime(serviceRequestDetail.Rows[0]["CreatedDate"]).ToString("dd-MM-yyyy HH:mm:ss");
                    string department = ddlDepartment.SelectedItem.ToString().Trim();
                    string ccmailid = "";

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


                    string redirectURL = Convert.ToString(ConfigurationManager.AppSettings["CustomerService_Req_App"]).Trim() + "?id=" + getmaxid + "&type=app";

                    string requestDescription = Txt_Service_Desription.Text.Trim();
                    DataTable dtEmpDetails = new DataTable();
                   // dtEmpDetails = spm.GetEmployeeData(AssigneTo);

                    SqlParameter[] spars = new SqlParameter[2];
                    spars[1] = new SqlParameter("@EMP_CODE", SqlDbType.VarChar);
                    spars[1].Value = AssigneTo;
                    DataSet dslist = spm.getDatasetList(spars, "SP_GETEMPDETAILS");
                    dtEmpDetails = dslist.Tables[0];



                    if (dtEmpDetails.Rows.Count > 0)
                    {
                        toMailIDs = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
                    }

                    #region create & Send Email
                    StringBuilder strbuild = new StringBuilder();

                    // var strsubject = "New Customer Service Request " + serviceRequestno + " is created, your action is required";
                    var strsubject = "CustomerFIRST :- Service Request Created for " + txtProjectName.Text + " by " + employeeName + " ( " + serviceRequestno + ")";
                    strbuild = new StringBuilder();
                    strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:100%;table-layout:fixed;'><tr><td>   ");
                    strbuild.Append("</td></tr>");
                    strbuild.Append("<tr><td style='height:20px;'></td></tr>");
                    strbuild.Append("<tr><td>Dear " + hdnPRM_Name.Value + ",</td></tr>");
                    strbuild.Append("<tr><td style='height:10px;'></td></tr>");
                    strbuild.Append("<tr><td> This is to inform you that " + employeeName + " has created Customer Service Request with following details. Please take appropriate action</td></tr>");
                    strbuild.Append("<tr><td style='height:20px;'></td></tr>");
                    strbuild.Append("<tr><td>");
                    strbuild.Append("<table style='width:80% !important;color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;table-layout:fixed;'>");
                    strbuild.Append("<tr><td width='25%'>Project/ Location Code : </td><td width='75%'> " + txtProjectName.Text + "</td></tr>");
                    strbuild.Append("<tr><td width='25%'>Service Request No : </td><td width='75%'>" + serviceRequestno + "</td></tr>");
                    strbuild.Append("<tr><td width='25%'>Created By : </td><td width='75%'>" + employeeName + "</td></tr>");
                    strbuild.Append("<tr><td width='25%'>Created On : </td><td width='75%'> " + createdDate + "</td></tr>");
                    strbuild.Append("<tr><td width='25%'>Service Request Description :</td><td width='75%' style='text-align:left !important;'>" + requestDescription + "</td></tr>");
                    strbuild.Append("</table>");
                    strbuild.Append("</td></tr>");
                    strbuild.Append("<tr><td style='height:20px'></td></tr>");
                    strbuild.Append("<tr><td style='height:20px'><a href='" + redirectURL + "' target='_blank'>Please click here to take action on Service Request</a></td></tr>");
                    strbuild.Append("<tr><td style='height:20px'></td></tr>");
                    strbuild.Append("<tr><td style='height:20px'>This is system generated mail, please do not reply.</td></tr>");
                    strbuild.Append("</table>");
                    //spm.sendMail(toMailIDs, strsubject, Convert.ToString(strbuild).Trim(), "", ccmailid);
                    spm.Custs_sendMailServiceRequest(toMailIDs, strsubject, Convert.ToString(strbuild).Trim(), "",Convert.ToString(dslist.Tables[3].Rows[0]["Emp_Emailaddress"]));
                    #endregion


                    #region create & Send Email Customer Service acknowledgement
                    StringBuilder strbuildAcknowledgment = new StringBuilder();

                    var strsubjectAcknowledgment = "CustomerFIRST :- Acknowledgment of Your :- " + serviceRequestno + "";

                    strbuildAcknowledgment = new StringBuilder();
                    strbuildAcknowledgment.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:100%;table-layout:fixed;'><tr><td>   ");
                    strbuildAcknowledgment.Append("</td></tr>");
                    strbuildAcknowledgment.Append("<tr><td style='height:20px;'></td></tr>");
                    strbuildAcknowledgment.Append("<tr><td>Dear " + hdncustName.Value + ",</td></tr>");
                    strbuildAcknowledgment.Append("<tr><td style='height:10px;'></td></tr>");
                    strbuildAcknowledgment.Append("<tr><td> Thank you for " + serviceRequestno + " We have successfully received your " + serviceRequestno + " and wanted to confirm that we are working on it.</td></tr>");
                    strbuildAcknowledgment.Append("<tr><td style='height:20px;'></td></tr>");
                    strbuildAcknowledgment.Append("<tr><td style='height:20px'>This is system generated mail, please do not reply.</td></tr>");
                    strbuildAcknowledgment.Append("</table>");
                    spm.Custs_sendMailServiceRequest(hdnCustEmailaddress.Value, strsubjectAcknowledgment, Convert.ToString(strbuildAcknowledgment).Trim(), "", ccmailid);
                    #endregion
                }

                //End
                lblmessage.Visible = true;
                lblmessage.Text = "Customer Service Request Submitted Successfully";
                Response.Redirect("~/procs/Custs_MyServiceReq.aspx");
            }
        }
        catch (Exception ex)
        {
            lblmessage.Visible = true;
            lblmessage.Text = ex.Message.ToString();
            return;
        }

    }

    #endregion

    #region Page Method

    public string ReplaceInvalidChars(string filename)
    {
        Regex illegalInFileName = new Regex(@"[#%&{}\!/<'>@+*?$|=]");
        string myString = illegalInFileName.Replace(filename, "_");
        //return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        return myString;
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


    
    public void getPMPRMDetails()
    {
        try
        {

            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getPMPRMDetails";

            spars[1] = new SqlParameter("@Custcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

            DataSet dslist = spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");
            if (dslist != null)
            {
                if (dslist.Tables[0].Rows.Count > 0)
                {
                    txtProjectName.Text = Convert.ToString(dslist.Tables[0].Rows[0]["Location_name"]).Trim();
                    hdnComp_Code.Value = Convert.ToString(dslist.Tables[0].Rows[0]["comp_code"]).Trim();
                    hdnPM_Code.Value = Convert.ToString(dslist.Tables[0].Rows[0]["PM_Code"]).Trim();
                    hdnPM_Name.Value = Convert.ToString(dslist.Tables[0].Rows[0]["PM_Name"]).Trim();
                    hdnPM_Emailaddress.Value = Convert.ToString(dslist.Tables[0].Rows[0]["PM_Email"]).Trim();

                    hdnPRM_Code.Value = Convert.ToString(dslist.Tables[0].Rows[0]["PRM_Code"]).Trim();
                    hdnPRM_Name.Value = Convert.ToString(dslist.Tables[0].Rows[0]["PRM_Name"]).Trim();
                    hdnPRM_Emailaddress.Value = Convert.ToString(dslist.Tables[0].Rows[0]["PRM_Email"]).Trim();

                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    #endregion



    #region old Code
    #region PageEvents

    //Use Create Sevice Request


    //Use Create Sevice Request
    public void loadDropDownDepartment()
    {
        DataTable dtleaveInbox = new DataTable();
        dtleaveInbox = spm.GetSerivesRequestDepartment();
        ddlDepartment.DataSource = dtleaveInbox;
        //ddlDepartment.DataBind();
        ddlDepartment.DataTextField = "DepartmentName";
        ddlDepartment.DataValueField = "DepartmentId";
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem("Select Department", "0")); //updated code



    }


    public void GetEmployeeDetails()
    {
        try
        {
            DataTable dtEmpDetails = new DataTable();
            dtEmpDetails = spm.GetEmployeeData(txtEmpCode.Text);
            if (dtEmpDetails.Rows.Count > 0)
            {
                txtEmpName.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
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

            throw;
        }
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

    //protected void lnkuploadRcpt_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ClaimsDocumentpath"]).Trim()), lnkuploadRcpt.Text);

    //        Response.ContentType = ContentType;
    //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
    //        Response.WriteFile(strfilepath);
    //        Response.End();
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message.ToString());
    //    }
    //}
    #endregion

    #region PageMethods



    public void GetMobileEligibility_New()
    {

        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.GetMobilelEligibilityMatrix_Emp_Grade(Convert.ToString(hflGrade.Value), Convert.ToString(txtEmpCode.Text), Convert.ToString(1));
        //dtApproverEmailIds = spm.GetMobilelEligibilityMatrix(Convert.ToString(hdnGrade.Value));

        if (dtApproverEmailIds.Rows.Count <= 0)
        {

            mobile_btnSave.Visible = false;
            mobile_cancel.Visible = false;
            lblmessage.Text = "Sorry You are not entitled for mobile claims!";
        }
        if (dtApproverEmailIds.Rows.Count > 0)
        {
            if (Convert.ToString(dtApproverEmailIds.Rows[0]["Eligibility"]).Trim() != "")
            {
                if (Convert.ToDecimal(dtApproverEmailIds.Rows[0]["Eligibility"]) <= 0)
                {

                    mobile_btnSave.Visible = false;
                    mobile_cancel.Visible = false;
                    lblmessage.Text = "Sorry You are not entitled for mobile claims!";
                }
            }
        }

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

    public void getMobRemlsDetails_usingRemid()
    {
        try
        {
            DataSet dtTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_getMainTravelRequest_forMobile";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Int);
            spars[1].Value = hdnRemid.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;
            //dtTrDetails = spm.getMobileRemDataList(spars, "[SP_GETALLreembursement_DETAILS]");
            dtTrDetails = spm.getDatasetList(spars, "[SP_GETALLreembursement_DETAILS]");
            if (dtTrDetails.Tables[0].Rows.Count > 0)
            {
               
                //txtAmountTot.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["TotalAmount_Claimed"]);
                // txtDeviation.Text = Convert.ToString(dtTrDetails.Rows[0]["Reason"]);
                // txt_Approved_Amount.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["HOD_recm_Amt"]);
                hdnMobRemStatusM.Value = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Ren_Status"]);
                hdnTravelConditionid.Value = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Rem_Conditionid"]);

                // mobile_btnPrintPV.Visible = true;
                if (dtTrDetails.Tables[1].Rows.Count > 0)
                {
                    for (Int32 irow = 0; irow < dtTrDetails.Tables[1].Rows.Count; irow++)
                    {
                        if (Convert.ToString(dtTrDetails.Tables[1].Rows[irow]["Action"]).Trim() == "Approved")
                        {
                            hdnMobRemStatus_dtls.Value = Convert.ToString(dtTrDetails.Tables[1].Rows[irow]["Action"]).Trim();
                        }
                        if (Convert.ToString(dtTrDetails.Tables[1].Rows[irow]["Action"]).Trim() == "Pending" && Convert.ToString(dtTrDetails.Tables[1].Rows[irow]["Appr_id"]).Trim() == "107")
                        {
                            //Comment mobile_btnPrintPV.Visible = true;
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    }

    protected string GetApprove_RejectList(decimal dmaxremid)
    {
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        //dtAppRej = spm.GetMobileApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
        //dtAppRej = spm.GeTfuelApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), dmaxremid);
        //if (dtAppRej.Rows.Count > 0)
        //{
        //    sbapp.Append("<table>");
        //    for (int i = 0; i < dtAppRej.Rows.Count; i++)
        //    {
        //        sbapp.Append("<tr>");
        //        sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
        //        sbapp.Append("</tr>");
        //    }
        //    sbapp.Append("</table>");
        //}




        return Convert.ToString(sbapp);
    }

    public void getTravle_Desk_COS_ApproverCode()
    {

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_ACCCOS_apprver_code_Rem";

        spars[1] = new SqlParameter("@trappr", SqlDbType.VarChar);
        //spars[1].Value ="RCOS";
        spars[1].Value = "RCFO";

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = txtEmpCode.Text;

        dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

        //ACC Approver Code
        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            hdnApprovalCOS_Code.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_code"]).Trim();
            hdnApprovalCOS_ID.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_id"]).Trim();
            hdnApprovalCOS_mail.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();
            hdnApprovalCOS_Name.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_remarks"]).Trim();

        }

    }

   

   
    #endregion

    #region Reimbursement ModifyMethods
    //private void getApproverlist(string strempcodes, string reqid,Convert.ToInt32(hdn))
    //{
    //    DataTable dtapprover = new DataTable();
    //    dtapprover = spm.GetApproverStatus(, reqid, leavecondtiontypeid);
    //    lstApprover.Items.Clear();
    //    if (dtapprover.Rows.Count > 0)
    //    {
    //        lstApprover.DataSource = dtapprover;
    //        lstApprover.DataTextField = "names";
    //        lstApprover.DataValueField = "names";
    //        lstApprover.DataBind();

    //    }
    //    else
    //    {
    //        lblmessage.Text = "There is no request for approver.";
    //    }
    //}
    #endregion




    protected void txtFromdate_N_TextChanged(object sender, EventArgs e)
    {
        try
        {
 
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    }
    protected void lstBilltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txtBilltype.Text = lstBilltype.SelectedItem.Text;
        //PopupControlExtender2.Commit(lstBilltype.SelectedItem.Text);
        lblmessage.Text = "";
        hdnBilltype.Value = "";
        //  hdnBilltype.Value = lstBilltype.SelectedValue;


        ////if (Convert.ToString(txtTodate_N.Text).Trim() != "" && Convert.ToString(txtBilltype.Text).Trim() != "")
        ////{
        ////    checkPastMoths_AlreadySubmits();
        ////    GetMobileEligibility();
        ////}
    }

    
    public void BindCategoryDDL(string departmentId)
    {
        try
        {

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

    #endregion
}
