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

public partial class ManagejobPositions : System.Web.UI.Page
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
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/Recruitment_index");
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

                    // fillcountry();
                    country country = classaddress.GetCountryDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["country"].ToString()));
                    // ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByText(country.countryName.Trim()));

                    // fillstate(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["country"].ToString()));
                    states state = classaddress.GetStateDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["state"].ToString()));
                    // ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByText(state.statename.Trim()));

                    //  fillcity(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["state"].ToString()));

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
    #endregion

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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Recruitment_index");
            }
            else
            {
                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;

                    getMngMobileReqstList();
                    //   PopulateEmployeeLeaveData();

                    //getMngMobileReqstList();
                    
                    DisplayProfileProperties();

                    loadorder();
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
    protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnRemid.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
     //   hdnClaimsID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
        //Response.Redirect("TravelRequest.aspx?tripid=" + hdnTrip_Id.Value); clmid=1&remid=1
    //    delete_tmp_Functions_positions();
        Response.Redirect("positioncreation.aspx?clmid=0&rem_id=" + hdnRemid.Value + "&inb=0");
    }


   
    protected void btnTra_Details_Click(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        getMngMobileReqstList();
    }
    protected void claimmob_btnBack_Click(object sender, EventArgs e)
    {
        gvMngTravelRqstList.DataSource = null;
        gvMngTravelRqstList.DataBind();
    }
     
    #endregion

    #region Page Methods

   


     

    private void getMngMobileReqstList()
    {
        try
        { 

            DataSet tmpds_pv = new DataSet();
           
                SqlParameter[] spars = new SqlParameter[2];

                spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                spars[0].Value = "sp_get_positionlist_mng";
            
                //spars[0].Value = "sp_get_positionList_recuriter";

                spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
                spars[1].Value = Convert.ToString(strempcode).Trim();

                tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Masters");

                gvMngTravelRqstList.DataSource = null;
                gvMngTravelRqstList.DataBind();
                if (tmpds_pv.Tables[0].Rows.Count > 0)
                {
                    gvMngTravelRqstList.DataSource = tmpds_pv.Tables[0];
                    gvMngTravelRqstList.DataBind();
                }

            

           /* DataTable dtTravelRequest = new DataTable();
            dtTravelRequest = spm.get_Recruitment_MngList(strempcode);

            gvMngTravelRqstList.DataSource = null;
            gvMngTravelRqstList.DataBind();

            if (dtTravelRequest.Rows.Count > 0)
            {
                gvMngTravelRqstList.DataSource = dtTravelRequest;
                gvMngTravelRqstList.DataBind();
            }*/

        }
        catch (Exception ex)
        {

        }
    }

    protected void delete_tmp_Functions_positions()
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_get_jobpositions_byemp";

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = strempcode;

            dsTrDetails = spm.getDatasetList(spars, "SP_Recruitment_Masters");

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    #endregion 


    #region Search Employees

    [System.Web.Services.WebMethod]
    public static List<string> SearchLocations(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";


                /*strsql = " select Distinct location from addressbook where  " +
                                "  location like   '%' + @SearchText + '%' order by location ";*/


                strsql = "  Select t.locname from  ( " +
                    //"  Select CONCAT(loc_name,' - ',ltrim(RTRIM(str(loc_code)))) as locname " +
                         "  Select loc_name as locname " +
                         "  from Req_Location  " +
                         "   ) t " +
                         "   where t.locname like '%' + @SearchText + '%'   Order by t.locname ";



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
                        employees.Add(sdr["locname"].ToString());
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


                strsql = "  Select t.empname from  ( " +
                         " Select Emp_Name + ' - '  +Emp_Code as empname   " +
                         " from tbl_Employee_Mst   " +
                         " where Emp_Code in (select Distinct recruiter from Req_Candidate_Details) " +
                         " ) t  " +
                         " where t.empname like '%' + @SearchText + '%' Order by t.empname " ;

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

    #endregion
  
    
}