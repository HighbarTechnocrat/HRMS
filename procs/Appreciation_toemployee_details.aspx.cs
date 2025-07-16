using System; 
using System.Data;
using System.Data.SqlClient; 
using System.Web.UI;
using System.Configuration;


  
public partial class Appreciation_toemployee_Details : System.Web.UI.Page
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Appreciation_Letter_index");
            }
            else
            {
                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString(); 
                hdfilefath.Value = Convert.ToString(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["AppreciationLetter"]).Trim()));
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString.Count > 0)
                    {
                        
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
            DataSet dtEmp = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "to_employee_data";
            spars[1] = new SqlParameter("@sr_no", SqlDbType.VarChar);
            spars[1].Value = hdnReqid.Value;
            dtEmp = spm.getDatasetList(spars, "Appreciation_Letter");
            if (dtEmp.Tables[0].Rows.Count > 0)
            {
                hdnReqid.Value = srno.Text; 
                txtEmp_Name.Text = (string)dtEmp.Tables[0].Rows[0]["Emp_Name"];
                txt_letter.Text = (string)dtEmp.Tables[0].Rows[0]["Appreciation_Letter"];
                txtReceiveddate.Text = (string)dtEmp.Tables[0].Rows[0]["Send_date"];
                txtreceviedfrom.Text = (string)dtEmp.Tables[0].Rows[0]["Send_to"];
                txtpoint.Text = (string)dtEmp.Tables[0].Rows[0]["point"]; 
                ddl_letter.InnerText = dtEmp.Tables[0].Rows[0]["Drafts"].ToString();
                lpm.EmailAddress = dtEmp.Tables[0].Rows[0]["Emp_Emailaddress"].ToString();
                txtEmailAddress.Text = lpm.EmailAddress; 
                hdfilename.Value =  dtEmp.Tables[0].Rows[0]["Letter_pdf"].ToString(); 
                lnkviewletter.Text = Convert.ToString(dtEmp.Tables[0].Rows[0]["Letter_pdf"]).Trim();

                imgCard.Visible = false;
                if (dtEmp.Tables[0].Rows.Count > 0)
                {

                    string imagePath = dtEmp.Tables[0].Rows[0]["photo"].ToString();
                    imgCard.ImageUrl = "../AppreciationLetter/" + imagePath;
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


}
