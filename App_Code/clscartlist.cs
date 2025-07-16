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
/// Summary description for clscartlist
/// </summary>
public class clscartlist
{
	public clscartlist()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataSet get_cartlist_count_from_username(string UserName)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "get_cart_list_count_username";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = UserName;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCmd1(comm);
    }
    public static DataSet get_cartlist_from_username(string UserName,string m_flag)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "get_cart_list_username";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = UserName;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@flag";
        param.Value = m_flag;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCmd1(comm);
    }
    //deleting product from cart of that particular user
    public static bool Deletecartlistproduct(int cartlistid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_delete_cartlist";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@indexid";
        param.Value = cartlistid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch
        {
        }
        return (result >= 1);

    }

    public static DataSet get_update_ipaddress_cartlist(string UserName, string ipaddress)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_update_ipaddress_cartlist";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = UserName;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@ipaddress";
        param.Value = ipaddress;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCmd1(comm);
    }


    public static DataTable getallcategoryid(int productid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getcategoryByproductID";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@productid";
        paramz.Value = productid;
        paramz.DbType = DbType.Int32;
        commd.Parameters.Add(paramz);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

    public static DataTable getrangeCOD()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getrangeCOD";
        // execute the stored procedure and return the results
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

}