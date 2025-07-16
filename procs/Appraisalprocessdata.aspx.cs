using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls; 


public partial class Appraisalprocessdata : System.Web.UI.Page
{
    #region Creative_Default_methods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;

    SP_Methods spm = new SP_Methods();
    string strempcode = "";
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    protected void lnkcont_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/SampleForm");
    }

    #endregion
    public DataTable dtPOWONo;
    #region Page_Events

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            //Empcode_Appr
            if (Convert.ToString(Session["Empcode_Appr"]).Trim() == "" || Session["Empcode_Appr"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "procs/Appraisal_login.aspx");
            }

            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/appraisalindex");
            }
            else
            {
                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    hdnEmpCode.Value = Session["Empcode"].ToString();
                    get_PMSReport_DropdownList();

                    Get_AppraisalProcess_Report_Access();
                     
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
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
     try
        {
            if(Convert.ToString(lstPMS_Year.SelectedValue).Trim()=="0")
            {
                lblmessage.Text = "Please select Performance Appraisal Year.";
                return;
            }
            lblmessage.Text = "";
            Process_AppraisalData();
        }
        catch(Exception ex)
        {

        } 
    }

     
    protected void mobile_btnBack_Click(object sender, EventArgs e)
    { 
    }

    #endregion

    #region Page Methods

    private void get_PMSReport_DropdownList()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "get_PMS_Years_List"; 

        dsList = spm.ApprgetDatasetList(spars, "SP_Rpt_Appraisal_Process");

        if (dsList.Tables[0].Rows.Count > 0)
        {
            lstPMS_Year.DataSource = dsList.Tables[0];
            lstPMS_Year.DataTextField = "pyear";
            lstPMS_Year.DataValueField = "pyear";
            lstPMS_Year.DataBind();
        }
        lstPMS_Year.Items.Insert(0, new ListItem("Select PMS Appraisal Year", "0"));  

    }

    private void Get_AppraisalProcess_Report_Access()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "rpt_IsAppraisalProcess_Access";   

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCode.Value;

    

        dsList = spm.ApprgetDatasetList(spars, "SP_Rpt_Appraisal_Process");

        if (dsList != null)
        {
            if (dsList.Tables[0].Rows.Count <= 0)
            { 
                Response.Redirect("appraisalindex.aspx");
            }
        }
        else
        {
            Response.Redirect("appraisalindex.aspx");
        }

    }

  

    private void Process_AppraisalData()
    {
        DataSet dsApprovalStatusReport = new DataSet(); 

        #region get Approval Status Report 

        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "rpt_AppraisalProcessData";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCode.Value;

        spars[2] = new SqlParameter("@pyearA", SqlDbType.VarChar);
        spars[2].Value = Convert.ToString(lstPMS_Year.SelectedValue).Trim();
         
        dsApprovalStatusReport = spm.ApprgetDatasetList(spars, "SP_Rpt_Appraisal_Process");
        if (dsApprovalStatusReport != null)
        {
            if (dsApprovalStatusReport.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsApprovalStatusReport.Tables[0].Rows)
                {
                    if (Convert.ToString(dr["Reviewer_performance_Rating"]) != "")
                    {
                        dr["Reviewer_performance_Rating"] = spm.Decrypt(Convert.ToString(dr["Reviewer_performance_Rating"]));
                    }

                    if (Convert.ToString(dr["Reviewer_Performance_Rating_Remarks"]) != "")
                    {
                        dr["Reviewer_Performance_Rating_Remarks"] = spm.Decrypt(Convert.ToString(dr["Reviewer_Performance_Rating_Remarks"]));
                    }

                    if (Convert.ToString(dr["Reviewer_Attributes_Rating_Remarks"])!= "")
                    {
                        dr["Reviewer_Attributes_Rating_Remarks"] = spm.Decrypt(Convert.ToString(dr["Reviewer_Attributes_Rating_Remarks"]));
                    }

                    if (Convert.ToString(dr["Final_Reviewer_Performance_Rating"]) != "")
                    {
                        dr["Final_Reviewer_Performance_Rating"] = spm.Decrypt(Convert.ToString(dr["Final_Reviewer_Performance_Rating"]));
                    }

                    if (Convert.ToString(dr["Final_Reviewer_Performance_Rating_Remarks"]) != "")
                    {
                        dr["Final_Reviewer_Performance_Rating_Remarks"] = spm.Decrypt(Convert.ToString(dr["Final_Reviewer_Performance_Rating_Remarks"]));
                    }

                    if( Convert.ToString(dr["Final_Reviewer_Attributes_Rating_Remarks"]) != "")
                    {
                        dr["Final_Reviewer_Attributes_Rating_Remarks"] = spm.Decrypt(Convert.ToString(dr["Final_Reviewer_Attributes_Rating_Remarks"]));
                    }

                    if (Convert.ToString(dr["Additional_Reviewer_Performance_Rating"]) != "")
                    {
                        dr["Additional_Reviewer_Performance_Rating"] = spm.Decrypt(Convert.ToString(dr["Additional_Reviewer_Performance_Rating"]));
                    }

                    if (Convert.ToString(dr["Additional_Reviewer_Performance_Rating_Remarks"])!= "")
                    {
                        dr["Additional_Reviewer_Performance_Rating_Remarks"] = spm.Decrypt(Convert.ToString(dr["Additional_Reviewer_Performance_Rating_Remarks"]));
                    }

                    if (Convert.ToString(dr["Additional_Reviewer_Attributes_Rating_Remarks"]) != "")
                    {
                        dr["Additional_Reviewer_Attributes_Rating_Remarks"] = spm.Decrypt(Convert.ToString(dr["Additional_Reviewer_Attributes_Rating_Remarks"]));
                    }

                    if (Convert.ToString(dr["Reviewer_Promotion_Rating"]) != "")
                    {
                        dr["Reviewer_Promotion_Rating"] = spm.Decrypt(Convert.ToString(dr["Reviewer_Promotion_Rating"]));
                    }
                    if (Convert.ToString(dr["Reviewer_Promotion_Remarks"]) != "")
                    {
                        dr["Reviewer_Promotion_Remarks"] = spm.Decrypt(Convert.ToString(dr["Reviewer_Promotion_Remarks"]));
                    }
                    if (Convert.ToString(dr["Final_Reviewer_Promotion_Rating"]) != "")
                    {
                        dr["Final_Reviewer_Promotion_Rating"] = spm.Decrypt(Convert.ToString(dr["Final_Reviewer_Promotion_Rating"]));
                    }
                    if (Convert.ToString(dr["Final_Reviewer_Promotion_Remarks"]) != "")
                    {
                        dr["Final_Reviewer_Promotion_Remarks"] = spm.Decrypt(Convert.ToString(dr["Final_Reviewer_Promotion_Remarks"]));
                    }
                    if (Convert.ToString(dr["Additional_Reviewer_Promotion_Rating"]) != "")
                    {
                        dr["Additional_Reviewer_Promotion_Rating"] = spm.Decrypt(Convert.ToString(dr["Additional_Reviewer_Promotion_Rating"]));
                    }
                    if (Convert.ToString(dr["Additional_Reviewer_Promotion_Remarks"]) != "")
                    {
                        dr["Additional_Reviewer_Promotion_Remarks"] = spm.Decrypt(Convert.ToString(dr["Additional_Reviewer_Promotion_Remarks"]));
                    }
                }
            }
        }
       
        dsApprovalStatusReport.AcceptChanges();
        #endregion

        try
        {

            //ReportViewer1.ProcessingMode = ProcessingMode.Local;
            //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/Appraisal_Process_Rpt.rdlc");

            //ReportDataSource rds = new ReportDataSource("DSAppraisal_Process", dsApprovalStatusReport.Tables[0]);

            //ReportViewer1.LocalReport.DataSources.Clear();
            //ReportViewer1.LocalReport.DataSources.Add(rds);
            //ReportViewer1.LocalReport.Refresh();


            //ReportViewer viwer = new ReportViewer();
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/Appraisal_Process_Rpt.rdlc");

           ReportDataSource rds = new ReportDataSource("Appraisal_Process", dsApprovalStatusReport.Tables[0]);
          //  ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DSAppraisal_Process",dsApprovalStatusReport.Tables[0]));
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.Refresh();
        }
        catch (Exception ex)
        {

        }
    }

    

    #endregion
}


