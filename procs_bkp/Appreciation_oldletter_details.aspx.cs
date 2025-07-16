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
using iTextSharp.text.pdf.parser.clipper;
using DocumentFormat.OpenXml.Wordprocessing;

public partial class Appreciation_oldletter_details : System.Web.UI.Page
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

                    txtDescription.Text = "";

                    if (Request.QueryString.Count > 0)
                    {
                        //s  hdnReqid.Value = "1";
                        hdnReqid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        Appreciation_id.Text = hdnReqid.Value;
                    }
                    PopulateEmployeeData();
                   
                    int ss = Request.QueryString.Count;
                    //string b  =Convert.ToString(Request.QueryString[0]).Trim();
                    if (Request.QueryString.Count > 0)
                    {
                        hdnBankDetail_ID.Value = Convert.ToString(Request.QueryString[0]).Trim();
                       
                        // mobile_cancel.Visible = true;
                        //mobile_btnSave.Text = "Modify";
                        //mobile_btnSave.ToolTip = "Modify";
                       // lblheading.Text = "View JD Bank";
                    }
                    if (Request.QueryString.Count == 2)
                    {
                        hdnFilter.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        mobile_btnSave.Visible = false;
                        

                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

    public void PopulateEmployeeData()
    {
        try
        {
            DataSet dtEmp = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "Draft_data";
            spars[1] = new SqlParameter("@Appreciation_id", SqlDbType.VarChar);
            spars[1].Value = hdnReqid.Value;
            dtEmp = spm.getDatasetList(spars, "Appreciation_Letter");
            if (dtEmp.Tables[0].Rows.Count > 0)
            {
                hdnReqid.Value = Appreciation_id.Text;
                txt_lettertype.Text = (string)dtEmp.Tables[0].Rows[0]["Category"];
                txt_app_letter.Text = (string)dtEmp.Tables[0].Rows[0]["Appreciation_Letter"];
                txt_point.Text = (string)dtEmp.Tables[0].Rows[0]["point"];
                txt_sub.Text = (string)dtEmp.Tables[0].Rows[0]["letter_sub"];
                txtDescription.Text = (string)dtEmp.Tables[0].Rows[0]["draft"];
               // rbtnActive.Text = (string)dtEmp.Tables[0].Rows[0]["isActive"];

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }


    protected void mobile_btnSave_Click(object sender, EventArgs e)
    { 
        try
        {
            #region Check For Blank Fields

            lblmessage.Text = "";

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

             

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                lblmessage.Text = "Please enter the Appreciation Draft.";
                return;
            }
            if (Convert.ToString(txtDescription.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter the Appreciation Draft.";
                return;
            }


            StringBuilder sb = new StringBuilder();
            sb.Append(Convert.ToString(txtDescription.Text).Trim());

            if (Convert.ToString(sb).Trim() == "")
            {
                lblmessage.Text = "Please enter the Appreciation Draft.";
                return;
            }

            divtext.InnerHtml = sb.ToString();
            if (Convert.ToString(divtext.InnerHtml).Trim() == "")
            {
                lblmessage.Text = "Please enter the Appreciation Draft.";
                return;
            }

            if (string.IsNullOrWhiteSpace(Convert.ToString(divtext.InnerHtml)))
            {
                lblmessage.Text = "Please enter the Appreciation Draft.";
                return;
            }

            if (IsAppreciationDraftIsEmpty(txtDescription.Text) == false)
            {
                lblmessage.Text = "Please enter the Appreciation Draft.";
                return;
            }


            string qtype = "Appreciation_updateDraft";

            DataSet dsgoal = new DataSet();
            SqlParameter[] spars = new SqlParameter[7];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = qtype;

            spars[1] = new SqlParameter("@Appreciation_id", SqlDbType.Int);
            spars[1].Value = Convert.ToString(Appreciation_id.Text);

            spars[2] = new SqlParameter("@Appreciation_Letter", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(txt_app_letter.Text);

            spars[3] = new SqlParameter("@draft", SqlDbType.VarChar);
            spars[3].Value = Convert.ToString(txtDescription.Text.Trim());

            spars[4] = new SqlParameter("@point", SqlDbType.VarChar);
            spars[4].Value = Convert.ToString(txt_point.Text); 

            spars[5] = new SqlParameter("@letter_sub", SqlDbType.VarChar);
            spars[5].Value = Convert.ToString(txt_sub.Text);

            spars[6] = new SqlParameter("@isActive", SqlDbType.VarChar); 
            if (rbtnActive.Checked)
            {
                spars[6].Value = "A";  // Active
            }
            else if (rbtnDeactive.Checked)
            {
                spars[6].Value = "D";  // Deactive
            }


            dsgoal = spm.getDatasetList(spars, "Appreciation_Letter");

            Response.Redirect("~/procs/Appreciation_old_letter.aspx");

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

    private bool IsAppreciationDraftIsEmpty(string stext)
    {
        if (string.IsNullOrWhiteSpace(stext))
        {
            lblmessage.Text = "Please enter the Appreciation Draft.";
            return false;
        }

        // Remove HTML tags
        string content = Regex.Replace(stext, "<.*?>", "").Trim();

        // Also remove non-breaking spaces (&nbsp;) and other whitespace
        content = Regex.Replace(content, @"\s+", "").Replace("&nbsp;", "").Trim();

        if (string.IsNullOrWhiteSpace(content))
        {
            lblmessage.Text = "Please enter the Appreciation Draft.";
            return false;
        }

        return true;

    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/Appreciation_old_letter.aspx");
    }


    #endregion

    #region PageMethods



    #endregion
}