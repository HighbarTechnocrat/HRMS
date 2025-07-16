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
using System.Linq;

public partial class Stream_CV : System.Web.UI.Page
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
    String CEOInList = "N";
    double YearlymobileAmount = 0;
    #region PageEvents
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            //mobile_btnPrintPV.Visible = false;

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {

                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    // txt_SPOCComment.Attributes.Add("maxlength", "500");
                   // FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ServiceRequestPathpath"]).Trim());
                    //TXT_AssginmentDate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                    editform.Visible = true;
                    divbtn.Visible = false;
                    divmsg.Visible = false;
                    hdnempcode.Value = Convert.ToString(Session["Empcode"]);
                    BindDegreeDetails();

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                    hdnFamilyDetailID.Value = "0";
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

    private void BindDegreeDetails()
    {
        dg_FimalyDetails.DataSource = null;
        dg_FimalyDetails.DataBind();

        var dataTable = spm.getMasterTabledetails("getCVStream", 0);
        if (dataTable != null)
        {
            if (dataTable.Rows.Count > 0)
            {
                dg_FimalyDetails.DataSource = dataTable;
                dg_FimalyDetails.DataBind();
            }
            else
            {

            }
        }
    }
    private void ResetFamilyDetails()
    {
        hdnFamilyDetailID.Value = "0";
        btn_FD_Update.Visible = false;
        btn_FD_Cancel.Visible = false;
        btn_FD_Save.Visible = true;
        txt_FD_Name.Text = "";
        chk_IsActive.Checked = true;
        BindDegreeDetails();
    }
    //End
   
    #endregion
    // Family Details
    protected void btn_FD_Save_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            Label1.Text = "";
            LinkButton btn = (LinkButton)sender;
            if (btn.Text == "Save")
            {
                // var get 
                if (Convert.ToString(txt_FD_Name.Text).Trim() == "")
                {
                    Label1.Text = "Please enter name";
                    return;
                }
                var getName = Convert.ToString(txt_FD_Name.Text).Trim();
                var empCode = Convert.ToString(hdnempcode.Value).Trim();
                var getChkVal = chk_IsActive.Checked;
                var result = spm.InsertUpdateStream(0, empCode, getName, getChkVal);
                if (result == true)
                {
                    ResetFamilyDetails();
                }
                else
                {
                    Label1.Text = "Something went wroung";
                    return;
                }
            }
            else if (btn.Text == "Update")
            {
                // var get 
                if (Convert.ToString(txt_FD_Name.Text).Trim() == "")
                {
                    Label1.Text = "Please enter name";
                    return;
                }
                var getName = Convert.ToString(txt_FD_Name.Text).Trim();
                var empCode = Convert.ToString(hdnempcode.Value).Trim();
                var getId = int.Parse(hdnFamilyDetailID.Value);
                var getChkVal = chk_IsActive.Checked;
                var result = spm.InsertUpdateStream(getId, empCode, getName, getChkVal);
                if (result == true)
                {
                    ResetFamilyDetails();
                }
                else
                {
                    Label1.Text = "Something went wroung";
                    return;
                }
            }
            else if (btn.Text == "Cancel")
            {
                ResetFamilyDetails();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
            return;
        }
    }
    protected void lnk_FD_Edit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            //var getSelectedValues = ddl_Relation.SelectedValue.ToString();
            //ddl_Relation.Items.FindByValue(getSelectedValues).Selected = false;
            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var fId = Convert.ToString(dg_FimalyDetails.DataKeys[row.RowIndex].Values[0]).Trim();
           // var relationId = Convert.ToString(dg_FimalyDetails.DataKeys[row.RowIndex].Values[1]).Trim();
            if (fId != "0")
            {
                var getds = spm.getMasterTabledetails("getCVStream", int.Parse(fId));
                hdnFamilyDetailID.Value = fId;
                if (getds != null)
                {
                    if (getds.Rows.Count > 0)
                    {

                        btn_FD_Save.Visible = false;
                        txt_FD_Name.Text = getds.Rows[0]["Stream"].ToString();

                        var getIsActive = bool.Parse(getds.Rows[0]["IsActive"].ToString());
                        if (getIsActive == true)
                        {
                            chk_IsActive.Checked = true;
                        }
                        else
                        {
                            chk_IsActive.Checked = false;
                        }
                        btn_FD_Update.Visible = true;
                        btn_FD_Cancel.Visible = true;
                    }
                }
            }
            else
            {
                btn_FD_Update.Visible = true;
                btn_FD_Cancel.Visible = true;
                btn_FD_Save.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
            return;
        }
    }
    // End Family Details


    protected void dg_FimalyDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dg_FimalyDetails.PageIndex = e.NewPageIndex;
        this.BindDegreeDetails();
    }
}
