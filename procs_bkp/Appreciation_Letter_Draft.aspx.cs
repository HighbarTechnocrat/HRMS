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

public partial class procs_Appreciation_Letter_Draft : System.Web.UI.Page
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
    DataTable dtBankDetail;
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    #endregion

    SP_Methods spm = new SP_Methods();
    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
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
                if (!Page.IsPostBack)
                {
                    GetSkillsetName();
                    liSubject.Visible = false;
                    Point.Visible = false;
                    txt_subject.Visible = false;
                    txtdraft.Visible = false;

                    mobile_cancel.Visible = false;
                    int ss = Request.QueryString.Count;
                    //string b  =Convert.ToString(Request.QueryString[0]).Trim();
                    if (Request.QueryString.Count > 0)
                    {
                        hdnBankDetail_ID.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        GetJDBankDetail();
                        // mobile_cancel.Visible = true;
                        mobile_btnSave.Text = "Modify";
                        mobile_btnSave.ToolTip = "Modify";
                        lblheading.Text = "View JD Bank";
                    }
                    if (Request.QueryString.Count == 2)
                    {
                        hdnFilter.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        mobile_btnSave.Visible = false;
                        mobile_cancel.Visible = false;
                        lstSkillset.Enabled = false;
                         

                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    protected void emp_status_SelectedIndexChanged(object sender, EventArgs e)
    { 
      if(lstSkillset.SelectedValue!=null)
        {
            liSubject.Visible = true;
            Point.Visible = true;
            txt_subject.Visible = true;
            txtdraft.Visible = true;
        }
    }

        protected void mobile_btnSave_Click(object sender, EventArgs e)
    {

        //int BankDetail_ID = 0;
        //string Stype = "INSERT", IsActive = "0";
        try
        {
            #region Check For Blank Fields
            if (Convert.ToString(lstSkillset.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Letter Type";
                return;
            }

            if (Convert.ToString(txt_app_letter.Text).Trim() == "")
            {
                lblmessage.Text = "Please Enter Appreciation Letter";
                return;
            }

            if (Convert.ToString(txt_point.Text).Trim() == "")
            {
                lblmessage.Text = "Please Enter Letter Point";
                return;
            }

            if (Convert.ToString(txt_sub.Text).Trim() == "")
            {
                lblmessage.Text = "Please Enter Letter Subject";
                return;
            }

            if (Convert.ToString(txtText.InnerText).Trim() == "")
            {
                lblmessage.Text = "Please Enter Letter Description";
                return;
            }

            string qtype = "Appreciation_letter_updateDraft";

            DataSet dsgoal = new DataSet();
            SqlParameter[] spars = new SqlParameter[6];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = qtype;

             
            spars[1] = new SqlParameter("@Appreciation_Letter", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(txt_app_letter.Text);

            spars[2] = new SqlParameter("@draft", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(txtText.InnerText.Trim());

            spars[3] = new SqlParameter("@point", SqlDbType.VarChar);
            spars[3].Value = Convert.ToString(txt_point.Text); 
             
            spars[4] = new SqlParameter("@letter_category", SqlDbType.VarChar);
            spars[4].Value = Convert.ToString(lstSkillset.SelectedValue);

            spars[5] = new SqlParameter("@letter_sub", SqlDbType.VarChar);
            spars[5].Value = Convert.ToString(txt_sub.Text);

            dsgoal = spm.getDatasetList(spars, "Appreciation_Letter");

            Response.Redirect("~/procs/Appreciation_Letter_index.aspx");

            //string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            //if (confirmValue != "Yes")
            //{
            //    return;
            //}

            #endregion

            //BankDetail_ID = Convert.ToString(hdnBankDetail_ID.Value).Trim() != "" ? Convert.ToInt32(hdnBankDetail_ID.Value) : 0;
            //dtBankDetail = spm.InsertJDBankDetail(Stype, Convert.ToInt32(lstSkillset.SelectedValue), Convert.ToInt32(lstPositionName.SelectedValue), txtText.InnerText.Trim(), BankDetail_ID, IsActive, Convert.ToString(Session["Empcode"]).Trim());
            //if (dtBankDetail.Rows.Count > 0)
            //{
            //    Response.Redirect("~/procs/Req_JD_Bank_Index.aspx");
            //}
            //else
            //{
            //    lblmessage.Text = "Record already exists!.";
            //}

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    protected void mobile_cancel_Click(object sender, EventArgs e)
    {
        try
        {
            string Stype = "Delete";
            int Quest_ID = 0;
            Quest_ID = Convert.ToString(hdnBankDetail_ID.Value).Trim() != "" ? Convert.ToInt32(hdnBankDetail_ID.Value) : 0;
            spm.InsertJDBankDetail(Stype, 0, 0, "", Quest_ID, "0", Convert.ToString(Session["Empcode"]).Trim());
            Response.Redirect("~/procs/Appreciation_Letter_index.aspx");
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    #endregion

    #region PageMethods
   
    public void GetSkillsetName()
    {
        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "Appreciation_Letter_list_indraft";

        
        dsProjectsVendors = spm.getDatasetList(spars, "Appreciation_Letter");
         
        if (dsProjectsVendors != null && dsProjectsVendors.Tables.Count > 0 && dsProjectsVendors.Tables[0].Rows.Count > 0)
        {

            lstSkillset.DataSource = dsProjectsVendors.Tables[0];
            lstSkillset.DataTextField = "letter_category";
            lstSkillset.DataValueField = "category_id";
            lstSkillset.DataBind(); 

        }
        lstSkillset.Items.Insert(0, new ListItem("Select Appreciation Letter", "0"));
         
    }
    
    private void GetJDBankDetail()
    {

        DataSet dsQuestDetails = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_JD_Bank_Edit";

            spars[1] = new SqlParameter("@JD_BankDetail_ID", SqlDbType.Int);
            spars[1].Value = Convert.ToInt32(hdnBankDetail_ID.Value);

            //spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            //spars[2].Value = Convert.ToString(txtEmpCode.Text);
            dsQuestDetails = spm.getDatasetList(spars, "SP_Rec_JDBankDetail_INSERT");

            if (dsQuestDetails.Tables[0].Rows.Count > 0)
            {
                lstSkillset.SelectedValue = Convert.ToString(dsQuestDetails.Tables[0].Rows[0]["ModuleId"]).Trim();
                
                txtText.InnerText = Convert.ToString(dsQuestDetails.Tables[0].Rows[0]["JobDescription"]).Trim();
                //lnkuploadRcpt.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["rcptFile"]).Trim(); 
                if (Convert.ToString(dsQuestDetails.Tables[0].Rows[0]["IsActive"]).Trim() != "1")
                {
                    chkActive.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    #endregion
}