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
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;

public partial class procs_EmployeeReferralView : System.Web.UI.Page
{
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public int did = 0;
    SP_Methods spm = new SP_Methods();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Count > 0)
            {
                hdPostiontitleID.Value = Convert.ToString(Request.QueryString[0]).Trim();
                HDModuleID.Value = Convert.ToString(Request.QueryString[1]).Trim();
            }

            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + "procs/EmployeeReferralView.aspx?Pos_ID=" + hdPostiontitleID.Value + "&modl_ID=" + HDModuleID.Value);
            }
            lblmessage.Text = "";
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" +  "procs/EmployeeReferralView.aspx?Pos_ID=" + hdPostiontitleID.Value + "&modl_ID=" + HDModuleID.Value);
            lblmessage.Visible = true;
            //   
            if (Page.User.Identity.IsAuthenticated == false)
            {
               

                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + "procs/EmployeeReferralView.aspx?Pos_ID="+ hdPostiontitleID.Value + "&modl_ID="+ HDModuleID.Value);
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString.Count > 0)
                    {
                        //hdRecruitment_ReqID.Value = Request.QueryString["Recruitment_ReqID"];
                        //HDModuleID.Value = Request.QueryString["Recruitment_ReqID"];
                        hdPostiontitleID.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        HDModuleID.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        GetecruitmentDetail();
                    }
                    
                    
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    #region  All_Methods

    private void GetecruitmentDetail()
    {

        DataSet dsRecruitmentDetails = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
            spars[0].Value = "GetEmployeeReferralclick";
            spars[1] = new SqlParameter("@Ref_Candidate_ID", SqlDbType.Int);
            spars[1].Value = Convert.ToInt32(hdPostiontitleID.Value);
            spars[2] = new SqlParameter("@ModuleId", SqlDbType.Int);
            spars[2].Value = Convert.ToInt32(HDModuleID.Value);
            dsRecruitmentDetails = spm.getDatasetList(spars, "sp_Ref_CreateCandidate");

            if (dsRecruitmentDetails.Tables[0].Rows.Count > 0)
            {
                txtPostionTitle.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionTitle"]).Trim();
                txtNoofPostions.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["position"]).Trim();
                txtSkillRequired.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ModuleDesc"]).Trim();
                txtExpRequired.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Yearsofexp"]).Trim();
                txtJobDescription.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["JobDescription"]).Trim();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    #endregion

}