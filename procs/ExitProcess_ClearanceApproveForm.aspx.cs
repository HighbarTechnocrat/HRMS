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
using System.Text;
using System.IO;
using System.Linq;

public partial class procs_ExitProcess_ClearanceApproveForm : System.Web.UI.Page
{
    SP_Methods spm = new SP_Methods();
    public DataTable dt, dtExitInterview, dtGetApprover;
    public DataSet ds;
    string Emp_Code;
    public int ResignationID, DeptID;
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
            lblmessage.Text = "";
            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

            Emp_Code = Session["Empcode"].ToString();
            txt1012.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            TextBox1062.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            //txt1051.Attributes.Add("onkeypress", "return noanyCharecters(event);");
            txt1027.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
            txt1032.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
            txt1047.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
            txt1048.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
            txt1056.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
            txt1058.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
            bool hasKeys = Request.QueryString.HasKeys();
            if (hasKeys)
            {
                ResignationID = Convert.ToInt32(Request.QueryString["resignationid"]);
            }

            lblmessage.Visible = true;
            //   
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                Page.SmartNavigation = true;

                if (!Page.IsPostBack)
                {
                   FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ITAttachmentPath"]).Trim());
                   FilePathOther.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ITAttachmentPathAll"]).Trim());
                    hasKeys = Request.QueryString.HasKeys();
                    if (hasKeys)
                    {
                        LoadUserData(Convert.ToInt32(Request.QueryString["resignationid"]));
                        GetBG_PDC_DetailsCheck();
                        //claimmob_btnSubmit.PostBackUrl = "~/procs/ExitProcess_ClearanceInbox.aspx";
                        btnRecBack.Visible = true;
                        if (Request.QueryString["Type"] == "ApprovedView" || Request.QueryString["Type"] == "MyView")
                        {
                            GetClearanceViewData(Convert.ToInt32(Request.QueryString["resignationid"]));
                            SetControl();
                            //claimmob_btnSubmit.PostBackUrl = "~/procs/ExitProcess_ApprovedClearance.aspx";
                            btnRecBack.Visible = false;
                            btnViewBack.Visible = true;
                        }
                        else if (Request.QueryString["Type"] == "Add" && DeptID == 105)
                        {
                            GetClearanceViewData(Convert.ToInt32(Request.QueryString["resignationid"]));
                            SetControl();
                        }
                        //else
                        //{
                        //    GetClearanceViewData(Convert.ToInt32(Request.QueryString["resignationid"]));
                        //    SetControl();
                        //}
                        if (Request.QueryString["Type"] == "Add" && DeptID == 105)
                        {
                            TextBox1061.Enabled = false;
                            TextBox1062.Enabled = false;
                            TextBox1063.Enabled = false;
                            VerifyPendingtaskEmployeeApproval();
                        }

                        if (Request.QueryString["Type"] == "Add" && DeptID == 104)
                        {
                            TextBox1061.Enabled = false;
                            TextBox1062.Enabled = false;
                            TextBox1063.Enabled = false;
                        }

                        if (Request.QueryString["Type"] == "Add" && DeptID == 106)
                        {

                            dtExitInterview = spm.GetClearanceViewDataByResignationID(ResignationID);
                            if (dtExitInterview.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtExitInterview.Rows.Count; i++)
                                {
                                    if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceDeptID"]) == "101")
                                    {
                                        if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "1")
                                        {
                                            txt1011.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                                        }
                                        else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "2")
                                        {
                                            txt1012.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                                        }
                                        else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "3")
                                        {
                                            txt1013.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                                        }
                                        LblCurrentDeptCode.Text = Convert.ToString(dtExitInterview.Rows[i]["Emp_Name"]);
                                        LblCurrentDeptDate.Text = Convert.ToString(dtExitInterview.Rows[i]["CreatedOn"]);
                                    }
                                }
                            }
                            if (txt1011.Text.Trim() == "" && txt1012.Text.Trim() == "" && txt1013.Text.Trim() == "")
                            {
                                divCurrentDept.Visible = false;
                                LblCurrentDept.Visible = false;
                                LblCurrentDeptCode.Visible = false;
                                LblCurrentDeptD.Visible = false;
                                LblCurrentDeptDate.Visible = false;
                            }
                            else
                            {
                                divCurrentDept.Visible = true;
                                txt1011.Enabled = false;
                                txt1012.Enabled = false;
                                txt1013.Enabled = false;
                                LblCurrentDept.Visible = true;
                                LblCurrentDeptCode.Visible = true;
                                LblCurrentDeptD.Visible = true;
                                LblCurrentDeptDate.Visible = true;
                            }
                        }

                        if (Request.QueryString["Type"] == "ApprovedView" && DeptID == 106)
                        {
                            if (txt1011.Text.Trim() == "" && txt1012.Text.Trim() == "" && txt1013.Text.Trim() == "")
                            {
                                divCurrentDept.Visible = false;
                            }
                            else
                            {
                                divCurrentDept.Visible = true;
                            }
                                
                        }
                    }
                    else
                    {
                        //LoadUserData(Session["Empcode"].ToString());
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    public void VerifyPendingtaskEmployeeApproval()
    {
        
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
        spars[0].Value = "GetPendingtaskEmployeeApproval";

        spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCode.Value;

        DataTable tt = spm.getData_FromCode(spars, "spExitProc_ClearanceForm");

        GVEmployeependingTask.DataSource = null;
        GVEmployeependingTask.DataBind();
        if (tt.Rows.Count > 0)
        {
            GVEmployeependingTask.DataSource = tt;
            GVEmployeependingTask.DataBind();
            HDPendingtaskYsNo.Value = "1";
        }
        else
        {
            HDPendingtaskYsNo.Value = "0";
        }
    }

    public void LoadUserData(int ResignationID)
    {
        ds = spm.GetExitClearanceDataByResignationID(ResignationID);
        DataTable dtuser, dtClearanceApprover;
        if (ds.Tables[0].Rows.Count > 0)
        {
            dtuser = ds.Tables[0];
            dtClearanceApprover = ds.Tables[1];

            if (Convert.ToString(dtuser.Rows[0]["ExitClearanceEligible"]) == "Yes")
            {
                bool isAccess = false;
                for (int i = 0; i < dtClearanceApprover.Rows.Count; i++)
                {
                    if (Convert.ToString(dtClearanceApprover.Rows[i]["VerifyBy"]) == Emp_Code || Convert.ToString(dtuser.Rows[0]["Emp_Code"]) == Emp_Code)
                    {
                        isAccess = true;
                        break;
                    }
                    else if (Convert.ToString(dtClearanceApprover.Rows[i]["VerifyBy"]) != Emp_Code)
                    {
                        isAccess = false;
                    }
                }
                if (isAccess == true)
                {
                    txtProjectName.Text = Convert.ToString(dtuser.Rows[0]["emp_projectName"]);
                    txtResignationDate.Text = Convert.ToString(dtuser.Rows[0]["ResignationDate"]);
                    txtName.Text = Convert.ToString(dtuser.Rows[0]["Emp_Name"]);
                    txtDesignationGrade.Text = Convert.ToString(dtuser.Rows[0]["DesginationGrade"]);
                    txtDoJ.Text = Convert.ToString(dtuser.Rows[0]["JoiningDate"]);
                    txtLastWorkingDay.Text = Convert.ToString(dtuser.Rows[0]["LastWorkingDate"]);
                    txtDateRelease.Text = Convert.ToString(dtuser.Rows[0]["ReleaseDate"]);
                    hdnEmpCode.Value = Convert.ToString(dtuser.Rows[0]["Emp_Code"]);

                    for (int i = 0; i < dtClearanceApprover.Rows.Count; i++)
                    {
                        if (Convert.ToString(dtClearanceApprover.Rows[i]["ClearanceDeptID"]) == "101" && Convert.ToString(dtClearanceApprover.Rows[i]["VerifyBy"]) == Emp_Code)
                        {
                            DeptID = 101;
                            hdnDeptId.Value = Convert.ToString(DeptID);
                            
                            divCurrentDept.Visible = true;
                            DivHODCurrentDept.Visible = false;
                            divAccountDept.Visible = false;
                            divAdminDept.Visible = false;
                            divHRDept.Visible = false;
                            divITDept.Visible = false;
                            divHRDept.Visible = false;
                        }
                      else if (Convert.ToString(dtClearanceApprover.Rows[i]["ClearanceDeptID"]) == "106" && Convert.ToString(dtClearanceApprover.Rows[i]["VerifyBy"]) == Emp_Code)
                        {
                            DeptID = 106;
                            hdnDeptId.Value = Convert.ToString(DeptID);

                            //DataRow[] dr = dtClearanceApprover.Select("ClearanceDeptID=101");
                            //if (dr.Length == 1)
                            //{
                            //    divCurrentDept.Visible = true;
                            //}
                            //else
                            //{
                                
                            //}
                            divCurrentDept.Visible = false;
                            DivHODCurrentDept.Visible = true;
                            divAccountDept.Visible = false;
                            divAdminDept.Visible = false;
                            divHRDept.Visible = false;
                            divITDept.Visible = false;
                            divHRDept.Visible = false;
                        }
                        else if (Convert.ToString(dtClearanceApprover.Rows[i]["ClearanceDeptID"]) == "102" && Convert.ToString(dtClearanceApprover.Rows[i]["VerifyBy"]) == Emp_Code)
                        {
                            DeptID = 102;
                            hdnDeptId.Value = Convert.ToString(DeptID);
                            divCurrentDept.Visible = false;
                            DivHODCurrentDept.Visible = false;
                            divAccountDept.Visible = false;
                            divAdminDept.Visible = true;
                            divHRDept.Visible = false;
                            divITDept.Visible = false;
                            divHRDept.Visible = false;
                        }
                        else if (Convert.ToString(dtClearanceApprover.Rows[i]["ClearanceDeptID"]) == "103" && Convert.ToString(dtClearanceApprover.Rows[i]["VerifyBy"]) == Emp_Code)
                        {
                            DeptID = 103;
                            hdnDeptId.Value = Convert.ToString(DeptID);
                            divCurrentDept.Visible = false;
                            DivHODCurrentDept.Visible = false;
                            divAccountDept.Visible = false;
                            divAdminDept.Visible = false;
                            divHRDept.Visible = false;
                            divITDept.Visible = true;
                            divHRDept.Visible = false;
                        }
                        else if (Convert.ToString(dtClearanceApprover.Rows[i]["ClearanceDeptID"]) == "104" && Convert.ToString(dtClearanceApprover.Rows[i]["VerifyBy"]) == Emp_Code)
                        {
                            DeptID = 104;
                            hdnDeptId.Value = Convert.ToString(DeptID);
                            divCurrentDept.Visible = true;
                            DivHODCurrentDept.Visible = true;
                            divAccountDept.Visible = true;
                            divAdminDept.Visible = true;
                            divHRDept.Visible = false;
                            divITDept.Visible = true;

							LblCurrentDept.Visible = true;
							LblCurrentDeptCode.Visible = true;
							LblCurrentDeptD.Visible = true;
							LblCurrentDeptDate.Visible = true;

                            LblHODCurrentDept.Visible = true;
                            LblHODCurrentDeptCode.Visible = true;
                            LblHODCurrentDeptD.Visible = true;
                            LblHODCurrentDeptDate.Visible = true;

                            lbladminName.Visible = true;
							lbladminName1.Visible = true;
							lbladminSubOn.Visible = true;
							lbladminSubOn1.Visible = true;

							lblITDept1.Visible = true;
							lblITDept2.Visible = true;
							lblITDept3.Visible = true;
							lblITDept4.Visible = true;

							divHRDept.Visible = false;
							txt1011.Enabled = false;
							txt1012.Enabled = false;
							txt1013.Enabled = false;
							txt1021.Enabled = false;
							txt1022.Enabled = false;
							txt1023.Enabled = false;
							txt1024.Enabled = false;
							txt1025.Enabled = false;
							txt1026.Enabled = false;
                            txt1027.Enabled = false;
                            txt1028.Enabled = false;
                            txt1031.Enabled = false;
                            txt1032.Enabled = false;
                            txt1033.Enabled = false;
                            
                            GetClearanceViewData(ResignationID);
                        }
                        else if (Convert.ToString(dtClearanceApprover.Rows[i]["ClearanceDeptID"]) == "105" && Convert.ToString(dtClearanceApprover.Rows[i]["VerifyBy"]) == Emp_Code)
                        {
                            DeptID = 105;
                            hdnDeptId.Value = Convert.ToString(DeptID);
                            divCurrentDept.Visible = true;
                            DivHODCurrentDept.Visible = true;
                            divAccountDept.Visible = true;
                            divAdminDept.Visible = true;
                            divHRDept.Visible = true;
                            divITDept.Visible = true;
                            divHRDept.Visible = true;
                        }
                    }
                }
                else
                {
                    lblmessage.Text = "You do not have access to this page.";
                    txtProjectName.Text = "";
                    txtResignationDate.Text = "";
                    txtName.Text = "";
                    txtDesignationGrade.Text = "";
                    txtDoJ.Text = "";
                    txtDateRelease.Text = "";
                    txtLastWorkingDay.Text = "";
                    divCurrentDept.Visible = false;
                    DivHODCurrentDept.Visible = false;
                    divAdminDept.Visible = false;
                    divAccountDept.Visible = false;
                    divHRDept.Visible = false;
                    divITDept.Visible = false;
                    SetControl();
                }

            }
            else
            {
                lblmessage.Text = "You are not eligible to submit Exit Clearance Form.";
                SetControl();
                mobile_btnSave.Visible = false;
            }
        }
    }
    protected void btnVerify_Click(object sender, EventArgs e)
    {

        string selectedValue = "";
        int ExitIntQstID;
        int ExitIntQstOptID;
        int OptionResultValue;
        string ExitIntTextAns = "";
        DeptID = Convert.ToInt32(hdnDeptId.Value);

        if (HDPendingtaskYsNo.Value == "1")
        {
            lblmsgpendingApproval.Visible = true;
            lblmsgpendingApproval.Text = "You can't give the clearance please check the below Request are pending for apprvoal.";
            //string script = "alert('You can't give the clearance please check the below Request are pending for apprvoal.');";
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
            return;
        }
        else
        {
            lblmsgpendingApproval.Visible = false;
        }




        if (DeptID == 101)
        {
            for (int i = 1; i <= 3; i++)
            {
                if (i == 1)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1011.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 2)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1012.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 3)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1013.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
            }

            bool resultFinal = spm.UpdateExitClearanceFormApproverDetails(ResignationID, DeptID, Emp_Code);
        }

        else if (DeptID == 106)
        {
            for (int i = 1; i <= 3; i++)
            {
                if (i == 1)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = TextBox1061.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 2)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = TextBox1062.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 3)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = TextBox1063.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
            }

            bool resultFinal = spm.UpdateExitClearanceFormApproverDetails(ResignationID, DeptID, Emp_Code);
        }
        else if (DeptID == 102)
        {
            for (int i = 1; i <= 9; i++)
            {
                if (i == 1)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1021.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 2)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1022.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 3)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1023.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 4)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1024.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 5)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1025.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 6)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1026.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 7)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1027.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }

                else if (i == 8)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1028.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }

                else if (i == 9)
                {
                    //Upload IT Clearance Files
                    #region insert or upload multiple files
                    string[] strdate;
                    string strfromDate = "";
                    string filename = "";
                    string strfileName = "";
                    var getDate = DateTime.Now.ToString("dd-MM-yyyy");
                    if (FileUpload1.HasFile)
                    {
                        filename = FileUpload1.FileName;

                        strdate = Convert.ToString(getDate).Trim().Split('-');
                        strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);

                        string ITAttachmentPath = "";
                        ITAttachmentPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ITAttachmentPathAll"]).Trim());

                        Boolean blnfile = false;
                        int filecount = 0;
                        int fileuploadcount = 1;
                        filecount = FileUpload1.PostedFiles.Count();
                        foreach (HttpPostedFile postfiles in FileUpload1.PostedFiles)
                        {
                            //Get The File Extension  
                            string filetype = Path.GetExtension(postfiles.FileName);
                            strfileName = "";
                            strfileName = "AdminDept_" + Emp_Code + "_" + strfromDate + "_" + Convert.ToString(fileuploadcount).Trim() + filetype;
                            filename = strfileName;
                            postfiles.SaveAs(Path.Combine(ITAttachmentPath, strfileName));
                            fileuploadcount += 1;
                            SqlParameter[] spars = new SqlParameter[5];
                            spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
                            spars[0].Value = "InsertAll_Files";
                            spars[1] = new SqlParameter("@ResignationID", SqlDbType.Int);
                            spars[1].Value = ResignationID;
                            spars[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                            spars[2].Value = Emp_Code;
                            spars[3] = new SqlParameter("@filepath", SqlDbType.VarChar);
                            spars[3].Value = Convert.ToString(strfileName).Trim();
                            spars[4] = new SqlParameter("@ModeType", SqlDbType.VarChar);
                            spars[4].Value = "AdminDept";
                            DataSet DS = spm.getDatasetList(spars, "spExitProc_ClearanceForm");
                            blnfile = true;
                        }
                    }
                    #endregion
                }

            }

            bool resultFinal = spm.UpdateExitClearanceFormApproverDetails(ResignationID, DeptID, Emp_Code);
        }
        else if (DeptID == 103)
        {
            for (int i = 1; i <= 4; i++)
            {
                if (i == 1)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1031.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 2)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1032.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 3)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1033.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 4)
                {
                    //Upload IT Clearance Files
                    #region insert or upload multiple files
                    string[] strdatee;
                    string strfromDatee = "";
                    string filenamee = "";
                    string strfileNamee = "";
                    var getDatee = DateTime.Now.ToString("dd-MM-yyyy");
                    if (FileUpload2.HasFile)
                    {
                        filenamee = FileUpload2.FileName;

                        strdatee = Convert.ToString(getDatee).Trim().Split('-');
                        strfromDatee = Convert.ToString(strdatee[0]) + Convert.ToString(strdatee[1]) + Convert.ToString(strdatee[2]);

                        string ITAttachmentPath = "";
                        ITAttachmentPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ITAttachmentPathAll"]).Trim());

                        Boolean blnfile = false;
                        int filecount = 0;
                        int fileuploadcount = 1;
                        filecount = FileUpload2.PostedFiles.Count();
                        foreach (HttpPostedFile postfiles in FileUpload2.PostedFiles)
                        {
                            //Get The File Extension  
                            string filetype = Path.GetExtension(postfiles.FileName);
                            strfileNamee = "";
                            strfileNamee = "ITDept_" + Emp_Code + "_" + strfromDatee + "_" + Convert.ToString(fileuploadcount).Trim() + filetype;
                            filenamee = strfileNamee;
                            postfiles.SaveAs(Path.Combine(ITAttachmentPath, strfileNamee));
                            fileuploadcount += 1;
                            SqlParameter[] spars = new SqlParameter[5];
                            spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
                            spars[0].Value = "InsertAll_Files";
                            spars[1] = new SqlParameter("@ResignationID", SqlDbType.Int);
                            spars[1].Value = ResignationID;
                            spars[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                            spars[2].Value = Emp_Code;
                            spars[3] = new SqlParameter("@filepath", SqlDbType.VarChar);
                            spars[3].Value = Convert.ToString(strfileNamee).Trim();
                            spars[4] = new SqlParameter("@ModeType", SqlDbType.VarChar);
                            spars[4].Value = "ITDept";
                            DataSet DS = spm.getDatasetList(spars, "spExitProc_ClearanceForm");
                            blnfile = true;
                        }
                    }
                    #endregion
                }
            }

            bool resultFinal = spm.UpdateExitClearanceFormApproverDetails(ResignationID, DeptID, Emp_Code);
            //Upload IT Clearance Files
            #region insert or upload multiple files
            string[] strdate;
            string strfromDate = "";
            string filename = "";
            string strfileName = "";
            var getDate = DateTime.Now.ToString("dd-MM-yyyy");
            if (uploadfile.HasFile)
            {
                filename = uploadfile.FileName;

                strdate = Convert.ToString(getDate).Trim().Split('-');
                strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
                string ITAttachmentPath = "";
                ITAttachmentPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ITAttachmentPath"]).Trim());

                Boolean blnfile = false;
                int filecount = 0;
                int fileuploadcount = 1;
                filecount = uploadfile.PostedFiles.Count();
                foreach (HttpPostedFile postfiles in uploadfile.PostedFiles)
                {
                    //Get The File Extension  
                    string filetype = Path.GetExtension(postfiles.FileName);
                    strfileName = "";
                    strfileName = Emp_Code + "_" + strfromDate + "_" + Convert.ToString(fileuploadcount).Trim() + filetype;
                    filename = strfileName;
                    postfiles.SaveAs(Path.Combine(ITAttachmentPath, strfileName));
                    spm.InserITAttachmentUploaded_Files(ResignationID, Emp_Code, Convert.ToString(strfileName).Trim());
                    blnfile = true;
                    fileuploadcount += 1;
                }
            }

            #endregion

            //send mail to Accounts Dept-
            // SendMailToAccountsDept(ResignationID);


        }

        else if (DeptID == 104)
        {
            for (int i = 1; i <= 10; i++)
            {
                if (i == 1)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1041.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 2)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1042.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 3)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1043.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 4)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1044.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 5)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1045.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 6)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1046.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 7)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1047.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 8)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1048.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 9)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1049.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 10)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1050.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
            }


            //Upload IT Clearance Files
            #region insert or upload multiple files
            string[] strdatee;
            string strfromDatee = "";
            string filenamee = "";
            string strfileNamee = "";
            var getDatee = DateTime.Now.ToString("dd-MM-yyyy");
            if (FileUpload3.HasFile)
            {
                filenamee = FileUpload3.FileName;

                strdatee = Convert.ToString(getDatee).Trim().Split('-');
                strfromDatee = Convert.ToString(strdatee[0]) + Convert.ToString(strdatee[1]) + Convert.ToString(strdatee[2]);

                string ITAttachmentPath = "";
                ITAttachmentPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ITAttachmentPathAll"]).Trim());

                Boolean blnfile = false;
                int filecount = 0;
                int fileuploadcount = 1;
                filecount = FileUpload3.PostedFiles.Count();
                foreach (HttpPostedFile postfiles in FileUpload3.PostedFiles)
                {
                    //Get The File Extension  
                    string filetype = Path.GetExtension(postfiles.FileName);
                    strfileNamee = "";
                    strfileNamee = "AccountDept_" + Emp_Code + "_" + strfromDatee + "_" + Convert.ToString(fileuploadcount).Trim() + filetype;
                    filenamee = strfileNamee;
                    postfiles.SaveAs(Path.Combine(ITAttachmentPath, strfileNamee));
                    fileuploadcount += 1;
                    SqlParameter[] spars = new SqlParameter[5];
                    spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
                    spars[0].Value = "InsertAll_Files";
                    spars[1] = new SqlParameter("@ResignationID", SqlDbType.Int);
                    spars[1].Value = ResignationID;
                    spars[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                    spars[2].Value = Emp_Code;
                    spars[3] = new SqlParameter("@filepath", SqlDbType.VarChar);
                    spars[3].Value = Convert.ToString(strfileNamee).Trim();
                    spars[4] = new SqlParameter("@ModeType", SqlDbType.VarChar);
                    spars[4].Value = "AccountDept";
                    DataSet DS = spm.getDatasetList(spars, "spExitProc_ClearanceForm");
                    blnfile = true;
                }
            }
            #endregion


            //BG & PDC  Detail
            bool BGResult = spm.InserClearanceForm_BG_PCD_Details(ResignationID, DeptID, Emp_Code, lstBG.SelectedValue, lstPDC.SelectedValue);

            bool resultFinal = spm.UpdateExitClearanceFormApproverDetails(ResignationID, DeptID, Emp_Code);
        }
        else if (DeptID == 105)
        {
            for (int i = 1; i <= 8; i++)
            {
                //if (i == 1)
                //{
                //    ExitIntQstID = i;
                //    ExitIntQstOptID = 0;
                //    OptionResultValue = 0;
                //    ExitIntTextAns = txt1051.Text;
                //    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                //}
                if (i == 1)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1052.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 2)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1053.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 3)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1054.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 4)
                {
                    ExitIntQstID = i;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1055.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 5)
                {
                    // ExitIntQstID = i;
                    ExitIntQstID = 6;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1056.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 6)
                {
                    ExitIntQstID = 7;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1057.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 7)
                {
                    ExitIntQstID = 8;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1058.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
                else if (i == 8)
                {
                    ExitIntQstID = 9;
                    ExitIntQstOptID = 0;
                    OptionResultValue = 0;
                    ExitIntTextAns = txt1059.Text;
                    bool result = spm.InserClearanceFormUserDetails(ResignationID, Emp_Code, DeptID, ExitIntQstID, ExitIntQstOptID, OptionResultValue, ExitIntTextAns);
                }
            }

            //Upload HR Notice period recovery Clearance Files
            #region insert or upload multiple files
            string[] strdatee;
            string strfromDatee = "";
            string filenamee = "";
            string strfileNamee = "";
            var getDatee = DateTime.Now.ToString("dd-MM-yyyy");
            if (FileUpload4.HasFile)
            {
                filenamee = FileUpload4.FileName;

                strdatee = Convert.ToString(getDatee).Trim().Split('-');
                strfromDatee = Convert.ToString(strdatee[0]) + Convert.ToString(strdatee[1]) + Convert.ToString(strdatee[2]);

                string ITAttachmentPath = "";
                ITAttachmentPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ITAttachmentPathAll"]).Trim());
                Boolean blnfile = false;
                int filecount = 0;
                int fileuploadcount = 1;
                filecount = FileUpload4.PostedFiles.Count();
                foreach (HttpPostedFile postfiles in FileUpload4.PostedFiles)
                {
                    //Get The File Extension  
                    string filetype = Path.GetExtension(postfiles.FileName);
                    strfileNamee = "";
                    strfileNamee = "RecHRDept_" + Emp_Code + "_" + strfromDatee + "_" + Convert.ToString(fileuploadcount).Trim() + filetype;
                    filenamee = strfileNamee;
                    postfiles.SaveAs(Path.Combine(ITAttachmentPath, strfileNamee));
                    fileuploadcount += 1;
                    SqlParameter[] spars = new SqlParameter[5];
                    spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
                    spars[0].Value = "InsertAll_Files";
                    spars[1] = new SqlParameter("@ResignationID", SqlDbType.Int);
                    spars[1].Value = ResignationID;
                    spars[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                    spars[2].Value = Emp_Code;
                    spars[3] = new SqlParameter("@filepath", SqlDbType.VarChar);
                    spars[3].Value = Convert.ToString(strfileNamee).Trim();
                    spars[4] = new SqlParameter("@ModeType", SqlDbType.VarChar);
                    spars[4].Value = "RecHRDept";
                    DataSet DS = spm.getDatasetList(spars, "spExitProc_ClearanceForm");
                    blnfile = true;
                }

            }
            #endregion

            //Upload HR Aditional payout Clearance Files
            #region insert or upload multiple files
            string[] strdateee;
            string strfromDateee = "";
            string filenameee = "";
            string strfileNameee = "";
            var getDateee = DateTime.Now.ToString("dd-MM-yyyy");
            if (FileUpload5.HasFile)
            {
                filenameee = FileUpload5.FileName;

                strdateee = Convert.ToString(getDateee).Trim().Split('-');
                strfromDatee = Convert.ToString(strdateee[0]) + Convert.ToString(strdateee[1]) + Convert.ToString(strdateee[2]);

                string ITAttachmentPath = "";
                ITAttachmentPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ITAttachmentPathAll"]).Trim());

                Boolean blnfile = false;

                int filecount = 0;
                int fileuploadcount = 1;
                filecount = FileUpload5.PostedFiles.Count();
                foreach (HttpPostedFile postfiles in FileUpload5.PostedFiles)
                {
                    //Get The File Extension  
                    string filetype = Path.GetExtension(postfiles.FileName);
                    strfileNameee = "";
                    strfileNameee = "PayHRDept_" + Emp_Code + "_" + strfromDateee + "_" + Convert.ToString(fileuploadcount).Trim() + filetype;
                    filenameee = strfileNameee;
                    postfiles.SaveAs(Path.Combine(ITAttachmentPath, strfileNameee));
                    fileuploadcount += 1;
                    SqlParameter[] spars = new SqlParameter[5];
                    spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
                    spars[0].Value = "InsertAll_Files";
                    spars[1] = new SqlParameter("@ResignationID", SqlDbType.Int);
                    spars[1].Value = ResignationID;
                    spars[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                    spars[2].Value = Emp_Code;
                    spars[3] = new SqlParameter("@filepath", SqlDbType.VarChar);
                    spars[3].Value = Convert.ToString(strfileNameee).Trim();
                    spars[4] = new SqlParameter("@ModeType", SqlDbType.VarChar);
                    spars[4].Value = "PayHRDept";
                    DataSet DS = spm.getDatasetList(spars, "spExitProc_ClearanceForm");
                    blnfile = true;
                }

            }
            #endregion


            bool resultFinal = spm.UpdateExitClearanceFormApproverDetails(ResignationID, DeptID, Emp_Code);

            bool result2 = spm.UpdateUserDataInAdminExit(Emp_Code, "UpdateInAdminExitClearance", ResignationID);

            Response.Redirect("ExitProcess_Index.aspx");
        }

        get_Clearnce_Status_Account(ResignationID);// Only send mail verify of IT ,Cuurnet dept ,Admin//

        dtGetApprover = spm.GetExitCleranceApproverStatus(ResignationID);

        if (dtGetApprover.Rows.Count > 1)
        {
            bool Cleared = false;
            for (int i = 0; i < dtGetApprover.Rows.Count; i++)
            {

                if (Convert.ToString(dtGetApprover.Rows[i]["ClearanceDeptID"]) != "105" && Convert.ToString(dtGetApprover.Rows[i]["ClearedAll"]) == "Yes")
                {
                    Cleared = true;
                }
                else
                {
                    if (Convert.ToString(dtGetApprover.Rows[i]["ClearanceDeptID"]) != "105")
                    {
                        Cleared = false;
                        break;
                    }
                    else if (Convert.ToString(dtGetApprover.Rows[i]["ClearanceDeptID"]) == "105" && Convert.ToString(dtGetApprover.Rows[i]["ClearedAll"]) == "NO")
                    {
                        Cleared = false;
                        break;
                    }

                }
            }




            if (Cleared == true)
            {
                string Tomail = "";
                for (int i = 0; i < dtGetApprover.Rows.Count; i++)
                {
                    if (Convert.ToString(dtGetApprover.Rows[i]["ClearanceDeptID"]) == "102")
                    {
                        Tomail = Convert.ToString(dtGetApprover.Rows[i]["Emp_Emailaddress"]) + ";";
                    }
                    if (Convert.ToString(dtGetApprover.Rows[i]["ClearanceDeptID"]) == "103")
                    {
                        Tomail = Convert.ToString(dtGetApprover.Rows[i]["Emp_Emailaddress"]) + ";";
                    }
                    if (Convert.ToString(dtGetApprover.Rows[i]["ClearanceDeptID"]) == "106")
                    {
                        Tomail = Convert.ToString(dtGetApprover.Rows[i]["Emp_Emailaddress"]) + ";";
                    }
                }

                string emp_name = "", LWD = "";
                StringBuilder strbuild = new StringBuilder();
                SqlParameter[] spars = new SqlParameter[2];

                spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                spars[0].Value = "ResignationMail_EmployeeDetails";

                spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                spars[1].Value = hdnEmpCode.Value;

                DataTable tt = spm.getData_FromCode(spars, "SP_ExitProcess");

                string ResigForm_AppLink = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ExitProcess_ClearanceApproveForm.aspx";
                //string ResigForm_AppLink = "http://192.168.21.193/hrms/login.aspx?ReturnUrl=procs/ExitProcess_ClearanceApproveForm.aspx";
                //  string ResigForm_AppLink = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ExitProcess_ClearanceApproveForm.aspx";
                string redirectURL = Convert.ToString(ResigForm_AppLink).Trim() + "?resignationid=" + ResignationID + "&Type=Add";

                if (tt.Rows.Count > 0)
                {
                    strbuild.Length = 0;
                    strbuild.Clear();

                    strbuild.Append("<table border='1'>");

                    foreach (DataRow row in tt.Rows)
                    {
                        if (Convert.ToString(row["emp_location"].ToString()) == "HO-NaviMum")
                        {

                            strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
                            + "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
                            + "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
                            + "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
                            + "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
                            + "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
                            + "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
                            + "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
                            + "</td></tr><tr><td>Reporting Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
                            + "</td></tr><tr><td>HOD: </td><td>" + Convert.ToString(row["DH"].ToString())
                            + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
                            + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
                            + "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
                            + "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
                            + "</td></tr>");
                        }
                        else
                        {
                            strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
                            + "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
                            + "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
                            + "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
                            + "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
                            + "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
                            + "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
                            + "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
                            + "</td></tr><tr><td>Project Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
                            + "</td></tr><tr><td>Delivery Manager: </td><td>" + Convert.ToString(row["DM"].ToString())
                            + "</td></tr><tr><td>Program Manager: </td><td>" + Convert.ToString(row["PRM"].ToString())
                            + "</td></tr><tr><td>Delivery Head: </td><td>" + Convert.ToString(row["DH"].ToString())
                            + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
                            + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
                            + "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
                            + "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
                            + "</td></tr>");
                        }
                        emp_name = Convert.ToString(row["Emp_Name"].ToString());
                        LWD = Convert.ToString(row["LastWorkingDate"].ToString());
                    }
                    strbuild.Append("</table>");
                }

                string strsubject = txtName.Text + " Clearance Form Submitted";
                spm.SendMailToClearanceApprover(Tomail, strsubject, txtName.Text, Convert.ToString(strbuild), "", redirectURL, LWD);
            }

            if (DeptID == 101)
            {
                //string Tomail = "";
                for (int i = 0; i < dtGetApprover.Rows.Count; i++)
                {
                    if (Convert.ToString(dtGetApprover.Rows[i]["ClearanceDeptID"]) == "102")
                    {
                        // Tomail = Convert.ToString(dtGetApprover.Rows[i]["Emp_Emailaddress"]) + ";";
                        VerifyCurrentDeptAftermailSend(Convert.ToString(dtGetApprover.Rows[i]["Emp_Emailaddress"]));
                    }
                    if (Convert.ToString(dtGetApprover.Rows[i]["ClearanceDeptID"]) == "103")
                    {
                        //Tomail = Convert.ToString(dtGetApprover.Rows[i]["Emp_Emailaddress"]) + ";";
                        VerifyCurrentDeptAftermailSend(Convert.ToString(dtGetApprover.Rows[i]["Emp_Emailaddress"]));
                    }
                    if (Convert.ToString(dtGetApprover.Rows[i]["ClearanceDeptID"]) == "106")
                    {
                        // Tomail = Convert.ToString(dtGetApprover.Rows[i]["Emp_Emailaddress"]) + ";";
                        VerifyCurrentDeptAftermailSend(Convert.ToString(dtGetApprover.Rows[i]["Emp_Emailaddress"]));
                    }
                }
            }

            if (DeptID == 104)
            {
                for (int i = 0; i < dtGetApprover.Rows.Count; i++)
                {
                    if (Convert.ToString(dtGetApprover.Rows[i]["ClearanceDeptID"]) == "105")
                    {
                        VerifyCurrentDeptAftermailSend(Convert.ToString(dtGetApprover.Rows[i]["Emp_Emailaddress"]));
                    }
                }
            }
        }
        Response.Redirect("ExitProcess_Index.aspx");

    }

    public void VerifyCurrentDeptAftermailSend(string Tomail)
    {
        string emp_name = "", LWD = "";
        StringBuilder strbuild = new StringBuilder();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "ResignationMail_EmployeeDetails";

        spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCode.Value;

        DataTable tt = spm.getData_FromCode(spars, "SP_ExitProcess");

        string ResigForm_AppLink = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ExitProcess_ClearanceApproveForm.aspx";
        //string ResigForm_AppLink = "http://192.168.21.193/hrms/login.aspx?ReturnUrl=procs/ExitProcess_ClearanceApproveForm.aspx";
        //  string ResigForm_AppLink = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ExitProcess_ClearanceApproveForm.aspx";
        string redirectURL = Convert.ToString(ResigForm_AppLink).Trim() + "?resignationid=" + ResignationID + "&Type=Add";

        if (tt.Rows.Count > 0)
        {
            strbuild.Length = 0;
            strbuild.Clear();

            strbuild.Append("<table border='1'>");

            foreach (DataRow row in tt.Rows)
            {
                if (Convert.ToString(row["emp_location"].ToString()) == "HO-NaviMum")
                {

                    strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
                    + "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
                    + "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
                    + "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
                    + "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
                    + "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
                    + "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
                    + "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
                    + "</td></tr><tr><td>Reporting Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
                    + "</td></tr><tr><td>HOD: </td><td>" + Convert.ToString(row["DH"].ToString())
                    + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
                    + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
                    + "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
                    + "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
                    + "</td></tr>");
                }
                else
                {
                    strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
                    + "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
                    + "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
                    + "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
                    + "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
                    + "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
                    + "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
                    + "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
                    + "</td></tr><tr><td>Project Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
                    + "</td></tr><tr><td>Delivery Manager: </td><td>" + Convert.ToString(row["DM"].ToString())
                    + "</td></tr><tr><td>Program Manager: </td><td>" + Convert.ToString(row["PRM"].ToString())
                    + "</td></tr><tr><td>Delivery Head: </td><td>" + Convert.ToString(row["DH"].ToString())
                    + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
                    + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
                    + "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
                    + "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
                    + "</td></tr>");
                }
                emp_name = Convert.ToString(row["Emp_Name"].ToString());
                LWD = Convert.ToString(row["LastWorkingDate"].ToString());
            }
            strbuild.Append("</table>");
        }
        string strsubject = txtName.Text + " has filled Clearance Form";
        spm.SendMailToClearanceApprover(Tomail, strsubject, txtName.Text, Convert.ToString(strbuild), "", redirectURL, LWD);
    }

    public void GetClearanceViewData(int ResignationID)
    {
        dtExitInterview = spm.GetClearanceViewDataByResignationID(ResignationID);
        if (dtExitInterview.Rows.Count > 0)
        {
            for (int i = 0; i < dtExitInterview.Rows.Count; i++)
            {
                if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceDeptID"]) == "101")
                {
                    if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "1")
                    {
                        txt1011.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "2")
                    {
                        txt1012.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "3")
                    {
                        txt1013.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
					LblCurrentDeptCode.Text = Convert.ToString(dtExitInterview.Rows[i]["Emp_Name"]);
					LblCurrentDeptDate.Text = Convert.ToString(dtExitInterview.Rows[i]["CreatedOn"]);
				}

              else  if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceDeptID"]) == "106")
                {
                    if (txt1011.Text.Trim() == "")
                    {
                        divCurrentDept.Visible = false;
                    }
                    //if (txt1011.Text.Trim() != "")
                    //{
                    //    divCurrentDept.Visible = true;
                    //}

                    if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "1")
                    {
                        TextBox1061.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]); 
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "2")
                    {
                        TextBox1062.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "3")
                    {
                        TextBox1063.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    LblHODCurrentDeptCode.Text = Convert.ToString(dtExitInterview.Rows[i]["Emp_Name"]);
                    LblHODCurrentDeptDate.Text = Convert.ToString(dtExitInterview.Rows[i]["CreatedOn"]);
                }



                else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceDeptID"]) == "102")
                {
                    if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "1")
                    {
                        txt1021.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "2")
                    {
                        txt1022.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "3")
                    {
                        txt1023.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "4")
                    {
                        txt1024.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "5")
                    {
                        txt1025.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "6")
                    {
                        txt1026.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "7")
                    {
                        txt1027.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "8")
                    {
                        txt1028.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }

                    lbladminName1.Text = Convert.ToString(dtExitInterview.Rows[i]["Emp_Name"]);
					lbladminSubOn1.Text = Convert.ToString(dtExitInterview.Rows[i]["CreatedOn"]);
                    ITAdminDeptuploadLI.Visible = false;
                    getAdminUploadedFiles(ResignationID); 

                }
                else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceDeptID"]) == "103")
                {
                    if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "1")
                    {
                        txt1031.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "2")
                    {
                        txt1032.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "3")
                    {
                        txt1033.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }

                    lblITDept2.Text = Convert.ToString(dtExitInterview.Rows[i]["Emp_Name"]);
					lblITDept4.Text = Convert.ToString(dtExitInterview.Rows[i]["CreatedOn"]);
					//-------------view upload files
					getITUploadedFiles(ResignationID);
                    claimmob_uploadf.Visible = false;
                    getITUploadedFiles2(ResignationID);
                    ITDeptLi.Visible = false;
                }
                else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceDeptID"]) == "104")
                {
                    if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "1")
                    {
                        txt1041.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "2")
                    {
                        txt1042.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "3")
                    {
                        txt1043.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "4")
                    {
                        txt1044.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "5")
                    {
                        txt1045.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "6")
                    {
                        txt1046.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }

                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "7")
                    {
                        txt1047.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "8")
                    {
                        txt1048.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "9")
                    {
                        txt1049.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "10")
                    {
                        txt1050.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }


                    //Make IT dept visible to Accounts dept-
                    //if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "1")
                    //{
                    //    txt1031.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    //}
                    //BG Details
                    if (Convert.ToString(dtExitInterview.Rows[i]["IsBGVerify"]) != "")
					{
						lstBG.SelectedValue = Convert.ToString(dtExitInterview.Rows[i]["IsBGVerify"]);
					}
					//PDC Details
					if (Convert.ToString(dtExitInterview.Rows[i]["IsPDCVerify"]) != "")
					{
						lstPDC.SelectedValue = Convert.ToString(dtExitInterview.Rows[i]["IsPDCVerify"]);
					}
					lblAccDept2.Text = Convert.ToString(dtExitInterview.Rows[i]["Emp_Name"]);
					lblAccDept4.Text = Convert.ToString(dtExitInterview.Rows[i]["CreatedOn"]);
					//---view upload files by IT Dept
					divITDept.Visible = true;
					divCurrentDept.Visible = true;
					divAdminDept.Visible = true;
					getITUploadedFiles(ResignationID);
                    claimmob_uploadf.Visible = false;

                    getAccountUploadedFiles(ResignationID);
                    AccountDeptLI.Visible = false;
                }
                else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceDeptID"]) == "105")
                {
                    //if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "1")
                    //{
                    //    txt1051.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    //}
                     if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "1")
                    {
                        txt1052.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "2")
                    {
                        txt1053.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "3")
                    {
                        txt1054.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "4")
                    {
                        txt1055.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }

                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "6")
                    {
                        txt1056.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "7")
                    {
                        txt1057.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "8")
                    {
                        txt1058.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }
                    else if (Convert.ToString(dtExitInterview.Rows[i]["ClearanceQstID"]) == "9")
                    {
                        txt1059.Text = Convert.ToString(dtExitInterview.Rows[i]["ClearanceTextAns"]);
                    }

                    lblHRDept2.Text = Convert.ToString(dtExitInterview.Rows[i]["Emp_Name"]);
					lblHRDept4.Text = Convert.ToString(dtExitInterview.Rows[i]["CreatedOn"]);
                    getHRUploadedFiles1(ResignationID);
                    getHRUploadedFiles2(ResignationID);
                    HrDept1.Visible = false;
                    HRDept2.Visible = false;
                }

            }
        }
    }

    public void SetControl()
    {
        txt1011.Enabled = false;
        txt1012.Enabled = false;
        txt1013.Enabled = false;

        TextBox1061.Enabled = false;
        TextBox1062.Enabled = false;
        TextBox1063.Enabled = false;

        txt1021.Enabled = false;
        txt1022.Enabled = false;
        txt1023.Enabled = false;
        txt1024.Enabled = false;
        txt1025.Enabled = false;
        txt1026.Enabled = false;
        txt1027.Enabled = false;
        txt1028.Enabled = false;

        txt1031.Enabled = false;
        txt1032.Enabled = false;
        txt1033.Enabled = false;
        txt1041.Enabled = false;
        txt1042.Enabled = false;
        txt1043.Enabled = false;
        txt1044.Enabled = false;
        txt1045.Enabled = false;
        txt1046.Enabled = false;

        txt1047.Enabled = false;
        txt1048.Enabled = false;
        txt1049.Enabled = false;
        txt1050.Enabled = false;

        //txt1056.Enabled = false;
        //txt1057.Enabled = false;
        //txt1058.Enabled = false;
        //txt1059.Enabled = false;

        lstBG.Enabled = false;
		lstPDC.Enabled = false;
		LblCurrentDept.Visible = true;
		LblCurrentDeptCode.Visible = true;
		LblCurrentDeptD.Visible = true;
		LblCurrentDeptDate.Visible = true;

        LblHODCurrentDept.Visible = true;
        LblHODCurrentDeptCode.Visible = true;
        LblHODCurrentDeptD.Visible = true;
        LblHODCurrentDeptDate.Visible = true;

        lbladminName.Visible = true;
		lbladminName1.Visible = true;
		lbladminSubOn.Visible = true;
		lbladminSubOn1.Visible = true;

		lblITDept1.Visible = true;
		lblITDept2.Visible = true;
		lblITDept3.Visible = true;
		lblITDept4.Visible = true;

		lblAccDept1.Visible = true;
		lblAccDept2.Visible = true;
		lblAccDept3.Visible = true;
		lblAccDept4.Visible = true;

		

		if (DeptID == 105 && Request.QueryString["Type"] == "Add")
        {
			DataTable Dtresult = spm.GetEmploymentType(hdnEmpCode.Value);
			if (Dtresult.Rows.Count > 0)
			{
				//if (Convert.ToInt32(Dtresult.Rows[0]["EMPLOYMENT_TYPE"]) == 1)
				//{
				//	txt1051.Enabled = true;
				//	RequiredFieldValidator16.Enabled = true;
				//	//HR1.Visible = true;
				//}
				//else
				//{
				//	txt1051.Enabled = false;
				//	//HR1.Visible = false;
				//	RequiredFieldValidator16.Enabled = false;
				//}
			}

			
            txt1052.Enabled = true;
            txt1053.Enabled = true;
            txt1054.Enabled = true;
            txt1055.Enabled = true;

            txt1056.Enabled = true;
            txt1057.Enabled = true;
            txt1058.Enabled = true;
            txt1059.Enabled = true;

            mobile_btnSave.Visible = true;
        }
        else
        {
           // txt1051.Enabled = false;
            txt1052.Enabled = false;
            txt1053.Enabled = false;
            txt1054.Enabled = false;
            txt1055.Enabled = false;

            txt1056.Enabled = false;
            txt1057.Enabled = false;
            txt1058.Enabled = false;
            txt1059.Enabled = false;

            lblHRDept1.Visible = true;
			lblHRDept2.Visible = true;
			lblHRDept3.Visible = true;
			lblHRDept4.Visible = true;

			mobile_btnSave.Visible = false;
            mobile_cancel.Visible = false;
        }

    }

	public void GetBG_PDC_DetailsCheck()
	{
		try
		{
			DataSet dtBGcheck = new DataSet();//EmpCode
			dtBGcheck = spm.GetBG_PDC_Check(hdnEmpCode.Value);
			if (dtBGcheck.Tables[0].Rows.Count > 0)
			{
				liBG.Visible = true;
			}
			if (dtBGcheck.Tables[1].Rows.Count > 0)
			{
				liPDC.Visible = true;
			}

		}
		catch (Exception)
		{

			throw;
		}
	}

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

    public void getITUploadedFiles(int ResignationID)
    {

        DataTable dtTrDetails = new DataTable();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
        spars[0].Value = "GetInsertIT_Files";

        spars[1] = new SqlParameter("@ResignationID", SqlDbType.VarChar);
        spars[1].Value = ResignationID;

        dtTrDetails = spm.getDataList(spars, "spExitProc_ClearanceForm");

        gvuploadedFiles.DataSource = null;
        gvuploadedFiles.DataBind();
        if (dtTrDetails.Rows.Count > 0)
        {
            gvuploadedFiles.DataSource = dtTrDetails;
            gvuploadedFiles.DataBind();
        }
    }

    public void getITUploadedFiles2(int ResignationID)
    {

        DataTable dtTrDetails = new DataTable();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
        spars[0].Value = "GetInsertIT_Files1";

        spars[1] = new SqlParameter("@ResignationID", SqlDbType.VarChar);
        spars[1].Value = ResignationID;

        spars[2] = new SqlParameter("@ModeType", SqlDbType.VarChar);
        spars[2].Value = "ITDept";

        dtTrDetails = spm.getDataList(spars, "spExitProc_ClearanceForm");

        GVITDept.DataSource = null;
        GVITDept.DataBind();
        if (dtTrDetails.Rows.Count > 0)
        {
            GVITDept.DataSource = dtTrDetails;
            GVITDept.DataBind();
        }
    }

    public void getAdminUploadedFiles(int ResignationID)
    {

        DataTable dtTrDetails = new DataTable();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
        spars[0].Value = "GetInsertAdmin_Files";

        spars[1] = new SqlParameter("@ResignationID", SqlDbType.VarChar);
        spars[1].Value = ResignationID;

        spars[2] = new SqlParameter("@ModeType", SqlDbType.VarChar);
        spars[2].Value = "AdminDept";

        dtTrDetails = spm.getDataList(spars, "spExitProc_ClearanceForm");

        GVAdminDept.DataSource = null;
        GVAdminDept.DataBind();
        if (dtTrDetails.Rows.Count > 0)
        {
            GVAdminDept.DataSource = dtTrDetails;
            GVAdminDept.DataBind();
        }
    }

    public void getAccountUploadedFiles(int ResignationID)
    {

        DataTable dtTrDetails = new DataTable();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
        spars[0].Value = "GetInsertAccount_Files";

        spars[1] = new SqlParameter("@ResignationID", SqlDbType.VarChar);
        spars[1].Value = ResignationID;

        spars[2] = new SqlParameter("@ModeType", SqlDbType.VarChar);
        spars[2].Value = "AccountDept";

        dtTrDetails = spm.getDataList(spars, "spExitProc_ClearanceForm");

        GVAccountDept.DataSource = null;
        GVAccountDept.DataBind();
        if (dtTrDetails.Rows.Count > 0)
        {
            GVAccountDept.DataSource = dtTrDetails;
            GVAccountDept.DataBind();
        }
    }

    public void getHRUploadedFiles1(int ResignationID)
    {

        DataTable dtTrDetails = new DataTable();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
        spars[0].Value = "GetInsertRecHRDept_Files";

        spars[1] = new SqlParameter("@ResignationID", SqlDbType.VarChar);
        spars[1].Value = ResignationID;

        spars[2] = new SqlParameter("@ModeType", SqlDbType.VarChar);
        spars[2].Value = "RecHRDept";

        dtTrDetails = spm.getDataList(spars, "spExitProc_ClearanceForm");

        GVHrDept1.DataSource = null;
        GVHrDept1.DataBind();
        if (dtTrDetails.Rows.Count > 0)
        {
            GVHrDept1.DataSource = dtTrDetails;
            GVHrDept1.DataBind();
        }
    }

    public void getHRUploadedFiles2(int ResignationID)
    {

        DataTable dtTrDetails = new DataTable();
        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
        spars[0].Value = "GetInsertPayHRDept_Files";

        spars[1] = new SqlParameter("@ResignationID", SqlDbType.VarChar);
        spars[1].Value = ResignationID;

        spars[2] = new SqlParameter("@ModeType", SqlDbType.VarChar);
        spars[2].Value = "PayHRDept";

        dtTrDetails = spm.getDataList(spars, "spExitProc_ClearanceForm");

        GVHRDept2.DataSource = null;
        GVHRDept2.DataBind();
        if (dtTrDetails.Rows.Count > 0)
        {
            GVHRDept2.DataSource = dtTrDetails;
            GVHRDept2.DataBind();
        }
    }

    public void SendMailToAccountsDept( int ResignationID)
    {
        // hdnEmpCode.Value
        dtGetApprover = spm.getExitCleranceApprover(hdnEmpCode.Value);
        if (dtGetApprover.Rows.Count > 0)
        {
            for (int i = 0; i < dtGetApprover.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtGetApprover.Rows[i]["ClearanceDeptID"]) == 104)
                 {
                    //Get To mail-ids
                    string emp_name = "", LWD = "";
                    StringBuilder strbuild = new StringBuilder();
                    SqlParameter[] spars = new SqlParameter[2];

                    spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                    spars[0].Value = "ResignationMail_EmployeeDetails";

                    spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
                    spars[1].Value = hdnEmpCode.Value;

                    DataTable tt = spm.getData_FromCode(spars, "SP_ExitProcess");

                    string ResigForm_AppLink = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ExitProcess_ClearanceApproveForm.aspx";
                    //string ResigForm_AppLink = "http://192.168.21.193/hrms/login.aspx?ReturnUrl=procs/ExitProcess_ClearanceApproveForm.aspx";
                    //string ResigForm_AppLink = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ExitProcess_ClearanceApproveForm.aspx";
                    string redirectURL = Convert.ToString(ResigForm_AppLink).Trim() + "?resignationid=" + Convert.ToInt32(ResignationID) + "&Type=Add";

                    if (tt.Rows.Count > 0)
                    {
                        strbuild.Length = 0;
                        strbuild.Clear();

                        strbuild.Append("<table border='1'>");

                        foreach (DataRow row in tt.Rows)
                        {
                            if (Convert.ToString(row["emp_location"].ToString()) == "HO-NaviMum")
                            {

                                strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
                                + "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
                                + "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
                                + "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
                                + "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
                                + "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
                                + "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
                                + "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
                                + "</td></tr><tr><td>Reporting Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
                                + "</td></tr><tr><td>HOD: </td><td>" + Convert.ToString(row["DH"].ToString())
                                + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
                                + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
                                + "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
							   + "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
								+ "</td></tr>");
                            }
                            else
                            {
                                strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
                                + "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
                                + "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
                                + "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
                                + "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
                                + "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
                                + "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
                                + "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
                                + "</td></tr><tr><td>Project Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
                                + "</td></tr><tr><td>Delivery Manager: </td><td>" + Convert.ToString(row["DM"].ToString())
                                + "</td></tr><tr><td>Program Manager: </td><td>" + Convert.ToString(row["PRM"].ToString())
                                + "</td></tr><tr><td>Delivery Head: </td><td>" + Convert.ToString(row["DH"].ToString())
                                + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
                                + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
                                + "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
							   + "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
								+ "</td></tr>");
                            }
                            emp_name = Convert.ToString(row["Emp_Name"].ToString());
                            LWD = Convert.ToString(row["LastWorkingDate"].ToString());
                        }
                        strbuild.Append("</table>");
                    }

                    string strsubject = txtName.Text + " has filled Clearance Form";
                    spm.SendMailToClearanceApprover(Convert.ToString(dtGetApprover.Rows[i]["Emp_Emailaddress"]), strsubject, txtName.Text, Convert.ToString(strbuild), "", redirectURL, LWD);
                }
            }
            Response.Redirect("ExitProcess_Index.aspx");
        }
    }

	public void get_Clearnce_Status_Account(int ResignationID)
	{
		try
		{

		DataTable dtTrDetails = new DataTable();
		SqlParameter[] spars = new SqlParameter[2];
		spars[0] = new SqlParameter("@Type", SqlDbType.VarChar);
		spars[0].Value = "GetClearanceStatusAccount";
		spars[1] = new SqlParameter("@ResignationID", SqlDbType.VarChar);
		spars[1].Value = ResignationID;
		dtTrDetails = spm.getDataList(spars, "spExitProc_ClearanceForm");
		if (dtTrDetails.Rows.Count>0)
		{
			if (dtTrDetails.Rows.Count == 3)
			{
				SendMailToAccountsDept(ResignationID);
			}
		}
		}
		catch (Exception)
		{

			throw;
		}
	}
}