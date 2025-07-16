using System;
using System.Data;
using System.Data.Common;


public struct package
{
    public string pkg_name;
    public string pkg_id;
}
/// <summary>
/// Summary description for classpkg
/// </summary>
public class classpkg
{
	public classpkg()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public static DataTable getpkgprice()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_getpkgprice";
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getpkgdetail_package()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_getpkgdetail_package";
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getpkgdetail_packageid(decimal pkgid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_getpkgdetail_packageid";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@pkgid";
        param.Value = pkgid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getmoviedetailbyid(decimal pid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getmoviedetailbyid";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = pid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable getorderdetailbyemaild(string email,string pkgflag)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getorderdetailbyemail_orderitem";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@email";
        param.Value = email;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@packageflag";
        param.Value = pkgflag;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }



    public static DataTable getstatusorderdetailbyemaild(string email)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getstatusorderdetailbyemail_orderitem";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@email";
        param.Value = email;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable getcountorderdetailbyemaild(string email)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText ="sp_proc_getcountorderdetailbyemail_orderitem";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@email";
        param.Value = email;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    //method on 2dec to alert package
    public static DataTable getuserpackageemailalert()
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        // commd.CommandText = "sp_proc_gettoporderid";
        commd.CommandText = "sp_userepackagemailbyexiparydatetime";
        // commd.CommandText = "sp_coachemailbyvaliddatetime";
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

    //method added on 10th dec
    public static DataTable getstatusspecialorderdetailbyemaild(string email,decimal productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getstatusspecialorderdetailbyemail_orderitem";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@email";
        param.Value = email;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }



    public static DataTable getspecialorderdetailbyemaild(string email, string pkgflag, decimal productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getspecialorderdetailbyemailproductid_orderitem";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@email";
        param.Value = email;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@packageflag";
        param.Value = pkgflag;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }



    //new method on 28th feb 2015

    public static DataTable getspecialprice()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_getspecialprice";
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable getpremiumpkgdetails()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_getpkgdetails_package";
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
   
}