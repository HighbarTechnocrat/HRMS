<%@ WebHandler Language="C#" Class="Search_City" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

public class Search_City : IHttpHandler {
    public void ProcessRequest (HttpContext context) {
        string prefixText = context.Request.QueryString["q"];
        string search = context.Request.QueryString["d"];
        //string deptid = context.Request.QueryString["did"].ToString();
        using (SqlConnection conn = new SqlConnection())
        {
            if (prefixText.Length > 1)
            {
                StringBuilder sb = new StringBuilder();
                System.Data.DataTable dt_search = new System.Data.DataTable();
                dt_search = classentity.getSubdepartmentbyParentName(search.ToString().Trim(), prefixText.ToString().Trim());
                if (dt_search.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_search.Rows.Count; i++)
                    {
                        sb.Append(dt_search.Rows[i]["dept_name"].ToString());
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