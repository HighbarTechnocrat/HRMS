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

public partial class Payment_Req : System.Web.UI.Page
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

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Voucher");
            }
            else
            {
                Page.SmartNavigation = true;
                mobile_btnPrintPV.Visible = false;
                if (!Page.IsPostBack)
                {
                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PaymentVoucherDocumentpath"]).Trim());
                    editform.Visible = true;
                    divbtn.Visible = false;
                    //divmsg.Visible = false;
                    mobile_cancel.Visible = false;
                    btnTra_Details.Visible = true;
                    txtEmpCode.Text = Convert.ToString(Session["Empcode"]);
                    hdnTravelConditionid.Value = "1";
                    hdnRemid.Value = "0";
                    GetEmployeeDetails();
                    txtAmount.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    Txt_BillNo.Attributes.Add("onkeypress", "return ValidFilename(event);");
                    txtFromdateMain.Text = DateTime.Today.ToString("dd/MM/yyyy").Replace("-", "/");
                    txtFromdateMain.Enabled = false;
                   // GetTravelMode();
                    GetCompany_Location();
                    GetDepartMentList();
                    //txtFromdateMain.Text = ;
                    if (Request.QueryString.Count > 0)
                    {
                        hdnClaimid.Value = Convert.ToString(Request.QueryString[0]).Trim(); 
                        hdnRemid.Value = Convert.ToString(Request.QueryString[1]).Trim();                        
                        
                    }
                    if (Convert.ToString(hdnRemid.Value).Trim() != "0")
                    {
                        FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PaymentVoucherDocumentpath"]).Trim());
                    }
                    else
                    {
                        FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PaymentVoucherDocumentpath"]).Trim() + "paymentVoucher_temp/" + Convert.ToString(txtEmpCode.Text) + "/");
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
                    getMobileClaimDetails();
                  
                   
                    if (Convert.ToString(hdnMobRemStatusM.Value).Trim() == "2" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "3" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "4")
                    {
                        mobile_btnSave.Visible = false;
                        mobile_cancel.Visible = false;
                        btnTra_Details.Visible = false;
                       // dgMobileClaim.Enabled = false;
                        dgMobileClaim.Columns[5].Visible = false;
                        dgMobileClaim.Columns[6].Visible = false;

                    }
                    if (Convert.ToString(hdnMobRemStatus_dtls.Value).Trim() == "Approved")
                    {
                        mobile_btnSave.Visible = false;
                        mobile_cancel.Visible = false;
                        btnTra_Details.Visible = false;
                    }
                    //getMobileClaimDetails();
                    //GetMobileEligibility();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";

                    
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
        lstPosition = spm.getPaymentVoucherProject_List();
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

    protected void btnTra_Details_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Bill date";
            return;
        }
        AssigningSessions();
        //Response.Redirect("~/procs/PaymentClaim.aspx?clmid=0&rem_id=" + hdnRemid.Value);

        string[] strdate;
        string strfromDate = "";
        string filename = "";
        string rfilename = "";
        String strfileName = "";
        String strRfileName = "";

        if (Convert.ToString(lstTravelMode.SelectedValue).Trim() == "7")
        {
            if (Convert.ToString(txtReason.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Particulars.";
                txtReason.Focus();
                return;
            }
        }
        if (Convert.ToString(Txt_BillNo.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Bill / receipt No.";
            return;
        }

        if (Convert.ToString(txtAmount.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter bill amount";
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
        hdnclaimidO.Value = lstTravelMode.SelectedValue;
        ////if (Convert.ToString(hdnclaimqry.Value).Trim() != "" && Convert.ToString(hdnclaimqry.Value).Trim() != "0")
        ////{

        ////}
        ////else
        ////{
        ////    if (check_duplicates_Claims(Convert.ToInt32(hdnclaimidO.Value)) == true)
        ////    {
        ////        lblmessage.Text = "Please Check Bill for Selected Expense already sumitted.";
        ////        return;
        ////    }
        ////}
        if (uploadfile.HasFile)
        {
            filename = uploadfile.FileName;
        }

        if (Convert.ToString(lnkuplodedfile.Text).Trim() == "")
        {
            if (Convert.ToString(filename).Trim() == "")
            {
                lblmessage.Text = "Please upload Bill copy!";
                return;
            }
        }

        decimal eligamount = 0, enteredamount = 0;
        if (Convert.ToString(txtAmount.Text).Trim() != "")
        {
            enteredamount = Math.Round(Convert.ToDecimal(txtAmount.Text), 2);
        }

        #region date formatting
        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

        }
        #endregion

        #region Check Duplicate Claim on Emp_Code, Expense Type, Bill Date, Bill no
        hdnsptype.Value = "checkDuplicate";

        DataTable dt = spm.InsertPaymentVoucherDetails(Convert.ToInt32(hdnRemid.Value), strfromDate, enteredamount, txtEmpCode.Text, Convert.ToDecimal(0), hdnsptype.Value, hdnDeviation.Value, filename, hdnclaimidO.Value, txtReason.Text, Convert.ToString(hdnclaimqry.Value).Trim(), Txt_BillNo.Text.Trim());
        if (dt.Rows.Count > 0)
        {
            lblmessage.Text = "Sorry you cannot claim this bill as the claim with same Date & Bill no. already claimed by you";
            return;
        }
        #endregion

        hdnsptype.Value = "InsertTempTable";
        if (Convert.ToString(hdnclaimqry.Value).Trim() == "")
        {
            hdnclaimqry.Value = "0";
        }
        if (Convert.ToString(hdnclaimqry.Value).Trim() != "" && Convert.ToString(hdnclaimqry.Value).Trim() != "0")
            hdnsptype.Value = "updateTempTable";

        DataTable dtmaxClaimid = new DataTable();
        int maxClaimid = 0;
        dtmaxClaimid = spm.InsertPaymentVoucherDetails(Convert.ToInt32(hdnRemid.Value), strfromDate, enteredamount, txtEmpCode.Text, Convert.ToDecimal(0), hdnsptype.Value, hdnDeviation.Value, filename, hdnclaimidO.Value, txtReason.Text, Convert.ToString(hdnclaimqry.Value).Trim(), Txt_BillNo.Text.Trim());
        if (hdnclaimqry.Value == "0")
            maxClaimid = Convert.ToInt32(dtmaxClaimid.Rows[0]["maxclaimid"]);
        else
            maxClaimid = Convert.ToInt32(hdnclaimqry.Value);

        hdnclaimqry.Value = Convert.ToString(maxClaimid);
        #region insert or upload multiple files
        if (uploadfile.HasFile)
        {
            filename = uploadfile.FileName;
            if (Convert.ToString(filename).Trim() != "")
            {
                #region date formatting
                if (Convert.ToString(txtFromdate.Text).Trim() != "")
                {
                    strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                    strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
                }
                #endregion

                string FuelclaimPath = "";
                //if (Convert.ToString(hdnRemid.Value).Trim() != "0")
                //    FuelclaimPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PaymentVoucherDocumentpath"]).Trim());
                //else
                    FuelclaimPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PaymentVoucherDocumentpath"]).Trim() + "paymentVoucher_temp/" + Convert.ToString(txtEmpCode.Text) + "/");

                bool folderExists = Directory.Exists(FuelclaimPath);
                if (!folderExists)
                {
                    Directory.CreateDirectory(FuelclaimPath);
                }

                Boolean blnfile = false;
                HttpFileCollection fileCollection = Request.Files;
                for (int i = 0; i < fileCollection.Count; i++)
                {
                    strfileName = "";
                    HttpPostedFile uploadfileName = fileCollection[i];
                    string fileName = Path.GetFileName(uploadfileName.FileName);
                    if (uploadfileName.ContentLength > 0)
                    {
                        //if (Convert.ToString(hdnRemid.Value).Trim() != "0")
                        //    strfileName = hdnRemid.Value + "_" + txtEmpCode.Text + "_" + Convert.ToString(hdnclaimidO.Value) + "_" + strfromDate + "_" + Convert.ToString(i + 1).Trim() + "_payment_Voucher" + Path.GetExtension(uploadfileName.FileName);
                        //else
                        //strfileName = txtEmpCode.Text + "_" + Convert.ToString(hdnclaimqry.Value) + "_" + strfromDate + "_" + Convert.ToString(i + 1).Trim() + "_payment_Voucher" + Path.GetExtension(uploadfileName.FileName);
                        strfileName = Convert.ToString(Txt_BillNo.Text) + "_" + txtEmpCode.Text + "_" + Convert.ToString(hdnclaimqry.Value) + "_" + strfromDate + "_" + Convert.ToString(i + 1).Trim() + "_payment_Voucher" + Path.GetExtension(uploadfileName.FileName);
                        //Txt_BillNo.Text
                        filename = strfileName;
                        uploadfileName.SaveAs(Path.Combine(FuelclaimPath, strfileName));

                        //if (Convert.ToString(hdnRemid.Value).Trim() != "0")
                        //    spm.InsertpaymentUploaded_Files(Convert.ToDecimal(hdnRemid.Value), blnfile, Convert.ToString(strfileName).Trim(), "paymentVoucher_insert", i + 1, Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToInt32(hdnclaimidO.Value));
                        //else
                        spm.InsertpaymentUploaded_Files(Convert.ToDecimal(hdnRemid.Value), blnfile, Convert.ToString(strfileName).Trim(), "paymentVoucher_insertTmp", i + 1, Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToInt32(hdnclaimqry.Value));
                        blnfile = true;
                    }
                }

            }


        }
        #endregion

        txtFromdate.Text = "";
        //txtTravelMode.Text = "";
        txtRemark.Text = "";
        txtAmount.Text = "";
        txtReason.Text = "";
        //txtTravelMode.Text = "";
        Txt_BillNo.Text = "";
        lstTravelMode.SelectedValue = "0";

        getMobileClaimDetails();
        hdnclaimqry.Value = "0";
        hdnClaimid.Value = "0";
        hdnclaimidO.Value = "0";
        gvfuel_pvFiles.DataSource = null;
        gvfuel_pvFiles.DataBind();
        // Response.Redirect("~/procs/Payment_Req.aspx?clmid=" + hdnclaimid.Value + "&rem_id=" + hdnremid.Value);
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        AssigningSessions();
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnclaimidO.Value = Convert.ToString(dgMobileClaim.DataKeys[row.RowIndex].Values[2]).Trim();
        hdnclaimqry.Value = Convert.ToString(dgMobileClaim.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnClaimid.Value = Convert.ToString(dgMobileClaim.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnRemid.Value = Convert.ToString(dgMobileClaim.DataKeys[row.RowIndex].Values[1]).Trim();
        getClaimDetails();
        get_employee_FuelUploaded_Files();
        //lstTravelMode.Enabled = false;
        //Response.Redirect("~/procs/PaymentClaim.aspx?clmid=" + hdnClaimid.Value + "&remid=" + hdnRemid.Value);
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {

        String[] stremp;
        stremp = Convert.ToString(ddl_ProjectName.SelectedItem.Text).Split('/');

        if (Convert.ToString(txtAmountTot.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Claim details.";
            return;
        }
        else
        {
            if (Convert.ToString(txtAmountTot.Text) == "0")
            {
                lblmessage.Text = "Please enter Claim details.";
                return;
            }
        }
        string[] strdate;
        string strfromDate = "";
        string strclaimDate = "";
        //getTravle_Desk_COS_ApproverCode();

        lblmessage.Text = "";

        if (Convert.ToString(txtFromdateMain.Text).Trim() == "")
        {
            lblmessage.Text = "Please select Claim Date";
            return;
        }

        if (Convert.ToString(ddl_ProjectName.SelectedItem.Text).Trim() == "0" || Convert.ToString(ddl_ProjectName.SelectedItem.Text).Trim()=="")
        {
            lblmessage.Text = "Please enter Project Name";
            return;
        }
        else
        {
            //if (Convert.ToString(Txt_ProjectName.Text).Trim() == "Head Office")
            if (Convert.ToString(ddl_ProjectName.SelectedItem.Text).Contains("Head Office"))
            {
                if (Convert.ToString(ddl_DeptName.SelectedValue).Trim() == "" || Convert.ToString(ddl_DeptName.SelectedValue).Trim() == "0")
                {
                    lblmessage.Text = "Please enter Department Name";
                    return;
                }
            }
        }

        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }

        if (Convert.ToString(txtFromdateMain.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdateMain.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        

        if (Convert.ToString(dgMobileClaim.Rows[0].Cells[0].Text) != "")
        {
            strdate = Convert.ToString(dgMobileClaim.Rows[0].Cells[0].Text).Trim().Split('/');
            strclaimDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        DataTable dtMaxRempID = new DataTable();
        int status = 1;
        int maxRemid =0;

        dtMaxRempID = spm.InsertPaymentVoucherRembursement(strfromDate, Convert.ToDecimal(txtAmountTot.Text), txtDeviation.Text, txtEmpCode.Text, status, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToString(hdnRemid.Value), strclaimDate, Convert.ToString(Txt_Alt_Contact.Text), Convert.ToString(stremp[1].Trim()), Convert.ToString(ddl_DeptName.SelectedValue));
        maxRemid = Convert.ToInt32(dtMaxRempID.Rows[0]["maxRemid"]);
        
        if ( maxRemid==0)
            return;

        hdnRemid.Value = Convert.ToString(maxRemid);
        if (dgMobileClaim.Rows.Count > 0)
        {
            spm.InsertPaymentVouDetails(maxRemid, "", 0, txtEmpCode.Text, 0, "InsertMainTable", "", "", "", "");
        }

        String strmobeRemURL = "";
        Int32 tt= DgvApprover.Rows.Count;
        if (DgvApprover.Rows.Count == 1)
            strmobeRemURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_PVRem"]).Trim() + "?rem_id=" + hdnRemid.Value + "&mngexp=0&inbtype=RACC";
        else
        strmobeRemURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_PVRem"]).Trim() + "?rem_id=" + hdnRemid.Value + "&mngexp=0&inbtype=APP";


        spm.InsertPaymentVoucherApproverDetails(hflapprcode.Value, Convert.ToInt32(hdnApprId.Value), maxRemid, "");
        //spm.InsertPaymentVoucherApproverDetails(hdnApprovalCOS_Code.Value, Convert.ToInt32(hdnApprovalCOS_ID.Value), maxRemid, "");
        //GetApprove_RejectList
        string strclaim_month = "";
        DateTime tdate;
        if (dgMobileClaim.Rows.Count > 0)
        {
            strfromDate = Convert.ToString(dgMobileClaim.Rows[0].Cells[0].Text).Trim();

            if (Convert.ToString(strfromDate).Trim() != "")
            {
                strdate = Convert.ToString(strfromDate).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                tdate = Convert.ToDateTime(strfromDate);
                strclaim_month = tdate.ToString("MMM-yy");
                //strclaim_month = Convert.ToString(strdate[0]) + "-" + Convert.ToString(strdate[1]);
            }
        }

        
           #region insert or upload multiple files
        DataSet dpvfiles =new DataSet();
          dpvfiles = get_employee_FuelUploaded_Files(0);

          Boolean blnfile = false;
        if (dpvfiles.Tables[0].Rows.Count > 0)
        {
            for (Int32 irow = 0; irow < dpvfiles.Tables[0].Rows.Count; irow++)
            {
                string strpvTmp_filepath = "";
                string str_Source_filename="";
                string str_Destn_filename="";
                Int32 ifilesrno = 0;
                Int32 iclaimid = 0;
                str_Source_filename=Convert.ToString(dpvfiles.Tables[0].Rows[irow]["file_name"]).Trim();
                if (str_Source_filename.StartsWith(Convert.ToString(hdnRemid.Value + "_")))
                    str_Destn_filename = Convert.ToString(str_Source_filename).Trim();
                else
                    str_Destn_filename = Convert.ToString(hdnRemid.Value + "_" + str_Source_filename).Trim();
                ifilesrno = Convert.ToInt32(dpvfiles.Tables[0].Rows[irow]["file_sr_no"]);
                iclaimid= Convert.ToInt32(dpvfiles.Tables[0].Rows[irow]["claim_id"]);
                strpvTmp_filepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PaymentVoucherDocumentpath"]).Trim() + "paymentVoucher_temp/" + Convert.ToString(txtEmpCode.Text) + "/" + Convert.ToString(str_Source_filename));

                string strpaymntV_filepath = "";
                strpaymntV_filepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PaymentVoucherDocumentpath"]).Trim());


                bool folderExists = Directory.Exists(strpaymntV_filepath);
                if (!folderExists)
                {
                    Directory.CreateDirectory(strpaymntV_filepath);
                }
                if (File.Exists(strpvTmp_filepath))
                File.Copy(strpvTmp_filepath, strpaymntV_filepath + str_Destn_filename,true);

                spm.InsertpaymentUploaded_Files(Convert.ToDecimal(hdnRemid.Value), blnfile, Convert.ToString(str_Destn_filename).Trim(), "paymentVoucher_insert", ifilesrno,Convert.ToString(txtEmpCode.Text).Trim(),iclaimid);
                
                blnfile = true;
            }
        }

        #region Delete files
        string FuelclaimPath = "";
        FuelclaimPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PaymentVoucherDocumentpath"]).Trim() + "paymentVoucher_temp/" + Convert.ToString(txtEmpCode.Text) + "/");
        //else
        //    FuelclaimPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PaymentVoucherDocumentpath"]).Trim() + Convert.ToString(hdnremid.Value) + "/");

        bool folderExists_T = Directory.Exists(FuelclaimPath);
        if (!folderExists_T)
        {
            Directory.CreateDirectory(FuelclaimPath);
        }
        string[] Files = Directory.GetFiles(FuelclaimPath);
        foreach (string file in Files)
        {
            File.Delete(file);
        }


        #endregion

        spm.InsertpaymentUploaded_Files(Convert.ToDecimal(hdnRemid.Value), false, Convert.ToString("").Trim(), "paymentVoucher_delete", 0, Convert.ToString(txtEmpCode.Text).Trim(), 0);

        #endregion
        var ccEmail = "";
        var getCCEmail = spm.getRMForPaymentCC("getRMForPaymentCC", txtEmpCode.Text);
        if(getCCEmail!=null)
        {
            if(getCCEmail.Rows.Count>0)
            {
                if(hdnApprEmailaddress.Value!= Convert.ToString(getCCEmail.Rows[0]["Emp_Emailaddress"]))
                {
                    ccEmail = Convert.ToString(getCCEmail.Rows[0]["Emp_Emailaddress"]);
                }
            }
        }
        spm.PaymentVoucher_send_mailto_RM_Approver(txtEmpName.Text, hdnApprEmailaddress.Value, "Request for Payment Voucher Reimbursement ", "", txtAmountTot.Text, GetApprove_RejectList(Convert.ToDecimal(maxRemid)), txtEmpName.Text, ccEmail, strmobeRemURL, "", strclaim_month);
  
        lblmessage.Visible = true;
        lblmessage.Text = "Payment Voucher Reimbursement Reuqest Submitted Successfully";
        Response.Redirect("~/procs/Voucher.aspx");

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

        if (dgMobileClaim.Rows.Count > 0)
        {
            strfromDate = Convert.ToString(dgMobileClaim.Rows[0].Cells[0].Text).Trim();

            if (Convert.ToString(strfromDate).Trim() != "")
            {
                strdate = Convert.ToString(strfromDate).Trim().Split('/');
                strclaim_month = Convert.ToString(strdate[0]) + "-" + Convert.ToString(strdate[1]);
            }
        }

        hdnEligible.Value = "Cancellation";
        string strapprovermails = "";
        getTravle_Desk_COS_ApproverCode();
        //strapprovermails = GetApprove_RejectList(Convert.ToDecimal(hdnRemid.Value));
       strapprovermails = get_Apprves_list_formail_ifCancel();
        ;
        spm.InsertMobileClaimDetails(Convert.ToInt32(hdnRemid.Value), "", 0, txtEmpCode.Text, 0, Convert.ToString("CancelPayemntVoucherRem"), "", "", "","","","","");
        spm.Fuel_send_mail_Cancel(txtEmpName.Text, strapprovermails, "Request for Payment Voucher Reimbursement ", "", txtAmountTot.Text, GetApprove_RejectList(Convert.ToDecimal(hdnRemid.Value)), txtEmpName.Text, "", "", "", strfromDate);
        Response.Redirect("~/procs/Voucher.aspx");
    }

    protected void dgMobileClaim_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
             #region Comment by Sanjay on 27.02.2025
            //if (Convert.ToString(hdnMobRemStatusM.Value).Trim() == "2" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "3" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "4")
            //{
            //    e.Row.Cells[4].Visible = false;
            //}
            //else
            //{
            //    e.Row.Cells[4].Visible = true;
            //}
            //if (Convert.ToString(hdnMobRemStatus_dtls.Value).Trim() == "Approved")
            //{
            //    e.Row.Cells[4].Visible = false;
            //}
            #endregion

        }
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
            spars[0].Value = "payment_voucher";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDecimal(hdnRemid.Value);

            dspaymentVoucher = spm.getDatasetList(spars, "rpt_dataProcedure");

            #endregion

            if (dspaymentVoucher.Tables[0].Rows.Count > 0)
            {
                ReportViewer ReportViewer1 = new ReportViewer();


                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.LocalReport.DataSources.Clear();

                ReportParameter[] param = new ReportParameter[20];
                param[0] = new ReportParameter("pdocno", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Vouno"]));
                param[1] = new ReportParameter("ppvdate", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Rem_Sub_Date"]));
                param[2] = new ReportParameter("pempName", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Emp_Name"]));
                param[3] = new ReportParameter("pempCode", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Empcode"]));
                param[4] = new ReportParameter("pRsinwords", Convert.ToString(dspaymentVoucher.Tables[2].Rows[0]["AmountinWords"]));


                #region Cost Cente & Bank Details
                if (dspaymentVoucher.Tables[3].Rows.Count > 0)
                {
                    param[5] = new ReportParameter("pCostCenterCode", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["Cost_Center"]));
                    param[6] = new ReportParameter("pCostCenterNM", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["Cost_Center_desc"]));

                    param[7] = new ReportParameter("pBankName", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["BANK_name"]));
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

                    param[7] = new ReportParameter("pBankName", Convert.ToString(""));
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
                param[16] = new ReportParameter("PAlt_Contact", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["alternate_contact_no"]));
                param[17] = new ReportParameter("pProjectName", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Project_Name"]));
                param[18] = new ReportParameter("pDept_Name", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Dept_Name"]));
                param[19] = new ReportParameter("pTotalAmount_Claimed", Convert.ToString(dspaymentVoucher.Tables[4].Rows[0]["TotalAmount_Claimed"]));

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

    public void GetMobileEligibility()
    {

        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.GetMobilelEligibilityMatrix(Convert.ToString(hflGrade.Value));

        if (dtApproverEmailIds.Rows.Count == 0)
        {
            btnTra_Details.Visible = false;
            mobile_btnSave.Visible = false;
            mobile_cancel.Visible = false;
            lblmessage.Text = "Sorry You are not entitled for Payment Voucher claims!";
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

        Session["Fromdate"] = txtFromdateMain.Text;
        Session["ReqEmpCode"] = txtEmpCode.Text;
        Session["Grade"] = hflGrade.Value;  
        Session["TrDays"] = hdnTrdays.Value;

        //Response.Write(Convert.ToString(Session["Fromdate"]));
        //Response.End();

    }

    public void getMobileClaimDetails()
    {
        DataTable dtMobileDetails = new DataTable();
        dtMobileDetails = spm.GetPaymentVoucherDetails_Reqstpage(txtEmpCode.Text);

        dgMobileClaim.DataSource = null;
        dgMobileClaim.DataBind();
        txtAmountTot.Text = "0";

        if (dtMobileDetails.Rows.Count > 0)
        {
            //btnTra_Details.Visible = false;
            dgMobileClaim.DataSource = dtMobileDetails;
            dgMobileClaim.DataBind();

            #region Calulate Total Claim Amount
            txtAmountTot.Enabled = false;
            //hdnTravelConditionid.Value = "1";
            double dtotclaimAmt = 0, dttotalEligibility = 0, comapringamount = 1.5;
            for (Int32 irow = 0; irow < dgMobileClaim.Rows.Count; irow++)
            {
                if (Convert.ToString(dgMobileClaim.Rows[irow].Cells[1].Text).Trim() != "")
                    dtotclaimAmt += Convert.ToDouble(dgMobileClaim.Rows[irow].Cells[3].Text);

                if (Convert.ToString(dgMobileClaim.Rows[irow].Cells[1].Text).Trim() != "")
                    dttotalEligibility += Convert.ToDouble(dgMobileClaim.Rows[irow].Cells[3].Text);

                //if (Convert.ToString(dgMobileClaim.Rows[irow].Cells[2].Text) == "0")
                //    hdnTravelConditionid.Value = "2";
            }
            //txtAmountTot.Text = Convert.ToString(dtotclaimAmt);

            decimal parsed = decimal.Parse(Convert.ToString(dtotclaimAmt), CultureInfo.InvariantCulture);
            CultureInfo hindi = new CultureInfo("hi-IN");
            txtAmountTot.Text = string.Format(hindi, "{0:#,#0.00}", parsed);

            #endregion
        }
        //dgMobileClaim.Columns[5].Visible=false;
        //dgMobileClaim.Columns[6].Visible = false;
    }

    private void InsertMobileRem_DatatoTempTables_trvl()
    {
        try
        {

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_PaymentRem_insert_mainData_toTempTabls";

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
            spars[0].Value = "sp_getMainTravelRequest_forPaymentVoucher";

             spars[1] = new SqlParameter("@rem_id", SqlDbType.Int);
            spars[1].Value = hdnRemid.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;
            //dtTrDetails = spm.getMobileRemDataList(spars, "[SP_GETALLreembursement_DETAILS]");
            dtTrDetails = spm.getDatasetList(spars, "[SP_GETALLreembursement_DETAILS]");

            //mobile_btnPrintPV.Visible = false;
            mobile_btnPrintPV.Visible = true;
          //  GetCompany_Location();
          //  GetDepartMentList();
            if (dtTrDetails.Tables[0].Rows.Count > 0)
            {
                txtFromdateMain.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["created_on"]);
                txtAmountTot.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["TotalAmount_Claimed"]);
               // txtDeviation.Text = Convert.ToString(dtTrDetails.Rows[0]["Reason"]);
                hdnMobRemStatusM.Value = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Ren_Status"]);
                hdnTravelConditionid.Value = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Rem_Conditionid"]);
                if (Convert.ToString(hdnTravelConditionid.Value) == "2")
                {
                    chk_exception.Checked = true;
                }

                Txt_Alt_Contact.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["alternate_contact_no"]);
                
                string strprojectname = dtTrDetails.Tables[0].Rows[0]["Project_Name"].ToString();
               hdnDept_Id.Value =Convert.ToString(dtTrDetails.Tables[0].Rows[0]["DeptId"].ToString());
                ddl_ProjectName.SelectedItem.Text = strprojectname;
                var split = strprojectname.Split('/')[0];
                getApproverdata(split);
                ddl_DeptName.SelectedValue = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Dept_Name"]);
                lblheading.Text = "Payment Voucher - " + Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Vouno"]);
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
                           //Comment by Sa mobile_btnPrintPV.Visible = true;
                       }

                   }
                }
            if (Convert.ToString(ddl_ProjectName.SelectedItem).Contains("Head Office"))
                ddl_DeptName.Enabled = true;
            else
                    ddl_DeptName.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    }
    public void getApproverdata(string comp_code)
    {
        var getComp_Code = "";
        var getDept_Id = 0;
        if (comp_code=="")
        {
            getComp_Code = ddl_ProjectName.SelectedItem.Value;
        }
        else
        {
            getComp_Code = comp_code;
            getDept_Id = Convert.ToInt32(hdnDept_Id.Value);
        }
        if(ddl_DeptName.SelectedItem.Value!="0")
        {
            getDept_Id = Convert.ToInt32(ddl_DeptName.SelectedItem.Value);
        }
        DataTable dtApproverEmailIds = new DataTable();
        if(Convert.ToString(hdnRemid.Value).Trim()=="")
            dtApproverEmailIds = spm.GeTPaymentVoucherApproverEmailID_Comp(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), 0, getComp_Code, getDept_Id);
        else
            dtApproverEmailIds = spm.GeTPaymentVoucherApproverEmailID_Comp(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToDecimal(hdnRemid.Value), getComp_Code, getDept_Id);

        //IsEnabledFalse (true);

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
            GetTravelMode(getComp_Code, txtEmpCode.Text);
        }
        else
        {
            lblmessage.Text = "Since you are not assigned under any approver you can not apply for any type of Payment Voucher, please contact HR";

        }
    }

    protected string GetApprove_RejectList(decimal dmaxremid)
    {
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        var getComp_Code = ddl_ProjectName.SelectedValue;
        var getDept_Id = 0;
        if (ddl_DeptName.SelectedItem.Value != "0")
        {
            getDept_Id = Convert.ToInt32(ddl_DeptName.SelectedItem.Value);
        }
        dtAppRej = spm.GeTPaymentVoucherApproverEmailID_Comp(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), dmaxremid, getComp_Code, getDept_Id);
       
            //dtAppRej = spm.GetMobileApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
            //dtAppRej = spm.GeTPaymentVoucherApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), dmaxremid);
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
        spars[1].Value ="RCOS";

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


    public string get_Apprves_list_formail_ifCancel()
    {
        string strapps = "";
        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_apprs_forCancelpymntV";

        spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
        spars[1].Value =Convert.ToDecimal(hdnRemid.Value);

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = txtEmpCode.Text;

        dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

        //ACC Approver Code
        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            for(Int32 irow=0;irow<dsTrDetails.Tables[0].Rows.Count;irow++)
            {
                if (Convert.ToString(strapps).Trim() == "")
                {
                    strapps = Convert.ToString(dsTrDetails.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                }
                else
                {
                    strapps = strapps + ";" + Convert.ToString(dsTrDetails.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
                }
            }

        }
        return strapps;
    }

    public DataSet get_employee_FuelUploaded_Files(Int32 iclaimid)
    {
        DataSet tmpds_pv = new DataSet();
        try
        {            
            SqlParameter[] spars = new SqlParameter[5];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_pvFiles_onReqpage";

            spars[1] = new SqlParameter("@filetype", SqlDbType.VarChar);
            spars[1].Value = "paymentVoucher_insertTmp";

            spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[2].Value = hdnRemid.Value;

            spars[3] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[3].Value = Convert.ToString(txtEmpCode.Text).Trim();

            spars[4] = new SqlParameter("@claimsid", SqlDbType.Int);
            spars[4].Value = iclaimid;

            tmpds_pv = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

           

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
        return tmpds_pv;

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
        getApproverdata("");
        getPayementVoucher_forPrint();

    }

    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        try
        {

            //if (Convert.ToString(txtFromdate.Text).Trim() != "")
            //    checkPastMoths_AlreadySubmits();
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
                checkFutureDates_ForSubmits();


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    }

    public void checkFutureDates_ForSubmits()
    {

        try
        {
            //claimmob_btnSubmit.Visible = true;
            lblmessage.Text = "";
            #region date formatting

            string[] strdate;
            string strfromDate = "";
            string strToDate = "";


            if (Convert.ToString(Session["Fromdate"]).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            #endregion

            DataSet dsfuturedate = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "check_futurePV";

            spars[1] = new SqlParameter("@ClaimDate", SqlDbType.VarChar);
            spars[1].Value = strToDate;

            dsfuturedate = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            if (dsfuturedate.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(dsfuturedate.Tables[0].Rows[0]["msg"]) != "")
                {
                    lblmessage.Text = "Future date claims are not allowed. ";
                    //claimmob_btnSubmit.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }

    }


    ////protected void lstTravelMode_SelectedIndexChanged(object sender, EventArgs e)
    ////{
    ////    txtTravelMode.Text = lstTravelMode.SelectedItem.Text;
    ////    PopupControlExtender1.Commit(lstTravelMode.SelectedItem.Text);

    ////    SetAccountCode();

    ////    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetDeviation('" + hdnDeviation.Value + "','" + hdnClaimid.Value + "');", true);
    ////}


    public void SetAccountCode()
    {
        DataSet dtTripDev = new DataSet();
        dtTripDev = spm.getAccCode(Convert.ToInt32(lstTravelMode.SelectedValue));
        hdnclaimidO.Value = Convert.ToString(Convert.ToInt32(lstTravelMode.SelectedValue));
        if (dtTripDev.Tables[0].Rows.Count > 0)
        {
            hdnDeviation.Value = Convert.ToString(dtTripDev.Tables[0].Rows[0]["pv_type"]).Trim();
            txtRemark.Text = Convert.ToString(hdnDeviation.Value);
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

    protected void lnkViewFiles_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        string strfilename = Convert.ToString(gvfuel_pvFiles.Rows[row.RowIndex].Cells[0].Text).Trim();

        String strfilepath = "";
        if (Convert.ToString(hdnRemid.Value).Trim() != "0")
            strfilepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PaymentVoucherDocumentpath"]).Trim() + strfilename);
        else
            strfilepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PaymentVoucherDocumentpath"]).Trim() + "paymentVoucher_temp/" + Convert.ToString(txtEmpCode.Text) + "/" + strfilename);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
        Response.WriteFile(strfilepath);
        Response.End();

    }

    //public void GetTravelMode()
    //{
    //    DataTable dtTripMode = new DataTable();
    //    //hdnTryiptypeid.Value
    //    //dtTripMode = spm.getTravelMode();
    //    dtTripMode = spm.getPaymentVoucher_List(txtEmpCode.Text.ToString());
    //    if (dtTripMode.Rows.Count > 0)
    //    {
    //        lstTravelMode.DataSource = dtTripMode;
    //        lstTravelMode.DataTextField = "pv";
    //        lstTravelMode.DataValueField = "pv_id";
    //        lstTravelMode.DataBind();

    //    }
    //}
    public void GetTravelMode(string comp_code, string emp_code)
    {
        DataTable dtTripMode = new DataTable();
        lstTravelMode.DataSource = null;
        lstTravelMode.DataBind();
        //hdnTryiptypeid.Value
        //dtTripMode = spm.getTravelMode();
        //dtTripMode = spm.getPaymentVoucher_List(txtEmpCode.Text.ToString());
        dtTripMode = spm.getPaymentVoucher_List_COMP(txtEmpCode.Text.ToString(), comp_code);
        if (dtTripMode.Rows.Count > 0)
        {
            lstTravelMode.DataSource = dtTripMode;
            lstTravelMode.DataTextField = "pv";
            lstTravelMode.DataValueField = "pv_id";
            lstTravelMode.DataBind();

        }
    }

    public Boolean check_duplicates_Claims(int clmid)
    {
        Boolean blnCheckDuplicate = false;
        try
        {


            DataSet dsduplicateClaim = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "check_duplicate_claims";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

            spars[2] = new SqlParameter("@claimsid", SqlDbType.Int);

            spars[2].Value = clmid;

            dsduplicateClaim = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            if (dsduplicateClaim.Tables[0].Rows.Count > 0)
            {
                blnCheckDuplicate = true;
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
        return blnCheckDuplicate;

    }


    protected void Del_vou_bill(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnRemid.Value = Convert.ToString(dgMobileClaim.DataKeys[row.RowIndex].Values[1]).Trim();
        hdnclaimidO.Value = Convert.ToString(dgMobileClaim.DataKeys[row.RowIndex].Values[2]).Trim();
        hdnclaimqry.Value = Convert.ToString(dgMobileClaim.DataKeys[row.RowIndex].Values[0]).Trim();

        hdnsptype.Value = "deleteTempTable";
        DataTable dtmaxClaimid = new DataTable();
        dtmaxClaimid = spm.InsertPaymentVoucherDetails(Convert.ToInt32(hdnRemid.Value), "", Convert.ToDecimal(0), txtEmpCode.Text, Convert.ToDecimal(0), hdnsptype.Value, txtRemark.Text, "", hdnclaimqry.Value, "", Convert.ToString(hdnclaimqry.Value).Trim(), "");
        getMobileClaimDetails();
        //get_emp_fule_eligibility();
    }

    public void get_employee_FuelUploaded_Files()
    {
        try
        {


            DataSet dsfuelFiles = new DataSet();
            SqlParameter[] spars = new SqlParameter[5];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_PaymntVocherFiles_New";  //"get_PaymntVocherFiles";

            spars[1] = new SqlParameter("@filetype", SqlDbType.VarChar);
            spars[1].Value = "paymentVoucher_insertTmp";

            spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[2].Value = hdnRemid.Value;

            spars[3] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[3].Value = Convert.ToString(txtEmpCode.Text).Trim();

            spars[4] = new SqlParameter("@claimsid", SqlDbType.Int);
            spars[4].Value = hdnClaimid.Value;


            dsfuelFiles = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            gvfuel_pvFiles.DataSource = null;
            gvfuel_pvFiles.DataBind();
            if (dsfuelFiles.Tables[0].Rows.Count > 0)
            {
                gvfuel_pvFiles.DataSource = dsfuelFiles.Tables[0];
                gvfuel_pvFiles.DataBind();
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
        spars[0].Value = "getPaymentVoucherdetails_edit";

        spars[1] = new SqlParameter("@claimsid", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(hdnclaimqry.Value);

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = Convert.ToString(txtEmpCode.Text);

        spars[3] = new SqlParameter("@rem_id", SqlDbType.VarChar);
        spars[3].Value = Convert.ToString(hdnRemid.Value);

        dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");


        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            //txtTravelMode.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["pv"]).Trim();
            //lstTravelMode.SelectedItem.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["pv"]).Trim();
            txtFromdate.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Rem_Month"]).Trim();
            txtAmount.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Amount"]).Trim();
            //txtElgAmnt.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Eligible_Amt"]).Trim();
            txtRemark.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Remarks"]).Trim();
            lstTravelMode.SelectedValue = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Exp_Type"]);
            hdnDeviation.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Remarks"]).Trim();
            lnkuplodedfile.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["UploadFile"]).Trim();
            txtReason.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Particulars"]).Trim();
            Txt_BillNo.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Billno"]).Trim();
        }


    }

    //protected void Txt_ProjectName_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //Check Is Project Existix Or Not
    //        if (!checkIsExistProject(Txt_ProjectName.Text))
    //        {
    //            Txt_ProjectName.Text = "";
    //            lblmessage.Text = "Please select project name in list only";
    //            return;
    //        }

    //        //if (Txt_ProjectName.Text.Trim() == "Head Office")
    //        if (Convert.ToString(Txt_ProjectName.Text).Contains("Head Office"))
    //            Txt_DeptName.Enabled = true;
    //        else
    //            Txt_DeptName.Enabled = false;
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}

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
     public bool checkIsExistProject(string projectName)
    {
        bool blnchk = false;
        try
        {
            // var getProjectSplit = projectName.Split('/');
            // var getCode = "";
            var getLocation = projectName;
            if (getLocation == "")
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


   

    protected void chk_exception_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_exception.Checked)
            hdnTravelConditionid.Value = "2";
        else
            hdnTravelConditionid.Value = "1";




        string strprojectname = ddl_ProjectName.SelectedItem.Text;
        if (Convert.ToString(strprojectname) != "")
        {
            if (Convert.ToString(ddl_DeptName.SelectedValue) == "0" && ddl_DeptName.Enabled==true)
            {
                var split = strprojectname.Split('/')[0];
                getApproverdata(split);
            }
            else
            {
                getApproverdata("");
            }

        }
        else
        {
            getApproverdata("");
        }



    }

   

    protected void ddl_ProjectName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";

            String[] stremp;
            stremp = Convert.ToString(ddl_ProjectName.SelectedItem.Text).Split('/');

            //Check Is Project Existix Or Not
            if (!checkIsExistProject(stremp[1].Trim()))
            {
                ddl_ProjectName.SelectedValue = "0";
                lblmessage.Text = "Please select project name in list only";
                return;
            }

            //if (Txt_ProjectName.Text.Trim() == "Head Office")
            if (Convert.ToString(stremp[1].Trim()).Contains("Head Office"))
            {
                ddl_DeptName.Enabled = true;
            }
            else
            {
                ddl_DeptName.Enabled = false;
                getApproverdata("");
            }
            ddl_DeptName.SelectedValue = "0";

        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void ddl_DeptName_SelectedIndexChanged(object sender, EventArgs e)
    {
        getApproverdata("");
    }
}
