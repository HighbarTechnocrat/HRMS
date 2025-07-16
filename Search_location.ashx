<%@ WebHandler Language="C#" Class="Search_City" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

public class Search_City : IHttpHandler {
    public void ProcessRequest (HttpContext context) {
        string prefixText = context.Request.QueryString["q"];
        using (SqlConnection conn = new SqlConnection())
        {
            if (prefixText.Length > 1)
            {
                StringBuilder sb = new StringBuilder();
                System.Data.DataTable dt_search = new System.Data.DataTable();
                dt_search = classentity.getentitybysearch(prefixText.ToString().Trim());
                if (dt_search.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_search.Rows.Count; i++)
                    {
                        sb.Append(dt_search.Rows[i]["entity_name"].ToString());
                        sb.Append(Environment.NewLine);
                    }
                }                           
                context.Response.Write(sb.ToString());
            }
        }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }
}