using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Linq;
using System.Text;

public partial class ForgotPassword : System.Web.UI.Page
{
    SP_Methods adm = new SP_Methods();
    public string username = "";
    private static Random random = new Random();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string HashSHA1(string value)
    {
        var sha1 = System.Security.Cryptography.SHA1.Create();
        var inputBytes = Encoding.ASCII.GetBytes(value);
        var hash = sha1.ComputeHash(inputBytes);

        var sb = new StringBuilder();
        for (var i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        try
        {
        string search_str = "";
        if (email.Text.ToString() == "")
        {
            lblmsg.Text = "Please enter e-mail Id!";
            return;
        }
        Guid userGuid = System.Guid.NewGuid();
        string resetpwd = RandomString(8);
        // Hash the password together with our unique userGuid
        string hashedPassword = HashSHA1(resetpwd + userGuid.ToString());
        SqlParameter[] spars = new SqlParameter[4];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "Reset";

        spars[1] = new SqlParameter("@SearchString", SqlDbType.VarChar);
        if (email.Text.ToString() == "")
            spars[1].Value = DBNull.Value;
        else
            spars[1].Value = email.Text.ToString();

        spars[2] = new SqlParameter("@Pwd", SqlDbType.NVarChar);
        spars[2].Value = hashedPassword.ToString();

        spars[3] = new SqlParameter("@Guid", SqlDbType.UniqueIdentifier);
        spars[3].Value = userGuid;

        DataTable dt = adm.getDropdownList(spars, "SP_Admin_Validate_User");

        if (dt.Rows.Count > 0)
        {
            if (Convert.ToString(dt.Rows[0]["PWD"]) != "")
            {
                    //lblmsg.Text = Convert.ToString(dt.Rows[0]["msg"]) + " to " + resetpwd.ToString(); 
                    lblmsg.Text = Convert.ToString(dt.Rows[0]["msg"]);
                //adm.sendMail(email.Text.ToString(), Convert.ToString(dt.Rows[0]["msg"]), "Your New Password is: " + Convert.ToString(dt.Rows[0]["PWD"]) + " . Please Change Password on First Login!", "", "");
                adm.sendMail(email.Text.ToString(), "oneHR Portal Password Reset Request", "Your New Password is: " + resetpwd.ToString() + " . Please Change Password on First Login!", "", "");
            }
            else
            {
                lblmsg.Text = Convert.ToString(dt.Rows[0]["msg"]);
            }

        }
               }
        catch (Exception ex)
        {

        }
    }

    public static string Encrypt(string clearText)
    {
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
        catch (Exception)
        {

        }

        return clearText;
    }
}