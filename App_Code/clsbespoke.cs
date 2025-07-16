using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;

/// <summary>
/// Summary description for clsbespoke
/// </summary>
public class clsbespoke
{
	public clsbespoke()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable getbespokeidbyname(string name)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_bespoke_getbespokeidbyname";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@catname";
        param.Value = name;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable getproduct(int cat)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_bespoke_getproduct";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@catid";
        param.Value = cat;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable getcategory(string pid)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_bespoke_getcategory";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = pid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }


    public static DataTable getproductbyproductid(int pid)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_bespoke_getproductbyproductid";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = pid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable getproduct1(int cat)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_bespoke_getproduct1";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@catid";
        param.Value = cat;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

}