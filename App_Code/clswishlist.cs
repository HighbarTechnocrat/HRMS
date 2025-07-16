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
/// Summary description for clswishlist
/// </summary>
public class clswishlist
{
	public clswishlist()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataSet get_wishlist_from_username(string UserName)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "get_wish_list_username_1";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = UserName;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCmd1(comm);
    }
    //deleting product from cart of that particular user
    public static bool Deletewishlistproduct(int wishlistid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_delete_wishlist";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@indexid";
        param.Value = wishlistid;
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


    public static bool Deletewishlistproduct1(int pid,string uname)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_delete_wishlist1";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@pid";
        param.Value = pid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@uname";
        param.Value = uname;
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


    public static DataTable wishlist_GetSumOfProductFromQtyTable(string UserId, int ProductId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_wishlist_GetSumOfProductFromQtyTable";

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

    public static bool insertwishlist(string UserId, int ProductId, string qty, string OurPrice, string ipaddress)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_wishlist_insertwishlist";

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

    public static DataTable get_wishlist_product(string UserName, int productid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "get_wish_list_product_new";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = UserName;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@ProductId";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;

    }
    public static reviews getrating(int productid)
    {
	    DbCommand comm = GenericDataAccess.CreateCommand();
	    // set the stored procedure name
	    comm.CommandText = "sp_proc_GetRating";
	    // create a new parameter
	    DbParameter param = comm.CreateParameter();
	    param.ParameterName = "@productid";
	    param.Value = productid;
	    param.DbType = DbType.Int32;
	    comm.Parameters.Add(param);
	    DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
	    // wrap retrieved data into a ProductDetails object
	    reviews details = new reviews();
	    if ( table.Rows.Count > 0 )
	    {
		    // get the first table row
		    DataRow dr = table.Rows [0];
		    details.rating = Convert.ToInt32(dr ["rating"]);
		    details.productid = Convert.ToInt32(dr ["productid"]);

	    }
	    return details;

    }




    public static DataTable getproductrating_id(int productid)
    {
	    // get a configured DbCommand object
	    DbCommand comm = GenericDataAccess.CreateCommand();
	    // set the stored procedure name
	    comm.CommandText = "sp_proc_GetRating_wishlist";
	    // create a new parameter
	    DbParameter param = comm.CreateParameter();
	    param.ParameterName = "@productid";
	    param.Value = productid;
	    param.DbType = DbType.Int32;
	    comm.Parameters.Add(param);

	    return GenericDataAccess.ExecuteSelectCommand(comm);

    }



    public static DataTable getproductratingcnt_id(int productid)
    {
	    // get a configured DbCommand object
	    DbCommand comm = GenericDataAccess.CreateCommand();
	    // set the stored procedure name
	    comm.CommandText = "sp_proc_Getratingcnt_id_wishlist";
	    // create a new parameter
	    DbParameter param = comm.CreateParameter();
	    param.ParameterName = "@productid";
	    param.Value = productid;
	    param.DbType = DbType.Int32;
	    comm.Parameters.Add(param);

	    return GenericDataAccess.ExecuteSelectCommand(comm);

    }


    //method on 29th oct for delete favorites

    public static bool Deletewishlist_user(int wishlistid,string username)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_delete_wishlist_username";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@indexid";
        param.Value = wishlistid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@username";
        param.Value = username;
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


}