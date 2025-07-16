using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Xml;
using System.Collections.Generic;


/// <summary>
/// Summary description for AutoComplete
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//[WebService(Namespace = "http://developer.intuit.com/")]
[System.Web.Script.Services.ScriptService]


public class AutoComplete : System.Web.Services.WebService 
{
    public AutoComplete()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    
    
    [WebMethod]
    public string[] GetProductList(String prefixText)
    {

        DataSet Ds = classproduct.getsearchlistbyDeviceName(prefixText);
        string[] result = new string[Ds.Tables[0].Rows.Count];

  
        for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
        {
            result[i] = Ds.Tables[0].Rows[i]["Name"].ToString();
         

        }

        string[] NoresultFound = new string[1];
        NoresultFound[0] = "No Result Found";

        if (result.Length == 0)
        {
            return NoresultFound;
        }
        else
        {
            return result;
        }
       
    }
}