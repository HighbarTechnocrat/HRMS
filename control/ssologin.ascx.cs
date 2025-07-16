using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;
using System.Data;
using System.Xml.Linq;
using System.Web.Security;
using zyduscadila;

public partial class themes_ssologin : System.Web.UI.UserControl
{
    private Random random = new Random();
    public static string soapResult="",id = "", ssokey = "", ext_url = "", data = "", valid = "", eid = "", name = "",fname="",lname="", email = "", mobile = "",dob="",gender="", dept="", desg="", address="", tempadd="",extno="", uname ="";
    public static string urlpath = "", action = "", mainpath = "";

    public class Employee
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string continent { get; set; }
    }
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {
        //code by sony starts here

 //       if (Request.QueryString["user"] != null && Request.QueryString["user"] != "")
 //       {
 //           if (Request.QueryString["user"].ToString() == "Highbar")
 //           {
 //               HttpCookie usercookie = new HttpCookie("user", "Highbar");
 //               Response.Cookies.Add(usercookie);

 //               if (Request.QueryString["internet"] != null && Request.QueryString["internet"] != "")
 //               {
 //                   if (Request.QueryString["internet"].ToString() == "true")
 //                   {
 //                       HttpCookie cookie = new HttpCookie("internet", "true");
 //                       Response.Cookies.Add(cookie);
 //                       urlpath = ConfigurationManager.AppSettings["ZS1"].ToString();
 //                       action = ConfigurationManager.AppSettings["ZA1"].ToString();
 //                       mainpath = ConfigurationManager.AppSettings["internetURL"].ToString();
 //                   }
 //                   else
 //                   {
 //                       HttpCookie cookie = new HttpCookie("internet", "false");
 //                       Response.Cookies.Add(cookie);
 //                       urlpath = ConfigurationManager.AppSettings["ZS2"].ToString();
 //                       action = ConfigurationManager.AppSettings["ZA2"].ToString();
 //                       mainpath = ConfigurationManager.AppSettings["intranetURL"].ToString();
 //                   }
 //               }
 //               else
 //               {
 //                   HttpCookie cookie = new HttpCookie("internet", "false");
 //                   Response.Cookies.Add(cookie);
 //                   urlpath = ConfigurationManager.AppSettings["ZS2"].ToString();
 //                   action = ConfigurationManager.AppSettings["ZA2"].ToString();
 //                   mainpath = ConfigurationManager.AppSettings["intranetURL"].ToString();
 //               }


 //           }
 //           else
 //           {
 //               HttpCookie usercookie = new HttpCookie("user", "");
 //               Response.Cookies.Add(usercookie);
 //           }


 //           if (Request.QueryString["internet"] == null && Request.QueryString["internet"] == "")
 //           {
 //                   HttpCookie cookie = new HttpCookie("internet", "false");
 //                   Response.Cookies.Add(cookie);
 //                   urlpath = ConfigurationManager.AppSettings["ZS2"].ToString();
 //                   action = ConfigurationManager.AppSettings["ZA2"].ToString();
 //                   mainpath = ConfigurationManager.AppSettings["intranetURL"].ToString();                
 //           }
 //}

//code by sony ends here
        try
        {
            if (Request.QueryString["internet"] != null && Request.QueryString["internet"] != "")
            {
                if (Request.QueryString["internet"].ToString() == "true")
                {
                    HttpCookie cookie = new HttpCookie("internet", "true");
                    Response.Cookies.Add(cookie);
                    urlpath = ConfigurationManager.AppSettings["ZS1"].ToString();
                    action = ConfigurationManager.AppSettings["ZA1"].ToString();
                    mainpath = ConfigurationManager.AppSettings["internetURL"].ToString();
                }
                else
                {
                    HttpCookie cookie = new HttpCookie("internet", "false");
                    Response.Cookies.Add(cookie);
                    urlpath = ConfigurationManager.AppSettings["ZS2"].ToString();
                    action = ConfigurationManager.AppSettings["ZA2"].ToString();
                    mainpath = ConfigurationManager.AppSettings["intranetURL"].ToString();
                }
            }
            else
            {
                HttpCookie cookie = new HttpCookie("internet", "false");
                Response.Cookies.Add(cookie);
                urlpath = ConfigurationManager.AppSettings["ZS2"].ToString();
                action = ConfigurationManager.AppSettings["ZA2"].ToString();
                mainpath = ConfigurationManager.AppSettings["intranetURL"].ToString();
            }

        if (Request.QueryString["user"] != null && Request.QueryString["user"] != "")
        {
            if (Request.QueryString["user"].ToString() == "Highbar")
            {
                HttpCookie usercookie = new HttpCookie("user", "Highbar");
                Response.Cookies.Add(usercookie);
                HttpCookie cookie = new HttpCookie("internet", "false");
                Response.Cookies.Add(cookie);
            }
            else
            {
                HttpCookie usercookie = new HttpCookie("user", "");
                Response.Cookies.Add(usercookie);
                HttpCookie cookie = new HttpCookie("internet", "true");

                Response.Cookies.Add(cookie);
            }
        }
        else
        {
            HttpCookie usercookie = new HttpCookie("user", "");
            Response.Cookies.Add(usercookie);
        }


            if (Request.QueryString["id"] != null && Request.QueryString["id"] != "" && Request.QueryString["ssokey"] != null && Request.QueryString["ssokey"] != "")
            {
                id = Request.QueryString["id"].ToString();
                ssokey = Request.QueryString["ssokey"].ToString();
                display(id, ssokey);
            }
        }
        catch(Exception ex)
        {
			//sony uncommented this to cacth exception error		
            Response.Write(ex);
        }
    }
    public void authenticate(string param1,string param2)
    {
        try
        {
            ext_url = "http://portal.highbartech.com/UserManagement/SSOAuth.asmx";
            Uri myUri = new Uri(ext_url, UriKind.Absolute);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(myUri);
            request.Method = "POST";
            request.KeepAlive = true;
            request.AllowAutoRedirect = false;
            request.Accept = "*/*";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("User_ID", "1000000338");
            request.Headers.Add("SSOKey", "zdVgp9vzoR");
            request.ContentLength = 0;
            WebResponse resp = request.GetResponse();
            Stream imageStream = resp.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader readStream = new StreamReader(imageStream, encode);
            string responseImage = readStream.ReadToEnd();
            readStream.Close();
            imageStream.Close();
            resp.Close();
        }
        catch (WebException webex)
        {
            WebResponse errResp = webex.Response;
            using (Stream respStream = errResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(respStream);
                string text = reader.ReadToEnd();
            }
        }
    }
    public void display(string UserName,string SSOKey)
    {
        zyduscadila.SSOAuth cs = new SSOAuth();
        zyduscadila.SSOAuthServiceUser su = new SSOAuthServiceUser();
        su.UserName = "SSOAuthServiceUser";
        su.Password = "zyAuthu53r@123";
        zyduscadila.ValidateSSOKeyForIntranetRes rs = new ValidateSSOKeyForIntranetRes();
        cs.SSOAuthServiceUserValue = su;
        rs = cs.ValidateSSOKeyForIntranet(id, ssokey);  
        if(rs!=null)
        {
            if (rs.IsValid != null)
            {
                lblvalid.Text = rs.IsValid.ToString();
                valid = rs.IsValid.ToString();
                if (valid == "true" || valid == "True")
                {
                    if (rs.Name != null && rs.Name != "")
                    {
                        lblname.Text = rs.Name.ToString();
                        string[] names = rs.Name.ToString().Split(' ');
                        if (names.Length > 2)
                        {
                            fname = names[0];
                            lname = names[1] + names[2];
                        }
                        else if (names.Length > 1)
                        {
                            fname = names[0];
                            lname = names[1];
                        }
                        else
                        {
                            fname = names[0];
                            lname = "";
                        }
                    }
                    if (rs.EmployeeNo != null && rs.EmployeeNo != "")
                    {
                        eid = rs.EmployeeNo.ToString().Trim();
                        lblid.Text = eid;
                    }
                    if (rs.UserName != null && rs.UserName != "")
                    {
                        lbllogin.Text = rs.UserName.ToString().Trim();
                        uname = rs.UserName.ToString().Trim();
                    }
                    if (rs.MobileNo != null && rs.MobileNo != "")
                    {
                        mobile = rs.MobileNo.ToString().Trim();
                        lblmobile.Text = mobile;
                    }
                    if (rs.EmailID != null && rs.EmailID != "")
                    {
                        email = rs.EmailID.ToString();
                        lblemail.Text = email;
                    }
                    if (rs.DateOfBirth != null && rs.DateOfBirth != "")
                    {
                        dob = rs.DateOfBirth.ToString();
                        lblbod.Text = dob;
                    }
                    if (rs.Department.ToString() != null && rs.Department != "")
                    {
                        dept = rs.Department.ToString();
                        lbldept.Text = dept;
                    }
                    if (rs.Designation.ToString() != "" && rs.Designation != null)
                    {
                        desg = rs.Designation.ToString();
                        lbldesg.Text = desg;
                    }
                    if (rs.ExtensionNo.ToString() != null && rs.ExtensionNo != "")
                    {
                        extno = rs.ExtensionNo.ToString();
                        lblextno.Text = extno;
                    }
                    if (rs.PermanentAddress != null && rs.PermanentAddress != "")
                    {
                        address = rs.PermanentAddress.ToString();
                        lbladdress.Text = address;
                    }
                    if (rs.TemporaryAddress != null && rs.TemporaryAddress != "")
                    {
                        tempadd = rs.TemporaryAddress.ToString();
                        lbltempadd.Text = tempadd;
                    }
                    if (rs.Gender != "" && rs.Gender != null)
                    {
                        gender = rs.Gender.ToString();
                        if (gender == "1")
                        {
                            gender = "M";
                        }
                        else if (gender == "2")
                        {
                            gender = "F";
                        }
                        else
                        {
                            gender = "M";
                        }
                        lblgender.Text = gender;
                    }
                }
                else
                {
                    if (Request.QueryString["internet"] != null && Request.QueryString["internet"] != "")
                    {
                        if (Request.QueryString["internet"].ToString() == "true")
                        {
                            Response.Redirect(ConfigurationManager.AppSettings["internetURL"].ToString().Trim());
                        }
                        else
                        {
                            Response.Redirect(ConfigurationManager.AppSettings["intranetURL"].ToString().Trim());
                        }
                    }
                    else
                    {
                        Response.Redirect(ConfigurationManager.AppSettings["intranetURL"].ToString().Trim());
                    }
                    lblerror.Visible = true;
                }
            }
        }
        //data= CallWebService();
        //XmlDocument xmlDoc = new XmlDocument();
        //xmlDoc.LoadXml(data);
        //XmlNodeList xd = xmlDoc.GetElementsByTagName("ValidateSSOKeyForIntranetResult");
        //foreach (XmlNode node in xd)
        //{
        //    XmlElement xe = (XmlElement)node;
        //    XmlNodeList xdvalid= xe.GetElementsByTagName("IsValid");
        //    XmlNode xnvalid=xdvalid.Item(0);
        //    if(xnvalid!=null)
        //    {
        //        valid = xnvalid.InnerText;
        //        lblvalid.Text = valid;
        //    }
        //    XmlNodeList xdid = xe.GetElementsByTagName("EmployeeNo");
        //    XmlNode xnid = xdid.Item(0);
        //    if (xnid != null)
        //    {
        //        eid = xnid.InnerText;
        //        lblid.Text = eid;
        //    }
        //    XmlNodeList xdname = xe.GetElementsByTagName("Name");
        //    XmlNode xnname= xdname.Item(0);
        //    if (xnname != null)
        //    {
        //        name = xnname.InnerText;
        //        lblname.Text = name;
        //        string[] names = name.Split(' ');
        //        if (names.Length > 2)
        //        {
        //            fname = names[0];
        //            lname = names[1] + names[2];
        //        }
        //        else if (names.Length > 1)
        //        {
        //            fname = names[0];
        //            lname = names[1];
        //        }
        //        else
        //        {
        //            fname = names[0];
        //            lname = "";
        //        }
        //    }

        //    XmlNodeList xduname = xe.GetElementsByTagName("UserName");
        //    XmlNode xnuname = xduname.Item(0);
        //    if (xnuname != null)
        //    {
        //        uname = xnuname.InnerText;
        //        lbllogin.Text = email;
        //    }

        //    XmlNodeList xdemail = xe.GetElementsByTagName("EmailID");
        //    XmlNode xnemail = xdemail.Item(0);
        //    if (xnemail != null)
        //    {
        //        email = xnemail.InnerText;
        //        lblemail.Text = email;
        //    }
        //    XmlNodeList xdmobile = xe.GetElementsByTagName("MobileNo");
        //    XmlNode xnmobile = xdmobile.Item(0);
        //    if (xnmobile != null)
        //    {
        //        mobile = xnmobile.InnerText;
        //        lblmobile.Text = mobile;
        //    }
        //    XmlNodeList xbod = xe.GetElementsByTagName("DateOfBirth");
        //    XmlNode xnbod = xbod.Item(0);
        //    if (xnbod != null)
        //    {
        //        dob = xnbod.InnerText;
        //        lblbod.Text = dob;
        //    }
        //    XmlNodeList xddept = xe.GetElementsByTagName("Department");
        //    XmlNode xndept = xddept.Item(0);
        //    if (xndept != null)
        //    {
        //        dept = xndept.InnerText;
        //        lbldept.Text = dept;
        //    }
        //    XmlNodeList xddesg = xe.GetElementsByTagName("Designation");
        //    XmlNode xndesg = xddesg.Item(0);
        //    if (xndesg != null)
        //    {
        //        desg = xndesg.InnerText;
        //        lbldesg.Text = desg;
        //    }

        //    XmlNodeList xdextno = xe.GetElementsByTagName("ExtensionNo");
        //    XmlNode xnextno = xdextno.Item(0);
        //    if (xnextno != null)
        //    {
        //        extno = xnextno.InnerText;
        //        lblextno.Text = extno;
        //    }

        //    XmlNodeList xdadd = xe.GetElementsByTagName("PermanentAddress");
        //    XmlNode xnadd = xdadd.Item(0);
        //    if (xnadd != null)
        //    {
        //        address = xnadd.InnerText;
        //        lbladdress.Text = address;
        //    }

        //    XmlNodeList xdtempadd = xe.GetElementsByTagName("TemporaryAddress");
        //    XmlNode xntempadd = xdtempadd.Item(0);
        //    if (xntempadd != null)
        //    {
        //        tempadd = xntempadd.InnerText;
        //        lbltempadd.Text = tempadd;
        //    }

        //    XmlNodeList xgender = xe.GetElementsByTagName("Gender");
        //    XmlNode xngender = xgender.Item(0);
        //    if (xngender != null)
        //    {
        //        gender = xngender.InnerText;
        //        if(gender=="1")
        //        {
        //            gender = "M";
        //        }
        //        else if (gender == "2")
        //        {
        //            gender = "F";
        //        }
        //        else
        //        {
        //            gender = "M";
        //        }
        //        lblgender.Text = gender;
        //    }
            if(valid=="True" || valid == "true")
            {
                autologin(uname, fname, lname);
            }
            else
            {
                if (Request.QueryString["internet"] != null && Request.QueryString["internet"] != "")
                {
                    if(Request.QueryString["internet"].ToString()=="true")
                    {
                        Response.Redirect(ConfigurationManager.AppSettings["internetURL"].ToString().Trim());
                    }
                    else
                    {
                        Response.Redirect(ConfigurationManager.AppSettings["intranetURL"].ToString().Trim());
                    }
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["intranetURL"].ToString().Trim());
                }
                lblerror.Visible = true;
            }
        //}
    }
    public void autologin(string strusername, string strfname, string strlname)
    {
        CheckBox Persist = new CheckBox();
        Persist.Checked = true;
        bool admin_flag = false;

        MembershipUser currentUser = Membership.GetUser(strusername);

        if (Membership.GetUser(strusername) != null)
        {
            string strpassword = Convert.ToString(currentUser.GetPassword());
            admin_flag = Membership.ValidateUser(strusername, strpassword);
            System.Web.HttpCookie cookie;
            cookie = FormsAuthentication.GetAuthCookie(strusername, false);
            cookie.Domain = "highbartech.com";
            //cookie.Expires = DateTime.Now.AddDays(-1);
            Response.AppendCookie(cookie);
            if (admin_flag == true)
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                DateTime dt;
                string dob1;
                if(dob!= "" && dob != null)
                {
                    dt = DateTime.Parse(dob, culture, System.Globalization.DateTimeStyles.AssumeLocal);
                    dob1 = dt.ToString();
                }
                else
                {
                    dob1 = "";
                }
                
                //classaddress.editssouser(email, fname, lname, mobile, dob1, gender, dept, desg);
                classaddress.updatessouser(uname, fname, lname, mobile, dob1, gender, dept, desg, extno, address, tempadd, email);
                CreateJso();
                DataSet ds_userdetails = classaddress.getalluserbyusername(uname.ToString().Trim());
                if (ds_userdetails.Tables.Count > 0)
                {
                    classaddress.insertupdatebirth(uname.ToString().Trim(), dob1.ToString().Trim(), "B");
                    DataTable dtuw = Classuserwidget.getuserwidget(uname.ToString().Trim());
                     if (dtuw.Rows.Count <= 0)
                     {
                         DataTable dtw = Classuserwidget.getwidget();
                         if (dtw.Rows.Count > 0)
                         {
                             for (int j = 0; j < dtw.Rows.Count; j++)
                             {
                                 int wid = Convert.ToInt32(dtw.Rows[j]["widget_id"]);
                                 Classuserwidget.insertUserwidget(wid, uname.ToString().Trim(), "T");
                             }
                         }
                     }
                    DataTable user2 = classreviews.getuseridbyemail(uname.ToString().Trim());
                    if (user2.Rows.Count > 0)
                    {
                        if (user2.Rows[0]["profilephoto"].ToString().Trim() == "noimage1.png" || user2.Rows[0]["profilephoto"].ToString().Trim() == "noimage3.jpg" || user2.Rows[0]["profilephoto"].ToString().Trim() == "null" || user2.Rows[0]["profilephoto"].ToString().Trim() == "")
                        {
                            if (gender.Trim() == "M")
                            {
                                classreviews.insertupdateprofilephoto(uname.ToString().Trim(), "noimage1.png");
                            }
                            else
                            {
                                classreviews.insertupdateprofilephoto(uname.ToString().Trim(), "noimage3.jpg");
                            }
                        }
                        // Online status
                        classaddress.UpdateUserStatus(uname.ToString().Trim(), 1);
                        //Department Insert
                        if (dept != "" && dept != null)
                        {
                            DataTable dtdept = classentity.getDepartmentByMatchName(dept);
                            if (dtdept.Rows.Count == 0)
                            {
                                int dpt = Convert.ToInt32(classentity.createDepartment(dept, 0));
                                if (dpt > 0)
                                {

                                }
                            }
                        }
                        // Designation
                        if (desg != "" && desg != null)
                        {
                            DataTable dtdesg = classentity.GetDesignationBymatchName(desg);
                            if (dtdesg.Rows.Count == 0)
                            {
                                int desg1 = Convert.ToInt32(classentity.createDesignation(desg));
                                if (desg1 > 0)
                                {

                                }
                            }
                        }
                    }
                }
                AuthenticateEventArgs w = new AuthenticateEventArgs();
                w.Authenticated = true;
                FormsAuthentication.RedirectFromLoginPage(strusername, false);
                Response.Redirect(ReturnUrl("sitepathmain"));
                //if (Session["referurl2"] != null)
                //{
                //    string str = Session["referurl2"].ToString();
                //    Response.Redirect(ReturnUrl("sitepathmain") + Session["referurl2"].ToString());
                //}
            }
            else
            {
                Response.Redirect(ReturnUrl("sitepathmain"));
            }
        }
        else
        {
            autoregister(strusername, strfname, strlname,mobile);
            AuthenticateEventArgs w = new AuthenticateEventArgs();
            w.Authenticated = true;
            FormsAuthentication.RedirectFromLoginPage(strusername, Persist.Checked);
            Response.Redirect(ReturnUrl("sitepathmain"));
            //if (Session["referurl2"] != null)
            //{
            //    string str = Session["referurl2"].ToString();
            //    Response.Redirect(ReturnUrl("sitepathmain") + Session["referurl2"].ToString());
            //}
        }

    }
    public void autoregister(string strname, string strfname, string strlname,string strmobile)
    {
        try
        {
            CreateUserWizard cuw = new CreateUserWizard();
            cuw.Email = strname;

            bool flagrole = Roles.IsUserInRole(strname, "User");

            if (flagrole == false)
            {
                Roles.AddUserToRole(strname, "User");
            }


            ProfileCommon p = (ProfileCommon)ProfileCommon.Create(strname, true);
            p.FirstName = strfname;
            p.LastName = strlname;
            p.Address1 = "";

            DataTable dt = new DataTable();

            p.City = "";
            p.State = "";
            p.Country = "";
            p.countrycode = "";
            p.LandPhone = strmobile;
            p.MobileNo = strmobile;
            p.PinCode = "";
            p.addsource = "";
            p.Mailings = false;
            p.AssignedPoints = "";
            p.Save();
            string strpass = GenerateRandomCode();
            MembershipUser newuser = Membership.CreateUser(strname, strpass, strname);
            //bool bln = classaddress.createaddress(strname, strfname, strlname, strname, "", "", "", "", "", "", strmobile, strmobile, ' ', ' ', "", "", "", "");
            bool bln = classaddress.createaddressregister(strname, strfname, strlname, address, strmobile, 0, 0, 0, "", "", strmobile, dob, gender, "", dept, "", desg, extno, tempadd, email);
            CreateJso();
            DataSet ds_userdetails = classaddress.getalluserbyusername(strname.ToString().Trim());
            if(ds_userdetails.Tables.Count>0)
            {
                if(ds_userdetails.Tables[0].Rows.Count > 0)
                {
                    classaddress.insertupdatebirth(strname.ToString().Trim(), dob, "B");
                    if (gender == "M")
                    {
                        classreviews.insertupdateprofilephoto(strname.ToString().Trim(), "noimage1.png");
                    }
                    else
                    {
                        classreviews.insertupdateprofilephoto(strname.ToString().Trim(), "noimage3.jpg");
                    }
                    // Set Widget Status
                    DataTable dtw = Classuserwidget.getwidget();
                    if (dtw.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtw.Rows.Count; j++)
                        {
                            int wid = Convert.ToInt32(dtw.Rows[j]["widget_id"]);
                            Classuserwidget.insertUserwidget(wid, strname.ToString().Trim(), "T");
                        }
                    }
                    // Online status
                    classaddress.UpdateUserStatus(strname.ToString().Trim(), 1);
                    //Insert Department
                    if (dept != "" && dept != null)
                    {
                        DataTable dtdept = classentity.getDepartmentByMatchName(dept);
                        if (dtdept.Rows.Count == 0)
                        {
                            int dpt = Convert.ToInt32(classentity.createDepartment(dept, 0));
                            if (dpt > 0)
                            {

                            }
                        }
                    }
                    //Insert Designation
                    if (desg != "" && desg != null)
                    {
                        DataTable dtdesg = classentity.GetDesignationBymatchName(desg);
                        if (dtdesg.Rows.Count == 0)
                        {
                            int desg1 = Convert.ToInt32(classentity.createDesignation(desg));
                            if (desg1 > 0)
                            {

                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string str = ex.Message.ToString();
            if (str.Contains("email address already in use"))
            {
                CheckBox Persist = new CheckBox();
                Persist.Checked = true;
                AuthenticateEventArgs w = new AuthenticateEventArgs();
                w.Authenticated = true;
                FormsAuthentication.RedirectFromLoginPage(strname, Persist.Checked);
            }
        }
    }
    private string GenerateRandomCode()
    {
        string s = "";
        for (int i = 0; i < 6; i++)
            s = String.Concat(s, this.random.Next(10).ToString());
        return s;
    }

public static string CallWebService()
{
    try
    {
        var _url = urlpath;
        var _action = action;

        XmlDocument soapEnvelopeXml = CreateSoapEnvelope();
        HttpWebRequest webRequest = CreateWebRequest(_url, _action);
        InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

        // begin async call to web request.
        IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

        // suspend this thread until call is complete. You might want to
        // do something usefull here like update your UI.
        asyncResult.AsyncWaitHandle.WaitOne();

        // get the response from the completed web request.
        using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
        {
            using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
            {
                soapResult = rd.ReadToEnd();
            }
            Console.Write(soapResult);
        }
        return soapResult;
    }
    catch (WebException webex)
    {
        WebResponse errResp = webex.Response;
        using (Stream respStream = errResp.GetResponseStream())
        {
            StreamReader reader = new StreamReader(respStream);
            string text = reader.ReadToEnd();
        }
        return soapResult;
    }
}

private static HttpWebRequest CreateWebRequest(string url, string action)
{
    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
    webRequest.Headers.Add("SOAPAction", action);
    webRequest.ContentType = "text/xml;charset=\"utf-8\"";
    webRequest.Accept = "text/xml";
    webRequest.Method = "POST";
    return webRequest;
}

private static XmlDocument CreateSoapEnvelope()
{
    XmlDocument soapEnvelop = new XmlDocument();
    var _mpath = mainpath;
    soapEnvelop.LoadXml(@"<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/""><soap:Header><SSOAuthServiceUser xmlns=""http://portal.highbartech.com/""><UserName>SSOAuthServiceUser</UserName><Password>zyAuthu53r@123</Password></SSOAuthServiceUser></soap:Header><soap:Body><ValidateSSOKeyForIntranet xmlns=""http://portal.highbartech.com/""><UserName>" + id + @"</UserName><SSOKey>" + ssokey + "</SSOKey></ValidateSSOKeyForIntranet></soap:Body></soap:Envelope>");
    return soapEnvelop;
}

private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
{
    using (Stream stream = webRequest.GetRequestStream())
    {
        soapEnvelopeXml.Save(stream);
    }
}

public void CreateJso()
{
    Employee employee;
    List<Employee> emp = new List<Employee>();

    DataTable members = classaddress.getalluser();
    if (members.Rows.Count > 0)
    {
        int m = 0;
        for (int i = 0; i < members.Rows.Count; i++)
        {
            m += 1;
            employee = new Employee
            {
                Id = members.Rows[i]["indexid"].ToString(),
                Name = members.Rows[i]["fullname"].ToString() + " (" + members.Rows[i]["username"].ToString() + ")",
                Email = members.Rows[i]["username"].ToString(),
                continent = m.ToString()
            };
            emp.Add(employee);
            if (m == 5)
            {
                m = 0;
            }
        }
    }
    var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    string jsonString = javaScriptSerializer.Serialize(emp);
    //Response.Write(jsonString);
    string path = Server.MapPath("~/data/");
    // Write that JSON to txt file,  
    System.IO.File.WriteAllText(path + "users.json", jsonString);
}
}