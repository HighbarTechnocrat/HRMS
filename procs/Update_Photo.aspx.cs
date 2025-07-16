using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;  

public partial class  Update_Photo : System.Web.UI.Page
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
    
    string strempcode = "";

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

  
    private void Page_Load(object sender, System.EventArgs e)
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

            txt_EmpCode.Text = Convert.ToString(Session["Empcode"]).Trim();
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Update_Photo_index");
            }
            else
            {
                Page.SmartNavigation = true;
                FilePath.Value = Convert.ToString(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["UploadupdatePhoto"]).Trim()));
                strempcode = Session["Empcode"].ToString();
                lpm.Emp_Code = strempcode;
                if (!Page.IsPostBack)
                { 
                    editform.Visible = true;                     
                    PopulateEmployeeData();
                    ddl_card_image(); 
                    
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
                  
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
     
    public void PopulateEmployeeData()
    {
        try
        {
            DataTable dtEmp;
            dtEmp = spm.GetEmployeeData(Convert.ToString(txt_EmpCode.Text));

            if (dtEmp.Rows.Count > 0)
            {
                //txt_EmpCode.Text = Convert.ToString(dtEmp.Rows[0]["Emp_Code"]).Trim();
                txtEmp_Name.Text = Convert.ToString(dtEmp.Rows[0]["Emp_Name"]).Trim();
                txtEmp_Desigantion.Text = Convert.ToString(dtEmp.Rows[0]["DesginationName"]).Trim();
                txtEmp_Department.Text = Convert.ToString(dtEmp.Rows[0]["Department_Name"]).Trim();
                 
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }
     

    public void ddl_card_image()
    {
        DataSet dsVendorEdit = new DataSet();
        SqlParameter[] sparss = new SqlParameter[2];

        sparss[0] = new SqlParameter("@QUERYMODE", SqlDbType.VarChar);
        sparss[0].Value = "get_status";

        sparss[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        sparss[1].Value = strempcode;

        dsVendorEdit = spm.getDatasetList(sparss, "SP_Update_Photo");
        imgCard.Visible = false;
        if (dsVendorEdit.Tables.Count > 0 && dsVendorEdit.Tables[0].Rows.Count > 0)
        {
            object pendingApprovedValue = dsVendorEdit.Tables[0].Rows[0]["Pending_approved"];
             
            photo.Visible = true;
            photo_1.Visible = true;
            file.Visible = false;
            btnIn.Visible = false;
            btnback_mng.Visible = false;
            string imagePath = dsVendorEdit.Tables[0].Rows[0]["FileName"].ToString();
            imgCard.ImageUrl = "../Employee_Photo_Update/" + imagePath;
            imgCard.Visible = true; 

            if (pendingApprovedValue.ToString() == "3")
            { 
                file.Visible = true;
                btnback_mng.Visible = true;
                if (dsVendorEdit.Tables[0].Rows[0]["Remarks"].ToString() != "")
                {
                    txtRemark.Text = dsVendorEdit.Tables[0].Rows[0]["Remarks"].ToString();
                    txtRemark.Visible = true;
                    liRemarks.Visible = true;
                    liRemarks_1.Visible = true;
                }
            }

        }


    }
      
    protected void btnIn_Click(object sender, EventArgs e)
    {

        if (!uplodmultiple.HasFile)
        {
            lblmessage.Text = " Please upload your passort-size photo";
            return;
        } 
         

        if (uplodmultiple.HasFile)
        { 
            string FilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["UploadupdatePhoto"]).Trim());
            bool folderExists = Directory.Exists(FilePath);
            if (!folderExists)
            {
                Directory.CreateDirectory(FilePath);
            }
            string approveremailaddress = Convert.ToString(hdncurrid.Value).Trim();

            if (uplodmultiple.HasFiles)
            {
                string filename = "";
                foreach (HttpPostedFile uploadedFile in uplodmultiple.PostedFiles)
                {
                    if (uploadedFile.ContentLength > 0)
                    {
                        String InputFile = System.IO.Path.GetExtension(uploadedFile.FileName);

                        if(Convert.ToString(InputFile).Trim().ToLower() != ".jpg")
                        {
                            lblmessage.Text = "Please upload your passort-size photo in only .jpg format";
                            return;
                        }
                        string sPhotoName = "";
                        if (Convert.ToString(txt_EmpCode.Text).Contains("-"))
                            sPhotoName = Convert.ToString(txt_EmpCode.Text).Trim(); 
                        else
                            sPhotoName = Convert.ToString(Convert.ToInt32(txt_EmpCode.Text));

                        filename = sPhotoName +  InputFile;                        
                        uploadedFile.SaveAs(Path.Combine(FilePath, filename));                        
                        string empCode=txt_EmpCode.Text.Trim();
                        string emp_name = txtEmp_Name.Text.Trim();
                        string dep = txtEmp_Department.Text.Trim();
                        string des = txtEmp_Desigantion.Text.Trim(); 
                        DataSet ds=  spm.Insert_update_photho(empCode, "INSERT", emp_name, dep, des, Convert.ToString(filename.Trim()), "Photo");
                       if (ds != null)
                        {
                            var srnoid =ds.Tables[0].Rows[0]["MaxSrno"];
                            string HrName =Convert.ToString(ds.Tables[0].Rows[0]["HR_Name"]);
                            string HrEmailId= Convert.ToString(ds.Tables[0].Rows[0]["HR_Emailaddress"]);
                            string redirectURL = Convert.ToString(ConfigurationManager.AppSettings["approvephoto"]).Trim() + "?reqid=" + srnoid;
                            spm.send_mailto_HR_Approver(txtEmp_Name.Text,HrName, HrEmailId, "Request for employee photo approval : ", redirectURL);

                        }

                    }
                }
               // ddl_card_image();
            }

            Response.Redirect("~/procs/Update_Photo_index.aspx");
            
        }
 
    }
}
