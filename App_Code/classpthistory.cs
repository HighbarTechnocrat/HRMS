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
/// Summary description for classpthistory
/// </summary>
public class classpthistory
{
	public classpthistory()
	{
		//
		// TODO: Add constructor logic here
		//
	}

	public static DataSet getuserpointhistory(string username)
	{
		DbCommand comm = GenericDataAccess.CreateCommand();
		comm.CommandText = "sp_proc_usr_getpointhistory_mypassbook";
		DbParameter param = comm.CreateParameter();
		param.ParameterName = "@username";
		param.Value = username;
		param.DbType = DbType.String;
		comm.Parameters.Add(param);
		  //return GenericDataAccess.ExecuteSelectCmd1(command);
		return GenericDataAccess.ExecuteSelectCmd1(comm);
	}
	public static DataTable getassignpoint_user(string username)
	{
		// get a configured DbCommand object
		DbCommand comm = GenericDataAccess.CreateCommand();
		// set the stored procedure name
		//  comm.CommandText = "sp_get_assignpoints";
		comm.CommandText = "sp_get_assignpoints_user";
		// execute the stored procedure and return the results
		DbParameter param = comm.CreateParameter();
		param.ParameterName = "@username";
		param.Value = username;
		param.DbType = DbType.String;
		comm.Parameters.Add(param);
		// return enquiry list
		return GenericDataAccess.ExecuteSelectCommand(comm);
	}
}