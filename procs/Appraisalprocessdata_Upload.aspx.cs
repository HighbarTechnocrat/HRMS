using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls; 


public partial class Appraisalprocessdata_Upload : System.Web.UI.Page
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
                    Get_AppraisalProcess_Report_Access();
                    Process_AppraisalData();
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
            if (ploadexpfile.HasFile)
            {
                try
                {
                    string fileName = Path.GetFileName(ploadexpfile.FileName);
                    string fileExtension = Path.GetExtension(fileName); 

                    // Check if the file is an Excel file
                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        //string connectionString = "Your_SQL_Server_Connection_String";
                        //string targetTable = "Your_Target_Table_Name";

                        //string filePath = Server.MapPath("~/AppraisalFiles/AppraisalProcess/") + fileName;
                        //ploadexpfile.SaveAs(filePath);

                        // Read data from the Excel file
                        //  DataTable dt = ReadExcelFile(filePath);

                        string filename = ploadexpfile.FileName;
                        string FilePath = Server.MapPath("~/AppraisalFiles/AppraisalProcess/");
                        bool folderExists = Directory.Exists(FilePath);
                        if (!folderExists)
                        {
                            Directory.CreateDirectory(FilePath);
                        }
                        String InputFile = System.IO.Path.GetExtension(ploadexpfile.FileName);

                        string str = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
                        filename = hdnEmpCode.Value + "_AppraisalProcess_" + str + InputFile;
                        ploadexpfile.SaveAs(Path.Combine(FilePath, filename));
                        string strUplaodedFilepath = FilePath + filename;


                        DataTable dtlist = spm.ReadXCLFileOpenXl(strUplaodedFilepath);

                        bool blnIsEmpCode = false;
                        bool blnIsFinal_Rating = false;
                        bool blnIsFinal_Rating_Remarks = false; 
                        if (dtlist !=null)
                        {
                            if(dtlist.Rows.Count>0)
                            {
                                
                                foreach (DataColumn column in dtlist.Columns)
                                {
                                   if(Convert.ToString(column.ColumnName).Trim()== "Employee Code")
                                    {
                                        blnIsEmpCode = true;
                                    }
                                    if (Convert.ToString(column.ColumnName).Trim() == "Final Rating")
                                    {
                                        blnIsFinal_Rating = true;
                                    }
                                    if (Convert.ToString(column.ColumnName).Trim() == "Final Remarks")
                                    {
                                        blnIsFinal_Rating_Remarks = true;
                                    } 
                                }

                                if(blnIsEmpCode==false)  // && blnIsReviewerPromRec== false && blnIsFinalReviewerPromRec== false && blnIsAddReviewerPromRec== false)
                                {
                                    lblmessage.Text = "Please check Employee Code Header not found.";
                                    return; 
                                }
                                if (blnIsFinal_Rating == false) 
                                {
                                    lblmessage.Text = "Please check Final Rating Header not found.";
                                    return;
                                }
                                if (blnIsFinal_Rating_Remarks == false)   
                                {
                                    lblmessage.Text = "Please check Final Remarks Header not found.";
                                    return;
                                } 

                                foreach (DataRow row in dtlist.Rows)
                                {
                                    string strempcode = "";
                                    string strReviwerPromotionRec= "";
                                    string strFinalReviwerPromotionRec = ""; 
                                    if (Convert.ToString(row["Employee Code"]).Trim()!="")
                                        strempcode = Convert.ToString(row["Employee Code"]).Trim();

                                    if (Convert.ToString(row["Final Rating"]).Trim() != "")
                                        strReviwerPromotionRec = Convert.ToString(row["Final Rating"]).Trim();

                                    if (Convert.ToString(row["Final Remarks"]).Trim() != "")
                                        strFinalReviwerPromotionRec = Convert.ToString(row["Final Remarks"]).Trim();

                                    if (Convert.ToString(strReviwerPromotionRec).Trim() != "")
                                        uploadProcess_AppraisalData(strempcode, strReviwerPromotionRec, strFinalReviwerPromotionRec);
                                }
                            }
                        }

                        lblmessage.Text = "File uploaded and data inserted successfully!";
                    }
                    else
                    {
                        lblmessage.Text = "Please upload a valid Excel file.";
                    }
                }
                catch (Exception ex)
                {
                    lblmessage.Text = "Error: " + ex.Message;
                }
            }
            else
            {
                lblmessage.Text = "Please select an Excel file to upload.";
            }
        }
        catch(Exception ex)
        {

        } 
    }

      
    #endregion

    #region Page Methods

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

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "rpt_AppraisalProcessData";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCode.Value;  
      
        dsApprovalStatusReport = spm.ApprgetDatasetList(spars, "SP_Rpt_Appraisal_Process");
        if (dsApprovalStatusReport != null)
        { 
            try
            {
                if (dsApprovalStatusReport.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsApprovalStatusReport.Tables[0].Rows)
                    {
                        if (Convert.ToString(dr["Final_Rating"]) != "")
                        {
                            dr["Final_Rating"] = spm.Decrypt(Convert.ToString(dr["Final_Rating"]));
                        }

                        if (Convert.ToString(dr["Final_Remarks"]) != "")
                        {
                            dr["Final_Remarks"] = spm.Decrypt(Convert.ToString(dr["Final_Remarks"]));
                        } 
                    }
                }
                  
                //ReportViewer viwer = new ReportViewer();
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/Appraisal_Process_Final_Rpt.rdlc");

                ReportDataSource rds = new ReportDataSource("Appraisal_Process", dsApprovalStatusReport.Tables[0]); 
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception ex)
            {

            }
        }
    }


    private void uploadProcess_AppraisalData(string strempcode,string strFinalRating,string strFinalRemarks)
    {
        DataSet dsApprovalStatusReport = new DataSet();

        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "upload_Appraisal_finalRatings";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = strempcode;

        spars[2] = new SqlParameter("@FinalRating", SqlDbType.VarChar);
        if (Convert.ToString(strFinalRating).Trim() != "")
            spars[2].Value = spm.Encrypt(Convert.ToString(strFinalRating).Trim());
        else
            spars[2].Value = DBNull.Value;


        spars[3] = new SqlParameter("@FinalRating_Remarks", SqlDbType.VarChar);
        if (Convert.ToString(strFinalRemarks).Trim() != "")
            spars[3].Value =spm.Encrypt(Convert.ToString(strFinalRemarks).Trim());
        else
            spars[3].Value = DBNull.Value;


        dsApprovalStatusReport = spm.ApprgetDatasetList(spars, "SP_Rpt_Appraisal_Process");
      
    }

   

    #endregion
}


