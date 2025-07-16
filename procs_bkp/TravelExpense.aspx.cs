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



public partial class TravelExpense : System.Web.UI.Page
{
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc="", dept="", subdept="", desg = "";
    public int did = 0;
    LeaveBalance bl = new LeaveBalance();
    SP_Methods spm = new SP_Methods();
    Leave_Request_Parameters lpm = new Leave_Request_Parameters();
    public DataTable dtEmp, dtleavebal, dtApprover, dtLeaveTypes, dtApproverEmailIds, dtMaxRequestId, dtIntermediate, dtLeaveCalculation, dtHolidays;
    public int Leaveid;
    public int leavetype, openbal, avail, rembal, leaveconditionid;
    public string filename = "", approveremailaddress;
    DateTime holidaydate = new DateTime();

    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url;}
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
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/travelindex");
            }
            else
            {
                Page.SmartNavigation = true;
                if (!Page.IsPostBack)
                {
                    editform.Visible = true;
                    divbtn.Visible = false;
                    divmsg.Visible = false;
                    txtEmpCode.Text = "This is before function";
                    PopulateEmployeeData();
                    //txtReason.Text = "This is after function";
               
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

    public void PopulateEmployeeData()
    {
        try
        {
            string dgheader1 = "APPR_ID";
            string dgheader2 = "A_EMP_CODE";
           // lpm.Emp_Code = "00630134";
            txtEmpCode.Text = lpm.Emp_Code;

            //    txtEmpCode.Enabled = false; 

            //  BindControls();
            dtEmp = spm.GetEmployeeData(lpm.Emp_Code);
            //txtReason.Text = "This is after function";
            if (dtEmp.Rows.Count > 0)
            {
                txtReason.Text = "This is after function";
                //myTextBox.Text = (string) dt.Rows[0]["name"];
                lpm.Emp_Status = (string)dtEmp.Rows[0]["Emp_status"];
                if (lpm.Emp_Status == "Resgined")
                {
                    txtReason.Text = "This is Resigned function";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Information", "alert('You are not allowed to apply for any type of leave sicne your employee status is in resignation')", true);
                    //Response.Write("You are not allowed to apply for any type of leave sicne your employee status is in resignation");
                }
                else
                {
                    txtReason.Text = "This is value block function";
                    lpm.Emp_Name = (string)dtEmp.Rows[0]["Emp_Name"];
                    lpm.Designation_name = (string)dtEmp.Rows[0]["DesginationName"];
                    lpm.department_name = (string)dtEmp.Rows[0]["Department_Name"];
                    lpm.Grade = (string)dtEmp.Rows[0]["Grade"];
                    lpm.EmailAddress = (string)dtEmp.Rows[0]["Emp_EmailAddress"];

                    //txtEmpName.Text = lpm.Emp_Name;
                    //txtDepartment.Text = lpm.department_name;
                    //txtDesig.Text = lpm.Designation_name ;
                    hflGrade.Value = lpm.Grade;
                    hflEmailAddress.Value = lpm.EmailAddress;
                    hflEmpName.Value = lpm.Emp_Name;
                    hflEmpDepartment.Value = lpm.department_name;
                    hflEmpDesignation.Value = lpm.Designation_name;

                  
                    dtleavebal = spm.GetLeaveBalance(lpm.Emp_Code);
                    dgLeaveBalance.DataSource = dtleavebal;
                    dgLeaveBalance.DataBind();
                    GridView1.DataSource = dtleavebal;
                    GridView1.DataBind();
                    GridView2.DataSource = dtleavebal;
                    GridView2.DataBind();
                    GridView3.DataSource = dtleavebal;
                    GridView3.DataBind();


                    txtReason.Text = dtleavebal.Rows.Count.ToString() + " This is gridview function";

                    dtApprover = spm.GetApproverName(lpm.Emp_Code);

                    if (dtApprover.Rows.Count > 0)
                    {
                      
                        lstApprover.DataSource = dtApprover;
                        lstApprover.DataTextField = "Emp_Name";
                        lstApprover.DataValueField = "APPR_ID";
                        lstApprover.DataBind();

                        lpm.Approvers_code = (string)dtApprover.Rows[0]["A_EMP_CODE"];
                        hflapprcode.Value = lpm.Approvers_code;
                        txtReason.Text = dtApprover.Rows.Count.ToString() + " This is dtApprover function";
                    }

                    dtIntermediate = spm.GetIntermediateName(lpm.Emp_Code,0,"");
                    if (dtIntermediate.Rows.Count > 0)
                    {
                        lstIntermediate.DataSource = dtIntermediate;
                        lstIntermediate.DataTextField = "Emp_Name";
                        lstIntermediate.DataValueField = "APPR_ID";
                        lstIntermediate.DataBind();
                    }

                   // dtLeaveTypes = spm.GetLeaveType();
                   //ddlTripType.DataSource = dtLeaveTypes;
                   //ddlTripType.DataTextField = "Leave_Type_Description";
                   //ddlTripType.DataValueField = "Leavetype_id";
                   //ddlTripType.DataBind();
                   

                 


                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();
            txtReason.Text = "This is Catch function";
            throw;
        }
        //BindLeaveRequestProperties();
        //lpm.Emp_Code = txtEmpCode.Text;

    
    }

    public void BindLeaveRequestProperties()
    {
        lpm.Emp_Code = txtEmpCode.Text;
       
      //  lpm.Leave_Type_id = Convert.ToInt32( ddlLeaveType.SelectedValue);
     //   lpm.Leave_FromDate = Convert.ToDateTime(txtFromdate.Text);
       
     //   lpm.Leave_ToDate = Convert.ToDateTime(txtToDate.Text);
     
        lpm.Reason = txtReason.Text;
        lpm.Grade = hflGrade.Value.ToString();
        lpm.Approvers_code = hflapprcode.Value;
        lpm.appr_id = Convert.ToInt32(lstApprover.SelectedValue);
        lpm.EmailAddress = hflEmailAddress.Value;
        lpm.Emp_Name = hflEmpName.Value;

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
                if( ds_userdetails.Tables[0].Rows.Count > 0)
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
    protected void ddlTripType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFromdate.Text = ""; 
    
        txtToDate.Text = "";
      


        
    }
  
    protected void txtLeaveDays_TextChanged(object sender, EventArgs e)
    {
        
            
    }
    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            DateTime FromDate = Convert.ToDateTime(txtFromdate.Text );
            if (FromDate < DateTime.Now)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Information", "alert('Not allowed to apply leave for back dates')", true);
                txtFromdate.Text = "";
            }
        }
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        lpm.Emp_Code = txtEmpCode.Text;
        ; int lvid;
        double Days = 0;

     //   lvid = Convert.ToInt32(ddlLeaveType.SelectedValue);



        DateTime FromDate = Convert.ToDateTime(txtFromdate.Text);
        DateTime ToDate = Convert.ToDateTime(txtToDate.Text);

        // Function count no of sat and sun between the Leave Dates
        int noofweekends = spm.GetWeekends(FromDate, ToDate);

        // Getting the leave balance based on leave type
     //   dtLeaveCalculation = spm.LeaveCalculation(lpm.Emp_Code, lvid);
        lpm.leave_balance = (double)dtLeaveCalculation.Rows[0]["Balance"];


        // Gets the date of Holiday based on the selected date
        //dtHolidays = spm.GetHolidayDate(FromDate, ToDate);

        //if (dtHolidays.Rows.Count > 0)
        //{
        //    holidaydate = (DateTime)dtHolidays.Rows[0]["Holiday_Date"];
        //}


        // Logic to cross check the from date and to date Criteria
        if (ToDate < FromDate)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", "alert('To Date date should be greate than From Date date')", true);
            txtToDate.Text = " ";
            txtFromdate.Text = " ";
        }
        //else if ((FromDate == holidaydate || ToDate == holidaydate))
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", "alert('Holidays cannot be applied on public holiday')", true);
        //    txtToDate.Text = " ";
        //}

        else
        {
            TimeSpan objTimeSpan = ToDate - FromDate;
            Days = Convert.ToDouble(objTimeSpan.TotalDays);
            Days = Days + 1;


        }
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



        if (Days > lpm.leave_balance)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", "alert('Due to inadequate balance, you can not apply for Leave.')", true);
            txtToDate.Text = " ";
          //  txtLeaveDays.Text = " ";

        }
            
       
    }
    protected void ddlFromFor_SelectedIndexChanged(object sender, EventArgs e)
    {
       

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click1(object sender, EventArgs e)
    {
        try
        {
            BindLeaveRequestProperties();

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient();
            StringBuilder strbuild = new StringBuilder();


            #region fileUpload
            //if (Convert.ToString(ddlLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_SL"]).Trim() && lpm.LeaveDays > 5)
            //{
            //    if (uploadprofile.HasFile)
            //    {
            //        filename = Path.Combine(Server.MapPath(""), uploadprofile.FileName);
            //        uploadprofile.SaveAs(filename);

            //    }
            //}
            //else
            //{
            //    filename = "Not Applicable";
            //}
            #endregion

            #region LeaveConditionid
            //if (Convert.ToString(ddlLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_PL"]).Trim() && (lpm.LeaveDays <= 15))
            //{
            //    leaveconditionid = 2;
            //}
            //else if (Convert.ToString(ddlLeaveType.SelectedValue).Trim() == Convert.ToString(ConfigurationManager.AppSettings["leave_typeid_PL"]).Trim() && (lpm.LeaveDays > 15))
            //{
            //    leaveconditionid = 1;
            //}
            #endregion

            #region MaxRequestId

            dtMaxRequestId = spm.GetMaxRequestId();
            //reqid = (int)dtMaxRequestId.Rows[0]["Request_id"];
            //reqid = reqid + 1;

            #endregion

            #region MethodsCall

           // spm.InsertLeaveRequest(lpm.Emp_Code, lpm.Leave_Type_id, leaveconditionid, lpm.Leave_FromDate, lpm.Leave_From_for, lpm.Leave_ToDate, lpm.Leave_To_For, lpm.LeaveDays, lpm.Reason, filename);
            //spm.InsertApproverRequest(lpm.Approvers_code, lpm.appr_id);

            dtApproverEmailIds = spm.GetApproverEmailID(lpm.Emp_Code, lpm.Grade, leaveconditionid,"",0);
            approveremailaddress = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];

           // spm.send_mailto_RM_Approver(lpm.Emp_Name, lpm.EmailAddress, approveremailaddress, "Leave Request", "Peivillege Leave", lpm.LeaveDays.ToString(), lpm.Reason, lpm.Leave_FromDate.ToShortDateString(), lpm.Leave_From_for, lpm.Leave_ToDate.ToShortDateString(), lpm.Leave_To_For);


            //    (string ReqMailID, string toMailIDs, string strsubject, string tType, string tDays, string tRemarks, string tFrom, string tFrom_For, string tTo, string tTo_For, string AppMailID, string Rej_Remarks)
            //   spm.sendMail(approveremailaddress, "Leave Request", Convert.ToString(strbuild).Trim(), filename);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Information", "alert('Leave Request Submiteed and Email has been send to your Reporting Manager for Approver')", true);

            lblmessage.Text = "Leave Request Submiteed and Email has been send to your Reporting Manager for Approver"; 

            #endregion

        }
        catch (Exception)
        {

            throw;
        }
    
      
    }
    protected void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTriptype.Text = ListBox2.SelectedValue;
        PopupControlExtender2.Commit(ListBox2.SelectedValue);
    }
}
