using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Activities.Expressions;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mediclaimdata : System.Web.UI.Page
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
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }


            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");


            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/vscb_index");
            }
            else
            {
                

                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    hdnEmpCode.Value = Session["Empcode"].ToString();

                    CheckIs_SubmitMediclaimData_Access();
                    
                    get_SearchParameter_List();
                    getEmployee_MediclaimData();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }

 

  

    protected void CHK_clearancefromSubmitted_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBox = (CheckBox)sender;
        GridViewRow row = (GridViewRow)checkBox.NamingContainer;

        string empcode = Convert.ToString(gvproject.DataKeys[row.RowIndex].Value).Trim();

        foreach (GridViewRow checkRow in gvproject.Rows)
        {
            if (empcode == Convert.ToString(checkRow.Cells[1].Text).Trim())
            {
                CheckBox CHK_clearancefromSubmitted = (CheckBox)checkRow.FindControl("CHK_clearancefromSubmitted");
                if (checkBox.Checked)
                {
                    CHK_clearancefromSubmitted.Checked = true;
                }
                else
                {
                    CHK_clearancefromSubmitted.Checked = false;
                }
            }

        }
    }
    #endregion

    #region Page Methods 

    public void getEmployee_MediclaimData()
    { 
        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "get_employee_Mediclaim_list";

        spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
        if (Convert.ToString(lstEmployees.SelectedValue).Trim() != "0")
            spars[1].Value = Convert.ToString(lstEmployees.SelectedValue).Trim();
        else
            spars[1].Value = DBNull.Value;

        spars[2] = new SqlParameter("@NomineeName", SqlDbType.VarChar);
        if (Convert.ToString(lstNominees.SelectedValue).Trim() != "0")
            spars[2].Value = Convert.ToString(lstNominees.SelectedItem.Text).Trim();
        else
            spars[2].Value = DBNull.Value;
               
        spars[3] = new SqlParameter("@MediclaimStatus", SqlDbType.VarChar);
        if (Convert.ToString(lstMediclaimStatus.SelectedValue).Trim() != "0")
            spars[3].Value = Convert.ToString(lstMediclaimStatus.SelectedItem.Text).Trim();
        else
            spars[3].Value = DBNull.Value;

        DataSet lds = new DataSet();
        lds= spm.getDatasetList(spars, "SP_Employee_Mediclaim_Data");
        gvproject.DataSource = null;
        gvproject.DataBind();
        RecordCount.Text = "";
        lblmessage.Text = "";
        if (lds!=null)
        {
            RecordCount.Text = "Record Count : " + Convert.ToString(lds.Tables[0].Rows.Count);
            gvproject.DataSource = lds.Tables[0];
            gvproject.DataBind();
        }
    }

    private void get_SearchParameter_List()
    {
        try
        { 
            DataSet dsProjectsVendors = new DataSet();
            SqlParameter[] spars = new SqlParameter[1];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetDropDownSearch";

            dsProjectsVendors = spm.getDatasetList(spars, "SP_Employee_Mediclaim_Data");

            if (dsProjectsVendors.Tables[0].Rows.Count > 0)
            {
                lstEmployees.DataSource = dsProjectsVendors.Tables[0];
                lstEmployees.DataTextField = "MemberName";
                lstEmployees.DataValueField = "Empcode";
                lstEmployees.DataBind();                
            }
            lstEmployees.Items.Insert(0, new ListItem("Select Employee Name", "0"));

            if (dsProjectsVendors.Tables[1].Rows.Count > 0)
            {
                lstNominees.DataSource = dsProjectsVendors.Tables[1];
                lstNominees.DataTextField = "MemberName";
                lstNominees.DataValueField = "Empcode";
                lstNominees.DataBind();
            }
            lstNominees.Items.Insert(0, new ListItem("Select Nominee Name", "0"));

            if (dsProjectsVendors.Tables[2].Rows.Count > 0)
            {
                lstMediclaimStatus.DataSource = dsProjectsVendors.Tables[2];
                lstMediclaimStatus.DataTextField = "status";
                lstMediclaimStatus.DataValueField = "status";
                lstMediclaimStatus.DataBind();                
            }
            lstMediclaimStatus.Items.Insert(0, new ListItem("Select Mediclaim Status", "0"));

 
        }
        catch (Exception)
        {

        }
    }
    #endregion



    private void Check_Mediclaim_Data_Access()
    {
        DataSet dsList = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "rpt_IsAppraisalProcess_Access";

        //spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        //spars[1].Value = hflEmpCode.Value;



        dsList = spm.ApprgetDatasetList(spars, "SP_Rpt_Appraisal_Process");

        if (dsList != null)
        {
            if (dsList.Tables[0].Rows.Count > 0)
            {
                //lnk_AppraisalProcess.Visible = true;
            }
        }


    }

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        getEmployee_MediclaimData();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        getEmployee_MediclaimData();
    }

    protected void fuel_btnSave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvproject.Rows)
        {
            CheckBox CHK_clearancefromSubmitted = (CheckBox)row.FindControl("CHK_clearancefromSubmitted");
            if (CHK_clearancefromSubmitted.Checked == true)
            {
                SqlParameter[] spars1 = new SqlParameter[3];
                spars1[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                spars1[0].Value = "update_Employee_Mediclaim_data";

                spars1[1] = new SqlParameter("@IsDataSendtoMedical_insurance", SqlDbType.Bit);
                spars1[1].Value = true;

                spars1[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
                spars1[2].Value = Convert.ToString(row.Cells[1].Text).Trim();

                spm.Insert_Data(spars1, "SP_Employee_Mediclaim_Data"); 
               
            }
        }

        #region Send Employee Mediclaim Data to Medical Insurance Company
        StringBuilder sMediclaimData = new StringBuilder();
        sMediclaimData.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:90%;border:1px solid;border-collapse: collapse;'>");
        sMediclaimData.Append("<tr><td style='border: 1px solid;border-collapse: collapse;width:2%;text-align:center'>Sr.No</td>");
        sMediclaimData.Append("<td style='border: 1px solid;border-collapse: collapse;width:10%;text-align:center'>Entity</td>");
        sMediclaimData.Append("<td style='border: 1px solid;border-collapse: collapse;width:4%;text-align:center'>Policy No</td>");
        sMediclaimData.Append("<td style='border: 1px solid;border-collapse: collapse;width:3%;text-align:center'>Emp. No.</td>");
        sMediclaimData.Append("<td style='border: 1px solid;border-collapse: collapse;width:8%;text-align:center'>Name</td>");
        sMediclaimData.Append("<td style='border: 1px solid;border-collapse: collapse;width:5%;text-align:center'>Date of Birth (dd/mm/yyyy)</td>");
        sMediclaimData.Append("<td style='border: 1px solid;border-collapse: collapse;width:3%;text-align:center'>Age</td>");
        sMediclaimData.Append("<td style='border: 1px solid;border-collapse: collapse;width:3%;text-align:center'>Gender</td>");
        sMediclaimData.Append("<td style='border: 1px solid;border-collapse: collapse;width:3%;text-align:center'>Relation</td>");
        sMediclaimData.Append("<td style='border: 1px solid;border-collapse: collapse;width:3%;text-align:center'>Status</td>");
        

        int irow = 1;
        foreach (GridViewRow row in gvproject.Rows)
        {
            CheckBox CHK_clearancefromSubmitted = (CheckBox)row.FindControl("CHK_clearancefromSubmitted");
            if (CHK_clearancefromSubmitted.Checked == true)
            {
                sMediclaimData.Append("<tr><td style='border: 1px solid;border-collapse: collapse;'>&nbsp;" + Convert.ToString(irow).Trim() + "</td>"); // SR No
                sMediclaimData.Append("<td style='border: 1px solid;border-collapse: collapse;'>&nbsp;HIGHBAR TECHNOCRAT LIMITED</td>"); //Entity
                sMediclaimData.Append("<td style='border: 1px solid;border-collapse: collapse;'>&nbsp;84504737</td>"); // Policy No
                sMediclaimData.Append("<td style='border: 1px solid;border-collapse: collapse;'>&nbsp;" + Convert.ToString(row.Cells[1].Text).Trim() + "</td>"); //Employee Code
                sMediclaimData.Append("<td style='border: 1px solid;border-collapse: collapse;'>&nbsp;" + Convert.ToString(row.Cells[4].Text).Trim() + "</td>"); //Member Name
                sMediclaimData.Append("<td style='border: 1px solid;border-collapse: collapse;'>&nbsp;" + Convert.ToString(row.Cells[7].Text).Trim() + "</td>"); //Member DOB
                sMediclaimData.Append("<td style='border: 1px solid;border-collapse: collapse;'>&nbsp;" + Convert.ToString(row.Cells[8].Text).Trim() + "</td>"); //Member Age
                sMediclaimData.Append("<td style='border: 1px solid;border-collapse: collapse;'>&nbsp;" + Convert.ToString(row.Cells[6].Text).Trim() + "</td>"); //Member Gender                
                sMediclaimData.Append("<td style='border: 1px solid;border-collapse: collapse;'>&nbsp;" + Convert.ToString(row.Cells[5].Text).Trim() + "</td>"); //Member Relation
                sMediclaimData.Append("<td style='border: 1px solid;border-collapse: collapse;'>&nbsp;" + Convert.ToString(row.Cells[9].Text).Trim() + "</td></tr>"); //Status
                irow += 1;
            }
        }
         

        StringBuilder strbuild = new StringBuilder();

        strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS;font-style:Regular;width:100%'>");       
        //strbuild.Append("<tr><td colspan=2 style='height:20px'></td></tr>");
        strbuild.Append("<tr><td colspan=2> Dear Sir/Madam,");
        strbuild.Append("<tr><td colspan=2>Please find below atthced emloyee mediclaim data for your reference.</td></tr>");
        strbuild.Append("<tr><td colspan=2 style='height:20px'></td></tr>");
        strbuild.Append("<tr><td colspan=2 style='height:20px'></td></tr>");        
        strbuild.Append("</table>");
         
        string ccMail = get_HR_Emails_List();

        string toMail = Convert.ToString(ConfigurationManager.AppSettings["SendMediclaimData"]).Trim();
       // sMediclaimData.Append("<tr><td>This is an auto generated email, please do not reply to this email.</td></tr>");
        sMediclaimData.Append("</table>");

        spm.sendMail(toMail, "Employee Mediclaim Data for Updation", Convert.ToString(strbuild).Trim() + Convert.ToString(sMediclaimData).Trim(), "", ccMail);
         
        getEmployee_MediclaimData();
        #endregion
    }

    public string get_HR_Emails_List()
    {
        string semails = "";
        SqlParameter[] spar = new SqlParameter[1];
        spar[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spar[0].Value = "get_Hr_Operation_EmailId"; 
       
         DataSet dsemails =   spm.getDatasetList(spar, "SP_Get_HR_Emailaddress");
        if (dsemails != null)
        {
            if (dsemails.Tables[0].Rows.Count > 0)
            {
                semails = Convert.ToString(dsemails.Tables[0].Rows[0]["HR_Emails"]);
            }
        } 

        return semails;
    }

    public void CheckIs_SubmitMediclaimData_Access()
    {
        DataSet getdtDetails = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "check_SubmitMediclaimDataStatus_Access";

            spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hdnEmpCode.Value);

            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "SubmitMediclaimData";

            getdtDetails = spm.getDatasetList(spars, "SP_Employee_Mediclaim_Data");
           
            if (getdtDetails.Tables[0].Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Tables[0].Rows[0]["IsAccess"]);
                if (getStatus != "SHOW")
                {
                    Response.Redirect("Nominations.aspx");
                }
            }
            // return false;
        }
        catch (Exception ex)
        {
            // return false;
        }
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        get_SearchParameter_List();
        getEmployee_MediclaimData();
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)sender;
        foreach (GridViewRow row in gvproject.Rows)
        {
            CheckBox chkRow = (CheckBox)row.FindControl("CHK_clearancefromSubmitted");
            if (chkRow != null)
            {
                chkRow.Checked = chkAll.Checked;
            }
        }
    }

}