using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Linq;
using System.Data.Common;

/// <summary>
/// Summary description for classreminder
/// </summary>
public class classreminder
{
	public classreminder()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static bool insertreminder(string username,int pid, string rtime, string readflag, string isallday, string rfrom, string rto, int interval, string intervaltyp)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdateReminder";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@pid";
        param.Value = pid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //// create a new parameter
        //param = comm.CreateParameter();
        //param.ParameterName = "@username";
        //param.Value = username;
        //param.DbType = DbType.String;
        //comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@rtime";
        param.Value = rtime;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@read";
        param.Value = readflag;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@isallday";
        param.Value = isallday;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@rfrom";
        param.Value = rfrom;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@rto";
        param.Value = rto;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@interval";
        param.Value = interval;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@intervaltyp";
        param.Value = intervaltyp;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        int result = -1;
        result = GenericDataAccess.ExecuteNonQuery(comm);
        return (result >= 1);
    }

    public static bool updatereminder(string username,int pid, string rtime, string readflag, string isallday, string rfrom, string rto, int interval, string intervaltyp)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdateReminder";

        DbParameter param = comm.CreateParameter();
        //param.ParameterName = "@username";
        //param.Value = username;
        //param.DbType = DbType.String;
        //comm.Parameters.Add(param);

        // create a new parameter
        //param = comm.CreateParameter();
        param.ParameterName = "@pid";
        param.Value = pid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@rtime";
        param.Value = rtime;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@read";
        param.Value = readflag;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@isallday";
        param.Value = isallday;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@rfrom";
        param.Value = rfrom;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@rto";
        param.Value = rto;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@interval";
        param.Value = interval;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@intervaltyp";
        param.Value = intervaltyp;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        int result = -1;
        result = GenericDataAccess.ExecuteNonQuery(comm);
        return (result >= 1);
    }

    public static bool insertTaguser(int productid, string username, string flag)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdate_taguser1 ";

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

    //public static bool updateTaguser(int id, int productid, string username, string flag)
    //{
    //    // get a configured DbCommand object
    //    DbCommand comm = GenericDataAccess.CreateCommand();
    //    // set the stored procedure name
    //    comm.CommandText = "sp_proc_insertupdate_taguser1 ";

    //    DbParameter param = comm.CreateParameter();
    //    param.ParameterName = "@id";
    //    param.Value = id;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    // create a new parameter
    //    param = comm.CreateParameter();
    //    param.ParameterName = "@productid";
    //    param.Value = productid;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    // create a new parameter
    //    param = comm.CreateParameter();
    //    param.ParameterName = "@username";
    //    param.Value = username;
    //    param.DbType = DbType.String;
    //    comm.Parameters.Add(param);

    //    // create a new parameter
    //    param = comm.CreateParameter();
    //    param.ParameterName = "@read";
    //    param.Value = flag;
    //    param.DbType = DbType.String;
    //    comm.Parameters.Add(param);

    //    int result = -1;
    //    //try
    //    //{
    //    result = GenericDataAccess.ExecuteNonQuery(comm);
    //    //}
    //    //catch
    //    //{
    //    //}
    //    return (result >= 1);
    //}

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

    public static DataTable getReminderByProductid(int pid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getReminderByProductid";
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@pid";
        param.Value = pid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getTaguserForReminderUsingProductid(int pid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getTaguserForReminder";
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@pid";
        param.Value = pid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable displayReminderByUsername(string uname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_displayReminder";
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@uname ";
        param.Value = uname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable displayReminder_NoRepeat(string uname, string rtime)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_displayReminder_NoRepeat";
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@uname ";
        param.Value = uname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@rtime ";
        param.Value = rtime;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable displayReminder_Bydate(string uname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_displayReminder_Bydate";
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@uname ";
        param.Value = uname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static bool Update_ReminderStatus(string id)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_update_reminderstatus";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@id";
        param.Value = id;
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

    public static bool deleteTaguserForReminder(int pid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_DeleteTaguserUsingProductID";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = pid;
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