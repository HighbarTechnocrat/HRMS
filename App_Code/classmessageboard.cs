using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for classmessageboard
/// </summary>
public class classmessageboard
{
	public classmessageboard()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static bool insertMsgboard(string productname, string productnumber, string shortdescription, char viewstatus, char showonstatus, string createdby)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdate_msgboard";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productname";
        param.Value = productname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@productnumber";
        param.Value = productnumber;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@shortdescription";
        param.Value = shortdescription;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@viewstatus";
        param.Value = viewstatus;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@showonstatus";
        param.Value = showonstatus;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@createdby";
        param.Value = createdby;
        param.DbType = DbType.String;
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

    public static bool updateMsgboard(int productid, string productname, string productnumber, string shortdescription, char viewstatus, char showonstatus, string createdby)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdate_msgboard";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@productname";
        param.Value = productname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@productnumber";
        param.Value = productnumber;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@shortdescription";
        param.Value = shortdescription;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@viewstatus";
        param.Value = viewstatus;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@showonstatus";
        param.Value = showonstatus;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@createdby";
        param.Value = createdby;
        param.DbType = DbType.String;
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

    public static bool insertTaguser(int productid , string username)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdate_taguser";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
        param.DbType = DbType.String;
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

   public static bool updateTaguserFlag(int id, string flag)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_updatetaguserFlag";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@id";
        param.Value = id;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@read";
        param.Value = flag;
        param.DbType = DbType.String;
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
}