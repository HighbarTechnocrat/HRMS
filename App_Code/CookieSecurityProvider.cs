//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

///// <summary>
///// Summary description for CookieSecurityProvider
///// </summary>
//public class CookieSecurityProvider
//{
//    public CookieSecurityProvider()
//    {
//        //
//        // TODO: Add constructor logic here
//        //
//    }
//}



using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

/// <summary>
/// Summary description for CookieSecurityProvider
/// </summary>
public class CookieSecurityProvider
{
    private static MethodInfo _encode;
    private static MethodInfo _decode;
    // CookieProtection.All enables 'temper proffing' and 'encryption' for cookie
    private static CookieProtection _cookieProtection = CookieProtection.All;

    //Static constructor to get reference of Encode and Decode methods of Class CookieProtectionHelper
    //using Reflection.
    static CookieSecurityProvider()
    {
        Assembly systemWeb = typeof(HttpContext).Assembly;
        Type cookieProtectionHelper = systemWeb.GetType("System.Web.Security.CookieProtectionHelper");
        _encode = cookieProtectionHelper.GetMethod("Encode", BindingFlags.NonPublic | BindingFlags.Static);
        _decode = cookieProtectionHelper.GetMethod("Decode", BindingFlags.NonPublic | BindingFlags.Static);
    }

    public static HttpCookie Encrypt(HttpCookie httpCookie)
    {
        byte[] buffer = Encoding.Default.GetBytes(httpCookie.Value);

        //Referencing the Encode mehod of CookieProtectionHelper class
        httpCookie.Value = (string)_encode.Invoke(null, new object[] { _cookieProtection, buffer, buffer.Length });
        return httpCookie;
    }

    public static HttpCookie Decrypt(HttpCookie httpCookie)
    {
        //Referencing the Decode mehod of CookieProtectionHelper class
        byte[] buffer = (byte[])_decode.Invoke(null, new object[] { _cookieProtection, httpCookie.Value });
        httpCookie.Value = Encoding.Default.GetString(buffer, 0, buffer.Length);
        return httpCookie;
    }


    public static string Encode(string value, Byte[] key, string AlgorithmName)
    {
        // Convert string data to byte array  
        byte[] ClearData = System.Text.Encoding.UTF8.GetBytes(value);

        // Now create the algorithm from the provided name  
        SymmetricAlgorithm Algorithm = SymmetricAlgorithm.Create(AlgorithmName);
        Algorithm.Key = key; //you can apply your own key mechs here  
        MemoryStream Target = new MemoryStream();

        // Generate a random initialization vector (IV), helps to prevent brute-force  
        Algorithm.GenerateIV();
        Target.Write(Algorithm.IV, 0, Algorithm.IV.Length);

        // Encrypt information  
        CryptoStream cs = new CryptoStream(Target, Algorithm.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(ClearData, 0, ClearData.Length);
        cs.FlushFinalBlock();

        // Convert the encrypted stream back to string  
        return Convert.ToBase64String(Target.GetBuffer(), 0, (int)Target.Length);
    }

    public static string Decode(string value, Byte[] key, string AlgorithmName)
    {
        // Convert string data to byte array  
        byte[] ClearData = Convert.FromBase64String(value);

        // Create the algorithm  
        SymmetricAlgorithm Algorithm = SymmetricAlgorithm.Create(AlgorithmName);
        Algorithm.Key = key;
        MemoryStream Target = new MemoryStream();

        // Read IV and initialize the algorithm with it  
        int ReadPos = 0;
        byte[] IV = new byte[Algorithm.IV.Length];
        Array.Copy(ClearData, IV, IV.Length);
        Algorithm.IV = IV;
        ReadPos += Algorithm.IV.Length;

        // Decrypt information  
        CryptoStream cs = new CryptoStream(Target, Algorithm.CreateDecryptor(), CryptoStreamMode.Write);
        cs.Write(ClearData, ReadPos, ClearData.Length - ReadPos);
        cs.FlushFinalBlock();

        // Get the bytes from the memory stream and convert them to text  
        return System.Text.Encoding.UTF8.GetString(Target.ToArray());
    }
    public static HttpCookie EncodeCookie(HttpCookie cookie)
    {
        if (cookie == null) return null;

        //get key and algorithm from web.config/appsettings    
        //Comment this section if you do now what this method to be static    
        char[] chars = { ',' };
        string[] splits = ConfigurationManager.AppSettings["CookieEncodingKey"].Split(chars);
        string AlgorithmName = splits[0];
        byte[] Key = new byte[Int32.Parse(splits[1])];// = KEY_64;    
        for (int i = 2; i < Key.Length + 2; i++)
            Key[i - 2] = byte.Parse(splits[i].Trim());

        HttpCookie eCookie = new HttpCookie(cookie.Name);
        eCookie.Expires = cookie.Expires;

        for (int i = 0; i < cookie.Values.Count; i++)
        {
            string value = HttpContext.Current.Server.UrlEncode(Encode(cookie.Values[i], Key, AlgorithmName));
            string name = HttpContext.Current.Server.UrlEncode(Encode(cookie.Values.GetKey(i), Key, AlgorithmName));
            eCookie.Values.Set(name, value);
        }
        return eCookie;
    }

    public static HttpCookie DecodeCookie(HttpCookie cookie)
    {
        if (cookie == null) return null;

        //Comment this section if you do now what this method to be static    
        char[] chars = { ',' };
        string[] splits = ConfigurationManager.AppSettings["CookieEncodingKey"].Split(chars);
        string AlgorithmName = splits[0];
        byte[] Key = new byte[Int32.Parse(splits[1])];// = KEY_64;    
        for (int i = 2; i < Key.Length + 2; i++)
            Key[i - 2] = byte.Parse(splits[i].Trim());

        HttpCookie dCookie = new HttpCookie(cookie.Name);
        dCookie.Expires = cookie.Expires;

        for (int i = 0; i < cookie.Values.Count; i++)
        {
            string value = Decode(HttpContext.Current.Server.UrlDecode(cookie.Values[i]), Key, AlgorithmName);
            string name = Decode(HttpContext.Current.Server.UrlDecode(cookie.Values.GetKey(i)), Key, AlgorithmName);
            dCookie.Values.Set(name, value);
        }
        return dCookie;
    }
}