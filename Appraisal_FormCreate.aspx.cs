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
using System.Security.Cryptography;
using Microsoft.Reporting.WebForms;

public partial class Appraisal_FormCreate : System.Web.UI.Page
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
                    //   PopulateEmployeeLeaveData();
                    getAssessmentList();
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
        hdnAppYearTypeid.Value = Convert.ToString(gvMngRqstList.DataKeys[row.RowIndex].Values["App_Year_id"]).Trim();
        hdnempcode.Value = Convert.ToString(Session["Empcode"]);
        hdnAssessmentPeriodFrom.Value = Convert.ToString(gvMngRqstList.DataKeys[row.RowIndex].Values["AssessmentPeriodFrom"]).Trim();
        hdnAssessmentPeriodTo.Value = Convert.ToString(gvMngRqstList.DataKeys[row.RowIndex].Values["AssessmentPeriodTo"]).Trim();
        hdnPosMappid.Value = Convert.ToString(gvMngRqstList.DataKeys[row.RowIndex].Values["EmpPosMappId"]).Trim();
        hdnValidFrom.Value = Convert.ToString(gvMngRqstList.DataKeys[row.RowIndex].Values["ValidFrom"]).Trim();
        hdnValidTo.Value = Convert.ToString(gvMngRqstList.DataKeys[row.RowIndex].Values["ValidTo"]).Trim();
        hdn_ps.Value = Convert.ToString(gvMngRqstList.DataKeys[row.RowIndex].Values["p_s"]).Trim();
        hdn_pyear.Value = Convert.ToString(gvMngRqstList.DataKeys[row.RowIndex].Values["pyear"]).Trim();
        string[] strdate;
        string strfromDate = "";
        string strToDate = "";
        string strValidFrom = "";
        string strValidTo = "";
        if (Convert.ToString(hdnAssessmentPeriodFrom.Value).Trim() != "")
        {
            strdate = Convert.ToString(hdnAssessmentPeriodFrom.Value).Trim().Split('/');
            strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(hdnAssessmentPeriodTo.Value).Trim() != "")
        {
            strdate = Convert.ToString(hdnAssessmentPeriodTo.Value).Trim().Split('/');
            strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(hdnValidFrom.Value).Trim() != "")
        {
            strdate = Convert.ToString(hdnValidFrom.Value).Trim().Split('/');
            strValidFrom = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }
        if (Convert.ToString(hdnValidTo.Value).Trim() != "")
        {
            strdate = Convert.ToString(hdnValidTo.Value).Trim().Split('/');
            strValidTo = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        }

        getassessmentIdCreated(Convert.ToString(strfromDate), Convert.ToString(strToDate), Convert.ToString(Session["Empcode"]), Convert.ToDecimal(hdnAppYearTypeid.Value), Convert.ToDecimal(hdnPosMappid.Value));


        //if (Convert.ToString(btn.Text) == "Draft")
        if ((Convert.ToString(hdnAssessid.Value).Trim() != "0"))
        {
            getassessmentIdCreated(Convert.ToString(strfromDate), Convert.ToString(strToDate), Convert.ToString(Session["Empcode"]), Convert.ToDecimal(hdnAppYearTypeid.Value), Convert.ToDecimal(hdnPosMappid.Value));


            try
            {


                #region get Booklet With Assesment ID
                DataSet dspaymentVoucher = new DataSet();
                SqlParameter[] spars = new SqlParameter[2];
                decimal Assessid = 0;
                string A_from = "";
                string A_to = "";
                DataTable DT11 = new DataTable();
                DataTable DT12 = new DataTable();
                decimal Sumreviewee = 0;
                decimal Sumreviewer = 0;
                decimal Avgreviewee = 0;
                decimal Avgreviewer = 0;
                Assessid = Convert.ToDecimal(hdnAssessid.Value);
                spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                spars[0].Value = "sp_getPersonalDetails";

                spars[1] = new SqlParameter("@Assess_id", SqlDbType.Decimal);
                if (Assessid != 0)
                    spars[1].Value = Assessid;
                else
                    spars[1].Value = 0;


                dspaymentVoucher = spm.ApprgetDatasetList(spars, "SP_GETALL_Appraisal_DETAILS_Booklet_Self");

                if (dspaymentVoucher.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[0].Rows.Count; i++)
                    {
                        A_from = dspaymentVoucher.Tables[0].Rows[i]["FromDate"].ToString();
                        A_to = dspaymentVoucher.Tables[0].Rows[i]["ToDate"].ToString();
                        dspaymentVoucher.Tables[0].Rows[i]["Reviewee_Comment"] = spm.Decrypt(dspaymentVoucher.Tables[0].Rows[i]["Reviewee_Comment"].ToString());
                    }
                }

                if (dspaymentVoucher.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[1].Rows.Count; i++)
                    {

                        dspaymentVoucher.Tables[1].Rows[i]["AgreedTargetDesc"] = spm.Decrypt(dspaymentVoucher.Tables[1].Rows[i]["AgreedTargetDesc"].ToString());
                        dspaymentVoucher.Tables[1].Rows[i]["RevieweeDesc"] = spm.Decrypt(dspaymentVoucher.Tables[1].Rows[i]["RevieweeDesc"].ToString());
                        dspaymentVoucher.Tables[1].Rows[i]["ReviewerDesc"] = spm.Decrypt(dspaymentVoucher.Tables[1].Rows[i]["ReviewerDesc"].ToString());
                    }
                    dspaymentVoucher.Tables[1].AcceptChanges();
                }


                if (dspaymentVoucher.Tables[2].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[2].Rows.Count; i++)
                    {
                        dspaymentVoucher.Tables[2].Rows[i]["attsr"] = dspaymentVoucher.Tables[2].Rows[i]["attsr"].ToString();
                        dspaymentVoucher.Tables[2].Rows[i]["AttributesDesc"] = dspaymentVoucher.Tables[2].Rows[i]["AttributesDesc"].ToString();
                        dspaymentVoucher.Tables[2].Rows[i]["Comp_Detail_Desc"] = dspaymentVoucher.Tables[2].Rows[i]["Comp_Detail_Desc"].ToString();
                        dspaymentVoucher.Tables[2].Rows[i]["RevieweeRate"] = spm.Decrypt(dspaymentVoucher.Tables[2].Rows[i]["RevieweeRate"].ToString());
                        dspaymentVoucher.Tables[2].Rows[i]["ReviewerRate"] = spm.Decrypt(dspaymentVoucher.Tables[2].Rows[i]["ReviewerRate"].ToString());

                    }
                    dspaymentVoucher.Tables[2].AcceptChanges();
                    dspaymentVoucher.Tables[2].Columns.Add("TotalReviewee");
                    dspaymentVoucher.Tables[2].Columns.Add("TotalReviewer");
                    dspaymentVoucher.Tables[2].Columns.Add("AvgReviewee");
                    dspaymentVoucher.Tables[2].Columns.Add("AvgReviewer");
                    dspaymentVoucher.AcceptChanges();

                    for (int i = 0; i < dspaymentVoucher.Tables[2].Rows.Count; i++)
                    {
                        Avgreviewee = Avgreviewee + 1;
                        Avgreviewer = Avgreviewer + 1;
                        Sumreviewee = Sumreviewee + Convert.ToDecimal(0 + dspaymentVoucher.Tables[2].Rows[i]["RevieweeRate"].ToString());
                        Sumreviewer = Sumreviewer + Convert.ToDecimal(0 + dspaymentVoucher.Tables[2].Rows[i]["ReviewerRate"].ToString());
                    }

                    dspaymentVoucher.Tables[2].Rows[0]["TotalReviewee"] = Sumreviewee.ToString();
                    dspaymentVoucher.Tables[2].Rows[0]["TotalReviewer"] = Sumreviewer.ToString();
                    dspaymentVoucher.Tables[2].Rows[0]["AvgReviewee"] = decimal.Round(Sumreviewee / Avgreviewee, 2, MidpointRounding.AwayFromZero).ToString();
                    dspaymentVoucher.Tables[2].Rows[0]["AvgReviewer"] = decimal.Round(Sumreviewer / Avgreviewer, 2, MidpointRounding.AwayFromZero).ToString();
                    dspaymentVoucher.Tables[2].AcceptChanges();

                }


                if (dspaymentVoucher.Tables[3].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[3].Rows.Count; i++)
                    {
                        dspaymentVoucher.Tables[3].Rows[i]["PRD1"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["PRD1"].ToString());
                        dspaymentVoucher.Tables[3].Rows[i]["PRD2"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["PRD2"].ToString());
                        dspaymentVoucher.Tables[3].Rows[i]["PRD3"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["PRD3"].ToString());
                        dspaymentVoucher.Tables[3].Rows[i]["PRD4"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["PRD4"].ToString());

                        dspaymentVoucher.Tables[3].Rows[i]["IDP"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["IDP"].ToString());
                        dspaymentVoucher.Tables[3].Rows[i]["IDP1"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["IDP1"].ToString());
                        dspaymentVoucher.Tables[3].Rows[i]["IDP2"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["IDP2"].ToString());
                        dspaymentVoucher.Tables[3].Rows[i]["IDP3"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["IDP3"].ToString());
                    }
                    dspaymentVoucher.Tables[3].AcceptChanges();
                }

                if (dspaymentVoucher.Tables[4].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[4].Rows.Count; i++)
                    {
                        dspaymentVoucher.Tables[4].Rows[i]["Assess_Dev_Plan_id"] = dspaymentVoucher.Tables[4].Rows[i]["Assess_Dev_Plan_id"].ToString();
                        dspaymentVoucher.Tables[4].Rows[i]["Dev_area"] = spm.Decrypt(dspaymentVoucher.Tables[4].Rows[i]["Dev_area"].ToString());
                        dspaymentVoucher.Tables[4].Rows[i]["Method_desc"] = dspaymentVoucher.Tables[4].Rows[i]["Method_desc"].ToString();
                        dspaymentVoucher.Tables[4].Rows[i]["Timelines_desc"] = dspaymentVoucher.Tables[4].Rows[i]["Timelines_desc"].ToString();
                        dspaymentVoucher.Tables[4].Rows[i]["TrainingCodeDesc"] = dspaymentVoucher.Tables[4].Rows[i]["TrainingCodeDesc"].ToString();
                    }
                    dspaymentVoucher.Tables[4].AcceptChanges();
                }

                if (dspaymentVoucher.Tables[5].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[5].Rows.Count; i++)
                    {
                        dspaymentVoucher.Tables[5].Rows[i]["Strengthid"] = dspaymentVoucher.Tables[5].Rows[i]["Strengthid"].ToString();
                        dspaymentVoucher.Tables[5].Rows[i]["Strength"] = spm.Decrypt(dspaymentVoucher.Tables[5].Rows[i]["Strength"].ToString());
                    }
                    dspaymentVoucher.Tables[5].AcceptChanges();
                }

                if (dspaymentVoucher.Tables[6].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[6].Rows.Count; i++)
                    {
                        dspaymentVoucher.Tables[6].Rows[i]["pr1"] = "0";
                        dspaymentVoucher.Tables[6].Rows[i]["pr2"] = "0";
                        dspaymentVoucher.Tables[6].Rows[i]["pr3"] = "0";
                    }
                    dspaymentVoucher.Tables[6].AcceptChanges();
                }

                if (dspaymentVoucher.Tables[9].Rows.Count > 0)
                {

                    dspaymentVoucher.Tables[9].Rows[0]["oc1"] = dspaymentVoucher.Tables[9].Rows[0]["oc1"].ToString() == "0" ? "0" : spm.Decrypt(dspaymentVoucher.Tables[9].Rows[0]["oc1"].ToString());
                    dspaymentVoucher.Tables[9].Rows[0]["oc2"] = dspaymentVoucher.Tables[9].Rows[0]["oc2"].ToString() == "0" ? "0" : spm.Decrypt(dspaymentVoucher.Tables[9].Rows[0]["oc2"].ToString());
                    dspaymentVoucher.Tables[9].Rows[0]["oc3"] = dspaymentVoucher.Tables[9].Rows[0]["oc3"].ToString() == "0" ? "0" : spm.Decrypt(dspaymentVoucher.Tables[9].Rows[0]["oc3"].ToString());
                    dspaymentVoucher.Tables[9].AcceptChanges();
                }


                if (dspaymentVoucher.Tables[7].Rows.Count > 0)
                {
                    dspaymentVoucher.Tables[7].Rows[0]["Promo_Type_Desc"] = dspaymentVoucher.Tables[7].Rows[0]["Promo_Type_Desc"].ToString() == "0" ? "0" : spm.Decrypt(dspaymentVoucher.Tables[7].Rows[0]["Promo_Type_Desc"].ToString());
                    dspaymentVoucher.Tables[7].Rows[0]["pro1"] = "0";
                    dspaymentVoucher.Tables[7].Rows[0]["pro2"] = "0";
                    dspaymentVoucher.Tables[7].Rows[0]["pro3"] = "0";


                    dspaymentVoucher.Tables[7].AcceptChanges();
                }

                if (dspaymentVoucher.Tables[8].Rows.Count > 0)
                {
                    dspaymentVoucher.Tables[8].Rows[0]["Promo_Type_Desc"] = dspaymentVoucher.Tables[8].Rows[0]["Promo_Type_Desc"].ToString() == "0" ? "0" : spm.Decrypt(dspaymentVoucher.Tables[8].Rows[0]["Promo_Type_Desc"].ToString());
                    dspaymentVoucher.Tables[8].Rows[0]["pp1"] = "0";
                    dspaymentVoucher.Tables[8].Rows[0]["pp2"] = "0";
                    dspaymentVoucher.Tables[8].Rows[0]["pp3"] = "0";
                    dspaymentVoucher.Tables[8].AcceptChanges();
                }



                if (dspaymentVoucher.Tables[11].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[11].Rows.Count; i++)
                    {
                        dspaymentVoucher.Tables[11].Rows[i]["rating"] = spm.Decrypt(dspaymentVoucher.Tables[11].Rows[i]["rating"].ToString());
                        dspaymentVoucher.Tables[11].Rows[i]["ratingDesc"] = dspaymentVoucher.Tables[11].Rows[i]["rating"].ToString() + "-" + spm.Decrypt(dspaymentVoucher.Tables[11].Rows[i]["ratingDesc"].ToString());
                        //dspaymentVoucher.Tables[11].Rows[i]["ratingDesc"] = spm.Decrypt(dspaymentVoucher.Tables[11].Rows[i]["ratingDesc"].ToString());
                        dspaymentVoucher.Tables[11].AcceptChanges();


                    }

                    DataView dv = dspaymentVoucher.Tables[11].DefaultView;
                    dv.Sort = "rating desc";
                    DT11 = dv.ToTable();

                    // dspaymentVoucher.Tables[11] = dv.ToTable();
                    //  dspaymentVoucher.Tables[11].DefaultView.Sort = "rating desc";
                    // dspaymentVoucher.Tables[11].AcceptChanges();
                }

                if (dspaymentVoucher.Tables[12].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[12].Rows.Count; i++)
                    {
                        dspaymentVoucher.Tables[12].Rows[i]["rating"] = spm.Decrypt(dspaymentVoucher.Tables[12].Rows[i]["rating"].ToString());
                        dspaymentVoucher.Tables[12].Rows[i]["cnt"] = dspaymentVoucher.Tables[12].Rows[i]["rating"].ToString().Length;
                        //dspaymentVoucher.Tables[12].Rows[i]["ratingDesc"] = dspaymentVoucher.Tables[12].Rows[i]["rating"].ToString() + "-" + spm.Decrypt(dspaymentVoucher.Tables[12].Rows[i]["ratingDesc"].ToString());
                        dspaymentVoucher.Tables[12].Rows[i]["ratingDesc"] = spm.Decrypt(dspaymentVoucher.Tables[12].Rows[i]["ratingDesc"].ToString());

                        dspaymentVoucher.Tables[12].AcceptChanges();

                    }
                    DataView dv = dspaymentVoucher.Tables[12].DefaultView;


                    dv.Sort = "cnt desc,rating";
                    DT12 = dv.ToTable();
                }


                if (dspaymentVoucher.Tables[14].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[14].Rows.Count; i++)
                    {
                        dspaymentVoucher.Tables[14].Rows[i]["id"] = spm.Decrypt(dspaymentVoucher.Tables[14].Rows[i]["descript"].ToString());
                        //dspaymentVoucher.Tables[14].Rows[i]["desc"] = dspaymentVoucher.Tables[14].Rows[i]["id"] + "-" + dspaymentVoucher.Tables[14].Rows[i]["desc"];
                        dspaymentVoucher.Tables[14].Rows[i]["desc"] = dspaymentVoucher.Tables[14].Rows[i]["desc"];
                        dspaymentVoucher.Tables[14].AcceptChanges();
                    }

                }

                if (dspaymentVoucher.Tables[15].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[15].Rows.Count; i++)
                    {
                        dspaymentVoucher.Tables[15].Rows[i]["pr1"] = "0";
                        dspaymentVoucher.Tables[15].Rows[i]["pr2"] = "0";
                        dspaymentVoucher.Tables[15].Rows[i]["pr3"] = "0";
                    }
                    dspaymentVoucher.Tables[15].AcceptChanges();
                }

                

                if (dspaymentVoucher.Tables[0].Rows.Count > 0)
                {
                    ReportViewer ReportViewer1 = new ReportViewer();


                    ReportViewer1.LocalReport.Refresh();
                    ReportViewer1.LocalReport.DataSources.Clear();

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/Booklet.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();

                    ReportDataSource datasource = new ReportDataSource("dsPersonalData", dspaymentVoucher.Tables[0]);
                    ReportDataSource datasource1 = new ReportDataSource("dsSec1A", dspaymentVoucher.Tables[1]);
                    ReportDataSource datasourceB = new ReportDataSource("dsSec1B", dspaymentVoucher.Tables[2]);
                    ReportDataSource datasource2 = new ReportDataSource("dsSec1C", dspaymentVoucher.Tables[3]);
                    ReportDataSource datasource3 = new ReportDataSource("dsSec1CV_dev", dspaymentVoucher.Tables[4]);
                    ReportDataSource datasource4 = new ReportDataSource("dsSec1CV_str", dspaymentVoucher.Tables[5]);
                    ReportDataSource datasource5 = new ReportDataSource("dsSec2A", dspaymentVoucher.Tables[6]);
                    ReportDataSource datasource6 = new ReportDataSource("dsSec2B_GP", dspaymentVoucher.Tables[7]);
                    ReportDataSource datasource7 = new ReportDataSource("dsSec2B_PP", dspaymentVoucher.Tables[8]);
                    ReportDataSource datasource8 = new ReportDataSource("dsSec2C", dspaymentVoucher.Tables[9]);
                    ReportDataSource datasource9 = new ReportDataSource("dsReviewers", dspaymentVoucher.Tables[10]);

                    ReportDataSource datasource10 = new ReportDataSource("dspromRateDesc", DT11);
                    ReportDataSource datasource11 = new ReportDataSource("dsperformRateDesc", DT12);
                    ReportDataSource datasource12 = new ReportDataSource("dsmethodDesc", dspaymentVoucher.Tables[13]);
                    ReportDataSource datasource13 = new ReportDataSource("dsCompRating", dspaymentVoucher.Tables[14]);
                    ReportDataSource datasource14 = new ReportDataSource("dsSec2A1", dspaymentVoucher.Tables[15]);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.DataSources.Add(datasource1);
                    ReportViewer1.LocalReport.DataSources.Add(datasourceB);
                    ReportViewer1.LocalReport.DataSources.Add(datasource2);
                    ReportViewer1.LocalReport.DataSources.Add(datasource3);
                    ReportViewer1.LocalReport.DataSources.Add(datasource4);
                    ReportViewer1.LocalReport.DataSources.Add(datasource5);
                    ReportViewer1.LocalReport.DataSources.Add(datasource6);
                    ReportViewer1.LocalReport.DataSources.Add(datasource7);
                    ReportViewer1.LocalReport.DataSources.Add(datasource8);
                    ReportViewer1.LocalReport.DataSources.Add(datasource9);
                    ReportViewer1.LocalReport.DataSources.Add(datasource10);
                    ReportViewer1.LocalReport.DataSources.Add(datasource11);
                    ReportViewer1.LocalReport.DataSources.Add(datasource12);
                    ReportViewer1.LocalReport.DataSources.Add(datasource13);
                    ReportViewer1.LocalReport.DataSources.Add(datasource14);
                    ReportViewer1.LocalReport.Refresh();
                    //ReportViewer1.LocalReport.SetParameters(param);


                    #region Create PDF file
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;
                    DataTable DataTable1 = new DataTable();
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                    byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename=MyAppraisalForm." + extension);
                    try
                    {
                        Response.BinaryWrite(bytes);
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error while generating PDF.');", true);
                        Console.WriteLine(ex.StackTrace);
                    }

                    #endregion


                }
                                #endregion


            }
            catch (Exception ex)
            {
            }



        }
        else
        {
            try
            {


                #region get Booklet without Assessment ID
                DataSet dspaymentVoucher = new DataSet();
                SqlParameter[] spars = new SqlParameter[2];
                decimal Assessid = 0;
                string A_from = "";
                string A_to = "";
                DataTable DT11 = new DataTable();
                DataTable DT12 = new DataTable();
                decimal Sumreviewee = 0;
                decimal Sumreviewer = 0;
                decimal Avgreviewee = 0;
                decimal Avgreviewer = 0;
                Assessid = Convert.ToDecimal(hdnPosMappid.Value);
                spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                spars[0].Value = "sp_getPersonalDetails";

                spars[1] = new SqlParameter("@Assess_id", SqlDbType.Decimal);
                if (Assessid != 0)
                    spars[1].Value = Assessid;
                else
                    spars[1].Value = 0;


                dspaymentVoucher = spm.ApprgetDatasetList(spars, "SP_GETALL_Appraisal_DETAILS_Booklet_withoutAssessmentID");

                if (dspaymentVoucher.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[0].Rows.Count; i++)
                    {
                        A_from = dspaymentVoucher.Tables[0].Rows[i]["FromDate"].ToString();
                        A_to = dspaymentVoucher.Tables[0].Rows[i]["ToDate"].ToString();
                    }
                }

                if (dspaymentVoucher.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[1].Rows.Count; i++)
                    {

                        dspaymentVoucher.Tables[1].Rows[i]["AgreedTargetDesc"] = spm.Decrypt(dspaymentVoucher.Tables[1].Rows[i]["AgreedTargetDesc"].ToString());
                        dspaymentVoucher.Tables[1].Rows[i]["RevieweeDesc"] = spm.Decrypt(dspaymentVoucher.Tables[1].Rows[i]["RevieweeDesc"].ToString());
                        dspaymentVoucher.Tables[1].Rows[i]["ReviewerDesc"] = spm.Decrypt(dspaymentVoucher.Tables[1].Rows[i]["ReviewerDesc"].ToString());
                    }
                    dspaymentVoucher.Tables[1].AcceptChanges();
                }


                if (dspaymentVoucher.Tables[2].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[2].Rows.Count; i++)
                    {
                        dspaymentVoucher.Tables[2].Rows[i]["attsr"] = dspaymentVoucher.Tables[2].Rows[i]["attsr"].ToString();
                        dspaymentVoucher.Tables[2].Rows[i]["AttributesDesc"] = dspaymentVoucher.Tables[2].Rows[i]["AttributesDesc"].ToString();
                        dspaymentVoucher.Tables[2].Rows[i]["Comp_Detail_Desc"] = dspaymentVoucher.Tables[2].Rows[i]["Comp_Detail_Desc"].ToString();
                        dspaymentVoucher.Tables[2].Rows[i]["RevieweeRate"] = spm.Decrypt(dspaymentVoucher.Tables[2].Rows[i]["RevieweeRate"].ToString());
                        dspaymentVoucher.Tables[2].Rows[i]["ReviewerRate"] = spm.Decrypt(dspaymentVoucher.Tables[2].Rows[i]["ReviewerRate"].ToString());

                    }
                    dspaymentVoucher.Tables[2].AcceptChanges();
                    dspaymentVoucher.Tables[2].Columns.Add("TotalReviewee");
                    dspaymentVoucher.Tables[2].Columns.Add("TotalReviewer");
                    dspaymentVoucher.Tables[2].Columns.Add("AvgReviewee");
                    dspaymentVoucher.Tables[2].Columns.Add("AvgReviewer");
                    dspaymentVoucher.AcceptChanges();

                    for (int i = 0; i < dspaymentVoucher.Tables[2].Rows.Count; i++)
                    {
                        Avgreviewee = Avgreviewee + 1;
                        Avgreviewer = Avgreviewer + 1;
                        Sumreviewee = Sumreviewee + Convert.ToDecimal(0 + dspaymentVoucher.Tables[2].Rows[i]["RevieweeRate"].ToString());
                        Sumreviewer = Sumreviewer + Convert.ToDecimal(0 + dspaymentVoucher.Tables[2].Rows[i]["ReviewerRate"].ToString());
                    }

                    dspaymentVoucher.Tables[2].Rows[0]["TotalReviewee"] = Sumreviewee.ToString();
                    dspaymentVoucher.Tables[2].Rows[0]["TotalReviewer"] = Sumreviewer.ToString();
                    dspaymentVoucher.Tables[2].Rows[0]["AvgReviewee"] = decimal.Round(Sumreviewee / Avgreviewee, 2, MidpointRounding.AwayFromZero).ToString();
                    dspaymentVoucher.Tables[2].Rows[0]["AvgReviewer"] = decimal.Round(Sumreviewer / Avgreviewer, 2, MidpointRounding.AwayFromZero).ToString();
                    dspaymentVoucher.Tables[2].AcceptChanges();

                }


                if (dspaymentVoucher.Tables[3].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[3].Rows.Count; i++)
                    {
                        dspaymentVoucher.Tables[3].Rows[i]["PRD1"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["PRD1"].ToString());
                        dspaymentVoucher.Tables[3].Rows[i]["PRD2"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["PRD2"].ToString());
                        dspaymentVoucher.Tables[3].Rows[i]["PRD3"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["PRD3"].ToString());
                        dspaymentVoucher.Tables[3].Rows[i]["PRD4"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["PRD4"].ToString());

                        dspaymentVoucher.Tables[3].Rows[i]["IDP"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["IDP"].ToString());
                        dspaymentVoucher.Tables[3].Rows[i]["IDP1"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["IDP1"].ToString());
                        dspaymentVoucher.Tables[3].Rows[i]["IDP2"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["IDP2"].ToString());
                        dspaymentVoucher.Tables[3].Rows[i]["IDP3"] = spm.Decrypt(dspaymentVoucher.Tables[3].Rows[i]["IDP3"].ToString());
                    }
                    dspaymentVoucher.Tables[3].AcceptChanges();
                }

                if (dspaymentVoucher.Tables[4].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[4].Rows.Count; i++)
                    {
                        dspaymentVoucher.Tables[4].Rows[i]["Assess_Dev_Plan_id"] = dspaymentVoucher.Tables[4].Rows[i]["Assess_Dev_Plan_id"].ToString();
                        dspaymentVoucher.Tables[4].Rows[i]["Dev_area"] = spm.Decrypt(dspaymentVoucher.Tables[4].Rows[i]["Dev_area"].ToString());
                        dspaymentVoucher.Tables[4].Rows[i]["Method_desc"] = dspaymentVoucher.Tables[4].Rows[i]["Method_desc"].ToString();
                        dspaymentVoucher.Tables[4].Rows[i]["Timelines_desc"] = dspaymentVoucher.Tables[4].Rows[i]["Timelines_desc"].ToString();
                        dspaymentVoucher.Tables[4].Rows[i]["TrainingCodeDesc"] = dspaymentVoucher.Tables[4].Rows[i]["TrainingCodeDesc"].ToString();
                    }
                    dspaymentVoucher.Tables[4].AcceptChanges();
                }

                if (dspaymentVoucher.Tables[5].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[5].Rows.Count; i++)
                    {
                        dspaymentVoucher.Tables[5].Rows[i]["Strengthid"] = dspaymentVoucher.Tables[5].Rows[i]["Strengthid"].ToString();
                        dspaymentVoucher.Tables[5].Rows[i]["Strength"] = spm.Decrypt(dspaymentVoucher.Tables[5].Rows[i]["Strength"].ToString());
                    }
                    dspaymentVoucher.Tables[5].AcceptChanges();
                }

                if (dspaymentVoucher.Tables[6].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[6].Rows.Count; i++)
                    {
                        dspaymentVoucher.Tables[6].Rows[i]["pr1"] = "0";
                        dspaymentVoucher.Tables[6].Rows[i]["pr2"] = "0";
                        dspaymentVoucher.Tables[6].Rows[i]["pr3"] = "0";
                    }
                    dspaymentVoucher.Tables[6].AcceptChanges();
                }

                if (dspaymentVoucher.Tables[9].Rows.Count > 0)
                {

                    dspaymentVoucher.Tables[9].Rows[0]["oc1"] = dspaymentVoucher.Tables[9].Rows[0]["oc1"].ToString() == "0" ? "0" : spm.Decrypt(dspaymentVoucher.Tables[9].Rows[0]["oc1"].ToString());
                    dspaymentVoucher.Tables[9].Rows[0]["oc2"] = dspaymentVoucher.Tables[9].Rows[0]["oc2"].ToString() == "0" ? "0" : spm.Decrypt(dspaymentVoucher.Tables[9].Rows[0]["oc2"].ToString());
                    dspaymentVoucher.Tables[9].Rows[0]["oc3"] = dspaymentVoucher.Tables[9].Rows[0]["oc3"].ToString() == "0" ? "0" : spm.Decrypt(dspaymentVoucher.Tables[9].Rows[0]["oc3"].ToString());
                    dspaymentVoucher.Tables[9].AcceptChanges();
                }


                if (dspaymentVoucher.Tables[7].Rows.Count > 0)
                {
                    dspaymentVoucher.Tables[7].Rows[0]["Promo_Type_Desc"] = dspaymentVoucher.Tables[7].Rows[0]["Promo_Type_Desc"].ToString() == "0" ? "0" : spm.Decrypt(dspaymentVoucher.Tables[7].Rows[0]["Promo_Type_Desc"].ToString());
                    dspaymentVoucher.Tables[7].Rows[0]["pro1"] = "0";
                    dspaymentVoucher.Tables[7].Rows[0]["pro2"] = "0";
                    dspaymentVoucher.Tables[7].Rows[0]["pro3"] = "0";


                    dspaymentVoucher.Tables[7].AcceptChanges();
                }

                if (dspaymentVoucher.Tables[8].Rows.Count > 0)
                {
                    dspaymentVoucher.Tables[8].Rows[0]["Promo_Type_Desc"] = dspaymentVoucher.Tables[8].Rows[0]["Promo_Type_Desc"].ToString() == "0" ? "0" : spm.Decrypt(dspaymentVoucher.Tables[8].Rows[0]["Promo_Type_Desc"].ToString());
                    dspaymentVoucher.Tables[8].Rows[0]["pp1"] = "0";
                    dspaymentVoucher.Tables[8].Rows[0]["pp2"] = "0";
                    dspaymentVoucher.Tables[8].Rows[0]["pp3"] = "0";
                    dspaymentVoucher.Tables[8].AcceptChanges();
                }



                if (dspaymentVoucher.Tables[11].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[11].Rows.Count; i++)
                    {
                        dspaymentVoucher.Tables[11].Rows[i]["rating"] = spm.Decrypt(dspaymentVoucher.Tables[11].Rows[i]["rating"].ToString());
                        dspaymentVoucher.Tables[11].Rows[i]["ratingDesc"] = dspaymentVoucher.Tables[11].Rows[i]["rating"].ToString() + "-" + spm.Decrypt(dspaymentVoucher.Tables[11].Rows[i]["ratingDesc"].ToString());
                        //dspaymentVoucher.Tables[11].Rows[i]["ratingDesc"] = spm.Decrypt(dspaymentVoucher.Tables[11].Rows[i]["ratingDesc"].ToString());
                        dspaymentVoucher.Tables[11].AcceptChanges();


                    }

                    DataView dv = dspaymentVoucher.Tables[11].DefaultView;
                    dv.Sort = "rating desc";
                    DT11 = dv.ToTable();

                    // dspaymentVoucher.Tables[11] = dv.ToTable();
                    //  dspaymentVoucher.Tables[11].DefaultView.Sort = "rating desc";
                    // dspaymentVoucher.Tables[11].AcceptChanges();
                }

                if (dspaymentVoucher.Tables[12].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[12].Rows.Count; i++)
                    {
                        dspaymentVoucher.Tables[12].Rows[i]["rating"] = spm.Decrypt(dspaymentVoucher.Tables[12].Rows[i]["rating"].ToString());
                        dspaymentVoucher.Tables[12].Rows[i]["cnt"] = dspaymentVoucher.Tables[12].Rows[i]["rating"].ToString().Length;
                        //dspaymentVoucher.Tables[12].Rows[i]["ratingDesc"] = dspaymentVoucher.Tables[12].Rows[i]["rating"].ToString() + "-" + spm.Decrypt(dspaymentVoucher.Tables[12].Rows[i]["ratingDesc"].ToString());
                        dspaymentVoucher.Tables[12].Rows[i]["ratingDesc"] = spm.Decrypt(dspaymentVoucher.Tables[12].Rows[i]["ratingDesc"].ToString());

                        dspaymentVoucher.Tables[12].AcceptChanges();

                    }
                    DataView dv = dspaymentVoucher.Tables[12].DefaultView;


                    dv.Sort = "cnt desc,rating";
                    DT12 = dv.ToTable();
                }


                if (dspaymentVoucher.Tables[14].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[14].Rows.Count; i++)
                    {
                        dspaymentVoucher.Tables[14].Rows[i]["id"] = spm.Decrypt(dspaymentVoucher.Tables[14].Rows[i]["descript"].ToString());
                        //dspaymentVoucher.Tables[14].Rows[i]["desc"] = dspaymentVoucher.Tables[14].Rows[i]["id"] + "-" + dspaymentVoucher.Tables[14].Rows[i]["desc"];
                        dspaymentVoucher.Tables[14].Rows[i]["desc"] = dspaymentVoucher.Tables[14].Rows[i]["desc"];
                        dspaymentVoucher.Tables[14].AcceptChanges();
                    }

                }

                if (dspaymentVoucher.Tables[15].Rows.Count > 0)
                {
                    for (int i = 0; i < dspaymentVoucher.Tables[15].Rows.Count; i++)
                    {
                        dspaymentVoucher.Tables[15].Rows[i]["pr1"] = "0";
                        dspaymentVoucher.Tables[15].Rows[i]["pr2"] = "0";
                        dspaymentVoucher.Tables[15].Rows[i]["pr3"] = "0";
                    }
                    dspaymentVoucher.Tables[15].AcceptChanges();
                }

                if (dspaymentVoucher.Tables[0].Rows.Count > 0)
                {
                    ReportViewer ReportViewer1 = new ReportViewer();


                    ReportViewer1.LocalReport.Refresh();
                    ReportViewer1.LocalReport.DataSources.Clear();

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/procs/Booklet.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();

                    ReportDataSource datasource = new ReportDataSource("dsPersonalData", dspaymentVoucher.Tables[0]);
                    ReportDataSource datasource1 = new ReportDataSource("dsSec1A", dspaymentVoucher.Tables[1]);
                    ReportDataSource datasourceB = new ReportDataSource("dsSec1B", dspaymentVoucher.Tables[2]);
                    ReportDataSource datasource2 = new ReportDataSource("dsSec1C", dspaymentVoucher.Tables[3]);
                    ReportDataSource datasource3 = new ReportDataSource("dsSec1CV_dev", dspaymentVoucher.Tables[4]);
                    ReportDataSource datasource4 = new ReportDataSource("dsSec1CV_str", dspaymentVoucher.Tables[5]);
                    ReportDataSource datasource5 = new ReportDataSource("dsSec2A", dspaymentVoucher.Tables[6]);
                    ReportDataSource datasource6 = new ReportDataSource("dsSec2B_GP", dspaymentVoucher.Tables[7]);
                    ReportDataSource datasource7 = new ReportDataSource("dsSec2B_PP", dspaymentVoucher.Tables[8]);
                    ReportDataSource datasource8 = new ReportDataSource("dsSec2C", dspaymentVoucher.Tables[9]);
                    ReportDataSource datasource9 = new ReportDataSource("dsReviewers", dspaymentVoucher.Tables[10]);

                    ReportDataSource datasource10 = new ReportDataSource("dspromRateDesc", DT11);
                    ReportDataSource datasource11 = new ReportDataSource("dsperformRateDesc", DT12);
                    ReportDataSource datasource12 = new ReportDataSource("dsmethodDesc", dspaymentVoucher.Tables[13]);
                    ReportDataSource datasource13 = new ReportDataSource("dsCompRating", dspaymentVoucher.Tables[14]);
                    ReportDataSource datasource14 = new ReportDataSource("dsSec2A1", dspaymentVoucher.Tables[15]);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.DataSources.Add(datasource1);
                    ReportViewer1.LocalReport.DataSources.Add(datasourceB);
                    ReportViewer1.LocalReport.DataSources.Add(datasource2);
                    ReportViewer1.LocalReport.DataSources.Add(datasource3);
                    ReportViewer1.LocalReport.DataSources.Add(datasource4);
                    ReportViewer1.LocalReport.DataSources.Add(datasource5);
                    ReportViewer1.LocalReport.DataSources.Add(datasource6);
                    ReportViewer1.LocalReport.DataSources.Add(datasource7);
                    ReportViewer1.LocalReport.DataSources.Add(datasource8);
                    ReportViewer1.LocalReport.DataSources.Add(datasource9);
                    ReportViewer1.LocalReport.DataSources.Add(datasource10);
                    ReportViewer1.LocalReport.DataSources.Add(datasource11);
                    ReportViewer1.LocalReport.DataSources.Add(datasource12);
                    ReportViewer1.LocalReport.DataSources.Add(datasource13);
                    ReportViewer1.LocalReport.DataSources.Add(datasource14);
                    ReportViewer1.LocalReport.Refresh();
                    //ReportViewer1.LocalReport.SetParameters(param);


                    #region Create PDF file
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;
                    DataTable DataTable1 = new DataTable();
                    ReportViewer1.LocalReport.EnableExternalImages = true;
                    ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                    byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename=MyAppraisalForm." + extension);
                    try
                    {
                        Response.BinaryWrite(bytes);
                    }
                    catch (Exception ex)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Error while generating PDF.');", true);
                        Console.WriteLine(ex.StackTrace);
                    }

                    #endregion


                }
                #endregion

            }
            catch (Exception ex)
            {
            }
        }
    }
    protected void gvMngRqstList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow) // checking if row is datarow or not
        {
            //Label lblHasAtt = e.Row.FindControl("lblAtt") as Label;
            LinkButton lbtnViewAtt = e.Row.FindControl("lnkLeaveDetails") as LinkButton;
            string lsAppYearTypeid = gvMngRqstList.DataKeys[e.Row.RowIndex].Values[0].ToString();
            string lsfrdt = gvMngRqstList.DataKeys[e.Row.RowIndex].Values[2].ToString();
            string lstodt = gvMngRqstList.DataKeys[e.Row.RowIndex].Values[3].ToString();
            string Mappid = gvMngRqstList.DataKeys[e.Row.RowIndex].Values[4].ToString();

            string[] strdate;
            string strfromDate = "";
            string strToDate = "";
            if (Convert.ToString(lsfrdt).Trim() != "")
            {
                strdate = Convert.ToString(lsfrdt).Trim().Split('/');
                strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            if (Convert.ToString(lstodt).Trim() != "")
            {
                strdate = Convert.ToString(lstodt).Trim().Split('/');
                strToDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
            }
            getassessmentIdCreated(Convert.ToString(strfromDate), Convert.ToString(strToDate), Convert.ToString(Session["Empcode"]), Convert.ToDecimal(lsAppYearTypeid), Convert.ToDecimal(Mappid));
            lbtnViewAtt.Visible = true;
            //if ((Convert.ToString(hdnAssessid.Value).Trim() == "0"))
            //{
            //    lbtnViewAtt.Visible = true;
            // //   lbtnViewAtt.Attributes.Add("onclick", "return false;");
            //    lbtnViewAtt.Attributes.Add("onclick", "alert('The Performance Appraisal Form will be displayed once the Self Assessment is initiated.'); ");
            //}
            //else
            //{
            //    lbtnViewAtt.Visible = true;
                      
            //}
        }
    }

    #endregion

    #region Page Methods
    
    private void getAssessmentList()
    {
        try
        {            
            DataTable dtTravelRequest = new DataTable ();
            dtTravelRequest = spm.getSelfAssessList(strempcode);

            gvMngRqstList.DataSource = null;
            gvMngRqstList.DataBind();

            if (dtTravelRequest.Rows.Count > 0)
                {
                    gvMngRqstList.DataSource = dtTravelRequest;
                    gvMngRqstList.DataBind();
                }
            
        }
        catch (Exception ex)
        {

        }
    }
    public void getassessmentIdCreated(string fromDt, string ToDt, string empcode, decimal App_YearId, decimal id)
    {
        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[6];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "getassessmentIdCreated";

        spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(empcode);

        spars[2] = new SqlParameter("@App_Year_id", SqlDbType.Decimal);
        spars[2].Value = App_YearId;

        spars[3] = new SqlParameter("@from_date", SqlDbType.VarChar);
        spars[3].Value = fromDt;

        spars[4] = new SqlParameter("@todate", SqlDbType.VarChar);
        spars[4].Value = ToDt;
        spars[5] = new SqlParameter("@id1", SqlDbType.Decimal);
        spars[5].Value = id;

        dsTrDetails = spm.ApprgetDatasetList(spars, "[SP_GETALL_Appraisal_DETAILS]");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {
            hdnAssessid.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["assess_id"]).Trim();
            hdnstatus.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["krastatus"]).Trim();
        }
        else
        {

            hdnAssessid.Value = "0";
            hdnstatus.Value = "";
        }

    }
    
    #endregion 



    
}
