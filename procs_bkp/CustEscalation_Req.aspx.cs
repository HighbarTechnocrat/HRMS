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


public partial class CustEscalation_Req : System.Web.UI.Page
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
					loadCustMode();
					loadImpactProject();
					loadSeverity();
					loadRolePerson();
					loadCustSatisfaction();
					Get_Incident_Number();
					Txt_DateCreate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:MM:ss");

                    editform.Visible = true;
                    divbtn.Visible = false;                   
                    mobile_cancel.Visible = false;
                                    
                    txtEmpCode.Text = Convert.ToString(Session["Empcode"]);                 
                    hdnTravelConditionid.Value = "2";
                    hdnRemid.Value = "0";
                    hdnClaimid.Value = "0";
                    GetEmployeeDetails();

                    AssigningSessions();
                    hdnGrade.Value = Convert.ToString(Session["Grade"]);
                    //txtFromdateold.Text = ;
                    if (Request.QueryString.Count > 0)
                    {
                        hdnClaimid.Value = "1";  
                        hdnRemid.Value = Convert.ToString(Request.QueryString[1]).Trim();

                    }
                    if (Convert.ToString(hdnRemid.Value).Trim() != "0" && Convert.ToString(hdnRemid.Value).Trim() != "")
                    {
                        mobile_cancel.Visible = true;
                        if (Request.QueryString.Count > 2)
                        {
                           
                        }
                    }                                     
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
                strfileName = "CE_" + txtEmpCode.Text + "_" + strfromDate + Path.GetExtension(uploadfile.FileName);
                filename = strfileName;
                uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["CustomerFristPathpath"]).Trim()), strfileName));
            }
            //Assgine SPOC User
            var AssigneTo = "";
            DataTable dtSPOC = new DataTable();
			if (txtEscalationBy.Text.Trim()== "")
			{
				lblmessage.Text = "Please Enter Escalation Raised By.";
				return;
			}
			if (txtEmailID.Text.Trim() == "")
			{
				lblmessage.Text = "Please Enter Email ID";
				return;
			}

			if (txtEmailID.Text.Trim() != "")
			{
				string email = txtEmailID.Text.Trim();
				Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
				Match match = regex.Match(email);
				if (!match.Success)
				{
					lblmessage.Text = "Please Enter Valid Email ID";
					return;
				}
			}
			var id= Convert.ToString(ddlDepartment.SelectedValue.ToString().Trim());
            if (id == "0")
            {
                lblmessage.Text = "Please Select Project Name.";
                return;
            }
            var idCategory = Convert.ToInt32(ddlCustMode.SelectedValue.ToString().Trim());
            if (idCategory == 0)
            {
                lblmessage.Text = "Please Select Mode.";
                return;
            }
			var idRolePerson = Convert.ToInt32(ddlRolePerson.SelectedValue.ToString().Trim());
			if (idRolePerson == 0)
			{
				lblmessage.Text = "Please Select Role of the person.";
				return;
			}
			var idCustSatisfaction = Convert.ToInt32(ddlCustSatisfaction.SelectedValue.ToString().Trim());
			if (idCustSatisfaction == 0)
			{
				lblmessage.Text = "Please Select Customer Satisfaction Index.";
				return;
			}
			var idSeverity = Convert.ToInt32(ddlSeverity.SelectedValue.ToString().Trim());
			if (idSeverity == 0)
			{
				lblmessage.Text = "Please Select Severity.";
				return;
			}
			var IDImpactProject = Convert.ToInt32(ddlImpactProject.SelectedValue.ToString().Trim());
			if (IDImpactProject == 0)
			{
				lblmessage.Text = "Please Select Impact on Project.";
				return;
			}
			
			if (Convert.ToString(Txt_Service_Desription.Text).Trim() == "")
			{
				lblmessage.Text = "Please Enter Incident Description.";
				return;
			}
			dtSPOC = spm.GetCustEscalationSPOCData(id);
            if (dtSPOC.Rows.Count > 0)
            {               
                AssigneTo = Convert.ToString(dtSPOC.Rows[0]["SPOC_ID"]);
            }

            hdnsptype.Value = "Insert";

            var getId= spm.InsertCustEscalationDetails(txtEmpCode.Text, Txt_Service_Desription.Text, hdnsptype.Value, filename, AssigneTo, "",id, 1, 1, Convert.ToString(Session["Empcode"]).Trim(), "", idCategory, idRolePerson, idCustSatisfaction, idSeverity, IDImpactProject,txtEmailID.Text.Trim(),txtEscalationBy.Text.Trim());
            //var getId = 7;
            //Send Email To SPOC
            var serviceRequestDetail = spm.GetCustEscalationById(Convert.ToInt32(getId));// Get Inserted Service Requested Details
            if(serviceRequestDetail.Rows.Count>0)
            {
                
                string serviceRequestno = Convert.ToString(serviceRequestDetail.Rows[0]["ServicesRequestID"]);
                string toMailIDs = "";
                string employeeName = Session["emp_loginName"].ToString();
                string createdDate = Convert.ToDateTime(serviceRequestDetail.Rows[0]["CreatedDate"]).ToString("dd-MM-yyyy HH:mm:ss");
                string department = ddlDepartment.SelectedItem.ToString().Trim();
                string ccmailid = "";
                //View URL 
                string redirectURL = Convert.ToString(ConfigurationManager.AppSettings["Customer_Req_App"]).Trim() + "?id=" + getId + "&type=app";

                string requestDescription = Txt_Service_Desription.Text.Trim();
                DataTable dtEmpDetails = new DataTable();
                dtEmpDetails = spm.GetEmployeeData(AssigneTo);

                if (dtEmpDetails.Rows.Count > 0)
                {
                    toMailIDs = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
                }
                spm.CustEscalation_send_mailto_SPOC(serviceRequestno, toMailIDs, employeeName, createdDate, department, "", redirectURL, requestDescription,txtEscalationBy.Text.Trim(),txtEmailID.Text.Trim(),ddlCustMode.SelectedItem.Text,ddlCustSatisfaction.SelectedItem.Text,ddlSeverity.SelectedItem.Text,ddlRolePerson.SelectedItem.Text);
            }
          
            //End
            lblmessage.Visible = true;
            lblmessage.Text = "Service Request Submitted Successfully";
            Response.Redirect("~/procs/CustEscalation.aspx");
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
        dtleaveInbox = spm.GetCustEscalationDepartment("DDLDEPARTMENT");
        ddlDepartment.DataSource = dtleaveInbox;      
        ddlDepartment.DataTextField = "DepartmentName";
        ddlDepartment.DataValueField = "DepartmentId";
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem("Select Project", "0")); //updated code
    }
	public void loadCustMode()
	{
		DataTable dtleaveInbox = new DataTable();
		dtleaveInbox = spm.GetCustEscalationDepartment("DDLCUSTMODE");
		ddlCustMode.DataSource = dtleaveInbox;
		ddlCustMode.DataTextField = "ModeName";
		ddlCustMode.DataValueField = "ModeID";
		ddlCustMode.DataBind();
		ddlCustMode.Items.Insert(0, new ListItem("Select Mode", "0")); //updated code
	}
	public void loadRolePerson()
	{
		DataTable dtleaveInbox = new DataTable();
		dtleaveInbox = spm.GetCustEscalationDepartment("DDLROLEPERSON");
		ddlRolePerson.DataSource = dtleaveInbox;
		ddlRolePerson.DataTextField = "CustRole_Name";
		ddlRolePerson.DataValueField = "CustRole_ID";
		ddlRolePerson.DataBind();
		ddlRolePerson.Items.Insert(0, new ListItem("Select Role Person", "0")); //updated code
	}
	public void loadSeverity()
	{
		DataTable dtleaveInbox = new DataTable();
		dtleaveInbox = spm.GetCustEscalationDepartment("DDLSEVERITY");
		ddlSeverity.DataSource = dtleaveInbox;
		ddlSeverity.DataTextField = "Severity_Name";
		ddlSeverity.DataValueField = "Severity_ID";
		ddlSeverity.DataBind();
		ddlSeverity.Items.Insert(0, new ListItem("Select Severity", "0")); //updated code
	}
	public void loadImpactProject()
	{
		DataTable dtleaveInbox = new DataTable();
		dtleaveInbox = spm.GetCustEscalationDepartment("DDLIMPACTPROJECT");
		ddlImpactProject.DataSource = dtleaveInbox;
		ddlImpactProject.DataTextField = "Cust_Impact_Name";
		ddlImpactProject.DataValueField = "Cust_Impact_ID";
		ddlImpactProject.DataBind();
		ddlImpactProject.Items.Insert(0, new ListItem("Select Impact Project", "0")); //updated code
	}

	public void loadCustSatisfaction()
	{
		DataTable dtleaveInbox = new DataTable();
		dtleaveInbox = spm.GetCustEscalationDepartment("DDLCUSTSATISFACTION");
		ddlCustSatisfaction.DataSource = dtleaveInbox;
		ddlCustSatisfaction.DataTextField = "Cust_Satisfaction_Name";
		ddlCustSatisfaction.DataValueField = "Cust_Satisfaction_ID";
		ddlCustSatisfaction.DataBind();
		ddlCustSatisfaction.Items.Insert(0, new ListItem("Select Cust Satisfaction Index", "0")); //updated code
	}

	public void Get_Incident_Number()
	{
		try
		{
			DataTable dtEmpDetails = new DataTable();
			dtEmpDetails = spm.GetCustEscalationDepartment("ServiceNumber");
			if (dtEmpDetails.Rows.Count > 0)
			{
				//txtIncidentNo.Text = Convert.ToString(dtEmpDetails.Rows[0]["ServicesRequestNo"]);
								
			}
			//  getApproverdata();  
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());

			throw;
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

        //Session["Fromdate"] = txtFromdateold.Text;
        Session["ReqEmpCode"] = txtEmpCode.Text;
        Session["Grade"] = hflGrade.Value;
        Session["TrDays"] = hdnTrdays.Value;
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

   
    #endregion

    #region PageMethods


    
    public void GetMobileEligibility_New()
    {

        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.GetMobilelEligibilityMatrix_Emp_Grade(Convert.ToString(hflGrade.Value), Convert.ToString(txtEmpCode.Text), Convert.ToString(1));
        //dtApproverEmailIds = spm.GetMobilelEligibilityMatrix(Convert.ToString(hdnGrade.Value));

        if (dtApproverEmailIds.Rows.Count <= 0)
        {
            //btnTra_Details.Visible = false;
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
                   // btnTra_Details.Visible = false;
                    mobile_btnSave.Visible = false;
                    mobile_cancel.Visible = false;
                    lblmessage.Text = "Sorry You are not entitled for mobile claims!";
                }
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
           // btnTra_Details.Visible = false;
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
                //txtFromdateold.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["created_on"]);
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
        dtAppRej = spm.GeTfuelApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), dmaxremid);
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

  
}
