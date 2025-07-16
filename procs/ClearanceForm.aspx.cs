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
using System.Linq;

public partial class procs_ClearanceForm : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;

    SP_Methods spm = new SP_Methods();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    DataTable dtLeaveDetails;
    DataSet dsLeaveRequst;
    string strempcode = "";

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

    #region Page Events
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                hdnEMPIDEXit.Value = Convert.ToString(Request.QueryString[0]).Trim();
                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                lpm.Emp_Code = strempcode;
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    PopulateEmployeeData();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
   
    #endregion

    #region code Not in use

    public void PopulateEmployeeData()
    {
        try
        {
           // var dtEmp = spm.GETEMPDETAIL_ATT(lpm.Emp_Code);

            SqlParameter[] sparss = new SqlParameter[4];
            sparss[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            sparss[0].Value = "GETEMPDETAIL";
            sparss[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            sparss[1].Value = Session["Empcode"].ToString();

          DataSet dsEmp = spm.getDatasetList(sparss, "SP_Admin_Employee_Exit");


            if (dsEmp.Tables[0].Rows.Count > 0)
            {
                lpm.Emp_Status = (string)dsEmp.Tables[0].Rows[0]["Emp_status"];
                txt_EmpCode.Text = lpm.Emp_Code;
                HDNResignValue.Value = Convert.ToString(dsEmp.Tables[0].Rows[0]["Resig_Date"]).Trim();
                txtEmp_Name.Text = (string)dsEmp.Tables[0].Rows[0]["Emp_Name"];
                txtEmp_Desigantion.Text = (string)dsEmp.Tables[0].Rows[0]["DesginationName"];
                txtEmp_Department.Text = (string)dsEmp.Tables[0].Rows[0]["Department_Name"];
                txt_Project.Text = (string)dsEmp.Tables[0].Rows[0]["Project"];
                lpm.Grade = Convert.ToString(dsEmp.Tables[0].Rows[0]["Grade"]).Trim();
                lpm.EmailAddress = (string)dsEmp.Tables[0].Rows[0]["Emp_EmailAddress"];
                //hdnLeaveCount.Value = Convert.ToString(dsEmp.Tables[0].Rows[0]["SL_LeaveBalance"]);
                //hdnLeaveCountFix.Value = Convert.ToString(dsEmp.Tables[0].Rows[0]["SL_LeaveBalance"]);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }
    }

    #endregion
    protected void btnIn_Click(object sender, EventArgs e)
    {
        try
        {
            lblmessage.Text = "";
            string Files = "";
            string FilesExitSurvey = "";
            string FilesExitFormA = "";
            string extension = "";
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
           // var supportedTypes = new[] { ".pdf", ".jpeg", ".png", ".bmp", ".JPG", ".JPEG", ".PNG", ".BMP" };
            var supportedTypes = new[] { ".pdf", ".PDF" };

            if (FileUploadExitSurvey.HasFile)
            {
                extension = System.IO.Path.GetExtension(FileUploadExitSurvey.FileName);
                if (!supportedTypes.Contains(extension))
                {
                    lblmessage.Text = "File Extension Is InValid - Only Upload  pdf";
                    return;
                }
                else
                {
                    if (Convert.ToString(FileUploadExitSurvey.FileName).Trim() != "")
                    {
                        string fileName = FileUploadExitSurvey.FileName;
                        FilesExitSurvey = fileName;
                        string strfileName = "";
                        string strremoveSpace = "ExitSurvey_" + Session["Empcode"].ToString() + extension;
                        strfileName = strremoveSpace;
                        FilesExitSurvey = strremoveSpace;
                        FileUploadExitSurvey.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ClearanceFormUpload"]).Trim()), strfileName));
                    }
                }
            }
            else
            {
                lblmessage.Text = "Upload Exit Survey Form";
                return;
            }

            if (FileUploadExitFormA.HasFile)
            {
                extension = System.IO.Path.GetExtension(FileUploadExitFormA.FileName);
                if (!supportedTypes.Contains(extension))
                {
                    lblmessage.Text = "File Extension Is InValid - Only Upload  pdf";
                    return;
                }
                else
                {
                    if (Convert.ToString(FileUploadExitFormA.FileName).Trim() != "")
                    {
                        string fileName = FileUploadExitFormA.FileName;
                        FilesExitFormA = fileName;
                        string strfileName = "";
                        string strremoveSpace = "ExitFormA_" + Session["Empcode"].ToString() + extension;
                        strfileName = strremoveSpace;
                        FilesExitFormA = strremoveSpace;
                        FileUploadExitFormA.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ClearanceFormUpload"]).Trim()), strfileName));
                    }
                }
            }
            else
            {
                lblmessage.Text = "Upload Exit Form A";
                return;
            }

            if (uploadfile.HasFile)
            {
                 extension = System.IO.Path.GetExtension(uploadfile.FileName);
                if (!supportedTypes.Contains(extension))
                {
                    lblmessage.Text = "File Extension Is InValid - Only Upload  pdf";
                    return;
                }
                else
                {
                    if (Convert.ToString(uploadfile.FileName).Trim() != "")
                    {
                        string fileName = uploadfile.FileName;
                       // string str = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
                        Files = fileName;
                        string strfileName = "";
                        string strremoveSpace =  "ClearanceForm_" + Session["Empcode"].ToString()  + extension;
                        strfileName = strremoveSpace;
                        Files = strremoveSpace;
                        uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ClearanceFormUpload"]).Trim()), strfileName));
                    }
                    SqlParameter[] sparss = new SqlParameter[6];
                    sparss[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                    sparss[0].Value = "UpdateClearanceFormEntry";
                    sparss[1] = new SqlParameter("@EMPEXIT_ID", SqlDbType.Int);
                    sparss[1].Value = hdnEMPIDEXit.Value;
                    sparss[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
                    sparss[2].Value = Session["Empcode"].ToString();
                    sparss[3] = new SqlParameter("@ClearanceFormName", SqlDbType.VarChar);
                    sparss[3].Value = Files;
                    sparss[4] = new SqlParameter("@ExitSurveyFormName", SqlDbType.VarChar);
                    sparss[4].Value = FilesExitSurvey;
                    sparss[5] = new SqlParameter("@ExitFormA", SqlDbType.VarChar);
                    sparss[5].Value = FilesExitFormA;
                    DataSet dsEmp = spm.getDatasetList(sparss, "SP_Admin_Employee_Exit");

                    #region EmailToHR

                    SqlParameter[] spars = new SqlParameter[3];
                    StringBuilder strbuild = new StringBuilder();
                    string to_email = "";
                    string cc_email = "";
                    string emp_name = "";
                   
                    spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                    spars[0].Value = "SeparationMail_EmployeeDetails";
                    spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
                    spars[1].Value = Session["Empcode"].ToString();
                    spars[2] = new SqlParameter("@EMPEXIT_ID", SqlDbType.Int);
                    spars[2].Value = hdnEMPIDEXit.Value;

                    DataTable tt = spm.getData_FromCode(spars, "SP_Admin_Employee_Exit");
                    if (tt.Rows.Count > 0)
                    {
                        emp_name = tt.Rows[0]["Emp_Name"].ToString();
                        strbuild.Length = 0;
                        strbuild.Clear();
                    }

                    strbuild.Append("<table border='1'>");
                    foreach (DataRow row in tt.Rows)
                    {
                        strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
                        + "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
                        + "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
                        + "</td></tr><tr><td>Date of Joining: </td><td>" + Convert.ToString(row["emp_doj"].ToString())
                        + "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
                        + "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
                        + "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
                        + "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
                        + "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
                        + "</td></tr><tr><td>Main Module: </td><td>" + Convert.ToString(row["ModuleDesc"].ToString())
                        + "</td></tr><tr><td>Other Modules: </td><td>" + Convert.ToString(row["OTHER_MODULES"].ToString())
                        + "</td></tr><tr><td>Project Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
                        + "</td></tr><tr><td>Delivery Manager: </td><td>" + Convert.ToString(row["DM"].ToString())
                        + "</td></tr><tr><td>Program Manager: </td><td>" + Convert.ToString(row["PRM"].ToString())
                        + "</td></tr><tr><td>Delivery Head: </td><td>" + Convert.ToString(row["DH"].ToString())
                        + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["RESIGN_DATE"].ToString())
                        + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignReason"].ToString())
                        + "</td></tr><tr><td>Separation Date: </td><td>" + Convert.ToString(row["DATE_OF_SEPARATION"].ToString())
                        + "</td></tr><tr><td>Separation Status: </td><td>" + Convert.ToString(row["Particulars1"].ToString())
                        + "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LwdAsperpolicy"].ToString())
                        + "</td></tr>");
                    }
                    strbuild.Append("</table>");

                    spars = new SqlParameter[2];
                    spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                    spars[0].Value = "ClearanceForm_SubmitMailHR";
                    spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
                    spars[1].Value = Session["Empcode"].ToString();
                    DataTable tt1 = spm.getData_FromCode(spars, "SP_Admin_Employee_Exit");
                    if (tt1.Rows.Count > 0)
                    {
                        to_email = "";
                        foreach (DataRow row in tt1.Rows)
                        {
                            to_email = to_email + Convert.ToString(row["email"].ToString()) + ";";
                        }
                    }

                    spars = new SqlParameter[2];
                    spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                    spars[0].Value = "ClearanceForm_SubmitMailHR_CC";
                    spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
                    spars[1].Value = Session["Empcode"].ToString();
                    DataTable tt2 = spm.getData_FromCode(spars, "SP_Admin_Employee_Exit");
                    if (tt2.Rows.Count > 0)
                    {
                        cc_email = "";
                        foreach (DataRow row in tt2.Rows)
                        {
                            cc_email = cc_email + Convert.ToString(row["email"].ToString()) + ";";
                        }
                    }
                    spm.send_mail_HR_ClearanceFormSubmited(emp_name, to_email, emp_name + " Clearance Form Uploaded", Convert.ToString(strbuild), "", cc_email);

                    #endregion
                    Response.Redirect("~/default.aspx");
                }
            }
            else
            {
                lblmessage.Text = "Upload Clearance Form File";
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }
}