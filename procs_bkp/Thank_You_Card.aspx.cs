using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;




public partial class Thank_You_Card : System.Web.UI.Page
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

    #region PageEvents
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/ThankyouCard_index");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    imgCard.Visible = false;
                    getemployeelist();
                    getcardlist();                   
                                     

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";

                    BindImages();


                }
               
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    private void BindImages()
    {
        // Replace with your actual image paths
        List<string> imagePaths = new List<string>
        {
            "../thankyoucard/thankyoucard1.jpg",
            "../thankyoucard/thankyoucard2.jpg",
            "../thankyoucard/thankyoucard3.jpg",
            "../thankyoucard/thankyoucard4.jpg",
            "../thankyoucard/thankyoucard5.jpg",
            "../thankyoucard/thankyoucard6.jpg",
            "../thankyoucard/thankyoucard7.jpg",
            "../thankyoucard/thankyoucard8.jpg",
            "../thankyoucard/thankyoucard9.jpg",
            "../thankyoucard/thankyoucard10.jpg"
        };

        
    }

    public void getemployeelist()
    {
        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "employee_name";

        dsProjectsVendors = spm.getDatasetList(spars, "SP_thank_you_card");

        if (dsProjectsVendors.Tables[0].Rows.Count > 0)
        {
            string loggedInEmpCode = Session["Empcode"].ToString(); // Assuming you're storing logged-in Emp_Code in Session
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

    public void getcardlist()
    {
        

        DataSet dsProjectsVendors = new DataSet();
        SqlParameter[] spars = new SqlParameter[1];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "card_list";

        dsProjectsVendors = spm.getDatasetList(spars, "SP_thank_you_card");

        if (dsProjectsVendors.Tables[0].Rows.Count > 0)
        {
            
            ddl_cardlist.DataSource = dsProjectsVendors.Tables[0];
            ddl_cardlist.DataTextField = "card_sub";
            ddl_cardlist.DataValueField = "id";
            ddl_cardlist.DataBind();
        }
        ddl_cardlist.Items.Insert(0, new ListItem("Select Thank You Card", "0"));

         
        
    }
     
    protected void ddl_card_image(object sender, EventArgs e)
    {
        SqlParameter[] spars1 = new SqlParameter[2];

        spars1[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars1[1] = new SqlParameter("@id", SqlDbType.VarChar);
        spars1[0].Value = "load_image";
        spars1[1].Value = ddl_cardlist.SelectedValue.ToString();
        DataTable dt = spm.getDataList(spars1, "SP_thank_you_card");
        imgCard.Visible = false;
        if (dt.Rows.Count > 0)
        {

            string imagePath = dt.Rows[0]["photo"].ToString(); 
            imgCard.ImageUrl = "../thankyoucard/" + imagePath;
            imgCard.Visible = true;
        }
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
            lblmessage.Text = "Please select  Employee Name.";
            return;
        }

        if (Convert.ToString(ddl_cardlist.SelectedValue).Trim() == "0")
        {
            lblmessage.Text = "Please select Thank You Card.";
            return;
        }

        string qtype = "insert_card_detail";

        DataSet dsgoal = new DataSet();
        SqlParameter[] spars = new SqlParameter[6];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = qtype;

        spars[1] = new SqlParameter("@id", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(ddl_cardlist.SelectedValue);

        spars[2] = new SqlParameter("@card_id", SqlDbType.VarChar);
        spars[2].Value = Convert.ToString(ddl_cardlist.SelectedValue);

        spars[3] = new SqlParameter("@send_to", SqlDbType.VarChar);
        spars[3].Value = Convert.ToString(ddl_empname.SelectedValue);

        spars[4] = new SqlParameter("@createdby", SqlDbType.VarChar);
        spars[4].Value = Convert.ToString(Session["Empcode"]).Trim();

        spars[5] = new SqlParameter("@ThankYouRemarks", SqlDbType.VarChar);
        if (Convert.ToString(txt_remakrs.Text).Trim() != "")
            spars[5].Value = Convert.ToString(txt_remakrs.Text).Trim();
        else
            spars[5].Value = DBNull.Value;

        dsgoal = spm.getDatasetList(spars, "SP_thank_you_card");

        if (dsgoal != null) { 
            if(dsgoal.Tables != null)
            {
                if (dsgoal.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        var empName = ddl_empname.SelectedItem.Text.Trim();
                        var empEmail = dsgoal.Tables[0].Rows[0]["emp_email"].ToString();
                        var createdempname = dsgoal.Tables[0].Rows[0]["created_empname"].ToString();
                        var imagepath = imgCard.ImageUrl;
                       // var CardSubject = dsgoal.Tables[0].Rows[0]["CardSubject"].ToString();
                       string CardSubject = Convert.ToString(ddl_cardlist.SelectedItem.Text);
                        // var CardSubject = "Heartfelt Gratitude !!";
                        StreamReader reader = new StreamReader(@"D:\HRMS\hrms\thankyoucard\thankyoucard.html");
                        string myString = reader.ReadToEnd();                   
                        var empPhotoUrl = "http://localhost/hrms/thankyoucard/" + imagepath;
                        myString = myString.Replace("{emp_Name}", empName);
                        myString = myString.Replace("{created_empname}", createdempname);
                        myString = myString.Replace("{emp_Name}", empName);
                        myString = myString.Replace("{Bg_Img}", empPhotoUrl);
                        myString = myString.Replace("{ThankYouNote}", Convert.ToString(txt_remakrs.Text));
                        spm.sendMail(empEmail, CardSubject, myString, "", "");
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }        
        }
        Response.Redirect("~/procs/ThankyouCard_index.aspx");
        // Response.Redirect(Request.RawUrl);
    }
     

    #endregion

}
