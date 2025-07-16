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

public partial class ABAP_Object_Tracker_Index : System.Web.UI.Page
{
    public string userid;
    SP_Methods spm = new SP_Methods();
    public DataTable dtEmp, dtRectruter;
    public string filename = "", approveremailaddress;

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
            hflEmpCode.Value = Convert.ToString(Session["Empcode"]);
            //  lblmsg.Text =Convert.ToString(Session["Empcode"]); 
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/ABAP_Object_Tracker_Index.aspx");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {


                    GetUserRole();
                    //GetCTMTestCasesApprovalList();
                    //GetABAPPlanApprovalCount();
                    //GetRGSCountforUpdateStatus();
                    //GetFSCountforUpdateStatus();
                    //GetABAPperAcceptCountforUpdateStatus();
                    //GetABAPperCountforUpdateStatus();
                    //GetHBTCountforUpdateStatus();
                    //GetCTMCountforUpdateStatus();
                    //GetUATCountforUpdateStatus();
                    //GetGoLiveCountforUpdateStatus();
                    //get_ABAP_Object_Submitted_Plan_FSDetails();
                    GetCount();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    public void GetUserRole()
    {
        SqlParameter[] sparsd = new SqlParameter[2];
        sparsd[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        sparsd[0].Value = "GetUserRole";

        sparsd[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        sparsd[1].Value = Convert.ToString(Session["Empcode"].ToString());

        DataSet DSUserRole = spm.getDatasetList(sparsd, "SP_ABAPObjectTracking");
        //lnkUpdateStatusRGSFSABAPTest.Text = "";
        //PM Login 
        if (DSUserRole.Tables[0].Rows.Count > 0)
        {
            trPMHead.Visible = true;
            trPMHeadLine.Visible = true;
            tduploadabapobjectplan.Visible = true;
            tdviewabapplanlist.Visible = true;

            td_change_RGSFSHBTCTMConsultant.Visible = true;
            td_changed_RGSFSHBTCTMConsultant.Visible = true;
            td_change_ABAPConsultant.Visible = true;
            td_changed_ABAPConsultant.Visible = true;
            td_change_CTMConsultant.Visible = true;
            td_changed_CTMConsultant.Visible = true;
            td_changestatus_uatsignoff.Visible = true;
            td_changesstatus_golive.Visible = true;
            td_AuditReport.Visible = true;
            td_DelayReport.Visible = true;
            idTRApproverHead.Visible = true;
            idTRApproverHead_Line.Visible = true;
            tdctmtestcaseapproval.Visible = true;
            tdrgsstageapproval.Visible = true;
        }

        //RGS Login
        if (DSUserRole.Tables[1].Rows.Count > 0)
        {
            trConsultantHead.Visible = true;
            trConsultantHeadLine.Visible = true;
            tdviewassingedabapoplan.Visible = true;
            td_changestatus_RGS.Visible = true;

            //td_changestatus_RGSFSHBTCTMConsultant.Visible = true;
            //lnkUpdateStatusRGSFSABAPTest.Text = "Change Status RGS-FS-HBT-CTM Test";
            //Session["lbl_pagehead"] = lnkUpdateStatusRGSFSABAPTest.Text;
        }

        //FS Login
        if (DSUserRole.Tables[2].Rows.Count > 0)
        {
            trConsultantHead.Visible = true;
            trConsultantHeadLine.Visible = true;
            tdviewassingedabapoplan.Visible = true;
            td_changestatus_FS.Visible = true;
            //td_changestatus_RGSFSHBTCTMConsultant.Visible = true;
            //lnkUpdateStatusRGSFSABAPTest.Text = "Change Status RGS-FS-HBT-CTM Test";
            //Session["lbl_pagehead"] = lnkUpdateStatusRGSFSABAPTest.Text;
        }

        //HBT Login
        if (DSUserRole.Tables[3].Rows.Count > 0)
        {
            trConsultantHead.Visible = true;
            trConsultantHeadLine.Visible = true;
            tdviewassingedabapoplan.Visible = true;
            td_changestatus_HBTTesting.Visible = true;

            //td_changestatus_RGSFSHBTCTMConsultant.Visible = true;
            //lnkUpdateStatusRGSFSABAPTest.Text = "Change Status RGS-FS-HBT-CTM Test";
            //Session["lbl_pagehead"] = lnkUpdateStatusRGSFSABAPTest.Text;
        }

        //CTM Login
        if (DSUserRole.Tables[4].Rows.Count > 0)
        {
            td_changestatus_CTMTesting.Visible = true;
            trConsultantHead.Visible = true;
            trConsultantHeadLine.Visible = true;
            tdviewassingedabapoplan.Visible = true;

            //td_changestatus_RGSFSHBTCTMConsultant.Visible = true;
            //lnkUpdateStatusRGSFSABAPTest.Text = "Change Status RGS-FS-HBT-CTM Test";
            //Session["lbl_pagehead"] = lnkUpdateStatusRGSFSABAPTest.Text;
        }

        //ABAPer login
        if (DSUserRole.Tables[5].Rows.Count > 0)
        {
            trConsultantHead.Visible = true;
            trConsultantHeadLine.Visible = true;
            td_changeAcceptNotAccept_ABAP.Visible = true;
            td_changestatus_ABAP.Visible = true;
            tdviewassingedabapoplan.Visible = true;
            td_uploadSourceCode.Visible = true;

            //td_changestatus_RGSFSHBTCTMConsultant.Visible = true;
            //lnkUpdateStatusRGSFSABAPTest.Text = "Change Status ABAP Development";
            //Session["lbl_pagehead"] = lnkUpdateStatusRGSFSABAPTest.Text;
        }

        //PRM Login
        if (DSUserRole.Tables[6].Rows.Count > 0)
        {
            trPRMHead.Visible = true;
            trPRMHeadLine.Visible = true;

            tdViewPRMABAPObjectPlan.Visible = true;
            tdviewabapplanlist.Visible = true;
            tdinbox_planapproval.Visible = true;
            idTRApproverHead.Visible = true;
            idTRApproverHead_Line.Visible = true;
        }


    }
    
    public void GetCount()
    {
        var objemp_code = Convert.ToString(Session["Empcode"]).Trim();
        DataSet dtCount = new DataSet();
        SqlParameter[] spars = new SqlParameter[1];
        spars[0] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[0].Value = objemp_code;

        dtCount = spm.getDatasetList(spars, "SP_Notification_ABAP");
        if (dtCount != null)
        {
            #region  -- Table 1 -- ABAP Plan Approval Count
            if (dtCount.Tables[1].Rows.Count > 0 && Convert.ToInt32(dtCount.Tables[1].Rows[0]["ABAPPlanApprovalCount"]) > 0)
            {
                int strCount = Convert.ToInt32(dtCount.Tables[1].Rows[0]["ABAPPlanApprovalCount"]);
                lnkinboxapproval.Text = "Inbox Plan Approval (" + strCount + ")";
            }
            #endregion


            #region  -- Table 2 -- RGS Pending Count
            if (dtCount.Tables[2].Rows.Count > 0 && Convert.ToInt32(dtCount.Tables[2].Rows[0]["RGSPendingCount"]) > 0)
            {
                int strCount = Convert.ToInt32(dtCount.Tables[2].Rows[0]["RGSPendingCount"]);
                lnkUpdateStatusRGS.Text = "RGS - Update Status (" + strCount + ")";
            }
            #endregion


            #region  -- Table 3 -- FS Pending Count
            if (dtCount.Tables[3].Rows.Count > 0 && Convert.ToInt32(dtCount.Tables[3].Rows[0]["FSPendingCount"]) > 0)
            {
                int strCount = Convert.ToInt32(dtCount.Tables[3].Rows[0]["FSPendingCount"]);
                lnkUpdateStatusFS.Text = "FS - Update Status (" + strCount + ")";
            }
            #endregion


            #region  -- Table 4 -- Functional Acceptance Pending Count
            if (dtCount.Tables[4].Rows.Count > 0 && Convert.ToInt32(dtCount.Tables[4].Rows[0]["FunctionalAcceptancePendingCount"]) > 0)
            {
                int strCount = Convert.ToInt32(dtCount.Tables[4].Rows[0]["FunctionalAcceptancePendingCount"]);
                lnkUpdateAcceptNotAcceptABAP.Text = "Functional Specification Acceptance (" + strCount + ")";
            }
            #endregion


            #region  -- Table 5 -- ABAP Dev Pending Count
            if (dtCount.Tables[5].Rows.Count > 0 && Convert.ToInt32(dtCount.Tables[5].Rows[0]["ABAPDevPendingCount"]) > 0)
            {
                int strCount = Convert.ToInt32(dtCount.Tables[5].Rows[0]["ABAPDevPendingCount"]);
                lnkUpdateStatusABAP.Text = "ABAP Development - Update Status (" + strCount + ")";
            }
            #endregion


            #region  -- Table 6 -- HBTP ending Count
            if (dtCount.Tables[6].Rows.Count > 0 && Convert.ToInt32(dtCount.Tables[6].Rows[0]["HBTPendingCount"]) > 0)
            {
                int strCount = Convert.ToInt32(dtCount.Tables[6].Rows[0]["HBTPendingCount"]);
                lnkUpdateStatusHBTTesting.Text = "HBT Testing - Update Status (" + strCount + ")";
            }
            #endregion


            #region  -- Table 7 -- CTM Pending Count
            if (dtCount.Tables[7].Rows.Count > 0 && Convert.ToInt32(dtCount.Tables[7].Rows[0]["CTMPendingCount"]) > 0)
            {
                int strCount = Convert.ToInt32(dtCount.Tables[7].Rows[0]["CTMPendingCount"]);
                lnkUpdateStatusCTMTesting.Text = "CTM Testing - Update Status (" + strCount + ")";
            }
            #endregion


            #region  -- Table 8 -- CTM Approval Pending Count
            if (dtCount.Tables[8].Rows.Count > 0 && Convert.ToInt32(dtCount.Tables[8].Rows[0]["CTMApprovalCount"]) > 0)
            {
                int strCount = Convert.ToInt32(dtCount.Tables[8].Rows[0]["CTMApprovalCount"]);
                lnkCTMTestCaseUploadApproval.Text = "CTM Approval (" + strCount + ")";
            }
            #endregion

            #region  -- Table 9 -- UAT Pending Count
            if (dtCount.Tables[9].Rows.Count > 0 && Convert.ToInt32(dtCount.Tables[9].Rows[0]["UATPendingCount"]) > 0)
            {
                int strCount = Convert.ToInt32(dtCount.Tables[9].Rows[0]["UATPendingCount"]);
                Link_ChangeStatus_UATSignoff.Text = "UAT Sign-off - Update Status (" + strCount + ")";
            }
            #endregion

            #region  -- Table 10 -- Go Live Pending Count
            if (dtCount.Tables[10].Rows.Count > 0 && Convert.ToInt32(dtCount.Tables[10].Rows[0]["GoLivePendingCount"]) > 0)
            {
                int strCount = Convert.ToInt32(dtCount.Tables[10].Rows[0]["GoLivePendingCount"]);
                Link_ChangeStatus_GoLive.Text = "Go-Live - Update Status (" + strCount + ")";
            }
            #endregion

            #region  -- Table 11 -- RGS Approval Count
            if (dtCount.Tables[11].Rows.Count > 0 && Convert.ToInt32(dtCount.Tables[11].Rows[0]["RGSApprovalCount"]) > 0)
            {
                int strCount = Convert.ToInt32(dtCount.Tables[11].Rows[0]["RGSApprovalCount"]);
                lnkRGSStageApproval.Text = "RGS Approval (" + strCount + ")";
            }
            #endregion

            #region  -- Table 12 -- Source Code Upload Count
            if (dtCount.Tables[12].Rows.Count > 0 && Convert.ToInt32(dtCount.Tables[12].Rows[0]["ABAPDevUploadSourceCodeCount"]) > 0)
            {
                int strCount = Convert.ToInt32(dtCount.Tables[12].Rows[0]["ABAPDevUploadSourceCodeCount"]);
                lnkUploadSourceCode.Text = "Upload Source Code  (" + strCount + ")";
            }
            #endregion
        }
    }
}
