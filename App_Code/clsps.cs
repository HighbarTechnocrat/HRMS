using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for clsps
/// </summary>
public static class clsps
{
	static clsps()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //product seacrh
    public static DataTable SearchProducts(string DeviceID, string CategoryID, string BrandID, string AttributeID, string AttributGroupID, string FromPrice, string ToPrice, string AtoZ, string LowPrice, string HighPrice, string Search, string auction, int pageno, int pagesize)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_SearchProducts";
        DbParameter param = comm.CreateParameter();

        param = comm.CreateParameter();
        param.ParameterName = "@DeviceID";
        param.Value = DeviceID;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@CategoryID";
        param.Value = CategoryID;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@BrandID";
        param.Value = BrandID;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AttributeID";
        param.Value = AttributeID;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AttributGroupID";
        param.Value = AttributGroupID;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@FromPrice";
        param.Value = FromPrice;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@ToPrice";
        param.Value = ToPrice;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AtoZ";
        param.Value = AtoZ;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@LowPrice";
        param.Value = LowPrice;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@HighPrice";
        param.Value = HighPrice;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Search";
        param.Value = Search;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@auction";
        param.Value = auction;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
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

        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable SearchProductscount(string DeviceID, string CategoryID, string BrandID, string AttributeID, string AttributGroupID, string FromPrice, string ToPrice, string AtoZ, string LowPrice, string HighPrice, string Search, string auction,string recently)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
         comm.CommandText = "usp_Searchmoviescounts";
       //comm.CommandText = "usp_SearchProductscount";
        DbParameter param = comm.CreateParameter();

        param = comm.CreateParameter();
        param.ParameterName = "@DeviceID";
        param.Value = DeviceID;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@CategoryID";
        param.Value = CategoryID;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@BrandID";
        param.Value = BrandID;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AttributeID";
        param.Value = AttributeID;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AttributGroupID";
        param.Value = AttributGroupID;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@FromPrice";
        param.Value = FromPrice;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@ToPrice";
        param.Value = ToPrice;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AtoZ";
        param.Value = AtoZ;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@LowPrice";
        param.Value = LowPrice;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@HighPrice";
        param.Value = HighPrice;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Search";
        param.Value = Search;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@flag";
        param.Value = auction;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@recently";
        param.Value = recently;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable loadSearchProducts(string DeviceID, string CategoryID, string BrandID, string AttributeID, string AttributGroupID, string FromPrice, string ToPrice, string AtoZ, string LowPrice, string HighPrice, string Search, string flag,string recently, int pageno, int pagesize)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_Searchmovies_products";
        //comm.CommandText = "usp_loadSearchallProducts";

        //comm.CommandText = "usp_Searchmovies_product";
        //comm.CommandText = "usp_Searchmovies";
        DbParameter param = comm.CreateParameter();

        param = comm.CreateParameter();
        param.ParameterName = "@DeviceID";
        param.Value = DeviceID;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@CategoryID";
        param.Value = CategoryID;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@BrandID";
        param.Value = BrandID;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AttributeID";
        param.Value = AttributeID;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AttributGroupID";
        param.Value = AttributGroupID;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@FromPrice";
        param.Value = FromPrice;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@ToPrice";
        param.Value = ToPrice;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AtoZ";
        param.Value = AtoZ;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@LowPrice";
        param.Value = LowPrice;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@HighPrice";
        param.Value = HighPrice;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Search";
        param.Value = Search;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@flag";
        param.Value = flag;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@recently";
        param.Value = recently;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
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

        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable getbreadcumbnameattrname(string m_param)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "breadcumbnameattrname";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@param";
        param.Value = m_param;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable getbreadcumattrname(string m_param)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "breadcumattrname";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@param";
        param.Value = m_param;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    /* product search*/

    public static DataTable getparentbycategoryid()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_category_getcategoryid";
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }

    public static DataTable getcategorybyid(int categoryid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_category_getcategorybyid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@catid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure and return the results
        // return GenericDataAccess.ExecuteSelectCmd(comm);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }



}