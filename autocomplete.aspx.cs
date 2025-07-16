using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.IO;
using System.Data;

public partial class autocomplete : System.Web.UI.Page
{
    public class Employee
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string continent { get; set; }
    } 

     public string ReturnUrl(object path){string url = "";url = UrlRewritingVM.ChangeURL(path.ToString());return url;} protected void Page_Load(object sender, EventArgs e)
    {
        CreateJso();
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