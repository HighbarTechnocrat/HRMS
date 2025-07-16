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


public partial class Recruitment_Details : System.Web.UI.Page
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
                    
                
                    accmo_delete_btn.Visible = false;
                    txtFromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    Txt_Time.Attributes.Add("onkeypress", "return onCharOnlyNumber_time(event);");

                    txtEmpCode.Text = Convert.ToString(Session["ReqEmpCode"]);
                    hdnGrade.Value = Convert.ToString(Session["Grade"]);
                    //txtAmount.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");

                    //GetMobileEligibility();
                    //txtElgAmnt.Text = "0";
                    hdnposid.Value = "0";
                    GetComm_Type();
                    GetComm_Way("1,2,3,4,5,0");
                    GetStatus();
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
                    
                    if (Convert.ToString(hdnclaimqry.Value).Trim() != "0")
                    {
                        accmo_delete_btn.Visible = true;
                        getClaimDetails();
                        get_employee_FuelUploaded_Files();
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

    protected void claimmob_btnBack_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(hdnposid.Value).Trim() != "0")
            Response.Redirect("~/procs/Recruitments.aspx?clmid=" + hdnClaimid.Value + "&rem_id=" + hdnremid.Value + "&inb=" + hdnAttachPosNo.Value + "&posid=" + hdnposid.Value);
        else
            Response.Redirect("~/procs/Recruitments.aspx?clmid=" + hdnClaimid.Value + "&rem_id=" + hdnremid.Value);
    }

    protected void claimmob_btnSubmit_Click1(object sender, EventArgs e)
    {

        string[] strdate;
        string strfromDate="";
        string filename = "";
        string rfilename = "";
        String strfileName = "";
        String strRfileName = "";

        if (Convert.ToString(lstComm_Type.SelectedValue).Trim() == "")
        {
            lblmessage.Text = "Please Select Activity";
            return;
        }

        if (Convert.ToString(lst_Way.SelectedValue).Trim() == "")
        {
            lblmessage.Text = "Please Select Sub-Type";
            return;
        }

        if (Convert.ToString(txtFromdate.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter Date";
            return;
        }

        if (Convert.ToString(Txt_Time.Text).Trim() != "")
        {
            strdate = Convert.ToString(Txt_Time.Text).Trim().Split(':');
            if (strdate.Length == 2)
            {
                if (strdate[0].Length != 2 || strdate[1].Length != 2)
                {
                    lblmessage.Text = "Please enter correct Time.";
                    return;
                }
                else
                {
                    if (Convert.ToInt32(strdate[0].ToString()) > 24)
                    {
                        lblmessage.Text = "Please enter correct Time.";
                        return;
                    }
                    if (Convert.ToInt32(strdate[1].ToString()) > 59)
                    {
                        lblmessage.Text = "Please enter correct Time.";
                        return;
                    }
                }
            }
            else
            {
                lblmessage.Text = "Please enter correct Time.";
                return;
            }
        }

        if (uploadfile.HasFile)
        {
            filename = uploadfile.FileName;
        }

        if (Convert.ToString(lstComm_Type.SelectedValue).Trim() == "3")
        {
            if (Convert.ToString(lnkuplodedfile.Text).Trim() == "")
            {
                if (Convert.ToString(filename).Trim() == "")
                {
                    lblmessage.Text = "Please upload Offer Letter";
                    return;
                }
            }
        }


        if (Convert.ToString(hdnStatusid.Value).Trim()=="")
        {
            lblmessage.Text = "Please Select Status";
            return;
        }
     
        

        #region date formatting
        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

        }
        #endregion

      

        //if (Convert.ToString(rfilename).Trim() != "")
        //{
        //    rfilename = uploadRcpt.FileName;
        //    strRfileName = "";
        //    strRfileName = txtEmpCode.Text + "_Receipt_" + strfromDate + "_" + uploadRcpt.FileName;
        //    rfilename = strRfileName;
        //    uploadRcpt.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ClaimsDocumentpath"]).Trim()), strRfileName));
        //}
        string tpanel1 = "";
        string tpanel2 = "";
        string tpanel3 = "";
        string tpanel4 = "";
        string tpanel5 = "";
        string tpanel6 = "";

        String[] stremp;
        if (TxtPanel1.Text != "")
        {
            stremp = Convert.ToString(TxtPanel1.Text).Split('-');
            tpanel1 = Convert.ToString(stremp[1]).Trim();
        }
        if (TxtPanel2.Text != "")
        {
            stremp = Convert.ToString(TxtPanel2.Text).Split('-');
            tpanel2 = Convert.ToString(stremp[1]).Trim();
        }
        if (TxtPanel3.Text != "")
        {
            stremp = Convert.ToString(TxtPanel3.Text).Split('-');
            tpanel3 = Convert.ToString(stremp[1]).Trim();
        }
        if (TxtPanel4.Text != "")
        {
            stremp = Convert.ToString(TxtPanel4.Text).Split('-');
            tpanel4 = Convert.ToString(stremp[1]).Trim();
        }
        if (TxtPanel5.Text != "")
        {
            stremp = Convert.ToString(TxtPanel5.Text).Split('-');
            tpanel5 = Convert.ToString(stremp[1]).Trim();
        }
        if (TxtPanel6.Text != "")
        {
            stremp = Convert.ToString(TxtPanel6.Text).Split('-');
            tpanel6 = Convert.ToString(stremp[1]).Trim();
        }

        hdnsptype.Value = "InsertTempTable";
        if (Convert.ToString(hdnclaimqry.Value).Trim() != "" && Convert.ToString(hdnclaimqry.Value).Trim() != "0")
            hdnsptype.Value = "updateTempTable";
        hdnCommtypeid.Value = lstComm_Type.SelectedValue;
        spm.Insert_Recruitment_InterviewDetails(Convert.ToInt32(hdnremid.Value), strfromDate, txtEmpCode.Text, hdnsptype.Value, filename, Convert.ToInt32(hdnCommtypeidO.Value), Convert.ToInt32(hdnCommtypeid.Value), Txt_Time.Text, Convert.ToInt32(hdnCommtypeid.Value), Convert.ToInt32(hdnWayid.Value), Txt_Details.Text, Convert.ToInt32(hdnStatusid.Value), tpanel1, tpanel2, tpanel3, tpanel4, tpanel5);
        //spm.InsertPaymentVoucherDetails(Convert.ToInt32(hdnremid.Value), strfromDate, enteredamount, txtEmpCode.Text, Convert.ToDecimal(0), hdnsptype.Value, hdnDeviation.Value, filename, hdnCommtypeid.Value, "",Convert.ToString(hdnCommtypeidO.Value).Trim());


        #region insert or upload multiple files
        Int32 ifilesrno = 0;
        ifilesrno = get_Max_File_Srno(0);
        if (uploadfile.HasFile)
        {
            filename = uploadfile.FileName;
            //uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["FuelClaimsDocumentpath"]).Trim()), uploadfile.FileName));
            if (Convert.ToString(filename).Trim() != "")
            {
                #region date formatting
                if (Convert.ToString(txtFromdate.Text).Trim() != "")
                {
                    strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
                    strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
                }
                #endregion



                string FuelclaimPath = "";
                if (Convert.ToString(hdnremid.Value).Trim() != "0")
                    FuelclaimPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["RecruitmentDocumentpath"]).Trim());
                else
                    FuelclaimPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["RecruitmentDocumentpath"]).Trim() + "Recruitment_temp/" + Convert.ToString(txtEmpCode.Text) + "/");
                //else
                //    FuelclaimPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PaymentVoucherDocumentpath"]).Trim() + Convert.ToString(hdnremid.Value) + "/");
                
                bool folderExists = Directory.Exists(FuelclaimPath);
                if (!folderExists)
                {
                    Directory.CreateDirectory(FuelclaimPath);
                }


                //string[] Files = Directory.GetFiles(FuelclaimPath);
                //foreach (string file in Files)
                //{
                //    File.Delete(file);
                //}


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
                            strfileName =hdnremid.Value + "_" + txtEmpCode.Text + "_" + Convert.ToString(hdnClaimid.Value) + "_" + strfromDate + "_" + Convert.ToString(i + 1).Trim() + "_" + uploadfileName.FileName;
                        else
                            strfileName = txtEmpCode.Text + "_" + Convert.ToString(hdnClaimid.Value) + "_" + strfromDate + "_" + Convert.ToString(i + 1).Trim() + "_" + uploadfileName.FileName;

                        filename = strfileName;                        
                        uploadfileName.SaveAs(Path.Combine(FuelclaimPath, strfileName));

                        if (Convert.ToString(hdnremid.Value).Trim() != "0")
                            spm.InsertpaymentUploaded_Files(Convert.ToDecimal(hdnremid.Value), blnfile, Convert.ToString(strfileName).Trim(), "Recruitment_insert", i + 1, Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToInt32(hdnClaimid.Value));
                        else
                            spm.InsertpaymentUploaded_Files(Convert.ToDecimal(hdnremid.Value), blnfile, Convert.ToString(strfileName).Trim(), "Recruitment_insertTmp", i + 1, Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToInt32(hdnClaimid.Value));
                        blnfile = true;
                    }
                }

            }


        }
        #endregion



        if (Convert.ToString(hdnposid.Value).Trim() != "0")
            Response.Redirect("~/procs/Recruitments.aspx?clmid=" + hdnClaimid.Value + "&rem_id=" + hdnremid.Value + "&inb=" + hdnAttachPosNo.Value + "&posid=" + hdnposid.Value);
        else
            Response.Redirect("~/procs/Recruitments.aspx?clmid=" + hdnClaimid.Value + "&rem_id=" + hdnremid.Value);

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

    protected void accmo_delete_btn_Click(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";
        #region date formatting
        if (Convert.ToString(txtFromdate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
            //strfromDate = Convert.ToString(strdate[0]) + "_" + Convert.ToString(strdate[1]);
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);

        }
        #endregion
        hdnsptype.Value = "deleteTempTable";
        spm.Insert_Recruitment_InterviewDetails(Convert.ToInt32(hdnremid.Value), strfromDate, txtEmpCode.Text, hdnsptype.Value, "", Convert.ToInt32(hdnCommtypeidO.Value), Convert.ToInt32(hdnCommtypeid.Value), "", 0, 0, "", 0, "", "", "", "", "");
        //spm.InsertPaymentVoucherDetails(0, strfromDate, Convert.ToDecimal(0), txtEmpCode.Text, Convert.ToDecimal(0), hdnsptype.Value, "", "", hdnCommtypeid.Value, "", Convert.ToString(hdnCommtypeidO.Value).Trim());
        //Response.Redirect("~/procs/Recruitments.aspx?clmid=" + hdnCommtypeid.Value + "&rem_id=" + hdnremid.Value);
        if (Convert.ToString(hdnAttachPosNo.Value).Trim() != "0" && Convert.ToString(hdnAttachPosNo.Value).Trim() != "")
            Response.Redirect("~/procs/Recruitments.aspx?clmid=" + hdnCommtypeid.Value + "&rem_id=" + hdnremid.Value + "&inb=" + hdnAttachPosNo.Value);
        else
            Response.Redirect("~/procs/Recruitments.aspx?clmid=" + hdnCommtypeid.Value + "&rem_id=" + hdnremid.Value);

    }

    protected void lstComm_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtComm_Type.Text = lstComm_Type.SelectedItem.Text;
        PopupControlExtender1.Commit(lstComm_Type.SelectedItem.Text);

        SetType_toHidden();

        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetDeviation('"+ hdnCommtypeid.Value +"');", true);
    }

    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        try
        {

            //if (Convert.ToString(txtFromdate.Text).Trim() != "")
            //    checkPastMoths_AlreadySubmits();
            if (Convert.ToString(txtFromdate.Text).Trim() != "")
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
        //string strfilename = Convert.ToString(hdnCommtypeid.Value) + "/" + Convert.ToString(gvfuel_pvFiles.Rows[row.RowIndex].Cells[0].Text).Trim();
        string strfilename = Convert.ToString(gvfuel_pvFiles.Rows[row.RowIndex].Cells[0].Text).Trim();

        //String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PaymentVoucherDocumentpath"]).Trim() + "paymentVoucher_temp/"), strfilename);
        String strfilepath = "";
        if (Convert.ToString(hdnremid.Value).Trim() != "0")
          strfilepath=  Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["RecruitmentDocumentpath"]).Trim() + strfilename);
        else
            strfilepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["RecruitmentDocumentpath"]).Trim() + "Recruitment_temp/" + Convert.ToString(txtEmpCode.Text) + "/" + strfilename);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
        Response.WriteFile(strfilepath);
        Response.End();

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
            //lblmessage.Visible = true;
            claimmob_btnSubmit.Enabled = false;
            uploadfile.Enabled = false;
            uploadRcpt.Enabled = false;
            lblmessage.Text = "Sorry You are not entitled for Payment Voucher claims!";
        }
    }

    public void GetComm_Type()
    {
        DataTable dtTripMode = new DataTable();
        //hdnTryiptypeid.Value
        //dtTripMode = spm.getTravelMode();
        dtTripMode = spm.getComm_Type();
        if (dtTripMode.Rows.Count > 0)
        {
            lstComm_Type.DataSource = dtTripMode;
            lstComm_Type.DataTextField = "comm_type_name";
            lstComm_Type.DataValueField = "comm_type_id";
            lstComm_Type.DataBind();

        }
    }

    public void GetComm_Way(string ival)
    {
        DataTable dtTripMode = new DataTable();
        //hdnTryiptypeid.Value
        //dtTripMode = spm.getTravelMode();
        dtTripMode = spm.getComm_Way(ival);
        if (dtTripMode.Rows.Count > 0)
        {
            lst_Way.DataSource = null;
            lst_Way.DataSource = dtTripMode;
            lst_Way.DataTextField = "comm_way_name";
            lst_Way.DataValueField = "comm_way_id";
            lst_Way.DataBind();

        }
    }

    public void GetStatus()
    {
        DataTable dtTripMode = new DataTable();
        //hdnTryiptypeid.Value
        //dtTripMode = spm.getTravelMode();
        dtTripMode = spm.getStatus();
        if (dtTripMode.Rows.Count > 0)
        {
            lst_Status.DataSource = dtTripMode;
            lst_Status.DataTextField = "status_name";
            lst_Status.DataValueField = "status_id";
            lst_Status.DataBind();

        }
    }

    public void SetType_toHidden()
    {
        //DataSet dtTripDev = new DataSet();
        //dtTripDev = spm.getAccCode(Convert.ToInt32(lstComm_Type.SelectedValue));
        //if (dtTripDev.Tables[0].Rows.Count > 0)
        //{
        //    hdnDeviation.Value = Convert.ToString(dtTripDev.Tables[0].Rows[0]["pv_type"]).Trim();

        //}
        hdnCommtypeid.Value = Convert.ToString(Convert.ToInt32(lstComm_Type.SelectedValue));
        if (hdnCommtypeid.Value == "1")
            GetComm_Way("1,4,0");
        else if (hdnCommtypeid.Value == "2")
            GetComm_Way("2,3,4,0");
        else 
            GetComm_Way("1,2,5,0");

    }

    public void SetWay_toHidden()
    {
        //DataSet dtTripDev = new DataSet();
        //dtTripDev = spm.getAccCode(Convert.ToInt32(lstComm_Type.SelectedValue));
        //if (dtTripDev.Tables[0].Rows.Count > 0)
        //{
        //    hdnDeviation.Value = Convert.ToString(dtTripDev.Tables[0].Rows[0]["pv_type"]).Trim();

        //}
        hdnWayid.Value = Convert.ToString(Convert.ToInt32(lst_Way.SelectedValue));

    }

    public void SetStatus_toHidden()
    {
        //DataSet dtTripDev = new DataSet();
        //dtTripDev = spm.getAccCode(Convert.ToInt32(lstComm_Type.SelectedValue));
        //if (dtTripDev.Tables[0].Rows.Count > 0)
        //{
        //    hdnDeviation.Value = Convert.ToString(dtTripDev.Tables[0].Rows[0]["pv_type"]).Trim();

        //}
        hdnStatusid.Value = Convert.ToString(Convert.ToInt32(lst_Status.SelectedValue));

    }

     private void getClaimDetails()
     {

         DataSet dsTrDetails = new DataSet();
         SqlParameter[] spars = new SqlParameter[4];

         spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
         spars[0].Value = "getRecruitmentdetails_edit";

         spars[1] = new SqlParameter("@claimsid", SqlDbType.VarChar);
         spars[1].Value = Convert.ToString(hdnClaimid.Value);

         spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
         spars[2].Value = Convert.ToString(txtEmpCode.Text);

         spars[3] = new SqlParameter("@rem_id", SqlDbType.VarChar);
         spars[3].Value = Convert.ToString(hdnremid.Value);

         dsTrDetails = spm.getDatasetList(spars, "SP_Recruitment_Masters");

         if (dsTrDetails.Tables[0].Rows.Count > 0)
         {
             txtComm_Type.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["pv"]).Trim();
             lstComm_Type.SelectedValue = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["com_type"]);
             hdnCommtypeid.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["com_type"]);
             Txt_Way.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Particulars"]).Trim();
             lst_Way.SelectedValue = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["com_way"]);
             hdnWayid.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["com_way"]);
             Txt_Status.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["status_name"]).Trim();
             lst_Status.SelectedValue = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["com_status"]);
             hdnStatusid.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["com_status"]);
             Txt_Details.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["com_details"]).Trim();
             txtFromdate.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Rem_Month"]).Trim();
             Txt_Time.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["com_time"]).Trim();
             TxtPanel1.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["com_panel_mem1"]).Trim();
             TxtPanel2.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["com_panel_mem2"]).Trim();
             TxtPanel3.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["com_panel_mem3"]).Trim();
             TxtPanel4.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["com_panel_mem4"]).Trim();
             TxtPanel5.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["com_panel_mem5"]).Trim();
             TxtPanel6.Text = "";

             //lnkuplodedfile.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["UploadFile"]).Trim();
             //txtReason.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Particulars"]).Trim();
         }


     }
     public void checkPastMoths_AlreadySubmits()
     {
         try
         {
             lblmessage.Text = "";
             #region date formatting

             string[] strdate;
             string strfromDate = "";
             string strToDate = "";


             if (Convert.ToString(Session["Fromdate"]).Trim() != "")
             {
                 strdate = Convert.ToString(Session["Fromdate"]).Trim().Split('/');
                 strToDate = Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]) + "/" + Convert.ToString(strdate[2]);
             }

             #endregion

             DataSet dsTrDetails = new DataSet();
             SqlParameter[] spars = new SqlParameter[3];

             spars[0] = new SqlParameter("@sMonth", SqlDbType.VarChar);
             spars[0].Value = strToDate;

             spars[1] = new SqlParameter("@sclaimdate", SqlDbType.VarChar);
             spars[1].Value = txtFromdate.Text;

             spars[2] = new SqlParameter("@Empcode", SqlDbType.VarChar);
             spars[2].Value = txtEmpCode.Text;

             dsTrDetails = spm.getDatasetList(spars, "sp_check_mobile_rem_validation");

             //txtAmount.Enabled = true;
             claimmob_btnSubmit.Visible = true;
             if (Convert.ToString(hdnCommtypeid.Value).Trim() != "0")
             {
                 accmo_delete_btn.Visible = true;
             }
             if (dsTrDetails.Tables[0].Rows.Count > 0)
             {
                 if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["msg"]) != "")
                 {
                     //txtAmount.Enabled = false;
                     //txtAmount.Text = "";
                     lblmessage.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["msg"]);
                     claimmob_btnSubmit.Visible = false;
                     if (Convert.ToString(hdnCommtypeid.Value).Trim() != "0")
                     {
                         accmo_delete_btn.Visible = false;
                     }

                 }
             }
         }
         catch (Exception ex)
         {
             Response.Write(ex.Message.ToString());

         }

     }

     public void checkFutureDates_ForSubmits()
     {
         
         try
         {
             claimmob_btnSubmit.Visible = true;
             lblmessage.Text = "";
             #region date formatting

             string[] strdate;
             string strfromDate = "";
             string strToDate = "";


             if (Convert.ToString(Session["Fromdate"]).Trim() != "")
             {
                 strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
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
                     lblmessage.Text = "Future date are not allowed. ";
                     claimmob_btnSubmit.Visible = false;
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
             spars[0].Value = "get_RecruitmentFiles";

             spars[1] = new SqlParameter("@filetype", SqlDbType.VarChar);
             spars[1].Value = "Recruitment_insertTmp";

             spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
             spars[2].Value = hdnremid.Value;
             
             spars[3] = new SqlParameter("@empcode", SqlDbType.VarChar);
             spars[3].Value = Convert.ToString(txtEmpCode.Text).Trim();

             spars[4] = new SqlParameter("@claimsid", SqlDbType.Int);
             spars[4].Value = hdnClaimid.Value;


             dsfuelFiles = spm.getDatasetList(spars, "SP_Recruitment_Masters");

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

     #endregion

     protected void lst_Way_SelectedIndexChanged(object sender, EventArgs e)
     {
         Txt_Way.Text = lst_Way.SelectedItem.Text;
         PopupControlExtender2.Commit(lst_Way.SelectedItem.Text);
         SetWay_toHidden();

         ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetDeviationW('" + hdnWayid.Value + "');", true);
     }
     protected void lst_Status_SelectedIndexChanged(object sender, EventArgs e)
     {
         Txt_Status.Text = lst_Status.SelectedItem.Text;
         PopupControlExtender3.Commit(lst_Status.SelectedItem.Text);
         SetStatus_toHidden();

         ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetDeviationS('" + hdnStatusid.Value + "');", true);
     }
     protected void Link_Cand_Click(object sender, EventArgs e)
     {
         if (hdnCommtypeid.Value == "2")
         {
             if (TxtPanel1.Visible)
             {
                 TxtPanel1.Visible = false;
                 iSpan1.Visible = false;
                 TxtPanel2.Visible = false;
                 iSpan2.Visible = false;
                 TxtPanel3.Visible = false;
                 iSpan3.Visible = false;
                 TxtPanel4.Visible = false;
                 iSpan4.Visible = false;
                 TxtPanel5.Visible = false;
                 iSpan5.Visible = false;
                 TxtPanel6.Visible = false;
                 iSpan6.Visible = false;
             }
             else
             {
                 TxtPanel1.Visible = true;
                 iSpan1.Visible = true;
                 TxtPanel2.Visible = true;
                 iSpan2.Visible = true;
                 TxtPanel3.Visible = true;
                 iSpan3.Visible = true;
                 TxtPanel4.Visible = true;
                 iSpan4.Visible = true;
                 TxtPanel5.Visible = true;
                 iSpan5.Visible = true;
                 TxtPanel6.Visible = false;
                 iSpan6.Visible = false;
             }
         }
         else
         {
             TxtPanel1.Visible = false;
             iSpan1.Visible = false;
             TxtPanel2.Visible = false;
             iSpan2.Visible = false;
             TxtPanel3.Visible = false;
             iSpan3.Visible = false;
             TxtPanel4.Visible = false;
             iSpan4.Visible = false;
             TxtPanel5.Visible = false;
             iSpan5.Visible = false;
             TxtPanel6.Visible = false;
             iSpan6.Visible = false;
             TxtPanel1.Text = "";
             TxtPanel2.Text = "";
             TxtPanel3.Text = "";
             TxtPanel4.Text = "";
             TxtPanel5.Text = "";
             TxtPanel6.Text = "";
         }

     }


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
