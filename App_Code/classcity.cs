using System;
using System.Data;
using System.Data.Common;

public struct city
{
    public string cityname;
    public string stateid;
}

/// <summary>
/// Summary description for classcity
/// </summary>
public class classcity
{
	public classcity()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataSet GetCity()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetCity";
        // execute the stored procedure and return the results
        // return GenericDataAccess.ExecuteSelectCommand(comm);
        return GenericDataAccess.ExecuteSelectCmd(comm);
    }
    public static bool deletecity(int cityid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_CityDelete";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@cityid";
        param.Value = cityid;
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
    public static city GetCityDetails(int cityid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetCityDetails";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@cityid";
        param.Value = cityid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // wrap retrieved data into a DepartmentDetails object
        city details = new city();
        if (table.Rows.Count > 0)
        {
            details.cityname= table.Rows[0]["cityname"].ToString();
            details.stateid = table.Rows[0]["stateid"].ToString();

        }
        // return department details
        return details;
    }
    public static bool createcity(string cityname, int stateid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_AddCity";

        DbParameter param = comm.CreateParameter();
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@cityname";
        param.Value = cityname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
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
        return (result >= 1);
    }
    public static bool updatecity(int cityid, string cityname, int stateid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_AddCity";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@cityid";
        param.Value = cityid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        //DbParameter param = comm.CreateParameter();
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@cityname";
        param.Value = cityname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
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

    public static DataSet getcitybystate(int stateid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcitybystate";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@stateid";
        param.Value = stateid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCmd(comm);
    }

    public static DataTable getcityId(string cityname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getcityId";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@cityname";
        param.Value = cityname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable getstateandcountryfromcity(string cityname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_getstateandcountrynamefromcityname";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@cityname";
        param.Value = cityname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }


    public static DataTable getcitycatmap(int cityid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getcitycatmap";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@cityid";
        param.Value = cityid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static bool deletecitycatmap(int cityid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deletecitycatmap";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@cityid";
        param.Value = cityid;
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
