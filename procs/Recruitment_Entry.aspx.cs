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
using System.Web;
using System.Collections.Generic;

public partial class Recruitment_Entry : System.Web.UI.Page
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

    protected void loadorder()
    {
        DataTable dtp = classpkg.getcountorderdetailbyemaild(Page.User.Identity.Name.ToString().Trim());
        if (dtp.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtp.Rows[0]["orderid"].ToString()) == 0)
            {
                lihistory.Visible = false;
            }
        }
        else
        {
            lihistory.Visible = true;
        }
    }
    private void DisplayProfileProperties()
    {
        try
        {
            Boolean varfindcity = false;

            MembershipUser user = Membership.GetUser(this.Page.User.Identity.Name.ToString().Trim());
            DataSet ds_userdetails = classaddress.getalluserbyusername(this.Page.User.Identity.Name.ToString().Trim());
            if (ds_userdetails.Tables.Count > 0)
            {
                if (ds_userdetails.Tables[0].Rows.Count > 0)
                {
                    // txtemail.Text = ds_userdetails.Tables[0].Rows[0]["username"].ToString();
                    // txtemailadress.Text = ds_userdetails.Tables[0].Rows[0]["emailid"].ToString();
                    //txtfirstname.Text = ds_userdetails.Tables[0].Rows[0]["firstname"].ToString();
                    //txtlastname.Text = ds_userdetails.Tables[0].Rows[0]["lastname"].ToString();
                    //txtaddress1.Text = ds_userdetails.Tables[0].Rows[0]["address"].ToString();
                    // txtpincode.Text = ds_userdetails.Tables[0].Rows[0]["pincode"].ToString();
                    //txtphone.Text = ds_userdetails.Tables[0].Rows[0]["telno"].ToString();
                    //txtmobile.Text = ds_userdetails.Tables[0].Rows[0]["mobileno"].ToString();
                    //txttempaddress.Text = ds_userdetails.Tables[0].Rows[0]["tempaddress"].ToString();
                    //txtaltemail.Text = ds_userdetails.Tables[0].Rows[0]["alternateemail"].ToString();
                    //txtextension.Text = ds_userdetails.Tables[0].Rows[0]["extentionno"].ToString();
                    //txtoffno.Text = ds_userdetails.Tables[0].Rows[0]["officemob"].ToString();
                    //txtaltno.Text = ds_userdetails.Tables[0].Rows[0]["alternatemob"].ToString();
                    //txtoffphone.Text = ds_userdetails.Tables[0].Rows[0]["officephone"].ToString();
                    //txtfaxno.Text = ds_userdetails.Tables[0].Rows[0]["faxno"].ToString();
                    //  txtloc.Text = ds_userdetails.Tables[0].Rows[0]["location"].ToString();
                    //txtdept.Text = ds_userdetails.Tables[0].Rows[0]["department"].ToString();
                    //txtsubdept.Text = ds_userdetails.Tables[0].Rows[0]["sub_department"].ToString();
                    //txtdesg.Text = ds_userdetails.Tables[0].Rows[0]["designation"].ToString();

                    //DateTime dob1 = new DateTime();

                    //if (ds_userdetails.Tables[0].Rows[0]["DOB1"].ToString().Length > 0 && ds_userdetails.Tables[0].Rows[0]["DOB1"].ToString() != null && ds_userdetails.Tables[0].Rows[0]["DOB1"].ToString() != "")
                    //{
                    //    dob1 = Convert.ToDateTime(ds_userdetails.Tables[0].Rows[0]["DOB1"]);
                    //    if (dob1.ToString("dd/MMM/yyyy") == "01/Jan/1900")
                    //    {
                    //        txtdob1.Text = "";
                    //    }
                    //    else
                    //    {
                    //        txtdob1.Text = dob1.ToString("dd/MMM/yyyy");
                    //    }
                    //}
                    //else
                    //{
                    //    txtdob1.Text = "";
                    //}


                    DateTime dob = new DateTime();

                    if (ds_userdetails.Tables[0].Rows[0]["DOB"].ToString().Length > 0 && ds_userdetails.Tables[0].Rows[0]["DOB"].ToString() != null && ds_userdetails.Tables[0].Rows[0]["DOB"].ToString() != "")
                    {
                        dob = Convert.ToDateTime(ds_userdetails.Tables[0].Rows[0]["DOB"]);
                        if (dob.ToString("dd/MMM/yyyy") == "01/Jan/1900")
                        {
                            // txtdob.Text = "";
                        }
                        else
                        {
                            //  txtdob.Text = dob.ToString("dd/MMM/yyyy");
                        }
                    }
                    else
                    {
                        //txtdob.Text = "";
                    }

                    string gen = ds_userdetails.Tables[0].Rows[0]["gender"].ToString();
                    if (gen == "M" || gen == "m")
                    {
                        //rbtnmale.Checked = true;
                    }
                    else
                    {
                        //rbtnfemale.Checked = true;
                    }


                    DataTable user2 = classreviews.getuseridbyemail(Page.User.Identity.Name);

                    if (user2.Rows.Count > 0)
                    {
                        userid = user2.Rows[0]["indexid"].ToString();
                        if (user2.Rows[0]["profilephoto"].ToString() != "")
                        {
                            pimg = user2.Rows[0]["profilephoto"].ToString().Trim();
                            if (user2.Rows[0]["profilephoto"].ToString().Trim() == "noimage1.png" || user2.Rows[0]["profilephoto"].ToString().Trim() == "noimage3.jpg")
                            {
                                //  removeprofile.Visible = false;
                            }
                            else
                            {
                                // removeprofile.Visible = true;
                            }
                            if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profilephoto/" + user2.Rows[0]["profilephoto"].ToString())))
                            {
                                //  imgprofile.Src = ReturnUrl("sitepath") + "images/profilephoto/" + user2.Rows[0]["profilephoto"].ToString();
                            }
                            else
                            {
                                // imgprofile.Src = "http://graph.facebook.com/" + user2.Rows[0]["profilephoto"].ToString() + "/picture?type=large";
                            }
                        }
                        else
                        {
                            // imgprofile.Src = ConfigurationManager.AppSettings["adminsitepath"]+ "images/noprofile.jpg";
                            // removeprofile.Visible = false;
                        }
                        if (File.Exists(Server.MapPath("~/themes/creative1.0/images/coverphoto/" + user2.Rows[0]["coverphoto"].ToString())))
                        {
                            cimg = user2.Rows[0]["coverphoto"].ToString().Trim();
                            //imgcover.Src = ReturnUrl("sitepath") + "images/coverphoto/" + user2.Rows[0]["coverphoto"].ToString();
                        }
                        else
                        {
                            //imgcover.Visible = false;
                            //removecover.Visible = false;
                        }
                    }
                    else
                    {
                        // imgprofile.Visible = false;
                        //imgcover.Visible = false;
                    }

                    //  fillcountry();
                    country country = classaddress.GetCountryDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["country"].ToString()));
                    // ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByText(country.countryName.Trim()));

                    //  fillstate(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["country"].ToString()));
                    states state = classaddress.GetStateDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["state"].ToString()));
                    // ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByText(state.statename.Trim()));

                    //       fillcity(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["state"].ToString()));

                    city city = classaddress.GetCityDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["city"].ToString()));

                    //  ddlcity1.SelectedValue = ds_userdetails.Tables[0].Rows[0]["city"].ToString().Trim();
                    // txtcity.Text = ddlcity1.SelectedItem.Text.Trim();
                }
            }

        }
        catch (Exception ex)
        {

        }
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
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionendR.aspx");
            }

            lblmsg.Visible = false; ;
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "loginR.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Recruitment_index");
            }
            else
            {
                Page.SmartNavigation = true;
                mobile_btnPrintPV.Visible = false;
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    divbtn.Visible = false;
                    //divmsg.Visible = false;
                    mobile_cancel.Visible = false;
                    btnTra_Details.Visible = true;
                    txtEmpCode.Text = Convert.ToString(Session["Empcode"]);
                    hdnTravelConditionid.Value = "1";
                    hdnRemid.Value = "0";
                    lstPosting_Location.Enabled = true;
                    txtPosting_Location.Enabled = true;
                   // txtPosting_Location.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    GetEmployeeDetails();
                  //  txtPosting_Location.BackColor = Color.FromArgb(235, 235, 228);
                    
                    
                    txtFromdate.Text = DateTime.Today.ToString("dd/MM/yyyy").Replace("-", "/");
                    txtFromdate.Enabled = false;
                     
                    //txtFromdate.Text = ;
                    if (Request.QueryString.Count > 0)
                    {
                        hdnClaimid.Value = Convert.ToString(Request.QueryString[0]).Trim(); 
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
                    getMobileClaimDetails();
                    getApproverdata();
                    if (Convert.ToString(hdnMobRemStatusM.Value).Trim() == "2" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "3" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "4")
                    {
                        mobile_btnSave.Visible = false;
                        mobile_cancel.Visible = false;
                        btnTra_Details.Visible = false;
                       // dgMobileClaim.Enabled = false;
                    }
                    if (Convert.ToString(hdnMobRemStatus_dtls.Value).Trim() == "Approved")
                    {
                        mobile_btnSave.Visible = false;
                        mobile_cancel.Visible = false;
                        btnTra_Details.Visible = false;
                    }
                    //getMobileClaimDetails();
                    //GetMobileEligibility();
                    DisplayProfileProperties();
                    loadorder();
                   // GetTravelMode();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";

                    
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    protected void btnTra_Details_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter submission date";
            return;
        }
        AssigningSessions();
        Response.Redirect("~/procs/Recruitment_Details.aspx?clmid=0&rem_id=" + hdnRemid.Value);
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        AssigningSessions();        
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnClaimid.Value = Convert.ToString(dgMobileClaim.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnRemid.Value = Convert.ToString(dgMobileClaim.DataKeys[row.RowIndex].Values[1]).Trim();

        Response.Redirect("~/procs/PaymentClaim.aspx?clmid=" + hdnClaimid.Value + "&remid=" + hdnRemid.Value);
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        if(Convert.ToString(txtAmount.Text).Trim()=="")
        {
            lblmessage.Text = "Please enter Claim details.";
            return;
        }
        string[] strdate;
        string strfromDate = "";
        string strclaimDate = "";
        //getTravle_Desk_COS_ApproverCode();

        lblmessage.Text = "";

        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "Please select From Date";
            return;
        }

        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }

        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
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

        dtMaxRempID = spm.InsertPaymentVoucherRembursement(strfromDate, Convert.ToDecimal(txtAmount.Text), txtDeviation.Text, txtEmpCode.Text, status, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToString(hdnRemid.Value), strclaimDate);
             maxRemid = Convert.ToInt32(dtMaxRempID.Rows[0]["maxRemid"]);
        
        if ( maxRemid==0)
            return;

        hdnRemid.Value = Convert.ToString(maxRemid);
        if (dgMobileClaim.Rows.Count > 0)
        {
            spm.InsertPaymentVouDetails(maxRemid, "", 0, txtEmpCode.Text, 0, "InsertMainTable", "", "", "", "");
        }

        String strmobeRemURL = "";
        if (lstApprover.Items.Count==1)
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
     


        spm.PaymentVoucher_send_mailto_RM_Approver(txtEmpName.Text, hdnApprEmailaddress.Value, "Request for Payment Voucher Reimbursement ", "", txtAmount.Text, GetApprove_RejectList(Convert.ToDecimal(maxRemid)), txtEmpName.Text, "", strmobeRemURL, "", strclaim_month);
        

        lblmessage.Visible = true;
        lblmessage.Text = "Payment Voucher Reimbursement Reuqest Submitted Successfully";
        Response.Redirect("~/procs/Reembursementindex.aspx");

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
        spm.InsertMobileClaimDetails(Convert.ToInt32(hdnRemid.Value), "", 0, txtEmpCode.Text, 0, Convert.ToString("CancelPayemntVoucherRem"), "", "", "","","");
        spm.Fuel_send_mail_Cancel(txtEmpName.Text, strapprovermails, "Request for Payment Voucher Reimbursement ", "", txtAmount.Text, GetApprove_RejectList(Convert.ToDecimal(hdnRemid.Value)), txtEmpName.Text, "", "", "", strfromDate);
        Response.Redirect("~/procs/Reembursementindex.aspx");
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

    public void GetTravelMode()
    {
        DataTable dtTripMode = new DataTable();
        //hdnTryiptypeid.Value
        //dtTripMode = spm.getTravelMode();
        dtTripMode = spm.getPaymentVoucher_List(txtEmpCode.Text.ToString());

        if (dtTripMode.Rows.Count > 0)
        {
            lstPosting_Location.DataSource = dtTripMode;
            lstPosting_Location.DataTextField = "pv";
            lstPosting_Location.DataValueField = "pv_id";
            lstPosting_Location.DataBind();
        }
    }

    public void SetAccountCode()
    {
        DataSet dtTripDev = new DataSet();
        dtTripDev = spm.getAccCode(Convert.ToInt32(lstPosting_Location.SelectedValue));
        
        if (dtTripDev.Tables[0].Rows.Count > 0)
        {
            hdnDeviation.Value = Convert.ToString(dtTripDev.Tables[0].Rows[0]["pv_type"]).Trim();

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

                ReportParameter[] param = new ReportParameter[5];
                param[0] = new ReportParameter("pdocno", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Rem_id"]));
                param[1] = new ReportParameter("ppvdate", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Rem_Sub_Date"]));
                param[2] = new ReportParameter("pempName", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Emp_Name"]));
                param[3] = new ReportParameter("pempCode", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Empcode"]));
                param[4] = new ReportParameter("pRsinwords", Convert.ToString(dspaymentVoucher.Tables[2].Rows[0]["AmountinWords"]));

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

        Session["Fromdate"] = txtFromdate.Text;
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

        if (dtMobileDetails.Rows.Count > 0)
        {
            //btnTra_Details.Visible = false;
            dgMobileClaim.DataSource = dtMobileDetails;
            dgMobileClaim.DataBind();

            #region Calulate Total Claim Amount
            txtAmount.Text = "0";
            txtAmount.Enabled = false;
            hdnTravelConditionid.Value = "1";
            double dtotclaimAmt = 0, dttotalEligibility = 0, comapringamount = 1.5;
            for (Int32 irow = 0; irow < dgMobileClaim.Rows.Count; irow++)
            {
                if (Convert.ToString(dgMobileClaim.Rows[irow].Cells[1].Text).Trim() != "")
                    dtotclaimAmt += Convert.ToDouble(dgMobileClaim.Rows[irow].Cells[4].Text);

                if (Convert.ToString(dgMobileClaim.Rows[irow].Cells[2].Text).Trim() != "")
                    dttotalEligibility += Convert.ToDouble(dgMobileClaim.Rows[irow].Cells[4].Text);
                if (Convert.ToString(dgMobileClaim.Rows[irow].Cells[3].Text) == "0")
                    hdnTravelConditionid.Value = "2";
            }
            
            //if (dtotclaimAmt <= dttotalEligibility)
            //{
            //       hdnTravelConditionid.Value = "1";
            //}
            //else
            //{
            //    if (dtotclaimAmt < dttotalEligibility * comapringamount)
            //    {
            //       hdnTravelConditionid.Value = "2";
            //    }
            //    else if (dtotclaimAmt >= dttotalEligibility * comapringamount)
            //    {
            //       hdnTravelConditionid.Value = "3";
            //    }
            //}
            txtAmount.Text = Convert.ToString(dtotclaimAmt);
            #endregion
        }

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
            if (dtTrDetails.Tables[0].Rows.Count > 0)
            {
                txtFromdate.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["created_on"]);
                txtAmount.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["TotalAmount_Claimed"]);
               // txtDeviation.Text = Convert.ToString(dtTrDetails.Rows[0]["Reason"]);
                hdnMobRemStatusM.Value = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Ren_Status"]);
                hdnTravelConditionid.Value = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Rem_Conditionid"]);

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
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    }
    public void getApproverdata()
    {

        DataTable dtApproverEmailIds = new DataTable();
        if(Convert.ToString(hdnRemid.Value).Trim()=="")
            dtApproverEmailIds = spm.GeTPaymentVoucherApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), 0);
        else
            dtApproverEmailIds = spm.GeTPaymentVoucherApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToDecimal(hdnRemid.Value));

        //IsEnabledFalse (true);

        dspaymentVoucher_Apprs.Tables.Add(dtApproverEmailIds);
        lstApprover.Items.Clear();
        if (dtApproverEmailIds.Rows.Count > 0)
        {
            hdnApprEmailaddress.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["Emp_Emailaddress"]);
            hdnApprId.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["APPR_ID"]);

            //lstApprover.DataSource = null;
            //lstApprover.DataBind();

            lstApprover.DataSource = dtApproverEmailIds;
            //lstApprover.DataTextField = "Emp_Name";
            //lstApprover.DataValueField = "APPR_ID";
            lstApprover.DataTextField = "names";
            lstApprover.DataValueField = "APPR_ID";

            lstApprover.DataBind();
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
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        //dtAppRej = spm.GetMobileApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
        dtAppRej = spm.GeTPaymentVoucherApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), dmaxremid);
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
        getApproverdata();
        getPayementVoucher_forPrint();

    }
    protected void lstPosting_Location_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtPosting_Location.Text = lstPosting_Location.SelectedItem.Text;
        PopupControlExtender3.Commit(lstPosting_Location.SelectedItem.Text);
    }

    #region Search Employees


    [System.Web.Services.WebMethod]
    public static List<string> SearchLocations(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";


                /*strsql = " select Distinct location from addressbook where  " +
                                "  location like   '%' + @SearchText + '%' order by location ";*/


                strsql = "  Select t.empname from  ( " +
                         "  Select Emp_Name + ' - '  +Emp_Code as empname " +
                         "  from tbl_Employee_Mst  " +
                         "   where emp_status='Onboard' " +                         
                         "   ) t " +
                         "   where t.empname like '%' + @SearchText + '%'   Order by t.empname ";



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
                        employees.Add(sdr["empname"].ToString());
                    }
                }
                conn.Close();
                return employees;
            }
        }
    }


    #endregion
}
