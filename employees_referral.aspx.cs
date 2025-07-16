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
using Microsoft.Reporting.WebForms;


public partial class employees_referral : System.Web.UI.Page
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

    #region page Events

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            
            lblmsg.Visible = false; ;
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "employees_referral.aspx");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {

                    

                    editform.Visible = true;
                    divbtn.Visible = false;
                    txtEmpCode.Text = Convert.ToString(Session["Empcode"]);

                    hdnTravelConditionid.Value = "1";



                    txtGender.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    Txt_Candidate_Name.Attributes.Add("onkeypress", "return onCharOnly(event);");
                    Txt_Experience.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txt_curntCTC.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");


                    uploadfile.Attributes.Add("onchange", "return checkFileExtension(this);");

                }
                    
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
            Response.Write(ex.Message.ToString());

        }

    }    

    protected void lstGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtGender.Text = lstGender.SelectedItem.Text;
        PopupControlExtender4.Commit(lstGender.SelectedItem.Text);
    }

    protected void Txt_BirthDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Txt_BirthDate.Text).Trim() != "")
                getCandidate_Age();
            
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    }


    protected void lnkuplodedfile_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ClaimsDocumentpath"]).Trim()), lnkuplodedfile.Text);

            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
            Response.WriteFile(strfilepath);
            Response.End();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }


    protected void fuel_btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtMaxRempID = new DataTable();

            string[] strdate;
            string strfromDate = "";
            hdnremid.Value = "0";
            string filename = "";

            decimal dexprience = 0;
            Int32 dexprience_Months = 0;
            decimal dCrntCTC = 0;
            decimal dage = 0;


            #region Validation
            if (Convert.ToString(Txt_Candidate_Name.Text).Trim() == "")
            {
                Label1.Text = "Please enter Candidate Name";
                return;
            }
            if (Convert.ToString(Txt_BirthDate.Text).Trim() == "")
            {
                Label1.Text = "Please select Birth Date";
                return;
            }

            if (Convert.ToString(Txt_email.Text).Trim() == "")
            {
                Label1.Text = "Please enter e-mail ID";
                return;

            }

            if (Convert.ToString(Txt_mobile.Text).Trim() == "")
            {
                Label1.Text = "Please enter Mobile No.";
                return;
            }
            if (Convert.ToString(Txt_Experience.Text).Trim() == "")
            {
                Label1.Text = "Please enter Experience (Years).";
                return;
            }


            if (check_duplicates_Candidate() == true)
            {
                Label1.Text = "Candidate already Exisits.";
                return;
            }

            if (Convert.ToString(txt_age.Text).Trim() != "")
            {
                strdate = Convert.ToString(txt_age.Text).Trim().Split('.');
                if (strdate.Length > 2)
                {
                    Txt_Experience.Text = "0";
                    Label1.Text = "Please enter correct Age.";
                    return;
                }

                if (strdate.Length > 1)
                {
                    if (Convert.ToInt32(strdate[1]) > 11)
                    {
                        Label1.Text = "Please enter correct Age.";
                        return;
                    }
                }

                Decimal dfare = 0;
                dfare = Convert.ToDecimal(txt_age.Text);
                if (dfare == 0)
                {
                    Label1.Text = "Please enter correct Age.";
                    return;
                }

                if (Convert.ToInt32(Convert.ToString(txt_age.Text).Trim().Length) != 5)
                {
                    Label1.Text = "Please enter correct Age.";
                    return;
                }
            }



            if (Convert.ToString(Txt_Experience.Text).Trim() != "")
            {
                strdate = Convert.ToString(Txt_Experience.Text).Trim().Split('.');
                if (strdate.Length > 2)
                {
                    Txt_Experience.Text = "0";
                    Label1.Text = "Please enter correct Exprience.";
                    return;
                }

                if (strdate.Length > 1)
                {
                    if (Convert.ToInt32(strdate[1]) > 11)
                    {
                        Label1.Text = "Please enter correct Exprience.";
                        return;
                    }
                }



                Decimal dfare = 0;
                dfare = Convert.ToDecimal(Txt_Experience.Text);
                if (dfare == 0)
                {
                    Label1.Text = "Please enter correct Exprience.";
                    return;
                }

                if (dfare >= 60)
                {
                    Label1.Text = "Exprience in years should be less than 60 years.";
                    return;
                }
             

                Decimal dageV = 0;
                if (Convert.ToString(txt_age.Text).Trim() != "")
                {
                    dageV = Convert.ToDecimal(txt_age.Text);
                    if (dageV <= dfare)
                    {
                        Label1.Text = "Exprience in years should be less than Age.";
                        return;
                    }
                }
            }


            if (Convert.ToString(txt_curntCTC.Text).Trim() != "" && Convert.ToString(txt_curntCTC.Text).Trim() != "0.00" && Convert.ToString(txt_curntCTC.Text).Trim() != "0")
            {
                strdate = Convert.ToString(txt_curntCTC.Text).Trim().Split('.');
                if (strdate.Length > 2)
                {
                    txt_curntCTC.Text = "0";
                    Label1.Text = "Please enter correct Current CTC.";
                    return;
                }

                if (strdate.Length > 1)
                {
                    if (Convert.ToInt32(strdate[1]) > 99)
                    {
                        Label1.Text = "Please enter correct Current CTC.";
                        return;
                    }
                }

                Decimal dfare = 0;
                dfare = Convert.ToDecimal(txt_curntCTC.Text);
                if (dfare == 0)
                {
                    Label1.Text = "Please enter correct Current CTC.";
                    return;
                }


            }


            if (! uploadfile.HasFile)
            {
                Label1.Text = "Please select candidate Resume .";
                return;
            }
            
            #endregion


            if (Convert.ToString(Txt_Experience.Text).Trim() != "")
            {
                dexprience = Math.Round(Convert.ToDecimal(Txt_Experience.Text), 2);
                strdate = Convert.ToString(Txt_Experience.Text).Trim().Split('.');
                if (Convert.ToString(strdate[0]).Trim() != "")
                {
                    dexprience_Months = Convert.ToInt32(strdate[0]) * 12;
                    dexprience_Months = dexprience_Months + Convert.ToInt32(strdate[1]);

                }
            }


            if (Convert.ToString(txt_age.Text).Trim() != "")
            {
                dage = Math.Round(Convert.ToDecimal(txt_age.Text), 2);
            }

            if (Convert.ToString(txt_curntCTC.Text).Trim() != "")
            {
                dCrntCTC = Math.Round(Convert.ToDecimal(txt_curntCTC.Text), 2);
            }

            filename = uploadfile.FileName;
            if (Convert.ToString(Txt_BirthDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(Txt_BirthDate.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            Decimal maxRemid = 0;
           dtMaxRempID= spm.Insert_Recruitment_Candidate_Details(Convert.ToDecimal(hdnremid.Value), 0, 0, Convert.ToString(Txt_Candidate_Name.Text).Trim(), Convert.ToString(Txt_email.Text).Trim(), "", Convert.ToString(Txt_Address.Text).Trim(), 0, 0, "",
                                                    Convert.ToString(Txt_mobile.Text), "", "", "", filename, "", Convert.ToString(Txt_Experience.Text).Trim(), Convert.ToString(strfromDate).Trim(), Convert.ToString(txtGender.Text).Trim(),
                                                    Convert.ToString(txtEmpCode.Text).Trim(), 0, Convert.ToString(txt_age.Text).Trim(), "", "", "", Convert.ToString(dCrntCTC).Trim(),Convert.ToString(dexprience_Months).Trim());

           maxRemid = Convert.ToDecimal(dtMaxRempID.Rows[0]["maxRemid"]);
          
            #region insert or upload multiple files
            Int32 ifilesrno = 0;
            string FuelclaimPath = "";
            string strfileName = "";
           
            if (uploadfile.HasFile)
            {   
                filename = uploadfile.FileName;
                if (Convert.ToString(filename).Trim() != "")
                {

                    FuelclaimPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["employeeReferralResumes"]).Trim());

                    Boolean blnfile = false;
                    HttpFileCollection fileCollection = Request.Files;
                    for (int i = 0; i < fileCollection.Count; i++)
                    {
                        strfileName = "";
                        HttpPostedFile uploadfileName = fileCollection[i];
                        string fileName = Path.GetFileName(uploadfileName.FileName);
                        if (uploadfileName.ContentLength > 0)
                        { 
                             
                                strfileName =   uploadfileName.FileName;
                            

                            filename = strfileName;
                            uploadfileName.SaveAs(Path.Combine(FuelclaimPath, strfileName));
                         
                            ifilesrno = ifilesrno + 1;

                             

                        }
                    }

                }


            }
    
         

            #endregion


            #region set false
             
                 Label1.Text = "Resume Upload Successfully.";
              
            Txt_Candidate_Name.Enabled = false;
            txtGender.Enabled = false;
            Txt_BirthDate.Enabled = false;
            Txt_email.Enabled = false;
            Txt_mobile.Enabled = false;
            Txt_Experience.Enabled = false;
            txt_curntCTC.Enabled = false;
            Txt_Address.Enabled = false;
            fuel_btnSave.Visible = false;
            #endregion
        }
        catch(Exception ex)
        {

        }
    }

    #endregion


    #region page Method
    public void getCandidate_Age()
    {

        try
        {
            //  mobile_btnSave.Visible = true;
            Label1.Text = "";
            #region date formatting

            string[] strdate;
            string strToDate = "";


            if (Convert.ToString(Txt_BirthDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(Txt_BirthDate.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
            }

            #endregion

            DataSet dsfuturedate = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_getCandidate_Age";

            spars[1] = new SqlParameter("@formatdate", SqlDbType.VarChar);
            spars[1].Value = strToDate;

            dsfuturedate = spm.getDatasetList(spars, "SP_Recruitment_Masters");

            txt_age.Text = "";
            if (dsfuturedate.Tables[0].Rows.Count > 0)
            {
                txt_age.Text = Convert.ToString(dsfuturedate.Tables[0].Rows[0]["AgeYearsIntRound"]).Trim();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }

    }

    public Boolean check_duplicates_Candidate()
    {
        Boolean blnCheckDuplicate = false;
        try
        {
            string[] strdate;
            string strfromDate = "";
            if (Convert.ToString(Txt_BirthDate.Text).Trim() != "")
            {
                strdate = Convert.ToString(Txt_BirthDate.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }

            DataSet dsduplicateClaim = new DataSet();
            SqlParameter[] spars = new SqlParameter[7];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "check_duplicate_Candidate_emp_referral";

            spars[1] = new SqlParameter("@email_main", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(Txt_email.Text).Trim();

            spars[2] = new SqlParameter("@email_alternate", SqlDbType.VarChar);
            spars[2].Value = DBNull.Value;

            if (Request.QueryString.Count > 0)
            {
                if (Convert.ToString(Request.QueryString[1]).Trim() != "" || Convert.ToString(Request.QueryString[1]).Trim() != "0")
                {
                    spars[3] = new SqlParameter("@rem_id", SqlDbType.Int);
                    spars[3].Value = Convert.ToInt32(Convert.ToString(Request.QueryString[1]).Trim());
                }
                else
                {
                    spars[3] = new SqlParameter("@rem_id", SqlDbType.Int);
                    spars[3].Value = null;
                }
            }
            else
            {
                spars[3] = new SqlParameter("@rem_id", SqlDbType.Int);
                spars[3].Value = null;
            }

            spars[4] = new SqlParameter("@cand_Birthdate", SqlDbType.VarChar);
            if (Convert.ToString(Txt_BirthDate.Text).Trim() != "")
                spars[4].Value = Convert.ToString(strfromDate).Trim();
            else
                spars[4].Value = DBNull.Value;

            spars[5] = new SqlParameter("@cand_mob", SqlDbType.VarChar);
            if (Convert.ToString(Txt_mobile.Text).Trim() != "")
                spars[5].Value = Convert.ToString(Txt_mobile.Text).Trim();
            else
                spars[5].Value = DBNull.Value;


            spars[6] = new SqlParameter("@cand_alternate_mob", SqlDbType.VarChar);
            spars[6].Value = DBNull.Value;


            dsduplicateClaim = spm.getDatasetList_Recruitment(spars, "SP_Recruitment_Masters");

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
    
}

 



    
