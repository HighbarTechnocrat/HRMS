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

public partial class CustomerFirst_App : System.Web.UI.Page
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
            lblmessage.Text = "";
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
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    hdnempcode.Value = Convert.ToString(Session["Empcode"]);
                    GetEmployeeDetails();
                    editform.Visible = true;
                    divbtn.Visible = false;
                    divmsg.Visible = false;


                    if (Request.QueryString.Count > 0)
                    {
                        hdnRemid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdnRemid_Type.Value = Convert.ToString(Request.QueryString[1]).Trim();
                    }

                    if (Convert.ToString(hdnRemid.Value).Trim() != "")
                    {
                        if (Request.QueryString.Count > 0)
                        {
                            var getType = Convert.ToString(Request.QueryString[1]).Trim();

                            GetSurveyDetails(Convert.ToInt32(hdnRemid.Value));
                            backToArr.Visible = true;
                            var HODCode = "";
                            var PMCode = "";
                            //Get HOD AND PM Code
                            var departmentId = Convert.ToInt32(hdnDepartmentId.Value);
                            DataTable dtHOD = new DataTable();
                            dtHOD = spm.CheckCustomerFIRSTHOD(Convert.ToString(hdnempcode.Value));
                            if (dtHOD.Rows.Count > 0)
                            {
                                foreach (DataRow row in dtHOD.Rows)
                                {
                                    var dept = Convert.ToInt32(row["DeptId"]);
                                    if (departmentId == dept)
                                    {
                                        HODCode = Convert.ToString(row["Hod"]);
                                        PMCode = Convert.ToString(row["PMCode"]);
                                    }
                                }
                            }

                            DataTable dtCEOEMPCODE = new DataTable();
                            dtCEOEMPCODE = spm.GetCEOEmpCode();
                            if (dtCEOEMPCODE.Rows.Count > 0)
                            {
                                var loginCode = Convert.ToString(hdnempcode.Value);
                                var CeoEmpCode = Convert.ToString(dtCEOEMPCODE.Rows[0]["Emp_Code"]);
                                if (loginCode == CeoEmpCode)
                                {
                                    var pmComment = hdnPMComment.Value.ToString();
                                    var Status = hdnStatus.Value.ToString();
                                    if ((pmComment == "Auto escalated due to no action." && Status == "6") || (loginCode == "" && CeoEmpCode == ""))
                                    {
                                        libtn.Visible = false;
                                        litxtCommentForClient.Visible = false;
                                        litxtComment.Visible = false;
                                        backToArr.Visible = false;
                                        backToApp.Visible = false;

                                        editform.Visible = false;
                                        divbtn.Visible = false;
                                        divmsg.Visible = false;
                                        homeBtn.Visible = false;
                                        //var btn1 = Master.FindControl("MainContent_backToApp");
                                        //btn1.Visible = false;
                                        lblheading.Text = "CustomerFIRST - You don't have rights to view survey.";
                                    }

                                    else
                                    {
                                        lblCommentLble.InnerText = "CEO Comments";
                                        service_btnEscelateToCEO.Visible = true;
                                        service_btnEscelateToHOD.Visible = false;
                                        service_btnAssgine.Visible = false;
                                    }
                                }
                                else if (HODCode == PMCode)
                                {
                                    //Follwing condition added by manisha to Deactivate PM link once 15 Days Excallation is over
                                    var pmComment = hdnPMComment.Value.ToString();
                                    var Status = hdnStatus.Value.ToString();
                                    // if ((pmComment == "Auto escalated due to no action." && Status == "6") || (pmComment == "Auto escalated due to no action." && Status == "7"))
                                    if ((pmComment == "Auto escalated due to no action." && Status == "7") || (HODCode == "" && PMCode == ""))
                                    {
                                        libtn.Visible = false;
                                        litxtCommentForClient.Visible = false;
                                        litxtComment.Visible = false;
                                        backToArr.Visible = false;
                                        backToApp.Visible = false;

                                        editform.Visible = false;
                                        divbtn.Visible = false;
                                        divmsg.Visible = false;
                                        homeBtn.Visible = false;
                                        //var btn1 = Master.FindControl("MainContent_backToApp");
                                        //btn1.Visible = false;
                                        lblheading.Text = "CustomerFIRST - You don't have rights to view survey.";
                                    }


                                    //var pmComment = hdnPMComment.Value.ToString();
                                    //if(pmComment=="")
                                    //{
                                    //    lblCommentLble.InnerText = "Actor Comments";
                                    //    service_btnEscelateToCEO.Visible = false;
                                    //    service_btnEscelateToHOD.Visible = false;
                                    //    service_btnAssgine.Visible = true;
                                    //}
                                    else
                                    {
                                        lblCommentLble.InnerText = "Actor Comments";
                                        service_btnEscelateToCEO.Visible = false;
                                        service_btnEscelateToHOD.Visible = false;
                                        service_btnSendSPOC.Visible = true;
                                        service_btnAssgine.Visible = false;
                                    }
                                }
                                else if (loginCode == HODCode)
                                {
                                    //Added  for latest case
                                    var pmComment = hdnPMComment.Value.ToString();
                                    var Status = hdnStatus.Value.ToString();
                                    // if ((pmComment == "Auto escalated due to no action." && Status == "6") || (pmComment == "Auto escalated due to no action." && Status == "7"))
                                    if  (pmComment == "Auto escalated due to no action." && Status == "7")
                                    {
                                        libtn.Visible = false;
                                        litxtCommentForClient.Visible = false;
                                        litxtComment.Visible = false;
                                        backToArr.Visible = false;
                                        backToApp.Visible = false;

                                        editform.Visible = false;
                                        divbtn.Visible = false;
                                        divmsg.Visible = false;
                                        homeBtn.Visible = false;
                                        //Control btn1 = this.Master.FindControl("MainContent_backToApp");
                                        //btn1.Visible = false;
                                        lblheading.Text = "CustomerFIRST - You don't have rights to view survey.";
                                    }
                                    else
                                    {
                                        lblCommentLble.InnerText = "Actor Comments";
                                        service_btnEscelateToCEO.Visible = false;
                                        service_btnEscelateToHOD.Visible = true;
                                        service_btnAssgine.Visible = false;
                                   }
                                }
                                else if (loginCode == PMCode)
                                {
                                    var pmComment = hdnPMComment.Value.ToString();
                                    var Status = hdnStatus.Value.ToString();
                                    if ((pmComment == "Auto escalated due to no action." && Status == "6") || (pmComment == "Auto escalated due to no action." && Status == "7") || (loginCode == "" && PMCode == ""))
                                    {
                                        libtn.Visible = false;
                                        litxtCommentForClient.Visible = false;
                                        litxtComment.Visible = false;
                                        backToArr.Visible = false;
                                        backToApp.Visible = false;

                                        editform.Visible = false;
                                        divbtn.Visible = false;
                                        divmsg.Visible = false;
                                        homeBtn.Visible = false;
                                        //Control btn1 = this.Master.FindControl("MainContent_backToApp");
                                        //btn1.Visible = false;
                                        lblheading.Text = "CustomerFIRST - You don't have rights to view survey.";
                                    }
                                    else
                                    {
                                        lblCommentLble.InnerText = "Actor Comments";
                                        service_btnEscelateToCEO.Visible = false;
                                        service_btnEscelateToHOD.Visible = false;
                                        service_btnAssgine.Visible = true;
                                   }

                                }
                                else
                                {
                                    libtn.Visible = false;
                                    litxtCommentForClient.Visible = false;
                                    litxtComment.Visible = false;
                                    backToArr.Visible = true;
                                    backToApp.Visible = false;
                                    lblheading.Text = "CustomerFIRST - View Survey";
                                }

                                if (getType == "arr")
                                {
                                    libtn.Visible = false;
                                    litxtCommentForClient.Visible = false;
                                    litxtComment.Visible = false;
                                    backToArr.Visible = true;
                                    backToApp.Visible = false;
                                    lblheading.Text = "CustomerFIRST - View Survey";
                                }
                                else
                                {
                                    backToArr.Visible = false;
                                    backToApp.Visible = true;

                                }
                            }

                        }

                        this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                    }
                }
            }
        }

        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    #endregion

    #region PageMethods

    public void GetSurveyDetails(int id)
    {
        try
        {
            gvQuestionDetails.DataSource = null;
            gvQuestionDetails.DataBind();
            gvSurveyHistory.DataSource = null;
            gvSurveyHistory.DataBind();

            DataSet dtEmpDetails = new DataSet();
            dtEmpDetails = spm.GetSurveyDetails(id);
            if (dtEmpDetails.Tables[0].Rows.Count > 0)
            {
                var getSurveyDetail = dtEmpDetails.Tables[0];
                txtSurveyNo.Text = Convert.ToString(getSurveyDetail.Rows[0]["SurveyNo"]);
                txtClient.Text = Convert.ToString(getSurveyDetail.Rows[0]["ClientName"]);
                txtReplyDate.Text = Convert.ToString(getSurveyDetail.Rows[0]["ClientReplyDate"]);
                txtContactName.Text = Convert.ToString(getSurveyDetail.Rows[0]["ContactName"]);
                Txt_Designation.Text = Convert.ToString(getSurveyDetail.Rows[0]["ContactDesignation"]);
                txt_Customer_Description.Text = Convert.ToString(getSurveyDetail.Rows[0]["ClientComment"]);
                hdnContactEmail.Value = Convert.ToString(getSurveyDetail.Rows[0]["ContactEmail"]);
                hdnMailBody.Value = Convert.ToString(getSurveyDetail.Rows[0]["MailBody"]);
                hdnMailSubject.Value = Convert.ToString(getSurveyDetail.Rows[0]["MailSubject"]);
                hdnMailSubject.Value = Convert.ToString(getSurveyDetail.Rows[0]["MailSubject"]);
                hdnDepartmentId.Value = Convert.ToString(getSurveyDetail.Rows[0]["DepartmentId"]);
                hdnPMComment.Value = Convert.ToString(getSurveyDetail.Rows[0]["PMComment"]);
                hdnHODComment.Value = Convert.ToString(getSurveyDetail.Rows[0]["HODComment"]);
                hdnStatus.Value = Convert.ToString(getSurveyDetail.Rows[0]["Status"]);
            }
            if (dtEmpDetails.Tables[1].Rows.Count > 0)
            {
                var getAnswerdt = dtEmpDetails.Tables[1];
                lblQuestion.Value= Convert.ToString(getAnswerdt.Rows[0]["Question"]);
                gvQuestionDetails.DataSource = getAnswerdt;
                gvQuestionDetails.DataBind();
            }
            if (dtEmpDetails.Tables[2].Rows.Count > 0)
            {
                var getSurveydt = dtEmpDetails.Tables[2];
                gvSurveyHistory.DataSource = getSurveydt;
                gvSurveyHistory.DataBind();

                var getBindHistoryDataId= Convert.ToString(getSurveydt.Rows[getSurveydt.Rows.Count-1]["id"]);
                bindHistory(getBindHistoryDataId);

                //var rows2 = from row in getSurveydt.AsEnumerable()
                //            where row.Field<string>("StatusTitle").ToString().Trim() == "Call Back Incident - Opened"
                //            select row;
                //var rows3 = from row in getSurveydt.AsEnumerable()
                //            where row.Field<string>("StatusTitle").ToString().Trim() == "Customer Replied"
                //            select row;
                ////Customer Replied
                //int count2 = rows2.Count<DataRow>();
                //int count3 = rows3.Count<DataRow>();
                //if(count2==1)
                //{
                //    lblCommentLble.InnerText = "Delivery Head Comments";
                //    service_btnEscelateToCEO.Visible = false;
                //    service_btnAssgine.Visible = true;
                //}
                //else if(count2==2)
                //{
                //    lblCommentLble.InnerText = "CEO Comments";
                //    service_btnEscelateToCEO.Visible = true;
                //    service_btnAssgine.Visible = false;
                //}
                //else if(count2 > 3)
                //{
                //    lblCommentLble.InnerText = "Delivery Head Comments";
                //    service_btnEscelateToCEO.Visible = false;
                //    service_btnAssgine.Visible = true;
                //}

                //if(count3>0)
                //{
                //    libtn.Visible = false;
                //    litxtCommentForClient.Visible = false;
                //    litxtComment.Visible = false;
                //}
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();
            throw;
        }
    }

    public void GetEmployeeDetails()
    {
        try
        {
            DataTable dtEmpDetails = new DataTable();
            dtEmpDetails = spm.GetEmployeeData(Convert.ToString(hdnempcode.Value));
            if (dtEmpDetails.Rows.Count > 0)
            {
                hflEmpDesignation.Value = Convert.ToString(dtEmpDetails.Rows[0]["DesginationName"]);
                hflEmpDepartment.Value = Convert.ToString(dtEmpDetails.Rows[0]["Department_Name"]);
                hflGrade.Value = Convert.ToString(dtEmpDetails.Rows[0]["Grade"]).Trim();
                hflEmailAddress.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
                hflEmp_Name.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
            }
            //  getApproverdata();  
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

            throw;
        }
    }
    #endregion



    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            var id = Convert.ToInt32(hdnRemid.Value);
            var contactEmail = Convert.ToString(hdnContactEmail.Value);
            var question = Convert.ToString(lblQuestion.Value);
            var MailBody =hdnMailBody.Value.ToString();
            var MailSubject = hdnMailSubject.Value.ToString();

            if(txtComment.Text.Trim()=="")
            {
                lblmessage.Text = "Please Enter Delivery Program Manager Comments";
                return;
            }
            if (txtCommentForClient.Text.Trim() == "")
            {
                lblmessage.Text = "Please Enter For Client Comments";
                return;
            }
            var qtype = "UpdatePMFeedback";
            var comment = txtComment.Text.Trim();
            var remark= txtCommentForClient.Text.Trim();
            var updatedBy= Convert.ToString(hdnempcode.Value);
            var ClientName= Convert.ToString(txtContactName.Text.Trim());
            var loginEmpName= Convert.ToString(hflEmp_Name.Value);
            spm.updateHODCEOReplySurvey(qtype, id, remark, comment, updatedBy);
            spm.sendMailForCustomerFirstClient(contactEmail, id, remark, question, ClientName, loginEmpName, MailBody, MailSubject);
            spm.updateSendSurvey(id);
            Response.Redirect("~/procs/customerFirst.aspx");
            // GetSurveyDetails(id);
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return;
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    protected void service_btnEscelateToCEO_Click(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            var id = Convert.ToInt32(hdnRemid.Value);
            var contactEmail = Convert.ToString(hdnContactEmail.Value);
            var question = Convert.ToString(lblQuestion.Value);
            var MailBody = hdnMailBody.Value.ToString();
            var MailSubject = hdnMailSubject.Value.ToString();
            if (txtComment.Text.Trim() == "")
            {
                lblmessage.Text = "Please Enter Delivery Head Comments";
                return;
            }
            if (txtCommentForClient.Text.Trim() == "")
            {
                lblmessage.Text = "Please Enter For Client Comments";
                return;
            }
            var qtype = "UpdateCEOFeedback";
            var comment = txtComment.Text.Trim();
            var remark = txtCommentForClient.Text.Trim();
            var updatedBy = Convert.ToString(hdnempcode.Value);
            var ClientName = Convert.ToString(txtContactName.Text.Trim());
            var loginEmpName = Convert.ToString(hflEmp_Name.Value);
            spm.updateHODCEOReplySurvey(qtype, id, remark, comment, updatedBy);
            spm.sendMailForCustomerFirstClient(contactEmail, id, remark, question, ClientName, loginEmpName, MailBody, MailSubject);
            spm.updateSendSurvey(id);
            Response.Redirect("~/procs/customerFirst.aspx");
            //GetSurveyDetails(id);
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return;
        }
    }

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var id = Convert.ToString(gvSurveyHistory.DataKeys[row.RowIndex].Values[0]).Trim();
            bindHistory(id);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void bindHistory(string id)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            txtActionBy.Text = "";
            txtActionDate.Text = "";
            txtActionStatus.Text = "";
            txtCustomerCommntesHI.Text = "";
            txtActionComments.Text = "";
            txtCommentsForClientHi.Text = "";

           
            var getDetails = spm.GetHistorySurveyDetails(Convert.ToInt32(id));
            if (getDetails.Rows.Count > 0)
            {
                var getDate = Convert.ToString(getDetails.Rows[0]["ActionDate"].ToString());
                var getActionComments = Convert.ToString(getDetails.Rows[0]["ActionComments"].ToString());
                var getCommentsForClientContact = Convert.ToString(getDetails.Rows[0]["CommentsForClientContact"].ToString());
                var getClientComment = Convert.ToString(getDetails.Rows[0]["ClientComment"].ToString());
                var getActiondBY = Convert.ToString(getDetails.Rows[0]["ActionBy"].ToString());
                var getStatusName = Convert.ToString(getDetails.Rows[0]["ActionStatus"].ToString());

                txtActionBy.Text = getActiondBY;
                txtActionDate.Text = getDate;
                txtActionStatus.Text = getStatusName;
                txtCustomerCommntesHI.Text = getClientComment;
                txtActionComments.Text = getActionComments;
                txtCommentsForClientHi.Text = getCommentsForClientContact;

                var isClient = Convert.ToInt32(getDetails.Rows[0]["IsClient"].ToString());
                if (isClient == 1)
                {
                    GridView1.DataSource = getDetails;
                    GridView1.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }

    protected void service_btnEscelateToHOD_Click(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            var id = Convert.ToInt32(hdnRemid.Value);
            var contactEmail = Convert.ToString(hdnContactEmail.Value);
            var question = Convert.ToString(lblQuestion.Value);
            var MailBody = hdnMailBody.Value.ToString();
            var MailSubject = hdnMailSubject.Value.ToString();
            if (txtComment.Text.Trim() == "")
            {
                lblmessage.Text = "Please Enter Delivery Head Comments";
                return;
            }
            if (txtCommentForClient.Text.Trim() == "")
            {
                lblmessage.Text = "Please Enter For Client Comments";
                return;
            }
            var qtype = "UpdateHODFeedback";
            var comment = txtComment.Text.Trim();
            var remark = txtCommentForClient.Text.Trim();
            var updatedBy = Convert.ToString(hdnempcode.Value);
            var ClientName = Convert.ToString(txtContactName.Text.Trim());
            var loginEmpName = Convert.ToString(hflEmp_Name.Value);
            spm.updateHODCEOReplySurvey(qtype, id, remark, comment, updatedBy);
            spm.sendMailForCustomerFirstClient(contactEmail, id, remark, question, ClientName, loginEmpName,MailBody,MailSubject);
            spm.updateSendSurvey(id);
            Response.Redirect("~/procs/customerFirst.aspx");
            // GetSurveyDetails(id);
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
            lblmessage.Text = "";
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            var id = Convert.ToInt32(hdnRemid.Value);
            var contactEmail = Convert.ToString(hdnContactEmail.Value);
            var question = Convert.ToString(lblQuestion.Value);
            var MailBody = hdnMailBody.Value.ToString();
            var MailSubject = hdnMailSubject.Value.ToString();
            if (txtComment.Text.Trim() == "")
            {
                lblmessage.Text = "Please Enter Delivery Head Comments";
                return;
            }
            if (txtCommentForClient.Text.Trim() == "")
            {
                lblmessage.Text = "Please Enter For Client Comments";
                return;
            }
            var qtype = "UpdateHODPMFeedback";
            var comment = txtComment.Text.Trim();
            var remark = txtCommentForClient.Text.Trim();
            var updatedBy = Convert.ToString(hdnempcode.Value);
            var ClientName = Convert.ToString(txtContactName.Text.Trim());
            var loginEmpName = Convert.ToString(hflEmp_Name.Value);
            spm.updateHODCEOReplySurvey(qtype, id, remark, comment, updatedBy);
            spm.sendMailForCustomerFirstClient(contactEmail, id, remark, question, ClientName, loginEmpName, MailBody, MailSubject);
            spm.updateSendSurvey(id);
            Response.Redirect("~/procs/customerFirst.aspx");
            // GetSurveyDetails(id);
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
            return;
        }
    }
}
