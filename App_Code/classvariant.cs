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
using System.Data.Common;

public struct variant
{


}
/// <summary>
/// Summary description for classvariant
/// </summary>
public class classvariant
{
	public classvariant()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable getaatribtewithcommabyattrid(int parentprdid,string attributeid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_getattribteidbycomma_variants";
        //comm.CommandText = "sp_getattribteidbycomma_variant";
        // execute the stored procedure and return the results
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = parentprdid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@attributeid";
        param.Value = attributeid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    //method on 13th may for display variant
    public static DataTable getproductcombination(int productId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getproductcombination";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@pProductId";
        param.Value = productId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        return dt;
    }

    //method on 16th may for display variant
    public static DataTable getattrgridbyattrid_variant(int attributeid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_getattribtegridbyattributeid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@attributeid ";
        param.Value = attributeid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        return dt;
    }

    //method on 19th may for get parentproductid
    public static DataTable getparentproductcombination(string productId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getparentidbycombination";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        return dt;
    }


    public static DataTable getchildproductcombination(string productId,string attrgrid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getchildidbycombinationsid1";
        //comm.CommandText = "sp_proc_getchildidbycombinations";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@attributegroupid";
        param.Value = attrgrid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        return dt;
    }

    public static DataTable getchildproductcombinations(string productId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getchildidbycombinationsid";
        //comm.CommandText = "sp_proc_getchildidbycombinations";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

       
        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        return dt;
    }

    //new method on 21may
    public static DataTable getaatribtewithcommabyattrid_variant(string parentprdid, string attributeid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_getattribteidbycomma_variantsid";
        //comm.CommandText = "sp_getattribteidbycomma_variant";
        // execute the stored procedure and return the results
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = parentprdid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@attributeid";
        param.Value = attributeid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getatriewithcommabyattrid(string parentprdid, string attributeid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_getattribteidbycomma_variants";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = parentprdid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@attributeid";
        param.Value = attributeid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    //get attributeid by attributegroupid
    public static DataTable getattributeidbyattrgrid(int attrgrdid, int prodid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
     
        comm.CommandText = "sp_getattridbgattrgrid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@attrgroupid";
        param.Value = attrgrdid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@prodid";
        param.Value = prodid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getattriebuteidbygroupid(string parentprdid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_getattribteidbycommaid_variants";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = parentprdid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

      
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable getcommaprodattributeid(string attributeid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetSelectedProductIDAttrsgrpid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@pAttrId ";
        param.Value = attributeid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

       
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getattributeidbyprodid_variant(string prodid, string attributegrid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_getattributeidbyproductandgroupid";
      
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = prodid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@prdGroupId";
        param.Value = attributegrid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public static DataTable getvariantdetails_procuct_combination(string prodid )
        {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand ( );
        // set the stored procedure name
       comm.CommandText = "usp_getvariantdetails_productcombinations";
        // comm.CommandText = "usp_getvariantdetails_productcombination";

        DbParameter param = comm.CreateParameter ( );
        param.ParameterName = "@productid";
        param.Value = prodid;
        param.DbType = DbType.String;
        comm.Parameters.Add ( param );
         // return enquiry list
        return GenericDataAccess.ExecuteSelectCommand ( comm );
        }


    
    public static DataTable getcommagroupattributeids( string attributeid )
        {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand ( );
        // set the stored procedure name
        comm.CommandText = "sp_proc_GetSelectedProductIDAttrsgrpidswithcomma";
        DbParameter param = comm.CreateParameter ( );
        param.ParameterName = "@pAttrId";
        param.Value = attributeid;
        param.DbType = DbType.String;
        comm.Parameters.Add ( param );

      
        return GenericDataAccess.ExecuteSelectCommand ( comm );
        }


    public static DataTable getdetailcombination( string productId )
        {
        DbCommand comm = GenericDataAccess.CreateCommand ( );
        comm.CommandText = "sp_proc_getdetailsbycombination";
        DbParameter param = comm.CreateParameter ( );
        param.ParameterName = "@productid";
        param.Value = productId;
        param.DbType = DbType.String;
        comm.Parameters.Add ( param );

        DataTable dt = GenericDataAccess.ExecuteSelectCommand ( comm );
        return dt;
        }
}