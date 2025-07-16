using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class procs_VSCB_VendorCreate : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();

    #region Creative_Default_methods


    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    public string StrEmpCode="";
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
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

            StrEmpCode = Convert.ToString(Session["Empcode"]).Trim();
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/vscb_index.aspx");
            }
            else
            {
                Page.SmartNavigation = true;
                FilePath.Value = Convert.ToString(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBVendorFiles"]).Trim() + "/"));
                if (!Page.IsPostBack)
                {
                    txtAccountNo.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);"); 
                    //txtMilestone.Attributes.Add("maxlength", txtMilestone.MaxLength.ToString());
                    //txt_remakrs.Attributes.Add("maxlength", txt_remakrs.MaxLength.ToString());
                    //txtMilestoneDueDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    if (Request.QueryString.Count > 0)
                    {
                        hdVendorID.Value = Request.QueryString[0];
                        VendorEditItem();
                    }
                }

            }

        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    protected void trvl_btnSave_Click(object sender, EventArgs e)
    {
        if (txt_VendorName.Enabled == false)
        {
            if (txtEmailAddress.Text.Trim() == "")
            {
                lblmessage.Text = "Enter Email Address";
                return;
            }
            else
            {                                                                    
                System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$");
                if (txtEmailAddress.Text.Trim() != "")
                {
                    if (!rEMail.IsMatch(txtEmailAddress.Text.Trim()))
                    {
                        lblmessage.Text = "Enter valid Email Address";
                        return;
                    }
                }
            }
            DataSet DS = new DataSet();
            SqlParameter[] spars1 = new SqlParameter[4];

            spars1[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars1[0].Value = "vendoUpdatePOWOuser";
            
            spars1[1] = new SqlParameter("@VendorId", SqlDbType.VarChar);
            spars1[1].Value = hdVendorID.Value;

            spars1[2] = new SqlParameter("@VendorEmailAddress", SqlDbType.VarChar);
            spars1[2].Value = txtEmailAddress.Text.Trim();

            spars1[3] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars1[3].Value = Convert.ToString(Session["Empcode"]);

            DS = spm.getDatasetList(spars1, "SP_VSCB_GETALL_DETAILS");

        }
        else
        {
            #region validations

            if (txt_VendorName.Text.Trim() == "")
            {
                lblmessage.Text = "Enter Vendor Name";
                return;
            }
            if (txtVendorCode.Text.Trim() == "")
            {
                lblmessage.Text = "Enter Vendor Code";
                return;
            }
            if (txtAccountNo.Text.Trim() == "")
            {
                lblmessage.Text = "Enter Account Number";
                return;
            }
            if (txtIFSCCode.Text.Trim() == "")
            {
                lblmessage.Text = "Enter IFSC Code";
                return;
            }
            if (TxtBankName.Text.Trim() == "")
            {
                lblmessage.Text = "Enter Bank Name";
                return;
            }
            if (TxtVendorAddress.Text.Trim() == "")
            {
                lblmessage.Text = "Enter Vendor Address";
                return;
            }
            if (hdVendorID.Value == "")
            {
                if (VendorUploadfile.HasFile)
                {
                    var supportedTypes = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".JPG", ".JPEG", ".PNG", ".BMP", ".pdf" };
                    string extension = System.IO.Path.GetExtension(VendorUploadfile.FileName);
                    if (!supportedTypes.Contains(extension))
                    {
                        lblmessage.Text = "File Extension Is InValid - Only Upload Image & pdf";
                        return;
                    }
                }
                else
                {
                    lblmessage.Text = "Please upload file.";
                    return;
                }
            }
            if (txtEmailAddress.Text.Trim() != "")
            {
                string email = txtEmailAddress.Text.Trim();
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                if (match.Success)
                {

                }
                else
                {
                    lblmessage.Text = "Enter Vendor Correct Email Address";
                    return;
                }
            }
            else
            {
                lblmessage.Text = "Enter Vendor Email Address";
                return;
            }

            #endregion

            if (hdVendorID.Value == "")
            {

                DataSet dsDuplicateCheck = new DataSet();
                SqlParameter[] sparss = new SqlParameter[3];

                sparss[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                sparss[0].Value = "DuplicateCheck_Vendor";

                sparss[1] = new SqlParameter("@VendorCode", SqlDbType.VarChar);
                sparss[1].Value = txtVendorCode.Text.Trim();

                dsDuplicateCheck = spm.getDatasetList(sparss, "SP_VSCB_GETALL_DETAILS");

                if (dsDuplicateCheck.Tables[0].Rows.Count > 0)
                {
                    lblmessage.Text = "Vendor Code Already Exists";
                    return;
                }

                DataSet dsApprovedPOWO = new DataSet();
                SqlParameter[] spars = new SqlParameter[13];

                spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                spars[0].Value = "Create_vendorAdd";
                spars[1] = new SqlParameter("@VendorName", SqlDbType.VarChar);
                spars[1].Value = txt_VendorName.Text.Trim();
                spars[2] = new SqlParameter("@VendorCode", SqlDbType.VarChar);
                spars[2].Value = txtVendorCode.Text.Trim();
                spars[3] = new SqlParameter("@GSTIN_NO", SqlDbType.VarChar);
                spars[3].Value = txtGSTINNo.Text.Trim();
                spars[4] = new SqlParameter("@Acc_no", SqlDbType.VarChar);
                spars[4].Value = txtAccountNo.Text.Trim();
                spars[5] = new SqlParameter("@IFSC_Code", SqlDbType.VarChar);
                spars[5].Value = txtIFSCCode.Text.Trim();
                spars[6] = new SqlParameter("@BankName", SqlDbType.VarChar);
                spars[6].Value = TxtBankName.Text.Trim();
                spars[7] = new SqlParameter("@vendor_address", SqlDbType.VarChar);
                spars[7].Value = TxtVendorAddress.Text.Trim();
                spars[8] = new SqlParameter("@empcode", SqlDbType.VarChar);
                spars[8].Value = Convert.ToString(Session["Empcode"]);
                spars[9] = new SqlParameter("@Isactive", SqlDbType.VarChar);
                spars[9].Value = chkISActive.Checked;
                spars[10] = new SqlParameter("@VendorEmailAddress", SqlDbType.VarChar);
                spars[10].Value = txtEmailAddress.Text.Trim();


                #region insert files

                string StrFileName = "";

                if (VendorUploadfile.HasFile)
                {
                    string filename = VendorUploadfile.FileName;
                    string vendorFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBVendorFiles"]).Trim() + "/");
                    bool folderExists = Directory.Exists(vendorFilePath);
                    if (!folderExists)
                    {
                        Directory.CreateDirectory(vendorFilePath);
                    }
                    String InputFile = System.IO.Path.GetExtension(VendorUploadfile.FileName);
                    //filename = txtEmpCode.Text + "_" + Convert.ToString(dMaxReqId).Trim() + "_Invoice_PO" + InputFile;
                    string sVendorCode = Regex.Replace(Convert.ToString(txtVendorCode.Text.Trim()), @"[^0-9a-zA-Z\._]", "_");
                    //  filename = sVendorCode + "_" + strInvoiceDate_FileName + InputFile;
                    filename = sVendorCode + InputFile;
                    VendorUploadfile.SaveAs(Path.Combine(vendorFilePath, filename));
                    StrFileName = Convert.ToString(filename).Trim();
                }
                #endregion

                spars[11] = new SqlParameter("@VendorFile", SqlDbType.VarChar);
                spars[11].Value = StrFileName.Trim();

                dsApprovedPOWO = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");

            }
            else
            {

                DataSet dsDuplicateCheck = new DataSet();
                SqlParameter[] sparss = new SqlParameter[3];

                sparss[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                sparss[0].Value = "DuplicateCheck_Vendor";

                sparss[1] = new SqlParameter("@VendorCode", SqlDbType.VarChar);
                sparss[1].Value = txtVendorCode.Text.Trim();

                dsDuplicateCheck = spm.getDatasetList(sparss, "SP_VSCB_GETALL_DETAILS");

                if (dsDuplicateCheck.Tables[0].Rows.Count > 0)
                {
                    if (HDVendorCode.Value.Trim() == txtVendorCode.Text.Trim())
                    {
                    }
                    else
                    {
                        lblmessage.Text = "Vendor Code Already Exists";
                        return;
                    }
                }

                DataSet dsApprovedPOWO = new DataSet();
                SqlParameter[] spars = new SqlParameter[13];

                spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                spars[0].Value = "vendoUpdate";
                spars[1] = new SqlParameter("@VendorName", SqlDbType.VarChar);
                spars[1].Value = txt_VendorName.Text.Trim();
                spars[2] = new SqlParameter("@VendorCode", SqlDbType.VarChar);
                spars[2].Value = txtVendorCode.Text.Trim();
                spars[3] = new SqlParameter("@GSTIN_NO", SqlDbType.VarChar);
                spars[3].Value = txtGSTINNo.Text.Trim();
                spars[4] = new SqlParameter("@Acc_no", SqlDbType.VarChar);
                spars[4].Value = txtAccountNo.Text.Trim();
                spars[5] = new SqlParameter("@IFSC_Code", SqlDbType.VarChar);
                spars[5].Value = txtIFSCCode.Text.Trim();
                spars[6] = new SqlParameter("@BankName", SqlDbType.VarChar);
                spars[6].Value = TxtBankName.Text.Trim();
                spars[7] = new SqlParameter("@vendor_address", SqlDbType.VarChar);
                spars[7].Value = TxtVendorAddress.Text.Trim();
                spars[8] = new SqlParameter("@empcode", SqlDbType.VarChar);
                spars[8].Value = Convert.ToString(Session["Empcode"]);
                spars[9] = new SqlParameter("@VendorId", SqlDbType.VarChar);
                spars[9].Value = hdVendorID.Value;
                spars[10] = new SqlParameter("@Isactive", SqlDbType.VarChar);
                spars[10].Value = chkISActive.Checked;
                spars[11] = new SqlParameter("@VendorEmailAddress", SqlDbType.VarChar);
                spars[11].Value = txtEmailAddress.Text.Trim();

                #region insert files

                string StrFileName = "";

                if (VendorUploadfile.HasFile)
                {
                    var supportedTypes = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".JPG", ".JPEG", ".PNG", ".BMP", ".pdf" };
                    string extension = System.IO.Path.GetExtension(VendorUploadfile.FileName);
                    if (!supportedTypes.Contains(extension))
                    {
                        lblmessage.Text = "File Extension Is InValid - Only Upload Image & pdf";
                        return;
                    }

                    string filename = VendorUploadfile.FileName;
                    string vendorFilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBVendorFiles"]).Trim() + "/");
                    bool folderExists = Directory.Exists(vendorFilePath);
                    if (!folderExists)
                    {
                        Directory.CreateDirectory(vendorFilePath);
                    }
                    String InputFile = System.IO.Path.GetExtension(VendorUploadfile.FileName);
                    string sVendorCode = Regex.Replace(Convert.ToString(txtVendorCode.Text.Trim()), @"[^0-9a-zA-Z\._]", "_");
                    //  filename = sVendorCode + "_" + strInvoiceDate_FileName + InputFile;
                    filename = sVendorCode + InputFile;
                    VendorUploadfile.SaveAs(Path.Combine(vendorFilePath, filename));
                    StrFileName = Convert.ToString(filename).Trim();
                    spars[12] = new SqlParameter("@VendorFile", SqlDbType.VarChar);
                    spars[12].Value = StrFileName.Trim();
                }
                else
                {
                    spars[12] = new SqlParameter("@VendorFile", SqlDbType.VarChar);
                    spars[12].Value = lnkfile_Vendor.Text.Trim();
                }
                #endregion

                dsApprovedPOWO = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
            }
            lblmessage.Text = "";
        }

        Response.Redirect("VSCB_VendorList.aspx");
    }

    #endregion

    #region PageMethods

    public void VendorEditItem()
    {
        DataSet dsVendorEdit = new DataSet();
        SqlParameter[] sparss = new SqlParameter[3];

        sparss[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        sparss[0].Value = "EditItem_Vendor";

        sparss[1] = new SqlParameter("@VendorId", SqlDbType.VarChar);
        sparss[1].Value = hdVendorID.Value;

        sparss[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        sparss[2].Value = StrEmpCode;

        dsVendorEdit = spm.getDatasetList(sparss, "SP_VSCB_GETALL_DETAILS");
        if (dsVendorEdit.Tables[0].Rows.Count > 0)
        {
            txt_VendorName.Text = dsVendorEdit.Tables[0].Rows[0]["Name"].ToString();
            txtVendorCode.Text = dsVendorEdit.Tables[0].Rows[0]["VendorCode"].ToString();
            HDVendorCode.Value = dsVendorEdit.Tables[0].Rows[0]["VendorCode"].ToString();
            txtGSTINNo.Text = dsVendorEdit.Tables[0].Rows[0]["GSTIN_NO"].ToString();
            txtAccountNo.Text = dsVendorEdit.Tables[0].Rows[0]["Acc_no"].ToString();
            txtIFSCCode.Text = dsVendorEdit.Tables[0].Rows[0]["IFSC_Code"].ToString();
            TxtBankName.Text = dsVendorEdit.Tables[0].Rows[0]["BankName"].ToString();
            TxtVendorAddress.Text = dsVendorEdit.Tables[0].Rows[0]["vendor_address"].ToString();
            chkISActive.Checked = Convert.ToBoolean(dsVendorEdit.Tables[0].Rows[0]["Isactive"]);
            txtEmailAddress.Text = dsVendorEdit.Tables[0].Rows[0]["VendorEmailAddress"].ToString();
            lnkfile_Vendor.Text = Convert.ToString(dsVendorEdit.Tables[0].Rows[0]["VendorFile"]).Trim();
            lnkfile_Vendor.Visible = true;
        }
        if (dsVendorEdit.Tables[1].Rows.Count == 0)
        {
            txt_VendorName.Enabled = false;
            txtVendorCode.Enabled = false;
            txtGSTINNo.Enabled = false;
            txtAccountNo.Enabled = false;
            txtIFSCCode.Enabled = false;
            TxtBankName.Enabled = false;
            TxtVendorAddress.Enabled = false;
            chkISActive.Enabled = false;
            lnkfile_Vendor.Visible = false;
            VendorUploadfile.Visible = false;
            uploadfileid.Visible = false;
            uploadfileidd.Visible = false;
        }
    }

    #endregion

    protected void lnkfile_Vendor_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["VSCBVendorFiles"]).Trim()),lnkfile_Vendor.Text);
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

    protected void btnback_mng_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/VSCB_VendorList.aspx");

    }


}