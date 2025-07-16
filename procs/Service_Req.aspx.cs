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
using System.Collections.Generic;


public partial class Service_Req : System.Web.UI.Page
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
                    Txt_Service_Desription.Attributes.Add("maxlength", "500");
                    loadDropDownDepartment();
                    //
                    Txt_DateCreate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:MM:ss");

                    editform.Visible = true;
                    divbtn.Visible = false;
                    //divmsg.Visible = false;
                    mobile_cancel.Visible = false;
                    btnTra_Details.Visible = false;
                    // txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtEmpCode.Text = Convert.ToString(Session["Empcode"]);
                    // txtAmount.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    hdnTravelConditionid.Value = "2";
                    hdnRemid.Value = "0";
                    hdnClaimid.Value = "0";
                    GetEmployeeDetails();

                    txtFromdateold.Text = DateTime.Today.ToString("dd/MM/yyyy").Replace("-", "/");
                    txtFromdateold.Enabled = false;
                    AssigningSessions();
                    hdnGrade.Value = Convert.ToString(Session["Grade"]);
                    //txtFromdateold.Text = ;
                    if (Request.QueryString.Count > 0)
                    {
                        hdnClaimid.Value = "1"; // Convert.ToString(Request.QueryString[0]).Trim(); 
                        hdnRemid.Value = Convert.ToString(Request.QueryString[1]).Trim();

                    }
                    if (Convert.ToString(hdnRemid.Value).Trim() != "0" && Convert.ToString(hdnRemid.Value).Trim() != "")
                    {
                        mobile_cancel.Visible = true;
                        //getMobRemlsDetails_usingRemid();

                        if (Request.QueryString.Count > 2)
                        {
                            //InsertMobileRem_DatatoTempTables_trvl();
                        }
                    }
                    /*by Highbartech on 11-06-2020*/
                    //getMobileClaimDetails();
                    // getClaimDetails();
                   

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";


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
    //Use Create Sevice Request
    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";
        string strfromDateN = "";
        string strtoDateN = "";
        string filename = "";
        String strfileName = "";

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
                lblmessage.Text = "Please select category.";
                return;
            }
            if (Convert.ToString(Txt_Service_Desription.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Service Descripation.";
                return;
            }

            lblmessage.Text = "";


            if (uploadfile.HasFile)
            {
                filename = uploadfile.FileName;
            }

            //if (Convert.ToString(lnkuplodedfile.Text).Trim() == "")
            //{
            //    if (Convert.ToString(filename).Trim() == "")
            //    {
            //        lblmessage.Text = "Please upload file to Service Request !";
            //        return;
            //    }
            //}



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
            
            dtSPOC = spm.GetSPOCData(id);
            if (dtSPOC.Rows.Count > 0)
            {
                // txtEmpName.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
                AssigneTo = Convert.ToString(dtSPOC.Rows[0]["SPOC_ID"]);

            }

            hdnsptype.Value = "Insert";

            var getId= spm.InsertServiceRequestDetails(txtEmpCode.Text, Txt_Service_Desription.Text, hdnsptype.Value, filename, AssigneTo, '\0',id, 1, 1, Convert.ToString(Session["Empcode"]).Trim(), '\0', idCategory);
            //var getId = 7;
            //Send Email To SPOC
            var serviceRequestDetail = spm.GetServiceRequestById(Convert.ToInt32(getId));// Get Inserted Service Requested Details
            if(serviceRequestDetail.Rows.Count>0)
            {
                
                string serviceRequestno = Convert.ToString(serviceRequestDetail.Rows[0]["ServicesRequestID"]);
                string toMailIDs = "";
                string employeeName = Session["emp_loginName"].ToString();
                string createdDate = Convert.ToDateTime(serviceRequestDetail.Rows[0]["CreatedDate"]).ToString("dd-MM-yyyy HH:mm:ss");
                string department = ddlDepartment.SelectedItem.ToString().Trim();
                string ccmailid = "";
                //View URL 
                string redirectURL = Convert.ToString(ConfigurationManager.AppSettings["Service_Req_App"]).Trim() + "?id=" + getId + "&type=app";

                string requestDescription = Txt_Service_Desription.Text.Trim();
                DataTable dtEmpDetails = new DataTable();
                dtEmpDetails = spm.GetEmployeeData(AssigneTo);

                if (dtEmpDetails.Rows.Count > 0)
                {
                    toMailIDs = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
                }
                spm.ServiceRequest_send_mailto_SPOC(serviceRequestno, toMailIDs, employeeName, createdDate, department, "", redirectURL, requestDescription);
            }
            
            //End
            lblmessage.Visible = true;
            lblmessage.Text = "Service Request Submitted Successfully";
            Response.Redirect("~/procs/Service.aspx");
        }
        catch (Exception ex)
        {
            lblmessage.Visible = true;
            lblmessage.Text = ex.Message.ToString();
            return;
        }

    }

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
    public void AssigningSessions()
    {

        Session["Fromdate"] = txtFromdateold.Text;
        Session["ReqEmpCode"] = txtEmpCode.Text;
        Session["Grade"] = hflGrade.Value;
        Session["TrDays"] = hdnTrdays.Value;

        //Response.Write(Convert.ToString(Session["Fromdate"]));
        //Response.End();

    }
    
    
    
    protected void btnTra_Details_Click(object sender, EventArgs e)
    {
        // By Highbartech on 10-06-2020
        //if (Convert.ToString(txtFromdateold.Text).Trim() == "")
        //{
        //    lblmessage.Text = "Please enter submission date";
        //}
        //AssigningSessions();
        //Response.Redirect("~/procs/MobileClaim.aspx?clmid=0&rem_id="+hdnRemid.Value);

        string[] strdate;
        string strfromDate = "";
        string filename = "";
        String strfileName = "";
        string rfilename = "";
        String strRfileName = "";

        //if (Convert.ToString(txtAmount.Text).Trim() == "")
        //{
        //    lblmessage.Text = "Please enter claim amount";
        //    return;
        //}

        //if (Convert.ToString(txtAmount.Text).Trim() != "")
        //{
        //    strdate = Convert.ToString(txtAmount.Text).Trim().Split('.');
        //    if (strdate.Length > 2)
        //    {
        //        txtAmount.Text = "0";
        //        lblmessage.Text = "Please enter correct amount.";
        //        return;
        //    }

        //    Decimal dfare = 0;
        //    dfare = Convert.ToDecimal(txtAmount.Text);
        //    if (dfare == 0)
        //    {
        //        lblmessage.Text = "Please enter correct amount.";
        //        return;
        //    }
        //}

        if (uploadfile.HasFile)
        {
            filename = uploadfile.FileName;
        }

        if (Convert.ToString(lnkuplodedfile.Text).Trim() == "")
        {
            if (Convert.ToString(filename).Trim() == "")
            {
                lblmessage.Text = "Please upload Bill to Claim!";
                return;
            }
        }

        //if (uploadRcpt.HasFile)
        //{
        //    rfilename = uploadRcpt.FileName;
        //}

        //if (Convert.ToString(lnkuploadRcpt.Text).Trim() == "")
        //{
        //    if (Convert.ToString(rfilename).Trim() == "")
        //    {
        //        lblmessage.Text = "Please upload Receipt to claim mobile Reimbursement";
        //        return;
        //    }
        //}

        decimal eligamount = 0, enteredamount = 0;
        /*by Highbartech on 11-06-2020*/
        //if (Convert.ToString(txtAmount.Text).Trim() != "") //&& Convert.ToString(txtElgAmnt.Text).Trim() != "")
        //{
        //    //eligamount = Convert.ToDecimal(txtElgAmnt.Text);
        //    enteredamount = Math.Round(Convert.ToDecimal(txtAmount.Text), 2);
        //}
        //if (enteredamount > eligamount)
        //{
        //    //txtRemark.Text = "Deviation";
        //    txtRemark.Text = "Yes";
        //}
        //else
        //{
        //    //txtRemark.Text = "Eligible";
        //    txtRemark.Text = "No";
        //}
        /*by Highbartech on 11-06-2020*/
        #region date formatting
        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[0]) + "_" + Convert.ToString(strdate[1]);

        }
        #endregion

        if (Convert.ToString(filename).Trim() != "")
        {
            filename = uploadfile.FileName;
            strfileName = "";
            strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + Convert.ToString("Mobile_Bill").Trim() + Path.GetExtension(uploadfile.FileName);
            filename = strfileName;
            uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ClaimsDocumentpath"]).Trim()), strfileName));
        }

        //if (Convert.ToString(rfilename).Trim() != "")
        //{
        //    rfilename = uploadRcpt.FileName;
        //    strRfileName = "";
        //    strRfileName = txtEmpCode.Text + "_Receipt_" + strfromDate + "_" + Convert.ToString("Mobile_Bill").Trim() + Path.GetExtension(rfilename);
        //    rfilename = strRfileName;
        //    uploadRcpt.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ClaimsDocumentpath"]).Trim()), strRfileName));
        //}

        hdnsptype.Value = "InsertTempTable";
        if (Convert.ToString(hdnClaimid.Value).Trim() != "" && Convert.ToString(hdnClaimid.Value).Trim() != "0")
            hdnsptype.Value = "updateTempTable";

        spm.InsertMobileClaimDetails(Convert.ToInt32(hdnRemid.Value), txtTodate_N.Text, enteredamount, txtEmpCode.Text, Convert.ToDecimal(0), hdnsptype.Value, "Yes", filename, hdnClaimid.Value, rfilename, "", Txt_BillNo.Text.ToString(), "");

        //Response.Redirect("~/procs/Mobile_Req.aspx?clmid=" + hdnClaimid.Value + "&rem_id=" + hdnRemid.Value);
        // By highbartech on 10-06-2020

    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        AssigningSessions();
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnClaimid.Value = Convert.ToString(dgMobileClaim.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnRemid.Value = Convert.ToString(dgMobileClaim.DataKeys[row.RowIndex].Values[1]).Trim();

        Response.Redirect("~/procs/MobileClaim.aspx?clmid=" + hdnClaimid.Value + "&remid=" + hdnRemid.Value);
    }

    protected void mobile_cancel_Click(object sender, EventArgs e)
    {

        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }

        string strclaim_month = "";
        string[] strdate;
        string strfromDate = "";

        //if (dgMobileClaim.Rows.Count > 0)
        //{
        //strfromDate = Convert.ToString(dgMobileClaim.Rows[0].Cells[0].Text).Trim();
        strfromDate = Convert.ToString(txtFromdate.Text).Trim();

        if (Convert.ToString(strfromDate).Trim() != "")
        {
            strdate = Convert.ToString(strfromDate).Trim().Split('/');
            strclaim_month = Convert.ToString(strdate[0]) + "-" + Convert.ToString(strdate[1]);
        }
        //}

        hdnEligible.Value = "Cancellation";
        string strapprovermails = "";
        getTravle_Desk_COS_ApproverCode();
        strapprovermails = GetApprove_RejectList(Convert.ToDecimal(hdnRemid.Value));

        spm.InsertMobileClaimDetails(Convert.ToInt32(hdnRemid.Value), "", 0, txtEmpCode.Text, 0, Convert.ToString("CancelMobRem"), "", "", "", "", "", "", "");
       // spm.Fuel_send_mail_Cancel(txtEmpName.Text, hdnApprEmailaddress.Value, "Request for " + "" + " bill - " + Convert.ToString(hdnvouno.Value), "", txtAmount.Text, GetApprove_RejectList(Convert.ToDecimal(hdnRemid.Value)), txtEmpName.Text, "", "", "", strclaim_month);
        Response.Redirect("~/procs/Mobile.aspx");
    }

    protected void dgMobileClaim_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            if (Convert.ToString(hdnMobRemStatusM.Value).Trim() == "2" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "3" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "4")
            {
                e.Row.Cells[4].Visible = false;
            }
            else
            {
                e.Row.Cells[4].Visible = true;
            }
            if (Convert.ToString(hdnMobRemStatus_dtls.Value).Trim() == "Approved")
            {
                e.Row.Cells[4].Visible = false;
            }

        }
    }

    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    txtAmount.Text = "";
        //    if (Convert.ToString(txtFromdate.Text).Trim() != "")
        //    {
        //        checkPastMoths_AlreadySubmits();
        //        GetMobileEligibility();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Response.Write(ex.Message.ToString());

        //}
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
            btnTra_Details.Visible = false;
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
                    btnTra_Details.Visible = false;
                    mobile_btnSave.Visible = false;
                    mobile_cancel.Visible = false;
                    lblmessage.Text = "Sorry You are not entitled for mobile claims!";
                }
            }
        }

    }


    public void GetMobileEligibility()
    {
        string[] strdate;
        string strfromDate = "";

        if (Convert.ToString(txtTodate_N.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtTodate_N.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);

            DataTable dtApproverEmailIds = new DataTable();
            dtApproverEmailIds = spm.GetMobilelEligibilityMatrix_Emp_Grade(Convert.ToString(hdnGrade.Value), Convert.ToString(txtEmpCode.Text), Convert.ToString(strfromDate));
            //dtApproverEmailIds = spm.GetMobilelEligibilityMatrix(Convert.ToString(hdnGrade.Value));

            if (dtApproverEmailIds.Rows.Count > 0)
            {
                //txtElgAmnt.Text = Convert.ToString(dtApproverEmailIds.Rows[0]["Eligibility"]);
                //txtElgAmnt.Enabled = false;
                if (Convert.ToString(dtApproverEmailIds.Rows[0]["view_mobile"]).Trim() == "N")
                {
                    mobile_btnSave.Enabled = false;
                    mobile_btnSave.Visible = false;
                    uploadfile.Enabled = false;
                    //uploadRcpt.Enabled = false;
                    lblmessage.Text = "Sorry Due to location change, You are not entitled for mobile claims!";
                  //  txtAmount.Enabled = false;
                  //  txtAmount.Text = "";
                    //txtElgAmnt.Text = "";
                }

            }
            else
            {
                //lblmessage.Visible = true;
                mobile_btnSave.Enabled = false;
                uploadfile.Enabled = false;
                //uploadRcpt.Enabled = false;
                lblmessage.Text = "Sorry You are not entitled for mobile claims!";
              //  txtAmount.Text = "";
                //txtElgAmnt.Text = "";
            }
        }

    }

    public void getMobileClaimDetails()
    {
        DataTable dtMobileDetails = new DataTable();
        dtMobileDetails = spm.GetMobileClaimDetails_Reqstpage(txtEmpCode.Text);

        dgMobileClaim.DataSource = null;
        dgMobileClaim.DataBind();

        if (dtMobileDetails.Rows.Count > 0)
        {
            btnTra_Details.Visible = false;
            dgMobileClaim.DataSource = dtMobileDetails;
            dgMobileClaim.DataBind();

            #region Calulate Total Claim Amount
            //txtAmountTot.Text = "0";
            //txtAmountTot.Enabled = false;
            double dtotclaimAmt = 0, dttotalEligibility = 0, comapringamount = 1.5;
            for (Int32 irow = 0; irow < dgMobileClaim.Rows.Count; irow++)
            {
                if (Convert.ToString(dgMobileClaim.Rows[irow].Cells[1].Text).Trim() != "")
                    dtotclaimAmt += Convert.ToDouble(dgMobileClaim.Rows[irow].Cells[1].Text);

                if (Convert.ToString(dgMobileClaim.Rows[irow].Cells[2].Text).Trim() != "")
                    dttotalEligibility += Convert.ToDouble(dgMobileClaim.Rows[irow].Cells[2].Text);
            }

            if (dtotclaimAmt <= dttotalEligibility)
            {
                hdnTravelConditionid.Value = "1";
            }
            else
            {
                if (dtotclaimAmt < dttotalEligibility * comapringamount)
                {
                    hdnTravelConditionid.Value = "2";
                }
                else if (dtotclaimAmt >= dttotalEligibility * comapringamount)
                {
                    hdnTravelConditionid.Value = "3";
                }
            }
            //txtAmountTot.Text = Convert.ToString(dtotclaimAmt);
            #endregion
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
                txtFromdateold.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["created_on"]);
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

    public void checkPastMoths_AlreadySubmits()
    {
        try
        {
            lblmessage.Text = "";
            #region date formatting

            string[] strdate;
            string strfromDate = "";
            string strToDate = "";


            if (Convert.ToString(Session["Fromdate"]).Trim() != "")
            {
                strdate = Convert.ToString(Session["Fromdate"]).Trim().Split('/');
                strToDate = Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]) + "/" + Convert.ToString(strdate[2]);
            }

            if (Convert.ToString(txtTodate_N.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtTodate_N.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            #endregion

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@sMonth", SqlDbType.VarChar);
            spars[0].Value = strToDate;

            spars[1] = new SqlParameter("@sclaimdate", SqlDbType.VarChar);
            spars[1].Value = strfromDate;

            spars[2] = new SqlParameter("@Empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            spars[3] = new SqlParameter("@Billtype", SqlDbType.VarChar);
            spars[3].Value = "";

            dsTrDetails = spm.getDatasetList(spars, "sp_check_mobile_rem_validation");

           // txtAmount.Enabled = true;
            btnTra_Details.Visible = false;
            //if (Convert.ToString(hdnclaimid.Value).Trim() != "0")
            //{
            //    accmo_delete_btn.Visible = true;
            //}
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["msg"]) != "")
                {
                  //  txtAmount.Enabled = false;
                   // txtAmount.Text = "";
                    txtFromdate_N.Text = "";
                    txtTodate_N.Text = "";
                    lblmessage.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["msg"]);
                    btnTra_Details.Visible = false;
                    //if (Convert.ToString(hdnclaimid.Value).Trim() != "0")
                    //{
                    //    accmo_delete_btn.Visible = false;
                    //}

                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }

    }

    private void getClaimDetails()
    {

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "getMobclaimdetails_edit";

        spars[1] = new SqlParameter("@claimsid", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(hdnClaimid.Value);

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = Convert.ToString(txtEmpCode.Text);

        spars[3] = new SqlParameter("@rem_id", SqlDbType.VarChar);
        spars[3].Value = Convert.ToString(hdnRemid.Value);

        dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            txtFromdate.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Rem_Month"]).Trim();
           // txtAmount.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Amount"]).Trim();
            Txt_BillNo.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Billno"]).Trim();
            // txtBilltype.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["BillType"]).Trim();
            // lstBilltype.SelectedValue = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["BillType"]).Trim();
            hdnBilltype.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["BillType"]).Trim();
            lblheading.Text = "Mobile bill Voucher - " + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Vouno"]);
            hdnvouno.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Vouno"]);
            //Txt_ProjectName.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Project_Name"]).Trim();
            //Txt_DeptName.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Dept_Name"]).Trim();
            txtFromdate_N.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["From_date"]).Trim();
            txtTodate_N.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["To_date"]).Trim();
            //txtElgAmnt.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Eligible_Amt"]).Trim();
            //txtRemark.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Remarks"]).Trim();
            lnkuplodedfile.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["UploadFile"]).Trim();
            //lnkuploadRcpt.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["rcptFile"]).Trim();
            // txtReason.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Reason"]).Trim();
            //if (Txt_DeptName.Text.ToString() == "")
            //    Txt_DeptName.Enabled = false;
            //else
            //    Txt_DeptName.Enabled = true;
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
    

    [System.Web.Services.WebMethod]
    public static List<string> SearchProject(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";
                strsql = "SELECT distinct Location_name FROM tbl_hmst_company_Location " +
                           "   where Location_name like '%' + @SearchText + '%' order by Location_name asc";

                cmd.CommandText = strsql;
                cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));

                cmd.Connection = conn;
                conn.Open();
                List<string> employees = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        employees.Add(sdr["Location_name"].ToString());
                    }
                }
                conn.Close();
                return employees;
            }
        }
    }

    [System.Web.Services.WebMethod]
    public static List<string> SearchDepartment(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";
                strsql = "SELECT Department_Name FROM tblDepartmentMaster " +
                           "   where Department_Name like '%' + @SearchText + '%' order by Department_Name asc";

                cmd.CommandText = strsql;
                cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));

                cmd.Connection = conn;
                conn.Open();
                List<string> employees = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        employees.Add(sdr["Department_Name"].ToString());
                    }
                }
                conn.Close();
                return employees;
            }
        }
    }
    protected void txtFromdate_N_TextChanged(object sender, EventArgs e)
    {
        try
        {

            DateTime endDate = DateTime.ParseExact(txtFromdate_N.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            Int64 addedDays = 29;
            endDate = endDate.AddDays(addedDays);
            DateTime end = endDate;
            txtTodate_N.Text = end.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            //txtAmount.Text = "";
            //if (Convert.ToString(txtTodate_N.Text).Trim() != "" && Convert.ToString(txtBilltype.Text).Trim() != "")
            //{
            //    checkPastMoths_AlreadySubmits();
            //    GetMobileEligibility();
            //}
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

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList list = (DropDownList)sender;
            string value = (string)list.SelectedValue;
            if (value == "0")
            {
                ddlCategory.Items.Clear();
                ddlCategory.DataSource = null;
                ddlCategory.DataBind();
            }
            else
            {
                BindCategoryDDL(value);
                
            }
        }
        catch (Exception ex)
        {

        }
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

}
