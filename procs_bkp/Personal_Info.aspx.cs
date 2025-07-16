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

public partial class Personal_Info : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    DataSet dspaymentVoucher_Apprs = new DataSet();
    #region Creative_Default_methods

    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
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
            Session["chkTrvlAccLocalTrvlbtnStatus"] = "";

            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

            
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/travelindex.aspx");
            }
            else
            {
                Page.SmartNavigation = true;

                if (!Page.IsPostBack)
                {

                    txtEmpCode.Text = Convert.ToString(Session["Empcode"]);

                    hdnTravelConditionid.Value = "2";
                    hdnTraveltypeid.Value = "1";

                    hdnTrdays.Value = "2";
                    hdnEligible.Value = "Eligible";
                    getEmployee_Details();
                    GetVisaDetails();
                    GetMediclaimDetails();
                    GetPFDetails();
                    //GetEmployeeDetails();
                    hdnexp_id.Value = "0";

                    if (Request.QueryString.Count > 0)
                    {
                        hdnTripid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdnexp_id.Value = Convert.ToString(Request.QueryString[1]).Trim();

                    }
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
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

    }
    #endregion

    #region PageMethods


    public void getEmployee_Details()
    {
        try
        {
            string photoname = "";
            DataSet dtTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];


            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "emp_details";

            spars[1] = new SqlParameter("@EMP_CODE", SqlDbType.VarChar);
            spars[1].Value = txtEmpCode.Text;

            //dtTrDetails = spm.getMobileRemDataList(spars, "[SP_GETALLreembursement_DETAILS]");
            dtTrDetails = spm.getDatasetList(spars, "[SP_GETPersonalInfo]");
            if (dtTrDetails.Tables[0].Rows.Count > 0)
            {
                lbl_code.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Emp_Code"]);
                lbl_EmpName.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Emp_Name"]);
                lbl_desg.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Designation"]);
                lbl_dept.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Department"]);
                lbl_band.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["grade"]);
                lbl_Location.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["emp_location"]);
                lbl_email.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Emp_Emailaddress"]);
                lbl_mob.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["mobile"]);
                lbl_telephone.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["emp_office_contact_no"]);
                lbl_DOJ.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["doj"]);
                lbl_dob.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["dob"]);
                lbl_gender.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Gender"]);
                lbl_bloodgrp.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["BloodGrp"]);
                lbl_ismarried.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["ISMARRIED"]);
                lbl_bank_ac_name.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["BANK_AC_NAME"]);
                lbl_bank_branch.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["BANK_BRANCH_NO"]);
                lbl_MICR.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["BANK_MICR"]);
                lbl_bank_add.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["BANK_ADDRESS"]);
                lbl_bank_name.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["BANK_NAME"]);
                lbl_bank_ac_no.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["BANK_AC"]);
                lbl_IFSC.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["IFSC_CODE"]);
                lbl_PAN.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["PAN"]);
                lbl_AADHAR.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["AADHAR"]);
                lbl_PF.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["EPFO_NO"]);
                lbl_UAN.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["UANNO"]);
                lbl_Passport.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["PASSPORT"]);
                lbl_date_Pass_Issue.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["PASS_DATEOFISSUE"]);
                lbl_date_Pass_Exp.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["PASS_EXPIRYDATE"]);
                lbl_Pass_Issue.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["PASS_PLACEOFISSUE"]);
                lbl_ECR.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["ECR_STAMP"]);
                lbl_curadd.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Cur_ADD"]);
                lbl_peradd.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Per_ADD"]);
                lbl_personalemail.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["PERSONAL_EMAIL"]);
                lbl_cont1_name.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["CONTACT_PER_1_NAME"]);
                lbl_cont1_no.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["CONTACT_PER_1_NO"]);
                lbl_cont2_name.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["CONTACT_PER_2_NAME"]);
                lbl_cont2_no.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["CONTACT_PER_2_NO"]);
                if (Convert.ToString(dtTrDetails.Tables[0].Rows[0]["EMPLOYMENT_TYPE"]) == "1")
                    photoname = Convert.ToString(Convert.ToInt32(dtTrDetails.Tables[0].Rows[0]["Emp_Code"])) + ".jpg";
                else
                    photoname = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Emp_Code"]) + ".jpg";
                emp_img.Src = "http://localhost/hrms/themes/creative1.0/images/profile55x55/" + photoname;
                //lbl_code.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["emp_photo"]);
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    }

    public void GetVisaDetails()
    {
        try
        {
            DataTable dtEmpDetails = new DataTable();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "Visa";

            spars[1] = new SqlParameter("@EMP_CODE", SqlDbType.VarChar);
            spars[1].Value = txtEmpCode.Text;

            dtEmpDetails = spm.getDropdownList(spars, "[SP_GETPersonalInfo]");

            if (dtEmpDetails.Rows.Count > 0)
            {
                dgVisadetails.DataSource = dtEmpDetails;
                dgVisadetails.DataBind();
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

    public void GetMediclaimDetails()
    {
        try
        {
            DataTable dtEmpDetails = new DataTable();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "Mediclaim";

            spars[1] = new SqlParameter("@EMP_CODE", SqlDbType.VarChar);
            spars[1].Value = txtEmpCode.Text;

            dtEmpDetails = spm.getDropdownList(spars, "[SP_GETPersonalInfo]");

            if (dtEmpDetails.Rows.Count > 0)
            {
                dgMediclaim.DataSource = dtEmpDetails;
                dgMediclaim.DataBind();
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

    public void GetPFDetails()
    {
        try
        {
            DataTable dtEmpDetails = new DataTable();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "PF";

            spars[1] = new SqlParameter("@EMP_CODE", SqlDbType.VarChar);
            spars[1].Value = txtEmpCode.Text;

            dtEmpDetails = spm.getDropdownList(spars, "[SP_GETPersonalInfo]");

            if (dtEmpDetails.Rows.Count > 0)
            {
                dgPF.DataSource = dtEmpDetails;
                dgPF.DataBind();
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
        Session["ReqEmpCode"] = txtEmpCode.Text;
    }

    private void SessiontoControl()
    {
        txtEmpCode.Text = Convert.ToString(Session["ReqEmpCode"]);
    }

    #endregion
}