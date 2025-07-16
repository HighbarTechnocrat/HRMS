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
/// Summary description for classcategory
/// </summary>
public struct CategoryDetail
{
    public int categoryid;
    public string categoryname;
    public string shortdescription;
    public string logdescription;
    public string campaignname;
    public string catimage;
    public int parentid;
    public string status;
    public string catstatus;
    public string catfeature;
    public string strshowonhomepage;
    public int priority;
}
public struct Categorycountmapping
{
   
    public int usercount;
    
}
public struct ParentCategorymapping
{

    public string categoryname;

}



public class classcategory
{
  
	public classcategory()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static bool categoryadd(string name, string shortdesc, string longdesc, int parentid, string image, char viewstatus, string modifiedby)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_categoryinsertupdateitem";
        DbParameter param = comm.CreateParameter();
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@categoryname";
        param.Value = name;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@shortdescription";
        param.Value = shortdesc;
        param.DbType = DbType.String;
        //param.Size = 1000;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@longdescription";
        param.Value = longdesc;
        param.DbType = DbType.String;
        //param.Size = 4000;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@parentid";
        if (parentid == 0)
        {
            param.Value = DBNull.Value;
        }
        else
        {
            param.Value = parentid;
        }
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@catimage";
        param.Value = image;
        param.DbType = DbType.String;
        param.Size = 50;
        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@status";
        param.Value = viewstatus;
        param.DbType = DbType.String;
        param.Size = 1;
        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@modifiedby";
        param.Value = modifiedby;
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
    
    public static void Addcategory(string category, string createdby)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_categoryinsertupdateitem";
        // execute the stored procedure and return the results
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryname";
        param.Value = category;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@createdby";
        param.Value = createdby;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        GenericDataAccess.ExecuteNonQuery(comm);
    }
   
    public static bool categoryadd(string name, string shortdesc, string longdesc, string campaignname, int parentid, string image, char viewstatus, char catstatus, string modifiedby, char catfeature, char strshowonHpage)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_categoryinsertupdateitem";
        DbParameter param = comm.CreateParameter();
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@categoryname";
        param.Value = name;
        param.DbType = DbType.String;
        //param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@shortdescription";
        param.Value = shortdesc;
        param.DbType = DbType.String;
        //param.Size = 1000;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@longdescription";
        param.Value = longdesc;
        param.DbType = DbType.String;
        //param.Size = 4000;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@campaignname";
        param.Value = campaignname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@parentid";
        if (parentid == 0)
        {
            param.Value = DBNull.Value;
        }
       
        else
        {
            param.Value = parentid;
        }
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@catimage";
        param.Value = image;
        param.DbType = DbType.String;
        //param.Size = 50;
        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@status";
        param.Value = viewstatus;
        param.DbType = DbType.String;
        //param.Size = 1;
        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@catstatus";
        param.Value = catstatus;
        param.DbType = DbType.String;
        //param.Size = 1;
        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@modifiedby";
        param.Value = modifiedby;
        param.DbType = DbType.String;
        
        //param.Size=1;
       // comm.Parameters.Add(param);
        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@catfeature";
        param.Value = catfeature;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@showonHpage";
        param.Value = strshowonHpage;
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
    public static DataTable getAllAttributeByAttributeGroupId(int categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getAllAttributeByAttributeGroupId";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@AttributeGroupId";
        paramp.Value = categoryid;
        paramp.DbType = DbType.Int32;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable getfeaturedbrandlist(int categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getfeaturedbrandlist";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@categoryid";
        paramp.Value = categoryid;
        paramp.DbType = DbType.Int32;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataSet getcategorylistbyvendor(string vendorname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getcategorylistbyvendordropdown";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCmd1(comm);

    }
    public static DataSet getcategorycitybycityname(string cityname, string categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getcategorycitybycityname";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@cityname";
        param.Value = cityname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        param = comm.CreateParameter();

        //param = comm.CreateParameter();
        //param.ParameterName = "@country";
        //param.Value = country;
        //param.DbType = DbType.String;
        //comm.Parameters.Add(param);
        //param = comm.CreateParameter();

        //param = comm.CreateParameter();
        //param.ParameterName = "@state";
        //param.Value = state;
        //param.DbType = DbType.String;
        //comm.Parameters.Add(param);
        //param = comm.CreateParameter();

        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCmd1(comm);

    }
    public static DataSet getcategorycitycount( string categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getcategorycitycount";
        DbParameter param = comm.CreateParameter();
         param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        return GenericDataAccess.ExecuteSelectCmd1(comm);

    }
    public static DataTable Getcategorylist()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcategorylist";
        // execute the stored procedure and return the results
       // return GenericDataAccess.ExecuteSelectCmd(comm);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataTable Getcategorylist_add_product()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcategorylist_Add_Product";
        // execute the stored procedure and return the results
        // return GenericDataAccess.ExecuteSelectCmd(comm);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataSet GetcategorylistBrand()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcategorylist";
        // execute the stored procedure and return the results
        // return GenericDataAccess.ExecuteSelectCmd(comm);
        return GenericDataAccess.ExecuteSelectCmd1(comm);

    }
    public static DataSet Getcategorylist1()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        ///////////commented by Pramod on 30th Oct 2012 comm.CommandText = "sp_proc_getcategorylist";

        comm.CommandText = "sp_proc_getcategorylist_mini_Use";
        // execute the stored procedure and return the results
        return GenericDataAccess.ExecuteSelectCmd(comm);
    }
    public static DataSet Getsubcategorylist()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getsubcategorylist";
        // execute the stored procedure and return the results
        return GenericDataAccess.ExecuteSelectCmd(comm);

    }
    public static DataSet Getprcategory()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getprcategory";
        // execute the stored procedure and return the results
        return GenericDataAccess.ExecuteSelectCmd(comm);

    }    
    public static DataSet Getsubcategory()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_Getsubcategory";
        // execute the stored procedure and return the results
        return GenericDataAccess.ExecuteSelectCmd(comm);

    }
    public static DataSet Getsubcategory2()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_Getsubcategory2";
        // execute the stored procedure and return the results
        return GenericDataAccess.ExecuteSelectCmd(comm);

    }
    public static DataTable selectcategory()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "Sp_proc_Selectcategory ";
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataSet GetParentcatlist()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getparentcatlist";
        // execute the stored procedure and return the results
        return GenericDataAccess.ExecuteSelectCmd(comm);

    }    
    public static DataTable Getcatlist()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcategorylist";
        // execute the stored procedure and return the results
        // return faq list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable Getcategoryparentlist()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcategoryparentlist";
        // execute the stored procedure and return the results
        // return faq list
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataSet GetAllcategorylist()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getAllcategorylist";
        // execute the stored procedure and return the results
        return GenericDataAccess.ExecuteSelectCmd(comm);

    }
    public static DataSet GetAllcategorylistbanner()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcategorylistbanner";
        // execute the stored procedure and return the results
        return GenericDataAccess.ExecuteSelectCmd(comm);

    }
    public static CategoryDetail getcategorydetails(int categoryId)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcategorydetails";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryId";
        param.Value = categoryId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // wrap retrieved data into a DepartmentDetails object
        CategoryDetail details = new CategoryDetail();
        if (table.Rows.Count > 0)
        {
            details.categoryid = Convert.ToInt32(table.Rows[0]["categoryid"]);
            details.categoryname = table.Rows[0]["categoryname"].ToString();
            details.catimage = table.Rows[0]["catimage"].ToString();
            details.shortdescription = table.Rows[0]["shortdescription"].ToString();
            details.logdescription = table.Rows[0]["longdescription"].ToString();
           // details.campaignname = Convert.ToString(table.Rows[0]["campaignname"]);
            if (table.Rows[0]["parentid"].ToString() != "")
            {
                details.parentid = Convert.ToInt32(table.Rows[0]["parentid"]);
            }
            else
            {
                details.parentid = 0;
            }
            //details.status = table.Rows[0]["status"].ToString();
            details.catstatus = table.Rows[0]["catstatus"].ToString();
           // details.catfeature = table.Rows[0]["catfeature"].ToString();
          //  details.strshowonhomepage = table.Rows[0]["showonHpage"].ToString();
            //if (table.Rows[0]["priority"] != DBNull.Value)
            //{
            //    details.priority = Convert.ToInt32(table.Rows[0]["priority"].ToString());
            //}
            //else
            //{
            //    details.priority = 0;
            //}
        }
        // return department details
        return details;
    }
    public static bool updatecategory(int categoryid, string name, string shortdesc, string longdesc, string campaignname, int parentid, string image, char viewstatus, char catstatus, string modifiedby,int priority,char catfeature, char strshowonHpage )
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_categoryinsertupdateitem";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@categoryname";
        param.Value = name;
        param.DbType = DbType.String;
        //param.Size = 50;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@shortdescription";
        param.Value = shortdesc;
        param.DbType = DbType.String;
        //param.Size = 1000;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@longdescription";
        param.Value = longdesc;
        param.DbType = DbType.String;
        //param.Size = 4000;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@campaignname";
        param.Value = campaignname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
       
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@parentid";
        param.Value = parentid;
       
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
       
        
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@catimage";
        param.Value = image;
        param.DbType = DbType.String;
        //param.Size = 50;
        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@status";
        param.Value = viewstatus;
        param.DbType = DbType.String;
        //param.Size = 1;
        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@catstatus";
        param.Value = catstatus;
        param.DbType = DbType.String;
        //param.Size = 1;
        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@modifiedby";
        param.Value = modifiedby;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        //param.Size=1;
        param = comm.CreateParameter();
        param.ParameterName = "@priority";
        param.Value = priority;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@catfeature";
        param.Value = catfeature;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@showonHpage";
        param.Value = strshowonHpage;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

               

        // result will represent the number of changed rows
        int result = -1;
        //try
        //{
            // execute the stored procedure
            result = GenericDataAccess.ExecuteNonQuery(comm);
        //}
        //catch
        //{
        //    // any errors are logged in GenericDataAccess, we ingore them here
        //}
        // result will be 1 in case of success 
        return (result != -1);
    }
    //public static int prioritycategory(int categoryid)
    //{
    //    DbCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = "sp_proc_deletecategory";
    //    DbParameter param = comm.CreateParameter();
    //    param.ParameterName = "@CategoryId";
    //    param.Value = categoryid;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);
    //    int result = -1;
    //    try
    //    {
    //        result = GenericDataAccess.ExecuteNonQuery(comm);
    //    }
    //    catch
    //    {
    //    }
    //    return result;

    //}
    public static int deletecitybycaegoryid(int CategoryId)
    {
        DbCommand cmd = GenericDataAccess.CreateCommand();
        cmd.CommandText = "sp_proc_deletecitybycaegoryid";
        DbParameter param = cmd.CreateParameter();
        param.ParameterName = "CategoryId";
        param.Value = CategoryId;
        param.DbType = DbType.Int32;
        cmd.Parameters.Add(param);

        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(cmd);

        }
        catch
        {
        }
        return result;
    }
    public static int deletestatebycaegoryid(int CategoryId)
    {
        DbCommand cmd = GenericDataAccess.CreateCommand();
        cmd.CommandText = "sp_proc_deletestatebycaegoryid";
        DbParameter param = cmd.CreateParameter();
        param.ParameterName = "CategoryId";
        param.Value = CategoryId;
        param.DbType = DbType.Int32;
        cmd.Parameters.Add(param);

        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(cmd);

        }
        catch
        {
        }
        return result;
    }
    public static int deletecategory(int id)
    {        
        DbCommand comm = GenericDataAccess.CreateCommand();   
        comm.CommandText = "sp_proc_deletecategory";      
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@CategoryId";
        param.Value = id;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        int result;
        //try
        //{
            result = GenericDataAccess.ExecuteNonQuery(comm);
       // }
       // catch
        //{            
       // }        
        return result;

    }
    public static bool deletecategorydev(int deviceid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deletecategorydev";
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@deviceid";
        param.Value = deviceid;
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
    public static bool adddevicetocategory(int categoryid, int deviceid)
    {

        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertdevicetocategory";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@deviceid";
        param.Value = deviceid;
        param.DbType = DbType.Int32;
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
    public static DataSet getmaincatlist()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getmaincatlist";
        // execute the stored procedure and return the results
        return GenericDataAccess.ExecuteSelectCmd(comm);

    }
    public static DataSet getmaincatlists()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getmaincatlists";
        // execute the stored procedure and return the results
        return GenericDataAccess.ExecuteSelectCmd(comm);

    }
    public static DataTable getmaincatlistfeature()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getmaincatlistfeature";
        // execute the stored procedure and return the results
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataTable selectsubcategory(int categoryid)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_selectsubcategory";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        //extcute the stored procedure and return result
        //return faq list based on faqcatid
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataTable getdefaultcategory()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getparentcategory";     
        //execute the stored procedure
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable getdefaultcategorycampaign(string campaignname)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getparentcategory_campaign";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@campaignname";
        param.Value = campaignname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        //extcute the stored procedure and return result
        //return faq list based on faqcatid
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable getallcategory(int catid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getallcategory";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@catid";
        param.Value = catid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // calculate how many pages of products and set the out parameter
        // return the page of products
        return table;
    }
    public static DataTable getallcategorybyid(int catid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getallcategorybyid";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@catid";
        param.Value = catid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // calculate how many pages of products and set the out parameter
        // return the page of products
        return table;
    }
    public static DataTable getAllCategoryBydeviceId(int deviceid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getAllCategoryBydeviceId";
        // create a new parameter
        DbParameter paramc = comm.CreateParameter();
        paramc.ParameterName = "@deviceid";
        paramc.Value = deviceid;
        paramc.DbType = DbType.Int32;
        comm.Parameters.Add(paramc);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public static DataTable getAllCategoryBybrandIdmenu(int deviceid,string brandid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getAllCategoryBybrandWithMenu";
        // create a new parameter
        DbParameter paramc = comm.CreateParameter();
        paramc.ParameterName = "@deviceid";
        paramc.Value = deviceid;
        paramc.DbType = DbType.Int32;
        comm.Parameters.Add(paramc);

        paramc = comm.CreateParameter();
        paramc.ParameterName = "@brandid";
        paramc.Value = brandid;
        paramc.DbType = DbType.String;
        comm.Parameters.Add(paramc);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    //public static DataTable getAllCategoryBybrandIdAttributeIdmenu(int deviceid, string brandid,string Attributeid)
    //{
    //    DbCommand comm = GenericDataAccess.CreateCommand();
    //    // set the stored procedure name
    //    comm.CommandText = "sp_proc_getAllCategoryBybrandByAttrWithMenu";
    //    // create a new parameter
    //    DbParameter paramc = comm.CreateParameter();
    //    paramc.ParameterName = "@deviceid";
    //    paramc.Value = deviceid;
    //    paramc.DbType = DbType.Int32;
    //    comm.Parameters.Add(paramc);

    //    paramc = comm.CreateParameter();
    //    paramc.ParameterName = "@brandid";
    //    paramc.Value = brandid;
    //    paramc.DbType = DbType.String;
    //    comm.Parameters.Add(paramc);

    //    paramc = comm.CreateParameter();
    //    paramc.ParameterName = "@Attribute";
    //    paramc.Value = Attributeid;
    //    paramc.DbType = DbType.String;
    //    comm.Parameters.Add(paramc);
    //    return GenericDataAccess.ExecuteSelectCommand(comm);

    //}

    public static DataTable CategoryAttributes(string CategoryID)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_CategoryAttributes";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@CategoryID";
        paramp.Value = CategoryID;
        paramp.DbType = DbType.String;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }    
    public static DataTable getAllAttributeBycategoryId(int categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getAllAttributeBycategoryId";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@categoryid";
        paramp.Value = categoryid;
        paramp.DbType = DbType.Int32;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataTable getAllShopsBycategoryId(int categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getAllShopsBycategoryId";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@categoryid";
        paramp.Value = categoryid;
        paramp.DbType = DbType.Int32;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataTable getAllVendorsBycategoryId(int categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getAllVendorsBycategoryId";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@categoryid";
        paramp.Value = categoryid;
        paramp.DbType = DbType.Int32;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataTable getAllBrandBycategoryId(int categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getAllBrandBycategoryId";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@categoryid";
        paramp.Value = categoryid;
        paramp.DbType = DbType.Int32;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

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
    public static DataTable getcategoryidByproductID(int productid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getcategoryidByproductID";
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
    public static bool deletecategory_category(int productid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deleteproduct_category";
        DbParameter param = comm.CreateParameter();
        param = comm.CreateParameter();
        param.ParameterName = "@productid";
        param.Value = productid;
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
    public static CategoryDetail getgrattributes(int categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();       
        comm.CommandText = "sp_proc_getAllAttributes";       
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);      
        CategoryDetail details = new CategoryDetail();
        if (table.Rows.Count > 0)
        {           
            DataRow dr = table.Rows[0];
            details.categoryid = Convert.ToInt32(dr["CategoryId"]);         

        }
        return details;

    }
    public static DataTable getshowonHpagecategory()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_getshowonHpagecategory";
        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // return the page of products
        return table;
    }
    public static bool createcategorycountry(int categoryid, string vendorname, string createdby, int countryid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdatecountry";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@createdby";
        param.Value = createdby;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@countryid";
        param.Value = countryid;
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
    public static bool createcategorystate(int categoryid, int stateid, string vendorname, string createdby)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdatestate";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@stateid";
        param.Value = stateid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@createdby";
        param.Value = createdby;
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
    public static bool createcategorycity(int categoryid, int cityid, string vendorname, string createdby)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_insertupdatecity";

        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@cityid";
        param.Value = cityid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@createdby";
        param.Value = createdby;
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
    public static DataTable getcategorycountry(int categoryid, string vendorname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcategorycountry1";
        // create a new parameter
        DbParameter param = comm.CreateParameter();

        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;

    }
    public static DataTable getcategorystate(int categoryid, string vendorname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcategorystate1";
        // create a new parameter
        DbParameter param = comm.CreateParameter();

        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);

        return table;

    }
    public static DataTable getcategorycity(int categoryid, string vendorname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcategorycity1";
        // create a new parameter
        DbParameter param = comm.CreateParameter();

        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }
    public static DataTable getcategorymapping(string vendorname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcategorymapping";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;

    }
    public static bool deletecategorycountry(int categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_deletecategorycountry";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
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
    public static bool deletecategorystate(string vendorname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_deletecategorystate";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
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
    public static bool deletecategorycity(string vendorname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_deletecategorycity";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
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

    public static Categorycountmapping getcountcategorymapping(string vendorname)
    {
        //bool flag;
        int flag = 0;
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcountcategorymapping";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // execute the stored procedure
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // wrap retrieved data into a DepartmentDetails object
        Categorycountmapping details = new Categorycountmapping();
        if (table.Rows.Count > 0)
        {
             details.usercount = Convert.ToInt32(table.Rows[0]["usercount"]);           
        }
        // return department details
        return details;
    }

    public static DataTable getfeaturecategorybycatid(int categoryid,int itemperpage,int pagenumber)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_getfeaturecategorybycatid";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@ItemsPerPage";
        param.Value = itemperpage;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@PageNo";
        param.Value = pagenumber;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }

    public static int getPaginationcategory(int categoryid, int itemperpage, int LastNo)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_PaginationCategoryId";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@ItemsPerPage";
        param.Value = itemperpage;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@LastPageNo";
        param.Value = LastNo;
        param.Direction = ParameterDirection.Output;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        LastNo = Int32.Parse(comm.Parameters["@LastPageNo"].Value.ToString());
        return LastNo;
    }
    public static DataTable getparentcategorylist(int parentid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getparentcategorylist";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@parentid";
        paramp.Value = parentid;
        paramp.DbType = DbType.Int32;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
   /* public static DataTable getparentcategorylist1(int parentid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getparentcategorylist1";
        // create a new parameter
        DbParameter paramp = comm.CreateParameter();
        paramp.ParameterName = "@parentid";
        paramp.Value = parentid;
        paramp.DbType = DbType.Int32;
        comm.Parameters.Add(paramp);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    */ 

    public static ParentCategorymapping getparentcategorylist1(int parentid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getparentcategorylist1";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@parentid";
        param.Value = parentid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        ParentCategorymapping details = new ParentCategorymapping();
        if (table.Rows.Count > 0)
        {
            DataRow dr = table.Rows[0];
          
            details.categoryname = Convert.ToString(dr["categoryname"]);

        }
        return details;

    }


    public static bool deletecategorycountry1(string vendorname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_deletecategorycountry1";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
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


    public static DataTable getcategorycountry1(string vendorname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcategorycountry";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;

    }
    public static DataTable getcategorystate1(string vendorname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcategorystate";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;

    }
    public static DataTable getcategorycity1(string vendorname)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcategorycity";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }

    public static ParentCategorymapping getcategorydetailsbycatid(int categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getcategorydetailsbycatid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryId";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        ParentCategorymapping details = new ParentCategorymapping();
        if (table.Rows.Count > 0)
        {
            DataRow dr = table.Rows[0];
            details.categoryname = Convert.ToString(dr["categoryname"]);
        }
        return details;
    }


    public static DataTable getcategorymappingload(string vendorname, int categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcategorymappingload";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }

    public static DataTable selectategorys(int categoryid)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_getcategorylists";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        //extcute the stored procedure and return result
        //return faq list based on faqcatid
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }


    public static DataTable Gethomepagecategorylist()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();       
        comm.CommandText = "sp_proc_getcategoryparentlist";      
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static List<int> getproductncatcountBycategoruId(int categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "getproductncatcountBycategoruId";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
       
        param = comm.CreateParameter();
        param.ParameterName = "@TotalRowsproduct";
        param.Direction = ParameterDirection.Output;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@TotalRowscategory";
        param.Direction = ParameterDirection.Output;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        List<int> list = new List<int>();

        // execute the stored procedure and save the results in a DataTable
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        // calculate how many pages of products and set the out parameter
        int TotalRowsproduct = Int32.Parse(comm.Parameters["@TotalRowsproduct"].Value.ToString());
        int TotalRowscategory = Int32.Parse(comm.Parameters["@TotalRowscategory"].Value.ToString());
        list.Add(TotalRowsproduct);
        list.Add(TotalRowscategory);
        return list;
       
    }
    public static DataTable selecTopTensubcategoryBycatid(int categoryid,out int CatId)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_selecTopTensubcategoryBycatid";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@catId";
        param.Direction = ParameterDirection.Output;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        //extcute the stored procedure and return result
        //return faq list based on faqcatid
        DataTable Dt = GenericDataAccess.ExecuteSelectCommand(comm);
        CatId = Int32.Parse(comm.Parameters["@catId"].Value.ToString());
        return Dt;
    }
    public static DataTable getbrandbymulticatid(string categoryid)
    {        
        DbCommand comm = GenericDataAccess.CreateCommand();     
        comm.CommandText = "sp_proc_getbrandbymulticatid";       
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);    
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static string checksubcategorybycatid(int categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_checksubcategorybycatid";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        string strreturn =  GenericDataAccess.ExecuteScalar(comm);
        return strreturn;
    }
    public static DataTable Gettop2categorylist()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_gettop2categorylist";
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public static DataTable Gettop2brandlist()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_gettop2brandlist";
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    
   // public static DataTable getparentcategorybycatid(int categoryid, out string catname, out string parentname, out string outcatid)
    public static DataTable getparentcategorybycatid(int categoryid)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_getparentcategorybycatid";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable Dt = GenericDataAccess.ExecuteSelectCommand(comm);    
        return Dt;
    }

    public static DataTable getfulldirectory()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "usp_getfulldirectory";
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
    public static DataTable  getcategorylistexceptchild(int categoryid)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "usp_getcategorylistexceptchild";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@category";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable Dt = GenericDataAccess.ExecuteSelectCommand(comm);    
        return Dt;
    }


    public static bool deletecatproducts(int categoryid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_deletecatproducts";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        int result = -1;

        result = GenericDataAccess.ExecuteNonQuery(comm);


        return (result != -1);
    }


    public static DataTable chkservicelocation(int productid, int indexid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_chkservicelocation";
        // create a new parameter
        DbParameter param = comm.CreateParameter();

        param.ParameterName = "@productid";
        param.Value = productid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@indexid";
        param.Value = indexid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;

    }

    public static DataTable getparentfulldirectory(int pcatid)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_getparentfulldirectory";
        //create new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@pcatid";
        param.Value = pcatid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable Dt = GenericDataAccess.ExecuteSelectCommand(comm);
        return Dt;
    }

    //new 
    public static DataTable getfeaturecategorybycatid1(int categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_getfeaturecategorybycatid1";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;
    }



    
    public static bool deletecategorybycountry(int categoryid,int countryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_deletecategorybycountry";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@countryid";
        param.Value = countryid;
        param.DbType = DbType.Int32;
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






    public static bool deleteuserbycountry(int countryid, int categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_deleteuserbycountry";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@countryid";
        param.Value = countryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
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

    public static bool deleteuserbystate(int stateid, int categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_deleteuserbystate";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@stateid";
        param.Value = stateid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
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

    public static bool deleteuserbycity(int cityid, int categoryid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_deleteuserbycity";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@cityid";
        param.Value = cityid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
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

    public static DataTable getcategorycountrybycatid(string vendorname,int catid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcategorycountrybycatid";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = catid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;

    }

    public static DataTable getcategorystatebycatid(string vendorname, int catid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcategorystatebycatid";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = catid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;

    }

    public static DataTable getcategorycitybycatid(string vendorname, int catid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcategorycitybycatid";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@vendorname";
        param.Value = vendorname;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = catid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;

    }

    public static DataTable getspprocgetvaluesubCategory(int catid,string CatN)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getvaluesubCategory";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = catid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@CatN";
        param.Value = CatN;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        DataTable table = GenericDataAccess.ExecuteSelectCommand(comm);
        return table;

    }

    public static DataTable getcategoryiddetails(int categoryid)
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "sp_proc_getcategorydetails";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@categoryid";
        param.Value = categoryid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        // execute the stored procedure and return the results
        // return GenericDataAccess.ExecuteSelectCmd(comm);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }


    public static DataTable getcategoryidpackageByproductID(int productid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getcategoryidpackageByproductID";
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

    public static DataTable selectproductimage(int productid)
    {
        //get confired DbCommand Object
        DbCommand comm = GenericDataAccess.CreateCommand();
        //set the stored procedure
        comm.CommandText = "sp_proc_selectproductimage";
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


    public static DataTable getcategoryidbyproduct(int productid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getcategoryidbyproduct";
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

    public static DataTable getcategoryproduct(string productid, string categoryid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_category_getcategoryproduct";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@productid";
        paramz.Value = productid;
        paramz.DbType = DbType.String;
        commd.Parameters.Add(paramz);

        paramz = commd.CreateParameter();
        paramz.ParameterName = "@categoryid";
        paramz.Value = categoryid;
        paramz.DbType = DbType.String;
        commd.Parameters.Add(paramz);

        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

    public static DataTable getparentcategoryidbyproduct(int productid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getparentcategoryidbyproduct";
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
     //new method on 28th 
    public static DataTable getallcatsbyhomestatus()
      {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getallcategorys";
       //execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
      }

    public static DataTable getproductidbycatidsdisplayhome(string catid)
     {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_getmoviebycatgoryid";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@catgid";
        paramz.Value = catid;
        paramz.DbType = DbType.String;
        commd.Parameters.Add(paramz);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
     }
    public static DataTable geteventsbycatidsdisplayhome(string catid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_geteventsbycatgoryid";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@catgid";
        paramz.Value = catid;
        paramz.DbType = DbType.String;
        commd.Parameters.Add(paramz);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

    //sagar added below method for task 16dec2017
    public static DataTable geteventsbycatidsdisplayhome1()
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_geteventsbycatgoryid1";
        // create a new parameter
        //DbParameter paramz = commd.CreateParameter();
        //paramz.ParameterName = "@catgid";
        //paramz.Value = catid;
        //paramz.DbType = DbType.String;
        //commd.Parameters.Add(paramz);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }


    public static DataTable getmyevents(string catid,string username)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_getmyevents";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@catgid";
        paramz.Value = catid;
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


    //sagar added below method for task 16dec2017
    public static DataTable getmyevents1(string username)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_getmyevents1";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        //paramz.ParameterName = "@catgid";
        //paramz.Value = catid;
        //paramz.DbType = DbType.String;
        //commd.Parameters.Add(paramz);

        paramz = commd.CreateParameter();
        paramz.ParameterName = "@username";
        paramz.Value = username;
        paramz.DbType = DbType.String;
        commd.Parameters.Add(paramz);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

    public static DataTable getgrouptextpost(string catid, string gid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_getgrouptextbycatidgid";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz = commd.CreateParameter();
        paramz.ParameterName = "@catgid";
        paramz.Value = catid;
        paramz.DbType = DbType.String;
        commd.Parameters.Add(paramz);

        paramz = commd.CreateParameter();
        paramz.ParameterName = "@gid";
        paramz.Value = gid;
        paramz.DbType = DbType.String;
        commd.Parameters.Add(paramz);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

    public static DataTable getsubcategorylistthreelevel()
    {
        // get a configured DbCommand object
        DbCommand comm = GenericDataAccess.CreateCommand();
        // set the stored procedure name
        comm.CommandText = "usp_getsubcategorylistthreelevel";
        // execute the stored procedure and return the results
        // return GenericDataAccess.ExecuteSelectCmd(comm);
        return GenericDataAccess.ExecuteSelectCommand(comm);

    }
    public static DataTable getallcatsbyviewstatus()
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getallcategorybyviewstatus";
        //execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }
    public static DataTable getposttypebycatid(string catid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getcattype_catid";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@catid";
        paramz.Value = catid;
        paramz.DbType = DbType.String;
        commd.Parameters.Add(paramz);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

    public static DataTable getposttypeByCatidAndStatus(string catid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getcattype_BycatidandViewsattus";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@catid";
        paramz.Value = catid;
        paramz.DbType = DbType.String;
        commd.Parameters.Add(paramz);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }
    /* code added by krishna for different category id under particular post such as doc img video txt */

    public static DataTable getgrpforuser(string emailid)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getgroupforpost";
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@username";
        paramz.Value = emailid;
        paramz.DbType = DbType.String;
        commd.Parameters.Add(paramz);
        //execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }


    public static DataTable getallcatsfordoc()
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getallcategoryfordoc";
        //execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

    public static DataTable getAllCategoryByCattype(string cattype)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getAllCategoryUsingCattype";
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@cattype";
        paramz.Value = cattype;
        paramz.DbType = DbType.String;
        commd.Parameters.Add(paramz);
        //execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

     public static DataTable getallsurveylist()
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_topSurveyList";
        //execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

    
    public static DataTable getallcatsforimg()
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getallcategoryforimg";
        //execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

    public static DataTable getallcatsforads()
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getallcategoryforads";
        //execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

    public static DataTable getallcatsforvideo()
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getallcategoryforvideo";
        //execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }

    public static DataTable getCategoryByName(string catname)
    {
        DbCommand commd = GenericDataAccess.CreateCommand();
        commd.CommandText = "sp_proc_getCategoryByName";
        // create a new parameter
        DbParameter paramz = commd.CreateParameter();
        paramz.ParameterName = "@catname";
        paramz.Value = catname;
        paramz.DbType = DbType.String;
        commd.Parameters.Add(paramz);
        // execute the stored procedure
        DataTable dtc = GenericDataAccess.ExecuteSelectCommand(commd);
        return dtc;
    }
    public static DataTable gettaskpost(string catid)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_gettaskpost";
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@catid";
        param.Value = catid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }
	
	 public static DataSet getallIdeaCentral( int PageIndex, int PageSize, out int RecordCount)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = "sp_proc_getallIdeaCentral";
        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@PageIndex";
        param.Value = PageIndex;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
       // param = comm.CreateParameter();
        //param.ParameterName = "@username";
        //param.Value = projecttitle;
        //param.DbType = DbType.String;
       // comm.Parameters.Add(param);

        // create a new parameter
       // param = comm.CreateParameter();
       // param.ParameterName = "@catgid";
       // param.Value = projectcatid;
       // param.DbType = DbType.String;
       // comm.Parameters.Add(param);

        // create a new parameter
        //param = comm.CreateParameter();
        //param.ParameterName = "@projectstatusid";
        //param.Value = projectstatusid;
        //param.DbType = DbType.String;
        //comm.Parameters.Add(param);

        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@PageSize";
        param.Value = PageSize;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);
        // create a new parameter
        param = comm.CreateParameter();
        param.ParameterName = "@RecordCount";
        param.Value = PageSize;
        param.DbType = DbType.Int32;
        param.Direction = ParameterDirection.Output;
        comm.Parameters.Add(param);



        DataSet dt = GenericDataAccess.ExecuteSelectCmd(comm);
        RecordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
        return dt;
    }

	

}
