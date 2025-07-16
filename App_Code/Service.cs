using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Services;

/// <summary>
/// Summary description for Service
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService {

    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //public string HelloWorld() {
    //    return "Hello World";
    //}

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetCustomers(string prefix)
    {
        List<string> customers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            List<string> terms = prefix.Split(',').ToList();
            terms = terms.Select(s => s.Trim()).ToList();

            //Extract the term to be searched from the list
            string searchTerm = terms.LastOrDefault().ToString().Trim();

            //Return if Search Term is empty
            if (string.IsNullOrEmpty(searchTerm))
            {
                return new string[0];
            }

            //Populate the terms that need to be filtered out
            List<string> excludeTerms = new List<string>();
            if (terms.Count > 1)
            {
                terms.RemoveAt(terms.Count - 1);
                excludeTerms = terms;
            }
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["GlobalConnection"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                string query = "select  firstname+' '+lastname as fullname, indexid from addressbook where"+" firstname like @SearchText+'%'";
                //Filter out the existing searched items
                if (excludeTerms.Count > 0)
                {
                    query += string.Format(" and firstname not in ({0})", string.Join(",", excludeTerms.Select(s => "'" + s + "'").ToArray()));
                }
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@SearchText", searchTerm);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["fullname"], sdr["indexid"]));
                    }
                    conn.Close();
                }
                return customers.ToArray();
            }
        }
    }
}
