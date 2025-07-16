using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Linq;
using System.Text;


public partial class procs_Appraisal_login : System.Web.UI.Page
{ 
    
    SP_Methods spm = new SP_Methods(); 
    private static Random random = new Random(); 

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            hdnEmpCode.Value = Convert.ToString(Session["Empcode"]).Trim();
            hdnEmpEmail.Value = Convert.ToString(Session["LoginEmpmail"]).Trim();
            hdnEmpName.Value= Convert.ToString(Session["emp_loginName"]).Trim();

            Page.SmartNavigation = true;
            if (!Page.IsPostBack)
            {
 		txtAccess_Password.Text = "";
                txtChange_Password.Text = "";
                txtCreate_Password.Text = "";
                txt_Old_Passowrd.Text = "";

                if (Request.QueryString.Count > 0)
                {
                    hdnisForgotPMSKey.Value = Convert.ToString(Request.QueryString["p"]).Trim();
                    if(Convert.ToString(hdnisForgotPMSKey.Value).Trim()=="f")
                    {
                        Forgot_PMSKey("forgot");
                    } 
                }

                Check_LoginEmployee_IsCreate_PMS_Secret_Key();
                if (Request.QueryString.Count > 0)
                {
                    if (Convert.ToString(hdnisForgotPMSKey.Value).Trim() == "c")
                    {
                        li_Change_PMS_Key_old_1.Visible = true;
                        li_Change_PMS_Key_old_2.Visible = true;
                        li_Change_PMS_Key_old_3.Visible = true;

                        li_Change_PMS_Key_1.Visible = true;
                        li_Change_PMS_Key_2.Visible = true;
                        li_Change_PMS_Key_3.Visible = true;

                        li_PMS_Pass_1.Visible = false;
                        li_PMS_Pass_2.Visible = false;
                        li_PMS_Pass_3.Visible = false;
                        if (Request.QueryString.Count ==2)
                        {
                            btn_change_PMS_Key.Visible = false;
                            btn_forgot_PMS_Key.Visible = false;
                        }
                    }

                }
                this.Title = creativeconfiguration.SiteName + ": Edit Profile "; 

            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string HashSHA1(string value)
    {
        var sha1 = System.Security.Cryptography.SHA1.Create();
        var inputBytes = Encoding.ASCII.GetBytes(value);
        var hash = sha1.ComputeHash(inputBytes);

        var sb = new StringBuilder();
        for (var i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }

    public void Forgot_PMSKey(string stype)
    {
        try
        { 
            Guid userGuid = System.Guid.NewGuid();
            string resetpwd = RandomString(8);  
            string hashedPassword = HashSHA1(resetpwd ); //+ userGuid.ToString()
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Forgot_PMS_Access_password";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hdnEmpCode.Value).Trim();

            spars[2] = new SqlParameter("@LocationCode", SqlDbType.VarChar);
            if (Convert.ToString(stype) == "forgot")
                spars[2].Value = spm.Encrypt(resetpwd.ToString());
            else
                spars[2].Value = spm.Encrypt(txtChange_Password.Text); 

            DataSet dsReset = spm.ApprgetDatasetList(spars, "SP_GETALL_Appraisal_DETAILS");

            if (dsReset.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(dsReset.Tables[0].Rows[0]["PMS_Pwd"]) != "")
                {
                    if (Convert.ToString(stype) == "forgot")
                        lblmessage.Text = "PMS Access Key is reset Successfully & new key is sent on your Email Address!";// to " + resetpwd.ToString();
                    
                    if (Convert.ToString(stype) == "forgot")
                        spm.sendMail(Convert.ToString(hdnEmpEmail.Value), "oneHR PMS Access Key Reset Request", "Your New PMS Access Key is: " + resetpwd.ToString() + " . Please Change PMS Access on First Login!", "", "");
                    else
                        spm.sendMail(Convert.ToString(hdnEmpEmail.Value), "oneHR PMS Access Key Change ", " Dear "+Convert.ToString(Session["emp_loginName"]) + ", <br/<br/> This is to inform you that, your PMS Access Key is changed. If it is not done by you then please change your password immediately & contact Administrator. ", "", "");

                } 
            }
        }
        catch (Exception ex)
        {

        }
    }

    public bool Validate_PMSKey()
    {
        bool blnCheck = true;
        try
        {
           
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Validate_PMS_Access_password";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hdnEmpCode.Value).Trim();

            spars[2] = new SqlParameter("@LocationCode", SqlDbType.VarChar);
            spars[2].Value = spm.Encrypt(txt_Old_Passowrd.Text); 

            DataSet dsReset = spm.ApprgetDatasetList(spars, "SP_GETALL_Appraisal_DETAILS");

            if (dsReset != null)
            {
                if (dsReset.Tables[0].Rows.Count == 0)
                {
                    lblmessage.Text = "Please enter correct Old PMS Access Key. Please try again";
                    spm.sendMail(Convert.ToString(hdnEmpEmail.Value), "OneHR – PMS: Wrong Password Entry while changing password! ", " Dear " + Convert.ToString(Session["emp_loginName"]) + ", <br/<br/> " +
                        "This is to inform you that wrong password was entered while changing password for OneHR – PMS. If it is not done by you then please change your password immediately.", "", "");

                    blnCheck = false;
                }
            }
        }
        catch (Exception ex)
        {

        }
        return blnCheck;
    }


    public void Check_LoginEmployee_IsCreate_PMS_Secret_Key()
    {

        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "check_PMS_Access_password";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = hdnEmpCode.Value;

            dsTrDetails = spm.ApprgetDatasetList(spars, "SP_GETALL_Appraisal_DETAILS");

            li_PMS_Pass_1.Visible = false;
            li_PMS_Pass_2.Visible = false;
            li_PMS_Pass_3.Visible = false;

            li_Create_PMS_Pass_1.Visible = false;
            li_Create_PMS_Pass_2.Visible = false;
            li_Create_PMS_Pass_3.Visible = false;

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                li_PMS_Pass_1.Visible = true;
                li_PMS_Pass_2.Visible = true;
                li_PMS_Pass_3.Visible = true;
                  btn_change_PMS_Key.Visible = true;
                btn_forgot_PMS_Key.Visible = true;
            }
            else
            {
                li_Create_PMS_Pass_1.Visible = true;
                li_Create_PMS_Pass_2.Visible = true;
                li_Create_PMS_Pass_3.Visible = true;
                 btn_change_PMS_Key.Visible = false;
                btn_forgot_PMS_Key.Visible = false;
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void trvl_btnSave_Click(object sender, EventArgs e)
    {
        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            return;
        }

        lblmessage.Text = "";

        if(Convert.ToString(hdnisForgotPMSKey.Value)=="c")
        {
            if (Convert.ToString(txtChange_Password.Text) == "")
            {
                lblmessage.Text = "Please enter Old PMS Access Secret Key.";
                return;
            }

            if (Convert.ToString(txtChange_Password.Text) =="")
            {
                lblmessage.Text = "Please enter PMS Access Secret Key.";
                return;
            }

            if(Validate_PMSKey()==false)
            {
                txt_Old_Passowrd.Text = "";

                return;
            }
                Forgot_PMSKey("change");

            divClick.Visible = true;
            li_PMS_Pass_1.Visible = false;
            li_PMS_Pass_2.Visible = false;
            li_PMS_Pass_3.Visible = false;

            li_Create_PMS_Pass_1.Visible = false;
            li_Create_PMS_Pass_2.Visible = false;
            li_Create_PMS_Pass_3.Visible = false;

            li_Change_PMS_Key_1.Visible = false;
            li_Change_PMS_Key_3.Visible = false;
            li_Change_PMS_Key_3.Visible = false;


        }

        if (li_Create_PMS_Pass_1.Visible == true)
        {
            if (Convert.ToString(txtCreate_Password.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter PMS Access Secret Key.";
                return;
            }


            #region create PMS Access Key
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "create_PMS_Access_password";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = hdnEmpCode.Value;

            spars[2] = new SqlParameter("@LocationCode", SqlDbType.VarChar);
            spars[2].Value = spm.Encrypt(txtCreate_Password.Text);


            dsTrDetails = spm.ApprgetDatasetList(spars, "SP_GETALL_Appraisal_DETAILS");
            if (dsTrDetails != null)
            {
                if (dsTrDetails.Tables[0].Rows.Count > 0)
                {
                    li_PMS_Pass_1.Visible = false;
                    li_PMS_Pass_2.Visible = false;
                    li_PMS_Pass_3.Visible = false;

                    li_Create_PMS_Pass_1.Visible = false;
                    li_Create_PMS_Pass_2.Visible = false;
                    li_Create_PMS_Pass_3.Visible = false;
                    trvl_btnSave.Visible = false;
                    divClick.Visible = true;
                }
               
            }
            #endregion


        }

        if (li_PMS_Pass_1.Visible == true)
        {

            if (Convert.ToString(txtAccess_Password.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter PMS Access Secret Key.";
                return;
            }


            #region Access PMS Key
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "check_PMS_Access_password";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = hdnEmpCode.Value;

            spars[2] = new SqlParameter("@LocationCode", SqlDbType.VarChar);
            spars[2].Value = spm.Encrypt(txtAccess_Password.Text);

            dsTrDetails = spm.ApprgetDatasetList(spars, "SP_GETALL_Appraisal_DETAILS");

            if (dsTrDetails != null)
            {
                if (dsTrDetails.Tables[0].Rows.Count > 0)
                {
                    string sDecryptPass = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["PMS_Pwd"]).Trim());
                    if (Convert.ToString(txtAccess_Password.Text).Trim() == sDecryptPass)
                    {
                        Session["Empcode_Appr"] = Convert.ToString(hdnEmpCode.Value).Trim();
                        Response.Redirect("Appraisalindex.aspx");
                    }
                    else
                    {
                        lblmessage.Text = "Please enter correct PMS Access Key.";


                        spm.sendMail(Convert.ToString(hdnEmpEmail.Value), "OneHR – PMS: Wrong Password Entry! ", " Dear " + Convert.ToString(Session["emp_loginName"]) + ", <br/<br/> " +
                           "This is to inform you that wrong password was entered while login to the OneHR – PMS. If it is not done by you then please change your password immediately.", "", "");


                    }
                }
            }
            #endregion


        }

    }
}
