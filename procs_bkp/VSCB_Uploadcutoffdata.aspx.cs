using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VSCB_Uploadcutoffdata : System.Web.UI.Page
{

    #region Creative_Default_methods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;

    SP_Methods spm = new SP_Methods();
    string strempcode = "";
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void lnkcont_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/SampleForm");
    }

    #endregion
    public DataTable dtPOWONo;

    OleDbConnection oConnstr;

    #region Page_Events

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }


            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");


            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/vscb_index");
            }
            else
            {
                

                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    hdnEmpCode.Value = Session["Empcode"].ToString(); 
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    {

        
    }


    #endregion

    #region Page Methods

    private string  get_Uploaded_FileName(string squeryType)
    {
        string sFileName = "";
        try
        {
            DataSet dsList = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
            spars[0].Value = squeryType;

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hdnEmpCode.Value).Trim();              

            dsList = spm.getDatasetList(spars, "SP_VSCB_Upload_CutoffData");

            
            if (dsList.Tables[0].Rows.Count > 0)
            {
                sFileName = Convert.ToString(dsList.Tables[0].Rows[0]["posted_FileName"]).Trim();
            }


        }
        catch (Exception ex)
        {

        }
        return sFileName;
    }
    #endregion 


    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
       
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        

    }

    protected void lstCostCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
     
    }



    protected void btnback_mng_Click(object sender, EventArgs e)
    {
      
    }

    protected void trvl_btnSave_Click(object sender, EventArgs e)
    {
        try
        {  
             
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnUploadPOWO_Click(object sender, EventArgs e)
    {
        Boolean blnIsUpload = false;

        DeleteUploadedData();


        if (!uploadPOWOCutoffdata.HasFile)
        {
            lblmessage.Text = "Please upload POWO file.";
            return;
        }



        #region Read File and update Dataset
        string sfileName = get_Uploaded_FileName("get_filename_uploadPOWO_asCutoffdata");

        string filename = uploadPOWOCutoffdata.FileName;
        string POWOFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBUploadCutofdatafiles"]).Trim() + "/");
        bool folderExists = Directory.Exists(POWOFilePath);
        if (!folderExists)
        {
            Directory.CreateDirectory(POWOFilePath);
        }
        String InputFile = System.IO.Path.GetExtension(uploadPOWOCutoffdata.FileName);

        filename = sfileName + "_POWO" + InputFile;
        uploadPOWOCutoffdata.SaveAs(Path.Combine(POWOFilePath, filename));


        string powoUplaodedFile = POWOFilePath + filename;

        //string read = System.IO.Path.GetFullPath(Server.MapPath("~/empdetail.xlsx"));
        string read = System.IO.Path.GetFullPath(powoUplaodedFile);

        if (Path.GetExtension(read) == ".xls")
        {
            oConnstr = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + read + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");
        }
        else if (Path.GetExtension(read) == ".xlsx")
        { 
            oConnstr = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + read + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
        }

        
        OleDbCommand excommand = new OleDbCommand();
        OleDbDataAdapter oadp = new OleDbDataAdapter();
        DataSet dsPOWO = new DataSet();
        excommand.Connection = oConnstr;
        excommand.CommandType = CommandType.Text;
        //Table 0
        excommand.CommandText = "SELECT* FROM [POWO$] where PONumber is not null" ;
        oadp = new OleDbDataAdapter(excommand);
        oadp.Fill(dsPOWO, "powo");

        //Table 1
        excommand.CommandType = CommandType.Text;
        excommand.CommandText = "SELECT* FROM [Milestone$] where PONumber is not null";
        oadp = new OleDbDataAdapter(excommand);
        oadp.Fill(dsPOWO, "Milestone");

        //Table 2
        excommand.CommandType = CommandType.Text;
        excommand.CommandText = "SELECT* FROM [POWO Approver$] where  PONumber is not null";
        oadp = new OleDbDataAdapter(excommand);
        oadp.Fill(dsPOWO, "POWOApprover");

        //Table 3
        excommand.CommandType = CommandType.Text;
        excommand.CommandText = "SELECT* FROM [Invoice$] where  PONumber is not null";
        oadp = new OleDbDataAdapter(excommand);
        oadp.Fill(dsPOWO, "Invoice");

        //Table 4
        excommand.CommandType = CommandType.Text;
        excommand.CommandText = "SELECT* FROM [Invoice_Approvers$] where  PONumber is not null";
        oadp = new OleDbDataAdapter(excommand);
        oadp.Fill(dsPOWO, "InvoiceApprover");

        //Table 5
        excommand.CommandType = CommandType.Text;
        excommand.CommandText = "SELECT* FROM [Payment$] where  InvoiceNo is not null ";
        oadp = new OleDbDataAdapter(excommand);
        oadp.Fill(dsPOWO, "PaymentReq");

        #endregion




        bool isValid = true;
        StringBuilder sberrorMsg = new StringBuilder();
        sberrorMsg.Append("Please correct the below issue and try to re-upload<br/>");
        string sponumber = "";
        if (dsPOWO != null)
        {
            if (dsPOWO.Tables.Count > 0)
            {
                if (dsPOWO.Tables[0].Rows.Count > 0)
                {
                    sponumber = Convert.ToString(dsPOWO.Tables[0].Rows[0]["PONumber"]).Trim();

                    #region check PO blank cells 

                    if (dsPOWO.Tables[0].Rows.Count > 1)
                    {
                        sberrorMsg.Append("Only One PO/ WO should be upload. <br/>");
                        isValid = false;
                    }

                    if (Convert.ToString(dsPOWO.Tables[0].Rows[0]["PONumber"]).Trim() == "")
                    {
                        sberrorMsg.Append("Please enter PO Number.<br/>");
                        isValid = false;
                    }
                    if (Convert.ToString(dsPOWO.Tables[0].Rows[0]["PODate"]).Trim() == "")
                    {
                        sberrorMsg.Append("Please enter PO Date.<br/>");
                        isValid = false;
                    }

                    if (Convert.ToString(dsPOWO.Tables[0].Rows[0]["POAmount_WithoutTax"]).Trim() == "")
                    {
                        sberrorMsg.Append("Please enter PO Amount Without Tax.<br/>");
                        isValid = false;
                    }
                    if (Convert.ToString(dsPOWO.Tables[0].Rows[0]["POAmount_WithTax"]).Trim() == "")
                    {
                        sberrorMsg.Append("Please enter PO Amount With Tax.<br/>");
                        isValid = false;
                    }
                    if (Convert.ToString(dsPOWO.Tables[0].Rows[0]["PO_PaymentStaus"]).Trim() == "")
                    {
                        sberrorMsg.Append("Please enter PO Payment Status.<br/>");
                        isValid = false;
                    }

                    if (Convert.ToString(dsPOWO.Tables[0].Rows[0]["POBalnce_Amount"]).Trim() == "")
                    {
                        sberrorMsg.Append("Please enter PO Balance Amount.<br/>");
                        isValid = false;
                    }

                    if (Convert.ToString(dsPOWO.Tables[0].Rows[0]["PO_Terms"]).Trim() == "")
                    {
                        sberrorMsg.Append("Please enter Sign PO copy Name.<br/>");
                        isValid = false;
                    }

                    #endregion

                    #region validate  master Table  --PO                  

                    DataSet dsPoList = new DataSet();
                    SqlParameter[] spars = new SqlParameter[7];

                    spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
                    spars[0].Value = "Validate_uploadedPOWOdata_asCutoffdata";

                    spars[1] = new SqlParameter("@CostCentre", SqlDbType.VarChar);
                    spars[1].Value = Convert.ToString(dsPOWO.Tables[0].Rows[0]["Costcenter"]).Trim();

                    spars[2] = new SqlParameter("@Department", SqlDbType.VarChar);
                    spars[2].Value = Convert.ToString(dsPOWO.Tables[0].Rows[0]["Department_Name"]).Trim();

                    spars[3] = new SqlParameter("@POWOType", SqlDbType.VarChar);
                    spars[3].Value = Convert.ToString(dsPOWO.Tables[0].Rows[0]["POType"]).Trim();

                    spars[4] = new SqlParameter("@VendorName", SqlDbType.VarChar);
                    spars[4].Value = Convert.ToString(dsPOWO.Tables[0].Rows[0]["Vendor_Name"]).Trim();

                    spars[5] = new SqlParameter("@PONumber", SqlDbType.VarChar);
                    spars[5].Value = Convert.ToString(dsPOWO.Tables[0].Rows[0]["PONumber"]).Trim();

                    spars[6] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                    spars[6].Value = Convert.ToString(dsPOWO.Tables[0].Rows[0]["PO_Maker (employee Code)"]).Trim();

                    dsPoList = spm.getDatasetList(spars, "SP_VSCB_Upload_CutoffData");


                    if (dsPoList != null)
                    {
                        if (dsPoList.Tables[0].Rows.Count == 0)
                        {
                            sberrorMsg.Append("cost center not exists in OneHr.<br/>");
                            isValid = false;
                        }
                        if (dsPoList.Tables[1].Rows.Count == 0)
                        {
                            sberrorMsg.Append("Department not exists in OneHr.<br/>");
                            isValid = false;
                        }
                        if (dsPoList.Tables[2].Rows.Count == 0)
                        {
                            sberrorMsg.Append("PO Type not exists in OneHr.<br/>");
                            isValid = false;
                        }
                        if (dsPoList.Tables[3].Rows.Count == 0)
                        {
                            sberrorMsg.Append("Vendor Name not exists in OneHr.<br/>");
                            isValid = false;
                        }

                        if (dsPoList.Tables[4].Rows.Count > 0)
                        {
                            sberrorMsg.Append("PO Number already exists in OneHr.<br/>");
                            isValid = false;
                        }
                        if (dsPoList.Tables[5].Rows.Count == 0)
                        {
                            sberrorMsg.Append("Employee Code not exists in OneHr.<br/>");
                            isValid = false;
                        }

                        if (dsPoList.Tables[6].Rows.Count == 0)
                        {
                            sberrorMsg.Append("Tally Code not exists in OneHr.<br/>");
                            isValid = false;
                        }
                    }

                    Double dPOBalAmt = 0;
                    Double dAllMilestonebal = 0;
                    if (Convert.ToString(dsPOWO.Tables[0].Rows[0]["POBalnce_Amount"]).Trim() != "")
                    {
                        dPOBalAmt = Convert.ToDouble(dsPOWO.Tables[0].Rows[0]["POBalnce_Amount"]);
                    }

                    #endregion                    

                    #region Validate Master Table --Milestone
                    if (dsPOWO.Tables[1].Rows.Count > 0)
                    {

                        for (int i = 0; i < dsPOWO.Tables[1].Rows.Count; i++)
                        {
                            #region check Milestone blank Cells

                            if (Convert.ToString(dsPOWO.Tables[1].Rows[i]["PONumber"]).Trim() == "")
                            {
                                sberrorMsg.Append("Please enter PO Number.<br/>");
                                isValid = false;
                            }
                            if (Convert.ToString(dsPOWO.Tables[1].Rows[i]["Milestone Sr No"]).Trim() == "")
                            {
                                sberrorMsg.Append("Please enter Milestone Sr No.<br/>");
                                isValid = false;
                            }

                            //if (Convert.ToString(dsPOWO.Tables[1].Rows[i]["MilestoneDescription"]).Trim() == "")
                            //{
                            //    sberrorMsg.Append("Please enter Milestone Description.<br/>");
                            //    isValid = false;
                            //}

                            if (Convert.ToString(dsPOWO.Tables[1].Rows[i]["MilestoneName"]).Trim() == "")
                            {
                                sberrorMsg.Append("Please enter Milestone Name.<br/>");
                                isValid = false;
                            }

                            if (Convert.ToString(dsPOWO.Tables[1].Rows[i]["Milestone_due_date"]).Trim() == "")
                            {
                                sberrorMsg.Append("Please enter Milestone Due Date.<br/>");
                                isValid = false;
                            }
                            if (Convert.ToString(dsPOWO.Tables[1].Rows[i]["Quantity"]).Trim() == "")
                            {
                                sberrorMsg.Append("Please enter Quantity.<br/>");
                                isValid = false;
                            }
                            if (Convert.ToString(dsPOWO.Tables[1].Rows[i]["Rate"]).Trim() == "")
                            {
                                sberrorMsg.Append("Please enter Rate.<br/>");
                                isValid = false;
                            }
                            if (Convert.ToString(dsPOWO.Tables[1].Rows[i]["Amount"]).Trim() == "")
                            {
                                sberrorMsg.Append("Please enter Amount.<br/>");
                                isValid = false;
                            }
                            if (Convert.ToString(dsPOWO.Tables[1].Rows[i]["Milestome Amt With Tax"]).Trim() == "")
                            {
                                sberrorMsg.Append("Please enter Milestone Amt With Tax.<br/>");
                                isValid = false;
                            }
                            if (Convert.ToString(dsPOWO.Tables[1].Rows[i]["PaymentStatus"]).Trim() == "")
                            {
                                sberrorMsg.Append("Please enter Payment Status.<br/>");
                                isValid = false;
                            }

                            #endregion

                            #region validate  Milestone master Table
                            if (Convert.ToString(dsPOWO.Tables[1].Rows[i]["PONumber"]).Trim() != "")
                            {

                                DataSet dsMList = new DataSet();
                                SqlParameter[] sparsM = new SqlParameter[2];

                                sparsM[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
                                sparsM[0].Value = "Validate_uploadedMilestonedata_asCutoffdata";

                                sparsM[1] = new SqlParameter("@Item_detail", SqlDbType.VarChar);
                                sparsM[1].Value = Convert.ToString(Convert.ToString(dsPOWO.Tables[1].Rows[i]["MilestoneDescription"]).Trim()).Trim();

                                dsMList = spm.getDatasetList(sparsM, "SP_VSCB_Upload_CutoffData");


                                if (dsMList != null)
                                {
                                    if (dsMList.Tables[0].Rows.Count == 0)
                                    {
                                        sberrorMsg.Append("Milestone Description not exists in OneHr.<br/>");
                                        isValid = false;
                                    }

                                }


                            }
                            #endregion

                            if (Convert.ToString(dsPOWO.Tables[1].Rows[i]["Milesstone_Balance_Amt"]).Trim() != "")
                            {
                                dAllMilestonebal += Convert.ToDouble(dsPOWO.Tables[1].Rows[i]["Milesstone_Balance_Amt"]);
                            }

                            if (Convert.ToString(dsPOWO.Tables[0].Rows[0]["PONumber"]).Trim() != Convert.ToString(dsPOWO.Tables[1].Rows[i]["PONumber"]).Trim())
                            {
                                sberrorMsg.Append("PONumber should be Match with POWO sheet and Milestone sheet <br/>");
                                isValid = false;
                                break;
                            }
                        }
                    }

                    //check PO Balnce Amt & Milestone Bal Amt
                    if (dPOBalAmt != dAllMilestonebal)
                    {
                        sberrorMsg.Append("PO Balance Amount should be match with Milestone Balance Amount. <br/>");
                        isValid = false;
                    }

                    #endregion

                }
            }
        }

        if (isValid == false)
        {
            lblmessage.Text = Convert.ToString(sberrorMsg).Trim();
            blnIsUpload = false;
            return;
        }

        sberrorMsg.Clear();
        sberrorMsg.Append("Please correct the below issue and try to re-upload<br/>");
        #region Insert PO and Milestone Cutoff data
        if (isValid == true)
        {
            if (dsPOWO != null)
            {
                if (dsPOWO.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        DataSet dsPo = new DataSet();
                        SqlParameter[] sparPO = new SqlParameter[14];

                        string[] strdate;
                        string strPOWODate = "";

                        #region date formatting
                        if (Convert.ToString(dsPOWO.Tables[0].Rows[0]["PODate"]).Trim() != "")
                        {
                            strdate = Convert.ToString(dsPOWO.Tables[0].Rows[0]["PODate"]).Trim().Split('-');
                            strPOWODate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                        }
                        #endregion

                        #region Upload PO/WO cutoff data
                        sparPO[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
                        sparPO[0].Value = "Upload_POWO_Cutoffdata";

                        sparPO[1] = new SqlParameter("@CostCentre", SqlDbType.VarChar);
                        sparPO[1].Value = Convert.ToString(dsPOWO.Tables[0].Rows[0]["Costcenter"]).Trim();

                        sparPO[2] = new SqlParameter("@Department", SqlDbType.VarChar);
                        sparPO[2].Value = Convert.ToString(dsPOWO.Tables[0].Rows[0]["Department_Name"]).Trim();

                        sparPO[3] = new SqlParameter("@PONumber", SqlDbType.VarChar);
                        sparPO[3].Value = Convert.ToString(dsPOWO.Tables[0].Rows[0]["PONumber"]).Trim();

                        sparPO[4] = new SqlParameter("@PODate", SqlDbType.VarChar);
                        sparPO[4].Value = Convert.ToString(strPOWODate).Trim();

                        sparPO[5] = new SqlParameter("@POTitle", SqlDbType.VarChar);
                        sparPO[5].Value = Convert.ToString(dsPOWO.Tables[0].Rows[0]["POType"]).Trim();

                        sparPO[6] = new SqlParameter("@VendorName", SqlDbType.VarChar);
                        sparPO[6].Value = Convert.ToString(dsPOWO.Tables[0].Rows[0]["Vendor_Name"]).Trim();

                        sparPO[7] = new SqlParameter("@POWO_T_BaseAmt", SqlDbType.VarChar);
                        sparPO[7].Value = Convert.ToString(dsPOWO.Tables[0].Rows[0]["POAmount_WithoutTax"]).Trim();

                        sparPO[8] = new SqlParameter("@POWOAmt", SqlDbType.VarChar);
                        sparPO[8].Value = Convert.ToString(dsPOWO.Tables[0].Rows[0]["POAmount_WithTax"]).Trim();

                        sparPO[9] = new SqlParameter("@PO_Paid_Amt", SqlDbType.VarChar);
                        if (Convert.ToString(dsPOWO.Tables[0].Rows[0]["POPaid_Amoint"]).Trim() != "")
                            sparPO[9].Value = Convert.ToString(dsPOWO.Tables[0].Rows[0]["POPaid_Amoint"]).Trim();
                        else
                            sparPO[9].Value = "0";

                        sparPO[10] = new SqlParameter("@PO_Bal_Amt", SqlDbType.VarChar);
                        sparPO[10].Value = Convert.ToString(dsPOWO.Tables[0].Rows[0]["POBalnce_Amount"]).Trim();

                        sparPO[11] = new SqlParameter("@DirectTaxCollection_Amt", SqlDbType.VarChar);
                        if (Convert.ToString(dsPOWO.Tables[0].Rows[0]["PO_DirectTaxCollection_Amt"]).Trim() != "")
                            sparPO[11].Value = Convert.ToString(dsPOWO.Tables[0].Rows[0]["PO_DirectTaxCollection_Amt"]).Trim();
                        else
                            sparPO[11].Value = "0";

                        sparPO[12] = new SqlParameter("@PaymentStatusID", SqlDbType.VarChar);
                        if (Convert.ToString(dsPOWO.Tables[0].Rows[0]["PO_PaymentStaus"]).Trim() == "Paid")
                            sparPO[12].Value = "2";
                        else if (Convert.ToString(dsPOWO.Tables[0].Rows[0]["PO_PaymentStaus"]).Trim() == "Pending")
                            sparPO[12].Value = "1";
                        else if (Convert.ToString(dsPOWO.Tables[0].Rows[0]["PO_PaymentStaus"]).Trim() == "Partial")
                            sparPO[12].Value = "3";
                        else
                            sparPO[12].Value = DBNull.Value;

                        sparPO[13] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                        sparPO[13].Value = Convert.ToString(dsPOWO.Tables[0].Rows[0]["PO_Maker (employee Code)"]).Trim();

                        dsPo = spm.getDatasetList(sparPO, "SP_VSCB_Upload_CutoffData");
                        #endregion

                        Double dMaxPOID = 0;
                        string sProject_Dept_Name = "";
                        Int32 iPOTypeId = 0;
                        if (dsPo != null)
                        {
                            if (dsPo.Tables[0].Rows.Count > 0)
                            {
                                dMaxPOID = Convert.ToDouble(dsPo.Tables[0].Rows[0]["POID"]);
                                sProject_Dept_Name = Convert.ToString(dsPo.Tables[0].Rows[0]["Project_Dept_Name"]).Trim();
                                iPOTypeId = Convert.ToInt32(dsPo.Tables[0].Rows[0]["PotypeId"]);

                                #region Upload Milestone
                                blnIsUpload = Upload_Milestones(dMaxPOID, dsPOWO.Tables[1], Convert.ToString(dsPOWO.Tables[0].Rows[0]["PONumber"]).Trim());
                                if (blnIsUpload == false)
                                {
                                    DeleteUploadedData();
                                    sberrorMsg.Append("Something went wrong in Milestone sheet please check and try again.");
                                    lblmessage.Text = Convert.ToString(sberrorMsg).Trim();
                                    return;
                                }
                                #endregion

                                #region Upload PO/WO Approver
                                blnIsUpload = Upload_POWOApprovers(dMaxPOID, dsPOWO.Tables[2], Convert.ToString(dsPOWO.Tables[0].Rows[0]["PONumber"]).Trim());
                                if (blnIsUpload == false)
                                {
                                    DeleteUploadedData();
                                    sberrorMsg.Append("Something went wrong in POWO Approver sheet please check and try again.");
                                    lblmessage.Text = Convert.ToString(sberrorMsg).Trim();
                                    return;
                                }
                                #endregion


                            }
                            else
                            {
                                blnIsUpload = false;
                                sberrorMsg.Append("Something went wrong please try again.");
                                lblmessage.Text = Convert.ToString(sberrorMsg).Trim();

                            }
                        }
                        else
                        {
                            blnIsUpload = false;
                            sberrorMsg.Append("Something went wrong please try again.");
                            lblmessage.Text = Convert.ToString(sberrorMsg).Trim();

                        }
                    }
                    catch (Exception ex)
                    {
                        blnIsUpload = false;
                        sberrorMsg.Append("Something went wrong please try again.");
                        lblmessage.Text = Convert.ToString(sberrorMsg).Trim();


                    }
                }

            }

        }
        #endregion


        sberrorMsg.Clear();
        sberrorMsg.Append("Please correct the below issue and try to re-upload<br/>");

        //Delete PO and Milestone if POWO Milestone insert fail
        if (isValid == false)
        {
            DeleteUploadedData();
            return;
        }



        #region Validate Invoice
        if (dsPOWO.Tables[3].Rows.Count > 0)
        {
            sberrorMsg.Clear();
            sberrorMsg.Append("Please correct the below issue and try to re-upload<br/>");

            if (dsPOWO.Tables[3].Rows.Count > 0)
            {
                for (int irow = 0; irow < dsPOWO.Tables[3].Rows.Count; irow++)
                {
                    #region validate Blank cells
                    if (sponumber == Convert.ToString(dsPOWO.Tables[3].Rows[irow]["PONumber"]).Trim())
                    {
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["Milestone Sr No"]).Trim() == "")
                        {
                            sberrorMsg.Append("Please enter Milestone Sr.No.<br/>");
                            isValid = false;
                        }
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["MilestoneName"]).Trim() == "")
                        {
                            sberrorMsg.Append("Please enter Milestone Name.<br/>");
                            isValid = false;
                        }
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["VoucherNo"]).Trim() == "")
                        {
                            sberrorMsg.Append("Please enter Voucher No.<br/>");
                            isValid = false;
                        }
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["InvoiceNo"]).Trim() == "")
                        {
                            sberrorMsg.Append("Please enter Invoice No.<br/>");
                            isValid = false;
                        }
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["Supplier_Invoice_Date"]).Trim() == "")
                        {
                            sberrorMsg.Append("Please enter Supplier Invoice Date.<br/>");
                            isValid = false;
                        }
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["AmtWithoutTax"]).Trim() == "")
                        {
                            sberrorMsg.Append("Please enter Amount Without Tax.<br/>");
                            isValid = false;
                        }
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["AmtWithTax"]).Trim() == "")
                        {
                            sberrorMsg.Append("Please enter Amount With Tax.<br/>");
                            isValid = false;
                        }
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["TDS/TCS"]).Trim() == "")
                        {
                            sberrorMsg.Append("Please enter TDS/TCS.<br/>");
                            isValid = false;
                        }
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["TDS/TCS"]).Trim() == "")
                        {
                            sberrorMsg.Append("Please enter Direct Tax Type.<br/>");
                            isValid = false;
                        }
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["Payable Amount with Tax"]).Trim() == "")
                        {
                            sberrorMsg.Append("Please enter Payable Amount with Tax.<br/>");
                            isValid = false;
                        }
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["IsLDC_Applicable"]).Trim() == "")
                        {
                            sberrorMsg.Append("Please enter Is LDC Applicable.<br/>");
                            isValid = false;
                        }
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["PaymentStatus"]).Trim() == "")
                        {
                            sberrorMsg.Append("Please enter Payment Status.<br/>");
                            isValid = false;
                        }
                    }
                    #endregion

                    if (isValid == false)
                    {
                        lblmessage.Text = Convert.ToString(sberrorMsg).Trim();
                        break;
                    }
                }

                //Check Milestone Sr no in Invoice exist in Milestone
                for (int irow = 0; irow < dsPOWO.Tables[3].Rows.Count; irow++)
                {
                    if (sponumber == Convert.ToString(dsPOWO.Tables[3].Rows[irow]["PONumber"]).Trim())
                    {
                        SqlParameter[] sparI = new SqlParameter[5];

                        sparI[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
                        sparI[0].Value = "validate_invoice_cutoffdata";

                        sparI[1] = new SqlParameter("@PONumber", SqlDbType.VarChar);
                        sparI[1].Value = Convert.ToString(sponumber).Trim();

                        sparI[2] = new SqlParameter("@TSrno", SqlDbType.VarChar);
                        sparI[2].Value = Convert.ToString(dsPOWO.Tables[3].Rows[irow]["Milestone Sr No"]).Trim();

                        sparI[3] = new SqlParameter("@VendorName", SqlDbType.VarChar);
                        sparI[3].Value = Convert.ToString(dsPOWO.Tables[3].Rows[irow]["Direct Tax Type"]).Trim();

                        sparI[4] = new SqlParameter("@InvoiceNo", SqlDbType.VarChar);
                        sparI[4].Value = Convert.ToString(dsPOWO.Tables[3].Rows[irow]["InvoiceNo"]).Trim();

                        DataSet dsinvocie = spm.getDatasetList(sparI, "SP_VSCB_Upload_CutoffData");

                        if (dsinvocie != null)
                        {
                            if (dsinvocie.Tables[0].Rows.Count == 0)
                            {
                                sberrorMsg.Append("Please Milestone Sr.No in Invoice sheet should be match with Milestone sheet.<br/>");
                                isValid = false;
                            }
                            if (dsinvocie.Tables[1].Rows.Count == 0)
                            {
                                sberrorMsg.Append("Direct Tax Type not found in OneHr.<br/>");
                                isValid = false;
                            }
                            if (dsinvocie.Tables[2].Rows.Count >= 1)
                            {
                                sberrorMsg.Append("Invoice No already in use.<br/>");
                                isValid = false;
                            }
                        }

                        if (isValid == false)
                        {
                            lblmessage.Text = Convert.ToString(sberrorMsg).Trim();
                            break;
                        }

                    }
                }

                #region Check Invoice Approver
                /* if (dsPOWO.Tables[4].Rows.Count > 0)
                 {
                     int iapprcnt = 0;
                     for (int irow = 0; irow < dsPOWO.Tables[3].Rows.Count; irow++)
                     {
                         iapprcnt = 0;
                         for (int jrow = 0; jrow < dsPOWO.Tables[4].Rows.Count; jrow++)
                         {
                             if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["InvoiceNo"]).Trim() == Convert.ToString(dsPOWO.Tables[4].Rows[jrow]["InvoiceNo"]).Trim())
                             {
                                 if (Convert.ToString(dsPOWO.Tables[4].Rows[jrow]["Approver EmpCode"]).Trim() == "")
                                 {
                                     sberrorMsg.Append("Approver not enter in Invoice Approver sheet for this invoice :-" + Convert.ToString(dsPOWO.Tables[4].Rows[jrow]["InvoiceNo"]).Trim());
                                     lblmessage.Text = Convert.ToString(sberrorMsg);
                                     isValid = false;
                                     break;
                                 }


                                 if (Convert.ToString(dsPOWO.Tables[4].Rows[jrow]["Approver EmpCode"]).Trim() != "")
                                 {
                                     iapprcnt += 1;
                                 }

                             }
                         }

                         if (iapprcnt == 0)
                         {
                             sberrorMsg.Append("Approver not enter in Invoice Approver sheet for this invoice :-" + Convert.ToString(dsPOWO.Tables[3].Rows[irow]["InvoiceNo"]).Trim());
                             lblmessage.Text = Convert.ToString(sberrorMsg);
                             isValid = false;
                             break;
                         }
                     }
                 }
                 else
                 {
                     isValid = false;
                 }
                 */
                #endregion
            }
        }
        #endregion

        sberrorMsg.Clear();
        sberrorMsg.Append("Please correct the below issue and try to re-upload<br/>");

        //Delete PO and Milestone if Invoice validate fail
        if (isValid == false)
        {
            DeleteUploadedData();
            return;
        }


        #region Insert Invoice Cutoff data
        if (isValid == true)
        {
            if (dsPOWO.Tables[3].Rows.Count > 0)
            {
                sberrorMsg.Clear();
                sberrorMsg.Append("Please correct the below issue and try to re-upload<br/>");

                //string sponumber = Convert.ToString(dsPOWO.Tables[0].Rows[0]["PONumber"]).Trim();
                for (int irow = 0; irow < dsPOWO.Tables[3].Rows.Count; irow++)
                {
                    if (sponumber == Convert.ToString(dsPOWO.Tables[3].Rows[irow]["PONumber"]).Trim())
                    {
                        string[] strdate;
                        string sSupplierInvoiceDate = "";
                        var sInvoiceNo = "";
                        sInvoiceNo = Convert.ToString(dsPOWO.Tables[3].Rows[irow]["InvoiceNo"]).Trim();

                        #region date formatting
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["Supplier_Invoice_Date"]).Trim() != "")
                        {
                            strdate = Convert.ToString(dsPOWO.Tables[3].Rows[irow]["Supplier_Invoice_Date"]).Trim().Split('-');
                            sSupplierInvoiceDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                        }
                        #endregion

                        SqlParameter[] sparI = new SqlParameter[23];
                        sparI[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
                        sparI[0].Value = "Upload_Invoice_Cutoffdata";

                        sparI[1] = new SqlParameter("@PONumber", SqlDbType.VarChar);
                        sparI[1].Value = Convert.ToString(sponumber).Trim();

                        sparI[2] = new SqlParameter("@TSrno", SqlDbType.VarChar);
                        sparI[2].Value = Convert.ToString(dsPOWO.Tables[3].Rows[irow]["Milestone Sr No"]).Trim();

                        sparI[3] = new SqlParameter("@VoucherNo", SqlDbType.VarChar);
                        sparI[3].Value = Convert.ToString(dsPOWO.Tables[3].Rows[irow]["VoucherNo"]).Trim();

                        sparI[4] = new SqlParameter("@InvoiceDate", SqlDbType.VarChar);
                        sparI[4].Value = Convert.ToString(sSupplierInvoiceDate).Trim();

                        sparI[5] = new SqlParameter("@InvoiceNo", SqlDbType.VarChar);
                        sparI[5].Value = sInvoiceNo;

                        sparI[6] = new SqlParameter("@POWOAmt", SqlDbType.VarChar);
                        sparI[6].Value = Convert.ToString(dsPOWO.Tables[3].Rows[irow]["AmtWithoutTax"]).Trim();

                        sparI[7] = new SqlParameter("@CGST_Per", SqlDbType.VarChar);
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["CGST_Per"]).Trim() != "")
                            sparI[7].Value = Convert.ToString(dsPOWO.Tables[3].Rows[irow]["CGST_Per"]).Trim();
                        else
                            sparI[7].Value = DBNull.Value;

                        sparI[8] = new SqlParameter("@SGST_Per", SqlDbType.VarChar);
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["SGST_Per"]).Trim() != "")
                            sparI[8].Value = Convert.ToString(dsPOWO.Tables[3].Rows[irow]["SGST_Per"]).Trim();
                        else
                            sparI[8].Value = DBNull.Value;

                        sparI[9] = new SqlParameter("@IGST_Per", SqlDbType.VarChar);
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["IGST_Per"]).Trim() != "")
                            sparI[9].Value = Convert.ToString(dsPOWO.Tables[3].Rows[irow]["IGST_Per"]).Trim();
                        else
                            sparI[9].Value = DBNull.Value;

                        sparI[10] = new SqlParameter("@CGST_Amt", SqlDbType.VarChar);
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["CGST_Amt"]).Trim() != "")
                            sparI[10].Value = Convert.ToString(dsPOWO.Tables[3].Rows[irow]["CGST_Amt"]).Trim();
                        else
                            sparI[10].Value = DBNull.Value;

                        sparI[11] = new SqlParameter("@SGST_Amt", SqlDbType.VarChar);
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["SGST_Amt"]).Trim() != "")
                            sparI[11].Value = Convert.ToString(dsPOWO.Tables[3].Rows[irow]["SGST_Amt"]).Trim();
                        else
                            sparI[11].Value = DBNull.Value;

                        sparI[12] = new SqlParameter("@IGST_Amt", SqlDbType.VarChar);
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["IGST_Amt"]).Trim() != "")
                            sparI[12].Value = Convert.ToString(dsPOWO.Tables[3].Rows[irow]["IGST_Amt"]).Trim();
                        else
                            sparI[12].Value = DBNull.Value;

                        sparI[13] = new SqlParameter("@AmtWithTax", SqlDbType.VarChar);
                        sparI[13].Value = Convert.ToString(dsPOWO.Tables[3].Rows[irow]["AmtWithTax"]).Trim();

                        sparI[14] = new SqlParameter("@DirectTax_TypeID", SqlDbType.VarChar);
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["TDS/TCS"]).Trim() == "NA")
                            sparI[14].Value = "1";
                        else if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["TDS/TCS"]).Trim() == "TDS")
                            sparI[14].Value = "2";
                        else if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["TDS/TCS"]).Trim() == "TCS")
                            sparI[14].Value = "3";
                        else
                            sparI[14].Value = DBNull.Value;

                        sparI[15] = new SqlParameter("@VendorName", SqlDbType.VarChar);
                        sparI[15].Value = Convert.ToString(dsPOWO.Tables[3].Rows[irow]["Direct Tax Type"]).Trim();

                        sparI[16] = new SqlParameter("@DirectTax_Percentage", SqlDbType.VarChar);
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["Direct Tax Percentage"]).Trim() != "")
                            sparI[16].Value = Convert.ToString(dsPOWO.Tables[3].Rows[irow]["Direct Tax Percentage"]).Trim();
                        else
                            sparI[16].Value = DBNull.Value;

                        sparI[17] = new SqlParameter("@DirectTax_Amount", SqlDbType.VarChar);
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["Direct Tax Amount"]).Trim() != "")
                            sparI[17].Value = Convert.ToString(dsPOWO.Tables[3].Rows[irow]["Direct Tax Amount"]).Trim();
                        else
                            sparI[17].Value = DBNull.Value;

                        sparI[18] = new SqlParameter("@Payable_Amt_With_Tax", SqlDbType.VarChar);
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["Payable Amount with Tax"]).Trim() != "")
                            sparI[18].Value = Convert.ToString(dsPOWO.Tables[3].Rows[irow]["Payable Amount with Tax"]).Trim();
                        else
                            sparI[18].Value = DBNull.Value;

                        sparI[19] = new SqlParameter("@AccountPaidAmt", SqlDbType.VarChar);
                        sparI[19].Value = Convert.ToString(dsPOWO.Tables[3].Rows[irow]["Account Paid Amount"]).Trim();

                        sparI[20] = new SqlParameter("@IsLDC_Applicable", SqlDbType.VarChar);
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["IsLDC_Applicable"]).Trim() == "Yes")
                            sparI[20].Value = "1";
                        else
                            sparI[20].Value = "0";

                        sparI[21] = new SqlParameter("@InvoiceBalAmt", SqlDbType.VarChar);
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["Invoice Balance Amount"]).Trim() != "")
                            sparI[21].Value = Convert.ToString(dsPOWO.Tables[3].Rows[irow]["Invoice Balance Amount"]).Trim();
                        else
                            sparI[21].Value = 0;

                        sparI[22] = new SqlParameter("@PaymentStatusID", SqlDbType.VarChar);
                        if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["PaymentStatus"]).Trim() == "Pending")
                            sparI[22].Value = "1";
                        else if (Convert.ToString(dsPOWO.Tables[3].Rows[irow]["PaymentStatus"]).Trim() == "Partial")
                            sparI[22].Value = "3";
                        else
                            sparI[22].Value = "2";


                        DataSet dsinvocie = spm.getDatasetList(sparI, "SP_VSCB_Upload_CutoffData");

                        if (dsinvocie != null)
                        {
                            if (dsinvocie.Tables[0].Rows.Count > 0)
                            {
                                if (Convert.ToString(dsinvocie.Tables[0].Rows[0]["msg"]).Trim() == "insert")
                                {
                                    var iInvoiceId = Convert.ToString(dsinvocie.Tables[0].Rows[0]["InvoiceId"]).Trim();
                                    #region upload Invoice Approver
                                    /*
                                    for (int iarow = 0; iarow < dsPOWO.Tables[4].Rows.Count; iarow++)
                                    {
                                        if (Convert.ToString(dsPOWO.Tables[4].Rows[iarow]["PONumber"]).Trim() == sponumber && Convert.ToString(dsPOWO.Tables[4].Rows[iarow]["InvoiceNo"]).Trim() == sInvoiceNo)
                                        {
                                            SqlParameter[] sparA = new SqlParameter[4];
                                            sparA[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
                                            sparA[0].Value = "Upload_InvoiceApprover_Cutoffdata";

                                            sparA[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                                            sparA[1].Value = Convert.ToString(dsPOWO.Tables[4].Rows[iarow]["Approver EmpCode"]).Trim();

                                            sparA[2] = new SqlParameter("@ApprId", SqlDbType.VarChar);
                                            if (Convert.ToString(dsPOWO.Tables[4].Rows[iarow]["Appr_ID"]).Trim() == "HOD" || Convert.ToString(dsPOWO.Tables[4].Rows[iarow]["Appr_ID"]).Trim() == "PM")
                                                sparA[2].Value = 1;
                                            else if (Convert.ToString(dsPOWO.Tables[4].Rows[iarow]["Appr_ID"]).Trim() == "DH")
                                                sparA[2].Value = 7;
                                            else if (Convert.ToString(dsPOWO.Tables[4].Rows[iarow]["Appr_ID"]).Trim() == "COO")
                                                sparA[2].Value = 11;
                                            else if (Convert.ToString(dsPOWO.Tables[4].Rows[iarow]["Appr_ID"]).Trim() == "CEO")
                                                sparA[2].Value = 13;
                                            else
                                                sparA[2].Value = DBNull.Value;

                                            sparA[3] = new SqlParameter("@InvoiceID", SqlDbType.VarChar);
                                            sparA[3].Value = iInvoiceId;

                                            DataSet dsappr = spm.getDatasetList(sparA, "SP_VSCB_Upload_CutoffData");

                                            if (dsappr != null)
                                            {
                                                if (dsappr.Tables[0].Rows.Count > 0)
                                                {
                                                    if (Convert.ToString(dsappr.Tables[0].Rows[0]["msg"]).Trim() != "insert")
                                                    {
                                                        sberrorMsg.Append("</br>Invoice Approver not updated in OneHr for this invoice :-" + Convert.ToString(sponumber).Trim());
                                                        lblmessage.Text = Convert.ToString(sberrorMsg);
                                                        isValid = false;
                                                        break;
                                                    }

                                                }
                                                else
                                                {
                                                    sberrorMsg.Append("</br>Invoice Approver not updated in OneHr for this invoice :-" + Convert.ToString(sponumber).Trim());
                                                    lblmessage.Text = Convert.ToString(sberrorMsg);
                                                    isValid = false;
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                sberrorMsg.Append("</br>Invoice Approver not updated in OneHr for this invoice :-" + Convert.ToString(sponumber).Trim());
                                                lblmessage.Text = Convert.ToString(sberrorMsg);
                                                isValid = false;
                                                break;
                                            }

                                        }
                                    }
                                    */
                                    #endregion  
                                }
                            }
                            else
                            {
                                sberrorMsg.Append("Invoice details not updated in OneHr for this invoice :-" + Convert.ToString(sponumber).Trim());
                                lblmessage.Text = Convert.ToString(sberrorMsg);
                                isValid = false;
                                break;
                            }
                        }
                        else
                        {
                            sberrorMsg.Append("Invoice details not updated in OneHr for this invoice :-" + Convert.ToString(sponumber).Trim());
                            lblmessage.Text = Convert.ToString(sberrorMsg);
                            isValid = false;
                            break;
                        }
                    }
                }
            }

        }

        #endregion

        sberrorMsg.Clear();
        sberrorMsg.Append("Please correct the below issue and try to re-upload<br/>");

        //Delete PO,Milestone and Invoice if Invoice insert fail
        if (isValid == false)
        {
            DeleteUploadedData();
            return;
        }

        #region validate Payment Request
        if (dsPOWO != null)
        {
            if (dsPOWO.Tables[5].Rows.Count > 0)
            {

                for (int irow = 0; irow < dsPOWO.Tables[5].Rows.Count; irow++)
                {
                    if (Convert.ToString(dsPOWO.Tables[5].Rows[irow]["InvoiceNo"]).Trim() == "")
                    {
                        sberrorMsg.Append("Please enter Invoice No.<br/>");
                        isValid = false;
                    }
                    if (Convert.ToString(dsPOWO.Tables[5].Rows[irow]["PaymentReqDate"]).Trim() == "")
                    {
                        sberrorMsg.Append("Please enter Payment Request Date.<br/>");
                        isValid = false;
                    }
                    if (Convert.ToString(dsPOWO.Tables[5].Rows[irow]["Payment Req Amount"]).Trim() == "")
                    {
                        sberrorMsg.Append("Please enter Payment Request Amount.<br/>");
                        isValid = false;

                    }
                    if (Convert.ToString(dsPOWO.Tables[5].Rows[irow]["Amount Paid by Acc"]).Trim() == "")
                    {
                        sberrorMsg.Append("Please enter Amount paid by Account.<br/>");
                        isValid = false;
                    }
                    if (Convert.ToString(dsPOWO.Tables[5].Rows[irow]["PaymentStatus"]).Trim() == "")
                    {
                        sberrorMsg.Append("Please enter Payment Status.<br/>");
                        isValid = false;
                    }

                    if (Convert.ToString(dsPOWO.Tables[5].Rows[irow]["PaymentStatus"]).Trim() == "")
                    {
                        sberrorMsg.Append("Please enter Payment Status.<br/>");
                        isValid = false;
                    }

                    if (Convert.ToString(dsPOWO.Tables[5].Rows[irow]["Is Batch Created"]).Trim() == "")
                    {
                         
                          sberrorMsg.Append("Please enter Is Batch Created.<br/>");
                          isValid = false;
                         
                     }

                    //if (Convert.ToString(dsPOWO.Tables[5].Rows[irow]["PaymentStatus"]).Trim() == "Partial")
                    //{
                    //    sberrorMsg.Append("Please check with Sanjay Payment Status is Partial.<br/>");
                    //    isValid = false;
                    //}


                    #region Check Invoice Created for for the PO

                    SqlParameter[] sparP = new SqlParameter[3];
                    sparP[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
                    sparP[0].Value = "Validate_InvoiceOnPaymentReq_Cutoffdata";

                    sparP[1] = new SqlParameter("@PONumber", SqlDbType.VarChar);
                    sparP[1].Value = Convert.ToString(sponumber).Trim();

                    sparP[2] = new SqlParameter("@InvoiceNo", SqlDbType.VarChar);
                    if (Convert.ToString(dsPOWO.Tables[5].Rows[irow]["InvoiceNo"]).Trim() != "")
                        sparP[2].Value = Convert.ToString(dsPOWO.Tables[5].Rows[irow]["InvoiceNo"]).Trim();
                    else
                        sparP[2].Value = DBNull.Value;

                    DataSet dsInvoicePayment = spm.getDatasetList(sparP, "SP_VSCB_Upload_CutoffData");

                    if (dsInvoicePayment != null)
                    {
                        if (dsInvoicePayment.Tables[0].Rows.Count == 0)
                        {
                            sberrorMsg.Append("This Invoice No:- " + Convert.ToString(dsPOWO.Tables[5].Rows[irow]["InvoiceNo"]).Trim() + " not uploaded in OneHr.<br/>");
                            isValid = false;
                        }
                    }
                    #endregion
                    if (isValid == false)
                    {
                        lblmessage.Text = Convert.ToString(sberrorMsg).Trim();
                        break;
                    }
                }
            }
        }
        #endregion

        sberrorMsg.Clear();
        sberrorMsg.Append("Please correct the below issue and try to re-upload<br/>");

        //Delete PO,Milestone and Invoice if Payment Validate fail
        if (isValid == false)
        {
            DeleteUploadedData();
            return;
        }

        #region Insert Payment Request Cutoff data
       
        if (dsPOWO.Tables[5].Rows.Count > 0)
        {
            sberrorMsg.Clear();
            sberrorMsg.Append("Please correct the below issue and try to re-upload<br/>");

            for (int irow = 0; irow < dsPOWO.Tables[5].Rows.Count; irow++)
            {
                if (Convert.ToString(dsPOWO.Tables[5].Rows[irow]["InvoiceNo"]).Trim()!="")
                {
                    string[] strdate;
                    string spaymentDate= "";                      

                    #region date formatting
                    if (Convert.ToString(dsPOWO.Tables[5].Rows[irow]["PaymentReqDate"]).Trim() != "")
                    {
                        strdate = Convert.ToString(dsPOWO.Tables[5].Rows[irow]["PaymentReqDate"]).Trim().Split('-');
                        spaymentDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                    }
                    #endregion

                    SqlParameter[] sparP = new SqlParameter[11];
                    sparP[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
                    sparP[0].Value = "Upload_PaymentReq_Cutoffdata";

                    sparP[1] = new SqlParameter("@PONumber", SqlDbType.VarChar);
                    sparP[1].Value = Convert.ToString(sponumber).Trim();

                    sparP[2] = new SqlParameter("@InvoiceNo", SqlDbType.VarChar);
                    sparP[2].Value = Convert.ToString(dsPOWO.Tables[5].Rows[irow]["InvoiceNo"]).Trim();

                    sparP[3] = new SqlParameter("@IsPartialPayment", SqlDbType.VarChar);
                    if (Convert.ToString(dsPOWO.Tables[5].Rows[irow]["PaymentStatus"]).Trim() == "Partial")
                        sparP[3].Value = 1;
                    else
                        sparP[3].Value = 0; 

                    sparP[4] = new SqlParameter("@InvoiceDate", SqlDbType.VarChar);
                    sparP[4].Value = spaymentDate;

                    sparP[5] = new SqlParameter("@Payable_Amt_With_Tax", SqlDbType.VarChar);
                    sparP[5].Value = Convert.ToString(dsPOWO.Tables[5].Rows[irow]["Payment Req Amount"]).Trim();

                    sparP[6] = new SqlParameter("@AccountPaidAmt", SqlDbType.VarChar);
                    sparP[6].Value = Convert.ToString(dsPOWO.Tables[5].Rows[irow]["Amount Paid by Acc"]).Trim();

                    sparP[7] = new SqlParameter("@PO_Bal_Amt", SqlDbType.VarChar);
                    if (Convert.ToString(dsPOWO.Tables[5].Rows[irow]["Payment Balance Amt"]).Trim() != "")
                        sparP[7].Value = Convert.ToString(dsPOWO.Tables[5].Rows[irow]["Payment Balance Amt"]).Trim();
                    else
                        sparP[7].Value = "0";

                    sparP[8] = new SqlParameter("@Ref_PaymentReqNo", SqlDbType.VarChar);
                    sparP[8].Value = Convert.ToString(dsPOWO.Tables[5].Rows[irow]["Payment Ref No"]).Trim();

                    sparP[9] = new SqlParameter("@PaymentStatusID", SqlDbType.VarChar);
                    if (Convert.ToString(dsPOWO.Tables[5].Rows[irow]["PaymentStatus"]).Trim() == "Pending")
                        sparP[9].Value = 1;
                    else if (Convert.ToString(dsPOWO.Tables[5].Rows[irow]["PaymentStatus"]).Trim() == "Partial")
                        sparP[9].Value = 3;
                    else
                        sparP[9].Value = 2;

                    sparP[10] = new SqlParameter("@IsBatchApproved", SqlDbType.VarChar);
                    if (Convert.ToString(dsPOWO.Tables[5].Rows[irow]["Is Batch Created"]).Trim() == "No")
                        sparP[10].Value = 0;
                    else
                        sparP[10].Value = 1;

                       
                         
                    DataSet dsInvoicePayment = spm.getDatasetList(sparP, "SP_VSCB_Upload_CutoffData");

                    if (dsInvoicePayment != null)
                    {
                        if (dsInvoicePayment.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToString(dsInvoicePayment.Tables[0].Rows[0]["msg"]).Trim() != "insert")
                            {
                                sberrorMsg.Append("Payment Request not updated in OneHr.<br/>");
                                isValid = false;
                            }
                        }
                        else
                        {
                            sberrorMsg.Append("Payment Request not updated in OneHr.<br/>");
                            isValid = false;
                        }
                    }
                    else
                    {
                        sberrorMsg.Append("Payment Request not updated in OneHr.<br/>");
                        isValid = false; 
                    }

                    
                    if (isValid == false)
                    {
                        lblmessage.Text = Convert.ToString(sberrorMsg).Trim();
                        break;
                    }
                }
            }
        }
       
        #endregion

        //Delete PO,Milestone and Invoice,payment req if Payment insert fail
        if (isValid == false)
        {
            DeleteUploadedData();
            return;
        }



        if (isValid == true)
        {
           if( Uploaded_Cutoff_MainData()==false)
            {
                sberrorMsg.Append("Procurment Cutoff data not uploaded in OneHr. Please try to re-upload<br/>");
                lblmessage.Text = Convert.ToString(sberrorMsg).Trim();
                DeleteUploadedData();
                
            }
        }
    }

   

   

    private bool Upload_Milestones (double dPOWOID,DataTable dtMilestones,string sponumber)
    {
        bool blnInsert = false;
        try
        {
           if(dtMilestones.Rows.Count>0)
            {
                Int32 iInsertMilestoneCount = 0;
                for(int irow=0;irow<dtMilestones.Rows.Count;irow++)
                {
                    if (Convert.ToString(dtMilestones.Rows[irow]["PONumber"]).Trim() != "")
                    {
                        if (sponumber == Convert.ToString(dtMilestones.Rows[irow]["PONumber"]).Trim())
                        {

                            DataSet dsMiles = new DataSet();
                            SqlParameter[] sparM = new SqlParameter[23];

                            string[] strdate;
                            string sMilestoneDueDate = "";

                            #region date formatting
                            if (Convert.ToString(dtMilestones.Rows[irow]["Milestone_due_date"]).Trim() != "")
                            {
                                strdate = Convert.ToString(dtMilestones.Rows[0]["Milestone_due_date"]).Trim().Split('-');
                                sMilestoneDueDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                            }
                            #endregion

                            sparM[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
                            sparM[0].Value = "Upload_POWOMilestone_Cutoffdata";

                            sparM[1] = new SqlParameter("@PONumber", SqlDbType.VarChar);
                            sparM[1].Value = sponumber;

                            sparM[2] = new SqlParameter("@TSrno", SqlDbType.VarChar);
                            sparM[2].Value = Convert.ToString(dtMilestones.Rows[irow]["Milestone Sr No"]);

                            sparM[3] = new SqlParameter("@MilestoneName", SqlDbType.VarChar);
                            sparM[3].Value = Convert.ToString(dtMilestones.Rows[irow]["MilestoneName"]).Trim();

                            sparM[4] = new SqlParameter("@MilestoneDescription", SqlDbType.VarChar);
                            sparM[4].Value = Convert.ToString(dtMilestones.Rows[irow]["MilestoneDescription"]).Trim();

                            sparM[5] = new SqlParameter("@Milestone_due_date", SqlDbType.VarChar);
                            sparM[5].Value = Convert.ToString(sMilestoneDueDate).Trim();

                             sparM[6] = new SqlParameter("@Quantity", SqlDbType.VarChar);
                            sparM[6].Value = Convert.ToString(dtMilestones.Rows[irow]["Quantity"]).Trim();

                            sparM[7] = new SqlParameter("@Rate", SqlDbType.VarChar);
                            sparM[7].Value = Convert.ToString(dtMilestones.Rows[irow]["Rate"]).Trim();

                            sparM[8] = new SqlParameter("@Amount", SqlDbType.VarChar);
                            sparM[8].Value = Convert.ToString(dtMilestones.Rows[irow]["Amount"]).Trim();

                           sparM[9] = new SqlParameter("@CGST_Per", SqlDbType.VarChar);
                            if (Convert.ToString(dtMilestones.Rows[irow]["CGST_Per"]).Trim() == "")
                                sparM[9].Value = DBNull.Value;
                            else if (Convert.ToString(dtMilestones.Rows[irow]["CGST_Per"]).Trim() == "0.00")
                                sparM[9].Value = DBNull.Value;
                            else if (Convert.ToString(dtMilestones.Rows[irow]["CGST_Per"]).Trim() != "0.00")
                                sparM[9].Value = Convert.ToString(dtMilestones.Rows[irow]["CGST_Per"]).Trim();
                            else
                                sparM[9].Value = DBNull.Value;

                            sparM[10] = new SqlParameter("@SGST_Per", SqlDbType.VarChar);
                            if (Convert.ToString(dtMilestones.Rows[irow]["SGST_Per"]).Trim() == "")
                                sparM[10].Value = DBNull.Value;
                            else if (Convert.ToString(dtMilestones.Rows[irow]["SGST_Per"]).Trim() == "0.00")
                                sparM[10].Value = DBNull.Value;                          
                            else if (Convert.ToString(dtMilestones.Rows[irow]["SGST_Per"]).Trim() != "0.00")
                                sparM[10].Value = Convert.ToString(dtMilestones.Rows[irow]["SGST_Per"]).Trim();
                            else
                                sparM[10].Value = DBNull.Value;

                            sparM[11] = new SqlParameter("@IGST_Per", SqlDbType.VarChar);
                            if (Convert.ToString(dtMilestones.Rows[irow]["IGST_Per"]).Trim() == "")
                                sparM[11].Value = DBNull.Value;
                            else  if (Convert.ToString(dtMilestones.Rows[irow]["IGST_Per"]).Trim() == "0.00")
                                sparM[11].Value = DBNull.Value;                          
                            else if (Convert.ToString(dtMilestones.Rows[irow]["IGST_Per"]).Trim() != "0.00")
                                sparM[11].Value = Convert.ToString(dtMilestones.Rows[irow]["IGST_Per"]).Trim();
                            else
                                sparM[11].Value = DBNull.Value;

                            sparM[12] = new SqlParameter("@CGST_Amt", SqlDbType.VarChar);
                            if (Convert.ToString(dtMilestones.Rows[irow]["CGST_Amount"]).Trim() != "")
                                sparM[12].Value = Convert.ToString(dtMilestones.Rows[irow]["CGST_Amount"]).Trim();
                            else
                                sparM[12].Value = DBNull.Value;

                            sparM[13] = new SqlParameter("@SGST_Amt", SqlDbType.VarChar);
                            if (Convert.ToString(dtMilestones.Rows[irow]["SGST_Amount"]).Trim() != "")
                                sparM[13].Value = Convert.ToString(dtMilestones.Rows[irow]["SGST_Amount"]).Trim();
                            else
                                sparM[13].Value = DBNull.Value;

                            sparM[14] = new SqlParameter("@IGST_Amt", SqlDbType.VarChar);
                            if (Convert.ToString(dtMilestones.Rows[irow]["IGST_Amount"]).Trim() != "")
                                sparM[14].Value = Convert.ToString(dtMilestones.Rows[irow]["IGST_Amount"]).Trim();
                            else
                                sparM[14].Value = DBNull.Value;

                            sparM[15] = new SqlParameter("@Purchase_Ledger", SqlDbType.VarChar);
                            sparM[15].Value = Convert.ToString(dtMilestones.Rows[irow]["Purchase_Ledger"]).Trim();

                            sparM[16] = new SqlParameter("@CGST_Ledger", SqlDbType.VarChar);
                            sparM[16].Value = Convert.ToString(dtMilestones.Rows[irow]["CGST_Ledger"]).Trim();

                            sparM[17] = new SqlParameter("@SGST_Ledger", SqlDbType.VarChar);
                            sparM[17].Value = Convert.ToString(dtMilestones.Rows[irow]["SGST_Ledger"]).Trim();

                            sparM[18] = new SqlParameter("@IGST_Ledger", SqlDbType.VarChar);
                            sparM[18].Value = Convert.ToString(dtMilestones.Rows[irow]["IGST_Ledger"]).Trim();

                            sparM[19] = new SqlParameter("@AmtWithTax", SqlDbType.VarChar);
                            sparM[19].Value = Convert.ToString(dtMilestones.Rows[irow]["Milestome Amt With Tax"]).Trim();

                            sparM[20] = new SqlParameter("@Collect_TDS_Amt", SqlDbType.VarChar);
                            if (Convert.ToString(dtMilestones.Rows[irow]["Collect_TDS_Amt"]).Trim() != "")
                                sparM[20].Value = Convert.ToString(dtMilestones.Rows[irow]["Collect_TDS_Amt"]).Trim();
                            else
                                sparM[20].Value = DBNull.Value;

                            sparM[21] = new SqlParameter("@Milesstone_Balance_Amt", SqlDbType.VarChar);
                            sparM[21].Value = Convert.ToString(dtMilestones.Rows[irow]["Milesstone_Balance_Amt"]).Trim();

                            sparM[22] = new SqlParameter("@MilestonePaymentStatus", SqlDbType.VarChar);
                            if (Convert.ToString(dtMilestones.Rows[irow]["PaymentStatus"]).Trim() == "Paid")
                                sparM[22].Value = "2";
                            else if (Convert.ToString(dtMilestones.Rows[irow]["PaymentStatus"]).Trim() == "Pending")
                                sparM[22].Value = "1";
                            else if (Convert.ToString(dtMilestones.Rows[irow]["PaymentStatus"]).Trim() == "Partial")
                                sparM[22].Value = "3";
                            else
                                sparM[22].Value = DBNull.Value;

 
                            dsMiles = spm.getDatasetList(sparM, "SP_VSCB_Upload_CutoffData");
                            blnInsert = false;
                            if (dsMiles!=null)
                            {
                                if(dsMiles.Tables[0].Rows.Count>0)
                                {
                                    blnInsert = true;
                                    iInsertMilestoneCount += 1;
                                }
                            }
                        }
                    }
                }

                if(iInsertMilestoneCount != dtMilestones.Rows.Count)
                {
                    blnInsert = false;
                }
            }
        }
        catch(Exception ex)
        {

        }
        return blnInsert;
    }

    private bool Upload_POWOApprovers(double dPOWOID, DataTable dtApprover, string sponumber)
    {
        bool blnIsuploadAppr = false;
        try
        {
            if (dtApprover.Rows.Count > 0)
            {
                Int32 iInsertApproverCount = 0;
                for (int irow = 0; irow < dtApprover.Rows.Count; irow++)
                {
                    if (Convert.ToString(dtApprover.Rows[irow]["PONumber"]).Trim() != "")
                    {
                        if (sponumber == Convert.ToString(dtApprover.Rows[irow]["PONumber"]).Trim())
                        {
                            DataSet dsMiles = new DataSet();
                            SqlParameter[] sparM = new SqlParameter[4];

                            sparM[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
                            sparM[0].Value = "Upload_POWOApprover_Cutoffdata";

                            sparM[1] = new SqlParameter("@PONumber", SqlDbType.VarChar);
                            sparM[1].Value = sponumber;

                            sparM[2] = new SqlParameter("@ApprId", SqlDbType.VarChar);
                            if (Convert.ToString(dtApprover.Rows[irow]["Appr_ID"]).Trim() == "HOD" || Convert.ToString(dtApprover.Rows[irow]["Appr_ID"]).Trim() == "PM")
                                sparM[2].Value = 1;
                            else if (Convert.ToString(dtApprover.Rows[irow]["Appr_ID"]).Trim() == "DH")
                                sparM[2].Value = 7;
                            else if (Convert.ToString(dtApprover.Rows[irow]["Appr_ID"]).Trim() == "COO")
                                sparM[2].Value = 11;
                            else if (Convert.ToString(dtApprover.Rows[irow]["Appr_ID"]).Trim() == "CEO")
                                sparM[2].Value = 13;
                            else
                                sparM[2].Value = DBNull.Value;

                            sparM[3] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                            sparM[3].Value = Convert.ToString(dtApprover.Rows[irow]["Approver EmpCode"]).Trim();

                            dsMiles = spm.getDatasetList(sparM, "SP_VSCB_Upload_CutoffData");
                            blnIsuploadAppr = false;
                            if (dsMiles != null)
                            {
                                if (dsMiles.Tables[0].Rows.Count > 0)
                                {
                                    blnIsuploadAppr = true;
                                    iInsertApproverCount += 1;
                                }
                            }
                        }
                    }
                }

                if (iInsertApproverCount != dtApprover.Rows.Count)
                {
                    blnIsuploadAppr = false;
                }
            }
        }
        catch(Exception ex)
        {
            blnIsuploadAppr = false;
        }
        return blnIsuploadAppr;
    }

    private void DeleteUploadedData()
    {
        SqlParameter[] sparM = new SqlParameter[1];

        DataSet dsDelete = new DataSet();

        sparM[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
        sparM[0].Value = "Delete_Uploaded_Cutoffdata";
        dsDelete = spm.getDatasetList(sparM, "SP_VSCB_Upload_CutoffData");

    }

    private Boolean Uploaded_Cutoff_MainData()
    {
        Boolean blnc = false;
        SqlParameter[] sparM = new SqlParameter[1];

        DataSet dsuplaod = new DataSet();

        sparM[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
        sparM[0].Value = "Upload_Cutoffdata_Main_tables";

        dsuplaod = spm.getDatasetList(sparM, "SP_VSCB_Upload_CutoffData");

        if (dsuplaod != null)
        {
            if (dsuplaod.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(dsuplaod.Tables[0].Rows[0]["msg"]).Trim() == "upload")
                {
                    blnc = true;
                    lblmessage.Text ="PO Cutoff data uploaded Successfully" ;
                }
            }
        }
       
        return blnc;



    }
}