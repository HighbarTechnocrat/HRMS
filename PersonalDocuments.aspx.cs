using System;
using System.Data;
using System.Data.SqlClient;
public partial class PersonalDocuments : System.Web.UI.Page
{
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    SP_Methods spm = new SP_Methods();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        { 
            if (string.IsNullOrEmpty(Convert.ToString(Session["Empcode"])))
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
            hflEmpCode.Value = Convert.ToString(Session["Empcode"]);

            if (!Page.IsPostBack)
            {
                CheckPersonalDocument_Permissions(hflEmpCode.Value);
            }

            
            ////DataTable dtApprovers = new DataTable();
            ////dtApprovers = spm.CheckHOD(Convert.ToString(hflEmpCode.Value).Trim());
            ////if (dtApprovers.Rows.Count > 0)
            ////{
            ////    //REPO_STRU.Visible = true;
            ////    EMP_RPT.Visible = true;
            ////}
            ////else
            ////{

            ////    //REPO_STRU.Visible = false;
            ////    EMP_RPT.Visible = false;
            ////}
            //Add Check CUSTOMERFIRST CHECK IS Available OR NOT- VIDHYADHAR


            //This is for Customer Feedback
           //// CheckCustomerFIRSTAccess();

            // CheckCustomerFIRSTAccess_ServiceRequest();

          //// CheckCustomerFIRSTHOD(); 

            //CheckIsEMPTRequestShow();
           // CheckIsITAssetRequestShow();
           // CheckSalaryStUpdtAccess(); 
            //updateemployeephoto();
           // check_upload_gallery_images_Access();
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }


    private void CheckPersonalDocument_Permissions(string empCode)
    {
        try
        {

        SqlParameter[] spars = new SqlParameter[1];
        spars[0] = new SqlParameter("@emp_code", SqlDbType.VarChar){ Value = empCode };

        DataSet  dsPermissions = spm.getDatasetList(spars, "SP_GetAll_PersonalDocument_Permissions");
            if (dsPermissions != null)
            {
                // Table [0]  CheckCustomerFIRSTAccess_ServiceRequest
                if (dsPermissions.Tables[0].Rows.Count > 0)
                {
                    var loginCode = Convert.ToString(hflEmpCode.Value).Trim();
                    var CEOCode = Convert.ToString(dsPermissions.Tables[0].Rows[0]["EmpCode"]).Trim();
                    CustomerFIRST.Visible = true;
                }

                // Table [1]  CustEscalated_Authorization
                if (dsPermissions.Tables[1].Rows.Count > 0)
                {
                    var loginCode = Convert.ToString(hflEmpCode.Value).Trim();
                    var CEOCode = Convert.ToString(dsPermissions.Tables[1].Rows[0]["EmpCode"]).Trim();
                    CustomerFIRST.Visible = true;
                }

                // Table [2] CheckIsEMPTRequestShow
                if (dsPermissions.Tables[2].Rows.Count > 0)
                {
                    var getStatus = Convert.ToString(dsPermissions.Tables[2].Rows[0]["IsAccess"]);
                    if (getStatus == "SHOW")
                    {
                        EMPTRequest.Visible = true;
                    }
                    else
                    {
                        EMPTRequest.Visible = false;
                    }
                }

                // Table [3] CheckIsITAssetRequestShow
                if (dsPermissions.Tables[3].Rows.Count > 0)
                {
                    var getStatus = Convert.ToString(dsPermissions.Tables[3].Rows[0]["IsAccess"]);
                    if (getStatus == "SHOW")
                    {
                        ITASSETRequest.Visible = true;
                    }
                    else
                    {
                        ITASSETRequest.Visible = false;
                    }
                }

                // Table [4] updateemployeephoto
                if (dsPermissions.Tables[4].Rows.Count > 0)
                {
                    updatephoto.Visible = true;
                }
                else
                {
                    updatephoto.Visible = false;
                }

                // Table [5] check_upload_gallery_images_Access
                if (dsPermissions.Tables[5].Rows.Count > 0)
                {
                    var getStatus = Convert.ToString(dsPermissions.Tables[5].Rows[0]["IsAccess"]);
                    if (getStatus == "SHOW")
                    {
                        gallery_images.Visible = true;
                    }
                }

                // Table [6] CheckSalaryStUpdtAccess
                if (dsPermissions.Tables[6].Rows.Count > 0)
                {
                    divSalStatUpdt.Visible = true;
                }
                else
                {
                    divSalStatUpdt.Visible = false;
                }

                // Table[7] CheckSalaryStUpdtAccess
                if (dsPermissions.Tables[7].Rows.Count > 0)
                {
                    divSalStatUpdt.Visible = true;
                }


                // Table [8] CheckCustomerFIRSTHOD
                //if (dsPermissions.Tables[8].Rows.Count > 0)
                //{
                //    CustomerFIRST.Visible = true;
                //}

                // Table [9] CheckCustomerFIRSTHOD
                //if (dsPermissions.Tables[9].Rows.Count > 0)
                //{
                //    var loginCode = Convert.ToString(hflEmpCode.Value).Trim();
                //    var CEOCode = Convert.ToString(dsPermissions.Tables[9].Rows[0]["Emp_Code"]).Trim();
                //    if (loginCode == CEOCode)
                //    {
                //        CustomerFIRST.Visible = true;
                //    }
                //}

                //   Table[10] CheckCustomerFIRSTAccess
                //if (dsPermissions.Tables[10].Rows.Count > 0)
                //{
                //    var loginCode = Convert.ToString(hflEmpCode.Value).Trim();
                //    var CEOCode = Convert.ToString(dsPermissions.Tables[10].Rows[0]["EmpCode"]).Trim();
                //    CustomerFIRST.Visible = true;
                //}


            }
        }
        catch (Exception)
        {
        }


    }

    //public void updateemployeephoto()
    //{
    //    var getdtDetails = new DataTable();
    //    try
    //    {
    //        SqlParameter[] spars = new SqlParameter[2];
    //        spars[0] = new SqlParameter("@QUERYMODE", SqlDbType.VarChar);
    //        spars[0].Value = "updatephotoaccess";

    //        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
    //        spars[1].Value = Convert.ToString(hflEmpCode.Value);

    //        getdtDetails = spm.getTeamReportAllDDL(spars, "SP_Update_Photo");
    //        if (getdtDetails.Rows.Count > 0)
    //        {
    //            updatephoto.Visible = true;
    //        }
    //        else
    //        {
    //            updatephoto.Visible = false;
    //        }
    //    }
    //    catch (Exception)
    //    {
    //    }
    //}


    //public void CheckCustomerFIRSTAccess()
    //{
    //    try
    //    {
    //        DataTable dtTrDetails = new DataTable();
    //        SqlParameter[] spars = new SqlParameter[3];
    //        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
    //        spars[0].Value = "CustEscalated_Authorization";
    //        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
    //        spars[1].Value = Convert.ToString(hflEmpCode.Value).Trim();
    //        dtTrDetails = spm.getMobileRemDataList(spars, "SP_INSERTCUST_ESCALATION");
    //        if (dtTrDetails.Rows.Count > 0)
    //        {
    //            var loginCode = Convert.ToString(hflEmpCode.Value).Trim();
    //            var CEOCode = Convert.ToString(dtTrDetails.Rows[0]["EmpCode"]).Trim();
    //            CustomerFIRST.Visible = true;
    //        }
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}
    protected void lnk_Attendancereg_Click()
    {
        if (Convert.ToString(Session["Empcode"]).Trim() != "")
        {
            spm.clear_Reimbursement_temp_tables(Convert.ToString(Session["Empcode"]), "DeleteNominationsTemp");
            Response.Redirect("~/procs/Nominations.aspx");
        }

        else
        {
            return;
        }
    }
    //public void CheckCustomerFIRSTHOD()
    //{
    //    try
    //    {
    //        var getData = spm.CheckCustomerFIRSTHOD(Convert.ToString(hflEmpCode.Value).Trim());
    //        if (getData.Rows.Count > 0)
    //        {
    //            CustomerFIRST.Visible = true;
    //        }


    //        var getCEOData = spm.GetCEOEmpCode();
    //        if (getCEOData.Rows.Count > 0)
    //        {
    //            var loginCode = Convert.ToString(hflEmpCode.Value).Trim();
    //            var CEOCode = Convert.ToString(getCEOData.Rows[0]["Emp_Code"]).Trim();
    //            if (loginCode == CEOCode)
    //            {
    //                CustomerFIRST.Visible = true;
    //            }
    //        }
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}
    //public void CheckIsEMPTRequestShow()
    //{
    //    var getdtDetails = new DataTable();
    //    try
    //    {
    //        SqlParameter[] spars = new SqlParameter[3];
    //        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
    //        spars[0].Value = "CheckIsShowLOPReport";

    //        spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
    //        spars[1].Value = Convert.ToString(hflEmpCode.Value);

    //        spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
    //        spars[2].Value = "ETRequest";
    //        getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
    //        if (getdtDetails.Rows.Count > 0)
    //        {
    //            var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
    //            if (getStatus == "SHOW")
    //            {
    //                EMPTRequest.Visible = true;
    //            }
    //            else
    //            {
    //                EMPTRequest.Visible = false;
    //            }
    //        }
    //    }
    //    catch (Exception)
    //    {
    //    }
    //}

    //public void CheckIsITAssetRequestShow()
    //{
    //    var getdtDetails = new DataTable();
    //    try
    //    {
    //        SqlParameter[] spars = new SqlParameter[3];
    //        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
    //        spars[0].Value = "CheckIsShowLOPReport";

    //        spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
    //        spars[1].Value = Convert.ToString(hflEmpCode.Value);

    //        spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
    //        spars[2].Value = "ITAssetsRequest";
    //        getdtDetails = spm.getTeamReportAllDDL(spars, "SP_LOP_Report_Detail");
    //        if (getdtDetails.Rows.Count > 0)
    //        {
    //            var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
    //            if (getStatus == "SHOW")
    //            {
    //                ITASSETRequest.Visible = true;
    //            }
    //            else
    //            {
    //                ITASSETRequest.Visible = false;
    //            }
    //        }
    //    }
    //    catch (Exception)
    //    {
    //    }
    //}

    //public void CheckSalaryStUpdtAccess()
    //{
    //    DataTable dt = new DataTable();

    //    dt = spm.getEmpDetailsForSalaryApproval(hflEmpCode.Value);
    //    if (dt.Rows.Count > 0)
    //    {
    //        divSalStatUpdt.Visible = true;
    //    }
    //    else
    //    {
    //        divSalStatUpdt.Visible = false;
    //    }

    //    DataSet DSConsultants = new DataSet();
    //    SqlParameter[] spars = new SqlParameter[2];

    //    spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
    //    spars[0].Value = "GetEmpDataCheckAcessConsultants";
    //    spars[1] = new SqlParameter("@EMPCode", SqlDbType.VarChar);
    //    spars[1].Value = hflEmpCode.Value;
    //    DSConsultants = spm.getDatasetList(spars, "spGetEmpDetailsForSalary");
    //    if (DSConsultants.Tables[0].Rows.Count > 0)
    //    {
    //        divSalStatUpdt.Visible = true;
    //    }

    //}

    //public void CheckCustomerFIRSTAccess_ServiceRequest()
    //{
    //    try
    //    {
    //        DataTable dtTrDetails = new DataTable();
    //        SqlParameter[] spars = new SqlParameter[3];
    //        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
    //        spars[0].Value = "CustomerService_Access";
    //        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
    //        spars[1].Value = Convert.ToString(hflEmpCode.Value).Trim();
    //        dtTrDetails = spm.getMobileRemDataList(spars, "SP_INSERTSERVICE_REQUEST");
    //        //if (dtTrDetails.Rows.Count > 0)
    //        //{
    //        //    var loginCode = Convert.ToString(hflEmpCode.Value).Trim();
    //        //    var CEOCode = Convert.ToString(dtTrDetails.Rows[0]["EmpCode"]).Trim();
    //        //    CustomerFIRST.Visible = true;
    //        //}
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}

    //public void check_upload_gallery_images_Access()
    //{
    //    var getdtDetails = new DataTable();
    //    try
    //    {
    //        gallery_images.Visible = false;

    //        SqlParameter[] spars = new SqlParameter[2];
    //        spars[0] = new SqlParameter("@QUERYMODE", SqlDbType.VarChar);
    //        spars[0].Value = "CheckIsShow_uploaded_image_page";
    //        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
    //        spars[1].Value = Convert.ToString(hflEmpCode.Value);
    //        getdtDetails = spm.getTeamReportAllDDL(spars, "SP_Gallery");

    //        if (getdtDetails.Rows.Count > 0)
    //        {
    //            var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
    //            if (getStatus == "SHOW")
    //            {
    //                gallery_images.Visible = true;
    //            }
    //        }
    //    }
    //    catch (Exception)
    //    {
    //    }
    //}
}