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
/// Summary description for clspkgorder
/// </summary>
public class clspkgorder
{
	public clspkgorder()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable getemailidbyorder_order(decimal orderid)
    {

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_useremailbyorder";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@orderid";
        param.Value = orderid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        return dt;
    }

    public static DataTable getpackflag_orderid(decimal orderid)
    {

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_get_proc_getpkgflag";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@orderid";
        param.Value = orderid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        return dt;
    }

    public static DataTable getpackdetailinvoice_orderid(decimal orderid)
    {

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_get_proc_getorderdetailbyorderid_orderitem";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@orderid";
        param.Value = orderid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        return dt;
    }

} 