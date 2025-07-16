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

public partial class Req_Questionnaire_Create : System.Web.UI.Page
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



                    GetSkillsetName();
                    GetPositionName();
                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["QuestionnaireDocumentpath"]).Trim());
                    mobile_cancel.Visible = false;
                    if (Request.QueryString.Count > 0)
                    {
                        hdnQuest_ID.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        GetReq_QuestionnaireDetails();
						// mobile_cancel.Visible = true;
						mobile_btnSave.Text = "Modify";
						mobile_btnSave.ToolTip = "Modify";
						lblheading.Text = "View Questionnaire";
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
        string filename = "";
        string strfileName = "", IsActive="0";
        int Quest_ID = 0;
        string Stype = "INSERT";
        DataTable dtQuestio;
        try
        {
            #region Check For Blank Fields
            if (Convert.ToString(lstSkillset.SelectedValue).Trim() == "" || Convert.ToString(lstSkillset.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Skill Set";
                return;
            }
            if (Convert.ToString(lstPositionName.SelectedValue).Trim() == "" || Convert.ToString(lstPositionName.SelectedValue).Trim() == "0")
            {
                lblmessage.Text = "Please select Position Title";
                return;
            }
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            if (uploadfile.HasFile)
            {
                filename = uploadfile.FileName;
            }

            if (Convert.ToString(lnkuplodedfile.Text).Trim() == "")
            {
                if (Convert.ToString(filename).Trim() == "")
                {
                    lblmessage.Text = "Please upload Questionnaire File!";
                    return;
                }
            }
            else
            {
                if (Convert.ToString(filename).Trim() != "")
                {
                    string file = "";
                    file = (Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["QuestionnaireDocumentpath"]).Trim()), lnkuplodedfile.Text.ToString().Trim()));

                    if ((System.IO.File.Exists(file)))
                    {
                        System.IO.File.Delete(file);
                    }
                }

            }
            #endregion
            if (Convert.ToString(filename).Trim() != "")
            {
                filename = uploadfile.FileName;
                strfileName = "";
                //var birthDate = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
                // strfileName = lstSkillset.SelectedItem.ToString() + "_" + lstPositionName.SelectedItem.ToString() + "_" + DateTime.Now.ToString("dd-MM-yyyy") + Path.GetExtension(uploadfile.FileName);
                strfileName = lstPositionName.SelectedItem.ToString() + "_" + DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + Path.GetExtension(uploadfile.FileName);
                filename = strfileName;
                uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["QuestionnaireDocumentpath"]).Trim()), strfileName));
            }
            if (chkActive.Checked)//if checked, uncheck it
            {
                IsActive = "1";
            }
           
            filename = filename != "" ? filename : lnkuplodedfile.Text;
            Quest_ID = Convert.ToString(hdnQuest_ID.Value).Trim() != "" ? Convert.ToInt32(hdnQuest_ID.Value) : 0;
            dtQuestio = spm.Insert_Req_Questionnaire_Files(Stype, Convert.ToInt32(lstSkillset.SelectedValue), Convert.ToInt32(lstPositionName.SelectedValue), filename, Quest_ID, IsActive, Convert.ToString(Session["Empcode"]).Trim());
            if (dtQuestio.Rows.Count > 0)
            {
                Response.Redirect("~/procs/Req_Questionnaire_Index.aspx");
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
            int Quest_ID = 0;
            Quest_ID = Convert.ToString(hdnQuest_ID.Value).Trim() != "" ? Convert.ToInt32(hdnQuest_ID.Value) : 0;
            spm.Insert_Req_Questionnaire_Files(Stype, 0, 0, "", Quest_ID,"0", Convert.ToString(Session["Empcode"]).Trim());
            Response.Redirect("~/procs/Req_Questionnaire_Index.aspx");
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
        DataTable dtSkillset = new DataTable();
        dtSkillset = spm.GetRecruitment_SkillsetName();
        if (dtSkillset.Rows.Count > 0)
        {
            lstSkillset.DataSource = dtSkillset;
            lstSkillset.DataTextField = "ModuleDesc";
            lstSkillset.DataValueField = "ModuleId";
            lstSkillset.DataBind();
            lstSkillset.Items.Insert(0, new ListItem("Select Skillset", "0"));

        }
    }
    public void GetPositionName()
    {
        DataTable dtPositionName = new DataTable();
        dtPositionName = spm.GetRecruitment_PositionTitle();
        if (dtPositionName.Rows.Count > 0)
        {
            lstPositionName.DataSource = dtPositionName;
            lstPositionName.DataTextField = "PositionTitle";
            lstPositionName.DataValueField = "PositionTitle_ID";
            lstPositionName.DataBind();
            lstPositionName.Items.Insert(0, new ListItem("Select Position", "0"));
        }
    }
    private void GetReq_QuestionnaireDetails()
    {

        DataSet dsQuestDetails = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "getAssignQuestionnaire_Edit";

            spars[1] = new SqlParameter("@AssignQuestionnaire_ID", SqlDbType.Int);
            spars[1].Value = Convert.ToInt32(hdnQuest_ID.Value);

            //spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            //spars[2].Value = Convert.ToString(txtEmpCode.Text);
            dsQuestDetails = spm.getDatasetList(spars, "SP_Rec_Assign_Questionnaire");

            if (dsQuestDetails.Tables[0].Rows.Count > 0)
            {
                lstSkillset.SelectedValue = Convert.ToString(dsQuestDetails.Tables[0].Rows[0]["ModuleId"]).Trim();
                lstPositionName.SelectedValue = Convert.ToString(dsQuestDetails.Tables[0].Rows[0]["PositionTitle_ID"]).Trim();
                lnkuplodedfile.Text = Convert.ToString(dsQuestDetails.Tables[0].Rows[0]["UploadData"]).Trim();
               // Convert.ToString(hdnQuest_ID.Value).Trim() != "" ? Convert.ToInt32(hdnQuest_ID.Value) : 0;
                if (Convert.ToString(dsQuestDetails.Tables[0].Rows[0]["IsActive"]).Trim()!="1" )
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
