using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections.Generic;

/// <summary>
/// Summary description for NVPAPICaller
/// </summary>
public class NVPAPICaller
{
    //private static readonly ILog log = LogManager.GetLogger(typeof(NVPAPICaller));
	
    private string pendpointurl = "https://api-3t.paypal.com/nvp";
    private const string CVV2 = "CVV2";

    //Flag that determines the PayPal environment (live or sandbox)
    private const bool bSandbox = false;

    private const string SIGNATURE = "SIGNATURE";
    private const string PWD = "PWD";
    private const string ACCT = "ACCT";

    //Replace <API_USERNAME> with your API Username
    //Replace <API_PASSWORD> with your API Password
    //Replace <API_SIGNATURE> with your Signature
    public string APIUsername = "abhay_api1.indiemuviz.com";
    private string APIPassword = "TEE3CS9RJXM2F9AV";
    private string APISignature = "AiPC9BjkCyDFQXbSkoZcgqH3hpacAnaRXHfeYtGV4YFP0VV1U7WTqW6f";
    private string Subject = "";
	private string BNCode = "PP-ECWizard";
    string sitepath = creativeconfiguration.SitePathMain;
    //HttpWebRequest Timeout specified in milliseconds 
    private const int Timeout = 5000;
    private static readonly string[] SECURED_NVPS = new string[] { ACCT, CVV2, SIGNATURE, PWD };


    /// <summary>
    /// Sets the API Credentials
    /// </summary>
    /// <param name="Userid"></param>
    /// <param name="Pwd"></param>
    /// <param name="Signature"></param>
    /// <returns></returns>
    public void SetCredentials(string Userid, string Pwd, string Signature)
    {
        APIUsername = Userid;
        APIPassword = Pwd;
        APISignature = Signature;
    }

    
    public bool ShortcutExpressCheckout(string amt,string subtotal, ref string token, ref string retMsg)
    {
		string host = "www.paypal.com";
		if (bSandbox)
		{
			pendpointurl = "https://api-3t.sandbox.paypal.com/nvp";
			host = "www.sandbox.paypal.com";
		}
		
        string returnURL = sitepath+"response.aspx";
        string cancelURL = sitepath+"cancelresponse.aspx";

        NVPCodec encoder = new NVPCodec();
        encoder["METHOD"] = "SetExpressCheckout";
        encoder["RETURNURL"] = returnURL;
        encoder["CANCELURL"] = cancelURL;
        encoder["PAYMENTREQUEST_0_AMT"] = amt;
        encoder["PAYMENTREQUEST_0_ITEMAMT"] = subtotal;
        encoder["PAYMENTREQUEST_0_PAYMENTACTION"] = "Sale";
        encoder["PAYMENTREQUEST_0_CURRENCYCODE"] = "USD";
        encoder["L_BILLINGTYPE0"] = "RecurringPayments";
        encoder["L_BILLINGAGREEMENTDESCRIPTION0"] = "MovieMembership";

        string pStrrequestforNvp = encoder.Encode();
        string pStresponsenvp = HttpCall(pStrrequestforNvp);

        NVPCodec decoder = new NVPCodec();
        decoder.Decode(pStresponsenvp);

        string strAck = decoder["ACK"].ToLower();
        if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
        {
            token = decoder["TOKEN"];

            string ECURL = "https://" + host + "/cgi-bin/webscr?cmd=_express-checkout" + "&token=" + token;

            retMsg = ECURL;
            return true;
        }
        else
        {
            retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                "Desc2=" + decoder["L_LONGMESSAGE0"];

            return false;
        }
    }
   
    public bool ShortcutExpressCheckout(string amt,string subtotal,string shiptotal, ref string token, ref string retMsg, List<KeyValuePair<string, string>> customParams)
    {
        string host = "www.paypal.com";
        if (bSandbox)
        {
            pendpointurl = "https://api-3t.sandbox.paypal.com/nvp";
            host = "www.sandbox.paypal.com";
        }

        string returnURL = sitepath + "response.aspx";
        string cancelURL = sitepath + "cancelresponse.aspx";

        NVPCodec encoder = new NVPCodec();

        encoder["METHOD"] = "SetExpressCheckout";
        encoder["RETURNURL"] = returnURL;
        encoder["CANCELURL"] = cancelURL;
        encoder["PAYMENTREQUEST_0_AMT"] = amt;
        encoder["PAYMENTREQUEST_0_ITEMAMT"] = subtotal;
        encoder["PAYMENTREQUEST_0_SHIPPINGAMT"] = shiptotal;
        //encoder["AMT"] = amt;
        encoder["PAYMENTREQUEST_0_PAYMENTACTION"] = "Sale";
        encoder["PAYMENTREQUEST_0_CURRENCYCODE"] = "USD";
        encoder["NOSHIPPING"] = "1";
        encoder["L_BILLINGTYPE0"] = "RecurringPayments";
        encoder["L_BILLINGAGREEMENTDESCRIPTION0"] = "MovieMembership";

        // for custom parameters       
        if (customParams != null)
        {
            customParams.ForEach(kvp => encoder[kvp.Key] = kvp.Value);
        }


        // encoder["PAYMENTREQUEST_0_PAYMENTACTION"] = "Sale";
        //// encoder["L_PAYMENTREQUEST_0_NAME0"] = "10% Decaf Kona Blend Coffee";
        // //encoder["L_PAYMENTREQUEST_0_NUMBER0"] = "623083";
        // //encoder["L_PAYMENTREQUEST_0_DESC0"] = "Size: 8.8-oz";
        // //encoder["L_PAYMENTREQUEST_0_AMT0"] = "40.00";
        // //encoder["L_PAYMENTREQUEST_0_QTY0"] = "2";
        // encoder["PAYMENTREQUEST_0_AMT"] = "80.00";
        // encoder["PAYMENTREQUEST_0_CURRENCYCODE"] = "USD";
        // encoder["ALLOWNOTE"] = "1";



        string pStrrequestforNvp = encoder.Encode();
        string pStresponsenvp = HttpCall(pStrrequestforNvp);

        NVPCodec decoder = new NVPCodec();
        decoder.Decode(pStresponsenvp);

        string strAck = decoder["ACK"].ToLower();
        if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
        {
            token = decoder["TOKEN"];

            string ECURL = "https://" + host + "/cgi-bin/webscr?cmd=_express-checkout" + "&token=" + token;

            retMsg = ECURL;
            return true;
        }
        else
        {
            retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" + "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" + "Desc2=" + decoder["L_LONGMESSAGE0"];
            return false;
        }
    }
    /// <summary>
    /// MarkExpressCheckout: The method that calls SetExpressCheckout API, invoked from the 
    /// Billing Page EC placement
    /// </summary>
    /// <param name="amt"></param>
    /// <param ref name="token"></param>
    /// <param ref name="retMsg"></param>
    /// <returns></returns>
    public bool MarkExpressCheckout(string amt, 
                        string shipToName, string shipToStreet, string shipToStreet2,
                        string shipToCity, string shipToState, string shipToZip, 
                        string shipToCountryCode,ref string token, ref string retMsg)
    {
		string host = "www.paypal.com";
		if (bSandbox)
		{
			pendpointurl = "https://api-3t.sandbox.paypal.com/nvp";
			host = "www.sandbox.paypal.com";
		}

        string returnURL = sitepath + "response.aspx";
        string cancelURL = sitepath + "cancelresponse.aspx";


        NVPCodec encoder = new NVPCodec();
        encoder["METHOD"] = "SetExpressCheckout";
        encoder["RETURNURL"] = returnURL;
        encoder["CANCELURL"] = cancelURL;
        encoder["PAYMENTREQUEST_0_AMT"] = amt;
        encoder["PAYMENTREQUEST_0_PAYMENTACTION"] = "Sale";
        encoder["PAYMENTREQUEST_0_CURRENCYCODE"]  = "USD";

        //Optional Shipping Address entered on the merchant site
        encoder["PAYMENTREQUEST_0_SHIPTONAME"]       = shipToName;
        encoder["PAYMENTREQUEST_0_SHIPTOSTREET"]     = shipToStreet;
        encoder["PAYMENTREQUEST_0_SHIPTOSTREET2"]    = shipToStreet2;
        encoder["PAYMENTREQUEST_0_SHIPTOCITY"]       = shipToCity;
        encoder["PAYMENTREQUEST_0_SHIPTOSTATE"]      = shipToState;
        encoder["PAYMENTREQUEST_0_SHIPTOZIP"]        = shipToZip;
        encoder["PAYMENTREQUEST_0_SHIPTOCOUNTRYCODE"]= shipToCountryCode;
	

        string pStrrequestforNvp = encoder.Encode();
        string pStresponsenvp = HttpCall(pStrrequestforNvp);

        NVPCodec decoder = new NVPCodec();
        decoder.Decode(pStresponsenvp);

        string strAck = decoder["ACK"].ToLower();
        if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
        {
            token = decoder["TOKEN"];

            string ECURL = "https://" + host + "/cgi-bin/webscr?cmd=_express-checkout" + "&token=" + token;

            retMsg = ECURL;
            return true;
        }
        else
        {
            retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                "Desc2=" + decoder["L_LONGMESSAGE0"];

            return false;
        }
    }


    /// <summary>
    /// GetShippingDetails: The method that calls SetExpressCheckout API, invoked from the 
    /// Billing Page EC placement
    /// </summary>
    /// <param name="token"></param>
    /// <param ref name="retMsg"></param>
    /// <returns></returns>
    public bool GetShippingDetails(string token, ref string PayerId, ref string ShippingAddress, ref string retMsg)
    {

		if (bSandbox)
		{
			pendpointurl = "https://api-3t.sandbox.paypal.com/nvp";
		}
		
        NVPCodec encoder = new NVPCodec();
        encoder["METHOD"] = "GetExpressCheckoutDetails";
        encoder["TOKEN"] = token;
        encoder["PAYERID"] = PayerId;
        string pStrrequestforNvp = encoder.Encode();
        string pStresponsenvp = HttpCall( pStrrequestforNvp );

        NVPCodec decoder = new NVPCodec();
        decoder.Decode( pStresponsenvp );

        string strAck = decoder["ACK"].ToLower();
        if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
        {
            ShippingAddress = "<table><tr>";
            ShippingAddress += "<td> First Name </td><td>" + decoder["FIRSTNAME"] + "</td></tr>";
            ShippingAddress += "<td> Last Name </td><td>" + decoder["LASTNAME"] + "</td></tr>";
            ShippingAddress += "<td colspan='2'> Shipping Address</td></tr>";
            ShippingAddress += "<td> Name </td><td>" + decoder["PAYMENTREQUEST_0_SHIPTONAME"] + "</td></tr>";
            ShippingAddress += "<td> Street1 </td><td>" + decoder["PAYMENTREQUEST_0_SHIPTOSTREET"] + "</td></tr>";
            ShippingAddress += "<td> Street2 </td><td>" + decoder["PAYMENTREQUEST_0_SHIPTOSTREET2"] + "</td></tr>";
            ShippingAddress += "<td> City </td><td>" + decoder["PAYMENTREQUEST_0_SHIPTOCITY"] + "</td></tr>";
            ShippingAddress += "<td> State </td><td>" + decoder["PAYMENTREQUEST_0_SHIPTOSTATE"] + "</td></tr>";
            ShippingAddress += "<td> Zip </td><td>" + decoder["PAYMENTREQUEST_0_SHIPTOZIP"] + "</td>";
            ShippingAddress += "</tr>";

            return true;
        }
        else
        {
            retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                "Desc2=" + decoder["L_LONGMESSAGE0"];

            return false;
        }
    }


    /// <summary>
    /// ConfirmPayment: The method that calls SetExpressCheckout API, invoked from the 
    /// Billing Page EC placement
    /// </summary>
    /// <param name="token"></param>
    /// <param ref name="retMsg"></param>
    /// <returns></returns>
    public bool ConfirmPayment(string finalPaymentAmount, string token, string PayerId, ref NVPCodec decoder, ref string retMsg )
    {
		if (bSandbox)
		{
			pendpointurl = "https://api-3t.sandbox.paypal.com/nvp";
		}
		
        NVPCodec encoder = new NVPCodec();
        encoder["METHOD"] = "DoExpressCheckoutPayment";
        encoder["TOKEN"] = token;
        encoder["PAYMENTREQUEST_0_PAYMENTACTION"] = "Sale";
        encoder["PAYERID"] = PayerId;
        encoder["PAYMENTREQUEST_0_AMT"] = finalPaymentAmount;
       encoder["PAYMENTREQUEST_0_CURRENCYCODE"] = "USD";

        string pStrrequestforNvp = encoder.Encode();
        string pStresponsenvp = HttpCall(pStrrequestforNvp);

        decoder = new NVPCodec();
        decoder.Decode(pStresponsenvp);

        string strAck = decoder["ACK"].ToLower();
        if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
        {
            return true;
        }
        else
        {
            retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                "Desc2=" + decoder["L_LONGMESSAGE0"];

            return false;
        }
    }
    public bool CreateRecurringPaymentsProfileCode(ref string token, string amount, string profileDate, string billingPeriod, string billingFrequency, ref string retMsg)
    {
        if (bSandbox)
        {
            pendpointurl = "https://api-3t.sandbox.paypal.com/nvp";
        }

        NVPCodec encoder = new NVPCodec();

        // Add request-specific fields to the request.
        encoder["METHOD"] = "CreateRecurringPaymentsProfile";
        encoder["TOKEN"] = token;
        encoder["AMT"] = amount;
        encoder["CURRENCYCODE"] = "USD";
        encoder["PROFILESTARTDATE"] = profileDate; //Date format from server expects Ex: 2006-9-6T0:0:0
        encoder["BILLINGPERIOD"] = billingPeriod;
        encoder["BILLINGFREQUENCY"] = billingFrequency;
        encoder["DESC"] = "MovieMembership";

        // Execute the API operation and obtain the response.
        string pStrrequestforNvp = encoder.Encode();
        string pStresponsenvp = HttpCall(pStrrequestforNvp);

        NVPCodec decoder = new NVPCodec();
        decoder.Decode(pStresponsenvp);
        //return decoder["ACK"];
        string strAck = decoder["ACK"].ToLower();
        if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
        {
            retMsg = decoder["ProfileID"];

            return true;
        }
        else
        {
            retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                "Desc2=" + decoder["L_LONGMESSAGE0"];

            return false;
        }
    }



    /// <summary>
    /// Cancels a subscription
    /// </summary>
    /// <returns>Whether or not the action suceeded.</returns>
    public bool CancelSubscription(string profileId, string action,ref string retMsg)
    {
        if (bSandbox)
        {
            pendpointurl = "https://api-3t.sandbox.paypal.com/nvp";
        }

        NVPCodec encoder = new NVPCodec();
        encoder["METHOD"] ="ManageRecurringPaymentsProfileStatus";
        encoder["PROFILEID"] = profileId;
       // encoder["PROFILEID"] = profileId;
        encoder["NOTE"] = "Subscription cancelled";
        encoder["ACTION"] = action;

        string pStrrequestforNvp = encoder.Encode();
        string pStresponsenvp = HttpCall(pStrrequestforNvp);

        NVPCodec decoder = new NVPCodec();
        decoder.Decode(pStresponsenvp);
        string strAck = decoder["ACK"];

        if (strAck != null && (strAck == "Success" || strAck == "successwithwarning"))
        {
            return true;
        }
        else
        {
            retMsg = decoder[0] ?? string.Empty;
            return false;
        }
    }

    /// <summary>
    /// HttpCall: The main method that is used for all API calls
    /// </summary>
    /// <param name="NvpRequest"></param>
    /// <returns></returns>
    public string HttpCall(string NvpRequest) //CallNvpServer
    {
        string url = pendpointurl;

        //To Add the credentials from the profile
        string strPost = NvpRequest + "&" + buildCredentialsNVPString();
		strPost = strPost + "&BUTTONSOURCE=" + HttpUtility.UrlEncode( BNCode );

        HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
        objRequest.Timeout = Timeout;
        objRequest.Method = "POST";
        objRequest.ContentLength = strPost.Length;

        try
        {
            using (StreamWriter myWriter = new StreamWriter(objRequest.GetRequestStream()))
            {
                myWriter.Write(strPost);
            }
        }
        catch (Exception e)
        {
            /*
            if (log.IsFatalEnabled)
            {
                log.Fatal(e.Message, this);
            }*/
        }

        //Retrieve the Response returned from the NVP API call to PayPal
        HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
        string result;
        using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
        {
            result = sr.ReadToEnd();
        }

        //Logging the response of the transaction
        /* if (log.IsInfoEnabled)
         {
             log.Info("Result :" +
                       " Elapsed Time : " + (DateTime.Now - startDate).Milliseconds + " ms" +
                      result);
         }
         */
        return result;
    }

    /// <summary>
    /// Credentials added to the NVP string
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    private string buildCredentialsNVPString()
    {
        NVPCodec codec = new NVPCodec();

        if (!IsEmpty(APIUsername))
            codec["USER"] = APIUsername;

        if (!IsEmpty(APIPassword))
            codec[PWD] = APIPassword;

        if (!IsEmpty(APISignature))
            codec[SIGNATURE] = APISignature;

        if (!IsEmpty(Subject))
            codec["SUBJECT"] = Subject;

        codec["VERSION"] = "86.0";

        return codec.Encode();
    }

    /// <summary>
    /// Returns if a string is empty or null
    /// </summary>
    /// <param name="s">the string</param>
    /// <returns>true if the string is not null and is not empty or just whitespace</returns>
    public static bool IsEmpty(string s)
    {
        return s == null || s.Trim() == string.Empty;
    }

    
 
}

public sealed class NVPCodec : NameValueCollection
{
    private const string AMPERSAND = "&";
    private const string EQUALS = "=";
    private static readonly char[] AMPERSAND_CHAR_ARRAY = AMPERSAND.ToCharArray();
    private static readonly char[] EQUALS_CHAR_ARRAY = EQUALS.ToCharArray();

    /// <summary>
    /// Returns the built NVP string of all name/value pairs in the Hashtable
    /// </summary>
    /// <returns></returns>
    public string Encode()
    {
        StringBuilder sb = new StringBuilder();
        bool firstPair = true;
        foreach (string kv in AllKeys)
        {
            string name = HttpUtility.UrlEncode(kv);
            string value = HttpUtility.UrlEncode(this[kv]);
            if (!firstPair)
            {
                sb.Append(AMPERSAND);
            }
            sb.Append(name).Append(EQUALS).Append(value);
            firstPair = false;
        }
        return sb.ToString();
    }

    /// <summary>
    /// Decoding the string
    /// </summary>
    /// <param name="nvpstring"></param>
    public void Decode(string nvpstring)
    {
        Clear();
        foreach (string nvp in nvpstring.Split(AMPERSAND_CHAR_ARRAY))
        {
            string[] tokens = nvp.Split(EQUALS_CHAR_ARRAY);
            if (tokens.Length >= 2)
            {
                string name = HttpUtility.UrlDecode(tokens[0]);
                string value = HttpUtility.UrlDecode(tokens[1]);
                Add(name, value);
            }
        }
    }


    #region Array methods
    public void Add(string name, string value, int index)
    {
        this.Add(GetArrayName(index, name), value);
    }

    public void Remove(string arrayName, int index)
    {
        this.Remove(GetArrayName(index, arrayName));
    }

    /// <summary>
    /// 
    /// </summary>
    public string this[string name, int index]
    {
        get
        {
            return this[GetArrayName(index, name)];
        }
        set
        {
            this[GetArrayName(index, name)] = value;
        }
    }

    private static string GetArrayName(int index, string name)
    {
        if (index < 0)
        {
            throw new ArgumentOutOfRangeException("index", "index can not be negative : " + index);
        }
        return name + index;
    }
    #endregion


  

}
