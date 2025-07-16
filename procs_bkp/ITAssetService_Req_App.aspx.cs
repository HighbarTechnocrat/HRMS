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
using System.Activities.Expressions;
using DocumentFormat.OpenXml.Spreadsheet;


public partial class ITAssetService_Req_App : System.Web.UI.Page
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
    public static DataTable dtSearchAsset = new DataTable();
    public static DataTable dtSelectedOptions = new DataTable();
    SP_Methods spm = new SP_Methods();
    string strempcode = "";int Id;
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    
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

            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                Page.SmartNavigation = true;
                hdnEmpCode.Value = Convert.ToString(Session["Empcode"]);
                if (!Page.IsPostBack)
                {
                    string strfilepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["AssetInventoryPath"]).Trim());
                    FilePath.Value = strfilepath;
                    editform.Visible = true;
                    if (Request.QueryString.Count > 0)
                    {
                        hdnId.Value = Convert.ToString(Request.QueryString[0]);
                        hdnRemid_Type.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        var getType = Convert.ToString(Request.QueryString[1]).Trim();
                        Id = Convert.ToInt32(hdnId.Value);
                        GetDetails(Id);
                        if (getType == "arr")
                        {
                            mobile_btnSave.Visible = false;
                            mobile_cancel.Visible = false;
                            mobile_btnBack.Visible = false;
                            txtRemarks.Enabled = false;
                            ddlAssignment.Enabled = false;
                            uploadfile.Enabled = false;
                            backToArr.Visible = true;

                            foreach (GridViewRow i in gvAssetOption.Rows)
                            {
                                RadioButton Rb = (RadioButton)i.FindControl("rbSelect");
                                Rb.Checked = true;
                            }
                            gvAssetOption.Enabled = false;

                        }
                        SearchAsset();

                        if (getType == "emp")
                        {
                            mobile_btnSave.Visible = false;
                            mobile_cancel.Visible = false;
                            mobile_btnBack.Visible = false;
                            txtRemarks.Enabled = false;
                            ddlAssignment.Enabled = false;
                            uploadfile.Enabled = false;
                            backToEmployee.Visible = true;

                        }

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
    public void GetDetails(int Id)
    {
        try
        {
            string EmpCode = "";
            string CustodianCode = "";
            int ReqStatus;

            DataTable dtDetails = new DataTable();
            dtDetails = spm.GetDetails(Id);
            if (dtDetails.Rows.Count > 0)
            {
                EmpCode = Convert.ToString(dtDetails.Rows[0]["EmpCode"]);
                txtAssetAlloReqNo.Text = Convert.ToString(dtDetails.Rows[0]["AARNo"]);
                var reqDate = Convert.ToDateTime(dtDetails.Rows[0]["AssignedDate"]).ToString("dd/MM/yyyy");
                txtReqDate.Text = reqDate;
                //var getDate = DateTime.Now.ToString("dd/MM/yyyy");
                //getDate = getDate.Replace('-', '/');
                //txtReqDate.Text = getDate;
                //string[] arreqDate = reqDate.Split(' ');
                //txtReqDate.Text = arreqDate[0].ToString();
                CustodianCode = Convert.ToString(dtDetails.Rows[0]["CustodianCode"]);
                hdnCustodianCode.Value = CustodianCode;
                txtCustRemarks.Text = Convert.ToString(dtDetails.Rows[0]["Remarks"]);
                txtRemarks.Text = Convert.ToString(dtDetails.Rows[0]["AppRemarks"]);
                if (Convert.ToString(dtDetails.Rows[0]["isAssigned"]) == "1")
                {
                    ddlAssignment.SelectedValue = "1";
                }
                else { ddlAssignment.SelectedValue = "0"; }

                ReqStatus = Convert.ToInt32(dtDetails.Rows[0]["Status"]);
                if ((ReqStatus == 3) || (ReqStatus == 4) || (ReqStatus == 5))
                {
                    ddlAssignment.Enabled = false;
                    txtRemarks.Enabled = false;
                    mobile_btnSave.Enabled = false;
                    mobile_cancel.Enabled = false;
                    mobile_btnBack.Enabled = false;
                    gvViewFiles.Enabled = false;
                   //gvAssetOption.Enabled = false;
                    ddlAssignment.Enabled = false;

                    foreach (GridViewRow i in gvAssetOption.Rows)
                    {
                        RadioButton Rb = (RadioButton)i.FindControl("rbSelect");
                        Rb.Checked = true;
                    }
                    gvAssetOption.Enabled = false;
                }

            }

            GetOptionList(txtAssetAlloReqNo.Text.ToString().Trim());
            GetEmployeeDetails(EmpCode);
            GetFilesDetails(txtAssetAlloReqNo.Text.ToString().Trim(), EmpCode, CustodianCode);


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

            throw;
        }
    }

    public void GetEmployeeDetails(string EmpCode)
    {
        try
        {
            DataTable dtEmpDetails = new DataTable();
            dtEmpDetails = spm.GetEmpData(EmpCode);
            if (dtEmpDetails.Rows.Count > 0)
            {
                txtEmpCode.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Code"]);
                txtEmpName.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
                txtEmpLocation.Text = Convert.ToString(dtEmpDetails.Rows[0]["emp_location"]);
                txtEmpDesig.Text = Convert.ToString(dtEmpDetails.Rows[0]["Designation"]);
                txtEmpDept.Text = Convert.ToString(dtEmpDetails.Rows[0]["Department"]);
                txtEmpType.Text = Convert.ToString(dtEmpDetails.Rows[0]["Particulars"]);
                txtDoj.Text = Convert.ToString(dtEmpDetails.Rows[0]["doj"]);
                txtMainModule.Text = Convert.ToString(dtEmpDetails.Rows[0]["emp_module"]);
                txtRMgr.Text = Convert.ToString(dtEmpDetails.Rows[0]["RM"]);
                txtHOD.Text = Convert.ToString(dtEmpDetails.Rows[0]["HOD"]);
                txtEmail.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
                txtMobileNo.Text = Convert.ToString(dtEmpDetails.Rows[0]["mobile"]);
                txtCurAddress.Text = Convert.ToString(dtEmpDetails.Rows[0]["C_ADD"]);
                txtPerAddress.Text = Convert.ToString(dtEmpDetails.Rows[0]["P_ADD"]);
            }



        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

            throw;
        }
    }
    public void GetFilesDetails(string AARNo, string EmpCode, string CustodianCode)
    {
        DataTable dtDetails = new DataTable();
        dtDetails = spm.GetFileDetails(AARNo, EmpCode, CustodianCode);
        gvViewFiles.DataSource = null;
        gvViewFiles.DataBind();
        if (dtDetails.Rows.Count > 0)
        {
            gvViewFiles.DataSource = dtDetails;
            gvViewFiles.DataBind();
        }
    }
    public void GetOptionList(string AARNo)
    {
        DataTable dtDetails = new DataTable();
        dtDetails = spm.GetOptionList(AARNo);
        //gvAssetOption.DataSource = null;
        gvAssetOption.DataBind();
        if (dtDetails.Rows.Count > 0)
        {
            gvAssetOption.DataSource = dtDetails;
            gvAssetOption.DataBind();
        }
        lblAssetOptions.Visible = true;
    }

    #endregion

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        //APPROVE
        //update ITAssetRequestDetails,ITAssetRequestLog//update AssetOptionList//upload App Files
        //UpdateAutoChkInData/Email to user/email to all custodians
        lblmessage.Text = "";
        string[] strdate;
        string strfromDate = "";
        string filename = "";
        String strfileName = "";
        string AppRemarks = "";
        int isAssigned = Convert.ToInt32(ddlAssignment.SelectedValue);
        ///collect data for Email
        var EmpName = txtEmpName.Text.ToString().Trim();
        var AssetAlloReqNo = txtAssetAlloReqNo.Text.ToString().Trim();
        var EmpCode = txtEmpCode.Text.ToString().Trim();
        var Doj = txtDoj.Text.ToString().Trim();
        var Location = txtEmpLocation.Text.ToString().Trim();
        var Dept = txtEmpDept.Text.ToString().Trim();
        var Designation = txtEmpDesig.Text.ToString().Trim();
        var RMgr = txtRMgr.Text.ToString().Trim();
        var HOD = txtHOD.Text.ToString().Trim();
        var ReqDate = txtReqDate.Text.ToString().Trim();
        var emailTo = txtEmail.Text.ToString().Trim();
        var Remarks = txtCustRemarks.Text.ToString().Trim();

        try
        {
            if (Convert.ToString(txtRemarks.Text).Trim() != "")
            {
                AppRemarks = txtRemarks.Text.ToString().Trim();
            }
            if (uploadfile.HasFile)
            {
                filename = uploadfile.FileName;
            }
            #region insert or upload multiple files
            if (uploadfile.HasFile)
            {
                filename = uploadfile.FileName;
                if (Convert.ToString(filename).Trim() != "")
                {
                    #region date formatting
                    if (Convert.ToString(txtReqDate.Text).Trim() != "")
                    {
                        strdate = Convert.ToString(txtReqDate.Text).Trim().Split('/');
                        strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
                    }
                    #endregion

                    string AssetInventoryPath = "";
                    //if (Convert.ToString(hdnRemid.Value).Trim() != "0")
                    //    FuelclaimPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PaymentVoucherDocumentpath"]).Trim());
                    //else
                    AssetInventoryPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["AssetInventoryPath"]).Trim());

                    bool folderExists = Directory.Exists(AssetInventoryPath);
                    if (!folderExists)
                    {
                        Directory.CreateDirectory(AssetInventoryPath);
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
                            strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + Convert.ToString(i + 1).Trim() + uploadfileName.FileName.ToString();
                            filename = strfileName;
                            uploadfileName.SaveAs(Path.Combine(AssetInventoryPath, strfileName));
                            spm.InserITAssetRequestFiles(txtAssetAlloReqNo.Text.ToString().Trim(), txtEmpCode.Text.ToString().Trim(), hdnEmpCode.Value, Convert.ToString(strfileName).Trim());
                            blnfile = true;
                        }
                    }
                }
            }
            #endregion

            string AssetNumberID = ""; string AssetNumber = "";
            string AssigneTo = txtEmpCode.Text.ToString().Trim();
            if (ddlAssignment.SelectedValue == "1")
            {
                foreach (GridViewRow row in gvAssetOption.Rows)
                {
                    RadioButton rb = (RadioButton)row.FindControl("rbSelect");
                    if (rb.Checked == true)
                    {
                        AssetNumberID = row.Cells[0].Text;
                        AssetNumber = row.Cells[1].Text;
                    }
                }
                spm.UpdateAutoCheckInData(AssigneTo, AssetNumberID);

                spm.InsertITAssetServiceRequestDetails(Convert.ToString(Session["Empcode"]).Trim(), Remarks, AppRemarks, "UpdateWF", AssigneTo, isAssigned, 4, 1, Convert.ToString(Session["Empcode"]).Trim(), Convert.ToInt32(hdnId.Value));
                //update AssetoptionList to Selected AssetNumbar
                spm.UpdateAssetOptionList(AssetAlloReqNo, AssetNumber, "1");
            }
            else
            {
                spm.InsertITAssetServiceRequestDetails(Convert.ToString(Session["Empcode"]).Trim(), Remarks, AppRemarks, "UpdateWF", AssigneTo, isAssigned, 5, 1, Convert.ToString(Session["Empcode"]).Trim(), Convert.ToInt32(hdnId.Value));
            }
            //Get All Custodians-
            string to_email = ""; string cc_email = "";
            DataTable t2 = spm.GetAllCustodianDetails();
            if (t2.Rows.Count > 0)
            {
                to_email = "";

                foreach (DataRow row in t2.Rows)
                {
                    to_email = to_email + Convert.ToString(row["email"].ToString()) + ";";
                    cc_email = cc_email + Convert.ToString(row["email"].ToString()) + ";";
                }
            }

            //Get HODEmailid GetHODemailId
            DataTable dtHod = spm.GetHODemailId();
            string HodEmailcc = "";
            if (dtHod.Rows.Count > 0)
            {
                HodEmailcc = Convert.ToString(dtHod.Rows[0]["HodEmailId"]);
            }

            //Get RM and PRM of employee
            string RM = "";
            string PRM = "";
            DataTable t = spm.GetEmpDataByEmpCode(txtEmpCode.Text.ToString().Trim());
            if (t.Rows.Count > 0)
            {
                RM = Convert.ToString(t.Rows[0]["RMEmail"]);
                PRM = Convert.ToString(t.Rows[0]["PRMEmail"]);
            }
            //Get HR HOD Emailid
            string HR = "";
            DataTable t1 = spm.GetHrHOD();
            if (t1.Rows.Count > 0)
            {
                HR = Convert.ToString(t1.Rows[0]["HREmail"]);
            }

            if (ddlAssignment.SelectedValue == "1")
            {
                //send asset allocation mail to emp/custodian,PN,Rm,HOD
                DataTable dtData = spm.GetCheckInDataById(Convert.ToInt32(AssetNumberID));
                //GetCheckInDataById
                if (dtData != null)
                {
                    string AssetType = Convert.ToString(dtData.Rows[0]["AssetType"]);
                    string AssetDesc = Convert.ToString(dtData.Rows[0]["AssetDesc"]);
                    string SrNo = Convert.ToString(dtData.Rows[0]["SrNo"]);
                    string Manufacturer = Convert.ToString(dtData.Rows[0]["BrandName"]);
                    string Model = Convert.ToString(dtData.Rows[0]["Model"]);
                    string EmailTo = Convert.ToString(dtData.Rows[0]["Emp_Emailaddress"]);
                    //SendAssetAssignmentMail(AssetType, AssetDesc, SrNo, Manufacturer, Model, EmailTo, HodEmailcc, cc_email, RM, PRM, HR);
                    //send Asset Assignment mail to Newjoinee
                    string ITAssetService_Req_App = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ITAssetTermsAndConditions.aspx";
                    string redirectURL = Convert.ToString(ITAssetService_Req_App).Trim() + "?id=" + Convert.ToInt32(hdnId.Value) + "&type=app";
                    SendAssetAlocationMailToNewJoinee(EmpName, AssetType, AssetDesc, SrNo, Manufacturer, Model, emailTo, redirectURL);
                }

                //send Asset Assignment mail to Newjoinee
                //string ITAssetService_Req_App = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ITAssetTermsAndConditions.aspx";
                //string redirectURL = Convert.ToString(ITAssetService_Req_App).Trim() + "?id=" + Convert.ToInt32(hdnId.Value) + "&type=app";
                //SendNotificationMailToNewJoinee(EmpName, AssetAlloReqNo, EmpCode, Doj, Location, Dept, Designation, RMgr, HOD, "", redirectURL, emailTo);

            }
            SendNotificationMailToCustodians(EmpName, AssetAlloReqNo, EmpCode, Doj, Location, Dept, Designation, RMgr, HOD, "", to_email, "Approve", "");
            lblmessage.Text = "IT Asset Service Request Approved Successfully";
            Response.Redirect("~/procs/ITAssetService.aspx");
        }
        catch (Exception ex)
        {
            lblmessage.Visible = true;
            lblmessage.Text = ex.Message.ToString();
            return;
        }
    }

    protected void mobile_cancel_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        string[] strdate;
        string strfromDate = "";
        string filename = "";
        String strfileName = "";
        string AppRemarks = "";
        int isAssigned = Convert.ToInt32(ddlAssignment.SelectedValue);
        ///collect data for Email
        var EmpName = txtEmpName.Text.ToString().Trim();
        var AssetAlloReqNo = txtAssetAlloReqNo.Text.ToString().Trim();
        var EmpCode = txtEmpCode.Text.ToString().Trim();
        var Doj = txtDoj.Text.ToString().Trim();
        var Location = txtEmpLocation.Text.ToString().Trim();
        var Dept = txtEmpDept.Text.ToString().Trim();
        var Designation = txtEmpDesig.Text.ToString().Trim();
        var RMgr = txtRMgr.Text.ToString().Trim();
        var HOD = txtHOD.Text.ToString().Trim();
        var ReqDate = txtReqDate.Text.ToString().Trim();
        var emailTo = txtEmail.Text.ToString().Trim();
        var Remarks = txtCustRemarks.Text.ToString().Trim();
        try
        {
            if (Convert.ToString(txtRemarks.Text).Trim() != "")
            {
                AppRemarks = txtRemarks.Text.ToString().Trim();
            }
            if (uploadfile.HasFile)
            {
                filename = uploadfile.FileName;
            }
            #region insert or upload multiple files
            if (uploadfile.HasFile)
            {
                filename = uploadfile.FileName;
                if (Convert.ToString(filename).Trim() != "")
                {
                    #region date formatting
                    if (Convert.ToString(txtReqDate.Text).Trim() != "")
                    {
                        strdate = Convert.ToString(txtReqDate.Text).Trim().Split('/');
                        strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
                    }
                    #endregion

                    string AssetInventoryPath = "";
                    //if (Convert.ToString(hdnRemid.Value).Trim() != "0")
                    //    FuelclaimPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PaymentVoucherDocumentpath"]).Trim());
                    //else
                    AssetInventoryPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["AssetInventoryPath"]).Trim());

                    bool folderExists = Directory.Exists(AssetInventoryPath);
                    if (!folderExists)
                    {
                        Directory.CreateDirectory(AssetInventoryPath);
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
                            strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + Convert.ToString(i + 1).Trim() + uploadfileName.FileName.ToString();
                            filename = strfileName;
                            uploadfileName.SaveAs(Path.Combine(AssetInventoryPath, strfileName));

                            //spm.InsertpaymentUploaded_Files(Convert.ToDecimal(hdnRemid.Value), blnfile, Convert.ToString(strfileName).Trim(), "paymentVoucher_insertTmp", i + 1, Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToInt32(hdnclaimqry.Value));
                            spm.InserITAssetRequestFiles(txtAssetAlloReqNo.Text.ToString().Trim(), txtEmpCode.Text.ToString().Trim(), hdnEmpCode.Value, Convert.ToString(strfileName).Trim());
                            blnfile = true;
                        }
                    }
                }
            }
            #endregion



            //SqlParameter[] spars2 = new SqlParameter[2];

            //spars2[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            //spars2[0].Value = "getSoftware_listdective";
            //foreach (GridViewRow assetRows in gvAssetOption.Rows)
            //{
            //    if (assetRows.RowType == DataControlRowType.DataRow)
            //    {

            //        spars2[1] = new SqlParameter("@AssetNumber", SqlDbType.NVarChar);
            //        spars2[1].Value = Convert.ToString(assetRows.Cells[1].Text).Trim();

            //        DataTable dssoftwareLists = spm.getDataList(spars2, "SP_Admin_AssetInventory");
            //    }
            //}

            foreach (GridViewRow row in gvAssetOption.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    // Access the cell value from the current row
                    string asset = Convert.ToString(row.Cells[1].Text).Trim();

                    // Define SQL parameters
                    SqlParameter[] spars4 = new SqlParameter[2];
                    spars4[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                    spars4[1] = new SqlParameter("@Asset_number", SqlDbType.NVarChar);

                    spars4[0].Value = "getSoftware_listdective";
                    spars4[1].Value = asset;

                    // Call the method to insert data
                    spm.Insert_Data(spars4, "SP_Admin_AssetInventory");
                }
            }


            foreach (GridViewRow row in DgvApprover.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string sSoftId = Convert.ToString(DgvApprover.DataKeys[row.RowIndex].Values[0]).Trim();
                    SqlParameter[] spars3 = new SqlParameter[2];
                    spars3[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                    spars3[1] = new SqlParameter("@soft_id", SqlDbType.Int);

                    spars3[0].Value = "getSoftware_listadd";
                    spars3[1].Value = Convert.ToInt32(sSoftId);

                    spm.Insert_Data(spars3, "SP_Admin_AssetInventory");
                }
            }

            string AssigneTo = txtEmpCode.Text.ToString().Trim();
            spm.InsertITAssetServiceRequestDetails(Convert.ToString(Session["Empcode"]).Trim(), Remarks, AppRemarks, "UpdateWF", AssigneTo, isAssigned, 5, 1, Convert.ToString(Session["Empcode"]).Trim(), Convert.ToInt32(hdnId.Value));
            //Get All Custodians-
            string to_email = "";
            DataTable t2 = spm.GetAllCustodianDetails();
            if (t2.Rows.Count > 0)
            {
                to_email = "";

                foreach (DataRow row in t2.Rows)
                {
                    to_email = to_email + Convert.ToString(row["email"].ToString()) + ";";
                }
            }
            SendNotificationMailToCustodians(EmpName, AssetAlloReqNo, EmpCode, Doj, Location, Dept, Designation, RMgr, HOD, "", to_email, "Reject", "");
            lblmessage.Text = "IT Asset Service Request Rejected Successfully";
            Response.Redirect("~/procs/ITAssetService.aspx");
        }
        catch (Exception ex)
        {
            lblmessage.Visible = true;
            lblmessage.Text = ex.Message.ToString();
            return;
        }
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        string[] strdate;
        string strfromDate = "";
        string filename = "";
        String strfileName = "";
        string AppRemarks = "";
        int isAssigned = Convert.ToInt32(ddlAssignment.SelectedValue);
        ///collect data for Email
        var EmpName = txtEmpName.Text.ToString().Trim();
        var AssetAlloReqNo = txtAssetAlloReqNo.Text.ToString().Trim();
        var EmpCode = txtEmpCode.Text.ToString().Trim();
        var Doj = txtDoj.Text.ToString().Trim();
        var Location = txtEmpLocation.Text.ToString().Trim();
        var Dept = txtEmpDept.Text.ToString().Trim();
        var Designation = txtEmpDesig.Text.ToString().Trim();
        var RMgr = txtRMgr.Text.ToString().Trim();
        var HOD = txtHOD.Text.ToString().Trim();
        var ReqDate = txtReqDate.Text.ToString().Trim();
        var emailTo = txtEmail.Text.ToString().Trim();
        var Remarks = txtCustRemarks.Text.ToString().Trim();
        try
        {
            if (Convert.ToString(txtRemarks.Text).Trim() != "")
            {
                AppRemarks = txtRemarks.Text.ToString().Trim();
            }
            if (uploadfile.HasFile)
            {
                filename = uploadfile.FileName;
            }

            #region insert or upload multiple files
            if (uploadfile.HasFile)
            {
                filename = uploadfile.FileName;
                if (Convert.ToString(filename).Trim() != "")
                {
                    #region date formatting
                    if (Convert.ToString(txtReqDate.Text).Trim() != "")
                    {
                        strdate = Convert.ToString(txtReqDate.Text).Trim().Split('/');
                        strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
                    }
                    #endregion

                    string AssetInventoryPath = "";
                    //if (Convert.ToString(hdnRemid.Value).Trim() != "0")
                    //    FuelclaimPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PaymentVoucherDocumentpath"]).Trim());
                    //else
                    AssetInventoryPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["AssetInventoryPath"]).Trim());

                    bool folderExists = Directory.Exists(AssetInventoryPath);
                    if (!folderExists)
                    {
                        Directory.CreateDirectory(AssetInventoryPath);
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
                            strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + Convert.ToString(i + 1).Trim() + uploadfileName.FileName.ToString();
                            filename = strfileName;
                            uploadfileName.SaveAs(Path.Combine(AssetInventoryPath, strfileName));

                            //spm.InsertpaymentUploaded_Files(Convert.ToDecimal(hdnRemid.Value), blnfile, Convert.ToString(strfileName).Trim(), "paymentVoucher_insertTmp", i + 1, Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToInt32(hdnclaimqry.Value));
                            spm.InserITAssetRequestFiles(txtAssetAlloReqNo.Text.ToString().Trim(), txtEmpCode.Text.ToString().Trim(), hdnEmpCode.Value, Convert.ToString(strfileName).Trim());
                            blnfile = true;
                        }
                    }

                }
            }
            #endregion
            string AssigneTo = hdnCustodianCode.Value;
            spm.InsertITAssetServiceRequestDetails(Convert.ToString(Session["Empcode"]).Trim(), Remarks, AppRemarks, "UpdateWF", AssigneTo, isAssigned, 3, 1, Convert.ToString(Session["Empcode"]).Trim(), Convert.ToInt32(hdnId.Value));
            //Get All Custodians-
            string to_email = "";
            DataTable t2 = spm.GetAllCustodianDetails();
            if (t2.Rows.Count > 0)
            {
                to_email = "";

                foreach (DataRow row in t2.Rows)
                {
                    to_email = to_email + Convert.ToString(row["email"].ToString()) + ";";
                }
            }

            string ITAssetService_Req = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ITAssetService_Req.aspx";
            string redirectURL = Convert.ToString(ITAssetService_Req).Trim() + "?id=" + Convert.ToInt32(hdnId.Value) + "&type=emp";

            SendNotificationMailToCustodians(EmpName, AssetAlloReqNo, EmpCode, Doj, Location, Dept, Designation, RMgr, HOD, "", to_email, "SentBack", redirectURL);
            lblmessage.Visible = true;
            lblmessage.Text = "IT Asset Service Request Sent Back Successfully";
            Response.Redirect("~/procs/ITAssetService.aspx");
        }
        catch (Exception ex)
        {
            lblmessage.Visible = true;
            Response.Redirect("~/procs/ITAssetService.aspx");
            return;
        }
    }

    protected void ddlAssignment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAssignment.SelectedValue == "1")
        {
            gvAssetOption.Visible = true;
            lblAssetOptions.Visible = true;
        }
        else
        {
            gvAssetOption.Visible = false;
            lblAssetOptions.Visible = false;
        }
    }

    protected void gvAssetOption_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "edititem")
        {
        }
    }

    protected void gvAssetOption_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAssetOption.PageIndex = e.NewPageIndex;
    }

    protected void SendNotificationMailToCustodians(string EmpName, string AssetAlloReqNo, string EmpCode, string Doj, string Location, string Dept, string Designation, string RMgr, string HOD, string AttachmentFileName, string to_email, string ReqType, string redirectURL)
    {

        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient();
        StringBuilder strbuild = new StringBuilder();
        var strsubject = "";
        if (ReqType == "Approve")
        {
            strsubject = "New Joinee Asset Allocation Request Approval- " + EmpName;
        }
        if (ReqType == "Reject")
        {
            strsubject = "New Joinee Asset Allocation Request Rejection- " + EmpName;
        }
        if (ReqType == "SentBack")
        {
            strsubject = "New Joinee Asset Allocation Request Sent Back- " + EmpName;
        }

        try
        {
            strbuild = new StringBuilder();

            strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;width:100%;table-layout:fixed;'><tr><td>   ");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td>Dear Sir/Madam,</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("</td></tr>");

            if (ReqType == "Approve")
            {
                strbuild.Append("<tr><td width = '25%'>For Following Employee Asset Allocation Request has been approved by HOD.</td ></ tr>");
            }
            if (ReqType == "Reject")
            {
                strbuild.Append("<tr><td width = '25%'>For Following Employee Asset Allocation Request has been rejected by HOD.</ td ></ tr>");
            }
            if (ReqType == "SentBack")
            {
                strbuild.Append("<tr><td width = '25%'>For Following Employee Asset Allocation Request has been Sent Back For Correction by HOD.</td ></ tr>");
            }

            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td>");
            strbuild.Append("<table style='width:80%;color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;table-layout:fixed;'>");
            strbuild.Append("<tr><td width='25%'>Asset Allocation Request : </td><td >" + AssetAlloReqNo + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Employee Code : </td><td >" + EmpCode + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Employee Name : </td><td >" + EmpName + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Onboarding Date : </td><td width='75%'>" + Doj + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Location : </td><td width='75%'> " + Location + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Department :</td><td width='75%'>" + Dept + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Designation :</td><td width='75%'>" + Designation + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Reporting Manager :</td><td width='75%'>" + RMgr + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>HOD :</td><td width='75%'>" + HOD + "</td></tr>");
            strbuild.Append("</table>");
            strbuild.Append("</td></tr>");
            strbuild.Append("</td></tr>");

            if (ReqType == "SentBack")
            {
                strbuild.Append("<tr><td style='height:20px'></td></tr>");
                strbuild.Append("<tr><td style='height:20px'><a href='" + redirectURL + "' target='_blank'> Click here to take action</td></tr>");
                strbuild.Append("<tr><td style='height:20px'></td></tr>");
            }
            strbuild.Append("<tr><td style='height:20px'>This is a system generated mail, Please do not reply.</td></tr>");
            strbuild.Append("</table>");

            sendMail(to_email, "", "", strsubject, Convert.ToString(strbuild).Trim(), "", "");
            

        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void SendNotificationMailToNewJoinee(string EmpName, string AssetAlloReqNo, string EmpCode, string Doj, string Location, string Dept, string Designation, string RMgr, string HOD, string AttachmentFileName, string redirectURL, string emailTo)
    {

        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient();
        StringBuilder strbuild = new StringBuilder();

        var strsubject = "New Joinee Asset Allocation Request Approval- " + EmpName;

        try
        {
            strbuild = new StringBuilder();

            strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;width:100%;table-layout:fixed;'><tr><td>   ");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td>Dear Sir/Madam,</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td width = '25%'> For following Employee Asset Allocation Request has been approved by HOD.</td ></ tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td>");
            strbuild.Append("<table style='width:80%;color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;table-layout:fixed;'>");
            strbuild.Append("<tr><td width='25%'>Asset Allocation Request : </td><td >" + AssetAlloReqNo + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Employee Code : </td><td >" + EmpCode + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Employee Name : </td><td >" + EmpName + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Onboarding Date : </td><td width='75%'>" + Doj + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Location : </td><td width='75%'> " + Location + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Department :</td><td width='75%'>" + Dept + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Designation :</td><td width='75%'>" + Designation + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Reporting Manager :</td><td width='75%'>" + RMgr + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>HOD :</td><td width='75%'>" + HOD + "</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td style='height:20px'><a href='" + redirectURL + "' target='_blank'> Click here to take action</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td style='height:20px'>This is a system generated mail, Please do not reply.</td></tr>");
            strbuild.Append("</table>");

            
            sendMail(emailTo, "", "", strsubject, Convert.ToString(strbuild).Trim(), "", "");
            
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void SendAssetAlocationMailToNewJoinee(string EmpName, string AssetType, string AssetDesc, string SrNo, string Manufacturer, string Model, string emailTo, string redirectURL)
    {

        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient();
        StringBuilder strbuild = new StringBuilder();

        var strsubject = "New Asset Allocated – Accept Terms And Conditions.";

        try
        {
            strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;width:100%;table-layout:fixed;'><tr><td>   ");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td></td></tr>");
            strbuild.Append("<tr><td>Dear Sir/Madam,</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td >Following IT Asset has been allocated to you, please accept terms and condition for the same.</td ></ tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td>");
            strbuild.Append("<table style='width:80%;color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;table-layout:fixed;'>");
            strbuild.Append("<tr><td>Asset Type: </td><td >" + AssetType + "</td></tr>");
            strbuild.Append("<tr><td>Asset Description: </td><td >" + AssetDesc + "</td></tr>");
            strbuild.Append("<tr><td>Sr.No: </td><td >" + SrNo + "</td></tr>");
            strbuild.Append("<tr><td>Manufacturer/Brand: </td><td width='75%'>" + Manufacturer + "</td></tr>");
            strbuild.Append("<tr><td>Model: </td><td width='75%'> " + Model + "</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td><a href='" + redirectURL + "' target='_blank'>Click here to view and accept Terms & Conditions</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td></td></tr>");
            strbuild.Append("<tr><td>Note: -After 5 working days, terms and condition will be deemed accepted.</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td>This is a system generated mail, Please do not reply.</td></tr>");
            strbuild.Append("</table>");

            sendMail(emailTo, "", "", strsubject, Convert.ToString(strbuild).Trim(), "", "");
            
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void sendMail(string emailTo, string emailCc1, string emailCc2, string strsubject, string strbody, string strattchfilepath, string ccMailIDs)
    {
        try
        {
            MailMessage mail = new MailMessage();

            //Prod
            if (emailTo.Contains(";"))
            {
                string[] strtoEmail = Convert.ToString(emailTo).Trim().Split(';');
                for (int i = 0; i < strtoEmail.Length; i++)
                {
                    if (Convert.ToString(strtoEmail[i]).Trim() != "")
                        mail.To.Add(strtoEmail[i]);
                }
            }
            else
            {
                mail.To.Add(emailTo);
            }

            //Dev
            //emailTo = "Manisha.tambade@highbartech.com";
            //mail.CC.Add("Raj.patel@highbartech.com");
            //mail.To.Add(emailTo);

            StringBuilder strsignature = new StringBuilder();
            mail.From = new MailAddress("noreply@highbartech.com");
            mail.Subject = strsubject;
            mail.Body = Convert.ToString(strbody) + Convert.ToString(strsignature);
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Low;

            if (Convert.ToString(strattchfilepath).Trim() != "")
            {
                Attachment attch = new Attachment(strattchfilepath);
                mail.Attachments.Add(attch);
            }

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = "smtp.office365.com"; //Highbar SMTP

                smtp.Port = 587;
                smtp.TargetName = "STARTTLS/smtp.office365.com";
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential("noreply@highbartech.com", "HBT@2019");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = SMTPUserInfo;
                smtp.EnableSsl = true;
                System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;
                smtp.Send(mail);
            }

            mail.Dispose();
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void SendAssetAssignmentMail(string AssetType, string AssetDesc, string SrNo, string Manufacturer, string Model, string emailTo, string HodEmailcc, string Emailcc1, string Emailcc2)
    {
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient();
        StringBuilder strbuild = new StringBuilder();
        DataTable dt_emp = new DataTable();


        var strsubject = "Assignment Of IT Asset";
        try
        {
            strbuild = new StringBuilder();

            strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;width:100%;table-layout:fixed;'><tr><td>   ");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td>Dear Sir/Madam,</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td width = '25%'> Following IT asset has been assigned to you.</td ></ tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td>");
            strbuild.Append("<table style='width:80%;color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;table-layout:fixed;'>");
            strbuild.Append("<tr><td width='25%'>Asset Type: </td><td >" + AssetType + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Asset Description: </td><td >" + AssetDesc + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Sr.No: </td><td >" + SrNo + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Manufacturer/Brand: </td><td width='75%'>" + Manufacturer + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Model: </td><td width='75%'> " + Model + "</td></tr>");
            strbuild.Append("<tr><td></td></tr>");
            strbuild.Append("</table>");
            strbuild.Append("</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td> For any discrepancy, Please contact IT department.</td ></ tr>");
            strbuild.Append("<tr><td style='height:20px'><b>Note :- This is a system generated email, Please do not reply.</b></td></tr>");
            strbuild.Append("</table>");
            //commented for unnecessory email shoot--
            sendMail(emailTo, HodEmailcc, Emailcc1, Emailcc2, strsubject, Convert.ToString(strbuild).Trim(), "");

        }
        catch (Exception)
        {

            throw;
        }
    }


    protected void SendAssetAssignmentMail(string AssetType, string AssetDesc, string SrNo, string Manufacturer, string Model, string emailTo, string HodEmailcc, string cc_email, string RM, string PRM, string HR)
    {
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient();
        StringBuilder strbuild = new StringBuilder();
        DataTable dt_emp = new DataTable();

        var strsubject = "Assignment Of IT Asset";
        try
        {
            strbuild = new StringBuilder();

            strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;width:100%;table-layout:fixed;'><tr><td>   ");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td>Dear Sir/Madam,</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td width = '25%'> Following IT asset has been assigned to you.</td ></ tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td>");
            strbuild.Append("<table style='width:80%;color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;table-layout:fixed;'>");
            strbuild.Append("<tr><td width='25%'>Asset Type: </td><td >" + AssetType + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Asset Description: </td><td >" + AssetDesc + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Sr.No: </td><td >" + SrNo + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Manufacturer/Brand: </td><td width='75%'>" + Manufacturer + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Model: </td><td width='75%'> " + Model + "</td></tr>");
            strbuild.Append("<tr><td></td></tr>");
            strbuild.Append("</table>");
            strbuild.Append("</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td> For any discrepancy, Please contact IT department.</td ></ tr>");
            strbuild.Append("<tr><td style='height:20px'><b>Note :- This is a system generated email, Please do not reply.</b></td></tr>");
            strbuild.Append("</table>");
            //commented for unnecessory email shoot--
            sendMailAssetallocation(emailTo, HodEmailcc, cc_email, strsubject, Convert.ToString(strbuild).Trim(), "", RM, PRM, HR);

        }
        catch (Exception)
        {

            throw;
        }
    }

     public void sendMailAssetallocation(string emailTo, string HodEmailcc, string cc_email, string strsubject, string strbody, string strattchfilepath, string RM, string PRM, string HR)
    {
        try
        {
            MailMessage mail = new MailMessage();

            //Prod
            mail.To.Add(emailTo);
            string[] strCCEmail = Convert.ToString(cc_email).Trim().Split(';');

            mail.CC.Add(HodEmailcc);
            mail.CC.Add(RM);
            mail.CC.Add(PRM);
            mail.CC.Add(HR);

            //Dev
            //emailTo = "Manisha.tambade@highbartech.com";
            //mail.CC.Add("Raj.patel@highbartech.com");
            //mail.To.Add(emailTo);

            //old
            //for (int i = 0; i < strCCEmail.Length; i++)
            //{
            //    if (Convert.ToString(strCCEmail[i]).Trim() != "")
            //        mail.CC.Add(strCCEmail[i]);
            //}

            StringBuilder strsignature = new StringBuilder();
            mail.From = new MailAddress("noreply@highbartech.com");
            mail.Subject = strsubject;
            mail.Body = Convert.ToString(strbody) + Convert.ToString(strsignature);
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Low;

            //if (Convert.ToString(strattchfilepath).Trim() != "")
            //{
            //    Attachment attch = new Attachment(strattchfilepath);
            //    mail.Attachments.Add(attch);
            //}

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = "smtp.office365.com"; //Highbar SMTP

                smtp.Port = 587;
                smtp.TargetName = "STARTTLS/smtp.office365.com";
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential("noreply@highbartech.com", "HBT@2019");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = SMTPUserInfo;
                smtp.EnableSsl = true;
                System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;
                smtp.Send(mail);
            }
            mail.Dispose();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void SearchAsset()
    {
        
            SqlParameter[] spars2 = new SqlParameter[2];
            
            spars2[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars2[0].Value = "getSoftware_list_maintable";
        foreach (GridViewRow assetRow in gvAssetOption.Rows)
        {

            spars2[1] = new SqlParameter("@Asset_number", SqlDbType.NVarChar);
            spars2[1].Value = Convert.ToString(assetRow.Cells[1].Text).Trim();

            DataTable dssoftwareList = spm.getDataList(spars2, "SP_Admin_AssetInventory");

            if (dssoftwareList.Rows.Count > 0)
            {
                DgvApprover.DataSource = dssoftwareList;
                DgvApprover.DataBind();
            }

        }
    }

        protected void DgvApprover_softlist(object sender, EventArgs e)
    {
        SearchAsset();

    }

}