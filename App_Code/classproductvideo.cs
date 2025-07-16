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
/// Summary description for classproductvideo
/// </summary>
/// 


public struct productvideo
{
    public int productid;
    public int prodvideoid;
    public string videoembed;
}
public class classproductvideo
{

	public classproductvideo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataTable loadimagefiledtls()
    {
        //get configured database command
        DbCommand comm = GenericDataAccess.CreateCommand();
        // get command text as stored procedure
        comm.CommandText = "sp_proc_loadimagefiledtls";
        //execute the stored procedure
        //retuen the results
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable selectproductimage(int productid)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_selectproductvideo";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        //extcute the stored procedure and return result
        //return faq list based on faqcatid
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static bool deleteimagefile(int productvideoid)
    {
        //get configured db command

        DbCommand comm = GenericDataAccess.CreateCommand();

        // get command type as stored procedure

        comm.CommandText = "sp_proc_deleteproductvideo";

        //create parameter

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productvideoid";
        param.Value = productvideoid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // result will represent the number of changed rows
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch
        {
            // any errors are logged in GenericDataAccess, we ingore them here
        }
        // result will be 1 in case of success 
        return (result != -1);
    }
    public static DataTable loadvideo()
    {
        //get configured database command
        DbCommand comm = GenericDataAccess.CreateCommand();
        // get command text as stored procedure
       
       comm.CommandText = "sp_proc_loadimagefiledtls";
        //execute the stored procedure
        //retuen the results
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static bool createproductvideo(int productid, string videoembed)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdateproductvideo";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@videoembed";
        param.Value = videoembed;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch
        {
            // any errors are logged in GenericDataAccess, we ingore them here
        }
        // result will be 1 in case of success 
        return (result >= 1);
    }


    public static bool updateproductvideo(int productid,int productvideoid, string videoembed)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdateproductvideo";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@productvideoid";
        param.Value = productvideoid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@videoembed";
        param.Value = videoembed;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
       
        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch
        {
            // any errors are logged in GenericDataAccess, we ingore them here
        }
        // result will be 1 in case of success 
        return (result >= 1);
    }

    public static productvideo GetprodvideoDetails(int productid, int prodvideoid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetProdVideosDetails";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
       param = comm.CreateParameter();
        param.ParameterName = "@prodvideoid";
        param.Value = prodvideoid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // wrap retrieved data into a DepartmentDetails object
        productvideo details = new productvideo();
        if (table.Rows.Count > 0)
        {
            details.videoembed = table.Rows[0]["videoembed"].ToString();
            //details.prodvideoid =Convert.ToInt32( table.Rows[0]["prodvideoid"].ToString());
            //details.productid  = Convert.ToInt32( table.Rows[0]["productid"].ToString());
           
        }
        // return shop details
        return details;
    }


    public static DataTable getproductforprodvideos()
    {

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_searchproductforvideo";
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getproductforvideosbysearch(string strprodname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_searchproductforvideobytextsearch";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@prodname";
        param.Value = strprodname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getproductforvideobyprodid(int strprodid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getproductforvideobyprodid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@prodid";
        param.Value = strprodid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable deleteproductvideobyid(int strprodid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_deleteproductforvideo_productvideo";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@prodid";
        param.Value = strprodid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    //new method on 16aug 2013
    public static bool deleteproductvideosbyid(int strprodid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_deleteproductforvideo_productvideo";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@prodid";
        param.Value = strprodid;
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
        return (result != -1);

    }


    //method on 14 aug to add embed code
 public static bool insertvideocode(decimal productid, string videotrailerembed, string vedioembed, string video_length, decimal video_viewcount, string movie_actor, string movie_producer)
       
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_product_videoinsertupdate";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@videotrailerembed";
        param.Value = videotrailerembed;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@vedioembed";
        param.Value = vedioembed;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@video_length";
        param.Value = video_length;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@video_viewcount";
        param.Value = video_viewcount;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@movie_actor";
        param.Value = movie_actor;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@movie_producer";
        param.Value = movie_producer;
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
public static bool updatevideocode(decimal videoid, decimal productid, string videotrailerembed, string vedioembed, string video_length, decimal video_viewcount, string movie_actor, string movie_producer)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_product_videoinsertupdate";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@videoid";
        param.Value = videoid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@videotrailerembed";
        param.Value = videotrailerembed;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@vedioembed";
        param.Value = vedioembed;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@video_length";
        param.Value = video_length;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@video_viewcount";
        param.Value = video_viewcount;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@movie_actor";
        param.Value = movie_actor;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@movie_producer";
        param.Value = movie_producer;
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
 public static bool deletemovieembed(decimal productid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_delete_product_video";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        int result = -1;
        try
        {
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch
        {
            // any errors are logged in GenericDataAccess, we ingore them here
        }
        // result will be 1 in case of success 
        return (result != -1);
    }

 public static DataTable getembedbyproductvideo(decimal strprodid)
 {
     DbCommand comm = GenericDataAccess.CreateCommand();
     comm.CommandText = "sp_proc_getembed_product_video";
     DbParameter param = comm.CreateParameter();
     param.ParameterName = "@productid";
     param.Value = strprodid;
     param.DbType = DbType.Decimal;
     comm.Parameters.Add(param);
     return GenericDataAccess.ExecuteSelectCommand(comm);
 }
}
