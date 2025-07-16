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
using System.Web.Mail;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
public partial class myaccount_SampleForm7 : System.Web.UI.Page
{
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

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            //txtemail.ReadOnly.Equals(true);
            //txtemail.Enabled = false;
            //txtfirstname.ReadOnly.Equals(true);
            //txtlastname.ReadOnly.Equals(true);
            //txtfirstname.Enabled = false;
            //txtlastname.Enabled = false;
            //new code starts by krishna add readonly
            //txtdept.ReadOnly.Equals(true);
            //txtdesg.ReadOnly.Equals(true);
            //new code ends by krishna add readonly

            lblmsg.Visible = false; ;
            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/SampleForm");
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
                    //txtemail.Text = ds_userdetails.Tables[0].Rows[0]["username"].ToString();
                    //txtemailadress.Text = ds_userdetails.Tables[0].Rows[0]["emailid"].ToString();
                    //txtfirstname.Text = ds_userdetails.Tables[0].Rows[0]["firstname"].ToString();
                    //txtlastname.Text = ds_userdetails.Tables[0].Rows[0]["lastname"].ToString();
                    //txtaddress1.Text = ds_userdetails.Tables[0].Rows[0]["address"].ToString();
                    //txtpincode.Text = ds_userdetails.Tables[0].Rows[0]["pincode"].ToString();
                    //txtphone.Text = ds_userdetails.Tables[0].Rows[0]["telno"].ToString();
                    //txtmobile.Text = ds_userdetails.Tables[0].Rows[0]["mobileno"].ToString();
                    //txttempaddress.Text = ds_userdetails.Tables[0].Rows[0]["tempaddress"].ToString();
                    //txtaltemail.Text = ds_userdetails.Tables[0].Rows[0]["alternateemail"].ToString();
                    //txtextension.Text = ds_userdetails.Tables[0].Rows[0]["extentionno"].ToString();
                    //txtoffno.Text = ds_userdetails.Tables[0].Rows[0]["officemob"].ToString();
                    //txtaltno.Text = ds_userdetails.Tables[0].Rows[0]["alternatemob"].ToString();
                    //txtoffphone.Text = ds_userdetails.Tables[0].Rows[0]["officephone"].ToString();
                    //txtfaxno.Text = ds_userdetails.Tables[0].Rows[0]["faxno"].ToString();
                    txtloc.Text = ds_userdetails.Tables[0].Rows[0]["location"].ToString();
                    //txtdept.Text = ds_userdetails.Tables[0].Rows[0]["department"].ToString();
                    txtsubdept.Text = ds_userdetails.Tables[0].Rows[0]["sub_department"].ToString();
                    //txtdesg.Text = ds_userdetails.Tables[0].Rows[0]["designation"].ToString();

                    DateTime dob1 = new DateTime();

                    if (ds_userdetails.Tables[0].Rows[0]["DOB1"].ToString().Length > 0 && ds_userdetails.Tables[0].Rows[0]["DOB1"].ToString() != null && ds_userdetails.Tables[0].Rows[0]["DOB1"].ToString() != "")
                    {
                        dob1 = Convert.ToDateTime(ds_userdetails.Tables[0].Rows[0]["DOB1"]);
                        if (dob1.ToString("dd/MMM/yyyy") == "01/Jan/1900")
                        {
                            //txtdob1.Text = "";
                        }
                        else
                        {
                            //txtdob1.Text = dob1.ToString("dd/MMM/yyyy");
                        }
                    }
                    else
                    {
                        //txtdob1.Text = "";
                    }


                    DateTime dob = new DateTime();

                    if (ds_userdetails.Tables[0].Rows[0]["DOB"].ToString().Length > 0 && ds_userdetails.Tables[0].Rows[0]["DOB"].ToString() != null && ds_userdetails.Tables[0].Rows[0]["DOB"].ToString() != "")
                    {
                        dob = Convert.ToDateTime(ds_userdetails.Tables[0].Rows[0]["DOB"]);
                        if (dob.ToString("dd/MMM/yyyy") == "01/Jan/1900")
                        {
                            //txtdob.Text = "";
                        }
                        else
                        {
                            //txtdob.Text = dob.ToString("dd/MMM/yyyy");
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


                    //DataTable user2 = classreviews.getuseridbyemail(Page.User.Identity.Name);

                    //if (user2.Rows.Count > 0)
                    //{
                    //    userid = user2.Rows[0]["indexid"].ToString();
                    //    if (user2.Rows[0]["profilephoto"].ToString() != "")
                    //    {
                    //        pimg = user2.Rows[0]["profilephoto"].ToString().Trim();
                    //        if (user2.Rows[0]["profilephoto"].ToString().Trim() == "noimage1.png" || user2.Rows[0]["profilephoto"].ToString().Trim() == "noimage3.jpg")
                    //        {
                    //            removeprofile.Visible = false;
                    //        }
                    //        else
                    //        {
                    //            removeprofile.Visible = true;
                    //        }
                    //        if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profilephoto/" + user2.Rows[0]["profilephoto"].ToString())))
                    //        {
                    //            imgprofile.Src = ReturnUrl("sitepath") + "images/profilephoto/" + user2.Rows[0]["profilephoto"].ToString();
                    //        }
                    //        else
                    //        {
                    //            imgprofile.Src = "http://graph.facebook.com/" + user2.Rows[0]["profilephoto"].ToString() + "/picture?type=large";
                    //        }
                    //    }
                    //    else
                    //    {
                    //        imgprofile.Src = ConfigurationManager.AppSettings["adminsitepath"] + "images/noprofile.jpg";
                    //        removeprofile.Visible = false;
                    //    }
                    //    if (File.Exists(Server.MapPath("~/themes/creative1.0/images/coverphoto/" + user2.Rows[0]["coverphoto"].ToString())))
                    //    {
                    //        cimg = user2.Rows[0]["coverphoto"].ToString().Trim();
                    //        //imgcover.Src = ReturnUrl("sitepath") + "images/coverphoto/" + user2.Rows[0]["coverphoto"].ToString();
                    //    }
                    //    else
                    //    {
                    //        //imgcover.Visible = false;
                    //        //removecover.Visible = false;
                    //    }
                    //}
                    //else
                    //{
                    //    imgprofile.Visible = false;
                    //    //imgcover.Visible = false;
                    //}

                    fillcountry();
                    country country = classaddress.GetCountryDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["country"].ToString()));
                    ddlcountry.SelectedIndex = ddlcountry.Items.IndexOf(ddlcountry.Items.FindByText(country.countryName.Trim()));

                    fillstate(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["country"].ToString()));
                    states state = classaddress.GetStateDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["state"].ToString()));
                    ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByText(state.statename.Trim()));

                    fillcity(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["state"].ToString()));

                    city city = classaddress.GetCityDetails(Convert.ToInt32(ds_userdetails.Tables[0].Rows[0]["city"].ToString()));

                    //ddlcity1.SelectedValue = ds_userdetails.Tables[0].Rows[0]["city"].ToString().Trim();
                    //txtcity.Text = ddlcity1.SelectedItem.Text.Trim();
                }
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        //Jayesh_Prajyot Comments below lin 14sep2017
        // Response.Redirect(ReturnUrl("sitepathmain") + "default");

    }
    protected void btnSaveChanges_Click(object sender, System.EventArgs e)
    { }
    //Jayesh_Prajyot Commneted Below tnSaveChanges_Click  Function to disable the functionality of Update button 14sep2017
    //protected void btnSaveChanges_Click(object sender, System.EventArgs e)
    //{
    //    bool iserror = false;
    //    string address1 = "";
    //    string tempaddr = "";
    //    string genders = "";

    //    if (rbtnmale.Checked == true)
    //    {
    //        genders = "M";
    //    }
    //    else
    //    {
    //        genders = "F";
    //    }
    //    if (txtdob.Text == "")
    //    {

    //    }
    //    else
    //    {
    //        DateTime dob = new DateTime();
    //        dob = Convert.ToDateTime(txtdob.Text);
    //        dob1 = dob.ToString("d");
    //    }

    //    if (txtemail.Text == "")
    //    {

    //    }
    //    else
    //    {

    //    }

    //    if (txtmobile.Text == "")
    //    {

    //    }
    //    else
    //    {
    //        string pattern = @"^[\+]?[1-9]{1,3}\s?[0-9]{6,11}$";
    //        Match match = Regex.Match(txtmobile.Text, pattern, RegexOptions.IgnoreCase);

    //        if (match.Success)
    //        {
    //            lblmob.Visible = false;

    //        }
    //        else
    //        {
    //            lblmob.Visible = true;
    //            diverror6.Visible = true;
    //            lblmob.ForeColor = System.Drawing.Color.Red;
    //            lblmob.Text = "Please enter a valid Mobile No.";

    //        }
    //    }

    //    if (txtfirstname.Text == "")
    //    {
    //        iserror = true;
    //        lblfname.Visible = true;
    //        diverror.Visible = true;
    //        lblfname.ForeColor = System.Drawing.Color.Red;
    //        lblfname.Text = "Please Enter first name";
    //    }
    //    else
    //    {
    //        string pattern = @"^[A-Za-z ]*$";
    //        Match match = Regex.Match(txtfirstname.Text.Trim(), pattern, RegexOptions.IgnoreCase);

    //        if (match.Success)
    //        {
    //            lblfname.Visible = false;
    //        }
    //        else
    //        {
    //            lblfname.Visible = true;
    //            diverror.Visible = true;
    //            lblfname.ForeColor = System.Drawing.Color.Red;
    //            lblfname.Text = "Only characters are allowed";
    //        }
    //    }

    //    if (txtlastname.Text == "")
    //    {
    //        iserror = true;
    //        diverror1.Visible = true;
    //        lbllame.ForeColor = System.Drawing.Color.Red;
    //        lbllame.Text = "Enter last name";
    //    }
    //    else
    //    {
    //        string pattern = @"^[A-Za-z ]*$";
    //        Match match = Regex.Match(txtlastname.Text.Trim(), pattern, RegexOptions.IgnoreCase);

    //        if (match.Success)
    //        {
    //            lbllame.Visible = false;

    //        }
    //        else
    //        {
    //            lbllame.Visible = true;



    //            diverror1.Visible = true;
    //            lbllame.ForeColor = System.Drawing.Color.Red;
    //            lbllame.Text = "Only characters are allowed";

    //        }

    //        if (txtaddress1.Text.ToString().Trim() == "")
    //        {
    //        }
    //        else
    //        {
    //            lbladdress.Text = "";
    //        }
    //        string var = System.IO.Path.GetExtension(uploadprofile.FileName);
    //        string savePath;

    //        string saveFile;
    //        DataTable user = classreviews.getuseridbyemail(Page.User.Identity.Name);
    //        if (user.Rows.Count > 0)
    //        {
    //            userid = user.Rows[0]["indexid"].ToString();
    //            imgprofile.Src = ReturnUrl("sitepath") + "images/profilephoto/" + user.Rows[0]["profilephoto"].ToString();
    //            pimg = user.Rows[0]["profilephoto"].ToString().ToString();
    //            imgcover.Src = ReturnUrl("sitepath") + "images/coverphoto/" + user.Rows[0]["coverphoto"].ToString();
    //            cimg = user.Rows[0]["coverphoto"].ToString();
    //        }

    //        if (uploadprofile.FileName != "")
    //        {
    //            try
    //            {
    //                System.Drawing.Image UploadedImage = System.Drawing.Image.FromStream(uploadprofile.PostedFile.InputStream);
    //                float UploadedImageWidth = UploadedImage.PhysicalDimension.Width;
    //                float UploadedImageHeight = UploadedImage.PhysicalDimension.Height;
    //                if ((var == ".jpg") || (var == ".gif") || (var == ".JPG") || (var == ".GIF") || (var == ".png") || (var == ".jpeg") || (var == ".PNG"))
    //                {
    //                    filedelete(Request.PhysicalApplicationPath + "themes/creative1.0/images/profile110x110", pimg);
    //                    filedelete(Request.PhysicalApplicationPath + "themes/creative1.0/images/profile55x55", pimg);
    //                    filedelete(Request.PhysicalApplicationPath + "themes/creative1.0/images/profilephoto", pimg);
    //                    iserror = false;
    //                    savePath = Path.Combine(Request.PhysicalApplicationPath, "themes/creative1.0/images/profilephoto/");
    //                    saveFile = Path.Combine(savePath, uploadprofile.FileName);
    //                    uploadprofile.SaveAs(saveFile);


    //                    classreviews.insertupdateprofilephoto(Page.User.Identity.Name, uploadprofile.FileName);
    //                    #region favourite img


    //                    string savePathsmallimage = Path.Combine(Request.PhysicalApplicationPath, "themes/creative1.0/images/profile110x110/");
    //                    string saveFilesmallimage = Path.Combine(savePathsmallimage, (uploadprofile.FileName));

    //                    System.Drawing.Image originalImage = System.Drawing.Image.FromStream(uploadprofile.PostedFile.InputStream, true, true);
    //                    if ((var.ToLower() == ".jpg") || (var.ToLower() == ".jpeg"))
    //                    {
    //                        System.Drawing.Image resizedImage = originalImage.GetThumbnailImage(125, (125 * originalImage.Height) / originalImage.Width, null, IntPtr.Zero);
    //                        resizedImage.Save(saveFilesmallimage, ImageFormat.Jpeg);
    //                        resizedImage.Dispose();
    //                    }
    //                    else if (var.ToLower() == ".bmp")
    //                    {
    //                        System.Drawing.Image resizedImage = originalImage.GetThumbnailImage(125, (125 * originalImage.Height) / originalImage.Width, null, IntPtr.Zero);
    //                        resizedImage.Save(saveFilesmallimage, ImageFormat.Bmp);
    //                        resizedImage.Dispose();
    //                    }
    //                    else if (var.ToLower() == ".gif")
    //                    {
    //                        System.Drawing.Image resizedImage = originalImage.GetThumbnailImage(125, (125 * originalImage.Height) / originalImage.Width, null, IntPtr.Zero);
    //                        resizedImage.Save(saveFilesmallimage, ImageFormat.Gif);
    //                        resizedImage.Dispose();
    //                    }
    //                    else if (var.ToLower() == ".png")
    //                    {
    //                        System.Drawing.Image resizedImage = originalImage.GetThumbnailImage(125, (125 * originalImage.Height) / originalImage.Width, null, IntPtr.Zero);
    //                        resizedImage.Save(saveFilesmallimage, ImageFormat.Png);
    //                        resizedImage.Dispose();
    //                    }
    //                    else
    //                    {
    //                        System.Drawing.Image resizedImage = originalImage.GetThumbnailImage(125, (125 * originalImage.Height) / originalImage.Width, null, IntPtr.Zero);
    //                        resizedImage.Save(saveFilesmallimage, ImageFormat.Jpeg);
    //                        resizedImage.Dispose();
    //                    }



    //                    #endregion
    //                    #region wall profile Image


    //                    string savePathsmallimage1 = Path.Combine(Request.PhysicalApplicationPath, "themes/creative1.0/images/profile55x55/");
    //                    string saveFilesmallimage1 = Path.Combine(savePathsmallimage1, (uploadprofile.FileName));

    //                    if ((var.ToLower() == ".jpg") || (var.ToLower() == ".jpeg"))
    //                    {
    //                        System.Drawing.Image resizedImage1 = originalImage.GetThumbnailImage(45, (45 * originalImage.Height) / originalImage.Width, null, IntPtr.Zero);
    //                        resizedImage1.Save(saveFilesmallimage1, ImageFormat.Jpeg);
    //                        resizedImage1.Dispose();
    //                    }
    //                    else if (var.ToLower() == ".bmp")
    //                    {
    //                        System.Drawing.Image resizedImage1 = originalImage.GetThumbnailImage(45, (45 * originalImage.Height) / originalImage.Width, null, IntPtr.Zero);
    //                        resizedImage1.Save(saveFilesmallimage1, ImageFormat.Bmp);
    //                        resizedImage1.Dispose();
    //                    }
    //                    else if (var.ToLower() == ".gif")
    //                    {
    //                        System.Drawing.Image resizedImage1 = originalImage.GetThumbnailImage(45, (45 * originalImage.Height) / originalImage.Width, null, IntPtr.Zero);
    //                        resizedImage1.Save(saveFilesmallimage1, ImageFormat.Gif);
    //                        resizedImage1.Dispose();
    //                    }
    //                    else if (var.ToLower() == ".png")
    //                    {
    //                        System.Drawing.Image resizedImage1 = originalImage.GetThumbnailImage(45, (45 * originalImage.Height) / originalImage.Width, null, IntPtr.Zero);
    //                        resizedImage1.Save(saveFilesmallimage1, ImageFormat.Png);
    //                        resizedImage1.Dispose();
    //                    }
    //                    else
    //                    {
    //                        System.Drawing.Image resizedImage1 = originalImage.GetThumbnailImage(45, (45 * originalImage.Height) / originalImage.Width, null, IntPtr.Zero);
    //                        resizedImage1.Save(saveFilesmallimage1, ImageFormat.Jpeg);
    //                        resizedImage1.Dispose();
    //                    }

    //                    #endregion
    //                    imgprofile.Src = ReturnUrl("sitepath") + "images/profilephoto/" + uploadprofile.FileName;
    //                    imgprofile.Visible = true;
    //                    removeprofile.Visible = true;
    //                }
    //                else
    //                {
    //                    lblstatus.Visible = true;
    //                    iserror = true;
    //                    lblstatus.ForeColor = System.Drawing.Color.Red;
    //                    lblstatus.Text = "Upload .png/.gif/.jpg/.jpeg image only!";
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                lblstatus.Visible = true;
    //                iserror = true;
    //                lblstatus.ForeColor = System.Drawing.Color.Red;
    //                lblstatus.Text = "Upload .png/.gif/.jpg/.jpeg image only!";
    //            }
    //        }
    //        else
    //        {
    //            if (pimg == "noimage1.png" || pimg == "noimage3.jpg" || pimg == "" || pimg == null)
    //            {
    //                if (genders == "M")
    //                {
    //                    classreviews.insertupdateprofilephoto(Page.User.Identity.Name, "noimage1.png");
    //                }
    //                else
    //                {
    //                    classreviews.insertupdateprofilephoto(Page.User.Identity.Name, "noimage3.jpg");
    //                }
    //            }
    //        }

    //        var = System.IO.Path.GetExtension(uploadcover.FileName);
    //        if (uploadcover.FileName != "")
    //        {
    //            try
    //            {
    //                System.Drawing.Image UploadedImage = System.Drawing.Image.FromStream(uploadcover.PostedFile.InputStream);
    //                float UploadedImageWidth = UploadedImage.PhysicalDimension.Width;
    //                float UploadedImageHeight = UploadedImage.PhysicalDimension.Height;
    //                if ((var == ".jpg") || (var == ".gif") || (var == ".JPG") || (var == ".GIF") || (var == ".png") || (var == ".jpeg") || (var == ".PNG"))
    //                {
    //                    iserror = false;
    //                    savePath = Path.Combine(Request.PhysicalApplicationPath, "themes/creative1.0/images/coverphoto/");
    //                    saveFile = Path.Combine(savePath, uploadcover.FileName);
    //                    uploadcover.SaveAs(saveFile);
    //                    if (cimg != "")
    //                    {
    //                        filedelete(Request.PhysicalApplicationPath + "themes/creative1.0/images/coverphoto", cimg);
    //                    }
    //                    classreviews.insertupdatecoverphoto(Page.User.Identity.Name, uploadcover.FileName);
    //                    imgcover.Src = ReturnUrl("sitepath") + "images/coverphoto/" + uploadcover.FileName;
    //                    imgcover.Visible = true;
    //                    removecover.Visible = true;
    //                }
    //                else
    //                {
    //                    lblstatus2.Visible = true;
    //                    iserror = true;
    //                    lblstatus2.ForeColor = System.Drawing.Color.Red;
    //                    lblstatus2.Text = "Upload .png/.gif/.jpg/.jpeg image only!";
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                lblstatus2.Visible = true;
    //                iserror = true;
    //                lblstatus2.ForeColor = System.Drawing.Color.Red;
    //                lblstatus2.Text = "Upload .png/.gif/.jpg/.jpeg image only!";
    //            }
    //        }

    //        int countryid = 0, stateid = 0, cityid = 0;
    //        DataTable dt = classaddress.getcountrystatebycity(txtcity.Text.Trim());
    //        if (dt.Rows.Count > 0)
    //        {
    //            countryid = Convert.ToInt32(dt.Rows[0]["countryid"].ToString());
    //            stateid = Convert.ToInt32(dt.Rows[0]["stateid"].ToString());
    //            cityid = Convert.ToInt32(dt.Rows[0]["cityid"].ToString());
    //        }
    //        else
    //        {
    //            iserror = true;
    //            lblcity.Visible = true;
    //            lblcity.ForeColor = System.Drawing.Color.Red;
    //            lblcity.Text = "Please select correct city";
    //        }

    //        // Location, department, sub_department and designation validation code

    //        if (txtloc.Text != "" && txtloc.Text.Length > 0)
    //        {
    //            DataTable dtloc = classentity.getentitybyname(txtloc.Text.Trim());
    //            if (dtloc.Rows.Count > 0)
    //            {
    //                loc = txtloc.Text.Trim();
    //            }


    //            else
    //            {
    //                iserror = true;
    //                lblloc.Visible = true;
    //                lblloc.ForeColor = System.Drawing.Color.Red;
    //                lblloc.Text = "Please select correct location";
    //                txtloc.Text = "";

    //            }
    //        }
    //        // SONY uncommented above  IF statement STARTS HERE

    //        //if (txtdept.Text != "" && txtdept.Text.Length > 0)
    //        //{
    //        //    DataTable dtdept = classentity.getDepartmentByMatchName(txtdept.Text.Trim());
    //        //    if (dtdept.Rows.Count > 0)
    //        //    {
    //        //        did = Convert.ToInt32(dtdept.Rows[0]["dept_id"]);
    //        //        dept = txtdept.Text.Trim();
    //        //    }
    //        //    else
    //        //    {
    //        //        iserror = true;
    //        //        lbldept.Visible = true;
    //        //        lbldept.ForeColor = System.Drawing.Color.Red;
    //        //        lbldept.Text = "Please select correct department";
    //        //        txtdept.Text = "";
    //        //    }
    //        //}
    //        // SONY uncommented above  IF statement ENDS HERE

    //        dept = txtdept.Text.ToString().Trim();
    //        desg = txtdesg.Text.ToString().Trim();

    //        // SONY uncommented above  IF statement STARTS HERE

    //        //if (txtdesg.Text != "" && txtdesg.Text.Length > 0)
    //        //{
    //        //    DataTable dtdesg = classentity.GetDesignationBymatchName(txtdesg.Text.Trim());
    //        //    if (dtdesg.Rows.Count > 0)
    //        //    {
    //        //        desg = txtdesg.Text.Trim();
    //        //    }
    //        //    else
    //        //    {
    //        //        iserror = true;
    //        //        lbldesg.Visible = true;
    //        //        lbldesg.ForeColor = System.Drawing.Color.Red;
    //        //        lbldesg.Text = "Please select correct designation";
    //        //        txtdesg.Text = "";
    //        //    }
    //        //}
    //        // SONY uncommented above  IF statement ENDS HERE


    //        if (txtsubdept.Text != "" && txtsubdept.Text.Length > 0)
    //        {
    //            DataTable dtsubdept = classentity.getSubdeptByParentIDandSubdeptName(did, txtsubdept.Text.Trim());
    //            if (dtsubdept.Rows.Count > 0)
    //            {
    //                subdept = txtsubdept.Text.Trim();
    //            }
    //            else
    //            {
    //                iserror = true;
    //                lblsubdept.Visible = true;
    //                lblsubdept.ForeColor = System.Drawing.Color.Red;
    //                lblsubdept.Text = "Please select correct sub department";
    //                txtsubdept.Text = "";
    //            }
    //        }

    //        if (iserror == false)
    //        {
    //            lblstatus.Visible = false;
    //            lblstatus2.Visible = false;
    //            classaddress.edituser(this.Page.User.Identity.Name, txtfirstname.Text.ToString().Trim(), txtlastname.Text.ToString().Trim(), txtaddress1.Text.ToString().Trim(), txtmobile.Text.ToString().Trim(), countryid, stateid, cityid, txtpincode.Text.Trim(), txtphone2.Text.Trim(), txtphone.Text.Trim(), txtdob.Text.ToString().Trim(), genders, txtoffno.Text.ToString().Trim(), txtaltno.Text.ToString().Trim(), txtoffphone.Text.ToString().Trim(), txtextension.Text.ToString().Trim(), txtaltemail.Text.ToString(), txtfaxno.Text.ToString(), loc.ToString().Trim(), txtdept.Text.ToString().Trim(), "", txtdesg.Text.ToString().Trim(), txttempaddress.Text.ToString().Trim(), txtemailadress.Text.ToString().Trim());

    //            if (txtdob.Text.ToString() != "" && txtdob.Text.ToString() != null)
    //            {
    //                classaddress.insertupdatebirth(txtemail.Text.ToString(), dob1, "B");
    //            }

    //            //Department Insert
    //            if (dept != "" && dept != null)
    //            {
    //                DataTable dtdept = classentity.getDepartmentByMatchName(dept.ToString().Trim());
    //                if (dtdept.Rows.Count == 0)
    //                {
    //                    int dpt = Convert.ToInt32(classentity.createDepartment(dept, 0));
    //                    if (dpt > 0)
    //                    {

    //                    }
    //                }
    //            }

    //            // Designation
    //            if (desg != "" && desg != null)
    //            {
    //                DataTable dtdesg = classentity.GetDesignationBymatchName(desg.ToString().Trim());
    //                if (dtdesg.Rows.Count == 0)
    //                {
    //                    int desg1 = Convert.ToInt32(classentity.createDesignation(desg));
    //                    if (desg1 > 0)
    //                    {

    //                    }
    //                }
    //            }

    //            DataTable dt1 = classxml.topbdayList();
    //            if (dt1.Rows.Count > 0)
    //            {
    //                DataSet ds = new DataSet();
    //                ds.Tables.Add(dt1);
    //                saveXml(ds, "birthday.xml");
    //            }
    //            else
    //            {
    //                DataSet ds = new DataSet();
    //                ds.Tables.Add(dt1);
    //                saveXml(ds, "birthday.xml");
    //            }
    //            lblmessage.Visible = true;
    //            divmsg.Visible = true;
    //            lblmessage.Text = "Profile Updated Successfully !!!";
    //            editform.Visible = false;
    //            divbtn.Visible = true;
    //            mywall();
    //        }
    //    }
    //}
    public void mywall()
    {
        DataSet dswall = classproduct.gettopmywall();
        dswall = saveXml2(dswall, "mywall.xml");
        if (!dswall.Tables[0].Columns.Contains("videoembed") || !dswall.Tables[0].Columns.Contains("filename") || !dswall.Tables[0].Columns.Contains("movietrailorcode") || !dswall.Tables[0].Columns.Contains("bigimage"))
        {
            if (!dswall.Tables[0].Columns.Contains("videoembed"))
            {
                dswall.Tables[0].Columns.Add("videoembed");
            }
            if (!dswall.Tables[0].Columns.Contains("movietrailorcode"))
            {
                dswall.Tables[0].Columns.Add("movietrailorcode");
            }
            if (!dswall.Tables[0].Columns.Contains("filename"))
            {
                dswall.Tables[0].Columns.Add("filename");
            }
            if (!dswall.Tables[0].Columns.Contains("filename"))
            {
                dswall.Tables[0].Columns.Add("filename");
            }
            saveXml(dswall, "mywall.xml");
        }
    }
    public DataSet saveXml2(DataSet ds, string filename)
    {
        string fpath = Server.MapPath("~/xml") + "\\" + filename;
        StreamWriter myStreamWriter = new StreamWriter(@"" + fpath);
        ds.WriteXml(myStreamWriter);
        myStreamWriter.Close();
        return ds;
    }
    public void saveXml(DataSet ds, string filename)
    {
        string fpath = Server.MapPath("~/xml") + "\\" + filename;
        StreamWriter myStreamWriter = new StreamWriter(@"" + fpath);
        ds.WriteXml(myStreamWriter);
        myStreamWriter.Close();
    }
    public string GetSafeFileName(string Filename)
    {
        string newStr = "";
        Filename = Filename.Replace("<", newStr);
        Filename = Filename.Replace(">", newStr);
        Filename = Filename.Replace(" ", newStr);
        Filename = Filename.Replace("%", newStr);
        Filename = Filename.Replace("*", newStr);
        Filename = Filename.Replace("|", newStr);
        Filename = Filename.Replace("-", newStr);
        Filename = Filename.Replace("#", newStr);
        Filename = Filename.Replace("&", newStr);
        Filename = Filename.Replace("@", newStr);
        Filename = Filename.Replace("!", newStr);
        Filename = Filename.Replace("$", newStr);
        Filename = Filename.Replace(" ", newStr);
        return Filename;
    }
    public string ReplaceFileName(string str)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < str.Length; i++)
        {
            if (char.IsLetterOrDigit(str[i]) || char.IsSymbol('.'))
            {
                sb.Append(str[i]);
            }
        }
        return sb.ToString();
    }
    protected void FCLoginView_ViewChanged(object sender, System.EventArgs e)
    {
        DisplayProfileProperties();
    }
    public void fillcountry()
    {
        ddlcountry.Items.Clear();
        ProfileCommon profile = this.Profile;
        profile = this.Profile.GetProfile(this.Page.User.Identity.Name);
        source = new SqlConnection(creativeconfiguration.DbConnectionString);
        string SqlQuery1 = "select distinct ltrim(rtrim(countryname)) as countryname,countryID from country order by countryname asc";
        sqladp = new SqlDataAdapter(SqlQuery1, source);
        DataTable dt = new DataTable();
        sqladp.Fill(dt);
        ddlcountry.DataSource = dt;
        ddlcountry.Items.Clear();
        ddlcountry.DataTextField = "countryname";
        ddlcountry.DataValueField = "countryID";
        ddlcountry.DataBind();
        ListItem item = new ListItem("--Choose Country--", "0");
        ddlcountry.Items.Insert(0, item);

        if (ddlcountry.SelectedItem.Text != "--Choose Country--")
        {
            fillstate(Convert.ToInt32(ddlcountry.SelectedValue));
            fillcity(Convert.ToInt32(ddlstate.SelectedValue));
        }

        else
        {

            ListItem item1 = new ListItem("--Choose State--", "0");
            ddlstate.Items.Insert(0, item1);

            ListItem item2 = new ListItem("--Choose City--", "0");
            //ddlcity1.Items.Insert(0, item2);
        }

    }
    public void fillstate(int country)
    {
        DropDownList ddl = new DropDownList();

        ddl = ddlcountry;
        source = new SqlConnection(creativeconfiguration.DbConnectionString);
        string SqlQuery1 = "select ltrim(rtrim(statename)) as statename,stateid from state where countryid=" + country + " order by statename asc";
        sqladp = new SqlDataAdapter(SqlQuery1, source);
        DataTable dt = new DataTable();
        sqladp.Fill(dt);
        ddlstate.DataSource = dt;
        ddlstate.Items.Clear();
        ddlstate.DataTextField = "statename";
        ddlstate.DataValueField = "stateid";
        ddlstate.DataBind();
        ListItem item = new ListItem("--Choose State--", "0");
        ddlstate.Items.Insert(0, item);

        if (ddlstate.SelectedItem.Text != "--Choose State--")
        {
            fillcity(Convert.ToInt32(ddlstate.SelectedValue));
        }

        else
        {

            ListItem item2 = new ListItem("--Choose City--", "0");
            //ddlcity1.Items.Insert(0, item2);
        }

    }
    public void fillcity(int city)
    {
        DropDownList ddl = new DropDownList();

        ddl = ddlstate;

        source = new SqlConnection(creativeconfiguration.DbConnectionString);
        string SqlQuery1 = "select ltrim(rtrim(cityname)) as cityname,cityid from city where stateid=" + city + " order by cityname asc";

        sqladp = new SqlDataAdapter(SqlQuery1, source);
        DataTable dt = new DataTable();
        sqladp.Fill(dt);

        //ddlcity1.DataSource = dt;
        //ddlcity1.Items.Clear();
        //ddlcity1.DataTextField = "cityname";
        //ddlcity1.DataValueField = "cityid";

        DropDownList ddcitylist = new DropDownList();
        TextBox txtci = new TextBox();
        int i = 0;

        //ddcitylist = ddlcity1;
        //ddcitylist.Items.Clear();
        ListItem lst3 = new ListItem();

        //ddlcity1.DataBind();
        ListItem item = new ListItem("--Choose City--", "0");
        ddcitylist.Items.Insert(0, item);

    }
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddslist1 = new DropDownList();
        ddslist1 = ddlcountry;

        if (ddslist1.SelectedItem.Value != "--Choose Country--")
        {
            fillstate(Convert.ToInt32(ddlcountry.SelectedItem.Value.ToString()));
            fillcity(Convert.ToInt32(ddlstate.SelectedValue));
        }
    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddslist = new DropDownList();
        ddslist = ddlstate;
        if (ddslist.SelectedItem.Value != "--Choose State--")
        {
            fillcity(Convert.ToInt32(ddslist.SelectedItem.Value.ToString()));
        }
    }
    protected void ddlcity1_SelectedIndexChanged1(object sender, EventArgs e)
    {

        //if (ddlcity1.SelectedValue == "Others")
        {
            pnlothercity.Visible = false;
            txtothercity.Text = "";
            txtothercity.Visible = false;
        }
        //else
        //{
        //    txtothercity.Visible = false;
        //}
    }
    protected void ddlprofile_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlprofile.SelectedItem.Value.ToString() == "edit")
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "procs/SampleForm");
        }
        else if (ddlprofile.SelectedItem.Value.ToString() == "pwd")
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "procs/changepassword");
        }
        else if (ddlprofile.SelectedItem.Value.ToString() == "wishlist")
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "procs/wishlist");
        }
        else if (ddlprofile.SelectedItem.Value.ToString() == "preference")
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "procs/preference");
        }
        else if (ddlprofile.SelectedItem.Value.ToString() == "subscription")
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "procs/subscriptionhistory");
        }
        else if (ddlprofile.SelectedItem.Value.ToString() == "pthistory")
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "procs/pthistory");
        }
        else if (ddlprofile.SelectedItem.Value.ToString() == "logout")
        {
            Session.Abandon();
            Request.Cookies.Clear();
            FormsAuthentication.SignOut();
            Response.Redirect(ReturnUrl("sitepathmain") + "default", true);
        }

    }
    protected void btnSingOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Request.Cookies.Clear();
        FormsAuthentication.SignOut();
        Response.Redirect(ReturnUrl("sitepathmain") + "default", true);
    }
    protected void removeprofile_Click(object sender, EventArgs e)
    {
        bool iserror;
        try
        {
            iserror = classreviews.insertupdateprofilephoto(Page.User.Identity.Name, "");
            if (iserror == false)
            {
                lblstatus.Text = "Please try again !";
            }
            else
            {
                lblstatus.Visible = true;
                filedelete(Request.PhysicalApplicationPath + "themes/creative1.0/images/profile110x110", pimg);
                filedelete(Request.PhysicalApplicationPath + "themes/creative1.0/images/profile55x55", pimg);
                filedelete(Request.PhysicalApplicationPath + "themes/creative1.0/images/profilephoto", pimg);
                lblstatus.Text = "Profile photo removed successfully !";
                //imgprofile.Visible = false;
                //removeprofile.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblstatus.Text = "Please try again !";
        }
    }
    protected void removecover_Click(object sender, EventArgs e)
    {
        bool iserror;
        try
        {
            iserror = classreviews.insertupdatecoverphoto(Page.User.Identity.Name, "");
            if (iserror == false)
            {
                lblstatus2.Text = "Please try again !";
            }
            else
            {
                lblstatus2.Visible = true;
                filedelete(Request.PhysicalApplicationPath + "themes/creative1.0/images/coverphoto", cimg);
                lblstatus2.Text = "Cover photo removed successfully !";
                //imgcover.Visible = false;
                //removecover.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblstatus2.Text = "Please try again !";
        }
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
        string[] st;
        st = Directory.GetFiles(path);
        path += "\\" + filename;
        int i;
        if (filename != "noimage1.png" && filename != "noimage3.jpg")
        {
            for (i = 0; i < st.Length; i++)
            {
                try
                {
                    if (st.GetValue(i).ToString() == path)
                    {
                        File.Delete(st.GetValue(i).ToString());
                    }
                }
                catch { }
            }
        }
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlloc_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlsubdept_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddldesg_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
