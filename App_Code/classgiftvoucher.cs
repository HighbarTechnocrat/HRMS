using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;

public struct giftvoucher
{
    public int gvid;
    public string title;
    public string username;
    public DateTime postdate;
    public DateTime expirydate;
    public decimal  gvamount;
    public decimal  minpurchaseamt;
    public string gvstatus;
    public string gvcode;
    public string bannerimage;
    public string banneralt;
    public string description;    
    public string conditions;
    public string redeemedby;
    public DateTime redeemedon;
    public DateTime canceledon;
    
}

/// <summary>
/// Summary description for classgiftvoucher
/// </summary>
public class classgiftvoucher
{
	public classgiftvoucher()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public static bool creategiftvoucher(string title, string username, DateTime expirydate, decimal gvamount, decimal minpurchaseamt, string gvstatus, string gvcode, string gvpassword, string bannerimage, string banneralt, string description, string conditions,char flag)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText ="sp_proc_insertupdategiftvoucher";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName ="@title";
        param.Value = title;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName ="@username";
        param.Value =username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName ="@expirydate";
        param.Value = expirydate;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@gvamount";
        param.Value = gvamount;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName ="@minpurchaseamt";
        param.Value = minpurchaseamt;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName ="@gvstatus";
        param.Value = gvstatus;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName ="@gvcode";
        param.Value = gvcode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@gvpassword";
        param.Value = gvpassword;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@bannerimage";
        param.Value = bannerimage;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@banneralt";
        param.Value = banneralt;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@description";
        param.Value = description;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@conditions";
        param.Value = conditions;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@gvflag";
        param.Value = flag;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch
        {
            // any errors are logged in GenericDataAccess, we ingore them here
        }
        // result will be 1 in case of success 
        return (result >= 1);
    }
  
   
    public static DataTable selectgiftvoucherdetls()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_selectgiftvoucherdetails";
        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;

    }

    public static DataTable get_reedamble_points()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_reedamble_points";
        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;

    }

    public static giftvoucher selectgiftvoucherdetails(int gvid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getgiftvoucherdtls";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@gvid";
        param.Value = gvid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        //return GenericDataAccess.ExecuteSelectCommand(comm);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // wrap retrieved data into a ItemDetails object
        giftvoucher details = new giftvoucher();
        if (table.Rows.Count > 0)
        {
            // get the first table row
            DataRow dr = table.Rows[0];
            // get product details
            details.gvid= Convert.ToInt32(dr["gvid"]);
            details.title = Convert.ToString(dr["title"]);
            details.gvamount = Convert.ToUInt32(dr["gvamount"]);
            details.minpurchaseamt = Convert.ToUInt32(dr["minpurchaseamt"]);
            details.expirydate = Convert.ToDateTime(dr["expirydate"]);
            details.description = Convert.ToString(dr["description"]);
            details.conditions = Convert.ToString(dr["conditions"]);
        }
        //return department details
        return details;
    }

   
    //method on 17th oct
    public static DataTable get_giftvoucher_user(string username,decimal amt)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_create_getgiftvoucher";
        // execute the stored procedure and save the results in a DataTable
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@uesrname";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@amt";
        param.Value = amt;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;

    }

    public static DataTable get_giftvoucher_rewardpoint(decimal rewardpoint)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_giftvoucherRangeFrompoint";
        // execute the stored procedure and save the results in a DataTable
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@rewardpoint";
        param.Value = rewardpoint;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;

    }

    public static DataTable get_giftvoucher_validation(string username, string gvcode, string gvpass,decimal amt)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_checkgiftvoucherpointvalidate";
        // execute the stored procedure and save the results in a DataTable
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@gvcode";
        param.Value = gvcode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@passwd";
        param.Value = gvpass;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@amt";
        param.Value = amt;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;

    }



    public static bool updategiftvoucherflag(string gusername, char gvflag, string gvid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_updategiftvoucheflag";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@gusername";
        param.Value = gusername;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        // create a new parameter

        param = comm.CreateParameter();
        param.ParameterName = "@flagv";
        param.Value = gvflag;
        param.DbType = DbType.String;
        //  param.Size = 1;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@gvid";
        param.Value = gvid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch
        {
            // any errors are logged in GenericDataAccess, we ingore them here
        }
        // result will be 1 in case of success 
        return (result != -1);
    }



    public static bool updategiftvoucherstatus_user(string gusername)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_updategiftvoucherstatus_user";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@gusername";
        param.Value = gusername;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
       
        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch
        {
            // any errors are logged in GenericDataAccess, we ingore them here
        }
        // result will be 1 in case of success 
        return (result != -1);
    }


    public static DataTable get_giftvoucheramtbygvid(int gvid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "get_gvamountbygvid";
        // execute the stored procedure and save the results in a DataTable
        DbParameter param = comm.CreateParameter();
        param.ParameterName ="@gvid";
        param.Value = gvid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;

    }

    /*add invoice sp*/

    public static DataTable Getgvidbyorderid(decimal Id)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getgvidbyorerid ";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@orderid";
        param.Value = Id;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable Getamtbygvid_giftvoucher(decimal Id)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getgvamtbygvid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@gvid";
        param.Value = Id;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }


}
