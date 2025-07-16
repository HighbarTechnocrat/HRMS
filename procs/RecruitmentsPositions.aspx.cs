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

public partial class RecruitmentsPositions : System.Web.UI.Page
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
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionendR.aspx");
            }



            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "loginR.aspx");


            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "loginR.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Recruitment_index");
            }
            else
            {
                Page.SmartNavigation = true;
                strempcode = Session["Empcode"].ToString();
                hdnempcode.Value = Session["Empcode"].ToString();
                if (!Page.IsPostBack)
                {
                    //lblheading.Text = Convert.ToString(Session["emp_loginName"]).Trim(); 
                    editform.Visible = true;
                    get_Recuiter_isCreate_Position();

                    getMobileClaimDetails();
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
    protected void rptpositions_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RepeaterItem item = e.Item;
                //Reference the Controls.
                string customerId = (item.FindControl("lblposid") as Label).Text;
                Repeater tTable = (item.FindControl("rptdtppositions") as Repeater);
                if (Convert.ToString(customerId).Trim() != "")
                    get_dept_location_byposition(Convert.ToDecimal(customerId), tTable);
            }
        }
        catch (Exception ex)
        { }
    }
    #endregion

    #region Page Methods

    public void getMobileClaimDetails()
    {
        DataSet dtMobileDetails = new DataSet();

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_get_recuriter_position";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(strempcode).Trim();

        // spars[2] = new SqlParameter("@rem_id", SqlDbType.VarChar);
        //spars[2].Value = Convert.ToString(hdnremid.Value).Trim();


        dtMobileDetails = spm.getDatasetList(spars, "SP_Recruitment_Masters");

        rptpositions.DataSource = null;
        rptpositions.DataBind();

        if (dtMobileDetails.Tables[0].Rows.Count > 0)
        {
            rptpositions.DataSource = dtMobileDetails.Tables[0];
            rptpositions.DataBind();
        }

    }

    public string getFileUrl(object filename, object Recruitercode)
    {
        string strurl = "";
        try
        {

            if (filename != "")
            {
                if (Convert.ToString(hdnIsPostionCreator.Value).Trim() == "Y")
                {
                    if (Convert.ToString(hdnempcode.Value).Trim() == Convert.ToString(Recruitercode).Trim())
                        strurl = ReturnUrl("hccurlmain") + "/procs/RecruiterSearch.aspx?clmid=0&rem_id=" + filename + "&inb=0";
                    else
                        strurl = "#";
                }
                else
                {
                    strurl = ReturnUrl("hccurlmain") + "/procs/RecruiterSearch.aspx?clmid=0&rem_id=" + filename + "&inb=0";
                }
            }
           
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }

    public string getAttcehedUrl(object interviewid, object atchposid, object attachposno)
    {
        string strurl = "";
        try
        {

            if (interviewid != "")
            {
               // strurl = ReturnUrl("hccurlmain") + "/procs/viewattached_resume.aspx?clmid=0&rem_id=" + filename + "&inb=0";
                strurl = ReturnUrl("hccurlmain") + "/procs/Recruitments.aspx?clmid=0&rem_id=" + interviewid + "&inb=" + attachposno + "&posid=" + atchposid + "&tid=0";
               
            }
           
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }

    protected void get_dept_location_byposition(Decimal dposid, Repeater tmpTable)
    {
        try
        {

            #region Department & Location
            /*DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_get_dept_location_byposition";

            spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[2].Value = dposid;

            dsTrDetails = spm.getDatasetList(spars, "SP_Recruitment_Masters");
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                tmpTable.DataSource = dsTrDetails.Tables[0];
                tmpTable.DataBind();
            }*/
            #endregion

            DataSet dtMobileDetails = new DataSet();

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_get_attached_candidates_recuriter_Srch";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(strempcode).Trim();

            spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[2].Value = Convert.ToDecimal(dposid);


            dtMobileDetails = spm.getDatasetList(spars, "SP_Recruitment_Masters");

            
            if (dtMobileDetails.Tables[0].Rows.Count > 0)
            {
                tmpTable.DataSource = dtMobileDetails.Tables[0];
                tmpTable.DataBind();
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    private void get_Recuiter_isCreate_Position()
    {
        
        try
        {

            DataSet tmpds_pv = new DataSet();

            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_get_recruiter_isCreate_position";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(strempcode).Trim();

            tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Masters");

            hdnIsPostionCreator.Value = "N";
            if (tmpds_pv.Tables[0].Rows.Count > 0)
            {
                hdnIsPostionCreator.Value = Convert.ToString(tmpds_pv.Tables[0].Rows[0]["is_pos_creation"]).Trim();
            }

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
            spars[0].Value = "sp_delete_functins_Positions_tmp";

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


    protected void lbldownloadResume_Click(object sender, EventArgs e)
    {

        delete_tmp_Functions_positions();
        /*LinkButton btn = (LinkButton)sender;
        Repeater row = (Repeater)btn.NamingContainer;
        Label lblFilename = ((Label)row.FindControl("lbldownloadResume"));*/
        String strresume = "";
        LinkButton btn = (LinkButton)(sender);
        strresume = btn.CommandArgument;

        if (Convert.ToString(strresume).Trim() != "")
        {
            String strfilepath = "";            
            strfilepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["RecruitmentDocumentpath"]).Trim() + strresume);            
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
            Response.WriteFile(strfilepath);
            Response.End();
        }

 
    }
  
}