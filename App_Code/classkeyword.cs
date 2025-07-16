using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

/// <summary>
/// Summary description for classkeyword
/// </summary>
/// 
public struct keyword
{
    public string keywordname;
    //public string code;
}
public class classkeyword
{
	public classkeyword()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public static DataSet getsearchlistBySearchbyname(string strKeywordName)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getsearchlistBySearchbyname";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@keywordname";
        param.Value = strKeywordName;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCmd1(comm);
    }

    public static bool updatekeywordfrequency(decimal keywordid, decimal keyordfrequency)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_updatekeywordfrequency";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@keywordid";
        param.Value = keywordid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@keyordfrequency";
        param.Value = keyordfrequency;
        param.DbType = DbType.Decimal;
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


    public static bool createkeyword(string keywordname, decimal keyordfrequency, string keywordurl, char viewstatus, string createdby)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertkeyword";
        DbParameter param = comm.CreateParameter();

        param = comm.CreateParameter();
        param.ParameterName = "@keywordname";
        param.Value = keywordname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@keyordfrequency";
        param.Value = keyordfrequency;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName ="@keywordurl";
        param.Value = keywordurl;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName ="@viewstatus";
        param.Value = viewstatus;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName ="@createdby";
        param.Value = createdby;
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