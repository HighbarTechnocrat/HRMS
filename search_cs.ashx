<%@ WebHandler Language="C#" Class="search_cs" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public class search_cs : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string prefixtext = context.Request.QueryString["q"];
        if (prefixtext.Length >= 1)
        {
            DataSet ds = classproduct.getsearchlistbyDeviceName(prefixtext);
            StringBuilder sb = new StringBuilder();
            DataTableReader dtr = ds.Tables[0].CreateDataReader();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    while (dtr.Read())
                    {
                        sb.Append(dtr["Name"]);
                        sb.Append(Environment.NewLine);
                    }
                }
                //else
                //{
                //    sb.Append("No records found.");
                //    sb.Append(Environment.NewLine);
                //}
            }
            //else
            //{
            //    sb.Append("No records found.");
            //    sb.Append(Environment.NewLine);
            //}
            context.Response.Write(sb.ToString());
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}