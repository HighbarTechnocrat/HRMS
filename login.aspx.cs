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

public partial class login : System.Web.UI.Page
{
	SP_Methods adm = new SP_Methods();
	public string username = "";
	public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
	protected void Page_Load(object sender, EventArgs e)
	{
		//Response.Redirect("http://localhost/hrms/undermaintain.aspx");
	}

	protected void SubmitButton_Click(object sender, EventArgs e)
	{
        SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconnection"].ToString());
		try
		{
        	#region Check Login Employee Is Abscond
            SqlParameter[] spars_1 = new SqlParameter[2];
            spars_1[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars_1[0].Value = "get_IsEmployee_Abscond";

            spars_1[1] = new SqlParameter("@Emp_Emailaddress", SqlDbType.VarChar);
            spars_1[1].Value = Convert.ToString(Request.Form["email"]).Trim();
            DataTable dtAbsocnd = adm.getDropdownList(spars_1, "SP_Employee_Abscond_Admin");

            if (dtAbsocnd.Rows.Count > 0)
            {
                lblmsg.Text = "Your OneHR Login is suspended. Please contact HR!";
                return;
            }

            #endregion

			string search_str = "";
			Int32 ichk = 0;
			string chekApprUrl = "";
			SqlParameter[] spars = new SqlParameter[3];

			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = "Validate_Salt";
			string username = String.Format("{0}", Request.Form["email"]);
			string userpwd = String.Format("{0}", Request.Form["password"]);
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

			DataTable dt = adm.getDropdownList(spars, "SP_Admin_Validate_User");

			if (dt.Rows.Count > 0)
			{
				string dbPassword = Convert.ToString(dt.Rows[0]["Pwd"]);
				string dbUserGuid = Convert.ToString(dt.Rows[0]["UserGuid"]);

				string hashedPassword = HashSHA1(userpwd + dbUserGuid);

				if (dbPassword == hashedPassword)
				{
					Session["Empcode"] = dt.Rows[0]["Emp_Code"].ToString();
					//Session["LoginEmpmail"] = Convert.ToString(UserName.Text).Trim();
					Session["LoginEmpmail"] = dt.Rows[0]["Emp_Emailaddress"].ToString();
					Session["emp_loginName"] = dt.Rows[0]["Emp_Name"].ToString();
                    Session["IsActualLastWorkingDt"] = dt.Rows[0]["Emp_Name"].ToString();


                    //Response.Redirect("http://localhost/hrms/Default.aspx");
                    chekApprUrl = HttpContext.Current.Request.Url.AbsoluteUri;
					string EmpStatus = Convert.ToString(dt.Rows[0]["emp_status"]);
					if (EmpStatus == "Resigned")
					{
						Session["EmpStatus"] = "Resigned";
					}
					if (chekApprUrl.Contains("ReturnUrl"))
					{
						string[] str = chekApprUrl.Split('=');
						Int32 tt = chekApprUrl.IndexOf("=");
						// Get first three characters.
						ichk = Convert.ToInt32(chekApprUrl.Length);
						string aaprURL = chekApprUrl.Substring(tt + 1);
						Session["aprurl"] = aaprURL;
					}
					string duser = dt.Rows[0]["Emp_Emailaddress"].ToString();
					creative.Common cmn = new creative.Common();

					string email = "";
					//email = cmn.getADUserDetaildByName(UrlRewritingVM.Decrypt(duser));
					email = cmn.getADUserDetaildByName_Portal(duser);

					DataTable dtUserlogin = adm.getuserinfodetails(email);

					if (dtUserlogin.Rows.Count > 0)
					{
						if (Convert.ToString(dtUserlogin.Rows[0]["errmsg"]).Trim() == "")
						{
							Session["Empcode"] = dtUserlogin.Rows[0]["Emp_Code"].ToString();
							//Session["LoginEmpmail"] = Convert.ToString(UserName.Text).Trim();
							Session["LoginEmpmail"] = dtUserlogin.Rows[0]["Emp_Emailaddress"].ToString();
							Session["emp_loginName"] = dtUserlogin.Rows[0]["Emp_Name"].ToString();
							//m_valid = true;
						}
					}

                  


                    #region Insert use log
                    int i = (int)Application["NoOfVisitors"];
					// if (i == 1)
					//{

					if (scon.State == ConnectionState.Closed || scon.State == ConnectionState.Broken)
						scon.Open();
					SqlCommand com = new SqlCommand();
					com = scon.CreateCommand();
					com.CommandType = CommandType.StoredProcedure;
					com.CommandText = "SP_insert_UserLog";
					com.Parameters.AddWithValue("@empcode", Session["Empcode"].ToString());
					com.Parameters.AddWithValue("@from_date", DateTime.Now.Date);
					com.Parameters.AddWithValue("@count", 1);
					com.ExecuteNonQuery();
                    scon.Close();    
					//}
					#endregion
					username = dt.Rows[0]["Emp_Emailaddress"].ToString();
                    //email = cmn.getADUserDetaildByName(UrlRewritingVM.Decrypt(username));

                    //Session["Empcode"] = "00002082";
                    //Session["LoginEmpmail"] = "anilkumar.jeur@highbartech.com";
                    //Session["emp_loginName"] = "Mr. Anilkumar Sangameshw Jeur";

                    if (email != null && email != "")
					{


						CheckBox Persist = new CheckBox();
						Persist.Checked = true;
						AuthenticateEventArgs w = new AuthenticateEventArgs();
						w.Authenticated = true;
						//Response.Write(UrlRewritingVM.Decrypt(username));
						// Response.End();

						FormsAuthentication.RedirectFromLoginPage(email, Persist.Checked);

						if (Convert.ToString(Session["aprurl"]).Trim() != "")
						{

							Response.Redirect(Convert.ToString(Session["aprurl"]).Replace("%2f", "/").Replace("%3f", "?").Replace("%3d", "=").Replace("%3a", ":").Trim(),false);
						}
						else
						{
							Response.Redirect("Default.aspx",false);
						}
					}
					//Response.Redirect("http://localhost/hrms/login_Working.aspx?user=" + Encrypt(dt.Rows[0]["Emp_Emailaddress"].ToString()));
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
			adm.sendMail("sada2703@gmail.com", "LoginButton", ex.Message.ToString(), "", "");
			throw;
		}
        finally
        {
            scon.Close();
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
			SP.sendMail("sada2703@gmail.com", "HashSHA1", ex.Message.ToString(), "", "");
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
			SP.sendMail("sada2703@gmail.com", "Encrypt", ex.Message.ToString(), "", "");
		}

		return clearText;
	}
}