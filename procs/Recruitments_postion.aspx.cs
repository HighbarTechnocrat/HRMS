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

public partial class Recruitments_postion : System.Web.UI.Page
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
                    trvl_accmo_btn.Visible = false;

                    spn_release.Visible = false;                    
                    chkRealse.Visible = false;
                    spn_position_recuriter.Visible = false;
                    txt_position_by_recuriter.Visible = false;

                    hdnTravelConditionid.Value = "1";
                    hdnremid.Value = "0";
                    txtPosting_Location.Enabled = true;
                    GetEmployeeDetails();
                    txtEmpCode.Text = Convert.ToString(Session["Empcode"]);

                    //txt_functions.Visible = false;
                    //lnk_addFunctions.Visible = false;
                    //txt_positions.Visible = false;
                    //lnk_addPostions_tmp.Visible = false;
                    txtGender.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    Txt_Candidate_Name.Attributes.Add("onkeypress", "return onCharOnly(event);");
                    Txt_mobile.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    Txt_alternate_mobile.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    Txt_BirthDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    Txt_Experience.Attributes.Add("onkeypress", "return onCharOnlyNumber_e(event);");
                    
                    txt_age.Attributes.Add("onkeypress", "return onCharOnlyNumber_e(event);");
                    //GetMobileEligibility();
                    //GetTravelMode();
                    GetSource();
                    GetFuntions();
                    GetPositions();
                    hdnpageType.Value = "0";
                   // GetDepartment();
                    if (Request.QueryString.Count > 0)
                    {
                        hdnclaimid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdnremid.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        
                    }
                    if (Request.QueryString.Count == 3)
                    {
                        hdnpageType.Value =  Convert.ToString(Request.QueryString[2]).Trim();
                        
                        if (Convert.ToString(hdnpageType.Value).Trim() != "0" )
                        {
                            Session["attchposid"] = hdnclaimid.Value;//hdnpageType.Value;
                            txt_confidential_resume_recuriter.Visible = false;
                            chkConfidential.Visible = false;
                            idisconfidential.Visible = false;
                        }
                    }

                    if (Convert.ToString(hdnremid.Value).Trim() != "0" && Convert.ToString(hdnremid.Value).Trim() != "")
                    {
                        btnTra_Details.Visible = true;
                        spmdet.Visible = true;
                        dgMobileClaim.Visible = true;
                        mobile_cancel.Visible = true;
                        gvExtendRights.Visible = false;
                        getMobRemlsDetails_usingRemid();
                        get_Candiate_Qualification_Details();
                        //Check_IsRecruiter_ForExtend();

                        if (Convert.ToString(hdnpageType.Value).Trim() != "0")
                        {
                            get_Position_byRecuriter();
                        }

                        Check_Recruiter_ViewUpdate_rights();
                        if (Convert.ToString(hdnisRecuirterlogin.Value).Trim() == "true")
                        {
                            gvExtendRights.Visible = true;
                            getEmployeeList_extendRights();
                        }
                        if (Request.QueryString.Count > 2)
                        {
                            if (Convert.ToString(hdnclaimid.Value).Trim()=="0")
                            InsertMobileRem_DatatoTempTables_trvl();                            
                           
                        }

                        
                        DataSet dsfuelFiles = new DataSet();
                        dsfuelFiles = get_employee_FuelUploaded_Files(0);
                        gvfuel_pvFiles.DataSource = null;
                        gvfuel_pvFiles.DataBind();
                        if (dsfuelFiles.Tables[0].Rows.Count > 0)
                        {
                            gvfuel_pvFiles.DataSource = dsfuelFiles.Tables[0];
                            gvfuel_pvFiles.DataBind();
                        }
                        getMobileClaimDetails();
                    }

                    get_candidates_function_frmtmp();
                    get_candidates_position_frmtmp();

                    if (Convert.ToString(get_Recuiter_isConfidential_view()).Trim()=="Y")
                    {
                        chkConfidential.Visible = true;
                        idisconfidential.Visible = true;

                    }
                    if (Convert.ToString(hdnMobRemStatusM.Value).Trim() == "2" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "3" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "4")
                    {
                        /*mobile_btnSave.Visible = false;
                        mobile_cancel.Visible = false;
                        btnTra_Details.Visible = false;*/
                        // dgMobileClaim.Enabled = false;
                    }
                    if (Convert.ToString(hdnMobRemStatus_dtls.Value).Trim() == "Approved")
                    {
                        mobile_btnSave.Visible = false;
                        mobile_cancel.Visible = false;
                        btnTra_Details.Visible = false;
                    }

                    if(Convert.ToString(hdnpageType.Value).Trim()!="0")
                    {
                        spn_release.Visible = true;
                        chkRealse.Visible = true;
                        spn_position_recuriter.Visible = true;
                        txt_position_by_recuriter.Visible = true;
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

    protected void claimmob_btnBack_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(hdnpageType.Value).Trim() == "1")
            Response.Redirect("~/procs/positionslist.aspx");
        else
            Response.Redirect("~/procs/ManageRecruitments.aspx");
    }

    
    protected void mobile_btnSave_Click1(object sender, EventArgs e)
    {

        string[] strdate;
        string strfromDate="";
        string filename = "";
        String strfileName = "";
        string strclaimDate = "";
        string confidential_resume_recuriter = "";
        string strconfidential = "N";
        decimal dexprience = 0;

        #region Validation
        if (Convert.ToString(Txt_Candidate_Name.Text).Trim() == "")
        {
            Label1.Text = "Please enter Candidate Name";
            return;
        }

        if (Convert.ToString(Txt_email.Text).Trim() == "")
        {
            Label1.Text = "Please enter e-mail ID";
            return;

        }
        else
        {
            if (check_duplicates_Candidate() == true)
            {
                Label1.Text = "Candidate with the e-mail ID already Exisits.";
                return;
            }
        }
        if (Convert.ToString(Txt_mobile.Text).Trim() == "")
        {
            Label1.Text = "Please enter Mobile No.";
            return;
        }

        if (Convert.ToString(Txt_BirthDate.Text).Trim() == "")
        {
            Label1.Text = "Please select Birth Date";
            return;
        }

        if(Convert.ToString(txt_PGMastDo.Text).Trim()!="")
        {
            if(Convert.ToString(txt_PGMastDoc_Type.Text).Trim()=="")
            {
                Label1.Text = "Please select Type for PG/Master/Doctrate. ";
                return;
            }
        }
        if (Convert.ToString(txt_graduate.Text).Trim() != "")
        {
            if (Convert.ToString(txt_graduate_type.Text).Trim() == "")
            {
                Label1.Text = "Please select Type for Graduate. ";
                return;
            }
        }

        if (Convert.ToString(txt_diploma.Text).Trim() != "")
        {
            if (Convert.ToString(txt_diploma_type.Text).Trim() == "")
            {
                Label1.Text = "Please select Type for Dimploma. ";
                return;
            }
        }

        if (Convert.ToString(txt_others.Text).Trim() != "")
        {
            if (Convert.ToString(txt_others_type.Text).Trim() == "")
            {
                Label1.Text = "Please select Type for Others. ";
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

            Decimal dfare = 0;
            dfare = Convert.ToDecimal(Txt_Experience.Text);
            if (dfare == 0)
            {
                Label1.Text = "Please enter correct Exprience.";
                return;
            }
        }         

        if(chkConfidential.Checked==true)
        {
            strconfidential = "Y";

            /*if(Convert.ToString(txt_confidential_resume_recuriter.Text).Trim()=="")
            {
                Label1.Text = "Please select Recuriter Name. ";
                return;
            }


            string[] srecuirterCode;
            if (Convert.ToString(txt_confidential_resume_recuriter.Text).Trim() != "")
            {
                strconfidential = "Y";
                srecuirterCode = Convert.ToString(txt_confidential_resume_recuriter.Text).Trim().Split('-');
                if (srecuirterCode.Length > 0)
                {
                    strconfidential="Y";
                    confidential_resume_recuriter = Convert.ToString(srecuirterCode[0]).Trim();
                }
            */
        }

        if (Convert.ToString(hdnpageType.Value).Trim() != "0")
        {
            if (Convert.ToString(txt_position_by_recuriter.Text).Trim() != "")
            {
                get_candidate_forPositionID();

                if (Convert.ToString(hdnLocation.Value).Trim() == "0")
                {
                    
                     Label1.Text = "Please select Position by Recriter. ";
                     return;
                }
            }
        }
        #endregion

        #region 
        /*if (Convert.ToString(Txt_Dept.Text).Trim() == "")
        {
            Label1.Text = "Please select Department";
            return;
        }
        if (Convert.ToString(Txt_Position.Text).Trim() == "")
        {
            Label1.Text = "Please select Position";
            return;
        }

        if (Convert.ToString(txtPosting_Location.Text).Trim() == "")
        {
            Label1.Text = "Please select Posting Location";
            return;
        }*/
        #endregion

        

        string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
        if (confirmValue != "Yes")
        {
            //return;
        }
        Label1.Text = "";

        #region not required

        /*DataSet dtTripDev = new DataSet();
        dtTripDev = spm.get_Recruitment_Location_Code(txtPosting_Location.Text.Trim(), "get_Recruitment_Location_Code");
        if (dtTripDev.Tables[0].Rows.Count > 0)
        {
            hdnLocation.Value = Convert.ToString(dtTripDev.Tables[0].Rows[0][0]).Trim();
        }
        else
        {
            hdnLocation.Value = "0";
        }
        dtTripDev = new DataSet();
        dtTripDev = spm.get_Recruitment_Location_Code(Txt_Dept.Text.Trim(), "get_Recruitment_Dept_Code");
        if (dtTripDev.Tables[0].Rows.Count > 0)
        {
            hdnDept.Value = Convert.ToString(dtTripDev.Tables[0].Rows[0][0]).Trim();
        }
        else
        {
            hdnDept.Value = "0";
        }
        dtTripDev = new DataSet();
        dtTripDev = spm.get_Recruitment_Location_Code(Txt_Position.Text.Trim(), "get_Recruitment_Position_Code");
        if (dtTripDev.Tables[0].Rows.Count > 0)
        {
            hdnPosition.Value = Convert.ToString(dtTripDev.Tables[0].Rows[0][0]).Trim();
        }
        else
        {
            hdnPosition.Value = "0";
        }*/
        #endregion
        hdnLocation.Value = "0";
        hdnPosition.Value = "0";
        hdnDept.Value = "0";
        if (Convert.ToString(Txt_BirthDate.Text).Trim() != "")
        {
            strdate = Convert.ToString(Txt_BirthDate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        if (dgMobileClaim.Rows.Count > 0)
        {
            if (Convert.ToString(dgMobileClaim.Rows[0].Cells[0].Text) != "")
            {
                strdate = Convert.ToString(dgMobileClaim.Rows[0].Cells[0].Text).Trim().Split('/');
                strclaimDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
        }

        if (Convert.ToString(Txt_Experience.Text).Trim() != "")
        {
            dexprience = Math.Round(Convert.ToDecimal(Txt_Experience.Text), 2);
        }

        DataTable dtMaxRempID = new DataTable();
        
        Decimal maxRemid = 0;
       // hdnDept.Value = "0";
        hdnPosition.Value = "0";
       // hdnLocation.Value = "0";

        /*dtMaxRempID = spm.Insert_Recruitment_Candidate_Details(Convert.ToInt32(hdnremid.Value), Convert.ToInt32(hdnLocation.Value), Convert.ToInt32(hdnDept.Value), Txt_Candidate_Name.Text, Txt_email.Text,
                    Txt_alternate_email.Text, Txt_Address.Text, Convert.ToInt32(hdnPosition.Value), Convert.ToInt32(hdnSourceid.Value), "", Txt_mobile.Text, Txt_alternate_mobile.Text,
                    Txt_Qualification.Text, Txt_Profile.Text, "", Txt_Source_Details.Text, Txt_Experience.Text, strfromDate, txtGender.Text, txtEmpCode.Text,1);
         */
        if (Convert.ToString(hdnSourceid.Value).Trim() == "")
            hdnSourceid.Value = "0";

       

        ////dtMaxRempID = spm.Insert_Recruitment_Candidate_Details(Convert.ToDecimal(hdnremid.Value), Convert.ToInt32(hdnLocation.Value), Convert.ToInt32(hdnDept.Value), Txt_Candidate_Name.Text, Txt_email.Text,
        ////            Txt_alternate_email.Text, Txt_Address.Text, Convert.ToInt32(hdnPosition.Value), Convert.ToInt32(hdnSourceid.Value), "", Txt_mobile.Text, Txt_alternate_mobile.Text,
        ////            "", Txt_Profile.Text, "", Txt_Source_Details.Text, Convert.ToString(dexprience).Trim(), strfromDate, txtGender.Text, txtEmpCode.Text, 1, Convert.ToString(txt_age.Text).Trim(), Convert.ToString(txt_search_keywords.Text).Trim(), Convert.ToString(confidential_resume_recuriter).Trim(), strconfidential);
        
        
        maxRemid = Convert.ToDecimal(dtMaxRempID.Rows[0]["maxRemid"]);

        #region Update_Release_Candidate_FromPosition
        if (Convert.ToString(hdnpageType.Value).Trim() != "0")
        {
            string scandRelease = "N";
            Decimal dcanPosid = 0;
            Decimal d_deptid= 0;
            Decimal d_location = 0;
                        
                if (Convert.ToString(txt_position_by_recuriter.Text).Trim() != "")
                {
                    get_candidate_forPositionID();
                    if (Convert.ToString(hdnDept.Value).Trim() != "")
                        d_deptid = Convert.ToDecimal(hdnDept.Value);

                    if (Convert.ToString(hdnLocation.Value).Trim() != "")
                        d_location = Convert.ToDecimal(hdnLocation.Value);

                }
            

           // dcanPosid = get_candidate_forPositionID();

            if (Convert.ToString(hdnclaimid.Value).Trim() != "")
                dcanPosid = Convert.ToDecimal(hdnclaimid.Value);
            if (chkRealse.Checked)
                scandRelease = "Y";

            if (Convert.ToString(hdnpageType.Value).Trim() == "")
                hdnpageType.Value = "0";

            spm.Update_Release_Candidate_FromPosition(Convert.ToDecimal(hdnpageType.Value), Convert.ToString(txtEmpCode.Text).Trim(), scandRelease, dcanPosid, d_deptid, d_location,0);
        }
    #endregion


        if (maxRemid == 0)
            return;

        hdnremid.Value = Convert.ToString(maxRemid);
        if (dgMobileClaim.Rows.Count > 0)
        {
            spm.Insert_Recruitment_InterviewDetails(maxRemid, "", txtEmpCode.Text, "InsertMainTable", "", 0, 0, "", 0, 0, "", 0, "", "", "", "", "");
        }

        string strclaim_month = "";
        DateTime tdate;
        if (dgMobileClaim.Rows.Count > 0)
        {
            strfromDate = Convert.ToString(dgMobileClaim.Rows[0].Cells[0].Text).Trim();

            if (Convert.ToString(strfromDate).Trim() != "")
            {
                strdate = Convert.ToString(strfromDate).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                tdate = Convert.ToDateTime(strfromDate);
         
       strclaim_month = tdate.ToString("MMM-yy");
            }
        }

        #region instert Qualification
        //PG
        if (Convert.ToString(txt_PGMastDo.Text).Trim() != "")
            spm.Insert_Candidate_Qualification(maxRemid, Convert.ToString(txt_PGMastDo.Text).Trim(), Convert.ToString(txt_PGMastDoc_Type.Text).Trim(), Convert.ToString(txt_PGMastDo_institute.Text).Trim(), "Insert_Can_Qualification","PG");
        //Graduate
        if (Convert.ToString(txt_graduate.Text).Trim() !="")
            spm.Insert_Candidate_Qualification(maxRemid, Convert.ToString(txt_graduate.Text).Trim(), Convert.ToString(txt_graduate_type.Text).Trim(), Convert.ToString(txt_graduate_institute.Text).Trim(), "Insert_Can_Qualification","Graduate");

        //Diploma
        if (Convert.ToString(txt_diploma.Text).Trim() != "")
            spm.Insert_Candidate_Qualification(maxRemid, Convert.ToString(txt_diploma.Text).Trim(), Convert.ToString(txt_diploma_type.Text).Trim(), Convert.ToString(txt_diploma_institute.Text).Trim(), "Insert_Can_Qualification","Diploma");

        //Others
        if (Convert.ToString(txt_others.Text).Trim() != "")
            spm.Insert_Candidate_Qualification(maxRemid, Convert.ToString(txt_others.Text).Trim(), Convert.ToString(txt_others_type.Text).Trim(), Convert.ToString(txt_others_institute.Text).Trim(), "Insert_Can_Qualification","Others");

        #endregion

        #region insert or upload multiple files
        Int32 ifilesrno = 0;
        string FuelclaimPath = "";
        ifilesrno = get_Max_File_Srno(0);
        if (uploadfile.HasFile)
        {
            filename = uploadfile.FileName;
            if (Convert.ToString(filename).Trim() != "")
            {
                #region date formatting
                if (Convert.ToString(Txt_BirthDate.Text).Trim() != "")
                {
                    strdate = Convert.ToString(Txt_BirthDate.Text).Trim().Split('/');
                    strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
                }
                #endregion

                if (Convert.ToString(hdnremid.Value).Trim() != "0")
                    FuelclaimPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["RecruitmentDocumentpath"]).Trim());
                else
                    FuelclaimPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["RecruitmentDocumentpath"]).Trim() + "Recruitment_temp/" + Convert.ToString(txtEmpCode.Text) + "/");

                bool folderExists = Directory.Exists(FuelclaimPath);
                if (!folderExists)
                {
                    Directory.CreateDirectory(FuelclaimPath);
                }


                Boolean blnfile = false;
                HttpFileCollection fileCollection = Request.Files;
                for (int i = 0; i < fileCollection.Count; i++)
                {
                    strfileName = "";
                    HttpPostedFile uploadfileName = fileCollection[i];
                    string fileName = Path.GetFileName(uploadfileName.FileName);
                    if (uploadfileName.ContentLength > 0)
                    {
                        if (Convert.ToString(hdnremid.Value).Trim() != "0")
                            strfileName = hdnremid.Value + "_" + txtEmpCode.Text + "_" + Convert.ToString(0) + "_" + strfromDate + "_" + Convert.ToString(ifilesrno + 1).Trim() + "_" + uploadfileName.FileName;
                        else
                            strfileName = txtEmpCode.Text + "_" + Convert.ToString(0) + "_" + strfromDate + "_" + Convert.ToString(ifilesrno + 1).Trim() + "_" + uploadfileName.FileName;

                        filename = strfileName;
                        uploadfileName.SaveAs(Path.Combine(FuelclaimPath, strfileName));

                        if (Convert.ToString(hdnremid.Value).Trim() != "0")
                            spm.InsertpaymentUploaded_Files(Convert.ToDecimal(hdnremid.Value), blnfile, Convert.ToString(strfileName).Trim(), "Recruitment_insert", ifilesrno + 1, Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToInt32(0));
                        else
                            spm.InsertpaymentUploaded_Files(Convert.ToDecimal(hdnremid.Value), blnfile, Convert.ToString(strfileName).Trim(), "Recruitment_insertTmp", ifilesrno + 1, Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToInt32(0));
                        blnfile = true;
                        ifilesrno = ifilesrno + 1;
                    }
                }

            }


        }
        DataSet dpvfiles = new DataSet();
        dpvfiles = get_employee_FuelUploaded_Files(0);

        //Boolean blnfile = false;
        //if (dpvfiles.Tables[0].Rows.Count > 0)
        //{
        //    for (Int32 irow = 0; irow < dpvfiles.Tables[0].Rows.Count; irow++)
        //    {
        //        string strpvTmp_filepath = "";
        //        string str_Source_filename = "";
        //        string str_Destn_filename = "";
        //        ifilesrno = 0;
        //        Int32 iclaimid = 0;
        //        str_Source_filename = Convert.ToString(dpvfiles.Tables[0].Rows[irow]["file_name"]).Trim();
        //        str_Destn_filename = Convert.ToString(hdnremid.Value + "_" + str_Source_filename).Trim();
        //        ifilesrno = Convert.ToInt32(dpvfiles.Tables[0].Rows[irow]["file_sr_no"]);
        //        iclaimid = Convert.ToInt32(dpvfiles.Tables[0].Rows[irow]["claim_id"]);
        //        strpvTmp_filepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["RecruitmentDocumentpath"]).Trim() + "Recruitment_temp/" + Convert.ToString(txtEmpCode.Text) + "/" + Convert.ToString(str_Source_filename));

        //        string strpaymntV_filepath = "";
        //        strpaymntV_filepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["RecruitmentDocumentpath"]).Trim());


        //        bool folderExists = Directory.Exists(strpaymntV_filepath);
        //        if (!folderExists)
        //        {
        //            Directory.CreateDirectory(strpaymntV_filepath);
        //        }
        //        File.Copy(strpvTmp_filepath, strpaymntV_filepath + str_Destn_filename, true);

        //        spm.InsertpaymentUploaded_Files(Convert.ToDecimal(hdnremid.Value), blnfile, Convert.ToString(str_Destn_filename).Trim(), "Recruitment_insert", ifilesrno, Convert.ToString(txtEmpCode.Text).Trim(), iclaimid);

        //        blnfile = true;


        //    }


        //}

        //#region Delete files
        //FuelclaimPath = "";
        //FuelclaimPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["RecruitmentDocumentpath"]).Trim() + "Recruitment_temp/" + Convert.ToString(txtEmpCode.Text) + "/");
        ////else
        ////    n = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PaymentVoucherDocumentpath"]).Trim() + Convert.ToString(hdnremid.Value) + "/");

        //bool folderExists_T = Directory.Exists(FuelclaimPath);
        //if (!folderExists_T)
        //{
        //    Directory.CreateDirectory(FuelclaimPath);
        //}
        //string[] Files = Directory.GetFiles(FuelclaimPath);
        //foreach (string file in Files)
        //{
        //    File.Delete(file);
        //}


        //#endregion

        //spm.InsertpaymentUploaded_Files(Convert.ToDecimal(hdnremid.Value), false, Convert.ToString("").Trim(), "Recruitment_delete", 0, Convert.ToString(txtEmpCode.Text).Trim(), 0);



        #endregion

        if (Convert.ToString(hdnisRecuirterlogin.Value).Trim() == "true")
        {
            #region Inster Extend Rights


            String strsqlInsert = " Delete Req_Cand_Share where interview_id=" + hdnremid.Value + ";  Insert into  Req_Cand_Share(interview_id,emp_code,is_view,is_update) Values ";
            StringBuilder strsqValues = new StringBuilder();

            String isUpdate = "N";
            String isView = "N";
            strsqValues.Append(" ( " + hdnremid.Value + ",'" + txtEmpCode.Text + "','Y','Y' ) ");

            if (gvExtendRights.Rows.Count > 0)
            {


                for (Int32 irow = 0; irow < gvExtendRights.Rows.Count; irow++)
                {
                    isView = "N";
                    isUpdate = "N";

                    CheckBox chkExtend = (CheckBox)gvExtendRights.Rows[irow].FindControl("chkExtend");
                    CheckBox chkView = (CheckBox)gvExtendRights.Rows[irow].FindControl("chkView");
                    CheckBox chkUpdate = (CheckBox)gvExtendRights.Rows[irow].FindControl("chkUpdate");
                    String strempcode = Convert.ToString(gvExtendRights.DataKeys[irow].Value);

                    if (chkView.Checked == true)
                        isView = "Y";

                    if (chkUpdate.Checked == true)
                    {
                        isUpdate = "Y";
                        isView = "Y";
                    }


                    if (chkExtend.Checked == true)
                    {
                        if (Convert.ToString(strsqValues).Trim() == "")
                            strsqValues.Append(" ( " + hdnremid.Value + ",'" + strempcode + "','" + isView + "','" + isUpdate + "' ) ");
                        else
                            strsqValues.Append(" , ( " + hdnremid.Value + ",'" + strempcode + "','" + isView + "','" + isUpdate + "' ) ");
                    }
                }

            }
            spm.Insert_Recruitment_SQL(Convert.ToString(strsqlInsert) + Convert.ToString(strsqValues));
            #endregion
        }
        Label1.Visible = true;
        Label1.Text = "Payment Voucher Reimbursement Reuqest Submitted Successfully";
        Response.Redirect("~/procs/Recruitment_index.aspx");
    }


    protected void Txt_BirthDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Txt_BirthDate.Text).Trim() != "")
                checkFutureDates_ForSubmits();
            

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

    protected void lnkuploadRcpt_Click(object sender, EventArgs e)
    {
        try
        {
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ClaimsDocumentpath"]).Trim()), lnkuploadRcpt.Text);

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

    protected void lnkViewFiles_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        //hdnRemid.Value = Convert.ToString(dgFuelClaim.DataKeys[row.RowIndex].Values[0]).Trim();
        //string strfilename = Convert.ToString(hdnclaimid.Value) + "/" + Convert.ToString(gvfuel_pvFiles.Rows[row.RowIndex].Cells[0].Text).Trim();
        string strfilename = Convert.ToString(gvfuel_pvFiles.Rows[row.RowIndex].Cells[0].Text).Trim();

        //String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PaymentVoucherDocumentpath"]).Trim() + "paymentVoucher_temp/"), strfilename);
        String strfilepath = "";
        if (Convert.ToString(hdnremid.Value).Trim() != "0")
            strfilepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["RecruitmentDocumentpath"]).Trim() + strfilename);
        else
            strfilepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["RecruitmentDocumentpath"]).Trim() + "Recruitment_temp/" + Convert.ToString(txtEmpCode.Text) + "/" + strfilename);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
        Response.WriteFile(strfilepath);
        Response.End();

    }


    protected void lstGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtGender.Text = lstGender.SelectedItem.Text;
        PopupControlExtender4.Commit(lstGender.SelectedItem.Text);
    }
    protected void lst_Source_SelectedIndexChanged(object sender, EventArgs e)
    {
        Txt_Source.Text = lst_Source.SelectedItem.Text;
        PopupControlExtender5.Commit(lst_Source.SelectedItem.Text);
        SetSource_toHidden();
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetDeviation('" + hdnSourceid.Value + "');", true);
    }
    protected void lnkExtendRights_Click(object sender, EventArgs e)
    {
        getEmployeeList_extendRights();
    }
    
    protected void lst_functions_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_functions.Text = lst_functions.SelectedItem.Text;
        PopupControlExtender1.Commit(lst_functions.SelectedItem.Text);
        
    }
    protected void lst_positions_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_positions.Text = lst_positions.SelectedItem.Text;
        PopupControlExtender2.Commit(lst_positions.SelectedItem.Text);
    }
    protected void PGMastDoc_Type_SelectedIndexChanged(object sender, EventArgs e)
    {

        txt_PGMastDoc_Type.Text = lst_PGMastDoc_Type.SelectedItem.Text;
        PopupControlExtender3.Commit(lst_PGMastDoc_Type.SelectedItem.Text);
    }
    protected void lst_graduate_type_SelectedIndexChanged(object sender, EventArgs e)
    {

        txt_graduate_type.Text = lst_graduate_type.SelectedItem.Text;
        PopupControlExtender6.Commit(lst_graduate_type.SelectedItem.Text);
    }
    protected void lst_diploma_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_diploma_type.Text = lst_diploma_type.SelectedItem.Text;
        PopupControlExtender7.Commit(lst_diploma_type.SelectedItem.Text);
    }
    protected void lst_others_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_others_type.Text = lst_others_type.SelectedItem.Text;
        PopupControlExtender8.Commit(lst_others_type.SelectedItem.Text);
    }


    protected void addfunctions_Click(object sender, EventArgs e)
    {
        //txt_functions.Visible = true;
        //lnk_addFunctions.Visible = true;

        if (Convert.ToString(txt_functions.Text).Trim() == "")
        {
            return;
        }

        spm.Insert_Reqcuritment_Functions(Convert.ToDecimal(hdnremid.Value), lst_functions.SelectedItem.Text, Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToInt32(lst_functions.SelectedItem.Value), "insert_tmp_req_function");
        GetFuntions();
        txt_functions.Text = "";
        get_candidates_function_frmtmp();


    }
    protected void lnk_addFunctions_Click(object sender, EventArgs e)
    {
        
    }
    protected void lnkRemove_function_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int ifunctioid = Convert.ToInt32(gv_candidates_functions.DataKeys[row.RowIndex].Values[0]);
        Decimal interview_id = Convert.ToDecimal(gv_candidates_functions.DataKeys[row.RowIndex].Values[1]);
        spm.Insert_Reqcuritment_Functions(interview_id, "", Convert.ToString(txtEmpCode.Text).Trim(), ifunctioid, "delete_tmp_req_function");
        get_candidates_function_frmtmp();
    }
    protected void lnk_addPositions_Click(object sender, EventArgs e)
    {
     //   txt_positions.Visible = true;
      //  lnk_addPostions_tmp.Visible = true;

        if (Convert.ToString(txt_positions.Text).Trim() == "")
        {
            return;
        }

        spm.Insert_Reqcuritment_Functions(Convert.ToDecimal(hdnremid.Value), lst_positions.SelectedItem.Text, Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToInt32(lst_positions.SelectedItem.Value), "insert_tmp_req_Position");
        GetPositions();
        txt_positions.Text = "";
        get_candidates_position_frmtmp();

    }
    protected void lnk_addPostions_tmp_Click(object sender, EventArgs e)
    {
         
    }
    protected void lnkRemove_position_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int ipositinoid = Convert.ToInt32(gv_candidates_positions.DataKeys[row.RowIndex].Values[0]);
        Decimal interview_id = Convert.ToDecimal(gv_candidates_positions.DataKeys[row.RowIndex].Values[1]);
        spm.Insert_Reqcuritment_Functions(interview_id, "", Convert.ToString(txtEmpCode.Text).Trim(), ipositinoid, "delete_tmp_req_position");
        get_candidates_position_frmtmp();

    }
    #endregion

    #region PageMethods

    public void GetMobileEligibility()
    {

        DataTable dtApproverEmailIds = new DataTable();
        dtApproverEmailIds = spm.GetMobilelEligibilityMatrix_Emp_Grade(Convert.ToString(hdnGrade.Value), Convert.ToString(txtEmpCode.Text));
        //dtApproverEmailIds = spm.GetMobilelEligibilityMatrix(Convert.ToString(hdnGrade.Value));

        if (dtApproverEmailIds.Rows.Count > 0)
        {
            //txtElgAmnt.Text = Convert.ToString(dtApproverEmailIds.Rows[0]["Eligibility"]);
            //txtElgAmnt.Enabled = false;
        }
        else
        {
            //Label1.Visible = true;
            mobile_btnSave.Enabled = false;
            uploadfile.Enabled = false;
            uploadRcpt.Enabled = false;
            Label1.Text = "Sorry You are not entitled for Payment Voucher claims!";
        }
    }

    private void InsertMobileRem_DatatoTempTables_trvl()
    {
        try
        {

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_Recruitment_insert_mainData_toTempTabls";

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

    public void getMobRemlsDetails_usingRemid()
    {
        try
        {
            DataSet dtTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_get_Candidate_Details_From_Code";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Int);
            spars[1].Value = hdnremid.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;            
            dtTrDetails = spm.getDatasetList(spars, "[SP_Recruitment_Masters]");            
            if (dtTrDetails.Tables[0].Rows.Count > 0)
            {
                Txt_BirthDate.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["cand_BirthdateF"]);
                Txt_Candidate_Name.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["cand_name"]);
                if (Convert.ToString(dtTrDetails.Tables[0].Rows[0]["cand_gender"]) == "Male")
                    txtGender.Text = "Male";
                else
                    txtGender.Text = "Female";
                Txt_email.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["cand_email"]);
                Txt_alternate_email.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["cand_alternate_mail"]);
                Txt_mobile.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["cand_mob"]);
                Txt_alternate_mobile.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["cand_alternate_mob"]);
                Txt_Address.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["cand_address"]);
                Txt_Profile.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["cand_details"]);
                Txt_Qualification.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["cand_qual"]);
                Txt_Experience.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["cand_exp"]);
                Txt_Source.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["source_name"]);
                //lst_Source.SelectedItem.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["source_name"]);
                lst_Source.SelectedValue = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["cand_source"]);
                hdnSourceid.Value = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["cand_source"]);
                Txt_Source_Details.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["cand_source_details"]);
                txtPosting_Location.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["loc_name"]);
                Txt_Dept.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["dept_name"]);
                Txt_Position.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["pos_name"]);
                hdnLocation.Value = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["Job_Location"]);
                hdnDept.Value = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["for_department"]);
                hdnPosition.Value = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["job_position"]);
                hdnMobRemStatusM.Value = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["status"]);

                txt_age.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["cand_age"]);
                txt_search_keywords.Text = Convert.ToString(dtTrDetails.Tables[0].Rows[0]["srch_keywords"]);

                chkConfidential.Checked = false;
                if (Convert.ToString(dtTrDetails.Tables[0].Rows[0]["is_confidential"]).Trim() == "Y")
                {
                    chkConfidential.Checked = true;
                }


            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    }

    public Int32 get_Max_File_Srno(Int32 iclaimid)
    {
        DataSet tmpds_pv = new DataSet();
        int maxsr = 0;
        try
        {
            SqlParameter[] spars = new SqlParameter[5];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_RecruitmentFiles_MaxSrno";

            spars[1] = new SqlParameter("@filetype", SqlDbType.VarChar);
            spars[1].Value = "Recruitment_insertTmp";

            spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            spars[2].Value = hdnremid.Value;

            spars[3] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[3].Value = Convert.ToString(txtEmpCode.Text).Trim();

            spars[4] = new SqlParameter("@claimsid", SqlDbType.Int);
            spars[4].Value = iclaimid;

            tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Masters");
            if (tmpds_pv.Tables[0].Rows.Count > 0)
            {
                maxsr = Convert.ToInt32(tmpds_pv.Tables[0].Rows[0][0].ToString());
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
        return maxsr;

    }

    
     public void checkFutureDates_ForSubmits()
     {
         
         try
         {
           //  mobile_btnSave.Visible = true;
             Label1.Text = "";
             #region date formatting

             string[] strdate;
             string strfromDate = "";
             string strToDate = "";


             if (Convert.ToString(Session["BirthDate"]).Trim() != "")
             {
                 strdate = Convert.ToString(Txt_BirthDate.Text).Trim().Split('/');
                 strToDate = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
             }

             #endregion

             DataSet dsfuturedate = new DataSet();
             SqlParameter[] spars = new SqlParameter[2];

             spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
             spars[0].Value = "check_futurePV";

             spars[1] = new SqlParameter("@ClaimDate", SqlDbType.VarChar);
             spars[1].Value = strToDate;

             dsfuturedate = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

             if (dsfuturedate.Tables[0].Rows.Count > 0)
             {
                 if (Convert.ToString(dsfuturedate.Tables[0].Rows[0]["msg"]) != "")
                 {
                     Label1.Text = "Future date is not allowed. ";
                     Txt_BirthDate.Text = "";
                   //  mobile_btnSave.Visible = false;
                 }
             }
         }
         catch (Exception ex)
         {
             Response.Write(ex.Message.ToString());

         }

     }

     public void get_employee_FuelUploaded_Files()
     {
         try
         {


             DataSet dsfuelFiles = new DataSet();
             SqlParameter[] spars = new SqlParameter[5];

             spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
             spars[0].Value = "get_PaymntVocherFiles";

             spars[1] = new SqlParameter("@filetype", SqlDbType.VarChar);
             spars[1].Value = "paymentVoucher_insertTmp";

             spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
             spars[2].Value = hdnremid.Value;
             
             spars[3] = new SqlParameter("@empcode", SqlDbType.VarChar);
             spars[3].Value = Convert.ToString(txtEmpCode.Text).Trim();

             spars[4] = new SqlParameter("@claimsid", SqlDbType.Int);
             spars[4].Value = hdnclaimid.Value;

             
             dsfuelFiles = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

             gvfuel_pvFiles.DataSource = null;
             gvfuel_pvFiles.DataBind();
             if (dsfuelFiles.Tables[0].Rows.Count > 0)
             {
                 gvfuel_pvFiles.DataSource = dsfuelFiles.Tables[0];
                 gvfuel_pvFiles.DataBind();
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


             DataSet dsduplicateClaim = new DataSet();
             SqlParameter[] spars = new SqlParameter[4];

             spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
             spars[0].Value = "check_duplicate_Candidate";

             spars[1] = new SqlParameter("@email_main", SqlDbType.VarChar);
             spars[1].Value = Convert.ToString(Txt_email.Text).Trim();

             spars[2] = new SqlParameter("@email_alternate", SqlDbType.VarChar);
             if (Convert.ToString(Txt_alternate_email.Text).Trim() != "")
                 spars[2].Value = Convert.ToString(Txt_alternate_email.Text).Trim();
             else
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

             dsduplicateClaim = spm.getDatasetList(spars, "SP_Recruitment_Masters");

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

     public void getEmployeeList_extendRights()
     {

         try
         {   

             DataSet dsfuturedate = new DataSet();
             SqlParameter[] spars = new SqlParameter[3];

             spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
             spars[0].Value = "get_Recruiter_listForExtend";

             spars[1] = new SqlParameter("@apprempcode", SqlDbType.VarChar);
             spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

             spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
             spars[2].Value = Convert.ToString(hdnremid.Value).Trim();


             dsfuturedate = spm.getDatasetList(spars, "SP_Recruitment_Masters");
             gvExtendRights.DataSource = null;
             gvExtendRights.DataBind();
             if (dsfuturedate.Tables[0].Rows.Count > 0)
             {
                 gvExtendRights.DataSource = dsfuturedate.Tables[0];
                 gvExtendRights.DataBind();

                 for (int i = 0; i < dsfuturedate.Tables[0].Rows.Count; i++)
                 {
                     foreach (GridViewRow gvr in gvExtendRights.Rows)
                     {


                         CheckBox chkView = (CheckBox)gvr.FindControl("chkView");
                         CheckBox chkUpdate = (CheckBox)gvr.FindControl("chkUpdate");
                         CheckBox chkExtend = (CheckBox)gvr.FindControl("chkExtend");

                         String strempcode  = Convert.ToString(gvExtendRights.DataKeys[gvr.RowIndex].Value).Trim();

                         if (Convert.ToString(dsfuturedate.Tables[0].Rows[i]["emp_code"]).Trim() == strempcode)
                         {
                             if (Convert.ToString(dsfuturedate.Tables[0].Rows[i]["is_view"]).Trim() == "Y")
                             {
                                 chkView.Checked = true;
                                 chkExtend.Checked = true;
                             }
                             else
                             {
                                 chkView.Checked = false;
                             }

                             if (Convert.ToString(dsfuturedate.Tables[0].Rows[i]["is_update"]).Trim() == "Y")
                             {
                                 chkUpdate.Checked = true;
                                 chkExtend.Checked = true;
                             }
                             else
                             {
                                 chkUpdate.Checked = false;
                             }
                         }
                     }
                 }
             }
         }
         catch (Exception ex)
         {
             Response.Write(ex.Message.ToString());

         }

     }

     public void Check_IsRecruiter_ForExtend()
     {

         try
         {

             DataSet dsfuturedate = new DataSet();
             SqlParameter[] spars = new SqlParameter[3];

             spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
             spars[0].Value = "Check_IsRecruiter_ForExtend";

             spars[1] = new SqlParameter("@apprempcode", SqlDbType.VarChar);
             spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

             spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
             spars[2].Value = Convert.ToString(hdnremid.Value).Trim();


             dsfuturedate = spm.getDatasetList(spars, "SP_Recruitment_Masters");
             hdnisRecuirterlogin.Value = "false";
             if (dsfuturedate.Tables[0].Rows.Count > 0)
             {
                 trvl_accmo_btn.Visible = true;
                 hdnisRecuirterlogin.Value = "true";
             }
         }
         catch (Exception ex)
         {
             Response.Write(ex.Message.ToString());

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
                 mobile_btnSave.Visible = false;
                 btnTra_Details.Enabled = false;
                if (Convert.ToString(dsfuturedate.Tables[0].Rows[0]["is_update"]).Trim()=="Y")
                {
                    mobile_btnSave.Visible = true;
                    btnTra_Details.Enabled = true;
                }
             }
         }
         catch (Exception ex)
         {
             Response.Write(ex.Message.ToString());

         }

     }

     public DataSet get_employee_FuelUploaded_Files(Int32 iclaimid)
     {
         DataSet tmpds_pv = new DataSet();
         try
         {
             SqlParameter[] spars = new SqlParameter[5];

             spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
             spars[0].Value = "get_RecruitmentFiles";

             spars[1] = new SqlParameter("@filetype", SqlDbType.VarChar);
             spars[1].Value = "Recruitment_insertTmp";

             spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
             spars[2].Value = hdnremid.Value;

             spars[3] = new SqlParameter("@empcode", SqlDbType.VarChar);
             spars[3].Value = Convert.ToString(txtEmpCode.Text).Trim();

             spars[4] = new SqlParameter("@claimsid", SqlDbType.Int);
             spars[4].Value = 0;

             tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Masters");



         }
         catch (Exception ex)
         {
             Response.Write(ex.Message.ToString());

         }
         return tmpds_pv;

     }



     public void GetSource()
     {
         DataTable dtTripMode = new DataTable();
         //hdnTryiptypeid.Value
         //dtTripMode = spm.getTravelMode();
         dtTripMode = spm.getSource_List();

         if (dtTripMode.Rows.Count > 0)
         {
             lst_Source.DataSource = dtTripMode;
             lst_Source.DataTextField = "source_name";
             lst_Source.DataValueField = "source_id";
             lst_Source.DataBind();
         }
     }

     public void GetFuntions()
     {
         DataTable dtTripMode = new DataTable();         
         dtTripMode = spm.getFunction_List();
         lst_functions.Items.Clear();
         lst_functions.DataSource = null;
         lst_functions.DataBind();
         if (dtTripMode.Rows.Count > 0)
         {
             lst_functions.DataSource = dtTripMode;
             lst_functions.DataTextField = "function_name";
             lst_functions.DataValueField = "function_id";
             lst_functions.DataBind();
         }
     }

     public void GetPositions()
     {
         DataTable dtTripMode = new DataTable();
         dtTripMode = spm.getPosition_List();
         lst_positions.Items.Clear();
         lst_positions.DataSource = null;
         lst_positions.DataBind();
         if (dtTripMode.Rows.Count > 0)
         {
             lst_positions.DataSource = dtTripMode;
             lst_positions.DataTextField = "position_tile";
             lst_positions.DataValueField = "pos_id";
             lst_positions.DataBind();
         }
     }

     public void get_candidates_function_frmtmp()
     {
         DataSet tmpds_pv = new DataSet();
         try
         {
             SqlParameter[] spars = new SqlParameter[2];

             spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
             spars[0].Value = "sp_get_cadidates_Functions_List";

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

     public void get_candidates_position_frmtmp()
     {
         DataSet tmpds_pv = new DataSet();
         try
         {
             SqlParameter[] spars = new SqlParameter[2];

             spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
             spars[0].Value = "sp_get_cadidates_Positions_List";

             spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
             spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

             tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Masters");

             gv_candidates_positions.DataSource = null;
             gv_candidates_positions.DataBind();
             if (tmpds_pv.Tables[0].Rows.Count > 0)
             {
                 gv_candidates_positions.DataSource = tmpds_pv.Tables[0];
                 gv_candidates_positions.DataBind();
             }

         }
         catch (Exception ex)
         {
             Response.Write(ex.Message.ToString());

         }


     }

     public void SetSource_toHidden()
     {
         //DataSet dtTripDev = new DataSet();
         //dtTripDev = spm.getAccCode(Convert.ToInt32(lstComm_Type.SelectedValue));
         //if (dtTripDev.Tables[0].Rows.Count > 0)
         //{
         //    hdnDeviation.Value = Convert.ToString(dtTripDev.Tables[0].Rows[0]["pv_type"]).Trim();

         //}
         hdnSourceid.Value = Convert.ToString(Convert.ToInt32(lst_Source.SelectedValue));

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

     public void getMobileClaimDetails()
     {
         DataTable dtMobileDetails = new DataTable();
         dtMobileDetails = spm.GetRecruitmentDetails_Reqstpage(Convert.ToInt32(hdnremid.Value));

         dgMobileClaim.DataSource = null;
         dgMobileClaim.DataBind();

         if (dtMobileDetails.Rows.Count > 0)
         {
             //btnTra_Details.Visible = false;
             dgMobileClaim.DataSource = dtMobileDetails;
             dgMobileClaim.DataBind();

             #region Calulate Total Claim Amount
             hdnTravelConditionid.Value = "1";
             #endregion
         }

     }

     public void get_Candiate_Qualification_Details()
     {
         DataSet tmpds_pv = new DataSet();
         try
         {
             SqlParameter[] spars = new SqlParameter[2];

             spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
             spars[0].Value = "sp_get_candidate_Qualification";
             
             spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
             spars[1].Value = hdnremid.Value;

             tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Masters");

             txt_PGMastDo.Text = "";
             txt_PGMastDoc_Type.Text ="";
             txt_PGMastDo_institute.Text = "";

             txt_graduate.Text ="";
             txt_graduate_type.Text = "";
             txt_graduate_institute.Text = "";

             txt_diploma.Text = "";
             txt_diploma_type.Text = "";
             txt_diploma_institute.Text = "";

             txt_others.Text = "";
             txt_others_type.Text = "";
             txt_others_institute.Text = "";

             if(tmpds_pv.Tables.Count>0)
             {
                 if(tmpds_pv.Tables[0].Rows.Count>0)
                 {
                     txt_PGMastDo.Text = Convert.ToString(tmpds_pv.Tables[0].Rows[0]["qualification_name"]);
                     txt_PGMastDoc_Type.Text = Convert.ToString(tmpds_pv.Tables[0].Rows[0]["qualification_type"]);
                     txt_PGMastDo_institute.Text = Convert.ToString(tmpds_pv.Tables[0].Rows[0]["qualification_institute"]);
                 }

                 if (tmpds_pv.Tables[1].Rows.Count > 0)
                 {
                     txt_graduate.Text = Convert.ToString(tmpds_pv.Tables[1].Rows[0]["qualification_name"]);
                     txt_graduate_type.Text = Convert.ToString(tmpds_pv.Tables[1].Rows[0]["qualification_type"]);
                     txt_graduate_institute.Text = Convert.ToString(tmpds_pv.Tables[1].Rows[0]["qualification_institute"]);
                 }
                 if (tmpds_pv.Tables[2].Rows.Count > 0)
                 {
                     txt_diploma.Text = Convert.ToString(tmpds_pv.Tables[2].Rows[0]["qualification_name"]);
                     txt_diploma_type.Text = Convert.ToString(tmpds_pv.Tables[2].Rows[0]["qualification_type"]);
                     txt_diploma_institute.Text = Convert.ToString(tmpds_pv.Tables[2].Rows[0]["qualification_institute"]);
                 }

                 if (tmpds_pv.Tables[3].Rows.Count > 0)
                 {
                     txt_others.Text = Convert.ToString(tmpds_pv.Tables[3].Rows[0]["qualification_name"]);
                     txt_others_type.Text = Convert.ToString(tmpds_pv.Tables[3].Rows[0]["qualification_type"]);
                     txt_others_institute.Text = Convert.ToString(tmpds_pv.Tables[3].Rows[0]["qualification_institute"]);
                 }
             }


         }
         catch (Exception ex)
         {
             Response.Write(ex.Message.ToString());

         }
       
     }

     public void get_candidate_forPositionID()
     {
         DataSet tmpds_pv = new DataSet();
         hdnDept.Value = "0";
         hdnLocation.Value = "0";
         try
         {

             string[] srecuirterCode;
             String strDepartment = "";
             String strlocation = "";

             if (Convert.ToString(txt_position_by_recuriter.Text).Trim() != "")
             {
                 srecuirterCode = Convert.ToString(txt_position_by_recuriter.Text).Trim().Split(':');
                 if(srecuirterCode.Length >0)
                 {
                     strDepartment = Convert.ToString(srecuirterCode[0]).Trim();
                     strlocation = Convert.ToString(srecuirterCode[1]).Trim();
                 }
             }

             SqlParameter[] spars = new SqlParameter[4];

             spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
             spars[0].Value = "sp_get_dept_location_code_position_byRecuriter";

             spars[1] = new SqlParameter("@location_name", SqlDbType.VarChar);
             spars[1].Value = Convert.ToString(strlocation).Trim();

             spars[2] = new SqlParameter("@dept_name", SqlDbType.VarChar);
             spars[2].Value = Convert.ToString(strDepartment).Trim();

             spars[3] = new SqlParameter("@claimsid", SqlDbType.Decimal); // position id
             spars[3].Value = Convert.ToDecimal(hdnclaimid.Value);

             tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Masters");
             
             if (tmpds_pv.Tables[0].Rows.Count > 0)
             {
                 hdnDept.Value = Convert.ToString(tmpds_pv.Tables[0].Rows[0]["dept_id"]);
                 hdnLocation.Value = Convert.ToString(tmpds_pv.Tables[1].Rows[0]["loc_code"]);
             }


         }
         catch (Exception ex)
         {
             Response.Write(ex.Message.ToString());

         }
      

     }
    
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

    private void get_Position_byRecuriter()
     {
         
         try
         {

             DataSet tmpds_pv = new DataSet();

             SqlParameter[] spars = new SqlParameter[3];

             spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
             spars[0].Value = "sp_get_position_by_recuriter";


             spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
             spars[1].Value = Convert.ToDecimal(hdnpageType.Value);

             spars[2] = new SqlParameter("@claimsid", SqlDbType.Decimal);
             spars[2].Value = Convert.ToDecimal(hdnclaimid.Value);

             tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Masters");


             if (tmpds_pv.Tables[0].Rows.Count > 0)
             {
                 txt_position_by_recuriter.Text = Convert.ToString(tmpds_pv.Tables[0].Rows[0]["dept_location"]).Trim();
             }

         }
         catch (Exception ex)
         {

         }
         
     }
     #endregion

     protected void btnTra_Details_Click(object sender, EventArgs e)
     {
         if (Convert.ToString(Txt_Candidate_Name.Text).Trim() == "")
         {
             Label1.Text = "Please enter Candidate Name";
             return;
         }
         if (Convert.ToString(Txt_email.Text).Trim() == "")
         {
             Label1.Text = "Please enter Candidate E-mail";
             return;
         }
         if (Convert.ToString(Txt_mobile.Text).Trim() == "")
         {
             Label1.Text = "Please enter Candidate Mobile No.";
             return;
         }
         AssigningSessions();
         if (Convert.ToString(hdnpageType.Value).Trim() !="0")
             Response.Redirect("~/procs/Recruitment_Details.aspx?clmid="+hdnclaimid.Value+"&rem_id=" + hdnremid.Value + "&inb=" + hdnpageType.Value);
         else
         Response.Redirect("~/procs/Recruitment_Details.aspx?clmid=0&rem_id=" + hdnremid.Value);
     }

     

     public void AssigningSessions()
     {

         Session["Fromdate"] = Txt_BirthDate.Text;
         Session["ReqEmpCode"] = txtEmpCode.Text;
         Session["Grade"] = hflGrade.Value;
         Session["TrDays"] = hdnTrdays.Value;

         //Response.Write(Convert.ToString(Session["BirthDate"]));
         //Response.End();

     }

     protected void lnkEdit_Click(object sender, EventArgs e)
     {
         AssigningSessions();
         LinkButton btn = (LinkButton)sender;
         GridViewRow row = (GridViewRow)btn.NamingContainer;
         hdnclaimid.Value = Convert.ToString(dgMobileClaim.DataKeys[row.RowIndex].Values[0]).Trim();
         hdnremid.Value = Convert.ToString(dgMobileClaim.DataKeys[row.RowIndex].Values[1]).Trim();

         if (Convert.ToString(hdnpageType.Value).Trim() != "0")
             Response.Redirect("~/procs/Recruitment_Details.aspx?clmid=" + hdnclaimid.Value + "&rem_id=" + hdnremid.Value + "&inb=" + hdnpageType.Value);
         else
             Response.Redirect("~/procs/Recruitment_Details.aspx?clmid=" + hdnclaimid.Value + "&remid=" + hdnremid.Value);
     }


     protected void dgMobileClaim_RowCreated(object sender, GridViewRowEventArgs e)
     {
         if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
         {
             if (Convert.ToString(hdnMobRemStatusM.Value).Trim() == "2" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "3" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "4")
             {
                 e.Row.Cells[4].Visible = false;
             }
             else
             {
                 e.Row.Cells[4].Visible = true;
             }
             if (Convert.ToString(hdnMobRemStatus_dtls.Value).Trim() == "Approved")
             {
                 e.Row.Cells[4].Visible = false;
             }

         }
     }
         

    
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


                /* strsql = "  Select position_tile " +
                          "  from Req_job_position  " +
                          "   where position_tile like '%' + @SearchText + '%'   Order by position_tile ";*/
                 strsql = "  Select dept_location " +
                          "  from ( "   +
                          "  Select dept_name +'  :  '+ loc_name as dept_location "  +
                          "  from Req_job_position_dtls jp inner join Req_department d on d.dept_id=jp.dept_id " +
                          "  inner join Req_Location l on l.loc_code=jp.loc_code " +
                          "  where pos_id in ( select cand_attach_position from Req_Candiate_Position_attach_dtls where cand_attach_position='" + Convert.ToString(HttpContext.Current.Session["attchposid"]) + "')" + 
                          "  ) t " +
                          "   where t.dept_location like'%" + Convert.ToString(prefixText).Trim() + "%' order by  dept_location ";
                           


                 cmd.CommandText = strsql;
                 //cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                // cmd.Parameters.AddWithValue("@attchposid", Convert.ToString(HttpContext.Current.Session["attchposid"]));
                 cmd.Connection = conn;
                 conn.Open();
                 List<string> employees = new List<string>();
                 using (SqlDataReader sdr = cmd.ExecuteReader())
                 {
                     while (sdr.Read())
                     {
                         employees.Add(sdr["dept_location"].ToString());
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


                 /*strsql = "  Select t.locname from  ( " +
                        
                          "  Select loc_name as locname " +
                          "  from Req_Location  " +
                          "   ) t " +
                          "   where t.locname like '%' + @SearchText + '%'   Order by t.locname ";
                 */
                 strsql = "  select e.Emp_Code,e.Emp_Name, e.Emp_Code + '  - ' + e.Emp_Name as empname " +
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

     #endregion

     


         
}
