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


public partial class Recruitment_addqualification : System.Web.UI.Page
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

            lblmessage.Text = "";
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
                    lblmessage.Text = "";
                    editform.Visible = true;
                    divbtn.Visible = false;


                    txtQualificationType.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txt_PGMastDocGrdother_Type.Attributes.Add("onkeypress", "return noanyCharecters(event);");

                    accmo_delete_btn.Visible = false;      

                    txtEmpCode.Text = Convert.ToString(Session["ReqEmpCode"]);
                    hdnGrade.Value = Convert.ToString(Session["Grade"]);
                    hdnposid.Value = "0";
                    
                    if (Request.QueryString.Count > 0)
                    {
                        hdnclaimqry.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdnClaimid.Value = Convert.ToString(Request.QueryString[0]).Trim();

                        hdnremid.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        hdnCommtypeidO.Value = Convert.ToString(Request.QueryString[0]).Trim();

                    }
                    if (Request.QueryString.Count > 2)
                    {
                        hdnAttachPosNo.Value = Convert.ToString(Request.QueryString[2]).Trim();
                        hdnposid.Value = Convert.ToString(Request.QueryString[3]).Trim();
                    }

                    if (Convert.ToString(hdnClaimid.Value).Trim() != "0")
                    {
                        accmo_delete_btn.Visible = true;
                        getQualificationDetails();
                    }

                    if (Convert.ToString(hdnremid.Value).Trim() != "0")
                    {
                        Check_Recruiter_ViewUpdate_rights();
                    }

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

    protected void claimmob_btnSubmit_Click(object sender, EventArgs e)
    {
      try
      {
          if (Convert.ToString(txtQualificationType).Trim() == "")
          {
              lblmessage.Text = "Please Select Qualification Type";
              return;
          }

          if (Convert.ToString(txt_qualificationName.Text).Trim() == "")
          {
              lblmessage.Text = "Please enter Qualification Name";
              return;
          }

          if (Convert.ToString(txt_PGMastDocGrdother_Type.Text).Trim() == "")
          {
              lblmessage.Text = "Please Select  Type";
              return;
          }

          hdnsptype.Value = "InsertTempTable_Qualification";
          if (Convert.ToString(hdnClaimid.Value).Trim() != "" && Convert.ToString(hdnClaimid.Value).Trim() != "0")
              hdnsptype.Value = "updateTempTable_Qualification";

          spm.Insert_Recruitment_Candidate_QualificationDtls(Convert.ToDouble(hdnremid.Value), Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(txt_qualificationName.Text).Trim(), Convert.ToString(txtQualificationType.Text).Trim(), Convert.ToInt32(hdnClaimid.Value), Convert.ToString(txtpremiuminstitute.Text).Trim(), Convert.ToString(txt_PGMastDocGrdother_Type.Text).Trim(), Convert.ToString(hdnsptype.Value).Trim());


          if (Convert.ToString(hdnposid.Value).Trim() != "0")
              Response.Redirect("~/procs/Recruitments.aspx?clmid=" + hdnClaimid.Value + "&rem_id=" + hdnremid.Value + "&inb=" + hdnAttachPosNo.Value + "&posid=" + hdnposid.Value);
          else
              Response.Redirect("~/procs/Recruitments.aspx?clmid=" + hdnClaimid.Value + "&rem_id=" + hdnremid.Value);


      }
        catch(Exception ex)
      {

      }
    }

    protected void claimmob_btnBack_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(hdnposid.Value).Trim() != "0")
            Response.Redirect("~/procs/Recruitments.aspx?clmid=" + hdnClaimid.Value + "&rem_id=" + hdnremid.Value + "&inb=" + hdnAttachPosNo.Value + "&posid=" + hdnposid.Value);
        else
            Response.Redirect("~/procs/Recruitments.aspx?clmid=" + hdnClaimid.Value + "&rem_id=" + hdnremid.Value);
    }


    protected void accmo_delete_btn_Click(object sender, EventArgs e)
    {

        hdnsptype.Value = "deleteTempTable_Qualification";
        spm.Insert_Recruitment_Candidate_QualificationDtls(Convert.ToDouble(hdnremid.Value), Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToString(txt_qualificationName.Text).Trim(), Convert.ToString(txtQualificationType.Text).Trim(), Convert.ToInt32(hdnClaimid.Value), Convert.ToString(txtpremiuminstitute.Text).Trim(), Convert.ToString(txt_PGMastDocGrdother_Type.Text).Trim(), Convert.ToString(hdnsptype.Value).Trim());

        if (Convert.ToString(hdnAttachPosNo.Value).Trim() != "0" && Convert.ToString(hdnAttachPosNo.Value).Trim() != "")
            Response.Redirect("~/procs/Recruitments.aspx?clmid=" + hdnCommtypeid.Value + "&rem_id=" + hdnremid.Value + "&inb=" + hdnAttachPosNo.Value);
        else
            Response.Redirect("~/procs/Recruitments.aspx?clmid=" + hdnCommtypeid.Value + "&rem_id=" + hdnremid.Value);

    }


    protected void lstQualificationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtQualificationType.Text = lstQualificationType.SelectedItem.Text;
        PopupControlExtender4.Commit(lstQualificationType.SelectedItem.Text);
    }

    protected void lst_PGMastDoc_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_PGMastDocGrdother_Type.Text = lst_PGMastDoc_Type.SelectedItem.Text;
        PopupControlExtender5.Commit(lst_PGMastDoc_Type.SelectedItem.Text);
    }

  

    #endregion

    #region PageMethods
    private void getQualificationDetails()
    {

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "getQualification_details_edit";

        spars[1] = new SqlParameter("@claimsid", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(hdnClaimid.Value);

        spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[2].Value = Convert.ToString(txtEmpCode.Text);

        spars[3] = new SqlParameter("@rem_id", SqlDbType.VarChar);
        spars[3].Value = Convert.ToString(hdnremid.Value);

        dsTrDetails = spm.getDatasetList(spars, "SP_Recruitment_Masters");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            txt_qualificationName.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["qualification_name"]).Trim();
            txt_PGMastDocGrdother_Type.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["pg_grdute_others_type"]).Trim();
            lst_PGMastDoc_Type.SelectedValue = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["pg_grdute_others_type"]);
            txtpremiuminstitute.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["qualification_institute"]).Trim();


            txtQualificationType.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["qualification_type"]).Trim();
            lstQualificationType.SelectedValue = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["qualification_type"]);


         
        }


    }

    public void Check_Recruiter_ViewUpdate_rights()
    {

        try
        {

            DataSet dsfuturedate = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Check_Recruiter_ViewUpdate_rights";

            spars[1] = new SqlParameter("@apprempcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

            spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[2].Value = Convert.ToString(hdnremid.Value).Trim();


            dsfuturedate = spm.getDatasetList(spars, "SP_Recruitment_Masters");

            if (dsfuturedate.Tables[0].Rows.Count > 0)
            {
                claimmob_btnSubmit.Visible = false;
                accmo_delete_btn.Visible = false;
                if (Convert.ToString(dsfuturedate.Tables[0].Rows[0]["is_update"]).Trim() == "Y")
                {
                    claimmob_btnSubmit.Visible = true;
                    accmo_delete_btn.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }

    }


    public Boolean check_duplicates_Claims()
    {
        Boolean blnCheckDuplicate = false;
        try
        {


            DataSet dsduplicateClaim = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "check_duplicate_claims";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

            spars[2] = new SqlParameter("@claimsid", SqlDbType.Int);
            spars[2].Value = hdnCommtypeid.Value;

            dsduplicateClaim = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            if (dsduplicateClaim.Tables[0].Rows.Count > 0)
            {
                blnCheckDuplicate = true;
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
        return blnCheckDuplicate;

    }

    #endregion

    
    [System.Web.Services.WebMethod]
    public static List<string> SearchEmployees(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";

                strsql = "  Select t.empname from  ( " +
                         "  Select Emp_Name + ' - '  +Emp_Code as empname " +
                         "  from tbl_Employee_Mst  " +
                         "   ) t " +
                         "   where t.empname like '%' + @SearchText + '%' Order by t.empname ";

                cmd.CommandText = strsql;
                cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
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

}
