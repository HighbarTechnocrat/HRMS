
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Nominations : System.Web.UI.Page
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
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
                //Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                Page.SmartNavigation = true;
                lblmessage.Text = "";

                if (!Page.IsPostBack)
                {

                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["NominationsDocumentpath"]).Trim());
                    Session["Nominations"] = "YES";

                    editform.Visible = true;
                    divbtn.Visible = false;
                    divmsg.Visible = false;
                    lblmessage.Visible = true;
                    txtEmpCode.Text = Convert.ToString(Session["Empcode"]);

                    hdnTravelConditionid.Value = "4";

                    fuel_btncancel.Visible = false;
                    txtFromdateMain.Text = DateTime.Today.ToString("dd/MM/yyyy").Replace("-", "/");
                    txtFromdateMain.Enabled = false;
                    AssigningSessions();
                    txtFromdateMain.Text = Convert.ToString(Session["Fromdate"]);
                    if (Convert.ToString(hdnRemid.Value).Trim() == "")
                    {
                        hdnRemid.Value = "0";
                    }
                    txtFromdateMain.Enabled = true;
                    GetEmployeeDetails();
                    get_employee_Medical_nominationsList();
                    CheckIs_SubmitMediclaimData_Access();

                    //fuel_btnSave.Visible = false;
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
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
        string pf_sex = "";
        if (Convert.ToString(txtFromdateMain.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Submission Date";
            return;
        }
        AssigningSessions();

        string[] strdate;
        string strfromDate = "";

        if (Convert.ToString(txtBirthdatedate_PF.Text).Trim() == "")
        {
            lblmessage.Text = "Birtdate Date cannot be blank!";
            return;
        }

        if (Convert.ToString(txtNominee.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Nominee Name!";
            return;
        }

        if (Convert.ToString(txtRelationPF.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Relation with Employee.";
            return;
        }

        if (Convert.ToString(txtAge_PF.Text).Trim() == "" || Convert.ToString(txtAge_PF.Text).Trim() == "0")
        {
            lblmessage.Text = "Birtdate Date cannot be blank!";
            return;
        }

        if (PF_male.Checked)
        {
            pf_sex = "Male";
        }
        else
        {
            pf_sex = "Female";
        }
        if (pf_sex == "")
        {
            lblmessage.Text = "Please Select Gender!";
            return;
        }

        if (Convert.ToString(txtBirthdatedate_PF.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtBirthdatedate_PF.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        hdnsptype.Value = "InsertTempTable";
        if (Convert.ToString(hdnClaimsID.Value).Trim() == "")
        {
            hdnClaimsID.Value = "0";
        }
        if (Convert.ToString(hdnClaimsID.Value).Trim() != "" && Convert.ToString(hdnClaimsID.Value).Trim() != "0")
            hdnsptype.Value = "updateTempTable";

        spm.Insert_Nominations_PF(Convert.ToInt32(hdnRemid.Value), strfromDate, txtEmpCode.Text, txtNominee.Text.ToString().Trim(), txtRelationPF.Text.ToString().Trim(), Convert.ToDecimal(txtAge_PF.Text), pf_sex, Convert.ToString(hdnsptype.Value), Convert.ToInt32(hdnClaimsID.Value));
        if (Convert.ToString(Session["Birthdatedate_PF"]).Trim() == "")
        {
            Session["Birthdatedate_PF"] = Convert.ToString(txtBirthdatedate_PF.Text).Trim();
        }
        //Response.Redirect("~/procs/Fuel_Req.aspx?clmid=" + hdnClaimsID.Value + "&rem_id=" + hdnremid.Value);
        get_NominationsDetails();
        txtBirthdatedate_PF.Text = "";
        txtAge_PF.Text = "";
        txtNominee.Text = "";
        txtRelationPF.Text = "";
        // get_emp_Nominations_File();
        hdnClaimsID.Value = "0";
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
    }

    private void AddUpdate_MediclaimData(string sNomiId, string MedNom_id, string sRelation, string MemberName, string sBirthDate, string sAge, string sGender)
    {

        string[] strdate;
        if (Convert.ToString(sBirthDate).Trim() != "")
        {
            strdate = Convert.ToString(sBirthDate).Trim().Split('/');
            sBirthDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        string medical_type = "Update";
        if (Convert.ToString(hdnNominiId.Value).Trim() == "0")
            medical_type = "Add";

        DataSet dsgoal = new DataSet();
        SqlParameter[] spars = new SqlParameter[10];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "insert_update_employee_mediclaim_details";

        spars[1] = new SqlParameter("@Nom_id", SqlDbType.Decimal);
        spars[1].Value = Convert.ToDouble(sNomiId);

        spars[2] = new SqlParameter("@MedNom_id", SqlDbType.Decimal);
        spars[2].Value = Convert.ToDouble(MedNom_id);

        spars[3] = new SqlParameter("@BirthDate", SqlDbType.VarChar);
        spars[3].Value = Convert.ToString(sBirthDate).Trim();

        spars[4] = new SqlParameter("@MemberName", SqlDbType.VarChar);
        spars[4].Value = Convert.ToString(MemberName).Trim();

        spars[5] = new SqlParameter("@Member_Age", SqlDbType.Decimal);
        if (Convert.ToString(sAge).Trim() != "")
            spars[5].Value = Convert.ToDouble(sAge);
        else
            spars[5].Value = Convert.ToDouble("0");

        spars[6] = new SqlParameter("@Member_Rel", SqlDbType.VarChar);
        spars[6].Value = Convert.ToString(sRelation).Trim();

        spars[7] = new SqlParameter("@Member_Sex", SqlDbType.VarChar);
        spars[7].Value = Convert.ToString(sGender);

        spars[8] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[8].Value = Convert.ToString(txtEmpCode.Text).Trim();

        spars[9] = new SqlParameter("@medical_type", SqlDbType.VarChar);
        spars[9].Value = Convert.ToString(medical_type).Trim();

        dsgoal = spm.getDatasetList(spars, "SP_Insert_Nominations_Mediclaim");
    }

    protected void fuel_btnSave_Click(object sender, EventArgs e)
    {
        #region Add or Update Employee Mediclaim data
        if (Convert.ToString(txtMember_Name_self.Text).Trim() != "")
        {
            if (Convert.ToString(txt_Birthdate_self.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Self Birth Date.";
                return;
            }
            if (Convert.ToString(ddl_Gender_self.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Self Gender.";
                return;
            }
            if (Convert.ToString(txt_Age_self.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter valid Self Birth Date.";
                return;
            }
            AddUpdate_MediclaimData(hdnNominiId.Value, "1", "Self", txtMember_Name_self.Text, txt_Birthdate_self.Text, txt_Age_self.Text, ddl_Gender_self.SelectedValue);

        }

        if (Convert.ToString(txtMember_Name_spouse.Text).Trim() != "")
        {
            if (Convert.ToString(txt_Birthdate_spouse.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Spouse Birth Date.";
                return;
            }
            if (Convert.ToString(ddl_Gender_spouse.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Spouse Gender.";
                return;
            }
            if (Convert.ToString(txt_Age_spouse.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter valid Spouse Birth Date.";
                return;
            }

            AddUpdate_MediclaimData(hdnNominiId.Value, "2", "Spouse", txtMember_Name_spouse.Text, txt_Birthdate_spouse.Text, txt_Age_spouse.Text, ddl_Gender_spouse.SelectedValue);
        }

        if (Convert.ToString(txtMember_Name_childern1.Text).Trim() != "")
        {
            if (Convert.ToString(txt_Birthdate_childern1.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Childern1 Birth Date.";
                return;
            }
            if (Convert.ToString(ddl_Gender_childern1.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Childern1 Gender.";
                return;
            }
            if (Convert.ToString(txt_Age_childern1.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter valid Childern1 Birth Date.";
                return;
            }

            AddUpdate_MediclaimData(hdnNominiId.Value, "3", "Children1", txtMember_Name_childern1.Text, txt_Birthdate_childern1.Text, txt_Age_childern1.Text, ddl_Gender_childern1.SelectedValue);

        }
        if (Convert.ToString(txtMember_Name_childern2.Text).Trim() != "")
        {
            if (Convert.ToString(txt_Birthdate_childern2.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Childern2 Birth Date.";
                return;
            }
            if (Convert.ToString(ddl_Gender_childern2.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Childern2 Gender.";
                return;
            }
            if (Convert.ToString(txt_Age_childern2.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter valid Childern2 Birth Date.";
                return;
            }


            AddUpdate_MediclaimData(hdnNominiId.Value, "4", "Children2", txtMember_Name_childern2.Text, txt_Birthdate_childern2.Text, txt_Age_childern2.Text, ddl_Gender_childern2.SelectedValue);
        }
        #endregion

        lblmessage.Text = "Mediclaim Data Updated Successfully";
        return;


        #region old code

        string[] strdate;
        string strfromDate = "";
        lblmessage.Text = "";
        string filename = "";
        string strfileName = "";

        #region Validation

        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }
        //if (dgFuelClaim.Rows.Count == 0)
        //{
        //    lblmessage.Text = "Please Submit Nominations for PF!";
        //    return;
        //}

        if (dgFuelOut.Rows.Count == 0)
        {
            lblmessage.Text = "Please Add Members for Mediclaim Nominations!";
            return;
        }

        #region File Upload
        if (uploadfile.HasFile)
        {
            //
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
            if (dgFuelOut.Rows.Count > 0)
            {
                ////if (Convert.ToString(filename).Trim() == "")
                ////{
                ////    lblmessage.Text = "Please upload file for Fuel Claim .";
                ////    return;
                ////}
            }
        }
        #endregion File Upload
        #endregion

        #region File Upload
        if (uploadfile.HasFile)
        {
            if (dgFuelOut.Rows.Count > 0)
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

        dtMaxRempID = spm.InsertNominations(strfromDate, txtEmpCode.Text, Convert.ToString(filename).Trim(), Convert.ToString(hdnRemid.Value).Trim(), DateTime.Today.Year.ToString(), Convert.ToString(ddlMaritalStatus.SelectedValue).Trim());

        int maxRemid = Convert.ToInt32(dtMaxRempID.Rows[0]["maxRemid"]);
        if (dgFuelClaim.Rows.Count > 0)
        {
            spm.Insert_Nominations_PF(maxRemid, null, txtEmpCode.Text, "", "", 0, "", "InsertMainTable", 0);
        }

        if (dgFuelOut.Rows.Count > 0)
        {
            spm.Insert_Nominations_Mediclaim(maxRemid, null, txtEmpCode.Text, "", "", "", 0, "InsertMainTable", 0, Convert.ToString(ddlMaritalStatus.SelectedValue).Trim());
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


                string FuelclaimPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["NominationsDocumentpath"]).Trim() + Convert.ToString(hdnRemid.Value) + "/");
                bool folderExists = Directory.Exists(FuelclaimPath);
                if (!folderExists)
                {
                    Directory.CreateDirectory(FuelclaimPath);
                }


                string[] Files = Directory.GetFiles(FuelclaimPath);
                foreach (string file in Files)
                {
                    File.Delete(file);
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
                        String InputFile = System.IO.Path.GetExtension(uploadfileName.FileName);
                        strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + Convert.ToString(i + 1).Trim() + "_Nominations" + (i + 1).ToString() + InputFile;
                        filename = strfileName;
                        uploadfileName.SaveAs(Path.Combine(FuelclaimPath, strfileName));

                        spm.InsertNomination_Files(maxRemid, blnfile, Convert.ToString(strfileName).Trim(), "Nominations", i + 1);
                        blnfile = true;
                    }
                }

            }


        }
        #endregion

        // spm.Fuel_send_mailto_RM_Approver(txtEmpName.Text, hflEmailAddress.Value, hdnApprEmailaddress.Value, "Request for Fuel bill Reimbursement", txtAmount.Text, txtQuantityClaim.Text, txtEmpName.Text, "","");
        string strFuelClaimtable = "";
        strFuelClaimtable = getFuelClaimDetails_table();

        string strcliamMonth = Convert.ToString(hdnfrmdate_emial.Value).Trim();
        ////spm.Fuel_send_mailto_RM_Approver(txtEmpName.Text, hdnApprovalCOS_mail.Value, "PF & Mediclaim Nominations", "", "","", txtEmpName.Text, "", "", strFuelClaimtable, strcliamMonth);
        //Session["Insertsecond"] = null;
        lblmessage.Visible = true;
        lblmessage.Text = "Nominations Submitted Successfully";
        Response.Redirect("~/PersonalDocuments.aspx");
        #endregion
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

        //strapprovermails = GetApprove_RejectList(Convert.ToDecimal(hdnRemid.Value));
        string strFuelClaimtable = "";
        strFuelClaimtable = getFuelClaimDetails_table();
        string strcliamMonth = Convert.ToString(hdnfrmdate_emial.Value).Trim();

        spm.Insert_Nominations_PF(Convert.ToInt32(hdnRemid.Value), null, txtEmpCode.Text, "", "", 0, "", "CancelFuelRem", 0);
        // spm.InsertMobileClaimDetails(Convert.ToInt32(hdnRemid.Value), "", 0, txtEmpCode.Text, 0, Convert.ToString("CancelMobRem"), "", "", "");
        ////spm.Fuel_send_mail_Cancel(txtEmpName.Text, hdnApprovalCOS_mail.Value, "Request for Fuel bill Reimbursement of", "", "", "", strapprovermails, "", "", strFuelClaimtable, strcliamMonth);
        Session["tollchrgs"] = "";
        Session["airportparking"] = "";
        Session["parkwashclaimed"] = "";
        Response.Redirect("~/PersonalDocuments.aspx");
    }

    protected void lnkEdit_Click1(object sender, EventArgs e)
    {
        AssigningSessions();
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnRemid.Value = Convert.ToString(dgFuelClaim.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnClaimsID.Value = Convert.ToString(dgFuelClaim.DataKeys[row.RowIndex].Values[1]).Trim();

        if (Convert.ToString(hdnClaimsID.Value).Trim() != "0")
        {
            get_Nominations_PF_Edit();
            //claimfuel_btnDelete.Visible = true;
        }
        //Response.Redirect("~/procs/FuelClaim_o.aspx?clmid="+hdnClaimsID.Value+"&rem_id=" + hdnRemid.Value );
    }

    protected void Del_Fuel_bill(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnRemid.Value = Convert.ToString(dgFuelClaim.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnClaimsID.Value = Convert.ToString(dgFuelClaim.DataKeys[row.RowIndex].Values[1]).Trim();

        hdnsptype.Value = "deleteTempTable";

        spm.Insert_Nominations_Mediclaim(0, null, txtEmpCode.Text, "", "", "", '0', Convert.ToString(hdnsptype.Value), Convert.ToInt32(hdnClaimsID.Value), "");
        //spm.Insert_Nominations_PF(Convert.ToInt32(hdnRemid.Value), "", txtEmpCode.Text, "", "", 0, "", Convert.ToString(hdnsptype.Value), Convert.ToInt32(hdnClaimsID.Value));
        get_NominationsDetails();
        //get_emp_Nominations_File();
    }

    protected void Del_Outstation(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnRemid.Value = Convert.ToString(dgFuelOut.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnClaimsID.Value = Convert.ToString(dgFuelOut.DataKeys[row.RowIndex].Values[1]).Trim();

        hdnsptype.Value = "deleteTempTable";

        //spm.Insert_Nominations_Mediclaim(0, null, txtEmpCode.Text, "", "", "", Convert.ToDecimal(txtAge_Med.Text), Convert.ToString(hdnsptype.Value), Convert.ToInt32(hdnClaimsID.Value));
        spm.Insert_Nominations_Mediclaim(0, null, txtEmpCode.Text, "", "", "", '0', Convert.ToString(hdnsptype.Value), Convert.ToInt32(hdnClaimsID.Value), "");
        get_NominationsDetails();
        //get_emp_Nominations_File();
    }

    protected void dgFuelClaim_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            if (Convert.ToString(hdnRemid.Value).Trim() == "" || Convert.ToString(hdnRemid.Value).Trim() == "0")
            {
                e.Row.Cells[5].Visible = true;
                e.Row.Cells[6].Visible = true;
            }
            else
            {
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
            }
        }
    }

    protected void btnOut_Details_Click(object sender, EventArgs e)
    {
        string Med_sex = "";
        if (Convert.ToString(ddlMaritalStatus.SelectedValue).Trim() == "0")
        {
            lblmessage.Text = "Please select marital status";
            return;
        }
        if (ddlRelationMed.SelectedValue == "Select")
        {
            lblmessage.Text = "Please select Relation with Employee!";
            return;
        }
        if (Convert.ToString(txtMember_Name.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Member Name!";
            return;
        }
        if (Convert.ToString(txtFromdateMain.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter submission date";
            return;
        }
        if (Convert.ToString(txtBirthdatedate_Med.Text).Trim() == "")
        {
            lblmessage.Text = "Birthdate cannot be blank";
            return;
        }

        if (Convert.ToString(txtAge_Med.Text).Trim() == "")
        {
            lblmessage.Text = "Birthdate cannot be blank";
            return;
        }
        AssigningSessions();
        //Response.Redirect("~/procs/fuelClaim_Out.aspx?clmid=0&rem_id=" + hdnRemid.Value + "&inb=0");

        string[] strdate;
        #region Validations
        if (((ddlRelationMed.SelectedValue == "Children1") && (Convert.ToInt32(txtAge_Med.Text.Trim()) > 25)) || ((ddlRelationMed.SelectedValue == "Children2") && (Convert.ToInt32(txtAge_Med.Text.Trim()) > 25)))
        {
            lblmessage.Text = "Children above age 25 yrs not allowed.";
            return;
        }
        //if (Convert.ToString(txtRelationMed.Text).Trim() == "")
        //{
        //    lblmessage.Text = "Please enter Relation with Employee!";
        //    return;
        //}

        if (Med_male.Checked)
        {
            Med_sex = "Male";
        }
        else
        {
            Med_sex = "Female";
        }
        if (Med_sex == "")
        {
            lblmessage.Text = "Please Select Gender!";
            return;
        }
        #endregion

        string strfromDate = "";

        if (Convert.ToString(txtBirthdatedate_Med.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtBirthdatedate_Med.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        hdnsptype.Value = "InsertTempTable";

        if (Convert.ToString(hdnClaimsID.Value).Trim() == "")
        {
            hdnClaimsID.Value = "0";
        }
        if (Convert.ToString(hdnClaimsID.Value).Trim() != "" && Convert.ToString(hdnClaimsID.Value).Trim() != "0")
            hdnsptype.Value = "updateTempTable";

        //CheckalreadyExistsRelation
        DataTable Dt = spm.ChkRelationExist(txtEmpCode.Text, ddlRelationMed.SelectedValue.ToString().Trim());
        if (Dt.Rows.Count > 0)
        {
            lblmessage.Text = "Relation already exists.";
        }
        else
        {

            //spm.Insert_Nominations_Mediclaim(Convert.ToInt32(hdnRemid.Value), strfromDate, txtEmpCode.Text, txtMember_Name.Text.ToString().Trim(), txtRelationMed.Text.ToString().Trim(), Med_sex, Convert.ToDecimal(txtAge_Med.Text), Convert.ToString(hdnsptype.Value), Convert.ToInt32(hdnClaimsID.Value));
            spm.Insert_Nominations_Mediclaim(Convert.ToInt32(hdnRemid.Value), strfromDate, txtEmpCode.Text, txtMember_Name.Text.ToString().Trim(), ddlRelationMed.SelectedValue.ToString().Trim(), Med_sex, Convert.ToDecimal(txtAge_Med.Text), Convert.ToString(hdnsptype.Value), Convert.ToInt32(hdnClaimsID.Value), Convert.ToString(ddlMaritalStatus.SelectedValue).Trim());
            //Response.Redirect("~/procs/Fuel_Req.aspx?clmid=" + hdnClaimsID.Value + "&rem_id=" + hdnremid.Value);
            get_NominationsDetails();
            txtBirthdatedate_Med.Text = "";
            //spousebirthdate.Text = "";
            txtAge_Med.Text = "";
            txtMember_Name.Text = "";
            //txtRelationMed.Text = "";
            ddlRelationMed.SelectedValue = "Select";
            // get_emp_Nominations_File();
            hdnClaimsID.Value = "0";
        }

    }

    protected void lnkEdit_Click2(object sender, EventArgs e)
    {
        AssigningSessions();
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnRemid.Value = Convert.ToString(dgFuelOut.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnClaimsID.Value = Convert.ToString(dgFuelOut.DataKeys[row.RowIndex].Values[1]).Trim();
        if (Convert.ToString(hdnClaimsID.Value).Trim() != "0")
        {
            get_Nominations_Med_Edit();
            //claimfuel_btnDelete.Visible = true;
        }

        // Response.Redirect("~/procs/fuelClaim_Out.aspx?clmid=" + hdnClaimsID.Value + "&rem_id=" + hdnRemid.Value);
    }

    protected void dgFuelOut_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            if (Convert.ToString(hdnRemid.Value).Trim() == "" || Convert.ToString(hdnRemid.Value).Trim() == "0")
            {
                e.Row.Cells[5].Visible = true;
                e.Row.Cells[6].Visible = true;
            }
            else
            {
                //e.Row.Cells[5].Visible = false;
                //e.Row.Cells[6].Visible = false;
            }
        }
    }


    protected void lnkuplodedfile_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["NominationsDocumentpath"]).Trim()), lnkuplodedfile.Text);

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

        String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["NominationsDocumentpath"]).Trim()), strfilename);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
        Response.WriteFile(strfilepath);
        Response.End();

    }

    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        try
        {


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    }

    private string getMember_Age(string sbirthdate)
    {
        string[] strdate;
        string sbdate = "";

        string memberbdate = "";
        strdate = Convert.ToString(sbirthdate).Trim().Split('/');
        sbdate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);


        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_employee_Age";

        spars[1] = new SqlParameter("@att_date", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(sbdate).Trim();

        dsList = spm.getDatasetList(spars, "Usp_getEmployee_Details_All");

        if (dsList.Tables[0].Rows.Count > 0)
        {
            memberbdate = Convert.ToString(dsList.Tables[0].Rows[0]["age"]);
        }
        return memberbdate;

    }
    protected void txtFromdateOut_TextChanged(object sender, EventArgs e)
    {
        try
        {
            /*string[] strdate;
            string strFromDate = "";
            if (Convert.ToString(txtBirthdatedate_Med.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtBirthdatedate_Med.Text).Trim().Split('/');
                strFromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

                System.DateTime firstDate = new System.DateTime(Convert.ToInt32(strdate[2]), Convert.ToInt32(strdate[1]), Convert.ToInt32(strdate[0]));
                System.DateTime secondDate = new System.DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
                double diff2 = (secondDate - firstDate).TotalDays;
                txtAge_Med.Text = Convert.ToString(Math.Round((diff2 / 365), 0));
            }*/

            string[] strdate;
            string strFromDate = "";
            if (Convert.ToString(txtBirthdatedate_Med.Text).Trim() != "")
            {
                strdate = Convert.ToString(txtBirthdatedate_Med.Text).Trim().Split('/');
                strFromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

                //System.DateTime firstDate = new System.DateTime(Convert.ToInt32(strdate[2]), Convert.ToInt32(strdate[1]), Convert.ToInt32(strdate[0]));
                //System.DateTime secondDate = new System.DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
                //double diff2 = (secondDate - firstDate).TotalDays;
                // txtAge_PF.Text = Convert.ToString(Math.Round((diff2 / 365),0));


                DataSet dsList = new DataSet();
                SqlParameter[] spars = new SqlParameter[2];

                spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                spars[0].Value = "get_employee_Age";

                spars[1] = new SqlParameter("@att_date", SqlDbType.VarChar);
                spars[1].Value = Convert.ToString(strFromDate).Trim();

                dsList = spm.getDatasetList(spars, "Usp_getEmployee_Details_All");

                if (dsList.Tables[0].Rows.Count > 0)
                {
                    txtAge_Med.Text = Convert.ToString(dsList.Tables[0].Rows[0]["age"]);
                }
                else
                {
                    txtAge_Med.Text = "";
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    }

    #endregion

    #region PageMethods

    public void get_employee_FuelUploaded_Files()
    {
        try
        {


            DataSet dsfuelFiles = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_NominationsFiles";

            spars[1] = new SqlParameter("@filetype", SqlDbType.VarChar);
            spars[1].Value = "Nominations";

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
    public void get_emp_Nominations_File()
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "getemp_Nominations_File";

            spars[1] = new SqlParameter("@Empcode", SqlDbType.VarChar);
            spars[1].Value = txtEmpCode.Text;

            spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[2].Value = hdnRemid.Value;

            spars[3] = new SqlParameter("@ClaimDate", SqlDbType.VarChar);
            spars[3].Value = DBNull.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(dsTrDetails.Tables[2].Rows[0]["UploadFile"]) != "")
                {
                    lnkuplodedfile.Text = Convert.ToString(dsTrDetails.Tables[2].Rows[0]["UploadFile"]);
                }

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

                if (Convert.ToString(dtEmpDetails.Rows[0]["EMPLOYMENT_TYPE"]) == "3" || Convert.ToString(dtEmpDetails.Rows[0]["EMPLOYMENT_TYPE"]) == "6" || Convert.ToString(dtEmpDetails.Rows[0]["EMPLOYMENT_TYPE"]) == "7" || Convert.ToString(dtEmpDetails.Rows[0]["EMPLOYMENT_TYPE"]) == "8")
                {
                    Div1.Visible = false;
                    Div2.Visible = false;
                    Label1.Text = "You are not entitled for Nomination";
                    Label1.Visible = true;
                }
                else
                {
                    Div1.Visible = true;
                    Div2.Visible = true;
                    Label1.Visible = false;
                }

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
    }

    public void check_Nomination()
    {
        DataTable dtMobileDetails = new DataTable();
        dtMobileDetails = spm.Check_Nominations(txtEmpCode.Text);
        if (dtMobileDetails.Rows.Count > 0)
        {
            hdnRemid.Value = Convert.ToString(dtMobileDetails.Rows[0]["Nom_id"]);
        }
    }

    public void get_NominationsDetails()
    {
        DataTable dtMobileDetails = new DataTable();
        dtMobileDetails = spm.GetNominations_PF(txtEmpCode.Text);
        dgFuelClaim.DataSource = null;
        dgFuelClaim.DataBind();

        if (dtMobileDetails.Rows.Count > 0)
        {
            //hdnClaimsID.Value = Convert.ToString(dtMobileDetails.Rows[0]["FuelClaims_id"]);
            dgFuelClaim.DataSource = dtMobileDetails;
            dgFuelClaim.DataBind();
        }

        DataTable dtOutDetails = new DataTable();
        dtOutDetails = spm.GetNominations_Med(txtEmpCode.Text);

        dgFuelOut.DataSource = null;
        dgFuelOut.DataBind();

        if (dtOutDetails.Rows.Count > 0)
        {
            //hdnClaimsID.Value = Convert.ToString(dtOutDetails.Rows[0]["FuelClaims_id"]);
            var getMaritalStatus = Convert.ToString(dtOutDetails.Rows[0]["MaritalStatus"]);
            if (getMaritalStatus == "")
            {
                getMaritalStatus = "0";
            }
            dgFuelOut.DataSource = dtOutDetails;
            dgFuelOut.DataBind();
            var getv = ddlMaritalStatus.SelectedValue;
            ddlMaritalStatus.Items.FindByValue(Convert.ToString(getv)).Selected = false;
            ddlMaritalStatus.Items.FindByValue(Convert.ToString(getMaritalStatus)).Selected = true;
            if (getMaritalStatus == "Single")
            {
                ddlMaritalStatus.Enabled = true;
            }
            else if (getMaritalStatus == "Married")
            {
                ddlMaritalStatus.Enabled = false;
            }
        }
    }

    private void InsertNominations_DatatoTempTables()
    {
        try
        {

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_Nominations_insert_mainData_toTempTabls";

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

    public void getNominations_usingRemid()
    {
        try
        {
            DataSet dtTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_getMainNominations";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Int);
            spars[1].Value = hdnRemid.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            dtTrDetails = spm.getDatasetList(spars, "[SP_GETALLreembursement_DETAILS]");
            if (dtTrDetails.Tables[0].Rows.Count > 0)
            {
                txtFromdateMain.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Nom_Sub_Date"]);
                txtprofile.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["UploadFile"]);
                lnkuplodedfile.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["UploadFile"]).Trim();

                //  txtDeviation.Text = Convert.ToString(dtTrDetails.Rows[0]["Reason"]);
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
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
            sbapp.Append("<td colspan=5>Provident Funds Nominations</td>");

            sbapp.Append("</tr>");


            sbapp.Append("<tr>");
            sbapp.Append("<td>Nominee name</td>");
            sbapp.Append("<td>Nominee Relation</td>");
            sbapp.Append("<td>Gender</td>");
            sbapp.Append("<td>Birthdate</td>");
            sbapp.Append("<td>Age</td>");
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

        #region Fuel Outstation Table
        if (dgFuelOut.Rows.Count > 0)
        {
            sbapp.Append("<br/><br/><br/>");
            //sbapp.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:80%;' border=1>");
            //sbapp.Append("<table style='border: 1px solid black' cellspacing='1' cellpading='1'>");
            sbapp.Append("<table width='100%' cellspacing='0' cellpadding='0' border='1' align='center'>");
            sbapp.Append("<tr>");
            sbapp.Append("<td colspan=6>MediClaim Policy Declaration</td>");
            sbapp.Append("</tr>");

            sbapp.Append("<tr>");
            sbapp.Append("<td>Member Name</td>");
            sbapp.Append("<td>Member Relation</td>");
            sbapp.Append("<td>Gender</td>");
            sbapp.Append("<td>Birth Date</td>");
            sbapp.Append("<td>Age</td>");
            sbapp.Append("</tr>");

            for (int i = 0; i < dgFuelOut.Rows.Count; i++)
            {
                sbapp.Append("<tr>");
                sbapp.Append("<td>" + Convert.ToString(dgFuelOut.Rows[i].Cells[0].Text).Trim() + "</td>");
                sbapp.Append("<td>" + Convert.ToString(dgFuelOut.Rows[i].Cells[1].Text).Trim() + "</td>");
                sbapp.Append("<td>" + Convert.ToString(dgFuelOut.Rows[i].Cells[2].Text).Trim() + "</td>");
                sbapp.Append("<td>" + Convert.ToString(dgFuelOut.Rows[i].Cells[3].Text).Trim() + "</td>");
                sbapp.Append("<td>" + Convert.ToString(dgFuelOut.Rows[i].Cells[4].Text).Trim() + "</td>");
                sbapp.Append("</tr>");
            }
            sbapp.Append("</table>");
        }
        #endregion


        //return Convert.ToString(sbapp);
        return Convert.ToString("");
    }

    public void get_Nominations_PF_Edit()
    {
        DataTable dtMobileDetails = new DataTable();
        dtMobileDetails = spm.GetNominations_PFEdit(Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnClaimsID.Value), Convert.ToString(txtEmpCode.Text));

        if (dtMobileDetails.Rows.Count > 0)
        {
            txtBirthdatedate_PF.Text = Convert.ToString(dtMobileDetails.Rows[0]["BirthDate"]);
            txtNominee.Text = Convert.ToString(dtMobileDetails.Rows[0]["NomineeName"]);
            txtRelationPF.Text = Convert.ToString(dtMobileDetails.Rows[0]["Nominee_Rel"]);
            txtAge_PF.Text = Convert.ToString(dtMobileDetails.Rows[0]["Age"]);
            if (Convert.ToString(dtMobileDetails.Rows[0]["Nominee_Sex"]) == "Male")
            {
                PF_male.Checked = true;
                PF_female.Checked = false;
            }
            else
            {
                PF_male.Checked = false;
                PF_female.Checked = true;
            }
        }

    }

    public void get_Nominations_Med_Edit()
    {
        DataTable dtMobileDetails = new DataTable();
        dtMobileDetails = spm.GetNominations_MedEdit(Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnClaimsID.Value), Convert.ToString(txtEmpCode.Text));

        if (dtMobileDetails.Rows.Count > 0)
        {
            txtBirthdatedate_Med.Text = Convert.ToString(dtMobileDetails.Rows[0]["BirthDate"]);
            string Age = string.Format("{0:0.##}", dtMobileDetails.Rows[0]["Age"]);
            txtAge_Med.Text = Age;
            txtMember_Name.Text = Convert.ToString(dtMobileDetails.Rows[0]["MemberName"]);
            //txtRelationMed.Text = Convert.ToString(dtMobileDetails.Rows[0]["Member_Rel"]);
            ddlRelationMed.SelectedValue = Convert.ToString(dtMobileDetails.Rows[0]["Member_Rel"]);
            if (Convert.ToString(dtMobileDetails.Rows[0]["Member_Sex"]) == "Male")
            {
                PF_male.Checked = true;
                PF_female.Checked = false;
                Med_male.Checked = true;
                Med_female.Checked = false;
            }
            else
            {
                PF_male.Checked = false;
                PF_female.Checked = true;
                Med_male.Checked = false;
                Med_female.Checked = true;
            }
        }

    }
    protected void ddlMaritalStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var getValue = Convert.ToString(ddlMaritalStatus.SelectedItem.Value);
            if (getValue == "0")
            {
                lblmessage.Text = "Please select Marital Status!";
                var getv = ddlRelationMed.SelectedValue;
                ddlRelationMed.Items.FindByValue(Convert.ToString(getv)).Selected = false;
                ddlRelationMed.Items.FindByValue(Convert.ToString("Select")).Selected = true;
                ddlRelationMed.Enabled = true;
            }
            else
            {
                if (getValue == "Single")
                {
                    var getv = ddlRelationMed.SelectedValue;
                    ddlRelationMed.Items.FindByValue(Convert.ToString(getv)).Selected = false;
                    ddlRelationMed.Items.FindByValue(Convert.ToString("Self")).Selected = true;
                    ddlRelationMed.Enabled = false;
                    //getEmployeeJoiningDetails();
                }
                else
                {
                    var getv = ddlRelationMed.SelectedValue;
                    ddlRelationMed.Items.FindByValue(Convert.ToString(getv)).Selected = false;
                    ddlRelationMed.Items.FindByValue(Convert.ToString("Select")).Selected = true;
                    ddlRelationMed.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    public bool Between(DateTime input, DateTime date1, DateTime date2)
    {
        return (input >= date1 && input <= date2);
    }

    public void getEmployeeJoiningStatus()
    {
        DataTable dataTable = new DataTable();
        try
        {
            fuel_btnSave.Visible = true;
            DataSet dtTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getEmployeeJoiningStatus";
            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = txtEmpCode.Text;

            dtTrDetails = spm.getDatasetList(spars, "[SP_Insert_Nominations_Mediclaim]");
            if (dtTrDetails != null)
            {
                dataTable = dtTrDetails.Tables[0];
                var getStatus = Convert.ToString(dataTable.Rows[0]["EmployeeStatus"]);
                if (getStatus == "New Joining")
                {
                    fuel_btnSave.Visible = true;
                }
                else
                {
                    var getYear = DateTime.Now.Year;
                    var getToDayDate = DateTime.Now.ToString("dd-MM-yyyy");
                    //var getToDayDate = "01-05-2023";
                    //DateTime getStartDate = DateTime.ParseExact("01-04-" + getYear, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    //DateTime getEndDate = DateTime.ParseExact("15-04-" + getYear, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    DateTime getStartDate = DateTime.ParseExact("20-03-" + getYear, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    DateTime getEndDate = DateTime.ParseExact("03-04-" + getYear, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                    DateTime getToDate = DateTime.ParseExact(getToDayDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    var get = Between(getToDate, getStartDate, getEndDate);
                    if (get == true)
                    {
                        fuel_btnSave.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            dataTable = null;
        }
    }

    public void getEmployeeJoiningDetails()
    {
        DataTable dataTable = new DataTable();
        try
        {
            // fuel_btnSave.Visible = false;
            DataSet dtTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getEmployeeJoiningDetails";
            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = txtEmpCode.Text;

            dtTrDetails = spm.getDatasetList(spars, "[SP_Insert_Nominations_Mediclaim]");
            if (dtTrDetails != null)
            {
                dataTable = dtTrDetails.Tables[0];
                var Emp_Name = Convert.ToString(dataTable.Rows[0]["Emp_Name"]);
                var Emp_Emailaddress = Convert.ToString(dataTable.Rows[0]["Emp_Emailaddress"]);
                var Employee_DOB = Convert.ToString(dataTable.Rows[0]["Employee_DOB"]);
                var AGE = Convert.ToString(dataTable.Rows[0]["AGE"]);
                var gender = Convert.ToString(dataTable.Rows[0]["Gender"]);
                txtMember_Name.Text = Emp_Name;
                txtBirthdatedate_Med.Text = Employee_DOB;
                txtAge_Med.Text = AGE;
                txtgender.Text = gender;
            }
        }
        catch (Exception ex)
        {
            dataTable = null;
        }
    }

    public void get_employee_Medical_nominationsList()
    {
        DataTable dataTable = new DataTable();
        try
        {
            // fuel_btnSave.Visible = false;
            DataSet dlist = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "get_Employee_Medical_Nominations_list";
            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = txtEmpCode.Text;

            dlist = spm.getDatasetList(spars, "[SP_Insert_Nominations_Mediclaim]");
            if (dlist != null)
            {
                if (dlist.Tables[0].Rows.Count > 0)
                {
                    hdnNominiId.Value = Convert.ToString(dlist.Tables[0].Rows[0]["MediclaimID"]);
                    foreach (DataRow row in dlist.Tables[0].Rows)
                    {
                        if (Convert.ToString(row["Member_Rel"]).Trim() == "Self")
                        {
                            txtMember_Name_self.Text = Convert.ToString(row["MemberName"]);
                            txt_Birthdate_self.Text = Convert.ToString(row["BirthDate"]);
                            txt_Age_self.Text = Convert.ToString(row["AGE"]);
                            ddl_Gender_self.SelectedValue = Convert.ToString(row["Member_Sex"]);
                        }
                        if (Convert.ToString(row["Member_Rel"]).Trim() == "Spouse")
                        {
                            txtMember_Name_spouse.Text = Convert.ToString(row["MemberName"]);
                            txt_Birthdate_spouse.Text = Convert.ToString(row["BirthDate"]);
                            txt_Age_spouse.Text = Convert.ToString(row["AGE"]);
                            ddl_Gender_spouse.SelectedValue = Convert.ToString(row["Member_Sex"]);
                        }

                        if (Convert.ToString(row["Member_Rel"]).Trim() == "Children1")
                        {
                            txtMember_Name_childern1.Text = Convert.ToString(row["MemberName"]);
                            txt_Birthdate_childern1.Text = Convert.ToString(row["BirthDate"]);
                            txt_Age_childern1.Text = Convert.ToString(row["AGE"]);
                            ddl_Gender_childern1.SelectedValue = Convert.ToString(row["Member_Sex"]);
                        }

                        if (Convert.ToString(row["Member_Rel"]).Trim() == "Children2")
                        {
                            txtMember_Name_childern2.Text = Convert.ToString(row["MemberName"]);
                            txt_Birthdate_childern2.Text = Convert.ToString(row["BirthDate"]);
                            txt_Age_childern2.Text = Convert.ToString(row["AGE"]);
                            ddl_Gender_childern2.SelectedValue = Convert.ToString(row["Member_Sex"]);
                        }

                    }
                }
            }
        }
        catch (Exception ex)
        {
            dataTable = null;
        }
    }



    protected void ddlRelationMed_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var getVal = Convert.ToString(ddlRelationMed.SelectedValue);
            if (getVal == "Select")
            {
                txtMember_Name.Text = "";
                txtBirthdatedate_Med.Text = "";
                txtAge_Med.Text = "";
            }
            else
            {
                if (getVal == "Self")
                {
                    //getEmployeeJoiningDetails();
                }
                else
                {
                    if (dgFuelOut.Rows.Count == 0)
                    {
                        lblmessage.Text = "Please add self entry first.";
                        var getv = ddlRelationMed.SelectedValue;
                        ddlRelationMed.Items.FindByValue(Convert.ToString(getv)).Selected = false;
                        ddlRelationMed.Items.FindByValue(Convert.ToString("Select")).Selected = true;
                        return;
                    }
                    if (ddlMaritalStatus.SelectedValue == "Single")
                    {
                        lblmessage.Text = "Not Alowed .";
                        return;
                    }

                    txtMember_Name.Text = "";
                    txtBirthdatedate_Med.Text = "";
                    txtAge_Med.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void txt_Birthdate_self_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txt_Birthdate_self.Text).Trim() != "")
            txt_Age_self.Text = getMember_Age(txt_Birthdate_self.Text);
        else
            txt_Age_self.Text = "";
    }

    protected void txt_Birthdate_spouse_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txt_Birthdate_spouse.Text).Trim() != "")
            txt_Age_spouse.Text = getMember_Age(txt_Birthdate_spouse.Text);
        else
            txt_Age_spouse.Text = "";
    }

    protected void txt_Birthdate_childern1_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txt_Birthdate_childern1.Text).Trim() != "")
            txt_Age_childern1.Text = getMember_Age(txt_Birthdate_childern1.Text);
        else
            txt_Age_childern1.Text = "";
    }

    protected void txt_Birthdate_childern2_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(txt_Birthdate_childern2.Text).Trim() != "")
            txt_Age_childern2.Text = getMember_Age(txt_Birthdate_childern2.Text);
        else
            txt_Age_childern2.Text = "";
    }



    public void CheckIs_SubmitMediclaimData_Access()
    {
        DataSet getdtDetails = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "check_SubmitMediclaimDataStatus_Access";

            spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(txtEmpCode.Text);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "SubmitMediclaimData";

            getdtDetails = spm.getDatasetList(spars, "SP_Employee_Mediclaim_Data");
            spnMediclaimData.Visible = false;
            if (getdtDetails.Tables[0].Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Tables[0].Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    spnMediclaimData.Visible = true;
                }
            }
            // return false;
        }
        catch (Exception ex)
        {
            // return false;
        }
    }

    #endregion


}
 

