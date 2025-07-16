using System;
using System.Data;
using System.Data.Common;

public struct states
{
    public string statename;
    public string countryid;
}


/// <summary>
/// Summary description for classstate
/// </summary>
public class classstate
{
	public classstate()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataSet GetState()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetState";
        // execute the stored procedure and return the results
        // return GenericDataAccess.ExecuteSelectCommand(comm);
        return GenericDataAccess.ExecuteSelectCmd(comm);
    }
    public static bool deletestate(int stateid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_StateDelete";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@stateid";
        param.Value = stateid;
        param.DbType = DbType.Int32;
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
    public static states GetStateDetails(int stateid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetStateDetails";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@stateid";
        param.Value = stateid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // wrap retrieved data into a DepartmentDetails object
        states details = new states();
        if (table.Rows.Count > 0)
        {
            details.statename = table.Rows[0]["statename"].ToString();
            details.countryid = table.Rows[0]["countryid"].ToString();

        }
        // return department details
        return details;
    }
    public static bool createstate(string statename, int countryid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_AddState";

        DbParameter param = comm.CreateParameter();
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@statename";
        param.Value = statename;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@countryid";
        param.Value = countryid;
        param.DbType = DbType.Int32;
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
    public static bool updatestate(int stateid, string statename, int countyid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_AddState";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@stateid";
        param.Value = stateid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        //DbParameter param = comm.CreateParameter();
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@statename";
        param.Value = statename;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@countryid";
        param.Value = countyid;
        param.DbType = DbType.Int32;
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
    public static DataSet getstatebycountry(int countryid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getstatebycountry";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@countryid";
        param.Value = countryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCmd(comm);
    }
    public static DataTable getstateId(string statename)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getstateId";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@statename";
        param.Value = statename;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable getstatecatmap(int stateid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getstatecatmap";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@stateid";
        param.Value = stateid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    

    public static bool deletestatecatmap(int stateid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deletestatecatmap";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@stateid";
        param.Value = stateid;
        param.DbType = DbType.Int32;
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
}
