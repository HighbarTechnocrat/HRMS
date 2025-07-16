using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SqlConnect
/// </summary>
public class SqlConnect
{
    public SqlConnection con;
    public SqlCommand cmd, cmd1, cmd2, cmd3, cmd4, cmd5, cmd6, cmd7, cmd8, cmd9, cmd10, cmd11,cmd12,cmd13,cmd14;
    public SqlDataAdapter adp, adp1, adp2, adp3, adp4, adp5, adp6, adp7, adp8, adp9,adp10,adp11;
    public DataSet ds;
	public SqlConnect()
	{
        con = new SqlConnection();
        cmd = new SqlCommand();
        cmd1 = new SqlCommand();
        cmd2 = new SqlCommand();
        cmd3 = new SqlCommand();
        cmd4 = new SqlCommand();
        cmd5 = new SqlCommand();
        cmd6 = new SqlCommand();
        cmd7 = new SqlCommand();
        cmd8 = new SqlCommand();
        cmd9 = new SqlCommand();
        cmd10 = new SqlCommand();
        cmd11 = new SqlCommand();
        cmd12 = new SqlCommand();
        cmd13 = new SqlCommand();
        cmd14 = new SqlCommand();
        adp = new SqlDataAdapter();
        adp1 = new SqlDataAdapter();
        adp2 = new SqlDataAdapter();
        adp3 = new SqlDataAdapter();
        adp4 = new SqlDataAdapter();
        adp5 = new SqlDataAdapter();
        adp6 = new SqlDataAdapter();
        adp7 = new SqlDataAdapter();
        adp8 = new SqlDataAdapter();
        adp9 = new SqlDataAdapter();
        adp10 = new SqlDataAdapter();
        adp11 = new SqlDataAdapter();
        ds = new DataSet();
	}
}