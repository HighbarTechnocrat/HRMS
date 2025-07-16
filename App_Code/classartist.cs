using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for classartist
/// </summary>
public class classartist
{
	public classartist()
	{
	
	}

    public static DataTable getallartist()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_artist_getallartist";
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }

    public static DataTable getidbyartist(int artistid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_artist_getidbyartist";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@artistid";
        param.Value = artistid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    //public static DataTable getidbycategory(int artistid)
    //{
    //    DbCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = "sp_proc_category_getidbycategory";
    //    DbParameter param = comm.CreateParameter();
    //    param.ParameterName = "@artistid";
    //    param.Value = artistid;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);
    //    return GenericDataAccess.ExecuteSelectCommand(comm);
    //}

    public static DataTable getrelatedproducts(string flag,int artid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_productbrand_getrelatedproducts";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@flag";
        param.Value = flag;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@artid";
        param.Value = artid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // calculate how many pages of products and set the out parameter
        // return the page of products
        return table;
    }


    public static DataTable getidbyartistmetatitle(int artistid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_artist_getidbyartistmetatitle";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@artistid";
        param.Value = artistid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


}