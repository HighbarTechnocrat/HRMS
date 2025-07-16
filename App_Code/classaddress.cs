using System;
using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for classaddress
/// </summary>
public struct address
{
    public int indexid;
    public string username;
    public string fistname;
    public string lastname;
    public string emailid;
    public string address1;
    public string address2;
    public string city;
    public string pincode;
    public string state;
    public string country;
    public string telno;
    public string mobileno;
    public string citycode;
    public string cityid;
    public char defaultshipaddress;
    public char defaultbilladdress; 
}
public class classaddress
{
	public classaddress()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static bool insertupdatebirth(string username, string event_date, string event_flag)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insert_birth_anniversary";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@event_date";
        param.Value = event_date;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@event_flag";
        param.Value = event_flag;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

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

    //////Insert Customer
    public static bool createmypassbook(string username, string Campaignname, int Assignpoints, string vouchername, string Actiontaken)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdatemypassbook";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@UserNamee";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Campaignname";
        param.Value = Campaignname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Assignpoints";
        param.Value = Assignpoints;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@vouchername";
        param.Value = vouchername;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Actiontaken";
        param.Value = Actiontaken;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
       
        // create a new parameter
        // result will represent the number of changed rows
        int result = -1;
        //try
        //{
        //    // execute the stored procedure
           result = GenericDataAccess.ExecuteNonQuery(comm);
        //}
        //catch
        //{
        //    // any errors are logged in GenericDataAccess, we ingore them here
        //}
        // result will be 1 in case of success 
        return (result >= 1);
    }
    public static bool updatemypassbook(int indexid, string username, string Campaignname, int Assignpoints, string vouchername, string Actiontaken)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdatemypassbook";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@indexid";
        param.Value = indexid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Campaignname";
        param.Value = Campaignname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Assignpoints";
        param.Value = Assignpoints;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@vouchername";
        param.Value = vouchername;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@Actiontaken";
        param.Value = Actiontaken;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        // result will represent the number of changed rows
        int result = -1;
        try
        {
             //execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch
        {
             //any errors are logged in GenericDataAccess, we ingore them here
        }
        // result will be 1 in case of success 
        return (result >= 1);
    }
    public static DataTable getallcity(string cityname)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_getallcity";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@cityname";
        paramz.Value = cityname;
        paramz.DbType = DbType.String;
        commd.Parameters.Add(paramz);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }
    public static bool createaddress(string username, string firstname, string lastname, string emailid, string address1, string address2, string city, string pincode, string state, string country, string telno, string mobileno, char defaultshipaddress, char defaultbilladdress, string source, string macid, string countrycode, string citycode)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdateaddress";
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
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = emailid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@address1";
        param.Value = address1;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@address2";
        param.Value = address2;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@city";
        param.Value = city;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@pincode";
        param.Value = pincode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@state";
        param.Value = state;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@country";
        param.Value = country;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@telno";
        param.Value = telno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@mobileno";
        param.Value = mobileno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@defaultshipaddress";
        param.Value = defaultshipaddress;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@source";
        param.Value = source;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@macid";
        param.Value = macid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@defaultbilladdress";
        param.Value = defaultbilladdress;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@countrycode";
        param.Value = countrycode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@citycode";
        param.Value = citycode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
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
    public static bool createaddress1(string username, string firstname, string lastname, string emailid, string address1, string address2, string city, string pincode, string state, string country, string telno, string mobileno, char defaultshipaddress, char defaultbilladdress, string source, string macid, string countrycode, string citycode, string title, string dob)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdateaddress1";
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
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = emailid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@address1";
        param.Value = address1;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@address2";
        param.Value = address2;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@city";
        param.Value = city;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@pincode";
        param.Value = pincode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@state";
        param.Value = state;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@country";
        param.Value = country;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@telno";
        param.Value = telno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@mobileno";
        param.Value = mobileno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@defaultshipaddress";
        param.Value = defaultshipaddress;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@source";
        param.Value = source;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@macid";
        param.Value = macid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@defaultbilladdress";
        param.Value = defaultbilladdress;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@countrycode";
        param.Value = countrycode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@citycode";
        param.Value = citycode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@title";
        param.Value = title;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@dob";
        param.Value = dob;
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
    public static bool createaddress2(string username, string firstname, string lastname, string emailid, string address1, string address2, string city, string pincode, string state, string country, string telno, string mobileno, char defaultshipaddress, char defaultbilladdress, string source, string macid, string countrycode, string citycode, string title, string dob,string area)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdateaddress2";
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
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = emailid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@address1";
        param.Value = address1;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@address2";
        param.Value = address2;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@city";
        param.Value = city;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@pincode";
        param.Value = pincode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@state";
        param.Value = state;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@country";
        param.Value = country;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@telno";
        param.Value = telno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@mobileno";
        param.Value = mobileno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@defaultshipaddress";
        param.Value = defaultshipaddress;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@source";
        param.Value = source;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@macid";
        param.Value = macid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@defaultbilladdress";
        param.Value = defaultbilladdress;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@countrycode";
        param.Value = countrycode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@citycode";
        param.Value = citycode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@title";
        param.Value = title;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@dob";
        param.Value = dob;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@area";
        param.Value = area;
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
    public static bool updateaddress(int indexid, string username, string firstname, string lastname, string emailid, string address1, string address2, string city, string pincode, string state, string country, string telno, string mobileno, char defaultshipaddress, char defaultbilladdress, string countrycode, string citycode)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdateaddress";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@indexid";
        param.Value = indexid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
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
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = emailid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@address1";
        param.Value = address1;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@address2";
        param.Value = address2;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@city";
        param.Value = city;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@pincode";
        param.Value = pincode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@state";
        param.Value = state;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@country";
        param.Value = country;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@telno";
        param.Value = telno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@mobileno";
        param.Value = mobileno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@defaultshipaddress";
        param.Value = defaultshipaddress;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        
        //// create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@source";
        param.Value = "source";
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@defaultbilladdress";
        param.Value = defaultbilladdress;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@countrycode";
        param.Value = countrycode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@citycode";
        param.Value = citycode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
          // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@macid";
        param.Value = "macid";
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        

        // create a new parameter
        // result will represent the number of changed rows
        int result = -1;
        //try
        //{
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        //}
        //catch
        //{
        //    // any errors are logged in GenericDataAccess, we ingore them here
        //}
        // result will be 1 in case of success 
        return (result != -1);
    }
 
//Get address list by username
    public static DataTable useraddresslist(string username)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getaddress";
        // execute the stored procedure and save the results in a DataTable
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
       DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        
        return table;
    }

    public static DataSet AddressList(string username)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getaddress";
        // execute the stored procedure and return the results
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param); 
        return GenericDataAccess.ExecuteSelectCmd(comm);

    }
    public static DataTable getaddressbyusername(string uesrname)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getaddressbyusername";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = uesrname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
  
    public static DataTable getdefaultaddressbyusername(string uesrname)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getdefaultaddbyusername";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = uesrname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable updatevisitcount(string username)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_updateuservisitcount";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    //public static DataSet Addresslistbyid(int indexid)
    //{
    //    // get a configured DbCommand object
    //    DbCommand comm = GenericDataAccess.CreateCommand();
    //    // set the stored procedure name
    //    comm.CommandText = "sp_proc_getaddressdetails";
    //    // execute the stored procedure and return the results
    //    DbParameter param = comm.CreateParameter();
    //    param.ParameterName = "@indexid";
    //    param.Value = indexid;
    //    param.DbType = DbType.String;
    //    comm.Parameters.Add(param);
    //    return GenericDataAccess.ExecuteSelectCmd(comm);

    //}
    ////Get Address Details by indexid [sp_proc_getaddressdetails]//
    public static address getaddressdetails(int indexid, string username)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getaddressdetails";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@indexid";
        param.Value = indexid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // execute the stored procedure
        //return GenericDataAccess.ExecuteSelectCommand(comm);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // wrap retrieved data into a ProductDetails object
        address details = new address();
        if (table.Rows.Count > 0)
        {
            // get the first table row
            DataRow dr = table.Rows[0];
            // get product details
            details.indexid = Convert.ToInt32(dr["indexid"]);
            details.username = Convert.ToString(dr["username"]);
            details.fistname = Convert.ToString(dr["firstname"]);
            details.lastname = Convert.ToString(dr["lastname"]);
            details.emailid = Convert.ToString(dr["emailid"]);
            details.address1 = Convert.ToString(dr["address1"]);
            details.address2 = Convert.ToString(dr["address2"]);
            details.city = Convert.ToString(dr["city"]);
            details.pincode = Convert.ToString(dr["pincode"]);
            details.state = Convert.ToString(dr["state"]);
            details.country = Convert.ToString(dr["country"]);
            details.telno = Convert.ToString(dr["telno"]);
            details.mobileno = Convert.ToString(dr["mobileno"]);
            details.citycode = Convert.ToString(dr["citycode"]);
            details.defaultshipaddress = Convert.ToChar(dr["defaultshipaddress"]);
            details.defaultbilladdress = Convert.ToChar(dr["defaultbilladdress"]);
        }
        //return department details
        return details;
    }

    public static DataTable getaddressdetails1(int indexid, string username)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getaddressdetails";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@indexid";
        param.Value = indexid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static int deleteaddressbyid(int indexid, out int count)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deleteaddress";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@indexid";
        param.Value = indexid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@HowManyProducts";
        param.Direction = ParameterDirection.Output;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        int howManyProducts = Int32.Parse(comm.Parameters["@HowManyProducts"].Value.ToString());
        count = (int)howManyProducts;
        // execute the stored procedure
        return count;
    }
    public static DataTable getaddressbookbymultipltids(string strindexid)
    { 
            
        DbCommand comm = GenericDataAccess.CreateCommand();        
        comm.CommandText = "sp_getaddressbookbymultipltids";        
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@indexid";
        param.Value = strindexid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);        
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getexportcustemail(string username)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getexportcustemail";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataSet deleteuser(string username)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deleteuser";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCmd(comm);
    }

   
    public static DataSet GetuserIdtoupdatemembership(string UserId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetuserIdtoupdatemembership";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@UserId";
        param.Value = UserId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCmd(comm);
    }

    public static DataTable getindexidbyusername(string username)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getuserindexid";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@username";
        paramp.Value = username;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }


    public static DataTable getvendorendtbyenquiryid(int enquiryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetUservendorEnqDtByEnquiryID";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@EnquiryID";
        paramp.Value = enquiryid;
        paramp.DbType = DbType.Int32;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable getareacitybyvendorname(string vendorname)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_getselectcityarea";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
       
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable getareabycoach(string vendorname)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_getselectareabycoach";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }




    public static bool updateaddress2(int indexid, string username, string firstname, string lastname, string emailid, string address1, string address2, string city, string pincode, string state, string country, string telno, string mobileno,string source, char defaultshipaddress, char defaultbilladdress, string countrycode, string citycode, string title, string dob, string area)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdateaddress2";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@indexid";
        param.Value = indexid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
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
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = emailid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@address1";
        param.Value = address1;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@address2";
        param.Value = address2;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@city";
        param.Value = city;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@pincode";
        param.Value = pincode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@state";
        param.Value = state;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@country";
        param.Value = country;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@telno";
        param.Value = telno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@mobileno";
        param.Value = mobileno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@defaultshipaddress";
        param.Value = defaultshipaddress;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        //// create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@source";
        param.Value = source;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@defaultbilladdress";
        param.Value = defaultbilladdress;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@countrycode";
        param.Value = countrycode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@citycode";
        param.Value = citycode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@macid";
        param.Value = "macid";
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@title";
        param.Value = title;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@dob";
        param.Value = dob;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@area";
        param.Value = area;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        // result will represent the number of changed rows
        int result = -1;
        //try
        //{
        // execute the stored procedure
        result = GenericDataAccess.ExecuteNonQuery(comm);
        //}
        //catch
        //{
        //    // any errors are logged in GenericDataAccess, we ingore them here
        //}
        // result will be 1 in case of success 
        return (result != -1);
    }

    public static DataTable getstatecitybyvendorname(string vendorname)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_getselectstatecity";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    //new method for learner details

    public static DataTable getlearnerdt(string username)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_getlearneremail";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@usermail";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    //new method for coach mail ad approved

    public static DataTable getallcoachcount()
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_totalcoach";
        //create new parameter

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable get_count_pagecount()
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_get_pagecount";
        //create new parameter

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataSet getallcaochlist(int pageno, int pagesize)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_coachlist";
        // execute the stored procedure and save the results in a DataTable
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@Page";
        param.Value = pageno;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //create new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@PageSize";
        param.Value = pagesize;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCmd1(comm);

    }
    public static bool createmailids( string username, string flag)
   {

       // get a configured DbCommand object
       DbCommand comm = GenericDataAccess.CreateCommand();
       // set the stored procedure name
       comm.CommandText = "sp_proc_insertupdcoachemail";
       // create a new parameter
       DbParameter param = comm.CreateParameter();
       

       param.ParameterName = "@username";
       param.Value = username;
       param.DbType = DbType.String;
       comm.Parameters.Add(param);
       // create a new parameter
       param = comm.CreateParameter();
       param.ParameterName = "@flag";
       param.Value = flag;
       param.DbType = DbType.String;
       comm.Parameters.Add(param);
       // create a new parameter
       // result will represent the number of changed rows
       int result = -1;
       //try
       //{
       // execute the stored procedure
       result = GenericDataAccess.ExecuteNonQuery(comm);
       //}
       //catch
       //{
       //    // any errors are logged in GenericDataAccess, we ingore them here
       //}
       // result will be 1 in case of success 
       return (result != -1);
   }

    public static bool get_count_insert_tempemail(int username)
    {

        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_count_insert_tempemail";
        // create a new parameter
        DbParameter param = comm.CreateParameter();

        param.ParameterName = "@page_count";
        param.Value = username;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // result will represent the number of changed rows
        int result = -1;
        // execute the stored procedure
        result = GenericDataAccess.ExecuteNonQuery(comm);
        return (result != -1);
    }

    public static DataTable getallcoachlist()
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_totalcoachlist";
        //create new parameter

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    //new method on 4th dec for getting details of user from addressbook

    public static DataTable getalluserdetailsbyuserid(int indexid, string username)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_getaddressdetails";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@indexid";
        param.Value = indexid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable getuserinfo(string emailid)
    {
        DbCommand command = GenericDataAccess.CreateCommand();
        command.CommandText = "sp_proc_membership_userinfo";
        DbParameter param = command.CreateParameter();
        param.ParameterName = "@user_emailid";
        param.Value = emailid;
        param.DbType = DbType.String;
        command.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(command);

    }

    public static bool createaddressregister(string user_emailid, string firstname, string lastname, string address, string mobileno, int countryid, int stateid, int cityid,string pincode,string citycode,string telno)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_userdetails_insertupdateuserinfo";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
      
        param = comm.CreateParameter();
        param.ParameterName = "@user_emailid";
        param.Value = user_emailid;
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
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@address";
        param.Value = address;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@mobileno";
        param.Value = mobileno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@countryid";
        param.Value = countryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@stateid";
        param.Value = stateid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@cityid";
        param.Value = cityid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@pincode";
        param.Value = pincode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@citycode";
        param.Value = citycode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@telno";
        param.Value = telno;
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

    public static DataSet getalluserbyusername(string username)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_userdetails_getalluserbyusername";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCmd(comm);
    }

    public static DataTable task(string username)
    {
        //DbCommand comm = GenericDataAccess.CreateCommand();
        //// comm.CommandText = "usp_SearchProducts";
        //comm.CommandText = "sp_proc_getpost_by_user_task";
        //DbParameter param = comm.CreateParameter();



        //param = comm.CreateParameter();
        //param.ParameterName = "@username";
        //param.Value = username;
        //param.DbType = DbType.String;
        //comm.Parameters.Add(param);

        //DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        //return dt;
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getpost_by_user_task";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
       // return GenericDataAccess.ExecuteSelectCmd(comm);
	   return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable meetings(string username)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getpost_by_user_meeting";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        //return GenericDataAccess.ExecuteSelectCmd(comm);
		return GenericDataAccess.ExecuteSelectCommand(comm);
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

            details.code = table.Rows [0] ["countrycode"].ToString ( );
        }
        // return department details
        return details;
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
            details.cityname = table.Rows[0]["cityname"].ToString();
            details.stateid = table.Rows[0]["stateid"].ToString();

        }
        // return department details
        return details;
    }

    public static DataTable getuserinfodetails(string emailid)
    {
        DbCommand command = GenericDataAccess.CreateCommand();
        command.CommandText = "sp_proc_userinfo";
        DbParameter param = command.CreateParameter();
        param.ParameterName = "@user_emailid";
        param.Value = emailid;
        param.DbType = DbType.String;
        command.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(command);
    }


    public static DataTable getuserbirthdaygreeting(string emailid)
    {
        DbCommand command = GenericDataAccess.CreateCommand();
        command.CommandText = "sp_proc_userbdaygreeting";
        DbParameter param = command.CreateParameter();
        param.ParameterName = "@username";
        param.Value = emailid;
        param.DbType = DbType.String;
        command.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(command);
    }

    public static bool edituser(string user_emailid, string firstname, string lastname, string address, string mobileno, int countryid, int stateid, int cityid, string pincode, string citycode, string telno, string dob, string gen, string offmob, string altmob, string offphone, string extno, string altemail, string fax, string loc, string dept , string subdept, string desg, string tempadd, string email)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_edituser";

        // create a new parameter
        DbParameter param = comm.CreateParameter();

        param.ParameterName = "@user_emailid";
        param.Value = user_emailid;
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
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@address";
        param.Value = address;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@mobileno";
        param.Value = mobileno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@countryid";
        param.Value = countryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@stateid";
        param.Value = stateid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@cityid";
        param.Value = cityid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@pincode";
        param.Value = pincode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@citycode";
        param.Value = citycode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@telno";
        param.Value = telno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@dob";
        param.Value = dob;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@gender";
        param.Value = gen;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@offmob";
        param.Value = offmob;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@altno";
        param.Value = altmob;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@offphone";
        param.Value = offphone;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@extno";
        param.Value = extno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@altemail";
        param.Value = altemail;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@faxno";
        param.Value = fax;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@loc";
        param.Value = loc;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@dept";
        param.Value = dept;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@subdept";
        param.Value = subdept;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@desg";
        param.Value = desg;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@tempaddress";
        param.Value = tempadd;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@email";
        param.Value = email;
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

    public static bool editssouser(string user_emailid, string firstname, string lastname, string mobileno, string dob, string gen, string dept, string desg)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_editssouser";

        // create a new parameter
        DbParameter param = comm.CreateParameter();

        param.ParameterName = "@user_emailid";
        param.Value = user_emailid;
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
         // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@mobileno";
        param.Value = mobileno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@dob";
        param.Value = dob;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@gender";
        param.Value = gen;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@dept";
        param.Value = dept;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@desg";
        param.Value = desg;
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


    public static bool updatessouser(string user_emailid, string firstname, string lastname, string mobileno, string dob, string gen, string dept, string desg, string extno, string address, string tempadd, string email)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_updatessouser";

        // create a new parameter
        DbParameter param = comm.CreateParameter();

        param.ParameterName = "@uname";
        param.Value = user_emailid;
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
         // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@mobileno";
        param.Value = mobileno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@dob";
        param.Value = dob;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@gender";
        param.Value = gen;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@dept";
        param.Value = dept;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@desg";
        param.Value = desg;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@extno";
        param.Value = extno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@address";
        param.Value = address;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@tempadd";
        param.Value = tempadd;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@email";
        param.Value = email;
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

    public static bool insertpostenquiery(string name,string contactcountrycode, string contact, string email,string cname, string add1,string add2,string postcode,string country,string state,string city,string method,string countrycode, string mobileno,string cemail,DateTime estart,DateTime eeend,int nattence,string venue,string layout,string avrequired,decimal bfrom,decimal bto)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_postenquiery_insertpostenquiery";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@name";
        param.Value = name;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@contactcountrycode";
        param.Value = contactcountrycode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@contact";
        param.Value = contact;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@email";
        param.Value = email;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@companyname";
        param.Value = cname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@add1";
        param.Value = add1;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@add2";
        param.Value = add2;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@postcode";
        param.Value = postcode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@country";
        param.Value = country;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@state";
        param.Value = state;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@town";
        param.Value = city;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

         // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@method";
        param.Value = method;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@mobileno";
        param.Value = mobileno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

         // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "countrycode";
        param.Value = countrycode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

         // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@cemail";
        param.Value = cemail;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@estart";
        param.Value = estart;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@eend";
        param.Value = eeend;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@nattence";
        param.Value = nattence;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@venue";
        param.Value = venue;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
      

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@layout";
        param.Value = layout;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

     
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@avrequired";
        param.Value = avrequired;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@bfrom";
        param.Value = bfrom;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@bto";
        param.Value = bto;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);
        int result = -1;
        //try
        //{
        result = GenericDataAccess.ExecuteNonQuery(comm);
        //}
        //catch
        //{
        //}
        return (result >= 1);
    }

    public static bool insertenquirytoproduct(int enquiryid, int productid)
    {

        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertenquirytoproduct";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@enquiryid";
        param.Value = enquiryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
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

    public static bool insertenquirytocategory(int enquiryid, int categoryid)
    {

        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertenquirytocategory";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@enquiryid";
        param.Value = enquiryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
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

    public static DataTable gettopenquiryid()
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_gettopenquiryid";
        //create new parameter

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getallcategoryid(int productid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getcategoryByproductID";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@productid";
        paramz.Value = productid;
        paramz.DbType = DbType.Int32;
        commd.Parameters.Add(paramz);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }


    //new method on 20th aug
    public static DataTable getallcountry()
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getcountryname";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
       
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }


    public static DataTable getallstatebycntryid(decimal countryid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getstatename_countryid";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@countryid";
        paramz.Value = countryid;
        paramz.DbType = DbType.Decimal;
        commd.Parameters.Add(paramz);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

    public static DataTable getallcitybystateid(decimal stateid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getcountryname_stateid";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@stateid";
        paramz.Value = stateid;
        paramz.DbType = DbType.Decimal;
        commd.Parameters.Add(paramz);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }


    public static bool createFBAddress(string user_emailid, string firstname, string lastname, string address)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_userdetails_insertFBupdateuserinfo";

        // create a new parameter
        DbParameter param = comm.CreateParameter();

        param = comm.CreateParameter();
        param.ParameterName = "@user_emailid";
        param.Value = user_emailid;
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
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@address";
        param.Value = address;
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


    /*add sanjivani for scratch card*/

    public static bool createscrach(string promocode, string macid, string username, string status)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertscrach";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@promocode";
        param.Value = promocode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@macid";
        param.Value = macid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@status";
        param.Value = status;
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

    public static DataTable getscrach(string promocode)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_selectscrach";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@promocode";
        param.Value = promocode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable selectscrachid(string promocode)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_selectscrachid";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@promocode";
        param.Value = promocode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getpromodisbyusername(string username)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getpromodisbyusername";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getpromoidbystatus(string username)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getpromoidbystatus";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable selectscrachbyfromdate(string promocode)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_selectscrachbyfromdate";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@promocode";
        param.Value = promocode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable selectscrachbyenddate(string promocode)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_selectscrachbyenddate";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@promocode";
        param.Value = promocode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    /**/
    public static bool createscrachcarduser(string promocode,int promoid,int orderid, string macid, string username, string status,string flag)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertscrachcarduser";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@promocode";
        param.Value = promocode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@promoid";
        param.Value = promoid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@orderid";
        param.Value = orderid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@macid";
        param.Value = macid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@status";
        param.Value = status;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@flag";
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

    public static DataTable getuserpromobyusername(string username)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getuserpromobyusername";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static bool updatescrachcarduser(string flag, string username,int orderid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_updateuserpromobyusername";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@flag";
        param.Value = flag;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@orderid";
        param.Value = orderid;
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

    public static DataTable getalluser()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_userdetails_getalluser";
        // execute the stored procedure and return the results
        // return GenericDataAccess.ExecuteSelectCommand(comm);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }



    public static DataTable getuserbyindexid(int indexid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getuserbyindexid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@indexid";
        param.Value = indexid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // execute the stored procedure and return the results
        // return GenericDataAccess.ExecuteSelectCommand(comm);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable Getuserbyemail(string username)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getuserindexid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataSet GetuserId(string username)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetuserId";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCmd(comm);
    }
    public static DataTable getcountrystatebycity(string cityname)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getcountrystatebycity";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@cityname";
        paramz.Value = cityname;
        paramz.DbType = DbType.String;
        commd.Parameters.Add(paramz);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

    public static DataTable getallautocompletename(string uname)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_autocompleteuser";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@uname";
        paramz.Value = uname;
        paramz.DbType = DbType.String;
        commd.Parameters.Add(paramz);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

    public static bool createaddressregister(string user_emailid, string firstname, string lastname, string address, string mobileno, int countryid, int stateid, int cityid, string pincode, string citycode, string telno, string bday, string gender, string location, string dept, string subdept, string desg, string extno, string tempadd, string email)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_userdetails_insertupdateuserinfo1";//sp_proc_userdetails_insertupdateuserinfo

        // create a new parameter
        DbParameter param = comm.CreateParameter();

        param = comm.CreateParameter();
        param.ParameterName = "@user_emailid";
        param.Value = user_emailid;
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
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@address";
        param.Value = address;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@mobileno";
        param.Value = mobileno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@countryid";
        param.Value = countryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@stateid";
        param.Value = stateid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@cityid";
        param.Value = cityid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@pincode";
        param.Value = pincode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@citycode";
        param.Value = citycode;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@telno";
        param.Value = telno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@bday";
        param.Value = bday;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@gender";
        param.Value = gender;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@location";
        param.Value = location;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@dept";
        param.Value = dept;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@subdept";
        param.Value = subdept;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@desg";
        param.Value = desg;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@extno";
        param.Value = extno;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@tempadd";
        param.Value = tempadd;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@email";
        param.Value = email;
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

    public static DataTable GetuserFullname(string username)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetFullname";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable UpdateUserStatus(string username, int status)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "UserOnlineStatusUpdate";
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@ostatus";
        param.Value = status;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
}
