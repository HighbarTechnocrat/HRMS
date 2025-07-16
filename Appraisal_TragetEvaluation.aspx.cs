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



public partial class Appraisal_TragetEvaluation : System.Web.UI.Page
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
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }



            lblmessage.Text = "";
            lblmsg.Visible = false; ;
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {                    
                    lblmessage.Text = "";
                    editform.Visible = true;
                    divbtn.Visible = false;

                    //txtDescription.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtpointsAlloted.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtRevisedpointsAlloted.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                   // txtTargetAchDescription.Attributes.Add("onkeyDown", "return noanyCharecters(event);");
                    txtTargetAchPointAlloted.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtTargetAchPointAllotedReviewer.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    //txtRevisedPointAchieved.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
                    txtRevisedPointAchieved.Visible = false;                 
                    txtEmpCode.Text = Convert.ToString(Session["ReqEmpCode"]);
                    hdnGrade.Value = Convert.ToString(Session["Grade"]);
                    getActiveAppYear();                  
                 

                    if (Request.QueryString.Count > 0)
                    {
                        hdnclaimid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdnremid.Value = Convert.ToString(Request.QueryString[1]).Trim();

                        hdnAssessid.Value = Convert.ToString(Request.QueryString[0]).Trim();
                        hdnAssessKRAdtlid.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        mngAssess.Value = Convert.ToString(Request.QueryString[2]).Trim();
                        if (Convert.ToString(mngAssess.Value) == "PerR") //Performance Review Reviewer
                        {
                            if (Convert.ToString(hdnremid.Value)=="0")
                            {
                                lireviewee1.Visible = false;
                                lireviewee2.Visible = false;
                                lireviewee3.Visible = false;
                                lireviewee4.Visible = false;
                            }
                            spanuploadfile.Visible = false;
                            spanuploadfiletext.Visible = false;
                            btnSubmit.Visible = true;
                            btnDelete.Visible = false;
                            btnBack.Visible = true;
                            //txtKRA.Enabled = false;
                            txtDescription.Enabled = true;
                            //txtKPI.Enabled = false;
                            //txtUnit.Enabled = false;
                            //txtQuantity.Enabled = false;
                            txtpointsAlloted.Enabled = false;
                            txtTargetAchDescription.Enabled = false;
                            txtTargetAchPointAlloted.Enabled = false;
                            ploadexpfile.Visible = false;
                            gvuploadedFiles.Columns[1].Visible = false;
                            txtTargetAchReviewer.Focus();
                            //txtDissHeldOn.Enabled = false;
                                
                        }
                        if (Convert.ToString(mngAssess.Value) == "Rwee")//Self Assessment Reviewee
                        {
                            btnSubmit.Visible = true;
                            btnDelete.Visible = true;
                            btnBack.Visible = true;
                            txtTargetAchReviewer.Visible = false;
                            span1.Visible = false;
                            txtTargetAchPointAllotedReviewer.Visible = false;
                            span2.Visible = false;
                            span3.Visible = false;
                            span4.Visible = false;
                            span5.Visible = false;
                            //Span7.Visible = false;
                            Span6.Visible = false;
                            txtRevisedpointsAlloted.Visible = false;
                            //txtRevisedPointAchieved.Visible = false;
                        }
                        if (Convert.ToString(mngAssess.Value) == "ReV")//Self Assessment Reviewee View
                        {
                            btnSubmit.Visible = false;
                            btnDelete.Visible = false;
                            btnBack.Visible = true;
                           // txtKRA.Enabled = false;
                            txtDescription.Enabled = false;
                            //txtKPI.Enabled = false;
                            //txtUnit.Enabled = false;
                            //txtQuantity.Enabled = false;
                            txtpointsAlloted.Enabled = false;
                            txtTargetAchDescription.Enabled = false;
                            txtTargetAchPointAlloted.Enabled = false;
                            ploadexpfile.Visible = false;
                            gvuploadedFiles.Columns[1].Visible = false;
                            txtTargetAchReviewer.Enabled = false;
                            txtTargetAchPointAllotedReviewer.Enabled = false;
                            txtTargetAchReviewer.Visible = true;
                            txtTargetAchPointAllotedReviewer.Visible = true;
                           // txtRevisedPointAchieved.Enabled = false;
                            txtRevisedpointsAlloted.Enabled = false;
                            //txtDissHeldOn.Enabled = false;
                            span1.Visible = true;
                            span2.Visible = true;
                            span3.Visible = true;
                            span4.Visible = true;
                            span5.Visible = true;
                            
                        }
                        if ((Convert.ToString(mngAssess.Value) == "ReP") || (Convert.ToString(mngAssess.Value) == "RePV"))//Performance Review Reviewee 
                        {
                            btnSubmit.Visible = false;
                            btnDelete.Visible = false;
                            btnBack.Visible = true;
                           // txtKRA.Enabled = false;
                            txtDescription.Enabled = false;
                            //txtKPI.Enabled = false;
                            //txtUnit.Enabled = false;
                            //txtQuantity.Enabled = false;
                            txtpointsAlloted.Enabled = false;
                            txtTargetAchDescription.Enabled = false;
                            txtTargetAchPointAlloted.Enabled = false;
                            ploadexpfile.Visible = false;
                            gvuploadedFiles.Columns[1].Visible = false;
                            txtTargetAchReviewer.Enabled = false;
                            txtTargetAchPointAllotedReviewer.Enabled = false;
                            txtTargetAchReviewer.Visible = true;
                            txtTargetAchPointAllotedReviewer.Visible = true;
                           // txtRevisedPointAchieved.Enabled = false;
                            txtRevisedpointsAlloted.Enabled = false;
                            //txtDissHeldOn.Enabled = false;
                            span1.Visible = true;
                            span2.Visible = true;
                            span3.Visible = true;
                            span4.Visible = true;
                            span5.Visible = true;
                        }
                        if ((Convert.ToString(mngAssess.Value) == "N1") || (Convert.ToString(mngAssess.Value) == "N2") || (Convert.ToString(mngAssess.Value) == "AD1") || (Convert.ToString(mngAssess.Value) == "N1V") || (Convert.ToString(mngAssess.Value) == "N2V") || (Convert.ToString(mngAssess.Value) == "AD1V") || (Convert.ToString(mngAssess.Value) == "AD1VP"))//Recommendation
                        {
                            btnSubmit.Visible = false;
                            btnDelete.Visible = false;
                            spanuploadfile.Visible = false;
                            spanuploadfiletext.Visible = false;
                            btnBack.Visible = true;
                            //txtKRA.Enabled = false;
                            txtDescription.Enabled = false;
                            //txtDissHeldOn.Enabled = false;
                            //txtKPI.Enabled = false;
                            //txtUnit.Enabled = false;
                            //txtQuantity.Enabled = false;
                            txtpointsAlloted.Enabled = false;
                            txtTargetAchDescription.Enabled = false;
                            txtTargetAchPointAlloted.Enabled = false;
                            ploadexpfile.Visible = false;
                            gvuploadedFiles.Columns[1].Visible = false;
                            txtTargetAchReviewer.Enabled = false;
                            txtTargetAchPointAllotedReviewer.Enabled = false;
                           // txtRevisedPointAchieved.Enabled = false;
                            txtRevisedpointsAlloted.Enabled = false;
                        }
                       
                    }
                    if (Convert.ToString(hdnAssessKRAdtlid.Value).Trim() != "0")
                    {
                        //GetKRAQuantity();
                        //GetKRAUnit();                  
                        getKRADetails();
                        getUploadedFiles(Convert.ToDecimal(hdnAssessid.Value), Convert.ToDecimal(hdnAssessKRAdtlid.Value));
                    }
                    else
                    {
                        //GetKRAQuantity();
                        //GetKRAUnit();
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(mngAssess.Value) == "PerR") //Performance Review Reviewer
        {
            Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?gt65tgy=" + hdnAssessid.Value + "&dgfhjhvvv=PerR");
        }
        if (Convert.ToString(mngAssess.Value) == "Rwee")//Self Assessment Reviewee
        {
            Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?gt65tgy=" + hdnAssessid.Value + "&dgfhjhvvv=Rwee");
        }
        if (Convert.ToString(mngAssess.Value) == "ReV")//Self Assessment Reviewee View
        {
            Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?gt65tgy=" + hdnAssessid.Value + "&dgfhjhvvv=ReV");
        }
        if (Convert.ToString(mngAssess.Value) == "ReP")//Performance Review Reviewee 
        {
            Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?gt65tgy=" + hdnAssessid.Value + "&dgfhjhvvv=ReP");
        }
        if (Convert.ToString(mngAssess.Value) == "RePV")//Performance Review Reviewee 
        {
             Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?gt65tgy=" + hdnAssessid.Value + "&dgfhjhvvv=RePV");
        }
        if (Convert.ToString(mngAssess.Value) == "N1")//Performance Review Reviewee 
        {
            Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?gt65tgy=" + hdnAssessid.Value + "&dgfhjhvvv=N1&dbf678ndbnb=reco");
        }
        if (Convert.ToString(mngAssess.Value) == "N2")//Performance Review Reviewee 
        {
            Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?gt65tgy=" + hdnAssessid.Value + "&dgfhjhvvv=N2&dbf678ndbnb=reco");
        }
        if (Convert.ToString(mngAssess.Value) == "AD1")//Performance Review Reviewee 
        {
            Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?gt65tgy=" + hdnAssessid.Value + "&dgfhjhvvv=AD1&dbf678ndbnb=reco");
        }
        if((Convert.ToString(mngAssess.Value) == "N1V") || (Convert.ToString(mngAssess.Value) == "N2V") || (Convert.ToString(mngAssess.Value) == "AD1V"))
        {
            Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?gt65tgy=" + hdnAssessid.Value + "&dgfhjhvvv=" + mngAssess.Value + "&dbf678ndbnb=RevWerEd");
        }
        if (Convert.ToString(mngAssess.Value) == "AD1VP")
        {
            Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?gt65tgy=" + hdnAssessid.Value + "&dgfhjhvvv=" + mngAssess.Value + "&dbf678ndbnb=RevWerHd");
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       if (Convert.ToString(mngAssess.Value) == "Rwee")//Self Assessment Reviewee
       {
           #region Self Assessment Reviewee Submit

          

                    if (ploadexpfile.HasFiles)
                    {
                        if (ploadexpfile.PostedFiles.Count > 3)
                        {
                            lblmessage.Text = "Maximum 3 files can only be uploaded.";
                            return;
                        }
                        string[] validFileTypes = { "pdf", "docx", "doc", "xlsx", "xls" };
                        string ext = System.IO.Path.GetExtension(ploadexpfile.PostedFile.FileName);
                        bool isValidFile = false;
                        for (int i = 0; i < validFileTypes.Length; i++)
                        {
                            if (ext == "." + validFileTypes[i])
                            {
                                isValidFile = true;
                                break;
                            }
                        }
                        int filesize = 0;
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            HttpPostedFile PostedFile = Request.Files[i];
                            filesize = filesize + PostedFile.ContentLength;

                        }
                        if (filesize > 10364325)
                        {
                            lblmessage.Text = "The total files size should not exceed 10 MB.";
                            return;
                        }
                        if (!isValidFile)
                        {
                            lblmessage.Text = "Please upload a File with extension " +
                            string.Join(",", validFileTypes);
                            return;
                        }
                        
                    }
                    else
                    {
                        if (gvuploadedFiles.Rows.Count <= 0)
                        {
                           // lblmessage.Text = "Please Upload Files.";
                           // return;
                        }
                        
                    }
                   
                    
                    //if (Convert.ToString(txtKRA.Text) == "")
                    //{
                    //    lblmessage.Text = "Key Result Area cannot be blank";
                    //    return; 
                    //}
                    if (Convert.ToString(txtDescription.Text) == "")
                    {
                        lblmessage.Text = "Please enter the description for the Agreed Target.";
                        return;
                    }
                    //if (Convert.ToString(txtKPI.Text) == "")
                    //{
                    //    lblmessage.Text = "Key Performance Indicator cannot be blank";
                    //    return;
                    //}
                    if (Convert.ToString(txtTargetAchDescription.Text) == "")
                    {
                        lblmessage.Text = "Please enter the description for the Acheived Target.";
                        return;
                    }
                    //if (Convert.ToString(txtUnit.Text).Trim() == "")
                    //{
                    //    lblmessage.Text = "Target Unit cannot be blank";
                    //    return;
                    //}
                    //if (Convert.ToString(txtUnit.Text).Trim() == "Numbers")
                    //{
                    //    if (Convert.ToString(txtQuantity.Text).Trim() == "")
                    //    {
                    //        txtDissHeldOn.Enabled = false;
                    //        txtQuantity.Enabled = true;
                    //        lblmessage.Text = "Target Quantity cannot be blank";
                    //        return;
                    //    }
                    //}
                    //if (Convert.ToString(txtUnit.Text).Trim() == "Dates")
                    //{
                    //    if (Convert.ToString(txtDissHeldOn.Text).Trim() == "")
                    //    {
                    //        txtQuantity.Enabled = false;
                    //        txtDissHeldOn.Enabled = true;
                    //        lblmessage.Text = "Target Date cannot be blank";
                    //        return;
                    //    }
                    //}
                    string[] strdate;
                    string strsetdate = "";
                        #region date formatting
                    //if (Convert.ToString(txtDissHeldOn.Text).Trim() != "")
                    //    {
                    //        strdate = Convert.ToString(txtDissHeldOn.Text).Trim().Split('/');
                    //        strsetdate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
                    //    }
                 #endregion
                    getKRAPointsSum();
                    Double ptAlloted = 0;
                    Double ptAchieved = 0;
                   
                    if (Convert.ToString(txtpointsAlloted.Text) == "")
                        txtpointsAlloted.Text = "0";
                    if (Convert.ToString(txtTargetAchPointAlloted.Text) == "")
                        txtTargetAchPointAlloted.Text = "0";
                    //if ( Convert.ToDouble(txtTargetAchPointAlloted.Text) == 0)
                    //{
                    //    lblmessage.Text = "Points achieved can not be 0";
                    //   // txtTargetAchPointAlloted.Focus();
                    //    return; 
                    //}
                    if (Convert.ToDouble(txtpointsAlloted.Text) == 0)
                    {
                        lblmessage.Text = "Please enter the Points for the Agreed Target.";
                      //  txtpointsAlloted.Focus();
                        return;
                    }

                    if (Convert.ToDouble(txtTargetAchPointAlloted.Text) > Convert.ToDouble(txtpointsAlloted.Text))
                    {
                        lblmessage.Text = "The Points Achieved cannot be greater than the Points Allotted. Please Rectify.";                       
                        return;
                    }

                    ptAlloted = Convert.ToDouble(hdnTotalPtAlloted.Value) + Convert.ToDouble(txtpointsAlloted.Text);
                    ptAchieved = Convert.ToDouble(hdnTotalPtAchieved.Value) + Convert.ToDouble(txtTargetAchPointAlloted.Text);

                    if (ptAlloted > 100)
                    {
                        lblmessage.Text = "The Total Points Allotted should equate to 100. Please reallocate the Points.";
                      //  txtpointsAlloted.Focus();
                        return; 
                    }
                    if (ptAchieved > 100)
                    {
                        lblmessage.Text = "The Total Points Allotted should equate to 100. Please reallocate the Points.";
                      //  txtTargetAchPointAlloted.Focus();
                            return;
                    }
       
                    Int32 irecupdate = 0;
                    decimal maxtripid = 0; //= Convert.ToInt32(dtMaxTripID.Rows[0]["maxtripid"]);
                    string maxtripIdStr = null;


                     if (Convert.ToString(hdnAssessKRAdtlid.Value).Trim() == "0")
                        {
                            hdnsptype.Value = "sp_Insert_Assess_KRA_Detail";
                            //spm.InsertAssessKRADetails(Convert.ToDecimal(hdnAssessid.Value), spm.Encrypt(txtKRA.Text.Trim()), spm.Encrypt(txtDescription.Text.Trim()), spm.Encrypt(txtKPI.Text.Trim()), Convert.ToInt32(txtpointsAlloted.Text), spm.Encrypt(txtTargetAchDescription.Text.Trim()), Convert.ToInt32(txtTargetAchPointAlloted.Text), Convert.ToInt32(hdnAppYearTypeid.Value), txtUnit.Text, txtQuantity.Text, strsetdate, hdnsptype.Value);
                            spm.InsertAssessKRADetails(Convert.ToDecimal(hdnAssessid.Value), "", spm.Encrypt(txtDescription.Text.Trim()), "", Convert.ToInt32(txtpointsAlloted.Text), spm.Encrypt(txtTargetAchDescription.Text.Trim()), Convert.ToInt32(txtTargetAchPointAlloted.Text), Convert.ToInt32(hdnAppYearTypeid.Value), "", "", strsetdate, hdnsptype.Value,Convert.ToString(Session["Empcode"]).Trim());

                            get_MaxKRA_Det_id(Convert.ToDecimal(hdnAssessid.Value));
                            getMaxFileId(Convert.ToDecimal(hdnAssessid.Value), Convert.ToDecimal(hdnAssessKRAdtlid.Value));
                            maxtripid = Convert.ToDecimal(hdnfileid.Value);
                            if (Convert.ToDecimal(hdnfileid.Value) > 0)
                                UploadFiles(Convert.ToDecimal(hdnfileid.Value));
                         }
                    else
                         {
                                hdnsptype.Value = "sp_Update_Assess_KRA_Detail";
                                //spm.UpdateAssessKRADetails(Convert.ToDecimal(hdnAssessid.Value), Convert.ToDecimal(hdnAssessKRAdtlid.Value), spm.Encrypt(txtKRA.Text.Trim()), spm.Encrypt(txtDescription.Text.Trim()), spm.Encrypt(txtKPI.Text.Trim()), Convert.ToInt32(txtpointsAlloted.Text), spm.Encrypt(txtTargetAchDescription.Text.Trim()), Convert.ToInt32(txtTargetAchPointAlloted.Text), Convert.ToInt32(hdnAppYearTypeid.Value), txtUnit.Text, txtQuantity.Text, strsetdate, hdnsptype.Value);
                                spm.UpdateAssessKRADetails(Convert.ToDecimal(hdnAssessid.Value), Convert.ToDecimal(hdnAssessKRAdtlid.Value), "", spm.Encrypt(txtDescription.Text.Trim()), "", Convert.ToInt32(txtpointsAlloted.Text), spm.Encrypt(txtTargetAchDescription.Text.Trim()), Convert.ToInt32(txtTargetAchPointAlloted.Text), Convert.ToInt32(hdnAppYearTypeid.Value), "", "", strsetdate, hdnsptype.Value,Convert.ToString(Session["Empcode"]).Trim());            

                                getMaxFileId(Convert.ToDecimal(hdnAssessid.Value), Convert.ToDecimal(hdnAssessKRAdtlid.Value));
                                maxtripid=Convert.ToDecimal(hdnfileid.Value);
           
                                if (Convert.ToDecimal(hdnfileid.Value) > 0)
                                    UploadFiles(Convert.ToDecimal(hdnfileid.Value));
                          }

                    Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?tysder=" + hdnAssessid.Value + "&jukfq67fg=Rwee");
           #endregion
       }
       if (Convert.ToString(mngAssess.Value) == "PerR") //Performance Review Reviewer
       {
           #region Performance Review Reviewer Submit
           if (Convert.ToString(txtTargetAchPointAllotedReviewer.Text) == "")
               txtTargetAchPointAllotedReviewer.Text = "0";

           if (Convert.ToString(txtRevisedpointsAlloted.Text) == "")
               txtRevisedpointsAlloted.Text = txtpointsAlloted.Text;
           
           if (Convert.ToString(txtRevisedPointAchieved.Text) == "")
               //txtRevisedPointAchieved.Text = txtTargetAchPointAlloted.Text;
               txtRevisedPointAchieved.Text = "0";


           if (Convert.ToString(txtDescription.Text) == "")
           {
               lblmessage.Text = "Please enter the description for the Agreed Target.";
               return;
           }
           //if (Convert.ToString(txtTargetAchDescription.Text) == "")
           //{
           //    lblmessage.Text = "Reviewee Targets Achieved Description cannot be blank";
           //    return;
           //}
           if (Convert.ToString(txtTargetAchReviewer.Text.Trim()) == "")
           {
               lblmessage.Text = "Please enter the description for the Acheived Target.";
               return;
           }
           //if (Convert.ToDouble(txtRevisedPointAchieved.Text) > Convert.ToDouble(txtRevisedpointsAlloted.Text))
           //{
           //    lblmessage.Text = "Reviewee Targets Achieved : Revised Points Achieved can not be greater than Agreed Targets : Revised Points Alloted";
           //    return;
           //}
           if (Convert.ToDouble(txtTargetAchPointAllotedReviewer.Text) > Convert.ToDouble(txtRevisedpointsAlloted.Text))
           {
               lblmessage.Text = "The Points Achieved cannot be greater than the Points Allotted. Please Rectify.";
               return;
           }
           if (Convert.ToDouble(txtRevisedpointsAlloted.Text) > 100)
           {
               lblmessage.Text = "The Total Points Allotted should equate to 100. Please reallocate the Points.";
               return;
           }


           Double ptTargetAchieved = 0;
           Double ptRevPtAlloted = 0;
           Double ptRevPtRevieweeAchieved = 0;
           getKRAPointsSum();          

           //if (Convert.ToDouble(txtTargetAchPointAllotedReviewer.Text) == 0)
           //{
           //    lblmessage.Text = "Targets achieved points Alloted can not be 0";               
           //    return;
           //}
           ptTargetAchieved = Convert.ToDouble(hdnTargetTotalPtAchieved.Value) + Convert.ToDouble(txtTargetAchPointAllotedReviewer.Text);
           ptRevPtAlloted = Convert.ToDouble(hdnRevPointsAlloted.Value) + Convert.ToDouble(txtRevisedpointsAlloted.Text);
           //ptRevPtRevieweeAchieved = Convert.ToDouble(hdnRevRevieweeAchieved.Value) + Convert.ToDouble(txtRevisedPointAchieved.Text);

           if (ptTargetAchieved > 100)
           {
               lblmessage.Text = "The total of the Points Allotted should equate to 100. Please reallocate the Points.";               
               return;
           }

           if (ptRevPtAlloted > 100)
           {
               lblmessage.Text = "The Points Achieved cannot be greater than the Points Allotted. Please Rectify.";
               return;
           }

           //if (ptRevPtRevieweeAchieved > 100)
           //{
           //    lblmessage.Text = "Sum of Revised Reviewee Points achieved is " + ptRevPtRevieweeAchieved + ", Kindly edit and reduce Points achieved";
           //    return;
           //}

           //if (Convert.ToDouble(txtTargetAchPointAllotedReviewer.Text) > Convert.ToDouble(txtpointsAlloted.Text))
           //{
           //    lblmessage.Text = "Targets Achieved (Reviewer): Points Alloted can not be greater than Points Alloted";
           //    return;
           //}

          
                   if (Convert.ToString(hdnAssessKRAdtlid.Value).Trim() == "0")
                   {
                       hdnsptype.Value = "sp_Insert_Assess_KRA_DetailReviewer";
                       //spm.InsertAssessKRADetails(Convert.ToDecimal(hdnAssessid.Value), spm.Encrypt(txtKRA.Text.Trim()), spm.Encrypt(txtDescription.Text.Trim()), spm.Encrypt(txtKPI.Text.Trim()), Convert.ToInt32(txtpointsAlloted.Text), spm.Encrypt(txtTargetAchDescription.Text.Trim()), Convert.ToInt32(txtTargetAchPointAlloted.Text), Convert.ToInt32(hdnAppYearTypeid.Value), txtUnit.Text, txtQuantity.Text, strsetdate, hdnsptype.Value);
                       spm.InsertAssessKRADetailsReviewer(Convert.ToDecimal(hdnAssessid.Value), "", spm.Encrypt(txtDescription.Text.Trim()), "", Convert.ToInt32(txtRevisedpointsAlloted.Text), "", Convert.ToInt32(txtRevisedPointAchieved.Text), Convert.ToInt32(hdnAppYearTypeid.Value), spm.Encrypt(txtTargetAchReviewer.Text.Trim()), Convert.ToInt32(txtTargetAchPointAllotedReviewer.Text), hdnsptype.Value, Convert.ToString(Session["Empcode"]).Trim());

                   }
                   else
                   {

                       hdnsptype.Value = "sp_Update_Assess_KRA_DetailReviewer";
                       //spm.UpdateAssessKRADetailsReviewer(Convert.ToDecimal(hdnAssessid.Value), Convert.ToDecimal(hdnAssessKRAdtlid.Value), spm.Encrypt(txtTargetAchReviewer.Text.Trim()), Convert.ToInt32(txtTargetAchPointAllotedReviewer.Text), hdnsptype.Value);            
                       spm.UpdateAssessKRADetailsReviewer(Convert.ToDecimal(hdnAssessid.Value), Convert.ToDecimal(hdnAssessKRAdtlid.Value), spm.Encrypt(txtTargetAchReviewer.Text.Trim()), Convert.ToInt32(txtTargetAchPointAllotedReviewer.Text), spm.Encrypt(txtDescription.Text.Trim()), "", Convert.ToInt32(txtRevisedpointsAlloted.Text), Convert.ToInt32(txtRevisedPointAchieved.Text), hdnsptype.Value, Convert.ToString(Session["Empcode"]).Trim());
                   }
           Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?tysder=" + hdnAssessid.Value + "&jukfq67fg=PerR");
           #endregion
       }

       
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        hdnsptype.Value = "sp_Delete_Assess_KRA_Detail";
        spm.DeleteAssessKRADetails(Convert.ToDecimal(hdnAssessid.Value), Convert.ToDecimal(hdnAssessKRAdtlid.Value), hdnsptype.Value);
        //Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?assessid=" + hdnclaimid.Value + "&mngAssess=1");
        
        if (Convert.ToString(mngAssess.Value) == "Rwee")//Self Assessment Reviewee
        {
            Response.Redirect("~/procs/Appraisal_SelfAssessment.aspx?rtfDelgh=" + hdnAssessid.Value + "&htygdh=Rwee");
        }
        
    }
    protected void lnkviewfile_Click(object sender, EventArgs e)
    {
        try
        {
            hdnfileid.Value = "";
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            Int32 ifileid = Convert.ToInt32(gvuploadedFiles.DataKeys[row.RowIndex].Values[0]);
            LinkButton lnkviewfile = (LinkButton)row.FindControl("lnkviewfile");

            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["AppraisalFiles"]).Trim()), lnkviewfile.Text);
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
    protected void lnkDeleteexpFile_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            Int32 ifileid = Convert.ToInt32(gvuploadedFiles.DataKeys[row.RowIndex].Values[0]);
            Int32 ifiledetailid = Convert.ToInt32(gvuploadedFiles.DataKeys[row.RowIndex].Values[1]);
            LinkButton lnkviewfile = (LinkButton)row.FindControl("lnkviewfile");
            hdnfileid.Value = "";
            String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["AppraisalFiles"]).Trim()), lnkviewfile.Text);

            if (System.IO.File.Exists(strfilepath))
            {
                System.IO.File.Delete(strfilepath);
                hdnfileid.Value = Convert.ToString(ifileid);
                hdnsptype.Value = "sp_Delete_Assess_KRA_Files";                
                spm.DeleteAssessKRADetails(Convert.ToDecimal(ifileid), Convert.ToDecimal(ifiledetailid), hdnsptype.Value);        
                getUploadedFiles(Convert.ToDecimal(hdnAssessid.Value), Convert.ToDecimal(hdnAssessKRAdtlid.Value));
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }


    }
    protected void gvuploadedFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            ////LinkButton lnkDeleteexpFile
            //if (Convert.ToString(hdnmainexpStatus.Value).Trim() == "1" && Convert.ToString(hdn_apprStatus.Value).Trim() == "Approved")
            //{
            //    e.Row.Cells[1].Visible = false;
            //}
            //if (Convert.ToString(hdnmainexpStatus.Value).Trim() == "2")
            //{
            //    e.Row.Cells[1].Visible = false;
            //}
            //if (Convert.ToString(hdnmainexpStatus.Value).Trim() == "3" || Convert.ToString(hdnmainexpStatus.Value).Trim() == "4")
            //{
            //    e.Row.Cells[1].Visible = false;
            //}
            e.Row.Cells[1].Visible = true;
        }
    }
    //protected void lstUnit_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtUnit.Text = lstUnit.SelectedItem.Text;
    //    PopupControlExtender2.Commit(lstUnit.SelectedItem.Text);
    //    hdnUnit.Value = lstUnit.SelectedItem.Text;
       
    //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "SetCntrls('" + lstUnit.SelectedItem.Text + "');", true);
    //}
    //protected void lstQuantity_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtQuantity.Text = lstQuantity.SelectedItem.Text;
    //    PopupControlExtender1.Commit(lstQuantity.SelectedItem.Text);
    //}
    //protected void txtDissHeldOn_TextChanged(object sender, EventArgs e)
    //{
    //    if (lstUnit.SelectedItem.Text == "Dates")
    //    {
    //        txtDissHeldOn.Enabled = true;
    //        txtQuantity.Enabled = false;
    //    }
    //    if (lstUnit.SelectedItem.Text == "Numbers")
    //    {
    //        txtDissHeldOn.Enabled = false;
    //        txtQuantity.Enabled = true;
    //    }
    //}

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
            btnSubmit.Enabled = false;           
            lblmessage.Text = "Sorry You are not entitled!";
        }
    }
     private void getActiveAppYear()
    {

        DataSet dsTrDetails = new DataSet();
        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "sp_get_ActiveAppYear";

        spars[1] = new SqlParameter("@location", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString("1100");

        dsTrDetails = spm.ApprgetDatasetList(spars, "[SP_GETALL_Appraisal_DETAILS]");

        if (dsTrDetails.Tables[0].Rows.Count > 0)
        {          
            hdnAppYearStartDate.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["App_From_Date"]).Trim();
            hdnAppYearEndDate.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["App_To_Date"]).Trim();
            hdnAppAppType.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["App_Type"]).Trim();           
            hdnAppYearTypeid.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["App_Year_id"]).Trim();
        }

    }
     private void getMaxFileId( decimal id1, decimal id2)
     {
         DataTable dtTrDetails = new DataTable();
         dtTrDetails = spm.GetFileID(id1, id2);
         if (dtTrDetails.Rows.Count > 0)
         {
             hdnfileid.Value = Convert.ToString(dtTrDetails.Rows[0]["fileid"]).Trim();
         }
         else
         {
             //lblmessage.Visible = true;
             btnSubmit.Enabled = false;            
         }
     }
     private void get_MaxKRA_Det_id(decimal id1)
     {
         DataTable dtTrDetails = new DataTable();
         dtTrDetails = spm.get_MaxKRA_Det_id(id1);
         if (dtTrDetails.Rows.Count > 0)
         {
             hdnAssessKRAdtlid.Value = Convert.ToString(dtTrDetails.Rows[0]["KRA_Det_id"]).Trim();
         }
         else
         {
             //lblmessage.Visible = true;
             btnSubmit.Enabled = false;
         }
     }  
     private void getKRADetails()
     {

         
         DataSet dsTrDetails = new DataSet();
         SqlParameter[] spars = new SqlParameter[3];

         spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
         spars[0].Value = "get_Assess_KRA_details_edit";

         spars[1] = new SqlParameter("@Assess_id", SqlDbType.VarChar);
         spars[1].Value = Convert.ToString(hdnAssessid.Value);

         spars[2] = new SqlParameter("@KRA_Det_id", SqlDbType.VarChar);
         spars[2].Value = Convert.ToString(hdnAssessKRAdtlid.Value);

         dsTrDetails = spm.ApprgetDatasetList(spars, "SP_GETALL_Appraisal_DETAILS");

         if (dsTrDetails.Tables[0].Rows.Count > 0)
         {
             txtDescription.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["KRA_Description"]).Trim());
             txtpointsAlloted.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["KRA_Points_Alloted"]).Trim();
             txtTargetAchDescription.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["KRA_Target_Achieved_Description"]).Trim());
             txtTargetAchPointAlloted.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["KRA_Target_Achieved_Points"]).Trim();
             //txtKPI.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["KPI"]).Trim());
             //txtKRA.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["KRA_Header"]).Trim());
             
             //lstUnit.SelectedValue = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["unit_id"]);
             //lstQuantity.SelectedValue = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["qty_id"]);
             
             //txtUnit.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["unit_desc"]).Trim();
             //txtQuantity.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["qty_desc"]).Trim();
             
             txtTargetAchReviewer.Text = spm.Decrypt(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Target_Achieved_Des_Reviewer"]).Trim());
             txtTargetAchPointAllotedReviewer.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Target_Achieved_Points_Reviewer"]).Trim();
             //txtDissHeldOn.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["KRADate"]).Trim();
             if(Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Rev_Points_Alloted"]).Trim() == "")
             {
                txtRevisedpointsAlloted.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["KRA_Points_Alloted"]).Trim();
             }
             else
             {
                 txtRevisedpointsAlloted.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Rev_Points_Alloted"]).Trim();
             }
             if (Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Rev_Points_Reviewee"]).Trim() == "")
             {
                 //txtRevisedPointAchieved.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["KRA_Target_Achieved_Points"]).Trim();
                 txtRevisedPointAchieved.Text = "0";
             }
             else
             {
                 //txtRevisedPointAchieved.Text = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Rev_Points_Reviewee"]).Trim();
                 txtRevisedPointAchieved.Text = "0";
             }
             

             //hdnUnit.Value = txtUnit.Text;
             //if ((txtUnit.Text == "Dates") && (Convert.ToString(mngAssess.Value) == "Rwee"))
             //{
             //    txtDissHeldOn.Enabled = true;
             //    txtQuantity.Enabled = false;
             //    txtQuantity.Text = "";
             //}
             //if ((txtUnit.Text == "Numbers") && (Convert.ToString(mngAssess.Value) == "Rwee"))
             //{
             //    txtDissHeldOn.Enabled = false;
             //    txtQuantity.Enabled = true;
             //    txtDissHeldOn.Text = "";
             //}
         }


     }
     public void  getUploadedFiles(decimal id1, decimal id2)
     {

         DataTable dtTrDetails = new DataTable();
         SqlParameter[] spars = new SqlParameter[3];

         spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
         spars[0].Value = "get_UplodedfileDetails";

         spars[1] = new SqlParameter("@Assess_id", SqlDbType.Decimal);
         spars[1].Value = id1;

         spars[2] = new SqlParameter("@KRA_Det_id", SqlDbType.Decimal);
         spars[2].Value = id2;//hdnexp_id.Value




         dtTrDetails = spm.apprgetDataList(spars, "SP_GETALL_Appraisal_DETAILS");

         gvuploadedFiles.DataSource = null;
         gvuploadedFiles.DataBind();
         if (dtTrDetails.Rows.Count > 0)
         {
             gvuploadedFiles.DataSource = dtTrDetails;
             gvuploadedFiles.DataBind();
         }
     }
     private void UploadFiles(decimal expid)
     {
        
         
         // Check File Prasent or not  
         if (ploadexpfile.HasFiles)
         {
             StringBuilder sbinsertmain = new StringBuilder();
             StringBuilder sbinsertValues = new StringBuilder();
             sbinsertmain.Append("Insert into Appr_Tra_Assess_KRA_Detail_File([File_Id],[file_name],[File_Dtl_Id]) Values  ");
             string serverfolder = string.Empty;
             string serverpath = string.Empty;
             serverfolder = Server.MapPath("");
             String strfileName = "";
             string[] strdate;
             string strfromDate = "";
             #region date formatting
             //if (Convert.ToString(hdn.Text).Trim() != "")
             //{
             //    strdate = Convert.ToString(txtFromdate.Text).Trim().Split('/');
             //    strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
             //}


             #endregion
             Int32 ifilecnt = 1;


             foreach (HttpPostedFile postfiles in ploadexpfile.PostedFiles)
             {
                 strfileName = "";
                 //strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + postfiles.FileName + "_" + ifilecnt;
                 //strfileName = txtEmpCode.Text + "_" + ifilecnt + "_" + postfiles.FileName;
                 strfileName = Convert.ToString(Session["Empcode"]) + "_" + ifilecnt + "_" + expid + "_" + postfiles.FileName;
                 
                 postfiles.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["AppraisalFiles"]).Trim()), strfileName));

                 if (Convert.ToString(sbinsertValues).Trim() == "")
                     sbinsertValues.Append(" ( " + expid + ",'" + strfileName + "'," + ifilecnt + " ) ");
                 else
                     sbinsertValues.Append(" , ( " + expid + ",'" + strfileName + "'," + ifilecnt + " ) ");
                 ifilecnt += 1;
             }
             if (Convert.ToString(sbinsertValues).Trim() != "")
             {
                 Int32 irecupdate = 0;
                 irecupdate = spm.add_KRA_Files(Convert.ToString(sbinsertmain).Trim() + Convert.ToString(sbinsertValues).Trim());

             }

         }
     }
     //public void GetKRAQuantity()
     //{
     //    DataTable KRAQuantity = new DataTable();
     //    KRAQuantity = spm.getKRAQuantity();
     //    if (KRAQuantity.Rows.Count > 0)
     //    {
     //        lstQuantity.DataSource = KRAQuantity;
     //        lstQuantity.DataTextField = "qty_desc";
     //        lstQuantity.DataValueField = "qty_id";
     //        lstQuantity.DataBind();

     //    }
     //}
     //public void GetKRAUnit()
     //{
     //    DataTable KRAUnit = new DataTable();
     //    KRAUnit = spm.getKRAUnit();
     //    if (KRAUnit.Rows.Count > 0)
     //    {
     //        lstUnit.DataSource = KRAUnit;
     //        lstUnit.DataTextField = "unit_desc";
     //        lstUnit.DataValueField = "unit_id";
     //        lstUnit.DataBind();
     //    }
     //}
     public void getKRAPointsSum()
     {
         DataSet dsTrDetails = new DataSet();
         SqlParameter[] spars = new SqlParameter[3];

         spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
         spars[0].Value = "getKRAPointsSum_withoutKRAdtlId";

         spars[1] = new SqlParameter("@Assess_id", SqlDbType.VarChar);
         spars[1].Value = Convert.ToString(hdnAssessid.Value);

         spars[2] = new SqlParameter("@KRA_det_id", SqlDbType.VarChar);
         spars[2].Value = Convert.ToString(hdnAssessKRAdtlid.Value);


         dsTrDetails = spm.ApprgetDatasetList(spars, "SP_GETALL_Appraisal_DETAILS");

         if (dsTrDetails.Tables[0].Rows.Count > 0)
         {
             hdnTotalPtAlloted.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Points_Alloted"]).Trim();
             hdnTotalPtAchieved.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Achieved_Points"]).Trim();
             hdnTargetTotalPtAchieved.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["TargetAchieved_Points"]).Trim();
             hdnRevPointsAlloted.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["RevPointsAlloted"]).Trim();
             hdnRevRevieweeAchieved.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["RevRevieweeAchieved"]).Trim();  
         }
     }

     #endregion
       
}
