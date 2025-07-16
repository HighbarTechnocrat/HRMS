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

public partial class Recruiter_assignment : System.Web.UI.Page
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
                    if (Convert.ToString(hdnremid.Value).Trim() != "0" && Convert.ToString(hdnremid.Value).Trim() != "")
                    {   
                       
                    }

                    get_Recruiters_dtls();
                    //get_candidates_function_frmtmp();


                    if (Convert.ToString(hdnMobRemStatusM.Value).Trim() == "2" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "3" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "4")
                    {

                    }
                    if (Convert.ToString(hdnMobRemStatus_dtls.Value).Trim() == "Approved")
                    {
                        mobile_btnSave.Visible = false;
                        mobile_cancel.Visible = false;
                        
                    }
                    
                    txt_fromdate.Enabled = false;
                    txt_todate.Enabled = false;


                    DisplayProfileProperties();
                    loadorder();
                    
                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
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
        // Response.Redirect("~/procs/positionslist.aspx");
        Response.Redirect("~/procs/Recruitment_index.aspx");
    }

    protected void mobile_btnSave_Click1(object sender, EventArgs e)
    {
        try
        {
             
            DataSet tmpds_pv = new DataSet();

            for (Int32 irow = 0; irow < gv_position_DptLocation.Rows.Count; irow++)
            {

                CheckBox chkConfidentialRecruiter = ((CheckBox)gv_position_DptLocation.Rows[irow].FindControl("chkConfidentialRecruiter"));
                CheckBox chkPositionCreationRecruiter = ((CheckBox)gv_position_DptLocation.Rows[irow].FindControl("chkPositionCreationRecruiter"));

                Label lblConfidentialRecruiter = ((Label)gv_position_DptLocation.Rows[irow].FindControl("lblConfidentialRecruiter"));
                Label lblPositionCreationRecruiter =((Label)gv_position_DptLocation.Rows[irow].FindControl("lblPositionCreationRecruiter"));

                string  strRecruiterCode="0";
                string strConfidentialRecruiter = "N";
                string strPositionCreationRecruiter = "N";

                strRecruiterCode = Convert.ToString(gv_position_DptLocation.DataKeys[irow].Values[0]).Trim();

                if (chkConfidentialRecruiter.Checked)
                {
                    strConfidentialRecruiter = "Y";
                }

                if (chkPositionCreationRecruiter.Checked)
                {
                    strPositionCreationRecruiter = "Y";
                }
                                
                    SqlParameter[] spars = new SqlParameter[4];
                
                    spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                    spars[0].Value = "recruiter_Assignments";

                    spars[1] = new SqlParameter("@isConfidentialRecruiter", SqlDbType.VarChar);
                    spars[1].Value = Convert.ToString(strConfidentialRecruiter).Trim();

                    spars[2] = new SqlParameter("@isPosCreationRecruiter", SqlDbType.VarChar);
                    spars[2].Value = Convert.ToString(strPositionCreationRecruiter).Trim();

                    spars[3] = new SqlParameter("@recruiter", SqlDbType.VarChar);
                    spars[3].Value = Convert.ToString(strRecruiterCode).Trim();

                    tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Candidate_Insert");
                
            }
            lblmsg.Visible = true;
            lblmsg.Text = "Recruiter Assigment updated Successfully";
            
            
        }
        catch (Exception ex)
        {

        }
    }

    

    #endregion


    #region PageMethods

  

    private string get_Recuiter_isConfidential_view()
    {
        string strYesNo = "N";
        try
        {

            DataSet tmpds_pv = new DataSet();

            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_get_confidential_recruiter";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

            tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Masters");


            if (tmpds_pv.Tables[0].Rows.Count > 0)
            {
                strYesNo = Convert.ToString(tmpds_pv.Tables[0].Rows[0]["is_confidential_recuriter"]).Trim();
            }

        }
        catch (Exception ex)
        {

        }
        return strYesNo;
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

     

    private void get_attachCandidate_dept_location()
    {

        try
        {

            DataSet tmpds_pv = new DataSet();

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_get_attachCandidate_dept_location";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

            spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            if (Convert.ToString(hdnremid.Value).Trim() != "")
                spars[2].Value = Convert.ToDecimal(hdnremid.Value);
            else
                spars[2].Value = 0;

            tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Masters");

            if (tmpds_pv.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(tmpds_pv.Tables[0].Rows[0]["nopositions"]).Trim() == Convert.ToString(tmpds_pv.Tables[0].Rows[0]["fullfill"]).Trim())
                {
                    mobile_btnSave.Visible = false;
                    
                }
            }

        }
        catch (Exception ex)
        {

        }

    }
          

    #endregion


    protected void get_Recruiters_dtls()
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_get_recruiter_dtls";

            spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[2].Value = hdnremid.Value;

            dsTrDetails = spm.getDatasetList(spars, "SP_Recruitment_Masters");
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                gv_position_DptLocation.Visible = true;
                gv_position_DptLocation.DataSource = dsTrDetails.Tables[0];
                gv_position_DptLocation.DataBind();
            }



        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }



    protected void gv_position_DptLocation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkConfidentialRecruiter = (CheckBox)e.Row.FindControl("chkConfidentialRecruiter");
            CheckBox chkPositionCreationRecruiter = (CheckBox)e.Row.FindControl("chkPositionCreationRecruiter");

            Label lblConfidentialRecruiter = (Label)e.Row.FindControl("lblConfidentialRecruiter");
            Label lblPositionCreationRecruiter = (Label)e.Row.FindControl("lblPositionCreationRecruiter");

            chkConfidentialRecruiter.Checked = false;
            chkPositionCreationRecruiter.Checked = false;

            if (Convert.ToString(lblConfidentialRecruiter.Text).Trim()=="Y")
            {
                chkConfidentialRecruiter.Checked = true;
            }

            if (Convert.ToString(lblPositionCreationRecruiter.Text).Trim() == "Y")
            {
                chkPositionCreationRecruiter.Checked = true;
            }
        }
    }
}
