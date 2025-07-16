using System;
using System.Data;
using System.Data.Common;
using System.Configuration;

/// <summary>
/// Summary description for classprofile
/// </summary>
public class classprofile
{
	public classprofile()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //added by sapna ranaware on 2013-08-22 18 fields
    public static bool createprofile(string username, string firstname, string lastname)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertprofile";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@firstname";
        param.Value = firstname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@lastname";
        param.Value = lastname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
       
        //result will represent the number of changed rows
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

    public static DataTable getprofiledetailbyusername(string uesrname)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_profile_getdetailsbyusername";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = uesrname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

   
}