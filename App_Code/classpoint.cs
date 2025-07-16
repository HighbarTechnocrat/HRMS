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
using System.Data.Common;

/// <summary>
/// Summary description for classpoint
/// </summary>
public class classpoint
{
	public classpoint()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public static DataTable getbonuspoint()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_getbonuspt";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getassignpoint_username(string username)
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