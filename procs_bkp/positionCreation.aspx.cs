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
using System.Collections.Generic;

public partial class positionCreation : System.Web.UI.Page
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

    protected void loadorder()
    {
        DataTable dtp = classpkg.getcountorderdetailbyemaild(Page.User.Identity.Name.ToString().Trim());
        if (dtp.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtp.Rows[0]["orderid"].ToString()) == 0)
            {
                lihistory.Visible = false;
            }
        }
        else
        {
            lihistory.Visible = true;
        }
    }
    private void DisplayProfileProperties()
    {
        try
        {
            Boolean varfindcity = false;

            MembershipUser user = Membership.GetUser(this.Page.User.Identity.Name.ToString().Trim());
            DataSet ds_userdetails = classaddress.getalluserbyusername(this.Page.User.Identity.Name.ToString().Trim());
            if (ds_userdetails.Tables.Count > 0)
            {
                if (ds_userdetails.Tables[0].Rows.Count > 0)
                {
                    // txtemail.Text = ds_userdetails.Tables[0].Rows[0]["username"].ToString();
                    // txtemailadress.Text = ds_userdetails.Tables[0].Rows[0]["emailid"].ToString();
                    //txtfirstname.Text = ds_userdetails.Tables[0].Rows[0]["firstname"].ToString();
                    //txtlastname.Text = ds_userdetails.Tables[0].Rows[0]["lastname"].ToString();
                    //txtaddress1.Text = ds_userdetails.Tables[0].Rows[0]["address"].ToString();
                    // txtpincode.Text = ds_userdetails.Tables[0].Rows[0]["pincode"].ToString();
                    //txtphone.Text = ds_userdetails.Tables[0].Rows[0]["telno"].ToString();
                    //txtmobile.Text = ds_userdetails.Tables[0].Rows[0]["mobileno"].ToString();
                    //txttempaddress.Text = ds_userdetails.Tables[0].Rows[0]["tempaddress"].ToString();
                    //txtaltemail.Text = ds_userdetails.Tables[0].Rows[0]["alternateemail"].ToString();
                    //txtextension.Text = ds_userdetails.Tables[0].Rows[0]["extentionno"].ToString();
                    //txtoffno.Text = ds_userdetails.Tables[0].Rows[0]["officemob"].ToString();
                    //txtaltno.Text = ds_userdetails.Tables[0].Rows[0]["alternatemob"].ToString();
                    //txtoffphone.Text = ds_userdetails.Tables[0].Rows[0]["officephone"].ToString();
                    //txtfaxno.Text = ds_userdetails.Tables[0].Rows[0]["faxno"].ToString();
                    //  txtloc.Text = ds_userdetails.Tables[0].Rows[0]["location"].ToString();
                    //txtdept.Text = ds_userdetails.Tables[0].Rows[0]["department"].ToString();
                    //txtsubdept.Text = ds_userdetails.Tables[0].Rows[0]["sub_department"].ToString();
                    //txtdesg.Text = ds_userdetails.Tables[0].Rows[0]["designation"].ToString();

                    //DateTime dob1 = new DateTime();

                    //if (ds_userdetails.Tables[0].Rows[0]["DOB1"].ToString().Length > 0 && ds_userdetails.Tables[0].Rows[0]["DOB1"].ToString() != null && ds_userdetails.Tables[0].Rows[0]["DOB1"].ToString() != "")
                    //{
                    //    dob1 = Convert.ToDateTime(ds_userdetails.Tables[0].Rows[0]["DOB1"]);
                    //    if (dob1.ToString("dd/MMM/yyyy") == "01/Jan/1900")
                    //    {
                    //        txtdob1.Text = "";
                    //    }
                    //    else
                    //    {
                    //        txtdob1.Text = dob1.ToString("dd/MMM/yyyy");
                    //    }
                    //}
                    //else
                    //{
                    //    txtdob1.Text = "";
                    //}


                    DateTime dob = new DateTime();

                    if (ds_userdetails.Tables[0].Rows[0]["DOB"].ToString().Length > 0 && ds_userdetails.Tables[0].Rows[0]["DOB"].ToString() != null && ds_userdetails.Tables[0].Rows[0]["DOB"].ToString() != "")
                    {
                        dob = Convert.ToDateTime(ds_userdetails.Tables[0].Rows[0]["DOB"]);
                        if (dob.ToString("dd/MMM/yyyy") == "01/Jan/1900")
                        {
                            // txtdob.Text = "";
                        }
                        else
                        {
                            //  txtdob.Text = dob.ToString("dd/MMM/yyyy");
                        }
                    }
                    else
                    {
                        //txtdob.Text = "";
                    }

                    string gen = ds_userdetails.Tables[0].Rows[0]["gender"].ToString();
                    if (gen == "M" || gen == "m")
                    {
                        //rbtnmale.Checked = true;
                    }
                    else
                    {
                        //rbtnfemale.Checked = true;
                    }


                    DataTable user2 = classreviews.getuseridbyemail(Page.User.Identity.Name);

                    if (user2.Rows.Count > 0)
                    {
                        userid = user2.Rows[0]["indexid"].ToString();
                        if (user2.Rows[0]["profilephoto"].ToString() != "")
                        {
                            pimg = user2.Rows[0]["profilephoto"].ToString().Trim();
                            if (user2.Rows[0]["profilephoto"].ToString().Trim() == "noimage1.png" || user2.Rows[0]["profilephoto"].ToString().Trim() == "noimage3.jpg")
                            {
                                //  removeprofile.Visible = false;
                            }
                            else
                            {
                                // removeprofile.Visible = true;
                            }
                            if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profilephoto/" + user2.Rows[0]["profilephoto"].ToString())))
                            {
                                //  imgprofile.Src = ReturnUrl("sitepath") + "images/profilephoto/" + user2.Rows[0]["profilephoto"].ToString();
                            }
                            else
                            {
                                // imgprofile.Src = "http://graph.facebook.com/" + user2.Rows[0]["profilephoto"].ToString() + "/picture?type=large";
                            }
                        }
                        else
                        {
                            // imgprofile.Src = ConfigurationManager.AppSettings["adminsitepath"]+ "images/noprofile.jpg";
                            // removeprofile.Visible = false;
                        }
                        if (File.Exists(Server.MapPath("~/themes/creative1.0/images/coverphoto/" + user2.Rows[0]["coverphoto"].ToString())))
                        {
                            cimg = user2.Rows[0]["coverphoto"].ToString().Trim();
                            //imgcover.Src = ReturnUrl("sitepath") + "images/coverphoto/" + user2.Rows[0]["coverphoto"].ToString();
                        }
                        else
                        {
                            //imgcover.Visible = false;
                            //removecover.Visible = false;
                        }
                    }
                    else
                    {
                        // imgprofile.Visible = false;
                        //imgcover.Visible = false;
                    }

                    //  fillcountry();
                    country country = classaddress.GetCountryDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["country"].ToString()));
                    // ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByText(country.countryName.Trim()));

                    //  fillstate(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["country"].ToString()));
                    states state = classaddress.GetStateDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["state"].ToString()));
                    // ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByText(state.statename.Trim()));

                    //       fillcity(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["state"].ToString()));

                    city city = classaddress.GetCityDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["city"].ToString()));

                    //  ddlcity1.SelectedValue = ds_userdetails.Tables[0].Rows[0]["city"].ToString().Trim();
                    // txtcity.Text = ddlcity1.SelectedItem.Text.Trim();
                }
            }

        }
        catch (Exception ex)
        {

        }
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    #endregion

    SP_Methods spm = new SP_Methods();

    #region PageEvents
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionendR.aspx");
            }

            Label1.Text = "";
            lblmsg.Visible = false; ;
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "loginR.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Recruitment_index");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    Label1.Text = "";
                    editform.Visible = true;
                    divbtn.Visible = false;

                    hdnTravelConditionid.Value = "1";
                    hdnremid.Value = "0";
                    
                    GetEmployeeDetails();
                    txtEmpCode.Text = Convert.ToString(Session["Empcode"]);
       
                    if (Request.QueryString.Count > 0)
                    {
                        hdnclaimid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdnremid.Value = Convert.ToString(Request.QueryString[1]).Trim();
                    }
                    SettingSessions();
                    if (Convert.ToString(hdnremid.Value).Trim() != "0" && Convert.ToString(hdnremid.Value).Trim() != "")
                    {
                        
                        get_job_position_Details();
                        if (Request.QueryString.Count > 2)
                        {
                            InsertPosition_DatatoTempTables_trvl();

                        }
                        mobile_cancel.Visible = true;
                    }                    
                    
                    if (Convert.ToString(hdnMobRemStatus_dtls.Value).Trim() == "Approved")
                    {
                        mobile_btnSave.Visible = false;
                        mobile_cancel.Visible = false;
                    }
                    DisplayProfileProperties();
                    loadorder();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";

                    
                    get_jobsPositions_frmtmp();
                }
                else
                {
                    
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
            Response.Write(ex.Message.ToString());
          
        }

    }    

    protected void claimmob_btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/ManagejobPositions.aspx");
    }

    
    protected void mobile_btnSave_Click1(object sender, EventArgs e)
    {

        string[] strdate;
        string strfromDate="";
        string filename = "";
        String strfileName = "";
        string strclaimDate = "";


        #region Validation
        if (Convert.ToString(txt_title.Text).Trim() == "")
        {
            Label1.Text = "Please enter position title";
            return;
        }

        if (gv_candidates_functions.Rows.Count <=0 )
        {
            Label1.Text = "Please add positions";
            return;
        }

        //if (Convert.ToString(get_check_duplicate_PositionTitle()).Trim() != "")
        //{
        //    Label1.Text = "Please enter position title already Exists";
        //    return;
        //} 
        
        #endregion

     
        

        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            //return;
        }

        
        Label1.Text = "";

        
        DataTable dtMaxRempID = new DataTable();
        
        Decimal maxRemid = 0;
        hdnDept.Value = "0";
        hdnPosition.Value = "0";
        hdnLocation.Value = "0";        
        string strattend = "";

        string[] srecuirterCode;
        if (Convert.ToString(txt_Allotted_Recruiter.Text).Trim() != "")
        {
            srecuirterCode = Convert.ToString(txt_Allotted_Recruiter.Text).Trim().Split('-');
            if (srecuirterCode.Length>0)
            strattend = Convert.ToString(srecuirterCode[0]).Trim();
        }
        if (Convert.ToString(strattend).Trim()=="")
        {
            strattend = Convert.ToString(txtEmpCode.Text).Trim();
        }
        dtMaxRempID = spm.Insert_Recruitment_JobPosition(Convert.ToDecimal(hdnremid.Value), Convert.ToString(txt_title.Text).Trim(), Convert.ToString(txt_position_name.Text).Trim(), strattend, Convert.ToString(txtEmpCode.Text).Trim(), "insert");
        //spm.Insert_Recruitment_JobPosition(Convert.ToString(hdnremid.Value), Convert.ToString(txt_title.Text).Trim(), Convert.ToString(txt_position_name.Text).Trim(), strattend, Convert.ToString(txtEmpCode.Text).Trim(), "insert");

        maxRemid = Convert.ToDecimal(dtMaxRempID.Rows[0]["maxposid"]);

        if (maxRemid == 0)
            return;

        hdnremid.Value = Convert.ToString(maxRemid);
        

       
        Label1.Visible = true;
        Label1.Text = "Payment Voucher Reimbursement Reuqest Submitted Successfully";
        Response.Redirect("~/procs/Recruitment_index.aspx");
    }
       
    protected void lnkRemove_function_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int ifunctioid = Convert.ToInt32(gv_candidates_functions.DataKeys[row.RowIndex].Values[0]);
        int pos_srno = Convert.ToInt32(gv_candidates_functions.Rows[row.RowIndex].Cells[0].Text);
        AssigningSessions();
        Response.Redirect("addposition.aspx?clmid=" + pos_srno + "&rem_id=" + hdnremid.Value);
        
    }
    
     
    protected void lnkRemove_position_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;        
        
    }

    protected void addfunctions_Click(object sender, EventArgs e)
    {

        if (Convert.ToString(txt_title.Text).Trim() == "")
        {
            Label1.Text = "Please enter Title";
            return;
        }
        if (Convert.ToString(txt_position_name.Text).Trim() == "")
        {
            Label1.Text = "Please Select Position";
            return;
        }

        AssigningSessions();
        Response.Redirect("~/procs/addposition.aspx?j=0&posid=" + hdnremid.Value);
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        //hdnclaimid.Value = Convert.ToString(dgMobileClaim.DataKeys[row.RowIndex].Values[0]).Trim();
        //hdnremid.Value = Convert.ToString(dgMobileClaim.DataKeys[row.RowIndex].Values[1]).Trim();

        Response.Redirect("~/procs/Recruitment_Details.aspx?clmid=" + hdnclaimid.Value + "&remid=" + hdnremid.Value);
    }
    #endregion

    #region PageMethods

    public void AssigningSessions()
    {

        Session["postitle"] = Convert.ToString(txt_title.Text).Trim();
        Session["posDesp"] = Convert.ToString(txt_position_name.Text);
        Session["recruiterCode"] = Convert.ToString(hdnRecruiterCode.Value).Trim();

    }
    public void SettingSessions()
    {

        txt_title.Text = Convert.ToString(Session["postitle"]).Trim();
        txt_position_name.Text = Convert.ToString(Session["posDesp"]);
        // Session["recruiterCode"] = Convert.ToString(hdnRecruiterCode.Value).Trim();

    }

     public void GetEmployeeDetails()
     {
         try
         {
             DataTable dtEmpDetails = new DataTable();
             dtEmpDetails = spm.GetEmployeeData(txtEmpCode.Text);
             if (dtEmpDetails.Rows.Count > 0)
             {
                 txtEmpName.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
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

             throw;
         }
     }

     private void InsertPosition_DatatoTempTables_trvl()
     {
         try
         {

             SqlParameter[] spars = new SqlParameter[3];

             spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
             spars[0].Value = "sp_position_insert_mainData_toTempTabls";

             spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
             spars[1].Value = Convert.ToDecimal(hdnremid.Value);

             spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
             spars[2].Value = txtEmpCode.Text;

             DataTable dtMaxTripID = spm.InsertorUpdateData(spars, "[SP_Recruitment_Masters]");

         }
         catch (Exception ex)
         {
             Response.Write(ex.Message.ToString());
         }


     }
     public void get_jobsPositions_frmtmp()
     {
         DataSet tmpds_pv = new DataSet();
         try
         {
             SqlParameter[] spars = new SqlParameter[2];

             spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
             spars[0].Value = "sp_get_jobpositions_fromTmp";

             spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
             spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

             tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Masters");

             gv_candidates_functions.DataSource = null;
             gv_candidates_functions.DataBind();
             if (tmpds_pv.Tables[0].Rows.Count > 0)
             {
                 gv_candidates_functions.DataSource = tmpds_pv.Tables[0];
                 gv_candidates_functions.DataBind();
             }

         }
         catch (Exception ex)
         {
             Response.Write(ex.Message.ToString());

         }


     }


     public void get_job_position_Details()
     {
         DataSet tmpds_pv = new DataSet();
         try
         {
             SqlParameter[] spars = new SqlParameter[2];

             spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
             spars[0].Value = "sp_get_position_dtls";

             spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
             spars[1].Value = hdnremid.Value;

             tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Masters");

             if (tmpds_pv.Tables.Count > 0)
             {
                 if (tmpds_pv.Tables[0].Rows.Count > 0)
                 {
                     txt_title.Text = Convert.ToString(tmpds_pv.Tables[0].Rows[0]["position_tile"]);
                     txt_position_name.Text = Convert.ToString(tmpds_pv.Tables[0].Rows[0]["position_desp"]);
                     txt_Allotted_Recruiter.Text = Convert.ToString(tmpds_pv.Tables[0].Rows[0]["Recruiter_code"]);
                 }
             }


         }
         catch (Exception ex)
         {
             Response.Write(ex.Message.ToString());

         }

     }

     public string get_check_duplicate_PositionTitle()
     {
         string strpos = "";
         DataSet tmpds_pv = new DataSet();
         try
         {
             SqlParameter[] spars = new SqlParameter[2];

             spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
             spars[0].Value = "sp_check_duplicate_PositionTitle";

             spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
             spars[1].Value = Convert.ToString(txt_title.Text).Trim();

             tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Masters");

             if (tmpds_pv.Tables.Count > 0)
             {
                 if (tmpds_pv.Tables[0].Rows.Count > 0)
                 {
                    strpos="yes";
                 }
             }


         }
         catch (Exception ex)
         {
             Response.Write(ex.Message.ToString());

         }
         return strpos;
     }
     #endregion

     


     #region Search Employees
    
     [System.Web.Services.WebMethod]
     public static List<string> SearchRecruiter(string prefixText, int count)
     {

         using (SqlConnection conn = new SqlConnection())
         {
             conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

             using (SqlCommand cmd = new SqlCommand())
             {
                 string strsql = "";


                 /*strsql = " select Distinct location from addressbook where  " +
                                 "  location like   '%' + @SearchText + '%' order by location ";*/


                 /*strsql = "  Select t.locname from  ( " +
                        
                          "  Select loc_name as locname " +
                          "  from Req_Location  " +
                          "   ) t " +
                          "   where t.locname like '%' + @SearchText + '%'   Order by t.locname ";
                 */
                 strsql = "  select e.Emp_Code,e.Emp_Name, e.Emp_Code + ' - ' + e.Emp_Name as empname " +
                          " from Req_Auth_Users u inner join tbl_Employee_Mst e on u.emp_code=e.Emp_Code " +
                          " where is_active='Y' and e.Emp_Name like '%' + @SearchText + '%'  order by e.Emp_Name ";


                 cmd.CommandText = strsql;
                 cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                 //cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));
                 cmd.Connection = conn;
                 conn.Open();
                 List<string> employees = new List<string>();
                 using (SqlDataReader sdr = cmd.ExecuteReader())
                 {
                     while (sdr.Read())
                     {
                         employees.Add(sdr["empname"].ToString());
                     }
                 }
                 conn.Close();
                 return employees;
             }
         }
     }

     [System.Web.Services.WebMethod]
     public static List<string> SearchDepartment(string prefixText, int count)
     {

         using (SqlConnection conn = new SqlConnection())
         {
             conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

             using (SqlCommand cmd = new SqlCommand())
             {
                 string strsql = "";


                 /*strsql = " select Distinct location from addressbook where  " +
                                 "  location like   '%' + @SearchText + '%' order by location ";*/


                 strsql = "  Select dept_name " +
                          "  from Req_department  " +
                          "   where dept_name like '%' + @SearchText + '%'   Order by dept_name ";

                 cmd.CommandText = strsql;
                 cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                 //cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));
                 cmd.Connection = conn;
                 conn.Open();
                 List<string> employees = new List<string>();
                 using (SqlDataReader sdr = cmd.ExecuteReader())
                 {
                     while (sdr.Read())
                     {
                         employees.Add(sdr["dept_name"].ToString());
                     }
                 }
                 conn.Close();
                 return employees;
             }
         }
     }

     [System.Web.Services.WebMethod]
     public static List<string> SearchPosition(string prefixText, int count)
     {

         using (SqlConnection conn = new SqlConnection())
         {
             conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

             using (SqlCommand cmd = new SqlCommand())
             {
                 string strsql = "";


                 /*strsql = " select Distinct location from addressbook where  " +
                                 "  location like   '%' + @SearchText + '%' order by location ";*/


                 strsql = "  Select pos_name " +
                          "  from Req_position  " +
                          "   where pos_name like '%' + @SearchText + '%'   Order by pos_name ";

                 cmd.CommandText = strsql;
                 cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                 //cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));
                 cmd.Connection = conn;
                 conn.Open();
                 List<string> employees = new List<string>();
                 using (SqlDataReader sdr = cmd.ExecuteReader())
                 {
                     while (sdr.Read())
                     {
                         employees.Add(sdr["pos_name"].ToString());
                     }
                 }
                 conn.Close();
                 return employees;
             }
         }
     }
     #endregion

     



     
}
