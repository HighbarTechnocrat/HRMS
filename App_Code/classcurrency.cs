using System;
using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for classcurrency
/// </summary>
public class classcurrency
{
	public classcurrency()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable GetUSDrates()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getusdrates";
        // execute the stored procedure and return the results
        // return GenericDataAccess.ExecuteSelectCommand(comm);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
}