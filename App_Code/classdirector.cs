using System;
using System.Data;
using System.Data.Common;


/// <summary>
/// Summary description for classdirector
/// </summary>
/// 
public struct director
{
}
public class classdirector
{
	public classdirector()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    //new method on 20th aug
    public static DataTable getalldirector()
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getdirectorname";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();

        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }


    public static DataTable getalldirectorbyid(decimal productid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getdirectornamebyprodid";
        // create a new parameter
        DbParameter param = commd.CreateParameter();
        param = commd.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Decimal;
        commd.Parameters.Add(param);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }



    public static DataTable getallactorsproducerbyid(decimal productid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getallactorsproducer";
        // create a new parameter
        DbParameter param = commd.CreateParameter();


        param = commd.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Decimal;
        commd.Parameters.Add(param);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }



    public static DataTable getallattributegroup()
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getallattributegroup";
        //execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }


    public static DataTable getallattridbygrid(string attrgrid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_getattributeidbygrid";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@attrgrid";
        paramz.Value = attrgrid;
        paramz.DbType = DbType.String;
        commd.Parameters.Add(paramz);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }



    public static DataTable getallvideocntbyid(decimal productid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getvideocn_id";
        // create a new parameter
        DbParameter param = commd.CreateParameter();
        param = commd.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Decimal;
        commd.Parameters.Add(param);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

    public static DataTable getcntofdirprodbyid(decimal productid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getallactorsproducerdirectorcnt_productid";
        // create a new parameter
        DbParameter param = commd.CreateParameter();
        param = commd.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Decimal;
        commd.Parameters.Add(param);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

    public static DataTable getmetadirectordetails(int directorid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getmetadirectordetails";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@directorid";
        param.Value = directorid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    

}