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

public partial class procs_Fuel_ReqNew : System.Web.UI.Page
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
    DataTable Summ = new DataTable();
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                Page.SmartNavigation = true;
                lblmessage.Text = "";

                if (!Page.IsPostBack)
                {
                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["FuelClaimsDocumentpath"]).Trim());
                    Session["FuelClaim"] = "YES";
                    txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtQuantity.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtElgAmnt.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txt_parkwash_month.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtTollChargesMain.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txt_airport_parking.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");


                    editform.Visible = true;
                    divbtn.Visible = false;
                    divmsg.Visible = false;
                    mobile_btnPrintPV.Visible = false;
                    lblmessage.Visible = true;
                    txtEmpCode.Text = Convert.ToString(Session["Empcode"]);

                    if (Request.QueryString.Count == 0)
                    {
                        spm.clear_Reimbursement_temp_tables(txtEmpCode.Text, "DeleteFuelTemp");
                    }
                    

                    fuel_btncancel.Visible = false;
                    hdnTravelConditionid.Value = "4";
                    hdnFuelReimbursementType.Value = "";
                    txtFromdateMain.Text = DateTime.Today.ToString("dd/MM/yyyy").Replace("-", "/");
                    txtFromdateMain.Enabled = false;
                    AssigningSessions();
                    txtFromdateMain.Text = Convert.ToString(Session["Fromdate"]);
                    if (Convert.ToString(hdnRemid.Value).Trim() == "")
                    {
                        hdnRemid.Value = "0";
                    }
                    txtFromdateMain.Enabled = true;

                    if (Convert.ToString(Session["FuelClaim"]).Trim() != "")
                    {
                        txtTollChargesMain.Text = Convert.ToString(Session["tollchrgs"]);
                        txt_airport_parking.Text = Convert.ToString(Session["airportparking"]);
                    }
                    else
                    {
                        Session["tollchrgs"] = "";
                        Session["airportparking"] = "";
                        Session["parkwashclaimed"] = "";
                    }
                    GetEmployeeDetails();
                    if (Request.QueryString.Count > 0)
                    {
                        hdnRemid.Value = Convert.ToString(Request.QueryString[1]).Trim();
                    }

                    if (Request.QueryString.Count > 2)
                    {
                        hdnRemid.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        getMobRemlsDetails_usingRemidTwoRecordGet();
                    }

                    getFueClaimDetails();
                    if (Convert.ToString(hdnRemid.Value).Trim() != "0" && Convert.ToString(hdnRemid.Value).Trim() != "")
                    {
                        fuel_btncancel.Visible = true;
                        if (Request.QueryString.Count > 2)
                        {
                            hdnRemid.Value = Convert.ToString(Request.QueryString[1]).Trim();
                            getMobRemlsDetails_usingRemid();
                            InsertMobileRem_DatatoTempTables_trvl();
                        }
                    }
                    get_emp_fule_eligibility();
                    getApproverdata();
                }
                get_employee_FuelUploaded_Files();

                if (Convert.ToString(hdnMobRemStatusM.Value).Trim() == "2" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "3" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "4")
                {
                    fuel_btnSave.Visible = false;
                    fuel_btncancel.Visible = false;
                    btnfuel_Details.Visible = false;
                    mobile_btnPrintPV.Visible = true;

                    // Hide specific columns in GridView
                    foreach (DataControlField column in dgFuelClaim.Columns)
                    {
                        if (column.HeaderText == "Edit") // Replace with the actual column header text
                        {
                            column.Visible = false;
                        }
                        if (column.HeaderText == "Delete") // Replace with the actual column header text
                        {
                            column.Visible = false;
                        }
                    }

                }

                if (Convert.ToString(hdnMobRemStatus_dtls.Value).Trim() == "Approved")
                {
                    fuel_btnSave.Visible = false;
                    fuel_btncancel.Visible = false;
                    btnfuel_Details.Visible = false;
                    mobile_btnPrintPV.Visible = true;
                    // Hide specific columns in GridView
                    foreach (DataControlField column in dgFuelClaim.Columns)
                    {
                        if (column.HeaderText == "Edit") // Replace with the actual column header text
                        {
                            column.Visible = false;
                        }
                        if (column.HeaderText == "Delete") // Replace with the actual column header text
                        {
                            column.Visible = false;
                        }
                    }
                }

                if (Convert.ToString(hdnMobRemStatus_dtls.Value).Trim() == "Approved" && Convert.ToString(hdnMobRemStatusM.Value).Trim() == "5")
                {
                    fuel_btnSave.Visible = true;
                    fuel_btncancel.Visible = true;
                    btnfuel_Details.Visible = true;
                    mobile_btnPrintPV.Visible = false;
                    // Hide specific columns in GridView
                    foreach (DataControlField column in dgFuelClaim.Columns)
                    {
                        if (column.HeaderText == "Edit") // Replace with the actual column header text
                        {
                            column.Visible = true;
                        }
                        if (column.HeaderText == "Delete") // Replace with the actual column header text
                        {
                            column.Visible = true;
                        }
                    }
                }
                idspnParkWash_month.Visible = true;
                txt_parkwash_month.Visible = true;
                if (dgFuelClaim.Rows.Count > 0)
                {
                    idspnParkWash_month.Visible = false;
                    txt_parkwash_month.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    protected void btnfuel_Details_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(txtFromdateMain.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter submission date";
            return;
        }
        AssigningSessions();
        string[] strdate;
        string strfromDate = "";

        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "Bill Date cannot be blank";
            return;
        }
        if (Txt_BalanceAmountPreviousMonth.Text == "0.00" && Txt_BalanceAmountCurrentMonth.Text =="0.00")
        {
            lblmessage.Text = "Both Balance Amount Not Available";
            return;
        }

        

        if (Convert.ToString(txtElgAmnt.Text).Trim() == "" || Convert.ToString(txtElgAmnt.Text).Trim() == "0")
        {
            lblmessage.Text = "Please enter Correct Amount.";
            return;
        }

        if (Txt_BalanceAmountCurrentMonth.Text == "")
        {
            Txt_BalanceAmountCurrentMonth.Text = "0.00";
        }
        if (Txt_BalanceAmountPreviousMonth.Text == "")
        {
            Txt_BalanceAmountPreviousMonth.Text = "0.00";
        }

        if (Convert.ToDecimal(txtElgAmnt.Text) > (Convert.ToDecimal(Txt_BalanceAmountCurrentMonth.Text) + Convert.ToDecimal(Txt_BalanceAmountPreviousMonth.Text)))
        {
                lblmessage.Text = "Amount not greter than Both amount's";
                return;
        }
        if (Convert.ToString(txtBalanceQty.Text).Trim() != "")
        {
            if (Convert.ToDecimal(txtBalanceQty.Text) < 0)
            {
                lblmessage.Text = "Cannot claim for Fuel more than Eligible Quantity.";
                return;
            }
        }

        if (Convert.ToString(txtTollCharges.Text).Trim() == "")
        {
            txtTollCharges.Text = "0";
        }

        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        HDEditFag.Value = "";

        hdnsptype.Value = "InsertTempTable";
        if (Convert.ToString(hdnClaimsID.Value).Trim() == "")
        {
            hdnClaimsID.Value = "0";
        }
        if (Convert.ToString(hdnClaimsID.Value).Trim() != "" && Convert.ToString(hdnClaimsID.Value).Trim() != "0")
            hdnsptype.Value = "updateTempTable";
        
        SqlParameter[] spar = new SqlParameter[8];
        spar[0] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spar[0].Value = txtEmpCode.Text;
        spar[1] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spar[1].Value = hdnsptype.Value;
        spar[2] = new SqlParameter("@Rem_id", SqlDbType.Int);
        spar[2].Value = Convert.ToInt32(hdnRemid.Value);
        spar[3] = new SqlParameter("@Rem_Month", SqlDbType.VarChar);
        spar[3].Value = strfromDate;
        spar[4] = new SqlParameter("@Amount", SqlDbType.Decimal);
        spar[4].Value = Convert.ToDecimal(txtElgAmnt.Text);
        spar[5] = new SqlParameter("@FuelClaims_id", SqlDbType.Int);
        spar[5].Value = Convert.ToInt32(hdnClaimsID.Value);
        spar[6] = new SqlParameter("@AmountCurrentMonth", SqlDbType.Decimal);
        spar[6].Value = Convert.ToDecimal(Txt_BalanceAmountCurrentMonth.Text);
        spar[7] = new SqlParameter("@AmountPreviusMonth", SqlDbType.Decimal);
        spar[7].Value = Convert.ToDecimal(Txt_BalanceAmountPreviousMonth.Text);
        DataSet DSUserRole = spm.getDatasetList(spar, "SP_Insert_Fuel_ClaimDetails");

        if (Convert.ToString(Session["sclamdt"]).Trim() == "")
        {
            Session["sclamdt"] = Convert.ToString(txtFromdate.Text).Trim();
        }
       
        getFueClaimDetails();
        txtFromdate.Text = "";
        txtRate.Text = "";
        txtQuantity.Text = "";
        txtElgAmnt.Text = "";
        get_emp_fule_eligibility(); 
        checkPastMoths_AlreadySubmits();
        if (Request.QueryString.Count > 2)
        {
            HDFlagCheck.Value = "0";
            hdnRemid.Value = Convert.ToString(Request.QueryString[1]).Trim();
            getMobRemlsDetails_usingRemid();
        }

    }

    protected void fuel_btnSave_Click(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";
        lblmessage.Text = "";
        string filename = "";
        string strfileName = "";

        #region Validation
       
        if (Convert.ToString(txtTollChargesMain.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtTollChargesMain.Text).Trim().Split('.');
            if (strdate.Length > 2)
            {
                txtTollChargesMain.Text = "0";
                lblmessage.Text = "Please enter correct Toll Charges.";
                return;
            }

            if (Convert.ToString(txtTollChargesMain.Text).Trim() == "")
            {
                txtTollChargesMain.Text = "0";
            }
        }

        // Added by R1 on 04-10-2018
        if (Convert.ToString(txt_airport_parking.Text).Trim() != "")
        {
            strdate = Convert.ToString(txt_airport_parking.Text).Trim().Split('.');
            if (strdate.Length > 2)
            {
                txt_airport_parking.Text = "0";
                lblmessage.Text = "Please enter correct Airport Parking Charges.";
                return;
            }
            if (Convert.ToString(txt_airport_parking.Text).Trim() == "")
            {
                txt_airport_parking.Text = "0";
            }
        }
        // Added by R1 on 04-10-2018
        string confirmValue = hdnYesNo.Value.ToString(); 
        if (confirmValue != "Yes")
        {
            return;
        }
        #region File Upload
        if (uploadfile.HasFile)
        {

        }
        else
        {
            filename = "";
            if (Convert.ToString(hdnRemid.Value).Trim() != "")
            {
                filename = Convert.ToString(lnkuplodedfile.Text).Trim();
                if (gvfuel_claimsFiles.Rows.Count > 0)
                {
                    filename = "files";
                }
                else
                {
                    filename = "";
                }
            }
        }
        #endregion File Upload

        #endregion

        getTravle_Desk_COS_ApproverCode();
        #region File Upload
        if (uploadfile.HasFile)
        {
            if (dgFuelClaim.Rows.Count > 0)
            {

                filename = uploadfile.FileName;
                //uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["FuelClaimsDocumentpath"]).Trim()), uploadfile.FileName));
                if (Convert.ToString(filename).Trim() != "")
                {
                    #region date formatting
                    if (Convert.ToString(txtFromdateMain.Text).Trim() != "")
                    {
                        strdate = Convert.ToString(txtFromdateMain.Text).Trim().Split('/');
                        strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
                    }
                    #endregion

                    HttpFileCollection fileCollection = Request.Files;
                    for (int i = 0; i < fileCollection.Count; i++)
                    {
                    }
                }
            }
           
        }
        if (Convert.ToString(filename).Trim() == "")
        {
            if (dgFuelClaim.Rows.Count > 0)
            {
                lblmessage.Text = "Please upload file for Fuel Claim.";
                return;
            }
        }

        #endregion File Upload
        if (Convert.ToString(txtFromdateMain.Text).Trim() == "")
        {
            lblmessage.Text = "Please select From Date";
            return;
        }

        if (Convert.ToString(txtFromdateMain.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdateMain.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        Decimal dtxtQty = 0;
        Decimal dtxtouttrvl = 0;
       
        DataTable dtMaxRempID = new DataTable();
        int status = 1;
       
        if (Convert.ToString(hdnRemid.Value).Trim() == "")
        {
            hdnRemid.Value = "0";
        }

        Decimal dtollCharges = 0;
        if (Convert.ToString(txtTollChargesMain.Text).Trim() != "" && Convert.ToString(txtTollChargesMain.Text).Trim() != "0")
            dtollCharges = Math.Round(Convert.ToDecimal(txtTollChargesMain.Text), 2);

        Decimal dairportparking = 0;
        if (Convert.ToString(txt_airport_parking.Text).Trim() != "" && Convert.ToString(txt_airport_parking.Text).Trim() != "0")
            dairportparking = Math.Round(Convert.ToDecimal(txt_airport_parking.Text), 2);

        Decimal dparkwashclaimed = 0;
        if (Convert.ToString(Txt_WashingAllowance.Text).Trim() != "" && Convert.ToString(Txt_WashingAllowance.Text).Trim() != "0")
            dparkwashclaimed = Math.Round(Convert.ToDecimal(Txt_WashingAllowance.Text), 2);

        if (Txt_BalanceAmountCurrentMonth.Text == "")
        {
            Txt_BalanceAmountCurrentMonth.Text = "0.00";
        }
        if (Txt_BalanceAmountPreviousMonth.Text == "")
        {
            Txt_BalanceAmountPreviousMonth.Text = "0.00";
        }

        dtMaxRempID = spm.InsertFuelRembursementNew(strfromDate, Convert.ToDecimal(txt_TotalClaimAmount.Text),txtEmpCode.Text, status, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToString(filename).Trim(), Convert.ToString(hdnRemid.Value).Trim(), dtollCharges, dairportparking, Convert.ToDecimal(dparkwashclaimed), dparkwashclaimed,Convert.ToDecimal(HDYearEligibility.Value),Convert.ToDecimal(Txt_BalanceAmountCurrentMonth.Text),Convert.ToDecimal(Txt_BalanceAmountPreviousMonth.Text));

        int maxRemid = Convert.ToInt32(dtMaxRempID.Rows[0]["maxRemid"]);
        if (dgFuelClaim.Rows.Count > 0)
        {
            spm.InsertFuelClaimDetailsNew(maxRemid, null, txtEmpCode.Text, 0, 0, 0, 0, 0, 0, "InsertMainTableNew", 0,Txt_BalanceAmountCurrentMonth.Text,Txt_BalanceAmountPreviousMonth.Text);
        }
        else
        {
                if (Convert.ToString(txt_parkwash_month.Text).Trim() != "")
                {
                    strdate = Convert.ToString(txt_parkwash_month.Text).Trim().Split('/');
                    strfromDate = txt_parkwash_month.Text;
                }
           
            spm.InsertFuelClaimDetailsNew(maxRemid, strfromDate, txtEmpCode.Text, 0, 0, 0, Convert.ToDecimal(0), Convert.ToDecimal(0), 0, "InsertMainTable_ParkwashChargedNew", 1, Txt_BalanceAmountCurrentMonth.Text, Txt_BalanceAmountPreviousMonth.Text);

        }

        hdnRemid.Value = Convert.ToString(maxRemid);

        #region insert or upload multiple files 
        if (uploadfile.HasFile)
        {
            filename = uploadfile.FileName;
            //uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["FuelClaimsDocumentpath"]).Trim()), uploadfile.FileName));
            if (Convert.ToString(filename).Trim() != "")
            {
                #region date formatting
                if (Convert.ToString(txtFromdateMain.Text).Trim() != "")
                {
                    strdate = Convert.ToString(txtFromdateMain.Text).Trim().Split('/');
                    strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
                }
                #endregion


                string FuelclaimPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["FuelClaimsDocumentpath"]).Trim() + Convert.ToString(hdnRemid.Value) + "/");
                bool folderExists = Directory.Exists(FuelclaimPath);
                if (!folderExists)
                {
                    Directory.CreateDirectory(FuelclaimPath);
                }

                Int32 t_cnt = spm.getFuelUploaded_Files(maxRemid);

                Boolean blnfile = true;
                HttpFileCollection fileCollection = Request.Files;
                for (int i = 0; i < fileCollection.Count; i++)
                {
                    strfileName = "";
                    HttpPostedFile uploadfileName = fileCollection[i];
                    string fileName = Path.GetFileName(uploadfileName.FileName);
                    if (uploadfileName.ContentLength > 0)
                    {
                        String InputFile = System.IO.Path.GetExtension(uploadfileName.FileName);
                        strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + Convert.ToString(t_cnt + 1).Trim() + "_FuelClaim" + (t_cnt + 1).ToString() + InputFile;
                        filename = strfileName;
                        uploadfileName.SaveAs(Path.Combine(FuelclaimPath, strfileName));
                        spm.InsertFuelUploaded_Files(maxRemid, blnfile, Convert.ToString(strfileName).Trim(), "FuelClaim", t_cnt + 1);
                        spm.getFuelUploaded_Files(maxRemid);
                        //blnfile = true;
                        t_cnt = t_cnt + 1;
                    }
                }

            }
        }
        #endregion

        String strmobeRemURL = "";
        
        strmobeRemURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_FuelRem"]).Trim() + "?rem_id=" + hdnRemid.Value + "&mngexp=0&inbtype=RCOS";

        spm.InsertFuelApproverDetails(hdnApprovalCOS_Code.Value, Convert.ToInt32(hdnApprovalCOS_ID.Value), maxRemid, "");
        string strFuelClaimtable = "";
        strFuelClaimtable = getFuelClaimDetails_table();


        getFromdateTodate_FroEmail();
        string strcliamMonth = Convert.ToString(hdnfrmdate_emial.Value).Trim();
        spm.Fuel_send_mailto_RM_ApproverNew(txtEmpName.Text, hdnApprovalCOS_mail.Value, "Request for Fuel bill Reimbursement", txt_TotalClaimAmount.Text, GetApprove_RejectList(Convert.ToDecimal(maxRemid)), txtEmpName.Text, "", strmobeRemURL, strFuelClaimtable, strcliamMonth);

        lblmessage.Visible = true;
        Session["tollchrgs"] = null;
        Session["airportparking"] = null;
        Session["parkwashclaimed"] = null;
        lblmessage.Text = "Fuel Reimbursement Reuqest Submitted Successfully";
        Response.Redirect("~/procs/Fuel.aspx");
    }

    protected void lnkEdit_Click1(object sender, EventArgs e)
    {
        AssigningSessions();
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnRemid.Value = Convert.ToString(dgFuelClaim.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnClaimsID.Value = Convert.ToString(dgFuelClaim.DataKeys[row.RowIndex].Values[1]).Trim();
        ImageButton deleteButton = (ImageButton)row.FindControl("btn_del");
        if (Convert.ToString(hdnClaimsID.Value).Trim() != "0")
        {
            HDEditFag.Value = "0";
            getFueClaimDetailsEdit();
            deleteButton.Enabled = false;


        }
    }

    protected void Del_Fuel_bill(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnRemid.Value = Convert.ToString(dgFuelClaim.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnClaimsID.Value = Convert.ToString(dgFuelClaim.DataKeys[row.RowIndex].Values[1]).Trim();
        string stramtt = Convert.ToString(dgFuelClaim.Rows[row.RowIndex].Cells[2].Text).Trim();

        if (HDMainAmountMonthPrevious.Value == Txt_BalanceAmountPreviousMonth.Text)
        {
            Txt_BalanceAmountCurrentMonth.Text = Convert.ToString(Convert.ToDecimal(Txt_BalanceAmountCurrentMonth.Text) + Convert.ToDecimal(stramtt));
        }
        else if (HDMainAmountMonthPrevious.Value != Txt_BalanceAmountPreviousMonth.Text)
        {
            string strbapm = Txt_BalanceAmountPreviousMonth.Text;
            if (Convert.ToDecimal(HDMainAmountMonthPrevious.Value) >= Math.Abs(Convert.ToDecimal(strbapm) + Convert.ToDecimal(stramtt)))
            {
                Txt_BalanceAmountPreviousMonth.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(strbapm) + Convert.ToDecimal(stramtt)));
            }
            else
            {
                string stramttt = Convert.ToString(Math.Abs(Convert.ToDecimal(HDMainAmountMonthPrevious.Value) - Convert.ToDecimal(strbapm)));
                Txt_BalanceAmountPreviousMonth.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(strbapm) + Convert.ToDecimal(stramttt)));

                string stram = Convert.ToString(Math.Abs(Convert.ToDecimal(stramttt) - Convert.ToDecimal(stramtt)));
                Txt_BalanceAmountCurrentMonth.Text = Convert.ToString(Convert.ToDecimal(Txt_BalanceAmountCurrentMonth.Text) + Convert.ToDecimal(stram));
            }
        }

        hdnsptype.Value = "deleteTempTableNew";

        if (Convert.ToString(txtElgAmnt.Text).Trim() == "")
        {
            txtElgAmnt.Text = "0";
        }
        spm.InsertFuelClaimDetails(Convert.ToInt32(hdnRemid.Value), "", txtEmpCode.Text, 0, 0, 0, 0, 0, 0, Convert.ToString(hdnsptype.Value), Convert.ToInt32(hdnClaimsID.Value));

        hdnClaimsID.Value = "";
        getFueClaimDetails();
        get_emp_fule_eligibility();
        checkPastMoths_AlreadySubmits();
        if (Request.QueryString.Count > 2)
        {
            hdnRemid.Value = Convert.ToString(Request.QueryString[1]).Trim();
            getMobRemlsDetails_usingRemid();
        }
    }

    protected void lnkuplodedfile_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["FuelClaimsDocumentpath"]).Trim()), lnkuplodedfile.Text);

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
        //hdnRemid.Value = Convert.ToString(dgFuelClaim.DataKeys[row.RowIndex].Values[0]).Trim();
        string strfilename = Convert.ToString(hdnRemid.Value) + "/" + Convert.ToString(gvfuel_claimsFiles.Rows[row.RowIndex].Cells[0].Text).Trim();

        String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["FuelClaimsDocumentpath"]).Trim()), strfilename);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
        Response.WriteFile(strfilepath);
        Response.End();

    }

    protected void txtTollChargesMain_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txtTollChargesMain.Text) != "")
        {
            if (Convert.ToDecimal(txtTollCharges_Bal.Text) < Convert.ToDecimal(txtTollChargesMain.Text))
            {
                txtTollChargesMain.Text = "0.00";
                lblmessage.Text = "Cannot Claim more than Entitlement!";
                return;
            }
            else
            {
                if (txt_TotalClaimAmount.Text == "")
                {
                    txt_TotalClaimAmount.Text = "0.00";
                }
                Decimal amount = Convert.ToDecimal(txt_TotalClaimAmount.Text) + Convert.ToDecimal(txtTollChargesMain.Text);
                txt_TotalClaimAmount.Text = Convert.ToString(amount);

            }
        }

    }

    protected void txt_airport_parking_TextChanged(object sender, EventArgs e)
    {
        if (txt_TotalClaimAmount.Text == "")
        {
            txt_TotalClaimAmount.Text = "0.00";
        }
        Decimal amount = Convert.ToDecimal(txt_TotalClaimAmount.Text) + Convert.ToDecimal(txt_airport_parking.Text);
        txt_TotalClaimAmount.Text = Convert.ToString(amount);
    }

    protected void txt_parkwash_claimed_TextChanged(object sender, EventArgs e)
    {
    }

    protected void txt_parkwash_month_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txt_parkwash_month.Text).Trim() != "")
        {
            txtTollChargesMain.Text = "0.00";
            get_emp_fule_eligibility();
            checkPastMoths_AlreadySubmits();
           
        }
    }

    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                get_emp_fule_eligibility_fromsub();
                checkPastMoths_AlreadySubmits_Fromsub();
            }
            else
            {
                HDMainAmountCurrentMonth.Value = "";
                Txt_BalanceAmountCurrentMonth.Text = "";
                HDMainAmountMonthPrevious.Value = "";
                Txt_BalanceAmountPreviousMonth.Text = "";
                Txt_BlanceAmount.Text = "";


            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    }

    #endregion

    #region PageMethods

 
    public void get_emp_fule_eligibility()
    {
        try
        {
            btnfuel_Details.Visible = true;
            fuel_btnSave.Visible = true;
            if (dgFuelClaim.Rows.Count > 0)
            {
                hdnClaimDate.Value = Convert.ToString(dgFuelClaim.Rows[0].Cells[0].Text);
            }

            #region date formatting

            string[] strdate;
            string strFromDate = "";
            if (Convert.ToString(hdnClaimDate.Value).Trim() != "")
            {
                strdate = Convert.ToString(hdnClaimDate.Value).Trim().Split('/');
                strFromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            else
            {
                if (Convert.ToString(txt_parkwash_month.Text).Trim() != "")
                {
                    DateTime dt = DateTime.ParseExact(txt_parkwash_month.Text, "MMM/yyyy", CultureInfo.InvariantCulture);
                    strFromDate = dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
            }
            #endregion


            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "getemp_fule_eligibility";

            spars[1] = new SqlParameter("@Empcode", SqlDbType.VarChar);
            spars[1].Value = txtEmpCode.Text;

            spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[2].Value = hdnRemid.Value;

            spars[3] = new SqlParameter("@ClaimDate", SqlDbType.VarChar);
            if (Convert.ToString(strFromDate).Trim() != "")
                spars[3].Value = strFromDate;
            else
                spars[3].Value = DBNull.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            if (dsTrDetails.Tables[4].Rows.Count == 0)
            {
                #region Set Enable False if not application for Fuel Claim
                lblmessage.Text = "Sorry You are not entitled for Fuel claims!";
                btnfuel_Details.Visible = false;
                fuel_btnSave.Visible = false;
                return;
                #endregion

            }

            Decimal dActualTotalFuelConsump = 0;
            if (dsTrDetails.Tables[1].Rows.Count > 0)
            {
                if (Convert.ToString(dsTrDetails.Tables[1].Rows[0]["totfuelConsm"]) != "")
                {
                    dActualTotalFuelConsump = Convert.ToDecimal(dsTrDetails.Tables[1].Rows[0]["totfuelConsm"]);
                }
            }

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Eligibility"]) != "")
                {
                    HDYearEligibility.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Eligibility"]);
                    txtTollCharges_Ent.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["TOLL_PASS"]).Trim();
                }
            }

            if (dsTrDetails.Tables[3].Rows.Count > 0)
            {
                if (Convert.ToString(dsTrDetails.Tables[3].Rows[0]["Eligibility"]) != "")
                {
                    Txt_WashingAllowance.Text = Convert.ToString(Convert.ToDecimal(dsTrDetails.Tables[3].Rows[0]["Eligibility"])).Trim();
                }
                else
                {
                    Txt_WashingAllowance.Text = "0.00";

                }
            }

            if (dsTrDetails.Tables[2].Rows.Count > 0)
            {
                if (Convert.ToString(dsTrDetails.Tables[2].Rows[0]["UploadFile"]) != "")
                {
                    lnkuplodedfile.Text = Convert.ToString(dsTrDetails.Tables[2].Rows[0]["UploadFile"]);
                }

            }

            idspnParkWash_month.Visible = true;
            txt_parkwash_month.Visible = true;
            if (dgFuelClaim.Rows.Count > 0)
            {
                idspnParkWash_month.Visible = false;
                txt_parkwash_month.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }

    }

    public void get_emp_fule_eligibility_fromsub()
    {
        try
        {
            btnfuel_Details.Visible = true;
            fuel_btnSave.Visible = true;

            #region date formatting

            string[] strdate;
            string strFromDate = "";
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strFromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            #endregion


            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "getemp_fule_eligibility";

            spars[1] = new SqlParameter("@Empcode", SqlDbType.VarChar);
            spars[1].Value = txtEmpCode.Text;

            spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[2].Value = hdnRemid.Value;

            spars[3] = new SqlParameter("@ClaimDate", SqlDbType.VarChar);
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
                spars[3].Value = strFromDate;
            else
                spars[3].Value = DBNull.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");
            txtEligibleQty.Text = "0";
            //claimfuel_btnSubmit.Visible = true;
            txtQuantity.Enabled = true;
            txtElgAmnt.Enabled = true;

            if (dsTrDetails.Tables[4].Rows.Count > 0)
            {
                if (Convert.ToString(dsTrDetails.Tables[4].Rows[0]["Eligibility"]) == "N")
                {
                    #region Set Enable False if not application for Fuel Claim
                    lblmessage.Text = "Sorry due to location change, you are not entitled for Fuel claims!";
                    //claimfuel_btnSubmit.Visible = false;
                    txtQuantity.Enabled = false;
                    txtElgAmnt.Enabled = false;
                    txtQuantity.Text = "";
                    txtElgAmnt.Text = "";
                    txtBalanceQty.Text = "";
                    txtEligibleQty_Monthly.Text = "";
                    HDMainAmountCurrentMonth.Value = "";
                    Txt_BalanceAmountCurrentMonth.Text = "";
                    HDMainAmountMonthPrevious.Value = "";
                    Txt_BalanceAmountPreviousMonth.Text = "";
                    Txt_BlanceAmount.Text = "";
                    return;
                    #endregion
                }

            }

            else
            {
                #region Set Enable False if not application for Fuel Claim
                lblmessage.Text = "Sorry you are not entitled for Fuel claims!";
                //claimfuel_btnSubmit.Visible = false;
                txtQuantity.Enabled = false;
                txtElgAmnt.Enabled = false;
                txtQuantity.Text = "";
                txtElgAmnt.Text = "";
                txtBalanceQty.Text = "";
                txtEligibleQty_Monthly.Text = "";
                HDMainAmountCurrentMonth.Value = "";
                Txt_BalanceAmountCurrentMonth.Text = "";
                HDMainAmountMonthPrevious.Value = "";
                Txt_BalanceAmountPreviousMonth.Text = "";
                Txt_BlanceAmount.Text = "";
                btnfuel_Details.Visible = false;
                fuel_btnSave.Visible = false;
                return;
                #endregion
            }


            Decimal deligibleQty = 0;
            Decimal PreviusMonthtotal = 0;
            int Monthnumber = 0;
            txtEligibleQty.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Eligibility"]).Trim();
            int div = Convert.ToInt32(dsTrDetails.Tables[0].Rows[0]["noofMonths"]);
            if(dsTrDetails.Tables[5].Rows.Count > 0)
            {
                Monthnumber = Convert.ToInt32(dsTrDetails.Tables[5].Rows[0]["MonthNumber"]);
                PreviusMonthtotal = Convert.ToDecimal(dsTrDetails.Tables[5].Rows[0]["TotalThreeColumn"]);
            }
            

            txtEligibleQty_Monthly.Text = "0";
            if (Convert.ToString(txtEligibleQty.Text).Trim() != "")
            {
                if (div > 0)
                {
                    deligibleQty = Convert.ToDecimal(txtEligibleQty.Text);
                    txtEligibleQty_Monthly.Text = Convert.ToString(Math.Round(deligibleQty / div, 2));
                    if (txtFromdate.Text.Trim() != "")
                    {
                        if (HDBalnceAmtCurrentMonth.Value == "")
                        {
                            if (HDAmountCurrentclaimed.Value == "")
                            {
                                Txt_BalanceAmountCurrentMonth.Text = txtEligibleQty_Monthly.Text;
                            }
                            else
                            {
                                Decimal dCamt;
                                if (Convert.ToDecimal(txtEligibleQty_Monthly.Text) >= Convert.ToDecimal(HDAmountCurrentclaimed.Value))
                                {
                                    dCamt = Convert.ToDecimal(txtEligibleQty_Monthly.Text) - Convert.ToDecimal(HDAmountCurrentclaimed.Value);
                                    Txt_BalanceAmountCurrentMonth.Text = Convert.ToString(dCamt);
                                }
                                else
                                {
                                    HDAmountCurrentclaimed.Value = "0.00";
                                    Txt_BalanceAmountCurrentMonth.Text = "0.00";
                                }
                               
                            }
                        }
                        if (HDBalnceAmtCurrentMonth.Value == "0.00")
                        {
                            Txt_BalanceAmountCurrentMonth.Text = "0.00";
                        }

                        if (Txt_BalanceAmountPreviousMonth.Text == "")
                        {
                            string Totalcalmonthamount = Convert.ToString(Math.Round(Convert.ToDecimal(txtEligibleQty_Monthly.Text) * Monthnumber, 2));
                            if (Convert.ToDecimal(Totalcalmonthamount) >= PreviusMonthtotal)
                            {
                                Txt_BalanceAmountPreviousMonth.Text = Convert.ToString(Math.Round(Convert.ToDecimal(Totalcalmonthamount) - PreviusMonthtotal, 2));
                            }
                            else
                            {
                                Txt_BalanceAmountPreviousMonth.Text = "0.00";
                            }
                        }

                        decimal Amount;
                        if (Txt_BalanceAmountCurrentMonth.Text == "")
                        {
                            if (HDBalnceAmtCurrentMonth.Value != "0.00")
                            {
                                Txt_BalanceAmountCurrentMonth.Text = HDBalnceAmtCurrentMonth.Value;
                            }
                            
                        }
                        Amount = Convert.ToDecimal(Txt_BalanceAmountCurrentMonth.Text) + Convert.ToDecimal(Txt_BalanceAmountPreviousMonth.Text);
                        Txt_BlanceAmount.Text = Convert.ToString(Amount);

                        if (HDMainAmountCurrentMonth.Value == "" && HDMainAmountMonthPrevious.Value == "")
                        {
                            HDMainAmountCurrentMonth.Value = Txt_BalanceAmountCurrentMonth.Text;
                            HDMainAmountMonthPrevious.Value = Txt_BalanceAmountPreviousMonth.Text;
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

    public void getMobRemlsDetails_usingRemid()
    {
        try
        {
            DataSet dtTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_getMainTravelRequest_forFuelNew";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Int);
            spars[1].Value = hdnRemid.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            dtTrDetails = spm.getDatasetList(spars, "[SP_GETALLreembursement_DETAILS]");
           // mobile_btnPrintPV.Visible = false;
            if (dtTrDetails.Tables[0].Rows.Count > 0)
            {
                txtFromdateMain.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["created_on"]);
                txtprofile.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["UploadFile"]);
                hdnTravelConditionid.Value = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Fuel_Conditionid"]);
                hdnMobRemStatusM.Value = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Rem_Status"]);
                lnkuplodedfile.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["UploadFile"]).Trim();
                txtTollChargesMain.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["toll_Charges"]).Trim();
                txt_airport_parking.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["airport_parking"]).Trim();
                lblheading.Text = "Fuel Bill - " + Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Vouno"]);
                string parkwash_claimed = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["parkwash_claimed"]).Trim();
                if (HDFlagCheck.Value == "")
                {
                    Txt_BalanceAmountCurrentMonth.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["BalnceAmountCurrent"]).Trim();
                    Txt_BalanceAmountPreviousMonth.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["BalnceAmountPrevious"]).Trim();
                    HDMainAmountCurrentMonth.Value = Txt_BalanceAmountCurrentMonth.Text;
                    HDMainAmountMonthPrevious.Value = Txt_BalanceAmountPreviousMonth.Text;
                }


                if (HDFlagCheck.Value == "")
                {
                    decimal Amount, Amount1, AmountSum;
                    Amount = Convert.ToDecimal(txt_TotalClaimAmount.Text);
                    Amount1 = Convert.ToDecimal(txt_airport_parking.Text) + Convert.ToDecimal(txtTollChargesMain.Text) + Convert.ToDecimal(parkwash_claimed);
                    AmountSum = Convert.ToDecimal(Amount) + Convert.ToDecimal(Amount1);
                    txt_TotalClaimAmount.Text = Convert.ToString(AmountSum);
                }


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
                          //  mobile_btnPrintPV.Visible = true;
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

    public void getMobRemlsDetails_usingRemidTwoRecordGet()
    {
        try
        {
            DataSet dtTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_getMainTravelRequest_forFuelNew";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Int);
            spars[1].Value = hdnRemid.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            dtTrDetails = spm.getDatasetList(spars, "[SP_GETALLreembursement_DETAILS]");
            // mobile_btnPrintPV.Visible = false;
            if (dtTrDetails.Tables[0].Rows.Count > 0)
            {
               
                Txt_BalanceAmountCurrentMonth.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["BalnceAmountCurrent"]).Trim();
                Txt_BalanceAmountPreviousMonth.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["BalnceAmountPrevious"]).Trim();
                HDMainAmountCurrentMonth.Value = Txt_BalanceAmountCurrentMonth.Text;
                HDMainAmountMonthPrevious.Value = Txt_BalanceAmountPreviousMonth.Text;
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    }

    private void InsertMobileRem_DatatoTempTables_trvl()
    {
        try
        {

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_FuelRem_insert_mainData_toTempTablsNew";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDecimal(hdnRemid.Value);

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            DataTable dtMaxTripID = spm.InsertorUpdateData(spars, "[SP_GETALLreembursement_DETAILS]");
            txt_parkwash_month.Text = "";
            if (dtMaxTripID.Rows.Count > 0)
            {
                txt_parkwash_month.Text = Convert.ToString(dtMaxTripID.Rows[0]["Rem_Month"]);
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }


    }

    public void get_employee_FuelUploaded_Files()
    {
        try
        {
            DataSet dsfuelFiles = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_fuelclaimFilesNew";

            spars[1] = new SqlParameter("@filetype", SqlDbType.VarChar);
            spars[1].Value = "FuelClaim";

            spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[2].Value = hdnRemid.Value;
            dsfuelFiles = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            gvfuel_claimsFiles.DataSource = null;
            gvfuel_claimsFiles.DataBind();

            if (dsfuelFiles.Tables[0].Rows.Count > 0)
            {
                gvfuel_claimsFiles.DataSource = dsfuelFiles.Tables[0];
                gvfuel_claimsFiles.DataBind();
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
            Response.End();
            throw;
        }
    }

    public void AssigningSessions()
    {

        Session["Fromdate"] = txtFromdateMain.Text;
        Session["ReqEmpCode"] = txtEmpCode.Text;
        Session["tollchrgs"] = Convert.ToString(txtTollChargesMain.Text);
        Session["airportparking"] = Convert.ToString(txt_airport_parking.Text);

        //Session["Grade"] = hflGrade.Value;
        //Session["TrDays"] = hdnTrdays.Value;


    }

    public void getFueClaimDetails()
    {
        DataSet dtMobileDetails = new DataSet();
        dtMobileDetails = spm.GetFuelClaimDetailsNew(txtEmpCode.Text);

        dgFuelClaim.DataSource = null;
        dgFuelClaim.DataBind();
        if (dtMobileDetails.Tables[0].Rows.Count > 0)
        {
            dgFuelClaim.DataSource = dtMobileDetails.Tables[0];
            dgFuelClaim.DataBind();
            decimal totalAmount = 0;
            foreach (DataRow row in dtMobileDetails.Tables[0].Rows)
            {
                totalAmount += Convert.ToDecimal(row["Amount"]);
            }
            txt_TotalClaimAmount.Text = Convert.ToString(totalAmount);

            if (txtElgAmnt.Text !="")
            {
                if (Txt_BalanceAmountCurrentMonth.Text != "0.00")
                {
                    if (Convert.ToDecimal(Txt_BalanceAmountCurrentMonth.Text) >= Convert.ToDecimal(txtElgAmnt.Text))
                    {
                        Txt_BalanceAmountCurrentMonth.Text = Convert.ToString((Convert.ToDecimal(Txt_BalanceAmountCurrentMonth.Text) - Convert.ToDecimal(txtElgAmnt.Text)));
                    }
                    else
                    {
                        string stramt = Convert.ToString((Convert.ToDecimal(txtElgAmnt.Text) - Convert.ToDecimal(Txt_BalanceAmountCurrentMonth.Text)));
                        Txt_BalanceAmountPreviousMonth.Text = Convert.ToString((Convert.ToDecimal(Txt_BalanceAmountPreviousMonth.Text) - Convert.ToDecimal(stramt)));
                        Txt_BalanceAmountCurrentMonth.Text = "0.00";
                    }
                }
                else if (Txt_BalanceAmountPreviousMonth.Text != "0.00" || Txt_BalanceAmountPreviousMonth.Text != "")
                {
                    Txt_BalanceAmountCurrentMonth.Text = "0.00";
                    Txt_BalanceAmountPreviousMonth.Text = Convert.ToString((Convert.ToDecimal(Txt_BalanceAmountPreviousMonth.Text) - Convert.ToDecimal(txtElgAmnt.Text)));
                }
            }

            HDBalnceAmtCurrentMonth.Value = Txt_BalanceAmountCurrentMonth.Text;
            decimal Amount;
            if (Txt_BalanceAmountCurrentMonth.Text == "")
            {
                Txt_BalanceAmountCurrentMonth.Text = "0.00";
            }
            if (Txt_BalanceAmountPreviousMonth.Text == "")
            {
                Txt_BalanceAmountPreviousMonth.Text = "0.00";
            }
            Amount = Convert.ToDecimal(Txt_BalanceAmountCurrentMonth.Text) + Convert.ToDecimal(Txt_BalanceAmountPreviousMonth.Text);
            Txt_BlanceAmount.Text = Convert.ToString(Amount);

            decimal parking=0,Totllpasschnarge=0 , SumAmount;
            if (txt_airport_parking.Text != "0.00")
            {
                parking = Convert.ToDecimal(txt_airport_parking.Text);
            }
            if (txtTollChargesMain.Text != "0.00")
            {
                Totllpasschnarge = Convert.ToDecimal(txtTollChargesMain.Text);
            }
           
            SumAmount = parking + Totllpasschnarge + totalAmount;
            txt_TotalClaimAmount.Text = Convert.ToString(SumAmount);

        }
    }

    public void checkPastMoths_AlreadySubmits()
    {

        try
        {
            txtTollCharges_Bal.Text = "0";
            Span_Toll_Balance.InnerText = "Toll Pass / Charges (Balance: " + Convert.ToString(Convert.ToDecimal(txtTollCharges_Ent.Text)) + ")";
            lblmessage.Text = "";
            #region date formatting

            string[] strdate;
            string strToDate = "";
            string date_to_check = "";

            string strFromDate = "";
            string strFclmDate = "";
            for (Int32 irow = 0; irow < dgFuelClaim.Rows.Count; irow++)
            {
                if (Convert.ToString(dgFuelClaim.Rows[irow].Cells[0].Text).Trim() != "")
                    date_to_check = Convert.ToString(dgFuelClaim.Rows[irow].Cells[0].Text);
            }
            
            if (date_to_check == "")
            {
                if (Convert.ToString(txt_parkwash_month.Text).Trim() != "")
                {
                    DateTime dt = DateTime.ParseExact(txt_parkwash_month.Text, "MMM/yyyy", CultureInfo.InvariantCulture);
                    date_to_check = dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
            }
            #endregion
            if (date_to_check != "")
            {
                if (Convert.ToString(date_to_check).Trim() != "")
                {
                    strdate = Convert.ToString(date_to_check).Trim().Split('/');
                    strFromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                    strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                }

                DataSet dsTrDetails = new DataSet();
                SqlParameter[] spars = new SqlParameter[5];
                string strToDate1 = strToDate;
                spars[0] = new SqlParameter("@sMonth", SqlDbType.VarChar);
                spars[0].Value = strToDate;


                spars[1] = new SqlParameter("@sclaimdate", SqlDbType.VarChar);
                spars[1].Value = strFromDate;


                spars[2] = new SqlParameter("@Empcode", SqlDbType.VarChar);
                spars[2].Value = txtEmpCode.Text;

                spars[3] = new SqlParameter("@rem_id", SqlDbType.Decimal);
                if (Convert.ToString(hdnRemid.Value) != "" && Convert.ToString(hdnRemid.Value) != "0")
                    spars[3].Value = hdnRemid.Value;
                else
                    spars[3].Value = DBNull.Value;

                spars[4] = new SqlParameter("@Type", SqlDbType.VarChar);
                spars[4].Value = "Fuels";

                dsTrDetails = spm.getDatasetList(spars, "sp_check_Fuel_rem_validation");

                // dsTrDetails = spm.getDatasetList(spars, "FuelvalidationOptimized");
                if (dsTrDetails.Tables.Count > 1)
                {
                    if (dsTrDetails.Tables[1].Rows.Count > 0)
                    {
                        if (Convert.ToDecimal(dsTrDetails.Tables[1].Rows[0]["TollCharges"]) != 0)
                        {
                            txtTollCharges_Bal.Text = Convert.ToString(Convert.ToDecimal(txtTollCharges_Ent.Text) - Convert.ToDecimal(dsTrDetails.Tables[1].Rows[0]["TollCharges"]));
                            Span_Toll_Balance.InnerText = "Toll Pass / Charges (Balance: " + Convert.ToString(Convert.ToDecimal(txtTollCharges_Ent.Text) - Convert.ToDecimal(dsTrDetails.Tables[1].Rows[0]["TollCharges"])) + ")";
                            ////txtTollChargesMain.Enabled = false;
                            ////txtTollChargesMain.Text = "0.00";
                        }
                        else
                        {
                            txtTollCharges_Bal.Text = Convert.ToString(Convert.ToDecimal(txtTollCharges_Ent.Text));
                            Span_Toll_Balance.InnerText = "Toll Pass / Charges (Balance: " + Convert.ToString(Convert.ToDecimal(txtTollCharges_Ent.Text)) + ")";
                        }
                        if (Convert.ToDecimal(dsTrDetails.Tables[1].Rows[0]["parkwash_claimed"]) != 0)
                        {
                            Txt_WashingAllowance.Text = "0.00";
                        }
                        else
                        {
                            if (Txt_WashingAllowance.Text != "0.00")
                            {
                                decimal totalclaimamount;
                                totalclaimamount = Convert.ToDecimal(txt_TotalClaimAmount.Text) + Convert.ToDecimal(Txt_WashingAllowance.Text);
                                txt_TotalClaimAmount.Text = Convert.ToString(totalclaimamount);
                            }
                        }
                    }
                    else
                    {
                        txtTollCharges_Bal.Text = Convert.ToString(Convert.ToDecimal(txtTollCharges_Ent.Text));
                        Span_Toll_Balance.InnerText = "Toll Pass / Charges (Balance: " + Convert.ToString(Convert.ToDecimal(txtTollCharges_Ent.Text)) + ")";

                        if (Txt_WashingAllowance.Text != "0.00")
                        {
                            if (txt_TotalClaimAmount.Text =="")
                            {
                                txt_TotalClaimAmount.Text = "0.00";
                            }

                            decimal totalclaimamount;
                            totalclaimamount = Convert.ToDecimal(txt_TotalClaimAmount.Text) + Convert.ToDecimal(Txt_WashingAllowance.Text);
                            txt_TotalClaimAmount.Text = Convert.ToString(totalclaimamount);
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

    public void checkPastMoths_AlreadySubmits_Fromsub()
    {
        try
        {
            lblmessage.Text = "";
            #region date formatting

            string[] strdate;
            string strToDate = "";

            string strFromDate = "";
            string strFclmDate = "";
            if (Convert.ToString(Session["Fromdate"]).Trim() != "")
            {
                strdate = Convert.ToString(Session["Fromdate"]).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                strFromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(Session["sclamdt"]).Trim() != "")
            {
                strdate = Convert.ToString(Session["sclamdt"]).Trim().Split('/');
                strFclmDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }

            #endregion


            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[5];

            spars[0] = new SqlParameter("@sMonth", SqlDbType.VarChar);
            spars[0].Value = strToDate;

            spars[1] = new SqlParameter("@sclaimdate", SqlDbType.VarChar);
            spars[1].Value = strFromDate;

            spars[2] = new SqlParameter("@Empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            spars[3] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            if (Convert.ToString(hdnRemid.Value) != "" && Convert.ToString(hdnRemid.Value) != "0")
                spars[3].Value = hdnRemid.Value;
            else
                spars[3].Value = DBNull.Value;

            spars[4] = new SqlParameter("@Type", SqlDbType.VarChar);
            spars[4].Value = "Fuel";

            dsTrDetails = spm.getDatasetList(spars, "sp_check_Fuel_rem_validation");

            txtQuantity.Enabled = true;
            txtElgAmnt.Enabled = true;
            txtTollCharges.Enabled = true;
            if (dsTrDetails.Tables.Count > 0)
            {
               
                if (dsTrDetails.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["msg"]) != "")
                    {
                        txtQuantity.Enabled = false;
                       
                        txtTollCharges.Enabled = false;
                        if (HDEditFag.Value == "")
                        {
                            txtQuantity.Text = "";
                            txtElgAmnt.Text = "";
                            txtTollCharges.Text = "";
                            HDMainAmountCurrentMonth.Value = "";
                            Txt_BalanceAmountCurrentMonth.Text = "";
                            HDMainAmountMonthPrevious.Value = "";
                            Txt_BalanceAmountPreviousMonth.Text = "";
                            Txt_BlanceAmount.Text = "";
                            txtElgAmnt.Enabled = false;
                        }
                        

                        lblmessage.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["msg"]);
                        return;
                    }
                }
                if (dsTrDetails.Tables[1].Rows.Count > 0)
                {
                    HDAmountCurrentclaimed.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["Amount"]);
                }
            }
            get_emp_fule_eligibility_fromsub();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }

    }

    public void getApproverdata()
    {

        DataTable dtApproverEmailIds = new DataTable();
        if (Convert.ToString(hdnRemid.Value).Trim() == "")
            dtApproverEmailIds = spm.GeTfuelApproverEmailID_OrgFuel(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), 0);
        else
            dtApproverEmailIds = spm.GeTfuelApproverEmailID_OrgFuel(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToDecimal(hdnRemid.Value));

        dspaymentVoucher_Apprs.Tables.Add(dtApproverEmailIds);
        //IsEnabledFalse (true);
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
            lblmessage.Text = "Since you are not assigned under any approver you can not apply for any type of Fuel request, please contact HR";

        }
    }

    public void getTravle_Desk_COS_ApproverCode()
    {

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_ACCCOS_apprver_code_Rem";

        spars[1] = new SqlParameter("@trappr", SqlDbType.VarChar);
        spars[1].Value = "RCOS";

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = txtEmpCode.Text;


        dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");
        hdnIs_GFHCCEOGFO.Value = "";
        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            hdnApprovalCOS_Code.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_code"]).Trim();
            hdnApprovalCOS_ID.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_id"]).Trim();
            hdnApprovalCOS_mail.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();
            hdnApprovalCOS_Name.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_remarks"]).Trim();
        }
        
        if (dsTrDetails.Tables[1].Rows.Count > 0)
        {
            hdnIs_GFHCCEOGFO.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["E_ROLE"]).Trim();
        }
    }

    protected string GetApprove_RejectList(decimal dmaxremid)
    {
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        dtAppRej = spm.GeTfuelApproverEmailID_OrgFuel(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), dmaxremid);
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

    private string getFuelClaimDetails_table()
    {
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;

        #region Fuel Table
        if (dgFuelClaim.Rows.Count > 0)
        {
            //sbapp.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:80%;' border=1>");
            //sbapp.Append("<table style='border: 1px solid black' cellspacing='1' cellpading='1'>");
            //sbapp.Append("<table width='100%' cellspacing='0' cellpadding='0' border='0' align='center' bgcolor='#999999'>");
            sbapp.Append("<table width='100%' cellspacing='0' cellpadding='0' border='1' align='center'>");
            sbapp.Append("<tr>");
            sbapp.Append("<td colspan=5>Fuel Claim Details</td>");

            sbapp.Append("</tr>");


            sbapp.Append("<tr>");
            sbapp.Append("<td>Date</td>");
            sbapp.Append("<td>Quantity</td>");
            sbapp.Append("<td>Amount</td>");
            sbapp.Append("<td>Rate</td>");
            sbapp.Append("<td>Toll Charges</td>");
            sbapp.Append("</tr>");

            for (int i = 0; i < dgFuelClaim.Rows.Count; i++)
            {
                sbapp.Append("<tr>");
                sbapp.Append("<td>" + Convert.ToString(dgFuelClaim.Rows[i].Cells[0].Text).Trim() + "</td>");
                sbapp.Append("<td>" + Convert.ToString(dgFuelClaim.Rows[i].Cells[1].Text).Trim() + "</td>");
                sbapp.Append("<td>" + Convert.ToString(dgFuelClaim.Rows[i].Cells[2].Text).Trim() + "</td>");
                sbapp.Append("<td>" + Convert.ToString(dgFuelClaim.Rows[i].Cells[3].Text).Trim() + "</td>");
                sbapp.Append("<td>" + Convert.ToString(dgFuelClaim.Rows[i].Cells[4].Text).Trim() + "</td>");
                sbapp.Append("</tr>");
            }
            sbapp.Append("</table>");
        }
        #endregion

        //return Convert.ToString(sbapp);
        return Convert.ToString("");
    }


    private void getFromdateTodate_FroEmail()
    {
        try
        {

            string[] strdate;
            string strfromDate = "";


            if (dgFuelClaim.Rows.Count > 0)
            {
                strfromDate = Convert.ToString(dgFuelClaim.Rows[0].Cells[0].Text).Trim();

                if (Convert.ToString(strfromDate).Trim() != "")
                {
                    strdate = Convert.ToString(strfromDate).Trim().Split('/');
                    strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                }
            }
            else
            {
                if (Convert.ToString(txt_parkwash_month.Text).Trim() != "")
                {
                    DateTime dt = DateTime.ParseExact(txt_parkwash_month.Text, "MMM/yyyy", CultureInfo.InvariantCulture);
                    strfromDate = dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
            }
            #region date formatting



            #endregion


            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_fuelclaimMonth";

            spars[1] = new SqlParameter("@formatdate", SqlDbType.VarChar);
            spars[1].Value = strfromDate;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                hdnfrmdate_emial.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["fuelclaimMonth"]).Trim();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }


    public void checkPastMoths_AlreadySubmits_CarparkingWashingCharges()
    {
        try
        {
            lblmessage.Text = "";
            #region date formatting

            string[] strdate;
            string strFromDate = "";

            string strToDate = "";
            if (Convert.ToString(txtFromdateMain.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtFromdateMain.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }

            if (Convert.ToString(txt_parkwash_month.Text).Trim() != "")
            {
                //strdate = Convert.ToString(txt_parkwash_month.Text).Trim().Split('/');
                //  strFromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + "01"; //Convert.ToString(strdate[0]);
            }



            #endregion


            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[5];

            spars[0] = new SqlParameter("@sMonth", SqlDbType.VarChar);
            spars[0].Value = strToDate;

            spars[1] = new SqlParameter("@sclaimdate", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(txt_parkwash_month.Text).Trim();

            spars[2] = new SqlParameter("@Empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            spars[3] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[3].Value = DBNull.Value;

            spars[4] = new SqlParameter("@Type", SqlDbType.VarChar);
            spars[4].Value = "car_parking_washing_claim";

            DataSet dcarparkingWashingClaim = spm.getDatasetList(spars, "sp_check_Fuel_rem_validation");

            if (dcarparkingWashingClaim.Tables.Count > 0)
            {
                if (dcarparkingWashingClaim.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(dcarparkingWashingClaim.Tables[0].Rows[0]["msg"]) != "")
                    {
                        lblmessage.Text = Convert.ToString(dcarparkingWashingClaim.Tables[0].Rows[0]["msg"]);
                        return;
                    }
                }
            }
            else
            {
                get_emp_fule_eligibility();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }

    }


    public void getFueClaimDetailsEdit()
    {
        DataTable dtMobileDetails = new DataTable();
        dtMobileDetails = spm.GetFuelClaimDetailsEdit(Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnClaimsID.Value), Convert.ToString(txtEmpCode.Text));

        if (dtMobileDetails.Rows.Count > 0)
        {
            txtFromdate.Text = Convert.ToString(dtMobileDetails.Rows[0]["Rem_Month"]);
            txtElgAmnt.Text = Convert.ToString(dtMobileDetails.Rows[0]["Amount"]);
            txtEligibleQty.Text = Convert.ToString(dtMobileDetails.Rows[0]["Eligible_Qty"]);
            txtBalanceQty.Text = Convert.ToString(dtMobileDetails.Rows[0]["Balance_Qty"]);
            hdnfuelQty.Value = Convert.ToString(dtMobileDetails.Rows[0]["Quantity"]);
            if (HDMainAmountMonthPrevious.Value == Txt_BalanceAmountPreviousMonth.Text)
            {
                Txt_BalanceAmountCurrentMonth.Text = Convert.ToString(Convert.ToDecimal(Txt_BalanceAmountCurrentMonth.Text) + Convert.ToDecimal(txtElgAmnt.Text));
            }
            else if (HDMainAmountMonthPrevious.Value != Txt_BalanceAmountPreviousMonth.Text)
            {
                string strbapm = Txt_BalanceAmountPreviousMonth.Text;
                if (Convert.ToDecimal(HDMainAmountMonthPrevious.Value) >= Math.Abs(Convert.ToDecimal(strbapm) + Convert.ToDecimal(txtElgAmnt.Text)))
                {
                    Txt_BalanceAmountPreviousMonth.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(strbapm) + Convert.ToDecimal(txtElgAmnt.Text)));
                }
                else
                {
                    string stramttt = Convert.ToString(Math.Abs(Convert.ToDecimal(HDMainAmountMonthPrevious.Value) - Convert.ToDecimal(strbapm)));
                    Txt_BalanceAmountPreviousMonth.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(strbapm) + Convert.ToDecimal(stramttt)));

                    string stram = Convert.ToString(Math.Abs(Convert.ToDecimal(stramttt) - Convert.ToDecimal(txtElgAmnt.Text)));
                    Txt_BalanceAmountCurrentMonth.Text = Convert.ToString(Convert.ToDecimal(Txt_BalanceAmountCurrentMonth.Text) + Convert.ToDecimal(stram));
                }
            }

        }

    }

    protected void btn_del_File_Click1(object sender, EventArgs e)
    {
        Int32 File_id = 0;
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnRemid.Value = Convert.ToString(gvfuel_claimsFiles.DataKeys[row.RowIndex].Values[0]).Trim();
        File_id = Convert.ToInt32(gvfuel_claimsFiles.DataKeys[row.RowIndex].Values[1]);
        string File_name = Convert.ToString(gvfuel_claimsFiles.Rows[row.RowIndex].Cells[0].Text).Trim();
        string FuelclaimPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["FuelClaimsDocumentpath"]).Trim() + Convert.ToString(hdnRemid.Value) + "/");
        string[] Files = Directory.GetFiles(FuelclaimPath);
        foreach (string file in Files)
        {
            if (file.ToString() == File_name)
                File.Delete(file);
        }
        hdnsptype.Value = "DeleteFuelFile";

        SqlParameter[] spar = new SqlParameter[4];

        spar[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spar[0].Value = hdnsptype.Value;
        spar[1] = new SqlParameter("@Rem_id", SqlDbType.Int);
        spar[1].Value = Convert.ToInt32(hdnRemid.Value);
        spar[2] = new SqlParameter("@FuelClaims_id", SqlDbType.Int);
        spar[2].Value = Convert.ToInt32(File_id);
        DataSet DSUserRole = spm.getDatasetList(spar, "SP_Insert_Fuel_OutstationDetails");
        get_employee_FuelUploaded_Files();
    }

    protected void fuel_btncancel_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }

        //Session["Insertsecond"] = null;
        hdnEligible.Value = "Cancellation";
        string strapprovermails = "";
        getTravle_Desk_COS_ApproverCode();
        strapprovermails = GetApprove_RejectList(Convert.ToDecimal(hdnRemid.Value));
        string strFuelClaimtable = "";
        strFuelClaimtable = getFuelClaimDetails_table();
        getFromdateTodate_FroEmail();
        string strcliamMonth = Convert.ToString(hdnfrmdate_emial.Value).Trim();

        spm.InsertFuelClaimDetails(Convert.ToInt32(hdnRemid.Value), null, txtEmpCode.Text, 0, 0, 0, 0, 0, 0, "CancelFuelRem", 0);
        spm.Fuel_send_mail_CancelNew(txtEmpName.Text, hdnApprovalCOS_mail.Value, "Request for Fuel bill Reimbursement of", txt_TotalClaimAmount.Text, GetApprove_RejectList(Convert.ToDecimal(hdnRemid.Value)), strapprovermails, "", "", strFuelClaimtable, strcliamMonth);
        Session["tollchrgs"] = "";
        Session["airportparking"] = "";
        Session["parkwashclaimed"] = "";
        Response.Redirect("~/procs/Fuel.aspx");
    }

    protected void mobile_btnPrintPV_Click(object sender, EventArgs e)
    {
        getApproverdata();
        getPayementVoucher_forPrint();
    }

    private void getPayementVoucher_forPrint()
    {
        try
        {
            #region get payment Voucher details
            DataSet dspaymentVoucher = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "fuel_claim";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDecimal(hdnRemid.Value);

            dspaymentVoucher = spm.getDatasetList(spars, "rpt_dataProcedure");

            HDTotaltillAmount.Value = dspaymentVoucher.Tables[6].Rows[0]["Amountt"].ToString();

            Fill_Summary_Grid();

            #endregion

            if (dspaymentVoucher.Tables[0].Rows.Count > 0)
            {
                ReportViewer ReportViewer1 = new ReportViewer();


                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.LocalReport.DataSources.Clear();

                ReportParameter[] param = new ReportParameter[17];
                param[0] = new ReportParameter("pdocno", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Vouno"]));
                param[1] = new ReportParameter("ppvdate", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Rem_Sub_Date"]));
                param[2] = new ReportParameter("pempName", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Emp_Name"]));
                param[3] = new ReportParameter("pempCode", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Empcode"]));
                param[4] = new ReportParameter("pRsinwords", Convert.ToString(dspaymentVoucher.Tables[2].Rows[0]["AmountinWords"]));

                param[5] = new ReportParameter("pFuelClaimheads", Convert.ToString("Fuel Claim Details"));

                if (dspaymentVoucher.Tables[4].Rows.Count > 0)
                    param[6] = new ReportParameter("pFuelOutClaimheads", Convert.ToString("Outsation Details"));
                else
                    param[6] = new ReportParameter("pFuelOutClaimheads", "");


                #region Cost Cente & Bank Details
                if (dspaymentVoucher.Tables[5].Rows.Count > 0)
                {
                    param[7] = new ReportParameter("pCostCenterCode", Convert.ToString(dspaymentVoucher.Tables[5].Rows[0]["Cost_Center"]));
                    param[8] = new ReportParameter("pCostCenterNM", Convert.ToString(dspaymentVoucher.Tables[5].Rows[0]["Cost_Center_desc"]));

                    param[9] = new ReportParameter("pBankName", Convert.ToString(dspaymentVoucher.Tables[5].Rows[0]["BANK_name"]));
                    param[10] = new ReportParameter("pBankAccCode", Convert.ToString(dspaymentVoucher.Tables[5].Rows[0]["BANK_acc"]));
                    param[11] = new ReportParameter("pBankIFSCcode", Convert.ToString(dspaymentVoucher.Tables[5].Rows[0]["BANK_ifsc"]));
                    param[12] = new ReportParameter("pBankBranch", Convert.ToString(dspaymentVoucher.Tables[5].Rows[0]["BANK_Branch"]));

                    param[13] = new ReportParameter("pRbnkName", Convert.ToString(dspaymentVoucher.Tables[5].Rows[0]["R_BANK_name"]));
                    param[14] = new ReportParameter("pRBnkAccCode", Convert.ToString(dspaymentVoucher.Tables[5].Rows[0]["R_BANK_acc"]));
                    param[15] = new ReportParameter("pRbnkIFSCcode", Convert.ToString(dspaymentVoucher.Tables[5].Rows[0]["R_BANK_ifsc"]));
                    param[16] = new ReportParameter("pRbnkBranch", Convert.ToString(dspaymentVoucher.Tables[5].Rows[0]["R_BANK_Branch"]));
                }
                else
                {
                    param[7] = new ReportParameter("pCostCenterCode", Convert.ToString(""));
                    param[8] = new ReportParameter("pCostCenterNM", Convert.ToString(""));

                    param[9] = new ReportParameter("pBankName", Convert.ToString(""));
                    param[10] = new ReportParameter("pBankAccCode", Convert.ToString(""));
                    param[11] = new ReportParameter("pBankIFSCcode", Convert.ToString(""));
                    param[12] = new ReportParameter("pBankBranch", Convert.ToString(""));

                    param[13] = new ReportParameter("pRbnkName", Convert.ToString(""));
                    param[14] = new ReportParameter("pRBnkAccCode", Convert.ToString(""));
                    param[15] = new ReportParameter("pRbnkIFSCcode", Convert.ToString(""));
                    param[16] = new ReportParameter("pRbnkBranch", Convert.ToString(""));
                }
                #endregion

                // Create Report DataSource
                ReportDataSource rds = new ReportDataSource("dspaymentVoucher", dspaymentVoucher.Tables[0]);
                ReportDataSource rdsApprs = new ReportDataSource("dspaymentVoucher_Apprs", dspaymentVoucher_Apprs.Tables[0]);
                ReportDataSource rdsFuelClaims = new ReportDataSource("dsfuelClaimdtls", dspaymentVoucher.Tables[3]);
                ReportDataSource rdsFuelClaimsOutstn = new ReportDataSource("dsfuelclaimOutstaiondtls", dspaymentVoucher.Tables[4]);
                ReportDataSource rdsFuelSumm = new ReportDataSource("ds_Fuel_Summ", Summ);


                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/PaymentVoucher_FuelNew.rdlc");

                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.DataSources.Add(rdsApprs);

                ReportViewer1.LocalReport.DataSources.Add(rdsFuelClaimsOutstn);
                ReportViewer1.LocalReport.DataSources.Add(rdsFuelClaims);
                ReportViewer1.LocalReport.DataSources.Add(rdsFuelSumm);

                ReportViewer1.LocalReport.SetParameters(param);
                ReportViewer1.LocalReport.Refresh();

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
            Response.Write(ex.Message);
            Response.End();
        }
    }

    private void Fill_Summary_Grid()
    {
        Decimal dTotalClaimAmt_out = 0;
        Decimal douttrvltot = 0;
        Decimal dtot_Outquantity = 0;

        Decimal dtotclaimAmt = 0, dtotquantity = 0;

        for (Int32 irow = 0; irow < dgFuelClaim.Rows.Count; irow++)
        {
            if (Convert.ToString(dgFuelClaim.Rows[irow].Cells[1].Text).Trim() != "")
                dtotquantity += Convert.ToDecimal(dgFuelClaim.Rows[irow].Cells[1].Text);

            if (Convert.ToString(dgFuelClaim.Rows[irow].Cells[2].Text).Trim() != "")
                dtotclaimAmt += Convert.ToDecimal(dgFuelClaim.Rows[irow].Cells[2].Text);


        }

        DataTable dt = new DataTable();
        Summ = new DataTable();
        if (dt.Columns.Count == 0)
        {
            dt.Columns.Add("COL1", typeof(string));
            dt.Columns.Add("COL2", typeof(string));
            dt.Columns.Add("COL3", typeof(string));
            dt.Columns.Add("COL4", typeof(string));
        }

        string amtdtotclaimAmt, amtYearEligibility, amtBalanceEligibility, amtdTotalClaimAmt_out = "";
        CultureInfo hindi = new CultureInfo("hi-IN");

        decimal parsed = decimal.Parse(Convert.ToString(dtotclaimAmt), CultureInfo.InvariantCulture);
        amtdtotclaimAmt = string.Format(hindi, "{0:#,#0.00}", parsed);

        decimal parsed2 = decimal.Parse(Convert.ToString(dTotalClaimAmt_out), CultureInfo.InvariantCulture);
        amtdTotalClaimAmt_out = string.Format(hindi, "{0:#,#0.00}", parsed2);

        decimal parsed3 = decimal.Parse(Convert.ToString(HDYearEligibility.Value), CultureInfo.InvariantCulture);
        amtYearEligibility = string.Format(hindi, "{0:#,#0.00}", parsed3);

        DataRow NewRow = dt.NewRow();
        NewRow[0] = "Parking / Washing Allowance: ";
        NewRow[1] = Convert.ToString(Convert.ToDecimal(Txt_WashingAllowance.Text));
        NewRow[2] = "Yearly Eligibility: ";
        NewRow[3] = amtYearEligibility;
        dt.Rows.Add(NewRow);

        NewRow = dt.NewRow();
        NewRow[0] = "Fuel Charges: ";
        NewRow[1] = Convert.ToString(amtdtotclaimAmt);
        NewRow[2] = "";
        NewRow[3] = "";
        dt.Rows.Add(NewRow);

        NewRow = dt.NewRow();
        NewRow[0] = "Toll Pass: ";
        NewRow[1] = txtTollCharges.Text;
        NewRow[2] = "";
        NewRow[3] = "";
        dt.Rows.Add(NewRow);

        NewRow = dt.NewRow();
        NewRow[0] = "Airport Parking: ";
        NewRow[1] = txt_airport_parking.Text;
        NewRow[2] = "Amount Claimed till date: ";
        NewRow[3] = HDTotaltillAmount.Value;
        dt.Rows.Add(NewRow);

        if (HDYearEligibility.Value == "")
        {
            HDYearEligibility.Value = "0.00";
        }
        if (HDTotaltillAmount.Value == "")
        {
            HDTotaltillAmount.Value = "0.00";
        }

        decimal dcramteligilibity = Convert.ToDecimal(HDYearEligibility.Value) - Convert.ToDecimal(HDTotaltillAmount.Value);
        decimal parsed4 = decimal.Parse(Convert.ToString(dcramteligilibity), CultureInfo.InvariantCulture);
        amtBalanceEligibility = string.Format(hindi, "{0:#,#0.00}", parsed4);

        decimal parsed5 = decimal.Parse(Convert.ToString(txt_TotalClaimAmount.Text), CultureInfo.InvariantCulture);
        string amtTotalClaimAmount = string.Format(hindi, "{0:#,#0.00}", parsed5);

        NewRow = dt.NewRow();
        NewRow[0] = "Amount Claimed for the Month: ";
        NewRow[1] = amtTotalClaimAmount;
        NewRow[2] = "Balance Eligibility: ";
        NewRow[3] = amtBalanceEligibility;
        dt.Rows.Add(NewRow);
        Summ = dt.Copy();

    }

}


    #endregion
