using System;
using System.Data;
using System.Data.Common;

/// <summary>
/// Wraps country details data
/// </summary>
public struct country
{
    public string countryName;
    public string code;    
}
/// <summary>
/// Summary description for countryclass
/// </summary>
public class classcountry
{
    public classcountry()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataTable GetCountry()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetCountry";
        // execute the stored procedure and return the results
       // return GenericDataAccess.ExecuteSelectCommand(comm);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static bool deletecountry(int countryid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_CountryDelete";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
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
        return (result != -1);
    }
    public static country GetCountryDetails(int countryid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetCountryDetails";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@countryid";
        param.Value = countryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // wrap retrieved data into a DepartmentDetails object
        country details = new country();
        if (table.Rows.Count > 0)
        {
            details.countryName = table.Rows[0]["countryName"].ToString();
            details.code = table.Rows[0]["code"].ToString();
        }
        // return department details
        return details;
    }
    public static bool createcountry(string code, string  countryname)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_AddCountry";
       
        DbParameter param = comm.CreateParameter();
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@code";
        param.Value = code;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@countryname";
        param.Value = countryname;
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
    public static bool updatecountry(int countryid,string code,string countryname)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_AddCountry";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@countryid";
        param.Value = countryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        //DbParameter param = comm.CreateParameter();
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@code";
        param.Value = code;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@countryname";
        param.Value = countryname;
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


    public static string getcounbtrynamebyname(string countryname)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "getcounbtrynamebyname";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@name";
        param.Value = countryname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
       
        
     string   result = GenericDataAccess.ExecuteScalar(comm);
       
        return result ;
    }

    public static string getstateidbyname(string statename)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_getstateidbyname";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@name";
        param.Value = statename;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        string result = GenericDataAccess.ExecuteScalar(comm);

        return result;
    }

    public static DataTable getcountryId(string countryname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getcountryId";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@countryname";
        param.Value = @countryname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }


    public static DataTable getcountrycatmap(int countryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getcountrycatmap";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@countryid";
        param.Value = countryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static bool deletecountrycatmap(int countryid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deletecountrycatmap";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
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
        return (result != -1);
    }

    public static DataTable get_deletecatcityCStateMultiMapping(string categoryId)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_deletecatcityCStateMultiMapping";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryId";
        param.Value = categoryId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // result will represent the number of changed rows
        return GenericDataAccess.ExecuteSelectCommand(comm); 
    }

    public static DataTable get_usp_categoryMapQuery(string vendorname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_categoryMapQuery";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;

    }

    public static DataTable get_usp_categorySingleMapQuery()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_categorySingleMapQuery";
        // create a new parameter
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;

    }
    public static int getcountrycount(int productid, string countrycode)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getrestrictedcountry";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@country";
        param.Value = countrycode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        DataTable result = GenericDataAccess.ExecuteSelectCommand(comm);
        return Convert.ToInt32(result.Rows[0]["count"]);
    }

}
