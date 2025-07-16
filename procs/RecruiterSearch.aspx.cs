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

public partial class RecruiterSearch : System.Web.UI.Page
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



                    spn_release.Visible = false;
                    chkRealse.Visible = false;
                    spn_position_recuriter.Visible = false;
                    txt_position_by_recuriter.Visible = false;

                    hdnTravelConditionid.Value = "1";
                    hdnremid.Value = "0";

                    GetEmployeeDetails();
                    txtEmpCode.Text = Convert.ToString(Session["Empcode"]);
                    GetStatus();
                    if (Request.QueryString.Count > 0)
                    {
                        hdnclaimid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdnremid.Value = Convert.ToString(Request.QueryString[1]).Trim();
                    }
                    if (Convert.ToString(hdnremid.Value).Trim() != "0" && Convert.ToString(hdnremid.Value).Trim() != "")
                    {
                        get_Recuriter_PositionDtls();

                        btnTra_Details.Visible = true;
                        spmdet.Visible = true;
                        dgMobileClaim.Visible = true;

                        getMobileClaimDetails();
                        get_position_dept_location();
                    }

                    //get_candidates_function_frmtmp();


                    if (Convert.ToString(hdnMobRemStatusM.Value).Trim() == "2" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "3" || Convert.ToString(hdnMobRemStatusM.Value).Trim() == "4")
                    {

                    }
                    if (Convert.ToString(hdnMobRemStatus_dtls.Value).Trim() == "Approved")
                    {
                        mobile_btnSave.Visible = false;
                        mobile_cancel.Visible = false;
                        btnTra_Details.Visible = false;
                    }
                    txt_recruiter_code.Enabled = false;
                    txt_recruiter_name.Enabled = false;
                    txt_position_title.Enabled = false;
                    txt_fromdate.Enabled = false;
                    txt_todate.Enabled = false;


                    DisplayProfileProperties();
                    loadorder();
                    if (Convert.ToString(hdnremid.Value).Trim() != "0" && Convert.ToString(hdnremid.Value).Trim() != "")
                    {
                        get_attachCandidate_dept_location();
                    }
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
        Response.Redirect("~/procs/RecruitmentsPositions.aspx");
    }

    protected void mobile_btnSave_Click1(object sender, EventArgs e)
    {
        try
        {
            Boolean blnchhk = false;

            for (Int32 irow = 0; irow < gv_search_candidates.Rows.Count; irow++)
            {
                CheckBox Chkattach = ((CheckBox)gv_search_candidates.Rows[irow].FindControl("chkAttach"));
                if (Chkattach.Checked)
                {
                    blnchhk = true;
                    break;
                }
            }

            if (blnchhk == false)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Please select Candidate";
                return;
            }
            DataSet tmpds_pv = new DataSet();

            for (Int32 irow = 0; irow < gv_search_candidates.Rows.Count; irow++)
            {

                CheckBox Chkattach = ((CheckBox)gv_search_candidates.Rows[irow].FindControl("chkAttach"));
                Decimal interview_id = 0;
                interview_id = Convert.ToDecimal(gv_search_candidates.DataKeys[irow].Values[0]);

                if (Chkattach.Checked)
                {
                    Label lbl_interviewid = ((Label)gv_search_candidates.Rows[irow].FindControl("lbl_interviewid"));

                    SqlParameter[] spars = new SqlParameter[4];

                    if (Convert.ToString(lbl_interviewid.Text).Trim() != "")
                        interview_id = Convert.ToDecimal(lbl_interviewid.Text);


                    spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                    spars[0].Value = "attach_candidate_to_position";

                    spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
                    if (Convert.ToString(hdnremid.Value).Trim() != "")
                        spars[1].Value = Convert.ToDecimal(hdnremid.Value);
                    else
                        spars[1].Value = 0;

                    spars[2] = new SqlParameter("@interview_id", SqlDbType.Decimal);
                    if (Convert.ToString(interview_id).Trim() != "")
                        spars[2].Value = Convert.ToDecimal(interview_id);
                    else
                        spars[2].Value = 0;

                    spars[3] = new SqlParameter("@recruiter", SqlDbType.VarChar);
                    spars[3].Value = Convert.ToString(txt_recruiter_code.Text).Trim();

                    tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Candidate_Insert");
                }

            }

            // Response.Redirect("~/procs/positionslist.aspx"); 
            // Response.Redirect("~/procs/RecruitmentsPositions.aspx");
            getMobileClaimDetails();
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnkSearch_candidates_Click(object sender, EventArgs e)
    {
        Search_Candidate_Resume();
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        delete_tmp_Functions_positions();

        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        //int ipositinoid = Convert.ToInt32(gv_candidates_positions.DataKeys[row.RowIndex].Values[0]);


        Decimal interview_id = Convert.ToDecimal(dgMobileClaim.DataKeys[row.RowIndex].Values[0]);
        Decimal attach_pos_no = Convert.ToDecimal(dgMobileClaim.DataKeys[row.RowIndex].Values[1]);
        Decimal ipositinoid = Convert.ToInt32(dgMobileClaim.DataKeys[row.RowIndex].Values[2]);

        //Response.Redirect(ReturnUrl("hccurlmain") + "/procs/Recruitments.aspx?clmid=" + ipositinoid + "&rem_id=" + interview_id + "&inb=" + attach_pos_no);
        Response.Redirect(ReturnUrl("hccurlmain") + "/procs/Recruitments.aspx?clmid=0&rem_id=" + interview_id + "&inb=" + attach_pos_no + "&posid=" + ipositinoid + "&tid=0");

    }

    protected void lnkLeaveDetails_Click(object sender, EventArgs e)
    {

        try
        {

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            Label lblFilename = ((Label)row.FindControl("lblFilename"));

            if (Convert.ToString(lblFilename.Text).Trim() != "")
            {
                String strfilepath = "";
                if (Convert.ToString(hdnremid.Value).Trim() != "0")
                    strfilepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["RecruitmentDocumentpath"]).Trim() + lblFilename.Text);
                else
                    strfilepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["RecruitmentDocumentpath"]).Trim() + "Recruitment_temp/" + Convert.ToString(txtEmpCode.Text) + "/" + lblFilename.Text);
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
                Response.WriteFile(strfilepath);
                Response.End();
            }

        }
        catch (Exception ex)
        { }
    }

    protected void gv_search_candidates_RowCreated(object sender, GridViewRowEventArgs e)
    {


    }

    protected void gv_search_candidates_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkAttach = (CheckBox)e.Row.FindControl("chkAttach");

            if (Convert.ToString(e.Row.Cells[5].Text).Trim().ToLower() == "open")
            {
                chkAttach.Enabled = true;
            }
            else
            {
                //chkAttach.Enabled = false;
                chkAttach.Visible = false;
            }
        }
    }

    protected void lnkResume_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            Label lblFilename = ((Label)row.FindControl("lblFilename"));

            if (Convert.ToString(lblFilename.Text).Trim() != "")
            {
                String strfilepath = "";
                if (Convert.ToString(hdnremid.Value).Trim() != "0")
                    strfilepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["RecruitmentDocumentpath"]).Trim() + lblFilename.Text);
                else
                    strfilepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["RecruitmentDocumentpath"]).Trim() + "Recruitment_temp/" + Convert.ToString(txtEmpCode.Text) + "/" + lblFilename.Text);
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
                Response.WriteFile(strfilepath);
                Response.End();
            }

        }
        catch (Exception ex)
        { }
    }

    protected void lstGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtsrchBy_Gender.Text = lstGender.SelectedItem.Text;
        PopupControlExtender4.Commit(lstGender.SelectedItem.Text);
    }

    protected void lst_Status_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtsrchBy_Status.Text = lst_Status.SelectedItem.Text;
        PopupControlExtender3.Commit(lst_Status.SelectedItem.Text);
    }

    #endregion


    #region PageMethods

    private void get_Recuriter_PositionDtls()
    {
        try
        {

            DataSet tmpds_pv = new DataSet();

            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_get_positiondtls_recuriter_Srch";

            spars[1] = new SqlParameter("@rem_id", SqlDbType.Decimal);
            if (Convert.ToString(hdnremid.Value).Trim() != "")
                spars[1].Value = Convert.ToDecimal(hdnremid.Value);
            else
                spars[1].Value = 0;


            tmpds_pv = spm.getDatasetList(spars, "SP_Recruitment_Masters");

            #region reset Controls
            txt_recruiter_name.Text = "";
            txt_recruiter_code.Text = "";
            txt_position_title.Text = "";
            txt_fromdate.Text = "";
            txt_todate.Text = "";
            #endregion

            if (tmpds_pv.Tables[0].Rows.Count > 0)
            {
                txt_recruiter_name.Text = Convert.ToString(tmpds_pv.Tables[0].Rows[0]["Emp_Name"]).Trim();
                txt_recruiter_code.Text = Convert.ToString(tmpds_pv.Tables[0].Rows[0]["Emp_Code"]).Trim();
                txt_position_title.Text = Convert.ToString(tmpds_pv.Tables[0].Rows[0]["position_tile"]).Trim();
                txt_fromdate.Text = Convert.ToString(tmpds_pv.Tables[0].Rows[0]["fromdate"]).Trim();
                txt_todate.Text = Convert.ToString(tmpds_pv.Tables[0].Rows[0]["todate"]).Trim();
            }




        }
        catch (Exception ex)
        {

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

    private void Search_Candidate_Resume()
    {
        try
        {
            String strSelect = "";
            String strWhere = "";
            String strSQL = "";
            String strOrderby = " order by tr.cand_name  ";
            string strisconfidential = "'N'";

            if (Convert.ToString(get_Recuiter_isConfidential_view()).Trim() == "Y")
            {
                strisconfidential = "'N','Y'";
            }

            #region Create Search Query
            strSelect =
                               " select  0 as position_id, ROW_NUMBER() OVER(ORDER BY cand_name ASC)  as srno, tr.interview_id ,tr.cand_name,   " +
                               " case when (Select q.qualification_name from Req_Qualification_dtls q where q.pg_grdute_others_type in ('Post Graduate','Master','Doctrate') and q.interview_id=tr.interview_id) !=''  " +
                               " then " +
                               "     (Select top (1) q.qualification_name from Req_Qualification_dtls q where q.pg_grdute_others_type in ('Post Graduate','Master','Doctrate') and q.interview_id=tr.interview_id)  " +

                               "  when (Select top (1) q.qualification_name from Req_Qualification_dtls q where q.pg_grdute_others_type='Graduate' and q.interview_id=tr.interview_id) !='' " +
                               "  then " +
                               "  (Select top (1) q.qualification_name from Req_Qualification_dtls q where q.pg_grdute_others_type='Graduate' and q.interview_id=tr.interview_id)  " +

                               "  when (Select top (1) q.qualification_name from Req_Qualification_dtls q where q.pg_grdute_others_type='Diploma' and q.interview_id=tr.interview_id) !='' " +
                               "  then " +
                               "   (Select top (1) q.qualification_name from Req_Qualification_dtls q where q.pg_grdute_others_type='Diploma' and q.interview_id=tr.interview_id)  " +

                               "  when (Select top (1) q.qualification_name from Req_Qualification_dtls q where q.pg_grdute_others_type='Others' and q.interview_id=tr.interview_id) !='' " +
                               "  then " +
                               "  (Select top (1) q.qualification_name from Req_Qualification_dtls q where q.pg_grdute_others_type='Others' and q.interview_id=tr.interview_id)  " +

                              "   end Qualification,tr.cand_exp, " +

                              "  case when (Select top (1) q.qualification_name from Req_Qualification_dtls q where q.pg_grdute_others_type in ('Post Graduate','Master','Doctrate') and q.interview_id=tr.interview_id) !=''  " +
                              "  then " +
                              "      (Select top (1) q.qualification_institute from Req_Qualification_dtls q where q.pg_grdute_others_typein ('Post Graduate','Master','Doctrate') and q.interview_id=tr.interview_id)  " +

                              "   when (Select top (1) q.qualification_institute from Req_Qualification_dtls q where q.pg_grdute_others_type='Graduate' and q.interview_id=tr.interview_id) !='' " +
                              "   then " +
                              "   (Select top (1) q.qualification_institute from Req_Qualification_dtls q where q.pg_grdute_others_type='Graduate' and q.interview_id=tr.interview_id)  " +

                              "   when (Select top (1) q.qualification_institute from Req_Qualification_dtls q where q.pg_grdute_others_type='Diploma' and q.interview_id=tr.interview_id) !='' " +
                              "   then " +
                              "   (Select top (1) q.qualification_institute from Req_Qualification_dtls q where q.pg_grdute_others_type='Diploma' and q.interview_id=tr.interview_id)  " +

                              "   when (Select top (1) q.qualification_institute from Req_Qualification_dtls q where q.pg_grdute_others_type='Others' and q.interview_id=tr.interview_id) !='' " +
                              "   then " +
                              "   (Select top (1) q.qualification_institute from Req_Qualification_dtls q where q.pg_grdute_others_type='Others' and q.interview_id=tr.interview_id)  " +

                              "  end qualification_institute, " +
                              "  case when (Select Distinct ISNULL(e.Emp_Name,'') as Emp_Name " +
                              "         from tbl_Employee_Mst e inner join Req_Candiate_Position_attach_dtls c on e.Emp_Code=c.cand_attach_recruiter  " +
                              "          and tr.interview_id=c.interview_id and c.cand_realse='N') is null then 'open'  else " +
                              "       (Select Distinct e.Emp_Name  from tbl_Employee_Mst e inner join Req_Candiate_Position_attach_dtls c on e.Emp_Code=c.cand_attach_recruiter and c.interview_id=tr.interview_id  and c.cand_realse='N')  end cand_recruiter, " +

                              "  (Select e.Emp_Name  from tbl_Employee_Mst e inner join Req_Candidate_Details c on e.Emp_Code=c.cand_attach_recruiter and c.interview_id=tr.interview_id) as cand_recruiter, " +
                              "   (Select file_name from tbl_uploaded_files t where t.file_type ='Recruitment' and t.t_id=tr.interview_id " +
                              "      and t.file_sr_no in (Select max(f.file_sr_no) from tbl_uploaded_files f where f.file_type ='Recruitment' and f.t_id=tr.interview_id)) as ResumeFile " +
                             "   from Req_Candidate_Details tr ";



            #endregion





            strWhere = "where tr.is_confidential in (" + strisconfidential + ") and tr.interview_id not in (Select d.interview_id from Req_Candiate_Position_attach_dtls d where d.cand_for_pos_dpt is not null and cand_for_pos_loc_code is not null and cand_realse='Y')";//  and tr.interview_id not in (Select interview_id from Req_Candiate_Position_attach_dtls where cand_attach_recruiter='" + Convert.ToString(txtEmpCode.Text).Trim() + "' and cand_realse='N') ";



            #region Attach where Parameter

            if (Convert.ToString(hdnremid.Value).Trim() != "")
            {
                if (Convert.ToString(strWhere).Trim() == "")
                    strWhere = " tr.interview_id not in (Select d.interview_id from Req_Candiate_Position_attach_dtls d where d.cand_realse='N' and d.cand_attach_position=" + Convert.ToString(hdnremid.Value).Trim() + ") ";
                else
                    strWhere = strWhere + " and tr.interview_id not in (Select d.interview_id from Req_Candiate_Position_attach_dtls d where d.cand_realse='N' and d.cand_attach_position=" + Convert.ToString(hdnremid.Value).Trim() + ") ";


            }


            if (Convert.ToString(txt_srchQualfication.Text).Trim() != "")
            {
                if (Convert.ToString(strWhere).Trim() == "")
                    strWhere = " tr.interview_id in (select interview_id from Req_Qualification_dtls where qualification_name like '%" + Convert.ToString(txt_srchQualfication.Text).Trim() + "%')";
                else
                    strWhere = strWhere + " and   tr.interview_id in (select interview_id from Req_Qualification_dtls where qualification_name like '%" + Convert.ToString(txt_srchQualfication.Text).Trim() + "%')";
            }

            if (Convert.ToString(txt_srchexprience.Text).Trim() != "")
            {
                if (Convert.ToString(strWhere).Trim() == "")
                    strWhere = "  tr.cand_exp like '%" + Convert.ToString(txt_srchexprience.Text).Trim() + "%'";
                else
                    strWhere = strWhere + " and  tr.cand_exp like '%" + Convert.ToString(txt_srchexprience.Text).Trim() + "%'";
            }

            if (Convert.ToString(txt_srchkeywords.Text).Trim() != "")
            {
                if (Convert.ToString(strWhere).Trim() == "")
                    strWhere = " tr.srch_keywords like '%" + Convert.ToString(txt_srchkeywords.Text).Trim() + "%'";
                else
                    strWhere = strWhere + " and   tr.srch_keywords like '%" + Convert.ToString(txt_srchkeywords.Text).Trim() + "%'";
            }


            if (Convert.ToString(txt_srchFunction.Text).Trim() != "")
            {
                if (Convert.ToString(strWhere).Trim() == "")
                    strWhere = " tr.interview_id in (Select f.interview_id from Req_Functions f inner join Req_functions_mst m on f.function_id=m.function_id where m.function_name like '%" + Convert.ToString(txt_srchFunction.Text).Trim() + "%' ) ";
                else
                    strWhere = strWhere + " and tr.interview_id in (Select f.interview_id from Req_Functions f inner join Req_functions_mst m on f.function_id=m.function_id where m.function_name like '%" + Convert.ToString(txt_srchFunction.Text).Trim() + "%' ) ";
            }



            if (Convert.ToString(txtsrchBy_Gender.Text).Trim() != "")
            {
                if (Convert.ToString(strWhere).Trim() == "")
                    strWhere = "  tr.cand_gender='" + Convert.ToString(txtsrchBy_Gender.Text).Trim()  + "'";
                else
                    strWhere = strWhere + "  and  tr.cand_gender='" + Convert.ToString(txtsrchBy_Gender.Text).Trim() + "'";
            }


            if (Convert.ToString(txtsrchBy_Status.Text).Trim() != "")
            {
                if (Convert.ToString(strWhere).Trim() == "")
                    strWhere = " tr.status  ='" + Convert.ToString(lst_Status.SelectedValue).Trim() + "' ";
                else
                    strWhere = strWhere + " and  tr.status ='" + Convert.ToString(lst_Status.SelectedValue).Trim() + "' ";

            }
            

            #endregion






            if (Convert.ToString(strWhere).Trim() != "")
            {
                strSQL = strSelect + strWhere + strOrderby;
            }
            else
            {
                strSQL = strSelect + strOrderby;
            }


           // Response.Write(strSQL);
          //  Response.End();

            DataTable dtTravelRequest = new DataTable();
            dtTravelRequest = spm.getDataList_SQL(strSQL);

            gv_search_candidates.DataSource = null;
            gv_search_candidates.DataBind();

            if (dtTravelRequest.Rows.Count > 0)
            {


                gv_search_candidates.DataSource = dtTravelRequest;
                gv_search_candidates.DataBind();
            }


        }
        catch (Exception ex)
        {

        }
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
        DataSet dtMobileDetails = new DataSet();

        SqlParameter[] spars = new SqlParameter[3];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_get_attached_candidates_recuriter_Srch";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

        spars[2] = new SqlParameter("@rem_id", SqlDbType.Decimal);
        spars[2].Value = Convert.ToDecimal(hdnremid.Value);


        dtMobileDetails = spm.getDatasetList(spars, "SP_Recruitment_Masters");

        dgMobileClaim.DataSource = null;
        dgMobileClaim.DataBind();

        if (dtMobileDetails.Tables[0].Rows.Count > 0)
        {
            dgMobileClaim.DataSource = dtMobileDetails.Tables[0];
            dgMobileClaim.DataBind();
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
                    LinkButton1.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {

        }

    }


    protected void get_position_dept_location()
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_get_dept_location_byposition";

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


    protected void delete_tmp_Functions_positions()
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "sp_delete_functins_Positions_tmp";

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;

            dsTrDetails = spm.getDatasetList(spars, "SP_Recruitment_Masters");

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
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

    #endregion


    #region Search web method

    [System.Web.Services.WebMethod]
    public static List<string> SearchLocations(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";

                strsql = "  Select Distinct p.loc_name " +
                    //"  Select CONCAT(loc_name,' - ',ltrim(RTRIM(str(loc_code)))) as locname " +                       
                         "  from Req_job_position_dtls jp inner join Req_Location p on jp.loc_code=p.loc_code  " +
                         "   where p.is_active='A' and p.loc_name like '%' + @SearchText + '%'   Order by p.loc_name ";



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
                        employees.Add(sdr["loc_name"].ToString());
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

                strsql = "  Select Distinct d.dept_name " +
                         "  from Req_job_position_dtls jp inner join Req_department d on jp.dept_id=d.dept_id  " +
                         "  where d.is_active='A' and d.dept_name like '%' + @SearchText + '%'   Order by d.dept_name ";

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


                strsql = "  Select Distinct position_tile " +
                         "  from Req_Positions p inner join  Req_job_position jp on p.position_id=jp.pos_id  " +
                         "   where jp.isactive='A' and jp.position_tile like '%' + @SearchText + '%'   order by jp.position_tile ";

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
                        employees.Add(sdr["position_tile"].ToString());
                    }
                }
                conn.Close();
                return employees;
            }
        }
    }

    [System.Web.Services.WebMethod]
    public static List<string> SearchFunction(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";

                strsql = "  Select Distinct fm.function_name " +
                         "  from Req_Functions f inner join  Req_functions_mst fm on f.function_id=fm.function_id  " +
                         "   where fm.isactive='A' and fm.function_name like '%' + @SearchText + '%'   Order by fm.function_name ";

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
                        employees.Add(sdr["function_name"].ToString());
                    }
                }
                conn.Close();
                return employees;
            }
        }
    }

    [System.Web.Services.WebMethod]
    public static List<string> SearchQualification(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";

                strsql = "  Select Distinct qualification_name " +
                         "  from Req_Qualification_dtls" +
                         "   where qualification_name like '%' + @SearchText + '%'   Order by  qualification_name ";

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
                        employees.Add(sdr["qualification_name"].ToString());
                    }
                }
                conn.Close();
                return employees;
            }
        }
    }

    [System.Web.Services.WebMethod]
    public static List<string> Searchkeywaords(string prefixText, int count)
    {

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlCommand cmd = new SqlCommand())
            {
                string strsql = "";

                strsql = "  Select Distinct srch_keywords " +
                         "  from Req_Candidate_Details" +
                         "   where srch_keywords like '%' + @SearchText + '%'   Order by  srch_keywords ";

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
                        employees.Add(sdr["srch_keywords"].ToString());
                    }
                }
                conn.Close();
                return employees;
            }
        }
    }
    #endregion

}
