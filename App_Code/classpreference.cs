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
/// Summary description for classpreference
/// </summary>
/// 

public struct preferenceDetail
{
    
}
public class classpreference
{
	public classpreference()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    public static DataTable getAllAttributeGroup()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "getattributegroup_attributegroup";
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable getattributeidbyattrgrid_attribute(string attrgpid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // comm.CommandText = "sp_proc_attributes_movie";
        comm.CommandText = "getattributename_Attributes";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@attributegr";
        param.Value = attrgpid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static bool createpreference(string useremail,decimal movie_rate, string text_message, string textmessage_improve)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertpreference";

        DbParameter param = comm.CreateParameter();

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@useremail";
        param.Value = useremail;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@movie_rate";
        param.Value = movie_rate;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@text_message";
        param.Value = text_message;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@textmessage_improve";
        param.Value = textmessage_improve;
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


    public static bool updatepreferences(decimal id ,string useremail, decimal categoryid, decimal attributegr_id, decimal attribute_id, decimal movie_rate, string text_message, string textmessage_improve)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdatepreference";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@id";
        param.Value = id;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@useremail";
        param.Value = useremail;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@attributegr_id";
        param.Value = attributegr_id;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@attribute_id";
        param.Value = attribute_id;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@movie_rate";
        param.Value = movie_rate;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@text_message";
        param.Value = text_message;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@textmessage_improve";
        param.Value = textmessage_improve;
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
        return (result != -1);
    }
public static DataTable gettopprefid()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_toppreferenceid";
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
public static bool addcategorytopreference(decimal preferenceid,string useremail, decimal categoryid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertcategorytopreference";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@id";
        param.Value = preferenceid;
        param.DbType = DbType.Decimal;

        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@useremail";
        param.Value = useremail;
        param.DbType = DbType.String;
       

        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
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
public static bool addattributepreference(decimal preferenceid, string useremail, decimal attrid,decimal attrgr_id)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertattributepreference";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@id";
        param.Value = preferenceid;
        param.DbType = DbType.Decimal;

        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@useremail";
        param.Value = useremail;
        param.DbType = DbType.String;
       

        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@attributeid";
        param.Value = attrid;
        param.DbType = DbType.Decimal;
       

        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@attributegr_id";
        param.Value = attrgr_id;
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
 public static DataTable getuserprefcnt_preference(string useremail)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // comm.CommandText = "sp_proc_attributes_movie";
        comm.CommandText = "sp_getcountuserpreferences";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@useremail";
        param.Value = useremail;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

 public static bool deletepreferences_useremail(string useremail)
 {
     // get a configured DbCommand object
     DbCommand comm = GenericDataAccess.CreateCommand();
     // set the stored procedure name
     comm.CommandText = "sp_deleteuserpreferencesdetail_useremail";
     // create a new parameter
     DbParameter param = comm.CreateParameter();
     param.ParameterName ="@useremail";
     param.Value = useremail;
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
     return (result != -1);
 }

 public static DataTable getuserdetailsbuemail_preference(string useremail)
 {
     DbCommand comm = GenericDataAccess.CreateCommand();
     // comm.CommandText = "sp_proc_attributes_movie";
     comm.CommandText = "sp_uesrdetailsbyemail_useremail";
     //create new parameter
     DbParameter param = comm.CreateParameter();
     param = comm.CreateParameter();
     param.ParameterName = "@useremail";
     param.Value = useremail;
     param.DbType = DbType.String;
     comm.Parameters.Add(param);

     return GenericDataAccess.ExecuteSelectCommand(comm);
 }
 public static DataTable getallcategoryidbyuseremail_preference(string useremail)
 {
     DbCommand commd = GenericDataAccess.CreateCommand();
     commd.CommandText = "sp_proc_getcategoryByuseremail_preference";
     // create a new parameter
     DbParameter paramz = commd.CreateParameter();
     paramz.ParameterName = "@useremail";
     paramz.Value = useremail;
     paramz.DbType = DbType.String;
     commd.Parameters.Add(paramz);
     // execute the stored procedure
     DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
     return dtc;
 }
 public static DataTable getallattributeidbyuseremail_preference(string useremail)
 {
     DbCommand commd = GenericDataAccess.CreateCommand();
     commd.CommandText = "sp_proc_getattributeByuseremail_preference";
     // create a new parameter
     DbParameter paramz = commd.CreateParameter();
     paramz.ParameterName = "@useremail";
     paramz.Value = useremail;
     paramz.DbType = DbType.String;
     commd.Parameters.Add(paramz);
     // execute the stored procedure
     DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
     return dtc;
 }


 public static DataTable getsubcategorylist_productcategory()
 {
     // get a configured DbCommand object
     DbCommand comm = GenericDataAccess.CreateCommand();
     // set the stored procedure name
     comm.CommandText = "usp_getsubcategorylist_category";
     // execute the stored procedure and return the results
     // return GenericDataAccess.ExecuteSelectCmd(comm);
     return GenericDataAccess.ExecuteSelectCommand(comm);

 }


}