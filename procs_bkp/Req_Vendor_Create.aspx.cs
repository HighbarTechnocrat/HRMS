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

public partial class Req_Vendor_Create : System.Web.UI.Page
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

                      mobile_cancel.Visible = false;
                    if (Request.QueryString.Count > 0)
                    {
                        hdnVenderID.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        GetReq_VendorDetails();
						//  mobile_cancel.Visible = true;
						mobile_btnSave.Text = "Modify";
						mobile_btnSave.ToolTip = "Modify";
						lblheading.Text = "View Vendor";
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        
        int VenderID = 0;
        string Stype = "INSERT", EmpCode, IsActive = "0";
        DataTable dtVendor;
        try
        {
            #region Check For Blank Fields
            if (Convert.ToString(txtVendorName.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter Vendor Name";
                return;
            }
            
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }

            #endregion
            if (chkActive.Checked)//if checked, uncheck it
            {
                IsActive = "1";
            }

            EmpCode = Convert.ToString(Session["Empcode"]).Trim();
            VenderID = Convert.ToString(hdnVenderID.Value).Trim() != "" ? Convert.ToInt32(hdnVenderID.Value) : 0;
            dtVendor = spm.Insert_Req_Vendor_Details(Stype, VenderID, txtVendorName.Text.Trim(), IsActive, EmpCode);
            if (dtVendor.Rows.Count > 0)
            {
                Response.Redirect("~/procs/Req_Vendor_Details.aspx");
            }
            else
            {
                lblmessage.Text = "Record already exists!.";
            }
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
            int VenderID = 0;
            VenderID = Convert.ToString(hdnVenderID.Value).Trim() != "" ? Convert.ToInt32(hdnVenderID.Value) : 0;
            spm.Insert_Req_Vendor_Delete(Stype, VenderID);
            Response.Redirect("~/procs/Req_Vendor_Details.aspx");
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    #endregion
    #region PageMethods
    
    private void GetReq_VendorDetails()
    {

        DataSet dsQuestDetails = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "getEdit";

            spars[1] = new SqlParameter("@VenderID", SqlDbType.Int);
            spars[1].Value = Convert.ToInt32(hdnVenderID.Value);

            //spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            //spars[2].Value = Convert.ToString(txtEmpCode.Text);
            dsQuestDetails = spm.getDatasetList(spars, "SP_Rec_Vendor_Details");

            if (dsQuestDetails.Tables[0].Rows.Count > 0)
            {
                txtVendorName.Text = Convert.ToString(dsQuestDetails.Tables[0].Rows[0]["VenderName"]).Trim();
                // lstPositionName.SelectedValue = Convert.ToString(dsQuestDetails.Tables[0].Rows[0]["PositionTitle_ID"]).Trim();
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