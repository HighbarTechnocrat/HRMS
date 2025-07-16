using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for multivalueauto
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class multivalueauto : System.Web.Services.WebService {

    public multivalueauto () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
     [WebMethod]
        public List<string> GetCustomers(string prefix)
        {
            List<string> result = new List<string>();
            DataTable tblCustomer = classaddress.getallautocompletename(prefix); //( called to function );
            if(tblCustomer .Rows.Count > 0)
            {
                for (int i = 0; i < tblCustomer .Rows.Count; i++)
                {
                    result.Add(tblCustomer.Rows[i]["fullname"].ToString());
                }
            }
            return result;
        }
}
