using HtmlAgilityPack;
using Microsoft.Reporting.WebForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
//using System.Net.Http;
public partial class Appreciation_Letter : System.Web.UI.Page
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
    private object document;

    #region PageEvents
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Appreciation_Letter_index");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    hdnEmpCode.Value = Session["Empcode"].ToString();

                    imgCard.Visible = false;
                    Appreciation_Draft.Visible = false;
                    mobile_btnSave.Visible = false;
                    txt_sub.Visible = false;
                    txt_lettere_sub.Visible = false;
                    getemployeelist();
                    get_letter_list();
                    txt_sub.Attributes.Add("maxlength", "150");


                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";

                    if (check_mc_member() == false)
                    {
                        Response.Redirect("Appreciation_Letter_index.aspx");
                    }

                }

            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    protected void emp_status_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddl_letters.Visible = emp_status.SelectedValue != "0";

        DataSet dsLocations = new DataSet();

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "check_letter";

        spars[1] = new SqlParameter("@letter_category", SqlDbType.Int);
        spars[1].Value = emp_status.Text;

        dsLocations = spm.getDatasetList(spars, "Appreciation_Letter");

        if (dsLocations != null && dsLocations.Tables.Count > 0 && dsLocations.Tables[0].Rows.Count > 0)
        {
            // Bind the filtered list to your UI (e.g., GridView, ListView, etc.)
            emp_status.DataSource = dsLocations.Tables[0];
        }
        getcardlist();

    }

    public void PopulateEmployeeData()
    {
        try
        {
            DataSet dtEmp = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "letter_subject";
            spars[1] = new SqlParameter("@Appreciation_id", SqlDbType.Int);
            spars[1].Value = ddl_cardlist.SelectedValue;
            dtEmp = spm.getDatasetList(spars, "Appreciation_Letter");
            if (dtEmp.Tables[0].Rows.Count > 0)
            {
                txt_sub.Text = (string)dtEmp.Tables[0].Rows[0]["letter_sub"];
                txtpoint.Text = (string)dtEmp.Tables[0].Rows[0]["point"];
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }
    public Boolean check_mc_member()
    {
        Boolean isvalid = false;

        DataSet dsLocations = new DataSet();

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "check_mc_member";

        spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
        spars[1].Value = hdnEmpCode.Value;

        dsLocations = spm.getDatasetList(spars, "Appreciation_Letter");

        if (dsLocations != null)
        {
            if (dsLocations.Tables[0].Rows.Count > 0)
            {
                isvalid = true;
            }
        }
        return isvalid;

    }

    public void getemployeelist()
    {
        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "employee_name";

        dsProjectsVendors = spm.getDatasetList(spars, "Appreciation_Letter");

        if (dsProjectsVendors.Tables[0].Rows.Count > 0)
        {
            string loggedInEmpCode = Session["Empcode"].ToString();
            DataRow[] rowsToRemove = dsProjectsVendors.Tables[0].Select("Emp_Code = '" + loggedInEmpCode + "'");

            foreach (DataRow row in rowsToRemove)
            {
                dsProjectsVendors.Tables[0].Rows.Remove(row);
            }

            ddl_empname.DataSource = dsProjectsVendors.Tables[0];
            ddl_empname.DataTextField = "Emp_Name";
            ddl_empname.DataValueField = "Emp_Code";
            ddl_empname.DataBind();
        }
        ddl_empname.Items.Insert(0, new ListItem("Select Employee Name", "0"));
    }

    public void get_letter_list()
    {

        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "Appreciation_letter_category";

        spars[1] = new SqlParameter("@letter_category", SqlDbType.VarChar);
        spars[1].Value = emp_status.Text;

        dsProjectsVendors = spm.getDatasetList(spars, "Appreciation_Letter");


        if (dsProjectsVendors != null && dsProjectsVendors.Tables.Count > 0 && dsProjectsVendors.Tables[0].Rows.Count > 0)
        {

            emp_status.DataSource = dsProjectsVendors.Tables[0];
            emp_status.DataTextField = "letter_category";
            emp_status.DataValueField = "category_id";
            emp_status.DataBind();

        }
        emp_status.Items.Insert(0, new ListItem("Select Appreciation Letter", "0"));

    }

    public void getcardlist()
    {

        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "Appreciation_Letter_list";

        spars[1] = new SqlParameter("@letter_category", SqlDbType.VarChar);
        spars[1].Value = emp_status.Text;

        dsProjectsVendors = spm.getDatasetList(spars, "Appreciation_Letter");


        if (dsProjectsVendors != null && dsProjectsVendors.Tables.Count > 0 && dsProjectsVendors.Tables[0].Rows.Count > 0)
        {

            ddl_cardlist.DataSource = dsProjectsVendors.Tables[0];
            ddl_cardlist.DataTextField = "Appreciation_Letter";
            ddl_cardlist.DataValueField = "Appreciation_id";
            ddl_cardlist.DataBind();

        }
        ddl_cardlist.Items.Insert(0, new ListItem("Select Appreciation Letter", "0"));

    }

    protected void ddl_card_image(object sender, EventArgs e)
    {
        SqlParameter[] spars1 = new SqlParameter[2];

        spars1[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars1[1] = new SqlParameter("@Appreciation_id", SqlDbType.VarChar);
        spars1[0].Value = "load_image";
        spars1[1].Value = ddl_cardlist.SelectedValue.ToString();
        DataTable dt = spm.getDataList(spars1, "Appreciation_Letter");
        imgCard.Visible = false;
        mobile_btnSave.Visible = false;
        Appreciation_Draft.Visible = false;
        txt_sub.Visible = false;
        txt_lettere_sub.Visible = false;
        liSubject.Visible = true;
        txt_point.Visible = true;
        if (dt.Rows.Count > 0)
        {

            string imagePath = dt.Rows[0]["photo"].ToString();
            imgCard.ImageUrl = "../AppreciationLetter/" + imagePath;
            imgCard.Visible = false;
             
            SqlParameter[] spars2 = new SqlParameter[2];
            spars2[0] = new SqlParameter("@qtype", SqlDbType.VarChar) { Value = "load_draft" };
            spars2[1] = new SqlParameter("@Appreciation_id", SqlDbType.VarChar) { Value = ddl_cardlist.SelectedValue.ToString() };
             
            DataTable dtt = spm.getDataList(spars2, "Appreciation_Letter");

            if (dtt != null && dtt.Rows.Count > 0)
            {
                var empName = ddl_empname.SelectedItem.Text.Trim();
                ddl_emp_names.Text = empName;
                ddl_letter.Text = dtt.Rows[0]["Draft"].ToString();
               
            }



            mobile_btnSave.Visible = true;
            Appreciation_Draft.Visible = true;
            txt_sub.Visible = true;
            txt_lettere_sub.Visible = true;
            PopulateEmployeeData();
        }
 
    }

    private bool IsAppreciationDraftIsEmpty(string stext)
    {
        if (string.IsNullOrWhiteSpace(stext))
        {
            lblmessage.Text = "Please enter the Appreciation Draft.";
            return false;
        }
        string content = Regex.Replace(stext, "<.*?>", "").Trim();
        content = Regex.Replace(content, @"\s+", "").Replace("&nbsp;", "").Trim();
        if (string.IsNullOrWhiteSpace(content))
        {
            lblmessage.Text = "Please enter the Appreciation Draft.";
            return false;
        }
        return true;
    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {

        string confirmValue = hdnYesNo.Value.ToString();
        if (confirmValue != "Yes")
        {
            return;
        }

        lblmessage.Text = "";
        if (Convert.ToString(ddl_empname.SelectedValue).Trim() == "0")
        {
            lblmessage.Text = "Please Select Employee Name.";
            return;
        }

        if (Convert.ToString(emp_status.SelectedValue).Trim() == "0")
        {
            lblmessage.Text = "Please Select Appreciation Letter Type.";
            return;
        }

        if (Convert.ToString(ddl_cardlist.SelectedValue).Trim() == "0")
        {
            lblmessage.Text = "Please Select Appreciation Letter.";
            return;
        }

        if (Convert.ToString(txt_sub.Text).Trim() == "")
        {
            lblmessage.Text = "Please Enter Appreciation Letter Subject";
            return;
        } 

        if (IsAppreciationDraftIsEmpty(ddl_letter.Text) == false)
        {
            lblmessage.Text = "Please enter the Appreciation Draft.";
            return;
        }

        string filename = ddl_empname.SelectedValue + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";

        DataSet dsgoal = new DataSet();
        SqlParameter[] spars = new SqlParameter[7];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "insert_card_detail";

        spars[1] = new SqlParameter("@Appreciation_id", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(ddl_cardlist.SelectedValue).Trim();

        spars[2] = new SqlParameter("@send_to", SqlDbType.VarChar);
        spars[2].Value = Convert.ToString(ddl_empname.SelectedValue).Trim();

        spars[3] = new SqlParameter("@createdby", SqlDbType.VarChar);
        spars[3].Value = Convert.ToString(Session["Empcode"]).Trim();

        spars[4] = new SqlParameter("@draft", SqlDbType.VarChar);
        spars[4].Value = Convert.ToString(ddl_letter.Text).Trim();

        spars[5] = new SqlParameter("@letter_sub", SqlDbType.VarChar);
        spars[5].Value = Convert.ToString(txt_sub.Text).Trim();

        spars[6] = new SqlParameter("@Letter_pdf", SqlDbType.VarChar);
        spars[6].Value = Convert.ToString(filename).Trim();

        dsgoal = spm.getDatasetList(spars, "Appreciation_Letter");
          
        string reportPath = Server.MapPath("~/procs/Employee_Appreciation_Letter.rdlc");
         
        LocalReport report = new LocalReport
        {
            ReportPath = reportPath
        };
        ReportDataSource rds = new ReportDataSource("DataSet1", dsgoal.Tables[0]);
        report.DataSources.Clear();
        report.DataSources.Add(rds);
        string mimeType, encoding, fileExtension;
        string[] streams;
        Warning[] warnings;
        byte[] pdfBytes = report.Render("PDF", null, out mimeType, out encoding, out fileExtension, out streams, out warnings);
        string saveFolder = Server.MapPath("~/AppreciationLetter");
        if (!Directory.Exists(saveFolder)) Directory.CreateDirectory(saveFolder);
        string fullPath = Path.Combine(saveFolder, filename);
        File.WriteAllBytes(fullPath, pdfBytes);
         
        hdfilefath.Value = Convert.ToString(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["AppreciationLetter"]).Trim() + filename));


        if (dsgoal.Tables[1].Rows.Count > 0)
        {
            var empEmail = dsgoal.Tables[1].Rows[0]["Emp_Emailaddress"].ToString();
            var empname = dsgoal.Tables[1].Rows[0]["Emp_Name"].ToString();
            var RM_Emailaddress = dsgoal.Tables[1].Rows[0]["RM_Emailaddress"].ToString();

            if (dsgoal != null && dsgoal.Tables.Count > 0 && dsgoal.Tables[2].Rows.Count > 0)
            {
                 
              //  var empName = ddl_empname.SelectedItem.Text.Trim();
                var created_emp = dsgoal.Tables[2].Rows[0]["Emp_Name"].ToString();
                var empdes = dsgoal.Tables[2].Rows[0]["Designation"].ToString();
                var draft = Convert.ToString(ddl_letter.Text).Trim();
                var sub = Convert.ToString(txt_sub.Text).Trim();
                string reportPathHTML = Server.MapPath("~/AppreciationLetter/Email_SendAppreciation_Letter.html");
                string myString; 
                using (StreamReader reader = new StreamReader(reportPathHTML))
                {
                    myString = reader.ReadToEnd();
                } 
                myString = myString.Replace("{emp_Name}", empname);
                myString = myString.Replace("{emp_des}", empdes);
                myString = myString.Replace("{draft}", draft);
                myString = myString.Replace("{created_emp}", created_emp);
                                 
                string strsubject = sub;
                StringBuilder strbuild = new StringBuilder();

                strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:100%'>");
                strbuild.Append("<tr><td style='height:20px'></td></tr>");
                strbuild.Append("<tr><td>" + myString + "</td></tr>");
                strbuild.Append("<tr><td style='height:20px'></td></tr>");
                strbuild.Append("</table>");

                // Send email
                spm.sendMail(empEmail, strsubject, strbuild.ToString(), hdfilefath.Value, RM_Emailaddress); // Ensure sendMail supports attachments

            }
        }

        Response.Redirect("~/procs/Appreciation_Letter_index.aspx");
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/Appreciation_Letter_index.aspx");
    }
 

    #endregion

}
