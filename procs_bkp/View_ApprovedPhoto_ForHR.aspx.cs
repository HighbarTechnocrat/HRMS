using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class View_ApprovedPhoto_ForHR : System.Web.UI.Page
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

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Update_Photo_index");
            }
            else
            {
                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                 
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString.Count > 0)
                    {
                        //s  hdnReqid.Value = "1";
                        hdnReqid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        srno.Text = hdnReqid.Value;
                    }
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

    

    #region  

    public void PopulateEmployeeData()
    {
        try
        { 

            var dtEmp = spm.getemployeeupdatephoto(hdnReqid.Value);

            if (dtEmp.Rows.Count > 0)
            { 
                hdnReqid.Value= srno.Text;
                txt_EmpCode.Text = (string)dtEmp.Rows[0]["Emp_Code"];
                txtEmp_Name.Text = (string)dtEmp.Rows[0]["Emp_Name"];
                txtEmp_Desigantion.Text = (string)dtEmp.Rows[0]["Designation"];
                txtEmp_Department.Text = (string)dtEmp.Rows[0]["Department"];
                lpm.EmailAddress = dtEmp.Rows[0]["Emp_Emailaddress"].ToString(); 
                txtEmailAddress.Text = lpm.EmailAddress;

                imgCard.Visible = false;
                if (dtEmp.Rows.Count > 0)
                {

                    string imagePath = dtEmp.Rows[0]["FileName"].ToString();
                    imgCard.ImageUrl = "../Employee_Photo_Update/" + imagePath;
                    imgCard.Visible = true;
                    hdnEmployeePhoto.Value = imagePath;
                }

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
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            lblmessage.Text = "";
            if (Convert.ToString(txtRemark.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter remark.";
                return;
            }
            #region get First Approver id
            var toMail = txtEmailAddress.Text; 
            var empCode = txt_EmpCode.Text;
            var emp_name = txtEmp_Name.Text;
            var strsubject = "OneHR - Approved Uploaded Photo";
            var loginEmp_Name = Convert.ToString(Session["emp_loginName"]);
            // var Appr_id = Convert.ToInt32(hdnReqid.Value); 
            var Reamrk = Convert.ToString(txtRemark.Text).Trim();
            #endregion 
                //Final Approval
                spm.insertapprover_remarks("UpdatePhotoApprovar", empCode,  Reamrk);
               spm.send_mailto_Approved_photo(toMail, emp_name, loginEmp_Name,Reamrk, strsubject);
            #region Tansfer employee potho to thems folder


            // Get the source file path
            string sourcePath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["UploadupdatePhoto"]).Trim()),hdnEmployeePhoto.Value);

            // Get the destination paths
            string destinationPath_1 = Path.Combine(Server.MapPath("~/themes/creative1.0/images/profile55x55"), hdnEmployeePhoto.Value);

            string destinationPath_2 = Path.Combine(Server.MapPath("~/themes/creative1.0/images/profile110x110"),hdnEmployeePhoto.Value);

             // Check if the source file exists
                if (File.Exists(sourcePath))
                {
                    // Ensure the destination directories exist
                   // string directory1 = Path.GetDirectoryName(destinationPath_1);
                    ///string directory2 = Path.GetDirectoryName(destinationPath_2);

                    // Copy the file to the destination paths
                    File.Copy(sourcePath, destinationPath_1, true);
                    File.Copy(sourcePath, destinationPath_2, true);
                 
                }
                
           
            


            #endregion
            Response.Redirect("~/procs/View_updatephoto_forHR.aspx");
        }
        catch (Exception ex)
        { }
    }

    protected void btnIn_Correction(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }
            lblmessage.Text = "";
            if (Convert.ToString(txtRemark.Text).Trim() == "")
            {
                lblmessage.Text = "Please enter remark.";
                return;
            }
            #region get First Approver id
            var toMail = txtEmailAddress.Text;
            var empCode = txt_EmpCode.Text;
            var emp_name = txtEmp_Name.Text;
            var strsubject = "Request for Correction of Photo in OneHR";
            var loginEmp_Name = Convert.ToString(Session["emp_loginName"]); 
            var Reamrk = Convert.ToString(txtRemark.Text).Trim();
            string redirectURL = Convert.ToString(ConfigurationManager.AppSettings["employeephoto"]).Trim();
            #endregion
             
            spm.insertcorrection_remarks("UpdatePhotoCorrection", empCode, Reamrk);
            spm.send_mailto_correction_photo(toMail, emp_name, loginEmp_Name, Reamrk, strsubject, redirectURL);
           
            Response.Redirect("~/procs/View_updatephoto_forHR.aspx");
        }
        catch (Exception ex)
        { }
    }
     
     
}
