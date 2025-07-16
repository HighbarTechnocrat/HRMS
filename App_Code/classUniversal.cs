using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using System.Data;

public static class classUniversal
{
    static classUniversal()
	{
	}

    public static DataTable getUnivesalFileByProdIDandFiletype(int pid, string filetyp)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getAllFileListUsingProdIDandFiletyp";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@pid";
        param.Value = pid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@filetyp";
        param.Value = filetyp;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static bool insertFiles(int pid, string filename, string filetyp)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertFiles";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@pid";
        param.Value = pid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@filename";
        param.Value = filename;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@filetyp";
        param.Value = filetyp;
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