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



public partial class Appraisal_HODCalendar : System.Web.UI.Page
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

    public static string companyid = "";
    public static string locationid = "";
    public static string departmentid = "";
    public static string searchempid = "";
  
    


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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/travelindex");
            }
            else
            {
                Session["chkbtnStatus"] = "";

                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    //getMngSelfAssessList();
                    

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
    protected void lnkreset_Click(object sender, EventArgs e)
    {
        reset();
        getMngSelfAssessList();
    }
    public void reset()
    {
        txtcompanysearch.Text = "";
        txtlocation.Text = "";
        txtdepartmentsearch.Text = "";
        txtemployee.Text = "";
    }
    
    protected void gvMngSelfAssessList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "<div  style=\"font-weight:normal; \"> Employee  </div>";
            HeaderCell.ColumnSpan = 6;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);
          
            HeaderCell = new TableCell();
            HeaderCell.Text = "<div  style=\"font-weight:normal; \"> Self Assessment Status </div> ";
            HeaderCell.ColumnSpan = 1;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "<div  style=\"font-weight:normal; \"> Performance Review Status </div>";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "<div  style=\"font-weight:normal; \">Recommendations </div>";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell.CssClass = "font-weight: normal !important";

            HeaderCell.ColumnSpan = 3;
            HeaderGridRow.Cells.Add(HeaderCell);
            
            gvMngSelfAssessList.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }
    protected void gvMngSelfAssessList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnViewAtt = e.Row.FindControl("lnkLeaveDetails") as LinkButton;
            hdnyear.Value = gvMngSelfAssessList.DataKeys[e.Row.RowIndex].Values[1].ToString();
            lblheading.Text = "All Team Appraisals : " + Convert.ToString(hdnyear.Value).Trim();
            hdnAssessid.Value = gvMngSelfAssessList.DataKeys[e.Row.RowIndex].Values[0].ToString();
            if ((Convert.ToString(hdnAssessid.Value).Trim() == "0") || (Convert.ToString(hdnAssessid.Value).Trim() == ""))
            {
                lbtnViewAtt.Visible = false;
            }
            else
            {
                lbtnViewAtt.Visible = true;
                //if ((Convert.ToString(hdnstatus.Value).Trim() == "3"))
                //{
                //    lnkLeaveDetails.Text = "Submitted";
                //    lnkLeaveDetails.Attributes.Add("onclick", "return false;");
                //}
                //else
                //{
                //    lnkLeaveDetails.Text = "Draft";
                //}


            }
        }
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "AppraisalTeamCalendar_" + Convert.ToString(hdnyear.Value).Trim() + "_" + DateTime.Now + ".xls";
        
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

        gvMngSelfAssessList.GridLines = GridLines.Both;
        gvMngSelfAssessList.HeaderStyle.Font.Bold = true;
        gvMngSelfAssessList.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();



    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }


    #endregion



    #region Page Methods

    



    
  

    protected void lnksearch_Click(object sender, EventArgs e)
    {
        getMngSelfAssessList();
    }

    private void getMngSelfAssessList()
    {
        try
        {
            
            if (txtcompanysearch.Text.Length > 0 && txtcompanysearch.Text.ToString() != "")
            {
                companyid = txtcompanysearch.Text.ToString().ToUpper();
            }
            else
            {
                companyid =  "";
            }
            if (txtlocation.Text.Length > 0 && txtlocation.Text.ToString() != "")
            {
                locationid = txtlocation.Text.ToString().ToUpper();
            }
            else
            {
                locationid = "";
            }
            if (txtdepartmentsearch.Text.Length > 0 && txtdepartmentsearch.Text.ToString() != "")
            {
                departmentid = txtdepartmentsearch.Text.ToString().ToUpper();
            }
            else
            {
                departmentid = "";
            }
            if (txtemployee.Text.Length > 0 && txtemployee.Text.ToString() != "")
            {
                searchempid = txtemployee.Text.ToString().ToUpper();
            }
            else
            {
                searchempid = "";
            }
            
            DataTable dtSelfAssess = new DataTable();
            dtSelfAssess = spm.getSelfAssess_HODList(strempcode, "TeamcalenderHod", companyid, locationid, departmentid, searchempid);

            gvMngSelfAssessList.DataSource = null;
            gvMngSelfAssessList.DataBind();

            if (dtSelfAssess.Rows.Count > 0)
                {
                    gvMngSelfAssessList.DataSource = dtSelfAssess;
                    gvMngSelfAssessList.DataBind();
                }
            
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdnAssessid.Value = Convert.ToString(gvMngSelfAssessList.DataKeys[row.RowIndex].Values[0]).Trim();
        hdnAssessStatus.Value = Convert.ToString(gvMngSelfAssessList.DataKeys[row.RowIndex].Values[2]).Trim();
        hdnAssessTyp.Value = "RevWerHd";
        ////if (Convert.ToString(hdnAssessStatus.Value) == "6")  //6=N1Pending
        ////{
        ////Response.Redirect("Appraisal_SelfAssessment.aspx?8787hghj767=" + hdnAssessid.Value + "&mndfbmdbf=RevWerEd");     
        //Response.Redirect("Appraisal_SelfAssessment.aspx?1232ghghg=" + hdnAssessid.Value + "&dfjk78hjdf=" + hdnAssessStatus.Value + "&mfngndfhfgnj=" + hdnAssessTyp.Value);
        ////}
        ////if (Convert.ToString(hdnAssessStatus.Value) == "7") //N1Submitted
        ////{
        ////    Response.Redirect("Appraisal_SelfAssessment.aspx?1232ghghg=" + hdnAssessid.Value + "&dfjk78hjdf=N2&mfngfdfnfgnj=" + hdnAssessTyp.Value);
        ////}
        ////if (Convert.ToString(hdnAssessStatus.Value) == "8") //8=N2Submitted
        ////{
        ////    Response.Redirect("Appraisal_SelfAssessment.aspx?1232ghghg=" + hdnAssessid.Value + "&dfjk78hjdf=AD1&dddf54544ddd=" + hdnAssessTyp.Value);
        ////}

       
        if (Convert.ToString(hdnAssessStatus.Value) == "4") //8=N2Submitted
        {
            Response.Redirect("Appraisal_SelfAssessment.aspx?1232ghghg=" + hdnAssessid.Value + "&dfjk78hjdf=AD1VP&dddf54544ddd=" + hdnAssessTyp.Value);
        }

    }

    #endregion 



    #region Search Location,Department,Company
    //[System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
    [System.Web.Services.WebMethod]
    public static List<string> SearchDepartments(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["AppraisalConnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";
              

                strsql = " select Distinct Department from tbl_Employee_Mst where  " +
                                " Department!= '0' and   department like  '%' + @SearchText + '%' order by department ";

                cmd.CommandText = strsql;
                cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["department"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }

    
    [System.Web.Services.WebMethod]
    public static List<string> SearchCompaniess(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["AppraisalConnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";


                strsql = " select Distinct company_name from tbl_hmst_company where isactive='A' and " +
                                "  company_name like   '%' + @SearchText + '%' and company_name != 'Hindustan Construction Co. Ltd.' order by company_name ";

                cmd.CommandText = strsql;
                cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                customers.Add("Hindustan Construction Co. Ltd.");
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["company_name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }

    [System.Web.Services.WebMethod]
    public static List<string> SearchLocations(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["AppraisalConnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";


                strsql = " select Distinct Location_name from Appr_Mst_Emp_Position_Mapp pm   " +
                         " inner join tbl_hmst_company cm on pm.Location_Code=cm.comp_code  " +
                         " where Location_name like   '%' + @SearchText + '%' order by Location_name ";

                cmd.CommandText = strsql;
                cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Location_name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
    
    
     [System.Web.Services.WebMethod]
    public static List<string> SearchEmployee(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["AppraisalConnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";


                strsql = " select Emp_Code +' - '+Emp_Name as ename ,Emp_Code,Emp_Name from tbl_Employee_Mst    " +                        
                         " where pyear = (select pyear from Appr_period where isactive='Active') and Emp_Name like   '%' + @SearchText + '%'  "+
                         " or Emp_Code like   '%' + @SearchText + '%'  " +
                         " order by Emp_Name ";

                cmd.CommandText = strsql;
                cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["Emp_Name"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }
    #endregion


     protected void gvMngSelfAssessList_PageIndexChanging(object sender, GridViewPageEventArgs e)
     {
         getMngSelfAssessList();

         gvMngSelfAssessList.PageIndex = e.NewPageIndex;
         gvMngSelfAssessList.DataBind();
     }
}
