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


public partial class Mobile_Req : System.Web.UI.Page
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

            mobile_btnPrintPV.Visible = false;
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    divbtn.Visible = false;
                    //divmsg.Visible = false;
                    mobile_cancel.Visible = false;
                    btnTra_Details.Visible = false;
                    txtBilltype.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtFromdate_N.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtEmpCode.Text = Convert.ToString(Session["Empcode"]);
                    txtAmount.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    hdnTravelConditionid.Value = "2";
                    hdnRemid.Value = "0";
                    hdnClaimid.Value = "0";
                    GetEmployeeDetails();
                    GetCompany_Location();
                    GetDepartMentList();
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
                        getMobRemlsDetails_usingRemid();

                        if (Request.QueryString.Count >2)
                        {
                            InsertMobileRem_DatatoTempTables_trvl();
                        }
                    }
                    /*by Highbartech on 11-06-2020*/
                    //getMobileClaimDetails();
                    getClaimDetails();
                    /*by Highbartech on 11-06-2020*/
                   // getApproverdata();
                    if (Convert.ToString(hdnMobRemStatusM.Value).Trim() == "2" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "3" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "4" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "5")
                    {
                        mobile_btnSave.Visible = false;
                        mobile_cancel.Visible = false;
                        Spn_HOD_Recm_Amt.Visible = false;
                        txt_Approved_Amount.Visible = false;
                        btnTra_Details.Visible = false;
                       // dgMobileClaim.Enabled = false;
                        mobile_btnPrintPV.Visible = false;
                    }
                    if (Convert.ToString(hdnMobRemStatus_dtls.Value).Trim() == "Approved")
                    {
                        mobile_btnSave.Visible = false;
                        mobile_cancel.Visible = false;
                        btnTra_Details.Visible = false;
                        mobile_btnPrintPV.Visible = true;
                    }
                    //getMobileClaimDetails();
                    if (Convert.ToString(hdnMobRemStatusM.Value).Trim() == "5")
                    {
                        mobile_btnSave.Visible = true;
                    }
                    if (Convert.ToString(hdnMobRemStatusM.Value).Trim() == "2")
                    {
                        Spn_HOD_Recm_Amt.Visible = true;
                        txt_Approved_Amount.Visible = true;
                    }

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
                else
                {
                    if (Convert.ToString(txtAmount.Text).Trim() != "")
                    {
                        string[] strdate;
                        strdate = Convert.ToString(txtAmount.Text).Trim().Split('.');
                        if (strdate.Length > 2)
                        {
                            txtAmount.Text = "0";
                            lblmessage.Text = "Please enter correct amount.";
                            return;
                        }

                        Decimal dfare = 0;
                        dfare = Convert.ToDecimal(txtAmount.Text);
                        if (dfare == 0)
                        {
                            lblmessage.Text = "Please enter correct amount.";
                            return;
                        }
                    }

                    /*by Highbartech on 11-06-2020*/
                    if (Convert.ToString(txtAmount.Text).Trim() != "")// && Convert.ToString(txtElgAmnt.Text).Trim() != "")
                    {
                        decimal enteredamount = 0;
                        enteredamount = Convert.ToDecimal(txtAmount.Text);
                    }
                    
                    //if (Convert.ToString(txtAmount.Text).Trim() != "" && Convert.ToString(txtElgAmnt.Text).Trim() != "")
                    //{
                    //    txtAmountTot.Text = txtAmount.Text;
                    //    decimal eligamount = 0, enteredamount = 0;
                    //    eligamount = Convert.ToDecimal(txtElgAmnt.Text);
                    //    enteredamount = Convert.ToDecimal(txtAmount.Text);

                    //    if (enteredamount > eligamount)
                    //    {
                    //        //txtRemark.Text = "Deviation";
                    //        txtRemark.Text = "Yes";
                    //    }
                    //    else
                    //    {
                    //        //txtRemark.Text = "Eligible";
                    //        txtRemark.Text = "No";
                    //    }
                    //}
                    /*by Highbartech on 11-06-2020*/
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    public void GetCompany_Location()
    {
        DataTable lstPosition = new DataTable();
        lstPosition = spm.getMobileVoucherProject_List();
        if (lstPosition.Rows.Count > 0)
        {
            ddl_ProjectName.DataSource = lstPosition;
            ddl_ProjectName.DataTextField = "Location_name";
            ddl_ProjectName.DataValueField = "comp_code";
            ddl_ProjectName.DataBind();
            ddl_ProjectName.Items.Insert(0, new ListItem("Select  Project", "0"));
        }
    }
    public void GetDepartMentList()
    {
        DataTable lstPosition = new DataTable();
        lstPosition = spm.getPaymentVoucherDepartment_List();
        if (lstPosition.Rows.Count > 0)
        {
            ddl_DeptName.DataSource = lstPosition;
            ddl_DeptName.DataTextField = "Department_Name";
            ddl_DeptName.DataValueField = "Department_id";
            ddl_DeptName.DataBind();
            ddl_DeptName.Items.Insert(0, new ListItem("Select  Department", "0"));
        }
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";
        string strfromDateN = "";
        string strtoDateN = "";
        string filename = "";
        String strfileName = "";

        String[] stremp;
        stremp = Convert.ToString(ddl_ProjectName.SelectedItem.Text).Split('/');

        //if (Convert.ToString(ddl_ProjectName.SelectedItem.Text).Trim() == "0" || Convert.ToString(ddl_ProjectName.SelectedItem.Text).Trim() == "")
        //if (ddl_ProjectName.SelectedIndex == 0 || Convert.ToString(ddl_ProjectName.SelectedItem.Text).Trim() == "")
        //if (Convert.ToString(ddl_ProjectName.SelectedValue) == "0" || Convert.ToString(ddl_ProjectName.SelectedItem.Text).Trim() == "")
        //{
        //    lblmessage.Text = "Please select Project Name";
        //    return;
        //}
        if(Convert.ToString(ddl_ProjectName.SelectedValue) == "0" || Convert.ToString(ddl_ProjectName.SelectedItem.Text).Trim() == "")
        {
            lblmessage.Text = "Please select Project Name";
            return;
        }
        else
        {
            //if (Convert.ToString(Txt_ProjectName.Text).Trim() == "Head Office")
            if (Convert.ToString(ddl_ProjectName.SelectedItem.Text).Contains("Head Office"))
            {
                if (ddl_DeptName.SelectedIndex == 0  || Convert.ToString(ddl_DeptName.SelectedValue).Trim() == "")
                {
                    lblmessage.Text = "Please select Department Name";
                    return;
                }
            }
        }
        if (Convert.ToString(txtBilltype.Text).Trim() == "")
        {
            lblmessage.Text = "Please select Bill Type";
            return;
        }

        if (Convert.ToString(txtAmount.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Bill Amount.";
            return;
        }
        if (Convert.ToString(txtAmount.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtAmount.Text).Trim().Split('.');
            if (strdate.Length > 2)
            {
                txtAmount.Text = "0";
                lblmessage.Text = "Please enter correct amount.";
                return;
            }

            Decimal dfare = 0;
            dfare = Convert.ToDecimal(txtAmount.Text);
            if (dfare == 0)
            {
                lblmessage.Text = "Please enter correct amount.";
                return;
            }
        }
        //if (dgMobileClaim.Rows.Count <= 0)
        //{
        //    lblmessage.Text = "Please enter Calim details.";
        //    return;
        //}

        getTravle_Desk_COS_ApproverCode();

        lblmessage.Text = "";

        if (Convert.ToString(txtFromdateold.Text).Trim() == "")
        {
            lblmessage.Text = "Please select From Date";
            return;
        }

        
        if (Convert.ToString(txtFromdate_N.Text).Trim() == "")
        {
            lblmessage.Text = "Please select From Date";
            return;
        }
        if (Convert.ToString(txtTodate_N.Text).Trim() == "")
        {
            lblmessage.Text = "Please select To Date";
            return;
        }
        
        if (Convert.ToString(Txt_BillNo.Text).Trim() == "")
        {
            lblmessage.Text = "Please Enter Bill / Receipt no.";
            return;
        }
        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }
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

        return;

        decimal eligamount = 0, enteredamount = 0;
        /*by Highbartech on 11-06-2020*/
        if (Convert.ToString(txtAmount.Text).Trim() != "")
        {
            enteredamount = Math.Round(Convert.ToDecimal(txtAmount.Text), 2);
        }
        
        //if (Convert.ToString(txtAmount.Text).Trim() != "" && Convert.ToString(txtElgAmnt.Text).Trim() != "")
        //{
        //    eligamount = Convert.ToDecimal(txtElgAmnt.Text);
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
        string strclaim_month = "";
        if (Convert.ToString(txtTodate_N.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtTodate_N.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[1]) + "_" + Convert.ToString(strdate[2]);
            strclaim_month = Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[2]);
        }
        #endregion

        if (Convert.ToString(filename).Trim() != "")
        {
            filename = uploadfile.FileName;
            strfileName = "";
            strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + txtBilltype.Text.ToString() + Convert.ToString("_Bill").Trim() + Path.GetExtension(uploadfile.FileName);
            filename = strfileName;
            uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ClaimsDocumentpath"]).Trim()), strfileName));
        }
        if (Convert.ToString(txtFromdate_N.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate_N.Text).Trim().Split('/');
            strfromDateN = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(txtTodate_N.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtTodate_N.Text).Trim().Split('/');
            strtoDateN = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        //if (Convert.ToString(rfilename).Trim() != "")
        //{
        //    rfilename = uploadRcpt.FileName;
        //    strRfileName = "";
        //    strRfileName = txtEmpCode.Text + "_Receipt_" + strfromDate + "_" + Convert.ToString("Mobile_Bill").Trim() + Path.GetExtension(rfilename);
        //    rfilename = strRfileName;
        //    uploadRcpt.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ClaimsDocumentpath"]).Trim()), strRfileName));
        //}

        #region Check Duplicate Claim on Emp_Code, Expense Type, Bill Date, Bill no
        hdnsptype.Value = "checkDuplicate";

        DataTable dt = spm.CheckDuplicateMobileClaimDetails(Convert.ToInt32(hdnRemid.Value), strtoDateN, enteredamount, txtEmpCode.Text, Convert.ToDecimal(0), hdnsptype.Value, "Yes", filename, hdnClaimid.Value, "", txtReason.Text, Txt_BillNo.Text.ToString(), txtBilltype.Text.ToString().Trim());
        if (dt.Rows.Count > 0)
        {
            //lblmessage.Text = "Sorry you cannot claim this bill as the claim with same Bill no. already claimed by you";
            lblmessage.Text = "Sorry you cannot claim this bill as the claim with same Bill no. was already claimed by you";

            txtFromdate_N.Text = "";
            txtTodate_N.Text = "";
            return;
        }
        #endregion

        hdnsptype.Value = "InsertTempTable";
        if (Convert.ToString(hdnClaimid.Value).Trim() != "" && Convert.ToString(hdnClaimid.Value).Trim() != "0")
            hdnsptype.Value = "updateTempTable";

        spm.InsertMobileClaimDetails(Convert.ToInt32(hdnRemid.Value), strtoDateN, enteredamount, txtEmpCode.Text, Convert.ToDecimal(0), hdnsptype.Value, "Yes", filename, hdnClaimid.Value, "", txtReason.Text, Txt_BillNo.Text.ToString(), txtBilltype.Text.ToString().Trim());

        if (Convert.ToString(txtFromdateold.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdateold.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        DataTable dtMaxRempID = new DataTable();
        int status = 1;
        int maxRemid = 0;

        dtMaxRempID = spm.InsertMobileRembursement(strfromDate, Convert.ToDecimal(txtAmount.Text), "", txtEmpCode.Text, status, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToString(hdnRemid.Value), Convert.ToString(stremp[1].Trim()), ddl_DeptName.SelectedValue, strfromDateN, strtoDateN);
        maxRemid = Convert.ToInt32(dtMaxRempID.Rows[0]["maxRemid"]);

        if (maxRemid == 0)
            return;

        hdnRemid.Value = Convert.ToString(maxRemid);
        //if (dgMobileClaim.Rows.Count > 0)
        //{
            spm.InsertMobileClaimDetails(maxRemid, "", 0, txtEmpCode.Text, 0, "InsertMainTable", "", "", "", "", "","","");
        //}

        String strmobeRemURL = "";
        //strmobeRemURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_MobRem"]).Trim() + "?rem_id=" + hdnRemid.Value + "&mngexp=0&inbtype=RCOS";

        if (DgvApprover.Rows.Count == 1)
            strmobeRemURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_MobRem"]).Trim() + "?rem_id=" + hdnRemid.Value + "&mngexp=0&inbtype=RACC";
        else
            strmobeRemURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_MobRem"]).Trim() + "?rem_id=" + hdnRemid.Value + "&mngexp=0&inbtype=APP";


        spm.InsertMobileApproverDetails(hflapprcode.Value, Convert.ToInt32(hdnApprId.Value), maxRemid, "");
        //spm.InsertMobileApproverDetails(hdnApprovalCOS_Code.Value, Convert.ToInt32(hdnApprovalCOS_ID.Value), maxRemid, "");
        //GetApprove_RejectList

        //if (dgMobileClaim.Rows.Count > 0)
        //{
        //    strfromDate = Convert.ToString(dgMobileClaim.Rows[0].Cells[0].Text).Trim();

        //    if (Convert.ToString(strfromDate).Trim() != "")
        //    {
        //        strdate = Convert.ToString(strfromDate).Trim().Split('/');
        //        strclaim_month = Convert.ToString(strdate[0]) + "-" + Convert.ToString(strdate[1]);
        //    }
        //}

        spm.Fuel_send_mailto_RM_Approver(txtEmpName.Text, hdnApprEmailaddress.Value, "Request for " + txtBilltype.Text.ToString() + " bill - " + Convert.ToString(hdnvouno.Value), "", txtAmount.Text, GetApprove_RejectList(Convert.ToDecimal(maxRemid)), txtEmpName.Text, "", strmobeRemURL, "", strclaim_month);

        lblmessage.Visible = true;
        lblmessage.Text = "Mobile Reimbursement Request Submitted Successfully";
        Response.Redirect("~/procs/Mobile.aspx");

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

        if (Convert.ToString(txtAmount.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter claim amount";
            return;
        }

        if (Convert.ToString(txtAmount.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtAmount.Text).Trim().Split('.');
            if (strdate.Length > 2)
            {
                txtAmount.Text = "0";
                lblmessage.Text = "Please enter correct amount.";
                return;
            }

            Decimal dfare = 0;
            dfare = Convert.ToDecimal(txtAmount.Text);
            if (dfare == 0)
            {
                lblmessage.Text = "Please enter correct amount.";
                return;
            }
        }

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
        if (Convert.ToString(txtAmount.Text).Trim() != "") //&& Convert.ToString(txtElgAmnt.Text).Trim() != "")
        {
            //eligamount = Convert.ToDecimal(txtElgAmnt.Text);
            enteredamount = Math.Round(Convert.ToDecimal(txtAmount.Text), 2);
        }
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

        spm.InsertMobileClaimDetails(Convert.ToInt32(hdnRemid.Value), txtTodate_N.Text, enteredamount, txtEmpCode.Text, Convert.ToDecimal(0), hdnsptype.Value, "Yes", filename, hdnClaimid.Value, rfilename, txtReason.Text, Txt_BillNo.Text.ToString(), txtBilltype.Text.ToString());

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

        spm.InsertMobileClaimDetails(Convert.ToInt32(hdnRemid.Value), "", 0, txtEmpCode.Text, 0, Convert.ToString("CancelMobRem"), "", "", "","","","","");
        spm.Fuel_send_mail_Cancel(txtEmpName.Text, hdnApprEmailaddress.Value, "Request for " + txtBilltype.Text.ToString() + " bill - " + Convert.ToString(hdnvouno.Value), "", txtAmount.Text, GetApprove_RejectList(Convert.ToDecimal(hdnRemid.Value)), txtEmpName.Text, "", "", "", strclaim_month);
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
                LocalReport ReportViewer2 = new LocalReport();
                ReportViewer2.ReportPath = Server.MapPath("~/procs/PaymentVoucher_New.rdlc"); // Server.MapPath(strpath);

                   // Create Report DataSource
                ReportDataSource rds = new ReportDataSource("dspaymentVoucher", dspaymentVoucher.Tables[0]);
                ReportDataSource rdsApprs = new ReportDataSource("dspaymentVoucher_Apprs", dspaymentVoucher_Apprs.Tables[0]);
                ReportDataSource rdsEmployeeInfo = new ReportDataSource("dsEmployeeInfo", dspaymentVoucher.Tables[1]);
                ReportDataSource rdsAmtInWords = new ReportDataSource("dsAmountInWords", dspaymentVoucher.Tables[2]);

                 

                ReportViewer2.DataSources.Add(rds);
                ReportViewer2.DataSources.Add(rdsApprs);
                ReportViewer2.DataSources.Add(rdsAmtInWords);
                ReportViewer2.DataSources.Add(rdsEmployeeInfo);
                

                #region Create payment Voucher PDF file
                Warning[] warnings;
                string[] streamIds;
                string contentType;
                string encoding;
                string extension;

                //Export the RDLC Report to Byte Array.
                byte[] bytes = ReportViewer2.Render("pdf", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                //Download the RDLC Report in Word, Excel, PDF and Image formats.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=paymentvoucher." + extension);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();

                #endregion


            }

        }
        catch (Exception ex)
        {
        }
    }

    private void getPayementVoucher_forPrint_Old()
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
                param[0] = new ReportParameter("pdocno", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["vouno"]));
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
    public void GetMobileEligibility_New()
    {

        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.GetMobilelEligibilityMatrix_Emp_Grade(Convert.ToString(hflGrade.Value), Convert.ToString(txtEmpCode.Text),Convert.ToString(1));
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
                if (Convert.ToDecimal(dtApproverEmailIds.Rows[0]["Eligibility"]) <=0)
                {
                    btnTra_Details.Visible = false;
                    mobile_btnSave.Visible = false;
                    mobile_cancel.Visible = false;
                    lblmessage.Text = "Sorry You are not entitled for mobile claims!";
                }
            }
        }
         
    }
    

    //public void GetMobileEligibility()
    //{

    //    DataTable dtApproverEmailIds = new DataTable();
    //    dtApproverEmailIds = spm.GetMobilelEligibilityMatrix(Convert.ToString(hflGrade.Value));

    //    if (dtApproverEmailIds.Rows.Count == 0)
    //    {
    //        btnTra_Details.Visible = false;
    //        mobile_btnSave.Visible = false;
    //        mobile_cancel.Visible = false;
    //        lblmessage.Text = "Sorry You are not entitled for mobile claims!";
    //    }
    //}

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
                    txtAmount.Enabled = false;
                    txtAmount.Text = "";
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
                txtAmount.Text = "";
                //txtElgAmnt.Text = "";
            }
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

        Session["Fromdate"] = txtFromdateold.Text;
        Session["ReqEmpCode"] = txtEmpCode.Text;
        Session["Grade"] = hflGrade.Value;  
        Session["TrDays"] = hdnTrdays.Value;

        //Response.Write(Convert.ToString(Session["Fromdate"]));
        //Response.End();

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
                txt_Approved_Amount.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["HOD_recm_Amt"]);
                hdnMobRemStatusM.Value = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Ren_Status"]);
                hdnTravelConditionid.Value = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Rem_Conditionid"]);

                mobile_btnPrintPV.Visible = true;
               if (dtTrDetails.Tables[1].Rows.Count>0)
                {
                   for(Int32 irow =0;irow<dtTrDetails.Tables[1].Rows.Count;irow++)
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
    public void getApproverdata()
    {
        var getcompSelectedText = ddl_ProjectName.SelectedItem.Text;
        var getcompSelectedval = ddl_ProjectName.SelectedItem.Value;
        var Dept_id = 0;
        if (getcompSelectedText.Contains("Head Office"))
        {
            Dept_id= Convert.ToInt32(ddl_DeptName.SelectedItem.Value);
            if(Dept_id == 0)
            {
                Dept_id = Convert.ToInt32(hdnDept_Id.Value);
            }
        }
        if(getcompSelectedval=="0")
        {
            getcompSelectedval =Convert.ToString(hdncomp_code.Value);
        }
        DataTable dtApproverEmailIds = new DataTable();
        if(Convert.ToString(hdnRemid.Value).Trim()=="")
        dtApproverEmailIds = spm.GeTfuelApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value),0,getcompSelectedval,Dept_id);
        else
            dtApproverEmailIds = spm.GeTfuelApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToDecimal(hdnRemid.Value), getcompSelectedval, Dept_id);

        dspaymentVoucher_Apprs.Tables.Add(dtApproverEmailIds);

        lstApprover.Items.Clear();
        if (dtApproverEmailIds.Rows.Count > 0)
        {
            hdnApprEmailaddress.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["Emp_Emailaddress"]);
            hdnApprId.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["APPR_ID"]);

            //lstApprover.DataSource = dtApproverEmailIds;
            //lstApprover.DataTextField = "names";
            //lstApprover.DataValueField = "APPR_ID";

            //lstApprover.DataBind();

            DgvApprover.DataSource = null;
            DgvApprover.DataBind();

            if (dtApproverEmailIds.Rows.Count > 0)
            {
                DgvApprover.DataSource = dtApproverEmailIds;
                DgvApprover.DataBind();
            }
            hflapprcode.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["approver_emp_code"]);
            //hflapprcode.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["A_EMP_CODE"]);
        }
        else
        {
            lblmessage.Text = "Since you are not assigned under any approver you can not apply for any type of Travel request, please contact HR";

        }
    }

    protected string GetApprove_RejectList(decimal dmaxremid)
    {
        var getcompSelectedText = ddl_ProjectName.SelectedItem.Text;
        var getcompSelectedval = ddl_ProjectName.SelectedItem.Value;
        var Dept_id = 0;
        if (getcompSelectedText.Contains("Head Office"))
        {
            Dept_id = Convert.ToInt32(ddl_DeptName.SelectedItem.Value);
        }
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        //dtAppRej = spm.GetMobileApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
        dtAppRej = spm.GeTfuelApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), dmaxremid,getcompSelectedval,Dept_id);
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

    public void checkPastMoths_AlreadySubmits()
    {
        try
        {
            lblmessage.Text = "";
            #region date formatting

            string[] strdate;
            string strfromDate = "";
            string strToDate = "";

            
            #region Comment by Sanjay on 11.07.2022 from date from session not required
            /*if (Convert.ToString(Session["Fromdate"]).Trim() != "")
            {
                strdate = Convert.ToString(Session["Fromdate"]).Trim().Split('/');
                strToDate = Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]) + "/" + Convert.ToString(strdate[2]);
            }
            if (Convert.ToString(txtTodate_N.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtTodate_N.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }*/
            #endregion

            if (Convert.ToString(txtFromdate_N.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate_N.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }


            if (Convert.ToString(txtTodate_N.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtTodate_N.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            #endregion

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            //spars[0] = new SqlParameter("@sMonth", SqlDbType.VarChar);
            //spars[0].Value = strToDate;

            //spars[1] = new SqlParameter("@sclaimdate", SqlDbType.VarChar);
            //spars[1].Value = strfromDate;

            spars[0] = new SqlParameter("@cFromdate", SqlDbType.VarChar);
            spars[0].Value = strfromDate;

            spars[1] = new SqlParameter("@cTodate", SqlDbType.VarChar);
            spars[1].Value = strToDate;

            spars[2] = new SqlParameter("@Empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            spars[3] = new SqlParameter("@Billtype", SqlDbType.VarChar);
            spars[3].Value = txtBilltype.Text;

            dsTrDetails = spm.getDatasetList(spars, "sp_check_mobile_rem_validation");

            txtAmount.Enabled = true;
            btnTra_Details.Visible = false;
            //if (Convert.ToString(hdnclaimid.Value).Trim() != "0")
            //{
            //    accmo_delete_btn.Visible = true;
            //}
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["msg"]) != "")
                {
                    txtAmount.Enabled = false;
                    txtAmount.Text = "";
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
            txtAmount.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Amount"]).Trim();
            Txt_BillNo.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Billno"]).Trim();
            txtBilltype.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["BillType"]).Trim();
            lstBilltype.SelectedValue = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["BillType"]).Trim();
            hdnBilltype.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["BillType"]).Trim();
            lblheading.Text = "Mobile bill Voucher - " + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Vouno"]);
            hdnvouno.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Vouno"]);
            //  ddl_ProjectName.SelectedItem.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Project_Name"]).Trim();
            ddl_ProjectName.SelectedValue = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["comp_Code"]).Trim();
            ddl_DeptName.SelectedValue = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Dept_Name"]).Trim();
            txtFromdate_N.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["From_date"]).Trim();
            txtTodate_N.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["To_date"]).Trim();
            //txtElgAmnt.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Eligible_Amt"]).Trim();
            //txtRemark.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Remarks"]).Trim();
            lnkuplodedfile.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["UploadFile"]).Trim();
            //lnkuploadRcpt.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["rcptFile"]).Trim();
            txtReason.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Reason"]).Trim();
            
            if (Convert.ToString(ddl_ProjectName.SelectedItem.Text).Contains("Head Office"))
                ddl_DeptName.Enabled = true;
            else
                ddl_DeptName.Enabled = false;

            hdncomp_code.Value= Convert.ToString(dsTrDetails.Tables[0].Rows[0]["comp_Code"]).Trim();
            hdnDept_Id.Value= Convert.ToString(dsTrDetails.Tables[0].Rows[0]["DeptId"]).Trim();

            getApproverdata();
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
    protected void mobile_btnPrintPV_Click(object sender, EventArgs e)
    {
        getApproverdata();
        getPayementVoucher_forPrint();
    }


    //protected void Txt_ProjectName_TextChanged(object sender, EventArgs e)
    //{
        
    //   try
    //    {
    //        //Check Is Project Existix Or Not
    //        if (!checkIsExistProject(Txt_ProjectName.Text))
    //        {
    //            Txt_ProjectName.Text = "";
    //            lblmessage.Text = "Please select project name in list only";
    //            return;
    //        }

          
    //    if (Convert.ToString(Txt_ProjectName.Text).Contains("Head Office"))
    //        Txt_DeptName.Enabled = true;
    //    else
    //        Txt_DeptName.Enabled = false;

    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}

    //[System.Web.Services.WebMethod]
    //public static List<string> SearchProject(string prefixText, int count)
    //{

    //    using (SqlConnection conn = new SqlConnection())
    //    {
    //        conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

    //        using (SqlCommand cmd = new SqlCommand())
    //        {
    //            string strsql = "";
    //            strsql = "SELECT distinct Location_name FROM tbl_hmst_company_Location " +
    //                       "   where Location_name like '%' + @SearchText + '%' order by Location_name asc";

    //            cmd.CommandText = strsql;
    //            cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
    //            cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));

    //            cmd.Connection = conn;
    //            conn.Open();
    //            List<string> employees = new List<string>();
    //            using (SqlDataReader sdr = cmd.ExecuteReader())
    //            {
    //                while (sdr.Read())
    //                {
    //                    employees.Add(sdr["Location_name"].ToString());
    //                }
    //            }
    //            conn.Close();
    //            return employees;
    //        }
    //    }
    //}

    //[System.Web.Services.WebMethod]
    //public static List<string> SearchDepartment(string prefixText, int count)
    //{

    //    using (SqlConnection conn = new SqlConnection())
    //    {
    //        conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

    //        using (SqlCommand cmd = new SqlCommand())
    //        {
    //            string strsql = "";
    //            strsql = "SELECT Department_Name FROM tblDepartmentMaster " +
    //                       "   where Department_Name like '%' + @SearchText + '%' order by Department_Name asc";

    //            cmd.CommandText = strsql;
    //            cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
    //            cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));

    //            cmd.Connection = conn;
    //            conn.Open();
    //            List<string> employees = new List<string>();
    //            using (SqlDataReader sdr = cmd.ExecuteReader())
    //            {
    //                while (sdr.Read())
    //                {
    //                    employees.Add(sdr["Department_Name"].ToString());
    //                }
    //            }
    //            conn.Close();
    //            return employees;
    //        }
    //    }
    //}
    protected void txtFromdate_N_TextChanged(object sender, EventArgs e)
    {
        try
        {

            DateTime endDate = DateTime.ParseExact(txtFromdate_N.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //comment by sanjay on 11.07.2022 
            //Int64 addedDays = 27;
            Int64 addedDays = 30;
            endDate = endDate.AddDays(addedDays);
            DateTime end = endDate;
            txtTodate_N.Text = end.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            //txtAmount.Text = "";
            if (Convert.ToString(txtTodate_N.Text).Trim() != "" && Convert.ToString(txtBilltype.Text).Trim() != "")
            {
               // checkPastMoths_AlreadySubmits();
              ///  GetMobileEligibility();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    }
    protected void lstBilltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtBilltype.Text = lstBilltype.SelectedItem.Text;
        PopupControlExtender2.Commit(lstBilltype.SelectedItem.Text);
        lblmessage.Text = "";
        hdnBilltype.Value = "";
        hdnBilltype.Value = lstBilltype.SelectedValue;


        ////if (Convert.ToString(txtTodate_N.Text).Trim() != "" && Convert.ToString(txtBilltype.Text).Trim() != "")
        ////{
        ////    checkPastMoths_AlreadySubmits();
        ////    GetMobileEligibility();
        ////}
    }
   public bool checkIsExistProject(string projectName)
    {
        bool blnchk = false;
        try
        {
           // var getProjectSplit = projectName.Split('/');
           // var getCode = "";


            var getLocation = projectName;
            if (getLocation=="")
            {                
                return false;
            }

            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CheckProjectExistOnClaim";

            spars[1] = new SqlParameter("@projectName", SqlDbType.VarChar);
            if (Convert.ToString(getLocation).Trim() != "")
                spars[1].Value = Convert.ToString(getLocation).Trim();

            DataTable dtcities = spm.getDataList(spars, "SP_TimesheetCheckProjectANDTask");

            if (dtcities != null)
            {
                if (dtcities.Rows.Count > 0)
                {
                    blnchk = true;
                }
            }
            return blnchk;
        }
        catch (Exception)
        {
            return blnchk;
        }
    }

    protected void ddl_ProjectName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //Check Is Project Existix Or Not
            String[] stremp;
            stremp = Convert.ToString(ddl_ProjectName.SelectedItem.Text).Split('/');

            if (!checkIsExistProject(stremp[1]))
            {
                ddl_ProjectName.SelectedValue = "0";
                lblmessage.Text = "Please select project name in list only";
                return;
            }

            //if (Txt_ProjectName.Text.Trim() == "Head Office")
            if (Convert.ToString(stremp[1]).Contains("Head Office"))
            {
                ddl_DeptName.Enabled = true;
            }
            else
            {
                ddl_DeptName.Enabled = false;
                getApproverdata();

            }             
            ddl_DeptName.SelectedValue = "0";
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void txtTodate_N_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txtTodate_N.Text).Trim() != "" && Convert.ToString(txtBilltype.Text).Trim() != "")
        {
            checkPastMoths_AlreadySubmits();
            GetMobileEligibility();
        }
    }

    protected void ddl_DeptName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(ddl_DeptName.SelectedIndex>0)
            {
                getApproverdata();
            }           
        }
        catch (Exception)
        {

            throw;
        }
    }
}
