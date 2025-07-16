using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for classxml
/// </summary>
public class classxml
{
	public classxml()
	{
		
	}

    public static DataTable getallcategory()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getallcategorys1";
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable latestpostxml1(int cid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getpostallbycat";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@catid";
        param.Value = cid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(comm);
        return dtc;
    }

    public static DataTable latestpostxml(int cid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getpostallbycat1";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@catid";
        param.Value = cid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(comm);
        return dtc;
    }

    public static DataTable gettopnewslist()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_topNewsList";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable topthoughtList()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_topthoughtList";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable topbdayList()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_topbdayList";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
}