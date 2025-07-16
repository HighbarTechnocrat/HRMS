using System;
using System.Data;
using System.Data.Common;


public struct video
{
   
}

/// <summary>
/// Summary description for classvideo
/// </summary>
public class classvideo
{
	public classvideo()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    //new method on 11 sept for fetch embed video by id

    public static DataTable getmovideoembedbyid(int productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getvideoembedcodebyid";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
    
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }


    public static bool insertflagwatch(decimal productid,char flag, string username)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_addmoviewatchflag";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName ="@productid";
        param.Value = productid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName ="@flag";
        param.Value = flag;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName ="@useremail";
        param.Value = username;
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


    public static DataTable getwatchmoviecount(decimal productid,string username)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getuserwatchcount";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@useremail";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }


    public static DataTable getorderdetailsbyusername_customer(string parentflag, string username,string productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getallpackagedetailscount";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@searchby";
        param.Value = parentflag;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@searchbytext";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@searchprdid";
        param.Value = productid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }


}