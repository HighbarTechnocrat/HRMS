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
/// <summary>
/// Summary description for classmodel
/// </summary>
public class classmodel
{
	public classmodel()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable getallcntcat()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_countallcatdetl_metatag";

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataSet getallcatlist(int pageno, int pagesize)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_addtls";
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

    public static DataTable getallcntads()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_countalladdetl_metatag";

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataSet getalladlist(int pageno, int pagesize)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getalladdtls";
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


}