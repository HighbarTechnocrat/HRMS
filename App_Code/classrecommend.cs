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
using System.Collections.Generic;

/// <summary>
/// Summary description for classrecommend
/// </summary>
public class classrecommend
{
	public classrecommend()
	{
		//
		// TODO: Add constructor logic here
		//
	}

 public static bool createuserproductcntbysearch(decimal productid,string useremail,decimal prdcount)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText ="insertuserproductcnt_product_count";

        DbParameter param = comm.CreateParameter();
       //create a new parameter
        param = comm.CreateParameter();
        param.ParameterName ="@productid";
        param.Value = productid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName ="@user_email";
        param.Value = useremail;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@product_count";
        param.Value = prdcount;
        param.DbType = DbType.Decimal;
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
        return (result >= 1);
    }
public static DataTable getproductcategorybyid(decimal parentid)
 {
     //get confired DbCommand Object
     DbCommand comm = GenericDataAccess.CreateCommand();
     //set the stored procedure
     comm.CommandText = "getproductcategorybyproductid_productcategory";
     //create new parameter
     DbParameter param = comm.CreateParameter();
     param.ParameterName = "@productid";
     param.Value = parentid;
     param.DbType = DbType.Decimal;
     comm.Parameters.Add(param);
     return GenericDataAccess.ExecuteSelectCommand(comm);
 }
public static DataTable getproductbycategorynameid(decimal parentid)
{
    //get confired DbCommand Object
    DbCommand comm = GenericDataAccess.CreateCommand();
    //set the stored procedure
    comm.CommandText = "sp_proc_getproductbycategorynameid";
    //create new parameter
    DbParameter param = comm.CreateParameter();
    param.ParameterName = "@productid";
    param.Value = parentid;
    param.DbType = DbType.Decimal;
    comm.Parameters.Add(param);
    return GenericDataAccess.ExecuteSelectCommand(comm);
}
public static bool createuserctegorycntbysearch(string useremail,decimal catid, decimal catcount)
 {
     // get a configured DbCommand object
     DbCommand comm = GenericDataAccess.CreateCommand();
     // set the stored procedure name
     comm.CommandText = "insertusercategorycnt_category_usercount";

     DbParameter param = comm.CreateParameter();

     // create a new parameter
   
     param = comm.CreateParameter();
     param.ParameterName = "@user_email";
     param.Value = useremail;
     param.DbType = DbType.String;
     comm.Parameters.Add(param);

     param = comm.CreateParameter();
     param.ParameterName = "@categoryid";
     param.Value = catid;
     param.DbType = DbType.Decimal;
     comm.Parameters.Add(param);


     param = comm.CreateParameter();
     param.ParameterName = "@cat_count";
     param.Value = catcount;
     param.DbType = DbType.Decimal;
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
     return (result >= 1);
 }
public static DataTable getproductusercount(string username,decimal productid)
 {
     DbCommand comm = GenericDataAccess.CreateCommand();
     comm.CommandText = "getproductcount_product_usercount";
     DbParameter param = comm.CreateParameter();
     param.ParameterName = "@useremail";
     param.Value = username;
     param.DbType = DbType.String;
     comm.Parameters.Add(param);

     param = comm.CreateParameter();
     param.ParameterName = "@productid";
     param.Value = productid;
     param.DbType = DbType.Decimal;
     comm.Parameters.Add(param);

     return GenericDataAccess.ExecuteSelectCommand(comm);
 }
public static bool updateuserproductcntbysearch(decimal productid, string useremail, decimal prdcount)
{
    // get a configured DbCommand object
    DbCommand comm = GenericDataAccess.CreateCommand();
    // set the stored procedure name
    comm.CommandText = "updateuserproductcnt_product_count";

    DbParameter param = comm.CreateParameter();
    //create a new parameter
    param = comm.CreateParameter();
    param.ParameterName = "@productid";
    param.Value = productid;
    param.DbType = DbType.Decimal;
    comm.Parameters.Add(param);


    param = comm.CreateParameter();
    param.ParameterName = "@user_email";
    param.Value = useremail;
    param.DbType = DbType.String;
    comm.Parameters.Add(param);

    param = comm.CreateParameter();
    param.ParameterName = "@product_count";
    param.Value = prdcount;
    param.DbType = DbType.Decimal;
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
    return (result >= 1);
}
public static DataTable getproductusercountb_product_usercount(decimal productid, string username)
{
    DbCommand comm = GenericDataAccess.CreateCommand();
    comm.CommandText = "sp_proc_Getcountuserproduct_product_usercount";
    DbParameter param = comm.CreateParameter();
    param.ParameterName = "@productid";
    param.Value = productid;
    param.DbType = DbType.Decimal;
    comm.Parameters.Add(param);

    param = comm.CreateParameter();
    param.ParameterName = "@username";
    param.Value = username;
    param.DbType = DbType.String;
    comm.Parameters.Add(param);

    return GenericDataAccess.ExecuteSelectCommand(comm);
}
public static bool updateusercategorycntbysearch(decimal catid, string useremail, decimal catcount)
{
    // get a configured DbCommand object
    DbCommand comm = GenericDataAccess.CreateCommand();
    // set the stored procedure name
    comm.CommandText ="updateusercategorycnt_product_count";

    DbParameter param = comm.CreateParameter();
    //create a new parameter
    param = comm.CreateParameter();
    param.ParameterName = "@category_id";
    param.Value = catid;
    param.DbType = DbType.Decimal;
    comm.Parameters.Add(param);


    param = comm.CreateParameter();
    param.ParameterName ="@user_email";
    param.Value = useremail;
    param.DbType = DbType.String;
    comm.Parameters.Add(param);

    param = comm.CreateParameter();
    param.ParameterName = "@category_count";
    param.Value = catcount;
    param.DbType = DbType.Decimal;
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
    return (result >= 1);
}
public static DataTable getcatcountuserbysearch_category_usercount(string catid, string username)
{
    DbCommand comm = GenericDataAccess.CreateCommand();
    comm.CommandText = "sp_getcountcategoryid_category_count";
    DbParameter param = comm.CreateParameter();
    param.ParameterName = "@catid";
    param.Value = catid;
    param.DbType = DbType.String;
    comm.Parameters.Add(param);

    param = comm.CreateParameter();
    param.ParameterName = "@username";
    param.Value = username;
    param.DbType = DbType.String;
    comm.Parameters.Add(param);

    return GenericDataAccess.ExecuteSelectCommand(comm);
}
public static DataTable getcategoryparentcount(int catid)
{
    DbCommand comm = GenericDataAccess.CreateCommand();
   // comm.CommandText = "sp_proc_category_getparentcategorycount";
    comm.CommandText = "sp_proc_category_getparentidcategorycount";
    DbParameter param = comm.CreateParameter();
    param.ParameterName = "@catid";
    param.Value = catid;
    param.DbType = DbType.Int32;
    comm.Parameters.Add(param);
    return GenericDataAccess.ExecuteSelectCommand(comm);
}
public static DataTable getallcategorybypreferencesbyusername(string useremail)
{
    DbCommand commd = GenericDataAccess.CreateCommand();
    commd.CommandText = "sp_proc_getallcategorybypreferencesbyusername";
    // create a new parameter
    DbParameter paramz = commd.CreateParameter();
    paramz.ParameterName = "@username";
    paramz.Value = useremail;
    paramz.DbType = DbType.String;
    commd.Parameters.Add(paramz);
    // execute the stored procedure
    DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
    return dtc;
}

public static DataTable getallattributesbypreferencesbyusername(string useremail)
{
    DbCommand commd = GenericDataAccess.CreateCommand();
    commd.CommandText = "sp_proc_getallattributesbypreferencesbyusername";
    // create a new parameter
    DbParameter paramz = commd.CreateParameter();
    paramz.ParameterName = "@username";
    paramz.Value = useremail;
    paramz.DbType = DbType.String;
    commd.Parameters.Add(paramz);
    // execute the stored procedure
    DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
    return dtc;
}
public static DataTable getproductidbyattributesdisplaypreference(string arttrid, string username)
{
    DbCommand commd = GenericDataAccess.CreateCommand();
    commd.CommandText = "sp_getmoviebyattributeid";
    // create a new parameter
    DbParameter paramz = commd.CreateParameter();
    paramz.ParameterName = "@attributeid";
    paramz.Value = arttrid;
    paramz.DbType = DbType.String;
    commd.Parameters.Add(paramz);

    paramz = commd.CreateParameter();
    paramz.ParameterName = "@username";
    paramz.Value = username;
    paramz.DbType = DbType.String;
    commd.Parameters.Add(paramz);
    // execute the stored procedure
    DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
    return dtc;
}

public static DataTable getcategoryusercount(string username, decimal categoryid)
{
    DbCommand comm = GenericDataAccess.CreateCommand();
    comm.CommandText = "getcategorycount_category_usercount";
    DbParameter param = comm.CreateParameter();
    param.ParameterName = "@useremail";
    param.Value = username;
    param.DbType = DbType.String;
    comm.Parameters.Add(param);

    param = comm.CreateParameter();
    param.ParameterName ="@categoryid";
    param.Value = categoryid;
    param.DbType = DbType.Decimal;
    comm.Parameters.Add(param);

    return GenericDataAccess.ExecuteSelectCommand(comm);
}

public static DataTable getcategoryusercountb_category_usercount(decimal categoryid, string username)
{
    DbCommand comm = GenericDataAccess.CreateCommand();
    comm.CommandText = "sp_proc_Getcountusercategory_category_usercount";
    DbParameter param = comm.CreateParameter();
    param.ParameterName = "@categoryid";
    param.Value = categoryid;
    param.DbType = DbType.Decimal;
    comm.Parameters.Add(param);

    param = comm.CreateParameter();
    param.ParameterName = "@username";
    param.Value = username;
    param.DbType = DbType.String;
    comm.Parameters.Add(param);

    return GenericDataAccess.ExecuteSelectCommand(comm);
}


public static bool updatecategory_category_userupdateparentcount(int catid, string useremail)
{
    // get a configured DbCommand object
    DbCommand comm = GenericDataAccess.CreateCommand();
    // set the stored procedure name
    comm.CommandText = "usp_getparentcategorycntbycatidandupdatecatcount";

    DbParameter param = comm.CreateParameter();
    //create a new parameter
    param = comm.CreateParameter();
    param.ParameterName ="@catid";
    param.Value = catid;
    param.DbType = DbType.Int32;
    comm.Parameters.Add(param);


    param = comm.CreateParameter();
    param.ParameterName = "@username";
    param.Value = useremail;
    param.DbType = DbType.String;
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
    return (result >= 1);
}

public static DataTable fetchcategoryidbycategoryusercountnotinpreference(string username)
{
    DbCommand comm = GenericDataAccess.CreateCommand();
    comm.CommandText = "getcategorybyusersearchcountdescend_categoryusrcnt";
    DbParameter param = comm.CreateParameter();
    param.ParameterName = "@user_email";
    param.Value = username;
    param.DbType = DbType.String;
    comm.Parameters.Add(param);


    return GenericDataAccess.ExecuteSelectCommand(comm);
}

public static DataTable getuserproductonrecommend(string username)
{
    DbCommand comm = GenericDataAccess.CreateCommand();
    comm.CommandText = "sp_proc_getmoviefromproductsusercount_user_productcount";

    DbParameter param = comm.CreateParameter();
    //create a new parameter
    param = comm.CreateParameter();
    param.ParameterName = "@uesrname";
    param.Value = username;
    param.DbType = DbType.String;
    comm.Parameters.Add(param);
    DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
    return table;
}

public static DataTable getproductidbycatidsdisplay_recommendation(string catid,string username)
{
    DbCommand commd = GenericDataAccess.CreateCommand();
    commd.CommandText = "sp_getmoviebycatgoryid_recommend";
    // create a new parameter
    DbParameter paramz = commd.CreateParameter();
    paramz.ParameterName = "@catgid";
    paramz.Value = catid;
    paramz.DbType = DbType.String;
    commd.Parameters.Add(paramz);

    paramz = commd.CreateParameter();
    paramz.ParameterName ="@username";
    paramz.Value = username;
    paramz.DbType = DbType.String;
    commd.Parameters.Add(paramz);

    // execute the stored procedure
    DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
    return dtc;
}


public static DataTable getproductsbyvisitcount_product(string username)
{
    DbCommand comm = GenericDataAccess.CreateCommand();
    comm.CommandText = "getdproductbyvisitcount_product";
    DbParameter paramz = comm.CreateParameter();
    paramz.ParameterName = "@uesrname";
    paramz.Value = username;
    paramz.DbType = DbType.String;
    comm.Parameters.Add(paramz);


    return GenericDataAccess.ExecuteSelectCommand(comm);
}

}