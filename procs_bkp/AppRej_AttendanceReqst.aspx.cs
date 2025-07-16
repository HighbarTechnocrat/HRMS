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



public partial class AppRej_AttendanceReqst : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    String strloginid = "";
    String strempcode = "";
    String strempcode_app;
    SP_Methods spm = new SP_Methods();

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

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

            strempcode_app = Convert.ToString(Session["Empcode"]).Trim();
            hdnCurrentAppName.Value = Convert.ToString(Session["emp_loginName"]).Trim();
            lblmsg.Visible = false;
            hdnleaveconditiontypeid.Value = "11";
            if (Request.QueryString.Count > 0)
            {
                //s hdnReqid.Value = "1";
                strempcode = Convert.ToString(Request.QueryString[0]).Trim();
                hdnreqid.Value = Convert.ToString(Request.QueryString[1]).Trim();
                hdncrntApp_id.Value = Convert.ToString(Request.QueryString[2]).Trim();
                hdnappr_type.Value = Convert.ToString(Request.QueryString[3]).Trim();
               // txtRequest_Date.Text = hdnreqdt.Value;
                string[] strdate;
                string strreqDate;
                #region date formatting

                if (Convert.ToString(hdnreqdt.Value).Trim() != "")
                {
                    strdate = Convert.ToString(hdnreqdt.Value).Trim().Split('/');
                    strreqDate = Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]) + "-" + Convert.ToString(strdate[2]);
                    hdnreqdt.Value = strreqDate;
                }
                else
                {
                    strreqDate = "";
                }

                #endregion

            }
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    divbtn.Visible = false;
                    divmsg.Visible = false;
               
                 
                    DisplayProfileProperties();
                  //  getApproverlevels(strempcode);
                    get_Appr_id_forApproval();
                    getSelectedEmpDetails_View();
                    hdnAppr_Cnt.Value = "2";
                    getApproverdata();
                    getSelectedEmpAttendanceDetails_View();
                    getApproverdata();
                    
                    
                    
                    GetCuurentApprID();
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

    private DataSet get_HRApproval_mailsDtls_SelfAppr()
    {
        DataSet dsList = new DataSet();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_HREmailAttend_isSelfAppr";

            spars[1] = new SqlParameter("@approver_type", SqlDbType.VarChar);
            spars[1].Value = "HRLWP";

            spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[2].Value = txtEmpCode.Text;



            dsList = spm.getDatasetList(spars, "Usp_getEmployee_Details_All");

            if (dsList.Tables != null)
            {
                if (dsList.Tables[0].Rows.Count > 0)
                {
                    hdnHR_ApproverCode.Value = Convert.ToString(dsList.Tables[0].Rows[0]["A_EMP_CODE"]).Trim();
                    hdnHR_EMailID.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
                    hdnHR_Appr_id.Value = Convert.ToString(dsList.Tables[0].Rows[0]["APPR_ID"]).Trim();
                    hdnHR_Appr_Name.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Name"]).Trim();
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
        return dsList;
    }


    protected void getApproverdata()
    {
        hdnleaveconditiontypeid.Value = "11";
        DataSet ldstmp = new DataSet();
        DataTable dtApproverEmailIds;

        if (Convert.ToString(hdnisSelfAppr.Value).Trim() == "Y")
            ldstmp = get_HRApproval_mailsDtls_SelfAppr();
        else
            ldstmp = spm.GetApproverEmailID_Attendance(txtEmpCode.Text, hflGrade.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value));

        //IsEnabledFalse (true);
        lstApprover.Items.Clear();
        if (ldstmp.Tables[0].Rows.Count > 0)
        {
           hdnAppr_Cnt.Value = Convert.ToString(ldstmp.Tables[0].Rows.Count);
           // hdnApproverMailid.Value = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];
            //hdnApproverid.Value = Convert.ToString(dtApproverEmailIds.Rows[0]["APPR_ID"]).Trim();

            //lstApprover.DataSource = null;
            //lstApprover.DataBind();
            string strApprname="";
            for (Int32 irow = 0; irow < ldstmp.Tables[0].Rows.Count;irow++ )
            {
                strApprname=Convert.ToString( ldstmp.Tables[0].Rows[irow]["Emp_Name"]).Trim();
                if(irow==0)
                {
                    ldstmp.Tables[0].Rows[irow]["Emp_Name"] = "Approver 1: " + strApprname;
                }
                if (irow == 1)
                {
                    ldstmp.Tables[0].Rows[irow]["Emp_Name"] = "Approver 2: " + strApprname;
                }
                if (irow == 2)
                {
                    ldstmp.Tables[0].Rows[irow]["Emp_Name"] = "Approver 3: " + strApprname;
                }
            }
            //if (ldstmp.Tables[0].Rows.Count==2)
            //{
            //    ldstmp.Tables[0].Rows.Add();
            //}
            dtApproverEmailIds = ldstmp.Tables[0];
            lstApprover.DataSource = dtApproverEmailIds;
            lstApprover.DataTextField = "Emp_Name";
            lstApprover.DataValueField = "APPR_ID";
            lstApprover.DataBind();

            //hdn.Approvers_code = (string)dtApproverEmailIds.Rows[0]["A_EMP_CODE"];
            hflapprcode.Value = (string)dtApproverEmailIds.Rows[0]["A_EMP_CODE"];

        }
        else
        {
            lblmessage.Text = "Since you are not assigned under any approver you can not apply  for any leave, please contact HR";

        }


    }
    protected void GetCuurentApprID()
    {
        int capprid;
        string Actions = "";
        DataTable dtCApprID = new DataTable();
        dtCApprID = spm.GetCurrentApprID_Attendance(hdnreqid.Value, strempcode_app);
        capprid = (int)dtCApprID.Rows[0]["APPR_ID"];
        Actions = (string)dtCApprID.Rows[0]["Action"];
        hdnCurrentID.Value = capprid.ToString();

        if (Convert.ToString(hdnCurrentID.Value).Trim() == "")
        {
            lblmessage.Text = "Acton on this Request not yet taken by other approvals";
            return;
        }
        else if (Convert.ToString(Actions).Trim() != "Pending")
        {
            lblmessage.Text = "You already actioned for this request";
            return;
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        
    }

    public void BindLeaveRequestProperties()
    {
        //   lpm.Emp_Code = txtEmpCode.Text;
        //   lpm.LeaveDays = Convert.ToInt32(txtLeaveDays.Text);
        //   lpm.Leave_Type_id = Convert.ToInt32(ddlLeaveType.SelectedValue);
        ////   lpm.Leave_FromDate = Convert.ToDateTime(txtFromdate.Text);
        //   lpm.Leave_From_for = ddlFromFor.Text;
        ////   lpm.Leave_ToDate = Convert.ToDateTime(txtToDate.Text);
        //   lpm.Leave_To_For = ddlToFor.Text;
        //   lpm.Reason = txtReason.Text;
        //   lpm.Grade = hflGrade.Value.ToString();
        //   lpm.Approvers_code = hflapprcode.Value;
        //   lpm.appr_id = Convert.ToInt32(lstApprover.SelectedValue);
        //   lpm.EmailAddress = hflEmailAddress.Value;
        //   lpm.Emp_Name = hflEmpName.Value;

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

                    fillcountry();
                    country country = classaddress.GetCountryDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["country"].ToString()));
                    // ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByText(country.countryName.Trim()));

                    fillstate(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["country"].ToString()));
                    states state = classaddress.GetStateDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["state"].ToString()));
                    // ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByText(state.statename.Trim()));

                    fillcity(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["state"].ToString()));

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

    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        Update_AttendanceApproval();
    }
    protected void btnSaveChanges_Click(object sender, System.EventArgs e)
    {




    }

    public void mywall()
    {
        //DataSet dswall = classproduct.gettopmywall();
        //dswall = saveXml2(dswall, "mywall.xml");
        //if (!dswall.Tables[0].Columns.Contains("videoembed") || !dswall.Tables[0].Columns.Contains("filename") || !dswall.Tables[0].Columns.Contains("movietrailorcode") || !dswall.Tables[0].Columns.Contains("bigimage"))
        //{
        //    if (!dswall.Tables[0].Columns.Contains("videoembed"))
        //    {
        //        dswall.Tables[0].Columns.Add("videoembed");
        //    }
        //    if (!dswall.Tables[0].Columns.Contains("movietrailorcode"))
        //    {
        //        dswall.Tables[0].Columns.Add("movietrailorcode");
        //    }
        //    if (!dswall.Tables[0].Columns.Contains("filename"))
        //    {
        //        dswall.Tables[0].Columns.Add("filename");
        //    }
        //    if (!dswall.Tables[0].Columns.Contains("filename"))
        //    {
        //        dswall.Tables[0].Columns.Add("filename");
        //    }
        //    saveXml(dswall, "mywall.xml");
        //}
    }
    //public DataSet saveXml2(DataSet ds, string filename)
    //{
    //    //string fpath = Server.MapPath("~/xml") + "\\" + filename;
    //    //StreamWriter myStreamWriter = new StreamWriter(@"" + fpath);
    //    //ds.WriteXml(myStreamWriter);
    //    //myStreamWriter.Close();
    //    //return ds;
    //}
    public void saveXml(DataSet ds, string filename)
    {
        //string fpath = Server.MapPath("~/xml") + "\\" + filename;
        //StreamWriter myStreamWriter = new StreamWriter(@"" + fpath);
        //ds.WriteXml(myStreamWriter);
        //myStreamWriter.Close();
    }
    //public string GetSafeFileName(string Filename)
    //{
    //    //string newStr = "";
    //    //Filename = Filename.Replace("<", newStr);
    //    //Filename = Filename.Replace(">", newStr);
    //    //Filename = Filename.Replace(" ", newStr);
    //    //Filename = Filename.Replace("%", newStr);
    //    //Filename = Filename.Replace("*", newStr);
    //    //Filename = Filename.Replace("|", newStr);
    //    //Filename = Filename.Replace("-", newStr);
    //    //Filename = Filename.Replace("#", newStr);
    //    //Filename = Filename.Replace("&", newStr);
    //    //Filename = Filename.Replace("@", newStr);
    //    //Filename = Filename.Replace("!", newStr);
    //    //Filename = Filename.Replace("$", newStr);
    //    //Filename = Filename.Replace(" ", newStr);
    //    //return Filename;
    //}
    //public string ReplaceFileName(string str)
    //{
    //    //StringBuilder sb = new StringBuilder();

    //    //for (int i = 0; i < str.Length; i++)
    //    //{
    //    //    if (char.IsLetterOrDigit(str[i]) || char.IsSymbol('.'))
    //    //    {
    //    //        sb.Append(str[i]);
    //    //    }
    //    //}
    //    //return sb.ToString();
    //}
    protected void FCLoginView_ViewChanged(object sender, System.EventArgs e)
    {
        // DisplayProfileProperties();
    }
    public void fillcountry()
    {
        //ddlcountry.Items.Clear();
        //ProfileCommon profile = this.Profile;
        //profile = this.Profile.GetProfile(this.Page.User.Identity.Name);
        //source = new SqlConnection(creativeconfiguration.DbConnectionString);
        //string SqlQuery1 = "select distinct ltrim(rtrim(countryname)) as countryname,countryID from country order by countryname asc";
        //sqladp = new SqlDataAdapter(SqlQuery1, source);
        //DataTable dt = new DataTable();
        //sqladp.Fill(dt);
        //ddlcountry.DataSource = dt;
        //ddlcountry.Items.Clear();
        //ddlcountry.DataTextField = "countryname";
        //ddlcountry.DataValueField = "countryID";
        //ddlcountry.DataBind();
        //ListItem item = new ListItem("--Choose Country--", "0");
        //ddlcountry.Items.Insert(0, item);

        //if (ddlcountry.SelectedItem.Text != "--Choose Country--")
        //{
        //    fillstate(Convert.ToInt32(ddlcountry.SelectedValue));
        //    fillcity(Convert.ToInt32(ddlstate.SelectedValue));
        //}

        //else
        //{

        //    ListItem item1 = new ListItem("--Choose State--", "0");
        //    ddlstate.Items.Insert(0, item1);

        //    ListItem item2 = new ListItem("--Choose City--", "0");
        //    ddlcity1.Items.Insert(0, item2);
        //}

    }
    public void fillstate(int country)
    {
        //DropDownList ddl = new DropDownList();

        //ddl = ddlcountry;
        //source = new SqlConnection(creativeconfiguration.DbConnectionString);
        //string SqlQuery1 = "select ltrim(rtrim(statename)) as statename,stateid from state where countryid=" + country + " order by statename asc";
        //sqladp = new SqlDataAdapter(SqlQuery1, source);
        //DataTable dt = new DataTable();
        //sqladp.Fill(dt);
        //ddlstate.DataSource = dt;
        //ddlstate.Items.Clear();
        //ddlstate.DataTextField = "statename";
        //ddlstate.DataValueField = "stateid";
        //ddlstate.DataBind();
        //ListItem item = new ListItem("--Choose State--", "0");
        //ddlstate.Items.Insert(0, item);

        //if (ddlstate.SelectedItem.Text != "--Choose State--")
        //{           
        //    fillcity(Convert.ToInt32(ddlstate.SelectedValue));
        //}

        //else
        //{

        //    ListItem item2 = new ListItem("--Choose City--", "0");
        //    ddlcity1.Items.Insert(0, item2);
        //}

    }
    public void fillcity(int city)
    {
        //DropDownList ddl = new DropDownList();

        //ddl = ddlstate;

        //source = new SqlConnection(creativeconfiguration.DbConnectionString);
        //string SqlQuery1 = "select ltrim(rtrim(cityname)) as cityname,cityid from city where stateid=" + city + " order by cityname asc";

        //sqladp = new SqlDataAdapter(SqlQuery1, source);
        //DataTable dt = new DataTable();
        //sqladp.Fill(dt);

        //ddlcity1.DataSource = dt;
        //ddlcity1.Items.Clear();
        //ddlcity1.DataTextField = "cityname";
        //ddlcity1.DataValueField = "cityid";

        //DropDownList ddcitylist = new DropDownList();
        //TextBox txtci = new TextBox();
        //int i = 0;

        //ddcitylist = ddlcity1;
        ////ddcitylist.Items.Clear();
        //ListItem lst3 = new ListItem();

        //ddlcity1.DataBind();
        //ListItem item = new ListItem("--Choose City--", "0");
        //ddcitylist.Items.Insert(0, item);

    }
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DropDownList ddslist1 = new DropDownList();
        //ddslist1 = ddlcountry;

        //if (ddslist1.SelectedItem.Value != "--Choose Country--")
        //{
        //    fillstate(Convert.ToInt32(ddlcountry.SelectedItem.Value.ToString()));
        //    fillcity(Convert.ToInt32(ddlstate.SelectedValue));
        //}
    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DropDownList ddslist = new DropDownList();
        //ddslist = ddlstate;
        //if (ddslist.SelectedItem.Value != "--Choose State--")
        //{
        //    fillcity(Convert.ToInt32(ddslist.SelectedItem.Value.ToString()));
        //}
    }
    protected void ddlcity1_SelectedIndexChanged1(object sender, EventArgs e)
    {

        //if (ddlcity1.SelectedValue == "Others")
        //{
        //    pnlothercity.Visible = false;
        //    txtothercity.Text = "";
        //    txtothercity.Visible = false;
        //}
        //else
        //{
        //    txtothercity.Visible = false;
        //}
    }
    protected void ddlprofile_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlprofile.SelectedItem.Value.ToString() == "edit")
        //{
        //    Response.Redirect(ReturnUrl("sitepathmain") + "procs/SampleForm");
        //}
        //else if (ddlprofile.SelectedItem.Value.ToString() == "pwd")
        //{
        //    Response.Redirect(ReturnUrl("sitepathmain") + "procs/changepassword");
        //}
        //else if (ddlprofile.SelectedItem.Value.ToString() == "wishlist")
        //{
        //    Response.Redirect(ReturnUrl("sitepathmain") + "procs/wishlist");
        //}
        //else if (ddlprofile.SelectedItem.Value.ToString() == "preference")
        //{
        //    Response.Redirect(ReturnUrl("sitepathmain") + "procs/preference");
        //}
        //else if (ddlprofile.SelectedItem.Value.ToString() == "subscription")
        //{
        //    Response.Redirect(ReturnUrl("sitepathmain") + "procs/subscriptionhistory");
        //}
        //else if (ddlprofile.SelectedItem.Value.ToString() == "pthistory")
        //{
        //    Response.Redirect(ReturnUrl("sitepathmain") + "procs/pthistory");
        //}
        //else if (ddlprofile.SelectedItem.Value.ToString() == "logout")
        //{
        //    Session.Abandon();
        //    Request.Cookies.Clear();
        //    FormsAuthentication.SignOut();
        //    Response.Redirect(ReturnUrl("sitepathmain") + "default", true);           
        //}

    }
    protected void btnSingOut_Click(object sender, EventArgs e)
    {
        //Session.Abandon();
        //Request.Cookies.Clear();
        //FormsAuthentication.SignOut();
        //Response.Redirect(ReturnUrl("sitepathmain") + "default", true);
    }
    protected void removeprofile_Click(object sender, EventArgs e)
    {
        //bool iserror;
        //try
        //{
        //    iserror=classreviews.insertupdateprofilephoto(Page.User.Identity.Name, "");
        //    if (iserror==false)
        //    {
        //        lblstatus.Text = "Please try again !";               
        //    }
        //    else
        //    {
        //        lblstatus.Visible = true;
        //        filedelete(Request.PhysicalApplicationPath + "themes/creative1.0/images/profile110x110",pimg);
        //        filedelete(Request.PhysicalApplicationPath + "themes/creative1.0/images/profile55x55", pimg);
        //        filedelete(Request.PhysicalApplicationPath + "themes/creative1.0/images/profilephoto", pimg);
        //        lblstatus.Text = "Profile photo removed successfully !";
        //        imgprofile.Visible = false;
        //        removeprofile.Visible = false;
        //    }
        //}
        //catch(Exception ex)
        //{
        //    lblstatus.Text = "Please try again !";
        //}
    }
    protected void removecover_Click(object sender, EventArgs e)
    {
        //bool iserror;
        //try
        //{
        //    iserror = classreviews.insertupdatecoverphoto(Page.User.Identity.Name, "");
        //    if (iserror==false)
        //    {
        //        lblstatus2.Text = "Please try again !";
        //    }
        //    else
        //    {
        //        lblstatus2.Visible = true;
        //        filedelete(Request.PhysicalApplicationPath + "themes/creative1.0/images/coverphoto", cimg);
        //        lblstatus2.Text = "Cover photo removed successfully !";
        //        //imgcover.Visible = false;
        //        //removecover.Visible = false;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    lblstatus2.Text = "Please try again !";
        //}
    }
    protected void profileupload_Click(object sender, EventArgs e)
    {

    }
    protected void lnkcont_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "procs/SampleForm");
    }
    //File delete function
    private void filedelete(string path, string filename)
    {
        //string[] st;
        //st = Directory.GetFiles(path);
        //path += "\\" + filename;
        //int i;
        //if (filename != "noimage1.png" && filename != "noimage3.jpg")
        //{
        //    for (i = 0; i < st.Length; i++)
        //    {
        //        try
        //        {
        //            if (st.GetValue(i).ToString() == path)
        //            {
        //                File.Delete(st.GetValue(i).ToString());
        //            }
        //        }
        //        catch { }
        //    }
        //}
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlsubdept_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddldesg_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txtFromdate.Text = ""; 

        //txtToDate.Text = "";
        //txtLeaveDays.Text = "";


        //if (Convert.ToString(ddlLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_TO"]).Trim())
        //{
        //    txtToDate.Enabled = false;          
        //    ddlToFor.Enabled = false;
        //}
        //else
        //{
        //    txtToDate.Enabled = true;
        //    ddlToFor.Enabled = true;
        //}
    }

    protected void txtLeaveDays_TextChanged(object sender, EventArgs e)
    {
        //int lvdays = Convert.ToInt32(txtLeaveDays.Text);
        //if (Convert.ToString(ddlLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_SL"]).Trim() && (lvdays >= 5))
        //{
        //    uploadprofile.Enabled = false;
        //}
        //else
        //{
        //    uploadprofile.Enabled = true;
        //}

    }
    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        //if (IsPostBack == false)
        //{
        //    DateTime FromDate = Convert.ToDateTime(txtFromdate.Text );
        //    if (FromDate < DateTime.Now)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Information", "alert('Not allowed to apply leave for back dates')", true);
        //        txtFromdate.Text = "";
        //    }
        //}
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        //lpm.Emp_Code = txtEmpCode.Text;
        //; int lvid;
        //double Days = 0;

        //lvid = Convert.ToInt32(ddlLeaveType.SelectedValue);



        ////DateTime FromDate = Convert.ToDateTime(txtFromdate.Text);
        ////DateTime ToDate = Convert.ToDateTime(txtToDate.Text);

        //// Function count no of sat and sun between the Leave Dates
        //int noofweekends = spm.GetWeekends(FromDate, ToDate);

        //// Getting the leave balance based on leave type
        //dtLeaveCalculation = spm.LeaveCalculation(lpm.Emp_Code, lvid);
        //lpm.leave_balance = (double)dtLeaveCalculation.Rows[0]["Balance"];


        //// Gets the date of Holiday based on the selected date
        //dtHolidays = spm.GetHolidayDate(FromDate, ToDate);

        //if (dtHolidays.Rows.Count > 0)
        //{
        //    holidaydate = (DateTime)dtHolidays.Rows[0]["Holiday_Date"];
        //}


        //// Logic to cross check the from date and to date Criteria
        //if (ToDate < FromDate)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", "alert('To Date date should be greate than From Date date')", true);
        //    txtToDate.Text = " ";
        //    txtFromdate.Text = " ";
        //}
        //else if ((FromDate == holidaydate || ToDate == holidaydate))
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", "alert('Holidays cannot be applied on public holiday')", true);
        //    txtToDate.Text = " ";
        //}

        //else
        //{
        //    TimeSpan objTimeSpan = ToDate - FromDate;
        //    Days = Convert.ToDouble(objTimeSpan.TotalDays);
        //    Days = Days + 1;


        //}
        //if ((noofweekends > 0) && (lvid == 1))
        //{
        //    //Days = Days - noofweekends;
        //    txtLeaveDays.Text = Days.ToString();

        //}

        //else if ((noofweekends > 0) && (lvid == 2))
        //{
        //    Days = Days - noofweekends;
        //    txtLeaveDays.Text = Days.ToString();

        //}

        //else
        //{
        //    txtLeaveDays.Text = Days.ToString();
        //}



        //if (Days > lpm.leave_balance)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", "alert('Due to inadequate balance, you can not apply for Leave.')", true);
        //    txtToDate.Text = " ";
        //    txtLeaveDays.Text = " ";

        //}


    }
    protected void ddlFromFor_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click1(object sender, EventArgs e)
    {
        //    try
        //    {
        //        BindLeaveRequestProperties();

        //        MailMessage mail = new MailMessage();
        //        SmtpClient SmtpServer = new SmtpClient();
        //        StringBuilder strbuild = new StringBuilder();


        //        #region fileUpload
        //        if (Convert.ToString(ddlLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_SL"]).Trim() && lpm.LeaveDays > 5)
        //        {
        //            if (uploadprofile.HasFile)
        //            {
        //                filename = Path.Combine(Server.MapPath(""), uploadprofile.FileName);
        //                uploadprofile.SaveAs(filename);

        //            }
        //        }
        //        else
        //        {
        //            filename = "Not Applicable";
        //        }
        //        #endregion

        //        #region LeaveConditionid
        //        if (Convert.ToString(ddlLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_PL"]).Trim() && (lpm.LeaveDays <= 15))
        //        {
        //            leaveconditionid = 2;
        //        }
        //        else if (Convert.ToString(ddlLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_PL"]).Trim() && (lpm.LeaveDays > 15))
        //        {
        //            leaveconditionid = 1;
        //        }
        //        #endregion

        //        #region MaxRequestId

        //        dtMaxRequestId = spm.GetMaxRequestId();
        //        //reqid = (int)dtMaxRequestId.Rows[0]["Request_id"];
        //        //reqid = reqid + 1;

        //        #endregion

        //        #region MethodsCall

        //        spm.InsertLeaveRequest(lpm.Emp_Code, lpm.Leave_Type_id, leaveconditionid, lpm.Leave_FromDate, lpm.Leave_From_for, lpm.Leave_ToDate, lpm.Leave_To_For, lpm.LeaveDays, lpm.Reason, filename);
        //        spm.InsertApproverRequest(lpm.Approvers_code, lpm.appr_id);

        //        dtApproverEmailIds = spm.GetApproverEmailID(lpm.Emp_Code, lpm.Grade, leaveconditionid);
        //        approveremailaddress = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];

        //        spm.send_mailto_RM_Approver(lpm.Emp_Name, lpm.EmailAddress, approveremailaddress, "Leave Request", "Peivillege Leave", lpm.LeaveDays.ToString(), lpm.Reason, lpm.Leave_FromDate.ToShortDateString(), lpm.Leave_From_for, lpm.Leave_ToDate.ToShortDateString(), lpm.Leave_To_For);


        //        //    (string ReqMailID, string toMailIDs, string strsubject, string tType, string tDays, string tRemarks, string tFrom, string tFrom_For, string tTo, string tTo_For, string AppMailID, string Rej_Remarks)
        //        //   spm.sendMail(approveremailaddress, "Leave Request", Convert.ToString(strbuild).Trim(), filename);

        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Information", "alert('Leave Request Submiteed and Email has been send to your Reporting Manager for Approver')", true);

        //        lblmessage.Text = "Leave Request Submiteed and Email has been send to your Reporting Manager for Approver"; 

        //        #endregion

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }


        //}
    }

    private void getSelectedEmpDetails_View()
    {
        try
        {
            DataTable dtEmp = spm.GetEmployeeData(strempcode);
            if (dtEmp.Rows.Count > 0)
            {
                //myTextBox.Text = (string) dt.Rows[0]["name"];
                txtEmpCode.Text = strempcode;
                txtEmpName.Text = Convert.ToString(dtEmp.Rows[0]["Emp_Name"]).Trim();
                txtDesignation.Text = Convert.ToString(dtEmp.Rows[0]["DesginationName"]).Trim();
                txtDepartment.Text = Convert.ToString(dtEmp.Rows[0]["Department_Name"]).Trim();                
                hdnemp_email.Value = Convert.ToString(dtEmp.Rows[0]["Emp_EmailAddress"]).Trim();
                hflGrade.Value = Convert.ToString(dtEmp.Rows[0]["grade"]).Trim();
            }
            else
            {
                Response.Write("Invalid Employee Code");
            }

        }
        catch (Exception ex)
        {

        }
    }

    private void getSelectedEmpAttendanceDetails_View()
    {
        try
        {

            DataSet dsList = new DataSet();
            //  dsList = spm.getAttendanceRequest_MngEdit(strempcode, hdnreqdt.Value);

            //dsList = spm.get_Approvers_Attendance_Arppoval(strempcode, hdnreqid.Value , strempcode_app, hdnapplevel.Value);
            Int32 iapprid_F = 0;
            Int32 iapprid_S = 0;
            Int32 iapprid_T = 0;
            Int32 iapprid_Curnt = 0;

            if (Convert.ToString(hdnApproverid_F.Value).Trim() != "")
                iapprid_F = Convert.ToInt32(hdnApproverid_F.Value);

            if (Convert.ToString(hdnApproverid_S.Value).Trim() != "")
                iapprid_S = Convert.ToInt32(hdnApproverid_S.Value);

            if (Convert.ToString(hdnApproverid_T.Value).Trim() != "")
                iapprid_T = Convert.ToInt32(hdnApproverid_T.Value);

            if (Convert.ToString(hdncrntApp_id.Value).Trim() != "")
                iapprid_Curnt = Convert.ToInt32(hdncrntApp_id.Value);

            dsList = spm.get_Approvers_Attendance_Arppoval(strempcode, hdnreqid.Value, iapprid_F, iapprid_S, iapprid_T, iapprid_Curnt);


            hdnisSelfAppr.Value = "";
            if (dsList.Tables != null)
            {
                if (dsList.Tables[1].Rows.Count > 0)
                {
                    hdnisSelfAppr.Value = Convert.ToString(dsList.Tables[1].Rows[0]["emp_IsSelftAppr"]);
                }

                if (dsList.Tables[0].Rows.Count > 0)
                {
                    txtRequest_Date.Text = Convert.ToString(dsList.Tables[0].Rows[0]["request_date"]).Trim();
                    dgAttendance_AppReject.Visible = true;
                    dgAttendance_AppReject.DataSource = dsList.Tables[0];
                    dgAttendance_AppReject.DataBind();
                }
                else
                {
                  Response.Redirect(ReturnUrl("sitepathmain") + "procs/InboxAttendance");
                }
                 
            }
            else
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "procs/InboxAttendance");
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void getNextApprover()
    { 
         DataTable dsapproverNxt = new DataTable();
         dsapproverNxt = spm.GetNextApproverDetails_Attendance(strempcode, hdnreqid.Value, Convert.ToInt32(hdnleaveconditiontypeid.Value));
            if (dsapproverNxt.Rows.Count > 0)
            {
                hdnnxtapproverid.Value = Convert.ToString(dsapproverNxt.Rows[0]["APPR_ID"]).Trim(); ;
                hdnnxtapproverName.Value = Convert.ToString(dsapproverNxt.Rows[0]["Emp_Name"]).Trim(); ;
                hdnnxtapprovercode.Value = Convert.ToString(dsapproverNxt.Rows[0]["A_EMP_CODE"]).Trim(); ;
                hdnnxtapproverEmail.Value = Convert.ToString(dsapproverNxt.Rows[0]["Emp_Emailaddress"]).Trim();
            }
            else
            {
                hdnstaus.Value = "Final Approver";
                getPreviousApprovesEmailList();
            }

        if (Convert.ToString(hdnisSelfAppr.Value).Trim()=="Y")
        {
            DataSet tds = get_HRApproval_mailsDtls_SelfAppr();
            if (tds.Tables[0].Rows.Count > 0)
            {
                hdnnxtapproverid.Value = Convert.ToString(tds.Tables[0].Rows[0]["APPR_ID"]).Trim(); ;
                hdnnxtapproverName.Value = Convert.ToString(tds.Tables[0].Rows[0]["Emp_Name"]).Trim(); ;
                hdnnxtapprovercode.Value = Convert.ToString(tds.Tables[0].Rows[0]["A_EMP_CODE"]).Trim(); ;
                hdnnxtapproverEmail.Value = Convert.ToString(tds.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
                hdnstaus.Value = "Final Approver";
                getPreviousApprovesEmailList();
            }
        }
    }
    private void getApproverlevels(string tempcode)
    {
        try
        {

            DataSet dsList = new DataSet();
            dsList = spm.getNxtApprover_Codes(tempcode, strempcode_app);

            if (dsList.Tables != null)
            {
                if (dsList.Tables[0].Rows.Count > 0)
                {
                    //hdnnxtapproverid.Value = Convert.ToString(dsList.Tables[0].Rows[0]["APPR_ID"]);
                    //hdnnxtapprovercode.Value = Convert.ToString(dsList.Tables[0].Rows[0]["A_EMP_CODE"]).Trim();
                    //hdnnxtapproverName.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Name"]).Trim();
                    //hdnnxtapproverEmail.Value = Convert.ToString(dsList.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
                }

                if (dsList.Tables[1].Rows.Count > 0)
                {
                    for (Int32 irow = 0; irow < dsList.Tables[1].Rows.Count; irow++)
                    {
                        if (Convert.ToString(dsList.Tables[1].Rows[irow]["A_EMP_CODE"]).Trim() == strempcode_app)
                        {
                            hdnapplevel.Value = Convert.ToString(dsList.Tables[1].Rows[irow]["level_id"]).Trim();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void getPreviousApprovesEmailList()
    {
        DataTable dtPreApp = new DataTable();
        dtPreApp = spm.GetPreviousApproverDetails_Attendance(strempcode, hdnreqid.Value, strempcode_app);
        if (dtPreApp.Rows.Count > 0)
        {

            for (int i = 0; i < dtPreApp.Rows.Count; i++)
            {
                if (Convert.ToString(hdnPreviousApprovermails.Value).Trim() == "")
                {
                    hdnPreviousApprovermails.Value = Convert.ToString(dtPreApp.Rows[i]["Emp_Emailaddress"]).Trim();
                }
                else
                {
                    hdnPreviousApprovermails.Value += ";" + Convert.ToString(dtPreApp.Rows[i]["Emp_Emailaddress"]).Trim();
                }
            }
        }
    }

    private void Update_AttendanceApproval()
    {
        try
        {
            lblmessage.Text = "";
            string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
            if (confirmValue != "Yes")
            {
                return;
            }

            /* not required code for Check multiclick if (Convert.ToString(Session["chkbtnStatus_Appr"]).Trim() != "")
            {
                Response.Redirect("~/procs/InboxAttendance.aspx?apptype=" + hdnappr_type.Value);
            }
            Session["chkbtnStatus_Appr"] = "Approved button Event is Submitted";*/


            String strstatus = "Approved";
            Boolean blnrejected = true;
             
            
            #region check rejected 
            foreach (GridViewRow gvr in dgAttendance_AppReject.Rows)
            {

                DropDownList ddlApprover1 = (DropDownList)gvr.FindControl("ddlApprover1");
                DropDownList ddlApprover2 = (DropDownList)gvr.FindControl("ddlApprover2");
                DropDownList ddlApprover3 = (DropDownList)gvr.FindControl("ddlApprover3");

                CheckBox chkApprover1 = (CheckBox)gvr.FindControl("chkApprover1");
                CheckBox chkApprover2 = (CheckBox)gvr.FindControl("chkApprover2");
                CheckBox chkApprover3 = (CheckBox)gvr.FindControl("chkApprover3");


                                
                TextBox txtReason = (TextBox)gvr.FindControl("txtReason");

                #region Leve wise 
                    if (hdnapplevel.Value == "1")
                    {
                        /*if (Convert.ToString(ddlApprover1.SelectedValue).Trim()=="")
                        {
                            lblmessage.Text = "Kindly set status for Attendance Records to submit.";
                            return;
                        */
                        //if (Convert.ToString(ddlApprover1.SelectedValue).Trim() == "Approve")
                        if (chkApprover1.Checked == true)
                        {
                            blnrejected = false;
                        }
                        //if (Convert.ToString(ddlApprover1.SelectedValue).Trim() == "Reject")
                        if (chkApprover1.Checked == false)
                        {
                            if (Convert.ToString(txtReason.Text).Trim() == "")
                            {
                                //lblmessage.Text = "Please mention the Approver Remarks before rejecting the Attendance";
                                lblmessage.Text = "Please mention the Approver Remarks for rejected Attendance";
                                return;
                            }
                        }
                    }
                    if (hdnapplevel.Value == "2")
                    {
                       /* if (Convert.ToString(ddlApprover2.SelectedValue).Trim() == "")
                        {
                            lblmessage.Text = "Kindly set status for Attendance Records to submit.";
                            return;
                        }*/

                        //if (Convert.ToString(ddlApprover2.SelectedValue).Trim() == "Approve")
                        if (chkApprover2.Checked == true)
                        {
                            blnrejected = false;
                        }
                       // if (Convert.ToString(ddlApprover2.SelectedValue).Trim() == "Reject")
                        if (chkApprover2.Checked == false)
                        {
                            if (Convert.ToString(txtReason.Text).Trim() == "")
                            {
                                //lblmessage.Text = "Please mention the Approver Remark before rejecting the Attendance";
                                lblmessage.Text = "Please mention the Approver Remarks for rejected Attendance";
                                return;
                            }
                        }
                    }
                    if (hdnapplevel.Value == "3")
                    {
                        /*if (Convert.ToString(ddlApprover3.SelectedValue).Trim() == "")
                        {
                            lblmessage.Text = "Please mention the Approver Remark before rejecting the Attendance";
                            return;
                        }*/
                        //if (Convert.ToString(ddlApprover3.SelectedValue).Trim() == "Approve")
                        if (chkApprover3.Checked == true)
                        {
                            blnrejected = false;
                        }
                        //if (Convert.ToString(ddlApprover3.SelectedValue).Trim() == "Reject")
                        if (chkApprover3.Checked == false)
                        {
                            if (Convert.ToString(txtReason.Text).Trim() == "")
                            {
                                //lblmessage.Text = "Please mention the Approver Remark before rejecting the Attendance";
                                lblmessage.Text = "Please mention the Approver Remarks for rejected Attendance";
                                return;
                            }
                        }
                    }
                #endregion

            }
            #endregion


            

            if (blnrejected == true)
                strstatus = "Rejected";

                       

            hdnstaus.Value = "";
            String strApproverList="";
            strApproverList = Convert.ToString(GetApprove_List());
           
            getNextApprover();
             
              Decimal dattid = 0;
              StringBuilder strsql = new StringBuilder();
              if (Convert.ToString(hdnstaus.Value).Trim() == "Final Approver")
              {
                  if (Convert.ToString(hdnisSelfAppr.Value).Trim() != "Y")
                  {
                      if (Check_HR_Attendance_Approval_Status() == false)
                      {
                          get_HRApproval_mailsDtls();
                          hdnstaus.Value = "";
                          hdnnxtapproverEmail.Value = hdnHR_EMailID.Value;
                          hdnnxtapprovercode.Value = hdnHR_ApproverCode.Value;
                          hdnnxtapproverid.Value = hdnHR_Appr_id.Value;
                      }
                  }
                  //spm.InsertAtt_ApproverRequest(Convert.ToString(strempcode_app).Trim(), Convert.ToInt32(hdnHR_Appr_id.Value), Convert.ToDecimal(hdnreqid.Value));
              }
               
                  spm.Update_Attendance_AppRequest(Convert.ToDecimal(hdnreqid.Value), strstatus, "", Convert.ToString((hdnstaus.Value).Trim()), Convert.ToInt32(hdnCurrentID.Value));
              

              #region Update Status
             foreach (GridViewRow gvr in dgAttendance_AppReject.Rows)
             {
                 DropDownList ddlApprover1 = (DropDownList)gvr.FindControl("ddlApprover1");
                 DropDownList ddlApprover2 = (DropDownList)gvr.FindControl("ddlApprover2");
                 DropDownList ddlApprover3 = (DropDownList)gvr.FindControl("ddlApprover3");

                 CheckBox chkApprover1 = (CheckBox)gvr.FindControl("chkApprover1");
                 CheckBox chkApprover2 = (CheckBox)gvr.FindControl("chkApprover2");
                 CheckBox chkApprover3 = (CheckBox)gvr.FindControl("chkApprover3");


                 TextBox txtReason = (TextBox)gvr.FindControl("txtReason");
                 dattid = Convert.ToDecimal(dgAttendance_AppReject.DataKeys[gvr.RowIndex].Values[0]);

                 // strsql.Append(" Update tbl_emp_attendance_approval_status  set approver_status=");
                 strsql.Append(" Update tbl_attendance_req_details  set approver_status=");

                 //For First
                 if (hdnapplevel.Value == "1")
                 {
                     if (chkApprover1.Checked == true)
                         strsql.Append("'" + Convert.ToString("Approve").Trim() + "' ");
                     else
                         strsql.Append("'" + Convert.ToString("Reject").Trim() + "' ");

                     //strsql.Append("'" + Convert.ToString(ddlApprover1.SelectedItem.Text).Trim() + "' ");
                 }


                 if (hdnapplevel.Value == "2")
                 {
                     if (chkApprover2.Checked == true)
                         strsql.Append("'" + Convert.ToString("Approve").Trim() + "' ");
                     else
                         strsql.Append("'" + Convert.ToString("Reject").Trim() + "' ");
                    //strsql.Append("'" + Convert.ToString(ddlApprover2.SelectedItem.Text).Trim() + "' ");
                 }

                 if (hdnapplevel.Value == "3")
                 {
                     if (chkApprover3.Checked == true)
                         strsql.Append("'" + Convert.ToString("Approve").Trim() + "' ");
                     else
                         strsql.Append("'" + Convert.ToString("Reject").Trim() + "' ");

                    //strsql.Append("'" + Convert.ToString(ddlApprover3.SelectedItem.Text).Trim() + "' ");
                 }

                 if (Convert.ToString(txtReason.Text.Trim()) != "")
                     strsql.Append(", approv_dt=getdate() , updated_on=GETDATE(), remarks='" + Convert.ToString(txtReason.Text).Trim() + "'");
                 else
                     strsql.Append(", approv_dt=getdate() , updated_on=GETDATE()");


                 strsql.Append(" where  approver_id ="+ hdncrntApp_id.Value +" and req_id=" + hdnreqid.Value + " and  emp_code='" + strempcode + "' and att_id=" + dattid + " and approver_emp_code='" + strempcode_app + "'; ");
             }
            Int32 irecupdatea = spm.add_Regularise_Attendance_emp(Convert.ToString(strsql).Trim());

            strsql.Clear();
            strsql.Length = 0;

            #endregion

             #region Create Table for Attendance Data
             StringBuilder strbuild = new StringBuilder();
             strbuild.Length = 0;
             strbuild.Clear();

             if (Convert.ToString(hdnisSelfAppr.Value).Trim() == "Y")
             {
                 strbuild.Append("<table border='1'>");
                 strbuild.Append("<tr style='background-color:#C5BE97'>");
                 strbuild.Append("<td>Date</td><td>Type</td><td>Time</td><td>Status</td><td>Category</td><td>Reason/Remarks</td><td>Approver 1</td>");

             }
             else
             {
                 strbuild.Append("<table border='1'>");
                 strbuild.Append("<tr style='background-color:#C5BE97'>");
                 strbuild.Append("<td>Date</td><td>Type</td><td>Time</td><td>Status</td><td>Category</td><td>Reason/Remarks</td><td>Approver 1</td><td>Approver 2</td>");
             }
             if (Convert.ToString(hdnAppr_Cnt.Value).Trim() == "3")
             {
                 strbuild.Append("<td>Approver 3</td>");
             }
             strbuild.Append("<td>Approver Remarks</td>");
             strbuild.Append("</tr>");
             foreach (GridViewRow gvr in dgAttendance_AppReject.Rows)
             {

                 DropDownList ddlApprover1 = (DropDownList)gvr.FindControl("ddlApprover1");
                 DropDownList ddlApprover2 = (DropDownList)gvr.FindControl("ddlApprover2");
                 DropDownList ddlApprover3 = (DropDownList)gvr.FindControl("ddlApprover3");

                 CheckBox chkApprover1 = (CheckBox)gvr.FindControl("chkApprover1");
                 CheckBox chkApprover2 = (CheckBox)gvr.FindControl("chkApprover2");
                 CheckBox chkApprover3 = (CheckBox)gvr.FindControl("chkApprover3");

                 TextBox txtReason = (TextBox)gvr.FindControl("txtReason");

                 if (Convert.ToString(hdnisSelfAppr.Value).Trim() == "Y")
                 {
                     strbuild.Append("<tr><td>" + Convert.ToString(gvr.Cells[0].Text) + "</td><td>" + Convert.ToString(gvr.Cells[1].Text) + "</td><td>" + Convert.ToString(gvr.Cells[2].Text) + "</td><td>" + Convert.ToString(gvr.Cells[3].Text) + "</td><td>" + Convert.ToString(gvr.Cells[4].Text) + "</td><td>" + Convert.ToString(gvr.Cells[5].Text) + "</td>");
                     if (hdnapplevel.Value == "1")
                     {
                         //if (Convert.ToString(ddlApprover1.SelectedItem.Text).Trim() == "Approve")
                         if (chkApprover1.Checked == true)
                             strbuild.Append("<td>" + "Approved" + "</td>");
                         else
                             //if (Convert.ToString(ddlApprover1.SelectedItem.Text).Trim() == "Reject")
                             strbuild.Append("<td>" + "Rejected" + "</td>");
                     }

                 }
                 else
                 {
                     strbuild.Append("<tr><td>" + Convert.ToString(gvr.Cells[0].Text) + "</td><td>" + Convert.ToString(gvr.Cells[1].Text) + "</td><td>" + Convert.ToString(gvr.Cells[2].Text) + "</td><td>" + Convert.ToString(gvr.Cells[3].Text) + "</td><td>" + Convert.ToString(gvr.Cells[4].Text) + "</td><td>" + Convert.ToString(gvr.Cells[5].Text) + "</td>");
                     if (hdnapplevel.Value == "1")
                     {
                         //if (Convert.ToString(ddlApprover1.SelectedItem.Text).Trim() == "Approve")
                         if (chkApprover1.Checked == true)
                             strbuild.Append("<td>" + "Approved" + "</td>");
                         else
                             //if (Convert.ToString(ddlApprover1.SelectedItem.Text).Trim() == "Reject")
                             strbuild.Append("<td>" + "Rejected" + "</td>");

                         strbuild.Append("<td>&nbsp;</td>");

                         if (Convert.ToString(hdnAppr_Cnt.Value).Trim() == "3")
                         {
                             strbuild.Append("<td>&nbsp;</td>");
                         }
                     }
                     if (hdnapplevel.Value == "2")
                     {
                         if (chkApprover1.Checked == true)
                             strbuild.Append("<td>" + "Approved" + "</td>");
                         else
                             //if (Convert.ToString(ddlApprover1.SelectedItem.Text).Trim() == "Reject")
                             strbuild.Append("<td>" + "Rejected" + "</td>");

                         //strbuild.Append("<td>" + Convert.ToString(ddlApprover1.SelectedItem.Text) + "</td>");
                        //if (Convert.ToString(ddlApprover2.SelectedItem.Text).Trim() == "Approve")
                         if (chkApprover2.Checked == true)
                             strbuild.Append("<td>" + "Approved" + "</td>");
                         //if (Convert.ToString(ddlApprover2.SelectedItem.Text).Trim() == "Reject")
                         else
                             strbuild.Append("<td>" + "Rejected" + "</td>");

                         if (Convert.ToString(hdnAppr_Cnt.Value).Trim() == "3")
                         {
                             strbuild.Append("<td>&nbsp;</td>");
                         }
                     }
                     if (hdnapplevel.Value == "3")
                     {
                         if (Convert.ToString(hdnAppr_Cnt.Value).Trim() == "3")
                         {
                             if (chkApprover1.Checked == true)
                                 strbuild.Append("<td>" + "Approved" + "</td>");
                             else                             
                                 strbuild.Append("<td>" + "Rejected" + "</td>");

                             if (chkApprover2.Checked == true)
                                 strbuild.Append("<td>" + "Approved" + "</td>");                             
                             else
                                 strbuild.Append("<td>" + "Rejected" + "</td>");

                             //strbuild.Append("<td>" + Convert.ToString(ddlApprover1.SelectedItem.Text) + "</td><td>" + Convert.ToString(ddlApprover2.SelectedItem.Text) + "</td>");

                            // if (Convert.ToString(ddlApprover3.SelectedItem.Text).Trim() == "Approve")
                             if (chkApprover3.Checked == true)
                                 strbuild.Append("<td>" + "Approved" + "</td>");
                             //if (Convert.ToString(ddlApprover3.SelectedItem.Text).Trim() == "Reject")
                             else
                                 strbuild.Append("<td>" + "Rejected" + "</td>");

                         }
                     }
                 }
                 strbuild.Append("<td>" + Convert.ToString(txtReason.Text.Trim()) + "</td>");
                 strbuild.Append("</tr>");
             }
             strbuild.Append("</table>");
             #endregion

             hdnPreviousApprovermails.Value = hdnPreviousApprovermails.Value; //+ ";" + Convert.ToString(ConfigurationManager.AppSettings["AttMailId_HR"]).Trim();


             /*Comment  if (Convert.ToString(hdnPreviousApprovermails.Value).Trim() != "")
             {
                 //mail to Previous approvers  
                 spm.send_mailto_Attenance_Previous_RM_Approver(txtEmpName.Text, hdnemp_email.Value, "Request for Attendance Regularisation of " + txtEmpName.Text, Convert.ToString(strbuild), hdnPreviousApprovermails.Value, strApproverList);
                 if (Convert.ToString(hdnstaus.Value).Trim() == "Final Approver")
                 {
                     Response.Redirect("~/procs/InboxAttendance.aspx?apptype=" + hdnappr_type.Value);
                 }
             }*/

             //mail to Previous approvers  
             spm.send_mailto_Attenance_Previous_RM_Approver(txtEmpName.Text, hdnemp_email.Value, "Request for Attendance Regularisation of " + txtEmpName.Text, Convert.ToString(strbuild), hdnPreviousApprovermails.Value, strApproverList);
             if (Convert.ToString(hdnstaus.Value).Trim() == "Final Approver")
             {
                 Response.Redirect("~/procs/InboxAttendance.aspx?apptype=" + hdnappr_type.Value);
             }
                
             if (blnrejected == true)
             {
                 //mail to Previous approvers if Reject Attendance
                 //spm.send_mailto_Attenance_Reject_Previous_RM_Approver(hdnCurrentAppName.Value, hdnPreviousApprovermails.Value, "Request for Attendance Regularisation of " + txtEmpName.Text, Convert.ToString(strbuild));
                 spm.send_mailto_Attenance_Reject_Previous_RM_Approver(hdnCurrentAppName.Value, hdnemp_email.Value, "Request for Attendance Regularisation of " + txtEmpName.Text, Convert.ToString(strbuild), hdnPreviousApprovermails.Value, strApproverList);
                 Response.Redirect("~/procs/InboxAttendance.aspx?apptype="+hdnappr_type.Value);
             }

        

             if (Convert.ToString((hdnnxtapproverEmail.Value).Trim()) != "")
             {
                 if (Convert.ToString((hdnstaus.Value).Trim()) != "")
                 {
                     String strAttendRstURL = "";
                     //strAttendRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_AR"]).Trim() + "?ec=" + Convert.ToString(strempcode) + "&reqid=" + hdnReqid.Value + "&appid=" + hdnApproverid.Value + "&app_type=0";


                     //mail to Previous approvers
                     //hdnPreviousApprovermails.Value = hdnPreviousApprovermails.Value + ";" + Convert.ToString(ConfigurationManager.AppSettings["AttMailId_HR"]).Trim();
                     spm.send_mailto_Attenance_RM_Approver(txtEmpName.Text, hdnPreviousApprovermails.Value, "Request for Attendance Regularisation of " + txtEmpName.Text, Convert.ToString(strbuild), strAttendRstURL, strApproverList);
                     Response.Redirect("~/procs/InboxAttendance.aspx?apptype=" + hdnappr_type.Value);
                 }
                 else
                 {
                     #region if not final Approver ---Insert & send mail to next approver

                    spm.InsertAtt_ApproverRequest(Convert.ToString(hdnnxtapprovercode.Value).Trim(), Convert.ToInt32(hdnnxtapproverid.Value), Convert.ToDecimal(hdnreqid.Value));
                   //  spm.InsertAtt_ApproverRequest(Convert.ToString(strempcode).Trim(), Convert.ToInt32(hdnnxtapproverid.Value), Convert.ToDecimal(hdnreqid.Value));
                     
                     //Insert into tbl_emp_attendance_approval_status(att_id,emp_code,approver_id,approver_status,created_on,approver_emp_code)
                     //Values(1,'00630134',3,'Pending',null,GETDATE(),'00630151')
                   
                    
                     StringBuilder sql_insertNxtApprover = new StringBuilder();
                     //sql_insertNxtApprover.Append("Insert into tbl_emp_attendance_approval_status(att_id,emp_code,approver_id,approver_status,created_on,approver_emp_code) Values");
                     sql_insertNxtApprover.Append("Insert into tbl_attendance_req_details(req_id,att_id,emp_code,approver_id,approver_status,created_on,approver_emp_code) Values");

                     StringBuilder sql_ValuesNxtApprover = new StringBuilder();
                     StringBuilder sql_buildValuesNxtA = new StringBuilder();

                     foreach (GridViewRow gvr in dgAttendance_AppReject.Rows)
                     {
                         DropDownList ddlApprover1 = (DropDownList)gvr.FindControl("ddlApprover1");
                         DropDownList ddlApprover2 = (DropDownList)gvr.FindControl("ddlApprover2");                        
                         DropDownList ddlApprover3 = (DropDownList)gvr.FindControl("ddlApprover3");

                         CheckBox chkApprover1 = (CheckBox)gvr.FindControl("chkApprover1");
                         CheckBox chkApprover2 = (CheckBox)gvr.FindControl("chkApprover2");
                         CheckBox chkApprover3 = (CheckBox)gvr.FindControl("chkApprover3");


                         dattid = Convert.ToDecimal(dgAttendance_AppReject.DataKeys[gvr.RowIndex].Values[0]);

                         #region not required
                         //// strsql.Append(" Update tbl_emp_attendance_approval_status  set approver_status=");
                         //strsql.Append(" Update tbl_attendance_req_details  set approver_status=");

                         ////For First
                         //if (hdnapplevel.Value == "1")
                         //    strsql.Append("'" + Convert.ToString(ddlApprover1.SelectedItem.Text).Trim() + "' ");

                         //if (hdnapplevel.Value == "2")
                         //    strsql.Append("'" + Convert.ToString(ddlApprover2.SelectedItem.Text).Trim() + "' ");

                         //if (hdnapplevel.Value == "3")
                         //    strsql.Append("'" + Convert.ToString(ddlApprover3.SelectedItem.Text).Trim() + "' ");

                         //strsql.Append(", approv_dt=getdate() , updated_on=GETDATE()");

                         //strsql.Append(" where req_id=" + hdnreqid.Value + " and  emp_code='" + strempcode + "' and att_id=" + dattid + " and approver_emp_code='" + strempcode_app + "'; ");

                         #endregion

                         if (Convert.ToString(hdnnxtapprovercode.Value).Trim() != "")
                         {
                             sql_ValuesNxtApprover.Clear();
                             sql_ValuesNxtApprover.Length = 0;
                             #region not requird
                             /* sql_ValuesNxtApprover.Append("(" + hdnreqid.Value + "," + dattid + ",'" + strempcode + "'," + hdnnxtapproverid.Value + ",'Pending', GETDATE(),'" + Convert.ToString(hdnnxtapprovercode.Value) + "')");

                             if (Convert.ToString(sql_buildValuesNxtA).Trim() == "")
                                 sql_buildValuesNxtA.Append(sql_ValuesNxtApprover);
                             else
                                 sql_buildValuesNxtA.Append("," + sql_ValuesNxtApprover);
                             */
                             #endregion
                             #region if only for Approved request to next Approver
                             if (hdnapplevel.Value == "1")
                             {
                                 //if (Convert.ToString(ddlApprover1.SelectedValue).Trim() == "Approve")
                                 if (chkApprover1.Checked == true)
                                 {
                                     sql_ValuesNxtApprover.Append("(" + hdnreqid.Value + "," + dattid + ",'" + strempcode + "'," + hdnnxtapproverid.Value + ",'Pending', GETDATE(),'" + Convert.ToString(hdnnxtapprovercode.Value) + "')");

                                     if (Convert.ToString(sql_buildValuesNxtA).Trim() == "")
                                         sql_buildValuesNxtA.Append(sql_ValuesNxtApprover);
                                     else
                                         sql_buildValuesNxtA.Append("," + sql_ValuesNxtApprover);
                                 }
                             }
                             if (hdnapplevel.Value == "2")
                             {
                                 //if (Convert.ToString(ddlApprover2.SelectedValue).Trim() == "Approve")
                                 if (chkApprover2.Checked == true)
                                 {
                                     sql_ValuesNxtApprover.Append("(" + hdnreqid.Value + "," + dattid + ",'" + strempcode + "'," + hdnnxtapproverid.Value + ",'Pending', GETDATE(),'" + Convert.ToString(hdnnxtapprovercode.Value) + "')");

                                     if (Convert.ToString(sql_buildValuesNxtA).Trim() == "")
                                         sql_buildValuesNxtA.Append(sql_ValuesNxtApprover);
                                     else
                                         sql_buildValuesNxtA.Append("," + sql_ValuesNxtApprover);
                                 }
                             }
                             if (hdnapplevel.Value == "3")
                             {
                                // if (Convert.ToString(ddlApprover3.SelectedValue).Trim() == "Approve")
                                 if (chkApprover3.Checked == true)
                                 {
                                     sql_ValuesNxtApprover.Append("(" + hdnreqid.Value + ","  + dattid + ",'" + strempcode + "'," + hdnnxtapproverid.Value + ",'Pending', GETDATE(),'" + Convert.ToString(hdnnxtapprovercode.Value) + "')");

                                     if (Convert.ToString(sql_buildValuesNxtA).Trim() == "")
                                         sql_buildValuesNxtA.Append(sql_ValuesNxtApprover);
                                     else
                                         sql_buildValuesNxtA.Append("," + sql_ValuesNxtApprover);
                                 }
                             }
                             #endregion

                         }
                     }

                     Int32 irecupdate = 0;
                     //if (Convert.ToString(strsql).Trim() != "")
                     //{
                         if (Convert.ToString(hdnnxtapprovercode.Value).Trim() != "")
                             irecupdate = spm.add_Regularise_Attendance_emp(Convert.ToString( sql_insertNxtApprover) + Convert.ToString( sql_buildValuesNxtA));
                         //else
                         //    irecupdate = spm.add_Regularise_Attendance_emp(Convert.ToString(strsql).Trim());

                   //  }
                     if (irecupdate != 0)
                     {
                         lblmsg.Text = "Attedance Regularization updated successfully.";
                         //Send Mail to Approver.
                         String strAttendRstURL = "";

                         if (Convert.ToInt32(hdnnxtapproverid.Value)>99)
                             strAttendRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_AR"]).Trim() + "?ec=" + Convert.ToString(txtEmpCode.Text) + "&reqid=" + hdnreqid.Value + "&appid=" + hdnnxtapproverid.Value + "&app_type=1";
                         else
                         strAttendRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_AR"]).Trim() + "?ec=" + Convert.ToString(txtEmpCode.Text) + "&reqid=" + hdnreqid.Value + "&appid=" + hdnnxtapproverid.Value + "&app_type=0";
                         spm.send_mailto_Attenance_RM_Approver(txtEmpName.Text, hdnnxtapproverEmail.Value, "Request for Attendance Regularisation of " + txtEmpName.Text, Convert.ToString(strbuild), strAttendRstURL, strApproverList);
                         Response.Redirect("~/procs/InboxAttendance.aspx?apptype=" + hdnappr_type.Value);
                     }
                     #endregion
                 }
             }
             else
             {
                       

                 
             }

        }
        catch (Exception ex)
        {
        }
    }

    private void get_HRApproval_mailsDtls()
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_HR_emails_dtls_Attendnce_approval";

            spars[1] = new SqlParameter("@approver_type", SqlDbType.VarChar);
            spars[1].Value = "HRLWP";

            spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[2].Value = strempcode;

            
            DataSet dsList = new DataSet();
            dsList = spm.getDatasetList(spars, "Usp_getEmployee_Details_All");
            
            if (dsList.Tables != null)
            {
                if (dsList.Tables[0].Rows.Count > 0)
                {
                    hdnHR_ApproverCode.Value = Convert.ToString(dsList.Tables[0].Rows[0]["approver_emp_code"]).Trim();
                    hdnHR_EMailID.Value = Convert.ToString(dsList.Tables[0].Rows[0]["approver_emp_mail"]).Trim();
                    hdnHR_Appr_id.Value = Convert.ToString(dsList.Tables[0].Rows[0]["app_id"]).Trim();
                    hdnHR_Appr_Name.Value = Convert.ToString(dsList.Tables[0].Rows[0]["app_remarks"]).Trim();
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
    }


    private Boolean Check_HR_Attendance_Approval_Status()
    {
        Boolean blnCheckHR = false;
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Check_HR_Attendance_ForApproval";

            spars[1] = new SqlParameter("@appr_id_Curnt", SqlDbType.Int);
            if (Convert.ToString(hdncrntApp_id.Value).Trim() != "")
                spars[1].Value = Convert.ToInt32(hdncrntApp_id.Value);
            else
                spars[1].Value = 0;

            spars[2] = new SqlParameter("@req_id", SqlDbType.Decimal);
            if (Convert.ToString(hdnreqid.Value).Trim() != "")
                spars[2].Value = Convert.ToDecimal(hdnreqid.Value);
            else
                spars[2].Value = 0;

          //  DataSet dsTrDetails = new DataSet();
            dsTrDetails = spm.getDatasetList(spars, "Usp_get_Approver_EmpAttendance_list");

           // if (dsList.Tables != null)
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                if (dsTrDetails.Tables[0].Rows.Count > 0)
                {
                    blnCheckHR = true; 
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

        }
         return blnCheckHR;
    }

    private void InsertCategory_AppRej(DropDownList ddlcategory)
    {
        ddlcategory.Items.Clear();
        ddlcategory.Items.Add(new ListItem("", ""));
        ddlcategory.Items.Add(new ListItem("Approve", "Approve"));
        ddlcategory.Items.Add(new ListItem("Reject", "Reject"));
    }

    private void InsertCategory_Approved_Rejected(DropDownList ddlcategory)
    {
        ddlcategory.Items.Clear();
        ddlcategory.Items.Add(new ListItem("", ""));
        ddlcategory.Items.Add(new ListItem("Approved", "Approved"));
        ddlcategory.Items.Add(new ListItem("Rejected", "Rejected"));
    }

    protected void dgAttendance_AppReject_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chkApprover1_All = (CheckBox)e.Row.FindControl("chkApprover1_All");
            CheckBox chkApprover2_All = (CheckBox)e.Row.FindControl("chkApprover2_All");
            CheckBox chkApprover3_All = (CheckBox)e.Row.FindControl("chkApprover3_All");
            if (Convert.ToString(hdnapplevel.Value).Trim() == "1")
            {
                chkApprover2_All.Visible = false;
                chkApprover3_All.Visible = false;
            }
            if (Convert.ToString(hdnapplevel.Value).Trim() == "2")
            {
                chkApprover1_All.Visible = false;
                chkApprover3_All.Visible = false;
            }

            if (Convert.ToString(hdnapplevel.Value).Trim() == "3")
            {
                chkApprover1_All.Visible = false;
                chkApprover2_All.Visible = false;
            }


        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToString(hdnapplevel.Value).Trim() == "1")
            {
               // DropDownList ddlApprover1 = (DropDownList)e.Row.FindControl("ddlApprover1");
                CheckBox chkApprover1 = (CheckBox)e.Row.FindControl("chkApprover1");

                CheckBox chkApprover2 = (CheckBox)e.Row.FindControl("chkApprover2");
                CheckBox chkApprover3 = (CheckBox)e.Row.FindControl("chkApprover3");

               // InsertCategory_AppRej(ddlApprover1);
                TextBox txtapproverstatus1 = (TextBox)e.Row.FindControl("txtapproverstatus1");
                if (Convert.ToString(txtapproverstatus1.Text).Trim() != "")
                {
                    //ddlApprover1.SelectedValue = Convert.ToString(txtapproverstatus1.Text).Trim();
                    if (Convert.ToString(txtapproverstatus1.Text).Trim() == "Approved")
                    chkApprover1.Checked = true;
                }

                chkApprover2.Enabled = false;
                chkApprover3.Enabled = false;
                 //chkApprover2.Visible = false;
                // chkApprover3.Visible = false;
            }
            if (Convert.ToString(hdnapplevel.Value).Trim() == "2")
            {

                //DropDownList ddlApprover1 = (DropDownList)e.Row.FindControl("ddlApprover1");
                CheckBox chkApprover1 = (CheckBox)e.Row.FindControl("chkApprover1");
                

                //InsertCategory_Approved_Rejected(ddlApprover1);

                TextBox txtapproverstatus1 = (TextBox)e.Row.FindControl("txtapproverstatus1");
                if (Convert.ToString(txtapproverstatus1.Text).Trim() == "Approved")
                {
                    //ddlApprover1.SelectedValue = Convert.ToString(txtapproverstatus1.Text).Trim();
                    chkApprover1.Checked = true;
                }
                else
                { 
                    chkApprover1.Checked = false; 
                }

                //ddlApprover1.Enabled = false;
                chkApprover1.Enabled= false;
                //chkApprover1.Visible= false;

                //DropDownList ddlApprover2 = (DropDownList)e.Row.FindControl("ddlApprover2");
                CheckBox chkApprover2 = (CheckBox)e.Row.FindControl("chkApprover2");
                //InsertCategory_AppRej(ddlApprover2);

                TextBox txtapproverstatus2 = (TextBox)e.Row.FindControl("txtapproverstatus2");
                if (Convert.ToString(txtapproverstatus2.Text).Trim() == "Approved")
                {
                    
                    chkApprover2.Checked = true;
                }
                else
                {
                    chkApprover2.Checked = false;
                }
            }
            if (Convert.ToString(hdnapplevel.Value).Trim() == "3")
            {
                CheckBox chkApprover1 = (CheckBox)e.Row.FindControl("chkApprover1");


                CheckBox chkApprover2 = (CheckBox)e.Row.FindControl("chkApprover2");

                TextBox txtapproverstatus1 = (TextBox)e.Row.FindControl("txtapproverstatus1");
                if (Convert.ToString(txtapproverstatus1.Text).Trim() == "Approved")
                {  
                    chkApprover1.Checked = true;
                }
                

                TextBox txtapproverstatus2 = (TextBox)e.Row.FindControl("txtapproverstatus2");
                if (Convert.ToString(txtapproverstatus2.Text).Trim() != "")
                {
                   
                    if (Convert.ToString(txtapproverstatus2.Text).Trim() == "Approved")
                        chkApprover2.Checked = true;
                }
                 

                chkApprover1.Enabled = false;
                chkApprover2.Enabled = false;
                

                CheckBox chkApprover3 = (CheckBox)e.Row.FindControl("chkApprover3");

                TextBox txtapproverstatus3 = (TextBox)e.Row.FindControl("txtapproverstatus3");
                if (Convert.ToString(txtapproverstatus3.Text).Trim() != "")
                {   
                    if (Convert.ToString(txtapproverstatus3.Text).Trim() == "Approve")
                    chkApprover3.Checked = true;
                }
            }

        }
    }

    protected string GetApprove_List()
    {
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        
        if (lstApprover.Items.Count>0)
        {
            sbapp.Append("<table style='width:300px'>");
            for (int i = 0; i < lstApprover.Items.Count; i++)
            {
                sbapp.Append("<tr>");
                //sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
                sbapp.Append("<td>" + Convert.ToString(lstApprover.Items[i].Text) + "</td>");
                sbapp.Append("</tr>");
            }
            sbapp.Append("</table>");
        }

        return Convert.ToString(sbapp);
    }
    protected void get_Appr_id_forApproval()
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "gte_Approval_Attendance_apprid";

            spars[1] = new SqlParameter("@req_id", SqlDbType.Decimal);
            spars[1].Value = hdnreqid.Value;


            dsTrDetails = spm.getDatasetList(spars, "Usp_get_Approver_EmpAttendance_list");
            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                 
                for(Int32 irow=0; irow < dsTrDetails.Tables[0].Rows.Count;irow++)
                {
                    if (irow == 0)
                    {
                        hdnapplevel.Value = "1";
                        hdnApproverid_F.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[irow]["APPR_ID"]).Trim();
                    }

                    if (irow == 1)
                    {
                        hdnapplevel.Value = "2";
                        hdnApproverid_S.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[irow]["APPR_ID"]).Trim();
                    }

                    if (irow == 2)
                    {
                        hdnapplevel.Value = "3";
                        hdnApproverid_T.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[irow]["APPR_ID"]).Trim();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    protected void att_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/procs/InboxAttendance.aspx?apptype=" + hdnappr_type.Value);

    }
    protected void dgAttendance_AppReject_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            if (Convert.ToString(hdnAppr_Cnt.Value).Trim() == "2")
            {
                e.Row.Cells[8].Visible = false;
            }

            if (Convert.ToString(hdnisSelfAppr.Value).Trim() == "Y")
            {
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
            }

        }
    }

    protected void chkApprover1_All_CheckedChanged(object sender, EventArgs e)
    {

        CheckBox chkApprover1_All = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chkApprover1_All.NamingContainer;

        if (Convert.ToString(hdnapplevel.Value).Trim() == "1")
        {
            if (lstApprover.Items.Count == 1)
            {
                dgAttendance_AppReject.Columns[7].Visible = false;
                dgAttendance_AppReject.Columns[8].Visible = false;
            }
            if (lstApprover.Items.Count == 2)
            {
                dgAttendance_AppReject.Columns[8].Visible = false;
            }
        }
        if (chkApprover1_All.Checked == true)
        {
            foreach (GridViewRow gvr in dgAttendance_AppReject.Rows)
            {
                CheckBox chkApprover1 = (CheckBox)gvr.FindControl("chkApprover1");
                CheckBox chkApprover2 = (CheckBox)gvr.FindControl("chkApprover2");
                CheckBox chkApprover3 = (CheckBox)gvr.FindControl("chkApprover3");

                chkApprover1.Checked = true;
                chkApprover2.Enabled= false;
                chkApprover3.Enabled = false;
            }
        }
        else
        {
            foreach (GridViewRow gvr in dgAttendance_AppReject.Rows)
            {
                CheckBox chkApprover1 = (CheckBox)gvr.FindControl("chkApprover1");
                chkApprover1.Checked = false;
            }
        }
    }
    protected void chkApprover2_All_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkApprover2_All = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chkApprover2_All.NamingContainer;

        if (lstApprover.Items.Count == 1)
        {
            dgAttendance_AppReject.Columns[7].Visible = false;
            dgAttendance_AppReject.Columns[8].Visible = false;
        }
        if (lstApprover.Items.Count == 2)
        {
            dgAttendance_AppReject.Columns[8].Visible = false;
        }

        if (chkApprover2_All.Checked == true)
        {
            foreach (GridViewRow gvr in dgAttendance_AppReject.Rows)
            {
                CheckBox chkApprover2 = (CheckBox)gvr.FindControl("chkApprover2");
                 CheckBox chkApprover3 = (CheckBox)gvr.FindControl("chkApprover3");
                chkApprover2.Checked = true;
                chkApprover3.Enabled = false;
            }
        }
        else
        {
            foreach (GridViewRow gvr in dgAttendance_AppReject.Rows)
            {
                CheckBox chkApprover2 = (CheckBox)gvr.FindControl("chkApprover2");
                chkApprover2.Checked = false;
            }
        }
    }


    protected void chkApprover3_All_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkApprover3_All = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chkApprover3_All.NamingContainer;

        if (chkApprover3_All.Checked == true)
        {
            foreach (GridViewRow gvr in dgAttendance_AppReject.Rows)
            {
                CheckBox chkApprover3 = (CheckBox)gvr.FindControl("chkApprover3");
                chkApprover3.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow gvr in dgAttendance_AppReject.Rows)
            {
                CheckBox chkApprover3 = (CheckBox)gvr.FindControl("chkApprover3");
                chkApprover3.Checked = false;
            }
        }
    }
}
