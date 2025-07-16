using System;
using System.Collections.Generic;
using System.Web;
using System.Data.Common;
using System.Data;

/// <summary>
/// Summary description for ClassForUpdateQty
/// </summary>
public class ClassForUpdateQty
{
	public ClassForUpdateQty()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static bool UpdateQtyInTable(string UserId, int ProductId, string qty, string OurPrice, int bid,string ipaddress)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_UpdateQtyIntbl_ProflieQtyUpdate";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@UserId";
        param.Value = UserId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ProductId";
        param.Value = ProductId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@qty";
        param.Value = qty;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@OurPrice";
        param.Value = OurPrice;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@bid";
        param.Value = bid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ipaddress";
        param.Value = ipaddress;
        param.DbType = DbType.String;
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
    public static DataTable getQtyInfoFromTable(string UserId, int ProductId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_GetQtyFromTable";
        // execute the stored procedure and save the results in a DataTable

        DbParameter param = comm.CreateParameter();

        param.ParameterName = "@UserId";
        param.Value = UserId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@ProductId";
        param.Value = ProductId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable SumOfQtyFormTable(string UserId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_GetSumOfQunatity";

        DbParameter param = comm.CreateParameter();

        param.ParameterName = "@UserId";
        param.Value = UserId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;
    }
    public static DataTable SumOfQtyFormProductTable(string UserId, int ProductId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_GetSumOfProductFromQtyTable";

        DbParameter param = comm.CreateParameter();

        param.ParameterName = "@UserId";
        param.Value = UserId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@ProductId";
        param.Value = ProductId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;
    }
    public static bool DeleteQtyInTable(string UserId, int ProductId)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_DeleteQtyProductFromQtyTable";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@UserId";
        param.Value = UserId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ProductId";
        param.Value = ProductId;
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
    public static DataTable getMaxShippingValueForMinus(double CartValue)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "GetMinValueForMsg";
// execute the stored procedure and save the results in a DataTable
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@CartValue";
        param.Value = CartValue;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;
    }
    public static bool DeleteQtyTableAsperProfile(string UserId)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "DeleteQtyUpdateTableResUsers";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@UserId";
        param.Value = UserId;
        param.DbType = DbType.String;
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
    public static DataTable ProductCount(string UserId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_GetProductCount";

        DbParameter param = comm.CreateParameter();

        param.ParameterName = "@UserId";
        param.Value = UserId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;
    }
    public static DataTable getstockvaluefromtable(int productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_getstockvaluefromtable";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@Productid";
        param.Value = productid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;
    }
    public static bool UpdateQtyInTbl(int ProductId, string OurPrice)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_UpdateQtyIntbl_ProflieQtyUpdprod";

        DbParameter param = comm.CreateParameter();

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ProductId";
        param.Value = ProductId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter


        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@OurPrice";
        param.Value = OurPrice;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter

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
    public static int CheckForProductSoldToday(string username)
    {
        DbCommand cmd = GenericDataAccess.CreateCommand();
        cmd.CommandText = "sp_checkProductOrderedToday";

        DbParameter param = cmd.CreateParameter();
        //param.ParameterName = "@productId";
        //param.Value = productid;
        //param.DbType = DbType.Int32;
        //cmd.Parameters.Add(param);

        param = cmd.CreateParameter();
        param.ParameterName = "@userID";
        param.Value = username;
        param.DbType = DbType.String;
        cmd.Parameters.Add(param);

        int result = -1;
        try
        {
            DataTable dt = GenericDataAccess.ExecuteSelectCommand(cmd);
            result = dt.Rows.Count;
            return result;
        }
        catch(Exception ex)
        {
            return -1;
        }
        
        
    }
    public static DataTable getZeroStockProductInTable(string email)
    {
        DbCommand cmd = GenericDataAccess.CreateCommand();
        cmd.CommandText = "usp_getStockZeroProductFromQtyTable";

        DbParameter param = cmd.CreateParameter();
        param.ParameterName = "@emailid";
        param.Value = email;
        param.DbType = DbType.String;
        cmd.Parameters.Add(param);
        DataTable dt = GenericDataAccess.ExecuteSelectCommand(cmd);
        return dt;
    }
}