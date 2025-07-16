using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ABAP_Object_Tracker_Plan_for_Approval : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();

    #region Creative_Default_methods


    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    public DataTable dtEmp;
    public DataSet dsDirecttaxSectionList = new DataSet();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }
    #endregion

    #region Page_Events

    private void Page_Load(object sender, System.EventArgs e)
    {
        try

        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            lblmessage.Text = "";



            txtEmpCode.Text = Convert.ToString(Session["Empcode"]);

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/ABAP_Object_Tracker_Index.aspx");
            }
            else
            {
                Page.SmartNavigation = true;

                if (!Page.IsPostBack)
                {
                    getProjectLocation();
                    DDLProjectLocation.Enabled = false;
                    if (Request.QueryString.Count > 0)
                    {
                        hdnABAPODUploadId.Value = Convert.ToString(Request.QueryString["ABAPODId"]).Trim();
                        get_ABAP_Object_Submitted_Plan_Details_View();

                    }

                }

            }

        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }


    protected void lstTripType_SelectedIndexChanged(object sender, EventArgs e)
    {
        clear_POWO_Cntrls();
        clear_Milestone_Cntrls();
    }

    protected void trvldeatils_delete_btn_Click(object sender, EventArgs e)
    {

    }

    protected void trvldeatils_cancel_btn_Click(object sender, EventArgs e)
    {
    }



    protected void lnkfile_Invoice_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBInvoicefiles"]).Trim()), "");
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
    public void GetExpeses_Details()
    {
        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Expenses_Details_List";

        spars[1] = new SqlParameter("@VendorId", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(0);


        dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        if (dsProjectsVendors.Tables[0].Rows.Count > 0)
        {

        }
    }

    private void Calculate_DirectTax_Amount()
    {

        Decimal dDirectTaxPercentage_Amt = 0;
        Decimal dDirectTaxPercentage = 0;

        Decimal rCGST = 0;
        Decimal rSGST = 0;
        Decimal rIGST = 0;

        #region format Currency

        DataSet dtPOWODetails = new DataSet();
        decimal dinvoiceAmt = 0, dCGSTAmt = 0, dSGSTAmt = 0, dIGSTAmt = 0, dInvoiceAmtWithtax = 0;

        dtPOWODetails = spm.getFormated_Amount("get_formated_Invoice_Amount", dinvoiceAmt, dCGSTAmt, dSGSTAmt, dIGSTAmt, dInvoiceAmtWithtax);


        if (dtPOWODetails.Tables[0].Rows.Count > 0)
        {
        }


        #endregion

    }

    private void Calculate_DirectTax_Amount_ACC()
    {



        Decimal dDirectTaxPercentage_Amt = 0;
        Decimal dDirectTaxPercentage = 0;
        Decimal SecurityDeposit = 0, Adv_Settlement_Amt = 0, InvoiceAmtWithoutGST_ACC = 0;
        Decimal rCGST = 0;
        Decimal rSGST = 0;
        Decimal rIGST = 0;
        Decimal Adv_Settlment_Amt = 0;
        decimal dGSTAmt = 0;



        int j = 0;



        int J = 0;
        if (J == 1)
        {
            DataSet dtAdvDetails = new DataSet();
            decimal Adv_Bal_Amt1 = 0;

            Adv_Bal_Amt1 = Convert.ToDecimal(Convert.ToDecimal("") - Convert.ToDecimal(Adv_Settlement_Amt) - Convert.ToDecimal(SecurityDeposit));

            //if (Adv_Bal_Amt1 < 0)
            //    Adv_Bal_Amt1 = 0;

            dtAdvDetails = spm.getFormated_Amount("get_formated_Invoice_Amount", Adv_Bal_Amt1, 0, 0, 0, 0);
            if (dtAdvDetails.Tables[0].Rows.Count > 0)
            {
            }
        }
        else
        {

        }
        #region format Currency

        DataSet dtPOWODetails = new DataSet();
        decimal dinvoiceAmt = 0, dCGSTAmt = 0, dSGSTAmt = 0, dIGSTAmt = 0, dInvoiceAmtWithtax = 0;

        if (Convert.ToString("Value").Trim() != "")
            dCGSTAmt = Convert.ToDecimal("Value");

        dtPOWODetails = spm.getFormated_Amount("get_formated_Invoice_Amount", dinvoiceAmt, dCGSTAmt, dSGSTAmt, dIGSTAmt, dInvoiceAmtWithtax);


        if (dtPOWODetails.Tables[0].Rows.Count > 0)
        {
        }


        #endregion

    }

    public void PopulateEmployeeData()
    {
        try
        {
            dtEmp = spm.GetEmployeeData(txtEmpCode.Text);
            if (dtEmp.Rows.Count > 0)
            {
                lpm.Emp_Code = txtEmpCode.Text;
                lpm.Emp_Status = (string)dtEmp.Rows[0]["Emp_status"];
                lpm.Emp_Name = (string)dtEmp.Rows[0]["Emp_Name"];
                lpm.Designation_name = (string)dtEmp.Rows[0]["DesginationName"];
                lpm.department_name = (string)dtEmp.Rows[0]["Department_Name"];
                lpm.Grade = Convert.ToString(dtEmp.Rows[0]["Grade"]).Trim();
                lpm.EmailAddress = (string)dtEmp.Rows[0]["Emp_EmailAddress"];

                hflGrade.Value = lpm.Grade;
                hflEmailAddress.Value = lpm.EmailAddress;
                hflEmpName.Value = lpm.Emp_Name;
                hflEmpDepartment.Value = lpm.department_name;
                hflEmpDesignation.Value = lpm.Designation_name;

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }

    public void clear_POWO_Cntrls()
    {


    }

    public void clear_Milestone_Cntrls()
    {
        hdnSrno.Value = "";

    }


    public void GetPODetails_List()
    {
        DataTable dtPOWODetails = new DataTable();
        dtPOWODetails = spm.get_ALLPOWOList(txtEmpCode.Text);
        if (dtPOWODetails.Rows.Count > 0)
        {


        }
    }

    public void GetPOTypes()
    {
        DataTable dtPOWODetails = new DataTable();
        dtPOWODetails = spm.getPOTypes();
        if (dtPOWODetails.Rows.Count > 0)
        {


        }
    }




    public string ReplaceInvalidChars(string filename)
    {
        Regex illegalInFileName = new Regex(@"[#%&{}\!/<'>@+*?$|=]");
        string myString = illegalInFileName.Replace(filename, "_");
        //return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        return myString;
    }

    public void getInvoiceUploadedFiles()
    {

        DataSet dsFiles = spm.getVSCB_UploadedFiles("get_VSCB_UploadedFiels", "Invoice", Convert.ToDouble(hdnInvoiceId.Value), "", "");

        //gvuploadedFiles.DataSource = null;
        //gvuploadedFiles.DataBind();
        //if (dsFiles.Tables[0].Rows.Count > 0)
        //{
        //    gvuploadedFiles.DataSource = dsFiles;
        //    gvuploadedFiles.DataBind();
        //    spnSupportinFiles.Visible = true;
        //}
    }


    public void get_ProjectDept_Vendor_List()
    {

        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Project_Dept_Vendor_List";

        dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");


        if (dsProjectsVendors.Tables[3].Rows.Count > 0)
        {

        }

    }


    public void get_DirectTaxTypeList()
    {

        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_DirectTaxType_List";

        dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");


    }


    public void get_DirectTaxSectionsList()
    {

        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_TDS_Sections";

        dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        dsDirecttaxSectionList = dsProjectsVendors;

    }

    public void get_DirectTax_Percentage()
    {

        DataSet dsTDSPer = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_TDS_Percentage";

        spars[1] = new SqlParameter("@Srno", SqlDbType.Int);
        spars[1].Value = Convert.ToInt32(0);


        dsTDSPer = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");


    }

    public void get_DirectTax_Percentage_ACC(string isrno, TextBox txtDirectTaxPercentage_ACC)
    {

        DataSet dsTDSPer = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_TDS_Percentage";

        spars[1] = new SqlParameter("@Srno", SqlDbType.Int);
        spars[1].Value = Convert.ToInt32(isrno);


        dsTDSPer = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        txtDirectTaxPercentage_ACC.Text = "";
        if (dsTDSPer.Tables[0].Rows.Count > 0)
        {
            txtDirectTaxPercentage_ACC.Text = Convert.ToString(dsTDSPer.Tables[0].Rows[0]["TDS_Percentage"]).Trim();
        }
    }
    public void check_POWO_IsForLoginEployeee()
    {
        DataSet dsEmployee = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "Check_Invoice_forLoginEmployee";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode.Text);

        spars[2] = new SqlParameter("@InvoiceId", SqlDbType.BigInt);
        spars[2].Value = Convert.ToDouble(hdnInvoiceId.Value);

        dsEmployee = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        if (dsEmployee.Tables[0].Rows.Count == 0)
        {
            Response.Redirect("~/procs/vscb_index.aspx");
        }
    }
    public void get_Check_Approver_IsInvoice_Approved()
    {

        DataSet dsapproved = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_Checked_Approver_Approved_Invocie";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

        spars[2] = new SqlParameter("@InvoiceId", SqlDbType.BigInt);
        spars[2].Value = Convert.ToDouble(hdnInvoiceId.Value);

        dsapproved = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

        if (dsapproved.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToString(dsapproved.Tables[0].Rows[0]["Action"]).Trim() == "Approved")
            {
                //trvl_btnSave.Visible = false;
                //btnCancel.Visible = false;
                //btnCorrection.Visible = false;

                if (Convert.ToString(dsapproved.Tables[0].Rows[0]["Appr_ID"]).Trim() == "102")
                {
                }
            }
        }

    }

    public void UpdateOn_Final_TDSPercetage_TDS_Section_ID(int itds_DescripId, string dTds_Per, string dmilestoneID, decimal iamtwithoutTax, decimal Adv_Settlement_Amt, decimal SecDeposit_Settlement_Amt)
    {

        Decimal dDirectTaxPercentage_Amt = 0;
        Decimal dDirectTaxPercentage = 0;

        if (Convert.ToString(dTds_Per).Trim() != "")
            dDirectTaxPercentage = Convert.ToDecimal(dTds_Per);

        if (Convert.ToString(iamtwithoutTax).Trim() != "")
            dDirectTaxPercentage_Amt = Convert.ToDecimal(iamtwithoutTax);


        if (SecDeposit_Settlement_Amt > 0)
            dDirectTaxPercentage_Amt += SecDeposit_Settlement_Amt;

        dDirectTaxPercentage_Amt = Math.Round(dDirectTaxPercentage_Amt * dDirectTaxPercentage / 100, 2);


        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[7];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "UpdateInvoice_Milestone_TDS_Amt";

        spars[1] = new SqlParameter("@DirectTax_DescriptionID", SqlDbType.Int);
        spars[1].Value = itds_DescripId;

        spars[2] = new SqlParameter("@DirectTax_Percentage", SqlDbType.Decimal);
        spars[2].Value = dDirectTaxPercentage;

        spars[3] = new SqlParameter("@MstoneID", SqlDbType.BigInt);
        spars[3].Value = Convert.ToDouble(dmilestoneID);

        spars[4] = new SqlParameter("@DirectTax_Amount", SqlDbType.Decimal);
        spars[4].Value = dDirectTaxPercentage_Amt;

        spars[5] = new SqlParameter("@InvoiceID", SqlDbType.BigInt);
        spars[5].Value = Convert.ToDouble(hdnInvoiceId.Value);

        spars[6] = new SqlParameter("@ADSettlement_Amt", SqlDbType.Decimal);
        spars[6].Value = Convert.ToDouble(Adv_Settlement_Amt);

        dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_CreateInvoice");

    }

    public void UpdateOn_Final_Security_DepositAmount()
    {



        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "UpdateInvoice_Secutity_SettlemtAmt";

        spars[1] = new SqlParameter("@InvoiceID", SqlDbType.BigInt);
        spars[1].Value = Convert.ToDouble(hdnInvoiceId.Value);

        spars[2] = new SqlParameter("@ADSettlement_Amt", SqlDbType.Decimal);
        spars[2].Value = Convert.ToDouble("");

        dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_CreateInvoice");

    }

    public void get_Milestone_TDS_Details(DropDownList lstDirectTaxSections_ACC, TextBox txtDirectTaxPercentage_ACC, CheckBox chkLDC_Applicable_ACC, string sMstoneID)
    {

        DataSet dsMilestoneTDS = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_milestone_TDS_details";

        spars[1] = new SqlParameter("@MstoneID", SqlDbType.BigInt);
        spars[1].Value = Convert.ToDouble(sMstoneID);

        spars[2] = new SqlParameter("@InvoiceId", SqlDbType.BigInt);
        spars[2].Value = Convert.ToDouble(hdnInvoiceId.Value);

        dsMilestoneTDS = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        if (dsMilestoneTDS.Tables[0].Rows.Count > 0)
        {
            lstDirectTaxSections_ACC.SelectedValue = Convert.ToString(dsMilestoneTDS.Tables[0].Rows[0]["TDS_Descript_ID"]);
            txtDirectTaxPercentage_ACC.Text = Convert.ToString(dsMilestoneTDS.Tables[0].Rows[0]["DirectTax_Percentage"]);
            if (Convert.ToString(dsMilestoneTDS.Tables[0].Rows[0]["IsLDC"]) == "true")
                chkLDC_Applicable_ACC.Checked = true;
            else
                chkLDC_Applicable_ACC.Checked = false;
        }

    }

    #endregion
    protected void lnkfile_PO_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBSignPOWOfiles"]).Trim()), "value");
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
    protected void btnTra_Details_Click(object sender, EventArgs e)
    {
        DataSet dsPOWOContent = new DataSet();

        #region get POWO Content 


        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_POWO_Content_FromTally";

        spars[1] = new SqlParameter("@POWO_Number", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString("value").Trim();  //"PO/042021/00001"; 

        spars[2] = new SqlParameter("@POWOID", SqlDbType.Decimal);
        spars[2].Value = Convert.ToDouble(hdnPOWOID.Value);  //"PO/042021/00001";


        dsPOWOContent = spm.getDatasetList(spars, "SP_VSCB_Reports_Details");

        #endregion

        try
        {
            string strpath = Server.MapPath("~/procs/VSCB_Rpt_POWO_Content_New.rdlc");
            string PowoNumber = Convert.ToString("value").Trim().Replace("/", "-");
            LocalReport ReportViewer2 = new LocalReport();
            ReportViewer2.ReportPath = strpath;// Server.MapPath(strpath);
            ReportDataSource rds = new ReportDataSource("dspowoContent", dsPOWOContent.Tables[0]);
            ReportDataSource rds_2 = new ReportDataSource("dsowoDetails", dsPOWOContent.Tables[1]);
            ReportDataSource rds_3 = new ReportDataSource("dsMilestone", dsPOWOContent.Tables[2]);
            ReportDataSource rds_4 = new ReportDataSource("dsPOWOAmountinWords", dsPOWOContent.Tables[3]);

            ReportDataSource rds_5 = new ReportDataSource("dsPOPreparedBy", dsPOWOContent.Tables[4]);
            ReportDataSource rds_6 = new ReportDataSource("dsPOCheckedBy", dsPOWOContent.Tables[5]);
            ReportDataSource rds_7 = new ReportDataSource("dsPOApprovedBY_HOD", dsPOWOContent.Tables[6]);
            ReportDataSource rds_8 = new ReportDataSource("dsPOApprovedBY_COO", dsPOWOContent.Tables[7]);
            ReportDataSource rds_9 = new ReportDataSource("dsPOApprovedBY_CEO", dsPOWOContent.Tables[8]);

            ReportViewer2.DataSources.Clear();
            ReportViewer2.DataSources.Add(rds);
            ReportViewer2.DataSources.Add(rds_2);
            ReportViewer2.DataSources.Add(rds_3);
            ReportViewer2.DataSources.Add(rds_4);

            ReportViewer2.DataSources.Add(rds_5);
            ReportViewer2.DataSources.Add(rds_6);
            ReportViewer2.DataSources.Add(rds_7);
            ReportViewer2.DataSources.Add(rds_8);
            ReportViewer2.DataSources.Add(rds_9);
            ReportViewer2.Refresh();

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
            Response.AppendHeader("Content-Disposition", "attachment; filename=POWO_" + PowoNumber + "." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();
        }
    }
    protected void chkLDC_Applicable_CheckedChanged(object sender, EventArgs e)
    {


    }
    protected void lstExpenses_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region get Expeses List
        GetExpeses_Details();

        #endregion
    }

    protected void dgCostcenterList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList lstMilestoneAmtCostCenter = (e.Row.FindControl("lstMilestoneAmtCostCenter") as DropDownList);
            DataSet dsCostcenters = GetCostcenter_list();
            lstMilestoneAmtCostCenter.DataSource = dsCostcenters.Tables[0];
            lstMilestoneAmtCostCenter.DataTextField = "CostCentre";
            lstMilestoneAmtCostCenter.DataValueField = "Dept_ID";
            lstMilestoneAmtCostCenter.DataBind();


        }
    }
    public DataSet GetCostcenter_list()
    {
        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_CostCenter_list_forInvoice";


        dsProjectsVendors = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
        return dsProjectsVendors;
    }
    protected void txtCostCenterAmt_TextChanged(object sender, EventArgs e)
    {
        TextBox txtCostCenterAmt = (TextBox)sender;
        GridViewRow row = (GridViewRow)txtCostCenterAmt.NamingContainer;

        DropDownList lstMilestoneAmtCostCenter = (DropDownList)row.FindControl("lstMilestoneAmtCostCenter");


        if (Convert.ToString(lstMilestoneAmtCostCenter.SelectedValue).Trim() == "0")
        {
            txtCostCenterAmt.Text = "";
            return;
        }
        bool blnval = Validate_CostcenterAmt(txtCostCenterAmt, "N");
    }
    protected bool Validate_CostcenterAmt(TextBox txtCostcenterAmt, string sflag)
    {
        bool blnValidate = true;
        Int32 iduplicateCnt = 0;

        decimal dCostcenterAmt = 0;

        if (Convert.ToString("").Trim() != "")
        {
            //if (dCostcenterAmt > Convert.ToDecimal(txtAmtWithTax_Invoice.Text))
            if (dCostcenterAmt > Convert.ToDecimal(""))
            {
                txtCostcenterAmt.Text = "";
                blnValidate = false;
            }
            if (Convert.ToString(sflag).Trim() == "Y")
            {
                if (dCostcenterAmt < Convert.ToDecimal(""))
                {
                    blnValidate = false;
                }
            }


        }

        return blnValidate;
    }
    protected void btnback_mng_Click(object sender, EventArgs e)
    {

        Response.Redirect("ABAP_Object_Tracker_PendingList.aspx");

    }


    protected void lstDirectTaxSections_ACC_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList lstDirectTaxSections_ACC = (DropDownList)sender;
        GridViewRow row = (GridViewRow)lstDirectTaxSections_ACC.NamingContainer;

        TextBox txtDirectTaxPercentage_ACC = (TextBox)row.FindControl("txtDirectTaxPercentage_ACC");
        TextBox txtInvoiceAmtWithoutGST_ACC = (TextBox)row.FindControl("txtInvoiceAmtWithoutGST_ACC");
        CheckBox chkLDC_Applicable_ACC = (CheckBox)row.FindControl("chkLDC_Applicable_ACC");

        chkLDC_Applicable_ACC.Enabled = false;

        if (Convert.ToString(lstDirectTaxSections_ACC.SelectedValue).Trim() == "1")
        {
            txtDirectTaxPercentage_ACC.Text = "0.00";
            txtDirectTaxPercentage_ACC.Enabled = false;
            chkLDC_Applicable_ACC.Checked = false;
            chkLDC_Applicable_ACC.Enabled = false;
        }
        else
        {
            chkLDC_Applicable_ACC.Enabled = true;
        }
        get_DirectTax_Percentage_ACC(Convert.ToString(lstDirectTaxSections_ACC.SelectedValue), txtDirectTaxPercentage_ACC);
        // Calculate_DirectTax_Amount_ACC(lstDirectTaxSections_ACC, txtDirectTaxPercentage_ACC, txtInvoiceAmtWithoutGST_ACC);
        Calculate_DirectTax_Amount_ACC();
    }

    protected void chkLDC_Applicable_ACC_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkLDC_Applicable_ACC = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chkLDC_Applicable_ACC.NamingContainer;

        TextBox txtDirectTaxPercentage_ACC = (TextBox)row.FindControl("txtDirectTaxPercentage_ACC");
        DropDownList lstDirectTaxSections_ACC = (DropDownList)row.FindControl("lstDirectTaxSections_ACC");
        //CheckBox chkLDC_Applicable_ACC = (CheckBox)row.FindControl("chkLDC_Applicable_ACC");

        if (chkLDC_Applicable_ACC.Checked)
        {
            txtDirectTaxPercentage_ACC.Enabled = true;
            txtDirectTaxPercentage_ACC.ReadOnly = false;
        }
        else
        {
            txtDirectTaxPercentage_ACC.Enabled = false;
            txtDirectTaxPercentage_ACC.ReadOnly = true;
        }

        get_DirectTax_Percentage_ACC(Convert.ToString(lstDirectTaxSections_ACC.SelectedValue), txtDirectTaxPercentage_ACC);
        Calculate_DirectTax_Amount_ACC();

    }
    protected void txtDirectTaxPercentage_ACC_TextChanged(object sender, EventArgs e)
    {
        TextBox txtDirectTaxPercentage_ACC = (TextBox)sender;
        GridViewRow row = (GridViewRow)txtDirectTaxPercentage_ACC.NamingContainer;

        DropDownList lstDirectTaxSections_ACC = (DropDownList)row.FindControl("lstDirectTaxSections_ACC");
        TextBox txtInvoiceAmtWithoutGST_ACC = (TextBox)row.FindControl("txtInvoiceAmtWithoutGST_ACC");

        if (Convert.ToString(txtDirectTaxPercentage_ACC.Text).Trim() != "")
        {
            decimal dDirectTaxperce = Convert.ToDecimal(txtDirectTaxPercentage_ACC.Text);

            if (dDirectTaxperce > 100)
            {
                lblmessage.Text = "Please enter correct Direct Tax Percentage.";
                txtDirectTaxPercentage_ACC.Text = "";
            }
        }
        // Calculate_DirectTax_Amount_ACC(lstDirectTaxSections_ACC, txtDirectTaxPercentage_ACC, txtInvoiceAmtWithoutGST_ACC);
        Calculate_DirectTax_Amount_ACC();
    }

    public void Check_Advance_Payment_POWO()
    {
        DataSet dtAdvBal = new DataSet();
        SqlParameter[] spars12 = new SqlParameter[3];
        spars12[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
        //spars12[0].Value = "Get_ADV_Pay_POWO_Invoice"; 
        spars12[0].Value = "get_Advance_Security_Amount_Status";
        spars12[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        spars12[1].Value = txtEmpCode.Text;
        spars12[2] = new SqlParameter("@POID", SqlDbType.VarChar);
        spars12[2].Value = DBNull.Value;

        dtAdvBal = spm.getDatasetList(spars12, "sp_VSCB_CreatePOWO_Users");

        if (dtAdvBal.Tables[0].Rows.Count > 0)
        {
            if (!String.IsNullOrEmpty(dtAdvBal.Tables[0].Rows[0]["AdvPayment_Amt"].ToString()))
            {

            }
            else
            {
            }
        }
    }
    protected void txtSecurityDeposit_TextChanged(object sender, EventArgs e)
    {

    }




    #region ABAP Object Submitted Plan 
    //private void getProjectLocation()
    //{
    //    try
    //    {
    //        SqlParameter[] spars = new SqlParameter[2];
    //        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
    //        spars[0].Value = "GetDropDownprojectLocation";

    //        DataSet DS = spm.getDatasetList(spars, "SP_TASK_M_DETAILS");
    //        DDLProjectLocation.DataSource = DS.Tables[0];
    //        DDLProjectLocation.DataTextField = "Location_name";
    //        DDLProjectLocation.DataValueField = "comp_code";
    //        DDLProjectLocation.DataBind();
    //        DDLProjectLocation.Items.Insert(0, new ListItem("Select Project Location", "0"));
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
     private void getProjectLocation()
    {
        try
        {
            var getCreatedBy = Convert.ToString(Session["Empcode"]).Trim();

            SqlParameter[] spars = new SqlParameter[1];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getLocationMasterForApproval";
            
            DataSet DS = spm.getDatasetList(spars, "SP_ABAPObjectTracking");

            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                DDLProjectLocation.DataSource = DS.Tables[0];
                DDLProjectLocation.DataTextField = "Location_name";
                DDLProjectLocation.DataValueField = "comp_code";
                DDLProjectLocation.DataBind();
                DDLProjectLocation.Items.Insert(0, new ListItem("Select Project Location", "0"));
            }
            
        }
        catch (Exception ex)
        {

        }
    }

    public void get_ABAP_Object_Submitted_Plan_Details_View()
    {
        DataSet dsABAPObjectPlanSubmitted = new DataSet();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "getABAPObjectTrackerPlanDetailsForApprover";

        spars[1] = new SqlParameter("@ABAPODUploadId", SqlDbType.VarChar);
        spars[1].Value = Convert.ToInt32(hdnABAPODUploadId.Value);

        spars[2] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
        spars[2].Value = (Session["Empcode"]).ToString().Trim();

        spars[2] = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
        spars[2].Value = (Session["Empcode"]).ToString().Trim();

        dsABAPObjectPlanSubmitted = spm.getDatasetList(spars, "SP_ABAPObjectTracking");
        if (dsABAPObjectPlanSubmitted != null && dsABAPObjectPlanSubmitted.Tables[0].Rows.Count > 0)
        {
            gvDetailPlan.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvDetailPlan.DataBind();

            gvRGSDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvRGSDetails.DataBind();
            gvRGSDetails.Visible = false;

            //gvRGSDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            //gvRGSDetails.DataBind();


            gvFSDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvFSDetails.DataBind();
            gvFSDetails.Visible = false;

            gvABAPDevDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvABAPDevDetails.DataBind();
            gvABAPDevDetails.Visible = false;

            gvHBTTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvHBTTestDetails.DataBind();
            gvHBTTestDetails.Visible = false;

            gvCTMTestDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvCTMTestDetails.DataBind();
            gvCTMTestDetails.Visible = false;

            gvUATSignOffDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvUATSignOffDetails.DataBind();
            gvUATSignOffDetails.Visible = false;

            gvGoLiveDetails.DataSource = dsABAPObjectPlanSubmitted.Tables[0];
            gvGoLiveDetails.DataBind();
            gvGoLiveDetails.Visible = false;


            DDLProjectLocation.SelectedValue = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["ProjectLocation"].ToString().Trim();
            txtPRM.Text = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ProjectManager"].ToString().Trim();
            hdnProjectManagerName.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ProjectManager"].ToString().Trim();
            hdnProjectManagerMail.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["ProjectManagerMail"].ToString().Trim();
            hdnDeliveryHeadMail.Value = dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["DeliveryHeadEMail"].ToString().Trim();


            hdnProgramManagerName.Value = dsABAPObjectPlanSubmitted.Tables[2].Rows[0]["ApproverName"].ToString().Trim();
            hdnProgramManagerMail.Value = dsABAPObjectPlanSubmitted.Tables[2].Rows[0]["ProgramManagerEmail"].ToString().Trim();


            hdndowloadfile.Value = dsABAPObjectPlanSubmitted.Tables[0].Rows[0]["FilePath"].ToString().Trim();
            if (Session["Empcode"].ToString().Trim() == dsABAPObjectPlanSubmitted.Tables[1].Rows[0]["Emp_Code"].ToString().Trim())
            {
                trvl_btnSave.Visible = false;
                btnCancel.Visible = false;
                divRemark.Visible = false;
            }
            else
            {
                trvl_btnSave.Visible = true;
                btnCancel.Visible = true;
                divRemark.Visible = true;
            }
            DgvApprover.DataSource = dsABAPObjectPlanSubmitted.Tables[2];
            DgvApprover.DataBind();

            hdnABAPObjectCreator_Email.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[2].Rows[0]["ABAPObjectCreator_Email"]).Trim();

            for (int irow = 0; irow < dsABAPObjectPlanSubmitted.Tables[2].Rows.Count; irow++)
            {
                if (Convert.ToString(dsABAPObjectPlanSubmitted.Tables[2].Rows[irow]["Action"]).Trim() == "")
                {
                    hdn_next_Appr_Empcode.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[2].Rows[irow]["Approval_Emp_Code"]).Trim();
                    hdn_next_Appr_EmpEmail_ID.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[2].Rows[irow]["Emp_Emailaddress"]).Trim();
                    hdn_next_Appr_Emp_Name.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[2].Rows[irow]["ApproverName"]).Trim();

                    break;
                }
                if (Convert.ToString(dsABAPObjectPlanSubmitted.Tables[2].Rows[irow]["Action"]).Trim() == "Pending")
                {
                    hdn_curnt_Appr_Empcode.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[2].Rows[irow]["Approval_Emp_Code"]).Trim();
                    hdn_curnt_Appr_EmpEmail_ID.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[2].Rows[irow]["Emp_Emailaddress"]).Trim();
                    hdn_curnt_Appr_Emp_Name.Value = Convert.ToString(dsABAPObjectPlanSubmitted.Tables[2].Rows[irow]["ApproverName"]).Trim();
                }

                if (Convert.ToString(dsABAPObjectPlanSubmitted.Tables[2].Rows[irow]["Action"]).Trim() == "Approved")
                {
                    trvl_btnSave.Visible = false;
                    btnCancel.Visible = false;
                    txtRemarks.Visible = false;

                }
            }
        }
    }

    protected void btnABAPPlanSubmit_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvRGSDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string secondColumnValue = row.Cells[1].Text.Trim();
                if (string.IsNullOrEmpty(secondColumnValue))
                {
                    lblmessage.Text = "Consultant value cannot be empty.";
                    lblmessage.Visible = true;
                }
            }

        }

        foreach (GridViewRow row in gvFSDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string secondColumnValue = row.Cells[1].Text.Trim();
                if (string.IsNullOrEmpty(secondColumnValue))
                {
                    lblmessage.Text = "Consultant value cannot be empty.";
                    lblmessage.Visible = true;
                }
            }

        }


        foreach (GridViewRow row in gvRGSDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string secondColumnValue = row.Cells[1].Text.Trim();
                if (string.IsNullOrEmpty(secondColumnValue))
                {
                    lblmessage.Text = "Error: Second column value cannot be empty.";
                    lblmessage.Visible = true;
                }
            }

        }

        foreach (GridViewRow row in gvRGSDetails.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string secondColumnValue = row.Cells[1].Text.Trim();
                if (string.IsNullOrEmpty(secondColumnValue))
                {
                    lblmessage.Text = "Error: Second column value cannot be empty.";
                    lblmessage.Visible = true;
                }
            }

        }
    }


    protected void ABAP_Object_Approve_btnSave_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        UpdateABAPObjectTrackerApprovalStaus("ApproveABAPObjectSubmittedPlan", Convert.ToInt32(hdnABAPODUploadId.Value), Convert.ToString(txtRemarks.Text).Trim(), Convert.ToString(hdn_curnt_Appr_Empcode.Value));

        #region Send Email 
        string strSubject = "";
        strSubject = DDLProjectLocation.SelectedItem.Text.ToString().Trim() + " ABAP Object Plan Approved";
        string sApproverEmail_CC = "";
        sApproverEmail_CC = hdnDeliveryHeadMail.Value.ToString().Trim();

        string strInvoiceURL = "";
        strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["ViewLink_ABAPObjectPlan"]).Trim() + "?ABAPODId=" + hdnABAPODUploadId.Value).Trim();
        StringBuilder strbuild = new StringBuilder();

        strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'>");
        strbuild.Append("<tr><td>Dear " + hdnProjectManagerName.Value.ToString().Trim() + "</td></tr>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>");
        strbuild.Append("<tr><td colspan='2'> This is to inform you that the ABAP Object development plan for the project <b>" + DDLProjectLocation.SelectedItem.Text.ToString().Trim() + "</b> has been approved by <b>" + hdnProgramManagerName.Value.ToString().Trim() + ".</b></td></tr>");
        strbuild.Append("<tr><td></td></tr>");
        strbuild.Append("</table>");
        strbuild.Append("<br/><p><a href=" + strInvoiceURL + ">Please click here to view ABAP Object Plan.</a></p>");
        strbuild.Append("<br><br>This is an auto generated email, please do not reply!");
        spm.sendMail(hdnProjectManagerMail.Value.ToString(), strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);
        #endregion

        Response.Redirect("~/procs/ABAP_Object_Tracker_PendingList.aspx");
  }

    public DataSet UpdateABAPObjectTrackerApprovalStaus(string qtype, int ABAPODUploadId, string ApprovalRemark, string ApprEmpcode)
    {
        DataSet dsData = new DataSet();

        try
        {
            List<SqlParameter> spars = new List<SqlParameter>
            {
                new SqlParameter("@qtype", SqlDbType.VarChar) { Value = qtype },
                new SqlParameter("@ABAPODUploadId", SqlDbType.VarChar) { Value = ABAPODUploadId },
                new SqlParameter("@approver_remark", SqlDbType.VarChar) { Value = ApprovalRemark },
                new SqlParameter("@app_empcode", SqlDbType.VarChar) { Value = ApprEmpcode },
            };

            dsData = spm.getDatasetList(spars.ToArray(), "SP_ABAPObjectTracking");
            return dsData;

        }
        catch (Exception e)
        {

        }
        return dsData;
    }
    
    protected void ABAP_Object_Reject_btnSave_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        if (txtRemarks.Text == "")
        {
            lblmessage.Text = "Please enter the remark.";
            return;
        }

        UpdateABAPObjectTrackerApprovalStaus("RejectABAPObjectSubmittedPlan", Convert.ToInt32(hdnABAPODUploadId.Value), Convert.ToString(txtRemarks.Text).Trim(), Convert.ToString(hdn_curnt_Appr_Empcode.Value));

        #region Send Email 
        string strSubject = "";
        strSubject = DDLProjectLocation.SelectedItem.Text.ToString().Trim() + " ABAP Object Plan has been Rejected";
        string sApproverEmail_CC = "";
        sApproverEmail_CC = hdnDeliveryHeadMail.Value.ToString().Trim();

        string strInvoiceURL = "";
        strInvoiceURL = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["ViewLink_ABAPObjectPlan"]).Trim() + "?ABAPODId=" + hdnABAPODUploadId.Value).Trim();
        StringBuilder strbuild = new StringBuilder();

        strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:80%'>");
        strbuild.Append("<tr><td>Dear " + hdnProjectManagerName.Value.ToString().Trim() + "</td></tr>");
        strbuild.Append("<tr><td style='height:20px'></td></tr>");
        strbuild.Append("<tr><td colspan='2'> This is to inform you that the ABAP Object development plan for the project <b>" + DDLProjectLocation.SelectedItem.Text.ToString().Trim() + "</b> has been Rejected by <b>" + hdnProgramManagerName.Value.ToString().Trim() + "</b>.</td></tr>");
        strbuild.Append("<tr><td></td></tr>");
        strbuild.Append("</table>");
        strbuild.Append("<br/><p><a href=" + strInvoiceURL + ">Please click here to view ABAP Object Plan.</a></p>");
        strbuild.Append("<br><br>This is an auto generated email, please do not reply!");
        spm.sendMail(hdnProjectManagerMail.Value.ToString(), strSubject, Convert.ToString(strbuild).Trim(), "", sApproverEmail_CC);
        #endregion

        Response.Redirect("~/procs/ABAP_Object_Tracker_PendingList.aspx");
    }
    
    public void DownloadFile(object sender, EventArgs e)
    {
        string filePath = Server.MapPath("~/ABAPTracker/" + hdndowloadfile.Value);

        if (System.IO.File.Exists(filePath))
        {
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + hdndowloadfile.Value);
            Response.WriteFile(filePath);
            Response.End();
        }
        else
        {
            Response.Write("File not found.");
        }

    }


    protected void btnRGS_Details_Click(object sender, EventArgs e)
    {
        gvDetailPlan.Visible = true;
        if (gvRGSDetails.Visible)
        {
            gvRGSDetails.Visible = false;
            btnRGS_Details.Text = "+";
        }
        else
        {
            btnRGS_Details.Text = "-";
            gvRGSDetails.Visible = true;
        }
    }

    protected void btnFS_Details_Click(object sender, EventArgs e)
    {
        gvDetailPlan.Visible = true;
        if (gvFSDetails.Visible)
        {
            gvFSDetails.Visible = false;
            btnFS_Details.Text = "+";
        }
        else
        {
            gvFSDetails.Visible = true;
            btnFS_Details.Text = "-";

        }
    }

    protected void btnABAPDev_Details_Click(object sender, EventArgs e)
    {
        gvDetailPlan.Visible = true;
        if (gvABAPDevDetails.Visible)
        {
            gvABAPDevDetails.Visible = false;
            btnABAPDev_Details.Text = "+";
        }
        else
        {
            gvABAPDevDetails.Visible = true;
            btnABAPDev_Details.Text = "-";

        }
    }

    protected void btnHBT_Details_Click(object sender, EventArgs e)
    {
        gvDetailPlan.Visible = true;
        if (gvHBTTestDetails.Visible)
        {
            gvHBTTestDetails.Visible = false;
            btnHBT_Details.Text = "+";
        }
        else
        {
            gvHBTTestDetails.Visible = true;
            btnHBT_Details.Text = "-";
        }
    }

    protected void btnCTM_Details_Click(object sender, EventArgs e)
    {
        gvDetailPlan.Visible = true;
        if (gvCTMTestDetails.Visible)
        {
            gvCTMTestDetails.Visible = false;
            btnCTM_Details.Text = "+";
        }
        else
        {
            gvCTMTestDetails.Visible = true;
            btnCTM_Details.Text = "-";

        }
    }

    protected void btnUATSignOff_Details_Click(object sender, EventArgs e)
    {
        gvDetailPlan.Visible = true;
        if (gvUATSignOffDetails.Visible)
        {
            gvUATSignOffDetails.Visible = false;
            btnUATSignOff_Details.Text = "+";
        }
        else
        {
            gvUATSignOffDetails.Visible = true;
            btnUATSignOff_Details.Text = "-";
        }
    }

    protected void btnGoLive_Details_Click(object sender, EventArgs e)
    {
        gvDetailPlan.Visible = true;
        if (gvGoLiveDetails.Visible)
        {
            gvGoLiveDetails.Visible = false;
            btnGoLive_Details.Text = "+";
        }
        else
        {
            gvGoLiveDetails.Visible = true;
            btnGoLive_Details.Text = "-";
        }
    }
    protected void btnAllDetails_Click(object sender, EventArgs e)
    {


        bool allVisible = lirgsdetails.Visible &&
                       lifsdetails.Visible &&
                       liabapdev.Visible &&
                       lihbttest.Visible &&
                       lictmtest.Visible &&
                       liuatsignoff.Visible &&
                       ligolive.Visible;
        if (allVisible)
        {
            btnAllDetails.Text = "CLICK TO SEE STAGE-WISE DETAILS";
            lirgsdetails.Visible = false;
            lifsdetails.Visible = false;
            liabapdev.Visible = false;
            lihbttest.Visible = false;
            lictmtest.Visible = false;
            liuatsignoff.Visible = false;
            ligolive.Visible = false;

           
        }
        else
        {
            btnAllDetails.Text = "CLICK TO SEE STAGE-WISE DETAILS";
            lirgsdetails.Visible = true;
            lifsdetails.Visible = true;
            liabapdev.Visible = true;
            lihbttest.Visible = true;
            lictmtest.Visible = true;
            liuatsignoff.Visible = true;
            ligolive.Visible = true;
        }

        return;
    }
    #endregion
}