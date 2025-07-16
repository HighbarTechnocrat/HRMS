using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_CustsService_Req_View : System.Web.UI.Page
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
                Response.Redirect(ReturnUrl("sitepathmain") + "SessionEnd.aspx");
            }


            //mobile_btnPrintPV.Visible = false;

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Custs_Service");
            }
            else
            {

                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    txt_SPOCComment.Attributes.Add("maxlength", "500");
                    FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["CustomerServiceRequestPathpath"]).Trim());
                    TXT_AssginmentDate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                    editform.Visible = true;
                    divbtn.Visible = false;
                    divmsg.Visible = false;
                    hdnempcode.Value = Convert.ToString(Session["Empcode"]);

                    if (Request.QueryString.Count > 0)
                    {
                        hdnRemid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdnRemid_Type.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        var getType = Convert.ToString(Request.QueryString[1]).Trim();
                       
                            var empCode = Convert.ToString(Session["Empcode"]).Trim();
                            //var getSPOCStatus = spm.GETSPOCEXISTS(empCode);
                            var getSPOCStatus = Check_IsLoginEmployee_AsSPOC(empCode);
                            if (getSPOCStatus.Rows.Count > 0)
                            {
                                var Message = Convert.ToString(getSPOCStatus.Rows[0]["Message"]);
                                if (Message == "YES")
                                {
                                    empShow.Visible = true;
                                    empShow2.Visible = true;
                                    empShow12.Visible = true;
                                    ddlCategory.Enabled = true;
                                    divsh.Visible = false;
                                    ddlDepartment.Enabled = true;
                                }
                                else
                                {
                                    lblAssgineTitle.InnerText = "Action Details";
                                    lblActionAssginDate.InnerText = "Action Date";
                                    lblActionAssginComment.InnerText = "Action Description";
                                    empShow.Visible = false;
                                    empShow2.Visible = false;
                                    empShow12.Visible = false;
                                }
                            }
                            else
                            {
                                empShow.Visible = false;
                                empShow2.Visible = false;
                                empShow12.Visible = false;
                            }
                            backToSPOC.Visible = true;

                        GetServiceRequestDetails(hdnRemid.Value, hdnempcode.Value);

                        if (getType == "arr")
                        {
                            empShow.Visible = false;
                            empShow2.Visible = false;
                            empShow12.Visible = false;
                            empShow1.Visible = false;
                            empShow12.Visible = false;
                            empShow.Visible = false;
                            empShow2.Visible = false;
                            empShow3.Visible = false;
                            empShow4.Visible = false;
                            empShow5.Visible = false;
                            empShow7.Visible = false;
                            backToEmployee.Visible = false;
                            backToSPOC.Visible = false;
                            backToArr.Visible = true;


                        }

                    }

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                    empShow12.Visible = false;
                    empShow.Visible = false;
                    empShow1.Visible = false;
                    empShow33.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    //Close Service Request TO SPOC,HOD,CEO and Assgine By
   

    private DataTable GetSPOCData(string id)
    {
        DataTable dttrInbox = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CustomerService_GETSPOCDETAIL";

            spars[1] = new SqlParameter("@id", SqlDbType.Int);
            spars[1].Value = Convert.ToInt32(id);
            dttrInbox = spm.getDataList(spars, "SP_INSERTSERVICE_REQUEST");

        }
        catch (Exception ex)
        {
        }

        return dttrInbox;
    }
  
    public string ReplaceInvalidChars(string filename)
    {
        Regex illegalInFileName = new Regex(@"[#%&{}\!/<'>@+*?$|=]");
        string myString = illegalInFileName.Replace(filename, "_");
        //return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        return myString;
    }
   
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList list = (DropDownList)sender;
            string value = (string)list.SelectedValue;
            if (value == "0")
            {
                ddl_AssginmentEMP.Items.Clear();
                ddlCategory.Items.Clear();
                ddl_AssginmentEMP.DataSource = null;
                ddl_AssginmentEMP.DataBind();

                ddlCategory.DataSource = null;
                ddlCategory.DataBind();
            }
            else
            {
                ddl_AssginmentEMP.Items.Clear();
                ddlCategory.Items.Clear();
                ddl_AssginmentEMP.DataSource = null;
                ddl_AssginmentEMP.DataBind();

                ddlCategory.DataSource = null;
                ddlCategory.DataBind();
                //BindEmpDDL(value);
                BindCategoryDDL(value);
                //var dptId = Convert.ToInt32(value);
                //var getDepartment = spm.GetEmpDepartment(dptId);
                //if (getDepartment.Rows.Count > 0)
                //{
                //    //DataTable dtleaveInbox = new DataTable();
                //    //dtleaveInbox = spm.GetSerivesRequestDepartment();
                //    ddl_AssginmentEMP.DataSource = getDepartment;
                //    //ddlDepartment.DataBind();
                //    ddl_AssginmentEMP.DataTextField = "EmployeeName";
                //    ddl_AssginmentEMP.DataValueField = "Emp_Code";
                //    ddl_AssginmentEMP.DataBind();
                //    ddl_AssginmentEMP.Items.Insert(0, new ListItem("Select Assigned To Employee", "0")); //updated code
                //}
                //else
                //{
                //    ddl_AssginmentEMP.DataSource = null;
                //}
            }

        }
        catch (Exception)
        {

            throw;
        }
    }

    public void BindEmpDDL(string departmentId)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            // spars[0].Value = "GET_Locations_employeeList";
            spars[0].Value = "GET_employeeList";

            spars[1] = new SqlParameter("@departmentName", SqlDbType.VarChar);
            spars[1].Value = hdnCompcode.Value;


            dsTrDetails = spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                ddl_AssginmentEMP.DataSource = dsTrDetails.Tables[0];
                ddl_AssginmentEMP.DataTextField = "EmployeeName";
                ddl_AssginmentEMP.DataValueField = "Emp_Code";
                ddl_AssginmentEMP.DataBind();
            }
            ddl_AssginmentEMP.Items.Insert(0, new ListItem("Select Assigned To Employee", "0"));
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    public string getLocation_PM_Emails(string id, string sempcpde)
    {
        string sCCmails = "";
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CustomerService_GETCC_Emails";

            spars[1] = new SqlParameter("@id", SqlDbType.Int);
            spars[1].Value = Convert.ToInt32(id);

            spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(sempcpde);

            dsTrDetails = spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                sCCmails = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["ccmails"]);
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

        return sCCmails;
    }



    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //hdnIsExceletd.Value = "0";
            //DataSet ds = new DataSet();

            ImageButton btn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            var id = Convert.ToString(gvServiceHistory.DataKeys[row.RowIndex].Values[0]).Trim();
            var getDetails = spm.CustomerService_GETHISTORYBYID(Convert.ToInt32(id));
            if (getDetails.Rows.Count > 0)
            {

                var getDate = Convert.ToString(getDetails.Rows[0]["ActionDate"].ToString());
                var getDescripation = Convert.ToString(getDetails.Rows[0]["ServicesDescripation"].ToString());
                var getFileName = Convert.ToString(getDetails.Rows[0]["FilePath"].ToString());
                var getAssignedDate = Convert.ToString(getDetails.Rows[0]["AssignedDate"].ToString());
                var getAssignedBY = Convert.ToString(getDetails.Rows[0]["AssignedBY"].ToString());

                var getActiondBY = Convert.ToString(getDetails.Rows[0]["ActionBy"].ToString());
                var getStatusName = Convert.ToString(getDetails.Rows[0]["StatusName"].ToString());


                txt_AssgimentShowDate.Text = getDate;
                txt_Service_Description.Text = getDescripation;

                bindFilePath.InnerText = getFileName;
                txt_ASDate.Text = getStatusName;
                txtActionBy.Text = getActiondBY;
                txt_Assigne_By.Text = getAssignedBY;
                txtActionBy.Text = getActiondBY;
                if (getStatusName == "Closed")
                {
                    txt_Assigne_By.Text = "";



                }
                else if (getStatusName == "Not Satisfied")
                {
                    txt_Assigne_By.Text = "";
                    // txt_ASDate.Text = "Pending";
                }

                DataSet dsfiles = getServiceLogFiles(Convert.ToInt32(id));
                liUploadedFiles3.Visible = false;
                liUploadedFiles4.Visible = false;
                gvuploadedFiles_History.DataSource = null;
                gvuploadedFiles_History.DataBind();
                if (dsfiles != null)
                {
                    gvuploadedFiles_History.DataSource = dsfiles.Tables[0];
                    gvuploadedFiles_History.DataBind();
                    liUploadedFiles3.Visible = true;
                    liUploadedFiles4.Visible = true;
                }

            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message.ToString();
        }
    }

    //
    public void GetServiceRequestDetails(string id, string empCode)
    {
        try
        {
            hdnIsExceletd.Value = "0";
            DataSet ds = new DataSet();
            //  ds = spm.getServiceRequestDetail(Convert.ToInt32(id), empCode);

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "CustomerService_GETApprovelPageDetail";

            spars[1] = new SqlParameter("@id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDecimal(hdnRemid.Value);

            spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(empCode);

            ds = spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");

            var departmentId = ds.Tables[3].Rows[0]["ServiceDepartment"].ToString();
            hdnDepartmentID.Value = ds.Tables[3].Rows[0]["ServiceDepartment"].ToString();
            hdnCategoryId.Value = ds.Tables[3].Rows[0]["CategoryId"].ToString();
            hdnCurrentAssgineEMP.Value = ds.Tables[3].Rows[0]["AssignedTo"].ToString();



            //Assgine Emp Data
            if (ds.Tables[0].Rows.Count > 0)
            {
                var empdetail = ds.Tables[0];
                txtEmpName.Text = Convert.ToString(empdetail.Rows[0]["Emp_Name"]);
                Txt_DeptName.Text = Convert.ToString(empdetail.Rows[0]["Location_name"]);
                txtEmpCode.Text = Convert.ToString(empdetail.Rows[0]["Emp_Code"]);
                Txt_Designation.Text = Convert.ToString(empdetail.Rows[0]["Designation"]).Trim();
                Txt_EmpEmail.Text = Convert.ToString(empdetail.Rows[0]["Emp_Emailaddress"]);
                Txt_EmpMobile.Text = Convert.ToString(empdetail.Rows[0]["mobile"]);
                hdnCompcode.Value = Convert.ToString(empdetail.Rows[0]["comp_code"]);
                txtFirstDeacription.Text = Convert.ToString(empdetail.Rows[0]["CustomerServiceDescription"]).Replace("<br><br>", "\n");
            }
            //Assigne  Service History
            gvServiceHistory.DataSource = null;
            gvServiceHistory.DataBind();
            if (ds.Tables[1].Rows.Count > 0)
            {
                var rowsCount = ds.Tables[1].Rows.Count;

                var getServiceLog = ds.Tables[1];
                //Asgine Value For History detail
                var getRowData = ds.Tables[1].Rows[rowsCount - 1];

                var getDate = Convert.ToString(getRowData["ActionDate"].ToString());
                var getDescripation = Convert.ToString(getRowData["ServicesDescripation"].ToString());
                var getFileName = Convert.ToString(getRowData["FilePath"].ToString());
                var getServiceRequestID = Convert.ToString(getRowData["ServicesRequestID"].ToString());
                var getAssignedDate = Convert.ToString(getRowData["AssignedDate"].ToString());
                var getAssignedBY = Convert.ToString(getRowData["AssignedBY"].ToString());
                var getActiondBY = Convert.ToString(getRowData["ActionBy"].ToString());
                var getStatusName = Convert.ToString(getRowData["StatusName"].ToString());
                var getAssignedBY_Code = Convert.ToString(getRowData["AssignedTo"].ToString());
                txt_SPOCComment.Text = getDescripation;
                var getFristRowData = ds.Tables[1].Rows[0];
                var getFiestAssignedDate = Convert.ToString(getFristRowData["AssignedDate"].ToString());

                var getFirstDescription = Convert.ToString(getFristRowData["ServicesDescripation"].ToString());

                var getFirstCategory = Convert.ToString(getFristRowData["CategoryTitle"].ToString());
                var getFirstFilePath = Convert.ToString(getFristRowData["FilePath"].ToString());
                txtFirstCreatedDate.Text = getFiestAssignedDate;
                lblCreateFile.InnerText = getFirstFilePath;

               // txtFirstDeacription.Text = getFirstDescription;
                TxtSystemTargetDate.Text = Convert.ToString(getFristRowData["System_TargetDate"].ToString());

                if (Convert.ToString(Session["Empcode"]).Trim() == ds.Tables[5].Rows[0]["HOD"].ToString())
                {
                    txtNewtargetDate.Text = Convert.ToString(ds.Tables[3].Rows[0]["New_TargetDatee"].ToString());
                    txtNewtargetDate.Enabled = false;
                  
                    empShow5.Visible = false;
                }

                txt_category.Text = getFirstCategory;
                hdnServiceRequestNo.Value = getServiceRequestID;
                lblheading.Text = "CUSTOMERFIRST Service request - " + getServiceRequestID;
                txt_AssgimentShowDate.Text = getDate;
                txt_Service_Description.Text = getDescripation;
                bindFilePath.InnerText = getFileName;
                txt_Assigne_By.Text = getAssignedBY;
                txt_Assign_By_EmpCode.Text = getAssignedBY_Code;
                txt_ASDate.Text = getStatusName;
                txtActionBy.Text = getActiondBY;

                if (Convert.ToString(getFileName).Trim() == "")
                {
                    liUploadedFiles3.Visible = false;
                    liUploadedFiles4.Visible = false;
                }
                if (Convert.ToString(getFirstFilePath).Trim() == "")
                {
                    liuploadedFiles1.Visible = false;
                    liuploadedFiles2.Visible = false;
                }

                DataSet dsfiles = getServiceLogFiles(Convert.ToInt32(getRowData["Id"].ToString()));
                if (dsfiles != null)
                {
                    gvuploadedFiles_History.DataSource = dsfiles;
                    gvuploadedFiles_History.DataBind();

                    if (dsfiles.Tables[0].Rows.Count > 0)
                    {
                        liUploadedFiles3.Visible = true;
                        liUploadedFiles3.Visible = true;
                    }
                }

                if (getStatusName == "Closed")
                {
                    txt_Assigne_By.Text = "";

                }
                else if (getStatusName == "Not Satisfied")
                {
                    txt_Assigne_By.Text = "";
                    // txt_ASDate.Text = "Pending";
                }
                var rows2 = from row in ds.Tables[1].AsEnumerable()
                            where row.Field<string>("StatusName") == "Closed"
                            select row;

                int count2 = rows2.Count<DataRow>();
                if (count2 == 1)
                {
                    hdnIsExceletd.Value = "1";//Escelate to HOD
                }
                else if (count2 == 2)
                {
                    hdnIsExceletd.Value = "2";//Escelate to CEO
                }
                else
                {
                    hdnIsExceletd.Value = "0";//
                }

                if (getServiceLog.Rows.Count > 1)
                {
                    // DataTable selectedTable = getServiceLog.AsEnumerable().Where(r => r.Field<string>("ReceivedDate") != null).CopyToDataTable();
                    gvServiceHistory.DataSource = getServiceLog;
                    gvServiceHistory.DataBind();
                }
                else
                {
                    gvServiceHistory.DataSource = getServiceLog;
                    gvServiceHistory.DataBind(); ;
                }

            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                var getEmpDetail = ds.Tables[2];
                var getCat = ds.Tables[4];
                loadDropDownEmp(getEmpDetail, departmentId);
                var selectedDepartment = getEmpDetail.AsEnumerable().Where(r => r.Field<int>("DepartmentId") == Convert.ToInt32(departmentId)).Select(s => s.Field<string>("DepartmentName")).FirstOrDefault();
                txtSelectedDepartment.Text = selectedDepartment;
                getFillDropdownlist();
                // BindCategoryDDL(departmentId);
                ddlCategory.SelectedValue = Convert.ToString(hdnCategoryId.Value);
                BindEmpDDL(Convert.ToString(hdnCompcode.Value));
                ddl_AssginmentEMP.SelectedValue = txt_Assign_By_EmpCode.Text;
                var getCatogary = getCat.AsEnumerable().Where(r => r.Field<int>("Id") == Convert.ToInt32(Convert.ToString(hdnCategoryId.Value))).Select(s => s.Field<string>("CategoryTitle")).FirstOrDefault();
                txt_category.Text = getCatogary;
            }
            if (ds.Tables[3].Rows.Count > 0)
            {
                var departID = Convert.ToInt32(ds.Tables[3].Rows[0]["ServiceDepartment"].ToString());

                foreach (DataRow drow in ds.Tables[2].Rows)
                {
                    var value = Convert.ToInt32(drow["DepartmentId"].ToString());
                    if (departID == value)
                        hdnDepartmentName.Value = drow["DepartmentName"].ToString();
                }

                var getType = Convert.ToString(Request.QueryString[1]).Trim();
                var getService = ds.Tables[3];
                var getStatusCEO = Convert.ToInt32(getService.Rows[0]["Status"].ToString());

                if (getType == "emp")
                {



                    var getStatus = Convert.ToInt32(getService.Rows[0]["Status"].ToString());
                    if (getStatus == 3)
                    {
                        lblAssgineTitle.InnerText = "Escalation Details";
                        lblActionAssginDate.InnerText = "Escalation Date";
                        lblActionAssginComment.InnerText = "Escalation Description";
                        var getDayes = 8;
                        var diffDay = 0.00;
                        var getTodayDate = DateTime.Now.Date;
                        var lastupdatedDate = Convert.ToDateTime(getService.Rows[0]["AssignedDate"].ToString()).Date;//AssignedDate
                        //Call Day Count
                        string officeLocation = "HO-NaviMum";
                        var getDayesdt = spm.GetEscalationServiceCount(officeLocation, lastupdatedDate, getTodayDate);
                        if (getDayesdt.Rows.Count > 0)
                        {
                            diffDay = Convert.ToInt32(getDayesdt.Rows[0]["WORKINGDAY"]);
                        }
                        else
                        {
                            diffDay = (getTodayDate - lastupdatedDate).TotalDays;
                        }
                        var getDepartment = Convert.ToInt32(hdnDepartmentID.Value);
                        DataTable dtSPOC = spm.GetSPOCData(getDepartment);
                        if (dtSPOC.Rows.Count > 0)
                        {
                            getDayes = Convert.ToInt32(dtSPOC.Rows[0]["USER_ESCALATION"]);
                        }
                        if (getDayes >= diffDay)
                        {
                            empShow1.Visible = true;
                            empShow3.Visible = true;
                            divsh.Visible = true;
                            empShow4.Visible = true;
                            empShow5.Visible = true;
                            var rows = from row in ds.Tables[1].AsEnumerable()
                                       where row.Field<string>("StatusName") == "Closed"
                                       select row;

                            var rowsCEO = from row in ds.Tables[1].AsEnumerable() where row.Field<string>("StatusName") == "Escalated - CEO" select row;
                            var rowsHead = from row in ds.Tables[1].AsEnumerable() where row.Field<string>("StatusName") == "Escalated - HR Head(Auto)" || row.Field<string>("StatusName") == "Escalated - IT Head(Auto)" select row;

                            int count = rows.Count<DataRow>();
                            int countCEOStatus = rowsCEO.Count<DataRow>();
                            int countHeadStatus = rowsHead.Count<DataRow>();


                            // int numberOfRecords = ds.Tables[1].Select("StatusName = Closed").Length;
                            if (countCEOStatus == 1)
                            {
                                empShow1.Visible = false;
                                empShow12.Visible = false;
                                empShow.Visible = false;
                                empShow2.Visible = false;
                                empShow3.Visible = false;
                                empShow4.Visible = false;
                                empShow5.Visible = false;
                                empShow7.Visible = false;
                            }
                            else if (countHeadStatus == 1)
                            {
                                hdnIsExceletd.Value = "2";//Escelate to CEO
                               

                                lblAssgineTitle.InnerText = "Bad Service Feedback";
                                lblActionAssginDate.InnerText = "Feedback Date";
                                lblActionAssginComment.InnerText = "Feedback Description";
                            }
                            else if (count == 1)
                            {
                                hdnIsExceletd.Value = "1";//Escelate to HOD
                               
                            }
                            else if (count == 2)
                            {
                                hdnIsExceletd.Value = "2";//Escelate to CEO

                                lblAssgineTitle.InnerText = "Bad Service Feedback";
                                lblActionAssginDate.InnerText = "Feedback Date";
                                lblActionAssginComment.InnerText = "Feedback Description";
                            }
                            else
                            {
                                empShow1.Visible = false;
                                empShow12.Visible = false;
                                empShow.Visible = false;
                                empShow2.Visible = false;
                                empShow3.Visible = false;
                                empShow4.Visible = false;
                                empShow5.Visible = false;
                                empShow7.Visible = false;
                            }
                        }
                        else
                        {
                            empShow1.Visible = false;
                            empShow12.Visible = false;
                            empShow.Visible = false;
                            empShow2.Visible = false;
                            empShow3.Visible = false;
                            empShow4.Visible = false;
                            empShow5.Visible = false;
                            empShow7.Visible = false;
                           
                        }

                    }
                    else
                    {
                        empShow1.Visible = false;
                        empShow12.Visible = false;
                        empShow.Visible = false;
                        empShow2.Visible = false;
                        empShow3.Visible = false;
                        empShow4.Visible = false;
                        empShow5.Visible = false;
                        empShow7.Visible = false;
                       
                    }
                }

               
            }
            // Bind Depart Ment

            //if (ds.Tables[3].Rows[0]["New_TargetDatee"].ToString() != "")
            //{
            //    txtNewtargetDate.Enabled = false;
            //    txtNewtargetDate.Text = ds.Tables[3].Rows[0]["New_TargetDatee"].ToString();
            //    if ("01-01-1900" == txtNewtargetDate.Text)
            //    {
            //        txtNewtargetDate.Text = "";
            //        txtNewtargetDate.Visible = false;
            //        SpanNewTargetDate.Visible = false;
            //    }
            //}
            if (ds.Tables[3].Rows[0]["New_TargetDatee"].ToString() != "")
            {

                txtNewtargetDate.Text = ds.Tables[3].Rows[0]["New_TargetDatee"].ToString();
                if ("01-01-1900" == txtNewtargetDate.Text)
                {
                    txtNewtargetDate.Text = "";
                    txtNewtargetDate.Enabled = true;
                    txtNewtargetDate.Visible = false;
                    SpanNewTargetDate.Visible = false;
                }
                else
                {
                    txtNewtargetDate.Enabled = false;
                    
                }
                if (hdnempcode.Value == ds.Tables[3].Rows[0]["RequestApprover"].ToString())
                {
                    txtNewtargetDate.Enabled = false;
                }

            }
            if (ds.Tables[3].Rows[0]["Status"].ToString() == "7" || ds.Tables[3].Rows[0]["Status"].ToString() == "8")
            {
               
                HdnStatusIDServicelast.Value = "CSCEO";
                txtNewtargetDate.Enabled = false;
                
            }
            if (ds.Tables[7].Rows.Count > 0)
            {
                TxtApproverComment.Text = ds.Tables[7].Rows[0]["Remarks"].ToString();
                TxtApproverComment.Visible = true;
                SpanApproverComment.Visible = true;
                TxtApproverComment.Enabled = false;
            }

            txt_SPOCComment.Enabled = false;
            
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();
            throw;
        }

    }
    public void loadDropDownEmp(DataTable dtleaveInbox, string id)
    {
        //DataTable dtleaveInbox = new DataTable();
        //dtleaveInbox = spm.GetSerivesRequestDepartment();
        ddlDepartment.DataSource = dtleaveInbox;
        //ddlDepartment.DataBind();
        ddlDepartment.DataTextField = "DepartmentName";
        ddlDepartment.DataValueField = "DepartmentId";
        ddlDepartment.SelectedValue = id;
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem("Select Assigned To Department", "0")); //updated code
        //BindEmpDDL(id);
    }


    #endregion

    #region PageMethods

    public DataSet getServiceLogFiles(int slogid)
    {

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "CustomerService_LogFiles";

        spars[1] = new SqlParameter("@id", SqlDbType.Int);
        spars[1].Value = slogid;

        DataSet dslist = spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");

        return dslist;

    }

    private DataTable Check_IsLoginEmployee_AsSPOC(string empCode)
    {
        DataTable dttrInbox = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "Check_CustomerService_PM_SPOC";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = empCode;
            dttrInbox = spm.getDataList(spars, "SP_INSERTSERVICE_REQUEST");

        }
        catch (Exception ex)
        {
        }

        return dttrInbox;
    }

    private void getPayementVoucher_forPrint()
    {
        try
        {


            #region get payment Voucher details
            DataSet dspaymentVoucher = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "mobile_claim";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToDecimal(hdnRemid.Value);

            dspaymentVoucher = spm.getDatasetList(spars, "rpt_dataProcedure");

            #endregion

            if (dspaymentVoucher.Tables[0].Rows.Count > 0)
            {
                ReportViewer ReportViewer1 = new ReportViewer();


                ReportViewer1.LocalReport.Refresh();
                ReportViewer1.LocalReport.DataSources.Clear();

                ReportParameter[] param = new ReportParameter[19];
                param[0] = new ReportParameter("pdocno", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Vouno"]));
                param[1] = new ReportParameter("ppvdate", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Rem_Sub_Date"]));
                param[2] = new ReportParameter("pempName", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Emp_Name"]));
                param[3] = new ReportParameter("pempCode", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Empcode"]));
                param[4] = new ReportParameter("pRsinwords", Convert.ToString(dspaymentVoucher.Tables[2].Rows[0]["AmountinWords"]));
                param[7] = new ReportParameter("pBankName", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["tperiod"]));

                #region Cost Cente & Bank Details
                if (dspaymentVoucher.Tables[3].Rows.Count > 0)
                {
                    param[5] = new ReportParameter("pCostCenterCode", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["Cost_Center"]));
                    param[6] = new ReportParameter("pCostCenterNM", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["Cost_Center_desc"]));

                    //param[7] = new ReportParameter("pBankName", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["BANK_name"]));
                    param[8] = new ReportParameter("pBankAccCode", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["BANK_acc"]));
                    param[9] = new ReportParameter("pBankIFSCcode", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["BANK_ifsc"]));
                    param[10] = new ReportParameter("pBankBranch", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["BANK_Branch"]));

                    param[11] = new ReportParameter("pRbnkName", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["R_BANK_name"]));
                    param[12] = new ReportParameter("pRBnkAccCode", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["R_BANK_acc"]));
                    param[13] = new ReportParameter("pRbnkIFSCcode", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["R_BANK_ifsc"]));
                    param[14] = new ReportParameter("pRbnkBranch", Convert.ToString(dspaymentVoucher.Tables[3].Rows[0]["R_BANK_Branch"]));
                }
                else
                {
                    param[5] = new ReportParameter("pCostCenterCode", Convert.ToString(""));
                    param[6] = new ReportParameter("pCostCenterNM", Convert.ToString(""));

                    //param[7] = new ReportParameter("pBankName", Convert.ToString(""));
                    param[8] = new ReportParameter("pBankAccCode", Convert.ToString(""));
                    param[9] = new ReportParameter("pBankIFSCcode", Convert.ToString(""));
                    param[10] = new ReportParameter("pBankBranch", Convert.ToString(""));

                    param[11] = new ReportParameter("pRbnkName", Convert.ToString(""));
                    param[12] = new ReportParameter("pRBnkAccCode", Convert.ToString(""));
                    param[13] = new ReportParameter("pRbnkIFSCcode", Convert.ToString(""));
                    param[14] = new ReportParameter("pRbnkBranch", Convert.ToString(""));
                }
                #endregion

                param[15] = new ReportParameter("pContact", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["mobile"]));
                param[16] = new ReportParameter("PAlt_Contact", "");
                param[17] = new ReportParameter("pProjectName", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Project_Name"]));
                param[18] = new ReportParameter("pDept_Name", Convert.ToString(dspaymentVoucher.Tables[1].Rows[0]["Dept_Name"]));

                // Create Report DataSource
                ReportDataSource rds = new ReportDataSource("dspaymentVoucher", dspaymentVoucher.Tables[0]);
                ReportDataSource rdsApprs = new ReportDataSource("dspaymentVoucher_Apprs", dspaymentVoucher_Apprs.Tables[0]);

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/PaymentVoucher.rdlc");
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.DataSources.Add(rdsApprs);
                ReportViewer1.LocalReport.SetParameters(param);


                #region Create payment Voucher PDF file
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                DataTable DataTable1 = new DataTable();
                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=paymentVoucherDetails." + extension);
                try
                {
                    Response.BinaryWrite(bytes);
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error while generating PDF.');", true);
                    Console.WriteLine(ex.StackTrace);
                }

                #endregion


            }

        }
        catch (Exception ex)
        {
        }
    }



    public void GetEmployeeDetails()
    {
        try
        {
            DataTable dtEmpDetails = new DataTable();
            dtEmpDetails = spm.GetEmployeeData(txtEmpCode.Text);
            if (dtEmpDetails.Rows.Count > 0)
            {

                hflempName.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
                hflEmpDesignation.Value = Convert.ToString(dtEmpDetails.Rows[0]["DesginationName"]);
                hflEmpDepartment.Value = Convert.ToString(dtEmpDetails.Rows[0]["Department_Name"]);
                hflGrade.Value = Convert.ToString(dtEmpDetails.Rows[0]["Grade"]).Trim();
                hflEmailAddress.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
            }
            //  getApproverdata();  
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();
            throw;
        }
    }

    public void GetEmployeeDetails_loginemployee()
    {
        try
        {
            DataTable dtEmpDetails = new DataTable();
            dtEmpDetails = spm.GetEmployeeData(hdnempcode.Value);
            if (dtEmpDetails.Rows.Count > 0)
            {

                hdnloginemployee_name.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);

            }
            //  getApproverdata();  
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();
            throw;
        }
    }
    public void AssigningSessions()
    {

        //Session["Fromdate"] = txtFromdate_sub.Text;
        Session["ReqEmpCode"] = txtEmpCode.Text;
        Session["Grade"] = hflGrade.Value;

        Session["TrDays"] = hdnTrdays.Value;


    }


    //public void getApproverdata()
    //{

    //    DataTable dtApproverEmailIds = new DataTable();
    //    dtApproverEmailIds = spm.GeTfuelApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToDecimal(hdnRemid.Value));
    //    //IsEnabledFalse (true);
    //    dspaymentVoucher_Apprs.Tables.Add(dtApproverEmailIds);
    //    lstApprover.Items.Clear();
    //    if (dtApproverEmailIds.Rows.Count > 0)
    //    {
    //        foreach (DataRow row in dtApproverEmailIds.Rows)
    //        {
    //            if (row[1].ToString() == "00002726")
    //            {
    //                CEOInList = "Y";
    //            }
    //        }
    //        hdnApprEmailaddress.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["Emp_Emailaddress"]);
    //        hdnApprId.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["APPR_ID"]);

    //        //lstApprover.DataSource = dtApproverEmailIds;
    //        //lstApprover.DataTextField = "names";
    //        //lstApprover.DataValueField = "APPR_ID";
    //        //lstApprover.DataBind();

    //        //DgvApprover.DataSource = null;
    //        //DgvApprover.DataBind();

    //        //if (dtApproverEmailIds.Rows.Count > 0)
    //        //{
    //        //    DgvApprover.DataSource = dtApproverEmailIds;
    //        //    DgvApprover.DataBind();
    //        //}

    //        hflapprcode.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["approver_emp_code"]);
    //    }
    //    else
    //    {
    //        lblmessage.Text = "Since you are not assigned under any approver you can not apply for any type of Travel request, please contact HR";

    //    }
    //}

    //public void getApproverdata_old()
    //{

    //    DataTable dtApproverEmailIds = new DataTable();
    //    dtApproverEmailIds = spm.GeTfuelApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), 0);
    //    //IsEnabledFalse (true);
    //    lstApprover.Items.Clear();
    //    if (dtApproverEmailIds.Rows.Count > 0)
    //    {
    //        hdnApprEmailaddress.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["Emp_Emailaddress"]);
    //        hdnApprId.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["APPR_ID"]);

    //        //lstApprover.DataSource = null;
    //        //lstApprover.DataBind();

    //        lstApprover.DataSource = dtApproverEmailIds;
    //        lstApprover.DataTextField = "Emp_Name";
    //        lstApprover.DataValueField = "APPR_ID";
    //        lstApprover.DataBind();

    //        hflapprcode.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["A_EMP_CODE"]);
    //    }
    //    else
    //    {
    //        lblmessage.Text = "Since you are not assigned under any approver you can not apply for any type of Travel request, please contact HR";

    //    }
    //}

    protected void GetCuurentApprID()
    {

        string Actions = "";
        DataTable dtCApprID = new DataTable();
        dtCApprID = spm.GetCurrentApprIDMobile(Convert.ToInt32(hdnRemid.Value), hdnempcode.Value);
        hdnCurrentApprID.Value = Convert.ToString(dtCApprID.Rows[0]["APPR_ID"]);
        Actions = Convert.ToString(dtCApprID.Rows[0]["Action"]);

        if (Convert.ToString(hdnCurrentApprID.Value).Trim() == "")
        {
            lblmessage.Text = "Acton on this REquest not yet taken by other approvals";
            return;
        }
        else if (Convert.ToString(Actions).Trim() != "Pending")
        {
            lblmessage.Text = "You already actioned for this request";
            return;
        }
    }

    private void getApproverlist()
    {
        DataTable dtapprover = new DataTable();
        dtapprover = spm.GetMobileApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
        lstApprover.Items.Clear();
        if (dtapprover.Rows.Count > 0)
        {
            lstApprover.DataSource = dtapprover;
            lstApprover.DataTextField = "names";
            lstApprover.DataValueField = "names";
            lstApprover.DataBind();

        }
        else
        {
            lblmessage.Text = "There is no request for approver.";
        }
    }

    public void getnextAppIntermediate()
    {
        //Check if Cureent login is Final Approver
        if (Convert.ToString(hdnTravelConditionid.Value) == "1")
        {

        }
        DataTable dsapproverNxt = new DataTable();
        dsapproverNxt = spm.GetMobilelNextApproverDetails(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
        if (dsapproverNxt.Rows.Count > 0)
        {
            hdnNextApprId.Value = Convert.ToString(dsapproverNxt.Rows[0]["APPR_ID"]);
            hdnNextApprCode.Value = Convert.ToString(dsapproverNxt.Rows[0]["A_EMP_CODE"]);
            hdnNextApprName.Value = Convert.ToString(dsapproverNxt.Rows[0]["Emp_Name"]);
            hdnNextApprEmail.Value = Convert.ToString(dsapproverNxt.Rows[0]["Emp_Emailaddress"]);

            //DataTable dtintermediateemail = new DataTable();
            //dtintermediateemail = spm.TravelNextIntermediateName(Convert.ToInt32(hdnCurrentApprID.Value), txtEmpCode.Text);
            //if (dtintermediateemail.Rows.Count > 0)
            //{
            //    hdnIntermediateEmail.Value = (string)dtintermediateemail.Rows[0]["emp_emailaddress"];
            //}
        }
        else
        {
            hdnstaus.Value = "Final Approver";

            //For  Previous approver   
            getPreviousApprovesEmailList();

            //hdnIntermediateEmail.Value = "";
            //DataTable dtPreInt = new DataTable();
            //dtPreInt = spm.FuelPreviousIntermidaterDetails(txtEmpCode.Text, Convert.ToInt32(hdnCurrentApprID.Value));
            //if (dtPreInt.Rows.Count > 0)
            //{

            //    for (int i = 0; i < dtPreInt.Rows.Count; i++)
            //    {
            //        if (Convert.ToString(hdnIntermediateEmail.Value).Trim() == "")
            //        {
            //            hdnIntermediateEmail.Value = Convert.ToString(dtPreInt.Rows[i]["Emp_Emailaddress"]).Trim();
            //        }
            //        else
            //        {
            //            hdnIntermediateEmail.Value += ";" + Convert.ToString(dtPreInt.Rows[i]["Emp_Emailaddress"]).Trim();
            //        }
            //    }
            //}
        }

    }

    private void get_HOD_ACC_CFO_details_ForNextApprover(string strstype)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[5];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            if (Convert.ToString(strstype) == "HOD" || Convert.ToString(strstype) == "RCFO" || Convert.ToString(strstype) == "RACC")
                //if (Convert.ToString(strstype) == "HOD" || Convert.ToString(strstype) == "RCFO" || Convert.ToString(strstype) == "RCOS" || Convert.ToString(strstype) == "RACC")
                spars[0].Value = "get_next_Approver_dtls_MobClaim";
            else
                spars[0].Value = "get_ACC_HOD_isApproved_claim";

            spars[1] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[1].Value = strstype;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            spars[3] = new SqlParameter("@rem_id", SqlDbType.VarChar);
            spars[3].Value = hdnRemid.Value;

            spars[4] = new SqlParameter("@conditiontypeid", SqlDbType.Int);
            spars[4].Value = hdnTravelConditionid.Value;


            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                hdnstaus.Value = "";
                hdnApprovalACCHOD_mail.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();
                hdnApprovalACCHOD_Code.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_code"]).Trim();
                hdnApprovalACCHOD_ID.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_id"]).Trim();
                hdnApprovalACCHOD_Name.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_remarks"]).Trim();

            }
            if (dsTrDetails.Tables[1].Rows.Count > 0)
            {
                if (dsTrDetails.Tables[0].Rows.Count == 0)
                {
                    hdnApprovalACCHOD_mail.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["approver_emp_mail"]).Trim();
                    hdnApprovalACCHOD_Code.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["approver_emp_code"]).Trim();
                    hdnApprovalACCHOD_ID.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["app_id"]).Trim();
                    hdnApprovalACCHOD_Name.Value = Convert.ToString(dsTrDetails.Tables[1].Rows[0]["app_remarks"]).Trim();
                    hdnstaus.Value = "";
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    private void checkisCOSor_ACC_ClaimApproved()
    {


        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            // spars[0].Value = "check_COS_ACC_HOD_isApproved_claim";
            spars[0].Value = "check_COS_ACC_HOD_isApproved_claim_Mobile";


            spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[1].Value = hdnRemid.Value;
            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                for (Int32 irow = 0; irow < dsTrDetails.Tables[0].Rows.Count; irow++)
                {
                    if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Action"]).Trim() == "Pending" && (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_type"]).Trim() == "RCOS" || Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_type"]).Trim() == "RCFO"))
                    {
                        hdnCurrentApprID.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Appr_id"]).Trim();
                        get_HOD_ACC_CFO_details_ForNextApprover("RACC");

                    }

                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    //protected string GetApprove_RejectList()
    //{
    //    StringBuilder sbapp = new StringBuilder();
    //    sbapp.Length = 0;
    //    sbapp.Capacity = 0;
    //    DataTable dtAppRej = new DataTable();
    //    //dtAppRej = spm.GetMobileApproverStatus(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), Convert.ToInt32(hdnTravelConditionid.Value));
    //    dtAppRej = spm.GeTfuelApproverEmailID(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnTravelConditionid.Value), Convert.ToDecimal(hdnRemid.Value));
    //    if (dtAppRej.Rows.Count > 0)
    //    {
    //        sbapp.Append("<table>");
    //        for (int i = 0; i < dtAppRej.Rows.Count; i++)
    //        {
    //            sbapp.Append("<tr>");
    //            sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
    //            sbapp.Append("</tr>");
    //        }
    //        sbapp.Append("</table>");
    //    }
    //    return Convert.ToString(sbapp);
    //}
    private void getPreviousApprovesEmailList()
    {
        DataTable dtPreApp = new DataTable();
        dtPreApp = spm.MobilePreviousApproverDetails(Convert.ToInt32(hdnRemid.Value));
        if (dtPreApp.Rows.Count > 0)
        {

            for (int i = 0; i < dtPreApp.Rows.Count; i++)
            {
                if (Convert.ToString(hflEmailAddress.Value).Trim() == "")
                {
                    hflEmailAddress.Value = Convert.ToString(dtPreApp.Rows[i]["Emp_Emailaddress"]).Trim();
                }
                else
                {
                    hflEmailAddress.Value += ";" + Convert.ToString(dtPreApp.Rows[i]["Emp_Emailaddress"]).Trim();
                }
            }
        }
    }
    protected string getRejectionCorrectionmailList()
    {
        string email_ids = "";
        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.MobileApproverDetails_Rejection_cancellation(txtEmpCode.Text, Convert.ToInt32(hdnRemid.Value), "get_MobileApproverDetails_mail_rejection_correction");
        lstApprover.Items.Clear();
        if (dtApproverEmailIds.Rows.Count > 0)
        {
            for (int irow = 0; irow < dtApproverEmailIds.Rows.Count; irow++)
            {
                if (Convert.ToString(email_ids).Trim() == "")
                    email_ids = Convert.ToString(dtApproverEmailIds.Rows[irow]["Emp_Emailaddress"]).Trim();
                else
                    email_ids = email_ids + ";" + Convert.ToString(dtApproverEmailIds.Rows[irow]["Emp_Emailaddress"]).Trim();
            }
        }

        return email_ids;

    }


    #endregion


    protected void lnkviewfile_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            //Int32 ifileid = Convert.ToInt32(gvuploadedFiles.DataKeys[row.RowIndex].Values[0]);
            LinkButton lnkviewfile = (LinkButton)row.FindControl("lnkviewfile");

            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ClaimsDocumentpath"]).Trim()), lnkviewfile.Text);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
            Response.WriteFile(strfilepath);
            Response.End();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    //protected void mobile_btnPrintPV_Click(object sender, EventArgs e)
    //{
    //    getApproverdata();
    //    getPayementVoucher_forPrint();
    //}
    //Update This Function 20-01-2021

    //protected void chk_exception_CheckedChanged(object sender, EventArgs e)
    //{
    //    //if (chk_exception.Checked)
    //    //    hdnTravelConditionid.Value = "3";
    //    //else
    //    hdnTravelConditionid.Value = "2";
    //    getApproverdata();
    //}
    protected void lnkuplodedfile_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ClaimsDocumentpath"]).Trim()), lnkuplodedfile.Text);

            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
            Response.WriteFile(strfilepath);
            Response.End();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    public void BindCategoryDDL(string departmentId)
    {
        try
        {

            ddl_AssginmentEMP.DataSource = null;
            ddl_AssginmentEMP.DataBind();
            var dptId = Convert.ToInt32(departmentId);
            var getDepartment = spm.GetCategoryDepartment(dptId);
            if (getDepartment.Rows.Count > 0)
            {
                //DataTable dtleaveInbox = new DataTable();
                //dtleaveInbox = spm.GetSerivesRequestDepartment();
                ddlCategory.DataSource = getDepartment;
                //ddlDepartment.DataBind();
                ddlCategory.DataTextField = "CategoryTitle";
                ddlCategory.DataValueField = "Id";
                ddlCategory.DataBind();

                ddlCategory.Items.Insert(0, new ListItem("Select Category", "0")); //updated code
            }
            else
            {
                ddlCategory.DataSource = null;
                ddlCategory.DataBind();
            }


        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            /* DropDownList list = (DropDownList)sender;
             string value = (string)list.SelectedValue;
             if (value == "0")
             {
                 ddl_AssginmentEMP.DataSource = null;
             }
             else
             {
                 BindEmpDDL(value);
             }*/

        }
        catch (Exception)
        {

            throw;
        }
    }


    public void getFillDropdownlist()
    {
        try
        {

            SqlParameter[] spars = new SqlParameter[1];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "getCustomerServiceCategoryList";

            DataSet dslist = spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");


            ddlDepartment.DataSource = dslist.Tables[0];
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentId";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("Select Department", "0"));
            ddlDepartment.SelectedIndex = 1;

            ddlCategory.DataSource = dslist.Tables[1];
            ddlCategory.DataTextField = "CategoryTitle";
            ddlCategory.DataValueField = "Id";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("Select Service Request Category", "0"));

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

   
}