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


public partial class addposition : System.Web.UI.Page
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

                    txtEmpCode.Text = Convert.ToString(Session["Empcode"]);
                    hdnGrade.Value = Convert.ToString(Session["Grade"]);          
          
                    txt_Title.Text = Convert.ToString(Session["postitle"]).Trim();                   
                    txt_Title.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txt_fromdate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txt_todate.Attributes.Add("onkeypress", "return noanyCharecters(event);");

                    if (Request.QueryString.Count > 0)
                    {
                        hdnclaimqry.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdnClaimid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdnremid.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        hdnCommtypeidO.Value = Convert.ToString(Request.QueryString[0]).Trim();
                    }
                    if (Convert.ToString(hdnclaimqry.Value).Trim() != "0")
                    {
                        accmo_delete_btn.Visible = true;
                        getClaimDetails();
                      //  get_employee_FuelUploaded_Files();
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
        Response.Redirect("~/procs/positioncreation.aspx?clmid=" + hdnCommtypeid.Value + "&rem_id=" + hdnremid.Value);
        
    }

    protected void claimmob_btnSubmit_Click1(object sender, EventArgs e)
    {

        string[] strdate;
        string strfromDate="";
        string filename = "";
        string rfilename = "";
        String strfileName = "";
        String strRfileName = "";
        String strToDate = "";
        lblmessage.Text ="";
        #region  validation

        if (Convert.ToString(txt_Title.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter title";
            return;
        }

        if (Convert.ToString(txt_department.Text).Trim() == "")
        {
            lblmessage.Text = "Please select Department";
            return;
        }

        if (Convert.ToString(txt_location.Text).Trim() == "")
        {
            lblmessage.Text = "Please select Location";
            return;
        }

        if (Convert.ToString(txt_fromdate.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter From Date";
            return;
        }

        if (Convert.ToString(txt_todate.Text).Trim() == "")
        {
            lblmessage.Text = "Please enter From Date";
            return;
        }

        if (Convert.ToString(check_dept_Location_code_already_add()).Trim()!="")
        {
            lblmessage.Text = "Department or Location already Exists";
            return;
        }
        get_department_code();
        get_Location_code();
        get_From_toDate_Validation();

        if(Convert.ToString(hdndeptcode.Value).Trim()=="")
        {
            lblmessage.Text = "Please select Department Name";
            return;
        }

        if (Convert.ToString(hdnlocationcode.Value).Trim() == "")
        {
            lblmessage.Text = "Please select Location Name";
            return;
        }
        if (Convert.ToString(hdndatevalidation.Value).Trim() != "")
        {
            lblmessage.Text = Convert.ToString(hdndatevalidation.Value).Trim();
            return;
        }

        #endregion


        if (Convert.ToString(txt_fromdate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txt_fromdate.Text).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(txt_todate.Text).Trim() != "")
        {
            strdate = Convert.ToString(txt_todate.Text).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        hdnsptype.Value = "InsertTempTable";

        if (Convert.ToString(hdnClaimid.Value).Trim() != "" && Convert.ToString(hdnClaimid.Value).Trim() != "0")
            hdnsptype.Value = "updateTempTable";


        hdnCommtypeid.Value = lstComm_Type.SelectedValue;

        spm.Insert_Recruitment_JobPosition_dtls(Convert.ToDecimal(hdnremid.Value), Convert.ToDecimal(hdndeptcode.Value), Convert.ToDecimal(hdnlocationcode.Value), Convert.ToString(txtEmpCode.Text), hdnsptype.Value, Convert.ToString(strfromDate).Trim(), Convert.ToString(strToDate).Trim(), Convert.ToInt32(hdnClaimid.Value),Convert.ToString(txtRemarks.Text).Trim());
        

        Response.Redirect("~/procs/positioncreation.aspx?clmid=" + hdnClaimid.Value + "&rem_id=" + hdnremid.Value);

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

    public void get_department_code()
    {
        DataSet tmpds_pv = new DataSet();
        
        try
        {
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_get_Department_cdoe";

            spars[1] = new SqlParameter("@dept_location_name", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(txt_department.Text).Trim();

            tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Masters");
            hdndeptcode.Value = "";
            if (tmpds_pv.Tables[0].Rows.Count > 0)
            {
                hdndeptcode.Value = Convert.ToString(tmpds_pv.Tables[0].Rows[0]["dept_id"]);
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
        

    }

    public void get_Location_code()
    {
        DataSet tmpds_pv = new DataSet();

        try
        {
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_get_location_cdoe";

            spars[1] = new SqlParameter("@dept_location_name", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(txt_location.Text).Trim();

            tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Masters");
            hdnlocationcode.Value = "";
            if (tmpds_pv.Tables[0].Rows.Count > 0)
            {
                hdnlocationcode.Value = Convert.ToString(tmpds_pv.Tables[0].Rows[0]["loc_code"]);
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }


    }

    public void get_From_toDate_Validation()
    {
        DataSet tmpds_pv = new DataSet();
        
        try
        {
            string[] strdate;
            string strfromDate = "";
            string strToDate = "";
            if (Convert.ToString(txt_fromdate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txt_fromdate.Text).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            if (Convert.ToString(txt_todate.Text).Trim() != "")
            {
                strdate = Convert.ToString(txt_todate.Text).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_frmdate_notgrater_thantodate";

            spars[1] = new SqlParameter("@FromDate", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(strfromDate).Trim();

            spars[2] = new SqlParameter("@ToDate", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(strToDate).Trim();

            tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Masters");
            hdndatevalidation.Value = "";
            if (tmpds_pv.Tables[0].Rows.Count > 0)
            {
                hdndatevalidation.Value = Convert.ToString(tmpds_pv.Tables[0].Rows[0]["Message"]);
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    

    }


    public String check_dept_Location_code_already_add()
    {
        DataSet tmpds_pv = new DataSet();
        String str = "";
        try
        {
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_get_dept_location_isAlready_add";

            spars[1] = new SqlParameter("@location_name", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(txt_location.Text).Trim();

            spars[2] = new SqlParameter("@dept_location_name", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(txt_department.Text).Trim();

            tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Masters");
            
            if (tmpds_pv.Tables[0].Rows.Count > 0)
            {
                str = Convert.ToString(tmpds_pv.Tables[0].Rows[0]["dept_name"]);
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
        return str;


    }

    protected void accmo_delete_btn_Click(object sender, EventArgs e)
    {
        string[] strdate;
        string strfromDate = "";
        #region date formatting
         
        #endregion
        hdnsptype.Value = "deleteTempTable";
        spm.Insert_Recruitment_JobPosition_dtls(Convert.ToDecimal(hdnremid.Value), Convert.ToDecimal(0), Convert.ToDecimal(0), Convert.ToString(txtEmpCode.Text), hdnsptype.Value, Convert.ToString("").Trim(), Convert.ToString("").Trim(), Convert.ToInt32(hdnClaimid.Value),Convert.ToString(txtRemarks.Text).Trim());
        Response.Redirect("~/procs/positioncreation.aspx?clmid=" + hdnClaimid.Value + "&rem_id=" + hdnremid.Value);
    }

    protected void lstComm_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtComm_Type.Text = lstComm_Type.SelectedItem.Text;
        PopupControlExtender1.Commit(lstComm_Type.SelectedItem.Text);

        SetType_toHidden();

        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetDeviation('"+ hdnCommtypeid.Value +"');", true);
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

    protected void licnkViewFiles_Clk(object sender, EventArgs e)
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
         SqlParameter[] spars = new SqlParameter[3];

         spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
         spars[0].Value = "sp_get_dpt_loc_for_edit_tmp";

         spars[1] = new SqlParameter("@trip_dtls_id", SqlDbType.Int);
         spars[1].Value = Convert.ToInt32(hdnClaimid.Value);

         spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
         spars[2].Value = Convert.ToString(txtEmpCode.Text);

         
         dsTrDetails = spm.getDatasetList(spars, "SP_Recruitment_Masters");

         if (dsTrDetails.Tables[0].Rows.Count > 0)
         {
             txt_department.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["dept_name"]).Trim();
             txt_location.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["loc_name"]);
             txt_fromdate.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["fromdate"]);
             txt_todate.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["todate"]);
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
             spars[1].Value = DBNull.Value;

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
                     lblmessage.Text = "Future date claims are not allowed. ";
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

    


     [System.Web.Services.WebMethod]
     public static List<string> SearchLocation(string prefixText, int count)
     {

         using (SqlConnection conn = new SqlConnection())
         {
             conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

             using (SqlCommand cmd = new SqlCommand())
             {
                 string strsql = "";

                 /*strsql = "  Select t.empname from  ( " +
                          "  Select Emp_Name + ' - '  +Emp_Code as empname " +
                          "  from tbl_Employee_Mst  " +
                          "   ) t " +
                          "   where t.empname like '%' + @SearchText + '%' Order by t.empname ";
                    */
                 strsql = "   select loc_name  from Req_Location " +
                           " where is_active='A' and loc_name like '%' + @SearchText + '%' order by loc_name ";




                 cmd.CommandText = strsql;
                 cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
                 cmd.Connection = conn;
                 conn.Open();
                 List<string> employees = new List<string>();
                 using (SqlDataReader sdr = cmd.ExecuteReader())
                 {
                     while (sdr.Read())
                     {
                         employees.Add(sdr["loc_name"].ToString());
                     }
                 }
                 conn.Close();
                 return employees;
             }
         }
     }

    [System.Web.Services.WebMethod]
     public static List<string> SearchDeprtment(string prefixText, int count)
     {

         using (SqlConnection conn = new SqlConnection())
         {
             conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

             using (SqlCommand cmd = new SqlCommand())
             {
                 string strsql = "";

                 strsql = " select dept_name  from Req_department " +
                         "  where is_active='A' and dept_name like '%' + @SearchText + '%' order by dept_name ";



                 cmd.CommandText = strsql;
                 cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
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
}
