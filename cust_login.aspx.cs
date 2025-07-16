using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

public partial class cust_login : System.Web.UI.Page
{
	SP_Methods adm = new SP_Methods();
	public string username = "";
	public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
	protected void Page_Load(object sender, EventArgs e)
	{
        
        if (!IsPostBack)
        {
            this.Title = ConfigurationManager.AppSettings["PageTitle"];
        }

    }

	protected void SubmitButton_Click(object sender, EventArgs e)
	{
        //Session["Empcode"] = "00631295";
        //Session["LoginEmpmail"] = "sanjay.patil@highbartech.com";
        //Session["emp_loginName"] = "Sanjay Patil";

        //Session["CustCode"] = "CS101";
        //Session["CustEmailaddress"] = "patilsanjay84@rediffmail.com";
        //Session["custName"] ="Sanjay";
        //Response.Redirect("procs/Custs_MyServiceReq.aspx", false);
        try
		{   
			Int32 ichk = 0;
			string chekApprUrl = "";
			SqlParameter[] spars = new SqlParameter[3];

			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = "Validate_Salt";
			string username = String.Format("{0}", Request.Form["email"]);
            string userpwd = password.Text;  
			spars[1] = new SqlParameter("@SearchString", SqlDbType.VarChar);
			if (username.ToString() == "")
				spars[1].Value = DBNull.Value;
			else
				spars[1].Value = username.ToString();

			spars[2] = new SqlParameter("@Pwd", SqlDbType.NVarChar);
			if (userpwd.ToString() == "")
				spars[2].Value = DBNull.Value;
			else
				spars[2].Value = userpwd.ToString();

			DataTable dt = adm.getDropdownList(spars, "SP_Cust_Validate_User");

			if (dt.Rows.Count > 0)
			{
				string dbPassword = Convert.ToString(dt.Rows[0]["Pwd"]);
				string dbUserGuid = Convert.ToString(dt.Rows[0]["UserGuid"]);
				string hashedPassword = HashSHA1(userpwd + dbUserGuid);

				if (dbPassword == hashedPassword)
                {
                    Session["CustCode"] = dt.Rows[0]["cust_code"].ToString();
                    Session["CustEmailaddress"] = dt.Rows[0]["cust_emailaddress"].ToString();
                    Session["custName"] = dt.Rows[0]["cust_name"].ToString(); 

                    chekApprUrl = HttpContext.Current.Request.Url.AbsoluteUri;

					if (chekApprUrl.Contains("ReturnUrl"))
					{
						string[] str = chekApprUrl.Split('=');
						Int32 tt = chekApprUrl.IndexOf("=");					
						ichk = Convert.ToInt32(chekApprUrl.Length);
						string aaprURL = chekApprUrl.Substring(tt + 1);
						Session["custaprurl"] = aaprURL;
					}
                    string email = "";

                    #region Code comment by Sanjay no use
                    /*string duser = dt.Rows[0]["cust_emailaddress"].ToString();
					creative.Common cmn = new creative.Common();					 				
					email = cmn.getADUserDetaildByName_Portal(duser);
					DataTable dtUserlogin = adm.getuserinfodetails(email);

					if (dtUserlogin.Rows.Count > 0)
					{
						if (Convert.ToString(dtUserlogin.Rows[0]["errmsg"]).Trim() == "")
						{
							Session["Empcode"] = dtUserlogin.Rows[0]["Emp_Code"].ToString();							
							Session["LoginEmpmail"] = dtUserlogin.Rows[0]["Emp_Emailaddress"].ToString();
							Session["emp_loginName"] = dtUserlogin.Rows[0]["Emp_Name"].ToString();
							 
						}
					}*/
                    #endregion

                    //username = dt.Rows[0]["cust_emailaddress"].ToString();
                    //email = cmn.getADUserDetaildByName(UrlRewritingVM.Decrypt(username));
                    email = dt.Rows[0]["cust_emailaddress"].ToString();

                    if (email != null && email != "")
					{
                        CheckBox Persist = new CheckBox();
                        Persist.Checked = true;
                        AuthenticateEventArgs w = new AuthenticateEventArgs();
                        w.Authenticated = true;
                        //Response.Write(UrlRewritingVM.Decrypt(username));
                        //Response.End();

                        FormsAuthentication.RedirectFromLoginPage(email, Persist.Checked);

                        if (Convert.ToString(Session["custaprurl"]).Trim() != "")
                        {

                            Response.Redirect(Convert.ToString(Session["custaprurl"]).Replace("%2f", "/").Replace("%3f", "?").Replace("%3d", "=").Replace("%3a", ":").Trim());
                        }
                        else
                        {
                            Response.Redirect("procs/Custs_MyServiceReq.aspx", false);
                        }
                    }
					
				}
				else
				{
					lblmsg.Text = "Invalid Login Credentials!";
				}

			}
			else
			{
				lblmsg.Text = "Invalid Login Credentials!";
			}
		}
		catch (Exception ex)
		{
			adm.sendMail("sanjay.patil@highbartech.com", "LoginButton", ex.Message.ToString(), "", "");
			throw;
		}
	}


	public static string HashSHA1(string value)
	{
		var sb = new StringBuilder();
		SP_Methods SP = new SP_Methods();
		try
		{
			var sha1 = System.Security.Cryptography.SHA1.Create();
			var inputBytes = Encoding.ASCII.GetBytes(value);
			var hash = sha1.ComputeHash(inputBytes);

			 sb = new StringBuilder();
			for (var i = 0; i < hash.Length; i++)
			{
				sb.Append(hash[i].ToString("X2"));
			}
		}
		catch (Exception ex)
		{
			SP.sendMail("bharat.mainkar@highbartech.com", "HashSHA1", ex.Message.ToString(), "", "");
		}
		return sb.ToString();
	}

	public static string Encrypt(string clearText)
	{
		SP_Methods SP = new SP_Methods();
		try
		{

			//string EncryptionKey = exp.key;
			string EncryptionKey = "c4a5i4e2e6m1l9";

			byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
			using (Aes encryptor = Aes.Create())
			{
				//encryptor.
				Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
				encryptor.Key = pdb.GetBytes(32);
				encryptor.IV = pdb.GetBytes(16);
				using (MemoryStream ms = new MemoryStream())
				{
					using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cs.Write(clearBytes, 0, clearBytes.Length);
						cs.Close();
					}
					clearText = Convert.ToBase64String(ms.ToArray());
				}
			}
			clearText = clearText.Replace("+", "-").Replace("/", "_");
		}
		catch (Exception ex)
		{
			SP.sendMail("bharat.mainkar@highbartech.com", "Encrypt", ex.Message.ToString(), "", "");
		}

		return clearText;
	}
}