using System;
using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for classpassbook
/// </summary>
public class classpassbook
{
	public classpassbook()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public static bool createmypassbook(string username, string campaign,int assignpts,string vouchername,decimal orderid, string voucherdate,string actiontaken)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertpoints_mypassbook";

        DbParameter param = comm.CreateParameter();
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@UserName";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName ="@Campaignname";
        param.Value = campaign;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName ="@Assignpoints";
        param.Value = assignpts;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName ="@vouchername";
        param.Value = vouchername;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName ="@orderid";
        param.Value = orderid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName ="@voucherdate";
        param.Value = voucherdate;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName ="@Actiontaken";
        param.Value = actiontaken;
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


    public static bool addassignpoints(string username, int assignpoints, string active_date)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertpoints_user_assignpoint";

        DbParameter param = comm.CreateParameter();
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@UserName";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Assignpoints";
        param.Value = assignpoints;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@active_date";
        param.Value = active_date;
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



    public static bool deleteassignpoints(string username)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deletepoints_user_assignpoint";

        DbParameter param = comm.CreateParameter();
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@UserName";
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
        return (result >= 1);
    }
}